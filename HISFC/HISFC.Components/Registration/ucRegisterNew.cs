using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Reflection;
using System.Collections.Generic;
using FS.HISFC.Models.Account;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using FS.HISFC.BizProcess.Interface;


namespace FS.HISFC.Components.Registration
{
    /// <summary>
    /// 门诊挂号 -- 新增界面
    /// 
    /// 1、挂号记录不再保存费用相关信息
    /// 2、挂号费、诊金、病历本费、诊疗卡工本费均保存在fin_opb_accountCardFee表里；
    /// 
    /// </summary>
    public partial class ucRegisterNew : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer, FS.HISFC.BizProcess.Interface.FeeInterface.ISIReadCard
    {
        public ucRegisterNew()
        {
            InitializeComponent();

            this.Load += new EventHandler(ucRegister_Load);
            this.cmbRegLevel.SelectedIndexChanged += new EventHandler(cmbRegLevel_SelectedIndexChanged);
            this.cmbDept.SelectedIndexChanged += new EventHandler(cmbDept_SelectedIndexChanged);
            this.cmbDoctor.SelectedIndexChanged += new EventHandler(cmbDoctor_SelectedIndexChanged);

            this.cmbCardType.KeyDown += new KeyEventHandler(cmbCardType_KeyDown);
            this.dtBookingDate.ValueChanged += new EventHandler(dtBookingDate_ValueChanged);
            this.dtBookingDate.KeyDown += new KeyEventHandler(dtBookingDate_KeyDown);
            this.dtBegin.ValueChanged += new EventHandler(dtBegin_ValueChanged);
            this.dtBegin.KeyDown += new KeyEventHandler(dtBegin_KeyDown);
            this.dtEnd.ValueChanged += new EventHandler(dtEnd_ValueChanged);
            this.dtEnd.KeyDown += new KeyEventHandler(dtEnd_KeyDown);
            this.txtOrder.KeyDown += new KeyEventHandler(txtOrder_KeyDown);
            this.cmbUnit.SelectedIndexChanged += new EventHandler(cmbUnit_SelectedIndexChanged);
            this.txtOrder.TextChanged += new EventHandler(txtOrder_TextChanged);
            this.llPd.Click += new EventHandler(llPd_Click);
            this.txtRecipeNo.KeyDown += new KeyEventHandler(txtRecipeNo_KeyDown);
            this.txtRecipeNo.Validating += new CancelEventHandler(txtRecipeNo_Validating);
            this.fpSpread1.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(fpSpread1_CellClick);
            this.cmbDoctor.TextChanged += new EventHandler(cmbDoctor_TextChanged);
            this.txtPhone.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPhone_KeyDown);
            this.txtPhone.Enter += new System.EventHandler(this.txtCardNo_Enter);
            this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
            this.txtName.Leave += new System.EventHandler(this.txtName_Leave);
            this.txtName.Enter += new System.EventHandler(this.txtName_Enter);
            this.cmbDept.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbDept_KeyDown);
            this.cmbDept.TextChanged += new System.EventHandler(this.cmbDept_TextChanged);
            this.cmbDept.Enter += new System.EventHandler(this.cmbDept_Enter);
            this.cmbDoctor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbDoctor_KeyDown);
            this.cmbDoctor.Enter += new System.EventHandler(this.cmbDoctor_Enter);
            this.cmbUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbUnit_KeyDown);
            this.txtAge.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAge_KeyDown);
            this.txtAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAddress_KeyDown);
            this.txtAddress.Leave += new System.EventHandler(this.txtAddress_Leave);
            this.txtAddress.Enter += new System.EventHandler(this.txtAddress_Enter);
            this.txtMcardNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMcardNo_KeyDown);
            this.txtMcardNo.Enter += new System.EventHandler(this.txtCardNo_Enter);
            this.cmbPayKind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbPayKind_KeyDown);
            this.cmbPayKind.Enter += new System.EventHandler(this.cmbPayKind_Enter);
            this.cmbSex.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSex_KeyDown);
            this.cmbSex.Enter += new System.EventHandler(this.txtCardNo_Enter);
            this.txtCardNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCardNo_KeyDown);
            this.txtCardNo.Enter += new System.EventHandler(this.txtCardNo_Enter);
            this.cmbRegLevel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbRegLevel_KeyDown);
            //this.cmbRegLevel.Enter += new System.EventHandler(this.cmbRegLevel_Enter);
            this.dtBirthday.KeyDown += new KeyEventHandler(dtBirthday_KeyDown);

            this.cmbPatientType.KeyDown += new KeyEventHandler(cmbPatientType_KeyDown);

            this.cmbBookingType.KeyDown += new KeyEventHandler(cmbBookingType_KeyDown);
        }

        void cmbPatientType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PageUp)
            {
                //反相跳转
                this.setPriorControlFocus();
            }
            else
            {
                this.setNextControlFocus();
            }
        }

        void cmbBookingType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtCardNo.Focus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.llPd_Click(new object(), new EventArgs());
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //反相跳转
                this.setPriorControlFocus();
            }
        }

        #region 变量
        #region 管理类
        /// <summary>
        /// 常数管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager conMgr = new FS.HISFC.BizProcess.Integrate.Manager();
        private FS.HISFC.BizLogic.Fee.Account account = new FS.HISFC.BizLogic.Fee.Account();
        /// <summary>
        /// 挂号员权限类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Permission permissMgr = new FS.HISFC.BizLogic.Registration.Permission();
        /// <summary>
        /// 参数控制类
        /// </summary>
        private FS.FrameWork.Management.ControlParam ctlMgr = new FS.FrameWork.Management.ControlParam();
        /// <summary>
        /// 排班管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Schema SchemaMgr = new FS.HISFC.BizLogic.Registration.Schema();
        /// <summary>
        /// 患者管理类
        /// </summary>
        //private FS.HISFC.BizProcess.Integrate.RADT patientMgr = new FS.HISFC.BizProcess.Integrate.RADT();
        private FS.HISFC.BizProcess.Integrate.RADT patientMgr = new FS.HISFC.BizProcess.Integrate.RADT();
        /// <summary>
        /// 挂号管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();
        /// <summary>
        /// 合同单位管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeMgr = new FS.HISFC.BizProcess.Integrate.Fee();
        /// <summary>
        /// 挂号级别管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.RegLevel RegLevelMgr = new FS.HISFC.BizLogic.Registration.RegLevel();
        /// <summary>
        /// 预约管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Booking bookingMgr = new FS.HISFC.BizLogic.Registration.Booking();
        /// <summary>
        /// 预约订单管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Appointment appointmentMgr = new FS.HISFC.BizLogic.Registration.Appointment();
        /// <summary>
        /// 就诊卡管理类
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();
        /// <summary>
        /// 挂号费管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.RegLvlFee regFeeMgr = new FS.HISFC.BizLogic.Registration.RegLvlFee();
        /// <summary>
        /// 系统参数控制
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParma = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// 医保接口
        /// </summary>
        FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy interfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();

        /// <summary>
        /// 护士分诊信息
        /// </summary>
        //private FS.HISFC.BizProcess.Integrate. assMgr = new FS.HISFC.BizLogic.Nurse.Assign();
        /// <summary>
        /// 医保接口类
        /// </summary>
        //private MedicareInterface.Class.Clinic SIMgr = new MedicareInterface.Class.Clinic();
        //private FS.HISFC.BizLogic.Fee.Interface InterfaceMgr = new FS.HISFC.BizLogic.Fee.Interface();
        ////
        #endregion

        /// <summary>
        /// 挂号界面默认的中文输入法
        /// </summary>
        private InputLanguage CHInput = null;
        //// <summary>
        //// 挂号票是否按发票管理
        //// </summary>
        //private bool IsGetInvoice = false;
        /// <summary>
        /// 挂号实体
        /// </summary>
        private FS.HISFC.Models.Registration.Register regObj;
        /// <summary>
        /// 门诊科室列表
        /// </summary>
        private ArrayList alDept = new ArrayList();
        /// <summary>
        /// 急诊科室
        /// </summary>
        private ArrayList alEmergDept = new ArrayList();
        /// <summary>
        /// 允许挂号员挂号的科室
        /// </summary>
        private ArrayList alAllowedDept = new ArrayList();
        /// <summary>
        /// 允许挂号员挂号的科室
        /// </summary>
        private ArrayList alAllowedEmergDept = new ArrayList();
        /// <summary>
        /// 医生列表
        /// </summary>
        private ArrayList alDoct = new ArrayList();
        /// <summary>
        /// 午别
        /// </summary>
        private ArrayList alNoon = new ArrayList();
        /// <summary>
        /// 是否触发SelectedIndexChanged事件
        /// </summary>
        private bool IsTriggerSelectedIndexChanged = true;
        private bool isBirthdayEnd = true;

        /// <summary>
        /// 是否显示账户余额（医保等患者信息） {54603DD0-3484-4dba-B88A-B89F2F59EA40}
        /// </summary>
        private bool isShowSIBalanceCost = true;

        /// <summary>
        /// 是否处理老人的普通挂号费
        /// </summary>
        private bool isDealPTreg = false;

        /// <summary>
        /// 多少岁以上老人挂号减免，默认65
        /// </summary>
        private int dealPTregAge = 65;

        /// <summary>
        /// 需要处理老人的挂号级别
        /// </summary>
        private string strDealPTregLevels = string.Empty;

        /// <summary>
        /// 外屏接口
        /// </summary>
        protected FS.HISFC.BizProcess.Interface.FeeInterface.IMultiScreen iMultiScreen = null;

        #region 参数
        /// <summary>
        /// 默认显示的合同单位代码
        /// </summary>
        private string DefaultPactID = "";
        /// <summary>
        /// 公费患者允许日挂号限额
        /// </summary>
        private int DayRegNumOfPub = 10;
        /// <summary>
        /// 诊金是否记帐
        /// </summary>
        private bool IsPubDiagFee = false;
        /// <summary>
        /// 专家号是否先选择科室
        /// </summary>
        private bool isSelectDeptFirst = true;

        /// <summary>
        /// 专家号是否先选择科室
        /// </summary>
        [Category("控件设置"), Description("专家号是否先选择科室")]
        public bool IsSelectDeptFirst
        {
            get
            {
                return isSelectDeptFirst;
            }
            set
            {
                isSelectDeptFirst = value;
            }
        }
        /// <summary>
        /// 挂号是否录入姓名
        /// </summary>
        private bool IsInputName = true;
        //{920686B9-AD51-496e-9240-5A6DA098404E}
        /// <summary>
        /// 医生、科室下拉列表是否显示全院的医生、科室，哈哈，谁能看明白，谁神经病
        /// </summary>
        //private bool ComboxIsListAll = true;
        /// <summary>
        /// 挂号科室显示列数
        /// </summary>
        private int DisplayDeptColumnCnt = 1;
        /// <summary>
        /// 挂号医生显示列数
        /// </summary>
        private int DisplayDoctColumnCnt = 1;
        /// <summary>
        /// 挂号是否允许超出排班限额
        /// </summary>
        private bool IsAllowOverrun = true;
        /// <summary>
        /// 2处方号对操作员连续、1由操作员自己录入处方号
        /// </summary>
        private int GetRecipeType = 1;

        private int GetInvoiceType = 1;
        /// <summary>
        /// 回车是否跳到预言流水号处
        /// </summary>
        private bool IsInputOrder = true;
        /// <summary>
        /// 光标是否跳到预约时间段处
        /// </summary>
        private bool IsInputTime = true;
        /// <summary>
        /// 保存时是否提示
        /// </summary>
        private bool IsPrompt = true;
        /// <summary>
        /// 是否预约号序号排在现场号前面
        /// </summary>
        private bool IsPreFirst = false;

        /// <summary>
        /// “其它费”类型0：空调费1病历本费2：其他费
        /// </summary>
        private string otherFeeType = string.Empty;

        /// <summary>
        /// 专家号是否区分教授级别
        /// </summary>
        private bool IsDivLevel = false;

        /// <summary>
        /// 多张号是否认为是加号
        /// </summary>
        private bool MultIsAppend = true;


        #region 是否ATM补打发票
        private bool ISATMPRINT = false;
        [Category("控件设置"), Description("是否ATM补打发票")]
        public bool SetISATMPRINT
        {
            set
            {
                this.ISATMPRINT = value;
            }
            get
            {
                return this.ISATMPRINT;
            }
        }
        #endregion

        private bool isLevelUnlimited = false;
        /// <summary>
        /// 是否挂号级别与排班级别无限制
        /// </summary>
        [Category("控件设置"), Description("是否挂号级别与排班级别无限制")]
        public bool IsLevelUnlimited
        {
            get { return isLevelUnlimited; }
            set { isLevelUnlimited = value; }
        }


        /// <summary>
        /// 教授列表
        /// </summary>
        private ArrayList alProfessor = new ArrayList();
        #endregion
        /// <summary>
        /// 选择预约时间段
        /// </summary>
        private ucChooseBookingDate ucChooseDate;

        /// <summary>
        /// 医保接口代理
        /// </summary>
        FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy MedcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();
        private bool isReadCard = false;
        /// <summary>
        /// 挂号信息实体：医保借口使用
        /// </summary>
        //FS.HISFC.Models.Registration.Register myYBregObj = new FS.HISFC.Models.Registration.Register ();

        /// <summary>
        /// 是否弹出找零窗口{F0661633-4754-4758-B683-CB0DC983922B}
        /// </summary>
        private bool isShowChangeCostForm = false;
        /// <summary>
        /// 是否显示收取卡费用选择框,
        /// {23BA226E-A1E5-4a0b-A1D5-92FA97AF3E85}
        /// </summary>
        private bool isShowChbCardFee = false;

        /// <summary>
        /// 门诊病历号输入处为空，是否给予提示
        /// </summary>
        private bool isShowTipsWhenCardNoIsNull = true;

        /// <summary>
        /// 是否根据午别来过滤专科排班和专家排班
        /// </summary>
        private bool isFilterSchemeByNoon = false;

        #region 发票
        /// <summary>
        /// 打印接口
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Registration.IRegPrint IRegPrint = null;

        /// <summary>
        /// 查找卡号接口
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Registration.ISearchCardNoFalse ISearchCard = null;
        #endregion

        #region 地址输入接口

        /// <summary>
        /// 补全地址接口
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Registration.ICompletionAddress ICompletionAddress = null;

        #endregion

        #region 挂号费特殊计算接口

        /// <summary>
        /// 挂号费特殊计算接口
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Registration.ICountSpecialRegFee ICountSpecialRegFee = null;

        #endregion

        private DataSet dsItems;
        private DataView dvDepts;
        private DataView dvDocts;

        private ArrayList al = new ArrayList();
        /// <summary>
        /// 提示：是否使用帐户支付
        /// </summary>
        private bool isAccountMessage = true;

        #region 医生焦点属性控制和修改添加医生列表和科室列表{920686B9-AD51-496e-9240-5A6DA098404E}
        /// <summary>
        /// 是否添加所有医生
        /// </summary>
        private bool isAddAllDoct = false;

        /// <summary>
        /// 是否列出所有科室
        /// </summary>
        private bool isAddAllDept = false;

        /// <summary>
        /// 普通号时，医生控件是否获得焦点
        /// </summary>
        private bool isSetDoctFocusForCommon = false;

        /// <summary>
        /// 保存时处理{E43E0363-0B22-4d2a-A56A-455CFB7CF211}
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Registration.IProcessRegiter iProcessRegiter = null;

        #endregion

        #region 账户新增
        ///// <summary>
        ///// 账户是否终端扣费
        ///// </summary>
        //bool isAccountTerimalFee = false;
        #endregion

        /// <summary>
        /// 如果金额为0是否打印发票
        /// </summary>
        private bool isPrintIfZero = true;
        /// <summary>
        /// 合同单位变更需要处理的合同单位ID列表
        /// </summary>
        private List<string> lstPactSelectChange = new List<string>();

        #endregion

        #region 是否启用读卡失败接口
        private bool setCardReadFalse = false;
        [Category("控件设置"), Description("是否启用读卡失败接口")]
        public bool SetCardReadFalse
        {
            set
            {
                this.setCardReadFalse = value;
            }
            get
            {
                return this.setCardReadFalse;
            }
        }
        #endregion

        #region 是否补全地址
        private bool isCompletionAddress = false;
        /// <summary>
        /// 是否启用补全地址
        /// </summary>
        [Category("控件设置"), Description("是否启用补全地址")]
        public bool IsCompletionAddress
        {
            set
            {
                this.isCompletionAddress = value;
            }
            get
            {
                return this.isCompletionAddress;
            }
        }
        private bool isCompleted = false;
        #endregion

        #region 是否启用挂号费特殊处理
        private bool isCountSpecialRegFee = false;
        /// <summary>
        /// 是否启用挂号费特殊处理
        /// </summary>
        [Category("控制设置"), Description("是否启用挂号费特殊处理")]
        public bool IsCountSpecialRegFee
        {
            set
            {
                this.isCountSpecialRegFee = value;
            }
            get
            {
                return this.isCountSpecialRegFee;
            }
        }
        #endregion

        #region 是否限制电话和住址必须输一项

        private EnumLimit isLimit = EnumLimit.Half;
        [Category("控件设置"), Description("是否限制电话和住址必须输一项")]
        public EnumLimit IsLimit
        {
            set
            {
                this.isLimit = value;
            }
            get
            {
                return this.isLimit;
            }
        }

        #endregion

        #region 属性

        /// <summary>
        /// 是否显示加密
        /// </summary>
        [Category("控件设置"), Description("是否显示加密")]
        public bool IsShowEncrpt
        {
            get
            {
                return this.chbEncrpt.Visible;
            }
            set
            {
                this.chbEncrpt.Visible = value;
            }
        }



        /// <summary>
        /// 是否处理老人的普通挂号费
        /// </summary>
        [Category("控件设置"), Description("是否处理老人的普通挂号费")]
        public bool IsDealPTreg
        {
            get
            {
                return this.isDealPTreg;
            }
            set
            {
                this.isDealPTreg = value;
            }
        }

        /// <summary>
        /// 老人挂号减免年龄
        /// </summary>
        [Category("控制设置"), Description("老人挂号减免年龄,默认65")]
        public int DealPTregAge
        {
            get
            {
                return this.dealPTregAge;
            }
            set
            {
                this.dealPTregAge = value;
            }
        }

        /// <summary>
        /// 需要处理老人的挂号级别
        /// </summary>
        [Category("控件设置"), Description("需要处理老人的挂号级别 多个挂号级别请用','分开")]
        public string DealPTregLevels
        {
            get
            {
                return this.strDealPTregLevels;
            }
            set
            {
                this.strDealPTregLevels = value;
            }
        }


        string padLeftChar = "";
        /// <summary>
        /// 自动填充字符
        /// </summary>
        [Category("控件设置"), Description("自动填充字符")]
        public string PadLeftChar
        {
            get
            {
                return this.padLeftChar;
            }
            set
            {
                this.padLeftChar = value;
            }
        }

        /// <summary>
        /// 是否直接打印
        /// </summary>
        private bool isAutoPrint = true;

        /// <summary>
        /// 是否自动打印{D623D221-1472-4dc9-B84C-F3E0F4D0C256}修改注释
        /// </summary>
        [Category("控件设置"), Description("保存后是否自动打印挂号单"), DefaultValue(true)]
        public bool IsAutoPrint
        {
            get
            {
                return this.isAutoPrint;
            }
            set
            {
                this.isAutoPrint = value;
            }
        }

        private bool isPrintSellOnlyMedicalRecord = false;

        /// <summary>
        /// 单独买病历本是否打印挂号发票
        /// </summary>
        [Category("控件设置"), Description("单独买病历本是否打印挂号发票, 默认fasle"), DefaultValue(false)]
        public bool IsPrintSellOnlyMedicalRecord
        {
            set { this.isPrintSellOnlyMedicalRecord = value; }
            get { return this.isPrintSellOnlyMedicalRecord; }
        }

        /// <summary>
        /// 出生日期是否获得焦点{F0661633-4754-4758-B683-CB0DC983922B}
        /// </summary>
        [Category("控件设置"), Description("生日控件是否获得焦点 True:生日控件将获得焦点，年龄控件将不获得焦点；False:生日控件将不获得焦点，年龄控件将获得焦点"), DefaultValue(true)]
        public bool IsBirthdayEnd
        {
            get
            {
                return isBirthdayEnd;
            }
            set
            {
                isBirthdayEnd = value;
            }
        }

        [Category("控件设置"), Description("提示：是否使用帐户支付True:提示,False:不提示扣取帐户")]
        public bool IsAccountMessage
        {
            get
            {
                return isAccountMessage;
            }
            set
            {
                isAccountMessage = value;
            }
        }

        // {54603DD0-3484-4dba-B88A-B89F2F59EA40}
        [Category("控件设置"), Description("提示：显示患者账户（医保）余额True:显示,False:不显示")]
        public bool IsShowSIBalanceCost
        {
            get
            {
                return this.isShowSIBalanceCost;
            }
            set
            {
                this.isShowSIBalanceCost = value;
                this.lblSIBalanceTEXT.Visible = value;
                this.tbSIBalanceCost.Visible = value;
            }
        }
        #region 医生焦点属性控制和修改添加医生列表和科室列表{920686B9-AD51-496e-9240-5A6DA098404E}
        /// <summary>
        /// 挂号医生是否随着科室变化
        /// </summary>
        [Category("控件设置"), Description("是否添加全院医生，True:添加全院医生，选择科室时医生列表不跟着变化,False:变化"), DefaultValue(true)]
        public bool IsAddAllDoct
        {
            get { return isAddAllDoct; }
            set { isAddAllDoct = value; }
        }

        /// <summary>
        /// 挂号医生是否随着科室变化
        /// </summary>
        [Category("控件设置"), Description("是否添加全院科室，True:添加,False:只添加挂号科室"), DefaultValue(false)]
        public bool IsAddAllDept
        {
            get { return isAddAllDept; }
            set { isAddAllDept = value; }
        }


        /// <summary>
        /// 普通号时，医生控件是否获得焦点
        /// </summary>
        [Category("控件设置"), Description("普通号时，医生控件是否获得焦点，True:获得,False:不获得"), DefaultValue(false)]
        public bool IsSetDoctFocusForCommon
        {
            get { return isSetDoctFocusForCommon; }
            set { isSetDoctFocusForCommon = value; }
        }
        /// <summary>
        /// 是否显示收取卡费用选择框
        /// {23BA226E-A1E5-4a0b-A1D5-92FA97AF3E85}
        /// </summary>
        [Category("控件设置"), Description("是否允许手功收取诊疗卡工本费,true=允许,此时参数800001,800003需要设置为0,不然会重复收取;false=不允许")]
        public bool IsShowChbCardFee
        {
            get { return isShowChbCardFee; }
            set { isShowChbCardFee = value; }
        }

        /// <summary>
        /// 挂号时控制挂号费、诊金是否收取。
        /// 0=都收，1=收取挂号费，2=收取诊金，3=都不收取 
        /// </summary>
        private int iFeeDiagReg = 0;

        /// <summary>
        /// 挂号的时候，只收挂号费(即使挂号费中维护好了诊查费)
        /// </summary>
        [Category("控件设置"), Description("挂号时控制挂号费、诊金是否收取。 0=都收，1=收取挂号费，2=收取诊金，3=都不收取 ")]
        public int IFeeDiagReg
        {
            set
            {
                this.iFeeDiagReg = value;
            }
            get
            {
                return this.iFeeDiagReg;
            }
        }

        private bool isShowChbBookFee = true;
        /// <summary>
        /// 是否显默认显示病历本√选框
        /// </summary>
        [Category("控件设置"), Description("是否显默认显示病历本√选框")]
        public bool IsShowChbBookFee
        {
            set
            {
                this.isShowChbBookFee = value;
            }
            get
            {
                return this.isShowChbBookFee;
            }
        }
        private bool isShowChbCheckFee = false;
        /// <summary>
        /// 是否显默认显示婴儿体检费√选框
        /// </summary>
        [Category("控件设置"), Description("是否显默认显示婴儿体检费√选框")]
        public bool IsShowChbCheckFee
        {
            set
            {
                this.isShowChbCheckFee = value;
            }
            get
            {
                return this.isShowChbCheckFee;
            }
        }
        private bool isCheckBookFee = true;
        /// <summary>
        /// 是否显默认选择病历本复选框
        /// </summary>
        [Category("控件设置"), Description("是否显默认选择病历本复选框")]
        public bool IsCheckBookFee
        {
            set
            {
                this.isCheckBookFee = value;
                this.chbBookFee.Checked = this.isCheckBookFee;
            }
            get
            {
                return this.isCheckBookFee;
            }
        }
        private bool isShowNoRegFee = false;
        /// <summary>
        /// 是否默认显示免收挂号费
        /// </summary>
        [Category("控件设置"), Description("是否显默认免收挂号费")]
        public bool IsShowNoRegFee
        {
            set
            {
                this.isShowNoRegFee = value;
            }
            get
            {
                return this.isShowNoRegFee;
            }
        }
        private bool isCheckNoRegFee = false;
        /// <summary>
        /// 是否默认选中免收挂号费复选框
        /// </summary>
        [Category("控件设置"), Description("是否默认选中免收挂号费复选框")]
        public bool IsCheckNoRegFee
        {
            set
            {
                this.isCheckNoRegFee = value;
            }
            get
            {
                return this.isCheckNoRegFee;
            }
        }
        private bool isShowNoCheckFee = false;
        /// <summary>
        /// 是否默认显示免诊金
        /// </summary>
        [Category("控件设置"), Description("是否默认显示免诊金")]
        public bool IsShowNoCheckFee
        {
            set
            {
                this.isShowNoCheckFee = value;
            }
            get
            {
                return this.isShowNoCheckFee;
            }
        }
        private bool isCheckNoCheckFee = false;
        /// <summary>
        /// 是否默认选中免诊金复选框
        /// </summary>
        [Category("控件设置"), Description("是否默认选中免诊金复选框")]
        public bool IsCheckNoCheckFee
        {
            set
            {
                this.isCheckNoCheckFee = value;
            }
            get
            {
                return this.isCheckNoCheckFee;
            }
        }


        private bool isSetDefaultRegLev = false;
        /// <summary>
        /// 是否默认选中免诊金复选框
        /// </summary>
        [Category("控件设置"), Description("是否收费后设置默认挂号级别")]
        public bool IsSetDefaultRegLev
        {
            set
            {
                this.isSetDefaultRegLev = value;
            }
            get
            {
                return this.isSetDefaultRegLev;
            }
        }

        private bool isFilterDoc = false;
        /// <summary>
        /// 是否挂号级别过滤医生
        /// </summary>
        [Category("控件设置"), Description("是否挂号级别过滤医生")]
        public bool IsFilterDoc
        {
            set
            {
                this.isFilterDoc = value;
            }
            get
            {
                return this.isFilterDoc;
            }
        }
        #endregion

        /// <summary>
        /// 是否弹出找零窗口{F0661633-4754-4758-B683-CB0DC983922B}
        /// </summary>
        [Category("控件设置"), Description("是否弹出找零窗口"), DefaultValue(false)]
        public bool IsShowChangeCostForm
        {
            get { return isShowChangeCostForm; }
            set { isShowChangeCostForm = value; }
        }

        private bool isShowMiltScreen = true;
        [Category("控件设置"), Description("是否外屏显示"), DefaultValue(true)]
        public bool IsShowMiltScreen
        {
            get { return isShowMiltScreen; }
            set { isShowMiltScreen = value; }
        }

        /// <summary>
        /// 是否显示处方号控件 {63858620-21A6-4080-8520-E5B948C5EE13}
        /// </summary>
        [Category("控件设置"), Description("是否显示处方号控件"), DefaultValue(false)]
        public bool IsShowRecipeNO
        {
            set
            {
                this.label11.Visible = value;
                this.txtRecipeNo.Visible = value;
            }
            get
            {
                return this.txtRecipeNo.Visible && this.label11.Visible;
            }
        }

        /// <summary>
        /// 如果金额为0是否打印发票
        /// </summary>
        [Category("控件设置"), Description("金额为0的时候是否打印发票：true打印；false不打印"), DefaultValue(true)]
        public bool IsPrintIfZero
        {
            get
            {
                return this.isPrintIfZero;
            }
            set
            {
                this.isPrintIfZero = value;
            }
        }

        private bool isReaderIDCard = false;

        private bool isTSAccount = false;

        /// <summary>
        /// 
        /// </summary>
        [Category("控件设置"), Description("是否提示账户支付"), DefaultValue(false)]
        public bool IsTSAccount
        {
            get
            {
                return isTSAccount;
            }
            set
            {
                isTSAccount = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("控件设置"), Description("是否自动读取身份证"), DefaultValue(false)]
        public bool IsReaderIDCard
        {
            get
            {
                return isReaderIDCard;
            }
            set
            {
                isReaderIDCard = value;
            }
        }
        /// <summary>
        /// 合同单变更时需处理的合同单位ID
        /// </summary>
        [Category("控件设置"), Description("合同单位改变时，需要从医保系统获取数据的合同单位ID，多个以|分开")]
        public string PactSelectChange
        {
            get
            {
                if (lstPactSelectChange == null || lstPactSelectChange.Count <= 0)
                {
                    return "";
                }
                else
                {
                    string strPact = "";
                    foreach (string str in lstPactSelectChange)
                    {
                        strPact += str + "|";
                    }

                    return strPact;
                }
            }
            set
            {
                lstPactSelectChange.Clear();
                if (string.IsNullOrEmpty(value))
                {
                    return;
                }
                else
                {
                    lstPactSelectChange.AddRange(value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries));
                }
            }
        }
        //{819FEDB1-9A41-4f41-8D53-C4CB3271CBB3}kjl
        /// <summary>
        /// 是否根据挂号级别加载科室列表
        /// </summary>
        private bool isJudgeReglevl = true;
        /// <summary>
        /// 是否根据挂号级别加载科室列表
        /// </summary>
        [Category("控件设置"), Description("选挂号级别时，是否根据挂号级别加载科室列表"), DefaultValue(true)]
        public bool IsJudgeReglevl
        {
            get
            {
                return this.isJudgeReglevl;
            }
            set
            {
                this.isJudgeReglevl = value;
            }
        }
        /// {7E2D78C8-265E-4b54-AC5B-6DD927DDF81D} Huangd
        /// <summary>
        /// 挂号时间是否取当前操作时间
        /// </summary>
        private bool isCurrRegDate = false;
        /// <summary>
        /// 挂号时间是否取当前操作时间
        /// </summary>
        [Category("控件设置"), Description("挂号时间是否取当前操作时间"), DefaultValue(false)]
        public bool IsCurrRegDate
        {
            get
            {
                return this.isCurrRegDate;
            }
            set
            {
                this.isCurrRegDate = value;
            }
        }

        /// <summary>
        /// 输入流水号后，排班选择事件是否有效
        /// </summary>
        private bool isTriggerEventActive = false;
        /// <summary>
        /// 输入流水号后，排班选择事件是否有效
        /// </summary>
        [Category("控件设置"), Description("输入流水号后，排班选择事件是否有效，默认(false)"), DefaultValue(false)]
        public bool IsTriggerEventActive
        {
            get
            {
                return this.isTriggerEventActive;
            }
            set
            {
                this.isTriggerEventActive = value;
            }
        }

        #region by lijp 2012-09-25 {C108A02B-D1A3-4c0b-B024-67EECA401A6C}
        /// <summary>
        /// 是否显示预约类型
        /// </summary>
        private bool isShowBookingType = false;

        /// <summary>
        /// 是否显示预约类型
        /// </summary>
        [Category("控件设置"), Description("是否显示预约类型"), DefaultValue(false)]
        public bool IsShowBookingType
        {
            get { return isShowBookingType; }
            set { isShowBookingType = value; }
        }
        #endregion

        /// <summary>
        /// 门诊病历号输入处为空，是否给予提示
        /// </summary>
        [Category("控件设置"), Description("门诊病历号输入处为空，是否给予提示，默认(true)"), DefaultValue(true)]
        public bool IsShowTipsWhenCardNoIsNull
        {
            get { return isShowTipsWhenCardNoIsNull; }
            set { isShowTipsWhenCardNoIsNull = value; }
        }

        /// <summary>
        /// 是否根据午别来过滤专科排班和专家排班
        /// </summary>
        [Category("控件设置"), Description("是否根据午别来过滤专科排班和专家排班，默认(false)"), DefaultValue(false)]
        public bool IsFilterSchemeByNoon
        {
            get { return isFilterSchemeByNoon; }
            set { isFilterSchemeByNoon = value; }
        }

        #endregion

        #region 初始化
        private void ucRegister_Load(object sender, EventArgs e)
        {
            if (this.DesignMode) return;
            this.init();
            this.SetRegLevelDefault();
            this.Clear();
            this.initInputMenu();
            this.readInputLanguage();
            this.ChangeRecipe();

            // 新加的
            this.chbCardFee.Visible = this.IsShowChbCardFee;
            this.cmbPayKind.SelectedIndexChanged += new EventHandler(cmbPayKind_SelectedIndexChanged);


            if (Screen.PrimaryScreen.Bounds.Height == 600)
            {
                this.panel5.Height = 29;
            }

            this.LoadPrint();

            this.FindForm().FormClosing += new FormClosingEventHandler(ucRegister_FormClosing);

            //界面显示当前使用的发票号码
            this.InitInvoiceInfo();


        }

        private Dictionary<string, string> dictionaryYKDept = new Dictionary<string, string>();

        private void GetYKdept()
        {
            ArrayList al = CommonController.Instance.QueryConstant("YkDept");
            if (al != null)
            {
                foreach (FS.FrameWork.Models.NeuObject obj in al)
                {
                    dictionaryYKDept[obj.ID] = obj.Name;
                }
            }
        }

        void cmbPayKind_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetCost();
            if (this.regObj == null || string.IsNullOrEmpty(this.regObj.PID.CardNO))
            {
                try
                {
                    this.regObj.Pact.ID = this.cmbPayKind.Tag.ToString();
                    this.regObj.Pact.Name = this.cmbPayKind.Text;
                }
                catch { }
                return;
            }

            //处理合同单位被删除，再重新选择合同单位时未将对象引用实例
            try
            {
                this.regObj.Pact.ID = this.cmbPayKind.Tag.ToString();
                this.regObj.Pact.Name = this.cmbPayKind.Text;
            }
            catch { }

            if (lstPactSelectChange.Contains(this.regObj.Pact.ID))
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                this.interfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                this.interfaceProxy.SetPactCode(this.regObj.Pact.ID);
                if (this.interfaceProxy.Connect() == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.interfaceProxy.Rollback();
                    MessageBox.Show("连接医保出错!" + this.interfaceProxy.ErrMsg);
                    return;
                }

                //获取医保登记信息
                if (this.interfaceProxy.GetRegInfoOutpatient(this.regObj) != 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.interfaceProxy.Rollback();
                    MessageBox.Show(interfaceProxy.ErrMsg);
                    return;
                }

                //断开连接
                if (this.interfaceProxy.Disconnect() != 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.interfaceProxy.Rollback();
                    MessageBox.Show(interfaceProxy.ErrMsg);
                    return;
                }

                FS.FrameWork.Management.PublicTrans.Commit();
                interfaceProxy.Commit();

                // 赋值
                this.txtCardNo.Text = this.regObj.PID.CardNO;
                this.txtName.Text = this.regObj.Name;
                this.cmbSex.Tag = this.regObj.Sex.ID;
                this.txtMcardNo.Text = this.regObj.SSN;
                this.txtPhone.Text = this.regObj.PhoneHome;
                this.txtAddress.Text = this.regObj.AddressHome;
                this.txtIdNO.Text = this.regObj.IDCard;
                if (this.regObj.Birthday != DateTime.MinValue)
                    this.dtBirthday.Value = this.regObj.Birthday;

                this.cmbCardType.Tag = this.regObj.CardType.ID;

                this.setAge(this.regObj.Birthday);
            }


        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void init()
        {
            //FS.neuFC.Interface.Classes.Function.ShowWaitForm("") ;
            //Application.DoEvents() ;
            this.GetParameter();
            this.initDataSet();
            this.setStyle();
            this.initRegLevel();
            this.alDept = this.GetClinicDepts();
            if (this.alDept == null) this.alDept = new ArrayList();

            foreach (FS.FrameWork.Models.NeuObject obj in this.alDept)
            {
                if (obj.Name.Contains("急"))
                {
                    alEmergDept.Add(obj);
                }
            }
            this.InitRegDept();
            this.InitDoct();
            this.initPact();
            this.InitBookingDate();
            this.InitNoon();

            #region by lijp 2012-09-25 {C108A02B-D1A3-4c0b-B024-67EECA401A6C}
            if (isShowBookingType)
            {
                this.cmbBookingType.Visible = true;
                this.cmbBookingType.Enabled = true;
                this.lblBookingType.Visible = true;
                this.InitBookingType();
            }
            else
            {
                this.lblBookingType.Visible = false;
                this.cmbBookingType.Visible = false;
            }
            #endregion

            this.cmbSex.AddItems(FS.HISFC.Models.Base.SexEnumService.List());

            ArrayList alPateintType = this.conMgr.GetConstantList("PersonType");
            if (alPateintType == null)
            {

            }
            cmbPatientType.AddItems(alPateintType);
            if (cmbPatientType.Items.Count > 0)
            {
                cmbPatientType.SelectedIndex = 0;
            }
            else
            {
                cmbPatientType.Text = "";
            }

            this.InitCardType();
            this.Retrieve();
            this.GetRecipeNo(regMgr.Operator.ID);

            //FS.neuFC.Interface.Classes.Function.HideWaitForm() ;

            this.cmbRegLevel.IsFlat = true;
            this.cmbDept.IsFlat = true;
            this.cmbDoctor.IsFlat = true;
            this.cmbPayKind.IsFlat = true;
            this.cmbSex.IsFlat = true;
            this.cmbUnit.IsFlat = true;
            this.cmbCardType.IsFlat = true;
            this.cmbPayKind.IsLike = false;//不允许模糊查询

            this.chbBookFee.Visible = this.IsShowChbBookFee;
            this.chbNoCheckFee.Visible = this.IsShowNoCheckFee;
            this.chbNoRegFee.Visible = this.IsShowNoRegFee;
            this.chbCheckFee.Visible = this.IsShowChbCheckFee;
            //{F3258E87-7BCC-411a-865E-A9843AD2C6DD}
            //为病历本本费时显示
            if (this.otherFeeType == "1" && this.IsShowChbBookFee)
            {
                this.chbBookFee.Visible = true;
            }
            else
            {
                this.chbBookFee.Visible = false;
            }

            if (IsCheckBookFee)
            {
                this.chbBookFee.Checked = true;
            }
            else
            {
                this.chbBookFee.Checked = false;
            }
            //{E43E0363-0B22-4d2a-A56A-455CFB7CF211}
            this.InitInterface();
            //判断是否宜康科室
            GetYKdept();
            if (!dictionaryYKDept.ContainsKey(((FS.HISFC.Models.Base.Employee)(regMgr.Operator)).Dept.ID))
            {
                //界面显示当前使用的发票号码
                ChangeInvoiceNOMessage();
            }

            #region 外屏显示
            if (isShowMiltScreen)
            {
                //外屏接口{67C90AAC-CFAD-4089-96F4-9F9FC82D8754}
                if (Screen.AllScreens.Length > 1)
                {
                    iMultiScreen = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject<FS.HISFC.BizProcess.Interface.FeeInterface.IMultiScreen>(this.GetType());
                    if (iMultiScreen == null)
                    {
                        iMultiScreen = new Forms.frmMiltScreen();

                    }
                    //显示收费员
                    FS.HISFC.Models.Base.Employee currentOperator = account.Operator as FS.HISFC.Models.Base.Employee;
                    System.Collections.Generic.List<Object> lo = new System.Collections.Generic.List<object>();
                    lo.Add("");//患者基本信息
                    lo.Add("");//挂号级别
                    lo.Add("");// 挂号科室
                    lo.Add("");//挂号医生;
                    lo.Add("");//挂号费用
                    lo.Add(currentOperator);//收费员信息（非初始化界面值为空）
                    this.iMultiScreen.ListInfo = lo;

                    iMultiScreen.ShowScreen();
                    this.lbReceive.TextChanged += new EventHandler(lbReceive_TextChanged);
                    this.FindForm().Activated += new EventHandler(ucRegisterNew_Activated);
                    this.FindForm().Deactivate += new EventHandler(ucRegisterNew_Deactivate);
                }
            }

            #endregion
        }

        /// <summary>
        /// by lijp 2012-09-25 {C108A02B-D1A3-4c0b-B024-67EECA401A6C}
        /// 初始化预约类型"BookingType"
        /// </summary>
        private void InitBookingType()
        {
            try
            {
                ArrayList arrBookingType = new ArrayList();
                arrBookingType = this.conMgr.QueryConstantList("BookingType");
                this.cmbBookingType.AddItems(arrBookingType);
            }
            catch
            {
                MessageBox.Show("获取门诊预约挂号类别失败！");
                return;
            }
        }

        #region 设置外屏显示

        private int ShowMultiScreen()
        {
            return 1;
        }

        #endregion

        /// <summary>
        /// 初始化界面的发票信息显示
        /// </summary>
        /// <returns></returns>
        public int InitInvoiceInfo()
        {
            string strInvioceNO = "";
            string strRealInvoiceNO = "";
            string strErrText = "";
            int iRes = 0;
            string strInvoiceType = "R";   //挂号收据

            FS.HISFC.Models.Base.Employee employee = this.regMgr.Operator as FS.HISFC.Models.Base.Employee;

            iRes = this.feeMgr.GetInvoiceNO(employee, strInvoiceType, ref strInvioceNO, ref strRealInvoiceNO, ref strErrText);

            if (iRes == -1)
            {
                MessageBox.Show("获取挂号发票出错!");
                return -1;
            }


            this.tbInvoiceNO.Text = strInvioceNO;
            this.tbRealInvoiceNO.Text = strRealInvoiceNO;

            return 1;
        }

        /// <summary>
        /// init DataSet
        /// </summary>
        private void initDataSet()
        {
            dsItems = new DataSet();
            dsItems.Tables.Add("Dept");
            dsItems.Tables.Add("Doct");

            dsItems.Tables["Dept"].Columns.AddRange(new DataColumn[]
                {
                    new DataColumn("ID",System.Type.GetType("System.String")),
                    new DataColumn("Name",System.Type.GetType("System.String")),
                    new DataColumn("Spell_Code",System.Type.GetType("System.String")),
                    new DataColumn("Wb_code",System.Type.GetType("System.String")),
                    new DataColumn("Input_Code",System.Type.GetType("System.String")),
                    new DataColumn("RegLmt",System.Type.GetType("System.Decimal")),
                    new DataColumn("Reged",System.Type.GetType("System.Decimal")),
                    new DataColumn("TelLmt",System.Type.GetType("System.Decimal")),
                    new DataColumn("Teled",System.Type.GetType("System.Decimal")),
                    new DataColumn("BeginTime",System.Type.GetType("System.DateTime")),
                    new DataColumn("EndTime",System.Type.GetType("System.DateTime")),
                    new DataColumn("Noon",System.Type.GetType("System.String")),
                    new DataColumn("IsAppend",System.Type.GetType("System.Boolean"))
                });

            dsItems.Tables["Doct"].Columns.AddRange(new DataColumn[]
                {
                    new DataColumn("ID",System.Type.GetType("System.String")),
                    new DataColumn("Name",System.Type.GetType("System.String")),
                    new DataColumn("Spell_Code",System.Type.GetType("System.String")),
                    new DataColumn("Wb_code",System.Type.GetType("System.String")),					
                    new DataColumn("RegLmt",System.Type.GetType("System.Decimal")),
                    new DataColumn("Reged",System.Type.GetType("System.Decimal")),
                    new DataColumn("TelLmt",System.Type.GetType("System.Decimal")),
                    new DataColumn("Teled",System.Type.GetType("System.Decimal")),
                    new DataColumn("SpeLmt",System.Type.GetType("System.Decimal")),
                    new DataColumn("Sped",System.Type.GetType("System.Decimal")),
                    new DataColumn("BeginTime",System.Type.GetType("System.DateTime")),
                    new DataColumn("EndTime",System.Type.GetType("System.DateTime")),
                    new DataColumn("Noon",System.Type.GetType("System.String")),
                    new DataColumn("IsAppend",System.Type.GetType("System.Boolean")),
                    new DataColumn("Memo",System.Type.GetType("System.String")),
                    new DataColumn("IsProfessor",System.Type.GetType("System.Boolean"))
                });

            dsItems.CaseSensitive = false;

            dvDepts = new DataView(dsItems.Tables["Dept"]);
            dvDocts = new DataView(dsItems.Tables["Doct"]);
        }
        /// <summary>
        /// 设置farpoint的格式
        /// </summary>
        private void setStyle()
        {
            FarPoint.Win.Spread.CellType.TextCellType txt = new FarPoint.Win.Spread.CellType.TextCellType();

            #region 挂号级别
            //参数设置挂号级别显示几列
            string colCount = this.ctlMgr.QueryControlerInfo("400001");
            //没有默认显示一列
            if (colCount == null || colCount == "-1" || colCount == "")
                colCount = "1";


            this.fpRegLevel.ColumnCount = int.Parse(colCount) * 2;
            int width = /*this.fpSpread1.Width*/500 * 2 / this.fpRegLevel.ColumnCount;
            //设置列
            for (int i = 0; i < this.fpRegLevel.ColumnCount; i++)
            {
                if (i % 2 == 0)
                {
                    this.fpRegLevel.ColumnHeader.Cells[0, i].Text = "代码";
                    this.fpRegLevel.Columns[i].Width = width / 3;
                    this.fpRegLevel.Columns[i].BackColor = Color.Linen;
                    this.fpRegLevel.Columns[i].CellType = txt;
                }
                else
                {
                    this.fpRegLevel.ColumnHeader.Cells[0, i].Text = "挂号级别名称";
                    this.fpRegLevel.Columns[i].Width = width * 2 / 3;
                }
            }

            this.fpRegLevel.GrayAreaBackColor = System.Drawing.SystemColors.Window;
            this.fpRegLevel.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpRegLevel.RowHeader.Visible = false;
            this.fpRegLevel.RowCount = 0;
            #endregion

            #region 结算类别
            colCount = this.ctlMgr.QueryControlerInfo("400003");
            if (colCount == null || colCount == "-1" || colCount == "") colCount = "1";

            this.fpPayKind.ColumnCount = int.Parse(colCount) * 2;
            width = /*this.fpSpread1.Width*/500 * 2 / this.fpPayKind.ColumnCount;

            FarPoint.Win.Spread.CellType.TextCellType txtType = new FarPoint.Win.Spread.CellType.TextCellType();
            txtType.StringTrim = System.Drawing.StringTrimming.EllipsisCharacter;

            //设置列
            for (int i = 0; i < this.fpPayKind.ColumnCount; i++)
            {
                if (i % 2 == 0)
                {
                    this.fpPayKind.ColumnHeader.Cells[0, i].Text = "代码";
                    this.fpPayKind.Columns[i].Width = width / 3;
                    this.fpPayKind.Columns[i].BackColor = Color.Linen;
                    this.fpPayKind.Columns[i].CellType = txt;
                }
                else
                {
                    this.fpPayKind.ColumnHeader.Cells[0, i].Text = "类别名称";
                    this.fpPayKind.Columns[i].Width = width * 2 / 3;
                    this.fpPayKind.Columns[i].CellType = txtType;
                }
            }

            this.fpPayKind.GrayAreaBackColor = SystemColors.Window;
            this.fpPayKind.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpPayKind.RowHeader.Visible = false;
            this.fpPayKind.RowCount = 0;
            #endregion

            #region 患者挂号信息
            this.fpList.ColumnHeader.Cells[0, 0].Text = "就诊卡号";
            this.fpList.Columns[0].Width = 100F;
            this.fpList.Columns[0].AllowAutoSort = true;
            this.fpList.Columns[0].CellType = txt;
            this.fpList.ColumnHeader.Cells[0, 1].Text = "姓名";
            this.fpList.Columns[1].Width = 90F;
            this.fpList.Columns[1].AllowAutoSort = true;
            this.fpList.ColumnHeader.Cells[0, 2].Text = "结算类别";
            this.fpList.Columns[2].Width = 90F;
            this.fpList.Columns[2].AllowAutoSort = true;
            this.fpList.ColumnHeader.Cells[0, 3].Text = "出生年月";
            this.fpList.Columns[3].Width = 100F;
            this.fpList.Columns[3].AllowAutoSort = true;
            this.fpList.ColumnHeader.Cells[0, 4].Text = "年龄";
            this.fpList.Columns[4].Width = 70F;
            this.fpList.Columns[4].AllowAutoSort = true;
            this.fpList.ColumnHeader.Cells[0, 5].Text = "挂号级别";
            this.fpList.Columns[5].Width = 80F;
            this.fpList.Columns[5].AllowAutoSort = true;
            this.fpList.ColumnHeader.Cells[0, 6].Text = "挂号科室";
            this.fpList.Columns[6].Width = 80F;
            this.fpList.Columns[6].AllowAutoSort = true;
            this.fpList.ColumnHeader.Cells[0, 7].Text = "看诊医生";
            this.fpList.Columns[7].Width = 78F;
            this.fpList.Columns[7].AllowAutoSort = true;
            this.fpList.ColumnHeader.Cells[0, 8].Text = "序号";
            this.fpList.Columns[8].Width = 40;
            this.fpList.Columns[8].AllowAutoSort = true;
            this.fpList.ColumnHeader.Cells[0, 9].Text = "挂号费(自费总额)";
            this.fpList.Columns[9].Width = 120;
            this.fpList.Columns[9].AllowAutoSort = true;
            this.fpList.ColumnHeader.Cells[0, 10].Text = "记帐诊金金额";
            this.fpList.Columns[10].Width = 80;
            this.fpList.Columns[10].AllowAutoSort = true;
            this.fpList.ColumnHeader.Cells[0, 11].Text = "票据电脑号";
            this.fpList.Columns[11].Width = 100;
            this.fpList.Columns[11].AllowAutoSort = true;
            this.fpList.Columns.Count = 12;

            this.fpList.GrayAreaBackColor = SystemColors.Window;
            this.fpList.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpList.RowCount = 0;
            #endregion

            //初始不显示排班科室
            this.SetDeptFpStyle(false);

            this.SetDoctFpStyle(false);
        }
        /// <summary>
        /// 设置科室列表显示的格式
        /// </summary>
        /// <param name="IsDisplaySchema"></param>
        private void SetDeptFpStyle(bool IsDisplaySchema)
        {
            //显示专科排班科室,显示代码、科室名称、午别、时间段、挂号限额、已挂数量、预约限额、预约已挂
            this.fpDept.Reset();
            this.fpDept.SheetName = "挂号科室";

            FarPoint.Win.Spread.CellType.TextCellType txt = new FarPoint.Win.Spread.CellType.TextCellType();

            if (IsDisplaySchema)
            {
                this.fpDept.ColumnCount = 7;
                this.fpDept.ColumnHeader.Cells[0, 0].Text = "代码";
                this.fpDept.ColumnHeader.Columns[0].Width = 45;
                this.fpDept.Columns[0].CellType = txt;
                this.fpDept.ColumnHeader.Cells[0, 1].Text = "科室名称";
                this.fpDept.ColumnHeader.Columns[1].Width = 95;
                this.fpDept.ColumnHeader.Cells[0, 2].Text = "出诊时间";
                this.fpDept.ColumnHeader.Columns[2].Width = 120;
                this.fpDept.ColumnHeader.Cells[0, 3].Text = "挂号限额";
                this.fpDept.Columns[3].ForeColor = Color.Red;
                this.fpDept.Columns[3].Font = new Font("宋体", 10, FontStyle.Bold);
                this.fpDept.ColumnHeader.Cells[0, 4].Text = "已挂号数";
                this.fpDept.ColumnHeader.Cells[0, 5].Text = "预约限额";
                this.fpDept.Columns[5].ForeColor = Color.Blue;
                this.fpDept.Columns[5].Font = new Font("宋体", 10, FontStyle.Bold);
                this.fpDept.ColumnHeader.Cells[0, 6].Text = "预约已挂";
            }
            else//对于专家、特诊和没有排班的科室,只显示代码和名称
            {
                this.fpDept.ColumnCount = this.DisplayDeptColumnCnt * 2;
                int width = /*this.fpSpread1.Width*/500 * 2 / this.fpDept.ColumnCount;

                //设置列
                for (int i = 0; i < this.fpDept.ColumnCount; i++)
                {
                    if (i % 2 == 0)
                    {
                        this.fpDept.ColumnHeader.Cells[0, i].Text = "代码";
                        this.fpDept.Columns[i].Width = width / 3;
                        this.fpDept.Columns[i].BackColor = Color.Linen;
                        this.fpDept.Columns[i].CellType = txt;
                    }
                    else
                    {
                        this.fpDept.ColumnHeader.Cells[0, i].Text = "科室名称";
                        this.fpDept.Columns[i].Width = width * 2 / 3;
                    }
                }
            }
            this.fpDept.GrayAreaBackColor = SystemColors.Window;
            this.fpDept.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpDept.RowHeader.Visible = false;
            this.fpDept.RowCount = 0;
        }
        /// <summary>
        /// 设置医生列表显示的格式
        /// </summary>
        /// <param name="IsDisplaySchema"></param>
        private void SetDoctFpStyle(bool IsDisplaySchema)
        {
            this.fpDoctor.Reset();
            this.fpDoctor.SheetName = "出诊教授";

            FarPoint.Win.Spread.CellType.TextCellType txt = new FarPoint.Win.Spread.CellType.TextCellType();

            if (IsDisplaySchema)
            {
                this.fpDoctor.ColumnCount = 10;
                this.fpDoctor.ColumnHeader.Rows[0].Height = 30;

                this.fpDoctor.ColumnHeader.Cells[0, 0].Text = "代码";
                this.fpDoctor.ColumnHeader.Columns[0].Width = 40;
                this.fpDoctor.Columns[0].CellType = txt;
                this.fpDoctor.ColumnHeader.Cells[0, 1].Text = "专家名称";
                this.fpDoctor.ColumnHeader.Columns[1].Width = 60;
                this.fpDoctor.ColumnHeader.Cells[0, 2].Text = "出诊时间";
                this.fpDoctor.ColumnHeader.Columns[2].Width = 120;
                this.fpDoctor.ColumnHeader.Cells[0, 3].Text = "挂号限额";
                this.fpDoctor.ColumnHeader.Columns[3].Width = 35;
                this.fpDoctor.Columns[3].ForeColor = Color.Red;
                this.fpDoctor.Columns[3].Font = new Font("宋体", 10, FontStyle.Bold);
                this.fpDoctor.ColumnHeader.Cells[0, 4].Text = "剩余号数";
                this.fpDoctor.ColumnHeader.Columns[4].Width = 35;
                this.fpDoctor.ColumnHeader.Cells[0, 5].Text = "预约限额";
                this.fpDoctor.ColumnHeader.Columns[5].Width = 35;
                this.fpDoctor.Columns[5].ForeColor = Color.Blue;
                this.fpDoctor.Columns[5].Font = new Font("宋体", 10, FontStyle.Bold);
                this.fpDoctor.ColumnHeader.Cells[0, 6].Text = "已预约数";
                this.fpDoctor.ColumnHeader.Columns[6].Width = 35;
                this.fpDoctor.ColumnHeader.Cells[0, 7].Text = "特诊限额";
                this.fpDoctor.ColumnHeader.Columns[7].Width = 35;
                this.fpDoctor.Columns[7].ForeColor = Color.Magenta;
                this.fpDoctor.Columns[7].Font = new Font("宋体", 10, FontStyle.Bold);
                this.fpDoctor.ColumnHeader.Cells[0, 8].Text = "特诊已挂";
                this.fpDoctor.ColumnHeader.Columns[8].Width = 35;
                this.fpDoctor.ColumnHeader.Cells[0, 9].Text = "专长";
                this.fpDoctor.ColumnHeader.Columns[9].Width = 100;
            }
            else
            {
                this.fpDoctor.ColumnCount = this.DisplayDoctColumnCnt * 2;
                int width = /*this.fpSpread1.Width*/500 * 2 / this.fpDoctor.ColumnCount;

                //设置列
                for (int i = 0; i < this.fpDoctor.ColumnCount; i++)
                {
                    if (i % 2 == 0)
                    {
                        this.fpDoctor.ColumnHeader.Cells[0, i].Text = "代码";
                        this.fpDoctor.Columns[i].Width = width / 3;
                        this.fpDoctor.Columns[i].BackColor = Color.Linen;
                        this.fpDoctor.Columns[i].CellType = txt;
                    }
                    else
                    {
                        this.fpDoctor.ColumnHeader.Cells[0, i].Text = "教授名称";
                        this.fpDoctor.Columns[i].Width = width * 2 / 3;
                    }
                }
            }
            this.fpDoctor.GrayAreaBackColor = SystemColors.Window;
            this.fpDoctor.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpDoctor.RowHeader.Visible = false;
            this.fpDoctor.RowCount = 0;
        }
        /// <summary>
        /// 获取参数设置
        /// </summary>
        private void GetParameter()
        {
            //默认显示合同单位
            this.DefaultPactID = this.ctlMgr.QueryControlerInfo("400005");
            if (DefaultPactID == null || DefaultPactID == "-1") DefaultPactID = "";
            //公费患者挂号日限
            string rtn = this.ctlMgr.QueryControlerInfo("400007");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "10";

            this.DayRegNumOfPub = int.Parse(rtn);
            //诊金是否报销
            rtn = this.ctlMgr.QueryControlerInfo("400008");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "0";
            this.IsPubDiagFee = FS.FrameWork.Function.NConvert.ToBoolean(rtn);
            //专家号是否选择科室
            //rtn = this.ctlMgr.QueryControlerInfo("400010");
            //if (rtn == null || rtn == "-1" || rtn == "") rtn = "0";
            //this.isSelectDeptFirst = FS.FrameWork.Function.NConvert.ToBoolean(rtn);
            //			//挂专科号是否只显示出诊专科
            //			rtn = this.ctlMgr.QueryControlerInfo("400011") ;
            //			if( rtn == null || rtn == "-1" || rtn == "") rtn = "0" ;
            //			this.IsDisplaySchemaDept = FS.neuFC.Function.NConvert.ToBoolean(rtn) ;
            //挂号是否允许超出排班限额
            rtn = this.ctlMgr.QueryControlerInfo("400015");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "1";
            this.IsAllowOverrun = FS.FrameWork.Function.NConvert.ToBoolean(rtn);
            //挂号科室显示列数
            rtn = this.ctlMgr.QueryControlerInfo("400002");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "1";
            this.DisplayDeptColumnCnt = int.Parse(rtn);
            //挂号医生显示列数
            rtn = this.ctlMgr.QueryControlerInfo("400004");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "1";
            this.DisplayDoctColumnCnt = int.Parse(rtn);
            //打印收据?
            //			rtn = this.ctlMgr.QueryControlerInfo("400017");
            //			if( rtn == null || rtn == "-1" || rtn == "") rtn = "Invoice" ;
            //			this.PrintWhat = rtn ;

            //获取处方号类型（1物理票号,2电脑票号－－挂号收据号,3电脑票号－－门诊收据号）
            rtn = this.ctlMgr.QueryControlerInfo("400019");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "1";
            this.GetRecipeType = int.Parse(rtn);


            //获取光标是否跳到预约流水号处
            rtn = this.ctlMgr.QueryControlerInfo("400020");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "1";
            this.IsInputOrder = FS.FrameWork.Function.NConvert.ToBoolean(rtn);

            //光标是否跳到预约时间段处
            rtn = this.ctlMgr.QueryControlerInfo("400023");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "1";
            this.IsInputTime = FS.FrameWork.Function.NConvert.ToBoolean(rtn);

            //保存时是否提示
            rtn = this.ctlMgr.QueryControlerInfo("400024");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "1";
            this.IsPrompt = FS.FrameWork.Function.NConvert.ToBoolean(rtn);

            ///是否预约号看诊序号排在现场号前面别
            rtn = this.ctlMgr.QueryControlerInfo("400026");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "0";
            this.IsPreFirst = FS.FrameWork.Function.NConvert.ToBoolean(rtn);

            ///其它费类型0：空调费1病历本费2：其他费
            rtn = this.ctlMgr.QueryControlerInfo("400027");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "1";
            //{F3258E87-7BCC-411a-865E-A9843AD2C6DD}
            //this.IsKTF = FS.FrameWork.Function.NConvert.ToBoolean(rtn);
            this.otherFeeType = rtn;

            //专家号是否区分教授级别
            rtn = this.ctlMgr.QueryControlerInfo("400028");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "0";
            this.IsDivLevel = FS.FrameWork.Function.NConvert.ToBoolean(rtn);

            if (this.IsDivLevel)
            {
                this.alProfessor = this.conMgr.QueryConstantList("Professor");
            }

            //多张号第二张是否当做加号
            rtn = this.ctlMgr.QueryControlerInfo("400029");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "1";
            this.MultIsAppend = FS.FrameWork.Function.NConvert.ToBoolean(rtn);

        }
        /// <summary>
        /// 不允许使用直接收费生成的号再进行挂号
        /// </summary>
        /// <param name="CardNO"></param>
        /// <returns></returns>
        private int ValidCardNO(string CardNO)
        {
            FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParams = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

            string cardRule = controlParams.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.NO_REG_CARD_RULES, false, "9");
            if (CardNO != "" && CardNO != string.Empty)
            {
                if (CardNO.Substring(0, 1) == cardRule && CardNO.Length == 10)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("此号段为直接收费使用，请选择其它号段"), FS.FrameWork.Management.Language.Msg("提示"));
                    return -1;
                }
            }
            return 1;
        }

        #region regLevel
        /// <summary>
        /// 初始化挂号级别
        /// </summary>
        /// <returns></returns>
        private int initRegLevel()
        {
            al = this.getRegLevelFromXML();
            if (al == null) return -1;

            ///如果本地没有配置,从数据库中读取 
            if (al.Count == 0)
            {
                al = this.RegLevelMgr.Query(true);
            }

            if (al == null)
            {
                MessageBox.Show("查询挂号级别出错!" + this.RegLevelMgr.Err, "提示");
                return -1;
            }
            else
            {
                foreach (FS.HISFC.Models.Registration.RegLevel lev in al)
                {
                    lev.SpellCode = FrameWork.Public.String.GetSpell(lev.Name);
                }
            }

            this.AddRegLevelToFp(al);
            this.AddRegLevelToCombox(al);
            return 0;
        }

        /// <summary>
        /// 从本地读取挂号级别,权限控制
        /// </summary>
        /// <returns></returns>
        private ArrayList getRegLevelFromXML()
        {
            ArrayList alLists = new ArrayList();
            XmlDocument doc = new XmlDocument();

            try
            {
                doc.Load(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/RegLevelList.xml");
            }
            catch { return alLists; }


            try
            {
                XmlNodeList nodes = doc.SelectNodes(@"//Level");

                foreach (XmlNode node in nodes)
                {
                    FS.HISFC.Models.Registration.RegLevel level = new FS.HISFC.Models.Registration.RegLevel();
                    level.ID = node.Attributes["ID"].Value;//
                    level.Name = node.Attributes["Name"].Value;
                    level.IsExpert = FS.FrameWork.Function.NConvert.ToBoolean(node.Attributes["IsExpert"].Value);
                    level.IsFaculty = FS.FrameWork.Function.NConvert.ToBoolean(node.Attributes["IsFaculty"].Value);
                    level.IsSpecial = FS.FrameWork.Function.NConvert.ToBoolean(node.Attributes["IsSpecial"].Value);
                    level.IsDefault = FS.FrameWork.Function.NConvert.ToBoolean(node.Attributes["IsDefault"].Value);

                    alLists.Add(level);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("获取挂号级别出错!" + e.Message);
                return null;
            }

            return alLists;
        }
        /// <summary>
        /// 将挂号级别添加到FarPoint列表中
        /// </summary>
        /// <param name="regLevels"></param>
        /// <returns></returns>
        private int AddRegLevelToFp(ArrayList regLevels)
        {
            int count = 0, row = 0, colCount = 0;

            colCount = this.fpRegLevel.ColumnCount / 2;

            if (this.fpRegLevel.RowCount > 0)
                this.fpRegLevel.Rows.Remove(0, this.fpRegLevel.RowCount);

            foreach (FS.FrameWork.Models.NeuObject obj in regLevels)
            {
                if (count % colCount == 0)
                {
                    this.fpRegLevel.Rows.Add(this.fpRegLevel.RowCount, 1);
                    row = this.fpRegLevel.RowCount - 1;
                }

                this.fpRegLevel.SetValue(row, 2 * (count % colCount), obj.ID, false);
                this.fpRegLevel.SetValue(row, 2 * (count % colCount) + 1, obj.Name, false);

                count++;
            }

            return 0;
        }
        /// <summary>
        /// 将挂号级别添加到Combox中
        /// </summary>
        /// <param name="regLevels"></param>
        /// <returns></returns>
        private int AddRegLevelToCombox(ArrayList regLevels)
        {
            //添加到下拉列表
            this.cmbRegLevel.AddItems(al);

            return 0;
        }

        #endregion

        #region dept
        /// <summary>
        /// 获取所有门诊科室
        /// </summary>
        /// <returns></returns>
        private ArrayList GetClinicDepts()
        {
            al = this.conMgr.QueryRegDepartment();
            if (al == null)
            {
                MessageBox.Show("获取门诊科室时出错!" + this.conMgr.Err, "提示");
                return null;
            }

            return al;
        }
        /// <summary>
        /// 获取操作员挂号科室
        /// </summary>
        private int InitRegDept()
        {
            //获取允许操作员挂号的科室列表
            this.alAllowedDept = this.GetAllowedDepts();

            //出错
            if (alAllowedDept == null)
            {
                this.alAllowedDept = new ArrayList();
                return -1;
            }

            foreach (FS.FrameWork.Models.NeuObject obj in alAllowedDept)
            {
                if (obj.Name.Contains("急"))
                {
                    alAllowedEmergDept.Add(obj);
                }
            }

            //添加到DataSet中
            this.AddAllowedDeptToDataSet(this.alAllowedDept);

            //没有维护操作员对应的挂号科室,默认可挂所有门诊科室
            if (alAllowedDept.Count == 0)
            {
                this.AddClinicDeptsToDataSet(this.alDept);
            }

            //将dataset添加到farpoint
            this.addRegDeptToFp(false);
            //将dataset添加到combox
            this.addRegDeptToCombox();

            return 0;
        }
        /// <summary>
        /// 获取允许操作员挂号的科室列表
        /// </summary>
        /// <returns></returns>
        private ArrayList GetAllowedDepts()
        {
            al = this.permissMgr.Query((FS.FrameWork.Models.NeuObject)this.regMgr.Operator);
            if (al == null)
            {
                MessageBox.Show("获取操作员挂号科室时出错!" + this.permissMgr.Err, "提示");
                return null;
            }

            //{8AB04EE1-0A7B-45f9-A897-8CD01CE29ED1}

            if (al.Count > 0)
            {
                FS.FrameWork.Models.NeuObject obj = al[0] as FS.FrameWork.Models.NeuObject;
                if (obj.Memo == "0") //排除法
                {
                    al = this.permissMgr.QueryOutContain((FS.FrameWork.Models.NeuObject)this.regMgr.Operator);
                    if (al == null)
                    {
                        MessageBox.Show("获取操作员挂号科室时出错(排除)!" + this.permissMgr.Err, "提示");
                        return null;
                    }
                }

            }

            return al;
        }
        /// 将允许操作员挂号的科室添加到DataSet
        /// </summary>
        /// <param name="allowedDepts"></param>
        private void AddAllowedDeptToDataSet(ArrayList allowedDepts)
        {
            this.dsItems.Tables[0].Rows.Clear();

            //允许挂号科室数组返回的是neuobject实体
            foreach (FS.FrameWork.Models.NeuObject obj in allowedDepts)
            {
                //先转换为Deptartment 实体,
                FS.HISFC.Models.Base.Department dept;
                //根据代码检索实体
                dept = this.GetDeptByID(obj.User01);
                //将实体添加到DataSet中
                if (dept != null)
                    this.addDeptToDataSet(dept);
            }
        }
        /// <summary>
        /// 查找科室-根据科室代码
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Base.Department GetDeptByID(string ID)
        {
            #region no used
            //			IEnumerator index=this.alDept.GetEnumerator();
            //			
            //			while(index.MoveNext())
            //			{
            //				if((index.Current as FS.HISFC.Models.Base.Department).ID==ID)
            //					return (index.Current;
            //			}
            //			return null;
            #endregion

            foreach (FS.HISFC.Models.Base.Department obj in this.alDept)
            {
                if (obj.ID == ID)
                    return obj;
            }
            return null;
        }
        /// <summary>
        /// Add deptartment to DataSet,可以实现动态过滤功能
        /// </summary>
        /// <param name="dept"></param>
        private void addDeptToDataSet(FS.HISFC.Models.Base.Department dept)
        {
            dsItems.Tables["Dept"].Rows.Add(new object[]
                {
                    dept.ID,
                    dept.Name,
                    dept.SpellCode,
                    dept.WBCode,
                    dept.UserCode,
                    0,
                    0,
                    0,
                    0,
                    DateTime.MinValue,
                    DateTime.MinValue,
                    "",
                    false});
        }

        /// <summary>
        /// 将门诊科室添加到Dataset
        /// </summary>
        /// <param name="depts"></param>
        private void AddClinicDeptsToDataSet(ArrayList depts)
        {
            this.dsItems.Tables[0].Rows.Clear();

            foreach (FS.HISFC.Models.Base.Department dept in depts)
            {
                this.addDeptToDataSet(dept);
            }
        }
        /// <summary>
        /// 生成挂号科室列表-FarPoint
        /// </summary>
        /// <returns></returns>
        private int addRegDeptToFp(bool IsDisplaySchema)
        {
            //添加到farpoint
            if (this.fpDept.RowCount > 0)
                this.fpDept.Rows.Remove(0, this.fpDept.RowCount);

            DataRowView dataRow;

            //DateTime current = this.regMgr.GetDateTimeFromSysDateTime();
            //string noonCode = this.getNoon(current);

            if (IsDisplaySchema)
            {
                int i = 0;
                for (int s = 0; s < dvDepts.Count; s++)
                {
                    dataRow = dvDepts[i];

                    //if (dataRow[2].Equals(noonCode) == false && FS.FrameWork.Function.NConvert.ToDateTime(dataRow[3]).Date == current.Date)
                    //{
                    //    continue;
                    //}
                    this.fpDept.Rows.Add(this.fpDept.RowCount, 1);
                    i = this.fpDept.RowCount - 1;
                    this.fpDept.SetValue(i, 0, dataRow["ID"], false);
                    this.fpDept.SetValue(i, 1, dataRow["Name"], false);

                    if (dataRow["IsAppend"].ToString().ToUpper() == "TRUE")//加号
                    {
                        this.fpDept.SetValue(i, 2, this.getNoon(dataRow["Noon"].ToString()) + "[加号]", false);
                    }
                    else
                    {
                        this.fpDept.SetValue(i, 2, this.getNoon(dataRow["Noon"].ToString()) +
                            "[" + DateTime.Parse(dataRow["BeginTime"].ToString()).ToString("HH:mm") + "～" +
                            DateTime.Parse(dataRow["EndTime"].ToString()).ToString("HH:mm") + "]", false);
                    }

                    this.fpDept.SetValue(i, 3, dataRow["RegLmt"], false);
                    this.fpDept.SetValue(i, 4, dataRow["Reged"], false);
                    this.fpDept.SetValue(i, 5, dataRow["TelLmt"], false);
                    this.fpDept.SetValue(i, 6, dataRow["Teled"], false);
                }
                this.fpDept.Tag = "1";
            }
            else
            {
                #region ""
                int count = 0, colCount = 0, row = 0;

                colCount = this.fpDept.Columns.Count / 2;

                for (int i = 0; i < dvDepts.Count; i++)
                {
                    if (count % colCount == 0)
                    {
                        this.fpDept.Rows.Add(this.fpDept.RowCount, 1);
                        row = this.fpDept.RowCount - 1;
                    }

                    dataRow = dvDepts[i];
                    this.fpDept.SetValue(row, 2 * (count % colCount), dataRow[0].ToString(), false);
                    this.fpDept.SetValue(row, 2 * (count % colCount) + 1, dataRow[1].ToString(), false);
                    count++;
                }
                #endregion
                this.fpDept.Tag = "0";
            }
            return 0;
        }

        /// <summary>
        /// init Reg department combox
        /// </summary>
        private void addRegDeptToCombox()
        {
            DataRow row;
            al = new ArrayList();

            for (int i = 0; i < this.dsItems.Tables["Dept"].Rows.Count; i++)
            {
                row = this.dsItems.Tables["Dept"].Rows[i];
                //重复的不添加
                if (i > 0 && row["ID"].ToString() == dsItems.Tables["Dept"].Rows[i - 1]["ID"].ToString()) continue;

                FS.HISFC.Models.Base.Department dept = new FS.HISFC.Models.Base.Department();
                dept.ID = row["ID"].ToString();
                dept.Name = row["Name"].ToString();
                dept.SpellCode = row["Spell_Code"].ToString();
                dept.WBCode = row["Wb_Code"].ToString();
                dept.UserCode = row["Input_Code"].ToString();

                this.al.Add(dept);
            }

            this.cmbDept.AddItems(this.al);
        }
        #endregion

        #region doct
        /// <summary>
        /// 初始化医生列表
        /// </summary>
        /// <returns></returns>
        private int InitDoct()
        {
            alDoct = this.conMgr.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            if (alDoct == null)
            {
                MessageBox.Show("获取门诊医生列表时出错!" + conMgr.Err, "提示");
                alDoct = new ArrayList();
                //return -1;
            }

            this.cmbDoctor.AddItems(alDoct);

            this.AddDoctToDataSet(alDoct);
            this.AddDoctToFp(false);

            return 0;
        }
        /// <summary>
        /// 将医生添加到DataSet 
        /// </summary>
        /// <param name="alPersons"></param>
        /// <returns></returns>
        private int AddDoctToDataSet(ArrayList alPersons)
        {
            dsItems.Tables["Doct"].Rows.Clear();

            foreach (FS.HISFC.Models.Base.Employee person in alPersons)
            {
                this.dsItems.Tables["Doct"].Rows.Add(new object[]
                    {
                        person.ID,	//医生代码
                        person.Name,//医生名称
                        person.SpellCode,
                        person.WBCode,
                        0,0,0,0,0,0,DateTime.MinValue,DateTime.MinValue,"",false,"",false
                    });
            }

            return 0;
        }

        /// <summary>
        /// 将出诊医生添加到医生列表
        /// </summary>
        /// <param name="ds"></param>
        private void AddDoctToDataSet(DataSet ds)
        {
            dsItems.Tables["Doct"].Rows.Clear();

            //当前时间
            DateTime dtNow = this.SchemaMgr.GetDateTimeFromSysDateTime();  //获取系统时间

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];

                if (this.IsFilterSchemeByNoon)
                {
                    #region 根据午别来过滤掉【专家排班】

                    if (row[2].Equals(CommonController.Instance.GetNoonID(dtNow)))
                    {
                        dsItems.Tables["Doct"].Rows.Add(new object[]
                        {
                            row[0],//医生代码
                            row[1],//医生名称
                            row[12],//拼音吗
                            row[13],//五笔码						
                            row[5],//挂号限额
                            row[6],//已挂号数
                            row[7],//预约限额
                            row[8],//已预约数
                            row[9],//特诊限额
                            row[10],//特诊已挂
                            row[3],//开始时间
                            row[4],//结束时间
                            row[2],//午别
                            FS.FrameWork.Function.NConvert.ToBoolean(row[11]),
                            row[14],
                            FS.FrameWork.Function.NConvert.ToBoolean(row[15])//是否教授
                        });

                    }

                    #endregion
                }
                else
                {

                    dsItems.Tables["Doct"].Rows.Add(new object[]
                    {
                        row[0],//医生代码
                        row[1],//医生名称
                        row[12],//拼音吗
                        row[13],//五笔码						
                        row[5],//挂号限额
                        row[6],//已挂号数
                        row[7],//预约限额
                        row[8],//已预约数
                        row[9],//特诊限额
                        row[10],//特诊已挂
                        row[3],//开始时间
                        row[4],//结束时间
                        row[2],//午别
                        FS.FrameWork.Function.NConvert.ToBoolean(row[11]),
                        row[14],
                        FS.FrameWork.Function.NConvert.ToBoolean(row[15])//是否教授
                    });
                }
            }
        }
        /// <summary>
        /// 将医生集合添加到FarPoint中
        /// </summary>	
        /// <param name="IsDisplaySchema"></param>	
        /// <returns></returns>
        private int AddDoctToFp(bool IsDisplaySchema)
        {
            //清除
            if (this.fpDoctor.RowCount > 0)
                this.fpDoctor.Rows.Remove(0, this.fpDoctor.RowCount);

            DataRowView row;
            //DateTime current = this.regMgr.GetDateTimeFromSysDateTime();
            //string noonCode = this.getNoon(current);

            if (IsDisplaySchema)
            {
                #region ""

                FS.HISFC.Models.Registration.RegLevel level = (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;

                if (this.IsProfessor(level))//挂教授号，教授排在前面
                {
                    this.dvDocts.Sort = "IsProfessor Desc, ID, Noon, IsAppend, BeginTime";
                }
                else//付教授号,付教授排在前面
                {
                    this.dvDocts.Sort = "IsProfessor, ID, Noon, IsAppend, BeginTime";
                }
                int i = 0;
                for (int s = 0; s < dvDocts.Count; s++)
                {
                    row = dvDocts[s];

                    //if (row[12].Equals(noonCode) == false && FS.FrameWork.Function.NConvert.ToDateTime(row[10]).Date == current.Date)
                    //{
                    //    continue;
                    //}

                    this.fpDoctor.Rows.Add(this.fpDoctor.RowCount, 1);
                    i = this.fpDoctor.RowCount - 1;
                    this.fpDoctor.SetValue(i, 0, row["ID"], false);
                    this.fpDoctor.SetValue(i, 1, row["Name"], false);

                    if (row["IsAppend"].ToString().ToUpper() == "TRUE")//加号
                    {
                        this.fpDoctor.SetValue(i, 2, this.getNoon(row["Noon"].ToString()) + "[加号]", false);
                    }
                    else
                    {
                        this.fpDoctor.SetValue(i, 2, this.getNoon(row["Noon"].ToString()) +
                            "[" + DateTime.Parse(row["BeginTime"].ToString()).ToString("HH:mm") + "～" +
                            DateTime.Parse(row["EndTime"].ToString()).ToString("HH:mm") + "]", false);
                    }

                    this.fpDoctor.SetValue(i, 3, row["RegLmt"], false);
                    this.fpDoctor.SetValue(i, 4, FS.FrameWork.Function.NConvert.ToInt32(row["RegLmt"]) - FS.FrameWork.Function.NConvert.ToInt32(row["Reged"]), false);
                    this.fpDoctor.SetValue(i, 5, row["TelLmt"], false);
                    this.fpDoctor.SetValue(i, 6, row["Teled"], false);
                    this.fpDoctor.SetValue(i, 7, row["SpeLmt"], false);
                    this.fpDoctor.SetValue(i, 8, row["Sped"], false);
                    this.fpDoctor.SetValue(i, 9, row["Memo"], false);
                    //教授、付教授颜色区分
                    if (row["IsProfessor"].ToString().ToUpper() == "TRUE")
                    {
                        this.fpDoctor.Rows[i].BackColor = Color.LightGreen;
                    }
                }
                this.Span();

                #endregion
                this.fpDoctor.Tag = "1";
            }
            else
            {
                int RowCount = 0, ColumnCount, Row = 0;

                ColumnCount = this.fpDoctor.ColumnCount / 2;
                foreach (DataRowView dv in this.dvDocts)
                {
                    if (RowCount % ColumnCount == 0)
                    {
                        this.fpDoctor.Rows.Add(this.fpDoctor.RowCount, 1);
                        Row = this.fpDoctor.RowCount - 1;
                    }

                    this.fpDoctor.SetValue(Row, 2 * (RowCount % ColumnCount), dv["ID"].ToString(), false);
                    this.fpDoctor.SetValue(Row, 2 * (RowCount % ColumnCount) + 1, dv["Name"].ToString(), false);

                    RowCount++;
                }
                this.fpDoctor.Tag = "0";
            }

            return 0;
        }
        /// <summary>
        /// 压缩显示医生姓名
        /// </summary>
        private void Span()
        {
            int rowLastDoct = 0;

            int rowCnt = this.fpDoctor.RowCount;

            for (int i = 0; i < rowCnt; i++)
            {
                if (i > 0 && this.fpDoctor.GetText(i, 0) != this.fpDoctor.GetText(i - 1, 0))
                {
                    if (i - rowLastDoct > 1)
                    {
                        this.fpDoctor.Models.Span.Add(rowLastDoct, 0, i - rowLastDoct, 1);
                        this.fpDoctor.Models.Span.Add(rowLastDoct, 1, i - rowLastDoct, 1);
                    }

                    rowLastDoct = i;
                }

                //最后一行处理
                if (i > 0 && i == rowCnt - 1 && this.fpDoctor.GetText(i, 0) == this.fpDoctor.GetText(i - 1, 0))
                {
                    this.fpDoctor.Models.Span.Add(rowLastDoct, 0, i - rowLastDoct + 1, 1);
                    this.fpDoctor.Models.Span.Add(rowLastDoct, 1, i - rowLastDoct + 1, 1);
                }
            }
        }
        /// <summary>
        /// add doctor to combox
        /// </summary>
        private void AddDoctToCombox()
        {
            DataRow row;
            al = new ArrayList();

            for (int i = 0; i < this.dsItems.Tables["Doct"].Rows.Count; i++)
            {
                row = this.dsItems.Tables["Doct"].Rows[i];
                //重复的不添加
                if (i > 0 && row["ID"].ToString() == dsItems.Tables["Doct"].Rows[i - 1]["ID"].ToString()) continue;

                FS.HISFC.Models.Base.Employee p = new FS.HISFC.Models.Base.Employee();
                p.ID = row["ID"].ToString();
                p.Name = row["Name"].ToString();
                p.SpellCode = row["Spell_Code"].ToString();
                p.WBCode = row["Wb_Code"].ToString();
                p.IsExpert = FS.FrameWork.Function.NConvert.ToBoolean(row["IsProfessor"].ToString());//是否专家
                p.Memo = "[" + this.getNoon(row["Noon"].ToString()) + "] " + row["Memo"].ToString();

                this.al.Add(p);
            }

            this.cmbDoctor.AddItems(this.al);
        }
        #endregion

        /// <summary>
        /// 初始化证件类别
        /// </summary>
        /// <returns></returns>
        private int InitCardType()
        {
            al = this.conMgr.QueryConstantList("IDCard");
            if (al == null)
            {
                MessageBox.Show("获取证件类型时出错!" + this.conMgr.Err, "提示");
                return -1;
            }

            this.cmbCardType.AddItems(al);

            return 0;
        }

        /// <summary>
        /// 生成结算类别列表
        /// </summary>
        /// <returns></returns>
        private int initPact()
        {
            int count = 0, colCount = 0, row = 0;

            //{B71C3094-BDC8-4fe8-A6F1-7CEB2AEC55DD}
            //this.al = this.conMgr.QueryConstantList("PACTUNIT");
            //this.al = this.pactMgr.GetPactUnitInfo() ;
            this.al = feeMgr.QueryPactUnitOutPatient();
            if (al == null)
            {
                MessageBox.Show("获取患者合同单位信息时出错!" + this.conMgr.Err, "提示");
                return -1;
            }

            colCount = this.fpPayKind.ColumnCount / 2;

            if (this.fpPayKind.RowCount > 0)
                this.fpPayKind.Rows.Remove(0, this.fpPayKind.RowCount);
            //{B71C3094-BDC8-4fe8-A6F1-7CEB2AEC55DD}
            //foreach (FS.HISFC.Models.Base.Const obj in this.al)
            foreach (FS.FrameWork.Models.NeuObject obj in this.al)
            {
                //if (obj.IsValid == false) continue;//废弃

                if (count % colCount == 0)
                {
                    this.fpPayKind.Rows.Add(this.fpPayKind.RowCount, 1);
                    row = this.fpPayKind.RowCount - 1;
                }

                this.fpPayKind.SetValue(row, 2 * (count % colCount), obj.ID, false);
                this.fpPayKind.SetValue(row, 2 * (count % colCount) + 1, obj.Name, false);

                count++;
            }
            this.cmbPayKind.AddItems(this.al);
            if (this.al.Count > 0)
            {
                this.cmbPayKind.SelectedIndex = 0;
            }

            return 0;
        }
        /// <summary>
        /// 生成输入法列表
        /// </summary>
        private void initInputMenu()
        {

            for (int i = 0; i < InputLanguage.InstalledInputLanguages.Count; i++)
            {
                InputLanguage t = InputLanguage.InstalledInputLanguages[i];
                System.Windows.Forms.ToolStripMenuItem m = new ToolStripMenuItem();
                m.Text = t.LayoutName;
                //m.Checked = true;
                m.Click += new EventHandler(m_Click);

                this.neuContextMenuStrip1.Items.Add(m);
            }
        }

        /// <summary>
        /// 初始化预约时间控件
        /// </summary>
        private void InitBookingDate()
        {
            this.ucChooseDate = new ucChooseBookingDate();

            this.panel1.Controls.Add(ucChooseDate);

            this.ucChooseDate.BringToFront();
            this.ucChooseDate.Location = new Point(this.dtBookingDate.Left, this.dtBookingDate.Top + this.dtBookingDate.Height);
            this.ucChooseDate.Visible = false;
            this.ucChooseDate.SelectedItem += new Registration.ucChooseBookingDate.dSelectedItem(ucChooseDate_SelectedItem);
        }

        /// <summary>
        /// 清屏
        /// </summary>
        private void Clear()
        {
            DateTime current = this.regMgr.GetDateTimeFromSysDateTime();

            this.regObj = null;
            if (isSetDefaultRegLev)
            {
                this.SetRegLevelDefault();
            }
            //设定默认
            //this.SetRegLevelDefault() ;

            this.cmbDept.Tag = "";

            this.cmbDoctor.Tag = "";
            this.txtCardNo.Text = "";

            this.cmbSex.Text = "男";

            this.txtAge.Text = "";
            this.txtName.Text = "";
            this.cmbUnit.SelectedIndex = 0;
            this.dtBirthday.Value = current.AddYears(-1);
            this.cmbPayKind.Tag = this.DefaultPactID;
            this.txtMcardNo.Text = "";
            this.txtPhone.Text = "";
            this.txtAddress.Text = "";

            if (cmbPatientType.Items.Count > 0)
            {
                cmbPatientType.SelectedIndex = 0;
            }
            else
            {
                cmbPatientType.Text = "";
            }

            this.cmbCardType.Enabled = true;
            this.cmbCardType.Tag = "";
            //this.lbSum.Text = this.fpList.RowCount.ToString(); 
            this.lbSum.Text = this.SetRegNum();
            //this.lbTot.Text = "";
            //this.lbReceive.Text = "";
            this.lbTip.Text = "";

            this.ClearBookingInfo();
            this.SetBookingDate(current);
            this.SetDefaultBookingTime(current);
            this.txtCardNo.Focus();
            this.chbEncrpt.Checked = false;
            this.isReadCard = false;

            this.chbBookFee.Checked = this.isCheckBookFee;
            this.chbNoCheckFee.Checked = this.IsCheckNoCheckFee;
            this.chbNoRegFee.Checked = this.IsCheckNoRegFee;
            this.chbCheckFee.Checked = false;

            this.txtIdNO.Text = "";
            this.tbSIBalanceCost.Text = string.Empty;

            // this.myYBregObj = null;
            this.SetEnabled(true);
            //{0C30F7F0-2BCF-4c03-BA6E-D7E22A638E97}
            this.txtCardNo.Enabled = true;
            this.txtCardNo.Tag = null;
            this.txtMarkNo.Text = string.Empty;

            //显示收费员当前使用的发票号
            this.InitInvoiceInfo();
            if (isShowMiltScreen)
            {
                //外屏清空{67C90AAC-CFAD-4089-96F4-9F9FC82D8754}
                if (Screen.AllScreens.Length > 1)
                {
                    //this.iMultiScreen.ListInfo = null;
                    //显示初始化界面
                    FS.HISFC.Models.Base.Employee currentOperator = account.Operator as FS.HISFC.Models.Base.Employee;
                    System.Collections.Generic.List<Object> lo = new System.Collections.Generic.List<object>();
                    lo.Add("");//患者基本信息
                    lo.Add("");//挂号级别
                    lo.Add("");// 挂号科室
                    lo.Add("");//挂号医生;
                    lo.Add("");//挂号费用
                    lo.Add(currentOperator);//收费员信息（非初始化界面值为空）
                    this.iMultiScreen.ListInfo = lo;
                }
            }

            this.cmbBookingType.Tag = null;
            this.cmbBookingType.Text = string.Empty;
            this.QueryRegLevl();
        }

        /// <summary>
        /// 清除预约信息
        /// </summary>
        private void ClearBookingInfo()
        {
            this.txtOrder.Text = "";
            this.txtOrder.Tag = null;
        }

        /// <summary>
        /// 设定挂号级别的默认值
        /// </summary>
        private void SetRegLevelDefault()
        {
            if (this.cmbRegLevel.alItems != null)
            {
                foreach (FS.FrameWork.Models.NeuObject obj in this.cmbRegLevel.alItems)
                {
                    if ((obj as FS.HISFC.Models.Registration.RegLevel).IsDefault)
                    {
                        this.cmbRegLevel.Text = (obj as FS.HISFC.Models.Registration.RegLevel).Name;
                        this.cmbRegLevel.Tag = (obj as FS.HISFC.Models.Registration.RegLevel).ID;
                        return;
                    }
                }
            }
            this.cmbRegLevel.Tag = "";//此地是机关,如果没有默认值会回车保存会提示无挂号级别,烦
        }

        /// <summary>
        /// 初始化午别
        /// </summary>
        private void InitNoon()
        {
            FS.HISFC.BizLogic.Registration.Noon noonMgr = new FS.HISFC.BizLogic.Registration.Noon();

            this.alNoon = noonMgr.Query();
            if (alNoon == null)
            {
                MessageBox.Show("获取午别信息时出错!" + noonMgr.Err, "提示");
                return;
            }
        }

        /// <summary>
        /// 获取午别
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        private string getNoon(DateTime current)
        {
            if (this.alNoon == null) return "";
            /*
             * 理解错误：以为午别应该是包含一天全部时间上午：06~12,下午:12~18其余为晚上,
             * 实际午别为医生出诊时间段,上午可能为08~11:30，下午为14~17:30
             * 所以如果挂号员如果不在这个时间段挂号,就有可能提示午别未维护
             * 所以改为根据传人时间所在的午别例如：9：30在06~12之间，那么就判断是否有午别在
             * 06~12之间，全包含就说明9:30是那个午别代码
             */
            //			foreach(FS.HISFC.Models.Registration.Noon obj in alNoon)
            //			{
            //				if(int.Parse(current.ToString("HHmmss"))>=int.Parse(obj.BeginTime.ToString("HHmmss"))&&
            //					int.Parse(current.ToString("HHmmss"))<int.Parse(obj.EndTime.ToString("HHmmss")))
            //				{
            //					return obj.ID;
            //				}
            //			}

            //int[,] zones = new int[,] { { 0, 120000 }, { 120000, 180000 }, { 180000, 235959 } };
            int time = int.Parse(current.ToString("HHmmss"));
            //int begin = 0, end = 0;

            //for (int i = 0; i < 3; i++)
            //{
            //    if (zones[i, 0] <= time && zones[i, 1] > time)
            //    {
            //        begin = zones[i, 0];
            //        end = zones[i, 1];
            //        break;
            //    }
            //}

            foreach (FS.HISFC.Models.Base.Noon obj in alNoon)
            {
                if (time >= int.Parse(obj.StartTime.ToString("HHmmss")) &&
                   time <= int.Parse(obj.EndTime.ToString("HHmmss")))
                {
                    return obj.ID;
                }
            }

            return "";
        }
        /// <summary>
        /// 根据午别代码获取午别名称
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private string getNoon(string ID)
        {
            if (this.alNoon == null) return ID;

            foreach (FS.HISFC.Models.Base.Noon obj in alNoon)
            {
                if (obj.ID == ID) return obj.Name;
            }

            return ID;
        }
        private string QeryNoonName(string noonid)
        {
            FS.HISFC.BizLogic.Registration.Noon noonMgr = new FS.HISFC.BizLogic.Registration.Noon();
            return noonMgr.Query(noonid);

        }

        #region Get、Set Oper's Recipe
        /// <summary>
        /// 获取当前处方号
        /// </summary>
        /// <param name="OperID"></param>		
        private void GetRecipeNo(string OperID)
        {
            if (this.GetRecipeType == 1)
            {
                this.txtRecipeNo.Text = "";//每次登陆自己录入处方号
            }
            else if (this.GetRecipeType == 2)
            {
                FS.FrameWork.Models.NeuObject obj = this.conMgr.GetConstansObj("RegRecipeNo", OperID);
                if (obj == null)
                {
                    MessageBox.Show("获取处方号出错!" + this.conMgr.Err, "提示");
                    return;
                }
                if (obj.Name == "")
                {
                    this.txtRecipeNo.Text = "0";
                }
                else
                {
                    this.txtRecipeNo.Text = obj.Name;
                }
            }
            //{B0B20CE3-195C-4aee-AB13-CEBB5EA9BB94}
            else
            {
                FS.FrameWork.Models.NeuObject obj = this.conMgr.GetConstansObj("RegRecipeNo", OperID);
                if (obj == null)
                {
                    MessageBox.Show("获取处方号出错!" + this.conMgr.Err, "提示");
                    return;
                }
                if (obj.Name == "")
                {
                    this.txtRecipeNo.Text = "0";
                }
                else
                {
                    this.txtRecipeNo.Text = obj.Name;
                }
            }
        }

        /// <summary>
        /// 修改处方号
        /// </summary>
        private void ChangeRecipe()
        {
            //this.txtRecipeNo.TabStop = true ;
            this.txtRecipeNo.BorderStyle = BorderStyle.Fixed3D;
            this.txtRecipeNo.BackColor = SystemColors.Window;
            this.txtRecipeNo.ReadOnly = false;
            this.txtRecipeNo.ForeColor = SystemColors.WindowText;
            this.txtRecipeNo.Font = new Font("宋体", 10);
            this.txtRecipeNo.Location = new Point(381, 10);

            this.txtRecipeNo.Focus();
        }
        private void txtRecipeNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.cmbRegLevel.Focus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }
        /// <summary>
        /// 设置处方
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>		
        private void txtRecipeNo_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtRecipeNo.ReadOnly == false)
            {
                string r = this.txtRecipeNo.Text.Trim();

                try
                {
                    if (long.Parse(r) < 0)
                    {
                        MessageBox.Show("处方号不能小于零!", "提示");
                        e.Cancel = true;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    string err = ex.Message;
                    MessageBox.Show("处方号必须是数字!", "提示");
                    e.Cancel = true;
                    return;
                }
                this.SetRecipeNo();
            }
        }

        /// <summary>
        /// 设置处方号只读
        /// </summary>
        private void SetRecipeNo()
        {
            //this.txtRecipeNo.TabStop = false ;
            this.txtRecipeNo.ReadOnly = true;
            this.txtRecipeNo.Location = new Point(381, 14);
            this.txtRecipeNo.BackColor = SystemColors.AppWorkspace;
            this.txtRecipeNo.ForeColor = Color.Yellow;
            this.txtRecipeNo.Font = new Font("宋体", 11, FontStyle.Bold);
            this.txtRecipeNo.BorderStyle = BorderStyle.None;
        }


        /// <summary>
        /// 关闭窗体时保存挂号员的处方号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ucRegister_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (this.regMgr.Connection.State == ConnectionState.Closed) return;
            string recipeNO = this.txtRecipeNo.Text.Trim();
            if ((recipeNO != "" && recipeNO != string.Empty))
            {
                if (this.SaveRecipeNo() == -1)
                {
                    //e.Cancel = true ;
                }
            }
        }
        /// <summary>
        /// 保存处方记录
        /// </summary>
        /// <returns></returns>
        private int SaveRecipeNo()
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(this.regMgr.con);
            //SQLCA.BeginTransaction();

            try
            {
                this.conMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                FS.HISFC.Models.Base.Const con = new FS.HISFC.Models.Base.Const();
                con.ID = this.regMgr.Operator.ID;//操作员
                con.Name = this.txtRecipeNo.Text.Trim();//处方号
                con.IsValid = true;

                int rtn = this.conMgr.UpdateConstant("RegRecipeNo", con);
                if (rtn == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.conMgr.Err, "提示");
                    return -1;
                }
                if (rtn == 0)//更新没有数据、插入
                {
                    if (this.conMgr.InsertConstant("RegRecipeNo", con) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.conMgr.Err, "提示");
                        return -1;
                    }
                }

                FS.FrameWork.Management.PublicTrans.Commit();
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "提示");
                return -1;
            }

            return 0;
        }
        #endregion

        #region Query operator's registration information of today
        /// <summary>
        /// 按操作员检索当日挂号信息
        /// </summary>
        private void Retrieve()
        {
            DateTime current = this.regMgr.GetDateTimeFromSysDateTime();

            al = this.regMgr.Query(current.Date, current.Date.AddDays(1), this.regMgr.Operator.ID);
            if (al == null)
            {
                MessageBox.Show("检索挂号员当日挂号信息时出错!" + regMgr.Err, "提示");
                return;
            }

            if (this.fpList.RowCount > 0)
                this.fpList.Rows.Remove(0, this.fpList.RowCount);

            foreach (FS.HISFC.Models.Registration.Register obj in al)
            {
                this.addRegister(obj);
            }
            this.lbSum.Text = this.SetRegNum();

        }
        /// <summary>
        /// 更新有效挂号数

        /// </summary>
        /// <returns></returns>
        private string SetRegNum()
        {
            DateTime current = this.regMgr.GetDateTimeFromSysDateTime();
            string result = this.regMgr.QueryValidRegNumByOperAndOperDT(this.regMgr.Operator.ID, current.Date.ToString(), current.Date.AddDays(1).ToString());
            if (result == "-1")
            {
                MessageBox.Show(this.regMgr.Err);
                result = "0";
            }

            return result;

        }
        /// <summary>
        /// 添加挂号记录
        /// </summary>
        /// <param name="obj"></param>
        private void addRegister(FS.HISFC.Models.Registration.Register obj)
        {
            //  string strTemp = "12345".PadLeft(10,'0');//fee.GetAccountByCardNo(obj.PID.CardNO).AccountCard.MarkNO;

            this.fpList.Rows.Add(this.fpList.RowCount, 1);
            int cnt = this.fpList.RowCount - 1;
            this.fpList.ActiveRowIndex = cnt;
            //this.fpList.SetValue(cnt, 0, obj.PID.CardNO, false);//病历号
            try
            {
                if (obj.Card != null && !string.IsNullOrEmpty(obj.Card.ID))
                {
                    this.fpList.SetValue(cnt, 0, obj.Card.ID, false);
                }
                else
                {
                    List<FS.HISFC.Models.Account.AccountCard> list = account.GetMarkList(obj.PID.CardNO);
                    if (list.Count == 0)
                    {
                        this.fpList.SetValue(cnt, 0, obj.PID.CardNO, false);
                    }
                    else
                    {
                        this.fpList.SetValue(cnt, 0, list[0].MarkNO, false);
                    }
                }
            }
            catch (Exception e)
            {
                this.fpList.SetValue(cnt, 0, obj.PID.CardNO, false);
            }

            this.fpList.SetValue(cnt, 1, obj.Name, false);//姓名
            this.fpList.SetValue(cnt, 2, obj.Pact.Name, false);
            this.fpList.SetValue(cnt, 3, obj.Birthday.ToShortDateString(), false);
            this.fpList.SetValue(cnt, 4, permissMgr.GetAge(obj.Birthday), false);
            this.fpList.SetValue(cnt, 5, obj.DoctorInfo.Templet.RegLevel.Name, false);
            this.fpList.SetValue(cnt, 6, obj.DoctorInfo.Templet.Dept.Name, false);
            this.fpList.SetValue(cnt, 7, obj.DoctorInfo.Templet.Doct.Name, false);
            this.fpList.SetValue(cnt, 8, obj.OrderNO, false);
            this.fpList.SetValue(cnt, 9, obj.OwnCost, false);
            this.fpList.SetValue(cnt, 10, obj.PubCost, false);
            this.fpList.SetValue(cnt, 11, obj.InvoiceNO, false);
            if (obj.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Back ||
                obj.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Cancel)
            {
                this.fpList.Rows[cnt].BackColor = Color.Green;
            }

            this.fpList.Rows[cnt].Tag = obj;
        }

        #endregion

        /// <summary>
        /// 装载打印控件
        /// </summary>
        /// <returns></returns>
        private int LoadPrint()
        {
            //获取打印控件的类名   

            //object o = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(UFC.Registration.ucRegister), typeof(FS.HISFC.BizProcess.Interface.Registration.IRegPrint));
            //if (o == null)
            //{
            //    MessageBox.Show("请维护UFC.Registration.ucRegister里面接口FS.HISFC.BizProcess.Interface.Registration.IRegPrint的实例对照!");                
            //}
            //else
            //{
            //    IRegPrint = o as FS.HISFC.BizProcess.Interface.Registration.IRegPrint;
            //}

            return 0;
        }

        #endregion

        #region Set booking Date
        /// <summary>
        /// set booking date
        /// </summary>
        /// <param name="seeDate"></param>
        private void SetBookingDate(DateTime seeDate)
        {
            this.dtBookingDate.Value = seeDate.Date;
            this.lbWeek.Text = this.getWeek(seeDate);
        }
        /// <summary>
        /// 获得星期
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        private string getWeek(DateTime current)
        {
            string[] week = new string[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };

            return week[(int)current.DayOfWeek];
        }

        /// <summary>
        /// 设置默认情况下,就诊安排时间段显示
        /// </summary>
        /// <param name="seeDate"></param>
        private void SetDefaultBookingTime(DateTime seeDate)
        {
            FS.HISFC.Models.Registration.Schema schema = new FS.HISFC.Models.Registration.Schema();
            schema.Templet.Begin = seeDate.Date;
            schema.Templet.End = seeDate.Date;

            this.SetBookingTime(schema);

            this.SetBookingTag(null);
        }

        /// <summary>
        /// 设置就诊时间段
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        private void SetBookingTime(FS.HISFC.Models.Registration.Schema schema)
        {
            this.dtBegin.Value = schema.Templet.Begin;
            this.dtEnd.Value = schema.Templet.End;

            this.SetBookingTag(schema);
        }
        /// <summary>
        /// 保留看诊时间段实体信息
        /// </summary>
        /// <param name="schema"></param>
        private void SetBookingTag(FS.HISFC.Models.Registration.Schema schema)
        {
            this.dtBookingDate.Tag = schema;

            if (schema == null)
            {
                this.lbRegLmt.Text = "";
                this.lbReg.Text = "";
                this.lbTelLmt.Text = "";
                this.lbTel.Text = "";
                this.lbSpeLmt.Text = "";
                this.lbSpe.Text = "";
            }
            else
            {
                this.lbRegLmt.Text = schema.Templet.RegQuota.ToString();//来人挂号限额
                this.lbReg.Text = schema.RegedQTY.ToString();//已挂号数量
                this.lbTelLmt.Text = schema.Templet.TelQuota.ToString();//来电限额
                this.lbTel.Text = schema.TeledQTY.ToString();
                this.lbSpeLmt.Text = schema.Templet.SpeQuota.ToString();//特诊限额
                this.lbSpe.Text = schema.SpedQTY.ToString();
            }
        }
        #endregion

        #region 焦点
        /// <summary>
        /// 挂号级别得到焦点,显示挂号级别列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbRegLevel_Enter(object sender, System.EventArgs e)
        {
            if (this.fpSpread1.ActiveSheetIndex != 0) this.fpSpread1.ActiveSheetIndex = 0;

            this.setEnterColor(this.cmbRegLevel);
        }
        /// <summary>
        /// 挂号科室得到焦点，显示挂号科室列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDept_Enter(object sender, System.EventArgs e)
        {
            this.setEnterColor(this.cmbDept);

            if (this.fpSpread1.ActiveSheetIndex != 1) this.fpSpread1.ActiveSheetIndex = 1;
        }
        /// <summary>
        /// 医生得到焦点，显示医生列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDoctor_Enter(object sender, System.EventArgs e)
        {

            if (this.fpSpread1.ActiveSheetIndex != 2) this.fpSpread1.ActiveSheetIndex = 2;

            this.setEnterColor(this.cmbDoctor);
        }
        /// <summary>
        /// 结算类别得到焦点，显示列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbPayKind_Enter(object sender, System.EventArgs e)
        {
            if (this.fpSpread1.ActiveSheetIndex != 3) this.fpSpread1.ActiveSheetIndex = 3;

            this.setEnterColor(this.cmbPayKind);
        }
        /// <summary>
        /// 病历号得到焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCardNo_Enter(object sender, System.EventArgs e)
        {
            if (this.fpSpread1.ActiveSheetIndex != 4) this.fpSpread1.ActiveSheetIndex = 4;

            this.setEnterColor(this.txtCardNo);
        }
        private void txtName_Enter(object sender, System.EventArgs e)
        {
            if (this.CHInput != null) InputLanguage.CurrentInputLanguage = this.CHInput;
            if (this.fpSpread1.ActiveSheetIndex != 4) this.fpSpread1.ActiveSheetIndex = 4;

            this.setEnterColor(this.txtName);
        }
        private void txtName_Leave(object sender, System.EventArgs e)
        {
            if (this.txtIdNO.Focused)
            {
                if (this.IsInputName && this.txtName.Text.Trim() == "")
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("请输入患者姓名"), "提示");
                    this.txtName.Focus();
                    return;
                }

                //没有输入病历号,需根据患者姓名检索挂号信息
                if (this.txtCardNo.Text.Trim() == "")
                {
                    string CardNo = this.GetCardNoByName(this.txtName.Text.Trim());

                    if (CardNo == "")
                    {
                        int autoGetCardNO;
                        autoGetCardNO = regMgr.AutoGetCardNO();
                        if (autoGetCardNO == -1)
                        {
                            MessageBox.Show("未能成功自动产生卡号，请手动输入！", "提示");
                        }
                        else
                        {
                            this.txtCardNo.Text = autoGetCardNO.ToString().PadLeft(10, '0');
                            this.regObj = this.getRegInfo(this.txtCardNo.Text);
                        }
                        cmbSex.Focus();
                        return;
                    }
                    else
                    {
                        this.txtCardNo.Enabled = false;
                    }
                    this.txtCardNo.Text = CardNo;

                    this.txtCardNo_KeyDown(new object(), new KeyEventArgs(Keys.Enter));


                }
            }
            InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
        }
        private void txtAddress_Enter(object sender, System.EventArgs e)
        {
            if (this.CHInput != null) InputLanguage.CurrentInputLanguage = this.CHInput;
            if (this.fpSpread1.ActiveSheetIndex != 4) this.fpSpread1.ActiveSheetIndex = 4;

            this.setEnterColor(this.txtAddress);
        }

        private void txtAddress_Leave(object sender, System.EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
        }


        #endregion

        #region 设置当前控件颜色
        private void setEnterColor(Control ctl)
        {
            ctl.BackColor = Color.OldLace;
        }
        private void setLeaveColor(Control ctl)
        {
            ctl.BackColor = Color.WhiteSmoke;
        }
        #endregion

        #region 回车

        #region reglevel
        /// <summary>
        /// 设置相应挂号信息(模板,已挂,剩余等信息)
        /// </summary>
        private void QueryRegLevl()
        {
            //恢复初始状态
            this.cmbDept.Tag = "";

            this.cmbDoctor.Tag = "";
            this.lbTip.Text = "";

            if (this.ucChooseDate.Visible) this.ucChooseDate.Visible = false;

            #region 生成挂号级别对应的科室、医生列表

            //{9C164CC2-29C6-4471-B53B-07853A82F9DF} 修改初始化bug
            if (this.cmbRegLevel.SelectedItem == null)
            {
                return;
            }
            FS.HISFC.Models.Registration.RegLevel Level = (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;

            if (Level.IsExpert || Level.IsSpecial)//专家、特诊
            {
                #region 专家
                if (this.isSelectDeptFirst)//如果先选科室,生成科室排班列表
                {
                    this.SetDeptFpStyle(false);
                    //生成右侧出诊专家的科室列表
                    this.GetSchemaDept(FS.HISFC.Models.Base.EnumSchemaType.Doct, Level);
                    this.addRegDeptToFp(false);

                    //生成Combox下拉列表
                    if (!this.isAddAllDept)
                    {
                        this.addRegDeptToCombox();
                    }
                    else
                    {
                        this.cmbDept.AddItems(this.alDept);
                    }

                    //清空医生列表,等选择科室后再检索出诊专家
                    ArrayList al = new ArrayList();

                    this.AddDoctToDataSet(al);
                    this.AddDoctToFp(true);
                    this.cmbDoctor.AddItems(al);
                }
                else
                {
                    //专家号直接选择医生,不跳到科室处,生成全部门诊科室列表
                    this.SetDeptFpStyle(false);
                    this.AddClinicDeptsToDataSet(this.alDept);
                    this.addRegDeptToFp(false);
                    this.cmbDept.AddItems(this.alDept);
                    //
                    this.GetDoct(Level);//获得全部出诊医生
                }
                #endregion
            }
            else if (Level.IsFaculty)//专科
            {
                #region 专科
                //获取出诊专科列表
                this.SetDeptFpStyle(true);
                this.GetSchemaDept(FS.HISFC.Models.Base.EnumSchemaType.Dept, Level);
                this.addRegDeptToFp(true);

                //生成Combox科室下拉列表
                //{920686B9-AD51-496e-9240-5A6DA098404E}
                //if (this.ComboxIsListAll)
                if (this.isAddAllDept)
                {
                    this.cmbDept.AddItems(this.alDept);
                }
                else
                {
                    this.addRegDeptToCombox();
                }

                //清空医生列表,专科不需要选择医生
                ArrayList al = new ArrayList();

                this.AddDoctToDataSet(al);
                this.AddDoctToFp(false);
                this.cmbDoctor.AddItems(al);
                #endregion
            }
            else if (Level.IsEmergency)
            {
                //显示科室列表
                this.SetDeptFpStyle(false);
                if (this.alAllowedEmergDept != null && this.alAllowedEmergDept.Count > 0)
                {
                    this.AddAllowedDeptToDataSet(this.alAllowedEmergDept);
                    this.addRegDeptToCombox();
                }
                else//显示全部科室
                {
                    this.AddClinicDeptsToDataSet(this.alEmergDept);
                    this.cmbDept.AddItems(this.alEmergDept);
                }
                this.addRegDeptToFp(false);
            }
            else//普通
            {
                //显示科室列表
                this.SetDeptFpStyle(false);
                if (this.alAllowedDept != null && this.alAllowedDept.Count > 0)
                {
                    this.AddAllowedDeptToDataSet(this.alAllowedDept);
                    this.addRegDeptToCombox();
                }
                else//显示全部科室
                {
                    this.AddClinicDeptsToDataSet(this.alDept);
                    this.cmbDept.AddItems(this.alDept);
                }
                this.addRegDeptToFp(false);

            }
            #endregion

            //清除预约信息
            this.ClearBookingInfo();

            //设定默认就诊时间段
            this.SetDefaultBookingTime(this.dtBookingDate.Value);

        }

        /// <summary>
        /// 选择挂号级别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbRegLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////恢复初始状态
            //this.cmbDept.Tag = "";
            //this.cmbDoctor.Tag = "";
            //this.lbTip.Text = "";

            //if (this.ucChooseDate.Visible) this.ucChooseDate.Visible = false;

            //#region 生成挂号级别对应的科室、医生列表
            //FS.HISFC.Models.Registration.RegLevel Level = (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;

            //if (Level.IsExpert || Level.IsSpecial)//专家、特诊
            //{
            //    #region 专家
            //    if (this.IsSelectDeptFirst)//如果先选科室,生成科室排班列表
            //    {
            //        this.SetDeptFpStyle(false);
            //        //生成右侧出诊专家的科室列表
            //        this.GetSchemaDept(FS.HISFC.Models.Base.EnumSchemaType.Doct);
            //        this.addRegDeptToFp(false);

            //        //生成Combox下拉列表
            //        if (!this.ComboxIsListAll)
            //        {
            //            this.addRegDeptToCombox();
            //        }
            //        else
            //        {
            //            this.cmbDept.AddItems(this.alDept);
            //        }

            //        //清空医生列表,等选择科室后再检索出诊专家
            //        ArrayList al = new ArrayList();

            //        this.AddDoctToDataSet(al);
            //        this.AddDoctToFp(true);
            //        this.cmbDoctor.AddItems(al);
            //    }
            //    else
            //    {
            //        //专家号直接选择医生,不跳到科室处,生成全部门诊科室列表
            //        this.SetDeptFpStyle(false);
            //        this.AddClinicDeptsToDataSet(this.alDept);
            //        this.addRegDeptToFp(false);
            //        this.cmbDept.AddItems(this.alDept);
            //        //
            //        this.GetDoct();//获得全部出诊医生
            //    }
            //    #endregion
            //}
            //else if (Level.IsFaculty)//专科
            //{
            //    #region 专科
            //    //获取出诊专科列表
            //    this.SetDeptFpStyle(true);
            //    this.GetSchemaDept(FS.HISFC.Models.Base.EnumSchemaType.Dept);
            //    this.addRegDeptToFp(true);

            //    //生成Combox科室下拉列表
            //    if (this.ComboxIsListAll)
            //    {
            //        this.cmbDept.AddItems(this.alDept);
            //    }
            //    else
            //    {
            //        this.addRegDeptToCombox();
            //    }

            //    //清空医生列表,专科不需要选择医生
            //    ArrayList al = new ArrayList();

            //    this.AddDoctToDataSet(al);
            //    this.AddDoctToFp(false);
            //    this.cmbDoctor.AddItems(al);
            //    #endregion
            //}
            //else//普通
            //{
            //    //显示科室列表
            //    this.SetDeptFpStyle(false);
            //    if (this.alAllowedDept != null && this.alAllowedDept.Count > 0)
            //    {
            //        this.AddAllowedDeptToDataSet(this.alAllowedDept);
            //        this.addRegDeptToCombox();
            //    }
            //    else//显示全部科室
            //    {
            //        this.AddClinicDeptsToDataSet(this.alDept);
            //        this.cmbDept.AddItems(this.alDept);
            //    }
            //    this.addRegDeptToFp(false);

            //}
            //#endregion

            ////清除预约信息
            //this.ClearBookingInfo();

            ////设定默认就诊时间段
            //this.SetDefaultBookingTime(this.dtBookingDate.Value);
            this.QueryRegLevl();
        }
        /// <summary>
        /// 挂号级别回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbRegLevel_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.cmbRegLevel.Tag == null || this.cmbRegLevel.Tag.ToString() == "")
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("请选择挂号级别"), "提示");
                    this.cmbRegLevel.Focus();
                    return;
                }

                //判断是专家号,就跳到医生处
                FS.HISFC.Models.Registration.RegLevel Level = (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;
                //生成费用
                if (this.GetCost() == -1)
                {
                    this.cmbRegLevel.Focus();
                    return;
                }

                //焦点跳转
                //专家、特诊号不用选择挂号科室,直接跳到医生处
                if (Level.IsExpert || Level.IsSpecial)
                {
                    if (this.isSelectDeptFirst)
                    {
                        this.cmbDept.Focus();
                    }
                    else
                    {
                        this.cmbDoctor.Focus();
                    }
                }
                else if (Level.IsFaculty)//专科号,直接跳到科室处
                {
                    this.cmbDept.Focus();
                }
                else//不是以上3种,不需要更新排班限额,适用不排班医院,可添加参数,设定跳转顺序,以及是否要录入看诊医生
                {
                    this.cmbDept.Focus();
                }
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //反相跳转
                this.setPriorControlFocus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }

        /// <summary>
        /// 获取出诊科室
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private int GetSchemaDept(FS.HISFC.Models.Base.EnumSchemaType type, FS.HISFC.Models.Registration.RegLevel regLevel)
        {
            DataSet ds = new DataSet();
            if (!this.IsJudgeReglevl)
            {
                ds = this.SchemaMgr.QueryDept(this.dtBookingDate.Value.Date, this.regMgr.GetDateTimeFromSysDateTime(), type);
            }
            else
            {
                ds = this.SchemaMgr.QueryDept(this.dtBookingDate.Value.Date,
                                            this.regMgr.GetDateTimeFromSysDateTime(), type, regLevel.ID);
            }
            if (ds == null)
            {
                MessageBox.Show(this.SchemaMgr.Err, "提示");
                return -1;
            }

            this.addDeptToDataSet(ds, type, regLevel);

            return 0;
        }
        /// <summary>
        /// 将专科、专家出诊科室添加到DataSet
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="type"></param>
        private void addDeptToDataSet(DataSet ds, FS.HISFC.Models.Base.EnumSchemaType type, FS.HISFC.Models.Registration.RegLevel regLevel)
        {
            dsItems.Tables[0].Rows.Clear();

            //当前时间
            DateTime dtNow = this.SchemaMgr.GetDateTimeFromSysDateTime();  //获取系统时间

            if (type == FS.HISFC.Models.Base.EnumSchemaType.Dept)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (this.IsFilterSchemeByNoon)
                    {
                        #region 根据午别来过滤掉【专科排班】

                        if (row[2].Equals(CommonController.Instance.GetNoonID(dtNow)))
                        {
                            dsItems.Tables["Dept"].Rows.Add(new object[]
                            {
                                row[0],//科室代码
                                row[1],//科室名称
                                row[10],//拼音吗
                                row[11],//五笔码
                                row[12],//自定义码
                                row[5],//挂号限额
                                row[6],//已挂号数
                                row[7],//预约限额
                                row[8],//已预约数
                                row[3],//开始时间
                                row[4],//结束时间
                                row[2],//午别
                                FS.FrameWork.Function.NConvert.ToBoolean(row[9])
                            });
                        }

                        #endregion
                    }
                    else
                    {
                        dsItems.Tables["Dept"].Rows.Add(new object[]
                        {
                            row[0],//科室代码
                            row[1],//科室名称
                            row[10],//拼音吗
                            row[11],//五笔码
                            row[12],//自定义码
                            row[5],//挂号限额
                            row[6],//已挂号数
                            row[7],//预约限额
                            row[8],//已预约数
                            row[3],//开始时间
                            row[4],//结束时间
                            row[2],//午别
                            FS.FrameWork.Function.NConvert.ToBoolean(row[9])
                        });
                    }
                }
            }
            else
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    dsItems.Tables["Dept"].Rows.Add(new object[]
                        {
                            row[0],//科室代码
                            row[1],//科室名称
                            row[2],//拼音吗
                            row[3],//五笔码
                            row[4],//自定义码
                            0,//挂号限额
                            0,//已挂号数
                            0,//预约限额
                            0,//已预约数
                            DateTime.MinValue,//开始时间
                            DateTime.MinValue,//结束时间
                            "",//午别
                            false
                        });
                }
            }
        }
        #endregion

        #region dept
        /// <summary>
        /// 选择挂号科室
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.IsTriggerSelectedIndexChanged == false) return;

            if (this.ucChooseDate.Visible) this.ucChooseDate.Visible = false;

            //清除预约信息
            this.ClearBookingInfo();
            //清空医生
            this.cmbDoctor.Tag = "";

            //专家、专科、特诊号都需要扣排班限额
            FS.HISFC.Models.Registration.RegLevel regLevel = (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;
            if (regLevel == null)//没有选择挂号级别
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("请先选择挂号级别"), "提示");
                this.cmbRegLevel.Focus();
                return;
            }

            //显示该科室下医生列表
            if (regLevel.IsSpecial || regLevel.IsExpert)
            {
                this.GetDoctByDept(this.cmbDept.Tag.ToString(), true);
            }
            else if (regLevel.IsFaculty)
            {
                this.GetDoctByDept(this.cmbDept.Tag.ToString(), true);

                if (cmbDoctor.alItems == null || cmbDoctor.alItems.Count == 0)
                {
                    this.GetDoctByDept(this.cmbDept.Tag.ToString(), false);
                }
            }
            else
            {
                this.GetDoctByDept(this.cmbDept.Tag.ToString(), false);
            }

            if (regLevel.IsExpert || regLevel.IsSpecial || regLevel.IsFaculty)
            {
                //设定一个有效的就诊时间段
                this.SetDeptZone(this.cmbDept.Tag.ToString(), this.dtBookingDate.Value, regLevel);
            }
            else
            {
                //设定默认预约时间段
                this.SetDefaultBookingTime(this.dtBookingDate.Value);
            }
        }
        /// <summary>
        /// 挂号科室回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDept_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                FS.HISFC.Models.Registration.RegLevel regLevel = (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;
                if (regLevel == null)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("请先选择挂号级别"), "提示");
                    this.cmbRegLevel.Focus();
                    return;
                }
                if (this.cmbDept.Text.Trim().Length > 0 && this.cmbDept.Text.Trim().Length < 4)
                {
                    try
                    {
                        int.Parse(this.cmbDept.Text);
                        this.cmbDept.Tag = this.cmbDept.Text.Trim().PadLeft(4, '0');
                    }
                    catch
                    {

                    }

                }
                //没有选择科室,显示所有的医生
                if (this.cmbDept.Tag == null || this.cmbDept.Tag.ToString() == "")
                {
                    if (regLevel.IsExpert || regLevel.IsSpecial)
                    {
                        this.GetDoct(regLevel);//获得全部出诊医生
                    }
                    else
                    {
                        this.SetDoctFpStyle(false);
                        this.cmbDoctor.AddItems(this.alDoct);
                        this.AddDoctToDataSet(this.alDoct);
                        this.AddDoctToFp(false);
                    }
                    //设定默认预约时间段
                    this.SetDefaultBookingTime(this.dtBookingDate.Value);
                }

                this.cmbDoctor.Tag = "";

                if (regLevel.IsFaculty)
                {
                    if (this.cmbDept.Tag != null & this.cmbDept.Tag.ToString() != "")
                    {
                        if (this.DisplaySchemaTip(FS.HISFC.Models.Base.EnumSchemaType.Dept) == -1)
                        {
                            this.cmbDept.Focus();
                            return;
                        }
                    }
                    if (this.IsSetDoctFocusForCommon)
                    {
                        this.cmbDoctor.Focus();
                    }
                    else
                    {
                        this.dtBookingDate.Focus();
                    }
                }
                else if (regLevel.IsSpecial || regLevel.IsExpert)
                {
                    this.cmbDoctor.Focus();
                }
                else//不是专家、专科、特诊号不需输入看诊医生和就诊时间,当然可设置参数要求光标跳到医生处
                {
                    //{920686B9-AD51-496e-9240-5A6DA098404E} 更具属性维护是否添加所有医生
                    if (this.IsSetDoctFocusForCommon)
                    {
                        this.cmbDoctor.Focus();
                    }
                    else
                    {
                        this.txtCardNo.Focus();
                    }
                }
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //反相跳转
                this.setPriorControlFocus();
                return;
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }

        /// <summary>
        /// 根据科室代码查询出诊医生
        /// </summary>
        /// <param name="deptID"></param>
        /// <param name="IsDisplaySchema"></param>
        /// <returns></returns>
        private int GetDoctByDept(string deptID, bool IsDisplaySchema)
        {
            if (IsDisplaySchema)
            {
                DataSet ds;
                if (IsFilterDoc)
                {
                    FS.HISFC.Models.Registration.RegLevel regLevel = (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;
                    ds = this.SchemaMgr.QueryDoct(this.dtBookingDate.Value,
                                                    this.regMgr.GetDateTimeFromSysDateTime(), deptID, regLevel.ID);

                }
                else
                {
                    ds = this.SchemaMgr.QueryDoct(this.dtBookingDate.Value,
                                                    this.regMgr.GetDateTimeFromSysDateTime(), deptID);
                }
                if (ds == null)
                {
                    MessageBox.Show(this.SchemaMgr.Err, "提示");
                    return -1;
                }

                this.SetDoctFpStyle(true);
                this.AddDoctToDataSet(ds);
                //{920686B9-AD51-496e-9240-5A6DA098404E} 更具属性维护是否添加所有医生
                //if (this.ComboxIsListAll)


                this.AddDoctToCombox();

                //专家号有排班的，就只选择排班的医生了吧
                //if (this.isAddAllDoct)
                //{
                //    this.cmbDoctor.AddItems(this.alDoct);
                //}
                //else
                //{
                //    this.AddDoctToCombox();
                //}
            }
            else
            {
                //{920686B9-AD51-496e-9240-5A6DA098404E} 更具属性维护是否添加所有医生
                if (this.isAddAllDoct)
                {
                    this.cmbDoctor.AddItems(this.alDoct);
                    this.SetDoctFpStyle(false);
                    this.AddDoctToDataSet(this.alDoct);
                }
                else
                {
                    al = this.conMgr.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D, deptID);
                    if (al == null)
                    {
                        MessageBox.Show("获取出诊医生时出错!" + this.conMgr.Err, "提示");
                        return -1;
                    }
                    this.cmbDoctor.AddItems(al);
                    this.SetDoctFpStyle(false);
                    this.AddDoctToDataSet(al);
                }
            }

            this.AddDoctToFp(IsDisplaySchema);

            return 0;
        }
        /// <summary>
        /// 获取当日出诊全部医生
        /// </summary>
        /// <returns></returns>
        private int GetDoct(FS.HISFC.Models.Registration.RegLevel Level)
        {
            DataSet ds;
            if (!this.IsJudgeReglevl)
            {
                ds = this.SchemaMgr.QueryDoct(this.dtBookingDate.Value, this.regMgr.GetDateTimeFromSysDateTime());
            }
            else
            {
                ds = this.SchemaMgr.QueryDoctByRegLevel(this.dtBookingDate.Value, this.regMgr.GetDateTimeFromSysDateTime(), Level.ID);
            }
            if (ds == null)
            {
                MessageBox.Show(this.SchemaMgr.Err, "提示");
                return -1;
            }

            this.SetDoctFpStyle(true);
            this.AddDoctToDataSet(ds);
            this.AddDoctToFp(true);

            if (this.isAddAllDoct)
            {
                this.cmbDoctor.AddItems(this.alDoct);
            }
            else
            {
                this.AddDoctToCombox();
            }

            return 0;
        }

        /// <summary>
        /// 挂的是教授号or副教授号
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private bool IsProfessor(FS.HISFC.Models.Registration.RegLevel level)
        {
            bool rtn = false;

            if (level.IsExpert || level.IsSpecial)
            {
                foreach (FS.HISFC.Models.Base.Const con in this.alProfessor)
                {
                    if (con.ID == level.ID)
                    {
                        return true;
                    }
                }
            }

            return rtn;
        }
        /// <summary>
        /// 设定科室默认就诊时间段
        /// </summary>
        /// <param name="deptID"></param>
        /// <param name="bookingDate"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        private int SetDeptZone(string deptID, DateTime bookingDate, FS.HISFC.Models.Registration.RegLevel level)
        {
            Registration.RegTypeNUM regType = Registration.RegTypeNUM.Faculty;

            #region Set regType value
            regType = this.getRegType(level);
            #endregion
            if (!this.IsJudgeReglevl)
            {
                this.ucChooseDate.QueryDeptBooking(bookingDate, deptID, regType);
            }
            else
            {
                this.ucChooseDate.QueryDeptBooking(bookingDate, deptID, regType, level);
            }

            //默认显示第一条符合条件（时间未过期、限额未满）的排班信息
            FS.HISFC.Models.Registration.Schema schema = this.ucChooseDate.GetValidBooking(regType);

            if (schema == null)//没有符合条件的
            {
                this.SetDefaultBookingTime(bookingDate.Date);
            }
            else
            {
                //如果当前时间，选择了排班，提示是否继续
                if (string.IsNullOrEmpty(schema.Templet.ID) == false)
                {
                    //取当前时间
                    DateTime currentTime = CommonController.Instance.GetSystemTime();

                    //如果是挂当天号的
                    if (schema.Templet.Begin.Date == currentTime.Date)
                    {
                        //如果午别ID不一致，提示是否继续
                        if (schema.Templet.Noon.ID.Equals(CommonController.Instance.GetNoonID(currentTime)) == false)
                        {
                            if (MessageBox.Show(this, "您选择的排班午别是：" + CommonController.Instance.GetNoonName(schema.Templet.Noon.ID) + "，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            {
                                this.ClearBookingInfo();
                                this.SetDefaultBookingTime(currentTime);
                                this.SetBookingTag(null);
                                return -1;
                            }
                        }
                    }
                }

                this.SetBookingTime(schema);
            }

            return 0;
        }
        /// <summary>
        /// 根据挂号级别转换为枚举,挂号级别必须为专家、专科、特诊
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private Registration.RegTypeNUM getRegType(FS.HISFC.Models.Registration.RegLevel level)
        {
            Registration.RegTypeNUM regType = Registration.RegTypeNUM.Faculty;

            if (level.IsExpert)
            {
                regType = Registration.RegTypeNUM.Expert;
            }
            else if (level.IsFaculty)
            {
                regType = Registration.RegTypeNUM.Faculty;
            }
            else if (level.IsSpecial)
            {
                regType = Registration.RegTypeNUM.Special;
            }

            return regType;
        }

        /// <summary>
        /// 获取最近的有效排班信息
        /// </summary>
        /// <param name="schemaType"></param>
        /// <param name="regLevel"></param>
        /// <param name="deptID"></param>
        /// <param name="doctID"></param>
        /// <returns></returns>
        private int getLastSchema(FS.HISFC.Models.Base.EnumSchemaType schemaType,
            FS.HISFC.Models.Registration.RegLevel regLevel, string deptID, string doctID)
        {
            FS.HISFC.Models.Registration.Schema schema = this.SchemaMgr.Query(schemaType,
                                                    this.regMgr.GetDateTimeFromSysDateTime(), deptID, doctID);
            if (schema == null)
            {
                //出错
                MessageBox.Show("获取最近排班信息出错!" + this.SchemaMgr.Err, "提示");
                return -1;
            }


            if (schema.Templet.ID == "")
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("没有有效的排班记录"), "提示");
                return -1;
            }

            this.IsTriggerSelectedIndexChanged = false;
            this.cmbDept.Tag = schema.Templet.Dept.ID;
            this.IsTriggerSelectedIndexChanged = true;

            this.SetBookingDate(schema.SeeDate);
            this.SetBookingTime(schema);

            return 0;
        }

        /// <summary>
        /// 显示医生一周出诊信息
        /// </summary>
        /// <returns></returns>
        private int DisplaySchemaTip(FS.HISFC.Models.Base.EnumSchemaType schemaType)
        {
            this.lbTip.Text = "";

            //当天没有出诊医生
            if (this.dtBookingDate.Tag == null)
            {
                DateTime current = this.dtBookingDate.Value.Date;

                DateTime end = current.AddDays(6 - (int)current.DayOfWeek);

                //不写业务层了，今天烦
                //string sql = "SELECT distinct week FROM fin_opr_schema WHERE " +
                //    " see_date>to_date('" + current.ToString() + "','yyyy-mm-dd hh24:mi:ss') AND " +
                //    " see_date<=to_date('" + end.ToString() + "','yyyy-mm-dd hh24:mi:ss') ";

                DataSet ds = new DataSet();

                if (schemaType == FS.HISFC.Models.Base.EnumSchemaType.Dept)
                {
                    //sql = sql + " AND schema_type = '0' AND dept_code = '" + this.cmbDept.Tag.ToString() + "'" +
                    //    " AND valid_flag = '1' ";
                    ds = this.SchemaMgr.QuerySchemaForRegister(current.ToString(), end.ToString(), "0", this.cmbDept.Tag.ToString(), "A");
                    if (ds == null)
                    {
                        MessageBox.Show(this.SchemaMgr.Err);
                        return -1;
                    }
                }
                else
                {
                    if (this.cmbDept.Tag != null && this.cmbDept.Tag.ToString() != "")
                    {
                        //sql = sql + " AND schema_type = '1' AND doct_code = '" + this.cmbDoctor.Tag.ToString() + "'" +
                        //    " AND dept_code = '" + this.cmbDept.Tag.ToString() + "' AND valid_flag = '1' ";
                        ds = this.SchemaMgr.QuerySchemaForRegister(current.ToString(), end.ToString(), "1", this.cmbDept.Tag.ToString(), this.cmbDoctor.Tag.ToString());
                        if (ds == null)
                        {
                            MessageBox.Show(this.SchemaMgr.Err);
                            return -1;
                        }
                    }
                    else
                    {
                        //sql = sql + " AND schema_type = '1' AND doct_code = '" + this.cmbDoctor.Tag.ToString() + "'" +
                        //    " AND valid_flag = '1' ";
                        ds = this.SchemaMgr.QuerySchemaForRegister(current.ToString(), end.ToString(), "1", "A", this.cmbDoctor.Tag.ToString());
                        if (ds == null)
                        {
                            MessageBox.Show(this.SchemaMgr.Err);
                            return -1;
                        }
                    }
                }

                //DataSet ds = new DataSet();

                //if (this.SchemaMgr.ExecQuery(sql, ref ds) == -1)
                //{
                //    MessageBox.Show("获取排班信息表出错!" + this.SchemaMgr.Err, "提示");
                //    return -1;
                //}

                if (ds == null || ds.Tables[0].Rows.Count == 0)
                {
                    if (schemaType == FS.HISFC.Models.Base.EnumSchemaType.Dept)
                    {
                        if (this.fpDept.RowCount == 0)
                        {
                            this.lbTip.Text = FS.FrameWork.Management.Language.Msg("该专科一周无出诊");
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("今日无有效排班记录"), "提示");
                            return -1;
                        }
                        else
                        {
                            this.lbTip.Text = FS.FrameWork.Management.Language.Msg("今日已挂满,一周无出诊");
                            return 0;
                        }
                    }
                    else
                    {
                        if (this.fpDoctor.RowCount == 0)
                        {
                            this.lbTip.Text = FS.FrameWork.Management.Language.Msg("该医生一周未排班");
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("今日无有效排班记录"), "提示");
                            return -1;
                        }
                        else
                        {
                            this.lbTip.Text = FS.FrameWork.Management.Language.Msg("今日已挂满,一周无出诊");
                            return 0;
                        }
                    }
                }

                string[] week = new string[] { "日", "一", "二", "三", "四", "五", "六" };
                string tip = "周";

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    tip = tip + week[FS.FrameWork.Function.NConvert.ToInt32(row[0])] + "、";
                }
                this.lbTip.Text = tip.Substring(0, tip.Length - 1) + "出诊";

                //MessageBox.Show("今日无有效排班记录!","提示") ;

                return 0;
            }

            return 0;
        }
        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDept_TextChanged(object sender, System.EventArgs e)
        {
            string strFilter = "ID like '%" + this.cmbDept.Text + "%' or Spell_Code like '%" + this.cmbDept.Text + "%'"
                    + " or Name like '%" + this.cmbDept.Text + "%'";
            /* or Wb_Code like '%"+this.cmbDept.Text
            +"%' or Input_Code like '%"+this.cmbDept.Text+"%'";*/

            try
            {
                dvDepts.RowFilter = strFilter;
            }
            catch { }

            this.addRegDeptToFp(FS.FrameWork.Function.NConvert.ToBoolean(this.fpDept.Tag));
        }
        #endregion

        #region doctor
        /// <summary>
        /// 选择医生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {


            //选择一条符合条件的排班信息做为预约时间
            if (this.IsTriggerSelectedIndexChanged == false) return;

            if (this.ucChooseDate.Visible) this.ucChooseDate.Visible = false;
            //清除预约信息
            this.ClearBookingInfo();

            //专家、专科、特诊号都需要扣排班限额
            FS.HISFC.Models.Registration.RegLevel regLevel = (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;
            if (regLevel == null)//没有选择挂号级别
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("请先选择挂号级别"), "提示");
                this.cmbRegLevel.Focus();
                return;
            }

            if (regLevel.IsExpert || regLevel.IsSpecial)
            {
                //设定一个有效的就诊时间段
                this.SetDoctZone(this.cmbDoctor.Tag.ToString(), this.dtBookingDate.Value, regLevel);
            }
            else if (regLevel.IsFaculty)
            {
                if (this.cmbDoctor.Tag != null)
                {
                    //设定一个有效的就诊时间段
                    this.SetDoctZone(this.cmbDoctor.Tag.ToString(), this.dtBookingDate.Value, regLevel);
                }
            }
            else
            {
                //设定默认预约时间段
                this.SetDefaultBookingTime(this.dtBookingDate.Value);
            }

            //挂号职级与医生职级提示
            this.WarningDoctLevel();
         

        }

        /// <summary>
        /// 挂号职级与医生职级提示
        /// </summary>
        private void WarningDoctLevel()
        {
            if (string.IsNullOrEmpty(this.cmbRegLevel.Text)||string.IsNullOrEmpty(this.cmbDoctor.Text))
            {
                return;
            }
            string reglevlCode = "";

            DateTime dtNow = regFeeMgr.GetDateTimeFromSysDateTime();
            
            //获取医生排班
            FS.HISFC.Models.Registration.Schema schema = SchemaMgr.GetSchema(this.cmbDoctor.Tag.ToString(), dtNow);

            if (schema == null)
            {
                if (MessageBox.Show(this.cmbDoctor.Text + "当日无排班\r\n是否继续挂号？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    return;
                }
                return;
            }

            reglevlCode = schema.Templet.RegLevel.ID;

            try
            {

                if ((this.cmbRegLevel.Tag) is FS.HISFC.Models.Registration.RegLevel)
                {
                    if (reglevlCode != ((this.cmbRegLevel.Tag) as FS.HISFC.Models.Registration.RegLevel).ID)
                    {

                        if (MessageBox.Show("您看诊的挂号级别为:【" + ((this.cmbRegLevel.Tag) as FS.HISFC.Models.Registration.RegLevel).Name + "】，\r\n而患者的挂号级别为：【" + regObj.DoctorInfo.Templet.RegLevel.Name + "】\r\n\r\n是否继续挂号？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                        {
                            return;
                        }
                    }
                }
                else if (reglevlCode != this.cmbRegLevel.Tag.ToString())
                {
                    if (MessageBox.Show("您看诊的挂号级别为:【" + SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().GetRegLevelName(cmbRegLevel.Tag.ToString()) + "】，\r\n而患者的挂号级别为：【" + regObj.DoctorInfo.Templet.RegLevel.Name + "】\r\n\r\n是否继续挂号？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                    {
                        return;
                    }
                }
            }
            catch (Exception ex) { }
        }

        /// <summary>
        /// 设定专家默认就诊时间段
        /// </summary>
        /// <param name="doctID"></param>
        /// <param name="bookingDate"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        private int SetDoctZone(string doctID, DateTime bookingDate, FS.HISFC.Models.Registration.RegLevel level)
        {
            Registration.RegTypeNUM regType = Registration.RegTypeNUM.Faculty;

            #region Set regType value
            regType = this.getRegType(level);
            #endregion

            if (this.cmbDept.Tag != null && this.cmbDept.Tag.ToString() != "")
            {
                this.ucChooseDate.QueryDoctBooking(bookingDate, doctID, this.cmbDept.Tag.ToString(), regType);
                if (this.ucChooseDate.GetValidBooking(regType) == null)
                {
                    this.ucChooseDate.QueryDoctBooking(bookingDate, doctID, regType);
                }
            }
            else
            {
                this.ucChooseDate.QueryDoctBooking(bookingDate, doctID, regType);
            }
          

            //默认显示第一条符合条件（时间未过期、限额未满）的排班信息
            FS.HISFC.Models.Registration.Schema schema = this.ucChooseDate.GetValidBooking(regType);

            if (schema == null)//没有符合条件的
            {
                this.SetDefaultBookingTime(bookingDate.Date);
            }
            else
            {

                //如果当前时间，选择了排班，提示是否继续
                if (string.IsNullOrEmpty(schema.Templet.ID) == false)
                {
                    //取当前时间
                    DateTime currentTime = CommonController.Instance.GetSystemTime();

                    //如果是挂当天号的
                    if (schema.Templet.Begin.Date == currentTime.Date)
                    {
                        //如果午别ID不一致，提示是否继续
                        if (schema.Templet.Noon.ID.Equals(CommonController.Instance.GetNoonID(currentTime)) == false)
                        {
                            if (MessageBox.Show(this, "您选择的排班午别是：" + CommonController.Instance.GetNoonName(schema.Templet.Noon.ID) + "，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            {
                                this.ClearBookingInfo();
                                this.SetDefaultBookingTime(currentTime);
                                this.SetBookingTag(null);
                                return -1;
                            }
                        }
                    }
                }

                this.IsTriggerSelectedIndexChanged = false;
                this.cmbDept.Tag = schema.Templet.Dept.ID;
                this.IsTriggerSelectedIndexChanged = true;

                this.SetBookingTime(schema);
            }

            return 0;
        }

        /// <summary>
        /// 医生回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDoctor_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FS.HISFC.Models.Registration.RegLevel regLevel = (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;
                if (regLevel == null)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("请先选择挂号级别"), "提示");
                    this.cmbRegLevel.Focus();
                    return;
                }
                //因为还可能是预约号,所以不限制必须输入医生
                //				if((regLevel.IsExpert || regLevel.IsSpecial)&&(this.cmbDoctor.Tag == null||this.cmbDoctor.Tag.ToString() == ""))
                //				{
                //					MessageBox.Show("专家号必须指定就诊医生!","提示") ;
                //					this.cmbDoctor.Focus();
                //					return ;
                //				}
                //string temp="";
                //int len = 0;
                //if (!string.IsNullOrEmpty(padLeftChar))
                //{
                //    string text = this.cmbDoctor.Text.Trim();
                //    try
                //    {
                //        int.Parse(text);
                //        foreach (string str in padLeftChar.Split(','))
                //        {
                //            len = 6 - str.Trim().Length;
                //            if (this.cmbDoctor.Text.Trim().Length > 0 && this.cmbDoctor.Text.Trim().Length < len)
                //            {
                //                this.cmbDoctor.Tag = str.Trim() + this.cmbDoctor.Text.Trim().PadLeft(len, '0');
                //            }
                //            if (text != this.cmbDoctor.Text.Trim()) break;

                //        }
                //    }
                //    catch { }

                //}
                if (regLevel.IsFaculty)
                {
                    //获取最近有效的一条排班信息
                    #region
                    /*if(this.cmbDept.Tag != null && this.cmbDept.Tag.ToString() != "")
                    {
                        if( this.getLastSchema(FS.HISFC.Models.Registration.SchemaTypeNUM.Dept,regLevel,
                            this.cmbDept.Tag.ToString(), "") == -1)
                        {
                            this.cmbDept.Focus() ;
                            return ;
                        }
                    }*/

                    if (this.cmbDept.Tag != null && this.cmbDept.Tag.ToString() != "")
                    {
                        if (this.DisplaySchemaTip(FS.HISFC.Models.Base.EnumSchemaType.Dept) == -1)
                        {
                            this.cmbDept.Focus();
                            return;
                        }
                    }

                    this.dtBookingDate.Focus();
                    #endregion
                }
                else if (regLevel.IsExpert)
                {
                    //获取最近有效的一条排班信息
                    #region
                    /*if(this.cmbDoctor.Tag != null && this.cmbDoctor.Tag.ToString() != "")
                    {
                        if( this.getLastSchema(FS.HISFC.Models.Registration.SchemaTypeNUM.Doct,regLevel,
                            "",this.cmbDoctor.Tag.ToString()) == -1)
                        {
                            this.cmbDoctor.Focus() ;
                            return ;
                        }
                    }*/

                    if (this.cmbDoctor.Tag != null && this.cmbDoctor.Tag.ToString() != "")
                    {
                        ///判断教授号录入挂号级别是否正确
                        ///

                        //						if(!this.VerifyIsProfessor(regLevel,(FS.HISFC.Models.RADT.Person)this.cmbDoctor.SelectedItem))
                        //						{
                        //							this.cmbDoctor.Focus() ;
                        //							return ;
                        //						}

                        FS.HISFC.Models.Registration.Schema schema = (FS.HISFC.Models.Registration.Schema)this.dtBookingDate.Tag;
                        if (schema != null)
                        {
                            if (this.VerifyIsProfessor(regLevel, schema) == false)
                            {
                                this.cmbDoctor.Focus();
                                return;
                            }
                        }

                        if (this.DisplaySchemaTip(FS.HISFC.Models.Base.EnumSchemaType.Doct) == -1)
                        {
                            this.cmbDoctor.Focus();
                            return;
                        }
                    }

                    #endregion
                    if (this.IsInputOrder)
                    {
                        this.txtOrder.Focus();
                    }
                    else
                    {
                        this.dtBookingDate.Focus();
                    }
                }
                else if (regLevel.IsSpecial)
                {
                    //获取最近有效的一条排班信息
                    #region
                    /*if(this.cmbDoctor.Tag != null && this.cmbDoctor.Tag.ToString() != "")
                    {
                        if( this.getLastSchema(FS.HISFC.Models.Registration.SchemaTypeNUM.Doct,regLevel,
                            "",this.cmbDoctor.Tag.ToString()) == -1)
                        {
                            this.cmbDoctor.Focus() ;
                            return ;
                        }
                    }*/
                    FS.HISFC.Models.Registration.Schema schema = (FS.HISFC.Models.Registration.Schema)this.dtBookingDate.Tag;
                    if (schema != null)
                    {
                        if (this.VerifyIsProfessor(regLevel, schema) == false)
                        {
                            this.cmbDoctor.Focus();
                            return;
                        }
                    }
                    if (this.cmbDoctor.Tag != null && this.cmbDoctor.Tag.ToString() != "")
                    {
                        if (this.DisplaySchemaTip(FS.HISFC.Models.Base.EnumSchemaType.Doct) == -1)
                        {
                            this.cmbDoctor.Focus();
                            return;
                        }
                    }
                    #endregion
                    this.dtBookingDate.Focus();
                }
                else
                {
                    if (this.cmbDoctor.Tag == null || this.cmbDoctor.Tag.ToString() == "")
                    {
                        this.cmbDoctor.Text = "";
                        this.cmbDoctor.Tag = null;
                        #region 普通号，不应该限制医生 暂时屏蔽 2012-10-06
                        //MessageBox.Show(FS.FrameWork.Management.Language.Msg("请选择有效医生"), "提示");
                        //this.cmbDoctor.SelectAll();
                        //this.cmbDoctor.Focus();

                        this.txtCardNo.Focus();
                        #endregion
                        return;
                    }

                    else if (this.cmbDoctor.Tag != null || this.cmbDoctor.Tag.ToString() != "" || (!string.IsNullOrEmpty(this.cmbDoctor.Text)))
                    {
                        FS.HISFC.Models.Base.Employee employee = this.conMgr.GetEmployeeInfo(this.cmbDoctor.Tag.ToString());
                        if (this.cmbDoctor.Text.Trim() != employee.Name)
                        {
                            this.cmbDoctor.Text = "";
                            this.cmbDoctor.Tag = null;
                            #region 普通号，不应该限制医生 暂时屏蔽 2012-10-06
                            //MessageBox.Show(FS.FrameWork.Management.Language.Msg("请选择有效医生"), "提示");
                            //this.cmbDoctor.SelectAll();
                            //this.cmbDoctor.Focus();

                            this.txtCardNo.Focus();
                            #endregion
                            return;
                        }
                        else
                        {
                            this.txtCardNo.Focus();
                        }
                    }

                    else
                    {
                        this.txtCardNo.Focus();
                    }

                }
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //反相跳转
                this.setPriorControlFocus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }
        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDoctor_TextChanged(object sender, EventArgs e)
        {
            string strFilter = "ID like '%" + this.cmbDoctor.Text + "%' or Spell_Code like '%" + this.cmbDoctor.Text + "%'"
                    + " or Name like '%" + this.cmbDoctor.Text + "%'";
            /* or Wb_Code like '%"+this.cmbDept.Text
            +"%' or Input_Code like '%"+this.cmbDept.Text+"%'";*/

            try
            {
                dvDocts.RowFilter = strFilter;
                this.AddDoctToFp(FS.FrameWork.Function.NConvert.ToBoolean(this.fpDoctor.Tag));

            }
            catch { }

            if (this.isShowMiltScreen)
            {
                // 外屏显示{67C90AAC-CFAD-4089-96F4-9F9FC82D8754}
                if (Screen.AllScreens.Length > 1)
                {
                    System.Collections.Generic.List<Object> outScreen = new System.Collections.Generic.List<object>();
                    outScreen.Add(this.regObj);//患者信息
                    outScreen.Add(this.cmbRegLevel.Text);//挂号级别
                    outScreen.Add(this.cmbDept.Text);//挂号科室
                    outScreen.Add(this.cmbDoctor.Text);//挂号医生
                    outScreen.Add(this.lbReceive.Text);//应收费用
                    outScreen.Add("");//收费员
                    this.iMultiScreen.ListInfo = outScreen;
                }
            }
            //
        }

        /// <summary>
        /// 验证教授号挂的是否是教授，付教授号挂的是否是付教授
        /// </summary>
        /// <param name="level"></param>
        /// <param name="doct"></param>
        /// <returns></returns>
        private bool VerifyIsProfessor(FS.HISFC.Models.Registration.RegLevel level, FS.HISFC.Models.Base.Employee doct)
        {
            if (this.IsDivLevel)
            {
                if (!level.IsSpecial)//特诊号不用判断
                {
                    if (this.IsProfessor(level))//教授号
                    {
                        if (!doct.IsExpert)
                        {
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("该医生是副教授,不能挂教授号"), "提示");
                            return false;
                        }
                    }
                    else//副教授
                    {
                        if (doct.IsExpert)
                        {
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("该医生是教授,不能挂副教授号"), "提示");
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        private bool VerifyIsProfessor(FS.HISFC.Models.Registration.RegLevel level, string doctID)
        {
            if (this.IsDivLevel)
            {
                if (!level.IsSpecial)//特诊号不用判断
                {
                    FS.HISFC.Models.Base.Employee p = this.conMgr.GetEmployeeInfo(doctID);
                    if (p == null)
                    {
                        MessageBox.Show("获取人员信息出错!" + this.conMgr.Err, "提示");
                        return false;
                    }

                    if (this.IsProfessor(level))//教授号
                    {
                        if (!(p.Level.ID == "2" || p.Level.ID == "21" || p.Level.ID == "17" || p.Level.ID == "33"))
                        {
                            MessageBox.Show("该医生是副教授,不能挂教授号!", "提示");
                            return false;
                        }
                    }
                    else//副教授
                    {
                        if (p.Level.ID == "2" || p.Level.ID == "21" || p.Level.ID == "17" || p.Level.ID == "33")
                        {
                            MessageBox.Show("该医生是教授,不能挂副教授号!", "提示");
                            return false;
                        }
                    }
                }
            }
            return true;
        }


        private bool VerifyIsProfessor(FS.HISFC.Models.Registration.RegLevel level, FS.HISFC.Models.Registration.Schema schema)
        {
            if (this.IsDivLevel && !this.isLevelUnlimited)
            {
                if (schema.Templet.RegLevel.ID != null && schema.Templet.RegLevel.ID != "" &&
                    level.ID != schema.Templet.RegLevel.ID)
                {
                    MessageBox.Show(schema.Templet.Doct.Name + "医生排班级别为:" + schema.Templet.RegLevel.Name + ",不能挂:" +
                        level.Name + ",请修改!", "提示");
                    return false;
                }
            }

            return true;
        }

        private bool VerifyIsProfessor(FS.HISFC.Models.Registration.RegLevel level, FS.HISFC.Models.Registration.Booking booking)
        {
            FS.HISFC.Models.Registration.Schema schema = this.SchemaMgr.GetByID(booking.DoctorInfo.Templet.ID);

            if (schema == null || schema.Templet.ID == "")
            {
                MessageBox.Show("无代码为:" + schema.Templet.ID + "的排班信息!", "提示");
                return false;
            }

            if (this.VerifyIsProfessor(level, schema) == false) return false;

            return true;
        }
        #endregion

        #region Set booking zone
        /// <summary>
        /// 变更预约流水号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOrder_TextChanged(object sender, EventArgs e)
        {
            this.txtOrder.Tag = null;
        }
        /// <summary>
        /// 预约流水号回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOrder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string ID = this.txtOrder.Text.Trim();

                if (ID != "")
                {
                    FS.HISFC.Models.Registration.Booking booking = this.bookingMgr.GetByID(ID);

                    FS.HISFC.Models.Registration.AppointmentOrder app = appointmentMgr.QueryAppointmentOrderBySerialNO(booking.ID);

                    if (app != null)
                    {
                        if (app.OrderState.Equals("3") || app.OrderState.Equals("4"))
                        {
                            MessageBox.Show("该病人为网络预约并已交费,请勿再次收费");
                            return;
                        }
                    }

                    FS.HISFC.Models.Registration.RegLevel Level = null;
                    if (booking.DoctorInfo.Templet.RegLevel.ID == null || booking.DoctorInfo.Templet.RegLevel.ID == string.Empty)
                    {
                        //{6F15CA5C-3610-4c29-B4B0-DBFA5EB39A4F}
                        //{0B4C5A74-98EB-4adc-9E52-47295201EB97}
                        if (this.cmbRegLevel.Tag == null || this.cmbRegLevel.Tag.ToString() == "")
                        {
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("请选择挂号级别"), "提示");
                            this.cmbRegLevel.Focus();
                            return;
                        }

                        //判断是专家号,就跳到医生处
                        Level = (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;
                        if (!(Level.IsSpecial || Level.IsExpert || Level.IsFaculty))
                        {
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("预约号必须是专家/专科号"), "提示");
                            this.txtOrder.Text = "";
                            this.cmbRegLevel.Focus();
                            return;
                        }
                    }
                    else
                    {



                        //add by niuxinyuan

                        //{0B4C5A74-98EB-4adc-9E52-47295201EB97}

                        this.cmbRegLevel.SelectedValueChanged -= new EventHandler(cmbRegLevel_SelectedIndexChanged);
                        this.cmbRegLevel.Tag = booking.DoctorInfo.Templet.RegLevel.ID;
                        this.cmbRegLevel.SelectedValueChanged += new EventHandler(cmbRegLevel_SelectedIndexChanged);
                        Level = (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;
                    }


                    if (booking == null)
                    {
                        MessageBox.Show("获取预约挂号信息出错!" + this.bookingMgr.Err, "提示");
                        this.txtOrder.Focus();
                        return;
                    }

                    if (booking.ID == null || booking.ID == "")
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("没有对应该流水号的预约信息"), "提示");
                        this.txtOrder.Focus();
                        return;
                    }

                    if (booking.IsSee)
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("该预约信息已看诊,请选择其他预约信息"), "提示");
                        this.txtOrder.Focus();
                        return;
                    }

                    if (Level.IsExpert && (booking.DoctorInfo.Templet.Doct.ID == null || booking.DoctorInfo.Templet.Doct.ID == ""))
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("该预约信息为专科号,不能挂专家号"), "提示");
                        this.cmbRegLevel.Focus();
                        return;
                    }

                    //if (!Level.IsExpert && booking.DoctorInfo.Templet.Doct.ID != null && booking.DoctorInfo.Templet.Doct.ID != "")
                    //{
                    //    MessageBox.Show(FS.FrameWork.Management.Language.Msg("该预约信息为专家号,不能挂专科号"), "提示");
                    //    this.cmbRegLevel.Focus();
                    //    return;
                    //}

                    if (this.IsInputTime)//中山不用判断是否超时
                    {
                        if (!booking.DoctorInfo.Templet.IsAppend)
                        {
                            DateTime current = this.bookingMgr.GetDateTimeFromSysDateTime();

                            if (booking.DoctorInfo.Templet.End < current)
                            {
                                MessageBox.Show(FS.FrameWork.Management.Language.Msg("该预约信息已经过期,不能使用"), "提示");
                                this.txtOrder.Focus();
                                return;
                            }

                            if (booking.DoctorInfo.Templet.Begin > current)
                            {
                                DialogResult dr = MessageBox.Show(FS.FrameWork.Management.Language.Msg("还没有到预约时间,是否继续进行操作") + "?", "提示",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                                if (dr == DialogResult.No)
                                {
                                    this.txtOrder.Focus();
                                    return;
                                }
                            }

                            if (booking.DoctorInfo.Templet.Begin < current &&
                                booking.DoctorInfo.Templet.End > current)
                            {
                                DialogResult dr = MessageBox.Show(FS.FrameWork.Management.Language.Msg("已经超过预约开始时间,是否继续") + "?", "提示",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                                if (dr == DialogResult.No)
                                {
                                    this.txtOrder.Focus();
                                    return;
                                }
                            }
                        }
                        else
                        {
                            if (booking.DoctorInfo.SeeDate.Date < this.bookingMgr.GetDateTimeFromSysDateTime().Date)
                            {
                                MessageBox.Show(FS.FrameWork.Management.Language.Msg("该预约信息已经过期,不能使用"), "提示");
                                this.txtOrder.Focus();
                                return;
                            }
                        }
                    }

                    //取排班
                    FS.HISFC.Models.Registration.Schema schema = this.SchemaMgr.GetByID(booking.DoctorInfo.Templet.ID);

                    this.dtBookingDate.Value = booking.DoctorInfo.SeeDate;
                    this.dtBookingDate.Tag = schema;
                    //赋值					
                    this.IsTriggerSelectedIndexChanged = this.IsTriggerEventActive;//由设置来控制kjl
                    this.cmbDept.AddItems(this.alDept);

                    this.cmbDoctor.AddItems(this.alDoct);
                    this.AddDoctToDataSet(this.alDoct);
                    this.AddDoctToFp(false);
                    this.cmbDoctor.Tag = booking.DoctorInfo.Templet.Doct.ID;//预约医生
                    this.cmbDept.Tag = booking.DoctorInfo.Templet.Dept.ID;//预约科室	

                    if (isShowBookingType)
                    {
                        this.cmbBookingType.Tag = booking.BookingTypeId;
                        this.cmbBookingType.Text = booking.BookingTypeName;
                    }


                    this.IsTriggerSelectedIndexChanged = true;



                    ///判断教授号录入是否正确
                    ///
                    if (Level.IsExpert)
                    {
                        if (this.VerifyIsProfessor(Level, booking) == false)
                        {
                            this.cmbRegLevel.Focus();
                            return;
                        }
                    }
                    this.dtBegin.Value = booking.DoctorInfo.Templet.Begin;
                    this.dtEnd.Value = booking.DoctorInfo.Templet.End;


                    this.txtOrder.Text = ID;//Text、Tag顺序不能颠倒
                    this.txtOrder.Tag = booking;

                    this.txtCardNo.Text = booking.PID.CardNO;
                    this.txtCardNo_KeyDown(new object(), new KeyEventArgs(Keys.Enter));
                }
                else
                {
                    this.dtBookingDate.Focus();
                }
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //反相跳转
                this.setPriorControlFocus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }
        /// <summary>
        /// 变更日期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtBookingDate_ValueChanged(object sender, EventArgs e)
        {
            this.QueryRegLevl();
            if (this.ucChooseDate.Visible) this.ucChooseDate.Visible = false;
            this.SetBookingTag(null);
            //清除预约信息
            this.ClearBookingInfo();

            this.lbWeek.Text = this.getWeek(this.dtBookingDate.Value);
        }
        /// <summary>
        /// 预约日期回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtBookingDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.dtBookingDate.Value.Date < this.regMgr.GetDateTimeFromSysDateTime().Date)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("预约日期不能小于当前日期"), "提示");
                    this.dtBookingDate.Focus();
                    return;
                }


                if (this.IsInputTime)
                {
                    this.dtBegin.Focus();
                }
                else
                {
                    this.txtCardNo.Focus();
                }
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.llPd_Click(new object(), new EventArgs());
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //反相跳转
                this.setPriorControlFocus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }
        /// <summary>
        /// 变更起始时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtBegin_ValueChanged(object sender, EventArgs e)
        {
            //清除预约信息
            if (this.dtBookingDate.Tag is FS.HISFC.Models.Registration.Schema)
            {
                if (((FS.HISFC.Models.Registration.Schema)this.dtBookingDate.Tag).Templet.Begin.ToString("HHmm").CompareTo(this.dtBegin.Value.ToString("HHmm")) > 0
                    ||
                    ((FS.HISFC.Models.Registration.Schema)this.dtBookingDate.Tag).Templet.End.ToString("HHmm").CompareTo(this.dtBegin.Value.ToString("HHmm")) < 0
                    )
                {
                    this.ClearBookingInfo();
                    this.SetBookingTag(null);
                }
            }
            else
            {
                this.ClearBookingInfo();
                this.SetBookingTag(null);
            }
            if (this.ucChooseDate.Visible) this.ucChooseDate.Visible = false;
        }

        /// <summary>
        /// 开始时间回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtBegin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.dtEnd.Focus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.llPd_Click(new object(), new EventArgs());
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //反相跳转
                this.setPriorControlFocus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }

        /// <summary>
        /// 变更结束时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtEnd_ValueChanged(object sender, EventArgs e)
        {
            //清除预约信息
            //如果是选择的时间超过排班的时间 则进行清空

            if (this.dtBookingDate.Tag is FS.HISFC.Models.Registration.Schema)
            {
                if (((FS.HISFC.Models.Registration.Schema)this.dtBookingDate.Tag).Templet.Begin.ToString("HHmm").CompareTo(this.dtEnd.Value.ToString("HHmm")) > 0
                    ||
                    ((FS.HISFC.Models.Registration.Schema)this.dtBookingDate.Tag).Templet.End.ToString("HHmm").CompareTo(this.dtEnd.Value.ToString("HHmm")) < 0
                    )
                {
                    this.ClearBookingInfo();
                    this.SetBookingTag(null);
                }
            }
            else
            {
                this.ClearBookingInfo();
                this.SetBookingTag(null);
            }
            if (this.ucChooseDate.Visible) this.ucChooseDate.Visible = false;
        }

        /// <summary>
        /// 结束时间回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtEnd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!isShowBookingType)
                {
                    if (this.dtBookingDate.Tag == null)
                    {
                        FS.HISFC.Models.Registration.RegLevel level = (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;
                        if (level != null)
                        {
                            FS.HISFC.Models.Registration.Schema schema = this.GetValidSchema(level);

                            this.SetBookingTag(schema);
                        }
                    }

                    this.txtCardNo.Focus();
                }
                else
                {
                    this.cmbBookingType.Focus();
                }
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.llPd_Click(new object(), new EventArgs());
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //反相跳转
                this.setPriorControlFocus();
            }
        }
        /// <summary>
        /// 选择预约时间段
        /// </summary>
        /// <param name="sender"></param>
        private void ucChooseDate_SelectedItem(FS.HISFC.Models.Registration.Schema sender)
        {
            this.ucChooseDate.Visible = false;

            if (sender == null) return;

            //如果当前时间，选择了排班，提示是否继续
            if (string.IsNullOrEmpty(sender.Templet.ID) == false)
            {
                //取当前时间
                DateTime currentTime = CommonController.Instance.GetSystemTime();

                //如果是挂当天号的
                if (sender.Templet.Begin.Date == currentTime.Date)
                {
                    //如果午别ID不一致，提示是否继续
                    if (sender.Templet.Noon.ID.Equals(CommonController.Instance.GetNoonID(currentTime)) == false)
                    {
                        if (MessageBox.Show(this, "您选择的排班午别是：" + CommonController.Instance.GetNoonName(sender.Templet.Noon.ID) + "，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        {
                            this.ClearBookingInfo();
                            this.SetDefaultBookingTime(currentTime);
                            this.SetBookingTag(null);
                            return;
                        }
                    }
                }
            }

            FS.HISFC.Models.Registration.RegLevel level = (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;
            if (level == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("请先选择挂号级别"), "提示");
                this.cmbRegLevel.Focus();
                return;
            }

            if (!level.IsSpecial && !level.IsExpert && !level.IsFaculty) return;

            Registration.RegTypeNUM regType = this.getRegType(level);

            #region 屏蔽，最后一起判断是否超出限额
            /* 
            if((regType == Registration.RegTypeNUM.Faculty ||regType == Registration.RegTypeNUM.Expert)
                &&sender.Templet.RegLmt<=sender.RegedQty)
            {
                if(MessageBox.Show("现场挂号数已经大于设号数,是否继续?","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    this.dtBookingDate.Focus() ;
                    return ;
                }
            }

            if(regType == Registration.RegTypeNUM.Special &&sender.Templet.SpeLmt<=sender.SpeReged)
            {
                if(MessageBox.Show("特诊挂号数已经大于设号数,是否继续?","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    this.dtBookingDate.Focus() ;
                    return ;
                }
            }*/
            #endregion

            //专家、专科号现场已挂号数大于现场设号数
            if ((
                ((regType == Registration.RegTypeNUM.Faculty || regType == Registration.RegTypeNUM.Expert) && (sender.Templet.RegQuota <= sender.RegedQTY && sender.Templet.TelQuota <= sender.TelingQTY))
                ||
                //或者特诊号、特诊已挂号数大于特诊设号数
                (regType == Registration.RegTypeNUM.Special && sender.Templet.SpeQuota <= sender.SpedQTY)) &&
                //并且不是加号
                !sender.Templet.IsAppend)
            {
                if (!this.IsAllowOverrun)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("已超出排班限额，不允许再进行挂号"), "提示");
                    this.dtBookingDate.Focus();
                    return;
                }
            }

            //科室
            this.IsTriggerSelectedIndexChanged = false;
            this.cmbDept.Tag = sender.Templet.Dept.ID;
            //医生
            if (sender.Templet.Doct.ID == "None")//专科号
            {
                this.cmbDoctor.Tag = "";
            }
            else
            {
                this.cmbDoctor.Tag = sender.Templet.Doct.ID;
            }
            this.IsTriggerSelectedIndexChanged = true;

            //预约时间段
            this.SetBookingTime(sender);
            this.txtCardNo.Focus();
        }
        /// <summary>
        /// 显示预约时间段列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void llPd_Click(object sender, EventArgs e)
        {
            if (this.ucChooseDate.Visible)
            {
                this.ucChooseDate.Visible = false;
                this.dtBookingDate.Focus();
            }
            else
            {
                DateTime bookingDate = this.dtBookingDate.Value;
                DateTime current = this.bookingMgr.GetDateTimeFromSysDateTime();

                if (bookingDate.Date < current.Date)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("预约日期不能小于当前日期"), "提示");
                    this.dtBookingDate.Focus();
                    return;
                }

                FS.HISFC.Models.Registration.RegLevel level = (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;
                if (level == null)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("请先选择挂号级别"), "提示");
                    this.cmbRegLevel.Focus();
                    return;
                }

                if (!level.IsFaculty && !level.IsExpert && !level.IsSpecial) return;

                string deptID = this.cmbDept.Tag.ToString();
                string doctID = this.cmbDoctor.Tag.ToString();

                //专科号,挂号科室不能为空
                if (level.IsFaculty)
                {
                    #region dept
                    if (deptID == null || deptID == "")
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("专科号必须指定挂号科室"), "提示");
                        this.cmbDept.Focus();
                        return;
                    }
                    this.SetDeptZone(deptID, bookingDate, level);


                    if (this.ucChooseDate.Count > 0)
                    {
                        this.ucChooseDate.Visible = true;
                        this.ucChooseDate.Focus();
                    }
                    else if (this.ucChooseDate.Bookings.Count > 0)
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("没有符合条件的排班信息,请重新选择预约日期"), "提示");
                        this.dtBookingDate.Focus();
                        return;
                    }
                    else
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("专科没有排班"), "提示");
                        this.dtBookingDate.Focus();
                        return;
                    }
                    #endregion
                }
                //专家号,必须指定看诊医生
                if (level.IsExpert || level.IsSpecial)
                {
                    #region doct
                    if (doctID == null || doctID == "")
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("专家号必须指定出诊专家"), "提示");
                        this.cmbDoctor.Focus();
                        return;
                    }

                    this.SetDoctZone(doctID, bookingDate, level);

                    if (this.ucChooseDate.Count > 0)
                    {
                        this.ucChooseDate.Visible = true;
                        this.ucChooseDate.Focus();
                    }
                    else if (this.ucChooseDate.Bookings.Count > 0)
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("没有符合条件的排班信息,请重新选择预约日期"), "提示");
                        this.dtBookingDate.Focus();
                        return;
                    }
                    else
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("专家没有排班"), "提示");
                        this.dtBookingDate.Focus();
                        return;
                    }
                    #endregion
                }
            }
        }

        #endregion

        #region txtCardNo
        /// <summary>
        /// 病历号回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCardNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtCardNo.Tag = null;

                string cardNo = this.txtCardNo.Text.Trim();
                if (string.IsNullOrEmpty(cardNo))
                {
                    if (this.IsShowTipsWhenCardNoIsNull)
                    {
                        if (MessageBox.Show("病历号为空，是否根据姓名检索？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                        {
                            this.txtCardNo.Focus();
                            return;
                        }
                        else
                        {
                            //直接跳到姓名处,可根据输入的姓名检索患者信息
                            this.txtName.Focus();
                            return;
                        }
                    }
                    else
                    {
                        //直接跳到姓名处,可根据输入的姓名检索患者信息
                        this.txtName.Focus();
                        return;
                    }
                }

                if (this.ValidCardNO(cardNo) < 0)
                {
                    this.txtCardNo.Focus();
                    this.txtCardNo.SelectAll();
                    return;
                }

                FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();

                if (this.isReadCard == true) //|| this.myYBregObj.SIMainInfo.Memo != null || this.myYBregObj.SIMainInfo.Memo.Trim() != "")
                {
                    cardNo = cardNo.PadLeft(10, '0');
                    this.txtCardNo.Text = cardNo;

                    //医保患者
                    FS.HISFC.Models.RADT.PatientInfo mySiRegobj = new FS.HISFC.Models.RADT.PatientInfo();
                    FS.HISFC.BizProcess.Integrate.RADT radt = new FS.HISFC.BizProcess.Integrate.RADT();
                    mySiRegobj = radt.QueryComPatientInfo(cardNo);
                    if (mySiRegobj != null)
                    {
                        this.regObj.PhoneHome = mySiRegobj.PhoneHome;
                        this.regObj.PhoneBusiness = mySiRegobj.PhoneBusiness;
                    }
                    this.regObj.PID.CardNO = cardNo;

                    #region 不允许录入汉字
                    if (FS.FrameWork.Public.String.ValidMaxLengh(cardNo, 10) == false)
                    {
                        MessageBox.Show("病历号不能输入汉字!", "提示");
                        this.txtCardNo.Focus();
                        return;
                    }
                    #endregion

                    this.txtPhone.Focus();

                }
                else
                {
                    //用作查找卡记录时的挂号标记
                    //accountCard.Memo = "1";
                    int rev = this.feeMgr.ValidMarkNO(cardNo, ref accountCard);

                    if (rev > 0)
                    {
                        cardNo = accountCard.Patient.PID.CardNO;
                        decimal vacancy = 0m;
                        if (feeMgr.GetAccountVacancy(cardNo, ref vacancy) > 0)
                        {
                            this.cmbCardType.Enabled = true;

                            this.txtIdNO.Enabled = false;
                            this.tbSIBalanceCost.Text = vacancy.ToString();
                        }
                        else
                        {
                            this.tbSIBalanceCost.Text = string.Empty;
                        }
                    }
                    //返回错误了
                    else if (rev == -1)
                    {
                        if (ISearchCard != null)
                        {
                            if (ISearchCard.PostMarkNo(ref cardNo) == 1)
                            {
                                this.txtCardNo.Text = cardNo;
                                this.txtCardNo.Focus();
                                
                                //模拟回车事件
                                //SendKeys.Send("{ENTER}");
                                System.Windows.Forms.KeyEventArgs eKeyEvent = new KeyEventArgs(Keys.Enter);
                                this.txtCardNo_KeyDown(null, eKeyEvent);

                            }
                            else
                            {
                                this.txtCardNo.Clear();
                            }
                            return;
                        }
                        //else
                        //{
                        //    MessageBox.Show("该卡未发放", "提示");
                        //    this.txtCardNo.Focus();
                        //    this.txtCardNo.SelectAll();
                        //    return;

                        //}
                    }
                    cardNo = cardNo.PadLeft(10, '0');
                    #region 不允许录入汉字
                    if (FS.FrameWork.Public.String.ValidMaxLengh(cardNo, 10) == false)
                    {
                        MessageBox.Show("病历号不能输入汉字!", "提示");
                        this.txtCardNo.Focus();
                        this.txtCardNo.SelectAll();
                        return;
                    }
                    #endregion

                    #region 检索患者信息
                    this.regObj = this.getRegInfo(cardNo);
                    if (regObj == null)
                    {
                        this.txtCardNo.Focus();
                        this.txtCardNo.SelectAll();
                        return;
                    }
                    #endregion

                    #region 赋值

                    //记录当前使用的就诊卡号和就诊卡类型
                    this.regObj.Card.CardType = accountCard.MarkType.Clone();
                    this.regObj.Card.ID = accountCard.MarkNO;

                    // {23BA226E-A1E5-4a0b-A1D5-92FA97AF3E85}
                    this.txtCardNo.Tag = accountCard;


                    this.txtCardNo.Text = this.regObj.PID.CardNO;
                    ArrayList accountCardArr = this.accountMgr.GetMarkByCardNo(this.regObj.PID.CardNO, this.regObj.Card.CardType.ID,"1");
                    if (accountCardArr != null && accountCardArr.Count > 0)
                    {
                        FS.FrameWork.Models.NeuObject accountCardTmp = accountCardArr[0] as FS.FrameWork.Models.NeuObject;
                        this.txtMarkNo.Text = accountCardTmp.Name;
                    }
                    
                    this.txtName.Text = this.regObj.Name;

                    if (this.regObj.Birthday != DateTime.MinValue)
                        this.dtBirthday.Value = this.regObj.Birthday;

                    this.cmbSex.Tag = this.regObj.Sex.ID;
                    this.cmbPayKind.Tag = this.regObj.Pact.ID;
                    if (this.cmbPayKind.Tag == null || string.IsNullOrEmpty(this.cmbPayKind.Tag.ToString()))
                    {
                        this.cmbPayKind.Tag = this.DefaultPactID;
                    }
                    this.txtMcardNo.Text = this.regObj.SSN;
                    this.txtPhone.Text = this.regObj.PhoneHome;
                    this.txtAddress.Text = this.regObj.AddressHome;
                    this.txtIdNO.Text = this.regObj.IDCard;

                    if (!string.IsNullOrEmpty(regObj.PatientType))
                    {
                        cmbPatientType.Tag = regObj.PatientType;
                    }
                    this.cmbCardType.Tag = this.regObj.CardType.ID;

                    this.setAge(this.regObj.Birthday);
                    this.GetCost();

                    FS.HISFC.Models.Base.PactInfo pact = conMgr.GetPactUnitInfoByPactCode(this.regObj.Pact.ID);
                    this.getPayRate(pact);

                    #endregion

                    if (this.isShowMiltScreen)
                    {
                        // 外屏显示
                        if (Screen.AllScreens.Length > 1)
                        {
                            System.Collections.Generic.List<Object> outScreen = new System.Collections.Generic.List<object>();
                            outScreen.Add(this.regObj);//患者信息
                            outScreen.Add(this.cmbRegLevel.Text);//挂号级别
                            outScreen.Add(this.cmbDept.Text);//挂号科室
                            outScreen.Add(this.cmbDoctor.Text);//挂号医生
                            outScreen.Add(this.lbReceive.Text);//应收费用
                            outScreen.Add("");//收费员信息（非初始化界面值为空）
                            this.iMultiScreen.ListInfo = outScreen;
                        }
                    }

                }
                if (this.IsInputName) this.txtName.Focus();
                else { this.cmbSex.Focus(); }

                this.cmbRegLevel.Focus();   //合同单位设置为焦点


            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //反相跳转
                this.setPriorControlFocus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
            else if (e.KeyCode == Keys.Space)
            {
                FS.HISFC.Models.RADT.PatientInfo p = new FS.HISFC.Models.RADT.PatientInfo();
                if (FS.HISFC.Components.Common.Classes.Function.QueryComPatientInfo(ref p) == 1)
                {
                    this.txtCardNo.Text = p.PID.CardNO;
                    this.txtCardNo_KeyDown(null, new KeyEventArgs(Keys.Enter));
                }
            }
        }

        /// <summary>
        /// 根据病历号获得患者挂号信息
        /// </summary>
        /// <param name="CardNo"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Registration.Register getRegInfo(string CardNo)
        {
            if (string.IsNullOrEmpty(CardNo))
            {
                return null;
            }

            FS.HISFC.Models.Registration.Register ObjReg = new FS.HISFC.Models.Registration.Register();
            FS.HISFC.BizProcess.Integrate.RADT radt = new FS.HISFC.BizProcess.Integrate.RADT();
            FS.HISFC.Models.RADT.PatientInfo p;
            int regCount = this.regMgr.QueryRegiterByCardNO(CardNo);

            if (regCount == 1)
            {
                ObjReg.IsFirst = false;
            }
            else
            {
                if (regCount == 0)
                {
                    ObjReg.IsFirst = true;

                }
                else
                {
                    return null;
                }
            }
            //先检索患者基本信息表,看是否存在该患者信息
            p = radt.QueryComPatientInfo(CardNo);

            if (p == null || p.Name == "")
            {
                //不存在基本信息
                ObjReg.PID.CardNO = CardNo;
                //ObjReg.IsFirst = true;
                ObjReg.Sex.ID = "M";
                ObjReg.Pact.ID = this.DefaultPactID;

            }
            else
            {
                //存在患者基本信息,取基本信息

                ObjReg.PID.CardNO = CardNo;
                ObjReg.Name = p.Name;
                ObjReg.Sex.ID = p.Sex.ID;
                ObjReg.Birthday = p.Birthday;
                ObjReg.Pact.ID = p.Pact.ID;
                ObjReg.Pact.PayKind.ID = p.Pact.PayKind.ID;
                ObjReg.SSN = p.SSN;
                ObjReg.PhoneHome = p.PhoneHome;
                ObjReg.AddressHome = p.AddressHome;
                ObjReg.IDCard = p.IDCard;
                ObjReg.NormalName = p.NormalName;
                ObjReg.IsEncrypt = p.IsEncrypt;
                //{6B6167F7-3A9B-4f6c-9326-C5CD6AA3AC98}
                ObjReg.IDCard = p.IDCard;

                if (p.IsEncrypt == true)
                {
                    ObjReg.Name = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(p.NormalName);
                }
                this.chbEncrpt.Checked = p.IsEncrypt;
                //ObjReg.IsFirst = false;

                if (this.validCardType(p.IDCardType.ID))//借用Memo存储证件类别
                {
                    ObjReg.CardType.ID = p.IDCardType.ID;

                }
            }

            return ObjReg;
        }

        /// <summary>
        /// 验证证件类别是否有效
        /// </summary>
        /// <param name="cardType"></param>
        /// <returns></returns>
        private bool validCardType(string cardType)
        {
            bool found = false;

            foreach (FS.FrameWork.Models.NeuObject obj in this.cmbCardType.alItems)
            {
                if (obj.ID == cardType)
                {
                    found = true;
                    break;
                }
            }
            return found;
        }
        /// <summary>
        /// 检索患者预约信息
        /// </summary>
        /// <param name="CardNo"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Registration.Register GetBookingInfo(string CardNo)
        {
            FS.HISFC.Models.Registration.Booking booking = null;// this.bookingMgr.Query(CardNo);

            if (booking == null)
            {
                MessageBox.Show("检索患者预约信息时出错!" + this.bookingMgr.Err, "提示");
                return null;
            }

            FS.HISFC.Models.Registration.Register regInfo = new FS.HISFC.Models.Registration.Register();
            //没有预约信息
            //if (booking.ID == null || booking.ID == "")
            //{
            //    regInfo.PID.CardNO = CardNo;
            //    regInfo.IsFirst = true;
            //    regInfo.Sex.ID = "M";
            //    regInfo.Pact.ID = this.DefaultPactID;
            //}
            //else
            //{
            //    regInfo = (FS.HISFC.Models.RADT.Patient)booking;
            //    regInfo.PID.CardNO = CardNo;
            //    regInfo.IsFirst = true;
            //    regInfo.Pact.ID = this.DefaultPactID;
            //}

            return regInfo;
        }
        /// <summary>
        /// Set Age
        /// </summary>
        /// <param name="birthday"></param>
        private void setAge(DateTime birthday)
        {
            this.txtAge.Text = "";

            if (birthday == DateTime.MinValue)
            {
                return;
            }

            DateTime current;
            int year = 0, month = 0, day = 0;

            current = this.regMgr.GetDateTimeFromSysDateTime();
            this.regMgr.GetAge(birthday, current, ref year, ref month, ref day);
            //year = current.Year - birthday.Year;
            //month = current.Month - birthday.Month;
            //day = current.Day - birthday.Day;

            if (year > 1)
            {
                this.txtAge.Text = year.ToString();
                this.cmbUnit.SelectedIndexChanged -= this.cmbUnit_SelectedIndexChanged;
                this.cmbUnit.SelectedIndex = 0;
                this.cmbUnit.SelectedIndexChanged += this.cmbUnit_SelectedIndexChanged;
            }
            else if (year == 1)
            {
                if (month >= 0)//一岁
                {
                    this.txtAge.Text = year.ToString();
                    this.cmbUnit.SelectedIndexChanged -= this.cmbUnit_SelectedIndexChanged;
                    this.cmbUnit.SelectedIndex = 0;
                    this.cmbUnit.SelectedIndexChanged += this.cmbUnit_SelectedIndexChanged;
                }
                else
                {
                    this.txtAge.Text = Convert.ToString(12 + month);
                    this.cmbUnit.SelectedIndexChanged -= this.cmbUnit_SelectedIndexChanged;
                    this.cmbUnit.SelectedIndex = 1;
                    this.cmbUnit.SelectedIndexChanged += this.cmbUnit_SelectedIndexChanged;
                }
            }
            else if (month > 0)
            {
                this.txtAge.Text = month.ToString();
                this.cmbUnit.SelectedIndexChanged -= this.cmbUnit_SelectedIndexChanged;
                this.cmbUnit.SelectedIndex = 1;
                this.cmbUnit.SelectedIndexChanged += this.cmbUnit_SelectedIndexChanged;
            }
            else if (day > 0)
            {
                this.txtAge.Text = day.ToString();
                this.cmbUnit.SelectedIndexChanged -= this.cmbUnit_SelectedIndexChanged;
                this.cmbUnit.SelectedIndex = 2;
                this.cmbUnit.SelectedIndexChanged += this.cmbUnit_SelectedIndexChanged;
            }
            this.txtAge.Tag = this.txtAge.Text;
            this.GetCost();

        }

        /// <summary>
        /// 得到患者应付
        /// </summary>		
        /// <returns></returns>
        private int GetCost()
        {
            this.lbReceive.Text = "";

            if (this.cmbRegLevel.Tag == null || this.cmbRegLevel.Tag.ToString() == "" ||
                this.cmbPayKind.Tag == null || this.cmbPayKind.Tag.ToString() == "")
                return 0;//没录入完全，返回

            string regLvlID, pactID;
            decimal regfee = 0, chkfee = 0, digfee = 0, othfee = 0, owncost = 0, pubcost = 0;

            regLvlID = this.cmbRegLevel.Tag.ToString();
            pactID = this.cmbPayKind.Tag.ToString();

            int rtn = this.GetRegFee(pactID, regLvlID, ref regfee, ref chkfee, ref digfee, ref othfee);

            if (rtn == -1) return 0;

            #region 超过65岁普通号挂号费处理
            if (!string.IsNullOrEmpty(strDealPTregLevels) && isDealPTreg)
            {
                string[] strLevels = this.strDealPTregLevels.Split(',');
                for (int i = 0; i <= strLevels.Length - 1; ++i)
                {
                    if (null == this.cmbRegLevel.Tag) break;
                    if (this.cmbRegLevel.Tag.ToString() == strLevels[i])
                    {
                        int year = 0, month = 0, day = 0;
                        this.regFeeMgr.GetAge(this.dtBirthday.Value, this.conMgr.GetDateTimeFromSysDateTime(), ref year, ref month, ref day);
                        //大于65岁,dealPTregAge需要维护
                        if (year >= dealPTregAge && regfee >= 1)
                        {
                            regfee = regfee - 1;
                            break;
                        }
                    }
                }

            }
            #endregion

            #region 挂号费特殊处理

            if (this.IsCountSpecialRegFee && this.ICountSpecialRegFee != null)
            {
                ICountSpecialRegFee.CountSpecialRegFee(this.dtBirthday.Value, this.txtName.Text, this.txtIdNO.Text, ref regfee, ref digfee, ref othfee, ref this.regObj);
            }

            #endregion

            //获得患者应收、报销
            if (this.regObj == null || this.regObj.PID.CardNO == "")
            {
                this.GetCost(regfee, chkfee, digfee, ref othfee, ref owncost, ref pubcost, "");
            }
            else
            {
                this.GetCost(regfee, chkfee, digfee, ref othfee, ref owncost, ref pubcost, this.regObj.PID.CardNO);
            }

            // 新加判断，是否收取卡费用
            decimal decCardFee = 0;
            if (chbCardFee.Visible && chbCardFee.Checked)
            {
                AccountCard accCard = this.txtCardNo.Tag as AccountCard;
                if (accCard != null)
                {
                    FS.HISFC.Models.Base.Const obj = accCard.MarkType as FS.HISFC.Models.Base.Const;
                    if (obj != null)
                    {
                        decCardFee = FS.FrameWork.Function.NConvert.ToDecimal(obj.UserCode);
                    }
                }
            }



            //this.lbReceive.Text = owncost.ToString();

            this.lbReceive.Text = (owncost + decCardFee).ToString();

            return 0;
        }
        /// <summary>
        /// 获取挂号费
        /// </summary>
        /// <param name="pactID"></param>
        /// <param name="regLvlID"></param>
        /// <param name="regFee"></param>
        /// <param name="chkFee"></param>
        /// <param name="digFee"></param>
        /// <param name="othFee"></param>
        /// <returns></returns>
        private int GetRegFee(string pactID, string regLvlID, ref decimal regFee, ref decimal chkFee, ref decimal digFee, ref decimal othFee)
        {
            FS.HISFC.Models.Registration.RegLvlFee p = this.regFeeMgr.Get(pactID, regLvlID);
            if (p == null)//找不到就默认自费
            {
                p = this.regFeeMgr.Get("1", regLvlID);
            }
            if (p.ID == null || p.ID == "")//没有维护挂号费
            {
                return 1;
            }

            regFee = p.RegFee;
            chkFee = p.ChkFee;
            digFee = p.OwnDigFee;
            othFee = p.OthFee;

            //判断是否只收取挂号费
            switch (this.IFeeDiagReg)
            {
                case 1:
                    // 收取挂号费
                    chkFee = 0;
                    digFee = 0;
                    break;
                case 2:
                    // 收取诊金
                    regFee = 0;
                    break;
                case 3:
                    // 不收取诊金、挂号
                    regFee = 0;
                    chkFee = 0;
                    digFee = 0;
                    break;

                default:
                    // 默认都收取
                    break;

            }

            return 0;
        }

        /// <summary>
        /// 将应缴金额转为挂号实体,
        /// 属性不能作为ref参数传递 TNND
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private int ConvertRegFeeToObject(FS.HISFC.Models.Registration.Register obj)
        {
            decimal regFee = 0, chkFee = 0, digFee = 0, othFee = 0;
            //直接用挂号实体作为ref参数？
            int rtn = this.GetRegFee(obj.Pact.ID, obj.DoctorInfo.Templet.RegLevel.ID,
                          ref regFee, ref chkFee, ref digFee, ref othFee);

            if (this.IsCountSpecialRegFee && this.ICountSpecialRegFee != null)
            {
                ICountSpecialRegFee.CountSpecialRegFee(this.dtBirthday.Value, this.txtName.Text, this.txtIdNO.Text, ref regFee, ref digFee, ref othFee, ref obj);
            }

            obj.RegLvlFee.RegFee = regFee;
            obj.RegLvlFee.ChkFee = chkFee;
            obj.RegLvlFee.OwnDigFee = digFee;
            obj.RegLvlFee.OthFee = othFee;


            #region 超过65岁普通号挂号费处理
            if (!string.IsNullOrEmpty(strDealPTregLevels) && isDealPTreg)
            {
                string[] strLevels = this.strDealPTregLevels.Split(',');
                for (int i = 0; i <= strLevels.Length - 1; ++i)
                {
                    if (obj.DoctorInfo.Templet.RegLevel.ID == strLevels[i])
                    {
                        //大于65岁
                        if (obj.Birthday.AddYears(65) < this.conMgr.GetDateTimeFromSysDateTime() && regFee >= 1)
                        {
                            obj.RegLvlFee.RegFee = regFee - 1;

                        }
                    }
                }

            }
            #endregion

            return rtn;
        }

        /// <summary>
        /// 获得患者应交金额、报销金额
        /// </summary>
        /// <param name="regFee"></param>
        /// <param name="chkFee"></param>
        /// <param name="digFee"></param>
        /// <param name="othFee"></param>
        /// <param name="digPub"></param>
        /// <param name="ownCost"></param>
        /// <param name="pubCost"></param>
        /// <param name="cardNo"></param>		
        private List<AccountCardFee> GetCost(decimal regFee, decimal chkFee, decimal digFee, ref decimal othFee, ref decimal ownCost, ref decimal pubCost, string cardNo)
        {
            List<AccountCardFee> lstAccFee = new List<AccountCardFee>();
            AccountCardFee cardFee = null;

            if (regFee > 0)
            {
                cardFee = BuildAccountCardFee(AccCardFeeType.RegFee, regFee, 0);
                lstAccFee.Add(cardFee);
            }
            else if (regFee == 0 && this.IsPrintIfZero)
            {
                //如果挂号费为0的情况，看是否打印发票
                cardFee = BuildAccountCardFee(AccCardFeeType.RegFee, regFee, 0);
                lstAccFee.Add(cardFee);
            }

            if (this.chbCheckFee.Checked)
            {
                if (chkFee > 0)
                {
                    cardFee = BuildAccountCardFee(AccCardFeeType.ChkFee, chkFee, 0);
                    lstAccFee.Add(cardFee);
                }
            }

            if (this.otherFeeType == "0")
            {
                /*
                 * 空调费收取算法
                 * 患者上、下午挂号分别收取一次空调费。
                 * 同一患者同一午别挂多张号只收取一次空调费
                 * 空调费用othFee表示
                 */

                #region 空调费
                //没有输入患者信息时，默认显示收取空调费
                if (cardNo == null || cardNo == "")
                {
                    ///
                }
                else
                {
                    DateTime regDate = this.dtBookingDate.Value.Date;

                    if (this.dtBegin.Value.ToString("HHmm") == "0000")
                    {
                        regDate = DateTime.Parse(regDate.ToString("yyyy-MM-dd") + " " + this.regMgr.GetSysDateTime("HH24:mi:ss"));
                    }
                    else
                    {
                        regDate = DateTime.Parse(regDate.ToString("yyyy-MM-dd") + " " + this.dtBegin.Value.ToString("HH:mm:ss"));
                    }

                    ///按病历号查询患者最近一次挂号信息
                    ArrayList alRegs = this.regMgr.Query(cardNo, regDate.Date);

                    string currentNoon = this.getNoon(regDate);

                    if (alRegs != null)
                    {
                        foreach (FS.HISFC.Models.Registration.Register obj in alRegs)
                        {
                            //未挂号或者最后一次挂号时间同当前时间午别不同,都收取挂号费
                            if (obj.DoctorInfo.SeeDate.Date == regDate.Date)
                            {
                                if (obj.DoctorInfo.Templet.Noon.ID != currentNoon)
                                {
                                    ///
                                }
                                else
                                {
                                    othFee = 0;
                                    break;
                                }
                            }
                        }
                    }
                }

                if (othFee > 0)
                {
                    cardFee = BuildAccountCardFee(AccCardFeeType.AirConFee, othFee, 0);
                    lstAccFee.Add(cardFee);
                }

                #endregion
            }
            else if (this.otherFeeType == "1" && this.IsShowChbBookFee)
            {
                // 病历本费
                if (!this.chbBookFee.Checked) //通过界面选择
                {
                    othFee = 0;
                }
                if (othFee > 0)
                {
                    cardFee = BuildAccountCardFee(AccCardFeeType.CaseFee, othFee, 0);
                    lstAccFee.Add(cardFee);
                }
            }
            else if (this.otherFeeType == "2")
            {
                // 其他费用
                if (othFee > 0)
                {
                    cardFee = BuildAccountCardFee(AccCardFeeType.OthFee, othFee, 0);
                    lstAccFee.Add(cardFee);
                }
            }

            if (this.IsPubDiagFee)
            {
                if (digFee > 0)
                {
                    cardFee = BuildAccountCardFee(AccCardFeeType.DiaFee, 0, digFee);
                    lstAccFee.Add(cardFee);
                }

                ownCost = regFee + chkFee + othFee;
                pubCost = digFee;
            }
            else
            {
                if (digFee > 0)
                {
                    cardFee = BuildAccountCardFee(AccCardFeeType.DiaFee, digFee, 0);
                    lstAccFee.Add(cardFee);
                }

                ownCost = regFee + chkFee + othFee + digFee;
                pubCost = 0;
            }

            return lstAccFee;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private AccountCardFee BuildAccountCardFee(AccCardFeeType feeType, decimal ownCost, decimal pubCost)
        {
            AccountCardFee cardFee = new AccountCardFee();
            cardFee.FeeType = feeType;
            cardFee.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
            cardFee.IStatus = 1;
            cardFee.Own_cost = ownCost;
            cardFee.Pub_cost = pubCost;
            cardFee.Tot_cost = ownCost + pubCost;

            return cardFee;
        }

        /// <summary>
        /// 将应缴金额转为挂号实体,
        /// 属性不能作为ref参数传递 TNND
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="lstCardFee"></param>
        private void ConvertCostToObject(FS.HISFC.Models.Registration.Register obj, out List<AccountCardFee> lstCardFee)
        {
            lstCardFee = null;
            decimal othFee = 0, ownCost = 0, pubCost = 0;
            othFee = obj.RegLvlFee.OthFee; //add by niux
            //直接用挂号实体作为ref参数？
            if (this.IsCountSpecialRegFee && this.ICountSpecialRegFee != null)
            {
                decimal regFee = obj.RegLvlFee.RegFee, digFee = obj.RegLvlFee.OwnDigFee;
                ICountSpecialRegFee.CountSpecialRegFee(this.dtBirthday.Value, this.txtName.Text, this.txtIdNO.Text, ref regFee, ref digFee, ref othFee, ref obj);
                lstCardFee = this.GetCost(regFee, obj.RegLvlFee.ChkFee, digFee, ref othFee, ref ownCost, ref pubCost, this.regObj.PID.CardNO);
            }
            else
            {
                lstCardFee = this.GetCost(obj.RegLvlFee.RegFee, obj.RegLvlFee.ChkFee, obj.RegLvlFee.OwnDigFee,
                        ref othFee, ref ownCost, ref pubCost, this.regObj.PID.CardNO);
            }

            obj.RegLvlFee.OthFee = othFee;
            obj.OwnCost = ownCost;
            obj.PubCost = pubCost;

        }
        #endregion

        #region txtName
        /// <summary>
        /// 患者姓名回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.IsInputName && this.txtName.Text.Trim() == "")
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("请输入患者姓名"), "提示");
                    this.txtName.Focus();
                    return;
                }

                //没有输入病历号,需根据患者姓名检索挂号信息
                if (this.txtCardNo.Text.Trim() == "")
                {
                    string CardNo = this.GetCardNoByName(this.txtName.Text.Trim());

                    if (CardNo == "")
                    {
                        int autoGetCardNO;
                        autoGetCardNO = regMgr.AutoGetCardNO();
                        if (autoGetCardNO == -1)
                        {
                            MessageBox.Show("未能成功自动产生卡号，请手动输入！", "提示");
                        }
                        else
                        {
                            this.txtCardNo.Text = autoGetCardNO.ToString().PadLeft(10, '0');
                            this.regObj = this.getRegInfo(this.txtCardNo.Text);
                        }
                        cmbSex.Focus();
                        return;
                    }
                    else
                    {
                        //{0C30F7F0-2BCF-4c03-BA6E-D7E22A638E97}
                        this.txtCardNo.Enabled = false;
                    }
                    this.txtCardNo.Text = CardNo;

                    this.txtCardNo_KeyDown(new object(), new KeyEventArgs(Keys.Enter));
                }
                else
                {
                    //this.cmbSex.Focus();
                    this.setNextControlFocus();
                }
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //反相跳转
                this.setPriorControlFocus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }

        }
        /// <summary>
        /// 通过患者姓名检索患者挂号信息
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        private string GetCardNoByName(string Name)
        {
            frmQueryPatientByName f = new frmQueryPatientByName();

            if (f.QueryByName(Name) > 0)
            {
                DialogResult dr = f.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    string CardNo = f.SelectedCardNo;
                    f.Dispose();
                    return CardNo;
                }

                f.Dispose();
            }

            return "";
        }


        /// <summary>
        /// 通过患者姓名检索患者挂号信息
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        private string GetCardNoByMCardNO(string MCardNO)
        {
            frmQueryPatientByName f = new frmQueryPatientByName();

            if (f.QueryByMCardNO(MCardNO) > 0)
            {
                DialogResult dr = f.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    string CardNo = f.SelectedCardNo;
                    f.Dispose();
                    return CardNo;
                }

                f.Dispose();
            }

            return "";
        }
        #endregion

        #region KeyEnter
        private void cmbSex_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.cmbSex.Text.Trim().Length > 0 && this.cmbSex.Text.Trim().Length < 2)
                {
                    try
                    {
                        int intsex = int.Parse(this.cmbSex.Text);
                        switch (intsex)
                        {
                            case 1:
                                this.cmbSex.Tag = "M";
                                break;
                            case 2:
                                this.cmbSex.Tag = "F";
                                break;
                            default:
                                break;
                        }
                    }
                    catch
                    {

                    }

                }
                if (this.cmbSex.Tag == null || this.cmbSex.Tag.ToString() == "")
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("请选择患者性别"), "提示");
                    this.cmbSex.Focus();
                    return;
                }
                if (IsBirthdayEnd)
                {
                    this.dtBirthday.Focus();
                }
                else
                {
                    cmbPayKind.Focus();
                }
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //反相跳转
                this.setPriorControlFocus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }

        private void txtAge_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.GetBirthday();

                this.cmbUnit.Focus();
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //反相跳转
                this.setPriorControlFocus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }

        /// <summary>
        /// 获取出生日期
        /// </summary>
        private void GetBirthday()
        {
            string age = this.txtAge.Text.Trim();
            int i = 0;

            if (age == "") age = "0";

            try
            {
                i = int.Parse(age);
            }
            catch (Exception e)
            {
                string error = e.Message;
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("输入年龄不正确,请重新输入"), "提示");
                this.txtAge.Focus();
                return;
            }

            if (FS.FrameWork.Function.NConvert.ToInt32(age) > 110)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("输入年龄过大,请重新输入"), "提示");
                this.txtAge.Focus();
                return;
            }
            ///
            ///

            DateTime birthday = DateTime.MinValue;

            this.GetBirthday(i, this.cmbUnit.Text, ref birthday);

            if (birthday < this.dtBirthday.MinDate)
            {
                MessageBox.Show("年龄不能过大!", "提示");
                this.txtAge.Focus();
                return;
            }

            this.dtBirthday.Value = birthday;
        }
        /// <summary>
        /// 根据年龄得到出生日期
        /// </summary>
        /// <param name="age"></param>
        /// <param name="ageUnit"></param>
        /// <param name="birthday"></param>
        private void GetBirthday(int age, string ageUnit, ref DateTime birthday)
        {
            DateTime current = this.regMgr.GetDateTimeFromSysDateTime();
            int year = 0, month = 0, day = 0;
            if (FS.FrameWork.Function.NConvert.ToInt32(this.txtAge.Tag) == age)
            {
                this.regMgr.GetAge(this.dtBirthday.Value, current, ref year, ref month, ref day);
            }
            if (ageUnit == "岁")
            {
                birthday = this.regMgr.GetDateFromAge(current, age, month, day);
            }
            else if (ageUnit == "月")
            {
                birthday = this.regMgr.GetDateFromAge(current, 0, age, 0);
            }
            else if (ageUnit == "天")
            {
                birthday = this.regMgr.GetDateFromAge(current, 0, 0, age);
            }

        }

        private void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetBirthday();
        }
        private void cmbUnit_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtPhone.Focus();
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //反相跳转
                this.setPriorControlFocus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }

        private void cmbPayKind_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.cmbPayKind.Tag == null || this.cmbPayKind.Tag.ToString() == "")
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("请选择患者结算类别"), "提示");
                    this.cmbPayKind.Focus();
                    return;
                }

                if (this.ValidCombox(FS.FrameWork.Management.Language.Msg("您选择的合同单位有误或不在合同单位的下拉列表中,请重新选择")) < 0)
                {
                    //this.cmbPayKind.Focus();
                    return;
                }

                //判断是否需要输入医疗证号,如果需要,焦点跳到医疗证号处
                FS.HISFC.Models.Base.PactInfo pact = conMgr.GetPactUnitInfoByPactCode(this.cmbPayKind.Tag.ToString());
                if (pact == null)
                {
                    MessageBox.Show("检索合同单位时出错!" + conMgr.Err, "提示");
                    this.cmbPayKind.Focus();
                    return;
                }

                if (pact.ID == null || pact.ID == "")//没有检索到
                {
                    MessageBox.Show("数据库已经变动,请退出窗口重新登陆!", "提示");
                    return;
                }

                this.GetCost();

                this.getPayRate(pact);

                if (pact.IsNeedMCard /*&& IsBirthdayEnd*/)
                {
                    this.txtMcardNo.Focus();
                }
                else
                {
                    this.txtAge.Focus();
                }
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //反相跳转
                //this.setPriorControlFocus() ;
                this.dtBirthday.Focus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }

        /// <summary>
        /// 显示合同单位支付比率
        /// </summary>
        /// <param name="pact"></param>
        private void getPayRate(FS.HISFC.Models.Base.PactInfo pact)
        {
            this.lbTot.Text = "";

            if (pact != null && pact.Rate.PayRate != 0)
            {
                decimal rate = pact.Rate.PayRate * 100;
                this.lbTot.Text = rate.ToString("###") + "%";
            }
        }

        private void txtMcardNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //没有输入病历号,需根据患者医疗证号检索挂号信息
                if (string.IsNullOrEmpty(this.txtCardNo.Text.Trim()))
                {
                    string CardNo = this.GetCardNoByMCardNO(this.txtMcardNo.Text.Trim());

                    if (string.IsNullOrEmpty(CardNo) == false)
                    {
                        this.txtCardNo.Enabled = false;
                        this.txtCardNo.Text = CardNo;
                        this.txtCardNo_KeyDown(new object(), new KeyEventArgs(Keys.Enter));
                    }
                }
                else if (this.txtAge.Text.Trim() == "")
                {
                    this.txtAge.Focus();
                }
                else
                {
                    this.txtPhone.Focus();
                }
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //反相跳转
                this.setPriorControlFocus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }

        private void dtBirthday_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DateTime current = this.regMgr.GetDateTimeFromSysDateTime().Date;

                if (this.dtBirthday.Value.Date > current)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("出生日期不能大于当前时间"), "提示");
                    this.dtBirthday.Focus();
                    return;
                }

                //计算年龄
                if (this.dtBirthday.Value.Date != current)
                {
                    this.setAge(this.dtBirthday.Value);
                }

                if (this.cmbPayKind.Tag != null)
                {
                    if (!string.IsNullOrEmpty(strDealPTregLevels) && isDealPTreg)
                    {
                        string[] strLevels = this.strDealPTregLevels.Split(',');
                        for (int i = 0; i <= strLevels.Length - 1; ++i)
                        {
                            if (null == this.cmbRegLevel.Tag) break;
                            if (this.cmbRegLevel.Tag.ToString() == strLevels[i])
                                this.GetCost();

                        }
                    }

                }
                this.cmbPayKind.Focus();
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //反相跳转
                this.setPriorControlFocus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }

        private void EditingControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PageUp)
            {
                //反相跳转
                this.cmbSex.Focus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.cmbPayKind.Focus();
            }
        }
        private void txtPhone_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtAddress.Focus();
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //反相跳转
                this.setPriorControlFocus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }

        private void txtAddress_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ICompletionAddress != null)
                {
                    if (this.txtAddress.SelectionStart == this.txtAddress.Text.Length && isCompleted)
                    {
                        this.cmbCardType.Focus();
                        isCompleted = false;
                    }
                    else
                    {
                        this.txtAddress.Text = ICompletionAddress.CompletionAddress(this.txtAddress.Text);
                        //光标显示到最后
                        this.txtAddress.SelectionStart = this.txtAddress.Text.Length;
                        isCompleted = true;
                    }
                }
                else
                {
                    this.cmbCardType.Focus();
                }
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //反相跳转
                this.setPriorControlFocus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
            else
            {
                isCompleted = false;
            }
        }

        private void cmbCardType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.cmbPatientType.Focus();
                //if (this.Save() == -1)
                //{
                //    cmbRegLevel.Focus();
                //}

                return;
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //反相跳转
                this.setPriorControlFocus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }
        #endregion
        #endregion

        #region PageUp,PageDown切换焦点跳转
        /// <summary>
        /// 设置上一个控件获得焦点
        /// </summary>		
        private void setPriorControlFocus()
        {
            System.Windows.Forms.SendKeys.Send("+{TAB}");

        }

        /// <summary>
        /// 设置下一个控件获得焦点
        /// </summary>		
        private void setNextControlFocus()
        {
            System.Windows.Forms.SendKeys.Send("{TAB}");
        }
        #endregion

        #region 输入法菜单事件
        private void m_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem m in this.neuContextMenuStrip1.Items)
            {
                if (sender == m)
                {
                    m.Checked = true;
                    this.CHInput = this.getInputLanguage(m.Text);
                    //保存输入法
                    this.SaveInputLanguage();
                }
                else
                {
                    m.Checked = false;
                }
            }
        }
        /// <summary>
        /// 读取当前默认输入法
        /// </summary>
        private void readInputLanguage()
        {
            if (!System.IO.File.Exists(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml"))
            {
                FS.HISFC.Components.Common.Classes.Function.CreateFeeSetting();

            }
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml");
                XmlNode node = doc.SelectSingleNode("//IME");

                this.CHInput = this.getInputLanguage(node.Attributes["currentmodel"].Value);

                if (this.CHInput != null)
                {
                    foreach (ToolStripMenuItem m in this.neuContextMenuStrip1.Items)
                    {
                        if (m.Text == this.CHInput.LayoutName)
                        {
                            m.Checked = true;
                        }
                    }
                }

                //添加到工具栏

            }
            catch (Exception e)
            {
                MessageBox.Show("获取挂号默认中文输入法出错!" + e.Message);
                return;
            }
        }

        private void addContextToToolbar()
        {
            FS.FrameWork.WinForms.Controls.NeuToolBar main = null;

            foreach (Control c in FindForm().Controls)
            {
                if (c.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuToolBar))
                {
                    main = (FS.FrameWork.WinForms.Controls.NeuToolBar)c;
                }
            }

            ToolBarButton button = null;

            if (main != null)
            {
                foreach (ToolBarButton b in main.Buttons)
                {
                    if (b.Text == "输入法") button = b;
                }
            }

            //if(button != null)
            //{
            //    ToolStripDropDownButton drop = (ToolStripDropDownButton)button;
            //    foreach(ToolStripMenuItem m in neuContextMenuStrip1.Items)
            //    {
            //        drop.DropDownItems.Add(m);
            //    }
            //}
        }

        /// <summary>
        /// 根据输入法名称获取输入法
        /// </summary>
        /// <param name="LanName"></param>
        /// <returns></returns>
        private InputLanguage getInputLanguage(string LanName)
        {
            foreach (InputLanguage input in InputLanguage.InstalledInputLanguages)
            {
                if (input.LayoutName == LanName)
                {
                    return input;
                }
            }
            return null;
        }
        /// <summary>
        /// 保存当前输入法
        /// </summary>
        private void SaveInputLanguage()
        {
            if (!System.IO.File.Exists(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml"))
            {
                FS.HISFC.Components.Common.Classes.Function.CreateFeeSetting();
            }
            if (this.CHInput == null) return;

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml");
                XmlNode node = doc.SelectSingleNode("//IME");

                node.Attributes["currentmodel"].Value = this.CHInput.LayoutName;

                doc.Save(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml");
            }
            catch (Exception e)
            {
                MessageBox.Show("保存挂号默认中文输入法出错!" + e.Message);
                return;
            }
        }
        #endregion

        #region 函数内容

        /// <summary>
        /// 自动获取就诊卡号
        /// </summary>
        private void AutoGetCardNO()
        {
            int autoGetCardNO;
            autoGetCardNO = regMgr.AutoGetCardNO();
            if (autoGetCardNO == -1)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("未能成功自动产生卡号，请手动输入"), "提示");
            }
            else
            {
                this.txtCardNo.Text = autoGetCardNO.ToString().PadLeft(10, '0');
            }
            this.txtCardNo.Focus();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        private int Save()
        {
            if (this.valid() == -1)
            {
                return 2;
            }

            // 费用明细
            List<AccountCardFee> lstAccFee = null;

            if (this.GetValue(out lstAccFee) == -1)
            {
                return 2;
            }

            if (this.ValidCardNO(this.regObj.PID.CardNO) < 0)
            {
                return -1;
            }

            if (this.IsPrompt)
            {
                //确认提示
                if (MessageBox.Show(FS.FrameWork.Management.Language.Msg("请确认录入数据是否正确"), "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    this.cmbRegLevel.Focus();
                    return -1;
                }
            }

            this.MedcareInterfaceProxy.SetPactCode(this.regObj.Pact.ID);
            //黑名单
            if (this.regObj.Pact.PayKind.ID == "03")
            {
                //2014-09-27 by han-zf 信息科余小强要求挂号界面不再与中心做任何交互
                //if (this.MedcareInterfaceProxy.IsInBlackList(this.regObj))
                //{
                //    MessageBox.Show(this.MedcareInterfaceProxy.ErrMsg, "提示");
                //    this.cmbPayKind.Focus();
                //    return -1;
                //}
            }

            decimal vacancy = 0;

            #region 账户判断

            bool isAccountFee = this.controlParma.GetControlParam<bool>("MZ2011", true, false);

            if (isAccountFee)
            {
                int result = this.feeMgr.GetAccountVacancy(this.regObj.PID.CardNO, ref vacancy);
                if (result < 0)
                {
                    MessageBox.Show("取账户信息失败！" + this.feeMgr.Err);
                    return -1;
                }

                //余额不足，不继续使用账户
                if (vacancy <= 0)
                {
                    isAccountFee = false;
                }

                if (isAccountFee)
                {
                    if (isTSAccount)
                    {
                        if (MessageBox.Show("是否账户支付", "确认", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            isAccountFee = false;
                        }
                    }
                }

                if (isAccountFee)
                {
                    if (!feeMgr.CheckAccountPassWord(this.regObj))
                    {
                        MessageBox.Show("账户支付失败！");
                        return -1;
                    }
                }

            }
            #endregion

            #region 卡费用特殊处理

            // 卡费用
            // {23BA226E-A1E5-4a0b-A1D5-92FA97AF3E85}
            AccountCardFee cardFee = null;

            if (chbCardFee.Visible && chbCardFee.Checked)
            {
                AccountCard accountCard = this.txtCardNo.Tag as AccountCard;
                if (accountCard != null)
                {
                    cardFee = new AccountCardFee();
                    cardFee.FeeType = AccCardFeeType.CardFee;
                    cardFee.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                    cardFee.MarkNO = accountCard.MarkNO;
                    cardFee.MarkType = accountCard.MarkType;

                    FS.HISFC.Models.Base.Const obj = cardFee.MarkType as FS.HISFC.Models.Base.Const;
                    if (obj != null)
                    {
                        cardFee.Tot_cost = FS.FrameWork.Function.NConvert.ToDecimal(obj.UserCode);
                    }
                    cardFee.Own_cost = cardFee.Tot_cost;

                    cardFee.IsBalance = false;
                    cardFee.BalanceNo = "";
                    cardFee.BalnaceOper.ID = "";
                    cardFee.IStatus = 1;

                }
            }

            if (lstAccFee == null)
            {
                lstAccFee = new List<AccountCardFee>();
            }
            if (cardFee != null)
            {
                lstAccFee.Add(cardFee);

                // 处理挂号记录， 卡费用归到挂号表其他费用中
                this.regObj.RegLvlFee.OthFee += cardFee.Tot_cost;
                this.regObj.OwnCost += cardFee.Own_cost;
                this.regObj.PubCost += cardFee.Pub_cost;
                this.regObj.PayCost += cardFee.Pay_cost;
            }

            #endregion

            #region 挂号费支付方式

            if (this.DualAccountCardFee(ref lstAccFee) < 0)
            {
                return -1;
            }

            #endregion

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            this.regMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.bookingMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.SchemaMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.patientMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.feeMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            int rtn;
            string Err = "";
            //接口实现{E43E0363-0B22-4d2a-A56A-455CFB7CF211}
            if (this.iProcessRegiter != null)
            {
                rtn = this.iProcessRegiter.SaveBegin(ref regObj, ref Err);

                if (rtn < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Err);
                    return -1;
                }
            }

            //事务开始
            DateTime current = this.regMgr.GetDateTimeFromSysDateTime();

            #region 作废
            //如果打印发票则lstAccFee.count > 0；不打印发票lstAccFee.count = 0
            //如果要打印发票的情况，费用必须要有挂号费信息，否则不让挂号
            //if (lstAccFee.Count > 0)
            //{
            //    bool isExistRegFee = false;
            //    foreach (FS.HISFC.Models.Account.AccountCardFee tempCardFee in lstAccFee)
            //    {
            //        if (tempCardFee.FeeType == AccCardFeeType.RegFee)
            //        {
            //            isExistRegFee = true;
            //            break;
            //        }
            //    }

            //    if (!isExistRegFee)
            //    {
            //        FS.FrameWork.Management.PublicTrans.RollBack();
            //        this.MedcareInterfaceProxy.Rollback();
            //        MessageBox.Show("挂号必须要有挂号费信息!", "警告");
            //        return -1;
            //    }

            //}
            #endregion

            #region 取发票号

            string strInvioceNO = "";
            string strRealInvoiceNO = "";
            string strErrText = "";
            int iRes = 0;
            string strInvoiceType = "R";

            FS.HISFC.Models.Base.Employee employee = this.regMgr.Operator as FS.HISFC.Models.Base.Employee;

            //有费用信息的时候才打发票
            if (lstAccFee.Count > 0)
            {

                if (this.GetRecipeType == 1)
                {
                    strInvioceNO = this.regObj.RecipeNO.ToString().PadLeft(12, '0');
                    strRealInvoiceNO = "";
                }
                else
                {
                    if (this.GetRecipeType == 2)
                    {
                        strInvoiceType = "R";
                    }
                    else if (this.GetRecipeType == 3)
                    {
                        // 取门诊收据
                        strInvoiceType = "C";
                    }

                    iRes = this.feeMgr.GetInvoiceNO(employee, strInvoiceType, ref strInvioceNO, ref strRealInvoiceNO, ref strErrText);

                    if (iRes == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(strErrText);
                        return -1;
                    }
                }
            }

            this.regObj.InvoiceNO = strInvioceNO;

            #endregion

            #region 处理费用明细信息

            //有费用信息的时候才处理
            if (lstAccFee.Count > 0)
            {

                foreach (AccountCardFee accFee in lstAccFee)
                {
                    accFee.InvoiceNo = strInvioceNO;
                    accFee.Print_InvoiceNo = strRealInvoiceNO;
                    accFee.ClinicNO = this.regObj.ID;

                    accFee.Patient.PID.CardNO = this.regObj.PID.CardNO;
                    accFee.Patient.Name = this.regObj.Name;

                    accFee.IStatus = 1;

                    accFee.FeeOper.ID = employee.ID;
                    accFee.FeeOper.Name = employee.Name;
                    accFee.FeeOper.OperTime = current;

                    accFee.Oper.ID = employee.ID;
                    accFee.Oper.Name = employee.Name;
                    accFee.Oper.OperTime = current;

                    accFee.IsBalance = false;
                    accFee.BalanceNo = "";

                }

            }

            #endregion

            decimal OwnCostTot = this.regObj.OwnCost;

            #region 账户新增

            if (isAccountFee)
            {
                decimal cost = 0m;

                if (vacancy < OwnCostTot)
                {
                    cost = vacancy;
                    this.regObj.PayCost = vacancy;
                    this.regObj.OwnCost = this.regObj.OwnCost - vacancy;
                }
                else
                {
                    cost = OwnCostTot;
                    this.regObj.PayCost = this.regObj.OwnCost;
                    this.regObj.OwnCost = 0;
                }
                if (this.feeMgr.AccountPay(this.regObj, cost, this.regObj.InvoiceNO, this.regObj.DoctorInfo.Templet.Dept.ID, "R") < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.feeMgr.Err);
                    return -1;
                }
                this.regObj.IsAccount = true;
            }
            #endregion

            try
            {

                #region 更新看诊序号
                int orderNo = 0;

                //2看诊序号		
                if (this.UpdateSeeID(this.regObj.DoctorInfo.Templet.Dept.ID, this.regObj.DoctorInfo.Templet.Doct.ID,
                    this.regObj.DoctorInfo.Templet.Noon.ID, this.regObj.DoctorInfo.SeeDate, ref orderNo,
                    ref Err) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Err, "提示");
                    return -1;
                }

                regObj.DoctorInfo.SeeNO = orderNo;

                //专家、专科、特诊、预约号更新排班限额
                #region schema
                if (this.UpdateSchema(this.SchemaMgr, this.regObj.RegType, ref orderNo, ref Err) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    if (Err != "") MessageBox.Show(Err, "提示");
                    return -1;
                }

                //regObj.DoctorInfo.SeeNO = orderNo;
                #endregion

                //1全院流水号			
                if (this.Update(this.regMgr, this.regObj.DoctorInfo.SeeDate, ref orderNo, ref Err) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Err, "提示");
                    return -1;
                }

                regObj.OrderNO = orderNo;
                #endregion

                //预约号更新已看诊标志
                #region booking
                if (this.regObj.RegType == FS.HISFC.Models.Base.EnumRegType.Pre)
                {
                    FS.HISFC.Models.Registration.Booking booking = this.txtOrder.Tag as FS.HISFC.Models.Registration.Booking;
                    //更新看诊限额
                    rtn = this.bookingMgr.Update(booking.ID, true, regMgr.Operator.ID, current);
                    if (rtn == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更新预约看诊信息出错!" + this.bookingMgr.Err, "提示");
                        return -1;
                    }
                    if (rtn == 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("预约挂号信息状态已经变更,请重新检索"), "提示");
                        return -1;
                    }

                    if (booking.DoctorInfo.SeeNO > 0 && booking.OrderNO > 0)
                    {
                        regObj.DoctorInfo.SeeNO = booking.DoctorInfo.SeeNO;
                        regObj.OrderNO = booking.OrderNO;
                    }

                    if (this.bookingMgr.UpdateBookReg(booking.ID,this.regObj.ID)==-1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("更新预约挂号失败"), "提示");
                        return -1;
                    }

                    #region 通知预约平台 add by yerl

                    FS.HISFC.Models.Registration.AppointmentOrder appOrder = appointmentMgr.QueryAppointmentOrderBySerialNO(booking.ID);
                    //不为空说明这个号是卫生局预约过来的
                    if (appOrder != null)
                    {
                        rtn = appointmentMgr.UpdateAppointmentOrderSerialNO(appOrder.OrderID, "4", regMgr.Operator.ID, regObj.ID);

                        if (rtn > 0)
                        {
                            AppointmentService appointmentService = new AppointmentService();
                            //多线程异步调用,不理会结果 add by yerl
                            appointmentService.Invoke(AppointmentService.funs.payOrderByHis,
                                new AppointmentService.InvokeCompletedEventHandler(appointmentService_InvokeCompleted),
                                appOrder.OrderID,
                                (appOrder.RegFee + appOrder.TreatFee).ToString(),
                                current.ToString("yyyy-MM-dd HH:mm:ss"),
                                AppointmentService.funs.payOrderByHis.ToString());
                        }
                    }
                    #endregion
                }
                #endregion

                if (lstPactSelectChange.Contains(this.regObj.Pact.ID))
                {
                    #region 待遇接口实现
                    //2014-09-27 by han-zf 信息科余小强要求挂号界面不再与中心做任何交互
                    //this.MedcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    //this.MedcareInterfaceProxy.Connect();

                    //this.MedcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    //this.MedcareInterfaceProxy.BeginTranscation();

                    //this.regObj.SIMainInfo.InvoiceNo = this.regObj.InvoiceNO;
                    //int returnValue = this.MedcareInterfaceProxy.UploadRegInfoOutpatient(this.regObj);
                    //if (returnValue == -1)
                    //{
                    //    this.MedcareInterfaceProxy.Rollback();
                    //    FS.FrameWork.Management.PublicTrans.RollBack()
                    //        ;
                    //    MessageBox.Show(FS.FrameWork.Management.Language.Msg("上传挂号信息失败!") + this.MedcareInterfaceProxy.ErrMsg);

                    //    return -1;
                    //}
                    //////医保患者登记医保信息
                    ////if (this.regObj.Pact.PayKind.ID == "02")
                    ////{
                    //this.regObj.OwnCost = this.regObj.SIMainInfo.OwnCost;  //自费金额
                    //this.regObj.PubCost = this.regObj.SIMainInfo.PubCost;  //统筹金额
                    //this.regObj.PayCost = this.regObj.SIMainInfo.PayCost;  //帐户金额
                    ////}
                    #endregion
                }

                #region 平费用

                decimal totcost = this.regObj.RegLvlFee.RegFee + this.regObj.RegLvlFee.ChkFee + this.regObj.RegLvlFee.OwnDigFee + this.regObj.RegLvlFee.OthFee;
                if (totcost - this.regObj.PubCost - this.regObj.PayCost - this.regObj.OwnCost != 0)
                {
                    this.regObj.OwnCost = totcost - this.regObj.PubCost - this.regObj.PayCost;
                }

                #endregion

                //登记挂号信息
                if (this.regMgr.Insert(this.regObj) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.MedcareInterfaceProxy.Rollback();
                    MessageBox.Show(this.regMgr.Err, "提示");
                    return -1;
                }

                #region 医保信息插入 by han-zf 2014-07-11
                //by han-zf 2014-09-28 挂号不再与中心交互
                //this.MedcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                //this.MedcareInterfaceProxy.BeginTranscation();
                //if (this.MedcareInterfaceProxy.UploadRegInfoOutpatient(this.regObj) == -1)
                //{
                //    this.MedcareInterfaceProxy.Rollback();
                //    FS.FrameWork.Management.PublicTrans.RollBack()
                //        ;
                //    MessageBox.Show(FS.FrameWork.Management.Language.Msg("门诊患者医保信息插入失败!") + this.MedcareInterfaceProxy.ErrMsg);

                //    return -1;
                //}
                #endregion


                #region 保存费用明细信息

                if (lstAccFee != null && lstAccFee.Count > 0)
                {
                    if (this.feeMgr.SaveAccountCardFee(lstAccFee) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.MedcareInterfaceProxy.Rollback();
                        MessageBox.Show(this.feeMgr.Err, "提示");
                        return -1;
                    }
                }

                #endregion


                //更新患者基本信息
                if (this.UpdatePatientinfo(this.regObj, this.patientMgr, this.regMgr, ref Err) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.MedcareInterfaceProxy.Rollback();
                    MessageBox.Show(Err, "提示");
                    return -1;
                }
                //接口实现{E43E0363-0B22-4d2a-A56A-455CFB7CF211}
                if (this.iProcessRegiter != null)
                {
                    rtn = this.iProcessRegiter.SaveEnd(ref regObj, ref Err);
                    if (rtn < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.MedcareInterfaceProxy.Rollback();
                        MessageBox.Show(Err);
                        return -1;
                    }
                }

                #region 发票走号

                //有费用信息的时候，才处理发票
                if (lstAccFee.Count > 0)
                {

                    if (this.GetRecipeType == 2 || this.GetRecipeType == 3)
                    {
                        string invoiceStytle = controlParma.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.GET_INVOICE_NO_TYPE, false, "0");
                        if (this.feeMgr.UseInvoiceNO(employee, invoiceStytle, strInvoiceType, 1, ref strInvioceNO, ref strRealInvoiceNO, ref strErrText) < 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(this.feeMgr.Err);
                            return -1;
                        }

                        if (this.feeMgr.InsertInvoiceExtend(strInvioceNO, strInvoiceType, strRealInvoiceNO, "00") < 1)
                        {
                            // 发票头暂时先保存00
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(this.feeMgr.Err);
                            return -1;
                        }
                    }

                }

                #endregion


                //挂号自动分诊接口
                if (InterfaceManager.GetIADT() != null)
                {
                    //挂号没有选择医生时，这里可能会根据排班队列和候诊人数自动分配一个医生，所以提前到前面
                    if (InterfaceManager.GetIADT().Register(this.regObj, true) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this, "挂号失败，请向系统管理员报告错误信息：" + InterfaceManager.GetIADT().Err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return -1;
                    }
                }

                FS.FrameWork.Management.PublicTrans.Commit();
                this.MedcareInterfaceProxy.Commit();
                this.MedcareInterfaceProxy.Disconnect();

                //最后更新处方号,加 1,防止中途返回跳号
                this.UpdateRecipeNo(1);

                this.QueryRegLevl();
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "提示");
                return -1;
            }

            #region 找零放在发票打印后面【废弃】

            ////找零{F0661633-4754-4758-B683-CB0DC983922B}
            //if (this.isShowChangeCostForm)
            //{
            //    rtn = this.ShowChangeForm(this.regObj);
            //    {
            //        if (rtn < 0)
            //        {
            //            return -1;
            //        }
            //    }
            //}

            #endregion

            // 记录发票费用的打印信息
            this.regObj.LstCardFee = lstAccFee;

            //条码打印
            this.PrintBarCode(this.regObj);

            //有费用信息的时候，才打印发票
            if (lstAccFee.Count > 0)
            {

                if (this.isAutoPrint)
                {
                    this.Print(this.regObj, this.regMgr);
                }
                else
                {
                    DialogResult rs = MessageBox.Show(FS.FrameWork.Management.Language.Msg("请选择是否打印挂号票"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (rs == DialogResult.Yes)
                    {
                        this.Print(this.regObj, this.regMgr);
                    }
                }

            }
            else if (lstAccFee.Count == 0)
            {
                MessageBox.Show("挂号成功! 不打印发票!", "提示");
            }

            this.addRegister(this.regObj);

            this.Clear();
            ChangeInvoiceNOMessage();
            this.txtCardNo.Focus();
            return 0;
        }

        /// <summary>
        /// 异步获取Web调用结果
        /// </summary>
        /// <param name="result"></param>
        private void appointmentService_InvokeCompleted(AppointmentService.InvokeResult result)
        {
            if (result.ResultCode == "0")
                MessageBox.Show("通知卫生局已取号成功");
            else
                MessageBox.Show("通知卫生局已取号失败" + result.ResultDesc);
        }

        /// <summary>
        /// 发票号切换判断
        /// </summary>
        private void ChangeInvoiceNOMessage()
        {
            GetYKdept();
            if (dictionaryYKDept.ContainsKey(((FS.HISFC.Models.Base.Employee)(regMgr.Operator)).Dept.ID))
            {
                return;
            }
            string invoiceNO = string.Empty;
            string invoiceType = string.Empty;
            if (this.GetRecipeType == 2)
            {
                invoiceType = "R";
            }
            else if (this.GetRecipeType == 3)
            {
                //取门诊收据
                invoiceType = "C";
            }
            else
            {
                return;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            invoiceNO = this.feeMgr.GetNewInvoiceNO(invoiceType);
            if (string.IsNullOrEmpty(invoiceNO))
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                //自己设置发票
                FS.HISFC.Components.Common.Forms.frmUpdateInvoice frm = new FS.HISFC.Components.Common.Forms.frmUpdateInvoice();
                frm.InvoiceType = invoiceType;
                frm.ShowDialog(this);


                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                invoiceNO = this.feeMgr.GetNewInvoiceNO(invoiceType);
                if (string.IsNullOrEmpty(invoiceNO))
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("该操作员没有可以使用的门诊挂号发票，请领取"));
                    return;
                }

            }
            string errText = string.Empty;
            FS.FrameWork.Management.PublicTrans.RollBack();
            //int resultValue = feeMgr.InvoiceMessage(regFeeMgr.Operator.ID, invoiceType, invoiceNO, 1, ref errText);
            //if (resultValue < 0)
            //{
            //    MessageBox.Show(errText);
            //}
        }

        #region 有效性验证
        /// <summary>
        /// 有效性验证
        /// </summary>
        /// <returns></returns>
        private int valid()
        {
            this.txtCardNo.Focus();//防止在combox下不回车就保存出错

            if (this.txtRecipeNo.Text.Trim() == "")
            {
                MessageBox.Show("请输入处方号!", "提示");
                this.ChangeRecipe();
                return -1;
            }

            if (this.cmbRegLevel.Tag == null || this.cmbRegLevel.Tag.ToString() == "")
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("请选择挂号级别"), "提示");
                this.cmbRegLevel.Focus();
                return -1;
            }

            FS.HISFC.Models.Registration.RegLevel level = (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;
            if ((this.cmbDept.Tag == null || this.cmbDept.Tag.ToString() == ""))
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("请选择挂号科室"), "提示");
                this.cmbDept.Focus();
                return -1;
            }

            if ((level.IsExpert) && (this.cmbDoctor.Tag == null || this.cmbDoctor.Tag.ToString() == ""))
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("专家号必须指定看诊医生"), "提示");
                this.cmbDoctor.Focus();
                return -1;

                if (this.dtBookingDate.Tag == null)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("专家号或专科号排班为空，请重新选择！"), "提示");
                    this.cmbDoctor.Focus();
                    return -1;
                }

            }
            else if (level.IsFaculty)
            {
                if (this.dtBookingDate.Tag == null)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("专家号或专科号排班为空，请重新选择！"), "提示");
                    this.cmbDept.Focus();
                    return -1;
                }
            }

            if (this.regObj == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("请录入挂号患者"), "提示");
                this.txtCardNo.Focus();
                return -1;
            }

            //{05B4AB01-C7FC-4e1b-9A77-80B83E77F77F} 判断病历号是否为空 xuc
            if (string.IsNullOrEmpty(this.regObj.PID.CardNO) == true)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("请录入病历号"), "提示");
                this.txtCardNo.Focus();
                return -1;
            }

            if (this.IsInputName && this.txtName.Text.Trim() == "")
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("请输入患者姓名"), "提示");
                this.txtName.Focus();
                return -1;
            }

            if (this.dtBegin.Value.TimeOfDay > this.dtEnd.Value.TimeOfDay)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("挂号开始时间不能大于结束时间"), "提示");
                this.dtEnd.Focus();
                return -1;
            }

            if (FS.FrameWork.Public.String.ValidMaxLengh(this.txtName.Text.Trim(), 40) == false)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("患者名称最多可录入20个汉字"), "提示");
                this.txtName.Focus();
                return -1;
            }
            if (this.cmbSex.Tag == null || this.cmbSex.Tag.ToString() == "")
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("请选择患者性别"), "提示");
                this.cmbSex.Focus();
                return -1;
            }

            if (this.cmbPayKind.Tag == null || this.cmbPayKind.Tag.ToString() == "")
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("患者结算类别不能为空"), "提示");
                this.cmbPayKind.Focus();
                return -1;
            }
            if (FS.FrameWork.Public.String.ValidMaxLengh(this.txtPhone.Text.Trim(), 30) == false)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("联系电话最多可录入20位数字"), "提示");
                this.txtPhone.Focus();
                return -1;
            }
            if (FS.FrameWork.Public.String.ValidMaxLengh(this.txtAddress.Text.Trim(), 60) == false)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("联系人地址最多可录入30个汉字"), "提示");
                this.txtAddress.Focus();
                return -1;
            }
            if (FS.FrameWork.Public.String.ValidMaxLengh(this.txtCardNo.Text, 10) == false)
            {
                MessageBox.Show("病历号输入有问题,请核对病历号!", "提示");
                this.txtCardNo.Focus();
                return -1;
            }
            if (FS.FrameWork.Public.String.ValidMaxLengh(this.txtMcardNo.Text.Trim(), 20) == false)
            {
                MessageBox.Show("医疗证号最多可录入20位数字!", "提示");
                this.txtMcardNo.Focus();
                return -1;
            }
            if (FS.FrameWork.Public.String.ValidMaxLengh(this.txtAge.Text.Trim(), 3) == false)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("年龄最多可录入3位数字"), "提示");
                this.txtAge.Focus();
                return -1;
            }
            if (this.isLimit == EnumLimit.Half)
            {
                if ((this.txtPhone.Text == null || this.txtPhone.Text.Trim() == "") &&
                    (this.txtAddress.Text == null || this.txtAddress.Text.Trim() == ""))
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("联系电话和地址不能同时为空,必须输入一个"), "提示");
                    this.txtPhone.Focus();
                    return -1;
                }
            }
            else if (this.isLimit == EnumLimit.All)
            {
                if (this.txtPhone.Text == null || this.txtPhone.Text.Trim() == "")
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("联系电话不能为空,必须输入一个"), "提示");
                    this.txtPhone.Focus();
                    return -1;
                }
                else if (this.txtAddress.Text == null || this.txtAddress.Text.Trim() == "")
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("联系地址不能为空,必须输入一个"), "提示");
                    this.txtAddress.Focus();
                    return -1;
                }

            }

            if (this.txtAge.Text.Trim().Length > 0)
            {
                try
                {
                    int age = int.Parse(this.txtAge.Text.Trim());
                    if (age <= 0)
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("年龄不能为负数"), "提示");
                        this.txtAge.Focus();
                        return -1;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("年龄录入格式不正确!" + e.Message, "提示");
                    this.txtAge.Focus();
                    return -1;
                }
            }
            DateTime current = this.regMgr.GetDateTimeFromSysDateTime().Date;
            if (this.dtBirthday.Value.Date > current)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("出生日期不能大于当前时间"), "提示");
                this.dtBirthday.Focus();
                return -1;
            }

            //校验合同单位
            if (this.ValidCombox(FS.FrameWork.Management.Language.Msg("您选择的合同单位有误或不在合同单位的下拉列表中,请重新选择")) < 0)
            {
                this.cmbPayKind.Focus();
                return -1;
            }
            //chenxin 不选证件类型直接录入身份证号大于18位时报错
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtIdNO.Text, 18))
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("证件号过长,请重新输入!"), "提示");
                this.txtIdNO.Select();
                return -1;
            }
            if (cmbCardType.SelectedItem != null && cmbCardType.SelectedItem.Name == "身份证")
            {

                //校验身份证号
                if (!string.IsNullOrEmpty(this.txtIdNO.Text))
                {

                    int reurnValue = this.ProcessIDENNO(this.txtIdNO.Text, EnumCheckIDNOType.Saveing);

                    if (reurnValue < 0)
                    {
                        return -1;
                    }
                }
            }

            if (!string.IsNullOrEmpty(this.cmbBookingType.Text.Trim()))
            {
                if (this.regObj.RegExtend == null)
                {
                    this.regObj.RegExtend = new FS.HISFC.Models.Registration.RegisterExtend();
                }
                this.regObj.RegExtend.BookingTypeId = this.cmbBookingType.Tag.ToString();
                this.regObj.RegExtend.BookingTypeName = this.cmbBookingType.Text.Trim();
            }

            return 0;
        }

        #region 校验合同单位
        /// <summary>
        /// 校验combox
        /// </summary>
        private int ValidCombox(string ErrMsg)
        {
            int j = 0;
            for (int i = 0; i < this.cmbPayKind.Items.Count; i++)
            {
                if (this.cmbPayKind.Text.Trim() == this.cmbPayKind.Items[i].ToString())
                {

                    this.cmbPayKind.SelectedIndex = i;
                    j++;
                    break;

                }


            }
            //"您选择的合同单位有误或不在合同单位的下拉列表中,请重新选择"
            if (j == 0)
            {
                MessageBox.Show(ErrMsg);

                return -1;
            }
            return 1;


        }
        #endregion

        #endregion

        #region 验证此就诊卡号是否住过院
        ///// <summary>
        ///// 验证此就诊卡号是否住过院
        ///// </summary>
        ///// <param name="cardNO"></param>
        ///// <returns></returns>
        //{F3258E87-7BCC-411a-865E-A9843AD2C6DD}
        //private ArrayList ValidIsSendInhosCase(string cardNO)
        //{


        //    return patientMgr.GetPatientInfoHaveCaseByCardNO(cardNO);

        //}
        #endregion

        #region 获取挂号信息
        /// <summary>
        /// 获取挂号信息
        /// </summary>
        /// <returns></returns>
        private int GetValue(out List<AccountCardFee> lstAccFee)
        {
            lstAccFee = null;
            //门诊号
            this.regObj.ID = this.regMgr.GetSequence("Registration.Register.ClinicID");
            this.regObj.TranType = FS.HISFC.Models.Base.TransTypes.Positive;//正交易

            this.regObj.DoctorInfo.Templet.RegLevel.ID = this.cmbRegLevel.Tag.ToString();
            this.regObj.DoctorInfo.Templet.RegLevel.Name = this.cmbRegLevel.Text;
            //{156C449B-60A9-4536-B4FB-D00BC6F476A1}
            this.regObj.DoctorInfo.Templet.RegLevel.IsEmergency = (this.cmbRegLevel.SelectedItem as FS.HISFC.Models.Registration.RegLevel).IsEmergency;
            this.regObj.DoctorInfo.Templet.RegLevel.IsExpert = (this.cmbRegLevel.SelectedItem as FS.HISFC.Models.Registration.RegLevel).IsExpert;
            if (this.dtBookingDate.Tag is FS.HISFC.Models.Registration.Schema)
            {
                this.regObj.DoctorInfo.Templet.Noon = ((FS.HISFC.Models.Registration.Schema)this.dtBookingDate.Tag).Templet.Noon;
            }

            this.regObj.DoctorInfo.Templet.Dept.ID = this.cmbDept.Tag.ToString();
            this.regObj.DoctorInfo.Templet.Dept.Name = this.cmbDept.Text;

            this.regObj.DoctorInfo.Templet.Doct.ID = this.cmbDoctor.Tag.ToString();
            this.regObj.DoctorInfo.Templet.Doct.Name = this.cmbDoctor.Text;

            //{0BA561B1-376F-4412-AAD0-F19A0C532A03}
            this.regObj.Name = FS.FrameWork.Public.String.TakeOffSpecialChar(this.txtName.Text.Trim(), "'");//患者姓名
            this.regObj.Sex.ID = this.cmbSex.Tag.ToString();//性别

            this.regObj.Birthday = this.dtBirthday.Value;//出生日期			

            FS.HISFC.Models.Registration.RegLevel level = (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;
            this.regObj.RegType = FS.HISFC.Models.Base.EnumRegType.Reg;
            //不为空说明是预约号
            if (this.txtOrder.Tag != null)
            {
                this.regObj.RegType = FS.HISFC.Models.Base.EnumRegType.Pre;
            }
            else if (level.IsSpecial)
            {
                this.regObj.RegType = FS.HISFC.Models.Base.EnumRegType.Spe;
            }

            FS.HISFC.Models.Registration.Schema schema = null;

            //只有专家、专科、特诊需要输入看诊时间段、更新限额
            if (this.regObj.RegType != FS.HISFC.Models.Base.EnumRegType.Pre
                        && (level.IsSpecial || level.IsFaculty || level.IsExpert))
            {
                //添加排班信息 add by yerl
                if (this.regObj.RegType == FS.HISFC.Models.Base.EnumRegType.Reg)
                {
                    schema = this.GetValidSchema(level);
                    if (schema != null)
                        this.regObj.DoctorInfo.Templet.ID = schema.Templet.ID;
                }
                if (!isFilterDoc)
                {
                    schema = this.GetValidSchema(level);
                    if (schema == null)
                    {
                        MessageBox.Show("预约时间指定错误,没有符合条件的排班信息!", "提示");
                        this.dtBookingDate.Focus();
                        return -1;
                    }
                    this.SetBookingTag(schema);
                }
                if (level.IsExpert)
                {
                    schema = this.GetValidSchema(level);
                    if (schema == null)
                    {
                        MessageBox.Show("预约时间指定错误,没有符合条件的排班信息!", "提示");
                        this.dtBookingDate.Focus();
                        return -1;
                    }
                    this.SetBookingTag(schema);
                }
            }


            if (level.IsExpert && this.regObj.RegType != FS.HISFC.Models.Base.EnumRegType.Pre)
            {
                if (this.VerifyIsProfessor(level, schema) == false)
                {
                    this.cmbRegLevel.Focus();
                    return -1;
                }
            }


            #region 结算类别
            this.regObj.Pact.ID = this.cmbPayKind.Tag.ToString();//合同单位
            //this.regObj.Pact.Name = this.cmbPayKind.Text;

            FS.HISFC.Models.Base.PactInfo pact = conMgr.GetPactUnitInfoByPactCode(this.regObj.Pact.ID);
            if (pact == null || pact.ID == "")
            {
                MessageBox.Show("获取代码为:" + this.regObj.Pact.ID + "的合同单位信息出错!" + this.conMgr.Err, "提示");
                return -1;
            }
            this.regObj.Pact.Name = pact.Name;
            this.regObj.Pact.PayKind.Name = pact.PayKind.Name;
            this.regObj.Pact.PayKind.ID = pact.PayKind.ID;
            this.regObj.SSN = this.txtMcardNo.Text.Trim();//医疗证号

            if (pact.IsNeedMCard && this.regObj.SSN == "")
            {
                //MessageBox.Show("需要输入医疗证号!", "提示");
                //this.txtMcardNo.Focus();
                //return -1;
            }
            //人员黑名单判断
            if (this.validMcardNo(this.regObj.Pact.ID, this.regObj.SSN) == -1) return -1;

            #endregion

            this.regObj.PhoneHome = FS.FrameWork.Public.String.TakeOffSpecialChar(this.txtPhone.Text.Trim(), "'");//联系电话
            this.regObj.AddressHome = FS.FrameWork.Public.String.TakeOffSpecialChar(this.txtAddress.Text.Trim(), "'");//联系地址
            this.regObj.CardType.ID = this.cmbCardType.Tag.ToString();

            regObj.PatientType = cmbPatientType.Tag.ToString();

            #region 预约时间段
            if (this.regObj.RegType == FS.HISFC.Models.Base.EnumRegType.Pre)//预约号扣排班限额
            {
                this.regObj.IDCard = (this.txtOrder.Tag as FS.HISFC.Models.Registration.Booking).IDCard;
                this.regObj.DoctorInfo.Templet.Noon.ID = (this.txtOrder.Tag as FS.HISFC.Models.Registration.Booking).DoctorInfo.Templet.Noon.ID;
                this.regObj.DoctorInfo.Templet.IsAppend = (this.txtOrder.Tag as FS.HISFC.Models.Registration.Booking).DoctorInfo.Templet.IsAppend;
                this.regObj.DoctorInfo.SeeDate = DateTime.Parse(this.dtBookingDate.Value.ToString("yyyy-MM-dd") + " " +
                    this.dtBegin.Value.ToString("HH:mm:ss"));//挂号时间
                this.regObj.DoctorInfo.Templet.Begin = DateTime.Parse(this.dtBookingDate.Value.ToString("yyyy-MM-dd") + " " +
                    this.dtBegin.Value.ToString("HH:mm:ss"));
                this.regObj.DoctorInfo.Templet.End = DateTime.Parse(this.dtBookingDate.Value.ToString("yyyy-MM-dd") + " " +
                    this.dtEnd.Value.ToString("HH:mm:ss"));//结束时间
                this.regObj.DoctorInfo.Templet.ID = (this.txtOrder.Tag as FS.HISFC.Models.Registration.Booking).DoctorInfo.Templet.ID;
            }
            else if (level.IsSpecial || level.IsExpert || level.IsFaculty)//专家、专科、特诊号扣排班限额
            {
                if (!isFilterDoc)
                {
                    this.regObj.DoctorInfo.Templet.Noon.ID = (this.dtBookingDate.Tag as FS.HISFC.Models.Registration.Schema).Templet.Noon.ID;
                    this.regObj.DoctorInfo.Templet.IsAppend = (this.dtBookingDate.Tag as FS.HISFC.Models.Registration.Schema).Templet.IsAppend;
                    this.regObj.DoctorInfo.Templet.ID = (this.dtBookingDate.Tag as FS.HISFC.Models.Registration.Schema).Templet.ID;
                }

                this.regObj.DoctorInfo.Templet.Begin = DateTime.Parse(this.dtBookingDate.Value.ToString("yyyy-MM-dd") + " " + this.dtBegin.Value.ToString("HH:mm:ss"));
                this.regObj.DoctorInfo.Templet.End = DateTime.Parse(this.dtBookingDate.Value.ToString("yyyy-MM-dd") + " " + this.dtEnd.Value.ToString("HH:mm:ss"));//结束时间

                DateTime dtNow = this.regMgr.GetDateTimeFromSysDateTime();
                if (DateTime.Compare(this.dtBookingDate.Value.Date, dtNow.Date) > 0)
                {
                    this.regObj.DoctorInfo.SeeDate = DateTime.Parse(this.dtBookingDate.Value.ToString("yyyy-MM-dd") + " " + this.dtBegin.Value.ToString("HH:mm:ss"));
                }
                else
                {
                    if (dtNow <= this.regObj.DoctorInfo.Templet.Begin)
                    {
                        this.regObj.DoctorInfo.SeeDate = DateTime.Parse(dtNow.ToString("yyyy-MM-dd") + " " + this.dtBegin.Value.ToString("HH:mm:ss"));
                    }
                    else
                    {
                        this.regObj.DoctorInfo.SeeDate = dtNow;
                    }
                }
            }
            else//其他号不扣限额
            {
                if (DateTime.Compare(this.dtBookingDate.Value.Date, this.regMgr.GetDateTimeFromSysDateTime().Date) > 0)
                {
                    this.regObj.DoctorInfo.SeeDate = DateTime.Parse(this.dtBookingDate.Value.ToString("yyyy-MM-dd") + " " + this.dtBegin.Value.ToString("HH:mm:ss"));
                }
                else
                {
                    this.regObj.DoctorInfo.SeeDate = this.regMgr.GetDateTimeFromSysDateTime();
                }
                this.regObj.DoctorInfo.Templet.Begin = DateTime.Parse(this.regObj.DoctorInfo.SeeDate.Date.ToString("yyyy-MM-dd") + " " +
                        this.dtBegin.Value.ToString("HH:mm:ss"));
                this.regObj.DoctorInfo.Templet.End = DateTime.Parse(this.regObj.DoctorInfo.SeeDate.Date.ToString("yyyy-MM-dd") + " " +
                        this.dtEnd.Value.ToString("HH:mm:ss"));

                ///如果挂号日期大于今天,为预约挂明日的号,更新挂号时间
                ///
                if (this.regObj.DoctorInfo.SeeDate.Date < this.dtBookingDate.Value.Date)
                {
                    this.regObj.DoctorInfo.SeeDate = DateTime.Parse(this.dtBookingDate.Value.ToString("yyyy-MM-dd") + " " +
                        this.dtBegin.Value.ToString("HH:mm:ss"));//挂号时间
                    this.regObj.DoctorInfo.Templet.Begin = this.regObj.DoctorInfo.SeeDate;
                    this.regObj.DoctorInfo.Templet.End = DateTime.Parse(this.dtBookingDate.Value.ToString("yyyy-MM-dd") + " " +
                        this.dtEnd.Value.ToString("HH:mm:ss"));//结束时间

                    this.regObj.DoctorInfo.Templet.Noon.ID = this.getNoon(this.regObj.DoctorInfo.Templet.Begin);
                }
                else
                {
                    this.regObj.DoctorInfo.Templet.Noon.ID = this.getNoon(this.regObj.DoctorInfo.SeeDate);
                }


                if (this.regObj.DoctorInfo.Templet.Noon.ID == "")
                {
                    MessageBox.Show("未维护午别信息,请先维护!", "提示");
                    return -1;
                }
                this.regObj.DoctorInfo.Templet.ID = "";
            }
            #endregion

            if (this.isCurrRegDate)  //挂号时间是否取当前操作时间 {7E2D78C8-265E-4b54-AC5B-6DD927DDF81D}
            {
                this.regObj.DoctorInfo.SeeDate = this.regMgr.GetDateTimeFromSysDateTime();
            }

            if (this.regObj.Pact.PayKind.ID == "03")//公费日限判断
            {
                if (this.IsAllowPubReg(this.regObj.PID.CardNO, this.regObj.DoctorInfo.SeeDate) == -1) return -1;
            }

            #region 挂号费
            int rtn = ConvertRegFeeToObject(regObj);
            if (rtn == -1)
            {
                MessageBox.Show("获取挂号费出错!" + this.regFeeMgr.Err, "提示");
                this.cmbRegLevel.Focus();
                return -1;
            }
            if (rtn == 1)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("该挂号级别未维护挂号费,请先维护挂号费"), "提示");
                this.cmbRegLevel.Focus();
                return -1;
            }

            //获得患者应收、报销
            ConvertCostToObject(regObj, out lstAccFee);

            #endregion

            //处方号
            //  this.regObj.InvoiceNO = this.txtRecipeNo.Text.Trim();
            this.regObj.RecipeNO = this.txtRecipeNo.Text.Trim();


            this.regObj.IsFee = false;
            this.regObj.Status = FS.HISFC.Models.Base.EnumRegisterStatus.Valid;
            this.regObj.IsSee = false;
            this.regObj.InputOper.ID = this.regMgr.Operator.ID;
            this.regObj.InputOper.OperTime = this.regMgr.GetDateTimeFromSysDateTime();
            //add by niuxinyuan
            //this.regObj.DoctorInfo.SeeDate = this.regObj.InputOper.OperTime;
            this.regObj.DoctorInfo.Templet.Noon.Name = this.QeryNoonName(this.regObj.DoctorInfo.Templet.Noon.ID);
            // add by niuxinyuan
            this.regObj.CancelOper.ID = "";
            this.regObj.CancelOper.OperTime = DateTime.MinValue;
            ArrayList al = new ArrayList();

            if (this.chbEncrpt.Checked)
            {
                this.regObj.IsEncrypt = true;
                this.regObj.NormalName = FS.FrameWork.WinForms.Classes.Function.Encrypt3DES(this.regObj.Name);
                this.regObj.Name = "******";
            }

            this.regObj.IDCard = this.txtIdNO.Text;
            this.regObj.IsFee = true;
            return 0;
        }
        #endregion

        #region 判断是否帐户，如果是帐户，处理帐户
        ///// <summary>
        ///// 判断是否帐户，如果是帐户，处理帐户
        ///// </summary>
        ///// <returns></returns>
        //private int AccountPatient()
        //{
        //    decimal vacancy = 0;
        //    decimal OwnCostTot = this.regObj.OwnCost;
        //    int result = this.feeMgr.GetAccountVacancy(this.regObj.PID.CardNO, ref vacancy);
        //    if (result < 0)
        //    {
        //        MessageBox.Show(this.feeMgr.Err);
        //        return -1;
        //    }



        //    if (result > 0)
        //    {   //如果帐户余额等于0按自费处理（直接跳出）
        //        if (vacancy == 0)
        //        {
        //            return 1;
        //        }
        //        //if (IsAccountMessage)
        //        //{
        //        //    DialogResult diaResult = MessageBox.Show("是否使用帐户支付？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //        //    if (diaResult == DialogResult.No)
        //        //    {
        //        //        return 1;
        //        //    }
        //        //}
        //        //余额不够扣 
        //        if (vacancy < this.regObj.OwnCost)
        //        {
        //            //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
        //            //bool returnValue = this.feeMgr.AccountPay(this.regObj.PID.CardNO, vacancy, this.regObj.InvoiceNO, this.regObj.DoctorInfo.Templet.Dept.ID);
        //            if (!feeMgr.CheckAccountPassWord(this.regObj))
        //            {
        //                return -1;
        //            }
        //            int returnValue = this.feeMgr.AccountPay(this.regObj, vacancy, this.regObj.InvoiceNO, this.regObj.DoctorInfo.Templet.Dept.ID, "R");
        //            {
        //                if (returnValue < 0)
        //                {
        //                    MessageBox.Show(this.feeMgr.Err);
        //                    return -1;
        //                }
        //                this.regObj.PayCost = vacancy;
        //                this.regObj.OwnCost = this.regObj.OwnCost - vacancy;
        //            }

        //        }
        //        else //余额够扣
        //        {
        //            if (!feeMgr.CheckAccountPassWord(this.regObj))
        //            {
        //                return -1;
        //            }

        //            int returnValue = this.feeMgr.AccountPay(this.regObj,this.regObj.OwnCost, this.regObj.InvoiceNO, this.regObj.DoctorInfo.Templet.Dept.ID, "R");
        //            //if (returnValue == false)
        //            if (returnValue < 0)
        //            {
        //                MessageBox.Show(this.feeMgr.Err);
        //                return -1;
        //            }
        //            this.regObj.PayCost = this.regObj.OwnCost;
        //            this.regObj.OwnCost = 0;

        //        }

        //    }


        //    return 1;
        //}
        #endregion

        #region 校验医保卡号
        /// <summary>
        /// 判断医疗证号是否已经封锁
        /// </summary>
        /// <param name="pactID"></param>
        /// <param name="mcardNo"></param>
        /// <returns></returns>
        private int validMcardNo(string pactID, string mcardNo)
        {
            //本院职工判断医疗证号是否冻结
            //FS.HISFC.BizLogic.Medical.MedicalCard mCardMgr = new FS.HISFC.BizLogic.Medical.MedicalCard();

            //if (!mCardMgr.isValidInnerEmployee(pactID, mcardNo))
            //{
            //    MessageBox.Show("本院职工医疗证已被封锁,不能使用!");
            //    this.cmbPayKind.Focus();
            //    return -1;
            //}

            ////判断医疗证是否在全院黑名单
            //FS.HISFC.BizLogic.Fee.Interface interfaceMgr = new FS.HISFC.BizLogic.Fee.Interface();

            //if (interfaceMgr.ExistBlackList(pactID, mcardNo))
            //{
            //    MessageBox.Show("该患者医疗证在人员黑名单中,不能挂号!");
            //    this.cmbPayKind.Focus();
            //    return -1;
            //}

            return 0;
        }
        #endregion

        #region 更新患者基本信息
        /// <summary>
        /// 更新患者基本信息
        /// </summary>
        /// <param name="regInfo"></param>
        /// <param name="patMgr"></param>
        /// <param name="registerMgr"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int UpdatePatientinfo(FS.HISFC.Models.Registration.Register regInfo,
            FS.HISFC.BizProcess.Integrate.RADT patMgr, FS.HISFC.BizLogic.Registration.Register registerMgr,
            ref string Err)
        {

            //增加判断原有患者姓名不同，不允许修改姓名，挂号直接改姓名不出错了？lingk
            Hashtable hsInfo = new Hashtable();
            ArrayList al = patientMgr.QueryPatientByName(regInfo.Name);
            if (al != null)
            {
                if (al.Count > 0)
                {
                    foreach (FS.HISFC.Models.RADT.PatientInfo obj in al)
                    {
                        if (obj != null)
                        {
                            hsInfo.Add(obj.PID.CardNO, obj.Name);
                        }
                    }
                }
            }
            FS.HISFC.Models.RADT.PatientInfo pInfo = new FS.HISFC.Models.RADT.PatientInfo();
            pInfo = patientMgr.QueryComPatientInfo(regInfo.PID.CardNO);
            if (!hsInfo.ContainsKey(regInfo.PID.CardNO) && pInfo != null && !string.IsNullOrEmpty(pInfo.PID.CardNO))
            {
                Err = "已有患者信息不能修改姓名！";
                return -1;
            }

            int rtn = registerMgr.Update(FS.HISFC.BizLogic.Registration.EnumUpdateStatus.PatientInfo,
                                            regInfo);

            if (rtn == -1)
            {
                Err = registerMgr.Err;
                return -1;
            }

            if (rtn == 0)//没有更新到患者信息，插入
            {
                FS.HISFC.Models.RADT.PatientInfo p = new FS.HISFC.Models.RADT.PatientInfo();

                p.PID.CardNO = regInfo.PID.CardNO;
                p.Name = regInfo.Name;
                p.Sex.ID = regInfo.Sex.ID;
                p.Birthday = regInfo.Birthday;
                p.Pact = regInfo.Pact;
                p.Pact.PayKind.ID = regInfo.Pact.PayKind.ID;
                p.SSN = regInfo.SSN;
                p.PhoneHome = regInfo.PhoneHome;
                p.AddressHome = regInfo.AddressHome;
                p.IDCard = regInfo.IDCard;
                p.Memo = regInfo.CardType.ID;
                p.NormalName = regInfo.NormalName;
                p.IsEncrypt = regInfo.IsEncrypt;

                if (patientMgr.RegisterComPatient(p) == -1)
                {
                    Err = patientMgr.Err;
                    return -1;
                }
            }

            return 0;
        }
        #endregion

        #region 医保接口,先屏蔽
        /*
        /// <summary>
        /// 医保接口
        /// </summary>
        /// <param name="reg"></param>
        /// <param name="MedMgr"></param>
        /// <param name="ifeMgr"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int RegSI(FS.HISFC.Models.Registration.Register reg,
            MedicareInterface.Class.Clinic MedMgr, FS.HISFC.BizLogic.Fee.Interface ifeMgr, ref string Err)
        {
            //连接医保
            //if (MedMgr.Connect(reg.Pact.ID) == false)
            //{
            //    Err = MedMgr.ErrMsg;
            //    return -1;
            //}

            ////获取医保登记信息
            //if (!MedMgr.Reg(ref reg))
            //{
            //    Err = MedMgr.ErrMsg;
            //    return -1;
            //}

            //保存到本地
            //			if( ifeMgr.InsertSIMainInfo(reg) == -1)
            //			{
            //				Err = ifeMgr.Err ;
            //				return -1 ;
            //			}

            //断开连接
            //if (!MedMgr.DisConnect(reg.Pact.ID))
            //{
            //    Err = MedMgr.ErrMsg;
            //    return -1;
            //}

            return 0;
        }*/

        #endregion

        #region 验证
        /// <summary>
        /// 获取有效的排班信息。适用于
        /// 不是从项目列表选择看诊时间段,而是直接输入
        /// 看诊时间段
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Registration.Schema GetValidSchema(FS.HISFC.Models.Registration.RegLevel level)
        {
            FS.HISFC.Models.Registration.Schema schema = (FS.HISFC.Models.Registration.Schema)this.dtBookingDate.Tag;
            if (schema != null) return schema;

            DateTime bookingDate = this.dtBookingDate.Value.Date;
            al = null;

            if (level.IsFaculty)//专科号
            {
                if (!this.IsJudgeReglevl)
                {
                    al = this.SchemaMgr.QueryByDept(bookingDate.Date, this.cmbDept.Tag.ToString());
                }
                else
                {
                    al = this.SchemaMgr.QueryByDept(bookingDate.Date, this.cmbDept.Tag.ToString(), level.ID);
                }
            }
            else if (level.IsExpert)//专家号
            {
                al = this.SchemaMgr.QueryByDoct(bookingDate.Date, this.cmbDoctor.Tag.ToString());
            }
            else if (level.IsSpecial)//特诊号
            {
                al = this.SchemaMgr.QueryByDoct(bookingDate.Date, this.cmbDoctor.Tag.ToString());
            }

            if (al == null || al.Count == 0) return null;

            return this.GetValidSchema(al, level);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="Schemas"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Registration.Schema GetValidSchema(ArrayList Schemas,
            FS.HISFC.Models.Registration.RegLevel level)
        {
            DateTime current = this.SchemaMgr.GetDateTimeFromSysDateTime();
            DateTime begin = this.dtBegin.Value;
            DateTime end = this.dtEnd.Value;

            string currentNoon = this.getNoon(current);

            foreach (FS.HISFC.Models.Registration.Schema obj in Schemas)
            {
                if (obj.SeeDate < current.Date) continue;//小于当前日期

                //只有当日的才判断时间
                if (obj.SeeDate == current.Date)
                {
                    if (obj.Templet.Begin.TimeOfDay != begin.TimeOfDay) continue;//开始时间不等
                    if (obj.Templet.End.TimeOfDay != end.TimeOfDay) continue;//结束时间不等
                }

                #region 因为允许超限挂号,所以不过滤
                /*
                if(level.IsFaculty || level.IsExpert)
                {
                    if(!obj.Templet.IsAppend && obj.Templet.RegLmt == 0)continue ;//没有设定预约限额				
                    if(!obj.Templet.IsAppend && obj.Templet.RegLmt <= obj.RegedQty) continue;//超出限额
                }
                else if(level.IsSpecial)
                {
                    if(!obj.Templet.IsAppend && obj.Templet.SpeLmt == 0)continue ;//没有设定预约限额				
                    if(!obj.Templet.IsAppend && obj.Templet.SpeLmt <= obj.SpeReged) continue;//超出限额
                }*/
                #endregion

                if (!obj.Templet.IsAppend)
                {
                    //
                    //只有日期相同,才判断时间是否超时,否则就是预约到以后日期,时间不用判断
                    //
                    if (current.Date == obj.SeeDate)
                    {
                        if (obj.Templet.End.TimeOfDay < current.TimeOfDay) continue;//时间小于当前时间
                    }
                }
                else
                {
                    if (obj.SeeDate.Date == current.Date)//当日挂号,加号不能全为上午,需根据当前时间判断应是上午还是下午加号
                    {
                        if (currentNoon != obj.Templet.Noon.ID) continue;
                    }
                }

                return obj;
            }
            return null;
        }


        /// <summary>
        /// 公费患者日限判断
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="regDate"></param>
        /// <returns></returns>
        private int IsAllowPubReg(string cardNo, DateTime regDate)
        {
            int num = this.regMgr.QuerySeeNum(cardNo, regDate);
            if (num == -1)
            {
                MessageBox.Show(this.regMgr.Err, "提示");
                return -1;
            }

            if (num >= this.DayRegNumOfPub)
            {
                DialogResult dr = MessageBox.Show("公费患者挂号日限:" + this.DayRegNumOfPub.ToString() + ", 该患者已挂号数量:" +
                    num.ToString() + ",是否允许继续挂号?", "提示", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (dr == DialogResult.No)
                {
                    this.txtCardNo.Focus();
                    return -1;
                }
            }

            return 0;
        }


        /// <summary>
        /// 更新处方号		
        /// </summary>
        /// <param name="Cnt"></param>
        private void UpdateRecipeNo(int Cnt)
        {
            this.txtRecipeNo.Text = Convert.ToString(long.Parse(this.txtRecipeNo.Text.Trim()) + Cnt);
        }
        #endregion

        #region 更新看诊序号
        /// <summary>
        /// 更新全院看诊序号
        /// </summary>
        /// <param name="rMgr"></param>
        /// <param name="current"></param>
        /// <param name="seeNo"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int Update(FS.HISFC.BizLogic.Registration.Register rMgr, DateTime current, ref int seeNo,
            ref string Err)
        {
            //更新看诊序号
            //全院是全天大排序，所以午别不生效，默认 1
            if (rMgr.UpdateSeeNo("4", current, "ALL", "1") == -1)
            {
                Err = rMgr.Err;
                return -1;
            }

            //获取全院看诊序号
            if (rMgr.GetSeeNo("4", current, "ALL", "1", ref seeNo) == -1)
            {
                Err = rMgr.Err;
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// 更新医生或科室的看诊序号
        /// </summary>
        /// <param name="deptID"></param>
        /// <param name="doctID"></param>
        /// <param name="noonID"></param>
        /// <param name="regDate"></param>
        /// <param name="seeNo"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int UpdateSeeID(string deptID, string doctID, string noonID, DateTime regDate,
            ref int seeNo, ref string Err)
        {
            string Type = "", Subject = "";

            #region ""

            if (doctID != null && doctID != "")
            {
                Type = "1";//医生
                Subject = doctID;
            }
            else
            {
                Type = "2";//科室
                Subject = deptID;
            }

            #endregion

            //更新看诊序号
            if (this.regMgr.UpdateSeeNo(Type, regDate, Subject, noonID) == -1)
            {
                Err = this.regMgr.Err;
                return -1;
            }

            //获取看诊序号		
            if (this.regMgr.GetSeeNo(Type, regDate, Subject, noonID, ref seeNo) == -1)
            {
                Err = this.regMgr.Err;
                return -1;
            }

            return 0;
        }

        #endregion

        #region 更新看诊限额
        /// <summary>
        /// 更新看诊限额
        /// </summary>
        /// <param name="SchMgr"></param>
        /// <param name="regType"></param>
        /// <param name="seeNo"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int UpdateSchema(FS.HISFC.BizLogic.Registration.Schema SchMgr,
            FS.HISFC.Models.Base.EnumRegType regType, ref int seeNo, ref string Err)
        {
            int rtn = 1;
            //挂号级别
            FS.HISFC.Models.Registration.RegLevel level =
                                (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;

            if (regType == FS.HISFC.Models.Base.EnumRegType.Pre)//预约号,更新预约限额
            {
                FS.HISFC.Models.Registration.Booking booking =
                                        (FS.HISFC.Models.Registration.Booking)this.txtOrder.Tag;

                rtn = SchMgr.Increase(booking.DoctorInfo.Templet.ID, false, false, true, false);

                //判断限额是否允许挂号

                if (this.IsPermitOverrun(SchMgr, regType, booking.DoctorInfo.Templet.ID, level, ref seeNo, ref Err) == -1)
                {
                    return -1;
                }
            }
            //else if(regType == FS.HISFC.Models.Registration.RegTypeNUM.Reg) 
            else if (level.IsFaculty || level.IsExpert)//专家、专科,扣挂号限额
            {
                if ((this.dtBookingDate.Tag as FS.HISFC.Models.Registration.Schema).Templet.RegQuota - (this.dtBookingDate.Tag as FS.HISFC.Models.Registration.Schema).RegedQTY > 0)
                {
                    rtn = SchMgr.Increase(
                        (this.dtBookingDate.Tag as FS.HISFC.Models.Registration.Schema).Templet.ID,
                        true, false, false, false);
                }
                else//减预约限额
                {
                    rtn = SchMgr.Increase(
                        (this.dtBookingDate.Tag as FS.HISFC.Models.Registration.Schema).Templet.ID,
                        false, true, true, false);
                }

                //判断限额是否允许挂号
                if (this.IsPermitOverrun(SchMgr, regType, (this.dtBookingDate.Tag as FS.HISFC.Models.Registration.Schema).Templet.ID,
                                            level, ref seeNo, ref Err) == -1)
                {
                    return -1;
                }
            }
            //else if(regType == FS.HISFC.Models.Registration.RegTypeNUM.Spe) 
            else if (level.IsSpecial && !isFilterDoc)//特诊扣特诊限额
            {
                rtn = SchMgr.Increase(
                    (this.dtBookingDate.Tag as FS.HISFC.Models.Registration.Schema).Templet.ID,
                    false, false, false, true);

                //判断限额是否允许挂号

                if (this.IsPermitOverrun(SchMgr, regType, (this.dtBookingDate.Tag as FS.HISFC.Models.Registration.Schema).Templet.ID,
                                    level, ref seeNo, ref Err) == -1)
                {
                    return -1;
                }
            }

            if (rtn == -1)
            {
                Err = "更新排班看诊限额时出错!" + SchMgr.Err;
                return -1;
            }

            if (rtn == 0)
            {
                Err = FS.FrameWork.Management.Language.Msg("医生排班信息已经改变,请重新选择看诊时段");
                return -1;
            }

            return 0;
        }
        #endregion

        #region 判断超出挂号限额是否允许挂号
        /// <summary>
        /// 判断超出挂号限额是否允许挂号
        /// </summary>
        /// <param name="schMgr"></param>
        /// <param name="regType"></param>
        /// <param name="schemaID"></param>
        /// <param name="level"></param>
        /// <param name="seeNo"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int IsPermitOverrun(FS.HISFC.BizLogic.Registration.Schema schMgr,
                    FS.HISFC.Models.Base.EnumRegType regType,
                    string schemaID, FS.HISFC.Models.Registration.RegLevel level,
                    ref int seeNo, ref string Err)
        {
            bool isOverrun = false;//是否超额

            FS.HISFC.Models.Registration.Schema schema = schMgr.GetByID(schemaID);
            if (schema == null || schema.Templet.ID == "")
            {
                Err = "查询排班信息出错!" + schMgr.Err;
                return -1;
            }

            if (regType == FS.HISFC.Models.Base.EnumRegType.Pre)//预约号,不用判断限额,因为预约时已经判断
            {
                if (this.IsPreFirst)
                {
                    seeNo = int.Parse(schema.TeledQTY.ToString());
                }
                else
                {
                    //seeNo = int.Parse(Convert.ToString(schema.RegedQty + schema.TelReged + schema.SpeReged)) ;//获得当前时段已看诊数,作为看诊序列号
                    seeNo = schema.SeeNO;
                }
            }
            else if (level.IsExpert || level.IsFaculty)//专家、专科判断限额是否大于已挂号
            {
                if (schema.Templet.RegQuota - schema.RegedQTY < 0)
                {
                    if (schema.Templet.TelQuota - schema.TelingQTY < 0)
                    {
                        isOverrun = true;
                    }
                }

                if (this.IsPreFirst)
                {
                    //seeNo = int.Parse(Convert.ToString(schema.RegedQty + schema.TelReging + schema.SpeReged)) ;//获得当前时段已看诊数,作为看诊序列号
                    seeNo = int.Parse(Convert.ToString(schema.SeeNO + schema.TelingQTY - schema.TeledQTY));
                }
                else
                {
                    //seeNo = int.Parse(Convert.ToString(schema.RegedQty + schema.TelReged + schema.SpeReged)) ;//获得当前时段已看诊数,作为看诊序列号
                    seeNo = schema.SeeNO;
                }
            }
            else if (level.IsSpecial)//特诊判断特诊限额是否超表
            {
                if (schema.Templet.SpeQuota - schema.SpedQTY < 0)
                {
                    isOverrun = true;
                }

                if (this.IsPreFirst)
                {
                    //seeNo = int.Parse(Convert.ToString(schema.RegedQty + schema.TelReging + schema.SpeReged)) ;//获得当前时段已看诊数,作为看诊序列号
                    seeNo = int.Parse(Convert.ToString(schema.SeeNO + schema.TelingQTY - schema.TeledQTY));
                }
                else
                {
                    //seeNo = int.Parse(Convert.ToString(schema.RegedQty + schema.TelReged + schema.SpeReged)) ;//获得当前时段已看诊数,作为看诊序列号
                    seeNo = schema.SeeNO;
                }
            }

            if (isOverrun)
            {
                //加号不用提示
                if (schema.Templet.IsAppend) return 0;

                if (!this.IsAllowOverrun)
                {
                    Err = "已经超出出诊排班限额,不能挂号!";
                    return -1;
                }
                else
                {
                    frmWaitingAnswer f = new frmWaitingAnswer();
                    DialogResult dr = f.ShowDialog();//防止锁死，3秒后关闭
                    f.Dispose();

                    //DialogResult dr = MessageBox.Show("挂号数已经大于设号数,是否继续?","提示",MessageBoxButtons.YesNo,
                    //	MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) ;

                    //选择No
                    if (dr == DialogResult.No)
                    {
                        return -1;
                    }
                }
            }

            return 0;
        }
        #endregion

        #region 打印挂号票

        /// <summary>
        /// 打印挂号发票
        /// </summary>
        /// <param name="regObj"></param>
        private void Print(FS.HISFC.Models.Registration.Register regObj, FS.HISFC.BizLogic.Registration.Register regmr)
        {
            #region 屏蔽
            /*if( this.PrintWhat == "Invoice")//打印发票
            {
                this.ucInvoice.Registeration = regObj ;
			
                System.Drawing.Printing.PaperSize size ;

                if( PrintCnt % 2 == 0)
                {
                    size = new System.Drawing.Printing.PaperSize("RegInvoice1", 425 ,288);
                }
                else
                {
                    size = new System.Drawing.Printing.PaperSize("RegInvoice2",425,280) ;
                }

                PrintCnt ++ ;

                printer.SetPageSize(size);
                printer.PrintPage(0,0,ucInvoice) ;
            }
            else//打印处方
            {
                //fuck
                FS.neuFC.Object.neuObject obj = this.conMgr.Get("PrintRecipe",regObj.RegDept.ID) ;

                //不包含的，都打印
                if( obj == null || obj.ID == "")
                {
                    this.ucBill.Register = regObj ;
					
                    System.Drawing.Printing.PaperSize size = new System.Drawing.Printing.PaperSize("Recipe", 670 ,1120);
                    printer.SetPageSize(size);
                    printer.PrintPage(0,0,this.ucBill) ;
                }
            }*/
            #endregion
            #region by niuxy
            /*
            try
            {
                if (IRegPrint != null)
                {
                    this.IRegPrint.RegInfo = regObj;
                    this.IRegPrint.Print();
                }
            }
            catch { }
             */
            #endregion
            FS.HISFC.BizProcess.Interface.Registration.IRegPrint regprint = null;
            regprint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Registration.IRegPrint)) as FS.HISFC.BizProcess.Interface.Registration.IRegPrint;
            if (regprint == null)
            {
                MessageBox.Show(FS.FrameWork.WinForms.Classes.UtilInterface.Err, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //regprint.SetPrintValue(regObj,regmr);
            if (regObj.IsEncrypt)
            {
                regObj.Name = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(this.regObj.NormalName);
            }
            if (ISATMPRINT)
                regObj.InputOper.ID = regMgr.Operator.ID;
            regprint.SetPrintValue(regObj);
            regprint.Print();
            //regprint.PrintView();

            if (regObj.PrintInvoiceCnt == 0)
            {
                //找零窗口
                if (this.isShowChangeCostForm)
                {
                    this.ShowChangeForm(regObj);
                }
                else
                {
                    //弹出窗口提示收费员收取多少钱
                    decimal totCost = 0m;
                    foreach (FS.HISFC.Models.Account.AccountCardFee accFee in regObj.LstCardFee)
                    {
                        totCost += accFee.Own_cost;
                    }
                    if (!this.IsJudgeReglevl)//深圳医院临时用这个参数来判断处理，后续修改getCost（）
                    {
                        MessageBox.Show("应收金额：" + regObj.OwnCost.ToString("F2") + " 元", "提示");
                    }
                    else
                    {
                        MessageBox.Show("应收金额：" + totCost.ToString("F2") + " 元", "提示");
                    }
                }
            }

        }

        /// <summary>
        /// 打印条码
        /// </summary>
        /// <param name="regObj"></param>
        private void PrintBarCode(FS.HISFC.Models.Registration.Register register)
        {
            //if (register.PrintInvoiceCnt == 2 || (register.PrintInvoiceCnt == 0 && register.IsFirst))
            //本地化里面判断是否打印条码
            {
                FS.HISFC.BizProcess.Interface.Registration.IPrintBar printBarCode = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Registration.IPrintBar)) as FS.HISFC.BizProcess.Interface.Registration.IPrintBar;
                if (printBarCode != null)
                {
                    string errText = string.Empty;
                    if (printBarCode.printBar(register, ref errText) < 0)
                    {
                        MessageBox.Show(errText, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

        }
        #endregion

        #region 补打
        /// <summary>
        /// 重打
        /// </summary>
        private void Reprint()
        {
            string Err = "";

            int row = this.fpList.ActiveRowIndex;

            //			if(row <0 || this.fpList.RowCount == 0) return ;

            FS.HISFC.Models.Registration.Register obj;

            if (ISATMPRINT)
            {
                frmModifyRegistrationatm f = new frmModifyRegistrationatm();
                DialogResult dr = f.ShowDialog();

                if (dr != DialogResult.OK) return;
                obj = f.Register;
                f.Dispose();
            }
            else
            {
                frmModifyRegistration f = new frmModifyRegistration();
                DialogResult dr = f.ShowDialog();

                if (dr != DialogResult.OK) return;
                obj = f.Register;
                f.Dispose();
            }



            //不再作废挂号记录，因为发票号已经存入fin_opb_accountcardfee
            //发票重打只退掉fin_opb_accountcardfee的原发票信息
            //重新获取新发票号
            FS.HISFC.BizLogic.Registration.EnumUpdateStatus flag = FS.HISFC.BizLogic.Registration.EnumUpdateStatus.Cancel;

            FS.HISFC.BizLogic.Fee.Account accountFeeMgr = new FS.HISFC.BizLogic.Fee.Account();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            try
            {
                accountFeeMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.regMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.feeMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.patientMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                DateTime current = this.regMgr.GetDateTimeFromSysDateTime();
                //查找对应的发票信息
                List<FS.HISFC.Models.Account.AccountCardFee> cardFeeList = new List<AccountCardFee>();
                if (accountFeeMgr.QueryAccCardFeeByClinic(obj.PID.CardNO, obj.ID, out cardFeeList) == -1 || cardFeeList == null)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("取发票信息失败，原因：" + accountFeeMgr.Err, "提示");
                    return;
                }

                if (cardFeeList.Count == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("没有需要重打发票信息", "提示");
                    return;
                }

                #region 取发票号

                string strInvioceNO = "";
                string strRealInvoiceNO = "";
                string strErrText = "";
                int iRes = 0;
                string strInvoiceType = "R";

                FS.HISFC.Models.Base.Employee employee = this.regMgr.Operator as FS.HISFC.Models.Base.Employee;

                if (this.GetRecipeType == 1)
                {
                    strInvioceNO = this.txtRecipeNo.Text.Trim().PadLeft(12, '0');
                    strRealInvoiceNO = "";
                }
                else
                {
                    if (this.GetRecipeType == 2)
                    {
                        strInvoiceType = "R";
                    }
                    else if (this.GetRecipeType == 3)
                    {
                        // 取门诊收据
                        strInvoiceType = "C";
                    }

                    iRes = this.feeMgr.GetInvoiceNO(employee, strInvoiceType, ref strInvioceNO, ref strRealInvoiceNO, ref strErrText);

                    if (iRes == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("取门诊收据失败，原因：" + strErrText);
                        return;
                    }
                }


                #endregion

                #region 处理发票信息
                List<FS.HISFC.Models.Account.AccountCardFee> listReprint = new List<AccountCardFee>();
                foreach (FS.HISFC.Models.Account.AccountCardFee accountCardFee in cardFeeList)
                {
                    if (accountCardFee.TransType == FS.HISFC.Models.Base.TransTypes.Negative)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("发票已作废信息，不允许重打", "提示");
                        return;
                    }

                    if (accountCardFee.IStatus != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("发票已退费，不允许重打", "提示");
                        return;
                    }
                    //生成负记录
                    FS.HISFC.Models.Account.AccountCardFee returnCardFee = accountCardFee.Clone();
                    returnCardFee.FeeOper.ID = accountFeeMgr.Operator.ID;
                    returnCardFee.FeeOper.OperTime = current;
                    returnCardFee.Oper.ID = accountFeeMgr.Operator.ID;
                    returnCardFee.Oper.ID = accountFeeMgr.Operator.ID;
                    returnCardFee.Oper.OperTime = current;
                    returnCardFee.PayType.ID = accountCardFee.PayType.ID;
                    returnCardFee.PayType.Memo = accountCardFee.PayType.Memo;
                    returnCardFee.PayType.Name = accountCardFee.PayType.Name;
                    returnCardFee.Tot_cost = -returnCardFee.Tot_cost;
                    returnCardFee.Own_cost = -returnCardFee.Own_cost;
                    returnCardFee.Pub_cost = -returnCardFee.Pub_cost;
                    returnCardFee.Pay_cost = -returnCardFee.Pay_cost;
                    returnCardFee.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                    returnCardFee.IStatus = 2;

                    if (accountFeeMgr.InsertAccountCardFee(returnCardFee) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("插入退费信息失败，原因：" + accountFeeMgr.Err, "提示");
                        return;
                    }

                    //生成新纪录
                    FS.HISFC.Models.Account.AccountCardFee newCardFee = accountCardFee.Clone();

                    newCardFee.InvoiceNo = strInvioceNO;
                    newCardFee.Print_InvoiceNo = strRealInvoiceNO;
                    newCardFee.FeeOper.ID = accountFeeMgr.Operator.ID;
                    newCardFee.FeeOper.OperTime = current;
                    newCardFee.Oper.ID = accountFeeMgr.Operator.ID;
                    newCardFee.Oper.ID = accountFeeMgr.Operator.ID;
                    newCardFee.Oper.OperTime = current;
                    newCardFee.PayType.ID = accountCardFee.PayType.ID;
                    newCardFee.PayType.Memo = accountCardFee.PayType.Memo;
                    newCardFee.PayType.Name = accountCardFee.PayType.Name;
                    newCardFee.IStatus = 1;
                    newCardFee.TransType = FS.HISFC.Models.Base.TransTypes.Positive;

                    if (accountFeeMgr.InsertAccountCardFee(newCardFee) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("插入收费信息失败，原因：" + accountFeeMgr.Err, "提示");
                        return;
                    }

                    //原始记录退费

                    if (accountFeeMgr.CancelAccountCardFee(accountCardFee.InvoiceNo, FS.HISFC.Models.Base.TransTypes.Positive, accountCardFee.FeeType) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("作废收费信息失败，原因：" + accountFeeMgr.Err, "提示");
                        return;
                    }
                }
                #endregion

                #region 处理挂号记录

                obj.InvoiceNO = strInvioceNO;
                obj.RecipeNO = this.txtRecipeNo.Text.Trim().PadLeft(12, '0');
                //更新发票号，费用不变
                iRes = this.regMgr.UpdateRegFeeCost(obj);
                if (iRes <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this, "处理挂号记录失败，原因：" + this.regMgr.Err, "提示");
                    return;
                }

                #endregion

                #region 发票走号

                if (this.GetRecipeType == 2 || this.GetRecipeType == 3)
                {
                    string invoiceStytle = controlParma.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.GET_INVOICE_NO_TYPE, false, "0");
                    if (this.feeMgr.UseInvoiceNO(employee, invoiceStytle, strInvoiceType, 1, ref strInvioceNO, ref strRealInvoiceNO, ref strErrText) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.feeMgr.Err);
                        return;
                    }

                    if (this.feeMgr.InsertInvoiceExtend(strInvioceNO, strInvoiceType, strRealInvoiceNO, "00") < 1)
                    {
                        // 发票头暂时先保存00
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.feeMgr.Err);
                        return;
                    }
                }

                #endregion

                //更新患者基本信息
                if (this.UpdatePatientinfo(obj, this.patientMgr, this.regMgr, ref Err) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Err, "提示");
                    return;
                }

                FS.FrameWork.Management.PublicTrans.Commit();

                //最后加处方号,防止跳号
                this.UpdateRecipeNo(1);
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "提示");
                return;
            }

            this.Print(obj, this.regMgr);

            this.Retrieve();
            this.cmbRegLevel.Focus();
            this.Clear();

        }

        /// <summary>
        /// 发票只打印
        /// </summary>
        private void OnlyPrint()
        {
            FS.HISFC.Models.Registration.Register obj;

            frmAccountCardFeeInvoiceReprint f = new frmAccountCardFeeInvoiceReprint();
            DialogResult dr = f.ShowDialog();

            if (dr != DialogResult.OK) return;
            obj = f.Register;
            f.Dispose();

            this.Print(obj, this.regMgr);

            this.PrintBarCode(obj);
        }

        /// <summary>
        /// 补打条码
        /// </summary>
        private void ReprintBarCode()
        {
            if (this.regObj == null || string.IsNullOrEmpty(this.regObj.PID.CardNO))
            {
                MessageBox.Show("请刷卡输入患者信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.regObj.PrintInvoiceCnt = 2;
            this.PrintBarCode(this.regObj);
            this.regObj.PrintInvoiceCnt = 0;
        }

        #endregion

        #region 扣限额
        /// <summary>
        /// 扣限额
        /// </summary>
        /// <returns></returns>
        private int Reduce()
        {
            #region 验证
            if (this.cmbRegLevel.Tag == null || this.cmbRegLevel.Tag.ToString() == "")
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("请选择挂号级别"), "提示");
                this.cmbRegLevel.Focus();
                return -2;
            }

            FS.HISFC.Models.Registration.RegLevel level = (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;

            //必须是专家号才能扣限额
            if (!(level.IsExpert || level.IsFaculty || level.IsSpecial))
            {
                MessageBox.Show("非专家/专科号不能扣限额!", "提示");
                this.cmbRegLevel.Focus();
                return -2;
            }

            if ((this.cmbDept.Tag == null || this.cmbDept.Tag.ToString() == ""))
            {
                MessageBox.Show("请输入挂号科室!", "提示");
                this.cmbDept.Focus();
                return -2;
            }

            if ((level.IsExpert || level.IsSpecial) &&
                (this.cmbDoctor.Tag == null || this.cmbDoctor.Tag.ToString() == ""))
            {
                MessageBox.Show("专家号必须指定看诊医生!", "提示");
                this.cmbDoctor.Focus();
                return -2;
            }

            FS.HISFC.Models.Registration.Schema schema;//排班实体

            //查询符合条件的排班信息
            schema = this.GetValidSchema(level);
            if (schema == null)
            {
                MessageBox.Show("预约时间指定错误,没有符合条件的排班信息!", "提示");
                this.dtBookingDate.Focus();
                return -2;
            }
            this.SetBookingTag(schema);

            #endregion

            int seeNo = 0;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(regMgr.con);
            //SQLCA.BeginTransaction();

            try
            {
                this.SchemaMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.regMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                string Err = "";

                #region 更新看诊序号

                //获取看诊序号
                string noon = schema.Templet.Noon.ID;//午别

                if (this.UpdateSeeID(this.cmbDept.Tag.ToString(), this.cmbDoctor.Tag.ToString(), noon, this.dtBookingDate.Value.Date,
                    ref seeNo, ref Err) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Err, "提示");
                    return -1;
                }

                #endregion

                FS.HISFC.Models.Base.EnumRegType regType = FS.HISFC.Models.Base.EnumRegType.Reg;
                //不为空说明是预约号
                if (this.txtOrder.Tag != null)
                {
                    regType = FS.HISFC.Models.Base.EnumRegType.Pre;
                }
                else if (level.IsSpecial)
                {
                    regType = FS.HISFC.Models.Base.EnumRegType.Spe;
                }

                //更新排班限额
                if (this.UpdateSchema(this.SchemaMgr, regType, ref seeNo, ref Err) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    if (Err != "") MessageBox.Show(Err, "提示");
                    return -1;
                }

                //获取全院看诊序号
                int i = 0;

                if (this.Update(this.regMgr, this.regMgr.GetDateTimeFromSysDateTime(), ref i, ref Err) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Err, "提示");
                    return -1;
                }

                FS.FrameWork.Management.PublicTrans.Commit();

            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "提示");
                return -1;
            }

            string Msg = "";

            Msg = "[" + seeNo.ToString() + "]";

            MessageBox.Show("更新成功! 流水号为:" + Msg, "提示");
            this.Clear();
            //需要重新刷新排班
            this.init();
            return 0;
        }
        #endregion

        #region 暂存患者基本信息
        /// <summary>
        /// 保存患者信息
        /// </summary>
        /// <returns></returns>
        private int SavePatient()
        {
            #region 验证
            if (this.regObj == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("请录入挂号患者"), "提示");
                this.txtCardNo.Focus();
                return -1;
            }

            if (this.txtName.Text.Trim() == "")
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("请输入患者姓名"), "提示");
                this.txtName.Focus();
                return -1;
            }

            if (FS.FrameWork.Public.String.ValidMaxLengh(this.txtName.Text.Trim(), 40) == false)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("患者名称最多可录入20个汉字"), "提示");
                this.txtName.Focus();
                return -1;
            }

            if (this.cmbSex.Tag == null || this.cmbSex.Tag.ToString() == "")
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("请选择患者性别"), "提示");
                this.cmbSex.Focus();
                return -1;
            }

            if (this.cmbPayKind.Tag == null || this.cmbPayKind.Tag.ToString() == "")
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("患者结算类别不能为空"), "提示");
                this.cmbPayKind.Focus();
                return -1;
            }

            if (FS.FrameWork.Public.String.ValidMaxLengh(this.txtPhone.Text.Trim(), 20) == false)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("联系电话最多可录入20位数字"), "提示");
                this.txtPhone.Focus();
                return -1;
            }

            if (FS.FrameWork.Public.String.ValidMaxLengh(this.txtAddress.Text.Trim(), 60) == false)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("联系人地址最多可录入30个汉字"), "提示");
                this.txtAddress.Focus();
                return -1;
            }

            if (FS.FrameWork.Public.String.ValidMaxLengh(this.txtMcardNo.Text.Trim(), 18) == false)
            {
                MessageBox.Show("医疗证号最多可录入18位数字!", "提示");
                this.txtMcardNo.Focus();
                return -1;
            }

            if (FS.FrameWork.Public.String.ValidMaxLengh(this.txtAge.Text.Trim(), 3) == false)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("年龄最多可录入3位数字"), "提示");
                this.txtAge.Focus();
                return -1;
            }

            if (this.txtAge.Text.Trim().Length > 0)
            {
                try
                {
                    int age = int.Parse(this.txtAge.Text.Trim());
                    if (age <= 0)
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("年龄不能为负数"), "提示");
                        this.txtAge.Focus();
                        return -1;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("年龄录入格式不正确!" + e.Message, "提示");
                    this.txtAge.Focus();
                    return -1;
                }
            }
            #endregion

            this.regObj.Name = this.txtName.Text.Trim();//患者姓名 
            this.regObj.Sex.ID = this.cmbSex.Tag.ToString();//性别
            this.regObj.Birthday = this.dtBirthday.Value;//出生日期
            this.regObj.Pact.ID = this.cmbPayKind.Tag.ToString();

            FS.HISFC.Models.Base.PactInfo pact = conMgr.GetPactUnitInfoByPactCode(this.regObj.Pact.ID);
            if (pact == null || pact.ID == "")
            {
                MessageBox.Show("获取代码为:" + this.regObj.Pact.ID + "的合同单位信息出错!" + conMgr.Err, "提示");
                return -1;
            }

            this.regObj.Pact.Name = pact.Name;
            this.regObj.Pact.PayKind = pact.PayKind;
            this.regObj.SSN = this.txtMcardNo.Text.Trim();
            this.regObj.PhoneHome = this.txtPhone.Text.Trim();
            this.regObj.AddressHome = this.txtAddress.Text.Trim();
            //{6B6167F7-3A9B-4f6c-9326-C5CD6AA3AC98}
            this.regObj.IDCard = this.txtIdNO.Text;
            if (this.cmbCardType.Tag != null)
            {
                this.regObj.CardType.ID = this.cmbCardType.Tag.ToString();
            }
            else
            {
                this.regObj.CardType.ID = "";
            }
            if (this.chbEncrpt.Checked)
            {

                this.regObj.NormalName = FS.FrameWork.WinForms.Classes.Function.Encrypt3DES(this.regObj.Name);
                this.regObj.IsEncrypt = true;
                this.regObj.Name = "******";
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(regMgr.con);
            //SQLCA.BeginTransaction();

            string Err = "";
            try
            {
                this.regMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.patientMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                if (this.UpdatePatientinfo(regObj, this.patientMgr, this.regMgr, ref Err) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Err, "提示");
                    return -1;
                }

                FS.FrameWork.Management.PublicTrans.Commit();
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "提示");
                return -1;
            }

            MessageBox.Show("暂存成功!", "提示");
            this.Clear();

            return 0;
        }
        #endregion

        /// <summary>
        /// 接口初始化 {E43E0363-0B22-4d2a-A56A-455CFB7CF211}
        /// </summary>
        protected virtual int InitInterface()
        {
            this.iProcessRegiter = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(),
                typeof(FS.HISFC.BizProcess.Interface.Registration.IProcessRegiter)) as FS.HISFC.BizProcess.Interface.Registration.IProcessRegiter;

            if (ISearchCard == null && this.SetCardReadFalse)
            {
                ISearchCard = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Registration.ISearchCardNoFalse)) as FS.HISFC.BizProcess.Interface.Registration.ISearchCardNoFalse;
            }
            if (ICompletionAddress == null && this.IsCompletionAddress)
            {
                ICompletionAddress = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Registration.ICompletionAddress)) as FS.HISFC.BizProcess.Interface.Registration.ICompletionAddress;
            }
            if (ICountSpecialRegFee == null && this.IsCountSpecialRegFee)
            {
                ICountSpecialRegFee = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Registration.ICountSpecialRegFee)) as FS.HISFC.BizProcess.Interface.Registration.ICountSpecialRegFee;
            }
            return 1;
        }

        /// <summary>
        /// 找零窗口{F0661633-4754-4758-B683-CB0DC983922B}
        /// </summary>
        /// <returns></returns>
        protected virtual int ShowChangeForm(FS.HISFC.Models.Registration.Register regObj)
        {
            Forms.frmReturnCostNew frmOpen = new FS.HISFC.Components.Registration.Forms.frmReturnCostNew();
            frmOpen.RegObj = regObj;
            DialogResult r = frmOpen.ShowDialog();

            return 1;
        }

        #endregion

        #region 事件
        /// <summary>
        /// 快捷键
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Clear();
                return true;
            }

            if (keyData == Keys.F1)
            {
                this.SellMedicalRecords();
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }
        /// <summary>
        /// 设置当前行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader || this.fpList.RowCount == 0) return;
            this.fpList.ActiveRowIndex = e.Row;
        }

        private void cmbRegLevel_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            this.GetCost();
            this.WarningDoctLevel();
        }

        //{F3258E87-7BCC-411a-865E-A9843AD2C6DD}
        private void chbBookFee_CheckedChanged(object sender, EventArgs e)
        {
            //涮洗挂号费
            this.GetCost();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        //{6B6167F7-3A9B-4f6c-9326-C5CD6AA3AC98}身份证信息
        private void txtIdNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            string idNO = txtIdNO.Text.Trim();
            //01为身份证号
            if (!string.IsNullOrEmpty(cmbCardType.Tag.ToString()) && cmbCardType.Tag.ToString() == "01" && !string.IsNullOrEmpty(idNO))
            {
                int returnValue = this.ProcessIDENNO(idNO, EnumCheckIDNOType.BeforeSave);
                if (returnValue < 0)
                {
                    return;
                }

            }
            else
            {
                this.setNextControlFocus();
            }
        }

        private int ProcessIDENNO(string idNO, EnumCheckIDNOType enumType)
        {
            try
            {
                string errText = string.Empty;

                //校验身份证号


                //{99BDECD8-A6FC-44fc-9AAA-7F0B166BB752}

                //string idNOTmp = FS.FrameWork.WinForms.Classes.Function.TransIDFrom15To18(idNO);
                string idNOTmp = string.Empty;
                if (idNO.Length == 15)
                {
                    idNOTmp = FS.FrameWork.WinForms.Classes.Function.TransIDFrom15To18(idNO);
                }
                else
                {
                    idNOTmp = idNO;
                }

                //校验身份证号
                int returnValue = FS.FrameWork.WinForms.Classes.Function.CheckIDInfo(idNOTmp, ref errText);



                if (returnValue < 0)
                {
                    MessageBox.Show(errText);
                    this.txtIdNO.Focus();
                    return -1;
                }
                string[] reurnString = errText.Split(',');
                if (enumType == EnumCheckIDNOType.BeforeSave)
                {
                    string temp = reurnString[1];
                    this.dtBirthday.Value = DateTime.Parse(temp);
                    this.cmbSex.Text = reurnString[2];
                    this.setAge(this.dtBirthday.Value);
                    this.cmbPayKind.Focus();
                }
                else
                {
                    if (this.dtBirthday.Text != reurnString[1])
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("输入的生日日期与身份证号码中的生日不符"));
                        this.dtBirthday.Focus();
                        return -1;
                    }

                    if (this.cmbSex.Text != reurnString[2])
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("输入的性别与身份证中号的性别不符"));
                        this.cmbSex.Focus();
                        return -1;
                    }
                }
            }
            catch
            {
            }
            return 1;
        }

        ////{6B6167F7-3A9B-4f6c-9326-C5CD6AA3AC98}身份证信息
        private void dtBirthday_ValueChanged(object sender, EventArgs e)
        {
            //{AE0D67EA-32C9-46e2-8036-2EC797A13B94}
            this.setAge(this.dtBirthday.Value);

        }

        private void cmbRegLevel_Leave(object sender, EventArgs e)
        {
            this.GetCost();
        }

        /// <summary>
        /// 外接屏幕相关{67C90AAC-CFAD-4089-96F4-9F9FC82D8754}
        /// </summary>

        private void ucRegisterNew_Deactivate(object sender, EventArgs e)
        {
            this.iMultiScreen.CloseScreen();
        }

        private void ucRegisterNew_Activated(object sender, EventArgs e)
        {
            this.iMultiScreen.ShowScreen();
        }

        private void lbReceive_TextChanged(object sender, EventArgs e)
        {
            string cardNo = this.txtCardNo.Text.Trim().PadLeft(10, '0');
            if (cardNo.ToString() != "0000000000")
            {
                this.regObj = this.getRegInfo(cardNo);


                if (this.isShowMiltScreen)
                {
                    // 外屏显示{67C90AAC-CFAD-4089-96F4-9F9FC82D8754}
                    if (Screen.AllScreens.Length > 1)
                    {
                        //this.showPatientInfo = this.GetPatientInfomation();
                        System.Collections.Generic.List<Object> outScreen = new System.Collections.Generic.List<object>();
                        outScreen.Add(this.regObj);//患者信息
                        outScreen.Add(this.cmbRegLevel.Text);//挂号级别
                        outScreen.Add(this.cmbDept.Text);//挂号科室
                        outScreen.Add(this.cmbDoctor.Text);//挂号医生
                        outScreen.Add(this.lbReceive.Text);//应收费用
                        outScreen.Add("");//收费员
                        this.iMultiScreen.ListInfo = outScreen;
                        //
                    }
                }
            }
        }
        #endregion

        #region IInterfaceContainer 成员

        Type[] FS.FrameWork.WinForms.Forms.IInterfaceContainer.InterfaceTypes
        {
            get
            {
                Type[] type = new Type[2];
                type[0] = typeof(FS.HISFC.BizProcess.Interface.Registration.IRegPrint);
                //{E43E0363-0B22-4d2a-A56A-455CFB7CF211}
                type[1] = typeof(FS.HISFC.BizProcess.Interface.Registration.IProcessRegiter);

                return type;
            }
        }

        #endregion

        #region 菜单
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("扣限额", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.F分解, true, false, null);
            toolBarService.AddToolButton("改处方号", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X修改, true, false, null);
            toolBarService.AddToolButton("暂存", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Z暂存, true, false, null);
            toolBarService.AddToolButton("清屏", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);

            toolBarService.AddToolButton("补打", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C重打, true, false, null);

            toolBarService.AddToolButton("重打发票", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C重打, true, false, null);
            toolBarService.AddToolButton("补打发票", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);

            toolBarService.AddToolButton("生成卡号", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q权限, true, false, null);
            //{5BF35827-FF8E-4e23-A581-DFDE73EB95BE}
            toolBarService.AddToolButton("病历本", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X信息, true, false, null);
            toolBarService.AddToolButton("加密", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X修改, true, false, null);

            toolBarService.AddToolButton("购买病历本", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B病历本费, true, false, null);

            toolBarService.AddToolButton("购买卡工本费", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S收费, true, false, null);

            toolBarService.AddToolButton("发票纸打印", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            toolBarService.AddToolButton("刷卡", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B报警, true, false, null);
            toolBarService.AddToolButton("补打条码", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B病历添加, true, false, null);
            toolBarService.AddToolButton("身份证", "身份证", (int)FS.FrameWork.WinForms.Classes.EnumImageList.M模版, true, false, null);
            toolBarService.AddToolButton("发票累计", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.L累计开始, true, false, null);

            toolBarService.AddToolButton("读卡", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S手动录入, true, false, null);
            return toolBarService;
        }

        protected override int OnSave(object sender, object neuObject)
        {
            if (this.Save() == -1)
            {
                this.cmbRegLevel.Focus();
            }
            return base.OnSave(sender, neuObject);
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "扣限额":
                    if (this.Reduce() == -1)
                    {
                        this.cmbRegLevel.Focus();
                    }
                    break;
                case "改处方号":
                    this.ChangeRecipe();
                    break;
                case "暂存":
                    SavePatient();
                    break;
                case "清屏":
                    Clear();
                    break;
                case "重打发票":
                case "补打":
                    Reprint();
                    break;
                case "生成卡号":
                    this.AutoGetCardNO();
                    break;
                //{5BF35827-FF8E-4e23-A581-DFDE73EB95BE}
                case "病历本":
                    {
                        this.chbBookFee.Checked = !this.chbBookFee.Checked;
                        break;
                    }
                case "加密":
                    {
                        this.chbEncrpt.Checked = !this.chbEncrpt.Checked;
                        break;
                    }
                case "购买病历本":
                    {
                        SellMedicalRecords();
                        break;
                    }
                case "购买卡工本费":
                    {
                        this.SellMedicalCards();
                        break;
                    }
                case "补打发票":
                case "发票纸打印":
                    {
                        this.OnlyPrint();
                        break;
                    }
                case "刷卡":
                    {
                        string cardNo = "";
                        string error = "";
                        if (Function.OperCard(ref cardNo, ref error) == -1)
                        {
                            CommonController.Instance.MessageBox(error, MessageBoxIcon.Error);
                            return;
                        }

                        txtCardNo.Text = cardNo;
                        txtCardNo.Focus();
                        this.txtCardNo_KeyDown(null, new KeyEventArgs(Keys.Enter));

                        break;
                    }
                case "补打条码":
                    {
                        this.ReprintBarCode();
                        break;
                    }
                case "身份证":
                    this.ReadIDInfo();
                    break;
                case "发票累计":
                    Forms.frmPreCountInvos frmAccount = new Forms.frmPreCountInvos();
                    frmAccount.ShowDialog();
                    break;

                case "读卡":
                    if (this.regObj == null || this.regObj.Pact == null || this.regObj.Pact.ID.Length == 0)
                    {
                        if (this.cmbPayKind.Tag != null)
                        {
                            this.ReadCard(this.cmbPayKind.Tag.ToString());
                        }
                        else
                        {
                            MessageBox.Show("请先选择结算类别","提示");
                        }

                    }
                    else
                    {
                        this.ReadCard(this.regObj.Pact.ID);
                    }


                    //this.ReadMCardInfo();
                    break;
                default:
                    break;


            }

            base.ToolStrip_ItemClicked(sender, e);
        }
        #endregion

        #region 医保接口
        /// <summary>
        /// 通过toolBar的读卡方法接口
        /// </summary>
        /// <param name="pactCode">合同单位编码</param>
        /// <returns>成功 1 失败 －1</returns>
        public int ReadCard(string pactCode)
        {
            long returnValue = 0;
            FS.HISFC.BizProcess.Integrate.RADT radt = new FS.HISFC.BizProcess.Integrate.RADT();
            regObj = new FS.HISFC.Models.Registration.Register();

            //{04102034-382D-488e-BC45-F5B8CDBDE70D}
            regObj.Pact.ID = pactCode;

            returnValue = this.MedcareInterfaceProxy.SetPactCode(pactCode);
            if (returnValue != 1)
            {
                MessageBox.Show(this.MedcareInterfaceProxy.ErrMsg);
                // {DBCB798D-2F21-449e-BBE7-8F95E0F08B8A}
                if (this.MedcareInterfaceProxy.Rollback() < 0)
                {
                    MessageBox.Show(this.MedcareInterfaceProxy.ErrMsg);
                    return -1;
                }

                return -1;
            }

            returnValue = this.MedcareInterfaceProxy.Connect();
            if (returnValue != 1)
            {
                MessageBox.Show(this.MedcareInterfaceProxy.ErrMsg);
                // {DBCB798D-2F21-449e-BBE7-8F95E0F08B8A}
                if (this.MedcareInterfaceProxy.Rollback() < 0)
                {
                    MessageBox.Show(this.MedcareInterfaceProxy.ErrMsg);
                    return -1;
                }
                return -1;
            }

            returnValue = this.MedcareInterfaceProxy.GetRegInfoOutpatient(this.regObj);
            if (returnValue != 1)
            {
                // MessageBox.Show(this.MedcareInterfaceProxy.ErrMsg);
                // {DBCB798D-2F21-449e-BBE7-8F95E0F08B8A}
                if (this.MedcareInterfaceProxy.Rollback() < 0)
                {
                    MessageBox.Show(this.MedcareInterfaceProxy.ErrMsg);
                    return -1;
                }
                return -1;
            }

            //by han-zf 2014-07-11 读卡获取的信息返回给页面

            this.txtMcardNo.Text = regObj.SSN;
            this.txtIdNO.Text = regObj.IDCard;
            this.txtName.Text = regObj.Name;
            this.cmbSex.Tag = regObj.Sex.ID;
            this.cmbSex.Text = regObj.Sex.Name;
            this.txtPhone.Text = regObj.PhoneHome;

            if (regObj.Birthday > DateTime.MinValue)
            {
                this.dtBirthday.Value = regObj.Birthday;
            }


            returnValue = this.MedcareInterfaceProxy.Disconnect();
            if (returnValue != 1)
            {
                MessageBox.Show(this.MedcareInterfaceProxy.ErrMsg);
                // {DBCB798D-2F21-449e-BBE7-8F95E0F08B8A}
                if (this.MedcareInterfaceProxy.Rollback() < 0)
                {
                    MessageBox.Show(this.MedcareInterfaceProxy.ErrMsg);
                    return -1;
                }

                return -1;
            }

            FS.HISFC.Models.RADT.PatientInfo p = null;

            p = radt.QueryComPatientInfoByMcardNO(this.regObj.SSN);
            if (p != null)
            {
                this.regObj.PID.CardNO = p.PID.CardNO;

                int regCount = this.regMgr.QueryRegiterByCardNO(p.PID.CardNO);

                if (regCount == 1)
                {
                    this.regObj.IsFirst = false;
                }
                else
                {
                    if (regCount == 0)
                    {
                        this.regObj.IsFirst = true;
                    }
                }

                this.regObj.PhoneHome = p.PhoneHome;
                this.regObj.AddressHome = p.AddressHome;

            }
            this.regObj.User01 = "1";
            // this.regObj = myYBregObj;

            if (txtOrder.Tag != null) //预约病人处理
            {
                regObj.PID.CardNO = txtCardNo.Text;
                regObj.PhoneHome = txtPhone.Text;
            }

            this.SetSIPatientInfo();
            this.SetEnabled(false);
            //读社保卡时如果社保登记的身份证不是大陆的造成无法挂号
            if (this.txtIdNO.Text.Length < 15)
            {
                this.SetEnabled(true);
            }
            this.cmbPayKind.Enabled = true;
            this.isReadCard = true;


            this.cmbCardType.Enabled = true;

            //this.registerControl.SetRegInfo();         

            return 1;
        }
        /// <summary>
        /// 设置界面患者基本信息
        /// </summary>
        /// <returns>成功 1 失败 －1</returns>
        public int SetSIPatientInfo()
        {
            this.txtCardNo.Text = this.regObj.PID.CardNO;
            this.txtName.Text = this.regObj.Name;
            //医保读卡结束后，门诊卡号为空时，重新通过姓名检索病人信息表
            if (string.IsNullOrEmpty(this.txtCardNo.Text) && !string.IsNullOrEmpty(this.txtName.Text))
            {
                string CardNo = this.GetCardNoByName(this.txtName.Text.Trim());

                if (!string.IsNullOrEmpty(CardNo))
                {
                    this.regObj.PID.CardNO = CardNo;
                    this.txtCardNo.Text = CardNo;

                    int regCount = this.regMgr.QueryRegiterByCardNO(CardNo);

                    if (regCount == 1)
                    {
                        this.regObj.IsFirst = false;
                    }
                    else
                    {
                        if (regCount == 0)
                        {
                            this.regObj.IsFirst = true;
                        }
                    }
                }
            }
            this.cmbSex.Tag = this.regObj.Sex.ID;
            this.cmbPayKind.Tag = this.regObj.Pact.ID;
            this.txtMcardNo.Text = this.regObj.SSN;
            this.txtPhone.Text = this.regObj.PhoneHome;
            this.txtAddress.Text = this.regObj.AddressHome;
            this.txtIdNO.Text = this.regObj.IDCard;

            if (this.regObj.Birthday != DateTime.MinValue)
                this.dtBirthday.Value = this.regObj.Birthday;

            this.cmbCardType.Tag = this.regObj.CardType.ID;

            //{54603DD0-3484-4dba-B88A-B89F2F59EA40}
            if (this.isShowSIBalanceCost == true)
            {
                this.tbSIBalanceCost.Text = this.regObj.SIMainInfo.IndividualBalance.ToString();
            }

            this.setAge(this.regObj.Birthday);
            this.GetCost();
            return 1;
        }
        /// <summary>
        /// 是否可用
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public int SetEnabled(bool Value)
        {
            //this.txtCardNo.Enabled = Value;
            this.txtName.Enabled = Value;
            this.cmbSex.Enabled = Value;
            this.txtMcardNo.Enabled = Value;
            this.cmbPayKind.Enabled = Value;
            this.dtBirthday.Enabled = Value;
            this.txtAge.Enabled = Value;
            this.cmbUnit.Enabled = Value;
            this.txtIdNO.Enabled = Value;

            this.cmbCardType.Enabled = Value;
            this.cmbPatientType.Enabled = Value;
            return 1;
        }
        #endregion

        /// <summary>
        /// 判断身份证//{6B6167F7-3A9B-4f6c-9326-C5CD6AA3AC98}身份证信息
        /// </summary>
        private enum EnumCheckIDNOType
        {
            /// <summary>
            /// 保存之前校验
            /// </summary>
            BeforeSave = 0,

            /// <summary>
            /// 保存时校验
            /// </summary>
            Saveing
        }

        /// <summary>
        /// 限制条件
        /// </summary>
        public enum EnumLimit
        {
            /// <summary>
            /// 不限制
            /// </summary>
            None,
            /// <summary>
            /// 限制其中一个
            /// </summary>
            Half,
            /// <summary>
            /// 限制全部
            /// </summary>
            All
        }

        private void chbCardFee_CheckedChanged(object sender, EventArgs e)
        {
            this.GetCost();
        }

        /// <summary>
        /// 购买病历本费
        /// </summary>
        private void SellMedicalRecords()
        {
            if (this.regObj == null || string.IsNullOrEmpty(this.regObj.PID.CardNO))
            {
                MessageBox.Show("请刷卡输入患者信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("是否收取病历本费用?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                return;
            }

            AccountCardFee bookFee = null;
            decimal decOthFee = 1;

            int iRes = 0;

            if (cmbRegLevel.alItems != null && cmbRegLevel.alItems.Count > 0)
            {
                FS.HISFC.Models.Registration.RegLevel reglevel = cmbRegLevel.alItems[0] as FS.HISFC.Models.Registration.RegLevel;

                string strPact = this.cmbPayKind.Tag.ToString();
                decimal regFee = 0, chkFee = 0, digFee = 0, othFee = 0;

                iRes = GetRegFee(strPact, reglevel.ID, ref regFee, ref chkFee, ref digFee, ref othFee);
                if (iRes == -1)
                {
                    MessageBox.Show("获取挂号费出错!" + this.regFeeMgr.Err, "提示");
                    return;
                }

                decOthFee = othFee;
            }

            if (this.isShowMiltScreen)
            {
                // 外屏显示{67C90AAC-CFAD-4089-96F4-9F9FC82D8754}
                if (Screen.AllScreens.Length > 1)
                {
                    System.Collections.Generic.List<Object> outScreen = new System.Collections.Generic.List<object>();
                    outScreen.Add(this.regObj);//患者信息
                    outScreen.Add("");//挂号级别
                    outScreen.Add("");//挂号科室
                    outScreen.Add("");//挂号医生
                    outScreen.Add(decOthFee.ToString());//应收费用
                    outScreen.Add("");//收费员（非初始化界面值为空)
                    this.iMultiScreen.ListInfo = outScreen;
                }
                //
            }

            bookFee = new AccountCardFee();
            bookFee.FeeType = AccCardFeeType.CaseFee;
            bookFee.InvoiceNo = "";
            bookFee.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
            bookFee.Tot_cost = decOthFee;
            bookFee.Own_cost = bookFee.Tot_cost;
            bookFee.IStatus = 1;

            bookFee.Patient.PID.CardNO = this.regObj.PID.CardNO;
            bookFee.Patient.Name = regObj.Name;

            bookFee.ClinicNO = "";
            bookFee.Remark = "";

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            iRes = this.feeMgr.SaveAccountCardFee(ref bookFee);
            if (iRes == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("保存失败！" + this.feeMgr.Err, "提示");
                return;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("保存成功！", "提示");

            this.regObj.RegLvlFee.OthFee = decOthFee;
            this.regObj.OwnCost = decOthFee;
            this.regObj.LstCardFee.Add(bookFee);  //打发票使用
            this.regObj.InvoiceNO = bookFee.InvoiceNo;//发票电脑号

            this.regObj.DoctorInfo.SeeDate = DateTime.Now;
            
            this.regObj.InputOper.ID = this.regMgr.Operator.ID;
            this.regObj.InputOper.OperTime = this.regMgr.GetDateTimeFromSysDateTime();

            if (isPrintSellOnlyMedicalRecord)
            {
                if (this.isAutoPrint)
                {
                    this.Print(this.regObj, this.regMgr);
                }
                else
                {
                    DialogResult rs = MessageBox.Show(FS.FrameWork.Management.Language.Msg("请选择是否打印挂号票"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (rs == DialogResult.Yes)
                    {
                        this.Print(this.regObj, this.regMgr);
                    }
                }
            }

            this.Clear();
        }

        /// <summary>
        /// 收取卡工本费
        /// </summary>
        private void SellMedicalCards()
        {
            AccountCard accountCard = this.txtCardNo.Tag as AccountCard;

            if (this.regObj == null || string.IsNullOrEmpty(this.regObj.PID.CardNO) || accountCard == null)
            {
                MessageBox.Show("请刷卡输入患者信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (this.isShowMiltScreen)
            {
                // 外屏显示{67C90AAC-CFAD-4089-96F4-9F9FC82D8754}
                if (Screen.AllScreens.Length > 1)
                {
                    System.Collections.Generic.List<Object> outScreen = new System.Collections.Generic.List<object>();
                    outScreen.Add(this.regObj);//患者信息
                    outScreen.Add(this.cmbRegLevel.Text);//挂号级别
                    outScreen.Add(this.cmbDept.Text);//挂号科室
                    outScreen.Add(this.cmbDoctor.Text);//挂号医生
                    outScreen.Add("1");//应收费用
                    outScreen.Add("");//收费员（非初始化界面值为空）
                    this.iMultiScreen.ListInfo = outScreen;
                }
            }
            //

            if (MessageBox.Show("是否收取卡工本费用?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                return;
            }

            AccountCardFee cardFee = null;
            decimal decOthFee = 1;   //卡工本费用1元
            int iRes = 0;


            cardFee = new AccountCardFee();
            cardFee.FeeType = AccCardFeeType.CardFee;
            cardFee.InvoiceNo = "";
            cardFee.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
            cardFee.Tot_cost = decOthFee;
            cardFee.Own_cost = cardFee.Tot_cost;
            cardFee.IStatus = 1;

            cardFee.Patient.PID.CardNO = this.regObj.PID.CardNO;
            cardFee.Patient.Name = regObj.Name;

            cardFee.MarkNO = accountCard.MarkNO;
            cardFee.MarkType = accountCard.MarkType;

            cardFee.ClinicNO = "";
            cardFee.Remark = "";

            FS.HISFC.Models.Base.Const obj = cardFee.MarkType as FS.HISFC.Models.Base.Const;
            if (obj != null)
            {
                cardFee.Tot_cost = FS.FrameWork.Function.NConvert.ToDecimal(obj.UserCode);
            }
            cardFee.Own_cost = cardFee.Tot_cost;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            iRes = this.feeMgr.SaveAccountCardFee(ref cardFee);
            if (iRes == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("保存失败！" + this.feeMgr.Err, "提示");
                return;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("保存成功！", "提示");

            this.regObj.LstCardFee.Add(cardFee);  //打发票使用
            this.regObj.InvoiceNO = cardFee.InvoiceNo;//发票电脑号

            this.regObj.DoctorInfo.SeeDate = DateTime.Now;


            if (this.isAutoPrint)
            {
                this.Print(this.regObj, this.regMgr);
            }
            else
            {
                DialogResult rs = MessageBox.Show(FS.FrameWork.Management.Language.Msg("请选择是否打印挂号票"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (rs == DialogResult.Yes)
                {
                    this.Print(this.regObj, this.regMgr);
                }
            }

            this.Clear();
        }

        /// <summary>
        /// 发票号更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string invoiceNO = this.tbInvoiceNO.Text.Trim();
            string realInvoiceNO = this.tbRealInvoiceNO.Text.Trim();

            if (string.IsNullOrEmpty(invoiceNO))
            {
                MessageBox.Show("请录入有效的电脑号!");
                return;
            }

            if (string.IsNullOrEmpty(realInvoiceNO))
            {
                MessageBox.Show("请录入有效的印刷号!");
                return;
            }

            realInvoiceNO = realInvoiceNO.PadLeft(12, '0');
            FS.HISFC.Models.Base.Employee oper = this.regMgr.Operator as FS.HISFC.Models.Base.Employee;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            this.feeMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //更新实际发票号
            if (this.feeMgr.UpdateNextInvoiceNO(oper.ID, "R", invoiceNO, realInvoiceNO) <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("更新实际发票号失败!" + this.feeMgr.Err);
                return;
            }

            //更新发票电脑号
            //if (this.feeMgr.UpdateNextInvoliceNo(oper.ID, "INVOICE-R", invoiceNO) <= 0)
            //{
            //    FS.FrameWork.Management.PublicTrans.RollBack();
            //    MessageBox.Show("更新发票电脑号失败!" + this.feeMgr.Err);
            //    return;
            //}

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("更新成功!");
            InitInvoiceInfo();
        }

        #region 自动刷新

        /// <summary>
        /// 自动刷新开始时间
        /// </summary>
        private DateTime autoRefreshBeginTime = DateTime.Now;

        /// <summary>
        /// 终端设置的刷新间隔
        /// </summary>
        private int refreshInterval = 2;

        private System.Threading.Timer autoRefreshTimer = null;
        private System.Threading.TimerCallback autoRefreshCallBack = null;
        private delegate void autoRefreshHandler();
        private autoRefreshHandler autoRefreshEven;

        private void BeginAutoRefresh()
        {
            if (this.autoRefreshCallBack == null)
            {
                this.autoRefreshCallBack = new System.Threading.TimerCallback(this.AutoRefreshTimerCallback);
            }
            this.autoRefreshTimer = new System.Threading.Timer(this.autoRefreshCallBack, null, refreshInterval * 1000, this.refreshInterval * 1000);
        }

        /// <summary>
        /// 刷新处方列表
        /// </summary>
        /// <param name="param">参数（没有使用）</param>
        /// <returns></returns>
        private void AutoRefreshTimerCallback(object param)
        {
            if (this.autoRefreshEven == null)
            {
                autoRefreshEven = new autoRefreshHandler(this.AutoRefresh);
            }

            if (this.ParentForm != null)
            {
                this.ParentForm.BeginInvoke(this.autoRefreshEven);
            }

        }

        /// <summary>
        /// 自动刷新
        /// </summary>
        public void AutoRefresh()
        {
            bool isCloseRefresh = false;
            try
            {
                if (this.autoRefreshTimer != null)
                {
                    this.autoRefreshTimer.Dispose();
                }

                isCloseRefresh = this.ReadIDInfo();
            }
            finally
            {
                if (isCloseRefresh == false)
                {
                    this.BeginAutoRefresh();
                }
            }
        }

        /// <summary>
        /// 读取身份证信息
        /// </summary>
        public bool ReadIDInfo()
        {
            bool isCloseRefresh = false;
            if (InterfaceManager.GetIReadIDCard() != null)
            {
                string code = "", name = "", sex = "", nation = "", agent = "", add = "", message = "";
                DateTime birth = DateTime.MinValue, bigen = DateTime.MinValue, end = DateTime.MinValue;
                string photoFileName = "";
                int rtn = InterfaceManager.GetIReadIDCard().GetIDInfo(ref code, ref name, ref sex, ref birth, ref nation, ref add, ref agent, ref bigen, ref end, ref photoFileName, ref message);
                if (rtn == -1)
                {
                    CommonController.Instance.MessageBox(this, "读卡失败，" + message, MessageBoxIcon.Asterisk);
                    isCloseRefresh = true;
                }
                else if (rtn == 0)
                {
                    CommonController.Instance.MessageBox(this, "读卡失败，" + message, MessageBoxIcon.Asterisk);
                    isCloseRefresh = false;
                }
                else
                {
                    this.Clear();
                    ArrayList alPatient = patientMgr.QueryComPatientInfoListByIDNO(code);
                    if (alPatient == null || alPatient.Count == 0)
                    {
                        this.txtName.Text = name;
                        this.txtIdNO.Text = code;
                        this.cmbSex.Text = sex;
                        this.txtAddress.Text = add;
                        this.ProcessIDENNO(code, EnumCheckIDNOType.BeforeSave);
                        this.txtName_KeyDown(null, new KeyEventArgs(Keys.Enter));
                    }
                    else
                    {
                        //病历号
                        string cardNo = "";
                        for (int i = 0; i < alPatient.Count; i++)
                        {
                            FS.HISFC.Models.RADT.PatientInfo patientinfo = alPatient[i] as FS.HISFC.Models.RADT.PatientInfo;
                            if (patientinfo.Name == name)
                            {
                                cardNo = patientinfo.PID.CardNO;
                                break;
                            }
                        }
                        if (string.IsNullOrEmpty(cardNo) == false)
                        {
                            txtCardNo.Text = cardNo;
                            txtCardNo_KeyDown(null, new KeyEventArgs(Keys.Enter));
                        }
                        else
                        {
                            this.txtName.Text = name;
                            this.txtIdNO.Text = code;
                            this.cmbSex.Text = sex;
                            this.txtAddress.Text = add;
                            this.ProcessIDENNO(code, EnumCheckIDNOType.BeforeSave);
                            this.txtName_KeyDown(null, new KeyEventArgs(Keys.Enter));
                        }
                    }
                }
            }
            else
            {
                isCloseRefresh = true;
            }

            return isCloseRefresh;
        }

        /// <summary>
        /// 读取社保卡信息
        /// </summary>
        /// <returns></returns>
        public int ReadMCardInfo()
        {
            if (InterfaceManager.GetIReadMCard() != null)
            {
                string cardId = string.Empty;
                string cardNo = string.Empty;
                DateTime cardIssueDate = new DateTime();
                string userId = string.Empty;
                string userName = string.Empty;
                string userSex = string.Empty;
                string userPhoneNumber = string.Empty;
                string errInfo = string.Empty;
                DateTime dtBirth = new DateTime();

                int rtn = InterfaceManager.GetIReadMCard().GetMCardInfo(ref cardId, ref cardNo, ref cardIssueDate, ref userId,
                    ref userName, ref userSex, ref userPhoneNumber, ref dtBirth, ref errInfo);

                if (rtn == 1)
                {
                    //TODO:界面赋值
                    this.txtMcardNo.Text = cardNo;
                    this.txtIdNO.Text = userId;
                    this.txtName.Text = userName;
                    this.cmbSex.Tag = (userSex == "1") ? "M" : "F";
                    this.cmbSex.Text = (userSex == "1") ? "男" : "女";
                    this.txtPhone.Text = userPhoneNumber;
                    this.dtBirthday.Value = dtBirth;
                }
                else
                {
                    MessageBox.Show(errInfo, "提示");
                }

                return rtn;
            }
            else
            {
                MessageBox.Show("没有维护医保卡读卡接口", "提示");
                return -1;
            }
        }

        #endregion

        private void ucRegisterNew_Enter(object sender, EventArgs e)
        {
            if (this.IsReaderIDCard)
            {
                this.BeginAutoRefresh();
            }
        }

        private void ucRegisterNew_Leave(object sender, EventArgs e)
        {
            if (this.autoRefreshTimer != null)
            {
                this.autoRefreshTimer.Dispose();
            }
        }

        private void chbCheckFee_CheckedChanged(object sender, EventArgs e)
        {
            setIFeeDiagReg();
            this.GetCost();
        }

        private void chbNoRegFee_CheckedChanged(object sender, EventArgs e)
        {
            setIFeeDiagReg();
            this.GetCost();
        }
        private void setIFeeDiagReg()
        {
            if (this.chbNoCheckFee.Checked == true && this.chbNoRegFee.Checked == true && this.chbNoCheckFee.Visible == true && this.chbNoRegFee.Visible == true)
                this.iFeeDiagReg = 3;
            else if (this.chbNoCheckFee.Checked == false && this.chbNoRegFee.Checked == true && this.chbNoRegFee.Visible == true)
                this.iFeeDiagReg = 2;
            else if (this.chbNoCheckFee.Checked == true && this.chbNoCheckFee.Visible == true && this.chbNoRegFee.Checked == false)
                this.iFeeDiagReg = 1;
            else if (this.chbNoCheckFee.Checked == false && this.chbNoRegFee.Checked == false)
                this.iFeeDiagReg = 0;
        }

        #region 挂号费支付方式处理

        private int DualAccountCardFee(ref List<AccountCardFee> lstAccFee)
        {
            FS.HISFC.Components.Registration.Forms.frmAccountCardFeePayTypeInput frmPayType = new FS.HISFC.Components.Registration.Forms.frmAccountCardFeePayTypeInput();
            frmPayType.AccountCardFeeList = lstAccFee;
            DialogResult dr = frmPayType.ShowDialog();
            if (dr == DialogResult.OK)
            {
                lstAccFee = frmPayType.AccountCardFeeList;
                return 1;
            }
            else
            {
                return -1;
            }
        }

        #endregion
    }
}
