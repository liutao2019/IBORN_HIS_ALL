using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using FS.HISFC.Models.Fee.Outpatient;
using FS.FrameWork.Models;
using FS.FrameWork.Management;
using FS.FrameWork.Function;
using FarPoint.Win.Spread;
using FS.HISFC.BizProcess.Interface.FeeInterface;

namespace FS.HISFC.Components.OutpatientFee.Froms
{
    /// <summary>
    /// �����շѽ���
    /// </summary>
    public partial class frmDealBalance : Form, FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientPopupFee
    {
        public frmDealBalance()
        {
            InitializeComponent();
            isDealCentNoAllCancy = this.controlParam.GetControlParam<bool>("MZ9932", true, false);
        }

        #region ����
        /// <summary>
        /// �������з��ֽ�֧����ʽ���Ƿ�����������(Ĭ��true)
        /// </summary>
        private bool isDealCentNoAllCancy = true;
        /// <summary>
        /// �Է�ҩ���
        /// </summary>
        protected decimal selfDrugCost;

        /// <summary>
        /// ����ҩ���
        /// </summary>
        protected decimal overDrugCost;

        /// <summary>
        /// �Էѽ��
        /// </summary>
        protected decimal ownCost;

        /// <summary>
        /// �Ը����
        /// </summary>
        protected decimal payCost;

        /// <summary>
        /// ���ʽ��
        /// </summary>
        protected decimal pubCost;

        /// <summary>
        /// �Է��ܶ� = �Էѽ�� + �Ը����
        /// </summary>
        protected decimal totOwnCost;

        /// <summary>
        /// �ܽ��
        /// </summary>
        protected decimal totCost;
        /// <summary>
        /// ������
        /// 
        /// ͨ�������㷨�������ܲ����������
        // {A14864D7-2EF9-45e7-84DE-B2D800DA9F1B}
        /// </summary>
        protected decimal rebateRate;

        /// <summary>
        /// ʵ�����
        /// </summary>
        protected decimal realCost;

        /// <summary>
        /// ������
        /// </summary>
        protected decimal leastCost;

        /// <summary>
        /// ���ַ�Ʊ����
        /// </summary>
        protected int splitCounts;

        /// <summary>
        /// �Ƿ���Էַ�Ʊ
        /// </summary>
        protected bool isCanSplit;

        /// <summary>
        /// �Ƿ��ֽ����
        /// </summary>
        protected bool isCashPay = false;

        /// <summary>
        /// �շ�ʱӦ��ֻ��ʾ�ֽ���
        /// </summary>
        protected bool isDisplayCashOnly = false;

        /// <summary>
        /// �Ƿ�����޸ķ�Ʊ��ӡ����
        /// </summary>
        protected bool isModifyDate = false;

        /// <summary>
        /// ����Ʊ�ͷ�Ʊ��ϸ����
        /// </summary>
        protected ArrayList alInvoiceAndDetails = new ArrayList();

        /// <summary>
        /// ����Ʊ����
        /// </summary>
        protected ArrayList alInvoices = new ArrayList();

        /// <summary>
        /// ��Ʊ��ϸ����
        /// </summary>
        protected ArrayList alInvoiceDetails = new ArrayList();

        /// <summary>
        /// ������ϸ����
        /// </summary>
        protected ArrayList alFeeDetails = new ArrayList();

        //{E6CD2A14-1DCB-4361-834C-9CF9B9DC669A}���һ�����ԣ����水��Ʊ����ķ�����ϸ liuq
        /// <summary>
        /// ��Ʊ������ϸ����
        /// </summary>
        private ArrayList alInvoiceFeeDetails = new ArrayList();

        /// <summary>
        /// ��С�������
        /// </summary>
        protected ArrayList alMinFee = new ArrayList();

        /// <summary>
        /// ��ͬ��λ��Ϣ
        /// </summary>
        protected FS.HISFC.Models.Base.PactInfo pactInfo = new FS.HISFC.Models.Base.PactInfo();

        /// <summary>
        /// ���߻�����Ϣ
        /// </summary>
        protected FS.HISFC.Models.Registration.Register rInfo = new FS.HISFC.Models.Registration.Register();

        /// <summary>
        /// ���Ʋ���ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParam = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// ���Ʋ���ҵ���
        /// </summary>
        protected FS.FrameWork.Management.ControlParam myCtrl = new ControlParam();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// �����˻�ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();

        /// <summary>
        /// ֧����ʽ��Ϣ
        /// </summary>
        protected ArrayList alPatientPayModeInfo = new ArrayList();

        /// <summary>
        /// ֧����ʽ�б�
        /// </summary>
        protected ArrayList alPayModes = new ArrayList();

        /// <summary>
        /// �ַ�Ʊ�б�
        /// </summary>
        protected ArrayList alSplitInvoices = new ArrayList();

        /// <summary>
        /// ���б�
        /// </summary>
        protected ArrayList alBanks = new ArrayList();

        /// <summary>
        /// payMode�б��ѯ
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper helpPayMode = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// �����б��ѯ
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper helpBank = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        ///��С�����б��ѯ
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper helpMinFee = null;

        /// <summary>
        /// ��С�����б��ѯ
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper helpMinFeeList = new FS.FrameWork.Public.ObjectHelper();


        /// <summary>
        /// �շѰ�ť����
        /// </summary>
        public event FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateFee FeeButtonClicked;

        /// <summary>
        /// ���۰�ť����
        /// </summary>
        public event FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateChangeSomething ChargeButtonClicked;

        /// <summary>
        /// ֧����ʽѡ���б�
        /// </summary>
        FS.FrameWork.WinForms.Controls.PopUpListBox lbPayMode = new FS.FrameWork.WinForms.Controls.PopUpListBox();

        /// <summary>
        /// ������Ϣ���
        /// </summary>
        FS.HISFC.Components.OutpatientFee.Controls.ucPayingAgentMess ucPayingAgentMessControl = new FS.HISFC.Components.OutpatientFee.Controls.ucPayingAgentMess();

        /// <summary>
        /// ����ѡ���б�
        /// </summary>
        FS.FrameWork.WinForms.Controls.PopUpListBox lbBank = new FS.FrameWork.WinForms.Controls.PopUpListBox();
        /// <summary>
        /// ���ʻ��߽ӿ�
        /// </summary>
        FS.HISFC.BizProcess.Interface.Fee.IKeepAccountPatient keepAccountPatient = null;
        /// <summary>
        /// �Ƿ���ʻ���
        /// </summary>
        bool isKeepAccountPatient = false;
        /// <summary>
        /// ACY(�ʻ�֧��)
        /// </summary>
        private int rowACY = -1;

        /// <summary>
        /// ACD(�ʻ�����) - ��ʵ��������
        /// </summary>
        private int rowACD = -1;

        /// <summary>
        /// ECO(�Ż�) - ��ʵ��������
        /// </summary>
        private int rowECO = -1;

        /// <summary>
        /// �ֽ�֧����ʽ
        /// </summary>
        private int rowCA = -1;

        /// <summary>
        /// �˻�ʵ��
        /// </summary>
        private FS.HISFC.Models.Account.Account currentAccountInfo = null;
        /// <summary>
        /// �˻�����ʵ��
        /// </summary>
        private FS.HISFC.Models.Account.AccountDetail currentAccountDetail = null;
        /// <summary>
        /// �˻�����
        /// </summary>
        private System.Collections.Generic.List<FS.HISFC.Models.Account.AccountDetail> accountDetailList = new System.Collections.Generic.List<FS.HISFC.Models.Account.AccountDetail>();
               
               
        /// <summary>
        /// ��Ա����
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper accLevelHelper = null;
        /// <summary>
        /// �˷ѵ�ʱ���ж��Ƿ���ȡ��
        /// </summary>
        protected bool isPushCancelButton = false;

        /// <summary>
        /// �Ƿ�ɹ��շѣ�δ�˳�
        /// {AFEDD473-052A-4c8a-9EA4-9D002443DF52}
        /// </summary>
        private bool isSuccessFee = false;
        /// <summary>
        /// �Ƿ�ɹ��շѣ�δ�˳�
        /// {AFEDD473-052A-4c8a-9EA4-9D002443DF52}
        /// </summary>
        public bool IsSuccessFee
        {
            get
            {
                return isSuccessFee;
            }
            set
            {
                isSuccessFee = value;
            }
        }

        /// <summary>
        /// �Ƿ��˷ѵ���
        /// </summary>
        protected bool isQuitFee = false;

        /// <summary>
        /// ҽ��������
        /// </summary>
        protected bool isSICanUserCardPayAll = false;

        FS.HISFC.Models.Base.FT ftFeeInfo = new FS.HISFC.Models.Base.FT();

        /// <summary>
        /// ���ݿ�����
        /// </summary>
        protected FS.FrameWork.Management.Transaction trans = null;

        /// <summary>
        /// ֧����ʽ�Ƿ�����ɹ�
        /// </summary>
        protected bool isPaySuccess = false;
        /// <summary>
        /// �����ӿ�
        /// </summary>
        private FS.HISFC.BizProcess.Interface.FeeInterface.IBankTrans bankTrans = null;

        FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();

        protected FS.HISFC.BizLogic.Fee.Outpatient outpatient = new FS.HISFC.BizLogic.Fee.Outpatient();

        /// <summary>
        /// ҽԺ����
        /// {C3CC048B-8307-447a-ABA3-B43768D1E154}
        /// </summary>
        private string hospitalCode = "";
        /// <summary>
        /// ҽԺ����
        /// {C3CC048B-8307-447a-ABA3-B43768D1E154}
        /// </summary>
        public string HospitalCode
        {
            get
            {
                if (string.IsNullOrEmpty(hospitalCode))
                {
                    hospitalCode = this.controlParam.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.HosCode, true, "");
                }
                return hospitalCode;
            }
        }
        #endregion

        #region ����
        public FS.HISFC.BizProcess.Interface.FeeInterface.IBankTrans BankTrans
        {
            get { return bankTrans; }
            set { bankTrans = value; }
        }
        //{E6CD2A14-1DCB-4361-834C-9CF9B9DC669A}���һ�����ԣ����水��Ʊ����ķ�����ϸ liuq
        /// <summary>
        /// ��Ʊ������ϸ����
        /// </summary>
        public ArrayList InvoiceFeeDetails
        {
            get { return alInvoiceFeeDetails; }
            set { alInvoiceFeeDetails = value; }
        }


        /// <summary>
        /// �˷ѵ�ʱ���ж��Ƿ���ȡ��
        /// </summary>
        public bool IsPushCancelButton
        {
            get
            {
                return this.isPushCancelButton;
            }
            set
            {
                this.isPushCancelButton = value;
            }
        }

        /// <summary>
        /// �Ƿ��ֽ����
        /// </summary>
        public bool IsCashPay
        {
            get
            {
                return this.isCashPay;
            }
            set
            {
                this.isCashPay = value;
            }
        }

        /// <summary>
        /// �Ƿ��˷�
        /// </summary>
        public bool IsQuitFee
        {
            set
            {
                isQuitFee = value;
                if (isQuitFee)
                {
                    this.tbCharge.Enabled = false;

                }
            }
        }

        /// <summary>
        /// ���ݿ�����
        /// </summary>
        public FS.FrameWork.Management.Transaction Trans
        {
            set
            {
                trans = value;
            }
        }

        /// <summary>
        /// �շ���Ϣ
        /// </summary>
        public FS.HISFC.Models.Base.FT FTFeeInfo
        {
            get
            {
                return this.ftFeeInfo;
            }
        }

        /// <summary>
        /// �Һ���Ϣ
        /// </summary>
        public FS.HISFC.Models.Registration.Register PatientInfo
        {
            get
            {
                return this.rInfo;
            }
            set
            {
                this.rInfo = value;
            }
        }

        /// <summary>
        /// ����Ʊ�ͷ�Ʊ��ϸ����
        /// </summary>
        public ArrayList InvoiceAndDetails
        {
            set
            {
                alInvoiceAndDetails = value;
            }
            get
            {
                return alInvoiceAndDetails;
            }
        }

        /// <summary>
        /// ����Ʊ����
        /// </summary>
        public ArrayList Invoices
        {
            set
            {
                alInvoices = value;
                if (alInvoices != null)
                {
                    this.fpSplit_Sheet1.RowCount = alInvoices.Count;
                    for (int i = 0; i < alInvoices.Count; i++)
                    {
                        Balance balance = alInvoices[i] as Balance;
                        this.fpSplit_Sheet1.Cells[i, 0].Text = balance.Invoice.ID;
                        this.fpSplit_Sheet1.Cells[i, 1].Text = balance.FT.TotCost.ToString();
                        string tmp = null;
                        switch (balance.Memo)
                        {
                            case "5":
                                tmp = "�ܷ�Ʊ";
                                break;
                            case "1":
                                tmp = "�Է�";
                                break;
                            case "2":
                                tmp = "����";
                                break;
                            case "3":
                                tmp = "����";
                                break;
                            case "4":
                                tmp = "ҽ��";
                                break;
                        }
                        this.fpSplit_Sheet1.Cells[i, 2].Text = tmp;
                        this.fpSplit_Sheet1.Cells[i, 2].Tag = balance.Memo;
                        this.fpSplit_Sheet1.Cells[i, 3].Text = balance.FT.OwnCost.ToString();
                        this.fpSplit_Sheet1.Cells[i, 4].Text = balance.FT.PayCost.ToString();
                        this.fpSplit_Sheet1.Cells[i, 5].Text = balance.FT.PubCost.ToString();
                        //��Ʊ����
                        this.fpSplit_Sheet1.Rows[i].Tag = balance;
                        //��Ʊ��ϸ
                        this.fpSplit_Sheet1.Cells[i, 0].Tag = ((ArrayList)alInvoiceDetails[i])[0] as ArrayList;
                        //{E6CD2A14-1DCB-4361-834C-9CF9B9DC669A}���һ�����ԣ����水��Ʊ����ķ�����ϸ liuq
                        //������ϸ
                        this.fpSplit_Sheet1.Cells[i, 3].Tag = ((ArrayList)InvoiceFeeDetails[i]) as ArrayList;
                    }
                }
            }
            get
            {
                return alInvoices;
            }
        }

        /// <summary>
        /// ��Ʊ��ϸ����
        /// </summary>
        public ArrayList InvoiceDetails
        {
            set
            {
                alInvoiceDetails = value;
            }
            get
            {
                return alInvoiceDetails;
            }
        }

        /// <summary>
        /// ������ϸ����
        /// </summary>
        public ArrayList FeeDetails
        {
            set
            {
                alFeeDetails = value;
                this.SpliteMinFee();
            }
            get
            {
                return alFeeDetails;
            }
        }

        /// <summary>
        /// ���ߺ�ͬ��λ��Ϣ
        /// </summary>
        public FS.HISFC.Models.Base.PactInfo PactInfo
        {
            set
            {
                this.pactInfo = value;
                if (this.pactInfo.PayKind.ID == "01")//�Էѻ��߿��Է�Ʊ
                {
                    this.tpSplitInvoice.Show();
                }
                else
                {
                    this.tpSplitInvoice.Hide();
                }
            }
        }

        /// <summary>
        /// �Է�ҩ���
        /// </summary>
        public decimal SelfDrugCost
        {
            set
            {
                selfDrugCost = value;
                this.tbSelfDrug.Text = selfDrugCost.ToString();
            }
        }

        /// <summary>
        /// ����ҩ���
        /// </summary>
        public decimal OverDrugCost
        {
            set
            {
                overDrugCost = value;
                this.tbOverDrug.Text = overDrugCost.ToString();
            }
        }

        /// <summary>
        /// Ӧ�����
        /// </summary>
        public decimal OwnCost
        {
            set
            {
                ownCost = value;
                this.tbOwnCost.Text = ownCost.ToString();
            }
            get
            {
                return this.ownCost;
            }
        }

        /// <summary>
        /// ���ʽ��
        /// </summary>
        public decimal PubCost
        {
            set
            {
                pubCost = value;
                this.tbPubCost.Text = pubCost.ToString();
                //{0F169460-7FF9-4b76-A22E-C2D0A1DCD438}֧����ʽ����Զ����䡢�����жϽ�����
                //ע���¼�
                this.fpPayMode_Sheet1.CellChanged -= new FarPoint.Win.Spread.SheetViewEventHandler(this.fpPayMode_Sheet1_CellChanged);
                int rowIndex = this.GetRowIndexByName(fpPayMode_Sheet1, "ͳ��(ҽԺ�渶)");

                this.fpPayMode_Sheet1.Cells[rowIndex, (int)PayModeCols.PayMode].Text = "ͳ��(ҽԺ�渶)";
                this.fpPayMode_Sheet1.Cells[rowIndex, (int)PayModeCols.Cost].Text = value.ToString();
                this.fpPayMode_Sheet1.Cells[rowIndex, (int)PayModeCols.PayMode].Locked = true;
                this.fpPayMode_Sheet1.Cells[rowIndex, (int)PayModeCols.Cost].Locked = true;
            }
            get
            {
                return this.pubCost;
            }
        }

        /// <summary>
        /// �Ը����
        /// </summary>
        public decimal PayCost
        {
            set
            {
                payCost = value;
                this.tbPayCost.Text = payCost.ToString();
                //if (this.rInfo.Pact.PayKind.ID == "02" && !this.isCashPay)
                //    if ((this.rInfo.Pact.PayKind.ID == "02" || rInfo.Pact.PayKind.ID == "03") && !this.isCashPay)

                //{
                //    //{0F169460-7FF9-4b76-A22E-C2D0A1DCD438}֧����ʽ����Զ����䡢�����жϽ�����
                //    //ע���¼�
                //    //this.fpPayMode_Sheet1.CellChanged -= new FarPoint.Win.Spread.SheetViewEventHandler(this.fpPayMode_Sheet1_CellChanged);
                //    this.fpPayMode_Sheet1.Cells[4, (int)PayModeCols.PayMode].Text = "�����ʻ�";
                //    this.fpPayMode_Sheet1.Cells[4, (int)PayModeCols.Cost].Text = value.ToString();
                //    this.fpPayMode_Sheet1.Cells[4, (int)PayModeCols.PayMode].Locked = true;// modify  by jlj
                //    //this.fpPayMode_Sheet1.CellChanged += new FarPoint.Win.Spread.SheetViewEventHandler(this.fpPayMode_Sheet1_CellChanged);
                //}
            }
            get
            {
                return this.payCost;
            }
        }

        /// <summary>
        /// �ܽ��
        /// </summary>
        public decimal TotCost
        {
            set
            {
                totCost = value;
                this.tbTotCost.Text = totCost.ToString();
            }
            get
            {
                return totCost;
            }
        }

        /// <summary>
        /// ʵ�����
        /// </summary>
        public decimal RealCost
        {
            set
            {
                realCost = value;

                //this.tbRealCost.Text = realCost.ToString();
            }
        }
        /// <summary>
        /// ������
        /// 
        /// ͨ�������㷨�������ܲ����������
        /// {A14864D7-2EF9-45e7-84DE-B2D800DA9F1B}
        /// </summary>
        public decimal RebateRate
        {
            get { return rebateRate; }
            set
            {
                rebateRate = value;
                if (rebateRate > 0)
                {
                    this.fpPayMode_Sheet1.Cells[6, (int)PayModeCols.PayMode].Text = "����";
                    this.fpPayMode_Sheet1.SetValue(6, (int)PayModeCols.Cost, rebateRate);
                    this.fpPayMode_Sheet1.Cells[6, (int)PayModeCols.PayMode].Locked = true;
                }
            }
        }

        /// <summary>
        /// Ӧ�ɽ��
        /// {A14864D7-2EF9-45e7-84DE-B2D800DA9F1B}
        /// </summary>
        public decimal TotOwnCost
        {
            set
            {
                totOwnCost = value;                                  //totOwnCost = Class.Function.DealCent(totOwnCost);
                this.tbTotOwnCost.Text = totOwnCost.ToString();
                this.ucPayingAgentMessControl.TotOwnCost = totOwnCost;
                this.tbRealCost.SelectAll();                          //this.tbRealCost.Text = Class.Function.DealCent(totOwnCost).ToString();

                this.fpPayMode_Sheet1.Cells[this.rowCA, (int)PayModeCols.PayMode].Text = "�ֽ�";

                //ʵ�ʷ����Ľ��
                decimal decRealCost = totOwnCost - rebateRate;

                #region �����˻�����

                //�ж��Ƿ���������˻�
                if (this.trans != null)
                {
                    this.accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                }

                if (this.currentAccountInfo != null && !string.IsNullOrEmpty(this.currentAccountInfo.ID))
                {
                    accountDetailList = this.accountManager.GetAccountDetail(this.currentAccountInfo.ID,this.ucPayingAgentMessControl.AccountTypeCode,"1");
                    if (accountDetailList.Count <= 0)
                    {
                        this.fpPayMode_Sheet1.Cells[this.rowACY, (int)PayModeCols.Cost].Text = "0";
                        this.fpPayMode_Sheet1.Cells[this.rowACD, (int)PayModeCols.Cost].Text = "0";
                        this.fpPayMode_Sheet1.Cells[this.rowCA, (int)PayModeCols.Cost].Text = decRealCost.ToString("F2");
                           
                        return;
                    }
                    currentAccountDetail = accountDetailList[0] as FS.HISFC.Models.Account.AccountDetail;
                    decimal baseVacancy = this.currentAccountDetail.BaseVacancy;          //��ֵ�˻����
                    decimal donateVacancy = this.currentAccountDetail.DonateVacancy;    //�����˻����

                    //������������˻�,�����˻�������0, ��Ҫ���Էѽ�� ����0, ��ô��ʾԺ���˻�֧����ʽ
                    if (baseVacancy > 0 && decRealCost > 0)
                    {
                        decimal donatePay = Math.Round(decRealCost * (donateVacancy / (baseVacancy + donateVacancy)), 2);
                        decimal basePay = decRealCost - donatePay;

                        if (basePay <= baseVacancy)
                        {
                            //�����˻����ã�
                            this.fpPayMode_Sheet1.Cells[this.rowACY, (int)PayModeCols.Cost].Text = basePay.ToString();

                            //�����˻�
                            if (donatePay <= donateVacancy)
                            {
                                //����
                                this.fpPayMode_Sheet1.Cells[this.rowACD, (int)PayModeCols.Cost].Text = donatePay.ToString();
                            }
                            else
                            {
                                //������
                                donatePay = donateVacancy;
                                this.fpPayMode_Sheet1.Cells[this.rowACD, (int)PayModeCols.Cost].Text = donatePay.ToString();
                            }

                        }
                        else
                        {
                            //�����˻������ã�
                            basePay = baseVacancy;
                            this.fpPayMode_Sheet1.Cells[this.rowACY, (int)PayModeCols.Cost].Text = basePay.ToString();

                            decimal accoutRealCost = Math.Round(basePay / (baseVacancy / (baseVacancy + donateVacancy)), 2);
                            donatePay = accoutRealCost - basePay;

                            //�����˻�
                            if (donatePay <= donateVacancy)
                            {
                                //����
                                this.fpPayMode_Sheet1.Cells[this.rowACD, (int)PayModeCols.Cost].Text = donatePay.ToString();
                            }
                            else
                            {
                                //������
                                donatePay = donateVacancy;
                                this.fpPayMode_Sheet1.Cells[this.rowACD, (int)PayModeCols.Cost].Text = donatePay.ToString();
                            }

                        }

                        //�Է�֧����� CA
                        decimal leftCAcost = decRealCost - basePay - donatePay;
                        this.fpPayMode_Sheet1.Cells[this.rowCA, (int)PayModeCols.Cost].Text = leftCAcost.ToString();
                        this.fpPayMode_Sheet1.Cells[this.rowCA, (int)PayModeCols.PayMode].Locked = true;

                    }
                    else
                    {
                        //ȫ�ֽ�֧��
                        this.fpPayMode_Sheet1.SetValue(this.rowCA, (int)PayModeCols.Cost, decRealCost);
                        this.fpPayMode_Sheet1.Cells[this.rowCA, (int)PayModeCols.PayMode].Locked = true;
                    }
                }

                #endregion

                #region ���˴���

                if (isKeepAccountPatient)
                {
                    //this.fpPayMode_Sheet1.SetValue(4, (int)PayModeCols.Cost, decRealCost);
                }

                #endregion

                this.tbRealCost.Text = Class.Function.DealCent(totOwnCost).ToString();
            }

            get
            {
                return totOwnCost;
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        public decimal LeastCost
        {
            set
            {
                leastCost = value;
            }
        }

        #endregion

        #region ��ö��
        /// <summary>
        /// ֧����ʽ��ö��
        /// </summary>
        protected enum PayModeCols
        {
            /// <summary>
            /// ֧����ʽ
            /// </summary>
            PayMode = 0,
            /// <summary>
            /// ���
            /// </summary>
            Cost = 1,
            /// <summary>
            /// ��������
            /// </summary>
            Bank = 2,
            /// <summary>
            /// �ʺ�
            /// </summary>
            Account = 3,
            /// <summary>
            /// ���ݵ�λ
            /// </summary>
            Company = 4,
            /// <summary>
            /// ֧Ʊ����Ʊ�����׺�
            /// </summary>
            PosNo = 5
        }

        #endregion

        #region ����

        #region ˽�з���

        /// <summary>
        /// ��ƱԤ��
        /// </summary>
        private void PreViewInvoice()
        {
            int row = this.fpSplit_Sheet1.ActiveRowIndex;
            if (this.fpSplit_Sheet1.RowCount <= 0)
            {
                return;
            }

            Balance invoicePreView = this.fpSplit_Sheet1.Rows[row].Tag as Balance;
            ArrayList invoiceDetailsPreview = this.fpSplit_Sheet1.Cells[row, 0].Tag as ArrayList;
            ArrayList InvoiceFeeDetailsPreview = this.fpSplit_Sheet1.Cells[row, 3].Tag as ArrayList;

            IInvoicePrint iInvoicePrint = null;

            string returnValue = controlParam.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.INVOICEPRINT, false, string.Empty);
            if (string.IsNullOrEmpty(returnValue))
            {
                iInvoicePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IInvoicePrint)) as IInvoicePrint;
            }
            else
            {
                returnValue = Application.StartupPath + returnValue;
                try
                {
                    Assembly a = Assembly.LoadFrom(returnValue);
                    Type[] types = a.GetTypes();
                    foreach (System.Type type in types)
                    {
                        if (type.GetInterface("IInvoicePrint") != null)
                        {
                            iInvoicePrint = System.Activator.CreateInstance(type) as IInvoicePrint;

                            break;
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("��ʼ����Ʊʧ��!" + e.Message);

                    return;
                }
            }

            if (iInvoicePrint == null)
            {
                MessageBox.Show("��ά����ӡƱ�ݣ����Ҵ�ӡƱ��ʧ�ܣ�");
                return;
            }

            try
            {
                if (this.trans != null)
                {
                    iInvoicePrint.Trans = FS.FrameWork.Management.PublicTrans.Trans;
                }
                iInvoicePrint.SetPrintValue(this.rInfo, invoicePreView, invoiceDetailsPreview, alFeeDetails, alPatientPayModeInfo, true);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

                return;
            }

            FS.FrameWork.WinForms.Classes.Function.PopShowControl((Control)iInvoicePrint);
        }

        /// <summary>
        /// ������С����
        /// </summary>
        private void SpliteMinFee()
        {
            this.alMinFee = new ArrayList();

            helpMinFee.ArrayObject = this.alMinFee;

            if (this.alFeeDetails == null || this.alFeeDetails.Count <= 0)
            {
                return;
            }

            if (this.trans != null)
            {
                this.managerIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            }

            foreach (FeeItemList feeItemList in alFeeDetails)
            {
                #region 2007-8-24 liuq ����С��������
                string minFeeName = string.Empty;
                #endregion
                if (this.helpMinFee.GetObjectFromID(feeItemList.Item.MinFee.ID) == null)
                {
                    NeuObject obj = new NeuObject();

                    obj.ID = feeItemList.Item.MinFee.ID;

                    if (this.helpMinFeeList.GetObjectFromID(feeItemList.Item.MinFee.ID) == null)
                    {
                        obj.Name = this.managerIntegrate.GetConstansObj("MINFEE", feeItemList.Item.MinFee.ID).Name;
                    }
                    else
                    {
                        obj.Name = this.helpMinFeeList.GetObjectFromID(feeItemList.Item.MinFee.ID).Name;
                    }

                    obj.Memo = feeItemList.FT.TotCost.ToString();
                    #region 2007-8-24 liuq ����С��������
                    minFeeName = obj.Name;
                    #endregion
                    alMinFee.Add(obj);
                }
                else
                {
                    NeuObject obj = helpMinFee.GetObjectFromID(feeItemList.Item.MinFee.ID);
                    #region 2007-8-24 liuq ����С��������
                    minFeeName = obj.Name;
                    #endregion
                    obj.Memo = FS.FrameWork.Public.String.FormatNumber(NConvert.ToDecimal(obj.Memo) + feeItemList.FT.TotCost, 2).ToString();

                }
                #region 2007-8-24 liuq ����С��������
                feeItemList.Item.MinFee.Name = minFeeName;
                #endregion
            }

            if (this.fpSpread1_Sheet1.Rows.Count > 0)
            {
                this.fpSpread1_Sheet1.Rows.Remove(0, this.fpSpread1_Sheet1.Rows.Count);
            }

            if (alMinFee.Count > 0)
            {
                this.fpSpread1_Sheet1.Rows.Add(0, alMinFee.Count / 5 + 1);
            }

            for (int i = 0; i < alMinFee.Count; i++)
            {
                this.fpSpread1_Sheet1.Cells[(i + 1) / 5, 2 * (i % 5)].Text = (alMinFee[i] as NeuObject).Name;
                this.fpSpread1_Sheet1.Cells[(i + 1) / 5, 2 * (i % 5) + 1].Text = (alMinFee[i] as NeuObject).Memo;
            }
        }

        /// <summary>
        /// ��ʼ���ַ�Ʊ��Ϣ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int InitSplitInvoice()
        {
            string tmpCtrlValue = null;

            tmpCtrlValue = this.feeIntegrate.GetControlValue(FS.HISFC.BizProcess.Integrate.Const.CANSPLIT, "0");

            if (tmpCtrlValue == null || tmpCtrlValue == "-1" || tmpCtrlValue == string.Empty)
            {
                MessageBox.Show("�Ƿ�ַ�Ʊ����û��ά�������ڲ���Ĭ��ֵ: ���ɷַ�Ʊ!");

                tmpCtrlValue = "0";
            }

            this.isCanSplit = NConvert.ToBoolean(tmpCtrlValue);

            this.rbAuto.Enabled = isCanSplit;
            this.rbMun.Enabled = isCanSplit;
            this.tbCount.Enabled = isCanSplit;
            this.btnSplit.Enabled = isCanSplit;
            this.tbDefault.Enabled = isCanSplit;

            this.splitCounts = this.controlParam.GetControlParam<int>(FS.HISFC.BizProcess.Integrate.Const.SPLITCOUNTS, false, 9);

            bool isCanModifyInvoiceDate = this.controlParam.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.CAN_MODIFY_INVOICE_DATE, false, false);

            if (!isCanModifyInvoiceDate)//�������޸ķ�Ʊ����
            {
                this.tbSplitDay.Text = "0";
                this.tbSplitDay.Enabled = false;
            }
            else
            {
                this.tbSplitDay.Text = "1";
                this.tbSplitDay.Enabled = true;
            }

            return 1;
        }

        /// <summary>
        /// ��ʼ��farpoint,����һЩ�ȼ�
        /// </summary>
        private void InitFp()
        {
            InputMap im;
            im = this.fpPayMode.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpPayMode.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Down, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpPayMode.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Up, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpPayMode.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Escape, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpPayMode.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Back, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
        }

        /// <summary>
        /// ��֤֧����ʽ�����Ƿ�Ϸ�
        /// </summary>
        /// <param name="errText">������Ϣ</param>
        /// <param name="errRow">������</param>
        /// <param name="errCol">������</param>
        /// <returns>�ɹ� true ����false</returns>
        private bool IsPayModesValid(ref string errText, ref int errRow, ref int errCol)
        {
            string tempPayMode = string.Empty;
            //��֤���;
            decimal tempCost = 0;
            decimal cardPayCost = 0;//ҽ����֧�����
            string tmpPayCost = string.Empty;

            for (int i = 0; i < this.fpPayMode_Sheet1.RowCount; i++)
            {
                tempPayMode = this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text;
                tmpPayCost = this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Text;
                if (tempPayMode == string.Empty || tmpPayCost == string.Empty)
                {
                    continue;
                }
                tempPayMode = tempPayMode.Trim();
                string tempId = helpPayMode.GetID(tempPayMode);
                if (tempId == null || tempId == string.Empty)
                {
                    errText = "֧����ʽ�������!";
                    errRow = i;
                    errCol = (int)PayModeCols.PayMode;

                    return false;
                }
                #region MyRegion

                if (tempPayMode == "��Ʊ")
                {
                    if (this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Bank].Text == string.Empty)
                    {
                        errText = "֧����ʽ" + tempPayMode + "û������������Ϣ";
                        errRow = i;
                        errCol = (int)PayModeCols.Bank;

                        return false;
                    }
                    if (this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Account].Text == string.Empty)
                    {
                        errText = "֧����ʽ" + tempPayMode + "û�������ʺ���Ϣ";
                        errRow = i;
                        errCol = (int)PayModeCols.Account;

                        return false;
                    }
                }
                if (tempPayMode == "֧Ʊ")
                {
                    if (this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Account].Text == string.Empty)
                    {
                        errText = "֧����ʽ" + tempPayMode + "û�������ʺ���Ϣ";
                        errRow = i;
                        errCol = (int)PayModeCols.Account;

                        return false;
                    }
                }
                #endregion

                if (tempPayMode == "�����ʻ�" || tempPayMode == "�籣��")
                {
                    cardPayCost += NConvert.ToDecimal(
                        this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value.ToString());
                }
                try
                {
                    tempCost += NConvert.ToDecimal(
                        this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("������벻�Ϸ�" + ex.Message);
                    errRow = i;
                    errCol = (int)PayModeCols.Account;

                    return false;
                }
            }

            if (!isSICanUserCardPayAll)
            {
                if (cardPayCost > this.payCost)
                {
                    errText = "ҽ����������֧���ԷѲ���!����֤������";

                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// ���֧����ʽ�ļ���
        /// </summary>
        /// <returns>�ɹ� ֧����ʽ�ļ��� ʧ�� null</returns>
        private ArrayList QueryBalancePays()
        {
            ArrayList balancePays = new ArrayList();
            BalancePay balancePay = null;

            for (int i = 0; i < this.fpPayMode_Sheet1.RowCount; i++)
            {
                if (this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text == string.Empty)
                {
                    continue;
                }
                if (this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Text == string.Empty)
                {
                    continue;
                }
                if (NConvert.ToDecimal
                    (this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value) == 0)
                {
                    continue;
                }
                balancePay = new BalancePay();

                balancePay.PayType.Name = this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text;
                balancePay.PayType.ID = helpPayMode.GetID(balancePay.PayType.Name);
                if (balancePay.PayType.ID == null || balancePay.PayType.ID.ToString() == string.Empty)
                {
                    return null;
                }
                //balancePay.FT.TotCost = totOwnCost; // NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value.ToString());
                balancePay.FT.TotCost = NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value.ToString());
                if (balancePay.PayType.Name == "�ֽ�")
                {
                    balancePay.FT.RealCost = Class.Function.DealCent(balancePay.FT.TotCost);
                }
                else
                {
                    balancePay.FT.RealCost = balancePay.FT.TotCost;
                }
                balancePay.Bank.Name = this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Bank].Text;
                balancePay.Bank.ID = helpBank.GetID(balancePay.Bank.Name);
                balancePay.Bank.Account = this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Account].Text;
                if (balancePay.PayType.Name == "֧Ʊ" || balancePay.PayType.Name == "��Ʊ")
                {
                    balancePay.Bank.InvoiceNO = this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PosNo].Text;
                }
                else
                {
                    balancePay.POSNO = this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PosNo].Text;
                }
                if (balancePay.PayType.ID.ToString() == "CA")
                {
                    balancePay.FT.RealCost = FS.HISFC.Components.OutpatientFee.Class.Function.DealCent(balancePay.FT.RealCost);
                }
                else
                {
                    balancePay.FT.RealCost = balancePay.FT.RealCost;
                }
                if (balancePay.PayType.ID.ToString() == "YS" || balancePay.PayType.ID.ToString() == "DC")
                {
                    balancePay.IsEmpPay = this.ucPayingAgentMessControl.IsPayForAnother;
                    balancePay.EmpowerPatient = this.ucPayingAgentMessControl.EmpowerPatient;
                    balancePay.AccountTypeCode = this.ucPayingAgentMessControl.AccountTypeCode;
                }
                balancePays.Add(balancePay);
            }

            return balancePays;
        }

        /// <summary>
        /// ��֤�ַ�Ʊ�����Ƿ�Ϸ�
        /// </summary>
        /// <returns>�ɹ� true ʧ�� false</returns>
        private bool IsSplitInvoicesValid()
        {
            decimal tempTotCost = 0;

            for (int i = 0; i < this.fpSplit_Sheet1.RowCount; i++)
            {
                if (this.fpSplit_Sheet1.Cells[i, 2].Text == "�ܷ�Ʊ")
                {
                    continue;
                }
                try
                {
                    //tempTotCost += NConvert.ToDecimal(this.fpSplit_Sheet1.Cells[i, 1].Text);
                    //�ſ����޸�
                    tempTotCost += NConvert.ToDecimal(this.fpSplit_Sheet1.Cells[i, 3].Text) + NConvert.ToDecimal(this.fpSplit_Sheet1.Cells[i, 4].Text) + NConvert.ToDecimal(this.fpSplit_Sheet1.Cells[i, 5].Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("������벻�Ϸ�!" + ex.Message);

                    return false;
                }
            }

            if (FS.FrameWork.Public.String.FormatNumber(tempTotCost, 2) != this.totCost)
            {
                MessageBox.Show("�ַ�Ʊ������ܽ���!�����·���!");

                return false;
            }

            return true;
        }

        /// <summary>
        /// ��÷ַ�Ʊ��Ϣ
        /// </summary>
        /// <returns>�ɹ� �ַ�Ʊ��Ϣ ʧ�� null</returns>
        protected ArrayList QuerySplitInvoices()
        {
            NeuObject obj = null;
            ArrayList objs = new ArrayList();

            if (this.pactInfo.ID == "01")//�Էѷ�Ʊ
            {
                for (int i = 0; i < this.fpSplit_Sheet1.RowCount; i++)
                {
                    obj = new NeuObject();
                    obj.ID = i.ToString();
                    obj.User01 = this.fpSplit_Sheet1.Cells[i, 1].Text;
                    objs.Add(obj);
                }
            }
            else //���Ѻ�ҽ��
            {
                obj = new NeuObject();
                obj.User01 = ownCost.ToString();
                obj.User02 = payCost.ToString();
                obj.User03 = pubCost.ToString();
                objs.Add(obj);
            }

            return objs;
        }

        /// <summary>
        /// ������
        /// </summary>
        /// <returns>�ɹ� ture ʧ�� false</returns>
        private bool ComputCost()
        {
            decimal tmpCost = 0;
            decimal realCode = this.isDealCentNoAllCancy == true ? NConvert.ToDecimal(Class.Function.DealCent(this.totOwnCost)) : NConvert.ToDecimal(this.totOwnCost);
            for (int i = 0; i < this.fpPayMode_Sheet1.RowCount; i++)
            {
                tmpCost += NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value);
                if (tmpCost > realCode)
                {
                    MessageBox.Show("������ܴ��ڿɲ���Էѽ��!");
                    this.fpPayMode.Focus();
                    this.fpPayMode_Sheet1.SetActiveCell(i, (int)PayModeCols.Cost, false);

                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// ��ʼ��������Ϣ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int InitBanks()
        {
            lbBank.AddItems(alBanks);
            Controls.Add(lbBank);
            lbBank.Hide();
            lbBank.BorderStyle = BorderStyle.FixedSingle;
            lbBank.BringToFront();
            lbBank.SelectItem += new FS.FrameWork.WinForms.Controls.PopUpListBox.MyDelegate(lbBank_SelectItem);

            return 1;
        }

        /// <summary>
        /// ��ʼ��֧����ʽ��Ϣ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int InitPayMode()
        {
            #region ����-����PAYMODES�е�ά��

            //ArrayList alPayModesClone = (ArrayList)alPayModes.Clone();
            //NeuObject objCA = null;
            //NeuObject objJZ = null;

            //foreach (NeuObject obj in alPayModesClone)
            //{
            //    if (obj.Name == "�ֽ�")
            //    {
            //        objCA = obj;
            //    }

            //    if (obj.ID == "JZ")
            //    {
            //        objJZ = obj;
            //    }
            //}

            //alPayModesClone.Remove(objCA);

            //if (this.trans != null)
            //{
            //    this.accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //}

            ////�ж��Ƿ���������˻�
            //decimal vacancy = 0;

            //int returnValue = this.accountManager.GetVacancy(this.rInfo.PID.CardNO, ref vacancy);
            //if (returnValue == -1)
            //{
            //    MessageBox.Show(this.accountManager.Err);

            //    return -1;
            //}
            ////������������˻�,�����˻�������0, ��Ҫ���Էѽ�� ����0, ��ô��ʾԺ���˻�֧����ʽ
            //if (returnValue > 0 && vacancy > 0 && this.TotOwnCost > 0)
            //{
            //    this.fpPayMode_Sheet1.Cells[5, (int)PayModeCols.PayMode].Text = "�ʻ�֧��";
            //}
            //else//����ȥ�������˻�֧����ʽ��ѡ������
            //{
            //    foreach (NeuObject obj in alPayModesClone)
            //    {
            //        if (obj.Name == "�ʻ�֧��")
            //        {
            //            objCA = obj;
            //        }
            //    }
            //}
            //alPayModesClone.Remove(objCA);


            //if (keepAccountPatient != null)
            //{
            //    isKeepAccountPatient = keepAccountPatient.IsKeepAccountPatient(this.rInfo);
            //    if (isKeepAccountPatient)
            //    {
            //        if (objJZ == null)
            //        {
            //            objJZ = new NeuObject();
            //            objJZ.ID = "JZ";
            //            objJZ.Name = "����";

            //            alPayModes.Add(objJZ);
            //        }

            //        this.fpPayMode_Sheet1.Cells[4, (int)PayModeCols.PayMode].Text = objJZ.Name;
            //        this.fpPayMode_Sheet1.Cells[4, (int)PayModeCols.PayMode].Tag = objJZ.ID;
            //        this.fpPayMode_Sheet1.Cells[4, (int)PayModeCols.PayMode].Locked = true;
            //    }
            //}


            //lbPayMode.AddItems(alPayModesClone);
            //Controls.Add(lbPayMode);
            //lbPayMode.Hide();
            //lbPayMode.BorderStyle = BorderStyle.FixedSingle;
            //lbPayMode.BringToFront();
            //lbPayMode.SelectItem += new FS.FrameWork.WinForms.Controls.PopUpListBox.MyDelegate(lbPayMode_SelectItem);
            //this.fpPayMode_Sheet1.Cells[0, (int)PayModeCols.PayMode].Text = "�ֽ�";
            //this.fpPayMode_Sheet1.Cells[1, (int)PayModeCols.PayMode].Text = "��ǿ�";
            //this.fpPayMode_Sheet1.Cells[2, (int)PayModeCols.PayMode].Text = "���ÿ�";
            //this.fpPayMode_Sheet1.Cells[3, (int)PayModeCols.PayMode].Text = "֧Ʊ";
 

            //this.fpPayMode_Sheet1.Cells[0, (int)PayModeCols.PayMode].Locked = true;
            //this.fpPayMode_Sheet1.Cells[1, (int)PayModeCols.PayMode].Locked = true;
            //this.fpPayMode_Sheet1.Cells[2, (int)PayModeCols.PayMode].Locked = true;
            //this.fpPayMode_Sheet1.Cells[3, (int)PayModeCols.PayMode].Locked = true;


            ////֧����ʽ��������4������ά����.
            //int index = 4;
            //for (int i = 0; i < alPayModes.Count; i++)
            //{
            //    FS.FrameWork.Models.NeuObject obj = new NeuObject();
            //    obj = alPayModes[i] as NeuObject;
            //    if (obj.Name == "�ֽ�" || obj.Name == "��ǿ�" ||
            //        obj.Name == "���ÿ�" || obj.Name == "֧Ʊ" ||
            //        obj.Name == "�ʻ�֧��" || obj.ID == "CA" || obj.ID == "CD" ||
            //        obj.ID == "DB" || obj.ID == "CH" ||
            //        obj.ID == "JZ" || obj.ID == "YS")
            //    {
            //        continue;
            //    }

            //    if (index > 9)
            //    {
            //        this.fpPayMode_Sheet1.Rows.Count++;
            //    }

            //    this.fpPayMode_Sheet1.Cells[index, (int)PayModeCols.PayMode].Text = obj.Name;
            //    this.fpPayMode_Sheet1.Cells[index, (int)PayModeCols.PayMode].Tag = obj.ID;
            //    this.fpPayMode_Sheet1.Cells[index, (int)PayModeCols.PayMode].Locked = true;
            //    index++;

            //}
            #endregion
            //�����Ϣ
            this.currentAccountInfo = null;   //����˻���Ϣ
            //this.lblAccountInfo.Text = string.Empty;

            //HIS��ά����֧����ʽ
            ArrayList alPayModesClone = (ArrayList)alPayModes.Clone();

            lbPayMode.AddItems(alPayModesClone);
            Controls.Add(lbPayMode);
            lbPayMode.Hide();
            lbPayMode.BorderStyle = BorderStyle.FixedSingle;
            lbPayMode.BringToFront();
            lbPayMode.SelectItem += new FS.FrameWork.WinForms.Controls.PopUpListBox.MyDelegate(lbPayMode_SelectItem);

            //֧����ʽ
            for (int i = 0; i < alPayModes.Count; i++)
            {
                NeuObject obj = alPayModes[i] as NeuObject;

                //�ֽ�֧��
                if (obj.ID == "CA")
                {
                    this.rowCA = i;
                }

                //ACY(�ʻ�֧��)
                if ((obj as Models.Base.Const).UserCode == "ACY")
                {
                    this.rowACY = i;
                }
                //ACD(�ʻ�����)
                if ((obj as Models.Base.Const).UserCode == "ACD")
                {
                    this.rowACD = i;
                }

                //ECO(�Ż�)
                if ((obj as Models.Base.Const).UserCode == "ECO")
                {
                    this.rowECO = i;
                }
            }
            if (this.rowCA < 0)
            {
                MessageBox.Show("����ϵ��Ϣ��ά���ֽ�֧����ʽCA!");
            }

            //֧����ʽ��ֵ
            this.fpPayMode_Sheet1.Rows.Count = 0;  //���
            for (int i = 0; i < alPayModes.Count; i++)
            {
                //����1��
                this.fpPayMode_Sheet1.Rows.Count++;

                NeuObject obj = alPayModes[i] as NeuObject;

                this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text = obj.Name;   //֧����ʽ����
                this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Tag = obj.ID;     //֧����ʽ����
                this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Locked = true;

            }

            //֧����ʽ�е�USER_CODE��ECO(�Ż�)��ACY(�ʻ�֧��)��ACD(�ʻ�����)

            #region �˻�����

            //�ж��Ƿ���������˻�
            if (this.trans != null)
            {
                this.accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            }

            this.currentAccountInfo = this.accountManager.GetAccountByCardNoEX(this.rInfo.PID.CardNO);

            if (this.currentAccountInfo != null && !string.IsNullOrEmpty(this.currentAccountInfo.ID))
            {
                this.accountDetailList = this.accountManager.GetAccountDetail(this.currentAccountInfo.ID, this.ucPayingAgentMessControl.AccountTypeCode,"1");
                if (accountDetailList.Count > 0)
                {
                    this.currentAccountDetail = accountDetailList[0] as FS.HISFC.Models.Account.AccountDetail;
                }
                decimal baseVacancy = this.currentAccountDetail.BaseVacancy;          //��ֵ�˻����
                decimal donateVacancy = this.currentAccountDetail.DonateVacancy;    //�����˻����

                //������������˻�,�����˻�������0, ��Ҫ���Էѽ�� ����0, ��ô��ʾԺ���˻�֧����ʽ
                if (baseVacancy > 0 && this.TotOwnCost > 0)
                {
                    if (this.rowACY >= 0)
                    {
                        //ACY(�ʻ�֧��)
                        this.fpPayMode_Sheet1.Cells[this.rowACY, (int)PayModeCols.Cost].Locked = true;
                    }
                    if (this.rowACD >= 0)
                    {
                        //ACD(�ʻ�����)
                        this.fpPayMode_Sheet1.Cells[this.rowACD, (int)PayModeCols.Cost].Locked = true;
                    }
                }
                else
                {
                    //����ȥ�������˻�֧����ʽ��ѡ�����ݣ���Ϊ����ѡ��
                    if (this.rowACY >= 0)
                    {
                        //ACY(�ʻ�֧��)
                        this.fpPayMode_Sheet1.Cells[this.rowACY, (int)PayModeCols.Cost].Locked = true;
                    }
                    if (this.rowACD >= 0)
                    {
                        //ACD(�ʻ�����)
                        this.fpPayMode_Sheet1.Cells[this.rowACD, (int)PayModeCols.Cost].Locked = true;
                    }
                }

                string accInfo = "{0}�������˻���{1}�������˻���{2}";
                //NeuObject obj = this.accLevelHelper.GetObjectFromID(this.currentAccountInfo.AccountLevel.ID);
                //this.lblAccountInfo.Text = string.Format(accInfo, (obj != null ? obj.Name : ""), baseVacancy.ToString("F2"), donateVacancy.ToString("F2"));
            }
            else
            {
                //����ȥ�������˻�֧����ʽ��ѡ�����ݣ���Ϊ����ѡ��
                if (this.rowACY >= 0)
                {
                    //ACY(�ʻ�֧��)
                    this.fpPayMode_Sheet1.Cells[this.rowACY, (int)PayModeCols.Cost].Locked = true;
                }
                if (this.rowACD >= 0)
                {
                    //ACD(�ʻ�����)
                    this.fpPayMode_Sheet1.Cells[this.rowACD, (int)PayModeCols.Cost].Locked = true;
                }

                //this.lblAccountInfo.Text = "�ǻ�Ա";
            }

            //������
            if (this.rowACY >= 0)
            {
                //ACY(�ʻ�֧��)
                this.fpPayMode_Sheet1.Cells[this.rowACY, (int)PayModeCols.Cost].Locked = false;
            }
            if (this.rowACD >= 0)
            {
                //ACD(�ʻ�����)
                this.fpPayMode_Sheet1.Cells[this.rowACD, (int)PayModeCols.Cost].Locked = false;
            }


            #endregion

            #region �Ż�

            if (this.rowECO >= 0)
            {
                //�Żݴ���
            }

            #endregion

            #region ����

            if (keepAccountPatient != null)
            {
                isKeepAccountPatient = keepAccountPatient.IsKeepAccountPatient(this.rInfo);
                if (isKeepAccountPatient)
                {
                    //���˴���ʽ
                }
            }

            #endregion

            return 1;
        }

        /// <summary>
        /// ����λ��
        /// </summary>
        private void SetLocation()
        {
            if (this.fpPayMode_Sheet1.ActiveColumnIndex == (int)PayModeCols.PayMode)
            {
                Control cell = this.fpPayMode.EditingControl;
                lbPayMode.Location = new Point(this.fpPayMode.Location.X + cell.Location.X + 4,
                    this.panel1.Location.Y + this.tabControl1.Location.Y + this.fpPayMode.Location.Y + cell.Location.Y + cell.Height * 2 + SystemInformation.Border3DSize.Height * 2);
                lbPayMode.Size = new Size(cell.Width + 50 + SystemInformation.Border3DSize.Width * 2, 150);
                if (lbPayMode.Location.Y + lbPayMode.Height > this.fpPayMode.Location.Y + this.fpPayMode.Height)
                {
                    lbPayMode.Location = new Point(this.fpPayMode.Location.X + cell.Location.X + 4,
                        this.panel1.Location.Y + this.tabControl1.Location.Y + this.fpPayMode.Location.Y + cell.Location.Y + cell.Height * 2 + SystemInformation.Border3DSize.Height * 2
                        - lbPayMode.Size.Height - cell.Height);
                }
            }
            if (this.fpPayMode_Sheet1.ActiveColumnIndex == (int)PayModeCols.Bank)
            {
                Control cell = this.fpPayMode.EditingControl;
                lbBank.Location = new Point(this.fpPayMode.Location.X + cell.Location.X + 4,
                    this.panel1.Location.Y + this.tabControl1.Location.Y + this.fpPayMode.Location.Y + cell.Location.Y + cell.Height * 2 + SystemInformation.Border3DSize.Height * 2);
                lbBank.Size = new Size(cell.Width + 200 + SystemInformation.Border3DSize.Width * 2, 150);
                if (lbBank.Location.Y + lbBank.Height > this.fpPayMode.Location.Y + this.fpPayMode.Height)
                {
                    lbBank.Location = new Point(this.fpPayMode.Location.X + cell.Location.X + 4,
                        this.panel1.Location.Y + this.tabControl1.Location.Y + this.fpPayMode.Location.Y + cell.Location.Y + cell.Height * 2 + SystemInformation.Border3DSize.Height * 2
                        - lbBank.Size.Height - cell.Height);
                }
            }
        }

        /// <summary>
        /// ֧����ʽ�Ļس�
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int ProcessPayMode()
        {
            int currRow = this.fpPayMode_Sheet1.ActiveRowIndex;
            if (currRow < 0)
            {
                return 0;
            }
            NeuObject item = null;

            int returnValue = lbPayMode.GetSelectedItem(out item);
            if (returnValue == -1)
            {
                return -1;
            }
            if (item == null)
            {
                return -1;
            }

            fpPayMode_Sheet1.SetValue(currRow, (int)PayModeCols.PayMode, item.Name);
            fpPayMode.StopCellEditing();
            decimal nowCost = 0;
            decimal currCost = 0;
            bool isOnlyCash = true;

            for (int i = 0; i < this.fpPayMode_Sheet1.RowCount; i++)
            {
                if (this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text != string.Empty)
                {
                    if (i == 0)
                    {
                        continue;
                    }

                    nowCost += NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value);
                }
            }

            currCost = NConvert.ToDecimal(this.tbTotOwnCost.Text) - nowCost;
            this.fpPayMode_Sheet1.Cells[0, (int)PayModeCols.Cost].Text = currCost.ToString();

            nowCost = 0;
            for (int i = 0; i < this.fpPayMode_Sheet1.RowCount; i++)
            {
                if (this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text != string.Empty)
                {
                    if (this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text != "�ֽ�")
                    {
                        isOnlyCash = false;
                    }
                    if (i == currRow)
                    {
                        continue;
                    }

                    nowCost += NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value);
                }
            }

            if (isOnlyCash)
            {
                currCost = this.TotOwnCost - nowCost;
                this.tbTotOwnCost.Text = FS.FrameWork.Public.String.FormatNumber(TotOwnCost, 2).ToString();
            }
            else
            {
                currCost = this.realCost - nowCost;
                this.tbTotOwnCost.Text = FS.FrameWork.Public.String.FormatNumber(realCost, 2).ToString();
            }

            this.fpPayMode_Sheet1.Cells[currRow, (int)PayModeCols.Cost].Value = currCost;

            this.lbPayMode.Visible = false;

            return 1;
        }

        /// <summary>
        /// ����������Ϣ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int ProcessPayBank()
        {
            if (lbBank.Visible == false)
            {
                return -1;
            }
            int currRow = this.fpPayMode_Sheet1.ActiveRowIndex;
            if (currRow < 0)
            {
                return 0;
            }
            NeuObject item = null;
            int returnValue = lbBank.GetSelectedItem(out item);
            if (returnValue == -1)
            {
                return -1;
            }
            if (item == null)
            {
                return -1;
            }

            fpPayMode.StopCellEditing();
            fpPayMode_Sheet1.SetValue(currRow, (int)PayModeCols.Bank, item.Name);
            this.lbBank.Visible = false;

            return 1;
        }

        #endregion

        /// <summary>
        /// ���ƽ���
        /// </summary>
        public void SetControlFocus()
        {
            this.panel1.Focus();
            this.groupBox2.Focus();
            this.tbRealCost.Focus();
        }

        /// <summary>
        /// ��ʼ����Ϣ
        /// </summary>
        /// <returns></returns>
        public int Init()
        {
            //��ʼ��FarPoint��Ϣ
            this.panel4.Controls.Add((Control)this.ucPayingAgentMessControl);
            this.ucPayingAgentMessControl.CardNo = this.rInfo.PID.CardNO;
            int returnValue = this.ucPayingAgentMessControl.Init();
            if (returnValue < 0)
            {
               return -1;
            }
            this.InitFp();

            isKeepAccountPatient = false;
            keepAccountPatient = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject<
                           FS.HISFC.BizProcess.Interface.Fee.IKeepAccountPatient>(this.GetType());

            //��ʼ��֧����ʽ��Ϣ{93E6443C-1FB5-45a7-B89D-F21A92200CF6}
            //alPayModes = FS.HISFC.Models.Fee.EnumPayTypeService.List();
            alPayModes = this.managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PAYMODES);
            if (alPayModes == null || alPayModes.Count <= 0)
            {
                MessageBox.Show("��ȡ֧����ʽ����");

                return -1;
            }
            this.InitPayMode();

            //��ʼ��������Ϣ
            if (trans != null)
            {
                this.managerIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            }
            alBanks = this.managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.BANK);
            if (alBanks == null || alBanks.Count <= 0)
            {
                MessageBox.Show("��ȡ�����б�ʧ��!");

                return -1;
            }
            InitBanks();

            NeuObject objCA = new NeuObject();
            objCA.ID = "CA";
            objCA.Name = "�ֽ�";

            NeuObject objCD = new NeuObject();
            objCD.ID = "CD";
            objCD.Name = "���ÿ�";

            NeuObject objDB = new NeuObject();
            objDB.ID = "DB";
            objDB.Name = "��ǿ�";

            NeuObject objCH = new NeuObject();
            objCH.ID = "CH";
            objCH.Name = "֧Ʊ";


            helpPayMode.ArrayObject = alPayModes;

            if (helpPayMode.GetObjectFromID(objCA.ID) == null)
            {
                helpPayMode.ArrayObject.Add(objCA);
            }
            else
            {
                helpPayMode.GetObjectFromID(objCA.ID).Name = objCA.Name;
            }

            if (helpPayMode.GetObjectFromID(objCD.ID) == null)
            {
                helpPayMode.ArrayObject.Add(objCD);
            }
            else
            {
                helpPayMode.GetObjectFromID(objCD.ID).Name = objCD.Name;
            }

            if (helpPayMode.GetObjectFromID(objDB.ID) == null)
            {
                helpPayMode.ArrayObject.Add(objDB);
            }
            else
            {
                helpPayMode.GetObjectFromID(objDB.ID).Name = objDB.Name;
            }

            helpBank.ArrayObject = alBanks;

            //��ʼ���ַ�Ʊ��Ϣ
            this.InitSplitInvoice();

            //��ʼ����С�����б�
            ArrayList alMinFeeList = this.managerIntegrate.GetConstantList("MINFEE");
            if (alMinFeeList != null)
            {
                this.helpMinFeeList.ArrayObject = alMinFeeList;
            }
            bool autoBankTrans = this.controlParam.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.CAN_MODIFY_INVOICE_DATE, false, false);

            string tempControlValue = this.feeIntegrate.GetControlValue(FS.HISFC.BizProcess.Integrate.Const.CANUSEMCARND, "0");

            this.isSICanUserCardPayAll = NConvert.ToBoolean(tempControlValue);

            string fpVisible = this.feeIntegrate.GetControlValue(FS.HISFC.BizProcess.Integrate.Const.MINFEE_DISPLAY_WHENFEE, "0");

            this.fpSpread1.Visible = NConvert.ToBoolean(fpVisible);

            string modifyDate = this.feeIntegrate.GetControlValue(FS.HISFC.BizProcess.Integrate.Const.MODIFY_INVOICE_PRINTDATE, "0");

            this.isModifyDate = NConvert.ToBoolean(modifyDate);

            this.currentAccountInfo = this.accountManager.GetAccountByCardNoEX(this.rInfo.PID.CardNO);

            if (this.isModifyDate == true)
            {
                this.dateTimePicker1.Enabled = true;
            }
            else
            {
                this.dateTimePicker1.Enabled = false;
            }

            this.isDisplayCashOnly = NConvert.ToBoolean(this.feeIntegrate.GetControlValue(FS.HISFC.BizProcess.Integrate.Const.CASH_ONLY_WHENFEE, "0"));

            this.helpMinFee = new FS.FrameWork.Public.ObjectHelper();

            return 1;
        }

        /// <summary>
        /// �շѱ���
        /// </summary>
        /// <returns>�ɹ� ture ʧ�� false</returns>
        public bool SaveFee()
        {
            string errText = string.Empty;
            int errRow = 0, errCol = 0;
            this.GetFT();
            if (!this.IsPayModesValid(ref errText, ref errRow, ref errCol))
            {
                MessageBox.Show(errText, "��ʾ");
                this.fpPayMode.Focus();
                this.fpPayMode_Sheet1.SetActiveCell(errRow, errCol, false);

                return false;
            }

            alPatientPayModeInfo = QueryBalancePays();
            if (alPatientPayModeInfo == null)
            {
                MessageBox.Show("���֧����ʽ��Ϣ����!", "��ʾ");

                return false;
            }
            if (!this.IsSplitInvoicesValid())
            {
                this.fpSplit.Focus();

                return false;
            }

            foreach (BalancePay p in alPatientPayModeInfo)
            {
                if (p.PayType.ID == "YS" || p.PayType.ID == "DC")// {FE32F3CC-19B9-49f3-B073-D61235DB11B0}
                {
                    if (this.ucPayingAgentMessControl.IsPayForAnother)
                    {
                        if (this.ucPayingAgentMessControl.EmpowerPatient == null)
                        {
                            MessageBox.Show("����������˵ľ��￨�ţ�");
                            return false;
                        }
                        if (string.IsNullOrEmpty(this.ucPayingAgentMessControl.AccountTypeCode))
                        {
                            MessageBox.Show("��ѡ���˻����ͣ�");
                            return false;

                        }
                        if (!feeIntegrate.CheckAccountPassWord(this.ucPayingAgentMessControl.EmpowerPatient))
                        {
                            MessageBox.Show("�������벻��ȷ��");
                            return false;
                        }

                        this.currentAccountInfo = this.ucPayingAgentMessControl.CurrentAccountInfo;
                        if (currentAccountInfo == null)
                        {
                            MessageBox.Show("�Ҳ����������˻���Ϣ����ȷ���������Ƿ�����˻���");
                            return false;

                        }
                        accountDetailList = this.accountManager.GetAccountDetail(currentAccountInfo.ID, this.ucPayingAgentMessControl.AccountTypeCode,"1");
                        if (accountDetailList.Count > 0)
                        {
                            this.currentAccountDetail = accountDetailList[0] as FS.HISFC.Models.Account.AccountDetail;
                            if (p.PayType.ID == "YS" && p.FT.TotCost > currentAccountDetail.BaseVacancy)
                            {
                                MessageBox.Show("�����˻����㣡");
                                return false;
                            }
                            if (p.PayType.ID == "DC" && p.FT.TotCost > currentAccountDetail.DonateVacancy)
                            {
                                MessageBox.Show("�����˻����㣡");
                                return false;

                            }
                        }
                        else
                        {
                            MessageBox.Show("�Ҳ����������˻���Ϣ����ȷ���������Ƿ������˻����ͣ�");
                            return false;
                        }
                        
                    }
                    else
                    {
                        if (!feeIntegrate.CheckAccountPassWord(this.rInfo))
                        {
                            MessageBox.Show("�������벻��ȷ��");
                            return false;
                        }
                    }
                    break;
                }
            }

            ArrayList alTempInvoices = new ArrayList();
            ArrayList alTempInvoiceDetals = new ArrayList();
            ArrayList alTempInvoiceDetailsSec = new ArrayList();
            ArrayList alTempInvoiceFeeItemDetals = new ArrayList();
            ArrayList alTempInvoiceFeeItemDetalsSec = new ArrayList();
            Balance invoiceTemp = new Balance();

            for (int i = 0; i < this.fpSplit_Sheet1.RowCount; i++)
            {
                invoiceTemp = this.fpSplit_Sheet1.Rows[i].Tag as Balance;
                alTempInvoices.Add(invoiceTemp);
                ArrayList tempArrayListTempInvoiceDetails = this.fpSplit_Sheet1.Cells[i, 0].Tag as ArrayList;
                alTempInvoiceDetailsSec.Add(tempArrayListTempInvoiceDetails);
                #region liuq 2007-8-27 ׷�Ӷ�Ӧ������ϸ
                ArrayList tempArrayListTempInvoiceFeeItemDetals = this.fpSplit_Sheet1.Cells[i, 3].Tag as ArrayList;
                alTempInvoiceFeeItemDetalsSec.Add(tempArrayListTempInvoiceFeeItemDetals);
                #endregion
            }

            alTempInvoiceDetals.Add(alTempInvoiceDetailsSec);
            #region liuq 2007-8-27 ׷�Ӷ�Ӧ������ϸ
            alTempInvoiceFeeItemDetals.Add(alTempInvoiceFeeItemDetalsSec);
            #endregion

            //{283BD55B-83E0-4f35-86E8-152CF0C1DA85} ��������
            this.FeeButtonClicked(alPatientPayModeInfo, alTempInvoices, alTempInvoiceDetals, alTempInvoiceFeeItemDetals);

            return true;
        }

        /// <summary>
        /// ���۱���
        /// </summary>
        /// <returns>�ɹ� true ʧ�� false</returns>
        public bool SaveCharge()
        {
            this.ChargeButtonClicked();

            return true;
        }

        #endregion

        private FS.HISFC.Models.Base.FT GetFT()
        {
            //this.tbLeast.Text = tempCost.ToString();

            //this.frmDisplay.RInfo = this.rInfo;
            FS.HISFC.Models.Base.FT feeInfo = new FS.HISFC.Models.Base.FT();
            feeInfo.TotCost = totCost;
            feeInfo.OwnCost = ownCost;
            feeInfo.PayCost = payCost;
            feeInfo.PubCost = pubCost;
            feeInfo.BalancedCost = NConvert.ToDecimal(tbTotOwnCost.Text);
            feeInfo.SupplyCost = totOwnCost;
            feeInfo.RealCost = NConvert.ToDecimal(this.tbRealCost.Text);
            feeInfo.ReturnCost = NConvert.ToDecimal(this.tbLeast.Text);
            this.ftFeeInfo = feeInfo;
            return this.ftFeeInfo;
        }
        #region �¼�

        /// <summary>
        /// Load�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void frmDealBalance_Load(object sender, EventArgs e)
        {
            this.tbRealCost.Select();
            this.tbRealCost.Focus();
            this.tbRealCost.SelectAll();
            this.tbLeast.Text = "0";
            leastCost = 0;
        }

        /// <summary>
        /// ����շѰ�ť����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tbFee_Click(object sender, EventArgs e)
        {
            this.Tag = "�շ�";
            this.tbFee.Enabled = false;

            this.fpPayMode.StopCellEditing();

            if (!this.isPaySuccess)
            {
                return;
            }
            this.isPaySuccess = false;

            if (!this.SaveFee())
            {
                this.tbFee.Enabled = true;

                return;
            }
            this.tbFee.Enabled = true;
            this.tbRealCost.Focus();

            //����ȡ�������ҽ������ع�{AFEDD473-052A-4c8a-9EA4-9D002443DF52}
            isSuccessFee = true;

            this.Close();
        }

        /// <summary>
        /// ���۱��水ť����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tbCharge_Click(object sender, EventArgs e)
        {
            this.Tag = "���۱���";
            this.tbCharge.Enabled = false;
            this.SaveCharge();
            this.tbCharge.Enabled = true;
            this.tbRealCost.Focus();
            this.Close();
        }

        /// <summary>
        /// ���ȡ����ť����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tbCancel_Click(object sender, EventArgs e)
        {
            this.Tag = "ȡ��";
            isPushCancelButton = true;
            this.tbRealCost.Focus();
            this.Close();
        }

        /// <summary>
        /// ����ַ�ƱĬ�ϰ�ť����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tbDefault_Click(object sender, EventArgs e)
        {
            Invoices = this.alInvoices;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.F12)
            {
                this.panel1.Focus();
                this.groupBox2.Focus();
                this.tbRealCost.Focus();
                this.tbRealCost.SelectAll();
            }
            if (keyData == Keys.Add)
            {
                this.tbFee.Enabled = false;
                if (!this.SaveFee())
                {
                    this.tbFee.Enabled = true;
                    return false;
                }
                this.tbFee.Enabled = true;
                this.tbRealCost.Focus();
                this.Close();
            }
            if (keyData == Keys.F5)
            {
                this.tabControl1.SelectedTab = this.tpSplitInvoice;

                this.tpSplitInvoice.Focus();
                this.tbCount.Focus();
            }
            if (keyData == Keys.F6)
            {
                this.panel1.Focus();
                this.tabControl1.Focus();
                this.tabControl1.SelectedTab = this.tpPayMode;
                this.tpPayMode.Focus();
                this.fpPayMode.Focus();
                this.fpPayMode_Sheet1.ActiveRowIndex = 1;
                this.fpPayMode_Sheet1.SetActiveCell(1, (int)PayModeCols.Cost, false);
            }
            if (keyData == Keys.Escape)
            {

                if (lbPayMode.Visible)
                {
                    lbPayMode.Visible = false;
                    this.fpPayMode.StopCellEditing();
                }
                else if (lbBank.Visible)
                {
                    lbBank.Visible = false;
                    this.fpPayMode.StopCellEditing();
                }
                else
                {
                    this.tbRealCost.Focus();
                    this.isPushCancelButton = true;
                    this.Close();
                }
            }
            if (this.fpPayMode.ContainsFocus)
            {
                if (keyData == Keys.Up)
                {
                    if (lbPayMode.Visible)
                    {
                        lbPayMode.PriorRow();
                    }
                    else if (lbBank.Visible)
                    {
                        lbBank.PriorRow();
                    }
                    else
                    {
                        int currRow = this.fpPayMode_Sheet1.ActiveRowIndex;
                        if (currRow > 0)
                        {
                            this.fpPayMode_Sheet1.ActiveRowIndex = currRow - 1;
                            if (this.fpPayMode_Sheet1.Cells[currRow - 1, (int)PayModeCols.PayMode].Locked == true)
                            {
                                this.fpPayMode_Sheet1.SetActiveCell(currRow - 1, (int)PayModeCols.Cost);
                            }
                            else
                            {
                                this.fpPayMode_Sheet1.SetActiveCell(currRow - 1, (int)PayModeCols.PayMode);
                            }
                        }
                    }
                }
                if (keyData == Keys.Back)
                {
                    int currRow = this.fpPayMode_Sheet1.ActiveRowIndex;
                    int currCol = this.fpPayMode_Sheet1.ActiveColumnIndex;
                    if (this.fpPayMode_Sheet1.Cells[currRow, currCol].Text == string.Empty)
                    {
                        if (currCol == 0)
                        {

                            this.fpPayMode_Sheet1.SetActiveCell(currRow - 1, 0, false);
                        }
                        else
                        {
                            this.fpPayMode_Sheet1.SetActiveCell(currRow, currCol - 1, false);
                        }
                    }
                }
                if (keyData == Keys.Down)
                {
                    if (lbPayMode.Visible)
                    {
                        lbPayMode.NextRow();
                    }
                    else if (lbBank.Visible)
                    {
                        lbBank.NextRow();
                    }
                    else
                    {
                        int currRow = this.fpPayMode_Sheet1.ActiveRowIndex;

                        if (currRow <= 8)
                        {
                            this.fpPayMode_Sheet1.ActiveRowIndex = currRow + 1;
                            if (this.fpPayMode_Sheet1.Cells[currRow + 1, (int)PayModeCols.PayMode].Locked == true)
                            {
                                this.fpPayMode_Sheet1.SetActiveCell(currRow + 1, (int)PayModeCols.Cost);
                            }
                            else
                            {
                                this.fpPayMode_Sheet1.SetActiveCell(currRow + 1, (int)PayModeCols.PayMode);
                            }
                        }
                    }

                }
                if (keyData == Keys.Enter)
                {
                    int currRow = this.fpPayMode_Sheet1.ActiveRowIndex;
                    int currCol = this.fpPayMode_Sheet1.ActiveColumnIndex;
                    this.fpPayMode.StopCellEditing();
                    if (currCol == (int)PayModeCols.PayMode)
                    {
                        ProcessPayMode();
                        this.fpPayMode_Sheet1.SetActiveCell(currRow, (int)PayModeCols.Cost, false);

                    }
                    if (currCol == (int)PayModeCols.Cost)
                    {
                        decimal cost = NConvert.ToDecimal(
                            this.fpPayMode_Sheet1.Cells[currRow, (int)PayModeCols.Cost].Value);
                        if (cost < 0)
                        {
                            MessageBox.Show("����С����");
                            this.fpPayMode.Focus();
                            this.fpPayMode_Sheet1.SetActiveCell(currRow, (int)PayModeCols.Cost, false);

                            return false;
                        }
                        else
                        {
                            decimal tempOwnCost = NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[0, (int)PayModeCols.Cost].Value);

                            if (!ComputCost())
                            {
                                return false;
                            }
                            if (currRow == 0)//�ֽ�
                            {
                                this.fpPayMode_Sheet1.SetActiveCell(currRow + 1, (int)PayModeCols.Cost, false);
                            }
                            else
                            {
                                if (this.fpPayMode_Sheet1.Cells[currRow, (int)PayModeCols.PayMode].Text != "֧Ʊ")
                                {
                                    this.fpPayMode_Sheet1.SetActiveCell(currRow + 1, (int)PayModeCols.Cost, false);
                                }
                                else
                                {
                                    this.fpPayMode_Sheet1.SetActiveCell(currRow, (int)PayModeCols.Bank, false);
                                }
                            }
                        }

                        this.SetCostValue((int)PayModeCols.Cost);
                    }
                    if (currCol == (int)PayModeCols.Bank)
                    {
                        ProcessPayBank();
                        this.fpPayMode_Sheet1.SetActiveCell(currRow, (int)PayModeCols.Account, false);
                    }
                    if (currCol == (int)PayModeCols.Account)
                    {
                        this.fpPayMode_Sheet1.SetActiveCell(currRow, (int)PayModeCols.Company, false);
                    }
                    if (currCol == (int)PayModeCols.Company)
                    {
                        this.fpPayMode_Sheet1.SetActiveCell(currRow, (int)PayModeCols.PosNo, false);
                    }
                    if (currCol == (int)PayModeCols.PosNo)
                    {
                        this.fpPayMode_Sheet1.SetActiveCell(currRow + 1, (int)PayModeCols.Cost, false);
                    }
                }
            }

            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tbRealCost_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                decimal tempCost = 0;
                decimal cashCost = 0;
                for (int i = 0; i < this.fpPayMode_Sheet1.RowCount; i++)
                {
                    string tmpPayMode = this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text;
                    decimal tmpCashCost = 0;
                    if (tmpPayMode == "�ֽ�")
                    {
                        tmpCashCost = FS.FrameWork.Public.String.FormatNumber(
                            NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value), 2);
                    }
                    cashCost += tmpCashCost;
                }
                try
                {
                    tempCost = NConvert.ToDecimal(this.tbRealCost.Text) - Class.Function.DealCent(cashCost);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("��������ֲ��Ϸ�������֤����" + ex.Message);
                    this.tbRealCost.Text = string.Empty;
                    this.tbRealCost.Focus();

                    return;
                }

                if (tempCost < 0)
                {
                    DialogResult result = MessageBox.Show("�������ʵ�����С��Ӧ���ֽ�,�Ƿ���������?", "��ʾ!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (result == DialogResult.Yes)
                    {
                        this.tbRealCost.SelectAll();
                        this.tbRealCost.Focus();
                        return;
                    }

                }

                this.tbLeast.Text = tempCost.ToString();

                //this.frmDisplay.RInfo = this.rInfo;
                FS.HISFC.Models.Base.FT feeInfo = new FS.HISFC.Models.Base.FT();
                feeInfo.TotCost = totCost;
                feeInfo.OwnCost = ownCost;
                feeInfo.PayCost = payCost;
                feeInfo.PubCost = pubCost;
                feeInfo.BalancedCost = NConvert.ToDecimal(tbTotOwnCost.Text);
                feeInfo.SupplyCost = totOwnCost;
                feeInfo.RealCost = NConvert.ToDecimal(this.tbRealCost.Text);
                feeInfo.ReturnCost = tempCost;
                this.ftFeeInfo = feeInfo;
                //this.frmDisplay.FeeInfo = feeInfo;
                //this.frmDisplay.FpPayMode = this.fpPayMode;
                //frmDisplay.SetValue();

                string tmpContrlValue = "0";

                tmpContrlValue = this.feeIntegrate.GetControlValue(FS.HISFC.BizProcess.Integrate.Const.ENTER_TO_FEE, "0");

                if (tmpContrlValue == "1")
                {
                    this.tbFee.Enabled = false;
                    if (!this.SaveFee())
                    {
                        this.tbFee.Enabled = true;
                        return;
                    }
                    this.tbFee.Enabled = true;
                    this.tbRealCost.Focus();
                    this.Close();
                }
                else
                {
                    tbFee.Focus();
                }
            }

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (this.tbRealCost.Focused && keyData==Keys.Down)
            {
                this.tabControl1.Select();
                this.tabControl1.Focus();
                this.fpPayMode.Select();
                this.fpPayMode.Focus();
                this.fpPayMode_Sheet1.SetActiveCell(0, 1);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void fpSplit_CellDoubleClick(object sender, CellClickEventArgs e)
        {
            this.PreViewInvoice();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tbCount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int count = 0;
                try
                {
                    count = Convert.ToInt32(this.tbCount.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("�����Ʊ���������Ϸ�" + ex.Message);
                    this.tbCount.Focus();
                    this.tbCount.SelectAll();

                    return;
                }
                if (count > this.splitCounts)
                {
                    MessageBox.Show("��ǰ�ɷַ�Ʊ�����ܴ���: " + splitCounts.ToString());
                    this.tbCount.Focus();
                    this.tbCount.SelectAll();

                    return;
                }

                this.tbSplitDay.Focus();
                this.tbSplitDay.SelectAll();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tbSplitDay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int count = 0;
                try
                {
                    count = Convert.ToInt32(this.tbSplitDay.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("�������������Ϸ�" + ex.Message);
                    this.tbSplitDay.Focus();
                    this.tbSplitDay.SelectAll();
                    return;
                }
                if (count > 999)
                {
                    MessageBox.Show("����������ܴ���999��!");
                    this.tbSplitDay.Focus();
                    this.tbSplitDay.SelectAll();
                    return;
                }

                btnSplit.Focus();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void fpPayMode_EditModeOn(object sender, EventArgs e)
        {
            this.currentAccountInfo = this.ucPayingAgentMessControl.CurrentAccountInfo;
            this.TotOwnCost = decimal.Parse(this.tbTotOwnCost.Text);
            fpPayMode.EditingControl.KeyDown += new KeyEventHandler(EditingControl_KeyDown);
            SetLocation();
            if (fpPayMode_Sheet1.ActiveColumnIndex != (int)PayModeCols.PayMode)
            {
                lbPayMode.Visible = false;
            }
            if (fpPayMode_Sheet1.ActiveColumnIndex != (int)PayModeCols.Bank)
            {
                lbBank.Visible = false;
                //return;
            }
            if (fpPayMode_Sheet1.ActiveColumnIndex == (int)PayModeCols.Cost)
            {
                #region MyRegion

                string tempString = this.fpPayMode_Sheet1.Cells[fpPayMode_Sheet1.ActiveRowIndex, (int)PayModeCols.PayMode].Text;

                if (tempString == string.Empty)
                {
                    for (int i = 0; i < this.fpPayMode_Sheet1.Columns.Count; i++)
                    {
                        this.fpPayMode_Sheet1.Cells[fpPayMode_Sheet1.ActiveRowIndex, i].Text = string.Empty;
                    }
                }

                bool isOnlyCash = true;
                decimal nowCost = 0;
                //������֧����ʽ�����ٽ��н������� {0F169460-7FF9-4b76-A22E-C2D0A1DCD438}֧����ʽ����Զ����䡢�����жϽ�����
                bool isReturnZero = false;

                for (int i = 0; i < this.fpPayMode_Sheet1.RowCount; i++)
                {
                    if (this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text != string.Empty)
                    {
                        if (this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text != "�ֽ�" &&  //!this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Locked &&
                             NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value) > 0
                            )
                        {
                            isOnlyCash = false;
                            nowCost += NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value);
                        }
                    }
                }
                if (isOnlyCash)
                {
                    this.tbTotOwnCost.Text = FS.FrameWork.Public.String.FormatNumber(totOwnCost, 2).ToString();
                    this.fpPayMode_Sheet1.Cells[this.rowCA, (int)PayModeCols.Cost].Text = totOwnCost.ToString();

                }
                else
                {
                    if (realCost - nowCost < 0)
                    {
                        this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.ActiveRowIndex, (int)PayModeCols.Cost].Value = 0;
                        this.fpPayMode_Sheet1.SetActiveCell(this.fpPayMode_Sheet1.ActiveRowIndex, (int)PayModeCols.Cost, false);

                        nowCost = 0;
                    }
                    else
                    {

                        this.tbTotOwnCost.Text = FS.FrameWork.Public.String.FormatNumber(realCost, 2).ToString();
                        this.fpPayMode_Sheet1.Cells[this.rowCA, (int)PayModeCols.Cost].Value = this.isDealCentNoAllCancy == true ? Class.Function.DealCent(realCost - nowCost) : (realCost - nowCost);
                    }

                    if (this.isDisplayCashOnly)
                    {
                        this.tbRealCost.Text = Class.Function.DealCent(realCost - nowCost).ToString();
                    }
                }

                if (this.isDisplayCashOnly)
                {
                    this.tbRealCost.Text = Class.Function.DealCent(realCost - nowCost).ToString();
                }

                this.isPaySuccess = true;

                #endregion
            }
        }

        void EditingControl_KeyDown(object sender, KeyEventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void fpPayMode_EditChange(object sender, EditorNotifyEventArgs e)
        {
            if (e.Column == (int)PayModeCols.PayMode)
            {
                string text = fpPayMode_Sheet1.ActiveCell.Text;
                lbPayMode.Filter(text);
                if (!lbPayMode.Visible)
                {
                    lbPayMode.Visible = true;
                }
            }
            if (e.Column == (int)PayModeCols.Bank)
            {
                string text = fpPayMode_Sheet1.ActiveCell.Text;
                lbBank.Filter(text);
                if (!lbBank.Visible)
                {
                    lbBank.Visible = true;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void fpPayMode_Sheet1_CellChanged(object sender, SheetViewEventArgs e)
        {
            string tempString = this.fpPayMode_Sheet1.Cells[e.Row, (int)PayModeCols.PayMode].Text;

            if (tempString == string.Empty)
            {
                for (int i = 0; i < this.fpPayMode_Sheet1.Columns.Count; i++)
                {
                    this.fpPayMode_Sheet1.Cells[e.Row, i].Text = string.Empty;
                }
            }
            bool isOnlyCash = true;
            decimal nowCost = 0;
            //{0F169460-7FF9-4b76-A22E-C2D0A1DCD438}֧����ʽ����Զ����䡢�����жϽ�����
            //������֧����ʽ�����ٽ��н�������
            bool isReturnZero = false;

            for (int i = 0; i < this.fpPayMode_Sheet1.RowCount; i++)
            {
                if (this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text != string.Empty)
                {
                    if (this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text != "�ֽ�" &&
                        NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value) > 0
                        )
                    {
                        isOnlyCash = false;
                        nowCost += NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value);
                    }
                }
            }

            if (isOnlyCash)
            {
                this.tbTotOwnCost.Text = FS.FrameWork.Public.String.FormatNumber(totOwnCost, 2).ToString();
                this.fpPayMode_Sheet1.Cells[this.rowCA, (int)PayModeCols.Cost].Text = Class.Function.DealCent(totOwnCost).ToString();
            }
            else
            {
                decimal temp = realCost;
                if (this.isDealCentNoAllCancy)
                {
                    temp = Class.Function.DealCent(realCost);
                }

                if (temp - nowCost < 0)
                {
                    this.fpPayMode_Sheet1.Cells[e.Row, (int)PayModeCols.Cost].Value = 0;
                    this.fpPayMode_Sheet1.SetActiveCell(e.Row, (int)PayModeCols.Cost, false);
                    nowCost = 0;

                    //�Ƿ���й�������� //{0F169460-7FF9-4b76-A22E-C2D0A1DCD438}֧����ʽ����Զ����䡢�����жϽ�����
                    isReturnZero = true;
                }
                this.tbTotOwnCost.Text = FS.FrameWork.Public.String.FormatNumber(realCost, 2).ToString();
                if (isDealCentNoAllCancy)
                {
                    this.fpPayMode_Sheet1.Cells[this.rowCA, (int)PayModeCols.Cost].Value = Class.Function.DealCent(realCost - nowCost);
                }
                else
                {
                    this.fpPayMode_Sheet1.Cells[this.rowCA, (int)PayModeCols.Cost].Value = realCost - nowCost;
                }

                if (this.isDisplayCashOnly)
                {
                    //����������¼���nowCost //{0F169460-7FF9-4b76-A22E-C2D0A1DCD438}֧����ʽ����Զ����䡢�����жϽ�����
                    if (isReturnZero)
                    {
                        for (int i = 0; i < this.fpPayMode_Sheet1.RowCount; i++)
                        {
                            if (this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text != string.Empty)
                            {
                                if (this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text != "�ֽ�" &&
                                    NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value) > 0)
                                {
                                    isOnlyCash = false;
                                    nowCost += NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value);
                                }
                            }
                        }
                    }
                    this.tbRealCost.Text = Class.Function.DealCent(realCost - nowCost).ToString();
                }
            }

            if (this.isDisplayCashOnly)
            {
                this.tbRealCost.Text = Class.Function.DealCent(realCost - nowCost).ToString();
            }

            this.isPaySuccess = true;

            #region

            if (isAutoBankTrans == true)
            {
                bool needBankTran = false;
                if ((this.fpPayMode_Sheet1.ActiveRowIndex == e.Row) && (this.fpPayMode_Sheet1.ActiveColumnIndex == e.Column))
                {
                    //NeuObject no = this.helpPayMode.GetObjectFromName(tempString);
                    //if (no != null)
                    //{
                    //    needBankTran = true;
                    //}
                    if (tempString == "��ǿ�" || tempString == "���ÿ�")
                    {
                        needBankTran = true;
                    }
                }
                if (needBankTran == true)
                {
                    decimal bankTransTot = 0m;
                    bankTransTot = NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.ActiveRowIndex
                       , (int)PayModeCols.Cost].Value);
                    if (bankTransTot > 0)
                    {

                        if (this.fpPayMode_Sheet1.ActiveRow.Locked == true)
                        {
                            return;
                        }
                        bool isBankTransOK = false;
                        try
                        {
                            bankTrans.InputListInfo.Clear();
                            bankTrans.OutputListInfo.Clear();
                            /// 0:�������ͣ�1�����׽��
                            bankTrans.InputListInfo.Add("0");
                            bankTrans.InputListInfo.Add(bankTransTot);
                            isBankTransOK = bankTrans.Do();
                        }
                        catch (Exception ex)
                        {
                            //this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.ActiveRowIndex, (int)PayModeCols.Cost].Text = Class.Function.DealCent(0).ToString();
                            isBankTransOK = false;
                        }
                        if (isBankTransOK == false)
                        {
                            this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.ActiveRowIndex, (int)PayModeCols.Cost].Text = Class.Function.DealCent(0).ToString();
                            this.fpPayMode_Sheet1.SetActiveCell(0, (int)PayModeCols.Cost, false);
                            MessageBox.Show(BankTrans.OutputListInfo[0].ToString());
                        }
                        else
                        {
                            if (bankTrans.OutputListInfo.Count >= 4)
                            {
                                if (bankTransTot != NConvert.ToDecimal(bankTrans.OutputListInfo[3]))
                                {
                                    this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.ActiveRowIndex, (int)PayModeCols.Cost].Text = Class.Function.DealCent(0).ToString();
                                    this.fpPayMode_Sheet1.SetActiveCell(0, (int)PayModeCols.Cost, false);
                                    MessageBox.Show("����������" + bankTransTot.ToString() + "�����ڽ��׽��" + NConvert.ToDecimal(bankTrans.OutputListInfo[3]) + ",����ʧ�ܣ�");
                                }
                                else
                                {
                                    MessageBox.Show("���׳ɹ������" + bankTransTot.ToString());

                                    this.fpPayMode_Sheet1.CellChanged -= new SheetViewEventHandler(fpPayMode_Sheet1_CellChanged);
                                    ///  0:���� 1���˺� 2��pos�� 3�����
                                    this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.ActiveRowIndex, (int)PayModeCols.Bank].Text =
                                        bankTrans.OutputListInfo[0].ToString();
                                    this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.ActiveRowIndex, (int)PayModeCols.Account].Text =
                                        bankTrans.OutputListInfo[1].ToString();
                                    this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.ActiveRowIndex, (int)PayModeCols.PosNo].Text =
                                        bankTrans.OutputListInfo[2].ToString();
                                    this.fpPayMode_Sheet1.ActiveRow.Locked = true;
                                    this.fpPayMode_Sheet1.CellChanged += new SheetViewEventHandler(fpPayMode_Sheet1_CellChanged);
                                    this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.ActiveRowIndex, (int)PayModeCols.Cost].Text =
                                        bankTrans.OutputListInfo[3].ToString();
                                }
                            }
                        }
                    }
                    else
                    {
                        this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.ActiveRowIndex, (int)PayModeCols.Cost].Text = Class.Function.DealCent(0).ToString();
                        this.fpPayMode_Sheet1.SetActiveCell(0, (int)PayModeCols.Cost, false);
                    }
                }
            }

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.fpSplit_Sheet1.RowCount; i++)
            {
                if (this.fpSplit_Sheet1.Rows[i].Tag != null)
                {
                    if (this.fpSplit_Sheet1.Rows[i].Tag is Balance)
                    {
                        Balance invoice = this.fpSplit_Sheet1.Rows[i].Tag as Balance;
                        invoice.PrintTime = this.dateTimePicker1.Value;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        int lbBank_SelectItem(Keys key)
        {
            ProcessPayBank();
            fpPayMode.Focus();
            fpPayMode_Sheet1.SetActiveCell(fpPayMode_Sheet1.ActiveRowIndex, (int)PayModeCols.Account, true);

            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        int lbPayMode_SelectItem(Keys key)
        {
            ProcessPayMode();
            fpPayMode.Focus();
            fpPayMode_Sheet1.SetActiveCell(fpPayMode_Sheet1.ActiveRowIndex, (int)PayModeCols.Cost, true);

            return 1;
        }

        /// <summary>
        /// �ַ�Ʊ��ť����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSplit_Click(object sender, EventArgs e)
        {
            int row = this.fpSplit_Sheet1.ActiveRowIndex;
            if (this.fpSplit_Sheet1.RowCount <= 0)
            {
                return;
            }

            string tempType = this.fpSplit_Sheet1.Cells[row, 2].Tag.ToString();

            if (tempType != "1")//ֻ���Էѷ�Ʊ���Է�Ʊ
            {
                return;
            }
            string beginInvoiceNo = this.fpSplit_Sheet1.Cells[row, 0].Text;
            string beginRealInvoiceNo = "";
            FS.HISFC.Models.Fee.Outpatient.Balance invoice = null;
            ArrayList invoiceDetails = null;
            try
            {
                invoice = this.fpSplit_Sheet1.Rows[row].Tag as FS.HISFC.Models.Fee.Outpatient.Balance;
                beginRealInvoiceNo = invoice.PrintedInvoiceNO;
                invoiceDetails = this.fpSplit_Sheet1.Cells[row, 0].Tag as ArrayList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            InvoicePrint.ucSplitInvoice split = new FS.HISFC.Components.OutpatientFee.InvoicePrint.ucSplitInvoice();

            int count = 0;
            try
            {
                count = Convert.ToInt32(this.tbCount.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("�����Ʊ���������Ϸ�" + ex.Message);
                this.tbCount.Focus();
                this.tbCount.SelectAll();
                return;
            }
            if (count > this.splitCounts)
            {
                MessageBox.Show("��ǰ�ɷַ�Ʊ�����ܴ���: " + splitCounts.ToString());
                this.tbCount.Focus();
                this.tbCount.SelectAll();
                return;
            }
            if (count <= 0)
            {
                MessageBox.Show("��ǰ�ɷַ�Ʊ������С�ڻ����0");
                this.tbCount.Focus();
                this.tbCount.SelectAll();
                return;
            }
            int days = 0;
            try
            {
                days = Convert.ToInt32(this.tbSplitDay.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("�������������Ϸ�" + ex.Message);
                this.tbSplitDay.Focus();
                this.tbSplitDay.SelectAll();
                return;
            }
            if (days > 999)
            {
                MessageBox.Show("����������ܴ���999��!");
                this.tbSplitDay.Focus();
                this.tbSplitDay.SelectAll();
                return;
            }
            string invoiceNoType = this.controlParam.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.GET_INVOICE_NO_TYPE, true, "0");

            if (invoiceNoType == "2" && this.fpSplit_Sheet1.RowCount > 1)
            {
                MessageBox.Show("�Ѿ����ڷ�Ʊ��¼,���Ҫ������Ʊ,����Ĭ�ϰ�ť,���·���!");
                this.tbSplitDay.Focus();
                this.tbSplitDay.SelectAll();

                return;
            }

            this.btnSplit.Focus();
            split.Count = count;
            split.Days = days;
            split.InvoiceType = tempType;
            split.InvoiceNoType = invoiceNoType;
            split.BeginInvoiceNo = beginInvoiceNo;
            split.BeginRealInvoiceNo = beginRealInvoiceNo;
            split.Invoice = invoice;
            split.InvoiceDetails = invoiceDetails;
            split.AddInvoiceUnits(count, this.rbAuto.Checked ? "1" : "0");
            split.IsAuto = this.rbAuto.Checked;
            Form frmTemp = new Form();
            split.Dock = DockStyle.Fill;
            frmTemp.Controls.Add(split);
            frmTemp.Text = "�ַ�Ʊ";
            frmTemp.WindowState = FormWindowState.Maximized;
            frmTemp.ShowDialog(this);

            if (!split.IsConfirm)
            {
                return;
            }

            this.dateTimePicker1.Enabled = false;//�ֹ���Ʊ֮��������ͨ���շѽ����޸ķ�Ʊ����

            ArrayList splitInvoices = split.SplitInvoices;
            ArrayList splitInvoiceDetails = split.SplitInvoiceDetails;


            this.fpSplit_Sheet1.Rows.Add(row + 1, splitInvoices.Count);
            for (int i = 0; i < splitInvoices.Count; i++)
            {
                FS.HISFC.Models.Fee.Outpatient.Balance invoiceTemp = splitInvoices[i] as FS.HISFC.Models.Fee.Outpatient.Balance;
                this.fpSplit_Sheet1.Cells[row + 1 + i, 0].Text = invoiceTemp.Invoice.ID;
                this.fpSplit_Sheet1.Cells[row + 1 + i, 1].Text = invoiceTemp.FT.TotCost.ToString();
                string tmp = null;
                switch (invoiceTemp.Memo)
                {
                    case "5":
                        tmp = "�ܷ�Ʊ";
                        break;
                    case "1":
                        tmp = "�Է�";
                        break;
                    case "2":
                        tmp = "����";
                        break;
                    case "3":
                        tmp = "����";
                        break;
                }
                this.fpSplit_Sheet1.Cells[row + 1 + i, 2].Text = tmp;
                this.fpSplit_Sheet1.Cells[row + 1 + i, 2].Tag = invoiceTemp.Memo;
                this.fpSplit_Sheet1.Cells[row + 1 + i, 3].Text = invoiceTemp.FT.OwnCost.ToString();
                this.fpSplit_Sheet1.Cells[row + 1 + i, 4].Text = invoiceTemp.FT.PayCost.ToString();
                this.fpSplit_Sheet1.Cells[row + 1 + i, 5].Text = invoiceTemp.FT.PubCost.ToString();
                this.fpSplit_Sheet1.Rows[row + 1 + i].Tag = invoiceTemp;
                this.fpSplit_Sheet1.Cells[row + 1 + i, 0].Tag = ((ArrayList)splitInvoiceDetails[i]) as ArrayList;
            }
            this.fpSplit_Sheet1.Rows.Remove(row, 1);
            for (int i = row + splitInvoices.Count; i < this.fpSplit_Sheet1.RowCount; i++)
            {
                FS.HISFC.Models.Fee.Outpatient.Balance tempInvoice =
                    this.fpSplit_Sheet1.Rows[i].Tag as FS.HISFC.Models.Fee.Outpatient.Balance;

                string nextInvoiceNo = ""; string nextRealInvoiceNo = ""; string errText = "";

                if (invoiceNoType == "2")//��ͨģʽ��ҪTrans֧��
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    this.feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                    int iReturn = this.feeIntegrate.GetNextInvoiceNO(invoiceNoType, tempInvoice.Invoice.ID, tempInvoice.PrintedInvoiceNO, ref nextInvoiceNo, ref nextRealInvoiceNo, splitInvoices.Count - 1, ref errText);
                    if (iReturn < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(errText);

                        return;
                    }

                    FS.FrameWork.Management.PublicTrans.RollBack();//��Ϊ��ʱ��һ���������ݿ�,���Իع�,���ַ�Ʊ������
                }
                else
                {

                    int iReturn = this.feeIntegrate.GetNextInvoiceNO(invoiceNoType, tempInvoice.Invoice.ID, tempInvoice.PrintedInvoiceNO, ref nextInvoiceNo, ref nextRealInvoiceNo, splitInvoices.Count - 1, ref errText);
                    if (iReturn < 0)
                    {
                        MessageBox.Show(errText);
                        return;
                    }
                }
                tempInvoice.Invoice.ID = nextInvoiceNo;
                tempInvoice.PrintedInvoiceNO = nextRealInvoiceNo;

                this.fpSplit_Sheet1.Cells[i, 0].Text = tempInvoice.Invoice.ID;
                this.fpSplit_Sheet1.Rows[i].Tag = tempInvoice;
                ArrayList alTemp = this.fpSplit_Sheet1.Cells[i, 0].Tag as ArrayList;
                foreach (FS.HISFC.Models.Fee.Outpatient.BalanceList detail in alTemp)
                {
                    detail.BalanceBase.Invoice.ID = tempInvoice.Invoice.ID;
                }
                this.fpSplit_Sheet1.Cells[i, 0].Tag = alTemp;
            }
        }

        private void tbLeast_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }
            //this.frmDisplay.RInfo = this.rInfo;
            FS.HISFC.Models.Base.FT feeInfo = new FS.HISFC.Models.Base.FT();
            feeInfo.TotCost = totCost;
            feeInfo.OwnCost = ownCost;
            feeInfo.PayCost = payCost;
            feeInfo.PubCost = pubCost;
            feeInfo.BalancedCost = NConvert.ToDecimal(tbTotOwnCost.Text);
            feeInfo.SupplyCost = totOwnCost;
            feeInfo.RealCost = NConvert.ToDecimal(this.tbRealCost.Text);
            //feeInfo.ReturnCost = NConvert.ToDecimal(tbLeast.Text);
            feeInfo.ReturnCost = NConvert.ToDecimal(tbTotOwnCost.Text) - NConvert.ToDecimal(tbLeast.Text);
            this.ftFeeInfo = feeInfo;
            //this.frmDisplay.FeeInfo = feeInfo;
            //this.frmDisplay.FpPayMode = this.fpPayMode;
            //frmDisplay.SetValue();

            //string tmpContrlValue = "0";

            //tmpContrlValue = this.feeIntegrate.GetControlValue(FS.HISFC.BizProcess.Integrate.Const.ENTER_TO_FEE, "0");

            //if (tmpContrlValue == "1")
            //{
            this.tbFee.Enabled = false;
            if (!this.SaveFee())
            {
                this.tbFee.Enabled = true;
                return;
            }
            this.tbFee.Enabled = true;
            this.tbRealCost.Focus();
            this.Close();
            //}
            //else
            //{
            //    tbFee.Focus();
            //}
        }

        private void tbRealCost_TextChanged(object sender, EventArgs e)
        {
            #region ������ʾ

            decimal tempCost = 0;
            decimal cashCost = 0;
            for (int i = 0; i < this.fpPayMode_Sheet1.RowCount; i++)
            {
                string tmpPayMode = this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text;
                decimal tmpCashCost = 0;
                if (tmpPayMode == "�ֽ�")
                {
                    tmpCashCost = FS.FrameWork.Public.String.FormatNumber(NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value), 2);

                }
                cashCost += tmpCashCost;
            }
            try
            {
                tempCost = NConvert.ToDecimal(this.tbRealCost.Text) - cashCost;
            }
            catch (Exception ex)
            {
                MessageBox.Show("��������ֲ��Ϸ�������֤����" + ex.Message);
                this.tbRealCost.Text = string.Empty;
                this.tbRealCost.Focus();

                return;
            }

            this.tbLeast.Text = tempCost.ToString();

            #endregion

            #region {221FCC64-7D41-471a-9EED-C30BA1CE330A} ��ֹ�޷�����С����
            string dealCent = FS.HISFC.Components.OutpatientFee.Class.Function.DealCent(NConvert.ToDecimal(tbRealCost.Text)).ToString();
            if (NConvert.ToDecimal(dealCent) != NConvert.ToDecimal(tbRealCost.Text))
            {
                this.tbRealCost.Text = dealCent;
            }
            //this.tbRealCost.Text = FS.HISFC.Components.OutpatientFee.Class.Function.DealCent(Convert.ToDecimal(tbRealCost.Text)).ToString();
            if (this.RealCostChange != null)
            {
                this.RealCostChange(this.tbRealCost.Text, this.tbLeast.Text);
            }
            #endregion
        }
        #endregion

        #region IOutpatientPopupFee ��Ա

        private bool isAutoBankTrans = false;
        public bool IsAutoBankTrans
        {
            set { isAutoBankTrans = value; }
        }

        #endregion
        private void SetCostValue(int column)
        {
            if (column == (int)PayModeCols.Cost)
            {
                try
                {
                    if (FS.FrameWork.Function.NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.ActiveRowIndex, (int)PayModeCols.Cost].Value) > 0)
                    {
                        return;
                    }

                    decimal CAcost = 0;
                    for (int i = 0; i < this.fpPayMode_Sheet1.RowCount; i++)
                    {
                        try
                        {
                            if (this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text != string.Empty)
                            {
                                if (this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text == "�ֽ�")
                                {
                                    CAcost += FS.FrameWork.Function.NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value);
                                    this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value = 0;
                                }
                            }
                        }
                        catch
                        {
                        }

                    }

                    if (CAcost > 0)
                    {
                        this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.ActiveRowIndex, (int)PayModeCols.Cost].Value = CAcost;
                    }
                }
                catch
                {
                }
            }
        }

        private void fpPayMode_EnterCell(object sender, EnterCellEventArgs e)
        {
            this.SetCostValue(e.Column);
        }

        /// <summary>
        /// ��ȡ֧����ʽ���к�
        /// </summary>
        /// <param name="sv"></param>
        /// <param name="name">֧����ʽ</param>
        /// <returns></returns>
        private int GetRowIndexByName(SheetView sv, string name)
        {
            for (int i = 0; i <= sv.Rows.Count - 1; ++i)
            {
                if (sv.Cells[i,0].Text==name)
                {
                    return i;
                }
            }
            return 0;
        }

        #region IOutpatientPopupFee ��Ա


        public event FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateRealCost RealCostChange;

        #endregion
    }
}