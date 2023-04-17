using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using System.Collections;
using FS.HISFC.Models;

namespace FS.SOC.HISFC.InpatientFee.Components.Balance
{
    /// <summary>
    /// [功能描述: 结算支付方式选择]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-4]<br></br>
    /// </summary>
    public partial class frmBalancePayNew : FS.FrameWork.WinForms.Forms.BaseForm
    {
        public frmBalancePayNew()
        {
            InitializeComponent();

            this.txtPay.KeyDown += new KeyEventHandler(txtPay_KeyDown);
            this.txtPay.Leave+=new EventHandler(txtPay_Leave);
            this.txtCharge.KeyDown += new KeyEventHandler(txtCharge_KeyDown);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            this.btnOK.Click += new EventHandler(btnOK_Click);
            this.fpPayType_Sheet1.CellChanged += new FarPoint.Win.Spread.SheetViewEventHandler(fpPayType_Sheet1_CellChanged);
            this.btnReCompute.Click += new EventHandler(btnReCompute_Click);
        }
        
        #region 变量

        private FS.FrameWork.Public.ObjectHelper payWayHelper = new FS.FrameWork.Public.ObjectHelper();
        private FS.FrameWork.Public.ObjectHelper bankHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ACY(帐户支付)
        /// </summary>
        private int rowACY = -1;

        /// <summary>
        /// ACD(帐户赠送) - 非实际收入金额
        /// </summary>
        private int rowACD = -1;

        /// <summary>
        /// ECO(优惠) - 非实际收入金额
        /// </summary>
        private int rowECO = -1;

        /// <summary>
        /// 现金支付方式
        /// </summary>
        private int rowCA = -1;
        /// <summary>
        /// 所有账户类型
        /// </summary>
        private ArrayList alAccountType = new ArrayList();
        /// <summary>
        /// 账户实体
        /// </summary>
        private FS.HISFC.Models.Account.Account currentAccountInfo = null;

        /// <summary>
        /// 账户业务层
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();

        /// <summary>
        /// 实体变量
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// 管理业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// 账户类型
        /// </summary>
        private List<FS.HISFC.Models.Account.AccountDetail> accountDetailList = new List<FS.HISFC.Models.Account.AccountDetail>();
        /// <summary>
        /// 账户类型实体
        /// </summary>
        private FS.HISFC.Models.Account.AccountDetail currentAccountDetail = new FS.HISFC.Models.Account.AccountDetail();


        #endregion

        #region 属性

        /// <summary>
        /// 患者信息
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            get { return patientInfo; }
            set { patientInfo = value; }
        }
        private decimal shouldPay = 0M;
        /// <summary>
        /// 应收金额
        /// </summary>
        public decimal ShouldPay
        {
            set
            {
                this.shouldPay = value;
            }
        }

        private decimal arrearBalance = 0M;
        /// <summary>
        /// 是否欠费结算
        /// </summary>
        public decimal ArrearBalance
        {
            set
            {
                arrearBalance = value;
            }
        }

        private decimal returnPay = 0M;
        /// <summary>
        /// 应返金额
        /// </summary>
        public decimal ReturnPay
        {
            set
            {
                returnPay = value;
            }
        }

        private bool isOK = false;
        public bool IsOK
        {
            get
            {
                return isOK;
            }
        }

        private List<FS.HISFC.Models.Fee.Inpatient.BalancePay> listBalancePay = new List<FS.HISFC.Models.Fee.Inpatient.BalancePay>();
        /// <summary>
        /// 支付方式信息
        /// </summary>
        public List<FS.HISFC.Models.Fee.Inpatient.BalancePay> ListBalancePay
        {
            get
            {
                return listBalancePay;
            }
        }

        /// <summary>
        /// 自付金额 对应 中山医保IC卡
        /// </summary>
        private decimal balancePayCost = 0m;

        /// <summary>
        /// 自付金额 对应 中山医保IC卡
        /// </summary>
        public decimal BalancePayCost
        {
            get { return balancePayCost; }
            set { balancePayCost = value; }
        }

        /// <summary>
        /// 民政补助金额
        /// </summary>
        private decimal balanceMZCost = 0m;

        /// <summary>
        /// 民政补助金额
        /// </summary>
        public decimal BalanceMZCost
        {
            get { return balanceMZCost; }
            set { balanceMZCost = value; }
        }



        #endregion

        #region 方法

        protected virtual void Init()
        {
            //清空
            this.fpPayType_Sheet1.RowCount = 0;

            #region 支付方式
            this.InitPayMode();
            #endregion

            #region 银行
            this.InitBankType();
            #endregion

            #region 账户类型
            alAccountType = managerIntegrate.GetAccountTypeList();

            if (alAccountType == null)
            {
                MessageBox.Show("帐户类型加载失败！");
                return;
            }
            this.cmbAccountType.AddItems(alAccountType);
            this.cmbAccountType.Tag = "1";
            #endregion
            this.currentAccountInfo = this.accountManager.GetAccountByCardNoEX(this.patientInfo.PID.CardNO);
            string mess = "基本账户余额：{0}   赠送账户余额：{1} ";
            if (this.currentAccountInfo != null)
            {
                accountDetailList = this.accountManager.GetAccountDetail(this.currentAccountInfo.ID, this.cmbAccountType.Tag.ToString(),"1");
                if (accountDetailList.Count > 0)
                {
                    currentAccountDetail = accountDetailList[0] as FS.HISFC.Models.Account.AccountDetail;
                    mess = string.Format(mess, currentAccountDetail.BaseVacancy.ToString("F2"), currentAccountDetail.DonateVacancy.ToString("F2"));
                }
                else
                {
                    mess = string.Format(mess, "0.00", "0.00");
                }
            }
            else
            {
                mess = string.Format(mess, "0.00", "0.00");
            }
            this.txtVacancyShow.Text = mess;
            this.ComputeCost();
        }
        /// <summary>
        /// 开户银行初始化
        /// </summary>
        private void InitBankType()
        {

            FarPoint.Win.Spread.CellType.ComboBoxCellType banktype = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            ArrayList al = CommonController.CreateInstance().QueryConstant(FS.HISFC.Models.Base.EnumConstant.BANK);
            if (al == null)
            {
                CommonController.CreateInstance().MessageBox("获取开户银行失败", MessageBoxIcon.Warning);
                return;
            }
            bankHelper.ArrayObject = al;
            banktype.Items = new string[al.Count];
            for (int i = 0; i < al.Count; i++)
            {
                FS.FrameWork.Models.NeuObject obj = al[i] as FS.FrameWork.Models.NeuObject;
                banktype.Items[i] = obj.Name;
            }
            this.fpPayType_Sheet1.Columns[(int)ColumnPayNew.PayBank].CellType = banktype;
        }
        /// <summary>
        /// 支付方式初始化
        /// </summary>
        private void InitPayMode()
        {

            ArrayList al = CommonController.CreateInstance().QueryConstant(FS.HISFC.Models.Base.EnumConstant.PAYMODES);
            if (al == null)
            {
                CommonController.CreateInstance().MessageBox("获取支付方式失败", MessageBoxIcon.Warning);
                return;
            }
            payWayHelper.ArrayObject = al;
            this.fpPayType_Sheet1.Rows.Count = 0;  //清空
            for (int i = 0; i < al.Count; i++)
            {
                //增加1行
                this.fpPayType_Sheet1.Rows.Count++;

                FS.FrameWork.Models.NeuObject obj = al[i] as FS.FrameWork.Models.NeuObject;

                //现金支付
                if (obj.ID == "CA")
                {
                    this.rowCA = i;
                }

                //ACY(帐户支付)
                if ((obj as FS.HISFC.Models.Base.Const).UserCode == "ACY")
                {
                    this.rowACY = i;
                }
                //ACD(帐户赠送)
                if ((obj as FS.HISFC.Models.Base.Const).UserCode == "ACD")
                {
                    this.rowACD = i;
                }

                //ECO(优惠)
                if ((obj as FS.HISFC.Models.Base.Const).UserCode == "ECO")
                {
                    this.rowECO = i;
                }
                this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayWay].Text = obj.Name;   //支付方式名字
                this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayWay].Tag = obj.ID;     //支付方式编码
                this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayWay].Locked = true;
            }
        }
        private void ComputeCost()
        {
            this.Clear();
            if (arrearBalance > 0)
            {
                this.lblShouldPay.Text = "欠费金额";
                this.txtShouldPay.Text = (shouldPay + arrearBalance).ToString("F2");
                this.lblShouldPay.ForeColor = Color.Red;
                this.txtShouldPay.ForeColor = Color.Red;

                this.fpPayType_Sheet1.RowHeader.Rows[0].Label = "欠";
                this.fpPayType_Sheet1.RowHeader.Rows[0].BackColor = Color.Red;
                this.fpPayType_Sheet1.Cells[0, (int)ColumnPayNew.PayCost].Value = shouldPay;             

                this.txtPay.Text = shouldPay.ToString("F2");
            }
            else if(shouldPay>0)
            {
                this.lblShouldPay.Text = "应收(自费)";
                this.txtShouldPay.Text = (shouldPay + arrearBalance).ToString("F2");
                this.lblShouldPay.ForeColor = Color.Red;
                this.txtShouldPay.ForeColor = Color.Red;

                //this.fpPayType_Sheet1.RowHeader.Rows[0].Label = "收";
                //this.fpPayType_Sheet1.RowHeader.Rows[0].BackColor = Color.Red;

                if (this.currentAccountInfo == null)
                {

                    this.fpPayType_Sheet1.RowHeader.Rows[this.rowCA].Label = "收";
                    this.fpPayType_Sheet1.RowHeader.Rows[this.rowCA].BackColor = Color.Red;
                    this.fpPayType_Sheet1.Cells[this.rowCA, (int)ColumnPayNew.PayCost].Value = shouldPay;
                }
                else
                {
                    accountDetailList = this.accountManager.GetAccountDetail(this.currentAccountInfo.ID, this.cmbAccountType.Tag.ToString(),"1");
                    if (accountDetailList.Count <= 0)
                    {
                        this.fpPayType_Sheet1.Cells[this.rowACY, (int)ColumnPayNew.PayCost].Locked = true;
                        this.fpPayType_Sheet1.Cells[this.rowACD, (int)ColumnPayNew.PayCost].Locked = true;
                        this.fpPayType_Sheet1.Cells[this.rowACD, (int)ColumnPayNew.PayCost].Value = null;
                        this.fpPayType_Sheet1.Cells[this.rowACY, (int)ColumnPayNew.PayCost].Value = null;
                        if (shouldPay > 0)
                        {
                            this.fpPayType_Sheet1.RowHeader.Rows[this.rowCA].Label = "收";
                            this.fpPayType_Sheet1.RowHeader.Rows[this.rowCA].BackColor = Color.Red;
                        }
                        this.fpPayType_Sheet1.Cells[this.rowCA, (int)ColumnPayNew.PayCost].Value = shouldPay;
                        this.SetFP();

                        return;
                    }
                    else
                    {
                        this.fpPayType_Sheet1.Cells[this.rowACY, (int)ColumnPayNew.PayCost].Locked = false;
                        this.fpPayType_Sheet1.Cells[this.rowACD, (int)ColumnPayNew.PayCost].Locked = false;
                        this.fpPayType_Sheet1.Cells[this.rowCA, (int)ColumnPayNew.PayCost].Value = null;
                        currentAccountDetail = accountDetailList[0] as FS.HISFC.Models.Account.AccountDetail;
                        decimal baseVacancy = this.currentAccountDetail.BaseVacancy;          //充值账户余额
                        decimal donateVacancy = this.currentAccountDetail.DonateVacancy;    //赠送账户余额
                        if (baseVacancy > 0 && shouldPay > 0)
                        {

                            decimal donatePay = Math.Round(shouldPay * (donateVacancy / (baseVacancy + donateVacancy)), 2);
                            decimal basePay = shouldPay - donatePay;
                            if (basePay <= baseVacancy)
                            {
                                //基本账户余额够用；
                                if (basePay > 0)
                                {
                                    this.fpPayType_Sheet1.RowHeader.Rows[this.rowACY].Label = "收";
                                    this.fpPayType_Sheet1.RowHeader.Rows[this.rowACY].BackColor = Color.Red;
                                }
                                this.fpPayType_Sheet1.Cells[this.rowACY, (int)ColumnPayNew.PayCost].Text = basePay.ToString();

                                //赠送账户
                                if (donatePay <= donateVacancy)
                                {
                                    //余额够用
                                    if (donatePay > 0)
                                    {
                                        this.fpPayType_Sheet1.RowHeader.Rows[this.rowACD].Label = "收";
                                        this.fpPayType_Sheet1.RowHeader.Rows[this.rowACD].BackColor = Color.Red;
                                    }
                                    this.fpPayType_Sheet1.Cells[this.rowACD, (int)ColumnPayNew.PayCost].Text = donatePay.ToString();
                                }
                                else
                                {
                                    //余额不够用
                                    donatePay = donateVacancy;
                                    if (donatePay > 0)
                                    {
                                        this.fpPayType_Sheet1.RowHeader.Rows[this.rowACD].Label = "收";
                                        this.fpPayType_Sheet1.RowHeader.Rows[this.rowACD].BackColor = Color.Red;
                                    }
                                    this.fpPayType_Sheet1.Cells[this.rowACD, (int)ColumnPayNew.PayCost].Text = donatePay.ToString();
                                }
                            }
                            else
                            {

                                //基本账户余额不够用；
                                basePay = baseVacancy;
                                this.fpPayType_Sheet1.Cells[this.rowACY, (int)ColumnPayNew.PayCost].Text = basePay.ToString();

                                decimal accoutRealCost = Math.Round(basePay / (baseVacancy / (baseVacancy + donateVacancy)), 2);
                                donatePay = accoutRealCost - basePay;

                                //赠送账户
                                if (donatePay <= donateVacancy)
                                {
                                    //余额够用
                                    if (donatePay > 0)
                                    {
                                        this.fpPayType_Sheet1.RowHeader.Rows[this.rowCA].Label = "收";
                                        this.fpPayType_Sheet1.RowHeader.Rows[this.rowCA].BackColor = Color.Red;
                                    }
                                    this.fpPayType_Sheet1.Cells[this.rowACD, (int)ColumnPayNew.PayCost].Text = donatePay.ToString();
                                }
                                else
                                {
                                    //余额不够用
                                    donatePay = donateVacancy;
                                    if (donatePay > 0)
                                    {
                                        this.fpPayType_Sheet1.RowHeader.Rows[this.rowCA].Label = "收";
                                        this.fpPayType_Sheet1.RowHeader.Rows[this.rowCA].BackColor = Color.Red;
                                    }
                                    this.fpPayType_Sheet1.Cells[this.rowACD, (int)ColumnPayNew.PayCost].Text = donatePay.ToString();
                                }
                            }
                            //自费支付金额 CA
                            decimal leftCAcost = shouldPay - basePay - donatePay;
                            if (leftCAcost > 0)
                            {
                                this.fpPayType_Sheet1.RowHeader.Rows[this.rowCA].Label = "收";
                                this.fpPayType_Sheet1.RowHeader.Rows[this.rowCA].BackColor = Color.Red;
                            }
                            this.fpPayType_Sheet1.Cells[this.rowCA, (int)ColumnPayNew.PayCost].Text = leftCAcost.ToString();
                            this.fpPayType_Sheet1.Cells[this.rowCA, (int)ColumnPayNew.PayWay].Locked = true;

                        }
                    }
                    this.SetFP();
                }
                
                this.txtPay.Text = shouldPay.ToString("F2");
            }
            else if (returnPay > 0)
            {
                this.lblShouldPay.Text = "应退金额";
                this.txtShouldPay.Text = (returnPay).ToString("F2");
                this.lblShouldPay.ForeColor = Color.Blue;
                this.txtShouldPay.ForeColor = Color.Blue;

                this.fpPayType_Sheet1.RowHeader.Rows[0].Label = "退";
                this.fpPayType_Sheet1.RowHeader.Rows[0].BackColor = Color.Blue;                
                this.fpPayType_Sheet1.Cells[0, (int)ColumnPayNew.PayCost].Value = returnPay;

                this.txtPay.Text = returnPay.ToString("F2");
            }
            //this.fpPayType_Sheet1.Cells[0, (int)ColumnPayNew.PayWay].Text = "现金";

            if (this.BalancePayCost > 0 && this.BalanceMZCost <= 0)
            {
                #region 中山医保的PAY_COST的特殊处理(都是收取)

                this.Clear();
                if (arrearBalance > 0)
                {
                    this.lblShouldPay.Text = "欠费金额";
                    this.txtShouldPay.Text = (shouldPay + arrearBalance).ToString("F2");
                    this.lblShouldPay.ForeColor = Color.Red;
                    this.txtShouldPay.ForeColor = Color.Red;

                    this.fpPayType_Sheet1.RowHeader.Rows[0].Label = "收";
                    this.fpPayType_Sheet1.RowHeader.Rows[0].BackColor = Color.Red;
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPayNew.PayWay].Text = "社会保障卡(中山)";
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPayNew.PayCost].Value = this.BalancePayCost;

                    this.txtPay.Text = shouldPay.ToString("F2");
                }
                else if (shouldPay > 0)
                {
                    this.lblShouldPay.Text = "应收(自费)";
                    this.txtShouldPay.Text = (shouldPay + arrearBalance).ToString("F2");
                    this.lblShouldPay.ForeColor = Color.Red;
                    this.txtShouldPay.ForeColor = Color.Red;

                    this.fpPayType_Sheet1.RowHeader.Rows[0].Label = "收";
                    this.fpPayType_Sheet1.RowHeader.Rows[0].BackColor = Color.Red;
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPayNew.PayWay].Text = "社会保障卡(中山)";
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPayNew.PayCost].Value = this.BalancePayCost;

                    this.txtPay.Text = shouldPay.ToString("F2");
                }
                else if (returnPay > 0)
                {
                    this.lblShouldPay.Text = "应退金额";
                    this.txtShouldPay.Text = (returnPay).ToString("F2");
                    this.lblShouldPay.ForeColor = Color.Blue;
                    this.txtShouldPay.ForeColor = Color.Blue;

                    this.fpPayType_Sheet1.RowHeader.Rows[0].Label = "收";
                    this.fpPayType_Sheet1.RowHeader.Rows[0].BackColor = Color.Red;
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPayNew.PayWay].Text = "社会保障卡(中山)";
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPayNew.PayCost].Value = this.BalancePayCost;

                    this.txtPay.Text = returnPay.ToString("F2");
                }

                #endregion
            }

            if (this.BalancePayCost <= 0 && this.BalanceMZCost > 0)
            {
                #region 民政补助金额的特殊处理(都是收取)

                this.Clear();
                if (arrearBalance > 0)
                {
                    this.lblShouldPay.Text = "欠费金额";
                    this.txtShouldPay.Text = (shouldPay + arrearBalance).ToString("F2");
                    this.lblShouldPay.ForeColor = Color.Red;
                    this.txtShouldPay.ForeColor = Color.Red;

                    this.fpPayType_Sheet1.RowHeader.Rows[0].Label = "收";
                    this.fpPayType_Sheet1.RowHeader.Rows[0].BackColor = Color.Red;
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPayNew.PayWay].Text = "优惠";
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPayNew.PayCost].Value = this.BalanceMZCost;

                    this.txtPay.Text = shouldPay.ToString("F2");
                }
                else if (shouldPay > 0)
                {
                    this.lblShouldPay.Text = "应收(自费)";
                    this.txtShouldPay.Text = (shouldPay + arrearBalance).ToString("F2");
                    this.lblShouldPay.ForeColor = Color.Red;
                    this.txtShouldPay.ForeColor = Color.Red;

                    this.fpPayType_Sheet1.RowHeader.Rows[0].Label = "收";
                    this.fpPayType_Sheet1.RowHeader.Rows[0].BackColor = Color.Red;
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPayNew.PayWay].Text = "优惠";
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPayNew.PayCost].Value = this.BalanceMZCost;

                    this.txtPay.Text = shouldPay.ToString("F2");
                }
                else if (returnPay > 0)
                {
                    this.lblShouldPay.Text = "应退金额";
                    this.txtShouldPay.Text = (returnPay).ToString("F2");
                    this.lblShouldPay.ForeColor = Color.Blue;
                    this.txtShouldPay.ForeColor = Color.Blue;

                    this.fpPayType_Sheet1.RowHeader.Rows[0].Label = "收";
                    this.fpPayType_Sheet1.RowHeader.Rows[0].BackColor = Color.Red;
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPayNew.PayWay].Text = "优惠";
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPayNew.PayCost].Value = this.BalanceMZCost;

                    this.txtPay.Text = returnPay.ToString("F2");
                }

                #endregion
            }

            if (this.BalancePayCost > 0 && this.BalanceMZCost > 0)
            {
                #region 中山医保的PAY_COST的特殊处理 与 民政补助金额的特殊处理(都是收取)

                this.Clear();
                if (arrearBalance > 0)
                {
                    this.lblShouldPay.Text = "欠费金额";
                    this.txtShouldPay.Text = (shouldPay + arrearBalance).ToString("F2");
                    this.lblShouldPay.ForeColor = Color.Red;
                    this.txtShouldPay.ForeColor = Color.Red;

                    this.fpPayType_Sheet1.CellChanged -= new FarPoint.Win.Spread.SheetViewEventHandler(fpPayType_Sheet1_CellChanged);
                    this.fpPayType_Sheet1.RowHeader.Rows[0].Label = "收";
                    this.fpPayType_Sheet1.RowHeader.Rows[0].BackColor = Color.Red;
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPayNew.PayWay].Text = "社会保障卡(中山)";
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPayNew.PayCost].Value = this.BalancePayCost;
                    this.fpPayType_Sheet1.CellChanged += new FarPoint.Win.Spread.SheetViewEventHandler(fpPayType_Sheet1_CellChanged);

                    this.fpPayType_Sheet1.RowHeader.Rows[1].Label = "收";
                    this.fpPayType_Sheet1.RowHeader.Rows[1].BackColor = Color.Red;
                    this.fpPayType_Sheet1.Cells[1, (int)ColumnPayNew.PayWay].Text = "优惠";
                    this.fpPayType_Sheet1.Cells[1, (int)ColumnPayNew.PayCost].Value = this.BalanceMZCost;

                    this.txtPay.Text = shouldPay.ToString("F2");
                }
                else if (shouldPay > 0)
                {
                    this.lblShouldPay.Text = "应收(自费)";
                    this.txtShouldPay.Text = (shouldPay + arrearBalance).ToString("F2");
                    this.lblShouldPay.ForeColor = Color.Red;
                    this.txtShouldPay.ForeColor = Color.Red;

                    this.fpPayType_Sheet1.CellChanged -= new FarPoint.Win.Spread.SheetViewEventHandler(fpPayType_Sheet1_CellChanged);
                    this.fpPayType_Sheet1.RowHeader.Rows[0].Label = "收";
                    this.fpPayType_Sheet1.RowHeader.Rows[0].BackColor = Color.Red;
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPayNew.PayWay].Text = "社会保障卡(中山)";
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPayNew.PayCost].Value = this.BalancePayCost;
                    this.fpPayType_Sheet1.CellChanged += new FarPoint.Win.Spread.SheetViewEventHandler(fpPayType_Sheet1_CellChanged);

                    this.fpPayType_Sheet1.RowHeader.Rows[1].Label = "收";
                    this.fpPayType_Sheet1.RowHeader.Rows[1].BackColor = Color.Red;
                    this.fpPayType_Sheet1.Cells[1, (int)ColumnPayNew.PayWay].Text = "优惠";
                    this.fpPayType_Sheet1.Cells[1, (int)ColumnPayNew.PayCost].Value = this.BalanceMZCost;

                    this.txtPay.Text = shouldPay.ToString("F2");
                }
                else if (returnPay > 0)
                {
                    this.lblShouldPay.Text = "应退金额";
                    this.txtShouldPay.Text = (returnPay).ToString("F2");
                    this.lblShouldPay.ForeColor = Color.Blue;
                    this.txtShouldPay.ForeColor = Color.Blue;

                    this.fpPayType_Sheet1.CellChanged -= new FarPoint.Win.Spread.SheetViewEventHandler(fpPayType_Sheet1_CellChanged);
                    this.fpPayType_Sheet1.RowHeader.Rows[0].Label = "收";
                    this.fpPayType_Sheet1.RowHeader.Rows[0].BackColor = Color.Red;
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPayNew.PayWay].Text = "社会保障卡(中山)";
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPayNew.PayCost].Value = this.BalancePayCost;
                    this.fpPayType_Sheet1.CellChanged += new FarPoint.Win.Spread.SheetViewEventHandler(fpPayType_Sheet1_CellChanged);

                    this.fpPayType_Sheet1.RowHeader.Rows[1].Label = "收";
                    this.fpPayType_Sheet1.RowHeader.Rows[1].BackColor = Color.Red;
                    this.fpPayType_Sheet1.Cells[1, (int)ColumnPayNew.PayWay].Text = "优惠";
                    this.fpPayType_Sheet1.Cells[1, (int)ColumnPayNew.PayCost].Value = this.BalanceMZCost;

                    this.txtPay.Text = returnPay.ToString("F2");
                }

                #endregion
            }

            this.txtPay.Focus();
            this.txtPay.SelectAll();
        }

        /// <summary>
        /// 清屏
        /// </summary>
        public void Clear()
        {
            this.fpPayType_Sheet1.CellChanged -= new FarPoint.Win.Spread.SheetViewEventHandler(fpPayType_Sheet1_CellChanged);

            this.listBalancePay.Clear();
            //for (int i = 0; i < this.fpPayType_Sheet1.RowCount; i++)
            //{
            //    this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayWay].Text = string.Empty;
            //    this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayCost].Value = 0M;
            //    this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayBank].Text = string.Empty;
            //    this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayCorporation].Text = string.Empty;
            //    this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayAccountNO].Text = string.Empty;
            //    this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayTranscationNO].Text = string.Empty;

            //    this.fpPayType_Sheet1.RowHeader.Rows[i].Label = (i + 1).ToString();  //行号
            //    this.fpPayType_Sheet1.RowHeader.Rows[i].BackColor = Color.White;
            //}
            this.lblShouldPay.Text = "应收(自费)";
            this.txtShouldPay.Text = "0.00";
            this.lblShouldPay.ForeColor = Color.Red;
            this.txtShouldPay.ForeColor = Color.Red;
            this.txtPay.Text = "0.00";
            this.txtCharge.Text = "0.00";
            this.txtCharge.ReadOnly = true;
            this.txtCharge.AllowNegative = true;
            this.isOK = false;

            this.fpPayType_Sheet1.CellChanged += new FarPoint.Win.Spread.SheetViewEventHandler(fpPayType_Sheet1_CellChanged);

        }

        private bool ValidBalancePay(ref string errText)
        {
            decimal payTypeCost = 0m;
            string payType = "";
            decimal payAllCost = 0m;

            for (int i = 0; i < this.fpPayType_Sheet1.Rows.Count; i++)
            {
                payType = this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayWay].Text;
                if (this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayCost].Value == null)
                {
                    payTypeCost = 0;
                }
                else
                {
                    int sign = 1;
                    if (arrearBalance > 0 && this.fpPayType_Sheet1.RowHeader.Rows[i].Label == "欠")
                    {
                        sign = 1;
                    }
                    else if (shouldPay > 0 && this.fpPayType_Sheet1.RowHeader.Rows[i].Label == "收")
                    {
                        sign = 1;
                    }
                    else if (returnPay > 0 && this.fpPayType_Sheet1.RowHeader.Rows[i].Label == "退")
                    {
                        sign = 1;
                    }
                    else
                    {
                        sign = -1;
                    }

                    payTypeCost = decimal.Parse(this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayCost].Value.ToString()) * sign;
                }

                #region 屏蔽 by ma_d
                //if (payType.Trim() != "" && payTypeCost > 0)
                //{
                //    if (payType.Trim() == "支票" || payType.Trim() == "汇票")
                //    {
                //        if (this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayBank].Text.Trim() == "" ||
                //            this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayAccountNO].Text.Trim() == "" ||
                //            this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayCorporation].Text.Trim() == "" ||
                //            this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayTranscationNO].Text.Trim() == "")
                //        {
                //            errText = "支票汇票必须将完整的开户银行,帐户,开据单位,票号补充完整!";
                //            return false;
                //        }
                //    }
                //    if (payType.Trim() == "借记卡" || payType.Trim() == "信用卡")
                //    {
                //        if (this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayBank].Text.Trim() == "" ||
                //            this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayAccountNO].Text.Trim() == "" ||
                //            this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayTranscationNO].Text.Trim() == "")
                //        {
                //            errText = "支票汇票必须将完整的开户,银行帐户,交易号补充完整!";
                //            return false;
                //        }
                //    }
                //}
                #endregion

                payAllCost += payTypeCost;
            }
            //控制不能同时使用两种相同的支付方式
            for (int i = 0; i < this.fpPayType_Sheet1.RowCount; i++)
            {
                string paytype = this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayWay].Text.Trim();
                if (paytype != "")
                {
                    for (int j = i + 1; j < this.fpPayType_Sheet1.RowCount; j++)
                    {
                        if (paytype == this.fpPayType_Sheet1.Cells[j, (int)ColumnPayNew.PayWay].Text.Trim())
                        {
                            errText = "不能同时使用两种相同的支付方式";
                            return false;
                        }
                    }
                }
                else
                {
                    if (FS.FrameWork.Function.NConvert.ToDecimal(this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayCost].Value)>0)
                    {
                        errText = "支付方式不能为空";
                        return false;
                    }
                }
            }

            //如果是欠费
            if (arrearBalance>0)
            {
                if (this.shouldPay + arrearBalance < payAllCost)
                {
                    errText = "实收费用之和" + payAllCost.ToString() + "大于应收款项总额" + this.shouldPay.ToString() + "!";
                    return false;
                }
            }
            else
            {
                //判断是否金额相等
                if (returnPay > 0)
                {
                    if (this.returnPay != payAllCost)
                    {
                        errText = "应退款项总额" + this.returnPay.ToString() + "与分项费用之和" + payAllCost.ToString() + "不符合!";
                        return false;
                    }
                }
                else
                {
                    if (this.shouldPay != payAllCost)
                    {
                        errText = "应收款项总额" + this.shouldPay.ToString() + "与分项费用之和" + payAllCost.ToString() + "不符合!";
                        return false;
                    }
                }
            }

            return true;
        }

        private int Save()
        {
            string errText="";

            if (!this.ValidBalancePay(ref errText))
            {
                CommonController.CreateInstance().MessageBox(errText, MessageBoxIcon.Warning);
                return -1;
            }

            #region 屏蔽 
            //string payType = "";
            //decimal payTypeCost = 0m;
            //string bankName = "";
            //for (int i = 0; i < this.fpPayType_Sheet1.Rows.Count; i++)
            //{
            //    payType = this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayWay].Text;
            //    bankName = this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayBank].Text;
            //    if (this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayCost].Value == null)
            //    {
            //        payTypeCost = 0;
            //    }
            //    else
            //    {
            //        payTypeCost = FS.FrameWork.Function.NConvert.ToDecimal(this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayCost].Value.ToString());
            //    }

            //    if (payType.Trim() != "" && payTypeCost > 0)
            //    {
            //        FS.HISFC.Models.Fee.Inpatient.BalancePay balancePay = new FS.HISFC.Models.Fee.Inpatient.BalancePay();
            //        balancePay.TransKind.ID = "1";
            //        balancePay.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
            //        balancePay.PayType.ID = payWayHelper.GetID(payType);
            //        balancePay.PayType.Name = payType;
            //        balancePay.FT.TotCost = payTypeCost;
            //        balancePay.Qty = 1;
            //        balancePay.Bank.ID = bankHelper.GetID(bankName);
            //        balancePay.Bank.Name = bankName;
            //        balancePay.Bank.Account = this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayAccountNO].Text;
            //        balancePay.Bank.WorkName = this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayCorporation].Text;
            //        balancePay.Bank.InvoiceNO = this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayTranscationNO].Text;

            //        //1补收；2返还
            //        //balancePay.RetrunOrSupplyFlag = returnPay > 0 ? "2" : "1";
            //        if (this.fpPayType_Sheet1.RowHeader.Rows[i].Label == "退")
            //        {
            //            balancePay.RetrunOrSupplyFlag = "2";
            //        }
            //        else
            //        {
            //            balancePay.RetrunOrSupplyFlag = "1";
            //        }

            //        this.listBalancePay.Add(balancePay.Clone());
            //    }
            //}
            #endregion
            this.listBalancePay = this.QueryBalancePays();

            bool needCheckPassWord = false;
            foreach (FS.HISFC.Models.Fee.Inpatient.BalancePay pay in this.listBalancePay)
            {
                if (pay.PayType.ID == "DC" || pay.PayType.ID == "YS")
                {
                    needCheckPassWord = true;
                    break;
                }
            }

            //{2FB3E463-C00D-4ff4-974A-5D7AA0DB9CA0}
            if (needCheckPassWord)
            {
                FS.HISFC.BizProcess.Integrate.Fee feeMgr = new FS.HISFC.BizProcess.Integrate.Fee();
                if (!feeMgr.CheckAccountPassWord(this.PatientInfo))
                {
                    MessageBox.Show("账户支付失败！");
                    return -1;
                }
            }

            return 1;
        }

        #endregion

        #region 事件

        /// <summary>
        /// 支付farpoint CellChange事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void fpPayType_Sheet1_CellChanged(object sender, FarPoint.Win.Spread.SheetViewEventArgs e)
        {
            //只响应金额的变化
            if (e.Column != (int)ColumnPayNew.PayCost) return;
            //金额判断
            decimal PayTypeCost = 0m;
            for (int i = 0; i < this.fpPayType_Sheet1.Rows.Count; i++)
            {
                if (this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayCost].Value == null)
                {
                    continue;
                }
                else
                {
                    int sign = 1;
                    if (arrearBalance > 0 && this.fpPayType_Sheet1.RowHeader.Rows[i].Label == "欠")
                    {
                        sign = 1;
                    }
                    else if (shouldPay > 0 && this.fpPayType_Sheet1.RowHeader.Rows[i].Label == "收")
                    {
                        sign = 1;
                    }
                    else if (returnPay > 0 && this.fpPayType_Sheet1.RowHeader.Rows[i].Label == "退")
                    {
                        sign = 1;
                    }
                    else
                    {
                        sign = -1;
                    }

                    PayTypeCost += decimal.Parse(this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayCost].Value.ToString()) * sign;
                }
            }
            decimal Spay = decimal.Parse(this.txtShouldPay.Text);
            if (PayTypeCost == Spay)
            {
                return;
            }
            if (PayTypeCost < Spay)
            {
                decimal Rest = Spay - PayTypeCost;
                for (int i = 0; i < this.fpPayType_Sheet1.Rows.Count; i++)
                {
                    if (this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayWay].Value == null)
                    {
                        if (arrearBalance > 0)
                        {
                            this.fpPayType_Sheet1.RowHeader.Rows[i].Label = "欠";
                            this.fpPayType_Sheet1.RowHeader.Rows[i].BackColor = Color.Red;
                        }
                        else if (shouldPay > 0)
                        {
                            this.fpPayType_Sheet1.RowHeader.Rows[i].Label = "收";
                            this.fpPayType_Sheet1.RowHeader.Rows[i].BackColor = Color.Red;
                        }
                        else if (returnPay > 0)
                        {
                            this.fpPayType_Sheet1.RowHeader.Rows[i].Label = "退";
                            this.fpPayType_Sheet1.RowHeader.Rows[i].BackColor = Color.Blue;
                        }

                        this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayWay].Value = "现金";
                        this.fpPayType_Sheet1.Cells[i,  (int)ColumnPayNew.PayCost].Value = Rest;
                        this.fpPayType.Focus();
                        this.fpPayType_Sheet1.SetActiveCell(i, (int)ColumnPayNew.PayWay);
                        break;
                    }
                }
                return;
            }
            if (PayTypeCost > Spay)
            {
                bool found = false;

                for (int i = 0; i < this.fpPayType_Sheet1.Rows.Count; i++)
                {
                    if (false && this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayWay].Text.Trim() == "现金")
                    {
                        #region 废弃

                        //decimal Cash = 0m;//现金
                        //decimal Rest = 0m;//其他支付方式
                        //Cash = decimal.Parse(this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayCost].Value.ToString());
                        //Rest = PayTypeCost - Cash;
                        //if (Rest > Spay)
                        //{
                        //    FS.FrameWork.WinForms.Classes.Function.Msg("现金为负值，如果不允许，请更改！", 111);
                        //}

                        //if (decimal.Parse(this.txtPay.Text) > Cash)
                        //{
                        //    //找零
                        //    decimal RealPayCost = decimal.Parse(this.txtPay.Text);
                        //    decimal charge = 0m;
                        //    charge = RealPayCost - Cash;
                        //    this.txtCharge.Text = charge.ToString();
                        //}

                        ////现金为其他支付方式与应收应付之间的差额
                        //this.fpPayType_Sheet1.SetValue(i, (int)ColumnPayNew.PayCost, Spay - Rest);
                        //found = true;
                        //break;

                        #endregion
                    }
                    else if (this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayWay].Value == null)
                    {
                        found = true;
                        decimal Rest = Spay - PayTypeCost;  //差异金额

                        if (arrearBalance > 0)
                        {
                            this.fpPayType_Sheet1.RowHeader.Rows[i].Label = "退";
                            this.fpPayType_Sheet1.RowHeader.Rows[i].BackColor = Color.Blue;
                        }
                        else if (shouldPay > 0)
                        {
                            this.fpPayType_Sheet1.RowHeader.Rows[i].Label = "退";
                            this.fpPayType_Sheet1.RowHeader.Rows[i].BackColor = Color.Blue;
                        }
                        else if (returnPay > 0)
                        {
                            this.fpPayType_Sheet1.RowHeader.Rows[i].Label = "收";
                            this.fpPayType_Sheet1.RowHeader.Rows[i].BackColor = Color.Red;
                        }

                        this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayWay].Value = "现金";
                        this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayCost].Value = Math.Abs(Rest);
                        this.fpPayType.Focus();
                        this.fpPayType_Sheet1.SetActiveCell(i, (int)ColumnPayNew.PayWay);
                        break;
                    }
                }

                if (!found)
                {
                    FS.FrameWork.WinForms.Classes.Function.Msg("除现金外，其它支付方式不能大于应收应付金额", 111);
                    return;
                }
            }
        }

        private void txtPay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtCharge.Focus();
            }
        }

        protected void txtPay_Leave(object sender, EventArgs e)
        {
            if (this.txtPay.Text == "") this.txtPay.Text = "0.00";

            if (decimal.Parse(this.txtPay.Text) == 0)
            {
                this.txtCharge.Text = "0.00";
                return;
            }
            if (decimal.Parse(this.txtPay.Text) < 0)
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("实付金额应大于零!", 111);
                return;
            }
            //实收
            decimal RealPayCost = 0m;
            //ShouldPay = decimal.Parse(this.txtShouldPay.Text);
            //应收只计算现金部分，其他的不计算
            decimal Cash = 0m;//现金
            for (int i = 0; i < this.fpPayType_Sheet1.Rows.Count; i++)
            {
                if (this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayWay].Text.Trim() == "现金")
                {
                    Cash = decimal.Parse(this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayCost].Value.ToString());
                    break;
                }
            }
            RealPayCost = decimal.Parse(this.fpPayType_Sheet1.Cells[this.rowCA, (int)ColumnPayNew.PayCost].Value.ToString());
            //找零
            decimal charge = 0m;
            charge = RealPayCost - Cash;
            this.txtCharge.Text = charge.ToString();
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            this.isOK = false;
            this.Close();
        }

        void btnOK_Click(object sender, EventArgs e)
        {
            if (this.Save() > 0)
            {
                this.isOK = true;
                this.Close();
            }
        }

        void btnReCompute_Click(object sender, EventArgs e)
        {
            this.ComputeCost();
        }

        void txtCharge_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.btnOK.Focus();
            }
        }

        #endregion

        #region 重载

        protected override void OnLoad(EventArgs e)
        {
            this.Init();
            base.OnLoad(e);
        }

        #endregion

        private void fpPayType_EditChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {

        }

        private void SetCostValue(int column)
        {
            if (column == (int)ColumnPayNew.PayCost)
            {
                try
                {
                    if (FS.FrameWork.Function.NConvert.ToDecimal(this.fpPayType_Sheet1.Cells[this.fpPayType_Sheet1.ActiveRowIndex, (int)ColumnPayNew.PayCost].Value) > 0)
                    {
                        return;
                    }

                    decimal CAcost = 0;
                    for (int i = 0; i < this.fpPayType_Sheet1.RowCount; i++)
                    {
                        try
                        {
                            if (this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayWay].Text != string.Empty)
                            {
                                if (this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayWay].Text == "现金")
                                {
                                    CAcost += FS.FrameWork.Function.NConvert.ToDecimal(this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayCost].Value);
                                    this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayCost].Value = 0;
                                }
                            }
                        }
                        catch
                        {
                        }

                    }

                    if (CAcost > 0)
                    {
                        if (this.currentAccountDetail == null)// {970D1FA7-19B2-4fad-992E-922156E3F10D}
                        {
                            if (this.fpPayType_Sheet1.ActiveRowIndex == this.rowACD || this.fpPayType_Sheet1.ActiveRowIndex == this.rowACY)
                            {
                                this.fpPayType_Sheet1.Cells[this.rowCA, (int)ColumnPayNew.PayCost].Value = CAcost;

                            }
                            else
                            {
                                this.fpPayType_Sheet1.Cells[this.fpPayType_Sheet1.ActiveRowIndex, (int)ColumnPayNew.PayCost].Value = CAcost;
                            }

                        }
                        else
                        {
                            this.fpPayType_Sheet1.Cells[this.fpPayType_Sheet1.ActiveRowIndex, (int)ColumnPayNew.PayCost].Value = CAcost;

                        }
                            this.SetFP();
                    }
                }
                catch
                {
                }
            }
        }

        private void fpPayType_EnterCell(object sender, FarPoint.Win.Spread.EnterCellEventArgs e)
        {
            this.SetCostValue(e.Column);
        }

        /// <summary>
        /// 获得支付方式的集合
        /// </summary>
        /// <returns>成功 支付方式的集合 失败 null</returns>
        private List<FS.HISFC.Models.Fee.Inpatient.BalancePay> QueryBalancePays()
        {
            List<FS.HISFC.Models.Fee.Inpatient.BalancePay> balancePays = new List<FS.HISFC.Models.Fee.Inpatient.BalancePay>();
            FS.HISFC.Models.Fee.Inpatient.BalancePay balancePay = null;

            for (int i = 0; i < this.fpPayType_Sheet1.RowCount; i++)
            {
                if (this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayWay].Text == string.Empty)
                {
                    continue;
                }
                if (this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayCost].Text == string.Empty)
                {
                    continue;
                }
                if (FS.FrameWork.Function.NConvert.ToDecimal
                    (this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayCost].Value) == 0)
                {
                    continue;
                }
                balancePay = new FS.HISFC.Models.Fee.Inpatient.BalancePay();

                balancePay.PayType.Name = this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayWay].Text;
                balancePay.PayType.ID = payWayHelper.GetID(balancePay.PayType.Name);
                if (balancePay.PayType.ID == null || balancePay.PayType.ID.ToString() == string.Empty)
                {
                    return null;
                }
                balancePay.FT.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayCost].Value.ToString());
                if (balancePay.PayType.Name == "现金")
                {
                    balancePay.FT.RealCost = balancePay.FT.TotCost;//暂时不四舍五入
                }
                else
                {
                    balancePay.FT.RealCost = balancePay.FT.TotCost;
                }
                balancePay.Bank.Name = this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayBank].Text;
                balancePay.Bank.ID = bankHelper.GetID(balancePay.Bank.Name);
                balancePay.Bank.Account = this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayAccountNO].Text;
                if (balancePay.PayType.Name == "支票" || balancePay.PayType.Name == "汇票")
                {
                    balancePay.Bank.InvoiceNO = this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayTranscationNO].Text;
                }
                else
                {
                    balancePay.POSNO = this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayTranscationNO].Text;
                }
                if (balancePay.PayType.ID.ToString() == "CA")
                {
                    balancePay.FT.RealCost = balancePay.FT.RealCost;//暂时不四舍五入
                }
                else
                {
                    balancePay.FT.RealCost = balancePay.FT.RealCost;
                }
                //1补收；2返还
                if (this.fpPayType_Sheet1.RowHeader.Rows[i].Label == "退")
                {
                    balancePay.RetrunOrSupplyFlag = "2";
                }
                else
                {
                    balancePay.RetrunOrSupplyFlag = "1";
                }
                balancePay.TransKind.ID = "1";
                balancePay.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                balancePay.Qty = 1;
                //if (balancePay.PayType.ID.ToString() == "YS" || balancePay.PayType.ID.ToString() == "DC")//代付
                //{
                //    balancePay.IsEmpPay = this.ucPayingAgentMessControl.IsPayForAnother;
                //    balancePay.EmpowerPatient = this.ucPayingAgentMessControl.EmpowerPatient;
                //    balancePay.AccountTypeCode = this.ucPayingAgentMessControl.AccountTypeCode;
                //}
                if (balancePay.PayType.ID == "YS" || balancePay.PayType.ID == "DC")
                {
                    balancePay.IsEmpPay = false;//暂时没有代付
                    balancePay.AccountNo = this.currentAccountInfo.ID;
                    balancePay.AccountTypeCode = this.cmbAccountType.Tag.ToString();
                }

                balancePays.Add(balancePay);
            }

            return balancePays;
        }

        /// <summary>
        /// 设置位置
        /// </summary>
        private void SetLocation()
        {
            if (this.fpPayType_Sheet1.ActiveColumnIndex == (int)ColumnPayNew.PayWay)
            {
                Control cell = this.fpPayType.EditingControl;
                fpPayType.Location = new Point(this.fpPayType.Location.X + cell.Location.X + 4, 
                this.neuGroupBox1.Location.Y + this.fpPayType.Location.Y + cell.Location.Y + cell.Height * 2 + SystemInformation.Border3DSize.Height * 2);
                fpPayType.Size = new Size(cell.Width + 50 + SystemInformation.Border3DSize.Width * 2, 150);
                if (fpPayType.Location.Y + fpPayType.Height > this.fpPayType.Location.Y + this.fpPayType.Height)
                {
                    fpPayType.Location = new Point(this.fpPayType.Location.X + cell.Location.X + 4,
                        this.neuGroupBox1.Location.Y + this.fpPayType.Location.Y + cell.Location.Y + cell.Height * 2 + SystemInformation.Border3DSize.Height * 2
                        - fpPayType.Size.Height - cell.Height);
                }
            }
        }

        private void SetFP()
        {
            for (int i = 0; i < this.fpPayType_Sheet1.RowCount; i++)
            {
                if (!string.IsNullOrEmpty(this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayCost].Text))
                {
                    if (FS.FrameWork.Function.NConvert.ToDecimal(this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayCost].Value.ToString()) > 0)
                    {
                        this.fpPayType_Sheet1.RowHeader.Rows[i].Label = "收";
                        this.fpPayType_Sheet1.RowHeader.Rows[i].BackColor = Color.Red;
                    }
                    else if (FS.FrameWork.Function.NConvert.ToDecimal(this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayCost].Value.ToString()) < 0)
                    {
                        this.fpPayType_Sheet1.RowHeader.Rows[i].Label = "欠";
                        this.fpPayType_Sheet1.RowHeader.Rows[i].BackColor = Color.Red;
                    }
                    else
                    {
                        this.fpPayType_Sheet1.RowHeader.Rows[i].Label = "";
                        this.fpPayType_Sheet1.RowHeader.Rows[i].BackColor = System.Drawing.Color.Gainsboro;
                    }
                }
                else
                {

                    this.fpPayType_Sheet1.RowHeader.Rows[i].Label = "";
                    this.fpPayType_Sheet1.RowHeader.Rows[i].BackColor = System.Drawing.Color.Gainsboro;
                }
            }
        }
        private void fpPayType_EditModeOn(object sender, EventArgs e)
        {

            SetLocation();
            if (fpPayType_Sheet1.ActiveColumnIndex == (int)ColumnPayNew.PayCost)
            {
                #region MyRegion

                string tempString = this.fpPayType_Sheet1.Cells[fpPayType_Sheet1.ActiveRowIndex, (int)ColumnPayNew.PayWay].Text;

                if (tempString == string.Empty)
                {
                    for (int i = 0; i < this.fpPayType_Sheet1.Columns.Count; i++)
                    {
                        this.fpPayType_Sheet1.Cells[fpPayType_Sheet1.ActiveRowIndex, i].Text = string.Empty;
                    }
                }

                bool isOnlyCash = true;
                decimal nowCost = 0;
                //不存在支付方式金额不等再进行结算问题 {0F169460-7FF9-4b76-A22E-C2D0A1DCD438}支付方式金额自动分配、结算判断金额相等
                bool isReturnZero = false;

                for (int i = 0; i < this.fpPayType_Sheet1.RowCount; i++)
                {
                    if (this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayWay].Text != string.Empty)
                    {
                        if (this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayWay].Text != "现金" &&  //!this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Locked &&
                              FS.FrameWork.Function.NConvert.ToDecimal(this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayCost].Value) > 0
                            )
                        {
                            isOnlyCash = false;
                            nowCost += FS.FrameWork.Function.NConvert.ToDecimal(this.fpPayType_Sheet1.Cells[i, (int)ColumnPayNew.PayCost].Value);
                        }
                    }
                }
                if (isOnlyCash)
                {
                    this.txtPay.Text = FS.FrameWork.Public.String.FormatNumber(shouldPay, 2).ToString();
                    this.fpPayType_Sheet1.Cells[this.rowCA, (int)ColumnPayNew.PayCost].Text = shouldPay.ToString(); 
                    this.SetFP();

                }
                else
                {
                    if (shouldPay - nowCost < 0)
                    {
                        this.fpPayType_Sheet1.Cells[this.fpPayType_Sheet1.ActiveRowIndex, (int)ColumnPayNew.PayCost].Value = 0;
                        this.fpPayType_Sheet1.SetActiveCell(this.fpPayType_Sheet1.ActiveRowIndex, (int)ColumnPayNew.PayCost, false);

                        nowCost = 0;
                    }
                    else
                    {

                        this.txtPay.Text = FS.FrameWork.Public.String.FormatNumber(shouldPay, 2).ToString();
                        this.fpPayType_Sheet1.Cells[this.rowCA, (int)ColumnPayNew.PayCost].Value = shouldPay - nowCost;
                        this.SetFP();
                    }
                }


                #endregion
            }
        }

        private void cmbAccountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // {970D1FA7-19B2-4fad-992E-922156E3F10D}
            string mess = "基本账户余额：{0}   赠送账户余额：{1} ";
            accountDetailList = this.accountManager.GetAccountDetail(this.currentAccountInfo.ID, this.cmbAccountType.Tag.ToString(),"1");
            if (accountDetailList.Count > 0)
            {
                currentAccountDetail = accountDetailList[0] as FS.HISFC.Models.Account.AccountDetail;
                mess = string.Format(mess, currentAccountDetail.BaseVacancy.ToString("F2"), currentAccountDetail.DonateVacancy.ToString("F2"));
            }
            else
            {
                currentAccountDetail = null;
                mess = string.Format(mess, "0.00", "0.00");
            }
            this.txtVacancyShow.Text = mess;
            this.ComputeCost();
        }
    }


    /// <summary>
    /// 支付方式列表
    /// </summary>
    enum ColumnPayNew
    {
        PayWay,
        PayCost,
        PayBank,
        PayAccountNO,
        PayCorporation,
        PayTranscationNO
    }
}
