using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FS.FrameWork.Management;
using FS.HISFC.Models.Base;
using System.Xml;
using System.Collections;
using FS.HISFC.BizLogic.Fee;
using FS.FrameWork.Models;
using FS.HISFC.BizProcess.Integrate.FeeInterface;
using System.Text.RegularExpressions;

namespace FS.HISFC.Components.Account.Controls.IBorn
{
    public partial class ucRegPatientInfoIBorn : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucRegPatientInfoIBorn()
        {
            InitializeComponent();
        }

        #region 自定义事件

        /// <summary>
        /// 当焦点进入ComBox时触发的事件
        /// </summary>
        public event HandledEventHandler CmbFoucs;

        /// <summary>
        /// 当当前UC焦点到达最后一个时发生的事件
        /// </summary>
        public event HandledEventHandler OnFoucsOver;

        /// <summary>
        /// 在输入患者信息回车时查找患者信息
        /// </summary>
        public event HandledEventHandler OnEnterSelectPatient;

        #endregion

        #region 变量

        #region 业务层变量
        /// <summary>
        /// Manager业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 门诊费用业务类
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Outpatient outpatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();

        /// <summary>
        /// Acount业务层
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();

        /// <summary>
        /// 入出转
        /// </summary>
        private HISFC.BizProcess.Integrate.RADT radtManager = new FS.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// 合同单位业务层
        /// </summary>
        private PactUnitInfo pactManager = new PactUnitInfo();

        private List<string> pactList = new List<string>();

        /// <summary>
        /// 控制参数业务层
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        /// <summary>
        /// 合同单位验证接口
        /// </summary>
        MedcareInterfaceProxy medcareProxy = new MedcareInterfaceProxy();


        /// <summary>
        /// 外屏接口{67C90AAC-CFAD-4089-96F4-9F9FC82D8754}
        /// </summary>
        public FS.HISFC.BizProcess.Interface.FeeInterface.IMultiScreen iMultiScreen = null;
        public FS.HISFC.Models.RADT.PatientInfo showPatientInfo = null;

        public FS.HISFC.Models.Base.PactInfo PatientPactInfo = null;

        #endregion

        #region 变量
        /// <summary>
        /// 婚姻状态实体
        /// </summary>
        HISFC.Models.RADT.MaritalStatusEnumService maritalService = new FS.HISFC.Models.RADT.MaritalStatusEnumService();

        /// <summary>
        /// 患者信息
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo patientInfo = null;

        /// <summary>
        /// 门诊卡号
        /// </summary>
        private string cardNO = string.Empty;

        /// <summary>
        /// 民族
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper NationHelp = null;

        /// <summary>
        /// 证件类型
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper IdCardTypeHelp = null;

        /// <summary>
        /// 是否急诊
        /// </summary>
        private bool isTreatment = false;

        /// <summary>
        /// MPI接口
        /// </summary>
        //FS.HISFC.BizProcess.Interface.Platform.IEmpiCommutative iEmpi = null;
        /// <summary>
        /// 数据是否只在本地处理，不往数据中心发送
        /// {BCE8D830-5FEA-4681-A08A-4BB48D172E20}
        /// </summary>
        private bool isLocalOperation = true;

        private string mcardNO = string.Empty;

        //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
        /// <summary>
        /// 输入卡的类别 0或空表示用就诊卡号做物理卡号  不等于0为多卡
        /// </summary>
        private bool cardWay = true;
        /// <summary>
        /// 办卡时是否实时判断，是否享受相应的合同单位
        /// {C49AFFB1-D0EA-41bf-AD60-9F921D91E93D}
        /// </summary>
        private bool isJudgePact = false;

        /// <summary>
        /// 输入身份证时判断如果有医保则合同单位变成医保
        /// </summary>
        private bool isJudgePactByIdno = false;

        /// <summary>
        /// 办卡时输入身份证时判断如果有医保则合同单位变成医保时，转换的合同单位
        /// </summary>
        private string changePactId = "";

        /// <summary>
        /// card_no当物理卡号办卡时是否已生成card_no
        /// </summary>
        private bool isAutoBuildCardNo = false;
        /// <summary>
        /// 医保身份ID, 多个以“|”分开
        /// {870D6A8C-9B17-4e33-A0B6-DB0F1CF15BAE}
        /// </summary>
        List<string> lstPactID = null;
        #endregion

        #region 输入控制变量

        #region 必须录入项目
        /// <summary>
        /// 是否必须输入姓名
        /// </summary>
        private bool isInputName = true;

        /// <summary>
        /// 是否必须输入性别
        /// </summary>
        private bool isInputSex = false;

        /// <summary>
        /// 是否必须输入合同单位
        /// </summary>
        private bool isInputPact = false;

        /// <summary>
        /// 是否必须输入医保证号
        /// </summary>
        private bool isInputSiNo = false;

        /// <summary>
        /// 是否必须输入出生日期
        /// </summary>
        private bool isInputBirthDay = false;

        /// <summary>
        /// 是否必须输入证件类型
        /// </summary>
        private bool isInputIDEType = false;

        /// <summary>
        /// 是否必须输入证件号
        /// </summary>
        private bool isInputIDENO = false;

        /// <summary>
        /// 是否必须输入联系电话
        /// </summary>
        private bool isInputPHONE = false;


        /// <summary>
        /// 是否必须输入紧急联系人
        /// </summary>
        private bool isInputLinkMan = true;

        /// <summary>
        /// 是否必须输入紧急联系人电话
        /// </summary>
        private bool isInputLinkPhone = true;

        /// <summary>
        /// 是否必须按顺序跳转
        /// </summary>
        private bool isInSequentialOrder = false;


        /// <summary>
        ///身份证不正确，1提示是否继续、2仅提示、3不提示// {71A043C7-ADEE-401e-81E2-FF32FBD39D78}
        /// </summary>
        private int isTooltipCARDID = 1;

        #endregion

        #region 是否可以修改
        /// <summary>
        /// 费用来源是否可以修改
        /// </summary>
        private bool isEnablePact = true;

        /// <summary>
        /// 医保证号是否可以修改
        /// </summary>
        private bool isEnableSiNO = true;

        /// <summary>
        /// 是否可以修改证件类型
        /// </summary>
        private bool isEnableIDEType = true;

        /// <summary>
        /// 是否可以修改证件号
        /// </summary>
        private bool isEnableIDENO = true;

        /// <summary>
        /// Vip是否可用
        /// </summary>
        private bool isEnableVip = true;

        /// <summary>
        /// 姓名加密是否可用
        /// </summary>
        private bool isEnableEntry = true;
        #endregion

        /// <summary>
        /// 必须录入控件
        /// </summary>
        private Hashtable InputHasTable = new Hashtable();

        /// <summary>
        /// 是否可以修改控件
        /// </summary>
        private List<Control> EnableControlList = new List<Control>();

        /// <summary>
        /// 是否按照必录项跳转输入焦点
        /// </summary>
        private bool isMustInputTabInde = false;

        /// <summary>
        /// 本院职工合同单位
        /// </summary>
        private string isValidHospitalStaff = "";

        /// <summary>
        /// 必须输入控件最大TabIndex
        /// </summary>
        int inpubMaxTabIndex = 0;
        /// <summary>
        /// 不能同时为空项目
        /// 0 = 不控制
        /// 1 = 控制 身份证与电话号码
        /// </summary>
        private int iMustInpubByOne = 0;

        /// <summary>
        /// card_no当物理卡号办卡时自动生成的card_no
        /// </summary>
        private string autoCardNo = "";

        ///// <summary>
        ///// 病案科室配置路径
        ///// </summary>
        //string filePath = FS.FrameWork.WinForms.Classes.Function.SettingPath + "/CasDeptDefaultValue.xml"; 
        #endregion

        #endregion

        #region 属性
        /// <summary>
        /// 办卡时是否实时判断，是否享受相应的合同单位
        /// {C49AFFB1-D0EA-41bf-AD60-9F921D91E93D}
        /// </summary>
        public bool IsJudgePact
        {
            get { return isJudgePact; }
            set { isJudgePact = value; }
        }

        /// <summary>
        /// 输入身份证时判断如果有医保则合同单位变成医保
        /// </summary>
        public bool IsJudgePactByIdno
        {
            get
            {
                return isJudgePactByIdno;
            }
            set
            {
                isJudgePactByIdno = value;
            }
        }

        /// <summary>
        /// card_no当物理卡号办卡时是否已生成card_no
        /// </summary>
        public bool IsAutoBuildCardNo
        {
            get
            {
                return isAutoBuildCardNo;
            }
            set
            {
                isAutoBuildCardNo = value;
            }
        }

        //card_no当物理卡号办卡时自动生成的card_no
        public string AutoCardNo
        {
            get { return autoCardNo; }
            set { autoCardNo = value; }
        }
        /// <summary>
        /// 合同单位判断
        /// </summary>
        public String IsValidHospitalStaff
        {
            get
            {
                return this.isValidHospitalStaff;
            }
            set
            {
                this.isValidHospitalStaff = value;
            }
        }

        /// <summary>
        /// 门诊卡号
        /// </summary>
        public string CardNO
        {
            set
            {
                if (this.DesignMode) return;
                cardNO = value;
                if (value != string.Empty)
                {
                    SetInfo(cardNO);
                }
            }
            get
            {
                return cardNO;
            }
        }

        //物理就诊卡号
        public string McardNO
        {
            get { return mcardNO; }
            set { mcardNO = value; }
        }

        [Category("控件设置"), Description("是否急诊发卡 True:是 false:否")]
        public bool IsTreatment
        {
            get { return isTreatment; }
            set { isTreatment = value; }
        }

        [Category("控件设置"), Description("是否按照必录项跳转输入焦点")]
        public bool IsMustInputTabIndex
        {
            get
            {
                return isMustInputTabInde;
            }
            set
            {
                isMustInputTabInde = value;
            }
        }


        private bool isJumpHomePhone = false;
        [Category("控件设置"), Description("是否输入家庭地址后直接跳到电话字段")]
        public bool IsJumpHomePhone
        {
            get { return isJumpHomePhone; }
            set { isJumpHomePhone = value; }
        }


        /// <summary>
        /// 数据是否只在本地处理，不往数据中心发送
        /// {BCE8D830-5FEA-4681-A08A-4BB48D172E20}
        /// </summary>
        public bool IsLocalOperation
        {
            get
            {
                return isLocalOperation;
            }
            set
            {
                isLocalOperation = value;
            }
        }

        //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
        [Category("控件设置"), Description("false:就诊卡号做物理卡号 true:就诊卡号与物理卡号不同")]
        public bool CardWay
        {
            get
            {
                return cardWay;
            }
            set
            {
                cardWay = value;
            }
        }
        /// <summary>
        /// 在输入过程中是否允许通过姓名证件号动态查找患者信息 True:是 False:否
        /// </summary>
        [Category("控件设置"), Description("在输入过程中是否允许通过姓名证件号动态查找患者信息 True:是 False:否")]
        public bool IsSelectPatientByNameIDCardByEnter
        {
            get { return isSelectPatientByNameIDCardByEnter; }
            set { isSelectPatientByNameIDCardByEnter = value; }
        }
        /// <summary>
        /// 在输入过程中是否允许通过姓名证件号动态查找患者信息
        /// </summary>
        private bool isSelectPatientByNameIDCardByEnter = false;

        private bool isMutilPactInfo = false;
        public bool IsMutilPactInfo
        {
            get
            {
                return this.isMutilPactInfo;
            }
            set
            {
                this.isMutilPactInfo = value;
                this.cmbPact.Visible = !this.isMutilPactInfo;
                this.comboBoxPactSelect1.Visible = this.isMutilPactInfo;
            }
        }
         
        private bool isMustInputIDNO = false;
        /// <summary>
        /// 是否必须输入身份证号
        /// </summary>
        public bool IsMustInputIDNO
        {
            get
            {
                return this.isMustInputIDNO;
            }
            set
            {
                this.isMustInputIDNO = value;
                this.AddOrRemoveUnitAtMustInputLists(this.lblIDNO, this.txtIDNO, this.isMustInputIDNO);

            }
        }


        private bool isMustNowHome = false;
        /// <summary>
        /// 是否必须输入现住址// {0AFA59AE-59B4-4140-83B1-54DA5B98ED5E}
        /// </summary>
        public bool IsMustNowHome
        {
            get
            {
                return this.isMustNowHome;
            }
            set
            {
                this.isMustNowHome = value;
                this.AddOrRemoveUnitAtMustInputLists(this.lblNowHome, this.txtNowHome, this.isMustNowHome);

            }
        }
        private FS.FrameWork.WinForms.Controls.NeuComboBox ComPact
        {
            get
            {
                return isMutilPactInfo ? this.comboBoxPactSelect1 : this.cmbPact;
            }
        }
        /// <summary>
        /// 是否修改患者信息模式
        /// </summary>
        public bool IsEditMode
        {
            get { return blnEditMode; }
            set
            {
                blnEditMode = value;
            }
        }
        private bool blnEditMode = false;

        #region 输入控制属性
        [Category("控件设置"), Description("姓名是否必须输入！")]
        public bool IsInputName
        {
            get
            {
                return isInputName;
            }
            set
            {
                isInputName = value;
                this.AddOrRemoveUnitAtMustInputLists(this.lblName, this.txtName, value);
            }
        }

        [Category("控件设置"), Description("性别是否必须输入！")]
        public bool IsInputSex
        {
            get
            {
                return isInputSex;
            }
            set
            {
                isInputSex = value;
                this.AddOrRemoveUnitAtMustInputLists(this.lblSex, this.cmbSex, value);
            }
        }

        [Category("控件设置"), Description("合同单位是否必须输入！")]
        public bool IsInputPact
        {
            get
            {
                return isInputPact;
            }
            set
            {
                isInputPact = value;
            }
        }

        [Category("控件设置"), Description("医保证号是否必须输入！")]
        public bool IsInputSiNo
        {
            get { return isInputSiNo; }
            set
            {
                isInputSiNo = value;
                this.AddOrRemoveUnitAtMustInputLists(this.lblSiNO, this.txtSiNO, value);
            }
        }

        [Category("控件设置"), Description("出生日期是否必须输入！")]
        public bool IsInputBirthDay
        {
            get { return isInputBirthDay; }
            set
            {
                isInputBirthDay = value;
                //this.AddOrRemoveUnitAtMustInputLists(this.lblBirthDay, this.dtpBirthDay, value);
                this.AddOrRemoveUnitAtMustInputLists(this.lblBirthDay, this.txtYear, value);
            }
        }

        [Category("控件设置"), Description("证件类型是否必须输入！")]
        public bool IsInputIDEType
        {
            get { return isInputIDEType; }
            set
            {
                isInputIDEType = value;
                this.AddOrRemoveUnitAtMustInputLists(this.lblCardType, this.cmbCardType, value);
            }
        }

        [Category("控件设置"), Description("证件号是否必须输入！")]
        public bool IsInputIDENO
        {
            get { return isInputIDENO; }
            set
            {
                isInputIDENO = value;
                this.AddOrRemoveUnitAtMustInputLists(this.lblIDNO, this.txtIDNO, value);
            }
        }

        [Category("控件设置"), Description("电话是否必须输入！")]
        public bool IsInputPHONE
        {
            get { return isInputPHONE; }
            set
            {
                isInputPHONE = value;
                this.AddOrRemoveUnitAtMustInputLists(this.lblHomePhone, this.txtHomePhone, value);
            }
        }

        [Category("控件设置"), Description("紧急联系人是否必须输入！")]
        public bool IsInputLinkMan
        {
            get { return isInputLinkMan; }
            set
            {
                isInputLinkMan = value;
                this.AddOrRemoveUnitAtMustInputLists(this.lblLinkMan, this.txtLinkMan, value);
            }
        }
        [Category("控件设置"), Description("紧急联系人电话是否必须输入！")]
        public bool IsInputLinkPhone
        {
            get { return isInputLinkPhone; }
            set
            {
                isInputLinkPhone = value;
                this.AddOrRemoveUnitAtMustInputLists(this.lblLinkPhone, this.txtLinkPhone, value);
            }
        }
        [Category("控件设置"), Description("是否按生日->电话->地址->卡号输入！")]
        public bool IsInSequentialOrder
        {
            get { return isInSequentialOrder; }
            set
            {
                isInSequentialOrder = value;
                this.ChangeTabIdex(value);
            }
        }

        [Category("控件设置"), Description("身份证不正确是否提示，1提示是否继续、2仅提示、3不提示")]
        public int IsTooltipCARDID
        {
            get
            {
                return isTooltipCARDID;
            }
            set
            {
                isTooltipCARDID = value;
            }
        }
        #endregion

        #region 是否可以修改控制
        [Category("修改控制"), Description("费用来源是否可以修改")]
        public bool IsEnablePact
        {
            get { return isEnablePact; }
            set
            {
                isEnablePact = value;
                AddOrRemoveUnitAtEnableLists(this.ComPact, value);
            }
        }

        [Category("修改控制"), Description("医保证号是否可以修改")]
        public bool IsEnableSiNO
        {
            get { return isEnableSiNO; }
            set
            {
                isEnableSiNO = value;
                AddOrRemoveUnitAtEnableLists(this.txtSiNO, value);
            }
        }

        [Category("修改控制"), Description("是否可以修改证件类型")]
        public bool IsEnableIDEType
        {
            get { return isEnableIDEType; }
            set
            {
                isEnableIDEType = value;
                AddOrRemoveUnitAtEnableLists(this.cmbCardType, value);
            }
        }

        [Category("修改控制"), Description("是否可以修改证件号")]
        public bool IsEnableIDENO
        {
            get { return isEnableIDENO; }
            set
            {
                isEnableIDENO = value;
                AddOrRemoveUnitAtEnableLists(this.txtIDNO, value);
            }
        }

        [Category("修改控制"), Description("是否可以修改Vip标识")]
        public bool IsEnableVip
        {
            get
            {
                return isEnableVip;
            }
            set
            {
                isEnableVip = value;
                AddOrRemoveUnitAtEnableLists(this.ckVip, value);
            }
        }

        [Category("修改控制"), Description("患者姓名加密是否可以修改")]
        public bool IsEnableEntry
        {
            get
            {
                return isEnableEntry;
            }
            set
            {
                isEnableEntry = value;
                AddOrRemoveUnitAtEnableLists(this.ckEncrypt, value);
            }
        }
        /// <summary>
        /// 不能同时为空项目  0 = 不控制 1 = 控制 身份证与电话号码
        /// </summary>
        [Category("修改控制"), Description("不能同时为空项目  0 = 不控制 1 = 控制 身份证与电话号码")]
        public int IMustInpubByOne
        {
            get { return this.iMustInpubByOne; }
            set { this.iMustInpubByOne = value; }

        }


        public bool IsShowTitle
        {
            set
            {
                lblshow.Visible = value;
            }
            get
            {
                return lblshow.Visible;
            }
        }
        #endregion

        #endregion

        #region 私有方法
        /// <summary>
        /// 初始化下拉列表
        /// </summary>
        /// <returns></returns>
        protected virtual int Init()
        {
            try
            {
                //性别列表
                this.cmbSex.AddItems(FS.HISFC.Models.Base.SexEnumService.List());
                ////{2C77F7F4-49BF-4a36-BE98-90BF9E1947B8}
                this.cmbSex.Tag = "F";

                //民族
                this.cmbNation.AddItems(managerIntegrate.GetConstantList(EnumConstant.NATION));
                this.cmbNation.Text = "汉族";
                NationHelp = new FS.FrameWork.Public.ObjectHelper(this.cmbNation.alItems);
                //婚姻状态
                this.cmbMarry.AddItems(HISFC.Models.RADT.MaritalStatusEnumService.List());

                //国家
                this.cmbCountry.AddItems(managerIntegrate.GetConstantList(EnumConstant.COUNTRY));
                this.cmbCountry.Text = "中国";

                //职业信息
                this.cmbProfession.AddItems(managerIntegrate.GetConstantList(EnumConstant.PROFESSION));

                //工作单位
                this.cmbWorkAddress.AddItems(managerIntegrate.GetConstantList(EnumConstant.WORKNAME));

                //联系人信息
                this.cmbRelation.AddItems(managerIntegrate.GetConstantList(EnumConstant.RELATIVE));

                //联系人地址信息
                this.cmbLinkAddress.AddItems(managerIntegrate.GetConstantList(EnumConstant.AREA));

                //家庭住址信息
                this.cmbHomeAddress.AddItems(managerIntegrate.GetConstantList(EnumConstant.AREA));

                //籍贯
                this.cmbDistrict.AddItems(managerIntegrate.GetConstantList(EnumConstant.DIST));

                //开发渠道
                this.cmbDeveChannel.AddItems(managerIntegrate.QueryConstantList("DeveChannel"));

                //客服专员
                this.cmbCusService.AddItems(managerIntegrate.QueryConstantList("ServiceInfo"));

                //患者来源
                this.cmbPatientSource.AddItems(managerIntegrate.QueryConstantList("PatientSource"));

                this.dtpBirthDay.ValueChanged -= new EventHandler(dtpBirthDay_ValueChanged);

                this.txtYear.TextChanged += new EventHandler(txtBirthDay_TextChanged);
                this.txtMonth.TextChanged += new EventHandler(txtBirthDay_TextChanged);
                this.txtDays.TextChanged += new EventHandler(txtBirthDay_TextChanged);

                //生日
                //this.dtpBirthDay.Value = this.accountManager.GetDateTimeFromSysDateTime();//出生日期
                this.txtAge.TextChanged -= new EventHandler(txtAge_TextChanged);
                this.txtAge.Text = this.GetAge(0, 0, 0);// "  0岁 0月 0天";
                this.txtAge.TextChanged += new EventHandler(txtAge_TextChanged);
                this.dtpBirthDay.ValueChanged += new EventHandler(dtpBirthDay_ValueChanged);
                //地区
                this.cmbArea.AddItems(managerIntegrate.GetConstantList(EnumConstant.AREA));

                //合同单位{B71C3094-BDC8-4fe8-A6F1-7CEB2AEC55DD}
                //this.cmbPact.AddItems(managerIntegrate.GetConstantList(EnumConstant.PACTUNIT));
                this.ComPact.AddItems(managerIntegrate.QueryPactUnitOutPatient());
                this.ComPact.Tag = "1";

                //this.cmbPact.Text = "现金";
                //证件类型
                this.cmbCardType.AddItems(managerIntegrate.QueryConstantList("IDCard"));
                this.cmbCompany.AddItems(managerIntegrate.QueryConstantList("BICOMPANY"));
                this.cmbCardType.Text = "身份证";
                IdCardTypeHelp = new FS.FrameWork.Public.ObjectHelper(this.cmbCardType.alItems);

                FS.FrameWork.Management.ControlParam ctlParam = new FS.FrameWork.Management.ControlParam();

                //取卡规则 0 表示用病历号做卡号
                string returnValue = ctlParam.QueryControlerInfo("800006");

                FS.HISFC.BizProcess.Integrate.Common.ControlParam controlMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                this.isJudgePactByIdno = controlMgr.GetControlParam<bool>("HNMZ94", true, false);

                changePactId = controlMgr.GetControlParam<string>("HNMZ93", true, "");

                if (string.IsNullOrEmpty(returnValue))
                {
                    returnValue = "0";
                }

                this.McardNO = returnValue;
                CmbEvent();
                SetInputMenu();

                returnValue = ctlParam.QueryControlerInfo(FS.HISFC.BizProcess.Integrate.AccountConstant.SIPactUnitID);
                if (!string.IsNullOrEmpty(returnValue))
                {
                    lstPactID = new List<string>();
                    lstPactID.AddRange(returnValue.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries));
                }
                else
                {
                    lstPactID = new List<string>();
                }

                //MPI接口{BCE8D830-5FEA-4681-A08A-4BB48D172E20}
                //this.iEmpi = FS.HISFC.BizProcess.Integrate.PlatformInstance.GetIEmpiInstance();

                this.AddOrRemoveUnitAtMustInputLists(this.lblPact, this.ComPact, this.isInputPact);

                if (txtYear.Text.Trim() == "")
                {
                    this.txtMonth.TextChanged -= new EventHandler(txtBirthDay_TextChanged);
                    this.txtDays.TextChanged -= new EventHandler(txtBirthDay_TextChanged);

                    this.txtMonth.Text = "01"; //DateTime.Now.Month.ToString();
                    this.txtDays.Text = "01"; //DateTime.Now.Day.ToString();

                    this.txtMonth.TextChanged += new EventHandler(txtBirthDay_TextChanged);
                    this.txtDays.TextChanged += new EventHandler(txtBirthDay_TextChanged);

                    this.txtYear.Text = DateTime.Now.Year.ToString();

                }

            }
            catch (Exception e)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(e.Message);

                return -1;
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            return 1;
        }

        /// <summary>
        /// 根据参数判断是否按排序跳转
        /// </summary>
        /// <param name="isNeedChange"></param>
        private void ChangeTabIdex(bool isNeedChange)
        {
            if (isNeedChange)
            {
                foreach (Control control in this.panelControl.Controls)
                {
                    control.TabIndex = 0;
                }

                this.txtName.TabIndex = 1;
                this.cmbSex.TabIndex = 2;
                this.txtYear.TabIndex = 3;
                this.txtMonth.TabIndex = 4;
                this.txtDays.TabIndex = 5;
                this.txtAge.TabIndex = 6;
                this.cmbCardType.TabIndex = 7;
                this.txtIDNO.TabIndex = 8;
                this.cmbArea.TabIndex = 9;
                this.cmbProfession.TabIndex = 10;
                this.cmbPact.TabIndex = 11;// {71A043C7-ADEE-401e-81E2-FF32FBD39D78}
                this.txtSiNO.TabIndex = 12;
                this.cmbNation.TabIndex = 13;
                this.cmbCountry.TabIndex = 14;
                this.cmbMarry.TabIndex = 15;
                this.cmbDistrict.TabIndex = 16;
                this.txtHomePhone.TabIndex = 17;
                this.txtNowHome.TabIndex = 18;
                this.txtHomeAddrDoorNo.TabIndex = 19;
                this.txtHomeAddressZip.TabIndex = 20;
                this.cmbHomeAddress.TabIndex = 21;
                this.txtReMark.TabIndex = 22;
                this.cmbWorkAddress.TabIndex = 23;
                this.txtWorkPhone.TabIndex = 24;
                this.txtLinkMan.TabIndex = 25;
                this.cmbRelation.TabIndex = 26;
                this.txtLinkPhone.TabIndex = 27;
                this.cmbLinkAddress.TabIndex = 28;
                this.cmbDeveChannel.TabIndex = 29;
                this.txtEmail.TabIndex = 30;
                this.txtMatherName.TabIndex = 31;
                this.txtOtherCardNo.TabIndex = 32;
                this.cmbCusService.TabIndex = 33;
                this.cmbPatientSource.TabIndex = 34;
                this.txtReferralPerson.TabIndex = 35;
            }
        }

        /// <summary>
        /// 显示数据
        /// </summary>
        /// <param name="cardno">门诊卡号</param>
        private void SetInfo(string cardno)
        {
            this.Clear(false);
            if (cardno == string.Empty || cardno == null) return;
            this.patientInfo = radtManager.QueryComPatientInfo(cardno);
            if (accountManager.GetPatientPactInfo(this.patientInfo) == -1)
            {
                MessageBox.Show("获取一卡多身份的数据错误" + accountManager.Err);
                return;
            }
            //集成平台 嵌入主索引{BCE8D830-5FEA-4681-A08A-4BB48D172E20} 
            if (this.patientInfo == null && isLocalOperation == false)
            {
                //if (iEmpi != null )
                //{
                //    this.patientInfo = iEmpi.GetBasePatientinfo(FS.HISFC.BizProcess.Interface.Platform.HisDomain.Outpatient, cardno);                    
                //}
            }
            if (this.patientInfo != null)
            {
                SetPatient();
            }
            else
            {
                this.Clear(true);
            }
        }

        /// <summary>
        /// 显示患者基本信息
        /// </summary>
        /// 
        private void SetPatient()
        {
            //modify by sung 2009-2-24 {DCAA485E-753C-41ed-ABCF-ECE46CD41B33}
            if (this.patientInfo.IsEncrypt)
            {
                patientInfo.Name = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(patientInfo.NormalName);
            }
            this.txtName.Text = patientInfo.Name;//患者姓名

            //this.txtName.Text = this.patientInfo.Name;               //姓名
            //if (this.patientInfo.IsEncrypt)
            //{

            //    this.txtName.Tag = this.patientInfo.DecryptName;         //真实姓名                  
            //}
            //else
            //{
            //    this.txtName.Tag = null;
            //}
            this.cmbSex.Text = this.patientInfo.Sex.Name;            //性别
            this.cmbSex.Tag = this.patientInfo.Sex.ID;               //性别
            this.ComPact.Text = this.patientInfo.Pact.Name;          //合同单位名称
            this.ComPact.Tag = this.patientInfo.Pact.ID;             //合同单位ID
            //{6D7EC8BC-BDBB-4a47-BCFF-36BB0113499A}//添加商保公司
            this.cmbCompany.Text = this.patientInfo.Insurance.Name;//商业保险公司名称
            this.cmbCompany.Tag = this.patientInfo.Insurance.ID;//BICompanyID;//商业保险公司id
            
            this.cmbArea.Tag = this.patientInfo.AreaCode;            //地区
            this.cmbCountry.Tag = this.patientInfo.Country.ID;       //国籍
            this.cmbNation.Tag = this.patientInfo.Nationality.ID;    //民族
            //{BE0CBF3B-9CE8-42ca-8448-08CCF11755DF}
            //this.txtAge.Text = this.accountManager.GetAge(this.patientInfo.Birthday);//年龄
            if (this.patientInfo.Birthday > DateTime.MinValue)
            {
                //string Ages = this.accountManager.GetAge(this.patientInfo.Birthday);
                //避免出现年龄字符串阶段问题
                //this.txtAge.Text = Ages.Substring(0, Ages.Length - 1);
                this.dtpBirthDay.Value = this.patientInfo.Birthday;      //出生日期
            }
            else
            {

                this.dtpBirthDay.Value = this.accountManager.GetDateTimeFromSysDateTime();      //出生日期
            }
            this.cmbDistrict.Text = this.patientInfo.DIST;            //籍贯
            this.cmbProfession.Tag = this.patientInfo.Profession.ID; //职业
            this.txtIDNO.Text = this.patientInfo.IDCard;             //身份证号
            this.cmbWorkAddress.Text = this.patientInfo.CompanyName; //工作单位
            this.txtWorkPhone.Text = this.patientInfo.PhoneBusiness; //单位电话
            this.cmbMarry.Tag = this.patientInfo.MaritalStatus.ID.ToString();//婚姻状况
            this.cmbHomeAddress.Text = this.patientInfo.AddressHome;  //家庭住址
            this.txtNowHome.Text = this.patientInfo.User01;  //现住址
            this.txtHomePhone.Text = this.patientInfo.PhoneHome;     //家庭电话
            this.txtLinkMan.Text = this.patientInfo.Kin.Name;        //联系人 
            this.cmbRelation.Tag = this.patientInfo.Kin.Relation.ID; //联系人关系
            this.cmbLinkAddress.Text = this.patientInfo.Kin.RelationAddress;//联系人地址
            this.txtLinkPhone.Text = this.patientInfo.Kin.RelationPhone;//联系人电话
            this.ckEncrypt.Checked = this.patientInfo.IsEncrypt; //是否加密姓名
            this.ckVip.Checked = this.patientInfo.VipFlag;//是否vip
            this.txtMatherName.Text = this.patientInfo.MatherName;//母亲姓名
            this.cmbCardType.Tag = this.patientInfo.IDCardType.ID; //证件类型
            this.txtSiNO.Text = this.patientInfo.SSN;//社会保险号
            this.txtReMark.Text = this.patientInfo.Memo;//备注


            // {793CA9DB-FD85-460a-B8B4-971C31FFAD45}
            this.cmbPatientSource.Tag = this.patientInfo.PatientSourceInfo.ID;//患者来源
            this.cmbCusService.Tag = this.patientInfo.ServiceInfo.ID;//客服专员
            this.cmbDeveChannel.Tag = this.patientInfo.ChannelInfo.ID;//开发渠道
            this.txtOtherCardNo.Text = this.patientInfo.OtherCardNo;//其他卡号
            this.txtReferralPerson.Text = this.patientInfo.ReferralPerson;//转诊人
            //{05024624-3FF4-44B5-92BF-BD4C6FAF6897}添加儿童标签
            cbxChild.Checked = patientInfo.ChildFlag == "1";

            if (this.isMutilPactInfo)
            {
                if (patientInfo.MutiPactInfo != null)
                {
                    if (this.ComPact is ComboBoxPactSelect)
                    {
                        ((ComboBoxPactSelect)this.ComPact).AddValues(new ArrayList(patientInfo.MutiPactInfo));
                    }
                }
            }


            #region added by zhaoyang 2008-10-13
            txtHomeAddressZip.Text = this.patientInfo.HomeZip;//邮编
            txtHomeAddrDoorNo.Text = this.patientInfo.AddressHomeDoorNo;//家庭地址门牌号
            txtEmail.Text = this.patientInfo.Email;//电子邮件
            #endregion

            //查找图片
            byte[] photo = null;
            if (this.accountManager.GetIdenInfoPhoto(this.patientInfo.PID.CardNO, this.patientInfo.IDCardType.ID, ref photo) > 0 && photo != null)
            {
                System.IO.MemoryStream me = null;
                try
                {
                    me = new System.IO.MemoryStream(photo);
                    this.pictureBox.Image = Image.FromStream(me);
                    me.Close();
                }
                catch (Exception objEx)
                {
                    // MessageBox.Show("获取患者相片信息出错！" + objEx.Message);
                }
                finally
                {
                    if (me != null)
                    {
                        me.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 获取名字字符串
        /// </summary>
        /// <param name="patient"></param>
        private void GetPatienName(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            //选择加密
            if (ckEncrypt.Checked)
            {
                string name = string.Empty;
                if (this.txtName.Tag == null || this.txtName.Tag.ToString() == string.Empty)
                {
                    name = this.txtName.Text;
                }
                else
                {
                    name = this.txtName.Tag.ToString();
                }
                string encryptStr = FS.FrameWork.WinForms.Classes.Function.Encrypt3DES(name);

                patientInfo.Name = "******";
                patientInfo.NormalName = encryptStr;
                patientInfo.DecryptName = name;
            }
            else
            {
                patientInfo.Name = this.txtName.Text;
            }
        }

        private void CmbEvent()
        {
            foreach (Control c in this.panelControl.Controls)
            {
                c.Enter += new EventHandler(c_Enter);
            }
        }

        /// <summary>
        /// 控件获得焦点时响应的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void c_Enter(object sender, EventArgs e)
        {
            if (sender == this.txtName || sender == this.txtLinkMan || sender == cmbHomeAddress || sender == cmbLinkAddress || sender == this.txtMatherName || sender == this.cmbWorkAddress)
                InputLanguage.CurrentInputLanguage = CHInput;

            else
                InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
            if (CmbFoucs != null)
                this.CmbFoucs(sender, null);

        }

        /// <summary>
        /// 根据控件属性,判断是否在必须输入控件列表中加入或者删除该控件
        /// </summary>
        /// <param name="nameControl">名称控件</param>
        /// <param name="inputControl">输入控件</param>
        /// <param name="isMustInput">是否必须输入</param>
        private void AddOrRemoveUnitAtMustInputLists(Control nameControl, Control inputControl, bool isMustInput)
        {
            if (isMustInput)
            {
                if (!InputHasTable.ContainsKey(nameControl))
                {
                    InputHasTable.Add(nameControl, inputControl);
                    nameControl.ForeColor = Color.Blue;
                }
            }
            else
            {
                if (InputHasTable.ContainsKey(nameControl))
                {
                    InputHasTable.Remove(nameControl);
                    nameControl.ForeColor = Color.Black;
                }
            }
            inpubMaxTabIndex = 0;
            foreach (DictionaryEntry de in InputHasTable)
            {
                Control c = de.Value as Control;
                //获取最大的tabIndex
                if (inpubMaxTabIndex < c.TabIndex)
                {
                    inpubMaxTabIndex = c.TabIndex;
                }
            }
        }

        /// <summary>
        /// 根据控件属性,判断是否在必须输入控件列表中加入或者删除该控件
        /// </summary>
        /// <param name="enableControl">输入控件</param>
        /// <param name="isEnable">是否可以编辑</param>
        private void AddOrRemoveUnitAtEnableLists(Control enableControl, bool isEnable)
        {
            if (isEnable)
            {
                if (EnableControlList.Contains(enableControl))
                {
                    EnableControlList.Remove(enableControl);
                    enableControl.Enabled = true;
                }
            }
            if (!isEnable)
            {
                if (!EnableControlList.Contains(enableControl))
                {
                    EnableControlList.Add(enableControl);
                    enableControl.Enabled = false;
                }
            }
        }

        #region 病案室默认值设置
        ///// <summary>
        ///// 保存病案默认科室
        ///// </summary>
        ///// <param name="deptCode"></param>
        //private void SaveCasDeptdefautValue(string deptCode)
        //{
        //    if (!System.IO.File.Exists(filePath))
        //    {
        //        this.CreateXml();
        //    }
        //    XmlDocument doc = new XmlDocument();
        //    doc.Load(filePath);
        //    XmlNode xn = doc.SelectSingleNode("//DefaultValue");
        //    xn.InnerText = deptCode;
        //    doc.Save(filePath);
        //}

        ///// <summary>
        ///// 建立xml
        ///// </summary>
        //private void CreateXml()
        //{
        //    XmlDocument doc = new XmlDocument();
        //    doc.LoadXml("<setting>  </setting>");
        //    XmlNode xn = doc.DocumentElement;
        //    XmlComment xc = doc.CreateComment("门诊患者信息录入病案室默认值");
        //    XmlElement xe = doc.CreateElement("DefaultValue");
        //    xn.AppendChild(xc);
        //    xn.AppendChild(xe);
        //    doc.Save(filePath);
        //}

        ///// <summary>
        ///// 读取病案默认值
        ///// </summary>
        ///// <returns></returns>
        //private string ReadCaseDept()
        //{
        //    if (!System.IO.File.Exists(filePath))
        //    {
        //        this.CreateXml();
        //        return string.Empty;
        //    }
        //    XmlDocument doc = new XmlDocument();
        //    doc.Load(filePath);
        //    XmlNode xn = doc.SelectSingleNode("//DefaultValue");
        //    return xn.InnerText;
        //}
        #endregion
        #endregion

        #region 方法

        /// <summary>
        /// 获得焦点
        /// </summary>
        /// <returns></returns>
        public new bool Focus()
        {
            return this.txtName.Focus();
        }

        /// <summary>
        /// 清空数据
        /// </summary>
        public virtual void Clear(bool isClearCardNO)
        {
            if (isClearCardNO)
            {
                this.CardNO = string.Empty;
            }


            foreach (Control c in this.panelControl.Controls)
            {
                if (c is FS.FrameWork.WinForms.Controls.NeuComboBox ||
                    c is FrameWork.WinForms.Controls.NeuTextBox)
                {
                    c.Text = string.Empty;
                    c.Tag = string.Empty;
                }
            }

            this.txtAge.ReadOnly = false;
            this.ckEncrypt.Checked = false;
            this.cmbCountry.Text = "中国";

            //{2C77F7F4-49BF-4a36-BE98-90BF9E1947B8}
            //性别默认为空
            //this.cmbSex.Text = "";
            //this.cmbSex.Tag = "";
            this.cmbSex.Tag = "F";
            if (this.cmbCardType.alItems.Count > 0 && this.cmbCardType.alItems[0] is NeuObject)
            {
                this.cmbCardType.Tag = (this.cmbCardType.alItems[0] as NeuObject).ID;
            }
            //this.cmbNation.Text = "族";
            this.cmbNation.Tag = "1";
            if (isMutilPactInfo)
            {
                if (this.ComPact is ComboBoxPactSelect)
                {
                    ((ComboBoxPactSelect)this.ComPact).Clear();
                }
            }
            this.ComPact.Tag = "1";
            this.dtpBirthDay.ValueChanged -= new EventHandler(dtpBirthDay_ValueChanged);
            this.txtAge.TextChanged -= new EventHandler(txtAge_TextChanged);
            this.dtpBirthDay.Value = this.accountManager.GetDateTimeFromSysDateTime();//出生日期

            this.txtAge.Text = this.GetAge(0, 0, 0);// "  0岁 0月 0天";
            this.txtAge.TextChanged += new EventHandler(txtAge_TextChanged);
            this.dtpBirthDay.ValueChanged += new EventHandler(dtpBirthDay_ValueChanged);
            //this.txtYear.Text = DateTime.Now.Year.ToString();
            //this.txtMonth.Text = "1";
            //this.txtDays.Text = "1";
            this.ckVip.Checked = false;
            //{05024624-3FF4-44B5-92BF-BD4C6FAF6897}添加儿童标签
            this.cbxChild.Checked = false;
            this.pictureBox.Image = null;
            this.pictureBox.Tag = null;
            this.patientInfo = null;
        }

        /// <summary>
        /// 数据合理化校验
        /// </summary>
        /// <returns></returns>
        public virtual bool InputValid()
        {
            //判断必须输入的控件是否都已经输入信息
            foreach (DictionaryEntry d in this.InputHasTable)
            {
                if (d.Value is FS.FrameWork.WinForms.Controls.NeuComboBox)
                {
                    if (((Control)d.Value).Tag == null || ((Control)d.Value).Tag.ToString() == string.Empty || ((Control)d.Value).Text.Trim() == string.Empty)
                    {
                        MessageBox.Show(((Control)d.Key).Text.Replace(':', ' ') + Language.Msg("必须输入信息!"));
                        ((Control)d.Value).Focus();

                        return false;
                    }
                }
                else
                {
                    if (((Control)d.Value).Text == string.Empty)
                    {
                        MessageBox.Show(((Control)d.Key).Text.Replace(':', ' ') + Language.Msg("必须输入信息!"));
                        ((Control)d.Value).Focus();

                        return false;
                    }
                }
            }
            //{05024624-3FF4-44B5-92BF-BD4C6FAF6897}添加儿童标签 儿童的必须填写联系人电话 非儿童则必须填写联系电话 且联系电话不能重复
            if (cbxChild.Checked)
            {
                if (string.IsNullOrEmpty(txtLinkMan.Text) || string.IsNullOrEmpty(txtLinkPhone.Text))
                {
                    string strMsg = Language.Msg("联系人电话或姓名不能为空！");
                    MessageBox.Show(strMsg, "系统提示", MessageBoxButtons.OK);
                    this.txtLinkPhone.Focus();
                    return false;
                }
                else
                {

                    Regex rx = new Regex(@"^0{0,1}(13[0-9]|14[0-9]|15[7-9]|15[0-6]|16[0-9]|17[0-9]|18[0-9]|19[0-9])[0-9]{8}$");
                    if (!rx.IsMatch(txtLinkPhone.Text)) //不匹配
                    {
                        txtLinkPhone.Text = ""; //变成空
                        MessageBox.Show("联系人手机号格式不对，请重新输入！");    //弹框提示
                        txtLinkPhone.Focus();
                        return false;
                    }


                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtHomePhone.Text))
                {
                    string strMsg = Language.Msg("联系电话不能为空！");
                    MessageBox.Show(strMsg, "系统提示", MessageBoxButtons.OK);
                    this.txtHomePhone.Focus();
                    return false;
                }
                else
                {
                    Regex rx = new Regex(@"^0{0,1}(13[0-9]|14[0-9]|15[7-9]|15[0-6]|16[0-9]|17[0-9]|18[0-9]|19[0-9])[0-9]{8}$");
                    if (!rx.IsMatch(txtHomePhone.Text)) //不匹配
                    {
                        txtHomePhone.Text = ""; //变成空
                        MessageBox.Show("联系人手机号格式不对，请重新输入！");    //弹框提示
                        txtHomePhone.Focus();
                        return false;
                    }

                    
                }
            }
            if (!string.IsNullOrEmpty(txtHomePhone.Text)) //{05024624-3FF4-44B5-92BF-BD4C6FAF6897}添加儿童标签 儿童的必须填写联系人电话 非儿童则必须填写联系电话 且联系电话不能重复
            {
                List<FS.HISFC.Models.RADT.PatientInfo> list = new List<FS.HISFC.Models.RADT.PatientInfo>();
                list = this.accountManager.QueryPatientInfoByWhere("", "", txtHomePhone.Text, "");// {CDB01BF4-B40F-4cdc-9F0D-23F074290136}

                if (list != null && list.Count > 0)// {06A6BD92-97A6-4f01-8B6E-533B35CBF2DD}
                {
                    FS.HISFC.Models.RADT.PatientInfo ac = list[0];
                    if (this.patientInfo != null  )
                    {
                        if (string.IsNullOrEmpty(patientInfo.PID.CardNO))
                        {
                            string  strMsg = Language.Msg(string.Format("已存在相同【联系电话】,病历号为：{0}", ac.PID.CardNO));
                            MessageBox.Show(strMsg);
                            return false;
                        }
                        else
                        {
                            if (list.Count > 1)
                            {
                                string strMsg = null;
                                if (list[0].PID.CardNO == patientInfo.PID.CardNO)
                                {

                                    strMsg = Language.Msg(string.Format("已存在相同【联系电话】,病历号为：{0}", list[1].PID.CardNO));
                                }
                                else
                                {
                                    strMsg = Language.Msg(string.Format("已存在相同【联系电话】,病历号为：{0}", list[0].PID.CardNO));
                                }
                                MessageBox.Show(strMsg);
                                return false;
                            }
                            else
                            {
                                if (list[0].PID.CardNO != patientInfo.PID.CardNO)
                                {
                                    string strMsg = Language.Msg(string.Format("已存在相同【联系电话】,病历号为：{0}", list[0].PID.CardNO));
                                    MessageBox.Show(strMsg);
                                    return false;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (list.Count > 0)
                        {
                            string strMsg = Language.Msg(string.Format("已存在相同【联系电话】,病历号为：{0}", list[0].PID.CardNO));
                            MessageBox.Show(strMsg);
                            return false;
                        }
                    }
                    //if (MessageBox.Show(strMsg, "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                    //{
                    //    return false;
                    //}
                }
            }
            if (!this.ckEncrypt.Checked && this.txtName.Text == "******")
            {
                MessageBox.Show(Language.Msg("该患者姓名没有加密，请输入正确的患者姓名！"));
                this.txtName.Focus();
                this.txtName.SelectAll();
                return false;
            }

            //
            // 增加姓名判断，防止办卡时，查询并显示患者信息基础下，未清空患者信息就办卡
            // 
            if (this.patientInfo != null && !this.IsEditMode)
            {
                string patName = this.txtName.Text.Trim();
                if (!string.IsNullOrEmpty(patientInfo.Name) && !string.IsNullOrEmpty(patName))
                {
                    if (patientInfo.Name.Trim() != patName)
                    {
                        string strMsg = Language.Msg("输入患者姓名与所选患者姓名不同，新办卡请清空患者信息！");
                        MessageBox.Show(strMsg, "系统提示", MessageBoxButtons.OK);
                        this.txtIDNO.Focus();
                        return false;
                    }
                }
            }

            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtName.Text, 50))
            {
                MessageBox.Show(Language.Msg("姓名录入超长！"));
                this.txtName.Focus();
                return false;
            }

            //判断字符超长医疗证号
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtSiNO.Text, 20))
            {
                MessageBox.Show(Language.Msg("医疗证号录入超长！"));
                this.txtSiNO.Focus();
                return false;
            }
            //判断字符超长籍贯
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.cmbDistrict.Text, 50))
            {
                MessageBox.Show(Language.Msg("籍贯录入超长！"));
                this.cmbDistrict.Focus();
                return false;
            }
            //判断字符超长证件号
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtIDNO.Text, 20))
            {
                MessageBox.Show(Language.Msg("证件号录入超长！"));
                this.txtIDNO.Focus();
                return false;
            }

            //判断身份证号// {D59C1D74-CE65-424a-9CB3-7F9174664504}
            if (this.cmbCardType.Tag != null && this.cmbCardType.Tag.ToString() == "01" && this.txtIDNO.Text.Trim() != string.Empty)
            {
                string err = string.Empty;
                if (FS.FrameWork.WinForms.Classes.Function.CheckIDInfo(this.txtIDNO.Text.Trim(), ref err) < 0)
                {
                    if (isTooltipCARDID == 1)// {71A043C7-ADEE-401e-81E2-FF32FBD39D78}
                    {
                        string strMsg = Language.Msg(err + "\r\n是否继续？");
                        if (MessageBox.Show(strMsg, "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                        {
                            this.txtIDNO.Focus();
                            return false;
                        }
                    }
                    else if (isTooltipCARDID == 2)
                    {

                        string strMsg = Language.Msg(err + "\r\n不允许办理");

                        MessageBox.Show(strMsg, "系统提示");
                        this.txtIDNO.Focus();
                        return false;
                    }
                    else
                    {
                    }
                }
            }

            if (this.isMutilPactInfo)
            {
                if (this.ComPact is ComboBoxPactSelect)
                {
                    ArrayList arl = ((ComboBoxPactSelect)this.ComPact).GetValues();
                    if (arl == null || arl.Count <= 0)
                    {
                        MessageBox.Show(Language.Msg("请选择患者合同单位！"));
                        this.ComPact.Focus();
                        return false;
                    }
                }
            }


            //判断字符超长工作单位
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.cmbWorkAddress.Text, 50))
            {
                MessageBox.Show(Language.Msg("工作单位录入超长！"));
                this.cmbWorkAddress.Focus();
                return false;
            }

            //判断单位电话长度
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtWorkPhone.Text, 30))
            {
                MessageBox.Show(Language.Msg("单位电话号码录入超长"));
                this.txtWorkPhone.Focus();
                return false;
            }

            //判断家庭地址长度
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.cmbHomeAddress.Text, 100))
            {
                MessageBox.Show(Language.Msg("家庭地址录入超长"));
                this.cmbHomeAddress.Focus();
                return false;
            }

            //判断家庭电话号码长度
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtHomePhone.Text, 30))
            {
                MessageBox.Show(Language.Msg("家庭电话号码录入超长"));
                this.txtHomePhone.Focus();
                return false;
            }

            //判断联系电话号码长度
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtLinkPhone.Text, 30))
            {
                MessageBox.Show(Language.Msg("联系人电话号码录入超长"));
                this.txtLinkPhone.Focus();
                return false;
            }
            //判断联系联系人长度
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtLinkMan.Text, 50))
            {
                MessageBox.Show(Language.Msg("联系人录入超长"));
                this.txtLinkMan.Focus();
                return false;
            }
            //联系人地址
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.cmbLinkAddress.Text, 100))
            {
                MessageBox.Show(Language.Msg("联系人地址录入超长"));
                this.cmbLinkAddress.Focus();
                return false;
            }

            //母亲姓名
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtMatherName.Text, 50))
            {
                MessageBox.Show(Language.Msg("母亲姓名录入超长"));
                this.txtMatherName.Focus();
                return false;
            }

            if (this.dtpBirthDay.Value.Date > this.accountManager.GetDateTimeFromSysDateTime().Date)
            {
                MessageBox.Show(Language.Msg("出生日期大于当前日期,请重新输入!"));
                this.dtpBirthDay.Focus();
                return false;
            }

            #region added by zhaoyang 2008-10-13
            if (string.IsNullOrEmpty(txtEmail.Text) == false)
            {
                Regex r = new Regex("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$");//{187A73EB-008A-4A25-A6CB-28CAE0E629A7}添加邮箱正则匹配
                if (r.IsMatch(txtEmail.Text.Trim()) == false)
                {
                    txtEmail.Focus();
                    txtEmail.SelectAll();
                    MessageBox.Show("电子邮箱输入格式错误，请更改或重新输入。");
                    return false;
                }
                //if (NFC.Public.String.isMail(txtEmail.Text.Trim()) == false)
                //{
                //    txtEmail.Focus();
                //    txtEmail.SelectAll();
                //    MessageBox.Show("电子邮箱输入格式错误，请更改或重新输入。");
                //    return false;
                //}
                if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtEmail.Text, 50))
                {
                    MessageBox.Show(Language.Msg("电子邮箱录入超长!"));
                    txtEmail.Focus();
                    txtEmail.SelectAll();
                    return false;
                }
            }
            //else
            //{
            //    MessageBox.Show(Language.Msg("邮箱不能为空!"));
            //    return false;
            //}
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtHomeAddrDoorNo.Text, 40))
            {
                MessageBox.Show(Language.Msg("门牌号录入超长！"));
                txtHomeAddrDoorNo.SelectAll();
                txtHomeAddrDoorNo.Focus();
                return false;
            }

            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtHomeAddressZip.Text, 6))
            {
                MessageBox.Show(Language.Msg("邮编录入超长！"));
                txtHomeAddressZip.SelectAll();
                txtHomeAddressZip.Focus();
                return false;
            }
            #endregion

            #region 不能同时为空项目

            if (iMustInpubByOne != 0)
            {
                if (iMustInpubByOne == 1)
                {
                    if (string.IsNullOrEmpty(txtIDNO.Text.Trim()) && string.IsNullOrEmpty(txtHomePhone.Text.Trim()))
                    {
                        MessageBox.Show(Language.Msg("证件号与电话号码不能同时为空！"));
                        this.txtIDNO.Focus();
                        return false;
                    }
                }
            }

            #endregion

            #region 医保身份判断证件号
            string strPactID = this.ComPact.Tag == null ? "" : this.ComPact.Tag.ToString().Trim();
            if (!string.IsNullOrEmpty(strPactID))
            {
                if (lstPactID.Contains(strPactID))
                {
                    string strTemp = txtIDNO.Text.Trim();
                    if (string.IsNullOrEmpty(strTemp))
                    {
                        MessageBox.Show(Language.Msg("医保患者，证件号不能为空！"));
                        txtIDNO.Focus();
                        return false;
                    }
                }
            }


            #endregion

            #region 办卡时是否实时判断，是否享受相应的合同单位
            // 办卡时是否实时判断，是否享受相应的合同单位
            // {C49AFFB1-D0EA-41bf-AD60-9F921D91E93D}
            if (this.isJudgePact)
            {
                FS.HISFC.Models.Registration.Register r = new FS.HISFC.Models.Registration.Register();
                r.IDCard = this.txtIDNO.Text.Trim();
                r.Pact.ID = this.ComPact.Tag.ToString();
                r.Pact.Name = this.ComPact.Text.ToString();

                ArrayList al = new ArrayList();
                if (this.isMutilPactInfo)
                {
                    if (this.ComPact is ComboBoxPactSelect)
                    {
                        al = ((ComboBoxPactSelect)this.ComPact).GetValues();
                    }
                }
                al.Add(r.Pact);
                if (al != null)
                {
                    foreach (FS.HISFC.Models.Base.PactInfo pactinfo in al)
                    {
                        if (lstPactID.Contains(pactinfo.ID))
                        {
                            string strTemp = txtIDNO.Text.Trim();
                            if (string.IsNullOrEmpty(strTemp))
                            {
                                MessageBox.Show(Language.Msg("患者多个身份包含医保类别，证件号不能为空！"));
                                txtIDNO.Focus();
                                return false;
                            }
                        }
                        if (this.ValidHospitalStaff(r) == false)
                        {
                            return false;
                        }
                        r.IDCard = this.txtIDNO.Text.Trim();
                        r.Pact = pactinfo;
                        if (this.InputValid(r) == false)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return this.InputValid(r);
                }


            }
            #endregion
            //{6D7EC8BC-BDBB-4a47-BCFF-36BB0113499A}//如果是商保客户则必须添加商保公司
            if (this.cmbPact.Tag.ToString() == "2")
            {
                if (string.IsNullOrEmpty(cmbCompany.Tag.ToString()))
                {
                    MessageBox.Show(Language.Msg("商保客户必须选择商保公司,请重新输入!"));
                    cmbCompany.Focus();
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 判断本院职工合同单位
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        private bool ValidHospitalStaff(FS.HISFC.Models.Registration.Register r)
        {
            if (string.IsNullOrEmpty(isValidHospitalStaff))
            {
                return true;
            }
            else
            {
                String[] arr = isValidHospitalStaff.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                pactList.AddRange(arr);
                if (pactList.Contains(r.Pact.ID))
                {
                    if (r == null || string.IsNullOrEmpty(r.IDCard))
                    {
                        MessageBox.Show(Language.Msg("本院职工参数信息为空或身份证错误"));
                        return false;
                    }

                    if (string.IsNullOrEmpty(r.Pact.ID))
                    {
                        MessageBox.Show(Language.Msg("本院职工参数信息不正确"));
                        return false;
                    }
                    string sql = "select count(1) from com_employee where IDENNO='{0}'";

                    FS.FrameWork.Management.DataBaseManger mgrManager = new FS.FrameWork.Management.DataBaseManger();
                    string retStr = mgrManager.ExecSqlReturnOne(string.Format(sql, r.IDCard));

                    int i = FS.FrameWork.Function.NConvert.ToInt32(retStr);

                    if (i <= 0)
                    {
                        MessageBox.Show(Language.Msg("非本院职工，不能选择本院职工类型"));
                        return false;
                    }
                }
                return true;
            }

        }

        /// <summary>
        /// 合同单位验证
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        private bool InputValid(FS.HISFC.Models.Registration.Register r)
        {
            if (!string.IsNullOrEmpty(r.IDCard) && !string.IsNullOrEmpty(r.Pact.ID))
            {
                int iRes = this.medcareProxy.SetPactCode(r.Pact.ID);
                if (iRes != 1)
                {
                    MessageBox.Show(Language.Msg(this.medcareProxy.ErrMsg), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ComPact.Focus();
                    return false;
                }

                iRes = this.medcareProxy.QueryCanMedicare(r);
                if (iRes == -2)
                {
                    MessageBox.Show(Language.Msg(this.medcareProxy.ErrMsg + "请修改合同单位 \r\n" + this.medcareProxy.ErrMsg), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ComPact.Focus();
                    this.ComPact.Text = "普通";
                    this.ComPact.Tag = "1";
                    return false;
                }
                else if (iRes == -1)
                {
                    MessageBox.Show(Language.Msg(this.medcareProxy.ErrMsg + "请修改合同单位\r\n" + this.medcareProxy.ErrMsg), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ComPact.Focus();
                    this.ComPact.Text = "普通";
                    this.ComPact.Tag = "1";
                    return false;
                }

                ////调用2次医保判断身份方法，怀疑医保第一次判断身份出错，有些以前办过医保的自费患者也办医保
                //iRes = this.medcareProxy.QueryCanMedicare(r);
                //if (iRes == -2)
                //{
                //    MessageBox.Show(Language.Msg(this.medcareProxy.ErrMsg + "请修改合同单位"), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    this.ComPact.Focus();
                //    this.ComPact.Text = "普通";
                //    this.ComPact.Tag = "1";
                //    return false;
                //}
                //else if (iRes == -1)
                //{
                //    MessageBox.Show(Language.Msg(this.medcareProxy.ErrMsg + "请修改合同单位\r\n" + this.medcareProxy.ErrMsg), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    this.ComPact.Focus();
                //    this.ComPact.Text = "普通";
                //    this.ComPact.Tag = "1";
                //    return false;
                //}
            }

            return true;
        }

        /// <summary>
        /// 通过合同单位编码,获得结算类别实体
        /// </summary>
        /// <param name="pactID">合同单位编码</param>
        /// <returns>成功: 结算类别实体 失败: null</returns>
        private PayKind GetPayKindFromPactID(string pactID)
        {
            FS.HISFC.Models.Base.PactInfo pact = this.pactManager.GetPactUnitInfoByPactCode(pactID);
            if (pact == null)
            {
                MessageBox.Show(Language.Msg("获得合同单位详细出错!"));

                return null;
            }

            return pact.PayKind;
        }

        /// <summary>
        /// 获得患者实体
        /// </summary>
        /// <returns></returns>
        public FS.HISFC.Models.RADT.PatientInfo GetPatientInfomation()
        {
            //刷新患者基本信息
            patientInfo = managerIntegrate.QueryComPatientInfo(cardNO);
            //集成平台 嵌入主索引{BCE8D830-5FEA-4681-A08A-4BB48D172E20}
            if (this.patientInfo == null && isLocalOperation == false)
            {
                //if (iEmpi != null)
                //{
                //    this.patientInfo = iEmpi.GetBasePatientinfo(FS.HISFC.BizProcess.Interface.Platform.HisDomain.Outpatient, cardNO);
                //}
            }
            if (patientInfo == null)
            {
                patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
            }

            patientInfo.PID.CardNO = cardNO;//门诊卡号
            if (this.ComPact.Tag != null && this.ComPact.Tag.ToString() != string.Empty)
                patientInfo.Pact.PayKind = GetPayKindFromPactID(this.ComPact.Tag.ToString());//结算类别

            patientInfo.Pact.ID = this.ComPact.Tag.ToString();//合同单位 
            //{6D7EC8BC-BDBB-4a47-BCFF-36BB0113499A}//添加商保公司
            patientInfo.Pact.Name = this.ComPact.Text.ToString();//合同单位名称
            patientInfo.Insurance.ID = cmbCompany.Tag.ToString();
            patientInfo.Insurance.Name = cmbCompany.Text;
            //patientInfo.BICompanyID = cmbCompany.Tag.ToString();//商业保险公司
            //patientInfo.BICompanyName = cmbCompany.Text;//商业保险公司名称
            if (!this.isTreatment)
            {
                this.GetPatienName(patientInfo); //患者姓名
                patientInfo.IsEncrypt = this.ckEncrypt.Checked; //是否加密
            }
            else
            {
                this.patientInfo.Name = "无名氏";
                patientInfo.IsEncrypt = false;
            }
            patientInfo.Sex.ID = this.cmbSex.Tag.ToString();//性别
            patientInfo.AreaCode = this.cmbArea.Tag.ToString();//地区
            patientInfo.Country.ID = this.cmbCountry.Tag.ToString();//国籍
            patientInfo.Nationality.ID = this.cmbNation.Tag.ToString();//民族
            patientInfo.Birthday = this.dtpBirthDay.Value;//出生日期
            patientInfo.Age = outpatientManager.GetAge(this.dtpBirthDay.Value);//年龄
            patientInfo.DIST = this.cmbDistrict.Text.ToString();//籍贯
            patientInfo.Profession.ID = this.cmbProfession.Tag.ToString();//职业
            patientInfo.IDCard = this.txtIDNO.Text;//证件号
            patientInfo.IDCardType.ID = this.cmbCardType.Tag.ToString();//证件类型
            patientInfo.IDCardType.Name = this.cmbCardType.Text;
            patientInfo.CompanyName = this.cmbWorkAddress.Text.Trim();//工作单位
            patientInfo.PhoneBusiness = this.txtWorkPhone.Text.Trim();//单位电话
            patientInfo.MaritalStatus.ID = this.cmbMarry.Tag.ToString();//婚姻状况 
            patientInfo.AddressHome = this.cmbHomeAddress.Text.Trim();//家庭住址
            patientInfo.User01 = this.txtNowHome.Text.Trim();//现住址
            patientInfo.PhoneHome = this.txtHomePhone.Text.Trim();//家庭电话
            patientInfo.Kin.Name = this.txtLinkMan.Text.Trim();//联系人 
            patientInfo.Kin.Relation.ID = this.cmbRelation.Tag.ToString();//与联系人关系
            patientInfo.Kin.RelationAddress = this.cmbLinkAddress.Text;//联系人地址
            patientInfo.Kin.RelationPhone = this.txtLinkPhone.Text.Trim();  //联系人电话
            patientInfo.VipFlag = this.ckVip.Checked; //是否vip
            patientInfo.MatherName = this.txtMatherName.Text;//母亲姓名
            patientInfo.IsTreatment = this.IsTreatment;//是否急诊
            patientInfo.SSN = this.txtSiNO.Text;//社会保险号
            patientInfo.Memo = this.txtReMark.Text;//备注

            // {793CA9DB-FD85-460a-B8B4-971C31FFAD45}
            patientInfo.ReferralPerson = this.txtReferralPerson.Text.Trim();//转诊人
            patientInfo.OtherCardNo = this.txtOtherCardNo.Text.Trim();//其他卡号
            patientInfo.PatientSourceInfo.ID = this.cmbPatientSource.Tag.ToString();//患者来源
            patientInfo.PatientSourceInfo.Name = this.cmbPatientSource.Text;
            patientInfo.ServiceInfo.ID = this.cmbCusService.Tag.ToString();//客服专员
            patientInfo.ServiceInfo.Name = this.cmbCusService.Text;
            patientInfo.ChannelInfo.ID = this.cmbDeveChannel.Tag.ToString();//开发渠道
            patientInfo.ChannelInfo.Name = this.cmbDeveChannel.Text;
            //{05024624-3FF4-44B5-92BF-BD4C6FAF6897}添加儿童标签
            patientInfo.ChildFlag = cbxChild.Checked ? "1" : "0";


            //一卡多身份
            if (this.isMutilPactInfo)
            {
                patientInfo.MutiPactInfo = new List<PactInfo>();
                ArrayList al = new ArrayList();
                if (this.ComPact is ComboBoxPactSelect)
                {
                    al = ((ComboBoxPactSelect)this.ComPact).GetValues();
                }
                if (al != null)
                {
                    foreach (FS.HISFC.Models.Base.PactInfo pactinfo in al)
                    {
                        patientInfo.MutiPactInfo.Add(pactinfo);
                    }
                }
            }

            #region added by zhaoyang 2008-10-13
            patientInfo.HomeZip = this.txtHomeAddressZip.Text.Trim();//邮编
            patientInfo.AddressHomeDoorNo = txtHomeAddrDoorNo.Text.Trim();//家庭地址门牌号
            patientInfo.Email = txtEmail.Text.Trim();//电子邮箱
            #endregion
            return patientInfo;
        }

        /// <summary>
        /// 保存患者数据
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            // 去除判断，在外面调用
            ////普通患者就诊卡发放
            //if (!IsTreatment)
            //{
            //    if (!InputValid()) return -1;

            //}

            this.patientInfo = this.GetPatientInfomation();

            if (patientInfo.Pact.PayKind.ID == "02")
            {
                ////当患者是医保护患者时姓名、性别、医保证号不能为空
                //if (this.txtName.Text == string.Empty || this.txtSiNO.Text == string.Empty
                //    || this.cmbSex.Tag == null || this.cmbSex.Tag.ToString() == string.Empty)
                //{
                //    MessageBox.Show("该患者是医保患者，姓名、性别、医保证号不能为空！", "提示");
                //    return -1;
                //}

                // 当患者是医保护患者时姓名、性别 不能为空
                if (this.txtName.Text == string.Empty
                    || this.cmbSex.Tag == null || this.cmbSex.Tag.ToString() == string.Empty)
                {
                    MessageBox.Show("该患者是医保患者，姓名、性别不能为空！", "提示");
                    return -1;
                }
            }

            if (string.IsNullOrEmpty(patientInfo.PID.CardNO))
            {
                //集成平台 处理主索引{BCE8D830-5FEA-4681-A08A-4BB48D172E20}
                //if (this.iEmpi != null && isLocalOperation == false)
                //{
                //    if (iEmpi.GetDomainID(FS.HISFC.BizProcess.Interface.Platform.HisDomain.Outpatient, patientInfo, false, ref cardNO) == -1)
                //    {
                //        MessageBox.Show("由数据中心获取新病例卡号发生错误" + iEmpi.Message);
                //        return -1;
                //    }
                //    if (string.IsNullOrEmpty(cardNO))
                //    {
                //        cardNO = outpatientManager.GetAutoCardNO();
                //        cardNO = cardNO.PadLeft(HISFC.BizProcess.Integrate.Common.ControlParam.GetCardNOLen(), '0');
                //    }
                //}
                //else
                //{
                //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
                if (!this.cardWay)
                {
                    cardNO = this.McardNO;
                }
                else if (isAutoBuildCardNo)
                {
                    cardNO = autoCardNo;
                }
                else
                {
                    cardNO = outpatientManager.GetAutoCardNO();
                }

                //cardNO = cardNO.PadLeft(HISFC.BizProcess.Integrate.Common.ControlParam.GetCardNOLen(), '0');
                cardNO = cardNO.PadLeft(10, '0');
                //}
            }
            else
            {
                cardNO = patientInfo.PID.CardNO;
            }
            patientInfo.PID.CardNO = cardNO;

            if (radtManager.RegisterComPatient(patientInfo) < 0)
            {
                MessageBox.Show(radtManager.Err);
                return -1;
            }

            if (accountManager.InsertPatientPactInfo(patientInfo) < 0)
            {
                MessageBox.Show(radtManager.Err);
                return -1;
            }


            if (!string.IsNullOrEmpty(patientInfo.IDCardType.ID) && !string.IsNullOrEmpty(patientInfo.IDCard))
            {

                if (accountManager.InsertIdenInfo(patientInfo) == -1)
                {
                    if (this.accountManager.DBErrCode == 1)
                    {
                        if (accountManager.UpdateIdenInfo(patientInfo) == -1)
                        {
                            MessageBox.Show(accountManager.Err);
                            return -1;
                        }
                    }
                }

                //加入照片
                if (this.pictureBox.Image != null)
                {
                    System.IO.MemoryStream m = new System.IO.MemoryStream();
                    if (this.pictureBox.Tag != null)
                    {
                        Image img = Image.FromFile(this.pictureBox.Tag.ToString());
                        if (img != null)
                        {
                            img.Save(m, System.Drawing.Imaging.ImageFormat.Bmp);
                            img.Dispose();
                            if (m != null)
                            {
                                if (accountManager.UpdatePhoto(patientInfo, m.ToArray()) == -1)
                                {
                                    m.Close();
                                    MessageBox.Show(accountManager.Err);
                                    return -1;
                                }

                                m.Close();
                            }
                        }
                    }


                }
            }

            //集成平台 处理主索引{BCE8D830-5FEA-4681-A08A-4BB48D172E20}
            //if (this.iEmpi != null && isLocalOperation == false)
            //{
            //    if (iEmpi.CreateOrUpdatePatient(FS.HISFC.BizProcess.Interface.Platform.HisDomain.Outpatient, patientInfo) == -1)
            //    {
            //        MessageBox.Show("创建或更新患者主索引信息出错" + iEmpi.Message);
            //        return -1;
            //    }
            //}
            return 1;
        }

        /// <summary>
        /// 根据ID获得名称
        /// </summary>
        /// <param name="ID">民族ID</param>
        /// <param name="aMod">0:民族 1:证件类型</param>
        /// <returns></returns>
        public string GetName(string ID, int aMod)
        {
            if (aMod == 0)
            {
                return NationHelp.GetName(ID);
            }
            else
            {
                return IdCardTypeHelp.GetName(ID);
            }
        }

        /// <summary>
        /// 根据必须输入控件跳转输入焦点
        /// </summary>
        private void SetMustInputFocus(Control currentControl)
        {
            if (currentControl == null)
            {
                SendKeys.Send("{Tab}");
                return;
            }
            //查找下应当等到焦点必须输入的控件
            Control tempControl = this.NextFocusControl(currentControl);
            if (tempControl != null && tempControl.CanFocus)
            {
                tempControl.Focus();
            }
            else
            {
                //但是最后一个焦点的时候触发此事件
                if (this.OnFoucsOver != null)
                {
                    this.OnFoucsOver(null, null);
                }
                else
                {
                    SendKeys.Send("{Tab}");
                }
            }
        }

        /// <summary>
        /// 根据当前的TabIndex查找下一个应该得到焦点的控件
        /// </summary>
        /// <param name="CurrentTabIndex"></param>
        /// <returns></returns>
        private Control NextFocusControl(Control currentContol)
        {
            Control tempControl = null;
            foreach (DictionaryEntry de in InputHasTable)
            {
                Control c = de.Value as Control;
                if (currentContol.TabIndex < c.TabIndex)
                {
                    if (tempControl == null)
                    {
                        tempControl = c;
                        continue;
                    }
                    if (tempControl != null && tempControl.TabIndex > c.TabIndex)
                    {
                        tempControl = c;
                    }
                }
            }
            return tempControl;
        }

        /// <summary>
        /// 获取当前有焦点控件
        /// </summary>
        /// <returns></returns>
        private Control GetCurrentFoucsControl()
        {
            foreach (Control c in panelControl.Controls)
            {
                if (c.Focused)
                {
                    return c;
                }
            }
            return null;
        }

        /// <summary>
        /// 设置控件enable属性
        /// </summary>
        /// <param name="isEnabled"></param>
        public void SetControlEnable(bool isEnabled)
        {
            foreach (Control c in this.panelControl.Controls)
            {
                c.Enabled = isEnabled;
            }
        }

        /// <summary>
        /// 提示栏
        /// </summary>
        /// <param name="title"></param>
        public void SetTitle(string title)
        {
            this.lblshow.Text = title;
        }


        public int ReadIDCard()
        {
            this.panelControl.Controls.Add(this.pictureBox);


            FS.HISFC.BizProcess.Interface.Account.IReadIDCard readIDCard = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Account.IReadIDCard)) as FS.HISFC.BizProcess.Interface.Account.IReadIDCard;
            if (readIDCard == null)
            {
                MessageBox.Show("读身份证接口没有实现");
                return -1;
            }
            string code = "", name = "", sex = "", nation = "", agent = "", add = "", message = "";
            DateTime birth = DateTime.MinValue, bigen = DateTime.MinValue, end = DateTime.MinValue;
            string photoFileName = "";
            int rtn = readIDCard.GetIDInfo(ref code, ref name, ref sex, ref birth, ref nation, ref add, ref agent, ref bigen, ref end, ref photoFileName, ref message);
            if (rtn == -1)
            {
                MessageBox.Show("读取身份证出错！" + message);
                return -1;
            }

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(code))
            {
                MessageBox.Show("读取身份证失败，请确认放好位置！" + message);
                return -1;
            }
            this.txtName.Text = name;
            this.txtIDNO.Text = code;
            string error = string.Empty;
            if (code != string.Empty)
            {
                //if (FS.FrameWork.WinForms.Classes.Function.CheckIDInfo(code, ref error) < 0)
                //{
                //    string strMsg = Language.Msg(error + "\r\n是否继续？");
                //    if (MessageBox.Show(strMsg, "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                //    {
                //        return;
                //    }
                //}
                //根据身份证号获取患者性别
                FS.FrameWork.Models.NeuObject obj = Class.Function.GetSexFromIdNO(code, ref error);
                if (obj == null)
                {
                    MessageBox.Show(error);
                    return -1;
                }
                this.cmbSex.Tag = obj.ID;
                this.cmbCardType.Text = "身份证";
                this.cmbCardType.Tag = "01";
                //根据患者身份证号获取生日
                string birthdate = Class.Function.GetBirthDayFromIdNO(code, ref error);
                if (birthdate == "-1")
                {
                    MessageBox.Show(error);
                    return -1;
                }
                this.dtpBirthDay.Value = FrameWork.Function.NConvert.ToDateTime(birthdate);
            }
            //this.dtpBirthDay.Value = birth;
            //this.cmbSex.SelectedItem.ID = sex;
            this.cmbHomeAddress.Text = add;
            this.txtHomePhone.Focus();
            try
            {
                System.IO.MemoryStream m = new System.IO.MemoryStream();
                Image img = Image.FromFile(photoFileName);
                img.Save(m, System.Drawing.Imaging.ImageFormat.Bmp);
                img.Dispose();
                this.pictureBox.Image = Image.FromStream(m);
                this.pictureBox.Tag = photoFileName;
                m.Close();
            }
            catch
            { }

            return 1;
        }

        public int AutoReadIDCard()
        {
            FS.HISFC.BizProcess.Interface.Account.IReadIDCard readIDCard = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Account.IReadIDCard)) as FS.HISFC.BizProcess.Interface.Account.IReadIDCard;
            if (readIDCard == null)
            {

                //MessageBox.Show("读身份证接口没有实现");
                return -2;
            }
            string code = "", name = "", sex = "", nation = "", agent = "", add = "", message = "";
            DateTime birth = DateTime.MinValue, bigen = DateTime.MinValue, end = DateTime.MinValue;
            string photoFileName = "";
            int rtn = readIDCard.GetIDInfo(ref code, ref name, ref sex, ref birth, ref nation, ref add, ref agent, ref bigen, ref end, ref photoFileName, ref message);
            if (rtn == -1)
            {
                if (message.IndexOf("端口打开失败") >= 0)
                {
                    return -2;
                }
                //MessageBox.Show("读取身份证出错！" + message);
                return -1;
            }

            this.Clear(true);
            this.panelControl.Controls.Add(this.pictureBox);
            this.txtName.Text = name;
            this.txtIDNO.Text = code;
            string error = string.Empty;
            if (code != string.Empty)
            {
                //if (FS.FrameWork.WinForms.Classes.Function.CheckIDInfo(code, ref error) < 0)
                //{
                //    string strMsg = Language.Msg(error + "\r\n是否继续？");
                //    if (MessageBox.Show(strMsg, "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                //    {
                //        return;
                //    }
                //}
                //根据身份证号获取患者性别
                FS.FrameWork.Models.NeuObject obj = Class.Function.GetSexFromIdNO(code, ref error);
                if (obj == null)
                {
                    MessageBox.Show(error);
                    return -1;
                }
                this.cmbSex.Tag = obj.ID;
                this.cmbCardType.Text = "身份证";
                this.cmbCardType.Tag = "01";
                //根据患者身份证号获取生日
                string birthdate = Class.Function.GetBirthDayFromIdNO(code, ref error);
                if (birthdate == "-1")
                {
                    MessageBox.Show(error);
                    return -1;
                }
                this.dtpBirthDay.Value = FrameWork.Function.NConvert.ToDateTime(birthdate);
            }
            //this.dtpBirthDay.Value = birth;
            //this.cmbSex.SelectedItem.ID = sex;
            this.cmbHomeAddress.Text = add;
            this.txtHomePhone.Focus();
            System.IO.MemoryStream m = new System.IO.MemoryStream();
            Image img = Image.FromFile(photoFileName);
            img.Save(m, System.Drawing.Imaging.ImageFormat.Bmp);
            img.Dispose();
            this.pictureBox.Image = Image.FromStream(m);
            this.pictureBox.Tag = photoFileName;
            m.Close();

            return 1;
        }
        #endregion

        #region 输入法

        /// <summary>
        /// 默认的中文输入法
        /// </summary>
        private InputLanguage CHInput = null;

        /// <summary>
        /// 初始化输入法菜单
        /// </summary>
        private void SetInputMenu()
        {

            for (int i = 0; i < InputLanguage.InstalledInputLanguages.Count; i++)
            {
                InputLanguage t = InputLanguage.InstalledInputLanguages[i];
                System.Windows.Forms.ToolStripMenuItem m = new ToolStripMenuItem();
                m.Text = t.LayoutName;
                m.Click += new EventHandler(m_Click);
                neuContextMenuStrip1.Items.Add(m);
            }

            this.ReadInputLanguage();
        }

        /// <summary>
        /// 读取当前默认输入法
        /// </summary>
        private void ReadInputLanguage()
        {
            if (!System.IO.File.Exists(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml"))
            {
                // FS.UFC.Common.Classes.Function.CreateFeeSetting();

            }
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml");
                XmlNode node = doc.SelectSingleNode("//IME");

                CHInput = GetInputLanguage(node.Attributes["currentmodel"].Value);

                if (CHInput != null)
                {
                    foreach (ToolStripMenuItem m in neuContextMenuStrip1.Items)
                    {
                        if (m.Text == CHInput.LayoutName)
                        {
                            m.Checked = true;
                        }
                    }
                }

                //添加到工具栏

            }
            catch (Exception e)
            {
                MessageBox.Show("获取默认中文输入法出错!" + e.Message);
                return;
            }
        }

        /// <summary>
        /// 根据输入法名称获取输入法
        /// </summary>
        /// <param name="LanName"></param>
        /// <returns></returns>
        private InputLanguage GetInputLanguage(string LanName)
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
                // FS.UFC.Common.Classes.Function.CreateFeeSetting();
            }
            if (CHInput == null)
                return;

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml");
                XmlNode node = doc.SelectSingleNode("//IME");

                node.Attributes["currentmodel"].Value = CHInput.LayoutName;

                doc.Save(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml");
            }
            catch (Exception e)
            {
                MessageBox.Show("保存默认中文输入法出错!" + e.Message);
                return;
            }
        }

        private void m_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem m in this.neuContextMenuStrip1.Items)
            {
                if (sender == m)
                {
                    m.Checked = true;
                    this.CHInput = this.GetInputLanguage(m.Text);
                    //保存输入法
                    this.SaveInputLanguage();
                }
                else
                {
                    m.Checked = false;
                }
            }
        }

        #endregion

        #region 事件
        private void ucPatientInfo_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
                return;

            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower() != "devenv")
            {
                #region 权限判断
                FS.HISFC.BizLogic.Manager.UserPowerDetailManager user = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
                NeuObject dept = (accountManager.Operator as HISFC.Models.Base.Employee).Dept;
                //判断是否有加密权限
                this.IsEnableEntry = user.JudgeUserPriv(accountManager.Operator.ID, dept.ID, "5001", "01");

                //Vip权限设置
                this.IsEnableVip = user.JudgeUserPriv(accountManager.Operator.ID, dept.ID, "5002", "01");


                #endregion

                this.Init();
                this.ActiveControl = this.txtName;
            }

            // {BDEC89C8-C3BB-433b-8A98-AA65B2FFCEE5}
            this.isJudgePact = controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.AccountConstant.BuildCardIsJudgePact, false);
            //外屏显示{67C90AAC-CFAD-4089-96F4-9F9FC82D8754}
            if (Screen.AllScreens.Length > 1)
            {
                iMultiScreen = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject<
                        FS.HISFC.BizProcess.Interface.FeeInterface.IMultiScreen>(this.GetType());
                if (iMultiScreen == null)
                {
                    iMultiScreen = new Forms.frmMiltScreen();

                }
                //iMultiScreen.ListInfo = null;
                //显示初始化界面
                FS.HISFC.Models.Base.Employee currentOperator = accountManager.Operator as FS.HISFC.Models.Base.Employee;
                System.Collections.Generic.List<Object> lo = new System.Collections.Generic.List<object>();
                lo.Add("");//患者基本信息
                lo.Add("");//新办卡号
                lo.Add(currentOperator.ID);//收费员工号
                lo.Add(currentOperator.Name);//收费员名字
                this.iMultiScreen.ListInfo = lo;
                //
                iMultiScreen.ShowScreen();
            }
        }

        private void dtpBirthDay_ValueChanged(object sender, EventArgs e)
        {
            if (this.dtpBirthDay.Value == null || this.dtpBirthDay.Value < new DateTime(1700, 1, 1))
            {
                return;
            }
            // string age = this.accountManager.GetAge(this.dtpBirthDay.Value, true);
            int iyear = 0;
            int iMonth = 0;
            int iDay = 0;

            DateTime sysdate = this.accountManager.GetDateTimeFromSysDateTime();
            if (sysdate > this.dtpBirthDay.Value)
            {
                iyear = sysdate.Year - this.dtpBirthDay.Value.Year;
                if (iyear < 0)
                {
                    iyear = 0;
                }
                iMonth = sysdate.Month - this.dtpBirthDay.Value.Month;
                if (iMonth < 0)
                {
                    if (iyear > 0)
                    {
                        iyear = iyear - 1;
                        DateTime dt = new DateTime(this.dtpBirthDay.Value.Year + 1, 1, 1);
                        iMonth = dt.AddMonths(-1).Month + iMonth;//用当前的月份减
                    }

                    if (iMonth < 0)
                    {
                        iMonth = 0;
                    }
                }
                iDay = sysdate.Day - this.dtpBirthDay.Value.Day;
                if (iDay < 0)
                {
                    if (iMonth > 0)
                    {
                        iMonth = iMonth - 1;
                        DateTime dt = new DateTime(this.dtpBirthDay.Value.Year, this.dtpBirthDay.Value.Month, 1).AddMonths(1);
                        iDay = dt.AddDays(-1).Day + iDay;
                    }
                    else if (iyear > 0)
                    {
                        iyear = iyear - 1;
                        DateTime dt = new DateTime(this.dtpBirthDay.Value.Year + 1, 1, 1);
                        iMonth = dt.AddMonths(-1).Month - 1;
                        dt = new DateTime(this.dtpBirthDay.Value.Year, this.dtpBirthDay.Value.Month, 1).AddMonths(1);
                        iDay = dt.AddDays(-1).Day + iDay;
                    }
                    else
                    {
                        iDay = 0;
                    }
                }
            }


            // this.GetAgeNumber(age, ref iyear, ref iMonth, ref iDay);
            this.txtAge.TextChanged -= new EventHandler(txtAge_TextChanged);
            this.txtAge.Text = this.GetAge(iyear, iMonth, iDay);
            this.txtAge.TextChanged += new EventHandler(txtAge_TextChanged);

            this.txtYear.TextChanged -= new EventHandler(txtBirthDay_TextChanged);
            this.txtMonth.TextChanged -= new EventHandler(txtBirthDay_TextChanged);
            this.txtDays.TextChanged -= new EventHandler(txtBirthDay_TextChanged);
            this.txtYear.Text = this.dtpBirthDay.Value.Year.ToString();
            this.txtMonth.Text = this.dtpBirthDay.Value.Month.ToString();
            this.txtDays.Text = this.dtpBirthDay.Value.Day.ToString();
            this.txtYear.TextChanged += new EventHandler(txtBirthDay_TextChanged);
            this.txtMonth.TextChanged += new EventHandler(txtBirthDay_TextChanged);
            this.txtDays.TextChanged += new EventHandler(txtBirthDay_TextChanged);

            //this.cmbAgeUnit.SelectedIndexChanged -=new EventHandler(cmbAgeUnit_SelectedIndexChanged);
            //this.cmbAgeUnit.Text = age.Substring(age.Length - 1, 1);
            //this.cmbAgeUnit.SelectedIndexChanged += new EventHandler(cmbAgeUnit_SelectedIndexChanged);
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (Screen.AllScreens.Length > 1)
                {
                    //实时外屏显示{67C90AAC-CFAD-4089-96F4-9F9FC82D8754}  
                    this.showPatientInfo = this.GetPatientInfomation();
                    System.Collections.Generic.List<Object> lo = new System.Collections.Generic.List<object>();
                    lo.Add(this.showPatientInfo);//患者信息
                    lo.Add("");//卡号
                    lo.Add("");//收费员id
                    lo.Add("");//收费员姓名
                    this.iMultiScreen.ListInfo = lo;
                    //
                }


                //if (isMustInputTabInde)
                //{
                //    //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
                //    if (txtMatherName.Focused)
                //    {
                //        if (OnFoucsOver != null)
                //        {
                //            OnFoucsOver(null, null);
                //            return true;
                //        }
                //    }
                //    if (this.txtIDNO.Focused)
                //    {
                //        if (this.cmbCardType.Tag.ToString() == "01")
                //        {
                //            string error = string.Empty;
                //            string idNO = this.txtIDNO.Text.Trim();
                //            if (idNO != string.Empty)
                //            {
                //                if (FS.FrameWork.WinForms.Classes.Function.CheckIDInfo(idNO, ref error) < 0)
                //                {
                //                    MessageBox.Show(error);
                //                    return true;
                //                }
                //                //根据身份证号获取患者性别
                //                FS.FrameWork.Models.NeuObject obj = Class.Function.GetSexFromIdNO(idNO, ref error);
                //                if (obj == null)
                //                {
                //                    MessageBox.Show(error);
                //                    return true;
                //                }
                //                this.cmbSex.Tag = obj.ID;
                //                //根据患者身份证号获取生日
                //                string birthdate = Class.Function.GetBirthDayFromIdNO(idNO, ref error);
                //                if (birthdate == "-1")
                //                {
                //                    MessageBox.Show(error);
                //                    return true;
                //                }
                //                this.dtpBirthDay.Value = FrameWork.Function.NConvert.ToDateTime(birthdate);
                //            }
                //        }
                //    }

                //    try
                //    {
                //        if (this.txtYear.Focused)
                //        {
                //            this.dtpBirthDay.Value = new DateTime(int.Parse(txtYear.Text), int.Parse(txtMonth.Text), int.Parse(txtDays.Text));
                //            this.txtMonth.Focus();
                //            return true;
                //        }
                //        else if (this.txtMonth.Focused)
                //        {
                //            this.dtpBirthDay.Value = new DateTime(int.Parse(txtYear.Text), int.Parse(txtMonth.Text), int.Parse(txtDays.Text));
                //            this.txtDays.Focus();
                //            return true;
                //        }
                //        else if (this.txtDays.Focused)
                //        {
                //            this.dtpBirthDay.Value = new DateTime(int.Parse(txtYear.Text), int.Parse(txtMonth.Text), int.Parse(txtDays.Text));
                //            this.txtAge.Focus();
                //            return true;
                //        }
                //    }
                //    catch
                //    {
                //        MessageBox.Show("您当前输入的日期格式错误，请重新输入！");

                //        if (this.txtMonth.Focused)
                //        {
                //            this.txtDays.Text = "1";
                //            this.txtMonth.Text = "1";

                //            this.txtMonth.SelectAll();
                //        }

                //        if (this.txtDays.Focused)
                //        {
                //            this.txtDays.Text = "1";
                //            this.txtMonth.Text = "1";

                //            this.txtDays.SelectAll();
                //        }


                //        return true;
                //    }
                //    //输完年龄跳转到地址、联系电话
                //    if (this.txtAge.Focused)
                //    {
                //        this.cmbHomeAddress.Focus();
                //        return true;
                //    }
                //    if (this.cmbHomeAddress.Focused)
                //    {
                //        if (isJumpHomePhone)
                //        {
                //            this.txtHomePhone.Focus();
                //            return true;
                //        }
                //        //else
                //        //{
                //        //    this.txtLinkPhone.Focus();
                //        //}

                //        //return true;
                //    }
                //    if (this.txtHomePhone.Focused)
                //    {
                //        if (isJumpHomePhone)
                //        {
                //            if (OnFoucsOver != null)
                //            {
                //                OnFoucsOver(null, null);
                //                return true;
                //            }
                //        }
                //    }

                //    if (this.cmbSex.Focused)
                //    {
                //        if (this.cmbSex.Text.Trim().Length > 0 && this.cmbSex.Text.Trim().Length < 2)
                //        {
                //            try
                //            {
                //                int intsex = int.Parse(this.cmbSex.Text);
                //                switch (intsex)
                //                {
                //                    case 1:
                //                        this.cmbSex.Tag = "M";
                //                        break;
                //                    case 2:
                //                        this.cmbSex.Tag = "F";
                //                        break;
                //                    default:
                //                        break;
                //                }
                //            }
                //            catch
                //            {

                //            }

                //        }
                //    }
                //}

                //

                Control currentContol = this.GetCurrentFoucsControl();

                if (isMustInputTabInde)
                {
                    SetMustInputFocus(currentContol);
                }
                else
                {
                    SendKeys.Send("{Tab}");
                }

                Application.DoEvents();

                #region 查询患者信息
                if (isSelectPatientByNameIDCardByEnter)
                {
                    if (currentContol.Name == "txtName" || currentContol.Name == "txtIDNO")
                    {
                        if (OnEnterSelectPatient != null)
                        {
                            this.OnEnterSelectPatient(null, null);
                        }
                    }
                }
                //else if (inpubMaxTabIndex == currentContol.TabIndex)
                //{
                //    if (OnEnterSelectPatient != null)
                //    {
                //        this.OnEnterSelectPatient(null, null);
                //    }
                //}
                #endregion





                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// 根据年龄算生日
        /// </summary>
        /// <param name="age"></param>
        /// <param name="ageUnit"></param>
        /// <returns></returns>
        private void ConvertBirthdayByAge(bool isUpdateAgeText)
        {
            DateTime birthDay = this.accountManager.GetDateTimeFromSysDateTime();
            if (birthDay == null || birthDay < new DateTime(1700, 1, 1))
            {
                return;
            }
            string ageStr = this.txtAge.Text.Trim();
            int iyear = 0;
            int iMonth = 0;
            int iDay = 0;
            this.GetAgeNumber(ageStr, ref iyear, ref iMonth, ref iDay);

            //birthDay = birthDay.AddDays(-iDay).AddMonths(-iMonth).AddYears(-iyear);
            int year = birthDay.Year - iyear;
            int m = birthDay.Month - iMonth;
            if (m <= 0)
            {
                if (year > 0)
                {
                    year = year - 1;
                    DateTime dt = new DateTime(year, 1, 1);
                    m = dt.AddYears(1).AddDays(-1).Month + m;
                }
            }

            int day = birthDay.Day - iDay;
            if (day <= 0)
            {
                if (m > 0)
                {
                    m = m - 1;
                    DateTime dt = new DateTime(year, m + 1, 1).AddMonths(-1);
                    day = dt.AddMonths(1).AddDays(-1).Day + day;
                }
                else if (year > 0)
                {
                    year = year - 1;
                    DateTime dt = new DateTime(year, 1, 1);
                    m = dt.AddYears(1).AddDays(-1).Month - 1;
                    dt = new DateTime(year, m + 1, 1).AddMonths(-1);
                    day = dt.AddMonths(1).AddDays(-1).Day + day;
                }

                if (m <= 0)
                {
                    if (year > 0)
                    {
                        year = year - 1;
                        DateTime dt = new DateTime(year, 1, 1);
                        m = dt.AddYears(1).AddDays(-1).Month + m;
                    }
                }
            }
            else
            {
                DateTime dt = new DateTime(year, m, 1);
                if (day > dt.AddMonths(1).AddDays(-1).Day)
                {
                    day = dt.AddMonths(1).AddDays(-1).Day;
                }
            }

            birthDay = new DateTime(year, m, day);

            if (birthDay < dtpBirthDay.MinDate || birthDay > dtpBirthDay.MaxDate)
            {
                MessageBox.Show("年龄输入过大请重新输入！");
                this.txtAge.Text = this.GetAge(0, 0, 0);
                return;
            }
            if (isUpdateAgeText)
            {
                this.txtAge.TextChanged -= new EventHandler(txtAge_TextChanged);
                this.txtAge.Text = this.GetAge(iyear, iMonth, iDay);
                this.dtpBirthDay.Value = birthDay;
                this.txtAge.TextChanged += new EventHandler(txtAge_TextChanged);
            }
            else
            {
                this.dtpBirthDay.ValueChanged -= new EventHandler(dtpBirthDay_ValueChanged);
                this.dtpBirthDay.Value = birthDay;
                this.dtpBirthDay.ValueChanged += new EventHandler(dtpBirthDay_ValueChanged);

                this.txtYear.TextChanged -= new EventHandler(txtBirthDay_TextChanged);
                this.txtMonth.TextChanged -= new EventHandler(txtBirthDay_TextChanged);
                this.txtDays.TextChanged -= new EventHandler(txtBirthDay_TextChanged);
                this.txtYear.Text = this.dtpBirthDay.Value.Year.ToString();
                this.txtMonth.Text = this.dtpBirthDay.Value.Month.ToString();
                this.txtDays.Text = this.dtpBirthDay.Value.Day.ToString();
                this.txtYear.TextChanged += new EventHandler(txtBirthDay_TextChanged);
                this.txtMonth.TextChanged += new EventHandler(txtBirthDay_TextChanged);
                this.txtDays.TextChanged += new EventHandler(txtBirthDay_TextChanged);
            }
        }

        public string GetAge(int year, int month, int day)
        {
            return string.Format("{0}岁{1}月{2}天", year <= 0 ? "___" : year.ToString().PadLeft(3, '_'), year <= 0 && month <= 0 ? "__" : month.ToString().PadLeft(2, '_'), day.ToString().PadLeft(2, '_'));
        }

        public void GetAgeNumber(string age, ref int year, ref int month, ref int day)
        {
            year = 0;
            month = 0;
            day = 0;
            age = age.Replace("_", "");
            int ageIndex = age.IndexOf("岁");
            int monthIndex = age.IndexOf("月");
            int dayIndex = age.IndexOf("天");

            if (ageIndex > 0)
            {
                year = FS.FrameWork.Function.NConvert.ToInt32(age.Substring(0, ageIndex));
            }
            if (ageIndex >= 0 && monthIndex > 0 && monthIndex > ageIndex)
            {
                month = FS.FrameWork.Function.NConvert.ToInt32(age.Substring(ageIndex + 1, monthIndex - ageIndex - 1));
            }
            if (ageIndex < 0 && monthIndex > 0 && monthIndex > ageIndex)
            {
                month = FS.FrameWork.Function.NConvert.ToInt32(age.Substring(0, monthIndex));//只有月
            }
            if (monthIndex >= 0 && dayIndex > 0 && dayIndex > monthIndex)
            {
                day = FS.FrameWork.Function.NConvert.ToInt32(age.Substring(monthIndex + 1, dayIndex - monthIndex - 1));
            }
            if (ageIndex < 0 && monthIndex < 0 && dayIndex > 0 && dayIndex > monthIndex)
            {
                day = FS.FrameWork.Function.NConvert.ToInt32(age.Substring(0, dayIndex));//只有日
            }
            if (ageIndex >= 0 && monthIndex < 0 && dayIndex > 0 && dayIndex > monthIndex)
            {
                day = FS.FrameWork.Function.NConvert.ToInt32(age.Substring(ageIndex + 1, dayIndex - ageIndex - 1));//只有年，日
            }
        }

        private void txtAge_TextChanged(object sender, EventArgs e)
        {
            this.ConvertBirthdayByAge(false);
        }


        private void txtAge_Leave(object sender, EventArgs e)
        {
            this.ConvertBirthdayByAge(true);
        }



        #endregion
        /// <summary>
        /// 获取界面证件信息
        /// </summary>
        /// <param name="idCardType"></param>
        /// <param name="idCardNo"></param>
        public void GetIdCardInfo(out string idCardType, out string idCardNo, out string strCardNo)
        {
            idCardType = this.cmbCardType.Tag.ToString(); //证件类型
            idCardNo = this.txtIDNO.Text.Trim();          //证件号
            strCardNo = this.cardNO;
        }

        /// <summary>
        /// 获取界面患者信息// {06A6BD92-97A6-4f01-8B6E-533B35CBF2DD}
        /// </summary>
        /// <param name="name"></param>
        /// <param name="sexCode"></param>
        /// <param name="phone"></param>
        public void GetPatientInfo(out string name, out string sexCode, out string phone)
        {
            name = this.txtName.Text.Trim();       //名字
            sexCode = this.cmbSex.Tag.ToString();  //性别编号
            phone = this.txtHomePhone.Text.Trim();  //电话
        }

        private void txtBirthDay_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (sender == this.txtYear)
                {
                    if (this.txtYear.Text.Length == "2011".Length)
                    {
                        this.txtMonth.Focus();
                        this.dtpBirthDay.Value = new DateTime(int.Parse(txtYear.Text), int.Parse(txtMonth.Text), int.Parse(txtDays.Text));
                    }
                }
                else if (sender == this.txtMonth)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(txtMonth.Text))
                        {
                            if (this.txtMonth.Text.Length == "12".Length)
                            {
                                this.txtDays.Focus();
                                this.dtpBirthDay.Value = new DateTime(int.Parse(txtYear.Text), int.Parse(txtMonth.Text), int.Parse(txtDays.Text));
                            }
                            else if (txtMonth.Text.Length > 1
                                && FS.FrameWork.Function.NConvert.ToInt32(txtMonth.Text.Substring(0, 1)) > 1)
                            {
                                txtDays.Focus();
                                this.dtpBirthDay.Value = new DateTime(int.Parse(txtYear.Text), int.Parse(txtMonth.Text), int.Parse(txtDays.Text));
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("日期输入错误！请重新输入！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (sender == this.txtDays)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(txtMonth.Text))
                        {
                            if (this.txtDays.Text.Length == "12".Length)
                            {
                                this.dtpBirthDay.Value = new DateTime(int.Parse(txtYear.Text), int.Parse(txtMonth.Text), int.Parse(txtDays.Text));
                            }
                            else if (txtDays.Text.Length > 1
                                && FS.FrameWork.Function.NConvert.ToInt32(txtDays.Text.Substring(0, 1)) > 3)
                            {
                                txtDays.Focus();
                                this.dtpBirthDay.Value = new DateTime(int.Parse(txtYear.Text), int.Parse(txtMonth.Text), int.Parse(txtDays.Text));
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("日期输入错误！请重新输入！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("日期输入错误！" + ex.Message, "警告", MessageBoxButtons.OK);
                if (sender == this.txtYear)
                {
                    this.txtYear.Focus();
                }
                else if (sender == this.txtMonth)
                {
                    this.txtMonth.Focus();
                }
                else
                {
                    this.txtDays.Focus();
                }
            }
        }

        private void txtYear_Leave(object sender, EventArgs e)
        {
            try
            {
                this.dtpBirthDay.Value = new DateTime(int.Parse(txtYear.Text), int.Parse(txtMonth.Text), int.Parse(txtDays.Text));
                this.txtMonth.Focus();
            }
            catch
            {
                MessageBox.Show("您当前输入的日期格式错误，请重新输入！");

                if (this.txtMonth.Focused)
                {
                    this.txtDays.Text = "1";
                    this.txtMonth.Text = "1";
                    this.txtMonth.SelectAll();
                }

                if (this.txtDays.Focused)
                {
                    this.txtDays.Text = "1";
                    this.txtMonth.Text = "1";
                    this.txtDays.SelectAll();
                }
            }
        }

        private void txtMonth_Leave(object sender, EventArgs e)
        {
            try
            {
                this.dtpBirthDay.Value = new DateTime(int.Parse(txtYear.Text), int.Parse(txtMonth.Text), int.Parse(txtDays.Text));
                this.txtDays.Focus();
            }
            catch
            {
                MessageBox.Show("您当前输入的日期格式错误，请重新输入！");

                if (this.txtMonth.Focused)
                {
                    this.txtDays.Text = "1";
                    this.txtMonth.Text = "1";
                    this.txtMonth.SelectAll();
                }

                if (this.txtDays.Focused)
                {
                    this.txtDays.Text = "1";
                    this.txtMonth.Text = "1";
                    this.txtDays.SelectAll();
                }
            }

        }

        private void txtDays_Leave(object sender, EventArgs e)
        {
            try
            {
                this.dtpBirthDay.Value = new DateTime(int.Parse(txtYear.Text), int.Parse(txtMonth.Text), int.Parse(txtDays.Text));
                this.txtAge.Focus();
            }
            catch
            {
                MessageBox.Show("您当前输入的日期格式错误，请重新输入！");

                if (this.txtMonth.Focused)
                {
                    this.txtDays.Text = "1";
                    this.txtMonth.Text = "1";
                    this.txtMonth.SelectAll();
                }

                if (this.txtDays.Focused)
                {
                    this.txtDays.Text = "1";
                    this.txtMonth.Text = "1";
                    this.txtDays.SelectAll();
                }
            }
        }

        private void txtIDNO_Leave(object sender, EventArgs e)
        {
            try
            {

                if (isJudgePactByIdno)
                {
                    this.JudgePactByIdno();
                }

                if (this.cmbCardType.Tag.ToString() == "01")
                {
                    string error = string.Empty;
                    string idNO = this.txtIDNO.Text.Trim();
                    if (idNO != string.Empty)
                    {

                        if (this.cmbCardType.Tag != null && this.cmbCardType.Tag.ToString() == "01" && this.txtIDNO.Text.Trim() != string.Empty)
                        {
                            string err = string.Empty;
                            if (FS.FrameWork.WinForms.Classes.Function.CheckIDInfo(this.txtIDNO.Text.Trim(), ref err) < 0)
                            {
                                //if (isTooltipCARDID == 1)// {71A043C7-ADEE-401e-81E2-FF32FBD39D78}
                                //{
                                //    string strMsg = Language.Msg(err + "\r\n是否继续？");
                                //    if (MessageBox.Show(strMsg, "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                //    {
                                //        this.txtIDNO.Focus();
                                //        return;
                                //    }
                                //}
                                //else if (isTooltipCARDID == 2)
                                //{

                                //    string strMsg = Language.Msg(err + "");

                                //    MessageBox.Show(strMsg, "系统提示");
                                //    this.txtIDNO.Focus();
                                //    return;
                                //}
                                //else
                                //{
                                //}

                                return;
                            }
                        }
                        //根据身份证号获取患者性别
                        FS.FrameWork.Models.NeuObject obj = Class.Function.GetSexFromIdNO(idNO, ref error);
                        if (obj == null)
                        {
                            return;
                        }
                        if (!string.IsNullOrEmpty(obj.ID))
                        {
                            this.cmbSex.Tag = obj.ID;
                        }

                        //根据患者身份证号获取生日
                        string birthdate = Class.Function.GetBirthDayFromIdNO(idNO, ref error);
                        if (birthdate == "-1")
                        {
                            return;
                        }
                        if (!string.IsNullOrEmpty(birthdate))
                        {
                            this.dtpBirthDay.Value = FrameWork.Function.NConvert.ToDateTime(birthdate);


                        }
                        else
                        {
                            this.dtpBirthDay.Value = DateTime.Now;

                        }

                    }
                }


            }

            catch
            {
                MessageBox.Show("身份证格式不正确！");
                //this.txtIDNO.Focus();
                return;
            }
        }

        public void JudgePactByIdno()
        {
            #region 根据身份证号判断是否享受居民医保
            if (changePactId == "")
            {
                MessageBox.Show("请维护转换成医保时，医保合同单位");
                return;
            }
            FS.HISFC.Models.Registration.Register r = new FS.HISFC.Models.Registration.Register();
            r.IDCard = this.txtIDNO.Text.Trim();
            r.Pact.ID = this.changePactId.ToString();
            if (!string.IsNullOrEmpty(r.IDCard) && !string.IsNullOrEmpty(r.Pact.ID))
            {
                int iRes = this.medcareProxy.SetPactCode(r.Pact.ID);
                if (iRes != 1)
                {
                    MessageBox.Show(Language.Msg(this.medcareProxy.ErrMsg), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ComPact.Focus();
                    return;
                }

                iRes = this.medcareProxy.QueryCanMedicare(r);
                //if (this.medcareProxy.ErrMsg!="")
                //{
                //    MessageBox.Show(Language.Msg(this.medcareProxy.ErrMsg), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                //不享受医保则不变
                if (iRes == -2 || iRes == -1)
                {
                    return;
                }

                ArrayList al = new ArrayList();
                //享受医保后判断是否是否走一卡多身份流程
                if (this.isMutilPactInfo)
                {
                    al = ((ComboBoxPactSelect)this.ComPact).GetValues();
                    if (al != null)
                    {
                        //如果已经在多身份选择了则不加载,如果没有则加载
                        bool isSelect = false;
                        foreach (FS.HISFC.Models.Base.PactInfo pactinfo in al)
                        {
                            if (pactinfo.ID == this.changePactId)
                            {
                                isSelect = true;
                                break;
                            }
                        }
                        if (!isSelect)
                        {
                            this.comboBoxPactSelect1.Tag = this.changePactId;
                        }
                    }
                }
                //不是一卡多身份流程，则把合同单位的cmb改成居民医保
                else
                {
                    this.cmbPact.Tag = this.changePactId;
                }
            }

            #endregion
        }

        private void clearPhotoStrip_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确定清空该相片？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                FrameWork.Management.PublicTrans.BeginTransaction();
                this.accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                if (this.accountManager.DeletePhoto(this.patientInfo) == -1)
                {
                    MessageBox.Show("删除相片失败" + this.accountManager.Err);
                    FrameWork.Management.PublicTrans.RollBack();
                }
                FrameWork.Management.PublicTrans.Commit();
                this.pictureBox.Image = null;
                this.pictureBox.Tag = null;
            }
        }

        /// <summary>
        /// 显示患者基本信息
        /// </summary>
        /// 
        public void SetPatient(FS.HISFC.Models.RADT.PatientInfo patInfo)
        {
            //modify by sung 2009-2-24 {DCAA485E-753C-41ed-ABCF-ECE46CD41B33}
            if (patInfo.IsEncrypt)
            {
                patInfo.Name = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(patInfo.NormalName);
            }
            this.txtName.Text = patInfo.Name;//患者姓名

            this.cmbSex.Text = patInfo.Sex.Name;            //性别
            this.cmbSex.Tag = patInfo.Sex.ID;               //性别
            //this.txtAge.Text = this.accountManager.GetAge(this.patientInfo.Birthday);//年龄
            if (patInfo.Birthday > DateTime.MinValue)
            {
                this.dtpBirthDay.Value = patInfo.Birthday;      //出生日期
            }
            else
            {
                this.dtpBirthDay.Value = this.accountManager.GetDateTimeFromSysDateTime();      //出生日期
            }
            //this.cmbDistrict.Text = this.patientInfo.DIST;            //籍贯
            //this.cmbProfession.Tag = this.patientInfo.Profession.ID; //职业
            this.txtIDNO.Text = patInfo.IDCard;             //身份证号
            this.cmbWorkAddress.Text = patInfo.CompanyName; //工作单位
            this.txtWorkPhone.Text = patInfo.PhoneBusiness; //单位电话
            //this.cmbMarry.Tag = patInfo.MaritalStatus.ID.ToString();//婚姻状况
            this.txtNowHome.Text = patInfo.User01;  //现住址
            this.cmbHomeAddress.Text = patInfo.AddressHome;  //家庭住址
            this.txtHomePhone.Text = patInfo.PhoneHome;     //家庭电话
            this.txtLinkMan.Text = patInfo.Kin.Name;        //联系人 
            this.cmbLinkAddress.Text = patInfo.Kin.RelationAddress;//联系人地址
            this.txtLinkPhone.Text = patInfo.Kin.RelationPhone;//联系人电话
            //{05024624-3FF4-44B5-92BF-BD4C6FAF6897}添加儿童标签
            cbxChild.Checked = patInfo.ChildFlag == "1";

            //this.cmbCardType.Tag = patInfo.IDCardType.ID; //证件类型
        }

        /// <summary>
        ///   //{6D7EC8BC-BDBB-4a47-BCFF-36BB0113499A}//如果结算类别是商保则必须添加商保公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbPact_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPact.Tag.ToString() == "2")
            {
                lblcompany.Visible = cmbCompany.Visible = true;
                cmbPact.Width = 120;
            }
            else
            {
                lblcompany.Visible = cmbCompany.Visible = false;
                cmbPact.Width = 350;
            }

            PatientPactInfo = this.cmbPact.SelectedItem as FS.HISFC.Models.Base.PactInfo; //{fa76c153-d727-439d-9e93-aa86bfad208b}
        }


    }
}