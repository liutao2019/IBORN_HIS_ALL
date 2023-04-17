using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;

namespace FS.SOC.Local.OutpatientFee.FoSi.Forms
{
    public partial class ucCostDisplay : UserControl, FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientOtherInfomationRight
    {
        public ucCostDisplay()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ��Ŀ��Ϣ
        /// </summary>
        private DataSet dsItem = null;

        /// <summary>
        /// ���ô����ӿ�
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = null;

        /// <summary>
        /// ����ҩƷ����С�����б�
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper drugFeeCodeHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// �������ҵ���
        /// </summary>
        FS.HISFC.BizLogic.Fee.Outpatient outpatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();

        private FS.FrameWork.Public.ObjectHelper diagnoseTypeHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ���Ʋ���ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// ��Ʊ����
        /// </summary>
        protected string invoiceType = "0";

        /// <summary>
        /// ��ƱԤ������
        /// </summary>
        protected string invoicePreviewType = "0";

        /// <summary>
        /// ����ĳЩ��Ϣ,�����Ƿ�Ʊʱ�򴥷�
        /// </summary>
        public event FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateChangeSomething InvoiceUpdated;

        /// <summary>
        /// ���߹ҺŻ�����Ϣ
        /// </summary>
        protected FS.HISFC.Models.Registration.Register regInfo = null;

        /// <summary>
        /// ����ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// ������Ϣ
        /// </summary>
        protected string errText = string.Empty;

        /// <summary>
        /// ��ǰ��Ʊ��
        /// </summary>
        protected string invoiceNO = string.Empty;

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public string ErrText
        {
            get
            {
                return this.errText;
            }
            set
            {
                this.errText = value;
            }
        }

        /// <summary>
        /// ��ǰ��Ʊ��
        /// </summary>
        public string InvoiceNO
        {
            get
            {
                return this.tbRealInvoiceNO.Text.Trim();
            }
            set
            {
                this.tbRealInvoiceNO.Text = value;
            }
        }


        /// <summary>
        /// ��Ʊ����
        /// </summary>
        public string InvoiceType
        {
            get
            {
                return this.invoiceType;
            }
            set
            {
                this.invoiceType = value;
            }
        }

        /// <summary>
        /// ��ƱԤ������
        /// </summary>
        public string InvoicePreviewType
        {
            get
            {
                return this.invoicePreviewType;
            }
            set
            {
                this.invoicePreviewType = value;
            }
        }

        /// <summary>
        /// ���߹ҺŻ�����Ϣ
        /// </summary>
        public FS.HISFC.Models.Registration.Register PatientInfo
        {
            get
            {
                return this.regInfo;
            }
            set
            {
                this.regInfo = value;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ����ҩƷ����С�����б�
        /// </summary>
        public FS.FrameWork.Public.ObjectHelper DrugFeeCodeHelper 
        {
            set 
            {
                this.drugFeeCodeHelper = value;
            }
        }

        /// <summary>
        /// ���ô����ӿڱ���
        /// </summary>
        public FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy MedcareInterfaceProxy 
        {
            set 
            {
                this.medcareInterfaceProxy = value;
            }
        }
        private bool isPreeFee = false;
        ///
        /// <summary>
        /// ҽ�������Ƿ�Ԥ����
        /// </summary>
        public bool IsPreFee
        {
            set
            {
                this.isPreeFee = value;
            }
            get
            {
                return this.isPreeFee;
            }
        }
        ///
        /// <summary>
        /// �Ƿ���ʾ�������
        /// </summary>
        protected bool isSetDiag = false;

        #endregion

        #region ����

        /// <summary>
        /// ��������ҩƷ����С�����б�
        /// </summary>
        /// <param name="drugFeeCodeHelper">����ҩƷ����С�����б�</param>
        public void SetFeeCodeIsDrugArrayListObj(FS.FrameWork.Public.ObjectHelper drugFeeCodeHelper) 
        {
            this.drugFeeCodeHelper = drugFeeCodeHelper;
        }

        /// <summary>
        /// ���ô����ӿڱ���
        /// </summary>
        /// <param name="medcareInterfaceProxy">�ӿڱ���</param>
        public void SetMedcareInterfaceProxy(FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy) 
        {
            this.medcareInterfaceProxy = medcareInterfaceProxy;
        }

        /// <summary>
        /// ������ʾ��Ϣ
        /// </summary>
        /// <param name="patient">�ҺŻ�����Ϣ</param>
        /// <param name="ft">�����ķ��÷�����Ϣ</param>
        /// <param name="feeItemLists">������ϸ������Ϣ</param>
        /// <param name="diagLists">�����Ϣ</param>
        /// <param name="otherInfomations">������Ϣ</param>
        /// flag  Ϊ�Ƿ�ÿ����Ŀ����ʾһ��
        public void SetInfomation(FS.HISFC.Models.Registration.Register patient, FS.HISFC.Models.Base.FT ft, ArrayList feeItemLists, ArrayList diagLists,
            params string[] otherInfomations)
        {
            this.Clear();
            this.tbRealOwnCost.SelectAll();
            System.Collections.Generic.List<Object> lo = new System.Collections.Generic.List<object>();
            lo.Add(patient);
            lo.Add(ft);
            lo.Add(feeItemLists);
            lo.Add(diagLists);
            lo.Add(otherInfomations);
            //��ʵʱ���¡� xingz �Ժ�ú����ϡ�
            // otherInfomations[0] = "1" ����ҽ�����ұ��
            // otherInfomations[0] = "2" ��ͬ��λ���
            // otherInfomations[0] = "3" �շ�ǰ��ʾ
            // otherInfomations[0] = "4" �շѺ����
            string strTemp = otherInfomations[0];
            if (this.iMultiScreen !=null && (strTemp == "1" || strTemp == "2" || strTemp == "3" || strTemp == "4"))
            {
                this.iMultiScreen.ListInfo = lo; 
            }
         
            if (this.medcareInterfaceProxy == null)
            {
                this.medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();
            }
            
            if (this.medcareInterfaceProxy == null) 
            {
                return;
            }

            if (patient == null) 
            {
                return;
            }
            
            this.medcareInterfaceProxy.SetPactCode(patient.Pact.ID);
            // {293FDD11-FC10-4ceb-8E4C-1A4304F22592}
            this.medcareInterfaceProxy.IsLocalProcess = false;

            if (feeItemLists == null)// || feeItemLists.Count <= 0)
            {
                if (ft != null)
                {
                    this.tbDrugSendInfo.Text = ft.User01;
                    this.tbRealOwnCost.Text = ft.RealCost.ToString();
                    this.tbReturnCost.Text = ft.ReturnCost.ToString();
                }

                return;
            }

            //if (feeItemLists == null || feeItemLists.Count <= 0)
            //{
            //    return;
            //}


            //if (patient.Pact.PayKind.ID == "02" && isPreeFee == false)
            //{
            //    //ҽ�����۰��Է���
            //}
            //else
            //{

             //{CC93C88A-9DD0-49fe-9DC0-B6DA445A7F30}���ݲ����жϡ��������Ƿ���ҽ�������ˣ�Ҫ���ò������Ʋ����Էѻ��ߡ�--�޸�����ֱ�ʱ���ֵ�����
            if (isPreeFee)
            {
                long returnValue = this.medcareInterfaceProxy.DeleteUploadedFeeDetailsOutpatient(patient, ref feeItemLists);
                if (returnValue == -1)
                {
                    MessageBox.Show(Language.Msg("�����ӿ��ϴ���ϸʧ��!") + this.medcareInterfaceProxy.ErrMsg);

                    return;
                }
                returnValue = this.medcareInterfaceProxy.UploadFeeDetailsOutpatient(patient, ref feeItemLists);
                if (returnValue == -1)
                {
                    MessageBox.Show(Language.Msg("�����ӿ��ϴ���ϸʧ��!") + this.medcareInterfaceProxy.ErrMsg);

                    return;
                }

                returnValue = this.medcareInterfaceProxy.PreBalanceOutpatient(patient, ref feeItemLists);
                if (returnValue == -1)
                {
                    MessageBox.Show(Language.Msg("�����ӿ�Ԥ�������ʧ��!") + this.medcareInterfaceProxy.ErrMsg);

                    return;
                }
                //����ϴ���ϸ,�����ٴ�
                this.medcareInterfaceProxy.Rollback();
            }

            decimal sumTotCost = 0, sumPayCost = 0, sumPubCost = 0, sumOwnCost = 0; decimal sumDrugCost = 0;

            if(feeItemLists != null)
            {
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in feeItemLists)
                {
                    if (f.IsAccounted)
                    {
                        if (f.FT.OwnCost > 0)
                        {
                            sumPayCost += f.FT.OwnCost;
                            sumOwnCost += 0;
                        }
                        else 
                        {
                            sumPayCost += f.FT.PayCost;
                        }
                        
                    }
                    else
                    {
                        sumTotCost += f.FT.TotCost;
                        sumPayCost += f.FT.PayCost;
                        //{C623A693-19A7-4378-859D-5C07CFF9BEB1}
                        sumPubCost += f.FT.PubCost + f.FT.RebateCost ;
                        sumOwnCost += f.FT.OwnCost - f.FT.RebateCost;
                    }
                    if (this.drugFeeCodeHelper.ArrayObject != null && this.drugFeeCodeHelper.ArrayObject.Count > 0 && this.drugFeeCodeHelper.GetObjectFromID(f.Item.MinFee.ID) != null)
                    {
                        sumDrugCost += f.FT.OwnCost + f.FT.PayCost + f.FT.PubCost;
                    }
                }
            
                if (patient.Pact.PayKind.ID == "01")
                {
                    this.tbOwnCost.Text = FS.FrameWork.Public.String.FormatNumber(sumOwnCost, 2).ToString();
                    this.tbTotCost.Text = FS.FrameWork.Public.String.FormatNumber(sumTotCost, 2).ToString();
                    this.tbPayCost.Text = sumPayCost.ToString();
                    //{C623A693-19A7-4378-859D-5C07CFF9BEB1}
                    //this.tbPubCost.Text = "0.00";
                    this.tbPubCost.Text = sumPubCost.ToString();
                    this.tbDrugCost.Text = sumDrugCost.ToString();
                }
                else if (patient.Pact.PayKind.ID == "03")
                {
                    this.tbTotCost.Text = FS.FrameWork.Public.String.FormatNumber(sumTotCost, 2).ToString();
                    this.tbOwnCost.Text = FS.FrameWork.Public.String.FormatNumber(sumOwnCost, 2).ToString();
                    this.tbPayCost.Text = FS.FrameWork.Public.String.FormatNumber(sumPayCost, 2).ToString();
                    this.tbPubCost.Text = FS.FrameWork.Public.String.FormatNumber(sumPubCost, 2).ToString();
                    this.tbDrugCost.Text = sumDrugCost.ToString();
                }
                else
                {
                    if (patient.Pact.PayKind.ID == "02" && isPreeFee == false)
                    {
                        //ҽ�����۰��Է���
                        this.tbOwnCost.Text = FS.FrameWork.Public.String.FormatNumber(sumOwnCost, 2).ToString();
                        this.tbTotCost.Text = FS.FrameWork.Public.String.FormatNumber(sumTotCost, 2).ToString();
                        this.tbPayCost.Text = sumPayCost.ToString();
                        this.tbPubCost.Text = "0.00";
                        this.tbDrugCost.Text = sumDrugCost.ToString();
                    }
                    else
                    {
                        this.tbTotCost.Text = FS.FrameWork.Public.String.FormatNumber(sumTotCost, 2).ToString();
                        //this.tbOwnCost.Text = FS.FrameWork.Public.String.FormatNumber(sumTotCost, 2).ToString();
                        //this.tbPayCost.Text = "0.00";
                        //this.tbPubCost.Text = "0.00";
                        this.tbOwnCost.Text = FS.FrameWork.Public.String.FormatNumber(patient.SIMainInfo.OwnCost, 2).ToString();
                        this.tbPayCost.Text = FS.FrameWork.Public.String.FormatNumber(patient.SIMainInfo.PayCost, 2).ToString();
                        this.tbPubCost.Text = FS.FrameWork.Public.String.FormatNumber(patient.SIMainInfo.PubCost, 2).ToString();
                        this.tbDrugCost.Text = sumDrugCost.ToString();
                    }
                }
            }
            this.isSetDiag = this.controlParamIntegrate.GetControlParam<bool>("MZ9204", true, false);
            if (this.isSetDiag)
            {
                this.neuSpread1.Visible = true;
                this.SetDiay(patient.ID);
            }
            if (ft != null)
            {
                this.tbDrugSendInfo.Text = ft.User01;
                this.tbRealOwnCost.Text = ft.RealCost.ToString();
                this.tbReturnCost.Text = ft.ReturnCost.ToString();
            }
        }

        /// <summary>
        /// ���õ�����Ŀ��Ϣ
        /// </summary>
        /// <param name="f">��Ŀ��Ϣ</param>
        public void SetSingleFeeItemInfomation(FS.HISFC.Models.Fee.Outpatient.FeeItemList f) 
        {
            string siType = string.Empty;
            decimal siRate = 0;

            if (f.Compare == null)
            {
                siType = "�Է�";
                siRate = 100;
            }
            else
            {
                if (f.Compare.CenterItem.ItemGrade == "1")
                {
                    siType = "����";
                    siRate = 0;
                }
                if (f.Compare.CenterItem.ItemGrade == "2")
                {
                    siType = "����";
                    siRate = f.Compare.CenterItem.Rate * 100;
                }
                if (f.Compare.CenterItem.ItemGrade == "3")
                {
                    siType = "�Է�";
                    siRate = 100;
                }
                if (f.Compare.CenterItem.ID.Length <= 0)
                {
                    siType = "�Է�";
                    siRate = 100;
                }
            }
            //if (f.Item.IsPharmacy)
            if(f.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                string itemCode = f.Item.ID;
                DataRow findRow;

                DataRow[] rowFinds = this.dsItem.Tables[0].Select("ITEM_CODE = " + "'" + itemCode + "'");

                if (rowFinds == null || rowFinds.Length == 0)
                {
                    MessageBox.Show(Language.Msg("����Ϊ: [") + itemCode + Language.Msg(" ] ����Ŀ����ʧ��!"));

                    return;
                }
                findRow = rowFinds[0];

                this.lbItemInfo.Text = "ҽ�����: " + siType + " ����:" + siRate.ToString() + "%" + "\n"
                    + "ͨ����:" + findRow["cus_name"].ToString() + " Ӣ����:" + findRow["en_name"].ToString().ToLower() + "\n" +
                    "����:" + findRow["OTHER_NAME"].ToString() + "\n" +
                    "���:" + f.Item.Specs;
            }
            else
            {
                this.lbItemInfo.Text = "ҽ�����: " + siType + " ����:" + siRate.ToString() + "%";
            }
        }

        /// <summary>
        /// ������Ŀ��Ϣ
        /// </summary>
        /// <param name="dsItem">��Ŀ��Ϣ����</param>
        public void SetDataSet(DataSet dsItem) 
        {
            this.dsItem = dsItem;
        }
        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int Init() 
        {
            if (this.drugFeeCodeHelper != null && (this.drugFeeCodeHelper.ArrayObject == null || this.drugFeeCodeHelper.ArrayObject.Count == 0))
            {
                ArrayList drugFeeCodeList = this.managerIntegrate.GetConstantList("DrugMinFee");
                if (drugFeeCodeList == null)
                {
                    MessageBox.Show(Language.Msg("���ҩƷ��С�����б����!") + this.managerIntegrate.Err);

                    return -1;
                }
                
                this.drugFeeCodeHelper.ArrayObject = drugFeeCodeList;
            }
            //��ȡ������
            ArrayList diagnoseType = FS.HISFC.Models.HealthRecord.DiagnoseType.SpellList();
            diagnoseTypeHelper.ArrayObject = diagnoseType;
            this.InitInvoice();
            this.tbRealOwnCost.ReadOnly = false;
            return 1;
        }
        /// <summary>
        ///��û��������Ϣ
        /// </summary>
        /// <param name="patientId">���������</param>
        public void SetDiay(string patientId)
        {
            if (string.IsNullOrEmpty(patientId))
            {
                return;
            }
            ArrayList alDiay = null;
            FS.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
            try
            {
                alDiay = diagManager.QueryCaseDiagnoseForClinicByState(patientId, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("��û��ߵ������Ϣ����" + ex.Message, "��ʾ");
                return;
            }
            if (alDiay == null)
            {
                return;
            }
            if (this.diagnoseTypeHelper.ArrayObject.Count <= 0)
            {
                this.diagnoseTypeHelper.ArrayObject = FS.HISFC.Models.HealthRecord.DiagnoseType.SpellList();
            }
            //���
            this.neuSpread1_Sheet1.RowCount = 0;
            foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in alDiay)
            {
                if (diag.IsValid)//ֻ��ʾ��Чҽ��
                {
                    this.neuSpread1_Sheet1.Rows.Add(0, 1);
                    this.neuSpread1_Sheet1.Cells[0, 0].Text = this.diagnoseTypeHelper.GetObjectFromID(diag.DiagInfo.DiagType.ID).Name;
                    this.neuSpread1_Sheet1.Cells[0, 1].Value = diag.DiagInfo.IsMain;//�Ƿ�����
                    this.neuSpread1_Sheet1.Cells[0, 2].Text = diag.DiagInfo.ICD10.ID;//icd��
                    this.neuSpread1_Sheet1.Cells[0, 3].Text = diag.DiagInfo.ICD10.Name;//icd����
                    this.neuSpread1_Sheet1.Cells[0, 4].Value = FS.FrameWork.Function.NConvert.ToBoolean(diag.DubDiagFlag);//�Ƿ�����
                    this.neuSpread1_Sheet1.Cells[0, 5].Text = diag.DiagInfo.DiagDate.Date.ToShortDateString();//����
                    //this.neuSpread1_Sheet1.Cells[0, 7].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    //this.neuSpread1_Sheet1.Cells[0, 7].Text = diag.DiagInfo.Doctor.ID;//����
                    this.neuSpread1_Sheet1.Cells[0, 6].Text = diag.DiagInfo.Doctor.Name;//���ҽ��
                }
                
            }
            this.InitInvoice();
        }
        /// <summary>
        /// ���
        /// </summary>
        public void Clear() 
        {
            this.tbDrugCost.Text = "0.00";
            this.lbItemInfo.Text = string.Empty;
            //this.neuSpread1_Sheet1.RowCount = 0;
            //this.neuSpread1_Sheet1.RowCount = 2;
            if (iMultiScreen != null)
            {
                this.iMultiScreen.ListInfo = null;
            }
            // ����Ҫ���
            //this.tbOwnCost.Text = "0.00";
            //this.tbPayCost.Text = "0.00";
            //this.tbPubCost.Text = "0.00";

            this.tbRealOwnCost.Text = "";
            //this.tbReturnCost.Text = "0.00";
            //this.tbDrugCost.Text = "0.00";
            //this.tbDrugSendInfo.Text = "";
            //this.tbTotCost.Text = "0.00";
            this.InitInvoice();

        }

        #endregion

        #region IOutpatientOtherInfomationRight ��Ա

        private FS.HISFC.BizProcess.Interface.FeeInterface.IMultiScreen iMultiScreen;
        public FS.HISFC.BizProcess.Interface.FeeInterface.IMultiScreen MultiScreen
        {
            set { iMultiScreen = value; }
        }

        #endregion

        private void tbRealOwnCost_TextChanged(object sender, EventArgs e)
        {
            #region ������ʾ
            decimal tempCost = 0;
            decimal cashCost = 0;
            cashCost = FS.FrameWork.Function.NConvert.ToDecimal(this.tbOwnCost.Text);
            try
            {
                tempCost = FS.FrameWork.Function.NConvert.ToDecimal(this.tbRealOwnCost.Text) - cashCost;
            }
            catch (Exception ex)
            {
                MessageBox.Show("��������ֲ��Ϸ�������֤����" + ex.Message);
                this.tbRealOwnCost.Text = string.Empty;

                return;
            }
            if (tempCost <= 0)
            {
                this.tbReturnCost.Text = "0";
            }
            else
            {
                this.tbReturnCost.Text = tempCost.ToString();
            }
            #endregion

            #region {221FCC64-7D41-471a-9EED-C30BA1CE330A} ��ֹ�޷�����С����
            string dealCent = FS.HISFC.Components.OutpatientFee.Class.Function.DealCent(FS.FrameWork.Function.NConvert.ToDecimal(tbRealOwnCost.Text)).ToString();
            if (FS.FrameWork.Function.NConvert.ToDecimal(dealCent) != FS.FrameWork.Function.NConvert.ToDecimal(tbRealOwnCost.Text))
            {
                this.tbRealOwnCost.Text = dealCent;
            }
            //this.tbRealCost.Text = FS.HISFC.Components.OutpatientFee.Class.Function.DealCent(Convert.ToDecimal(tbRealCost.Text)).ToString(); 
            #endregion
        }

        /// <summary>
        /// ��ò���Ա�ĵ�ǰ��Ʊ��ʼ��
        /// </summary>
        /// <returns></returns>
        public int InitInvoice()
        {
            string invoiceNO = ""; string realInvoiceNO = ""; string errText = "";

            FS.HISFC.Models.Base.Employee oper = this.outpatientManager.Operator as FS.HISFC.Models.Base.Employee;


            int iReturn = this.feeIntegrate.GetInvoiceNO(oper, "C", ref invoiceNO, ref realInvoiceNO, ref errText);
            if (iReturn == -1)
            {
                MessageBox.Show(errText);

                return -1;
            }
            //��ʾ��ǰ��Ʊ��
            this.tbRealInvoiceNO.Text = realInvoiceNO;
            this.tbInvoiceNO.Text = invoiceNO;
            //if (feeIntegrate.InvoiceMessage(oper.ID, "C", invoiceNO, 1, ref errText) < 0)
            //{
            //    MessageBox.Show(errText);
            //    return -1;
            //}
            return 0;
        }
         /// <summary>
        /// ���²���Ա��Ʊ����Ϣ
        /// {C075EA48-EFC2-4de0-A0F2-BFD3F119A85F}
        /// </summary>
        /// <param name="invoiceNO"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public int UpdateInvoice(string invoiceNO,ref string errInfo)
        {
            if (string.IsNullOrEmpty(invoiceNO))
            {
                //MessageBox.Show("��¼����Чӡˢ��Ʊ�ţ�");
                errInfo = "��¼����Чӡˢ��Ʊ�ţ�";
                return 2;
            }

            invoiceNO = invoiceNO.PadLeft(12, '0');

            FS.HISFC.Models.Base.Employee oper = outpatientManager.Operator as FS.HISFC.Models.Base.Employee;

            //FS.FrameWork.Management.PublicTrans.BeginTransaction();
            //feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            int iRes = feeIntegrate.UpdateNextInvoiceNO(oper.ID, "C", invoiceNO);
            if (iRes <= 0)
            {
                //FS.FrameWork.Management.PublicTrans.RollBack();
                //MessageBox.Show(feeIntegrate.Err);
                errInfo = feeIntegrate.Err;
                return 2;
            }
            else
            {
                //FS.FrameWork.Management.PublicTrans.Commit();
                //MessageBox.Show("���³ɹ���");
                errInfo = "���³ɹ���";
                return 1;
            }


        }

        /// <summary>
        /// ���ٶ�λ����Ʊ��
        /// </summary>
        public void SetFocus()
        {
            this.tbRealInvoiceNO.Focus();
            this.tbRealInvoiceNO.SelectAll();
        }
        /// <summary>
        /// ��Ʊ����Ч���ж�
        /// </summary>
        /// <returns></returns>
        public bool IsValid() 
        {
            string invoiceType = this.controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.GET_INVOICE_NO_TYPE, false, "0");
            if (invoiceType != "2")//Ĭ�Ϸ�Ʊģʽ,��Ҫtrans֧��
            {
                if (this.tbInvoiceNO.Text == string.Empty)
                {
                    this.errText = "�����÷�Ʊ��";

                    return false;
                }
            }
            return true;
        }

        private void tbInvoiceNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string errInfo = "";
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                int iReturn = this.UpdateInvoice(this.tbRealInvoiceNO.Text.Trim(), ref errInfo);

                if (iReturn != 2)
                {
                    InvoiceUpdated();
                }
                else
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(errInfo);
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("���³ɹ���");
            }
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            string errInfo = "";
            this.IsValid();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            int iReturn = this.UpdateInvoice(this.tbRealInvoiceNO.Text.Trim(), ref errInfo);

            if (iReturn != 2)
            {
            }
            else
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(errInfo);
                return;
            }

            string invoiceNo = this.tbInvoiceNO.Text;
            if (string.IsNullOrEmpty(invoiceNo))
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("��¼����Ч��Ʊ�ţ�");
                return;
            }

            //���µ��Է�Ʊ�� 2011-6-24 houwb
            iReturn = this.feeIntegrate.UpdateNextInvoliceNo(this.outpatientManager.Operator.ID, "INVOICE-C", invoiceNo);
            if (iReturn == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(this.feeIntegrate.Err);
                return;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("���³ɹ���");
        }
        
    }
}
