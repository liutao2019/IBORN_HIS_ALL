using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HISFC.Models.Base;
using FS.HISFC.BizProcess.Integrate;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.OrderIntegrate
{
    /// <summary>
    /// 医嘱、处方通用交叉业务
    /// </summary>
    public class OrderBase : FS.HISFC.BizProcess.Integrate.IntegrateBase
    {
        private static FS.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        private static FS.HISFC.BizProcess.Integrate.Common.ControlParam controlMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        private static FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new Fee();

        private static FS.HISFC.BizLogic.Pharmacy.Item myPha = new FS.HISFC.BizLogic.Pharmacy.Item();

        private static FS.HISFC.BizLogic.Pharmacy.Constant myPhaCons = new FS.HISFC.BizLogic.Pharmacy.Constant();

        /// <summary>
        /// 住院是否使用预扣库存 P00200
        /// </summary>
        private static int isUseInDrugPreOut = -1;

        /// <summary>
        /// 门诊是否使用预扣库存 P00320
        /// </summary>
        private static int isUseOutDrugPreOut = -1;

        #region 判断开立有效性

        /// <summary>
        /// 检查缺药、停用
        /// </summary>
        /// <param name="drugDept">扣库科室</param>
        /// <param name="item">项目</param>
        /// <param name="IsOutPatient">是否门诊开立</param>
        /// <param name="drugItem">返回的项目信息</param>
        /// <param name="errInfo">错误信息</param>
        /// <returns>0 表示不允许开立，允许组套； -1 表示错误</returns>
        public static int CheckDrugState(FS.FrameWork.Models.NeuObject patientInfo,FS.FrameWork.Models.NeuObject drugDept, FS.FrameWork.Models.NeuObject reciptDept, FS.HISFC.Models.Base.Item itemObj, bool IsOutPatient, ref FS.HISFC.Models.Pharmacy.Item drugItem, ref  FS.HISFC.Models.Pharmacy.Storage storage, ref string errInfo)
        {
            if (itemObj == null)
            {
                errInfo = "项目为空！";
                return -1;
            }
            if (itemObj.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                return 1;
            }

            if (itemObj.ID == "999")
            {
                return 1;
            }

            if (storage == null)
            {
                storage = new FS.HISFC.Models.Pharmacy.Storage();
            }

            #region 获取库存信息

            //库存信息里面的药品信息可能不全，所以提前获取必要信息，如价格等
            drugItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(itemObj.ID);

            if (drugDept != null && !string.IsNullOrEmpty(drugDept.ID))
            {
                storage = phaIntegrate.GetStockInfoByDrugCode(drugDept.ID, itemObj.ID);
            }
            else
            {
                if (IsOutPatient)
                {
                    storage = phaIntegrate.GetItemStorage(reciptDept.ID, "O", itemObj.ID);
                }
                else
                {
                    storage = phaIntegrate.GetItemStorage(reciptDept.ID, "I", itemObj.ID);
                }
            }

            if (storage == null)
            {
                errInfo = "查询库存相关信息错误!\r\n" + phaIntegrate.Err;
                return -1;
            }

            //drugItem = storage.Item;
            //drugItem = phaIntegrate.GetItem(itemObj.ID);
            //if (drugItem == null)
            //{
            //    errInfo = "查询药品【" + itemObj.Name + "】失败:" + phaIntegrate.Err;
            //    return -1;
            //}
            drugItem.Price = storage.Item.Price;

            if (storage.Item.ID == "")
            {
                //在该药房不存在  houwb 2011-5-30 改为取药药房判断
                //errInfo = "【" + drugItem.Name + "】在" + FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(storage.StockDept.ID) + "不存在!";
                if (IsOutPatient)
                {
                    errInfo = "【" + drugItem.Name + "】在本科室对应门诊系统的" + (drugDept == null || string.IsNullOrEmpty(drugDept.Name) ? "" : "【" + drugDept.Name + "】") + "库存不足!";
                }
                else
                {
                    errInfo = "【" + drugItem.Name + "】在本科室对应住院系统的" + (drugDept == null || string.IsNullOrEmpty(drugDept.Name) ? "" : "【" + drugDept.Name + "】") + "库存不足!";
                }
                return -1;
            }
            else if (storage.Item.ValidState != EnumValidState.Valid)
            {
                //在全院已停用
                errInfo = "【" + drugItem.Name + "】全院已停用！";
                return -1;
            }
            else if (storage.IsLack)
            {
                //在全院已缺药
                errInfo = "【" + drugItem.Name + "】全院已缺药！";
                return -1;
            }
            else if (storage.ValidState != FS.HISFC.Models.Base.EnumValidState.Valid)
            {
                //在药房已停用
                errInfo = "【" + drugItem.Name + "】在" + FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(storage.StockDept.ID) + "已停用!";
                return 0;
            }

            if (IsOutPatient)
            {
                if (!storage.IsUseForOutpatient)
                {
                    //不能用于门诊
                    errInfo = "【" + drugItem.Name + "】在" + FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(storage.StockDept.ID) + "设置为不能用于门诊用药!";
                    return 0;
                }
                //门诊缺药
                else if (storage.IsLack)
                {
                    //药房缺药
                    errInfo = "【" + drugItem.Name + "】" + FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(storage.StockDept.ID) + "已缺药!";
                    return 0;
                }

                if (isUseOutDrugPreOut == -1)
                {
                    isUseOutDrugPreOut = controlMgr.GetControlParam<int>(PharmacyConstant.OutDrug_Pre_Out, true);
                }

                if (isUseOutDrugPreOut == 1)
                {
                    if (storage.StoreQty - storage.PreOutQty <= 0)
                    {
                        errInfo = "【" + drugItem.Name + "】在" + FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(storage.StockDept.ID) + "库存为0，不允许开立!";
                        return 0;
                    }
                }
                else
                {
                    if (storage.StoreQty <= 0)
                    {
                        errInfo = "【" + drugItem.Name + "】在" + FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(storage.StockDept.ID) + "库存为0，不允许开立!";
                        return 0;
                    }
                }
            }
            else
            {
                if (!storage.IsUseForInpatient)
                {
                    //不能用于门诊
                    errInfo = "【" + drugItem.Name + "】在" + FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(storage.StockDept.ID) + "设置为不能用于住院用药!";
                    return 0;
                }
                //住院缺药
                else if (storage.IsLackForInpatient)
                {
                    //药房缺药
                    errInfo = "【" + drugItem.Name + "】" + FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(storage.StockDept.ID) + "已缺药!";
                    return 0;
                }

                if (isUseInDrugPreOut == -1)
                {
                    isUseInDrugPreOut = controlMgr.GetControlParam<int>(PharmacyConstant.InDrug_Pre_Out, true);
                }

                if (isUseInDrugPreOut == 1)
                {
                    if (storage.StoreQty - storage.PreOutQty <= 0)
                    {
                        errInfo = "【" + drugItem.Name + "】在" + FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(storage.StockDept.ID) + "库存为0，不允许开立!";
                        return 0;
                    }
                }
                else
                {
                    if (storage.StoreQty <= 0)
                    {
                        errInfo = "【" + drugItem.Name + "】在" + FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(storage.StockDept.ID) + "库存为0，不允许开立!";
                        return 0;
                    }
                }
            }

            decimal orgPrice = 0;
            if (patientInfo is FS.HISFC.Models.Registration.Register)
            {
                itemObj.Price = feeIntegrate.GetPrice(itemObj.ID, null, 0, drugItem.Price, drugItem.Price, drugItem.Price, 0, ref orgPrice);
                drugItem.Price = feeIntegrate.GetPrice(itemObj.ID, null, 0, drugItem.Price, drugItem.Price, drugItem.Price, 0, ref orgPrice);
            }
            else
            {
                decimal price = 0;
                feeIntegrate.GetPriceForInpatient(patientInfo as FS.HISFC.Models.RADT.PatientInfo, drugItem, ref price, ref orgPrice);
                itemObj.Price = price;
                drugItem.Price = price;
            }

            #endregion
            return 1;
        }

        /// <summary>
        /// 检查非药品项目有效性
        /// </summary>
        /// <param name="undrug"></param>
        /// <param name="errInfo"></param>
        /// <returns>0 表示不允许开立，允许组套； -1 表示错误</returns>
        public static int CheckUnDrugState(FS.HISFC.Models.Order.Order order, ref FS.HISFC.Models.Fee.Item.Undrug undrug, ref string errInfo)
        {
            undrug = SOC.HISFC.BizProcess.Cache.Fee.GetItem(order.Item.ID);
            if (undrug == null)
            {
                errInfo = "获取项目【" + order.Item.Name + "】信息失败！请联系信息科！\r\n" + feeIntegrate.Err;
                return -1;
            }

            string sql = @"select o.valid_state from fin_com_undruginfo o
where o.item_code='{0}'";
            sql = string.Format(sql, undrug.ID);

            string strValid = myPha.ExecSqlReturnOne(sql, "-1");

            if (strValid != "1")
            {
                return 0;
            }
            if (undrug.UnitFlag == "1" && string.IsNullOrEmpty(undrug.PriceUnit))
            {
                undrug.PriceUnit = "次";
            }

            order.Item.Price = undrug.Price;
            order.Item.ChildPrice = undrug.ChildPrice;
            order.Item.SpecialPrice = undrug.SpecialPrice;

            if (order.GetType() == typeof(FS.HISFC.Models.Order.OutPatient.Order))
            {
                decimal orgPrice = 0;

                FS.HISFC.Models.Registration.Register regObj = new FS.HISFC.Models.Registration.Register();
                regObj.Pact = order.Patient.Pact;

                undrug.Price = feeIntegrate.GetPrice(order.Item.ID, regObj, 0, undrug.Price, undrug.ChildPrice, undrug.SpecialPrice, 0, ref orgPrice);
                order.Item.Price = undrug.Price;
            }

            return 1;
        }

        #endregion

        #region 处方开立权限：处方权、特限药等
        
        /// <summary>
        /// 是否控制处方权
        /// </summary>
        private static int isUseControl = -1;

        private static FS.HISFC.BizLogic.Order.Medical.Ability docAbility = new FS.HISFC.BizLogic.Order.Medical.Ability();
        private static FS.HISFC.BizProcess.Integrate.Manager interMgr = new Manager();
        private static ArrayList alDrugGrade = null;

        private static ArrayList alDrugPosition = null;

        private static ArrayList alSpeDrugs = null;

        /// <summary>
        /// 医生站权限判断，含处方权、组套修改权、等级药物开立权、特限药物开立权、非药品项目开立权
        /// </summary>
        /// <param name="order"></param>
        /// <param name="doctor"></param>
        /// <param name="dept"></param>
        /// <param name="priv"></param>
        /// <param name="outpatient"></param>
        /// <param name="error"></param>
        /// <returns>1 有权限；0 无权限、可操作；-1 无权限、不可操作</returns>
        public static int JudgeEmplPriv(FS.HISFC.Models.Order.Order order, FS.FrameWork.Models.NeuObject doctor,
            FS.FrameWork.Models.NeuObject dept, FS.HISFC.Models.Base.DoctorPrivType priv,
            bool outpatient, ref string error)
        {
            int rev = 1;

            if (order.Item.ID == "999")
                return 1;

            //现在系统允许无处方权的医生开立非药品，貌似没有找到相关文件不允许开立非药品

            //1、组套权限所有都判断
            //2、处方权，门诊不允许开立，住院允许开立，状态为无效
            //3、等级药品，门诊不允许开立，住院允许开立，状态为无效
            //4、特限药，门诊、住院都不允许开立
            //5、特限非药品，门诊、住院都不允许开立

            #region  组套修改权
            if (priv == DoctorPrivType.GroupManager)
            {
                #region 组套修改判断
                string sysClass = "groupManager";

                rev = docAbility.CheckPopedom(doctor.ID, order.Item.ID, sysClass, false, ref error);

                return rev;
                #endregion
            }
            #endregion

            else
            {
                #region 处方权

                //是否启用开立权限控制
                if (isUseControl == -1)
                {
                    FS.HISFC.BizProcess.Integrate.Common.ControlParam controler = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                    isUseControl = controler.GetControlParam<Int32>("HNMET1", true, 0);
                }

                if (isUseControl == 1)
                {
                    if (order.Item.ItemType == EnumItemType.UnDrug)
                    {
                        rev = 1;
                        return rev;
                    }
                    #region 处方权
                    rev = docAbility.CheckPopedom(doctor.ID, order.Item.ID, order.Item.SysClass.ID.ToString(), false, ref error);

                    if (rev < 0)
                    {
                        return rev;
                    }
                    #endregion
                }
                #endregion

                #region 药品等级开立控制（抗生素级别）

                #region 药品等级限制

                //当前医生拥有的权限
                int drugPermission = 0;

                //是否控制权限
                bool isControlDrugPermission = false;

                FS.HISFC.Models.Base.Employee doctObj = null;

                //判断对照数据(职级<-->药品等级  职务<-->药品等级)
                doctObj = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeInfo(doctor.ID);

                if (doctObj == null)
                {
                    error = "获取医生信息错误！ 工号【" + doctor.ID + "】";
                    return -1;
                }

                #region 获取对照
                if (alDrugGrade == null)
                {
                    alDrugGrade = interMgr.GetConstantList("SpeDrugGrade");

                    if (alDrugGrade == null)
                    {
                        error = interMgr.Err;
                        return -1;
                    }
                }

                for (int i = 0; i < alDrugGrade.Count; i++)
                {
                    int intIndex = ((FS.FrameWork.Models.NeuObject)alDrugGrade[i]).ID.IndexOf('|');

                    if (intIndex <= 0)
                    {
                        continue;
                    }

                    string level = ((FS.FrameWork.Models.NeuObject)alDrugGrade[i]).ID.Substring(0, intIndex);

                    if (doctObj.Level.ID.Trim() == level.Trim() && (alDrugGrade[i] as FS.HISFC.Models.Base.Const).IsValid)
                    {
                        drugPermission = FS.FrameWork.Function.NConvert.ToInt32(((FS.FrameWork.Models.NeuObject)alDrugGrade[i]).ID.Substring(intIndex + 1));
                        break;
                    }
                }

                if (alDrugPosition == null)
                {
                    alDrugPosition = interMgr.GetConstantList("SpeDrugPosition");
                    if (alDrugPosition == null)
                    {
                        error = interMgr.Err;
                        return -1;
                    }
                }

                for (int i = 0; i < alDrugPosition.Count; i++)
                {
                    int intIndex = ((FS.FrameWork.Models.NeuObject)alDrugPosition[i]).ID.IndexOf('|');

                    if (intIndex <= 0)
                    {
                        continue;
                    }

                    string level = ((FS.FrameWork.Models.NeuObject)alDrugPosition[i]).ID.Substring(0, intIndex);

                    if (doctObj.Duty.ID.Trim() == level.Trim()
                        && (alDrugPosition[i] as FS.HISFC.Models.Base.Const).IsValid)
                    {
                        if (drugPermission < FS.FrameWork.Function.NConvert.ToInt32(((FS.FrameWork.Models.NeuObject)alDrugPosition[i]).ID.Substring(intIndex + 1)))
                        {
                            drugPermission = FS.FrameWork.Function.NConvert.ToInt32(((FS.FrameWork.Models.NeuObject)alDrugPosition[i]).ID.Substring(intIndex + 1));
                        }
                        break;
                    }
                }

                #endregion

                #region 进行核对

                if ((alDrugGrade != null && alDrugGrade.Count > 0) 
                    || alDrugPosition != null && alDrugPosition.Count > 0)
                {
                    isControlDrugPermission = true;
                }

                if (isControlDrugPermission)
                {
                    //抗生素：药品 
                    if (order.Item.ItemType == EnumItemType.Drug)
                    {
                        FS.HISFC.Models.Pharmacy.Item phaItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID);

                        if (phaItem == null)
                        {
                            error = "获取药品信息失败!";
                            return -1;
                        }

                        //三类抗生素不允许门诊开立
                        if (order.GetType() == typeof(FS.HISFC.Models.Order.OutPatient.Order)
                            && phaItem.Grade == "3")
                        {
                            error = "三类限制药品，不允许门诊开立！\r\n";
                            //rev = 0;
                            return 0;
                        }

                        order.Item.Grade = phaItem.Grade;

                        if (!string.IsNullOrEmpty(order.Item.Grade))
                        {
                            if (outpatient)
                            {
                                if (FS.FrameWork.Function.NConvert.ToInt32(order.Item.Grade) > drugPermission)
                                {
                                    error = "您的职称(职级)没有开立此药品【" + order.Item.Name + "】的权限！";
                                    rev = 0;
                                }
                            }
                            else
                            {
                                try
                                {
                                    //药品等级与与医生的抗生素等级（A、B、C）比较
                                    //药品等级比较高的话 就没有医嘱开立权限
                                    if (FS.FrameWork.Function.NConvert.ToInt32(order.Item.Grade) > drugPermission)
                                    {
                                        //error = "您的职称(职级)没有开立此药品【" + order.Item.Name + "】的权限！\r\n系统默认该医嘱为无效状态，需上级医生审核后才能生效！";
                                        //这块系统还没有处理好，所以先屏蔽了
                                        error = "您的职称(职级)没有开立此药品【" + order.Item.Name + "】的权限！";
                                        rev = 0;
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                }
                #endregion
                #endregion

                #region 特限药物开立权

                //目前药品方面还没有整理此功能，我们就没有必要乱搞乱搞了

                ArrayList alSpeDrugs = myPhaCons.QueryAllSpeDrugPerAndDep();


                if (alSpeDrugs != null && alSpeDrugs.Count > 0)
                    {
                        bool isHavePriv = true;

                        foreach (Employee emp in alSpeDrugs)
                        {
                            if (emp.User01 == order.Item.ID)
                            {
                                isHavePriv = false;
                            }
                        }

                        //特限药怎么会有这么多表存储的
                        //pha_com_spedrug_per_dep
                        //pha_com_spedrug_maintenance
                        //pha_com_spedrug

                        foreach (Employee emp in alSpeDrugs)
                        {
                            //ID存医生或科室编码;User01存项目编码
                            //if (emp.ID + emp.User01 == doctor.ID + dept.ID)
                            if (emp.ID + emp.User01 == doctor.ID + order.Item.ID)
                            {
                                isHavePriv = true;
                                break;
                            }
                            if (emp.ID + emp.User01 == dept.ID + order.Item.ID)
                            {
                                isHavePriv = true;
                                break;
                            }
                        }

                        if (!isHavePriv)
                        {
                            error = "此药品为特限药品,\r\n你没有开立药品【" + order.Item.Name + "】的权限！";
                            return -1;
                        }
                    }
                #endregion

                #endregion
            }
            return rev;
        }

        #endregion
    }
}
