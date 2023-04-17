using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Collections.Generic;
using FS.HISFC.Models.Fee.Outpatient;
using FS.FrameWork.Models;
using FS.FrameWork.Management;
using FS.FrameWork.Function;
using FarPoint.Win.Spread;
using FS.HISFC.BizProcess.Interface.FeeInterface;
using FS.HISFC.Models.MedicalPackage.Fee;

namespace FS.HISFC.Components.OutpatientFee.Forms
{
    /// <summary>
    /// 门诊收费结算
    /// </summary>
    public partial class frmDealBalanceNew : Form, FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientPopupFee
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public frmDealBalanceNew()
        {
            //{DCE8B897-7F98-45c1-B953-DC0B5F0A55F5}
            //因为存在收费完成后，收费记录还在（生成了两条完全相同的feedetail），所以先把划价保存禁用
            InitializeComponent();
        }

        #region IOutpatientPopupFee 成员

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
        /// 银联接口
        /// </summary>
        public FS.HISFC.BizProcess.Interface.FeeInterface.IBankTrans BankTrans
        {
            get
            {
                return this.bankTrans;
            }

            set
            {
                this.bankTrans = value;
            }
        }

        public bool IsAutoBankTrans
        {
            set
            {
                isAutoBankTrans = value;
            }
        }

        /// <summary>
        /// 是否现金冲账
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
        /// 退费的时候判断是否点的取消
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
        /// 是否退费
        /// </summary>
        public bool IsQuitFee
        {
            set
            {
                this.isQuitFee = value;
                if (this.isQuitFee)
                {
                    this.tbCharge.Enabled = false;

                }
            }
        }

        /// <summary>
        /// 是否成功收费，未退出
        /// </summary>
        public bool IsSuccessFee
        {
            get
            {
                return this.isSuccessFee;
            }
            set
            {
                this.isSuccessFee = value;
            }
        }

        /// <summary>
        /// 划价按钮触发
        /// </summary>
        public event FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateChangeSomething ChargeButtonClicked;

        /// <summary>
        /// 收费按钮触发
        /// </summary>
        public event FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateFee FeeButtonClicked;

        /// <summary>
        /// 外屏显示实收金额，应找金额
        /// </summary>
        public event FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateRealCost RealCostChange;


        /// <summary>
        /// 门诊患者挂号信息
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
        /// 患者合同单位信息
        /// </summary>
        public FS.HISFC.Models.Base.PactInfo PactInfo
        {
            set
            {
                this.pactInfo = value;

                //分发票都不显示
                if (this.pactInfo.PayKind.ID == "01")
                {
                    //this.tpSplitInvoice.Show(); //自费患者可以分票

                    this.tpSplitInvoice.Hide();  //默认都隐藏
                }
                else
                {
                    this.tpSplitInvoice.Hide();
                }
            }
        }


        /// <summary>
        /// 门诊费用明细集合
        /// </summary>
        public System.Collections.ArrayList FeeDetails
        {
            set
            {
                this.alFeeDetails = value;
            }
            get
            {
                return this.alFeeDetails;
            }
        }

        /// <summary>
        /// 主发票和发票明细集合【废弃】
        /// </summary>
        public System.Collections.ArrayList InvoiceAndDetails
        {
            set
            {
                throw new NotImplementedException();
            }
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 发票明细集合
        /// </summary>
        public System.Collections.ArrayList InvoiceDetails
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
        /// 发票费用明细集合【保存按发票分组的费用明细】
        /// </summary>
        public System.Collections.ArrayList InvoiceFeeDetails
        {
            get
            {
                return this.alInvoiceFeeDetails;
            }
            set
            {
                this.alInvoiceFeeDetails = value;
            }
        }

        /// <summary>
        /// 主发票集合
        /// </summary>
        public System.Collections.ArrayList Invoices
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
                        //费用明细【保存按发票分组的费用明细】
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
        /// 总金额
        /// </summary>
        public decimal TotCost
        {
            set
            {
                this.totCost = value;
                this.tbTotCost.Text = this.totCost.ToString();
            }

            get
            {
                return this.totCost;
            }
        }

        /// <summary>
        /// 自费金额
        /// </summary>
        public decimal OwnCost
        {
            set
            {
                this.ownCost = value;
                this.tbOwnCost.Text = this.ownCost.ToString();
            }
            get
            {
                return this.ownCost;
            }
        }

        /// <summary>
        /// 自付金额
        /// </summary>
        public decimal PayCost
        {
            set
            {
                this.payCost = value;
                //this.zsCoupon.Text = payCost.ToString();
            }

            get
            {
                return this.payCost;
            }
        }

        /// <summary>
        /// 医保金额
        /// </summary>
        public decimal PubCost
        {
            set
            {
                pubCost = value;
                this.tbPubCost.Text = pubCost.ToString();
            }

            get
            {
                return this.pubCost;
            }
        }

        /// <summary>
        /// 减免金额 【通过待遇算法处理，可能产生减免费用】
        /// </summary>
        public decimal RebateRate
        {
            get
            {
                return this.rebateRate;
            }

            set
            {
                this.rebateRate = value;
                this.tbEcoCost.Text = this.rebateRate.ToString("F2");
            }
        }


        /// <summary>
        /// 应缴金额-》自费总额 = 自费金额 + 自付金额 【Own_Cost+Pay_Cost】
        /// ??gmz??
        /// </summary>
        public decimal TotOwnCost
        {
            get
            {
                return this.totOwnCost;
            }

            set
            {
                this.totOwnCost = value;
                this.tbRealOwnCost.Text = this.totOwnCost.ToString();

                this.payModeFpCost = 0;

                //设置金额
                this.SetCost();
            }
        }

        /// <summary>
        /// 实付金额
        /// </summary>
        public decimal RealCost
        {
            set
            {
                this.realCost = value;
            }
        }

        /// <summary>
        /// 找零金额
        /// </summary>
        public decimal LeastCost
        {
            set
            {
                this.leastCost = value;
            }
        }



        /// <summary>
        /// 超标药金额
        /// </summary>
        public decimal OverDrugCost
        {
            set
            {
                this.overDrugCost = value;
            }
        }

        /// <summary>
        /// 自费药金额
        /// </summary>
        public decimal SelfDrugCost
        {
            set
            {
                this.selfDrugCost = value;
            }
        }

        /// <summary>
        /// 套餐匹配后自动计算的优惠金额
        /// </summary>
        private decimal packageDiscount = 0.0m;



        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public int Init()
        {
            //添加事件
            this.AddEvent();
            //隐藏该控件
            this.tpSplitInvoice.Hide();

            #region 事务

            if (this.trans != null)
            {
                this.managerIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.controlParam.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.accountMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.packageMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            }

            #endregion

            this.InitFp();

            //初始化支付方式信息
            this.alPayModes = this.managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PAYMODES);
            if (this.alPayModes == null || this.alPayModes.Count <= 0)
            {
                MessageBox.Show("获取支付方式错误!");
                return -1;
            }
            this.InitPayMode();
            this.helpPayMode.ArrayObject = alPayModes;

            //初始化银行信息
            this.alBanks = this.managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.BANK);
            if (this.alBanks == null || this.alBanks.Count <= 0)
            {
                MessageBox.Show("获取银行列表失败!");
                return -1;
            }
            this.InitBanks();
            this.helpBank.ArrayObject = alBanks;

            //初始化分发票信息
            this.InitSplitInvoice();

            //初始化最小费用列表
            ArrayList alMinFeeList = this.managerIntegrate.GetConstantList("MINFEE");
            if (alMinFeeList != null)
            {
                this.helpMinFeeList.ArrayObject = alMinFeeList;
            }

            #region 控制参数

            string tempControlValue = this.feeIntegrate.GetControlValue(FS.HISFC.BizProcess.Integrate.Const.CANUSEMCARND, "0");
            this.isSICanUserCardPayAll = NConvert.ToBoolean(tempControlValue);


            string modifyDate = this.feeIntegrate.GetControlValue(FS.HISFC.BizProcess.Integrate.Const.MODIFY_INVOICE_PRINTDATE, "0");
            this.isModifyDate = NConvert.ToBoolean(modifyDate);
            if (this.isModifyDate)
            {
                this.dateTimePicker1.Enabled = true;
            }
            else
            {
                this.dateTimePicker1.Enabled = false;
            }

            this.isDisplayCashOnly = NConvert.ToBoolean(this.feeIntegrate.GetControlValue(FS.HISFC.BizProcess.Integrate.Const.CASH_ONLY_WHENFEE, "0"));

            this.isDealCentNoAllCancy = this.controlParam.GetControlParam<bool>("MZ9932", true, false);

            //{333D2AD8-DC4A-4c30-A14E-D6815AC858F9}
            this.IsCouponModuleInUse = this.controlParam.GetControlParam<bool>("CP0001", true, false);

            #endregion

            //初始化剩余积分/赠送积分
            RefreshCoupon();
            return 1;

        }
        //{283BD55B-83E0-4f35-86E8-152CF0C1DA85} 剩余积分的显示
        private void RefreshCoupon()
        {
            if (string.IsNullOrEmpty(this.rInfo.PID.CardNO))
            {
                return;
            }
            //剩余积分
            decimal couponAmount = 0.0m;
            string resultCode = "0";
            string errorMsg = "";
            Dictionary<string, string> dic = FS.HISFC.BizProcess.Integrate.WSHelper.QueryAccountInfo(this.rInfo.PID.CardNO, out resultCode, out errorMsg);

            if (dic == null)
            {
                MessageBox.Show("查询账户出错:" + errorMsg);
                return;
            }

            if (dic.ContainsKey("couponvacancy"))
            {
                couponAmount = decimal.Parse(dic["couponvacancy"].ToString());
            }
            //剩余积分=原来积分+赠送积分
            this.zsCoupon.Text = couponAmount.ToString("F2");

        }

        /// <summary>
        /// 控制焦点
        /// </summary>
        public void SetControlFocus()
        {
            this.panel1.Focus();
            this.groupBox2.Focus();
            this.tbRealCost.Focus();
        }

        /// <summary>
        /// 划价保存
        /// </summary>
        /// <returns>成功 true 失败 false</returns>
        public bool SaveCharge()
        {
            this.ChargeButtonClicked();

            return true;
        }

        /// <summary>
        /// 收费保存
        /// </summary>
        /// <returns>成功 ture 失败 false</returns>
        public bool SaveFee()
        {
            string errText = string.Empty;
            int errRow = 0;
            int errCol = 0;

            this.GetFT();
            if (!this.IsPayModesValid(ref errText, ref errRow, ref errCol))
            {
                MessageBox.Show(errText, "提示");
                this.fpPayMode.Focus();
                this.fpPayMode_Sheet1.SetActiveCell(errRow, errCol, false);

                return false;
            }

            //获取支付方式
            this.alPatientPayModeInfo = this.QueryBalancePays();
            if (alPatientPayModeInfo == null)
            {
                MessageBox.Show("获得支付方式信息出错!", "提示");
                return false;
            }

            if (!this.IsSplitInvoicesValid())
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


        #endregion


        #region 变量和属性

        #region 业务变量

        /// <summary>
        /// 管理业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 费用业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 控制参数业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParam = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// 账户管理类
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();

        /// <summary>
        /// 套餐业务管理类
        /// </summary>
        private FS.HISFC.BizLogic.MedicalPackage.Fee.Package packageMgr = new FS.HISFC.BizLogic.MedicalPackage.Fee.Package();

        #endregion

        /// <summary>
        /// 数据库连接
        /// </summary>
        protected FS.FrameWork.Management.Transaction trans = null;

        /// <summary>
        /// 银联接口
        /// </summary>
        private FS.HISFC.BizProcess.Interface.FeeInterface.IBankTrans bankTrans = null;

        private bool isAutoBankTrans = false;

        /// <summary>
        /// 是否现金冲账
        /// </summary>
        protected bool isCashPay = false;

        /// <summary>
        /// 退费的时候判断是否点的取消
        /// </summary>
        protected bool isPushCancelButton = false;

        /// <summary>
        /// {333D2AD8-DC4A-4c30-A14E-D6815AC858F9}
        /// 积分模块是否启用
        /// </summary>
        private bool IsCouponModuleInUse = false;

        /// <summary>
        /// 是否退费调用
        /// </summary>
        protected bool isQuitFee = false;

        /// <summary>
        /// 是否成功收费，未退出
        /// </summary>
        private bool isSuccessFee = false;

        /// <summary>
        /// 门诊患者挂号信息
        /// </summary>
        protected FS.HISFC.Models.Registration.Register rInfo = new FS.HISFC.Models.Registration.Register();

        /// <summary>
        /// 合同单位信息
        /// </summary>
        protected FS.HISFC.Models.Base.PactInfo pactInfo = new FS.HISFC.Models.Base.PactInfo();

        /// <summary>
        /// 费用明细集合
        /// </summary>
        protected ArrayList alFeeDetails = new ArrayList();

        /// <summary>
        /// 发票明细集合
        /// </summary>
        protected ArrayList alInvoiceDetails = new ArrayList();

        /// <summary>
        /// 发票费用明细集合【保存按发票分组的费用明细】
        /// </summary>
        private ArrayList alInvoiceFeeDetails = new ArrayList();

        /// <summary>
        /// 主发票集合
        /// </summary>
        protected ArrayList alInvoices = new ArrayList();

        /// <summary>
        /// 收费信息
        /// </summary>
        private FS.HISFC.Models.Base.FT ftFeeInfo = new FS.HISFC.Models.Base.FT();

        /// <summary>
        /// 总金额
        /// </summary>
        protected decimal totCost;

        /// <summary>
        /// 自费总额 = 自费金额 + 自付金额 【Own_Cost+Pay_Cost】
        /// </summary>
        protected decimal totOwnCost;

        /// <summary>
        /// 自费金额
        /// </summary>
        protected decimal ownCost;

        /// <summary>
        /// 自付金额
        /// </summary>
        protected decimal payCost;

        /// <summary>
        /// 医保金额
        /// </summary>
        protected decimal pubCost;

        /// <summary>
        /// 实付金额
        /// </summary>
        protected decimal realCost;

        /// <summary>
        /// 减免金额【通过待遇算法处理，可能产生减免费用】 
        /// </summary>
        protected decimal rebateRate;

        /// <summary>
        /// 找零金额
        /// </summary>
        protected decimal leastCost;

        /// <summary>
        /// 超标药金额
        /// </summary>
        protected decimal overDrugCost;

        /// <summary>
        /// 自费药金额
        /// </summary>
        protected decimal selfDrugCost;

        /// <summary>
        /// 在FP选择支付方式金额
        /// </summary>
        private decimal payModeFpCost;

        #region 控件

        /// <summary>
        /// 银行选择列表
        /// </summary>
        private FS.FrameWork.WinForms.Controls.PopUpListBox lbBank = new FS.FrameWork.WinForms.Controls.PopUpListBox();

        #endregion

        /// <summary>
        /// 支付方式列表
        /// </summary>
        protected ArrayList alPayModes = new ArrayList();

        /// <summary>
        /// 支付方式信息
        /// </summary>
        protected ArrayList alPatientPayModeInfo = new ArrayList();

        /// <summary>
        /// 银行表
        /// </summary>
        protected ArrayList alBanks = new ArrayList();

        /// <summary>
        /// payMode列表查询
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper helpPayMode = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 银行列表查询
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper helpBank = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 最小费用列表查询
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper helpMinFeeList = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 现金支付方式
        /// </summary>
        private int rowCA = -1;

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

        /// <summary>
        /// 是否可以分发票
        /// </summary>
        protected bool isCanSplit;

        /// <summary>
        /// 最多分发票张数
        /// </summary>
        protected int splitCounts;

        /// <summary>
        /// 医保可以用
        /// </summary>
        protected bool isSICanUserCardPayAll = false;

        /// <summary>
        /// 收费时应缴只显示现金金额
        /// </summary>
        protected bool isDisplayCashOnly = false;

        /// <summary>
        /// 是否可以修改发票打印日期
        /// </summary>
        protected bool isModifyDate = false;

        /// <summary>
        /// 当包含有非现金支付方式的是否处理四舍五入(默认true)
        /// </summary>
        private bool isDealCentNoAllCancy = true;

        /// <summary>
        /// 支付方式是否输入成功
        /// </summary>
        protected bool isPaySuccess = false;

        #endregion

        #region 方法

        /// <summary>
        /// 添加事件
        /// </summary>
        private void AddEvent()
        {
            this.tbFee.Click += new System.EventHandler(this.tbFee_Click);
            this.tbCharge.Click += new System.EventHandler(this.tbCharge_Click);
            this.tbCancel.Click += new System.EventHandler(this.tbCancel_Click);

            this.btnSplit.Click += new System.EventHandler(this.btnSplit_Click);
            this.tbDefault.Click += new System.EventHandler(this.tbDefault_Click);

            this.btAccount.Click += new System.EventHandler(this.btAccount_Click);
            this.btEmpower.Click += new System.EventHandler(this.btEmpower_Click);
            this.btPackage.Click += new System.EventHandler(this.btPackage_Click);
            this.btnDiscountCard.Click += new System.EventHandler(this.btDiscountCard_Click);

            //{333D2AD8-DC4A-4c30-A14E-D6815AC858F9}
            this.btCoupon.Click += new EventHandler(btCoupon_Click);

            this.fpPayMode.EnterCell += new FarPoint.Win.Spread.EnterCellEventHandler(this.fpPayMode_EnterCell);
            this.fpPayMode.EditModeOn += new System.EventHandler(this.fpPayMode_EditModeOn);
            this.fpPayMode.EditChange += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.fpPayMode_EditChange);
            this.fpPayMode_Sheet1.CellChanged += new FarPoint.Win.Spread.SheetViewEventHandler(this.fpPayMode_Sheet1_CellChanged);

            this.fpSplit.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpSplit_CellDoubleClick);


            this.tbRealCost.TextChanged += new System.EventHandler(this.tbRealCost_TextChanged);
            this.tbRealCost.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbRealCost_KeyDown);

            this.tbCount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbCount_KeyDown);
            this.tbSplitDay.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSplitDay_KeyDown);

            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);

            this.tbLeast.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbLeast_KeyDown);

            this.tbEcoCost.TextChanged += new System.EventHandler(this.tbEcoCost_TextChanged);
        }

        /// <summary>
        /// 初始化farpoint,屏蔽一些热键
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
        /// 初始化支付方式信息
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        private int InitPayMode()
        {
            //HIS中维护的支付方式
            for (int i = 0; i < this.alPayModes.Count; i++)
            {
                NeuObject obj = alPayModes[i] as NeuObject;

                //现金支付
                if (obj.ID == "CA")
                {
                    this.rowCA = i;
                }

                //ACY(帐户支付)
                if ((obj as Models.Base.Const).UserCode == "ACY")
                {
                    this.ycObj = obj;
                }
                //ACD(帐户赠送)
                if ((obj as Models.Base.Const).UserCode == "ACD")
                {
                    this.dcObj = obj;
                }

                //ECO(优惠)
                if ((obj as Models.Base.Const).UserCode == "ECO")
                {
                    this.rcObj = obj;
                }

                //PR(套餐实收支付方式)
                if ((obj as Models.Base.Const).UserCode == "PR")
                {
                    this.prObj = obj;
                }

                //PD(套餐赠送支付方式)
                if ((obj as Models.Base.Const).UserCode == "PD")
                {
                    this.pdObj = obj;
                }

                //PY(套餐优惠支付方式)
                if ((obj as Models.Base.Const).UserCode == "PY")
                {
                    this.pyObj = obj;
                }

                //DS(购物卡支付方式)
                if ((obj as Models.Base.Const).UserCode == "DS")
                {
                    this.dsObj = obj;
                }

            }
            if (this.rowCA < 0)
            {
                MessageBox.Show("请联系信息科维护现金支付方式CA!");
            }

            //支付方式赋值
            this.fpPayMode_Sheet1.Rows.Count = 0;
            for (int i = 0; i < this.alPayModes.Count; i++)
            {
                NeuObject obj = alPayModes[i] as NeuObject;
                //ACY(帐户支付)；ACD(帐户赠送)；ECO(优惠)；PR(套餐实收)；PD(套餐赠送)；PY(套餐优惠)；DS(购物卡支付)； Memo为false也不显示
                if ((obj as Models.Base.Const).UserCode == "ACY" ||
                    (obj as Models.Base.Const).UserCode == "ACD" ||
                    (obj as Models.Base.Const).UserCode == "ECO" ||
                    (obj as Models.Base.Const).UserCode == "PR" ||
                    (obj as Models.Base.Const).UserCode == "PD" ||
                    (obj as Models.Base.Const).UserCode == "PY" ||
                    (obj as Models.Base.Const).UserCode == "DS" ||
                    !NConvert.ToBoolean((obj as Models.Base.Const).Memo))
                {
                    continue;
                }

                //增加1行
                this.fpPayMode_Sheet1.Rows.Count++;

                int rowIndex = this.fpPayMode_Sheet1.Rows.Count - 1;

                this.fpPayMode_Sheet1.Cells[rowIndex, (int)PayModeCols.PayMode].Text = obj.Name;   //支付方式名字
                this.fpPayMode_Sheet1.Cells[rowIndex, (int)PayModeCols.PayMode].Tag = obj.ID;     //支付方式编码
                this.fpPayMode_Sheet1.Cells[rowIndex, (int)PayModeCols.PayMode].Locked = true;

                if (obj.ID == "CO" ||
                   obj.ID == "TCO" ||
                   obj.ID == "ECO")
                {
                    this.fpPayMode_Sheet1.Rows[rowIndex].Locked = true;
                }
            }

            return 1;
        }

        /// <summary>
        /// 初始化银行信息
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        private int InitBanks()
        {
            this.lbBank.AddItems(alBanks);
            this.Controls.Add(lbBank);
            this.lbBank.Hide();
            this.lbBank.BorderStyle = BorderStyle.FixedSingle;
            this.lbBank.BringToFront();
            this.lbBank.SelectItem += new FS.FrameWork.WinForms.Controls.PopUpListBox.MyDelegate(this.lbBank_SelectItem);

            return 1;
        }

        /// <summary>
        /// 处理银行信息
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        private int ProcessPayBank()
        {
            if (this.lbBank.Visible == false)
            {
                return -1;
            }
            int currRow = this.fpPayMode_Sheet1.ActiveRowIndex;
            if (currRow < 0)
            {
                return 0;
            }
            NeuObject item = null;
            int returnValue = this.lbBank.GetSelectedItem(out item);
            if (returnValue == -1)
            {
                return -1;
            }
            if (item == null)
            {
                return -1;
            }

            this.fpPayMode.StopCellEditing();
            this.fpPayMode_Sheet1.SetValue(currRow, (int)PayModeCols.Bank, item.Name);
            this.lbBank.Visible = false;

            return 1;
        }

        /// <summary>
        /// 初始化分发票信息
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        private int InitSplitInvoice()
        {
            string tmpCtrlValue = this.feeIntegrate.GetControlValue(FS.HISFC.BizProcess.Integrate.Const.CANSPLIT, "0");
            if (tmpCtrlValue == null || tmpCtrlValue == "-1" || tmpCtrlValue == string.Empty)
            {
                MessageBox.Show("是否分发票参数没有维护，现在采用默认值: 不可分发票!");
                tmpCtrlValue = "0";
            }

            this.isCanSplit = NConvert.ToBoolean(tmpCtrlValue);

            this.rbAuto.Enabled = this.isCanSplit;
            this.rbMun.Enabled = this.isCanSplit;
            this.tbCount.Enabled = this.isCanSplit;
            this.btnSplit.Enabled = this.isCanSplit;
            this.tbDefault.Enabled = this.isCanSplit;

            this.splitCounts = this.controlParam.GetControlParam<int>(FS.HISFC.BizProcess.Integrate.Const.SPLITCOUNTS, false, 9);

            //不可以修改发票日期
            bool isCanModifyInvoiceDate = this.controlParam.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.CAN_MODIFY_INVOICE_DATE, false, false);
            if (!isCanModifyInvoiceDate)
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
        /// 计算金额
        /// </summary>
        /// <returns>成功 ture 失败 false</returns>
        private bool ComputCost()
        {
            decimal realCode = this.GetPayModeFpCost();
            decimal tmpCost = 0;
            for (int i = 0; i < this.fpPayMode_Sheet1.RowCount; i++)
            {
                tmpCost += NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value);
                if (tmpCost > realCode)
                {
                    MessageBox.Show("单项金额不能大于可拆分自费金额!");
                    this.fpPayMode.Focus();
                    this.fpPayMode_Sheet1.SetActiveCell(i, (int)PayModeCols.Cost, false);

                    return false;
                }
            }

            return true;
        }




        /// <summary>
        /// 设置支付方式金额
        /// </summary>
        /// <returns></returns>
        private int SetCost()
        {
            //1、优惠金额
            this.rebateRate = NConvert.ToDecimal(this.tbEcoCost.Text);
            decimal rebateCost = this.rebateRate;

            //2、会员支付
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




            //3、会员代付
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

            //4、套餐支付
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

                this.lblPackageInfo.Text = string.Format("套餐实付：{0}元；套餐赠送：{1}；套餐优惠：{2}；套餐折后自动优惠金额：{3}", prCost.ToString("F2"), pdCost.ToString("F2"), pyCost.ToString("F2"), packageDiscount.ToString("F2"));

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
            this.payModeFpCost = this.OwnCost - rebateCost - accountCost - empowerCost - packageCost - discountCost;

            try
            {
                this.fpPayMode_Sheet1.CellChanged -= new FarPoint.Win.Spread.SheetViewEventHandler(this.fpPayMode_Sheet1_CellChanged);
                for (int k = 0; k < this.fpPayMode_Sheet1.Rows.Count; k++)
                {
                    this.fpPayMode_Sheet1.Cells[k, (int)PayModeCols.Cost].Text = string.Empty;
                    this.fpPayMode_Sheet1.Cells[k, (int)PayModeCols.PayMode].Locked = true;
                }
                this.fpPayMode_Sheet1.CellChanged += new FarPoint.Win.Spread.SheetViewEventHandler(this.fpPayMode_Sheet1_CellChanged);

                this.fpPayMode_Sheet1.Cells[this.rowCA, (int)PayModeCols.Cost].Text = this.payModeFpCost.ToString();
                this.fpPayMode_Sheet1.Cells[this.rowCA, (int)PayModeCols.PayMode].Locked = true;
            }
            catch (Exception ex) { }

            return 1;
        }

        /// <summary>
        /// 获取FP支付方式的金额
        /// </summary>
        /// <returns></returns>
        private decimal GetPayModeFpCost()
        {
            //1、优惠金额
            this.rebateRate = NConvert.ToDecimal(this.tbEcoCost.Text);
            decimal rebateCost = this.rebateRate;

            //2、会员支付
            decimal accountCost = NConvert.ToDecimal(this.tbAccountCost.Text);

            //3、会员代付
            decimal empowerCost = NConvert.ToDecimal(this.tbEmpowerCost.Text);

            //4、套餐支付
            decimal packageCost = NConvert.ToDecimal(this.tbPackageCost.Text);

            //5、积分支付
            decimal couponCost = NConvert.ToDecimal(this.tbCouponCost.Text);

            //6、购物卡支付
            decimal disCountCost = NConvert.ToDecimal(this.tbDiscountCost.Text);

            //7、FP的支付方式选择
            this.payModeFpCost = this.OwnCost - rebateCost - accountCost - empowerCost - packageCost - couponCost - disCountCost;

            return this.payModeFpCost;
        }

        /// <summary>
        /// 设置FP的Cost值 【将CA金额赋值给选择的支付方式】
        /// </summary>
        /// <param name="column"></param>
        private void SetCostValue(int column)
        {
            if (column == (int)PayModeCols.Cost)
            {
                try
                {
                    if (NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.ActiveRowIndex, (int)PayModeCols.Cost].Value) > 0)
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
                                if (this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text == "现金")
                                {
                                    CAcost += FS.FrameWork.Function.NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value);
                                    this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value = 0;
                                }
                            }
                        }
                        catch { }

                    }

                    if (CAcost > 0)
                    {
                        this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.ActiveRowIndex, (int)PayModeCols.Cost].Value = CAcost;
                    }
                }
                catch { }
            }
        }

        private void EditingControl_KeyDown(object sender, KeyEventArgs e)
        {

        }

        /// <summary>
        /// 设置下来列表的显示位置
        /// </summary>
        private void SetLocation()
        {
            if (this.fpPayMode_Sheet1.ActiveColumnIndex == (int)PayModeCols.Bank)
            {
                Control cell = this.fpPayMode.EditingControl;
                this.lbBank.Location = new Point(this.fpPayMode.Location.X + cell.Location.X + 4,
                    this.panel1.Location.Y + this.tabControl1.Location.Y + this.fpPayMode.Location.Y + cell.Location.Y + cell.Height * 2 + SystemInformation.Border3DSize.Height * 2);

                this.lbBank.Size = new Size(cell.Width + 200 + SystemInformation.Border3DSize.Width * 2, 150);

                if (this.lbBank.Location.Y + this.lbBank.Height > this.fpPayMode.Location.Y + this.fpPayMode.Height)
                {
                    this.lbBank.Location = new Point(this.fpPayMode.Location.X + cell.Location.X + 4,
                        this.panel1.Location.Y + this.tabControl1.Location.Y + this.fpPayMode.Location.Y + cell.Location.Y + cell.Height * 2 + SystemInformation.Border3DSize.Height * 2
                        - this.lbBank.Size.Height - cell.Height);
                }
            }
        }

        /// <summary>
        /// 发票预览
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
                iInvoicePrint.SetPrintValue(this.rInfo, invoicePreView, invoiceDetailsPreview, this.alFeeDetails, this.alPatientPayModeInfo, true);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }

            FS.FrameWork.WinForms.Classes.Function.PopShowControl((Control)iInvoicePrint);
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
            //验证金额;
            decimal tempCost = 0;
            decimal cardPayCost = 0;//医保卡支付金额
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
                if (string.IsNullOrEmpty(tempId))
                {
                    errText = "支付方式输入错误!";
                    errRow = i;
                    errCol = (int)PayModeCols.PayMode;

                    return false;
                }

                #region MyRegion

                if (tempPayMode == "汇票")
                {
                    if (this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Bank].Text == string.Empty)
                    {
                        errText = "支付方式" + tempPayMode + "没有输入银行信息";
                        errRow = i;
                        errCol = (int)PayModeCols.Bank;

                        return false;
                    }
                    if (this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Account].Text == string.Empty)
                    {
                        errText = "支付方式" + tempPayMode + "没有输入帐号信息";
                        errRow = i;
                        errCol = (int)PayModeCols.Account;

                        return false;
                    }
                }

                if (tempPayMode == "支票")
                {
                    if (this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Account].Text == string.Empty &&
                        NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value.ToString()) > 0)
                    {
                        errText = "支付方式" + tempPayMode + "没有输入帐号信息";
                        errRow = i;
                        errCol = (int)PayModeCols.Account;

                        return false;
                    }
                }

                #endregion

                if (tempPayMode == "保险帐户" || tempPayMode == "社保卡")
                {
                    cardPayCost += NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value.ToString());
                }

                try
                {
                    tempCost += NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("金额输入不合法" + ex.Message);
                    errRow = i;
                    errCol = (int)PayModeCols.Account;

                    return false;
                }
            }

            if (!isSICanUserCardPayAll)
            {
                if (cardPayCost > this.payCost)
                {
                    errText = "医保卡不允许支付自费部分!请验证再输入";
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 验证分发票数据是否合法
        /// </summary>
        /// <returns>成功 true 失败 false</returns>
        private bool IsSplitInvoicesValid()
        {
            decimal tempTotCost = 0;
            for (int i = 0; i < this.fpSplit_Sheet1.RowCount; i++)
            {
                if (this.fpSplit_Sheet1.Cells[i, 2].Text == "总发票")
                {
                    continue;
                }
                try
                {
                    tempTotCost += NConvert.ToDecimal(this.fpSplit_Sheet1.Cells[i, 3].Text) + NConvert.ToDecimal(this.fpSplit_Sheet1.Cells[i, 4].Text) + NConvert.ToDecimal(this.fpSplit_Sheet1.Cells[i, 5].Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("金额输入不合法!" + ex.Message);
                    return false;
                }
            }

            if (FS.FrameWork.Public.String.FormatNumber(tempTotCost, 2) != this.totCost)
            {
                MessageBox.Show("分发票金额与总金额不符!请重新分配!");
                return false;
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

            decimal balancePayCost = 0;

            #region 1、优惠支付方式

            //优惠金额
            this.rebateRate = NConvert.ToDecimal(this.tbEcoCost.Text);
            decimal rebateCost = this.rebateRate;

            decimal compargeCost = 0;
            if (rebateCost > 0)
            {
                balancePay = new BalancePay();

                balancePay.PayType.ID = this.rcObj.ID;
                balancePay.PayType.Name = this.rcObj.Name;

                if (string.IsNullOrEmpty(balancePay.PayType.ID))
                {
                    MessageBox.Show("优惠支付编码为空!", "警告");
                    return null;
                }
                balancePay.FT.TotCost = rebateCost;
                balancePay.FT.RealCost = balancePay.FT.TotCost;

                balancePayCost += balancePay.FT.TotCost;
                compargeCost += balancePay.FT.TotCost;

                balancePays.Add(balancePay);
            }

            if (rebateCost != compargeCost)
            {
                MessageBox.Show("优惠支付方式金额不符!", "警告");
                return null;
            }


            #endregion

            #region 2、会员支付

            decimal accountCost = NConvert.ToDecimal(this.tbAccountCost.Text);

            compargeCost = 0;
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

                            balancePayCost += balancePay.FT.TotCost;
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

            #region 3、会员代付

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

                            balancePayCost += balancePay.FT.TotCost;
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

            #region 4、套餐支付

            decimal packageCost = NConvert.ToDecimal(this.tbPackageCost.Text);

            bool isAddCostDetail = false;
            compargeCost = 0;
            if (packageCost > 0 || this.lblPackageInfo.Tag != null)
            {
                List<BalancePay> accPayList = this.tbPackageCost.Tag as List<BalancePay>;
                if (accPayList != null && accPayList.Count > 0)
                {
                    foreach (BalancePay bp in accPayList)
                    {
                        if (bp != null && !string.IsNullOrEmpty(bp.PayType.ID))
                        {
                            balancePay = bp.Clone();

                            balancePayCost += balancePay.FT.TotCost;
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

            #region 5、会员支付
            //{333D2AD8-DC4A-4c30-A14E-D6815AC858F9}

            decimal couponCost = NConvert.ToDecimal(this.tbCouponCost.Text);

            compargeCost = 0;
            if (couponCost > 0)
            {
                List<BalancePay> accPayList = this.tbCouponCost.Tag as List<BalancePay>;
                if (accPayList != null && accPayList.Count > 0)
                {
                    foreach (BalancePay bp in accPayList)
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
            if (couponCost != compargeCost)
            {
                MessageBox.Show("会员支付方式金额不符!", "警告");
                return null;
            }

            #endregion

            #region 6、购物卡支付

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

            #region 7、FP支付方式

            this.payModeFpCost = this.GetPayModeFpCost();
            compargeCost = 0;
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
                if (NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value) == 0)
                {
                    continue;
                }

                //支付方式
                balancePay = new BalancePay();
                balancePay.PayType.Name = this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text;
                balancePay.PayType.ID = this.helpPayMode.GetID(balancePay.PayType.Name);
                if (balancePay.PayType.Name == "应收其他" && NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value.ToString()) > 0)//{B7E5CA30-EC53-4fea-BB60-55B8D8DE3CAB} 判断应收其他是否填写了备注信息
                {
                    if (string.IsNullOrEmpty(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Memo].Text))
                    {
                        MessageBox.Show("支付方式是应收其他则必须填写备注信息!");
                        return null;
                    }
                    //else
                    //{
                    //    balancePay.Memo = this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Memo].Text;
                    //    //balancePay.PayType.Memo = this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Memo].Text;

                    //}
                }

                //{82C1F780-8D95-48cc-A337-FDE543ECB239}
                balancePay.Memo = this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Memo].Text;

                if (string.IsNullOrEmpty(balancePay.PayType.ID))
                {
                    return null;
                }
                balancePay.FT.TotCost = NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value.ToString());
                if (balancePay.PayType.Name == "现金")
                {
                    balancePay.FT.RealCost = balancePay.FT.TotCost;
                }
                else
                {
                    balancePay.FT.RealCost = balancePay.FT.TotCost;
                }

                balancePay.Bank.Name = this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Bank].Text;
                balancePay.Bank.ID = this.helpBank.GetID(balancePay.Bank.Name);
                balancePay.Bank.Account = this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Account].Text;

                if (balancePay.PayType.Name == "支票" || balancePay.PayType.Name == "汇票")
                {
                    balancePay.Bank.InvoiceNO = this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PosNo].Text;
                }
                else
                {
                    balancePay.POSNO = this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PosNo].Text;
                }

                balancePayCost += balancePay.FT.TotCost;
                compargeCost += balancePay.FT.TotCost;

                balancePays.Add(balancePay);
            }

            if (this.payModeFpCost != compargeCost)
            {
                MessageBox.Show("患者其它支付方式金额不符!", "警告");
                return null;
            }

            #endregion

            if (this.totOwnCost != balancePayCost)
            {
                MessageBox.Show("支付方式总金额不等于应缴金额!");
                return null;
            }

            return balancePays;
        }

        /// <summary>
        /// 获得分发票信息
        /// </summary>
        /// <returns>成功 分发票信息 失败 null</returns>
        protected ArrayList QuerySplitInvoices()
        {
            NeuObject obj = null;
            ArrayList objs = new ArrayList();

            if (this.pactInfo.ID == "01")
            {
                //自费分票
                for (int i = 0; i < this.fpSplit_Sheet1.RowCount; i++)
                {
                    obj = new NeuObject();
                    obj.ID = i.ToString();
                    obj.User01 = this.fpSplit_Sheet1.Cells[i, 1].Text;
                    objs.Add(obj);
                }
            }
            else
            {
                //公费和医保
                obj = new NeuObject();
                obj.User01 = ownCost.ToString();
                obj.User02 = payCost.ToString();
                obj.User03 = pubCost.ToString();
                objs.Add(obj);
            }

            return objs;
        }

        /// <summary>
        /// 获得总金额
        /// </summary>
        /// <returns></returns>
        private FS.HISFC.Models.Base.FT GetFT()
        {
            FS.HISFC.Models.Base.FT feeInfo = new FS.HISFC.Models.Base.FT();
            feeInfo.TotCost = totCost;
            feeInfo.OwnCost = ownCost;
            feeInfo.PayCost = payCost;
            feeInfo.PubCost = pubCost;
            feeInfo.BalancedCost = totOwnCost;
            feeInfo.SupplyCost = totOwnCost;
            feeInfo.RealCost = NConvert.ToDecimal(this.tbRealCost.Text);
            feeInfo.ReturnCost = NConvert.ToDecimal(this.tbLeast.Text);
            this.ftFeeInfo = feeInfo;

            return this.ftFeeInfo;
        }

        /// <summary>
        /// 套餐消费支付方式
        /// </summary>
        /// <param name="costPackDetails"></param>
        /// <returns></returns>
        private int SetPackagePayMode(List<PackageDetail> costPackDetails, bool isOld)
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

                //{BD46CBA6-A670-40cf-A1FD-EAFBC9797D04}
                //this.payModeFpCost = this.GetPayModeFpCost();
                if ((prCost + pdCost + pyCost) > this.GetPackageCost())
                {
                    this.tbPackageCost.Text = "0.00";
                    this.tbPackageCost.Tag = null;
                    this.lblPackageInfo.Tag = null;
                    this.SetCost();

                    MessageBox.Show(string.Format("套餐消费的金额【{0}】大于套餐项目总金额{1}】", (prCost + pdCost + pyCost).ToString(), this.GetPackageCost(), "警告"));
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


                //存在单独核销金额为0的情况
                if (bpList.Count == 0 && costPackDetails.Count > 0)
                {
                    BalancePay bp = new BalancePay();
                    bp.PayType.ID = this.pyObj.ID;
                    bp.PayType.Name = this.pyObj.Name;
                    bp.FT.TotCost = 0;
                    bp.FT.RealCost = 0;
                    bpList.Add(bp);
                }


                this.tbPackageCost.Tag = bpList;
                this.tbPackageCost.Text = (prCost + pdCost + pyCost).ToString("F2");

                this.lblPackageInfo.Tag = costPackDetails;
                if (isOld)
                {

                }
                else
                {
                    decimal discount = this.SetDiscount(costPackDetails);
                    this.tbEcoCost.Text = (NConvert.ToDecimal(this.tbEcoCost.Text) + discount).ToString();
                }
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
        /// 设置优惠金额
        /// </summary>
        /// <param name="costPackDetails"></param>
        /// <returns></returns>
        private decimal SetDiscount(List<PackageDetail> costPackDetails)
        {
            decimal totCost = 0.0m;
            if (costPackDetails == null || costPackDetails.Count == 0)
            {
                return totCost;
            }

            if (this.alFeeDetails == null || this.alFeeDetails.Count == 0)
            {
                return totCost;
            }

            Dictionary<string, decimal> undrugComb = new Dictionary<string, decimal>();

            Dictionary<string, decimal> feeItemTotals = new Dictionary<string, decimal>();

            foreach (FeeItemList feeItemList in this.alFeeDetails)
            {
                if (feeItemList.IsPackage == "1")
                {
                    //组合
                    if (!string.IsNullOrEmpty(feeItemList.UndrugComb.ID))
                    {
                        decimal total = undrugCombIDTotal[feeItemList.UndrugComb.ID];

                        if (total < 0)
                        {
                            continue;
                        }
                        if (undrugComb.ContainsKey(feeItemList.UndrugComb.ID))
                        {
                            continue;
                        }
                        else
                        {

                            totCost += SetItmeRebateCost(costPackDetails, feeItemList.UndrugComb.ID);
                            undrugComb.Add(feeItemList.UndrugComb.ID, 1);
                        }


                    }
                    else //单项目
                    {


                        if (feeItemList.NoBackQty > 1)
                        {

                            foreach (PackageDetail item in costPackDetails)
                            {
                                if (feeItemList.Item.ID == item.Item.ID)
                                {
                                    if (feeItemList.FT.RebateCost > 0)
                                    {
                                        continue;  //说明已折扣
                                    }

                                    if (feeItemTotals.ContainsKey(feeItemList.Item.ID))
                                    {
                                        feeItemTotals[feeItemList.Item.ID] = feeItemTotals[feeItemList.Item.ID] + item.Detail_Cost;
                                    }
                                    else
                                    {
                                        feeItemTotals.Add(feeItemList.Item.ID, item.Detail_Cost);
                                    }

                                }
                            }

                            decimal rebcost = feeItemList.FT.TotCost - feeItemTotals[feeItemList.Item.ID];

                            if (rebcost < 0)
                            {

                            }
                            else
                            {
                                feeItemList.FT.RebateCost = rebcost;
                                totCost += rebcost;
                            }

                        }
                        else
                        {
                            foreach (PackageDetail item in costPackDetails)
                            {
                                if (feeItemList.Item.ID == item.Item.ID)
                                {
                                    if (feeItemList.FT.RebateCost > 0)
                                    {
                                        continue;  //说明已折扣
                                    }
                                    else
                                    {
                                        decimal rebcost = feeItemList.FT.TotCost - item.Detail_Cost;

                                        if (rebcost < 0)
                                        {

                                        }
                                        else
                                        {
                                            feeItemList.FT.RebateCost = rebcost;
                                            totCost += rebcost;
                                        }

                                    }
                                }
                            }
                        }

                    }
                }
            }


            packageDiscount = totCost;
            return totCost;

        }


        private decimal SetItmeRebateCost(List<PackageDetail> costPackDetails, string undrugCombID)
        {
            decimal total = undrugCombIDTotal[undrugCombID];
            decimal itemtotal = 0.0m;
            foreach (PackageDetail item in costPackDetails)
            {
                if (undrugCombID == item.Item.ID)
                {
                    itemtotal += item.Detail_Cost;

                }
            }
            if (total == 0 || total - itemtotal < 0)
            {
                return 0;
            }

            decimal rebateRate = FS.FrameWork.Public.String.FormatNumber((total - itemtotal) / total, 2);
            decimal tempRebateCost = 0;
            decimal tempFix = 0;
            FeeItemList lastFeeItem = null;
            foreach (FeeItemList feeItemList in this.alFeeDetails)
            {
                if (feeItemList.IsPackage == "1")
                {
                    if (feeItemList.UndrugComb.ID == undrugCombID)
                    {
                        if (feeItemList.FT.RebateCost > 0)
                        {
                            continue;  //说明已折扣
                        }

                        feeItemList.FT.RebateCost = (feeItemList.FT.OwnCost) * rebateRate;
                        tempRebateCost += feeItemList.FT.RebateCost;

                        lastFeeItem = feeItemList;
                    }
                }
            }

            if (lastFeeItem != null)
            {
                tempFix = (total - itemtotal) - tempRebateCost;
                lastFeeItem.FT.RebateCost = lastFeeItem.FT.RebateCost + tempFix;
                return total - itemtotal;
            }
            else
            {
                return 0;
            }



        }



        Dictionary<string, decimal> undrugCombIDTotal = null;

        //{BD46CBA6-A670-40cf-A1FD-EAFBC9797D04}
        private decimal GetPackageCost()
        {
            if (this.alFeeDetails == null || this.alFeeDetails.Count == 0)
            {
                return 0.0m;
            }

            undrugCombIDTotal = new Dictionary<string, decimal>();
            decimal totCost = 0.0m;
            foreach (FeeItemList feeItemList in this.alFeeDetails)
            {
                if (feeItemList.IsPackage == "1")
                {
                    totCost += feeItemList.FT.OwnCost - feeItemList.FT.RebateCost;

                    decimal mony = feeItemList.FT.OwnCost + feeItemList.FT.PubCost + feeItemList.FT.PayCost;
                    if (undrugCombIDTotal.ContainsKey(feeItemList.UndrugComb.ID))
                    {
                        undrugCombIDTotal[feeItemList.UndrugComb.ID] = undrugCombIDTotal[feeItemList.UndrugComb.ID] + mony;
                    }
                    else
                    {
                        undrugCombIDTotal.Add(feeItemList.UndrugComb.ID, mony);
                    }
                }
            }

            return totCost;
        }

        #endregion

        #region 事件

        /// <summary>
        /// Load事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDealBalanceNew_Load(object sender, EventArgs e)
        {
            this.tbRealCost.Select();
            this.tbRealCost.Focus();
            this.tbRealCost.SelectAll();

            //清空
            this.tbLeast.Text = "0";
            this.leastCost = 0;

            this.tbAccountCost.Text = "0.00";
            this.tbAccountCost.Tag = null;
            this.lblAccountInfo.Text = string.Empty;

            this.tbEmpowerCost.Text = "0.00";
            this.tbEmpowerCost.Tag = null;
            this.lblEmpwoerInfo.Text = string.Empty;

            //{333D2AD8-DC4A-4c30-A14E-D6815AC858F9}
            this.tbCouponCost.Text = "0.00";
            this.tbCouponCost.Tag = null;
            this.lblCouponInfo.Text = string.Empty;

            this.tbPackageCost.Text = "0.00";
            this.tbPackageCost.Tag = null;
            this.lblPackageInfo.Text = string.Empty;
            this.lblPackageInfo.Tag = null;
        }

        /// <summary>
        /// 银行选择事件
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int lbBank_SelectItem(Keys key)
        {
            ProcessPayBank();
            this.fpPayMode.Focus();
            this.fpPayMode_Sheet1.SetActiveCell(fpPayMode_Sheet1.ActiveRowIndex, (int)PayModeCols.Account, true);

            return 1;
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

            //用于取消结算后，医保结算回滚
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
        /// 点击取消按钮触发
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
        /// 分发票按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSplit_Click(object sender, EventArgs e)
        {
            MessageBox.Show("不支持分发票!");
        }

        /// <summary>
        /// 点击分发票默认按钮触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbDefault_Click(object sender, EventArgs e)
        {
            this.Invoices = this.alInvoices;
        }


        /// <summary>
        /// 会员支付结算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btAccount_Click(object sender, EventArgs e)
        {
            //判断是否存在病历号
            FS.HISFC.Models.RADT.PatientInfo patient = this.accountMgr.GetPatientInfoByCardNO(this.rInfo.PID.CardNO);
            if (patient == null || string.IsNullOrEmpty(patient.PID.CardNO))
            {
                MessageBox.Show("该患者未进行基本信息登记，不允许使用会员支付", "警告");
                return;
            }

            //判断是否有会员
            FS.HISFC.Models.Account.Account account = this.accountMgr.GetAccountByCardNoEX(this.rInfo.PID.CardNO);
            if (account == null || string.IsNullOrEmpty(account.ID))
            {
                MessageBox.Show("该患者无会员账户，不允许使用会员支付", "警告");
                return;
            }
            List<FS.HISFC.Models.Account.AccountDetail> accountList = this.accountMgr.GetAccountDetail(account.ID, "ALL", "1");
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
            //{ECECDF2F-BA74-4615-A240-C442BE0A0074}
            frmAccCost.FeeDetails = this.FeeDetails;
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
            //判断是否存在病历号
            FS.HISFC.Models.RADT.PatientInfo selfPatient = this.accountMgr.GetPatientInfoByCardNO(this.rInfo.PID.CardNO);
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
                List<FS.HISFC.Models.Account.AccountDetail> accDetails = this.accountMgr.GetAccountDetail(emBp.AccountNo, "ALL", "1");
                if (accDetails != null && accDetails.Count > 0)
                {
                    empowerPatient = this.accountMgr.GetPatientInfoByCardNO(accDetails[0].CardNO);
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
        /// 积分支付
        /// {333D2AD8-DC4A-4c30-A14E-D6815AC858F9}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btCoupon_Click(object sender, EventArgs e)
        {
            if (!IsCouponModuleInUse)
            {
                MessageBox.Show("账户积分尚未启用！");
                return;
            }

            //判断是否存在病历号
            FS.HISFC.Models.RADT.PatientInfo patient = accountMgr.GetPatientInfoByCardNO(this.rInfo.PID.CardNO);
            if (patient == null || string.IsNullOrEmpty(patient.PID.CardNO))
            {
                MessageBox.Show("未查到该患者的有效信息", "警告");
                return;
            }

            //获取会员代付的金额
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

            //积分支付框
            frmCouponCost couponCost = new frmCouponCost();
            couponCost.PatientInfo = patient;
            //{4E4E36FF-EFBB-42ea-90EB-13FADAA4623A}
            couponCost.IsEmpower = false;
            couponCost.OriginalCardNO = patient.PID.CardNO;
            couponCost.DeliverableCost = this.GetPayModeFpCost() + cost;
            couponCost.SetPayModeRes += new DelegateHashtableSet(couponCost_SetPayModeRes);
            couponCost.ShowDialog();
        }

        /// <summary>
        /// 套餐结算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btPackage_Click(object sender, EventArgs e)
        {
            ////{4690DECD-6AB9-42d7-8415-3E788907C46D}
            //此处一定要做限制，否则退费的时候无法区分优惠来自套餐还是折扣！
            //if (this.RebateRate > 0)
            //{
            //    MessageBox.Show("打折与套餐结算无法同时使用，如需使用套餐结算，请勿进行折扣！");
            //    return;
            //}

            //判断是否存在病历号
            FS.HISFC.Models.RADT.PatientInfo patient = this.accountMgr.GetPatientInfoByCardNO(this.rInfo.PID.CardNO);
            if (patient == null || string.IsNullOrEmpty(patient.PID.CardNO))
            {
                MessageBox.Show("该患者未进行基本信息登记，不允许使用套餐结算", "警告");
                return;
            }
            ///{DD31280F-7321-42BB-B150-4C63018ED85F} 查询家庭成员是否有套餐
            ArrayList alPackage = this.packageMgr.QueryAvailablePackage1(patient.PID.CardNO, HISFC.Models.Base.ServiceTypes.C.ToString());
            if (alPackage == null || alPackage.Count <= 0)
            {
                MessageBox.Show("该患者不存在有效的套餐，不允许使用套餐结算", "警告");
                return;
            }

            //套餐支付窗口
            FS.HISFC.Components.Common.Forms.frmPackageCost frmPackage = new FS.HISFC.Components.Common.Forms.frmPackageCost();
            frmPackage.PatientInfo = patient;
            //{9D8048C5-1DC4-4dcd-9C2F-A3EF0B298C69}
            frmPackage.AlFeeDetails = this.alFeeDetails;

            frmPackage.SetPackPayMode += new FS.HISFC.Components.Common.Forms.DelegatePackageDetail(frmPackage_SetPackPayMode);
            frmPackage.ShowDialog();

        }


        /// <summary>
        /// 购物卡结算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btDiscountCard_Click(object sender, EventArgs e)
        {
            //判断是否存在病历号
            FS.HISFC.Models.RADT.PatientInfo patient = this.accountMgr.GetPatientInfoByCardNO(this.rInfo.PID.CardNO);
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
        /// 进入FP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpPayMode_EnterCell(object sender, EnterCellEventArgs e)
        {
            //设置FP的Cost值 【将CA金额赋值给选择的支付方式】不符合收费员的操作习惯
            //this.SetCostValue(e.Column);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void fpPayMode_EditModeOn(object sender, EventArgs e)
        {
            this.payModeFpCost = this.GetPayModeFpCost();

            this.fpPayMode.EditingControl.KeyDown += new KeyEventHandler(this.EditingControl_KeyDown);
            this.SetLocation();

            if (fpPayMode_Sheet1.ActiveColumnIndex != (int)PayModeCols.PayMode)
            {
            }

            if (fpPayMode_Sheet1.ActiveColumnIndex != (int)PayModeCols.Bank)
            {
                this.lbBank.Visible = false;
            }

            if (this.fpPayMode_Sheet1.ActiveColumnIndex == (int)PayModeCols.Cost)
            {
                #region MyRegion 金额

                string tempString = this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.ActiveRowIndex, (int)PayModeCols.PayMode].Text;
                if (tempString == string.Empty)
                {
                    for (int i = 0; i < this.fpPayMode_Sheet1.Columns.Count; i++)
                    {
                        this.fpPayMode_Sheet1.Cells[fpPayMode_Sheet1.ActiveRowIndex, i].Text = string.Empty;
                    }
                }

                bool isOnlyCash = true;
                decimal nowCost = 0;
                for (int i = 0; i < this.fpPayMode_Sheet1.RowCount; i++)
                {
                    if (this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text != string.Empty)
                    {
                        if (this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text != "现金" &&
                             NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value) > 0)
                        {
                            isOnlyCash = false;
                            nowCost += NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value);
                        }
                    }
                }

                this.payModeFpCost = this.GetPayModeFpCost();
                if (isOnlyCash)
                {
                    this.fpPayMode_Sheet1.Cells[this.rowCA, (int)PayModeCols.Cost].Text = this.payModeFpCost.ToString();

                }
                else
                {
                    if (this.payModeFpCost - nowCost < 0)
                    {
                        this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.ActiveRowIndex, (int)PayModeCols.Cost].Value = 0;
                        this.fpPayMode_Sheet1.SetActiveCell(this.fpPayMode_Sheet1.ActiveRowIndex, (int)PayModeCols.Cost, false);

                        nowCost = 0;
                    }
                    this.payModeFpCost = this.GetPayModeFpCost();
                    this.fpPayMode_Sheet1.Cells[this.rowCA, (int)PayModeCols.Cost].Value = (this.payModeFpCost - nowCost);

                }

                if (this.isDisplayCashOnly)
                {
                    this.tbRealCost.Text = (this.payModeFpCost - nowCost).ToString();
                }
                else
                {
                    this.tbRealCost.Text = (this.payModeFpCost - nowCost).ToString();
                }

                this.isPaySuccess = true;

                #endregion
            }
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
                string text = this.fpPayMode_Sheet1.ActiveCell.Text;
            }
            if (e.Column == (int)PayModeCols.Bank)
            {
                string text = this.fpPayMode_Sheet1.ActiveCell.Text.Trim();
                this.lbBank.Filter(text);
                if (!this.lbBank.Visible)
                {
                    this.lbBank.Visible = true;
                }
            }
        }

        /// <summary>
        /// FP支付方式修改
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
            bool isReturnZero = false;  //不存在支付方式金额不等再进行结算问题；支付方式金额自动分配、结算判断金额相等


            //非现金的支付方式
            for (int i = 0; i < this.fpPayMode_Sheet1.RowCount; i++)
            {
                if (this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text != string.Empty)
                {
                    if (this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text != "现金" &&
                        NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value) > 0)
                    {
                        isOnlyCash = false;
                        nowCost += NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value);
                    }
                }
            }

            this.payModeFpCost = this.GetPayModeFpCost();
            if (isOnlyCash)
            {
                this.fpPayMode_Sheet1.Cells[this.rowCA, (int)PayModeCols.Cost].Text = this.payModeFpCost.ToString();
            }
            else
            {
                decimal temp = this.payModeFpCost;
                if (temp - nowCost < 0)
                {
                    this.fpPayMode_Sheet1.Cells[e.Row, (int)PayModeCols.Cost].Value = 0;
                    this.fpPayMode_Sheet1.SetActiveCell(e.Row, (int)PayModeCols.Cost, false);
                    nowCost = 0;

                    //是否进行过归零操作；支付方式金额自动分配、结算判断金额相等
                    isReturnZero = true;
                }
                this.fpPayMode_Sheet1.Cells[this.rowCA, (int)PayModeCols.Cost].Value = this.payModeFpCost - nowCost;


                if (this.isDisplayCashOnly)
                {
                    //归零操作重新计算nowCost；支付方式金额自动分配、结算判断金额相等
                    if (isReturnZero)
                    {
                        for (int i = 0; i < this.fpPayMode_Sheet1.RowCount; i++)
                        {
                            if (this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text != string.Empty)
                            {
                                if (this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text != "现金" &&
                                    NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value) > 0)
                                {
                                    isOnlyCash = false;
                                    nowCost += NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value);
                                }
                            }
                        }
                    }

                }


            }

            if (this.isDisplayCashOnly)
            {
                this.tbRealCost.Text = (this.payModeFpCost - nowCost).ToString();
            }
            else
            {
                this.tbRealCost.Text = (this.payModeFpCost - nowCost).ToString();
            }
            this.isPaySuccess = true;

            #region 银联自动处理

            if (this.isAutoBankTrans == true)
            {
                //未实现
            }

            #endregion
        }

        /// <summary>
        /// 预览发票
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void fpSplit_CellDoubleClick(object sender, CellClickEventArgs e)
        {
            this.PreViewInvoice();
        }

        /// <summary>
        /// 按键触发
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.F12)
            {
                #region 设置【实付金额】焦点

                this.panel1.Focus();
                this.groupBox2.Focus();
                this.tbRealCost.Focus();
                this.tbRealCost.SelectAll();

                #endregion
            }
            if (keyData == Keys.Add)
            {
                #region 收费

                this.tbFee.Enabled = false;
                if (!this.SaveFee())
                {
                    this.tbFee.Enabled = true;
                    return false;
                }
                this.tbFee.Enabled = true;
                this.tbRealCost.Focus();
                this.Close();

                #endregion
            }
            if (keyData == Keys.F5)
            {
                #region 分发票【暂时屏蔽】

                //this.tabControl1.SelectedTab = this.tpSplitInvoice;

                //this.tpSplitInvoice.Focus();
                //this.tbCount.Focus();

                #endregion
            }
            if (keyData == Keys.F6)
            {
                #region 设FP支付方式焦点

                this.panel1.Focus();
                this.tabControl1.Focus();
                this.tabControl1.SelectedTab = this.tpPayMode;
                this.tpPayMode.Focus();
                this.fpPayMode.Focus();
                this.fpPayMode_Sheet1.ActiveRowIndex = 1;
                this.fpPayMode_Sheet1.SetActiveCell(1, (int)PayModeCols.Cost, false);

                #endregion
            }
            if (keyData == Keys.Escape)
            {
                if (this.lbBank.Visible)
                {
                    this.lbBank.Visible = false;
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
                    if (this.lbBank.Visible)
                    {
                        this.lbBank.PriorRow();
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
                    if (lbBank.Visible)
                    {
                        lbBank.NextRow();
                    }
                    else
                    {
                        int currRow = this.fpPayMode_Sheet1.ActiveRowIndex;

                        if (currRow <= this.fpPayMode_Sheet1.Rows.Count - 2)
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
                        //this.fpPayMode_Sheet1.SetActiveCell(currRow, (int)PayModeCols.Cost, false);

                    }
                    if (currCol == (int)PayModeCols.Cost)
                    {
                        decimal cost = NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[currRow, (int)PayModeCols.Cost].Value);
                        if (cost < 0)
                        {
                            MessageBox.Show("金额不能小于零");
                            this.fpPayMode.Focus();
                            this.fpPayMode_Sheet1.SetActiveCell(currRow, (int)PayModeCols.Cost, false);

                            return false;
                        }
                        else
                        {
                            if (!this.ComputCost())
                            {
                                return false;
                            }

                            if (currRow == this.rowCA) //现金
                            {
                                this.fpPayMode_Sheet1.SetActiveCell(currRow + 1, (int)PayModeCols.Cost, false);
                            }
                            else
                            {
                                if (this.fpPayMode_Sheet1.Cells[currRow, (int)PayModeCols.PayMode].Text != "支票")
                                {
                                    this.fpPayMode_Sheet1.SetActiveCell(currRow + 1, (int)PayModeCols.Cost, false);
                                }
                                else
                                {
                                    this.fpPayMode_Sheet1.SetActiveCell(currRow, (int)PayModeCols.Bank, false);
                                }
                            }
                        }

                        //设置FP的Cost值 【将CA金额赋值给选择的支付方式】不符合收费员的操作习惯
                        //this.SetCostValue((int)PayModeCols.Cost);
                    }
                    if (currCol == (int)PayModeCols.Bank)
                    {
                        this.ProcessPayBank();
                        this.fpPayMode_Sheet1.SetActiveCell(currRow, (int)PayModeCols.Account, false);
                    }
                    if (currCol == (int)PayModeCols.Account)
                    {
                        this.fpPayMode_Sheet1.SetActiveCell(currRow, (int)PayModeCols.Company, false);
                    }
                    if (currCol == (int)PayModeCols.Company)
                    {
                        this.fpPayMode_Sheet1.SetActiveCell(currRow, (int)PayModeCols.Memo, false);
                    }
                    if (currCol == (int)PayModeCols.Memo)
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
        /// 从实付金额直接到FP支付方式
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (this.tbRealCost.Focused && keyData == Keys.Down)
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
        /// 实付金额回车触发
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
                    if (tmpPayMode == "现金")
                    {
                        tmpCashCost = FrameWork.Public.String.FormatNumber(NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value), 2);
                    }
                    cashCost += tmpCashCost;
                }
                try
                {
                    tempCost = NConvert.ToDecimal(this.tbRealCost.Text) - Class.Function.DealCent(cashCost);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("输入的数字不合法！请验证输入" + ex.Message);
                    this.tbRealCost.Text = string.Empty;
                    this.tbRealCost.Focus();

                    return;
                }

                if (tempCost < 0)
                {
                    DialogResult result = MessageBox.Show("您输入的实付金额小于应收现金,是否重新输入?", "提示!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (result == DialogResult.Yes)
                    {
                        this.tbRealCost.SelectAll();
                        this.tbRealCost.Focus();
                        return;
                    }

                }

                //找零金额
                this.tbLeast.Text = tempCost.ToString();

                FS.HISFC.Models.Base.FT feeInfo = new FS.HISFC.Models.Base.FT();
                feeInfo.TotCost = totCost;
                feeInfo.OwnCost = ownCost;
                feeInfo.PayCost = payCost;
                feeInfo.PubCost = pubCost;
                feeInfo.BalancedCost = this.totOwnCost;
                feeInfo.SupplyCost = this.totOwnCost;
                feeInfo.RealCost = NConvert.ToDecimal(this.tbRealCost.Text);
                feeInfo.ReturnCost = tempCost;
                this.ftFeeInfo = feeInfo;

                string tmpContrlValue = this.feeIntegrate.GetControlValue(FS.HISFC.BizProcess.Integrate.Const.ENTER_TO_FEE, "0");
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


        /// <summary>
        /// 找零显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbRealCost_TextChanged(object sender, EventArgs e)
        {
            #region 找零显示

            decimal tempCost = 0;
            decimal cashCost = 0;
            for (int i = 0; i < this.fpPayMode_Sheet1.RowCount; i++)
            {
                string tmpPayMode = this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.PayMode].Text;
                decimal tmpCashCost = 0;
                if (tmpPayMode == "现金")
                {
                    tmpCashCost = FrameWork.Public.String.FormatNumber(NConvert.ToDecimal(this.fpPayMode_Sheet1.Cells[i, (int)PayModeCols.Cost].Value), 2);

                }
                cashCost += tmpCashCost;
            }
            try
            {
                tempCost = NConvert.ToDecimal(this.tbRealCost.Text) - cashCost;
            }
            catch (Exception ex)
            {
                MessageBox.Show("输入的数字不合法！请验证输入" + ex.Message);
                this.tbRealCost.Text = string.Empty;
                this.tbRealCost.Focus();

                return;
            }

            this.tbLeast.Text = tempCost.ToString();

            #endregion

            #region 防止无法输入小数点

            string dealCent = FS.HISFC.Components.OutpatientFee.Class.Function.DealCent(NConvert.ToDecimal(tbRealCost.Text)).ToString();
            if (NConvert.ToDecimal(dealCent) != NConvert.ToDecimal(tbRealCost.Text))
            {
                this.tbRealCost.Text = dealCent;
            }
            if (this.RealCostChange != null)
            {
                this.RealCostChange(this.tbRealCost.Text, this.tbLeast.Text);
            }

            #endregion
        }

        /// <summary>
        /// 分发票数量
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

        /// <summary>
        /// 天数
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

        /// <summary>
        /// 日期发生变化
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
        /// 找零回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbLeast_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }
            FS.HISFC.Models.Base.FT feeInfo = new FS.HISFC.Models.Base.FT();
            feeInfo.TotCost = totCost;
            feeInfo.OwnCost = ownCost;
            feeInfo.PayCost = payCost;
            feeInfo.PubCost = pubCost;
            feeInfo.BalancedCost = this.totOwnCost;
            feeInfo.SupplyCost = totOwnCost;
            feeInfo.RealCost = NConvert.ToDecimal(this.tbRealCost.Text);
            feeInfo.ReturnCost = this.totOwnCost - NConvert.ToDecimal(tbLeast.Text);
            this.ftFeeInfo = feeInfo;


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

        /// <summary>
        /// 优惠金额
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbEcoCost_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.rebateRate = NConvert.ToDecimal(this.tbEcoCost.Text);
            }
            catch (Exception ex)
            {
                this.rebateRate = 0;
                this.tbEcoCost.Text = this.rebateRate.ToString("F2");
                MessageBox.Show("金额输入不合法!" + ex.Message, "警告");
            }

            if (this.rebateRate > this.OwnCost)
            {
                this.rebateRate = 0;
                this.tbEcoCost.Text = this.rebateRate.ToString("F2");
                MessageBox.Show("优惠金额不能大于自费金额!", "警告");
            }

            this.payModeFpCost = this.GetPayModeFpCost();
            if (this.payModeFpCost < 0)
            {
                this.rebateRate = 0;
                this.tbEcoCost.Text = this.rebateRate.ToString("F2");
                MessageBox.Show("优惠金额不能大于实付金额!", "警告");
            }

            this.SetCost();


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
                this.payModeFpCost = this.GetPayModeFpCost();
                if (accPayCost > this.payModeFpCost)
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
                this.payModeFpCost = this.GetPayModeFpCost();
                if (empowerCost > this.payModeFpCost)
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
        /// 积分支付
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

                //先清空
                this.tbCouponCost.Text = "0.00";
                this.tbCouponCost.Tag = null;
                this.SetCost();

                if (hsTable == null)
                {
                    this.tbCouponCost.Text = "0.00";
                    this.tbCouponCost.Tag = null;
                    this.SetCost();

                    MessageBox.Show("获取积分支付信息出错！", "警告");
                    return -1;
                }

                //基本账户支付方式列表
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

                decimal accPayCost = 0m;
                foreach (BalancePay balancePay in payModeList)
                {
                    accPayCost += balancePay.FT.TotCost;
                }

                this.payModeFpCost = this.GetPayModeFpCost();
                if (accPayCost > this.payModeFpCost)
                {
                    this.tbCouponCost.Text = "0.00";
                    this.tbCouponCost.Tag = null;
                    this.SetCost();

                    MessageBox.Show("积分支付的金额大于患者需要支付的金额", "警告");
                    return -1;
                }
                this.tbCouponCost.Text = accPayCost.ToString("F2");

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
        /// 套餐支付处理
        /// </summary>
        /// <param name="packDetails"></param>
        /// <returns></returns>
        private int frmPackage_SetPackPayMode(List<PackageDetail> packDetails, bool isOld)
        {
            return this.SetPackagePayMode(packDetails, isOld);
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

                this.payModeFpCost = this.GetPayModeFpCost();
                if (disPayCost > this.payModeFpCost)
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

        #region 列枚举

        /// <summary>
        /// 支付方式列枚举
        /// </summary>
        protected enum PayModeCols
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
            Bank = 5,

            /// <summary>
            /// 帐号
            /// </summary>
            Account = 3,

            /// <summary>
            /// 开据单位
            /// </summary>
            Company = 4,

            /// <summary>
            /// 备注信息
            /// </summary>
            Memo = 2,

            /// <summary>
            /// 支票，汇票，交易号
            /// </summary>
            PosNo = 6
        }

        #endregion


    }
}
