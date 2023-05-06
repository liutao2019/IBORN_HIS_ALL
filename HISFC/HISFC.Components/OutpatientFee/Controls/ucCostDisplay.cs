using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.OutpatientFee.Controls
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

        private FS.FrameWork.Public.ObjectHelper diagnoseTypeHelper = new FS.FrameWork.Public.ObjectHelper();

        private FS.HISFC.BizLogic.Fee.UndrugPackAge managerUndrugPackAge = new FS.HISFC.BizLogic.Fee.UndrugPackAge();

        /// <summary>
        /// ���Ʋ���ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

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
            if (iMultiScreen != null)
            {
                this.iMultiScreen.ListInfo = null;
            }

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
                        sumPubCost += f.FT.PubCost + f.FT.RebateCost + f.FT.DiscountCardEco;
                        sumOwnCost += f.FT.OwnCost - f.FT.RebateCost - f.FT.DiscountCardEco;
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
                        if (patient.SIMainInfo.PubCost != 0)
                        {
                            this.tbTotCost.Text = FS.FrameWork.Public.String.FormatNumber(sumTotCost, 2).ToString();
                            this.tbOwnCost.Text = FS.FrameWork.Public.String.FormatNumber(patient.SIMainInfo.OwnCost, 2).ToString();
                            this.tbPayCost.Text = FS.FrameWork.Public.String.FormatNumber(patient.SIMainInfo.PayCost, 2).ToString();
                            this.tbPubCost.Text = FS.FrameWork.Public.String.FormatNumber(patient.SIMainInfo.PubCost, 2).ToString();
                            this.tbDrugCost.Text = sumDrugCost.ToString();
                        }
                        else
                        {
                            //ҽ�����۰��Է���
                            this.tbOwnCost.Text = FS.FrameWork.Public.String.FormatNumber(sumOwnCost, 2).ToString();
                            this.tbTotCost.Text = FS.FrameWork.Public.String.FormatNumber(sumTotCost, 2).ToString();
                            this.tbPayCost.Text = sumPayCost.ToString();
                            this.tbPubCost.Text = "0.00";
                            this.tbDrugCost.Text = sumDrugCost.ToString();
                        }
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

            if (sumTotCost != 0)
            {
                this.ucPatientLabel1.getUserLabelByHisCardNo(patient.PID.CardNO);
                this.ucPatientLabel1.Visible = true;
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

                this.rtbItemInfo.Text = "ҽ�����: " + siType + " ����:" + siRate.ToString() + "%" + "\n"
                    + "ͨ����:" + findRow["cus_name"].ToString() + " Ӣ����:" + findRow["en_name"].ToString().ToLower() + "\n" +
                    "����:" + findRow["OTHER_NAME"].ToString() + "\n" +
                    "���:" + f.Item.Specs;
            }
            else
            {
                if (!f.IsGroup)
                {
                    this.rtbItemInfo.Text = "ҽ�����: " + siType + " ����:" + siRate.ToString() + "%";
                }
                else
                {
                    this.rtbItemInfo.Text = "";

                    ArrayList alDetails = this.managerUndrugPackAge.QueryUndrugPackagesBypackageCode(f.Item.ID);

                    foreach (FS.HISFC.Models.Fee.Item.UndrugComb info in alDetails)
                    {
                        this.rtbItemInfo.Text += info.Name + "\n";
                    }
                }
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

            return 1;
        }
        /// <summary>
        ///��û���������Ϣ
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
                alDiay = diagManager.QueryCaseDiagnoseForClinicByState(patientId,
                    FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC,
                    true);//Ĭ�ϲ���ʾ�������
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
        /// <summary>
        /// ���
        /// </summary>
        public void Clear() 
        {
            this.tbDrugCost.Text = "0.00";
            this.rtbItemInfo.Text = string.Empty;
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.RowCount = 2;
            if (iMultiScreen != null)
            {
                this.iMultiScreen.ListInfo = null;
            }

            // ����Ҫ���
            this.tbOwnCost.Text = "0.00";
            this.tbPayCost.Text = "0.00";
            this.tbPubCost.Text = "0.00";

            this.tbRealOwnCost.Text = "0.00";
            this.tbReturnCost.Text = "0.00";
            this.tbDrugCost.Text = "0.00";
            this.tbDrugSendInfo.Text = "";
            this.tbTotCost.Text = "0.00";

        }

        #endregion

        #region IOutpatientOtherInfomationRight ��Ա

        private FS.HISFC.BizProcess.Interface.FeeInterface.IMultiScreen iMultiScreen;
        public FS.HISFC.BizProcess.Interface.FeeInterface.IMultiScreen MultiScreen
        {
            set { iMultiScreen = value; }
        }

        #endregion
    }
}
