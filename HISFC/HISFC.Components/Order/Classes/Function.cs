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
    /// [��������: ҽ�����ú���]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2004-10-12]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class Function
    {
        /// <summary>
        /// ��xml��ȡҽԺlogo����picturebox
        /// </summary>
        /// <param name="xmlpath">xml·�������ԣ�  PS���Ӹ�Ŀ¼��ʼ</param>
        /// <param name="root">xml���ڵ�</param>
        /// <param name="secondNode">Ҫ���ҵ�Ŀ��ڵ�</param>
        /// <param name="erro">������Ϣ</param>
        public static string GetHospitalLogo(string xmlpath, string root, string secondNode, string erro)
        {
            xmlpath = FS.FrameWork.WinForms.Classes.Function.CurrentPath + xmlpath;
            return FS.SOC.Public.XML.SettingFile.ReadSetting(xmlpath, root, secondNode, erro);
        }

        #region Ĭ��ִ�п����б�

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
        /// ��ȡִ�п�����Ϣ
        /// </summary>
        /// <param name="reciptDept">��������</param>
        /// <param name="itemCode">��Ŀ����</param>
        /// <param name="execDept">ԭ��ִ�п���</param>
        /// <param name="defaultExecDept">���ص�Ĭ��ִ�п���</param>
        /// <param name="alExecDept">ִ�п����б�</param>
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
                            //��һ����ǰԺ����ִ�п���
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
        /// Ĭ��ִ�п��ҽӿ�
        /// </summary>
        private static FS.HISFC.BizProcess.Interface.Fee.IExecDept IExecDept = null;

        /// <summary>
        /// �Ƿ��Ѳ�ѯĬ��ִ�п��ҽӿ�
        /// </summary>
        private static int IGetExecDept = 0;

        /// <summary>
        /// ��ȡĬ��ִ�п����б�
        /// </summary>
        /// <param name="recipeDept"></param>
        /// <param name="item"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        [Obsolete("���ϣ���SetExecDept����", true)]
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
                Components.Order.Classes.Function.ShowBalloonTip(2, "��ȡĬ��ִ�п��Ҵ���", errInfo, ToolTipIcon.Error);
            }
            al = SOC.HISFC.BizProcess.Cache.Common.GetValidDept();
            return al;
        }

        /// <summary>
        /// ��ȡĬ��ִ�п��ұ���
        /// </summary>
        /// <param name="recipeDept"></param>
        /// <param name="item"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        [Obsolete("���ϣ���SetExecDept����", true)]
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
                    Components.Order.Classes.Function.ShowBalloonTip(2, "��ȡĬ��ִ�п��Ҵ���", errInfo, ToolTipIcon.Error);
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

        #region ҽ������
        /// <summary>
        /// ����ҽ���״�Ƶ����Ϣ
        /// </summary>
        /// <param name="order"></param>
        public static void SetDefaultOrderFrequency(FS.HISFC.Models.Order.Inpatient.Order order)
        {
            if (order.OrderType.IsDecompose
                || order.OrderType.ID == "CD"
                || order.OrderType.ID == "QL")//Ĭ��Ϊ��Ŀ��Ƶ��
            {
                if (order.Item.GetType() == typeof(FS.HISFC.Models.Pharmacy.Item)
                    && SOC.HISFC.BizProcess.Cache.Order.GetFrequency((order.Item as FS.HISFC.Models.Pharmacy.Item).Frequency.ID) != null
                    && !string.IsNullOrEmpty(SOC.HISFC.BizProcess.Cache.Order.GetFrequency((order.Item as FS.HISFC.Models.Pharmacy.Item).Frequency.ID).ID))
                {
                    order.Frequency = (order.Item as FS.HISFC.Models.Pharmacy.Item).Frequency.Clone();
                    order.Frequency.Time = "25:00";//Ĭ��Ϊ�����㣬��Ҫ����
                }
            }
            //else if (order.Item.IsPharmacy && order.OrderType.IsDecompose == false)//ҩƷ ��ʱҽ����Ƶ��Ϊ�գ�Ĭ��Ϊ��Ҫʱ�����prn
            else if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug
                && order.OrderType.IsDecompose == false)//ҩƷ ��ʱҽ����Ƶ��Ϊ�գ�Ĭ��Ϊ��Ҫʱ�����prn
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
                    order.Frequency.ID = GetDefaultFrequencyID();//ҩƷ��ʱҽ��Ĭ��Ϊ��Ҫʱִ��
                }
            }
            //else if (order.Item.IsPharmacy == false && order.OrderType.IsDecompose == false)
            else if (order.Item.ItemType != EnumItemType.Drug
                && order.OrderType.IsDecompose == false)
            {
                order.Frequency.ID = GetDefaultFrequencyID();//��ҩƷ��ʱҽ��Ĭ��Ϊÿ��һ��
            }
        }

        /// <summary>
        /// �Ƿ�������
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
        /// ����ҽ������ж�,�Ƿ��Զ���������
        /// </summary>
        /// <param name="orderType"></param>
        /// <returns></returns>
        [Obsolete("����", true)]
        public static bool IsAutoCalTotal(FS.HISFC.Models.Order.OrderType orderType)
        {
            return false;
        }

        #region ��ȡƵ����Ϣ

        /// <summary>
        /// ��ȡ������Ĭ��Ƶ��
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
        /// ��ȡ������Ĭ��Ƶ��ID
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
        /// Ƶ���б�
        /// </summary>
        private static ArrayList alFrequency = null;

        /// <summary>
        /// Ƶ�ΰ�����
        /// </summary>
        private static FS.FrameWork.Public.ObjectHelper freqHelper = null;

        /// <summary>
        /// ������е�Ƶ�ΰ�����
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
        /// Ƥ������
        /// </summary>
        //public const string TipHypotest = "(��Ƥ��)";
        public const string TipHypotest = "";

        #endregion

        #region ����Ȩ

        /// <summary>
        /// ְ��������
        /// </summary>
        static FS.FrameWork.Public.ObjectHelper levlHelper = null;

        static FS.HISFC.Models.Base.Employee emplObj = null;
        static FS.HISFC.Models.Base.Const levlObj = null;

        /// <summary>
        /// ����Ƿ��д���Ȩ
        /// </summary>
        /// <param name="emplCode"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public static int CheckIsHaveOrderPower(string emplCode, ref string errInfo)
        {
            try
            {
                //ҽ��Ȩ����֤����//{BFDA551D-7569-47dd-85C4-1CA21FE494BD}
                //�Ƿ���ƴ���Ȩ
                bool isUseControl = CacheManager.ContrlManager.GetControlParam<bool>("HNMET1", true, false);

                if (isUseControl)
                {
                    //��ô����ҩƷ�ȼ����жϴ���Ȩ�ˣ�����
                    #region ��ȡ����
                    if (alDrugGrade == null)
                    {
                        alDrugGrade = CacheManager.GetConList("SpeDrugGrade");
                    }

                    if (alDrugPosition == null)
                    {
                        alDrugPosition = CacheManager.GetConList("SpeDrugPosition");
                    }

                    #endregion

                    #region ��ȡ����

                    int drugPermission = 0;

                    //�ж϶�������(ְ��<-->ҩƷ�ȼ�  ְ��<-->ҩƷ�ȼ�)

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
                    //���ڸ���Ҫ��ϼ� �Ȱ���ְ������ ��ʱ����
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
                        errInfo = "��ȡ��Ա��Ϣʧ�ܣ�����ϵ��Ϣ�ƣ�";
                        return -1;
                    }

                    levlObj = levlHelper.GetObjectFromID(emplObj.Level.ID) as FS.HISFC.Models.Base.Const;
                    if (levlObj == null)
                    {
                        errInfo = "��ȡְ����Ϣʧ�ܣ�����ϵ��Ϣ�ƣ�";
                        return -1;
                    }

                    if (levlObj.Memo.Trim() == "�޴���Ȩ")
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


        #region ��������ʾ

        /// <summary>
        /// У�龯���߷�ʽ��0 �������н���жϣ�1 �����շѺ�������ж�
        /// </summary>
        private static int checkMoneyAlertMode = -1;

        /// <summary>
        /// �жϾ�����
        /// </summary>
        /// <param name="paitentInfo"></param>
        /// <param name="messType"></param>
        /// <returns></returns>
        public static int CheckMoneyAlertForAdd(FS.HISFC.Models.RADT.PatientInfo patientInfo, MessType messType)
        {

            return 1;
        }

        /// <summary>
        /// �жϾ�����
        /// </summary>
        /// <param name="paitentInfo"></param>
        /// <param name="alOrders"></param>
        /// <param name="messType"></param>
        /// <returns></returns>
        public static int CheckMoneyAlert(FS.HISFC.Models.RADT.PatientInfo patientInfo, ArrayList alOrders, MessType messType)
        {
            //ʵʱ��ȡ���µľ����ߡ�������Ϣ
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
                    ShowBalloonTip(2, "������ʾ", CacheManager.FeeIntegrate.Err, ToolTipIcon.Warning);
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


            //��ʽ0�����������;������ж�
            if (checkMoneyAlertMode == 0)
            {
                if (CacheManager.FeeIntegrate.IsPatientLackFee(patientInfo, 0))
                {
                    if (messType == MessType.Y)
                    {
                        MessageBox.Show("���ߡ�" + patientInfo.Name + "���Ѿ�Ƿ�ѣ����ܼ���������\r\n\r\n\r\n" + "������ " + patientInfo.FT.LeftCost.ToString() + " С�ڹ涨��" + moneyAlert.ToString() + "Ԫ\r\n\r\n�����շ��Էѽ� " + totCost.ToString() + "\r\n\r\n", "����", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Stop);
                        return -1;
                    }
                    else if (messType == MessType.M)
                    {
                        if (MessageBox.Show("���ߡ�" + patientInfo.Name + "���Ѿ�Ƿ�ѣ��Ƿ����������\r\n\r\n\r\n" + "������ " + patientInfo.FT.LeftCost.ToString() + " С�ڹ涨��" + moneyAlert.ToString() + "Ԫ\r\n\r\n�����շ��Էѽ� " + totCost.ToString() + "\r\n\r\n", "ѯ��", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == DialogResult.No)
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
            //1 �����շѺ�������ж�
            else if (checkMoneyAlertMode == 1)
            {
                if (CacheManager.FeeIntegrate.IsPatientLackFee(patientInfo, totCost))
                {
                    if (messType == MessType.Y)
                    {
                        MessageBox.Show("���ߡ�" + patientInfo.Name + "���Ѿ�Ƿ�ѣ����ܼ���������\r\n\r\n\r\n�Էѽ� " + totCost.ToString() + "\r\n\r\n�����ߣ� " + patientInfo.PVisit.MoneyAlert.ToString() + "\r\n\r\n�� " + patientInfo.FT.LeftCost.ToString() + "\r\n\r\n�շѺ��� " + (patientInfo.FT.LeftCost - totCost).ToString() + " С�ڹ涨��50Ԫ\r\n\r\n", "����", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Stop);
                        return -1;
                    }
                    else if (messType == MessType.M)
                    {
                        if (MessageBox.Show("���ߡ�" + patientInfo.Name + "���Ѿ�Ƿ�ѣ��Ƿ����������\r\n\r\n\r\n�Էѽ� " + totCost.ToString() + "\r\n\r\n�����ߣ� " + patientInfo.PVisit.MoneyAlert.ToString() + "\r\n\r\n�� " + patientInfo.FT.LeftCost.ToString() + "\r\n\r\n�շѺ��� " + (patientInfo.FT.LeftCost - totCost).ToString() + " С�ڹ涨��50Ԫ\r\n\r\n", "ѯ��", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == DialogResult.No)
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

        #region ���ó���
        private static FS.FrameWork.Public.ObjectHelper helpUsage = null;

        /// <summary>
        /// �÷�
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
        /// ��ȡ��ҩ�÷�
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
                //�ж���H��ͷ�Ķ�Ϊ��ҩ�÷�
                if (!usage.UserCode.Equals(null) && usage.UserCode.StartsWith("H"))
                {
                    herbalUsageHelper.ArrayObject.Add(usage);
                }
            }
            return herbalUsageHelper;
        }

        //private static FS.FrameWork.Public.ObjectHelper helpFrequency = null;

        /// <summary>
        /// Ƶ��
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

        #region ���������ͼ�鲿λ{0A4BC81A-2F2B-4dae-A8E6-C8DC1F87AA32}

        private static FS.FrameWork.Public.ObjectHelper helpSample = null;
        /// <summary>
        /// ����
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
        /// ��鲿λ
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

        #region "�Ƿ�Ĭ�Ͽ���ҽ��ʱ��" (������)

        //protected static bool bIsDefaultMoDate = false;
        //protected static bool bFirst = true;//������

        ///// <summary>
        ///// �Ƿ�Ĭ�Ͽ���ҽ��ʱ��
        ///// </summary>
        //public static bool IsDefaultMoDate
        //{
        //    get
        //    {
        //        if (bFirst)
        //        {
        //            try//����Ƿ��޸� ����ʱ�����������200012
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

        #region Ĭ�Ͽ���ʱ��

        /// <summary>
        /// Ĭ�Ͽ�ʼʱ��ģʽ ��λ���ֱ�ʾ����1λΪ������2λΪ������3λΪ���� ���ƣ�100
        /// 0 ��ǰʱ�䣻1 Ĭ������ҽ��ʱ�䣻2 Ĭ�ϵ����賿��3 Ĭ�ϵ������磻4 Ĭ�ϵ�������
        /// </summary>
        static string defaultMoDateMode;

        static FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();

        /// <summary>
        /// ��ȡĬ��ҽ����ʼʱ��
        /// </summary>
        /// <param name="orderType">0 ���ﴦ����1 ����ҽ����2 ��ʱҽ��</param>
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
                    case 0: //��ǰʱ��
                        return dtNow;
                        break;
                    case 1: //Ĭ������ҽ��ʱ��
                        return DateTime.MinValue;
                        break;
                    case 2: //Ĭ�ϵ����賿
                        return new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 0, 0, 0);
                        break;
                    case 3: //Ĭ�ϵ�������
                        return new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 12, 0, 0);
                        break;
                    case 4: //Ĭ�ϵ�������
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

        #region ��ȡĬ��������


        /// <summary>
        /// Ĭ����������ʾ 0 �̶�Ϊ1��1 ���ݿ���ʱ���Զ����㣻2 ���Ƶ������3 �̶�Ϊ0;4 Ĭ��Ϊ��
        /// </summary>
        private static int firstOrderDaysMode = -1;

        /// <summary>
        /// ���ݿ�ʼʱ�䡢Ƶ�Σ���ȡĬ��������
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

            //����Ҫ��������������Ĭ��Ϊ0

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

        #region �¿�ҽ��Ĭ����Ч������� (������)

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

        #region ���ҽ�� ����Ķ���column �����Ŀ��
        /// <summary>
        /// �������ұ�
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
                            #region "��"
                            if (o.Cells[i, column].Text == "0") o.Cells[i, column].Text = "";
                            tmp = o.Cells[i, column].Text + "";
                            o.Cells[i, column].Tag = tmp;
                            if (curComboNo != tmp && tmp != "") //��ͷ
                            {
                                curComboNo = tmp;
                                o.Cells[i, DrawColumn].Text = "��";
                                try
                                {
                                    if (o.Cells[i - 1, DrawColumn].Text == "��")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "��";
                                    }
                                    else if (o.Cells[i - 1, DrawColumn].Text == "��")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "";
                                    }
                                }
                                catch { }
                            }
                            else if (curComboNo == tmp && tmp != "")
                            {
                                o.Cells[i, DrawColumn].Text = "��";
                            }
                            else if (curComboNo != tmp && tmp == "")
                            {
                                try
                                {
                                    if (o.Cells[i - 1, DrawColumn].Text == "��")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "��";
                                    }
                                    else if (o.Cells[i - 1, DrawColumn].Text == "��")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "";
                                    }
                                }
                                catch { }
                                o.Cells[i, DrawColumn].Text = "";
                                curComboNo = "";
                            }
                            if (i == o.RowCount - 1 && o.Cells[i, DrawColumn].Text == "��") o.Cells[i, DrawColumn].Text = "��";
                            if (i == o.RowCount - 1 && o.Cells[i, DrawColumn].Text == "��") o.Cells[i, DrawColumn].Text = "";
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
                                #region "��"
                                if (c.Cells[j, column].Text == "0") c.Cells[j, column].Text = "";
                                tmp = c.Cells[j, column].Text + "";

                                c.Cells[j, column].Tag = tmp;
                                if (curComboNo != tmp && tmp != "") //��ͷ
                                {
                                    curComboNo = tmp;
                                    c.Cells[j, DrawColumn].Text = "��";
                                    try
                                    {
                                        if (c.Cells[j - 1, DrawColumn].Text == "��")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "��";
                                        }
                                        else if (c.Cells[j - 1, DrawColumn].Text == "��")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "";
                                        }
                                    }
                                    catch { }
                                }
                                else if (curComboNo == tmp && tmp != "")
                                {
                                    c.Cells[j, DrawColumn].Text = "��";
                                }
                                else if (curComboNo != tmp && tmp == "")
                                {
                                    try
                                    {
                                        if (c.Cells[j - 1, DrawColumn].Text == "��")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "��";
                                        }
                                        else if (c.Cells[j - 1, DrawColumn].Text == "��")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "";
                                        }
                                    }
                                    catch { }
                                    c.Cells[j, DrawColumn].Text = "";
                                    curComboNo = "";
                                }
                                if (j == c.RowCount - 1 && c.Cells[j, DrawColumn].Text == "��") c.Cells[j, DrawColumn].Text = "��";
                                if (j == c.RowCount - 1 && c.Cells[j, DrawColumn].Text == "��") c.Cells[j, DrawColumn].Text = "";
                                //c.Cells[j, DrawColumn].ForeColor = System.Drawing.Color.Red;
                                #endregion

                            }
                        }
                    }
                    break;
            }

        }
        /// <summary>
        /// ���������
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
                            #region "��"
                            if (o.Cells[i, column].Text == "0") o.Cells[i, column].Text = "";
                            tmp = o.Cells[i, column].Text + "";
                            o.Cells[i, column].Tag = tmp;
                            if (curComboNo != tmp && tmp != "") //��ͷ
                            {
                                curComboNo = tmp;
                                o.Cells[i, DrawColumn].Text = "��";
                                try
                                {
                                    if (o.Cells[i - 1, DrawColumn].Text == "��")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "��";
                                    }
                                    else if (o.Cells[i - 1, DrawColumn].Text == "��")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "";
                                    }
                                }
                                catch { }
                            }
                            else if (curComboNo == tmp && tmp != "")
                            {
                                o.Cells[i, DrawColumn].Text = "��";
                            }
                            else if (curComboNo != tmp && tmp == "")
                            {
                                try
                                {
                                    if (o.Cells[i - 1, DrawColumn].Text == "��")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "��";
                                    }
                                    else if (o.Cells[i - 1, DrawColumn].Text == "��")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "";
                                    }
                                }
                                catch { }
                                o.Cells[i, DrawColumn].Text = "";
                                curComboNo = "";
                            }
                            if (i == o.RowCount - 1 && o.Cells[i, DrawColumn].Text == "��") o.Cells[i, DrawColumn].Text = "��";
                            if (i == o.RowCount - 1 && o.Cells[i, DrawColumn].Text == "��") o.Cells[i, DrawColumn].Text = "";
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
                                #region "��"
                                if (c.Cells[j, column].Text == "0") c.Cells[j, column].Text = "";
                                tmp = c.Cells[j, column].Text + "";

                                c.Cells[j, column].Tag = tmp;
                                if (curComboNo != tmp && tmp != "") //��ͷ
                                {
                                    curComboNo = tmp;
                                    c.Cells[j, DrawColumn].Text = "��";
                                    try
                                    {
                                        if (c.Cells[j - 1, DrawColumn].Text == "��")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "��";
                                        }
                                        else if (c.Cells[j - 1, DrawColumn].Text == "��")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "";
                                        }
                                    }
                                    catch { }
                                }
                                else if (curComboNo == tmp && tmp != "")
                                {
                                    c.Cells[j, DrawColumn].Text = "��";
                                }
                                else if (curComboNo != tmp && tmp == "")
                                {
                                    try
                                    {
                                        if (c.Cells[j - 1, DrawColumn].Text == "��")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "��";
                                        }
                                        else if (c.Cells[j - 1, DrawColumn].Text == "��")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "";
                                        }
                                    }
                                    catch { }
                                    c.Cells[j, DrawColumn].Text = "";
                                    curComboNo = "";
                                }
                                if (j == c.RowCount - 1 && c.Cells[j, DrawColumn].Text == "��") c.Cells[j, DrawColumn].Text = "��";
                                if (j == c.RowCount - 1 && c.Cells[j, DrawColumn].Text == "��") c.Cells[j, DrawColumn].Text = "";
                                c.Cells[j, DrawColumn].ForeColor = System.Drawing.Color.Red;
                                #endregion

                            }
                        }
                    }
                    break;
            }

        }
        /// <summary>
        /// ����Ϻ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="column"></param>
        /// 		/// <param name="DrawColumn"></param>
        public static void DrawCombo(object sender, int column, int DrawColumn)
        {
            DrawCombo(sender, column, DrawColumn, 0);
        }
        /// <summary>
        /// ����Ϻ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="column"></param>
        /// 		/// <param name="DrawColumn"></param>
        public static void DrawComboLeft(object sender, int column, int DrawColumn)
        {
            DrawComboLeft(sender, column, DrawColumn, 0);
        }

        #endregion

        #region ����Ƿ���Կ����Ϊ���ҩƷ
        /// <summary>
        /// ����Ƿ���Կ����Ϊ���ҩƷ
        /// </summary>
        /// <returns></returns>
        public static int GetIsOrderCanNoStock()
        {
            FS.FrameWork.Management.ControlParam controler = new FS.FrameWork.Management.ControlParam();
            return FS.FrameWork.Function.NConvert.ToInt32(controler.QueryControlerInfo("200001"));

        }
        #endregion

        #region �����

        /// <summary>
        /// �����
        /// </summary>
        /// <param name="iCheck"></param>
        /// <param name="itemID"></param>
        /// <param name="itemName"></param>
        /// <param name="deptCode"></param>
        /// <param name="qty"></param>
        /// <param name="sendType">�������� A:ȫ����O:���I:סԺ</param>
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
                    //houwb 2011-5-30 ���ӷ��������ж�
                    //phaItem = CacheManager.PhaIntegrate.GetItemForInpatient(deptCode, itemID);
                    phaItem = CacheManager.PhaIntegrate.GetItemStorage(deptCode, sendType, itemID);

                    if (phaItem == null) return true;
                    if (qty > FS.FrameWork.Function.NConvert.ToDecimal(phaItem.StoreQty))
                    {
                        return false;
                    }
                    break;
                case 1:
                    //houwb 2011-5-30 ���ӷ��������ж�
                    //phaItem = CacheManager.PhaIntegrate.GetItemForInpatient(deptCode, itemID);
                    phaItem = CacheManager.PhaIntegrate.GetItemStorage(deptCode, sendType, itemID);


                    if (phaItem == null) return true;
                    if (qty > FS.FrameWork.Function.NConvert.ToDecimal(phaItem.StoreQty))
                    {
                        if (MessageBox.Show("ҩƷ��" + itemName + "���Ŀ�治�����Ƿ����ִ�У�", "��ʾ��治��", MessageBoxButtons.OKCancel) == DialogResult.OK)
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

        #region ���з���
        /// <summary>
        /// �Ƿ��з��͹�
        /// </summary>
        /// <param name="DeptCode">���ұ���</param>
        /// <returns>���ؿ�����չʵ��</returns>
        public static FS.HISFC.Models.Base.ExtendInfo IsDeptHaveDruged(string DeptCode)
        {
            FS.FrameWork.Management.ExtendParam m = new FS.FrameWork.Management.ExtendParam();
            FS.HISFC.Models.Base.ExtendInfo obj = m.GetComExtInfo(FS.HISFC.Models.Base.EnumExtendClass.DEPT, "ORDER_ISDRUGED", DeptCode);
            if (obj == null) return null;
            return obj;
        }
        /// <summary>
        /// �Ѿ����з���
        /// </summary>
        /// <param name="DeptCode"></param>
        /// <returns></returns>
        public static int HaveDruged(string DeptCode)
        {
            return Function.HaveDruged(DeptCode, 1M);
        }
        /// <summary>
        /// ����û���з���
        /// </summary>
        /// <param name="DeptCode"></param>
        /// <returns></returns>
        public static int NotHaveDruged(string DeptCode)
        {
            return Function.HaveDruged(DeptCode, 0M);
        }
        /// <summary>
        /// ������չ��Ϣ��
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
            obj.Name = "סԺ���Ҽ��а�ҩ";
            obj.PropertyCode = "ORDER_ISDRUGED";
            obj.PropertyName = "סԺ���Ҽ��а�ҩ";
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

        #region "ҽ��Ĭ��Ƶ��"
        /// <summary>
        /// ����ҽ��Ĭ��Ƶ��
        /// </summary>
        /// <param name="o"></param>
        public static void SetDefaultFrequency(FS.HISFC.Models.Order.Inpatient.Order o)
        {
            //����ҽ������⣬�˴����ٸı�Ƶ�Σ������Ҫ�ı�Ƶ������Ϊ�޸�

            //ҩƷ ��ʱҽ����Ƶ��Ϊ�գ�Ĭ��Ϊ��Ҫʱ�����prn
            //if (o.Item.ItemType == EnumItemType.Drug && o.OrderType.IsDecompose == false)
            //{
            //    o.Frequency.ID = "QD";//ҩƷ��ʱҽ��Ĭ��Ϊ��Ҫʱִ��
            //    o.Frequency = helpFrequency.GetObjectFromID(o.Frequency.ID) as FS.HISFC.Models.Order.Frequency;
            //}
            //else if (o.Item.ItemType != EnumItemType.Drug && o.OrderType.IsDecompose == false)
            //{
            //    o.Frequency.ID = "QD";//��ҩƷ��ʱҽ��Ĭ��Ϊÿ��һ��
            //    o.Frequency = helpFrequency.GetObjectFromID(o.Frequency.ID) as FS.HISFC.Models.Order.Frequency;
            //}
        }
        #endregion

        #region ����״̬�ж�

        /// <summary>
        /// �жϻ����Ƿ������ˡ��ֽ�ҽ����״̬
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="p"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public static int CheckPatientState(string patientID, ref FS.HISFC.Models.RADT.PatientInfo p, ref string errInfo)
        {
            string memo = "";

            //�ж��� ת�Ƶ������
            string dept = "";

            if (p != null)
            {
                memo = p.Memo;
                //�ж��� ת�Ƶ������
                dept = p.PVisit.PatientLocation.Dept.ID;
            }

            p = CacheManager.RadtIntegrate.GetPatientInfomation(patientID);

            if (p == null)
            {
                errInfo = "��û��߻�����Ϣ����" + CacheManager.RadtIntegrate.Err;
                return -1;
            }

            p.Memo = memo;
            if (p.PVisit.InState.ID.ToString() == "O" || //��Ժ����
                p.PVisit.InState.ID.ToString() == "B" || //��Ժ�Ǽ�
                p.PVisit.InState.ID.ToString() == "P" || //ԤԼ��Ժ
                p.PVisit.InState.ID.ToString() == "N")   //�޷���Ժ
            {
                errInfo = "����" + p.Name + "������Ժ״̬,�޷��������в�����";
                p = null;
                return -1;
            }

            //if (!string.IsNullOrEmpty(dept) && p.PVisit.PatientLocation.Dept.ID != dept)
            //{
            //    errInfo = "����" + p.Name + "�Ѿ�ת�ƣ��޷��������в�����";
            //    return -1;
            //}
            return 1;
        }

        #endregion

        #region ��λ����
        /// <summary>
        /// ��ʾ��λ��
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

        #region ҽ��״̬

        /// <summary>
        /// תҽ��״̬
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string OrderStatus(int i)
        {
            switch (i)
            {
                case 0:
                    return "�¿���";
                case 1:
                    return "�����";
                case 2:
                    return "ִ��";
                case 3:
                    return "ֹͣ/ȡ��";
                case 4:
                    return "ֹͣ/ȡ��";
                default:
                    return "δ֪";
            }
        }
        #endregion

        #region ����ҽ������
        /// <summary>
        /// ����ҽ�����ͣ���������ҽ��������ҽ�����������͵�
        /// </summary>
        /// <param name="info"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public static string MakeSpeDrugType(FS.HISFC.Models.RADT.PatientInfo info, FS.HISFC.Models.Order.Inpatient.Order order)
        {
            string speDrugType = "";

            if (order.SpeOrderType.Length <= 0)
            {
                if (info.Memo == "����")
                {
                    speDrugType = "CONS";
                }
                else if (info.Memo == "����")
                {
                    speDrugType = "DEPT" + order.ExeDept.ID;
                }
                else if (info.Memo == "ҽ��")
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
        /// ���ȱҩ��ͣ��
        /// </summary>
        /// <param name="drugDept">�ۿ����</param>
        /// <param name="item">��Ŀ</param>
        /// <param name="IsOutPatient">�Ƿ����￪��</param>
        /// <param name="drugItem">���ص���Ŀ��Ϣ</param>
        /// <param name="errInfo">������Ϣ</param>
        /// <returns></returns>
        //[Obsolete("���ϣ��Ƶ�FS.SOC.HISFC.BizProcess.OrderIntegrate.OrderBase����", true)]
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
            MessageBox.Show(errInfo, "��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #region ҽ��վȨ���ж�

        private static FS.HISFC.BizLogic.Order.Medical.Ability docAbility = new FS.HISFC.BizLogic.Order.Medical.Ability();

        private static FS.HISFC.BizLogic.Manager.Person personMgr = new FS.HISFC.BizLogic.Manager.Person();

        private static FS.HISFC.BizLogic.Pharmacy.Item myPha = new FS.HISFC.BizLogic.Pharmacy.Item();

        private static FS.HISFC.BizLogic.Pharmacy.Constant myPhaCons = new FS.HISFC.BizLogic.Pharmacy.Constant();

        private static ArrayList alDrugGrade = null;

        private static ArrayList alDrugPosition = null;

        private static ArrayList alSpeDrugs = null;

        private static List<FS.HISFC.Models.Order.Medical.Popedom> alAllPopom = null;

        /// <summary>
        /// �Ƿ���ƴ���Ȩ
        /// </summary>
        private static int isUseControl = -1;

        /// <summary>
        /// ҩƷ������Ϣ
        /// </summary>
        private static FS.HISFC.Models.Pharmacy.Item phaItem = null;

        /// <summary>
        /// ҩƷ�б�
        /// </summary>
        private static Hashtable hsPhaItem = new Hashtable();

        /// <summary>
        /// ҽ��վȨ���жϣ�������Ȩ�������޸�Ȩ���ȼ�ҩ�￪��Ȩ������ҩ�￪��Ȩ����ҩƷ��Ŀ����Ȩ
        /// </summary>
        /// <param name="order"></param>
        /// <param name="doctor"></param>
        /// <param name="dept"></param>
        /// <param name="priv"></param>
        /// <param name="outpatient"></param>
        /// <param name="error"></param>
        /// <returns>1 ��Ȩ�ޣ�0 ��Ȩ�ޡ��ɲ�����-1 ��Ȩ�ޡ����ɲ���</returns>
        [Obsolete("���ϣ��Ƶ�FS.SOC.HISFC.BizProcess.OrderIntegrate.OrderBase����", true)]
        public static int JudgeEmplPriv(FS.HISFC.Models.Order.Order order, FS.FrameWork.Models.NeuObject doctor,
            FS.FrameWork.Models.NeuObject dept, FS.HISFC.Models.Base.DoctorPrivType priv,
            bool outpatient, ref string error)
        {
            int rev = 1;

            if (order.Item.ID == "999")
                return 1;

            //����ϵͳ�����޴���Ȩ��ҽ��������ҩƷ��ò��û���ҵ�����ļ�����������ҩƷ

            //1������Ȩ�����ж��ж�
            //2������Ȩ�����ﲻ��������סԺ��������״̬Ϊ��Ч
            //3���ȼ�ҩƷ�����ﲻ��������סԺ��������״̬Ϊ��Ч
            //4������ҩ�����סԺ����������
            //5�����޷�ҩƷ�����סԺ����������

            #region  �����޸�Ȩ
            if (priv == DoctorPrivType.GroupManager)
            {
                #region �����޸��ж�
                string sysClass = "groupManager";

                rev = docAbility.CheckPopedom(doctor.ID, order.Item.ID, sysClass, false, ref error);

                return rev;
                #endregion
            }
            #endregion

            else
            {
                #region ����Ȩ

                //�Ƿ����ÿ���Ȩ�޿���
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
                    #region ����Ȩ
                    rev = docAbility.CheckPopedom(doctor.ID, order.Item.ID, order.Item.SysClass.ID.ToString(), false, ref error);

                    if (rev < 0)
                    {
                        return rev;
                    }
                    #endregion
                }
                #endregion

                #region ҩƷ�ȼ��������ƣ������ؼ���

                #region ҩƷ�ȼ�����

                //��ǰҽ��ӵ�е�Ȩ��
                int drugPermission = 0;

                //�Ƿ����Ȩ��
                bool isControlDrugPermission = false;

                FS.HISFC.Models.Base.Employee doctObj = null;

                //�ж϶�������(ְ��<-->ҩƷ�ȼ�  ְ��<-->ҩƷ�ȼ�)
                doctObj = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeInfo(doctor.ID);

                if (doctObj == null)
                {
                    MessageBox.Show("��ȡҽ����Ϣ���� ���š�" + doctor.ID + "��", "����", MessageBoxButtons.OK);
                    return -1;
                }

                #region ��ȡ����
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

                #region ���к˶�

                if ((alDrugGrade != null && alDrugGrade.Count > 0) || alDrugPosition != null && alDrugPosition.Count > 0)
                {
                    isControlDrugPermission = true;
                }

                if (isControlDrugPermission)
                {
                    //�����أ�ҩƷ 
                    if (order.Item.ItemType == EnumItemType.Drug)
                    {
                        phaItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID);

                        if (phaItem == null)
                        {
                            error = "��ȡҩƷ��Ϣʧ�ܣ�" + myPha.Err;
                            return -1;
                        }

                        order.Item.Grade = phaItem.Grade;

                        if (!string.IsNullOrEmpty(order.Item.Grade))
                        {
                            if (outpatient)
                            {
                                if (FS.FrameWork.Function.NConvert.ToInt32(order.Item.Grade) > drugPermission)
                                {
                                    error = "����ְ��(ְ��)û�п�����ҩƷ��" + order.Item.Name + "����Ȩ�ޣ�";
                                    rev = 0;
                                }
                            }
                            else
                            {
                                try
                                {
                                    //ҩƷ�ȼ�����ҽ���Ŀ����صȼ���A��B��C���Ƚ�
                                    //ҩƷ�ȼ��ȽϸߵĻ� ��û��ҽ������Ȩ��
                                    if (FS.FrameWork.Function.NConvert.ToInt32(order.Item.Grade) > drugPermission)
                                    {
                                        error = "����ְ��(ְ��)û�п�����ҩƷ��" + order.Item.Name + "����Ȩ�ޣ�\r\nϵͳĬ�ϸ�ҽ��Ϊ��Ч״̬�����ϼ�ҽ����˺������Ч��";
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

                #region ����ҩ�￪��Ȩ

                //ĿǰҩƷ���滹û������˹��ܣ����Ǿ�û�б�Ҫ�Ҹ��Ҹ���
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

                        //����ҩ��ô������ô���洢��
                        //pha_com_spedrug_per_dep
                        //pha_com_spedrug_maintenance
                        //pha_com_spedrug


                        foreach (Employee emp in alSpeDrugs)
                        {
                            //ID��ҽ������ұ���;User01����Ŀ����
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
                            error = "��ҩƷΪ����ҩƷ,\r\n��û�п���ҩƷ��" + order.Item.Name + "����Ȩ�ޣ�";
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

        #region ����ʷ�ж�

        private static FS.HISFC.BizLogic.Order.Medical.AllergyManager allergyManager = new FS.HISFC.BizLogic.Order.Medical.AllergyManager();

        public static Hashtable HsAllItems = new Hashtable();


        /// <summary>
        /// ����ʷ�ж�
        /// </summary>
        /// <param name="patientType 1���� 2סԺ"></param>
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
                MessageBox.Show("��ȡƤ����Ϣʧ�ܣ�" + allergyManager.Err);
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
                            MessageBox.Show("��ȡҩƷ��Ϣʧ�ܣ�" + CacheManager.PhaIntegrate.Err);
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
                            MessageBox.Show("��ȡҩƷ��Ϣʧ�ܣ�" + CacheManager.PhaIntegrate.Err);
                        }
                        HsAllItems.Add(algItem.ID, algItem);
                    }

                    if ((newItem != null && algItem != null
                        && newItem.PhyFunction3.ID == algItem.PhyFunction3.ID)
                       || order.Item.ID == info.Allergen.ID)
                    {
                        if (MessageBox.Show("�¿���ҩ�" + order.Item.Name + "���뻼����ʷ����ҩ�" + algItem.Name + "����ͬ����Ƿ������", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
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
        /// �ж����Լ������
        /// </summary>
        /// <param name="orderFrom"></param>
        /// <param name="orderTo"></param>
        /// <param name="isNew">�ǲ�����ҽ���� ��ҽ��������Ϣ���Բ��ж�</param>
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
                MessageBox.Show(orderTo.Item.Name + "ҽ���Ѿ���ӡ������������ã�");
                return -1;

            }

            if (orderTo.Status != 0 && orderTo.Status != 5)
            {
                MessageBox.Show(orderTo.Item.Name + "�����¿���ҽ��������������ã�");
                return -1;

            }


            bool isDecSysClassWhenGetRecipeNO = FS.FrameWork.Function.NConvert.ToBoolean(FS.HISFC.Components.Order.OutPatient.Classes.Function.GetBatchControlParam("MZ0073", false, "0"));

            if (isDecSysClassWhenGetRecipeNO)
            {
                if ("PCZ,P".Contains(orderFrom.Item.SysClass.ID.ToString()) &&
                    "PCZ,P".Contains(orderTo.Item.SysClass.ID.ToString()))
                {
                    //��ҩ�ͳ�ҩ�������
                }
                else
                {
                    if (orderFrom.Item.SysClass.ID.ToString() != orderTo.Item.SysClass.ID.ToString())
                    {
                        MessageBox.Show("ϵͳ���ͬ������������ã�");
                        return -1;
                    }
                }
            }
            else
            {
                if (orderFrom.Item.SysClass.ID.ToString() != orderTo.Item.SysClass.ID.ToString())
                {
                    MessageBox.Show("ϵͳ���ͬ������������ã�");
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
                        MessageBox.Show("ϵͳ���ͬ������������ã�");
                        return -1;
                    }
                    if (orderFrom.ExeDept.ID != orderTo.ExeDept.ID)
                    {
                        MessageBox.Show("ִ�п��Ҳ�ͬ���������ʹ��!", "��ʾ");
                        return -1;

                    }
                }

                return 1;
            }


            if (isNew && !string.IsNullOrEmpty(orderFrom.Frequency.ID) && orderFrom.Frequency.ID != "PRN")
            {
                if (orderFrom.Frequency.ID != orderTo.Frequency.ID)
                {
                    MessageBox.Show("Ƶ�β�ͬ������������ã�");
                    return -1;
                }
            }


            if (isNew && orderFrom.InjectCount > 0)
            {
                if (orderFrom.InjectCount != orderTo.InjectCount)
                {
                    MessageBox.Show("Ժע������ͬ������������ã�");
                    return -1;
                }
            }

            if (orderFrom.ExeDept.ID != orderTo.ExeDept.ID)
            {
                MessageBox.Show("ִ�п��Ҳ�ͬ���������ʹ��!", "��ʾ");
                return -1;
            }

            if (orderFrom.Item.ItemType == EnumItemType.Drug)		//ֻ��ҩƷ�ж��÷��Ƿ���ͬ
            {
                if (isNew && !string.IsNullOrEmpty(orderFrom.Usage.ID))
                {
                    #region �÷��ж�
                    if (orderFrom.Item.SysClass.ID.ToString() != "PCC")
                    {
                        if (!IsSameUsage(orderFrom.Usage.ID, orderTo.Usage.ID))
                        {
                            MessageBox.Show("�÷���ͬ�������Խ�����ϣ�");
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
                            MessageBox.Show("��ҩ������ͬ������������ã�");
                            return -1;
                        }
                    }
                }
            }
            else
            {
                if (orderFrom.Item.SysClass.ID.ToString() != orderTo.Item.SysClass.ID.ToString())
                {
                    MessageBox.Show("ϵͳ���ͬ������������ã�");
                    return -1;
                }
                if (orderFrom.Item.SysClass.ID.ToString() == "UL")//����
                {
                    if (isNew && orderFrom.Qty > 0)
                    {
                        if (orderFrom.Qty != orderTo.Qty)
                        {
                            MessageBox.Show("����������ͬ������������ã�");
                            return -1;
                        }
                    }

                    if (isNew && string.IsNullOrEmpty(orderFrom.Sample.Name))
                    {
                        if (orderFrom.Sample.Name != orderTo.Sample.Name)
                        {
                            MessageBox.Show("����������ͬ������������ã�");
                            return -1;
                        }
                    }
                }
            }
            return 1;
        }

        /// <summary>
        /// �÷��б�
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper usageHelper = null;

        /// <summary>
        /// �ж���ͬ�÷��Ƿ����ϵͳ��� 1 ����ϵͳ����жϣ��������ݱ����ж�
        /// </summary>
        public static int isJudgSameSysUsageBysSysCode = -1;

        /// <summary>
        /// �Ƿ���ͬ�÷�
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
                    //ϵͳ����ж��Ƿ�ͬһ����÷�
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

        #region ȡ���㷨

        /// <summary>
        /// �Ƿ��Ѿ���ѯȡ���ӿڣ�����ӿ�Ϊ�յĻ� ����Ĭ�Ϸ���
        /// </summary>
        private static bool isGetSplitInterface = false;

        /// <summary>
        /// ҽ��ȡ���ӿ�
        /// </summary>
        private static FS.SOC.HISFC.BizProcess.OrderInterface.Common.IOrderSplit iOrderSplit = null;

        /// <summary>
        /// ��ȡҩƷ������ȡ������
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
        /// ��ȡҩƷ��ȡ������
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

        //ע��סԺ������ȡ�����Ƿŵ����߿�����洦���

        /// <summary>
        /// ���¼�������(�˴�ֻ�Ǽ���סԺ���������ﴦ���������ļ����ڻ��߿���д���
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public static int ReComputeQty(FS.HISFC.Models.Order.Order orderBase)
        {
            //����û����ʱ��������
            if (orderBase.HerbalQty <= 0)
            {
                return 1;
            }

            if (orderBase.Item.ID != "999")
            {
                #region �ӿ�ȡ��

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
                    #region Ĭ��ȡ������
                    try
                    {
                        //��ҩ���㷽ʽ��һ��
                        if (orderBase.Item.ItemType == EnumItemType.Drug)
                        {
                            HISFC.Models.Pharmacy.Item phaItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(orderBase.Item.ID);

                            if (phaItem == null)
                            {
                                MessageBox.Show("����ҩƷ��Ŀʧ��");
                                return -1;
                            }

                            #region ��������

                            #region ����Ƶ��
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

                            #region ����ȡ������

                            //0 ��С��λ����ȡ��" ���ݿ�ֵ 0
                            //1 ��װ��λ����ȡ��" ���ݿ�ֵ 1  �ڷ��ر����г�ҩ��������ҩ�϶�
                            //2 ��С��λÿ��ȡ��" ���ݿ�ֵ 2  ����϶�����
                            //3 ��װ��λÿ��ȡ��" ���ݿ�ֵ 3  ����û����
                            //4 ��С��λ�ɲ�� ���������κ�ȡ��

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

                            //0 ��װ��λ��1 ��С��λ
                            string unitFlag = "";

                            //��ȡִ��Ƶ�Σ�ֻ��Ϊ����
                            decimal execQty = Math.Ceiling(frequence * orderBase.HerbalQty);

                            switch (splitType)
                            {
                                case "0":
                                    //��ҩ������ȡ��  houwb 2011-3-15{E0FCD746-5605-4318-860B-50467115BE6D}
                                    //��ҩ��������ȡ��������1.5g����1.5g
                                    //�����1��ÿ�μ�����λ6g��������һ��3g �Ͱ���0.5����ҩ
                                    //if (phaItem.SysClass.ID.ToString() == "PCC")
                                    //{
                                    //    orderBase.Qty = Math.Round(doseOnce * execQty / phaItem.BaseDose, 2);
                                    //    orderBase.Unit = phaItem.MinUnit;
                                    //    unitFlag = "1";//��С��λ
                                    //}
                                    //else
                                    //{
                                    //��ҩ�����������������ÿ������2/3Ƭ�ģ�
                                    // ���ڳ�����������������������ȡһ�� ��ȡ�� houwb
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
                                    //��ҩ������ȡ��  houwb 2011-3-15{E0FCD746-5605-4318-860B-50467115BE6D}
                                    //��ҩ��������ȡ��������1.5g����1.5g
                                    //�����1��ÿ�μ�����λ6g��������һ��3g �Ͱ���0.5����ҩ
                                    //if (phaItem.SysClass.ID.ToString() == "PCC")
                                    //{
                                    //    orderBase.Qty = Math.Round((doseOnce * execQty / phaItem.BaseDose) / phaItem.PackQty, 2);
                                    //    orderBase.Unit = phaItem.PackUnit;
                                    //    unitFlag = "0";//��װ��λ
                                    //}
                                    //else
                                    //{
                                    orderBase.Qty = Math.Ceiling((doseOnce * execQty / phaItem.BaseDose) / phaItem.PackQty);
                                    orderBase.Unit = phaItem.PackUnit;
                                    unitFlag = "0";
                                    //}
                                    break;
                                case "2":
                                    //��ҩ������ȡ��  houwb 2011-3-15{E0FCD746-5605-4318-860B-50467115BE6D}
                                    //��ҩ��������ȡ��������1.5g����1.5g
                                    //�����1��ÿ�μ�����λ6g��������һ��3g �Ͱ���0.5����ҩ
                                    //if (phaItem.SysClass.ID.ToString() == "PCC")
                                    //{
                                    //    orderBase.Qty = Math.Round(doseOnce * execQty / phaItem.BaseDose, 2);
                                    //    orderBase.Unit = phaItem.MinUnit;
                                    //    unitFlag = "1";//��С��λ
                                    //}
                                    //else
                                    //{
                                    orderBase.Qty = Math.Ceiling(Math.Round(Math.Ceiling(doseOnce / phaItem.BaseDose) * execQty, 6));
                                    orderBase.Unit = phaItem.MinUnit;
                                    unitFlag = "1";
                                    //}
                                    break;
                                case "3":
                                    //��ҩ������ȡ��  houwb 2011-3-15{E0FCD746-5605-4318-860B-50467115BE6D}
                                    //��ҩ��������ȡ��������1.5g����1.5g
                                    //�����1��ÿ�μ�����λ6g��������һ��3g �Ͱ���0.5����ҩ
                                    //if (phaItem.SysClass.ID.ToString() == "PCC")
                                    //{
                                    //    orderBase.Qty = Math.Round((doseOnce / phaItem.BaseDose * execQty) / phaItem.PackQty, 2);
                                    //    //orderBase.Unit = phaItem.MinUnit;
                                    //    orderBase.Unit = phaItem.PackUnit;
                                    //    unitFlag = "0";//��װ��λ
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
        /// ͨ�����׿���
        /// </summary>
        /// <param name="orderBase"></param>
        /// <param name="isGroup"></param>
        /// <returns></returns>
        public static int ReComputeQty(FS.HISFC.Models.Order.Order orderBase, bool isGroup)
        {

            //����û����ʱ��������
            if (orderBase.HerbalQty <= 0)
            {
                return 1;
            }

            if (orderBase.Item.ID != "999")
            {
                #region �ӿ�ȡ��

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
                    #region Ĭ��ȡ������
                    try
                    {
                        //��ҩ���㷽ʽ��һ��
                        if (orderBase.Item.ItemType == EnumItemType.Drug)
                        {
                            HISFC.Models.Pharmacy.Item phaItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(orderBase.Item.ID);

                            if (phaItem == null)
                            {
                                MessageBox.Show("����ҩƷ��Ŀʧ��");
                                return -1;
                            }

                            #region ��������

                            #region ����Ƶ��
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

                            #region ����ȡ������

                            //0 ��С��λ����ȡ��" ���ݿ�ֵ 0
                            //1 ��װ��λ����ȡ��" ���ݿ�ֵ 1  �ڷ��ر����г�ҩ��������ҩ�϶�
                            //2 ��С��λÿ��ȡ��" ���ݿ�ֵ 2  ����϶�����
                            //3 ��װ��λÿ��ȡ��" ���ݿ�ֵ 3  ����û����
                            //4 ��С��λ�ɲ�� ���������κ�ȡ��

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

                            //0 ��װ��λ��1 ��С��λ
                            string unitFlag = "";

                            //��ȡִ��Ƶ�Σ�ֻ��Ϊ����
                            decimal execQty = Math.Ceiling(frequence * orderBase.HerbalQty);

                            switch (splitType)
                            {
                                case "0":
                                    //��ҩ������ȡ��  houwb 2011-3-15{E0FCD746-5605-4318-860B-50467115BE6D}
                                    //��ҩ��������ȡ��������1.5g����1.5g
                                    //�����1��ÿ�μ�����λ6g��������һ��3g �Ͱ���0.5����ҩ
                                    //if (phaItem.SysClass.ID.ToString() == "PCC")
                                    //{
                                    //    orderBase.Qty = Math.Round(doseOnce * execQty / phaItem.BaseDose, 2);
                                    //    orderBase.Unit = phaItem.MinUnit;
                                    //    unitFlag = "1";//��С��λ
                                    //}
                                    //else
                                    //{
                                    //��ҩ�����������������ÿ������2/3Ƭ�ģ�
                                    // ���ڳ�����������������������ȡһ�� ��ȡ�� houwb
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
                                    //��ҩ������ȡ��  houwb 2011-3-15{E0FCD746-5605-4318-860B-50467115BE6D}
                                    //��ҩ��������ȡ��������1.5g����1.5g
                                    //�����1��ÿ�μ�����λ6g��������һ��3g �Ͱ���0.5����ҩ
                                    //if (phaItem.SysClass.ID.ToString() == "PCC")
                                    //{
                                    //    orderBase.Qty = Math.Round((doseOnce * execQty / phaItem.BaseDose) / phaItem.PackQty, 2);
                                    //    orderBase.Unit = phaItem.PackUnit;
                                    //    unitFlag = "0";//��װ��λ
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
                                    //��ҩ������ȡ��  houwb 2011-3-15{E0FCD746-5605-4318-860B-50467115BE6D}
                                    //��ҩ��������ȡ��������1.5g����1.5g
                                    //�����1��ÿ�μ�����λ6g��������һ��3g �Ͱ���0.5����ҩ
                                    //if (phaItem.SysClass.ID.ToString() == "PCC")
                                    //{
                                    //    orderBase.Qty = Math.Round(doseOnce * execQty / phaItem.BaseDose, 2);
                                    //    orderBase.Unit = phaItem.MinUnit;
                                    //    unitFlag = "1";//��С��λ
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
                                    //��ҩ������ȡ��  houwb 2011-3-15{E0FCD746-5605-4318-860B-50467115BE6D}
                                    //��ҩ��������ȡ��������1.5g����1.5g
                                    //�����1��ÿ�μ�����λ6g��������һ��3g �Ͱ���0.5����ҩ
                                    //if (phaItem.SysClass.ID.ToString() == "PCC")
                                    //{
                                    //    orderBase.Qty = Math.Round((doseOnce / phaItem.BaseDose * execQty) / phaItem.PackQty, 2);
                                    //    //orderBase.Unit = phaItem.MinUnit;
                                    //    orderBase.Unit = phaItem.PackUnit;
                                    //    unitFlag = "0";//��װ��λ
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
        /// ͼ��֪ͨ
        /// </summary>
        static System.Windows.Forms.NotifyIcon notify = null;

        /// <summary>
        /// ͼ��֪ͨ�¼�
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

        #region ��ѯ��Ŀҽ����������Ϣ

        /// <summary>
        /// ��ȡ��Ŀ�ȼ�
        /// </summary>
        /// <param name="itemGrade"></param>
        /// <returns></returns>
        public static string GetItemGrade(string itemGrade)
        {
            if (itemGrade == "1")
            {
                return "����";
            }
            else if (itemGrade == "2")
            {
                return "����";
            }
            else if (itemGrade == "3")
            {
                return "����";
            }
            return "";
        }

        /// <summary>
        /// ����ҽ��������Ϣ
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public static FS.HISFC.Models.SIInterface.Compare GetPactItem(FS.HISFC.Models.Order.Order order)
        {
            /*
             *  1���ԷѲ�����
                2��ҽ��
                       1�����Ӷ��ձ����
                3������
                       1��������Ŀ���Էѱ���ң����Է���Ŀ��
                       2��������Ŀ���Ը�����ң���������Ŀ��
                       2��������С���ô��Ը�����ң���������Ŀ��
                       3�������Һ�ͬ��λά���ı��������ѱ�����Ŀ��
                       
              ��ʾ�� 
               1���ȼ������ұ�������Ϊ�գ�
               2���������Ը�����
               3���Ƿ���������ֻ��Թ�ҽ��
             * */

            //if (SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(order.Patient.Pact.ID).PayKind.ID == "01")
            //{
            //    return null;
            //}

            //����ͼ��ѯ
            string sql = @"select t.�ȼ� ��Ŀ�ȼ�,
                                   t.�Ը����� �Ը�����,
                                   t.paykind_code ��ͬ��λ���,
                                   t.pact_code ��ͬ��λ,
                                   t.item_code ��Ŀ����,
                                   t.������,
                                   t.center_memo ҽ��������Ϣ,
                                   1 sort
                            from view_get_pactitem t
                            where (t.pact_code='{0}' or t.pact_code is null)
                            and t.paykind_code='{1}'
                            and t.item_code='{2}' 
                            
                            union all
                            select t.�ȼ� ��Ŀ�ȼ�,
                                   t.�Ը����� �Ը�����,
                                   t.paykind_code ��ͬ��λ���,
                                   t.pact_code ��ͬ��λ,
                                   t.item_code ��Ŀ����,
                                   t.������,
                                   t.center_memo ҽ��������Ϣ,
                                   2 sort
                            from view_get_pactitem t
                            where (t.pact_code='{0}' or t.pact_code is null)
                            and t.paykind_code='{1}'
                            and t.item_code='{3}' 
                            
                            union all
                            select t.�ȼ� ��Ŀ�ȼ�,
                                   t.�Ը����� �Ը�����,
                                   t.paykind_code ��ͬ��λ���,
                                   t.pact_code ��ͬ��λ,
                                   t.item_code ��Ŀ����,
                                   t.������,
                                   t.center_memo ҽ��������Ϣ,
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
                        compareObj.CenterFlag = drow[5].ToString(); //��������Ƿ���������Ŀ��ֻ��Թ�ҽ��
                        compareObj.Practicablesymptomdepiction = drow[6].ToString(); //ҽ��������ҩ��ʾ��Ϣ

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

        #region ��ѯҩƷ��չ��Ϣ����ҩ��

        /// <summary>
        /// ��ȡҩƷ��ҩ���
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
                ShowBalloonTip(2, "����", personMgr.Err, ToolTipIcon.Info);
                return "";
            }

            string ss = personMgr.ExecSqlReturnOne(string.Format(sql, phaItem.ID), "");

            return ss;
        }

        /// <summary>
        /// ��ȡ������ҩ��ʾ
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
                ShowBalloonTip(2, "����", personMgr.Err, ToolTipIcon.Info);
                return "";
            }

            string ss = personMgr.ExecSqlReturnOne(string.Format(sql, phaItem.ID), "");

            return ss;
        }

        #endregion

        /*
         * 
        
        /// <summary>
        /// �޸�ҽ�����������ӿ�
        /// </summary>
        private static FS.SOC.HISFC.BizProcess.OrderInterface.Common.IModifyOrder IModifyOrder = null;

        /// <summary>
        /// �Ƿ��ѯ�޸�ҽ���ӿ�
        /// </summary>
        private static bool isIModifyOrder = false;

        /// <summary>
        /// �޸�ҽ���ӿ�ʵ��
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
                        MessageBox.Show(IModifyOrder.ErrInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    return -1;
                }
            }
            return 1;
        }

        /// <summary>
        /// �޸�ҽ���ӿ�ʵ��
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
                        MessageBox.Show(IModifyOrder.ErrInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    return -1;
                }
            }
            return 1;
        }
         * */

        #region DLLǶ�׵���
        //{993C4984-D7C6-462c-A554-6BA7251E3D4B}

        /// <summary>
        /// ��¼�ɹ�������������ǿ����ģ�����ʾ�����Ҷ�Ӧ��ҽ��������Ϣ���ܣ��������Ϣ�ں�̨�������ҽ���������
        /// </summary>
        /// <param name="DOCTOR_ID"></param>
        /// <returns></returns>
        [DllImport("Queue.dll", EntryPoint = "Queue_Login", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Queue_Login(String DOCTOR_ID);

        /// <summary>
        /// �ɹ��������ǰ��Ӧ����������ҽ����ظ����Լ��Ŷ���Ϣ
        /// </summary>
        /// <returns></returns>
        [DllImport("Queue.dll", EntryPoint = "Queue_Logout", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Queue_Logout();

        /// <summary>
        /// ҽ�����й���
        /// </summary>
        /// <param name="CURR_ID">����ID---�����ӦV_CALL_REGISTER���ID�ֶ�</param>
        /// <param name="CURR_NAME">������Ա����</param>
        /// <param name="QUEUE_NUM">�������</param>
        /// <param name="NEXT_ID"></param>
        /// <param name="NEXT_NAME"></param>
        /// <param name="NEXT_QUEUE_NUM"></param>
        /// <returns></returns>
        [DllImport("Queue.dll", EntryPoint = "Queue_CallNext", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Queue_CallNext(String CURR_ID, String CURR_NAME, int QUEUE_NUM, String NEXT_ID, String NEXT_NAME, int NEXT_QUEUE_NUM);

        /// <summary>
        /// ҽ�����﹦��
        /// </summary>
        /// <param name="CURR_ID">Int Queue_CallEndcall(String CURR_ID)</param>
        /// <returns></returns>
        [DllImport("Queue.dll", EntryPoint = "Queue_CallEndcall", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Queue_CallEndcall(String CURR_ID);

        /// <summary>
        /// ���ź��б�ʶ
        /// </summary>
        /// <param name="CURR_ID">����ID---�����ӦV_CALL_REGISTER���ID�ֶ�</param>
        /// <param name="CURR_NAME">������Ա����</param>
        /// <param name="QUEUE_NUM">�������</param>
        /// <returns></returns>
        [DllImport("Queue.dll", EntryPoint = "Queue_NoCall", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Queue_NoCall(String CURR_ID, String CURR_NAME, int QUEUE_NUM);

        /// <summary>
        /// ȡ������
        /// </summary>
        /// <param name="CURR_ID">����ID---�����ӦV_CALL_REGISTER���ID�ֶ�</param>
        /// <returns></returns>
        [DllImport("Queue.dll", EntryPoint = "Queue_Cancel", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Queue_Cancel(String CURR_ID);

        /// <summary>
        /// ���´���
        /// </summary>
        /// <param name="CURR_ID">����ID---�����ӦV_CALL_REGISTER���ID�ֶ�</param>
        /// <returns></returns>
        [DllImport("Queue.dll", EntryPoint = "Queue_Update", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Queue_Update(String CURR_ID);

        /// <summary>
        /// ���������Ϣ
        /// </summary>
        /// <param name="CURR_ID">����ID---�����ӦV_CALL_REGISTER���ID�ֶ�</param>
        /// <param name="CURR_NAME">������Ա����</param>
        /// <param name="QUEUE_NUM">�������</param>
        /// <param name="DEPART_ID">���ұ���</param>
        /// <param name="DEPART_NAME">��������</param>
        /// <param name="DOCTOR_ID">ҽ������</param>
        /// <param name="DOCTOR_NAME">ҽ������</param>
        /// <returns></returns>
        [DllImport("Queue.dll", EntryPoint = "Queue_Insert", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Queue_Insert(String CURR_ID, String CURR_NAME, int QUEUE_NUM, String DEPART_ID, String DEPART_NAME, String DOCTOR_ID, String DOCTOR_NAME);

        /// <summary>
        /// ��ʾ��ǰ���ﻼ����Ϣ
        /// </summary>
        /// <param name="CURR_ID">����ID---�����ӦV_CALL_REGISTER���ID�ֶ�</param>
        /// <param name="CURR_NAME">������Ա����</param>
        /// <param name="QUEUE_NUM">�������</param>
        /// <returns></returns>
        [DllImport("Queue.dll", EntryPoint = "Queue_Show", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Queue_Show(String CURR_ID, String CURR_NAME, int QUEUE_NUM);


        #endregion

        #region �۵��Ŷӽк�webservice
        //{390EA9BE-1A9C-43da-B26B-08533FC00415}
        /// <summary>
        /// ��ҪWebService֧��Post����
        /// </summary>
        public static bool PostWebServiceByJson(String URL, String MethodName, Hashtable Pars, bool IsLoginOrOut)
        {
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(URL);
            request.Method = "POST";
            request.ContentType = "text/xml; charset=utf-8";

            // ƾ֤
            request.Credentials = System.Net.CredentialCache.DefaultCredentials;
            //��ʱʱ��
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
    /// ҽ����ѯ�󣬴�ӡ��ҩ���ӿ�liu.xq20071025
    /// </summary>
    public interface IOrderExeQuery
    {
        /// <summary>
        /// סԺ����ʵ��
        /// </summary>
        FS.HISFC.Models.RADT.PatientInfo patientInfoObj
        {
            set;
            get;
        }
        /// <summary>
        /// ��ֵ����
        /// </summary>
        /// <returns></returns>
        int SetValue(ArrayList alExeOrder);

        /// <summary>
        /// ��ӡ
        /// </summary>
        void Print();
    }

    /// <summary>
    /// ��־��¼
    /// </summary>
    public class LogManager
    {
        public static void Write(string logInfo)
        {

            //���Ŀ¼�Ƿ����
            if (System.IO.Directory.Exists(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "./Log/Order/InOrder") == false)
            {
                System.IO.Directory.CreateDirectory(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "./Log/Order/InOrder");
            }
            FS.HISFC.BizLogic.Manager.DataBase dbMgr = new FS.HISFC.BizLogic.Manager.DataBase();
            DateTime dtNow = dbMgr.GetDateTimeFromSysDateTime();

            //����һ�ܵ���־
            System.IO.File.Delete(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "./Log/Order/OutOrder/" + dtNow.AddDays(-7).ToString("yyyyMMdd") + ".LOG");
            string name = dtNow.ToString("yyyyMMdd");
            System.IO.StreamWriter w = new System.IO.StreamWriter(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "./Log/Order/OutOrder/" + name + ".LOG", true);
            w.WriteLine(dtNow.ToString("yyyy-MM-dd HH:mm:ss") + logInfo);
            w.Flush();
            w.Close();
        }
    }
}