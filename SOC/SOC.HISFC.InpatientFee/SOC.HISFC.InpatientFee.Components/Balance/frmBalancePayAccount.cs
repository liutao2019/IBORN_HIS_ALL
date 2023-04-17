using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models;
using FS.HISFC.Models.Base;
using FS.HISFC.Models.MedicalPackage.Fee;
using FS.HISFC.Models.Fee.Inpatient;
using FS.FrameWork.Models;
using FS.FrameWork.Function;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.HISFC.InpatientFee.Components.Balance
{
    /// <summary>
    /// [功能描述: 结算支付方式选择- 增加了会员结算]<br></br>
    /// [创 建 者: gumzh]<br></br>
    /// [创建时间: 2017-10]<br></br>
    /// </summary>
    public partial class frmBalancePayAccount : FS.FrameWork.WinForms.Forms.BaseForm
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public frmBalancePayAccount()
        {
            InitializeComponent();

            //添加事件
            this.AddEvent();
        }

        #region 变量

        /// <summary>
        /// 支付方式帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper payWayHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 银行帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper bankHelper = new FS.FrameWork.Public.ObjectHelper();

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

        /// <summary>
        /// 现金支付方式
        /// </summary>
        private int rowCA = -1;

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

        #region 特殊支付方式

        /// <summary>
        /// 优惠支付方式
        /// </summary>
        private NeuObject rcObj = null;

        /// <summary>
        /// 账户-基本账户支付方式
        /// </summary>
        private NeuObject ycObj = null;

        /// <summary>
        /// 账户-赠送账户支付方式
        /// </summary>
        private NeuObject dcObj = null;

        /// <summary>
        /// 套餐实收支付方式
        /// </summary>
        private NeuObject prObj = null;

        /// <summary>
        /// 套餐赠送支付方式
        /// </summary>
        private NeuObject pdObj = null;

        /// <summary>
        /// 套餐优惠支付方式
        /// </summary>
        private NeuObject pyObj = null;

        /// <summary>
        /// 购物卡支付方式
        /// </summary>
        private NeuObject dsObj = null;
        #endregion


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

        /// <summary>
        /// 在FP选择支付方式金额
        /// </summary>
        private decimal payModeFpCostShould;

        /// <summary>
        /// {FAE56BB8-F958-411f-9663-CC359D6D494B}
        /// 积分模块是否启用
        /// </summary>
        public bool IsCouponModuleInUse = false;

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

            this.ComputeCost();

            //初始化积分
            RefreshCoupon();
        }

       　/// <summary>
        /// {dd227cc2-3277-40c4-6872-a60abfa4e957}
       /// </summary>
        private void RefreshCoupon()
        {
            if (string.IsNullOrEmpty(this.PatientInfo.PID.CardNO))
            {
                return;
            }

            decimal couponAmount = 0.0m;

            string resultCode = "0";
            string errorMsg = "";
            Dictionary<string, string> dic = FS.HISFC.BizProcess.Integrate.WSHelper.QueryAccountInfo(this.PatientInfo.PID.CardNO, out resultCode, out errorMsg);

            if (dic == null)
            {
                MessageBox.Show("查询账户出错:" + errorMsg);
                return;
            }

            if (dic.ContainsKey("couponvacancy"))
            {
                couponAmount = decimal.Parse(dic["couponvacancy"].ToString());
            }
            else
            {
                MessageBox.Show("查询账户余额出错！");
                return;
            }

            this.sycoupon.Text = couponAmount.ToString("F2");
        }



        /// <summary>
        /// 添加事件
        /// </summary>
        private void AddEvent()
        {
            this.txtPay.KeyDown += new KeyEventHandler(txtPay_KeyDown);
            this.txtPay.Leave += new EventHandler(txtPay_Leave);
            this.txtCharge.KeyDown += new KeyEventHandler(txtCharge_KeyDown);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            this.btnOK.Click += new EventHandler(btnOK_Click);
            this.fpPayType_Sheet1.CellChanged += new FarPoint.Win.Spread.SheetViewEventHandler(fpPayType_Sheet1_CellChanged);
            this.fpPayType.EditModeOn += new System.EventHandler(this.fpPayType_EditModeOn);
            this.fpPayType.EnterCell += new FarPoint.Win.Spread.EnterCellEventHandler(this.fpPayType_EnterCell);

            this.btnReCompute.Click += new EventHandler(btnReCompute_Click);

            this.btAccount.Click += new System.EventHandler(this.btAccount_Click);
            this.btEmpower.Click += new System.EventHandler(this.btEmpower_Click);
            this.btPackage.Click += new System.EventHandler(this.btPackage_Click);

            this.btCoupon.Click += new EventHandler(btCoupon_Click);
            this.btnDiscountCard.Click += new System.EventHandler(this.btDiscountCard_Click);
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
            this.fpPayType_Sheet1.Columns[(int)ColumnPayAccount.PayBank].CellType = banktype;
        }

        /// <summary>
        /// 支付方式初始化
        /// </summary>
        private void InitPayMode()
        {

            ArrayList al = CommonController.CreateInstance().QueryConstant(FS.HISFC.Models.Base.EnumConstant.PAYMODES);
            if (al == null || al.Count <= 0)
            {
                CommonController.CreateInstance().MessageBox("获取支付方式失败", MessageBoxIcon.Warning);
                return;
            }
            this.payWayHelper.ArrayObject = al;

            //HIS中维护的支付方式
            for (int i = 0; i < al.Count; i++)
            {
                NeuObject obj = al[i] as NeuObject;

                //现金支付
                if (obj.ID == "CA")
                {
                    this.rowCA = i;
                }

                //ACY(帐户支付)
                if ((obj as FS.HISFC.Models.Base.Const).UserCode == "ACY")
                {
                    this.ycObj = obj;

                    this.rowACY = i;
                }

                //ACD(帐户赠送)
                if ((obj as FS.HISFC.Models.Base.Const).UserCode == "ACD")
                {
                    this.dcObj = obj;

                    this.rowACD = i;
                }

                //ECO(优惠)
                if ((obj as FS.HISFC.Models.Base.Const).UserCode == "ECO")
                {
                    this.rcObj = obj;

                    this.rowECO = i;
                }

                //PR(套餐实收支付方式)
                if ((obj as FS.HISFC.Models.Base.Const).UserCode == "PR")
                {
                    this.prObj = obj;
                }

                //PD(套餐赠送支付方式)
                if ((obj as FS.HISFC.Models.Base.Const).UserCode == "PD")
                {
                    this.pdObj = obj;
                }

                //PY(套餐优惠支付方式)
                if ((obj as FS.HISFC.Models.Base.Const).UserCode == "PY")
                {
                    this.pyObj = obj;
                }

                //DS(购物卡支付方式)
                if ((obj as FS.HISFC.Models.Base.Const).UserCode == "DS")
                {
                    this.dsObj = obj;
                }
            }
            if (this.rowCA < 0)
            {
                MessageBox.Show("请联系信息科维护现金支付方式CA!");
            }

            this.fpPayType_Sheet1.Rows.Count = 0;  //清空
            for (int i = 0; i < al.Count; i++)
            {
                FS.FrameWork.Models.NeuObject obj = al[i] as FS.FrameWork.Models.NeuObject;
                //ACY(帐户支付)；ACD(帐户赠送)；ECO(优惠)；PR(套餐实收)；PD(套餐赠送)；PY(套餐优惠)；
                if ((obj as FS.HISFC.Models.Base.Const).UserCode == "ACY" ||
                    (obj as FS.HISFC.Models.Base.Const).UserCode == "ACD" ||
                    (obj as FS.HISFC.Models.Base.Const).UserCode == "ECO" ||
                    (obj as FS.HISFC.Models.Base.Const).UserCode == "PR" ||
                    (obj as FS.HISFC.Models.Base.Const).UserCode == "PD" ||
                    (obj as FS.HISFC.Models.Base.Const).UserCode == "PY" ||
                    (obj as FS.HISFC.Models.Base.Const).UserCode == "DS" ||
                    !NConvert.ToBoolean((obj as FS.HISFC.Models.Base.Const).Memo))

                {
                    continue;
                }

                //增加1行
                this.fpPayType_Sheet1.Rows.Count++;
                int rowIndex = this.fpPayType_Sheet1.Rows.Count - 1;

                this.fpPayType_Sheet1.Cells[rowIndex, (int)ColumnPayAccount.PayWay].Text = obj.Name;   //支付方式名字
                this.fpPayType_Sheet1.Cells[rowIndex, (int)ColumnPayAccount.PayWay].Tag = obj.ID;     //支付方式编码
                this.fpPayType_Sheet1.Cells[rowIndex, (int)ColumnPayAccount.PayWay].Locked = true;

                if (obj.ID == "CO" || obj.ID == "TCO" || obj.ID == "ECO")
                {
                    this.fpPayType_Sheet1.Rows[rowIndex].Locked = true;
                }
            }
        }

        /// <summary>
        /// 计算金额
        /// </summary>
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
                this.fpPayType_Sheet1.Cells[0, (int)ColumnPayAccount.PayCost].Value = shouldPay;

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

                this.fpPayType_Sheet1.Cells[this.rowCA , (int)ColumnPay.PayCost].Value = shouldPay;
                                  

                this.txtPay.Text = shouldPay.ToString("F2");

                //设置金额
                this.SetCost();
            }
            else if (returnPay > 0)
            {
                this.lblShouldPay.Text = "应退金额";
                this.txtShouldPay.Text = (returnPay).ToString("F2");
                this.lblShouldPay.ForeColor = Color.Blue;
                this.txtShouldPay.ForeColor = Color.Blue;

                this.fpPayType_Sheet1.RowHeader.Rows[0].Label = "退";
                this.fpPayType_Sheet1.RowHeader.Rows[0].BackColor = Color.Blue;
                this.fpPayType_Sheet1.Cells[0, (int)ColumnPayAccount.PayCost].Value = returnPay;

                this.txtPay.Text = returnPay.ToString("F2");
            }
            //this.fpPayType_Sheet1.Cells[0, (int)ColumnPayAccount.PayWay].Text = "现金";

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
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPayAccount.PayWay].Text = "社会保障卡(中山)";
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPayAccount.PayCost].Value = this.BalancePayCost;

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
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPayAccount.PayWay].Text = "社会保障卡(中山)";
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPayAccount.PayCost].Value = this.BalancePayCost;

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
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPayAccount.PayWay].Text = "社会保障卡(中山)";
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPayAccount.PayCost].Value = this.BalancePayCost;

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
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPayAccount.PayWay].Text = "优惠";
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPayAccount.PayCost].Value = this.BalanceMZCost;

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
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPayAccount.PayWay].Text = "优惠";
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPayAccount.PayCost].Value = this.BalanceMZCost;

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
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPayAccount.PayWay].Text = "优惠";
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPayAccount.PayCost].Value = this.BalanceMZCost;

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
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPayAccount.PayWay].Text = "社会保障卡(中山)";
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPayAccount.PayCost].Value = this.BalancePayCost;
                    this.fpPayType_Sheet1.CellChanged += new FarPoint.Win.Spread.SheetViewEventHandler(fpPayType_Sheet1_CellChanged);

                    this.fpPayType_Sheet1.RowHeader.Rows[1].Label = "收";
                    this.fpPayType_Sheet1.RowHeader.Rows[1].BackColor = Color.Red;
                    this.fpPayType_Sheet1.Cells[1, (int)ColumnPayAccount.PayWay].Text = "优惠";
                    this.fpPayType_Sheet1.Cells[1, (int)ColumnPayAccount.PayCost].Value = this.BalanceMZCost;

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
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPayAccount.PayWay].Text = "社会保障卡(中山)";
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPayAccount.PayCost].Value = this.BalancePayCost;
                    this.fpPayType_Sheet1.CellChanged += new FarPoint.Win.Spread.SheetViewEventHandler(fpPayType_Sheet1_CellChanged);

                    this.fpPayType_Sheet1.RowHeader.Rows[1].Label = "收";
                    this.fpPayType_Sheet1.RowHeader.Rows[1].BackColor = Color.Red;
                    this.fpPayType_Sheet1.Cells[1, (int)ColumnPayAccount.PayWay].Text = "优惠";
                    this.fpPayType_Sheet1.Cells[1, (int)ColumnPayAccount.PayCost].Value = this.BalanceMZCost;

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
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPayAccount.PayWay].Text = "社会保障卡(中山)";
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPayAccount.PayCost].Value = this.BalancePayCost;
                    this.fpPayType_Sheet1.CellChanged += new FarPoint.Win.Spread.SheetViewEventHandler(fpPayType_Sheet1_CellChanged);

                    this.fpPayType_Sheet1.RowHeader.Rows[1].Label = "收";
                    this.fpPayType_Sheet1.RowHeader.Rows[1].BackColor = Color.Red;
                    this.fpPayType_Sheet1.Cells[1, (int)ColumnPayAccount.PayWay].Text = "优惠";
                    this.fpPayType_Sheet1.Cells[1, (int)ColumnPayAccount.PayCost].Value = this.BalanceMZCost;

                    this.txtPay.Text = returnPay.ToString("F2");
                }

                #endregion
            }

            this.txtPay.Focus();
            this.txtPay.SelectAll();
        }

        /// <summary>
        /// 获取FP支付方式的金额
        /// </summary>
        /// <returns></returns>
        private decimal GetPayModeFpCost()
        {
            //1、会员支付
            decimal accountCost = NConvert.ToDecimal(this.tbAccountCost.Text);

            //2、会员代付
            decimal empowerCost = NConvert.ToDecimal(this.tbEmpowerCost.Text);

            //3、套餐支付
            decimal packageCost = NConvert.ToDecimal(this.tbPackageCost.Text);

            //4、积分支付
            decimal couponCost = NConvert.ToDecimal(this.tbCouponCost.Text);

            //5、购物卡支付
            decimal dicountCost = NConvert.ToDecimal(this.tbDiscountCost.Text);

            //6、FP的支付方式选择
            this.payModeFpCostShould = this.shouldPay - accountCost - empowerCost - packageCost - couponCost - dicountCost;

            return this.payModeFpCostShould;
        }

        /// <summary>
        /// 清屏
        /// </summary>
        public void Clear()
        {
            this.fpPayType_Sheet1.CellChanged -= new FarPoint.Win.Spread.SheetViewEventHandler(fpPayType_Sheet1_CellChanged);

            this.listBalancePay.Clear();
            for (int i = 0; i < this.fpPayType_Sheet1.RowCount; i++)
            {
                //this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayWay].Text = string.Empty;   //支付方式名字
                this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayCost].Value = 0M;
                this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayBank].Text = string.Empty;
                this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayCorporation].Text = string.Empty;
                this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayAccountNO].Text = string.Empty;
                this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayTranscationNO].Text = string.Empty;

                this.fpPayType_Sheet1.RowHeader.Rows[i].Label = (i + 1).ToString();  //行号
                this.fpPayType_Sheet1.RowHeader.Rows[i].BackColor = Color.White;
            }
            this.lblShouldPay.Text = "应收(自费)";
            this.txtShouldPay.Text = "0.00";
            this.lblShouldPay.ForeColor = Color.Red;
            this.txtShouldPay.ForeColor = Color.Red;
            this.txtPay.Text = "0.00";
            this.txtCharge.Text = "0.00";
            this.txtCharge.ReadOnly = true;
            this.txtCharge.AllowNegative = true;
            this.isOK = false;

            //会员支付
            this.tbAccountCost.Text = "0.00";
            this.tbAccountCost.Tag = null;
            this.lblAccountInfo.Text = string.Empty;

            //会员代付
            this.tbEmpowerCost.Text = "0.00";
            this.tbEmpowerCost.Tag = null;
            this.lblEmpwoerInfo.Text = string.Empty;

            //套餐支付
            this.tbPackageCost.Text = "0.00";
            this.tbPackageCost.Tag = null;
            this.lblPackageInfo.Text = string.Empty;
            this.lblPackageInfo.Tag = null;

            //积分支付
            this.tbCouponCost.Text = "0.00";
            this.tbCouponCost.Tag = null;
            this.lblCoupon.Text = string.Empty;

            //购物卡支付
            this.tbDiscountCost.Text = "0.00";
            this.tbDiscountCost.Tag = null;
            this.lblDisCount.Text = string.Empty;

            this.fpPayType_Sheet1.CellChanged += new FarPoint.Win.Spread.SheetViewEventHandler(fpPayType_Sheet1_CellChanged);

        }

        private bool ValidBalancePay(ref string errText)
        {
            decimal payTypeCost = 0m;
            string payType = "";
            decimal payAllCost = 0m;

            for (int i = 0; i < this.fpPayType_Sheet1.Rows.Count; i++)
            {
                payType = this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayWay].Text;
                if (this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayCost].Value == null)
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

                    payTypeCost = decimal.Parse(this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayCost].Value.ToString()) * sign;
                }

                #region 屏蔽 by ma_d
                //if (payType.Trim() != "" && payTypeCost > 0)
                //{
                //    if (payType.Trim() == "支票" || payType.Trim() == "汇票")
                //    {
                //        if (this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayBank].Text.Trim() == "" ||
                //            this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayAccountNO].Text.Trim() == "" ||
                //            this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayCorporation].Text.Trim() == "" ||
                //            this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayTranscationNO].Text.Trim() == "")
                //        {
                //            errText = "支票汇票必须将完整的开户银行,帐户,开据单位,票号补充完整!";
                //            return false;
                //        }
                //    }
                //    if (payType.Trim() == "借记卡" || payType.Trim() == "信用卡")
                //    {
                //        if (this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayBank].Text.Trim() == "" ||
                //            this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayAccountNO].Text.Trim() == "" ||
                //            this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayTranscationNO].Text.Trim() == "")
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
                string paytype = this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayWay].Text.Trim();
                if (paytype != "")
                {
                    for (int j = i + 1; j < this.fpPayType_Sheet1.RowCount; j++)
                    {
                        if (paytype == this.fpPayType_Sheet1.Cells[j, (int)ColumnPayAccount.PayWay].Text.Trim())
                        {
                            errText = "不能同时使用两种相同的支付方式";
                            return false;
                        }
                    }
                }
                else
                {
                    if (FS.FrameWork.Function.NConvert.ToDecimal(this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayCost].Value) > 0)
                    {
                        errText = "支付方式不能为空";
                        return false;
                    }
                }
            }

            //如果是欠费
            if (arrearBalance > 0)
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
                    this.payModeFpCostShould = this.GetPayModeFpCost();
                    //if (this.shouldPay != payAllCost)
                    if (this.payModeFpCostShould != payAllCost)
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
            string errText = "";

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
            //    payType = this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayWay].Text;
            //    bankName = this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayBank].Text;
            //    if (this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayCost].Value == null)
            //    {
            //        payTypeCost = 0;
            //    }
            //    else
            //    {
            //        payTypeCost = FS.FrameWork.Function.NConvert.ToDecimal(this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayCost].Value.ToString());
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
            //        balancePay.Bank.Account = this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayAccountNO].Text;
            //        balancePay.Bank.WorkName = this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayCorporation].Text;
            //        balancePay.Bank.InvoiceNO = this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayTranscationNO].Text;

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
            if (listBalancePay==null)
            {
                
                return -1;
            }
            #region 判断输入密码

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
            if (false && needCheckPassWord)
            {
                FS.HISFC.BizProcess.Integrate.Fee feeMgr = new FS.HISFC.BizProcess.Integrate.Fee();
                if (!feeMgr.CheckAccountPassWord(this.PatientInfo))
                {
                    MessageBox.Show("账户支付失败！");
                    return -1;
                }
            }

            #endregion

            return 1;
        }

        /// <summary>
        /// 套餐消费支付方式
        /// </summary>
        /// <param name="costPackDetails"></param>
        /// <returns></returns>
        private int SetPackagePayMode(List<PackageDetail> costPackDetails)
        {
            try
            {
                //先清空
                this.tbPackageCost.Text = "0.00";
                this.tbPackageCost.Tag = null;
                this.lblPackageInfo.Tag = null;
                this.SetCost();

                if (costPackDetails == null || costPackDetails.Count <= 0)
                {
                    this.tbPackageCost.Text = "0.00";
                    this.tbPackageCost.Tag = null;
                    this.lblPackageInfo.Tag = null;
                    this.SetCost();

                    MessageBox.Show("请选择需要支付的套餐明细!", "警告");
                    return -1;
                }

                decimal prCost = 0;  //套餐实付金额
                decimal pdCost = 0;  //套餐赠送金额
                decimal pyCost = 0;  //套餐优惠支付
                foreach (PackageDetail pDetail in costPackDetails)
                {
                    if (pDetail.Real_Cost > 0)
                    {
                        prCost += pDetail.Real_Cost;
                    }
                    if (pDetail.Gift_cost > 0)
                    {
                        pdCost += pDetail.Gift_cost;
                    }
                    if (pDetail.Etc_cost > 0)
                    {
                        pyCost += pDetail.Etc_cost;
                    }
                }

                this.payModeFpCostShould = this.GetPayModeFpCost();
                if ((prCost + pdCost + pyCost) > this.payModeFpCostShould)
                {
                    this.tbPackageCost.Text = "0.00";
                    this.tbPackageCost.Tag = null;
                    this.lblPackageInfo.Tag = null;
                    this.SetCost();

                    MessageBox.Show(string.Format("套餐消费的金额【{0}】大于患者需要支付金额【{1}】", (prCost + pdCost + pyCost).ToString(), this.payModeFpCostShould.ToString(), "警告"));
                    return -1;
                }

                //套餐消费支付方式
                List<BalancePay> bpList = new List<BalancePay>();
                //套餐实付
                if (prCost > 0)
                {
                    BalancePay bp = new BalancePay();

                    bp.PayType.ID = this.prObj.ID;
                    bp.PayType.Name = this.prObj.Name;
                    bp.FT.TotCost = prCost;
                    bp.FT.RealCost = prCost;

                    bpList.Add(bp);
                }
                //套餐赠送
                if (pdCost > 0)
                {
                    BalancePay bp = new BalancePay();

                    bp.PayType.ID = this.pdObj.ID;
                    bp.PayType.Name = this.pdObj.Name;
                    bp.FT.TotCost = pdCost;
                    bp.FT.RealCost = pdCost;

                    bpList.Add(bp);
                }
                //套餐优惠
                if (pyCost > 0)
                {
                    BalancePay bp = new BalancePay();

                    bp.PayType.ID = this.pyObj.ID;
                    bp.PayType.Name = this.pyObj.Name;
                    bp.FT.TotCost = pyCost;
                    bp.FT.RealCost = pyCost;

                    bpList.Add(bp);
                }
                this.tbPackageCost.Tag = bpList;
                this.tbPackageCost.Text = (prCost + pdCost + pyCost).ToString("F2");
                this.lblPackageInfo.Tag = costPackDetails;

                this.SetCost();

                return 1;
            }
            catch (Exception ex)
            {
                this.tbPackageCost.Text = "0.00";
                this.tbPackageCost.Tag = null;
                this.lblPackageInfo.Tag = null;
                this.SetCost();

                MessageBox.Show(ex.Message, "警告");
                return -1;
            }
        }

        /// <summary>
        /// 设置支付方式金额
        /// </summary>
        /// <returns></returns>
        private int SetCost()
        {
            //1、会员支付
            decimal accountCost = NConvert.ToDecimal(this.tbAccountCost.Text);

            this.lblAccountInfo.Text = string.Empty;
            if (this.tbAccountCost.Tag != null && (this.tbAccountCost.Tag as List<BalancePay>) != null &&
                (this.tbAccountCost.Tag as List<BalancePay>).Count > 0)
            {
                List<BalancePay> accPayList = this.tbAccountCost.Tag as List<BalancePay>;

                decimal accPayCost = 0m;
                decimal accGiftCost = 0m;
                foreach (BalancePay bp in accPayList)
                {
                    if (bp.PayType.ID == "YS")
                    {
                        accPayCost += bp.FT.TotCost;
                    }
                    else if (bp.PayType.ID == "DC")
                    {
                        accGiftCost += bp.FT.TotCost;
                    }
                }
                this.lblAccountInfo.Text = string.Format("基本账户：{0}；赠送账户：{1}", accPayCost.ToString("F2"), accGiftCost.ToString("F2"));
            }

            //2、会员代付
            decimal empowerCost = NConvert.ToDecimal(this.tbEmpowerCost.Text);

            this.lblEmpwoerInfo.Text = string.Empty;
            if (this.tbEmpowerCost.Tag != null && (this.tbEmpowerCost.Tag as List<BalancePay>) != null &&
                (this.tbEmpowerCost.Tag as List<BalancePay>).Count > 0)
            {
                List<BalancePay> empowerPayList = this.tbEmpowerCost.Tag as List<BalancePay>;

                decimal empowerAccPayCost = 0m;
                decimal empowerAccGiftCost = 0m;
                foreach (BalancePay bp in empowerPayList)
                {
                    if (bp.PayType.ID == "YS")
                    {
                        empowerAccPayCost += bp.FT.TotCost;
                    }
                    else if (bp.PayType.ID == "DC")
                    {
                        empowerAccGiftCost += bp.FT.TotCost;
                    }
                }
                this.lblEmpwoerInfo.Text = string.Format("基本账户：{0}；赠送账户：{1}", empowerAccPayCost.ToString("F2"), empowerAccGiftCost.ToString("F2"));
            }

            //3、套餐支付
            decimal packageCost = NConvert.ToDecimal(this.tbPackageCost.Text);

            this.lblPackageInfo.Text = string.Empty;
            if (this.tbPackageCost.Tag != null && (this.tbPackageCost.Tag as List<BalancePay>) != null &&
                (this.tbPackageCost.Tag as List<BalancePay>).Count > 0)
            {
                List<BalancePay> packPayList = this.tbPackageCost.Tag as List<BalancePay>;

                decimal prCost = 0;
                decimal pdCost = 0;
                decimal pyCost = 0;

                foreach (BalancePay bp in packPayList)
                {
                    if (bp.PayType.ID == this.prObj.ID)
                    {
                        prCost += bp.FT.TotCost;
                    }
                    if (bp.PayType.ID == this.pdObj.ID)
                    {
                        pdCost += bp.FT.TotCost;
                    }
                    if (bp.PayType.ID == this.pyObj.ID)
                    {
                        pyCost += bp.FT.TotCost;
                    }
                }
                this.lblPackageInfo.Text = string.Format("套餐实付：{0}元；套餐赠送：{1}；套餐优惠：{2}", prCost.ToString("F2"), pdCost.ToString("F2"), pyCost.ToString("F2"));
            }

            //4、积分代付
            decimal couponCost = NConvert.ToDecimal(this.tbCouponCost.Text);

            this.lblCoupon.Text = string.Empty;
            if (this.tbCouponCost.Tag != null && (this.tbCouponCost.Tag as List<BalancePay>) != null &&
                (this.tbCouponCost.Tag as List<BalancePay>).Count > 0)
            {
                List<BalancePay> couponPayList = this.tbCouponCost.Tag as List<BalancePay>;

                decimal tbCouponCostPayCost = 0m;
                foreach (BalancePay bp in couponPayList)
                {
                    tbCouponCostPayCost += bp.FT.TotCost;
                }
                //{592F363C-D236-41d8-9CE8-C1782DAAC1A1}
                this.lblCoupon.Text = string.Format("积分支付：{0}", tbCouponCostPayCost.ToString());
            }

            //5、购物卡支付
            decimal discountCost = NConvert.ToDecimal(this.tbDiscountCost.Text);

            this.lblDisCount.Text = string.Empty;
            if (this.tbDiscountCost.Tag != null && (this.tbDiscountCost.Tag as List<BalancePay>) != null &&
                (this.tbDiscountCost.Tag as List<BalancePay>).Count > 0)
            {
                List<BalancePay> disPayList = this.tbDiscountCost.Tag as List<BalancePay>;

                decimal disPayCost = 0m;
                foreach (BalancePay bp in disPayList)
                {
                    if (bp.PayType.ID == "DS")
                    {
                        disPayCost += bp.FT.TotCost;
                    }
                }
                //购物卡提示信息
                this.lblDisCount.Text = "";
            }


            //6、FP的支付方式选择
            this.payModeFpCostShould = this.shouldPay - accountCost - empowerCost - packageCost - couponCost - discountCost;

            try
            {
                this.fpPayType_Sheet1.CellChanged -= new FarPoint.Win.Spread.SheetViewEventHandler(this.fpPayType_Sheet1_CellChanged);
                for (int k = 0; k < this.fpPayType_Sheet1.Rows.Count; k++)
                {
                    this.fpPayType_Sheet1.Cells[k, (int)ColumnPayAccount.PayCost].Text = string.Empty;
                    this.fpPayType_Sheet1.Cells[k, (int)ColumnPayAccount.PayWay].Locked = true;
                }
                this.fpPayType_Sheet1.CellChanged += new FarPoint.Win.Spread.SheetViewEventHandler(this.fpPayType_Sheet1_CellChanged);

                this.fpPayType_Sheet1.Cells[this.rowCA, (int)ColumnPayAccount.PayCost].Text = this.payModeFpCostShould.ToString();
                this.fpPayType_Sheet1.Cells[this.rowCA, (int)ColumnPayAccount.PayWay].Locked = true;
            }
            catch (Exception ex) { }

            //是指行头值
            this.SetFP();

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
            if (e.Column != (int)ColumnPayAccount.PayCost)
            {
                return;
            }

            string tempString = this.fpPayType_Sheet1.Cells[e.Row, (int)ColumnPayAccount.PayWay].Text;
            if (string.IsNullOrEmpty(tempString))
            {
                for (int i = 0; i < this.fpPayType_Sheet1.Columns.Count; i++)
                {
                    this.fpPayType_Sheet1.Cells[e.Row, i].Text = string.Empty;
                }
            }

            bool isOnlyCash = true;
            decimal nowCost = 0;
            bool isReturnZero = false;  //不存在支付方式金额不等再进行结算问题；支付方式金额自动分配、结算判断金额相等


            //非现金的支付方式
            for (int i = 0; i < this.fpPayType_Sheet1.RowCount; i++)
            {
                if (this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayWay].Text != string.Empty)
                {
                    if (this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayWay].Text != "现金" &&
                        NConvert.ToDecimal(this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayCost].Value) > 0)
                    {
                        isOnlyCash = false;
                        nowCost += NConvert.ToDecimal(this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayCost].Value);
                    }
                }
            }
            decimal shouldCost = (shouldPay > 0 ? this.GetPayModeFpCost() : decimal.Parse(this.txtShouldPay.Text));
            if (isOnlyCash)
            {
                this.fpPayType_Sheet1.Cells[this.rowCA, (int)ColumnPayAccount.PayCost].Text = shouldCost.ToString();
            }
            else
            {
                decimal temp = shouldCost;
                if (temp - nowCost < 0)
                {
                    this.fpPayType_Sheet1.Cells[e.Row, (int)ColumnPayAccount.PayCost].Value = 0;
                    this.fpPayType_Sheet1.SetActiveCell(e.Row, (int)ColumnPayAccount.PayCost, false);
                    nowCost = 0;

                    //是否进行过归零操作；支付方式金额自动分配、结算判断金额相等
                    isReturnZero = true;
                }
                this.fpPayType_Sheet1.Cells[this.rowCA, (int)ColumnPayAccount.PayCost].Value = shouldCost - nowCost;
            }

            this.SetFP();

            return;

            #region 废弃-gumzh

            ////金额判断
            //decimal PayTypeCost = 0m;
            //for (int i = 0; i < this.fpPayType_Sheet1.Rows.Count; i++)
            //{
            //    if (this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayCost].Value == null)
            //    {
            //        continue;
            //    }
            //    else
            //    {
            //        int sign = 1;
            //        if (arrearBalance > 0 && this.fpPayType_Sheet1.RowHeader.Rows[i].Label == "欠")
            //        {
            //            sign = 1;
            //        }
            //        else if (shouldPay > 0 && this.fpPayType_Sheet1.RowHeader.Rows[i].Label == "收")
            //        {
            //            sign = 1;
            //        }
            //        else if (returnPay > 0 && this.fpPayType_Sheet1.RowHeader.Rows[i].Label == "退")
            //        {
            //            sign = 1;
            //        }
            //        else
            //        {
            //            sign = -1;
            //        }

            //        PayTypeCost += decimal.Parse(this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayCost].Value.ToString()) * sign;
            //    }
            //}
            //decimal Spay = decimal.Parse(this.txtShouldPay.Text);
            //if (PayTypeCost == Spay)
            //{
            //    return;
            //}

            ////小于应收或应退金额
            //if (PayTypeCost < Spay)
            //{
            //    decimal Rest = Spay - PayTypeCost;
            //    for (int i = 0; i < this.fpPayType_Sheet1.Rows.Count; i++)
            //    {
            //        if (this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayWay].Value == null)
            //        {
            //            if (arrearBalance > 0)
            //            {
            //                this.fpPayType_Sheet1.RowHeader.Rows[i].Label = "欠";
            //                this.fpPayType_Sheet1.RowHeader.Rows[i].BackColor = Color.Red;
            //            }
            //            else if (shouldPay > 0)
            //            {
            //                this.fpPayType_Sheet1.RowHeader.Rows[i].Label = "收";
            //                this.fpPayType_Sheet1.RowHeader.Rows[i].BackColor = Color.Red;
            //            }
            //            else if (returnPay > 0)
            //            {
            //                this.fpPayType_Sheet1.RowHeader.Rows[i].Label = "退";
            //                this.fpPayType_Sheet1.RowHeader.Rows[i].BackColor = Color.Blue;
            //            }

            //            this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayWay].Value = "现金";
            //            this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayCost].Value = Rest;
            //            this.fpPayType.Focus();
            //            this.fpPayType_Sheet1.SetActiveCell(i, (int)ColumnPayAccount.PayWay);
            //            break;
            //        }
            //    }
            //    return;
            //}

            ////大于应收或应退金额
            //if (PayTypeCost > Spay)
            //{
            //    bool found = false;

            //    for (int i = 0; i < this.fpPayType_Sheet1.Rows.Count; i++)
            //    {
            //        if (false && this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayWay].Text.Trim() == "现金")
            //        {
            //            #region 废弃

            //            //decimal Cash = 0m;//现金
            //            //decimal Rest = 0m;//其他支付方式
            //            //Cash = decimal.Parse(this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayCost].Value.ToString());
            //            //Rest = PayTypeCost - Cash;
            //            //if (Rest > Spay)
            //            //{
            //            //    FS.FrameWork.WinForms.Classes.Function.Msg("现金为负值，如果不允许，请更改！", 111);
            //            //}

            //            //if (decimal.Parse(this.txtPay.Text) > Cash)
            //            //{
            //            //    //找零
            //            //    decimal RealPayCost = decimal.Parse(this.txtPay.Text);
            //            //    decimal charge = 0m;
            //            //    charge = RealPayCost - Cash;
            //            //    this.txtCharge.Text = charge.ToString();
            //            //}

            //            ////现金为其他支付方式与应收应付之间的差额
            //            //this.fpPayType_Sheet1.SetValue(i, (int)ColumnPayAccount.PayCost, Spay - Rest);
            //            //found = true;
            //            //break;

            //            #endregion
            //        }
            //        else if (this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayWay].Value == null)
            //        {
            //            found = true;
            //            decimal Rest = Spay - PayTypeCost;  //差异金额

            //            if (arrearBalance > 0)
            //            {
            //                this.fpPayType_Sheet1.RowHeader.Rows[i].Label = "退";
            //                this.fpPayType_Sheet1.RowHeader.Rows[i].BackColor = Color.Blue;
            //            }
            //            else if (shouldPay > 0)
            //            {
            //                this.fpPayType_Sheet1.RowHeader.Rows[i].Label = "退";
            //                this.fpPayType_Sheet1.RowHeader.Rows[i].BackColor = Color.Blue;
            //            }
            //            else if (returnPay > 0)
            //            {
            //                this.fpPayType_Sheet1.RowHeader.Rows[i].Label = "收";
            //                this.fpPayType_Sheet1.RowHeader.Rows[i].BackColor = Color.Red;
            //            }

            //            this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayWay].Value = "现金";
            //            this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayCost].Value = Math.Abs(Rest);
            //            this.fpPayType.Focus();
            //            this.fpPayType_Sheet1.SetActiveCell(i, (int)ColumnPayAccount.PayWay);
            //            break;
            //        }
            //    }

            //    if (!found)
            //    {
            //        FS.FrameWork.WinForms.Classes.Function.Msg("除现金外，其它支付方式不能大于应收应付金额", 111);
            //        return;
            //    }
            //}

            #endregion
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
                if (this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayWay].Text.Trim() == "现金")
                {
                    Cash = decimal.Parse(this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayCost].Value.ToString());
                    break;
                }
            }
            RealPayCost = decimal.Parse(this.fpPayType_Sheet1.Cells[this.rowCA, (int)ColumnPayAccount.PayCost].Value.ToString());
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

        /// <summary>
        /// 会员支付结算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btAccount_Click(object sender, EventArgs e)
        {
            //判断
            if (this.shouldPay <= 0)
            {
                MessageBox.Show("该患者需要退钱，不允许使用会员账户", "警告");
                return;
            }

            //判断是否存在病历号
            FS.HISFC.Models.RADT.PatientInfo patient = this.accountManager.GetPatientInfoByCardNO(this.PatientInfo.PID.CardNO);
            if (patient == null || string.IsNullOrEmpty(patient.PID.CardNO))
            {
                MessageBox.Show("该患者未进行基本信息登记，不允许使用会员支付", "警告");
                return;
            }

            //判断是否有会员
            FS.HISFC.Models.Account.Account account = this.accountManager.GetAccountByCardNoEX(this.PatientInfo.PID.CardNO);
            if (account == null || string.IsNullOrEmpty(account.ID))
            {
                MessageBox.Show("该患者无会员账户，不允许使用会员支付", "警告");
                return;
            }
            List<FS.HISFC.Models.Account.AccountDetail> accountList = this.accountManager.GetAccountDetail(account.ID, "ALL", "1");
            if (accountList == null || accountList.Count <= 0)
            {
                MessageBox.Show("该患者无会员账户，不允许使用会员支付", "警告");
                return;
            }

            //获取当前账户支付的金额
            List<BalancePay> accPayList = new List<BalancePay>();
            if (this.tbAccountCost.Tag != null && (this.tbAccountCost.Tag as List<BalancePay>) != null &&
                (this.tbAccountCost.Tag as List<BalancePay>).Count > 0)
            {
                accPayList.AddRange(this.tbAccountCost.Tag as List<BalancePay>);
            }

            decimal cost = 0;
            try
            {
                cost = NConvert.ToDecimal(this.tbAccountCost.Text);
            }
            catch
            {
                cost = 0;
            }

            //会员支付框
            frmAccountCost frmAccCost = new frmAccountCost();

            frmAccCost.IsEmpower = false;
            frmAccCost.SelftPatientInfo = patient;   //结算患者
            frmAccCost.PatientInfo = patient;       //支付患者
            frmAccCost.DeliverableCost = this.GetPayModeFpCost() + cost;

            string ErrInfo = string.Empty;
            if (frmAccCost.SetPayInfo(accPayList, ref ErrInfo) < 0)
            {
                MessageBox.Show(ErrInfo, "警告");
                return;
            }
            frmAccCost.SetPayModeRes += new DelegateHashtableSet(frmAccountCost_SetPayModeRes);
            frmAccCost.ShowDialog();
        }

        /// <summary>
        /// 会员代付结算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btEmpower_Click(object sender, EventArgs e)
        {
            //判断
            if (this.shouldPay <= 0)
            {
                MessageBox.Show("该患者需要退钱，不允许使用会员账户", "警告");
                return;
            }

            //判断是否存在病历号
            FS.HISFC.Models.RADT.PatientInfo selfPatient = this.accountManager.GetPatientInfoByCardNO(this.PatientInfo.PID.CardNO);
            if (selfPatient == null || string.IsNullOrEmpty(selfPatient.PID.CardNO))
            {
                MessageBox.Show("该患者未进行基本信息登记，不允许使用会员代付", "警告");
                return;
            }

            //获取会员代付的金额
            List<BalancePay> empowerPayList = new List<BalancePay>();
            if (this.tbEmpowerCost.Tag != null && (this.tbEmpowerCost.Tag as List<BalancePay>) != null &&
                (this.tbEmpowerCost.Tag as List<BalancePay>).Count > 0)
            {
                empowerPayList.AddRange(this.tbEmpowerCost.Tag as List<BalancePay>);
            }

            FS.HISFC.Models.RADT.PatientInfo empowerPatient = null;
            if (empowerPayList != null && empowerPayList.Count > 0)
            {
                BalancePay emBp = empowerPayList[0] as BalancePay;
                List<FS.HISFC.Models.Account.AccountDetail> accDetails = this.accountManager.GetAccountDetail(emBp.AccountNo, "ALL", "1");
                if (accDetails != null && accDetails.Count > 0)
                {
                    empowerPatient = this.accountManager.GetPatientInfoByCardNO(accDetails[0].CardNO);
                    if (empowerPatient == null || string.IsNullOrEmpty(empowerPatient.PID.CardNO))
                    {
                        MessageBox.Show("查询患者的会员代付信息失败!", "警告");
                        return;
                    }
                }
            }

            decimal cost = 0;
            try
            {
                cost = NConvert.ToDecimal(this.tbEmpowerCost.Text);
            }
            catch
            {
                cost = 0;
            }

            //代付支付框
            frmAccountCost frmEmpowerCost = new frmAccountCost();

            frmEmpowerCost.IsEmpower = true;
            frmEmpowerCost.SelftPatientInfo = selfPatient;   //结算患者
            frmEmpowerCost.PatientInfo = empowerPatient;     //支付患者
            frmEmpowerCost.DeliverableCost = this.GetPayModeFpCost() + cost;

            string ErrInfo = string.Empty;
            if (frmEmpowerCost.SetPayInfo(empowerPayList, ref ErrInfo) < 0)
            {
                MessageBox.Show(ErrInfo, "警告");
                return;
            }
            frmEmpowerCost.SetPayModeRes += new DelegateHashtableSet(frmEmpowerCost_SetPayModeRes);
            frmEmpowerCost.ShowDialog();

        }

        /// <summary>
        /// 套餐结算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btPackage_Click(object sender, EventArgs e)
        {
            MessageBox.Show("请使用【套餐结算】功能!");

        }

        /// <summary>
        /// 积分支付
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btCoupon_Click(object sender, EventArgs e)
        {
            if (!IsCouponModuleInUse)
            {
                MessageBox.Show("积分模块未启用，不能使用积分", "提示");
                return;
            }
            
            //判断
            if (this.shouldPay <= 0)
            {
                MessageBox.Show("该患者需要退钱，不允许使用积分", "警告");
                return;
            }

            //判断是否存在病历号
            FS.HISFC.Models.RADT.PatientInfo patient = this.accountManager.GetPatientInfoByCardNO(this.PatientInfo.PID.CardNO);
            if (patient == null || string.IsNullOrEmpty(patient.PID.CardNO))
            {
                MessageBox.Show("该患者未进行基本信息登记，不允许使用积分支付", "警告");
                return;
            }

            //获取当前账户支付的金额
            List<BalancePay> couponPayList = new List<BalancePay>();
            if (this.tbCouponCost.Tag != null && (this.tbCouponCost.Tag as List<BalancePay>) != null &&
                (this.tbCouponCost.Tag as List<BalancePay>).Count > 0)
            {
                couponPayList.AddRange(this.tbCouponCost.Tag as List<BalancePay>);
            }

            decimal cost = 0;
            try
            {
                cost = NConvert.ToDecimal(this.tbCouponCost.Text);
            }
            catch
            {
                cost = 0;
            }

            //会员支付框
            frmCouponCost CouponCost = new frmCouponCost();

            CouponCost.IsEmpower = false;
            CouponCost.PatientInfo = patient;   //结算患者
            CouponCost.DeliverableCost = this.GetPayModeFpCost() + cost;

            string ErrInfo = string.Empty;
            CouponCost.SetPayModeRes += new DelegateHashtableSet(CouponCost_SetPayModeRes);
            CouponCost.ShowDialog();
        }

        /// <summary>
        /// 购物卡结算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btDiscountCard_Click(object sender, EventArgs e)
        {
            //判断是否存在病历号
            FS.HISFC.Models.RADT.PatientInfo patient = this.accountManager.GetPatientInfoByCardNO(this.PatientInfo.PID.CardNO);
            if (patient == null || string.IsNullOrEmpty(patient.PID.CardNO))
            {
                MessageBox.Show("该患者未进行基本信息登记，不允许使用购物卡支付", "警告");
                return;
            }

            List<BalancePay> disPayList = new List<BalancePay>();
            if (this.tbDiscountCost.Tag != null && (this.tbDiscountCost.Tag as List<BalancePay>) != null &&
                (this.tbDiscountCost.Tag as List<BalancePay>).Count > 0)
            {
                disPayList.AddRange(this.tbDiscountCost.Tag as List<BalancePay>);
            }
            decimal cost = 0;
            try
            {
                cost = NConvert.ToDecimal(this.tbDiscountCost.Text);
            }
            catch
            {
                cost = 0;
            }
            //购物卡支付框
            frmDiscountCardCost frmdiscountCost = new frmDiscountCardCost();
            frmdiscountCost.IsEmpower = true;
            frmdiscountCost.SelftPatientInfo = patient;   //结算患者
            frmdiscountCost.PatientInfo = patient;       //支付患者
            frmdiscountCost.DeliverableCost = this.GetPayModeFpCost() + cost;

            frmdiscountCost.SetPayModeRes += new DelegateHashtableSet(discountCost_SetPayModeRes);
            frmdiscountCost.ShowDialog();

        }

        /// <summary>
        /// 会员支付处理
        /// </summary>
        /// <param name="hsTable"></param>
        /// <param name="totCost"></param>
        /// <returns></returns>
        private int frmAccountCost_SetPayModeRes(Hashtable hsTable, decimal totCost)
        {
            try
            {
                //先清空
                this.tbAccountCost.Text = "0.00";
                this.tbAccountCost.Tag = null;
                this.SetCost();

                if (hsTable == null)
                {
                    this.tbAccountCost.Text = "0.00";
                    this.tbAccountCost.Tag = null;
                    this.SetCost();

                    MessageBox.Show("获取会员支付信息出错！", "警告");
                    return -1;
                }

                //基本账户支付方式列表
                List<BalancePay> accountPayList = new List<BalancePay>();
                if (hsTable.ContainsKey("YS"))
                {
                    accountPayList = hsTable["YS"] as List<BalancePay>;
                }
                //赠送账户支付方式列表
                List<BalancePay> giftPayList = new List<BalancePay>();
                if (hsTable.ContainsKey("DC"))
                {
                    giftPayList = hsTable["DC"] as List<BalancePay>;
                }
                if (accountPayList == null || giftPayList == null)
                {
                    this.tbAccountCost.Text = "0.00";
                    this.tbAccountCost.Tag = null;
                    this.SetCost();

                    MessageBox.Show("获取会员支付信息出错！", "警告");
                    return -1;
                }
                List<BalancePay> payModeList = new List<BalancePay>();
                payModeList.AddRange(accountPayList);
                payModeList.AddRange(giftPayList);
                this.tbAccountCost.Tag = payModeList;

                decimal accPayCost = 0m;
                foreach (BalancePay balancePay in payModeList)
                {
                    accPayCost += balancePay.FT.TotCost;
                }
                this.payModeFpCostShould = this.GetPayModeFpCost();
                if (accPayCost > this.payModeFpCostShould)
                {
                    this.tbAccountCost.Text = "0.00";
                    this.tbAccountCost.Tag = null;
                    this.SetCost();

                    MessageBox.Show("会员支付的金额大于患者需要支付的金额", "警告");
                    return -1;
                }
                this.tbAccountCost.Text = accPayCost.ToString("F2");

                this.SetCost();

                return 1;
            }
            catch (Exception ex)
            {
                this.tbAccountCost.Text = "0.00";
                this.tbAccountCost.Tag = null;
                this.SetCost();

                MessageBox.Show(ex.Message, "警告");
                return -1;
            }
        }

        /// <summary>
        /// 会员代付处理
        /// </summary>
        /// <param name="hsTable"></param>
        /// <param name="totCost"></param>
        /// <returns></returns>
        private int frmEmpowerCost_SetPayModeRes(Hashtable hsTable, decimal totCost)
        {

            try
            {
                //先清空
                this.tbEmpowerCost.Text = "0.00";
                this.tbEmpowerCost.Tag = null;
                this.SetCost();

                if (hsTable == null)
                {
                    this.tbEmpowerCost.Text = "0.00";
                    this.tbEmpowerCost.Tag = null;
                    this.SetCost();

                    MessageBox.Show("获取会员代付方式出错！", "警告");
                    return -1;
                }

                //基本账户支付方式列表
                List<BalancePay> empowerAccountPayList = new List<BalancePay>();
                if (hsTable.ContainsKey("YS"))
                {
                    empowerAccountPayList = hsTable["YS"] as List<BalancePay>;
                }
                //赠送账户支付方式列表
                List<BalancePay> empowerGiftPayList = new List<BalancePay>();
                if (hsTable.ContainsKey("DC"))
                {
                    empowerGiftPayList = hsTable["DC"] as List<BalancePay>;
                }

                if (empowerAccountPayList == null || empowerGiftPayList == null)
                {
                    this.tbEmpowerCost.Text = "0.00";
                    this.tbEmpowerCost.Tag = null;
                    this.SetCost();

                    MessageBox.Show("获取会员代付方式出错！", "警告");
                    return -1;
                }
                List<BalancePay> empowerPayModeList = new List<BalancePay>();
                empowerPayModeList.AddRange(empowerAccountPayList);
                empowerPayModeList.AddRange(empowerGiftPayList);
                this.tbEmpowerCost.Tag = empowerPayModeList;

                decimal empowerCost = 0m;
                foreach (BalancePay bp in empowerPayModeList)
                {
                    empowerCost += bp.FT.TotCost;
                }
                this.payModeFpCostShould = this.GetPayModeFpCost();
                if (empowerCost > this.payModeFpCostShould)
                {
                    this.tbEmpowerCost.Text = "0.00";
                    this.tbEmpowerCost.Tag = null;
                    this.SetCost();

                    MessageBox.Show("会员代付的金额大于患者需要支付的金额！", "警告");
                    return -1;
                }
                this.tbEmpowerCost.Text = empowerCost.ToString("F2");

                this.SetCost();

                return 1;
            }
            catch (Exception ex)
            {
                this.tbEmpowerCost.Text = "0.00";
                this.tbEmpowerCost.Tag = null;
                this.SetCost();

                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// 积分支付处理
        /// </summary>
        /// <param name="hsTable"></param>
        /// <param name="totCost"></param>
        /// <returns></returns>
        public int CouponCost_SetPayModeRes(Hashtable hsTable, decimal totCost)
        {
            try
            {
                //先清空
                this.tbCouponCost.Text = "0.00";
                this.tbCouponCost.Tag = null;
                this.SetCost();

                if (hsTable == null)
                {
                    this.tbCouponCost.Text = "0.00";
                    this.tbCouponCost.Tag = null;
                    this.SetCost();

                    MessageBox.Show("获取会员支付信息出错！", "警告");
                    return -1;
                }

                //积分支付方式列表
                List<BalancePay> couponPayList = new List<BalancePay>();
                if (hsTable.ContainsKey("CO"))
                {
                    couponPayList = hsTable["CO"] as List<BalancePay>;
                }

                if (couponPayList == null)
                {
                    this.tbCouponCost.Text = "0.00";
                    this.tbCouponCost.Tag = null;
                    this.SetCost();

                    MessageBox.Show("获取积分支付信息出错！", "警告");
                    return -1;
                }
                List<BalancePay> payModeList = new List<BalancePay>();
                payModeList.AddRange(couponPayList);
                this.tbCouponCost.Tag = payModeList;

                decimal couponPayCost = 0m;
                foreach (BalancePay balancePay in payModeList)
                {
                    couponPayCost += balancePay.FT.TotCost;
                }
                this.payModeFpCostShould = this.GetPayModeFpCost();
                if (couponPayCost > this.payModeFpCostShould)
                {
                    this.tbCouponCost.Text = "0.00";
                    this.tbCouponCost.Tag = null;
                    this.SetCost();

                    MessageBox.Show("积分支付的金额大于患者需要支付的金额", "警告");
                    return -1;
                }
                this.tbCouponCost.Text = couponPayCost.ToString("F2");

                this.SetCost();

                return 1;
            }
            catch (Exception ex)
            {
                this.tbCouponCost.Text = "0.00";
                this.tbCouponCost.Tag = null;
                this.SetCost();

                MessageBox.Show(ex.Message, "警告");
                return -1;
            }
        }

        /// <summary>
        /// 购物卡支付
        /// </summary>
        /// <param name="hsTable"></param>
        /// <param name="totCost"></param>
        /// <returns></returns>
        private int discountCost_SetPayModeRes(Hashtable hsTable, decimal totCost)
        {
            try
            {
                if (hsTable == null)
                {
                    throw new Exception("获取购物卡支付方式出错！");
                }

                //先清空
                this.tbDiscountCost.Text = "0.00";
                this.tbDiscountCost.Tag = null;
                this.SetCost();

                if (hsTable == null)
                {
                    this.tbDiscountCost.Text = "0.00";
                    this.tbDiscountCost.Tag = null;
                    this.SetCost();

                    MessageBox.Show("获取购物卡支付信息出错！", "警告");
                    return -1;
                }

                //支付方式列表
                List<BalancePay> discountPayList = new List<BalancePay>();
                if (hsTable.ContainsKey("DS"))
                {
                    discountPayList = hsTable["DS"] as List<BalancePay>;
                }

                if (discountPayList == null)
                {
                    this.tbDiscountCost.Text = "0.00";
                    this.tbDiscountCost.Tag = null;
                    this.SetCost();

                    MessageBox.Show("获取购物卡支付信息出错！", "警告");
                    return -1;
                }

                List<BalancePay> payModeList = new List<BalancePay>();
                payModeList.AddRange(discountPayList);
                this.tbDiscountCost.Tag = payModeList;

                decimal disPayCost = 0m;
                foreach (BalancePay balancePay in payModeList)
                {
                    disPayCost += balancePay.FT.TotCost;
                }

                this.payModeFpCostShould = this.GetPayModeFpCost();
                if (disPayCost > this.payModeFpCostShould)
                {
                    this.tbDiscountCost.Text = "0.00";
                    this.tbDiscountCost.Tag = null;
                    this.SetCost();

                    MessageBox.Show("购物卡支付的金额大于患者需要支付的金额", "警告");
                    return -1;
                }
                this.tbDiscountCost.Text = disPayCost.ToString("F2");

                this.SetCost();

                return 1;
            }
            catch (Exception ex)
            {
                this.tbDiscountCost.Text = "0.00";
                this.tbDiscountCost.Tag = null;
                this.SetCost();

                MessageBox.Show(ex.Message, "警告");
                return -1;
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
            if (column == (int)ColumnPayAccount.PayCost)
            {
                try
                {
                    if (FS.FrameWork.Function.NConvert.ToDecimal(this.fpPayType_Sheet1.Cells[this.fpPayType_Sheet1.ActiveRowIndex, (int)ColumnPayAccount.PayCost].Value) > 0)
                    {
                        return;
                    }

                    decimal CAcost = 0;
                    for (int i = 0; i < this.fpPayType_Sheet1.RowCount; i++)
                    {
                        try
                        {
                            if (this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayWay].Text != string.Empty)
                            {
                                if (this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayWay].Text == "现金")
                                {
                                    CAcost += FS.FrameWork.Function.NConvert.ToDecimal(this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayCost].Value);
                                    this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayCost].Value = 0;
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
                                this.fpPayType_Sheet1.Cells[this.rowCA, (int)ColumnPayAccount.PayCost].Value = CAcost;

                            }
                            else
                            {
                                this.fpPayType_Sheet1.Cells[this.fpPayType_Sheet1.ActiveRowIndex, (int)ColumnPayAccount.PayCost].Value = CAcost;
                            }

                        }
                        else
                        {
                            this.fpPayType_Sheet1.Cells[this.fpPayType_Sheet1.ActiveRowIndex, (int)ColumnPayAccount.PayCost].Value = CAcost;

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
            //this.SetCostValue(e.Column);
        }

        /// <summary>
        /// 获得支付方式的集合
        /// </summary>
        /// <returns>成功 支付方式的集合 失败 null</returns>
        private List<FS.HISFC.Models.Fee.Inpatient.BalancePay> QueryBalancePays()
        {
            List<FS.HISFC.Models.Fee.Inpatient.BalancePay> balancePays = new List<FS.HISFC.Models.Fee.Inpatient.BalancePay>();
            FS.HISFC.Models.Fee.Inpatient.BalancePay balancePay = null;
            decimal bpCost = 0m;

            #region 1、会员支付

            decimal accountCost = NConvert.ToDecimal(this.tbAccountCost.Text);

            decimal compargeCost = 0;
            if (accountCost > 0)
            {
                List<BalancePay> accPayList = this.tbAccountCost.Tag as List<BalancePay>;
                if (accPayList != null && accPayList.Count > 0)
                {
                    foreach (BalancePay bp in accPayList)
                    {
                        if (bp != null && !string.IsNullOrEmpty(bp.PayType.ID))
                        {
                            balancePay = bp.Clone();
                            balancePay.RetrunOrSupplyFlag = "1";   //补收
                            balancePay.TransKind.ID = "1";    //结算款
                            balancePay.TransType = FS.HISFC.Models.Base.TransTypes.Positive;  //正交易
                            balancePay.Qty = 1; 

                            bpCost += balancePay.FT.TotCost;
                            compargeCost += balancePay.FT.TotCost;

                            balancePays.Add(balancePay);
                        }
                    }
                }
            }
            if (accountCost != compargeCost)
            {
                MessageBox.Show("会员支付方式金额不符!", "警告");
                return null;
            }

            #endregion

            #region 2、会员代付

            decimal empowerCost = NConvert.ToDecimal(this.tbEmpowerCost.Text);

            compargeCost = 0;
            if (empowerCost > 0)
            {
                List<BalancePay> accPayList = this.tbEmpowerCost.Tag as List<BalancePay>;
                if (accPayList != null && accPayList.Count > 0)
                {
                    foreach (BalancePay bp in accPayList)
                    {
                        if (bp != null && !string.IsNullOrEmpty(bp.PayType.ID))
                        {
                            balancePay = bp.Clone();
                            balancePay.RetrunOrSupplyFlag = "1";   //补收
                            balancePay.TransKind.ID = "1";    //结算款
                            balancePay.TransType = FS.HISFC.Models.Base.TransTypes.Positive;  //正交易
                            balancePay.Qty = 1; 

                            bpCost += balancePay.FT.TotCost;
                            compargeCost += balancePay.FT.TotCost;

                            balancePays.Add(balancePay);
                        }
                    }
                }
            }
            if (empowerCost != compargeCost)
            {
                MessageBox.Show("会员代付支付方式金额不符!", "警告");
                return null;
            }

            #endregion

            #region 3、套餐支付

            decimal packageCost = NConvert.ToDecimal(this.tbPackageCost.Text);

            bool isAddCostDetail = false;
            compargeCost = 0;
            if (packageCost > 0)
            {
                List<BalancePay> accPayList = this.tbPackageCost.Tag as List<BalancePay>;
                if (accPayList != null && accPayList.Count > 0)
                {
                    foreach (BalancePay bp in accPayList)
                    {
                        if (bp != null && !string.IsNullOrEmpty(bp.PayType.ID))
                        {
                            balancePay = bp.Clone();
                            balancePay.RetrunOrSupplyFlag = "1";   //补收
                            balancePay.TransKind.ID = "1";    //结算款
                            balancePay.TransType = FS.HISFC.Models.Base.TransTypes.Positive;  //正交易
                            balancePay.Qty = 1; 

                            bpCost += balancePay.FT.TotCost;
                            compargeCost += balancePay.FT.TotCost;

                            if (!isAddCostDetail)
                            {
                                balancePay.UsualObject = this.lblPackageInfo.Tag;
                                isAddCostDetail = true;
                            }

                            balancePays.Add(balancePay);
                        }
                    }
                }
            }
            if (packageCost != compargeCost)
            {
                MessageBox.Show("套餐支付方式金额不符!", "警告");
                return null;
            }

            #endregion

            #region 4、积分支付

            decimal couponCost = NConvert.ToDecimal(this.tbCouponCost.Text);

            compargeCost = 0;
            if (couponCost > 0)
            {
                List<BalancePay> couponPayList = this.tbCouponCost.Tag as List<BalancePay>;
                if (couponPayList != null && couponPayList.Count > 0)
                {
                    foreach (BalancePay bp in couponPayList)
                    {
                        if (bp != null && !string.IsNullOrEmpty(bp.PayType.ID))
                        {
                            balancePay = bp.Clone();
                            balancePay.RetrunOrSupplyFlag = "1";   //补收
                            balancePay.TransKind.ID = "1";    //结算款
                            balancePay.TransType = FS.HISFC.Models.Base.TransTypes.Positive;  //正交易
                            balancePay.Qty = 1;

                            bpCost += balancePay.FT.TotCost;
                            compargeCost += balancePay.FT.TotCost;

                            balancePays.Add(balancePay);
                        }
                    }
                }
            }
            if (couponCost != compargeCost)
            {
                MessageBox.Show("积分支付方式金额不符!", "警告");
                return null;
            }

            #endregion

            #region 5、购物卡支付

            decimal discountCost = NConvert.ToDecimal(this.tbDiscountCost.Text);

            compargeCost = 0;
            if (discountCost > 0)
            {
                List<BalancePay> disPayList = this.tbDiscountCost.Tag as List<BalancePay>;
                if (disPayList != null && disPayList.Count > 0)
                {
                    foreach (BalancePay bp in disPayList)
                    {
                        if (bp != null && !string.IsNullOrEmpty(bp.PayType.ID))
                        {
                            balancePay = bp.Clone();

                            balancePayCost += balancePay.FT.TotCost;
                            compargeCost += balancePay.FT.TotCost;

                            balancePays.Add(balancePay);
                        }
                    }
                }
            }
            if (discountCost != compargeCost)
            {
                MessageBox.Show("购物卡支付方式金额不符!", "警告");
                return null;
            }

            #endregion

            //6、FP支付方式
            compargeCost = 0;
            for (int i = 0; i < this.fpPayType_Sheet1.RowCount; i++)
            {
                if (this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayWay].Text == string.Empty)
                {
                    continue;
                }
                if (this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayCost].Text == string.Empty)
                {
                    continue;
                }
                if (FS.FrameWork.Function.NConvert.ToDecimal
                    (this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayCost].Value) == 0)
                {
                    continue;
                }
                balancePay = new FS.HISFC.Models.Fee.Inpatient.BalancePay();

                balancePay.PayType.Name = this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayWay].Text;
                balancePay.PayType.ID = payWayHelper.GetID(balancePay.PayType.Name);
                if (balancePay.PayType.ID == null || balancePay.PayType.ID.ToString() == string.Empty)
                {
                    return null;
                }
                balancePay.FT.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayCost].Value.ToString());
                if (balancePay.PayType.Name == "现金")
                {
                    balancePay.FT.RealCost = balancePay.FT.TotCost;//暂时不四舍五入
                }
                else
                {
                    balancePay.FT.RealCost = balancePay.FT.TotCost;
                }
                balancePay.Bank.Name = this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayBank].Text;
                balancePay.Bank.ID = bankHelper.GetID(balancePay.Bank.Name);
                balancePay.Bank.Account = this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayAccountNO].Text;
                if (balancePay.PayType.Name == "支票" || balancePay.PayType.Name == "汇票")
                {
                    balancePay.Bank.InvoiceNO = this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayTranscationNO].Text;
                }
                else
                {
                   
                        balancePay.POSNO = this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayTranscationNO].Text;
                }
                if (balancePay.PayType.Name == "应收其他" && balancePay.FT.TotCost>0)////{B7E5CA30-EC53-4fea-BB60-55B8D8DE3CAB} 判断应收其他是否填写了备注信息
                {
                    balancePay.Memo = this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.payMemo].Text;
                    //balancePay .POSTRANS_NO
                    if (string.IsNullOrEmpty(balancePay.Memo))
                    {
                        MessageBox.Show("支付方式是应收其他则处必须填写备注信息！");
                        return null;
                    }
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

                bpCost += balancePay.FT.TotCost;
                compargeCost += balancePay.FT.TotCost;

                balancePays.Add(balancePay);
            }

            //应缴款金额
            if (this.shouldPay > 0)
            {
                this.payModeFpCostShould = this.GetPayModeFpCost();
                if (this.payModeFpCostShould != compargeCost)
                {
                    MessageBox.Show("患者其它支付方式金额不符!", "警告");
                    return null;
                }

                if (this.shouldPay != bpCost)
                {
                    MessageBox.Show("支付方式总金额不等于应缴金额!");
                    return null;
                }

            }

            return balancePays;
        }

        /// <summary>
        /// 设置位置
        /// </summary>
        private void SetLocation()
        {
            if (this.fpPayType_Sheet1.ActiveColumnIndex == (int)ColumnPayAccount.PayWay)
            {
                Control cell = this.fpPayType.EditingControl;
                fpPayType.Location = new Point(this.fpPayType.Location.X + cell.Location.X + 4,
                this.gbPayMode.Location.Y + this.fpPayType.Location.Y + cell.Location.Y + cell.Height * 2 + SystemInformation.Border3DSize.Height * 2);
                fpPayType.Size = new Size(cell.Width + 50 + SystemInformation.Border3DSize.Width * 2, 150);
                if (fpPayType.Location.Y + fpPayType.Height > this.fpPayType.Location.Y + this.fpPayType.Height)
                {
                    fpPayType.Location = new Point(this.fpPayType.Location.X + cell.Location.X + 4,
                        this.gbPayMode.Location.Y + this.fpPayType.Location.Y + cell.Location.Y + cell.Height * 2 + SystemInformation.Border3DSize.Height * 2
                        - fpPayType.Size.Height - cell.Height);
                }
            }
        }

        private void SetFP()
        {
            for (int i = 0; i < this.fpPayType_Sheet1.RowCount; i++)
            {
                if (!string.IsNullOrEmpty(this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayCost].Text))
                {
                    if (FS.FrameWork.Function.NConvert.ToDecimal(this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayCost].Value.ToString()) > 0)
                    {
                        //this.fpPayType_Sheet1.RowHeader.Rows[i].Label = "收";
                        //this.fpPayType_Sheet1.RowHeader.Rows[i].BackColor = Color.Red;

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

                    }
                    else if (FS.FrameWork.Function.NConvert.ToDecimal(this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayCost].Value.ToString()) < 0)
                    {
                        //this.fpPayType_Sheet1.RowHeader.Rows[i].Label = "欠";
                        //this.fpPayType_Sheet1.RowHeader.Rows[i].BackColor = Color.Red;

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
            if (fpPayType_Sheet1.ActiveColumnIndex == (int)ColumnPayAccount.PayCost)
            {
                #region MyRegion

                string tempString = this.fpPayType_Sheet1.Cells[fpPayType_Sheet1.ActiveRowIndex, (int)ColumnPayAccount.PayWay].Text;

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
                    if (this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayWay].Text != string.Empty)
                    {
                        if (this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayWay].Text != "现金" &&  //!this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Locked &&
                              FS.FrameWork.Function.NConvert.ToDecimal(this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayCost].Value) > 0
                            )
                        {
                            isOnlyCash = false;
                            nowCost += FS.FrameWork.Function.NConvert.ToDecimal(this.fpPayType_Sheet1.Cells[i, (int)ColumnPayAccount.PayCost].Value);
                        }
                    }
                }
                if (isOnlyCash)
                {
                    this.txtPay.Text = FS.FrameWork.Public.String.FormatNumber(shouldPay, 2).ToString();
                    this.fpPayType_Sheet1.Cells[this.rowCA, (int)ColumnPayAccount.PayCost].Text = shouldPay.ToString();
                    this.SetFP();

                }
                else
                {
                    if (shouldPay - nowCost < 0)
                    {
                        this.fpPayType_Sheet1.Cells[this.fpPayType_Sheet1.ActiveRowIndex, (int)ColumnPayAccount.PayCost].Value = 0;
                        this.fpPayType_Sheet1.SetActiveCell(this.fpPayType_Sheet1.ActiveRowIndex, (int)ColumnPayAccount.PayCost, false);

                        nowCost = 0;
                    }
                    else
                    {

                        this.txtPay.Text = FS.FrameWork.Public.String.FormatNumber(shouldPay, 2).ToString();
                        this.fpPayType_Sheet1.Cells[this.rowCA, (int)ColumnPayAccount.PayCost].Value = shouldPay - nowCost;
                        this.SetFP();
                    }
                }


                #endregion
            }
        }

    }


    /// <summary>
    /// 支付方式列表
    /// </summary>
    enum ColumnPayAccount
    {
        PayWay,
        PayCost,
        PayBank,
        PayAccountNO,
        PayCorporation,
        PayTranscationNO,
        payMemo
    }
}
