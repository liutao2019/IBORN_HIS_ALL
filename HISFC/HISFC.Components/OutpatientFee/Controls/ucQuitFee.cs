using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using FS.HISFC.Models.Fee.Outpatient;
using FS.HISFC.Models.Base;
using System.Collections.Generic;
using FS.FrameWork.Function;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using FS.HISFC.BizProcess.Interface.Fee;
using FS.HISFC.Models.Registration;
using FS.HISFC.Models.Account;

namespace FS.HISFC.Components.OutpatientFee.Controls
{
    /// <summary>
    /// ucQuitFee<br></br>
    /// [功能描述: 门诊退费主界面UC]<br></br>
    /// [创 建 者: 王宇]<br></br>
    /// [创建时间: 2006-2-28]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucQuitFee : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucQuitFee()
        {
            try
            {
                InitializeComponent();
            }
            catch { }
        }

        #region 变量

        /// <summary>
        /// 门诊费用业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Outpatient outpatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();

        /// <summary>
        /// 门诊帐户类业务层
        /// </summary>
        protected static FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();

        /// <summary>
        /// 常数业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 挂号业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Registration.Registration registerIntegrate = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        /// <summary>
        /// 费用综合业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 医生综合业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Order orderIntegrate = new FS.HISFC.BizProcess.Integrate.Order();

        /// <summary>
        /// 药品业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        /// <summary>
        /// 退费申请业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.ReturnApply returnApplyManager = new FS.HISFC.BizLogic.Fee.ReturnApply();

        /// <summary>
        /// 非药品业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Item undrugManager = new FS.HISFC.BizLogic.Fee.Item();

        /// <summary>
        /// 组合项目业务层
        /// </summary>
        //protected FS.HISFC.BizProcess.Fee.UndrugComb undrugCombManager = new FS.HISFC.BizProcess.Fee.UndrugComb();

        /// <summary>
        /// 组合项目业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.UndrugPackAge undrugPackAgeManager = new FS.HISFC.BizLogic.Fee.UndrugPackAge();

        /// <summary>
        /// 控制参数业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        protected FS.HISFC.BizProcess.Integrate.Terminal.Confirm confirmIntegrate = new FS.HISFC.BizProcess.Integrate.Terminal.Confirm();

        /// <summary>
        /// 复合项目
        /// </summary>
        protected FS.HISFC.Models.Fee.Item.Undrug undrugComb = new FS.HISFC.Models.Fee.Item.Undrug();

        /// <summary>
        /// 当前退费的复合项目
        /// </summary>
        protected FS.HISFC.Models.Fee.Item.Undrug currentUndrugComb = new FS.HISFC.Models.Fee.Item.Undrug();

        /// <summary>
        /// 常数管理类
        /// </summary>
        protected FS.HISFC.BizLogic.Manager.Constant constManager = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 会员消费业务
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Account.AccountPay accountPay = new FS.HISFC.BizProcess.Integrate.Account.AccountPay();

        /// <summary>
        /// 控制管理类
        /// </summary>
        private FS.FrameWork.Management.ControlParam ctlMgr = new FS.FrameWork.Management.ControlParam();

        /// <summary>
        /// 要退费的发票集合
        /// </summary>
        protected ArrayList quitInvoices = new ArrayList();

        /// <summary>
        /// 发票费用信息
        /// </summary>
        protected ArrayList invoiceFeeItemLists = new ArrayList();

        /// <summary>
        /// 当前复合项目明细
        /// </summary>
        protected ArrayList currentUndrugCombs = new ArrayList();

        /// <summary>
        /// 再收费信息
        /// </summary>
        protected ArrayList againFeeItemLists = new ArrayList();

        /// <summary>
        /// 退费前的所有收费信息
        /// </summary>
        protected ArrayList oldFeeItemLists = new ArrayList();
        /// <summary>
        /// 退费入户的发票信息
        /// </summary>
        protected string CancleInvoiceNo = string.Empty;
        /// <summary>
        /// 半退时重新打印的发票信息
        /// </summary>
        protected string FeeInvoiceNo = string.Empty;
        ///// <summary>
        ///// 全局数据库事务
        ///// </summary>
        //protected FS.FrameWork.Management.Transaction t = null;

        /// <summary>
        /// 退费的类别
        /// </summary>
        protected string backType = string.Empty;

        /// <summary>
        /// 是否启用积分模块
        /// {333D2AD8-DC4A-4c30-A14E-D6815AC858F9}
        /// </summary>
        private bool IsCouponModuleInUse = false;

        /// <summary>
        /// 退费时需要调整积分
        /// </summary>
        private decimal quitOperateCouponAmount = 0.0m;

        /// <summary>
        /// 退还支付方式里消费的积分
        /// </summary>
        private decimal quitCostCouponAmount = 0.0m;

        /// <summary>
        /// 退费的发票号
        /// </summary>
        private string quitInvoiceNO = "";

        /// <summary>
        /// 组套项目是否全退 1是 0 不是
        /// </summary>
        protected bool isNeedAllQuit = false;

        /// <summary>
        /// 挂号信息实体
        /// </summary>
        protected FS.HISFC.Models.Registration.Register patient = new FS.HISFC.Models.Registration.Register();

        protected FS.HISFC.Models.Registration.Register oldPatient = new FS.HISFC.Models.Registration.Register();

        /// <summary>
        /// 划价过的发票
        /// </summary>
        protected Hashtable hsInvoice = new Hashtable();

        /// <summary>
        /// 修改后的支付方式
        /// </summary>
        protected ArrayList modifiedBalancePays = new ArrayList();

        /// <summary>
        /// 工具条
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// 是否有优惠金额
        /// </summary>
        protected bool isHaveRebateCost = false;

        protected string InvoiceNoStr = string.Empty;
        private FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();
        /// <summary>
        /// 物资收费
        /// </summary>
        //{143CA424-7AF9-493a-8601-2F7B1D635027}
        protected FS.HISFC.BizProcess.Integrate.Material.Material mateIntegrate = new FS.HISFC.BizProcess.Integrate.Material.Material();

        /// <summary>
        /// 是否显示收费界面
        /// </summary>
        protected bool isQuitFee = true;

        /// <summary>
        /// 是否账户退费
        /// </summary>
        protected bool isAccount = false;

        /// <summary>
        /// 是否收费成功 add by yerl
        /// </summary>
        private bool isSuccess = false;
        /// <summary>
        /// 需要退费的信息，每次保存前清空
        /// </summary>
        private ArrayList alQuitFeeItemList = new ArrayList();

        #region 插件变量

        /// <summary>
        /// 挂号信息插件
        /// </summary>
        FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientInfomation registerControl = null;

        /// <summary>
        /// 项目录入插件
        /// </summary>
        FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientItemInputAndDisplay itemInputControl = null;

        /// <summary>
        /// 支付方式列表
        /// </summary>
        protected ArrayList alPayModes = new ArrayList();
        /// <summary>
        /// 左侧信息插件
        /// </summary>
        FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientOtherInfomationLeft leftControl = null;
        /// <summary>
        /// 银联接口
        /// </summary>
        protected FS.HISFC.BizProcess.Interface.FeeInterface.IBankTrans iBankTrans = null;
        /// <summary>
        /// 收费弹出控件
        /// </summary>
        FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientPopupFee popFeeControl = null;

        /// <summary>
        /// 右侧信息显示控件
        /// </summary>
        FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientOtherInfomationRight rightControl = null;
        /// <summary>
        /// 银联接口
        /// </summary>
        protected FS.HISFC.BizProcess.Interface.FeeInterface.IBankTrans bankTrans = null;

        /// <summary>
        /// 四舍五舍接口配置
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Fee.ITruncFee ITruncFee = null;
        #endregion

        /// <summary>
        /// 是否门诊退药审核
        /// </summary>
        protected bool isQuitDrugConfirm = false;

        /// <summary>
        /// 费用取整接口
        /// </summary>
        protected FS.HISFC.BizProcess.Interface.FeeInterface.IOutPatientFeeRoundOff iOutPatientFeeRoundOff = null;

        /// <summary>
        /// payMode列表查询
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper helpPayMode = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 四舍五入费费用编码
        /// </summary>
        protected string roundFeeItemCode = "F00000053238";

        /// <summary>
        /// 自费诊查费项目编码
        /// </summary>
        protected string ownDiagFeeCode = string.Empty;

        #endregion

        #region 属性
        //退费时是否处理处方表
        protected bool isQuitFeeAndOperOrder = false;
        [Category("控件设置"), Description("退费时是否操作处方处方表 false:不允许 true:允许")]
        public bool IsQuitFeeAndOperOrder
        {
            get
            {
                return this.isQuitFeeAndOperOrder;
            }
            set
            {
                this.isQuitFeeAndOperOrder = value;
            }

        }

        //是否保存医保卡要支付的医保自付跟，广州默认不保存
        protected bool isSavePYfee = false;
        [Category("控件设置"), Description("是否保存医保卡要支付的医保自付跟，广州默认不保存 false:不允许 true:允许")]
        public bool IsSavePYfee
        {
            get
            {
                return this.isSavePYfee;
            }
            set
            {
                this.isSavePYfee = value;
            }

        }


        //是否保存医保卡要支付的医保自付跟，广州默认不保存
        protected bool isATM = false;
        [Category("控件设置"), Description("是否atm缴费退费")]
        public bool IsAtm
        {
            get
            {
                return this.isATM;
            }
            set
            {
                this.isATM = value;
            }

        }

        /// <summary>
        /// 是否提示填写退费的原因// {5A6C578A-D565-42c8-B3FA-B4A52D1FABFB}
        /// </summary>
        protected bool isShowWriteReason = true;
        [Category("控件设置"), Description("是否提示填写退费的原因")]
        public bool IsShowWriteReason
        {
            get
            {
                return this.isShowWriteReason;
            }
            set
            {
                this.isShowWriteReason = value;
            }

        }



        protected bool isPharmacySameRecipeQuitAll = false;
        /// <summary>
        /// 操作类别 
        /// </summary>
        [Category("控件设置"), Description("是否允许药品同一处方内药品必须全退 false:不允许 true:允许")]
        public bool IsPharmacySameRecipeQuitAll
        {
            set
            {
                this.isPharmacySameRecipeQuitAll = value;

            }
            get
            {
                return this.isPharmacySameRecipeQuitAll;
            }
        }
        protected bool isUnCAQuitAll = false;
        /// <summary>
        /// 操作类别IsUnCAQuitAll 
        /// </summary>
        [Category("控件设置"), Description("是否允许原始发票中包含银联或支票中任何一种支付方式时不允许部分退费，只能做全退处理 false:不允许 true:允许")]
        public bool IsUnCAQuitAll
        {
            set
            {
                this.isUnCAQuitAll = value;

            }
            get
            {
                return this.isUnCAQuitAll;
            }
        }
        private bool isQuitSamePayMod = false;
        /// <summary>
        /// 是否按原支付方式退费
        /// </summary>
        [Category("控件设置"), Description("是否允许按原支付方式退费 false:不允许 true:允许")]
        public bool IsQuitSamePayMod
        {
            set
            {
                this.isQuitSamePayMod = value;

            }
            get
            {
                //if (isUnCAQuitAll == false)
                //{
                return this.isQuitSamePayMod;
                //}
                //else
                //{
                //    if (justCA == true)
                //    {
                //        return this.isAllowQuitFeeHalf;
                //    }
                //    else
                //    {
                //        return false;
                //    }
                //}
            }
        }
        protected bool isAutoBankTrans = false;    //医保和HIS金额不等时收费

        /// <summary>
        /// 是否允许半退 false:不允许 true:允许
        /// </summary>
        private bool isAllowQuitFeeHalf = false;

        /// <summary>
        /// 操作类别
        /// </summary>
        [Category("控件设置"), Description("是否允许半退 false:不允许 true:允许")]
        public bool IsAllowQuitFeeHalf
        {
            set
            {
                this.isAllowQuitFeeHalf = value;

            }
            get
            {
                if (isUnCAQuitAll == false)
                {
                    return this.isAllowQuitFeeHalf;
                }
                else
                {
                    if (justCA == false)
                    {
                        if (this.isAllowQuitFeeHalf == true)
                        {
                            return false;
                        }
                    }
                    return this.isAllowQuitFeeHalf;
                }
            }
        }

        /// <summary>
        /// 是否打印负票 {E47AD522-2ACA-4482-8DC5-6F2D7C04F082}
        /// </summary>
        [Category("控件设置"), Description("是否打印负票")]
        private bool isPrintBill = false;

        /// <summary>
        /// 是否打印负票 {E47AD522-2ACA-4482-8DC5-6F2D7C04F082}
        /// </summary>
        [Category("控件设置"), Description("是否打印负票")]
        public bool IsPrintQuitBill
        {
            set
            {
                this.isPrintBill = value;

            }
            get
            {
                return this.isPrintBill;
            }
        }

        /// <summary>
        /// 是否收费 {C6F1CFDA-7848-47c4-905E-E161B9F5C4C8}
        /// </summary>
        [Category("控件设置"), Description("是否显示收费界面"), DefaultValue(true)]
        public bool IsQuitFee
        {
            set
            {
                this.isQuitFee = value;

            }
            get
            {
                return this.isQuitFee;
            }
        }

        /// <summary>
        /// 是否显示左侧查询树(通过卡号查询)
        /// </summary>
        private bool isShowRegTree = false;

        /// <summary>
        /// 是否显示左侧查询树(通过卡号查询)
        /// </summary>
        [Category("控件设置"), Description("是否显示左侧查询树(通过卡号查询)"), DefaultValue(false)]
        public bool IsShowRegTree
        {
            get { return isShowRegTree; }
            set { isShowRegTree = value; }
        }

        /// <summary>
        /// 是否门诊退药审核界面，退药审核界面不判断发票号码
        /// </summary>
        [Category("控件设置"), Description("是否门诊退药审核界面，退药审核界面不判断发票号码"), DefaultValue(false)]
        public bool IsQuitDrugConfirm
        {
            get
            {
                return isQuitDrugConfirm;
            }
            set
            {
                this.isQuitDrugConfirm = value;

                //退药审核界面 不显示补收费用窗口，不显示非药品退费界面
                this.isQuitFee = false;
            }
        }

        /// <summary>
        /// 是否可以退其他人费用
        /// </summary>
        private bool isQuitOtherFee = false;

        /// <summary>
        /// 是否可以退其他人费用
        /// </summary>
        [Category("控件设置"), Description("是否可以退其他人费用"), DefaultValue(false)]
        public bool IsQuitOtherFee
        {
            get { return isQuitOtherFee; }
            set { isQuitOtherFee = value; }
        }


        /// <summary>
        /// 是否可以退其他人费用
        /// </summary>
        private bool isUseLogout = false;

        /// <summary>
        /// 是否可以退其他人费用
        /// </summary>
        [Category("控件设置"), Description("是否启用作废功能，True = 是 ；False = 否"), DefaultValue(false)]
        public bool IsUseLogout
        {
            get { return isUseLogout; }
            set { isUseLogout = value; }
        }

        /// <summary>
        /// 操作权限
        /// </summary>
        private string operationPriv = string.Empty;

        /// <summary>
        /// 操作权限
        /// </summary>
        [Category("控件设置"), Description("界面操作权限，例如：0820+21，0820 代表二级权限，21 代表三级权限，为空则代表不需要权限也可以使用")]
        public string OperationPriv
        {
            get
            {
                return operationPriv;
            }
            set
            {
                operationPriv = value;
            }
        }


        /// <summary>
        /// 公费半退时是否可以修改费用比例
        /// </summary>
        private bool isCanModifyRate = false;

        /// <summary>
        /// 公费半退时是否可以修改费用比例
        /// </summary>
        [Category("控件设置"), Description("公费半退时是否可以修改费用比例，True = 是 ；False = 否"), DefaultValue(false)]
        public bool IsCanModifyRate
        {
            get { return isCanModifyRate; }
            set { isCanModifyRate = value; }
        }

        /// <summary>
        /// 公费半退时是否可以修改患者信息和结算方式
        /// </summary>
        private bool isCanModifyPatientInfo = false;

        /// <summary>
        /// 公费半退时是否可以修改患者信息和结算方式
        /// </summary>
        [Category("控件设置"), Description("公费半退时是否可以修改患者信息和结算方式，True = 是 ；False = 否"), DefaultValue(false)]
        public bool IsCanModifyPatientInfo
        {
            get { return isCanModifyPatientInfo; }
            set { isCanModifyPatientInfo = value; }
        }

        /// <summary>
        /// 公费半退时是否需要记账单号
        /// </summary>
        private bool isNeedJZD = false;

        [Category("控件设置"), Description("公费半退时是否需要记账单号,true:是；false：否。")]
        public bool IsNeedJZD
        {
            get { return this.isNeedJZD; }
            set { this.isNeedJZD = value; }
        }

        /// <summary>
        /// 划价保存时是否保存四舍五入费
        /// </summary>
        private bool isSaveChargeRoundFee = false;

        /// <summary>
        /// 划价保存时是否保存四舍五入费
        /// </summary>
        [Category("控件设置"), Description("划价保存时是否保存四舍五入费,true:是；false：否。")]
        public bool IsSaveChargeRoundFee
        {
            get { return this.isSaveChargeRoundFee; }
            set { this.isSaveChargeRoundFee = value; }
        }

        /// <summary>
        /// 划价保存时是否保存诊查费
        /// </summary>
        private bool isSaveChargeDiagFee = false;

        [Category("控件设置"), Description("划价保存时是否保存诊查费,true:是；false：否。")]
        public bool IsSaveChargeDiagFee
        {
            get { return this.isSaveChargeDiagFee; }
            set { this.isSaveChargeDiagFee = value; }
        }

        /// <summary>
        /// 是否显示补收费
        /// </summary>
        private bool isShowReChargeTab = true;

        /// <summary>
        /// 是否显示补收费
        /// </summary>
        [Category("控件设置"), Description("是否显示补收费,true:是；false：否。")]
        public bool IsShowReChargeTab
        {
            get { return this.isShowReChargeTab; }
            set { this.isShowReChargeTab = value; }
        }

        /// <summary>
        /// 是否在退费时提示划价保存
        /// </summary>
        private bool isShowSaveChargeHits = false;

        /// <summary>
        /// 是否在退费时提示划价保存
        /// </summary>
        [Category("控制设置"), Description("是否在退费时提示划价保存,trus:是;false:否")]
        public bool IsShowSaveChargeHits
        {
            get { return this.isShowSaveChargeHits; }
            set { this.isShowSaveChargeHits = value; }
        }

        private bool isJudgePrivWhileQuit = true;
        /// <summary>
        /// 退费时是否判断权限
        /// </summary>
        [Category("控制设置"), Description("退费时是否判断权限,trus:是;false:否")]
        public bool IsJudgePrivWhileQuit
        {
            get { return this.isJudgePrivWhileQuit; }
            set { this.isJudgePrivWhileQuit = value; }
        }

        #endregion

        #region  枚举
        /// <summary>
        /// 待退药显示列
        /// </summary>
        protected enum DrugList
        {
            /// <summary>
            /// 名称
            /// </summary>
            ItemName = 0,

            /// <summary>
            /// 组
            /// </summary>
            Comb = 1,

            /// <summary>
            /// 组号
            /// </summary>
            CombNo = 2,

            /// <summary>
            /// 规格
            /// </summary>
            Specs = 3,

            /// <summary>
            /// 数量
            /// </summary>
            Amount = 4,

            /// <summary>
            /// 单位
            /// </summary>
            PriceUnit = 5,

            /// <summary>
            /// 可退数量
            /// </summary>
            NoBackQty = 6,

            /// <summary>
            /// 金额
            /// </summary>
            Cost = 7,

            /// <summary>
            /// 每次量和付数
            /// </summary>
            DoseAndDays = 8,

            /// <summary>
            /// 医嘱退费原因
            /// </summary>
            RefundReason = 9
        }
        /// <summary>
        /// 待退非药显示列
        /// </summary>
        protected enum UndrugList
        {
            /// <summary>
            /// 名称
            /// </summary>
            ItemName = 0,

            /// <summary>
            /// 组
            /// </summary>
            Comb = 1,

            /// <summary>
            /// 组号
            /// </summary>
            CombNo = 2,

            /// <summary>
            /// 数量
            /// </summary>
            Amount = 3,

            /// <summary>
            /// 单位
            /// </summary>
            PriceUnit = 4,

            /// <summary>
            /// 可退数量
            /// </summary>
            NoBackQty = 5,

            /// <summary>
            /// 金额
            /// </summary>
            Cost = 6,

            /// <summary>
            /// 组合项目名称
            /// </summary>
            PackageName = 7,

            /// <summary>
            /// 医嘱退费原因
            /// </summary>
            RefundReason = 8
        }
        /// <summary>
        /// 已退药显示列
        /// </summary>
        protected enum DrugListQuit
        {
            /// <summary>
            /// 名称
            /// </summary>
            ItemName = 0,

            /// <summary>
            /// 规格
            /// </summary>
            Specs = 1,

            /// <summary>
            /// 数量
            /// </summary>
            Amount = 2,

            /// <summary>
            /// 单位
            /// </summary>
            PriceUnit = 3,

            /// <summary>
            /// 标志
            /// </summary>
            Flag = 4,

            Price = 5,

            Cost = 6
        }
        /// <summary>
        /// 已退非药显示列
        /// </summary>
        protected enum UndrugListQuit
        {
            /// <summary>
            /// 名称
            /// </summary>
            ItemName = 0,

            /// <summary>
            /// 数量
            /// </summary>
            Amount = 1,

            /// <summary>
            /// 单位
            /// </summary>
            PriceUnit = 2,

            /// <summary>
            /// 标志
            /// </summary>
            Flag = 3
        }

        #endregion

        #region 函数

        #region 私有方法



        /// <summary>
        /// 载入插件
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        protected virtual int LoadPlugins()
        {
            //this.itemInputControl = this.feeIntegrate.GetPlugIns<FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientItemInputAndDisplay>
            //       (FS.HISFC.BizProcess.Integrate.Const.INTERFACE_ITEM_INPUT, new ucDisplay());

            this.itemInputControl = this.ucDisplay1;
            this.ucDisplay1.IsQuitFee = true;

            this.itemInputControl.ItemKind = ItemKind.All;

            //this.leftControl = this.feeIntegrate.GetPlugIns<FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientOtherInfomationLeft>
            //        (FS.HISFC.BizProcess.Integrate.Const.INTERFACE_LEFT, new ucInvoicePreview());

            this.leftControl = this.ucInvoicePreview1;

            //{1B220814-0243-4725-882C-012E831C0DA1}
            this.leftControl.InvoiceUpdated += new FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateChangeSomething(leftControl_InvoiceUpdated);



            //用于判断收费还是划价

            this.leftControl.IsPreFee = false;
            this.itemInputControl.LeftControl = this.leftControl;

            //this.rightControl = this.feeIntegrate.GetPlugIns<FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientOtherInfomationRight>
            //    (FS.HISFC.BizProcess.Integrate.Const.INTERFACE_RIGHT, new ucCostDisplay());

            this.rightControl = this.ucCostDisplay1;
            this.rightControl.IsPreFee = false;

            this.itemInputControl.RightControl = this.rightControl;

            if (this.isQuitFee)
            {
                this.leftControl.IsValidFee = true;
                this.leftControl.Init();

                if (this.neuTabControl1.TabPages.Count > 1)
                {
                    this.leftControl.InitInvoice();
                }

                this.rightControl.Init();

                if (!isShowReChargeTab)
                {
                    this.neuTabControl1.Controls.Remove(this.tpFee);
                }
            }
            else//{C6F1CFDA-7848-47c4-905E-E161B9F5C4C8}
            {
                this.neuTabControl1.Controls.Remove(this.tpFee);
            }
            //银联接口
            bankTrans = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject<
                FS.HISFC.BizProcess.Interface.FeeInterface.IBankTrans>(this.GetType());
            if (bankTrans == null)
            {
                bankTrans = new Forms.frmBankTrans();
            }

            #region {2E5139C9-52D8-4fec-A96B-09BECFDDFBD1}

            if (this.isShowRegTree)
            {
                ucInvoiceView.Visible = true;
                ucInvoiceView.Focus();
            }
            else
            {
                ucInvoiceView.Visible = false;
            }

            #endregion

            return 1;
        }

        //{1B220814-0243-4725-882C-012E831C0DA1}
        void leftControl_InvoiceUpdated()
        {
            this.cmbRegDept.Focus();
        }

        /// <summary>
        /// 初始化项目录入插件
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        protected virtual int InitItemInputControl()
        {
            if (this.itemInputControl == null)
            {
                return -1;
            }

            if (this.isQuitFee)
            {
                this.itemInputControl.Init();
            }

            return 1;
        }
        bool justCA = true;
        /// <summary>
        /// 通过发票号获得发票信息集合
        /// </summary>
        /// <param name="invoiceNO">发票号</param>
        /// <returns>成功 发票信息集合 失败 null</returns>
        protected virtual ArrayList QueryBalancesByInvoiceNO(string invoiceNO)
        {
            //通过输入发票号，获得发票的序列，再通过序列获得所有发票集合。
            ArrayList balances = this.outpatientManager.QueryBalancesSameInvoiceCombNOByInvoiceNO(invoiceNO);
            ArrayList pays = this.outpatientManager.QueryBalancePaysByInvoiceNO(invoiceNO);
            //-		[0]	{}	object {FS.HISFC.Models.Fee.Outpatient.BalancePay}
            justCA = true;
            foreach (FS.HISFC.Models.Fee.Outpatient.BalancePay bp in pays)
            {
                if (bp != null)
                {
                    if (bp.PayType.ID != "CA")
                    {
                        justCA = false;
                        break;
                    }
                }
            }

            //查询业务层出错
            if (balances == null)
            {
                tbInvoiceNO.SelectAll();
                MessageBox.Show("查询发票出错!" + this.outpatientManager.Err);
                tbInvoiceNO.Focus();

                return null;
            }
            //没有找到纪录
            if (balances.Count == 0)
            {
                tbInvoiceNO.SelectAll();
                MessageBox.Show("发票号不存在,请重新录入");
                tbInvoiceNO.Focus();

                return null;
            }

            return balances;
        }

        /// <summary>
        /// 处理产生多发票时候的退费情况,发票获得
        /// </summary>
        /// <param name="balances">当前发票集合</param>
        /// <returns>成功 1 失败 -1 </returns>
        protected virtual int DealMulityBalancesCount(ref ArrayList balances)
        {
            bool isSelect = false;//默认不需要弹出选择发票窗口.
            string SeqNo = string.Empty;//发票序列号
            //循环检索当前获得的所有发票信息.
            foreach (Balance balance in balances)
            {
                if (SeqNo == string.Empty)
                {
                    SeqNo = balance.CombNO;

                    continue;
                }
                else
                {
                    //如果发现有发票序列不同的情况,说明存在重复发票号情况,需要弹出发票选择窗口.
                    if (SeqNo != balance.CombNO)
                    {
                        isSelect = true;
                    }
                }
            }
            if (isSelect) //判断是否需要选择发票
            {
                //声明选择发票窗口实例
                ucInvoiceSelect uc = new ucInvoiceSelect();
                //装载本次检索的所有发票信息
                uc.Add(balances);
                //弹出发票选择窗口
                FS.FrameWork.WinForms.Classes.Function.PopForm.TopMost = true;
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
                //如果操作员没有进行选择给予提示
                if (uc.SelectedBalance == null || uc.SelectedBalance.CombNO == string.Empty)
                {
                    MessageBox.Show("请选择要退的发票");

                    return -1;
                }
                //通过操作员选择的发票信息,选择了唯一发票序列,再根据发票序列获取本次应参与退费的所有发票信息.
                balances = outpatientManager.QueryBalancesByInvoiceSequence(uc.SelectedBalance.CombNO);
                if (balances == null)
                {
                    MessageBox.Show("查询发票失败" + outpatientManager.Err);

                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        /// 是否操作员可以退当前发票
        /// </summary>
        /// <param name="balances">当前发票集合</param>
        /// <returns>成功 true 失败 false</returns>
        protected virtual bool IsOperCanQuitTheseBalances(ArrayList balances)
        {
            //读取控制参数
            //读取是否允许退其他操作员的费用。
            bool isCanQuitOterhOper = this.controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.CAN_QUIT_OTHER_OPER_INVOICE, true, false);

            if (this.isQuitDrugConfirm == false)        //对于门诊退药审核 不需要增加此校验
            {
                //不允许退其他操作员费用,判断当前发票的收费操作员是否为当前操作员
                //如果不是，那么不允许继续退费;
                if (!isCanQuitOterhOper)
                {
                    Balance tempBalance = balances[0] as Balance;

                    if (tempBalance == null)
                    {
                        MessageBox.Show("发票格式转换出错!");
                        tbInvoiceNO.SelectAll();
                        tbInvoiceNO.Focus();

                        return false;
                    }

                    if (tempBalance.BalanceOper.ID != this.outpatientManager.Operator.ID)
                    {
                        MessageBox.Show("该发票的收费员为: " + tempBalance.BalanceOper.ID + "您没有权限进行退费!");
                        tbInvoiceNO.SelectAll();
                        tbInvoiceNO.Focus();

                        return false;
                    }

                    tempBalance = null;
                }
            }

            //获得是否可以退日结过费用控制参数
            bool isCanQuitDayBlanced = this.controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.CAN_QUIT_DAYBALANCED_INVOICE, true, false);

            if (!isCanQuitDayBlanced)//不允许退日结过费用
            {
                Balance tmpInvoice = balances[0] as Balance;

                if (tmpInvoice == null)
                {
                    MessageBox.Show("发票格式转换出错!");
                    tbInvoiceNO.SelectAll();
                    tbInvoiceNO.Focus();

                    return false;
                }
                if (tmpInvoice.IsDayBalanced)
                {
                    MessageBox.Show("该发票已经日结,您没有权限进行退费!");
                    tbInvoiceNO.SelectAll();
                    tbInvoiceNO.Focus();

                    return false;
                }
            }

            int canQuitDays = this.controlParamIntegrate.GetControlParam<int>(FS.HISFC.BizProcess.Integrate.Const.VALID_QUIT_DAYS, true, 10000);

            DateTime nowTime = this.outpatientManager.GetDateTimeFromSysDateTime();

            Balance tmpInvoiceValid = balances[0] as Balance;

            if (tmpInvoiceValid == null)
            {
                MessageBox.Show("发票格式转换出错!");
                tbInvoiceNO.SelectAll();
                tbInvoiceNO.Focus();

                return false;
            }

            int tempDays = (nowTime - tmpInvoiceValid.BalanceOper.OperTime).Days;

            if (tempDays >= canQuitDays)
            {
                MessageBox.Show("该发票已经超出可退费天数,不允许退费!");

                tbInvoiceNO.SelectAll();
                tbInvoiceNO.Focus();

                return false;
            }

            return true;
        }

        /// <summary>
        /// 查询发票信息
        /// </summary>
        /// <param name="invoiceNO">发票号</param>
        /// <returns>null错误 要处理的发票集合</returns>
        protected virtual ArrayList QueryInvoices(string invoiceNO)
        {
            //发票补位
            //invoiceNO = invoiceNO.PadLeft(12, '0');
            InvoiceNoStr = invoiceNO;
            //通过发票号获得发票信息集合
            ArrayList balances = this.QueryBalancesByInvoiceNO(invoiceNO);
            if (balances == null)
            {
                return null;
            }

            //是否操作员可以退当前发票
            if (!this.IsOperCanQuitTheseBalances(balances))
            {
                return null;
            }

            //如果获得的发票多余一张(因为分发票的情况存在),那么判断是否要弹出发票选择界面
            //这里的多张发票产生有两个可能 1 分发票产生的,发票号不同,但是发票序列相同,所以无需弹出选择发票界面.
            // 2 是由于设置的发票号重复(系统允许发票号重复),但是发票序列不同,这个时候要弹出选择发票界面,让操作员
            //进行选择.总之就是发票序列决定此次退费的发票信息.
            if (balances.Count > 1)
            {
                if (this.DealMulityBalancesCount(ref balances) == -1)
                {
                    return null;
                }
            }

            return balances;
        }

        /// <summary>
        /// 初始化 
        /// </summary>
        /// <returns>成功1 失败 -1</returns>
        protected virtual int Init()
        {
            if (this.LoadPlugins() < 0)
            {
                return -1;
            }

            if (this.InitItemInputControl() < 0)
            {
                return 1;
            }

            alPayModes = this.managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PAYMODES);
            if (alPayModes == null || alPayModes.Count <= 0)
            {
                MessageBox.Show("获取支付方式错误");

                return -1;
            }
            helpPayMode.ArrayObject = alPayModes;
            //初始化 挂号科室
            ArrayList regDeptList = this.managerIntegrate.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.C);
            if (regDeptList == null)
            {
                MessageBox.Show("初始化挂号科室出错!" + this.managerIntegrate.Err);

                return -1;
            }
            this.cmbRegDept.AddItems(regDeptList);

            //初始化医生列表，加入一个无归属医生。编号999
            ArrayList doctList = new ArrayList();
            doctList = this.managerIntegrate.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            if (doctList == null)
            {
                MessageBox.Show("初始化医生列表出错!" + this.managerIntegrate.Err);

                return -1;
            }
            FS.HISFC.Models.Base.Employee pNone = new FS.HISFC.Models.Base.Employee();
            pNone.ID = "999";
            pNone.Name = "无归属";
            pNone.SpellCode = "WGS";
            pNone.UserCode = "999";
            doctList.Add(pNone);

            this.cmbDoct.AddItems(doctList);

            //四舍五入费费用编码
            ArrayList lst = this.constManager.GetList("ROUNDFEEITEMCODE");
            if (lst.Count > 0)
            {
                roundFeeItemCode = ((FS.HISFC.Models.Base.Const)lst[0]).ID;
            }

            //自费诊查费项目编码
            this.ownDiagFeeCode = this.controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.AUTO_PUB_FEE_DIAG_FEE_CODE, true, string.Empty);

            if (ITruncFee == null)
            {
                ITruncFee = (FS.HISFC.BizProcess.Interface.Fee.ITruncFee)FS.HISFC.BizProcess.Interface.Fee.InterfaceManager.GetTruncFeeType();
            }

            //是否启用积分模块
            this.IsCouponModuleInUse = (this.ctlMgr.QueryControlerInfo("CP0001") == "1");

            return 1;
        }

        #endregion

        /// <summary>
        /// 获得费用明细
        /// </summary>
        /// <returns>-1 失败 1 成功</returns>
        protected virtual int GetItemList()
        {
            try
            {
                //获得本次退费所有发票的第一张作为临时发票信息
                Balance tempBalance = quitInvoices[0] as Balance;

                isHaveRebateCost = false;

                //通过发票序列号,获得所有应参与退费的药品信息
                ArrayList drugItemLists = this.outpatientManager.QueryDrugFeeItemListByInvoiceSequence(tempBalance.CombNO);
                if (drugItemLists == null)
                {
                    MessageBox.Show("获得药品信息出错!" + outpatientManager.Err);

                    return -1;
                }
                //通过发票序列号,获得所有应参与退费的非药品信息
                ArrayList undrugItemLists = outpatientManager.QueryUndrugFeeItemListByInvoiceSequence(tempBalance.CombNO);
                if (undrugItemLists == null)
                {
                    MessageBox.Show("获得非药品信息出错!" + outpatientManager.Err);

                    return -1;
                }

                #region 物资
                //{40DFDC91-0EC1-4cd4-81BC-0EAE4DE1D3AB}
                ArrayList mateItemLists = outpatientManager.QueryMateFeeItemListByInvoiceSequence(tempBalance.CombNO);
                if (mateItemLists == null)
                {
                    MessageBox.Show("获得物资信息出错！" + outpatientManager.Err);
                    return -1;
                }
                //暂时将物资放在非药品中处理
                undrugItemLists.AddRange(mateItemLists.ToArray());
                #endregion

                if (drugItemLists.Count + undrugItemLists.Count == 0)
                {
                    MessageBox.Show("没有费用信息!");

                    return -1;
                }

                this.invoiceFeeItemLists = outpatientManager.QueryFeeItemListsByInvoiceNO(tempBalance.Invoice.ID);

                ArrayList drugConfirmList = new ArrayList();//已经核准的退药信息
                ArrayList undrugConfirmList = new ArrayList();//已经核准退费的非药品信息
                //循环所有参与退费的发票,查询已经核准的药品和非药品信息
                //由于多张发票的存在,而明细只对应一个发票号,所以遍所有的参与退费的发票,其中只有一个发票号符合查询条件.
                foreach (Balance balance in this.quitInvoices)
                {
                    //如果已经获得了已经核准退费的药品信息,就不再获取
                    if (drugConfirmList == null || drugConfirmList.Count == 0)
                    {
                        //获得已经核准的退药信息
                        drugConfirmList = returnApplyManager.GetList(balance.Patient.ID, balance.Invoice.ID, true, false, "1");
                        if (drugConfirmList == null)
                        {
                            MessageBox.Show("获得确认药品项目列表出错!" + returnApplyManager.Err);

                            return -1;
                        }
                    }
                    //如果已经获得了已经核准退费的非药品信息,就不再获取
                    if (undrugConfirmList == null || undrugConfirmList.Count == 0)
                    {
                        //获得已经核准退费的非药品信息
                        undrugConfirmList = returnApplyManager.GetList(balance.Patient.ID, balance.Invoice.ID, true, false, "0");
                        if (undrugConfirmList == null)
                        {
                            MessageBox.Show("获得确认非药品项目列表出错!" + returnApplyManager.Err);

                            return -1;
                        }
                    }
                }



                //显示待退药品信息
                this.fpSpread1_Sheet1.RowCount = drugItemLists.Count;

                FeeItemList drugItem = null;//药品临时实体
                for (int i = 0; i < drugItemLists.Count; i++)
                {
                    drugItem = drugItemLists[i] as FeeItemList;

                    if (drugItem.FT.RebateCost > 0)
                    {
                        isHaveRebateCost = true;
                    }


                    //重新计算本条药品的总金额,方便以后参与计算费用
                    drugItem.FT.TotCost = drugItem.FT.OwnCost + drugItem.FT.PayCost + drugItem.FT.PubCost;

                    this.fpSpread1_Sheet1.Rows[i].Tag = drugItem;
                    //因为可能存在同一发票有不同看诊科室的情况,而且挂号信息中的看诊信息不一定与实际收费的看诊
                    //科室相同,所以这里把挂号实体的看诊可是赋值为收费明细时的看诊科室信息.
                    this.patient.DoctorInfo.Templet.Dept = drugItem.RecipeOper.Dept;

                    this.fpSpread1_Sheet1.Cells[i, (int)DrugList.ItemName].Text = drugItem.Item.Name;

                    this.fpSpread1_Sheet1.Cells[i, (int)DrugList.CombNo].Text = drugItem.Order.Combo.ID;

                    this.fpSpread1_Sheet1.Cells[i, (int)DrugList.Specs].Text = drugItem.Item.Specs;
                    this.fpSpread1_Sheet1.Cells[i, (int)DrugList.Amount].Text = drugItem.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(drugItem.Item.Qty / drugItem.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(drugItem.Item.Qty, 2).ToString();
                    this.fpSpread1_Sheet1.Cells[i, (int)DrugList.PriceUnit].Text = drugItem.Item.PriceUnit;
                    this.fpSpread1_Sheet1.Cells[i, (int)DrugList.NoBackQty].Text = drugItem.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(drugItem.NoBackQty / drugItem.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(drugItem.NoBackQty, 2).ToString();

                    this.fpSpread1_Sheet1.Cells[i, (int)DrugList.Cost].Text = (drugItem.FT.OwnCost + drugItem.FT.PayCost + drugItem.FT.PubCost).ToString();

                    if (drugItem.Item.SysClass.ID.ToString() == "PCC")
                    {
                        this.fpSpread1_Sheet1.Cells[i, (int)DrugList.DoseAndDays].Text = "每次量:" + drugItem.Order.DoseOnce.ToString() + drugItem.Order.DoseUnit + " " + "付数:" + drugItem.Days.ToString();
                    }
                    else
                    {
                        this.fpSpread1_Sheet1.Cells[i, (int)DrugList.DoseAndDays].Text = "每次量:" + drugItem.Order.DoseOnce.ToString() + drugItem.Order.DoseUnit;
                    }

                    Class.Function.DrawCombo(this.fpSpread1_Sheet1, (int)DrugList.CombNo, (int)DrugList.Comb, 0);

                    //{5C7887F1-A4D5-4a66-A814-18D45367443E}
                    if (drugItem.Order.QuitFlag == 1)
                    {
                        this.fpSpread1_Sheet1.RowHeader.Rows[i].BackColor = SystemColors.Control;
                    }
                    else
                    {
                        this.fpSpread1_Sheet1.RowHeader.Rows[i].BackColor = Color.Red;
                    }
                    this.fpSpread1_Sheet1.Cells[i, (int)DrugList.RefundReason].Text = drugItem.Order.RefundReason.ToString();
                    
                }

                //显示非药品信息
                this.fpSpread1_Sheet2.RowCount = undrugItemLists.Count;

                FeeItemList undrugItem = null;
                for (int i = 0; i < undrugItemLists.Count; i++)
                {
                    undrugItem = undrugItemLists[i] as FeeItemList;

                    #region 加载物资信息
                    ////{40DFDC91-0EC1-4cd4-81BC-0EAE4DE1D3AB}
                    //if (undrugItem.Item.ItemType == EnumItemType.UnDrug)
                    //{
                    //    //{143CA424-7AF9-493a-8601-2F7B1D635027}
                    //    string outNo = undrugItem.UpdateSequence.ToString();
                    //    List<HISFC.Models.FeeStuff.Output> list = mateIntegrate.QueryOutput(outNo);
                    //    undrugItem.MateList = list;
                    //}
                    #endregion

                    if (undrugItem.FT.RebateCost > 0)
                    {
                        isHaveRebateCost = true;
                    }

                    undrugItem.FT.TotCost = undrugItem.FT.OwnCost + undrugItem.FT.PayCost + undrugItem.FT.PubCost;
                    this.fpSpread1_Sheet2.Rows[i].Tag = undrugItem;
                    this.patient.DoctorInfo.Templet.Dept = undrugItem.RecipeOper.Dept;

                    this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.ItemName].Text = undrugItem.Item.Name;
                    this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.CombNo].Text = undrugItem.Order.Combo.ID;
                    this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.Amount].Text = undrugItem.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(undrugItem.Item.Qty / undrugItem.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(undrugItem.Item.Qty, 2).ToString();
                    this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.PriceUnit].Text = undrugItem.Item.PriceUnit;
                    this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.NoBackQty].Text = undrugItem.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(undrugItem.NoBackQty / undrugItem.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(undrugItem.NoBackQty, 2).ToString();
                    this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.Cost].Text = (undrugItem.FT.OwnCost + undrugItem.FT.PayCost + undrugItem.FT.PubCost).ToString();
                    if (undrugItem.UndrugComb.ID != null && undrugItem.UndrugComb.ID.Length > 0)
                    {
                        this.undrugComb = this.undrugManager.GetValidItemByUndrugCode(undrugItem.UndrugComb.ID);
                        if (this.undrugComb == null)
                        {
                            MessageBox.Show("获得组套信息出错，无法显示组套自定义码，但是不影响退费操作！");
                        }
                        else
                        {
                            undrugItem.UndrugComb.UserCode = this.undrugComb.UserCode;
                        }

                        FS.HISFC.Models.Fee.Item.Undrug item = this.undrugManager.GetValidItemByUndrugCode(undrugItem.ID);

                        if (item == null)
                        {
                            this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.PackageName].Text = "(" + undrugItem.UndrugComb.UserCode + ")" + undrugItem.UndrugComb.Name;
                        }
                        else
                        {
                            this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.PackageName].Text = "(" + undrugItem.UndrugComb.UserCode + ")" + undrugItem.UndrugComb.Name + "[" + item.UserCode + "]";
                        }

                    }
                    else
                    {
                        FS.HISFC.Models.Fee.Item.Undrug item = this.undrugManager.GetValidItemByUndrugCode(undrugItem.ID);

                        if (item != null)
                        {
                            this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.PackageName].Text = item.UserCode;
                        }
                    }

                    this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.RefundReason].Text = undrugItem.Order.RefundReason.ToString();

                    Class.Function.DrawCombo(this.fpSpread1_Sheet2, (int)UndrugList.CombNo, (int)UndrugList.Comb, 0);
                    //显示物资信息
                    SetMateData(undrugItem, i);
                    //{5C7887F1-A4D5-4a66-A814-18D45367443E}
                    if (undrugItem.Order.QuitFlag == 1)
                    {
                        this.fpSpread1_Sheet2.RowHeader.Rows[i].BackColor = SystemColors.Control;
                    }
                    else
                    {
                        this.fpSpread1_Sheet2.RowHeader.Rows[i].BackColor = Color.Red;
                    }
                }
                //显示确认退药信息
                this.fpSpread2_Sheet1.RowCount = drugItemLists.Count + drugConfirmList.Count;
                FS.HISFC.Models.Fee.ReturnApply drugReturn = null;
                for (int i = 0; i < drugConfirmList.Count; i++)
                {
                    drugReturn = drugConfirmList[i] as FS.HISFC.Models.Fee.ReturnApply;
                    this.fpSpread2_Sheet1.Rows[i].Tag = drugReturn;
                    this.fpSpread2_Sheet1.Cells[i, (int)DrugListQuit.ItemName].Text = drugReturn.Item.Name;
                    this.fpSpread2_Sheet1.Cells[i, (int)DrugListQuit.Amount].Text = drugReturn.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(drugReturn.Item.Qty / drugReturn.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(drugReturn.Item.Qty, 2).ToString();
                    this.fpSpread2_Sheet1.Cells[i, (int)DrugListQuit.PriceUnit].Text = drugReturn.Item.PriceUnit;
                    this.fpSpread2_Sheet1.Cells[i, (int)DrugListQuit.Specs].Text = drugReturn.Item.Specs;
                    this.fpSpread2_Sheet1.Cells[i, (int)DrugListQuit.Flag].Text = "确认";


                    int findRow = FindItem(drugReturn.RecipeNO, drugReturn.SequenceNO, this.fpSpread1_Sheet1);
                    if (findRow == -1)
                    {
                        MessageBox.Show("查找未退药项目出错!");

                        return -1;
                    }
                    FeeItemList modifyDrug = this.fpSpread1_Sheet1.Rows[findRow].Tag as FeeItemList;

                    modifyDrug.NoBackQty = modifyDrug.NoBackQty - drugReturn.Item.Qty;
                    modifyDrug.Item.Qty = modifyDrug.Item.Qty - drugReturn.Item.Qty;
                    modifyDrug.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(modifyDrug.Item.Price * modifyDrug.Item.Qty / modifyDrug.Item.PackQty, 2);
                    modifyDrug.FT.OwnCost = modifyDrug.FT.TotCost;

                    this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.Cost].Text = modifyDrug.FT.TotCost.ToString();
                    this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.Amount].Text = modifyDrug.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(modifyDrug.Item.Qty / modifyDrug.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(modifyDrug.Item.Qty, 2).ToString();
                    this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.NoBackQty].Text = modifyDrug.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(modifyDrug.NoBackQty / modifyDrug.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(modifyDrug.NoBackQty, 2).ToString();
                }
                this.fpSpread2_Sheet2.RowCount = undrugItemLists.Count + undrugConfirmList.Count;
                FS.HISFC.Models.Fee.ReturnApply undrugReturn = null;
                for (int i = 0; i < undrugConfirmList.Count; i++)
                {
                    undrugReturn = undrugConfirmList[i] as FS.HISFC.Models.Fee.ReturnApply;
                    this.fpSpread2_Sheet2.Rows[i].Tag = undrugReturn;
                    this.fpSpread2_Sheet2.Cells[i, (int)UndrugListQuit.ItemName].Text = undrugReturn.Item.Name;
                    this.fpSpread2_Sheet2.Cells[i, (int)UndrugListQuit.Amount].Text = undrugReturn.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(undrugReturn.Item.Qty / undrugReturn.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(undrugReturn.Item.Qty, 2).ToString();
                    this.fpSpread2_Sheet2.Cells[i, (int)UndrugListQuit.PriceUnit].Text = undrugReturn.Item.PriceUnit;
                    this.fpSpread2_Sheet2.Cells[i, (int)UndrugListQuit.Flag].Text = "确认";

                    int findRow = FindItem(undrugReturn.RecipeNO, undrugReturn.SequenceNO, this.fpSpread1_Sheet2);
                    if (findRow == -1)
                    {
                        MessageBox.Show("查找未退非药项目出错!");

                        return -1;
                    }
                    FeeItemList modifyUndrug = this.fpSpread1_Sheet2.Rows[findRow].Tag as FeeItemList;

                    modifyUndrug.NoBackQty = modifyUndrug.NoBackQty - undrugReturn.Item.Qty;
                    modifyUndrug.Item.Qty = modifyUndrug.Item.Qty - undrugReturn.Item.Qty;
                    modifyUndrug.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(modifyUndrug.Item.Price * modifyUndrug.Item.Qty / modifyUndrug.Item.PackQty, 2);
                    modifyUndrug.FT.OwnCost = modifyUndrug.FT.TotCost;

                    this.fpSpread1_Sheet2.Cells[findRow, (int)UndrugList.Cost].Text = modifyUndrug.FT.TotCost.ToString();
                    this.fpSpread1_Sheet2.Cells[findRow, (int)UndrugList.Amount].Text = modifyUndrug.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(modifyUndrug.Item.Qty / modifyUndrug.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(modifyUndrug.Item.Qty, 2).ToString();
                    this.fpSpread1_Sheet2.Cells[findRow, (int)UndrugList.NoBackQty].Text = modifyUndrug.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(modifyUndrug.NoBackQty / modifyUndrug.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(modifyUndrug.NoBackQty, 2).ToString();

                }

                if (isHaveRebateCost)
                {
                    this.ckbAllQuit.Checked = true;
                    this.ckbAllQuit.Enabled = false;
                }
                else
                {
                    this.ckbAllQuit.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 显示物资数据
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="rowIndex"></param>
        //{143CA424-7AF9-493a-8601-2F7B1D635026}
        protected virtual void SetMateData(FeeItemList feeItemList, int rowIndex)
        {

            int index = 0;
            //{4D6501CB-D2A4-4204-8CBA-F34F28D5300A} 非药品-物资对照方式退费修改
            if (feeItemList.MateList == null)
            {
                return;
            }
            if (feeItemList.MateList.Count < 1) return;

            fpSpread1_Sheet2.RowHeader.Cells[rowIndex, 0].Text = "+";
            fpSpread1_Sheet2.RowHeader.Cells[rowIndex, 0].BackColor = Color.YellowGreen;

            foreach (HISFC.Models.FeeStuff.Output outItem in feeItemList.MateList)
            {
                fpSpread1_Sheet2.Rows.Add(fpSpread1_Sheet2.Rows.Count, 1);
                index = fpSpread1_Sheet2.Rows.Count - 1;
                this.fpSpread1_Sheet2.Cells[index, 0].Text = outItem.StoreBase.Item.Name;
                this.fpSpread1_Sheet2.Cells[index, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                outItem.StoreBase.Item.Qty = outItem.StoreBase.Quantity - outItem.ReturnApplyNum - outItem.StoreBase.Returns;
                this.fpSpread1_Sheet2.Cells[index, 3].Text = outItem.StoreBase.Item.Qty.ToString();
                this.fpSpread1_Sheet2.Cells[index, 4].Text = outItem.StoreBase.Item.PriceUnit;
                this.fpSpread1_Sheet2.Cells[index, 5].Text = outItem.StoreBase.Item.Qty.ToString();
                this.fpSpread1_Sheet2.Cells[index, 6].Text = (outItem.StoreBase.Item.Qty * outItem.StoreBase.Item.Price).ToString();
                this.fpSpread1_Sheet2.Cells[index, 7].Text = outItem.StoreBase.Item.UserCode;
                this.fpSpread1_Sheet2.RowHeader.Cells[index, 0].Text = ".";
                this.fpSpread1_Sheet2.RowHeader.Cells[index, 0].BackColor = System.Drawing.Color.SkyBlue;
                this.fpSpread1_Sheet2.Rows[index].Tag = outItem;
                this.fpSpread1_Sheet2.Rows[index].Visible = false;

            }
        }

        /// <summary>
        /// 查找项目
        /// </summary>
        /// <param name="recipeNO">处方号</param>
        /// <param name="sequence">处方流水号</param>
        /// <param name="sv">当前SheetView</param>
        /// <returns></returns>
        protected virtual int FindItem(string recipeNO, int sequence, FarPoint.Win.Spread.SheetView sv)
        {
            for (int i = 0; i < sv.RowCount; i++)
            {
                if (sv.Rows[i].Tag is FeeItemList)
                {
                    FeeItemList f = sv.Rows[i].Tag as FeeItemList;
                    if (f.RecipeNO == recipeNO && f.SequenceNO == sequence)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }
        /// <summary>
        /// 查找项目
        /// </summary>
        /// <param name="recipeNO">处方号</param>
        /// <param name="sequence">处方流水号</param>
        /// <param name="sv">当前SheetView</param>
        /// <returns></returns>
        protected virtual int FindItem(string recipeNO, FarPoint.Win.Spread.SheetView sv)
        {
            for (int i = 0; i < sv.RowCount; i++)
            {
                if (sv.Rows[i].Tag is FeeItemList)
                {
                    FeeItemList f = sv.Rows[i].Tag as FeeItemList;
                    if (f.RecipeNO == recipeNO)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }
        /// <summary>
        /// 查找允许新增的行
        /// </summary>
        /// <param name="sv">当前SheetView页</param>
        /// <returns>成功 当前可以增加的行 失败 -1</returns>
        protected virtual int FindNullRow(FarPoint.Win.Spread.SheetView sv)
        {
            for (int i = 0; i < sv.RowCount; i++)
            {
                if (sv.Rows[i].Tag == null || !(sv.Rows[i].Tag is FS.FrameWork.Models.NeuObject))
                {
                    return i;
                }
                else if (string.IsNullOrEmpty(sv.Cells[i, (int)DrugList.ItemName].Text))
                {
                    return i;
                }

            }

            return -1;
        }

        /// <summary>
        /// 全退操作
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        protected virtual int AllQuit()
        {
            this.ckbAllQuit.Checked = true;

            int temp = 0;
            if (this.fpSpread1.ActiveSheet == this.fpSpread1_Sheet1)
            {
                temp = 1;
            }
            else
            {
                temp = 2;
            }

            this.fpSpread1.ActiveSheet = this.fpSpread1_Sheet1;
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                this.fpSpread1_Sheet1.ActiveRowIndex = i;
                if (this.QuitOperation() == -1)
                {
                    return -1;
                }
            }
            this.fpSpread1.ActiveSheet = this.fpSpread1_Sheet2;
            for (int i = 0; i < this.fpSpread1_Sheet2.RowCount; i++)
            {
                this.fpSpread1_Sheet2.ActiveRowIndex = i;
                if (this.QuitOperation() == -1)
                {
                    return -1;
                }
            }

            if (temp == 1)
            {
                this.fpSpread1.ActiveSheet = this.fpSpread1_Sheet1;
            }
            else
            {
                this.fpSpread1.ActiveSheet = this.fpSpread1_Sheet2;
            }

            return 1;
        }

        /// <summary>
        /// 处理双击,回车选择项目退费
        /// </summary>
        protected virtual void DealQuitOperation()
        {
            //bool isNeedGroupAllQuit = this.controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.GROUP_ITEM_ALLQUIT, false, false);

            ////tmpValue = bValue;

            if (this.fpSpread1.ActiveSheet == this.fpSpread1_Sheet2)//非药品
            {
                #region 非药品
                int currRow = this.fpSpread1_Sheet2.ActiveRowIndex;

                if (this.fpSpread1_Sheet2.Rows[currRow].Tag is FeeItemList)
                {
                    FeeItemList f = this.fpSpread1_Sheet2.Rows[currRow].Tag as FeeItemList;

                    //{5C7887F1-A4D5-4a66-A814-18D45367443E}
                    if (f.Order.QuitFlag != 1)
                    {
                        string ErrInfo = "【" + f.Item.Name + "】相应的医嘱未经医生批准，不允许退费！";
                        FS.FrameWork.WinForms.Classes.Function.ShowToolTip(ErrInfo, 1);
                        return;
                    }

                    if (f.UndrugComb.ID != string.Empty && this.isNeedAllQuit && this.ckbAllQuit.Checked)
                    {
                        for (int i = 0; i < this.fpSpread1_Sheet2.RowCount; i++)
                        {
                            if (this.fpSpread1_Sheet2.Rows[i].Tag is FeeItemList)
                            {
                                FeeItemList fTemp = this.fpSpread1_Sheet2.Rows[i].Tag as FeeItemList;
                                if (fTemp.UndrugComb.ID == f.UndrugComb.ID && fTemp.Order.ID == f.Order.ID)
                                {
                                    this.QuitUndrugOperation(i);
                                }
                            }
                        }

                        return;
                    }
                    else
                    {
                        QuitOperation();
                    }
                }
                //{143CA424-7AF9-493a-8601-2F7B1D635026}
                //物资项目
                if (this.fpSpread1_Sheet2.Rows[currRow].Tag is HISFC.Models.FeeStuff.Output)
                {
                    QuitOperationMate(currRow);
                }
                #endregion
            }
            else
            {
                //if (this.isPharmacySameRecipeQuitAll == false)
                //{
                QuitOperation();
                //}
                //else
                //{
                //    QuitOperationPharmacySameRecipeQuitAll();
                //}
            }
        }

        /// <summary>
        ///退去药品操作 
        /// </summary>
        /// <param name="currRow">当前行</param>
        /// <returns>成功 1 失败 -1</returns>
        protected virtual int QuitDrugOperation(int currRow)
        {
            if (this.fpSpread1_Sheet1.Rows[currRow].Tag is FeeItemList)
            {
                FeeItemList f = this.fpSpread1_Sheet1.Rows[currRow].Tag as FeeItemList;

                if (f.NoBackQty <= 0)
                {
                    return -2;
                }
                int findRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread2_Sheet1);
                //没有找到，那么新增一条;
                if (findRow == -1)
                {
                    findRow = FindNullRow(this.fpSpread2_Sheet1);
                    FeeItemList fClone = f.Clone();
                    this.fpSpread2_Sheet1.Rows[findRow].Tag = fClone;
                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.ItemName].Text = fClone.Item.Name;
                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Amount].Text = fClone.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(fClone.Item.Qty / fClone.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(fClone.Item.Qty, 2).ToString();
                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.PriceUnit].Text = fClone.Item.PriceUnit;
                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Flag].Text = "未核准";
                    //显示药品单价，显示金额，但不进行操作
                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Price].Text = fClone.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(fClone.Item.Price, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(fClone.Item.Price / fClone.Item.PackQty, 2).ToString();
                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Cost].Text = FS.FrameWork.Public.String.FormatNumber(fClone.Item.Price * fClone.Item.Qty / fClone.Item.PackQty, 2).ToString();
                }
                else //找到了累加数量
                {

                    FeeItemList fFind = this.fpSpread2_Sheet1.Rows[findRow].Tag as FeeItemList;
                    fFind.Item.Qty = fFind.Item.Qty + f.Item.Qty;
                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Amount].Text = fFind.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / fFind.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty, 2).ToString();
                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.ItemName].Text = fFind.Item.Name;
                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.PriceUnit].Text = fFind.Item.PriceUnit;
                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Flag].Text = "未核准";
                    //显示药品单价，显示金额，但不进行操作
                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Price].Text = fFind.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price / fFind.Item.PackQty, 2).ToString();
                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Cost].Text = FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price * fFind.Item.Qty / fFind.Item.PackQty, 2).ToString();
                }
                f.Item.Qty = f.Item.Qty - f.NoBackQty;
                f.NoBackQty = 0;
                f.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2);
                this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.Amount].Text = f.FeePack == "1" ?
                    FS.FrameWork.Public.String.FormatNumber(f.Item.Qty / f.Item.PackQty, 2).ToString() :
                    FS.FrameWork.Public.String.FormatNumber(f.Item.Qty, 2).ToString();
                this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.Cost].Text = f.FT.TotCost.ToString();
                this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.NoBackQty].Text = "0";
            }

            ComputCost();

            return 1;
        }

        /// <summary>
        /// 处理非药品当前行退费
        /// </summary>
        /// <param name="currRow">当前行</param>
        /// <returns>成功 1 失败 -1</returns>
        protected virtual int QuitUndrugOperation(int currRow)
        {
            if (this.fpSpread1_Sheet2.Rows[currRow].Tag is FeeItemList)
            {
                FeeItemList f = this.fpSpread1_Sheet2.Rows[currRow].Tag as FeeItemList;

                if (f.NoBackQty <= 0)
                {
                    return -2;
                }
                int findRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread2_Sheet2);
                //没有找到，那么新增一条;
                if (findRow == -1)
                {
                    findRow = FindNullRow(this.fpSpread2_Sheet2);
                    FeeItemList fClone = f.Clone();
                    this.fpSpread2_Sheet2.Rows[findRow].Tag = fClone;
                    this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.ItemName].Text = fClone.Item.Name;
                    this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Amount].Text = fClone.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(fClone.Item.Qty / fClone.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(fClone.Item.Qty, 2).ToString();
                    this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.PriceUnit].Text = fClone.Item.PriceUnit;
                    this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Flag].Text = "未核准";
                }
                else //找到了累加数量
                {

                    FeeItemList fFind = this.fpSpread2_Sheet2.Rows[findRow].Tag as FeeItemList;
                    fFind.Item.Qty = fFind.Item.Qty + f.Item.Qty;
                    this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Amount].Text = fFind.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / fFind.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty, 2).ToString();
                    this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.ItemName].Text = fFind.Item.Name;
                    this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.PriceUnit].Text = fFind.Item.PriceUnit;
                    this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Flag].Text = "未核准";
                }
                f.Item.Qty = f.Item.Qty - f.NoBackQty;
                f.NoBackQty = 0;
                f.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2);
                this.fpSpread1_Sheet2.Cells[currRow, (int)UndrugList.Amount].Text = f.FeePack == "1" ?
                    FS.FrameWork.Public.String.FormatNumber(f.Item.Qty / f.Item.PackQty, 2).ToString() :
                    FS.FrameWork.Public.String.FormatNumber(f.Item.Qty, 2).ToString();
                this.fpSpread1_Sheet2.Cells[currRow, (int)UndrugList.Cost].Text = f.FT.TotCost.ToString();
                this.fpSpread1_Sheet2.Cells[currRow, (int)UndrugList.NoBackQty].Text = "0";


            }

            ComputCost();

            return 1;
        }

        /// <summary>
        /// 处理退费操作
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        protected virtual int QuitOperation()
        {
            #region 药品
            bool isBack = false;

            if (this.fpSpread1.ActiveSheet == this.fpSpread1_Sheet1)//退药品
            {
                int currRow = this.fpSpread1_Sheet1.ActiveRowIndex;

                if (this.fpSpread1_Sheet1.Rows[currRow].Tag is FeeItemList)
                {
                    FeeItemList f = this.fpSpread1_Sheet1.Rows[currRow].Tag as FeeItemList;

                    //{5C7887F1-A4D5-4a66-A814-18D45367443E}
                    if (f.Order.QuitFlag != 1)
                    {
                        string ErrInfo = "【" + f.Item.Name + "】相应的医嘱未经医生批准，不允许退费！";
                        FS.FrameWork.WinForms.Classes.Function.ShowToolTip(ErrInfo, 1);
                        return -2;
                    }

                    if (this.IsJudgePrivWhileQuit)
                    {
                        if (this.outpatientManager.Operator.ID.Equals(f.FeeOper.ID) == false)
                        {
                            //判断权限,是否有退其他挂号员操作的权限
                            if (!CommonController.CreateInstance().JugePrive(Function.PrivQuit, Function.PrivQuitOtherOperFee))
                            {
                                CommonController.CreateInstance().MessageBox("您没有退其他操作员收费记录的权限，操作已取消，该费用的操作员是：" + CommonController.CreateInstance().GetEmployeeName(f.FeeOper.ID), MessageBoxIcon.Warning);
                                return -1;
                            }
                        }
                    }

                    if (this.ckbAllQuit.Checked)
                    {
                        if (!this.isNeedAllQuit || f.Item.SysClass.ID.ToString() != "PCC")
                        {
                            if (f.NoBackQty <= 0)
                            {
                                if (f.IsConfirmed)
                                {

                                    for (int i = 0; i <= this.fpSpread2_Sheet1.Rows.Count - 1; ++i)
                                    {
                                        if (this.fpSpread2_Sheet1.Rows[i].Tag is FS.HISFC.Models.Fee.ReturnApply)
                                        {
                                            FS.HISFC.Models.Fee.ReturnApply drugReturn = (FS.HISFC.Models.Fee.ReturnApply)this.fpSpread2_Sheet1.Rows[i].Tag;
                                            if (drugReturn.RecipeNO == f.RecipeNO && drugReturn.SequenceNO == f.SequenceNO)
                                            {
                                                isBack = true;
                                                break;
                                            }

                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }
                                    if (!isBack)
                                    {
                                        MessageBox.Show(f.Item.Name + "已经发药，请到药房做退药审核后，再做退费!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                        return -1;

                                    }


                                }
                                else
                                {
                                    return 1;
                                }
                            }

                            int findRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread2_Sheet1);
                            //没有找到，那么新增一条;
                            if (findRow == -1)
                            {
                                if (!isBack) //没有退药
                                {
                                    findRow = FindNullRow(this.fpSpread2_Sheet1);
                                    FeeItemList fClone = f.Clone();
                                    this.fpSpread2_Sheet1.Rows[findRow].Tag = fClone;
                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.ItemName].Text = fClone.Item.Name;
                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Specs].Text = fClone.Item.Specs;
                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Amount].Text = fClone.FeePack == "1" ?
                                        FS.FrameWork.Public.String.FormatNumber(fClone.NoBackQty / fClone.Item.PackQty, 2).ToString() :
                                        FS.FrameWork.Public.String.FormatNumber(fClone.NoBackQty, 2).ToString();
                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.PriceUnit].Text = fClone.Item.PriceUnit;
                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Flag].Text = "未核准";
                                    //显示药品单价，显示金额，但不进行操作
                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Price].Text = fClone.FeePack == "1" ?
                                        FS.FrameWork.Public.String.FormatNumber(fClone.Item.Price, 2).ToString() :
                                        FS.FrameWork.Public.String.FormatNumber(fClone.Item.Price / fClone.Item.PackQty, 2).ToString();
                                    if (ITruncFee != null)
                                    {
                                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Cost].Text = FS.FrameWork.Function.NConvert.ToDecimal(ITruncFee.TruncFee(fClone.Item.Price * fClone.Item.Qty / fClone.Item.PackQty)).ToString();
                                    }
                                    else
                                    {
                                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Cost].Text = FS.FrameWork.Public.String.FormatNumber(fClone.Item.Price * fClone.Item.Qty / fClone.Item.PackQty, 2).ToString();
                                    }
                                }
                            }
                            else //找到了累加数量
                            {

                                FeeItemList fFind = this.fpSpread2_Sheet1.Rows[findRow].Tag as FeeItemList;
                                fFind.Item.Qty = fFind.Item.Qty + f.NoBackQty;
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Amount].Text = fFind.FeePack == "1" ?
                                    FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / fFind.Item.PackQty, 2).ToString() :
                                    FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty, 2).ToString();
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.ItemName].Text = fFind.Item.Name;
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Specs].Text = fFind.Item.Specs;
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.PriceUnit].Text = fFind.Item.PriceUnit;
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Flag].Text = "未核准";
                                //显示药品单价，显示金额，但不进行操作
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Price].Text = fFind.FeePack == "1" ?
                                    FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price, 2).ToString() :
                                    FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price / fFind.Item.PackQty, 2).ToString();
                                if (ITruncFee != null)
                                {
                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Cost].Text = FS.FrameWork.Function.NConvert.ToDecimal(ITruncFee.TruncFee(fFind.Item.Price * fFind.Item.Qty / fFind.Item.PackQty)).ToString();
                                }
                                else
                                {
                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Cost].Text = FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price * fFind.Item.Qty / fFind.Item.PackQty, 2).ToString();
                                }




                            }

                            f.Item.Qty = f.Item.Qty - f.NoBackQty;
                            f.NoBackQty = 0;
                            f.FT.OwnCost = f.FT.OwnCost - f.FT.OwnCost;
                            f.FT.PubCost = f.FT.PubCost - f.FT.PubCost;
                            f.FT.PayCost = f.FT.PayCost - f.FT.PayCost;
                            f.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2);
                            this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.Amount].Text = f.FeePack == "1" ?
                                FS.FrameWork.Public.String.FormatNumber(f.Item.Qty / f.Item.PackQty, 2).ToString() :
                                FS.FrameWork.Public.String.FormatNumber(f.Item.Qty, 2).ToString();
                            this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.Cost].Text = f.FT.TotCost.ToString();
                            this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.NoBackQty].Text = "0";
                        }
                        else
                        {
                            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                            {
                                if (this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                                {
                                    FeeItemList fTemp = this.fpSpread1_Sheet1.Rows[i].Tag as FeeItemList;
                                    if (fTemp.Item.SysClass.ID.ToString() == "PCC" && fTemp.Order.Combo.ID == f.Order.Combo.ID)
                                    {
                                        this.QuitDrugOperation(i);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (f.Item.SysClass.ID.ToString() == "PCC" && f.Order.Combo.ID.Length > 0 && this.isNeedAllQuit)
                        {
                            ArrayList alFeeItem = new ArrayList();

                            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                            {
                                if (this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                                {
                                    FeeItemList fTemp = this.fpSpread1_Sheet1.Rows[i].Tag as FeeItemList;
                                    if (fTemp.Item.SysClass.ID.ToString() == "PCC" && fTemp.Order.Combo.ID == f.Order.Combo.ID)
                                    {
                                        alFeeItem.Add(fTemp);
                                    }
                                }
                            }

                            txtReturnItemName.Text = "中药组合";
                            txtReturnNum.Tag = alFeeItem;
                            txtRetSpecs.Text = string.Empty;
                            this.backType = "PCC";
                            txtReturnNum.Select();
                            txtReturnNum.Focus();
                        }
                        else
                        {
                            txtReturnNum.Select();
                            txtReturnNum.Focus();
                            txtReturnItemName.Text = f.Item.Name;
                            txtReturnNum.Tag = f;
                            txtRetSpecs.Text = f.Item.Specs;
                        }
                    }
                }
            }

            #endregion

            #region 非药品

            if (this.fpSpread1.ActiveSheet == this.fpSpread1_Sheet2)//退药品
            {
                int currRow = this.fpSpread1_Sheet2.ActiveRowIndex;

                // bool isNeedGroupAllQuit = this.controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.GROUP_ITEM_ALLQUIT, false, false);

                //tmpValue = bValue;

                #region 物资
                //{143CA424-7AF9-493a-8601-2F7B1D635026}
                //是否是多条对照
                //List<HISFC.Models.FeeStuff.Output> outitemList = new List<FS.HISFC.Models.FeeStuff.Output>();
                //string headerText = this.fpSpread1.ActiveSheet.RowHeader.Cells[currRow, 0].Text;
                //if (headerText == "+" || headerText == "-")
                //{
                //    if (!this.ckbAllQuit.Checked)
                //    {
                //        if (!this.ckbAllQuit.Checked && headerText != ".")
                //        {
                //            MessageBox.Show("请选择要退费的物资信息！");
                //            if (this.fpSpread1_Sheet2.RowHeader.Cells[currRow, 0].Text == "+")
                //            {
                //                this.ExpandOrCloseRow(false, currRow + 1);
                //            }
                //            return -1;
                //        }
                //    }
                //}
                #endregion

                #region 非药品
                if (this.fpSpread1_Sheet2.Rows[currRow].Tag is FeeItemList)
                {
                    FeeItemList f = this.fpSpread1_Sheet2.Rows[currRow].Tag as FeeItemList;

                    //{5C7887F1-A4D5-4a66-A814-18D45367443E}
                    if (f.Order.QuitFlag != 1)
                    {
                        string ErrInfo = "【" + f.Item.Name + "】相应的医嘱未经医生批准，不允许退费！";
                        FS.FrameWork.WinForms.Classes.Function.ShowToolTip(ErrInfo, 1);
                        return -2;
                    }

                    if (this.IsJudgePrivWhileQuit)
                    {
                        if (this.outpatientManager.Operator.ID.Equals(f.FeeOper.ID) == false)
                        {
                            //判断权限,是否有退其他挂号员操作的权限
                            if (!CommonController.CreateInstance().JugePrive(Function.PrivQuit, Function.PrivQuitOtherOperFee))
                            {
                                CommonController.CreateInstance().MessageBox("您没有退其他操作员收费记录的权限，操作已取消，该费用的操作员是：" + CommonController.CreateInstance().GetEmployeeName(f.FeeOper.ID), MessageBoxIcon.Warning);
                                return -1;
                            }
                        }
                    }

                    //if (f.Item.ID == this.roundFeeItemCode)
                    //{
                    //    CommonController.CreateInstance().MessageBox("四舍五入费不需退！", MessageBoxIcon.Warning);
                    //    return -1;

                    //}
                    if (this.ckbAllQuit.Checked)
                    {
                        if (f.NoBackQty == 0)//如果是四舍五入费可能是负数
                        {
                            return 1;
                            //MessageBox.Show(f.Item.Name + "已经没有可退数量，不能再退费!");
                            //return -2;
                        }

                        int findRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread2_Sheet2);
                        //没有找到，那么新增一条;
                        if (findRow == -1)
                        {
                            findRow = FindNullRow(this.fpSpread2_Sheet2);
                            FeeItemList fClone = new FeeItemList();
                            fClone = f.Clone();
                            this.fpSpread2_Sheet2.Rows[findRow].Tag = fClone;
                            this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.ItemName].Text = fClone.Item.Name;
                            this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Amount].Text = fClone.FeePack == "1" ?
                                FS.FrameWork.Public.String.FormatNumber(fClone.NoBackQty / fClone.Item.PackQty, 2).ToString() :
                                FS.FrameWork.Public.String.FormatNumber(fClone.NoBackQty, 2).ToString();
                            this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.PriceUnit].Text = fClone.Item.PriceUnit;
                            this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Flag].Text = "未核准";
                        }
                        else //找到了累加数量
                        {
                            FeeItemList fFind = this.fpSpread2_Sheet2.Rows[findRow].Tag as FeeItemList;
                            fFind.Item.Qty = fFind.Item.Qty + f.NoBackQty;
                            this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Amount].Text = fFind.FeePack == "1" ?
                                FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / fFind.Item.PackQty, 2).ToString() :
                                FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty, 2).ToString();
                            this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.ItemName].Text = fFind.Item.Name;
                            this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.PriceUnit].Text = fFind.Item.PriceUnit;
                            this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Flag].Text = "未核准";
                        }
                        f.Item.Qty = f.Item.Qty - f.NoBackQty;
                        f.NoBackQty = 0;

                        f.FT.OwnCost = f.FT.OwnCost - f.FT.OwnCost;
                        f.FT.PubCost = f.FT.PubCost - f.FT.PubCost;
                        f.FT.PayCost = f.FT.PayCost - f.FT.PayCost;

                        f.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2);
                        this.fpSpread1_Sheet2.Cells[currRow, (int)UndrugList.Amount].Text = f.FeePack == "1" ?
                            FS.FrameWork.Public.String.FormatNumber(f.Item.Qty / f.Item.PackQty, 2).ToString() :
                            FS.FrameWork.Public.String.FormatNumber(f.Item.Qty, 2).ToString();
                        this.fpSpread1_Sheet2.Cells[currRow, (int)UndrugList.Cost].Text = f.FT.TotCost.ToString();
                        this.fpSpread1_Sheet2.Cells[currRow, (int)UndrugList.NoBackQty].Text = "0";

                        #region 物资
                        //{143CA424-7AF9-493a-8601-2F7B1D635026}
                        int mateIndex = 0;
                        if (f.MateList.Count > 0)
                        {
                            foreach (HISFC.Models.FeeStuff.Output tempOut in f.MateList)
                            {
                                mateIndex = GetMateRowIndex(tempOut);
                                if (mateIndex == -1)
                                {
                                    MessageBox.Show("查找物资信息失败！");
                                    return -1;
                                }
                                tempOut.StoreBase.Item.Qty = 0;
                                this.fpSpread1_Sheet2.Cells[mateIndex, (int)UndrugList.NoBackQty].Text = "0";
                                this.fpSpread1_Sheet2.Cells[mateIndex, (int)UndrugList.Amount].Text = "0";
                                this.fpSpread1_Sheet2.Cells[mateIndex, (int)UndrugList.Cost].Text = "0";
                                this.fpSpread1_Sheet2.Rows[mateIndex].Tag = tempOut;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        //复合项目
                        if (f.UndrugComb.ID != null && f.UndrugComb.ID.Length > 0 && this.isNeedAllQuit)
                        {
                            ArrayList alFeeItem = new ArrayList();

                            this.currentUndrugComb = this.undrugManager.GetValidItemByUndrugCode(f.UndrugComb.ID);
                            if (this.currentUndrugComb == null)
                            {
                                MessageBox.Show("获得复合项目出错！" + this.undrugManager.Err);

                                return -1;
                            }

                            this.currentUndrugCombs = this.undrugPackAgeManager.QueryUndrugPackagesBypackageCode(this.currentUndrugComb.ID);

                            if (currentUndrugCombs == null)
                            {
                                MessageBox.Show("获得复合项目明细出错！" + this.undrugPackAgeManager.Err);

                                return -1;
                            }

                            for (int i = 0; i < this.fpSpread1_Sheet2.RowCount; i++)
                            {
                                if (this.fpSpread1_Sheet2.Rows[i].Tag is FeeItemList)
                                {
                                    FeeItemList fTemp = this.fpSpread1_Sheet2.Rows[i].Tag as FeeItemList;
                                    if (fTemp.UndrugComb.ID == f.UndrugComb.ID && fTemp.Order.ID == f.Order.ID)
                                    {
                                        alFeeItem.Add(fTemp);
                                    }
                                }
                            }

                            txtReturnItemName.Text = f.UndrugComb.Name;
                            txtReturnNum.Tag = alFeeItem;
                            txtRetSpecs.Text = string.Empty;
                            this.backType = "PACKAGE";
                            txtReturnNum.Select();
                            txtReturnNum.Focus();
                        }
                        else
                        {
                            txtReturnItemName.Text = f.Item.Name;
                            txtReturnNum.Tag = f;
                            txtRetSpecs.Text = f.Item.Specs;
                            this.backType = string.Empty;
                            txtReturnNum.Select();
                            txtReturnNum.Focus();
                        }
                    }
                }
                #endregion
            }

            #endregion

            ComputCost();

            return 1;
        }

        /// <summary>
        /// 处理退费操作
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        protected virtual int QuitOperationPharmacySameRecipeQuitAll()
        {
            #region 药品

            if (this.fpSpread1.ActiveSheet == this.fpSpread1_Sheet1)//退药品
            {
                int currRow = this.fpSpread1_Sheet1.ActiveRowIndex;

                if (this.fpSpread1_Sheet1.Rows[currRow].Tag is FeeItemList)
                {
                    FeeItemList f = this.fpSpread1_Sheet1.Rows[currRow].Tag as FeeItemList;

                    if (true)
                    {
                        if (!true)
                        {
                            if (f.NoBackQty <= 0)
                            {
                                MessageBox.Show(f.Item.Name + "已经没有可退数量，不能再退费!");

                                return -1;
                            }
                            int findRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread2_Sheet1);
                            //没有找到，那么新增一条;
                            if (findRow == -1)
                            {
                                findRow = FindNullRow(this.fpSpread2_Sheet1);
                                FeeItemList fClone = f.Clone();
                                this.fpSpread2_Sheet1.Rows[findRow].Tag = fClone;
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.ItemName].Text = fClone.Item.Name;
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Specs].Text = fClone.Item.Specs;
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Amount].Text = fClone.FeePack == "1" ?
                                    FS.FrameWork.Public.String.FormatNumber(fClone.NoBackQty / fClone.Item.PackQty, 2).ToString() :
                                    FS.FrameWork.Public.String.FormatNumber(fClone.NoBackQty, 2).ToString();
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.PriceUnit].Text = fClone.Item.PriceUnit;
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Flag].Text = "未核准";
                                //显示药品单价，显示金额，但不进行操作
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Price].Text = fClone.FeePack == "1" ?
                                    FS.FrameWork.Public.String.FormatNumber(fClone.Item.Price, 2).ToString() :
                                    FS.FrameWork.Public.String.FormatNumber(fClone.Item.Price / fClone.Item.PackQty, 2).ToString();
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Cost].Text = FS.FrameWork.Public.String.FormatNumber(fClone.Item.Price * fClone.Item.Qty / fClone.Item.PackQty, 2).ToString();
                            }
                            else //找到了累加数量
                            {

                                FeeItemList fFind = this.fpSpread2_Sheet1.Rows[findRow].Tag as FeeItemList;
                                fFind.Item.Qty = fFind.Item.Qty + f.NoBackQty;
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Amount].Text = fFind.FeePack == "1" ?
                                    FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / fFind.Item.PackQty, 2).ToString() :
                                    FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty, 2).ToString();
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.ItemName].Text = fFind.Item.Name;
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Specs].Text = fFind.Item.Specs;
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.PriceUnit].Text = fFind.Item.PriceUnit;
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Flag].Text = "未核准";
                                //显示药品单价，显示金额，但不进行操作
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Price].Text = fFind.FeePack == "1" ?
                                    FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price, 2).ToString() :
                                    FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price / fFind.Item.PackQty, 2).ToString();
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Cost].Text = FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price * fFind.Item.Qty / fFind.Item.PackQty, 2).ToString();
                            }
                            f.Item.Qty = f.Item.Qty - f.NoBackQty;
                            f.NoBackQty = 0;
                            f.FT.OwnCost = f.FT.OwnCost - f.FT.OwnCost;
                            f.FT.PubCost = f.FT.PubCost - f.FT.PubCost;
                            f.FT.PayCost = f.FT.PayCost - f.FT.PayCost;
                            f.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2);
                            this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.Amount].Text = f.FeePack == "1" ?
                                FS.FrameWork.Public.String.FormatNumber(f.Item.Qty / f.Item.PackQty, 2).ToString() :
                                FS.FrameWork.Public.String.FormatNumber(f.Item.Qty, 2).ToString();
                            this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.Cost].Text = f.FT.TotCost.ToString();
                            this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.NoBackQty].Text = "0";
                        }
                        else
                        {
                            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                            {
                                if (this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                                {
                                    FeeItemList fTemp = this.fpSpread1_Sheet1.Rows[i].Tag as FeeItemList;
                                    if (fTemp.RecipeNO == f.RecipeNO)
                                    {
                                        this.QuitDrugOperation(i);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (f.Item.SysClass.ID.ToString() == "PCC" && f.Order.Combo.ID.Length > 0 && this.isNeedAllQuit)
                        {
                            ArrayList alFeeItem = new ArrayList();

                            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                            {
                                if (this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                                {
                                    FeeItemList fTemp = this.fpSpread1_Sheet1.Rows[i].Tag as FeeItemList;
                                    if (fTemp.Item.SysClass.ID.ToString() == "PCC" && fTemp.Order.Combo.ID == f.Order.Combo.ID)
                                    {
                                        alFeeItem.Add(fTemp);
                                    }
                                }
                            }

                            txtReturnItemName.Text = "中药组合";
                            txtReturnNum.Tag = alFeeItem;
                            txtRetSpecs.Text = string.Empty;
                            this.backType = "PCC";
                            txtReturnNum.Select();
                            txtReturnNum.Focus();
                        }
                        else
                        {
                            txtReturnNum.Select();
                            txtReturnNum.Focus();
                            txtReturnItemName.Text = f.Item.Name;
                            txtReturnNum.Tag = f;
                            txtRetSpecs.Text = f.Item.Specs;
                        }
                    }
                }
            }

            #endregion

            #region 非药品

            if (this.fpSpread1.ActiveSheet == this.fpSpread1_Sheet2)//退药品
            {
                int currRow = this.fpSpread1_Sheet2.ActiveRowIndex;

                // bool isNeedGroupAllQuit = this.controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.GROUP_ITEM_ALLQUIT, false, false);

                //tmpValue = bValue;

                #region 物资
                //{143CA424-7AF9-493a-8601-2F7B1D635026}
                //是否是多条对照
                List<HISFC.Models.FeeStuff.Output> outitemList = new List<FS.HISFC.Models.FeeStuff.Output>();
                string headerText = this.fpSpread1.ActiveSheet.RowHeader.Cells[currRow, 0].Text;
                if (headerText == "+" || headerText == "-")
                {
                    if (!this.ckbAllQuit.Checked)
                    {
                        if (!this.ckbAllQuit.Checked && headerText != ".")
                        {
                            MessageBox.Show("请选择要退费的物资信息！");
                            if (this.fpSpread1_Sheet2.RowHeader.Cells[currRow, 0].Text == "+")
                            {
                                this.ExpandOrCloseRow(false, currRow + 1);
                            }
                            return -1;
                        }
                    }
                }
                #endregion
                #region 非药品
                if (this.fpSpread1_Sheet2.Rows[currRow].Tag is FeeItemList)
                {
                    FeeItemList f = this.fpSpread1_Sheet2.Rows[currRow].Tag as FeeItemList;

                    if (this.ckbAllQuit.Checked)
                    {
                        if (f.NoBackQty <= 0)
                        {
                            MessageBox.Show(f.Item.Name + "已经没有可退数量，不能再退费!");
                            return -2;
                        }
                        int findRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread2_Sheet2);
                        //没有找到，那么新增一条;
                        if (findRow == -1)
                        {
                            findRow = FindNullRow(this.fpSpread2_Sheet2);
                            FeeItemList fClone = f.Clone();
                            this.fpSpread2_Sheet2.Rows[findRow].Tag = fClone;
                            this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.ItemName].Text = fClone.Item.Name;
                            this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Amount].Text = fClone.FeePack == "1" ?
                                FS.FrameWork.Public.String.FormatNumber(fClone.NoBackQty / fClone.Item.PackQty, 2).ToString() :
                                FS.FrameWork.Public.String.FormatNumber(fClone.NoBackQty, 2).ToString();
                            this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.PriceUnit].Text = fClone.Item.PriceUnit;
                            this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Flag].Text = "未核准";
                        }
                        else //找到了累加数量
                        {
                            FeeItemList fFind = this.fpSpread2_Sheet2.Rows[findRow].Tag as FeeItemList;
                            fFind.Item.Qty = fFind.Item.Qty + f.NoBackQty;
                            this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Amount].Text = fFind.FeePack == "1" ?
                                FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / fFind.Item.PackQty, 2).ToString() :
                                FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty, 2).ToString();
                            this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.ItemName].Text = fFind.Item.Name;
                            this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.PriceUnit].Text = fFind.Item.PriceUnit;
                            this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Flag].Text = "未核准";
                        }
                        f.Item.Qty = f.Item.Qty - f.NoBackQty;
                        f.NoBackQty = 0;

                        f.FT.OwnCost = f.FT.OwnCost - f.FT.OwnCost;
                        f.FT.PubCost = f.FT.PubCost - f.FT.PubCost;
                        f.FT.PayCost = f.FT.PayCost - f.FT.PayCost;

                        f.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2);
                        this.fpSpread1_Sheet2.Cells[currRow, (int)UndrugList.Amount].Text = f.FeePack == "1" ?
                            FS.FrameWork.Public.String.FormatNumber(f.Item.Qty / f.Item.PackQty, 2).ToString() :
                            FS.FrameWork.Public.String.FormatNumber(f.Item.Qty, 2).ToString();
                        this.fpSpread1_Sheet2.Cells[currRow, (int)UndrugList.Cost].Text = f.FT.TotCost.ToString();
                        this.fpSpread1_Sheet2.Cells[currRow, (int)UndrugList.NoBackQty].Text = "0";
                        #region 物资
                        //{143CA424-7AF9-493a-8601-2F7B1D635026}
                        int mateIndex = 0;
                        if (f.MateList.Count > 0)
                        {
                            foreach (HISFC.Models.FeeStuff.Output tempOut in f.MateList)
                            {
                                mateIndex = GetMateRowIndex(tempOut);
                                if (mateIndex == -1)
                                {
                                    MessageBox.Show("查找物资信息失败！");
                                    return -1;
                                }
                                tempOut.StoreBase.Item.Qty = 0;
                                this.fpSpread1_Sheet2.Cells[mateIndex, (int)UndrugList.NoBackQty].Text = "0";
                                this.fpSpread1_Sheet2.Cells[mateIndex, (int)UndrugList.Amount].Text = "0";
                                this.fpSpread1_Sheet2.Cells[mateIndex, (int)UndrugList.Cost].Text = "0";
                                this.fpSpread1_Sheet2.Rows[mateIndex].Tag = tempOut;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        //复合项目
                        if (f.UndrugComb.ID != null && f.UndrugComb.ID.Length > 0 && this.isNeedAllQuit)
                        {
                            ArrayList alFeeItem = new ArrayList();

                            this.currentUndrugComb = this.undrugManager.GetValidItemByUndrugCode(f.UndrugComb.ID);
                            if (this.currentUndrugComb == null)
                            {
                                MessageBox.Show("获得复合项目出错！" + this.undrugManager.Err);

                                return -1;
                            }

                            this.currentUndrugCombs = this.undrugPackAgeManager.QueryUndrugPackagesBypackageCode(this.currentUndrugComb.ID);

                            if (currentUndrugCombs == null)
                            {
                                MessageBox.Show("获得复合项目明细出错！" + this.undrugPackAgeManager.Err);

                                return -1;
                            }

                            for (int i = 0; i < this.fpSpread1_Sheet2.RowCount; i++)
                            {
                                if (this.fpSpread1_Sheet2.Rows[i].Tag is FeeItemList)
                                {
                                    FeeItemList fTemp = this.fpSpread1_Sheet2.Rows[i].Tag as FeeItemList;
                                    if (fTemp.UndrugComb.ID == f.UndrugComb.ID && fTemp.Order.ID == f.Order.ID)
                                    {
                                        alFeeItem.Add(fTemp);
                                    }
                                }
                            }


                            txtReturnItemName.Text = f.UndrugComb.Name;
                            txtReturnNum.Tag = alFeeItem;
                            txtRetSpecs.Text = string.Empty;
                            this.backType = "PACKAGE";
                            txtReturnNum.Select();
                            txtReturnNum.Focus();
                        }
                        else
                        {
                            txtReturnItemName.Text = f.Item.Name;
                            txtReturnNum.Tag = f;
                            txtRetSpecs.Text = f.Item.Specs;
                            this.backType = string.Empty;
                            txtReturnNum.Select();
                            txtReturnNum.Focus();
                        }
                    }
                }
                #endregion
            }

            #endregion

            ComputCost();

            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowIndex">当前选种的行</param>
        /// <returns></returns>
        //{143CA424-7AF9-493a-8601-2F7B1D635026}
        private int QuitOperationMate(int rowIndex)
        {
            int undrugIndex = this.FinItemRowIndex(rowIndex);
            FeeItemList feeItem = this.fpSpread1_Sheet2.Rows[undrugIndex].Tag as FeeItemList;
            FeeItemList f = feeItem.Clone();
            HISFC.Models.FeeStuff.Output outItem = this.fpSpread1_Sheet2.Rows[rowIndex].Tag as HISFC.Models.FeeStuff.Output;
            List<HISFC.Models.FeeStuff.Output> list = new List<FS.HISFC.Models.FeeStuff.Output>();
            list.Add(outItem);
            f.MateList = list;
            int mateListIndex = 0;
            if (this.ckbAllQuit.Checked)
            {
                if (f.NoBackQty <= 0)
                {
                    MessageBox.Show(f.Item.Name + "已经没有可退数量，不能再退费!");

                    return -2;
                }
                int findRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread2_Sheet2);
                //{4D6501CB-D2A4-4204-8CBA-F34F28D5300A}
                FeeItemList fClone = f.Clone();
                f.Item.Qty = f.Item.Qty - (f.FeePack == "1" ? outItem.StoreBase.Item.Qty * f.Item.PackQty : outItem.StoreBase.Item.Qty);
                f.NoBackQty = f.NoBackQty - (f.FeePack == "1" ? outItem.StoreBase.Item.Qty * f.Item.PackQty : outItem.StoreBase.Item.Qty);
                f.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2);
                f.FT.OwnCost = f.FT.TotCost;
                if (findRow == -1)
                {
                    findRow = FindNullRow(this.fpSpread2_Sheet2);
                    //{4D6501CB-D2A4-4204-8CBA-F34F28D5300A}
                    //FeeItemList fClone = f.Clone();
                    fClone.Item.Qty = (f.FeePack == "1" ? outItem.StoreBase.Item.Qty * f.Item.PackQty : outItem.StoreBase.Item.Qty);
                    this.fpSpread2_Sheet2.Rows[findRow].Tag = fClone;
                    this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.ItemName].Text = fClone.Item.Name;
                    this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Amount].Text = fClone.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(fClone.NoBackQty / fClone.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(fClone.NoBackQty, 2).ToString();
                    this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.PriceUnit].Text = fClone.Item.PriceUnit;
                    this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Flag].Text = "未核准";
                }
                else //找到了累加数量
                {
                    FeeItemList fFind = this.fpSpread2_Sheet2.Rows[findRow].Tag as FeeItemList;
                    fFind.Item.Qty = fFind.Item.Qty + (f.FeePack == "1" ? outItem.StoreBase.Item.Qty * f.Item.PackQty : outItem.StoreBase.Item.Qty);
                    this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Amount].Text = fFind.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / fFind.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty, 2).ToString();
                    this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.ItemName].Text = fFind.Item.Name;
                    this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.PriceUnit].Text = fFind.Item.PriceUnit;
                    this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Flag].Text = "未核准";
                    //设置退费费用的物资信息
                    this.SetFeeItemList(fFind, outItem.Clone(), true);
                }

                f.FT.OwnCost = f.FT.OwnCost - f.FT.OwnCost;
                f.FT.PubCost = f.FT.PubCost - f.FT.PubCost;
                f.FT.PayCost = f.FT.PayCost - f.FT.PayCost;

                f.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2);

                this.fpSpread1_Sheet2.Cells[undrugIndex, (int)UndrugList.Amount].Text = f.FeePack == "1" ?
                    FS.FrameWork.Public.String.FormatNumber(f.Item.Qty / f.Item.PackQty, 2).ToString() :
                    FS.FrameWork.Public.String.FormatNumber(f.Item.Qty, 2).ToString();
                this.fpSpread1_Sheet2.Cells[undrugIndex, (int)UndrugList.Cost].Text = f.FT.TotCost.ToString();
                this.fpSpread1_Sheet2.Cells[undrugIndex, (int)UndrugList.NoBackQty].Text = f.NoBackQty.ToString();
                this.fpSpread1_Sheet2.Cells[rowIndex, (int)UndrugList.NoBackQty].Text = "0";
                this.fpSpread1_Sheet2.Cells[rowIndex, (int)UndrugList.Amount].Text = "0";
                this.fpSpread1_Sheet2.Cells[rowIndex, (int)UndrugList.Cost].Text = "0";
                //设置为退费费用的物资信息
                feeItem.Item.Qty = f.Item.Qty;
                feeItem.FT.OwnCost = f.FT.OwnCost;
                feeItem.FT.PubCost = f.FT.PubCost;
                feeItem.FT.PayCost = f.FT.PayCost;
                feeItem.FT.TotCost = f.FT.TotCost;
                feeItem.NoBackQty = f.NoBackQty;
                this.SetFeeItemList(feeItem, outItem, false, ref mateListIndex);
                this.fpSpread1_Sheet2.Rows[rowIndex].Tag = feeItem.MateList[mateListIndex];

            }
            else
            {
                txtReturnItemName.Text = outItem.StoreBase.Item.Name;
                txtReturnNum.Tag = f;
                txtRetSpecs.Text = outItem.StoreBase.Item.Specs;
                this.backType = string.Empty;
                txtReturnNum.Select();
                txtReturnNum.Focus();
            }
            ComputCost();
            return 1;

        }

        /// <summary>
        /// 设置FeeItemList中的物资信息
        /// </summary>
        /// <param name="f">费用信息</param>
        /// <param name="outItem">物资出库</param>
        /// <param name="isQuiteOperation">是否是退费操作</param>
        //{143CA424-7AF9-493a-8601-2F7B1D635026}
        protected virtual void SetFeeItemList(FeeItemList f, HISFC.Models.FeeStuff.Output outItem, bool isQuiteOperation)
        {
            if (f.MateList.Count == 0)
            {
                f.MateList.Add(outItem);
                return;
            }
            bool isFind = false;
            foreach (HISFC.Models.FeeStuff.Output item in f.MateList)
            {
                if (item.ID == outItem.ID && item.StoreBase.StockNO == outItem.StoreBase.StockNO)
                {
                    isFind = true;
                    if (isQuiteOperation)
                    {
                        item.StoreBase.Item.Qty += outItem.StoreBase.Item.Qty;

                    }
                    else
                    {
                        item.StoreBase.Item.Qty -= outItem.StoreBase.Item.Qty;
                    }
                    return;
                }
            }
            if (!isFind)
            {
                f.MateList.Add(outItem);
                return;
            }
        }

        /// <summary>
        /// 设置FeeItemList中的物资信息
        /// </summary>
        /// <param name="f">费用信息</param>
        /// <param name="outItem">物资出库</param>
        /// <param name="isQuiteOperation">是否是退费操作</param>
        //{143CA424-7AF9-493a-8601-2F7B1D635026}
        protected virtual void SetFeeItemList(FeeItemList f, HISFC.Models.FeeStuff.Output outItem, bool isQuiteOperation, ref int mateListindex)
        {
            mateListindex = 0;
            if (f.MateList.Count == 0)
            {
                f.MateList.Add(outItem);
                return;
            }
            bool isFind = false;
            HISFC.Models.FeeStuff.Output item = null;
            for (int i = 0; i < f.MateList.Count; i++)
            {
                item = f.MateList[i];
                if (item.ID == outItem.ID && item.StoreBase.StockNO == outItem.StoreBase.StockNO)
                {
                    isFind = true;
                    if (isQuiteOperation)
                    {
                        item.StoreBase.Item.Qty += outItem.StoreBase.Item.Qty;
                    }
                    else
                    {
                        item.StoreBase.Item.Qty -= outItem.StoreBase.Item.Qty;
                    }
                    mateListindex = i;
                    return;
                }
            }
            if (!isFind)
            {
                f.MateList.Add(outItem);
                mateListindex = f.MateList.Count - 1;
                return;
            }
        }

        /// <summary>
        /// 输入数量退费
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        protected virtual int QuitItemByNum()
        {
            if (this.txtReturnNum.Tag == null)
            {
                MessageBox.Show("请选择项目!");

                return -1;
            }
            decimal quitQty = 0;
            try
            {
                quitQty = FS.FrameWork.Function.NConvert.ToDecimal(this.txtReturnNum.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("数量输入不合法!" + ex.Message);
                this.txtReturnNum.SelectAll();
                this.txtReturnNum.Focus();

                return -1;
            }
            if (quitQty == 0)
            {
                MessageBox.Show("数量不能为零");
                this.txtReturnNum.SelectAll();
                this.txtReturnNum.Focus();

                return -1;
            }
            if (quitQty < 0)
            {
                MessageBox.Show("数量不能为小于零");
                this.txtReturnNum.SelectAll();
                this.txtReturnNum.Focus();

                return -1;
            }

            object objQuit = this.txtReturnNum.Tag;

            #region Tag为单个项目时

            if (objQuit is FeeItemList)
            {
                FeeItemList f = objQuit as FeeItemList;
                //{143CA424-7AF9-493a-8601-2F7B1D635027}
                if (f.MateList.Count > 0)
                {
                    if (quitQty > f.MateList[0].StoreBase.Item.Qty)
                    {
                        MessageBox.Show("输入的数量大于可退数量!");
                        this.txtReturnNum.SelectAll();
                        this.txtReturnNum.Focus();
                        return -1;
                    }
                }
                if (f.FeePack == "1")//包装单位
                {
                    if (quitQty > f.NoBackQty / f.Item.PackQty)
                    {
                        MessageBox.Show("输入的数量大于可退数量!");
                        this.txtReturnNum.SelectAll();
                        this.txtReturnNum.Focus();

                        return -1;
                    }
                }
                else
                {
                    if (quitQty > f.NoBackQty)
                    {
                        MessageBox.Show("输入的数量大于可退数量!");
                        this.txtReturnNum.SelectAll();
                        this.txtReturnNum.Focus();

                        return -1;
                    }
                }
                int currRow = 0;
                //if (f.Item.IsPharmacy)
                if (f.Item.ItemType == EnumItemType.Drug)
                {
                    currRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread1_Sheet1);
                    if (currRow == -1)
                    {
                        MessageBox.Show("查找药品失败！");

                        return -1;
                    }
                    if (f.Item.SysClass.ID.ToString() == "PCC")
                    {
                        decimal doseOnce = (f.NoBackQty - quitQty) / f.Days;

                        (this.fpSpread1_Sheet1.Rows[currRow].Tag as FeeItemList).Order.DoseOnce = doseOnce;

                        this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.DoseAndDays].Text = "每次量:" + FS.FrameWork.Public.String.FormatNumberReturnString(doseOnce, 3) + f.Order.DoseUnit + " " + "付数:" + f.Days.ToString();
                    }
                    else
                    {
                        //{A04F2877-2E51-4d6b-BA7C-6D4010EA4A00}
                        //药品不是草药退费不允许输入小数
                        if (f.Item.SysClass.ID.ToString() != "PCC")
                        {
                            int intQty = FS.FrameWork.Function.NConvert.ToInt32(quitQty);
                            if (quitQty > intQty)
                            {
                                MessageBox.Show("药品退费不可以输入小数,请重新输入！");
                                this.txtReturnNum.SelectAll();
                                this.txtReturnNum.Focus();
                                return -1;
                            }
                        }
                    }
                }
                else
                {
                    currRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread1_Sheet2);
                    if (currRow == -1)
                    {
                        MessageBox.Show("查找非药品失败！");

                        return -1;
                    }
                }

                f.Item.Qty = f.Item.Qty - (f.FeePack == "1" ? quitQty * f.Item.PackQty : quitQty);
                f.NoBackQty = f.NoBackQty - (f.FeePack == "1" ? quitQty * f.Item.PackQty : quitQty);
                f.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2);
                f.FT.OwnCost = f.FT.TotCost;

                //if (f.Item.IsPharmacy)//药品
                if (f.Item.ItemType == EnumItemType.Drug)//药品
                {
                    int findRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread2_Sheet1);
                    //没有找到，那么新增一条;
                    if (findRow == -1)
                    {
                        findRow = FindNullRow(this.fpSpread2_Sheet1);

                        FeeItemList fClone = f.Clone();
                        fClone.Item.Qty = fClone.FeePack == "1" ? quitQty * fClone.Item.PackQty : quitQty;

                        this.fpSpread2_Sheet1.Rows[findRow].Tag = fClone;
                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.ItemName].Text = fClone.Item.Name;
                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Specs].Text = fClone.Item.Specs;
                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Amount].Text = fClone.FeePack == "1" ?
                            FS.FrameWork.Public.String.FormatNumber(fClone.Item.Qty / fClone.Item.PackQty, 2).ToString() :
                            FS.FrameWork.Public.String.FormatNumber(fClone.Item.Qty, 2).ToString();
                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.PriceUnit].Text = fClone.Item.PriceUnit;
                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Flag].Text = "未核准";
                        //显示药品单价，显示金额，但不进行操作
                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Price].Text = fClone.FeePack == "1" ?
                            FS.FrameWork.Public.String.FormatNumber(fClone.Item.Price, 2).ToString() :
                            FS.FrameWork.Public.String.FormatNumber(fClone.Item.Price / fClone.Item.PackQty, 2).ToString();
                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Cost].Text = FS.FrameWork.Public.String.FormatNumber(fClone.Item.Price * fClone.Item.Qty / fClone.Item.PackQty, 2).ToString();

                    }
                    else //找到了累加数量
                    {
                        FeeItemList fFind = this.fpSpread2_Sheet1.Rows[findRow].Tag as FeeItemList;
                        fFind.Item.Qty = fFind.Item.Qty + (fFind.FeePack == "1" ? quitQty * fFind.Item.PackQty : quitQty);
                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Amount].Text = fFind.FeePack == "1" ?
                            FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / fFind.Item.PackQty, 2).ToString() :
                            FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty, 2).ToString();
                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.ItemName].Text = fFind.Item.Name;
                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Specs].Text = fFind.Item.Specs;
                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.PriceUnit].Text = fFind.Item.PriceUnit;
                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Flag].Text = "未核准";
                        //显示药品单价，显示金额，但不进行操作
                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Price].Text = fFind.FeePack == "1" ?
                            FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price, 2).ToString() :
                            FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price / fFind.Item.PackQty, 2).ToString();
                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Cost].Text = FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price * fFind.Item.Qty / fFind.Item.PackQty, 2).ToString();
                    }

                    this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.Amount].Text = f.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(f.Item.Qty / f.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(f.Item.Qty, 2).ToString();
                    this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.Cost].Text = f.FT.TotCost.ToString();
                    this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.NoBackQty].Text = f.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(f.NoBackQty / f.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(f.NoBackQty, 2).ToString();
                }
                else //非药品
                {
                    HISFC.Models.FeeStuff.Output outItem = null;

                    int findRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread2_Sheet2);
                    //没有找到，那么新增一条;
                    if (findRow == -1)
                    {
                        findRow = FindNullRow(this.fpSpread2_Sheet2);

                        FeeItemList fClone = f.Clone();
                        fClone.Item.Qty = fClone.FeePack == "1" ? quitQty * fClone.Item.PackQty : quitQty;

                        //fClone.NoBackQty = f.NoBackQty - (f.FeePack == "1" ? quitQty * f.Item.PackQty : quitQty);
                        fClone.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(fClone.Item.Price * fClone.Item.Qty / fClone.Item.PackQty, 2);
                        fClone.FT.OwnCost = fClone.FT.TotCost;

                        this.fpSpread2_Sheet2.Rows[findRow].Tag = fClone;
                        this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.ItemName].Text = fClone.Item.Name;
                        this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Amount].Text = fClone.FeePack == "1" ?
                            FS.FrameWork.Public.String.FormatNumber(fClone.Item.Qty / fClone.Item.PackQty, 2).ToString() :
                            FS.FrameWork.Public.String.FormatNumber(fClone.Item.Qty, 2).ToString();
                        this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.PriceUnit].Text = fClone.Item.PriceUnit;
                        this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Flag].Text = "未核准";
                        //{143CA424-7AF9-493a-8601-2F7B1D635026}
                        //物资收费
                        if (fClone.MateList.Count > 0)
                        {
                            outItem = fClone.MateList[0];
                            outItem.StoreBase.Item.Qty = quitQty;
                        }

                    }
                    else //找到了累加数量
                    {
                        FeeItemList fFind = this.fpSpread2_Sheet2.Rows[findRow].Tag as FeeItemList;
                        fFind.Item.Qty = fFind.Item.Qty + (fFind.FeePack == "1" ? quitQty * fFind.Item.PackQty : quitQty);
                        fFind.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price * fFind.Item.Qty / fFind.Item.PackQty, 2);
                        fFind.FT.OwnCost = fFind.FT.TotCost;
                        this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Amount].Text = fFind.FeePack == "1" ?
                            FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / fFind.Item.PackQty, 2).ToString() :
                            FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty, 2).ToString();
                        this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.ItemName].Text = fFind.Item.Name;
                        this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.PriceUnit].Text = fFind.Item.PriceUnit;
                        this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Flag].Text = "未核准";
                        //{143CA424-7AF9-493a-8601-2F7B1D635026}
                        //物资收费
                        if (f.MateList.Count > 0)
                        {
                            HISFC.Models.FeeStuff.Output tempoutItem = f.MateList[0].Clone();
                            tempoutItem.StoreBase.Item.Qty = quitQty;
                            this.SetFeeItemList(fFind, tempoutItem, true);
                        }
                    }


                    this.fpSpread1_Sheet2.Cells[currRow, (int)UndrugList.Amount].Text = f.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(f.Item.Qty / f.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(f.Item.Qty, 2).ToString();
                    this.fpSpread1_Sheet2.Cells[currRow, (int)UndrugList.Cost].Text = f.FT.TotCost.ToString();
                    this.fpSpread1_Sheet2.Cells[currRow, (int)UndrugList.NoBackQty].Text = f.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(f.NoBackQty / f.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(f.NoBackQty, 2).ToString();
                    //处理物资
                    //{143CA424-7AF9-493a-8601-2F7B1D635026}
                    FeeItemList tempItemList = this.fpSpread1_Sheet2.Rows[currRow].Tag as FeeItemList;
                    tempItemList.Item.Qty = f.Item.Qty;
                    tempItemList.NoBackQty = f.NoBackQty;
                    tempItemList.FT.TotCost = f.FT.TotCost;
                    tempItemList.FT.OwnCost = f.FT.OwnCost;

                    if (f.MateList.Count > 0)
                    {
                        outItem = f.MateList[0].Clone();

                        outItem.StoreBase.Item.Qty = quitQty;

                        int matelistIndex = 0;
                        this.SetFeeItemList(tempItemList, outItem, false, ref matelistIndex);

                        int mateRow = GetMateRowIndex(outItem);
                        outItem = tempItemList.MateList[matelistIndex];

                        this.fpSpread1_Sheet2.Cells[mateRow, (int)UndrugList.Amount].Text = outItem.StoreBase.Item.Qty.ToString();
                        this.fpSpread1_Sheet2.Cells[mateRow, (int)UndrugList.Cost].Text = (outItem.StoreBase.Item.Qty * outItem.StoreBase.Item.Price).ToString();
                        this.fpSpread1_Sheet2.Cells[mateRow, (int)UndrugList.NoBackQty].Text = outItem.StoreBase.Item.Qty.ToString();

                        this.fpSpread1_Sheet2.Rows[mateRow].Tag = outItem;
                    }
                }

            }

            #endregion

            else if (objQuit is ArrayList)
            {
                ArrayList alTemp = objQuit as ArrayList;

                if (this.backType == "PACKAGE")
                {
                    foreach (FeeItemList item in alTemp)
                    {
                        FS.HISFC.Models.Fee.Item.UndrugComb info = null;

                        foreach (FS.HISFC.Models.Fee.Item.UndrugComb undrugComb in this.currentUndrugCombs)
                        {
                            if (undrugComb.ID == item.ID)
                            {
                                info = undrugComb;

                                break;
                            }
                        }

                        if (info == null)
                        {
                            MessageBox.Show("新维护的组套中没有" + item.Item.Name + "请执行全退");

                            return -1;
                        }

                        #region 处理明细

                        FeeItemList f = item;
                        if (f.FeePack == "1")//包装单位
                        {
                            if (quitQty * info.Qty > f.NoBackQty / f.Item.PackQty)
                            {
                                MessageBox.Show("输入的数量大于可退数量!");
                                this.txtReturnNum.SelectAll();
                                this.txtReturnNum.Focus();

                                return -1;
                            }
                        }
                        else
                        {
                            if (quitQty * info.Qty > f.NoBackQty)
                            {
                                MessageBox.Show("输入的数量大于可退数量!");
                                this.txtReturnNum.SelectAll();
                                this.txtReturnNum.Focus();

                                return -1;
                            }
                        }
                        int currRow = 0;
                        //if (!f.Item.IsPharmacy)
                        if (f.Item.ItemType != EnumItemType.Drug)
                        {
                            currRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread1_Sheet2);
                            if (currRow == -1)
                            {
                                MessageBox.Show("查找非药品失败！");

                                return -1;
                            }
                        }

                        f.Item.Qty = f.Item.Qty - (f.FeePack == "1" ? quitQty * f.Item.PackQty * info.Qty : quitQty * info.Qty);
                        f.NoBackQty = f.NoBackQty - (f.FeePack == "1" ? quitQty * f.Item.PackQty * info.Qty : quitQty * info.Qty);
                        f.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2);

                        //if (!f.Item.IsPharmacy) //非药品
                        if (f.Item.ItemType != EnumItemType.Drug)
                        {
                            int findRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread2_Sheet2);
                            //没有找到，那么新增一条;
                            if (findRow == -1)
                            {
                                findRow = FindNullRow(this.fpSpread2_Sheet2);

                                FeeItemList fClone = f.Clone();
                                fClone.Item.Qty = fClone.FeePack == "1" ? quitQty * fClone.Item.PackQty * info.Qty : quitQty * info.Qty;

                                this.fpSpread2_Sheet2.Rows[findRow].Tag = fClone;
                                this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.ItemName].Text = fClone.Item.Name;
                                this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Amount].Text = fClone.FeePack == "1" ?
                                    FS.FrameWork.Public.String.FormatNumber(fClone.Item.Qty / fClone.Item.PackQty, 2).ToString() :
                                    FS.FrameWork.Public.String.FormatNumber(fClone.Item.Qty, 2).ToString();
                                this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.PriceUnit].Text = fClone.Item.PriceUnit;
                                this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Flag].Text = "未核准";
                            }
                            else //找到了累加数量
                            {
                                FeeItemList fFind = this.fpSpread2_Sheet2.Rows[findRow].Tag as FeeItemList;
                                fFind.Item.Qty = fFind.Item.Qty + (fFind.FeePack == "1" ? quitQty * fFind.Item.PackQty * info.Qty : quitQty * info.Qty);
                                this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Amount].Text = fFind.FeePack == "1" ?
                                    FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / fFind.Item.PackQty, 2).ToString() :
                                    FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty, 2).ToString();
                                this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.ItemName].Text = fFind.Item.Name;
                                this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.PriceUnit].Text = fFind.Item.PriceUnit;
                                this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Flag].Text = "未核准";
                            }

                            this.fpSpread1_Sheet2.Cells[currRow, (int)UndrugList.Amount].Text = f.FeePack == "1" ?
                                FS.FrameWork.Public.String.FormatNumber(f.Item.Qty / f.Item.PackQty, 2).ToString() :
                                FS.FrameWork.Public.String.FormatNumber(f.Item.Qty, 2).ToString();
                            this.fpSpread1_Sheet2.Cells[currRow, (int)UndrugList.Cost].Text = f.FT.TotCost.ToString();
                            this.fpSpread1_Sheet2.Cells[currRow, (int)UndrugList.NoBackQty].Text = f.FeePack == "1" ?
                                FS.FrameWork.Public.String.FormatNumber(f.NoBackQty / f.Item.PackQty, 2).ToString() :
                                FS.FrameWork.Public.String.FormatNumber(f.NoBackQty, 2).ToString();
                        }

                        #endregion
                    }
                }
                if (this.backType == "PCC")
                {
                    foreach (FeeItemList item in alTemp)
                    {
                        #region 处理明细

                        FeeItemList f = item;
                        if (f.FeePack == "1")//包装单位
                        {
                            if (quitQty * f.Order.DoseOnce > f.NoBackQty / f.Item.PackQty)
                            {
                                MessageBox.Show("输入的数量大于可退数量!");
                                this.txtReturnNum.SelectAll();
                                this.txtReturnNum.Focus();

                                return -1;
                            }
                        }
                        else
                        {
                            if (quitQty * f.Order.DoseOnce > f.NoBackQty)
                            {
                                MessageBox.Show("输入的数量大于可退数量!");
                                this.txtReturnNum.SelectAll();
                                this.txtReturnNum.Focus();

                                return -1;
                            }
                        }
                        int currRow = 0;
                        //if (f.Item.IsPharmacy)
                        if (f.Item.ItemType == EnumItemType.Drug)
                        {
                            currRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread1_Sheet1);
                            if (currRow == -1)
                            {
                                MessageBox.Show("查找药品失败！");

                                return -1;
                            }
                        }

                        f.Item.Qty = f.Item.Qty - (f.FeePack == "1" ? quitQty * f.Item.PackQty * f.Order.DoseOnce : quitQty * f.Order.DoseOnce);
                        f.NoBackQty = f.NoBackQty - (f.FeePack == "1" ? quitQty * f.Item.PackQty * f.Order.DoseOnce : quitQty * f.Order.DoseOnce);
                        f.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2);

                        //if (f.Item.IsPharmacy) //非药品
                        if (f.Item.ItemType == EnumItemType.Drug) //非药品
                        {
                            int findRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread2_Sheet1);
                            //没有找到，那么新增一条;
                            if (findRow == -1)
                            {
                                findRow = FindNullRow(this.fpSpread2_Sheet1);

                                FeeItemList fClone = f.Clone();
                                fClone.Item.Qty = fClone.FeePack == "1" ? quitQty * fClone.Item.PackQty * f.Order.DoseOnce : quitQty * f.Order.DoseOnce;

                                this.fpSpread2_Sheet1.Rows[findRow].Tag = fClone;
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.ItemName].Text = fClone.Item.Name;
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Amount].Text = fClone.FeePack == "1" ?
                                    FS.FrameWork.Public.String.FormatNumber(fClone.Item.Qty / fClone.Item.PackQty, 2).ToString() :
                                    FS.FrameWork.Public.String.FormatNumber(fClone.Item.Qty, 2).ToString();
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.PriceUnit].Text = fClone.Item.PriceUnit;
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Flag].Text = "未核准";
                                //显示药品单价，显示金额，但不进行操作
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Price].Text = fClone.FeePack == "1" ?
                                    FS.FrameWork.Public.String.FormatNumber(fClone.Item.Price, 2).ToString() :
                                    FS.FrameWork.Public.String.FormatNumber(fClone.Item.Price / fClone.Item.PackQty, 2).ToString();
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Cost].Text = FS.FrameWork.Public.String.FormatNumber(fClone.Item.Price * fClone.Item.Qty / fClone.Item.PackQty, 2).ToString();
                            }
                            else //找到了累加数量
                            {
                                FeeItemList fFind = this.fpSpread2_Sheet1.Rows[findRow].Tag as FeeItemList;
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.ItemName].Text = fFind.Item.Name;
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.PriceUnit].Text = fFind.Item.PriceUnit;
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Flag].Text = "未核准";
                                fFind.Item.Qty = fFind.Item.Qty + (fFind.FeePack == "1" ? quitQty * fFind.Item.PackQty * fFind.Order.DoseOnce : quitQty * fFind.Order.DoseOnce);
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Amount].Text = fFind.FeePack == "1" ?
                                    FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / fFind.Item.PackQty, 2).ToString() :
                                    FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty, 2).ToString();
                                //显示药品单价，显示金额，但不进行操作
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Price].Text = fFind.FeePack == "1" ?
                                    FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price, 2).ToString() :
                                    FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price / fFind.Item.PackQty, 2).ToString();
                                this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Cost].Text = FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price * fFind.Item.Qty / fFind.Item.PackQty, 2).ToString();
                            }

                            this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.Amount].Text = f.FeePack == "1" ?
                                FS.FrameWork.Public.String.FormatNumber(f.Item.Qty / f.Item.PackQty, 2).ToString() :
                                FS.FrameWork.Public.String.FormatNumber(f.Item.Qty, 2).ToString();
                            this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.Cost].Text = f.FT.TotCost.ToString();
                            this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.NoBackQty].Text = f.FeePack == "1" ?
                                FS.FrameWork.Public.String.FormatNumber(f.NoBackQty / f.Item.PackQty, 2).ToString() :
                                FS.FrameWork.Public.String.FormatNumber(f.NoBackQty, 2).ToString();
                        }

                        #endregion
                    }
                }
            }

            this.fpSpread1.Select();
            this.fpSpread1.Focus();
            if (this.fpSpread1.ActiveSheet.RowCount > 0)
            {
                this.fpSpread1.ActiveSheet.ActiveRowIndex = 0;
            }

            ComputCost();

            return 1;
        }

        /// <summary>
        /// 查找物资所在fp中的行
        /// </summary>
        /// <param name="outItem">物资出库信息</param>
        /// <returns>-1  失败</returns>
        protected virtual int GetMateRowIndex(HISFC.Models.FeeStuff.Output outItem)
        {
            string headText = string.Empty;
            HISFC.Models.FeeStuff.Output tempOut = null;
            for (int i = 0; i < this.fpSpread1_Sheet2.Rows.Count; i++)
            {
                headText = this.fpSpread1_Sheet2.RowHeader.Cells[i, 0].Text;
                if (headText != ".") continue;
                tempOut = this.fpSpread1_Sheet2.Rows[i].Tag as HISFC.Models.FeeStuff.Output;
                if (tempOut.StoreBase.StockNO == outItem.StoreBase.StockNO &&
                    tempOut.ID == outItem.ID)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// 处理取消退费操作
        /// </summary>
        protected virtual void DealCancelQuitOperation()
        {

            if (this.fpSpread2.ActiveSheet == this.fpSpread2_Sheet2)//非药品
            {
                int currRow = this.fpSpread2_Sheet2.ActiveRowIndex;

                if (this.fpSpread2_Sheet2.Rows[currRow].Tag is FeeItemList)
                {
                    FeeItemList f = this.fpSpread2_Sheet2.Rows[currRow].Tag as FeeItemList;
                    if (f.UndrugComb.ID != string.Empty && isNeedAllQuit)
                    {
                        for (int i = 0; i < this.fpSpread2_Sheet2.RowCount; i++)
                        {
                            if (this.fpSpread2_Sheet2.Rows[currRow].Tag is FeeItemList)
                            {
                                FeeItemList fTemp = this.fpSpread2_Sheet2.Rows[i].Tag as FeeItemList;
                                if (fTemp != null && fTemp.UndrugComb.ID == f.UndrugComb.ID && f.Order.ID == fTemp.Order.ID)
                                {
                                    CancelUndrugQuitOperation(i);
                                }
                            }
                        }
                        return;
                    }
                    else
                    {
                        CancelQuitOperation();
                    }
                }
            }
            else
            {
                if (isPharmacySameRecipeQuitAll == false)
                {
                    int currRow = this.fpSpread2_Sheet1.ActiveRowIndex;

                    if (this.fpSpread2_Sheet1.Rows[currRow].Tag is FeeItemList)
                    {
                        FeeItemList f = this.fpSpread2_Sheet1.Rows[currRow].Tag as FeeItemList;

                        if (f.Item.SysClass.ID.ToString() == "PCC" && f.Order.Combo.ID.Length > 0 && this.isNeedAllQuit)
                        {
                            for (int i = 0; i < this.fpSpread2_Sheet1.RowCount; i++)
                            {
                                if (this.fpSpread2_Sheet1.Rows[i].Tag is FeeItemList)
                                {
                                    FeeItemList fTemp = this.fpSpread2_Sheet1.Rows[i].Tag as FeeItemList;
                                    if (fTemp.Item.SysClass.ID.ToString() == "PCC" && fTemp.Order.Combo.ID == f.Order.Combo.ID)
                                    {
                                        CancelQuitDrugOperation(i);
                                    }
                                }
                            }
                        }
                        else
                        {
                            CancelQuitOperation();
                        }
                    }
                }
                else
                {
                    int currRow = this.fpSpread2_Sheet1.ActiveRowIndex;

                    if (this.fpSpread2_Sheet1.Rows[currRow].Tag is FeeItemList)
                    {
                        FeeItemList f = this.fpSpread2_Sheet1.Rows[currRow].Tag as FeeItemList;

                        if (true)
                        {
                            for (int i = 0; i < this.fpSpread2_Sheet1.RowCount; i++)
                            {
                                if (this.fpSpread2_Sheet1.Rows[i].Tag is FeeItemList)
                                {
                                    FeeItemList fTemp = this.fpSpread2_Sheet1.Rows[i].Tag as FeeItemList;
                                    if (fTemp.RecipeNO == f.RecipeNO)
                                    {
                                        CancelQuitDrugOperation(i);
                                    }
                                }
                            }
                        }
                        else
                        {
                            CancelQuitOperation();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 处理取消已退非药品操作
        /// </summary>
        /// <param name="currRow">当前行</param>
        /// <returns>成功 1 失败 -1</returns>
        protected virtual int CancelUndrugQuitOperation(int currRow)
        {
            if (this.fpSpread2_Sheet2.Rows[currRow].Tag == null)
            {
                return -1;
            }
            if (this.fpSpread2_Sheet2.Rows[currRow].Tag is FS.HISFC.Models.Fee.ReturnApply)
            {
                MessageBox.Show("已经核准非药品不能取消!");

                return -1;
            }
            if (this.fpSpread2_Sheet2.Rows[currRow].Tag is FeeItemList)
            {
                FeeItemList f = this.fpSpread2_Sheet2.Rows[currRow].Tag as FeeItemList;

                int findRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread1_Sheet2);

                if (findRow == -1)
                {
                    MessageBox.Show("查找未退非药品失败!");

                    return -1;
                }
                FeeItemList fFind = this.fpSpread1_Sheet2.Rows[findRow].Tag as FeeItemList;
                fFind.Item.Qty += f.Item.Qty;
                fFind.NoBackQty += f.Item.Qty;
                if (ITruncFee != null)
                {
                    fFind.FT.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(ITruncFee.TruncFee(fFind.Item.Price * fFind.Item.Qty / fFind.Item.PackQty));
                }
                else
                {
                    fFind.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price * fFind.Item.Qty / fFind.Item.PackQty, 2);
                }
                this.fpSpread1_Sheet2.Cells[findRow, (int)UndrugList.Amount].Text = fFind.FeePack == "1" ?
                    FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / fFind.Item.PackQty, 2).ToString() :
                    FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty, 2).ToString();
                this.fpSpread1_Sheet2.Cells[findRow, (int)UndrugList.Cost].Text = fFind.FT.TotCost.ToString();
                this.fpSpread1_Sheet2.Cells[findRow, (int)UndrugList.NoBackQty].Text = fFind.FeePack == "1" ?
                    FS.FrameWork.Public.String.FormatNumber(fFind.NoBackQty / fFind.Item.PackQty, 2).ToString() :
                    FS.FrameWork.Public.String.FormatNumber(fFind.NoBackQty, 2).ToString();
                f.Item.Qty = 0;
                this.fpSpread2_Sheet2.Cells[currRow, (int)UndrugListQuit.Amount].Text = string.Empty;
                this.fpSpread2_Sheet2.Cells[currRow, (int)UndrugListQuit.Flag].Text = string.Empty;
                this.fpSpread2_Sheet2.Cells[currRow, (int)UndrugListQuit.ItemName].Text = string.Empty;
                this.fpSpread2_Sheet2.Cells[currRow, (int)UndrugListQuit.PriceUnit].Text = string.Empty;
            }

            ComputCost();

            return 1;
        }

        /// <summary>
        /// 处理药品取消退费
        /// </summary>
        /// <param name="currRow">当前行</param>
        /// <returns>成功 1 失败 -1</returns>
        protected virtual int CancelQuitDrugOperation(int currRow)
        {
            if (this.fpSpread2_Sheet1.Rows[currRow].Tag == null)
            {
                return -1;
            }
            if (this.fpSpread2_Sheet1.Rows[currRow].Tag is FS.HISFC.Models.Fee.ReturnApply)
            {
                MessageBox.Show("已经核准药品不能取消!");

                return -1;
            }
            if (this.fpSpread2_Sheet1.Rows[currRow].Tag is FeeItemList)
            {
                FeeItemList f = this.fpSpread2_Sheet1.Rows[currRow].Tag as FeeItemList;

                int findRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread1_Sheet1);

                if (findRow == -1)
                {
                    MessageBox.Show("查找未退药品失败!");

                    return -1;
                }
                FeeItemList fFind = this.fpSpread1_Sheet1.Rows[findRow].Tag as FeeItemList;
                fFind.Item.Qty += f.Item.Qty;
                fFind.NoBackQty += f.Item.Qty;
                if (ITruncFee != null)
                {
                    fFind.FT.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(ITruncFee.TruncFee(fFind.Item.Price * fFind.Item.Qty / fFind.Item.PackQty));
                }
                else
                {
                    fFind.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price * fFind.Item.Qty / fFind.Item.PackQty, 2);
                }
                fFind.FT.OwnCost = fFind.FT.TotCost;
                this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.Amount].Text = fFind.FeePack == "1" ?
                    FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / fFind.Item.PackQty, 2).ToString() :
                    FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty, 2).ToString();
                this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.Cost].Text = fFind.FT.TotCost.ToString();
                this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.NoBackQty].Text = fFind.FeePack == "1" ?
                    FS.FrameWork.Public.String.FormatNumber(fFind.NoBackQty / fFind.Item.PackQty, 2).ToString() :
                    FS.FrameWork.Public.String.FormatNumber(fFind.NoBackQty, 2).ToString();
                f.Item.Qty = 0;

                this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.Amount].Text = string.Empty;
                this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.Flag].Text = string.Empty;
                this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.ItemName].Text = string.Empty;
                this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.PriceUnit].Text = string.Empty;
                this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.Specs].Text = string.Empty;
                //显示单价，金额为空；
                this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.Price].Text = string.Empty;
                this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.Cost].Text = string.Empty;
            }

            ComputCost();

            return 0;
        }

        /// <summary>
        /// 取消退费操作
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        protected virtual int CancelQuitOperation()
        {
            if (this.fpSpread2.ActiveSheet == this.fpSpread2_Sheet1)//药品
            {
                int currRow = this.fpSpread2_Sheet1.ActiveRowIndex;

                if (this.fpSpread2_Sheet1.Rows[currRow].Tag == null)
                {
                    return -1;
                }
                if (this.fpSpread2_Sheet1.Rows[currRow].Tag is FS.HISFC.Models.Fee.ReturnApply)
                {
                    MessageBox.Show("已经核准药品不能取消!");

                    return -1;
                }
                if (this.fpSpread2_Sheet1.Rows[currRow].Tag is FeeItemList)
                {
                    FeeItemList f = this.fpSpread2_Sheet1.Rows[currRow].Tag as FeeItemList;

                    int findRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread1_Sheet1);

                    if (findRow == -1)
                    {
                        MessageBox.Show("查找未退药品失败!");

                        return -1;
                    }
                    FeeItemList fFind = this.fpSpread1_Sheet1.Rows[findRow].Tag as FeeItemList;
                    fFind.Item.Qty += f.Item.Qty;
                    fFind.NoBackQty += f.Item.Qty;
                    if (ITruncFee != null)
                    {
                        fFind.FT.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(ITruncFee.TruncFee(fFind.Item.Price * fFind.Item.Qty / fFind.Item.PackQty)) - fFind.FT.RebateCost;
                        //fFind.FT.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(ITruncFee.TruncFee(fFind.Item.Price * fFind.Item.Qty / fFind.Item.PackQty)) - fFind.FT.RebateCost;
                    }
                    else
                    {
                        fFind.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price * fFind.Item.Qty / fFind.Item.PackQty, 2);
                        //fFind.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price * fFind.Item.Qty / fFind.Item.PackQty, 2) - fFind.FT.RebateCost;
                    }

                    fFind.FT.OwnCost = fFind.FT.TotCost;
                    //fFind.FT.TotCost += f.FT.TotCost;
                    //fFind.FT.PubCost += f.FT.PubCost;
                    //fFind.FT.PayCost += f.FT.PayCost;
                    //fFind.FT.OwnCost += f.FT.OwnCost;

                    this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.Amount].Text = fFind.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / fFind.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty, 2).ToString();
                    this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.Cost].Text = fFind.FT.TotCost.ToString();
                    this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.NoBackQty].Text = fFind.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(fFind.NoBackQty / fFind.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(fFind.NoBackQty, 2).ToString();
                    f.Item.Qty = 0;
                    if (f.Item.SysClass.ID.ToString() == "PCC")
                    {
                        decimal doseOnce = (fFind.NoBackQty) / fFind.Days;

                        (this.fpSpread1_Sheet1.Rows[findRow].Tag as FeeItemList).Order.DoseOnce = doseOnce;

                        this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.DoseAndDays].Text = "每次量:" + FS.FrameWork.Public.String.FormatNumberReturnString(doseOnce, 3) + f.Order.DoseUnit + " " + "付数:" + f.Days.ToString();
                    }
                    this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.Amount].Text = "0";
                    this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.Amount].Text = string.Empty;
                    this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.Flag].Text = string.Empty;
                    this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.ItemName].Text = string.Empty;
                    this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.PriceUnit].Text = string.Empty;
                    this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.Specs].Text = string.Empty;
                    //显示单价，金额为空；
                    this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.Price].Text = string.Empty;
                    this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.Cost].Text = string.Empty;
                    //{433AA56A-264F-4c8c-BC7E-52DAEAFDC605}
                    this.fpSpread2_Sheet1.Rows[currRow].Tag = null;
                    this.fpSpread1_Sheet1.Rows[findRow].Tag = fFind;
                }
            }
            if (this.fpSpread2.ActiveSheet == this.fpSpread2_Sheet2)//非药品
            {
                int currRow = this.fpSpread2_Sheet2.ActiveRowIndex;

                if (this.fpSpread2_Sheet2.Rows[currRow].Tag == null)
                {
                    return -1;
                }
                if (this.fpSpread2_Sheet2.Rows[currRow].Tag is FS.HISFC.Models.Fee.ReturnApply)
                {
                    MessageBox.Show("已经核准非药品不能取消!");

                    return -1;
                }
                if (this.fpSpread2_Sheet2.Rows[currRow].Tag is FeeItemList)
                {
                    FeeItemList f = this.fpSpread2_Sheet2.Rows[currRow].Tag as FeeItemList;

                    int findRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread1_Sheet2);

                    if (findRow == -1)
                    {
                        MessageBox.Show("查找未退非药品失败!");

                        return -1;
                    }
                    FeeItemList fFind = this.fpSpread1_Sheet2.Rows[findRow].Tag as FeeItemList;
                    fFind.Item.Qty += f.Item.Qty;
                    fFind.NoBackQty += f.Item.Qty;
                    if (ITruncFee != null)//{DD31280F-7321-42BB-B150-4C63018ED85F}
                    {
                        fFind.FT.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(ITruncFee.TruncFee(fFind.Item.Price * fFind.Item.Qty / fFind.Item.PackQty));
                        //fFind.FT.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(ITruncFee.TruncFee(fFind.Item.Price * fFind.Item.Qty / fFind.Item.PackQty)) - fFind.FT.RebateCost;
                    }
                    else
                    {
                        fFind.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price * fFind.Item.Qty / fFind.Item.PackQty, 2);
                        //fFind.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price * fFind.Item.Qty / fFind.Item.PackQty, 2) - fFind.FT.RebateCost;
                    }
                    fFind.FT.OwnCost = fFind.FT.TotCost;
                    //fFind.FT.TotCost += f.FT.TotCost;
                    //fFind.FT.PubCost += f.FT.PubCost;
                    //fFind.FT.PayCost += f.FT.PayCost;
                    //fFind.FT.OwnCost += f.FT.OwnCost; 
                    this.fpSpread1_Sheet2.Cells[findRow, (int)UndrugList.Amount].Text = fFind.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / fFind.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty, 2).ToString();
                    this.fpSpread1_Sheet2.Cells[findRow, (int)UndrugList.Cost].Text = fFind.FT.TotCost.ToString();
                    this.fpSpread1_Sheet2.Cells[findRow, (int)UndrugList.NoBackQty].Text = fFind.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(fFind.NoBackQty / fFind.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(fFind.NoBackQty, 2).ToString();
                    f.Item.Qty = 0;
                    //this.fpSpread2_Sheet2.Cells[currRow,(int)UndrugListQuit.Item.Qty].Text = "0";
                    this.fpSpread2_Sheet2.Cells[currRow, (int)UndrugListQuit.Amount].Text = string.Empty;
                    this.fpSpread2_Sheet2.Cells[currRow, (int)UndrugListQuit.Flag].Text = string.Empty;
                    this.fpSpread2_Sheet2.Cells[currRow, (int)UndrugListQuit.ItemName].Text = string.Empty;
                    this.fpSpread2_Sheet2.Cells[currRow, (int)UndrugListQuit.PriceUnit].Text = string.Empty;
                    this.fpSpread2_Sheet2.Rows[currRow].Tag = null;
                    #region 物资
                    //{143CA424-7AF9-493a-8601-2F7B1D635026}
                    int mateListIndex = 0;
                    if (f.MateList.Count > 0)
                    {
                        int mateIndex = 0;
                        foreach (HISFC.Models.FeeStuff.Output outItem in f.MateList)
                        {
                            mateIndex = this.GetMateRowIndex(outItem);
                            if (mateIndex == -1)
                            {
                                MessageBox.Show("查找物资信息失败！");
                                return -1;
                            }
                            this.SetFeeItemList(fFind, outItem, true, ref mateListIndex);

                            this.fpSpread1_Sheet2.Cells[mateIndex, (int)UndrugList.Amount].Text = fFind.MateList[mateListIndex].StoreBase.Item.Qty.ToString();
                            this.fpSpread1_Sheet2.Cells[mateIndex, (int)UndrugList.Cost].Text = (fFind.MateList[mateListIndex].StoreBase.Item.Qty * fFind.MateList[mateListIndex].StoreBase.Item.Price).ToString();
                            this.fpSpread1_Sheet2.Cells[mateIndex, (int)UndrugList.NoBackQty].Text = fFind.MateList[mateListIndex].StoreBase.Item.Qty.ToString();
                            this.fpSpread1_Sheet2.Rows[mateIndex].Tag = fFind.MateList[mateListIndex];
                        }
                    }
                    #endregion
                }
            }

            ComputCost();

            return 1;
        }

        /// <summary>
        /// 是否有效
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        protected virtual bool IsValid()
        {
            if (this.quitInvoices == null || this.quitInvoices.Count == 0)
            {
                MessageBox.Show("请输入发票信息");

                return false;
            }

            if (!IsQuitItem())
            {
                MessageBox.Show("请选择退费项目!");

                return false;
            }

            bool isCanQuitOtherOper = this.controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.CAN_QUIT_OTHER_OPER_INVOICE, true, false);
            if (!isCanQuitOtherOper)//不予许交叉退费
            {
                Balance tmpInvoice = quitInvoices[0] as Balance;
                if (tmpInvoice == null)
                {
                    MessageBox.Show("发票格式转换出错!");
                    this.tbInvoiceNO.SelectAll();
                    tbInvoiceNO.Focus();

                    return false;
                }

                if (tmpInvoice.BalanceOper.ID != this.outpatientManager.Operator.ID)
                {
                    MessageBox.Show("该发票为操作员" + tmpInvoice.BalanceOper.ID + "收取,您没有权限进重打!");
                    tbInvoiceNO.SelectAll();
                    tbInvoiceNO.Focus();

                    return false;
                }
            }

            bool isCanQuitDayBalanced = this.controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.CAN_QUIT_DAYBALANCED_INVOICE, true, false);
            if (!isCanQuitDayBalanced)//不予许退费日结后费用
            {
                Balance tmpInvoice = quitInvoices[0] as Balance;
                if (tmpInvoice == null)
                {
                    MessageBox.Show("发票格式转换出错!");
                    this.tbInvoiceNO.SelectAll();
                    tbInvoiceNO.Focus();

                    return false;
                }

                if (tmpInvoice.IsDayBalanced)
                {
                    MessageBox.Show("该发票已经日结,不能进行退费!");
                    tbInvoiceNO.SelectAll();
                    tbInvoiceNO.Focus();

                    return false;
                }
            }

            Balance invoice = quitInvoices[0] as Balance;
            if (invoice != null && invoice.IsAccount)
            {
                if (!IsAllQuit() && !this.isAllowQuitFeeHalf)
                {
                    MessageBox.Show("账户集中打印的发票必须全退！");

                    return false;
                }
            }

            if (this.patient.Pact.PayKind.ID == "02")//医保患者需要全退!
            {
                bool isSICanHalfQuit = this.controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.SI_CAN_HALF_QUIT, false, false);
                if (!isSICanHalfQuit)
                {
                    if (!IsAllQuit())
                    {
                        MessageBox.Show("公费或者医保患者要求全退!还有未退光的费用!");

                        return false;
                    }
                }
            }
            if (this.patient.Pact.PayKind.ID == "03")//公费患者需要全退!
            {
                string tmpControl = this.feeIntegrate.GetControlValue(FS.HISFC.BizProcess.Integrate.Const.PUB_CAN_HALF_QUIT, "0");
                if (tmpControl == "0")
                {
                    if (!IsAllQuit())
                    {
                        MessageBox.Show("公费或者医保患者要求全退!还有未退光的费用!");

                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 是否项目全退
        /// </summary>
        /// <returns>成功 true 失败 false</returns>
        protected virtual bool IsQuitItem()
        {
            decimal qty = 0;

            for (int i = 0; i < this.fpSpread2_Sheet1.RowCount; i++)
            {
                if (this.fpSpread2_Sheet1.Rows[i].Tag != null)
                {
                    if (this.fpSpread2_Sheet1.Rows[i].Tag is FeeItemList)
                    {
                        FeeItemList fTemp = this.fpSpread2_Sheet1.Rows[i].Tag as FeeItemList;

                        qty += fTemp.Item.Qty;
                    }
                }
                if (this.fpSpread2_Sheet1.Rows[i].Tag is FS.HISFC.Models.Fee.ReturnApply)
                {
                    FS.HISFC.Models.Fee.ReturnApply fTemp = this.fpSpread2_Sheet1.Rows[i].Tag as FS.HISFC.Models.Fee.ReturnApply;

                    qty += fTemp.Item.Qty;
                }
            }
            for (int i = 0; i < this.fpSpread2_Sheet2.RowCount; i++)
            {
                if (this.fpSpread2_Sheet2.Rows[i].Tag != null)
                {
                    if (this.fpSpread2_Sheet2.Rows[i].Tag is FeeItemList)
                    {
                        FeeItemList fTemp = this.fpSpread2_Sheet2.Rows[i].Tag as FeeItemList;

                        qty += fTemp.Item.Qty;
                    }
                }
                if (this.fpSpread2_Sheet2.Rows[i].Tag is FS.HISFC.Models.Fee.ReturnApply)
                {
                    FS.HISFC.Models.Fee.ReturnApply fTemp = this.fpSpread2_Sheet2.Rows[i].Tag as FS.HISFC.Models.Fee.ReturnApply;

                    qty += fTemp.Item.Qty;
                }
            }
            if (qty > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 是否全退
        /// </summary>
        /// <returns>成功true 失败 false</returns>
        protected virtual bool IsAllQuit()
        {
            decimal qty = 0;

            FeeItemList fTemp = null;

            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                if (this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                {
                    fTemp = this.fpSpread1_Sheet1.Rows[i].Tag as FeeItemList;

                    qty += fTemp.Item.Qty;
                }
            }
            for (int i = 0; i < this.fpSpread1_Sheet2.RowCount; i++)
            {
                if (this.fpSpread1_Sheet2.Rows[i].Tag is FeeItemList)
                {
                    fTemp = this.fpSpread1_Sheet2.Rows[i].Tag as FeeItemList;

                    qty += fTemp.Item.Qty;
                }
            }
            if (qty > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// {71d2d8aa-d310-4ec5-8b5a-429a26d235a9}
        /// 获取家庭账户积分信息  
        /// </summary>
        private int GetCouponVacancy(ref decimal vancancy)
        {
            try
            {
                vancancy = 0.0m;
                string resultCode = "0";
                string errorMsg = "";
                //{6481187A-826A-40d7-8548-026C8C501B3E}
                //Dictionary<string, string> dic = FS.HISFC.BizProcess.Integrate.WSHelper.QueryAccountInfoPersonal(this.patient.PID.CardNO, out resultCode, out errorMsg);
                Dictionary<string, string> dic = FS.HISFC.BizProcess.Integrate.WSHelper.QueryAccountInfo(this.patient.PID.CardNO, out resultCode, out errorMsg);


                if (dic == null)
                {
                    MessageBox.Show("查询账户出错:" + errorMsg);
                    return -1;
                }

                if (dic.ContainsKey("couponvacancy"))
                {
                    vancancy = decimal.Parse(dic["couponvacancy"].ToString());
                }
                else
                {
                    MessageBox.Show("查询账户积分出错！");
                    return -1;
                }

                return 1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }


        /// <summary>
        /// 获取积分倍数
        /// </summary>
        /// <param name="invoiceNO"></param>
        /// <param name="couponnum"></param>
        /// <returns></returns>
        private int GetCouponNum(string invoiceNO, ref decimal couponnum)
        {
            try
            {
                couponnum = 0.0m;
                string resultCode = "0";
                string errorMsg = "";
                Dictionary<string, string> dic = FS.HISFC.BizProcess.Integrate.WSHelper.QueryCouponNum(invoiceNO, out resultCode, out errorMsg);
                if (dic == null)
                {
                    couponnum = 1;
                }
                else
                {
                    if (dic.ContainsKey("couponNum"))
                    {
                        couponnum = decimal.Parse(dic["couponNum"].ToString());
                    }
                    else
                    {
                        couponnum = 1;
                    }
                }
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }

        }





        /// <summary>
        /// 是否账户临时发票
        /// {69245A77-FB7A-42ed-844B-855E7ABC612F}
        /// </summary>
        bool blnIsAccountInvoice = false;

        /// <summary>
        /// 保存退费
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        protected virtual int Save()
        {
            DialogResult result = MessageBox.Show("是否要退费?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.No)
            {
                return -1;
            }
            string returnReason = "";//退费原因
            if (this.oldPatient.Pact.PayKind.ID == "03" && this.IsCanModifyPatientInfo)
            {
                this.patient.Pact = this.oldPatient.Pact.Clone();
                this.patient.SSN = this.oldPatient.SSN;
                this.patient.LSH = this.oldPatient.LSH;
                this.patient.User03 = this.oldPatient.User03;
            }

            //判断有效性
            if (!IsValid())
            {
                return -1;
            }

            decimal couponVacancy = 0.0m;
            decimal couponNum = 0.0m;
            if (this.GetCouponVacancy(ref couponVacancy) < 0)
            {
                MessageBox.Show("获取患者积分余额失败", "提示");
                return -1;
            }


            Balance tmpInvoices = quitInvoices[0] as Balance;
            string incoicenos = tmpInvoices.Invoice.ID;
            if (this.GetCouponNum(incoicenos, ref couponNum) < 0)
            {
                MessageBox.Show("获取患者积分倍数失败");
                return -1;
            }

            long returnValue = 0;//返回值,主要给医保用

            this.medcareInterfaceProxy.SetPactCode(this.patient.Pact.ID);
            // {293FDD11-FC10-4ceb-8E4C-1A4304F22592}
            this.medcareInterfaceProxy.IsLocalProcess = false;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            this.accountPay.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            DateTime nowTime = outpatientManager.GetDateTimeFromSysDateTime();
            int iReturn = 0;

            //获得负发票流水号
            string invoiceSeqNegative = outpatientManager.GetInvoiceCombNO();
            if (invoiceSeqNegative == null || invoiceSeqNegative == string.Empty)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("获得发票流水号失败!" + outpatientManager.Err);

                return -1;
            }
            #region 记录作废发票的金额
            decimal CancelTotCost = 0; //作废发票的总金额
            decimal CancelOwnCost = 0;//作废发票的自费金额
            decimal CancelPayCost = 0;//作废发票的自付金额
            decimal CancelPubCost = 0;//作废发票的公费金额
            decimal CancelRebateCost = 0; // 作废优惠减免金额
            string InvoiceNO = "";
            #endregion

            // {69245A77-FB7A-42ed-844B-855E7ABC612F}
            blnIsAccountInvoice = false;

            //退费标记
            FS.HISFC.Models.Base.CancelTypes cancelType = CancelTypes.Canceled;

            //为了打退票，将发票明细存起来 {BB77678F-A3E1-4f62-9D8D-8D52C1C17F8B}
            ArrayList alInvoiceDetails = new ArrayList();


            foreach (Balance invoice in this.quitInvoices)
            {
                // {69245A77-FB7A-42ed-844B-855E7ABC612F}
                blnIsAccountInvoice = invoice.IsAccount;

                #region 发票主表处理

                InvoiceNO = invoice.Invoice.ID;
                //如果是当前操作员并且没有日结 则为作废
                if (isUseLogout && invoice.IsDayBalanced == false && invoice.BalanceOper.ID.Equals(outpatientManager.Operator.ID))
                {
                    cancelType = FS.HISFC.Models.Base.CancelTypes.LogOut;
                }

                iReturn = outpatientManager.UpdateBalanceCancelType(invoice.Invoice.ID, invoice.CombNO, nowTime, cancelType);
                if (iReturn == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("作废原始发票信息出错!" + outpatientManager.Err);

                    return -1;
                }
                if (iReturn == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("该发票已经作废!");

                    return -1;
                }

                if (isShowWriteReason)// {5A6C578A-D565-42c8-B3FA-B4A52D1FABFB}
                {
                    Forms.frmReturnReason frmReturnReason = new FS.HISFC.Components.OutpatientFee.Forms.frmReturnReason();
                    frmReturnReason.ShowDialog();
                    returnReason = frmReturnReason.Reason;
                }
                //插入负纪录冲账
                Balance invoClone = invoice.Clone();

                CancelTotCost += invoClone.FT.TotCost;
                CancelOwnCost += invoClone.FT.OwnCost;
                CancelPayCost += invoClone.FT.PayCost;
                CancelPubCost += invoClone.FT.PubCost;

                invoClone.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                invoClone.FT.TotCost = -invoClone.FT.TotCost;
                invoClone.FT.OwnCost = -invoClone.FT.OwnCost;
                invoClone.FT.PayCost = -invoClone.FT.PayCost;
                invoClone.FT.PubCost = -invoClone.FT.PubCost;
                //{8CA2308F-2E58-4800-8AB8-8661FAFE6994}
                invoClone.FT.RebateCost = -invoClone.FT.RebateCost;
                invoClone.FT.DonateCost = -invoClone.FT.DonateCost;
                invoClone.CancelType = cancelType;

                invoClone.CanceledInvoiceNO = invoice.ID;
                invoClone.CancelOper.ID = outpatientManager.Operator.ID;
                invoClone.BalanceOper.ID = outpatientManager.Operator.ID;//日结需要 改为当前退费人
                invoClone.BalanceOper.OperTime = nowTime;
                invoClone.CancelOper.OperTime = nowTime;
                invoClone.IsAuditing = false;
                invoClone.AuditingOper.ID = string.Empty;
                invoClone.AuditingOper.OperTime = DateTime.MinValue;
                invoClone.IsDayBalanced = false;
                invoClone.BalanceID = string.Empty;
                invoClone.DayBalanceOper.OperTime = DateTime.MinValue;
                invoClone.Memo = returnReason;//退费原因

                invoClone.CombNO = invoiceSeqNegative;

                iReturn = outpatientManager.InsertBalance(invoClone);
                if (iReturn <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("插入发票冲账信息出错!!" + outpatientManager.Err);

                    return -1;
                }
                #endregion

                #region 发票明细信息处理
                //处理发票明细表信息
                ArrayList alInvoiceDetail = outpatientManager.QueryBalanceListsByInvoiceNOAndInvoiceSequence(invoice.Invoice.ID, invoice.CombNO);
                if (alInvoiceDetail == null)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("获得发票明细出错!" + outpatientManager.Err);

                    return -1;
                }


                //作废发票明细表信息
                iReturn = outpatientManager.UpdateBalanceListCancelType(invoice.Invoice.ID, invoice.CombNO, nowTime, cancelType);
                if (iReturn <= 0)
                {
                    //如果作废明细表失败了，则查询是否本次是0元退费，{76662F26-4D1B-4aa1-8FD1-9AE177177505}
                    //如果是0元退费的话，就不存在发票明细，也不存在支付方式，积分也不会使用支付方式积分
                    if (invoClone.FT.TotCost == 0)
                    {
                        iReturn = 1;
                    }
                    else
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("作废发票明细出错!" + outpatientManager.Err);
                        return -1;
                    }
                }

                foreach (BalanceList d in alInvoiceDetail)
                {
                    d.BalanceBase.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                    d.BalanceBase.FT.OwnCost = -d.BalanceBase.FT.OwnCost;
                    d.BalanceBase.FT.PubCost = -d.BalanceBase.FT.PubCost;
                    d.BalanceBase.FT.PayCost = -d.BalanceBase.FT.PayCost;
                    d.BalanceBase.BalanceOper.OperTime = nowTime;
                    d.BalanceBase.BalanceOper.ID = outpatientManager.Operator.ID;
                    d.BalanceBase.CancelType = cancelType;
                    d.BalanceBase.IsDayBalanced = false;
                    d.BalanceBase.DayBalanceOper.ID = string.Empty;
                    d.BalanceBase.DayBalanceOper.OperTime = DateTime.MinValue;
                    //d.CombNO = invoiceSeqNegative;
                    ((Balance)d.BalanceBase).CombNO = invoiceSeqNegative;

                    iReturn = outpatientManager.InsertBalanceList(d);
                    if (iReturn <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("插入发票明细冲账信息出错!" + outpatientManager.Err);

                        return -1;
                    }
                }
                #endregion

                //为了打退票，将发票明细存起来 {D5FA97FA-8DBB-48e7-BF5B-8DF4049EEE2B}
                alInvoiceDetails.Add(alInvoiceDetail);
            }

            Balance invoiceInfo = ((Balance)quitInvoices[0]);

            #region 处理支付信息

            ArrayList payList = new ArrayList();
            string choosePayMode = this.feeIntegrate.GetControlValue(FS.HISFC.BizProcess.Integrate.Const.QUIT_PAY_MODE_SELECT, "1");
            ArrayList feePayMods = this.outpatientManager.QueryBalancePaysByInvoiceSequence(invoiceInfo.CombNO);

            string payInvoiceNo = string.Empty;

            //会员支付+会员代付
            Dictionary<string, List<BalancePay>> dictAcc = new Dictionary<string, List<BalancePay>>();

            //套餐支付结算
            bool isExistPackage = false;

            //{9F2C17EC-0AE6-4df8-B959-F4D99314E227}
            FS.HISFC.BizLogic.Manager.Constant constManager = new FS.HISFC.BizLogic.Manager.Constant();
            //本地积分已废除
            //FS.FrameWork.Models.NeuObject obj = constManager.GetConstant("JFZFFS", "1");

            FS.FrameWork.Models.NeuObject cashCouponPayMode = constManager.GetConstant("XJLZFFS", "1");

            quitOperateCouponAmount = 0.0m;
            quitCostCouponAmount = 0.0m;

            FS.HISFC.BizProcess.Integrate.Account.AccountPay accountPay = new FS.HISFC.BizProcess.Integrate.Account.AccountPay();

            //判断该支付方式是否计算积分
            foreach (BalancePay pay in feePayMods)
            {
                //{416C9A04-3A94-4d41-8F0A-14B73622C65E}
                //本地积分已废除
                //if (obj.Name.Contains(pay.PayType.ID.ToString()))
                //{
                //    if (accountPay.UpdateCoupon(this.patient.PID.CardNO, -pay.FT.TotCost, pay.Invoice.ID.ToString()) <= 0)
                //    {
                //        FS.FrameWork.Management.PublicTrans.RollBack();
                //        MessageBox.Show("扣除积分出错!");

                //        return -1;
                //    }
                //}

                if (cashCouponPayMode.Name.Contains(pay.PayType.ID.ToString()))
                {
                    quitOperateCouponAmount -= pay.FT.TotCost;
                }

                if (pay.PayType.ID == "CO")
                {
                    quitCostCouponAmount -= pay.FT.TotCost;
                }
            }

            //{F166B18B-62E3-4835-A729-4CA384F9ADEE}
            //if (quitOperateCouponAmount != 0)
            //{
            //    string errInfo = string.Empty;
            //    HISFC.BizProcess.Integrate.Account.CashCoupon cashCouponPrc = new FS.HISFC.BizProcess.Integrate.Account.CashCoupon();
            //    if (cashCouponPrc.CashCouponSave("MZTF", this.patient.PID.CardNO, InvoiceNO, quitOperateCouponAmount, ref errInfo) <= 0)
            //    {

            //        FS.FrameWork.Management.PublicTrans.RollBack();
            //        MessageBox.Show("计算现金流积分出错!" + errInfo);
            //        return -1;
            //    }

            //}

            //{FAE56BB8-F958-411f-9663-CC359D6D494B}
            //需要用钱来进行抵扣的积分
            //正负判断有点绕，一定要搞清楚
            decimal shouldPayCoupon = 0.0m;

            //将消费的积分金额转化为积分数量
            decimal quitCostCouponAmountToCoupon = quitCostCouponAmount * 100;

            quitOperateCouponAmount = quitOperateCouponAmount * couponNum;   //退费时积分要加上倍数   {c2d43b9d-eda4-4f87-92c1-f6fdb08d9d04}取多倍积分

            if (IsCouponModuleInUse)
            {
                if (couponVacancy + (-quitCostCouponAmountToCoupon) - (-quitOperateCouponAmount) < 0)
                {
                    //{C0A56225-DFF9-46f9-9776-4DEE25F6CBD0}
                    if (MessageBox.Show("家庭账户积分不够，是否扣款进行抵扣？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        shouldPayCoupon = Math.Floor((-quitOperateCouponAmount) - couponVacancy - (-quitCostCouponAmountToCoupon)) / 100;
                        quitOperateCouponAmount = -couponVacancy - (-quitCostCouponAmountToCoupon);
                    }
                }
            }

            if (feePayMods.Count >= 0)
            {
                if (this.IsQuitSamePayMod == false)
                {
                    #region 退还支付方式可选

                    int paySquence = 100;
                    BalancePay objPay = new BalancePay();
                    ArrayList alFeePayMode = new ArrayList();


                    //是否锁定退款支付方式-是否已经日结或者隔天
                    bool IsLockPayMode = false;

                    foreach (BalancePay pay in feePayMods)
                    {
                        paySquence--;
                        objPay = pay.Clone();
                        objPay.TransType = TransTypes.Negative;
                        objPay.FT.TotCost = -objPay.FT.TotCost;
                        objPay.FT.RealCost = -objPay.FT.RealCost;
                        objPay.FT.OwnCost = -objPay.FT.OwnCost;
                        objPay.InputOper.OperTime = nowTime;
                        objPay.Squence = paySquence.ToString();
                        objPay.InputOper.ID = outpatientManager.Operator.ID;
                        objPay.InvoiceCombNO = invoiceSeqNegative;
                        objPay.CancelType = cancelType;
                        objPay.IsChecked = false;
                        objPay.CheckOper.ID = string.Empty;
                        objPay.CheckOper.OperTime = DateTime.MinValue;
                        objPay.BalanceOper.ID = string.Empty;
                        objPay.IsDayBalanced = false;
                        objPay.IsAuditing = false;
                        objPay.AuditingOper.OperTime = DateTime.MinValue;
                        objPay.AuditingOper.ID = string.Empty;

                        //如果可退积分不为零，优先在此处进行扣除需要抵扣积分的金额
                        if (shouldPayCoupon > 0)
                        {
                            if (cashCouponPayMode.Name.Contains(objPay.PayType.ID.ToString()))
                            {
                                if (shouldPayCoupon >= (-objPay.FT.TotCost))
                                {
                                    shouldPayCoupon -= (-objPay.FT.TotCost);
                                    objPay.PayType.ID = "TCO";
                                }
                                else
                                {
                                    paySquence--;
                                    BalancePay couponPay = new BalancePay();
                                    couponPay = objPay.Clone();
                                    couponPay.Squence = paySquence.ToString();
                                    couponPay.FT.TotCost = -shouldPayCoupon;
                                    couponPay.FT.RealCost = -shouldPayCoupon;
                                    couponPay.PayType.ID = "TCO";

                                    objPay.FT.TotCost += shouldPayCoupon;
                                    objPay.FT.RealCost += shouldPayCoupon;

                                    shouldPayCoupon = 0;
                                    alFeePayMode.Add(couponPay);
                                }
                            }
                        }

                        //隔天退费只能选择现金支付方式
                        if (pay.IsDayBalanced || nowTime.Date != pay.InputOper.OperTime.Date)
                        {
                            IsLockPayMode = true;
                        }

                        alFeePayMode.Add(objPay);
                    }

                    if (shouldPayCoupon > 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("患者没有足够的退款金额支付所消耗的积分!缺少积分数量：" + (shouldPayCoupon * 100).ToString());
                        return -1;
                    }

                    Froms.frmChooseBalancePay frmTemp = new FS.HISFC.Components.OutpatientFee.Froms.frmChooseBalancePay();
                    frmTemp.IsLockPayMode = IsLockPayMode;
                    frmTemp.Init();
                    frmTemp.QuitPayModes = alFeePayMode;
                    frmTemp.InitQuitPayModes();
                    frmTemp.StartPosition = FormStartPosition.CenterScreen;
                    frmTemp.ShowDialog();

                    if (frmTemp.IsSelect == false)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("没有选择退费的支付方式，请重新退费!");

                        return -1;
                    }

                    alFeePayMode = frmTemp.ModifiedPayModes;
                    foreach (BalancePay pay in alFeePayMode)
                    {
                        iReturn = outpatientManager.InsertBalancePay(pay);
                        if (iReturn <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("插入支付负信息出错!" + outpatientManager.Err);

                            return -1;
                        }

                        #region 账户新增(账户冲掉扣费金额)

                        payInvoiceNo = pay.Invoice.ID;

                        //会员支付+会员代付处理
                        if ((!string.IsNullOrEmpty(pay.AccountNo) && !string.IsNullOrEmpty(pay.AccountTypeCode)) ||
                            pay.PayType.ID == "YS" || pay.PayType.ID == "DC")
                        {
                            string key = pay.AccountNo + "-" + pay.AccountTypeCode;
                            if (dictAcc.ContainsKey(key))
                            {
                                List<BalancePay> bpList = dictAcc[key];
                                bpList.Add(pay);
                            }
                            else
                            {
                                List<BalancePay> bpList = new List<BalancePay>();
                                bpList.Add(pay);
                                dictAcc.Add(key, bpList);
                            }
                        }

                        #endregion

                        #region 套餐支付处理

                        if (pay.PayType.ID == "PR" ||
                            pay.PayType.ID == "PD" ||
                            pay.PayType.ID == "PY")
                        {
                            isExistPackage = true;
                        }

                        #endregion


                    }

                    payList = alFeePayMode;

                    #region 作废
                    //CancelRebateCost = 0;
                    //int paySquence = 100;
                    //BalancePay objPay = new BalancePay();

                    //#region 对于减免、记帐患者，处理减免、记帐数据
                    ////添加参数控制是否保存医保卡的自付部分
                    //foreach (BalancePay payRebat in feePayMods)
                    //{
                    //    if (payRebat.PayType.ID == "RC" || payRebat.PayType.ID == "JZ" || payRebat.PayType.ID == "PB" || (isSavePYfee && payRebat.PayType.ID == "PY"))
                    //    {

                    //        paySquence--;

                    //        // 减免、记帐数据处理
                    //        if (payRebat.PayType.ID == "RC" || payRebat.PayType.ID == "JZ")
                    //        {
                    //            CancelRebateCost += payRebat.FT.TotCost;
                    //        }

                    //        objPay = payRebat.Clone();
                    //        objPay.TransType = TransTypes.Negative;
                    //        objPay.FT.TotCost = -objPay.FT.TotCost;
                    //        objPay.FT.RealCost = -objPay.FT.RealCost;
                    //        objPay.FT.OwnCost = -objPay.FT.OwnCost;
                    //        objPay.InputOper.OperTime = nowTime;
                    //        objPay.Invoice.ID = InvoiceNO;
                    //        objPay.Squence = paySquence.ToString();

                    //        objPay.InputOper.ID = outpatientManager.Operator.ID;
                    //        objPay.InvoiceCombNO = invoiceSeqNegative;
                    //        objPay.CancelType = cancelType;
                    //        objPay.IsChecked = false;
                    //        objPay.CheckOper.ID = string.Empty;
                    //        objPay.CheckOper.OperTime = DateTime.MinValue;
                    //        objPay.BalanceOper.ID = string.Empty;
                    //        objPay.IsDayBalanced = false;
                    //        objPay.IsAuditing = false;
                    //        objPay.AuditingOper.OperTime = DateTime.MinValue;
                    //        objPay.AuditingOper.ID = string.Empty;
                    //        iReturn = outpatientManager.InsertBalancePay(objPay);
                    //        if (iReturn <= 0)
                    //        {
                    //            FS.FrameWork.Management.PublicTrans.RollBack();
                    //            MessageBox.Show("插入支付负信息出错!" + outpatientManager.Err);

                    //            return -1;
                    //        }
                    //    }
                    //}

                    //#endregion

                    //if (CancelRebateCost - CancelOwnCost - CancelPayCost < 0)
                    //{
                    //    paySquence--;

                    //    // {B176923A-5C7E-46a9-A4C6-ED6313ACC4E5}
                    //    // 是否允许按原支付方式退费 false:不允许 true:允许
                    //    #region 原来的
                    //    //ArrayList payList = new ArrayList();
                    //    objPay = new BalancePay();
                    //    objPay.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                    //    objPay.FT.TotCost = -(CancelPayCost + CancelOwnCost - CancelRebateCost);
                    //    objPay.FT.RealCost = FS.HISFC.Components.OutpatientFee.Class.Function.DealCent(-(CancelPayCost + CancelOwnCost - CancelRebateCost));
                    //    objPay.FT.OwnCost = -(CancelOwnCost - CancelRebateCost);
                    //    objPay.InputOper.OperTime = nowTime;
                    //    objPay.Invoice.ID = InvoiceNO;
                    //    objPay.Squence = paySquence.ToString();
                    //    if (invoiceInfo.IsAccount)
                    //    {
                    //        objPay.PayType.ID = "YS";
                    //    }
                    //    else
                    //    {
                    //        objPay.PayType.ID = "CA";
                    //    }

                    //    if (isATM && Function.CheckAtmFee(InvoiceNO))
                    //    {
                    //        objPay.PayType.ID = "YS";
                    //    }
                    //    objPay.InputOper.ID = outpatientManager.Operator.ID;
                    //    objPay.InvoiceCombNO = invoiceSeqNegative;
                    //    objPay.CancelType = cancelType;
                    //    objPay.IsChecked = false;
                    //    objPay.CheckOper.ID = string.Empty;
                    //    objPay.CheckOper.OperTime = DateTime.MinValue;
                    //    objPay.BalanceOper.ID = string.Empty;
                    //    //p.BalanceNo = 0;
                    //    objPay.IsDayBalanced = false;
                    //    objPay.IsAuditing = false;
                    //    objPay.AuditingOper.OperTime = DateTime.MinValue;
                    //    objPay.AuditingOper.ID = string.Empty;
                    //    if (patient.Pact.PayKind.ID == "02")
                    //    {
                    //        objPay.FT.OwnCost = -CancelOwnCost;
                    //    }

                    //    // string choosePayMode = this.feeIntegrate.GetControlValue(FS.HISFC.BizProcess.Integrate.Const.QUIT_PAY_MODE_SELECT, "1");
                    //    if (!invoiceInfo.IsAccount && choosePayMode == "0") //选择支付方式
                    //    {
                    //        ArrayList payLists = new ArrayList();

                    //        payLists.Add(objPay);

                    //        Froms.frmChooseBalancePay frmTemp = new FS.HISFC.Components.OutpatientFee.Froms.frmChooseBalancePay();
                    //        frmTemp.Init();
                    //        frmTemp.QuitPayModes = payLists;
                    //        frmTemp.InitQuitPayModes();
                    //        frmTemp.StartPosition = FormStartPosition.CenterScreen;
                    //        frmTemp.ShowDialog();

                    //        if (frmTemp.IsSelect == false)
                    //        {
                    //            FS.FrameWork.Management.PublicTrans.RollBack();
                    //            MessageBox.Show("没有选择退费的支付方式，请重新退费!");

                    //            return -1;
                    //        }

                    //        payLists = new ArrayList();
                    //        payLists = frmTemp.ModifiedPayModes;

                    //        objPay = payLists[0] as FS.HISFC.Models.Fee.Outpatient.BalancePay;
                    //    }

                    //    //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
                    //    #region 账户新增(账户冲掉扣费金额)
                    //    if (objPay.PayType.ID == "YS")
                    //    {
                    //        if (feeIntegrate.AccountCancelPay(patient, objPay.FT.OwnCost, InvoiceNO, (outpatientManager.Operator as Employee).Dept.ID, "C") < 0)
                    //        {
                    //            FS.FrameWork.Management.PublicTrans.RollBack();
                    //            MessageBox.Show("账户退费入户失败！" + feeIntegrate.Err);
                    //            return -1;
                    //        }
                    //        CancleInvoiceNo = InvoiceNO;
                    //    }
                    //    #endregion

                    //    iReturn = outpatientManager.InsertBalancePay(objPay);
                    //    if (iReturn <= 0)
                    //    {
                    //        FS.FrameWork.Management.PublicTrans.RollBack();
                    //        MessageBox.Show("插入支付负信息出错!" + outpatientManager.Err);

                    //        return -1;
                    //    }
                    //    payList.Add(objPay);
                    //    #endregion
                    //}
                    #endregion

                    #endregion
                }
                else
                {
                    #region 新加的

                    int returnJValue = this.outpatientManager.UpdateBalancePayModeCancelType(invoiceInfo.Invoice.ID, invoiceInfo.CombNO, nowTime, cancelType);
                    if (returnJValue <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("作废发票支付方式出错!" + outpatientManager.Err);
                        return -1;
                    }

                    int bpIdx = 0;
                    foreach (FS.HISFC.Models.Fee.Outpatient.BalancePay bp in feePayMods)
                    {
                        if (bp != null)
                        {
                            BalancePay objPay = bp.Clone();
                            if (bp.PayType.ID == "CD" || bp.PayType.ID == "DB")
                            {
                                decimal bankTransTot = 0m;
                                bankTransTot = -objPay.FT.TotCost;
                                bool isBankTransOK = false;

                                try
                                {
                                    bankTrans.InputListInfo.Clear();
                                    bankTrans.OutputListInfo.Clear();
                                    /// 0:交易类型，1：交易金额
                                    bankTrans.InputListInfo.Add("1");
                                    bankTrans.InputListInfo.Add(bankTransTot);
                                    isBankTransOK = bankTrans.Do();
                                }
                                catch (Exception ex)
                                {
                                    isBankTransOK = false;
                                }
                                if (isBankTransOK == false)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show(bankTrans.OutputListInfo[0].ToString());
                                    return -1;
                                }
                                if (bankTrans.OutputListInfo.Count >= 4)
                                {
                                    if (bankTransTot != NConvert.ToDecimal(bankTrans.OutputListInfo[3]))
                                    {
                                        FS.FrameWork.Management.PublicTrans.RollBack();
                                        MessageBox.Show("交易请求金额" + bankTransTot.ToString() + "不等于交易金额" + NConvert.ToDecimal(bankTrans.OutputListInfo[3]) + ",交易失败！");
                                        return -1;
                                    }
                                    else
                                    {
                                        //MessageBox.Show("交易成功！金额" + bankTransTot.ToString());
                                        objPay.Bank.Name = bankTrans.OutputListInfo[0].ToString();
                                        objPay.Bank.Account = bankTrans.OutputListInfo[1].ToString();
                                        objPay.Squence = bankTrans.OutputListInfo[2].ToString();
                                    }
                                }

                                #region 废弃

                                //FS.HISFC.Components.OutpatientFee.Forms.frmBankTrans fbt = new FS.HISFC.Components.OutpatientFee.Forms.frmBankTrans();
                                //fbt.TotCost = bankTransTot;
                                //  FS.FrameWork.Models.NeuObject no = new FS.FrameWork.Models.NeuObject();
                                //  no.ID = objPay.Bank.Name;
                                //  no.Memo = objPay.Bank.Account;
                                //  no.User01 = objPay.Squence;
                                //  no.Name = objPay.FT.TotCost.ToString();

                                //  fbt.ListTransInfo.Add(no);
                                //fbt.ShowDialog();
                                //if (fbt.ListTransInfo.Count <= 0)
                                //{
                                //    FS.FrameWork.Management.PublicTrans.RollBack();
                                //    MessageBox.Show("银联退费失败!" + outpatientManager.Err);
                                //    return -1;
                                //}
                                //else
                                //{
                                //    if (fbt.ListTransInfo.Count == 1)
                                //    {
                                //        //this.fpPayMode_Sheet1.CellChanged -= new SheetViewEventHandler(fpPayMode_Sheet1_CellChanged);
                                //        //this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.ActiveRowIndex, (int)PayModeCols.Bank].Text =
                                //        //    fbt.ListTransInfo[0].ID;
                                //        //this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.ActiveRowIndex, (int)PayModeCols.Account].Text = 
                                //        //    fbt.ListTransInfo[0].Memo;
                                //        //this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.ActiveRowIndex, (int)PayModeCols.PosNo].Text = 
                                //        //    fbt.ListTransInfo[0].User01;
                                //        //this.fpPayMode_Sheet1.ActiveRow.Locked = true;
                                //        //this.fpPayMode_Sheet1.CellChanged += new SheetViewEventHandler(fpPayMode_Sheet1_CellChanged);
                                //        //this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.ActiveRowIndex, (int)PayModeCols.Cost].Text = 
                                //        //    fbt.ListTransInfo[0].Name;
                                //        objPay.Bank.Name = no.ID;
                                //        objPay.Bank.Account = no.Memo;
                                //        objPay.Squence = no.User01;
                                //        // objPay.FT.TotCost = no.Name;
                                //        }
                                //}

                                #endregion
                            }

                            #region

                            objPay.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                            objPay.FT.TotCost = -objPay.FT.TotCost;
                            objPay.FT.RealCost = -objPay.FT.RealCost;
                            objPay.FT.OwnCost = -objPay.FT.OwnCost;
                            objPay.InputOper.OperTime = nowTime;
                            objPay.Invoice.ID = InvoiceNO;
                            objPay.Squence = (99 - bpIdx).ToString();
                            objPay.InputOper.ID = outpatientManager.Operator.ID;
                            objPay.InvoiceCombNO = invoiceSeqNegative;
                            objPay.CancelType = cancelType;
                            objPay.IsChecked = false;
                            objPay.CheckOper.ID = string.Empty;
                            objPay.CheckOper.OperTime = DateTime.MinValue;
                            objPay.BalanceOper.ID = string.Empty;
                            //p.BalanceNo = 0;
                            objPay.IsDayBalanced = false;
                            objPay.IsAuditing = false;
                            objPay.AuditingOper.OperTime = DateTime.MinValue;
                            objPay.AuditingOper.ID = string.Empty;

                            #endregion

                            iReturn = outpatientManager.InsertBalancePay(objPay);
                            if (iReturn <= 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("插入支付负信息出错!" + outpatientManager.Err);
                                return -1;
                            }

                            #region 账户新增(账户冲掉扣费金额)

                            payInvoiceNo = objPay.Invoice.ID;

                            //会员支付+会员代付处理
                            if ((!string.IsNullOrEmpty(objPay.AccountNo) && !string.IsNullOrEmpty(objPay.AccountTypeCode)) ||
                                objPay.PayType.ID == "YS" || objPay.PayType.ID == "DC")
                            {
                                string key = objPay.AccountNo + "-" + objPay.AccountTypeCode;
                                if (dictAcc.ContainsKey(key))
                                {
                                    List<BalancePay> bpList = dictAcc[key];
                                    bpList.Add(objPay);
                                }
                                else
                                {
                                    List<BalancePay> bpList = new List<BalancePay>();
                                    bpList.Add(objPay);
                                    dictAcc.Add(key, bpList);
                                }
                            }

                            #endregion

                            #region 套餐支付处理

                            if (objPay.PayType.ID == "PR" ||
                                objPay.PayType.ID == "PD" ||
                                objPay.PayType.ID == "PY")
                            {
                                isExistPackage = true;
                            }

                            #endregion


                            bpIdx++;

                            #region 对于减免、记帐患者，处理减免、记帐数据

                            if (objPay.PayType.ID != "RC" || objPay.PayType.ID != "JZ")
                            {
                                payList.Add(objPay);
                            }

                            #endregion
                        }
                    }
                    #endregion
                }
            }

            #region 账户退费

            if (dictAcc != null && dictAcc.Count > 0)
            {
                //结算患者
                FS.HISFC.Models.RADT.PatientInfo selfPatient = accountManager.GetPatientInfoByCardNO(this.patient.PID.CardNO);
                if (selfPatient == null || string.IsNullOrEmpty(selfPatient.PID.CardNO))
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("查询患者基本信息失败!", "错误");
                    return -1;
                }

                //会员患者
                FS.HISFC.Models.RADT.PatientInfo empPatient = new FS.HISFC.Models.RADT.PatientInfo();

                foreach (List<BalancePay> bpList in dictAcc.Values)
                {
                    decimal baseCost = 0;                    //基本账户金额
                    decimal donateCost = 0;                  //赠送账户金额

                    BalancePay bp = new BalancePay();
                    for (int k = 0; k < bpList.Count; k++)
                    {
                        bp = bpList[k];
                        if (bp.PayType.ID == "YS")
                        {
                            baseCost += Math.Abs(bp.FT.TotCost);
                        }
                        else if (bp.PayType.ID.ToString() == "DC")
                        {
                            donateCost += Math.Abs(bp.FT.TotCost);
                        }
                    }

                    string accountNo = bp.AccountNo;      //账户
                    string accountTypeCode = bp.AccountTypeCode;   //账户类型
                    List<AccountDetail> accLists = accountManager.GetAccountDetail(accountNo, accountTypeCode, "1");
                    if (accLists == null || accLists.Count <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("查找账户失败!", "错误");
                        return -1;
                    }
                    AccountDetail detailAcc = accLists[0];

                    if (bp.IsEmpPay)
                    {
                        empPatient = accountManager.GetPatientInfoByCardNO(detailAcc.CardNO);
                        if (empPatient == null || string.IsNullOrEmpty(empPatient.PID.CardNO))
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(string.Format("查找授权患者【{0}】基本信息失败!", detailAcc.CardNO), "错误");
                            return -1;
                        }
                    }
                    else
                    {
                        empPatient = selfPatient;
                    }

                    iReturn = accountPay.OutpatientPay(selfPatient, accountNo, accountTypeCode, baseCost, donateCost, payInvoiceNo, empPatient, FS.HISFC.Models.Account.PayWayTypes.C, 0);
                    if (iReturn < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("账户退还出错!" + accountPay.Err, "错误");
                        return -1;
                    }

                }
            }

            #endregion

            #region 套餐退费

            if (isExistPackage)
            {
                string errInfo = string.Empty;
                iReturn = this.feeIntegrate.CancelCostPackageDetail(payInvoiceNo, ref errInfo);
                if (iReturn < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("套餐退费出错!" + feeIntegrate.Err, "错误");
                    return -1;
                }
            }

            #endregion

            #endregion

            bool isCashPay = false;//是否现金冲账

            #region 记录退费信息
            alQuitFeeItemList.Clear();
            FS.HISFC.Models.Fee.ReturnApply returnApply = null;
            foreach (FarPoint.Win.Spread.SheetView sv in fpSpread2.Sheets)
            {
                for (int i = 0; i < sv.Rows.Count; i++)
                {
                    if (sv.Rows[i].Tag is FS.HISFC.Models.Fee.ReturnApply)
                    {
                        returnApply = sv.Rows[i].Tag as FS.HISFC.Models.Fee.ReturnApply;
                        FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList = this.outpatientManager.GetFeeItemListBalanced(returnApply.RecipeNO, returnApply.SequenceNO);
                        if (feeItemList == null)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("获取退费申请对应费用明细失败！" + returnApplyManager.Err);
                            return -1;
                        }
                        alQuitFeeItemList.Add(feeItemList);
                    }
                    else if (sv.Rows[i].Tag is FS.HISFC.Models.Fee.Outpatient.FeeItemList)
                    {
                        alQuitFeeItemList.Add((FS.HISFC.Models.Fee.Outpatient.FeeItemList)sv.Rows[i].Tag);
                    }
                }
            }

            if (this.isQuitFeeAndOperOrder)
            {
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemTemp in alQuitFeeItemList)
                {
                    FS.HISFC.Models.Order.OutPatient.Order orderTemp = orderIntegrate.GetOneOrder(feeItemTemp.Patient.ID, feeItemTemp.Order.ID.ToString());
                    if (orderTemp != null && orderTemp.Status == 1)
                    {
                        this.orderIntegrate.UpdateOrderBeCaceled(orderTemp);
                    }
                }
            }

            #endregion

            #region 处理退费明细
            //处理费用明细
            ArrayList alFeeDetail = outpatientManager.QueryFeeItemListsByInvoiceSequence(invoiceInfo.CombNO);
            if (alFeeDetail == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("获得患者费用明细出错!" + outpatientManager.Err);

                return -1;
            }
            iReturn = outpatientManager.UpdateFeeItemListCancelType(invoiceInfo.CombNO, nowTime, cancelType);
            if (iReturn <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("作废患者明细出错!" + outpatientManager.Err);

                return -1;
            }

            oldFeeItemLists = new ArrayList();
            foreach (FeeItemList f in alFeeDetail)
            {
                oldFeeItemLists.Add(f.Clone());
                f.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                f.FT.OwnCost = -f.FT.OwnCost;
                f.FT.PayCost = -f.FT.PayCost;
                f.FT.PubCost = -f.FT.PubCost;
                //{8CA2308F-2E58-4800-8AB8-8661FAFE6994}
                f.FT.RebateCost = -f.FT.RebateCost;
                f.FT.DonateCost = -f.FT.DonateCost;
                //{BD46CBA6-A670-40cf-A1FD-EAFBC9797D04}
                f.FT.PackageEco = -f.FT.PackageEco;
                f.FT.TotCost = f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost;
                f.Item.Qty = -f.Item.Qty;
                f.CancelType = cancelType;
                f.FeeOper.ID = outpatientManager.Operator.ID;
                f.FeeOper.OperTime = nowTime;
                f.ChargeOper.OperTime = nowTime;
                f.InvoiceCombNO = invoiceSeqNegative;
                f.ConfirmedInjectCount = 0;

                iReturn = outpatientManager.InsertFeeItemList(f);
                if (iReturn <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("插入费用明细冲帐信息出错!" + outpatientManager.Err);

                    return -1;
                }
            }

            //this.t.BeginTransaction();


            //if (this.patient.Pact.PayKind.ID == "02" && DialogResult.Yes == MessageBox.Show("是否选择医保登记患者？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            if (this.patient.Pact.PayKind.ID == "02")
            {
                this.medcareInterfaceProxy.BeginTranscation();
                // {293FDD11-FC10-4ceb-8E4C-1A4304F22592}
                this.medcareInterfaceProxy.IsLocalProcess = false;

                //long returnValue = medcareInterfaceProxy.SetPactCode(this.patient.Pact.ID);


                returnValue = medcareInterfaceProxy.Connect();
                if (returnValue != 1)
                {
                    medcareInterfaceProxy.Rollback();
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("待遇接口初始化失败") + medcareInterfaceProxy.ErrMsg);
                    return -1;
                }
                this.patient.SIMainInfo.InvoiceNo = ((Balance)quitInvoices[0]).Invoice.ID;
                returnValue = medcareInterfaceProxy.GetRegInfoOutpatient(this.patient);
                if (returnValue != 1)
                {
                    medcareInterfaceProxy.Rollback();
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("待遇接口或得患者信息失败") + medcareInterfaceProxy.ErrMsg);
                    return -1;
                }
                returnValue = medcareInterfaceProxy.DeleteUploadedFeeDetailsOutpatient(this.patient, ref alFeeDetail);
                if (returnValue != 1)
                {
                    medcareInterfaceProxy.Rollback();
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("待遇接口上传退费明细失败") + medcareInterfaceProxy.ErrMsg);

                    return -1;
                }
                returnValue = medcareInterfaceProxy.CancelBalanceOutpatient(this.patient, ref alFeeDetail);
                if (returnValue != 1)
                {
                    medcareInterfaceProxy.Rollback();
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("待遇接口结算失败") + medcareInterfaceProxy.ErrMsg);

                    return -1;
                }
            }
            #endregion

            #region 未核准退药信息
            //针对未核准退药信息
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                if (this.fpSpread1_Sheet1.Rows[i].Tag != null)
                {
                    if (this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                    {
                        FeeItemList fQuit = this.fpSpread1_Sheet1.Rows[i].Tag as FeeItemList;
                        //有未确认的退药，作废退药申请!{A56F1E9D-9E9D-48bb-A3EA-8F17A6738619}
                        if (fQuit.IsConfirmed == false)
                        {
                            iReturn = pharmacyIntegrate.CancelApplyOutClinicMZTF(fQuit.RecipeNO, fQuit.SequenceNO);
                            if (iReturn < 0)//{5CEF35C8-9C9E-4208-934C-5DC02F3413B4}
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                medcareInterfaceProxy.Rollback();
                                MessageBox.Show("作废发药申请出错!药品可能已经发药，请刷新窗口重试");

                                return -1;
                            }
                        }
                    }
                }
            }

            #endregion

            #region 作废终端申请

            for (int i = 0; i < this.fpSpread1_Sheet2.RowCount; i++)
            {
                if (this.fpSpread1_Sheet2.Rows[i].Tag != null && this.fpSpread1_Sheet2.Rows[i].Tag is FeeItemList)
                {
                    FeeItemList fQuit = this.fpSpread1_Sheet2.Rows[i].Tag as FeeItemList;

                    //有未确认的退药，作废退药申请!
                    if (fQuit.IsConfirmed == false)
                    {
                        iReturn = confirmIntegrate.CancelConfirmTerminal(fQuit.Order.ID, fQuit.Item.ID);
                        if (iReturn < 0)
                        {
                            medcareInterfaceProxy.Rollback();
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("作废终端申请出错!" + confirmIntegrate.Err);

                            return -1;
                        }
                    }
                }
            }

            #endregion

            //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
            #region 更新退费申请退费标记
            //alQuitFeeItemList.Clear();
            //FS.HISFC.Models.Fee.ReturnApply returnApply = null;
            //DateTime operDate = outpatientManager.GetDateTimeFromSysDateTime();
            string operCode = outpatientManager.Operator.ID;
            foreach (FarPoint.Win.Spread.SheetView sv in fpSpread2.Sheets)
            {
                for (int i = 0; i < sv.Rows.Count; i++)
                {
                    if (sv.Rows[i].Tag is FS.HISFC.Models.Fee.ReturnApply)
                    {
                        returnApply = sv.Rows[i].Tag as FS.HISFC.Models.Fee.ReturnApply;
                        returnApply.CancelType = CancelTypes.Valid;
                        returnApply.CancelOper.ID = operCode;
                        returnApply.CancelOper.OperTime = nowTime;
                        if (returnApplyManager.UpdateApplyCharge(returnApply) <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("更新申请表退费标记失败！" + returnApplyManager.Err);
                            return -1;
                        }
                        //FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList = this.outpatientManager.GetFeeItemListAndQuitForFee(returnApply.RecipeNO, returnApply.SequenceNO);
                        //if (feeItemList == null)
                        //{
                        //    FS.FrameWork.Management.PublicTrans.RollBack();
                        //    MessageBox.Show("获取退费申请对应费用明细失败！" + returnApplyManager.Err);
                        //    return -1;
                        //}
                        //alQuitFeeItemList.Add(feeItemList);
                    }
                    //else if (sv.Rows[i].Tag is FS.HISFC.Models.Fee.Outpatient.FeeItemList)
                    //{
                    //    alQuitFeeItemList.Add((FS.HISFC.Models.Fee.Outpatient.FeeItemList)sv.Rows[i].Tag);
                    //}
                }
            }

            #endregion

            #region 对物资退费部分进行退库
            //{143CA424-7AF9-493a-8601-2F7B1D635027}
            ArrayList alMate = new ArrayList();
            List<string> MTCancleList = new List<string>();
            for (int i = 0; i < this.fpSpread2_Sheet2.RowCount; i++)
            {
                if (this.fpSpread2_Sheet2.Rows[i].Tag != null && this.fpSpread2_Sheet2.Rows[i].Tag is FeeItemList)
                {
                    FeeItemList fQuit = this.fpSpread2_Sheet2.Rows[i].Tag as FeeItemList;
                    if (fQuit.Item.SysClass.ID.ToString() == "UC")
                    {
                        MTCancleList.Add(fQuit.Order.ID);
                    }
                    //非对照的物资 {40DFDC91-0EC1-4cd4-81BC-0EAE4DE1D3AB}
                    if (fQuit.Item.ItemType == EnumItemType.MatItem)
                    {
                        alMate.Add(fQuit);
                    }
                    else
                    {
                        if (fQuit.MateList.Count > 0)
                        {
                            alMate.Add(fQuit);
                        }
                    }
                }
            }
            FS.HISFC.BizLogic.MedicalTechnology.Appointment appMgr = new FS.HISFC.BizLogic.MedicalTechnology.Appointment();
            MTCancleList.ForEach(t => appMgr.Cancle(t));
            if (alMate.Count > 0)
            {
                //退库
                if (mateIntegrate.MaterialFeeOutputBack(alMate) < 0)
                {
                    //{40DFDC91-0EC1-4cd4-81BC-0EAE4DE1D3AB}
                    medcareInterfaceProxy.Rollback();
                    FS.FrameWork.Management.PublicTrans.RollBack();

                    MessageBox.Show("物资退库失败,\n" + mateIntegrate.Err);

                    return -1;
                }
            }
            #endregion


            ////四舍五入费费用编码
            //string roundFeeItemCode = "F00000053238";
            //ArrayList lst = this.constManager.GetList("ROUNDFEEITEMCODE");
            //if (lst.Count > 0)
            //{
            //    roundFeeItemCode = ((FS.HISFC.Models.Base.Const)lst[0]).ID;
            //}

            #region 对剩余项目收费

            ArrayList feeDetails = new ArrayList();
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                if (this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                {
                    FeeItemList f = (this.fpSpread1_Sheet1.Rows[i].Tag as FeeItemList).Clone();
                    f.FT.OwnCost = f.FT.PubCost = f.FT.PayCost = 0;
                    f.FT.OwnCost = f.FT.TotCost;
                    //f.ConfirmedQty = 0;
                    if (FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)DrugList.Amount].Text) > 0)//by yuyun 解决门诊已发药之后做半退，退费时全退的情况
                    //if (f.Item.Qty > 0)
                    {
                        f.User03 = "HalfQuit";
                        if (f.Item.ID != roundFeeItemCode)//四舍五入费费用不重收
                            feeDetails.Add(f);
                    }
                }
            }
            for (int i = 0; i < this.fpSpread1_Sheet2.RowCount; i++)
            {
                if (this.fpSpread1_Sheet2.Rows[i].Tag is FeeItemList)
                {
                    FeeItemList f = (this.fpSpread1_Sheet2.Rows[i].Tag as FeeItemList).Clone();
                    f.FT.OwnCost = f.FT.PubCost = f.FT.PayCost = 0;
                    f.FT.OwnCost = f.FT.TotCost;
                    //f.IsConfirmed = false;
                    //f.ConfirmedQty = 0;
                    // if (f.Item.Qty > 0)

                    //{06212A22-5FD4-4db3-838C-1790F75FF286}
                    if (FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.Amount].Text) > 0)
                    {
                        FS.HISFC.Models.Fee.Item.Undrug unDrugTemp = this.undrugManager.GetUndrugByCode(f.Item.ID);
                        if (unDrugTemp != null)
                        {
                            f.Item.IsNeedConfirm = unDrugTemp.IsNeedConfirm;
                            f.Item.NeedConfirm = unDrugTemp.NeedConfirm;
                            f.Item.IsNeedBespeak = unDrugTemp.IsNeedBespeak;
                        }

                        //{06212A22-5FD4-4db3-838C-1790F75FF286}
                        if (f.IsConfirmed == true)
                        {
                            int row = this.FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread2_Sheet2);
                            if (row != -1)
                            {
                                FeeItemList quitItem = this.fpSpread2_Sheet2.Rows[row].Tag as FeeItemList;
                                if (confirmIntegrate.UpdateOrDeleteTerminalConfirmApply(f.Order.ID, (int)(f.Item.Qty + quitItem.Item.Qty), (int)quitItem.Item.Qty, FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty, 2)) == -1)
                                {
                                    medcareInterfaceProxy.Rollback();
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("更新终端确认信息出错!" + confirmIntegrate.Err);
                                    return -1;
                                }
                            }
                        }


                        f.User03 = "HalfQuit";
                        if (f.Item.ID != roundFeeItemCode)//四舍五入费费用不重收
                            feeDetails.Add(f);
                    }
                }
            }

            #endregion

            string returnCostString = string.Empty;

            #region 补收费用明细

            //{BBE9766A-A539-485e-A03B-9972DC675538} 退费补收
            ArrayList addFeeItemList = this.ucDisplay1.GetFeeItemList();
            if (addFeeItemList != null && addFeeItemList.Count > 0)
            {
                if (this.cmbRegDept.Tag == null || this.cmbRegDept.Tag.ToString() == string.Empty || this.cmbRegDept.Text.Trim() == string.Empty)
                {
                    medcareInterfaceProxy.Rollback();
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("请选择补收费用的看诊科室!" + confirmIntegrate.Err);

                    return -1;
                }

                if (this.cmbDoct.Tag == null || this.cmbDoct.Tag.ToString() == string.Empty || this.cmbDoct.Text.Trim() == string.Empty)
                {
                    medcareInterfaceProxy.Rollback();
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("请选择补收费用的开立医生!" + confirmIntegrate.Err);

                    return -1;
                }

                foreach (FeeItemList f in addFeeItemList)
                {
                    string doctCode = string.Empty;
                    doctCode = this.cmbDoct.Tag.ToString();
                    FS.HISFC.Models.Base.Employee empl = this.managerIntegrate.GetEmployeeInfo(doctCode);
                    if (empl != null)
                    {
                        f.RecipeOper.Dept.ID = empl.Dept.ID;
                    }
                    //看诊医生 {83283AE6-6D16-4b69-9B42-F2E0754FC8B2}
                    ((FS.HISFC.Models.Registration.Register)f.Patient).DoctorInfo.Templet.Doct.ID = this.cmbDoct.Tag.ToString();

                    f.RecipeOper.ID = doctCode;
                    (f.Patient as FS.HISFC.Models.Registration.Register).DoctorInfo.Templet.Dept.ID = this.cmbRegDept.Tag.ToString();
                    f.NoBackQty = f.Item.Qty;
                }

                feeDetails.AddRange(addFeeItemList);
            }
            //{BBE9766A-A539-485e-A03B-9972DC675538} 结束

            #endregion


            if (feeDetails.Count > 0)
            {
                #region 半退
                //{4690DECD-6AB9-42d7-8415-3E788907C46D}
                //if (isHaveRebateCost)
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack();
                //    medcareInterfaceProxy.Rollback();
                //    MessageBox.Show("该张发票存在优惠金额,请全退!");
                //    return -1;
                //}

                string errText = string.Empty, invoiceNo = string.Empty, realInvoiceNo = string.Empty;


                FS.HISFC.Models.Registration.Register tmpReg = null;

                //如果不是体检患者,那么重新获得患者挂号信息
                if (!(this.patient.ChkKind == "1" || this.patient.ChkKind == "2"))
                {

                    tmpReg = registerIntegrate.GetByClinic(this.patient.ID);
                    if (tmpReg == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        medcareInterfaceProxy.Rollback();
                        medcareInterfaceProxy.Disconnect();
                        MessageBox.Show("获得挂号信息失败!" + this.registerIntegrate.Err);

                        return -1;
                    }

                    #region 修改公费患者信息作废先

                    /*屏蔽不给用,如果在确认收费界面取消收费，患者挂号信息实体已被修改
                     *另外如果修改结算类型为自费或者医保，或者更新医疗证号，记账单号觉得有点混乱了，
                     *所以此功能先屏蔽掉
                    */
                    if (this.patient.Pact.PayKind.ID == "03" && this.IsCanModifyPatientInfo == true)
                    {
                        DialogResult _Reu;
                        _Reu = MessageBox.Show("需要修改结算患者信息吗?", "修改结算患者信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (_Reu == DialogResult.Yes)
                        {
                            if (this.patient != null && this.patient.Pact.PayKind.ID == "03")
                            {
                                FS.HISFC.Components.OutpatientFee.Forms.frmPatientInfo frmPatientInfo = new FS.HISFC.Components.OutpatientFee.Forms.frmPatientInfo();
                                frmPatientInfo.Init();
                                frmPatientInfo.IsNeedJZD = this.IsNeedJZD;
                                FS.HISFC.Models.Registration.Register tmpOrgReg = null;
                                tmpOrgReg = this.patient.Clone();

                                frmPatientInfo.PatientInfo = tmpOrgReg;

                                frmPatientInfo.ShowDialog();
                                if (frmPatientInfo.IsConfirm)
                                {
                                    this.patient.Pact = frmPatientInfo.PatientInfo.Pact;
                                    this.patient.SSN = frmPatientInfo.PatientInfo.SSN;
                                    this.patient.LSH = frmPatientInfo.PatientInfo.LSH;
                                    this.patient.User03 = frmPatientInfo.PatientInfo.User03;
                                    //重新设置必须加
                                    this.medcareInterfaceProxy.SetPactCode(this.patient.Pact.ID);
                                }
                                frmPatientInfo.Clear();
                            }
                        }
                    }

                    #endregion

                    tmpReg.Pact = this.patient.Pact;
                    tmpReg.User03 = this.patient.User03;//限额等级的GROUPID
                    tmpReg.LSH = this.patient.LSH;//记账单号，广医肿瘤用其他医院没用到
                    tmpReg.SSN = this.patient.SSN;//医疗证号
                    this.patient = tmpReg.Clone();
                }
                returnValue = medcareInterfaceProxy.GetRegInfoOutpatient(this.patient);
                if (returnValue == -1)
                {
                    medcareInterfaceProxy.Rollback();
                    medcareInterfaceProxy.Disconnect();
                    FS.FrameWork.Management.PublicTrans.RollBack();

                    MessageBox.Show("待遇接口获得接口患者基本信息失败!" + medcareInterfaceProxy.ErrMsg);

                    return -1;
                }
                if (tmpReg != null && tmpReg.IDCard != this.patient.IDCard)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    medcareInterfaceProxy.Rollback();
                    medcareInterfaceProxy.Disconnect();
                    MessageBox.Show("身份证与上次收费信息不符,可能选择错误!不能退费!");

                    return -1;
                }

                #region 修改公费比例

                if (this.patient.Pact.PayKind.ID == "03" && IsCanModifyRate == true)
                {
                    DialogResult _Reu;
                    _Reu = MessageBox.Show("需要修改公费特批比例吗?", "公费修改比例", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (_Reu == DialogResult.Yes)
                    {
                        if (this.patient == null)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            medcareInterfaceProxy.Rollback();
                            medcareInterfaceProxy.Disconnect();

                            MessageBox.Show("患者基本信息为空!", "错误");

                            return -1;
                        }
                        if (this.patient != null && this.patient.Pact.PayKind.ID == "03")
                        {
                            this.Focus();
                            ArrayList alFee = feeDetails;
                            ucModifyItemRate modifyRate = new ucModifyItemRate();
                            modifyRate.FeeDetails = alFee;
                            modifyRate.InitFeeDetails();
                            FS.FrameWork.WinForms.Classes.Function.PopShowControl(modifyRate);
                            FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "修改比例";
                        }
                    }
                }

                #endregion

                foreach (FeeItemList f in feeDetails)
                {
                    f.FeeOper.OperTime = nowTime;
                    f.FTSource = "0";
                    f.ConfirmedInjectCount = 0;
                }
                if (choosePayMode == "0")//选择支付方式
                {
                    #region 选择支付方式
                    decimal ownCost = 0, payCost = 0, pubCost = 0, totCostPayMode = 0; decimal overDrugFee = 0; decimal selfDrugFee = 0;
                    decimal rebateCost = 0;

                    if (this.patient.Pact.PayKind.ID == "01")//自费，直接累加各项目金额
                    {
                        foreach (FeeItemList f in feeDetails)
                        {
                            //{4690DECD-6AB9-42d7-8415-3E788907C46D}
                            //存在套餐支付时，不加载优惠金额，套餐结算不会与打折同时发生（程序已做控制）
                            //因此发生套餐结算时，优惠金额为零

                            //{BD46CBA6-A670-40cf-A1FD-EAFBC9797D04}
                            //否决上面的说话，由于在明细中新增了字段用来存储套餐中的优惠，使之得以实现
                            //{B215BA66-2807-4dd2-9403-7EECC07F4787}根据退的比例算出优惠金额
                            foreach (FeeItemList of in oldFeeItemLists)
                            {
                                //{F80D5E5D-CACC-4CCE-B233-ACDE8C34095F} 加上收费流水号对比
                                if (f.Item.ID == of.Item.ID && f.RecipeNO == of.RecipeNO && f.SequenceNO == of.SequenceNO)//{43CD957D-BACF-4DBA-A912-3F44580BA25E}
                                {
                                    f.FT.RebateCost = of.FT.RebateCost * f.Item.Qty / of.Item.Qty;
                                    //{416C9A04-3A94-4d41-8F0A-14B73622C65E}
                                    f.FT.PackageEco = Math.Ceiling(of.FT.PackageEco * f.Item.Qty * 100 / of.Item.Qty) / 100;
                                    //由于算法不一样，此处做判断
                                    //{4EE5ADE2-900D-4fc1-BFC6-E9CDD90B028E}
                                    if (of.FT.RebateCost == of.FT.PackageEco)
                                    {
                                        f.FT.RebateCost = f.FT.PackageEco;
                                    }
                                    break;
                                }
                            }
                            if (f.IsPackage == "1")
                            {
                                //如果是套餐，则优惠金额扣除从套餐中带来的部分，剩下的优惠为收费员手动打折的金额
                                f.FT.RebateCost -= f.FT.PackageEco;
                            }

                            rebateCost += f.FT.RebateCost;
                            ownCost += f.FT.OwnCost;
                            //payCost += f.FT.PayCost;
                            //pubCost += f.FT.PubCost;
                            totCostPayMode += f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost;
                            //if (f.Item.IsPharmacy)
                            if (f.Item.ItemType == EnumItemType.Drug)
                            {
                                overDrugFee += FS.FrameWork.Function.NConvert.ToDecimal(f.FT.ExcessCost);
                                selfDrugFee += FS.FrameWork.Function.NConvert.ToDecimal(f.FT.DrugOwnCost);
                            }

                            //f.NoBackQty = f.Item.Qty;
                        }
                    }
                    if (this.patient.Pact.PayKind.ID == "02")//医保
                    {
                        foreach (FeeItemList f in feeDetails)
                        {
                            totCostPayMode += f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost;
                        }
                        ownCost = totCostPayMode - this.patient.SIMainInfo.PubCost - this.patient.SIMainInfo.PayCost;
                        payCost += this.patient.SIMainInfo.PayCost;
                        pubCost += this.patient.SIMainInfo.PubCost;
                    }

                    #region 公费重新计算

                    if (this.patient.Pact.PayKind.ID == "03")//公费
                    {
                        returnValue = this.medcareInterfaceProxy.PreBalanceOutpatient(this.patient, ref feeDetails);
                        if (returnValue == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("获得公费结算信息失败!" + this.medcareInterfaceProxy.ErrMsg);
                            this.medcareInterfaceProxy.Rollback();
                            this.medcareInterfaceProxy.Disconnect();

                            return -1;
                        }

                        foreach (FeeItemList f in feeDetails)
                        {
                            overDrugFee += FS.FrameWork.Function.NConvert.ToDecimal(f.FT.ExcessCost);
                            selfDrugFee += FS.FrameWork.Function.NConvert.ToDecimal(f.FT.DrugOwnCost);
                        }

                        totCostPayMode = this.patient.SIMainInfo.TotCost;
                        ownCost = this.patient.SIMainInfo.OwnCost;
                        payCost = this.patient.SIMainInfo.PayCost;
                        pubCost = this.patient.SIMainInfo.PubCost;

                    }

                    #endregion

                    ownCost = FS.FrameWork.Public.String.FormatNumber(ownCost, 2);
                    payCost = FS.FrameWork.Public.String.FormatNumber(payCost, 2);
                    pubCost = FS.FrameWork.Public.String.FormatNumber(pubCost, 2);
                    totCostPayMode = FS.FrameWork.Public.String.FormatNumber(totCostPayMode, 2);

                    #region 收费金额取整作废先20121022


                    //-------------------------------delete 2012年10月22日 XF-----------------------------
                    /*
                    #region 收费金额取整
                    string s = this.controlParamIntegrate.GetControlParam<string>("MZ9927", true, string.Empty);
                    bool isGetByFirstItemFeeCode = this.controlParamIntegrate.GetControlParam<bool>("MZ9925", true, true);
                    if (s != string.Empty)
                    {
                        bool isInsertItemList = NConvert.ToBoolean(s);

                        if (isInsertItemList)
                        {
                            iOutPatientFeeRoundOff = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject<
                                    FS.HISFC.BizProcess.Interface.FeeInterface.IOutPatientFeeRoundOff>(this.GetType());
                            if (iOutPatientFeeRoundOff == null)
                            {
                                MessageBox.Show("费用取整接口未配置！");
                                return -1;
                            }
                            FeeItemList feeItemList = new FeeItemList();

                            // 凑整费最小费用，拿费用列表第一条记录最小费用
                            string drugFeeCode = "";

                            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList item in feeDetails)
                            {
                                if (string.IsNullOrEmpty(item.Item.MinFee.ID))
                                {
                                    continue;
                                }

                                drugFeeCode = item.Item.MinFee.ID;
                                break;
                            }
                            if (!string.IsNullOrEmpty(drugFeeCode) && isGetByFirstItemFeeCode)
                            {
                                feeItemList.User03 = drugFeeCode;
                            }

                            iOutPatientFeeRoundOff.OutPatientFeeRoundOff(this.patient, ref ownCost, ref feeItemList, (feeDetails[0] as FeeItemList).RecipeSequence);
                            if (feeItemList.Item.ID != "")
                            {
                                totCostPayMode = ownCost + payCost + pubCost;
                                this.patient.SIMainInfo.OwnCost = ownCost;
                                this.patient.SIMainInfo.TotCost = totCostPayMode;
                                feeDetails.Add(feeItemList);
                            }
                        }
                    }
                    #endregion
                    */
                    //-------------------------------delete 2012年10月22日 XF-----------------------------
                    #endregion

                    #region 门诊收费的收费金额取整

                    decimal shouldPayCost = 0;
                    if (this.patient.Pact.PayKind.ID == "03")
                    {
                        shouldPayCost = ownCost + payCost - rebateCost;
                    }
                    else
                    {
                        shouldPayCost = ownCost - rebateCost;
                    }

                    string isRoundFeeByDetail = this.controlParamIntegrate.GetControlParam<string>("MZ9927", true, string.Empty);

                    #region 收费金额取整
                    if (isRoundFeeByDetail != string.Empty)
                    {
                        bool isInsertItemList = NConvert.ToBoolean(isRoundFeeByDetail);
                        if (isInsertItemList)
                        {
                            iOutPatientFeeRoundOff = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject<
                                    FS.HISFC.BizProcess.Interface.FeeInterface.IOutPatientFeeRoundOff>(this.GetType());
                            if (iOutPatientFeeRoundOff != null)
                            {
                                FeeItemList feeItemList = new FeeItemList();
                                // 凑整费最小费用，拿费用列表第一条记录最小费用
                                string drugFeeCode = "";

                                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList item in feeDetails)
                                {
                                    if (string.IsNullOrEmpty(item.Item.MinFee.ID))
                                    {
                                        continue;
                                    }

                                    drugFeeCode = item.Item.MinFee.ID;
                                    break;
                                }
                                if (!string.IsNullOrEmpty(drugFeeCode))
                                {
                                    feeItemList.User03 = drugFeeCode;
                                }

                                if (this.patient.Pact.PayKind.ID == "03")
                                //公费部分对pay_cost也进行四舍五入
                                {
                                    iOutPatientFeeRoundOff.OutPatientFeeRoundOff(this.patient, ref shouldPayCost, ref feeItemList, (feeDetails[0] as FeeItemList).RecipeSequence);
                                    if (feeItemList.Item.ID != "")
                                    {
                                        {
                                            ownCost = shouldPayCost - payCost + rebateCost;//加上优惠金额
                                            totCostPayMode = ownCost + payCost + pubCost;
                                            feeItemList.ItemRateFlag = "1";
                                            this.patient.SIMainInfo.OwnCost = ownCost;
                                            this.patient.SIMainInfo.TotCost = totCostPayMode;
                                            feeDetails.Add(feeItemList);
                                        }
                                    }
                                }
                                else
                                {
                                    iOutPatientFeeRoundOff.OutPatientFeeRoundOff(this.patient, ref shouldPayCost, ref feeItemList, (feeDetails[0] as FeeItemList).RecipeSequence);
                                    if (feeItemList.Item.ID != "")
                                    {
                                        ownCost = shouldPayCost + rebateCost;//加上优惠金额
                                        totCostPayMode = ownCost + payCost + pubCost;
                                        this.patient.SIMainInfo.OwnCost = ownCost;
                                        this.patient.SIMainInfo.TotCost = totCostPayMode;
                                        feeDetails.Add(feeItemList);
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    #endregion

                    //{333D2AD8-DC4A-4c30-A14E-D6815AC858F9}
                    quitInvoiceNO = payInvoiceNo;

                    FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientPopupFee frmBalance = this.popFeeControl = this.feeIntegrate.GetPlugIns<FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientPopupFee>
                  (FS.HISFC.BizProcess.Integrate.Const.INTERFACE_POP_FEE, new Froms.frmDealBalance());
                    //frmBalance.Trans = t;
                    //this.frmBalance.ucDealBalance1.FrmDisplay = frmDisplay;
                    this.isAutoBankTrans = this.controlParamIntegrate.GetControlParam<bool>("MZ9001", true, false);
                    //银联接口
                    iBankTrans = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject<
                        FS.HISFC.BizProcess.Interface.FeeInterface.IBankTrans>(this.GetType());
                    if (iBankTrans == null)
                    {
                        iBankTrans = new Forms.frmBankTrans();
                    }
                    frmBalance.BankTrans = iBankTrans;
                    frmBalance.IsAutoBankTrans = this.isAutoBankTrans;
                    frmBalance.Init();
                    //如果是自助机退费,则允许使用账户里的钱,由于改接口太麻烦了,只好用反射  add by yerl
                    if (IsAtm)
                    {
                        try
                        {
                            frmBalance.GetType().GetProperty("IsAccountPay").SetValue(frmBalance, true, null);
                        }
                        catch { }
                    }
                    frmBalance.IsCashPay = isCashPay;
                    frmBalance.FeeButtonClicked += new FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateFee(frmBalance_FeeButtonClicked);
                    frmBalance.PatientInfo = this.patient;
                    frmBalance.SelfDrugCost = selfDrugFee;
                    frmBalance.OverDrugCost = overDrugFee;
                    frmBalance.RealCost = ownCost + payCost;
                    frmBalance.OwnCost = ownCost;
                    frmBalance.PayCost = payCost;
                    frmBalance.PubCost = pubCost;
                    frmBalance.TotCost = totCostPayMode;
                    frmBalance.TotOwnCost = ownCost + payCost;
                    frmBalance.FeeDetails = feeDetails;
                    frmBalance.IsQuitFee = true;
                    //frmBalance.Trans = this.t;

                    FS.HISFC.Models.Base.Employee employee = this.outpatientManager.Operator as FS.HISFC.Models.Base.Employee;

                    // 账户临时发票半退时取临时发票号
                    // {69245A77-FB7A-42ed-844B-855E7ABC612F}
                    int iReturnValue = this.feeIntegrate.GetInvoiceNO(employee, "C", blnIsAccountInvoice, ref invoiceNo, ref realInvoiceNo, ref errText);
                    if (iReturnValue == -1)
                    {
                        medcareInterfaceProxy.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(errText);

                        return -1;
                    }

                    //{18B0895D-9F55-4d93-B374-69E96F296D0D}  门诊取发票、半退Bug问题
                    Class.Function.IsQuitFee = true;

                    ArrayList alInvoiceAndDetails = Class.Function.MakeInvoice(this.feeIntegrate, this.patient, feeDetails, invoiceNo, realInvoiceNo, ref errText);
                    if (alInvoiceAndDetails == null)
                    {
                        medcareInterfaceProxy.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(errText);

                        return -1;
                    }
                    #region liuq 2007-8-27 发票费用明细
                    frmBalance.InvoiceFeeDetails = (ArrayList)alInvoiceAndDetails[2];
                    #endregion
                    frmBalance.InvoiceDetails = (ArrayList)alInvoiceAndDetails[1];

                    // 设置发票状态
                    // {69245A77-FB7A-42ed-844B-855E7ABC612F}
                    ArrayList invoices = (ArrayList)alInvoiceAndDetails[0];
                    foreach (Balance invoice in invoices)
                    {
                        invoice.IsAccount = blnIsAccountInvoice;
                        invoice.User02 = this.patient.LSH;//重新收费时取上次收费的记账单号
                        invoice.User03 = this.patient.User03;//限额等级
                        invoice.Patient.SSN = this.patient.SSN;//医疗证号
                    }
                    if (this.patient.Pact.PayKind.ID != "03")
                    {
                        #region 社保结算
                        // 不需要考虑患者身份
                        //if (this.patient.Pact.PayKind.ID == "02" || this.patient.Pact.PayKind.ID == "03")
                        //{
                        ownCost = decimal.Zero;
                        payCost = decimal.Zero;
                        pubCost = decimal.Zero;
                        totCostPayMode = decimal.Zero;

                        rebateCost = 0;

                        //foreach (Balance invoice in (ArrayList)alInvoiceAndDetails[0])
                        //{
                        //    if (invoice.Memo == "4")//记账发票!
                        //    {
                        //        invoice.FT.PubCost = pubCost;
                        //        invoice.FT.PayCost = payCost;
                        //        invoice.FT.OwnCost = invoice.FT.TotCost - pubCost - payCost;
                        //    }
                        //      ArrayList tempFeeItemListArray = (ArrayList)alInvoiceAndDetails[2];
                        //    for (int i = 0; i < tempFeeItemListArray.Count; i++)
                        //    {

                        //        FeeItemList tempFeeItemList = ((ArrayList)tempFeeItemListArray[i])[0] as FeeItemList;

                        //        if (invoice.Invoice.ID == tempFeeItemList.Invoice.ID)
                        //        {
                        //            ArrayList myFeeItemlist = new ArrayList();
                        //            myFeeItemlist = (ArrayList)tempFeeItemListArray[i];
                        //删除本次因为错误或者其他原因上传的明细
                        returnValue = this.medcareInterfaceProxy.DeleteUploadedFeeDetailsAllOutpatient(this.patient);

                        //重新上传所有明细
                        returnValue = this.medcareInterfaceProxy.UploadFeeDetailsOutpatient(this.patient, ref feeDetails);
                        if (returnValue == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            this.medcareInterfaceProxy.Rollback();
                            MessageBox.Show("上传费用明细失败!" + this.medcareInterfaceProxy.ErrMsg);

                            return -1;
                        }
                        returnValue = this.medcareInterfaceProxy.PreBalanceOutpatient(this.patient, ref feeDetails);
                        if (returnValue == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("获得医保预结算信息失败!" + this.medcareInterfaceProxy.ErrMsg);
                            this.medcareInterfaceProxy.Rollback();
                            this.medcareInterfaceProxy.Disconnect();

                            return -1;
                        }

                        foreach (FeeItemList f in feeDetails)
                        {
                            rebateCost += f.FT.RebateCost;
                        }


                        ownCost += this.patient.SIMainInfo.OwnCost;
                        payCost += this.patient.SIMainInfo.PayCost;
                        pubCost += this.patient.SIMainInfo.PubCost;
                        //{21EEC08E-53DA-458b-BEA3-0036EF6E3D37}
                        //    + this.patient.SIMainInfo.OfficalCost
                        //    + this.patient.SIMainInfo.OverCost;
                        totCostPayMode += this.patient.SIMainInfo.PayCost;
                        totCostPayMode += this.patient.SIMainInfo.OwnCost;
                        totCostPayMode += this.patient.SIMainInfo.PubCost;
                        //+ this.patient.SIMainInfo.OfficalCost
                        //+ this.patient.SIMainInfo.OverCost;
                        frmBalance.RealCost = ownCost;
                        frmBalance.OwnCost = ownCost;
                        frmBalance.PayCost = payCost;
                        frmBalance.PubCost = pubCost;
                        frmBalance.TotCost = totCostPayMode;
                        frmBalance.TotOwnCost = ownCost;

                        frmBalance.RebateRate = rebateCost;
                        ////断开待遇接口连接
                        //this.medcareInterfaceProxy.Rollback();
                        //this.medcareInterfaceProxy.Disconnect();
                        //FS.FrameWork.Management.PublicTrans.RollBack();

                        ////重新赋值
                        //invoice.FT.OwnCost = this.patient.SIMainInfo.OwnCost;
                        //invoice.FT.PubCost = this.patient.SIMainInfo.PubCost;
                        //invoice.FT.PayCost = this.patient.SIMainInfo.PayCost;
                        //        }
                        //    }

                        //}
                        //}

                        #endregion
                    }

                    againFeeItemLists = new ArrayList();

                    againFeeItemLists = feeDetails;

                    frmBalance.Invoices = invoices;

                    modifiedBalancePays = payList;

                    if (!((Form)frmBalance).Visible)
                    {
                        this.Focus();
                        frmBalance.SetControlFocus();
                        isSuccess = false;
                        frmBalance.IsPushCancelButton = true;
                        ((Form)frmBalance).Location = new Point(this.Location.X + 150, this.Location.Y + 200);
                        ((Form)frmBalance).ShowDialog();
                    }
                    //原来使用frmBalance.IsPushCancelButton来判断有没收费成功,后来发现有其它BUG,所以添加一个isSuccess来辅助判断add by yerl
                    if (!isSuccess || frmBalance.IsPushCancelButton == true)
                    {

                        #region 半退重收更新原明细的执行状态

                        foreach (FeeItemList f in feeDetails)
                        {
                            iReturn = outpatientManager.UpdateSampleState(f.RecipeNO, f.Order.ID);
                            if (iReturn <= 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("更新样本状态信息出错!" + outpatientManager.Err);
                                return -1;
                            }
                        }

                        #endregion

                        FS.FrameWork.Management.PublicTrans.RollBack();
                        medcareInterfaceProxy.Rollback();
                        return -1;
                    }

                    #endregion

                }
                else
                {

                    #region 按支付方式退
                    decimal ownCost = 0, payCost = 0, pubCost = 0; decimal totCost = 0;
                    decimal overDrugFee = 0; decimal selfDrugFee = 0;//加超标药金额和自费药金额
                    if (this.patient.Pact.PayKind.ID == "01") //自费，直接累加各项目金额
                    {
                        foreach (FeeItemList f in feeDetails)
                        {
                            ownCost += f.FT.OwnCost;
                            payCost += f.FT.PayCost;
                            pubCost += f.FT.PubCost;
                            totCost += f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost;
                        }
                    }
                    if (this.patient.Pact.PayKind.ID == "02")//医保
                    {
                        foreach (FeeItemList f in feeDetails)
                        {
                            totCost += f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost;
                        }
                        ownCost = totCost - this.patient.SIMainInfo.PubCost - this.patient.PayCost;
                        payCost += this.patient.SIMainInfo.PayCost;
                        pubCost += this.patient.SIMainInfo.PubCost;
                    }

                    #region 公费重新计算

                    if (this.patient.Pact.PayKind.ID == "03")//公费
                    {
                        returnValue = this.medcareInterfaceProxy.PreBalanceOutpatient(this.patient, ref feeDetails);
                        if (returnValue == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("获得公费结算信息失败!" + this.medcareInterfaceProxy.ErrMsg);
                            this.medcareInterfaceProxy.Rollback();
                            this.medcareInterfaceProxy.Disconnect();

                            return -1;
                        }

                        foreach (FeeItemList f in feeDetails)
                        {
                            overDrugFee += FS.FrameWork.Function.NConvert.ToDecimal(f.FT.ExcessCost);
                            selfDrugFee += FS.FrameWork.Function.NConvert.ToDecimal(f.FT.DrugOwnCost);
                        }

                        totCost = this.patient.SIMainInfo.TotCost;
                        ownCost = this.patient.SIMainInfo.OwnCost;
                        payCost = this.patient.SIMainInfo.PayCost;
                        pubCost = this.patient.SIMainInfo.PubCost;

                    }
                    #endregion

                    FS.HISFC.Models.Base.Employee employee = this.outpatientManager.Operator as FS.HISFC.Models.Base.Employee;

                    // 账户临时发票半退时取临时发票号
                    // {69245A77-FB7A-42ed-844B-855E7ABC612F}
                    iReturn = this.feeIntegrate.GetInvoiceNO(employee, "C", this.blnIsAccountInvoice, ref invoiceNo, ref realInvoiceNo, ref errText);
                    if (iReturn < 0)
                    {
                        medcareInterfaceProxy.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(errText);

                        return -1;
                    }
                    //生成新发票

                    //{18B0895D-9F55-4d93-B374-69E96F296D0D}  门诊取发票、半退Bug问题
                    Class.Function.IsQuitFee = true;

                    ArrayList invoicesAndDetails = Class.Function.MakeInvoice(this.feeIntegrate, this.patient, feeDetails, invoiceNo, realInvoiceNo, ref errText, FS.FrameWork.Management.PublicTrans.Trans);
                    if (invoicesAndDetails == null || invoicesAndDetails.Count == 0)
                    {
                        medcareInterfaceProxy.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(errText);

                        return -1;
                    }
                    ArrayList invoices = (ArrayList)invoicesAndDetails[0];
                    foreach (Balance invoice in invoices)
                    {
                        invoice.IsAccount = this.blnIsAccountInvoice;
                        invoice.User02 = this.patient.LSH;//公费记账单号
                        invoice.User03 = this.patient.User03;//限额等级
                        invoice.Patient.SSN = this.patient.SSN;//医疗证号
                    }


                    if (this.patient.Pact.PayKind.ID == "02")
                    {
                        foreach (Balance invoice in invoices)
                        {
                            if (invoice.Memo == "4")//记账发票!
                            {
                                invoice.FT.PubCost = pubCost;
                                invoice.FT.PayCost = payCost;
                                invoice.FT.OwnCost = invoice.FT.TotCost - pubCost - payCost;
                            }
                        }
                    }
                    ArrayList alTempInvoiceDetails = new ArrayList();
                    ArrayList alFinalInvoiceDetails = new ArrayList();
                    foreach (ArrayList alTemp in ((ArrayList)invoicesAndDetails[1]))
                    {
                        alTempInvoiceDetails.Add(alTemp[0]);
                    }
                    alFinalInvoiceDetails.Add(alTempInvoiceDetails);



                    BalancePay pFinal = new BalancePay();

                    //					foreach(FeeItemList f in feeDetails)
                    //					{
                    //						totCost += f.FT.OwnCost + f.FT.PayCost;
                    //					}
                    decimal orgCost = 0;
                    foreach (BalancePay p in payList)
                    {
                        //因为此时的支付方式为负
                        orgCost += -p.FT.RealCost;
                    }
                    decimal returnCost = orgCost - totCost;
                    decimal returnCostCent = Class.Function.DealCent(returnCost);
                    decimal centCost = returnCost - returnCostCent;
                    pFinal.FT.TotCost = totCost;
                    pFinal.FT.RealCost = totCost + centCost;
                    pFinal.PayType.Name = "现金";
                    pFinal.PayType.ID = "CA";

                    ArrayList alPay = new ArrayList();
                    alPay.Add(pFinal);

                    //退费,并且是默认发票号方式时,不需要再次更新发票号
                    this.feeIntegrate.IsNeedUpdateInvoiceNO = false;

                    // 收费
                    // {69245A77-FB7A-42ed-844B-855E7ABC612F}
                    bool bReturn = this.feeIntegrate.ClinicFee(FS.HISFC.Models.Base.ChargeTypes.Fee, this.blnIsAccountInvoice, this.patient,
                        invoices, alFinalInvoiceDetails, feeDetails, feeDetails, alPay, ref errText);

                    if (!bReturn)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        medcareInterfaceProxy.Rollback();
                        if (errText != string.Empty)
                        {
                            MessageBox.Show(errText);
                        }

                        return -1;
                    }

                    //if (InterfaceManager.GetIOrder() != null)
                    //{
                    //    if (InterfaceManager.GetIOrder().SendFeeInfo(this.patient, alQuitFeeItemList, false) < 0)
                    //    {
                    //        FS.FrameWork.Management.PublicTrans.RollBack();
                    //        this.medcareInterfaceProxy.Rollback();
                    //        MessageBox.Show(this, "退费失败，请向系统管理员报告错误信息："   +InterfaceManager.GetIOrder().Err, "提示>>", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //        return -1;
                    //    }
                    //}

                    if (InterfaceManager.GetIOrder().SendFeeInfo(this.patient, oldFeeItemLists, false) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.medcareInterfaceProxy.Rollback();
                        MessageBox.Show(this, "退费失败，请向系统管理员报告错误信息：" + InterfaceManager.GetIOrder().Err, "提示>>", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return -1;
                    }
                    if (InterfaceManager.GetIOrder().SendFeeInfo(this.patient, feeDetails, true) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.medcareInterfaceProxy.Rollback();
                        MessageBox.Show(this, "退费失败，请向系统管理员报告错误信息：" + InterfaceManager.GetIOrder().Err, "提示>>", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return -1;
                    }

                    this.medcareInterfaceProxy.Commit();
                    FS.FrameWork.Management.PublicTrans.Commit();



                    returnCostString = "应退金额: " + Class.Function.DealCent(returnCost).ToString();
                    tbQuitCash.Text = Class.Function.DealCent(returnCost).ToString();
                    #endregion
                }

                

                //FS.FrameWork.Management.PublicTrans.Commit();
                //this.medcareInterfaceProxy.Commit();
                MessageBox.Show("退费成功!" + "\n" + returnCostString);

                #endregion
            }

            else
            {
                #region 全退

                decimal orgCost = 0;
                decimal otherCost = 0m;
                bool isHaveCard = false;
                #region liu.xq1008
                //foreach (BalancePay p in payModes)
                //{
                //    //因为此时的支付方式为负
                //    if (p.PayType.ID.ToString() == "CA")
                //    {
                //        orgCost += -p.FT.RealCost;
                //    }
                //    if (p.PayType.ID.ToString() != "CA")
                //    {
                //        isHaveCard = true;
                //        otherCost += -p.FT.RealCost;
                //    }
                //}
                #endregion

                decimal caCost = 0m;
                decimal chCost = 0m;
                decimal cdCost = 0m;
                decimal dbCost = 0m;
                decimal pbCost = 0m;
                decimal xxCost = 0m;

                if (isHaveCard)
                {
                    if (otherCost > 0)
                    {
                        returnCostString = "应退金额:现金 " + (CancelOwnCost - CancelRebateCost).ToString() + "  其他支付方式:" + CancelPubCost.ToString();
                    }
                    else
                    {
                        returnCostString = "应退金额: " + (CancelOwnCost - CancelRebateCost).ToString();
                    }
                }
                else
                {
                    Hashtable htShowPayMode = new Hashtable();
                    foreach (BalancePay p in payList)
                    {
                        BalancePay payClone = p.Clone();
                        payClone.FT.TotCost = payClone.FT.TotCost;
                        if (htShowPayMode.ContainsKey(payClone.PayType.ID))
                        {
                            BalancePay tmp = htShowPayMode[payClone.PayType.ID] as BalancePay;
                            tmp.FT.TotCost += payClone.FT.TotCost;
                            htShowPayMode.Remove(payClone.PayType.ID);
                            htShowPayMode.Add(payClone.PayType.ID, tmp);
                        }
                        else
                        {
                            htShowPayMode.Add(payClone.PayType.ID, p);
                        }
                    }


                    foreach (BalancePay p in htShowPayMode.Values)
                    {
                        string payTypeName = helpPayMode.GetName(p.PayType.ID);
                        returnCostString += payTypeName + "：" + p.FT.TotCost.ToString("F2") + "\n";
                    }
                    returnCostString = "应退金额: " + CancelTotCost.ToString("F2") + "\n其中：\n" + returnCostString;

                }

                if (InterfaceManager.GetIOrder() != null)
                {
                    if (InterfaceManager.GetIOrder().SendFeeInfo(this.patient, alQuitFeeItemList, false) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.medcareInterfaceProxy.Rollback();
                        MessageBox.Show(this, "退费失败，请向系统管理员报告错误信息：" + InterfaceManager.GetIOrder().Err, "提示>>", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return -1;
                    }
                }

                #region 处理积分数据

                if (IsCouponModuleInUse)
                {
                    //{333D2AD8-DC4A-4c30-A14E-D6815AC858F9}
                    string resultCode = "0";
                    string errorMsg = "";
                    int reqRtn = -1;

                    //退费消耗的积分
                    if (quitCostCouponAmount != 0)
                    {
                        reqRtn = FS.HISFC.BizProcess.Integrate.WSHelper.CostCoupon(patient.PID.CardNO, patient.Name, patient.PID.CardNO, patient.Name, "MZTF", payInvoiceNo, quitCostCouponAmount, 0.0m, out resultCode, out errorMsg);
                        if (reqRtn < 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("处理会员积分出错:" + errorMsg);
                            return -1;
                        }
                    }

                    //扣除本单产生的积分
                    //{416C9A04-3A94-4d41-8F0A-14B73622C65E}
                    //退费时，无论是否有积分需要扣除，都需要调用，因为涉及到老带新，需要通过这部分信息去进行退积分
                    //if (quitOperateCouponAmount != 0)
                    //{c2d43b9d-eda4-4f87-92c1-f6fdb08d9d04}取多倍积分

                    if (!string.IsNullOrEmpty(payInvoiceNo))  //0元退费没有发票号
                    {
                        reqRtn = FS.HISFC.BizProcess.Integrate.WSHelper.OperateCoupon(patient.PID.CardNO, patient.Name, "MZTF", payInvoiceNo, quitOperateCouponAmount, 0.0m, "", "1", out resultCode, out errorMsg);
                        if (reqRtn < 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("处理会员积分出错:" + errorMsg);

                            if (quitCostCouponAmount != 0)
                            {
                                reqRtn = FS.HISFC.BizProcess.Integrate.WSHelper.CostCoupon(patient.PID.CardNO, patient.Name, patient.PID.CardNO, patient.Name, "MZSF", payInvoiceNo, -quitCostCouponAmount, 0.0m, out resultCode, out errorMsg);

                                if (reqRtn < 0)
                                {
                                    MessageBox.Show("回滚会员积分出错，请联系信息科处理，错误详情:" + errorMsg);
                                }
                            }

                            return -1;
                        }
                    }
                }
                #endregion

                FS.FrameWork.Management.PublicTrans.Commit();
                this.medcareInterfaceProxy.Commit();

                this.ucCostDisplay1.Clear();
                this.ucDisplay1.Clear();
                this.ucInvoicePreview1.Clear();

                tbQuitCash.Text = (caCost + chCost + cdCost + dbCost + xxCost).ToString("F2");

                MessageBox.Show("退费成功!" + "\n" + returnCostString);

                //this.SaveCharge();

                #endregion
            }

            //打印负票 {EC3C448A-2E7C-4eff-9348-0AC37B40F438}

            #region 打印

            if (this.isPrintBill)
            {
                string invoicePrintDll = null;

                invoicePrintDll = controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.INVOICEPRINT, false, string.Empty);

                // 更改发票打印类获取方式；兼容原来方式
                // 2011-08-04
                // 此处不作提示
                //if (invoicePrintDll == null || invoicePrintDll == string.Empty)
                //{
                //    MessageBox.Show("没有设置发票打印参数，收费请维护!");

                //}
                //发票
                ArrayList alInvoices = new ArrayList();
                Balance tmpInvoice = quitInvoices[0] as Balance;
                tmpInvoice.Invoice.ID += "(退费)";
                tmpInvoice.FT.TotCost = -tmpInvoice.FT.TotCost;
                tmpInvoice.FT.OwnCost = -tmpInvoice.FT.OwnCost;
                tmpInvoice.FT.PayCost = -tmpInvoice.FT.PayCost;
                tmpInvoice.FT.PubCost = -tmpInvoice.FT.PubCost;
                tmpInvoice.PrintTime = outpatientManager.GetDateTimeFromSysDateTime();
                alInvoices.Add(tmpInvoice);

                //发票明细
                ArrayList alIDetails = new ArrayList();
                foreach (ArrayList alInvoiceDetail in alInvoiceDetails)
                {
                    int sort = 0;
                    foreach (BalanceList balList in alInvoiceDetail)
                    {
                        sort++;
                        balList.BalanceBase.FT.TotCost = balList.BalanceBase.FT.OwnCost + balList.BalanceBase.FT.PayCost + balList.BalanceBase.FT.PubCost;
                        balList.FeeCodeStat.SortID = sort;
                        //balList.BalanceBase.FT.TotCost = -balList.BalanceBase.FT.TotCost;
                        //balList.BalanceBase.FT.OwnCost = -balList.BalanceBase.FT.OwnCost;
                        //balList.BalanceBase.FT.PayCost = -balList.BalanceBase.FT.PayCost;
                        //balList.BalanceBase.FT.PubCost = -balList.BalanceBase.FT.PubCost;
                    }
                }
                alIDetails.Add(alInvoiceDetails);

                //患者信息
                this.patient.SIMainInfo.TotCost = -this.patient.SIMainInfo.TotCost;
                this.patient.SIMainInfo.OwnCost = -this.patient.SIMainInfo.OwnCost;
                this.patient.SIMainInfo.PayCost = -this.patient.SIMainInfo.PayCost;
                this.patient.SIMainInfo.PubCost = -this.patient.SIMainInfo.PubCost;
                this.patient.SIMainInfo.OfficalCost = -this.patient.SIMainInfo.OfficalCost;
                this.patient.SIMainInfo.OverCost = -this.patient.SIMainInfo.OverCost;

                string errText = "";

                this.feeIntegrate.PrintInvoice(invoicePrintDll, this.patient, alInvoices, alIDetails, alFeeDetail, payList, false, ref errText);

            }

            #endregion

            // 退费成功
            // {2E5139C9-52D8-4fec-A96B-09BECFDDFBD1}
            //if (this.trvInvoice.SelectedNode != null)
            //{
            //    if(string.IsNullOrEmpty(txtCardNO.Text.Trim()))
            //    {
            //        trvInvoice.Nodes.Clear();
            //    }
            //    else
            //    {
            //        txtCardNO_KeyDown(null, new KeyEventArgs(Keys.Enter));
            //    }
            //}

            #region atm判断

            if (IsAtm)
            {
                FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();
                FS.HISFC.Models.Account.AccountRecord CancleRecord = accountMgr.GetAccountRecord(patient.PID.CardNO, CancleInvoiceNo);
                FS.HISFC.Models.Account.AccountRecord FeeRecord = accountMgr.GetAccountRecord(patient.PID.CardNO, FeeInvoiceNo);
                if (FeeRecord == null)
                {
                    decimal vacancy = 0;
                    accountMgr.GetVacancy(patient.PID.CardNO, ref vacancy);
                    CancleRecord.BaseVacancy = vacancy;
                }
                CancleInvoiceNo = string.Empty;
                FeeInvoiceNo = string.Empty;
                IPrintCancleFee Iprint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(IPrintCancleFee)) as IPrintCancleFee;
                if (Iprint != null)
                {
                    Iprint.SetValue(CancleRecord, FeeRecord);
                    Iprint.Print();
                }
                else
                {
                    MessageBox.Show("请维护打印票据，查找打印票据失败！");

                }
            }
            blnShowInvoiceNoFind = false;
            this.ucInvoiceView.RefurbishInvoice();
            blnShowInvoiceNoFind = true;

            #endregion


            return 1;
        }

        #region 打印门诊指引单
        /// <summary>
        /// 打印门诊指引单
        /// </summary>
        /// <param name="rInfo"></param>
        /// <param name="invoices"></param>
        /// <param name="feeDetails"></param>
        private void PrintGuide(Register rInfo, ArrayList invoices, ArrayList feeDetails)
        {
            IOutpatientGuide print = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(IOutpatientGuide)) as IOutpatientGuide;
            if (print != null)
            {
                print.SetValue(rInfo, invoices, feeDetails);
                print.Print();
            }
        }

        #endregion

        /// <summary>
        /// 显示计算器
        /// </summary>
        /// <returns></returns>
        protected virtual int DisplayCalc()
        {
            string tempValue = this.feeIntegrate.GetControlValue(FS.HISFC.BizProcess.Integrate.Const.CALCTYPE, "0");

            if (tempValue == "0")
            {
                System.Diagnostics.Process.Start("CALC.EXE");
            }
            else if (tempValue == "1")
            {
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(
                    new FS.HISFC.Components.Common.Controls.ucCalc());
            }
            else
            {
                System.Diagnostics.Process.Start("CALC.EXE");
            }

            return 1;
        }

        ///{283BD55B-83E0-4f35-86E8-152CF0C1DA85} 积分数量
        /// <summary>
        /// 收费按钮触发
        /// </summary>
        /// <param name="alPayModes">支付方式信息</param>
        /// <param name="invoices">发票信息（基本对应发票主表的信息，每个对象对应一个发票）</param>
        /// <param name="invoiceDetails">发票明细信息（对应本次结算的全部费用明细）</param>
        /// <param name="invoiceFeeItemDetails">发票费用明细信息（按发票分组后的费用明细，每个对象对应该发票下的费用明细）</param>
        void frmBalance_FeeButtonClicked(ArrayList balancePays, ArrayList invoices, ArrayList invoiceDetails, ArrayList invoiceFeeDetails)
        {
            string errText = string.Empty;

            this.feeIntegrate.IsNeedUpdateInvoiceNO = false;
            //============暂时先这样加吧 luzhp@nuesoft.com
            foreach (FS.HISFC.Models.Fee.Outpatient.Balance invoice in invoices)
            {
                invoice.CanceledInvoiceNO = InvoiceNoStr;
            }
            if (this.patient.Pact.PayKind.ID == "02"
                 || this.patient.Pact.PayKind.ID == "03"
                )
            {
                foreach (Balance myBalance in invoices)
                {
                    ArrayList myFeeItemListArray = new ArrayList();
                    for (int i = 0; i < invoiceFeeDetails.Count; i++)
                    {
                        ArrayList tempAarry = new ArrayList();
                        tempAarry = (ArrayList)invoiceFeeDetails[i];
                        for (int j = 0; j < tempAarry.Count; j++)
                        {

                            ArrayList tempAarry2 = new ArrayList();
                            tempAarry2 = (ArrayList)tempAarry[j];
                            for (int k = 0; k < tempAarry2.Count; k++)
                            {
                                FeeItemList myFeeItemList = new FeeItemList();
                                myFeeItemList = (FeeItemList)tempAarry2[k];
                                if (myBalance.Invoice.ID == myFeeItemList.Invoice.ID)
                                {
                                    myFeeItemListArray.Add(myFeeItemList);

                                }
                            }
                        }
                    }
                    #region 上传医保信息

                    this.patient.SIMainInfo.InvoiceNo = myBalance.Invoice.ID;
                    //设置合同单位

                    long returnMedcareValue = this.medcareInterfaceProxy.UploadFeeDetailsOutpatient(this.patient, ref myFeeItemListArray);
                    if (returnMedcareValue != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.medcareInterfaceProxy.Rollback();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("待遇接口上传明细失败") + this.medcareInterfaceProxy.ErrMsg);
                        return;
                    }
                    returnMedcareValue = this.medcareInterfaceProxy.GetRegInfoOutpatient(this.patient);
                    returnMedcareValue = this.medcareInterfaceProxy.BalanceOutpatient(this.patient, ref myFeeItemListArray);
                    if (returnMedcareValue != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.medcareInterfaceProxy.Rollback();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("待遇接口门诊结算失败") + this.medcareInterfaceProxy.ErrMsg);
                        return;
                    }

                    #endregion

                }
            }
            //==========
            bool bReturn = this.feeIntegrate.ClinicFee(FS.HISFC.Models.Base.ChargeTypes.Fee, this.blnIsAccountInvoice, this.patient, invoices, invoiceDetails, againFeeItemLists, invoiceFeeDetails, balancePays, ref errText);

            if (!bReturn)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                if (errText != string.Empty)
                {
                    MessageBox.Show(errText);
                }
                return;
            }

            if (InterfaceManager.GetIOrder() != null)
            {
                if (InterfaceManager.GetIOrder().SendFeeInfo(this.patient, oldFeeItemLists, false) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.medcareInterfaceProxy.Rollback();
                    MessageBox.Show(this, "退费失败，请向系统管理员报告错误信息：" + InterfaceManager.GetIOrder().Err, "提示>>", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (InterfaceManager.GetIOrder().SendFeeInfo(this.patient, againFeeItemLists, true) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.medcareInterfaceProxy.Rollback();
                    MessageBox.Show(this, "退费失败，请向系统管理员报告错误信息：" + InterfaceManager.GetIOrder().Err, "提示>>", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            //{333D2AD8-DC4A-4c30-A14E-D6815AC858F9}
            FS.HISFC.BizLogic.Manager.Constant constManager = new FS.HISFC.BizLogic.Manager.Constant();
            FS.FrameWork.Models.NeuObject cashCouponPayMode = constManager.GetConstant("XJLZFFS", "1");

            //quitOperateCouponAmount = 0.0m;
            //quitCostCouponAmount = 0.0m;

            decimal recostCouponAmount = 0.0m;
            decimal rechargeCouponAmount = 0.0m;

            foreach (BalancePay p in balancePays)
            {
                if (cashCouponPayMode.Name.Contains(p.PayType.ID.ToString()))
                {
                    rechargeCouponAmount += p.FT.TotCost;
                }

                if (p.PayType.ID == "CO")
                {
                    recostCouponAmount += p.FT.TotCost;
                }
            }

            //获得特殊显示类别
            /////GetSpDisplayValue(myCtrl, t);
            //第一个发票号
            string mainInvoiceNO = string.Empty;
            foreach (Balance balance in invoices)
            {
                if (balance.Memo == "5")
                {
                    mainInvoiceNO = balance.ID;

                    continue;
                }

                //自费患者不需要显示主发票,那么取第一个发票号作为主发票号
                if (mainInvoiceNO == string.Empty)
                {
                    mainInvoiceNO = balance.Invoice.ID;
                }
            }

            #region 处理积分数据

            if (IsCouponModuleInUse)
            {
                //{333D2AD8-DC4A-4c30-A14E-D6815AC858F9}
                string resultCode = "0";
                string errorMsg = "";
                int reqRtn = -1;

                //{C0A56225-DFF9-46f9-9776-4DEE25F6CBD0}
                #region 调整积分处理顺序

                //重新收费产生的积分
                if (rechargeCouponAmount != 0)
                {
                    reqRtn = FS.HISFC.BizProcess.Integrate.WSHelper.OperateCoupon(patient.PID.CardNO, patient.Name, "MZSF", mainInvoiceNO, rechargeCouponAmount, 0.0m, quitInvoiceNO, "", out resultCode, out errorMsg);
                    if (reqRtn < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.medcareInterfaceProxy.Rollback();
                        MessageBox.Show("【产生重新收费积分】重新收费积分出错：" + errorMsg);
                        return;
                    }
                }

                //重新收费消费的积分
                if (recostCouponAmount != 0)
                {
                    reqRtn = FS.HISFC.BizProcess.Integrate.WSHelper.CostCoupon(patient.PID.CardNO, patient.Name, patient.PID.CardNO, patient.Name, "MZSF", mainInvoiceNO, recostCouponAmount, 0.0m, out resultCode, out errorMsg);
                    if (reqRtn < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.medcareInterfaceProxy.Rollback();
                        MessageBox.Show("【1、扣除重新收费消费积分】扣除重新收费时的消费积分出错：" + errorMsg);

                        //回滚重新收费产生的积分
                        if (rechargeCouponAmount != 0)
                        {
                            reqRtn = FS.HISFC.BizProcess.Integrate.WSHelper.OperateCoupon(patient.PID.CardNO, patient.Name, "MZTF", mainInvoiceNO, -rechargeCouponAmount, 0.0m, out resultCode, out errorMsg);
                            if (reqRtn < 0)
                            {
                                MessageBox.Show("【1、扣除重新收费消费积分】回滚重新收费产生的积分出错，请联系信息科处理，错误详情:" + errorMsg);
                            }
                        }

                        return;
                    }
                }

                //退回原单消费的积分
                if (quitCostCouponAmount != 0)
                {
                    reqRtn = FS.HISFC.BizProcess.Integrate.WSHelper.CostCoupon(patient.PID.CardNO, patient.Name, patient.PID.CardNO, patient.Name, "MZTF", quitInvoiceNO, quitCostCouponAmount, 0.0m, out resultCode, out errorMsg);
                    if (reqRtn < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.medcareInterfaceProxy.Rollback();
                        MessageBox.Show("【2、退回原交易消费的积分】退回原交易消费的积分出错" + errorMsg);

                        //回滚重新收费产生的积分
                        if (rechargeCouponAmount != 0)
                        {
                            reqRtn = FS.HISFC.BizProcess.Integrate.WSHelper.OperateCoupon(patient.PID.CardNO, patient.Name, "MZTF", mainInvoiceNO, -rechargeCouponAmount, 0.0m, out resultCode, out errorMsg);
                            if (reqRtn < 0)
                            {
                                MessageBox.Show("【2、退回原交易消费的积分】回滚重新收费产生的积分出错，请联系信息科处理，错误详情:" + errorMsg);
                            }
                        }

                        //回滚重新收费消费的积分
                        if (recostCouponAmount != 0)
                        {
                            reqRtn = FS.HISFC.BizProcess.Integrate.WSHelper.CostCoupon(patient.PID.CardNO, patient.Name, patient.PID.CardNO, patient.Name, "MZTF", mainInvoiceNO, -recostCouponAmount, 0.0m, out resultCode, out errorMsg);
                            if (reqRtn < 0)
                            {
                                MessageBox.Show("【2、退回原交易消费的积分】回滚重新收费消费的积分出错，请联系信息科处理，错误详情:" + errorMsg);
                            }
                        }

                        return;
                    }
                }

                //扣除原单产生的积分
                //{416C9A04-3A94-4d41-8F0A-14B73622C65E}
                //退费时，无论是否有积分需要扣除，都需要调用，因为涉及到老带新，需要通过这部分信息去进行退积分
                //if (quitOperateCouponAmount != 0)

                //{c2d43b9d-eda4-4f87-92c1-f6fdb08d9d04}取多倍积分

                {
                    if (!string.IsNullOrEmpty(quitInvoiceNO)) //存在退费为0时无发票号
                    {
                        reqRtn = FS.HISFC.BizProcess.Integrate.WSHelper.OperateCoupon(patient.PID.CardNO, patient.Name, "MZTF", quitInvoiceNO, quitOperateCouponAmount, 0.0m, "", "1", out resultCode, out errorMsg);
                        if (reqRtn < 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            this.medcareInterfaceProxy.Rollback();
                            MessageBox.Show("【3、扣除原交易产生的积分】扣除原交易产生的积分出错：" + errorMsg);

                            //回滚重新收费产生的积分
                            if (rechargeCouponAmount != 0)
                            {
                                reqRtn = FS.HISFC.BizProcess.Integrate.WSHelper.OperateCoupon(patient.PID.CardNO, patient.Name, "MZTF", mainInvoiceNO, -rechargeCouponAmount, 0.0m, out resultCode, out errorMsg);
                                if (reqRtn < 0)
                                {
                                    MessageBox.Show("【3、扣除原交易产生的积分】回滚重新收费产生的积分出错，请联系信息科处理，错误详情:" + errorMsg);
                                }
                            }

                            //回滚重新收费消费的积分
                            if (recostCouponAmount != 0)
                            {
                                reqRtn = FS.HISFC.BizProcess.Integrate.WSHelper.CostCoupon(patient.PID.CardNO, patient.Name, patient.PID.CardNO, patient.Name, "MZTF", mainInvoiceNO, -recostCouponAmount, 0.0m, out resultCode, out errorMsg);
                                if (reqRtn < 0)
                                {
                                    MessageBox.Show("【3、扣除原交易产生的积分】回滚重新收费消费的积分出错，请联系信息科处理，错误详情:" + errorMsg);
                                }
                            }

                            //回滚原单消费的积分
                            if (quitCostCouponAmount != 0)
                            {
                                reqRtn = FS.HISFC.BizProcess.Integrate.WSHelper.CostCoupon(patient.PID.CardNO, patient.Name, patient.PID.CardNO, patient.Name, "MZSF", quitInvoiceNO, -quitCostCouponAmount, 0.0m, out resultCode, out errorMsg);

                                if (reqRtn < 0)
                                {
                                    MessageBox.Show("【3、扣除原交易产生的积分】回滚退回原交易消费的积分出错，请联系信息科处理，错误详情:" + errorMsg);
                                }
                            }

                            return;
                        }
                    }
                }

                #endregion

                #region 作废

                ////退回原单消费的积分
                //if (quitCostCouponAmount != 0)
                //{
                //    reqRtn = FS.HISFC.BizProcess.Integrate.WSHelper.CostCoupon(patient.PID.CardNO, patient.Name, patient.PID.CardNO, patient.Name, "MZTF", quitInvoiceNO, quitCostCouponAmount, 0.0m, out resultCode, out errorMsg);
                //    if (reqRtn < 0)
                //    {
                //        FS.FrameWork.Management.PublicTrans.RollBack();
                //        this.medcareInterfaceProxy.Rollback();
                //        MessageBox.Show("处理会员积分出错：" + errorMsg);
                //        return;
                //    }
                //}

                ////扣除原单产生的积分
                //if (quitOperateCouponAmount != 0)
                //{
                //    reqRtn = FS.HISFC.BizProcess.Integrate.WSHelper.OperateCoupon(patient.PID.CardNO, patient.Name, "MZTF", quitInvoiceNO, quitOperateCouponAmount, 0.0m, out resultCode, out errorMsg);
                //    if (reqRtn < 0)
                //    {
                //        FS.FrameWork.Management.PublicTrans.RollBack();
                //        this.medcareInterfaceProxy.Rollback();
                //        MessageBox.Show("处理会员积分出错:" + errorMsg);

                //        //回滚原单消费的积分
                //        if (quitCostCouponAmount != 0)
                //        {
                //            reqRtn = FS.HISFC.BizProcess.Integrate.WSHelper.CostCoupon(patient.PID.CardNO, patient.Name, patient.PID.CardNO, patient.Name, "MZSF", quitInvoiceNO, -quitCostCouponAmount, 0.0m, out resultCode, out errorMsg);

                //            if (reqRtn < 0)
                //            {
                //                MessageBox.Show("回滚会员积分出错，请联系信息科处理，错误详情:" + errorMsg);
                //            }
                //        }

                //        return;
                //    }
                //}

                ////本单消费的积分
                //if (recostCouponAmount != 0)
                //{
                //    reqRtn = FS.HISFC.BizProcess.Integrate.WSHelper.CostCoupon(patient.PID.CardNO, patient.Name, patient.PID.CardNO, patient.Name, "MZSF", mainInvoiceNO, recostCouponAmount, 0.0m, out resultCode, out errorMsg);
                //    if (reqRtn < 0)
                //    {
                //        FS.FrameWork.Management.PublicTrans.RollBack();
                //        this.medcareInterfaceProxy.Rollback();
                //        MessageBox.Show("处理会员积分出错：" + errorMsg);

                //        //回滚原单产生的积分
                //        if (quitOperateCouponAmount != 0)
                //        {
                //            reqRtn = FS.HISFC.BizProcess.Integrate.WSHelper.OperateCoupon(patient.PID.CardNO, patient.Name, "MZSF", quitInvoiceNO, -quitOperateCouponAmount, 0.0m, out resultCode, out errorMsg);
                //            if (reqRtn < 0)
                //            {
                //                MessageBox.Show("回滚原交易产生的积分出错，请联系信息科处理，错误详情:" + errorMsg);
                //            }
                //        }

                //        //回滚原单消费的积分
                //        if (quitCostCouponAmount != 0)
                //        {
                //            reqRtn = FS.HISFC.BizProcess.Integrate.WSHelper.CostCoupon(patient.PID.CardNO, patient.Name, patient.PID.CardNO, patient.Name, "MZSF", quitInvoiceNO, -quitCostCouponAmount, 0.0m, out resultCode, out errorMsg);

                //            if (reqRtn < 0)
                //            {
                //                MessageBox.Show("回滚原交易消费的积分出错，请联系信息科处理，错误详情:" + errorMsg);
                //            }
                //        }

                //        return;
                //    }
                //}

                ////本单产生的积分
                //if (rechargeCouponAmount != 0)
                //{
                //    reqRtn = FS.HISFC.BizProcess.Integrate.WSHelper.OperateCoupon(patient.PID.CardNO, patient.Name, "MZSF", mainInvoiceNO, rechargeCouponAmount, 0.0m, out resultCode, out errorMsg);
                //    if (reqRtn < 0)
                //    {
                //        FS.FrameWork.Management.PublicTrans.RollBack();
                //        this.medcareInterfaceProxy.Rollback();
                //        MessageBox.Show("处理会员积分出错：" + errorMsg);

                //        //回滚本单消费的积分
                //        if (recostCouponAmount != 0)
                //        {
                //            reqRtn = FS.HISFC.BizProcess.Integrate.WSHelper.CostCoupon(patient.PID.CardNO, patient.Name, patient.PID.CardNO, patient.Name, "MZTF", mainInvoiceNO, -recostCouponAmount, 0.0m, out resultCode, out errorMsg);
                //            if (reqRtn < 0)
                //            {
                //                MessageBox.Show("回滚新交易消费的积分出错，请联系信息科处理，错误详情:" + errorMsg);
                //            }
                //        }

                //        //回滚原单产生的积分
                //        if (quitOperateCouponAmount != 0)
                //        {
                //            reqRtn = FS.HISFC.BizProcess.Integrate.WSHelper.OperateCoupon(patient.PID.CardNO, patient.Name, "MZSF", quitInvoiceNO, -quitOperateCouponAmount, 0.0m, out resultCode, out errorMsg);
                //            if (reqRtn < 0)
                //            {
                //                MessageBox.Show("回滚原交易产生的积分出错，请联系信息科处理，错误详情:" + errorMsg);
                //            }
                //        }

                //        //回滚原单消费的积分
                //        if (quitCostCouponAmount != 0)
                //        {
                //            reqRtn = FS.HISFC.BizProcess.Integrate.WSHelper.CostCoupon(patient.PID.CardNO, patient.Name, patient.PID.CardNO, patient.Name, "MZSF", quitInvoiceNO, -quitCostCouponAmount, 0.0m, out resultCode, out errorMsg);

                //            if (reqRtn < 0)
                //            {
                //                MessageBox.Show("回滚原交易消费的积分出错，请联系信息科处理，错误详情:" + errorMsg);
                //            }
                //        }

                //        return;
                //    }
                //}

                #endregion
            }
            #endregion



            isSuccess = true;
            FS.FrameWork.Management.PublicTrans.Commit();

            this.medcareInterfaceProxy.Commit();

            #region 发票打印

            if (!this.blnIsAccountInvoice)
            {
                string invoicePrintDll = null;

                invoicePrintDll = controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.INVOICEPRINT, false, string.Empty);

                // 更改发票打印类获取方式；兼容原来方式
                // 2011-08-04
                // 此处不作提示
                //if (invoicePrintDll == null || invoicePrintDll == string.Empty)
                //{
                //    MessageBox.Show("没有设置发票打印参数，收费请维护!");

                //    //return false;
                //}

                this.feeIntegrate.PrintInvoice(invoicePrintDll, this.patient, invoices, invoiceDetails, againFeeItemLists, invoiceFeeDetails, balancePays, false, ref errText);
            }
            //保存收费的发票信息
            if (balancePays != null && balancePays.Count > 0)
            {
                FS.HISFC.Models.Fee.BalancePayBase bpb = balancePays[0] as FS.HISFC.Models.Fee.BalancePayBase;
                if (bpb != null)
                    this.FeeInvoiceNo = bpb.Invoice.ID;
            }

            #endregion

            #region 门诊指引单打印

            this.PrintGuide(this.patient, invoices, againFeeItemLists);

            #endregion

            decimal orgCost = 0;
            decimal newCost = 0;
            bool isHaveCard = false;
            decimal returnCost = 0;
            decimal accountOrgCost = 0m;

            decimal accountNewCost = 0m;
            decimal returnAccountCost = 0m;
            foreach (BalancePay p in modifiedBalancePays)
            {
                //if (p.PayType.ID == "RC")
                //{
                //    orgCost += p.FT.OwnCost;
                //}


                //因为此时的支付方式为负
                if (p.PayType.ID.ToString() == "CA")
                {
                    orgCost += -p.FT.RealCost;
                }
                if (p.PayType.ID.ToString() != "CA")
                {
                    isHaveCard = true;
                }
                if (p.PayType.ID.ToString() == "YS")
                {
                    accountOrgCost += -p.FT.RealCost;
                }
            }
            foreach (BalancePay p in balancePays)
            {
                //因为此时的支付方式为负
                if (p.PayType.ID.ToString() == "CA")
                {
                    newCost += p.FT.RealCost;
                }
                if (p.PayType.ID.ToString() == "YS")
                {
                    accountNewCost += p.FT.RealCost;
                }
            }
            returnCost = orgCost - newCost;
            returnAccountCost = accountOrgCost - accountNewCost;
            returnCost = Class.Function.DealCent(returnCost);
            string messageText = string.Empty;

            if (returnCost == 0)
            {
                if (returnAccountCost >= 0)
                {
                    messageText = "账户退费" + returnAccountCost.ToString();
                }
                else
                {
                    messageText = "账户收取" + (-returnAccountCost).ToString();
                }
            }
            else if (returnCost > 0)
            {
                messageText = "应退金额: " + returnCost.ToString();
            }
            else
            {
                messageText = "应收现金: " + (-returnCost).ToString();
            }
            MessageBox.Show(messageText);
            tbQuitCash.Text = returnCost.ToString();

            this.Clear();
        }

        #region 无用
        /// <summary>
        /// 收费按钮触发
        /// </summary>
        /// <param name="alPayModes">支付方式信息</param>
        /// <param name="invoices">发票信息（基本对应发票主表的信息，每个对象对应一个发票）</param>
        /// <param name="invoiceDetails">发票明细信息（对应本次结算的全部费用明细）</param>
        /// <param name="invoiceFeeItemDetails">发票费用明细信息（按发票分组后的费用明细，每个对象对应该发票下的费用明细）</param>
        void frmBalance_FeeButtonClicked1(ArrayList balancePays, ArrayList invoices, ArrayList invoiceDetails, ArrayList invoiceFeeDetails)
        {
            string errText = string.Empty;

            this.feeIntegrate.IsNeedUpdateInvoiceNO = false;
            //============暂时先这样加吧 luzhp@nuesoft.com
            foreach (FS.HISFC.Models.Fee.Outpatient.Balance invoice in invoices)
            {
                invoice.CanceledInvoiceNO = InvoiceNoStr;
            }
            if (this.patient.Pact.PayKind.ID == "02"
                 || this.patient.Pact.PayKind.ID == "03"
                )
            {
                foreach (Balance myBalance in invoices)
                {
                    ArrayList myFeeItemListArray = new ArrayList();
                    for (int i = 0; i < invoiceFeeDetails.Count; i++)
                    {
                        ArrayList tempAarry = new ArrayList();
                        tempAarry = (ArrayList)invoiceFeeDetails[i];
                        for (int j = 0; j < tempAarry.Count; j++)
                        {

                            ArrayList tempAarry2 = new ArrayList();
                            tempAarry2 = (ArrayList)tempAarry[j];
                            for (int k = 0; k < tempAarry2.Count; k++)
                            {
                                FeeItemList myFeeItemList = new FeeItemList();
                                myFeeItemList = (FeeItemList)tempAarry2[k];
                                if (myBalance.Invoice.ID == myFeeItemList.Invoice.ID)
                                {
                                    myFeeItemListArray.Add(myFeeItemList);

                                }
                            }
                        }
                    }
                    #region 上传医保信息

                    this.patient.SIMainInfo.InvoiceNo = myBalance.Invoice.ID;
                    //设置合同单位

                    long returnMedcareValue = this.medcareInterfaceProxy.UploadFeeDetailsOutpatient(this.patient, ref myFeeItemListArray);
                    if (returnMedcareValue != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.medcareInterfaceProxy.Rollback();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("待遇接口上传明细失败") + this.medcareInterfaceProxy.ErrMsg);
                        return;
                    }
                    returnMedcareValue = this.medcareInterfaceProxy.GetRegInfoOutpatient(this.patient);
                    returnMedcareValue = this.medcareInterfaceProxy.BalanceOutpatient(this.patient, ref myFeeItemListArray);
                    if (returnMedcareValue != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.medcareInterfaceProxy.Rollback();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("待遇接口门诊结算失败") + this.medcareInterfaceProxy.ErrMsg);
                        return;
                    }

                    #endregion

                }
            }
            //==========
            bool bReturn = this.feeIntegrate.ClinicFee(FS.HISFC.Models.Base.ChargeTypes.Fee, this.patient, invoices, invoiceDetails, againFeeItemLists, invoiceFeeDetails, balancePays, ref errText);

            if (!bReturn)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                if (errText != string.Empty)
                {
                    MessageBox.Show(errText);
                }
                return;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            this.medcareInterfaceProxy.Commit();
            #region//发票打印

            string invoicePrintDll = null;

            invoicePrintDll = controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.INVOICEPRINT, false, string.Empty);

            // 更改发票打印类获取方式；兼容原来方式
            // 2011-08-04
            // 此处不作提示
            //if (invoicePrintDll == null || invoicePrintDll == string.Empty)
            //{
            //    MessageBox.Show("没有设置发票打印参数，收费请维护!");

            //    //return false;
            //}

            //iReturn = PrintInvoice(invoicePrintDll, r, invoices, invoiceDetails, feeDetails, invoiceFeeDetails, payModes, false, ref errText);
            // this.feeIntegrate.PrintInvoice(invoicePrintDll, this.registerControl.PatientInfo, invoicesClinicFee, invoiceDetailsClinicFee, myFeeItemListArray, invoiceFeeDetailsClinicFee, null, false, ref errText);
            this.feeIntegrate.PrintInvoice(invoicePrintDll, this.patient, invoices, invoiceDetails, againFeeItemLists, invoiceFeeDetails, balancePays, false, ref errText);
            //if (iReturn == -1)
            //{
            //    return false;
            //}

            #endregion
            decimal orgCost = 0;
            decimal newCost = 0;
            bool isHaveCard = false;
            decimal returnCost = 0;
            foreach (BalancePay p in modifiedBalancePays)
            {
                //因为此时的支付方式为负
                if (p.PayType.ID.ToString() == "CA")
                {
                    orgCost += -p.FT.RealCost;
                }
                if (p.PayType.ID.ToString() != "CA")
                {
                    isHaveCard = true;
                }
            }
            foreach (BalancePay p in balancePays)
            {
                //因为此时的支付方式为负
                if (p.PayType.ID.ToString() == "CA")
                {
                    newCost += p.FT.RealCost;
                }
            }
            returnCost = orgCost - newCost;
            returnCost = Class.Function.DealCent(returnCost);

            if (returnCost >= 0)
            {
                MessageBox.Show("应退金额: " + returnCost.ToString());
            }
            else
            {
                MessageBox.Show("应收现金: " + (-returnCost).ToString());
            }
            tbQuitCash.Text = returnCost.ToString();

            this.Clear();
        }
        #endregion

        /// <summary>
        /// 计算金额
        /// </summary>
        protected virtual void ComputCost()
        {
            decimal realQuitCost = 0;

            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                if (this.fpSpread1_Sheet1.Rows[i].Tag != null)
                {
                    if (this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                    {
                        FeeItemList f = this.fpSpread1_Sheet1.Rows[i].Tag as FeeItemList;

                        realQuitCost += f.FT.TotCost;

                        //    realQuitCost += FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2) - 
                        //        (FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2) / f.FT.TotCost) * f.FT.RebateCost;
                    }
                }
            }
            for (int i = 0; i < this.fpSpread1_Sheet2.RowCount; i++)
            {
                if (this.fpSpread1_Sheet2.Rows[i].Tag != null)
                {
                    if (this.fpSpread1_Sheet2.Rows[i].Tag is FeeItemList)
                    {
                        FeeItemList f = this.fpSpread1_Sheet2.Rows[i].Tag as FeeItemList;

                        realQuitCost += f.FT.TotCost;

                        //realQuitCost += FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2) - 
                        //    (FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2) / f.FT.TotCost) * f.FT.RebateCost;
                    }
                }
            }



            //			for(int i = 0; i < this.fpSpread2_Sheet1.RowCount; i ++)
            //			{
            //				if(this.fpSpread2_Sheet1.Rows[i].Tag != null)
            //				{
            //					if(this.fpSpread2_Sheet1.Rows[i].Tag is FeeItemList)
            //					{
            //						FeeItemList f = this.fpSpread2_Sheet1.Rows[i].Tag as FeeItemList;
            //
            //						realQuitCost += FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2);
            //					}
            //					if(this.fpSpread2_Sheet1.Rows[i].Tag is FS.HISFC.Models.Fee.ReturnApply)
            //					{
            //						FS.HISFC.Models.Fee.ReturnApply f = this.fpSpread2_Sheet1.Rows[i].Tag as FS.HISFC.Models.Fee.ReturnApply;
            //						
            //						realQuitCost += FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2);
            //					}
            //				}
            //				
            //			}
            //			for(int i = 0; i < this.fpSpread2_Sheet2.RowCount; i ++)
            //			{
            //				if(this.fpSpread2_Sheet2.Rows[i].Tag != null)
            //				{
            //					if(this.fpSpread2_Sheet2.Rows[i].Tag is FeeItemList)
            //					{
            //						FeeItemList f = this.fpSpread2_Sheet2.Rows[i].Tag as FeeItemList;
            //
            //						realQuitCost += FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2);
            //					}
            //					if(this.fpSpread2_Sheet2.Rows[i].Tag is FS.HISFC.Models.Fee.ReturnApply)
            //					{
            //						FS.HISFC.Models.Fee.ReturnApply f = this.fpSpread2_Sheet2.Rows[i].Tag as FS.HISFC.Models.Fee.ReturnApply;
            //						
            //						realQuitCost += FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2);
            //					}
            //				}
            //			}
            decimal totCost = 0;
            totCost = FS.FrameWork.Function.NConvert.ToDecimal(tbTotCost.Text);
            this.tbQuitCost.Text = (totCost - realQuitCost).ToString();
            this.tbReturnCost.Text = realQuitCost.ToString();
        }

        /// <summary>
        /// 清空
        /// </summary>
        protected virtual void Clear()
        {
            //如果是退药确认时，不刷新收费相关控件
            if (!this.isQuitDrugConfirm)
            {
                this.ucDisplay1.Clear();
                this.ucCostDisplay1.Clear();
                this.ucInvoicePreview1.Clear();
            }
            this.quitInvoices = null;

            this.tbInvoiceNO.Text = string.Empty;
            this.tbCardNo.Text = string.Empty;
            this.tbName.Text = string.Empty;
            this.tbPactName.Text = string.Empty;
            this.tbQuitCost.Text = string.Empty;
            tbTotCost.Text = string.Empty;
            tbOwnCost.Text = string.Empty;
            tbPayCost.Text = string.Empty;
            tbPubCost.Text = string.Empty;
            tbReturnCost.Text = string.Empty;
            this.txtReturnItemName.Text = string.Empty;
            this.txtRetSpecs.Text = string.Empty;
            this.txtReturnNum.Text = string.Empty;
            this.txtUnit.Text = string.Empty;
            this.fpSpread1_Sheet1.RowCount = 0;
            this.fpSpread1_Sheet1.RowCount = 5;
            this.fpSpread1_Sheet2.RowCount = 0;
            this.fpSpread1_Sheet2.RowCount = 5;
            this.fpSpread2_Sheet1.RowCount = 0;
            this.fpSpread2_Sheet1.RowCount = 5;
            this.fpSpread2_Sheet2.RowCount = 0;
            this.fpSpread2_Sheet2.RowCount = 5;
            this.Focus();
            this.tbInvoiceNO.Focus();
            this.cmbDoct.Tag = string.Empty;
            this.cmbRegDept.Tag = string.Empty;
            this.isAccount = false;
        }

        /// <summary>
        /// 保存划价信息
        /// </summary>
        protected virtual void SaveCharge()
        {
            DialogResult result;

            result = MessageBox.Show("是否确定要划价？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, System.Windows.Forms.MessageBoxDefaultButton.Button2);

            if (result == DialogResult.No)
            {
                return;
            }
            if (this.quitInvoices != null && this.quitInvoices.Count > 0)
            {
                if (hsInvoice.Contains(quitInvoices[0]))
                {
                    DialogResult r = MessageBox.Show("该发票费用信息已经划价保存过,是否重新划价?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (r == DialogResult.Cancel)
                    {
                        return;
                    }
                    hsInvoice.Remove(quitInvoices[0]);
                }
            }

            if (this.invoiceFeeItemLists == null || this.invoiceFeeItemLists.Count <= 0)
            {
                MessageBox.Show("没有划价信息！");
                return;
            }
            ArrayList alTemp = new ArrayList();
            if (!isSaveChargeRoundFee || !isSaveChargeDiagFee)
            {
                foreach (FeeItemList f in invoiceFeeItemLists)
                {
                    if (!isSaveChargeRoundFee && f.Item.ID.Equals(roundFeeItemCode))
                    {
                        continue;
                    }
                    if (!isSaveChargeDiagFee && f.Item.ID.Equals(ownDiagFeeCode))
                    {
                        continue;
                    }
                    alTemp.Add(f.Clone());
                }
            }
            else
            {
                foreach (FeeItemList f in invoiceFeeItemLists)
                {
                    alTemp.Add(f.Clone());
                }
            }

            //System.Collections.Hashtable hsCombNos = new Hashtable();
            //int combNo = 100;

            Dictionary<string, string> dicMoOrder = new Dictionary<string, string>();
            foreach (FeeItemList item in alTemp)
            {
                //if (item.UndrugComb.ID != null && item.UndrugComb.ID.Length > 0)
                //{
                //    if (hsCombNos.ContainsKey(item.UndrugComb.ID))
                //    {
                //        item.Order.Combo.ID = hsCombNos[item.UndrugComb.ID].ToString();
                //    }
                //    else
                //    {
                //        hsCombNos.Add(item.UndrugComb.ID, combNo.ToString());
                //        combNo++;
                //    }
                //}
                item.FTSource = "0";//划价保存，费用来源为操作员 

                item.FT.TotCost = item.FT.PayCost + item.FT.OwnCost + item.FT.PubCost;

                item.FT.PayCost = 0m;
                item.FT.PubCost = 0m;
                item.FT.OwnCost = item.FT.TotCost;
                item.PayType = FS.HISFC.Models.Base.PayTypes.Charged;
                item.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                item.RecipeSequence = string.Empty;

                //item.RecipeNO = string.Empty;
                //item.SequenceNO = -1;
                //item.Order.ID = string.Empty;

                item.Invoice.ID = string.Empty;
                item.InvoiceCombNO = null;

                if (dicMoOrder.ContainsKey(item.Order.Combo.ID))
                {
                    item.Order.ID = dicMoOrder[item.Order.Combo.ID];
                }
                else
                {
                    item.Order.ID = orderIntegrate.GetNewOrderID();
                    if (item.Order.ID == null || item.Order.ID == string.Empty)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("获得医嘱流水号出错！\r\n" + orderIntegrate.Err);
                        return;
                    }
                    dicMoOrder.Add(item.Order.Combo.ID, item.Order.ID);
                }

                item.ConfirmedQty = 0;
                item.IsConfirmed = false;
                item.PayType = FS.HISFC.Models.Base.PayTypes.Charged;
                item.NoBackQty = item.Item.Qty;
                item.ConfirmedInjectCount = 0;
                //item.ConfirmOper = new FS.HISFC.Models.Base.OperEnvironment();

                item.ChargeOper.ID = this.outpatientManager.Operator.ID;

                item.FeeOper.OperTime = System.DateTime.MinValue;

                if (item.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug)
                {
                    bool flag = outpatientManager.IsHaveTechApplyNo(item.RecipeNO, item.SequenceNO);
                    if (flag)
                    {
                        item.Item.IsNeedConfirm = true;
                        item.Item.NeedConfirm = FS.HISFC.Models.Fee.Item.EnumNeedConfirm.Outpatient;
                    }
                }
                else
                {
                    item.IsConfirmed = false;
                }

                item.Item.SpecialFlag2 = FS.FrameWork.Function.NConvert.ToInt32(item.IsConfirmed).ToString();
            }

            bool iReturn = false;
            DateTime dtNow = outpatientManager.GetDateTimeFromSysDateTime();
            string errText = string.Empty;
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            iReturn = this.feeIntegrate.SetChargeInfo(this.patient, alTemp, dtNow, ref errText);

            if (iReturn == false)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("划价出错" + errText);
                return;
            }
            else
            {
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("划价成功！");
                if (this.quitInvoices != null && this.quitInvoices.Count > 0)
                {
                    Balance invo = this.quitInvoices[0] as Balance;

                    hsInvoice.Add(invo, null);
                }
            }
        }

        #endregion

        #region 事件

        /// <summary>
        /// 设置toolBar按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("清屏", "清除录入的信息", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
            toolBarService.AddToolButton("退费", "确认退费信息", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B保存, true, false, null);
            toolBarService.AddToolButton("取消", "取消已经选择的退费信息", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q取消, true, false, null);
            toolBarService.AddToolButton("全选", "选中全部费用", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q全退, true, false, null);
            toolBarService.AddToolButton("计算器", "打开计算器", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B病历, true, false, null);
            toolBarService.AddToolButton("帮助", "打开帮助文件", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B帮助, true, false, null);
            //{F28E9BBB-D37E-4d8b-B25A-24F834290FBC}增加划价保存功能
            toolBarService.AddToolButton("划价保存", "把当前退费项目划价", (int)FS.FrameWork.WinForms.Classes.EnumImageList.H划价保存, true, false, null);
            //{F28E9BBB-D37E-4d8b-B25A-24F834290FBC}完毕
            toolBarService.AddToolButton("刷卡", "刷卡", FS.FrameWork.WinForms.Classes.EnumImageList.B报警, true, false, null);
            return this.toolBarService;
        }

        /// <summary>
        /// 按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "退费":
                    string tempinvoiceNO = tbInvoiceNO.Text.Trim();
                    if (!checkisAtm(tempinvoiceNO))
                    {
                        return;
                    }
                    //if (isATM && Function.CheckAtmFee(tempinvoiceNO))
                    //{
                    //    isAccount = true;
                    //}

                    if (isShowSaveChargeHits)
                    {
                        if (this.quitInvoices != null && this.quitInvoices.Count > 0)
                        {
                            if (!hsInvoice.Contains(quitInvoices[0]))
                            {
                                DialogResult r = MessageBox.Show("是否需要划价保存该发票费用信息?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                                if (r == DialogResult.OK)
                                {
                                    this.SaveCharge();
                                }
                            }
                        }
                    }
                    if (patient != null && isAccount)
                    {
                        if (SaveAccountQuiteFee() == 1)
                        {
                            this.Clear();
                        }
                    }
                    else
                    {
                        if (this.Save() == 1)
                        {
                            this.Clear();
                        }
                    }
                    break;
                case "清屏":
                    this.Clear();
                    break;
                case "取消":
                    this.CancelQuitOperation();
                    break;

                case "计算器":
                    this.DisplayCalc();
                    break;

                case "全选":
                    this.AllQuit();
                    break;

                case "划价保存":
                    this.SaveCharge();
                    break;
                case "刷卡":
                    {
                        string cardNo = "";
                        string error = "";
                        //{119F302E-69D9-445c-BF56-4109D975AD98}
                        if (Function.OperMCard(ref cardNo, ref error) == -1)
                        {
                            MessageBox.Show(error, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        this.ucInvoiceView.MCardNo = ";" + cardNo;
                        break;
                    }
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        /// <summary>
        /// 按键
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.F5)
            {
                //				this.panel1.Focus();
                //				this.panel2.Focus();
                //				this.fpSpread1.Focus();
                //				this.fpSpread1.ActiveSheet = this.fpSpread1_Sheet1;
                //				if(this.fpSpread1_Sheet1.RowCount > 0)
                //				{
                //					this.fpSpread1_Sheet1.ActiveRowIndex = 0;
                //				}
                this.SaveCharge();
            }
            if (keyData == Keys.F6)
            {
                //				this.panel1.Focus();
                //				this.panel2.Focus();
                //				this.fpSpread1.Focus();
                //				this.fpSpread1.ActiveSheet = this.fpSpread1_Sheet2;
                //				if(this.fpSpread1_Sheet2.RowCount > 0)
                //				{
                //					this.fpSpread1_Sheet2.ActiveRowIndex = 0;
                //				}
                if (this.fpSpread1.Focused)
                {
                    if (this.fpSpread1.ActiveSheet == this.fpSpread1_Sheet1)
                    {
                        this.fpSpread2.Focus();
                        if (this.fpSpread2_Sheet1.RowCount > 0)
                        {
                            this.fpSpread2_Sheet1.ActiveRowIndex = 0;
                        }
                    }
                    if (this.fpSpread1.ActiveSheet == this.fpSpread1_Sheet2)
                    {
                        this.fpSpread2.Focus();
                        if (this.fpSpread2_Sheet2.RowCount > 0)
                        {
                            this.fpSpread2_Sheet2.ActiveRowIndex = 0;
                        }
                    }
                }
                else
                {
                    if (this.fpSpread2.ActiveSheet == this.fpSpread2_Sheet1)
                    {
                        this.fpSpread1.Focus();
                        if (this.fpSpread1_Sheet1.RowCount > 0)
                        {
                            this.fpSpread1_Sheet1.ActiveRowIndex = 0;
                        }
                    }
                    if (this.fpSpread2.ActiveSheet == this.fpSpread2_Sheet2)
                    {
                        this.fpSpread1.Focus();
                        if (this.fpSpread1_Sheet2.RowCount > 0)
                        {
                            this.fpSpread1_Sheet2.ActiveRowIndex = 0;
                        }
                    }
                }
            }
            if (keyData == Keys.F7)
            {
                this.panel1.Focus();
                this.fpSpread2.Focus();
                this.fpSpread2.ActiveSheet = this.fpSpread2_Sheet1;
                if (this.fpSpread2_Sheet1.RowCount > 0)
                {
                    this.fpSpread2_Sheet1.ActiveRowIndex = 0;
                }
            }
            if (keyData == Keys.F8)
            {
                this.panel1.Focus();
                this.fpSpread2.Focus();
                this.fpSpread2.ActiveSheet = this.fpSpread2_Sheet2;
                if (this.fpSpread2_Sheet2.RowCount > 0)
                {
                    this.fpSpread2_Sheet2.ActiveRowIndex = 0;
                }
            }
            if (keyData == Keys.F11)
            {
                this.ckbAllQuit.Checked = !this.ckbAllQuit.Checked;
                if (this.ckbAllQuit.Checked)
                {
                    this.txtReturnNum.Enabled = false;
                }
                else
                {
                    this.txtReturnNum.Enabled = true;
                }
            }
            if (keyData == Keys.F2)
            {
                this.neuTabControl1.SelectedTab = this.tpQuit;
                this.tpQuit.Focus();
                this.tbInvoiceNO.Select();
                this.tbInvoiceNO.Focus();
            }
            if (keyData == Keys.F3)
            {
                this.neuTabControl1.SelectedTab = this.tpFee;
                this.tpFee.Focus();
                this.ucDisplay1.Select();
                this.ucDisplay1.Focus();
            }
            if (keyData == Keys.F4)
            {
                this.AllQuit();
            }
            if (keyData == Keys.F9)
            {
                this.Clear();
            }
            if (keyData == Keys.F12)
            {
                this.FindForm().Close();
            }
            if (keyData == Keys.Escape)
            {
                this.FindForm().Close();
            }
            if (keyData == Keys.F1)
            {
                //FS.FrameWork.WinForms.Classes.Function.PopShowControl(new FS.Common.Controls.ucCalc());
            }

            return base.ProcessDialogKey(keyData);
        }

        protected bool checkisAtm(string invoiceno)
        {
            if (invoiceno.StartsWith("T"))
            {
                MessageBox.Show("请先去打印正式发票后退费。");
                this.tbInvoiceNO.Focus();
                return false;
            }
            if ((FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee).CurrentGroup.Name.Contains("药房"))
                return true;

            if (isATM)
            {
                if (Function.CheckAtmFee(invoiceno))
                {

                    return true;
                }
                else
                {
                    MessageBox.Show("这个非自助机发票，请去普通收费窗口办理!");
                    this.tbInvoiceNO.Focus();
                    return false;
                }
            }
            else
            {
                if (Function.CheckAtmFee(invoiceno))
                {
                    MessageBox.Show("这个是自助机发票，请去专窗办理!");
                    this.tbInvoiceNO.Focus();
                    return false;

                }
            }
            return true;
        }


        protected virtual void tbInvoiceNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string invoiceNO = this.tbInvoiceNO.Text;

                this.Clear();
                this.tbQuitCash.Text = string.Empty;

                if (invoiceNO == string.Empty)
                {
                    MessageBox.Show("请输入发票号!");
                    this.tbInvoiceNO.Focus();
                    return;
                }
                this.tbInvoiceNO.Text = invoiceNO;
                this.quitInvoices = QueryInvoices(invoiceNO);
                if (quitInvoices == null)
                {
                    return;
                }
                if (quitInvoices.Count == 0)
                {
                    return;
                }

                if (!checkisAtm(invoiceNO))
                {
                    return;
                }



                if (quitInvoices.Count > 1)
                {
                    string invoiceNoTemp = string.Empty;
                    foreach (Balance invoice in quitInvoices)
                    {
                        invoiceNoTemp += invoice.Invoice.ID + "\n";
                    }
                    MessageBox.Show("此次退费有:" + invoiceNoTemp + "请全部收回!");
                }


                Balance invoiceTemp = quitInvoices[0] as Balance;


                if (this.IsJudgePrivWhileQuit)
                {
                    if (invoiceTemp.BalanceOper.ID != this.outpatientManager.Operator.ID)
                    {
                        //判断权限,是否有退其他挂号员操作的权限
                        if (!CommonController.CreateInstance().JugePrive(Function.PrivQuit, Function.PrivQuitOtherOperFee))
                        {
                            CommonController.CreateInstance().MessageBox("您没有退其他操作员收费记录的权限，操作已取消，该费用的操作员是：" + CommonController.CreateInstance().GetEmployeeName(invoiceTemp.BalanceOper.ID), MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }

                if (this.IsJudgePrivWhileQuit)
                {
                    if (invoiceTemp.BalanceOper.OperTime < this.outpatientManager.GetDateTimeFromSysDateTime().Date)
                    {
                        //隔日退费
                        if (!CommonController.CreateInstance().JugePrive(Function.PrivQuit, Function.PrivQuitLastDayFee))
                        {
                            CommonController.CreateInstance().MessageBox("您没有隔日退费的权限，操作已取消", MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }



                FS.HISFC.Models.Registration.Register tmpReg = registerIntegrate.GetByClinic(invoiceTemp.Patient.ID);
                //by han-zf 珠海医保放化疗取消结算，需要结算日期    
                tmpReg.SIMainInfo.BalanceDate = invoiceTemp.BalanceOper.OperTime;
                this.patient.SIMainInfo.BalanceDate = invoiceTemp.BalanceOper.OperTime;

                if (tmpReg == null)
                {
                    MessageBox.Show("获得挂号信息出错!" + this.registerIntegrate.Err);

                    this.tbInvoiceNO.Focus();

                    return;
                }

                this.ucDisplay1.PatientInfo = tmpReg.Clone();
                this.ucDisplay1.PatientInfo.Pact = this.managerIntegrate.GetPactUnitInfoByPactCode(tmpReg.Pact.ID);

                this.tbCardNo.Text = invoiceTemp.Patient.PID.CardNO;
                this.tbName.Text = invoiceTemp.Patient.Name;
                this.tbPactName.Text = invoiceTemp.Patient.Pact.Name;

                this.patient.PID.CardNO = invoiceTemp.Patient.PID.CardNO;
                this.patient.Name = invoiceTemp.Patient.Name;
                this.patient.Pact.PayKind.ID = invoiceTemp.Patient.Pact.PayKind.ID;
                this.patient.Pact.ID = invoiceTemp.Patient.Pact.ID;
                this.patient.Pact.Name = invoiceTemp.Patient.Pact.Name;
                this.patient.ID = invoiceTemp.Patient.ID;
                this.patient.DoctorInfo.SeeDate = ((FS.HISFC.Models.Registration.Register)invoiceTemp.Patient).DoctorInfo.SeeDate;
                this.patient.SSN = invoiceTemp.Patient.SSN;
                this.patient.ChkKind = invoiceTemp.ExamineFlag;
                this.patient.User03 = invoiceTemp.User03;//公费限额等级
                this.patient.LSH = invoiceTemp.User02;//公费记账单号
                this.oldPatient = this.patient.Clone();
                //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
                isAccount = false;
                //this.patient.IsAccount = false;
                decimal totCost = 0;
                decimal ownCost = 0;
                decimal payCost = 0;
                decimal pubCost = 0;
                foreach (Balance invoice in quitInvoices)
                {
                    totCost += invoice.FT.TotCost;
                    ownCost += invoice.FT.OwnCost;
                    payCost += invoice.FT.PayCost;
                    pubCost += invoice.FT.PubCost;
                }
                this.tbTotCost.Text = totCost.ToString();
                this.tbOwnCost.Text = ownCost.ToString();
                this.tbPayCost.Text = payCost.ToString();
                this.tbPubCost.Text = pubCost.ToString();

                if (this.GetItemList() == -1)
                {
                    return;
                }

                if (this.fpSpread1_Sheet1.RowCount > 0)
                {
                    this.fpSpread1.ActiveSheet = this.fpSpread1_Sheet1;
                    this.fpSpread1.Focus();
                    this.fpSpread1_Sheet1.ActiveRowIndex = 0;
                }
                else
                {
                    this.fpSpread1.Focus();
                    this.fpSpread1.ActiveSheet = this.fpSpread1_Sheet2;
                    this.fpSpread1_Sheet2.ActiveRowIndex = 0;
                }
                //默认全退操作
                // AllQuit();
            }
        }

        protected virtual void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //是否允许半退
            if (IsAllowQuitFeeHalf == false)
            {
                MessageBox.Show("没有半退权限");
                return;
            }


            //记录目前是否需要全退
            bool isNeedAllQuitStatus = this.isNeedAllQuit;
            bool isCheckAll = this.ckbAllQuit.Checked;

            try
            {
                //草药需要全退
                if (this.fpSpread1.ActiveSheetIndex == 0)
                {
                    FeeItemList drugItem = this.fpSpread1_Sheet1.Rows[e.Row].Tag as FeeItemList;
                    if (drugItem != null && drugItem.Item.ItemType == EnumItemType.Drug && drugItem.Item.SysClass.ID.Equals(FS.HISFC.Models.Base.EnumSysClass.PCC.ToString()))
                    {
                        this.isNeedAllQuit = true;
                        this.ckbAllQuit.Checked = true;
                    }
                }

                if (this.fpSpread1.ActiveSheet.RowCount > 0)
                {
                    //if (this.patient.Pact.PayKind.ID == "02")
                    //{
                    //    MessageBox.Show("医保患者必须点全退");
                    //    //this.Clear();
                    //    return;
                    //}
                    this.DealQuitOperation();
                }
            }
            finally
            {
                this.isNeedAllQuit = isNeedAllQuitStatus;
                this.ckbAllQuit.Checked = isCheckAll;
            }


        }

        protected virtual void fpSpread2_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //是否允许半退
            if (IsAllowQuitFeeHalf == false)
            {
                MessageBox.Show("没有半退权限");
                return;
            }

            //记录目前是否需要全退
            bool isNeedAllQuitStatus = this.isNeedAllQuit;


            try
            {
                //草药需要全退
                if (this.fpSpread2.ActiveSheetIndex == 0)
                {
                    FeeItemList drugItem = this.fpSpread2_Sheet1.Rows[e.Row].Tag as FeeItemList;
                    if (drugItem != null && drugItem.Item.ItemType == EnumItemType.Drug && drugItem.Item.SysClass.ID.Equals(FS.HISFC.Models.Base.EnumSysClass.PCC.ToString()))
                    {
                        this.isNeedAllQuit = true;
                    }
                }

                if (this.fpSpread2.ActiveSheet.RowCount > 0)
                {
                    this.DealCancelQuitOperation();
                }
            }
            finally
            {
                this.isNeedAllQuit = isNeedAllQuitStatus;
            }
        }

        protected virtual void ckbAllQuit_Click(object sender, System.EventArgs e)
        {
            if (this.ckbAllQuit.Checked)
            {
                this.txtReturnNum.Enabled = false;
            }
            else
            {
                this.txtReturnNum.Enabled = true;
            }
        }

        protected virtual void txtReturnNum_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                QuitItemByNum();
            }
        }

        protected virtual void fpSpread1_ActiveSheetChanged(object sender, System.EventArgs e)
        {
            if (this.fpSpread1.ActiveSheet == this.fpSpread1_Sheet1)
            {
                this.fpSpread2.ActiveSheet = this.fpSpread2_Sheet1;
            }
            else
            {
                if (this.fpSpread2.ActiveSheet != null)
                {
                    this.fpSpread2.ActiveSheet = this.fpSpread2_Sheet2;
                }
            }
        }

        protected virtual void fpSpread2_ActiveSheetChanged(object sender, System.EventArgs e)
        {
            if (this.fpSpread2.ActiveSheet == this.fpSpread2_Sheet1)
            {
                this.fpSpread1.ActiveSheet = this.fpSpread1_Sheet1;
            }
            else
            {
                this.fpSpread1.ActiveSheet = this.fpSpread1_Sheet2;
            }
        }

        protected virtual void frmQuitFee_Load(object sender, System.EventArgs e)
        {
            this.fpSpread1.ActiveSheet = this.fpSpread1_Sheet1;
            try
            {
                this.isNeedAllQuit = this.controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.GROUP_ITEM_ALLQUIT, false, false);
                this.chkGoupAllQuit.Checked = isNeedAllQuit;
                if (this.Init() < 0)
                {
                    return;
                }

                this.ParentForm.FormClosing += new FormClosingEventHandler(ParentForm_FormClosing);
            }
            catch
            {
            }
        }

        void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //{E027D856-6334-4410-8209-5E9E36E31B53} 项目列表多线程载入
                //关闭窗口之前,如果载入项目列表线程没有结束,强行结束,避免线程例外
                this.ucDisplay1.threadItemInit.Abort();
            }
            catch { }
        }

        protected virtual void fpSpread1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //是否允许半退
                if (IsAllowQuitFeeHalf == false)
                {
                    MessageBox.Show("没有半退权限");
                    return;
                }
                if (this.fpSpread1.ActiveSheet.RowCount > 0)
                {
                    //if (this.patient.Pact.PayKind.ID == "02")
                    //{
                    //    MessageBox.Show("医保患者必须点全退");
                    //    //this.Clear();
                    //    return;
                    //}
                    this.DealQuitOperation();
                }

            }
        }

        protected virtual void fpSpread2_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //是否允许半退
                if (IsAllowQuitFeeHalf == false)
                {
                    MessageBox.Show("没有半退权限");
                    return;
                }
                if (this.fpSpread2.ActiveSheet.RowCount > 0)
                {
                    this.DealCancelQuitOperation();
                }
            }
        }

        protected virtual void chkGoupAllQuit_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.chkGoupAllQuit.Checked)
            {
                this.isNeedAllQuit = true;
            }
            else
            {
                this.isNeedAllQuit = false;
            }
        }

        protected virtual void ckbAllQuit_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbAllQuit.Checked)
            {
                this.txtReturnNum.Enabled = false;
                this.txtReturnNum.Text = string.Empty;
                this.txtReturnNum.Tag = null;
            }
            else
            {
                this.txtReturnNum.Enabled = true;
            }
        }

        #endregion


        private void fpSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.RowHeader && this.fpSpread1_Sheet2.RowHeader.Cells[e.Row, 0].Text == "+" &&
                this.fpSpread1.ActiveSheet == this.fpSpread1_Sheet2)
            {
                ExpandOrCloseRow(false, e.Row + 1);
                return;
            }
            if (e.RowHeader && fpSpread1_Sheet2.RowHeader.Cells[e.Row, 0].Text == "-" &&
                this.fpSpread1.ActiveSheet == this.fpSpread1_Sheet2)
            {
                ExpandOrCloseRow(true, e.Row + 1);
                return;
            }
        }
        /// <summary>
        /// 折叠显示物资数据
        /// </summary>
        /// <param name="isExpand"></param>
        /// <param name="index"></param>
        private void ExpandOrCloseRow(bool isExpand, int index)
        {

            for (int i = index; i < fpSpread1_Sheet2.Rows.Count; i++)
            {
                if (this.fpSpread1_Sheet2.RowHeader.Cells[i, 0].Text == "." && this.fpSpread1_Sheet2.Rows[i].Visible == isExpand)
                {
                    this.fpSpread1_Sheet2.Rows[i].Visible = !isExpand;
                }
                else
                {
                    break;
                }
            }
            if (isExpand)
            {
                fpSpread1_Sheet2.RowHeader.Cells[index - 1, 0].Text = "+";
            }
            else
            {
                fpSpread1_Sheet2.RowHeader.Cells[index - 1, 0].Text = "-";
            }
        }

        /// <summary>
        /// 查找物资所对照的非药品所对应的行
        /// </summary>
        /// <param name="rowIndex">物资所在的行</param>
        /// <returns></returns>
        private int FinItemRowIndex(int rowIndex)
        {
            for (int i = rowIndex; i >= 0; i--)
            {
                if (this.fpSpread1_Sheet2.RowHeader.Cells[i, 0].Text != ".")
                    return i;
            }
            return -1;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.ucDisplay1.DeleteRow();
        }

        private void neuTabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            ArrayList addItemList = this.ucDisplay1.GetFeeItemList();
            if (addItemList == null)
            {
                return;
            }

            if (addItemList.Count > 0)
            {

                if (this.cmbRegDept.Tag == null || this.cmbRegDept.Tag.ToString() == string.Empty || this.cmbRegDept.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("请选择补收费用的看诊科室!");

                    return;
                }

                if (this.cmbDoct.Tag == null || this.cmbDoct.Tag.ToString() == string.Empty || this.cmbDoct.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("请选择补收费用的开立医生!");

                    return;
                }
            }

            ArrayList phList = new ArrayList(); //药品列表
            ArrayList itemList = new ArrayList();//非药品列表

            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in addItemList)
            {
                if (f.Item.ItemType == EnumItemType.Drug)
                {
                    phList.Add(f);
                }
                else
                {
                    itemList.Add(f);
                }
            }

            for (int i = this.fpSpread1_Sheet1.RowCount - 1; i >= 0; i--)
            {
                if (this.fpSpread1_Sheet1.RowHeader.Cells[i, 0].Text == "补收")
                {
                    this.fpSpread1_Sheet1.Rows.Remove(i, 1);
                }
            }
            for (int i = this.fpSpread1_Sheet2.RowCount - 1; i >= 0; i--)
            {
                if (this.fpSpread1_Sheet2.RowHeader.Cells[i, 0].Text == "补收")
                {
                    this.fpSpread1_Sheet2.Rows.Remove(i, 1);
                }
            }

            int phOrgCount = this.fpSpread1_Sheet1.RowCount;

            this.fpSpread1_Sheet1.RowCount += phList.Count; //药品.

            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList drugItem in phList)
            {
                //this.fpSpread1_Sheet1.Rows[phOrgCount].Tag = drugItem;


                this.fpSpread1_Sheet1.Cells[phOrgCount, (int)DrugList.ItemName].Text = drugItem.Item.Name;

                this.fpSpread1_Sheet1.Cells[phOrgCount, (int)DrugList.CombNo].Text = drugItem.Order.Combo.ID;

                this.fpSpread1_Sheet1.Cells[phOrgCount, (int)DrugList.Specs].Text = drugItem.Item.Specs;
                this.fpSpread1_Sheet1.Cells[phOrgCount, (int)DrugList.Amount].Text = drugItem.FeePack == "1" ?
                    FS.FrameWork.Public.String.FormatNumber(drugItem.Item.Qty / drugItem.Item.PackQty, 2).ToString() :
                    FS.FrameWork.Public.String.FormatNumber(drugItem.Item.Qty, 2).ToString();
                this.fpSpread1_Sheet1.Cells[phOrgCount, (int)DrugList.PriceUnit].Text = drugItem.Item.PriceUnit;
                this.fpSpread1_Sheet1.Cells[phOrgCount, (int)DrugList.NoBackQty].Text = drugItem.FeePack == "1" ?
                    FS.FrameWork.Public.String.FormatNumber(drugItem.NoBackQty / drugItem.Item.PackQty, 2).ToString() :
                    FS.FrameWork.Public.String.FormatNumber(drugItem.NoBackQty, 2).ToString();
                this.fpSpread1_Sheet1.Cells[phOrgCount, (int)DrugList.Cost].Text = (drugItem.FT.OwnCost + drugItem.FT.PayCost + drugItem.FT.PubCost).ToString();

                if (drugItem.Item.SysClass.ID.ToString() == "PCC")
                {
                    this.fpSpread1_Sheet1.Cells[phOrgCount, (int)DrugList.DoseAndDays].Text = "每次量:" + drugItem.Order.DoseOnce.ToString() + drugItem.Order.DoseUnit + " " + "付数:" + drugItem.Days.ToString();
                }
                else
                {
                    this.fpSpread1_Sheet1.Cells[phOrgCount, (int)DrugList.DoseAndDays].Text = "每次量:" + drugItem.Order.DoseOnce.ToString() + drugItem.Order.DoseUnit;
                }

                Class.Function.DrawCombo(this.fpSpread1_Sheet1, (int)DrugList.CombNo, (int)DrugList.Comb, 0);

                this.fpSpread1_Sheet1.RowHeader.Cells[phOrgCount, 0].Text = "补收";

                phOrgCount++;
            }

            int unDrugOrgCount = this.fpSpread1_Sheet2.RowCount;

            this.fpSpread1_Sheet2.RowCount += itemList.Count; //药品.

            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList undrugItem in itemList)
            {

                #region 加载物资信息
                //{143CA424-7AF9-493a-8601-2F7B1D635027}
                string outNo = undrugItem.UpdateSequence.ToString();
                List<HISFC.Models.FeeStuff.Output> list = mateIntegrate.QueryOutput(outNo);
                undrugItem.MateList = list;
                #endregion

                if (undrugItem.FT.RebateCost > 0)
                {
                    isHaveRebateCost = true;
                }

                undrugItem.FT.TotCost = undrugItem.FT.OwnCost + undrugItem.FT.PayCost + undrugItem.FT.PubCost;
                //this.fpSpread1_Sheet2.Rows[unDrugOrgCount].Tag = undrugItem;


                this.fpSpread1_Sheet2.Cells[unDrugOrgCount, (int)UndrugList.ItemName].Text = undrugItem.Item.Name;
                this.fpSpread1_Sheet2.Cells[unDrugOrgCount, (int)UndrugList.CombNo].Text = undrugItem.Order.Combo.ID;
                this.fpSpread1_Sheet2.Cells[unDrugOrgCount, (int)UndrugList.Amount].Text = undrugItem.FeePack == "1" ?
                    FS.FrameWork.Public.String.FormatNumber(undrugItem.Item.Qty / undrugItem.Item.PackQty, 2).ToString() :
                    FS.FrameWork.Public.String.FormatNumber(undrugItem.Item.Qty, 2).ToString();
                this.fpSpread1_Sheet2.Cells[unDrugOrgCount, (int)UndrugList.PriceUnit].Text = undrugItem.Item.PriceUnit;
                this.fpSpread1_Sheet2.Cells[unDrugOrgCount, (int)UndrugList.NoBackQty].Text = undrugItem.FeePack == "1" ?
                    FS.FrameWork.Public.String.FormatNumber(undrugItem.NoBackQty / undrugItem.Item.PackQty, 2).ToString() :
                    FS.FrameWork.Public.String.FormatNumber(undrugItem.NoBackQty, 2).ToString();
                this.fpSpread1_Sheet2.Cells[unDrugOrgCount, (int)UndrugList.Cost].Text = (undrugItem.FT.OwnCost + undrugItem.FT.PayCost + undrugItem.FT.PubCost).ToString();
                if (undrugItem.UndrugComb.ID != null && undrugItem.UndrugComb.ID.Length > 0)
                {
                    this.undrugComb = this.undrugManager.GetValidItemByUndrugCode(undrugItem.UndrugComb.ID);
                    if (this.undrugComb == null)
                    {
                        MessageBox.Show("获得组套信息出错，无法显示组套自定义码，但是不影响退费操作！");
                    }
                    else
                    {
                        undrugItem.UndrugComb.UserCode = this.undrugComb.UserCode;
                    }

                    FS.HISFC.Models.Fee.Item.Undrug item = this.undrugManager.GetValidItemByUndrugCode(undrugItem.ID);

                    if (item == null)
                    {
                        this.fpSpread1_Sheet2.Cells[unDrugOrgCount, (int)UndrugList.PackageName].Text = "(" + undrugItem.UndrugComb.UserCode + ")" + undrugItem.UndrugComb.Name;
                    }
                    else
                    {
                        this.fpSpread1_Sheet2.Cells[unDrugOrgCount, (int)UndrugList.PackageName].Text = "(" + undrugItem.UndrugComb.UserCode + ")" + undrugItem.UndrugComb.Name + "[" + item.UserCode + "]";
                    }

                }
                else
                {
                    FS.HISFC.Models.Fee.Item.Undrug item = this.undrugManager.GetValidItemByUndrugCode(undrugItem.ID);

                    if (item != null)
                    {
                        this.fpSpread1_Sheet2.Cells[unDrugOrgCount, (int)UndrugList.PackageName].Text = item.UserCode;
                    }
                }

                Class.Function.DrawCombo(this.fpSpread1_Sheet2, (int)UndrugList.CombNo, (int)UndrugList.Comb, 0);
                //显示物资信息
                SetMateData(undrugItem, unDrugOrgCount);

                this.fpSpread1_Sheet2.RowHeader.Cells[unDrugOrgCount, 0].Text = "补收";

                unDrugOrgCount++;
            }

        }
        //{E3C20659-CA54-457b-A907-650EEA30516C} 增加两个回车事件
        private void cmbRegDept_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.cmbDoct.Focus();
            }
        }

        //{B5A6A688-2711-4b8a-9029-7D5C29436E81}
        private void cmbRegDept_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (this.ucDisplay1.PatientInfo != null)
            {
                this.ucDisplay1.PatientInfo.DoctorInfo.Templet.Dept.ID = this.cmbRegDept.Tag.ToString();
            }
        }


        private void cmbDoct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ucDisplay1.SetFocus();
            }
        }

        //{B5A6A688-2711-4b8a-9029-7D5C29436E81}
        private void cmbDoct_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (this.ucDisplay1.PatientInfo != null)
            {
                this.ucDisplay1.PatientInfo.DoctorInfo.Templet.Doct.ID = this.cmbDoct.Tag.ToString();
            }
        }

        //{E3C20659-CA54-457b-A907-650EEA30516C} 完毕

        //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}

        #region 账户新增

        protected virtual int GetFeeList(FS.HISFC.Models.RADT.PatientInfo p)
        {
            DateTime beginTime = DateTime.MinValue;
            DateTime endTime = DateTime.MinValue;
            int returnValues = FS.FrameWork.WinForms.Classes.Function.ChooseDate(ref beginTime, ref endTime);
            if (returnValues < 0)
            {
                return -1;
            }

            this.patient.PID = p.PID;
            this.patient.Name = p.Name;
            this.patient.Pact = p.Pact;
            this.patient.Birthday = p.Birthday;
            this.patient.Sex = p.Sex;

            FT ft = new FT();
            if (GetList(p.PID.CardNO, beginTime, endTime, ref ft) < 0)
            {
                return -1;
            }
            this.tbName.Text = p.Name;
            this.tbPactName.Text = p.Pact.Name;
            this.tbPayCost.Text = ft.PayCost.ToString();
            this.tbOwnCost.Text = ft.OwnCost.ToString();
            this.tbPubCost.Text = ft.PubCost.ToString();
            this.tbTotCost.Text = ft.TotCost.ToString();
            isAccount = true;
            return 1;
        }

        /// <summary>
        /// 显示患者费用信息
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="cost"></param>
        /// <returns></returns>
        protected virtual int GetList(string cardNO, DateTime beginDate, DateTime endDate, ref FT ft)
        {
            try
            {
                ArrayList drugItemLists = outpatientManager.GetDrugFeeByCardNODate(cardNO, beginDate, endDate, true);
                if (drugItemLists == null)
                {
                    MessageBox.Show("获得药品信息出错!" + outpatientManager.Err);

                    return -1;
                }
                ArrayList undrugItemLists = outpatientManager.GetDrugFeeByCardNODate(cardNO, beginDate, endDate, false);
                if (undrugItemLists == null)
                {
                    MessageBox.Show("获得非药品信息出错!" + outpatientManager.Err);

                    return -1;
                }
                if (drugItemLists.Count + undrugItemLists.Count == 0)
                {
                    MessageBox.Show("没有费用信息!");

                    return -1;
                }

                ArrayList drugConfirmList = new ArrayList();//已经核准的退药信息
                ArrayList undrugConfirmList = new ArrayList();//已经核准退费的非药品信息
                //循环所有参与退费的发票,查询已经核准的药品和非药品信息
                //由于多张发票的存在,而明细只对应一个发票号,所以遍所有的参与退费的发票,其中只有一个发票号符合查询条件.
                //foreach (Balance balance in this.quitInvoices)
                //{
                //如果已经获得了已经核准退费的药品信息,就不再获取
                if (drugConfirmList == null || drugConfirmList.Count == 0)
                {
                    //获得已经核准的退药信息
                    drugConfirmList = returnApplyManager.GetApplyReturn(cardNO, true, false, true);
                    if (drugConfirmList == null)
                    {
                        MessageBox.Show("获得确认药品项目列表出错!" + returnApplyManager.Err);

                        return -1;
                    }
                }
                //如果已经获得了已经核准退费的非药品信息,就不再获取
                if (undrugConfirmList == null || undrugConfirmList.Count == 0)
                {
                    //获得已经核准退费的非药品信息
                    undrugConfirmList = returnApplyManager.GetApplyReturn(cardNO, true, false, false);
                    if (undrugConfirmList == null)
                    {
                        MessageBox.Show("获得确认非药品项目列表出错!" + returnApplyManager.Err);

                        return -1;
                    }
                }
                //}
                //显示待退药品信息
                this.fpSpread1_Sheet1.RowCount = drugItemLists.Count;

                FeeItemList drugItem = null;//药品临时实体
                for (int i = 0; i < drugItemLists.Count; i++)
                {
                    drugItem = drugItemLists[i] as FeeItemList;

                    if (drugItem.FT.RebateCost > 0)
                    {
                        isHaveRebateCost = true;
                    }


                    //重新计算本条药品的总金额,方便以后参与计算费用
                    drugItem.FT.TotCost = drugItem.FT.OwnCost + drugItem.FT.PayCost + drugItem.FT.PubCost;

                    this.fpSpread1_Sheet1.Rows[i].Tag = drugItem;
                    //因为可能存在同一发票有不同看诊科室的情况,而且挂号信息中的看诊信息不一定与实际收费的看诊
                    //科室相同,所以这里把挂号实体的看诊可是赋值为收费明细时的看诊科室信息.
                    this.patient.DoctorInfo.Templet.Dept = drugItem.RecipeOper.Dept;

                    this.fpSpread1_Sheet1.Cells[i, (int)DrugList.ItemName].Text = drugItem.Item.Name;

                    this.fpSpread1_Sheet1.Cells[i, (int)DrugList.CombNo].Text = drugItem.Order.Combo.ID;

                    this.fpSpread1_Sheet1.Cells[i, (int)DrugList.Specs].Text = drugItem.Item.Specs;
                    this.fpSpread1_Sheet1.Cells[i, (int)DrugList.Amount].Text = drugItem.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(drugItem.Item.Qty / drugItem.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(drugItem.Item.Qty, 2).ToString();
                    this.fpSpread1_Sheet1.Cells[i, (int)DrugList.PriceUnit].Text = drugItem.Item.PriceUnit;
                    this.fpSpread1_Sheet1.Cells[i, (int)DrugList.NoBackQty].Text = drugItem.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(drugItem.NoBackQty / drugItem.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(drugItem.NoBackQty, 2).ToString();
                    this.fpSpread1_Sheet1.Cells[i, (int)DrugList.Cost].Text = (drugItem.FT.OwnCost + drugItem.FT.PayCost + drugItem.FT.PubCost).ToString();

                    if (drugItem.Item.SysClass.ID.ToString() == "PCC")
                    {
                        this.fpSpread1_Sheet1.Cells[i, (int)DrugList.DoseAndDays].Text = "每次量:" + drugItem.Order.DoseOnce.ToString() + drugItem.Order.DoseUnit + " " + "付数:" + drugItem.Days.ToString();
                    }
                    else
                    {
                        this.fpSpread1_Sheet1.Cells[i, (int)DrugList.DoseAndDays].Text = "每次量:" + drugItem.Order.DoseOnce.ToString() + drugItem.Order.DoseUnit;
                    }
                    //cost += drugItem.FT.TotCost;
                    ft.TotCost += drugItem.FT.TotCost;
                    ft.OwnCost += drugItem.FT.OwnCost;
                    ft.PubCost += drugItem.FT.PubCost;
                    ft.PayCost += drugItem.FT.PayCost;
                    Class.Function.DrawCombo(this.fpSpread1_Sheet1, (int)DrugList.CombNo, (int)DrugList.Comb, 0);
                }

                //显示非药品信息
                this.fpSpread1_Sheet2.RowCount = undrugItemLists.Count;

                FeeItemList undrugItem = null;
                for (int i = 0; i < undrugItemLists.Count; i++)
                {
                    undrugItem = undrugItemLists[i] as FeeItemList;

                    #region 加载物资信息
                    //{143CA424-7AF9-493a-8601-2F7B1D635027}
                    //string outNo = undrugItem.UpdateSequence.ToString();
                    //List<HISFC.Object.Material.Output> list = mateIntegrate.QueryOutput(outNo);
                    //undrugItem.MateList = list;
                    #endregion

                    if (undrugItem.FT.RebateCost > 0)
                    {
                        isHaveRebateCost = true;
                    }

                    undrugItem.FT.TotCost = undrugItem.FT.OwnCost + undrugItem.FT.PayCost + undrugItem.FT.PubCost;
                    this.fpSpread1_Sheet2.Rows[i].Tag = undrugItem;
                    this.patient.DoctorInfo.Templet.Dept = undrugItem.RecipeOper.Dept;

                    this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.ItemName].Text = undrugItem.Item.Name;
                    this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.CombNo].Text = undrugItem.Order.Combo.ID;
                    this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.Amount].Text = undrugItem.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(undrugItem.Item.Qty / undrugItem.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(undrugItem.Item.Qty, 2).ToString();
                    this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.PriceUnit].Text = undrugItem.Item.PriceUnit;
                    this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.NoBackQty].Text = undrugItem.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(undrugItem.NoBackQty / undrugItem.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(undrugItem.NoBackQty, 2).ToString();
                    this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.Cost].Text = (undrugItem.FT.OwnCost + undrugItem.FT.PayCost + undrugItem.FT.PubCost).ToString();
                    if (undrugItem.UndrugComb.ID != null && undrugItem.UndrugComb.ID.Length > 0)
                    {
                        this.undrugComb = this.undrugManager.GetValidItemByUndrugCode(undrugItem.UndrugComb.ID);
                        if (this.undrugComb == null)
                        {
                            MessageBox.Show("获得组套信息出错，无法显示组套自定义码，但是不影响退费操作！");
                        }
                        else
                        {
                            undrugItem.UndrugComb.UserCode = this.undrugComb.UserCode;
                        }

                        FS.HISFC.Models.Fee.Item.Undrug item = this.undrugManager.GetValidItemByUndrugCode(undrugItem.ID);

                        if (item == null)
                        {
                            this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.PackageName].Text = "(" + undrugItem.UndrugComb.UserCode + ")" + undrugItem.UndrugComb.Name;
                        }
                        else
                        {
                            this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.PackageName].Text = "(" + undrugItem.UndrugComb.UserCode + ")" + undrugItem.UndrugComb.Name + "[" + item.UserCode + "]";
                        }

                    }
                    else
                    {
                        FS.HISFC.Models.Fee.Item.Undrug item = this.undrugManager.GetValidItemByUndrugCode(undrugItem.ID);

                        if (item != null)
                        {
                            this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.PackageName].Text = item.UserCode;
                        }
                    }
                    //cost += undrugItem.FT.TotCost;
                    ft.TotCost += undrugItem.FT.TotCost;
                    ft.OwnCost += undrugItem.FT.OwnCost;
                    ft.PubCost += undrugItem.FT.PubCost;
                    ft.PayCost += undrugItem.FT.PayCost;
                    Class.Function.DrawCombo(this.fpSpread1_Sheet2, (int)UndrugList.CombNo, (int)UndrugList.Comb, 0);
                    //显示物资信息
                    //SetMateData(undrugItem, i);
                }
                //显示确认退药信息
                this.fpSpread2_Sheet1.RowCount = drugItemLists.Count + drugConfirmList.Count;
                FS.HISFC.Models.Fee.ReturnApply drugReturn = null;
                for (int i = 0; i < drugConfirmList.Count; i++)
                {
                    drugReturn = drugConfirmList[i] as FS.HISFC.Models.Fee.ReturnApply;
                    this.fpSpread2_Sheet1.Rows[i].Tag = drugReturn;
                    this.fpSpread2_Sheet1.Cells[i, (int)DrugListQuit.ItemName].Text = drugReturn.Item.Name;
                    this.fpSpread2_Sheet1.Cells[i, (int)DrugListQuit.Amount].Text = drugReturn.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(drugReturn.Item.Qty / drugReturn.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(drugReturn.Item.Qty, 2).ToString();
                    this.fpSpread2_Sheet1.Cells[i, (int)DrugListQuit.PriceUnit].Text = drugReturn.Item.PriceUnit;
                    this.fpSpread2_Sheet1.Cells[i, (int)DrugListQuit.Specs].Text = drugReturn.Item.Specs;
                    this.fpSpread2_Sheet1.Cells[i, (int)DrugListQuit.Flag].Text = "确认";

                    int findRow = FindItem(drugReturn.RecipeNO, drugReturn.SequenceNO, this.fpSpread1_Sheet1);
                    if (findRow == -1)
                    {
                        MessageBox.Show("查找未退药项目出错!");

                        return -1;
                    }
                    FeeItemList modifyDrug = this.fpSpread1_Sheet1.Rows[findRow].Tag as FeeItemList;

                    modifyDrug.NoBackQty = modifyDrug.NoBackQty - drugReturn.Item.Qty;
                    modifyDrug.Item.Qty = modifyDrug.Item.Qty - drugReturn.Item.Qty;
                    modifyDrug.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(modifyDrug.Item.Price * modifyDrug.Item.Qty / modifyDrug.Item.PackQty, 2);
                    modifyDrug.FT.OwnCost = modifyDrug.FT.TotCost;

                    this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.Cost].Text = modifyDrug.FT.TotCost.ToString();
                    this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.Amount].Text = modifyDrug.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(modifyDrug.Item.Qty / modifyDrug.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(modifyDrug.Item.Qty, 2).ToString();
                    this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.NoBackQty].Text = modifyDrug.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(modifyDrug.NoBackQty / modifyDrug.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(modifyDrug.NoBackQty, 2).ToString();
                }
                this.fpSpread2_Sheet2.RowCount = undrugItemLists.Count + undrugConfirmList.Count;
                FS.HISFC.Models.Fee.ReturnApply undrugReturn = null;
                for (int i = 0; i < undrugConfirmList.Count; i++)
                {
                    undrugReturn = undrugConfirmList[i] as FS.HISFC.Models.Fee.ReturnApply;
                    this.fpSpread2_Sheet2.Rows[i].Tag = undrugReturn;
                    this.fpSpread2_Sheet2.Cells[i, (int)UndrugListQuit.ItemName].Text = undrugReturn.Item.Name;
                    this.fpSpread2_Sheet2.Cells[i, (int)UndrugListQuit.Amount].Text = undrugReturn.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(undrugReturn.Item.Qty / undrugReturn.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(undrugReturn.Item.Qty, 2).ToString();
                    this.fpSpread2_Sheet2.Cells[i, (int)UndrugListQuit.PriceUnit].Text = undrugReturn.Item.PriceUnit;
                    this.fpSpread2_Sheet2.Cells[i, (int)UndrugListQuit.Flag].Text = "确认";

                    int findRow = FindItem(undrugReturn.RecipeNO, undrugReturn.SequenceNO, this.fpSpread1_Sheet2);
                    if (findRow == -1)
                    {
                        MessageBox.Show("查找未退非药项目出错!");

                        return -1;
                    }
                    FeeItemList modifyUndrug = this.fpSpread1_Sheet2.Rows[findRow].Tag as FeeItemList;

                    modifyUndrug.NoBackQty = modifyUndrug.NoBackQty - undrugReturn.Item.Qty;
                    modifyUndrug.Item.Qty = modifyUndrug.Item.Qty - undrugReturn.Item.Qty;
                    modifyUndrug.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(modifyUndrug.Item.Price * modifyUndrug.Item.Qty / modifyUndrug.Item.PackQty, 2);
                    modifyUndrug.FT.OwnCost = modifyUndrug.FT.TotCost;

                    this.fpSpread1_Sheet2.Cells[findRow, (int)UndrugList.Cost].Text = modifyUndrug.FT.TotCost.ToString();
                    this.fpSpread1_Sheet2.Cells[findRow, (int)UndrugList.Amount].Text = modifyUndrug.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(modifyUndrug.Item.Qty / modifyUndrug.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(modifyUndrug.Item.Qty, 2).ToString();
                    this.fpSpread1_Sheet2.Cells[findRow, (int)UndrugList.NoBackQty].Text = modifyUndrug.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(modifyUndrug.NoBackQty / modifyUndrug.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(modifyUndrug.NoBackQty, 2).ToString();

                }

                if (isHaveRebateCost)
                {
                    this.ckbAllQuit.Checked = true;
                    this.ckbAllQuit.Enabled = false;
                }
                else
                {
                    this.ckbAllQuit.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }


            return 1;

        }

        /// <summary>
        /// 作废费用信息
        /// </summary>
        /// <param name="f"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        private int SaveAccountQuiteFee()
        {

            DialogResult diaResult = MessageBox.Show("是否要退费?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (diaResult == DialogResult.No)
            {
                return -1;
            }

            if (!feeIntegrate.CheckAccountPassWord(this.patient))
            {
                return -1;
            }

            if (!IsQuitItem())
            {
                return -1;
            }

            ArrayList alFee = new ArrayList();
            FeeItemList tempf = null;
            DateTime nowTime = outpatientManager.GetDateTimeFromSysDateTime();
            int iReturn;
            FeeItemList f = null;
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            foreach (FarPoint.Win.Spread.SheetView sv in fpSpread1.Sheets)
            {
                for (int i = 0; i < sv.Rows.Count; i++)
                {
                    if (sv.Rows[i].Tag == null) continue;

                    f = (sv.Rows[i].Tag as FeeItemList).Clone();

                    #region 作废申请数据
                    if (f.Item.ItemType == EnumItemType.Drug)
                    {
                        if (f.IsConfirmed == false)
                        {
                            iReturn = pharmacyIntegrate.CancelApplyOutClinicMZTF(f.RecipeNO, f.SequenceNO);//{A56F1E9D-9E9D-48bb-A3EA-8F17A6738619}
                            if (iReturn < 0) //{5CEF35C8-9C9E-4208-934C-5DC02F3413B4}
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("作废发药申请出错!" + pharmacyIntegrate.Err);
                                return -1;
                            }
                        }

                        tempf = f.Clone();
                        tempf.FT.OwnCost = tempf.FT.PubCost = tempf.FT.PayCost = 0;
                        tempf.FT.OwnCost = tempf.FT.TotCost;
                        //if (FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)DrugList.Amount].Text) > 0)//by yuyun 解决门诊已发药之后做半退，退费时全退的情况
                        if (f.Item.Qty > 0)
                        {
                            tempf.User03 = "HalfQuit";
                            alFee.Add(tempf);
                        }
                    }
                    else
                    {
                        //有未确认的退药，作废退药申请!
                        if (f.IsConfirmed == false)
                        {
                            iReturn = confirmIntegrate.CancelConfirmTerminal(f.Order.ID, f.Item.ID);
                            if (iReturn < 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("作废终端申请出错!" + confirmIntegrate.Err);

                                return -1;
                            }
                        }
                        else
                        {
                            #region 更新终端确认信息
                            tempf = f.Clone();
                            tempf.FT.OwnCost = tempf.FT.PubCost = tempf.FT.PayCost = 0;
                            tempf.FT.OwnCost = tempf.FT.TotCost;

                            //{06212A22-5FD4-4db3-838C-1790F75FF286}
                            //if (FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.Amount].Text) > 0)
                            if (f.Item.Qty > 0)
                            {
                                FS.HISFC.Models.Fee.Item.Undrug unDrugTemp = this.undrugManager.GetUndrugByCode(f.Item.ID);
                                if (unDrugTemp != null)
                                {
                                    tempf.Item.IsNeedConfirm = unDrugTemp.IsNeedConfirm;
                                    tempf.Item.NeedConfirm = unDrugTemp.NeedConfirm;
                                    tempf.Item.IsNeedBespeak = unDrugTemp.IsNeedBespeak;
                                }

                                //{06212A22-5FD4-4db3-838C-1790F75FF286}
                                if (tempf.IsConfirmed == true)
                                {
                                    int row = this.FindItem(tempf.RecipeNO, tempf.SequenceNO, this.fpSpread2_Sheet2);
                                    if (row != -1)
                                    {
                                        FeeItemList quitItem = this.fpSpread2_Sheet2.Rows[row].Tag as FeeItemList;
                                        if (confirmIntegrate.UpdateOrDeleteTerminalConfirmApply(tempf.Order.ID, (int)(tempf.Item.Qty + quitItem.Item.Qty), (int)quitItem.Item.Qty, FS.FrameWork.Public.String.FormatNumber(tempf.Item.Price * tempf.Item.Qty, 2)) == -1)
                                        {
                                            FS.FrameWork.Management.PublicTrans.RollBack();
                                            MessageBox.Show("更新终端确认信息出错!" + confirmIntegrate.Err);
                                            return -1;
                                        }
                                    }
                                }
                                tempf.User03 = "HalfQuit";
                                alFee.Add(tempf);
                            }
                            #endregion
                        }

                    }

                    #endregion

                    #region 更新费用退费标记
                    if (outpatientManager.UpdateFeeItemListCancelType(f.RecipeNO, f.SequenceNO, CancelTypes.Canceled) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("作废患者明细出错!" + outpatientManager.Err);
                        return -1;
                    }
                    #endregion

                    #region 冲负记录

                    FeeItemList feeItem = outpatientManager.GetFeeItemListForFee(f.RecipeNO, f.SequenceNO);
                    if (feeItem == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("作废患者明细出错!" + outpatientManager.Err);
                        return -1;
                    }
                    feeItem.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                    feeItem.FT.OwnCost = -feeItem.FT.OwnCost;
                    feeItem.FT.PayCost = -feeItem.FT.PayCost;
                    feeItem.FT.PubCost = -feeItem.FT.PubCost;
                    feeItem.FT.TotCost = feeItem.FT.OwnCost + feeItem.FT.PubCost + feeItem.FT.PayCost;
                    feeItem.Item.Qty = -feeItem.Item.Qty;
                    feeItem.CancelType = FS.HISFC.Models.Base.CancelTypes.Canceled;
                    feeItem.FeeOper.ID = outpatientManager.Operator.ID;
                    feeItem.FeeOper.OperTime = nowTime;
                    feeItem.ChargeOper.OperTime = nowTime;
                    feeItem.ConfirmedInjectCount = 0;
                    feeItem.InvoiceCombNO = this.outpatientManager.GetTempInvoiceComboNO();
                    iReturn = outpatientManager.InsertFeeItemList(feeItem);
                    if (iReturn <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("插入费用明细冲帐信息出错!" + outpatientManager.Err);
                        return -1;
                    }
                    #endregion
                }
            }

            #region 半退
            ArrayList drugList = new ArrayList();
            if (alFee.Count > 0)
            {
                foreach (FeeItemList item in alFee)
                {
                    item.FeeOper.OperTime = nowTime;
                    item.PayType = PayTypes.Balanced;
                    item.TransType = TransTypes.Positive;
                    item.InvoiceCombNO = outpatientManager.GetTempInvoiceComboNO();
                    if (outpatientManager.InsertFeeItemList(item) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("插入费用明细失败！" + outpatientManager.Err);
                        return -1;
                    }

                    //发药申请
                    if (f.Item.ItemType == EnumItemType.Drug)
                    {
                        if (!f.IsConfirmed)
                        {
                            if (!f.Item.IsNeedConfirm)
                            {
                                drugList.Add(f);
                            }
                        }
                    }
                    else
                    {
                        //终端申请
                        if (!f.IsConfirmed)
                        {
                            if (f.Item.NeedConfirm == FS.HISFC.Models.Fee.Item.EnumNeedConfirm.Outpatient || f.Item.NeedConfirm == FS.HISFC.Models.Fee.Item.EnumNeedConfirm.All)
                            {
                                FS.HISFC.BizProcess.Integrate.Terminal.Result result = confirmIntegrate.ServiceInsertTerminalApply(f, this.patient);
                                if (result != FS.HISFC.BizProcess.Integrate.Terminal.Result.Success)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("处理终端申请确认表失败!" + confirmIntegrate.Err);

                                    return -1;
                                }
                            }
                        }
                    }

                    #region 半退重收更新原明细的执行状态

                    iReturn = outpatientManager.UpdateSampleState(item.RecipeNO, item.Order.ID);
                    if (iReturn <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更新样本状态信息出错!" + outpatientManager.Err);
                        return -1;
                    }

                    #endregion
                }
                if (drugList.Count > 0)
                {
                    string drugSendInfo = string.Empty;
                    iReturn = this.pharmacyIntegrate.ApplyOut(patient, drugList, string.Empty, nowTime, false, out drugSendInfo);
                    if (iReturn == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("处理药品明细失败!" + pharmacyIntegrate.Err);
                        return -1;
                    }
                }
            }
            #endregion

            #region 更新退费申请退费标记
            FS.HISFC.Models.Fee.ReturnApply returnApply = null;
            DateTime operDate = outpatientManager.GetDateTimeFromSysDateTime();
            string operCode = outpatientManager.Operator.ID;
            foreach (FarPoint.Win.Spread.SheetView sv in fpSpread2.Sheets)
            {
                for (int i = 0; i < sv.Rows.Count; i++)
                {
                    if (sv.Rows[i].Tag is FS.HISFC.Models.Fee.ReturnApply)
                    {
                        returnApply = sv.Rows[i].Tag as FS.HISFC.Models.Fee.ReturnApply;
                        returnApply.CancelType = CancelTypes.Valid;
                        returnApply.CancelOper.ID = operCode;
                        returnApply.CancelOper.OperTime = operDate;
                        if (returnApplyManager.UpdateApplyCharge(returnApply) <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("更新申请表退费标记失败！" + returnApplyManager.Err);
                            return -1;
                        }
                    }
                }
            }

            #endregion

            #region 返还账户
            decimal cost = 0m;
            FS.HISFC.Models.Fee.ReturnApply applyItem = null;
            FS.HISFC.Models.Fee.Outpatient.FeeItemList fitem = null;
            foreach (FarPoint.Win.Spread.SheetView sv in fpSpread2.Sheets)
            {
                for (int i = 0; i < sv.Rows.Count; i++)
                {
                    if (sv.Rows[i].Tag == null) continue;
                    if (sv.Rows[i].Tag is FS.HISFC.Models.Fee.ReturnApply)
                    {
                        applyItem = sv.Rows[i].Tag as FS.HISFC.Models.Fee.ReturnApply;
                        cost += FS.FrameWork.Public.String.FormatNumber(applyItem.Item.Price * applyItem.Item.Qty / applyItem.Item.PackQty, 2);
                    }
                    if (sv.Rows[i].Tag is FeeItemList)
                    {
                        fitem = sv.Rows[i].Tag as FeeItemList;
                        cost += FS.FrameWork.Public.String.FormatNumber(fitem.Item.Price * fitem.Item.Qty / fitem.Item.PackQty, 2);
                    }
                }
            }
            if (feeIntegrate.AccountCancelPay(patient, -cost, "门诊退费", (outpatientManager.Operator as Employee).Dept.ID, string.Empty) < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("账户退费入户失败！" + feeIntegrate.Err);
                return -1;
            }

            #endregion
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("返还账户金额" + cost.ToString() + "元");
            return 1;
        }
        #endregion


        #region IInterfaceContainer 成员

        public Type[] InterfaceTypes
        {
            get
            {
                Type[] type = new Type[1];
                type[0] = typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IBankTrans);
                return type;
            }
        }
        #endregion


        #region 增加通过卡号查找发票信息,实现预交金退费功能 {2E5139C9-52D8-4fec-A96B-09BECFDDFBD1}

        #region 换用发票浏览控件 不用

        //private void btnClose_Click(object sender, EventArgs e)
        //{
        //    panelWidth = this.pnlLeft.Width;
        //    this.pnlLeft.Width = this.btnShow.Width;
        //    this.panelTree.Visible = false;
        //    this.dtpInvoiceDate.Visible = false;
        //    this.txtCardNO.Visible = false;
        //    this.btnClose.Visible = false;
        //    this.btnShow.Visible = true;
        //}
        //private int panelWidth = 0;
        //private void btnShow_Click(object sender, EventArgs e)
        //{
        //    if (panelWidth == 0)
        //    {
        //        panelWidth = 210;
        //    }
        //    this.pnlLeft.Width = panelWidth;
        //    this.btnShow.Visible = false;
        //    this.panelTree.Visible = true;
        //    this.dtpInvoiceDate.Visible = true;
        //    this.txtCardNO.Visible = true;
        //    this.btnClose.Visible = true;
        //}

        //private void txtCardNO_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode != Keys.Enter)
        //        return;

        //    string strCard = txtCardNO.Text.Trim();
        //    if (string.IsNullOrEmpty(strCard))
        //        return;

        //    HISFC.Models.Account.AccountCard objCard = new FS.HISFC.Models.Account.AccountCard();
        //    int iTemp = feeIntegrate.ValidMarkNO(strCard, ref objCard);
        //    if (iTemp <= 0 || objCard == null)
        //    {
        //        MessageBox.Show("无效卡号，请联系管理员！");
        //        return ;
        //    }

        //    this.trvInvoice.Nodes.Clear();
        //    this.Clear();

        //    DateTime dtReg = this.dtpInvoiceDate.Value.Date;
        //    List<Balance> lstInvoice = null;
        //    iTemp = outpatientManager.QueryInvoiceInfoByCardNo(objCard.Patient.PID.CardNO, dtReg, DateTime.Now, out lstInvoice);
        //    if (iTemp <= 0)
        //    {
        //        MessageBox.Show(outpatientManager.Err);
        //        return;
        //    }

        //    if (lstInvoice == null || lstInvoice.Count <= 0)
        //    {
        //        MessageBox.Show("未找到发票信息！");
        //        return;
        //    }

        //    AddInvoiceToTree(lstInvoice);


        //}
        ///// <summary>
        ///// 添加发票信息到树
        ///// </summary>
        ///// <param name="lstInvoice"></param>
        //private void AddInvoiceToTree(List<Balance> lstInvoice)
        //{
        //    this.trvInvoice.Nodes.Clear();
        //    if (lstInvoice == null || lstInvoice.Count <= 0)
        //        return;

        //    foreach (Balance invoice in lstInvoice)
        //    {
        //        AddInvoiceToTree(invoice);
        //    }

        //}

        //private void AddInvoiceToTree(Balance invoice)
        //{
        //    if (invoice == null)
        //        return;

        //    TreeNode[] tnArr = trvInvoice.Nodes.Find(invoice.Patient.ID, true);

        //    TreeNode tn = null;
        //    TreeNode tnTemp = null;
        //    if (tnArr == null || tnArr.Length <= 0)
        //    {
        //        tn = new TreeNode();
        //        tn.Name = invoice.Patient.ID;
        //        tn.Text = ((FS.HISFC.Models.Registration.Register)invoice.Patient).DoctorInfo.SeeDate.ToString("yyyy-MM-dd HH:mm");

        //        trvInvoice.Nodes.Add(tn);

        //        tnTemp = new TreeNode();
        //        tnTemp.Name = invoice.Invoice.ID + "-" + invoice.CombNO;
        //        tnTemp.Text = invoice.Invoice.ID;
        //        tnTemp.Tag = invoice;

        //        tn.Nodes.Add(tnTemp);
        //    }
        //    else
        //    {
        //        tnTemp = new TreeNode();
        //        tnTemp.Name = invoice.Invoice.ID + "-" + invoice.CombNO;
        //        tnTemp.Text = invoice.Invoice.ID;
        //        tnTemp.Tag = invoice;

        //        tn = tnArr[0];
        //        tn.Nodes.Add(tnTemp);
        //    }
        //}

        //private void trvInvoice_AfterSelect(object sender, TreeViewEventArgs e)
        //{
        //    if (e.Node != null && e.Node.Tag != null)
        //    {
        //        this.Clear();

        //        Balance invoice = e.Node.Tag as Balance;
        //        if (invoice != null)
        //        {
        //            tbInvoiceNO.Text = invoice.Invoice.ID;

        //            System.Windows.Forms.KeyEventArgs keyEvent = new KeyEventArgs(Keys.Enter);

        //            tbInvoiceNo_KeyDown(sender, keyEvent);
        //        }

        //    }
        //}

        #endregion

        private void ucInvoiceView_evnInvoiceSelectChange(object sender, Balance invoice)
        {
            if (invoice != null)
            {
                tbInvoiceNO.Text = invoice.Invoice.ID;
                System.Windows.Forms.KeyEventArgs keyEvent = new KeyEventArgs(Keys.Enter);
                tbInvoiceNo_KeyDown(sender, keyEvent);
            }
        }

        bool blnShowInvoiceNoFind = true;
        private void ucInvoiceView_evnInvoiceNoFind()
        {
            if (blnShowInvoiceNoFind)
            {
                MessageBox.Show("未找到发票信息！");
            }
        }
        #endregion

        #region IPreArrange 成员

        public int PreArrange()
        {
            if (string.IsNullOrEmpty(this.operationPriv))
            {
                return 1;
            }
            else
            {
                //如果是管理员，则可以直接进来
                if (((FS.HISFC.Models.Base.Employee)outpatientManager.Operator).IsManager)
                {
                    return 1;
                }

                string[] privs = this.operationPriv.Split(new string[] { "+" }, StringSplitOptions.RemoveEmptyEntries);
                if (privs.Length == 0)
                {
                    return 1;
                }
                else if (privs.Length == 1)//只判断有没有二级权限
                {
                    if (CommonController.Instance.JugePrive(privs[0]) == false)
                    {
                        CommonController.Instance.MessageBox(this, "您没有操作退费的权限，操作已取消！", MessageBoxIcon.Stop);
                        return -1;
                    }
                }
                else
                {
                    string class2Code = privs[0];
                    string class3Code = privs[1];
                    if (CommonController.Instance.JugePrive(privs[0], privs[1]) == false)
                    {
                        CommonController.Instance.MessageBox(this, "您没有操作退费的权限，操作已取消！", MessageBoxIcon.Stop);
                        return -1;
                    }
                }
            }

            return 1;
        }

        #endregion

        #region 新增姓名查询(参考发票补打,临时处理)

        private void tbName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }
            ArrayList tal = new ArrayList();

            string cardNo;
            FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
            int ret = feeIntegrate.ValidMarkNO(tbName.Text, ref accountCard);
            if (ret > 0)
            {
                cardNo = accountCard.Patient.PID.CardNO;
                tbName.Text = cardNo;
            }
            else
            {
                cardNo = tbName.Text;
            }

            string sql = @"select i.invoice_no, i.print_invoiceno, i.tot_cost, i.oper_date, i.name
                            from fin_opb_invoiceinfo i
                           where (i.card_no = '{0}' or i.name = '{0}')
                             --and i.oper_date >= sysdate - 60
                             and i.TRANS_TYPE = '1'
                             and i.cancel_flag = '1'
                           order by oper_date desc";

            sql = string.Format(sql, cardNo);
            DataSet ds = new DataSet();

            int result = outpatientManager.ExecQuery(sql, ref ds);
            if (result == -1)
            {
                MessageBox.Show("查询数据失败!");
                return;
            }
            if (ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("没有找到发票数据!");
                return;
            }

            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                FS.HISFC.Models.Base.Const c = new FS.HISFC.Models.Base.Const();
                c.ID = c.ID = dr[0].ToString();
                c.Name = dr[1].ToString();
                c.Memo = dr[2].ToString();
                c.SpellCode = dr[3].ToString();
                c.WBCode = dr[4].ToString();

                tal.Add(c);
            }
            FS.HISFC.Models.Base.Const tobj = null;

            if (tal.Count == 0)
            {
                MessageBox.Show("没有找到发票数据!");
                return;
            }
            else
            {
                FrameWork.WinForms.Forms.frmEasyChoose fc = new FS.FrameWork.WinForms.Forms.frmEasyChoose(tal);
                string[] cols = { "发票电脑号", "发票印刷号", "金额", "日期", "姓名" };
                bool[] visibles = { true, true, true, true, true, false };
                int[] widths = { 80, 80, 80, 120, 60 };
                fc.SetFormat(cols, visibles, widths);
                fc.ShowDialog();

                tobj = fc.Object as FS.HISFC.Models.Base.Const;
                if (tobj == null)
                {
                    MessageBox.Show("请选择一条记录!");
                    return;
                }
            }

            tbInvoiceNO.Text = tobj.ID;
            tbInvoiceNo_KeyDown(null, new KeyEventArgs(Keys.Enter));
        }

        #endregion

        #region 新增卡号查询(参考发票补打,临时处理)

        protected virtual void tbCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }
            ArrayList tal = new ArrayList();

            string cardNo;
            FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
            int ret = feeIntegrate.ValidMarkNO(tbCardNo.Text, ref accountCard);
            if (ret > 0)
            {
                cardNo = accountCard.Patient.PID.CardNO;
                tbCardNo.Text = cardNo;
            }
            else
            {
                cardNo = tbCardNo.Text;
            }

            string sql = @"select i.invoice_no, i.print_invoiceno, i.tot_cost, i.oper_date, i.name
                            from fin_opb_invoiceinfo i
                           where (i.card_no = '{0}' or i.name = '{0}')
                             --and i.oper_date >= sysdate - 30
                             and i.TRANS_TYPE = '1'
                             and i.cancel_flag = '1'
                           order by oper_date desc";

            sql = string.Format(sql, cardNo);
            DataSet ds = new DataSet();

            int result = outpatientManager.ExecQuery(sql, ref ds);
            if (result == -1)
            {
                MessageBox.Show("查询数据失败!");
                return;
            }
            if (ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("没有找到发票数据!");
                return;
            }

            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                FS.HISFC.Models.Base.Const c = new FS.HISFC.Models.Base.Const();
                c.ID = c.ID = dr[0].ToString();
                c.Name = dr[1].ToString();
                c.Memo = dr[2].ToString();
                c.SpellCode = dr[3].ToString();
                c.WBCode = dr[4].ToString();

                tal.Add(c);
            }
            FS.HISFC.Models.Base.Const tobj = null;

            if (tal.Count == 0)
            {
                MessageBox.Show("没有找到发票数据!");
                return;
            }
            else
            {
                FrameWork.WinForms.Forms.frmEasyChoose fc = new FS.FrameWork.WinForms.Forms.frmEasyChoose(tal);
                string[] cols = { "发票电脑号", "发票印刷号", "金额", "日期", "姓名" };
                bool[] visibles = { true, true, true, true, true, false };
                int[] widths = { 80, 80, 80, 120, 60 };
                fc.SetFormat(cols, visibles, widths);
                fc.ShowDialog();

                tobj = fc.Object as FS.HISFC.Models.Base.Const;
                if (tobj == null)
                {
                    MessageBox.Show("请选择一条记录!");
                    return;
                }
            }

            tbInvoiceNO.Text = tobj.ID;
            tbInvoiceNo_KeyDown(null, new KeyEventArgs(Keys.Enter));
        }

        #endregion
    }
}
