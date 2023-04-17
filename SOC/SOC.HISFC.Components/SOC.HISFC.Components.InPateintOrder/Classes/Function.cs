using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using FS.HISFC.Models.Base;

namespace FS.SOC.HISFC.Components.InPateintOrder.Classes
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

        #region ҽ������
        /// <summary>
        /// ����ҽ���״�Ƶ����Ϣ
        /// </summary>
        /// <param name="order"></param>
        public static void SetDefaultOrderFrequency(FS.HISFC.Models.Order.Inpatient.Order order)
        {
            if (order.OrderType.IsDecompose || order.OrderType.ID == "CD" ||
                order.OrderType.ID == "QL")//Ĭ��Ϊ��Ŀ��Ƶ��
            {
                if (order.Item.GetType() == typeof(FS.HISFC.Models.Pharmacy.Item))
                {
                    order.Frequency = (order.Item as FS.HISFC.Models.Pharmacy.Item).Frequency.Clone();
                    order.Frequency.Time = "25:00";//Ĭ��Ϊ�����㣬��Ҫ����
                }
            }
            //else if (order.Item.IsPharmacy && order.OrderType.IsDecompose == false)//ҩƷ ��ʱҽ����Ƶ��Ϊ�գ�Ĭ��Ϊ��Ҫʱ�����prn
            else if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug && order.OrderType.IsDecompose == false)//ҩƷ ��ʱҽ����Ƶ��Ϊ�գ�Ĭ��Ϊ��Ҫʱ�����prn
            {
                order.Frequency.ID = GetDefaultFrequencyID();//ҩƷ��ʱҽ��Ĭ��Ϊ��Ҫʱִ��
            }
            //else if (order.Item.IsPharmacy == false && order.OrderType.IsDecompose == false)
            else if (order.Item.ItemType != EnumItemType.Drug && order.OrderType.IsDecompose == false)
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
                freqHelper.ArrayObject = GetAllFrequency();
            }

            return freqHelper;
        }

        /// <summary>
        /// ��ȡ����Ƶ��
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
                    MessageBox.Show("��ȡƵ����Ϣʧ�ܣ�" + managerIntegrate.Err);
                    return null;
                }
            }

            return alFrequency;
        }

        #endregion

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="order"></param>
        /// <returns>0 �ɹ� -1ʧ��</returns>
        [Obsolete("����", true)]
        public static int CalTotal(FS.HISFC.Models.Order.Inpatient.Order order, int days)
        {
            FS.HISFC.Models.Pharmacy.Item item = order.Item as FS.HISFC.Models.Pharmacy.Item;
            #region ���ʱ���
            if (order.Frequency.Usage.ID == "") order.Frequency.Usage = order.Usage.Clone();
            //***************���Ƶ��ʱ���(ÿ����ٴ�)******************
            if (days == 0) days = 1;
            #endregion
            if (item.OnceDose == 0M)//һ�μ���Ϊ�㣬Ĭ����ʾ��������
                order.Qty = order.Frequency.Times.Length * days;
            else
                order.Qty = item.OnceDose / item.BaseDose * order.Frequency.Times.Length * days;

            return 0;
        }

        /// <summary>
        /// ҽ�������б�
        /// </summary>
        /// <param name="isShort">�Ƿ���ʱҽ��</param>
        [Obsolete("���ϣ����������FS.SOC.HISFC.BizProcess.Cache.Order��GetOrderSysType����", true)]
        public static System.Collections.ArrayList OrderCatatagory(bool isShort)
        {
            System.Collections.ArrayList alAllType = FS.HISFC.Models.Base.SysClassEnumService.List();
            FS.FrameWork.Models.NeuObject objAll = new FS.FrameWork.Models.NeuObject();
            objAll.ID = "ALL";
            objAll.Name = "ȫ��";
            alAllType.Add(objAll);
            if (isShort)
                return alAllType;//��ʱҽ����ʾȫ��

            //����ҽ������Щ����
            System.Collections.ArrayList alOrderType = new ArrayList();
            foreach (FS.FrameWork.Models.NeuObject obj in alAllType)
            {
                switch (obj.ID)
                {
                    case "UO": //����
                    case "UC": //���
                    case "PCC": //�в�ҩ
                    case "MC": //����
                    case "MRB": //ת��
                    case "MRD": //ת��
                    case "MRH": //ԤԼ��Ժ
                    case "UL":  //����
                        break;
                    default:
                        alOrderType.Add(obj);
                        break;
                }
            }
            return alOrderType;
        }

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

        /// <summary>
        /// ��Ա������
        /// </summary>
        static FS.FrameWork.Public.ObjectHelper emplHelper = null;

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

                FS.HISFC.BizProcess.Integrate.Common.ControlParam controler = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                //�Ƿ���ƴ���Ȩ
                bool isUseControl = controler.GetControlParam<bool>("HNMET1", true, false);

                if (isUseControl)
                {
                    //��ô����ҩƷ�ȼ����жϴ���Ȩ�ˣ�����
                    #region ��ȡ����
                    if (alDrugGrade == null)
                    {
                        alDrugGrade = myConstant.GetAllList("SpeDrugGrade");
                    }

                    if (alDrugPosition == null)
                    {
                        alDrugPosition = myConstant.GetAllList("SpeDrugPosition");
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
        /// �жϾ�����
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
                    MessageBox.Show("���ߡ�" + patientInfo.Name + "���Ѿ�Ƿ�ѣ����ܼ���������\r\n\r\n\r\n�շѽ� " + totCost.ToString() + "\r\n\r\n�����ߣ� " + patientInfo.PVisit.MoneyAlert.ToString() + "\r\n\r\n�� " + patientInfo.FT.LeftCost.ToString() + "\r\n\r\n�շѺ��� " + (patientInfo.FT.LeftCost - totCost).ToString() + " С�ھ����� " + patientInfo.PVisit.MoneyAlert.ToString() + "\r\n\r\n", "����", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Stop);
                    return -1;
                }
                else if (messType == MessType.M)
                {
                    if (MessageBox.Show("���ߡ�" + patientInfo.Name + "���Ѿ�Ƿ�ѣ��Ƿ����������\r\n\r\n\r\n�շѽ� " + totCost.ToString() + "\r\n\r\n�����ߣ� " + patientInfo.PVisit.MoneyAlert.ToString() + "\r\n\r\n�� " + patientInfo.FT.LeftCost.ToString() + "\r\n\r\n�շѺ��� " + (patientInfo.FT.LeftCost - totCost).ToString() + " С�ھ����� " + patientInfo.PVisit.MoneyAlert.ToString() + "\r\n\r\n", "ѯ��", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == DialogResult.No)
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

        #region ���ó���

        private static FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
        private static FS.FrameWork.Public.ObjectHelper helpUsage = null;

        /// <summary>
        /// �÷�
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
                //�ж���H��ͷ�Ķ�Ϊ��ҩ�÷�
                if (!usage.UserCode.Equals(null) && usage.UserCode.StartsWith("H"))
                {
                    herbalUsageHelper.ArrayObject.Add(usage);
                }
            }
            return herbalUsageHelper;
        }

        private static FS.FrameWork.Public.ObjectHelper helpFrequency = null;
        /// <summary>
        /// Ƶ��
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
        /// ��鲿λ
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
        /// Ĭ����������ʾ 0 �̶�Ϊ1��1 ���ݿ���ʱ���Զ����㣻2 ���Ƶ����
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

        private static FS.HISFC.BizProcess.Integrate.Pharmacy phaManager = new FS.HISFC.BizProcess.Integrate.Pharmacy();

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
            //FS.HISFC.Manager.Item manager = new FS.HISFC.BizLogic.Pharmacy.Item();
            //FS.HISFC.Models.Pharmacy.item item = null;
            //.

            FS.HISFC.Models.Pharmacy.Storage phaItem = null;


            switch (iCheck)
            {
                case 0:
                    //houwb 2011-5-30 ���ӷ��������ж�
                    //phaItem = phaManager.GetItemForInpatient(deptCode, itemID);
                    phaItem = phaManager.GetItemStorage(deptCode, sendType, itemID);

                    if (phaItem == null) return true;
                    if (qty > FS.FrameWork.Function.NConvert.ToDecimal(phaItem.StoreQty))
                    {
                        return false;
                    }
                    break;
                case 1:
                    //houwb 2011-5-30 ���ӷ��������ж�
                    //phaItem = phaManager.GetItemForInpatient(deptCode, itemID);
                    phaItem = phaManager.GetItemStorage(deptCode, sendType, itemID);


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
            FS.HISFC.BizProcess.Integrate.RADT pManager = new FS.HISFC.BizProcess.Integrate.RADT();

            string memo = "";

            //�ж��� ת�Ƶ������
            string dept = "";

            if (p != null)
            {
                memo = p.Patient.Memo;
                //�ж��� ת�Ƶ������
                dept = p.PVisit.PatientLocation.Dept.ID;
            }

            p = pManager.GetPatientInfomation(patientID);

            p.Patient.Memo = memo;

            if (p == null)
            {
                errInfo = "��û��߻�����Ϣ����" + pManager.Err;
                p = null;
                return -1;
            }
            else if (p.PVisit.InState.ID.ToString() == "O" || //��Ժ����
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

        #region ��ȡ������Ϣ

        /// <summary>
        /// ���Ұ���ʵ��
        /// </summary>
        private static FS.FrameWork.Public.ObjectHelper deptHelper = null;

        /// <summary>
        /// ��ȡ����ʵ��
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
                if (info.Patient.Memo == "����")
                {
                    speDrugType = "CONS";
                }
                else if (info.Patient.Memo == "����")
                {
                    speDrugType = "DEPT" + order.ExeDept.ID;
                }
                else if (info.Patient.Memo == "ҽ��")
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

        private static FS.HISFC.BizLogic.Manager.Constant myConstant = new FS.HISFC.BizLogic.Manager.Constant();

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
                IModifyOrder = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.SOC.HISFC.Components.InPateintOrder.ucOrder), typeof(FS.SOC.HISFC.BizProcess.OrderInterface.Common.IModifyOrder)) as FS.SOC.HISFC.BizProcess.OrderInterface.Common.IModifyOrder;
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
            if (System.IO.Directory.Exists("./Log/Order/InOrder") == false)
            {
                System.IO.Directory.CreateDirectory("./Log/Order/InOrder");
            }
            //����һ�ܵ���־
            System.IO.File.Delete("./Log/Order/InOrder/" + DateTime.Now.AddDays(-7).ToString("yyyyMMdd") + ".LOG");
            string name = DateTime.Now.ToString("yyyyMMdd");
            System.IO.StreamWriter w = new System.IO.StreamWriter("./Log/Order/InOrder" + name + ".LOG", true);
            w.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + logInfo);
            w.Flush();
            w.Close();
        }
    }
}