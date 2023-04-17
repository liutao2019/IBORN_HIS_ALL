using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using FS.HISFC.Models.Base;
using System.Runtime.InteropServices;

namespace FS.HISFC.Components.Order.Classes
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
        /// <summary>
        /// 在xml中取医院logo赋予picturebox
        /// </summary>
        /// <param name="xmlpath">xml路径（绝对）  PS：从根目录开始</param>
        /// <param name="root">xml根节点</param>
        /// <param name="secondNode">要查找的目标节点</param>
        /// <param name="erro">错误信息</param>
        public static string GetHospitalLogo(string xmlpath, string root, string secondNode, string erro)
        {
            xmlpath = FS.FrameWork.WinForms.Classes.Function.CurrentPath + xmlpath;
            return FS.SOC.Public.XML.SettingFile.ReadSetting(xmlpath, root, secondNode, erro);
        }

        #region 默认执行科室列表

        private static Dictionary<string, FS.SOC.HISFC.Fee.Models.Undrug> dicUndrugExec = new Dictionary<string, FS.SOC.HISFC.Fee.Models.Undrug>();

        private static FS.SOC.HISFC.Fee.Models.Undrug GetUndrugExecInfo(string itemCode)
        {
            FS.SOC.HISFC.Fee.Models.Undrug item = null;
            if (dicUndrugExec.ContainsKey(itemCode))
            {
                item = dicUndrugExec[itemCode];
            }
            else
            {
                FS.SOC.HISFC.Fee.BizLogic.Undrug undrugMgr = new FS.SOC.HISFC.Fee.BizLogic.Undrug();
                item = undrugMgr.GetExecInfo(itemCode);

                dicUndrugExec.Add(itemCode, item);
            }

            return item;
        }

        public static string GetExecDept(bool isOut, string reciptDept, string execDept, string itemCode)
        {
            string defaultExecDept = "";
            ArrayList alExecDept = null;

            SetExecDept(isOut, reciptDept, itemCode, execDept, ref defaultExecDept, ref alExecDept);

            return defaultExecDept;
        }

        /// <summary>
        /// 获取执行科室信息
        /// </summary>
        /// <param name="reciptDept">开立科室</param>
        /// <param name="itemCode">项目编码</param>
        /// <param name="execDept">原有执行科室</param>
        /// <param name="defaultExecDept">返回的默认执行科室</param>
        /// <param name="alExecDept">执行科室列表</param>
        /// <returns></returns>
        public static int SetExecDept(bool isOut, string reciptDept, string itemCode, string execDept, ref string defaultExecDept, ref ArrayList alExecDept)
        {
            FS.SOC.HISFC.Fee.Models.Undrug item = GetUndrugExecInfo(itemCode);

            string thisExecDept = string.IsNullOrEmpty(execDept) ? reciptDept : execDept;

            if (item == null)
            {
                alExecDept = null;
                defaultExecDept = thisExecDept;
            }
            else
            {
                if (string.IsNullOrEmpty(item.ExecDept)
                    || item.ExecDept == "ALL"
                    || item.ExecDept == "ALL|")
                {
                    alExecDept = null;

                    if (!string.IsNullOrEmpty(execDept))
                    {
                        defaultExecDept = execDept;
                    }
                    else
                    {
                        defaultExecDept = isOut ? item.DefaultExecDeptForOut : item.DefaultExecDeptForIn;
                    }
                }
                else
                {
                    string[] depts = item.ExecDept.Split('|');
                    alExecDept = new ArrayList();

                    //{DD1FC594-2D6D-41c0-B318-A1AD080F72AA}
                    FS.HISFC.Models.Base.Employee curOper = (FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator;
                    FS.HISFC.Models.Base.Department curDept = curOper.Dept as FS.HISFC.Models.Base.Department;

                    string firstDept = "";
                    //{DD1FC594-2D6D-41c0-B318-A1AD080F72AA}
                    string firstSamePartDept = "";

                    for (int i = 0; i < depts.Length; i++)
                    {
                        FS.HISFC.Models.Base.Department deptObj = SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(depts[i]);
                        if (deptObj != null)
                        {
                            alExecDept.Add(deptObj);

                            if (string.IsNullOrEmpty(firstDept))
                            {
                                firstDept = deptObj.ID;
                            }

                            //{DD1FC594-2D6D-41c0-B318-A1AD080F72AA}
                            //第一个当前院区的执行科室
                            if (string.IsNullOrEmpty(firstSamePartDept) && (curDept.HospitalID == deptObj.HospitalID))
                            {
                                firstSamePartDept = deptObj.ID;
                            }
                        }
                    }

                    //{DD1FC594-2D6D-41c0-B318-A1AD080F72AA}
                    if (!string.IsNullOrEmpty(firstSamePartDept))
                    {
                        firstDept = firstSamePartDept;
                    }

                    if (item.ExecDept.Contains(thisExecDept))
                    {
                        defaultExecDept = thisExecDept;
                    }
                    else
                    {
                        defaultExecDept = isOut ? item.DefaultExecDeptForOut : item.DefaultExecDeptForIn;
                    }

                    if (string.IsNullOrEmpty(defaultExecDept)
                        && !string.IsNullOrEmpty(firstDept))
                    {
                        defaultExecDept = firstDept;
                    }
                }

                if (string.IsNullOrEmpty(defaultExecDept))
                {
                    defaultExecDept = thisExecDept;
                }
            }

            return 1;
        }


        /// <summary>
        /// 默认执行科室接口
        /// </summary>
        private static FS.HISFC.BizProcess.Interface.Fee.IExecDept IExecDept = null;

        /// <summary>
        /// 是否已查询默认执行科室接口
        /// </summary>
        private static int IGetExecDept = 0;

        /// <summary>
        /// 获取默认执行科室列表
        /// </summary>
        /// <param name="recipeDept"></param>
        /// <param name="item"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        [Obsolete("作废，用SetExecDept代替", true)]
        public static ArrayList GetExecDepts(FS.FrameWork.Models.NeuObject recipeDept, FS.HISFC.Models.Order.Order order)
        {
            if (IExecDept == null && IGetExecDept == 0)
            {
                IExecDept = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.Components.Order.Classes.Function), typeof(FS.HISFC.BizProcess.Interface.Fee.IExecDept)) as FS.HISFC.BizProcess.Interface.Fee.IExecDept;
                IGetExecDept = 1;
            }

            ArrayList al = null;
            if (IExecDept != null)
            {
                string errInfo = "";
                al = IExecDept.GetExecDept(recipeDept, (FS.HISFC.Models.Fee.Item.Undrug)order.Item, ref errInfo);
                if (al != null)
                {
                    return al;
                }
                Components.Order.Classes.Function.ShowBalloonTip(2, "获取默认执行科室错误", errInfo, ToolTipIcon.Error);
            }
            al = SOC.HISFC.BizProcess.Cache.Common.GetValidDept();
            return al;
        }

        /// <summary>
        /// 获取默认执行科室编码
        /// </summary>
        /// <param name="recipeDept"></param>
        /// <param name="item"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        [Obsolete("作废，用SetExecDept代替", true)]
        public static string GetExecDept(FS.FrameWork.Models.NeuObject recipeDept, FS.HISFC.Models.Order.Order order, string execDeptCode, bool isNew)
        {
            if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                return recipeDept.ID;
            }

            if (isNew || string.IsNullOrEmpty(execDeptCode))
            {
                if (order.Item.ID == "999")
                {
                    execDeptCode = "";
                }
                else
                {
                    execDeptCode = SOC.HISFC.BizProcess.Cache.Fee.GetItem(order.Item.ID).ExecDept;
                }
            }

            if (IExecDept == null && IGetExecDept == 0)
            {
                IExecDept = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.Components.Order.Classes.Function), typeof(FS.HISFC.BizProcess.Interface.Fee.IExecDept)) as FS.HISFC.BizProcess.Interface.Fee.IExecDept;
                IGetExecDept = 1;
            }

            ArrayList al = null;
            if (IExecDept != null)
            {
                string errInfo = "";
                al = IExecDept.GetExecDept(recipeDept, (FS.HISFC.Models.Fee.Item.Undrug)order.Item, ref errInfo);
                if (al != null)
                {
                }
                else
                {
                    Components.Order.Classes.Function.ShowBalloonTip(2, "获取默认执行科室错误", errInfo, ToolTipIcon.Error);
                    al = SOC.HISFC.BizProcess.Cache.Common.GetDept();
                }
            }
            else
            {
                al = SOC.HISFC.BizProcess.Cache.Common.GetDept();
            }

            string[] execDept = execDeptCode.Split('|');
            try
            {
                for (int k = 0; k < execDept.Length; k++)
                {
                    if (!string.IsNullOrEmpty(execDept[k]))
                    {
                        execDeptCode = execDept[k];

                        if (SOC.HISFC.BizProcess.Cache.Common.GetDept(recipeDept.ID).DeptType.ID.ToString() ==
                            SOC.HISFC.BizProcess.Cache.Common.GetDept(execDept[k]).DeptType.ID.ToString())
                        {
                            execDeptCode = execDept[k];
                            break;
                        }
                    }
                }
            }
            catch
            {
                execDeptCode = recipeDept.ID;
            }

            bool isRecipt = false;

            foreach (FS.FrameWork.Models.NeuObject obj in al)
            {
                if (obj.ID == execDeptCode)
                {
                    return obj.ID;
                    isRecipt = false;
                    break;
                }
                if (obj.ID == recipeDept.ID)
                {
                    isRecipt = true;
                }
            }
            if (isRecipt)
            {
                return recipeDept.ID;
            }
            else
            {
                if (al.Count > 0)
                {
                    for (int i = 0; i < al.Count; i++)
                    {
                        if (SOC.HISFC.BizProcess.Cache.Common.GetDept(recipeDept.ID).DeptType.ID.ToString() ==
                            SOC.HISFC.BizProcess.Cache.Common.GetDept(((FS.FrameWork.Models.NeuObject)al[i]).ID).DeptType.ID.ToString())
                        {
                            return ((FS.FrameWork.Models.NeuObject)al[i]).ID;
                            break;
                        }
                    }
                    return ((FS.FrameWork.Models.NeuObject)al[0]).ID;
                }
                return "";
            }
        }

        #endregion

        #region 医嘱管理
        /// <summary>
        /// 设置医嘱首次频次信息
        /// </summary>
        /// <param name="order"></param>
        public static void SetDefaultOrderFrequency(FS.HISFC.Models.Order.Inpatient.Order order)
        {
            if (order.OrderType.IsDecompose
                || order.OrderType.ID == "CD"
                || order.OrderType.ID == "QL")//默认为项目的频次
            {
                if (order.Item.GetType() == typeof(FS.HISFC.Models.Pharmacy.Item)
                    && SOC.HISFC.BizProcess.Cache.Order.GetFrequency((order.Item as FS.HISFC.Models.Pharmacy.Item).Frequency.ID) != null
                    && !string.IsNullOrEmpty(SOC.HISFC.BizProcess.Cache.Order.GetFrequency((order.Item as FS.HISFC.Models.Pharmacy.Item).Frequency.ID).ID))
                {
                    order.Frequency = (order.Item as FS.HISFC.Models.Pharmacy.Item).Frequency.Clone();
                    order.Frequency.Time = "25:00";//默认为２５点，需要更新
                }
            }
            //else if (order.Item.IsPharmacy && order.OrderType.IsDecompose == false)//药品 临时医嘱，频次为空，默认为需要时候服用prn
            else if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug
                && order.OrderType.IsDecompose == false)//药品 临时医嘱，频次为空，默认为需要时候服用prn
            {
                FS.HISFC.Models.Pharmacy.Item item = new FS.HISFC.Models.Pharmacy.Item();
                item = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID);// {4D67D981-6763-4ced-814E-430B518304E2}
                order.Frequency.ID = item.Frequency.ID;
                if (!string.IsNullOrEmpty(item.OnceDoseUnit))// {4D67D981-6763-4ced-814E-430B518304E2}
                {
                    order.DoseUnit = item.OnceDoseUnit;
                }
                if (string.IsNullOrEmpty(order.Frequency.ID))
                {
                    order.Frequency.ID = GetDefaultFrequencyID();//药品临时医嘱默认为需要时执行
                }
            }
            //else if (order.Item.IsPharmacy == false && order.OrderType.IsDecompose == false)
            else if (order.Item.ItemType != EnumItemType.Drug
                && order.OrderType.IsDecompose == false)
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
            if (SOC.HISFC.BizProcess.Cache.Order.QueryFrequency() != null
                && SOC.HISFC.BizProcess.Cache.Order.QueryFrequency().Count > 0)
            {
                return SOC.HISFC.BizProcess.Cache.Order.QueryFrequency()[0] as FS.HISFC.Models.Order.Frequency;
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
                freqHelper.ArrayObject = SOC.HISFC.BizProcess.Cache.Order.QueryFrequency();
            }

            return freqHelper;
        }

        #endregion

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
                //是否控制处方权
                bool isUseControl = CacheManager.ContrlManager.GetControlParam<bool>("HNMET1", true, false);

                if (isUseControl)
                {
                    //怎么根据药品等级来判断处方权了？？？
                    #region 获取常数
                    if (alDrugGrade == null)
                    {
                        alDrugGrade = CacheManager.GetConList("SpeDrugGrade");
                    }

                    if (alDrugPosition == null)
                    {
                        alDrugPosition = CacheManager.GetConList("SpeDrugPosition");
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
                        ArrayList alLevl = CacheManager.GetConList(FS.HISFC.Models.Base.EnumConstant.LEVEL);
                        if (alLevl == null)
                        {
                            errInfo = CacheManager.InterMgr.Err;
                            return -1;
                        }
                        levlHelper.ArrayObject = alLevl;
                    }

                    emplObj = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeInfo(emplCode);
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
        /// 校验警戒线方式：0 按照现有金额判断；1 按照收费后金额参与判断
        /// </summary>
        private static int checkMoneyAlertMode = -1;

        /// <summary>
        /// 判断警戒线
        /// </summary>
        /// <param name="paitentInfo"></param>
        /// <param name="messType"></param>
        /// <returns></returns>
        public static int CheckMoneyAlertForAdd(FS.HISFC.Models.RADT.PatientInfo patientInfo, MessType messType)
        {

            return 1;
        }

        /// <summary>
        /// 判断警戒线
        /// </summary>
        /// <param name="paitentInfo"></param>
        /// <param name="alOrders"></param>
        /// <param name="messType"></param>
        /// <returns></returns>
        public static int CheckMoneyAlert(FS.HISFC.Models.RADT.PatientInfo patientInfo, ArrayList alOrders, MessType messType)
        {
            //实时获取最新的警戒线、余额等信息
            FS.HISFC.Models.RADT.PatientInfo pInfo = CacheManager.InPatientMgr.QueryPatientInfoByInpatientNO(patientInfo.ID);
            if (pInfo != null)
            {
                patientInfo.FT = pInfo.FT;
                patientInfo.PVisit.MoneyAlert = pInfo.PVisit.MoneyAlert;
                patientInfo.PVisit.AlertType = pInfo.PVisit.AlertType;
                patientInfo.PVisit.AlertFlag = pInfo.PVisit.AlertFlag;
            }

            if (!patientInfo.PVisit.AlertFlag)
            {
                return 1;
            }
            if (alOrders.Count == 0)
            {
                return 1;
            }

            try
            {
                decimal totCost = 0;
                Hashtable hsCombNo = new Hashtable();

                ArrayList alFee = new ArrayList();

                if (alOrders[0].GetType() == typeof(FS.HISFC.Models.Order.ExecOrder))
                {
                    foreach (FS.HISFC.Models.Order.ExecOrder inOrder in alOrders)
                    {
                        if (inOrder.Order.ID != "999" && inOrder.Order.OrderType.IsCharge)
                        {
                            FS.HISFC.Models.Fee.Inpatient.FeeItemList itemList = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
                            itemList.Item.ID = inOrder.Order.Item.ID;
                            itemList.Item.Name = inOrder.Order.Item.Name;
                            itemList.Item.Qty = inOrder.Order.Qty;
                            itemList.Patient = patientInfo;
                            if (inOrder.Order.Item.ItemType == EnumItemType.Drug)
                            {
                                itemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber((inOrder.Order.Item.Price * inOrder.Order.Item.Qty / inOrder.Order.Item.PackQty), 2);
                            }
                            else
                            {
                                itemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber((inOrder.Order.Item.Price * inOrder.Order.Item.Qty), 2);
                            }
                            itemList.FT.OwnCost = itemList.FT.TotCost;

                            alFee.Add(itemList);
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
                        if (inOrder.ID != "999" && inOrder.OrderType.IsCharge)
                        {
                            FS.HISFC.Models.Fee.Inpatient.FeeItemList itemList = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
                            itemList.Item.ID = inOrder.Item.ID;
                            itemList.Item.Name = inOrder.Item.Name;
                            itemList.Item.Qty = inOrder.Qty;
                            itemList.Patient = patientInfo;

                            if (inOrder.Item.ItemType == EnumItemType.Drug)
                            {
                                itemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber((inOrder.Item.Price * inOrder.Item.Qty / inOrder.Item.PackQty), 2);
                            }
                            else
                            {
                                itemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber((inOrder.Item.Price * inOrder.Item.Qty), 2);
                            }
                            itemList.FT.OwnCost = itemList.FT.TotCost;

                            alFee.Add(itemList);
                        }
                        if (!hsCombNo.Contains(inOrder.Combo.ID))
                        {
                            ArrayList alCombOrder = CacheManager.InOrderMgr.QueryOrderByCombNO(inOrder.Combo.ID, true);
                            foreach (FS.HISFC.Models.Order.Inpatient.Order subOrder in alCombOrder)
                            {
                                FS.HISFC.Models.Fee.Inpatient.FeeItemList subItemList = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
                                subItemList.Item.ID = subOrder.Item.ID;
                                subItemList.Item.Name = subOrder.Item.Name;
                                subItemList.Item.Qty = subOrder.Qty;
                                subItemList.Patient = patientInfo;

                                if (subOrder.Item.ItemType == EnumItemType.Drug)
                                {

                                    subItemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber((subOrder.Item.Price * subOrder.Item.Qty / subOrder.Item.PackQty), 2);
                                    subItemList.FT.OwnCost = subItemList.FT.TotCost;
                                }
                                else
                                {
                                    subItemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber((subOrder.Item.Price * subOrder.Item.Qty), 2);
                                    subItemList.FT.OwnCost = subItemList.FT.TotCost;
                                }

                                alFee.Add(subItemList);
                            }
                            hsCombNo.Add(inOrder.Combo.ID, null);
                        }
                    }
                }
                FS.HISFC.Models.Base.FT ft = CacheManager.FeeIntegrate.ComputeInpatientFee(patientInfo, alFee);
                if (ft == null)
                {
                    ShowBalloonTip(2, "错误提示", CacheManager.FeeIntegrate.Err, ToolTipIcon.Warning);
                    return 1;
                }

                totCost = ft.OwnCost;

                if (totCost == 0)
                {
                    return 1;
                }
                return CheckMoneyAlert(patientInfo, totCost, messType);
            }
            catch (Exception ex)
            {
                MessageBox.Show("CheckMoneyAlert" + ex.Message);
            }
            return 1;
        }

        private static int CheckMoneyAlert(FS.HISFC.Models.RADT.PatientInfo patientInfo, decimal totCost, MessType messType)
        {
            if (checkMoneyAlertMode == -1)
            {
                checkMoneyAlertMode = CacheManager.ContrlManager.GetControlParam<int>("HNMET4", true, 1);
            }

            decimal moneyAlert = patientInfo.PVisit.MoneyAlert;


            //方式0，按现有余额和警戒线判断
            if (checkMoneyAlertMode == 0)
            {
                if (CacheManager.FeeIntegrate.IsPatientLackFee(patientInfo, 0))
                {
                    if (messType == MessType.Y)
                    {
                        MessageBox.Show("患者【" + patientInfo.Name + "】已经欠费，不能继续操作！\r\n\r\n\r\n" + "现有余额： " + patientInfo.FT.LeftCost.ToString() + " 小于规定的" + moneyAlert.ToString() + "元\r\n\r\n本次收费自费金额： " + totCost.ToString() + "\r\n\r\n", "警告", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Stop);
                        return -1;
                    }
                    else if (messType == MessType.M)
                    {
                        if (MessageBox.Show("患者【" + patientInfo.Name + "】已经欠费，是否继续操作？\r\n\r\n\r\n" + "现有余额： " + patientInfo.FT.LeftCost.ToString() + " 小于规定的" + moneyAlert.ToString() + "元\r\n\r\n本次收费自费金额： " + totCost.ToString() + "\r\n\r\n", "询问", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
            //1 按照收费后金额参与判断
            else if (checkMoneyAlertMode == 1)
            {
                if (CacheManager.FeeIntegrate.IsPatientLackFee(patientInfo, totCost))
                {
                    if (messType == MessType.Y)
                    {
                        MessageBox.Show("患者【" + patientInfo.Name + "】已经欠费，不能继续操作！\r\n\r\n\r\n自费金额： " + totCost.ToString() + "\r\n\r\n警戒线： " + patientInfo.PVisit.MoneyAlert.ToString() + "\r\n\r\n余额： " + patientInfo.FT.LeftCost.ToString() + "\r\n\r\n收费后余额： " + (patientInfo.FT.LeftCost - totCost).ToString() + " 小于规定的50元\r\n\r\n", "警告", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Stop);
                        return -1;
                    }
                    else if (messType == MessType.M)
                    {
                        if (MessageBox.Show("患者【" + patientInfo.Name + "】已经欠费，是否继续操作？\r\n\r\n\r\n自费金额： " + totCost.ToString() + "\r\n\r\n警戒线： " + patientInfo.PVisit.MoneyAlert.ToString() + "\r\n\r\n余额： " + patientInfo.FT.LeftCost.ToString() + "\r\n\r\n收费后余额： " + (patientInfo.FT.LeftCost - totCost).ToString() + " 小于规定的50元\r\n\r\n", "询问", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return -1;
                        }
                    }
                    else
                    {

                    }
                }
            }


            return 1;
        }

        #endregion

        #region 常用常数
        private static FS.FrameWork.Public.ObjectHelper helpUsage = null;

        /// <summary>
        /// 用法
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper HelperUsage
        {
            get
            {
                if (helpUsage == null)
                    helpUsage = new FS.FrameWork.Public.ObjectHelper(CacheManager.GetConList(FS.HISFC.Models.Base.EnumConstant.USAGE));
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
                HelperUsage.ArrayObject = CacheManager.GetConList(FS.HISFC.Models.Base.EnumConstant.USAGE);
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

        //private static FS.FrameWork.Public.ObjectHelper helpFrequency = null;

        /// <summary>
        /// 频次
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper HelperFrequency
        {
            get
            {
                return SOC.HISFC.BizProcess.Cache.Order.FrequencyHelper;
                //if (helpFrequency == null)
                //    helpFrequency = new FS.FrameWork.Public.ObjectHelper(CacheManager.InterMgr.QuereyFrequencyList());
                //return helpFrequency;
            }
            //set
            //{
            //    helpFrequency = value;
            //}
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
                    helpSample = new FS.FrameWork.Public.ObjectHelper(CacheManager.GetConList(FS.HISFC.Models.Base.EnumConstant.LABSAMPLE));
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
                    helpCheckPart = new FS.FrameWork.Public.ObjectHelper(CacheManager.GetConList("CHECKPART"));
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
                defaultMoDateMode = CacheManager.ContrlManager.GetControlParam<string>("HNMET3", false, "000");
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
        /// 默认首日量显示 0 固定为1；1 根据开立时间自动计算；2 最大频次数；3 固定为0;4 默认为空
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
                firstOrderDaysMode = CacheManager.ContrlManager.GetControlParam<int>("HNZY34", true, 0);
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

                if (SOC.HISFC.BizProcess.Cache.Order.GetFrequencyCount(inOrder.Frequency.ID) <= 1)
                {
                    if (count == 0)
                    {
                        count = 1;
                    }
                }
            }
            else if (firstOrderDaysMode == 2)
            {
                count = inOrder.Frequency.Times.Length;
            }
            else if (firstOrderDaysMode == 3)
            {
                count = 0;
            }
            else
            {
                count = -1;
            }

            //中五要求所有首日量的默认为0

            //if (inOrder.Frequency.ID.ToUpper().Replace(".", "") == "Q1H"
            //    || inOrder.Frequency.ID.ToUpper().Replace(".", "") == "QH")
            //{
            //    if (inOrder.BeginTime == DateTime.MinValue)
            //    {
            //        inOrder.BeginTime = dtNow;
            //    }
            //    count = 0;

            //    for (int i = 0; i < inOrder.Frequency.Times.Length; i++)
            //    {
            //        DateTime dt = FS.FrameWork.Function.NConvert.ToDateTime(inOrder.BeginTime.ToString("yyyy-MM-dd") + " " + inOrder.Frequency.Times[i]);
            //        if (dt >= inOrder.BeginTime)
            //        {
            //            count += 1;
            //        }
            //    }
            //}

            //if (count == 0)
            //{
            //    count = 1;
            //}
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
            //FS.HISFC.Manager.Item CacheManager.InterMgr = new FS.HISFC.BizLogic.Pharmacy.Item();
            //FS.HISFC.Models.Pharmacy.item item = null;
            //.

            FS.HISFC.Models.Pharmacy.Storage phaItem = null;


            switch (iCheck)
            {
                case 0:
                    //houwb 2011-5-30 增加发送类型判断
                    //phaItem = CacheManager.PhaIntegrate.GetItemForInpatient(deptCode, itemID);
                    phaItem = CacheManager.PhaIntegrate.GetItemStorage(deptCode, sendType, itemID);

                    if (phaItem == null) return true;
                    if (qty > FS.FrameWork.Function.NConvert.ToDecimal(phaItem.StoreQty))
                    {
                        return false;
                    }
                    break;
                case 1:
                    //houwb 2011-5-30 增加发送类型判断
                    //phaItem = CacheManager.PhaIntegrate.GetItemForInpatient(deptCode, itemID);
                    phaItem = CacheManager.PhaIntegrate.GetItemStorage(deptCode, sendType, itemID);


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
            obj.DateProperty = m.GetDateTimeFromSysDateTime();
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
            string memo = "";

            //判断下 转科的情况，
            string dept = "";

            if (p != null)
            {
                memo = p.Memo;
                //判断下 转科的情况，
                dept = p.PVisit.PatientLocation.Dept.ID;
            }

            p = CacheManager.RadtIntegrate.GetPatientInfomation(patientID);

            if (p == null)
            {
                errInfo = "获得患者基本信息错误" + CacheManager.RadtIntegrate.Err;
                return -1;
            }

            p.Memo = memo;
            if (p.PVisit.InState.ID.ToString() == "O" || //出院结算
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
                if (info.Memo == "会诊")
                {
                    speDrugType = "CONS";
                }
                else if (info.Memo == "科室")
                {
                    speDrugType = "DEPT" + order.ExeDept.ID;
                }
                else if (info.Memo == "医技")
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
                    isUseControl = CacheManager.ContrlManager.GetControlParam<Int32>("HNMET1", true, 0);
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
                    alDrugGrade = CacheManager.GetConList("SpeDrugGrade");

                    if (alDrugGrade == null)
                    {
                        error = CacheManager.ConManager.Err;
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
                    alDrugPosition = CacheManager.GetConList("SpeDrugPosition");
                    if (alDrugPosition == null)
                    {
                        error = CacheManager.ConManager.Err;
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

        #region 过敏史判断

        private static FS.HISFC.BizLogic.Order.Medical.AllergyManager allergyManager = new FS.HISFC.BizLogic.Order.Medical.AllergyManager();

        public static Hashtable HsAllItems = new Hashtable();


        /// <summary>
        /// 过敏史判断
        /// </summary>
        /// <param name="patientType 1门诊 2住院"></param>
        /// <param name="reg"></param>
        /// <param name="order"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static int JudgePatientAllergy(string patientType, FS.HISFC.Models.RADT.PID reg,
            FS.HISFC.Models.Order.Order order, ref string error)
        {
            int ret = 1;

            if (order.Item.ID == "999")
                return 1;

            if (order.Item.ItemType != EnumItemType.Drug)
                return 1;

            string patientID = "";

            if (patientType == "1")
            {
                patientID = reg.CardNO;
            }
            else
            {
                patientID = reg.PatientNO;
            }

            ArrayList al = new ArrayList();

            al = allergyManager.QueryValidAllergyInfo(patientID, patientType);
            if (al == null)
            {
                MessageBox.Show("获取皮试信息失败：" + allergyManager.Err);
                return 1;
            }

            FS.HISFC.Models.Pharmacy.Item newItem = null;

            FS.HISFC.Models.Pharmacy.Item algItem = null;

            foreach (FS.HISFC.Models.Order.Medical.AllergyInfo info in al)
            {
                if (info.Allergen != null && !string.IsNullOrEmpty(info.Allergen.ID))
                {
                    if (info.Allergen.ID.Substring(0, 1) != "Y")
                    {
                        continue;
                    }

                    if (HsAllItems.Contains(order.Item.ID))
                    {
                        newItem = HsAllItems[order.Item.ID] as FS.HISFC.Models.Pharmacy.Item;
                    }
                    else
                    {
                        newItem = CacheManager.PhaIntegrate.GetItem(order.Item.ID);
                        if (newItem == null)
                        {
                            MessageBox.Show("获取药品信息失败：" + CacheManager.PhaIntegrate.Err);
                        }
                        HsAllItems.Add(newItem.ID, newItem);
                    }

                    if (HsAllItems.Contains(info.Allergen.ID))
                    {
                        algItem = HsAllItems[info.Allergen.ID] as FS.HISFC.Models.Pharmacy.Item;
                    }
                    else
                    {
                        algItem = CacheManager.PhaIntegrate.GetItem(info.Allergen.ID);
                        if (algItem == null)
                        {
                            MessageBox.Show("获取药品信息失败：" + CacheManager.PhaIntegrate.Err);
                        }
                        HsAllItems.Add(algItem.ID, algItem);
                    }

                    if ((newItem != null && algItem != null
                        && newItem.PhyFunction3.ID == algItem.PhyFunction3.ID)
                       || order.Item.ID == info.Allergen.ID)
                    {
                        if (MessageBox.Show("新开立药物【" + order.Item.Name + "】与患者历史过敏药物【" + algItem.Name + "】相同类别，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        {
                            return -1;
                        }
                    }
                }
            }

            return ret;
        }

        #endregion

        /// <summary>
        /// 判断组合约束条件
        /// </summary>
        /// <param name="orderFrom"></param>
        /// <param name="orderTo"></param>
        /// <param name="isNew">是不是新医嘱？ 新医嘱部分信息可以不判断</param>
        /// <returns></returns>
        public static int ValidComboOrder(FS.HISFC.Models.Order.Inpatient.Order orderFrom, FS.HISFC.Models.Order.Inpatient.Order orderTo, bool isNew)
        {
            /*
             * 
             * */
            if (orderFrom.IsSubtbl || orderTo.IsSubtbl)
            {

                return 1;
            }

            if (orderTo.PageNo >= 0)
            {
                MessageBox.Show(orderTo.Item.Name + "医嘱已经打印，不可以组合用！");
                return -1;

            }

            if (orderTo.Status != 0 && orderTo.Status != 5)
            {
                MessageBox.Show(orderTo.Item.Name + "不是新开立医嘱，不可以组合用！");
                return -1;

            }


            bool isDecSysClassWhenGetRecipeNO = FS.FrameWork.Function.NConvert.ToBoolean(FS.HISFC.Components.Order.OutPatient.Classes.Function.GetBatchControlParam("MZ0073", false, "0"));

            if (isDecSysClassWhenGetRecipeNO)
            {
                if ("PCZ,P".Contains(orderFrom.Item.SysClass.ID.ToString()) &&
                    "PCZ,P".Contains(orderTo.Item.SysClass.ID.ToString()))
                {
                    //西药和成药允许组合
                }
                else
                {
                    if (orderFrom.Item.SysClass.ID.ToString() != orderTo.Item.SysClass.ID.ToString())
                    {
                        MessageBox.Show("系统类别不同，不可以组合用！");
                        return -1;
                    }
                }
            }
            else
            {
                if (orderFrom.Item.SysClass.ID.ToString() != orderTo.Item.SysClass.ID.ToString())
                {
                    MessageBox.Show("系统类别不同，不可以组合用！");
                    return -1;
                }
            }



            bool isChangeSubCombNoAlways = FS.FrameWork.Function.NConvert.ToBoolean(FS.HISFC.Components.Order.OutPatient.Classes.Function.GetBatchControlParam("HNMZ29", false, "0"));
            if (isChangeSubCombNoAlways)
            {

                if (orderFrom.Item.ItemType == EnumItemType.UnDrug)
                {
                    if (orderFrom.Item.SysClass.ID.ToString() != orderTo.Item.SysClass.ID.ToString())
                    {
                        MessageBox.Show("系统类别不同，不可以组合用！");
                        return -1;
                    }
                    if (orderFrom.ExeDept.ID != orderTo.ExeDept.ID)
                    {
                        MessageBox.Show("执行科室不同，不能组合使用!", "提示");
                        return -1;

                    }
                }

                return 1;
            }


            if (isNew && !string.IsNullOrEmpty(orderFrom.Frequency.ID) && orderFrom.Frequency.ID != "PRN")
            {
                if (orderFrom.Frequency.ID != orderTo.Frequency.ID)
                {
                    MessageBox.Show("频次不同，不可以组合用！");
                    return -1;
                }
            }


            if (isNew && orderFrom.InjectCount > 0)
            {
                if (orderFrom.InjectCount != orderTo.InjectCount)
                {
                    MessageBox.Show("院注次数不同，不可以组合用！");
                    return -1;
                }
            }

            if (orderFrom.ExeDept.ID != orderTo.ExeDept.ID)
            {
                MessageBox.Show("执行科室不同，不能组合使用!", "提示");
                return -1;
            }

            if (orderFrom.Item.ItemType == EnumItemType.Drug)		//只对药品判断用法是否相同
            {
                if (isNew && !string.IsNullOrEmpty(orderFrom.Usage.ID))
                {
                    #region 用法判断
                    if (orderFrom.Item.SysClass.ID.ToString() != "PCC")
                    {
                        if (!IsSameUsage(orderFrom.Usage.ID, orderTo.Usage.ID))
                        {
                            MessageBox.Show("用法不同，不可以进行组合！");
                            return -1;
                        }
                    }
                    #endregion
                }

                if (orderFrom.Item.SysClass.ID.ToString() == "PCC" || orderFrom.Item.SysClass.ID.ToString() == "C")
                {

                    if (isNew && orderFrom.HerbalQty > 0)
                    {
                        if (orderFrom.HerbalQty != orderTo.HerbalQty)
                        {
                            MessageBox.Show("草药付数不同，不可以组合用！");
                            return -1;
                        }
                    }
                }
            }
            else
            {
                if (orderFrom.Item.SysClass.ID.ToString() != orderTo.Item.SysClass.ID.ToString())
                {
                    MessageBox.Show("系统类别不同，不可以组合用！");
                    return -1;
                }
                if (orderFrom.Item.SysClass.ID.ToString() == "UL")//检验
                {
                    if (isNew && orderFrom.Qty > 0)
                    {
                        if (orderFrom.Qty != orderTo.Qty)
                        {
                            MessageBox.Show("检验数量不同，不可以组合用！");
                            return -1;
                        }
                    }

                    if (isNew && string.IsNullOrEmpty(orderFrom.Sample.Name))
                    {
                        if (orderFrom.Sample.Name != orderTo.Sample.Name)
                        {
                            MessageBox.Show("检验样本不同，不可以组合用！");
                            return -1;
                        }
                    }
                }
            }
            return 1;
        }

        /// <summary>
        /// 用法列表
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper usageHelper = null;

        /// <summary>
        /// 判断相同用法是否忽略系统类别 1 按照系统类别判断，其他根据编码判断
        /// </summary>
        public static int isJudgSameSysUsageBysSysCode = -1;

        /// <summary>
        /// 是否相同用法
        /// </summary>
        /// <param name="usageID1"></param>
        /// <param name="usageID2"></param>
        /// <returns></returns>
        public static bool IsSameUsage(string usageID1, string usageID2)
        {
            try
            {
                if (usageHelper == null)
                {
                    ArrayList alUsage = CacheManager.GetConList("USAGE");
                    usageHelper = new FS.FrameWork.Public.ObjectHelper(alUsage);
                }

                FS.HISFC.Models.Base.Const usageObj1 = usageHelper.GetObjectFromID(usageID1) as FS.HISFC.Models.Base.Const;
                FS.HISFC.Models.Base.Const usageObj2 = usageHelper.GetObjectFromID(usageID2) as FS.HISFC.Models.Base.Const;

                if (isJudgSameSysUsageBysSysCode == -1)
                {
                    isJudgSameSysUsageBysSysCode = CacheManager.ContrlManager.GetControlParam<int>("HNMZ28", true, 0);
                }

                if (isJudgSameSysUsageBysSysCode == 1
                    && (!string.IsNullOrEmpty(usageObj1.UserCode) && !string.IsNullOrEmpty(usageObj2.UserCode))
                    )
                {
                    //系统类别判断是否同一类别用法
                    if (usageObj1.UserCode.Trim() != usageObj2.UserCode.Trim())
                    {
                        return false;
                    }
                }
                else
                {
                    if (usageID1 != usageID2)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return false;
            }
        }

        #region 取整算法

        /// <summary>
        /// 是否已经查询取整接口，如果接口为空的话 就用默认方法
        /// </summary>
        private static bool isGetSplitInterface = false;

        /// <summary>
        /// 医嘱取整接口
        /// </summary>
        private static FS.SOC.HISFC.BizProcess.OrderInterface.Common.IOrderSplit iOrderSplit = null;

        /// <summary>
        /// 获取药品的门诊取整类型
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public static int GetSplitType(ref FS.HISFC.Models.Order.OutPatient.Order orderBase)
        {
            if (orderBase.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                return 1;
            }

            if (iOrderSplit == null && !isGetSplitInterface)
            {
                iOrderSplit = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.BizProcess.Integrate.Pharmacy), typeof(FS.SOC.HISFC.BizProcess.OrderInterface.Common.IOrderSplit)) as FS.SOC.HISFC.BizProcess.OrderInterface.Common.IOrderSplit;
                isGetSplitInterface = true;
            }

            if (iOrderSplit != null)
            {
                string split = iOrderSplit.GetSplitType(0, orderBase);
                if (!string.IsNullOrEmpty(split))
                {
                    ((FS.HISFC.Models.Pharmacy.Item)orderBase.Item).SplitType = split;
                }
            }

            return 1;
        }

        /// <summary>
        /// 获取药品的取整类型
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public static int GetSplitType(ref FS.HISFC.Models.Order.Inpatient.Order orderBase)
        {
            if (orderBase.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                return 1;
            }

            if (iOrderSplit == null && !isGetSplitInterface)
            {
                iOrderSplit = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.BizProcess.Integrate.Pharmacy), typeof(FS.SOC.HISFC.BizProcess.OrderInterface.Common.IOrderSplit)) as FS.SOC.HISFC.BizProcess.OrderInterface.Common.IOrderSplit;
                isGetSplitInterface = true;
            }

            if (iOrderSplit != null)
            {
                if (orderBase.OrderType.IsDecompose)
                {
                    string split = iOrderSplit.GetSplitType(2, orderBase);

                    if (!string.IsNullOrEmpty(split))
                    {
                        ((FS.HISFC.Models.Pharmacy.Item)orderBase.Item).CDSplitType = split;
                    }
                }
                else
                {
                    string split = iOrderSplit.GetSplitType(1, orderBase);

                    if (!string.IsNullOrEmpty(split))
                    {
                        ((FS.HISFC.Models.Pharmacy.Item)orderBase.Item).LZSplitType = split;
                    }
                }
            }
            else
            {
                HISFC.Models.Pharmacy.Item phaItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(orderBase.Item.ID);
                if (orderBase.OrderType.IsDecompose)
                {
                    ((FS.HISFC.Models.Pharmacy.Item)orderBase.Item).CDSplitType = phaItem.CDSplitType;
                }
                else
                {
                    ((FS.HISFC.Models.Pharmacy.Item)orderBase.Item).CDSplitType = phaItem.LZSplitType;
                }
            }

            return 1;
        }

        //注意住院长嘱的取整，是放到患者库存里面处理的

        /// <summary>
        /// 重新计算总量(此处只是计算住院临嘱和门诊处方，长嘱的计算在患者库存中处理）
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public static int ReComputeQty(FS.HISFC.Models.Order.Order orderBase)
        {
            //天数没有填时，不计算
            if (orderBase.HerbalQty <= 0)
            {
                return 1;
            }

            if (orderBase.Item.ID != "999")
            {
                #region 接口取整

                if (iOrderSplit == null && !isGetSplitInterface)
                {
                    iOrderSplit = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.BizProcess.Integrate.Pharmacy), typeof(FS.SOC.HISFC.BizProcess.OrderInterface.Common.IOrderSplit)) as FS.SOC.HISFC.BizProcess.OrderInterface.Common.IOrderSplit;
                    isGetSplitInterface = true;
                }

                if (iOrderSplit != null)
                {
                    if (iOrderSplit.ComputeOrderQty(orderBase) == -1)
                    {
                        MessageBox.Show(iOrderSplit.ErrInfo);
                        return -1;
                    }
                }
                #endregion
                else
                {
                    #region 默认取整规则
                    try
                    {
                        //草药计算方式不一样
                        if (orderBase.Item.ItemType == EnumItemType.Drug)
                        {
                            HISFC.Models.Pharmacy.Item phaItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(orderBase.Item.ID);

                            if (phaItem == null)
                            {
                                MessageBox.Show("查找药品项目失败");
                                return -1;
                            }

                            #region 处理数据

                            #region 处理频次
                            decimal frequence = 0;

                            if (phaItem.SysClass.ID.ToString() == "PCC")
                            {
                                frequence = 1;
                            }
                            else
                            {
                                if (orderBase.Frequency.Days[0] == "0" || string.IsNullOrEmpty(orderBase.Frequency.Days[0]))
                                {
                                    orderBase.Frequency.Days[0] = "1";
                                    frequence = orderBase.Frequency.Times.Length;
                                }
                                else
                                {
                                    try
                                    {
                                        frequence = SOC.HISFC.BizProcess.Cache.Order.GetFrequencyCount(orderBase.Frequency.ID);
                                        //frequence = Math.Round(orderBase.Frequency.Times.Length / FS.FrameWork.Function.NConvert.ToDecimal(orderBase.Frequency.Days[0]), 2);
                                    }
                                    catch
                                    {
                                        frequence = orderBase.Frequency.Times.Length;
                                    }
                                }
                            }
                            #endregion

                            string err = "";

                            decimal doseOnce = orderBase.DoseOnce;
                            if (orderBase.DoseUnit == phaItem.MinUnit)
                            {
                                doseOnce = orderBase.DoseOnce * phaItem.BaseDose;
                            }
                            #endregion

                            #region 计算取整总量

                            //0 最小单位总量取整" 数据库值 0
                            //1 包装单位总量取整" 数据库值 1  口服特别是中成药、妇科用药较多
                            //2 最小单位每次取整" 数据库值 2  针剂较多这样
                            //3 包装单位每次取整" 数据库值 3  几乎没有用
                            //4 最小单位可拆分 即不处理任何取整

                            string splitType = "4";
                            if (orderBase.GetType() == typeof(FS.HISFC.Models.Order.OutPatient.Order))
                            {
                                splitType = phaItem.SplitType;
                            }
                            else
                            {
                                if (((FS.HISFC.Models.Order.Inpatient.Order)orderBase).OrderType.IsDecompose)
                                {
                                    return 1;
                                }
                                splitType = phaItem.LZSplitType;
                            }

                            //0 包装单位；1 最小单位
                            string unitFlag = "";

                            //获取执行频次，只能为整数
                            decimal execQty = Math.Ceiling(frequence * orderBase.HerbalQty);

                            switch (splitType)
                            {
                                case "0":
                                    //草药开立不取整  houwb 2011-3-15{E0FCD746-5605-4318-860B-50467115BE6D}
                                    //草药的总量不取整，开出1.5g就是1.5g
                                    //如果是1包每次计量单位6g，开出来一天3g 就按照0.5包发药
                                    //if (phaItem.SysClass.ID.ToString() == "PCC")
                                    //{
                                    //    orderBase.Qty = Math.Round(doseOnce * execQty / phaItem.BaseDose, 2);
                                    //    orderBase.Unit = phaItem.MinUnit;
                                    //    unitFlag = "1";//最小单位
                                    //}
                                    //else
                                    //{
                                    //西药允许输入分数，对于每次用量2/3片的，
                                    // 由于除不尽，总量这里计算出来截取一下 再取整 houwb
                                    orderBase.Qty = Math.Ceiling(FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Public.String.FormatNumberReturnString(doseOnce * execQty / phaItem.BaseDose, 3)));

                                    orderBase.Unit = phaItem.MinUnit;
                                    //if (string.IsNullOrEmpty(orderBase.Unit))
                                    //{
                                    //    orderBase.Unit = phaItem.MinUnit;
                                    //}
                                    //else
                                    //{
                                    //    orderBase.Unit = phaItem.MinUnit;
                                    //}
                                    unitFlag = "1";
                                    //}
                                    break;
                                case "1":
                                    //草药开立不取整  houwb 2011-3-15{E0FCD746-5605-4318-860B-50467115BE6D}
                                    //草药的总量不取整，开出1.5g就是1.5g
                                    //如果是1包每次计量单位6g，开出来一天3g 就按照0.5包发药
                                    //if (phaItem.SysClass.ID.ToString() == "PCC")
                                    //{
                                    //    orderBase.Qty = Math.Round((doseOnce * execQty / phaItem.BaseDose) / phaItem.PackQty, 2);
                                    //    orderBase.Unit = phaItem.PackUnit;
                                    //    unitFlag = "0";//包装单位
                                    //}
                                    //else
                                    //{
                                    orderBase.Qty = Math.Ceiling((doseOnce * execQty / phaItem.BaseDose) / phaItem.PackQty);
                                    orderBase.Unit = phaItem.PackUnit;
                                    unitFlag = "0";
                                    //}
                                    break;
                                case "2":
                                    //草药开立不取整  houwb 2011-3-15{E0FCD746-5605-4318-860B-50467115BE6D}
                                    //草药的总量不取整，开出1.5g就是1.5g
                                    //如果是1包每次计量单位6g，开出来一天3g 就按照0.5包发药
                                    //if (phaItem.SysClass.ID.ToString() == "PCC")
                                    //{
                                    //    orderBase.Qty = Math.Round(doseOnce * execQty / phaItem.BaseDose, 2);
                                    //    orderBase.Unit = phaItem.MinUnit;
                                    //    unitFlag = "1";//最小单位
                                    //}
                                    //else
                                    //{
                                    orderBase.Qty = Math.Ceiling(Math.Round(Math.Ceiling(doseOnce / phaItem.BaseDose) * execQty, 6));
                                    orderBase.Unit = phaItem.MinUnit;
                                    unitFlag = "1";
                                    //}
                                    break;
                                case "3":
                                    //草药开立不取整  houwb 2011-3-15{E0FCD746-5605-4318-860B-50467115BE6D}
                                    //草药的总量不取整，开出1.5g就是1.5g
                                    //如果是1包每次计量单位6g，开出来一天3g 就按照0.5包发药
                                    //if (phaItem.SysClass.ID.ToString() == "PCC")
                                    //{
                                    //    orderBase.Qty = Math.Round((doseOnce / phaItem.BaseDose * execQty) / phaItem.PackQty, 2);
                                    //    //orderBase.Unit = phaItem.MinUnit;
                                    //    orderBase.Unit = phaItem.PackUnit;
                                    //    unitFlag = "0";//包装单位
                                    //}
                                    //else
                                    //{
                                    orderBase.Qty = Math.Ceiling(Math.Round((Math.Ceiling((doseOnce / phaItem.BaseDose) / phaItem.PackQty)) * execQty, 6));
                                    orderBase.Unit = phaItem.PackUnit;
                                    unitFlag = "0";
                                    //}
                                    break;
                                default:
                                    orderBase.Qty = Math.Round(doseOnce / phaItem.BaseDose, 2) * execQty;
                                    orderBase.Unit = phaItem.MinUnit;
                                    unitFlag = "1";
                                    break;
                            }

                            if (orderBase.GetType() == typeof(FS.HISFC.Models.Order.OutPatient.Order))
                            {
                                ((FS.HISFC.Models.Order.OutPatient.Order)orderBase).MinunitFlag = unitFlag;
                            }

                            #endregion
                        }
                        else
                        {
                            if (orderBase.Item.SysClass.ID.ToString() == "UZ")
                            {
                                decimal frequence = SOC.HISFC.BizProcess.Cache.Order.GetFrequencyCount(orderBase.Frequency.ID);
                                //orderBase.Qty = orderBase.HerbalQty * frequence;// {3BEDF8C4-0BC6-494f-B7B9-E94C63399197}
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ReComputeQty" + ex.Message);
                        return -1;
                    }
                    #endregion
                }
            }
            return 1;
        }



        /// <summary>
        /// 通过组套开立
        /// </summary>
        /// <param name="orderBase"></param>
        /// <param name="isGroup"></param>
        /// <returns></returns>
        public static int ReComputeQty(FS.HISFC.Models.Order.Order orderBase, bool isGroup)
        {

            //天数没有填时，不计算
            if (orderBase.HerbalQty <= 0)
            {
                return 1;
            }

            if (orderBase.Item.ID != "999")
            {
                #region 接口取整

                if (iOrderSplit == null && !isGetSplitInterface)
                {
                    iOrderSplit = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.BizProcess.Integrate.Pharmacy), typeof(FS.SOC.HISFC.BizProcess.OrderInterface.Common.IOrderSplit)) as FS.SOC.HISFC.BizProcess.OrderInterface.Common.IOrderSplit;
                    isGetSplitInterface = true;
                }

                if (iOrderSplit != null)
                {
                    if (iOrderSplit.ComputeOrderQty(orderBase) == -1)
                    {
                        MessageBox.Show(iOrderSplit.ErrInfo);
                        return -1;
                    }
                }
                #endregion
                else
                {
                    #region 默认取整规则
                    try
                    {
                        //草药计算方式不一样
                        if (orderBase.Item.ItemType == EnumItemType.Drug)
                        {
                            HISFC.Models.Pharmacy.Item phaItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(orderBase.Item.ID);

                            if (phaItem == null)
                            {
                                MessageBox.Show("查找药品项目失败");
                                return -1;
                            }

                            #region 处理数据

                            #region 处理频次
                            decimal frequence = 0;

                            if (phaItem.SysClass.ID.ToString() == "PCC")
                            {
                                frequence = 1;
                            }
                            else
                            {
                                if (orderBase.Frequency.Days[0] == "0" || string.IsNullOrEmpty(orderBase.Frequency.Days[0]))
                                {
                                    orderBase.Frequency.Days[0] = "1";
                                    frequence = orderBase.Frequency.Times.Length;
                                }
                                else
                                {
                                    try
                                    {
                                        frequence = SOC.HISFC.BizProcess.Cache.Order.GetFrequencyCount(orderBase.Frequency.ID);
                                        //frequence = Math.Round(orderBase.Frequency.Times.Length / FS.FrameWork.Function.NConvert.ToDecimal(orderBase.Frequency.Days[0]), 2);
                                    }
                                    catch
                                    {
                                        frequence = orderBase.Frequency.Times.Length;
                                    }
                                }
                            }
                            #endregion

                            string err = "";

                            decimal doseOnce = orderBase.DoseOnce;
                            if (orderBase.DoseUnit == phaItem.MinUnit)
                            {
                                doseOnce = orderBase.DoseOnce * phaItem.BaseDose;
                            }
                            #endregion

                            #region 计算取整总量

                            //0 最小单位总量取整" 数据库值 0
                            //1 包装单位总量取整" 数据库值 1  口服特别是中成药、妇科用药较多
                            //2 最小单位每次取整" 数据库值 2  针剂较多这样
                            //3 包装单位每次取整" 数据库值 3  几乎没有用
                            //4 最小单位可拆分 即不处理任何取整

                            string splitType = "4";
                            if (orderBase.GetType() == typeof(FS.HISFC.Models.Order.OutPatient.Order))
                            {
                                splitType = phaItem.SplitType;
                            }
                            else
                            {
                                if (((FS.HISFC.Models.Order.Inpatient.Order)orderBase).OrderType.IsDecompose)
                                {
                                    return 1;
                                }
                                splitType = phaItem.LZSplitType;
                            }

                            //0 包装单位；1 最小单位
                            string unitFlag = "";

                            //获取执行频次，只能为整数
                            decimal execQty = Math.Ceiling(frequence * orderBase.HerbalQty);

                            switch (splitType)
                            {
                                case "0":
                                    //草药开立不取整  houwb 2011-3-15{E0FCD746-5605-4318-860B-50467115BE6D}
                                    //草药的总量不取整，开出1.5g就是1.5g
                                    //如果是1包每次计量单位6g，开出来一天3g 就按照0.5包发药
                                    //if (phaItem.SysClass.ID.ToString() == "PCC")
                                    //{
                                    //    orderBase.Qty = Math.Round(doseOnce * execQty / phaItem.BaseDose, 2);
                                    //    orderBase.Unit = phaItem.MinUnit;
                                    //    unitFlag = "1";//最小单位
                                    //}
                                    //else
                                    //{
                                    //西药允许输入分数，对于每次用量2/3片的，
                                    // 由于除不尽，总量这里计算出来截取一下 再取整 houwb
                                    if (orderBase.Qty == 0)
                                    {
                                        orderBase.Qty = Math.Ceiling(FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Public.String.FormatNumberReturnString(doseOnce * execQty / phaItem.BaseDose, 3)));
                                    }
                                    else
                                    {
                                        orderBase.Qty = orderBase.Qty;
                                    }

                                    if (string.IsNullOrEmpty(orderBase.Unit))
                                    {
                                        orderBase.Unit = phaItem.MinUnit;
                                    }
                                    else
                                    {
                                        orderBase.Unit = orderBase.Unit;
                                    }


                                    if (orderBase.Unit == phaItem.MinUnit)
                                    {
                                        unitFlag = "1";
                                    }
                                    else
                                    {
                                        unitFlag = "0";
                                    }

                                    //if (string.IsNullOrEmpty(orderBase.Unit))
                                    //{
                                    //    orderBase.Unit = phaItem.MinUnit;
                                    //}
                                    //else
                                    //{
                                    //    orderBase.Unit = phaItem.MinUnit;
                                    //}
                                    //unitFlag = "1";
                                    //}
                                    break;
                                case "1":
                                    //草药开立不取整  houwb 2011-3-15{E0FCD746-5605-4318-860B-50467115BE6D}
                                    //草药的总量不取整，开出1.5g就是1.5g
                                    //如果是1包每次计量单位6g，开出来一天3g 就按照0.5包发药
                                    //if (phaItem.SysClass.ID.ToString() == "PCC")
                                    //{
                                    //    orderBase.Qty = Math.Round((doseOnce * execQty / phaItem.BaseDose) / phaItem.PackQty, 2);
                                    //    orderBase.Unit = phaItem.PackUnit;
                                    //    unitFlag = "0";//包装单位
                                    //}
                                    //else
                                    //{
                                    if (orderBase.Qty == 0)
                                    {
                                        orderBase.Qty = Math.Ceiling((doseOnce * execQty / phaItem.BaseDose) / phaItem.PackQty);
                                    }
                                    else
                                    {
                                        orderBase.Qty = orderBase.Qty;
                                    }

                                    //orderBase.Qty = Math.Ceiling((doseOnce * execQty / phaItem.BaseDose) / phaItem.PackQty);
                                    if (string.IsNullOrEmpty(orderBase.Unit))
                                    {
                                        orderBase.Unit = phaItem.PackUnit;
                                    }
                                    else
                                    {
                                        orderBase.Unit = orderBase.Unit;
                                    }


                                    unitFlag = "0";
                                    //}
                                    break;
                                case "2":
                                    //草药开立不取整  houwb 2011-3-15{E0FCD746-5605-4318-860B-50467115BE6D}
                                    //草药的总量不取整，开出1.5g就是1.5g
                                    //如果是1包每次计量单位6g，开出来一天3g 就按照0.5包发药
                                    //if (phaItem.SysClass.ID.ToString() == "PCC")
                                    //{
                                    //    orderBase.Qty = Math.Round(doseOnce * execQty / phaItem.BaseDose, 2);
                                    //    orderBase.Unit = phaItem.MinUnit;
                                    //    unitFlag = "1";//最小单位
                                    //}
                                    //else
                                    //{

                                    if (orderBase.Qty == 0)
                                    {
                                        orderBase.Qty = Math.Ceiling(Math.Round(Math.Ceiling(doseOnce / phaItem.BaseDose) * execQty, 6));
                                    }
                                    else
                                    {
                                        orderBase.Qty = orderBase.Qty;
                                    }
                                    //orderBase.Qty = Math.Ceiling(Math.Round(Math.Ceiling(doseOnce / phaItem.BaseDose) * execQty, 6));
                                    //orderBase.Unit = phaItem.MinUnit;

                                    if (string.IsNullOrEmpty(orderBase.Unit))
                                    {
                                        orderBase.Unit = phaItem.MinUnit;
                                    }
                                    else
                                    {
                                        orderBase.Unit = orderBase.Unit;
                                    }



                                    if (orderBase.Unit == phaItem.MinUnit)
                                    {
                                        unitFlag = "1";
                                    }
                                    else
                                    {
                                        unitFlag = "0";
                                    }

                                    //unitFlag = "1";
                                    //}
                                    break;
                                case "3":
                                    //草药开立不取整  houwb 2011-3-15{E0FCD746-5605-4318-860B-50467115BE6D}
                                    //草药的总量不取整，开出1.5g就是1.5g
                                    //如果是1包每次计量单位6g，开出来一天3g 就按照0.5包发药
                                    //if (phaItem.SysClass.ID.ToString() == "PCC")
                                    //{
                                    //    orderBase.Qty = Math.Round((doseOnce / phaItem.BaseDose * execQty) / phaItem.PackQty, 2);
                                    //    //orderBase.Unit = phaItem.MinUnit;
                                    //    orderBase.Unit = phaItem.PackUnit;
                                    //    unitFlag = "0";//包装单位
                                    //}
                                    //else
                                    //{
                                    if (orderBase.Qty == 0)
                                    {
                                        orderBase.Qty = Math.Ceiling(Math.Round((Math.Ceiling((doseOnce / phaItem.BaseDose) / phaItem.PackQty)) * execQty, 6));
                                    }
                                    else
                                    {
                                        orderBase.Qty = orderBase.Qty;
                                    }

                                    //orderBase.Qty = Math.Ceiling(Math.Round((Math.Ceiling((doseOnce / phaItem.BaseDose) / phaItem.PackQty)) * execQty, 6));


                                    if (string.IsNullOrEmpty(orderBase.Unit))
                                    {
                                        orderBase.Unit = phaItem.PackUnit;
                                    }
                                    else
                                    {
                                        orderBase.Unit = orderBase.Unit;
                                    }

                                    unitFlag = "0";
                                    //}
                                    break;
                                default:

                                    if (orderBase.Qty == 0)
                                    {
                                        orderBase.Qty = Math.Round(doseOnce / phaItem.BaseDose, 2) * execQty;
                                    }
                                    else
                                    {
                                        orderBase.Qty = orderBase.Qty;
                                    }
                                    //orderBase.Qty = Math.Round(doseOnce / phaItem.BaseDose, 2) * execQty;

                                    if (string.IsNullOrEmpty(orderBase.Unit))
                                    {
                                        orderBase.Unit = phaItem.MinUnit;
                                    }
                                    else
                                    {
                                        orderBase.Unit = orderBase.Unit;
                                    }

                                    //orderBase.Unit = phaItem.MinUnit;
                                    //orderBase.Unit = orderBase.Unit;


                                    if (orderBase.Unit == phaItem.MinUnit)
                                    {
                                        unitFlag = "1";
                                    }
                                    else
                                    {
                                        unitFlag = "0";
                                    }


                                    //unitFlag = "1";
                                    break;
                            }

                            if (orderBase.GetType() == typeof(FS.HISFC.Models.Order.OutPatient.Order))
                            {
                                ((FS.HISFC.Models.Order.OutPatient.Order)orderBase).MinunitFlag = unitFlag;
                            }

                            #endregion
                        }
                        else
                        {
                            if (orderBase.Item.SysClass.ID.ToString() == "UZ")
                            {
                                decimal frequence = SOC.HISFC.BizProcess.Cache.Order.GetFrequencyCount(orderBase.Frequency.ID);
                                //orderBase.Qty = orderBase.HerbalQty * frequence;// {3BEDF8C4-0BC6-494f-B7B9-E94C63399197}
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ReComputeQty" + ex.Message);
                        return -1;
                    }
                    #endregion
                }
            }
            return 1;




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

        #region 查询项目医保、公费信息

        /// <summary>
        /// 获取项目等级
        /// </summary>
        /// <param name="itemGrade"></param>
        /// <returns></returns>
        public static string GetItemGrade(string itemGrade)
        {
            if (itemGrade == "1")
            {
                return "甲类";
            }
            else if (itemGrade == "2")
            {
                return "乙类";
            }
            else if (itemGrade == "3")
            {
                return "丙类";
            }
            return "";
        }

        /// <summary>
        /// 返回医保对照信息
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public static FS.HISFC.Models.SIInterface.Compare GetPactItem(FS.HISFC.Models.Order.Order order)
        {
            /*
             *  1、自费不处理
                2、医保
                       1）、从对照表查找
                3、公费
                       1）、按项目从自费表查找（纯自费项目）
                       2）、按项目从自付表查找（需审批项目）
                       2）、按最小费用从自付表查找（需审批项目）
                       3）、查找合同单位维护的比例（公费报销项目）
                       
              显示： 
               1、等级：甲乙丙（可能为空）
               2、比例：自付比例
               3、是否需审批（只针对公医）
             * */

            //if (SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(order.Patient.Pact.ID).PayKind.ID == "01")
            //{
            //    return null;
            //}

            //从视图查询
            string sql = @"select t.等级 项目等级,
                                   t.自付比例 自付比例,
                                   t.paykind_code 合同单位类别,
                                   t.pact_code 合同单位,
                                   t.item_code 项目编码,
                                   t.需审批,
                                   t.center_memo 医保限制信息,
                                   1 sort
                            from view_get_pactitem t
                            where (t.pact_code='{0}' or t.pact_code is null)
                            and t.paykind_code='{1}'
                            and t.item_code='{2}' 
                            
                            union all
                            select t.等级 项目等级,
                                   t.自付比例 自付比例,
                                   t.paykind_code 合同单位类别,
                                   t.pact_code 合同单位,
                                   t.item_code 项目编码,
                                   t.需审批,
                                   t.center_memo 医保限制信息,
                                   2 sort
                            from view_get_pactitem t
                            where (t.pact_code='{0}' or t.pact_code is null)
                            and t.paykind_code='{1}'
                            and t.item_code='{3}' 
                            
                            union all
                            select t.等级 项目等级,
                                   t.自付比例 自付比例,
                                   t.paykind_code 合同单位类别,
                                   t.pact_code 合同单位,
                                   t.item_code 项目编码,
                                   t.需审批,
                                   t.center_memo 医保限制信息,
                                   3 sort
                            from view_get_pactitem t
                            where (t.pact_code='{0}' or t.pact_code is null)
                            and t.paykind_code='{1}'
                            and t.item_code is null
                            order by sort";


            try
            {
                FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();

                string MinFee = "";
                if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    MinFee = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID).MinFee.ID;
                }
                else
                {
                    MinFee = SOC.HISFC.BizProcess.Cache.Fee.GetItem(order.Item.ID).MinFee.ID;
                }

                sql = string.Format(sql, order.Patient.Pact.ID, SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(order.Patient.Pact.ID).PayKind.ID, order.Item.ID, MinFee);

                FS.HISFC.Models.SIInterface.Compare compareObj = null;

                System.Data.DataSet dtSet = new System.Data.DataSet();
                if (deptMgr.ExecQuery(sql, ref dtSet) == -1)
                {
                    MessageBox.Show(deptMgr.Err);
                    return null;
                }

                if (dtSet != null)
                {
                    foreach (System.Data.DataRow drow in dtSet.Tables[0].Rows)
                    {
                        compareObj = new FS.HISFC.Models.SIInterface.Compare();
                        compareObj.HisCode = order.Item.ID;
                        compareObj.ID = order.Item.ID;
                        compareObj.CenterItem.ItemGrade = drow[0].ToString();
                        compareObj.CenterItem.Rate = FS.FrameWork.Function.NConvert.ToDecimal(drow[1]);
                        compareObj.CenterItem.PactCode = order.Patient.Pact.ID;
                        compareObj.CenterFlag = drow[5].ToString(); //这个代表是否需审批项目（只针对公医）
                        compareObj.Practicablesymptomdepiction = drow[6].ToString(); //医保限制用药提示信息

                        break;
                    }
                }
                return compareObj;
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region 查询药品扩展信息，基药等

        /// <summary>
        /// 获取药品基药标记
        /// </summary>
        /// <param name="phaItem"></param>
        /// <returns></returns>
        public static string GetPhaEssentialDrugs(FS.HISFC.Models.Base.Item phaItem)
        {
            string sqlID = "Order.GetPhaEssentialDrugs";
            string sql = "";

            /*@"select (select name from com_dictionary y
                                               where y.type='BASEDRUGCODE'
                                               and y.code=f.extend2)
                                        from pha_com_baseinfo f
                                        where f.drug_code='{0}'";
             * */
            if (personMgr.Sql.GetSql(sqlID, ref sql) == -1)
            {
                ShowBalloonTip(2, "错误", personMgr.Err, ToolTipIcon.Info);
                return "";
            }

            string ss = personMgr.ExecSqlReturnOne(string.Format(sql, phaItem.ID), "");

            return ss;
        }

        /// <summary>
        /// 获取肿瘤用药提示
        /// </summary>
        /// <param name="phaItem"></param>
        /// <returns></returns>
        public static string GetPhaForTumor(FS.HISFC.Models.Base.Item phaItem)
        {
            string sqlID = "Order.GetPhaForTumor";
            string sql = "";

            /*@"select (select name from com_dictionary y
                                               where y.type='ZLDRUG'
                                               and y.code=f.extend4)
                                        from pha_com_baseinfo f
                                        where f.drug_code='{0}'";
             * */
            if (personMgr.Sql.GetSql(sqlID, ref sql) == -1)
            {
                ShowBalloonTip(2, "错误", personMgr.Err, ToolTipIcon.Info);
                return "";
            }

            string ss = personMgr.ExecSqlReturnOne(string.Format(sql, phaItem.ID), "");

            return ss;
        }

        #endregion

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
                IModifyOrder = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.Components.Order.Controls.ucOrder), typeof(FS.SOC.HISFC.BizProcess.OrderInterface.Common.IModifyOrder)) as FS.SOC.HISFC.BizProcess.OrderInterface.Common.IModifyOrder;
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

        #region DLL嵌套调用
        //{993C4984-D7C6-462c-A554-6BA7251E3D4B}

        /// <summary>
        /// 登录成功，如果诊室屏是开启的，将显示该诊室对应的医生个人信息介绍，具体的信息在后台管理里的医生管理进行
        /// </summary>
        /// <param name="DOCTOR_ID"></param>
        /// <returns></returns>
        [DllImport("Queue.dll", EntryPoint = "Queue_Login", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Queue_Login(String DOCTOR_ID);

        /// <summary>
        /// 成功会清除当前对应的诊室屏的医生相关个人以及排队信息
        /// </summary>
        /// <returns></returns>
        [DllImport("Queue.dll", EntryPoint = "Queue_Logout", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Queue_Logout();

        /// <summary>
        /// 医生呼叫功能
        /// </summary>
        /// <param name="CURR_ID">就诊ID---这个对应V_CALL_REGISTER里的ID字段</param>
        /// <param name="CURR_NAME">就诊人员姓名</param>
        /// <param name="QUEUE_NUM">就诊序号</param>
        /// <param name="NEXT_ID"></param>
        /// <param name="NEXT_NAME"></param>
        /// <param name="NEXT_QUEUE_NUM"></param>
        /// <returns></returns>
        [DllImport("Queue.dll", EntryPoint = "Queue_CallNext", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Queue_CallNext(String CURR_ID, String CURR_NAME, int QUEUE_NUM, String NEXT_ID, String NEXT_NAME, int NEXT_QUEUE_NUM);

        /// <summary>
        /// 医生结诊功能
        /// </summary>
        /// <param name="CURR_ID">Int Queue_CallEndcall(String CURR_ID)</param>
        /// <returns></returns>
        [DllImport("Queue.dll", EntryPoint = "Queue_CallEndcall", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Queue_CallEndcall(String CURR_ID);

        /// <summary>
        /// 过号呼叫标识
        /// </summary>
        /// <param name="CURR_ID">就诊ID---这个对应V_CALL_REGISTER里的ID字段</param>
        /// <param name="CURR_NAME">就诊人员姓名</param>
        /// <param name="QUEUE_NUM">就诊序号</param>
        /// <returns></returns>
        [DllImport("Queue.dll", EntryPoint = "Queue_NoCall", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Queue_NoCall(String CURR_ID, String CURR_NAME, int QUEUE_NUM);

        /// <summary>
        /// 取消分诊
        /// </summary>
        /// <param name="CURR_ID">就诊ID---这个对应V_CALL_REGISTER里的ID字段</param>
        /// <returns></returns>
        [DllImport("Queue.dll", EntryPoint = "Queue_Cancel", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Queue_Cancel(String CURR_ID);

        /// <summary>
        /// 重新待诊
        /// </summary>
        /// <param name="CURR_ID">就诊ID---这个对应V_CALL_REGISTER里的ID字段</param>
        /// <returns></returns>
        [DllImport("Queue.dll", EntryPoint = "Queue_Update", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Queue_Update(String CURR_ID);

        /// <summary>
        /// 插入分诊信息
        /// </summary>
        /// <param name="CURR_ID">就诊ID---这个对应V_CALL_REGISTER里的ID字段</param>
        /// <param name="CURR_NAME">就诊人员姓名</param>
        /// <param name="QUEUE_NUM">就诊序号</param>
        /// <param name="DEPART_ID">科室编码</param>
        /// <param name="DEPART_NAME">科室名称</param>
        /// <param name="DOCTOR_ID">医生编码</param>
        /// <param name="DOCTOR_NAME">医生名称</param>
        /// <returns></returns>
        [DllImport("Queue.dll", EntryPoint = "Queue_Insert", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Queue_Insert(String CURR_ID, String CURR_NAME, int QUEUE_NUM, String DEPART_ID, String DEPART_NAME, String DOCTOR_ID, String DOCTOR_NAME);

        /// <summary>
        /// 显示当前就诊患者信息
        /// </summary>
        /// <param name="CURR_ID">就诊ID---这个对应V_CALL_REGISTER里的ID字段</param>
        /// <param name="CURR_NAME">就诊人员姓名</param>
        /// <param name="QUEUE_NUM">就诊序号</param>
        /// <returns></returns>
        [DllImport("Queue.dll", EntryPoint = "Queue_Show", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Queue_Show(String CURR_ID, String CURR_NAME, int QUEUE_NUM);


        #endregion

        #region 聚点排队叫号webservice
        //{390EA9BE-1A9C-43da-B26B-08533FC00415}
        /// <summary>
        /// 需要WebService支持Post调用
        /// </summary>
        public static bool PostWebServiceByJson(String URL, String MethodName, Hashtable Pars, bool IsLoginOrOut)
        {
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(URL);
            request.Method = "POST";
            request.ContentType = "text/xml; charset=utf-8";

            // 凭证
            request.Credentials = System.Net.CredentialCache.DefaultCredentials;
            //超时时间
            request.Timeout = 10000;
            byte[] data = HashtableToSoap12(Pars, "http://tempuri.org/", MethodName, IsLoginOrOut);
            request.ContentLength = data.Length;
            System.IO.Stream writer = request.GetRequestStream();
            writer.Write(data, 0, data.Length);
            writer.Close();
            var response = request.GetResponse();
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream(), Encoding.UTF8);
            String retXml = sr.ReadToEnd();
            sr.Close();
            doc.LoadXml(retXml);
            System.Xml.XmlNamespaceManager mgr = new System.Xml.XmlNamespaceManager(doc.NameTable);
            mgr.AddNamespace("soap", "http://www.w3.org/2003/05/soap-envelope");
            String xmlStr = doc.SelectSingleNode("//soap:Body/*/*", mgr).InnerXml;

            if (xmlStr == "Call Success")
            {
                return true;
            }

            return false;
        }

        private static byte[] HashtableToSoap12(Hashtable ht, String XmlNs, String MethodName, bool isLoginOrOut)
        {
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml("<soap12:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap12=\"http://www.w3.org/2003/05/soap-envelope\"></soap12:Envelope>");
            System.Xml.XmlDeclaration decl = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.InsertBefore(decl, doc.DocumentElement);
            System.Xml.XmlElement soapBody = doc.CreateElement("soap12", "Body", "http://www.w3.org/2003/05/soap-envelope");

            System.Xml.XmlElement soapMethod = doc.CreateElement(MethodName);
            soapMethod.SetAttribute("xmlns", XmlNs);
            System.Xml.XmlElement soapPar = doc.CreateElement("Call_PatientInfo");
            soapPar.InnerXml = ObjectToSoapXml(HashtableToJson(ht, 0, isLoginOrOut));
            soapMethod.AppendChild(soapPar);
            soapBody.AppendChild(soapMethod);
            doc.DocumentElement.AppendChild(soapBody);
            return Encoding.UTF8.GetBytes(doc.OuterXml);
        }

        private static string ObjectToSoapXml(object o)
        {
            System.Xml.Serialization.XmlSerializer mySerializer = new System.Xml.Serialization.XmlSerializer(o.GetType());
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            mySerializer.Serialize(ms, o);
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml(Encoding.UTF8.GetString(ms.ToArray()));
            if (doc.DocumentElement != null)
            {
                return doc.DocumentElement.InnerXml;
            }
            else
            {
                return o.ToString();
            }
        }

        public static string HashtableToJson(Hashtable hr, int readcount, bool isLoginOrOut)
        {
            string json = string.Empty;
            if (isLoginOrOut)
            {
                json = "<Request>";
            }
            else
            {
                json = "<patient_info>";
            }

            foreach (DictionaryEntry row in hr)
            {
                try
                {
                    string keyStart = "<" + row.Key + ">";
                    string keyEnd = "</" + row.Key + ">";
                    if (row.Value is Hashtable)
                    {
                        Hashtable t = (Hashtable)row.Value;
                        if (t.Count > 0)
                        {
                            json += keyStart + HashtableToJson(t, readcount++, isLoginOrOut) + keyEnd + ",";
                        }
                        else { json += keyStart + "{}," + keyEnd; }
                    }
                    else
                    {
                        string value = "" + row.Value.ToString() + "";
                        json += keyStart + value + keyEnd;
                    }
                }
                catch { }
            }
            if (isLoginOrOut)
            {
                json = json + "</Request>";
            }
            else
            {
                json = json + "</patient_info>";
            }
            return json;
        }
        #endregion
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
            if (System.IO.Directory.Exists(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "./Log/Order/InOrder") == false)
            {
                System.IO.Directory.CreateDirectory(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "./Log/Order/InOrder");
            }
            FS.HISFC.BizLogic.Manager.DataBase dbMgr = new FS.HISFC.BizLogic.Manager.DataBase();
            DateTime dtNow = dbMgr.GetDateTimeFromSysDateTime();

            //保存一周的日志
            System.IO.File.Delete(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "./Log/Order/OutOrder/" + dtNow.AddDays(-7).ToString("yyyyMMdd") + ".LOG");
            string name = dtNow.ToString("yyyyMMdd");
            System.IO.StreamWriter w = new System.IO.StreamWriter(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "./Log/Order/OutOrder/" + name + ".LOG", true);
            w.WriteLine(dtNow.ToString("yyyy-MM-dd HH:mm:ss") + logInfo);
            w.Flush();
            w.Close();
        }
    }
}