using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FS.HISFC.Models.Order;

namespace FS.SOC.Local.Order.SubFeeSet.GYSY
{
    /// <summary>
    /// [功能描述: 广医四院门诊附材带出接口实现 ]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-03]<br></br>
    /// 说明：
    /// 1、这个接口是在医生站调用，目前是在保存函数的事务中调用的，启用全局事务，注意不可以在此回滚或提交
    /// 2、接口对传入参数不可以更改其值，仅对ref，out可以
    /// 3、医嘱删除问题：
    ///    医嘱的删除和附材的删除分开：附材的带出是接口处理的，那么附材的删除也应该由接口处理
    ///    目前医生站附材删除是根据组合号判断附材是否为医嘱的带出费用，如果带出的费用和医嘱组合不同，则删除不了附材
    /// 4、此函数中返回的附材列表只是项目和数量等信息，医嘱实体有调用出自己创建
    /// 
    /// 
    /// 注意：
    /// 1、对于是否符合规则的情况，要考虑 组合内有些项目符合 有些不符合
    /// </summary>>
    public class SubFeeSetGYSY : FS.HISFC.BizProcess.Interface.Order.IDealSubjob
    {
        #region 变量
        private static Dictionary<string, FS.HISFC.Models.Fee.Item.Undrug> dictionaryItem = new Dictionary<string, FS.HISFC.Models.Fee.Item.Undrug>();
        private static Dictionary<string, string> hsItem = new Dictionary<string, string>();
        //收取试管数量
        Dictionary<string, int> MapCuvetteCounts = new Dictionary<string, int>();

        /// <summary>
        /// 用法带出的项目是否允许弹出选择
        /// 避免效率太慢，一般情况下不需要
        /// </summary>
        bool isAllowUsageSubPopChoose = false;

        /// <summary>
        /// 用法带出的项目是否允许弹出选择
        /// 避免效率太慢，一般情况下不需要
        /// </summary>
        public bool IsAllowUsageSubPopChoose
        {
            set
            {
                isAllowUsageSubPopChoose = value;
            }
            get
            {
                return isAllowUsageSubPopChoose;
            }
        }


        FS.HISFC.BizLogic.Order.OutPatient.Order outOrderMgr = new FS.HISFC.BizLogic.Order.OutPatient.Order();
        
        FS.HISFC.BizProcess.Integrate.Fee inteFeeMgr = new FS.HISFC.BizProcess.Integrate.Fee();
        
        FS.HISFC.BizProcess.Integrate.Order orderIntegrate = new FS.HISFC.BizProcess.Integrate.Order();

        FS.HISFC.BizLogic.Order.SubtblManager subMgr = new FS.HISFC.BizLogic.Order.SubtblManager();

        FS.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 药品性质帮助类
        /// </summary>
        FS.FrameWork.Public.ObjectHelper quaulityHelper = null;

        /// <summary>
        /// 附材收费日志管理类
        /// </summary>
        private SubFeeManager subFeeLogMgr = new SubFeeManager();

        /// <summary>
        /// 判断是否儿童的年龄限制
        /// </summary>
        int childrenAge = -1;

        /// <summary>
        /// 医嘱按照开立顺序排序
        /// </summary>
        OrderCompareBySortID orrderCompare = new OrderCompareBySortID();

        /// <summary>
        /// 系统用法
        /// </summary>
        string sysUsage = "";

        /// <summary>
        /// 要收取的附材的日期
        /// </summary>
        private DateTime feeDate = System.DateTime.Now;

        /// <summary>
        /// 要收取的附材的日期
        /// </summary>
        public DateTime FeeDate
        {
            get
            {
                return feeDate;
            }
            set
            {
                feeDate = value;
            }
        }

        /// <summary>
        /// 附材处理模式，0 保存后自动带出；1 界面点击计算，显示在界面上允许修改
        /// </summary>
        private int dealSublMode = -1;

        /// <summary>
        /// 是否弹出选择
        /// </summary>
        private bool isPopForChose = false;

        /// <summary>
        /// 是否弹出选择
        /// </summary>
        public bool IsPopForChose
        {
            get
            {
                return isPopForChose;
            }
            set
            {
                isPopForChose = value;
            }
        }

        /// <summary>
        /// 药品性质帮助类
        /// </summary>
        private static FS.FrameWork.Public.ObjectHelper drugQuaulityHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 药品实体
        /// </summary>
        FS.HISFC.Models.Pharmacy.Item phaItemObj = null;

        /// <summary>
        /// 非药品实体
        /// </summary>
        FS.HISFC.Models.Base.Item undrugItemObj = null;

        /// <summary>
        /// 门诊当前医嘱，用于创建接瓶费医嘱
        /// </summary>
        private FS.HISFC.Models.Order.OutPatient.Order curentOrder = null;

        /// <summary>
        /// 所有附材信息
        /// </summary>
        Hashtable hsSubList = new Hashtable();

        /// <summary>
        /// 第一组收取的医嘱组合号
        /// </summary>
        Hashtable hsFirstFeeOrderCombNos = new Hashtable();

        /// <summary>
        /// 存放已经收取过的用法类别
        /// </summary>
        Hashtable hsFeeOrderUsages = new Hashtable();

        /// <summary>
        /// 存放第一组收取的用法类别
        /// </summary>
        Hashtable hsFirstFeeOrderUsages = new Hashtable();

        /// <summary>
        /// 第二组加收的项目
        /// </summary>
        Hashtable hsSecondFeeOrder = new Hashtable();

        /// <summary>
        /// 存放药品基本信息
        /// </summary>
        private Hashtable hsPhaItem = new Hashtable();

        /// <summary>
        /// 存放非药品项目信息
        /// </summary>
        private Hashtable hsUndrugItem = new Hashtable();

        /// <summary>
        /// 存放附材收费日志
        /// </summary>
        private Hashtable hsSubFeeLog = new Hashtable();

        /// <summary>
        /// 门诊附材医嘱
        /// </summary>
        FS.HISFC.Models.Order.OutPatient.Order outSubOrder = null;

        /// <summary>
        /// 剂型帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper dosageFormHelper = null;

        /// <summary>
        /// 不收取附材的科室
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper notSubDeptHelper = null;

        #endregion

        #region 通用函数        

        /// <summary>
        /// 是否是儿童
        /// </summary>
        /// <param name="birthday"></param>
        /// <returns>false 不是儿童</returns>
        private bool CheckIsChildren(DateTime birthday)
        {
            if(this.feeDate .AddYears (-childrenAge ).Date <birthday .Date )
            //if (this.feeDate.AddYears(childrenAge).Date < birthday.Date)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 是否大输液
        /// </summary>
        /// <param name="qualityID"></param>
        /// <returns></returns>
        private bool CheckIsSubPha(string qualityID)
        {
            if (drugQuaulityHelper == null || drugQuaulityHelper.ArrayObject.Count == 0)
            {
                
                //取药品剂型
                ArrayList alDrugQuaulity = managerIntegrate.GetConstantList("DRUGQUALITY");
                if (alDrugQuaulity == null)
                {
                    return true;
                }
                drugQuaulityHelper.ArrayObject = alDrugQuaulity;
            }

            if (((FS.HISFC.Models.Base.Const)drugQuaulityHelper.GetObjectFromID(qualityID)).UserCode.Trim() == "T"
                || (drugQuaulityHelper.GetObjectFromID(qualityID)).Memo.Contains("大输液"))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取组内品种数
        /// </summary>
        /// <param name="alOneCombo"></param>
        /// <param name="combNo"></param>
        /// <returns></returns>
        private decimal GetFeeCountByCombOrders(ArrayList alOneCombo, string combNo)
        {
            int comboQty = 0;
            foreach (FS.HISFC.Models.Order.Order order in alOneCombo)
            {
                if (order.Combo.ID == combNo)
                {
                    if (!this.CheckIsMainDrug(order))
                    {
                        continue;
                    }
                    //考虑到每次用量*频次*天数!=总量的情况
                    //int days = FS.FrameWork.Function.NConvert.ToInt32(order.HerbalQty);
                    //comboQty += days;
                    comboQty += 1;
                }
            }
            return comboQty;
        }

        /// <summary>
        /// 获取组内医嘱数量的合计
        /// </summary>
        /// <param name="alOneCombo"></param>
        /// <param name="combNo"></param>
        /// <param name="subPhaFlag">0 全部；1 大输液；2 非大输液</param>
        /// <returns></returns>
        private decimal GetTotalCountByCombOrders(ArrayList alOneCombo, string combNo, string subPhaFlag)
        {
            decimal comboQty = 0;
            foreach (FS.HISFC.Models.Order.OutPatient.Order order in alOneCombo)
            {
                if (order.Combo.ID == combNo)
                {
                    if (subPhaFlag == "1")
                    {
                        if (this.CheckIsMainDrug(order))
                        {
                            continue;
                        }
                    }

                    comboQty += order.Qty;
                }
            }

            //增加规则，对于属于此用法的，组合只有一个固定要收取附材
            if (alOneCombo.Count == 1 && comboQty == 0)
            {
                comboQty = ((FS.HISFC.Models.Order.OutPatient.Order)alOneCombo[0]).Qty;
            }
            return comboQty;
        }

        /// <summary>
        /// 检测药品是否为主药(溶液、非药品外的药品)
        /// </summary>
        /// <param name="drugNO"></param>
        /// <returns></returns>
        private bool CheckIsMainDrug(FS.HISFC.Models.Order.Order order)
        {
            if (order.Item.ID == "999")
            {
                return true;
            }
            if (order.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                return true;
            }

            if (hsPhaItem.Contains(order.Item.ID))
            {
                phaItemObj = hsPhaItem[order.Item.ID] as FS.HISFC.Models.Pharmacy.Item;
            }
            else
            {
                phaItemObj = phaIntegrate.GetItem(order.Item.ID);
                if (phaItemObj != null)
                {
                    if (!hsPhaItem.Contains(order.Item.ID))
                    {
                        hsPhaItem.Add(order.Item.ID, phaItemObj);
                    }
                }
            }

            if (CheckIsSubPha(phaItemObj.Quality.ID))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 增加到附材列表
        /// 一般附材增加到alSubOrder，第二组加收的增加到hsSecondFeeOrder
        /// </summary>
        /// <param name="order"></param>
        /// <param name="subObj"></param>
        /// <param name="alSubOrder"></param>
        /// <param name="hsSecondFeeOrder"></param>
        /// <param name="item"></param>
        /// <param name="birthday"></param>
        /// <param name="errInfo"></param>
        /// <returns>0 不增加 1成功 -1失败</returns>
        private int AddToArrayOutPatient(FS.HISFC.Models.Order.OutPatient.Order order, OrderSubtblNew subObj, ArrayList alSubOrder, Hashtable hsSecondFeeOrder, FS.HISFC.Models.Base.Item item, DateTime birthday, ref string errInfo)
        {
            //用于处理按照应执行频次收取时，特殊频次导致的小数问题
            //如 Q3D 4天 应执行次数为 4/3
            item.Qty = Math.Ceiling(item.Qty);

            outSubOrder = null;
            try
            {
                if (notSubDeptHelper == null)
                {
                    notSubDeptHelper = new FS.FrameWork.Public.ObjectHelper();

                    ArrayList alNotSubDept = managerIntegrate.GetConstantList("NotSubDept");
                    if (alNotSubDept == null)
                    {
                        errInfo = managerIntegrate.Err;
                        return -1;
                    }
                    notSubDeptHelper.ArrayObject = alNotSubDept;
                }

                if (item.Qty <= 0)
                {
                    return 1;
                }

                //不收取附材科室
                if (notSubDeptHelper.GetObjectFromID(order.ReciptDept.ID) != null)
                {
                    return 1;
                }

                if (subObj.IsCalculateByOnceDose) 
                {
                    if (order.DoseUnit != subObj.DoseUnit)
                    {
                        return 0;
                    }
                    if (order.DoseOnce <= subObj.OnceDoseFrom || order.DoseOnce > subObj.OnceDoseTo)
                    {
                        return 0;
                    }
                }

                if (this.isPopForChose != subObj.IsAllowPopChose)
                {
                    return 0;
                }

                #region 判断有效性

                FS.HISFC.Models.Fee.Item.Undrug UndrugItem = null;

                if (!hsUndrugItem.Contains(item.ID))
                {
                    UndrugItem = inteFeeMgr.GetItem(item.ID);//获得最新项目信息
                    if (UndrugItem == null)
                    {
                        errInfo = "获取项目失败，尝试获取项目的项目编码为" + item.ID;
                        return -1;
                    }
                    hsUndrugItem.Add(item.ID, UndrugItem.Clone());
                }
                else
                {
                    UndrugItem = hsUndrugItem[item.ID] as FS.HISFC.Models.Fee.Item.Undrug;
                }
                if (!UndrugItem.IsValid)
                {
                    return 1;
                }
                #region 自备药不带药袋

                if (order.Item.ID == "999" || order.Item.Name.Contains("自备"))
                {
                    if (UndrugItem.Name.Contains("药袋"))
                    {
                        return 1;
                    }
                }

                #endregion
                #endregion

                if (hsSecondFeeOrder == null)
                {
                    //限制类别：0 不限制 1 儿童使用 2 非儿童使用
                    if ((subObj.LimitType == "0") ||
                        (subObj.LimitType == "1" && CheckIsChildren(birthday)) ||
                        (subObj.LimitType == "2" && !CheckIsChildren(birthday)))
                    {
                        outSubOrder = this.CreateOrder(order, item.ID, item.Qty, ref errInfo);
                        if (outSubOrder == null)
                        {
                            return -1;
                        }
                        alSubOrder.Add(outSubOrder);
                    }
                }
                else
                {
                    if (hsSecondFeeOrder.Contains(item.ID))
                    {
                        decimal qty = item.Qty;
                        item = hsSecondFeeOrder[item.ID] as FS.HISFC.Models.Base.Item;

                        //限制类别：0 不限制 1 儿童使用 2 非儿童使用
                        if (subObj.LimitType == "0")
                        {
                            item.Qty += qty;
                        }
                        else if (subObj.LimitType == "1" && CheckIsChildren(birthday))
                        {
                            item.Qty += qty;
                        }
                        else if (subObj.LimitType == "2" && !CheckIsChildren(birthday))
                        {
                            item.Qty += qty;
                        }
                    }
                    else
                    {
                        hsSecondFeeOrder.Add(item.ID, item);
                    }
                }
            }
            catch (Exception ex)
            {
                errInfo = ex.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 增加到附材列表
        /// 一般附材增加到alSubOrder，第二组加收的增加到hsSecondFeeOrder
        /// </summary>
        /// <param name="inOrder"></param>
        /// <param name="limitType"></param>
        /// <param name="alSubOrder"></param>
        /// <param name="hsSecondFeeOrder"></param>
        /// <param name="item"></param>
        /// <param name="birthday"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        private int AddToArray(FS.HISFC.Models.Order.Inpatient.Order inOrder, OrderSubtblNew subObj, ArrayList alSubOrder, Hashtable hsSecondFeeOrder, ArrayList alExecOrders, FS.HISFC.Models.Base.Item item, DateTime birthday, ref string errInfo)
        {
            //用于处理按照应执行频次收取时，特殊频次导致的小数问题
            //如 Q3D 4天 应执行次数为 4/3
            item.Qty = Math.Ceiling(item.Qty);

            try
            {
                if (notSubDeptHelper == null)
                {
                    notSubDeptHelper = new FS.FrameWork.Public.ObjectHelper();

                    ArrayList alNotSubDept = managerIntegrate.GetConstantList("NotSubDept");
                    if (alNotSubDept == null)
                    {
                        errInfo = managerIntegrate.Err;
                        return -1;
                    }
                    notSubDeptHelper.ArrayObject = alNotSubDept;
                }

                if (item.Qty <= 0)
                {
                    return 1;
                }

                //不收取附材科室
                if (notSubDeptHelper.GetObjectFromID(inOrder.ReciptDept.ID) != null)
                {
                    return 1;
                }

                if (this.isPopForChose != subObj.IsAllowPopChose)
                {
                    return 1;
                }

                //医嘱类别：0 全部 1 长嘱 2 临嘱
                if ((inOrder.OrderType.IsDecompose && subObj.OrderType == "2")
                    || (!inOrder.OrderType.IsDecompose && subObj.OrderType == "1"))
                {
                    return 1;
                }

                //长嘱特殊处理
                if (inOrder.OrderType.IsDecompose)
                {
                    //增加长嘱是否首次分解收取 限制
                    if (subObj.FirstFeeFlag == "1")
                    {
                        if (subFeeLogMgr.QuerySubFeeLog(inOrder.Patient.ID, inOrder.ID) != null)
                        {
                            return 1;
                        }
                    }
                }

                if (subObj.IsCalculateByOnceDose)
                {
                    if (inOrder.DoseUnit != subObj.DoseUnit)
                    {
                        return 0;
                    }
                    if (inOrder.DoseOnce <= subObj.OnceDoseFrom || inOrder.DoseOnce > subObj.OnceDoseTo)
                    {
                        return 0;
                    }
                }

                FS.HISFC.Models.Fee.Item.Undrug UndrugItem = null;

                if (!hsPhaItem.Contains(item.ID))
                {
                    UndrugItem = inteFeeMgr.GetItem(item.ID);//获得最新项目信息
                    if (UndrugItem == null)
                    {
                        errInfo = "获取项目失败，尝试获取项目的项目编码为" + item.ID;
                        return -1;
                    }
                    hsPhaItem.Add(item.ID, UndrugItem.Clone());
                }
                else
                {
                    UndrugItem = hsPhaItem[item.ID] as FS.HISFC.Models.Fee.Item.Undrug;
                }
                if (!UndrugItem.IsValid)
                {
                    return 1;
                }

                UndrugItem.Qty = item.Qty;

                if (hsSecondFeeOrder == null)
                {
                    //限制类别：0 不限制 1 儿童使用 2 非儿童使用
                    if (subObj.LimitType == "0")
                    {
                        alSubOrder.Add(UndrugItem.Clone());
                    }
                    else if (subObj.LimitType == "1" && CheckIsChildren(birthday))
                    {
                        alSubOrder.Add(UndrugItem.Clone());
                    }
                    else if (subObj.LimitType == "2" && !CheckIsChildren(birthday))
                    {
                        alSubOrder.Add(UndrugItem.Clone());
                    }
                }
                else
                {
                    if (hsSecondFeeOrder.Contains(UndrugItem.ID))
                    {
                        decimal qty = UndrugItem.Qty;
                        UndrugItem = hsSecondFeeOrder[UndrugItem.ID] as FS.HISFC.Models.Fee.Item.Undrug;

                        //限制类别：0 不限制 1 儿童使用 2 非儿童使用
                        if (subObj.LimitType == "0")
                        {
                            UndrugItem.Qty += qty;
                        }
                        else if (subObj.LimitType == "1" && CheckIsChildren(birthday))
                        {
                            UndrugItem.Qty += qty;
                        }
                        else if (subObj.LimitType == "2" && !CheckIsChildren(birthday))
                        {
                            UndrugItem.Qty += qty;
                        }
                    }
                    else
                    {
                        hsSecondFeeOrder.Add(UndrugItem.ID, UndrugItem.Clone());
                    }
                }
            }
            catch (Exception ex)
            {
                errInfo = ex.Message;
                return -1;
            }
            return 1;
        }


        #endregion

        #region 门诊附材处理

        public int DealSubjob(FS.HISFC.Models.Registration.Register regInfo, System.Collections.ArrayList alOrders, FS.HISFC.Models.Order.OutPatient.Order outOrder, ref System.Collections.ArrayList alSubOrders, ref string errInfo)
        {
            return this.DealSubjobOutPatient(regInfo, alOrders, ref alSubOrders, ref errInfo);
        }

        /// <summary>
        /// 本地化特殊处理
        /// </summary>
        /// <param name="orderObj"></param>
        /// <param name="alCombOrder"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private int DealSpecial(FS.HISFC.Models.Order.Order orderObj, ArrayList alCombOrder, FS.HISFC.Models.Base.ServiceTypes type)
        {
            #region 桥头的特殊处理

            if (dosageFormHelper == null)
            {
                dosageFormHelper = new FS.FrameWork.Public.ObjectHelper();
                dosageFormHelper.ArrayObject = managerIntegrate.GetConstantList("20ML#DOSAGEFORM");
            }

            //广医四院基本2mg
            //排除大输液
            decimal doseOnce = 0;
            FS.HISFC.Models.Pharmacy.Item phaItem = null;

            foreach (FS.HISFC.Models.Order.Order obj in alCombOrder)
            {
                if (obj.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    continue;
                }
                decimal doseTemp = 0;

                if (obj.Item.ID == "999")
                {
                    if (obj.DoseUnit.Trim() == "ml")
                    {
                        doseOnce = obj.DoseOnce;
                    }
                }
                else
                {

                    if (hsPhaItem.Contains(obj.Item.ID))
                    {
                        phaItem = hsPhaItem[obj.Item.ID] as FS.HISFC.Models.Pharmacy.Item;
                    }
                    else
                    {
                        phaItem = phaIntegrate.GetItem(obj.Item.ID);//获得最新项目信息
                        if (phaItem != null)
                        {
                            hsPhaItem.Add(obj.Item.ID, phaItem);
                        }
                    }
                    if (phaItem != null)
                    {
                        if (CheckIsSubPha(phaItem.Quality.ID))
                        {
                            continue;
                        }

                        if (!phaItem.ExtNumber2.Equals(0))
                        {
                            doseOnce = phaItem.ExtNumber2 * obj.DoseOnce / phaItem.BaseDose;
                        }

                        if (!object.Equals(dosageFormHelper.GetObjectFromID(phaItem.DosageForm.ID),null))
                        {
                            //2011-10-30 修改 粉针剂 固定带出20ml注射器
                            doseOnce = 20;
                            break;
                        }
                    }
                }

            }

            if (doseOnce > 0)
            {
                orderObj.DoseOnce = doseOnce;
                orderObj.DoseUnit = "ml";
            }
            else
            {
                orderObj.DoseUnit = "哈哈";
            }

            #endregion

            return 1;
        }

        /// <summary>
        /// 门诊根据用法收取附材
        /// </summary>
        /// <param name="usage"></param>
        /// <param name="alOrders"></param>
        /// <param name="regInfo"></param>
        /// <param name="alSubOrders"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        private int DealSubjobByUsageForOutPatient(ArrayList alOrders, FS.HISFC.Models.Registration.Register regInfo, ref ArrayList alSubOrders, ref string errInfo)
        {
            ArrayList alSubInfo = new ArrayList();
            alSubOrders = new ArrayList();
            hsFirstFeeOrderCombNos = new Hashtable();
            hsFirstFeeOrderUsages = new Hashtable();
            hsFeeOrderUsages = new Hashtable();
            hsSecondFeeOrder = new Hashtable();

            Hashtable hsCombOrder = new Hashtable();

            //按照组合分组
            foreach (FS.HISFC.Models.Order.OutPatient.Order orderObj in alOrders)
            {
                if (orderObj.IsSubtbl)
                {
                    continue;
                }
                if (!hsCombOrder.Contains(orderObj.Combo.ID))
                {
                    ArrayList al = new ArrayList();
                    al.Add(orderObj.Clone());
                    hsCombOrder.Add(orderObj.Combo.ID, al);
                }
                else
                {
                    ((ArrayList)hsCombOrder[orderObj.Combo.ID]).Add(orderObj.Clone());
                }
            }

            int rev = -1;

            //存放所有带出的附材项目
            Hashtable hsSublItem = new Hashtable();

            ArrayList alAllCombOrder = new ArrayList(hsCombOrder.Values);
            alAllCombOrder.Sort(new OutCombOrderCompareByFreqAndDays());

            foreach (ArrayList alCombOrder in alAllCombOrder)
            {
                if (alCombOrder.Count == 0)
                {
                    continue;
                }

                //这里用克隆吧，后续可能修改里面的内容
                FS.HISFC.Models.Order.OutPatient.Order orderObj = (alCombOrder[0] as FS.HISFC.Models.Order.OutPatient.Order).Clone();

                #region 本地化特殊处理

                rev = DealSpecial(orderObj, alCombOrder, FS.HISFC.Models.Base.ServiceTypes.C);
                if (rev == -1)
                {
                    return -1;
                }
                else if (rev == 0)
                {
                    continue;
                    //return 1;
                }

                #endregion

                #region 根据用法的附材带出

                if (orderObj.Usage != null && !string.IsNullOrEmpty(orderObj.Usage.ID))
                {
                    alSubInfo = hsSubList[orderObj.Usage.ID] as ArrayList;

                    if (alSubInfo == null)
                    {
                        continue;
                    }
                    else if (alSubInfo.Count == 0)
                    {
                        continue;
                    }

                    sysUsage = Function.GetSysType(orderObj.Usage.ID, ref errInfo);
                    if (sysUsage == null)
                    {
                        return -1;
                    }

                    foreach (OrderSubtblNew subObj in alSubInfo)
                    {
                        undrugItemObj = null;

                        if (subObj.Area == "1")
                        {
                            continue;
                        }
                        if (orderObj.ReciptDept.ID == subObj.Dept_code || subObj.Dept_code == "ROOT")
                        {
                            undrugItemObj = new FS.HISFC.Models.Base.Item();
                            undrugItemObj.ID = subObj.Item.ID;

                            //0 每组收取
                            if (subObj.CombArea == "0")
                            {
                                #region 每组收取

                                switch (subObj.FeeRule)
                                {
                                    //0 固定数量
                                    case "0":
                                        undrugItemObj.Qty = subObj.Qty;
                                        break;
                                    //1 最大院注
                                    case "1":
                                        undrugItemObj.Qty = orderObj.InjectCount * subObj.Qty;
                                        break;
                                    //2 组内品种数
                                    case "2":
                                        undrugItemObj.Qty = GetFeeCountByCombOrders(alOrders, orderObj.Combo.ID) * subObj.Qty;
                                        break;
                                    //3 医嘱执行次数
                                    case "3":
                                        undrugItemObj.Qty = subObj.Qty * orderObj.HerbalQty *Function.GetFrequencyCountByOneDay(orderObj.Frequency.ID);
                                        break;
                                    //4 频次数
                                    case "4":
                                        undrugItemObj.Qty =Function.GetFrequencyCountByOneDay(orderObj.Frequency.ID) * subObj.Qty;
                                        break;
                                    //5 组内数量合计 目前只用于接瓶费了
                                    case "5":
                                        undrugItemObj.Qty = GetTotalCountByCombOrders(alOrders, orderObj.Combo.ID, "1") * subObj.Qty;
                                        break;
                                }

                                //是否允许重复收费
                                if (!(hsSublItem.Contains(undrugItemObj.ID) 
                                    && !subObj.IsAllowReFee))
                                {
                                    rev = this.AddToArrayOutPatient(orderObj, subObj, alSubOrders, null, undrugItemObj, regInfo.Birthday, ref errInfo);
                                    if (rev == -1)
                                    {
                                        return -1;
                                    }
                                    else if (rev == 0)
                                    {
                                        continue;
                                    }
                                }
                                if (!hsSublItem.Contains(undrugItemObj.ID))
                                {
                                    hsSublItem.Add(undrugItemObj.ID, undrugItemObj);
                                }

                                #endregion
                            }

                            else if (subObj.CombArea == "1")
                            {
                                //如果院注次数为0 ，则不再收取第一组收取的东东
                                if (orderObj.InjectCount == 0)
                                {
                                    continue;
                                }

                                #region 第一组收取
                                //此处取所有相同用法的最大数量

                                if (!this.hsFirstFeeOrderUsages.Contains(sysUsage)
                                        || hsFirstFeeOrderCombNos.Count == 0
                                        || hsFirstFeeOrderCombNos.Contains(orderObj.Combo.ID)
                                    )
                                {
                                    switch (subObj.FeeRule)
                                    {
                                        //0 固定数量
                                        case "0":
                                            undrugItemObj.Qty = subObj.Qty;
                                            break;
                                        //1 最大院注
                                        case "1":
                                            undrugItemObj.Qty = orderObj.InjectCount * subObj.Qty;
                                            break;
                                        //2 组内品种数
                                        case "2":
                                            undrugItemObj.Qty = GetFeeCountByCombOrders(alOrders, orderObj.Combo.ID) * subObj.Qty;
                                            break;
                                        //3 医嘱执行次数
                                        case "3":
                                            undrugItemObj.Qty = subObj.Qty * orderObj.HerbalQty *Function.GetFrequencyCountByOneDay(orderObj.Frequency.ID);
                                            break;
                                        //4 频次数
                                        case "4":
                                            undrugItemObj.Qty =Function.GetFrequencyCountByOneDay(orderObj.Frequency.ID) * subObj.Qty;
                                            break;
                                        //5 组内数量合计 目前只用于接瓶费了
                                        case "5":
                                            undrugItemObj.Qty = GetTotalCountByCombOrders(alOrders, orderObj.Combo.ID, "1") * subObj.Qty;
                                            break;
                                    }

                                    //是否允许重复收费
                                    if (!(hsSublItem.Contains(undrugItemObj.ID) && !subObj.IsAllowReFee))
                                    {
                                        rev = this.AddToArrayOutPatient(orderObj, subObj, alSubOrders, null, undrugItemObj, regInfo.Birthday, ref errInfo);
                                        if (rev == -1)
                                        {
                                            return -1;
                                        }
                                        else if (rev == 0)
                                        {
                                            continue;
                                        }
                                        if (!hsFirstFeeOrderCombNos.Contains(orderObj.Combo.ID))
                                        {
                                            hsFirstFeeOrderCombNos.Add(orderObj.Combo.ID, orderObj.Clone());
                                        }

                                        if (!hsFirstFeeOrderUsages.Contains(sysUsage))
                                        {
                                            hsFirstFeeOrderUsages.Add(sysUsage, null);
                                        }
                                    }
                                    if (!hsSublItem.Contains(undrugItemObj.ID))
                                    {
                                        hsSublItem.Add(undrugItemObj.ID, undrugItemObj);
                                    }
                                }

                                #endregion
                            }

                            else if (subObj.CombArea == "2")
                            {
                                #region 第二组加收

                                //此处限制 只有第一组收取的时候才能收取第二组加收
                                if (hsFeeOrderUsages.Contains(sysUsage)
                                    && (hsFirstFeeOrderCombNos.Count > 0 && !hsFirstFeeOrderCombNos.Contains(orderObj.Combo.ID))
                                    )
                                {
                                    switch (subObj.FeeRule)
                                    {
                                        //0 固定数量
                                        case "0":
                                            undrugItemObj.Qty = subObj.Qty;
                                            break;
                                        //1 最大院注
                                        case "1":
                                            undrugItemObj.Qty = orderObj.InjectCount * subObj.Qty;
                                            break;
                                        //2 组内品种数
                                        case "2":
                                            undrugItemObj.Qty = GetFeeCountByCombOrders(alOrders, orderObj.Combo.ID) * subObj.Qty;
                                            break;
                                        //3 医嘱执行次数
                                        case "3":
                                            undrugItemObj.Qty = subObj.Qty * orderObj.HerbalQty *Function.GetFrequencyCountByOneDay(orderObj.Frequency.ID);
                                            break;
                                        //4 频次数
                                        case "4":
                                            undrugItemObj.Qty =Function.GetFrequencyCountByOneDay(orderObj.Frequency.ID) * subObj.Qty;
                                            break;
                                        //5 组内数量合计 目前只用于接瓶费了
                                        case "5":
                                            undrugItemObj.Qty = GetTotalCountByCombOrders(alOrders, orderObj.Combo.ID, "1") * subObj.Qty;
                                            break;
                                    }

                                    this.curentOrder = orderObj.Clone();


                                    //是否允许重复收费
                                    if (!(hsSublItem.Contains(undrugItemObj.ID) && !subObj.IsAllowReFee))
                                    {
                                        rev = this.AddToArrayOutPatient(orderObj, subObj, alSubOrders, hsSecondFeeOrder, undrugItemObj, regInfo.Birthday, ref errInfo);
                                        if (rev == -1)
                                        {
                                            return -1;
                                        }
                                        else if (rev == 0)
                                        {
                                            continue;
                                        }
                                    }
                                    if (!hsSublItem.Contains(undrugItemObj.ID))
                                    {
                                        hsSublItem.Add(undrugItemObj.ID, undrugItemObj);
                                    }
                                }
                                #endregion
                            }

                            if (!hsFeeOrderUsages.Contains(sysUsage))
                            {
                                hsFeeOrderUsages.Add(sysUsage, orderObj.Clone());
                            }
                        }
                    }
                }

                #endregion
            }

            foreach (FS.HISFC.Models.Order.OutPatient.Order orderObj in alOrders)
            {
                if (orderObj.IsSubtbl)
                {
                    continue;
                }

                #region 项目带出附材
                //药品、非药品都允许根据项目带出附材

                alSubInfo = hsSubList[orderObj.Item.ID] as ArrayList;
                if (alSubInfo == null)
                {
                    continue;
                }
                else if (alSubInfo.Count == 0)
                {
                    continue;
                }

                foreach (OrderSubtblNew subObj in alSubInfo)
                {
                    if (subObj.Area == "1")
                    {
                        continue;
                    }
                    if (orderObj.ReciptDept.ID == subObj.Dept_code || subObj.Dept_code == "ROOT")
                    {
                        undrugItemObj = new FS.HISFC.Models.Base.Item();
                        undrugItemObj.ID = subObj.Item.ID;

                        if (subObj.CombArea == "0")
                        {
                            #region 每组收取

                            switch (subObj.FeeRule)
                            {
                                //0 固定数量
                                case "0":
                                    undrugItemObj.Qty = subObj.Qty;
                                    break;
                                //1 最大院注
                                case "1":
                                    undrugItemObj.Qty = orderObj.InjectCount * subObj.Qty;
                                    break;
                                //2 组内品种数
                                case "2":
                                    undrugItemObj.Qty = GetFeeCountByCombOrders(alOrders, orderObj.Combo.ID) * subObj.Qty;
                                    break;
                                //3 医嘱执行次数
                                case "3":
                                    undrugItemObj.Qty = subObj.Qty * orderObj.HerbalQty *Function.GetFrequencyCountByOneDay(orderObj.Frequency.ID);
                                    break;
                                //4 频次数
                                case "4":
                                    undrugItemObj.Qty =Function.GetFrequencyCountByOneDay(orderObj.Frequency.ID) * subObj.Qty;
                                    break;
                                //5 组内数量合计 目前只用于接瓶费了
                                case "5":
                                    undrugItemObj.Qty = GetTotalCountByCombOrders(alOrders, orderObj.Combo.ID, "1") * subObj.Qty;
                                    break;
                            }

                            //是否允许重复收费
                            if (!(hsSublItem.Contains(undrugItemObj.ID) && !subObj.IsAllowReFee))
                            {
                                try
                                {
                                    rev = this.AddToArrayOutPatient(orderObj, subObj, alSubOrders, null, undrugItemObj, regInfo.Birthday, ref errInfo);
                                }
                                catch { }
                                if (rev == -1)
                                {
                                    return -1;
                                }
                                else if (rev == 0)
                                {
                                    continue;
                                }
                            }
                            if (!hsSublItem.Contains(undrugItemObj.ID))
                            {
                                hsSublItem.Add(undrugItemObj.ID, undrugItemObj);
                            }

                            #endregion
                        }

                        else if (subObj.CombArea == "1")
                        {
                            #region 第一组收取

                            if (!this.hsFirstFeeOrderUsages.Contains(orderObj.Item.ID)
                                        || hsFirstFeeOrderCombNos.Count == 0
                                        || hsFirstFeeOrderCombNos.Contains(orderObj.Combo.ID)
                                )
                            {
                                switch (subObj.FeeRule)
                                {
                                    //0 固定数量
                                    case "0":
                                        undrugItemObj.Qty = subObj.Qty;
                                        break;
                                    //1 最大院注
                                    case "1":
                                        undrugItemObj.Qty = orderObj.InjectCount * subObj.Qty;
                                        break;
                                    //2 组内品种数
                                    case "2":
                                        undrugItemObj.Qty = GetFeeCountByCombOrders(alOrders, orderObj.Combo.ID) * subObj.Qty;
                                        break;
                                    //3 医嘱执行次数
                                    case "3":
                                        undrugItemObj.Qty = subObj.Qty * orderObj.HerbalQty *Function.GetFrequencyCountByOneDay(orderObj.Frequency.ID);
                                        break;
                                    //4 频次数
                                    case "4":
                                        undrugItemObj.Qty =Function.GetFrequencyCountByOneDay(orderObj.Frequency.ID) * subObj.Qty;
                                        break;
                                    //5 组内数量合计 目前只用于接瓶费了
                                    case "5":
                                        undrugItemObj.Qty = GetTotalCountByCombOrders(alOrders, orderObj.Combo.ID, "1") * subObj.Qty;
                                        break;
                                }

                                //是否允许重复收费
                                if (!(hsSublItem.Contains(undrugItemObj.ID) && !subObj.IsAllowReFee))
                                {
                                    rev = this.AddToArrayOutPatient(orderObj, subObj, alSubOrders, null, undrugItemObj, regInfo.Birthday, ref errInfo);
                                    if (rev == -1)
                                    {
                                        return -1;
                                    }
                                    else if (rev == 0)
                                    {
                                        continue;
                                    }

                                    if (!hsFirstFeeOrderCombNos.Contains(orderObj.Combo.ID))
                                    {
                                        hsFirstFeeOrderCombNos.Add(orderObj.Combo.ID, orderObj.Clone());
                                    }

                                    if (!hsFirstFeeOrderUsages.Contains(sysUsage))
                                    {
                                        hsFirstFeeOrderUsages.Add(sysUsage, null);
                                    }
                                }
                                if (!hsSublItem.Contains(undrugItemObj.ID))
                                {
                                    hsSublItem.Add(undrugItemObj.ID, undrugItemObj);
                                }
                            }
                            #endregion
                        }

                        else if (subObj.CombArea == "2")
                        {
                            #region 第二组加收

                            //第一组收取了，才收取第二组加收
                            if (this.hsFeeOrderUsages.Contains(sysUsage)
                                && (hsFirstFeeOrderCombNos.Count > 0 && !hsFirstFeeOrderCombNos.Contains(orderObj.Combo.ID))
                                )
                            {
                                switch (subObj.FeeRule)
                                {
                                    //0 固定数量
                                    case "0":
                                        undrugItemObj.Qty = subObj.Qty;
                                        break;
                                    //1 最大院注
                                    case "1":
                                        undrugItemObj.Qty = orderObj.InjectCount * subObj.Qty;
                                        break;
                                    //2 组内品种数
                                    case "2":
                                        undrugItemObj.Qty = GetFeeCountByCombOrders(alOrders, orderObj.Combo.ID) * subObj.Qty;
                                        break;
                                    //3 医嘱执行次数
                                    case "3":
                                        undrugItemObj.Qty = subObj.Qty * orderObj.HerbalQty *Function.GetFrequencyCountByOneDay(orderObj.Frequency.ID);
                                        break;
                                    //4 频次数
                                    case "4":
                                        undrugItemObj.Qty =Function.GetFrequencyCountByOneDay(orderObj.Frequency.ID) * subObj.Qty;
                                        break;
                                    //5 组内数量合计 目前只用于接瓶费了
                                    case "5":
                                        undrugItemObj.Qty = GetTotalCountByCombOrders(alOrders, orderObj.Combo.ID, "1") * subObj.Qty;
                                        break;
                                }

                                #region 加上第一组应收的次数

                                #endregion

                                //是否允许重复收费
                                if (!(hsSublItem.Contains(undrugItemObj.ID) && !subObj.IsAllowReFee))
                                {
                                    this.curentOrder = orderObj.Clone();

                                    rev = this.AddToArrayOutPatient(orderObj, subObj, alSubOrders, hsSecondFeeOrder, undrugItemObj, regInfo.Birthday, ref errInfo);
                                    if (rev == -1)
                                    {
                                        return -1;
                                    }
                                    else if (rev == 0)
                                    {
                                        continue;
                                    }
                                }
                                if (!hsSublItem.Contains(undrugItemObj.ID))
                                {
                                    hsSublItem.Add(undrugItemObj.ID, undrugItemObj);
                                }
                            }
                            #endregion
                        }

                        if (!this.hsFeeOrderUsages.Contains(sysUsage))
                        {
                            hsFeeOrderUsages.Add(sysUsage, orderObj.Clone());
                        }
                    }
                }
                #endregion
            }

            //对于第二组加收的，在此处增加到附材列表
            ArrayList alTemp = new ArrayList(hsSecondFeeOrder.Values);
            foreach (FS.HISFC.Models.Base.Item itemOjb in alTemp)
            {
                outSubOrder = new FS.HISFC.Models.Order.OutPatient.Order();
                outSubOrder = this.CreateOrder(this.curentOrder, itemOjb.ID, itemOjb.Qty, ref errInfo);
                if (outSubOrder == null)
                {
                    return -1;
                }
                alSubOrders.Add(outSubOrder);
            }

            //弹出选择
            if (this.isPopForChose)
            {
                frmChoseSublItem choseSublItem = new frmChoseSublItem();
                choseSublItem.ServerType = FS.HISFC.Models.Base.ServiceTypes.C;
                choseSublItem.AlSublOrders = alSubOrders;
                if (alSubOrders.Count > 0)
                {
                    choseSublItem.ShowDialog();
                    if (choseSublItem.DialogResult == System.Windows.Forms.DialogResult.OK)
                    {
                        alSubOrders = choseSublItem.AlSublOrders;
                    }
                    else
                    {
                        alSubOrders.Clear();
                    }
                }
            }

            #region 处理LIS试管带出

            if (!IsPopForChose)
            {
                if (InitDictionaryItem(ref errInfo) == -1)
                {
                    return -1;
                }

                string lisItem = string.Empty;
                foreach (ArrayList alTempSameCombo in alAllCombOrder)
                {
                    FS.HISFC.Models.Order.OutPatient.Order orderTemp = alTempSameCombo[0] as FS.HISFC.Models.Order.OutPatient.Order;

                    if (orderTemp == null)
                    {
                        continue;
                    }

                    if (orderTemp.Item.SysClass.ID.ToString() != "UL")
                    {
                        continue;
                    }

                    lisItem += orderTemp.Item.ID + "|";
                }

                if (!string.IsNullOrEmpty(lisItem))
                {
                    lisItem = lisItem.Remove(lisItem.Length - 1, 1);

                    //HIS项目+试管颜色
                    Dictionary<string, string> MapCuvetteItems = new Dictionary<string, string>();

                    //试管颜色+试管数量
                    Dictionary<string, int> MapCuvetteNums = new Dictionary<string, int>();

                    

                    LISSubManager orderMgr = new LISSubManager();

                    if (orderMgr.GetCuvetteItems(lisItem, ref MapCuvetteItems, ref MapCuvetteNums) == -1)
                    {
                        errInfo = "计算检验试管带出失败！" + orderMgr.Err;
                        return -1;
                    }

                    foreach (ArrayList alTempSameCombo in alAllCombOrder)
                    {
                        FS.HISFC.Models.Order.OutPatient.Order orderTemp = alTempSameCombo[0] as FS.HISFC.Models.Order.OutPatient.Order;

                        //首先判断是否应该带出试管
                        if (MapCuvetteItems.ContainsKey(orderTemp.Item.ID))
                        {
                            ////如果不存在该颜色试管，则数量默认为0
                            //if (!hsItem.ContainsKey(MapCuvetteItems[orderTemp.Item.ID]))
                            //{
                            //    errInfo = "Lis试管[" + MapCuvetteItems[orderTemp.Item.ID] + "管]对应的HIS带出项目没有维护，请联系管理员进行维护！\r\n[常数编码：OutLisCalculateItem]";
                            //    return -1;
                            //}
                            if (!MapCuvetteCounts.ContainsKey(MapCuvetteItems[orderTemp.Item.ID]))
                            {
                               // errInfo = "Lis试管[" + MapCuvetteItems[orderTemp.Item.ID] + "管]对应的HIS带出项目没有维护，请联系管理员进行维护[常数编码：OutLisCalculateItem]！";
                                //return -1;
                                continue;
                            }

                            //if (!MapCuvetteCounts.ContainsKey(MapCuvetteItems[orderTemp.Item.ID]))
                            //{
                            //    MapCuvetteCounts[MapCuvetteItems[orderTemp.Item.ID]] = 0;
                            //}
                            //else
                            //{
                            //    MapCuvetteCounts[MapCuvetteItems[orderTemp.Item.ID]]++;
                            //}

                            //判断带出试管数量有没有超出，并管的情况下，后面的医嘱不带试管。
                            if (MapCuvetteCounts[MapCuvetteItems[orderTemp.Item.ID]] < MapCuvetteNums[MapCuvetteItems[orderTemp.Item.ID]])
                            {
                                foreach (string key in dictionaryItem.Keys)
                                {

                                    string temp = key;
                                    if (key.IndexOf('[') > 0)
                                    {
                                        temp = key.Substring(0, key.IndexOf('['));
                                    }

                                    if (temp == MapCuvetteItems[orderTemp.Item.ID])
                                    {
                                        outSubOrder = new FS.HISFC.Models.Order.OutPatient.Order();
                                        outSubOrder = this.CreateOrder(orderTemp, dictionaryItem[key].ID, 1, ref errInfo);
                                        if (outSubOrder == null)
                                        {
                                            return -1;
                                        }
                                        alSubOrders.Add(outSubOrder);

                                        MapCuvetteCounts[MapCuvetteItems[orderTemp.Item.ID]]++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion

            #region 按项目合并显示
            //Hashtable hsItemTemp = new Hashtable();
            //foreach (FS.HISFC.Models.Order.OutPatient.Order outOrder in alSubOrders)
            //{
            //    if (!hsItemTemp.Contains(outOrder.Item.ID))
            //    {
            //        hsItemTemp.Add(outOrder.Item.ID, outOrder);
            //    }
            //    else
            //    {
            //        ((FS.HISFC.Models.Order.OutPatient.Order)hsItemTemp[outOrder.Item.ID]).Qty += outOrder.Qty;
            //    }
            //}

            //alSubOrders = new ArrayList();
            //foreach (FS.HISFC.Models.Order.OutPatient.Order outOrder in hsItemTemp.Values)
            //{
            //    alSubOrders.Add(outOrder);
            //}

            #endregion

            return 1;
        }

        /// <summary>
        /// 维护的LIS试管常数
        /// </summary>
        private static Hashtable hsListItem = new Hashtable();

        /// <summary>
        /// 返回Lis血管对应的项目
        /// </summary>
        /// <param name="TubeColor"></param>
        /// <returns></returns>
        private int InitDictionaryItem(ref string errInfo)
        {
            if (dictionaryItem == null || dictionaryItem.Count == 0)
            {
                FS.HISFC.BizLogic.Manager.Constant constManager = new FS.HISFC.BizLogic.Manager.Constant();
                FS.HISFC.BizLogic.Fee.Item itemManager = new FS.HISFC.BizLogic.Fee.Item();
                //ID为Lis试管的颜色，Name为HIS对应的项目名称
                ArrayList alConst = constManager.GetList("OutLisCalculateItem");
                if (alConst == null || alConst.Count == 0)
                {
                    errInfo = "初始化Lis试管对应的HIS项目失败，请联系管理员进行维护[常数编码：OutLisCalculateItem]！";
                    //return -1;
                }

                foreach (FS.FrameWork.Models.NeuObject con in alConst)
                {
                    FS.HISFC.Models.Fee.Item.Undrug undrug = null;
                    string[] arrCon = con.Name.Split(',');
                    if (arrCon.Length > 1)
                    {
                        for (int i = 0; i <= arrCon.Length - 1; ++i)
                        {
                            if (!string.IsNullOrEmpty(arrCon[i]))
                            {
                                undrug = itemManager.GetUndrugByCode(arrCon[i]);
                                if (undrug == null || string.IsNullOrEmpty(undrug.ID))
                                {
                                    errInfo = "Lis试管[" + con.ID + "管]对应的HIS带出项目已过期，请联系管理员进行维护[常数编码：OutLisCalculateItem]！";
                                    //return -1;
                                }
                                dictionaryItem.Add(con.ID + "[" + i.ToString() + "]", undrug);//红(1),红(2)

                                hsItem[undrug.ID + "[" + con.ID + "(" + i.ToString() + ")]"] = con.ID + "[" + i.ToString() + "]";
                            }

                        }

                    }
                    else
                    {
                        undrug = itemManager.GetUndrugByCode(con.Name);
                        if (undrug == null || string.IsNullOrEmpty(undrug.ID))
                        {
                            errInfo = "Lis试管[" + con.ID + "管]对应的HIS带出项目已过期，请联系管理员进行维护[常数编码：OutLisCalculateItem]！";
                            //return -1;
                        }
                        dictionaryItem.Add(con.ID, undrug);

                        hsItem[undrug.ID + "[" + con.ID + "]"] = con.ID;
                    }
                }
            }

            foreach (KeyValuePair<string, string> entry in hsItem)
            {
                string color = "";
                if (entry.Value.IndexOf('[') > 0)
                {
                    color = entry.Value.Substring(0, entry.Value.IndexOf('['));
                }
                else
                {
                    color = entry.Value;
                }

                //   string color=entry.Value.Substring(0,entry.Value.Length -entry.Value.IndexOf('[')
                //初始化试管数量
                MapCuvetteCounts[color] = 0;
            }
            return 1;

        }

        /// <summary>
        /// 门诊收取附材
        /// </summary>
        /// <param name="r"></param>
        /// <param name="alOrders"></param>
        /// <param name="alSubOrders"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public int DealSubjobOutPatient(FS.HISFC.Models.Registration.Register r, System.Collections.ArrayList alOrders, ref System.Collections.ArrayList alSubOrders, ref string errInfo)
        {
            if (r == null || r.ID.Length <= 0)
            {
                errInfo = "计算附材时，没有患者信息";
                return -1;
            }

            if (alOrders == null || alOrders.Count <= 0)
            {
                errInfo = "计算附材时，没有医嘱信息";
                return -1;
            }

            if (!isPopForChose)
            {
                //删除附材费用
                int param = Function.DeleteSubjobFeeForOutPatient(alOrders, ref errInfo);
                if (param == -1)
                {
                    return -1;
                }
            }

            if (childrenAge == -1)
            {
                //获取设置的儿童年龄上限
                childrenAge = ctrlMgr.GetControlParam<int>("HN0001", true, 14);
            }

            if (Function.GetSubListByDept(FS.HISFC.Models.Base.ServiceTypes.C, r.PVisit.PatientLocation.Dept.ID, ref errInfo, ref hsSubList) == -1)
            {
                return -1;
            }

            alOrders.Sort(orrderCompare);

            if (this.DealSubjobByUsageForOutPatient(alOrders, r, ref alSubOrders, ref errInfo) == -1)
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 创建医嘱
        /// </summary>
        /// <param name="itemNO">项目编码</param>
        /// <param name="qty">总数量</param>
        /// <param name="errText">错误信息</param>
        /// <returns>null 发生错误</returns>
        private FS.HISFC.Models.Order.OutPatient.Order CreateOrder(FS.HISFC.Models.Order.OutPatient.Order order, string itemNO, decimal qty, ref string errText)
        {
            if (order == null)
            {
                errText = "附材计算时，用于clone的医嘱为null";
                return null;
            }
            if (string.IsNullOrEmpty(itemNO))
            {
                errText = "附材计算时，用于获取项目的编码为空";
                return null;
            }
            FS.HISFC.Models.Fee.Item.Undrug item = null;

            if (!hsPhaItem.Contains(itemNO))
            {
                item = inteFeeMgr.GetItem(itemNO);//获得最新项目信息
                if (item == null)
                {
                    errText = "获取项目失败，尝试获取项目的项目编码为" + itemNO;
                    return null;
                }
                hsPhaItem.Add(itemNO, item);
            }
            else
            {
                item = hsPhaItem[itemNO] as FS.HISFC.Models.Fee.Item.Undrug;
            }

            if (item.UnitFlag == "1")
            {
                item.Price = inteFeeMgr.GetUndrugCombPrice(itemNO);
            }

            item.Qty = qty;

            FS.HISFC.Models.Order.OutPatient.Order newOrder = order.Clone();

            try
            {
                newOrder.ReciptNO = "";
                newOrder.SequenceNO = -1;

                newOrder.Item = item.Clone();
                newOrder.Qty = qty;
                newOrder.SubCombNO = order.SubCombNO;

                newOrder.Unit = item.PriceUnit;
                if (order.Combo == null || string.IsNullOrEmpty(order.Combo.ID))
                {
                    errText = "附材计算时，用于clone的医嘱组合号为null";
                    return null;
                }
                newOrder.Combo = order.Combo.Clone();//组合号
                newOrder.ReciptSequence = order.ReciptSequence;

                if (dealSublMode == -1)
                {
                    dealSublMode = ctrlMgr.GetControlParam<int>("HNMZ26", true, 0);
                }
                if (dealSublMode == 0)
                {
                    newOrder.ID = this.orderIntegrate.GetNewOrderID();//医嘱流水号
                    if (newOrder.ID == "")
                    {
                        errText = "计算项目附材时，对新增加的附材获得医嘱流水号出错！";
                        return null;
                    }
                }
                else
                {
                    newOrder.ID = "";
                }

                newOrder.Item.ItemType = FS.HISFC.Models.Base.EnumItemType.UnDrug;

                newOrder.DoseUnit = "";

                newOrder.IsEmergency = order.IsEmergency;
                newOrder.IsSubtbl = true;
                newOrder.Usage = new FS.FrameWork.Models.NeuObject();
                newOrder.SequenceNO = -1;
                if (newOrder.ExeDept.ID == "")//执行科室默认
                {
                    newOrder.ExeDept = (this.outOrderMgr.Operator as FS.HISFC.Models.Base.Employee).Dept.Clone();
                }

                newOrder.HerbalQty = 1;
                newOrder.Frequency = new Frequency();
                //newOrder.HerbalQty = order.HerbalQty;
                //newOrder.Frequency = order.Frequency;
                newOrder.InjectCount = 0;
                newOrder.IsSubtbl = true;
            }
            catch (Exception ex)
            {
                errText = "计算项目附材时，创建附材医嘱发生错误：" + ex.Message;
                return null;
            }
            return newOrder;
        }

        #endregion

        #region 住院附材处理

        /// <summary>
        /// 住院收取附材费用
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="isRealTime"></param>
        /// <param name="order"></param>
        /// <param name="alOrders"></param>
        /// <param name="alSubOrders"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public int DealSubjob(FS.HISFC.Models.RADT.PatientInfo patientInfo, bool isRealTime, FS.HISFC.Models.Order.Inpatient.Order order, ArrayList alOrders, ref ArrayList alSubOrders, ref string errInfo)
        {
            if (patientInfo == null || patientInfo.ID.Length <= 0)
            {
                errInfo = "计算附材时，没有患者信息";
                return -1;
            }

            if (alOrders == null || alOrders.Count <= 0)
            {
                errInfo = "计算附材时，没有医嘱信息";
                return -1;
            }

            if (childrenAge == -1)
            {
                //获取设置的儿童年龄上限
                childrenAge = ctrlMgr.GetControlParam<int>("HN0001", true, 14);
            }

            //这里获取附材的时候 都是根据患者所在科室获取的，所以手术室开立情况 后续要特殊处理了...
            if (Function.GetSubListByDept(FS.HISFC.Models.Base.ServiceTypes.I, patientInfo.PVisit.PatientLocation.Dept.ID, ref errInfo, ref hsSubList) == -1)
            {
                return -1;
            }

            alOrders.Sort(orrderCompare);

            if (this.DealSubjobByUsageForInPatient(isRealTime, "", order, alOrders, patientInfo, ref alSubOrders, ref errInfo) == -1)
            {
                return -1;
            }

            return 1;
        }
        FS.HISFC.BizLogic.Order.Order orderMgr = new FS.HISFC.BizLogic.Order.Order();

        /// <summary>
        /// 根据用法收取附材
        /// </summary>
        /// <param name="isRealTime">是否实时收取，否则为晚上后台收取</param>
        /// <param name="usage"></param>
        /// <param name="order"></param>
        /// <param name="alOrders"></param>
        /// <param name="patientInfo"></param>
        /// <param name="alSubOrders"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        private int DealSubjobByUsageForInPatient(bool isRealTime, string usage, FS.HISFC.Models.Order.Inpatient.Order inputOrder, ArrayList alOrders, FS.HISFC.Models.RADT.PatientInfo patientInfo, ref ArrayList alSubOrders, ref string errInfo)
        {
            ArrayList alSubInfo = new ArrayList();

            alSubOrders = new ArrayList();

            #region 实时收取，附加到医嘱上

            if (isRealTime)
            {
                FS.HISFC.Models.Order.Inpatient.Order order = inputOrder.Clone();
                 //单独的大输液不带出任何项目
                if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    foreach (FS.HISFC.Models.Order.Inpatient.Order inOrder in alOrders)
                    {
                        if (CheckIsMainDrug(inOrder) && inOrder.Combo.ID == inputOrder.Combo.ID)
                        {
                            order = inOrder.Clone();
                            break;
                        }
                    }
                    //单独的大输液不带出任何项目
                    if (!CheckIsMainDrug(order))
                    {
                        return 1;
                    }
                }
                if (order.Usage != null && !string.IsNullOrEmpty(order.Usage.ID))
                {
                    #region 根据用法带出

                    if (!hsSubList.Contains(order.Usage.ID))
                    {
                        return 1;
                    }

                    alSubInfo = hsSubList[order.Usage.ID] as ArrayList;
                    if (alSubInfo == null)
                    {
                        errInfo = subMgr.Err;
                        return -1;
                    }
                    else if (alSubInfo.Count == 0)
                    {
                        return 1;
                    }

                    foreach (OrderSubtblNew subObj in alSubInfo)
                    {
                        if (subObj.Area == "0")
                        {
                            continue;
                        }

                        if (order.ReciptDept.ID == subObj.Dept_code || subObj.Dept_code == "ROOT")
                        {
                            undrugItemObj = new FS.HISFC.Models.Base.Item();
                            undrugItemObj.ID = subObj.Item.ID;

                            //0 每组收取
                            if (subObj.CombArea == "0")
                            {
                                #region 每组收取

                                switch (subObj.FeeRule)
                                {
                                    //0 固定数量
                                    case "0":
                                        undrugItemObj.Qty = subObj.Qty;
                                        break;
                                    //1 最大院注
                                    case "1":
                                        undrugItemObj.Qty = order.InjectCount * subObj.Qty;
                                        break;
                                    //2 组内品种数
                                    case "2":
                                        undrugItemObj.Qty = GetFeeCountByCombOrders(alOrders, order.Combo.ID) * subObj.Qty;
                                        break;
                                    //3 医嘱执行次数
                                    case "3":
                                        if (order.OrderType.IsDecompose)
                                        {
                                            undrugItemObj.Qty = subObj.Qty;
                                        }
                                        else
                                        {
                                            undrugItemObj.Qty = subObj.Qty * order.HerbalQty * Function.GetFrequencyCountByOneDay(order.Frequency.ID);
                                        }
                                        break;
                                    //4 频次数
                                    case "4":
                                        //临嘱按照频次数量收取，长嘱按照固定数量收取
                                        if (order.OrderType.IsDecompose)
                                        {
                                            undrugItemObj.Qty = subObj.Qty;
                                        }
                                        else
                                        {
                                            undrugItemObj.Qty =Function.GetFrequencyCountByOneDay(order.Frequency.ID) * subObj.Qty;
                                        }
                                        break;
                                    //5 组内数量合计 目前只用于接瓶费了
                                    case "5":
                                        undrugItemObj.Qty = GetTotalCountByCombOrders(alOrders, order.Combo.ID, "1") * subObj.Qty;
                                        break;
                                }

                                if (this.AddToArray(order, subObj, alSubOrders, null, null, undrugItemObj, patientInfo.Birthday, ref errInfo) == -1)
                                {
                                    return -1;
                                }
                                #endregion
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    #region 项目带出

                    //if (order.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                    //{
                        alSubInfo = hsSubList[order.Item.ID] as ArrayList;
                        if (alSubInfo == null)
                        {
                            return 1;
                        }
                        else if (alSubInfo.Count == 0)
                        {
                            return 1;
                        }

                        foreach (OrderSubtblNew subObj in alSubInfo)
                        {
                            if (subObj.Area == "0")
                            {
                                continue;
                            }

                            if (order.ReciptDept.ID == subObj.Dept_code || subObj.Dept_code == "ROOT")
                            {
                                undrugItemObj = new FS.HISFC.Models.Base.Item();
                                undrugItemObj.ID = subObj.Item.ID;

                                //0 每组收取
                                if (subObj.CombArea == "0")
                                {
                                    #region 每组收取

                                    switch (subObj.FeeRule)
                                    {
                                        //0 固定数量
                                        case "0":
                                            undrugItemObj.Qty = subObj.Qty;
                                            break;
                                        //1 最大院注
                                        case "1":
                                            undrugItemObj.Qty = order.InjectCount * subObj.Qty;
                                            break;
                                        //2 组内品种数
                                        case "2":
                                            undrugItemObj.Qty = GetFeeCountByCombOrders(alOrders, order.Combo.ID) * subObj.Qty;
                                            break;
                                        //3 医嘱执行次数
                                        case "3":
                                            if (order.OrderType.IsDecompose)
                                            {
                                                undrugItemObj.Qty = subObj.Qty;
                                            }
                                            else
                                            {
                                                undrugItemObj.Qty = subObj.Qty * order.HerbalQty * Function.GetFrequencyCountByOneDay(order.Frequency.ID);
                                            }
                                            break;
                                        //4 频次数
                                        case "4":
                                            //临嘱按照频次数量收取，长嘱按照固定数量收取
                                            if (order.OrderType.IsDecompose)
                                            {
                                                undrugItemObj.Qty = subObj.Qty;
                                            }
                                            else
                                            {
                                                undrugItemObj.Qty =Function.GetFrequencyCountByOneDay(order.Frequency.ID) * subObj.Qty;
                                            }
                                            break;
                                        //5 组内数量合计 目前只用于接瓶费了
                                        case "5":
                                            undrugItemObj.Qty = GetTotalCountByCombOrders(alOrders, order.Combo.ID, "1") * subObj.Qty;
                                            break;
                                    }



                                    //if (this.AddToArray(order, subObj, alSubOrders, null, undrugItemObj, patientInfo.Birthday, ref errInfo) == -1) 
                                    if (this.AddToArray(order, subObj, alSubOrders, null, null, undrugItemObj, patientInfo.Birthday, ref errInfo) == -1)
                                    {
                                        return -1;
                                    }

                                    #endregion
                                }
                            }
                        //}
                    }

                    #endregion
                }


                //弹出选择
                if (this.isPopForChose)
                {
                    frmChoseSublItem choseSublItem = new frmChoseSublItem();
                    choseSublItem.ServerType = FS.HISFC.Models.Base.ServiceTypes.I;
                    choseSublItem.AlSublOrders = alSubOrders;
                    if (alSubOrders.Count > 0)
                    {
                        choseSublItem.ShowDialog();
                        if (choseSublItem.DialogResult == System.Windows.Forms.DialogResult.OK)
                        {
                            alSubOrders = choseSublItem.AlSublOrders;
                        }
                        else
                        {
                            alSubOrders.Clear();
                        }
                    }
                }
            }
            #endregion

            #region 后台收取
            else
            {
                hsFeeOrderUsages = new Hashtable();
                hsFirstFeeOrderUsages = new Hashtable();
                hsFirstFeeOrderCombNos = new Hashtable();
                hsSecondFeeOrder = new Hashtable();

                Hashtable hsCombOrder = new Hashtable();

                //按照组合分组
                foreach (FS.HISFC.Models.Order.Inpatient.Order inOrder in alOrders)
                {
                    if (inOrder.IsSubtbl)
                    {
                        continue;
                    }
                    //单独的大输液不带出任何项目
                    if (inOrder.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        //单独的大输液不带出任何项目
                        if (!CheckIsMainDrug(inOrder))
                        {
                            continue;
                        }
                    }

                    //作废的临嘱不处理
                    if (!inOrder.OrderType.IsDecompose
                        && (3 <= inOrder.Status && inOrder.Status <= 4))
                    {
                        continue;
                    }
                    //停止时间早于计费日期的不处理
                    if (inOrder.OrderType.IsDecompose
                        && (3 <= inOrder.Status && inOrder.Status <= 4)
                        && inOrder.DCOper.OperTime.Date < this.feeDate.Date)
                    {
                        continue;
                    }
                    if (!hsCombOrder.Contains(inOrder.Combo.ID))
                    {
                        ArrayList al = new ArrayList();
                        al.Add(inOrder.Clone());
                        hsCombOrder.Add(inOrder.Combo.ID, al);
                    }
                    else
                    {
                        ((ArrayList)hsCombOrder[inOrder.Combo.ID]).Add(inOrder.Clone());
                    }
                }

                ArrayList alAllCombOrder = new ArrayList(hsCombOrder.Values);

                //此处排序比较【重要】，输液费是按照第一个收取的
                alAllCombOrder.Sort(new InOrderCompare());


                //存储改患者当前时间段所有医嘱执行情况
                ArrayList alExecOrders = this.orderMgr.QueryExecOrder(patientInfo.ID, "1",
                    FeeDate.Date,
                    FeeDate.AddDays(1).Date);
                if (alExecOrders == null)
                {
                    errInfo = orderMgr.Err;
                    return -1;
                }

                //去掉无效的执行档
                ArrayList alTemp = new ArrayList();

                foreach (FS.HISFC.Models.Order.ExecOrder execOrder in alExecOrders)
                {
                    if (!execOrder.IsValid//作废的
                        || !execOrder.IsExec//未执行的
                        || (execOrder.Order.Status == 3 && (execOrder.DateUse >= execOrder.Order.DCOper.OperTime || execOrder.DateUse >= execOrder.DCExecOper.OperTime)))//停止时间之后的
                    {
                        continue;
                    }
                    alTemp.Add(execOrder);
                }
                alExecOrders = alTemp;

                FS.HISFC.Models.Order.Inpatient.Order orderObj = null;
                foreach (ArrayList alCombOrder in alAllCombOrder)
                {
                    if (alCombOrder.Count == 0)
                    {
                        continue;
                    }


                    //这里用克隆吧，后续可能修改里面的内容
                    orderObj = (alCombOrder[0] as FS.HISFC.Models.Order.Inpatient.Order).Clone();
                    //foreach (FS.HISFC.Models.Order.Inpatient.Order orderObj in alOrders)
                    //{
                    #region 用法带出
                    if (orderObj.Usage != null && !string.IsNullOrEmpty(orderObj.Usage.ID))
                    {
                        if (!hsSubList.Contains(orderObj.Usage.ID))
                        {
                            continue;
                        }

                        alSubInfo = hsSubList[orderObj.Usage.ID] as ArrayList;
                        if (alSubInfo == null)
                        {
                            errInfo = subMgr.Err;
                            return -1;
                        }
                        else if (alSubInfo.Count == 0)
                        {
                            continue;
                        }

                        sysUsage = Function.GetSysType(orderObj.Usage.ID, ref errInfo);
                        if (sysUsage == null)
                        {
                            return -1;
                        }

                        foreach (OrderSubtblNew subObj in alSubInfo)
                        {
                            if (subObj.Area == "0")
                            {
                                continue;
                            }

                            if (orderObj.ReciptDept.ID == subObj.Dept_code || subObj.Dept_code == "ROOT")
                            {
                                undrugItemObj = new FS.HISFC.Models.Base.Item();
                                undrugItemObj.ID = subObj.Item.ID;

                                //1 第一组收取
                                if (subObj.CombArea == "1")
                                {
                                    #region 第一组收取

                                    if (!hsFirstFeeOrderUsages.Contains(sysUsage)
                                        || (hsFirstFeeOrderCombNos.Count > 0
                                        && hsFirstFeeOrderCombNos.Contains(orderObj.Combo.ID))
                                    )
                                    {
                                        switch (subObj.FeeRule)
                                        {
                                            //0 固定数量
                                            case "0":
                                                undrugItemObj.Qty = subObj.Qty;
                                                break;
                                            //1 最大院注
                                            case "1":
                                                undrugItemObj.Qty = orderObj.InjectCount * subObj.Qty;
                                                break;
                                            //2 组内品种数
                                            case "2":
                                                undrugItemObj.Qty = GetFeeCountByCombOrders(alOrders, orderObj.Combo.ID) * subObj.Qty;
                                                break;
                                            //3 医嘱执行次数
                                            case "3":
                                                if (orderObj.OrderType.IsDecompose)
                                                {
                                                    undrugItemObj.Qty = subObj.Qty;
                                                }
                                                else
                                                {
                                                    undrugItemObj.Qty = subObj.Qty * orderObj.HerbalQty * Function.GetRealExecCount(alExecOrders, orderObj);
                                                }
                                                break;
                                            //4 频次数
                                            case "4":
                                                undrugItemObj.Qty = Function.GetRealExecCount(alExecOrders, orderObj) * subObj.Qty;
                                                break;
                                            //5 组内数量合计 目前只用于接瓶费了
                                            case "5":
                                                undrugItemObj.Qty = GetTotalCountByCombOrders(alOrders, orderObj.Combo.ID, "1") * subObj.Qty;
                                                break;
                                        }


                                        if (this.AddToArray(orderObj, subObj, alSubOrders, null, alExecOrders, undrugItemObj, patientInfo.Birthday, ref errInfo) == -1)
                                        {
                                            return -1;
                                        }


                                        if (!hsFirstFeeOrderCombNos.Contains(orderObj.Combo.ID))
                                        {
                                            hsFirstFeeOrderCombNos.Add(orderObj.Combo.ID, orderObj.Clone());
                                        }

                                        if (!hsFirstFeeOrderUsages.Contains(sysUsage))
                                        {
                                            hsFirstFeeOrderUsages.Add(sysUsage, null);
                                        }

                                    #endregion
                                    }
                                }

                                //2 第二组起加收
                                else if (subObj.CombArea == "2")
                                {
                                    #region 第二组加收

                                    if (hsFeeOrderUsages.Contains(sysUsage)
                                    && (hsFirstFeeOrderCombNos.Count > 0 && !hsFirstFeeOrderCombNos.Contains(orderObj.Combo.ID))
                                        )
                                    {
                                        switch (subObj.FeeRule)
                                        {
                                            //0 固定数量
                                            case "0":
                                                undrugItemObj.Qty = subObj.Qty;
                                                break;
                                            //1 最大院注
                                            case "1":
                                                undrugItemObj.Qty = orderObj.InjectCount * subObj.Qty;
                                                break;
                                            //2 组内品种数
                                            case "2":
                                                undrugItemObj.Qty = GetFeeCountByCombOrders(alOrders, orderObj.Combo.ID) * subObj.Qty;
                                                break;
                                            //3 医嘱执行次数
                                            case "3":
                                                if (orderObj.OrderType.IsDecompose)
                                                {
                                                    undrugItemObj.Qty = subObj.Qty;
                                                }
                                                else
                                                {
                                                    undrugItemObj.Qty = subObj.Qty * orderObj.HerbalQty * Function.GetRealExecCount(alExecOrders, orderObj);
                                                }
                                                break;
                                            //4 频次数
                                            case "4":
                                                undrugItemObj.Qty = Function.GetRealExecCount(alExecOrders, orderObj) * subObj.Qty;
                                                break;
                                            //5 组内数量合计 目前只用于接瓶费了
                                            case "5":
                                                undrugItemObj.Qty = GetTotalCountByCombOrders(alOrders, orderObj.Combo.ID, "1") * subObj.Qty;
                                                break;
                                        }

                                        if (this.AddToArray(orderObj, subObj, alSubOrders, hsSecondFeeOrder, alExecOrders, undrugItemObj, patientInfo.Birthday, ref errInfo) == -1)
                                        {
                                            return -1;
                                        }
                                    }
                                    #endregion
                                }


                                if (!this.hsFeeOrderUsages.Contains(sysUsage))
                                {
                                    hsFeeOrderUsages.Add(sysUsage, orderObj.Clone());
                                }
                            }
                        }
                    }

                    #endregion

                    #region 项目带出
                    else
                    {
                        //if (orderObj.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                        //{
                            alSubInfo = hsSubList[orderObj.Item.ID] as ArrayList;
                            if (alSubInfo == null)
                            {
                                return 1;
                            }
                            else if (alSubInfo.Count == 0)
                            {
                                return 1;
                            }

                            foreach (OrderSubtblNew subObj in alSubInfo)
                            {
                                if (subObj.Area == "0")
                                {
                                    continue;
                                }

                                if (orderObj.ReciptDept.ID == subObj.Dept_code || subObj.Dept_code == "ROOT")
                                {
                                    undrugItemObj = new FS.HISFC.Models.Base.Item();
                                    undrugItemObj.ID = subObj.Item.ID;


                                    //1 第一组收取
                                    if (subObj.CombArea == "1")
                                    {
                                        #region 第一组收取

                                        if (!this.hsFirstFeeOrderUsages.Contains(orderObj.Item.ID)
|| (hsFirstFeeOrderCombNos.Count > 0 && hsFirstFeeOrderCombNos.Contains(orderObj.Combo.ID))
                                            )
                                        {
                                            switch (subObj.FeeRule)
                                            {
                                                //0 固定数量
                                                case "0":
                                                    undrugItemObj.Qty = subObj.Qty;
                                                    break;
                                                //1 最大院注
                                                case "1":
                                                    undrugItemObj.Qty = orderObj.InjectCount * subObj.Qty;
                                                    break;
                                                //2 组内品种数
                                                case "2":
                                                    undrugItemObj.Qty = GetFeeCountByCombOrders(alOrders, orderObj.Combo.ID) * subObj.Qty;
                                                    break;
                                                //3 医嘱执行次数
                                                case "3":
                                                    if (orderObj.OrderType.IsDecompose)
                                                    {
                                                        undrugItemObj.Qty = subObj.Qty;
                                                    }
                                                    else
                                                    {
                                                        undrugItemObj.Qty = subObj.Qty * orderObj.HerbalQty * Function.GetRealExecCount(alExecOrders, orderObj);
                                                    }
                                                    break;
                                                //4 频次数
                                                case "4":
                                                    undrugItemObj.Qty = Function.GetRealExecCount(alExecOrders, orderObj) * subObj.Qty;
                                                    break;
                                                //5 组内数量合计 目前只用于接瓶费了
                                                case "5":
                                                    undrugItemObj.Qty = GetTotalCountByCombOrders(alOrders, orderObj.Combo.ID, "1") * subObj.Qty;
                                                    break;
                                            }

                                            if (this.AddToArray(orderObj, subObj, alSubOrders, null, null, undrugItemObj, patientInfo.Birthday, ref errInfo) == -1)
                                            {
                                                return -1;
                                            }



                                            if (!hsFirstFeeOrderCombNos.Contains(orderObj.Combo.ID))
                                            {
                                                hsFirstFeeOrderCombNos.Add(orderObj.Combo.ID, orderObj.Clone());
                                            }

                                            if (!hsFirstFeeOrderUsages.Contains(sysUsage))
                                            {
                                                hsFirstFeeOrderUsages.Add(sysUsage, null);
                                            }
                                        }
                                        #endregion
                                    }

                                    //2 第二组起加收
                                    else if (subObj.CombArea == "2")
                                    {
                                        #region 第二组加收

                                        if (hsFeeOrderUsages.Contains(sysUsage)
                                    && (hsFirstFeeOrderCombNos.Count > 0 && !hsFirstFeeOrderCombNos.Contains(orderObj.Combo.ID))
                                            )
                                        {
                                            switch (subObj.FeeRule)
                                            {
                                                //0 固定数量
                                                case "0":
                                                    undrugItemObj.Qty = subObj.Qty;
                                                    break;
                                                //1 最大院注
                                                case "1":
                                                    undrugItemObj.Qty = orderObj.InjectCount * subObj.Qty;
                                                    break;
                                                //2 组内品种数
                                                case "2":
                                                    undrugItemObj.Qty = GetFeeCountByCombOrders(alOrders, orderObj.Combo.ID) * subObj.Qty;
                                                    break;
                                                //3 医嘱执行次数
                                                case "3":

                                                    if (orderObj.OrderType.IsDecompose)
                                                    {
                                                        undrugItemObj.Qty = subObj.Qty;
                                                    }
                                                    else
                                                    {
                                                        undrugItemObj.Qty = subObj.Qty * orderObj.HerbalQty * Function.GetRealExecCount(alExecOrders, orderObj);
                                                    }
                                                    break;
                                                //4 频次数
                                                case "4":
                                                    undrugItemObj.Qty = Function.GetRealExecCount(alExecOrders, orderObj) * subObj.Qty;
                                                    break;
                                                //5 组内数量合计 目前只用于接瓶费了
                                                case "5":
                                                    undrugItemObj.Qty = GetTotalCountByCombOrders(alOrders, orderObj.Combo.ID, "1") * subObj.Qty;
                                                    break;
                                            }

                                            if (this.AddToArray(orderObj, subObj, alSubOrders, hsSecondFeeOrder, null, undrugItemObj, patientInfo.Birthday, ref errInfo) == -1)
                                            {
                                                return -1;
                                            }
                                        }
                                        #endregion
                                    }

                                    if (!this.hsFeeOrderUsages.Contains(sysUsage))
                                    {
                                        this.hsFeeOrderUsages.Add(sysUsage, null);
                                    }
                                }
                            //}
                        }
                    }
                    #endregion
                }

                //对于第二组加收的，在此处增加到附材列表
                ArrayList alTemp2 = new ArrayList(hsSecondFeeOrder.Values);
                foreach (FS.HISFC.Models.Fee.Item.Undrug undrugObj in alTemp2)
                {
                    alSubOrders.Add(undrugObj);
                }
            }
            #endregion

            return 1;
        }

        #endregion

        #region IDealSubjob 成员

        public int DealSubjob(FS.HISFC.Models.Registration.Register r, ArrayList alFee, ref string errText)
        {
            return 1;
        }

        #endregion
    }

    #region 排序

    /// <summary>
    /// 门诊组合列表根据频次*天数排序
    /// </summary>
    public class OutCombOrderCompareByFreqAndDays : IComparer
    {
        #region IComparer 成员

        public int Compare(object x, object y)
        {
            try
            {
                ArrayList al1 = x as ArrayList;
                ArrayList al2 = y as ArrayList;

                FS.HISFC.Models.Order.OutPatient.Order outOrder1 = al1[0] as FS.HISFC.Models.Order.OutPatient.Order;
                FS.HISFC.Models.Order.OutPatient.Order outOrder2 = al2[0] as FS.HISFC.Models.Order.OutPatient.Order;

                decimal count1 = outOrder1.HerbalQty * Function.GetFrequencyCountByOneDay(outOrder1.Frequency.ID);
                decimal count2 = outOrder2.HerbalQty * Function.GetFrequencyCountByOneDay(outOrder2.Frequency.ID);

                if (count1 > count2)
                {
                    return -1;
                }
                else if (count2 == count1)
                {
                    if (outOrder1.SubCombNO > outOrder2.SubCombNO)
                    {
                        return 1;
                    }
                    else if (outOrder2.SubCombNO == outOrder1.SubCombNO)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    return 1;
                }
            }
            catch
            {
                return 0;
            }
        }

        #endregion
    }

    /// <summary>
    /// 住院医嘱按照频次、时间排序
    /// </summary>
    public class InOrderCompare : IComparer
    {
        #region IComparer 成员

        public int Compare(object x, object y)
        {
            try
            {
                ArrayList al1 = x as ArrayList;
                ArrayList al2 = y as ArrayList;

                FS.HISFC.Models.Order.Inpatient.Order order1 = al1[0] as FS.HISFC.Models.Order.Inpatient.Order;
                FS.HISFC.Models.Order.Inpatient.Order order2 = al2[0] as FS.HISFC.Models.Order.Inpatient.Order;

                decimal count1 = Function.GetFrequencyCountByOneDay(order1.Frequency.ID);
                decimal count2 = Function.GetFrequencyCountByOneDay(order2.Frequency.ID);
                if (count1 > count2)
                {
                    return 1;
                }
                else if (count1 == count2)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }

                #region 旧的按照医嘱类别、时间排序的作废

                //int type1 = order1.OrderType.IsDecompose ? 0 : 1;
                //int type2 = order2.OrderType.IsDecompose ? 0 : 1;

                //if (type1 > type2)
                //{
                //    return 1;
                //}
                //else if (type1 == type2)
                //{
                //    if (order1.MOTime > order2.MOTime)
                //    {
                //        return 1;
                //    }
                //    else if (order1.MOTime == order2.MOTime)
                //    {
                //        return 0;
                //    }
                //    else
                //    {
                //        return -1;
                //    }
                //}
                //else
                //{
                //    return -1;
                //}
                #endregion
            }
            catch
            {
                return 0;
            }
        }

        #endregion
    }

    /// <summary>
    /// 医嘱按照序号sortid排序
    /// </summary>
    public class OrderCompareBySortID : IComparer
    {
        #region IComparer 成员

        public int Compare(object x, object y)
        {
            try
            {
                FS.HISFC.Models.Order.Order order1 = x as FS.HISFC.Models.Order.Order;
                FS.HISFC.Models.Order.Order order2 = y as FS.HISFC.Models.Order.Order;
                if (order1.SortID > order2.SortID)
                {
                    return 1;
                }
                else if (order1.SortID == order2.SortID)
                {
                    return string.Compare(order1.ID, order2.ID);
                }
                else
                {
                    return -1;
                }
            }
            catch
            {
                return 0;
            }
        }

        #endregion
    }

    /// <summary>
    /// 医嘱按照序号sortid排序
    /// </summary>
    public class OutOrderCompareBySortID : IComparer
    {
        #region IComparer 成员

        public int Compare(object x, object y)
        {
            try
            {
                FS.HISFC.Models.Order.Order order1 = x as FS.HISFC.Models.Order.Order;
                FS.HISFC.Models.Order.Order order2 = y as FS.HISFC.Models.Order.Order;
                if (order1.SortID > order2.SortID)
                {
                    return 1;
                }
                else if (order1.SortID == order2.SortID)
                {
                    return string.Compare(order1.ID, order2.ID);
                }
                else
                {
                    return -1;
                }
            }
            catch
            {
                return 0;
            }
        }

        #endregion
    }
    #endregion
}