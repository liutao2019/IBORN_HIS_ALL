using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using FS.HISFC.Models.Account;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using FarPoint.Win.Spread;

namespace FS.HISFC.Components.Registration.NewRegister
{
    public partial class ucRegisterbak : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {

        #region 控制参数

        /// <summary>
        /// 默认显示的合同单位代码
        /// </summary>
        private string DefaultPactID = "";
        /// <summary>
        /// 挂号是否允许超出排班限额
        /// </summary>
        private bool IsAllowOverrun = true;
        /// <summary>
        /// 挂号级别显示列数
        /// </summary>
        private int DisplayRegLvlColumnCnt = 1;
        /// <summary>
        /// 结算类别显示列数
        /// </summary>
        private int DisplayPayKindColumnCnt = 1;
        /// <summary>
        /// 挂号科室显示列数
        /// </summary>
        private int DisplayDeptColumnCnt = 1;
        /// <summary>
        /// 挂号医生显示列数
        /// </summary>
        private int DisplayDoctColumnCnt = 1;        
        /// <summary>
        /// 1由操作员自己录入处方号、2处方号对操作员连续
        /// </summary>
        private int GetRecipeType = 1;        
        /// <summary>
        /// 保存时是否提示
        /// </summary>
        private bool IsPrompt = true;       
        /// <summary>
        /// “其它费”类型0：空调费1病历本费2其他费
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
        /// <summary>
        /// 诊金是否记帐
        /// </summary>
        private bool IsPubDiagFee = false; 
        /// <summary>
        /// 挂号界面默认的中文输入法
        /// </summary>
        private InputLanguage CHInput = null;
        /// <summary>
        /// 是否预约号序号排在现场号前面
        /// </summary>
        private bool IsPreFirst = false;

        private bool isAddAllDept = false;
        /// <summary>
        /// 挂号医生是否随着科室变化
        /// </summary>
        [Category("控件设置"), Description("是否添加全院科室，True:添加,False:只添加挂号科室"), DefaultValue(false)]
        public bool IsAddAllDept
        {
            get { return isAddAllDept; }
            set { isAddAllDept = value; }
        }

        private bool isAddAllDoct = false;
        /// <summary>
        /// 挂号医生是否随着科室变化
        /// </summary>
        [Category("控件设置"), Description("是否添加全院医生，True:添加全院医生，选择科室时医生列表不跟着变化,False:变化"), DefaultValue(true)]
        public bool IsAddAllDoct
        {
            get { return isAddAllDoct; }
            set { isAddAllDoct = value; }
        }

        private bool isPrintIfZero = true;
        /// <summary>
        /// 如果金额为0是否打印发票
        /// </summary>
        [Category("控件设置"), Description("金额为0的时候是否打印发票:true打印,false不打印"), DefaultValue(true)]
        public bool IsPrintIfZero
        {
            get { return this.isPrintIfZero; }
            set { this.isPrintIfZero = value; }
        }

        private bool isFilterSchemeByNoon = false;
        /// <summary>
        /// 是否根据午别来过滤专科排班和专家排班
        /// </summary>
        [Category("控件设置"), Description("是否根据午别来过滤专科排班和专家排班，默认(false)"), DefaultValue(false)]
        public bool IsFilterSchemeByNoon
        {
            get { return isFilterSchemeByNoon; }
            set { isFilterSchemeByNoon = value; }
        }

        private bool isShowMiltScreen = true;
        /// <summary>
        /// 是否显示外屏
        /// </summary>
        [Category("控件设置"), Description("是否外屏显示"), DefaultValue(true)]
        public bool IsShowMiltScreen
        {
            get { return isShowMiltScreen; }
            set { isShowMiltScreen = value; }
        }

        private bool isSetDefaultRegLev = false;
        /// <summary>
        /// 是否收费后设置默认挂号级别
        /// </summary>
        [Category("控件设置"), Description("是否收费后设置默认挂号级别")]
        public bool IsSetDefaultRegLev
        {
            set { this.isSetDefaultRegLev = value; }
            get { return this.isSetDefaultRegLev; }
        }

        private bool isCountSpecialRegFee = false;
        /// <summary>
        /// 是否启用挂号费特殊处理
        /// </summary>
        [Category("控制设置"), Description("是否启用挂号费特殊处理")]
        public bool IsCountSpecialRegFee
        {
            set{ this.isCountSpecialRegFee = value; }
            get{ return this.isCountSpecialRegFee; }
        }

        private int iFeeDiagReg = 0;
        /// <summary>
        /// 挂号时控制挂号费、诊金是否收取
        /// </summary>
        [Category("控件设置"), Description("挂号时控制挂号费、诊金是否收取: 0=都收,1=收取挂号费,2=收取诊金,3=都不收取")]
        public int IFeeDiagReg
        {
            set{ this.iFeeDiagReg = value; }
            get{ return this.iFeeDiagReg; }
        }

        private bool isSelectDeptFirst = false;
        /// <summary>
        /// 专家号是否先选择科室
        /// </summary>
        [Category("控件设置"), Description("专家号是否先选择科室")]
        public bool IsSelectDeptFirst
        {
            get{ return isSelectDeptFirst; }
            set{ isSelectDeptFirst = value;}
        }

        private bool isJudgeReglevl = true;
        /// <summary>
        /// 是否根据挂号级别加载科室列表
        /// </summary>
        [Category("控件设置"), Description("选挂号级别时，是否根据挂号级别加载科室列表"), DefaultValue(true)]
        public bool IsJudgeReglevl
        {
            get{ return this.isJudgeReglevl; }
            set{ this.isJudgeReglevl = value;}
        }

        private bool isFilterDoc = true;
        /// <summary>
        /// 是否根据挂号级别过滤医生
        /// </summary>
        [Category("控件设置"), Description("是否根据挂号级别过滤医生")]
        public bool IsFilterDoc
        {
            set{ this.isFilterDoc = value; }
            get{ return this.isFilterDoc; }
        }

        private int roundControl = 2; 
        /// <summary>
        /// 小数保留函数设置
        /// </summary>
        [Category("控件设置"), Description("小数保留函数设置:0-保留整数，1-保留一位小数,2-保留两位小数,3-下取整,4-上取整")]
        public int RoundControl
        {
            set { this.roundControl = value; }
            get { return this.roundControl; }
        }

        private bool isATMPrint = false;
        /// <summary>
        /// 是否ATM补打发票
        /// </summary>
        [Category("控件设置"), Description("是否ATM补打发票")]
        public bool IsATMPrint
        {
            set{ this.isATMPrint = value; }
            get{ return this.isATMPrint; }
        }

        private bool isAutoPrint = true;
        /// <summary>
        /// 是否自动打印
        /// </summary>
        [Category("控件设置"), Description("保存后是否自动打印挂号单"), DefaultValue(true)]
        public bool IsAutoPrint
        {
            get{ return this.isAutoPrint; }
            set{ this.isAutoPrint = value;}
        }

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

        //{2E41B9BF-6B67-4b56-BD54-A836CE09F52B}
        private bool isJudgePackageFee = true;
        /// <summary>
        /// 是否判断套餐内是否有挂号费
        /// </summary>
        [Category("控件设置"), Description("是否判断套餐内是否有挂号费"), DefaultValue(false)]
        public bool IsJudgePackageFee
        {
            get { return isJudgePackageFee; }
            set { isJudgePackageFee = value; }
        }

        //{D3268012-7646-4c69-943C-CF8487AB7997}
        private bool isDocterChargeFee = false;
        /// <summary>
        /// 是否判断套餐内是否有挂号费
        /// </summary>
        [Category("控件设置"), Description("是否由医生站收取费用"), DefaultValue(false)]
        public bool IsDocterChargeFee
        {
            get { return isDocterChargeFee; }
            set { isDocterChargeFee = value; }
        }

        #endregion

        #region 管理类

        /// <summary>
        /// 参数控制类
        /// </summary>
        private FS.FrameWork.Management.ControlParam ctlMgr = new FS.FrameWork.Management.ControlParam();
        /// <summary>
        /// 常数管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager conMgr = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// //{2E41B9BF-6B67-4b56-BD54-A836CE09F52B}
        /// 套餐管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.MedicalPackage.Fee.Package feePackageMgr = new FS.HISFC.BizProcess.Integrate.MedicalPackage.Fee.Package();
        /// <summary>
        /// 挂号级别管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.RegLevel RegLevelMgr = new FS.HISFC.BizLogic.Registration.RegLevel();
        /// <summary>
        /// 挂号员权限类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Permission permissMgr = new FS.HISFC.BizLogic.Registration.Permission();
        /// <summary>
        /// 挂号管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();
        /// <summary>
        /// 排班管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Schema SchemaMgr = new FS.HISFC.BizLogic.Registration.Schema();
        /// <summary>
        /// 合同单位管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeMgr = new FS.HISFC.BizProcess.Integrate.Fee();
        /// <summary>
        /// 午别管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Noon noonMgr = new FS.HISFC.BizLogic.Registration.Noon();
        /// <summary>
        /// 账户管理类
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();
        /// <summary>
        /// 患者信息业务类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.RADT radtProcess = new FS.HISFC.BizProcess.Integrate.RADT();
        /// <summary>
        /// 控制参数管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParams = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        /// <summary>
        /// 挂号费管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.RegLvlFee regFeeMgr = new FS.HISFC.BizLogic.Registration.RegLvlFee();
        /// <summary>
        /// 账户管理
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Account.AccountPay accountPay = new FS.HISFC.BizProcess.Integrate.Account.AccountPay();

        #endregion

        #region 属性
        /// <summary>
        /// 挂号实体
        /// </summary>
        private FS.HISFC.Models.Registration.Register regObj;
        /// <summary>
        /// 教授列表
        /// </summary>
        private ArrayList alProfessor = new ArrayList();
        /// <summary>
        /// 公用数据集
        /// </summary>
        private DataSet dsItems;
        /// <summary>
        /// 科室列表数据集
        /// </summary>
        private DataView dvDepts;
        /// <summary>
        /// 医生数据集
        /// </summary>
        private DataView dvDocts;        
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
        /// 排版选择器
        /// </summary>
        private ucChooseBookingDate ucChooseDate;        
        /// <summary>
        /// 外屏接口
        /// </summary>
        protected FS.HISFC.BizProcess.Interface.FeeInterface.IMultiScreen iMultiScreen = null;
        /// <summary>
        /// 挂号费特殊计算接口
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Registration.ICountSpecialRegFee ICountSpecialRegFee = null;
        /// <summary>
        /// 支付方式列表
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper helpPayMode = new FS.FrameWork.Public.ObjectHelper();
        /// <summary>
        /// 存储优惠和支付类型的哈希表
        /// </summary>
        private Hashtable hsPayCost = new Hashtable();
        /// <summary>
        /// 费用详情
        /// </summary>
        private ArrayList RegFeeList = new ArrayList();
        /// <summary>
        /// 保存时处理
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Registration.IProcessRegiter iProcessRegiter = null;
        /// <summary>
        /// 会员账户支付
        /// </summary>
        private ArrayList accountPayList = new ArrayList();
        /// <summary>
        /// 会员赠送支付
        /// </summary>
        private ArrayList giftPayList = new ArrayList();
        /// <summary>
        /// 代付会员支付
        /// </summary>
        private ArrayList empowerAccountPayList = new ArrayList();
        /// <summary>
        /// 代付赠送支付
        /// </summary>
        private ArrayList empowerGiftPayList = new ArrayList();
        #endregion

        /// <summary>
        /// 门诊挂号
        /// </summary>
        public ucRegisterbak()
        {
            InitializeComponent();
            InitControls();
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            this.GetParameter();
            this.InitDataSet();
            this.SetStyles();
            this.InitRegLevel();
            this.InitRegDept();
            this.InitDoct();
            this.InitPact();
            this.InitChooseUC();
            this.InitOtherCtls();
            this.SetRegList();
            this.GetRecipeNo(regMgr.Operator.ID);
            this.InitInterface();
            this.ChangeInvoiceNO();
            this.Load += new EventHandler(ucRegister_Load);
        }

        /// <summary>
        /// 添加事件
        /// </summary>
        private void AddEvents()
        {
            this.FindForm().Activated += new EventHandler(ucRegister_Activated);
            this.FindForm().Deactivate += new EventHandler(ucRegister_Deactivate);
            this.FindForm().FormClosing += new FormClosingEventHandler(ucRegister_FormClosing);

            this.btnAccount.Click += new EventHandler(btnAccount_Click);
            this.btnEmpower.Click += new EventHandler(btnEmpower_Click);

            this.tbQueryNO.KeyDown += new KeyEventHandler(tbQueryNO_KeyDown);
            this.cmbCardType.SelectedIndexChanged += new EventHandler(cmbCardType_SelectedIndexChanged);
            this.tbIDNO.Leave += new EventHandler(tbIDNO_Leave);
            this.tbAge.TextChanged += new EventHandler(tbAge_TextChanged);
            this.cmbUnit.SelectedIndexChanged += new EventHandler(cmbUnit_SelectedIndexChanged);
            this.dtpBirthday.ValueChanged += new EventHandler(dtpBirthday_ValueChanged);
            this.cmbCardLevel.SelectedIndexChanged += new EventHandler(cmbCardLevel_SelectedIndexChanged);
            this.cmbPatientType.SelectedIndexChanged += new EventHandler(cmbPatientType_SelectedIndexChanged);
            this.cmbPatientType.Enter += new EventHandler(cmbPatientType_Enter);
            //this.cmbPatientType.Leave += new EventHandler(cmbPatientType_Leave);
            this.cmbRegLevel.SelectedIndexChanged += new EventHandler(cmbRegLevel_SelectedIndexChanged);
            this.cmbRegLevel.Enter += new EventHandler(cmbRegLevel_Enter);
            //this.cmbRegLevel.Leave += new EventHandler(cmbRegLevel_Leave);
            this.cmbDept.SelectedIndexChanged += new EventHandler(cmbDept_SelectedIndexChanged);
            this.cmbDept.Enter += new EventHandler(cmbDept_Enter);
            //this.cmbDept.Leave += new EventHandler(cmbDept_Leave);
            this.cmbDoctor.SelectedIndexChanged += new EventHandler(cmbDoctor_SelectedIndexChanged);
            this.cmbDoctor.Enter += new EventHandler(cmbDoctor_Enter);
            //this.cmbDoctor.Leave += new EventHandler(cmbDoctor_Leave);
            this.dtSeeDate.ValueChanged += new EventHandler(dtSeeDate_ValueChanged);
            this.dtBegin.ValueChanged += new EventHandler(dtBegin_ValueChanged);
            this.dtEnd.ValueChanged += new EventHandler(dtEnd_ValueChanged);
            this.lnkSchema.Click += new EventHandler(lnkSchema_Click);
            this.tbRealInvoiceNO.KeyDown += new KeyEventHandler(tbRealInvoiceNO_KeyDown);
            this.btnUpdate.Click += new EventHandler(btnUpdate_Click);
            //this.lnkPower.Click += new EventHandler(lnkPower_Click);
            //this.lnkPowerCancel.Click += new EventHandler(lnkPowerCancel_Click);
            this.tbRealCost.TextChanged += new EventHandler(tbRealCost_TextChanged);
            this.ucChooseDate.SelectedItem += new ucChooseBookingDate.dSelectedItem(ucChooseDate_SelectedItem);
            this.chbCardFee.CheckedChanged += new EventHandler(chbCardFee_CheckedChanged);

            //{0A94EF4E-FD1D-43be-A223-BC59B8433BF6}
            this.tbName.KeyDown += new KeyEventHandler(tbName_KeyDown);
            this.tbPhone.KeyDown += new KeyEventHandler(tbPhone_KeyDown);

            this.fpPayMode.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(fpPayMode_CellClick);
            this.fpPayMode.Leave += new EventHandler(fpPayMode_Leave);
            this.fpPayMode.EditModeOn += new EventHandler(fpPayMode_EditModeOn);
            this.fpPayMode.Change += new FarPoint.Win.Spread.ChangeEventHandler(fpPayMode_Change);
        }

        /// <summary>
        /// 删除事件
        /// </summary>
        private void DelEvents()
        {
            this.fpPayMode.Change -= new FarPoint.Win.Spread.ChangeEventHandler(fpPayMode_Change);
            this.fpPayMode.EditModeOn -= new EventHandler(fpPayMode_EditModeOn);
            this.fpPayMode.Leave -= new EventHandler(fpPayMode_Leave);
            this.fpPayMode.CellClick -= new FarPoint.Win.Spread.CellClickEventHandler(fpPayMode_CellClick);

            this.chbCardFee.CheckedChanged -= new EventHandler(chbCardFee_CheckedChanged);
            this.ucChooseDate.SelectedItem -= new ucChooseBookingDate.dSelectedItem(ucChooseDate_SelectedItem);
            this.tbRealCost.TextChanged -= new EventHandler(tbRealCost_TextChanged);
            //this.lnkPowerCancel.Click -= new EventHandler(lnkPowerCancel_Click);
            //this.lnkPower.Click -= new EventHandler(lnkPower_Click);
            this.btnUpdate.Click -= new EventHandler(btnUpdate_Click);
            this.tbRealInvoiceNO.KeyDown -= new KeyEventHandler(tbRealInvoiceNO_KeyDown);
            this.lnkSchema.Click -= new EventHandler(lnkSchema_Click);
            this.dtEnd.ValueChanged -= new EventHandler(dtEnd_ValueChanged);
            this.dtBegin.ValueChanged -= new EventHandler(dtBegin_ValueChanged);
            this.dtSeeDate.ValueChanged -= new EventHandler(dtSeeDate_ValueChanged);
            //this.cmbDoctor.Leave -= new EventHandler(cmbDoctor_Leave);
            this.cmbDoctor.Enter -= new EventHandler(cmbDoctor_Enter);
            this.cmbDoctor.SelectedIndexChanged -= new EventHandler(cmbDoctor_SelectedIndexChanged);
            //this.cmbDept.Leave -= new EventHandler(cmbDept_Leave);
            this.cmbDept.Enter -= new EventHandler(cmbDept_Enter);
            this.cmbDept.SelectedIndexChanged -= new EventHandler(cmbDept_SelectedIndexChanged);
            //this.cmbRegLevel.Leave -= new EventHandler(cmbRegLevel_Leave);
            this.cmbRegLevel.Enter -= new EventHandler(cmbRegLevel_Enter);
            this.cmbRegLevel.SelectedIndexChanged -= new EventHandler(cmbRegLevel_SelectedIndexChanged); 
            //this.cmbPatientType.Leave -= new EventHandler(cmbPatientType_Leave);
            this.cmbPatientType.Enter -= new EventHandler(cmbPatientType_Enter);
            this.cmbPatientType.SelectedIndexChanged -= new EventHandler(cmbPatientType_SelectedIndexChanged);
            this.cmbCardLevel.SelectedIndexChanged -= new EventHandler(cmbCardLevel_SelectedIndexChanged);
            this.dtpBirthday.ValueChanged -= new EventHandler(dtpBirthday_ValueChanged);
            this.cmbUnit.SelectedIndexChanged -= new EventHandler(cmbUnit_SelectedIndexChanged);
            this.tbAge.TextChanged -= new EventHandler(tbAge_TextChanged);
            this.tbIDNO.Leave -= new EventHandler(tbIDNO_Leave);
            this.cmbCardType.SelectedIndexChanged -= new EventHandler(cmbCardType_SelectedIndexChanged);
            this.tbQueryNO.KeyDown -= new KeyEventHandler(tbQueryNO_KeyDown);

            //{0A94EF4E-FD1D-43be-A223-BC59B8433BF6}
            this.tbName.KeyDown -= new KeyEventHandler(tbName_KeyDown);
            this.tbPhone.KeyDown -= new KeyEventHandler(tbPhone_KeyDown);

            this.btnEmpower.Click -= new EventHandler(btnEmpower_Click);
            this.btnAccount.Click -= new EventHandler(btnAccount_Click);

            this.FindForm().FormClosing -= new FormClosingEventHandler(ucRegister_FormClosing);
            this.FindForm().Deactivate -= new EventHandler(ucRegister_Deactivate);
            this.FindForm().Activated -= new EventHandler(ucRegister_Activated);
        }

        #region InitControls

        /// <summary>
        /// 获取设置参数
        /// </summary>
        private void GetParameter()
        {
            string rtn = string.Empty;
            //挂号级别显示列数
            rtn = this.ctlMgr.QueryControlerInfo("400001");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "1";
            this.DisplayRegLvlColumnCnt = int.Parse(rtn);
            //挂号科室显示列数
            rtn = this.ctlMgr.QueryControlerInfo("400002");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "1";
            this.DisplayDeptColumnCnt = int.Parse(rtn);
            //结算类别显示列数
            rtn = this.ctlMgr.QueryControlerInfo("400003");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "1";
            this.DisplayPayKindColumnCnt = int.Parse(rtn);
            //挂号医生显示列数
            rtn = this.ctlMgr.QueryControlerInfo("400004");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "1";
            this.DisplayDoctColumnCnt = int.Parse(rtn);
            //默认显示合同单位
            this.DefaultPactID = this.ctlMgr.QueryControlerInfo("400005");
            //诊金是否报销
            rtn = this.ctlMgr.QueryControlerInfo("400008");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "0";
            this.IsPubDiagFee = FS.FrameWork.Function.NConvert.ToBoolean(rtn);
            //挂号是否允许超出排班限额
            rtn = this.ctlMgr.QueryControlerInfo("400015");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "1";
            this.IsAllowOverrun = FS.FrameWork.Function.NConvert.ToBoolean(rtn);         
            //获取处方号类型（1物理票号,2电脑票号－－挂号收据号,3电脑票号－－门诊收据号）
            rtn = this.ctlMgr.QueryControlerInfo("400019");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "1";
            this.GetRecipeType = int.Parse(rtn);            
            //保存时是否提示
            rtn = this.ctlMgr.QueryControlerInfo("400024");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "1";
            this.IsPrompt = FS.FrameWork.Function.NConvert.ToBoolean(rtn);
            ///是否预约号看诊序号排在现场号前面别
            rtn = this.ctlMgr.QueryControlerInfo("400026");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "0";
            this.IsPreFirst = FS.FrameWork.Function.NConvert.ToBoolean(rtn);
            //其它费类型0：空调费1病历本费2：其他费
            rtn = this.ctlMgr.QueryControlerInfo("400027");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "1";
            this.otherFeeType = rtn;
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
        /// 初始化数据集
        /// </summary>
        private void InitDataSet()
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
        /// 设置显示风格
        /// </summary>
        private void SetStyles()
        {
            FarPoint.Win.Spread.CellType.TextCellType txtCell = new FarPoint.Win.Spread.CellType.TextCellType();
            
            #region 挂号级别

            this.fpRegLvlList.ColumnCount = int.Parse(this.DisplayRegLvlColumnCnt.ToString()) * 2;
            int width = 700 * 2 / this.fpRegLvlList.ColumnCount;
            for (int i = 0; i < this.fpRegLvlList.ColumnCount; i++)
            {
                if (i % 2 == 0)
                {
                    this.fpRegLvlList.ColumnHeader.Cells[0, i].Text = "代码";
                    this.fpRegLvlList.Columns[i].Width = width / 3;
                    this.fpRegLvlList.Columns[i].BackColor = Color.Linen;
                    this.fpRegLvlList.Columns[i].CellType = txtCell;
                }
                else
                {
                    this.fpRegLvlList.ColumnHeader.Cells[0, i].Text = "挂号级别名称";
                    this.fpRegLvlList.Columns[i].Width = width * 2 / 3;
                }
            }
            this.fpRegLvlList.GrayAreaBackColor = System.Drawing.SystemColors.Window;
            this.fpRegLvlList.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpRegLvlList.RowHeader.Visible = false;
            this.fpRegLvlList.RowCount = 0;

            #endregion

            #region 结算类别

            this.fpPayKindList.ColumnCount = int.Parse(this.DisplayPayKindColumnCnt.ToString()) * 2;
            width = 700 * 2 / this.fpPayKindList.ColumnCount;
            FarPoint.Win.Spread.CellType.TextCellType txtType = new FarPoint.Win.Spread.CellType.TextCellType();
            txtType.StringTrim = System.Drawing.StringTrimming.EllipsisCharacter;
            for (int i = 0; i < this.fpPayKindList.ColumnCount; i++)
            {
                if (i % 2 == 0)
                {
                    this.fpPayKindList.ColumnHeader.Cells[0, i].Text = "代码";
                    this.fpPayKindList.Columns[i].Width = width / 3;
                    this.fpPayKindList.Columns[i].BackColor = Color.Linen;
                    this.fpPayKindList.Columns[i].CellType = txtCell;
                }
                else
                {
                    this.fpPayKindList.ColumnHeader.Cells[0, i].Text = "类别名称";
                    this.fpPayKindList.Columns[i].Width = width * 2 / 3;
                    this.fpPayKindList.Columns[i].CellType = txtType;
                }
            }
            this.fpPayKindList.GrayAreaBackColor = SystemColors.Window;
            this.fpPayKindList.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpPayKindList.RowHeader.Visible = false;
            this.fpPayKindList.RowCount = 0;

            #endregion

            #region 患者挂号信息

            this.fpRegList.ColumnHeader.Cells[0, 0].Text = "就诊卡号";
            this.fpRegList.Columns[0].Width = 100F;
            this.fpRegList.Columns[0].AllowAutoSort = true;
            this.fpRegList.Columns[0].CellType = txtCell;
            this.fpRegList.ColumnHeader.Cells[0, 1].Text = "姓名";
            this.fpRegList.Columns[1].Width = 90F;
            this.fpRegList.Columns[1].AllowAutoSort = true;
            this.fpRegList.ColumnHeader.Cells[0, 2].Text = "结算类别";
            this.fpRegList.Columns[2].Width = 90F;
            this.fpRegList.Columns[2].AllowAutoSort = true;
            this.fpRegList.ColumnHeader.Cells[0, 3].Text = "出生年月";
            this.fpRegList.Columns[3].Width = 100F;
            this.fpRegList.Columns[3].AllowAutoSort = true;
            this.fpRegList.ColumnHeader.Cells[0, 4].Text = "年龄";
            this.fpRegList.Columns[4].Width = 70F;
            this.fpRegList.Columns[4].AllowAutoSort = true;
            this.fpRegList.ColumnHeader.Cells[0, 5].Text = "挂号级别";
            this.fpRegList.Columns[5].Width = 80F;
            this.fpRegList.Columns[5].AllowAutoSort = true;
            this.fpRegList.ColumnHeader.Cells[0, 6].Text = "挂号科室";
            this.fpRegList.Columns[6].Width = 80F;
            this.fpRegList.Columns[6].AllowAutoSort = true;
            this.fpRegList.ColumnHeader.Cells[0, 7].Text = "看诊医生";
            this.fpRegList.Columns[7].Width = 78F;
            this.fpRegList.Columns[7].AllowAutoSort = true;
            this.fpRegList.ColumnHeader.Cells[0, 8].Text = "序号";
            this.fpRegList.Columns[8].Width = 40;
            this.fpRegList.Columns[8].AllowAutoSort = true;
            this.fpRegList.ColumnHeader.Cells[0, 9].Text = "挂号费(自费总额)";
            this.fpRegList.Columns[9].Width = 120;
            this.fpRegList.Columns[9].AllowAutoSort = true;
            this.fpRegList.ColumnHeader.Cells[0, 10].Text = "记帐诊金金额";
            this.fpRegList.Columns[10].Width = 80;
            this.fpRegList.Columns[10].AllowAutoSort = true;
            this.fpRegList.ColumnHeader.Cells[0, 11].Text = "票据电脑号";
            this.fpRegList.Columns[11].Width = 100;
            this.fpRegList.Columns[11].AllowAutoSort = true;
            this.fpRegList.Columns.Count = 12;
            this.fpRegList.GrayAreaBackColor = SystemColors.Window;
            this.fpRegList.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpRegList.RowCount = 0;
            
            #endregion

            //初始不显示排班科室
            this.setDeptFpStyle(false);
            this.setDoctFpStyle(false);
        }

        /// <summary>
        /// 初始化挂号级别
        /// </summary>
        /// <returns></returns>
        private int InitRegLevel()
        {
            ArrayList al = this.getRegLevelFromXML();
            if (al == null) return -1;

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

            this.addRegLevelToFp(al);
            this.addRegLevelToCombox(al);
            return 0;
        }

        /// <summary>
        /// 初始化挂号科室
        /// </summary>
        /// <returns></returns>
        private int InitRegDept()
        {
            this.alDept = this.getClinicDepts();
            if (this.alDept == null) this.alDept = new ArrayList();
            foreach (FS.FrameWork.Models.NeuObject obj in this.alDept)
            {
                if (obj.Name.Contains("急"))
                {
                    alEmergDept.Add(obj);
                }
            }

            //获取允许操作员挂号的科室列表
            this.alAllowedDept = this.getAllowedDepts();
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
            this.addAllowedDeptToDataSet(this.alAllowedDept);

            //默认可挂所有门诊科室
            if (alAllowedDept.Count == 0)
            {
                this.addClinicDeptsToDataSet(this.alDept);
            }

            //添加挂号科室
            this.addRegDeptToFp(false);
            //添加挂号科室到combox
            this.addRegDeptToCombox();

            return 0;
        }

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
            }
            this.cmbDoctor.AddItems(alDoct);
            this.addDoctToDataSet(alDoct);
            this.addDoctToFp(false);
            return 0;
        }

        /// <summary>
        /// 初始化结算类别
        /// </summary>
        /// <returns></returns>
        private int InitPact()
        {
            int count = 0, colCount = 0, row = 0;
            colCount = this.fpPayKindList.ColumnCount / 2;
            this.fpPayKindList.RowCount = 0;
            ArrayList al = feeMgr.QueryPactUnitOutPatient();
            if (al == null)
            {
                MessageBox.Show("获取患者合同单位信息时出错!" + this.conMgr.Err, "提示");
                return -1;
            }
            foreach (FS.FrameWork.Models.NeuObject obj in al)
            {
                if (count % colCount == 0)
                {
                    this.fpPayKindList.Rows.Add(this.fpPayKindList.RowCount, 1);
                    row = this.fpPayKindList.RowCount - 1;
                }
                this.fpPayKindList.SetValue(row, 2 * (count % colCount), obj.ID, false);
                this.fpPayKindList.SetValue(row, 2 * (count % colCount) + 1, obj.Name, false);
                count++;
            }
            this.cmbPatientType.AddItems(al);
            if (al.Count > 0)
            {
                this.cmbPatientType.SelectedIndex = 0;
            }
            return 0;
        }

        /// <summary>
        /// 初始化排版选择器
        /// </summary>
        /// <returns></returns>
        private int InitChooseUC()
        {
            this.ucChooseDate = new ucChooseBookingDate();

            this.Controls.Add(ucChooseDate);
            this.ucChooseDate.BringToFront();
            this.ucChooseDate.Location = new Point(this.plRegInfo.Left + this.dtSeeDate.Left, this.plRegInfo.Top + this.dtSeeDate.Top + this.dtSeeDate.Height);
            this.ucChooseDate.Visible = false;
            return 0;
        }

        /// <summary>
        /// 初始化午别
        /// </summary>
        private void InitNoon()
        {
            this.alNoon = noonMgr.Query();
            if (alNoon == null)
            {
                MessageBox.Show("获取午别信息时出错!" + noonMgr.Err, "提示");
                return;
            }
        }

        /// <summary>
        /// 初始化其他控件
        /// </summary>
        /// <returns></returns>
        private int InitOtherCtls()
        {
            //FP热键屏蔽
            InputMap im;
            im = this.fpPayMode.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.fpPayMode.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Left, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.fpPayMode.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Right, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            ArrayList payModes = this.conMgr.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PAYMODES);
            this.helpPayMode.ArrayObject = payModes;
            //卡类型
            ArrayList cardType = this.conMgr.QueryConstantList("IDCard");
            if (cardType == null)
            {
                MessageBox.Show("获取证件类型时出错!" + this.conMgr.Err, "提示");
                return -1;
            }
            this.cmbCardType.AddItems(cardType);
            //患者性别
            this.cmbGender.AddItems(FS.HISFC.Models.Base.SexEnumService.List());
            //患者类型
            ArrayList alPateintType = this.conMgr.GetConstantList("MemCardType");
            if (alPateintType == null)
            {
                MessageBox.Show("获取会员类型时出错!" + this.conMgr.Err, "提示");
                return -1;
            }
            this.cmbCardLevel.AddItems(alPateintType);
            //外屏显示
            if (isShowMiltScreen)
            {
                if (Screen.AllScreens.Length > 1)
                {
                    iMultiScreen = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject<FS.HISFC.BizProcess.Interface.FeeInterface.IMultiScreen>(this.GetType());
                    if (iMultiScreen == null)
                    {
                        iMultiScreen = new Forms.frmMiltScreen();

                    }
                    //显示收费员
                    FS.HISFC.Models.Base.Employee currentOperator = accountMgr.Operator as FS.HISFC.Models.Base.Employee;
                    System.Collections.Generic.List<Object> lo = new System.Collections.Generic.List<object>();
                    lo.Add("");//患者基本信息
                    lo.Add("");//挂号级别
                    lo.Add("");// 挂号科室
                    lo.Add("");//挂号医生;
                    lo.Add("");//挂号费用
                    lo.Add(currentOperator);//收费员信息（非初始化界面值为空）
                    this.iMultiScreen.ListInfo = lo;
                    iMultiScreen.ShowScreen();
                }
            }
            return 0;
        }

        /// <summary>
        /// 获取当前处方号
        /// </summary>
        /// <param name="OperID"></param>		
        private void GetRecipeNo(string OperID)
        {
            if (this.GetRecipeType == 1)
            {
                this.tbRecipeNo.Text = "";//每次登陆自己录入处方号
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
                    this.tbRecipeNo.Text = "0";
                }
                else
                {
                    this.tbRecipeNo.Text = obj.Name;
                }
            }
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
                    this.tbRecipeNo.Text = "0";
                }
                else
                {
                    this.tbRecipeNo.Text = obj.Name;
                }
            }
        }

        /// <summary>
        /// 按操作员检索当日挂号信息
        /// </summary>
        private void SetRegList()
        {
            this.fpRegList.RowCount = 0;
            DateTime current = this.regMgr.GetDateTimeFromSysDateTime();
            ArrayList al = this.regMgr.Query(current.Date, current.Date.AddDays(1), this.regMgr.Operator.ID);
            if (al == null)
            {
                MessageBox.Show("检索挂号员当日挂号信息时出错!" + regMgr.Err, "提示");
                return;
            }
            foreach (FS.HISFC.Models.Registration.Register obj in al)
            {
                this.addRegister(obj);
            }
        }

        /// <summary>
        /// 接口初始化
        /// </summary>
        /// <returns></returns>
        protected virtual int InitInterface()
        {
            this.iProcessRegiter = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(),
                typeof(FS.HISFC.BizProcess.Interface.Registration.IProcessRegiter)) as FS.HISFC.BizProcess.Interface.Registration.IProcessRegiter;
            return 1;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void ChangeInvoiceNO()
        {
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
        }

        #region SetStyles

        /// <summary>
        /// 设置科室列表显示的格式
        /// </summary>
        /// <param name="IsDisplaySchema"></param>
        private void setDeptFpStyle(bool IsDisplaySchema)
        {
            //显示专科排班科室,显示代码、科室名称、午别、时间段、挂号限额、已挂数量、预约限额、预约已挂
            //this.fpDeptList.Reset();
            this.fpDeptList.RowCount = 0;
            this.fpDeptList.ColumnCount = 0;
            this.fpDeptList.SheetName = "科室列表";
            FarPoint.Win.Spread.CellType.TextCellType txtCell = new FarPoint.Win.Spread.CellType.TextCellType();
            if (IsDisplaySchema)
            {
                this.fpDeptList.ColumnCount = 7;
                this.fpDeptList.ColumnHeader.Columns[0].Width = 45;
                this.fpDeptList.ColumnHeader.Cells[0, 0].Text = "代码";
                this.fpDeptList.ColumnHeader.Columns[1].Width = 95;
                this.fpDeptList.ColumnHeader.Cells[0, 1].Text = "科室名称";
                this.fpDeptList.ColumnHeader.Columns[2].Width = 120;
                this.fpDeptList.ColumnHeader.Cells[0, 2].Text = "出诊时间";
                this.fpDeptList.ColumnHeader.Columns[3].Width = 95;
                this.fpDeptList.ColumnHeader.Cells[0, 3].Text = "挂号限额";
                this.fpDeptList.ColumnHeader.Columns[4].Width = 95;
                this.fpDeptList.ColumnHeader.Cells[0, 4].Text = "已挂号数";
                this.fpDeptList.ColumnHeader.Columns[5].Width = 95;
                this.fpDeptList.ColumnHeader.Cells[0, 5].Text = "预约限额";
                this.fpDeptList.Columns[0].CellType = txtCell;
                this.fpDeptList.Columns[3].ForeColor = Color.Red;
                this.fpDeptList.Columns[3].Font = new Font("宋体", 10, FontStyle.Bold);
                this.fpDeptList.Columns[5].ForeColor = Color.Blue;
                this.fpDeptList.Columns[5].Font = new Font("宋体", 10, FontStyle.Bold);
                this.fpDeptList.ColumnHeader.Cells[0, 6].Text = "预约已挂";
            }
            else//对于专家、特诊和没有排班的科室,只显示代码和名称
            {
                this.fpDeptList.ColumnCount = this.DisplayDeptColumnCnt * 2;
                int width = 700 * 2 / this.fpDeptList.ColumnCount;
                for (int i = 0; i < this.fpDeptList.ColumnCount; i++)
                {
                    if (i % 2 == 0)
                    {
                        this.fpDeptList.ColumnHeader.Cells[0, i].Text = "代码";
                        this.fpDeptList.Columns[i].Width = width / 3;
                        this.fpDeptList.Columns[i].BackColor = Color.Linen;
                        this.fpDeptList.Columns[i].CellType = txtCell;
                    }
                    else
                    {
                        this.fpDeptList.ColumnHeader.Cells[0, i].Text = "科室名称";
                        this.fpDeptList.Columns[i].Width = width * 2 / 3;
                    }
                }
            }
            this.fpDeptList.GrayAreaBackColor = SystemColors.Window;
            this.fpDeptList.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpDeptList.RowHeader.Visible = false;
            this.fpDeptList.RowCount = 0;
        }

        /// <summary>
        /// 设置医生列表显示的格式
        /// </summary>
        /// <param name="IsDisplaySchema"></param>
        private void setDoctFpStyle(bool IsDisplaySchema)
        {
            //this.fpDoctList.Reset();
            this.fpDoctList.RowCount = 0;
            this.fpDoctList.ColumnCount = 0;
            this.fpDoctList.SheetName = "医生列表";
            FarPoint.Win.Spread.CellType.TextCellType txtCell = new FarPoint.Win.Spread.CellType.TextCellType();
            if (IsDisplaySchema)
            {
                this.fpDoctList.ColumnCount = 10;
                this.fpDoctList.ColumnHeader.Rows[0].Height = 30;
                this.fpDoctList.ColumnHeader.Cells[0, 0].Text = "代码";
                this.fpDoctList.ColumnHeader.Columns[0].Width = 40;
                this.fpDoctList.ColumnHeader.Cells[0, 1].Text = "专家名称";
                this.fpDoctList.ColumnHeader.Columns[1].Width = 60;
                this.fpDoctList.ColumnHeader.Cells[0, 2].Text = "出诊时间";
                this.fpDoctList.ColumnHeader.Columns[2].Width = 120;
                this.fpDoctList.ColumnHeader.Cells[0, 3].Text = "挂号限额";
                this.fpDoctList.ColumnHeader.Columns[3].Width = 60;
                this.fpDoctList.ColumnHeader.Cells[0, 4].Text = "剩余号数";
                this.fpDoctList.ColumnHeader.Columns[4].Width = 60;
                this.fpDoctList.ColumnHeader.Cells[0, 5].Text = "预约限额";
                this.fpDoctList.ColumnHeader.Columns[5].Width = 60;
                this.fpDoctList.ColumnHeader.Cells[0, 6].Text = "已预约数";
                this.fpDoctList.ColumnHeader.Columns[6].Width = 60;
                this.fpDoctList.ColumnHeader.Cells[0, 7].Text = "特诊限额";
                this.fpDoctList.ColumnHeader.Columns[7].Width = 60;
                this.fpDoctList.ColumnHeader.Cells[0, 8].Text = "特诊已挂";
                this.fpDoctList.ColumnHeader.Columns[8].Width = 60;
                this.fpDoctList.ColumnHeader.Cells[0, 9].Text = "专长";
                this.fpDoctList.ColumnHeader.Columns[9].Width = 100;

                this.fpDoctList.Columns[0].CellType = txtCell;
                this.fpDoctList.Columns[3].ForeColor = Color.Red;
                this.fpDoctList.Columns[3].Font = new Font("宋体", 10, FontStyle.Bold);
                this.fpDoctList.Columns[5].ForeColor = Color.Blue;
                this.fpDoctList.Columns[5].Font = new Font("宋体", 10, FontStyle.Bold);
                this.fpDoctList.Columns[7].ForeColor = Color.Magenta;
                this.fpDoctList.Columns[7].Font = new Font("宋体", 10, FontStyle.Bold);
            }
            else
            {
                this.fpDoctList.ColumnCount = this.DisplayDoctColumnCnt * 2;
                int width = 700 * 2 / this.fpDoctList.ColumnCount;
                for (int i = 0; i < this.fpDoctList.ColumnCount; i++)
                {
                    if (i % 2 == 0)
                    {
                        this.fpDoctList.ColumnHeader.Cells[0, i].Text = "代码";
                        this.fpDoctList.Columns[i].Width = width / 3;
                        this.fpDoctList.Columns[i].BackColor = Color.Linen;
                        this.fpDoctList.Columns[i].CellType = txtCell;
                    }
                    else
                    {
                        this.fpDoctList.ColumnHeader.Cells[0, i].Text = "教授名称";
                        this.fpDoctList.Columns[i].Width = width * 2 / 3;
                    }
                }
            }
            this.fpDoctList.GrayAreaBackColor = SystemColors.Window;
            this.fpDoctList.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpDoctList.RowHeader.Visible = false;
            this.fpDoctList.RowCount = 0;
        }

        #endregion

        #region InitRegLevel

        /// <summary>
        /// 读取挂号级别,权限控制
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
            catch 
            { 
                return alLists; 
            }

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
        /// 添加挂号级别
        /// </summary>
        /// <param name="regLevels">挂号级别列表</param>
        /// <returns></returns>
        private int addRegLevelToFp(ArrayList regLevels)
        {
            int count = 0, row = 0, colCount = 0;
            colCount = this.fpRegLvlList.ColumnCount / 2;
            this.fpRegLvlList.RowCount = 0;

            foreach (FS.FrameWork.Models.NeuObject obj in regLevels)
            {
                if (count % colCount == 0)
                {
                    this.fpRegLvlList.Rows.Add(this.fpRegLvlList.RowCount, 1);
                    row = this.fpRegLvlList.RowCount - 1;
                }
                this.fpRegLvlList.SetValue(row, 2 * (count % colCount), obj.ID, false);
                this.fpRegLvlList.SetValue(row, 2 * (count % colCount) + 1, obj.Name, false);
                count++;
            }
            return 0;
        }

        /// <summary>
        /// 将挂号级别添加到Combox中
        /// </summary>
        /// <param name="regLevels"></param>
        /// <returns></returns>
        private int addRegLevelToCombox(ArrayList regLevels)
        {
            this.cmbRegLevel.AddItems(regLevels);
            return 0;
        }

        #endregion

        #region InitRegDept

        /// <summary>
        /// 获取所有门诊科室
        /// </summary>
        /// <returns></returns>
        private ArrayList getClinicDepts()
        {
            ArrayList al = this.conMgr.QueryRegDepartment();
            if (al == null)
            {
                MessageBox.Show("获取门诊科室时出错!" + this.conMgr.Err, "提示");
                return null;
            }
            return al;
        }

        /// <summary>
        /// 获取允许操作员挂号的科室列表
        /// </summary>
        /// <returns></returns>
        private ArrayList getAllowedDepts()
        {
            ArrayList al = this.permissMgr.Query((FS.FrameWork.Models.NeuObject)this.regMgr.Operator);
            if (al == null)
            {
                MessageBox.Show("获取操作员挂号科室时出错!" + this.permissMgr.Err, "提示");
                return null;
            }
            if (al.Count > 0)
            {
                FS.FrameWork.Models.NeuObject obj = al[0] as FS.FrameWork.Models.NeuObject;
                if (obj.Memo == "0")
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

        /// <summary>
        /// 将允许操作员挂号的科室添加到DataSet
        /// </summary>
        /// <param name="allowedDepts"></param>
        private void addAllowedDeptToDataSet(ArrayList allowedDepts)
        {
            this.dsItems.Tables[0].Rows.Clear();
            foreach (FS.FrameWork.Models.NeuObject obj in allowedDepts)
            {
                FS.HISFC.Models.Base.Department dept = this.GetDeptByID(obj.User01);
                if (dept != null)
                {
                    this.addDeptToDataSet(dept);
                }
            }
        }

        /// <summary>
        /// 将门诊科室添加到Dataset
        /// </summary>
        /// <param name="depts"></param>
        private void addClinicDeptsToDataSet(ArrayList depts)
        {
            this.dsItems.Tables[0].Rows.Clear();
            foreach (FS.HISFC.Models.Base.Department dept in depts)
            {
                this.addDeptToDataSet(dept);
            }
        }

        /// <summary>
        /// 添加挂号科室
        /// </summary>
        /// <returns></returns>
        private int addRegDeptToFp(bool IsDisplaySchema)
        {
            //添加到farpoint
            DataRowView dataRow;
            this.fpDeptList.RowCount = 0;
            if (IsDisplaySchema)
            {
                int i = 0;
                for (int s = 0; s < dvDepts.Count; s++)
                {
                    dataRow = dvDepts[i];
                    this.fpDeptList.Rows.Add(this.fpDeptList.RowCount, 1);
                    i = this.fpDeptList.RowCount - 1;
                    this.fpDeptList.SetValue(i, 0, dataRow["ID"], false);
                    this.fpDeptList.SetValue(i, 1, dataRow["Name"], false);

                    if (dataRow["IsAppend"].ToString().ToUpper() == "TRUE")//加号
                    {
                        this.fpDeptList.SetValue(i, 2, this.getNoon(dataRow["Noon"].ToString()) + "[加号]", false);
                    }
                    else
                    {
                        this.fpDeptList.SetValue(i, 2, this.getNoon(dataRow["Noon"].ToString()) +
                            "[" + DateTime.Parse(dataRow["BeginTime"].ToString()).ToString("HH:mm") + "～" +
                            DateTime.Parse(dataRow["EndTime"].ToString()).ToString("HH:mm") + "]", false);
                    }
                    this.fpDeptList.SetValue(i, 3, dataRow["RegLmt"], false);
                    this.fpDeptList.SetValue(i, 4, dataRow["Reged"], false);
                    this.fpDeptList.SetValue(i, 5, dataRow["TelLmt"], false);
                    this.fpDeptList.SetValue(i, 6, dataRow["Teled"], false);
                }
                this.fpDeptList.Tag = "1";
            }
            else
            {
                int count = 0, colCount = 0, row = 0;
                colCount = this.fpDeptList.Columns.Count / 2;
                for (int i = 0; i < dvDepts.Count; i++)
                {
                    if (count % colCount == 0)
                    {
                        this.fpDeptList.Rows.Add(this.fpDeptList.RowCount, 1);
                        row = this.fpDeptList.RowCount - 1;
                    }
                    dataRow = dvDepts[i];
                    this.fpDeptList.SetValue(row, 2 * (count % colCount), dataRow[0].ToString(), false);
                    this.fpDeptList.SetValue(row, 2 * (count % colCount) + 1, dataRow[1].ToString(), false);
                    count++;
                }
                this.fpDeptList.Tag = "0";
            }
            return 0;
        }

        /// <summary>
        /// 添加到挂号科室下拉列表
        /// </summary>
        private void addRegDeptToCombox()
        {
            DataRow row;
            ArrayList al = new ArrayList();
            for (int i = 0; i < this.dsItems.Tables["Dept"].Rows.Count; i++)
            {
                row = this.dsItems.Tables["Dept"].Rows[i];
                //重复的不添加
                if (i > 0 && row["ID"].ToString() == dsItems.Tables["Dept"].Rows[i - 1]["ID"].ToString())
                {
                    continue;
                }
                FS.HISFC.Models.Base.Department dept = new FS.HISFC.Models.Base.Department();
                dept.ID = row["ID"].ToString();
                dept.Name = row["Name"].ToString();
                dept.SpellCode = row["Spell_Code"].ToString();
                dept.WBCode = row["Wb_Code"].ToString();
                dept.UserCode = row["Input_Code"].ToString();

                al.Add(dept);
            }

            this.cmbDept.AddItems(al);
        }

        /// <summary>
        /// 查找科室-根据科室代码
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Base.Department GetDeptByID(string ID)
        {
            foreach (FS.HISFC.Models.Base.Department obj in this.alDept)
            {
                if (obj.ID == ID)
                    return obj;
            }
            return null;
        }

        /// <summary>
        /// Add deptartment to DataSet
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
            int time = int.Parse(current.ToString("HHmmss"));

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

        #endregion

        #region InitDoct

        /// <summary>
        /// 将医生添加到DataSet 
        /// </summary>
        /// <param name="alPersons"></param>
        /// <returns></returns>
        private int addDoctToDataSet(ArrayList alPersons)
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
        private void addDoctToDataSet(DataSet ds)
        {
            dsItems.Tables["Doct"].Rows.Clear();
            DateTime dtNow = this.SchemaMgr.GetDateTimeFromSysDateTime();  //获取系统时间

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];
                //挂今天以后的号无需午别限制
                if (this.IsFilterSchemeByNoon && this.dtSeeDate.Value.Date == dtNow.Date)
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
        private int addDoctToFp(bool IsDisplaySchema)
        {
            DataRowView row;
            this.fpDoctList.RowCount = 0;
            if (IsDisplaySchema)
            {
                #region ""

                FS.HISFC.Models.Registration.RegLevel level = (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;
                if (this.isProfessor(level)) //挂教授号，教授排在前面
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
                    this.fpDoctList.Rows.Add(this.fpDoctList.RowCount, 1);
                    i = this.fpDoctList.RowCount - 1;
                    this.fpDoctList.SetValue(i, 0, row["ID"], false);
                    this.fpDoctList.SetValue(i, 1, row["Name"], false);

                    if (row["IsAppend"].ToString().ToUpper() == "TRUE")//加号
                    {
                        this.fpDoctList.SetValue(i, 2, this.getNoon(row["Noon"].ToString()) + "[加号]", false);
                    }
                    else
                    {
                        this.fpDoctList.SetValue(i, 2, this.getNoon(row["Noon"].ToString()) +
                            "[" + DateTime.Parse(row["BeginTime"].ToString()).ToString("HH:mm") + "～" +
                            DateTime.Parse(row["EndTime"].ToString()).ToString("HH:mm") + "]", false);
                    }
                    this.fpDoctList.SetValue(i, 3, row["RegLmt"], false);
                    this.fpDoctList.SetValue(i, 4, FS.FrameWork.Function.NConvert.ToInt32(row["RegLmt"]) - FS.FrameWork.Function.NConvert.ToInt32(row["Reged"]), false);
                    this.fpDoctList.SetValue(i, 5, row["TelLmt"], false);
                    this.fpDoctList.SetValue(i, 6, row["Teled"], false);
                    this.fpDoctList.SetValue(i, 7, row["SpeLmt"], false);
                    this.fpDoctList.SetValue(i, 8, row["Sped"], false);
                    this.fpDoctList.SetValue(i, 9, row["Memo"], false);
                    //教授、付教授颜色区分
                    if (row["IsProfessor"].ToString().ToUpper() == "TRUE")
                    {
                        this.fpDoctList.Rows[i].BackColor = Color.LightGreen;
                    }
                }
                this.Span();

                #endregion

                this.fpDoctList.Tag = "1";
            }
            else
            {
                int RowCount = 0, ColumnCount, Row = 0;
                ColumnCount = this.fpDoctList.ColumnCount / 2;
                foreach (DataRowView dv in this.dvDocts)
                {
                    if (RowCount % ColumnCount == 0)
                    {
                        this.fpDoctList.Rows.Add(this.fpDoctList.RowCount, 1);
                        Row = this.fpDoctList.RowCount - 1;
                    }

                    this.fpDoctList.SetValue(Row, 2 * (RowCount % ColumnCount), dv["ID"].ToString(), false);
                    this.fpDoctList.SetValue(Row, 2 * (RowCount % ColumnCount) + 1, dv["Name"].ToString(), false);

                    RowCount++;
                }
                this.fpDoctList.Tag = "0";
            }
            return 0;
        }

        /// <summary>
        /// 挂的是教授号/副教授号
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private bool isProfessor(FS.HISFC.Models.Registration.RegLevel level)
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
        /// 压缩显示医生姓名
        /// </summary>
        private void Span()
        {
            int rowLastDoct = 0;
            int rowCnt = this.fpDoctList.RowCount;

            for (int i = 0; i < rowCnt; i++)
            {
                if (i > 0 && this.fpDoctList.GetText(i, 0) != this.fpDoctList.GetText(i - 1, 0))
                {
                    if (i - rowLastDoct > 1)
                    {
                        this.fpDoctList.Models.Span.Add(rowLastDoct, 0, i - rowLastDoct, 1);
                        this.fpDoctList.Models.Span.Add(rowLastDoct, 1, i - rowLastDoct, 1);
                    }

                    rowLastDoct = i;
                }

                //最后一行处理
                if (i > 0 && i == rowCnt - 1 && this.fpDoctList.GetText(i, 0) == this.fpDoctList.GetText(i - 1, 0))
                {
                    this.fpDoctList.Models.Span.Add(rowLastDoct, 0, i - rowLastDoct + 1, 1);
                    this.fpDoctList.Models.Span.Add(rowLastDoct, 1, i - rowLastDoct + 1, 1);
                }
            }
        }

        #endregion

        #region SetRegList

        /// <summary>
        /// 添加挂号记录
        /// </summary>
        /// <param name="obj"></param>
        private void addRegister(FS.HISFC.Models.Registration.Register obj)
        {
            this.fpRegList.Rows.Add(this.fpRegList.RowCount, 1);
            int cnt = this.fpRegList.RowCount - 1;
            this.fpRegList.ActiveRowIndex = cnt;
            try
            {
                if (obj.Card != null && !string.IsNullOrEmpty(obj.Card.ID))
                {
                    this.fpRegList.SetValue(cnt, 0, obj.Card.ID, false);
                }
                else
                {
                    List<FS.HISFC.Models.Account.AccountCard> list = this.accountMgr.GetMarkList(obj.PID.CardNO);
                    if (list.Count == 0)
                    {
                        this.fpRegList.SetValue(cnt, 0, obj.PID.CardNO, false);
                    }
                    else
                    {
                        this.fpRegList.SetValue(cnt, 0, list[0].MarkNO, false);
                    }
                }
            }
            catch (Exception e)
            {
                this.fpRegList.SetValue(cnt, 0, obj.PID.CardNO, false);
            }

            this.fpRegList.SetValue(cnt, 1, obj.Name, false);//姓名
            this.fpRegList.SetValue(cnt, 2, obj.Pact.Name, false);
            this.fpRegList.SetValue(cnt, 3, obj.Birthday.ToShortDateString(), false);
            this.fpRegList.SetValue(cnt, 4, permissMgr.GetAge(obj.Birthday), false);
            this.fpRegList.SetValue(cnt, 5, obj.DoctorInfo.Templet.RegLevel.Name, false);
            this.fpRegList.SetValue(cnt, 6, obj.DoctorInfo.Templet.Dept.Name, false);
            this.fpRegList.SetValue(cnt, 7, obj.DoctorInfo.Templet.Doct.Name, false);
            this.fpRegList.SetValue(cnt, 8, obj.OrderNO, false);
            this.fpRegList.SetValue(cnt, 9, obj.OwnCost, false);
            this.fpRegList.SetValue(cnt, 10, obj.PubCost, false);
            this.fpRegList.SetValue(cnt, 11, obj.InvoiceNO, false);
            if (obj.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Back ||
                obj.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Cancel)
            {
                this.fpRegList.Rows[cnt].BackColor = Color.Green;
            }
            this.fpRegList.Rows[cnt].Tag = obj;
        }

        /// <summary>
        /// 有效挂号数
        /// </summary>
        /// <returns></returns>
        private string GetRegNum()
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

        #endregion 

        #endregion

        #region AddEvents

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucRegister_Load(object sender, EventArgs e)
        {
            this.AddEvents();
            this.Clear();
            this.initInputMenu();
            this.readInputLanguage();
            this.ChangeRecipe();
            this.InitInvoiceInfo();
        }
        
        /// <summary>
        /// 窗体处于激活
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucRegister_Activated(object sender, EventArgs e)
        {
            if (this.iMultiScreen != null)
            {
                this.iMultiScreen.ShowScreen();
            }
        }
        
        /// <summary>
        /// 窗体失去焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucRegister_Deactivate(object sender, EventArgs e)
        {
            if (this.iMultiScreen != null)
            {
                this.iMultiScreen.CloseScreen();
            }
        }
        
        /// <summary>
        /// 窗口关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucRegister_FormClosing(object sender, FormClosingEventArgs e)
        {
            string recipeNO = this.tbRecipeNo.Text.Trim();
            if ((recipeNO != "" && recipeNO != string.Empty))
            {
                this.SaveRecipeNo();
            }
        }

        /// <summary>
        /// 会员代付弹出界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAccount_Click(object sender, EventArgs e)
        {
            this.AccountPayShow();
        }

        /// <summary>
        /// 会员支付弹出界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEmpower_Click(object sender, EventArgs e)
        {
            this.EmpowerPayShow();
        }


        //{0A94EF4E-FD1D-43be-A223-BC59B8433BF6}
        private void tbPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                HISFC.Components.Common.Forms.frmQueryPatientByConditions frmQuery = new FS.HISFC.Components.Common.Forms.frmQueryPatientByConditions();

                string QueryStr = this.tbPhone.Text;

                if (string.IsNullOrEmpty(QueryStr))
                {
                    return;
                }

                frmQuery.QueryByPhone(QueryStr);
                frmQuery.ShowDialog();

                if (frmQuery.DialogResult == DialogResult.OK)
                {
                    this.tbQueryNO.Text = frmQuery.PatientInfo.PID.CardNO;
                    tbQueryNO_KeyDown(new object(), new KeyEventArgs(Keys.Enter));
                }
            }

        }

        //{0A94EF4E-FD1D-43be-A223-BC59B8433BF6}
        private void tbName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                HISFC.Components.Common.Forms.frmQueryPatientByConditions frmQuery = new FS.HISFC.Components.Common.Forms.frmQueryPatientByConditions();

                string QueryStr = this.tbName.Text;

                if (string.IsNullOrEmpty(QueryStr))
                {
                    return;
                }

                frmQuery.QueryByName(QueryStr);
                frmQuery.ShowDialog();

                if (frmQuery.DialogResult == DialogResult.OK)
                {
                    this.tbQueryNO.Text = frmQuery.PatientInfo.PID.CardNO;
                    tbQueryNO_KeyDown(new object(), new KeyEventArgs(Keys.Enter));
                }
            }
        }

        /// <summary>
        /// 查询框按键事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbQueryNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    string cardNO = string.Empty;
                    string queryStr = this.tbQueryNO.Text.Trim();
                    if (string.IsNullOrEmpty(queryStr))
                    {
                        throw new Exception("请输入卡号、病历号、预约号进行检索！");
                    }

                    if (this.ValidCardNO(queryStr) < 0)
                    {
                        throw new Exception("此号段为直接收费使用，请选择其它号段!");
                    }
                    this.Clear();

                    FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
                    int rev = this.feeMgr.ValidMarkNO(queryStr, ref accountCard);
                    if (rev > 0)
                    {
                        FS.HISFC.Models.RADT.PatientInfo patient = accountMgr.GetPatientInfoByCardNO(accountCard.Patient.PID.CardNO);
                        if (patient == null || string.IsNullOrEmpty(patient.PID.CardNO))
                        {
                            throw new Exception("未找到患者信息！");
                        }

                        this.regObj = this.getRegInfo(patient.PID.CardNO);
                        if (regObj == null)
                        {
                            throw new Exception("获取挂号实体出错！");
                        }

                        this.setRegInfo(this.regObj, accountCard);
                        this.setPayInfo(this.regObj);
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
                                outScreen.Add(this.tbRealCost.Text);//应收费用
                                outScreen.Add("");//收费员信息
                                this.iMultiScreen.ListInfo = outScreen;
                            }
                        }
                        //跳转到挂号级别选择框
                        this.cmbRegLevel.Focus();
                    }
                    else if (rev == -1)
                    {
                        throw new Exception("未找到患者信息！");
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg(ex.Message), FS.FrameWork.Management.Language.Msg("提示"));
                    this.tbQueryNO.SelectAll();
                    this.tbQueryNO.Focus();
                    return;
                }
            }
        }
        
        /// <summary>
        /// 证件类型选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCardType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.tbIDNO.Text = "";
        }
        
        /// <summary>
        /// 证件号输入是否合法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbIDNO_Leave(object sender, EventArgs e)
        {
            if (cmbCardType.SelectedItem != null && cmbCardType.SelectedItem.Name == "身份证")
            {
                //校验身份证号
                if (!string.IsNullOrEmpty(this.tbIDNO.Text))
                {
                    this.DelEvents();
                    int reurnValue = this.ProcessIDENNO(this.tbIDNO.Text, EnumCheckIDNOType.BeforeSave);
                    this.AddEvents();
                }
            }
        }
        
        /// <summary>
        /// 年龄输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbAge_TextChanged(object sender, EventArgs e)
        {
            this.DelEvents();
            this.GetBirthday();
            this.AddEvents();
        }
        
        /// <summary>
        /// 年龄单位输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DelEvents();
            this.GetBirthday();
            this.AddEvents();
        }
        
        /// <summary>
        /// 生日改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpBirthday_ValueChanged(object sender, EventArgs e)
        {
            this.DelEvents();
            DateTime current = this.regMgr.GetDateTimeFromSysDateTime().Date;
            if (this.dtpBirthday.Value.Date > current)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("出生日期不能大于当前时间"), "提示");
                this.dtpBirthday.Focus();
                this.AddEvents();
                return;
            }

            //计算年龄
            if (this.dtpBirthday.Value.Date != current)
            {
                this.setAge(this.dtpBirthday.Value);
            }
            this.AddEvents();
        }
        
        /// <summary>
        /// 会员类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCardLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// 合同单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbPatientType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.DelEvents();
                this.queryRegLevl();
                this.getCost();
            }
            catch
            {
            }
            this.AddEvents();
        }

        /// <summary>
        /// 合同单位光标进入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbPatientType_Enter(object sender, EventArgs e)
        {
            this.fpTipsInfo.ActiveSheet = this.fpPayKindList;
        }

        /// <summary>
        /// 合同单位光标退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbPatientType_Leave(object sender, EventArgs e)
        {
            this.fpTipsInfo.ActiveSheet = this.fpRegList;
        }
        
        /// <summary>
        /// 挂号级别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbRegLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.DelEvents();
                this.queryRegLevl();
                this.getCost();
            }
            catch
            { 
            }
            this.AddEvents();
        }

        /// <summary>
        /// 挂号级别光标进入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbRegLevel_Enter(object sender, EventArgs e)
        {
            this.fpTipsInfo.ActiveSheet = this.fpRegLvlList;
        }

        /// <summary>
        /// 挂号级别光标离开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbRegLevel_Leave(object sender, EventArgs e)
        {
            this.fpTipsInfo.ActiveSheet = this.fpRegList;
        }
        
        /// <summary>
        /// 挂号科室
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.DelEvents();
                if (this.ucChooseDate.Visible) this.ucChooseDate.Visible = false;
                this.dtSeeDate.Tag = null;
                this.cmbDoctor.Tag = "";
                //专家、专科、特诊号都需要扣排班限额
                FS.HISFC.Models.Registration.RegLevel regLevel = (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;
                if (regLevel == null)
                {
                    throw new Exception("请先选择挂号级别!");
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
                    this.SetDeptZone(this.cmbDept.Tag.ToString(), this.dtSeeDate.Value, regLevel);
                }
                else
                {
                    //设定默认预约时间段
                    this.SetDefaultSeeDateTime(this.dtSeeDate.Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg(ex.Message), FS.FrameWork.Management.Language.Msg("提示"));
                this.cmbRegLevel.Focus();
                this.AddEvents();
                return;
            }

            this.AddEvents();
        }

        /// <summary>
        /// 科室列表进入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmbDept_Enter(object sender, EventArgs e)
        {
            this.fpTipsInfo.ActiveSheet = this.fpDeptList;
        }

        /// <summary>
        /// 科室列表离开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDept_Leave(object sender, EventArgs e)
        {
            this.fpTipsInfo.ActiveSheet = this.fpRegList;
        }
        
        /// <summary>
        /// 挂号医生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.DelEvents();
                if (this.ucChooseDate.Visible) this.ucChooseDate.Visible = false;
                this.dtSeeDate.Tag = null;

                //专家、专科、特诊号都需要扣排班限额
                FS.HISFC.Models.Registration.RegLevel regLevel = (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;
                if (regLevel == null)//没有选择挂号级别
                {
                    throw new Exception("请先选择挂号级别!");
                }

                if (regLevel.IsExpert || regLevel.IsSpecial)
                {
                    //设定一个有效的就诊时间段
                    this.SetDoctZone(this.cmbDoctor.Tag.ToString(), this.dtSeeDate.Value, regLevel);
                }
                else if (regLevel.IsFaculty)
                {
                    if (this.cmbDoctor.Tag != null)
                    {
                        //设定一个有效的就诊时间段
                        this.SetDoctZone(this.cmbDoctor.Tag.ToString(), this.dtSeeDate.Value, regLevel);
                    }
                }
                else
                {
                    //设定默认预约时间段
                    this.SetDefaultSeeDateTime(this.dtSeeDate.Value);
                }

                //挂号职级与医生职级提示
                this.WarningDoctLevel();
            }
            catch (Exception ex)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg(ex.Message), "提示");
                this.cmbRegLevel.Focus();
                this.AddEvents();
                return;
            }
            this.AddEvents();
        }

        /// <summary>
        /// 医生列表进入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDoctor_Enter(object sender, EventArgs e)
        {
            this.fpTipsInfo.ActiveSheet = this.fpDoctList;
        }

        /// <summary>
        /// 医生列表离开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDoctor_Leave(object sender, EventArgs e)
        {
            this.fpTipsInfo.ActiveSheet = this.fpRegList;
        }
        
        /// <summary>
        /// 看诊日期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtSeeDate_ValueChanged(object sender, EventArgs e)
        {
            this.queryRegLevl();
            if (this.ucChooseDate.Visible) this.ucChooseDate.Visible = false;
            this.dtSeeDate.Tag = null;
        }
        
        /// <summary>
        /// 看诊开始时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtBegin_ValueChanged(object sender, EventArgs e)
        {
            //清除预约信息
            if (this.dtSeeDate.Tag is FS.HISFC.Models.Registration.Schema)
            {
                if (((FS.HISFC.Models.Registration.Schema)this.dtSeeDate.Tag).Templet.Begin.ToString("HHmm").CompareTo(this.dtBegin.Value.ToString("HHmm")) > 0
                    ||
                    ((FS.HISFC.Models.Registration.Schema)this.dtSeeDate.Tag).Templet.End.ToString("HHmm").CompareTo(this.dtBegin.Value.ToString("HHmm")) < 0
                    || 
                    this.dtEnd.Value.ToString("HHmm").CompareTo(this.dtBegin.Value.ToString("HHmm")) < 0
                    )
                {
                    this.dtSeeDate.Tag = null;
                }
            }
            if (this.ucChooseDate.Visible) this.ucChooseDate.Visible = false;
        }
        
        /// <summary>
        /// 看诊结束时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtEnd_ValueChanged(object sender, EventArgs e)
        {
            //清除预约信息
            if (this.dtSeeDate.Tag is FS.HISFC.Models.Registration.Schema)
            {
                if (((FS.HISFC.Models.Registration.Schema)this.dtSeeDate.Tag).Templet.Begin.ToString("HHmm").CompareTo(this.dtEnd.Value.ToString("HHmm")) > 0
                    ||
                    ((FS.HISFC.Models.Registration.Schema)this.dtSeeDate.Tag).Templet.End.ToString("HHmm").CompareTo(this.dtEnd.Value.ToString("HHmm")) < 0
                    ||
                    this.dtEnd.Value.ToString("HHmm").CompareTo(this.dtBegin.Value.ToString("HHmm")) < 0
                    )
                {
                    this.dtSeeDate.Tag = null;
                }
            }
            if (this.ucChooseDate.Visible) this.ucChooseDate.Visible = false;
        }
        
        /// <summary>
        /// 选择排版
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkSchema_Click(object sender, EventArgs e)
        {

            if (this.ucChooseDate.Visible)
            {
                this.ucChooseDate.Visible = false;
                this.dtSeeDate.Focus();
            }
            else
            {
                DateTime seeDate = this.dtSeeDate.Value;
                DateTime current = this.regMgr.GetDateTimeFromSysDateTime();

                if (seeDate.Date < current.Date)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("挂号日期不能小于当前日期"), "提示");
                    this.dtSeeDate.Focus();
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

                Registration.RegTypeNUM regType = Registration.RegTypeNUM.Faculty;
                regType = this.getRegType(level);

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

                    if (!this.IsJudgeReglevl)
                    {
                        this.ucChooseDate.QueryDeptBooking(seeDate, deptID, regType);
                    }
                    else
                    {
                        this.ucChooseDate.QueryDeptBooking(seeDate, deptID, regType, level);
                    }

                    if (this.ucChooseDate.Count > 0)
                    {
                        this.ucChooseDate.Visible = true;
                        this.ucChooseDate.Focus();
                    }
                    else if (this.ucChooseDate.Bookings.Count > 0)
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("没有符合条件的排班信息,请重新选择预约日期"), "提示");
                        this.dtSeeDate.Focus();
                        return;
                    }
                    else
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("专科没有排班"), "提示");
                        this.dtSeeDate.Focus();
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

                    if (this.cmbDept.Tag != null && this.cmbDept.Tag.ToString() != "")
                    {
                        this.ucChooseDate.QueryDoctBooking(seeDate, doctID, this.cmbDept.Tag.ToString(), regType);
                        if (this.ucChooseDate.GetValidBooking(regType) == null)
                        {
                            this.ucChooseDate.QueryDoctBooking(seeDate, doctID, regType);
                        }
                    }
                    else
                    {
                        this.ucChooseDate.QueryDoctBooking(seeDate, doctID, regType);
                    }

                    if (this.ucChooseDate.Count > 0)
                    {
                        this.ucChooseDate.Visible = true;
                        this.ucChooseDate.Focus();
                    }
                    else if (this.ucChooseDate.Bookings.Count > 0)
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("没有符合条件的排班信息,请重新选择预约日期"), "提示");
                        this.dtSeeDate.Focus();
                        return;
                    }
                    else
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("专家没有排班"), "提示");
                        this.dtSeeDate.Focus();
                        return;
                    }
                    #endregion
                }
            }
        }
        
        /// <summary>
        /// 发票印刷号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbRealInvoiceNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnUpdate_Click(null, null);
            }
        }
        
        /// <summary>
        /// 更新印刷发票
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

            //realInvoiceNO = realInvoiceNO.PadLeft(12, '0');
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

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("更新成功!");
            InitInvoiceInfo();
        }
        
        /// <summary>
        /// 代付
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkPower_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// 代付取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkPowerCancel_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// 应收金额
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbRealCost_TextChanged(object sender, EventArgs e)
        {
            string cardNo = this.tbCardNO.Text.Trim().PadLeft(10, '0');
            if (cardNo.ToString() != "0000000000")
            {
                this.regObj = this.getRegInfo(cardNo);
                if (this.isShowMiltScreen)
                {
                    // 外屏显示
                    if (Screen.AllScreens.Length > 1)
                    {
                        System.Collections.Generic.List<Object> outScreen = new System.Collections.Generic.List<object>();
                        outScreen.Add(this.regObj);            //患者信息
                        outScreen.Add(this.cmbRegLevel.Text);  //挂号级别
                        outScreen.Add(this.cmbDept.Text);   //挂号科室
                        outScreen.Add(this.cmbDoctor.Text); //挂号医生
                        outScreen.Add(this.tbRealCost.Text);//应收费用
                        outScreen.Add("");                  //收费员
                        this.iMultiScreen.ListInfo = outScreen;
                    }
                }
            }
        }
        
        /// <summary>
        /// 排版选择框选择事件
        /// </summary>
        /// <param name="sender"></param>
        private void ucChooseDate_SelectedItem(FS.HISFC.Models.Registration.Schema sender)
        {
            try
            {
                this.DelEvents();
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
                                this.SetDefaultSeeDateTime(currentTime);
                                this.dtSeeDate.Tag = null;
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
                //专家、专科号现场已挂号数大于现场设号数
                //或者特诊号、特诊已挂号数大于特诊设号数
                if ((((regType == Registration.RegTypeNUM.Faculty || regType == Registration.RegTypeNUM.Expert) && 
                    (sender.Templet.RegQuota <= sender.RegedQTY && sender.Templet.TelQuota <= sender.TelingQTY))||
                    (regType == Registration.RegTypeNUM.Special && sender.Templet.SpeQuota <= sender.SpedQTY)) &&
                    !sender.Templet.IsAppend)
                {
                    if (!this.IsAllowOverrun)
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("已超出排班限额，不允许再进行挂号"), "提示");
                        this.dtSeeDate.Focus();
                        return;
                    }
                }

                this.cmbDept.Tag = sender.Templet.Dept.ID;
                if (sender.Templet.Doct.ID == "None") { this.cmbDoctor.Tag = ""; }
                else { this.cmbDoctor.Tag = sender.Templet.Doct.ID; }
                //挂号时间段
                this.SetSeeDateTime(sender);
                this.tbQueryNO.Focus();
            }
            catch
            { 
            }
            finally 
            {
                this.AddEvents();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chbCardFee_CheckedChanged(object sender, EventArgs e)
        {
            this.DelEvents();
            this.getCost();
            this.AddEvents();
        }

        /// <summary>
        /// 点击支付方式cell
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void fpPayMode_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.fpPayMode_Sheet1.SetActiveCell(e.Row, (int)PayModeCols.TotCost);
        }
        
        /// <summary>
        /// 焦点离开支付方式列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void fpPayMode_Leave(object sender, EventArgs e)
        {
            this.fpPayMode.StopCellEditing();
        }
        
        /// <summary>
        /// 存储输入值的上一个值，用于的输入值非法时进行恢复
        /// </summary>
        private double previousValue = 0.0;
        
        /// <summary>
        /// 开始编辑金额
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpPayMode_EditModeOn(object sender, EventArgs e)
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
        
        /// <summary>
        /// 金额发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpPayMode_Change(object sender, FarPoint.Win.Spread.ChangeEventArgs e)
        {
            //编辑的是支付金额
            if (e.Column == (int)PayModeCols.TotCost)
            {
                this.DelEvents();

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

                    if (this.setCostInfoAfterEdit() < 0)
                    {
                        throw new Exception("输入金额大于应缴纳金额！");
                    }
                }
                catch (Exception ex)
                {
                    this.fpPayMode_Sheet1.Cells[e.Row, e.Column].Value = this.previousValue;
                    MessageBox.Show(ex.Message);
                    this.AddEvents();
                    return;
                }

                this.AddEvents();
            }
        }
        
        /// <summary>
        /// 当前编辑控件触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditingControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                //PutArrow(Keys.Left);
            }
            if (e.KeyCode == Keys.Right)
            {
                //PutArrow(Keys.Right);
            }
        }

        #endregion

        #region ucRegister_Load

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
                m.Click += new EventHandler(m_Click);
                this.msTipsFp.Items.Add(m);
            }
        }

        /// <summary>
        /// 菜单点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem m in this.msTipsFp.Items)
            {
                if (sender == m)
                {
                    m.Checked = true;
                    this.CHInput = this.getInputLanguage(m.Text);
                    this.saveInputLanguage();
                }
                else
                {
                    m.Checked = false;
                }
            }
        }

        #endregion

        #region 私有

        /// <summary>
        /// 清屏
        /// </summary>
        private void Clear()
        {
            this.DelEvents();

            try
            {
                this.regObj = null;
                this.tbQueryNO.Text = "";
                this.tbCardNO.Text = "";
                this.tbName.Text = "";
                this.cmbCardType.Tag = "";
                this.tbIDNO.Text = "";
                this.cmbGender.Tag = "";
                this.tbAge.Text = "1";
                this.cmbUnit.SelectedIndex = 0;
                this.dtpBirthday.Value = this.regMgr.GetDateTimeFromSysDateTime().AddDays(-1);
                this.cmbCardLevel.Tag = "";
                this.cmbPatientType.Tag = "";
                this.tbPhone.Text = "";
                this.tbAddress.Text = "";
                this.cmbRegLevel.Tag = "";
                this.cmbDept.Tag = "";
                this.cmbDoctor.Tag = "";
                this.tbSeeNO.Text = "";
                this.dtSeeDate.Value = this.regMgr.GetDateTimeFromSysDateTime();
                this.dtBegin.Value = this.regMgr.GetDateTimeFromSysDateTime();
                this.dtEnd.Value = this.regMgr.GetDateTimeFromSysDateTime();
                this.lbSchemaTips.Text = "";
                this.InitInvoiceInfo();
                this.fpPayMode_Sheet1.RowCount = 0;
                this.tbAccount.Text = "0.0";
                this.tbPowerAmount.Text = "0.0";
                this.tbRealCost.Text = "0.0";
                this.tbEtcCost.Text = "0.0";
                this.tbTotCost.Text = "0.0";

                //清空会员支付信息
                this.accountPayList.Clear();
                this.giftPayList.Clear();
                this.empowerAccountPayList.Clear();
                this.empowerGiftPayList.Clear();

                ///支付方式列表
                this.fpPayMode_Sheet1.RowCount = 0;

                this.fpTipsInfo.ActiveSheet = this.fpRegList;

                if (isShowMiltScreen)
                {
                    if (Screen.AllScreens.Length > 1)
                    {
                        //显示初始化界面
                        FS.HISFC.Models.Base.Employee currentOperator = accountMgr.Operator as FS.HISFC.Models.Base.Employee;
                        System.Collections.Generic.List<Object> lo = new System.Collections.Generic.List<object>();
                        lo.Add("");                 //患者基本信息
                        lo.Add("");                 //挂号级别
                        lo.Add("");                 //挂号科室
                        lo.Add("");                 //挂号医生;
                        lo.Add("");                 //挂号费用
                        lo.Add(currentOperator);    //收费员信息
                        this.iMultiScreen.ListInfo = lo;
                    }
                }
                this.tbQueryNO.Focus();
            }
            catch (Exception ex)
            {

            }

            this.AddEvents();
        }

        /// <summary>
        /// 根据病历号获得患者挂号信息
        /// </summary>
        /// <param name="CardNo">卡号</param>
        /// <returns></returns>
        private FS.HISFC.Models.Registration.Register getRegInfo(string CardNo)
        {
            if (string.IsNullOrEmpty(CardNo))
            {
                return null;
            }

            FS.HISFC.Models.Registration.Register obj = new FS.HISFC.Models.Registration.Register();
            FS.HISFC.Models.RADT.PatientInfo p;
            int regCount = this.regMgr.QueryRegiterByCardNO(CardNo);
            if (regCount < 0)
            {
                return null;
            }

            if (regCount > 0)
            {
                obj.IsFirst = false;
            }
            else
            {
                obj.IsFirst = true;
            }
            //先检索患者基本信息表,看是否存在该患者信息
            p = this.radtProcess.QueryComPatientInfo(CardNo);

            if (p == null || p.Name == "")
            {
                //不存在基本信息
                obj.PID.CardNO = CardNo;
                obj.Sex.ID = "M";
                obj.Pact.ID = this.DefaultPactID;

            }
            else
            {
                //存在患者基本信息,取基本信息
                obj.PID.CardNO = CardNo;
                obj.Name = p.Name;
                obj.Sex.ID = p.Sex.ID;
                obj.Birthday = p.Birthday;
                obj.Pact.ID = p.Pact.ID;
                obj.Pact.PayKind.ID = p.Pact.PayKind.ID;
                obj.SSN = p.SSN;
                obj.PhoneHome = p.PhoneHome;
                obj.AddressHome = p.AddressHome;
                obj.IDCard = p.IDCard;
                obj.NormalName = p.NormalName;
                obj.IsEncrypt = p.IsEncrypt;
                obj.IDCard = p.IDCard;

                if (p.IsEncrypt == true)
                {
                    obj.Name = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(p.NormalName);
                }

                if (this.validCardType(p.IDCardType.ID))
                {
                    obj.CardType.ID = p.IDCardType.ID;
                }
            }
            return obj;
        }
        
        /// <summary>
        /// 根据患者信息设置界面显示
        /// </summary>
        /// <param name="regInfo">挂号信息信息</param>
        /// <param name="accountCard">卡信息</param>
        private void setRegInfo(FS.HISFC.Models.Registration.Register regInfo, FS.HISFC.Models.Account.AccountCard accountCard)
        {
            try
            {
                this.DelEvents();
                this.tbCardNO.Text = regInfo.PID.CardNO;
                this.tbName.Text = regInfo.Name;
                this.cmbCardType.Tag = regInfo.CardType.ID;
                this.tbIDNO.Text = regInfo.IDCard;
                this.cmbGender.Tag = regInfo.Sex.ID;
                this.setAge(regInfo.Birthday);
                this.dtpBirthday.Value = regInfo.Birthday;
                this.cmbCardLevel.Tag = accountCard.AccountLevel.ID;
                this.cmbPatientType.Tag = regInfo.Pact.ID;
                this.tbPhone.Text = regInfo.PhoneHome;
                this.tbAddress.Text = regInfo.AddressHome;
            }
            catch
            {
            }

            this.AddEvents(); 
        }
        
        /// <summary>
        /// 设置年龄
        /// </summary>
        /// <param name="birthday"></param>
        private void setAge(DateTime birthday)
        {
            this.tbAge.Text = "";
            if (birthday == DateTime.MinValue)
            {
                return;
            }
            DateTime current;
            int year = 0, month = 0, day = 0;
            current = this.regMgr.GetDateTimeFromSysDateTime();
            this.regMgr.GetAge(birthday, current, ref year, ref month, ref day);
            if (year > 1)
            {
                this.tbAge.Text = year.ToString();
                this.cmbUnit.SelectedIndex = 0;
            }
            else if (year == 1)
            {
                if (month >= 0)
                {
                    this.tbAge.Text = year.ToString();
                    this.cmbUnit.SelectedIndex = 0;
                }
                else
                {
                    this.tbAge.Text = Convert.ToString(12 + month);
                    this.cmbUnit.SelectedIndex = 1;
                }
            }
            else if (month > 0)
            {
                this.tbAge.Text = month.ToString();
                this.cmbUnit.SelectedIndex = 1;
            }
            else if (day > 0)
            {
                this.tbAge.Text = day.ToString();
                this.cmbUnit.SelectedIndex = 2;
            }
            this.tbAge.Tag = this.tbAge.Text;
        }

        /// <summary>
        /// 获取出生日期
        /// </summary>
        private void GetBirthday()
        {
            string age = this.tbAge.Text.Trim();
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
                this.tbAge.Focus();
                return;
            }

            if (FS.FrameWork.Function.NConvert.ToInt32(age) > 110)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("输入年龄过大,请重新输入"), "提示");
                this.tbAge.Focus();
                return;
            }

            DateTime birthday = DateTime.MinValue;
            this.GetBirthday(i, this.cmbUnit.Text, ref birthday);

            if (birthday < this.dtpBirthday.MinDate)
            {
                MessageBox.Show("年龄不能过大!", "提示");
                this.tbAge.Focus();
                return;
            }

            this.dtpBirthday.Value = birthday;
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
            if (FS.FrameWork.Function.NConvert.ToInt32(this.tbAge.Tag) == age)
            {
                this.regMgr.GetAge(this.dtpBirthday.Value, current, ref year, ref month, ref day);
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
        
        /// <summary>
        /// 设置支付方式列表
        /// </summary>
        /// <param name="regInfo"></param>
        private void setPayInfo(FS.HISFC.Models.Registration.Register regInfo)
        {
            this.DelEvents();
            try
            {
                this.fpPayMode_Sheet1.RowCount = 0;
                if (regInfo == null)
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
                        this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.RowCount - 1, (int)PayModeCols.Memo].Locked = false;

                        //账户支付
                        if (paymode.ID == "DC" || paymode.ID == "YS")
                        {
                            this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.RowCount - 1, (int)PayModeCols.TotCost].Locked = true;
                            this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.RowCount - 1, (int)PayModeCols.TotCost].BackColor = System.Drawing.SystemColors.Control;
                        }

                        if (paymode.ID == "RC")
                        {
                            this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.RowCount - 1, (int)PayModeCols.TotCost].Locked = true;
                            this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.RowCount - 1, (int)PayModeCols.Memo].Locked = true;
                        }
                    }
                }
            }
            catch
            { }
            this.AddEvents();
        }
        
        /// <summary>
        /// 验证证件类别是否有效
        /// </summary>
        /// <param name="cardType">卡号</param>
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
        /// 设置界面默认值
        /// </summary>
        private void SetDefaultValue()
        {
            //默认挂号级别
            if (this.cmbRegLevel.alItems != null)
            {
                foreach (FS.FrameWork.Models.NeuObject obj in this.cmbRegLevel.alItems)
                {
                    if ((obj as FS.HISFC.Models.Registration.RegLevel).IsDefault)
                    {
                        this.cmbRegLevel.Tag = (obj as FS.HISFC.Models.Registration.RegLevel).ID;
                        break;
                    }
                }
            }
            //默认合同单位
            this.cmbPatientType.Tag = this.DefaultPactID;
        }

        /// <summary>
        /// 修改处方号
        /// </summary>
        private void ChangeRecipe()
        {
            this.tbRecipeNo.BorderStyle = BorderStyle.Fixed3D;
            this.tbRecipeNo.BackColor = SystemColors.Window;
            this.tbRecipeNo.ReadOnly = false;
            this.tbRecipeNo.ForeColor = SystemColors.WindowText;
            this.tbRecipeNo.Font = new Font("宋体", 10);
            this.tbRecipeNo.Location = new Point(381, 10);
            this.tbRecipeNo.Focus();
        }

        /// <summary>
        /// 保存处方记录
        /// </summary>
        /// <returns></returns>
        private int SaveRecipeNo()
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            try
            {
                this.conMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                FS.HISFC.Models.Base.Const con = new FS.HISFC.Models.Base.Const();
                con.ID = this.regMgr.Operator.ID;//操作员
                con.Name = this.tbRecipeNo.Text.Trim();//处方号
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
                    foreach (ToolStripMenuItem m in this.msTipsFp.Items)
                    {
                        if (m.Text == this.CHInput.LayoutName)
                        {
                            m.Checked = true;
                        }
                    }
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("获取挂号默认中文输入法出错!" + e.Message);
                return;
            }
        }

        /// <summary>
        /// 保存当前输入法
        /// </summary>
        private void saveInputLanguage()
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

        /// <summary>
        /// 不允许使用直接收费生成的号再进行挂号
        /// </summary>
        /// <param name="CardNO"></param>
        /// <returns></returns>
        private int ValidCardNO(string CardNO)
        {
            string cardRule = this.controlParams.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.NO_REG_CARD_RULES, false, "9");
            if (CardNO != "" && CardNO != string.Empty)
            {
                if (CardNO.Substring(0, 1) == cardRule && CardNO.Length == 10)
                {
                    return -1;
                }
            }
            return 1;
        }

        /// <summary>
        /// 显示合同单位支付比率
        /// </summary>
        /// <param name="pact"></param>
        private void getPayRate(FS.HISFC.Models.Base.PactInfo pact)
        {
            if (pact != null && pact.Rate.PayRate != 0)
            {
                decimal rate = pact.Rate.PayRate * 100;
                //this.lbTot.Text = rate.ToString("###") + "%";
            }
        }

        /// <summary>
        /// 得到患者应付
        /// </summary>		
        /// <returns></returns>
        private int getCost()
        {

            this.tbRealCost.Text = "";

            if (this.cmbRegLevel.Tag == null || this.cmbRegLevel.Tag.ToString() == "" ||
                this.cmbPatientType.Tag == null || this.cmbPatientType.Tag.ToString() == "")
            {
                return 0;
            }

            string regLvlID, pactID;
            decimal regfee = 0, chkfee = 0, digfee = 0, othfee = 0, owncost = 0, etccost = 0;

            regLvlID = this.cmbRegLevel.Tag.ToString();
            pactID = this.cmbPatientType.Tag.ToString();

            int rtn = this.GetRegFee(pactID, regLvlID, ref regfee, ref chkfee, ref digfee, ref othfee);

            if (rtn == -1) return 0;

            if (this.IsCountSpecialRegFee && this.ICountSpecialRegFee != null)
            {
                ICountSpecialRegFee.CountSpecialRegFee(this.dtpBirthday.Value, this.tbName.Text, this.tbIDNO.Text, ref regfee, ref digfee, ref othfee, ref this.regObj);
            }

            //获得患者应收、报销
            if (this.regObj == null || this.regObj.PID.CardNO == "")
            {
                this.RegFeeList = this.getCost(regfee, chkfee, digfee, ref othfee, ref owncost, ref etccost, "");
            }
            else
            {
                this.RegFeeList = this.getCost(regfee, chkfee, digfee, ref othfee, ref owncost, ref etccost, this.regObj.PID.CardNO);
            }

            this.setPayInfo();
            return 0;
        }        

        /// <summary>
        /// 设置支付信息
        /// </summary>
        /// <returns></returns>
        private int setPayInfo()
        {

            this.hsPayCost.Clear();
            //套餐金额
            hsPayCost.Add("TOT", 0.0m);
            //实际金额
            hsPayCost.Add("REAL", 0.0m);
            //赠送金额
            hsPayCost.Add("GIFT", 0.0m);
            //实际的收入
            hsPayCost.Add("ACTU", 0.0m);
            //优惠金额
            hsPayCost.Add("ETC", 0.0m);
            //四舍五入位
            hsPayCost.Add("ROUND", 0.0m);

            //此处调用时，GIFT_COST应该是为零的，费用发生变化的时候支付方式清零，折扣清零
            foreach (HISFC.Models.Registration.RegisterFeeDetail detail in this.RegFeeList)
            {
                hsPayCost["TOT"] = (decimal)hsPayCost["TOT"] + detail.Tot_cost;
                hsPayCost["REAL"] = (decimal)hsPayCost["REAL"] + detail.Real_cost;
                hsPayCost["ETC"] = (decimal)hsPayCost["ETC"] + detail.Etc_cost;
            }

            //支付方式清空
            foreach (Row row in this.fpPayMode_Sheet1.Rows)
            {
                FS.HISFC.Models.Base.Const cst = row.Tag as FS.HISFC.Models.Base.Const;

                if (hsPayCost != null && hsPayCost.ContainsKey("ETC") && cst.ID == "RC")
                {
                    this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value = Decimal.Parse(hsPayCost["ETC"].ToString());
                }
            }

            this.setCostInfoAfterEdit();
            this.setCostDisplay();
            return 1;
        }

        /// <summary>
        /// 在修改支付金额之后重新设置支付信息
        /// </summary>
        private decimal setCostInfoAfterEdit()
        {
            //当前现金消费金额
            decimal CashCost = 0.0m;
            int CashRow = -1;

            if (this.hsPayCost == null || this.hsPayCost.Keys.Count == 0)
            {
                this.hsPayCost = new Hashtable();
                //套餐金额
                hsPayCost.Add("TOT", 0.0m);
                //实际金额
                hsPayCost.Add("REAL", 0.0m);
                //实际的收入
                hsPayCost.Add("ACTU", 0.0m);
                //赠送金额
                hsPayCost.Add("GIFT", 0.0m);
                //优惠金额
                hsPayCost.Add("ETC", 0.0m);
                //四舍五入位
                hsPayCost.Add("ROUND", 0.0m);
            }
            else
            {
                //实际的收入
                hsPayCost["ACTU"] = 0.0m;
                //赠送金额
                hsPayCost["GIFT"] = 0.0m;
                //四舍五入位
                hsPayCost["ROUND"] = 0.0m;
            }

            foreach (Row row in this.fpPayMode_Sheet1.Rows)
            {
                ////账户支付
                //if (row.Tag is FS.HISFC.Models.Account.AccountDetail)
                //{
                //    if (this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.Account].Value != null)
                //    {
                //        this.hsPayCost["ACTU"] = (decimal)this.hsPayCost["ACTU"] + Decimal.Parse(this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.Account].Value.ToString());
                //    }
                //    if (this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.Gift].Value != null)
                //    {
                //        this.hsPayCost["GIFT"] = (decimal)this.hsPayCost["GIFT"] + Decimal.Parse(this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.Gift].Value.ToString());
                //    }
                //}

                //if (row.Tag is FS.HISFC.Models.Base.Const)
                //{
                    
                //}

                //if (this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value != null)
                //{
                //    this.hsPayCost["ACTU"] = (decimal)this.hsPayCost["ACTU"] + Decimal.Parse(this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value.ToString());
                //}

                //if ((row.Tag as FS.HISFC.Models.Base.Const).ID == "CA")
                //{
                //    CashCost = Decimal.Parse(this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value.ToString());
                //    CashRow = row.Index;
                //}

                FS.HISFC.Models.Base.Const curPayMode = row.Tag as FS.HISFC.Models.Base.Const;
                if (this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value != null)
                {
                    decimal cost = Decimal.Parse(this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value.ToString());

                    //赠送
                    if (curPayMode.ID == "DC")
                    {
                        this.hsPayCost["GIFT"] = (decimal)this.hsPayCost["GIFT"] + cost;
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

            //判断所有支付方式总额是否等于应缴金额
            decimal JugdeCost = 0.0m;
            JugdeCost = (decimal)this.hsPayCost["REAL"] - (decimal)this.hsPayCost["ACTU"] - (decimal)this.hsPayCost["GIFT"];


            //相等则无需调整现金金额
            if (JugdeCost == 0)
            {
                return JugdeCost;
            }

            //缴纳金额少于应缴金额,递归计算现金应缴金额
            if (JugdeCost + CashCost >= 0)
            {
                //设置现金金额 = 原现金金额 + 少缴金额
                decimal realCash = CashCost + JugdeCost;
                this.fpPayMode_Sheet1.Cells[CashRow, (int)PayModeCols.TotCost].Value = realCash;
                return this.setCostInfoAfterEdit();
            }
            else  //缴纳金额大于应缴金额,发生此种情况只可能单笔押金大于总额
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
        /// 将费用信息显示在界面上
        /// </summary>
        private void setCostDisplay()
        {
            this.tbRealCost.Text = ((decimal)hsPayCost["REAL"]).ToString("F2");
            this.tbEtcCost.Text = ((decimal)hsPayCost["ETC"]).ToString("F2");
            this.tbTotCost.Text = ((decimal)hsPayCost["TOT"]).ToString("F2");
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
            if (p.ID == null || p.ID == "")
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
                    //不收取诊金、挂号
                    regFee = 0;
                    chkFee = 0;
                    digFee = 0;
                    break;
                default:
                    // 默认都收取
                    break;
            }

            //{2E41B9BF-6B67-4b56-BD54-A836CE09F52B}
            if (this.IsJudgePackageFee)
            {
                regFee = 0;
                chkFee = 0;
                digFee = 0;
                othFee = 0;
            }

            //{D3268012-7646-4c69-943C-CF8487AB7997}
            if (this.IsDocterChargeFee)
            {
                regFee = 0;
                chkFee = 0;
                digFee = 0;
                othFee = 0;
            }

            return 0;
        }
        
        /// <summary>
        /// 获得患者应交金额、报销金额
        /// </summary>
        /// <param name="regFee"></param>
        /// <param name="chkFee"></param>
        /// <param name="digFee"></param>
        /// <param name="othFee"></param>
        /// <param name="ownCost"></param>
        /// <param name="etcCost"></param>
        /// <param name="cardNo"></param>		
        private ArrayList getCost(decimal regFee, decimal chkFee, decimal digFee, ref decimal othFee, ref decimal ownCost, ref decimal etcCost, string cardNo)
        {
            ArrayList lstRegFee = new ArrayList();
            HISFC.Models.Registration.RegisterFeeDetail regFeeDetail = null;

            if (regFee > 0)
            {
                regFeeDetail = BuildRegFeeInfo(AccCardFeeType.RegFee, regFee, 0);
                lstRegFee.Add(regFeeDetail);
            }
            else if (regFee == 0 && this.IsPrintIfZero)
            {
                regFeeDetail = BuildRegFeeInfo(AccCardFeeType.RegFee, regFee, 0);
                lstRegFee.Add(regFeeDetail);
            }

            if (chkFee > 0)
            {
                regFeeDetail = BuildRegFeeInfo(AccCardFeeType.ChkFee, chkFee, 0);
                lstRegFee.Add(regFeeDetail);
            }

            if (digFee > 0)
            {
                regFeeDetail = BuildRegFeeInfo(AccCardFeeType.DiaFee, digFee, 0);
                lstRegFee.Add(regFeeDetail);
            }

            // 其他费用
            if (othFee > 0)
            {
                regFeeDetail = BuildRegFeeInfo(AccCardFeeType.OthFee, othFee, 0);
                lstRegFee.Add(regFeeDetail);
            }

            if (chbCardFee.Visible && chbCardFee.Checked)
            {
                regFeeDetail = BuildRegFeeInfo(AccCardFeeType.CardFee, 3000, 0);
                lstRegFee.Add(regFeeDetail);
            }

            ownCost = regFee + chkFee + digFee + othFee;
            etcCost = 0;
            return lstRegFee;
        }
        
        /// <summary>
        /// 创建收费条目
        /// </summary>
        /// <returns></returns>
        private HISFC.Models.Registration.RegisterFeeDetail BuildRegFeeInfo(AccCardFeeType feeType, decimal ownCost, decimal etcCost)
        {
            HISFC.Models.Registration.RegisterFeeDetail regFeeDetail = new HISFC.Models.Registration.RegisterFeeDetail();
            regFeeDetail.Memo = feeType.ToString();
            regFeeDetail.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
            regFeeDetail.Real_cost = ownCost;
            regFeeDetail.Gift_cost = 0.0m;
            regFeeDetail.Etc_cost = etcCost;
            regFeeDetail.Tot_cost = ownCost + etcCost;

            if (regFeeDetail.FeeItem == null)
            {
                regFeeDetail.FeeItem = new FS.HISFC.Models.Base.Item();
            }

            switch (feeType)
            {
                case AccCardFeeType.RegFee:
                    regFeeDetail.FeeItem.Name = "挂号费";
                    break;
                case AccCardFeeType.DiaFee:
                    regFeeDetail.FeeItem.Name = "诊金";
                    break;
                case AccCardFeeType.CardFee:
                    regFeeDetail.FeeItem.Name = "卡费";
                    break;
                case AccCardFeeType.CaseFee:
                    regFeeDetail.FeeItem.Name = "病历本费";
                    break;
                case AccCardFeeType.ChkFee:
                    regFeeDetail.FeeItem.Name = "检查费";
                    break;
                case AccCardFeeType.AirConFee:
                    regFeeDetail.FeeItem.Name = "空调费";
                    break;
                case AccCardFeeType.OthFee:
                    regFeeDetail.FeeItem.Name = "其他费";
                    break;
                default:
                    break;
            }

            return regFeeDetail;
        }

        /// <summary>
        /// 设置相应挂号信息(模板,已挂,剩余等信息)
        /// </summary>
        private void queryRegLevl()
        {
            //恢复初始状态
            this.cmbDept.Tag = "";
            this.cmbDoctor.Tag = "";
            this.lbSchemaTips.Text = "";
            if (this.ucChooseDate.Visible) this.ucChooseDate.Visible = false;

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
                    this.setDeptFpStyle(false);
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
                    this.addDoctToDataSet(al);
                    this.addDoctToFp(true);
                    this.cmbDoctor.AddItems(al);
                }
                else
                {
                    //专家号直接选择医生,不跳到科室处,生成全部门诊科室列表
                    this.setDeptFpStyle(false);
                    //this.addClinicDeptsToDataSet(this.alDept);
                    //this.cmbDept.AddItems(this.alDept);
                    this.GetSchemaDept(FS.HISFC.Models.Base.EnumSchemaType.Doct, Level);
                    if (!this.isAddAllDept)
                    {
                        this.addRegDeptToCombox();
                    }
                    else
                    {
                        this.cmbDept.AddItems(this.alDept);
                    }
                    this.addRegDeptToFp(false);
                    this.GetDoct(Level);//获得全部出诊医生
                }
                #endregion
            }
            else if (Level.IsFaculty)//专科
            {
                #region 专科
                //获取出诊专科列表
                this.setDeptFpStyle(true);
                this.GetSchemaDept(FS.HISFC.Models.Base.EnumSchemaType.Dept, Level);
                this.addRegDeptToFp(true);
                //生成Combox科室下拉列表
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
                this.addDoctToDataSet(al);
                this.addDoctToFp(false);
                this.cmbDoctor.AddItems(al);
                #endregion
            }
            else if (Level.IsEmergency)
            {
                //显示科室列表
                this.setDeptFpStyle(false);
                if (this.alAllowedEmergDept != null && this.alAllowedEmergDept.Count > 0)
                {
                    this.addAllowedDeptToDataSet(this.alAllowedEmergDept);
                    this.addRegDeptToCombox();
                }
                else//显示全部科室
                {
                    this.addClinicDeptsToDataSet(this.alEmergDept);
                    this.cmbDept.AddItems(this.alEmergDept);
                }
                this.addRegDeptToFp(false);
            }
            else//普通
            {
                //显示科室列表
                this.setDeptFpStyle(false);
                if (this.alAllowedDept != null && this.alAllowedDept.Count > 0)
                {
                    this.addAllowedDeptToDataSet(this.alAllowedDept);
                    this.addRegDeptToCombox();
                }
                else//显示全部科室
                {
                    this.addClinicDeptsToDataSet(this.alDept);
                    this.cmbDept.AddItems(this.alDept);
                }
                this.addRegDeptToFp(false);
            }

            if (this.fpDoctList.RowCount == 0 && this.fpDeptList.RowCount == 0)
            {
                this.lbSchemaTips.Text = "该级别当日无排班";
                this.lbSchemaTips.Visible = true;
            }
            else
            {
                this.lbSchemaTips.Visible = false;
            }
                
            this.dtSeeDate.Tag = null;
        }
        
        /// <summary>
        /// 获取出诊科室
        /// </summary>
        /// <param name="type"></param>
        /// <param name="regLevel"></param>
        /// <returns></returns>
        private int GetSchemaDept(FS.HISFC.Models.Base.EnumSchemaType type, FS.HISFC.Models.Registration.RegLevel regLevel)
        {
            DataSet ds = new DataSet();
            if (!this.IsJudgeReglevl)
            {
                ds = this.SchemaMgr.QueryDept(this.dtSeeDate.Value.Date, this.regMgr.GetDateTimeFromSysDateTime(), type);
            }
            else
            {
                ds = this.SchemaMgr.QueryDept(this.dtSeeDate.Value.Date,this.regMgr.GetDateTimeFromSysDateTime(), type, regLevel.ID);
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
        
        /// <summary>
        /// 获取当日出诊全部医生
        /// </summary>
        /// <returns></returns>
        private int GetDoct(FS.HISFC.Models.Registration.RegLevel Level)
        {
            DataSet ds;
            if (!this.IsJudgeReglevl)
            {
                ds = this.SchemaMgr.QueryDoct(this.dtSeeDate.Value, this.regMgr.GetDateTimeFromSysDateTime());
            }
            else
            {
                ds = this.SchemaMgr.QueryDoctByRegLevel(this.dtSeeDate.Value, this.regMgr.GetDateTimeFromSysDateTime(), Level.ID);
            }
            if (ds == null)
            {
                MessageBox.Show(this.SchemaMgr.Err, "提示");
                return -1;
            }

            this.setDoctFpStyle(true);
            this.addDoctToDataSet(ds);
            this.addDoctToFp(true);

            if (this.isAddAllDoct)
            {
                this.cmbDoctor.AddItems(this.alDoct);
            }
            else
            {
                this.addDoctToCombox();
            }

            return 0;
        }
        
        /// <summary>
        /// add doctor to combox
        /// </summary>
        private void addDoctToCombox()
        {
            DataRow row;
            ArrayList al = new ArrayList();

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

                al.Add(p);
            }

            this.cmbDoctor.AddItems(al);
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
                    ds = this.SchemaMgr.QueryDoct(this.dtSeeDate.Value,this.regMgr.GetDateTimeFromSysDateTime(), deptID, regLevel.ID);
                }
                else
                {
                    ds = this.SchemaMgr.QueryDoct(this.dtSeeDate.Value,this.regMgr.GetDateTimeFromSysDateTime(), deptID);
                }
                if (ds == null)
                {
                    MessageBox.Show(this.SchemaMgr.Err, "提示");
                    return -1;
                }
                this.setDoctFpStyle(true);
                this.addDoctToDataSet(ds);
                this.addDoctToCombox();
            }
            else
            {
                if (this.isAddAllDoct)
                {
                    this.cmbDoctor.AddItems(this.alDoct);
                    this.setDoctFpStyle(false);
                    this.addDoctToDataSet(this.alDoct);
                }
                else
                {
                    ArrayList al = this.conMgr.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D, deptID);
                    if (al == null)
                    {
                        MessageBox.Show("获取出诊医生时出错!" + this.conMgr.Err, "提示");
                        return -1;
                    }
                    this.cmbDoctor.AddItems(al);
                    this.setDoctFpStyle(false);
                    this.addDoctToDataSet(al);
                }
            }
            this.addDoctToFp(IsDisplaySchema);
            return 0;
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
            regType = this.getRegType(level);
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
                this.SetDefaultSeeDateTime(bookingDate.Date);
            }
            else
            {
                //如果当前时间，选择了排班，只有是转科号才提示是否继续，否则会造成重复提示
                if (string.IsNullOrEmpty(schema.Templet.ID) == false && level.IsFaculty)
                {
                    DateTime currentTime = CommonController.Instance.GetSystemTime();
                    if (schema.Templet.Begin.Date == currentTime.Date)
                    {
                        if (schema.Templet.Noon.ID.Equals(CommonController.Instance.GetNoonID(currentTime)) == false)
                        {
                            if (MessageBox.Show(this, "您选择的排班午别是：" + CommonController.Instance.GetNoonName(schema.Templet.Noon.ID) + "，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            {
                                this.SetDefaultSeeDateTime(currentTime);
                                this.dtSeeDate.Tag = null;
                                return -1;
                            }
                        }
                    }
                }
                this.SetSeeDateTime(schema);
            }
            return 0;
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
            regType = this.getRegType(level);

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
                this.SetDefaultSeeDateTime(bookingDate.Date);
            }
            else
            {
                //如果当前时间，选择了排班，只有专家或者特诊号才进行提示
                if (string.IsNullOrEmpty(schema.Templet.ID) == false && level.IsExpert && !level.IsSpecial)
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
                                this.SetDefaultSeeDateTime(currentTime);
                                this.dtSeeDate.Tag = null;
                                return -1;
                            }
                        }
                    }
                }
                this.cmbDept.Tag = schema.Templet.Dept.ID;
                this.SetSeeDateTime(schema);
            }
            return 0;
        }
        
        /// <summary>
        /// 挂号职级与医生职级提示
        /// </summary>
        private void WarningDoctLevel()
        {
            if (string.IsNullOrEmpty(this.cmbRegLevel.Text) || string.IsNullOrEmpty(this.cmbDoctor.Text))
            {
                return;
            }
            string reglevlCode = "";

            //DateTime dtNow = regFeeMgr.GetDateTimeFromSysDateTime();

            //获取医生排班
            FS.HISFC.Models.Registration.Schema schema = SchemaMgr.GetSchema(this.cmbDoctor.Tag.ToString(), this.dtBegin.Value);

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
        /// 设置默认情况下,就诊安排时间段显示
        /// </summary>
        /// <param name="seeDate"></param>
        private void SetDefaultSeeDateTime(DateTime seeDate)
        {
            FS.HISFC.Models.Registration.Schema schema = new FS.HISFC.Models.Registration.Schema();
            schema.Templet.Begin = seeDate.Date;
            schema.Templet.End = seeDate.Date;
            this.SetSeeDateTime(schema);
            this.dtSeeDate.Tag = null;
        }
        
        /// <summary>
        /// 设置就诊时间段
        /// </summary>
        /// <param name="schema"></param>
        private void SetSeeDateTime(FS.HISFC.Models.Registration.Schema schema)
        {
            this.dtBegin.Value = schema.Templet.Begin;
            this.dtEnd.Value = schema.Templet.End;
            this.dtSeeDate.Tag = schema;
        }

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
            ArrayList al = this.radtProcess.QueryPatientByName(regInfo.Name);
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
            pInfo = this.radtProcess.QueryComPatientInfo(regInfo.PID.CardNO);
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

                if (this.radtProcess.RegisterComPatient(p) == -1)
                {
                    Err = this.radtProcess.Err;
                    return -1;
                }
            }

            return 0;
        }
        
        /// <summary>
        /// 根据精度计算小数位
        /// </summary>
        /// <param name="oldDecimal"></param>
        /// <returns></returns>
        private decimal calculateDecimal(decimal oldDecimal)
        {
            decimal ShouldDecimal = oldDecimal;
            decimal RealDecimal = 0.0m;

            //处理四舍五入
            if (RoundControl < 3)
            {
                //保留0,1,2位小数
                RealDecimal = Math.Round(ShouldDecimal, RoundControl, MidpointRounding.AwayFromZero);
            }
            else if (roundControl == 3)
            {
                //下取整
                RealDecimal = Math.Floor(ShouldDecimal);
            }
            else
            {
                //上取整
                RealDecimal = Math.Ceiling(ShouldDecimal);
            }

            return RealDecimal;
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
                if (this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value != null &&
                    Double.Parse(this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value.ToString()) > 0)
                {
                    FS.HISFC.Models.Base.Const cst = this.fpPayMode_Sheet1.Rows[row.Index].Tag as FS.HISFC.Models.Base.Const;
                    if (cst.ID != "YS" && cst.ID != "DC")
                    {
                        FS.HISFC.Models.Registration.RegisterPayMode payMode = new FS.HISFC.Models.Registration.RegisterPayMode();
                        payMode.Mode_Code = cst.ID;
                        payMode.CancelFlag = 1;
                        payMode.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                        payMode.Tot_cost = payMode.Real_cost = (decimal)this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value;
                        payMode.Memo = this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.Memo].Text;

                        //现金支付需要进行四舍五入
                        if (payMode.Mode_Code == "CA")
                        {
                            decimal tmp = FS.HISFC.BizProcess.Integrate.Function.calculateDecimal(payMode.Tot_cost);
                            payMode.Tot_cost = payMode.Real_cost = tmp;

                        }

                        if (payMode.Tot_cost > 0)
                        {
                            paymodeList.Add(payMode);
                        }
                    }
                }
            }

            //此处要加上代付和会员支付的费用
            paymodeList.AddRange(this.accountPayList);
            paymodeList.AddRange(this.giftPayList);
            paymodeList.AddRange(this.empowerAccountPayList);
            paymodeList.AddRange(this.empowerGiftPayList);

            return paymodeList;
        }

        #region 菜单

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// 初始化工具栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("清屏", "清屏", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
            toolBarService.AddToolButton("折扣优惠", "折扣优惠", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S手动录入, true, false, null);
            toolBarService.AddToolButton("重打票据", "重打票据", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            toolBarService.AddToolButton("补打票据", "补打票据", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            toolBarService.AddToolButton("刷卡", "刷卡", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S手动录入, true, false, null);
            toolBarService.AddToolButton("套餐查询", "套餐查询", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T套餐, true, false, null);
            return toolBarService;
        }

        /// <summary>
        /// 按键处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "清屏":
                    this.Clear();
                    break;
                case "折扣优惠":
                    this.DisAccount();
                    break;
                case "重打票据":
                    this.Reprint();
                    break;
                case "补打票据":
                    this.OnlyPrint();
                    break;
                case "刷卡":
                    string cardNo = "";
                    string error = "";
                    if (Function.OperMCard(ref cardNo, ref error) == -1)
                    {
                        CommonController.Instance.MessageBox(error, MessageBoxIcon.Error);
                        return;
                    }
                    tbQueryNO.Text = ";" + cardNo;
                    tbQueryNO.Focus();
                    this.tbQueryNO_KeyDown(null, new KeyEventArgs(Keys.Enter));
                    break;
                case "套餐查询":
                    this.PackageQuery();
                    break;
                default:
                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        #endregion


        protected override int OnSave(object sender, object neuObject)
        {
            this.DelEvents();
            if (this.Save() == -1)
            {
                this.AddEvents();
                this.cmbRegLevel.Focus();
            }
            else
            {
                //因为clear里面进行了delevent操作，所以这里必须先恢复
                this.AddEvents();
                this.Clear(); 
            }
            return base.OnSave(sender, neuObject);
        }

        /// <summary>
        /// 打印挂号发票
        /// </summary>
        /// <param name="regObj"></param>
        private void Print(FS.HISFC.Models.Registration.Register regObj, FS.HISFC.BizLogic.Registration.Register regmr)
        {
            FS.HISFC.BizProcess.Interface.Registration.IRegPrint regprint = null;
            regprint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Registration.IRegPrint)) as FS.HISFC.BizProcess.Interface.Registration.IRegPrint;
            if (regprint == null)
            {
                MessageBox.Show(FS.FrameWork.WinForms.Classes.UtilInterface.Err, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (regObj.IsEncrypt)
            {
                regObj.Name = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(this.regObj.NormalName);
            }
            if (this.isATMPrint)
                regObj.InputOper.ID = regMgr.Operator.ID;
            regprint.SetPrintValue(regObj);
            regprint.Print();
        }

        /// <summary>
        /// 套餐折扣
        /// </summary>
        private void DisAccount()
        {
            this.DelEvents();
            this.getCost();
            if (this.RegFeeList == null || this.RegFeeList.Count == 0)
            {
                this.AddEvents();
                MessageBox.Show("没有需要进行折扣的费用！");
                return;
            }

            Forms.frmDiscount tmp = new FS.HISFC.Components.Registration.Forms.frmDiscount();
            tmp.RegFeeInfo = this.RegFeeList;

            if (tmp.Init())
            {
                tmp.ShowDialog();
            }
            else
            {
                this.AddEvents();
                MessageBox.Show("初始化折扣窗体处错！");
                return;
            }

            //支付方式清空
            foreach (Row row in this.fpPayMode_Sheet1.Rows)
            {
                this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value = 0;
                this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.Memo].Value = 0;
            }

            this.tbAccount.Text = "0.0";
            this.tbPowerAmount.Text = "0.0";

            this.accountPayList.Clear();
            this.giftPayList.Clear();
            this.empowerAccountPayList.Clear();
            this.empowerGiftPayList.Clear();

            this.setPayInfo();
            this.AddEvents();
        }

        /// <summary>
        /// 重打
        /// </summary>
        private void Reprint()
        {
            string Err = "";

            int row = this.fpRegList.ActiveRowIndex;
            FS.HISFC.Models.Registration.Register obj;

            if (IsATMPrint)
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

            //{E4CB6714-9AA1-4228-AE94-ACAF32EDFACA}
            FS.HISFC.BizLogic.Registration.RegDetail detailMgr = new FS.HISFC.BizLogic.Registration.RegDetail();
            FS.HISFC.BizLogic.Registration.RegPayMode payModeMgr = new FS.HISFC.BizLogic.Registration.RegPayMode();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();


            try
            {
                accountFeeMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.regMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.feeMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.radtProcess.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                detailMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                payModeMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                DateTime current = this.regMgr.GetDateTimeFromSysDateTime();
                //查找对应的发票信息
                //List<FS.HISFC.Models.Account.AccountCardFee> cardFeeList = new List<AccountCardFee>();
                //if (accountFeeMgr.QueryAccCardFeeByClinic(obj.PID.CardNO, obj.ID, out cardFeeList) == -1 || cardFeeList == null)
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack();
                //    MessageBox.Show("取发票信息失败，原因：" + accountFeeMgr.Err, "提示");
                //    return;
                //}

                //if (cardFeeList.Count == 0)
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack();
                //    MessageBox.Show("没有需要重打发票信息", "提示");
                //    return;
                //}

                List<FS.HISFC.Models.Registration.RegisterFeeDetail> detailList = detailMgr.QueryDetailByInvoiceNO(obj.InvoiceNO);
                List<FS.HISFC.Models.Registration.RegisterPayMode> paymodeList = payModeMgr.QueryDetailByInvoiceNO(obj.InvoiceNO);

                if (detailList == null || paymodeList == null || detailList.Count == 0 || paymodeList.Count == 0)
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
                    strInvioceNO = this.tbRecipeNo.Text.Trim().PadLeft(12, '0');
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
                //List<FS.HISFC.Models.Account.AccountCardFee> listReprint = new List<AccountCardFee>();
                //foreach (FS.HISFC.Models.Account.AccountCardFee accountCardFee in cardFeeList)
                //{
                //    if (accountCardFee.TransType == FS.HISFC.Models.Base.TransTypes.Negative)
                //    {
                //        FS.FrameWork.Management.PublicTrans.RollBack();
                //        MessageBox.Show("发票已作废信息，不允许重打", "提示");
                //        return;
                //    }

                //    if (accountCardFee.IStatus != 1)
                //    {
                //        FS.FrameWork.Management.PublicTrans.RollBack();
                //        MessageBox.Show("发票已退费，不允许重打", "提示");
                //        return;
                //    }
                //    //生成负记录
                //    FS.HISFC.Models.Account.AccountCardFee returnCardFee = accountCardFee.Clone();
                //    returnCardFee.FeeOper.ID = accountFeeMgr.Operator.ID;
                //    returnCardFee.FeeOper.OperTime = current;
                //    returnCardFee.Oper.ID = accountFeeMgr.Operator.ID;
                //    returnCardFee.Oper.ID = accountFeeMgr.Operator.ID;
                //    returnCardFee.Oper.OperTime = current;
                //    returnCardFee.PayType.ID = accountCardFee.PayType.ID;
                //    returnCardFee.PayType.Memo = accountCardFee.PayType.Memo;
                //    returnCardFee.PayType.Name = accountCardFee.PayType.Name;
                //    returnCardFee.Tot_cost = -returnCardFee.Tot_cost;
                //    returnCardFee.Own_cost = -returnCardFee.Own_cost;
                //    returnCardFee.Pub_cost = -returnCardFee.Pub_cost;
                //    returnCardFee.Pay_cost = -returnCardFee.Pay_cost;
                //    returnCardFee.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                //    returnCardFee.IStatus = 2;

                //    if (accountFeeMgr.InsertAccountCardFee(returnCardFee) <= 0)
                //    {
                //        FS.FrameWork.Management.PublicTrans.RollBack();
                //        MessageBox.Show("插入退费信息失败，原因：" + accountFeeMgr.Err, "提示");
                //        return;
                //    }

                //    //生成新纪录
                //    FS.HISFC.Models.Account.AccountCardFee newCardFee = accountCardFee.Clone();

                //    newCardFee.InvoiceNo = strInvioceNO;
                //    newCardFee.Print_InvoiceNo = strRealInvoiceNO;
                //    newCardFee.FeeOper.ID = accountFeeMgr.Operator.ID;
                //    newCardFee.FeeOper.OperTime = current;
                //    newCardFee.Oper.ID = accountFeeMgr.Operator.ID;
                //    newCardFee.Oper.ID = accountFeeMgr.Operator.ID;
                //    newCardFee.Oper.OperTime = current;
                //    newCardFee.PayType.ID = accountCardFee.PayType.ID;
                //    newCardFee.PayType.Memo = accountCardFee.PayType.Memo;
                //    newCardFee.PayType.Name = accountCardFee.PayType.Name;
                //    newCardFee.IStatus = 1;
                //    newCardFee.TransType = FS.HISFC.Models.Base.TransTypes.Positive;

                //    if (accountFeeMgr.InsertAccountCardFee(newCardFee) <= 0)
                //    {
                //        FS.FrameWork.Management.PublicTrans.RollBack();
                //        MessageBox.Show("插入收费信息失败，原因：" + accountFeeMgr.Err, "提示");
                //        return;
                //    }

                //    //原始记录退费

                //    if (accountFeeMgr.CancelAccountCardFee(accountCardFee.InvoiceNo, FS.HISFC.Models.Base.TransTypes.Positive, accountCardFee.FeeType) <= 0)
                //    {
                //        FS.FrameWork.Management.PublicTrans.RollBack();
                //        MessageBox.Show("作废收费信息失败，原因：" + accountFeeMgr.Err, "提示");
                //        return;
                //    }
                //}
                #endregion

                #region 处理挂号明细
                //{E4CB6714-9AA1-4228-AE94-ACAF32EDFACA}
                foreach (FS.HISFC.Models.Registration.RegisterFeeDetail detail in detailList)
                {
                    //更新旧记录
                    detail.CancelFlag = 2;
                    detail.CancelOper.ID = FS.FrameWork.Management.Connection.Operator.ID;
                    detail.CancelOper.OperTime = detailMgr.GetDateTimeFromSysDateTime();
                    if (detailMgr.Update(detail) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("作废费用明细信息失败，原因：" + detailMgr.Err, "提示");
                        return;
                    }

                    //插入新负纪录
                    detail.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                    detail.CancelFlag = 2;
                    detail.Tot_cost = -detail.Tot_cost;
                    detail.Real_cost = -detail.Real_cost;
                    detail.Gift_cost = -detail.Gift_cost;
                    detail.Etc_cost = -detail.Etc_cost;
                    detail.CancelOper.ID = FS.FrameWork.Management.Connection.Operator.ID;
                    detail.CancelOper.OperTime = current;
                    detail.BalanceOper = new FS.HISFC.Models.Base.OperEnvironment();
                    detail.BalanceNo = "";
                    if (detailMgr.Insert(detail) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("作废费用明细信息失败，原因：" + detailMgr.Err, "提示");
                        return;
                    }

                    //插入新记录
                    detail.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                    detail.CancelFlag = 1;
                    detail.Tot_cost = -detail.Tot_cost;
                    detail.Real_cost = -detail.Real_cost;
                    detail.Gift_cost = -detail.Gift_cost;
                    detail.Etc_cost = -detail.Etc_cost;
                    detail.CancelOper.ID = "";
                    detail.CancelOper.OperTime = System.DateTime.MinValue;
                    detail.BalanceOper = new FS.HISFC.Models.Base.OperEnvironment();
                    detail.BalanceNo = "";
                    detail.InvoiceNo = strInvioceNO;
                    detail.Print_InvoiceNo = strRealInvoiceNO;
                    if (detailMgr.Insert(detail) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("作废费用明细信息失败，原因：" + detailMgr.Err, "提示");
                        return;
                    }
                }

                #endregion

                #region 处理支付方式

                foreach (FS.HISFC.Models.Registration.RegisterPayMode paymode in paymodeList)
                {
                    //更新旧记录
                    paymode.CancelFlag = 2;
                    paymode.CancelOper.ID = FS.FrameWork.Management.Connection.Operator.ID;
                    paymode.CancelOper.OperTime = current;
                    if (payModeMgr.Update(paymode) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("操作失败！" + payModeMgr.Err), "提示");
                        return ;
                    }

                    //插入新负纪录
                    paymode.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                    paymode.CancelFlag = 2;
                    paymode.Tot_cost = paymode.Real_cost = -paymode.Real_cost;
                    paymode.CancelOper.ID = FS.FrameWork.Management.Connection.Operator.ID;
                    paymode.CancelOper.OperTime = current;
                    paymode.BalanceOper = new FS.HISFC.Models.Base.OperEnvironment();
                    paymode.BalanceNo = "";
                    if (payModeMgr.Insert(paymode) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("操作失败！" + payModeMgr.Err), "提示");
                        return;
                    }

                    //插入新记录
                    paymode.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                    paymode.CancelFlag = 1;
                    paymode.Tot_cost = paymode.Real_cost = -paymode.Real_cost;
                    paymode.CancelOper.ID = "";
                    paymode.CancelOper.OperTime = System.DateTime.MinValue;
                    paymode.BalanceOper = new FS.HISFC.Models.Base.OperEnvironment();
                    paymode.BalanceNo = "";
                    paymode.InvoiceNo = strInvioceNO;
                    paymode.Print_InvoiceNo = strRealInvoiceNO;
                    if (payModeMgr.Insert(paymode) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("操作失败！" + payModeMgr.Err), "提示");
                        return;
                    }
                }

                #endregion

                #region 处理挂号记录

                obj.InvoiceNO = strInvioceNO;
                obj.RecipeNO = this.tbRecipeNo.Text.Trim().PadLeft(12, '0');
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
                    string invoiceStytle = controlParams.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.GET_INVOICE_NO_TYPE, false, "0");
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
                if (this.UpdatePatientinfo(obj, this.radtProcess, this.regMgr, ref Err) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Err, "提示");
                    return;
                }

                FS.FrameWork.Management.PublicTrans.Commit();

                //最后加处方号,防止跳号
                //this.UpdateRecipeNo(1);
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "提示");
                return;
            }

            this.Print(obj, this.regMgr);

            this.SetRegList();
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
        /// 打印条码
        /// </summary>
        /// <param name="register"></param>
        private void PrintBarCode(FS.HISFC.Models.Registration.Register register)
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

        /// <summary>
        /// 套餐查询
        /// </summary>
        private void PackageQuery()
        {
            if (this.regObj == null || string.IsNullOrEmpty(this.regObj.PID.CardNO))
            {
                MessageBox.Show("请先检索患者！");
                return;
            }

            FS.HISFC.Components.Common.Forms.frmPackageQuery frmpackage = new FS.HISFC.Components.Common.Forms.frmPackageQuery();
            frmpackage.PatientInfo = accountMgr.GetPatientInfoByCardNO(this.regObj.PID.CardNO);
            frmpackage.ShowDialog();
        }

        /// <summary>
        /// 有效性验证
        /// </summary>
        /// <returns></returns>
        private int valid()
        {
            this.tbQueryNO.Focus();

            if (this.tbRecipeNo.Text.Trim() == "")
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

            if (this.dtSeeDate.Tag == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("专家号或专科号排班为空，请重新选择！"), "提示");
                this.cmbDoctor.Focus();
                return -1;
            }

            if ((level.IsExpert) && (this.cmbDoctor.Tag == null || this.cmbDoctor.Tag.ToString() == ""))
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("专家号必须指定看诊医生"), "提示");
                this.cmbDoctor.Focus();
                return -1;
            }
            else if (level.IsFaculty)
            {
                if (this.dtSeeDate.Tag == null)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("专家号或专科号排班为空，请重新选择！"), "提示");
                    this.cmbDept.Focus();
                    return -1;
                }
            }

            if (this.regObj == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("请录入挂号患者"), "提示");
                this.tbQueryNO.Focus();
                return -1;
            }

            if (string.IsNullOrEmpty(this.regObj.PID.CardNO) == true)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("请录入病历号"), "提示");
                this.tbQueryNO.Focus();
                return -1;
            }

            if (this.tbName.Text.Trim() == "")
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("请输入患者姓名"), "提示");
                this.tbName.Focus();
                return -1;
            }

            if (this.dtBegin.Value.TimeOfDay > this.dtEnd.Value.TimeOfDay)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("挂号开始时间不能大于结束时间"), "提示");
                this.dtEnd.Focus();
                return -1;
            }

            if (FS.FrameWork.Public.String.ValidMaxLengh(this.tbName.Text.Trim(), 40) == false)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("患者名称最多可录入20个汉字"), "提示");
                this.tbName.Focus();
                return -1;
            }

            if (this.cmbGender.Tag == null || this.cmbGender.Tag.ToString() == "")
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("请选择患者性别"), "提示");
                this.cmbGender.Focus();
                return -1;
            }

            if (this.cmbPatientType.Tag == null || this.cmbPatientType.Tag.ToString() == "")
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("患者结算类别不能为空"), "提示");
                this.cmbPatientType.Focus();
                return -1;
            }

            if (FS.FrameWork.Public.String.ValidMaxLengh(this.tbPhone.Text.Trim(), 30) == false)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("联系电话最多可录入20位数字"), "提示");
                this.tbPhone.Focus();
                return -1;
            }
            if (FS.FrameWork.Public.String.ValidMaxLengh(this.tbAddress.Text.Trim(), 60) == false)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("联系人地址最多可录入30个汉字"), "提示");
                this.tbAddress.Focus();
                return -1;
            }
            if (FS.FrameWork.Public.String.ValidMaxLengh(this.tbCardNO.Text, 10) == false)
            {
                MessageBox.Show("病历号输入有问题,请核对病历号!", "提示");
                this.tbCardNO.Focus();
                return -1;
            }

            if (FS.FrameWork.Public.String.ValidMaxLengh(this.tbAge.Text.Trim(), 3) == false)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("年龄最多可录入3位数字"), "提示");
                this.tbAge.Focus();
                return -1;
            }

            if (this.tbAge.Text.Trim().Length > 0)
            {
                try
                {
                    int age = int.Parse(this.tbAge.Text.Trim());
                    if (age <= 0)
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("年龄不能为负数"), "提示");
                        this.tbAge.Focus();
                        return -1;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("年龄录入格式不正确!" + e.Message, "提示");
                    this.tbAge.Focus();
                    return -1;
                }
            }
            DateTime current = this.regMgr.GetDateTimeFromSysDateTime().Date;
            if (this.dtpBirthday.Value.Date > current)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("出生日期不能大于当前时间"), "提示");
                this.dtpBirthday.Focus();
                return -1;
            }

            //校验合同单位
            if (this.ValidCombox(FS.FrameWork.Management.Language.Msg("您选择的合同单位有误或不在合同单位的下拉列表中,请重新选择")) < 0)
            {
                this.cmbPatientType.Focus();
                return -1;
            }

            //不选证件类型直接录入身份证号大于18位时报错
            if (this.cmbCardType.Tag == null || string.IsNullOrEmpty(this.cmbCardType.Tag.ToString()))
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("请选择证件类型!"), "提示");
                this.cmbCardType.Focus();
                return -1;
            }

            if (cmbCardType.SelectedItem != null && cmbCardType.SelectedItem.Name == "身份证")
            {
                //校验身份证号
                if (!string.IsNullOrEmpty(this.tbIDNO.Text))
                {
                    int reurnValue = this.ProcessIDENNO(this.tbIDNO.Text, EnumCheckIDNOType.Saveing);
                    if (reurnValue < 0)
                    {
                        return -1;
                    }
                }
            }

            //{2E41B9BF-6B67-4b56-BD54-A836CE09F52B}
            if (this.IsJudgePackageFee)
            {
                if (this.feePackageMgr.GetPackageRegisterCount(this.regObj.PID.CardNO) < 1)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("当前患者套餐中已无可用的挂号费!"), "提示");
                    return -1;
                }
            }

            return 0;
        }

        /// <summary>
        /// 校验combox
        /// </summary>
        private int ValidCombox(string ErrMsg)
        {
            int j = 0;
            for (int i = 0; i < this.cmbPatientType.Items.Count; i++)
            {
                if (this.cmbPatientType.Text.Trim() == this.cmbPatientType.Items[i].ToString())
                {
                    this.cmbPatientType.SelectedIndex = i;
                    j++;
                    break;
                }
            }
            if (j == 0)
            {
                MessageBox.Show(ErrMsg);
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 身份证有效校验
        /// </summary>
        /// <param name="idNO"></param>
        /// <param name="enumType"></param>
        /// <returns></returns>
        private int ProcessIDENNO(string idNO, EnumCheckIDNOType enumType)
        {
            try
            {
                string errText = string.Empty;
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
                    this.tbIDNO.Focus();
                    return -1;
                }
                string[] reurnString = errText.Split(',');
                if (enumType == EnumCheckIDNOType.BeforeSave)
                {
                    string temp = reurnString[1];
                    this.dtpBirthday.Value = DateTime.Parse(temp);
                    this.cmbGender.Text = reurnString[2];
                    this.setAge(this.dtpBirthday.Value);
                }
                else
                {
                    if (this.dtpBirthday.Value.ToShortDateString() != reurnString[1])
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("输入的生日日期与身份证号码中的生日不符"));
                        this.dtpBirthday.Focus();
                        return -1;
                    }

                    if (this.cmbGender.Text != reurnString[2])
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("输入的性别与身份证中号的性别不符"));
                        this.cmbGender.Focus();
                        return -1;
                    }
                }
            }
            catch
            {
            }
            return 1;
        }

        /// <summary>
        /// 获取有效的排班信息。适用于
        /// 不是从项目列表选择看诊时间段,而是直接输入
        /// 看诊时间段
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Registration.Schema GetValidSchema(FS.HISFC.Models.Registration.RegLevel level)
        {
            FS.HISFC.Models.Registration.Schema schema = (FS.HISFC.Models.Registration.Schema)this.dtSeeDate.Tag;
            if (schema != null) return schema;

            DateTime bookingDate = this.dtSeeDate.Value.Date;
            ArrayList al = null;

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
        /// 获取有效排版
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
        /// 判断是否为专家号
        /// </summary>
        /// <param name="level"></param>
        /// <param name="schema"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 获取挂号费
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
                ICountSpecialRegFee.CountSpecialRegFee(this.dtpBirthday.Value, this.tbName.Text, this.tbIDNO.Text, ref regFee, ref digFee, ref othFee, ref obj);
            }

            obj.RegLvlFee.RegFee = regFee;
            obj.RegLvlFee.ChkFee = chkFee;
            obj.RegLvlFee.OwnDigFee = digFee;
            obj.RegLvlFee.OthFee = othFee;

            return rtn;
        }

        /// <summary>
        /// 获取挂号信息
        /// </summary>
        /// <returns></returns>
        private int GetValue()
        {
            FS.HISFC.Models.Registration.RegLevel selectedLevel = this.cmbRegLevel.SelectedItem as FS.HISFC.Models.Registration.RegLevel;
            FS.HISFC.Models.Registration.Schema selectedSchema = this.dtSeeDate.Tag as FS.HISFC.Models.Registration.Schema;
            //门诊号
            this.regObj.ID = this.regMgr.GetSequence("Registration.Register.ClinicID");
            this.regObj.TranType = FS.HISFC.Models.Base.TransTypes.Positive;
            this.regObj.DoctorInfo.Templet.RegLevel.ID = this.cmbRegLevel.Tag.ToString();
            this.regObj.DoctorInfo.Templet.RegLevel.Name = this.cmbRegLevel.Text;
            this.regObj.DoctorInfo.Templet.RegLevel.IsEmergency = selectedLevel.IsEmergency;
            this.regObj.DoctorInfo.Templet.RegLevel.IsExpert = selectedLevel.IsExpert;
            this.regObj.DoctorInfo.Templet.Dept.ID = this.cmbDept.Tag.ToString();
            this.regObj.DoctorInfo.Templet.Dept.Name = this.cmbDept.Text;
            this.regObj.DoctorInfo.Templet.Doct.ID = this.cmbDoctor.Tag.ToString();
            this.regObj.DoctorInfo.Templet.Doct.Name = this.cmbDoctor.Text;
            if(selectedSchema!= null) this.regObj.DoctorInfo.Templet.Noon = selectedSchema.Templet.Noon;
            this.regObj.Name = FS.FrameWork.Public.String.TakeOffSpecialChar(this.tbName.Text.Trim(), "'");//患者姓名
            this.regObj.Sex.ID = this.cmbGender.Tag.ToString();//性别
            this.regObj.Birthday = this.dtpBirthday.Value;//出生日期
            this.regObj.Pact.ID = this.cmbPatientType.Tag.ToString();//合同单位
            FS.HISFC.Models.Base.PactInfo pact = conMgr.GetPactUnitInfoByPactCode(this.regObj.Pact.ID);
            if (pact == null || pact.ID == "")
            {
                throw new Exception("获取代码为:" + this.regObj.Pact.ID + "的合同单位信息出错!" + this.conMgr.Err);
            }
            this.regObj.Pact.Name = pact.Name;
            this.regObj.Pact.PayKind.ID = pact.PayKind.ID;
            this.regObj.Pact.PayKind.Name = pact.PayKind.Name;
            this.regObj.PhoneHome = FS.FrameWork.Public.String.TakeOffSpecialChar(this.tbPhone.Text.Trim(), "'");
            this.regObj.AddressHome = FS.FrameWork.Public.String.TakeOffSpecialChar(this.tbAddress.Text.Trim(), "'");
            this.regObj.CardType.ID = this.cmbCardType.Tag.ToString();
            this.regObj.IDCard = FS.FrameWork.Public.String.TakeOffSpecialChar(this.tbPhone.Text.Trim(), "'");
            this.regObj.RegType = FS.HISFC.Models.Base.EnumRegType.Reg;
            if (selectedLevel.IsSpecial)
            {
                this.regObj.RegType = FS.HISFC.Models.Base.EnumRegType.Spe;
            }

            //{496701C2-CCAE-4a8d-B3DB-7D528CFF7025}
            //初诊复诊
            int regCount = this.regMgr.QueryRegisterByCardNOTimeDept(this.regObj.PID.CardNO,
                                                                     this.regObj.DoctorInfo.Templet.Dept.ID,
                                                                     this.regMgr.GetDateTimeFromSysDateTime().AddYears(-1).Date);

            if (regCount > 0)
            {
                this.regObj.IsFirst = false;
            }
            else
            {
                this.regObj.IsFirst = true;
            }

            FS.HISFC.Models.Registration.Schema schema = null;
            //只有专家、专科、特诊需要输入看诊时间段、更新限额
            if (selectedLevel.IsSpecial || selectedLevel.IsFaculty || selectedLevel.IsExpert)
            {
                schema = this.GetValidSchema(selectedLevel);
                if (schema == null)
                {
                    this.dtSeeDate.Focus();
                    throw new Exception("时间指定错误,没有符合条件的排班信息!");
                }

                if (this.VerifyIsProfessor(selectedLevel, schema) == false)
                {
                    this.cmbRegLevel.Focus(); 
                    throw new Exception("");
                }
                this.dtSeeDate.Tag = schema;

                this.regObj.DoctorInfo.Templet.ID = schema.Templet.ID;
                this.regObj.DoctorInfo.Templet.Noon.ID = schema.Templet.Noon.ID;
                this.regObj.DoctorInfo.Templet.IsAppend = schema.Templet.IsAppend;
                this.regObj.DoctorInfo.Templet.ID = schema.Templet.ID;
                this.regObj.DoctorInfo.Templet.Begin = DateTime.Parse(this.dtSeeDate.Value.ToString("yyyy-MM-dd") + " " + this.dtBegin.Value.ToString("HH:mm:ss"));
                this.regObj.DoctorInfo.Templet.End = DateTime.Parse(this.dtSeeDate.Value.ToString("yyyy-MM-dd") + " " + this.dtEnd.Value.ToString("HH:mm:ss"));
                DateTime dtNow = this.regMgr.GetDateTimeFromSysDateTime();
                if (DateTime.Compare(this.dtSeeDate.Value.Date, dtNow.Date) > 0)
                {
                    this.regObj.DoctorInfo.SeeDate = DateTime.Parse(this.dtSeeDate.Value.ToString("yyyy-MM-dd") + " " + this.dtBegin.Value.ToString("HH:mm:ss"));
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
            else
            {
                if (DateTime.Compare(this.dtSeeDate.Value.Date, this.regMgr.GetDateTimeFromSysDateTime().Date) > 0)
                {
                    this.regObj.DoctorInfo.SeeDate = DateTime.Parse(this.dtSeeDate.Value.ToString("yyyy-MM-dd") + " " + this.dtBegin.Value.ToString("HH:mm:ss"));
                }
                else
                {
                    this.regObj.DoctorInfo.SeeDate = this.regMgr.GetDateTimeFromSysDateTime();
                }
                this.regObj.DoctorInfo.Templet.Begin = DateTime.Parse(this.regObj.DoctorInfo.SeeDate.Date.ToString("yyyy-MM-dd") + " " +
                        this.dtBegin.Value.ToString("HH:mm:ss"));
                this.regObj.DoctorInfo.Templet.End = DateTime.Parse(this.regObj.DoctorInfo.SeeDate.Date.ToString("yyyy-MM-dd") + " " +
                        this.dtEnd.Value.ToString("HH:mm:ss"));
                this.regObj.DoctorInfo.Templet.Noon.ID = this.getNoon(this.regObj.DoctorInfo.SeeDate);

                if (this.regObj.DoctorInfo.Templet.Noon.ID == "")
                {
                    throw new Exception("未维护午别信息,请先维护!");
                }
                this.regObj.DoctorInfo.Templet.ID = "";
            }

            #region 挂号费

            int rtn = ConvertRegFeeToObject(regObj);
            if (rtn == -1)
            {
                this.cmbRegLevel.Focus();
                throw new Exception("获取挂号费出错!" + this.regFeeMgr.Err);
            }

            if (rtn == 1)
            {
                this.cmbRegLevel.Focus();
                throw new Exception("该挂号级别未维护挂号费,请先维护挂号费");
            }

            #endregion

            //获得患者应收、报销
            //this.ConvertCostToObject(regObj);

            //处方号
            this.regObj.RecipeNO = this.tbRecipeNo.Text.Trim();
            this.regObj.IsFee = false;
            this.regObj.Status = FS.HISFC.Models.Base.EnumRegisterStatus.Valid;
            this.regObj.IsSee = false;
            this.regObj.InputOper.ID = this.regMgr.Operator.ID;
            this.regObj.InputOper.OperTime = this.regMgr.GetDateTimeFromSysDateTime();
            this.regObj.DoctorInfo.Templet.Noon.Name = this.noonMgr.Query(this.regObj.DoctorInfo.Templet.Noon.ID);
            this.regObj.CancelOper.ID = "";
            this.regObj.CancelOper.OperTime = DateTime.MinValue;
            ArrayList al = new ArrayList();
            this.regObj.IDCard = this.tbIDNO.Text;
            this.regObj.IsFee = true;
            return 0;
        }

        /// <summary>
        /// 将应缴金额转为挂号实体,
        /// 属性不能作为ref参数传递 TNND
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="lstCardFee"></param>
        private void ConvertCostToObject(FS.HISFC.Models.Registration.Register obj)
        {
            this.RegFeeList = null;
            decimal othFee = 0;
            decimal ownCost = 0;
            decimal pubCost = 0;
            othFee = obj.RegLvlFee.OthFee;
            if (this.IsCountSpecialRegFee && this.ICountSpecialRegFee != null)
            {
                decimal regFee = obj.RegLvlFee.RegFee;
                decimal digFee = obj.RegLvlFee.OwnDigFee;
                ICountSpecialRegFee.CountSpecialRegFee(this.dtpBirthday.Value, this.tbName.Text, this.tbIDNO.Text, ref regFee, ref digFee, ref othFee, ref obj);
                this.RegFeeList = this.getCost(regFee, obj.RegLvlFee.ChkFee, digFee, ref othFee, ref ownCost, ref pubCost, this.regObj.PID.CardNO);
            }
            else
            {
                this.RegFeeList = this.getCost(obj.RegLvlFee.RegFee, obj.RegLvlFee.ChkFee, obj.RegLvlFee.OwnDigFee,
                        ref othFee, ref ownCost, ref pubCost, this.regObj.PID.CardNO);
            }

            obj.RegLvlFee.OthFee = othFee;
            obj.OwnCost = ownCost;
            obj.PubCost = pubCost;

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

            if (level.IsFaculty || level.IsExpert)//专家、专科,扣挂号限额
            {
                if ((this.dtSeeDate.Tag as FS.HISFC.Models.Registration.Schema).Templet.RegQuota - (this.dtSeeDate.Tag as FS.HISFC.Models.Registration.Schema).RegedQTY > 0)
                {
                    rtn = SchMgr.Increase(
                        (this.dtSeeDate.Tag as FS.HISFC.Models.Registration.Schema).Templet.ID,
                        true, false, false, false);
                }
                else//减预约限额
                {
                    rtn = SchMgr.Increase(
                        (this.dtSeeDate.Tag as FS.HISFC.Models.Registration.Schema).Templet.ID,
                        false, true, true, false);
                }

                //判断限额是否允许挂号
                if (this.IsPermitOverrun(SchMgr, regType, (this.dtSeeDate.Tag as FS.HISFC.Models.Registration.Schema).Templet.ID,
                                            level, ref seeNo, ref Err) == -1)
                {
                    return -1;
                }
            }
            else if (level.IsSpecial && !isFilterDoc)//特诊扣特诊限额
            {
                rtn = SchMgr.Increase(
                    (this.dtSeeDate.Tag as FS.HISFC.Models.Registration.Schema).Templet.ID,
                    false, false, false, true);

                //判断限额是否允许挂号

                if (this.IsPermitOverrun(SchMgr, regType, (this.dtSeeDate.Tag as FS.HISFC.Models.Registration.Schema).Templet.ID,
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
        /// 更新处方号		
        /// </summary>
        /// <param name="Cnt"></param>
        private void UpdateRecipeNo(int Cnt)
        {
            this.tbRecipeNo.Text = Convert.ToString(long.Parse(this.tbRecipeNo.Text.Trim()) + Cnt);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        private int Save()
        {
            if (this.valid() == -1)
            {
                return -1;
            }

            if (this.GetValue() == -1)
            {
                return -1;
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


            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.regMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.SchemaMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.radtProcess.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.feeMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            int rtn;
            string Err = "";
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

            #region 取发票号

            string strInvioceNO = "";
            string strRealInvoiceNO = "";
            string strErrText = "";
            int iRes = 0;
            string strInvoiceType = "R";

            FS.HISFC.Models.Base.Employee employee = this.regMgr.Operator as FS.HISFC.Models.Base.Employee;

            //有费用信息的时候才打发票
            if (this.RegFeeList.Count > 0)
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
            if (RegFeeList.Count > 0)
            {
                FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
                int rev = this.feeMgr.ValidMarkNO(this.regObj.PID.CardNO, ref accountCard);

                decimal totCost = (decimal)this.hsPayCost["TOT"];
                decimal actuCost = (decimal)this.hsPayCost["ACTU"];
                decimal giftCost = (decimal)this.hsPayCost["GIFT"];
                decimal etcCost = (decimal)this.hsPayCost["ETC"];

                decimal countactu = actuCost;
                decimal countgift = giftCost;
                decimal countetc = etcCost;

                for (int i = 0; i < this.RegFeeList.Count; i++)
                {
                    HISFC.Models.Registration.RegisterFeeDetail regFeeDetail = this.RegFeeList[i] as HISFC.Models.Registration.RegisterFeeDetail;
                    regFeeDetail.InvoiceNo = strInvioceNO;
                    regFeeDetail.Print_InvoiceNo = strRealInvoiceNO;
                    regFeeDetail.ClinicNO = this.regObj.ID;
                    regFeeDetail.MarkType = accountCard.MarkType;
                    regFeeDetail.MarkNO = accountCard.MarkNO;
                    regFeeDetail.SequenceNO = (i+1).ToString();

                    regFeeDetail.Patient.PID.CardNO = this.regObj.PID.CardNO;
                    regFeeDetail.Patient.Name = this.regObj.Name;
                    regFeeDetail.CancelFlag = 1;

                    regFeeDetail.Oper.ID = employee.ID;
                    regFeeDetail.Oper.Name = employee.Name;
                    regFeeDetail.Oper.OperTime = current;

                    regFeeDetail.IsBalance = false;
                    regFeeDetail.BalanceNo = "";
                    regFeeDetail.Qty = 1;

                    decimal real_cost = 0.0m;
                    decimal gift_cost = 0.0m;
                    decimal etc_cost = 0.0m;

                    //最后一个项目的价格由总价格减去前面所有项目的价格
                    if (i == this.RegFeeList.Count - 1)
                    {
                        real_cost = countactu;
                        gift_cost = countgift;
                        etc_cost = countetc;
                    }
                    else
                    {
                        //总是直接舍去小数点后两位以后的数值，以防止最后一个项目的价格出现负数
                        real_cost = Math.Floor((regFeeDetail.Tot_cost * actuCost * 100) / totCost) / 100;
                        gift_cost = Math.Floor((regFeeDetail.Tot_cost * giftCost * 100) / totCost) / 100;
                        etc_cost = Math.Floor((regFeeDetail.Tot_cost * etcCost * 100) / totCost) / 100;
                    }

                    countactu -= real_cost;
                    countgift -= gift_cost;
                    countetc -= etc_cost;

                    if (real_cost < 0 || gift_cost < 0)
                    {
                        MessageBox.Show("分配优惠金额，赠送金额至挂号明细时出现错误！");
                        return -1;
                    }

                    //当总价不等于优惠金额+实收金额+赠送金额的时候
                    //此处不可能出现regFeeDetail.Tot_cost < real_cost + gift_cost + etc_cost
                    //的情况，因为上面都是直接舍去小数点后两位以后的数值
                    if (regFeeDetail.Tot_cost > real_cost + gift_cost + etc_cost)
                    {
                        decimal diff = regFeeDetail.Tot_cost - real_cost - gift_cost - etc_cost;

                        if (countactu + countgift + countetc >= diff)
                        {
                            if (countactu >= diff)
                            {
                                real_cost += diff;
                                countactu -= diff;
                            }
                            else
                            {
                                real_cost += countactu;
                                countactu = 0;
                                diff -= countactu;

                                if (countgift >= diff)
                                {
                                    countgift -= diff;
                                    gift_cost += diff;
                                }
                                else
                                {
                                    if (countetc >= diff)
                                    {
                                        countetc -= diff;
                                        etc_cost += diff;
                                    }
                                    else
                                    {
                                        MessageBox.Show("分配优惠金额，赠送金额至挂号明细时出现错误！");
                                        return -1;
                                    }
                                }
                            }
                        }
                    }

                    regFeeDetail.Real_cost = real_cost;
                    regFeeDetail.Gift_cost = gift_cost;
                    regFeeDetail.Etc_cost = etc_cost;
                }
            }

            #endregion


            #region
            ArrayList payMode = this.GetPayModeInfo();
            int payinfoSequence = 1;
            foreach (FS.HISFC.Models.Registration.RegisterPayMode pay in payMode)
            {
                pay.InvoiceNo = strInvioceNO;
                pay.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                pay.SequenceNO = payinfoSequence.ToString();
                pay.Oper.ID = FS.FrameWork.Management.Connection.Operator.ID;
                pay.Oper.OperTime = this.regMgr.GetDateTimeFromSysDateTime();

                //账户扣费
                if (pay.Mode_Code == "YS" || pay.Mode_Code == "DC")
                {
                    FS.HISFC.Models.RADT.PatientInfo powerpatient = null;
                    List<AccountDetail> tmp = accountMgr.GetAccountDetail(pay.AccountID, pay.AccountType,"1");
                    if (tmp != null && tmp.Count > 0)
                    {
                        AccountDetail tmpAc = tmp[0];
                        powerpatient = accountMgr.GetPatientInfoByCardNO(tmpAc.CardNO);
                    }

                    if (powerpatient == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("查找账户授权人失败！");
                        return -1;
                    }

                    FS.HISFC.Models.RADT.PatientInfo patient = accountMgr.GetPatientInfoByCardNO(this.regObj.PID.CardNO);

                    if (patient == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("查找当前患者信息失败！");
                        return -1;
                    }

                    if (accountPay.OutpatientPay(patient,
                                                pay.AccountID,
                                                pay.AccountType,
                                                pay.AccountFlag == "0" ? -pay.Tot_cost : 0,
                                                pay.AccountFlag == "0" ? 0 : -pay.Tot_cost,
                                                strInvioceNO, powerpatient,
                                                FS.HISFC.Models.Account.PayWayTypes.R,
                                                1) < 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("账户扣费失败！");
                        return -1;
                    }

                }

                payinfoSequence++;
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
                    MessageBox.Show(this.regMgr.Err, "提示");
                    return -1;
                }


                #region 保存费用明细信息
                if (this.RegFeeList != null && this.RegFeeList.Count > 0)
                {
                    if (this.feeMgr.SaveRegFeeList(this.RegFeeList) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.feeMgr.Err, "提示");
                        return -1;
                    }
                }

                if (payMode != null && payMode.Count > 0)
                {
                    //{9F2C17EC-0AE6-4df8-B959-F4D99314E227}
                    if (this.feeMgr.SaveRegPayModeList(this.regObj,payMode) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.feeMgr.Err, "提示");
                        return -1;
                    }
                }

                #endregion


                //更新患者基本信息
                if (this.UpdatePatientinfo(this.regObj, this.radtProcess, this.regMgr, ref Err) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
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
                        MessageBox.Show(Err);
                        return -1;
                    }
                }

                #region 发票走号

                //有费用信息的时候，才处理发票
                if (this.RegFeeList.Count > 0)
                {
                    if (this.GetRecipeType == 2 || this.GetRecipeType == 3)
                    {
                        string invoiceStytle = this.controlParams.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.GET_INVOICE_NO_TYPE, false, "0");
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

                //最后更新处方号,加 1,防止中途返回跳号
                this.UpdateRecipeNo(1);
                this.queryRegLevl();
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "提示");
                return -1;
            }

            //条码打印
            this.PrintBarCode(this.regObj);

            //有费用信息的时候，才打印发票
            if (this.RegFeeList.Count > 0)
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

                MessageBox.Show("挂号成功!");

            }
            else if (this.RegFeeList.Count == 0)
            {
                MessageBox.Show("挂号成功! 不打印发票!", "提示");
            }

            this.addRegister(this.regObj);
            this.ChangeInvoiceNO();
            this.tbQueryNO.Focus();
            return 0;
        }

        /// <summary>
        ///  获取会员账户支付方式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AccountPayShow()
        {
            if(this.regObj == null || string.IsNullOrEmpty(this.regObj.PID.CardNO))
            {
                MessageBox.Show("请先检索患者！");
                return;
            }

            try
            {
                this.DelEvents();

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

                if (CashRow == -1 || YSRow == -1 || DCRow == -1)
                {
                    throw new Exception("查找支付方式失败！");
                }

                //获取当前账户支付的金额
                ArrayList tmp = new ArrayList();
                tmp.AddRange(this.accountPayList);
                tmp.AddRange(this.giftPayList);


                //会员支付框
                string ErrInfo = string.Empty;
                frmAccountCost accountCost = new frmAccountCost();
                FS.HISFC.Models.RADT.PatientInfo patient = accountMgr.GetPatientInfoByCardNO(this.regObj.PID.CardNO);
                accountCost.PatientInfo = patient;
                //{4E4E36FF-EFBB-42ea-90EB-13FADAA4623A}
                accountCost.IsEmpower = false;
                accountCost.OriginalCardNO = this.regObj.PID.CardNO;
                accountCost.DeliverableCost = CashCost + Decimal.Parse(this.tbAccount.Text);
                if (accountCost.SetPayInfo(tmp, ref ErrInfo) < 0)
                {
                    throw new Exception(ErrInfo);
                }
                accountCost.SetPayModeRes += new DelegateHashtableSet(accountCost_SetPayModeRes);
                accountCost.ShowDialog();
            }
            catch (Exception ex)
            {
                this.AddEvents();
                MessageBox.Show("获取待支付金额出错:" + ex.Message);
                return;
            }

            this.AddEvents();
        }

        private int accountCost_SetPayModeRes(Hashtable hsTable, decimal totCost)
        {
            try
            {
                if (hsTable == null)
                {
                    throw new Exception("获取会员支付方式出错！");
                }

                if (hsTable.ContainsKey("YS"))
                {
                    this.accountPayList = hsTable["YS"] as ArrayList;
                }

                if (hsTable.ContainsKey("DC"))
                {
                    this.giftPayList = hsTable["DC"] as ArrayList;
                }

                if (this.accountPayList == null || this.giftPayList == null)
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

                    this.accountPayList = new ArrayList();
                    this.giftPayList = new ArrayList();
                    this.empowerGiftPayList = new ArrayList();
                    this.empowerAccountPayList = new ArrayList();
                    this.tbAccount.Text = "0.00";
                    this.tbPowerAmount.Text = "0.00";
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

                decimal empowerPayCount = 0.0m;
                decimal empowergiftCount = 0.0m;
                decimal accountPayCount = 0.0m;
                decimal accountgiftCount = 0.0m;

                foreach (Row row in this.fpPayMode_Sheet1.Rows)
                {
                    FS.HISFC.Models.Base.Const pay = row.Tag as FS.HISFC.Models.Base.Const;

                    if (pay.ID == "YS")
                    {
                        foreach (FS.HISFC.Models.Registration.RegisterPayMode payMode in this.empowerAccountPayList)
                        {
                            empowerPayCount += payMode.Tot_cost;
                        }

                        foreach (FS.HISFC.Models.Registration.RegisterPayMode payMode in this.accountPayList)
                        {
                            accountPayCount += payMode.Tot_cost;
                        }

                        this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value = empowerPayCount + accountPayCount;
                        totCost -= accountPayCount;
                        this.fpPayMode_Sheet1.Cells[CashRow, (int)PayModeCols.TotCost].Value = totCost;
                    }

                    if (pay.ID == "DC")
                    {
                        foreach (FS.HISFC.Models.Registration.RegisterPayMode payMode in this.empowerGiftPayList)
                        {
                            empowergiftCount += payMode.Tot_cost;
                        }

                        foreach (FS.HISFC.Models.Registration.RegisterPayMode payMode in this.giftPayList)
                        {
                            accountgiftCount += payMode.Tot_cost;
                        }

                        this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value = empowergiftCount + accountgiftCount;
                        totCost -= accountgiftCount;
                        this.fpPayMode_Sheet1.Cells[CashRow, (int)PayModeCols.TotCost].Value = totCost;
                    }
                }

                this.tbPowerAmount.Text = (empowerPayCount+empowergiftCount).ToString("F2");
                this.tbAccount.Text = (accountPayCount + accountgiftCount).ToString("F2");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }

            this.setCostInfoAfterEdit();
            return 1;
        }

        /// <summary>
        ///  获取会员账户支付方式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmpowerPayShow()
        {
            if (this.regObj == null || string.IsNullOrEmpty(this.regObj.PID.CardNO))
            {
                MessageBox.Show("请先检索患者！");
                return;
            }

            try
            {
                this.DelEvents();

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

                if (CashRow == -1 || YSRow == -1 || DCRow == -1)
                {
                    throw new Exception("查找支付方式失败！");
                }

                //获取当前账户支付的金额
                ArrayList tmp = new ArrayList();
                tmp.AddRange(this.empowerAccountPayList);
                tmp.AddRange(this.empowerGiftPayList);


                //代付支付框
                string ErrInfo = string.Empty;
                frmAccountCost EmpowerCost = new frmAccountCost();
                //{4E4E36FF-EFBB-42ea-90EB-13FADAA4623A}
                EmpowerCost.IsEmpower = true;
                EmpowerCost.OriginalCardNO = this.regObj.PID.CardNO;
                FS.HISFC.Models.RADT.PatientInfo patient = null;
                if (this.empowerAccountPayList.Count > 0)
                {
                    FS.HISFC.Models.Registration.RegisterPayMode paymode = empowerAccountPayList[0] as FS.HISFC.Models.Registration.RegisterPayMode;
                    List<FS.HISFC.Models.Account.AccountDetail> accounts = this.accountMgr.GetAccountDetail(paymode.AccountID, "ALL", "1");

                    if (accounts.Count > 0)
                    {
                        patient = accountMgr.GetPatientInfoByCardNO(accounts[0].CardNO);
                    }

                }
                else if(this.empowerGiftPayList.Count > 0)
                {
                    FS.HISFC.Models.Registration.RegisterPayMode paymode = empowerAccountPayList[0] as FS.HISFC.Models.Registration.RegisterPayMode;
                    List<FS.HISFC.Models.Account.AccountDetail> accounts = this.accountMgr.GetAccountDetail(paymode.AccountID, "ALL", "1");

                    if (accounts.Count > 0)
                    {
                        patient = accountMgr.GetPatientInfoByCardNO(accounts[0].CardNO);
                    }
                }

                EmpowerCost.PatientInfo = patient;
                EmpowerCost.DeliverableCost = CashCost + decimal.Parse(this.tbPowerAmount.Text);
                if (EmpowerCost.SetPayInfo(tmp, ref ErrInfo) < 0)
                {
                    throw new Exception(ErrInfo);
                }
                EmpowerCost.SetPayModeRes += new DelegateHashtableSet(EmpowerCost_SetPayModeRes);
                EmpowerCost.ShowDialog();
            }
            catch (Exception ex)
            {
                this.AddEvents();
                MessageBox.Show("获取待支付金额出错:" + ex.Message);
                return;
            }

            this.AddEvents();
        }

        private int EmpowerCost_SetPayModeRes(Hashtable hsTable, decimal totCost)
        {

            try
            {
                if (hsTable == null)
                {
                    throw new Exception("获取会员支付方式出错！");
                }

                if (hsTable.ContainsKey("YS"))
                {
                    this.empowerAccountPayList = hsTable["YS"] as ArrayList;
                }

                if (hsTable.ContainsKey("DC"))
                {
                    this.empowerGiftPayList = hsTable["DC"] as ArrayList;
                }

                if (this.empowerAccountPayList == null || this.empowerGiftPayList == null)
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

                    this.accountPayList = new ArrayList();
                    this.giftPayList = new ArrayList();
                    this.empowerGiftPayList = new ArrayList();
                    this.empowerAccountPayList = new ArrayList();
                    this.tbAccount.Text = "0.00";
                    this.tbPowerAmount.Text = "0.00";
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

                decimal empowerPayCount = 0.0m;
                decimal empowergiftCount = 0.0m;
                decimal accountPayCount = 0.0m;
                decimal accountgiftCount = 0.0m;

                foreach (Row row in this.fpPayMode_Sheet1.Rows)
                {
                    FS.HISFC.Models.Base.Const pay = row.Tag as FS.HISFC.Models.Base.Const;

                    if (pay.ID == "YS")
                    {
                        foreach (FS.HISFC.Models.Registration.RegisterPayMode payMode in this.empowerAccountPayList)
                        {
                            empowerPayCount += payMode.Tot_cost;
                        }

                        foreach (FS.HISFC.Models.Registration.RegisterPayMode payMode in this.accountPayList)
                        {
                            accountPayCount += payMode.Tot_cost;
                        }

                        this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value = empowerPayCount + accountPayCount;
                        totCost -= empowerPayCount;
                        this.fpPayMode_Sheet1.Cells[CashRow, (int)PayModeCols.TotCost].Value = totCost;
                    }

                    if (pay.ID == "DC")
                    {
                        foreach (FS.HISFC.Models.Registration.RegisterPayMode payMode in this.empowerGiftPayList)
                        {
                            empowergiftCount += payMode.Tot_cost;
                        }

                        foreach (FS.HISFC.Models.Registration.RegisterPayMode payMode in this.giftPayList)
                        {
                            accountgiftCount += payMode.Tot_cost;
                        }

                        this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value = empowergiftCount + accountgiftCount;
                        totCost -= empowergiftCount;
                        this.fpPayMode_Sheet1.Cells[CashRow, (int)PayModeCols.TotCost].Value = totCost;
                    }
                }

                this.tbPowerAmount.Text = (empowerPayCount + empowergiftCount).ToString("F2");
                this.tbAccount.Text = (accountPayCount + accountgiftCount).ToString("F2");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }

            this.setCostInfoAfterEdit();
            return 1;
        }

        #endregion

        /// <summary>
        /// 按键
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.ucChooseDate.Visible = false;
            }

            if (this.fpPayMode.ContainsFocus)
            {

                if (keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Enter)
                {
                    this.PutArrow(keyData);
                    return true;
                }
            }

            if (keyData == Keys.Enter)
            {
                //{0A94EF4E-FD1D-43be-A223-BC59B8433BF6}
                if (this.tbQueryNO.Focused || this.tbName.Focused || this.tbPhone.Focused)
                {
                    return base.ProcessDialogKey(keyData);
                }
                else
                {
                    if (this.dtEnd.Focused)
                    {
                        this.tbRealCost.Focus();
                        return true;
                    }

                    if (this.tbTotCost.Focused)
                    {
                        this.OnSave(null, null);
                        return true;
                    }

                    System.Windows.Forms.SendKeys.Send("{TAB}");
                    return true;
                }
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

        #region IInterfaceContainer 成员

        Type[] FS.FrameWork.WinForms.Forms.IInterfaceContainer.InterfaceTypes
        {
            get
            {
                Type[] type = new Type[2];
                type[0] = typeof(FS.HISFC.BizProcess.Interface.Registration.IRegPrint);
                type[1] = typeof(FS.HISFC.BizProcess.Interface.Registration.IProcessRegiter);
                return type;
            }
        }

        #endregion

        /// <summary>
        /// 判断身份证
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
    }
}
