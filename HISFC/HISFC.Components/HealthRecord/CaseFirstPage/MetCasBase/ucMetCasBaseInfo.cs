using System;
using System.Linq;
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
    public partial class ucMetCasBaseInfo : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {

        /// <summary>
        /// /// ucMetCasBaseInfo<br></br>
        /// [功能描述: 病案基本信息录入]<br></br>
        /// [创 建 者: 张俊义]<br></br>
        /// [创建时间: 2007-04-20]<br></br>
        /// </summary>
        public ucMetCasBaseInfo()
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
            this.ucTumourCard1.InitInfo();
            #endregion

            #region  转科
            //thread = new System.Threading.Thread(this.ucChangeDept1.InitInfo);
            //thread.Start(); 
            //this.ucChangeDept1.InitInfo();
            #endregion

            #region  诊断信息
            //thread = new System.Threading.Thread(this.ucDiagNoseInput1.InitInfo);
            //thread.Start();  
            this.ucDiagNoseInput1.InitInfo();
            #endregion

            #endregion

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
        [Category("最近出院患者"), Description("输入要查询的时间段，默认为3天")]
        /// <summary>
        /// 天数  病案首页最近显示出院病人的时间段
        /// 用法：直接输入整数
        /// </summary>
        private int days = 3;
        public int DAYs
        {
            get { return days; }
            set { days = value; }
        }

        // =======================以下两个属性作用:如果病案中不存在该患者,则使用窗体传来的值，作为查询基本信息的依据
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
        [Category("是否回写首页信息到主表"), Description("回写主表操作,True：回写 False:不回写")]
        public bool IsUpdataFinIprInmaininfo
        {
            get { return this.isUpdataFinIprInmaininfo; }
            set { this.isUpdataFinIprInmaininfo = value; }
        }


        #region  全局变量
        //标志 标识是医生站用还是病案调用
        private FS.HISFC.Models.HealthRecord.EnumServer.frmTypes frmType = FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC;
        //暂存当前修改人的病案基本信息
        private FS.HISFC.Models.HealthRecord.Base CaseBase = new FS.HISFC.Models.HealthRecord.Base();
        //病案基本信息操作类
        private FS.HISFC.BizLogic.HealthRecord.Base baseDml = new FS.HISFC.BizLogic.HealthRecord.Base();
        //add ren
        private FS.FrameWork.WinForms.Controls.PopUpListBox Drr = new FS.FrameWork.WinForms.Controls.PopUpListBox();
        private FS.FrameWork.Public.ObjectHelper DrrHelper = new FS.FrameWork.Public.ObjectHelper();
        //add chengym 临时住院流水号，LoadInfo中用于判断是否同一个患者，不同患者重新获取数据
        private string TempInpatient = string.Empty;
        private bool isNeedLoadInfo = false;
        private string in_State = string.Empty;//记录患者目前状态 loadinfo时查主表赋值
        private DateTime dt_out = System.DateTime.Now.AddYears(-1);//出院日期
        private string bedNo = string.Empty;//出院床位
        private string dept_out = string.Empty;//住院主表出院科室
        private string dutyNurse = string.Empty;//责任护士
        private string patient_no = string.Empty;//住院号 处理改号的情况2013-1-17
        //end add chengym
        //定义变量
        FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
        //门诊诊断 
        private FS.HISFC.Models.HealthRecord.Diagnose clinicDiag = null;
        //入院诊断 
        private FS.HISFC.Models.HealthRecord.Diagnose InDiag = null;
        //合同单位
        private FS.HISFC.BizLogic.Fee.PactUnitInfo pactManager = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
        //转科信息
        ArrayList changeDeptList = new ArrayList();
        FS.HISFC.BizLogic.HealthRecord.DeptShift deptChange = new FS.HISFC.BizLogic.HealthRecord.DeptShift();
        FS.HISFC.BizLogic.HealthRecord.Fee healthRecordFee = new FS.HISFC.BizLogic.HealthRecord.Fee();
        HealthRecord.Search.ucPatientList ucPatient = new HealthRecord.Search.ucPatientList();
        //标识手工输入的状态是插入还是更新  0默认状态  1  插入 2更新  
        private int HandCraft = 0;
        //保存病案的状态
        private int CaseFlag = 0;
        //提示窗体
        ucDiagNoseCheck frm = null;
        private FS.FrameWork.Models.NeuObject localObj = new FS.FrameWork.Models.NeuObject();
        //打印接口 
        private FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface healthRecordPrint = null;//打印正面
        private FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceBack healthRecordPrintBack = null;//背面 
        private FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceAdditional healthRecordInterfaceAdditional = null;//打印附加页
        //{B71C3094-BDC8-4fe8-A6F1-7CEB2AEC55DD}
        private FS.HISFC.BizProcess.Integrate.Manager manageIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
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
        /// <summary>
        /// 使用最新电子病历调取
        /// 显示到界面时cmb控件text未显示内容
        /// </summary>
        private bool isNewEMR = false;
        /// <summary>
        /// 打印的页数
        /// 默认按广东省标准打印3月 
        /// 有设置常数“CASEPRINTTOWP”打印两页（取消打印费用）
        /// </summary>
        private int PrintPageNum = 3;
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
            #region 加载选择框
            this.Controls.Add(this.ucPatient);
            this.ucPatient.BringToFront();
            this.ucPatient.Visible = false;
            this.ucPatient.SelectItem += new HealthRecord.Search.ucPatientList.ListShowdelegate(ucPatient_SelectItem);
            #endregion

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
            this.neuGroupBox3.Visible = false;//drgs前的病案首页字段不显示
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
            //{DC8452B8-FF77-4639-9522-A2CCED4B8A5C}
            toolBarService.AddToolButton("打印第二页", "打印第二页", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);

            toolBarService.AddToolButton("提交", "提交", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Z执行, true, false, null);
            this.toolBarService.AddToolButton("刷新", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S刷新, true, false, null);
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
                case "刷新":
                    //this.Save(2);
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
            //医疗付费方式 
            ArrayList pactKindlist = con.GetList("CASEPACT");
            this.cmbPactKind.AddItems(pactKindlist);
            //获取性别列表
            //ArrayList sexList = con.GetList("CASESEX");
            ArrayList sexList = FS.HISFC.Models.Base.SexEnumService.List();
            this.cmbPatientSex.AddItems(sexList);
            //查询国籍列表
            ArrayList countryList = con.GetList(FS.HISFC.Models.Base.EnumConstant.COUNTRY);
            this.cmbCountry.AddItems(countryList);
            //查询行政地址列表
            ArrayList AddrList = con.GetList("CASEADDR");
            //修改籍贯、出生地
            ArrayList addrlist = con.GetList("GBXZQYHF");
            this.cmbBirthAddr.AddItems(addrlist);//出生地
            this.cmbDist.AddItems(addrlist);//籍贯
            this.cmbCurrentAdrr.AddItems(addrlist);//现住址
            this.cmbHomeAdrr.AddItems(addrlist);//家庭住址

            //查询民族列表
            ArrayList Nationallist1 = con.GetList(FS.HISFC.Models.Base.EnumConstant.NATION);
            this.cmbNationality.AddItems(Nationallist1);

            //查询职业列表
            //ArrayList Professionlist = con.GetList(FS.HISFC.Models.Base.EnumConstant.PROFESSION);
            ArrayList Professionlist = con.GetList("CASEPROFESSION");
            this.cmbProfession.AddItems(Professionlist);
            //婚姻列表
            ArrayList MaritalStatusList = new ArrayList();
            MaritalStatusList = con.GetList("CASEMARITAL");
            if (MaritalStatusList == null || MaritalStatusList.Count == 0)
            {
                MaritalStatusList = FS.HISFC.Models.RADT.MaritalStatusEnumService.List();//con.GetList("CASEMARITAL"); 
            }
            this.cmbMaritalStatus.AddItems(MaritalStatusList);
            //与联系人关系
            ArrayList RelationList = con.GetList(FS.HISFC.Models.Base.EnumConstant.RELATIVE);
            this.cmbRelation.AddItems(RelationList);
            //获取入院途径列表
            ArrayList inAvenueList = con.GetAllList("INSOURCE");
            this.cmbInPath.AddItems(inAvenueList);

            //科室下拉列表
            FS.HISFC.BizLogic.Manager.Department dept = new FS.HISFC.BizLogic.Manager.Department();
            ArrayList deptList = dept.GetDeptmentAll();
            cmbDeptInHospital.AddItems(deptList);
            cmbDeptOutHospital.AddItems(deptList);
            txtFirstDept.AddItems(deptList);
            txtDeptSecond.AddItems(deptList);
            txtDeptThird.AddItems(deptList);

            //获取医生列表
            FS.HISFC.BizLogic.Manager.Person person = new FS.HISFC.BizLogic.Manager.Person();
            ArrayList DoctorList = person.GetEmployeeAll();//.GetEmployee(FS.HISFC.Models.RADT.PersonType.enuPersonType.D);
            FS.HISFC.Models.Base.Employee tempEmpl = new FS.HISFC.Models.Base.Employee();
            //广医四院强力要加 妥协了
            tempEmpl.ID = "-";
            tempEmpl.Name = "-";
            tempEmpl.SpellCode = "—";
            tempEmpl.WBCode = "-";
            DoctorList.Add(tempEmpl);
            this.cmbAdmittingDoctor.AddItems(DoctorList);
            this.cmbAttendingDoctor.AddItems(DoctorList);
            this.cmbConsultingDoctor.AddItems(DoctorList);
            this.cmbRefresherDocd.AddItems(DoctorList);
            //this.cmbRefresherDocd.Text = "-";
            cmbDutyNurse.AddItems(DoctorList);
            cmbQcDocd.AddItems(DoctorList);
            cmbQcNucd.AddItems(DoctorList);
            txtCodingCode.AddItems(DoctorList);
            this.cmbPraDocCode.AddItems(DoctorList);
            //this.cmbPraDocCode.Text = "-";
            this.cmbDeptChiefDoc.AddItems(DoctorList);
            this.txtClinicDocd.AddItems(DoctorList);//门诊医生
            //病例分型
            ArrayList ExampleTypeList = con.GetList("CASEEXAMPLETYPE");
            this.cmbExampleType.AddItems(ExampleTypeList);
            //血型列表
            ArrayList BloodTypeList = con.GetList("CASEBLOODTYPE");//con.GetList(FS.HISFC.Models.Base.EnumConstant.BLOODTYPE);//baseDml.GetBloodType();
            this.cmbBloodType.AddItems(BloodTypeList);
            //RH性质 
            ArrayList RHList = con.GetList(FS.HISFC.Models.Base.EnumConstant.RHSTATE); //baseDml.GetRHType();
            cmbRhBlood.AddItems(RHList);


            //病案质量
            ArrayList qcList = con.GetList(FS.HISFC.Models.Base.EnumConstant.CASEQUALITY);
            cmbMrQual.AddItems(qcList);

            //损伤中毒列表
            FS.HISFC.BizLogic.HealthRecord.ICD icd = new FS.HISFC.BizLogic.HealthRecord.ICD();
            //ArrayList listIcd = icd.Query(FS.HISFC.Models.HealthRecord.EnumServer.ICDTypes.ICD10, FS.HISFC.Models.HealthRecord.EnumServer.QueryTypes.Valid);
            //损伤中毒 范围是：V01-Y98
            ArrayList listIcd = icd.QueryDrgs(FS.HISFC.Models.HealthRecord.EnumServer.ICDTypes.ICD10);
            ArrayList injuryOrPoisoningCauseList = new ArrayList();
            ArrayList otherIcdList = new ArrayList();
            if (listIcd != null)
            {
                foreach (FS.HISFC.Models.HealthRecord.ICD info in listIcd)
                {
                    //info.ID.IndexOf('U') == 0 || || info.ID.IndexOf('Z') == 0取消 2012-10-22 chengym
                    if (info.ID.IndexOf('V') == 0 || info.ID.IndexOf('W') == 0 || info.ID.IndexOf('X') == 0 || info.ID.IndexOf('Y') == 0)
                    {
                        injuryOrPoisoningCauseList.Add(info);
                    }
                    else
                    {
                        if (info.ID.IndexOf("U") != 0)
                        {
                            otherIcdList.Add(info);
                        }
                    }
                }
            }
            FS.HISFC.Models.HealthRecord.ICD icdInfo = new FS.HISFC.Models.HealthRecord.ICD();
            icdInfo.ID = "-";
            icdInfo.Name = "未发现";
            icdInfo.SpellCode = "WFX";
            icdInfo.WBCode = "FNG";
            injuryOrPoisoningCauseList.Add(icdInfo);
            otherIcdList.Add(icdInfo);
            icdInfo = new FS.HISFC.Models.HealthRecord.ICD();
            icdInfo.ID = "—";
            icdInfo.Name = "—";
            icdInfo.SpellCode = "-";
            icdInfo.WBCode = "—";
            injuryOrPoisoningCauseList.Add(icdInfo);
            otherIcdList.Add(icdInfo);
            this.txtInjuryOrPoisoningCause.AddItems(injuryOrPoisoningCauseList);//损伤中毒
            this.cmbClinicDiagName.AddItems(otherIcdList);//门诊诊断
            this.cmbPathologicalDiagName.AddItems(otherIcdList);//病理诊断
            //病案状态
            this.caseStusHelper = new FS.FrameWork.Public.ObjectHelper(con.GetList("CASESTUS"));

            //1 无 2 有 常数列表
            ArrayList notOrHavedList = con.GetList("CASENOTORHAVED");
            this.cmbIsDrugAllergy.AddItems(notOrHavedList);//药物过敏
            this.cmbComeBackInMonth.AddItems(notOrHavedList);//是否出院31天内再住院

            //1是 2否 常数列表
            ArrayList yseOrNoList = con.GetList("CASEYSEORNO");
            this.cmbClinicPath.AddItems(yseOrNoList);//临床路径病例
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "-";
            obj.Name = "-";
            yseOrNoList.Add(obj);
            this.cmbDeathPatientBobyCheck.AddItems(yseOrNoList);//死亡患者尸检

            //兼容旧版本
            this.cmbYnFirst.AddItems(yseOrNoList);//是否首病例
            this.cmbVisiStat.AddItems(yseOrNoList);//是否随访
            this.cmbTechSerc.AddItems(yseOrNoList);//是否示教病例
            this.cmbDisease30.AddItems(yseOrNoList);//是否单病种
            //获取病人来源
            ArrayList Insourcelist = con.GetAllList("PATIENTINSOURCE");
            this.txtInAvenue.AddItems(Insourcelist);
            //入院情况
            ArrayList CircsList = con.GetList(FS.HISFC.Models.Base.EnumConstant.INCIRCS);
            this.txtCircs.AddItems(CircsList);
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
            //血液反应
            ArrayList ReactionBloodList = con.GetList(FS.HISFC.Models.Base.EnumConstant.BLOODREACTION);// baseDml.GetReactionBlood();
            txtReactionBlood.AddItems(ReactionBloodList);
            txtReactionTransfuse.AddItems(ReactionBloodList);//输液反应
            //传染病上报
            ArrayList ReportedList = con.GetList("Reported");
            this.txtFourDiseasesReport.AddItems(ReportedList);
            this.txtInfectionDiseasesReport.AddItems(ReportedList);
            //打印页数
            ArrayList PrintPageNumList = con.GetList("CASEPRINTTOWP");
            if (PrintPageNumList != null && PrintPageNumList.Count > 0)
            {
                this.PrintPageNum = 2;
            }
            ArrayList alTemp = new ArrayList();
            //邮编
            ArrayList alDist = con.GetList("CASEDIST");
            //ArrayList alDist = con.GetList("ZIP");
            foreach (FS.HISFC.Models.Base.Spell s in alDist)
            {
                FS.HISFC.Models.Base.Spell sClone = s.Clone();

                sClone.ID = sClone.Name;
                sClone.Name = sClone.UserCode;

                alTemp.Add(sClone);
            }
            this.txtCurrentZip.AddItems(alTemp);
            this.txtBusinessZip.AddItems(alTemp);
            this.txtHomeZip.AddItems(alTemp);
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
            //TreeNode tnTJParent;//已提交病人列表
            this.treeView1.HideSelection = false;
            //Neuosft.FS.HISFC.BizProcess.Integrate.RADT pQuery = new FS.HISFC.BizProcess.Integrate.RADT(); //t.RADT.InPatient();
            this.treeView1.BeginUpdate();
            this.treeView1.Nodes.Clear();
            //画树头
            tnParent = new TreeNode();
            tnParent.Text = "最近医生提交出院患者";
            tnParent.Tag = "%";
            try
            {
                tnParent.ImageIndex = 0;
                tnParent.SelectedImageIndex = 1;
            }
            catch { }
            this.treeView1.Nodes.Add(tnParent);
            ////画tnTJParent树头
            //tnTJParent = new TreeNode();
            //tnTJParent.Text = "最近医生提交的患者";
            //tnTJParent.Tag = "%";
            //try
            //{
            //    tnTJParent.ImageIndex = 4;
            //    tnTJParent.SelectedImageIndex = 5;
            //}
            //catch { }
            //this.treeView1.Nodes.Add(tnTJParent);//加已提交病人列表
            DateTime dt = this.baseDml.GetDateTimeFromSysDateTime();
            DateTime dt2 = dt.AddDays(-days);
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
                //TreeNode tnTJPatient = new TreeNode();

                tnPatient.Text = pInfo.Name + "[" + pInfo.PID.PatientNO + "]";
                tnPatient.Tag = pInfo;
                //ArrayList CaseBase = baseDml.GetCaseBaseInfo(dt,dt2);//病案主表信息（met_cas_base）
                //foreach (FS.HISFC.Models.HealthRecord.Base Casebase in CaseBase)
                //{
                //    if (FS.FrameWork.Function.NConvert.ToInt32(Casebase.PatientInfo.CaseState) == 2)
                //    {
                //        tnTJPatient.Text = pInfo.Name + "[" + pInfo.PID.PatientNO + "]";//
                //        tnTJPatient.Tag = pInfo;//
                //    }
                //    else
                //    {
                //        this.treeView1.EndUpdate();
                //    }
                //}
                try
                {
                    tnPatient.ImageIndex = 2;
                    tnPatient.SelectedImageIndex = 3;
                    //    tnTJPatient.ImageIndex = 6;///
                    //    tnTJPatient.SelectedImageIndex = 7;//
                }
                catch { }
                tnParent.Nodes.Add(tnPatient);
                //tnTJParent.Nodes.Add(tnTJPatient);//
            }

            tnParent.Expand();
            //tnTJParent.Expand();//
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
        /// 重写保存函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public int EmrSave(object sender, object neuObject)
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
            //if (type == 1)
            //{//提交时必填项
            //    if (this.PrintFristCheck() == -1)
            //    {
            //        return -1;
            //    }
            //}
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
            #region  判断诊断是否符合约束
            FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
            //这里的诊断检验条件没有具体研究过，需要后续完善，由于有些医院导致保存问题，这里先屏蔽。
            ArrayList alDiagCheck = con.GetList("CASEDIAGCHECK");
            FS.HISFC.BizLogic.HealthRecord.Diagnose diagNose = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
            if (alDiagCheck != null && alDiagCheck.Count != 0)
            {
                if (this.frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC) //医生站提示 病案室不需要提示
                {
                    if (DiagValueState(diagNose) != 1)
                    {
                        return -1;
                    }
                }
            }
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

                baseDml.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                diagNose.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                healthRecordFee.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                deptChange.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

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
                    if (frmRemark.DialogResult == DialogResult.No)
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
                this.ucDiagNoseInput1.Patient = info.PatientInfo;
                this.ucOperation1.Patient = info.PatientInfo;
                this.ucBabyCardInput1.Patient = info.PatientInfo;
                this.ucTumourCard1.Patient = info.PatientInfo;
                this.ucFeeInfo1.Patient = info.PatientInfo;

                #endregion
                #region 转科科别
                if (changeDeptList != null)
                {

                    if (deptChange.DeleteChangeDept(info.PatientInfo.ID) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("转科信息保存失败" + baseDml.Err);
                        return -1;
                    }
                    foreach (FS.HISFC.Models.RADT.Location locationInfo in changeDeptList)
                    {
                        if (deptChange.InsertOrUpdate(locationInfo) < 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("转科信息保存失败" + baseDml.Err);
                            return -1;
                        }
                    }
                }
                #endregion
                #region  病案诊断
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
                if (diagList != null && diagList.Count > 0)
                {
                    diagNose.DeleteDiagnoseAll(CaseBase.PatientInfo.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, FS.HISFC.Models.Base.ServiceTypes.I);
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
                ////暂时保存插入和修改过的数据 不是采用table changed的模式不需要暂存了
                //ArrayList tempDiag = diagNose.QueryCaseDiagnose(CaseBase.PatientInfo.ID, "%", frmType,FS.HISFC.Models.Base.ServiceTypes.I);
                #endregion
                #region  手术信息
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
                operation.deleteAll(CaseBase.PatientInfo.ID);
                if (operationList != null && operationList.Count > 0)
                {
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
                //ArrayList tempOperation = operation.QueryOperation(this.frmType, CaseBase.PatientInfo.ID);

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
                FS.HISFC.Models.HealthRecord.Tumour TumInfo = this.ucTumourCard1.GetTumourInfo();
                int m = this.ucTumourCard1.ValueTumourSate(TumInfo);
                tumour.DeleteTumour(TumInfo.InpatientNo);
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
                this.ucTumourCard1.deleteRow();
                this.ucTumourCard1.GetList("D", tumDel);
                this.ucTumourCard1.GetList("A", tumAdd);
                this.ucTumourCard1.GetList("M", tumMod);
                if (this.ucTumourCard1.ValueState(tumDel) == -1 || this.ucTumourCard1.ValueState(tumAdd) == -1 || this.ucTumourCard1.ValueState(tumMod) == -1)
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
                        if (obj.DrugInfo.Name != null && obj.DrugInfo.Name != "")
                        {
                            if (tumour.InsertTumourDetail(obj) < 1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("保存肿瘤信息失败" + tumour.Err);
                                return -1;
                            }
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
                #region  费用信息 新首页不再存病案费用信息
                //ArrayList feeList = this.ucFeeInfo1.GetFeeInfoList();
                //if (this.ucFeeInfo1.ValueState(feeList) == -1)
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack();//回退
                //    return -3;
                //}
                //if (feeList != null)
                //{
                //    foreach (FS.HISFC.Models.RADT.Patient obj in feeList)
                //    {
                //        obj.ID = this.CaseBase.PatientInfo.ID; //住院流水号
                //        obj.User01 = this.CaseBase.PatientInfo.PVisit.OutTime.ToString(); //出院日期
                //        if (healthRecordFee.UpdateFeeInfo(obj) < 1)
                //        {
                //            FS.FrameWork.Management.PublicTrans.RollBack();
                //            MessageBox.Show("保存费用信息失败" + baseDml.Err);
                //            return -1;
                //        }
                //    }
                //}
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
                //this.ucChangeDept1.fpEnterSaveChanges(tempChangeDept);
                //this.ucDiagNoseInput1.fpEnterSaveChanges(tempDiag);
                //this.ucOperation1.fpEnterSaveChanges(tempOperation);
                this.ucTumourCard1.fpEnterSaveChanges(tempTumour);

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
                    Function fun = new Function();
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
                        if (CaseBase.PatientInfo.MaritalStatus.ID != null && fun.ReturnStringValue(CaseBase.PatientInfo.MaritalStatus.ID.ToString(), false) != string.Empty)
                        {
                            switch (CaseBase.PatientInfo.MaritalStatus.ID.ToString())
                            {
                                case "1":// 未婚
                                    patientInfoForUpdate.MaritalStatus.ID = "S";//未婚
                                    break;
                                case "2"://已婚
                                    patientInfoForUpdate.MaritalStatus.ID = "M";//已婚
                                    break;
                                case "3"://丧偶
                                    patientInfoForUpdate.MaritalStatus.ID = "W";//丧偶
                                    break;
                                case "4"://离婚
                                    patientInfoForUpdate.MaritalStatus.ID = "D";//失婚
                                    break;
                                case "9":
                                    patientInfoForUpdate.MaritalStatus.ID = "";//     R再婚
                                    break;
                                default:
                                    patientInfoForUpdate.MaritalStatus.ID = info.PatientInfo.MaritalStatus.ID.ToString();
                                    break;
                            }
                            //patientInfoForUpdate.MaritalStatus.ID = CaseBase.PatientInfo.MaritalStatus.ID.ToString(); //婚姻状况
                        }
                        if (fun.ReturnStringValue(CaseBase.PatientInfo.Country.ID, false) != string.Empty)
                        {
                            patientInfoForUpdate.Country.ID = CaseBase.PatientInfo.Country.ID; //国籍
                        }
                        if (CaseBase.PatientInfo.BloodType.ID != null && fun.ReturnStringValue(CaseBase.PatientInfo.BloodType.ID.ToString(), false) != string.Empty)
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

                            MessageBox.Show("更新com_patientinfo出错!");

                            return -1;
                        }

                        if (baseDml.UpdatePatient(patientInfoForUpdate) < 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();

                            MessageBox.Show("更新fin_ipr_inmaininfo出错!");

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
                //MessageBox.Show("首页保存成功");
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
                //手术信息
                case "tabPage3":
                    //如果小于零 ，增加一行
                    if (this.ucOperation1.GetfpSpread1RowCount() == 0)
                    {
                        this.ucOperation1.AddRow();
                        this.ucOperation1.SetActiveCells();
                    }
                    break;
                // case "诊断信息":
                case "tabPage2":
                    if (this.ucDiagNoseInput1.GetfpSpreadRowCount() == 0)
                    {
                        this.ucDiagNoseInput1.AddRow();
                        this.ucDiagNoseInput1.SetActiveCells();
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
            #region
            try
            {
                //医疗付费方式 fin_ipr_inmaininfo获取数据将合同单位转换过来
                if (info.PatientInfo.CaseState == "1")
                {
                    FS.FrameWork.Models.NeuObject pactInfo = con.GetConstant("CASEPACTCHANGE", info.PatientInfo.Pact.ID);
                    if (pactInfo != null && pactInfo.Memo != "")
                    {
                        cmbPactKind.Tag = pactInfo.Memo;
                    }
                }
                else
                {
                    if (info.PatientInfo.Pact.PayKind.ID == "DRGS")
                    {
                        cmbPactKind.Tag = info.PatientInfo.Pact.ID;
                    }
                    else
                    {
                        FS.FrameWork.Models.NeuObject pactInfo = con.GetConstant("CASEPACTCHANGE", info.PatientInfo.Pact.ID);
                        if (pactInfo != null && pactInfo.Memo != "")
                        {
                            cmbPactKind.Tag = pactInfo.Memo;
                        }
                    }
                }
                //健康卡号
                txtHealthyCard.Text = info.PatientInfo.SSN;
                //住院次数
                if (info.PatientInfo.InTimes == 0)
                {
                    txtInTimes.Text = "1";
                }
                else
                {
                    txtInTimes.Text = info.PatientInfo.InTimes.ToString();
                }
                //病案号
                //if (info.CaseNO == "" || info.CaseNO == null)
                //{
                //    txtCaseNum.Text = info.PatientInfo.PID.PatientNO;
                //}
                //else
                //{
                //    txtCaseNum.Text = info.CaseNO;
                //}
                this.txtCaseNum.Text = this.patient_no;
                //姓名
                this.txtPatientName.Text = info.PatientInfo.Name;

                //性别
                if (info.PatientInfo.Sex.ID != null)
                {
                    cmbPatientSex.Tag = info.PatientInfo.Sex.ID.ToString();
                }
                //出生日期
                if (info.PatientInfo.Birthday != System.DateTime.MinValue)
                {
                    dtPatientBirthday.Value = info.PatientInfo.Birthday;
                }
                else
                {
                    dtPatientBirthday.Value = System.DateTime.Now;
                }
                #region 年龄
                //年龄 和 年龄不足一周岁
                //为保证年龄跟电子病历一致，直接使用电子病历的年龄计算函数fun_get_age_new
                if (info.PatientInfo.CaseState == "1")
                {

                    string strAge = string.Empty;
                    int age = 0;
                    if (this.isNewEMR)
                    {
                        strAge = this.baseDml.EmrGetAge(dtPatientBirthday.Value.Date, info.PatientInfo.PVisit.InTime.Date, ref age);
                    }
                    else
                    {
                        strAge = this.baseDml.GetAgeByFun(dtPatientBirthday.Value.Date, info.PatientInfo.PVisit.InTime.Date);
                    }
                    this.txtPatientAge.Text = strAge;
                    this.txtBabyAge.Text = strAge;
                }
                else
                {
                    if (info.AgeUnit == "0岁")
                    {
                        string strAge1 = string.Empty;
                        int age1 = 0;
                        if (this.isNewEMR)
                        {
                            strAge1 = this.baseDml.EmrGetAge(dtPatientBirthday.Value.Date, info.PatientInfo.PVisit.InTime.Date, ref age1);
                        }
                        else
                        {
                            strAge1 = this.baseDml.GetAgeByFun(dtPatientBirthday.Value.Date, info.PatientInfo.PVisit.InTime.Date);
                        }
                        this.txtPatientAge.Text = strAge1;
                        this.txtBabyAge.Text = strAge1;
                    }
                    else
                    {
                        this.txtPatientAge.Text = info.AgeUnit;
                        this.txtBabyAge.Text = info.BabyAge;
                    }
                }
                //转换显示格式
                if (this.txtPatientAge.Text != "" && this.txtPatientAge.Text != "0")
                {
                    if (this.txtPatientAge.Text.IndexOf("岁") > 0 && this.txtPatientAge.Text.IndexOf("月") < 0) //整岁
                    {
                        this.txtPatientAge.Text = "Y" + this.txtPatientAge.Text.Replace("岁", "");
                    }
                    else if (this.txtPatientAge.Text.IndexOf("岁") < 0 && this.txtPatientAge.Text.IndexOf("月") > 0 && this.txtPatientAge.Text.IndexOf("天") < 0)//整月
                    {
                        this.txtPatientAge.Text = "M" + this.txtPatientAge.Text.Replace("月", "");
                    }
                    else if (this.txtPatientAge.Text.IndexOf("岁") < 0 && this.txtPatientAge.Text.IndexOf("月") < 0 && this.txtPatientAge.Text.IndexOf("周") < 0 && this.txtPatientAge.Text.IndexOf("天") > 0)//整天
                    {
                        this.txtPatientAge.Text = "D" + this.txtPatientAge.Text.Replace("天", "");
                    }
                    else if (this.txtPatientAge.Text.IndexOf("岁") > 0 && this.txtPatientAge.Text.IndexOf("月") > 0 && this.txtPatientAge.Text.IndexOf("天") < 0)//N岁N月
                    {
                        string[] PAge = this.txtPatientAge.Text.Split('岁');
                        this.txtPatientAge.Text = "Y" + PAge[0] + "M" + PAge[1].Replace("岁", "").Replace("月", "");
                    }
                    else if (this.txtPatientAge.Text.IndexOf("岁") < 0 && this.txtPatientAge.Text.IndexOf("月") > 0 && this.txtPatientAge.Text.IndexOf("天") > 0)//N月N天
                    {
                        string[] PAge = this.txtPatientAge.Text.Split('月');
                        this.txtPatientAge.Text = "M" + PAge[0] + "D" + PAge[1].Replace("月", "").Replace("天", "");
                    }
                    else if (this.txtPatientAge.Text.IndexOf("岁") > 0 && this.txtPatientAge.Text.IndexOf("月") > 0 && this.txtPatientAge.Text.IndexOf("天") > 0)//N岁N月N天
                    {
                        string[] PAge = this.txtPatientAge.Text.Split('岁');

                        string[] PAge1 = PAge[1].Split('月');
                        this.txtPatientAge.Text = "Y" + PAge[0] + "M" + PAge1[0] + "D" + PAge1[1].Replace("月", "").Replace("天", "");
                    }
                    else if (this.txtPatientAge.Text.IndexOf("岁") < 0 && this.txtPatientAge.Text.IndexOf("月") < 0 && this.txtPatientAge.Text.IndexOf("周") > 0 && this.txtPatientAge.Text.IndexOf("天") > 0)//N周N天
                    {
                        string[] PAge = this.txtPatientAge.Text.Split('周');
                        this.txtPatientAge.Text = "W" + PAge[0] + "D" + PAge[1].Replace("周", "").Replace("天", "");
                    }
                }
                #endregion
                //国籍 编码
                cmbCountry.Tag = info.PatientInfo.Country.ID;
                //新生儿出生体重
                if (info.BabyBirthWeight == "0")
                {
                    this.txtBabyBirthWeight.Text = "-";
                }
                else
                {
                    this.txtBabyBirthWeight.Text = info.BabyBirthWeight;
                }
                //新生儿入院体重
                if (info.BabyInWeight == "0")
                {
                    this.txtBabyInWeight.Text = "-";
                }
                else
                {
                    this.txtBabyInWeight.Text = info.BabyInWeight;
                }
                //出生地
                if (info.PatientInfo.CaseState == "1")
                {
                    try
                    {
                        FS.FrameWork.Models.NeuObject area = this.con.GetConstant("AREA", info.PatientInfo.AreaCode);
                        if (area != null && area.Name != "")
                        {
                            this.cmbBirthAddr.Text = area.Name;
                        }
                    }
                    catch
                    {
                        this.cmbBirthAddr.Text = info.PatientInfo.AreaCode;
                    }
                }
                else
                {
                    this.cmbBirthAddr.Text = info.PatientInfo.AreaCode;
                }
                //籍贯
                this.cmbDist.Text = info.PatientInfo.DIST;
                //民族 
                cmbNationality.Tag = info.PatientInfo.Nationality.ID;
                //身份证号
                txtIDNo.Text = info.PatientInfo.IDCard;
                //职业
                cmbProfession.Tag = info.PatientInfo.Profession.ID;
                //婚姻
                if (info.PatientInfo.MaritalStatus.ID != null)
                {
                    switch (info.PatientInfo.MaritalStatus.ID.ToString())
                    {
                        case "S"://未婚
                            this.cmbMaritalStatus.Tag = "1";//未婚
                            break;
                        case "M"://已婚
                            this.cmbMaritalStatus.Tag = "2";//已婚
                            break;
                        case "W"://丧偶
                            this.cmbMaritalStatus.Tag = "3";//丧偶
                            break;
                        case "A"://分居
                            this.cmbMaritalStatus.Tag = "2";//已婚
                            break;
                        case "D"://失婚
                            this.cmbMaritalStatus.Tag = "4";//离婚
                            break;
                        case "R"://再婚
                            this.cmbMaritalStatus.Tag = "2";//已婚
                            break;
                        default:
                            this.cmbMaritalStatus.Tag = info.PatientInfo.MaritalStatus.ID.ToString();
                            break;
                    }
                }
                if (info.PatientInfo.CaseState == "1")
                {
                    //现住址 
                    cmbCurrentAdrr.Text = info.PatientInfo.AddressHome;
                    //现住址电话
                    txtCurrentPhone.Text = info.PatientInfo.PhoneHome;
                    //现住址邮编
                    txtCurrentZip.Text = info.PatientInfo.User02;
                }
                else
                {
                    //现住址
                    cmbCurrentAdrr.Text = info.CurrentAddr;
                    //现住址电话
                    txtCurrentPhone.Text = info.CurrentPhone;
                    //现住址邮编
                    txtCurrentZip.Text = info.CurrentZip;
                }
                //户口地址
                cmbHomeAdrr.Text = info.PatientInfo.AddressHome;
                //户口住址邮编
                if (info.PatientInfo.CaseState == "1")
                {
                    txtHomeZip.Text = info.PatientInfo.User02;
                }
                else
                {
                    txtHomeZip.Text = info.PatientInfo.HomeZip;
                }
                //工作单位及地址
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
                //与患者关系
                cmbRelation.Tag = info.PatientInfo.Kin.RelationLink;
                //联系人关系备注
                txtRelationMemo.Text = info.PatientInfo.Kin.Memo;
                //联系地址
                txtLinkmanAdd.Text = info.PatientInfo.Kin.RelationAddress;

                //联系电话
                txtLinkmanTel.Text = info.PatientInfo.Kin.RelationPhone;

                //入院途径
                cmbInPath.Tag = info.InPath;
                //病人来源
                txtInAvenue.Text = info.InAvenue;
                //入院日期
                FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

                int DtInDept = ctrlParamIntegrate.GetControlParam<int>("CASE03", true, 0);
                if (DtInDept == 1)
                {
                    FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
                    DateTime dtArrive = radtIntegrate.GetArriveDate(info.PatientInfo.ID);
                    if (dtArrive == System.DateTime.MinValue)
                    {
                        dtDateIn.Value = info.PatientInfo.PVisit.InTime;
                        //接诊时间
                        //dtDateIn.Value = dtArrive;
                    }
                    else
                    {
                        dtDateIn.Value = dtArrive;
                    }
                }
                else
                {
                    FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
                    DateTime dtArrive = radtIntegrate.GetArriveDate(info.PatientInfo.ID);
                    if (info.PatientInfo.PVisit.InTime != System.DateTime.MinValue)
                    {
                        //dtDateIn.Value = info.PatientInfo.PVisit.InTime;
                        //接诊时间
                        dtDateIn.Value = dtArrive;
                    }
                    else
                    {
                        dtDateIn.Value = System.DateTime.Now;
                    }
                }
                #region  入院科室，出院科室
                if (info.PatientInfo.CaseState == "1")//从fin_ipr_inmaininfo 中获取的数据
                {
                    FS.HISFC.Models.RADT.Location indept = baseDml.GetDeptIn(info.PatientInfo.ID);
                    //取接诊科室 ren.jch
                    //FS.HISFC.Models.RADT.Location indept = baseDml.GetDeptIn1(info.PatientInfo.ID);
                    if (indept != null) //入院科室 
                    {
                        CaseBase.InDept.ID = indept.Dept.ID;
                        CaseBase.InDept.Name = indept.Dept.Name;
                        //入院科室代码
                        cmbDeptInHospital.Tag = indept.Dept.ID;
                    }
                    else
                    {
                        //入院科室代码
                        cmbDeptInHospital.Tag = info.PatientInfo.PVisit.PatientLocation.Dept.ID;
                    }
                    //出院科室
                    CaseBase.OutDept.ID = info.PatientInfo.PVisit.PatientLocation.Dept.ID;
                    CaseBase.OutDept.Name = info.PatientInfo.PVisit.PatientLocation.Dept.Name;
                   
                    ////出院科室代码
                    //cmbDeptOutHospital.Tag = info.PatientInfo.PVisit.PatientLocation.Dept.ID;
                }
                else
                {
                    //入院科室代码
                    cmbDeptInHospital.Tag = info.InDept.ID;
                    ////出院科室代码
                    //cmbDeptOutHospital.Tag = info.OutDept.ID;
                }
                //出院科室代码
                cmbDeptOutHospital.Tag = this.dept_out;
                #endregion
                if (info.InRoom == null || info.InRoom == "")
                {
                    string changeBed = this.deptChange.QueryWardNoBedNOByInpatienNO(info.PatientInfo.ID, "1");
                    string WardNoBedNO = this.deptChange.QueryWardNoBedNOByInpatienNO(changeBed, "2");
                    //入院病室
                    this.txtInRoom.Text = WardNoBedNO;
                }
                else
                {
                    this.txtInRoom.Text = info.InRoom;
                    //this.txtInRoom.Text = this.bedNo.Substring(4);
                }
                #region 转科科别

                //保存转科信息的列表
                ArrayList changeDept = new ArrayList();
                //获取转科信息
                changeDept = this.deptChange.QueryChangeDeptFromShiftApply(info.PatientInfo.ID, "2");
                if (changeDept != null && changeDept.Count > 0)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (i >= changeDept.Count)
                        {
                            break;
                        }
                        if (i == 0)
                        {
                            FS.HISFC.Models.RADT.Location locaInfo = changeDept[0] as FS.HISFC.Models.RADT.Location;
                            this.dtFirstTime.Value = FS.FrameWork.Function.NConvert.ToDateTime(locaInfo.Dept.Memo);
                            this.txtFirstDept.Tag = locaInfo.Dept.ID;
                        }
                        else if (i == 1)
                        {
                            FS.HISFC.Models.RADT.Location locaInfo = changeDept[1] as FS.HISFC.Models.RADT.Location;
                            this.dtSecondTime.Value = FS.FrameWork.Function.NConvert.ToDateTime(locaInfo.Dept.Memo);
                            this.txtDeptSecond.Tag = locaInfo.Dept.ID;
                        }
                        else if (i == 2)
                        {
                            FS.HISFC.Models.RADT.Location locaInfo = changeDept[2] as FS.HISFC.Models.RADT.Location;
                            this.dtThirdTime.Value = FS.FrameWork.Function.NConvert.ToDateTime(locaInfo.Dept.Memo);
                            this.txtDeptThird.Tag = locaInfo.Dept.ID;
                        }

                    }
                }
                else
                {
                    changeDept = new ArrayList();
                    //获取转科信息
                    changeDept = this.deptChange.QueryChangeDeptFromShiftApply(info.PatientInfo.ID, "1");
                    if (changeDept != null)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (i >= changeDept.Count)
                            {
                                break;
                            }
                            if (i == 0)
                            {
                                FS.HISFC.Models.RADT.Location locaInfo = changeDept[0] as FS.HISFC.Models.RADT.Location;
                                this.dtFirstTime.Value = FS.FrameWork.Function.NConvert.ToDateTime(locaInfo.Dept.Memo);
                                this.txtFirstDept.Tag = locaInfo.Dept.ID;
                            }
                            else if (i == 1)
                            {
                                FS.HISFC.Models.RADT.Location locaInfo = changeDept[1] as FS.HISFC.Models.RADT.Location;
                                this.dtSecondTime.Value = FS.FrameWork.Function.NConvert.ToDateTime(locaInfo.Dept.Memo);
                                this.txtDeptSecond.Tag = locaInfo.Dept.ID;
                            }
                            else if (i == 2)
                            {
                                FS.HISFC.Models.RADT.Location locaInfo = changeDept[2] as FS.HISFC.Models.RADT.Location;
                                this.dtThirdTime.Value = FS.FrameWork.Function.NConvert.ToDateTime(locaInfo.Dept.Memo);
                                this.txtDeptThird.Tag = locaInfo.Dept.ID;
                            }

                        }
                    }
                }
                #endregion
               
                //出院病室
                if (info.OutRoom == null || info.OutRoom == "")
                {
                    this.txtOutRoom.Text = deptChange.QueryWardNoBedNOByInpatienNO(this.bedNo, "2");
                }
                else
                {
                    this.txtOutRoom.Text = info.OutRoom;
                    //this.txtOutRoom.Text = this.bedNo.Substring(4);
                }
                //出院日期
                if (info.PatientInfo.PVisit.OutTime != System.DateTime.MinValue && this.in_State != "I")
                {
                    //是否默认获取护士登记出院时间 
                    ArrayList outTimelist = con.GetList("CASEOUTTIME");
                    //出院编辑时再取一次出院日期保证不错  但是死亡病人有可能把出院时间提前的，看医院取舍吧2013-1-15
                    if (outTimelist != null && outTimelist.Count > 0)
                    {
                        this.txtDateOut.Value = this.dt_out;
                    }
                    else
                    {
                        //txtDateOut.Value = info.PatientInfo.PVisit.OutTime;
                        this.txtDateOut.Value = this.dt_out;
                    }
                }
                else
                {
                    txtDateOut.Value = System.DateTime.Now;
                }

                //实际住院天数

                if (this.in_State == "I")
                {
                    System.TimeSpan tt = this.baseDml.GetDateTimeFromSysDateTime().Date - this.dtDateIn.Value.Date;
                    if (tt.Days == 0)
                    {
                        this.txtPiDays.Text = "1";
                    }
                    else
                    {
                        this.txtPiDays.Text = Convert.ToString(tt.Days);
                    }
                }
                else
                {
                    System.TimeSpan ts = this.txtDateOut.Value.Date - this.dtDateIn.Value.Date;
                    if (ts.Days == 0)
                    {
                        this.txtPiDays.Text = "1";
                    }
                    else
                    {
                        this.txtPiDays.Text = Convert.ToString(ts.Days);
                    }
                }

                //门诊诊断名称
                if (info.ClinicDiag.Name != null)
                {
                    this.cmbClinicDiagName.Text = Funtion.ReplaceSingleQuotationMarks(info.ClinicDiag.Name, false);
                }
                else
                {
                    this.cmbClinicDiagName.Text = "";
                }
                //门诊诊断编码
                this.txtClinicDiagCode.Text = info.ClinicDiag.ID;
                //门诊诊断医生 ID
                txtClinicDocd.Tag = info.ClinicDoc.ID;
                //病例分型
                cmbExampleType.Tag = info.ExampleType;
                //临床路径病例
                cmbClinicPath.Tag = info.ClinicPath;
                //抢救次数
                txtSalvTimes.Text = info.SalvTimes.ToString();
                //成功次数
                txtSuccTimes.Text = info.SuccTimes.ToString();
                //第二页内容
                //损伤中毒原因
                if (info.InjuryOrPoisoningCauseCode != null && info.InjuryOrPoisoningCauseCode != "")
                {
                    txtInjuryOrPoisoningCauseCode.Text = info.InjuryOrPoisoningCauseCode;
                }

                if (info.InjuryOrPoisoningCause != null)
                {
                    txtInjuryOrPoisoningCause.Text = Funtion.ReplaceSingleQuotationMarks(info.InjuryOrPoisoningCause, false);
                }
                else
                {
                    txtInjuryOrPoisoningCause.Text = "";
                }

                //病理诊断
                if (info.PathologicalDiagName != null)
                {
                    this.cmbPathologicalDiagName.Text = Funtion.ReplaceSingleQuotationMarks(info.PathologicalDiagName, false);
                }
                else
                {
                    this.cmbPathologicalDiagName.Text = "";
                }
                this.txtPathologicalDiagCode.Text = info.PathologicalDiagCode;

                this.txtPathologicalDiagNum.Text = info.PathNum;
                //药物过敏
                cmbIsDrugAllergy.Tag = info.AnaphyFlag;
                //过敏药物
                if (info.FirstAnaphyPharmacy.ID != null && info.FirstAnaphyPharmacy.ID.ToString() != "")
                {
                    this.txtDrugAllergy.Text = Funtion.ReplaceSingleQuotationMarks(info.FirstAnaphyPharmacy.ID, false);
                }
                else
                {
                    this.PharmacyAllergicLoadInfo(info.PatientInfo.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
                }
                //死亡患者尸检
                cmbDeathPatientBobyCheck.Tag = info.CadaverCheck;
                //血型编码
                if (info.PatientInfo.BloodType.ID != null)
                {
                    cmbBloodType.Tag = info.PatientInfo.BloodType.ID.ToString();
                }
                //Rh血型(阴阳)
                cmbRhBlood.Tag = info.RhBlood;
                //科主任代码
                cmbDeptChiefDoc.Tag = info.PatientInfo.PVisit.ReferringDoctor.ID;
                //主任医师代码
                cmbConsultingDoctor.Tag = info.PatientInfo.PVisit.ConsultingDoctor.ID;
                //主治医师代码
                cmbAttendingDoctor.Tag = info.PatientInfo.PVisit.AttendingDoctor.ID;
                //住院医师代码
                cmbAdmittingDoctor.Tag = info.PatientInfo.PVisit.AdmittingDoctor.ID;
                //责任护士
                if (info.DutyNurse.ID == "")
                {
                    cmbDutyNurse.Tag = this.dutyNurse;
                }
                else
                {
                    cmbDutyNurse.Tag = info.DutyNurse.ID;
                }
                //进修医师代码
                cmbRefresherDocd.Tag = info.RefresherDoc.ID;
                //实习医师代码
                cmbPraDocCode.Tag = info.PatientInfo.PVisit.TempDoctor.ID;
                //编码员
                txtCodingCode.Tag = info.CodingOper.ID;
                //病案质量
                cmbMrQual.Tag = info.MrQuality;
                //质控医师代码
                cmbQcDocd.Tag = info.QcDoc.ID;
                //质控护士代码
                cmbQcNucd.Tag = info.QcNurse.ID;
                //检查时间
                if (info.CheckDate != System.DateTime.MinValue)
                {
                    txtCheckDate.Value = info.CheckDate;
                }
                else
                {
                    txtCheckDate.Value = System.DateTime.Now;
                }
                if (info.PatientInfo.CaseState != "I" && info.CheckDate.Date < info.PatientInfo.PVisit.OutTime.Date)
                {
                    //txtCheckDate.Value = info.PatientInfo.PVisit.OutTime.AddDays(3);//已出院，质检日期小于出院日期 默认出院日期+3天
                    txtCheckDate.Value = info.PatientInfo.PVisit.OutTime;//原来是出院日期加3天，按需求改为默认是出院日期 by zhy
                }
                //离院方式
                txtLeaveHopitalType.Text = info.Out_Type;
                //医嘱转院接收医疗机构
                txtHighReceiveHopital.Text = info.HighReceiveHopital;
                //医嘱转社区
                txtLowerReceiveHopital.Text = info.LowerReceiveHopital;
                //出院31天内再住院计划
                cmbComeBackInMonth.Tag = info.ComeBackInMonth;
                //出院31天再住院目的
                cmbComeBackPurpose.Text = info.ComeBackPurpose;
                //颅脑损伤患者昏迷时间 -入院前 天
                txtOutComeDay.Text = info.OutComeDay.ToString();
                //颅脑损伤患者昏迷时间 -入院前 小时
                txtOutComeHour.Text = info.OutComeHour.ToString();
                //颅脑损伤患者昏迷时间 -入院前 分钟
                txtOutComeMin.Text = info.OutComeMin.ToString();
                //颅脑损伤患者昏迷时间 -入院后 天
                txtInComeDay.Text = info.InComeDay.ToString();
                //颅脑损伤患者昏迷时间 -入院后 小时
                txtInComeHour.Text = info.InComeHour.ToString();
                //颅脑损伤患者昏迷时间 -入院后 分钟
                txtInComeMin.Text = info.InComeMin.ToString();

                //有无手术 2012-9-19
                if (info.Ever_Sickintodeath == null)
                {
                    info.Ever_Sickintodeath = string.Empty;
                }
                this.ucOperation1.IsHavedOps = info.Ever_Sickintodeath;
                //有无妇婴信息
                if (info.Ever_Firstaid == null)
                {
                    info.Ever_Firstaid = string.Empty;
                }
                this.ucBabyCardInput1.IsHavedBaby = info.Ever_Firstaid;
                //有无肿瘤卡信息
                if (info.Ever_Difficulty == null)
                {
                    info.Ever_Difficulty = string.Empty;
                }
                this.ucTumourCard1.IsHavedTum = info.Ever_Difficulty;
                #region 兼容旧版本
                //入院来源
                txtInAvenue.Tag = info.PatientInfo.PVisit.InSource.ID;
                //入院状态                  
                txtCircs.Tag = info.PatientInfo.PVisit.Circs.ID;
                //确诊日期
                if (info.DiagDate != System.DateTime.MinValue)
                {
                    txtDiagDate.Value = info.DiagDate;
                }
                else
                {
                    txtDiagDate.Value = info.PatientInfo.PVisit.InTime;
                }
                //入院诊断
                txtRuyuanDiagNose.Text = Funtion.ReplaceSingleQuotationMarks(info.InHospitalDiag.Name, false);
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
                //临床_病理符合
                txtClPa.Tag = info.ClPa;
                //放射_病理符合
                txtFsBl.Tag = info.FsBl;
                //首次病例
                this.cmbYnFirst.Tag = info.YnFirst;
                //示教科研
                this.cmbTechSerc.Tag = info.TechSerc;
                //是否随诊
                this.cmbVisiStat.Tag = info.VisiStat;
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
                //单病种
                cmbDisease30.Tag = info.Disease30;
                //院际会诊次数
                txtInconNum.Text = info.InconNum.ToString();
                //远程会诊
                txtOutconNum.Text = info.OutconNum.ToString();
                //输血反应（有无）
                txtReactionBlood.Tag = info.ReactionBlood;
                //输液反应
                this.txtReactionTransfuse.Tag = info.ReactionLiquid;
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
                if (info.BodyAnotomize == "" || info.BodyAnotomize == null)
                {
                    txtBodyAnotomize.Text = "0";
                }
                else
                {
                    txtBodyAnotomize.Text = info.BodyAnotomize;
                }
                //全血数
                if (info.BloodWhole == "" || info.BodyAnotomize == null)
                {
                    txtBloodWhole.Text = "0";
                }
                else
                {
                    txtBloodWhole.Text = info.BloodWhole;
                }
                //其他输血数
                if (info.BloodOther == "" || info.BodyAnotomize == null)
                {
                    txtBloodOther.Text = "0";
                }
                else
                {
                    txtBloodOther.Text = info.BloodOther;
                }
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

                //获取护理数量
                if (info.PatientInfo.CaseState == "1")
                {
                    this.GetNursingNum(info.PatientInfo.ID);
                }
                else
                {
                    if (this.in_State == "I" || (info.PatientInfo.CaseState == "2" && info.PatientInfo.PVisit.OutTime.Date < this.dt_out.Date))
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
                #endregion
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
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

            //住院流水号
            info.PatientInfo.ID = CaseBase.PatientInfo.ID;
            info.IsHandCraft = CaseBase.IsHandCraft;

            //医疗付费方式
            if (cmbPactKind.Tag != null)
            {
                info.PatientInfo.Pact.ID = cmbPactKind.Tag.ToString();
            }
            else
            {
                info.PatientInfo.Pact.ID = "";
            }
            info.PatientInfo.Pact.PayKind.ID = "DRGS";
            //医保公费号
            info.PatientInfo.SSN = txtHealthyCard.Text;
            //住院次数
            info.PatientInfo.InTimes = FS.FrameWork.Function.NConvert.ToInt32(txtInTimes.Text);
            //病案号
            info.CaseNO = txtCaseNum.Text;
            info.CaseNO = info.CaseNO.PadLeft(10, '0');
            //住院号
            if (patient_no != "")
            {
                info.PatientInfo.PID.PatientNO = this.patient_no;//CaseBase.PatientInfo.PID.PatientNO;
            }
            else
            {
                info.PatientInfo.PID.PatientNO = CaseBase.PatientInfo.PID.PatientNO;
            }

            //姓名
            info.PatientInfo.Name = txtPatientName.Text;

            //性别
            if (cmbPatientSex.Tag != null)
            {
                info.PatientInfo.Sex.ID = cmbPatientSex.Tag;
            }
            else
            {
                info.PatientInfo.Sex.ID = CaseBase.PatientInfo.Sex.ID;
            }
            if (info.PatientInfo.Sex.ID == null)
            {
                info.PatientInfo.Sex.ID = "";
            }

            //出生日期
            info.PatientInfo.Birthday = dtPatientBirthday.Value;
            //年龄单位　不存年龄和单位
            info.AgeUnit = this.txtPatientAge.Text;
            info.PatientInfo.Age = "0";
            info.BabyAge = this.txtBabyAge.Text;

            //国籍
            if (cmbCountry.Tag != null)
            {
                info.PatientInfo.Country.ID = cmbCountry.Tag.ToString();
            }
            else
            {
                info.PatientInfo.Country.ID = "";
            }
            //新生儿出生体重
            info.BabyBirthWeight = this.txtBabyBirthWeight.Text;
            //新生儿入院体重
            info.BabyInWeight = this.txtBabyInWeight.Text;
            //出生地
            info.PatientInfo.AreaCode = this.cmbBirthAddr.Text;
            //籍贯
            info.PatientInfo.DIST = this.cmbDist.Text;

            //民族 
            if (cmbNationality.Tag != null)
            {
                info.PatientInfo.Nationality.ID = cmbNationality.Tag.ToString();
            }
            else
            {
                info.PatientInfo.Nationality.ID = "";
            }
            //身份证号
            info.PatientInfo.IDCard = txtIDNo.Text;
            //职业
            if (cmbProfession.Tag != null)
            {
                info.PatientInfo.Profession.ID = cmbProfession.Tag.ToString();
            }
            else
            {
                info.PatientInfo.Profession.ID = "";
            }
            //婚姻
            if (cmbMaritalStatus.Tag != null)
            {
                info.PatientInfo.MaritalStatus.ID = cmbMaritalStatus.Tag;
            }
            else
            {
                info.PatientInfo.MaritalStatus.ID = "";
            }
            // 现住址
            info.CurrentAddr = cmbCurrentAdrr.Text;
            //现住址电话
            info.CurrentPhone = txtCurrentPhone.Text;
            //现住址邮编
            info.CurrentZip = txtCurrentZip.Text;
            //户口住址
            info.PatientInfo.AddressHome = cmbHomeAdrr.Text;
            //户口住址邮编
            info.PatientInfo.HomeZip = txtHomeZip.Text;
            //工作单位及地址
            info.PatientInfo.AddressBusiness = txtAddressBusiness.Text;
            //单位电话
            info.PatientInfo.PhoneBusiness = txtPhoneBusiness.Text;
            //单位邮编
            info.PatientInfo.BusinessZip = txtBusinessZip.Text;
            //联系人名称
            info.PatientInfo.Kin.Name = txtKin.Text;
            //与患者关系
            if (cmbRelation.Tag != null)
            {
                info.PatientInfo.Kin.RelationLink = cmbRelation.Tag.ToString();
            }
            else
            {
                info.PatientInfo.Kin.RelationLink = "";
            }
            //联系人关系备注
            info.PatientInfo.Kin.Memo = txtRelationMemo.Text;
            //联系地址
            info.PatientInfo.Kin.RelationAddress = txtLinkmanAdd.Text;
            //联系电话
            info.PatientInfo.Kin.RelationPhone = txtLinkmanTel.Text;
            //入院途径
            if (cmbInPath.Tag != null)
            {
                info.InPath = cmbInPath.Tag.ToString();
            }
            else
            {
                info.InPath = "";
            }
            //入院日期
            info.PatientInfo.PVisit.InTime = dtDateIn.Value;
            //入院科室代码
            if (cmbDeptInHospital.Tag != null)
            {
                info.InDept.ID = cmbDeptInHospital.Tag.ToString();
            }
            else
            {
                info.InDept.ID = "";
            }
            //入院科室名称
            info.InDept.Name = cmbDeptInHospital.Text;
            //入院病房
            info.InRoom = this.txtInRoom.Text;
            //转科科别
            changeDeptList = new ArrayList();
            FS.HISFC.Models.RADT.Location locationInfo = new FS.HISFC.Models.RADT.Location();
            if (this.txtFirstDept.Text != "")
            {
                locationInfo.User02 = info.PatientInfo.ID;
                locationInfo.User03 = "1";
                locationInfo.Dept.Memo = this.dtFirstTime.Value.ToString();
                if (this.txtFirstDept.Tag != null)
                {
                    locationInfo.Dept.ID = this.txtFirstDept.Tag.ToString();
                    locationInfo.Dept.Name = this.txtFirstDept.Text;
                }
                this.changeDeptList.Add(locationInfo);

                if (this.txtDeptSecond.Text != "")
                {
                    locationInfo = new FS.HISFC.Models.RADT.Location();
                    locationInfo.User02 = info.PatientInfo.ID;
                    locationInfo.User03 = "2";
                    locationInfo.Dept.Memo = this.dtSecondTime.Value.ToString();
                    if (this.txtDeptSecond.Tag != null)
                    {
                        locationInfo.Dept.ID = this.txtDeptSecond.Tag.ToString();
                        locationInfo.Dept.Name = this.txtDeptSecond.Text;
                    }
                    this.changeDeptList.Add(locationInfo);
                    if (this.txtDeptThird.Text != "")
                    {
                        locationInfo = new FS.HISFC.Models.RADT.Location();
                        locationInfo.User02 = info.PatientInfo.ID;
                        locationInfo.User03 = "3";
                        locationInfo.Dept.Memo = this.dtThirdTime.Value.ToString();
                        if (this.txtDeptThird.Tag != null)
                        {
                            locationInfo.Dept.ID = this.txtDeptThird.Tag.ToString();
                            locationInfo.Dept.Name = this.txtDeptThird.Text;
                        }
                        this.changeDeptList.Add(locationInfo);
                    }
                }
            }

            //出院日期
            info.PatientInfo.PVisit.OutTime = txtDateOut.Value;
            //出院科室代码
            if (cmbDeptOutHospital.Tag != null)
            {
                info.OutDept.ID = cmbDeptOutHospital.Tag.ToString();
            }
            else
            {
                info.OutDept.ID = "";
            }
            //出院科室名称
            info.OutDept.Name = cmbDeptOutHospital.Text;
            //出院病房
            info.OutRoom = this.txtOutRoom.Text;
            //实际住院天数
            info.InHospitalDays = FS.FrameWork.Function.NConvert.ToInt32(txtPiDays.Text);
            //门诊诊断
            info.ClinicDiag.Name = Funtion.ReplaceSingleQuotationMarks(this.cmbClinicDiagName.Text, true);
            //门诊诊断编码
            info.ClinicDiag.ID = this.txtClinicDiagCode.Text;
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

            //病例分型
            if (cmbExampleType.Tag != null)
            {
                info.ExampleType = cmbExampleType.Tag.ToString();
            }
            else
            {
                info.ExampleType = "";
            }
            //临床路径病例
            if (cmbClinicPath.Tag != null)
            {
                info.ClinicPath = cmbClinicPath.Tag.ToString();
            }
            else
            {
                info.ClinicPath = "2";
            }
            //抢救次数
            info.SalvTimes = FS.FrameWork.Function.NConvert.ToInt32(txtSalvTimes.Text.Trim());
            //成功次数
            info.SuccTimes = FS.FrameWork.Function.NConvert.ToInt32(txtSuccTimes.Text.Trim());

            //损伤中毒原因

            info.InjuryOrPoisoningCauseCode = this.txtInjuryOrPoisoningCauseCode.Text;
            info.InjuryOrPoisoningCause = Funtion.ReplaceSingleQuotationMarks(this.txtInjuryOrPoisoningCause.Text, true);


            //病理诊断

            info.PathologicalDiagName = Funtion.ReplaceSingleQuotationMarks(this.cmbPathologicalDiagName.Text, true);

            info.PathologicalDiagCode = this.txtPathologicalDiagCode.Text;

            info.PathNum = this.txtPathologicalDiagNum.Text;
            //药物过敏
            if (this.cmbIsDrugAllergy.Tag != null)
            {
                info.AnaphyFlag = this.cmbIsDrugAllergy.Tag.ToString();
            }
            else
            {
                info.AnaphyFlag = "";
            }
            //过敏药物
            info.FirstAnaphyPharmacy.ID = Funtion.ReplaceSingleQuotationMarks(this.txtDrugAllergy.Text, true);

            //死亡患者尸检
            if (cmbDeathPatientBobyCheck.Tag != null)
            {
                info.CadaverCheck = cmbDeathPatientBobyCheck.Tag.ToString();
            }
            else
            {
                info.CadaverCheck = "";
            }


            //血型编码
            info.PatientInfo.BloodType.ID = cmbBloodType.Tag;
            //Rh血型(阴阳)
            if (cmbRhBlood.Tag != null)
            {
                info.RhBlood = cmbRhBlood.Tag.ToString();
            }
            else
            {
                info.RhBlood = "";
            }
            //科主任代码
            if (cmbDeptChiefDoc.Tag != null)
            {
                info.PatientInfo.PVisit.ReferringDoctor.ID = cmbDeptChiefDoc.Tag.ToString();
                info.PatientInfo.PVisit.ReferringDoctor.Name = cmbDeptChiefDoc.Text;
            }
            else
            {
                info.PatientInfo.PVisit.ReferringDoctor.ID = "";
                info.PatientInfo.PVisit.ReferringDoctor.Name = "";
            }
            //主任医师代码
            if (cmbConsultingDoctor.Tag != null)
            {
                info.PatientInfo.PVisit.ConsultingDoctor.ID = cmbConsultingDoctor.Tag.ToString();
                info.PatientInfo.PVisit.ConsultingDoctor.Name = cmbConsultingDoctor.Text;
            }
            else
            {
                info.PatientInfo.PVisit.ConsultingDoctor.ID = "";
                info.PatientInfo.PVisit.ConsultingDoctor.Name = "";
            }
            //主治医师代码
            if (cmbAttendingDoctor.Tag != null)
            {
                info.PatientInfo.PVisit.AttendingDoctor.ID = cmbAttendingDoctor.Tag.ToString();
                info.PatientInfo.PVisit.AttendingDoctor.Name = cmbAttendingDoctor.Text;
            }
            else
            {
                info.PatientInfo.PVisit.AttendingDoctor.ID = "";
                info.PatientInfo.PVisit.AttendingDoctor.Name = "";
            }
            //住院医师代码
            if (cmbAdmittingDoctor.Tag != null)
            {
                info.PatientInfo.PVisit.AdmittingDoctor.ID = cmbAdmittingDoctor.Tag.ToString();
                //住院医师姓名
                info.PatientInfo.PVisit.AdmittingDoctor.Name = cmbAdmittingDoctor.Text;
            }
            else
            {
                info.PatientInfo.PVisit.AdmittingDoctor.ID = "";
                //住院医师姓名
                info.PatientInfo.PVisit.AdmittingDoctor.Name = "";
            }
            //责任护士
            if (cmbDutyNurse.Tag != null)
            {
                info.DutyNurse.ID = cmbDutyNurse.Tag.ToString();
                info.DutyNurse.Name = cmbDutyNurse.Text.Trim();
            }
            else
            {
                info.DutyNurse.ID = "";
                info.DutyNurse.Name = "";
            }
            //进修医师代码
            if (cmbRefresherDocd.Tag != null)
            {
                info.RefresherDoc.ID = cmbRefresherDocd.Tag.ToString();
                info.RefresherDoc.Name = cmbRefresherDocd.Text;
            }
            else
            {
                info.RefresherDoc.ID = "";
                info.RefresherDoc.Name = "";
            }
            //实习医师代码
            if (cmbPraDocCode.Tag != null)
            {
                info.PatientInfo.PVisit.TempDoctor.ID = cmbPraDocCode.Tag.ToString();
                info.PatientInfo.PVisit.TempDoctor.Name = cmbPraDocCode.Text.Trim();
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
            if (cmbMrQual.Tag != null)
            {
                info.MrQuality = cmbMrQual.Tag.ToString();
            }
            else
            {
                info.MrQuality = "";
            }
            //质控医师代码
            if (cmbQcDocd.Tag != null)
            {
                info.QcDoc.ID = cmbQcDocd.Tag.ToString();
                info.QcDoc.Name = cmbQcDocd.Text.Trim();
            }
            else
            {
                info.QcDoc.ID = "";
                info.QcDoc.Name = "";
            }
            //质控护士代码
            if (cmbQcNucd.Tag != null)
            {
                info.QcNurse.ID = cmbQcNucd.Tag.ToString();
                info.QcNurse.Name = cmbQcNucd.Text.Trim();
            }
            else
            {
                info.QcNurse.ID = "";
                info.QcNurse.Name = "";
            }
            //检查时间
            info.CheckDate = txtCheckDate.Value;

            //离院方式
            info.Out_Type = txtLeaveHopitalType.Text;
            //医嘱转院接收医疗机构
            info.HighReceiveHopital = txtHighReceiveHopital.Text;
            //医嘱转社区
            info.LowerReceiveHopital = txtLowerReceiveHopital.Text;

            //出院31天内再住院计划
            if (cmbComeBackInMonth.Tag != null)
            {
                info.ComeBackInMonth = cmbComeBackInMonth.Tag.ToString();
            }
            else
            {
                info.ComeBackInMonth = "1";//1无 2有
            }
            //出院31天再住院目的

            info.ComeBackPurpose = cmbComeBackPurpose.Text;

            //颅脑损伤患者昏迷时间 -入院前 天
            info.OutComeDay = FS.FrameWork.Function.NConvert.ToInt32(txtOutComeDay.Text);

            //颅脑损伤患者昏迷时间 -入院前 小时
            info.OutComeHour = FS.FrameWork.Function.NConvert.ToInt32(txtOutComeHour.Text);

            //颅脑损伤患者昏迷时间 -入院前 分钟
            info.OutComeMin = FS.FrameWork.Function.NConvert.ToInt32(txtOutComeMin.Text);

            //颅脑损伤患者昏迷时间 -入院后 天
            info.InComeDay = FS.FrameWork.Function.NConvert.ToInt32(txtInComeDay.Text);
            //颅脑损伤患者昏迷时间 -入院后 小时
            info.InComeHour = FS.FrameWork.Function.NConvert.ToInt32(txtInComeHour.Text);

            //颅脑损伤患者昏迷时间 -入院后 分钟
            info.InComeMin = FS.FrameWork.Function.NConvert.ToInt32(txtInComeMin.Text);

            info.LendStat = "1"; //病案借阅状态 0 为借出 1为在架 
            if (this.frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC && (this.CaseBase.PatientInfo.CaseState == "1" || string.IsNullOrEmpty(this.CaseBase.PatientInfo.CaseState)))
            {
                info.PatientInfo.CaseState = "2";
            }
            else if (this.frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS) //病案室整理病案
            {
                info.PatientInfo.CaseState = "3";
            }
            else
            {
                info.PatientInfo.CaseState = this.CaseBase.PatientInfo.CaseState;
            }
            //是否有并发症
            info.SyndromeFlag = this.ucDiagNoseInput1.GetSyndromeFlag();
            if (this.CaseBase.LendStat == null || this.CaseBase.LendStat == "") //病案借阅状态 
            {
                info.LendStat = "I";
            }
            else
            {
                info.LendStat = this.CaseBase.LendStat;
            }
            info.UploadStatu = "0";
            info.IsDrgs = "1";

            //有无手术 2012-9-19
            info.Ever_Sickintodeath = this.ucOperation1.IsHavedOps;
            //有无妇婴卡信息 2012-9-19
            info.Ever_Firstaid = this.ucBabyCardInput1.IsHavedBaby;
            //有无肿瘤卡信息 2012-9-19
            info.Ever_Difficulty = this.ucTumourCard1.IsHavedTum;
            #region 兼容旧版本数据
            //家庭电话
            info.PatientInfo.PhoneHome = this.CaseBase.PatientInfo.PhoneHome;
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
            //入院诊断
            info.InHospitalDiag.Name = Funtion.ReplaceSingleQuotationMarks(this.txtRuyuanDiagNose.Text, true);
            //研究生实习医师代码
            info.GraduateDoc.ID = this.CaseBase.GraduateDoc.ID;
            info.GraduateDoc.Name = this.CaseBase.GraduateDoc.Name;
            //转来医院
            info.ComeFrom = this.CaseBase.ComeFrom;
            //转往何医院
            info.OutDept.Memo = this.CaseBase.OutDept.Memo;
            //感染部位
            info.InfectionPosition.Name = this.CaseBase.InfectionPosition.Name;
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


            //示教科研
            if (this.cmbTechSerc.Tag != null)
            {
                info.TechSerc = this.cmbTechSerc.Tag.ToString();
            }
            else
            {
                info.TechSerc = "2";
            }
            //是否随诊
            if (this.cmbVisiStat.Tag != null)
            {
                info.VisiStat = this.cmbVisiStat.Tag.ToString();
            }
            else
            {
                info.VisiStat = "2";
            }
            //随访期限 周
            info.VisiPeriodWeek = this.CaseBase.VisiPeriodWeek;
            //随访期限 月
            info.VisiPeriodMonth = this.CaseBase.VisiPeriodMonth;
            //随访期限 年
            info.VisiPeriodYear = this.CaseBase.VisiPeriodYear;

            //手术操作治疗检查诊断为本院第一例项目
            if (this.cmbYnFirst.Tag != null)
            {
                info.YnFirst = this.cmbYnFirst.Tag.ToString();
            }
            else
            {
                info.YnFirst = "2";
            }
            //单病种 
            if (this.cmbDisease30.Tag != null)
            {
                info.Disease30 = this.cmbDisease30.Tag.ToString();
            }
            else
            {
                info.Disease30 = "2";
            }
            //院际会诊次数
            info.InconNum = FS.FrameWork.Function.NConvert.ToInt32(txtInconNum.Text.Trim());
            //远程会诊
            info.OutconNum = FS.FrameWork.Function.NConvert.ToInt32(txtOutconNum.Text.Trim());
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
            //输液反应
            if (this.txtReactionTransfuse.Tag != null)
            {
                info.ReactionLiquid = this.txtReactionTransfuse.Tag.ToString();
            }
            else
            {
                info.ReactionLiquid = "";
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
            //红细胞数
            info.BloodRed = txtBloodRed.Text;
            //血小板数
            info.BloodPlatelet = txtBloodPlatelet.Text;
            //血浆数
            info.BodyAnotomize = txtBodyAnotomize.Text;
            //全血数
            info.BloodWhole = txtBloodWhole.Text;
            //其他输血数
            info.BloodOther = txtBloodOther.Text;
            //X光号
            info.XNum = this.CaseBase.XNum;
            //CT号
            info.CtNum = this.CaseBase.CtNum;
            //MRI号
            info.MriNum = this.CaseBase.MriNum;
            //B超号
            info.DsaNum = this.CaseBase.DsaNum;
            //PET 号
            info.PetNum = this.CaseBase.PetNum;
            //ECT号
            info.EctNum = this.CaseBase.EctNum;
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

            info.OperInfo.ID = this.CaseBase.OperInfo.ID;

            //整理员 
            info.PackupMan.ID = this.CaseBase.PackupMan.ID;

            info.OperationCoding.ID = this.CaseBase.OperationCoding.ID;

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
                //使用最新电子病历2012-9-11
                FS.FrameWork.Models.NeuObject NewEMRInfo = con.GetConstant("CASENEWEMR", "1");
                if (NewEMRInfo != null && NewEMRInfo.Memo == "1")
                {
                    this.isNewEMR = true;
                    this.InitCountryList();
                }

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
                this.dutyNurse = patientInfo.PVisit.AdmittingNurse.ID;
                this.patient_no = patientInfo.PID.PatientNO;
                patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                //end add chengym
                CaseBase = baseDml.GetCaseBaseInfo(InpatientNo);//病案主表信息（met_cas_base）

                if (CaseBase == null)
                {
                    CaseBase = new FS.HISFC.Models.HealthRecord.Base();
                    CaseBase.PatientInfo.ID = InpatientNo;
                    CaseBase.PatientInfo.PID.PatientNO = this.PatientNo;
                    CaseBase.PatientInfo.PID.CardNO = this.CardNo;
                }
                else
                {
                    ArrayList DrgsTimesList = this.con.GetList("CASEDRGSUSEDTIME");
                    if (DrgsTimesList != null && DrgsTimesList.Count > 0)
                    {
                        DateTime dt = this.con.GetDateTimeFromSysDateTime().Date.AddDays(10);
                        FS.FrameWork.Models.NeuObject usedTime = DrgsTimesList[0] as FS.FrameWork.Models.NeuObject;
                        try
                        {
                            dt = FS.FrameWork.Function.NConvert.ToDateTime(usedTime.Memo);
                        }
                        catch
                        {
                        }
                        if (dt.Date != this.con.GetDateTimeFromSysDateTime().Date.AddDays(10)
                            && dt.Date > this.dt_out.Date && CaseBase.PatientInfo.Pact.PayKind.ID != "DRGS")
                        {
                            MessageBox.Show("该患者填写的是旧病案首页，请使用旧病案首页节点（上一个节点）查看！", "提示");
                            return -1;
                        }
                    }
                }
                //1. 如果病案表中没有信息 则去住院表中去查询
                //2. 如果 住院主表中有记录则提取信息 写到界面上. 
                if (CaseBase.PatientInfo.ID == "" || CaseBase.OperInfo.OperTime == DateTime.MinValue)//病案主表中没有记录
                {
                    #region 病案中（met_cas_base）没有记录
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
                this.frmType = Type;

                //如果是手工录入病案 可能查询出来的信息都为空 只有传入的InpatientNo 不为空
                if (CaseBase.PatientInfo.CaseState == "0")
                {
                    MessageBox.Show("该病人不允许有病案");
                    return 0;
                }
                //保存病案的状态
                CaseFlag = FS.FrameWork.Function.NConvert.ToInt32(CaseBase.PatientInfo.CaseState);

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

                        //CaseBase = baseDml.GetCaseBaseInfo(CaseBase.PatientInfo.ID);
                        //CaseBase.PatientInfo.CaseState = CaseFlag.ToString();
                        //if (CaseBase == null)
                        //{
                        //    MessageBox.Show("查询病案失败" + baseDml.Err);
                        //    return -1;
                        //}

                        //填充数据 
                        ConvertInfoToPanel(CaseBase);
                        //医生提交后，不允许编辑
                        if (this.baseDml.GetEmrQcCommit(CaseBase.PatientInfo.ID) > 0)
                        {
                            SetReadOnly(true);
                        }
                        else
                        {
                            SetReadOnly(false);
                        }
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
                        SetReadOnly(false);
                    }
                    else if (CaseBase.PatientInfo.CaseState == "2" || CaseBase.PatientInfo.CaseState == "3")
                    {
                        //从病案基本表中获取信息 并显示在界面上 
                        //CaseBase = baseDml.GetCaseBaseInfo(CaseBase.PatientInfo.ID);
                        //CaseBase.PatientInfo.CaseState = CaseFlag.ToString();
                        //if (CaseBase == null)
                        //{
                        //    MessageBox.Show("查询病案失败" + baseDml.Err);
                        //    return -1;
                        //}
                        //填充数据 
                        ConvertInfoToPanel(CaseBase);
                        SetReadOnly(true);
                    }
                    else if ((CaseBase.PatientInfo.CaseState == "" || CaseBase.PatientInfo.CaseState == null) && CaseBase.IsHandCraft == "1")
                    {
                        //填充数据
                        ConvertInfoToPanel(CaseBase);
                        SetReadOnly(false);
                    }
                    else
                    {
                        //病案已经封存 不允许修改。
                        ConvertInfoToPanel(CaseBase);
                        this.SetReadOnly(true); //设为只读 
                    }
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
                this.ucTumourCard1.LoadInfo(CaseBase.PatientInfo, frmType);
                #endregion
                #region 转科
                //this.ucChangeDept1.LoadInfo(CaseBase.PatientInfo, changeDept);
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
                this.ucFeeInfo1.LoadInfoDrgs(CaseBase.PatientInfo);
                #endregion

                //显示基本信息
                this.tab1.SelectedIndex = 0;
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

        private void pactKind_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtHealthyCard.Focus();
            }
        }
        private void txtHealthyCard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtInTimes.Focus();
            }
        }
        private void txtInTimes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtCaseNum.Focus();
            }
        }

        private void caseNum_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtPatientName.Focus();
            }
            else if (e.KeyData == Keys.Divide)
            {
                return;
            }
        }
        private void PatientName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbPatientSex.Focus();
            }
        }
        //性别
        private void PatientSex_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                dtPatientBirthday.Focus();
            }
        }
        private void patientBirthday_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbCountry.Focus();
            }
        }

        private void PatientAge_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbCountry.Focus();
            }
        }

        #region 国籍
        private void Country_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbNationality.Focus();
            }
        }
        #endregion
        private void txtBabyAge_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtBabyBirthWeight.Focus();
            }
        }
        private void txtBabyBirthWeight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtBabyInWeight.Focus();
            }
        }
        private void txtBabyInWeight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbBirthAddr.Focus();
            }
        }

        private void cmbBirthAddr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbDist.Focus();
            }
        }
        private void cmbDist_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtIDNo.Focus();
            }
        }
        #region  民族
        private void Nationality_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtBabyBirthWeight.Focus();
            }
        }
        #endregion
        private void IDNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbProfession.Focus();
            }
        }
        #region 职业
        private void Profession_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbMaritalStatus.Focus();
            }
        }
        #endregion
        #region 婚姻
        private void MaritalStatus_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbCurrentAdrr.Focus();
            }
        }
        #endregion
        private void cmbCurrentAdrr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtCurrentPhone.Focus();
            }
        }
        private void txtCurrentPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtCurrentZip.Focus();
            }
        }
        private void txtCurrentZip_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbHomeAdrr.Focus();
            }

        }
        private void cmbHomeAdrr_KeyDown(object sender, KeyEventArgs e)
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
                this.txtAddressBusiness.Focus();
            }
        }
        private void AddressBusiness_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
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
                this.txtBusinessZip.Focus();
            }
        }
        private void BusinessZip_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
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
                this.cmbRelation.Focus();
            }
        }
        #region 联系人关系
        private void Relation_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtLinkmanAdd.Focus();
            }
        }
        #endregion
        private void LinkmanAdd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtLinkmanTel.Focus();

            }
        }

        private void LinkmanTel_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbInPath.Focus();
            }
        }
        #region 病人来源
        private void InAvenue_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtInAvenue.Focus();
            }
        }
        private void txtInAvenue_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.dtDateIn.Focus();
            }
        }
        #endregion
        private void Date_In_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbDeptInHospital.Focus();
            }
        }
        private void txtInRoom_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.dtFirstTime.Focus();
            }
        }
        private void dtFirstTime_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtFirstDept.Focus();
            }
        }
        private void txtFirstDept_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.dtSecondTime.Focus();
            }
        }
        private void dtSecondTime_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtDeptSecond.Focus();
            }
        }
        private void txtDeptSecond_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.dtThirdTime.Focus();
            }
        }
        private void dtThirdTime_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtDeptThird.Focus();
            }
        }
        private void txtDeptThird_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtDateOut.Focus();
            }
        }
        private void txtOutRoom_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtPiDays.Focus();
            }
        }
        #region  入院科室
        private void DeptInHospital_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtInRoom.Focus();
            }
        }
        #endregion
        private void txtChangeDept_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtDateOut.Focus();
            }
        }
        private void Date_Out_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                System.TimeSpan tt = this.txtDateOut.Value.Date - this.dtDateIn.Value.Date;
                this.txtPiDays.Text = Convert.ToString(tt.Days);
                this.cmbDeptOutHospital.Focus();
            }
        }
        private void cmbDeptOutHospital_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtOutRoom.Focus();
            }
        }
        private void txtPiDays_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbClinicDiagName.Focus();
            }
        }
        private void cmbClinicDiagName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtClinicDiagCode.Focus();
            }
        }
        private void txtClinicDiagCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtClinicDocd.Focus();
            }
        }
        private void txtClinicDocd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbExampleType.Focus();
            }
        }
        private void cmbExampleType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbClinicPath.Focus();
            }
        }
        private void cmbClinicPath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtSalvTimes.Focus();
            }
        }
        private void txtSalvTimes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtSuccTimes.Focus();
            }
        }
        private void txtSuccTimes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtInjuryOrPoisoningCause.Focus();
            }
        }
        private void txtInjuryOrPoisoningCause_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbPathologicalDiagName.Focus();
            }
        }

        private void txtInjuryOrPoisoningCauseCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbPathologicalDiagName.Focus();
            }
        }
        private void cmbPathologicalDiagName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtPathologicalDiagCode.Focus();
            }
        }

        private void txtPathologicalDiagCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtPathologicalDiagNum.Focus();
            }
        }
        private void txtPathologicalDiagNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbIsDrugAllergy.Focus();
            }
        }
        private void cmbIsDrugAllergy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (cmbIsDrugAllergy.Tag != null && cmbIsDrugAllergy.Tag.ToString() == "2")
                {
                    this.txtDrugAllergy.Focus();
                }
                else
                {
                    this.cmbDeathPatientBobyCheck.Focus();
                }
            }
        }

        private void txtDrugAllergy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbDeathPatientBobyCheck.Focus();
            }
        }

        private void cmbDeathPatientBobyCheck_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbBloodType.Focus();
            }
        }

        #region  血型
        private void BloodType_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbRhBlood.Focus();
            }
        }
        #endregion
        #region  RH反应
        private void RhBlood_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtCePi.Focus();
            }
        }
        #endregion
        #region  门诊与出院
        private void txtCePi_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtClPa.Focus();
            }
        }
        #endregion
        #region  出院与病理
        private void txtClPa_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbDeptChiefDoc.Focus();
            }
        }
        #endregion
        private void cmbDeptChiefDoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbConsultingDoctor.Focus();
            }
        }
        #region 主任医师
        private void ConsultingDoctor_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                cmbAttendingDoctor.Focus();
            }
        }
        #endregion
        #region  主治医师
        private void AttendingDoctor_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                cmbAdmittingDoctor.Focus();
            }
        }
        #endregion
        #region  住院医生
        private void AdmittingDoctor_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbDutyNurse.Focus();
            }
        }
        #endregion
        #region 责任护士
        private void cmbDutyNurse_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                cmbRefresherDocd.Focus();
            }
        }
        #endregion
        #region 进修医师
        private void RefresherDocd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbPraDocCode.Focus();
            }
        }
        #endregion

        private void cmbPraDocCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtCodingCode.Focus();
            }
        }
        #region 编码员
        private void CodingCode_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbMrQual.Focus();
            }
        }
        #endregion
        #region 病案质量
        private void MrQual_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                cmbQcDocd.Focus();
            }
        }
        #endregion
        #region 质控医生
        private void QcDocd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                cmbQcNucd.Focus();
            }
        }
        #endregion
        private void cmbQcNucd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtCheckDate.Focus();
            }
        }
        private void CheckDate_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtLeaveHopitalType.Focus();
            }
        }
        private void txtLeaveHopitalType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (this.txtLeaveHopitalType.Text.Trim() == "2")
                {
                    this.txtHighReceiveHopital.Focus();
                }
                else if (this.txtLeaveHopitalType.Text.Trim() == "3")
                {
                    this.txtLowerReceiveHopital.Focus();
                }
                else
                {
                    cmbComeBackInMonth.Focus();
                }
            }
        }
        private void txtHighReceiveHopital_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbComeBackInMonth.Focus();
            }
        }

        private void txtLowerReceiveHopital_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbComeBackInMonth.Focus();
            }
        }
        private void cmbComeBackInMonth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (cmbComeBackInMonth.Tag != null && cmbComeBackInMonth.Tag.ToString() == "2")
                {
                    this.cmbComeBackPurpose.Focus();
                }
                else
                {
                    txtOutComeDay.Focus();
                }
            }
        }

        private void cmbComeBackPurpose_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtOutComeDay.Focus();
            }
        }

        private void txtOutComeDay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtOutComeHour.Focus();
            }
        }
        private void txtOutComeHour_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtOutComeMin.Focus();
            }
        }

        private void txtOutComeMin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtInComeDay.Focus();
            }
        }
        private void txtInComeDay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtInComeHour.Focus();
            }
        }
        private void txtInComeHour_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtInComeMin.Focus();
            }
        }
        private void txtInComeMin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.tab1.SelectedTab = this.tabPage2;
            }
        }
        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// tab页切换
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //基本信息
            if (keyData == Keys.F1)
            {
                this.tab1.SelectedTab = this.tabPage1;
            }
            //诊断信息
            if (keyData == Keys.F2)
            {
                this.tab1.SelectedTab = this.tabPage2;
            }
            //手术信息
            if (keyData == Keys.F3)
            {
                this.tab1.SelectedTab = this.tabPage3;
            }
            //妇婴信息
            if (keyData == Keys.F4)
            {
                this.tab1.SelectedTab = this.tabPage4;
            }
            //肿瘤信息
            if (keyData == Keys.F5)
            {
                this.tab1.SelectedTab = this.tabPage5;
            }
            //费用信息
            if (keyData == Keys.F8)
            {
                this.tab1.SelectedTab = this.tabPage6;
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
            this.cmbDeptOutHospital.Focus();
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
                        this.txtDrugAllergy.Text = "未发现";
                    }
                    else
                    {
                        foreach (FS.FrameWork.Models.NeuObject info in inDiagnose)
                        {
                            this.txtDrugAllergy.Text = info.Memo.ToString();
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 门诊诊断 code赋值事件
        /// </summary>
        private void cmbClinicDiagName_SelectItemChangEvent()
        {
            if (cmbClinicDiagName.Tag != null)
            {
                this.txtClinicDiagCode.Text = cmbClinicDiagName.Tag.ToString();
            }
        }
        /// <summary>
        /// 损伤中毒 code赋值事件
        /// </summary>
        private void txtInjuryOrPoisoningCause_SelectItemChangEvent()
        {
            if (txtInjuryOrPoisoningCause.Tag != null)
            {
                this.txtInjuryOrPoisoningCauseCode.Text = txtInjuryOrPoisoningCause.Tag.ToString();
            }
        }
        /// <summary>
        /// 病理诊断 code赋值事件
        /// </summary>
        private void cmbPathologicalDiagName_SelectItemChangEvent()
        {
            if (cmbPathologicalDiagName.Tag != null)
            {
                this.txtPathologicalDiagCode.Text = cmbPathologicalDiagName.Tag.ToString();
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
        /// 药物过敏事件
        /// </summary>
        private void cmbIsDrugAllergy_SelectItemChangEvent()
        {
            if (cmbIsDrugAllergy.Tag != null)
            {
                if (cmbIsDrugAllergy.Tag.ToString() == "1")
                {
                    this.txtDrugAllergy.Enabled = false;
                    this.txtDrugAllergy.Text = "未发现";
                }
                else if (cmbIsDrugAllergy.Tag.ToString() == "2")
                {
                    this.txtDrugAllergy.Focus();
                    this.txtDrugAllergy.Enabled = true;
                    this.txtDrugAllergy.Text = "";
                }
            }
        }
        private void cmbComeBackInMonth_SelectItemChangEvent()
        {
            if (cmbComeBackInMonth.Tag != null)
            {
                if (cmbComeBackInMonth.Tag.ToString() == "1")
                {
                    this.cmbComeBackPurpose.Enabled = false;
                    this.cmbComeBackPurpose.Text = "无";
                }
                else
                {
                    this.cmbComeBackPurpose.Focus();
                    this.cmbComeBackPurpose.Enabled = true;
                    this.cmbComeBackPurpose.Text = "";
                }
            }
        }
        private void txtLeaveHopitalType_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if ((int)e.KeyChar <= 32)
            {
                return;
            }
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                return;
            }
        }
        private void txtLeaveHopitalType_Leave(object sender, System.EventArgs e)
        {
            switch (this.txtLeaveHopitalType.Text.Trim())
            {
                case "1":
                    this.txtHighReceiveHopital.Text = "-";
                    this.txtLowerReceiveHopital.Text = "-";
                    break;
                case "2":
                    this.txtLowerReceiveHopital.Text = "-";
                    break;
                case "3":
                    this.txtHighReceiveHopital.Text = "-";
                    break;
                case "4":
                    this.txtHighReceiveHopital.Text = "-";
                    this.txtLowerReceiveHopital.Text = "-";
                    break;
                case "5":
                    this.txtHighReceiveHopital.Text = "-";
                    this.txtLowerReceiveHopital.Text = "-";
                    break;
                case "6":
                    this.txtHighReceiveHopital.Text = "-";
                    this.txtLowerReceiveHopital.Text = "-";
                    break;
                default:
                    break;
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
                    this.txtClinicDiagCode.Tag = obj.DiagInfo.ICD10.ID;
                    this.txtClinicDiagCode.Text = obj.DiagInfo.ICD10.Name;
                    //this.txtClinicDocd.Tag = obj.DiagInfo.Doctor.ID;
                    //this.txtClinicDocd.Text = obj.DiagInfo.Doctor.Name;
                    clinicDiag = obj;
                }
                else if (obj.DiagInfo.DiagType.ID == "11" && obj.DiagInfo.IsMain)
                {	//入院诊断
                    //txtRuyuanDiagNose.Tag = obj.DiagInfo.ICD10.ID;
                    //txtRuyuanDiagNose.Text = obj.DiagInfo.ICD10.Name;
                    InDiag = obj;
                }
            }
            #endregion

            #region 如果没有主诊断 则输入非主诊断诊断
            foreach (FS.HISFC.Models.HealthRecord.Diagnose obj in list)
            {
                if (obj.DiagInfo.DiagType.ID == "10")
                {	//门诊诊断 
                    if (this.txtClinicDiagCode.Tag == null)
                    {
                        this.txtClinicDiagCode.Tag = obj.DiagInfo.ICD10.ID;
                        this.txtClinicDiagCode.Text = obj.DiagInfo.ICD10.Name;
                        clinicDiag = obj;
                    }
                }
                else if (obj.DiagInfo.DiagType.ID == "11")
                {	//入院诊断
                    //if (txtRuyuanDiagNose.Tag == null)
                    //{
                    //    txtRuyuanDiagNose.Tag = obj.DiagInfo.ICD10.ID;
                    //    txtRuyuanDiagNose.Text = obj.DiagInfo.ICD10.Name;
                    //    InDiag = obj;
                    //}
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
            if (!ValidMaxLengh(info.PatientInfo.ID, 14))
            {
                MessageBox.Show("住院流水号过长");
                return -1;
            }
            if (!ValidMaxLengh(info.CaseNO, 10))
            {
                txtCaseNum.Focus();
                MessageBox.Show("病案号过长");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.PID.CardNO, 10))
            {
                MessageBox.Show("就诊卡号过长");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.SSN, 20))
            {
                txtHealthyCard.Focus();
                MessageBox.Show("医保号过长");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.Name, 20))
            {
                txtPatientName.Focus();
                MessageBox.Show("姓名过长");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.Country.ID, 3))
            {
                cmbCountry.Focus();
                MessageBox.Show("国籍编码过长");
                return -1;
            }

            if (!ValidMaxLengh(info.PatientInfo.Nationality.ID, 2))
            {
                cmbNationality.Focus();
                MessageBox.Show("民族编码过长");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.Profession.ID, 3))
            {
                cmbProfession.Focus();
                MessageBox.Show("职业编码过长");
                return -1;
            }
            if (info.PatientInfo.BloodType.ID != null)
            {
                if (!ValidMaxLengh(info.PatientInfo.BloodType.ID.ToString(), 2))
                {
                    cmbBloodType.Focus();
                    MessageBox.Show("血型编码过长");
                    return -1;
                }
            }
            if (info.PatientInfo.MaritalStatus.ID != null)
            {
                if (!ValidMaxLengh(info.PatientInfo.MaritalStatus.ID.ToString(), 10))
                {
                    cmbMaritalStatus.Focus();
                    MessageBox.Show("婚姻编码过长");
                    return -1;
                }
            }
            if (info.AgeUnit != null)
            {
                if (!ValidMaxLengh(info.AgeUnit, 15))
                {
                    txtPatientAge.Focus();
                    MessageBox.Show("年龄单位过长");
                    return -1;
                }
            }
            //判断没有转科的情况下入院科室与出院科室是否相同
            if ((txtFirstDept.Text=="") && (info.OutDept.ID.ToString() != info.InDept.ID.ToString()))
            {
                cmbDeptInHospital.Focus();
                MessageBox.Show("入院科室与出院科室不符,请核查");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.Age, 3))
            {
                txtPatientAge.Focus();
                MessageBox.Show("年龄过长");
                return -1;
            }
            //if (!ValidMaxLengh(info.PatientInfo.IDCard, 18))
            if (info.PatientInfo.Country.ID == "156")
            {
                if (this.ProcessIDENNO(info.PatientInfo.IDCard) != 1 && info.PatientInfo.IDCard != "未提供")
                {
                    txtIDNo.Focus();
                    MessageBox.Show("身份证号不合法，如果病人没提供请填写“未提供”");
                    return -1;
                }
            }
            if (!ValidMaxLengh(info.PatientInfo.Pact.PayKind.ID, 6))
            {
                cmbPactKind.Focus();
                MessageBox.Show("结算类别编码过长");
                return -1;
            }

            if (!ValidMaxLengh(info.PatientInfo.Pact.ID, 10))
            {
                cmbPactKind.Focus();
                MessageBox.Show("医疗付费方式过长");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.DIST, 50))
            {

                MessageBox.Show("籍贯过长");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.AddressHome, 100))
            {
                MessageBox.Show("家庭住址过长");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.PhoneHome, 25))
            {
                txtCurrentPhone.Focus();
                MessageBox.Show("家庭电话过长,如未提供请填写减号“-”");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.HomeZip, 6))
            {
                txtHomeZip.Focus();
                MessageBox.Show("住址邮编过长");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.AddressBusiness, 100))
            {
                txtAddressBusiness.Focus();
                MessageBox.Show("单位地址过长,如没有请填写减号“-”");
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
                MessageBox.Show("联系人过长,如未提供请填写减号“-”");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.Kin.RelationLink, 3))
            {
                cmbRelation.Focus();
                MessageBox.Show("联系人关系过长,如未提供请填写减号“-”");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.Kin.RelationAddress, 100))
            {
                txtLinkmanAdd.Focus();
                MessageBox.Show("联系地址过长,如未提供请填写减号“-”");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.Kin.RelationPhone, 25))
            {
                txtLinkmanTel.Focus();
                MessageBox.Show("联系电话过长,如未提供请填写减号“-”");
                return -1;
            }
            if (!ValidMaxLengh(info.ComeFrom, 100))
            {
                txtChangeDept.Focus();
                MessageBox.Show("转来医院");
                return -1;
            }
            if (info.PatientInfo.InTimes > 99)
            {
                txtChangeDept.Focus();
                MessageBox.Show("入院次数过大");
                return -1;
            }
            if (!ValidMaxLengh(info.BloodRed, 10))
            {
                txtOutComeDay.Focus();
                MessageBox.Show("红细胞数量过大");
                return -1;
            }
            if (!ValidMaxLengh(info.BloodPlatelet, 10))
            {
                txtOutComeHour.Focus();
                MessageBox.Show("血小板数量过大");
                return -1;
            }
            if (!ValidMaxLengh(info.BloodPlasma, 10))
            {
                txtOutComeMin.Focus();
                MessageBox.Show("血浆数量过大");
                return -1;
            }
            if (!ValidMaxLengh(info.BloodWhole, 10))
            {
                txtInComeDay.Focus();
                MessageBox.Show("全血数量过大");
                return -1;
            }
            if (!ValidMaxLengh(info.BloodOther, 10))
            {
                txtInComeHour.Focus();
                MessageBox.Show("其他输血数量过大");
                return -1;
            }
            if (!ValidMaxLengh(info.InjuryOrPoisoningCause, 200))
            {
                this.txtInjuryOrPoisoningCause.Focus();
                MessageBox.Show("损伤中毒的外部原因名称过长");
                return -1;
            }
            if (!ValidMaxLengh(info.PathologicalDiagName, 50))
            {
                this.cmbPathologicalDiagName.Focus();
                MessageBox.Show("病理诊断名称过长");
                return -1;
            }
            if (!ValidMaxLengh(info.HighReceiveHopital, 100))
            {
                this.txtHighReceiveHopital.Focus();
                MessageBox.Show("接收医疗机构名称过长");
                return -1;
            }
            if (!ValidMaxLengh(info.LowerReceiveHopital, 100))
            {
                this.txtLowerReceiveHopital.Focus();
                MessageBox.Show("转社区接收医疗机构名称过长");
                return -1;
            }
            if (!ValidMaxLengh(info.ComeBackPurpose, 100))
            {
                this.cmbComeBackPurpose.Focus();
                MessageBox.Show("再住院目的描述过长");
                return -1;
            }
            if (!ValidMaxLengh(info.OutComeHour.ToString(), 2))
            {
                this.txtOutComeHour.Focus();
                MessageBox.Show("颅脑损伤患者入院前昏迷小时数过大");
                return -1;
            }
            if (!ValidMaxLengh(info.OutComeMin.ToString(), 2))
            {
                this.txtOutComeMin.Focus();
                MessageBox.Show("颅脑损伤患者入院前昏迷分钟数过大");
                return -1;
            }
            if (!ValidMaxLengh(info.InComeHour.ToString(), 2))
            {
                this.txtInComeHour.Focus();
                MessageBox.Show("颅脑损伤患者入院后昏迷小时数过大");
                return -1;
            }
            if (!ValidMaxLengh(info.InComeMin.ToString(), 2))
            {
                this.txtInComeMin.Focus();
                MessageBox.Show("颅脑损伤患者入院后昏迷分钟数过大");
                return -1;
            }
            if (!ValidMaxLengh(info.CurrentAddr, 100))
            {
                this.cmbCurrentAdrr.Focus();
                MessageBox.Show("现住址过长");
                return -1;
            }
            if (!ValidMaxLengh(info.CurrentPhone, 18))
            {
                this.txtCurrentPhone.Focus();
                MessageBox.Show("现住址电话过长");
                return -1;
            }
            if (!ValidMaxLengh(info.CurrentZip, 6))
            {
                this.txtCurrentZip.Focus();
                MessageBox.Show("现住址邮编过长");
                return -1;
            }
            if (!ValidMaxLengh(info.InRoom, 20))
            {
                this.txtInRoom.Focus();
                MessageBox.Show("入院病房过长");
                return -1;
            }
            if (!ValidMaxLengh(info.OutRoom, 20))
            {
                this.txtOutRoom.Focus();
                MessageBox.Show("出院病房过长");
                return -1;
            }
            //add by 2011-9-20 chengym 后面打印时做判断，保存不再限制
            //if (info.PatientInfo.Pact.ID == null || info.PatientInfo.Pact.ID == "")
            //{
            //    cmbPactKind.Focus();
            //    MessageBox.Show("请选择“医疗付费方式”");
            //    return -1;
            //}
            //if (info.PatientInfo.Pact.ID == "1" || info.PatientInfo.Pact.ID == "2" || info.PatientInfo.Pact.ID == "3")
            //{
            //    if (this.txtHealthyCard.Text.Trim() == "")
            //    {
            //        this.txtHealthyCard.Focus();
            //        MessageBox.Show("医保患者请填入“健康卡号（医保号）”！");
            //        return -1;
            //    }
            //}
            //if (info.CurrentAddr == "" || info.CurrentAddr == "无" || info.CurrentAddr == "不详")
            //{
            //    this.cmbCurrentAdrr.Focus();
            //    MessageBox.Show("请填写详细的“现住址”！");
            //    return -1;
            //}
            //if (info.InPath == "")
            //{
            //    this.cmbInPath.Focus();
            //    MessageBox.Show("请填写“入院途径”！");
            //    return -1;
            //}
            //if (info.ExampleType == "")
            //{
            //    this.cmbExampleType.Focus();
            //    MessageBox.Show("请填写“病例分型”！");
            //    return -1;
            //}
            //if (info.ClinicPath == "")
            //{
            //    this.cmbClinicPath.Focus();
            //    MessageBox.Show("请填写“临床路径病例”！");
            //    return -1;
            //}
            //if (cmbDeptOutHospital.Text == "")
            //{
            //    MessageBox.Show("请填写出院科室信息");
            //    cmbDeptOutHospital.Focus();
            //    return -1;
            //}
            //if (cmbDeptInHospital.Text == "")
            //{
            //    MessageBox.Show("请填写入院科室信息");
            //    cmbDeptInHospital.Focus();
            //    return -1;
            //}
            //if (dtFirstTime.Value.Date < this.dtDateIn.Value.Date)
            //{
            //    MessageBox.Show("第一次转科时间不能小于入院时间");
            //    dtFirstTime.Focus();
            //    return -1;
            //}
            //if (dtFirstTime.Value.Date > this.txtDateOut.Value.Date)
            //{
            //    MessageBox.Show("第一次转科时间不能大于于出院时间");
            //    dtFirstTime.Focus();
            //    return -1;
            //}
            //if ((dtFirstTime.Value.Date > dtSecondTime.Value.Date) && txtDeptSecond.Text.Trim() != string.Empty)
            //{
            //    MessageBox.Show("第一次转科时间不能大于第二次转科时间");
            //    dtFirstTime.Focus();
            //    return -1;
            //}
            //if ((dtSecondTime.Value.Date > dtThirdTime.Value.Date) && txtDeptThird.Text.Trim() != string.Empty)
            //{
            //    MessageBox.Show("第二次转科时间不能大于第三次转科时间");
            //    dtSecondTime.Focus();
            //    return -1;
            //}
            //if (info.PatientInfo.PVisit.ReferringDoctor.ID == "" && info.PatientInfo.PVisit.ConsultingDoctor.ID == "" && info.PatientInfo.PVisit.AttendingDoctor.ID == "")
            //{
            //    MessageBox.Show("科主任、主任医师、主治医生都不能为空");
            //    return -1;
            //}

            //if (info.PatientInfo.PVisit.OutTime.Date < info.PatientInfo.PVisit.InTime.Date)
            //{
            //    MessageBox.Show("“出院日期”不能小于“入院时间”");
            //    return -1;
            //}
            //if (txtCheckDate.Value.Date > info.PatientInfo.PVisit.OutTime.Date.AddDays(15))
            //{
            //    this.txtCheckDate.Focus();
            //    MessageBox.Show("“质控日期”不能超过出院时间15天");
            //    return -1;
            //}
            if (info.SalvTimes < info.SuccTimes)
            {
                this.txtSuccTimes.Focus();
                MessageBox.Show("抢救成功次数不能大于抢救次数");
                return -1;
            }
            if (info.SuccTimes > 0 && info.SalvTimes - info.SuccTimes > 1)
            {
                this.txtSalvTimes.Focus();
                MessageBox.Show("应该只有一次抢救不成功，请核对！");
                return -1;
            }
            //if (info.Out_Type == "")
            //{
            //    this.txtLeaveHopitalType.Focus();
            //    MessageBox.Show("请填写“离院方式”！");
            //    return -1;
            //}
            //if (info.Out_Type == "2")
            //{
            //    if (info.HighReceiveHopital == "" || info.HighReceiveHopital == "无" || info.HighReceiveHopital == "不详")
            //    {
            //        this.txtHighReceiveHopital.Focus();
            //        MessageBox.Show("请填写“医嘱转院，拟接收医疗机构名称”！");
            //        return -1;
            //    }
            //}
            //if (info.Out_Type == "3")
            //{
            //    if (info.LowerReceiveHopital == "" || info.LowerReceiveHopital=="无" || info.LowerReceiveHopital=="不详")
            //    {
            //        this.txtLeaveHopitalType.Focus();
            //        MessageBox.Show("请填写“医嘱社区卫生服务机构/乡镇卫生院，拟接收医疗机构名称”！");
            //        return -1;
            //    }
            //}
            //if (info.ComeBackInMonth == "2")
            //{
            //    if (info.ComeBackPurpose == "")
            //    {
            //        this.cmbComeBackPurpose.Focus();
            //        MessageBox.Show("有出院31天内再住院的计划，请填写“再住院的目的”！");
            //        return -1;
            //    }
            //}
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
            this.ucDiagNoseInput1.SetReadOnly(type, this.frmType);
            this.ucOperation1.SetReadOnly(type, this.frmType);
            this.ucBabyCardInput1.SetReadOnly(type);
            this.ucTumourCard1.SetReadOnly(type);
            //费用类别
            cmbPactKind.ReadOnly = type;
            cmbPactKind.EnterVisiable = !type;
            cmbPactKind.BackColor = System.Drawing.Color.White;
            //健康卡号
            txtHealthyCard.ReadOnly = type;
            txtHealthyCard.BackColor = System.Drawing.Color.White;
            //住院次数
            txtInTimes.ReadOnly = type;
            txtInTimes.BackColor = System.Drawing.Color.White;
            //病案号 
            txtCaseNum.ReadOnly = type;
            txtCaseNum.BackColor = System.Drawing.Color.White;
            //姓名
            txtPatientName.ReadOnly = type;
            txtPatientName.Enabled = !type;
            txtPatientName.BackColor = System.Drawing.Color.White;
            //性别
            cmbPatientSex.ReadOnly = type;
            cmbPatientSex.EnterVisiable = !type;
            cmbPatientSex.BackColor = System.Drawing.Color.White;
            //生日
            dtPatientBirthday.Enabled = !type;
            //年龄
            txtPatientAge.ReadOnly = true;
            txtPatientAge.BackColor = System.Drawing.Color.White;
            //国籍
            cmbCountry.ReadOnly = type;
            cmbCountry.EnterVisiable = !type;
            cmbCountry.BackColor = System.Drawing.Color.White;
            //年龄不足一岁年龄
            txtBabyAge.ReadOnly = type;
            txtBabyAge.BackColor = System.Drawing.Color.White;
            //新生儿出生体重
            txtBabyBirthWeight.ReadOnly = type;
            txtBabyBirthWeight.BackColor = System.Drawing.Color.White;
            //新生儿入院体重
            txtBabyInWeight.ReadOnly = type;
            txtBabyInWeight.BackColor = System.Drawing.Color.White;
            //出生地
            cmbBirthAddr.ReadOnly = type;
            cmbBirthAddr.BackColor = System.Drawing.Color.White;
            //籍贯
            cmbDist.ReadOnly = type;
            cmbDist.BackColor = System.Drawing.Color.White;
            //民族
            cmbNationality.ReadOnly = type;
            cmbNationality.EnterVisiable = !type;
            cmbNationality.BackColor = System.Drawing.Color.White;
            //身份证
            txtIDNo.ReadOnly = type;
            txtIDNo.BackColor = System.Drawing.Color.White;
            //职业
            cmbProfession.ReadOnly = type;
            cmbProfession.EnterVisiable = !type;
            cmbProfession.BackColor = System.Drawing.Color.White;
            //婚姻
            cmbMaritalStatus.ReadOnly = type;
            cmbMaritalStatus.EnterVisiable = !type;
            cmbMaritalStatus.BackColor = System.Drawing.Color.White;
            //现住址
            cmbCurrentAdrr.ReadOnly = type;
            cmbCurrentAdrr.BackColor = System.Drawing.Color.White;
            //电话
            txtCurrentPhone.ReadOnly = type;
            txtCurrentPhone.BackColor = System.Drawing.Color.White;
            //邮编
            txtCurrentZip.ReadOnly = type;
            txtCurrentZip.BackColor = System.Drawing.Color.White;
            //户口地址
            cmbHomeAdrr.ReadOnly = type;
            cmbHomeAdrr.BackColor = System.Drawing.Color.White;
            //户口邮编
            txtHomeZip.ReadOnly = type;
            txtHomeZip.BackColor = System.Drawing.Color.White;

            //工作单位
            txtAddressBusiness.ReadOnly = type;
            txtAddressBusiness.BackColor = System.Drawing.Color.White;
            //单位电话
            txtPhoneBusiness.ReadOnly = type;
            txtPhoneBusiness.BackColor = System.Drawing.Color.White;
            //单位邮编
            txtBusinessZip.ReadOnly = type;
            txtBusinessZip.BackColor = System.Drawing.Color.White;

            //联系人姓名 
            txtKin.ReadOnly = type;
            txtKin.BackColor = System.Drawing.Color.White;
            //关系
            cmbRelation.ReadOnly = type;
            cmbRelation.EnterVisiable = !type;
            cmbRelation.BackColor = System.Drawing.Color.White;
            //关系备注
            txtRelationMemo.ReadOnly = type;
            txtRelationMemo.BackColor = System.Drawing.Color.White;
            //l联系人地址
            txtLinkmanAdd.ReadOnly = type;
            txtLinkmanAdd.BackColor = System.Drawing.Color.White;
            //联系电话
            txtLinkmanTel.ReadOnly = type;
            txtLinkmanTel.BackColor = System.Drawing.Color.White;
            //入院途径
            cmbInPath.ReadOnly = type;
            cmbInPath.EnterVisiable = !type;
            cmbInPath.BackColor = System.Drawing.Color.White;

            //入院时间 
            dtDateIn.Enabled = !type;
            //入院科室
            cmbDeptInHospital.ReadOnly = type;
            cmbDeptInHospital.EnterVisiable = !type;
            cmbDeptInHospital.BackColor = System.Drawing.Color.White;
            //入院病房
            txtInRoom.ReadOnly = type;
            txtInRoom.BackColor = System.Drawing.Color.White;
            //转科科别
            dtFirstTime.Enabled = !type;
            txtFirstDept.ReadOnly = type;
            txtFirstDept.BackColor = System.Drawing.Color.White;
            dtSecondTime.Enabled = !type;
            txtDeptSecond.ReadOnly = type;
            txtDeptSecond.BackColor = System.Drawing.Color.White;
            dtThirdTime.Enabled = !type;
            txtDeptThird.ReadOnly = type;
            txtDeptThird.BackColor = System.Drawing.Color.White;
            //出院时间
            txtDateOut.Enabled = !type;
            //出院科室
            cmbDeptOutHospital.ReadOnly = type;
            cmbDeptOutHospital.EnterVisiable = !type;
            cmbDeptOutHospital.BackColor = System.Drawing.Color.White;
            //出院病房
            txtOutRoom.ReadOnly = type;
            txtOutRoom.BackColor = System.Drawing.Color.White;
            //实际住院天数
            txtPiDays.ReadOnly = type;
            txtPiDays.BackColor = System.Drawing.Color.White;
            //门诊诊断
            if (this.frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS)
            {
                txtClinicDiagCode.ReadOnly = false;
                txtClinicDiagCode.Enabled = true;
            }
            else
            {
                txtClinicDiagCode.ReadOnly = type;
                txtClinicDiagCode.BackColor = System.Drawing.Color.White;
            }
            txtClinicDocd.ReadOnly = type;
            txtClinicDocd.BackColor = System.Drawing.Color.White;
            cmbClinicDiagName.ReadOnly = type;
            cmbClinicDiagName.BackColor = System.Drawing.Color.White;
            //病例分型
            cmbExampleType.ReadOnly = type;
            cmbExampleType.BackColor = System.Drawing.Color.White;
            //临床路径病例
            cmbClinicPath.ReadOnly = type;
            cmbClinicPath.BackColor = System.Drawing.Color.White;
            //抢救次数
            txtSalvTimes.ReadOnly = type;
            txtSalvTimes.BackColor = System.Drawing.Color.White;
            //成功抢救次数
            txtSuccTimes.ReadOnly = type;
            txtSuccTimes.BackColor = System.Drawing.Color.White;
            //损伤中毒的外部原因
            txtInjuryOrPoisoningCause.ReadOnly = type;
            txtInjuryOrPoisoningCause.BackColor = System.Drawing.Color.White;
            //疾病编码
            if (this.frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS)
            {
                txtInjuryOrPoisoningCauseCode.ReadOnly = false;
                txtInjuryOrPoisoningCauseCode.Enabled = true;
            }
            else
            {
                txtInjuryOrPoisoningCauseCode.ReadOnly = type;
                txtInjuryOrPoisoningCauseCode.BackColor = System.Drawing.Color.White;
            }
            //病理诊断
            cmbPathologicalDiagName.ReadOnly = type;
            cmbPathologicalDiagName.BackColor = System.Drawing.Color.White;
            //疾病编码
            if (this.frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS)
            {
                txtPathologicalDiagCode.ReadOnly = false;
                txtPathologicalDiagCode.Enabled = true;
            }
            else
            {
                txtPathologicalDiagCode.ReadOnly = type;
                txtPathologicalDiagCode.BackColor = System.Drawing.Color.White;
            }
            //病理号
            txtPathologicalDiagNum.ReadOnly = type;
            txtPathologicalDiagNum.BackColor = System.Drawing.Color.White;
            //是否药物过敏
            cmbIsDrugAllergy.ReadOnly = type;
            cmbIsDrugAllergy.BackColor = System.Drawing.Color.White;
            //药物过敏
            txtDrugAllergy.ReadOnly = type;
            txtDrugAllergy.BackColor = System.Drawing.Color.White;
            //死亡患者尸检
            cmbDeathPatientBobyCheck.ReadOnly = type;
            cmbDeathPatientBobyCheck.BackColor = System.Drawing.Color.White;

            //血型
            cmbBloodType.ReadOnly = type;
            cmbBloodType.EnterVisiable = !type;
            cmbBloodType.BackColor = System.Drawing.Color.White;
            cmbRhBlood.ReadOnly = type;
            cmbRhBlood.EnterVisiable = !type;
            cmbRhBlood.BackColor = System.Drawing.Color.White;
            //科主任
            cmbDeptChiefDoc.ReadOnly = type;
            cmbDeptChiefDoc.EnterVisiable = !type;
            cmbDeptChiefDoc.BackColor = System.Drawing.Color.White;
            //主任医师
            cmbConsultingDoctor.ReadOnly = type;
            cmbConsultingDoctor.EnterVisiable = !type;
            cmbConsultingDoctor.BackColor = System.Drawing.Color.White;
            //主治医师
            cmbAttendingDoctor.ReadOnly = type;
            cmbAttendingDoctor.EnterVisiable = !type;
            cmbAttendingDoctor.BackColor = System.Drawing.Color.White;
            //住院医师
            cmbAdmittingDoctor.ReadOnly = type;
            cmbAdmittingDoctor.EnterVisiable = !type;
            cmbAdmittingDoctor.BackColor = System.Drawing.Color.White;
            //责任护士
            cmbDutyNurse.ReadOnly = type;
            cmbDutyNurse.EnterVisiable = !type;
            cmbDutyNurse.BackColor = System.Drawing.Color.White;
            //进修医师
            cmbRefresherDocd.ReadOnly = type;
            cmbRefresherDocd.EnterVisiable = !type;
            cmbRefresherDocd.BackColor = System.Drawing.Color.White;
            //实习医生
            cmbPraDocCode.ReadOnly = type;
            cmbPraDocCode.EnterVisiable = !type;
            cmbPraDocCode.BackColor = System.Drawing.Color.White;
            //编码员
            txtCodingCode.ReadOnly = type;
            txtCodingCode.EnterVisiable = !type;
            txtCodingCode.BackColor = System.Drawing.Color.White;

            //病案质量
            cmbMrQual.ReadOnly = type;
            cmbMrQual.EnterVisiable = !type;
            cmbMrQual.BackColor = System.Drawing.Color.White;
            //质控医师
            cmbQcDocd.ReadOnly = type;
            cmbQcDocd.EnterVisiable = !type;
            cmbQcDocd.BackColor = System.Drawing.Color.White;
            //质控护士
            cmbQcNucd.ReadOnly = type;
            cmbQcNucd.EnterVisiable = !type;
            cmbQcNucd.BackColor = System.Drawing.Color.White;
            //质控时间
            txtCheckDate.Enabled = !type;
            //离院方式
            txtLeaveHopitalType.ReadOnly = type;
            txtLeaveHopitalType.BackColor = System.Drawing.Color.White;

            //接收医疗结构
            txtHighReceiveHopital.ReadOnly = type;
            txtHighReceiveHopital.BackColor = System.Drawing.Color.White;
            //接收医疗结构
            txtLowerReceiveHopital.ReadOnly = type;
            txtLowerReceiveHopital.BackColor = System.Drawing.Color.White;

            //出院31天内再住院
            cmbComeBackInMonth.ReadOnly = type;
            cmbComeBackInMonth.EnterVisiable = !type;
            cmbComeBackInMonth.BackColor = System.Drawing.Color.White;
            // 目的         
            cmbComeBackPurpose.ReadOnly = type;

            //颅脑损伤患者昏迷 入院前
            txtOutComeDay.ReadOnly = type;
            txtOutComeDay.BackColor = System.Drawing.Color.White;
            //颅脑损伤患者昏迷 入院前
            txtOutComeHour.ReadOnly = type;
            txtOutComeHour.BackColor = System.Drawing.Color.White;
            //颅脑损伤患者昏迷 入院前
            txtOutComeMin.ReadOnly = type;
            txtOutComeMin.BackColor = System.Drawing.Color.White;
            //颅脑损伤患者昏迷 入院后
            txtInComeDay.ReadOnly = type;
            txtInComeDay.BackColor = System.Drawing.Color.White;
            //颅脑损伤患者昏迷 入院后
            txtInComeHour.ReadOnly = type;
            txtInComeHour.BackColor = System.Drawing.Color.White;
            //颅脑损伤患者昏迷 入院后
            txtInComeMin.ReadOnly = type;
            txtInComeMin.BackColor = System.Drawing.Color.White;
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
                this.ucFeeInfo1.ClearInfo();
                //医疗付费方式
                cmbPactKind.Tag = null;
                //健康卡号
                txtHealthyCard.Text = "";
                //实际住院天数
                txtInTimes.Text = "";
                //病案号 
                txtCaseNum.Text = "";
                //姓名
                txtPatientName.Text = "";
                //性别
                cmbPatientSex.Tag = null;
                //生日

                //年龄
                txtPatientAge.Text = "";
                //国籍
                cmbCountry.Tag = null;
                //(年龄不足一周岁的)年龄
                txtBabyAge.Text = "";
                //新生儿出生体重
                txtBabyBirthWeight.Text = "";
                //新生儿入院体重
                txtBabyInWeight.Text = "";
                //出生地
                cmbBirthAddr.Tag = null;
                //籍贯
                cmbDist.Tag = null;
                //民族
                cmbNationality.Tag = null;
                //身份证
                txtIDNo.Text = "";
                //职业
                cmbProfession.Tag = null;
                //婚姻
                cmbMaritalStatus.Tag = null;
                //现住址
                cmbCurrentAdrr.Tag = null;
                //电话
                txtCurrentPhone.Text = "";
                //邮编
                txtCurrentZip.Text = "";
                //户口地址
                cmbHomeAdrr.Tag = null;
                //户口邮编
                txtHomeZip.Text = "";
                //工作单位及地址
                txtAddressBusiness.Text = "";
                //单位邮编
                txtBusinessZip.Text = "";
                //单位电话
                txtPhoneBusiness.Text = "";
                //联系人 
                txtKin.Text = "";
                //关系
                cmbRelation.Tag = null;
                //关系说明
                txtRelationMemo.Text = "";
                //l联系人地址
                txtLinkmanAdd.Text = "";
                //联系电话
                txtLinkmanTel.Text = "";


                //入院途径
                cmbInPath.Tag = null;
                //入院日期
                dtDateIn.Value = System.DateTime.Now;
                //入院科室
                cmbDeptInHospital.Tag = null;
                //病房 
                txtInRoom.Text = "";
                //转科科别
                txtChangeDept.Text = "";
                txtFirstDept.Text = "";
                dtFirstTime.Value = System.DateTime.Now;
                txtDeptSecond.Text = "";
                dtSecondTime.Value = System.DateTime.Now;
                txtDeptThird.Text = "";
                dtThirdTime.Value = System.DateTime.Now;

                //出院时间
                txtDateOut.Value = System.DateTime.Now;
                //出院科室
                cmbDeptOutHospital.Tag = null;
                //病房
                txtOutRoom.Text = "";
                //实际住院天数
                txtPiDays.Text = "";
                //门诊诊断
                txtClinicDiagCode.Text = "";
                //门诊诊断编码
                cmbClinicDiagName.Tag = null;

                //损伤中毒原因
                this.txtInjuryOrPoisoningCause.Text = "";
                //疾病编码
                this.txtInjuryOrPoisoningCauseCode.Text = "";
                //病理诊断
                cmbPathologicalDiagName.Tag = null;
                //疾病编码
                txtPathologicalDiagCode.Text = "";
                //病理号
                this.txtPathologicalDiagNum.Text = "";
                //是否药物过敏
                cmbIsDrugAllergy.Tag = null;
                //过敏药物名称
                txtDrugAllergy.Text = "";
                //死亡患者尸体检查
                cmbDeathPatientBobyCheck.Tag = null;
                //血型
                cmbBloodType.Tag = null;
                //Rh血型
                cmbRhBlood.Tag = null;
                //科主任
                cmbDeptChiefDoc.Tag = null;
                //主任（副主任）医师
                cmbConsultingDoctor.Tag = null;
                //主治医师
                cmbAttendingDoctor.Tag = null;
                //住院医师
                cmbAdmittingDoctor.Tag = null;
                //责任护士
                cmbDutyNurse.Tag = null;
                //进修医师
                cmbRefresherDocd.Tag = null;
                //实习医生
                cmbPraDocCode.Tag = null;
                //编码员
                txtCodingCode.Tag = null;
                //病案质量
                cmbMrQual.Tag = null;
                //质控医师
                cmbQcDocd.Tag = null;
                //质控护士
                cmbQcNucd.Tag = null;
                //质控时间
                txtCheckDate.Value = System.DateTime.Now;
                //离院方式
                txtLeaveHopitalType.Text = "";
                //接收医疗机构（平级或高级）
                txtHighReceiveHopital.Text = "";
                //接收医疗机构(社区或乡镇卫生院)
                txtLowerReceiveHopital.Text = "";
                //出院31天内再住院计划
                cmbComeBackInMonth.Tag = null;
                //再住院目的
                this.cmbComeBackPurpose.Tag = null;
                //颅脑损伤患者昏迷时间（入院前天）
                txtOutComeDay.Text = "";
                //颅脑损伤患者昏迷时间（入院前时）
                txtOutComeHour.Text = "";
                //颅脑损伤患者昏迷时间（入院前分）
                txtOutComeMin.Text = "";
                //颅脑损伤患者昏迷时间（入院后天）
                txtInComeDay.Text = "";
                //颅脑损伤患者昏迷时间（入院后天）
                txtInComeHour.Text = "";
                //颅脑损伤患者昏迷时间（入院后天）
                txtInComeMin.Text = "";



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
            frmPrintMetCasBase frmPrint = new frmPrintMetCasBase(this);
            frmPrint.PrintPageNum = this.PrintPageNum;
            frmPrint.ShowDialog();
            return 0;
        }
        protected override int OnPrint(object sender, object neuObject)
        {
            this.PrintInterface();
            return base.OnPrint(sender, neuObject);
        }
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public int PrintCase(string Type)
        {
            string tips = string.Empty;
            if (this.ContrastInfo(ref tips) == -1)
            {
                MessageBox.Show(tips + "发生变化，请保存再打印！", "提示");
                return -1;
            }

            switch (Type)
            {
                case "Print":
                    this.Print();
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
                case "PrintAdditional":
                    this.PrintAdditional(null);
                    break;
                case "PrintAdditionalPreview":
                    this.PrintAdditionalPreview(null, null);
                    break;
                default:
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
        public int Print()
        {
            if (this.PrintFristCheck() == -1)
            {
                return -1;
            }
            if (this.healthRecordPrint == null)
            {
                //this.healthRecordPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface)) as FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface;
                this.healthRecordPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.Components.HealthRecord.CaseFirstPage.ucMetCasBaseInfo), typeof(FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface)) as FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface;
                if (this.healthRecordPrint == null)
                {
                    MessageBox.Show("获得接口IExamPrint错误\n，可能没有维护相关的打印控件或打印控件没有实现接口IExamPrint\n请与系统管理员联系。");
                    return -1;
                }
            }
            this.healthRecordPrint.Reset();
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
            if (this.PrintSecCheck() == -1)
            {
                return -1;
            }
            if (this.healthRecordPrintBack == null)
            {
                //this.healthRecordPrintBack = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceBack)) as FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceBack;
                this.healthRecordPrintBack = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(ucMetCasBaseInfo), typeof(FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceBack)) as FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceBack;
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
            this.healthRecordPrintBack.Reset();
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
            if (this.PrintFristCheck() == -1)
            {
                return -1;
            }
            if (this.healthRecordPrint == null)
            {
                //this.healthRecordPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface)) as FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface;
                this.healthRecordPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(ucMetCasBaseInfo), typeof(FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface)) as FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface;
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
            this.healthRecordPrint.Reset();

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
            //反面赋值
            if (this.PrintSecCheck() == -1)
            {
                return -1;
            }
            if (this.healthRecordPrintBack == null)
            {
                //this.healthRecordPrintBack = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceBack)) as FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceBack;
                this.healthRecordPrintBack = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(ucMetCasBaseInfo), typeof(FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceBack)) as FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceBack;
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
            this.healthRecordPrintBack.Reset();
            this.healthRecordPrintBack.ControlValue(this.CaseBase);
            this.healthRecordPrintBack.PrintPreview();
            return 0;
        }

        /// <summary>
        /// 打印附加页
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int PrintAdditional(FS.HISFC.Models.HealthRecord.Base obj)
        {
            //反面赋值

            if (this.healthRecordInterfaceAdditional == null)
            {
                //this.healthRecordInterfaceAdditional = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceAdditional)) as FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceAdditional;
                this.healthRecordInterfaceAdditional = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(ucMetCasBaseInfo), typeof(FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceAdditional)) as FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceAdditional;
                if (this.healthRecordInterfaceAdditional == null)
                {
                    MessageBox.Show("获得接口IExamPrint(healthRecordInterfaceAdditional)错误\n，可能没有维护相关的打印控件或打印控件没有实现接口IExamPrint\n请与系统管理员联系。");
                    return -1;
                }
            }
            if (CaseBase == null)
            {
                return -1;
            }
            this.healthRecordInterfaceAdditional.Reset();
            this.healthRecordInterfaceAdditional.ControlValue(this.CaseBase);
            this.healthRecordInterfaceAdditional.Print();
            return 1;
        }
        /// <summary>
        /// 打印附加页预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public int PrintAdditionalPreview(object sender, object neuObject)
        {
            if (this.healthRecordInterfaceAdditional == null)
            {
                //this.healthRecordInterfaceAdditional = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceAdditional)) as FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceAdditional;
                this.healthRecordInterfaceAdditional = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(ucMetCasBaseInfo), typeof(FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceAdditional)) as FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceAdditional;
                if (this.healthRecordInterfaceAdditional == null)
                {
                    MessageBox.Show("获得接口IExamPrint(healthRecordInterfaceAdditional)错误\n，可能没有维护相关的打印控件或打印控件没有实现接口IExamPrint\n请与系统管理员联系。");
                    return -1;
                }
            }

            if (this.CaseBase == null)
            {
                return -1;
            }
            this.healthRecordInterfaceAdditional.Reset();
            this.healthRecordInterfaceAdditional.ControlValue(this.CaseBase);
            this.healthRecordInterfaceAdditional.PrintPreview();
            return 0;
        }

        /// <summary>
        /// 对比界面与数据库是否一致
        /// </summary>
        /// <returns></returns>
        private int ContrastInfo(ref string Tips)
        {
            FS.HISFC.Models.HealthRecord.Base dataBaseInfo = baseDml.GetCaseBaseInfo(this.CaseBase.PatientInfo.ID);
            if (dataBaseInfo == null)
            {
                Tips = "请保存首页，再打印！";
            }
            this.CaseBase = dataBaseInfo;//打印内容和数据库一致
            FS.HISFC.Models.HealthRecord.Base info = new FS.HISFC.Models.HealthRecord.Base();
            int i = this.GetInfoFromPanel(info);
            int ret = 0;
            if (dataBaseInfo.PatientInfo.ID != info.PatientInfo.ID)//住院流水号
            {
                Tips = "住院流水号、";
            }
            if (dataBaseInfo.PatientInfo.Pact.ID != info.PatientInfo.Pact.ID)//医疗付费方式
            {
                Tips += "付款方式、";
            }
            if (dataBaseInfo.PatientInfo.SSN != info.PatientInfo.SSN)//健康卡号
            {
                Tips += "医保号、";
            }
            if (dataBaseInfo.PatientInfo.InTimes != info.PatientInfo.InTimes)//住院次数
            {
                Tips += "住院次数、";
            }
            if (dataBaseInfo.CaseNO != info.CaseNO)
            {
                Tips += "病案号";
            }
            if (dataBaseInfo.PatientInfo.Name != info.PatientInfo.Name)//姓名
            {
                Tips += "姓名、";
            }

            if (dataBaseInfo.PatientInfo.Sex.ID.ToString() != info.PatientInfo.Sex.ID.ToString())//性别
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
            //新生儿出生体重
            try
            {
                int weight = FS.FrameWork.Function.NConvert.ToInt32(dataBaseInfo.BabyBirthWeight);
                if (weight > 0)
                {
                    if (dataBaseInfo.BabyBirthWeight != null && info.BabyBirthWeight != null && dataBaseInfo.BabyBirthWeight.Trim() != info.BabyBirthWeight.Trim())
                    {
                        Tips += "新生儿出生体重、";
                    }
                }
            }
            catch
            {
            }
            //新生儿入院体重
            try
            {
                int inWeight = FS.FrameWork.Function.NConvert.ToInt32(dataBaseInfo.BabyBirthWeight);
                if (inWeight > 0)
                {
                    if (dataBaseInfo.BabyInWeight != null && info.BabyInWeight != null && dataBaseInfo.BabyInWeight.Trim() != info.BabyInWeight.Trim())
                    {
                        Tips += "新生儿入院体重、";
                    }
                }
            }
            catch
            {
            }
            if (dataBaseInfo.PatientInfo.AreaCode != info.PatientInfo.AreaCode)//出生地
            {
                Tips += "出生地、";
            }
            if (dataBaseInfo.PatientInfo.DIST != info.PatientInfo.DIST)//籍贯
            {
                Tips += "籍贯、";
            }
            if (dataBaseInfo.PatientInfo.IDCard.ToString().Trim() != info.PatientInfo.IDCard.ToString().Trim())//身份证号
            {
                Tips += "身份证号、";
            }

            if (dataBaseInfo.PatientInfo.Profession.ID != info.PatientInfo.Profession.ID)//职业
            {
                Tips += "职业、";
            }
            if (dataBaseInfo.PatientInfo.MaritalStatus.ID.ToString().Trim() != info.PatientInfo.MaritalStatus.ID.ToString().Trim())//婚否
            {
                Tips += "婚姻、";
            }
            // 现住址
            if (dataBaseInfo.CurrentAddr != info.CurrentAddr)
            {
                Tips += "现住址、";
            }
            //现住址电话
            if (dataBaseInfo.CurrentPhone != info.CurrentPhone)
            {
                Tips += "现住址电话、";
            }
            //现住址邮编
            if (info.CurrentZip != info.CurrentZip)
            {
                Tips += "现住址邮编、";
            }
            if (dataBaseInfo.PatientInfo.AddressHome != info.PatientInfo.AddressHome)//家庭住址
            {
                Tips += "家庭住址、";
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
            if (dataBaseInfo.InPath != info.InPath)
            {
                Tips += "入院途径、";
            }

            if (dataBaseInfo.PatientInfo.PVisit.InTime.Date != info.PatientInfo.PVisit.InTime.Date)//入院日期
            {
                Tips += "入院日期、";
            }
            if (dataBaseInfo.InDept.ID != info.InDept.ID)//入院科室代码
            {
                Tips += "入院科室、";
            }
            if (dataBaseInfo.PatientInfo.PVisit.OutTime.Date != info.PatientInfo.PVisit.OutTime.Date)//出院日期
            {
                Tips += "出院日期、";
            }
            if (dataBaseInfo.OutDept.ID != info.OutDept.ID)//出院科室代码
            {
                Tips += "出院科室、";
            }
            if (dataBaseInfo.InHospitalDays != info.InHospitalDays)//住院天数
            {
                Tips += "住院天数、";
            }
            //门诊诊断
            if (dataBaseInfo.ClinicDiag.Name != info.ClinicDiag.Name)
            {
                Tips += "门诊诊断、";
            }
            //门诊诊断编码
            if (dataBaseInfo.ClinicDiag.ID != info.ClinicDiag.ID)
            {
                Tips += "门诊诊断编码、";
            }
            if (dataBaseInfo.ClinicDoc.ID != info.ClinicDoc.ID)//门诊诊断医生
            {
                Tips += "门诊诊断医生、";
            }
            //病例分型
            if (dataBaseInfo.ExampleType != info.ExampleType)
            {
                Tips += "病例分型、";
            }
            //临床路径病例
            if (dataBaseInfo.ClinicPath != info.ClinicPath)
            {
                Tips += "临床路径病例、";
            }
            if (dataBaseInfo.SalvTimes != info.SalvTimes)//抢救次数
            {
                Tips += "抢救次数、";
            }
            if (dataBaseInfo.SuccTimes != info.SuccTimes)//成功次数
            {
                Tips += "成功次数、";
            }
            //损伤中毒原因
            if (dataBaseInfo.InjuryOrPoisoningCauseCode != info.InjuryOrPoisoningCauseCode)
            {
                Tips += "损伤中毒原因编码、";
            }
            if (dataBaseInfo.InjuryOrPoisoningCause != info.InjuryOrPoisoningCause)
            {
                Tips += "损伤中毒原因、";
            }
            //病理诊断

            if (dataBaseInfo.PathologicalDiagName != info.PathologicalDiagName)
            {
                Tips += "病理诊断、";
            }

            if (dataBaseInfo.PathologicalDiagCode != info.PathologicalDiagCode)
            {
                Tips += "病理诊断编码、";
            }

            if (dataBaseInfo.PathNum != info.PathNum)
            {
                Tips += "病理号、";
            }
            //药物过敏
            if (dataBaseInfo.AnaphyFlag != info.AnaphyFlag)
            {
                Tips += "药物过敏、";
            }
            if (dataBaseInfo.FirstAnaphyPharmacy.ID != info.FirstAnaphyPharmacy.ID)//过敏药物名称
            {
                Tips += "过敏药物名称、";
            }
            if (dataBaseInfo.CadaverCheck != info.CadaverCheck)//尸检
            {
                Tips += "尸检、";
            }
            if (dataBaseInfo.PatientInfo.BloodType.ID.ToString().Trim() != info.PatientInfo.BloodType.ID.ToString().Trim())//血型编码
            {
                Tips += "血型、";
            }
            if (dataBaseInfo.RhBlood != info.RhBlood)//Rh血型(阴阳)
            {
                Tips += "Rh血型、";
            }
            if (dataBaseInfo.PatientInfo.PVisit.ReferringDoctor.ID != info.PatientInfo.PVisit.ReferringDoctor.ID)//科主任代码
            {
                Tips += "科主任、";
            }
            if (dataBaseInfo.PatientInfo.PVisit.ConsultingDoctor.ID != info.PatientInfo.PVisit.ConsultingDoctor.ID)//主任医师代码
            {
                Tips += "主任医师、";
            }
            if (dataBaseInfo.PatientInfo.PVisit.AttendingDoctor.ID != info.PatientInfo.PVisit.AttendingDoctor.ID)//主治医师代码
            {
                Tips += "主治医师、";
            }
            if (dataBaseInfo.PatientInfo.PVisit.AdmittingDoctor.ID != info.PatientInfo.PVisit.AdmittingDoctor.ID)//住院医师代码
            {
                Tips += "住院医师、";
            }
            if (dataBaseInfo.DutyNurse.ID != info.DutyNurse.ID)//责任护士
            {
                Tips += "责任护士、";
            }
            if (dataBaseInfo.RefresherDoc.ID != info.RefresherDoc.ID)//进修医师代码
            {
                Tips += "进修医师、";
            }
            if (dataBaseInfo.PatientInfo.PVisit.TempDoctor.ID != info.PatientInfo.PVisit.TempDoctor.ID)//实习医师代码
            {
                Tips += "实习医师项、";
            }
            if (dataBaseInfo.CodingOper.ID != info.CodingOper.ID)
            {
                Tips += "编码员、";
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
            //离院方式
            if (dataBaseInfo.Out_Type != info.Out_Type)
            {
                Tips += "离院方式、";
            }
            //医嘱转院接收医疗机构
            if (dataBaseInfo.HighReceiveHopital != info.HighReceiveHopital)
            {
                Tips += "医嘱转院接收医疗机构、";
            }
            //医嘱转社区
            if (dataBaseInfo.LowerReceiveHopital != info.LowerReceiveHopital)
            {
                Tips += "医嘱转社区、";
            }
            //出院31天内再住院计划
            if (dataBaseInfo.ComeBackInMonth != info.ComeBackInMonth)
            {
                Tips += "出院31天内再住院计划、";
            }
            //出院31天再住院目的

            if (dataBaseInfo.ComeBackPurpose != info.ComeBackPurpose)
            {
                Tips += "出院31天再住院目的、";
            }
            //颅脑损伤患者昏迷时间 -入院前 天
            if (dataBaseInfo.OutComeDay != info.OutComeDay)
            {
                Tips += "颅脑损伤患者昏迷时间入院前(天)、";
            }
            //颅脑损伤患者昏迷时间 -入院前 小时
            if (dataBaseInfo.OutComeHour != info.OutComeHour)
            {
                Tips += "颅脑损伤患者昏迷时间入院前(小时)、";
            }
            //颅脑损伤患者昏迷时间 -入院前 分钟
            if (dataBaseInfo.OutComeMin != info.OutComeMin)
            {
                Tips += "颅脑损伤患者昏迷时间入院前(分钟)、";
            }
            //颅脑损伤患者昏迷时间 -入院后 天
            if (dataBaseInfo.InComeDay != info.InComeDay)
            {
                Tips += "颅脑损伤患者昏迷时间入院后(天)、";
            }
            //颅脑损伤患者昏迷时间 -入院后 小时
            if (dataBaseInfo.InComeHour != info.InComeHour)
            {
                Tips += "颅脑损伤患者昏迷时间入院后(小时)、";
            }
            //颅脑损伤患者昏迷时间 -入院后 分钟
            if (dataBaseInfo.InComeMin != info.InComeMin)
            {
                Tips += "颅脑损伤患者昏迷时间入院后(分钟)、";
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

        /// <summary>
        /// 据说病案首页缺项比较多，所以打印时候需要做提示
        /// </summary>
        private int PrintFristCheck()
        {
            if (cmbPactKind.Tag == null || (cmbPactKind.Tag != null && cmbPactKind.Tag.ToString() == ""))
            {
                MessageBox.Show("请选择医疗付款方式！", "提示！");
                this.cmbPactKind.Focus();
                return -1;
            }
            //医保公费号
            if (txtHealthyCard.Text.Trim() == "")
            {
                MessageBox.Show("请填写健康卡号/医保号！", "提示！");
                this.txtHealthyCard.Focus();
                return -1;
            }
            //住院次数
            if (txtInTimes.Text.Trim() == "")
            {
                MessageBox.Show("请填写住院次数！", "提示！");
                this.txtInTimes.Focus();
                return -1;
            }
            //住院号  病历号
            if (txtCaseNum.Text.Trim() == "")
            {
                MessageBox.Show("请填写住院号！", "提示！");
                this.txtCaseNum.Focus();
                return -1;
            }
            //姓名
            if (txtPatientName.Text.Trim() == "")
            {
                MessageBox.Show("请填写姓名！", "提示！");
                this.txtPatientName.Focus();
                return -1;
            }
            //性别
            if (cmbPatientSex.Tag == null || (cmbPatientSex.Tag != null && cmbPatientSex.Tag.ToString() == ""))
            {
                MessageBox.Show("请选择患者性别！", "提示！");
                this.cmbPatientSex.Focus();
                return -1;
            }
            //生日
            if (txtPatientAge.Text.Trim() == "")
            {
                MessageBox.Show("请选择患者出生日期！", "提示！");
                this.dtPatientBirthday.Focus();
                return -1;
            }
            //国籍 编码
            if (cmbCountry.Tag == null || (cmbCountry.Tag != null && cmbCountry.Tag.ToString() == ""))
            {
                MessageBox.Show("请选择患者国籍！", "提示！");
                this.cmbCountry.Focus();
                return -1;
            }
            //民族 
            if (cmbCountry.Tag.ToString() == "156")
            {
                if (cmbNationality.Tag == null || (cmbNationality.Tag != null && cmbNationality.Tag.ToString() == ""))
                {
                    MessageBox.Show("请选择患者民族！", "提示！");
                    this.cmbNationality.Focus();
                    return -1;
                }
            }
            //新生儿出生体重
            FS.HISFC.BizLogic.HealthRecord.Baby babyMgr = new FS.HISFC.BizLogic.HealthRecord.Baby();
            ArrayList babyAl = babyMgr.QueryBabyByInpatientNo(this.CaseBase.PatientInfo.ID);
            if (babyAl != null && babyAl.Count > 0)
            {
                int weith = 0;
                try
                {
                    weith = FS.FrameWork.Function.NConvert.ToInt32(this.txtBabyBirthWeight.Text);
                }
                catch
                {
                    weith = 0;
                }
                if (weith == 0)
                {
                    if (this.txtBabyBirthWeight.Text.Trim() == "-" || this.txtBabyBirthWeight.Text.Trim() == "")
                    {
                        MessageBox.Show("有新生儿，请填写新生儿出生体重,新生儿入院体重！", "提示！");
                        this.txtBabyBirthWeight.Focus();
                        return -1;
                    }
                }
            }
            //新生儿入院体重
            if (this.txtBabyInWeight.Text == "")
            {
                MessageBox.Show("请填写新生儿入院体重！", "提示！");
                this.txtBabyInWeight.Focus();
                return -1;
            }
            //出生地
            if (cmbCountry.Tag.ToString() == "156")
            {
                ArrayList addrlist = con.GetList("GBXZQYHF");
                bool a = true;

                if ((cmbBirthAddr.Text.Trim().ToString().Length > 3 && cmbBirthAddr.Text.Trim().ToString().Substring(0, 3) == "北京市") || (cmbBirthAddr.Text.Trim().ToString().Length > 3 && cmbBirthAddr.Text.Trim().ToString().Substring(0, 3) == "天津市") || (cmbBirthAddr.Text.Trim().ToString().Length > 3 && cmbBirthAddr.Text.Trim().ToString().Substring(0, 3) == "上海市") || (cmbBirthAddr.Text.Trim().ToString().Length > 3 && cmbBirthAddr.Text.Trim().ToString().Substring(0, 3) == "重庆市") || (cmbBirthAddr.Text.Trim().ToString().Length > 6 && cmbBirthAddr.Text.Trim().ToString().Substring(0, 7) == "香港特别行政区") || (cmbBirthAddr.Text.Trim().ToString().Length > 6 && cmbBirthAddr.Text.Trim().ToString().Substring(0, 7) == "澳门特别行政区") || (cmbBirthAddr.Text.Trim().ToString().Length > 2 && cmbBirthAddr.Text.Trim().ToString().Substring(0, 3) == "台湾省") || (cmbBirthAddr.Text.Trim().ToString().Length > 2 && cmbBirthAddr.Text.Trim().ToString().Substring(0, 2) == "西藏") || (cmbBirthAddr.Text.Trim().ToString().Length > 2 && cmbBirthAddr.Text.Trim().ToString().Substring(0, 2) == "新疆") || (cmbBirthAddr.Text.Trim().ToString().Length > 7 && cmbBirthAddr.Text.Trim().ToString().Substring(0, 7) == "广西壮族自治区") || (cmbBirthAddr.Text.Trim().ToString().Length > 3 && cmbBirthAddr.Text.Trim().ToString().Substring(0, 3) == "内蒙古") || (cmbBirthAddr.Text.Trim().ToString().Length > 2 && cmbBirthAddr.Text.Trim().ToString().Substring(0, 2) == "宁夏"))
                {
                    a = false;
                }
                else if (cmbCountry.Tag.ToString() == "156")
                {
                    if (cmbBirthAddr.Text.Trim().IndexOf("省") > 0 && cmbBirthAddr.Text.Trim().IndexOf("市") > 0 && (cmbBirthAddr.Text.Trim().IndexOf("区") > 0 || cmbBirthAddr.Text.Trim().IndexOf("县") > 0))
                    {
                        a = false;
                    }
                    else if (cmbBirthAddr.Text.Trim().IndexOf("省") > 0 && cmbBirthAddr.Text.Trim().IndexOf("州") > 0 && (cmbBirthAddr.Text.Trim().IndexOf("市") > 0 || cmbBirthAddr.Text.Trim().IndexOf("县") > 0))
                    {
                        a = false;
                    }

                    else if (cmbBirthAddr.Text.Trim().IndexOf("省") > 0 && cmbBirthAddr.Text.Trim().IndexOf("自治县") > 0)
                    {
                        a = false;
                    }
                    else
                    {
                        MessageBox.Show("请按“省-市-区(镇)”填写患者出生地！如果没有区（县）直接在地址后面加“区”字", "提示！");
                        this.cmbBirthAddr.Focus();
                        return -1;
                    }



                }

                if (a)
                {
                    MessageBox.Show("请按“省-市-区(镇)”填写患者出生地！如果没有区（县）直接在地址后面加“区”字", "提示！");
                    this.cmbBirthAddr.Focus();
                    return -1;
                }
            }
            //if (cmbBirthAddr.Text.Trim() == "")
            //{
            //    MessageBox.Show("请填写患者出生地！", "提示！");
            //    this.cmbBirthAddr.Focus();
            //    return -1;
            //}
            //籍贯
            //if (cmbDist.Text.Trim() == "")
            //{
            //    MessageBox.Show("请填写患者籍贯！", "提示！");
            //    this.cmbDist.Focus();
            //    return -1;
            //}
            if (cmbCountry.Tag.ToString() == "156")
            {
                ArrayList addrlist = con.GetList("GBXZQYHF");
                bool a = true;

                if ((cmbDist.Text.Trim().ToString().Length > 3 && cmbDist.Text.Trim().ToString().Substring(0, 3) == "北京市") || (cmbDist.Text.Trim().ToString().Length > 3 && cmbDist.Text.Trim().ToString().Substring(0, 3) == "天津市") || (cmbDist.Text.Trim().ToString().Length > 3 && cmbDist.Text.Trim().ToString().Substring(0, 3) == "上海市") || (cmbDist.Text.Trim().ToString().Length > 3 && cmbDist.Text.Trim().ToString().Substring(0, 3) == "重庆市") || (cmbDist.Text.Trim().ToString().Length > 6 && cmbDist.Text.Trim().ToString().Substring(0, 7) == "香港特别行政区") || (cmbDist.Text.Trim().ToString().Length > 6 && cmbDist.Text.Trim().ToString().Substring(0, 7) == "澳门特别行政区") || (cmbDist.Text.Trim().ToString().Length > 2 && cmbDist.Text.Trim().ToString().Substring(0, 3) == "台湾省") || (cmbDist.Text.Trim().ToString().Length > 2 && cmbDist.Text.Trim().ToString().Substring(0, 2) == "西藏") || (cmbDist.Text.Trim().ToString().Length > 2 && cmbDist.Text.Trim().ToString().Substring(0, 2) == "新疆") || (cmbDist.Text.Trim().ToString().Length > 7 && cmbDist.Text.Trim().ToString().Substring(0, 7) == "广西壮族自治区") || (cmbDist.Text.Trim().ToString().Length > 3 && cmbDist.Text.Trim().ToString().Substring(0, 3) == "内蒙古") || (cmbDist.Text.Trim().ToString().Length > 2 && cmbDist.Text.Trim().ToString().Substring(0, 2) == "宁夏"))
                {
                    a = false;
                }
                else if (cmbCountry.Tag.ToString() == "156")
                {
                    if (cmbDist.Text.Trim().IndexOf("省") > 0 && cmbDist.Text.Trim().IndexOf("市") > 0 && (cmbDist.Text.Trim().IndexOf("区") > 0 || cmbDist.Text.Trim().IndexOf("县") > 0))
                    {
                        a = false;
                    }
                    else if (cmbDist.Text.Trim().IndexOf("省") > 0 && cmbDist.Text.Trim().IndexOf("州") > 0 && (cmbDist.Text.Trim().IndexOf("市") > 0 || cmbDist.Text.Trim().IndexOf("县") > 0))
                    {
                        a = false;
                    }

                    else if (cmbDist.Text.Trim().IndexOf("省") > 0 && cmbDist.Text.Trim().IndexOf("自治县") > 0)
                    {
                        a = false;
                    }
                    else
                    {
                        MessageBox.Show("请按“省-市-区(镇)”填写患者籍贯！如果没有区（县）直接在地址后面加“区”字", "提示！");
                        this.cmbDist.Focus();
                        return -1;
                    }



                }

                if (a)
                {
                    MessageBox.Show("请按“省-市-区(镇)”填写患者籍贯！如果没有区（县）直接在地址后面加“区”字", "提示！");
                    this.cmbDist.Focus();
                    return -1;
                }
            }
            //身份证号
            if (txtIDNo.Text.Trim() == "")
            {
                MessageBox.Show("请填写患者身份证号，如果没有请填未提供！", "提示！");
                this.txtIDNo.Focus();
                return -1;
            }
            //职业
            if (cmbProfession.Tag == null || (cmbProfession.Tag != null && cmbProfession.Tag.ToString() == ""))
            {
                MessageBox.Show("请选择患者职业！", "提示！");
                this.cmbProfession.Focus();
                return -1;
            }
            //婚姻
            if (cmbMaritalStatus.Tag == null || (cmbMaritalStatus.Tag != null && cmbMaritalStatus.Tag.ToString() == ""))
            {
                MessageBox.Show("请选择患者婚姻！", "提示！");
                this.cmbMaritalStatus.Focus();
                return -1;
            }
            //现住址  
            if (cmbCountry.Tag.ToString() == "156")
            {
                ArrayList addrlist = con.GetList("GBXZQYHF");
                bool a = true;

                if ((cmbCurrentAdrr.Text.Trim().ToString().Length > 3 && cmbCurrentAdrr.Text.Trim().ToString().Substring(0, 3) == "北京市") || (cmbCurrentAdrr.Text.Trim().ToString().Length > 3 && cmbCurrentAdrr.Text.Trim().ToString().Substring(0, 3) == "天津市") || (cmbCurrentAdrr.Text.Trim().ToString().Length > 3 && cmbCurrentAdrr.Text.Trim().ToString().Substring(0, 3) == "上海市") || (cmbCurrentAdrr.Text.Trim().ToString().Length > 3 && cmbCurrentAdrr.Text.Trim().ToString().Substring(0, 3) == "重庆市") || (cmbCurrentAdrr.Text.Trim().ToString().Length > 6 && cmbCurrentAdrr.Text.Trim().ToString().Substring(0, 7) == "香港特别行政区") || (cmbCurrentAdrr.Text.Trim().ToString().Length > 6 && cmbCurrentAdrr.Text.Trim().ToString().Substring(0, 7) == "澳门特别行政区") || (cmbCurrentAdrr.Text.Trim().ToString().Length > 2 && cmbCurrentAdrr.Text.Trim().ToString().Substring(0, 3) == "台湾省") || (cmbCurrentAdrr.Text.Trim().ToString().Length > 2 && cmbCurrentAdrr.Text.Trim().ToString().Substring(0, 2) == "西藏") || (cmbCurrentAdrr.Text.Trim().ToString().Length > 2 && cmbCurrentAdrr.Text.Trim().ToString().Substring(0, 2) == "新疆") || (cmbCurrentAdrr.Text.Trim().ToString().Length > 7 && cmbCurrentAdrr.Text.Trim().ToString().Substring(0, 7) == "广西壮族自治区") || (cmbCurrentAdrr.Text.Trim().ToString().Length > 3 && cmbCurrentAdrr.Text.Trim().ToString().Substring(0, 3) == "内蒙古") || (cmbCurrentAdrr.Text.Trim().ToString().Length > 2 && cmbCurrentAdrr.Text.Trim().ToString().Substring(0, 2) == "宁夏"))
                {
                    a = false;
                }
                else if (cmbCountry.Tag.ToString() == "156")
                {
                    if (cmbCurrentAdrr.Text.Trim().IndexOf("省") > 0 && cmbCurrentAdrr.Text.Trim().IndexOf("市") > 0 && (cmbCurrentAdrr.Text.Trim().IndexOf("区") > 0 || cmbCurrentAdrr.Text.Trim().IndexOf("县") > 0))
                    {
                        a = false;
                    }
                    else if (cmbCurrentAdrr.Text.Trim().IndexOf("省") > 0 && cmbCurrentAdrr.Text.Trim().IndexOf("州") > 0 && (cmbCurrentAdrr.Text.Trim().IndexOf("市") > 0 || cmbCurrentAdrr.Text.Trim().IndexOf("县") > 0))
                    {
                        a = false;
                    }
                    //else if (cmbCurrentAdrr.Text.Trim().IndexOf("区") > 0 && cmbCurrentAdrr.Text.Trim().IndexOf("市") > 0)
                    //{
                    //    a = false;
                    //}
                    else if (cmbCurrentAdrr.Text.Trim().IndexOf("省") > 0 && cmbCurrentAdrr.Text.Trim().IndexOf("自治县") > 0)
                    {
                        a = false;
                    }
                    else
                    {
                        MessageBox.Show("请按“省-市-区(镇)”填写患者现住址！如果没有区（县）直接在地址后面加“区”字", "提示！");
                        this.cmbCurrentAdrr.Focus();
                        return -1;
                    }



                }

                if (a)
                {
                    MessageBox.Show("请按“省-市-区(镇)”填写患者现住址！如果没有区（县）直接在地址后面加“区”字", "提示！");
                    this.cmbCurrentAdrr.Focus();
                    return -1;
                }
            }

            //现住址电话 //家庭电话
            if (this.cmbCurrentAdrr.Text.Trim() != "-" && this.txtCurrentPhone.Text.Trim() == "")
            {
                MessageBox.Show("请填写患者现住址电话！", "提示！");
                this.txtCurrentPhone.Focus();
                return -1;
            }
            //现住址邮编
            if (this.cmbCurrentAdrr.Text.Trim() != "-" && txtCurrentZip.Text.Trim() == "")
            {
                MessageBox.Show("请填写患者现住址邮编！", "提示！");
                this.txtCurrentZip.Focus();
                return -1;
            }
            //家庭住址户口地址
            //if (cmbHomeAdrr.Text.Trim() == "")
            //{
            //    MessageBox.Show("请填写患者家庭住址！", "提示！");
            //    this.cmbHomeAdrr.Focus();
            //    return -1;
            //}
            if (cmbCountry.Tag.ToString() == "156")
            {
                bool b = true;

                if ((cmbHomeAdrr.Text.Trim().ToString().Length > 3 && cmbHomeAdrr.Text.Trim().ToString().Substring(0, 3) == "北京市") || (cmbHomeAdrr.Text.Trim().ToString().Length > 3 && cmbHomeAdrr.Text.Trim().ToString().Substring(0, 3) == "天津市") || (cmbHomeAdrr.Text.Trim().ToString().Length > 3 && cmbHomeAdrr.Text.Trim().ToString().Substring(0, 3) == "上海市") || (cmbHomeAdrr.Text.Trim().ToString().Length > 3 && cmbHomeAdrr.Text.Trim().ToString().Substring(0, 3) == "重庆市") || (cmbHomeAdrr.Text.Trim().ToString().Length > 6 && cmbHomeAdrr.Text.Trim().ToString().Substring(0, 7) == "香港特别行政区") || (cmbHomeAdrr.Text.Trim().ToString().Length > 6 && cmbHomeAdrr.Text.Trim().ToString().Substring(0, 7) == "澳门特别行政区") || (cmbHomeAdrr.Text.Trim().ToString().Length > 2 && cmbHomeAdrr.Text.Trim().ToString().Substring(0, 3) == "台湾省") || (cmbHomeAdrr.Text.Trim().ToString().Length > 2 && cmbHomeAdrr.Text.Trim().ToString().Substring(0, 2) == "西藏") || (cmbHomeAdrr.Text.Trim().ToString().Length > 2 && cmbHomeAdrr.Text.Trim().ToString().Substring(0, 2) == "新疆") || (cmbHomeAdrr.Text.Trim().ToString().Length > 7 && cmbHomeAdrr.Text.Trim().ToString().Substring(0, 7) == "广西壮族自治区") || (cmbHomeAdrr.Text.Trim().ToString().Length > 3 && cmbHomeAdrr.Text.Trim().ToString().Substring(0, 3) == "内蒙古") || (cmbHomeAdrr.Text.Trim().ToString().Length > 2 && cmbHomeAdrr.Text.Trim().ToString().Substring(0, 2) == "宁夏"))
                {
                    b = false;
                }
                else if (cmbCountry.Tag.ToString() == "156")
                {
                    if (cmbHomeAdrr.Text.Trim().IndexOf("省") > 0 && cmbHomeAdrr.Text.Trim().IndexOf("市") > 0 && (cmbHomeAdrr.Text.Trim().IndexOf("区") > 0 || cmbHomeAdrr.Text.Trim().IndexOf("县") > 0))
                    {
                        b = false;
                    }
                    else if (cmbHomeAdrr.Text.Trim().IndexOf("省") > 0 && cmbHomeAdrr.Text.Trim().IndexOf("州") > 0 && (cmbHomeAdrr.Text.Trim().IndexOf("市") > 0 || cmbHomeAdrr.Text.Trim().IndexOf("县") > 0))
                    {
                        b = false;
                    }
                    //else if (cmbHomeAdrr.Text.Trim().IndexOf("区") > 0 && cmbHomeAdrr.Text.Trim().IndexOf("市") > 0)
                    //{
                    //    b = false;
                    //}
                    else if (cmbHomeAdrr.Text.Trim().IndexOf("省") > 0 && cmbHomeAdrr.Text.Trim().IndexOf("自治县") > 0)
                    {
                        b = false;
                    }
                    else
                    {
                        MessageBox.Show("请按“省-市-区(镇)”填写患者户口地址！如果没有区（县）直接在地址后面加“区”字", "提示！");
                        this.cmbCurrentAdrr.Focus();
                        return -1;
                    }

                }

                if (b)
                {
                    MessageBox.Show("请按“省-市-区(镇)”填写患者户口地址！如果没有区（县）直接在地址后面加“区”字", "提示！");
                    this.cmbHomeAdrr.Focus();
                    return -1;
                }
            }

            //住址邮编
            if (cmbHomeAdrr.Text.Trim() != "-" && txtHomeZip.Text.Trim() == "")
            {
                MessageBox.Show("请填写患者家庭住址邮编！", "提示！");
                this.txtHomeZip.Focus();
                return -1;
            }
            //单位地址
            if (txtAddressBusiness.Text.Trim() == "")
            {
                MessageBox.Show("请填写患者单位地址！", "提示！");
                this.txtAddressBusiness.Focus();
                return -1;
            }
            //单位电话
            if (txtAddressBusiness.Text.Trim() != "-" && txtPhoneBusiness.Text.Trim() == "")
            {
                MessageBox.Show("请填写患者单位电话！", "提示！");
                this.txtPhoneBusiness.Focus();
                return -1;
            }
            //单位邮编
            if (txtAddressBusiness.Text.Trim() != "-" && txtBusinessZip.Text.Trim() == "")
            {
                MessageBox.Show("请填写患者单位邮编！", "提示！");
                this.txtBusinessZip.Focus();
                return -1;
            }
            //联系人名称
            if (txtKin.Text.Trim() == "")
            {
                MessageBox.Show("请填写联系人名称！", "提示！");
                this.txtKin.Focus();
                return -1;
            }
            //与患者关系
            if (cmbRelation.Tag == null || (cmbRelation.Tag != null && cmbRelation.Tag.ToString() == ""))
            {
                MessageBox.Show("请选择与患者关系！", "提示！");
                this.cmbRelation.Focus();
                return -1;
            }
            //联系电话
            if (txtLinkmanTel.Text.Trim() == "")
            {
                MessageBox.Show("请填写联系电话！", "提示！");
                this.txtLinkmanTel.Focus();
                return -1;
            }
            //联系地址
            if (txtLinkmanAdd.Text.Trim() == "")
            {
                MessageBox.Show("请填写联系地址！", "提示！");
                this.txtLinkmanAdd.Focus();
                return -1;
            }
            //入院途径
            if (this.cmbInPath.Tag == null || (cmbInPath.Tag != null && cmbInPath.Tag.ToString() == ""))
            {
                MessageBox.Show("请选择入院途径！", "提示！");
                this.cmbInPath.Focus();
                return -1;
            }
            //入院科室
            if (cmbDeptInHospital.Tag == null || (cmbDeptInHospital.Tag != null && cmbDeptInHospital.Tag.ToString() == ""))
            {
                MessageBox.Show("请选择入院科室！", "提示！");
                this.cmbDeptInHospital.Focus();
                return -1;
            }
            //出院科室代码
            if (cmbDeptOutHospital.Tag == null || (cmbDeptOutHospital.Tag != null && cmbDeptOutHospital.Tag.ToString() == ""))
            {
                MessageBox.Show("请选择出院科室！", "提示！");
                this.cmbDeptOutHospital.Focus();
                return -1;
            }
            //入院病室
            if (txtInRoom.Text.Trim() == "")
            {
                MessageBox.Show("请填写入院病室！", "提示！");
                this.txtInRoom.Focus();
                return -1;
            }
            //出院病室
            if (txtOutRoom.Text.Trim() == "")
            {
                MessageBox.Show("请填写出院病室！", "提示！");
                this.txtOutRoom.Focus();
                return -1;
            }
            //出院日期

            //住院天数
            if (txtPiDays.Text.Trim() == "")
            {
                MessageBox.Show("请填写住院天数！", "提示！");
                this.txtPiDays.Focus();
                return -1;
            }

            if (cmbClinicDiagName.Text.Trim() == "")
            {
                MessageBox.Show("请填写门诊诊断！", "提示！");
                this.cmbClinicDiagName.Focus();
                return -1;
            }
            //门诊诊断编码
            //门诊诊断医生 ID
            if (cmbClinicDiagName.Text.Trim() != "" && (txtClinicDocd.Tag == null || (txtClinicDocd.Tag != null && txtClinicDocd.Tag.ToString() == "")))
            {
                MessageBox.Show("请选择门诊诊断医生！", "提示！");
                this.txtClinicDocd.Focus();
                return -1;
            }
            if (txtFirstDept.Text.Trim() != string.Empty && dtFirstTime.Value.Date < this.dtDateIn.Value.Date)
            {
                MessageBox.Show("第一次转科时间不能小于入院时间");
                dtFirstTime.Focus();
                return -1;
            }
            if (txtFirstDept.Text.Trim() != string.Empty && dtFirstTime.Value.Date > this.txtDateOut.Value.Date)
            {
                MessageBox.Show("第一次转科时间不能大于于出院时间");
                dtFirstTime.Focus();
                return -1;
            }
            if ((dtFirstTime.Value.Date > dtSecondTime.Value.Date) && txtDeptSecond.Text.Trim() != string.Empty)
            {
                MessageBox.Show("第一次转科时间不能大于第二次转科时间");
                dtFirstTime.Focus();
                return -1;
            }
            if ((dtSecondTime.Value.Date > dtThirdTime.Value.Date) && txtDeptThird.Text.Trim() != string.Empty)
            {
                MessageBox.Show("第二次转科时间不能大于第三次转科时间");
                dtSecondTime.Focus();
                return -1;
            }
            //病例分型
            if (cmbExampleType.Tag == null || (cmbExampleType.Tag != null && cmbExampleType.Tag.ToString() == ""))
            {
                MessageBox.Show("请选择病例分型！", "提示！");
                this.cmbExampleType.Focus();
                return -1;
            }
            //临床路径病例
            if (cmbClinicPath.Tag == null || (cmbClinicPath.Tag != null && cmbClinicPath.Tag.ToString() == ""))
            {
                MessageBox.Show("请选择临床路径病例！", "提示！");
                this.cmbClinicPath.Focus();
                return -1;
            }
            //抢救次数
            if (txtSalvTimes.Text.Trim() == "")
            {
                MessageBox.Show("请填写抢救次数！", "提示！");
                this.txtSalvTimes.Focus();
                return -1;
            }
            //成功次数
            if (txtSuccTimes.Text.Trim() == "")
            {
                MessageBox.Show("请填写成功次数！", "提示！");
                this.txtSuccTimes.Focus();
                return -1;
            }
            //第二页内容
            //损伤中毒原因
            if (txtInjuryOrPoisoningCause.Text.Trim() == "")
            {
                MessageBox.Show("请填写损伤中毒原因！", "提示！");
                this.txtInjuryOrPoisoningCause.Focus();
                return -1;
            }
            //病理诊断
            if (cmbPathologicalDiagName.Text.Trim() == "")
            {
                MessageBox.Show("请填写病理诊断！", "提示！");
                this.cmbPathologicalDiagName.Focus();
                return -1;
            }
            //病理诊断编码
            if (txtPathologicalDiagCode.Text.Trim() == "无")
            {
                MessageBox.Show("请填写病理诊断，如果没有请填写“未发现”或“-”！", "提示！");
                this.cmbPathologicalDiagName.Focus();
                return -1;
            }
            //病理号
            if (cmbPathologicalDiagName.Text.Trim() != "" && txtPathologicalDiagNum.Text.Trim() == "")
            {
                MessageBox.Show("请填写病理号！", "提示！");
                this.txtPathologicalDiagNum.Focus();
                return -1;
            }
            //药物过敏
            if (cmbIsDrugAllergy.Tag == null || (cmbIsDrugAllergy.Tag != null && cmbIsDrugAllergy.Tag.ToString() == ""))
            {
                MessageBox.Show("请填写药物过敏！", "提示！");
                this.cmbIsDrugAllergy.Focus();
                return -1;
            }
            ////过敏药物1
            if (cmbIsDrugAllergy.Tag.ToString() == "2" && txtDrugAllergy.Text.Trim() == "")
            {
                MessageBox.Show("请填写过敏药物！", "提示！");
                this.txtDrugAllergy.Focus();
                return -1;
            }
            //死亡患者尸检
            if (txtLeaveHopitalType.Text.Trim() == "5" && (cmbDeathPatientBobyCheck.Tag == null || (cmbDeathPatientBobyCheck.Tag != null && cmbDeathPatientBobyCheck.Tag.ToString() == "")))
            {
                MessageBox.Show("死亡患者,请选择死亡患者尸检！", "提示！");
                this.cmbDeathPatientBobyCheck.Focus();
                return -1;
            }
            if (cmbDeathPatientBobyCheck.Tag != null && (cmbDeathPatientBobyCheck.Tag.ToString() == "1" || cmbDeathPatientBobyCheck.Tag.ToString() == "2"))
            {
                if (txtLeaveHopitalType.Text.Trim() != "5")
                {
                    MessageBox.Show("死亡病人才能选择尸检，请重新选择。", "提示！");
                    this.cmbDeathPatientBobyCheck.Focus();
                    return -1;
                }
            }
            //血型编码
            if (cmbBloodType.Tag == null || (cmbBloodType.Tag != null && cmbBloodType.Tag.ToString() == ""))
            {
                MessageBox.Show("请选择血型！", "提示！");
                this.cmbBloodType.Focus();
                return -1;
            }
            //Rh血型(阴阳)
            if (cmbRhBlood.Tag == null || (cmbRhBlood.Tag != null && cmbRhBlood.Tag.ToString() == ""))
            {
                MessageBox.Show("请选择Rh血型！", "提示！");
                this.cmbRhBlood.Focus();
                return -1;
            }
            ////科主任代码
            //if (txtDeptChiefDoc.Tag == null)
            //{
            //    MessageBox.Show("请选择科主任！", "提示！");
            //    this.txtDeptChiefDoc.Focus();
            //    return;
            //}
            ////主任医师代码
            //txtConsultingDoctor.Tag = info.PatientInfo.PVisit.ConsultingDoctor.ID;
            ////ConsultingDoctor.Text = info.PatientInfo.PVisit.ConsultingDoctor.Name;
            ////主治医师代码
            //txtAttendingDoctor.Tag = info.PatientInfo.PVisit.AttendingDoctor.ID;
            ////AttendingDoctor.Text = info.PatientInfo.PVisit.AttendingDoctor.Name;
            ////住院医师代码
            //txtAdmittingDoctor.Tag = info.PatientInfo.PVisit.AdmittingDoctor.ID;
            //住院医师姓名
            //AdmittingDoctor.Text = info.PatientInfo.PVisit.AdmittingDoctor.Name;
            // 责任护士
            if (cmbDutyNurse.Tag == null || (cmbDutyNurse.Tag != null && cmbDutyNurse.Tag.ToString() == ""))
            {
                MessageBox.Show("请选择责任护士！", "提示！");
                this.cmbDutyNurse.Focus();
                return -1;
            }
            ////进修医师代码
            //txtRefresherDocd.Tag = info.RefresherDoc.ID;
            ////RefresherDocd.Text = info.RefresherDoc.Name;
            ////实习医师代码
            //txtPraDocCode.Tag = info.PatientInfo.PVisit.TempDoctor.ID;
            //PraDocCode.Text = info.GraduateDoc.Name;
            ////编码员
            //txtCodingCode.Tag = info.CodingOper.ID;
            //CodingCode.Text = info.CodingName;
            //病案质量
            //ArrayList QualList = con.GetList("CASEQUALFORPRI");
            //if (QualList != null && QualList.Count > 0)//医生打印时不需要质控
            //{

            //}
            //else
            //{
            //if (cmbMrQual.Tag == null || (cmbMrQual.Tag != null && cmbMrQual.Tag.ToString() == ""))
            //{
            //    MessageBox.Show("请选择病案质量！", "提示！");
            //    this.cmbMrQual.Focus();
            //    return -1;
            //}
            ////质控医师代码
            //if (cmbMrQual.Tag != null && (cmbQcDocd.Tag == null || (cmbQcDocd.Tag != null && cmbQcDocd.Tag.ToString() == "")))
            //{
            //    MessageBox.Show("请选择质控医师代码！", "提示！");
            //    this.cmbQcDocd.Focus();
            //    return -1;
            //}
            ////质控护士代码
            //if (cmbMrQual.Tag != null && (cmbQcNucd.Tag == null || (cmbQcNucd.Tag != null && cmbQcNucd.Tag.ToString() == "")))
            //{
            //    MessageBox.Show("请选择质控护士！", "提示！");
            //    this.cmbQcNucd.Focus();
            //    return -1;
            //    }
            //}
            //离院方式
            if (txtLeaveHopitalType.Text.Trim() == "")
            {
                MessageBox.Show("请填写离院方式！", "提示！");
                this.txtLeaveHopitalType.Focus();
                return -1;
            }
            //医嘱转院接收医疗机构
            if (txtLeaveHopitalType.Text.Trim() == "2" && txtHighReceiveHopital.Text.Trim() == "")
            {
                MessageBox.Show("请填写医嘱转院接收医疗机构！", "提示！");
                this.txtHighReceiveHopital.Focus();
                return -1;
            }
            //医嘱转社区
            if (txtLeaveHopitalType.Text.Trim() == "3" && txtLowerReceiveHopital.Text.Trim() == "")
            {
                MessageBox.Show("请填写医嘱转社区！", "提示！");
                this.txtLowerReceiveHopital.Focus();
                return -1;
            }
            //死亡
            int savTime = 0;
            int sunTime = 0;
            try
            {
                savTime = FS.FrameWork.Function.NConvert.ToInt32(this.txtSalvTimes.Text.Trim());
                sunTime = FS.FrameWork.Function.NConvert.ToInt32(this.txtSuccTimes.Text.Trim());
            }
            catch
            {
                savTime = 0;
                sunTime = 0;
            }
            if (txtLeaveHopitalType.Text.Trim() == "5" && savTime - sunTime > 1)
            {
                MessageBox.Show("死亡患者只有一次抢救不成功机会，请核对！", "提示！");
                this.txtSalvTimes.Focus();
                return -1;
            }
            if (txtLeaveHopitalType.Text.Trim() != "5" && sunTime > 0 && savTime - sunTime != 0)
            {
                MessageBox.Show("离院方式非死亡患者，抢救次数应等于抢救成功次数，请核对！", "提示！");
                this.txtSalvTimes.Focus();
                return -1;
            }
            //出院31天内再住院计划
            if (this.cmbComeBackInMonth.Tag == null || (cmbComeBackInMonth.Tag != null && cmbComeBackInMonth.Tag.ToString() == ""))
            {
                MessageBox.Show("请填写出院31天内再住院计划！", "提示！");
                this.cmbComeBackInMonth.Focus();
                return -1;
            }
            //出院31天再住院目的
            if (this.cmbComeBackInMonth.Tag.ToString() == "2" && cmbComeBackPurpose.Text.Trim() == "")
            {
                MessageBox.Show("请填写出院31天再住院目的！", "提示！");
                this.cmbComeBackPurpose.Focus();
                return -1;
            }
            //颅脑损伤患者昏迷时间 -入院前 天
            if (txtOutComeDay.Text.Trim() == "")
            {
                MessageBox.Show("请填写颅脑损伤患者昏迷时间 -入院前 天！", "提示！");
                this.txtOutComeDay.Focus();
                return -1;
            }
            //颅脑损伤患者昏迷时间 -入院前 小时
            if (txtOutComeHour.Text.Trim() == "")
            {
                MessageBox.Show("请填写颅脑损伤患者昏迷时间 -入院前 小时！", "提示！");
                this.txtOutComeHour.Focus();
                return -1;
            }
            //颅脑损伤患者昏迷时间 -入院前 分钟
            if (txtOutComeMin.Text.Trim() == "")
            {
                MessageBox.Show("请填写颅脑损伤患者昏迷时间 -入院前 分钟！", "提示！");
                this.txtOutComeMin.Focus();
                return -1;
            }
            //颅脑损伤患者昏迷时间 -入院后 天
            if (txtInComeDay.Text.Trim() == "")
            {
                MessageBox.Show("请填写颅脑损伤患者昏迷时间 -入院后 天！", "提示！");
                this.txtInComeDay.Focus();
                return -1;
            }
            //颅脑损伤患者昏迷时间 -入院后 小时
            if (txtInComeHour.Text.Trim() == "")
            {
                MessageBox.Show("请填写颅脑损伤患者昏迷时间 -入院后 小时！", "提示！");
                this.txtInComeHour.Focus();
                return -1;
            }
            //颅脑损伤患者昏迷时间 -入院后 分钟
            if (txtInComeMin.Text.Trim() == "")
            {
                MessageBox.Show("请填写颅脑损伤患者昏迷时间 -入院后 分钟！", "提示！");
                this.txtInComeMin.Focus();
                return -1;
            }
            //病人来源 保留
            if (txtInAvenue.Tag == null || (txtInAvenue.Tag != null && txtInAvenue.Tag.ToString() == ""))
            {
                MessageBox.Show("请选择病人来源！", "提示！");
                this.txtInAvenue.Focus();
                return -1;
            }
            //门诊与出院符合 保留
            if (txtCePi.Tag == null || (txtCePi.Tag != null && txtCePi.Tag.ToString() == ""))
            {
                MessageBox.Show("请选择门诊与出院符合！", "提示！");
                this.txtCePi.Focus();
                return -1;
            }
            //临床与病理符合 保留
            if (this.cmbPathologicalDiagName.Text.Trim() != "" && this.cmbPathologicalDiagName.Text.Trim() != "-")
            {
                if (txtClPa.Tag == null || (txtClPa.Tag != null && txtClPa.Tag.ToString() == ""))
                {
                    MessageBox.Show("请选择临床与病理符合！", "提示！");
                    this.txtClPa.Focus();
                    return -1;
                }
            }
            FS.HISFC.BizLogic.HealthRecord.Diagnose DiaMgr = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
            ArrayList al = new ArrayList();
            //al = DiaMgr.QueryCaseDiagnoseForClinic(this.CaseBase.PatientInfo.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
            al = DiaMgr.QueryCaseDiagnose(this.CaseBase.PatientInfo.ID, "%", FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, FS.HISFC.Models.Base.ServiceTypes.I);
            if (al == null || al.Count < 1)
            {
                MessageBox.Show("请填写患者出院诊断信息！", "提示！");
                this.tab1.SelectedTab = this.tabPage2;
                return -1;
            }
            bool isHavedMain = false;
            bool isHavedState = false;
            foreach (FS.HISFC.Models.HealthRecord.Diagnose diagNose in al)
            {

                if (diagNose.DiagInfo.DiagType.ID == "1" && !isHavedMain)
                {
                    isHavedMain = true;
                }
                if (diagNose.DiagOutState == "")
                {
                    isHavedState = true;
                }
            }
            if (!isHavedMain)
            {
                MessageBox.Show("请选择一个出院诊断作为主要诊断！", "提示！");
                this.tab1.SelectedTab = this.tabPage2;
                return -1;
            }
            if (isHavedState)
            {
                MessageBox.Show("请选择出院诊断的入院病情！", "提示！");
                this.tab1.SelectedTab = this.tabPage2;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 病历提交之前，病案首页缺项控制，所以病历提交时候需要做提示
        /// </summary>
        public int CommitCheck(string InpatientNo)
        {

            //使用最新电子病历2012-9-11
            FS.FrameWork.Models.NeuObject NewEMRInfo = con.GetConstant("CASENEWEMR", "1");
            if (NewEMRInfo != null && NewEMRInfo.Memo == "1")
            {
                this.isNewEMR = true;
                //this.InitCountryList();
            }

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
            this.dutyNurse = patientInfo.PVisit.AdmittingNurse.ID;
            this.patient_no = patientInfo.PID.PatientNO;
            patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
            //end add chengym
            CaseBase = baseDml.GetCaseBaseInfo(InpatientNo);//病案主表信息（met_cas_base）
            if (CaseBase.PatientInfo.Pact.ID == null || (CaseBase.PatientInfo.Pact.ID != null && CaseBase.PatientInfo.Pact.ID.ToString() == ""))
            {
                MessageBox.Show("请选择住院号" + patient_no + "的医疗付款方式！", "提示！");
                return -1;
            }
            //医保公费号
            if (CaseBase.PatientInfo.Pact.ID == "1" || CaseBase.PatientInfo.Pact.ID == "2" || CaseBase.PatientInfo.Pact.ID == "3")
            {

                if (CaseBase.PatientInfo.SSN == "")
                {
                    MessageBox.Show("请选择住院号" + patient_no + "的医保公费号！", "提示！");
                    return -1;
                }
            }
            //住院次数
            if (CaseBase.PatientInfo.InTimes.ToString() == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的住院次数！", "提示！");
                return -1;
            }
            ////住院号  病历号
            //if (CaseBase.PatientInfo.PID.CaseNO.ToString() == "")
            //{
            //    MessageBox.Show("请选择住院号" + InpatientNo + "的住院号、病历号！", "提示！");
            //    return -1;
            //}
            //姓名
            if (CaseBase.PatientInfo.Name == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的姓名！", "提示！");
                return -1;
            }
            //性别
            if (CaseBase.PatientInfo.Sex == null || (CaseBase.PatientInfo.Sex != null && CaseBase.PatientInfo.Sex.ToString() == ""))
            {
                MessageBox.Show("请选择住院号" + patient_no + "的性别！", "提示！");
                return -1;
            }
            //生日
            if (CaseBase.PatientInfo.Birthday.ToString() == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的生日！", "提示！");
                return -1;
            }
            //国籍 编码
            if (CaseBase.PatientInfo.Country.ID.ToString() == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的国籍！", "提示！");
                return -1;
            }
            //民族 
            if (CaseBase.PatientInfo.Nationality.ID.ToString() == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的民族！", "提示！");
                return -1;
            }
            //新生儿出生体重
            FS.HISFC.BizLogic.HealthRecord.Baby babyMgr = new FS.HISFC.BizLogic.HealthRecord.Baby();
            ArrayList babyAl = babyMgr.QueryBabyByInpatientNo(this.CaseBase.PatientInfo.ID);
            if (babyAl != null && babyAl.Count > 0)
            {
                int weith = 0;
                try
                {
                    weith = FS.FrameWork.Function.NConvert.ToInt32(CaseBase.BabyBirthWeight);
                }
                catch
                {
                    weith = 0;
                }
                if (weith == 0)
                {
                    if (CaseBase.BabyBirthWeight == "-" || CaseBase.BabyBirthWeight == "")
                    {
                        MessageBox.Show("有新生儿，请填写新生儿出生体重,新生儿入院体重！", "提示！");
                        return -1;
                    }

                }
            }
            //新生儿入院体重
            if (CaseBase.BabyInWeight == "")
            {
                MessageBox.Show("请填写新生儿入院体重！", "提示");
                return -1;
            }
            //出生地
            if (CaseBase.PatientInfo.AreaCode == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的出生地！", "提示！");
                return -1;
            }
            //籍贯
            if (CaseBase.PatientInfo.DIST == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的籍贯！", "提示！");
                return -1;
            }
            //身份证号
            if (CaseBase.PatientInfo.IDCard == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的身份证号！", "提示！");
                return -1;
            }
            //职业
            if (CaseBase.PatientInfo.Profession.ID == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的职业！", "提示！");
                return -1;
            }
            //婚姻 
            if (CaseBase.PatientInfo.MaritalStatus.ID.ToString() == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的婚姻！", "提示！");
                return -1;
            }
            //现住址
            //ArrayList addrlist = con.GetList("GBXZQYHF");
            //if (cmbCountry.Tag.ToString() == "156")
            //{
            //    bool aaa1 = true;

            //    if (CaseBase.CurrentAddr.IndexOf("区") > 0)
            //    {

            //        for (int i = 0; i < addrlist.Count; i++)
            //        {
            //            if (addrlist[i].ToString() == CaseBase.CurrentAddr.Substring(0, CaseBase.CurrentAddr.IndexOf("区") + 1))
            //            {
            //                if (addrlist[i].ToString() == "北京市市辖区" || addrlist[i].ToString() == "天津市市辖区" || addrlist[i].ToString() == "上海市市辖区" || addrlist[i].ToString() == "重庆市市辖区")
            //                {
            //                    aaa1 = false;
            //                }
            //                DrrHelper.ArrayObject = addrlist;
            //                if (DrrHelper.GetID(addrlist[i].ToString()).Length != 4)
            //                {
            //                    aaa1 = false;
            //                }
            //            }

            //        }
            //    }
            //    else if (CaseBase.CurrentAddr.IndexOf("县") > 0)
            //    {
            //        for (int i = 0; i < addrlist.Count; i++)
            //        {
            //            if (addrlist[i].ToString() == CaseBase.CurrentAddr.Substring(0, CaseBase.CurrentAddr.IndexOf("县") + 1))
            //            {
            //                DrrHelper.ArrayObject = addrlist;
            //                if (DrrHelper.GetID(addrlist[i].ToString()).Length != 4)
            //                {
            //                    aaa1 = false;
            //                }
            //            }
            //        }
            //    }
            //    else if (CaseBase.CurrentAddr.IndexOf("州") > 0)
            //    {
            //        for (int i = 0; i < addrlist.Count; i++)
            //        {
            //            if (addrlist[i].ToString() == CaseBase.CurrentAddr.Substring(0, CaseBase.CurrentAddr.IndexOf("州") + 1))
            //            {
            //                DrrHelper.ArrayObject = addrlist;
            //                if (DrrHelper.GetID(addrlist[i].ToString()).Length != 4)
            //                {
            //                    aaa1 = false;
            //                }
            //            }
            //        }
            //    }
            //    else if (CaseBase.CurrentAddr.IndexOf("省") > 0 && CaseBase.CurrentAddr.IndexOf("市") > 0)
            //    {
            //        for (int i = 0; i < addrlist.Count; i++)
            //        {
            //            if (addrlist[i].ToString() == CaseBase.CurrentAddr.Substring(0, CaseBase.CurrentAddr.IndexOf("市") + 1))
            //            {
            //                DrrHelper.ArrayObject = addrlist;
            //                if (DrrHelper.GetID(addrlist[i].ToString()).Length != 4)
            //                {
            //                    aaa1 = false;
            //                }
            //            }
            //        }
            //    }


            //    else
            //    {
            //        MessageBox.Show("请按“省-市-区(镇)”填写患者现住址！", "提示！");
            //        return -1;
            //    }
            //    if (aaa1)
            //    {
            //        MessageBox.Show("请按“省-市-区(镇)”填写患者现住址！", "提示！");
            //        return -1;
            //    }
            //}
            //if (CaseBase.CurrentAddr == "" || CaseBase.CurrentAddr == "无" || CaseBase.CurrentAddr == "未提供")
            //{
            //    MessageBox.Show("请选择住院号" + InpatientNo + "的详细现住址，如果没提供请填写减号“-”！", "提示！");
            //    return -1;
            //}
            //现住址电话 //家庭电话
            if (CaseBase.CurrentAddr.ToString() != "-" && CaseBase.CurrentPhone == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的现住址电话，如果没提供请填写减号“-”！", "提示！");
                return -1;
            }
            //现住址邮编
            if (CaseBase.CurrentAddr.ToString() != "-" && CaseBase.CurrentZip == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的现住址邮编，如果没提供请填写减号“-”！", "提示！");
                return -1;
            }
            //家庭住址
            //if (CaseBase.PatientInfo.AddressHome == "")
            //{
            //    MessageBox.Show("请选择住院号" + InpatientNo + "的家庭住址，如果没提供请填写减号“-”！", "提示！");
            //    return -1;
            //}
            //if (cmbCountry.Tag.ToString() == "156")
            //{
            //    bool bbb1 = true;

            //    if (CaseBase.PatientInfo.AddressHome.IndexOf("区") > 0)
            //    {

            //        for (int i = 0; i < addrlist.Count; i++)
            //        {
            //            if (addrlist[i].ToString() == CaseBase.PatientInfo.AddressHome.Substring(0, CaseBase.PatientInfo.AddressHome.IndexOf("区") + 1))
            //            {
            //                if (addrlist[i].ToString() == "北京市市辖区" || addrlist[i].ToString() == "天津市市辖区" || addrlist[i].ToString() == "上海市市辖区" || addrlist[i].ToString() == "重庆市市辖区")
            //                {
            //                    bbb1 = false;
            //                }
            //                DrrHelper.ArrayObject = addrlist;
            //                if (DrrHelper.GetID(addrlist[i].ToString()).Length != 4)
            //                {
            //                    bbb1 = false;
            //                }
            //            }

            //        }
            //    }
            //    else if (CaseBase.PatientInfo.AddressHome.IndexOf("县") > 0)
            //    {
            //        for (int i = 0; i < addrlist.Count; i++)
            //        {
            //            if (addrlist[i].ToString() == CaseBase.PatientInfo.AddressHome.Substring(0, CaseBase.PatientInfo.AddressHome.IndexOf("县") + 1))
            //            {
            //                DrrHelper.ArrayObject = addrlist;
            //                if (DrrHelper.GetID(addrlist[i].ToString()).Length != 4)
            //                {
            //                    bbb1 = false;
            //                }
            //            }
            //        }
            //    }
            //    else if (CaseBase.PatientInfo.AddressHome.IndexOf("州") > 0)
            //    {
            //        for (int i = 0; i < addrlist.Count; i++)
            //        {
            //            if (addrlist[i].ToString() == CaseBase.PatientInfo.AddressHome.Substring(0, CaseBase.PatientInfo.AddressHome.IndexOf("州") + 1))
            //            {
            //                DrrHelper.ArrayObject = addrlist;
            //                if (DrrHelper.GetID(addrlist[i].ToString()).Length != 4)
            //                {
            //                    bbb1 = false;
            //                }
            //            }
            //        }
            //    }
            //    else if (CaseBase.PatientInfo.AddressHome.IndexOf("省") > 0 && CaseBase.PatientInfo.AddressHome.IndexOf("市") > 0)
            //    {
            //        for (int i = 0; i < addrlist.Count; i++)
            //        {
            //            if (addrlist[i].ToString() == CaseBase.PatientInfo.AddressHome.Substring(0, CaseBase.PatientInfo.AddressHome.IndexOf("市") + 1))
            //            {
            //                DrrHelper.ArrayObject = addrlist;
            //                if (DrrHelper.GetID(addrlist[i].ToString()).Length != 4)
            //                {
            //                    bbb1 = false;
            //                }
            //            }
            //        }
            //    }


            //    else
            //    {
            //        MessageBox.Show("请按“省-市-区(镇)”填写患者户口地址！", "提示！");
            //        return -1;
            //    }
            //    if (bbb1)
            //    {
            //        MessageBox.Show("请按“省-市-区(镇)”填写患者户口地址！", "提示！");
            //        return -1;
            //    }
            //}

            //住址邮编
            if (CaseBase.PatientInfo.AddressHome != "-" && CaseBase.PatientInfo.HomeZip == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的家庭住址邮编，如果没提供请填写减号“-”！", "提示！");
                return -1;
            }
            //单位地址
            if (CaseBase.PatientInfo.AddressBusiness == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的单位地址，如果没提供请填写减号“-”！", "提示！");
                return -1;
            }
            //单位电话
            if (CaseBase.PatientInfo.AddressBusiness != "-" && CaseBase.PatientInfo.PhoneBusiness == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的单位电话，如果没提供请填写减号“-”！", "提示！");
                return -1;
            }
            //单位邮编
            if (CaseBase.PatientInfo.AddressBusiness != "-" && CaseBase.PatientInfo.BusinessZip == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的单位邮编，如果没提供请填写减号“-”！", "提示！");
                return -1;
            }
            //联系人名称
            if (CaseBase.PatientInfo.Kin.Name == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的联系人名称，如果没提供请填写减号“-”！", "提示！");
                return -1;
            }
            //与患者关系
            if (CaseBase.PatientInfo.Kin.RelationLink.ToString() == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的患者关系！", "提示！");
                return -1;
            }
            //联系电话
            if (CaseBase.PatientInfo.Kin.RelationPhone == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的联系电话，如果没提供请填写减号“-”！", "提示！");
                return -1;
            }
            //联系地址
            if (CaseBase.PatientInfo.Kin.RelationAddress == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的联系地址，如果没提供请填写减号“-”！", "提示！");
                return -1;
            }
            //入院途径
            if (CaseBase.InPath == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的入院途径！", "提示！");
                return -1;
            }
            //入院科室
            if (CaseBase.InDept.Name == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的入院科室！", "提示！");
                return -1;
            }
            //出院科室代码
            if (CaseBase.OutDept.Name == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的出院科室！", "提示！");
                return -1;
            }
            //入院病室
            if (CaseBase.InRoom == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的入院病室！", "提示！");
                return -1;
            }
            //出院病室
            if (CaseBase.OutRoom == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的出院病室！", "提示！");
                return -1;
            }
            //门诊诊断
            if (CaseBase.ClinicDiag.Name == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的门诊诊断！", "提示！");
                return -1;
            }
            //门诊诊断医生 ID
            if (CaseBase.ClinicDiag.Name != "" && CaseBase.ClinicDoc.Name == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的门诊诊断医生！", "提示！");
                return -1;
            }
            //转科时间没有控制
            //病例分型
            if (CaseBase.ExampleType == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的病历分型！", "提示！");
                return -1;
            }
            //临床路径病例
            if (CaseBase.ClinicPath == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的临床路径病历！", "提示！");
                return -1;
            }
            //抢救次数
            if (CaseBase.SalvTimes.ToString() == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的抢救次数！", "提示！");
                return -1;
            }
            //成功次数
            if (CaseBase.SuccTimes.ToString() == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的成功次数！", "提示！");
                return -1;
            }
            //第二页内容
            //损伤中毒原因
            if (CaseBase.InjuryOrPoisoningCause == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的损伤中毒原因，如果没提供请填写减号“-”！", "提示！");
                return -1;
            }
            //病理诊断
            if (CaseBase.PathologicalDiagName == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的病理诊断，如果没提供请填写减号“-”！", "提示！");
                return -1;
            }
            //病理号
            if (CaseBase.PathologicalDiagName != "" && CaseBase.PathNum == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的病理号！", "提示！");
                return -1;
            }
            //药物过敏
            if (CaseBase.AnaphyFlag == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的药物过敏！", "提示！");
                return -1;
            }
            ////过敏药物1
            if (CaseBase.AnaphyFlag == "2" && CaseBase.FirstAnaphyPharmacy.ID.ToString() == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的过敏药物！", "提示！");
                return -1;
            }
            //死亡患者尸检
            if (CaseBase.Out_Type == "5" && CaseBase.CadaverCheck == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的死亡患者尸检！", "提示！");
                return -1;
            }
            if (CaseBase.CadaverCheck != "" && (CaseBase.CadaverCheck == "1" || CaseBase.CadaverCheck == "2"))
            {
                if (CaseBase.Out_Type != "5")
                {
                    MessageBox.Show("死亡病人才能选择尸检，请重新选择。", "提示！");
                    return -1;
                }
            }
            //血型编码
            if (CaseBase.PatientInfo.BloodType.ToString() == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的血型！", "提示！");
                return -1;
            }
            //Rh血型(阴阳)
            if (CaseBase.RhBlood == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的Rh血型！", "提示！");
                return -1;
            }

            // 责任护士
            if (CaseBase.DutyNurse.ToString() == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的责任护士！", "提示！");
                return -1;
            }
            //离院方式
            if (CaseBase.Out_Type == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的离院方式！", "提示！");
                return -1;
            }
            //医嘱转院接收医疗机构
            if (CaseBase.Out_Type == "2" && CaseBase.HighReceiveHopital == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的医嘱转院接受医疗机构！", "提示！");
                return -1;
            }
            //医嘱转社区
            if (CaseBase.Out_Type == "3" && CaseBase.LowerReceiveHopital == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的医嘱转社区！", "提示！");
                return -1;
            }
            //死亡
            int savTime = 0;
            int sunTime = 0;
            try
            {
                savTime = FS.FrameWork.Function.NConvert.ToInt32(CaseBase.SalvTimes);
                sunTime = FS.FrameWork.Function.NConvert.ToInt32(CaseBase.SuccTimes);
            }
            catch
            {
                savTime = 0;
                sunTime = 0;
            }
            if (CaseBase.Out_Type == "5" && savTime - sunTime > 1)
            {
                MessageBox.Show("死亡患者只有一次抢救不成功机会，请核对！", "提示！");
                return -1;
            }
            if (CaseBase.Out_Type != "5" && sunTime > 0 && (savTime - sunTime != 0))
            {
                MessageBox.Show("离院方式非死亡患者，抢救次数应等于抢救成功次数，请核对！", "提示！");
                return -1;
            }
            //出院31天内再住院计划
            if (CaseBase.ComeBackInMonth == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的出院31天内再住院计划！", "提示！");
                return -1;
            }
            //出院31天再住院目的
            if (CaseBase.ComeBackInMonth == "2" && CaseBase.ComeBackPurpose == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的出院31天内再住院目的！", "提示！");
                return -1;
            }
            //颅脑损伤患者昏迷时间 -入院前 天
            if (CaseBase.OutComeDay.ToString() == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的颅脑损伤患者昏迷时间 -入院前 天！", "提示！");
                return -1;
            }
            //颅脑损伤患者昏迷时间 -入院前 小时
            if (CaseBase.OutComeHour.ToString() == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的颅脑损伤患者昏迷时间 -入院前 小时！", "提示！");
                return -1;
            }
            //颅脑损伤患者昏迷时间 -入院前 分钟
            if (CaseBase.OutComeMin.ToString() == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的颅脑损伤患者昏迷时间 -入院前 分钟！", "提示！");
                return -1;
            }
            //病人来源 保留
            if (CaseBase.InAvenue == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的病人来源！", "提示！");
                return -1;
            }
            //门诊与出院符合 保留
            if (CaseBase.CePi == "")
            {
                MessageBox.Show("请选择住院号" + patient_no + "的门诊与出院符合！", "提示！");
                return -1;
            }
            //临床与病理符合 保留
            if (CaseBase.PathologicalDiagName != "" && CaseBase.PathologicalDiagName != "-")
            {
                if (CaseBase.ClPa == "")
                {
                    MessageBox.Show("请选择住院号" + patient_no + "的临床与病理符合！", "提示！");
                    return -1;
                }
            }
            FS.HISFC.BizLogic.HealthRecord.Diagnose DiaMgr = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
            ArrayList al = new ArrayList();
            //al = DiaMgr.QueryCaseDiagnoseForClinic(this.CaseBase.PatientInfo.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
            al = DiaMgr.QueryCaseDiagnose(this.CaseBase.PatientInfo.ID, "%", FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, FS.HISFC.Models.Base.ServiceTypes.I);
            if (al == null || al.Count < 1)
            {
                MessageBox.Show("请填写住院号" + patient_no + "的患者出院诊断信息！", "提示！");
                this.tab1.SelectedTab = this.tabPage2;
                return -1;
            }
            bool isHavedMain = false;
            bool isHavedState = false;
            foreach (FS.HISFC.Models.HealthRecord.Diagnose diagNose in al)
            {

                if (diagNose.DiagInfo.DiagType.ID == "1" && !isHavedMain)
                {
                    isHavedMain = true;
                }
                if (diagNose.DiagOutState == "")
                {
                    isHavedState = true;
                }
            }
            if (!isHavedMain)
            {
                MessageBox.Show("请选择住院号" + patient_no + "的一个出院诊断作为主要诊断！", "提示！");
                return -1;
            }
            if (isHavedState)
            {
                MessageBox.Show("请选择住院号" + patient_no + "的出院诊断的入院病情！", "提示！");
                return -1;
            }
            return 1;

        }
        /// <summary>
        /// 第二页打印前检索
        /// </summary>
        /// <returns></returns>
        private int PrintSecCheck()
        {
            //从常数表中获取是否需要手术有无选择
            ArrayList list = con.GetList("CASEHAVEDOPS");
            if (list != null && list.Count > 0)
            {
                if (this.CaseBase.Ever_Sickintodeath == "")
                {
                    MessageBox.Show("请选择是否存在手术信息！", "提示！");
                    this.tab1.SelectedTab = this.tabPage3;
                    return -1;
                }
            }
            FS.HISFC.BizLogic.HealthRecord.Operation operationManager = new FS.HISFC.BizLogic.HealthRecord.Operation();
            ArrayList alOpr = operationManager.QueryOperationByInpatientNo(this.CaseBase.PatientInfo.ID);
       
            ArrayList alOPr = operationManager.QueryOperationByInpatientNo1(this.CaseBase.PatientInfo.ID);
            if (alOPr != null)
            {
                foreach (FS.HISFC.Models.HealthRecord.OperationDetail operationInfo in alOpr)
                {
                    
                    //if (operationInfo.MarcKind.ToString() == "")
                    //{
                    //    MessageBox.Show("请选择手术信息的麻醉方式！", "提示！");
                    //    return -1;
                    //}
                    if (operationInfo.NickKind.ToString() == "")
                    {
                        MessageBox.Show("请选择手术信息的切口种类，如果是操作类请填写01、0Ⅱ、0Ⅲ类切口，可不填愈合种类！", "提示！");
                        return -1;
                    }
                    if (operationInfo.NickKind.ToString() !="01"&&operationInfo.NickKind.ToString() !="02"&&operationInfo.NickKind.ToString() !="03"&&operationInfo.CicaKind.ToString() == "")
                    {
                        MessageBox.Show("请选择手术信息的愈合种类！", "提示！");
                        return -1;
                    }
                    //if (operationInfo.NarcDoctInfo.Name.ToString() == "")
                    //{
                    //    MessageBox.Show("请选择手术信息的麻醉医师！", "提示！");
                    //    return -1;
                    //}
                }
            }
          

            bool isBigOutDate = false;
            bool isMinInDate = false;
            if (alOpr != null && alOpr.Count > 0)
            {
                foreach (FS.HISFC.Models.HealthRecord.OperationDetail operationInfo in alOpr)
                {
                    if (this.CaseBase.PatientInfo.PVisit.OutTime.Date < operationInfo.OperationDate.Date)
                    {
                        isBigOutDate = true;
                        break;
                    }
                    if (this.CaseBase.PatientInfo.PVisit.InTime.Date > operationInfo.OperationDate.Date)
                    {
                        isMinInDate = true;
                        break;
                    }
                }
            }
            if (isBigOutDate)
            {
                MessageBox.Show("手术日期应该<=出院日期“" + this.CaseBase.PatientInfo.PVisit.OutTime.ToShortDateString() + "”！", "提示！");
                this.tab1.SelectedTab = this.tabPage3;
                return -1;
            }
            if (isMinInDate)
            {
                MessageBox.Show("手术日期应该>=入院日期“" + this.CaseBase.PatientInfo.PVisit.InTime.ToShortDateString() + "”！", "提示！");
                this.tab1.SelectedTab = this.tabPage3;
                return -1;
            }
            FS.HISFC.BizLogic.HealthRecord.Baby baby = new FS.HISFC.BizLogic.HealthRecord.Baby();
            //查询符合条件的数据
            ArrayList babyList = baby.QueryBabyByInpatientNo(this.CaseBase.PatientInfo.ID);
            bool isBrithEnd = false;
            if (babyList != null && babyList.Count > 0)
            {
                foreach (FS.HISFC.Models.HealthRecord.Baby babyInfo in babyList)
                {
                    if (babyInfo.BabyState == "3" && babyInfo.BirthEnd != "1")
                    {
                        isBrithEnd = true;
                        break;
                    }
                    if (babyInfo.BabyState == "2" && babyInfo.BirthEnd != "1")
                    {
                        isBrithEnd = true;
                        break;
                    }
                }
            }
            if (isBrithEnd)
            {
                MessageBox.Show("妇婴卡“分娩结果”是“出院”或“转科”的，“分娩情况”只能选择“活产”！！！请修改！", "提示！");
                this.tab1.SelectedTab = this.tabPage4;
                return -1;
            }
            //从常数表中获取是否需要判断有无妇婴卡信息
            ArrayList listBaby = con.GetList("CASEHAVEDBABY");
            if (listBaby != null && listBaby.Count > 0)
            {
                if (this.CaseBase.Ever_Firstaid == "")
                {
                    MessageBox.Show("请选择是否存在妇婴卡信息！", "提示！");
                    this.tab1.SelectedTab = this.tabPage4;
                    return -1;
                }
            }
            //从常数表中获取是否需要判断有无肿瘤卡信息
            ArrayList listTum = con.GetList("CASEHAVEDTUM");
            if (listTum != null && listTum.Count > 0)
            {
                if (this.CaseBase.Ever_Difficulty == "")
                {
                    MessageBox.Show("请选择是否存在肿瘤卡信息！", "提示！");
                    this.tab1.SelectedTab = this.tabPage5;
                    return -1;
                }
            }
            return 1;
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
            return 1;
        }
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

        //判断身份证号是否合法
        #region 判断身份证号
        private int ProcessIDENNO(string idNO)
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
                    this.txtIDNo.Focus();
                    return -1;
                }
                string[] reurnString = errText.Split(',');

                //if (this.dtPatientBirthday.Text != reurnString[1])
                //{
                //    MessageBox.Show(FS.FrameWork.Management.Language.Msg("输入的生日日期与身份证号码中的生日不符"));
                //    this.dtPatientBirthday.Focus();
                //    return -1;
                //}

                if (this.cmbPatientSex.Text != reurnString[2])
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("输入的性别与身份证中号的性别不符"));
                    this.cmbPatientSex.Focus();
                    return -1;
                }

            }
            catch
            {
            }
            return 1;
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


    }


}
