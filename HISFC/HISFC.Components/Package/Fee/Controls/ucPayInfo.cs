using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;

namespace HISFC.Components.Package.Fee.Controls
{
    public partial class ucPayInfo : FS.FrameWork.WinForms.Controls.ucBaseControl
    {

        #region 业务类

        /// <summary>
        /// 支付方式列表
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper helpPayMode = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 管理业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 账户管理类
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account account = new FS.HISFC.BizLogic.Fee.Account();

        /// <summary>
        /// 购买套餐管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.MedicalPackage.Fee.Package feePackageProcess = new FS.HISFC.BizProcess.Integrate.MedicalPackage.Fee.Package();

        /// <summary>
        /// 发票业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.InvoiceServiceNoEnum invoiceServiceManager = new FS.HISFC.BizLogic.Fee.InvoiceServiceNoEnum();

        /// <summary>
        /// 费用业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();        
        /// <summary>
        /// 账户管理类
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();

        #endregion

        #region 属性

        /// <summary>
        /// 小数保留函数设置
        /// </summary>
        //private int roundControl = 2;         // 0-保留证书，1-保留一位小数,2-保留两位小数,3-下取整,4-上取整

        /// <summary>
        /// 当前患者
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// 当前患者
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            get { return this.patientInfo; }
            set 
            {
                this.patientInfo = value;
                this.RefreshPayMode();
                this.RefreshDeposit();
            }
            
        }

        /// <summary>
        /// 存储优惠和支付类型的哈希表
        /// </summary>
        private Hashtable hsPayCost = new Hashtable();

        /// <summary>
        /// 存储优惠和支付类型的哈希表
        /// </summary>
        public Hashtable HsPayCost
        {
            get { return this.hsPayCost; }
            set
            {
                this.hsPayCost = value;
                this.setCostInfo();
            }
        }

        /// <summary>
        /// 套餐集合
        /// </summary>
        protected ArrayList alPackageLists = new ArrayList();

        /// <summary>
        /// 套餐集合
        /// </summary>
        public System.Collections.ArrayList PackageLists
        {
            set
            {
                this.alPackageLists = value;
            }
            get
            {
                return this.alPackageLists;
            }
        }

        /// <summary>
        /// 账户支付金额
        /// </summary>
        private ArrayList accountPay = new ArrayList();

        /// <summary>
        /// 赠送支付金额
        /// </summary>
        private ArrayList giftPay = new ArrayList();

        /// <summary>
        /// 代付账户金额
        /// </summary>
        private ArrayList otherAccountPay = new ArrayList();

        /// <summary>
        /// 代付赠送金额
        /// </summary>
        private ArrayList otherGiftPay = new ArrayList();

        /// <summary>
        /// 积分支付金额
        /// </summary>
        private ArrayList couponPay = new ArrayList();

        /// <summary>
        /// 购物卡支付金额
        /// </summary>
        private ArrayList discountCardPay = new ArrayList();
        #endregion

        /// <summary>
        /// 支付信息panel
        /// </summary>
        public ucPayInfo()
        {
            InitializeComponent();
            InitPayModeHelp();
            addEvent();
        }

        #region 内部函数

        /// <summary>
        /// 初始化支付方式帮助类
        /// </summary>
        private void InitPayModeHelp()
        {
            ArrayList payModes = this.managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PAYMODES);
            this.helpPayMode.ArrayObject = payModes;

            //FP热键屏蔽
            InputMap im;
            im = this.fpPayMode.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.fpPayMode.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Left, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.fpPayMode.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Right, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

        }

        /// <summary>
        /// 添加触发事件
        /// </summary>
        private void addEvent()
        {
            this.fpPayMode.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(fpPayMode_CellClick);
            this.fpPayMode.Leave += new EventHandler(fpPayMode_Leave);
            this.fpPayMode.EditModeOn += new EventHandler(fpPayMode_EditModeOn);
            this.fpPayMode.Change += new ChangeEventHandler(fpPayMode_Change);
            this.fpDeposit.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(fpDeposit_CellClick);
            this.fpDeposit.ButtonClicked += new EditorNotifyEventHandler(fpDeposit_ButtonClicked);
            this.btnUpdate.Click += new EventHandler(btnUpdate_Click);
            this.tbPAY.TextChanged += new EventHandler(tbPAY_TextChanged);
        }

        /// <summary>
        /// 删除触发事件
        /// </summary>
        private void delEvent()
        {
            this.fpPayMode.CellClick -= new FarPoint.Win.Spread.CellClickEventHandler(fpPayMode_CellClick);
            this.fpPayMode.Leave -= new EventHandler(fpPayMode_Leave);
            this.fpPayMode.EditModeOn -= new EventHandler(fpPayMode_EditModeOn);
            this.fpPayMode.Change -= new ChangeEventHandler(fpPayMode_Change);
            this.fpDeposit.CellClick -= new FarPoint.Win.Spread.CellClickEventHandler(fpDeposit_CellClick);
            this.fpDeposit.ButtonClicked -= new EditorNotifyEventHandler(fpDeposit_ButtonClicked);
            this.btnUpdate.Click -= new EventHandler(btnUpdate_Click);
            this.tbPAY.TextChanged -= new EventHandler(tbPAY_TextChanged);
        }

        void fpPayMode_Change(object sender, ChangeEventArgs e)
        {

            //编辑的是支付金额
            if (e.Column == (int)PayModeCols.TotCost )
            {
                this.delEvent();

                try
                {
                    //输入金额
                    decimal cost = 0.0m;

                    if (this.fpPayMode_Sheet1.Cells[e.Row, e.Column].Value == null)
                    { 
                        this.fpPayMode_Sheet1.Cells[e.Row, e.Column].Value = 0;
                    }

                    if (this.fpPayMode_Sheet1.Cells[e.Row, e.Column].Value != null)
                    {
                        cost = Decimal.Parse(this.fpPayMode_Sheet1.Cells[e.Row, e.Column].Value.ToString());
                    }

                    if (cost < 0)
                    {
                        throw new Exception("输入金额不能小于零！");
                    }

                    //非账户支付
                    if (this.fpPayMode_Sheet1.Rows[e.Row].Tag is FS.HISFC.Models.Base.Const)
                    {

                        if (this.setCostInfoAfterEdit() < 0)
                        {
                            throw new Exception("输入金额大于应缴纳金额！");
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.fpPayMode_Sheet1.Cells[e.Row, e.Column].Value = this.previousValue;
                    MessageBox.Show(ex.Message);
                }

                this.setCostInfoAfterEdit();
                this.setCostTextBox();
                this.addEvent();
            }
        }

        /// <summary>
        /// 押金列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpDeposit_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Column == 0)
            {
                return;
            }

            this.fpDeposit_Sheet1.SetActiveCell(e.Row, (int)DepositCols.Select);
        }

        /// <summary>
        /// 押金勾选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpDeposit_ButtonClicked(object sender, EditorNotifyEventArgs e)
        {
            this.delEvent();

            if (this.fpDeposit_Sheet1.RowCount == 0)
            {
                this.addEvent();
                return;
            }

            try
            {
                FS.HISFC.Models.MedicalPackage.Fee.Deposit deposit = new FS.HISFC.Models.MedicalPackage.Fee.Deposit();
                deposit = this.fpDeposit_Sheet1.Rows[e.Row].Tag as FS.HISFC.Models.MedicalPackage.Fee.Deposit;

                if (this.setCostInfoAfterEdit() < 0 && ((decimal)HsPayCost["ACTU"]) > 0)
                {
                    this.fpDeposit_Sheet1.Cells[e.Row, 0].Value = false;
                    throw new Exception("所缴金额大应付金额");
                }

                //若已勾选押金总额已大于所需金额时
                if ((decimal)this.HsPayCost["DEPO"] > Decimal.Parse(this.tbREAL.Text) &&
                   (decimal)this.HsPayCost["DEPO"] - deposit.Amount > Decimal.Parse(HsPayCost["REAL"].ToString()))
                {
                    this.fpDeposit_Sheet1.Cells[e.Row, 0].Value = false;
                    throw new Exception("所缴金额大应付金额");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.setCostInfoAfterEdit();
            this.setCostTextBox();
            this.addEvent();
        }

        /// <summary>
        /// 支付方式列表点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpPayMode_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.fpPayMode_Sheet1.SetActiveCell(e.Row, (int)PayModeCols.Name);
            this.fpPayMode_Sheet1.SetActiveCell(e.Row, (int)PayModeCols.TotCost);

            //账户
            FS.HISFC.Models.Base.Const cst = this.fpPayMode_Sheet1.Rows[e.Row].Tag as FS.HISFC.Models.Base.Const;
            if((cst.ID == "YS" || cst.ID == "DC" ) && e.Column == (int)PayModeCols.TotCost)
            {
                this.AccountPayShow();
            }

            //代付
            if ((cst.ID == "OTYS" || cst.ID == "OTDC") && e.Column == (int)PayModeCols.TotCost)
            {
                this.OtherAccountPayShow();
            }

            if (cst.ID == "CO" && e.Column == (int)PayModeCols.TotCost)
            {
                this.CouponPayShow();
            }

            if (cst.ID == "DS" && e.Column == (int)PayModeCols.TotCost)
            {
                this.DiscountCardShow();
            }
        }

        /// <summary>
        ///  获取会员账户支付方式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AccountPayShow()
        {
            try
            {
                this.delEvent();

                int CashRow = -1;
                int YSRow = -1;
                int DCRow = -1;
                decimal CashCost = 0.0m;
                decimal YSCost = 0.0m;
                decimal DCCost = 0.0m;

                //获取当前现金支付的金额
                foreach (Row row in this.fpPayMode_Sheet1.Rows)
                {
                    FS.HISFC.Models.Base.Const pay = row.Tag as FS.HISFC.Models.Base.Const;
                    if (pay == null)
                    {
                        throw new Exception("支付方式转换错误！");
                    }

                    if (pay.ID == "CA")
                    {
                        CashCost = Decimal.Parse(this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value.ToString());
                        CashRow = row.Index;
                    }

                    if (pay.ID == "YS")
                    {
                        YSCost = Decimal.Parse(this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value.ToString());
                        YSRow = row.Index;
                    }

                    if (pay.ID == "DC")
                    {
                        DCCost = Decimal.Parse(this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value.ToString());
                        DCRow = row.Index;
                    }
                }

                if (CashRow == -1 || YSRow == -1 || DCRow == -1 )
                {
                    throw new Exception("查找支付方式失败！");
                }

                //获取当前账户支付的金额
                ArrayList tmp = new ArrayList();
                tmp.AddRange(this.accountPay);
                tmp.AddRange(this.giftPay);


                //会员支付框
                string ErrInfo = string.Empty;
                Forms.frmAccountCost accountCost = new Forms.frmAccountCost();
                //{ECECDF2F-BA74-4615-A240-C442BE0A0074}
                accountCost.PackageLists = this.PackageLists;
                accountCost.PatientInfo = this.PatientInfo;
                //{892FDDD4-0CD2-4306-87E5-ACDEF6829C76}
                accountCost.IsEmpower = false;
                accountCost.DeliverableCost = CashCost + YSCost + DCCost;
                if (accountCost.SetPayInfo(tmp, ref ErrInfo) < 0)
                {
                    throw new Exception(ErrInfo);
                }
                accountCost.SetPayModeRes += new HISFC.Components.Package.Fee.Forms.DelegateHashtableSet(accountCost_SetPayModeRes);
                accountCost.ShowDialog();
            }
            catch (Exception ex)
            {
                this.addEvent();
                MessageBox.Show("获取待支付金额出错:"+ ex.Message);
                return;
            }

            this.addEvent();
        }

        /// <summary>
        /// 弹出会员支付框后点击确认触发事件
        /// </summary>
        /// <param name="hsTable"></param>
        /// <returns></returns>
        private int accountCost_SetPayModeRes(Hashtable hsTable,decimal totCashCost)
        {
            try
            {

                if (hsTable == null)
                {
                    throw new Exception("获取会员支付方式出错！");
                }

                if (hsTable.ContainsKey("YS"))
                {
                    this.accountPay = hsTable["YS"] as ArrayList;
                }

                if (hsTable.ContainsKey("DC"))
                {
                    this.giftPay = hsTable["DC"] as ArrayList;
                }

                if (this.accountPay == null || this.giftPay == null)
                {
                    foreach (Row row in this.fpPayMode_Sheet1.Rows)
                    {
                        FS.HISFC.Models.Base.Const pay = row.Tag as FS.HISFC.Models.Base.Const;

                        if (pay.ID == "YS")
                        {
                            this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value = 0.0m;
                        }

                        if (pay.ID == "DC")
                        {
                            this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value = 0.0m;
                        }
                    }

                    accountPay = new ArrayList();
                    giftPay = new ArrayList();
                    throw new Exception("获取会员支付信息出错！");
                }

                int CashRow = -1;
                //获取当前现金支付的金额
                foreach (Row row in this.fpPayMode_Sheet1.Rows)
                {
                    FS.HISFC.Models.Base.Const pay = row.Tag as FS.HISFC.Models.Base.Const;
                    if (pay == null)
                    {
                        throw new Exception("支付方式转换错误！");
                    }

                    if (pay.ID == "CA")
                    {
                        CashRow = row.Index;
                        break;
                    }
                }

                if (CashRow == -1)
                {
                    throw new Exception("不存在现金支付方式！");
                }


                foreach (Row row in this.fpPayMode_Sheet1.Rows)
                {
                    FS.HISFC.Models.Base.Const pay = row.Tag as FS.HISFC.Models.Base.Const;

                    if (pay.ID == "YS")
                    {
                        decimal ysCost = 0.0m;
                        foreach (FS.HISFC.Models.MedicalPackage.Fee.PayMode payMode in accountPay)
                        {
                            ysCost += payMode.Tot_cost;
                        }
                        this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value = ysCost;
                        totCashCost -= ysCost;
                        this.fpPayMode_Sheet1.Cells[CashRow, (int)PayModeCols.TotCost].Value = totCashCost;
                    }

                    if (pay.ID == "DC")
                    {
                        decimal gfCost = 0.0m;
                        foreach (FS.HISFC.Models.MedicalPackage.Fee.PayMode payMode in giftPay)
                        {
                            gfCost += payMode.Tot_cost;
                        }
                        this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value = gfCost;
                        totCashCost -= gfCost;
                        this.fpPayMode_Sheet1.Cells[CashRow, (int)PayModeCols.TotCost].Value = totCashCost;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }

            this.setCostInfoAfterEdit();
            this.setCostTextBox();
            return 1;
        }

        /// <summary>
        ///  获取会员代付
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OtherAccountPayShow()
        {
            try
            {
                this.delEvent();

                int CashRow = -1;
                int YSRow = -1;
                int DCRow = -1;
                decimal CashCost = 0.0m;
                decimal YSCost = 0.0m;
                decimal DCCost = 0.0m;

                //获取当前现金支付的金额
                foreach (Row row in this.fpPayMode_Sheet1.Rows)
                {
                    FS.HISFC.Models.Base.Const pay = row.Tag as FS.HISFC.Models.Base.Const;
                    if (pay == null)
                    {
                        throw new Exception("支付方式转换错误！");
                    }

                    if (pay.ID == "CA")
                    {
                        CashCost = Decimal.Parse(this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value.ToString());
                        CashRow = row.Index;
                    }

                    if (pay.ID == "OTYS")
                    {
                        YSCost = Decimal.Parse(this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value.ToString());
                        YSRow = row.Index;
                    }

                    if (pay.ID == "OTDC")
                    {
                        DCCost = Decimal.Parse(this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value.ToString());
                        DCRow = row.Index;
                    }
                }

                if (CashRow == -1 || YSRow == -1 || DCRow == -1)
                {
                    throw new Exception("查找支付方式失败！");
                }

                //获取当前账户支付的金额
                ArrayList tmp = new ArrayList();
                tmp.AddRange(this.otherAccountPay);
                tmp.AddRange(this.otherGiftPay);


                //会员支付框
                string ErrInfo = string.Empty;
                Forms.frmAccountCost otheraccountCost = new Forms.frmAccountCost();
                //{892FDDD4-0CD2-4306-87E5-ACDEF6829C76}
                otheraccountCost.IsEmpower = true;
                otheraccountCost.OriginalName = this.PatientInfo.Name; //{6ff15804-8b10-4f19-855a-e4c1d97e3714}
                otheraccountCost.OriginalCardNO = this.PatientInfo.PID.CardNO;
                FS.HISFC.Models.RADT.PatientInfo patient = null;
                if (this.otherAccountPay.Count > 0)
                {
                    FS.HISFC.Models.MedicalPackage.Fee.PayMode paymode = otherAccountPay[0] as FS.HISFC.Models.MedicalPackage.Fee.PayMode;
                    List<FS.HISFC.Models.Account.AccountDetail> accounts = this.accountMgr.GetAccountDetail(paymode.Account, "ALL","1");

                    if (accounts.Count > 0)
                    {
                        patient = accountMgr.GetPatientInfoByCardNO(accounts[0].CardNO);
                    }

                }
                else if (this.otherGiftPay.Count > 0)
                {
                    FS.HISFC.Models.MedicalPackage.Fee.PayMode paymode = otherGiftPay[0] as FS.HISFC.Models.MedicalPackage.Fee.PayMode;
                    List<FS.HISFC.Models.Account.AccountDetail> accounts = this.accountMgr.GetAccountDetail(paymode.Account, "ALL","1");

                    if (accounts.Count > 0)
                    {
                        patient = accountMgr.GetPatientInfoByCardNO(accounts[0].CardNO);
                    }
                }

                otheraccountCost.PatientInfo = patient;
                otheraccountCost.DeliverableCost = CashCost + YSCost + DCCost;
                if (otheraccountCost.SetPayInfo(tmp, ref ErrInfo) < 0)
                {
                    throw new Exception(ErrInfo);
                }
                otheraccountCost.SetPayModeRes += new HISFC.Components.Package.Fee.Forms.DelegateHashtableSet(otheraccountCost_SetPayModeRes);
                otheraccountCost.ShowDialog();
            }
            catch (Exception ex)
            {
                this.addEvent();
                MessageBox.Show("获取待支付金额出错:" + ex.Message);
                return;
            }

            this.addEvent();
        }

        /// <summary>
        /// 弹出会员支付框后点击确认触发事件
        /// </summary>
        /// <param name="hsTable"></param>
        /// <returns></returns>
        private int otheraccountCost_SetPayModeRes(Hashtable hsTable, decimal totCashCost)
        {
            try
            {

                if (hsTable == null)
                {
                    throw new Exception("获取会员支付方式出错！");
                }

                if (hsTable.ContainsKey("YS"))
                {
                    this.otherAccountPay = hsTable["YS"] as ArrayList;
                }

                if (hsTable.ContainsKey("DC"))
                {
                    this.otherGiftPay = hsTable["DC"] as ArrayList;
                }

                if (this.otherAccountPay == null || this.otherGiftPay == null)
                {
                    foreach (Row row in this.fpPayMode_Sheet1.Rows)
                    {
                        FS.HISFC.Models.Base.Const pay = row.Tag as FS.HISFC.Models.Base.Const;

                        if (pay.ID == "OTYS")
                        {
                            this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value = 0.0m;
                        }

                        if (pay.ID == "OTDC")
                        {
                            this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value = 0.0m;
                        }
                    }

                    otherAccountPay = new ArrayList();
                    otherGiftPay = new ArrayList();
                    throw new Exception("获取会员代付信息出错！");
                }

                int CashRow = -1;
                //获取当前现金支付的金额
                foreach (Row row in this.fpPayMode_Sheet1.Rows)
                {
                    FS.HISFC.Models.Base.Const pay = row.Tag as FS.HISFC.Models.Base.Const;
                    if (pay == null)
                    {
                        throw new Exception("支付方式转换错误！");
                    }

                    if (pay.ID == "CA")
                    {
                        CashRow = row.Index;
                        break;
                    }
                }

                if (CashRow == -1)
                {
                    throw new Exception("不存在现金支付方式！");
                }


                foreach (Row row in this.fpPayMode_Sheet1.Rows)
                {
                    FS.HISFC.Models.Base.Const pay = row.Tag as FS.HISFC.Models.Base.Const;

                    if (pay.ID == "OTYS")
                    {
                        decimal ysCost = 0.0m;
                        foreach (FS.HISFC.Models.MedicalPackage.Fee.PayMode payMode in otherAccountPay)
                        {
                            ysCost += payMode.Tot_cost;
                        }
                        this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value = ysCost;
                        totCashCost -= ysCost;
                        this.fpPayMode_Sheet1.Cells[CashRow, (int)PayModeCols.TotCost].Value = totCashCost;
                    }

                    if (pay.ID == "OTDC")
                    {
                        decimal gfCost = 0.0m;
                        foreach (FS.HISFC.Models.MedicalPackage.Fee.PayMode payMode in otherGiftPay)
                        {
                            gfCost += payMode.Tot_cost;
                        }
                        this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value = gfCost;
                        totCashCost -= gfCost;
                        this.fpPayMode_Sheet1.Cells[CashRow, (int)PayModeCols.TotCost].Value = totCashCost;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }

            this.setCostInfoAfterEdit();
            this.setCostTextBox();
            return 1;
        }

        /// <summary>
        /// 积分支付窗体
        /// </summary>
        private void CouponPayShow()
        {
            try
            {
                this.delEvent();

                int CashRow = -1;
                int CORow = -1;
                decimal CashCost = 0.0m;
                decimal COCost = 0.0m;

                //获取当前现金支付的金额
                foreach (Row row in this.fpPayMode_Sheet1.Rows)
                {
                    FS.HISFC.Models.Base.Const pay = row.Tag as FS.HISFC.Models.Base.Const;
                    if (pay == null)
                    {
                        throw new Exception("支付方式转换错误！");
                    }

                    if (pay.ID == "CA")
                    {
                        CashCost = Decimal.Parse(this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value.ToString());
                        CashRow = row.Index;
                    }

                    if (pay.ID == "CO")
                    {
                        COCost = Decimal.Parse(this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value.ToString());
                        CORow = row.Index;
                    }
                }

                if (CashRow == -1 || CORow == -1)
                {
                    throw new Exception("查找支付方式失败！");
                }

                //获取当前账户支付的金额
                ArrayList tmp = new ArrayList();
                tmp.AddRange(this.couponPay);


                //会员支付框
                string ErrInfo = string.Empty;
                Forms.frmCouponCost couponCost = new Forms.frmCouponCost();
                couponCost.PatientInfo = this.PatientInfo;
                couponCost.IsEmpower = false;
                couponCost.DeliverableCost = CashCost + COCost;
                couponCost.SetPayModeRes += new HISFC.Components.Package.Fee.Forms.DelegateHashtableSet(couponCost_SetPayModeRes);
                couponCost.ShowDialog();
            }
            catch (Exception ex)
            {
                this.addEvent();
                MessageBox.Show("获取待支付金额出错:" + ex.Message);
                return;
            }

            this.addEvent();
        }

        /// <summary>
        /// //{333D2AD8-DC4A-4c30-A14E-D6815AC858F9}
        /// </summary>
        /// <param name="hsTable"></param>
        /// <param name="totCost"></param>
        /// <returns></returns>
        private int couponCost_SetPayModeRes(Hashtable hsTable, decimal totCost)
        {
            try
            {

                if (hsTable == null)
                {
                    throw new Exception("获取积分支付方式出错！");
                }

                if (hsTable.ContainsKey("CO"))
                {
                    this.couponPay = hsTable["CO"] as ArrayList;
                }

                if (this.couponPay == null)
                {
                    foreach (Row row in this.fpPayMode_Sheet1.Rows)
                    {
                        FS.HISFC.Models.Base.Const pay = row.Tag as FS.HISFC.Models.Base.Const;

                        if (pay.ID == "CO")
                        {
                            this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value = 0.0m;
                        }
                    }

                    couponPay = new ArrayList();
                    throw new Exception("获取积分支付信息出错！");
                }

                int CashRow = -1;
                //获取当前现金支付的金额
                foreach (Row row in this.fpPayMode_Sheet1.Rows)
                {
                    FS.HISFC.Models.Base.Const pay = row.Tag as FS.HISFC.Models.Base.Const;
                    if (pay == null)
                    {
                        throw new Exception("支付方式转换错误！");
                    }

                    if (pay.ID == "CA")
                    {
                        CashRow = row.Index;
                        break;
                    }
                }

                if (CashRow == -1)
                {
                    throw new Exception("不存在现金支付方式！");
                }


                foreach (Row row in this.fpPayMode_Sheet1.Rows)
                {
                    FS.HISFC.Models.Base.Const pay = row.Tag as FS.HISFC.Models.Base.Const;

                    if (pay.ID == "CO")
                    {
                        decimal coCost = 0.0m;
                        foreach (FS.HISFC.Models.MedicalPackage.Fee.PayMode payMode in couponPay)
                        {
                            coCost += payMode.Tot_cost;
                        }
                        this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value = coCost;
                        totCost -= coCost;
                        this.fpPayMode_Sheet1.Cells[CashRow, (int)PayModeCols.TotCost].Value = totCost;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }

            this.setCostInfoAfterEdit();
            this.setCostTextBox();
            return 1;
        }

        /// <summary>
        /// 购物卡支付窗体
        /// </summary>
        private void DiscountCardShow()
        {
            try 
            {
                this.delEvent();
                int CashRow = -1;
                int DSRow = -1;
                decimal CashCost = 0.0m;
                decimal DSCost = 0.0m;

                //获取当前现金支付金额
                foreach (Row row in this.fpPayMode_Sheet1.Rows)
                {
                    FS.HISFC.Models.Base.Const pay = row.Tag as FS.HISFC.Models.Base.Const;
                    if (pay == null)
                    {
                        throw new Exception("支付方式转换错误！");
                    }

                    if (pay.ID == "CA")
                    {
                        CashCost = Decimal.Parse(this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value.ToString());
                        CashRow = row.Index;
                    }
                    if (pay.ID == "DS")
                    {
                        DSCost = Decimal.Parse(this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value.ToString());
                        DSRow = row.Index;
                    }

                }

                if (CashRow == -1 || DSRow == -1)
                {
                    throw new Exception("查找支付方式失败！");
                }

                 //获取当前购物卡支付的金额
                ArrayList tmp = new ArrayList();
                tmp.AddRange(this.discountCardPay);


                //购物卡支付框
                string ErrInfo = string.Empty;
                Forms.frmDiscountCardCost discountCardCost = new Forms.frmDiscountCardCost();
                discountCardCost.PatientInfo = this.PatientInfo;
                discountCardCost.IsEmpower = false;
                discountCardCost.DeliverableCost = CashCost + DSCost;
                discountCardCost.SetPayModeRes += new HISFC.Components.Package.Fee.Forms.DelegateHashtableSet(discountcardCost_SetPayModeRes);
                discountCardCost.ShowDialog();
            }
            catch (Exception ex)
            {
                this.addEvent();
                MessageBox.Show("获取待支付金额出错:" + ex.Message);
                return;
            }

            this.addEvent();
            
        }

        /// <summary>
        /// 购物卡支付框后点击确认触发事件
        /// </summary>
        /// <param name="hsTable"></param>
        /// <returns></returns>
        private int discountcardCost_SetPayModeRes(Hashtable hsTable, decimal totCost)
        {
            try
            {

                if (hsTable == null)
                {
                    throw new Exception("获取购物卡支付方式出错！");
                }

                if (hsTable.ContainsKey("DS"))
                {
                    this.discountCardPay = hsTable["DS"] as ArrayList;
                }

                if (this.discountCardPay == null)
                {
                    foreach (Row row in this.fpPayMode_Sheet1.Rows)
                    {
                        FS.HISFC.Models.Base.Const pay = row.Tag as FS.HISFC.Models.Base.Const;

                        if (pay.ID == "DS")
                        {
                            this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value = 0.0m;
                        }
                    }

                    discountCardPay = new ArrayList();
                    throw new Exception("获取购物卡支付信息出错！");
                }

                int CashRow = -1;
                //获取当前现金支付的金额
                foreach (Row row in this.fpPayMode_Sheet1.Rows)
                {
                    FS.HISFC.Models.Base.Const pay = row.Tag as FS.HISFC.Models.Base.Const;
                    if (pay == null)
                    {
                        throw new Exception("支付方式转换错误！");
                    }

                    if (pay.ID == "CA")
                    {
                        CashRow = row.Index;
                        break;
                    }
                }

                if (CashRow == -1)
                {
                    throw new Exception("不存在现金支付方式！");
                }


                foreach (Row row in this.fpPayMode_Sheet1.Rows)
                {
                    FS.HISFC.Models.Base.Const pay = row.Tag as FS.HISFC.Models.Base.Const;

                    if (pay.ID == "DS")
                    {
                        decimal dsCost = 0.0m;
                        foreach (FS.HISFC.Models.MedicalPackage.Fee.PayMode payMode in discountCardPay)
                        {
                            dsCost += payMode.Tot_cost;
                        }
                        this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value = dsCost;
                        totCost -= dsCost;
                        this.fpPayMode_Sheet1.Cells[CashRow, (int)PayModeCols.TotCost].Value = totCost;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }

            this.setCostInfoAfterEdit();
            this.setCostTextBox();
            return 1;
        }

        /// <summary>
        /// 存储输入值的上一个值，用于的输入值非法时进行恢复
        /// </summary>
        private double previousValue = 0.0;

        /// <summary>
        /// 当支付方式列表失去焦点的时候
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpPayMode_Leave(object sender, EventArgs e)
        {
            this.fpPayMode.StopCellEditing();
        }

        /// <summary>
        /// 编辑模式开启
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void fpPayMode_EditModeOn(object sender, EventArgs e)
        {
            try
            {
                previousValue = Double.Parse(this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.ActiveRowIndex, this.fpPayMode_Sheet1.ActiveColumnIndex].Value.ToString());

                this.fpPayMode.EditingControl.KeyDown += new KeyEventHandler(EditingControl_KeyDown);
            }
            catch
            { 
            }
        }

        private void EditingControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                PutArrow(Keys.Left);
            }
            if (e.KeyCode == Keys.Right)
            {
                PutArrow(Keys.Right);
            }
        }

        /// <summary>
        /// 刷新支付方式列表
        /// </summary>
        private void RefreshPayMode()
        {
            delEvent();

            try
            {
                this.fpPayMode_Sheet1.RowCount = 0;
                this.accountPay.Clear();
                this.giftPay.Clear();
                this.otherAccountPay.Clear();
                this.otherGiftPay.Clear();

                if (this.patientInfo == null)
                    return;

                if (this.helpPayMode.ArrayObject != null)
                {
                    ///增加普通支付方式
                    foreach (FS.HISFC.Models.Base.Const paymode in this.helpPayMode.ArrayObject)
                    {
                        if (paymode.Memo == "false")
                        {
                            continue;
                        }    

                        this.fpPayMode_Sheet1.Rows.Add(this.fpPayMode_Sheet1.RowCount, 1);
                        this.fpPayMode_Sheet1.Rows[this.fpPayMode_Sheet1.RowCount - 1].Tag = paymode;
                        this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.RowCount - 1, (int)PayModeCols.Name].Text = paymode.Name;
                        this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.RowCount - 1, (int)PayModeCols.TotCost].Value = 0;

                        //账户支付
                        ////{333D2AD8-DC4A-4c30-A14E-D6815AC858F9}
                        if (paymode.ID == "DC" || paymode.ID == "YS" || paymode.ID == "CO" || paymode.ID == "DS")
                        {
                            this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.RowCount - 1, (int)PayModeCols.TotCost].Locked = true;
                            this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.RowCount - 1, (int)PayModeCols.TotCost].BackColor = System.Drawing.SystemColors.Control;
                        }

                        if (paymode.ID == "RC" || paymode.ID == "TCO")
                        {
                            this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.RowCount - 1, (int)PayModeCols.TotCost].Locked = true;
                        }
                    }

                    //账户代付
                    FS.HISFC.Models.Base.Const otherAccount = new FS.HISFC.Models.Base.Const();
                    otherAccount.ID = "OTYS";
                    otherAccount.Name = "账户代付";
                    this.fpPayMode_Sheet1.Rows.Add(this.fpPayMode_Sheet1.RowCount, 1);
                    this.fpPayMode_Sheet1.Rows[this.fpPayMode_Sheet1.RowCount - 1].Tag = otherAccount;
                    this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.RowCount - 1, (int)PayModeCols.Name].Text = otherAccount.Name;
                    this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.RowCount - 1, (int)PayModeCols.TotCost].Value = 0;
                    this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.RowCount - 1, (int)PayModeCols.TotCost].Locked = true;
                    this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.RowCount - 1, (int)PayModeCols.TotCost].BackColor = System.Drawing.SystemColors.Control;

                    //赠送代付
                    FS.HISFC.Models.Base.Const otherGift = new FS.HISFC.Models.Base.Const();
                    otherGift.ID = "OTDC";
                    otherGift.Name = "赠送代付";
                    this.fpPayMode_Sheet1.Rows.Add(this.fpPayMode_Sheet1.RowCount, 1);
                    this.fpPayMode_Sheet1.Rows[this.fpPayMode_Sheet1.RowCount - 1].Tag = otherGift;
                    this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.RowCount - 1, (int)PayModeCols.Name].Text = otherGift.Name;
                    this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.RowCount - 1, (int)PayModeCols.TotCost].Value = 0;
                    this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.RowCount - 1, (int)PayModeCols.TotCost].Locked = true;
                    this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.RowCount - 1, (int)PayModeCols.TotCost].BackColor = System.Drawing.SystemColors.Control;
                }
            }
            catch
            { }
            addEvent();
        }

        /// <summary>
        /// 刷新押金信息
        /// </summary>
        private void RefreshDeposit()
        {
            delEvent();
            try
            {
                ArrayList DepositList = new ArrayList();
                this.fpDeposit_Sheet1.RowCount = 0;

                if (this.patientInfo == null)
                    return;

                DepositList = feePackageProcess.GetDepositsByPatient(this.patientInfo);

                if (DepositList != null)
                {
                    foreach (FS.HISFC.Models.MedicalPackage.Fee.Deposit deposit in DepositList)
                    {
                        this.fpDeposit_Sheet1.Rows.Add(this.fpDeposit_Sheet1.RowCount, 1);
                        this.fpDeposit_Sheet1.Rows[this.fpDeposit_Sheet1.RowCount - 1].Tag = deposit;
                        this.fpDeposit_Sheet1.Cells[this.fpDeposit_Sheet1.RowCount - 1, (int)DepositCols.Select].Locked = false;
                        this.fpDeposit_Sheet1.Cells[this.fpDeposit_Sheet1.RowCount - 1, (int)DepositCols.Select].Value = false;
                        this.fpDeposit_Sheet1.Cells[this.fpDeposit_Sheet1.RowCount - 1, (int)DepositCols.ID].Text = deposit.ID;
                        FS.HISFC.Models.Account.AccountDetail tmp = new FS.HISFC.Models.Account.AccountDetail();
                        if (deposit.Mode_Code == "YS" || deposit.Mode_Code == "DC")
                        {
                            List<FS.HISFC.Models.Account.AccountDetail> accountList = account.GetAccountDetail(deposit.Account, deposit.AccountType,"1");
                            if (accountList == null || accountList.Count == 0)
                            {
                                this.fpDeposit_Sheet1.RowCount = 0;
                                throw new Exception("查找押金信息失败！");
                            }
                            string Name = accountList[0].AccountType.Name;
                            if (deposit.AccountFlag == "0")
                            {
                                Name += "[基本]";
                            }
                            else
                            {
                                Name += "[赠送]";
                            }
                            this.fpDeposit_Sheet1.Cells[this.fpDeposit_Sheet1.RowCount - 1, (int)DepositCols.PayMode].Text = Name;
                        }
                        else
                        {
                            string Name = string.Empty;
                            foreach (FS.HISFC.Models.Base.Const paymode in this.helpPayMode.ArrayObject)
                            {
                                if (paymode.ID == deposit.Mode_Code)
                                {
                                    Name = paymode.Name;
                                    break;
                                }
                            }

                            if (string.IsNullOrEmpty(Name))
                            {
                                this.fpDeposit_Sheet1.RowCount = 0;
                                throw new Exception("查找押金信息失败！");
                            }
                            this.fpDeposit_Sheet1.Cells[this.fpDeposit_Sheet1.RowCount - 1, (int)DepositCols.PayMode].Text = Name;
                        }
                        this.fpDeposit_Sheet1.Cells[this.fpDeposit_Sheet1.RowCount - 1, (int)DepositCols.Amount].Value = deposit.Amount.ToString("F2");
                        this.fpDeposit_Sheet1.Cells[this.fpDeposit_Sheet1.RowCount - 1, (int)DepositCols.Memo].Value = deposit.Memo;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            addEvent();
        }

        /// <summary>
        /// 设置支付信息
        /// </summary>
        private void setCostInfo()
        {
            delEvent();

            //支付方式清空
            foreach (Row row in this.fpPayMode_Sheet1.Rows)
            {
                Cell currentCell = this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost];
                this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value = 0.0m;
                this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.Memo].Value = "";

                FS.HISFC.Models.Base.Const cst = row.Tag as FS.HISFC.Models.Base.Const;

                if (hsPayCost != null && hsPayCost.ContainsKey("ETC") && cst.ID == "RC")
                {
                    currentCell.Value = Decimal.Parse(hsPayCost["ETC"].ToString());
                }
            }

            //押金信息
            foreach (Row row in this.fpDeposit_Sheet1.Rows)
            {
                this.fpDeposit_Sheet1.Cells[row.Index, (int)DepositCols.Select].Value = false;
            }

            //清空账户支付
            this.accountPay.Clear();
            this.giftPay.Clear();
            this.otherAccountPay.Clear();
            this.otherGiftPay.Clear();

            //设置金额信息显示
            this.setCostInfoAfterEdit();
            this.setCostTextBox();

            addEvent();
        }

        /// <summary>
        /// 设置金额信息显示
        /// </summary>
        private void setCostTextBox()
        {
            try
            {
                this.tbREAL.Text = this.hsPayCost["REAL"].ToString();      //应收
                this.tbETC.Text = this.hsPayCost["ETC"].ToString();       //优惠
                this.tbTotCost.Text = this.hsPayCost["TOT"].ToString();   //总额


                this.tbACTU.Text = this.hsPayCost["ACTU"].ToString();     //实际
                this.tbGIFT.Text = this.hsPayCost["GIFT"].ToString();       //赠送
                this.tbDEPO.Text = this.hsPayCost["DEPO"].ToString();    //押金

                decimal CashCost = 0.0m;
                int CashRow = -1;
                foreach (Row row in this.fpPayMode_Sheet1.Rows)
                {
                    if (row.Tag is FS.HISFC.Models.Base.Const)
                    {
                        if ((row.Tag as FS.HISFC.Models.Base.Const).ID == "CA")
                        {
                            CashCost = Decimal.Parse(this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value.ToString());
                            CashRow = row.Index;
                            break;
                        }
                    }
                }

                this.tbCash.Text = (CashCost - (decimal)this.hsPayCost["ROUND"]).ToString();
                this.tbDecimal.Text = this.hsPayCost["ROUND"].ToString();

                if (CashCost > 0)
                {
                    string pay = this.tbPAY.Text;
                    if (string.IsNullOrEmpty(pay))
                    {
                        pay = "0";
                    }
                    this.tbRest.Text = (Decimal.Parse(pay) - Decimal.Parse(this.tbCash.Text)).ToString();
                }
                else
                {
                    this.tbPAY.Text = "0";
                    this.tbRest.Text = "0.00";
                }
            }
            catch
            {
                this.tbREAL.Text = "0.00";      //应收
                this.tbETC.Text = "0.00";       //优惠
                this.tbTotCost.Text = "0.00";   //总额


                this.tbACTU.Text = "0.00";    //实际
                this.tbGIFT.Text = "0.00";    //赠送
                this.tbDEPO.Text = "0.00";    //押金

                //待缴费金额
                this.tbPAY.Text = "0";
                this.tbRest.Text = "0.00";
                this.tbCash.Text = "0.00";
                this.tbDecimal.Text = "0.00";
            }
        }

        /// <summary>
        /// 在修改支付金额之后重新设置支付信息
        /// </summary>
        private decimal setCostInfoAfterEdit()
        {
            //当前现金消费金额
            decimal CashCost = 0.0m;
            int CashRow = -1;

            if (this.hsPayCost == null)
            {
                this.hsPayCost = new Hashtable();
                hsPayCost.Add("TOT", 0.0m);  //套餐金额
                hsPayCost.Add("REAL", 0.0m); //实收金额
                hsPayCost.Add("ETC", 0.0m);  //优惠金额

                hsPayCost.Add("ACTU", 0.0m);  //实际支付
                hsPayCost.Add("GIFT", 0.0m);  //支付金额
                hsPayCost.Add("DEPO", 0.0m);  //押金支付
                hsPayCost.Add("COU", 0.0m); //积分支付
                hsPayCost.Add("ROUND", 0.0m); //四舍五入
            }
            else
            {
                hsPayCost["ACTU"] = 0.0m;   //实际支付
                hsPayCost["GIFT"] = 0.0m;   //赠送金额
                hsPayCost["DEPO"] = 0.0m;   //押金支付
                hsPayCost["COU"] = 0.0m;   //积分支付
                hsPayCost["ROUND"] = 0.0m;  //四舍五入
            }

            foreach (Row row in this.fpPayMode_Sheet1.Rows)
            {
                FS.HISFC.Models.Base.Const curPayMode = row.Tag as FS.HISFC.Models.Base.Const;
                if (this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value != null)
                {
                    decimal cost = Decimal.Parse(this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value.ToString());

                    //赠送,代付赠送
                    if (curPayMode.ID == "DC" || curPayMode.ID == "OTDC")
                    {
                        this.hsPayCost["GIFT"] = (decimal)this.hsPayCost["GIFT"] + cost;
                    }
                    else if (curPayMode.ID == "CO")
                    {
                        this.hsPayCost["COU"] = (decimal)this.hsPayCost["COU"] + cost;
                    }
                    else if (curPayMode.ID != "RC")  //优惠金额已固定
                    {
                        this.hsPayCost["ACTU"] = (decimal)this.hsPayCost["ACTU"] + cost;
                    }
                }

                if ((row.Tag as FS.HISFC.Models.Base.Const).ID == "CA")
                {
                    CashCost = Decimal.Parse(this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value.ToString());
                    CashRow = row.Index;
                    hsPayCost["ROUND"] = CashCost - FS.HISFC.BizProcess.Integrate.Function.calculateDecimal(CashCost);
                }
            }

            foreach (Row row in this.fpDeposit_Sheet1.Rows)
            {
                if (row.Tag != null && row.Tag is FS.HISFC.Models.MedicalPackage.Fee.Deposit &&
                   (bool)this.fpDeposit_Sheet1.Cells[row.Index,(int)DepositCols.Select].Value)
                {
                    FS.HISFC.Models.MedicalPackage.Fee.Deposit deposit = row.Tag as FS.HISFC.Models.MedicalPackage.Fee.Deposit;
                    this.hsPayCost["DEPO"] = (decimal)this.hsPayCost["DEPO"] + deposit.Amount;
                }
            }

            decimal JugdeCost = 0.0m;
            JugdeCost = (decimal)this.hsPayCost["REAL"] - (decimal)this.hsPayCost["ACTU"] - (decimal)this.hsPayCost["GIFT"] - (decimal)this.hsPayCost["DEPO"] - (decimal)this.hsPayCost["COU"];

            //相等则无需调整现金金额
            if (JugdeCost == 0)
            { 
                return JugdeCost;
            }

            // 差价金额在现金金额可调整的范围内
            if (JugdeCost + CashCost >= 0)
            {
                //设置现金金额 = 原现金金额 + 少缴金额
                decimal realCash = CashCost + JugdeCost;
                this.fpPayMode_Sheet1.Cells[CashRow, (int)PayModeCols.TotCost].Value = realCash;
                return this.setCostInfoAfterEdit();
            }
            else  //不在现金金额可调整的范围内
            {
                if (CashCost > 0)
                {
                    this.fpPayMode_Sheet1.Cells[CashRow, (int)PayModeCols.TotCost].Value = 0;
                    return this.setCostInfoAfterEdit();
                }
                else
                {
                    return JugdeCost;
                }
            }
        }

        /// <summary>
        /// 更新发票
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string errInfo = "";
            FS.FrameWork.Management.PublicTrans.BeginTransaction();


            string invoiceNo = this.tbInvoiceNO.Text;
            if (string.IsNullOrEmpty(invoiceNo))
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("请录入有效发票号！");
                return;
            }

            if (this.UpdateInvoice(invoiceNo, this.tbRealInvoiceNO.Text.Trim(), ref errInfo))
            {
                this.InitInvoice(false);
            }
            else
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(errInfo);
                return;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show(errInfo);
        }
        
        /// <summary>
        /// 收取现金
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbPAY_TextChanged(object sender, EventArgs e)
        {
            string pay = this.tbPAY.Text;
            if (string.IsNullOrEmpty(pay))
            {
                pay = "0";
            }
            try
            {
                decimal payCost = Decimal.Parse(pay);
                this.tbRest.Text = (payCost - Decimal.Parse(this.tbCash.Text)).ToString();
            }
            catch
            {
                this.tbPAY.Text = this.previousStr;
                this.tbPAY.Select(this.tbPAY.TextLength, 0); 
            }
        }

        /// <summary>
        /// 更新操作员发票号信息
        /// </summary>
        /// <param name="invoiceNO"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public bool UpdateInvoice(string invoiceNO, string realInvoiceNO, ref string errInfo)
        {
            realInvoiceNO = realInvoiceNO.PadLeft(10, '0');
            FS.HISFC.Models.Base.Employee oper = this.account.Operator as FS.HISFC.Models.Base.Employee;

            int iRes = feeIntegrate.UpdateNextInvoiceNO(oper.ID, "M", invoiceNO, realInvoiceNO);
            if (iRes <= 0)
            {
                errInfo = feeIntegrate.Err;
                return false;
            }
            
            errInfo = "更新成功！";
            return true; ;
        }

        /// <summary>
        /// 根据精度计算小数位
        /// </summary>
        /// <param name="oldDecimal"></param>
        /// <returns></returns>
        //private decimal calculateDecimal(decimal oldDecimal)
        //{
        //    decimal ShouldDecimal = oldDecimal;
        //    decimal RealDecimal = 0.0m;

        //    //处理四舍五入
        //    if (roundControl < 3)
        //    {
        //        //保留0,1,2位小数
        //        RealDecimal = Math.Round(ShouldDecimal, roundControl, MidpointRounding.AwayFromZero);
        //    }
        //    else if (roundControl == 3)
        //    {
        //        //下取整
        //        RealDecimal = Math.Floor(ShouldDecimal);
        //    }
        //    else
        //    {
        //        //上取整
        //        RealDecimal = Math.Ceiling(ShouldDecimal);
        //    }

        //    return RealDecimal;
        //}

        string previousStr = string.Empty;
        /// <summary>
        /// 按键处理
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            try
            {
                if (this.fpPayMode.ContainsFocus)
                {

                    if (keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Enter)
                    {
                        this.PutArrow(keyData);
                        return true;
                    }
                }

                if(this.tbRealInvoiceNO.ContainsFocus && keyData == Keys.Enter)
                {
                    this.btnUpdate_Click(this.tbRealInvoiceNO, new EventArgs());
                }

                if (this.tbPAY.ContainsFocus)
                {
                    previousStr = this.tbPAY.Text;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Focus();
                this.fpPayMode.Focus();
                return false;
            }
            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// 方向按键
        /// </summary>
        /// <param name="key">当前的按键</param>
        private void PutArrow(Keys key)
        {
            int currCol = this.fpPayMode_Sheet1.ActiveColumnIndex;
            int currRow = this.fpPayMode_Sheet1.ActiveRowIndex;

            if (key == Keys.Right)
            {
                for (int i = 0; i < this.fpPayMode_Sheet1.Columns.Count; i++)
                {
                    if (i > currCol && this.fpPayMode_Sheet1.Cells[currRow, i].Locked == false)
                    {
                        this.fpPayMode_Sheet1.SetActiveCell(currRow, i, false);

                        return;
                    }
                }
            }

            if (key == Keys.Left)
            {
                for (int i = this.fpPayMode_Sheet1.Columns.Count - 1; i >= 0; i--)
                {
                    if (i < currCol && this.fpPayMode_Sheet1.Cells[currRow, i].Locked == false)
                    {
                        this.fpPayMode_Sheet1.SetActiveCell(currRow, i, false);
                        return;
                    }
                }
            }

            if (key == Keys.Up)
            {
                if (currRow > 0)
                {
                    this.fpPayMode_Sheet1.ActiveRowIndex = currRow - 1;
                    this.fpPayMode_Sheet1.SetActiveCell(currRow - 1, this.fpPayMode_Sheet1.ActiveColumnIndex);
                }
            }

            if (key == Keys.Down || key == Keys.Enter)
            {
                if (this.fpPayMode_Sheet1.ActiveRowIndex < this.fpPayMode_Sheet1.RowCount - 1)
                {
                    this.fpPayMode_Sheet1.SetActiveCell(currRow + 1, (int)PayModeCols.Name);
                    this.fpPayMode_Sheet1.SetActiveCell(currRow + 1, (int)PayModeCols.TotCost);
                }
                else
                {
                    this.fpPayMode.StopCellEditing();
                }
            }
        }

        #endregion

        #region 外部调用

        /// <summary>
        /// 获得操作员的当前发票起始号
        /// </summary>
        /// <returns></returns>
        public int InitInvoice(bool isFirst)
        {
            string invoiceNO = ""; 
            string realInvoiceNO = ""; 
            string errText = "";

            FS.HISFC.Models.Base.Employee oper = this.account.Operator as FS.HISFC.Models.Base.Employee;

            #region 增加发票号提示
            if (isFirst)
            {
                FS.HISFC.Components.Common.Forms.frmUpdateInvoice frm = new FS.HISFC.Components.Common.Forms.frmUpdateInvoice();
                frm.InvoiceType = "M";
                frm.ShowDialog(this);
            }
            if (this.feeIntegrate.GetInvoiceNO(oper, "M", ref invoiceNO, ref realInvoiceNO, ref errText) == -1)
            {
                MessageBox.Show(errText);
                return -1;
            }
            #endregion

            //显示当前发票号
            this.tbRealInvoiceNO.Text = realInvoiceNO;
            this.tbInvoiceNO.Text = invoiceNO;
            return 0;
        }

        /// <summary>
        /// 清空界面信息
        /// </summary>
        public void Clear()
        {
            this.fpDeposit.StopCellEditing();
            this.fpPayMode.StopCellEditing();
            this.PatientInfo = null;
            this.HsPayCost = null;
            this.InitInvoice(false);
        }

        /// <summary>
        /// 防止还在编辑的过程中进行保存
        /// </summary>
        public void Commit()
        {
            this.fpDeposit.StopCellEditing();
            this.fpPayMode.StopCellEditing();
        }

        /// <summary>
        /// 获取支付信息
        /// </summary>
        /// <returns></returns>
        public ArrayList GetPayModeInfo()
        {
            ArrayList paymodeList = new ArrayList();
            foreach (Row row in this.fpPayMode_Sheet1.Rows)
            {
                FS.HISFC.Models.Base.Const curPayMode = this.fpPayMode_Sheet1.Rows[row.Index].Tag as FS.HISFC.Models.Base.Const;

                if (curPayMode.ID != "YS" && curPayMode.ID != "DC" && curPayMode.ID != "OTYS" && curPayMode.ID != "OTDC")
                {
                    FS.HISFC.Models.MedicalPackage.Fee.PayMode payMode = new FS.HISFC.Models.MedicalPackage.Fee.PayMode();
                    payMode.Mode_Code = curPayMode.ID;
                    payMode.Cancel_Flag = "0";
                    payMode.Trans_Type = "1";
                    payMode.Tot_cost = payMode.Real_Cost = Decimal.Parse(this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value.ToString());
                    payMode.Memo = this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.Memo].Text;
                    if (curPayMode.ID == "YE" && Decimal.Parse(this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value.ToString())>0)//{B7E5CA30-EC53-4fea-BB60-55B8D8DE3CAB} 判断应收其他是否填写了备注信息
                    {
                        if (string.IsNullOrEmpty(payMode.Memo))
                        {
                            MessageBox.Show("支付方式是应收其他则必须填写备注信息！");
                            return null;
                        }
                    }
                    //现金支付需要进行四舍五入
                    if (payMode.Mode_Code == "CA")
                    {
                        decimal tmp = FS.HISFC.BizProcess.Integrate.Function.calculateDecimal(payMode.Tot_cost);
                        payMode.Tot_cost = payMode.Real_Cost = tmp;

                    }

                    if (payMode.Tot_cost > 0)
                    {
                        paymodeList.Add(payMode);
                    }
                }
            }

            paymodeList.AddRange(this.accountPay);
            paymodeList.AddRange(this.giftPay);
            paymodeList.AddRange(this.otherAccountPay);
            paymodeList.AddRange(this.otherGiftPay);

            return paymodeList;
        }

        /// <summary>
        /// 获取押金信息
        /// </summary>
        /// <returns></returns>
        public ArrayList GetDepositInfo()
        {
            ArrayList depositList = new ArrayList();
            foreach (Row row in this.fpDeposit_Sheet1.Rows)
            {
                if ((bool)this.fpDeposit_Sheet1.Cells[row.Index, (int)DepositCols.Select].Value == true)
                {
                    FS.HISFC.Models.MedicalPackage.Fee.Deposit deposit = this.fpDeposit_Sheet1.Rows[row.Index].Tag as FS.HISFC.Models.MedicalPackage.Fee.Deposit;
                    depositList.Add(deposit);
                }
            }
            return depositList;
        }

        #endregion

        /// <summary>
        /// 列枚举
        /// </summary>
        private enum PayModeCols
        {
            /// <summary>
            /// 支付方式
            /// </summary>
            Name = 0,

            /// <summary>
            /// 总金额
            /// </summary>
            TotCost = 1,

            /// <summary>
            /// 备注
            /// </summary>
            Memo = 2
        }

        /// <summary>
        /// 列枚举
        /// </summary>
        private enum DepositCols
        {
            /// <summary>
            /// 选择框
            /// </summary>
            Select = 0,

            /// <summary>
            /// 单据号
            /// </summary>
            ID = 1,

            /// <summary>
            /// 支付方式
            /// </summary>
            PayMode = 2,

            /// <summary>
            /// 金额
            /// </summary>
            Amount = 3,

            /// <summary>
            /// 备注
            /// </summary>
            Memo = 4
        }
    }
}
