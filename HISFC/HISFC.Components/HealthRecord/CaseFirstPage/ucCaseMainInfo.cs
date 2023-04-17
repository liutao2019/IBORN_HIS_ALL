using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Function;

namespace FS.HISFC.Components.HealthRecord.CaseFirstPage
{
    public partial class ucCaseMainInfo : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        /// <summary>
        /// ucCaseMainInfo<br></br>
        /// [功能描述: 病案基本信息录入]<br></br>
        /// [创 建 者: 张俊义]<br></br>
        /// [创建时间: 2007-04-20]<br></br>
        /// <修改记录 
        ///		修改人='金鹤' 
        ///		修改时间='2009-09-16' 
        ///		修改目的='作为随访录入的基本信息读取页面出现
        ///     {E9F858A6-BDBC-4052-BA57-68755055FB80}
        ///              '
        ///		修改描述='
        ///         增加属性，标识出本次加载的窗体是否为随访录入窗体
        ///             '
        ///  />
        /// </summary>
        public ucCaseMainInfo()
        {
            InitializeComponent();
            #region {E9F858A6-BDBC-4052-BA57-68755055FB80}
            //调整各空间加载时间提前于当前窗体的Load事件

            #region  费用
            this.ucFeeInfo1.InitInfo();
            #endregion

            #region  妇婴
            ucBabyCardInput1.InitInfo();
            #endregion

            #region 手术
            this.ucOperation1.InitInfo();
            ucOperation1.InitICDList();
            //thread = new System.Threading.Thread(ucOperation1.InitICDList);
            //thread.Start();
            #endregion

            #region 肿瘤
            //thread = new System.Threading.Thread(this.ucTumourCard2.InitInfo);
            //thread.Start();
            this.ucTumourCard2.InitInfo();
            #endregion

            #region  转科
            //thread = new System.Threading.Thread(this.ucChangeDept1.InitInfo);
            //thread.Start(); 
            this.ucChangeDept1.InitInfo();
            #endregion

            #region  诊断信息
            //thread = new System.Threading.Thread(this.ucDiagNoseInput1.InitInfo);
            //thread.Start();  
            this.ucDiagNoseInput1.InitInfo();
            #endregion

            #endregion

        }

        #region {E9F858A6-BDBC-4052-BA57-68755055FB80}

        #region 属性

        private bool isVisitInput=false;

        /// <summary>
        /// 是否为随访输入窗体
        /// </summary>
        public bool IsVisitInput
        {
            get { return isVisitInput; }
            set { isVisitInput = value; }
        }

        /// <summary>
        /// 是否拟诊  true 拟诊 false 非拟诊 chengym
        /// 用法：拟诊 直接录入描述性诊断  可以再编辑 icd10  非拟诊 直接录入 ICD10
        /// </summary>
        [Category("拟诊设置"), Description("诊断编辑界面,True：可以输入描述性诊断 False:直接录入ICD10")]
        public bool IsDoubt
        {
            get { return this.ucDiagNoseInput1.IsDoubt; }
            set { this.ucDiagNoseInput1.IsDoubt = value; }
        }

        // =======================以下两个属性作用:如果病案中不存在该患者,责使用窗体传来的值，作为查询基本信息的依据
        private string patientNo;
        /// <summary>
        /// 住院号
        /// </summary>
        public string PatientNo
        {
            get { return patientNo; }
            set { patientNo = value; }
        }

        private string cardNo;
        /// <summary>
        /// 病历号
        /// </summary>
        public string CardNo
        {
            get { return cardNo; }
            set { cardNo = value; }
        }
        //============================
        /// <summary>
        /// 是否回写首页信息到主表
        /// </summary>
        private bool isUpdataFinIprInmaininfo = true;
        /// <summary>
        /// 是否回写首页信息到主表
        /// </summary>
        [Category("是否回写首页信息到主表"), Description("回写主表操作,True：回写 False:不会写")]
        public bool IsUpdataFinIprInmaininfo
        {
            get { return this.isUpdataFinIprInmaininfo; }
            set { this.isUpdataFinIprInmaininfo = value; }
        }
        #endregion

        #endregion


        #region  全局变量
        //标志 标识是医生站用还是病案调用
        private FS.HISFC.Models.HealthRecord.EnumServer.frmTypes frmType = FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC;
        //暂存当前修改人的病案基本信息
        private FS.HISFC.Models.HealthRecord.Base CaseBase = new FS.HISFC.Models.HealthRecord.Base();
        //病案基本信息操作类
        private FS.HISFC.BizLogic.HealthRecord.Base baseDml = new FS.HISFC.BizLogic.HealthRecord.Base();

        #region {E9F858A6-BDBC-4052-BA57-68755055FB80}

        //随访基本信息操作类
        FS.HISFC.BizLogic.HealthRecord.Visit.LinkWay linkWayManager
            = new FS.HISFC.BizLogic.HealthRecord.Visit.LinkWay();

        /// <summary>
        /// 随访业务组合类

        /// </summary>
        FS.HISFC.BizProcess.Integrate.HealthRecord.Visit.Visit visitIntergrate
            = new FS.HISFC.BizProcess.Integrate.HealthRecord.Visit.Visit();

        /// <summary>
        /// 随访明细业务类
        /// </summary>
        FS.HISFC.BizLogic.HealthRecord.Visit.VisitRecord visitRecordManager
            = new FS.HISFC.BizLogic.HealthRecord.Visit.VisitRecord();


        //电话状态列表
        FarPoint.Win.Spread.CellType.ComboBoxCellType telStateBox;

        //与患者关系
        FarPoint.Win.Spread.CellType.ComboBoxCellType relationBox;

        //add chengym 临时住院流水号，LoadInfo中用于判断是否同一个患者，不同患者重新获取数据
        private string TempInpatient = string.Empty;
        private bool isNeedLoadInfo = false;
        private static ArrayList icdList = new ArrayList();

        private string in_State = string.Empty;//记录患者目前状态 loadinfo时查主表赋值
        private DateTime dt_out = System.DateTime.Now.AddYears(-1);//出院日期
        private string bedNo = string.Empty;//出院床位
        private string dept_out = string.Empty;//住院主表出院科室编码

        //end add chengym
        #endregion


        //定义变量
        FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
        //门诊诊断 
        private FS.HISFC.Models.HealthRecord.Diagnose clinicDiag = null;
        //入院诊断 
        private FS.HISFC.Models.HealthRecord.Diagnose InDiag = null;
        //合同单位
        private FS.HISFC.BizLogic.Fee.PactUnitInfo pactManager = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
        //转科信息
        ArrayList changeDept = new ArrayList();
        //第一次转科
        private FS.HISFC.Models.RADT.Location firDept = null;
        //第二次转科信息
        private FS.HISFC.Models.RADT.Location secDept = null;
        //第三次转科信息
        private FS.HISFC.Models.RADT.Location thirDept = null;
        FS.HISFC.BizLogic.HealthRecord.DeptShift deptChange = new FS.HISFC.BizLogic.HealthRecord.DeptShift();
        FS.HISFC.BizLogic.HealthRecord.Fee healthRecordFee = new FS.HISFC.BizLogic.HealthRecord.Fee();
        HealthRecord.Search.ucPatientList ucPatient = new HealthRecord.Search.ucPatientList();
        //标识手工输入的状态是插入还是更新  0默认状态  1  插入 2更新  
        private int HandCraft = 0;

        //		//入院诊断的标志位  0 默认， 1 修改 ，2 插入， 3 删除 
        //		public int RuDiag = 0;
        //		//门诊诊断的标志位  0 默认， 1 修改 ，2 插入， 3 删除 
        //		public int menDiag = 0;
        //保存病案的状态
        private int CaseFlag = 0;
        //提示窗体
        ucDiagNoseCheck frm = null;
        private FS.FrameWork.Models.NeuObject localObj = new FS.FrameWork.Models.NeuObject();
        private FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface healthRecordPrint = null;//打印接口
        //{DC8452B8-FF77-4639-9522-A2CCED4B8A5C}
        private FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceBack healthRecordPrintBack = null;//打印接口 背面


        //{B71C3094-BDC8-4fe8-A6F1-7CEB2AEC55DD}
        private FS.HISFC.BizProcess.Integrate.Manager manageIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        ////{FB6490C7-4A01-443c-8EF4-CC7281379979}
        /// <summary>
        /// 是否全院
        /// </summary>
        private bool isAllDept = false;

        private FS.FrameWork.Public.ObjectHelper caseStusHelper = null;

        /// <summary>
        /// 转科操作类型 1 自动加载系统转科信息 0 手工录入
        /// </summary>
        private int changeDeptType = 0;

        /// <summary>
        /// 手术操作类型 1 自动加载系统手术信息 0 手工录入
        /// </summary>
        private int operationType = 0;

        //{701EECD4-5ADF-4323-9FC4-73881FB1632D}add20120517
        /// <summary>
        /// 入院时间，确诊时间，转科时间是否取接诊时间（默认false）
        /// </summary>
        private bool isSetInDate = false;
        private FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
        private DateTime dtArrive = System.DateTime.Now;

        #endregion

        #region 控制属性
        ////{FB6490C7-4A01-443c-8EF4-CC7281379979}
        /// <summary>
        /// 是否全院科室
        /// </summary>
        [Category("控件设置"), Description("是否全院科室,True：全院 False:当前科室")]
        public bool IsAllDept
        {
            get
            {
                return isAllDept;
            }
            set
            {
                isAllDept = value;
            }
        }
        /// <summary>
        /// 医生or病案录入界面
        /// </summary>
        [Category("控件设置"), Description("医生or病案录入界面")]
        public FS.HISFC.Models.HealthRecord.EnumServer.frmTypes FrmTypes
        {
            get
            {
                return frmType;
            }
            set
            {
                frmType = value;
            }
        }
        //隐藏病历号录入框
        private bool isHideInputControl = true;
        /// <summary>
        /// 是否隐藏病历号录入框
        /// </summary>
        [Category("是否隐藏病历号录入框"), Description("是否隐藏病历号录入框：　医生站隐藏")]
        public bool IsHideInputControl
        {
            get
            {
                return this.isHideInputControl;
            }
            set
            {
                this.isHideInputControl = value;
                if (value)
                {
                    this.panel2.Width = 0;
                }
            }
        }
        #endregion

        #region 初始化
        //System.Threading.Thread thread = null;
        /// <summary>
        /// 初始化控件
        /// </summary>
        /// <returns></returns>
        public int InitCaseMainInfo()
        {
            InitCountryList();//国际

            #region {E9F858A6-BDBC-4052-BA57-68755055FB80}

            if (!IsVisitInput)//判断窗体类型(病案录入/随访录入)
            {
                this.tab1.TabPages.Remove(tabPage8);
            }
            else
            {
                this.panel2.Visible = false;
            }
            #endregion

            #region 加载选择框
            this.Controls.Add(this.ucPatient);
            this.ucPatient.BringToFront();
            this.ucPatient.Visible = false;
            this.ucPatient.SelectItem += new HealthRecord.Search.ucPatientList.ListShowdelegate(ucPatient_SelectItem);
            #endregion
            //{701EECD4-5ADF-4323-9FC4-73881FB1632D}初始化参数add20120517
            FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
            this.isSetInDate =ctrlParamIntegrate .GetControlParam <bool>("CASE03",true ,false );
            //this.InitControlParam();
            return 1;
        }
        /// <summary>
        /// 初始化控制参数
        /// </summary>
        private void InitControlParam()
        {
            FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
            changeDeptType = ctrlParamIntegrate.GetControlParam<int>("CASE01", true, 1);
            operationType = ctrlParamIntegrate.GetControlParam<int>("CASE02", true, 1);
        }
        private void ucCaseMainInfo_Load(object sender, EventArgs e)
        {
            InitCaseMainInfo();
            if (!this.isHideInputControl)
            {
                InitTreeView();
            }
            else
            {
                this.panel2.Width = 0;
            }
            this.txtCaseNum.Leave += new EventHandler(txtCaseNum_Leave);
            this.txtPatientNOSearch.Focus();
        }
        #endregion

        #region 工具栏信息

        /// <summary>
        /// 定义工具栏服务
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        #region 初始化工具栏
        /// <summary>
        /// 初始化工具栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("删除(&D)", "删除(&D)", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
            //{DC8452B8-FF77-4639-9522-A2CCED4B8A5C}
            toolBarService.AddToolButton("打印第二页", "打印第二页", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);

            toolBarService.AddToolButton("提交", "提交", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Z执行, true, false, null);

            toolBarService.AddToolButton("打回", "打回到医生站保存状态", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Z召回, true, false, null);
            return toolBarService;
        }
        #endregion

        #region 工具栏增加按钮单击事件
        /// <summary>
        /// 工具栏增加按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "删除(&D)":
                    DeleteActiveRow();
                    break;
                //{DC8452B8-FF77-4639-9522-A2CCED4B8A5C}
                case "打印第二页":
                    PrintBack(this.CaseBase);
                    break;
                case "提交":
                    this.Save(1);
                    break;
                case "打回":
                    this.Save(2);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #endregion

        #region  所有的下拉列表
        private int InitCountryList()
        {
            //获取列表
            ArrayList list = FS.HISFC.Models.Base.SexEnumService.List();
            //设置列表
            this.txtPatientSex.AddItems(list);
            //g查询国家列表
            ArrayList list1 = con.GetList(FS.HISFC.Models.Base.EnumConstant.COUNTRY);
            this.txtCountry.AddItems(list1);

            //查询民族列表
            ArrayList Nationallist1 = con.GetList(FS.HISFC.Models.Base.EnumConstant.NATION);
            this.txtNationality.AddItems(Nationallist1);

            //查询职业列表
            ArrayList Professionlist = con.GetList(FS.HISFC.Models.Base.EnumConstant.PROFESSION);
            this.txtProfession.AddItems(Professionlist);
            //血型列表
            ArrayList BloodTypeList = con.GetList("CASEBLOODTYPE");// con.GetList(FS.HISFC.Models.Base.EnumConstant.BLOODTYPE);// baseDml.GetBloodType();
            this.txtBloodType.AddItems(BloodTypeList);
            //婚姻列表
            ArrayList MaritalStatusList = FS.HISFC.Models.RADT.MaritalStatusEnumService.List();
            this.txtMaritalStatus.AddItems(MaritalStatusList);
            //结算类别{B71C3094-BDC8-4fe8-A6F1-7CEB2AEC55DD}
            //ArrayList pactKindlist = con.GetList(FS.HISFC.Models.Base.EnumConstant.PACTUNIT);// baseDml.GetPayKindCode(); //GetList(FS.HISFC.Models.Base.EnumConstant.PACTUNIT);
            ArrayList pactKindlist = this.manageIntegrate.QueryPactUnitAll();
            this.txtPactKind.AddItems(pactKindlist);
            //与联系人关系
            ArrayList RelationList = con.GetList(FS.HISFC.Models.Base.EnumConstant.RELATIVE);
            this.txtRelation.AddItems(RelationList);

            FS.HISFC.BizLogic.Manager.Person person = new FS.HISFC.BizLogic.Manager.Person();
            //获取医生列表
            ArrayList DoctorList = person.GetEmployeeAll();//.GetEmployee(FS.HISFC.Models.RADT.PersonType.enuPersonType.D);
            this.txtInputDoc.AddItems(DoctorList);
            this.txtCoordinate.AddItems(DoctorList);
            this.txtOperationCode.AddItems(DoctorList);
            this.txtAdmittingDoctor.AddItems(DoctorList);
            this.txtAttendingDoctor.AddItems(DoctorList);
            this.txtConsultingDoctor.AddItems(DoctorList);
            this.txtRefresherDocd.AddItems(DoctorList);
            txtClinicDocd.AddItems(DoctorList);
            txtGraDocCode.AddItems(DoctorList);
            txtQcDocd.AddItems(DoctorList);
            txtQcNucd.AddItems(DoctorList);
            txtCodingCode.AddItems(DoctorList);
            this.txtPraDocCode.AddItems(DoctorList);
            this.txtDeptChiefDoc.AddItems(DoctorList);
            //获取病人来源
            //			ArrayList InAvenuelist = baseDml.GetPatientSource();
            //ArrayList InAvenuelist = con.GetAllList(FS.HISFC.Models.Base.EnumConstant.INAVENUE);
            //this.txtInAvenue.AddItems(InAvenuelist);
            ArrayList Insourcelist = con.GetAllList("PATIENTINSOURCE");
            this.txtInAvenue.AddItems(Insourcelist);
            //入院情况
            ArrayList CircsList = con.GetList(FS.HISFC.Models.Base.EnumConstant.INCIRCS);
            this.txtCircs.AddItems(CircsList);

            //药物过敏
            ArrayList arraylist = con.GetList(FS.HISFC.Models.Base.EnumConstant.PHARMACYALLERGIC);// baseDml.GetHbsagList();
            this.txtHbsag.AddItems(arraylist);

            ////诊断符合情况
            //ArrayList diagAccord = con.GetList(FS.HISFC.Models.Base.EnumConstant.DIAGNOSEACCORD);// baseDml.GetDiagAccord();
            //this.CePi.AddItems(diagAccord);

            //病案质量
            ArrayList qcList = con.GetList(FS.HISFC.Models.Base.EnumConstant.CASEQUALITY);
            txtMrQual.AddItems(qcList);

            //RH性质 
            ArrayList RHList = con.GetList(FS.HISFC.Models.Base.EnumConstant.RHSTATE); //baseDml.GetRHType();
            txtRhBlood.AddItems(RHList);

            //ArrayList listAccord = con.GetList(FS.HISFC.Models.Base.EnumConstant.ACCORDSTAT);
            //txtHbsag.AddItems(listAccord);
            //txtHcvAb.AddItems(listAccord);
            //txtHivAb.AddItems(listAccord);
            //txtPiPo.AddItems(listAccord);
            //txtOpbOpa.AddItems(listAccord);
            //txtClPa.AddItems(listAccord);
            //txtFsBl.AddItems(listAccord);
            //txtCePi.AddItems(listAccord);
            //HBsAg HCV-Ab HIV-Ab
            ArrayList listAssayType = con.GetList("ASSAYTYPE");
            txtHbsag.AddItems(listAssayType);
            txtHcvAb.AddItems(listAssayType);
            txtHivAb.AddItems(listAssayType);

            //诊断符合情况
            ArrayList listAccord = con.GetList("ACCORDSTATNEW");
            txtPiPo.AddItems(listAccord);
            txtOpbOpa.AddItems(listAccord);
            txtClPa.AddItems(listAccord);
            txtFsBl.AddItems(listAccord);
            txtCePi.AddItems(listAccord);
            //科室下拉列表
            FS.HISFC.BizLogic.Manager.Department dept = new FS.HISFC.BizLogic.Manager.Department();
            ArrayList deptList = dept.GetDeptmentAll();
            txtFirstDept.AddItems(deptList);
            txtDeptSecond.AddItems(deptList);
            txtDeptInHospital.AddItems(deptList);
            txtDeptThird.AddItems(deptList);
            txtDeptOut.AddItems(deptList);

            //InitList(DeptListBox, deptList);
            //血液反应

            ArrayList ReactionBloodList = con.GetList(FS.HISFC.Models.Base.EnumConstant.BLOODREACTION);// baseDml.GetReactionBlood();
            txtReactionBlood.AddItems(ReactionBloodList);
            txtReactionTransfuse.AddItems(ReactionBloodList);//输液反应

            //感染部位
            //ArrayList InfectionPosition = con.GetList("INFECTPOSITION");
            //过敏药物
            ArrayList PharmacyAllergic = con.GetList("PHARMACYALLERGIC");
            this.txtPharmacyAllergic1.AddItems(PharmacyAllergic);
            this.txtPharmacyAllergic2.AddItems(PharmacyAllergic);

            ArrayList ReportedList = con.GetList("Reported");
            this.txtFourDiseasesReport.AddItems(ReportedList);
            this.txtInfectionDiseasesReport.AddItems(ReportedList);
            #region {E9F858A6-BDBC-4052-BA57-68755055FB80}


            //随访方式
            this.cmbLinkType.AddItems(con.GetList("CASE06"));
            //一般情况
            cmbCircs.AddItems(con.GetList("CASE07"));
            //症状表现
            cmbSymptom.AddItems(con.GetList("CASE10"));
            //后遗症
            neuComboBoxSequela.AddItems(con.GetList("CASE09"));
            //死亡原因
            neuComboBoxDeadReason.AddItems(con.GetList("CASE08"));
            //转移部位
            neuComboBoxTransferPosition.AddItems(con.GetList("CASE11"));

            //随访结果
            cmbResult.AddItems(con.GetList("CASE14"));


            //加载电话状态
            InitTelStateList();

            //加载关系
            IninRelation();

            #endregion

            FS.HISFC.BizLogic.HealthRecord.ICD icd = new FS.HISFC.BizLogic.HealthRecord.ICD();
            ArrayList listIcd = icd.Query(FS.HISFC.Models.HealthRecord.EnumServer.ICDTypes.ICD10, FS.HISFC.Models.HealthRecord.EnumServer.QueryTypes.Valid);
            foreach (FS.HISFC.Models.HealthRecord.ICD info in listIcd)
            {
                if (info.ID.IndexOf('U') == 0 || info.ID.IndexOf('V') == 0 || info.ID.IndexOf('W') == 0 || info.ID.IndexOf('X') == 0 || info.ID.IndexOf('Y') == 0 || info.ID.IndexOf('Z') == 0)
                {
                    icdList.Add(info);
                }
            }
            FS.HISFC.Models.HealthRecord.ICD icdInfo = new FS.HISFC.Models.HealthRecord.ICD();
            icdInfo.ID = "MS999";
            icdInfo.Name = "/";
            icdInfo.SpellCode = "WFX";
            icdInfo.WBCode = "FNG";
            icdList.Add(icdInfo);
            this.txtInjuryOrPoisoningCause.AddItems(icdList);

            this.caseStusHelper = new FS.FrameWork.Public.ObjectHelper(con.GetList("CASESTUS"));
            return 1;
        }

        #endregion

        #region 查询患者信息

        protected override int OnQuery(object sender, object neuObject)
        {
            FS.HISFC.Components.Common.Forms.frmSearchPatient frm = new FS.HISFC.Components.Common.Forms.frmSearchPatient();
            frm.SelectItem += new FS.HISFC.Components.Common.Forms.frmSearchPatient.ListShowdelegate(frm_SelectItem);
            frm.Show();
            
            return base.OnQuery(sender, neuObject);
        }

        void frm_SelectItem(FS.HISFC.Models.HealthRecord.Base obj)
        {
            LoadInfo(obj.PatientInfo.ID, this.frmType);
        }

        /// <summary>
        /// 初始化TreeView
        /// </summary>
        public void InitTreeView()
        {
            ArrayList al = new ArrayList();
            TreeNode tnParent;
            this.treeView1.HideSelection = false;
            //Neuosft.FS.HISFC.BizProcess.Integrate.RADT pQuery = new FS.HISFC.BizProcess.Integrate.RADT(); //t.RADT.InPatient();
            this.treeView1.BeginUpdate();
            this.treeView1.Nodes.Clear();
            //画树头
            tnParent = new TreeNode();
            tnParent.Text = "最近出院患者";
            tnParent.Tag = "%";
            try
            {
                tnParent.ImageIndex = 0;
                tnParent.SelectedImageIndex = 1;
            }
            catch { }
            this.treeView1.Nodes.Add(tnParent);
            DateTime dt = this.baseDml.GetDateTimeFromSysDateTime();
            DateTime dt2 = dt.AddDays(-3);
            ////{FB6490C7-4A01-443c-8EF4-CC7281379979}
            string strBegin = dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day.ToString() + " 23:59:59";
            string strEnd = dt2.Year.ToString() + "-" + dt2.Month.ToString() + "-" + dt2.Day.ToString() + " 00:00:00";
            FS.HISFC.Models.Base.Employee personObj = (FS.HISFC.Models.Base.Employee)baseDml.Operator;

            //获得最近出院结算患者信息
            if (isAllDept)
            {
                al = this.baseDml.QueryPatientOutHospital(strEnd, strBegin, "ALL");
            }
            else
            {
                al = this.baseDml.QueryPatientOutHospital(strEnd, strBegin, personObj.Dept.ID);
            }
            if (al == null)
            {
                MessageBox.Show("查询出院病人信息失败");
                return;
            }

            foreach (FS.HISFC.Models.RADT.PatientInfo pInfo in al)
            {
                TreeNode tnPatient = new TreeNode();

                tnPatient.Text = pInfo.Name + "[" + pInfo.PID.PatientNO + "]";
                tnPatient.Tag = pInfo;
                try
                {
                    tnPatient.ImageIndex = 2;
                    tnPatient.SelectedImageIndex = 3;
                }
                catch { }
                tnParent.Nodes.Add(tnPatient);
            }

            tnParent.Expand();
            this.treeView1.EndUpdate();
        }

        private void patientTreeView_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            if (e.Node.Tag.GetType() == typeof(FS.HISFC.Models.RADT.PatientInfo))
            {
                //				this.Reset();
                //				caseBase.PatientInfo = ((FS.HISFC.Models.RADT.PatientInfo)e.Node.Tag).Clone();
                //				this.ucCaseFirstPage1.Item = caseBase.Clone();
                //				ArrayList alOrg = new ArrayList();
                //				ArrayList alNew = new ArrayList();
                //				alOrg = myBaseDML.GetInhosDiagInfo( caseBase.PatientInfo.ID, "%");
                //				FS.HISFC.Models.HealthRecord.Diagnose dg;
                //				for(int i = 0; i < alOrg.Count; i++)
                //				{
                //					dg = new FS.HISFC.Models.HealthRecord.Diagnose();
                //					dg.DiagInfo = ((FS.HISFC.Models.Case.DiagnoseBase)alOrg[i]).Clone();
                //					alNew.Add( dg );
                //				}
                //				this.ucCaseFirstPage1.AlDiag = alNew;
            }
        }


        #endregion

        #region 事件

        #region 性别

        private void PatientSex_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                dtPatientBirthday.Focus();
            }
        }
        #endregion
        #region 门诊诊断
        private void ClinicDiag_Enter(object sender, System.EventArgs e)
        {
            //			//保存但前活动控件
            //			if(ClinicDiag.ReadOnly)
            //			{
            //				return ;
            //			}
            //			contralActive = this.ClinicDiag;
            //			listBoxActive = ICDListBox;
            //			ListBoxActiveVisible(true);
        }

        private void ClinicDiag_TextChanged(object sender, System.EventArgs e)
        {
            //			ListFilter();
        }
        private void ClinicDiag_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtClinicDocd.Focus();
            }
            //			else if(e.KeyData == Keys.Up)
            //			{
            //				ICDListBox.PriorRow();
            //			}
            //			else if(e.KeyData == Keys.Down)
            //			{
            //				ICDListBox.NextRow();
            //			}
        }
        #endregion
        #region 国籍
        private void Country_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtDIST.Focus();
            }
        }
        #endregion
        #region  民族
        private void Nationality_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtCountry.Focus();
            }
        }
        #endregion
        #region  血型
        private void BloodType_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtRhBlood.Focus();
            }
        }
        #endregion
        #region 婚姻
        private void MaritalStatus_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtProfession.Focus();
            }
        }
        #endregion
        #region 职业
        private void Profession_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtAreaCode.Focus();
            }
        }
        #endregion
        #region 联系人关系
        private void Relation_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtLinkmanTel.Focus();
            }
        }
        #endregion
        #region  入院情况


        private void Circs_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtInAvenue.Focus();
            }
        }

        #endregion
        #region 门诊医生
        private void ClinicDocd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtPiDays.Focus();
            }
        }
        #endregion
        #region 病人来源
        private void InAvenue_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtDateOut.Focus();
            }
        }
        #endregion
        #region 药物过敏
        private void Hbsag_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtHcvAb.Focus();
            }
        }
        private void HcvAb_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtHivAb.Focus();
            }
        }
        private void HivAb_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtCePi.Focus();
            }
        }
        #endregion
        #region 诊断符合

        private void CePi_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtPiPo.Focus();
            }
        }
        private void PiPo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtOpbOpa.Focus();
            }
        }

        private void OpbOpa_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtClPa.Focus();
            }
        }
        private void ClPa_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtFsBl.Focus();
            }
        }

        private void FsBl_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtSalvTimes.Focus();
            }
        }
        #endregion
        #region  住院医生
        private void AdmittingDoctor_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtRefresherDocd.Focus();
            }
        }
        #endregion
        #region 进修医师
        private void RefresherDocd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtPraDocCode.Focus();
            }
        }
        #endregion
        #region 研究生实习医师
        private void GraDocCode_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtComeFrom.Focus();
            }
        }
        #endregion
        #region 实习医生
        private void PraDocCode_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtGraDocCode.Focus();
            }
        }
        #endregion
        #region  主治医师
        private void AttendingDoctor_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtAdmittingDoctor.Focus();
            }
        }
        #endregion
        #region 主任医师
        private void ConsultingDoctor_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtAttendingDoctor.Focus();
            }
        }
        #endregion
        #region  质控护士
        private void QcNucd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtCodingCode.Focus();
            }
        }

        #endregion
        #region 质控医生
        private void QcDocd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtQcNucd.Focus();
            }
        }
        #endregion
        #region 编码员
        private void CodingCode_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtCoordinate.Focus();
            }
        }
        #endregion
        #region 整理员
        private void textBox33_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtOperationCode.Focus();
            }
        }
        #endregion
        #region 病案质量
        private void MrQual_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtQcDocd.Focus();
            }
        }
        #endregion
        #region  输血反映
        private void ReactionBlood_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                //if (ReactionBlood.Tag != null)
                //{
                //    //if (ReactionBlood.Tag.ToString() != "2")
                //    //{
                //    //    BloodRed.Focus();
                //    //}
                //    //else
                //    //{
                //        //院际会诊次数
                //        InconNum.Focus();
                //    //}
                //}
                //else
                //{
                txtBloodRed.Focus();
                //}
            }
        }

        #endregion
        #region 输入员
        private void InputDoc_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                //单独判断 先跳到诊断吧
                this.tab1.SelectedIndex = 1;
            }
        }
        #endregion
        #region  入院诊断


        private void RuyuanDiagNose_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                //单独判断 先跳到诊断
                this.txtConsultingDoctor.Focus();
            }
            //else if (e.KeyData == Keys.Up)
            //{
            //    listBoxActive.PriorRow();
            //}
            //else if (e.KeyData == Keys.Down)
            //{
            //    listBoxActive.NextRow();
            //}
        }


        #endregion
        #region  入院科室
        private void DeptInHospital_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtCircs.Focus();
            }
        }
        #endregion
        #region  RH反应
        private void RhBlood_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtReactionBlood.Focus();
            }
        }
        #endregion
        #region  出生地
        private void AreaCode_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtNationality.Focus();
            }
        }
        #endregion
        #region 转科1
        private void firstDept_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.dtSecond.Focus();
            }
        }
        #endregion
        private void deptSecond_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.dtThird.Focus();
            }
        }
        #endregion

        #region 删除当前行
        /// <summary>
        ///删除当前行
        /// </summary>
        /// <returns></returns>
        public int DeleteActiveRow()
        {
            switch (this.tab1.SelectedTab.Name)
            {
                // case "手术信息":
                case "tabPage6":
                    if (operationType == 0)
                    {
                        this.ucOperation1.DeleteActiveRow();
                    }
                    break;
                // case "诊断信息":
                case "tabPage5":
                    this.ucDiagNoseInput1.DeleteActiveRow();
                    break;
                // case "转科信息":
                case "tabPage3":
                    this.ucChangeDept1.DeleteActiveRow();
                    break;
                // case "肿瘤信息":
                case "tabPage7":
                    this.ucTumourCard2.DeleteActiveRow();
                    break;
                // case "妇婴信息":
                case "tabPage2":
                    this.ucBabyCardInput1.DeleteActiveRow();
                    break;
                //case "基本信息":
                case "tabPage1":
                    MessageBox.Show("基本信息不允许删除");
                    break;
            }
            return 1;
        }
        #endregion

        #region 保存数据
        /// <summary>
        /// 重写保存函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Save(object sender, object neuObject)
        {
            return this.Save(0);
        }

        /// <summary>
        /// 保存函数
        /// </summary>
        /// <param name="type">保存类型 0 暂存 1 提交 2 回退初始状态</param>
        /// <returns></returns>
        private int Save(int type)
        {
            if (CaseBase == null || CaseBase.PatientInfo.ID == null || CaseBase.PatientInfo.ID == "")
            {
                MessageBox.Show("请输入住院流水号或选择病人");
                return -1;
            }
            #region 判断患者是否在院
            FS.HISFC.BizLogic.RADT.InPatient radtMana = new FS.HISFC.BizLogic.RADT.InPatient();
            FS.HISFC.Models.RADT.PatientInfo patientInfoForUpdate = radtMana.QueryPatientInfoByInpatientNO(CaseBase.PatientInfo.ID);

            if (patientInfoForUpdate != null)
            {
                if (patientInfoForUpdate.PVisit.InState.ID != null)
                {
                    if (patientInfoForUpdate.PVisit.InState.ID.ToString() == "R" || patientInfoForUpdate.PVisit.InState.ID.ToString() == "I")
                    {
                        if (MessageBox.Show("该患者仍在住院，是否保存病案信息？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                        {
                            return 0;
                        }
                    }
                }
            }
            #endregion
            #region  判断诊断是否符合约束  暂时屏蔽判断 回去再看看具体的内容2012-3-30 chengym
            FS.HISFC.BizLogic.HealthRecord.Diagnose diagNose = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
            //if (this.frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC) //医生站提示 病案室不需要提示
            //{
            //    if (DiagValueState(diagNose) != 1)
            //    {
            //        return -1;
            //    }
            //}

            System.DateTime dt = diagNose.GetDateTimeFromSysDateTime(); //获取系统时间
            #endregion
            #region  判断住院号和住院次数是否已经存在
            int intI = baseDml.ExistCase(this.CaseBase.PatientInfo.ID, txtCaseNum.Text, txtInTimes.Text);
            if (intI == -1)
            {
                MessageBox.Show("查询数据失败");
                return -1;
            }
            if (intI == 2)
            {
                MessageBox.Show(txtCaseNum.Text + " 的" + "第 " + txtInTimes.Text + " 次入院已经存在,请更改入院次数");
                return -1;
            }
            #endregion
            //建立事务

            //FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction(baseDml.Connection);
            try
            {

                if (CaseBase == null)
                {
                    return -2;
                }
                if (CaseBase.PatientInfo.ID == "")
                {
                    MessageBox.Show("请指定要保存病案的病人");
                    return -2;
                }
                if (CaseBase.PatientInfo.CaseState == "0")
                {
                    MessageBox.Show("病人不允许有病案");
                    return 0;
                }
                if (this.frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC && FS.FrameWork.Function.NConvert.ToInt32(CaseBase.PatientInfo.CaseState) > 2) //医生站提交之后状态
                {
                    MessageBox.Show("病案状态为【" + this.caseStusHelper.GetName(CaseBase.PatientInfo.CaseState) + "】不允许再修改");
                    return -3;
                }
                if (this.frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC && (HandCraft == 1 || HandCraft == 2))
                {
                    MessageBox.Show("病案室已经存档不允许修改");
                    return -3;
                }
                if (HandCraft == 1 || HandCraft == 2)  //手工录入 插入
                {
                    CaseBase.PatientInfo.CaseState = "3";
                    CaseBase.IsHandCraft = "1";
                }

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                //trans.BeginTransaction();

                baseDml.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                diagNose.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                deptChange.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                healthRecordFee.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                #region 病案基本信息
                FS.HISFC.Models.HealthRecord.Base info = new FS.HISFC.Models.HealthRecord.Base();
                int i = this.GetInfoFromPanel(info);
                if (ValidState(info) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return -1;
                }
                if (type == 1)
                {
                    info.PatientInfo.CaseState = (FS.FrameWork.Function.NConvert.ToInt32(info.PatientInfo.CaseState) + 1).ToString();
                    if (MessageBox.Show("确认提交到【" + this.caseStusHelper.GetName(info.PatientInfo.CaseState) + "】状态吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return -1;
                    }
                }
                else if (type == 2)
                {
                    frmRemark frmRemark = new frmRemark();
                    frmRemark.CaseBase = info;
                    frmRemark.ShowDialog();
                    if (frmRemark.DialogResult== DialogResult.No)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return -1;
                    }
                    info.PatientInfo.CaseState = "2";
                }
                //先执行更新操作 
                if (baseDml.UpdateBaseInfo(info) < 1)
                {
                    //更新失败 则执行插入操作 
                    if (baseDml.InsertBaseInfo(info) < 1)
                    {
                        //回退
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("保存病人基本信息失败 :" + baseDml.Err);
                        return -1;
                    }
                }
                this.CaseBase = info;//add by chengym 2011-9-22 重新获取信息
                this.ucChangeDept1.Patient = info.PatientInfo;
                this.ucDiagNoseInput1.Patient = info.PatientInfo;
                this.ucOperation1.Patient = info.PatientInfo;
                this.ucBabyCardInput1.Patient = info.PatientInfo;
                this.ucTumourCard2.Patient = info.PatientInfo;
                this.ucFeeInfo1.Patient = info.PatientInfo;
                #region  最开始的设计,根据 住院主表的 病案标志 判断时更新还是插入操作  发觉不好控制 这里删掉了
                //				if(CaseBase.PatientInfo.CaseState == "1") 
                //				{
                //					//可以有病案 现在没有保存过的病案
                //					if(baseDml.InsertBaseInfo(info) < 1)
                //					{
                //						//回退
                //						FS.FrameWork.Management.PublicTrans.RollBack();
                //						MessageBox.Show("保存病人基本信息失败 :" +baseDml.Err );
                //						return -1;
                //					}
                //					#region 门诊诊断
                //					//					if(clinicDiag != null)
                //					//					{
                //					//						if(diagNose.InsertDiagnose(clinicDiag) < 1)
                //					//						{
                //					//							FS.FrameWork.Management.PublicTrans.RollBack();
                //					//							MessageBox.Show("保存门诊诊断失败 :" + diagNose.Err);
                //					//						}
                //					//					}
                //					#endregion 
                //					#region  入院诊断 
                //					//					if(InDiag != null)
                //					//					{
                //					//						if(diagNose.InsertDiagnose(InDiag) < 1)
                //					//						{
                //					//							FS.FrameWork.Management.PublicTrans.RollBack();
                //					//							MessageBox.Show("保存失败 :" + diagNose.Err);
                //					//						}
                //					//					}
                //					#endregion 
                //				}
                //				else if(CaseBase.PatientInfo.CaseState == "2" ||CaseBase.PatientInfo.CaseState == "3")
                //				{
                //					//已经保存过病案了 
                //					if(baseDml.UpdateBaseInfo(info)< 1)
                //					{
                //						FS.FrameWork.Management.PublicTrans.RollBack();
                //						MessageBox.Show("保存病人基本信息失败 :" +baseDml.Err );
                //						return -1;
                //					}
                //
                //					#region  门诊诊断 
                ////					if(clinicDiag != null)
                ////					{
                ////						if(diagNose.UpdateDiagnose(clinicDiag) < 1)
                ////						{
                ////							if(diagNose.InsertDiagnose(clinicDiag) < 1)
                ////							{
                ////								FS.FrameWork.Management.PublicTrans.RollBack();
                ////								MessageBox.Show("保存门诊诊断失败 :" + diagNose.Err);
                ////							}
                ////						}
                ////					}
                //					#endregion 
                //
                //					#region  入院诊断 
                ////					if(InDiag != null)
                ////					{
                ////						if(diagNose.UpdateDiagnose(InDiag) < 1)
                ////						{
                ////							if(diagNose.InsertDiagnose(InDiag) < 1)
                ////							{
                ////								FS.FrameWork.Management.PublicTrans.RollBack();
                ////								MessageBox.Show("保存入院诊断失败 :" + diagNose.Err);
                ////							}
                ////						}
                ////					}
                //					#endregion 
                //				}
                #endregion
                #endregion
                #region 转科信息
                if (this.changeDeptType == 0)
                {
                    //设计思路,先删除,然后同一插入.
                    //主界面上的 
                    ArrayList deptMain = new ArrayList();
                    //增加的 
                    ArrayList deptAdd = new ArrayList();
                    //修改过的 
                    ArrayList deptMod = new ArrayList();
                    #region 基本信息界面上的转科信息
                    #region 第一次转科信息
                    if (txtFirstDept.Tag != null && txtFirstDept.Text.Trim() != string.Empty)
                    {
                        FS.HISFC.Models.RADT.Location deptObj = new FS.HISFC.Models.RADT.Location();
                        deptObj.User02 = CaseBase.PatientInfo.ID;
                        deptObj.Dept.Name = txtFirstDept.Text;
                        deptObj.Dept.ID = txtFirstDept.Tag.ToString();
                        deptObj.Dept.Memo = this.dtFirstTime.Value.ToString();

                        deptMain.Add(deptObj);
                    }
                    #endregion
                    #region  第二次转科信息
                    if (txtDeptSecond.Tag != null && txtDeptSecond.Text.Trim() != string.Empty)
                    {
                        FS.HISFC.Models.RADT.Location deptObj = new FS.HISFC.Models.RADT.Location();
                        deptObj.User02 = CaseBase.PatientInfo.ID;
                        deptObj.Dept.Name = txtDeptSecond.Text;
                        deptObj.Dept.ID = txtDeptSecond.Tag.ToString();
                        deptObj.Dept.Memo = this.dtSecond.Value.ToString();
                        deptMain.Add(deptObj);
                    }
                    #endregion
                    #region 第三次转科
                    if (txtDeptThird.Tag != null && txtDeptThird.Text.Trim() != string.Empty)
                    {
                        FS.HISFC.Models.RADT.Location deptObj = new FS.HISFC.Models.RADT.Location();
                        deptObj.User02 = CaseBase.PatientInfo.ID;
                        deptObj.Dept.Name = txtDeptThird.Text;
                        deptObj.Dept.ID = txtDeptThird.Tag.ToString();
                        deptObj.Dept.Memo = this.dtThird.Value.ToString();
                        deptMain.Add(deptObj);
                    }
                    #endregion
                    #endregion
                    //删除空白行
                    this.ucChangeDept1.deleteRow();
                    //this.ucChangeDept1.GetList("D", deptDel);
                    this.ucChangeDept1.GetList("A", deptAdd);
                    this.ucChangeDept1.GetList("M", deptMod);

                    if (this.ucChangeDept1.ValueState(deptAdd) == -1 || this.ucChangeDept1.ValueState(deptMod) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return -3;
                    }
                    else
                    {
                        if (deptChange.DeleteChangeDept(info.PatientInfo.ID) < 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("转科信息保存失败" + baseDml.Err);
                            return -3;
                        }
                    }
                    //if (deptDel != null)
                    //{
                    //    foreach (FS.HISFC.Models.RADT.Location obj in deptDel)
                    //    {
                    //        if (deptChange.DeleteChangeDept(obj) < 1)
                    //        {
                    //            FS.FrameWork.Management.PublicTrans.RollBack();
                    //            MessageBox.Show("转科信息保存失败" + baseDml.Err);
                    //            return -1;
                    //        }
                    //    }
                    //}
                    if (deptAdd != null)
                    {
                        foreach (FS.HISFC.Models.RADT.Location obj in deptAdd)
                        {
                            if (deptChange.InsertChangeDept(obj) < 1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("转科信息保存失败" + baseDml.Err);
                                return -1;
                            }
                        }
                    }
                    if (deptMain != null)
                    {
                        foreach (FS.HISFC.Models.RADT.Location obj in deptMain)
                        {
                            if (deptChange.InsertChangeDept(obj) < 1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("转科信息保存失败" + baseDml.Err);
                                return -1;
                            }
                        }
                    }
                    if (deptMod != null)
                    {
                        foreach (FS.HISFC.Models.RADT.Location obj in deptMod)
                        {
                            if (deptChange.InsertChangeDept(obj) < 1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("转科信息保存失败" + baseDml.Err);
                                return -1;
                            }
                        }
                    }
                }
                //查询保存过的信息
                ArrayList tempChangeDept = deptChange.QueryChangeDeptFromShiftApply(CaseBase.PatientInfo.ID, "2");
                #endregion
                #region  病案诊断

                ////删除的
                //ArrayList diagDel = new ArrayList();
                ////增加的 
                //ArrayList diagAdd = new ArrayList();
                ////修改过的 
                //ArrayList diagMod = new ArrayList();
                //#region  门诊诊断
                ////				//0 默认， 1 修改 ，2 插入， 3 删除 
                ////				if(RuDiag == 1)
                ////				{
                ////					diagMod.Add(clinicDiag);
                ////				}
                ////				else if(RuDiag == 2)
                ////				{
                ////					diagAdd.Add(clinicDiag);
                ////				}
                ////				else if(RuDiag == 3)
                ////				{
                ////					diagDel.Add(clinicDiag);
                ////				}
                //#endregion
                //#region  入院诊断
                ////				if(menDiag == 1)
                ////				{
                ////					diagMod.Add(InDiag);
                ////				}
                ////				else if(menDiag == 2)
                ////				{
                ////					diagAdd.Add(InDiag);
                ////				}
                ////				else if(menDiag == 3)
                ////				{
                ////					diagDel.Add(InDiag);
                ////				}
                //#endregion
                ////删除空白行
                //this.ucDiagNoseInput1.deleteRow();
                //this.ucDiagNoseInput1.GetList("A", diagAdd);
                //this.ucDiagNoseInput1.GetList("M", diagMod);
                //this.ucDiagNoseInput1.GetList("D", diagDel);
                //if (this.ucDiagNoseInput1.ValueState(diagAdd) == -1 || this.ucDiagNoseInput1.ValueState(diagMod) == -1 || this.ucDiagNoseInput1.ValueState(diagDel) == -1)
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack(); //数据校验失败
                //    return -3;
                //}
                //if (diagDel != null)
                //{
                //    foreach (FS.HISFC.Models.HealthRecord.Diagnose obj in diagDel)
                //    {
                //        if (diagNose.DeleteDiagnoseSingle(obj.DiagInfo.Patient.ID, obj.DiagInfo.HappenNo, frmType) < 1)
                //        {
                //            FS.FrameWork.Management.PublicTrans.RollBack();
                //            MessageBox.Show("保存诊断信息失败" + diagNose.Err);
                //            return -1;
                //        }
                //    }
                //}
                //if (diagMod != null)
                //{
                //    foreach (FS.HISFC.Models.HealthRecord.Diagnose obj in diagMod)
                //    {
                //        if (diagNose.UpdateDiagnose(obj) < 1)
                //        {
                //            if (diagNose.InsertDiagnose(obj) < 1)
                //            {
                //                FS.FrameWork.Management.PublicTrans.RollBack();
                //                MessageBox.Show("保存诊断信息失败" + diagNose.Err);
                //                return -1;
                //            }
                //        }

                //    }
                //}
                //if (diagAdd != null)
                //{
                //    foreach (FS.HISFC.Models.HealthRecord.Diagnose obj in diagAdd)
                //    {
                //        if (diagNose.InsertDiagnose(obj) < 1)
                //        {
                //            FS.FrameWork.Management.PublicTrans.RollBack();
                //            MessageBox.Show("保存诊断信息失败" + diagNose.Err);
                //            return -1;
                //        }

                //    }
                //}
                ////暂时保存插入和修改过的数据
                //ArrayList tempDiag = diagNose.QueryCaseDiagnose(CaseBase.PatientInfo.ID, "%", frmType);

                //modify chengym 2011-9-27 
                List<FS.HISFC.Models.HealthRecord.Diagnose> diagList = new List<FS.HISFC.Models.HealthRecord.Diagnose>();
                this.ucDiagNoseInput1.deleteRow();

                this.ucDiagNoseInput1.GetDiagnosInfo(diagList);
                if (this.ucDiagNoseInput1.ValueStateNew(diagList) == -1)
                {
                    this.tab1.SelectedIndex = 1;
                    FS.FrameWork.Management.PublicTrans.RollBack(); //数据校验失败
                    return -3;
                }
                if (diagList != null)
                {
                    diagNose.DeleteDiagnoseAll(CaseBase.PatientInfo.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC,FS.HISFC.Models.Base.ServiceTypes.I);
                    foreach (FS.HISFC.Models.HealthRecord.Diagnose obj in diagList)
                    {
                        if (diagNose.InsertDiagnose(obj) < 1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("保存诊断信息失败" + diagNose.Err);
                            return -1;
                        }
                    }
                }
                //暂时保存插入和修改过的数据
                ArrayList tempDiag = diagNose.QueryCaseDiagnose(CaseBase.PatientInfo.ID, "%", frmType,FS.HISFC.Models.Base.ServiceTypes.I);
                #endregion
                #region  手术信息
                //FS.HISFC.BizLogic.HealthRecord.Operation operation = new FS.HISFC.BizLogic.HealthRecord.Operation();
                //operation.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                ////删除的
                //ArrayList operDel = new ArrayList();
                ////增加的 
                //ArrayList operAdd = new ArrayList();
                ////修改过的 
                //ArrayList operMod = new ArrayList();
                ////删除空白行
                //this.ucOperation1.deleteRow();
                //this.ucOperation1.GetList("D", operDel);
                //this.ucOperation1.GetList("A", operAdd);
                //this.ucOperation1.GetList("M", operMod);

                //if (this.ucOperation1.ValueState(operDel) == -1 || this.ucOperation1.ValueState(operAdd) == -1 || this.ucOperation1.ValueState(operMod) == -1)
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack(); //数据校验失败
                //    return -3;
                //}
                //if (operDel != null)
                //{
                //    foreach (FS.HISFC.Models.HealthRecord.OperationDetail obj in operDel)
                //    {
                //        if (operation.delete(frmType, obj) < 1)
                //        {
                //            FS.FrameWork.Management.PublicTrans.RollBack();
                //            MessageBox.Show("保存手术诊断信息失败" + operation.Err);
                //            return -1;
                //        }
                //    }
                //}
                //if (operAdd != null)
                //{
                //    foreach (FS.HISFC.Models.HealthRecord.OperationDetail obj in operAdd)
                //    {
                //        obj.OperDate = dt;
                //        if (operation.Insert(frmType, obj) < 1)
                //        {
                //            FS.FrameWork.Management.PublicTrans.RollBack();
                //            MessageBox.Show("保存手术诊断信息失败" + operation.Err);
                //            return -1;
                //        }
                //    }
                //}
                //if (operMod != null)
                //{
                //    foreach (FS.HISFC.Models.HealthRecord.OperationDetail obj in operMod)
                //    {
                //        if (operation.Update(frmType, obj) < 1)
                //        {
                //            FS.FrameWork.Management.PublicTrans.RollBack();
                //            MessageBox.Show("保存手术诊断信息失败" + operation.Err);
                //            return -1;
                //        }
                //    }
                //}
                //ArrayList tempOperation = operation.QueryOperation(this.frmType, CaseBase.PatientInfo.ID);

                FS.HISFC.BizLogic.HealthRecord.Operation operation = new FS.HISFC.BizLogic.HealthRecord.Operation();
                operation.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                ArrayList operationList = new ArrayList();
                this.ucOperation1.deleteRow();
                ucOperation1.GetOperationList(operationList);
                if (this.ucOperation1.ValueState(operationList) == -1)
                {
                    this.tab1.SelectedIndex = 2;
                    FS.FrameWork.Management.PublicTrans.RollBack(); //数据校验失败
                    return -3;
                }
                if (operationList != null)
                {
                    operation.deleteAll(CaseBase.PatientInfo.ID);

                    foreach (FS.HISFC.Models.HealthRecord.OperationDetail obj in operationList)
                    {
                        if (operation.Insert(frmType, obj) < 1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("保存手术诊断信息失败" + operation.Err);
                            return -1;
                        }
                    }
                }
                ArrayList tempOperation = operation.QueryOperation(this.frmType, CaseBase.PatientInfo.ID);

                #endregion
                #region 妇婴信息
                FS.HISFC.BizLogic.HealthRecord.Baby baby = new FS.HISFC.BizLogic.HealthRecord.Baby();
                baby.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                //删除的
                ArrayList babyDel = new ArrayList();
                //增加的 
                ArrayList babyAdd = new ArrayList();
                //修改过的 
                ArrayList babyMod = new ArrayList();
                //删除空白行
                this.ucBabyCardInput1.deleteRow();
                this.ucBabyCardInput1.GetList("D", babyDel);
                this.ucBabyCardInput1.GetList("A", babyAdd);
                this.ucBabyCardInput1.GetList("M", babyMod);
                if (this.ucBabyCardInput1.ValueState(babyDel) == -1 || this.ucBabyCardInput1.ValueState(babyAdd) == -1 || this.ucBabyCardInput1.ValueState(babyMod) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack(); //数据校验失败
                    return -3;
                }
                if (babyDel != null)
                {
                    foreach (FS.HISFC.Models.HealthRecord.Baby obj in babyDel)
                    {
                        if (baby.Delete(obj) < 1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("保存妇婴信息失败" + baby.Err);
                            return -1;
                        }
                    }
                }
                if (babyAdd != null)
                {
                    foreach (FS.HISFC.Models.HealthRecord.Baby obj in babyAdd)
                    {
                        if (baby.Insert(obj) < 1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("保存妇婴信息失败" + baby.Err);
                            return -1;
                        }
                    }

                }
                if (babyMod != null)
                {
                    foreach (FS.HISFC.Models.HealthRecord.Baby obj in babyMod)
                    {
                        if (baby.Update(obj) < 1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("保存妇婴信息失败" + baby.Err);
                            return -1;
                        }
                    }
                }
                //暂时存储保存过的信息
                ArrayList tempBaby = baby.QueryBabyByInpatientNo(CaseBase.PatientInfo.ID);
                #endregion
                #region  肿瘤信息
                FS.HISFC.BizLogic.HealthRecord.Tumour tumour = new FS.HISFC.BizLogic.HealthRecord.Tumour();
                tumour.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                FS.HISFC.Models.HealthRecord.Tumour TumInfo = this.ucTumourCard2.GetTumourInfo();
                int m = this.ucTumourCard2.ValueTumourSate(TumInfo);
                if (m == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return -3;
                }
                else if (m == 2) //有数据需要保存 
                {
                    if (tumour.UpdateTumour(TumInfo) < 1)
                    {
                        if (tumour.InsertTumour(TumInfo) < 1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(tumour.Err);
                            return -3;
                        }
                    }
                }
                //删除的
                ArrayList tumDel = new ArrayList();
                //增加的 
                ArrayList tumAdd = new ArrayList();
                //修改过的 
                ArrayList tumMod = new ArrayList();
                //删除空白行
                this.ucTumourCard2.deleteRow();
                this.ucTumourCard2.GetList("D", tumDel);
                this.ucTumourCard2.GetList("A", tumAdd);
                this.ucTumourCard2.GetList("M", tumMod);
                if (this.ucTumourCard2.ValueState(tumDel) == -1 || this.ucTumourCard2.ValueState(tumAdd) == -1 || this.ucTumourCard2.ValueState(tumMod) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();//回退
                    return -3;
                }
                if (tumDel != null)
                {
                    foreach (FS.HISFC.Models.HealthRecord.TumourDetail obj in tumDel)
                    {
                        if (tumour.DeleteTumourDetail(obj) < 1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("保存肿瘤信息失败" + tumour.Err);
                            return -1;
                        }
                    }
                }
                if (tumAdd != null)
                {
                    foreach (FS.HISFC.Models.HealthRecord.TumourDetail obj in tumAdd)
                    {
                        if (tumour.InsertTumourDetail(obj) < 1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("保存肿瘤信息失败" + tumour.Err);
                            return -1;
                        }
                    }
                }
                if (tumMod != null)
                {
                    foreach (FS.HISFC.Models.HealthRecord.TumourDetail obj in tumMod)
                    {
                        if (tumour.UpdateTumourDetail(obj) < 1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("保存肿瘤信息失败" + tumour.Err);
                            return -1;
                        }
                    }
                }
                //查询保存的信息
                ArrayList tempTumour = tumour.QueryTumourDetail(CaseBase.PatientInfo.ID);

                #endregion
                #region  费用信息
                ArrayList feeList = this.ucFeeInfo1.GetFeeInfoList();
                if (this.ucFeeInfo1.ValueState(feeList) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();//回退
                    return -3;
                }
                if (feeList != null)
                {
                    foreach (FS.HISFC.Models.RADT.Patient obj in feeList)
                    {
                        obj.ID = this.CaseBase.PatientInfo.ID; //住院流水号
                        obj.User01 = this.CaseBase.PatientInfo.PVisit.OutTime.ToString(); //出院日期
                        if (healthRecordFee.UpdateFeeInfo(obj) < 1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("保存费用信息失败" + baseDml.Err);
                            return -1;
                        }
                    }
                }
                #endregion

                #region  保存成功

                //根据目前病案标志 修改住院主表的病案信息
                if (this.frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC)
                {
                    //医生站录入病案
                    if (baseDml.UpdateMainInfoCaseFlag(CaseBase.PatientInfo.ID, CaseBase.PatientInfo.CaseState) < 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更新主表失败" + baseDml.Err);
                        return -1;
                    }
                    //CaseBase.PatientInfo.CaseState = "2";
                }
                else if (this.frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS && CaseBase.IsHandCraft != "1") //病案室录入病案
                {
                    if (baseDml.UpdateMainInfoCaseFlag(CaseBase.PatientInfo.ID, CaseBase.PatientInfo.CaseState) < 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更新主表 case_flag 失败" + baseDml.Err);
                        return -1;
                    }
                    if (baseDml.UpdateMainInfoCaseSendFlag(CaseBase.PatientInfo.ID, CaseBase.PatientInfo.CaseState) < 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更新主表casesend_flag 失败" + baseDml.Err);
                        return -1;
                    }
                    //CaseBase.PatientInfo.CaseState = "3";
                }
                //保存fpPoint的更改。
                this.ucBabyCardInput1.fpEnterSaveChanges(tempBaby);
                this.ucChangeDept1.fpEnterSaveChanges(tempChangeDept);
                //LoadChangeDept(tempChangeDept);
                this.ucDiagNoseInput1.fpEnterSaveChanges(tempDiag);
                this.ucOperation1.fpEnterSaveChanges(tempOperation);
                this.ucTumourCard2.fpEnterSaveChanges(tempTumour);
                //				RuDiag = 0;  //门诊诊断标志
                //				menDiag = 0; //入院诊断标志
                //费用信息
                //trans.Commit();

                #region 后续工作
                //更新病案基本表中 门诊诊断，入院诊断，出院诊断 ，手术 （第一诊断 或手术）
                //if (baseDml.UpdateBaseDiagAndOperation(CaseBase.PatientInfo.ID, frmType) == -1)
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack();
                //    MessageBox.Show("更新病案主表诊断手术信息失败.");
                //    return -1;
                //}
                if (baseDml.UpdateBaseDiagAndOperationNew(CaseBase.PatientInfo.ID, CaseBase.ClinicDiag.ID, CaseBase.ClinicDiag.Name, CaseBase.InHospitalDiag.Name, frmType) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("更新病案主表诊断手术信息失败.");
                    return -1;
                }
                //直接在手术信息保存时赋值了chengym
                //localObj.User01 = CaseBase.PatientInfo.PVisit.OutTime.ToString(); //出院日期
                //localObj.User02 = CaseBase.PatientInfo.PVisit.PatientLocation.ID; //出院科室 
                //if (baseDml.DiagnoseAndOperation(localObj, CaseBase.PatientInfo.ID) == -1)
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack();
                //    MessageBox.Show("更新病案主表诊断手术信息失败.");
                //    return -1;
                //}

                #region  更新主表内容 chengym 2011-9-28

                if (isUpdataFinIprInmaininfo)
                {
                    //radtMana.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    Function fun=new Function();
                    if (patientInfoForUpdate != null)
                    {
                        if (fun.ReturnStringValue(CaseBase.PatientInfo.Name, false) != string.Empty)
                        {
                            patientInfoForUpdate.Name = CaseBase.PatientInfo.Name;//姓名
                        }
                        if (CaseBase.PatientInfo.Sex.ID != null && fun.ReturnStringValue(CaseBase.PatientInfo.Sex.ID.ToString(), false) != string.Empty)//性别
                        {
                            patientInfoForUpdate.Sex.ID = CaseBase.PatientInfo.Sex.ID;
                        }
                        if (fun.ReturnStringValue(CaseBase.PatientInfo.IDCard, false) != string.Empty)
                        {
                            patientInfoForUpdate.IDCard = CaseBase.PatientInfo.IDCard; //身份证号
                        }

                        patientInfoForUpdate.Birthday = CaseBase.PatientInfo.Birthday; // 生日
                        if (fun.ReturnStringValue(CaseBase.PatientInfo.Profession.ID, false) != string.Empty)
                        {
                            patientInfoForUpdate.Profession.ID = CaseBase.PatientInfo.Profession.ID; //职业
                        }
                        if (fun.ReturnStringValue(CaseBase.PatientInfo.AddressBusiness, false) != string.Empty)
                        {
                            patientInfoForUpdate.CompanyName = CaseBase.PatientInfo.AddressBusiness; //工作单位
                        }
                        if (fun.ReturnStringValue(CaseBase.PatientInfo.PhoneBusiness, false) != string.Empty)
                        {
                            patientInfoForUpdate.PhoneBusiness = CaseBase.PatientInfo.PhoneBusiness; //单位电话
                        }
                        if (fun.ReturnStringValue(CaseBase.PatientInfo.BusinessZip, false) != string.Empty)
                        {
                            patientInfoForUpdate.BusinessZip = CaseBase.PatientInfo.BusinessZip; //单位邮编
                        }
                        if (fun.ReturnStringValue(CaseBase.PatientInfo.AddressHome, false) != string.Empty)
                        {
                            patientInfoForUpdate.AddressHome = CaseBase.PatientInfo.AddressHome; //户口或家庭所在
                        }
                        if (fun.ReturnStringValue(CaseBase.PatientInfo.PhoneHome, false) != string.Empty)
                        {
                            patientInfoForUpdate.PhoneHome = CaseBase.PatientInfo.PhoneHome; //家庭电话
                        }
                        if (fun.ReturnStringValue(CaseBase.PatientInfo.HomeZip, false) != string.Empty)
                        {
                            patientInfoForUpdate.HomeZip = CaseBase.PatientInfo.HomeZip;//户口或家庭邮政编码
                        }
                        if (fun.ReturnStringValue(CaseBase.PatientInfo.DIST, false) != string.Empty)
                        {
                            patientInfoForUpdate.DIST = CaseBase.PatientInfo.DIST; //籍贯
                        }
                        if (fun.ReturnStringValue(CaseBase.PatientInfo.Nationality.ID, false) != string.Empty)
                        {
                            patientInfoForUpdate.Nationality.ID = CaseBase.PatientInfo.Nationality.ID; //民族
                        }
                        if (fun.ReturnStringValue(CaseBase.PatientInfo.Kin.Name, false) != string.Empty)
                        {
                            patientInfoForUpdate.Kin.Name = CaseBase.PatientInfo.Kin.Name; //联系人姓名
                        }
                        if (fun.ReturnStringValue(CaseBase.PatientInfo.Kin.RelationPhone, false) != string.Empty)
                        {
                            patientInfoForUpdate.Kin.RelationPhone = CaseBase.PatientInfo.Kin.RelationPhone; //联系人电话
                        }
                        if (fun.ReturnStringValue(CaseBase.PatientInfo.Kin.RelationAddress, false) != string.Empty)
                        {
                            patientInfoForUpdate.Kin.RelationAddress = CaseBase.PatientInfo.Kin.RelationAddress; //联系人住址
                        }
                        if (fun.ReturnStringValue(CaseBase.PatientInfo.Kin.RelationLink, false) != string.Empty)
                        {
                            patientInfoForUpdate.Kin.Relation.ID = CaseBase.PatientInfo.Kin.RelationLink; //联系人关系
                        }
                        if (CaseBase.PatientInfo.MaritalStatus.ID !=null && fun.ReturnStringValue(CaseBase.PatientInfo.MaritalStatus.ID.ToString(), false) != string.Empty)
                        {
                            patientInfoForUpdate.MaritalStatus.ID = CaseBase.PatientInfo.MaritalStatus.ID.ToString(); //婚姻状况
                        }
                        if (fun.ReturnStringValue(CaseBase.PatientInfo.Country.ID, false) != string.Empty)
                        {
                            patientInfoForUpdate.Country.ID = CaseBase.PatientInfo.Country.ID; //国籍
                        }
                        if (CaseBase.PatientInfo.BloodType.ID!=null && fun.ReturnStringValue(CaseBase.PatientInfo.BloodType.ID.ToString(), false) != string.Empty)
                        {
                            patientInfoForUpdate.BloodType.ID = CaseBase.PatientInfo.BloodType.ID.ToString(); //血型
                        }
                        if (CaseBase.FirstAnaphyPharmacy.Name == "")
                        {
                            patientInfoForUpdate.Disease.IsAlleray = false; //药物过敏
                        }
                        else
                        {
                            patientInfoForUpdate.Disease.IsAlleray = true; //药物过敏
                        }

                        if (baseDml.UpdatePatientInfo(patientInfoForUpdate) < 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();

                            MessageBox.Show("更新com_patientinfo出错!" );

                            return -1;
                        }

                        if (baseDml.UpdatePatient(patientInfoForUpdate) < 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();

                            MessageBox.Show("更新fin_ipr_inmaininfo出错!" );

                            return -1;
                        }
                    }
                }
                #endregion
                FS.FrameWork.Management.PublicTrans.Commit();
                this.tab1.SelectedIndex = 0;
                #endregion
                //手工录入病案标志置成默认标志 
                this.HandCraft = 0;
                #endregion
                MessageBox.Show("保存成功");
                //this.ClearInfo();
            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(ex.Message);
                return -1;
            }
            return 1;
        }

        #endregion

        #region 选择TAB页
        private void tab1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            switch (this.tab1.SelectedTab.Name)
            {
                //case "肿瘤信息":
                case "tabPage7":
                    //如果小于零 ，增加一行
                    if (this.ucTumourCard2.GetfpSpreadRowCount() == 0)
                    {
                        this.ucTumourCard2.AddRow();
                        this.ucTumourCard2.SetActiveCells();
                    }
                    break;
                // case "手术信息":
                case "tabPage6":
                    //如果小于零 ，增加一行
                    if (this.ucOperation1.GetfpSpread1RowCount() == 0)
                    {
                        this.ucOperation1.AddRow();
                        this.ucOperation1.SetActiveCells();
                    }
                    break;
                // case "费用信息":
                case "tabPage4":
                    break;
                // case "诊断信息":
                case "tabPage5":
                    if (this.ucDiagNoseInput1.GetfpSpreadRowCount() == 0)
                    {
                        this.ucDiagNoseInput1.AddRow();
                        this.ucDiagNoseInput1.SetActiveCells();
                    }
                    break;
                //case "妇婴信息":
                case "tabPage2":
                    if (this.ucBabyCardInput1.GetfpSpreadRowCount() == 0)
                    {
                        this.ucBabyCardInput1.AddRow();
                        this.ucBabyCardInput1.SetActiveCells();
                    }
                    break;
                // case "转科信息":
                case "tabPage3":
                    if (this.ucChangeDept1.GetfpSpreadRowCount() == 0)
                    {
                        this.ucChangeDept1.AddRow();
                        this.ucChangeDept1.SetActiveCells();
                    }
                    break;
            }
        }
        #endregion

        #region  将数据显示到界面上

        #region 加载基本信息
        /// <summary>
        /// 将数据显示到界面上
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private int ConvertInfoToPanel(FS.HISFC.Models.HealthRecord.Base info)
        {
            try
            {
                #region  入院科室，出院科室
                if (CaseBase.PatientInfo.CaseState == "1")
                {
                    FS.HISFC.Models.RADT.Location indept = baseDml.GetDeptIn(CaseBase.PatientInfo.ID);
                    //FS.HISFC.Models.RADT.Location outdept = baseDml.GetDeptOut(CaseBase.PatientInfo.ID);
                    if (indept != null) //入院科室 
                    {
                        CaseBase.InDept.ID = indept.Dept.ID;
                        CaseBase.InDept.Name = indept.Dept.Name;
                        //入院科室代码
                        txtDeptInHospital.Tag = indept.Dept.ID;
                        //入院科室名称
                        //txtDeptInHospital.Text = indept.Dept.Name;

                    }
                    else
                    {
                        //入院科室代码
                        txtDeptInHospital.Tag = info.PatientInfo.PVisit.PatientLocation.Dept.ID;
                    }
                    //出院科室
                    CaseBase.OutDept.ID = info.PatientInfo.PVisit.PatientLocation.Dept.ID;
                    CaseBase.OutDept.Name = info.PatientInfo.PVisit.PatientLocation.Dept.Name;
                    ////出院科室代码
                    //txtDeptOut.Tag = info.PatientInfo.PVisit.PatientLocation.Dept.ID;
                }
                else
                {
                    //入院科室代码
                    txtDeptInHospital.Tag = info.InDept.ID;
                    ////出院科室代码
                    //txtDeptOut.Tag = info.OutDept.ID;
                }
                //出院科室代码
                txtDeptOut.Tag = this.dept_out;
                #endregion

                //住院号  病历号
                if (info.CaseNO == "" || info.CaseNO == null)
                {
                    txtCaseNum.Text = info.PatientInfo.PID.PatientNO;
                }
                else
                {
                    txtCaseNum.Text = info.CaseNO;
                }
                //就诊卡号  门诊号
                txtClinicNo.Text = info.PatientInfo.PID.CardNO;
                //姓名
                this.txtPatientName.Text = info.PatientInfo.Name;
                //曾用名
                txtNomen.Text = info.Nomen;
                //性别
                if (info.PatientInfo.Sex.ID != null)
                {
                    txtPatientSex.Tag = info.PatientInfo.Sex.ID.ToString();
                }
                //生日
                if (info.PatientInfo.Birthday != System.DateTime.MinValue)
                {
                    dtPatientBirthday.Value = info.PatientInfo.Birthday;
                }
                else
                {
                    dtPatientBirthday.Value = System.DateTime.Now;
                }
                //为保证年龄跟电子病历一致，直接使用电子病历的年龄计算函数fun_get_age_new
                this.txtPatientAge.Text = this.baseDml.GetAgeByFun(dtPatientBirthday.Value.Date, info.PatientInfo.PVisit.InTime.Date);
                //国籍 编码
                txtCountry.Tag = info.PatientInfo.Country.ID;
                //民族 
                txtNationality.Tag = info.PatientInfo.Nationality.ID;
                //职业
                txtProfession.Tag = info.PatientInfo.Profession.ID;
                //血型编码
                if (info.PatientInfo.BloodType.ID != null)
                {
                    txtBloodType.Tag = info.PatientInfo.BloodType.ID.ToString();
                }
                //婚姻
                if (info.PatientInfo.MaritalStatus.ID != null)
                {
                    txtMaritalStatus.Tag = info.PatientInfo.MaritalStatus.ID;
                }
                //身份证号
                txtIDNo.Text = info.PatientInfo.IDCard;
                //结算类别号
                txtPactKind.Tag = info.PatientInfo.Pact.ID;
                //医保公费号
                txtSSN.Text = info.PatientInfo.SSN;
                //籍贯
                txtDIST.Text = info.PatientInfo.DIST;
                //出生地
                txtAreaCode.Tag = info.PatientInfo.AreaCode;
                txtAreaCode.Text = info.PatientInfo.AreaCode;
                //if (txtAreaCode.Text == "")
                //{
                //    txtAreaCode.Text = info.PatientInfo.AreaCode;
                //}
                //家庭住址
                txtAddressHome.Text = info.PatientInfo.AddressHome;
                //家庭电话
                txtPhoneHome.Text = info.PatientInfo.PhoneHome;
                //住址邮编
                if (info.PatientInfo.CaseState == "1")
                {
                    txtHomeZip.Text = info.PatientInfo.User02;
                }
                else
                {
                    txtHomeZip.Text = info.PatientInfo.HomeZip;
                }
                //单位地址
                if (info.PatientInfo.CaseState == "1")
                {
                    txtAddressBusiness.Text = info.PatientInfo.CompanyName;
                }
                else
                {
                    txtAddressBusiness.Text = info.PatientInfo.AddressBusiness;
                }
                //单位电话
                txtPhoneBusiness.Text = info.PatientInfo.PhoneBusiness;
                //单位邮编
                if (info.PatientInfo.CaseState == "1")
                {
                    txtBusinessZip.Text = info.PatientInfo.User01;
                }
                else
                {
                    txtBusinessZip.Text = info.PatientInfo.BusinessZip;
                }
                //联系人名称
                txtKin.Text = info.PatientInfo.Kin.Name;
                txtKin.Tag = info.PatientInfo.Kin.ID;
                //与患者关系
                txtRelation.Tag = info.PatientInfo.Kin.RelationLink;
                //联系电话
                if (info.PatientInfo.CaseState == "1")
                {
                    txtLinkmanTel.Text = info.PatientInfo.Kin.RelationPhone;
                }
                else
                {
                    txtLinkmanTel.Text = info.PatientInfo.Kin.RelationPhone;
                }
                //联系地址
                
                txtLinkmanAdd.Text = info.PatientInfo.Kin.RelationAddress;

                //门诊诊断医生 ID
                txtClinicDocd.Tag = info.ClinicDoc.ID;
                //门诊诊断医生姓名
                //ClinicDocd.Text = info.ClinicDoc.Name;
                //转来医院
                txtComeFrom.Text = info.ComeFrom;
                //入院日期
                //{701EECD4-5ADF-4323-9FC4-73881FB1632D}
                if (this.isSetInDate)
                {
                    this.dtArrive = radtIntegrate.GetArriveDate(info.PatientInfo.ID);//获取接诊时间
                    if (this.dtArrive == System.DateTime.MinValue)
                    {                        
                        if (info.PatientInfo.PVisit.InTime != System.DateTime.MinValue)
                        {
                            dtDateIn.Value = info.PatientInfo.PVisit.InTime;
                            this.dtArrive = info.PatientInfo.PVisit.InTime;//入院时间不是最小值，接诊时间等于入院时间
                        }
                        else
                        {
                            dtDateIn.Value = System.DateTime.Now;
                            this.dtArrive = System.DateTime.Now;//入院时间是最小值，接诊时间等于当前时间
                        }
                    }
                    else
                    {
                        dtDateIn.Value = this.dtArrive;
                    }
                }
                else
                {
                    if (info.PatientInfo.PVisit.InTime != System.DateTime.MinValue)
                    {
                        dtDateIn.Value = info.PatientInfo.PVisit.InTime;
                    }
                    else
                    {
                        dtDateIn.Value = System.DateTime.Now;
                    }
                }

                //---------------------or-------------------------
                //FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                
                //int DtInDept = ctrlParamIntegrate.GetControlParam<int>("CASE03",true,0);
                //if (DtInDept == 1)
                //{
                //    FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
                //    DateTime dtArrive = radtIntegrate.GetArriveDate(info.PatientInfo.ID);
                //    if (dtArrive == System.DateTime.MinValue)
                //    {
                //        dtDateIn.Value = info.PatientInfo.PVisit.InTime;
                //    }
                //    else
                //    {
                //        dtDateIn.Value = dtArrive;
                //    }
                //}
                //else
                //{
                //    if (info.PatientInfo.PVisit.InTime != System.DateTime.MinValue)
                //    {
                //        dtDateIn.Value = info.PatientInfo.PVisit.InTime;
                //    }
                //    else
                //    {
                //        dtDateIn.Value = System.DateTime.Now;
                //    }
                //}
                if (info.PatientInfo.CaseState == "1")
                {
                    //院感次数 
                    txtInfectNum.Text = Convert.ToString(this.ucDiagNoseInput1.GetInfectionNum());
                }
                else
                {
                    //院感次数 
                    txtInfectNum.Text = info.InfectionNum.ToString();
                }
                //住院次数
                txtInTimes.Text = info.PatientInfo.InTimes.ToString();
                //入院来源

                txtInAvenue.Tag = info.PatientInfo.PVisit.InSource.ID;

                //入院状态                  
                txtCircs.Tag = info.PatientInfo.PVisit.Circs.ID;
                //确诊日期
                if (this.isSetInDate)
                {
                    //{701EECD4-5ADF-4323-9FC4-73881FB1632D}
                    txtDiagDate.Value = this.dtArrive;
                }
                else
                {
                    if (info.DiagDate != System.DateTime.MinValue)
                    {
                        txtDiagDate.Value = info.DiagDate;
                    }
                    else
                    {
                        txtDiagDate.Value = info.PatientInfo.PVisit.InTime;
                    }
                }
                //手术日期
                //			info.OperationDate 
                //出院日期
                if (info.PatientInfo.PVisit.OutTime != System.DateTime.MinValue)
                {
                    txtDateOut.Value = info.PatientInfo.PVisit.OutTime;
                }
                else
                {
                    txtDateOut.Value = System.DateTime.Now;
                }

                if (this.in_State != "I")//出院编辑时再取一次出院日期保证不错
                {
                    this.txtDateOut.Value = this.dt_out;
                }
                //转归代码
                //			info.PatientInfo.PVisit.Zg.ID 
                //确诊天数
                //			info.DiagDays
                //住院天数
                txtPiDays.Text = info.InHospitalDays.ToString();
                if (this.in_State != "I")
                {
                    System.TimeSpan tt = this.dt_out.Date - this.dtDateIn.Value.Date;
                    if (tt.Days == 0)
                    {
                        this.txtPiDays.Text = "1";
                    }
                    else
                    {
                        this.txtPiDays.Text = Convert.ToString(tt.Days);
                    }
                }
                //死亡日期
                //			info.DeadDate = 
                //死亡原因
                //			info.DeadReason
                //尸检
                if (info.CadaverCheck == "1")
                {
                    cbBodyCheck.Checked = true;
                }
                //死亡种类
                //			info.DeadKind 
                //尸体解剖号
                //			info.BodyAnotomize
                //乙肝表面抗原
                txtHbsag.Tag = info.Hbsag;
                //丙肝病毒抗体
                txtHcvAb.Tag = info.HcvAb;
                //获得性人类免疫缺陷病毒抗体
                txtHivAb.Tag = info.HivAb;
                //门急_出院符合
                txtCePi.Tag = info.CePi;
                //入出_院符合
                txtPiPo.Tag = info.PiPo;
                //术前_后符合
                txtOpbOpa.Tag = info.OpbOpa;
                //临床_CT符合
                //临床_MRI符合
                //临床_病理符合
                txtClPa.Tag = info.ClPa;
                //放射_病理符合
                txtFsBl.Tag = info.FsBl;

                //抢救次数
                txtSalvTimes.Text = info.SalvTimes.ToString();
                //成功次数
                txtSuccTimes.Text = info.SuccTimes.ToString();
                //示教科研
                if (info.TechSerc == "1")
                {
                    cbTechSerc.Checked = true;
                }
                //是否随诊
                if (info.VisiStat == "1")
                {
                    cbVisiStat.Checked = true;
                }
                //随访期限 周
                if (info.VisiPeriodWeek == "")
                {
                    txtVisiPeriWeek.Text = "0";
                }
                else
                {
                    txtVisiPeriWeek.Text = info.VisiPeriodWeek;
                }
                //随访期限 月
                if (info.VisiPeriodMonth == "")
                {
                    txtVisiPeriMonth.Text = "0";
                }
                else
                {
                    txtVisiPeriMonth.Text = info.VisiPeriodMonth;
                }
                //随访期限 年
                if (info.VisiPeriodYear == "")
                {
                    txtVisiPeriYear.Text = "0";
                }
                else
                {
                    txtVisiPeriYear.Text = info.VisiPeriodYear;
                }
                //院际会诊次数
                txtInconNum.Text = info.InconNum.ToString();
                //远程会诊
                txtOutconNum.Text = info.OutconNum.ToString();
                //药物过敏
                //			info.AnaphyFlag 
                //过敏药物1
                //this.txtPharmacyAllergic1.Tag = info.FirstAnaphyPharmacy.ID;
                if (info.FirstAnaphyPharmacy.ID != null && info.FirstAnaphyPharmacy.ID.ToString() != "")
                {
                    this.neuTxtPharmacyAllergic1.Text = info.FirstAnaphyPharmacy.ID;
                }
                else
                {
                    this.PharmacyAllergicLoadInfo(info.PatientInfo.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
                }
                ////过敏药物2
                //this.neuTxtPharmacyAllergic2.Text = info.SecondAnaphyPharmacy.ID;
                //this.txtPharmacyAllergic2.Tag = info.SecondAnaphyPharmacy.ID;
                //感染部位
                this.txtInfectionPositionNew.Text = info.InfectionPosition.Name;
                //更改后出院日期
                //			info.CoutDate
                //住院医师代码
                txtAdmittingDoctor.Tag = info.PatientInfo.PVisit.AdmittingDoctor.ID;
                //住院医师姓名
                //AdmittingDoctor.Text = info.PatientInfo.PVisit.AdmittingDoctor.Name;
                //主治医师代码
                txtAttendingDoctor.Tag = info.PatientInfo.PVisit.AttendingDoctor.ID;
                //AttendingDoctor.Text = info.PatientInfo.PVisit.AttendingDoctor.Name;
                //主任医师代码
                txtConsultingDoctor.Tag = info.PatientInfo.PVisit.ConsultingDoctor.ID;
                //ConsultingDoctor.Text = info.PatientInfo.PVisit.ConsultingDoctor.Name;
                //科主任代码
                //			info.PatientInfo.PVisit.ReferringDoctor.ID
                //科主任代码
                txtDeptChiefDoc.Tag = info.PatientInfo.PVisit.ReferringDoctor.ID;

                //进修医师代码
                txtRefresherDocd.Tag = info.RefresherDoc.ID;
                //RefresherDocd.Text = info.RefresherDoc.Name;
                //研究生实习医师代码
                txtGraDocCode.Tag = info.GraduateDoc.ID;
                //GraDocCode.Text = info.GraduateDoc.Name;
                //实习医师代码
                txtPraDocCode.Tag = info.PatientInfo.PVisit.TempDoctor.ID;
                //PraDocCode.Text = info.GraduateDoc.Name;
                //编码员
                txtCodingCode.Tag = info.CodingOper.ID;
                //CodingCode.Text = info.CodingName;
                //病案质量
                txtMrQual.Tag = info.MrQuality;
                //MrQual.Text = CaseQCHelper.GetName(info.MrQual);
                //合格病案
                //			info.MrElig
                //质控医师代码
                txtQcDocd.Tag = info.QcDoc.ID;
                //QcDocd.Text = info.QcDonm;
                //质控护士代码
                txtQcNucd.Tag = info.QcNurse.ID;
                //QcNucd.Text = info.QcNunm;
                //检查时间
                if (info.CheckDate != System.DateTime.MinValue)
                {
                    txtCheckDate.Value = info.CheckDate;
                }
                else
                {
                    txtCheckDate.Value = System.DateTime.Now;
                }

                if (this.in_State != "I" && info.CheckDate.Date<info.PatientInfo.PVisit.OutTime.Date)
                {
                    txtCheckDate.Value = info.PatientInfo.PVisit.OutTime.AddDays(3);//默认出院日期+3天
                }
                //手术操作治疗检查诊断为本院第一例项目
                if (info.YnFirst == "1")
                {
                    cbYnFirst.Checked = true;
                }
                //Rh血型(阴阳)
                txtRhBlood.Tag = info.RhBlood;
                //输血反应（有无）
                txtReactionBlood.Tag = info.ReactionBlood;
                //传染病报告
                txtInfectionDiseasesReport.Tag = info.InfectionDiseasesReport;
                //四病报告
                txtFourDiseasesReport.Tag = info.FourDiseasesReport;
                //红细胞数
                if (info.BloodRed == "" || info.BloodRed == null)
                {
                    txtBloodRed.Text = "0";
                }
                else
                {
                    txtBloodRed.Text = info.BloodRed;
                }
                //血小板数
                if (info.BloodPlatelet == "" || info.BloodPlatelet == null)
                {
                    txtBloodPlatelet.Text = "0";
                }
                else
                {
                    txtBloodPlatelet.Text = info.BloodPlatelet;
                }
                //血浆数
                if (info.BloodPlasma == "" || info.BloodPlasma == null)
                {
                    txtBodyAnotomize.Text = "0";
                }
                else
                {
                    txtBodyAnotomize.Text = info.BloodPlasma;
                }
                //全血数
                if (info.BloodWhole == "" || info.BloodWhole == null)
                {
                    txtBloodWhole.Text = "0";
                }
                else
                {
                    txtBloodWhole.Text = info.BloodWhole;
                }
                //其他输血数
                if (info.BloodOther == "" || info.BloodOther == null)
                {
                    txtBloodOther.Text = "0";
                }
                else
                {
                    txtBloodOther.Text = info.BloodOther;
                }
                //X光号
                txtXNumb.Text = info.XNum;
                //CT号
                txtCtNumb.Text = info.CtNum;
                //MRI号
                txtMriNumb.Text = info.MriNum;
                //病理号
                txtPathNumb.Text = info.PathNum;
                //B超号
                txtBC.Text = info.DsaNum;
                //ECT号
                txtECTNumb.Text = info.EctNum;
                //PET号
                txtPETNumb.Text = info.PetNum;

                //DSA号
                //			info.DsaNumb
                //PET号
                //			info.PetNumb
                //ECT号
                //			info.EctNumb
                //X线次数
                //			info.XTimes
                //CT次数
                //			info.CtTimes
                //MR次数
                //			info.MrTimes;
                //DSA次数
                //			info.DsaTimes
                //PET次数
                //			info.PetTimes
                //ECT次数
                //			info.EctTimes
                //说明
                //			info.Memo
                //归档条码号
                //			info.BarCode
                //病案借阅状态(O借出 I在架)
                //			info.LendStus
                //病案状态1科室质检2登记保存3整理4病案室质检5无效
                //			info.CaseStus 
                //特级护理时间
                txtSuperNus.Text = info.SuperNus.ToString();
                //I级护理时间
                txtINus.Text = info.INus.ToString();
                //II级护理时间
                txtIINus.Text = info.IINus.ToString();
                //III级护理时间
                txtIIINus.Text = info.IIINus.ToString();
                //重症监护时间
                txtStrictNuss.Text = info.StrictNuss.ToString();
                //特殊护理
                txtSPecalNus.Text = info.SpecalNus.ToString();
                //输入员
                txtInputDoc.Tag = info.OperInfo.ID;
                //InputDoc.Text = DoctorHelper.GetName(info.OperCode);
                //整理员 
                txtCoordinate.Tag = info.PackupMan.ID;
                //textBox33.Text = DoctorHelper.GetName(info.PackupMan.ID);
                //手术编码员 
                this.txtOperationCode.Tag = info.OperationCoding.ID;
                txtBC.Text = info.DsaNum;
                //单病种 
                cbDisease30.Checked = FS.FrameWork.Function.NConvert.ToBoolean(info.Disease30);

                if (this.caseStusHelper == null)
                {
                    this.caseStusHelper = new FS.FrameWork.Public.ObjectHelper(con.GetList("CASESTUS"));
                }

                //病案状态
                txtCaseStus.Text = this.caseStusHelper.GetName(info.PatientInfo.CaseState);

                //损伤中毒原因
                if (info.InjuryOrPoisoningCauseCode != null && info.InjuryOrPoisoningCauseCode!="")
                {
                    txtInjuryOrPoisoningCause.Tag = info.InjuryOrPoisoningCauseCode;
                }
                if (info.InjuryOrPoisoningCause != null)
                {
                    txtInjuryOrPoisoningCause.Text = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(info.InjuryOrPoisoningCause,false);
                }
                else
                {
                    txtInjuryOrPoisoningCause.Text = info.InjuryOrPoisoningCause;
                }
                //病理诊断
                if (info.PathologicalDiagName != null)
                {
                    this.neuTxtPharmacyAllergic2.Tag = info.PathologicalDiagCode;
                    this.neuTxtPharmacyAllergic2.Text = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(info.PathologicalDiagName,false);
                }
                else
                {
                    this.neuTxtPharmacyAllergic2.Tag = info.PathologicalDiagCode;
                    this.neuTxtPharmacyAllergic2.Text = info.PathologicalDiagName;
                }
                //备注说明
                txtRemark.Text = info.PatientInfo.Memo;
                //输液反应
                this.txtReactionTransfuse.Tag = info.ReactionLiquid;
                //转往何医院
                this.txtComeTo.Text = info.OutDept.Memo;

                if (info.ClinicDiag.Name != null)
                {
                    this.txtClinicDiag.Text = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(info.ClinicDiag.Name,false);
                }
                else
                {
                    this.txtClinicDiag.Text = info.ClinicDiag.Name;
                }
                if (info.InHospitalDiag.Name != null)
                {
                    this.txtRuyuanDiagNose.Text = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(info.InHospitalDiag.Name,false);
                }
                else
                {
                    this.txtRuyuanDiagNose.Text = info.InHospitalDiag.Name;
                }

                //获取护理数量
                if (info.PatientInfo.CaseState == "1")
                {
                    this.GetNursingNum(info.PatientInfo.ID);
                }
                else
                {
                    if (this.in_State=="I" || ( info.PatientInfo.CaseState=="2" && info.PatientInfo.PVisit.OutTime.Date < this.dt_out.Date))
                    {
                        this.GetNursingNum(info.PatientInfo.ID);
                    }
                    else if (info.SuperNus.ToString() == "0" && info.INus.ToString() == "0" && info.IIINus.ToString() == "0" && info.SpecalNus.ToString() == "0" && info.StrictNuss.ToString() == "0")
                    {
                        this.GetNursingNum(info.PatientInfo.ID);
                    }
                    else
                    {
                        this.txtSuperNus.Text = info.SuperNus.ToString();//特级护理
                        this.txtINus.Text = info.INus.ToString();//一级护理
                        this.txtIINus.Text = info.IINus.ToString();//二级护理
                        this.txtIIINus.Text = info.IIINus.ToString();//三级护理
                        this.txtSPecalNus.Text = info.SpecalNus.ToString();//特殊护理
                        this.txtStrictNuss.Text = info.StrictNuss.ToString();//重症护理
                    }
                }

                if (this.txtFirstDept.Text == "")//转科时间
                {
                    //{701EECD4-5ADF-4323-9FC4-73881FB1632D}
                    if (this.isSetInDate)
                    {                       
                            this.dtFirstTime.Value = this.dtArrive;
                            this.dtSecond.Value = this.dtArrive;
                            this.dtThird.Value = this.dtArrive;
                    }
                    else
                    {
                        if (info.PatientInfo.PVisit.InTime != System.DateTime.MinValue)
                        {
                            this.dtFirstTime.Value = info.PatientInfo.PVisit.InTime;
                            this.dtSecond.Value = info.PatientInfo.PVisit.InTime;
                            this.dtThird.Value = info.PatientInfo.PVisit.InTime;
                        }
                        else
                        {
                            this.dtFirstTime.Value = System.DateTime.Now;
                            this.dtSecond.Value = System.DateTime.Now;
                            this.dtThird.Value = System.DateTime.Now;
                        }
                    }
                }
                if (this.txtFirstDept.Text != "" && this.txtDeptSecond.Text == "")
                {
                    this.dtSecond.Value = this.dtFirstTime.Value;
                    this.dtThird.Value = this.dtFirstTime.Value;
                }
                if (this.txtDeptSecond.Text != "" && this.txtDeptThird.Text == "")
                {
                    this.dtThird.Value = this.dtSecond.Value;
                }
                FS.HISFC.BizLogic.HealthRecord.DeptShift deptMger = new FS.HISFC.BizLogic.HealthRecord.DeptShift();
                string changeBed = deptMger.QueryWardNoBedNOByInpatienNO(info.PatientInfo.ID, "1");
                string WardNoBedNO = deptMger.QueryWardNoBedNOByInpatienNO(changeBed, "2");
                this.txtInRoom.Text = WardNoBedNO;//入院病室

                this.txtOutRoom.Text = deptMger.QueryWardNoBedNOByInpatienNO(this.bedNo, "2"); //出院病室

                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }
        #endregion

        #region 加载前三次转科信息
        /// <summary>
        /// 加载前三次转科信息
        /// </summary>
        /// <param name="list"></param>
        private void LoadChangeDept(ArrayList list)
        {
            if (list == null)
            {
                return;
            }

            #region 转科信息的前三个在界面上显示
            if (list.Count > 0) //有转科信息
            {
                //转科信息的前三个在界面上显示
                int j = 0;
                if (list.Count >= 3)
                {
                    j = 3;
                }
                else
                {
                    j = list.Count;
                }
                for (int i = 0; i < j; i++)
                {
                    switch (i)
                    {
                        case 0:
                            firDept = (FS.HISFC.Models.RADT.Location)list[0];
                            break;
                        case 1:
                            secDept = (FS.HISFC.Models.RADT.Location)list[1];
                            break;
                        case 2:
                            thirDept = (FS.HISFC.Models.RADT.Location)list[2];
                            break;
                    }
                }
            }
            #endregion

            #region 转科信息
            if (this.firDept != null)
            {
                //firstDept.Text = firDept.Dept.Name;
                txtFirstDept.Tag = firDept.Dept.ID;
                this.dtFirstTime.Value = FS.FrameWork.Function.NConvert.ToDateTime(firDept.Dept.Memo);
            }
            if (this.secDept != null)
            {
                //deptSecond.Text = this.secDept.Dept.Name;
                txtDeptSecond.Tag = this.secDept.Dept.ID;
                this.dtSecond.Value = FS.FrameWork.Function.NConvert.ToDateTime(secDept.Dept.Memo);
            }
            if (this.thirDept != null)
            {
                //deptThird.Text = this.thirDept.Dept.Name;
                txtDeptThird.Tag = this.thirDept.Dept.ID;
                this.dtThird.Value = FS.FrameWork.Function.NConvert.ToDateTime(thirDept.Dept.Memo);
            }
            #endregion
        }
        #endregion

        #endregion

        #region 从控制面板上获取数据
        /// <summary>
        /// 从控制面板上获取数据
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private int GetInfoFromPanel(FS.HISFC.Models.HealthRecord.Base info)
        {
            //System.TimeSpan tt = this.txtDateOut.Value.Date - this.dtDateIn.Value.Date;
            //this.txtPiDays.Text = Convert.ToString(tt.Days);
            //住院流水号
            info.PatientInfo.ID = CaseBase.PatientInfo.ID;
            info.IsHandCraft = CaseBase.IsHandCraft;
            //住院号  病历号
            info.CaseNO = txtCaseNum.Text;
            info.CaseNO = info.CaseNO.PadLeft(10, '0');
            //住院号
            info.PatientInfo.PID.PatientNO = CaseBase.PatientInfo.PID.PatientNO;
            //就诊卡号  门诊号
            info.PatientInfo.PID.CardNO = txtClinicNo.Text;
            //姓名
            info.PatientInfo.Name = txtPatientName.Text;
            //曾用名
            info.Nomen = txtNomen.Text;
            //性别
            if (txtPatientSex.Tag != null)
            {
                info.PatientInfo.Sex.ID = txtPatientSex.Tag;
            }
            else
            {
                info.PatientInfo.Sex.ID = CaseBase.PatientInfo.Sex.ID;
            }
            if (info.PatientInfo.Sex.ID == null)
            {
                info.PatientInfo.Sex.ID = "";
            }
            //生日
            info.PatientInfo.Birthday = dtPatientBirthday.Value;
            //国籍
            if (txtCountry.Tag != null)
            {
                info.PatientInfo.Country.ID = txtCountry.Tag.ToString();
            }
            else
            {
                info.PatientInfo.Country.ID = "";
            }
            //民族 
            if (txtNationality.Tag != null)
            {
                info.PatientInfo.Nationality.ID = txtNationality.Tag.ToString();
            }
            else
            {
                info.PatientInfo.Nationality.ID = "";
            }
            //职业
            if (txtProfession.Tag != null)
            {
                info.PatientInfo.Profession.ID = txtProfession.Tag.ToString();
            }
            else
            {
                info.PatientInfo.Profession.ID = "";
            }
            //血型编码
            info.PatientInfo.BloodType.ID = txtBloodType.Tag;
            //婚姻
            if (txtMaritalStatus.Tag != null)
            {
                info.PatientInfo.MaritalStatus.ID = txtMaritalStatus.Tag;
            }
            else
            {
                info.PatientInfo.MaritalStatus.ID = "";
            }
            //年龄单位　不存年龄和单位
            //info.AgeUnit = cmbUnit.Text;
            //年龄
            //info.PatientInfo.Age = txtPatientAge.Text;
            //if (info.PatientInfo.Age == "")
            //{
            //    info.PatientInfo.Age = "0";
            //}
            info.AgeUnit = txtPatientAge.Text;
            info.PatientInfo.Age = "0";

            //身份证号
            info.PatientInfo.IDCard = txtIDNo.Text;
            //入院途径
            //			if( InSource.Tag != null)
            //			{
            //				info.PatientInfo.PVisit.InSource.ID =  InSource.Tag.ToString();
            //			}
            //结算类别号
            if (txtPactKind.Tag != null)
            {
                FS.HISFC.Models.Base.PactInfo pactInfo = this.pactManager.GetPactUnitInfoByPactCode(this.txtPactKind.Tag.ToString());
                if (pactInfo != null)
                {
                    info.PatientInfo.Pact.PayKind.ID = pactInfo.PayKind.ID;
                }
                else
                {
                    info.PatientInfo.Pact.PayKind.ID = txtPactKind.Tag.ToString();
                }
                info.PatientInfo.Pact.ID = txtPactKind.Tag.ToString();
            }
            else
            {
                info.PatientInfo.Pact.PayKind.ID = "";
                info.PatientInfo.Pact.ID = "";
            }
            //if (txtPactKind.Tag != null)
            //{
            //    info.PatientInfo.Pact.PayKind.ID = txtPactKind.Tag.ToString();
            //    info.PatientInfo.Pact.ID = txtPactKind.Tag.ToString();
            //}
            //else
            //{
            //    info.PatientInfo.Pact.PayKind.ID = "";
            //    info.PatientInfo.Pact.ID = "";
            //}
            //医保公费号
            info.PatientInfo.SSN = txtSSN.Text;
            //籍贯
            info.PatientInfo.DIST = txtDIST.Text;
            //出生地
            info.PatientInfo.AreaCode = txtAreaCode.Text;
            //家庭住址
            info.PatientInfo.AddressHome = txtAddressHome.Text;
            //家庭电话
            info.PatientInfo.PhoneHome = txtPhoneHome.Text;
            //住址邮编
            info.PatientInfo.HomeZip = txtHomeZip.Text;
            //单位地址
            info.PatientInfo.AddressBusiness = txtAddressBusiness.Text;
            //单位电话
            info.PatientInfo.PhoneBusiness = txtPhoneBusiness.Text;
            //单位邮编
            info.PatientInfo.BusinessZip = txtBusinessZip.Text;
            //联系人名称
            info.PatientInfo.Kin.Name = txtKin.Text;
            //与患者关系
            if (txtRelation.Tag != null)
            {
                info.PatientInfo.Kin.RelationLink = txtRelation.Tag.ToString();
            }
            else
            {
                info.PatientInfo.Kin.RelationLink = "";
            }
            //联系电话
            info.PatientInfo.Kin.RelationPhone = txtLinkmanTel.Text;
            //联系地址
            info.PatientInfo.Kin.RelationAddress = txtLinkmanAdd.Text;
            //门诊诊断医生 ID
            if (txtClinicDocd.Tag != null)
            {
                info.ClinicDoc.ID = txtClinicDocd.Tag.ToString();
            }
            else
            {
                info.ClinicDoc.ID = "";
            }
            //门诊诊断医生姓名
            info.ClinicDoc.Name = txtClinicDocd.Text;
            //转来医院
            info.ComeFrom = txtComeFrom.Text;
            //入院日期
            info.PatientInfo.PVisit.InTime = dtDateIn.Value;
            //住院次数
            info.PatientInfo.InTimes = FS.FrameWork.Function.NConvert.ToInt32(txtInTimes.Text);
            //入院科室代码
            if (txtDeptInHospital.Tag != null)
            {
                info.InDept.ID = txtDeptInHospital.Tag.ToString();
            }
            else
            {
                info.InDept.ID = "";
            }
            //入院科室名称
            info.InDept.Name = txtDeptInHospital.Text;
            //入院来源
            if (txtInAvenue.Tag != null)
            {
                info.PatientInfo.PVisit.InSource.ID = txtInAvenue.Tag.ToString();
                info.PatientInfo.PVisit.InSource.Name = txtInAvenue.Text;
            }
            else
            {
                info.PatientInfo.PVisit.InSource.ID = "";
                info.PatientInfo.PVisit.InSource.Name = "";
            }
            //入院状态
            if (txtCircs.Tag != null)
            {
                info.PatientInfo.PVisit.Circs.ID = txtCircs.Tag.ToString();
            }
            else
            {
                info.PatientInfo.PVisit.Circs.ID = "";
            }
            //确诊日期
            info.DiagDate = txtDiagDate.Value;

            this.ucDiagNoseInput1.DiagDate = info.DiagDate;//chengym 诊断获取时用
            //手术日期
            //			info.OperationDate 
            //出院日期
            info.PatientInfo.PVisit.OutTime = txtDateOut.Value;
            //出院科室代码
            if (txtDeptOut.Tag != null)
            {
                info.OutDept.ID = txtDeptOut.Tag.ToString();
            }
            else
            {
                info.OutDept.ID = "";
            }
            //出院科室名称
            info.OutDept.Name = txtDeptOut.Text;
            //转归代码
            //			info.PatientInfo.PVisit.Zg.ID 
            //确诊天数
            //			info.DiagDays
            //住院天数
            info.InHospitalDays = FS.FrameWork.Function.NConvert.ToInt32(txtPiDays.Text);
            //死亡日期
            //			info.DeadDate = 
            //死亡原因
            //			info.DeadReason
            //尸检
            if (cbBodyCheck.Checked)
            {
                info.CadaverCheck = "1";
            }
            else
            {
                info.CadaverCheck = "0";
            }
            //死亡种类
            //			info.DeadKind 
            //尸体解剖号
            //			info.BodyAnotomize
            //乙肝表面抗原
            if (txtHbsag.Tag != null)
            {
                info.Hbsag = txtHbsag.Tag.ToString();
            }
            else
            {
                info.Hbsag = "";
            }
            //丙肝病毒抗体
            if (txtHcvAb.Tag != null)
            {
                info.HcvAb = txtHcvAb.Tag.ToString();
            }
            else
            {
                info.HcvAb = "";
            }
            //获得性人类免疫缺陷病毒抗体
            if (txtHivAb.Tag != null)
            {
                info.HivAb = txtHivAb.Tag.ToString();
            }
            else
            {
                info.HivAb = "";
            }
            //门急_入院符合
            if (txtCePi.Tag != null)
            {
                info.CePi = txtCePi.Tag.ToString();
            }
            else
            {
                info.CePi = "";
            }
            //入出_院符合
            if (txtPiPo.Tag != null)
            {
                info.PiPo = txtPiPo.Tag.ToString();
            }
            else
            {
                info.PiPo = "";
            }
            //术前_后符合
            if (txtOpbOpa.Tag != null)
            {
                info.OpbOpa = txtOpbOpa.Tag.ToString();
            }
            else
            {
                info.OpbOpa = "";
            }
            //临床_病理符合

            //临床_CT符合
            //临床_MRI符合
            //临床_病理符合
            if (txtClPa.Tag != null)
            {
                info.ClPa = txtClPa.Tag.ToString();
            }
            else
            {
                info.ClPa = "";
            }
            //放射_病理符合
            if (txtFsBl.Tag != null)
            {
                info.FsBl = txtFsBl.Tag.ToString();
            }
            else
            {
                info.FsBl = "";
            }
            //抢救次数
            info.SalvTimes = FS.FrameWork.Function.NConvert.ToInt32(txtSalvTimes.Text.Trim());
            //成功次数
            info.SuccTimes = FS.FrameWork.Function.NConvert.ToInt32(txtSuccTimes.Text.Trim());
            //示教科研
            if (cbTechSerc.Checked)
            {
                info.TechSerc = "1";
            }
            else
            {
                info.TechSerc = "0";
            }
            //是否随诊
            if (cbVisiStat.Checked)
            {
                info.VisiStat = "1";
            }
            else
            {
                info.VisiStat = "0";
            }
            //随访期限 周
            info.VisiPeriodWeek = txtVisiPeriWeek.Text;
            //随访期限 月
            info.VisiPeriodMonth = txtVisiPeriMonth.Text;
            //随访期限 年
            info.VisiPeriodYear = txtVisiPeriYear.Text;
            //院际会诊次数
            info.InconNum = FS.FrameWork.Function.NConvert.ToInt32(txtInconNum.Text.Trim());
            //远程会诊
            info.OutconNum = FS.FrameWork.Function.NConvert.ToInt32(txtOutconNum.Text.Trim());
            //住院医师代码
            if (txtAdmittingDoctor.Tag != null)
            {
                info.PatientInfo.PVisit.AdmittingDoctor.ID = txtAdmittingDoctor.Tag.ToString();
                //住院医师姓名
                info.PatientInfo.PVisit.AdmittingDoctor.Name = txtAdmittingDoctor.Text;
            }
            else
            {
                info.PatientInfo.PVisit.AdmittingDoctor.ID = "";
                //住院医师姓名
                info.PatientInfo.PVisit.AdmittingDoctor.Name = "";
            }
            //主治医师代码
            if (txtAttendingDoctor.Tag != null)
            {
                info.PatientInfo.PVisit.AttendingDoctor.ID = txtAttendingDoctor.Tag.ToString();
                info.PatientInfo.PVisit.AttendingDoctor.Name = txtAttendingDoctor.Text;
            }
            else
            {
                info.PatientInfo.PVisit.AttendingDoctor.ID = "";
                info.PatientInfo.PVisit.AttendingDoctor.Name = "";
            }
            //主任医师代码
            if (txtConsultingDoctor.Tag != null)
            {
                info.PatientInfo.PVisit.ConsultingDoctor.ID = txtConsultingDoctor.Tag.ToString();
                info.PatientInfo.PVisit.ConsultingDoctor.Name = txtConsultingDoctor.Text;
            }
            else
            {
                info.PatientInfo.PVisit.ConsultingDoctor.ID = "";
                info.PatientInfo.PVisit.ConsultingDoctor.Name = "";
            }
            //科主任代码
            if (txtDeptChiefDoc.Tag != null)
            {
                info.PatientInfo.PVisit.ReferringDoctor.ID = txtDeptChiefDoc.Tag.ToString();
                info.PatientInfo.PVisit.ReferringDoctor.Name = txtDeptChiefDoc.Text;
            }
            else
            {
                info.PatientInfo.PVisit.ReferringDoctor.ID = "";
                info.PatientInfo.PVisit.ReferringDoctor.Name = "";
            }
            //进修医师代码
            if (txtRefresherDocd.Tag != null)
            {
                info.RefresherDoc.ID = txtRefresherDocd.Tag.ToString();
                info.RefresherDoc.Name = txtRefresherDocd.Text;
            }
            else
            {
                info.RefresherDoc.ID = "";
                info.RefresherDoc.Name = "";
            }
            //研究生实习医师代码
            if (txtGraDocCode.Tag != null)
            {
                info.GraduateDoc.ID = txtGraDocCode.Tag.ToString();
                info.GraduateDoc.Name = txtGraDocCode.Text.Trim();
            }
            else
            {
                info.GraduateDoc.ID = "";
                info.GraduateDoc.Name = "";
            }
            //实习医师代码
            if (txtPraDocCode.Tag != null)
            {
                info.PatientInfo.PVisit.TempDoctor.ID = txtPraDocCode.Tag.ToString();
                info.PatientInfo.PVisit.TempDoctor.Name = txtPraDocCode.Text.Trim();
            }
            else
            {
                info.PatientInfo.PVisit.TempDoctor.ID = "";
                info.PatientInfo.PVisit.TempDoctor.Name = "";
            }
            //编码员
            if (txtCodingCode.Tag != null)
            {
                info.CodingOper.ID = txtCodingCode.Tag.ToString();
                info.CodingOper.Name = txtCodingCode.Text.Trim();
            }
            else
            {
                info.CodingOper.ID = "";
                info.CodingOper.Name = "";
            }
            //病案质量
            if (txtMrQual.Tag != null)
            {
                info.MrQuality = txtMrQual.Tag.ToString();
            }
            else
            {
                info.MrQuality = "";
            }
            //合格病案
            //			info.MrElig
            //质控医师代码
            if (txtQcDocd.Tag != null)
            {
                info.QcDoc.ID = txtQcDocd.Tag.ToString();
                info.QcDoc.Name = txtQcDocd.Text.Trim();
            }
            else
            {
                info.QcDoc.ID = "";
                info.QcDoc.Name = "";
            }
            //质控护士代码
            if (txtQcNucd.Tag != null)
            {
                info.QcNurse.ID = txtQcNucd.Tag.ToString();
                info.QcNurse.Name = txtQcNucd.Text.Trim();
            }
            else
            {
                info.QcNurse.ID = "";
                info.QcNurse.Name = "";
            }
            //检查时间
            info.CheckDate = txtCheckDate.Value;
            //手术操作治疗检查诊断为本院第一例项目
            if (cbYnFirst.Checked)
            {
                info.YnFirst = "1";
            }
            else
            {
                info.YnFirst = "0";
            }
            //Rh血型(阴阳)
            if (txtRhBlood.Tag != null)
            {
                info.RhBlood = txtRhBlood.Tag.ToString();
            }
            else
            {
                info.RhBlood = "";
            }
            //输血反应（有无）
            if (txtReactionBlood.Tag != null)
            {
                info.ReactionBlood = txtReactionBlood.Tag.ToString();
            }
            else
            {
                info.ReactionBlood = "";
            }
            //传染病报告
            if (txtInfectionDiseasesReport.Tag != null)
            {
                info.InfectionDiseasesReport = txtInfectionDiseasesReport.Tag.ToString();
            }
            else
            {
                info.InfectionDiseasesReport = "";
            }
            //四病报告
            if (txtFourDiseasesReport.Tag != null)
            {
                info.FourDiseasesReport = txtFourDiseasesReport.Tag.ToString();
            }
            else
            {
                info.FourDiseasesReport = "";
            }

            //红细胞数
            info.BloodRed = txtBloodRed.Text;
            //血小板数
            info.BloodPlatelet = txtBloodPlatelet.Text;
            //血浆数
            info.BloodPlasma = txtBodyAnotomize.Text;
            //全血数
            info.BloodWhole = txtBloodWhole.Text;
            //其他输血数
            info.BloodOther = txtBloodOther.Text;
            //X光号
            info.XNum = txtXNumb.Text;
            //CT号
            info.CtNum = txtCtNumb.Text;
            //MRI号
            info.MriNum = txtMriNumb.Text;
            // 病理号 
            info.PathNum = txtPathNumb.Text;
            //B超号
            info.DsaNum = txtBC.Text;
            //PET 号
            info.PetNum = txtPETNumb.Text;
            //ECT号
            info.EctNum = txtECTNumb.Text;
            //特级护理时间
            info.SuperNus = FS.FrameWork.Function.NConvert.ToInt32(txtSuperNus.Text);
            //I级护理时间
            info.INus = FS.FrameWork.Function.NConvert.ToInt32(txtINus.Text);
            //II级护理时间
            info.IINus = FS.FrameWork.Function.NConvert.ToInt32(txtIINus.Text);
            //III级护理时间
            info.IIINus = FS.FrameWork.Function.NConvert.ToInt32(txtIIINus.Text);
            //重症监护时间
            info.StrictNuss = FS.FrameWork.Function.NConvert.ToInt32(txtStrictNuss.Text);
            //特殊护理
            info.SpecalNus = FS.FrameWork.Function.NConvert.ToInt32(txtSPecalNus.Text);
            if (txtInputDoc.Tag != null)
            {
                info.OperInfo.ID = txtInputDoc.Tag.ToString();
            }
            else
            {
                info.OperInfo.ID = "";
            }
            //整理员 
            if (txtCoordinate.Tag != null)
            {
                info.PackupMan.ID = txtCoordinate.Tag.ToString();
            }
            else
            {
                info.PackupMan.ID = "";
            }
            if (this.txtOperationCode.Tag != null)
            {
                info.OperationCoding.ID = this.txtOperationCode.Tag.ToString();
            }
            else
            {
                info.OperationCoding.ID = "";
            }
            //单病种 
            if (cbDisease30.Checked)
            {
                info.Disease30 = "1";
            }
            else
            {
                info.Disease30 = "0";
            }
            info.LendStat = "1"; //病案借阅状态 0 为借出 1为在架 
            if (this.frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC && (this.CaseBase.PatientInfo.CaseState == "1" || string.IsNullOrEmpty(this.CaseBase.PatientInfo.CaseState)))
            {
                info.PatientInfo.CaseState = "2";
            }
            else if (this.frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS && (this.CaseBase.PatientInfo.CaseState == "1" || string.IsNullOrEmpty(this.CaseBase.PatientInfo.CaseState))) //病案室录入病案
            {
                info.PatientInfo.CaseState = "3";
            }
            else
            {
                info.PatientInfo.CaseState = this.CaseBase.PatientInfo.CaseState;
            }
            //是否有并发症
            info.SyndromeFlag = this.ucDiagNoseInput1.GetSyndromeFlag();
            info.InfectionNum = FS.FrameWork.Function.NConvert.ToInt32(txtInfectNum.Text);  //感染次数
            if (this.CaseBase.LendStat == null || this.CaseBase.LendStat == "") //病案借阅状态 
            {
                info.LendStat = "I";
            }
            else
            {
                info.LendStat = this.CaseBase.LendStat;
            }

            //过敏药物1
            //if (this.txtPharmacyAllergic1.Tag != null)
            //{
            //    info.FirstAnaphyPharmacy.ID = this.txtPharmacyAllergic1.Tag.ToString();
            //    info.FirstAnaphyPharmacy.Name = this.txtPharmacyAllergic1.Text;
            //}
            //else
            //{
            //    info.FirstAnaphyPharmacy.ID = "";
            //    info.FirstAnaphyPharmacy.Name = "";
            //}
            if (this.neuTxtPharmacyAllergic1.Text != "")
            {
                info.FirstAnaphyPharmacy.ID = this.neuTxtPharmacyAllergic1.Text;
            }
            else
            {
                info.FirstAnaphyPharmacy.ID = "";
            }

            //过敏药物2
            //if (this.txtPharmacyAllergic2.Tag != null)
            //{
            //    info.SecondAnaphyPharmacy.ID = this.txtPharmacyAllergic2.Tag.ToString();
            //    info.SecondAnaphyPharmacy.Name = this.txtPharmacyAllergic2.Text;
            //}
            //else
            //{
            //    info.SecondAnaphyPharmacy.ID = "";
            //    info.SecondAnaphyPharmacy.Name = "";
            //}
            //if (this.neuTxtPharmacyAllergic2.Text != "")
            //{
            //    info.SecondAnaphyPharmacy.ID = this.neuTxtPharmacyAllergic2.Text;
            //}
            //else
            //{
            //    info.SecondAnaphyPharmacy.ID = "";
            //}
            //感染部位
            info.InfectionPosition.Name = this.txtInfectionPositionNew.Text;

            //损伤中毒原因
            if (this.txtInjuryOrPoisoningCause.Tag != null)
            {
                info.InjuryOrPoisoningCauseCode = this.txtInjuryOrPoisoningCause.Tag.ToString();
            }
            info.InjuryOrPoisoningCause = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(this.txtInjuryOrPoisoningCause.Text,true);

            //病理诊断
            if (this.neuTxtPharmacyAllergic2.Tag != null)
            {
                info.PathologicalDiagCode = this.neuTxtPharmacyAllergic2.Tag.ToString();
            }
            info.PathologicalDiagName = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(this.neuTxtPharmacyAllergic2.Text,true);
            //输液反应
            if (this.txtReactionTransfuse.Tag != null)
            {
                info.ReactionLiquid = this.txtReactionTransfuse.Tag.ToString();
            }
            else
            {
                info.ReactionLiquid = "";
            }
            //转往何医院
            info.OutDept.Memo = this.txtComeTo.Text;
            //门诊诊断
            info.ClinicDiag.Name = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(this.txtClinicDiag.Text,true);
            //入院诊断
            info.InHospitalDiag.Name = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(this.txtRuyuanDiagNose.Text,true);
            #region 兼容最新版本字段
            info.BabyAge=CaseBase.BabyAge;
            info.BabyBirthWeight = CaseBase.BabyBirthWeight;
            info.BabyInWeight = CaseBase.BabyInWeight;
            info.CurrentAddr = CaseBase.CurrentAddr;
            info.CurrentPhone = CaseBase.CurrentPhone;
            info.CurrentZip = CaseBase.CurrentZip;
            info.InPath = CaseBase.InPath;
            info.InRoom = CaseBase.InRoom;
            info.OutRoom = CaseBase.OutRoom;
            info.ClinicDiag.ID = CaseBase.ClinicDiag.ID;
            info.ExampleType = CaseBase.ExampleType;
            info.ClinicPath = CaseBase.ClinicPath;
            info.InjuryOrPoisoningCauseCode = CaseBase.InjuryOrPoisoningCauseCode;
            info.PathologicalDiagCode = CaseBase.PathologicalDiagCode;
            info.PathNum = CaseBase.PathNum;
            info.AnaphyFlag = CaseBase.AnaphyFlag;
            info.DutyNurse.ID = CaseBase.DutyNurse.ID;
            info.DutyNurse.Name = CaseBase.DutyNurse.Name;
            //离院方式
            info.Out_Type=CaseBase.Out_Type;
            //医嘱转院接收医疗机构
            info.HighReceiveHopital=CaseBase.HighReceiveHopital;
            //医嘱转社区
            info.LowerReceiveHopital=CaseBase.LowerReceiveHopital;
            //出院31天内再住院计划
            info.ComeBackInMonth = CaseBase.ComeBackInMonth;
            //出院31天再住院目的
            info.ComeBackPurpose=CaseBase.ComeBackPurpose;
            //颅脑损伤患者昏迷时间 -入院前 天
            info.OutComeDay=CaseBase.OutComeDay;
            //颅脑损伤患者昏迷时间 -入院前 小时
            info.OutComeHour=CaseBase.OutComeHour;
            //颅脑损伤患者昏迷时间 -入院前 分钟
            info.OutComeMin=CaseBase.OutComeMin;
            //颅脑损伤患者昏迷时间 -入院后 天
            info.InComeDay=CaseBase.InComeDay;
            //颅脑损伤患者昏迷时间 -入院后 小时
            info.InComeHour=CaseBase.InComeHour;
            //颅脑损伤患者昏迷时间 -入院后 分钟
            info.InComeMin = CaseBase.InComeMin;
            #endregion
            #region 门诊诊断
            //			if( ClinicDiag.Tag != null)
            //			{
            //
            //				if( clinicDiag == null)
            //				{
            //					#region 新加的门诊诊断
            //					clinicDiag = new FS.HISFC.Models.HealthRecord.Diagnose();
            //					clinicDiag.DiagInfo.ICD10.ID = ClinicDiag.Tag.ToString();
            //					clinicDiag.DiagInfo.ICD10.Name = ClinicDiag.Text;
            //					clinicDiag.DiagInfo.Patient.ID = CaseBase.PatientInfo.ID;
            //					if(ClinicDocd.Tag != null||CaseBase.PatientInfo.CaseState == "1")
            //					{
            //						clinicDiag.DiagInfo.Doctor.ID = ClinicDocd.Tag.ToString();
            //						clinicDiag.DiagInfo.Doctor.Name = ClinicDocd.Text;
            //					}
            //					else
            //					{
            //						clinicDiag.DiagInfo.Doctor.ID = baseDml.Operator.ID;
            //						clinicDiag.DiagInfo.Doctor.Name = baseDml.Operator.Name;
            //					}
            //					clinicDiag.Pvisit.Date_In = CaseBase.PatientInfo.PVisit.InTime;
            //					clinicDiag.DiagInfo.DiagType.ID = "14";
            //					clinicDiag.DiagInfo.DiagDate = System.DateTime.Now;
            //					//入院诊断的标志位  0 默认， 1 修改 ，2 插入， 3 删除 
            //					RuDiag = 2 ;
            //					#endregion 
            //				}
            //				else
            //				{
            //					#region 修改的门诊诊断
            //					clinicDiag.DiagInfo.ICD10.ID = ClinicDiag.Tag.ToString();
            //					clinicDiag.DiagInfo.ICD10.Name = ClinicDiag.Text;
            //					clinicDiag.DiagInfo.DiagType.ID = "14";
            //					if(clinicDiag.DiagInfo.Patient.ID == null || clinicDiag.DiagInfo.Patient.ID == "")
            //					{
            //						clinicDiag.DiagInfo.Patient.ID = CaseBase.PatientInfo.ID;
            //					}
            //					if(ClinicDocd.Tag != null)
            //					{
            //						clinicDiag.DiagInfo.Doctor.ID = ClinicDocd.Tag.ToString();
            //						clinicDiag.DiagInfo.Doctor.Name = ClinicDocd.Text;
            //					}
            //					else
            //					{
            //						clinicDiag.DiagInfo.Doctor.ID = baseDml.Operator.ID;
            //						clinicDiag.DiagInfo.Doctor.Name = baseDml.Operator.Name;
            //					}
            //					if(clinicDiag.Pvisit.Date_In == System.DateTime.MinValue )
            //					{
            //						clinicDiag.Pvisit.Date_In = CaseBase.PatientInfo.PVisit.InTime;
            //					}
            //					if(clinicDiag.DiagInfo.DiagDate == System.DateTime.MinValue)
            //					{
            //						clinicDiag.DiagInfo.DiagDate = System.DateTime.Now;
            //					}
            //					//入院诊断的标志位  0 默认， 1 修改 ，2 插入， 3 删除 
            //					RuDiag = 1 ;
            //					#endregion 
            //				}
            //				if(this.frmType == "DOC")
            //				{
            //					clinicDiag.OperType = "1";
            //				}
            //				else if(this.frmType == "CAS")
            //				{
            //					clinicDiag.OperType = "2";
            //				}
            //			}
            //			else  if(ClinicDiag.Tag == null && clinicDiag != null) 
            //			{
            //				RuDiag = 3 ;
            //			}
            //			else
            //			{
            //				RuDiag = 0 ;
            //			}
            #endregion
            #region 入院诊断
            //			if(RuyuanDiagNose.Tag != null) //有入院诊断
            //			{
            //				if(InDiag == null||CaseBase.PatientInfo.CaseState =="1")
            //				{
            //					#region 新加的入院诊断
            //					InDiag = new FS.HISFC.Models.HealthRecord.Diagnose();
            //					InDiag.DiagInfo.ICD10.ID = RuyuanDiagNose.Tag.ToString();
            //					InDiag.DiagInfo.ICD10.Name = RuyuanDiagNose.Text;
            //					InDiag.DiagInfo.Patient.ID = CaseBase.PatientInfo.ID;
            //					InDiag.DiagInfo.Doctor.ID = baseDml.Operator.ID;
            //					InDiag.DiagInfo.Doctor.Name = baseDml.Operator.Name;
            //					InDiag.Pvisit.Date_In = CaseBase.PatientInfo.PVisit.InTime;
            //					InDiag.DiagInfo.DiagType.ID = "1";
            //					InDiag.DiagInfo.DiagDate = System.DateTime.Now;
            //
            //					menDiag = 2;
            //					#endregion 
            //				}
            //				else
            //				{
            //					#region 修改的入院诊断
            //					InDiag.DiagInfo.ICD10.ID = RuyuanDiagNose.Tag.ToString();
            //					InDiag.DiagInfo.ICD10.Name = RuyuanDiagNose.Text.ToString();
            //					InDiag.DiagInfo.DiagType.ID = "1";
            //					if( InDiag.DiagInfo.Patient.ID == null ||InDiag.DiagInfo.Patient.ID == "")
            //					{
            //						InDiag.DiagInfo.Patient.ID = CaseBase.PatientInfo.ID;
            //					}
            //					if(InDiag.DiagInfo.Doctor.ID == null || InDiag.DiagInfo.Doctor.ID == "")
            //					{
            //						InDiag.DiagInfo.Doctor.ID = baseDml.Operator.ID;
            //					}
            //					if(InDiag.DiagInfo.Doctor.Name == null || InDiag.DiagInfo.Doctor.Name == "")
            //					{
            //						InDiag.DiagInfo.Doctor.Name = baseDml.Operator.Name;
            //					}
            //					if(InDiag.Pvisit.Date_In  == System.DateTime.MinValue)
            //					{
            //						InDiag.Pvisit.Date_In = CaseBase.PatientInfo.PVisit.InTime;
            //					}
            //					if(InDiag.DiagInfo.DiagDate == System.DateTime.MinValue)
            //					{
            //						InDiag.DiagInfo.DiagDate = System.DateTime.Now;
            //					}
            //
            //					menDiag = 1;
            //					#endregion 
            //				}
            //				if(this.frmType == "DOC")
            //				{
            //					InDiag.OperType = "1";
            //				}
            //				else if(this.frmType == "CAS")
            //				{
            //					InDiag.OperType = "2";
            //				}
            //			}
            //			else  if(RuyuanDiagNose.Tag == null && InDiag != null) 
            //			{
            //				menDiag = 3;
            //			}
            //			else
            //			{
            //				menDiag = 0;
            //			}
            #endregion
            return 0;
        }
        #endregion

        #region 根据住院流水号 加载病案信息
        /// <summary>
        /// 根据传入的病人信息的病案状态,加载病案信息 
        /// </summary>
        /// <param name="InpatientNo">病人住院流水号</param>
        /// <param name="Type">类型</param>
        /// <returns>-1 程序出错,或传入的病人信息为空 0 病人不允许有病案 1 手工录入信息 </returns>
        public int LoadInfo(string InpatientNo, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes Type)
        {
            try
            {
                //add chengym 2011-9-22切换前切换后操作的是同一个患者 不再重新获取数据 save里面重新对CaseBase赋值；
                if (TempInpatient == InpatientNo && !this.isNeedLoadInfo)
                {
                    return 1;
                }

                TempInpatient = InpatientNo;
                this.ClearInfo();
                //end add
                if (InpatientNo == null || InpatientNo == "")
                {
                    MessageBox.Show("传入的住院流水号为空");
                    return -1;
                }
                FS.HISFC.BizProcess.Integrate.RADT pa = new FS.HISFC.BizProcess.Integrate.RADT();
                FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                //add by chengym  现实中常出现医生先写首页的情况，所以这里必须先从主表获取病人状态信息；
                patientInfo = pa.QueryPatientInfoByInpatientNO(InpatientNo);
                this.in_State = patientInfo.PVisit.InState.ID.ToString();
                this.dt_out = patientInfo.PVisit.OutTime;
                this.bedNo = patientInfo.PVisit.PatientLocation.Bed.ID;
                this.dept_out = patientInfo.PVisit.PatientLocation.Dept.ID;
                patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                //end add chengym
                ArrayList DrgsTimesList = this.con.GetList("CASEDRGSUSEDTIME");
                if (DrgsTimesList != null && DrgsTimesList.Count > 0)
                {
                    DateTime dt = this.con.GetDateTimeFromSysDateTime().Date.AddDays(10);
                    FS.FrameWork.Models.NeuObject usedTime = DrgsTimesList[0] as FS.FrameWork.Models.NeuObject;
                    try
                    {
                        if (usedTime.Memo != "")
                        {
                            dt = FS.FrameWork.Function.NConvert.ToDateTime(usedTime.Memo);
                        }
                    }
                    catch
                    {
                    }
                    if (this.in_State == "I")
                    {
                        if (dt.Date <= this.baseDml.GetDateTimeFromSysDateTime().Date)
                        {
                            MessageBox.Show("该患者应该填写新病案首页！", "提示");
                            return -1;
                        }
                    }
                    else
                    {
                        if (dt.Date <= this.dt_out.Date)
                        {
                            MessageBox.Show("该患者应该填写新病案首页！", "提示");
                            return -1;
                        }
                    }
                }

                CaseBase = baseDml.GetCaseBaseInfo(InpatientNo);

                if (CaseBase == null)
                {
                    CaseBase = new FS.HISFC.Models.HealthRecord.Base();
                    CaseBase.PatientInfo.ID = InpatientNo;
                    CaseBase.PatientInfo.PID.PatientNO = this.PatientNo;
                    CaseBase.PatientInfo.PID.CardNO = this.CardNo;
                }
                #region {E9F858A6-BDBC-4052-BA57-68755055FB80}

                if (string.IsNullOrEmpty(CaseBase.PatientInfo.PID.PatientNO))
                {
                    CaseBase.PatientInfo.PID.PatientNO = this.PatientNo;
                }

                if (string.IsNullOrEmpty(CaseBase.PatientInfo.PID.CardNO))
                {
                    CaseBase.PatientInfo.PID.CardNO = this.CardNo;
                }

                #endregion


                //1. 如果病案表中没有信息 则去住院表中去查询
                //2. 如果 住院主表中有记录则提取信息 写到界面上. 
                if (CaseBase.PatientInfo.ID == "" || CaseBase.OperInfo.OperTime == DateTime.MinValue)//病案主表中没有记录
                {
                    #region 病案中没有记录
                    patientInfo = pa.QueryPatientInfoByInpatientNO(InpatientNo);
                    if (patientInfo.ID == "") //住院主表中也没有相关基本信息
                    {
                        MessageBox.Show("没有查到相关的病人信息");
                        return 1;
                    }
                    else
                    {
                        CaseBase.PatientInfo = patientInfo;
                    }
                    #endregion
                }
                //如果是手工录入病案 可能查询出来的信息都为空 只有传入的InpatientNo 不为空
                this.frmType = Type;
                if (CaseBase.PatientInfo.CaseState == "0")
                {
                    MessageBox.Show("该病人不允许有病案");
                    return 0;
                }
                //保存病案的状态
                CaseFlag = FS.FrameWork.Function.NConvert.ToInt32(CaseBase.PatientInfo.CaseState);

                #region  转科信息
                //保存转科信息的列表
                ArrayList changeDept = new ArrayList();
                //获取转科信息
                changeDept = deptChange.QueryChangeDeptFromShiftApply(CaseBase.PatientInfo.ID, "2");
                firDept = null;
                secDept = null;
                thirDept = null;
                if (changeDept != null)
                {
                    if (changeDept.Count == 0)
                    {
                        changeDept = deptChange.QueryChangeDeptFromShiftApply(CaseBase.PatientInfo.ID, "1");
                    }
                    //加载 
                    LoadChangeDept(changeDept);
                }
                #endregion
                if (frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC) // 医生站录入病历
                {
                    #region  医生站录入病历

                    //目前允许有病历 并且目前没有录入病历  或者标志位位空（默认是允许录入病历） 
                    if (CaseBase.PatientInfo.CaseState == "1" || CaseBase.PatientInfo.CaseState == null)
                    {
                        //从住院主表中获取信息 并显示在界面上 
                        ConvertInfoToPanel(CaseBase);
                        SetReadOnly(false);
                    }
                    // 医生站录入过病历 
                    else if (CaseBase.PatientInfo.CaseState == "2")
                    {
                        //从病案基本表中获取信息 并显示在界面上 
                        CaseBase = baseDml.GetCaseBaseInfo(CaseBase.PatientInfo.ID);
                        CaseBase.PatientInfo.CaseState = CaseFlag.ToString();
                        if (CaseBase == null)
                        {
                            MessageBox.Show("查询病案失败" + baseDml.Err);
                            return -1;
                        }
                        //填充数据 
                        ConvertInfoToPanel(CaseBase);
                        SetReadOnly(false);
                    }
                    else
                    {
                        // 病案已经封存已经不允许医生修改
                        ConvertInfoToPanel(CaseBase);
                        SetReadOnly(true);
                    }

                    #endregion
                }
                else if (frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS)//病案室录入病历
                {
                    #region 病案室录入病历
                    //目前允许有病历 并且目前没有录入病历  或者标志位位空（默认是允许录入病历） 
                    if (CaseBase.PatientInfo.CaseState == "1" || CaseBase.PatientInfo.CaseState == null)
                    {
                        //从住院主表中获取信息 并显示在界面上 
                        ConvertInfoToPanel(CaseBase);
                        SetReadOnly(true);
                    }
                    else if (CaseBase.PatientInfo.CaseState == "2" || CaseBase.PatientInfo.CaseState == "3")
                    {
                        //					//医生站已经录入病案
                        ////					list = diag.QueryCaseDiagnose(patientInfo.ID,"%","1");
                        //				}
                        //				else if( patientInfo.Patient.CaseState == "3")
                        //				{
                        //从病案基本表中获取信息 并显示在界面上 
                        CaseBase = baseDml.GetCaseBaseInfo(CaseBase.PatientInfo.ID);
                        CaseBase.PatientInfo.CaseState = CaseFlag.ToString();
                        if (CaseBase == null)
                        {
                            MessageBox.Show("查询病案失败" + baseDml.Err);
                            return -1;
                        }
                        //填充数据 
                        ConvertInfoToPanel(CaseBase);
                        SetReadOnly(true);
                    }
                    else if ((CaseBase.PatientInfo.CaseState == "" || CaseBase.PatientInfo.CaseState == null) && CaseBase.IsHandCraft == "1")
                    {
                        //填充数据
                        ConvertInfoToPanel(CaseBase);
                        SetReadOnly(true);
                    }
                    else
                    {
                        //病案已经封存 不允许修改。
                        //					MessageBox.Show("病案已经封存,不允许修改");
                        ConvertInfoToPanel(CaseBase);
                        this.SetReadOnly(true); //设为只读 
                    }
                    this.ucDiagNoseInput1.SetReadOnly(false);
                    #endregion
                }
                else
                {
                    //没有传入参数 不作任何处理
                }
                #region 诊断
                this.ucDiagNoseInput1.LoadInfo(CaseBase.PatientInfo, frmType);
                //LoadDiag(this.ucDiagNoseInput1.diagList);
                #endregion
                #region  妇婴卡
                this.ucBabyCardInput1.LoadInfo(CaseBase.PatientInfo);
                #endregion
                #region 手术
                this.ucOperation1.LoadInfo(CaseBase.PatientInfo, frmType);
                #endregion
                #region  肿瘤
                this.ucTumourCard2.LoadInfo(CaseBase.PatientInfo, frmType);
                #endregion
                #region 转科
                this.ucChangeDept1.LoadInfo(CaseBase.PatientInfo, changeDept);
                #endregion
                #region  费用
                if (CaseBase.IsHandCraft == "1") //手工录入病案
                {
                    //金额可以修改
                    this.ucFeeInfo1.BoolType = true;
                }
                else
                {
                    //金额可以修改
                    this.ucFeeInfo1.BoolType = false;
                }
                this.ucFeeInfo1.LoadInfo(CaseBase.PatientInfo);
                #endregion

                #region 患者联系人列表

                #region {E9F858A6-BDBC-4052-BA57-68755055FB80}


                InitLinkWay(CaseBase.PatientInfo.PID.PatientNO, CaseBase.PatientInfo.PID.CardNO);

                #endregion

                #endregion

                //显示基本信息
                this.tab1.SelectedIndex = 0;
                ////住院号
                //this.txtPatientNOSearch.Focus();
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        void ucPatient_SelectItem(FS.HISFC.Models.HealthRecord.Base HealthRecord)
        {
            LoadInfo(HealthRecord.PatientInfo.ID, this.frmType);
            this.isNeedLoadInfo = true;//add chengym 2011-9-23
        }
        #endregion

        #region 私有事件
        private void txtCaseNum_Leave(object sender, EventArgs e)
        {
            if (txtCaseNum.Text.Trim() == "")
            {
                return;
            }
            this.txtCaseNum.Text = txtCaseNum.Text.Trim().PadLeft(10, '0');
        }
        private void deptThird_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtInfectionPositionNew.Focus();
            }
        }
        private void pactKind_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtSSN.Focus();
            }
        }
        private void deptOut_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtClinicDiag.Focus();
            }
        }
        private void patientBirthday_ValueChanged(object sender, System.EventArgs e)
        {
            //if (dtPatientBirthday.Value > System.DateTime.Now)
            //{
            //    dtPatientBirthday.Value = System.DateTime.Now;
            //}
            //if (dtPatientBirthday.Value.Year == System.DateTime.Now.Year)
            //{
            //    if (dtPatientBirthday.Value.Month == System.DateTime.Now.Month)
            //    {
            //        System.TimeSpan span = System.DateTime.Now - dtPatientBirthday.Value;
            //        if (span.Days != 0) //天
            //        {
            //            txtPatientAge.Text = span.Days.ToString();
            //            cmbUnit.Text = "天";
            //        }
            //        else
            //        {
            //            txtPatientAge.Text = span.Hours.ToString();
            //            cmbUnit.Text = "小时";
            //        }
            //    }
            //    else //月
            //    {
            //        txtPatientAge.Text = Convert.ToString(System.DateTime.Now.Month - dtPatientBirthday.Value.Month);
            //        cmbUnit.Text = "月";
            //    }
            //}
            //else //岁
            //{
            //    txtPatientAge.Text = Convert.ToString(System.DateTime.Now.Year - dtPatientBirthday.Value.Year);
            //    cmbUnit.Text = "岁";
            //}
        }
        private void txtPatientNOSearch_Enter(object sender, EventArgs e)
        {
            this.ucPatient.Location = new Point(this.txtPatientNOSearch.Location.X, this.txtPatientNOSearch.Location.Y + this.txtPatientNOSearch.Height + 2);
            this.ucPatient.BringToFront();
            this.ucPatient.Visible = false;
        }
        private void txtCaseNOSearch_Enter(object sender, EventArgs e)
        {
            this.ucPatient.Location = new Point(this.txtCaseNOSearch.Location.X, this.txtCaseNOSearch.Location.Y + this.txtCaseNOSearch.Height + 2);
            this.ucPatient.BringToFront();
            this.ucPatient.Visible = false;
        }
        private void Date_Out_ValueChanged(object sender, System.EventArgs e)
        {
            if (txtDateOut.Value < this.dtDateIn.Value)
            {
                txtDateOut.Value = dtDateIn.Value;
            }
            System.TimeSpan tt = this.txtDateOut.Value.Date - this.dtDateIn.Value.Date;
            this.txtPiDays.Text = Convert.ToString(tt.Days);
        }

        #endregion

        #region 回车事件
        private void txtBC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtECTNumb.Focus();
            }
        }
        private void cmbUnit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtMaritalStatus.Focus();
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {
            if (this.label1.Text == "病 案 号")
            {
                label1.Text = "住 院 号";
            }
            else if (this.label1.Text == "住 院 号")
            {
                label1.Text = "病 案 号";
            }
        }
        private void caseNum_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtInTimes.Focus();
            }
            else if (e.KeyData == Keys.Divide)
            {
                return;
            }
        }


        private void SSN_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtClinicNo.Focus();
            }
        }

        private void PatientName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtPatientSex.Focus();
            }
        }

        private void clinicNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtPatientName.Focus();
            }
        }

        private void patientBirthday_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //string age= FS.FrameWork.Public.String.GetAge(this.dtPatientBirthday.Value, baseDml.GetDateTimeFromSysDateTime());
            //this.txtPatientAge.Text = age.Substring(0, age.Length - 1);
            //this.cmbUnit.Text = age.Substring(age.Length - 1);
            if (e.KeyData == Keys.Enter)
            {
                this.txtMaritalStatus.Focus();
            }
        }

        private void PatientAge_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbUnit.Focus();
            }
        }

        private void DIST_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtIDNo.Focus();
            }
        }

        private void IDNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtAddressBusiness.Focus();
            }
        }
        private void BusinessZip_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtPhoneBusiness.Focus();
            }
        }

        private void PhoneBusiness_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtAddressHome.Focus();
            }
        }

        private void AddressHome_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtHomeZip.Focus();
            }
        }
        private void HomeZip_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtPhoneHome.Focus();
            }
        }
        private void AddressBusiness_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtBusinessZip.Focus();
            }
        }

        private void PhoneHome_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtKin.Focus();
            }
        }

        private void Kin_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtRelation.Focus();
            }
        }

        private void LinkmanTel_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtLinkmanAdd.Focus();
            }
        }

        private void LinkmanAdd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtNomen.Focus();

            }
        }
        private void txtNomen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.dtDateIn.Focus();
            }
        }
        private void Date_In_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                //				System.TimeSpan tt = this.Date_Out.Value - this.Date_In.Value;
                //				this.PiDays.Text = Convert.ToString(tt.Days+1);
                this.txtDeptInHospital.Focus();
            }
        }
        private void dateTimePicker3_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtFirstDept.Focus();
            }
        }

        private void dateTimePicker4_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtDeptSecond.Focus();
            }
        }

        private void dtThird_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtDeptThird.Focus();
            }
        }

        private void Date_Out_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                System.TimeSpan tt = this.txtDateOut.Value.Date - this.dtDateIn.Value.Date;
                this.txtPiDays.Text = Convert.ToString(tt.Days);
                this.txtDeptOut.Focus();
            }
        }

        private void DiagDate_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtRuyuanDiagNose.Focus();
            }
        }

        private void PiDays_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtDiagDate.Focus();
            }
        }

        private void ComeFrom_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.dtFirstTime.Focus();
            }
        }

        private void Nomen_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtInAvenue.Focus();
            }
        }

        private void infectNum_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtPharmacyAllergic1.Focus();
            }
        }

        private void CheckDate_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtMrQual.Focus();
            }
        }

        private void YnFirst_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.NumPad1)
            {
                cbYnFirst.Checked = !cbYnFirst.Checked;
                this.cbVisiStat.Focus();
            }
            else if (e.KeyData == Keys.Enter)
            {
                this.cbVisiStat.Focus();
            }
        }

        private void VisiStat_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.NumPad1)
            {
                cbVisiStat.Checked = !cbVisiStat.Checked;
                this.txtVisiPeriWeek.Focus();
            }
            else if (e.KeyData == Keys.Enter)
            {
                this.txtVisiPeriWeek.Focus();
            }
        }

        private void VisiPeriWeek_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtVisiPeriMonth.Focus();
            }
        }

        private void VisiPeriMonth_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtVisiPeriYear.Focus();
            }
        }

        private void VisiPeriYear_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cbTechSerc.Focus();
            }
        }

        private void TechSerc_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.NumPad1)
            {
                cbTechSerc.Checked = !cbTechSerc.Checked;
                this.cbDisease30.Focus();
            }
            else if (e.KeyData == Keys.Enter)
            {
                this.cbDisease30.Focus();
            }
        }

        private void BloodRed_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtBloodPlatelet.Focus();
            }
        }

        private void BloodPlatelet_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtBodyAnotomize.Focus();
            }
        }

        private void BodyAnotomize_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtBloodWhole.Focus();
            }
        }

        private void BloodWhole_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtBloodOther.Focus();
            }
        }

        private void BloodOther_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtInconNum.Focus();
            }
        }

        private void InconNum_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtOutconNum.Focus();
            }
        }

        private void outOutconNum_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtSuperNus.Focus();
            }
        }

        private void SuperNus_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtINus.Focus();
            }
        }

        private void INus_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtIINus.Focus();
            }
        }

        private void IINus_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtIIINus.Focus();
            }
        }

        private void IIINus_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtStrictNuss.Focus();
            }
        }

        private void StrictNuss_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtSPecalNus.Focus();
            }
        }

        private void SPecalNus_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtInfectionDiseasesReport.Focus();
            }
        }

        private void CtNumb_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtPathNumb.Focus();
            }
        }

        private void textBox54_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtMriNumb.Focus();
            }
        }

        private void MriNumb_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtXNumb.Focus();
            }
        }

        private void XNumb_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtBC.Focus();
            }
        }

        private void checkBox9_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtInputDoc.Focus();
            }
        }

        private void SalvTimes_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtSuccTimes.Focus();
            }
        }

        private void SuccTimes_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtCtNumb.Focus();
            }
        }

        private void BodyCheck_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cbYnFirst.Focus();
            }
            else if (e.KeyData == Keys.NumPad1)
            {
                this.cbBodyCheck.Checked = !this.cbBodyCheck.Checked;
                this.cbYnFirst.Focus();
            }
        }

        private void checkBox8_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtBloodType.Focus();
            }
            else if (e.KeyData == Keys.NumPad1)
            {
                this.cbBodyCheck.Checked = !this.cbBodyCheck.Checked;
                this.txtBloodType.Focus();
            }
        }

        private void Date_In_Leave(object sender, System.EventArgs e)
        {
            System.TimeSpan tt = this.txtDateOut.Value.Date - this.dtDateIn.Value.Date;
            this.txtPiDays.Text = Convert.ToString(tt.Days);
        }

        private void Date_Out_Leave(object sender, System.EventArgs e)
        {
            System.TimeSpan tt = this.txtDateOut.Value.Date - this.dtDateIn.Value.Date;
            this.txtPiDays.Text = Convert.ToString(tt.Days);
        }
        private void OperationCOde_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtInputDoc.Focus();
            }
        }

        private void txtInfectionPosition_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtInjuryOrPoisoningCause.Focus();
            }
        }

        private void txtPharmacyAllergic1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtPharmacyAllergic2.Focus();
            }

        }

        private void txtPharmacyAllergic2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtHbsag.Focus();
            }

        }

        private void txtECTNumb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtPETNumb.Focus();
            }
        }

        private void txtPETNumb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.cbBodyCheck.Focus();
            }
        }

        #endregion

        #region 重载 捕获键盘
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Divide)
            {
                if (this.tab1.SelectedIndex != 6)
                {
                    this.tab1.SelectedIndex++;
                }
                else
                {
                    this.tab1.SelectedIndex = 0;
                }
            }
            else if (keyData == Keys.Escape)
            {
                this.ucPatient.Visible = false;
            }
            return base.ProcessDialogKey(keyData);
        }
        #endregion

        #region 加载 入院诊断和门诊诊断
        /// <summary>
        /// 加载 入院诊断和门诊诊断
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private int LoadDiag(ArrayList list)
        {
            if (list == null)
            {
                return -1;
            }
            clinicDiag = null;
            InDiag = null;
            #region 先默认输入一个门诊主诊断
            foreach (FS.HISFC.Models.HealthRecord.Diagnose obj in list)
            {
                if (obj.DiagInfo.DiagType.ID == "10" && obj.DiagInfo.IsMain)
                {	//门诊诊断 
                    this.txtClinicDiag.Tag = obj.DiagInfo.ICD10.ID;
                    this.txtClinicDiag.Text = obj.DiagInfo.ICD10.Name;
                    this.txtClinicDocd.Tag = obj.DiagInfo.Doctor.ID;
                    this.txtClinicDocd.Text = obj.DiagInfo.Doctor.Name;
                    clinicDiag = obj;
                }
                else if (obj.DiagInfo.DiagType.ID == "11" && obj.DiagInfo.IsMain)
                {	//入院诊断
                    txtRuyuanDiagNose.Tag = obj.DiagInfo.ICD10.ID;
                    txtRuyuanDiagNose.Text = obj.DiagInfo.ICD10.Name;
                    InDiag = obj;
                }
            }
            #endregion

            #region 如果没有主诊断 则输入非主诊断诊断
            foreach (FS.HISFC.Models.HealthRecord.Diagnose obj in list)
            {
                if (obj.DiagInfo.DiagType.ID == "10")
                {	//门诊诊断 
                    if (this.txtClinicDiag.Tag == null)
                    {
                        this.txtClinicDiag.Tag = obj.DiagInfo.ICD10.ID;
                        this.txtClinicDiag.Text = obj.DiagInfo.ICD10.Name;
                        this.txtClinicDocd.Tag = obj.DiagInfo.Doctor.ID;
                        this.txtClinicDocd.Text = obj.DiagInfo.Doctor.Name;
                        clinicDiag = obj;
                    }
                }
                else if (obj.DiagInfo.DiagType.ID == "11")
                {	//入院诊断
                    if (txtRuyuanDiagNose.Tag == null)
                    {
                        txtRuyuanDiagNose.Tag = obj.DiagInfo.ICD10.ID;
                        txtRuyuanDiagNose.Text = obj.DiagInfo.ICD10.Name;
                        InDiag = obj;
                    }
                }
            }
            #endregion
            return 0;
        }
        #endregion

        #region 检验数据的合法性
        /// <summary>
        /// 检验数据的合法性
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private int ValidState(FS.HISFC.Models.HealthRecord.Base info)
        {
            #region  校验
            if (txtDeptInHospital.Text == "" && txtDeptOut.Text != "")
            {
                MessageBox.Show("请先填写出院科室信息");
                txtDeptOut.Focus();
                return -1;
            }
            if (txtDeptInHospital.Text == "" && txtFirstDept.Text != "")
            {
                MessageBox.Show("请先填写入院科室信息");
                txtDeptInHospital.Focus();
                return -1;
            }
            if (txtFirstDept.Text == "" && txtDeptSecond.Text != "")
            {
                MessageBox.Show("请先填写第一次转科信息");
                txtFirstDept.Focus();
                return -1;
            }
            if (txtDeptSecond.Text == "" && txtDeptThird.Text != "")
            {
                MessageBox.Show("请先填写第二次转科信息");
                txtDeptSecond.Focus();
                return -1;
            }
            if (dtFirstTime.Value.Date < this.dtDateIn.Value.Date)
            {
                MessageBox.Show("第一次转科时间不能小于入院时间");
                dtFirstTime.Focus();
                return -1;
            }
            if (dtFirstTime.Value.Date > this.txtDateOut.Value.Date)
            {
                MessageBox.Show("第一次转科时间不能大于于出院时间");
                dtFirstTime.Focus();
                return -1;
            }
            if ((dtFirstTime.Value.Date > dtSecond.Value.Date) && txtDeptSecond.Text.Trim() != string.Empty)
            {
                MessageBox.Show("第一次转科时间不能大于第二次转科时间");
                dtFirstTime.Focus();
                return -1;
            }
            if ((dtSecond.Value.Date > dtThird.Value.Date) && txtDeptThird.Text.Trim() != string.Empty)
            {
                MessageBox.Show("第二次转科时间不能大于第三次转科时间");
                dtSecond.Focus();
                return -1;
            }
            #endregion
            if (info.PatientInfo.Pact.ID == null)
            {
                this.txtPactKind.Focus();
                MessageBox.Show("请选择结算类别");
                return -1;
            }
            if (info.PatientInfo.Pact.ID == "")
            {
                this.txtPactKind.Focus();
                MessageBox.Show("请选择结算类别");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.ID, 14))
            {
                MessageBox.Show("住院流水号过长");
                return -1;
            }
            //if (!ValidMaxLengh(info.PatientInfo.PID.PatientNO, 10))
            //{
            //    txtCaseNum.Focus();
            //    MessageBox.Show("住院号过长");
            //    return -1;
            //}
            if (!ValidMaxLengh(info.CaseNO, 10))
            {
                txtCaseNum.Focus();
                MessageBox.Show("病案号过长");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.PID.CardNO, 10))
            {
                txtClinicNo.Focus();
                MessageBox.Show("就诊卡号过长");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.SSN, 18))
            {
                txtSSN.Focus();
                MessageBox.Show("医保号过长");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.PID.CardNO, 10))
            {
                txtSSN.Focus();
                MessageBox.Show("卡号过长");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.Name, 20))
            {
                txtPatientName.Focus();
                MessageBox.Show("姓名过长");
                return -1;
            }
            if (!ValidMaxLengh(info.Nomen, 20))
            {
                txtNomen.Focus();
                MessageBox.Show("曾用名过长");
                return -1;
            }
            if (info.PatientInfo.Sex.ID != null)
            {
                if (!ValidMaxLengh(info.PatientInfo.Sex.ID.ToString(), 20))
                {
                    txtPatientSex.Focus();
                    MessageBox.Show("性别编码过长");
                    return -1;
                }
            }
            if (!ValidMaxLengh(info.PatientInfo.Country.ID, 20))
            {
                txtCountry.Focus();
                MessageBox.Show("国籍编码过长");
                return -1;
            }

            if (!ValidMaxLengh(info.PatientInfo.Nationality.ID, 20))
            {
                txtNationality.Focus();
                MessageBox.Show("民族编码过长");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.Profession.ID, 20))
            {
                txtProfession.Focus();
                MessageBox.Show("职业编码过长");
                return -1;
            }
            if (info.PatientInfo.BloodType.ID != null)
            {
                if (!ValidMaxLengh(info.PatientInfo.BloodType.ID.ToString(), 20))
                {
                    txtBloodType.Focus();
                    MessageBox.Show("血型编码过长");
                    return -1;
                }
            }
            if (info.PatientInfo.MaritalStatus.ID != null)
            {
                if (!ValidMaxLengh(info.PatientInfo.MaritalStatus.ID.ToString(), 10))
                {
                    txtMaritalStatus.Focus();
                    MessageBox.Show("婚姻编码过长");
                    return -1;
                }
            }
            if (info.AgeUnit != null)
            {
                if (!ValidMaxLengh(info.AgeUnit, 10))
                {
                    txtPatientAge.Focus();
                    MessageBox.Show("年龄单位过长");
                    return -1;
                }
            }

            if (!ValidMaxLengh(info.PatientInfo.Age, 3))
            {
                txtPatientAge.Focus();
                MessageBox.Show("年龄过长");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.IDCard, 18))
            {
                txtIDNo.Focus();
                MessageBox.Show("身份证过长");
                return -1;
            }
            //			if(info.PatientInfo.PVisit.InSource.ID.Length  > 1 )
            //			{
            //				In.Focus();
            //				MessageBox.Show("地区来源编码过长");
            //				return -1;
            //			} 
            if (!ValidMaxLengh(info.PatientInfo.Pact.PayKind.ID, 20))
            {
                txtPactKind.Focus();
                MessageBox.Show("结算类别编码过长");
                return -1;
            }

            if (!ValidMaxLengh(info.PatientInfo.Pact.ID, 20))
            {
                txtPactKind.Focus();
                MessageBox.Show("合同单位编码过长");
                return -1;
            }
            if (info.PatientInfo.Pact.ID == null || info.PatientInfo.Pact.ID == "")
            {
                txtPactKind.Focus();
                MessageBox.Show("合同单位编码不能为空");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.SSN, 18))
            {
                txtSSN.Focus();
                MessageBox.Show("医保公费号过长");
                return -1;
            }

            if (!ValidMaxLengh(info.PatientInfo.DIST, 50))
            {
                txtDIST.Focus();
                MessageBox.Show("籍贯过长");
                return -1;
            }

            if (!ValidMaxLengh(info.PatientInfo.AddressHome, 50))
            {
                txtAddressHome.Focus();
                MessageBox.Show("家庭住址过长");
                return -1;
            }

            if (!ValidMaxLengh(info.PatientInfo.PhoneHome, 25))
            {
                txtPhoneHome.Focus();
                MessageBox.Show("家庭电话过长");
                return -1;
            }

            if (!ValidMaxLengh(info.PatientInfo.HomeZip, 25))
            {
                txtHomeZip.Focus();
                MessageBox.Show("住址邮编过长");
                return -1;
            }

            if (!ValidMaxLengh(info.PatientInfo.AddressBusiness, 50))
            {
                txtAddressBusiness.Focus();
                MessageBox.Show("单位地址过长");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.PhoneBusiness, 25))
            {
                txtPhoneBusiness.Focus();
                MessageBox.Show("单位电话过长");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.BusinessZip, 6))
            {
                txtBusinessZip.Focus();
                MessageBox.Show("单位邮编过长");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.Kin.Name, 10))
            {
                txtKin.Focus();
                MessageBox.Show("联系人过长");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.Kin.RelationLink, 20))
            {
                txtRelation.Focus();
                MessageBox.Show("联系人关系过长");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.Kin.RelationAddress, 50))
            {
                txtLinkmanAdd.Focus();
                MessageBox.Show("联系地址过长");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.Kin.RelationPhone, 25))
            {
                txtLinkmanTel.Focus();
                MessageBox.Show("联系电话过长");
                return -1;
            }
            if (!ValidMaxLengh(info.ComeFrom, 100))
            {
                txtComeFrom.Focus();
                MessageBox.Show("转来医院");
                return -1;
            }
            if (info.PatientInfo.InTimes > 99)
            {
                txtComeFrom.Focus();
                MessageBox.Show("入院次数过大");
                return -1;
            }
            if (info.SalvTimes > 99)
            {
                txtSalvTimes.Focus();
                MessageBox.Show("抢救次数过大");
                return -1;
            }
            if (info.InfectionNum > 99)
            {
                txtInfectNum.Focus();
                MessageBox.Show("成功次数过大");
                return -1;
            }
            if (!ValidMaxLengh(info.VisiPeriodWeek, 6))
            {
                txtVisiPeriWeek.Focus();
                MessageBox.Show("诊断期限周数过长");
                return -1;
            }
            if (!ValidMaxLengh(info.VisiPeriodMonth, 6))
            {
                txtVisiPeriMonth.Focus();
                MessageBox.Show("诊断期限月数过长");
                return -1;
            }
            if (!ValidMaxLengh(info.VisiPeriodYear, 6))
            {
                txtVisiPeriYear.Focus();
                MessageBox.Show("诊断期限年数过长");
                return -1;
            }
            if (!ValidMaxLengh(info.BloodRed, 10))
            {
                txtBloodRed.Focus();
                MessageBox.Show("红细胞数量过大");
                return -1;
            }
            if (!ValidMaxLengh(info.BloodPlatelet, 10))
            {
                txtBloodPlatelet.Focus();
                MessageBox.Show("血小板数量过大");
                return -1;
            }
            if (!ValidMaxLengh(info.BloodPlasma, 10))
            {
                txtBodyAnotomize.Focus();
                MessageBox.Show("血浆数量过大");
                return -1;
            }
            if (!ValidMaxLengh(info.BloodWhole, 10))
            {
                txtBloodWhole.Focus();
                MessageBox.Show("全血数量过大");
                return -1;
            }
            if (!ValidMaxLengh(info.BloodOther, 10))
            {
                txtBloodOther.Focus();
                MessageBox.Show("其他输血数量过大");
                return -1;
            }
            if (info.InconNum > 99)
            {
                txtInconNum.Focus();
                MessageBox.Show("院际会诊次数过大");
                return -1;
            }
            if (info.OutconNum > 99)
            {
                txtOutconNum.Focus();
                MessageBox.Show("远程次数数量过大");
                return -1;
            }
            if (info.SpecalNus > 9999)
            {
                txtSuperNus.Focus();
                MessageBox.Show("特殊护理数量过大");
                return -1;
            }
            if (info.INus > 9999)
            {
                txtINus.Focus();
                MessageBox.Show("I级护理时间数量过大");
                return -1;
            }
            if (info.IINus > 9999)
            {
                txtIINus.Focus();
                MessageBox.Show("II级护理时间数量过大");
                return -1;
            }
            if (info.IIINus > 9999)
            {
                txtIIINus.Focus();
                MessageBox.Show("III级护理时间数量过大");
                return -1;
            }
            if (info.StrictNuss > 9999)
            {
                txtStrictNuss.Focus();
                MessageBox.Show("重症监护时间数量过大");
                return -1;
            }
            if (info.SuperNus > 9999)
            {
                txtSuperNus.Focus();
                MessageBox.Show("特级护理时间数量过大");
                return -1;
            }
            if (!ValidMaxLengh(info.CtNum, 10))
            {
                txtCtNumb.Focus();
                MessageBox.Show("CT号过大");
                return -1;
            }
            if (!ValidMaxLengh(info.XNum, 10))
            {
                txtXNumb.Focus();
                MessageBox.Show("X光号过大");
                return -1;
            }
            if (!ValidMaxLengh(info.MriNum, 10))
            {
                txtMriNumb.Focus();
                MessageBox.Show("M R 号过大");
                return -1;
            }
            if (!ValidMaxLengh(info.PathNum, 10))
            {
                txtPathNumb.Focus();
                MessageBox.Show("UFCT 号过大");
                return -1;
            }
            //add by 2011-9-20 chengym
            if (info.SalvTimes > 0 && info.SalvTimes < info.SuccTimes)
            {
                MessageBox.Show("抢救成功次数不应大于抢救次数", "提示");
                txtSuccTimes.Focus();
                return -1;
            }
            if (info.PatientInfo.PVisit.ReferringDoctor.ID == "" && info.PatientInfo.PVisit.ConsultingDoctor.ID == "" && info.PatientInfo.PVisit.AttendingDoctor.ID == "")
            {
                MessageBox.Show("科主任、主任医师、主治医生都不能为空");
                return -1;
            }
            //if (info.CePi == "" || info.PiPo == "")
            //{
            //    MessageBox.Show("门诊与出院或入院与出院；诊断符合情况不能为空");
            //    txtCePi.Focus();
            //    return -1;
            //}
            if (info.PatientInfo.PVisit.OutTime.Date < info.PatientInfo.PVisit.InTime.Date)
            {
                MessageBox.Show("“出院日期”不能小于“入院时间”");
                return -1;
            }
            if (txtCheckDate.Value.Date > info.PatientInfo.PVisit.OutTime.Date.AddDays(15))
            {
                this.txtCheckDate.Focus();
                MessageBox.Show("“质控日期”不能超过出院时间15天");
                return -1;
            }
            if (info.DiagDate.Date < info.PatientInfo.PVisit.InTime.Date)
            {
                MessageBox.Show("“确诊日期”不能小于入院日期");
                this.txtDiagDate.Focus();
                return -1;
            }
            if (info.DiagDate.Date > info.PatientInfo.PVisit.OutTime.Date)
            {
                MessageBox.Show("“确诊日期”不能大于出院日期");
                this.txtDiagDate.Focus();
                return -1;
            }
            return 1;
        }
        #region 获取最大值
        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private bool ValidMaxLengh(string str, int length)
        {
            return FS.FrameWork.Public.String.ValidMaxLengh(str, length);
        }
        #endregion
        #endregion

        #region 设置为只读
        /// <summary>
        /// 设置为只读
        /// </summary>
        /// <param name="type"></param> 
        public void SetReadOnly(bool type)
        {
            this.ucDiagNoseInput1.SetReadOnly(type);
            this.ucOperation1.SetReadOnly(type);
            this.ucTumourCard2.SetReadOnly(type);
            if (this.changeDeptType == 1) //自动加载转科信息
            {
                this.ucChangeDept1.SetReadOnly(true);
            }
            else
            {
                this.ucChangeDept1.SetReadOnly(type);
            }
            this.ucBabyCardInput1.SetReadOnly(type);
            //病案号 
            txtCaseNum.ReadOnly = type;
            txtCaseNum.BackColor = System.Drawing.Color.White;
            //住院次数
            txtInTimes.ReadOnly = type;
            txtInTimes.BackColor = System.Drawing.Color.White;
            //费用类别
            txtPactKind.ReadOnly = type;
            txtPactKind.EnterVisiable = !type;
            txtPactKind.BackColor = System.Drawing.Color.White;
            //医保号
            txtSSN.ReadOnly = type;
            txtSSN.BackColor = System.Drawing.Color.White;
            //门诊号
            txtClinicNo.ReadOnly = type;
            txtClinicNo.BackColor = System.Drawing.Color.White;
            //姓名
            txtPatientName.ReadOnly = type;
            txtPatientName.BackColor = System.Drawing.Color.White;
            //性别
            txtPatientSex.ReadOnly = type;
            txtPatientSex.EnterVisiable = !type;
            txtPatientSex.BackColor = System.Drawing.Color.White;
            //生日
            dtPatientBirthday.Enabled = !type;
            //年龄
            txtPatientAge.ReadOnly = true;
            txtPatientAge.BackColor = System.Drawing.Color.White;
            //婚姻
            txtMaritalStatus.ReadOnly = type;
            txtMaritalStatus.EnterVisiable = !type;
            txtMaritalStatus.BackColor = System.Drawing.Color.White;
            //职业
            txtProfession.ReadOnly = type;
            txtProfession.EnterVisiable = !type;
            txtProfession.BackColor = System.Drawing.Color.White;
            //出生地
            txtAreaCode.ReadOnly = type;
            txtAreaCode.BackColor = System.Drawing.Color.White;
            //民族
            txtNationality.ReadOnly = type;
            txtNationality.EnterVisiable = !type;
            txtNationality.BackColor = System.Drawing.Color.White;
            //国籍
            txtCountry.ReadOnly = type;
            txtCountry.EnterVisiable = !type;
            txtCountry.BackColor = System.Drawing.Color.White;
            //籍贯
            txtDIST.ReadOnly = type;
            txtDIST.BackColor = System.Drawing.Color.White;
            //身份证
            txtIDNo.ReadOnly = type;
            txtIDNo.BackColor = System.Drawing.Color.White;
            //工作单位
            txtAddressBusiness.ReadOnly = type;
            txtAddressBusiness.BackColor = System.Drawing.Color.White;
            //单位邮编
            txtBusinessZip.ReadOnly = type;
            txtBusinessZip.BackColor = System.Drawing.Color.White;
            //单位电话
            txtPhoneBusiness.ReadOnly = type;
            txtPhoneBusiness.BackColor = System.Drawing.Color.White;
            //户口地址
            txtAddressHome.ReadOnly = type;
            txtAddressHome.BackColor = System.Drawing.Color.White;
            //户口邮编
            txtHomeZip.ReadOnly = type;
            txtHomeZip.BackColor = System.Drawing.Color.White;
            //家庭电话
            txtPhoneHome.ReadOnly = type;
            txtPhoneHome.BackColor = System.Drawing.Color.White;
            //联系人 
            txtKin.ReadOnly = type;
            txtKin.BackColor = System.Drawing.Color.White;
            //关系
            txtRelation.ReadOnly = type;
            txtRelation.EnterVisiable = !type;
            txtRelation.BackColor = System.Drawing.Color.White;
            //联系电话
            txtLinkmanTel.ReadOnly = type;
            txtLinkmanTel.BackColor = System.Drawing.Color.White;
            //l联系人地址
            txtLinkmanAdd.ReadOnly = type;
            txtLinkmanAdd.BackColor = System.Drawing.Color.White;
            //入院科室
            txtDeptInHospital.ReadOnly = type;
            txtDeptInHospital.EnterVisiable = !type;
            txtDeptInHospital.BackColor = System.Drawing.Color.White;
            //入院时间 
            dtDateIn.Enabled = !type;
            //入院情况
            txtCircs.ReadOnly = type;
            txtCircs.EnterVisiable = !type;
            txtCircs.BackColor = System.Drawing.Color.White;
            //转入科室
            txtFirstDept.ReadOnly = type;
            txtFirstDept.EnterVisiable = !type;
            txtFirstDept.BackColor = System.Drawing.Color.White;
            //转科时间
            dtFirstTime.Enabled = !type;
            dtFirstTime.BackColor = System.Drawing.Color.White;
            //转入科室
            txtDeptSecond.ReadOnly = type;
            txtDeptSecond.EnterVisiable = !type;
            txtDeptSecond.BackColor = System.Drawing.Color.White;
            //转科时间
            dtSecond.Enabled = !type;
            //转入科室
            txtDeptThird.ReadOnly = type;
            txtDeptThird.EnterVisiable = !type;
            txtDeptThird.BackColor = System.Drawing.Color.White;
            //转科时间
            dtThird.Enabled = !type;
            //出院科室
            txtDeptOut.ReadOnly = type;
            txtDeptOut.EnterVisiable = !type;
            txtDeptOut.BackColor = System.Drawing.Color.White;
            //出院时间
            txtDateOut.Enabled = !type;
            //门诊诊断
            //			ClinicDiag.ReadOnly = type;
            txtClinicDiag.BackColor = System.Drawing.Color.Gainsboro;
            //诊断医生
            txtClinicDocd.ReadOnly = type;
            txtClinicDocd.EnterVisiable = !type;
            txtClinicDocd.BackColor = System.Drawing.Color.White;
            //住院天数
            txtPiDays.ReadOnly = type;
            txtPiDays.BackColor = System.Drawing.Color.White;
            //确证时间
            txtDiagDate.Enabled = !type;
            //入院诊断
            //			RuyuanDiagNose.ReadOnly = type;
            txtRuyuanDiagNose.BackColor = System.Drawing.Color.Gainsboro;
            //由何医院转来
            txtComeFrom.ReadOnly = type;
            txtComeFrom.BackColor = System.Drawing.Color.White;
            //曾用名
            txtNomen.ReadOnly = type;
            txtNomen.BackColor = System.Drawing.Color.White;
            //病人来源
            txtInAvenue.ReadOnly = type;
            txtInAvenue.EnterVisiable = !type;
            txtInAvenue.BackColor = System.Drawing.Color.White;
            //院感次数
            txtInfectNum.ReadOnly = type;
            txtInfectNum.BackColor = System.Drawing.Color.White;
            //hbsag
            txtHbsag.ReadOnly = type;
            txtHbsag.EnterVisiable = !type;
            txtHbsag.BackColor = System.Drawing.Color.White;
            txtHcvAb.ReadOnly = type;
            txtHcvAb.EnterVisiable = !type;
            txtHcvAb.BackColor = System.Drawing.Color.White;
            //门诊与出院
            txtCePi.ReadOnly = type;
            txtCePi.EnterVisiable = !type;
            txtCePi.BackColor = System.Drawing.Color.White;
            //入院与出院 
            txtPiPo.ReadOnly = type;
            txtPiPo.EnterVisiable = !type;
            txtPiPo.BackColor = System.Drawing.Color.White;
            //术前与术后
            txtOpbOpa.ReadOnly = type;
            txtOpbOpa.EnterVisiable = !type;
            txtOpbOpa.BackColor = System.Drawing.Color.White;
            //临床与病理
            txtClPa.ReadOnly = type;
            txtClPa.EnterVisiable = !type;
            txtClPa.BackColor = System.Drawing.Color.White;
            //放射与病理
            txtFsBl.ReadOnly = type;
            txtFsBl.EnterVisiable = !type;
            txtFsBl.BackColor = System.Drawing.Color.White;
            //抢救次数
            txtSalvTimes.ReadOnly = type;
            txtSalvTimes.BackColor = System.Drawing.Color.White;
            //成功次数
            txtSuccTimes.ReadOnly = type;
            txtSuccTimes.BackColor = System.Drawing.Color.White;
            //病案质量
            txtMrQual.ReadOnly = type;
            txtMrQual.EnterVisiable = !type;
            txtMrQual.BackColor = System.Drawing.Color.White;
            //质控医师
            txtQcDocd.ReadOnly = type;
            txtQcDocd.EnterVisiable = !type;
            txtQcDocd.BackColor = System.Drawing.Color.White;
            //质控护士
            txtQcNucd.ReadOnly = type;
            txtQcNucd.EnterVisiable = !type;
            txtQcNucd.BackColor = System.Drawing.Color.White;
            //主任医师
            txtConsultingDoctor.ReadOnly = type;
            txtConsultingDoctor.EnterVisiable = !type;
            txtConsultingDoctor.BackColor = System.Drawing.Color.White;
            //主治医师
            txtAttendingDoctor.ReadOnly = type;
            txtAttendingDoctor.EnterVisiable = !type;
            txtAttendingDoctor.BackColor = System.Drawing.Color.White;
            //住院医师
            txtAdmittingDoctor.ReadOnly = type;
            txtAdmittingDoctor.EnterVisiable = !type;
            txtAdmittingDoctor.BackColor = System.Drawing.Color.White;
            //进修医师
            txtRefresherDocd.ReadOnly = type;
            txtRefresherDocd.EnterVisiable = !type;
            txtRefresherDocd.BackColor = System.Drawing.Color.White;
            //研究生实习医师
            txtGraDocCode.ReadOnly = type;
            txtGraDocCode.EnterVisiable = !type;
            txtGraDocCode.BackColor = System.Drawing.Color.White;
            //质控时间
            txtCheckDate.Enabled = !type;
            //实习医生
            txtPraDocCode.ReadOnly = type;
            txtPraDocCode.EnterVisiable = !type;
            txtPraDocCode.BackColor = System.Drawing.Color.White;
            //编码员
            txtCodingCode.ReadOnly = type;
            txtCodingCode.EnterVisiable = !type;
            txtCodingCode.BackColor = System.Drawing.Color.White;
            //整理员 
            txtCoordinate.ReadOnly = type;
            txtCoordinate.EnterVisiable = !type;
            txtCoordinate.BackColor = System.Drawing.Color.White;
            this.txtOperationCode.ReadOnly = type;
            txtOperationCode.EnterVisiable = !type;
            this.txtOperationCode.BackColor = System.Drawing.Color.White;
            //尸蹇
            cbBodyCheck.Enabled = !type;
            cmbUnit.Enabled = !type;
            //手术、治疗、检查、诊断、是否本院首例
            cbYnFirst.Enabled = !type;
            //随诊
            cbVisiStat.Enabled = !type;
            //随诊期限
            txtVisiPeriWeek.ReadOnly = type;
            txtVisiPeriWeek.BackColor = System.Drawing.Color.White;
            txtVisiPeriMonth.ReadOnly = type;
            txtVisiPeriMonth.BackColor = System.Drawing.Color.White;
            txtVisiPeriYear.ReadOnly = type;
            txtVisiPeriYear.BackColor = System.Drawing.Color.White;
            //示教病案
            cbTechSerc.Enabled = !type;
            //单病种
            cbDisease30.Enabled = !type;
            //血型
            txtBloodType.ReadOnly = type;
            txtBloodType.EnterVisiable = !type;
            txtBloodType.BackColor = System.Drawing.Color.White;
            txtRhBlood.ReadOnly = type;
            txtRhBlood.EnterVisiable = !type;
            txtRhBlood.BackColor = System.Drawing.Color.White;
            //输血反应
            txtReactionBlood.ReadOnly = type;
            txtReactionBlood.EnterVisiable = !type;
            txtReactionBlood.BackColor = System.Drawing.Color.White;
            //红细胞
            txtBloodRed.ReadOnly = type;
            txtBloodRed.BackColor = System.Drawing.Color.White;
            //血小板
            txtBloodPlatelet.ReadOnly = type;
            txtBloodPlatelet.BackColor = System.Drawing.Color.White;
            //血浆
            txtBodyAnotomize.ReadOnly = type;
            txtBodyAnotomize.BackColor = System.Drawing.Color.White;
            //全血
            txtBloodWhole.ReadOnly = type;
            txtBloodWhole.BackColor = System.Drawing.Color.White;
            //其他
            txtBloodOther.ReadOnly = type;
            txtBloodOther.BackColor = System.Drawing.Color.White;
            //院际会诊
            txtInconNum.ReadOnly = type;
            txtInconNum.BackColor = System.Drawing.Color.White;
            //远程会诊
            txtOutconNum.ReadOnly = type;
            txtOutconNum.BackColor = System.Drawing.Color.White;
            //SuperNus 特级护理
            txtSuperNus.ReadOnly = type;
            txtSuperNus.BackColor = System.Drawing.Color.White;
            //一级护理
            txtINus.ReadOnly = type;
            txtINus.BackColor = System.Drawing.Color.White;
            //二级护理
            txtIINus.ReadOnly = type;
            txtIINus.BackColor = System.Drawing.Color.White;
            //三级护理
            txtIIINus.ReadOnly = type;
            txtIIINus.BackColor = System.Drawing.Color.White;
            //重症监护
            txtStrictNuss.ReadOnly = type;
            txtStrictNuss.BackColor = System.Drawing.Color.White;
            //特殊护理
            txtSPecalNus.ReadOnly = type;
            txtSPecalNus.BackColor = System.Drawing.Color.White;
            //ct
            txtCtNumb.ReadOnly = type;
            txtCtNumb.BackColor = System.Drawing.Color.White;
            //UCFT
            txtPathNumb.ReadOnly = type;
            txtPathNumb.BackColor = System.Drawing.Color.White;
            //MR
            txtMriNumb.ReadOnly = type;
            txtMriNumb.BackColor = System.Drawing.Color.White;
            //X光
            txtXNumb.ReadOnly = type;
            txtXNumb.BackColor = System.Drawing.Color.White;
            //B超
            txtBC.Enabled = !type;
            //输入员
            txtInputDoc.ReadOnly = type;
            txtInputDoc.EnterVisiable = !type;
            txtInputDoc.BackColor = System.Drawing.Color.White;

            txtInfectionPositionNew.ReadOnly = type;
            txtInjuryOrPoisoningCause.ReadOnly = type;
            txtInfectionDiseasesReport.ReadOnly = type;
            txtFourDiseasesReport.ReadOnly = type;
            #region {DB330B9D-7C52-40cc-AC23-73A81CA2AF73}
            txtPharmacyAllergic1.ReadOnly = type;
            txtPharmacyAllergic1.EnterVisiable = !type;
            txtPharmacyAllergic1.BackColor = Color.White;

            txtPharmacyAllergic2.ReadOnly = type;
            txtPharmacyAllergic2.EnterVisiable = !type;
            txtPharmacyAllergic2.BackColor = Color.White;

            txtHivAb.ReadOnly = type;
            txtHivAb.EnterVisiable = !type;
            txtHivAb.BackColor = Color.White;

            txtECTNumb.ReadOnly = type;
            txtECTNumb.BackColor = Color.White;

            txtPETNumb.ReadOnly = type;
            txtPETNumb.BackColor = Color.White;
            #endregion
        }
        #endregion

        #region 清空所有数据
        /// <summary>
        /// 清空所有数据
        /// </summary>
        public void ClearInfo()
        {
            try
            {
                this.ucDiagNoseInput1.ClearInfo();
                this.ucOperation1.ClearInfo();
                this.ucTumourCard2.ClearInfo();
                this.ucChangeDept1.ClearInfo();
                this.ucBabyCardInput1.ClearInfo();
                this.ucFeeInfo1.ClearInfo();
                //病案号 
                txtCaseNum.Text = "";
                //住院次数
                txtInTimes.Text = "";
                //费用类别
                txtPactKind.Tag = null;
                //医保号
                txtSSN.Text = "";
                //门诊号
                txtClinicNo.Text = "";
                //姓名
                txtPatientName.Text = "";
                //性别
                txtPatientSex.Tag = null;
                //生日
                //			patientBirthday.Enabled = !type;
                //年龄
                txtPatientAge.Text = "";
                //婚姻
                txtMaritalStatus.Tag = null;
                //职业
                txtProfession.Tag = null;
                //出生地
                txtAreaCode.Text = "";
                //民族
                txtNationality.Tag = null;
                //国籍
                txtCountry.Tag = null;
                //籍贯
                txtDIST.Text = "";
                //身份证
                txtIDNo.Text = "";
                //工作单位
                txtAddressBusiness.Text = "";
                //单位邮编
                txtBusinessZip.Text = "";
                //单位电话
                txtPhoneBusiness.Text = "";
                //户口地址
                txtAddressHome.Text = "";
                //户口邮编
                txtHomeZip.Text = "";
                //家庭电话
                txtPhoneHome.Text = "";
                //联系人 
                txtKin.Text = "";
                //关系
                txtRelation.Tag = null;
                //联系电话
                txtLinkmanTel.Text = "";
                //l联系人地址
                txtLinkmanAdd.Text = "";
                //入院科室
                txtDeptInHospital.Tag = null;
                //入院时间 
                //			Date_In.Enabled = !type;
                //入院情况
                txtCircs.Tag = null;
                //转入科室
                txtFirstDept.Tag = null;
                //转科时间
                dtFirstTime.Value = System.DateTime.Now;
                //转入科室
                txtDeptSecond.Tag = null;
                //转科时间
                dtSecond.Value = System.DateTime.Now;
                //转入科室
                txtDeptThird.Tag = null;
                //转科时间
                dtThird.Value = System.DateTime.Now;
                //出院科室
                txtDeptOut.Tag = null;
                //出院时间
                txtDateOut.Value = System.DateTime.Now;
                //门诊诊断
                txtClinicDiag.Text = "";
                //诊断医生
                txtClinicDocd.Tag = null;
                //住院天数
                txtPiDays.Text = "";
                //确证时间
                txtDiagDate.Value = System.DateTime.Now;
                //入院诊断
                txtRuyuanDiagNose.Text = "";
                //由何医院转来
                txtComeFrom.Text = "";
                //转往何医院
                txtComeTo.Text = "";
                //曾用名
                txtNomen.Text = "";
                //病人来源
                txtInAvenue.Tag = null;
                //院感次数
                txtInfectNum.Text = "";
                //hbsag
                txtHbsag.Tag = null;
                txtHcvAb.Tag = null;
                txtHivAb.Tag = null;
                //门诊与出院
                txtCePi.Tag = null;
                //入院与出院 
                txtPiPo.Tag = null;
                //术前与术后
                txtOpbOpa.Tag = null;
                //临床与病理
                txtClPa.Tag = null;
                //放射与病理
                txtFsBl.Tag = null;
                //抢救次数
                txtSalvTimes.Text = "";
                //成功次数
                txtSuccTimes.Text = "";
                //病案质量
                txtMrQual.Tag = null;
                //质控医师
                txtQcDocd.Tag = null;
                //质控护士
                txtQcNucd.Tag = null;
                //科主任
                txtDeptChiefDoc.Tag = null;
                //主任医师
                txtConsultingDoctor.Tag = null;
                //主治医师
                txtAttendingDoctor.Tag = null;
                //住院医师
                txtAdmittingDoctor.Tag = null;
                //进修医师
                txtRefresherDocd.Tag = null;
                //研究生实习医师
                txtGraDocCode.Tag = null;
                //质控时间
                txtCheckDate.Value = System.DateTime.Now;
                //实习医生
                txtPraDocCode.Tag = null;
                //编码员
                txtCodingCode.Tag = null;
                //整理员 
                txtCoordinate.Tag = null;
                this.txtOperationCode.Tag = null;
                //尸蹇
                cbBodyCheck.Checked = false;
                //手术、治疗、检查、诊断、是否本院首例
                cbYnFirst.Checked = false;
                //随诊
                cbVisiStat.Checked = false;
                //随诊期限
                txtVisiPeriWeek.Text = "";
                txtVisiPeriMonth.Text = "";
                txtVisiPeriYear.Text = "";
                //示教病案
                cbTechSerc.Checked = false;
                //单病种
                cbDisease30.Checked = false;
                //血型
                txtBloodType.Tag = null;
                txtRhBlood.Tag = null;
                //输液反应
                txtReactionTransfuse.Tag = null;
                //输血反应
                txtReactionBlood.Tag = null;
                //红细胞
                txtBloodRed.Text = "";
                //血小板
                txtBloodPlatelet.Text = "";
                //血浆
                txtBodyAnotomize.Text = "";
                //全血
                txtBloodWhole.Text = "";
                //其他
                txtBloodOther.Text = "";
                //院际会诊
                txtInconNum.Text = "";
                //远程会诊
                txtOutconNum.Text = "";
                //SuperNus 特级护理
                txtSuperNus.Text = "";
                //一级护理
                txtINus.Text = "";
                //二级护理
                txtIINus.Text = "";
                //三级护理
                txtIIINus.Text = "";
                //重症监护
                txtStrictNuss.Text = "";
                //特殊护理
                txtSPecalNus.Text = "";
                //ct
                txtCtNumb.Text = "";
                //UCFT
                txtPathNumb.Text = "";
                //MR
                txtMriNumb.Text = "";
                //X光
                txtXNumb.Text = "";
                //B超
                txtBC.Text = "";
                //输入员
                txtInputDoc.Tag = null;
                //感染部位
                this.txtInfectionPositionNew.Text = "";
                //过敏药物1
                this.txtPharmacyAllergic1.Tag = null;
                //过敏药物2
                this.txtPharmacyAllergic2.Tag = null;
                //pet号
                this.txtPETNumb.Text = "";
                //ect号
                this.txtECTNumb.Text = "";
                //病案状态
                this.txtCaseStus.Text = "";
                //损伤中毒原因
                this.txtInjuryOrPoisoningCause.Text = "";
                this.txtInfectionDiseasesReport.Tag = "";
                this.txtFourDiseasesReport.Tag = null;
                this.txtRemark.Text = "";
                //过敏药物
                neuTxtPharmacyAllergic1.Text = "";
                neuTxtPharmacyAllergic2.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 打印病案首页
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int PrintInterface()
        {
            frmPrintCase frmPrint= new frmPrintCase(this);
            frmPrint.ShowDialog();
            return 0;
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public  int PrintCase(string Type)
        {
            string tips = string.Empty;
            if (this.ContrastInfo(ref tips) == -1)
            {
                MessageBox.Show(tips+ "发生变化，请保存再打印！", "提示");
                return -1;
            }

            switch (Type)
            {
                case "Print":
                    this.Print(null, null);
                    break;
                case "PrintBack":
                    this.PrintBack(null);
                    break;
                case "PrintPreview":
                    this.PrintPreview(null, null);
                    break;
                case "PrintBackPreview":
                    this.PrintBackPreview(null, null);
                    break;
            }
            return 0;
        }
        /// <summary>
        /// 打印第一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Print(object sender, object neuObject)
        {
            if (this.healthRecordPrint == null)
            {
                this.healthRecordPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface)) as FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface;
                if (this.healthRecordPrint == null)
                {
                    MessageBox.Show("获得接口IExamPrint错误\n，可能没有维护相关的打印控件或打印控件没有实现接口IExamPrint\n请与系统管理员联系。");
                    return -1;
                }
            }
            this.healthRecordPrint.ControlValue(this.CaseBase);
            this.healthRecordPrint.Print();
            return 1;

        }

        /// <summary>
        /// 打印背面
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int PrintBack(FS.HISFC.Models.HealthRecord.Base obj)
        {
            //反面赋值

            if (this.healthRecordPrintBack == null)
            {
                this.healthRecordPrintBack = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(ucCaseMainInfo), typeof(FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceBack)) as FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceBack;
                if (this.healthRecordPrintBack == null)
                {
                    MessageBox.Show("获得接口IExamPrint错误\n，可能没有维护相关的打印控件或打印控件没有实现接口IExamPrint\n请与系统管理员联系。");
                    return -1;
                }
            }
            if (CaseBase == null)
            {
                return -1;
            }

            this.healthRecordPrintBack.ControlValue(this.CaseBase);
            this.healthRecordPrintBack.Print();
            return 1;
        }
        /// <summary>
        /// 打印预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int PrintPreview(object sender, object neuObject)
        {
            if (this.healthRecordPrint == null)
            {
                this.healthRecordPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface)) as FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface;
                if (this.healthRecordPrint == null)
                {
                    MessageBox.Show("获得接口IExamPrint错误\n，可能没有维护相关的打印控件或打印控件没有实现接口IExamPrint\n请与系统管理员联系。");
                    return -1;
                }
            }

            if (this.CaseBase == null)
            {
                return -1;
            }
            this.healthRecordPrint.ControlValue(this.CaseBase);
            this.healthRecordPrint.PrintPreview();
            return 0;
        }
        /// <summary>
        /// 打印背面预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public int PrintBackPreview(object sender, object neuObject)
        {
            if (this.healthRecordPrintBack == null)
            {
                this.healthRecordPrintBack = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(ucCaseMainInfo), typeof(FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceBack)) as FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceBack;               
                if (this.healthRecordPrintBack == null)
                {
                    MessageBox.Show("获得接口IExamPrint错误\n，可能没有维护相关的打印控件或打印控件没有实现接口IExamPrint\n请与系统管理员联系。");
                    return -1;
                }
            }

            if (this.CaseBase == null)
            {
                return -1;
            }

            this.healthRecordPrintBack.ControlValue(this.CaseBase);
            this.healthRecordPrintBack.PrintPreview();
            return 0;
        }
        /// <summary>
        /// 对比界面与数据库是否一致
        /// </summary>
        /// <returns></returns>
        private int ContrastInfo(ref string Tips)
        {
            FS.HISFC.Models.HealthRecord.Base dataBaseInfo = baseDml.GetCaseBaseInfo(this.CaseBase.PatientInfo.ID);
            FS.HISFC.Models.HealthRecord.Base info = new FS.HISFC.Models.HealthRecord.Base();
            int i = this.GetInfoFromPanel(info);
            int ret = 0;
            if (dataBaseInfo.PatientInfo.ID != info.PatientInfo.ID)//住院流水号
            {
                Tips = "住院流水号、";
            }

            if(dataBaseInfo.PatientInfo.PID.PatientNO!=info.PatientInfo.PID.PatientNO)//住院病历号
            {
                Tips += "住院病历号、";
            }
            if(dataBaseInfo.PatientInfo.PID.CardNO!=info.PatientInfo.PID.CardNO)//卡号
            {
                Tips += "卡号、";
            }
            if(dataBaseInfo.PatientInfo.Name!=info.PatientInfo.Name)//姓名
            {
                Tips += "姓名、";
            }

            if(dataBaseInfo.PatientInfo.Sex.ID.ToString()!=info.PatientInfo.Sex.ID.ToString())//性别
            {
                Tips += "性别、";
            }
            if (dataBaseInfo.PatientInfo.Birthday.Date != info.PatientInfo.Birthday.Date)//出生日期
            {
                Tips += "出生日期、";
            }
            if (dataBaseInfo.PatientInfo.Country.ID != info.PatientInfo.Country.ID)//国家
            {
                Tips += "国家、";
            }
            if (dataBaseInfo.PatientInfo.Nationality.ID != info.PatientInfo.Nationality.ID)//民族
            {
                Tips += "民族、";
            }
            if (dataBaseInfo.PatientInfo.Profession.ID != info.PatientInfo.Profession.ID)//职业
            {
                Tips += "职业、";
            }
            if (dataBaseInfo.PatientInfo.BloodType.ID.ToString().Trim() != info.PatientInfo.BloodType.ID.ToString().Trim())//血型编码
            {
                Tips += "血型、";
            }
            if (dataBaseInfo.PatientInfo.MaritalStatus.ID.ToString().Trim() != info.PatientInfo.MaritalStatus.ID.ToString().Trim())//婚否
            {
                Tips += "婚姻、";
            }
            if (dataBaseInfo.PatientInfo.IDCard.ToString().Trim() != info.PatientInfo.IDCard.ToString().Trim())//身份证号
            {
                Tips += "身份证号、";
            }
            if (dataBaseInfo.PatientInfo.PVisit.InSource.ID != info.PatientInfo.PVisit.InSource.ID)//地区来源
            {
                Tips += "地区来源、";
            }
            if (dataBaseInfo.PatientInfo.Pact.ID != info.PatientInfo.Pact.ID)//合同代码
            {
                Tips += "付款方式、";
            }

            if (dataBaseInfo.PatientInfo.SSN != info.PatientInfo.SSN)//医保公费号
            {
                Tips += "医保号、";
            }
            if (dataBaseInfo.PatientInfo.DIST != info.PatientInfo.DIST)//籍贯
            {
                Tips += "籍贯、";
            }
            if (dataBaseInfo.PatientInfo.AreaCode != info.PatientInfo.AreaCode)//出生地
            {
                Tips += "出生地、";
            }
            if (dataBaseInfo.PatientInfo.AddressHome != info.PatientInfo.AddressHome)//家庭住址
            {
                Tips += "家庭住址、";
            }
            if (dataBaseInfo.PatientInfo.PhoneHome != info.PatientInfo.PhoneHome)//家庭电话
            {
                Tips += "家庭电话、";
            }
            if (dataBaseInfo.PatientInfo.HomeZip != info.PatientInfo.HomeZip)//住址邮编
            {
                Tips += "住址邮编、";
            }
            if (dataBaseInfo.PatientInfo.AddressBusiness != info.PatientInfo.AddressBusiness)//单位地址
            {
                Tips += "单位地址、";
            }
            if (dataBaseInfo.PatientInfo.PhoneBusiness != info.PatientInfo.PhoneBusiness)//单位电话
            {
                Tips += "单位电话、";
            }
            if (dataBaseInfo.PatientInfo.BusinessZip != info.PatientInfo.BusinessZip)//单位邮编
            {
                Tips += "单位邮编、";
            }
            if (dataBaseInfo.PatientInfo.Kin.Name != info.PatientInfo.Kin.Name)//联系人
            {
                Tips += "联系人、";
            }
            if (dataBaseInfo.PatientInfo.Kin.RelationLink != info.PatientInfo.Kin.RelationLink)//与患者关系
            {
                Tips += "与患者关系、";
            }
            if (dataBaseInfo.PatientInfo.Kin.RelationPhone != info.PatientInfo.Kin.RelationPhone)//联系电话
            {
                Tips += "联系电话、";
            }
            if (dataBaseInfo.PatientInfo.Kin.RelationAddress != info.PatientInfo.Kin.RelationAddress)//联系地址
            {
                Tips += "联系地址、";
            }
            if (dataBaseInfo.ClinicDoc.ID != info.ClinicDoc.ID)//门诊诊断医生
            {
                Tips += "门诊诊断医生、";
            }

            if (dataBaseInfo.ComeFrom != info.ComeFrom)//转来医院
            {
                Tips += "转来医院、";
            }
            if (dataBaseInfo.PatientInfo.PVisit.InTime.Date != info.PatientInfo.PVisit.InTime.Date)//入院日期
            {
                Tips += "入院日期、";
            }
            if (dataBaseInfo.PatientInfo.InTimes != info.PatientInfo.InTimes)//住院次数
            {
                Tips += "住院次数、";
            }
            if (dataBaseInfo.InDept.ID != info.InDept.ID)//入院科室代码
            {
                Tips += "入院科室、";
            }

            if (dataBaseInfo.PatientInfo.PVisit.InSource.ID != info.PatientInfo.PVisit.InSource.ID)//入院来源
            {
                Tips += "入院来源、";
            }
            if (dataBaseInfo.PatientInfo.PVisit.Circs.ID != info.PatientInfo.PVisit.Circs.ID)//入院状态
            {
                Tips += "入院状态、";
            }
            if (dataBaseInfo.DiagDate.Date != info.DiagDate.Date)//确诊日期
            {
                Tips += "确诊日期、";
            }

            if (dataBaseInfo.PatientInfo.PVisit.OutTime.Date != info.PatientInfo.PVisit.OutTime.Date)//出院日期
            {
                Tips += "出院日期、";
            }
            if (dataBaseInfo.OutDept.ID != info.OutDept.ID)//出院科室代码
            {
                Tips += "出院科室、";
            }

            if (dataBaseInfo.DiagDays != info.DiagDays)//确诊天数
            {
                Tips += "确诊天数、";
            }
            if (dataBaseInfo.InHospitalDays != info.InHospitalDays)//住院天数
            {
                Tips += "住院天数、";
            }


            if (dataBaseInfo.CadaverCheck != info.CadaverCheck)//尸检
            {
                Tips += "尸检、";
            }
            if (dataBaseInfo.Hbsag != info.Hbsag)//乙肝表面抗原
            {
                Tips += "Hbsag、";
            }
            if (dataBaseInfo.HcvAb != info.HcvAb)//丙肝病毒抗体
            {
                Tips += "HcvAb、";
            }
            if (dataBaseInfo.HivAb != info.HivAb)//获得性人类免疫缺陷病毒抗体
            {
                Tips += "HivAb、";
            }
            if (dataBaseInfo.CePi != info.CePi)//门急_入院符合
            {
                Tips += "门诊与入院符合、";
            }
            if (dataBaseInfo.PiPo != info.PiPo)//入出_院符合
            {
                Tips += "入院与出院符合、";
            }
            if (dataBaseInfo.OpbOpa != info.OpbOpa)//术前_后符合
            {
                Tips += "术前与术后符合、";
            }

            if (dataBaseInfo.ClPa != info.ClPa)//临床_病理符合
            {
                Tips += "临床与病理符合、";
            }
            if (dataBaseInfo.FsBl != info.FsBl)//放射_病理符合
            {
                Tips += "放射与病理符合、";
            }
            if (dataBaseInfo.SalvTimes != info.SalvTimes)//抢救次数
            {
                Tips += "抢救次数、";
            }
            if (dataBaseInfo.SuccTimes != info.SuccTimes)//成功次数
            {
                Tips += "成功次数、";
            }
            if (dataBaseInfo.TechSerc != info.TechSerc)//示教科研
            {
                Tips += "示教科研、";
            }
            if (dataBaseInfo.VisiStat != info.VisiStat)//是否随诊
            {
                Tips += "随诊、";
            }

            if (dataBaseInfo.InconNum != info.InconNum)//院际会诊次数 70 远程会诊次数
            {
                Tips += "院际会诊次数、";
            }
            if (dataBaseInfo.OutconNum != info.OutconNum)//院际会诊次数 70 远程会诊次数
            {
                Tips += "远程会诊次数、";
            }

            if (dataBaseInfo.FirstAnaphyPharmacy.ID != info.FirstAnaphyPharmacy.ID)//过敏药物名称
            {
                Tips += "过敏药物名称、";
            }
            if (dataBaseInfo.PatientInfo.PVisit.AdmittingDoctor.ID != info.PatientInfo.PVisit.AdmittingDoctor.ID)//住院医师代码
            {
                Tips += "住院医师项、";
            }


            if (dataBaseInfo.PatientInfo.PVisit.AttendingDoctor.ID != info.PatientInfo.PVisit.AttendingDoctor.ID)//主治医师代码
            {
                Tips += "主治医师项、";
            }


            if (dataBaseInfo.PatientInfo.PVisit.ConsultingDoctor.ID != info.PatientInfo.PVisit.ConsultingDoctor.ID)//主任医师代码
            {
                Tips += "主任医师项、";
            }


            if (dataBaseInfo.PatientInfo.PVisit.ReferringDoctor.ID != info.PatientInfo.PVisit.ReferringDoctor.ID)//科主任代码
            {
                Tips += "科主任项、";
            }

            if (dataBaseInfo.RefresherDoc.ID != info.RefresherDoc.ID)//进修医师代码
            {
                Tips += "进修医师项、";
            }

            if (dataBaseInfo.GraduateDoc.ID != info.GraduateDoc.ID)//研究生实习医师代码
            {
                Tips += "研究生实习医师项、";
            }
            if (dataBaseInfo.PatientInfo.PVisit.TempDoctor.ID != info.PatientInfo.PVisit.TempDoctor.ID)//实习医师代码
            {
                Tips += "实习医师项、";
            }

            if (dataBaseInfo.MrQuality != info.MrQuality)//病案质量
            {
                Tips += "病案质量、";
            }

            if (dataBaseInfo.QcDoc.ID != info.QcDoc.ID)//质控医师代码
            {
                Tips += "质控医师项、";
            }

            if (dataBaseInfo.QcNurse.ID != info.QcNurse.ID)//质控护士代码
            {
                Tips += "质控护士项、";
            }

            if (dataBaseInfo.CheckDate.Date != info.CheckDate.Date)//检查时间
            {
                Tips += "质控日期项、";
            }
            if (dataBaseInfo.YnFirst != info.YnFirst)//手术操作治疗检查诊断为本院第一例项目
            {
                Tips += "手术操作治疗检查诊断为本院第一例项目、";
            }
            if (dataBaseInfo.RhBlood != info.RhBlood)//Rh血型(阴阳)
            {
                Tips += "Rh血型、";
            }
            if (dataBaseInfo.ReactionBlood != info.ReactionBlood)//输血反应（有无）
            {
                Tips += "输血反应、";
            }
            if (dataBaseInfo.BloodRed != info.BloodRed)//红细胞数
            {
                Tips += "红细胞数、";
            }
            if (dataBaseInfo.BloodPlatelet != info.BloodPlatelet)//血小板数
            {
                Tips += "血小板数、";
            }
            if (dataBaseInfo.BloodPlasma != info.BloodPlasma)//血浆数
            {
                Tips += "血浆数、";
            }
            if (dataBaseInfo.BloodWhole != info.BloodWhole)//全血数
            {
                Tips += "全血数、";
            }
            if (dataBaseInfo.BloodOther != info.BloodOther)//其他输血数
            {
                Tips += "其他输血数、";
            }

            if (dataBaseInfo.VisiPeriodWeek != info.VisiPeriodWeek) //随访期限 周
            {
                Tips += "随访期限-周、";
            }
            if (dataBaseInfo.VisiPeriodMonth != info.VisiPeriodMonth) //随访期限 月
            {
                Tips += "随访期限-月、";
            }
            if (dataBaseInfo.VisiPeriodYear != info.VisiPeriodYear)//随访期限 年
            {
                Tips += "随访期限-年、";
            }
            if (dataBaseInfo.SpecalNus != info.SpecalNus)  // 特殊护理(日)  
            {
                Tips += "特殊护理、";
            }
            if (dataBaseInfo.INus != info.INus) //I级护理时间(日)  
            {
                Tips += "I级护理、";
            }
            if (dataBaseInfo.IINus != info.IINus) //II级护理时间(日)   
            {
                Tips += "II级护理、";
            }
            if (dataBaseInfo.IIINus != info.IIINus) //III级护理时间(日)
            {
                Tips += "III级护理、";
            }
            if (dataBaseInfo.StrictNuss != info.StrictNuss) //重症监护时间( 小时)  
            {
                Tips += "重症监护、";
            }
            if (dataBaseInfo.SuperNus != info.SuperNus) //特级护理时间(小时)   
            {
                Tips += "特级护理、";
            }
            if (dataBaseInfo.ClinicDiag.Name != info.ClinicDiag.Name)
            {
                Tips += "门诊诊断、";
            }

            if (dataBaseInfo.InHospitalDiag.Name != info.InHospitalDiag.Name)
            {
                Tips += "入院诊断、";
            }

            if (dataBaseInfo.InfectionPosition.Name != info.InfectionPosition.Name) //院内感染部位名称
            {
                Tips += "院内感染、";
            }
            if (dataBaseInfo.InfectionPosition.Memo != info.InfectionPosition.Memo)//损伤中毒的外部因素编码-广医2010-2-2
            {
                Tips += "损伤中毒的外部因素";
            }

            if (dataBaseInfo.OutDept.Memo != info.OutDept.Memo)//转往何医院
            {
                Tips += "转往何医院";
            }
            if (Tips != "")
            {
                ret = -1;
            }
            else
            {
                ret = 1;
            }
            return ret;
        }
        /// <summary>
        /// 退出是否保存
        /// </summary>
        public void IsNeedSaveCheck()
        {
            string tips = string.Empty;
            if (this.ContrastInfo(ref tips) == -1)
            {
                if (MessageBox.Show(tips + "发生变化，请保存再打印！", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    this.Save(0);
                }
            }
        }
        #endregion

        #region 校验诊断约束
        /// <summary>
        /// 
        /// </summary>
        /// <param name="diag"></param>
        /// <returns></returns>
        public int DiagValueState(FS.HISFC.BizLogic.HealthRecord.Diagnose diag)
        {
            ArrayList allList = new ArrayList();
            this.ucDiagNoseInput1.GetAllDiagnose(allList);
            if (allList == null)
            {
                return -1;
            }
            if (allList.Count == 0)
            {
                return 1;
            }
            FS.HISFC.Models.Base.EnumSex sex;
            if (CaseBase.PatientInfo.Sex.ID.ToString() == "F")
            {
                sex = FS.HISFC.Models.Base.EnumSex.F;
            }
            else if (CaseBase.PatientInfo.Sex.ID.ToString() == "M")
            {
                sex = FS.HISFC.Models.Base.EnumSex.M;
            }
            else
            {
                sex = FS.HISFC.Models.Base.EnumSex.U;
            }
            //待定
            ArrayList diagCheckList = diag.QueryDiagnoseValueState(allList, sex);
            ucDiagnoseCheck ucdia = new ucDiagnoseCheck();
            if (diagCheckList == null)
            {
                MessageBox.Show("提取约束出错");
                return -1;
            }
            if (diagCheckList.Count == 0)
            {
                return 1;
            }
            try
            {
                if (frm != null)
                {
                    frm.Close();
                }
            }
            catch { }

            frm = new ucDiagNoseCheck();
            frm.initDiangNoseCheck(diagCheckList);
            if (frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC)
            {
                frm.Show();
                if (frm.GetRedALarm())
                {
                    return -1;
                }
            }
            //			else if(frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS)
            //			{
            //				frm.ShowDialog();
            //				if(frm.GetRedALarm() )
            //				{
            //					return -1;
            //				}
            //			}
            return 1;
        }
        #endregion

        #region 获取出生日期
        private void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.dtPatientBirthday.ValueChanged -= new EventHandler(txBirth_ValueChanged);
            //this.getBirthday();
            //this.dtPatientBirthday.ValueChanged += new EventHandler(txBirth_ValueChanged);
        }

        #region 校验事件是否合理
        private void txBirth_ValueChanged(object sender, System.EventArgs e)
        {
            //DateTime dtNow = this.baseDml.GetDateTimeFromSysDateTime();

            //DateTime dtBirth = this.dtPatientBirthday.Value;

            //if (dtBirth > dtNow)
            //{
            //    dtBirth = dtNow;
            //    //MessageBox.Show("出生日期不能大于系统日期！");
            //    return;
            //}

            //int years = 0;

            //System.TimeSpan span = new TimeSpan(dtNow.Ticks - dtBirth.Ticks);

            //years = span.Days / 365;

            //if (years <= 0)
            //{
            //    int month = span.Days / 30;

            //    if (month <= 0)
            //    {
            //        this.txtPatientAge.Text = span.Days.ToString();
            //        this.cmbUnit.SelectedIndex = 2;
            //    }
            //    else
            //    {
            //        this.txtPatientAge.Text = month.ToString();
            //        this.cmbUnit.SelectedIndex = 1;
            //    }
            //}
            //else
            //{
            //    this.txtPatientAge.Text = years.ToString();
            //    this.cmbUnit.SelectedIndex = 0;
            //}
        }
        #endregion
        /// <summary>
        /// 获取出生日期
        /// </summary>
        private void getBirthday()
        {
            //string age = this.txtPatientAge.Text.Trim();
            //int i = 0;

            //if (age == "") age = "0";

            //try
            //{
            //    i = int.Parse(age);
            //}
            //catch (Exception e)
            //{
            //    string error = e.Message;
            //    MessageBox.Show("输入年龄不正确,请重新输入!", "提示");
            //    this.txtPatientAge.Focus();
            //    return;
            //}
            //DateTime birthday = DateTime.MinValue;

            //this.getBirthday(i, this.cmbUnit.Text, ref birthday);

            //if (birthday <= this.dtPatientBirthday.MinDate)
            //{
            //    this.txtPatientAge.Focus();
            //    return;
            //}

            ////this.dtBirthday.Value = birthday ;

            //if (this.cmbUnit.Text == "岁")
            //{

            //    //数据库中存的是出生日期,如果年龄单位是岁,并且算出的出生日期和数据库中出生日期年份相同
            //    //就不进行重新赋值,因为算出的出生日期生日为当天,所以以数据库中为准

            //    if (this.dtPatientBirthday.Value.Year != birthday.Year)
            //    {
            //        this.dtPatientBirthday.Value = birthday;
            //    }
            //}
            //else
            //{
            //    this.dtPatientBirthday.Value = birthday;
            //}
        }
        #region 根据年龄得到出生日期
        /// <summary>
        /// 根据年龄得到出生日期
        /// </summary>
        /// <param name="age"></param>
        /// <param name="ageUnit"></param>
        /// <param name="birthday"></param>
        private void getBirthday(int age, string ageUnit, ref System.DateTime birthday)
        {
            DateTime current = this.baseDml.GetDateTimeFromSysDateTime();

            if (ageUnit == "岁")
            {
                birthday = current.AddYears(-age);
            }
            else if (ageUnit == "月")
            {
                birthday = current.AddMonths(-age);
            }
            else if (ageUnit == "天")
            {
                birthday = current.AddDays(-age);
            }
        }
        #endregion
        #endregion

        #region 设置手工录入
        private void SetHandcraft()
        {
            this.CaseBase = new FS.HISFC.Models.HealthRecord.Base();
            string strCaseNO = this.baseDml.GetCaseInpatientNO();
            if (strCaseNO == null || strCaseNO == "")
            {
                MessageBox.Show("获取住院流水号失败" + baseDml.Err);
                CaseBase = new FS.HISFC.Models.HealthRecord.Base();
                return;
            }
            CaseBase.PatientInfo.ID = strCaseNO;
            CaseBase.IsHandCraft = "1";
            ucFeeInfo1.BoolType = true;
            ucFeeInfo1.LoadInfo(CaseBase.PatientInfo);
            HandCraft = 1;
        }
        #endregion

        #region  按病案号查询
        private void txtCaseNOSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                try
                {
                    if (txtCaseNOSearch.Text == "")
                    {
                        MessageBox.Show("请输入病案号");
                        return;
                    }
                    else
                    {
                        txtCaseNOSearch.Text = txtCaseNOSearch.Text.Trim().PadLeft(10, '0');
                    }

                    #region 清空
                    HandCraft = 0;
                    this.CaseBase = null;
                    ClearInfo();
                    #endregion
                    if (!this.ucPatient.Visible)
                    {
                        #region 查询
                        ArrayList list = null;
                        list = ucPatient.Init(txtCaseNOSearch.Text, "1");
                        if (list == null)
                        {
                            MessageBox.Show("查询失败" + ucPatient.strErr);
                            return;
                        }
                        if (list.Count == 0)
                        {
                            if (frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS)
                            {
                                #region 病案室自己手工录入病案
                                if (MessageBox.Show("没有查到相关病案信息,是否手工录入病案", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    txtCaseNum.Text = txtCaseNOSearch.Text;
                                    txtCaseNum.Focus();
                                    SetHandcraft();
                                }
                                else
                                {
                                    return;
                                }
                                #endregion
                            }
                            else
                            {
                                MessageBox.Show("没有查到相关病案信息");
                                return;
                            }
                        }
                        else if (list.Count == 1) //只有一条病案信息
                        {
                            ucPatient.Visible = false;
                            FS.HISFC.Models.HealthRecord.Base obj = this.ucPatient.GetCaseInfo();
                            if (obj != null)
                            {
                                LoadInfo(obj.PatientInfo.ID, this.frmType);
                            }
                        }
                        else
                        {
                            ucPatient.Visible = true;
                        }
                        #endregion
                    }
                    else
                    {
                        #region  选择
                        FS.HISFC.Models.HealthRecord.Base obj = this.ucPatient.GetCaseInfo();
                        if (obj != null)
                        {
                            LoadInfo(obj.PatientInfo.ID, this.frmType);
                        }
                        this.ucPatient.Visible = false;
                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                ucPatient.PriorRow();
            }
            else if (e.KeyCode == Keys.Down)
            {
                ucPatient.NextRow();
            }
        }
        #endregion

        #region 按住院号查询
        private void txtPatientNOSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                try
                {
                    if (txtPatientNOSearch.Text == "")
                    {
                        MessageBox.Show("请输入住院号");
                        return;
                    }
                    else
                    {
                        txtPatientNOSearch.Text = txtPatientNOSearch.Text.Trim().PadLeft(10, '0');
                    }
                    #region 清空
                    HandCraft = 0;
                    this.CaseBase = null;
                    ClearInfo();
                    #endregion
                    if (!this.ucPatient.Visible)
                    {
                        #region 查询
                        ArrayList list = null;
                        list = ucPatient.Init(txtPatientNOSearch.Text, "2");
                        if (list == null)
                        {
                            MessageBox.Show("查询失败" + ucPatient.strErr);
                            return;
                        }
                        if (list.Count == 0)
                        {
                            if (frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS)
                            {
                                #region 病案室自己手工录入病案
                                if (MessageBox.Show("没有查到相关病案信息,是否手工录入病案", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    SetHandcraft();
                                }
                                else
                                {
                                    return;
                                }
                                #endregion
                            }
                            else
                            {
                                MessageBox.Show("没有查到相关病案信息");
                                return;
                            }
                        }
                        else if (list.Count == 1) //只有一条信息
                        {
                            ucPatient.Visible = false;
                            FS.HISFC.Models.HealthRecord.Base obj = this.ucPatient.GetCaseInfo();
                            if (obj != null)
                            {
                                LoadInfo(obj.PatientInfo.ID, this.frmType);
                                this.txtCaseNum.Focus();
                            }
                        }
                        else //多条信息 
                        {
                            ucPatient.Visible = true;
                        }
                        #endregion
                    }
                    else
                    {
                        #region  选择
                        FS.HISFC.Models.HealthRecord.Base obj = this.ucPatient.GetCaseInfo();
                        if (obj != null)
                        {
                            LoadInfo(obj.PatientInfo.ID, this.frmType);
                            this.txtCaseNum.Focus();
                        }
                        this.ucPatient.Visible = false;
                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                ucPatient.PriorRow();
            }
            else if (e.KeyCode == Keys.Down)
            {
                ucPatient.NextRow();
            }
        }
        #endregion 按住院号查询

        #region 双击树的节点
        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.treeView1.SelectedNode.Level == 0)
            {
                return;
            }

            try
            {
                if (this.treeView1.SelectedNode.Text == "最近出院患者")
                {
                    return;
                }
                FS.HISFC.Models.RADT.PatientInfo pa = (FS.HISFC.Models.RADT.PatientInfo)treeView1.SelectedNode.Tag;
                LoadInfo(pa.ID, this.frmType);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 查询患者联系人信息

        #region {E9F858A6-BDBC-4052-BA57-68755055FB80}

        /// <summary>
        /// 根据住院号或病历号查询患者联系人列表
        /// </summary>
        /// <param name="patientNo">住院号</param>
        /// <param name="cardNo">病历号</param>
        private void InitLinkWay(string patientNo, string cardNo)
        {
            if (string.IsNullOrEmpty(patientNo))
            {
                patientNo = this.PatientNo;
            }

            if (string.IsNullOrEmpty(cardNo))
            {
                cardNo = this.CardNo;
            }

            ArrayList list = new ArrayList();

            list = linkWayManager.QueryLinkWay(patientNo, cardNo);
            if (list == null)
            {
                return;
            }
            neuSpread1_Sheet1.Rows.Count = list.Count;
            for (int i = 0; i < list.Count; i++)
            {
                FS.HISFC.Models.HealthRecord.Visit.LinkWay linkWayObj
                    = list[i] as FS.HISFC.Models.HealthRecord.Visit.LinkWay;


                if (linkWayObj != null)
                {
                    this.neuSpread1_Sheet1.Cells[i, 1].Text = linkWayObj.Name;//联系人
                    this.neuSpread1_Sheet1.Cells[i, 2].Text = linkWayObj.Memo;//与患者关系
                    this.neuSpread1_Sheet1.Cells[i, 3].Text = linkWayObj.Phone;//联系电话
                    this.neuSpread1_Sheet1.Cells[i, 4].Text = linkWayObj.User01;//电话状态
                    this.neuSpread1_Sheet1.Cells[i, 5].Text = linkWayObj.Address;//联系地址
                    this.neuSpread1_Sheet1.Cells[i, 6].Text = linkWayObj.Mail;//电子邮件

                    this.neuSpread1_Sheet1.Rows[i].Tag = linkWayObj;

                }
            }

        }

        private void cmsMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "新增":
                    AddNewLinkRow();
                    break;
                case "保存":
                    SaveLinkRow();
                    break;
                case "删除":
                    DeleteLinkRow();
                    break;
            }
        }

        /// <summary>
        /// 添加联系人方法
        /// </summary>
        /// <returns>成功返回 0;失败返回 -1</returns>
        private int AddNewLinkRow()
        {
            try
            {
                int RowCount = this.neuSpread1_Sheet1.Rows.Count;
                this.neuSpread1_Sheet1.Rows.Add(RowCount, 1);
                this.neuSpread1_Sheet1.ActiveRowIndex = this.neuSpread1_Sheet1.Rows.Count;
                neuSpread1.ShowActiveCell(FarPoint.Win.Spread.VerticalPosition.Center, FarPoint.Win.Spread.HorizontalPosition.Center);
                this.neuSpread1_Sheet1.SetActiveCell(RowCount, 0);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// 保存联系人列表
        /// </summary>
        /// <returns></returns>
        private int SaveLinkRow()
        {

            linkWayManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
            {
                FS.HISFC.Models.HealthRecord.Visit.LinkWay linkWayObj1 =
                    neuSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.HealthRecord.Visit.LinkWay;//Tag中的对象

                FS.HISFC.Models.HealthRecord.Visit.LinkWay linkWayObj2 = GetLinkWayObj(i); //根据行的值生成的对象

                if (linkWayObj1 == null)
                {
                    //新增联系人
                    if (linkWayManager.InsertLinkWay(linkWayObj2) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        //t.RollBack();
                        MessageBox.Show("联系人列表保存发生错误:" + linkWayManager.Err);
                        return -1;
                    }
                }
                else if (linkWayObj1 != linkWayObj2)
                {
                    //更新联系人
                    linkWayObj2.ID = linkWayObj1.ID;
                    if (linkWayManager.UpdateInsertLinkWay(linkWayObj2) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        //t.RollBack();
                        MessageBox.Show("联系人列表保存发生错误:" + linkWayManager.Err);
                        return -1;
                    }
                }

            }

            FS.FrameWork.Management.PublicTrans.Commit();

            if (CaseBase != null)
            {
                InitLinkWay(CaseBase.PatientInfo.PID.PatientNO, CaseBase.PatientInfo.PID.CardNO);
            }

            return 0;
        }

        /// <summary>
        /// 删除联系人信息
        /// </summary>
        /// <returns></returns>
        private int DeleteLinkRow()
        {
            if (MessageBox.Show("是否删除选择的联系人信息", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                == DialogResult.Yes)
            {

                linkWayManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
                {
                    if (this.neuSpread1_Sheet1.Cells[i, 0].Text == true.ToString())
                    {
                        FS.HISFC.Models.HealthRecord.Visit.LinkWay linkWayObj1 =
                        neuSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.HealthRecord.Visit.LinkWay;//Tag中的对象
                        if (linkWayObj1 != null)
                        {
                            if (linkWayManager.DelLinkWay(linkWayObj1) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                //t.RollBack();
                                MessageBox.Show("删除联系人发生错误:" + linkWayManager.Err);
                                return -1;
                            }
                        }
                    }
                }

                FS.FrameWork.Management.PublicTrans.Commit();

                //重新加载联系人列表
                InitLinkWay(CaseBase.PatientInfo.PID.PatientNO, CaseBase.PatientInfo.PID.CardNO);
            }

            return 0;
        }

        /// <summary>
        /// 根据行索引获取对象
        /// </summary>
        /// <param name="index">FarPoint行索引</param>
        /// <returns></returns>
        private FS.HISFC.Models.HealthRecord.Visit.LinkWay GetLinkWayObj(int index)
        {
            FS.HISFC.Models.HealthRecord.Visit.LinkWay linkWayObj
            = new FS.HISFC.Models.HealthRecord.Visit.LinkWay();
            linkWayObj.Name = this.neuSpread1_Sheet1.Cells[index, 1].Text;//联系人
            linkWayObj.Memo = this.neuSpread1_Sheet1.Cells[index, 2].Text;  //与患者关系
            linkWayObj.Phone = this.neuSpread1_Sheet1.Cells[index, 3].Text;//联系电话
            linkWayObj.User01 = this.neuSpread1_Sheet1.Cells[index, 4].Text;//电话状态
            linkWayObj.Address = this.neuSpread1_Sheet1.Cells[index, 5].Text;//联系地址
            linkWayObj.Mail = this.neuSpread1_Sheet1.Cells[index, 6].Text;//电子邮件

            linkWayObj.Patient = CaseBase.PatientInfo;

            if (string.IsNullOrEmpty(linkWayObj.Patient.PID.CardNO))
            {
                linkWayObj.Patient.PID.CardNO = this.CardNo;
            }

            if (string.IsNullOrEmpty(linkWayObj.Patient.PID.PatientNO))
            {
                linkWayObj.Patient.PID.PatientNO = this.PatientNo;
            }


            return linkWayObj;
        }




        #endregion

        #endregion

        #region {E9F858A6-BDBC-4052-BA57-68755055FB80}

        private void neuCheckBoxIsDead_CheckedChanged(object sender, EventArgs e)
        {
            this.neuDateTimePickerDeadTime.Enabled = this.neuCheckBoxIsDead.Checked;
            this.neuComboBoxDeadReason.Enabled = this.neuCheckBoxIsDead.Checked;
            if (this.neuCheckBoxIsDead.Checked)
            {
                this.neuDateTimePickerDeadTime.Value = visitIntergrate.GetCurrentDateTime();
            }
            else
            {
                this.neuDateTimePickerDeadTime.Value = this.neuDateTimePickerDeadTime.MinDate;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveLinkRow() == -1)
            {
                MessageBox.Show("联系人信息保存失败");
                return;
            }

            if (cmbLinkType.SelectedIndex == -1)
            {
                MessageBox.Show("请选择随访方式");
                return;
            }

            if (cmbResult.SelectedIndex == -1)
            {
                MessageBox.Show("请选择随访结果");
                return;
            }

            //随访明细记录实体
            FS.HISFC.Models.HealthRecord.Visit.VisitRecord visitRecord
                = new FS.HISFC.Models.HealthRecord.Visit.VisitRecord();

            //随访主记录实体
            FS.HISFC.Models.HealthRecord.Visit.Visit visit = new FS.HISFC.Models.HealthRecord.Visit.Visit();

            string seqNo = visitRecordManager.GetVisitRecordSequ();

            if (seqNo == null)
            {
                MessageBox.Show(visitRecordManager.Err);
                return;
            }
            if (GetVisitRecordObj(ref visitRecord, ref visit) == -1)
            {
                return;
            }
            visitIntergrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            if (visitIntergrate.InsertAndUpdateVisit(visitRecord, seqNo, visit) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(visitIntergrate.Err);
                return;
            }
            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("随访信息保存成功");
            this.FindForm().Close();



        }

        /// <summary>
        /// 获取随访明细实体对象
        /// </summary>
        /// <param name="visitRecordObj">ref随访明细实体</param>
        /// <param name="visitObj">ref随访主记录实体</param>
        /// <returns>成功返回 0; 失败返回 -1;</returns>
        private int GetVisitRecordObj(ref FS.HISFC.Models.HealthRecord.Visit.VisitRecord visitRecordObj,
          ref FS.HISFC.Models.HealthRecord.Visit.Visit visitObj)
        {


            int checkCount = 0;//被选中的联系人数量

            FS.HISFC.Models.HealthRecord.Visit.LinkWay linkWayObj;

            for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
            {
                if (this.neuSpread1_Sheet1.Cells[i, 0].Text == true.ToString())
                {
                    linkWayObj = neuSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.HealthRecord.Visit.LinkWay;//Tag中的对象
                    checkCount += 1;
                }
            }

            if (checkCount != 1)
            {
                MessageBox.Show("必须且只能选择一个联系人");
                return -1;
            }

            visitRecordObj.Patient.PID.CardNO = CaseBase.PatientInfo.PID.CardNO;
            visitRecordObj.Circs.ID = ((FS.FrameWork.Models.NeuObject)cmbCircs.SelectedItem)
                == null ? "" : ((FS.FrameWork.Models.NeuObject)cmbCircs.SelectedItem).ID;
            visitRecordObj.DeadReason.ID = ((FS.FrameWork.Models.NeuObject)neuComboBoxDeadReason.SelectedItem)
                == null ? "" : ((FS.FrameWork.Models.NeuObject)neuComboBoxDeadReason.SelectedItem).ID;
            if (neuCheckBoxIsDead.Checked)
            {
                visitRecordObj.DeadTime = neuDateTimePickerDeadTime.Value;
            }
            visitRecordObj.IsDead = neuCheckBoxIsDead.Checked;
            visitRecordObj.IsRecrudesce = neuCheckBoxIsRecrudesce.Checked;
            visitRecordObj.IsSequela = neuCheckBoxIsSequela.Checked;
            visitRecordObj.VisitResult.ID = ((FS.FrameWork.Models.NeuObject)cmbResult.SelectedItem)
                == null ? "" : ((FS.FrameWork.Models.NeuObject)cmbResult.SelectedItem).ID;
            visitRecordObj.IsTransfer = neuCheckBoxIsTtransfer.Checked;

            if (neuCheckBoxIsRecrudesce.Checked)
            {
                visitRecordObj.RecrudesceTime = neuDateTimePickerRecrudesceTime.Value;
            }
            visitRecordObj.ResultOper.OperTime = System.DateTime.Now;
            visitRecordObj.VisitOper.OperTime = System.DateTime.Now;
            visitRecordObj.ResultOper.ID = FS.FrameWork.Management.Connection.Operator.ID;
            visitRecordObj.VisitOper.ID = FS.FrameWork.Management.Connection.Operator.ID;

            visitRecordObj.Sequela.ID = ((FS.FrameWork.Models.NeuObject)neuComboBoxSequela.SelectedItem)
                == null ? "" : ((FS.FrameWork.Models.NeuObject)neuComboBoxSequela.SelectedItem).ID;
            visitRecordObj.Symptom.ID = ((FS.FrameWork.Models.NeuObject)cmbSymptom.SelectedItem)
                == null ? "" : ((FS.FrameWork.Models.NeuObject)cmbSymptom.SelectedItem).ID;
            visitRecordObj.TransferPosition.ID = ((FS.FrameWork.Models.NeuObject)neuComboBoxTransferPosition.SelectedItem)
                == null ? "" : ((FS.FrameWork.Models.NeuObject)neuComboBoxTransferPosition.SelectedItem).ID;
            visitRecordObj.VisitType.ID = ((FS.FrameWork.Models.NeuObject)cmbLinkType.SelectedItem)
                == null ? "" : ((FS.FrameWork.Models.NeuObject)cmbLinkType.SelectedItem).ID;

            visitRecordObj.WriteBackPerson = txtWritebackPerson.Text;

            visitRecordObj.User01 = txtContent.Text.Trim();//随访详细内容

            for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
            {
                if (this.neuSpread1_Sheet1.Cells[i, 0].Text == true.ToString())
                {
                    //随访明细联系方式
                    visitRecordObj.LinkWay.LinkMan.Name = this.neuSpread1_Sheet1.Cells[i, 1].Text;//联系人姓名
                    visitRecordObj.LinkWay.Relation.ID = this.neuSpread1_Sheet1.Cells[i, 2].Text;
                    visitRecordObj.LinkWay.Phone = this.neuSpread1_Sheet1.Cells[i, 3].Text;
                    visitRecordObj.LinkWay.OtherLinkway = this.neuSpread1_Sheet1.Cells[i, 4].Text;//电话状态
                    visitRecordObj.LinkWay.Mail = this.neuSpread1_Sheet1.Cells[i, 6].Text;
                    visitRecordObj.LinkWay.Address = this.neuSpread1_Sheet1.Cells[i, 5].Text;


                    //末次随访联系方式
                    visitObj.Patient.PID.CardNO = CaseBase.PatientInfo.PID.CardNO;
                    visitObj.Linkway.Address = this.neuSpread1_Sheet1.Cells[i, 5].Text;
                    visitObj.Linkway.Mail = this.neuSpread1_Sheet1.Cells[i, 6].Text;
                    visitObj.Linkway.Phone = this.neuSpread1_Sheet1.Cells[i, 3].Text;
                    visitObj.LastVisitTime = System.DateTime.Now;
                    visitObj.Linkway.LinkWayType.ID = ((FS.FrameWork.Models.NeuObject)cmbLinkType.SelectedItem)
                == null ? "" : ((FS.FrameWork.Models.NeuObject)cmbLinkType.SelectedItem).ID;


                    visitObj.Linkway.LinkMan.Name = this.neuSpread1_Sheet1.Cells[i, 1].Text;
                    visitObj.Linkway.Relation.ID = this.neuSpread1_Sheet1.Cells[i, 2].Text;

                    if (rdbNormal.Checked)
                    {
                        visitObj.VisitState = FS.HISFC.Models.HealthRecord.Visit.EnumVisitState.Normal;
                    }
                    if (rdbStop.Checked)
                    {
                        visitObj.VisitState = FS.HISFC.Models.HealthRecord.Visit.EnumVisitState.Stop;
                    }


                }
            }

            return 0;
        }

        private void neuCheckBoxIsRecrudesce_CheckedChanged(object sender, EventArgs e)
        {
            this.neuDateTimePickerRecrudesceTime.Enabled = this.neuCheckBoxIsRecrudesce.Checked;
            if (this.neuCheckBoxIsRecrudesce.Checked)
            {
                this.neuDateTimePickerRecrudesceTime.Value = visitIntergrate.GetCurrentDateTime();
            }
            else
            {
                this.neuDateTimePickerRecrudesceTime.Value = this.neuDateTimePickerRecrudesceTime.MinDate;
            }
        }

        private void neuCheckBoxIsSequela_CheckedChanged(object sender, EventArgs e)
        {
            this.neuComboBoxSequela.Enabled = this.neuCheckBoxIsSequela.Checked;
        }

        private void neuCheckBoxIsTtransfer_CheckedChanged(object sender, EventArgs e)
        {
            this.neuComboBoxTransferPosition.Enabled = this.neuCheckBoxIsTtransfer.Checked;
        }

        /// <summary>
        /// 加载电话状态列表
        /// </summary>
        private void InitTelStateList()
        {
            telStateBox = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            ArrayList telStateList = new ArrayList();
            telStateList = con.GetList("TELSTATE");

            string[] s = new string[telStateList.Count];
            for (int i = 0; i < telStateList.Count; i++)
            {
                s[i] = ((FS.FrameWork.Models.NeuObject)telStateList[i]).Name.ToString();
            }

            telStateBox.Items = s;
            telStateBox.Editable = true;
            this.neuSpread1_Sheet1.Columns[4].CellType = telStateBox;
        }

        /// <summary>
        /// 加载与患者关系列表
        /// </summary>
        private void IninRelation()
        {
            ArrayList RelationList = con.GetList(FS.HISFC.Models.Base.EnumConstant.RELATIVE);

            relationBox = new FarPoint.Win.Spread.CellType.ComboBoxCellType();


            string[] s = new string[RelationList.Count];
            for (int i = 0; i < RelationList.Count; i++)
            {
                s[i] = ((FS.FrameWork.Models.NeuObject)RelationList[i]).Name.ToString();
            }

            relationBox.Items = s;
            this.neuSpread1_Sheet1.Columns[2].CellType = relationBox;

        }




        #endregion

        public override int Exit(object sender, object neuObject)
        {
            return base.Exit(sender, neuObject);

        }


        #region IInterfaceContainer 成员

        //{DC8452B8-FF77-4639-9522-A2CCED4B8A5C}
        Type[] FS.FrameWork.WinForms.Forms.IInterfaceContainer.InterfaceTypes
        {
            get
            {
                //return new Type[] { typeof(FS.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordInterface) }; ; 
                Type[] t = new Type[2];
                t[0] = typeof(FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface);
                t[1] = typeof(FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceBack);//转科申请
                return t;
            }
        }
        #endregion

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            //if (keyData == Keys.Enter)
            //{
            //    SendKeys.Send("{Tab}");
            //}
            //基本信息
            if (keyData == Keys.F2)
            {
                this.tab1.SelectedTab = this.tabPage1;
            }
            //诊断信息
            if (keyData == Keys.F3)
            {
                this.tab1.SelectedTab = this.tabPage5;
            }
            //手术信息
            if (keyData == Keys.F4)
            {
                this.tab1.SelectedTab = this.tabPage6;
            }
            //妇婴信息
            if (keyData == Keys.F5)
            {
                this.tab1.SelectedTab = this.tabPage2;
            }
            //转科信息
            if (keyData == Keys.F6)
            {
                this.tab1.SelectedTab = this.tabPage3;
            }
            //肿瘤信息
            if (keyData == Keys.F7)
            {
                this.tab1.SelectedTab = this.tabPage7;
            }
            //费用信息
            if (keyData == Keys.F8)
            {
                this.tab1.SelectedTab = this.tabPage4;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }


        private void txtNameSearch_Enter(object sender, EventArgs e)
        {
            this.ucPatient.Location = new Point(this.txtNameSearch.Location.X, this.txtNameSearch.Location.Y + this.txtNameSearch.Height + 2);
            this.ucPatient.BringToFront();
            this.ucPatient.Visible = false;
        }

        private void txtNameSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                try
                {
                    if (txtNameSearch.Text == "")
                    {
                        MessageBox.Show("请输入患者姓名");
                        return;
                    }
                    #region 清空
                    HandCraft = 0;
                    this.CaseBase = null;
                    ClearInfo();
                    #endregion
                    if (!this.ucPatient.Visible)
                    {
                        #region 查询
                        ArrayList list = null;
                        list = ucPatient.Init(txtNameSearch.Text.Trim(), "3");
                        if (list == null)
                        {
                            MessageBox.Show("查询失败" + ucPatient.strErr);
                            return;
                        }
                        if (list.Count == 0)
                        {
                            if (frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS)
                            {
                                #region 病案室自己手工录入病案
                                if (MessageBox.Show("没有查到相关病案信息,是否手工录入病案", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    txtPatientName.Text = txtNameSearch.Text;
                                    txtCaseNum.Focus();
                                    SetHandcraft();
                                }
                                else
                                {
                                    return;
                                }
                                #endregion
                            }
                            else
                            {
                                MessageBox.Show("没有查到相关病案信息");
                                return;
                            }
                        }
                        else if (list.Count == 1) //只有一条信息
                        {
                            ucPatient.Visible = false;
                            FS.HISFC.Models.HealthRecord.Base obj = this.ucPatient.GetCaseInfo();
                            if (obj != null)
                            {
                                LoadInfo(obj.PatientInfo.ID, this.frmType);
                                this.txtCaseNum.Focus();
                            }
                        }
                        else //多条信息 
                        {
                            ucPatient.Visible = true;
                        }
                        #endregion
                    }
                    else
                    {
                        #region  选择
                        FS.HISFC.Models.HealthRecord.Base obj = this.ucPatient.GetCaseInfo();
                        if (obj != null)
                        {
                            LoadInfo(obj.PatientInfo.ID, this.frmType);
                            this.txtCaseNum.Focus();
                        }
                        this.ucPatient.Visible = false;
                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                ucPatient.PriorRow();
            }
            else if (e.KeyCode == Keys.Down)
            {
                ucPatient.NextRow();
            }
        }

        private void txtPhoneHome_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtInTimes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtPactKind.Focus();
            }
        }

        private void txtDateOut_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtInjuryOrPoisoningCause_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtInfectNum.Focus();
            }
        }

        private void txtInfectionDiseasesReport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtFourDiseasesReport.Focus();
            }
        }

        private void txtFourDiseasesReport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtCheckDate.Focus();
            }
        }

        private void dtPatientBirthday_ValueChanged(object sender, EventArgs e)
        {
            //string age = FS.FrameWork.Public.String.GetAge(this.dtPatientBirthday.Value, baseDml.GetDateTimeFromSysDateTime());
            //this.txtPatientAge.Text = age.Substring(0, age.Length - 1);
            //this.cmbUnit.Text = age.Substring(age.Length - 1);
            this.txtPatientAge.Text = this.baseDml.GetAgeByFun(dtPatientBirthday.Value.Date, this.CaseBase.PatientInfo.PVisit.InTime.Date);
        }

        private void txtDateOut_ValueChanged(object sender, EventArgs e)
        {
            System.TimeSpan tt = this.txtDateOut.Value.Date - this.dtDateIn.Value.Date;
            if (tt.Days == 0)
            {
                this.txtPiDays.Text = "1";
            }
            else
            {
                this.txtPiDays.Text = Convert.ToString(tt.Days);
            }
            this.txtDeptOut.Focus();
        }
        
        /// <summary>
        /// 获取特级护理、一级护理、二级护理、三级护理
        /// </summary>
        /// <param name="inPatient"></param>
        private void GetNursingNum(string inPatient)
        {
            int tempT = this.baseDml.GetNursingNum(inPatient, "TJHL");//特级护理
            int tempY = this.baseDml.GetNursingNum(inPatient, "YJHL");//一级护理
            int tempE = this.baseDml.GetNursingNum(inPatient, "EJHL");//二级护理
            int tempS = this.baseDml.GetNursingNum(inPatient, "SJHL");//三级护理
            int tempTS = this.baseDml.GetNursingNum(inPatient, "TSHL");//特殊护理
            int tempZZ = this.baseDml.GetNursingNum(inPatient, "ZZJH");//重症监护

            this.txtSuperNus.Text = tempT.ToString();
            this.txtINus.Text = tempY.ToString();
            this.txtIINus.Text = tempE.ToString();
            this.txtIIINus.Text = tempS.ToString();
            this.txtSPecalNus.Text = tempTS.ToString();
            this.txtStrictNuss.Text = tempZZ.ToString();

        }

        /// <summary>
        /// 获取药物过敏史
        /// </summary>
        /// <param name="inPatient"></param>
        /// <param name="frmType"></param>
        private void PharmacyAllergicLoadInfo(string inPatient, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes frmType)
        {
            FS.HISFC.BizLogic.HealthRecord.Diagnose diagInfo = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
            List<FS.FrameWork.Models.NeuObject> inDiagnose = new List<FS.FrameWork.Models.NeuObject>();

            if (frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC)
            {
                inDiagnose = diagInfo.QueryPharmacyAllergicFromInCaseByInpatient(inPatient);
                if (inDiagnose != null)
                {
                    if (inDiagnose.Count == 0)
                    {
                        this.neuTxtPharmacyAllergic1.Text = "未发现";
                    }
                    else
                    {
                        foreach (FS.FrameWork.Models.NeuObject info in inDiagnose)
                        {
                            this.neuTxtPharmacyAllergic1.Text = info.Memo.ToString();
                        }
                    }
                }
            }
        }
    }


}
