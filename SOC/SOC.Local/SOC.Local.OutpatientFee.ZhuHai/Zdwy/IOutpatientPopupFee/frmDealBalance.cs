using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Fee.Outpatient;
using FS.FrameWork.Models;
using FS.FrameWork.Management;
using FS.FrameWork.Function;
using FS.HISFC.BizProcess.Interface.FeeInterface;
using FarPoint.Win.Spread;
using System.Reflection;

namespace FS.SOC.Local.OutpatientFee.ZhuHai.Zdwy.IOutpatientPopupFee
{
    /// <summary>
    /// �����շѽ���
    /// </summary>
    public partial class frmDealBalance : Form, FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientPopupFee
    {
        public frmDealBalance()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ����ҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// ���Ʋ���ҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParam = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// ���ݿ�����
        /// </summary>
        private FS.FrameWork.Management.Transaction trans;

        /// <summary>
        /// ���ݿ�����
        /// </summary>
        public FS.FrameWork.Management.Transaction Trans
        {
            set
            {
                this.trans = value;
            }
        }

        /// <summary>
        /// ֧����ʽ���
        /// </summary>
        private ArrayList alPayModes = new ArrayList();

        /// <summary>
        /// ֧����ʽ��Ϣ(���ڵ����շ�)
        /// </summary>
        private ArrayList alPatientPayModeInfo = new ArrayList();

        /// <summary>
        /// ��С�������
        /// </summary>
        private ArrayList alMinFees = new ArrayList();

        /// <summary>
        /// ������ϸ����
        /// </summary>
        private ArrayList alFeeDetails = new ArrayList();

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
        /// ��Ʊ����
        /// </summary>
        private ArrayList alInvoices;

        /// <summary>
        /// ��Ʊ����
        /// </summary>
        public ArrayList Invoices
        {
            set
            {
                this.alInvoices = value;
                if (this.alInvoices != null)
                {
                    this.fpSplit_Sheet1.RowCount = this.alInvoices.Count;
                    for (int i = 0; i < this.alInvoices.Count; i++)
                    {
                        Balance balance = this.alInvoices[i] as Balance;
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
                        //������ϸ
                        this.fpSplit_Sheet1.Cells[i, 3].Tag = ((ArrayList)InvoiceFeeDetails[i]) as ArrayList;
                    }
                }
            }
            get
            {
                return this.alInvoices;
            }
        }

        /// <summary>
        /// ��Ʊ��ϸ���
        /// </summary>
        private ArrayList alInvoiceDetails;

        /// <summary>
        /// ��Ʊ��ϸ���
        /// </summary>
        public ArrayList InvoiceDetails
        {
            set
            {
                this.alInvoiceDetails = value;
            }
            get
            {
                return this.alInvoiceDetails;
            }
        }

        /// <summary>
        /// ��Ʊ������ϸ����
        /// </summary>
        private ArrayList alInvoiceFeeDetails;

        public ArrayList InvoiceFeeDetails
        {
            set
            {
                this.alInvoiceFeeDetails = value;
            }
            get
            {
                return this.alInvoiceFeeDetails;
            }
        }

        /// <summary>
        /// �շ���Ϣ
        /// </summary>
        private FS.HISFC.Models.Base.FT ftFeeInfo;

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
        /// ��С�����б�
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper helpMinFee = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ֧����ʽ�б�
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper helpPayMode = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ���˷�ʽ�б�
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper helpPubType = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ��ͬ��λ��Ӧ֧����ʽ������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper helpPactToPayModes = new FS.FrameWork.Public.ObjectHelper();

        #region ���

        /// <summary>
        /// �ܽ��
        /// </summary>
        private decimal totCost;

        /// <summary>
        /// �ܽ��
        /// </summary>
        public decimal TotCost
        {
            set
            {
                this.totCost = value;
                this.tbTotCost.Text = totCost.ToString();
            }
            get
            {
                return totCost;
            }
        }

        /// <summary>
        /// �Էѽ��
        /// </summary>
        private decimal ownCost;

        /// <summary>
        /// �Էѽ��
        /// </summary>
        public decimal OwnCost
        {
            set
            {
                this.ownCost = value;
                this.tbOwnCost.Text = value.ToString();
            }
            get
            {
                return this.ownCost;
            }
        }

        /// <summary>
        /// �Ը����
        /// </summary>
        private decimal payCost;

        /// <summary>
        /// �Ը����
        /// </summary>
        public decimal PayCost
        {
            set
            {
                this.payCost = value;
                this.tbPayCost.Text = value.ToString();
            }
            get
            {
                return this.payCost;
            }
        }

        /// <summary>
        /// ���ʽ��
        /// </summary>
        private decimal pubCost;

        /// <summary>
        /// ���˽��
        /// </summary>
        public decimal PubCost
        {
            set
            {
                this.pubCost = value;
                this.tbPubCost.Text = pubCost.ToString();
                //ҽ�����⴦��(pub_cost)��Ҫ����֧����ʽ
                if (this.PatientInfo.Pact.PayKind.ID == "02")
                {
                    //if (this.alZhuHaiPactID.Contains(this.PatientInfo.Pact.ID) == true)
                    if (this.hsZhuHaiPactID.ContainsKey(this.PatientInfo.Pact.ID) == true)
                    {
                        this.fpPayMode_Sheet1.Cells["PBZH_Cost"].Text = value.ToString("F2");
                    }
                    //else if (this.alZhongShanPactID.Contains(this.PatientInfo.Pact.ID) == true)
                    else if (this.hsZhongShanPactID.ContainsKey(this.PatientInfo.Pact.ID) == true)
                    {
                        this.fpPayMode_Sheet1.Cells["PBZS_Cost"].Text = value.ToString("F2");
                    }
                    else
                    {
                        //Ϊ�˱�֤�ð汾��pub_costҲ���뵽֧����ʽ���У�����ҽ���ı���������Ҫ��ֵ֧����ʽ.
                        //û��ά����Ĭ��Ϊ��PBZH_Cost���麣����.
                        this.fpPayMode_Sheet1.Cells["PBZH_Cost"].Text = value.ToString("F2");
                    }
                }
            }
            get
            {
                return this.pubCost;
            }
        }

        /// <summary>
        /// ������(�Żݽ��)
        /// </summary>
        private decimal rebateCost;

        /// <summary>
        /// ������(�Żݽ��)
        /// </summary>
        public decimal RebateRate
        {
            set
            {
                this.rebateCost = value;
                this.tbRebateCost.Text = rebateCost.ToString();
                //�����Ѿ�����Init()����,����ֱ�ӽ��Żݸ�ֵ��֧����ʽ����
                if (this.fpPayMode_Sheet1.Cells["RC_Cost"] != null)
                {
                    this.fpPayMode_Sheet1.Cells["RC_Cost"].Value = value;
                }
            }
            get
            {
                return this.rebateCost;
            }
        }

        /// <summary>
        /// Ӧ�ɽ��(=�Էѽ��+�Ը����)
        /// </summary>
        private decimal totOwnCost;

        /// <summary>
        /// Ӧ�ɽ��(=�Էѽ��+�Ը����)
        /// </summary>
        public decimal TotOwnCost
        {
            set
            {
                this.totOwnCost = value;
                this.tbTotOwnCost.Text = value.ToString();
                this.tbRealCost.Text = value.ToString();
                this.tbRealCost.SelectAll();

                //���ݺ�ͬ��λĬ����Ӧ��֧����ʽ��������PACTTOPAYMODE��
                bool isCanFindPayMode = false;
                if (this.helpPactToPayModes != null && this.helpPactToPayModes.ArrayObject.Count > 0)
                {
                    FS.FrameWork.Models.NeuObject obj = this.helpPactToPayModes.GetObjectFromID(this.PatientInfo.Pact.ID);
                    if (obj != null && !string.IsNullOrEmpty(obj.ID) && !string.IsNullOrEmpty(obj.Name))
                    {
                        for (int i = 0; i < this.fpPayMode_Sheet1.Rows.Count; i++)
                        {
                            string payID = this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Tag.ToString();
                            if (payID == obj.Name)
                            {
                                this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value = value;
                                isCanFindPayMode = true;
                                break;
                            }
                        }
                    }
                }

                if (!isCanFindPayMode)
                {
                    this.fpPayMode_Sheet1.Cells[0, (int)PayModeCols.Cost].Value = value;
                }

                //��ɽҽ�����⴦��(PAY_COST���������Ϊ�̶�����ᱣ�Ͽ�(��ɽ)��)
                if (this.hsZhongShanPactID.ContainsKey(this.PatientInfo.Pact.ID) == true)
                {
                    decimal MCZS_Cost = this.PatientInfo.SIMainInfo.PayCost;
                    this.fpPayMode_Sheet1.Cells["MCZS_Cost"].Text = MCZS_Cost.ToString("F2");
                }

            }
            get
            {
                return this.totOwnCost;
            }
        }

        /// <summary>
        /// ʵ�����
        /// </summary>
        private decimal realCost;

        /// <summary>
        /// ʵ�����
        /// </summary>
        public decimal RealCost
        {
            set
            {
                //��ɽҽ�����⴦��
                //if (this.alZhongShanPactID.Contains(this.PatientInfo.Pact.ID) == true)
                //if (this.hsZhongShanPactID.ContainsKey(this.PatientInfo.Pact.ID) == true)
                //{
                //    decimal MCZS_Cost = this.PatientInfo.SIMainInfo.PayCost;
                //    FS.FrameWork.Models.NeuObject objGZZFUJE = this.PatientInfo.SIMainInfo.ExtendProperty["GZZFUJE"];
                //    FS.FrameWork.Models.NeuObject objGZZFEJE = this.PatientInfo.SIMainInfo.ExtendProperty["GZZFEJE"];
                //    if (objGZZFUJE != null && FS.FrameWork.Function.NConvert.ToDecimal(objGZZFUJE.Memo) > 0)
                //    {
                //        MCZS_Cost += FS.FrameWork.Function.NConvert.ToDecimal(objGZZFUJE.Memo);
                //    }
                //    if (objGZZFEJE != null && FS.FrameWork.Function.NConvert.ToDecimal(objGZZFEJE.Memo) > 9)
                //    {
                //        MCZS_Cost += FS.FrameWork.Function.NConvert.ToDecimal(objGZZFEJE.Memo);
                //    }
                //    this.fpPayMode_Sheet1.Cells["MCZS_Cost"].Text = MCZS_Cost.ToString("F2");
                //}

                this.realCost = value;
            }
        }

        #endregion

        /// <summary>
        /// ���ߺ�ͬ��λ��Ϣ
        /// </summary>
        private FS.HISFC.Models.Base.PactInfo pactInfo = new FS.HISFC.Models.Base.PactInfo();

        /// <summary>
        /// ���ߺ�ͬ��λ��Ϣ
        /// </summary>
        public FS.HISFC.Models.Base.PactInfo PactInfo
        {
            set
            {
                this.pactInfo = value;
                if ("01".Equals(value.PayKind.ID))//�Էѻ��߿��Է�Ʊ
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
        /// ���߹Һ���Ϣ
        /// </summary>
        private FS.HISFC.Models.Registration.Register rInfo = new FS.HISFC.Models.Registration.Register();

        /// <summary>
        /// ���߹Һ���Ϣ
        /// </summary>
        public FS.HISFC.Models.Registration.Register PatientInfo
        {
            set
            {
                this.rInfo = value;
            }
            get
            {
                return this.rInfo;
            }
        }

        #region ��ʱ����

        /// <summary>
        /// �麣ҽ�������ͬ��λ
        /// </summary>
        //private ArrayList alZhuHaiPactID = ArrayList.Adapter(new string[]{ "4", "14", "19", "20", "21" });
        private Hashtable hsZhuHaiPactID = new Hashtable();

        /// <summary>
        /// ��ɽҽ�������ͬ��λ
        /// </summary>
        //private ArrayList alZhongShanPactID = ArrayList.Adapter(new string[] { "18", "37", "38" });
        private Hashtable hsZhongShanPactID = new Hashtable();

        /// <summary>
        /// �����ֹ������Żݽ��ĺ�ͬ��λ
        /// </summary>
        private Hashtable hsCanInputRCMoneyPact = new Hashtable();

        #endregion

        #endregion

        #region �ؼ�

        /// <summary>
        /// ���˷�ʽѡ���б�
        /// </summary>
        private FS.FrameWork.WinForms.Controls.PopUpListBox lbPubType = new FS.FrameWork.WinForms.Controls.PopUpListBox();

        #endregion

        #region ����

        /// <summary>
        /// �Ƿ���Էַ�Ʊ
        /// </summary>
        private bool isCanSplit;

        /// <summary>
        /// ���ַ�Ʊ����
        /// </summary>
        private int splitCounts;

        /// <summary>
        /// �Ƿ�����޸ķ�Ʊ����
        /// </summary>
        private bool isCanModifyInvoiceDate;

        /// <summary>
        /// �շ�ʱ�Ƿ�����޸ķ�Ʊ��ӡ����
        /// </summary>
        private bool isCanModifyInvoicePrintDate;

        /// <summary>
        /// �Ƿ�ϵͳ������������
        /// </summary>
        private bool isAutoBankTrans = false;

        /// <summary>
        /// �Ƿ�ϵͳ������������
        /// </summary>
        public bool IsAutoBankTrans
        {
            set
            {
                isAutoBankTrans = value;
            }
        }

        /// <summary>
        /// �Ƿ��˷�
        /// </summary>
        private bool isQuitFee = false;

        /// <summary>
        /// �Ƿ��˷�
        /// </summary>
        public bool IsQuitFee
        {
            set
            {
                this.isQuitFee = value;
                if (this.isQuitFee == true)
                {
                    this.tbCharge.Enabled = false;
                }
            }
        }

        #endregion

        #region ״̬

        /// <summary>
        /// �Ƿ���ȡ����ť
        /// </summary>
        private bool isPushCancelButton = false;

        /// <summary>
        /// �Ƿ���ȡ����ť
        /// </summary>
        public bool IsPushCancelButton
        {
            set
            {
                this.isPushCancelButton = value;
            }
            get
            {
                return this.isPushCancelButton;
            }
        }

        /// <summary>
        /// �Ƿ����ɹ���δ�˳���
        /// </summary>
        private bool isSuccessFee = false;

        /// <summary>
        /// �Ƿ����ɹ���δ�˳���
        /// </summary>
        public bool IsSuccessFee
        {
            set
            {
                this.isSuccessFee = value;
            }
            get
            {
                return this.isSuccessFee;
            }
        }

        /// <summary>
        /// ֧����ʽ�Ƿ�����ɹ�
        /// </summary>
        private bool isPaySuccess = false;

        #endregion

        #region δʹ��

        /// <summary>
        /// �Է�ҩ���[δʹ��]
        /// </summary>
        public decimal SelfDrugCost
        {
            set { }
        }

        /// <summary>
        /// ����ҩ���[δʹ��]
        /// </summary>
        public decimal OverDrugCost
        {
            set { }
        }

        /// <summary>
        /// ������[δʹ��]
        /// </summary>
        public decimal LeastCost
        {
            set { }
        }

        /// <summary>
        /// �����ӿ�[δʹ��]
        /// </summary>
        public FS.HISFC.BizProcess.Interface.FeeInterface.IBankTrans BankTrans
        {
            set { }
            get { return null; }
        }

        /// <summary>
        /// ��Ʊ�ͷ�Ʊ��ϸ����[δʹ��]
        /// </summary>
        public ArrayList InvoiceAndDetails
        {
            set { }
            get { return null; }
        }

        /// <summary>
        /// �Ƿ��ֽ����
        /// </summary>
        public bool IsCashPay
        {
            set { }
            get { return false; }
        }

        #endregion

        #region �¼�

        /// <summary>
        /// ʵ�ս��ı䴥��
        /// </summary>
        public event FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateRealCost RealCostChange;

        /// <summary>
        /// �շѰ�ť����
        /// </summary>
        public event FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateFee FeeButtonClicked;

        /// <summary>
        /// ���۰�ť����
        /// </summary>
        public event FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateChangeSomething ChargeButtonClicked;

        private void frmDealBalance_Load(object sender, EventArgs e)
        {
            this.tbRealCost.Select();
            this.tbRealCost.Focus();
            this.tbRealCost.SelectAll();
            this.tbLeast.Text = "0";
        }

        private int lbPubType_SelectItem(Keys key)
        {
            this.ProcessPubType();
            this.fpPubType.Focus();
            this.fpPubType_Sheet1.SetActiveCell(fpPubType_Sheet1.ActiveRowIndex, (int)PubTypes.Cost);
            return 1;
        }

        private void fpPubType_EditModeOn(object sender, EventArgs e)
        {
            if (this.fpPubType_Sheet1.ActiveColumnIndex == (int)PubTypes.PubType)
            {
                #region �����б�λ
                Control cell = this.fpPubType.EditingControl;
                this.lbPubType.Location = new Point(this.fpPubType.Location.X + cell.Location.X + 4,
                    this.panel1.Location.Y + this.tabControl1.Location.Y + this.fpPubType.Location.Y + cell.Location.Y + cell.Height * 2 + SystemInformation.Border3DSize.Height * 2);
                this.lbPubType.Size = new Size(cell.Width + 50 + SystemInformation.Border3DSize.Width * 2, 150);
                if (this.lbPubType.Location.Y + this.lbPubType.Height > this.fpPubType.Location.Y + this.fpPubType.Height)
                {
                    this.lbPubType.Location = new Point(this.fpPubType.Location.X + cell.Location.X + 4,
                        this.panel1.Location.Y + this.tabControl1.Location.Y + this.fpPubType.Location.Y + cell.Location.Y + cell.Height * 2 + SystemInformation.Border3DSize.Height * 2
                        - this.lbPubType.Size.Height - cell.Height);
                }
                #endregion
            }
            else
            {
                this.lbPubType.Visible = false;
            }
        }

        private void fpPubType_EditChange(object sender, EditorNotifyEventArgs e)
        {
            if (e.Column == (int)PubTypes.PubType)
            {
                string text = fpPubType_Sheet1.ActiveCell.Text.Trim();
                this.lbPubType.Filter(text);
                if (this.lbPubType.Visible == false)
                {
                    this.lbPubType.Visible = true;
                }
                this.fpPubType.Focus();
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.F12)
            {
                this.panel1.Focus();
                this.groupBox2.Focus();
                this.tbRealCost.Focus();
                this.tbRealCost.SelectAll();
            }
            else if (keyData == Keys.F4)
            {
                this.tabControl1.SelectedTab = this.tpPubType;
                this.tpPubType.Focus();
                this.fpPubType_Sheet1.ActiveRowIndex = 1;
                this.fpPubType_Sheet1.SetActiveCell(1, (int)PubTypes.PubType, false);
                this.fpPubType.EditMode = true;
            }
            else if (keyData == Keys.F5)
            {
                this.tabControl1.SelectedTab = this.tpSplitInvoice;
                this.tpSplitInvoice.Focus();
                this.tbCount.Focus();
            }
            else if (keyData == Keys.F6)
            {
                this.panel1.Focus();
                this.tabControl1.Focus();
                this.tabControl1.SelectedTab = this.tpPayMode;
                this.tpPayMode.Focus();
                this.fpPayMode.Focus();
                this.fpPayMode_Sheet1.ActiveRowIndex = 0;
                this.fpPayMode_Sheet1.SetActiveCell(0, (int)PayModeCols.Cost, false);
            }
            else if (keyData == Keys.Escape)
            {
                if (this.lbPubType.Visible)
                {
                    this.lbPubType.Visible = false;
                    this.fpPubType.StopCellEditing();
                }
                else
                {
                    this.tbRealCost.Focus();
                    this.isPushCancelButton = true;
                    this.Close();
                }
            }
            else if (this.fpPubType.ContainsFocus)
            {
                if (keyData == Keys.Up)
                {
                    if (this.fpPubType.Visible == true)
                    {
                        this.lbPubType.PriorRow();
                    }
                }
                else if (keyData == Keys.Down)
                {
                    if (this.fpPubType.Visible == true)
                    {
                        this.lbPubType.NextRow();
                    }
                }
                else if (keyData == Keys.Enter)
                {
                    int curRow = this.fpPubType_Sheet1.ActiveRowIndex;
                    int curCol = this.fpPubType_Sheet1.ActiveColumnIndex;
                    this.fpPubType.StopCellEditing();
                    if (curCol == (int)PubTypes.PubType)
                    {
                        this.ProcessPubType();
                        this.fpPubType_Sheet1.SetActiveCell(curRow, (int)PubTypes.Cost, false);
                    }
                    else if (curCol == (int)PubTypes.Cost)
                    {
                        decimal cost = NConvert.ToDecimal(this.fpPubType_Sheet1.Cells[curRow, (int)PubTypes.Cost].Value);
                        if (cost < 0)
                        {
                            MessageBox.Show("����С����");
                            this.fpPubType.Focus();
                            this.fpPubType_Sheet1.SetActiveCell(curCol, (int)PubTypes.Cost, false);
                            return false;
                        }
                        this.fpPubType_Sheet1.SetActiveCell(curRow, (int)PubTypes.Mark, false);
                    }
                    else if (curCol == (int)PubTypes.Mark)
                    {
                        decimal cost = NConvert.ToDecimal(this.fpPubType_Sheet1.Cells[curRow, (int)PubTypes.Cost].Value);

                        //����¼�����֧����ʽ ?ͨ��֧����ʽ��������ȷ��������޸����֣����з��� gumzh?
                        int rowIndex = this.GetRowIndexByName(fpPayMode_Sheet1, "����");
                        this.fpPayMode_Sheet1.Cells[rowIndex, (int)PayModeCols.Cost].Text = cost.ToString();
                        this.fpPayMode_Sheet1.Cells[rowIndex, (int)PayModeCols.Cost].Locked = true;

                        this.tabControl1.SelectedTab = this.tpPayMode;
                        this.fpPayMode_Sheet1.SetActiveCell(0, (int)PayModeCols.Cost);
                    }
                }
            }
            else if (this.fpPayMode.ContainsFocus)
            {
                if (keyData == Keys.Enter)
                {
                    int curRow = this.fpPayMode_Sheet1.ActiveRowIndex;
                    int curCol = this.fpPayMode_Sheet1.ActiveColumnIndex;
                    if (curCol == (int)PayModeCols.Cost)
                    {
                        decimal cost = NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[curRow, (int)PayModeCols.Cost].Value);
                        if (cost < 0)
                        {
                            MessageBox.Show("����С����");
                            this.fpPayMode.Focus();
                            this.fpPayMode_Sheet1.SetActiveCell(curRow, (int)PayModeCols.Cost, false);
                            return false;
                        }
                        else
                        {
                            if (curRow == 0)
                            {
                                this.fpPayMode_Sheet1.SetActiveCell(curRow + 2, (int)PayModeCols.Cost, false);
                            }
                            else
                            {
                                this.fpPayMode_Sheet1.SetActiveCell(curRow + 1, (int)PayModeCols.Cost, false);
                            }
                        }

                    }
                }
            }
            else if (this.tbRealCost.ContainsFocus)
            {
                #region ��ת������
                
                if (keyData == Keys.Enter)
                {
                    this.tbLeast.Focus(); //����Ϊ����
                    this.tbLeast.SelectAll();
                }

                #endregion
            }
            else if (this.tbLeast.ContainsFocus)
            {
                #region ȷ���շ�

                if (keyData == Keys.Enter)
                {
                    if (NConvert.ToDecimal(this.tbLeast.Text) < 0)
                    {
                        MessageBox.Show("������С��0����ע��!");
                        this.tbRealCost.Focus();
                        this.tbRealCost.SelectAll();
                    }
                    else
                    {
                        this.tbFee.Focus(); //����Ϊ����
                    }
                }

                #endregion
            }

            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// ����շѰ�ť����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbFee_Click(object sender, EventArgs e)
        {
            this.Tag = "�շ�";
            this.tbFee.Enabled = false;
            this.fpPayMode.StopCellEditing();

            if (this.CheckPayMode() == false)
            {
                this.tbFee.Enabled = true;
                return;
            }

            if (this.isPaySuccess == false)
            {
                this.tbFee.Enabled = true;
                return;
            }

            this.isPaySuccess = false;

            if (this.SaveFee() == false)
            {
                this.tbFee.Enabled = true;
                return;
            }

            this.tbFee.Enabled = true;
            this.tbRealCost.Focus();

            this.isSuccessFee = true;

            this.Close();
        }

        /// <summary>
        /// ���۱��水ť����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbCharge_Click(object sender, EventArgs e)
        {
            this.Tag = "���۱���";
            this.tbCharge.Enabled = false;
            this.SaveCharge();
            this.tbCharge.Enabled = true;
            this.tbRealCost.Focus();
            this.Close();
        }

        /// <summary>
        /// ȡ����ť����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbCancel_Click(object sender, EventArgs e)
        {
            this.Tag = "ȡ��";
            this.isPushCancelButton = true;
            this.tbRealCost.Focus();
            this.Close();
        }

        /// <summary>
        /// �ַ�ƱĬ�ϰ�ť����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbDefault_Click(object sender, EventArgs e)
        {
            this.Invoices = this.alInvoices;
        }

        private void fpPayMode_Sheet1_CellChanged(object sender, SheetViewEventArgs e)
        {
            this.CheckPayMode();
        }

        private void tbTotOwnCost_TextChanged(object sender, EventArgs e)
        {
            this.tbRealCost.Text = this.tbTotOwnCost.Text;
        }

        /// <summary>
        /// ������-ʵ����� - �ֽ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbRealCost_TextChanged(object sender, EventArgs e)
        {
            decimal casCost = NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[0, (int)PayModeCols.Cost].Value);  //�ֽ�
            //this.tbLeast.Text = FS.FrameWork.Public.String.FormatNumberReturnString(NConvert.ToDecimal(this.tbRealCost.Text) - NConvert.ToDecimal(this.tbTotOwnCost.Text), 2);
            this.tbLeast.Text = FS.FrameWork.Public.String.FormatNumberReturnString(NConvert.ToDecimal(this.tbRealCost.Text) - casCost, 2);
        }

        private void btnSplit_Click(object sender, EventArgs e)
        {
            //##δ��ȫ����
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
            ucSplitInvoice split = new ucSplitInvoice();

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

        private void tbCount_KeyDown(object sender, KeyEventArgs e)
        {
            //##δ��ȫ����
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

        private void tbSplitDay_KeyDown(object sender, KeyEventArgs e)
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

        private void fpSplit_CellDoubleClick(object sender, CellClickEventArgs e)
        {
            this.PreViewInvoice();
        }

        #endregion

        #region ��ö��

        /// <summary>
        /// ֧����ʽ��ö��
        /// </summary>
        enum PayModeCols
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

        /// <summary>
        /// ���˷�ʽ��ö��
        /// </summary>
        enum PubTypes
        {
            /// <summary>
            /// ���˷�ʽ
            /// </summary>
            PubType = 0,
            /// <summary>
            /// ���
            /// </summary>
            Cost = 1,
            /// <summary>
            /// ��ע
            /// </summary>
            Mark = 2
        }

        #endregion

        #region ����

        private void PreViewInvoice()
        {
            //##δ��ȫ����
            int row = this.fpSplit_Sheet1.ActiveRowIndex;
            if (this.fpSplit_Sheet1.RowCount <= 0)
            {
                return;
            }

            Balance invoicePreView = this.fpSplit_Sheet1.Rows[row].Tag as Balance;
            ArrayList invoiceDetailsPreview = this.fpSplit_Sheet1.Cells[row, 0].Tag as ArrayList;
            ArrayList InvoiceFeeDetailsPreview = this.fpSplit_Sheet1.Cells[row, 3].Tag as ArrayList;

            FS.HISFC.BizProcess.Interface.FeeInterface.IInvoicePrint iInvoicePrint = null;

            string returnValue = controlParam.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.INVOICEPRINT, false, string.Empty);
            if (string.IsNullOrEmpty(returnValue))
            {
                iInvoicePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IInvoicePrint)) as FS.HISFC.BizProcess.Interface.FeeInterface.IInvoicePrint;
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
                            iInvoicePrint = System.Activator.CreateInstance(type) as FS.HISFC.BizProcess.Interface.FeeInterface.IInvoicePrint;

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

        private bool CheckPayMode()
        {
            decimal sumCost = 0m;
            decimal shouldPay = 0m;//Ӧ�ɽ��
            for (int i = 0; i < this.fpPayMode_Sheet1.RowCount; i++)
            {
                if (this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text != string.Empty)
                {
                    if (this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text != "�ֽ�" &&
                         this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text != "�麣ҽ��ͳ��" &&
                         this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text != "��ɽҽ��ͳ��"
                        )
                    {
                        //ͳ�Ƴ� ���ֽ� �� ͳ����(pub_cost)�� ֮��Ľ��
                        sumCost += NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value);
                    }
                }
            }
            if (realCost > sumCost)
            {
                //��ʣ��֧���������ֽ�
                this.fpPayMode_Sheet1.Cells[0, (int)PayModeCols.Cost].Value = realCost - sumCost;
            }
            else if (realCost < sumCost)
            {
                //��ʾ����
                MessageBox.Show("���׼֧����ʽ���,��Ӧ����" + realCost.ToString());
                return false;
            }
            else
            {
                this.fpPayMode_Sheet1.Cells[0, (int)PayModeCols.Cost].Value = 0;
            }

            for (int i = 0; i < this.fpPayMode_Sheet1.RowCount; i++)
            {
                if (this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Locked == false && this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value != null)
                {
                    shouldPay += FS.FrameWork.Function.NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value.ToString());
                }
            }
            this.tbTotOwnCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(shouldPay, 2);
            this.isPaySuccess = true;
            return true;
        }

        private FS.HISFC.Models.Base.FT GetFT()
        {
            FS.HISFC.Models.Base.FT feeInfo = new FS.HISFC.Models.Base.FT();
            feeInfo.TotCost = totCost;
            feeInfo.OwnCost = ownCost;
            feeInfo.PayCost = payCost;
            feeInfo.PubCost = pubCost;
            feeInfo.BalancedCost = NConvert.ToDecimal(this.tbTotOwnCost.Text);
            feeInfo.SupplyCost = NConvert.ToDecimal(this.tbTotOwnCost.Text);
            feeInfo.RealCost = NConvert.ToDecimal(this.tbRealCost.Text);
            feeInfo.ReturnCost = NConvert.ToDecimal(this.tbLeast.Text);
            this.ftFeeInfo = feeInfo;
            return feeInfo;
        }

        /// <summary>
        /// ������˷�ʽ�б�س�
        /// </summary>
        /// <returns></returns>
        private int ProcessPubType()
        {
            int currRow = this.fpPubType_Sheet1.ActiveRowIndex;
            if (currRow < 0)
            {
                return 0;
            }
            NeuObject item = null;

            int returnValue = this.lbPubType.GetSelectedItem(out item);
            if (returnValue == -1 || item == null)
            {
                return -1;
            }

            fpPubType_Sheet1.SetValue(currRow, (int)PubTypes.PubType, item.Name);
            fpPubType.StopCellEditing();

            this.lbPubType.Visible = false;

            return 0;
        }

        /// <summary>
        /// ������С����
        /// </summary>
        private void SpliteMinFee()
        {
            Hashtable htMinFee = new Hashtable();
            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList in alFeeDetails)
            {
                string minFeeName = string.Empty;
                if (htMinFee.ContainsKey(feeItemList.Item.MinFee.ID) == false)
                {
                    FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = feeItemList.Item.MinFee.ID;
                    obj.Memo = FS.FrameWork.Public.String.FormatNumber(feeItemList.FT.TotCost, 2).ToString();
                    if (htMinFee.ContainsKey(obj.ID) == false)
                    {
                        obj.Name = this.managerIntegrate.GetConstansObj("MINFEE", obj.ID).Name;
                    }
                    else
                    {
                        obj.Name = this.helpMinFee.GetObjectFromID(obj.ID).Name;
                    }
                    minFeeName = obj.Name;
                    htMinFee.Add(obj.ID, obj);
                }
                else
                {
                    FS.FrameWork.Models.NeuObject obj = htMinFee[feeItemList.Item.MinFee.ID] as FS.FrameWork.Models.NeuObject;
                    minFeeName = obj.Name;
                    obj.Memo = FS.FrameWork.Public.String.FormatNumber(FS.FrameWork.Function.NConvert.ToDecimal(obj.Memo) + feeItemList.FT.TotCost, 2).ToString();
                    htMinFee.Remove(obj.ID);
                    htMinFee.Add(obj.ID, obj);
                }
            }
            foreach (DictionaryEntry entry in htMinFee)
            {
                this.alMinFees.Add(entry.Value);
            }

            //���ý�����ʾ
            if (this.fpSpread1_Sheet1.Rows.Count > 0)
            {
                this.fpSpread1_Sheet1.Rows.Remove(0, this.fpSpread1_Sheet1.Rows.Count);
            }

            if (this.alMinFees.Count > 0)
            {
                this.fpSpread1_Sheet1.Rows.Add(0, alMinFees.Count / 4 + 1);
            }
            for (int i = 0; i < alMinFees.Count; i++)
            {
                this.fpSpread1_Sheet1.Cells[i / 4, 2 * (i % 4)].Text = (alMinFees[i] as FS.FrameWork.Models.NeuObject).Name;
                this.fpSpread1_Sheet1.Cells[i / 4, 2 * (i % 4) + 1].Text = (alMinFees[i] as FS.FrameWork.Models.NeuObject).Memo;
            }
        }

        /// <summary>
        /// ��ʼ��֧����ʽ��Ϣ
        /// </summary>
        private int InitPayMode()
        {
            ArrayList tempPayModes = this.managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PAYMODES);
            this.helpPayMode.ArrayObject = tempPayModes;
            if (tempPayModes == null || tempPayModes.Count == 0)
            {
                MessageBox.Show("��ȡ֧����ʽ�б����");
                return -1;
            }
            //���ٱ����ֽ�ͼ���֧����ʽ
            FS.FrameWork.Models.NeuObject objCA = new FS.FrameWork.Models.NeuObject();
            objCA.ID = "CA";
            objCA.Name = "�ֽ�";

            FS.FrameWork.Models.NeuObject objPB = new FS.FrameWork.Models.NeuObject();
            objPB.ID = "PB";
            objPB.Name = "����";

            if (helpPayMode.GetObjectFromID(objCA.ID) == null)
            {
                helpPayMode.ArrayObject.Add(objCA);
            }

            if (helpPayMode.GetObjectFromID(objPB.ID) == null)
            {
                helpPayMode.ArrayObject.Add(objPB);
            }

            //֧����ʽ�б��Ϊ�̶�ģʽ
            this.fpPayMode_Sheet1.RowCount = tempPayModes.Count;
            for (int i = 0; i < tempPayModes.Count; i++)
            {
                FS.FrameWork.Models.NeuObject obj = tempPayModes[i] as FS.FrameWork.Models.NeuObject;
                this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Tag = obj.ID;
                this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text = obj.Name;
                this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Locked = true;
                if ("PB".Equals(obj.ID))
                {
                    this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Locked = true;
                    this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Tag = "PB_Cost";
                }
                if ("RC".Equals(obj.ID))
                {
                    if (!this.hsCanInputRCMoneyPact.Contains(this.PatientInfo.Pact.ID))
                    {
                        this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Locked = true;
                        this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Tag = "RC_Cost";
                    }
                }
                if ("PBZH".Equals(obj.ID))
                {
                    this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Locked = true;
                    this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Tag = "PBZH_Cost";
                }
                if ("PBZS".Equals(obj.ID))
                {
                    this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Locked = true;
                    this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Tag = "PBZS_Cost";
                }
                if ("MCZS".Equals(obj.ID))
                {
                    this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Locked = true;
                    this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Tag = "MCZS_Cost";
                }
            }

            return 1;
        }

        /// <summary>
        /// ��ʼ���ַ�Ʊ��Ϣ
        /// </summary>
        /// <returns></returns>
        private int InitSplitInvoice()
        {
            string tmpCtrlValue = feeIntegrate.GetControlValue(FS.HISFC.BizProcess.Integrate.Const.CANSPLIT, "0");
            if (string.IsNullOrEmpty(tmpCtrlValue) || "1".Equals(tmpCtrlValue) == false)
            {
                MessageBox.Show("�Ƿ�ַ�Ʊ����û��ά�������ڲ���Ĭ��ֵ: ���ɷַ�Ʊ!");
                tmpCtrlValue = "0";
            }

            this.isCanSplit = FS.FrameWork.Function.NConvert.ToBoolean(tmpCtrlValue);

            this.rbAuto.Enabled = isCanSplit;
            this.rbMun.Enabled = isCanSplit;
            this.tbCount.Enabled = isCanSplit;
            this.btnSplit.Enabled = isCanSplit;
            this.tbDefault.Enabled = isCanSplit;

            this.splitCounts = this.controlParam.GetControlParam<int>(FS.HISFC.BizProcess.Integrate.Const.SPLITCOUNTS, false, 9);
            this.isCanModifyInvoiceDate = this.controlParam.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.CAN_MODIFY_INVOICE_DATE, false, false);

            if (isCanModifyInvoiceDate == false)
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

        private int InitPubType()
        {
            ArrayList tempPubTypes = this.managerIntegrate.GetConstantList("PUBTYPE");
            this.helpPubType.ArrayObject = tempPubTypes;
            if (tempPubTypes == null || tempPubTypes.Count == 0)
            {
                MessageBox.Show("��ȡ֧����ʽ�б����");
                return -1;
            }
            this.lbPubType.AddItems(tempPubTypes);
            this.Controls.Add(this.lbPubType);
            this.lbPubType.Hide();
            this.lbPubType.BorderStyle = BorderStyle.FixedSingle;
            this.lbPubType.BringToFront();
            this.lbPubType.SelectItem += new FS.FrameWork.WinForms.Controls.PopUpListBox.MyDelegate(lbPubType_SelectItem);

            return 1;
        }

        /// <summary>
        /// ��ʼ��farpoint,����һЩ�ȼ�
        /// </summary>
        private void InitFp()
        {
            InputMap im;

            #region ���˷�ʽFP

            im = this.fpPubType.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
            im.Put(new Keystroke(Keys.Down, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
            im.Put(new Keystroke(Keys.Up, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
            im.Put(new Keystroke(Keys.Escape, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
            im.Put(new Keystroke(Keys.Back, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
            im.Put(new Keystroke(Keys.F4, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
            im.Put(new Keystroke(Keys.F5, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
            im.Put(new Keystroke(Keys.F6, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
            #endregion

            #region ֧����ʽFP

            im = this.fpPayMode.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
            //im.Put(new Keystroke(Keys.Down, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
            //im.Put(new Keystroke(Keys.Up, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
            //im.Put(new Keystroke(Keys.Escape, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
            //im.Put(new Keystroke(Keys.Back, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
            im.Put(new Keystroke(Keys.F4, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
            im.Put(new Keystroke(Keys.F5, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
            im.Put(new Keystroke(Keys.F6, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
            #endregion
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns></returns>
        public int Init()
        {

            #region �����ֹ������Żݽ��ĺ�ͬ��λ

            try
            {
                //��ʱ����
                this.hsCanInputRCMoneyPact = new Hashtable();
                hsCanInputRCMoneyPact.Add("5", null);
                hsCanInputRCMoneyPact.Add("9", null);
            }
            catch (Exception ex)
            { }

            #endregion

            //��ʼ��FarPoint��Ϣ
            this.InitFp();

            //��ʼ����С�����б�
            this.helpMinFee.ArrayObject = this.managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.MINFEE);

            //��ʼ��֧����ʽ�б�
            if (this.InitPayMode() < 0)
            {
                return -1;
            }

            //��ʼ�����˷�ʽ�б�
            if (this.InitPubType() < 0)
            {
                return -1;
            }

            //��ʼ���ַ�Ʊ
            if (this.InitSplitInvoice() < 0)
            {
                return -1;
            }

            //�Ƿ�����޸ķ�Ʊ��ӡ����
            this.isCanModifyInvoiceDate = this.controlParam.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.MODIFY_INVOICE_PRINTDATE, false, false);
            if (this.isCanModifyInvoicePrintDate == true)
            {
                this.dateTimePicker1.Enabled = true;
            }
            else
            {
                this.dateTimePicker1.Enabled = false;
            }

            //�Ż����Ž��ڴ���??
            #region ��ͬ��λ��Ӧ֧����ʽ

            try
            {
                ArrayList al = this.managerIntegrate.GetConstantList("PACTTOPAYMODE");
                if (al != null && al.Count > 0)
                {
                    this.helpPactToPayModes.ArrayObject = al;
                }
            }
            catch (Exception ex)
            {}

            #endregion

            #region ���ҡ��麣ҽ�����͡���ɽҽ�����ĺ�ͬ��λ�����ݴ����㷨DLL����Ϊ��ѯ������

            try
            {
                //?����޸Ľӿ����ֵ����������Ҫ�޸� gumzh?
                FS.HISFC.BizLogic.Fee.PactUnitInfo pactMgr = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
                ArrayList alZH = pactMgr.QueryPactUnitByDLLName("ZhuHaiSI.dll");
                ArrayList alZS = pactMgr.QueryPactUnitByDLLName("ZhongShanSI.dll");

                //�麣�籣
                if (alZH != null && alZH.Count > 0)
                {
                    this.hsZhuHaiPactID = new Hashtable();
                    foreach (FS.FrameWork.Models.NeuObject obj in alZH)
                    {
                        if (!this.hsZhuHaiPactID.ContainsKey(obj.ID))
                        {
                            this.hsZhuHaiPactID.Add(obj.ID, obj);
                        }
                    }
                }
                //��ɽҽ��
                if (alZS != null && alZS.Count > 0)
                {
                    this.hsZhongShanPactID = new Hashtable();
                    foreach (FS.FrameWork.Models.NeuObject obj in alZS)
                    {
                        if (!this.hsZhongShanPactID.ContainsKey(obj.ID))
                        {
                            this.hsZhongShanPactID.Add(obj.ID, obj);
                        }
                    }
                }

            }
            catch (Exception ex)
            {}

            #endregion

            return 1;
        }

        /// <summary>
        /// ���滮����Ϣ
        /// </summary>
        /// <returns></returns>
        public bool SaveCharge()
        {
            this.ChargeButtonClicked();
            return true;
        }

        /// <summary>
        /// �����շ���Ϣ##
        /// </summary>
        /// <returns></returns>
        public bool SaveFee()
        {
            string errText = string.Empty;
            int errRow = 0, errCol = 0;
            this.GetFT();
            //�ж�֧����ʽ
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
            //�жϷַ�Ʊ��ʽ
            if (this.IsSplitInvoicesValid() == false)
            {
                this.fpSplit.Focus();
                return false;
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
                ArrayList tempArrayListTempInvoiceFeeItemDetals = this.fpSplit_Sheet1.Cells[i, 3].Tag as ArrayList;
                alTempInvoiceFeeItemDetalsSec.Add(tempArrayListTempInvoiceFeeItemDetals);
            }

            alTempInvoiceDetals.Add(alTempInvoiceDetailsSec);
            alTempInvoiceFeeItemDetals.Add(alTempInvoiceFeeItemDetalsSec);

            this.FeeButtonClicked(alPatientPayModeInfo, alTempInvoices, alTempInvoiceDetals, alTempInvoiceFeeItemDetals);

            return true;
        }

        /// <summary>
        /// ���ÿؼ�Ĭ�Ͻ���
        /// </summary>
        public void SetControlFocus()
        {
            this.panel1.Focus();
            this.groupBox2.Focus();
            this.tbRealCost.Focus();
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
                if (name.Equals(sv.Cells[i, 0].Text))
                {
                    return i;
                }
            }
            return 0;
        }

        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="col"></param>
        private void SetCostValue(int col)
        {
            if (col == (int)PayModeCols.Cost)
            {
                if (NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.ActiveRowIndex, (int)PayModeCols.Cost].Value) > 0)
                {
                    return;
                }

                decimal CACost = NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[0, (int)PayModeCols.Cost].Value);

                if (CACost > 0)
                {
                    this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.ActiveRowIndex, (int)PayModeCols.Cost].Value = CACost;
                }
            }
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
            decimal tempTotalCost = 0m;
            decimal tempCost = 0m;

            for (int i = 0; i < this.fpPayMode_Sheet1.RowCount; i++)
            {
                tempPayMode = this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text;

                try
                {
                    tempCost = NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value);
                    tempTotalCost += tempCost;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("������벻�Ϸ�" + ex.Message);
                    errRow = i;
                    errCol = (int)PayModeCols.Cost;
                    return false;
                }
                if (string.IsNullOrEmpty(tempPayMode) == true || tempCost == 0)
                {
                    continue;
                }

                string tempID = helpPayMode.GetID(tempPayMode);
                if (string.IsNullOrEmpty(tempID) == true)
                {
                    errText = "֧����ʽ�������!";
                    errRow = i;
                    errCol = (int)PayModeCols.PayMode;
                    return false;
                }
            }

            if (tempTotalCost != this.totCost)
            {
                errText = "���׼֧����ʽ���";
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

            decimal balancePayTotCost = 0;

            for (int i = 0; i < this.fpPayMode_Sheet1.RowCount; i++)
            {
                if (string.IsNullOrEmpty(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text) == true)
                {
                    continue;
                }
                if (string.IsNullOrEmpty(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Text) == true)
                {
                    continue;
                }
                if (NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value) == 0)
                {
                    continue;
                }
                balancePay = new BalancePay();
                balancePay.PayType.Name = this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text;
                balancePay.PayType.ID = helpPayMode.GetID(balancePay.PayType.Name);
                if (string.IsNullOrEmpty(balancePay.PayType.ID) == true)
                {
                    return null;
                }
                if ("PB".Equals(balancePay.PayType.ID))
                {
                    //����,��Ҫͬʱ��ȡ���˷�ʽ����е���Ϣ
                    balancePay.Bank.Name = this.fpPubType_Sheet1.Cells[0, (int)PubTypes.PubType].Text;
                    balancePay.Bank.ID = helpPubType.GetID(balancePay.Bank.Name);
                    balancePay.Memo = this.fpPubType_Sheet1.Cells[0, (int)PubTypes.Mark].Text;
                    if (string.IsNullOrEmpty(balancePay.Bank.ID) || string.IsNullOrEmpty(balancePay.Bank.Name))
                    {
                        MessageBox.Show("���˷�ʽ����!��ѡ����˷�ʽ!");
                        this.tabControl1.SelectedIndex = 2;
                        this.fpPubType.Focus();
                        return null;
                    }
                }
                balancePay.FT.TotCost = NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value);
                balancePay.FT.RealCost = balancePay.FT.TotCost;
                balancePays.Add(balancePay);

                balancePayTotCost += balancePay.FT.TotCost;
            }

            if (balancePayTotCost != this.TotCost)
            {
                MessageBox.Show("֧����ʽ���ܽ������ܽ���˶�!");
                return null;
            }

            return balancePays;
        }

        /// <summary>
        /// ��֤�ַ�Ʊ�����Ƿ�Ϸ�
        /// </summary>
        /// <returns>�ɹ� true ʧ�� false</returns>
        private bool IsSplitInvoicesValid()
        {
            decimal tempTotalCost = 0m;

            for (int i = 0; i < this.fpSplit_Sheet1.RowCount; i++)
            {
                if ("�ܷ�Ʊ".Equals(this.fpSplit_Sheet1.Cells[i, 2].Text))
                {
                    continue;
                }
                try
                {
                    tempTotalCost += NConvert.ToDecimal(this.fpSplit_Sheet1.Cells[i, 3].Text) +
                        NConvert.ToDecimal(this.fpSplit_Sheet1.Cells[i, 4].Text) +
                        NConvert.ToDecimal(this.fpSplit_Sheet1.Cells[i, 5].Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("������벻�Ϸ�!\n" + ex.Message);
                    return false;
                }
            }

            if (FS.FrameWork.Public.String.FormatNumber(tempTotalCost, 2) != this.totCost)
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
        private ArrayList QuerySplitInvoices()
        {
            NeuObject obj = null;
            ArrayList alObj = new ArrayList();

            if ("01".Equals(this.pactInfo.ID))
            {
                for (int i = 0; i < this.fpSplit_Sheet1.RowCount; i++)
                {
                    obj = new NeuObject();
                    obj.ID = i.ToString();
                    obj.User01 = this.fpSplit_Sheet1.Cells[i, 1].Text;
                    alObj.Add(obj);
                }
            }
            else
            {
                obj = new NeuObject();
                obj.User01 = ownCost.ToString();
                obj.User02 = payCost.ToString();
                obj.User03 = pubCost.ToString();
            }

            return alObj;
        }

        #endregion

    }
}