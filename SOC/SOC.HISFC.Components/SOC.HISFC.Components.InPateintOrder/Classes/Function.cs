using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using FS.HISFC.Models.Base;

namespace FS.SOC.HISFC.Components.InPateintOrder.Classes
{
    /// <summary>
    /// [功能描述: 医嘱公用函数]<br></br>
    /// [创 建 者: wolf]<br></br>
    /// [创建时间: 2004-10-12]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间=''
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public class Function
    {

        #region 医嘱管理
        /// <summary>
        /// 设置医嘱首次频次信息
        /// </summary>
        /// <param name="order"></param>
        public static void SetDefaultOrderFrequency(FS.HISFC.Models.Order.Inpatient.Order order)
        {
            if (order.OrderType.IsDecompose || order.OrderType.ID == "CD" ||
                order.OrderType.ID == "QL")//默认为项目的频次
            {
                if (order.Item.GetType() == typeof(FS.HISFC.Models.Pharmacy.Item))
                {
                    order.Frequency = (order.Item as FS.HISFC.Models.Pharmacy.Item).Frequency.Clone();
                    order.Frequency.Time = "25:00";//默认为２５点，需要更新
                }
            }
            //else if (order.Item.IsPharmacy && order.OrderType.IsDecompose == false)//药品 临时医嘱，频次为空，默认为需要时候服用prn
            else if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug && order.OrderType.IsDecompose == false)//药品 临时医嘱，频次为空，默认为需要时候服用prn
            {
                order.Frequency.ID = GetDefaultFrequencyID();//药品临时医嘱默认为需要时执行
            }
            //else if (order.Item.IsPharmacy == false && order.OrderType.IsDecompose == false)
            else if (order.Item.ItemType != EnumItemType.Drug && order.OrderType.IsDecompose == false)
            {
                order.Frequency.ID = GetDefaultFrequencyID();//非药品临时医嘱默认为每天一次
            }
        }

        /// <summary>
        /// 是否允许开立
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public static bool IsPermission(FS.HISFC.Models.RADT.PatientInfo patient
            , FS.HISFC.Models.Order.OrderType orderType
            , FS.HISFC.Models.Base.Item item)
        {
            return false;
        }

        /// <summary>
        /// 根据医嘱类别判断,是否自动计算总量
        /// </summary>
        /// <param name="orderType"></param>
        /// <returns></returns>
        [Obsolete("作废", true)]
        public static bool IsAutoCalTotal(FS.HISFC.Models.Order.OrderType orderType)
        {
            return false;
        }

        #region 获取频次信息

        /// <summary>
        /// 获取开立的默认频次
        /// </summary>
        /// <returns></returns>
        public static FS.HISFC.Models.Order.Frequency GetDefaultFrequency()
        {
            if (GetAllFrequency() != null && GetAllFrequency().Count > 0)
            {
                return GetAllFrequency()[0] as FS.HISFC.Models.Order.Frequency;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取开立的默认频次ID
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultFrequencyID()
        {
            if (GetDefaultFrequency() == null)
            {
                return "PRN";
            }
            else
            {
                return GetDefaultFrequency().ID;
            }
        }

        /// <summary>
        /// 频次列表
        /// </summary>
        private static ArrayList alFrequency = null;

        /// <summary>
        /// 频次帮助类
        /// </summary>
        private static FS.FrameWork.Public.ObjectHelper freqHelper = null;

        /// <summary>
        /// 获得所有的频次帮助类
        /// </summary>
        /// <returns></returns>
        public static FS.FrameWork.Public.ObjectHelper GetFreqHelper()
        {
            if (freqHelper == null)
            {
                freqHelper = new FS.FrameWork.Public.ObjectHelper();
                freqHelper.ArrayObject = GetAllFrequency();
            }

            return freqHelper;
        }

        /// <summary>
        /// 获取所有频次
        /// </summary>
        /// <returns></returns>
        public static ArrayList GetAllFrequency()
        {
            if (alFrequency == null || alFrequency.Count == 0)
            {
                FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
                alFrequency = managerIntegrate.QuereyFrequencyList();

                if (alFrequency == null)
                {
                    MessageBox.Show("获取频次信息失败：" + managerIntegrate.Err);
                    return null;
                }
            }

            return alFrequency;
        }

        #endregion

        /// <summary>
        /// 计算总量
        /// </summary>
        /// <param name="order"></param>
        /// <returns>0 成功 -1失败</returns>
        [Obsolete("作废", true)]
        public static int CalTotal(FS.HISFC.Models.Order.Inpatient.Order order, int days)
        {
            FS.HISFC.Models.Pharmacy.Item item = order.Item as FS.HISFC.Models.Pharmacy.Item;
            #region 获得时间点
            if (order.Frequency.Usage.ID == "") order.Frequency.Usage = order.Usage.Clone();
            //***************获得频次时间点(每天多少次)******************
            if (days == 0) days = 1;
            #endregion
            if (item.OnceDose == 0M)//一次剂量为零，默认显示基本剂量
                order.Qty = order.Frequency.Times.Length * days;
            else
                order.Qty = item.OnceDose / item.BaseDose * order.Frequency.Times.Length * days;

            return 0;
        }

        /// <summary>
        /// 医嘱类型列表
        /// </summary>
        /// <param name="isShort">是否临时医嘱</param>
        [Obsolete("作废，换做缓存表FS.SOC.HISFC.BizProcess.Cache.Order的GetOrderSysType方法", true)]
        public static System.Collections.ArrayList OrderCatatagory(bool isShort)
        {
            System.Collections.ArrayList alAllType = FS.HISFC.Models.Base.SysClassEnumService.List();
            FS.FrameWork.Models.NeuObject objAll = new FS.FrameWork.Models.NeuObject();
            objAll.ID = "ALL";
            objAll.Name = "全部";
            alAllType.Add(objAll);
            if (isShort)
                return alAllType;//临时医嘱显示全部

            //长期医嘱屏蔽些东西
            System.Collections.ArrayList alOrderType = new ArrayList();
            foreach (FS.FrameWork.Models.NeuObject obj in alAllType)
            {
                switch (obj.ID)
                {
                    case "UO": //手术
                    case "UC": //检查
                    case "PCC": //中草药
                    case "MC": //会诊
                    case "MRB": //转床
                    case "MRD": //转科
                    case "MRH": //预约出院
                    case "UL":  //检验
                        break;
                    default:
                        alOrderType.Add(obj);
                        break;
                }
            }
            return alOrderType;
        }

        /// <summary>
        /// 皮试字样
        /// </summary>
        //public const string TipHypotest = "(需皮试)";
        public const string TipHypotest = "";

        #endregion

        #region 处方权

        /// <summary>
        /// 职级帮助类
        /// </summary>
        static FS.FrameWork.Public.ObjectHelper levlHelper = null;

        /// <summary>
        /// 人员帮助类
        /// </summary>
        static FS.FrameWork.Public.ObjectHelper emplHelper = null;

        static FS.HISFC.Models.Base.Employee emplObj = null;
        static FS.HISFC.Models.Base.Const levlObj = null;

        /// <summary>
        /// 检查是否有处方权
        /// </summary>
        /// <param name="emplCode"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public static int CheckIsHaveOrderPower(string emplCode, ref string errInfo)
        {
            try
            {
                //医疗权限验证方法//{BFDA551D-7569-47dd-85C4-1CA21FE494BD}

                FS.HISFC.BizProcess.Integrate.Common.ControlParam controler = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                //是否控制处方权
                bool isUseControl = controler.GetControlParam<bool>("HNMET1", true, false);

                if (isUseControl)
                {
                    //怎么根据药品等级来判断处方权了？？？
                    #region 获取常数
                    if (alDrugGrade == null)
                    {
                        alDrugGrade = myConstant.GetAllList("SpeDrugGrade");
                    }

                    if (alDrugPosition == null)
                    {
                        alDrugPosition = myConstant.GetAllList("SpeDrugPosition");
                    }

                    #endregion

                    #region 获取对照

                    int drugPermission = 0;

                    //判断对照数据(职级<-->药品等级  职务<-->药品等级)

                    FS.HISFC.Models.Base.Employee employee = new FS.HISFC.Models.Base.Employee();

                    employee = personMgr.GetPersonByID(emplCode);

                    if (employee == null)
                    {
                        errInfo = personMgr.Err;
                        return -1;
                    }

                    for (int i = 0; i < alDrugGrade.Count; i++)
                    {
                        FS.FrameWork.Models.NeuObject obj = alDrugGrade[i] as FS.FrameWork.Models.NeuObject;

                        int intIndex = obj.ID.IndexOf('|');

                        if (intIndex <= 0)
                        {
                            continue;
                        }

                        string level = obj.ID.Substring(0, intIndex);

                        if (employee.Level.ID.Trim() == level.Trim() && (obj as FS.HISFC.Models.Base.Const).IsValid)
                        {
                            drugPermission = FS.FrameWork.Function.NConvert.ToInt32(obj.ID.Substring(intIndex + 1));
                            break;
                        }
                    }

                    for (int i = 0; i < alDrugPosition.Count; i++)
                    {
                        FS.FrameWork.Models.NeuObject obj = alDrugPosition[i] as FS.FrameWork.Models.NeuObject;

                        int intIndex = obj.ID.IndexOf('|');

                        if (intIndex <= 0)
                        {
                            continue;
                        }

                        string level = obj.ID.Substring(0, intIndex);

                        if (employee.Duty.ID.Trim() == level.Trim() && (obj as FS.HISFC.Models.Base.Const).IsValid)
                        {
                            drugPermission = FS.FrameWork.Function.NConvert.ToInt32(obj.ID.Substring(intIndex + 1));
                            break;
                        }
                    }

                    if (drugPermission > 0)
                        return 1;

                    #endregion
                }
                else
                {
                    //由于妇幼要求较急 先按照职级常数 临时处理
                    if (levlHelper == null || levlHelper.ArrayObject.Count == 0)
                    {
                        levlHelper = new FS.FrameWork.Public.ObjectHelper();
                        ArrayList alLevl = manager.GetConstantList(FS.HISFC.Models.Base.EnumConstant.LEVEL);
                        if (alLevl == null)
                        {
                            errInfo = manager.Err;
                            return -1;
                        }
                        levlHelper.ArrayObject = alLevl;
                    }

                    if (emplHelper == null || emplHelper.ArrayObject.Count == 0)
                    {
                        emplHelper = new FS.FrameWork.Public.ObjectHelper();
                        ArrayList alEmpl = manager.QueryEmployeeAll();
                        if (alEmpl == null)
                        {
                            errInfo = manager.Err;
                            return -1;
                        }
                        emplHelper.ArrayObject = alEmpl;
                    }

                    emplObj = emplHelper.GetObjectFromID(emplCode) as FS.HISFC.Models.Base.Employee;
                    if (emplObj == null)
                    {
                        errInfo = "获取人员信息失败！请联系信息科！";
                        return -1;
                    }

                    levlObj = levlHelper.GetObjectFromID(emplObj.Level.ID) as FS.HISFC.Models.Base.Const;
                    if (levlObj == null)
                    {
                        errInfo = "获取职级信息失败！请联系信息科！";
                        return -1;
                    }

                    if (levlObj.Memo.Trim() == "无处方权")
                    {
                        return 0;
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


        #region 警戒线提示

        /// <summary>
        /// 判断警戒线
        /// </summary>
        /// <param name="paitentInfo"></param>
        /// <param name="alOrders"></param>
        /// <param name="messType"></param>
        /// <returns></returns>
        public static int CheckMoneyAlert(FS.HISFC.Models.RADT.PatientInfo patientInfo, ArrayList alOrders, MessType messType)
        {
            if (patientInfo.PVisit.MoneyAlert == 0)
            {
                return 1;
            }
            if (alOrders.Count == 0)
            {
                return 1;
            }

            decimal totCost = 0;
            Hashtable hsCombNo = new Hashtable();

            if (alOrders[0].GetType() == typeof(FS.HISFC.Models.Order.ExecOrder))
            {
                foreach (FS.HISFC.Models.Order.ExecOrder inOrder in alOrders)
                {
                    if (inOrder.Order.Item.ItemType == EnumItemType.Drug)
                    {
                        totCost += FS.FrameWork.Public.String.FormatNumber((inOrder.Order.Item.Price * inOrder.Order.Item.Qty / inOrder.Order.Item.PackQty), 2);
                    }
                    else
                    {
                        totCost += FS.FrameWork.Public.String.FormatNumber((inOrder.Order.Item.Price * inOrder.Order.Item.Qty), 2);
                    }
                }
            }
            else
            {
                foreach (FS.HISFC.Models.Order.Inpatient.Order inOrder in alOrders)
                {
                    if (inOrder.Status != 0)
                    {
                        continue;
                    }

                    if (inOrder.Item.ItemType == EnumItemType.Drug)
                    {
                        totCost += FS.FrameWork.Public.String.FormatNumber((inOrder.Item.Price * inOrder.Item.Qty / inOrder.Item.PackQty), 2);
                    }
                    else
                    {
                        totCost += FS.FrameWork.Public.String.FormatNumber((inOrder.Item.Price * inOrder.Item.Qty), 2);
                    }
                    if (!hsCombNo.Contains(inOrder.Combo.ID))
                    {
                        FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();
                        ArrayList alCombOrder = orderManager.QueryOrderByCombNO(inOrder.Combo.ID, true);
                        foreach (FS.HISFC.Models.Order.Inpatient.Order subOrder in alCombOrder)
                        {
                            if (subOrder.Item.ItemType == EnumItemType.Drug)
                            {
                                totCost += FS.FrameWork.Public.String.FormatNumber((subOrder.Item.Price * subOrder.Item.Qty / subOrder.Item.PackQty), 2);
                            }
                            else
                            {
                                totCost += FS.FrameWork.Public.String.FormatNumber((subOrder.Item.Price * subOrder.Item.Qty), 2);
                            }
                        }
                        hsCombNo.Add(inOrder.Combo.ID, null);
                    }
                }
            }

            if (totCost == 0)
            {
                return 1;
            }

            if (patientInfo.FT.LeftCost - totCost < patientInfo.PVisit.MoneyAlert)
            {
                if (messType == MessType.Y)
                {
                    MessageBox.Show("患者【" + patientInfo.Name + "】已经欠费，不能继续操作！\r\n\r\n\r\n收费金额： " + totCost.ToString() + "\r\n\r\n警戒线： " + patientInfo.PVisit.MoneyAlert.ToString() + "\r\n\r\n余额： " + patientInfo.FT.LeftCost.ToString() + "\r\n\r\n收费后余额： " + (patientInfo.FT.LeftCost - totCost).ToString() + " 小于警戒线 " + patientInfo.PVisit.MoneyAlert.ToString() + "\r\n\r\n", "警告", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Stop);
                    return -1;
                }
                else if (messType == MessType.M)
                {
                    if (MessageBox.Show("患者【" + patientInfo.Name + "】已经欠费，是否继续操作？\r\n\r\n\r\n收费金额： " + totCost.ToString() + "\r\n\r\n警戒线： " + patientInfo.PVisit.MoneyAlert.ToString() + "\r\n\r\n余额： " + patientInfo.FT.LeftCost.ToString() + "\r\n\r\n收费后余额： " + (patientInfo.FT.LeftCost - totCost).ToString() + " 小于警戒线 " + patientInfo.PVisit.MoneyAlert.ToString() + "\r\n\r\n", "询问", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return -1;
                    }
                }
                else
                {

                }
            }
            return 1;
        }
        #endregion

        #region 常用常数

        private static FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
        private static FS.FrameWork.Public.ObjectHelper helpUsage = null;

        /// <summary>
        /// 用法
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper HelperUsage
        {
            get
            {
                if (helpUsage == null)
                    helpUsage = new FS.FrameWork.Public.ObjectHelper(manager.GetConstantList(FS.HISFC.Models.Base.EnumConstant.USAGE));
                return helpUsage;
            }
            set
            {
                helpUsage = value;
            }
        }

        private static FS.FrameWork.Public.ObjectHelper herbalUsageHelper = null;
        public static FS.FrameWork.Public.ObjectHelper HerbalUsageHelper
        {
            get
            {
                if (herbalUsageHelper == null)
                {
                    GetHerbalUsage();
                }
                return herbalUsageHelper;
            }
        }

        /// <summary>
        /// 获取草药用法
        /// </summary>
        /// <returns></returns>
        public static FS.FrameWork.Public.ObjectHelper GetHerbalUsage()
        {
            bool isHerbalUsage = false;
            herbalUsageHelper = new FS.FrameWork.Public.ObjectHelper();
            if (HelperUsage == null)
            {
                HelperUsage = new FS.FrameWork.Public.ObjectHelper();
                FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();
                HelperUsage.ArrayObject = constantMgr.GetList(FS.HISFC.Models.Base.EnumConstant.USAGE);
            }
            if (HelperUsage == null || HelperUsage.ArrayObject == null)
            {
                return null;
            }

            foreach (FS.FrameWork.Models.NeuObject neuObject in HelperUsage.ArrayObject)
            {
                FS.HISFC.Models.Base.Const usage = (FS.HISFC.Models.Base.Const)neuObject;
                if (usage == null)
                {
                    return null;
                }
                //判断以H开头的都为草药用法
                if (!usage.UserCode.Equals(null) && usage.UserCode.StartsWith("H"))
                {
                    herbalUsageHelper.ArrayObject.Add(usage);
                }
            }
            return herbalUsageHelper;
        }

        private static FS.FrameWork.Public.ObjectHelper helpFrequency = null;
        /// <summary>
        /// 频次
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper HelperFrequency
        {
            get
            {
                if (helpFrequency == null)
                    helpFrequency = new FS.FrameWork.Public.ObjectHelper(manager.QuereyFrequencyList());
                return helpFrequency;
            }
            set
            {
                helpFrequency = value;
            }
        }

        #region 新增样本和检查部位{0A4BC81A-2F2B-4dae-A8E6-C8DC1F87AA32}

        private static FS.FrameWork.Public.ObjectHelper helpSample = null;
        /// <summary>
        /// 样本
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper HelperSample
        {
            get
            {
                if (helpSample == null)
                    helpSample = new FS.FrameWork.Public.ObjectHelper(manager.GetConstantList(FS.HISFC.Models.Base.EnumConstant.LABSAMPLE));
                return helpSample;
            }
            set
            {
                helpSample = value;
            }
        }

        private static FS.FrameWork.Public.ObjectHelper helpCheckPart = null;
        /// <summary>
        /// 检查部位
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper HelperCheckPart
        {
            get
            {
                if (helpCheckPart == null)
                    helpCheckPart = new FS.FrameWork.Public.ObjectHelper(manager.GetConstantList("CHECKPART"));
                return helpCheckPart;
            }
            set
            {
                helpCheckPart = value;
            }
        }

        #endregion

        #endregion

        #region "是否默认开立医嘱时间" (已作废)

        //protected static bool bIsDefaultMoDate = false;
        //protected static bool bFirst = true;//计数器

        ///// <summary>
        ///// 是否默认开立医嘱时间
        ///// </summary>
        //public static bool IsDefaultMoDate
        //{
        //    get
        //    {
        //        if (bFirst)
        //        {
        //            try//获得是否修改 开立时间添加首日量200012
        //            {
        //                FS.FrameWork.Management.ControlParam mControl = new FS.FrameWork.Management.ControlParam();
        //                bIsDefaultMoDate = FS.FrameWork.Function.NConvert.ToBoolean(mControl.QueryControlerInfo("200012"));
        //            }
        //            catch
        //            {
        //            }
        //            bFirst = false;
        //        }
        //        else
        //        {
        //        }
        //        return bIsDefaultMoDate;
        //    }
        //}
        #endregion

        #region 默认开立时间

        /// <summary>
        /// 默认开始时间模式 三位数字表示，第1位为长嘱，2位为临嘱，3位为门诊 类似：100
        /// 0 当前时间；1 默认上条医嘱时间；2 默认当天凌晨；3 默认当天中午；4 默认当天晚上
        /// </summary>
        static string defaultMoDateMode;

        static FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();

        /// <summary>
        /// 获取默认医嘱开始时间
        /// </summary>
        /// <param name="orderType">0 门诊处方；1 长期医嘱；2 临时医嘱</param>
        /// <returns></returns>
        public static DateTime GetDefaultMoBeginDate(int orderType)
        {
            if (string.IsNullOrEmpty(defaultMoDateMode))
            {
                FS.HISFC.BizProcess.Integrate.Common.ControlParam controlMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                defaultMoDateMode = controlMgr.GetControlParam<string>("HNMET3", false, "000");
            }

            DateTime dtNow = dbMgr.GetDateTimeFromSysDateTime();

            try
            {
                int value = 0;
                if (orderType == 0)
                {
                    value = FS.FrameWork.Function.NConvert.ToInt32(defaultMoDateMode.Substring(0, 1));
                }
                else if (orderType == 1)
                {
                    value = FS.FrameWork.Function.NConvert.ToInt32(defaultMoDateMode.Substring(1, 1));
                }
                else
                {
                    value = FS.FrameWork.Function.NConvert.ToInt32(defaultMoDateMode.Substring(2, 1));
                }

                switch (value)
                {
                    case 0: //当前时间
                        return dtNow;
                        break;
                    case 1: //默认上条医嘱时间
                        return DateTime.MinValue;
                        break;
                    case 2: //默认当天凌晨
                        return new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 0, 0, 0);
                        break;
                    case 3: //默认当天中午
                        return new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 12, 0, 0);
                        break;
                    case 4: //默认当天晚上
                        return new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 23, 59, 59);
                        break;
                    default:
                        return dtNow;
                        break;
                }
            }
            catch
            {
                return dtNow;
            }
        }

        #endregion

        #region 获取默认首日量


        /// <summary>
        /// 默认首日量显示 0 固定为1；1 根据开立时间自动计算；2 最大频次数
        /// </summary>
        private static int firstOrderDaysMode = -1;

        /// <summary>
        /// 根据开始时间、频次，获取默认首日量
        /// </summary>
        /// <param name="inOrder"></param>
        /// <param name="dtNow"></param>
        /// <returns></returns>
        public static int GetFirstOrderDays(FS.HISFC.Models.Order.Inpatient.Order inOrder, DateTime dtNow)
        {
            if (firstOrderDaysMode == -1)
            {

                FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamManager = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

                firstOrderDaysMode = controlParamManager.GetControlParam<int>("HNZY34", true, 0);
            }

            int count = 0;

            if (firstOrderDaysMode == 0)
            {
                count = 1;
            }
            else if (firstOrderDaysMode == 1)
            {
                if (inOrder.BeginTime == DateTime.MinValue)
                {
                    inOrder.BeginTime = dtNow;
                }
                count = 0;

                for (int i = 0; i < inOrder.Frequency.Times.Length; i++)
                {
                    DateTime dt = FS.FrameWork.Function.NConvert.ToDateTime(inOrder.BeginTime.ToString("yyyy-MM-dd") + " " + inOrder.Frequency.Times[i]);
                    if (dt >= inOrder.BeginTime)
                    {
                        count += 1;
                    }
                }
            }
            else
            {
                count = inOrder.Frequency.Times.Length;
            }

            if (inOrder.Frequency.ID.ToUpper().Replace(".", "") == "Q1H"
                || inOrder.Frequency.ID.ToUpper().Replace(".", "") == "QH")
            {
                if (inOrder.BeginTime == DateTime.MinValue)
                {
                    inOrder.BeginTime = dtNow;
                }
                count = 0;

                for (int i = 0; i < inOrder.Frequency.Times.Length; i++)
                {
                    DateTime dt = FS.FrameWork.Function.NConvert.ToDateTime(inOrder.BeginTime.ToString("yyyy-MM-dd") + " " + inOrder.Frequency.Times[i]);
                    if (dt >= inOrder.BeginTime)
                    {
                        count += 1;
                    }
                }
            }

            if (count == 0)
            {
                count = 1;
            }
            return count;
        }

        #endregion

        #region 新开医嘱默认生效间隔天数 (已作废)

        //protected static int moDateDays = 0;
        //protected static bool isInitMoDateDays = true;

        //public static int MoDateDays
        //{
        //    get
        //    {
        //        if (isInitMoDateDays)
        //        {
        //            FS.FrameWork.Management.ControlParam mControl = new FS.FrameWork.Management.ControlParam();
        //            moDateDays = FS.FrameWork.Function.NConvert.ToInt32(mControl.QueryControlerInfo("200040"));

        //            isInitMoDateDays = false;
        //        }

        //        return moDateDays;
        //    }
        //}

        #endregion

        #region 组合医嘱 传入的对象，column 组合项目列
        /// <summary>
        /// 括号在右边
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="column"></param>
        /// <param name="DrawColumn"></param>
        /// <param name="ChildViewLevel"></param>
        public static void DrawCombo(object sender, int column, int DrawColumn, int ChildViewLevel)
        {
            switch (sender.GetType().ToString().Substring(sender.GetType().ToString().LastIndexOf(".") + 1))
            {
                case "SheetView":
                    FarPoint.Win.Spread.SheetView o = sender as FarPoint.Win.Spread.SheetView;
                    int i = 0;
                    string tmp = "", curComboNo = "";
                    if (ChildViewLevel == 0)
                    {
                        for (i = 0; i < o.RowCount; i++)
                        {
                            #region "画"
                            if (o.Cells[i, column].Text == "0") o.Cells[i, column].Text = "";
                            tmp = o.Cells[i, column].Text + "";
                            o.Cells[i, column].Tag = tmp;
                            if (curComboNo != tmp && tmp != "") //是头
                            {
                                curComboNo = tmp;
                                o.Cells[i, DrawColumn].Text = "┓";
                                try
                                {
                                    if (o.Cells[i - 1, DrawColumn].Text == "┃")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "┛";
                                    }
                                    else if (o.Cells[i - 1, DrawColumn].Text == "┓")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "";
                                    }
                                }
                                catch { }
                            }
                            else if (curComboNo == tmp && tmp != "")
                            {
                                o.Cells[i, DrawColumn].Text = "┃";
                            }
                            else if (curComboNo != tmp && tmp == "")
                            {
                                try
                                {
                                    if (o.Cells[i - 1, DrawColumn].Text == "┃")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "┛";
                                    }
                                    else if (o.Cells[i - 1, DrawColumn].Text == "┓")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "";
                                    }
                                }
                                catch { }
                                o.Cells[i, DrawColumn].Text = "";
                                curComboNo = "";
                            }
                            if (i == o.RowCount - 1 && o.Cells[i, DrawColumn].Text == "┃") o.Cells[i, DrawColumn].Text = "┛";
                            if (i == o.RowCount - 1 && o.Cells[i, DrawColumn].Text == "┓") o.Cells[i, DrawColumn].Text = "";
                            //o.Cells[i, DrawColumn].ForeColor = System.Drawing.Color.Red;
                            #endregion
                        }
                    }
                    else if (ChildViewLevel == 1)
                    {
                        for (int m = 0; m < o.RowCount; m++)
                        {
                            FarPoint.Win.Spread.SheetView c = o.GetChildView(m, 0);
                            for (int j = 0; j < c.RowCount; j++)
                            {
                                #region "画"
                                if (c.Cells[j, column].Text == "0") c.Cells[j, column].Text = "";
                                tmp = c.Cells[j, column].Text + "";

                                c.Cells[j, column].Tag = tmp;
                                if (curComboNo != tmp && tmp != "") //是头
                                {
                                    curComboNo = tmp;
                                    c.Cells[j, DrawColumn].Text = "┓";
                                    try
                                    {
                                        if (c.Cells[j - 1, DrawColumn].Text == "┃")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "┛";
                                        }
                                        else if (c.Cells[j - 1, DrawColumn].Text == "┓")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "";
                                        }
                                    }
                                    catch { }
                                }
                                else if (curComboNo == tmp && tmp != "")
                                {
                                    c.Cells[j, DrawColumn].Text = "┃";
                                }
                                else if (curComboNo != tmp && tmp == "")
                                {
                                    try
                                    {
                                        if (c.Cells[j - 1, DrawColumn].Text == "┃")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "┛";
                                        }
                                        else if (c.Cells[j - 1, DrawColumn].Text == "┓")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "";
                                        }
                                    }
                                    catch { }
                                    c.Cells[j, DrawColumn].Text = "";
                                    curComboNo = "";
                                }
                                if (j == c.RowCount - 1 && c.Cells[j, DrawColumn].Text == "┃") c.Cells[j, DrawColumn].Text = "┛";
                                if (j == c.RowCount - 1 && c.Cells[j, DrawColumn].Text == "┓") c.Cells[j, DrawColumn].Text = "";
                                //c.Cells[j, DrawColumn].ForeColor = System.Drawing.Color.Red;
                                #endregion

                            }
                        }
                    }
                    break;
            }

        }
        /// <summary>
        /// 括号在左边
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="column"></param>
        /// <param name="DrawColumn"></param>
        /// <param name="ChildViewLevel"></param>
        public static void DrawComboLeft(object sender, int column, int DrawColumn, int ChildViewLevel)
        {
            switch (sender.GetType().ToString().Substring(sender.GetType().ToString().LastIndexOf(".") + 1))
            {
                case "SheetView":
                    FarPoint.Win.Spread.SheetView o = sender as FarPoint.Win.Spread.SheetView;
                    int i = 0;
                    string tmp = "", curComboNo = "";
                    if (ChildViewLevel == 0)
                    {
                        for (i = 0; i < o.RowCount; i++)
                        {
                            #region "画"
                            if (o.Cells[i, column].Text == "0") o.Cells[i, column].Text = "";
                            tmp = o.Cells[i, column].Text + "";
                            o.Cells[i, column].Tag = tmp;
                            if (curComboNo != tmp && tmp != "") //是头
                            {
                                curComboNo = tmp;
                                o.Cells[i, DrawColumn].Text = "┏";
                                try
                                {
                                    if (o.Cells[i - 1, DrawColumn].Text == "┃")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "┗";
                                    }
                                    else if (o.Cells[i - 1, DrawColumn].Text == "┏")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "";
                                    }
                                }
                                catch { }
                            }
                            else if (curComboNo == tmp && tmp != "")
                            {
                                o.Cells[i, DrawColumn].Text = "┃";
                            }
                            else if (curComboNo != tmp && tmp == "")
                            {
                                try
                                {
                                    if (o.Cells[i - 1, DrawColumn].Text == "┃")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "┗";
                                    }
                                    else if (o.Cells[i - 1, DrawColumn].Text == "┏")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "";
                                    }
                                }
                                catch { }
                                o.Cells[i, DrawColumn].Text = "";
                                curComboNo = "";
                            }
                            if (i == o.RowCount - 1 && o.Cells[i, DrawColumn].Text == "┃") o.Cells[i, DrawColumn].Text = "┗";
                            if (i == o.RowCount - 1 && o.Cells[i, DrawColumn].Text == "┏") o.Cells[i, DrawColumn].Text = "";
                            o.Cells[i, DrawColumn].ForeColor = System.Drawing.Color.Red;
                            #endregion
                        }
                    }
                    else if (ChildViewLevel == 1)
                    {
                        for (int m = 0; m < o.RowCount; m++)
                        {
                            FarPoint.Win.Spread.SheetView c = o.GetChildView(m, 0);
                            for (int j = 0; j < c.RowCount; j++)
                            {
                                #region "画"
                                if (c.Cells[j, column].Text == "0") c.Cells[j, column].Text = "";
                                tmp = c.Cells[j, column].Text + "";

                                c.Cells[j, column].Tag = tmp;
                                if (curComboNo != tmp && tmp != "") //是头
                                {
                                    curComboNo = tmp;
                                    c.Cells[j, DrawColumn].Text = "┏";
                                    try
                                    {
                                        if (c.Cells[j - 1, DrawColumn].Text == "┃")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "┗";
                                        }
                                        else if (c.Cells[j - 1, DrawColumn].Text == "┏")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "";
                                        }
                                    }
                                    catch { }
                                }
                                else if (curComboNo == tmp && tmp != "")
                                {
                                    c.Cells[j, DrawColumn].Text = "┃";
                                }
                                else if (curComboNo != tmp && tmp == "")
                                {
                                    try
                                    {
                                        if (c.Cells[j - 1, DrawColumn].Text == "┃")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "┗";
                                        }
                                        else if (c.Cells[j - 1, DrawColumn].Text == "┏")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "";
                                        }
                                    }
                                    catch { }
                                    c.Cells[j, DrawColumn].Text = "";
                                    curComboNo = "";
                                }
                                if (j == c.RowCount - 1 && c.Cells[j, DrawColumn].Text == "┃") c.Cells[j, DrawColumn].Text = "┗";
                                if (j == c.RowCount - 1 && c.Cells[j, DrawColumn].Text == "┏") c.Cells[j, DrawColumn].Text = "";
                                c.Cells[j, DrawColumn].ForeColor = System.Drawing.Color.Red;
                                #endregion

                            }
                        }
                    }
                    break;
            }

        }
        /// <summary>
        /// 画组合号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="column"></param>
        /// 		/// <param name="DrawColumn"></param>
        public static void DrawCombo(object sender, int column, int DrawColumn)
        {
            DrawCombo(sender, column, DrawColumn, 0);
        }
        /// <summary>
        /// 画组合号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="column"></param>
        /// 		/// <param name="DrawColumn"></param>
        public static void DrawComboLeft(object sender, int column, int DrawColumn)
        {
            DrawComboLeft(sender, column, DrawColumn, 0);
        }

        #endregion

        #region 获得是否可以开库存为零的药品
        /// <summary>
        /// 获得是否可以开库存为零的药品
        /// </summary>
        /// <returns></returns>
        public static int GetIsOrderCanNoStock()
        {
            FS.FrameWork.Management.ControlParam controler = new FS.FrameWork.Management.ControlParam();
            return FS.FrameWork.Function.NConvert.ToInt32(controler.QueryControlerInfo("200001"));

        }
        #endregion

        #region 检查库存

        private static FS.HISFC.BizProcess.Integrate.Pharmacy phaManager = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        /// <summary>
        /// 检查库存
        /// </summary>
        /// <param name="iCheck"></param>
        /// <param name="itemID"></param>
        /// <param name="itemName"></param>
        /// <param name="deptCode"></param>
        /// <param name="qty"></param>
        /// <param name="sendType">发送类型 A:全部、O:门诊、I:住院</param>
        /// <returns></returns>
        public static bool CheckPharmercyItemStock(int iCheck, string itemID, string itemName, string deptCode, decimal qty, string sendType)
        {
            //FS.HISFC.Manager.Item manager = new FS.HISFC.BizLogic.Pharmacy.Item();
            //FS.HISFC.Models.Pharmacy.item item = null;
            //.

            FS.HISFC.Models.Pharmacy.Storage phaItem = null;


            switch (iCheck)
            {
                case 0:
                    //houwb 2011-5-30 增加发送类型判断
                    //phaItem = phaManager.GetItemForInpatient(deptCode, itemID);
                    phaItem = phaManager.GetItemStorage(deptCode, sendType, itemID);

                    if (phaItem == null) return true;
                    if (qty > FS.FrameWork.Function.NConvert.ToDecimal(phaItem.StoreQty))
                    {
                        return false;
                    }
                    break;
                case 1:
                    //houwb 2011-5-30 增加发送类型判断
                    //phaItem = phaManager.GetItemForInpatient(deptCode, itemID);
                    phaItem = phaManager.GetItemStorage(deptCode, sendType, itemID);


                    if (phaItem == null) return true;
                    if (qty > FS.FrameWork.Function.NConvert.ToDecimal(phaItem.StoreQty))
                    {
                        if (MessageBox.Show("药品【" + itemName + "】的库存不够！是否继续执行！", "提示库存不足", MessageBoxButtons.OKCancel) == DialogResult.OK)
                            return true;
                        else
                            return false;
                    }
                    break;
                case 2:
                    break;
                default:
                    return true;
            }
            return true;
        }
        #endregion

        #region 集中发送
        /// <summary>
        /// 是否集中发送过
        /// </summary>
        /// <param name="DeptCode">科室编码</param>
        /// <returns>返回科室扩展实体</returns>
        public static FS.HISFC.Models.Base.ExtendInfo IsDeptHaveDruged(string DeptCode)
        {
            FS.FrameWork.Management.ExtendParam m = new FS.FrameWork.Management.ExtendParam();
            FS.HISFC.Models.Base.ExtendInfo obj = m.GetComExtInfo(FS.HISFC.Models.Base.EnumExtendClass.DEPT, "ORDER_ISDRUGED", DeptCode);
            if (obj == null) return null;
            return obj;
        }
        /// <summary>
        /// 已经集中发送
        /// </summary>
        /// <param name="DeptCode"></param>
        /// <returns></returns>
        public static int HaveDruged(string DeptCode)
        {
            return Function.HaveDruged(DeptCode, 1M);
        }
        /// <summary>
        /// 更新没集中发送
        /// </summary>
        /// <param name="DeptCode"></param>
        /// <returns></returns>
        public static int NotHaveDruged(string DeptCode)
        {
            return Function.HaveDruged(DeptCode, 0M);
        }
        /// <summary>
        /// 更新扩展信息表
        /// </summary>
        /// <param name="DeptCode"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static int HaveDruged(string DeptCode, decimal i)
        {
            FS.FrameWork.Management.ExtendParam m = new FS.FrameWork.Management.ExtendParam();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(m.Connection);
            //t.BeginTransaction();
            m.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            FS.HISFC.Models.Base.ExtendInfo obj = new FS.HISFC.Models.Base.ExtendInfo();
            obj.ID = "ORDER_ISDRUGED";
            obj.Name = "住院科室集中摆药";
            obj.PropertyCode = "ORDER_ISDRUGED";
            obj.PropertyName = "住院科室集中摆药";
            obj.NumberProperty = i;
            obj.ExtendClass = FS.HISFC.Models.Base.EnumExtendClass.DEPT;
            obj.Item.ID = DeptCode;
            obj.StringProperty = "";
            obj.DateProperty = DateTime.Now;
            obj.Memo = "";
            if (m.SetComExtInfo(obj) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack(); ;
                MessageBox.Show(m.Err);
                return -1;
            }
            else
            {
                FS.FrameWork.Management.PublicTrans.Commit();
            }
            return 0;
        }
        #endregion

        #region "医嘱默认频次"
        /// <summary>
        /// 设置医嘱默认频次
        /// </summary>
        /// <param name="o"></param>
        public static void SetDefaultFrequency(FS.HISFC.Models.Order.Inpatient.Order o)
        {
            //避免医生不理解，此处不再改变频次，如果需要改变频次则人为修改

            //药品 临时医嘱，频次为空，默认为需要时候服用prn
            //if (o.Item.ItemType == EnumItemType.Drug && o.OrderType.IsDecompose == false)
            //{
            //    o.Frequency.ID = "QD";//药品临时医嘱默认为需要时执行
            //    o.Frequency = helpFrequency.GetObjectFromID(o.Frequency.ID) as FS.HISFC.Models.Order.Frequency;
            //}
            //else if (o.Item.ItemType != EnumItemType.Drug && o.OrderType.IsDecompose == false)
            //{
            //    o.Frequency.ID = "QD";//非药品临时医嘱默认为每天一次
            //    o.Frequency = helpFrequency.GetObjectFromID(o.Frequency.ID) as FS.HISFC.Models.Order.Frequency;
            //}
        }
        #endregion

        #region 患者状态判断

        /// <summary>
        /// 判断患者是否符合审核、分解医嘱的状态
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="p"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public static int CheckPatientState(string patientID, ref FS.HISFC.Models.RADT.PatientInfo p, ref string errInfo)
        {
            FS.HISFC.BizProcess.Integrate.RADT pManager = new FS.HISFC.BizProcess.Integrate.RADT();

            string memo = "";

            //判断下 转科的情况，
            string dept = "";

            if (p != null)
            {
                memo = p.Patient.Memo;
                //判断下 转科的情况，
                dept = p.PVisit.PatientLocation.Dept.ID;
            }

            p = pManager.GetPatientInfomation(patientID);

            p.Patient.Memo = memo;

            if (p == null)
            {
                errInfo = "获得患者基本信息错误" + pManager.Err;
                p = null;
                return -1;
            }
            else if (p.PVisit.InState.ID.ToString() == "O" || //出院结算
                p.PVisit.InState.ID.ToString() == "B" || //出院登记
                p.PVisit.InState.ID.ToString() == "P" || //预约出院
                p.PVisit.InState.ID.ToString() == "N")   //无费退院
            {
                errInfo = "患者" + p.Name + "不是在院状态,无法继续进行操作！";
                p = null;
                return -1;
            }

            //if (!string.IsNullOrEmpty(dept) && p.PVisit.PatientLocation.Dept.ID != dept)
            //{
            //    errInfo = "患者" + p.Name + "已经转科，无法继续进行操作！";
            //    return -1;
            //}
            return 1;
        }

        #endregion

        #region 床位处理
        /// <summary>
        /// 显示床位号
        /// </summary>
        /// <param name="orgBedNo"></param>
        /// <returns></returns>
        public static string BedDisplay(string orgBedNo)
        {
            if (orgBedNo == "")
            {
                return orgBedNo;
            }

            string tempBedNo = "";

            if (orgBedNo.Length > 4)
            {
                tempBedNo = orgBedNo.Substring(4);
            }
            else
            {
                return orgBedNo;
            }
            return tempBedNo;

        }
        #endregion

        #region 获取科室信息

        /// <summary>
        /// 科室帮助实体
        /// </summary>
        private static FS.FrameWork.Public.ObjectHelper deptHelper = null;

        /// <summary>
        /// 获取科室实体
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public static FS.HISFC.Models.Base.Department GetDept(string deptID)
        {
            try
            {
                FS.HISFC.Models.Base.Department deptTemp = null;
                if (deptHelper == null)
                {
                    deptHelper = new FS.FrameWork.Public.ObjectHelper();
                    deptHelper.ArrayObject = manager.GetDeptmentAllValid();
                }
                deptTemp = deptHelper.GetObjectFromID(deptID) as FS.HISFC.Models.Base.Department;
                if (deptTemp == null)
                {
                    FS.HISFC.BizProcess.Integrate.Manager interMgr = new FS.HISFC.BizProcess.Integrate.Manager();
                    deptTemp = interMgr.GetDepartment(deptID);
                }

                return deptTemp;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        #endregion

        #region 医嘱状态

        /// <summary>
        /// 转医嘱状态
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string OrderStatus(int i)
        {
            switch (i)
            {
                case 0:
                    return "新开立";
                case 1:
                    return "已审核";
                case 2:
                    return "执行";
                case 3:
                    return "停止/取消";
                case 4:
                    return "停止/取消";
                default:
                    return "未知";
            }
        }
        #endregion

        #region 特殊医嘱类型
        /// <summary>
        /// 特殊医嘱类型，区分麻醉医嘱，会诊医嘱，其他类型等
        /// </summary>
        /// <param name="info"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public static string MakeSpeDrugType(FS.HISFC.Models.RADT.PatientInfo info, FS.HISFC.Models.Order.Inpatient.Order order)
        {
            string speDrugType = "";

            if (order.SpeOrderType.Length <= 0)
            {
                if (info.Patient.Memo == "会诊")
                {
                    speDrugType = "CONS";
                }
                else if (info.Patient.Memo == "科室")
                {
                    speDrugType = "DEPT" + order.ExeDept.ID;
                }
                else if (info.Patient.Memo == "医技")
                {
                    speDrugType = "TERM" + order.ReciptDept.ID;
                }
                else
                {
                    speDrugType = "OTHER";
                }
            }
            else
            {
                if (order.SpeOrderType.IndexOf("DEPT") >= 0)
                {
                    speDrugType = "DEPT" + order.ExeDept.ID;
                }
            }
            return speDrugType;
        }

        /// <summary>
        /// 检查缺药、停用
        /// </summary>
        /// <param name="drugDept">扣库科室</param>
        /// <param name="item">项目</param>
        /// <param name="IsOutPatient">是否门诊开立</param>
        /// <param name="drugItem">返回的项目信息</param>
        /// <param name="errInfo">错误信息</param>
        /// <returns></returns>
        //[Obsolete("作废，移到FS.SOC.HISFC.BizProcess.OrderIntegrate.OrderBase里面", true)]
        public static int CheckDrugState(FS.FrameWork.Models.NeuObject patientInfo, FS.FrameWork.Models.NeuObject drugDept, FS.HISFC.Models.Base.Item item, bool IsOutPatient, ref FS.HISFC.Models.Pharmacy.Item drugItem, ref string errInfo)
        {
            if (item.ID == "999")
            {
                return 1;
            }
            else
            {
                FS.HISFC.Models.Pharmacy.Storage storage = null;
                return SOC.HISFC.BizProcess.OrderIntegrate.OrderBase.CheckDrugState(patientInfo, drugDept, null, item, IsOutPatient, ref drugItem, ref storage, ref errInfo);
            }
        }

        #endregion

        public static void ShowErr(string errInfo)
        {
            MessageBox.Show(errInfo, "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #region 医生站权限判断

        private static FS.HISFC.BizLogic.Order.Medical.Ability docAbility = new FS.HISFC.BizLogic.Order.Medical.Ability();

        private static FS.HISFC.BizLogic.Manager.Constant myConstant = new FS.HISFC.BizLogic.Manager.Constant();

        private static FS.HISFC.BizLogic.Manager.Person personMgr = new FS.HISFC.BizLogic.Manager.Person();

        private static FS.HISFC.BizLogic.Pharmacy.Item myPha = new FS.HISFC.BizLogic.Pharmacy.Item();

        private static FS.HISFC.BizLogic.Pharmacy.Constant myPhaCons = new FS.HISFC.BizLogic.Pharmacy.Constant();

        private static ArrayList alDrugGrade = null;

        private static ArrayList alDrugPosition = null;

        private static ArrayList alSpeDrugs = null;

        private static List<FS.HISFC.Models.Order.Medical.Popedom> alAllPopom = null;

        /// <summary>
        /// 是否控制处方权
        /// </summary>
        private static int isUseControl = -1;

        /// <summary>
        /// 药品基本信息
        /// </summary>
        private static FS.HISFC.Models.Pharmacy.Item phaItem = null;

        /// <summary>
        /// 药品列表
        /// </summary>
        private static Hashtable hsPhaItem = new Hashtable();

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
        [Obsolete("作废，移到FS.SOC.HISFC.BizProcess.OrderIntegrate.OrderBase里面", true)]
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
                    MessageBox.Show("获取医生信息错误！ 工号【" + doctor.ID + "】", "错误", MessageBoxButtons.OK);
                    return -1;
                }

                #region 获取对照
                if (alDrugGrade == null)
                {
                    alDrugGrade = myConstant.GetAllList("SpeDrugGrade");

                    if (alDrugGrade == null)
                    {
                        error = myConstant.Err;
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
                    alDrugPosition = myConstant.GetAllList("SpeDrugPosition");
                    if (alDrugPosition == null)
                    {
                        error = myConstant.Err;
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

                    if (doctObj.Duty.ID.Trim() == level.Trim() && (alDrugPosition[i] as FS.HISFC.Models.Base.Const).IsValid)
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

                if ((alDrugGrade != null && alDrugGrade.Count > 0) || alDrugPosition != null && alDrugPosition.Count > 0)
                {
                    isControlDrugPermission = true;
                }

                if (isControlDrugPermission)
                {
                    //抗生素：药品 
                    if (order.Item.ItemType == EnumItemType.Drug)
                    {
                        phaItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID);

                        if (phaItem == null)
                        {
                            error = "获取药品信息失败：" + myPha.Err;
                            return -1;
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
                                        error = "您的职称(职级)没有开立此药品【" + order.Item.Name + "】的权限！\r\n系统默认该医嘱为无效状态，需上级医生审核后才能生效！";
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
                if (false)
                {
                    FS.HISFC.Models.Pharmacy.DrugSpecial specialDrug = myPha.GetSpeDrugMaintenanceDrugByCode(order.Item.ID);

                    if (specialDrug != null && specialDrug.Item.ID.Length > 0)
                    {
                        if (alSpeDrugs == null)
                        {
                            alSpeDrugs = myPhaCons.QueryAllSpeDrugPerAndDep();
                        }

                        bool isHavePriv = false;

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
                }
                #endregion

                #endregion
            }
            return rev;
        }

        #endregion

        /// <summary>
        /// 图标通知
        /// </summary>
        static System.Windows.Forms.NotifyIcon notify = null;

        /// <summary>
        /// 图标通知事件
        /// </summary>
        /// <param name="timeout"></param>
        /// <param name="tipTitle"></param>
        /// <param name="tipText"></param>
        /// <param name="tipIcon"></param>
        public static void ShowBalloonTip(int timeout, string tipTitle, string tipText, ToolTipIcon tipIcon)
        {
            if (notify == null)
            {
                notify = new System.Windows.Forms.NotifyIcon();
                notify.Icon = Properties.Resources.HIS;
            }
            notify.Visible = true;
            notify.ShowBalloonTip(timeout, tipTitle, tipText, tipIcon);
        }

        /*
         * 
        
        /// <summary>
        /// 修改医嘱（处方）接口
        /// </summary>
        private static FS.SOC.HISFC.BizProcess.OrderInterface.Common.IModifyOrder IModifyOrder = null;

        /// <summary>
        /// 是否查询修改医嘱接口
        /// </summary>
        private static bool isIModifyOrder = false;

        /// <summary>
        /// 修改医嘱接口实现
        /// </summary>
        /// <param name="outOrder"></param>
        /// <param name="changedField"></param>
        /// <returns></returns>
        public static int ModifyOrder(FS.HISFC.Models.Order.OutPatient.Order outOrder, string changedField)
        {
            if (IModifyOrder == null && !isIModifyOrder)
            {
                IModifyOrder = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.Components.Order.OutPatient.Controls.ucOutPatientOrder), typeof(FS.SOC.HISFC.BizProcess.OrderInterface.Common.IModifyOrder)) as FS.SOC.HISFC.BizProcess.OrderInterface.Common.IModifyOrder;
                isIModifyOrder = true;
            }

            if (IModifyOrder != null)
            {
                if (IModifyOrder.ModifyOutOrder(outOrder, changedField) <= 0)
                {
                    if (!string.IsNullOrEmpty(IModifyOrder.ErrInfo))
                    {
                        MessageBox.Show(IModifyOrder.ErrInfo, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    return -1;
                }
            }
            return 1;
        }

        /// <summary>
        /// 修改医嘱接口实现
        /// </summary>
        /// <param name="outOrder"></param>
        /// <param name="changedField"></param>
        /// <returns></returns>
        public static int ModifyOrder(FS.HISFC.Models.Order.Inpatient.Order inOrder, string changedField)
        {
            if (IModifyOrder == null && !isIModifyOrder)
            {
                IModifyOrder = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.SOC.HISFC.Components.InPateintOrder.ucOrder), typeof(FS.SOC.HISFC.BizProcess.OrderInterface.Common.IModifyOrder)) as FS.SOC.HISFC.BizProcess.OrderInterface.Common.IModifyOrder;
                isIModifyOrder = true;
            }

            if (IModifyOrder != null)
            {
                if (IModifyOrder.ModifyInOrder(inOrder, changedField) <= 0)
                {
                    if (!string.IsNullOrEmpty(IModifyOrder.ErrInfo))
                    {
                        MessageBox.Show(IModifyOrder.ErrInfo, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    return -1;
                }
            }
            return 1;
        }
         * */
    }

    /// <summary>
    /// 医嘱查询后，打印领药单接口liu.xq20071025
    /// </summary>
    public interface IOrderExeQuery
    {
        /// <summary>
        /// 住院患者实体
        /// </summary>
        FS.HISFC.Models.RADT.PatientInfo patientInfoObj
        {
            set;
            get;
        }
        /// <summary>
        /// 赋值函数
        /// </summary>
        /// <returns></returns>
        int SetValue(ArrayList alExeOrder);

        /// <summary>
        /// 打印
        /// </summary>
        void Print();
    }

    /// <summary>
    /// 日志记录
    /// </summary>
    public class LogManager
    {
        public static void Write(string logInfo)
        {
            //检查目录是否存在
            if (System.IO.Directory.Exists("./Log/Order/InOrder") == false)
            {
                System.IO.Directory.CreateDirectory("./Log/Order/InOrder");
            }
            //保存一周的日志
            System.IO.File.Delete("./Log/Order/InOrder/" + DateTime.Now.AddDays(-7).ToString("yyyyMMdd") + ".LOG");
            string name = DateTime.Now.ToString("yyyyMMdd");
            System.IO.StreamWriter w = new System.IO.StreamWriter("./Log/Order/InOrder" + name + ".LOG", true);
            w.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + logInfo);
            w.Flush();
            w.Close();
        }
    }
}