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
    /// 门诊收费结算
    /// </summary>
    public partial class frmDealBalance : Form, FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientPopupFee
    {
        public frmDealBalance()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 管理业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 费用业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 控制参数业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParam = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// 数据库事务
        /// </summary>
        private FS.FrameWork.Management.Transaction trans;

        /// <summary>
        /// 数据库事务
        /// </summary>
        public FS.FrameWork.Management.Transaction Trans
        {
            set
            {
                this.trans = value;
            }
        }

        /// <summary>
        /// 支付方式组合
        /// </summary>
        private ArrayList alPayModes = new ArrayList();

        /// <summary>
        /// 支付方式信息(用于调用收费)
        /// </summary>
        private ArrayList alPatientPayModeInfo = new ArrayList();

        /// <summary>
        /// 最小费用组合
        /// </summary>
        private ArrayList alMinFees = new ArrayList();

        /// <summary>
        /// 费用明细集合
        /// </summary>
        private ArrayList alFeeDetails = new ArrayList();

        /// <summary>
        /// 费用明细集合
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
        /// 发票集合
        /// </summary>
        private ArrayList alInvoices;

        /// <summary>
        /// 发票集合
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
                                tmp = "总发票";
                                break;
                            case "1":
                                tmp = "自费";
                                break;
                            case "2":
                                tmp = "记帐";
                                break;
                            case "3":
                                tmp = "特殊";
                                break;
                            case "4":
                                tmp = "医保";
                                break;
                        }
                        this.fpSplit_Sheet1.Cells[i, 2].Text = tmp;
                        this.fpSplit_Sheet1.Cells[i, 2].Tag = balance.Memo;
                        this.fpSplit_Sheet1.Cells[i, 3].Text = balance.FT.OwnCost.ToString();
                        this.fpSplit_Sheet1.Cells[i, 4].Text = balance.FT.PayCost.ToString();
                        this.fpSplit_Sheet1.Cells[i, 5].Text = balance.FT.PubCost.ToString();
                        //发票主表
                        this.fpSplit_Sheet1.Rows[i].Tag = balance;
                        //发票明细
                        this.fpSplit_Sheet1.Cells[i, 0].Tag = ((ArrayList)alInvoiceDetails[i])[0] as ArrayList;
                        //费用明细
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
        /// 发票明细结合
        /// </summary>
        private ArrayList alInvoiceDetails;

        /// <summary>
        /// 发票明细结合
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
        /// 发票费用明细集合
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
        /// 收费信息
        /// </summary>
        private FS.HISFC.Models.Base.FT ftFeeInfo;

        /// <summary>
        /// 收费信息
        /// </summary>
        public FS.HISFC.Models.Base.FT FTFeeInfo
        {
            get
            {
                return this.ftFeeInfo;
            }
        }

        /// <summary>
        /// 最小费用列表
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper helpMinFee = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 支付方式列表
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper helpPayMode = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 记账方式列表
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper helpPubType = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 合同单位对应支付方式帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper helpPactToPayModes = new FS.FrameWork.Public.ObjectHelper();

        #region 金额

        /// <summary>
        /// 总金额
        /// </summary>
        private decimal totCost;

        /// <summary>
        /// 总金额
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
        /// 自费金额
        /// </summary>
        private decimal ownCost;

        /// <summary>
        /// 自费金额
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
        /// 自付金额
        /// </summary>
        private decimal payCost;

        /// <summary>
        /// 自付金额
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
        /// 记帐金额
        /// </summary>
        private decimal pubCost;

        /// <summary>
        /// 记账金额
        /// </summary>
        public decimal PubCost
        {
            set
            {
                this.pubCost = value;
                this.tbPubCost.Text = pubCost.ToString();
                //医保特殊处理(pub_cost)需要插入支付方式
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
                        //为了保证该版本的pub_cost也插入到支付方式表中：凡是医保的报销金额，都需要赋值支付方式.
                        //没有维护的默认为【PBZH_Cost】珠海记账.
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
        /// 减免金额(优惠金额)
        /// </summary>
        private decimal rebateCost;

        /// <summary>
        /// 减免金额(优惠金额)
        /// </summary>
        public decimal RebateRate
        {
            set
            {
                this.rebateCost = value;
                this.tbRebateCost.Text = rebateCost.ToString();
                //由于已经调用Init()方法,可以直接将优惠赋值到支付方式界面
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
        /// 应缴金额(=自费金额+自付金额)
        /// </summary>
        private decimal totOwnCost;

        /// <summary>
        /// 应缴金额(=自费金额+自付金额)
        /// </summary>
        public decimal TotOwnCost
        {
            set
            {
                this.totOwnCost = value;
                this.tbTotOwnCost.Text = value.ToString();
                this.tbRealCost.Text = value.ToString();
                this.tbRealCost.SelectAll();

                //根据合同单位默认相应的支付方式【常数：PACTTOPAYMODE】
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

                //中山医保特殊处理(PAY_COST分离出来作为固定【社会保障卡(中山)】)
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
        /// 实付金额
        /// </summary>
        private decimal realCost;

        /// <summary>
        /// 实付金额
        /// </summary>
        public decimal RealCost
        {
            set
            {
                //中山医保特殊处理
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
        /// 患者合同单位信息
        /// </summary>
        private FS.HISFC.Models.Base.PactInfo pactInfo = new FS.HISFC.Models.Base.PactInfo();

        /// <summary>
        /// 患者合同单位信息
        /// </summary>
        public FS.HISFC.Models.Base.PactInfo PactInfo
        {
            set
            {
                this.pactInfo = value;
                if ("01".Equals(value.PayKind.ID))//自费患者可以分票
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
        /// 患者挂号信息
        /// </summary>
        private FS.HISFC.Models.Registration.Register rInfo = new FS.HISFC.Models.Registration.Register();

        /// <summary>
        /// 患者挂号信息
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

        #region 临时处理

        /// <summary>
        /// 珠海医保门诊合同单位
        /// </summary>
        //private ArrayList alZhuHaiPactID = ArrayList.Adapter(new string[]{ "4", "14", "19", "20", "21" });
        private Hashtable hsZhuHaiPactID = new Hashtable();

        /// <summary>
        /// 中山医保门诊合同单位
        /// </summary>
        //private ArrayList alZhongShanPactID = ArrayList.Adapter(new string[] { "18", "37", "38" });
        private Hashtable hsZhongShanPactID = new Hashtable();

        /// <summary>
        /// 允许手工输入优惠金额的合同单位
        /// </summary>
        private Hashtable hsCanInputRCMoneyPact = new Hashtable();

        #endregion

        #endregion

        #region 控件

        /// <summary>
        /// 记账方式选择列表
        /// </summary>
        private FS.FrameWork.WinForms.Controls.PopUpListBox lbPubType = new FS.FrameWork.WinForms.Controls.PopUpListBox();

        #endregion

        #region 属性

        /// <summary>
        /// 是否可以分发票
        /// </summary>
        private bool isCanSplit;

        /// <summary>
        /// 最多分发票张数
        /// </summary>
        private int splitCounts;

        /// <summary>
        /// 是否可以修改发票日期
        /// </summary>
        private bool isCanModifyInvoiceDate;

        /// <summary>
        /// 收费时是否可以修改发票打印日期
        /// </summary>
        private bool isCanModifyInvoicePrintDate;

        /// <summary>
        /// 是否系统处理银联交易
        /// </summary>
        private bool isAutoBankTrans = false;

        /// <summary>
        /// 是否系统处理银联交易
        /// </summary>
        public bool IsAutoBankTrans
        {
            set
            {
                isAutoBankTrans = value;
            }
        }

        /// <summary>
        /// 是否退费
        /// </summary>
        private bool isQuitFee = false;

        /// <summary>
        /// 是否退费
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

        #region 状态

        /// <summary>
        /// 是否点击取消按钮
        /// </summary>
        private bool isPushCancelButton = false;

        /// <summary>
        /// 是否点击取消按钮
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
        /// 是否结算成功（未退出）
        /// </summary>
        private bool isSuccessFee = false;

        /// <summary>
        /// 是否结算成功（未退出）
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
        /// 支付方式是否输入成功
        /// </summary>
        private bool isPaySuccess = false;

        #endregion

        #region 未使用

        /// <summary>
        /// 自费药金额[未使用]
        /// </summary>
        public decimal SelfDrugCost
        {
            set { }
        }

        /// <summary>
        /// 超标药金额[未使用]
        /// </summary>
        public decimal OverDrugCost
        {
            set { }
        }

        /// <summary>
        /// 找零金额[未使用]
        /// </summary>
        public decimal LeastCost
        {
            set { }
        }

        /// <summary>
        /// 银联接口[未使用]
        /// </summary>
        public FS.HISFC.BizProcess.Interface.FeeInterface.IBankTrans BankTrans
        {
            set { }
            get { return null; }
        }

        /// <summary>
        /// 发票和发票明细集合[未使用]
        /// </summary>
        public ArrayList InvoiceAndDetails
        {
            set { }
            get { return null; }
        }

        /// <summary>
        /// 是否现金冲账
        /// </summary>
        public bool IsCashPay
        {
            set { }
            get { return false; }
        }

        #endregion

        #region 事件

        /// <summary>
        /// 实收金额改变触发
        /// </summary>
        public event FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateRealCost RealCostChange;

        /// <summary>
        /// 收费按钮触发
        /// </summary>
        public event FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateFee FeeButtonClicked;

        /// <summary>
        /// 划价按钮触发
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
                #region 下拉列表定位
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
                            MessageBox.Show("金额不能小于零");
                            this.fpPubType.Focus();
                            this.fpPubType_Sheet1.SetActiveCell(curCol, (int)PubTypes.Cost, false);
                            return false;
                        }
                        this.fpPubType_Sheet1.SetActiveCell(curRow, (int)PubTypes.Mark, false);
                    }
                    else if (curCol == (int)PubTypes.Mark)
                    {
                        decimal cost = NConvert.ToDecimal(this.fpPubType_Sheet1.Cells[curRow, (int)PubTypes.Cost].Value);

                        //金额记录入记账支付方式 ?通过支付方式的名字来确定，如果修改名字，则有风险 gumzh?
                        int rowIndex = this.GetRowIndexByName(fpPayMode_Sheet1, "记账");
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
                            MessageBox.Show("金额不能小于零");
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
                #region 跳转到找零
                
                if (keyData == Keys.Enter)
                {
                    this.tbLeast.Focus(); //设置为焦点
                    this.tbLeast.SelectAll();
                }

                #endregion
            }
            else if (this.tbLeast.ContainsFocus)
            {
                #region 确认收费

                if (keyData == Keys.Enter)
                {
                    if (NConvert.ToDecimal(this.tbLeast.Text) < 0)
                    {
                        MessageBox.Show("找零金额小于0，请注意!");
                        this.tbRealCost.Focus();
                        this.tbRealCost.SelectAll();
                    }
                    else
                    {
                        this.tbFee.Focus(); //设置为焦点
                    }
                }

                #endregion
            }

            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// 点击收费按钮触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbFee_Click(object sender, EventArgs e)
        {
            this.Tag = "收费";
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
        /// 划价保存按钮触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbCharge_Click(object sender, EventArgs e)
        {
            this.Tag = "划价保存";
            this.tbCharge.Enabled = false;
            this.SaveCharge();
            this.tbCharge.Enabled = true;
            this.tbRealCost.Focus();
            this.Close();
        }

        /// <summary>
        /// 取消按钮触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbCancel_Click(object sender, EventArgs e)
        {
            this.Tag = "取消";
            this.isPushCancelButton = true;
            this.tbRealCost.Focus();
            this.Close();
        }

        /// <summary>
        /// 分发票默认按钮触发
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
        /// 找零金额-实付金额 - 现金
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbRealCost_TextChanged(object sender, EventArgs e)
        {
            decimal casCost = NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[0, (int)PayModeCols.Cost].Value);  //现金
            //this.tbLeast.Text = FS.FrameWork.Public.String.FormatNumberReturnString(NConvert.ToDecimal(this.tbRealCost.Text) - NConvert.ToDecimal(this.tbTotOwnCost.Text), 2);
            this.tbLeast.Text = FS.FrameWork.Public.String.FormatNumberReturnString(NConvert.ToDecimal(this.tbRealCost.Text) - casCost, 2);
        }

        private void btnSplit_Click(object sender, EventArgs e)
        {
            //##未完全测试
            int row = this.fpSplit_Sheet1.ActiveRowIndex;
            if (this.fpSplit_Sheet1.RowCount <= 0)
            {
                return;
            }

            string tempType = this.fpSplit_Sheet1.Cells[row, 2].Tag.ToString();

            if (tempType != "1")//只有自费发票可以分票
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
                MessageBox.Show("输入分票得数量不合法" + ex.Message);
                this.tbCount.Focus();
                this.tbCount.SelectAll();
                return;
            }
            if (count > this.splitCounts)
            {
                MessageBox.Show("当前可分发票数不能大于: " + splitCounts.ToString());
                this.tbCount.Focus();
                this.tbCount.SelectAll();
                return;
            }
            if (count <= 0)
            {
                MessageBox.Show("当前可分发票数不能小于或等于0");
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
                MessageBox.Show("输入间隔天数不合法" + ex.Message);
                this.tbSplitDay.Focus();
                this.tbSplitDay.SelectAll();
                return;
            }
            if (days > 999)
            {
                MessageBox.Show("间隔天数不能大于999天!");
                this.tbSplitDay.Focus();
                this.tbSplitDay.SelectAll();
                return;
            }
            string invoiceNoType = this.controlParam.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.GET_INVOICE_NO_TYPE, true, "0");

            if (invoiceNoType == "2" && this.fpSplit_Sheet1.RowCount > 1)
            {
                MessageBox.Show("已经存在分票记录,如果要继续分票,请点击默认按钮,重新分配!");
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
            frmTemp.Text = "分发票";
            frmTemp.WindowState = FormWindowState.Maximized;
            frmTemp.ShowDialog(this);

            if (!split.IsConfirm)
            {
                return;
            }

            this.dateTimePicker1.Enabled = false;//分过发票之后不允许再通过收费界面修改发票日期

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
                        tmp = "总发票";
                        break;
                    case "1":
                        tmp = "自费";
                        break;
                    case "2":
                        tmp = "记帐";
                        break;
                    case "3":
                        tmp = "特殊";
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

                if (invoiceNoType == "2")//普通模式需要Trans支持
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

                    FS.FrameWork.Management.PublicTrans.RollBack();//因为此时不一定插入数据库,所以回滚,保持发票不跳号
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
            //##未完全测试
            if (e.KeyCode == Keys.Enter)
            {
                int count = 0;
                try
                {
                    count = Convert.ToInt32(this.tbCount.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("输入分票得数量不合法" + ex.Message);
                    this.tbCount.Focus();
                    this.tbCount.SelectAll();

                    return;
                }
                if (count > this.splitCounts)
                {
                    MessageBox.Show("当前可分发票数不能大于: " + splitCounts.ToString());
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
                    MessageBox.Show("输入间隔天数不合法" + ex.Message);
                    this.tbSplitDay.Focus();
                    this.tbSplitDay.SelectAll();
                    return;
                }
                if (count > 999)
                {
                    MessageBox.Show("间隔天数不能大于999天!");
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

        #region 列枚举

        /// <summary>
        /// 支付方式列枚举
        /// </summary>
        enum PayModeCols
        {
            /// <summary>
            /// 支付方式
            /// </summary>
            PayMode = 0,
            /// <summary>
            /// 金额
            /// </summary>
            Cost = 1,
            /// <summary>
            /// 开户银行
            /// </summary>
            Bank = 2,
            /// <summary>
            /// 帐号
            /// </summary>
            Account = 3,
            /// <summary>
            /// 开据单位
            /// </summary>
            Company = 4,
            /// <summary>
            /// 支票，汇票，交易号
            /// </summary>
            PosNo = 5
        }

        /// <summary>
        /// 记账方式列枚举
        /// </summary>
        enum PubTypes
        {
            /// <summary>
            /// 记账方式
            /// </summary>
            PubType = 0,
            /// <summary>
            /// 金额
            /// </summary>
            Cost = 1,
            /// <summary>
            /// 备注
            /// </summary>
            Mark = 2
        }

        #endregion

        #region 方法

        private void PreViewInvoice()
        {
            //##未完全测试
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
                    MessageBox.Show("初始化发票失败!" + e.Message);

                    return;
                }
            }

            if (iInvoicePrint == null)
            {
                MessageBox.Show("请维护打印票据，查找打印票据失败！");
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
            decimal shouldPay = 0m;//应缴金额
            for (int i = 0; i < this.fpPayMode_Sheet1.RowCount; i++)
            {
                if (this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text != string.Empty)
                {
                    if (this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text != "现金" &&
                         this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text != "珠海医保统筹" &&
                         this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text != "中山医保统筹"
                        )
                    {
                        //统计除 【现金 和 统筹金额(pub_cost)】 之外的金额
                        sumCost += NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value);
                    }
                }
            }
            if (realCost > sumCost)
            {
                //把剩余支付金额归入现金
                this.fpPayMode_Sheet1.Cells[0, (int)PayModeCols.Cost].Value = realCost - sumCost;
            }
            else if (realCost < sumCost)
            {
                //提示错误
                MessageBox.Show("请核准支付方式金额,总应付金额：" + realCost.ToString());
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
        /// 处理记账方式列表回车
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
        /// 归类最小费用
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

            //设置界面显示
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
        /// 初始化支付方式信息
        /// </summary>
        private int InitPayMode()
        {
            ArrayList tempPayModes = this.managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PAYMODES);
            this.helpPayMode.ArrayObject = tempPayModes;
            if (tempPayModes == null || tempPayModes.Count == 0)
            {
                MessageBox.Show("获取支付方式列表错误");
                return -1;
            }
            //至少保留现金和记账支付方式
            FS.FrameWork.Models.NeuObject objCA = new FS.FrameWork.Models.NeuObject();
            objCA.ID = "CA";
            objCA.Name = "现金";

            FS.FrameWork.Models.NeuObject objPB = new FS.FrameWork.Models.NeuObject();
            objPB.ID = "PB";
            objPB.Name = "记账";

            if (helpPayMode.GetObjectFromID(objCA.ID) == null)
            {
                helpPayMode.ArrayObject.Add(objCA);
            }

            if (helpPayMode.GetObjectFromID(objPB.ID) == null)
            {
                helpPayMode.ArrayObject.Add(objPB);
            }

            //支付方式列表改为固定模式
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
        /// 初始化分发票信息
        /// </summary>
        /// <returns></returns>
        private int InitSplitInvoice()
        {
            string tmpCtrlValue = feeIntegrate.GetControlValue(FS.HISFC.BizProcess.Integrate.Const.CANSPLIT, "0");
            if (string.IsNullOrEmpty(tmpCtrlValue) || "1".Equals(tmpCtrlValue) == false)
            {
                MessageBox.Show("是否分发票参数没有维护，现在采用默认值: 不可分发票!");
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
                MessageBox.Show("获取支付方式列表错误");
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
        /// 初始化farpoint,屏蔽一些热键
        /// </summary>
        private void InitFp()
        {
            InputMap im;

            #region 记账方式FP

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

            #region 支付方式FP

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
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public int Init()
        {

            #region 允许手工输入优惠金额的合同单位

            try
            {
                //临时处理
                this.hsCanInputRCMoneyPact = new Hashtable();
                hsCanInputRCMoneyPact.Add("5", null);
                hsCanInputRCMoneyPact.Add("9", null);
            }
            catch (Exception ex)
            { }

            #endregion

            //初始化FarPoint信息
            this.InitFp();

            //初始化最小费用列表
            this.helpMinFee.ArrayObject = this.managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.MINFEE);

            //初始化支付方式列表
            if (this.InitPayMode() < 0)
            {
                return -1;
            }

            //初始化记账方式列表
            if (this.InitPubType() < 0)
            {
                return -1;
            }

            //初始化分发票
            if (this.InitSplitInvoice() < 0)
            {
                return -1;
            }

            //是否可以修改发票打印日期
            this.isCanModifyInvoiceDate = this.controlParam.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.MODIFY_INVOICE_PRINTDATE, false, false);
            if (this.isCanModifyInvoicePrintDate == true)
            {
                this.dateTimePicker1.Enabled = true;
            }
            else
            {
                this.dateTimePicker1.Enabled = false;
            }

            //优化，放进内存中??
            #region 合同单位对应支付方式

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

            #region 查找【珠海医保】和【中山医保】的合同单位，根据待遇算法DLL来作为查询条件。

            try
            {
                //?如果修改接口名字的情况，这里要修改 gumzh?
                FS.HISFC.BizLogic.Fee.PactUnitInfo pactMgr = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
                ArrayList alZH = pactMgr.QueryPactUnitByDLLName("ZhuHaiSI.dll");
                ArrayList alZS = pactMgr.QueryPactUnitByDLLName("ZhongShanSI.dll");

                //珠海社保
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
                //中山医保
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
        /// 保存划价信息
        /// </summary>
        /// <returns></returns>
        public bool SaveCharge()
        {
            this.ChargeButtonClicked();
            return true;
        }

        /// <summary>
        /// 保存收费信息##
        /// </summary>
        /// <returns></returns>
        public bool SaveFee()
        {
            string errText = string.Empty;
            int errRow = 0, errCol = 0;
            this.GetFT();
            //判断支付方式
            if (!this.IsPayModesValid(ref errText, ref errRow, ref errCol))
            {
                MessageBox.Show(errText, "提示");
                this.fpPayMode.Focus();
                this.fpPayMode_Sheet1.SetActiveCell(errRow, errCol, false);

                return false;
            }
            alPatientPayModeInfo = QueryBalancePays();
            if (alPatientPayModeInfo == null)
            {
                MessageBox.Show("获得支付方式信息出错!", "提示");
                return false;
            }
            //判断分发票方式
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
        /// 设置控件默认焦点
        /// </summary>
        public void SetControlFocus()
        {
            this.panel1.Focus();
            this.groupBox2.Focus();
            this.tbRealCost.Focus();
        }

        /// <summary>
        /// 获取支付方式的行号
        /// </summary>
        /// <param name="sv"></param>
        /// <param name="name">支付方式</param>
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
        /// 设置列数据
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
        /// 验证支付方式输入是否合法
        /// </summary>
        /// <param name="errText">错误信息</param>
        /// <param name="errRow">错误行</param>
        /// <param name="errCol">错误列</param>
        /// <returns>成功 true 错误false</returns>
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
                    MessageBox.Show("金额输入不合法" + ex.Message);
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
                    errText = "支付方式输入错误!";
                    errRow = i;
                    errCol = (int)PayModeCols.PayMode;
                    return false;
                }
            }

            if (tempTotalCost != this.totCost)
            {
                errText = "请核准支付方式金额";
            }

            return true;
        }

        /// <summary>
        /// 获得支付方式的集合
        /// </summary>
        /// <returns>成功 支付方式的集合 失败 null</returns>
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
                    //记账,需要同时获取记账方式表格中的信息
                    balancePay.Bank.Name = this.fpPubType_Sheet1.Cells[0, (int)PubTypes.PubType].Text;
                    balancePay.Bank.ID = helpPubType.GetID(balancePay.Bank.Name);
                    balancePay.Memo = this.fpPubType_Sheet1.Cells[0, (int)PubTypes.Mark].Text;
                    if (string.IsNullOrEmpty(balancePay.Bank.ID) || string.IsNullOrEmpty(balancePay.Bank.Name))
                    {
                        MessageBox.Show("记账方式错误!请选择记账方式!");
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
                MessageBox.Show("支付方式加总金额不等于总金额，请核对!");
                return null;
            }

            return balancePays;
        }

        /// <summary>
        /// 验证分发票数据是否合法
        /// </summary>
        /// <returns>成功 true 失败 false</returns>
        private bool IsSplitInvoicesValid()
        {
            decimal tempTotalCost = 0m;

            for (int i = 0; i < this.fpSplit_Sheet1.RowCount; i++)
            {
                if ("总发票".Equals(this.fpSplit_Sheet1.Cells[i, 2].Text))
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
                    MessageBox.Show("金额输入不合法!\n" + ex.Message);
                    return false;
                }
            }

            if (FS.FrameWork.Public.String.FormatNumber(tempTotalCost, 2) != this.totCost)
            {
                MessageBox.Show("分发票金额与总金额不符!请重新分配!");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 获得分发票信息
        /// </summary>
        /// <returns>成功 分发票信息 失败 null</returns>
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