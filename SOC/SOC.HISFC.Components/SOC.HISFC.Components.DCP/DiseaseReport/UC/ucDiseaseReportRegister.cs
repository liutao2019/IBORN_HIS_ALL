using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;



namespace Neusoft.UFC.DCP.DiseaseReport
{

    /// <summary>
    /// ucDiseaseReportRegister<br></br>
    /// [功能描述: 报卡uc]<br></br>
    /// [创 建 者: ]<br></br>
    /// [创建时间: 2008-8-20]<br></br>
    /// <业务说明 
    ///		1、订正卡的概念：疑似病例改为确诊病例时对原来的卡进行订正。用户自行判断是否订正，不update原卡，copy原卡后由用户修改后insert
    ///                      对订正卡：扩展信息2记录原卡编号，原卡：扩展信息3记录订正卡编号（也可不记录）
    ///  />
    /// </summary>
    public partial class ucDiseaseReportRegister : Neusoft.FrameWork.WinForms.Controls.ucBaseControl,Neusoft.FrameWork.WinForms.Classes.IPreArrange
    {

        #region 帮助类

        /// <summary>
        /// 科室帮助类
        /// </summary>
        private Neusoft.FrameWork.Public.ObjectHelper deptHelper = new Neusoft.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 员工
        /// </summary>
        private Neusoft.FrameWork.Public.ObjectHelper employHelper = new Neusoft.FrameWork.Public.ObjectHelper();

        #endregion

        #region 变量
      
        /// <summary>
        /// 患者科室
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject PatientDept = new Neusoft.FrameWork.Models.NeuObject();       

        /// <summary>
        /// 住院患者管理类
        /// </summary>
        private Neusoft.HISFC.DCP.BizProcess.Patient patientProcess = new Neusoft.HISFC.DCP.BizProcess.Patient();

        /// <summary>
        /// 传染病管理类
        /// </summary>
        private Neusoft.HISFC.DCP.BizLogic.DiseaseReport diseaseMgr = new Neusoft.HISFC.DCP.BizLogic.DiseaseReport();        

        /// <summary>
        /// 开始时间
        /// </summary>
        private string BeginTime = "";

        /// <summary>
        /// 结束时间
        /// </summary>
        private string EndTime = "";

        /// <summary>
        /// 人员实体
        /// </summary>
        private Neusoft.HISFC.Models.Base.Employee User = new Neusoft.HISFC.Models.Base.Employee();        

        /// <summary>
        /// 患者的住院号
        /// </summary>
        //private string InpatientNO = "";
       
        /// <summary>
        /// 报告集合
        /// </summary>
        private ArrayList alReport = new ArrayList();        

        /// <summary>
        /// 常数管理类
        /// </summary>
        private Neusoft.HISFC.DCP.BizProcess.Common commonProcess = new Neusoft.HISFC.DCP.BizProcess.Common();

        /// <summary>
        /// ToolBar服务类
        /// </summary>
        private Neusoft.FrameWork.WinForms.Forms.ToolBarService toolBarService = new Neusoft.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// 操作类型
        /// </summary>
        private OperType operType;

        /// <summary>
        /// 所有传染病，用于弹出窗口
        /// </summary>
        private ArrayList alInfectItem = new ArrayList();

        /// <summary>
        /// 所有传染病，用于下拉
        /// </summary>
        private ArrayList alinfection = new ArrayList();

        /// <summary>
        /// 需要附卡的疾病
        /// </summary>
        private System.Collections.Hashtable hshNeedAdd;

        /// <summary>
        /// 传染病的类型[甲乙丙等]，检测选择传染病时是否选择了类型
        /// </summary>
        private System.Collections.Hashtable hshInfectClass;

        /// <summary>
        /// 需要报告性病卡的疾病
        /// </summary>
        private System.Collections.Hashtable hshNeedSexReport;

        /// <summary>
        /// 需要采血送检
        /// </summary>
        private System.Collections.Hashtable hshNeedCheckedBlood;

        /// <summary>
        /// 需要二级病例分类
        /// </summary>
        private System.Collections.Hashtable hshNeedCaseTwo;

        /// <summary>
        /// 需要电话报告的疾病
        /// </summary>
        private System.Collections.Hashtable hshNeedTelInfect;

        /// <summary>
        /// 需要结核病转诊单的疾病
        /// </summary>
        private System.Collections.Hashtable hshNeedBill;

        /// <summary>
        /// 需要备注的疾病
        /// </summary>
        private System.Collections.Hashtable hshNeedMemo;

        /// <summary>
        /// 新生儿破伤风
        /// </summary>
        private System.Collections.Hashtable hshLitteChild;

        /// <summary>
        /// 患者职业为学生[应提示填写学校机构之类]
        /// </summary>
        private System.Collections.Hashtable hshStudent;

        /// <summary>
        /// 需要二级名称的性病
        /// </summary>
        private System.Collections.Hashtable hshSexNeedGradeTwo;

        /// <summary>
        /// 需要描述的人群分类
        /// </summary>
        private System.Collections.Hashtable hshPatientTyepNeedDesc;

        /// <summary>
        /// 权限科室
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject myPriDept = new Neusoft.FrameWork.Models.NeuObject();        

        /// <summary>
        /// 地址信息是否从住院主表中加载
        /// </summary>
        bool isAddressLoad = false;              

        /// <summary>
        /// 附卡接口
        /// </summary>
        private Neusoft.HISFC.DCP.Interface.IAddition iAdditionReport;
        
        /// <summary>
        /// 患者待选择列表显示
        /// </summary>
        ListBox dataShowList = null;

        /// <summary>
        /// 是否预报预防控制权限
        /// </summary>
        private bool isCDCPriv = false;

        #region 权限相关变量

        /// <summary>
        /// 疾控权限相关科室
        /// </summary>
        List<Neusoft.FrameWork.Models.NeuObject> cdcPrivDeptList = new List<Neusoft.FrameWork.Models.NeuObject>();

        #endregion

        private string userPriv = "0";

        #endregion

        #region 属性

        /// <summary>
        /// 报告卡[在保健科审核时窗口show之前赋值]
        /// </summary>
        public ArrayList AlReport
        {
            get
            {
                return alReport;
            }
            set
            {
                alReport = value;
            }
        }

        /// <summary>
        /// 登录人员检索状态
        /// </summary>
        [Description("登录人员检索状态"), Category("设置"), DefaultValue("0")]
        public string UserPriv
        {
            get
            {
                return this.userPriv;
            }
            set
            {
                this.userPriv = value;
            }
        }


        /// <summary>
        /// 查询患者类型
        /// </summary>
        private Neusoft.HISFC.DCP.Enum.PatientType llbPatientType;

        /// <summary>
        /// 查询患者类型
        /// </summary>
        public Neusoft.HISFC.DCP.Enum.PatientType LlbPatientType
        {
            get
            {
                return llbPatientType;
            }
            set
            {
                llbPatientType = value;
            }
        }

        /// <summary>
        /// 患者实体
        /// </summary>
        private Neusoft.HISFC.Models.RADT.Patient patient = new Neusoft.HISFC.Models.RADT.Patient();

        /// <summary>
        /// 患者信息
        /// </summary>
        public Neusoft.HISFC.Models.RADT.Patient Patient
        {
            get
            {
                return patient;
            }
            set
            {
                this.ClearAll(true);
                patient = value;
            }
        }

        ///// <summary>
        ///// 患者病历号
        ///// </summary>
        //private string clinicNo = "";       

        ///// <summary>
        ///// 门诊Patient.ID号
        ///// </summary>
        //public string ClinicNo
        //{
        //    get
        //    {
        //        return clinicNo;
        //    }
        //    set
        //    {
        //        if (value != null && value != "")
        //        {
        //            clinicNo = value;
        //        }
        //    }
        //}

        /// <summary>
        /// 类型
        /// </summary>
        private Neusoft.HISFC.DCP.Enum.PatientType type = Neusoft.HISFC.DCP.Enum.PatientType.O;

        /// <summary>
        /// 类型[I住院 C门诊 O其他]
        /// </summary>
        public Neusoft.HISFC.DCP.Enum.PatientType PatientType
        {
            get
            {
                return type;
            }
            set
            {
                type = value;

                if (value == Neusoft.HISFC.DCP.Enum.PatientType.O)
                {
                    this.txtClinicNO.ReadOnly = true;
                }
                else
                {
                    this.txtClinicNO.ReadOnly = false;
                }
            }
        }

        /// <summary>
        /// 状态 患者列表为A 报告列表为B 选择了报告后更新为报告状态
        /// </summary>
        public enumTreeViewType treeViewType;

        /// <summary>
        /// 状态 患者列表为A 报告列表为B 选择了报告后更新为报告状态
        /// </summary>
        public enumTreeViewType TreeViewType
        {
            get
            {
                return treeViewType;
            }
            set
            {
                treeViewType = value;
                //this.ucReportRegister1.State = value;
            }
        }

        /// <summary>
        /// 传染病编号 根据诊断传入
        /// </summary>
        private bool isRenew = false;
        /// <summary>
        /// 是否订正 如果是订正是在copy原卡的基础上insert新卡
        /// 如果是修改在在原卡基础上update
        /// </summary>
        public bool IsRenew
        {
            get
            {
                return isRenew;
            }
            set
            {
                isRenew = value;
            }
        }

        /// <summary>
        /// 是否有高级权限，保健科都有
        /// </summary>
        private bool isAdvance = false;

        /// <summary>
        /// 是否有高级权限，保健科都有
        /// </summary>
        public bool IsAdvance
        {
            get
            {
                return isAdvance;
            }
            set
            {
                this.isAdvance = value;
            }
        }

        /// <summary>
        /// 是否需要附卡
        /// </summary>
        private bool isNeedAdd;

        /// <summary>
        /// 是否需要附卡
        /// </summary>
        public bool IsNeedAdd
        {
            get
            {
                return isNeedAdd;
            }
            set
            {
                isNeedAdd = value;
            }
        }

        /// <summary>
        /// 是否显示左边的查询树
        /// </summary>
        private bool isDisplayPanelLeft;

        /// <summary>
        /// 是否显示左边的查询树
        /// </summary>
        public bool IsDisplayPanelLeft
        {
            get
            {
                return isDisplayPanelLeft;
            }
            set
            {
                isDisplayPanelLeft = value;
            }
        }

        /// <summary>
        /// 是否需要附卡提示
        /// </summary>
        private bool isNeedAdditionMeg = true;

        /// <summary>
        /// 是否需要附卡提示
        /// </summary>
        public bool IsNeedAdditionMeg
        {
            get
            {
                return isNeedAdditionMeg;
            }
            set
            {
                isNeedAdditionMeg = value;
            }
        }

        private string infectCode = ""; 

        /// <summary>
        /// 指定疾病编码
        /// </summary>
        public string InfectCode
        {
            get { return infectCode; }
            set { infectCode = value; }
        }

        private Neusoft.HISFC.DCP.Enum.ReportOperResult reportOperResult = Neusoft.HISFC.DCP.Enum.ReportOperResult.Other;

        /// <summary>
        /// 报卡错作结果
        /// </summary>
        public Neusoft.HISFC.DCP.Enum.ReportOperResult ReportOperResult
        {
            get
            {
                return this.reportOperResult;
            }
            set
            {
                this.reportOperResult = value;
            }
        }
        #endregion

        #region 枚举

        /// <summary>
        /// 状态
        /// </summary>
        public enum enumTreeViewType
        {
            PatientInfo,
            ReportInfo,
            FeedBackInfo
        }

        /// <summary>
        /// 
        /// </summary>
        public enum enumResultType
        {
            lisResult,
            feedBack,
            other
        }

        /// <summary>
        /// 操作的类型
        /// </summary>
        public enum OperType
        {
            新增,
            保存,
            合格,
            不合格,
            订正,
            作废,
            恢复,
            删除,
            查询
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ucDiseaseReportRegister()
            : this(Neusoft.HISFC.DCP.Enum.PatientType.O)
        {
        }

        /// <summary>
        /// 重载构造函数
        /// </summary>
        /// <param name="patientType"></param>
        public ucDiseaseReportRegister(Neusoft.HISFC.DCP.Enum.PatientType parmPatientType)
        {
            InitializeComponent();

            this.tvPatientInfo.ImageList = this.tvPatientInfo.groupImageList;

            this.PatientType = parmPatientType;
            this.Load += new EventHandler(ucInfectionReport_Load);
        }

        #endregion

        #region 初始化

        /// <summary>
        /// 初始化加载界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucInfectionReport_Load(object sender, System.EventArgs e)
        {
            this.Init();

            this.InitEvent();

            this.InitQueryContent();

             //根据uc设置的参数初始化,只好放在load函数中          
            this.SetOperType( OperType.新增);

            this.ShowInfo();

            this.InitPrivInformation();

            //this.InitQueryContent();

            this.TreeViewAddReportsIgnorState(this.alReport);
  
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在初始化数据,请稍候...");
            Application.DoEvents();
            try
            {
                this.tvPatientInfo.Nodes.Clear();

                this.User = Neusoft.FrameWork.Management.Connection.Operator as Neusoft.HISFC.Models.Base.Employee;
                //下拉框
                this.Initcmb();

                //时间
                this.Initdtp();

                this.cmbDoctorDept.Tag = this.User.Dept.ID;//报卡科室
                this.cmbReportDoctor.Tag = this.User.ID;//报卡医生
                this.cmbReportDoctor.Enabled = true;
                

            }
            catch (Exception ex)
            {
                this.MyMessageBox("初始化失败！" + ex.Message, "err");
            }
            finally
            {
                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
        }

     
        #region 下拉框

        /// <summary>
        /// 初始化下拉框
        /// </summary>
        private void Initcmb()
        {
            //性别
            ArrayList alSex = Neusoft.HISFC.Models.Base.SexEnumService.List();
            this.cmbSex.AddItems(alSex);
            //报告科室
            ArrayList alDept = commonProcess.QueryDeptAllValidAndUnvalid();
            this.cmbDoctorDept.AddItems(alDept);
            //报告医生
            ArrayList alDoctor = this.commonProcess.QueryEmployeeAllValidAndUnvalid();
            this.cmbReportDoctor.AddItems(alDoctor);
            
            //疾病类型
            InitInfections();
            //病例分类
            InitCaseClass();
            //职业
            InitProfession();
        }

        /// <summary>
        /// 初始化疾病
        /// </summary>
        private void InitInfections()
        {

            //传染病的类型
            ArrayList alInfectClass = new ArrayList();

            alInfectClass.AddRange(commonProcess.QueryConstantList("INFECTCLASS"));
            if (alInfectClass.Count < 1)
            {
                return;
            }

            //需要附卡的传染病
            this.hshNeedAdd = new Hashtable();
            //类型
            this.hshInfectClass = new Hashtable();
            //需要报性病卡的疾病
            this.hshNeedSexReport = new Hashtable();
            //需要采血送检的疾病

            this.hshNeedCheckedBlood = new Hashtable();
            //需要二级病例的疾病
            this.hshNeedCaseTwo = new Hashtable();
            //需要电话报告的疾病
            this.hshNeedTelInfect = new Hashtable();
            //需要填写结核病转诊单的疾病
            this.hshNeedBill = new Hashtable();
            //新生儿破伤风
            this.hshLitteChild = new Hashtable();
            //需要二级名称的性病
            this.hshSexNeedGradeTwo = new Hashtable();
            //需要人群分类描述
            this.hshPatientTyepNeedDesc = new Hashtable();
            //需要备注
            this.hshNeedMemo = new Hashtable();

            //根据类型获取传染病

            int index = 1;
            foreach (Neusoft.HISFC.Models.Base.Const infectclass in alInfectClass)
            {
                ArrayList al = new ArrayList();
                ArrayList alItem = new ArrayList();


                infectclass.Name = "--" + infectclass.Name + "--";
                infectclass.Name = infectclass.Name.PadLeft(13, ' ');
                al.Add(infectclass);
                if (index == 1)
                {
                    Neusoft.HISFC.Models.Base.Const o = new Neusoft.HISFC.Models.Base.Const();
                    o.ID = "####";
                    o.Name = "--请选择--";
                    al.Insert(0, o);
                    index++;
                }
                alItem = commonProcess.QueryConstantList(infectclass.ID);

                al.AddRange(alItem);
                alInfectItem.AddRange(alItem);

                hshInfectClass.Add(infectclass.ID, null);
                foreach (Neusoft.HISFC.Models.Base.Const infect in al)
                {
                    //名称过长，维护在备注里，在此交换
                    if (infect.Name.IndexOf("备注", 0) != -1)
                    {
                        infect.Name = infect.Memo;
                        infect.Memo = "";
                    }
                    if (infect.Memo.IndexOf("需附卡", 0) != -1)
                    {
                        hshNeedAdd.Add(infect.ID, null);
                    }
                    if (infect.Memo.IndexOf("需性病报告", 0) != -1)
                    {
                        hshNeedSexReport.Add(infect.ID, null);
                    }
                    if (infect.Memo.IndexOf("需备注") != -1)
                    {
                        hshNeedMemo.Add(infect.ID, null);
                    }
                    //性病二级名称
                    if (infect.Memo.IndexOf("二级名称", 0) != -1)
                    {
                        hshSexNeedGradeTwo.Add(infect.ID, null);
                    }
                    if (infect.Memo.IndexOf("需采血送检", 0) != -1)
                    {
                        hshNeedCheckedBlood.Add(infect.ID, null);
                    }
                    //二级病例分类
                    if (infect.Memo.IndexOf("病例分类", 0) != -1)
                    {
                        hshNeedCaseTwo.Add(infect.ID, null);
                    }
                    //电话通知
                    if (infect.Memo.IndexOf("电话通知", 0) != -1)
                    {
                        hshNeedTelInfect.Add(infect.ID, null);
                    }
                    //结核病转诊单
                    if (infect.Memo.IndexOf("需转诊单", 0) != -1)
                    {
                        hshNeedBill.Add(infect.ID, null);
                    }
                    if (infect.Memo.IndexOf("新生儿破伤风", 0) != -1 || infect.Name.IndexOf("新生儿破伤风", 0) != -1)
                    {
                        hshLitteChild.Add(infect.ID, null);
                    }

                }
                alinfection.AddRange(al);
                Neusoft.FrameWork.Models.NeuObject ob = new Neusoft.FrameWork.Models.NeuObject();
                ob.ID = "####";
                ob.Name = "    ";
                alinfection.Add(ob);
            }
            this.cmbInfectionClass.AddItems(alinfection);
            this.cmbInfectionClass.DataSource = alinfection;
            this.cmbInfectionClass.DisplayMember = "Name";
            this.cmbInfectionClass.ValueMember = "ID";             
        }

        /// <summary>
        /// 病例分类
        /// </summary>
        private void InitCaseClass()
        {
            Neusoft.HISFC.Models.Base.Const obj = new Neusoft.HISFC.Models.Base.Const();
            obj.ID = "####";
            obj.Name = "--请选择--";
            ArrayList al = new ArrayList();
            al.Add(obj);
            al.AddRange(commonProcess.QueryConstantList("CASECLASS"));
            this.cmbCaseClassOne.AddItems(al);
            this.cmbCaseClassOne.DataSource = al;
            this.cmbCaseClassOne.ValueMember = "ID";
            this.cmbCaseClassOne.DisplayMember = "Name";

            ArrayList altwo = new ArrayList();
            Neusoft.HISFC.Models.Base.Const obone = new Neusoft.HISFC.Models.Base.Const();

            //altwo.Add(obj);
            Neusoft.HISFC.Models.Base.Const obthree = new Neusoft.HISFC.Models.Base.Const();
            obthree.ID = "2";
            obthree.Name = "未分型";
            altwo.Add(obthree);

            obone.ID = "0";
            obone.Name = "急性";
            altwo.Add(obone);

            Neusoft.HISFC.Models.Base.Const obtwo = new Neusoft.HISFC.Models.Base.Const();
            obtwo.ID = "1";
            obtwo.Name = "慢性";
            altwo.Add(obtwo);

            this.cmbCaseClaseTwo.DataSource = altwo;
            this.cmbCaseClaseTwo.ValueMember = "ID";
            this.cmbCaseClaseTwo.DisplayMember = "Name";
        }

        /// <summary>
        /// 初始化职业
        /// </summary>
        private void InitProfession()
        {
            Neusoft.HISFC.Models.Base.Const obj = new Neusoft.HISFC.Models.Base.Const();
            ArrayList alpro = new ArrayList();
            obj.ID = "####";
            obj.Name = "--请选择--";
            //职业
            alpro.Add(obj);
            alpro.AddRange(commonProcess.QueryConstantList(Neusoft.HISFC.Models.Base.EnumConstant.PROFESSION));
            this.hshStudent = new Hashtable();

            foreach (Neusoft.HISFC.Models.Base.Const con in alpro)
            {
                if (con.Name.IndexOf("幼托", 0) != -1 || con.Memo.IndexOf("儿童", 0) != -1 || con.Name.IndexOf("儿童", 0) != -1)
                {
                    hshStudent.Add(con.ID, "托幼机构及班级名称");
                }
                if (con.Name.IndexOf("学生", 0) != -1 || con.Memo.IndexOf("学生", 0) != -1)
                {
                    hshStudent.Add(con.ID, "学校及班级名称");
                }
            }
            this.cmbProfession.AddItems(alpro);
            this.cmbProfession.DataSource = alpro;
            this.cmbProfession.ValueMember = "ID";
            this.cmbProfession.DisplayMember = "Name";
            this.cmbProfession.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        /// <summary>
        /// 查询内容初始化
        /// </summary>
        private void InitQueryContent()
        {
            //初始化查询内容
            ArrayList al = new ArrayList();
            Neusoft.FrameWork.Models.NeuObject obj = new Neusoft.FrameWork.Models.NeuObject();
            if (this.PatientType == Neusoft.HISFC.DCP.Enum.PatientType.O)
            {
                obj.ID = "ReportInfo";
                obj.Name = "全院报卡信息查询";
                al.Add(obj);
            }

            obj = new Neusoft.FrameWork.Models.NeuObject();
            obj.ID = "PatientInfo";
            obj.Name = "患者查询";
            al.Add(obj);

            obj = new Neusoft.FrameWork.Models.NeuObject();
            obj.ID = "DeptReport";
            obj.Name = "本科室报卡信息查询";
            al.Add(obj);

            obj = new Neusoft.FrameWork.Models.NeuObject();
            obj.ID = "DeptUnReport";
            obj.Name = "本科室不合格报卡查询";
            al.Add(obj);

            this.cmbQueryContent.DataSource = al;
            this.cmbQueryContent.DisplayMember = "Name";
            this.cmbQueryContent.ValueMember = "ID";
            this.cmbQueryContent.SelectedIndex = 0;
            this.cmbQueryContent.DropDownStyle = ComboBoxStyle.DropDownList;

            //在此加载事件，避免第一次初始化事件时连续12次调用事件的脏读，严重影响第一次加载界面的性能！  --张雷（修改于 20:15 2010-12-26）
            //this.cmbQueryContent.SelectedValueChanged += new EventHandler(cmbQueryContent_SelectedValueChanged);
        }

        #endregion

        #region 时间

        /// <summary>
        /// 初始化时间
        /// </summary>
        private void Initdtp()
        {
            //生日
            this.dtBirthDay.Value = diseaseMgr.GetDateTimeFromSysDateTime();
            //诊断时间
            this.dtDiaDate.Value = diseaseMgr.GetDateTimeFromSysDateTime();
            //填表时间
            this.lbReportTime.Text = diseaseMgr.GetSysDateTime("yyyy年MM月dd日 HH:mm:ss");
            //发病 ， 死亡时间

            this.SetEnablellb(this.PatientType);

            //初始化查询时间
            System.DateTime dt = this.diseaseMgr.GetDateTimeFromSysDateTime();
            dt = dt.AddDays(1);
            this.dtEnd.Value = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
            this.dtBegin.Value = this.dtEnd.Value.AddDays(-30);
        }
        
        #endregion

        #region 事件委托

        /// <summary>
        /// 初始化事件委托
        /// </summary>
        private void InitEvent()
        {            
            this.tvPatientInfo.AfterSelect += new TreeViewEventHandler(tvPatientInfo_AfterSelect);
            this.cmbQueryContent.SelectedValueChanged += new EventHandler(cmbQueryContent_SelectedValueChanged);
            //this.cmbInfectionClass.Click += new EventHandler(cmbInfectionClass_Enter);
        }

        #endregion

        #endregion

        #region 工具栏

        /// <summary>
        /// 工具栏的初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override Neusoft.FrameWork.WinForms.Forms.ToolBarService OnInit (object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton( "新建", "", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.X新建, true, false, null );

            toolBarService.AddToolButton( "合格", "Eligible", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.Z执行, true, false, null );
            toolBarService.AddToolButton( "不合格", "UnEligible", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.Z注销, true, false, null );

            toolBarService.AddToolButton( "订正", "", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.X下一个, true, false, null );
            toolBarService.AddToolButton( "恢复", "", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.M默认, true, false, null );
            toolBarService.AddToolButton( "作废", "Cancel", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.Q取消, true, false, null );

            toolBarService.AddToolButton( "删除", "", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null );

            return toolBarService;
        }

        #region 工具栏按钮

        /// <summary>
        /// 工具栏按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked (object sender, ToolStripItemClickedEventArgs e)
        {
            this.ToolStrip_ItemClicked( e.ClickedItem.Text );
            base.ToolStrip_ItemClicked( sender, e );
        }

        /// <summary>
        ///  单击工具栏按钮是调用,写出函数,以便被外部调用
        /// </summary>
        /// <param name="clickedItemText"></param>
        public void ToolStrip_ItemClicked (string clickedItemText)
        {
            switch (clickedItemText)
            {
                case "新建":
                    if (this.SetOperType( OperType.保存 ) == 1)
                    {
                        //this.ClinicNo = "";
                        this.Patient = null;
                        this.TreeViewShowDeptPatient();
                        //this.ClearAll( true );
                    }
                    break;
                case "合格":
                    this.SetOperType( OperType.合格 );
                    break;
                case "不合格":
                    this.SetOperType( OperType.不合格 );
                    break;
                case "订正":
                    if (this.SetOperType( OperType.订正 ) == 1)
                    {
                        this.OnCorrect();
                    }
                    break;
                case "恢复":
                    this.SetOperType( OperType.恢复 );
                    break;
                case "作废":
                    this.SetOperType( OperType.作废 );
                    break;
                case "删除":
                    if (this.SetOperType( OperType.删除 ) == 1)
                    {
                        this.DeleteReport();
                    }
                    break;
                case "查询":
                    this.Query();
                    break;
                case "保存":
                    if (this.SetOperType( OperType.保存 ) == 1)
                    {
                        this.OnSave( new object(), new object() );
                    }
                    break;
                case "退出":
                    this.reportOperResult = Neusoft.HISFC.DCP.Enum.ReportOperResult.Cancel;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 订正方法
        /// </summary>
        public void OnCorrect ()
        {
            if (this.SaveCorrectReport() == 0)
            {
                this.ClearAll();

                if (this.AlReport != null && this.AlReport.Count > 0)
                {
                    this.ReflashTreeView(this.AlReport);
                }
                else
                {
                    this.QueryOldReport();
                }
                this.SetOperType(OperType.查询);

                // 下诊断后是否填写了报告卡，“保存成功”不可少
                this.Text += "--保存成功";
            }
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave (object sender, object neuObject)
        {
            if (this.lblState.Text == "1")
            {
                MessageBox.Show("已作废的报告卡不允许修改！");
                return -1;
            }

            if (this.SaveRepot() == 0)
            {
                this.ClearAll();
                if (this.AlReport != null && this.AlReport.Count > 0)
                {
                    this.ReflashTreeView(this.AlReport);
                }
                else
                {
                    this.QueryOldReport();
                }
                this.SetOperType(OperType.查询);
                // 下诊断后是否填写了报告卡，“保存成功”不可少
                this.Text += "--保存成功";
            }
            return 1;
        }

        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery (object sender, object neuObject)
        {
            this.Query();
            return 0;
        }

        /// <summary>
        /// 设置操作员的操作类型
        /// </summary>
        /// <param name="operType"></param>
        /// <returns>操作类型结果 1 成功 -1 失败</returns>
        private int SetOperType (OperType operType)
        {
            if (PreArrange() == -1)
            {
                return -1;
            }

            this.operType = operType;

            Neusoft.HISFC.DCP.Object.CommonReport report = this.GetSelectedReport();
            if (report == null || string.IsNullOrEmpty(report.ID)== true)
            {
                if (this.operType == OperType.保存)     //对于新建的单独处理
                {
                    return 1;
                }
                return -1;
            }
            if (IsAllowOper( report, operType ) == false)
            {
                return -1;
            }
            switch (this.operType)
            {
                case OperType.合格:
                    this.UpdateReportState( report, Neusoft.HISFC.DCP.Enum.ReportState.Eligible );
                    break;
                case OperType.不合格:
                    //2011-3-8 不合格的话输入退卡原因
                    if (string.IsNullOrEmpty(this.txtCase.Text))
                    {
                        MessageBox.Show("请填写不合格原因，再设置为不合格");
                        this.txtCase.Focus();
                        return -1;
                    }
                    report.OperCase = this.txtCase.Text;
                    this.UpdateReportState( report, Neusoft.HISFC.DCP.Enum.ReportState.UnEligible );
                    break;
                case OperType.作废:
                    if (this.User.ID == report.Oper.ID)
                    {
                        this.UpdateReportState( report, Neusoft.HISFC.DCP.Enum.ReportState.OwnCancel );
                    }
                    else
                    {
                        this.UpdateReportState( report, Neusoft.HISFC.DCP.Enum.ReportState.Cancel );
                    }
                    break;
                case OperType.恢复:
                    if (this.User.ID == report.Oper.ID)
                    {
                        this.UpdateReportState( report, Neusoft.HISFC.DCP.Enum.ReportState.New );
                    }
                    else
                    {
                        this.UpdateReportState( report, Neusoft.HISFC.DCP.Enum.ReportState.Cancel );
                    }
                    break;
            }

            return 1;
        }

        #endregion

        #endregion

        #region 权限相关操作

        /// <summary>
        /// 初始权限科室
        /// </summary>
        protected void InitPrivInformation ()
        {
            Neusoft.HISFC.DCP.BizProcess.Permission permissionProcess = new Neusoft.HISFC.DCP.BizProcess.Permission();
            this.cdcPrivDeptList = permissionProcess.QueryUserPriv( Neusoft.FrameWork.Management.Connection.Operator.ID, "8001" );

            if (this.cdcPrivDeptList == null)
            {
                MessageBox.Show( "获取CDC权限科室发生错误" + permissionProcess.Err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information );
                return;
            }
            /*
             * 医生可以进行的操作
             *  1、新建、查询
             *  2、保存、订正
             *  3、作废、删除
             *  4、修改本人建的卡
             * 
             * CDC权限运行进行的操作
             *  1、审核（合格、不合格）
             *  2、修改
             *  3、恢复
            */

            //疾控权限设置
            toolBarService.SetToolButtonEnabled( "合格", this.isCDCPriv );
            toolBarService.SetToolButtonEnabled( "不合格", this.isCDCPriv );
            this.cmbDoctorDept.Enabled = this.isCDCPriv;
        }

        /// <summary>
        /// 判断是否为CDC权限科室
        /// </summary>
        /// <param name="deptCode">科室编码</param>
        /// <returns>成功返回True 失败返回False</returns>
        protected bool IsCDCDept (string deptCode)
        {
            if (this.cdcPrivDeptList != null)
            {
                foreach (Neusoft.FrameWork.Models.NeuObject info in this.cdcPrivDeptList)
                {
                    if (info.ID == deptCode)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 操作权限的判断
        /// </summary>
        /// <param name="report">报告实体</param>
        /// <param name="operType">操作方式</param>
        /// <returns>true有操作权限 false无操作权限</returns>
        private bool IsAllowOper (Neusoft.HISFC.DCP.Object.CommonReport report, OperType operType)
        {
            if (report == null)
            {
                return true;
            }

            bool isAllow = false;

            switch (operType)
            {
                case OperType.保存:

                    #region Modify

                    switch (Int32.Parse( report.State ))
                    {
                        case (int)Neusoft.HISFC.DCP.Enum.ReportState.New:               //新建卡
                        case (int)Neusoft.HISFC.DCP.Enum.ReportState.UnEligible:        //不合格卡

                            if (this.User.ID == report.ReportDoctor.ID)                //填报人与当前操作员一致
                            {
                                isAllow = true;
                            }
                            else
                            {
                                if (this.IsCDCDept( this.User.Dept.ID ) == true)              //判断是否有CDC权限，如有权限运行修改
                                {
                                    isAllow = true;
                                }
                            }
                            if (isAllow == false)
                            {
                                MessageBox.Show("提示：不可修改他人填写的报告", "提示>>" ,MessageBoxButtons.OK,MessageBoxIcon.Information);
                            }
                            break;
                        case (int)Neusoft.HISFC.DCP.Enum.ReportState.Eligible:
                            MessageBox.Show( "提示：报告已经合格", "提示>>", MessageBoxButtons.OK, MessageBoxIcon.Information );
                            break;
                        case (int)Neusoft.HISFC.DCP.Enum.ReportState.OwnCancel:
                            MessageBox.Show( "提示：报告已经作废 不能再修改", "提示>>", MessageBoxButtons.OK, MessageBoxIcon.Information );
                            break;
                        case (int)Neusoft.HISFC.DCP.Enum.ReportState.Cancel:
                            MessageBox.Show( "提示：报告审核时已经作废 不能修改", "提示>>", MessageBoxButtons.OK, MessageBoxIcon.Information );
                            break;
                    }

                    #endregion

                    break;
                case OperType.作废:

                    #region Cancel

                    switch (Int32.Parse( report.State ))
                    {
                        case (int)Neusoft.HISFC.DCP.Enum.ReportState.New:
                        case (int)Neusoft.HISFC.DCP.Enum.ReportState.UnEligible:
                            if (this.User.ID == report.ReportDoctor.ID)
                            {
                                isAllow = true;
                            }
                            else
                            {
                                if (this.IsCDCDept( this.User.Dept.ID ) == true)              //判断是否有CDC权限，如有权限运行修改
                                {
                                    isAllow = true;
                                }
                            }
                            if (isAllow == false)
                            {
                                MessageBox.Show( this, "提示：不可修改他人填写的报告", "提示>>" );
                            }
                            else
                            {
                                if (MessageBox.Show( this, "确实要作废报告吗？", "提示>>", System.Windows.Forms.MessageBoxButtons.YesNo,
                                    System.Windows.Forms.MessageBoxIcon.Information, System.Windows.Forms.MessageBoxDefaultButton.Button2 )
                                    == System.Windows.Forms.DialogResult.No)
                                {
                                    isAllow = false;
                                }
                            }
                            break;
                        case (int)Neusoft.HISFC.DCP.Enum.ReportState.Eligible:
                            MessageBox.Show( this, "提示：报告已经合格 不能作废", "提示>>" );
                            break;
                        case (int)Neusoft.HISFC.DCP.Enum.ReportState.OwnCancel:
                            MessageBox.Show( this, "提示：报告已经被报告人作废", "提示>>" );
                            break;
                        case (int)Neusoft.HISFC.DCP.Enum.ReportState.Cancel:
                            MessageBox.Show( this, "提示：报告审核时已经作废", "提示>>" );
                            break;
                    }
                    #endregion

                    break;
                case OperType.删除:

                    #region 删除

                    if (Neusoft.FrameWork.Function.NConvert.ToInt32( report.State ) != (int)Neusoft.HISFC.DCP.Enum.ReportState.New)
                    {
                        MessageBox.Show( "非新建卡，不能进行删除操作", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information );
                    }
                    else
                    {
                        if (this.User.ID != report.ReportDoctor.ID)
                        {
                            MessageBox.Show( this, "提示：不可删除他人填写的报告", "提示>>" );
                        }
                        else
                        {
                            return true;
                        }
                    }

                    #endregion

                    break;

                case OperType.合格:

                    #region 合格

                    if (Int32.Parse( report.State ) == (int)Neusoft.HISFC.DCP.Enum.ReportState.New)
                    {
                        isAllow = true;
                    }
                    else if (Int32.Parse( report.State ) == (int)Neusoft.HISFC.DCP.Enum.ReportState.Eligible)
                    {
                        MessageBox.Show( this, "提示：报告已审核合格", "提示>>" );
                    }
                    else if (Int32.Parse( report.State ) == (int)Neusoft.HISFC.DCP.Enum.ReportState.UnEligible)
                    {
                        if (MessageBox.Show( this, "报告卡已审核，是否再审？", "提示>>", System.Windows.Forms.MessageBoxButtons.YesNo,
                            System.Windows.Forms.MessageBoxIcon.Information, System.Windows.Forms.MessageBoxDefaultButton.Button2 )
                            == System.Windows.Forms.DialogResult.Yes)
                            isAllow = true;
                    }
                    else if (Int32.Parse( report.State ) == (int)Neusoft.HISFC.DCP.Enum.ReportState.OwnCancel)
                    {
                        MessageBox.Show( this, "提示：报告已经被报人作废", "提示>>" );
                    }
                    else if (Int32.Parse( report.State ) == (int)Neusoft.HISFC.DCP.Enum.ReportState.Cancel)
                    {
                        MessageBox.Show( this, "提示：报告审核时已经作废", "提示>>" );
                    }
                    #endregion

                    break;
                case OperType.不合格:
                    #region
                    if (Int32.Parse( report.State ) == (int)Neusoft.HISFC.DCP.Enum.ReportState.New)
                    {
                        isAllow = true;
                    }
                    else if (Int32.Parse( report.State ) == (int)Neusoft.HISFC.DCP.Enum.ReportState.UnEligible)
                    {
                        MessageBox.Show( this, "提示：报告已审核不合格", "提示>>" );
                    }
                    else if (Int32.Parse( report.State ) == (int)Neusoft.HISFC.DCP.Enum.ReportState.Eligible)
                    {
                        if (MessageBox.Show( this, "报告卡已审核，是否再审？", "提示>>", System.Windows.Forms.MessageBoxButtons.YesNo,
                            System.Windows.Forms.MessageBoxIcon.Information, System.Windows.Forms.MessageBoxDefaultButton.Button2 )
                            == System.Windows.Forms.DialogResult.Yes)
                            isAllow = true;
                    }
                    else if (Int32.Parse( report.State ) == (int)Neusoft.HISFC.DCP.Enum.ReportState.OwnCancel)
                    {
                        MessageBox.Show( this, "提示：报告已经被报人作废", "提示>>" );
                    }
                    else if (Int32.Parse( report.State ) == (int)Neusoft.HISFC.DCP.Enum.ReportState.Cancel)
                    {
                        MessageBox.Show( this, "提示：报告审核时已经作废", "提示>>" );
                    }
                    break;
                    #endregion
                case OperType.恢复:
                    #region 恢复
                    if (this.IsCDCDept( this.User.Dept.ID ) == false)
                    {
                        MessageBox.Show( this, "提示：恢复报告卡请于疾病预防科联系", "提示>>" );
                    }
                    else
                    {
                        if (Int32.Parse( report.State ) != (int)Neusoft.HISFC.DCP.Enum.ReportState.OwnCancel && Int32.Parse( report.State ) != (int)Neusoft.HISFC.DCP.Enum.ReportState.Cancel)
                        {
                            MessageBox.Show( this, "提示：没有作废的报告不允许恢复", "提示>>" );
                            break;
                        }
                        isAllow = true;
                        if (MessageBox.Show( this, "确实要恢复吗？", "提示>>", System.Windows.Forms.MessageBoxButtons.YesNo,
                            System.Windows.Forms.MessageBoxIcon.Information, System.Windows.Forms.MessageBoxDefaultButton.Button2 )
                            == System.Windows.Forms.DialogResult.No)
                        {
                            isAllow = false;
                        }
                    }
                    break;
                    #endregion
                case OperType.订正:
                    if (this.User.ID == report.ReportDoctor.ID)
                    {
                        isAllow = true;

                        string state = this.diseaseMgr.GetCommonReportByID( report.ID ).State;
                        if (state == ((int)Neusoft.HISFC.DCP.Enum.ReportState.Eligible).ToString())
                        {
                            MessageBox.Show( this, "报告卡已经审核通过，不能进行订正操作", "警告>>" );
                            isAllow = false;
                        }
                        else if (state == ((int)Neusoft.HISFC.DCP.Enum.ReportState.Cancel).ToString() || state == ((int)Neusoft.HISFC.DCP.Enum.ReportState.OwnCancel).ToString())
                        {
                            MessageBox.Show( this, "报告卡已经作废，不能进行订正操作", "警告>>" );
                            isAllow = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show( this, "提示：不可对他人填写的报告进行订正操作", "提示>>" );
                    }
                    break;

            }
            return isAllow;
        }
        #endregion       

        #region 显示信息

        /// <summary>
        /// 显示信息
        /// </summary>
        public void ShowInfo()
        {
            if (this.PatientType != Neusoft.HISFC.DCP.Enum.PatientType.O)
            {
                if (this.Patient != null)
                {
                    this.ShowPatienInfo(this.Patient);

                    ////显示患者信息
                    //if (this.Type == Neusoft.HISFC.DCP.Enum.PatientType.C)              //门诊
                    //{
                    //    //this.ClinicNo = this.Patient.ID;
                    //}
                    //else if (this.Type == Neusoft.HISFC.DCP.Enum.PatientType.I)         //住院
                    //{
                    //    this.InpatientNO = this.Patient.ID;
                    //}

                    this.TreeViewShowDeptPatient();
                }
            }
            else
            {
                //显示查询树信息
            }
        }

        #endregion

        #region 树显示内容

        #region 保存后分状态显示报卡信息

        /// <summary>
        /// 查询全院传染病报卡
        /// </summary>
        private void QueryHospitalReport()
        {
            ArrayList al = new ArrayList();
            if (this.PatientType == Neusoft.HISFC.DCP.Enum.PatientType.O)
            {
                al = this.diseaseMgr.GetCommonReportListByReportTime(this.dtBegin.Value, this.dtEnd.Value);
            }
            if (al.Count > 0)
            {
                //显示报告
                this.TreeViewAddReports( al );
            }
        }

        /// <summary>
        ///  显示报告卡[同状态的报告]
        /// </summary>
        /// <param name="al">同状态的报告</param>
        private void TreeViewAddReports(ArrayList al)
        {
            if (al.Count < 1 || al == null)
            {
                return;
            }

            this.tvPatientInfo.Nodes.Clear();

            CompareState compare = new CompareState();

            //al.Sort( compare );

            ArrayList alTempSortData = new ArrayList();
            string privState = string.Empty;

            foreach (Neusoft.HISFC.DCP.Object.CommonReport info in al)
            {
                if (info.State != privState)
                {
                    if (alTempSortData.Count > 0)
                    {
                        this.TreeViewAddReports( alTempSortData, (Neusoft.HISFC.DCP.Enum.ReportState)Neusoft.FrameWork.Function.NConvert.ToInt32( privState ) );
                    }
                 
                    alTempSortData = new ArrayList();

                    alTempSortData.Add( info );

                    privState = info.State;
                }
                else
                {
                    alTempSortData.Add( info );
                }
            }

            if (alTempSortData.Count > 0)
            {
                this.TreeViewAddReports( alTempSortData, (Neusoft.HISFC.DCP.Enum.ReportState)Neusoft.FrameWork.Function.NConvert.ToInt32( privState ) );
            }
        }

        /// <summary>
        /// 显示报告列表[按状态在列表中增加分级节点]
        /// </summary>
        private void TreeViewAddReports(ArrayList al, Neusoft.HISFC.DCP.Enum.ReportState reportState)
        {           
            try
            {
                this.TreeViewType = enumTreeViewType.ReportInfo;
                System.Windows.Forms.TreeNode node = new TreeNode();
                int imagindex = 4;

                //父节点名称 显示报告状态

                switch (reportState)
                {
                    case Neusoft.HISFC.DCP.Enum.ReportState.New:
                        node.Text = "新填";
                        break;
                    case Neusoft.HISFC.DCP.Enum.ReportState.Eligible:
                        node.Text = "合格";
                        imagindex = 4;
                        break;
                    case Neusoft.HISFC.DCP.Enum.ReportState.UnEligible://审核
                        node.Text = "不合格（请修改报卡）";
                        node.ForeColor = System.Drawing.Color.Red;
                        imagindex = 3;
                        break;
                    case Neusoft.HISFC.DCP.Enum.ReportState.OwnCancel:
                        node.Text = "报告作废";
                        node.ForeColor = System.Drawing.Color.Red;
                        imagindex = 3;
                        break;
                    case Neusoft.HISFC.DCP.Enum.ReportState.Cancel://作废
                        node.Text = "保健科作废";//保健科作废
                        imagindex = 3;
                        break;
                    default:
                        break;
                }

                node.Tag = "root";
                node.ImageIndex = 0;
                this.tvPatientInfo.Nodes.Add(node);
                //子节点加载 显示患者姓名 报告编号
                string msg = "";
                foreach (Neusoft.HISFC.DCP.Object.CommonReport report in al)
                {
                    System.Windows.Forms.TreeNode reportnode = new TreeNode();
                    reportnode.Tag = report;
                    if (report.Patient.Name == null || report.Patient.Name == "")
                    {
                        reportnode.Text = report.PatientParents + "[" + report.ReportNO + "]" + report.ExtendInfo3;
                    }
                    else
                    {
                        reportnode.Text = report.Patient.Name + "[" + report.ReportNO + "]" + report.ExtendInfo3; ;
                    }
                    reportnode.ImageIndex = imagindex;
                    reportnode.SelectedImageIndex = 2;
                    this.tvPatientInfo.Nodes[this.tvPatientInfo.Nodes.Count - 1].Nodes.Add(reportnode);

                    
                }
                if (msg != "")
                {
                    MessageBox.Show(this, "您填写的" + msg + "报告卡不合格，请查看[退卡原因]栏进行相应修改", "退卡原因：");
                }
                this.tvPatientInfo.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "加载历史报告信息失败" + ex.Message, "错误>>");
            }
        }

        /// <summary>
        /// 按列表刷新
        /// </summary>
        /// <param name="alAllState">列表中的所有报告</param>
        private void ReflashTreeView(ArrayList alAllState)
        {
            //保健科审核时刷新报告列表 郁闷的算法

            //窗口初始化化前报告卡属性已经赋值，但经操作后状态改变。所以按状态重新分类显示

            try
            {
                ArrayList alNew = new ArrayList();//新加
                ArrayList alGood = new ArrayList();//合格
                ArrayList alBad = new ArrayList();//不合格
                ArrayList alCancel = new ArrayList();//报告人作废

                //ArrayList alfive = new ArrayList();//保健科作废


                foreach (ArrayList alonestate in alAllState)
                {
                    foreach (Neusoft.HISFC.DCP.Object.CommonReport report in alonestate)
                    {
                        Neusoft.HISFC.DCP.Object.CommonReport tempreport = new Neusoft.HISFC.DCP.Object.CommonReport();
                        tempreport = this.diseaseMgr.GetCommonReportByID(report.ID);
                        switch (Int32.Parse(tempreport.State))
                        {
                            case (int)Neusoft.HISFC.DCP.Enum.ReportState.New:
                                alNew.Add(tempreport);
                                break;
                            case (int)Neusoft.HISFC.DCP.Enum.ReportState.Eligible:
                                alGood.Add(tempreport);
                                break;
                            case (int)Neusoft.HISFC.DCP.Enum.ReportState.UnEligible:
                                alBad.Add(tempreport);
                                break;
                            case (int)Neusoft.HISFC.DCP.Enum.ReportState.OwnCancel:
                            case (int)Neusoft.HISFC.DCP.Enum.ReportState.Cancel:
                                alCancel.Add(tempreport);
                                break;
                        }
                    }
                }
                this.tvPatientInfo.Nodes.Clear();
                this.TreeViewAddReports(alNew,Neusoft.HISFC.DCP.Enum.ReportState.New);
                this.TreeViewAddReports(alGood,Neusoft.HISFC.DCP.Enum.ReportState.Eligible);
                this.TreeViewAddReports(alBad,Neusoft.HISFC.DCP.Enum.ReportState.UnEligible);
                this.TreeViewAddReports(alCancel,Neusoft.HISFC.DCP.Enum.ReportState.Cancel);

            }
            catch (Exception ex)
            {
                MessageBox.Show("刷新列表出错>>" + ex.Message);
            }
        }

        #endregion

        #region 初始化后显示患者信息

        /// <summary>
        /// 树节点显示科室患者
        /// </summary>
        private void TreeViewShowDeptPatient()
        {
            //在新添加报告卡时加载门诊患者或者病区患者
            try
            {
                if (this.type == Neusoft.HISFC.DCP.Enum.PatientType.C)//门诊
                {
                    #region 门诊处理

                    ArrayList alpatient = new ArrayList();
                    //if (this.ClinicNo != "")
                    if (this.Patient!=null&&string.IsNullOrEmpty(this.Patient.ID)==false)
                    {
                        //在门诊医生站下诊断时会传入看诊号，此时只显示一个病人
                        Neusoft.HISFC.Models.Registration.Register r = new Neusoft.HISFC.Models.Registration.Register();

                        r = this.patientProcess.GetByClinic(this.Patient.ID);
                        if (r != null && string.IsNullOrEmpty(r.ID) == false)
                        {
                            alpatient.Add(r);
                        }
                        else
                        {
                            alpatient.Add(this.Patient);
                        }
                    }
                    else
                    {
                        //显示该Dr所有不合格的报告
                        this.tvPatientInfo.Nodes.Clear();
                        ArrayList al = new ArrayList();
                        al = this.diseaseMgr.GetReportListByStateAndDoctor(Function.ConvertState(Neusoft.HISFC.DCP.Enum.ReportState.UnEligible), this.User.ID);
                        if (al != null && al.Count > 0)
                        {
                            this.TreeViewAddReports(al);
                        }
                        return;
                    }
                    this.TreeViewAddClincPatients(alpatient);
                    try
                    {
                        if (this.tvPatientInfo.Nodes.Count>0)
                        {
                            this.tvPatientInfo.SelectedNode = this.tvPatientInfo.Nodes[0].FirstNode;
                        }
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show(e.Message);
                        return;
                    }

                    #endregion
                }
                else if (this.type == Neusoft.HISFC.DCP.Enum.PatientType.I)//住院
                {
                    if (this.Patient != null && string.IsNullOrEmpty(this.Patient.ID) == false)
                    {
                        ArrayList alpatient = new ArrayList();
                        Neusoft.HISFC.Models.RADT.PatientInfo patientTemp = this.patientProcess.GetPatientInfomation( this.Patient.ID );
                        if (patientTemp == null)
                        {
                            MessageBox.Show( "根据住院号加载患者信息发生错误" + this.patientProcess.ErrorMsg );
                            return;
                        }

                        Neusoft.HISFC.DCP.Object.CommonReport newInpatientReport = new Neusoft.HISFC.DCP.Object.CommonReport();

                        newInpatientReport.Patient = patientTemp;
                        newInpatientReport.Patient.PID.CardNO = newInpatientReport.Patient.PID.PatientNO;

                        newInpatientReport.ReportDoctor = this.User;
                        newInpatientReport.DoctorDept = this.User.Dept;

                        ArrayList alTemp = new ArrayList();

                        alTemp.Add( newInpatientReport );

                        this.TreeViewAddReports( alTemp );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取患者信息失败>>" + ex.Message);
            }
        }

        /// <summary>
        /// 显示门诊患者列表
        /// </summary>
        /// <param name="al"></param>
        private void TreeViewAddClincPatients(ArrayList al)
        {
            this.TreeViewAddClincPatients(al, false, false);
        }

        /// <summary>
        /// 显示门诊患者列表
        /// </summary>
        /// <param name="al"></param>
        /// <param name="isDeptLimited">是否科室受限制</param>
        /// <param name="isTimeLimited">是否是时间受限制</param>
        private void TreeViewAddClincPatients(ArrayList al, bool isDeptLimited, bool isTimeLimited)
        {
            this.tvPatientInfo.Nodes.Clear();
            //添加根

            System.Windows.Forms.TreeNode node = new TreeNode();
            node.Text = "患者列表--姓名[就诊时间]挂号科室";
            node.Tag = "root";
            node.ImageIndex = 1;
            this.tvPatientInfo.Nodes.Add(node);

            //状态

            this.TreeViewType = enumTreeViewType.PatientInfo;

            //添加患者子结点
            foreach (Neusoft.HISFC.Models.Registration.Register reg in al)
            {
                if (isDeptLimited && reg.DoctorInfo.Templet.Dept.ID != this.User.Dept.ID)
                {
                    continue;
                }
                if (isTimeLimited && (reg.DoctorInfo.SeeDate.CompareTo(this.dtBegin.Value) < 0 || reg.DoctorInfo.SeeDate.CompareTo(this.dtEnd.Value) > 0))
                {
                    continue;
                }
                System.Windows.Forms.TreeNode patientnode = new TreeNode();
                Neusoft.HISFC.Models.RADT.Patient patient = patient = reg as Neusoft.HISFC.Models.RADT.Patient;
                patient.User01 = reg.DoctorInfo.Templet.Dept.ID;

                patientnode.Tag = patient;
                patientnode.Text = ((Neusoft.HISFC.Models.RADT.Patient)reg).Name + "[" + reg.DoctorInfo.SeeDate.ToShortDateString() + "]" + reg.DoctorInfo.Templet.Dept.Name;
                patientnode.ImageIndex = 4;
                patientnode.SelectedImageIndex = 2;
                this.tvPatientInfo.Nodes[0].Nodes.Add(patientnode);
            }
            this.tvPatientInfo.ExpandAll();
        }

        /// <summary>
        /// 显示住院患者列表
        /// </summary>
        /// <param name="al"></param>
        private void TreeViewAddInpatients(ArrayList al)
        {
            this.AddTreeViewInpatients(al, false, false);
        }

        /// <summary>
        /// 显示住院患者列表
        /// </summary>
        /// <param name="al"></param>
        /// <param name="isDeptLimited">是否科室受限制</param>
        /// <param name="isTimeLimited">是否是时间受限制</param>
        private void AddTreeViewInpatients(ArrayList al, bool isDeptLimited, bool isTimeLimited)
        {
            this.tvPatientInfo.Nodes.Clear();
            //添加根

            System.Windows.Forms.TreeNode node = new TreeNode();
            node.Text = "患者列表--姓名[入院日期]入院科室";
            node.Tag = "root";
            node.ImageIndex = 1;
            this.tvPatientInfo.Nodes.Add(node);

            //状态

            this.TreeViewType = enumTreeViewType.PatientInfo;
            foreach (Neusoft.HISFC.Models.RADT.PatientInfo info in al)
            {
                if (isDeptLimited && info.PVisit.PatientLocation.Dept.ID != this.User.Dept.ID)
                {
                    continue;
                }
                if (isTimeLimited && (info.PVisit.InTime.CompareTo(this.dtBegin.Value) < 0 || info.PVisit.InTime.CompareTo(this.dtEnd.Value) > 0))
                {
                    continue;
                }
                System.Windows.Forms.TreeNode patientnode = new TreeNode();

                info.User01 = info.PVisit.PatientLocation.Dept.ID;
                if (info.User01 == null || info.User01 == "")
                {
                    info.User01 = this.User.Dept.ID;
                }
                patientnode.Tag = info;
                string id = "";
                if (info.ID != null && info.ID.Length > 8)
                {
                    id = info.ID.Remove(0, 4);
                }
                patientnode.Text = info.Name + "[" + info.PVisit.InTime.ToShortDateString() + "]" + info.PVisit.PatientLocation.Dept.Name;
                patientnode.ImageIndex = 4;
                patientnode.SelectedImageIndex = 2;
                this.tvPatientInfo.Nodes[0].Nodes.Add(patientnode);
            }
            this.tvPatientInfo.ExpandAll();
        }

        #endregion

        #region 显示待报卡信息
        
        /// <summary>
        /// 待报卡信息
        /// </summary>
        /// <param name="al"></param>
        private void TreeViewAddFeedBack(ArrayList al)
        {
            System.Windows.Forms.TreeNode node = new TreeNode();
            this.treeViewType = enumTreeViewType.FeedBackInfo;
            int imagindex = 1;

            node.Text = "待报卡";
            node.Tag = "root";
            node.ImageIndex = 0;
            this.tvPatientInfo.Nodes.Add(node);
            this.tvPatientInfo.ExpandAll();

        }

        #endregion
        
        #endregion

        #region 触发事件

        #region 按病历号查询患者信息-txtClinicNO

        /// <summary>
        /// 根据输入的病历号获得患者信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtClinicNO_KeyDown (object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.dataShowList == null)
                {
                    this.dataShowList = new ListBox();
                    this.dataShowList.Dock = DockStyle.Fill;
                }

                this.txtClinicNO.Text = this.txtClinicNO.Text.Trim().PadLeft( 10, '0' );

                if (this.PatientType == Neusoft.HISFC.DCP.Enum.PatientType.I)          //住院患者
                {                 
                    ArrayList alAllPatient = this.patientProcess.GetPatientInfoByPatientNOAll( this.txtClinicNO.Text );
                    if (alAllPatient == null)
                    {
                        MessageBox.Show( "根据住院号获取住院患者信息发生错误" + this.patientProcess.ErrorMsg );
                        return;
                    }

                    foreach (Neusoft.HISFC.Models.RADT.PatientInfo info in alAllPatient)
                    {
                        if (info.PVisit.InState.ID.ToString() == "I")           //在院状态
                        {
                            ArrayList alTemp = new ArrayList();
                            alTemp.Add( info );

                            this.Patient = info;

                            this.ShowPatienInfo( info );
                            this.cbxDeadDate.Checked = true;

                            this.TreeViewAddInpatients( alTemp );
                            break;
                        }
                    }
                }
                else if (this.PatientType == Neusoft.HISFC.DCP.Enum.PatientType.C) //门诊患者
                {

                }
                else
                {
                    MessageBox.Show( "无此人的信息" );
                    this.ClearAll();
                    return;
                }
            }
        }

        #endregion

        #region 操作树-tvPatientInfo

        /// <summary>
        /// 操作树后发生-选择树节点后发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvPatientInfo_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //根节点tag为root
            if (this.tvPatientInfo.SelectedNode.Tag.ToString() == "root")
            {
                this.ClearAll(true);
                return;
            }

            //状态为A时加载的是患者列表 B时加载的是报告列表
            this.cmbProfession.SelectedValueChanged -= new System.EventHandler(cmbProfession_SelectedValueChanged);

            if (this.TreeViewType == enumTreeViewType.PatientInfo)
            {
                this.Patient = this.tvPatientInfo.SelectedNode.Tag as Neusoft.HISFC.Models.RADT.Patient;
                this.cbxDeadDate.Checked = true;

                ShowPatienInfo(this.Patient);
            }
            else if (this.treeViewType == enumTreeViewType.ReportInfo)
            {
                Neusoft.HISFC.DCP.Object.CommonReport report = this.tvPatientInfo.SelectedNode.Tag as Neusoft.HISFC.DCP.Object.CommonReport;
                //显示报告
                this.IsNeedAdditionMeg = false;
                this.ShowReportData(report);
                this.IsNeedAdditionMeg = true;
            }

            this.cmbProfession.SelectedValueChanged += new EventHandler(cmbProfession_SelectedValueChanged);
        }

        #endregion        

        #region 报告卡查询

        #region 查询类型改变-cmbQueryContent

        /// <summary>
        /// 查询类型发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbQueryContent_SelectedValueChanged(object sender, EventArgs e)
        {
            //this.ucReportRegister1.InfectCode = "";

            if (this.cmbQueryContent.SelectedValue.ToString() == "PatientInfo" )
            {
                //患者基本信息查询

                this.dtBegin.Enabled = false;
                this.dtEnd.Enabled = false;
                this.groupBox3.Enabled = true;
                this.tvPatientInfo.Nodes.Clear();
                this.txtReportNo.Enabled = false;
                this.txtDoctor.Enabled = false;
                this.txtName.Enabled = true;
                this.txtInPatienNo.Enabled = true;
                this.SetEnablellb(this.PatientType);
            }
            else if (this.cmbQueryContent.SelectedValue.ToString() == "ReportInfo")
            {
                //患者报卡信息查询

                this.dtBegin.Enabled = true;
                this.dtEnd.Enabled = true;
                this.groupBox3.Enabled = true;
                this.tvPatientInfo.Nodes.Clear();
                this.txtReportNo.Enabled = true;
                this.txtDoctor.Enabled = true;
                this.txtInPatienNo.Enabled = true;
                this.txtName.Enabled = true;
                this.llbPatienNO.Enabled = false;
            }
            else if (this.cmbQueryContent.SelectedValue.ToString() == "DeptReport"
                || this.cmbQueryContent.SelectedValue.ToString() == "DeptUnReport")
            {
                //科室报告信息查询
                this.dtBegin.Enabled = true;
                this.dtEnd.Enabled = true;
                this.groupBox3.Enabled = true;
                this.txtDoctor.Enabled = false;
                this.txtInPatienNo.Enabled = false;
                this.txtName.Enabled = false;
                this.txtReportNo.Enabled = false;
                this.tvPatientInfo.Nodes.Clear();
                //this.mySearchOldReport();
            }
            else if (this.cmbQueryContent.SelectedValue.ToString() == "choose")
            {
                this.groupBox3.Enabled = true;
                this.txtDoctor.Enabled = false;
                this.txtInPatienNo.Enabled = false;
                this.txtName.Enabled = false;
                this.txtReportNo.Enabled = false;
                this.dtBegin.Enabled = false;
                this.dtEnd.Enabled = false;
                this.tvPatientInfo.Nodes.Clear();
                //this.mySearchOldReport();
            }
            else if (this.cmbQueryContent.SelectedValue.ToString() == "deptLisResult")
            {
                this.groupBox3.Enabled = true;
                this.txtDoctor.Enabled = false;
                this.txtInPatienNo.Enabled = false;
                this.txtName.Enabled = false;
                this.txtReportNo.Enabled = false;
                this.dtBegin.Enabled = false;
                this.dtEnd.Enabled = false;
                this.tvPatientInfo.Nodes.Clear();
            }
        }

        #endregion

        #region 按虚拟编号查询报告卡-txtReportNo

        /// <summary>
        /// 根据输入的虚拟编号查询报告卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtReportNo_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                //报告卡虚拟编号查询

                //this.ucReportRegister1.InfectCode = "";

                this.QueryByReportNO();
            }
        }

        /// <summary>
        /// 报告卡虚拟编号查询
        /// </summary>
        private void QueryByReportNO()
        {
            //报告卡虚拟编号查询

            this.tvPatientInfo.Nodes.Clear();
            ArrayList al = new ArrayList();
            Neusoft.HISFC.DCP.Object.CommonReport report = new Neusoft.HISFC.DCP.Object.CommonReport();
            report = this.diseaseMgr.GetCommonReportByNO(this.txtReportNo.Text);
            if (report != null)
            {
                if (report.ReportTime > this.dtBegin.Value && report.ReportTime < this.dtEnd.Value)
                {
                    al.Add( report );
                }

                this.TreeViewAddReports( al );
            }
        }

        #endregion

        #region 按患者号查询-txtInPatienNo

        /// <summary>
        /// 患者号查询
        /// </summary>
        private void QueryByPatientNO()
        {
            //患者号查询
            ArrayList al = new ArrayList();
            if (this.txtInPatienNo.Text.Trim() == "")
            {
                return;
            }
            string patientno = this.txtInPatienNo.Text.Trim().PadLeft(10, '0');

            al = this.diseaseMgr.GetCommonReportListByPatientNO( patientno );
            if (al == null)
            {
                MessageBox.Show( "根据患者ID查询患者报告信息时发生错误" + this.diseaseMgr.Err );
                return;
            }
            else if (al.Count > 0)
            {
                this.TreeViewAddReports( al );
            }
            else             //无报卡信息 显示患者信息
            {
                #region 无报卡信息时显示患者信息

                //住院患者
                if (this.LlbPatientType == Neusoft.HISFC.DCP.Enum.PatientType.I)
                {
                    al = this.patientProcess.GetPatientInfoByPatientNOAll( patientno );
                }
                //门诊患者
                else if (this.LlbPatientType == Neusoft.HISFC.DCP.Enum.PatientType.C)
                {
                    al = this.patientProcess.Query( patientno, DateTime.Now.AddYears( -1000 ) );
                }

                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                if (al == null || al.Count == 0)
                {
                    MessageBox.Show( this, "没有患者信息！", "提示>>" );
                    this.tvPatientInfo.Nodes.Clear();
                    return;
                }
                //住院患者
                if (this.LlbPatientType == Neusoft.HISFC.DCP.Enum.PatientType.I)
                {
                    this.TreeViewAddInpatients( al );
                }
                //门诊患者
                else if (this.LlbPatientType == Neusoft.HISFC.DCP.Enum.PatientType.C)
                {
                    this.TreeViewAddClincPatients( al );
                }

                #endregion
            }            
        }

        #endregion

        #region 患者类型转换-llbPatientNO

        /// <summary>
        /// 患者类型转换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void llbPatienNO_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.LlbPatientType == Neusoft.HISFC.DCP.Enum.PatientType.I)
            {
                this.llbPatienNO.Text = "病 历 号";
                this.lbPatientName.Text = "门诊患者";
                this.LlbPatientType = Neusoft.HISFC.DCP.Enum.PatientType.C;
            }
            else
            {
                this.llbPatienNO.Text = "住 院 号";
                this.lbPatientName.Text = "住院患者";
                this.LlbPatientType = Neusoft.HISFC.DCP.Enum.PatientType.I;
            }
        }

        #endregion

        #region 按姓名模糊查询

        /// <summary>
        /// 患者姓名查询
        /// </summary>
        private void QueryByPatientName()
        {
            ArrayList al = new ArrayList();
            if (this.txtName.Text.Trim() == "")
            {
                return;
            }
            string patientName = this.txtName.Text.Trim();

            al = this.diseaseMgr.GetReportListByPatientName( patientName );
            if (al == null)
            {
                MessageBox.Show( "根据患者姓名检索报告卡信息时发生错误" + this.diseaseMgr.Err );
                return;
            }
            else if (al.Count > 0)
            {
                this.TreeViewAddReports( al );
            }
            else            //无报告卡时显示患者信息
            {
                //住院患者
                if (this.LlbPatientType == Neusoft.HISFC.DCP.Enum.PatientType.I)
                {
                    al = this.patientProcess.GetPatientInfoByPatientName( patientName );
                }
                //门诊患者
                else if (this.LlbPatientType == Neusoft.HISFC.DCP.Enum.PatientType.C)
                {
                    al = this.patientProcess.QueryValidPatientsByName( patientName );
                }

                if (al == null || al.Count == 0)
                {
                    MessageBox.Show( this, "没有患者信息！", "提示>>" );
                    this.tvPatientInfo.Nodes.Clear();
                    return;
                }
                if (this.LlbPatientType == Neusoft.HISFC.DCP.Enum.PatientType.I)
                {
                    this.TreeViewAddInpatients( al );
                }
                //门诊患者
                else if (this.LlbPatientType == Neusoft.HISFC.DCP.Enum.PatientType.C)
                {
                    TreeViewAddClincPatients( al );
                }
            }
        }
        /// <summary>
        /// 按照住院科室查找患者
        /// </summary>
        private void QueryPatientByDeptIN()
        {
            ArrayList al = new ArrayList();
            Neusoft.HISFC.Models.RADT.InStateEnumService instate = new Neusoft.HISFC.Models.RADT.InStateEnumService();
            instate.ID = "I";
            //住院患者
            if (this.LlbPatientType == Neusoft.HISFC.DCP.Enum.PatientType.I)
            {
                al = this.patientProcess.QueryPatientByDeptCode(((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).Dept.ID, instate);
            }

            if (al == null || al.Count == 0)
            {
                MessageBox.Show(this, "没有患者信息！", "提示>>");
                this.tvPatientInfo.Nodes.Clear();
                return;
            }

            if (this.LlbPatientType == Neusoft.HISFC.DCP.Enum.PatientType.I)
            {
                this.TreeViewAddInpatients(al);
            }
        }
        /// <summary>
        /// 按照医生查询门诊病人 --修改
        /// </summary>
        private void QueryPatientByDco()
        {
            ArrayList al = new ArrayList();
           
            if (this.LlbPatientType == Neusoft.HISFC.DCP.Enum.PatientType.C)
            {
                al = this.patientProcess.QueryBySeeDoc(((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).ID, Neusoft.FrameWork.Function.NConvert.ToDateTime(this.diseaseMgr.GetSysDateTime()).AddDays(-3), Neusoft.FrameWork.Function.NConvert.ToDateTime(this.diseaseMgr.GetSysDateTime()).Date.AddDays(1),true);
            }

            if (al == null || al.Count == 0)
            {
                MessageBox.Show(this, "没有患者信息！", "提示>>");
                this.tvPatientInfo.Nodes.Clear();
                return;
            }
            if (this.LlbPatientType == Neusoft.HISFC.DCP.Enum.PatientType.C)
            {
                #region {6949963F-0A35-405a-9A44-4D5CBB53FFA2} 刘旭 修改日期 2011-5-25
               
                //this.TreeViewAddInpatients(al);

                //门诊医生站 按患者查询的时候 应该显示门诊患者
                this.TreeViewAddClincPatients(al);

                #endregion
            }
        }

        #endregion

        #region 根据医生工号查询该医生已填写报告-txtDoctor

        /// <summary>
        /// 根据报告医生查询报告卡
        /// </summary>
        private void QueryByDoctorNO()
        {
            this.tvPatientInfo.Nodes.Clear();
            if (this.txtDoctor.Text.Trim() == "")
            {
                return;
            }
            ArrayList al = new ArrayList();
            foreach (Neusoft.HISFC.DCP.Enum.ReportState s in Enum.GetValues(typeof(Neusoft.HISFC.DCP.Enum.ReportState)))
            {
                al.AddRange(this.diseaseMgr.GetReportListByStateAndDoctor(Function.ConvertState(s), this.txtDoctor.Text.Trim().PadLeft(6, '0')));
            }

            this.TreeViewAddReports( al );
        }

        #endregion

        #endregion

        #region 报告卡填写控制

        #region 出生日期-dtBirthDay

        /// <summary>
        /// 选择出生日期时改变年龄
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtBirthDay_ValueChanged(object sender, System.EventArgs e)
        {
            string age = this.diseaseMgr.GetAge(this.dtBirthDay.Value, this.diseaseMgr.GetDateTimeFromSysDateTime()).Trim();
            try
            {
                int length = age.Length;
                //删除年龄单位
                this.txtAge.Text = age.Substring(0, length - 1);                
                if (age.Substring(length - 1) == "岁")
                {
                    this.rdbYear.Checked = true;
                }
                else if (age.Substring(length - 1) == "月")
                {
                    this.rdbMonth.Checked = true;
                }
                else
                {
                    this.rdbDay.Checked = true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg(ex.Message));
            }
        }

        #endregion

        #region 职业选择-cmbProfession

        /// <summary>
        /// 职业选择提示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbProfession_SelectedValueChanged(object sender, System.EventArgs e)
        {
            //托幼儿童、学生
            if (this.hshStudent.Contains(this.cmbProfession.SelectedValue.ToString()))
            {
                this.MyMessageBox("请注意在" + "\"" + "工作单位栏" + "\"" + "填写" + "\"" + this.hshStudent[this.cmbProfession.SelectedValue.ToString()].ToString() + "\"", "提示");
            }
        }

        #endregion              

        #region 归属选择

        private void cbxHomeAearOne_Click(object sender, System.EventArgs e)
        {
            if (this.cbxHomeAearOne.Checked)
                this.SetHomeArea(1);
        }

        private void cbxHomeAearTwo_Click(object sender, System.EventArgs e)
        {
            if (this.cbxHomeAearTwo.Checked)
                this.SetHomeArea(2);
        }

        private void cbxHomeAearThree_Click(object sender, System.EventArgs e)
        {
            if (this.cbxHomeAearThree.Checked)
                this.SetHomeArea(3);
        }

        private void cbxHomeAearFour_Click(object sender, System.EventArgs e)
        {
            if (this.cbxHomeAearFour.Checked)
                this.SetHomeArea(4);
        }

        private void cbxHomeAearFive_Click(object sender, System.EventArgs e)
        {
            if (this.cbxHomeAearFive.Checked)
                this.SetHomeArea(5);
        }

        private void cbxHomeAearSix_Click(object sender, System.EventArgs e)
        {
            if (this.cbxHomeAearSix.Checked)
                this.SetHomeArea(6);
        }

        #endregion

        #region 地址选择

        private void txtHomeAddress_Enter(object sender, System.EventArgs e)
        {
            //this.txtHomeAdd.Text = this.cmbprovince.Text + this.cmbCity.Text + this.cmbCouty.Text + this.cmbTown.Text;
        }

        #endregion

        #region 疾病选择

        #region 提示信息

        private void ShowMessageAfterSelect(string diseaseId)
        {
            string msg = "";
            if (diseaseId == "3001")
            {
                msg = @"需符合以下四条，并经本院专家组会诊不能诊断为其它疾病的的肺炎病例方可上报为“不明原因肺炎”！
不明原因肺炎定义：

①发热（腋下体温≥38℃）
②具有肺炎的影像学特征

③发病早期白细胞总数降低或正常，或淋巴细胞分类计数减少

④经规范抗菌药物治疗3~5天（参照中华医学会呼吸病学分会颁布的2006版“社区获得性肺炎诊断和治疗指南”，详见附件2），病情无明显改善或呈进行性加重

";

            }

            else if (diseaseId == "3003")
            {
                msg = @"符合以下急性弛缓性麻痹（AFP）病例定义，需进行报告。

AFP定义：所有15岁以下出现急性弛缓性麻痹症状的病例，和任何年龄临床诊断为脊灰的病例均作为急性弛缓性麻痹（AFP）病例。

AFP病例的诊断要点：急性起病、肌张力减弱、肌力下降、腱反射减弱或消失。

常见的AFP病例包括以下疾病：

（1）脊髓灰质炎；

（2）格林巴利综合征（感染性多发性神经根神经炎，GBS）；
（3）横贯性脊髓炎、脊髓炎、脑脊髓炎、急性神经根脊髓炎；
（4）多神经病（药物性多神经病，有毒物质引起的多神经病、原因不明性多神经病）；

（5）神经根炎；
（6）外伤性神经炎（包括臀肌药物注射后引发的神经炎）；
（7）单神经炎；
（8）神经丛炎；
（9）周期性麻痹（包括低钾性麻痹、高钾性麻痹、正常钾性麻痹）；

（10）肌病（包括全身型重症肌无力、中毒性、原因不明性肌病）；

（11）急性多发性肌炎；
（12）肉毒中毒；
（13）四肢瘫、截瘫和单瘫（原因不明）；

（14）短暂性肢体麻痹。

";
            }

            if (msg != "")
            {
                MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ShowMessageAfterSave(string diseaseId)
        {
            string msg = "";
            if (diseaseId == "3003")
            {
                msg = @"对所有AFP病例应采集双份大便标本用于病毒分离

⑴标本的采集要求是：在麻痹出现后14天内采集，两份标本采集时间至少间隔24小时；每份标本重量≥5克（约为成人的大拇指末节大小）。标本采集应填写《采样送检单》。

⑵采样器及《采样送检单》可派人到疾病预防科领取。

⑶标本送检地点：标本采集后可通知越秀区疾病预防控制中心（）派人前来领取。

";
            }
            if (msg != "")
            {
                MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        /// <summary>
        /// 选择疾病
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbInfectionClass_Enter(object sender, EventArgs e)
        {
            if (this.infectCode == null || this.infectCode == "")
            {
                this.cmbInfectionClass.Enabled = true;
                return;
            }
            string[] infectCodes = this.infectCode.Split(',');
            if (infectCodes.Length > 1)
            {
                ArrayList alTmp = new ArrayList();
                this.cmbInfectionClass.Enabled = true;
                foreach (string code in infectCodes)
                {
                    foreach (Neusoft.HISFC.Models.Base.Const disease in this.alInfectItem)
                    {
                        if (code == disease.ID)
                        {
                            alTmp.Add(disease);
                            break;
                        }
                    }
                }
                Neusoft.FrameWork.Models.NeuObject ob = new Neusoft.HISFC.Models.Base.Const();
                Neusoft.FrameWork.WinForms.Classes.Function.ChooseItem(alTmp, ref ob);
                this.cmbInfectionClass.SelectedValue = ob.ID;

                //二级分类
                this.cmbCaseClaseTwo.Enabled = this.hshNeedCaseTwo.Contains(ob.ID);
                this.cmbInfectionClass.Enabled = true;
            }
        }

        /// <summary>
        /// 选择疾病，判断是否需要附卡，是否需要填写疾病名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbInfectionClass_SelectedValueChanged(object sender, System.EventArgs e)
        {   
            //xiwx
            if (this.cmbInfectionClass.Tag == null || this.cmbInfectionClass.Tag.ToString() == "####")
            {
                return;
            }
            
            string strtempid  = this.cmbInfectionClass.Tag.ToString();
            
            if (this.hshNeedMemo.Contains(strtempid))
            {
                this.MyMessageBox("请在备注中填写疾病名称", "提示>>");
            }
            
            //判断是否需要附卡
            if (this.tcReport.TabPages.ContainsKey("tpAddition"))
            {
                //先删除并且将标志置为FALSE
                this.tcReport.TabPages.RemoveByKey("tpAddition");
                this.IsNeedAdd = false;
            }
            this.IsNeedAddition(strtempid);

            this.ShowMessageAfterSelect(strtempid);

            //二级分类
            this.cmbCaseClaseTwo.Enabled = this.hshNeedCaseTwo.Contains(strtempid);
            this.cmbCaseClaseTwo.TabStop = this.cmbCaseClaseTwo.Enabled;
        }

        #endregion

        #region 回车跳转

        /// <summary>
        /// 回车跳转
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                System.Windows.Forms.SendKeys.Send("{tab}");
                return base.ProcessCmdKey(ref msg, keyData);
            }
            return false;
        }
    
        #endregion

        #region 附卡切换-tcReport

        ///// <summary>
        ///// 选择选项卡时，不允许在附卡信息不完整的情况下切换Tab页
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void tcReport_Selecting(object sender, TabControlCancelEventArgs e)
        //{
        //    if (this.tcReport.TabPages.ContainsKey("tpAddition") && this.tcReport.TabPages["tpAddition"].Focus())
        //    {
        //        if (this.tcReport.SelectedIndex != 1)
        //        {
        //            if (this.JudgeAdditionInfo() == -1)
        //            {
        //                return;
        //            }
        //        }
        //    }
        //}

        #endregion

        #endregion

        #endregion

        #region 方法

        /// <summary>
        /// 精确查询患者信息
        /// </summary>
        /// <param name="patientNO"></param>
        /// <returns></returns>
        private Neusoft.HISFC.Models.RADT.Patient GetPatientInfo(string patientNO)
        {
            Neusoft.HISFC.BizProcess.Integrate.RADT radtItg = new Neusoft.HISFC.BizProcess.Integrate.RADT();

            return radtItg.QueryComPatientInfo(patientNO);
        }

        /// <summary>
        /// 查找历史报告[科室新增 删除 作废报告后或按下历史按钮后显示已填写报告列表]
        /// </summary>
        private void QueryOldReport()
        {
            if (this.PatientType == Neusoft.HISFC.DCP.Enum.PatientType.O)//疾病传染科
            {
                this.QueryHospitalReport();
            }
            else
            {
                this.QueryDeptReport();
            }
        }
 
        /// <summary>
        /// 设置报卡信息的状态
        /// </summary>
        /// <param name="report"></param>
        /// <param name="reportState"></param>
        private void UpdateReportState(Neusoft.HISFC.DCP.Object.CommonReport report, Neusoft.HISFC.DCP.Enum.ReportState reportState)
        {
            System.DateTime now = this.diseaseMgr.GetDateTimeFromSysDateTime();

            //操作信息
            report.Oper.ID = this.User.ID;
            report.OperDept.ID = this.User.Dept.ID;
            report.OperTime = now;// 

            //状态变化后返回 在更改期间有其他人操作

            if (!this.CheckState(report.ID, report.State))
            {
                return;
            }
            //新的状态

            report.State = Function.ConvertState(reportState);

            //更新数据库;

            if (this.diseaseMgr.UpdateCommonReport(report) != -1)
            {
                this.MyMessageBox("操作成功！", "提示>>");
                this.ClearAll();
            }
            else
            {
                this.MyMessageBox("操作失败！" + this.diseaseMgr.Err, "err");
            }

            #region

            //非审核时的刷新
            this.QueryOldReport();

            #endregion
        }

        /// <summary>
        /// 选择显示报告的日期范围
        /// </summary>
        public void ChanageTime()
        {
            //选择时间段，如果没有选择就返回
            DateTime dtbegin = this.diseaseMgr.GetDateTimeFromSysDateTime().Date;
            DateTime dtend = dtbegin;
            if (Neusoft.FrameWork.WinForms.Classes.Function.ChooseDate(ref dtbegin, ref dtend) == 0) return;
            this.BeginTime = dtbegin.ToString();
            this.EndTime = dtend.ToString();
        }   

        /// <summary>
        /// 科室反馈信息
        /// </summary>
        //private void QueryFeedBackByDept()
        //{
        //    ArrayList al = new ArrayList();//this.diseaseMgr.GetFeedBackByDoctAndDept(this.User.ID,this.User.Dept.ID);
        //    if (al.Count == 0)
        //    {
        //        MessageBox.Show(this, "没有科室反馈信息", "提示>>");
        //        return;
        //    }
        //    Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在加载本科室反馈信息");
        //    Application.DoEvents();
        //    this.TreeViewAddFeedBack(al);
        //    Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
        //}
       
        /// <summary>
        /// 提示函数
        /// </summary>
        /// <param name="information"></param>
        /// <returns></returns>
        private bool IsContinue(string information)
        {
            if (MessageBox.Show(this, information, "温馨提示>>", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information,System.Windows.Forms.MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取报告卡-选择树后的
        /// </summary>
        /// <returns></returns>
        private Neusoft.HISFC.DCP.Object.CommonReport GetSelectedReport()
        {
            Neusoft.HISFC.DCP.Object.CommonReport report = null;
            if (this.tvPatientInfo.SelectedNode != null && this.tvPatientInfo.SelectedNode.Tag != null && this.tvPatientInfo.SelectedNode.Tag is Neusoft.HISFC.DCP.Object.CommonReport)
            {
                report = this.tvPatientInfo.SelectedNode.Tag as Neusoft.HISFC.DCP.Object.CommonReport;
            }
            return report;
        }

        /// <summary>
        /// 清空头部信息
        /// </summary>
        private void ClearHeadInfo()
        { 
            txtClinicNO.Text = string.Empty;
            lbID.Text = string.Empty;
            lbState.Text = string.Empty;
        }

        #region 保存[新加或者修改]

        /// <summary>
        /// 保存,订正,修改
        /// </summary>
        /// <returns>-1失败</returns>
        public int SaveRepot()
        {
            #region 报卡信息获取、验证

            Neusoft.HISFC.DCP.Object.CommonReport report = this.GetSelectedReport();
            if (report == null || string.IsNullOrEmpty(report.ID))
            {
                report = new Neusoft.HISFC.DCP.Object.CommonReport();
            }

            if (this.AuthenticationInfo(ref report) == -1)
            {
                return -1;
            }
            #endregion

            #region 用户确认

            if (cmbInfectionClass.Enabled == true
                && MessageBox.Show(this, "您选择了【" + report.Disease.Name + "】\n保存后电子报卡由系统自动上传【疾病预防科】\n确认保存吗？", "温馨提示>>", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information)
                == System.Windows.Forms.DialogResult.No)
            {
                return -1;
            }

            #endregion

            #region 数据保存

            if (this.hshNeedBill.Contains(report.Disease.ID))
            {
                if (report.Memo != string.Empty)
                {
                    if (report.Memo.IndexOf("已转诊") == -1)
                    {
                        report.Memo = "已转诊\\\\" + report.Memo;
                    }
                }
                else
                {
                    report.Memo = "已转诊\\\\";
                }
            }

            Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在保存,请稍候....");
            Application.DoEvents();

            //报告编号为空 作为新卡插入数据库
            
            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();            

            this.diseaseMgr.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);

            if (string.IsNullOrEmpty( report.ID ) == true && string.IsNullOrEmpty(report.ReportNO) == true)
            {
                #region 新卡插入处理

                //如果是订正 需要更新原卡
                if (this.diseaseMgr.InsertCommonReport(report) == -1)
                {
                    Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    report.ID = string.Empty;
                    report.ReportNO = string.Empty;
                    this.MyMessageBox("报告卡保存失败>>" + this.diseaseMgr.Err, "err");
                    return -1;
                }

                //附卡保存
                if (IsNeedAdd)
                {
                    if(this.UpdateAdditionInfo(this.operType, report)==-1)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("存储附卡信息失败"));
                        return -1;
                    }
                }

                #endregion
            }
            else
            {
                #region 旧卡更新处理
                Neusoft.HISFC.DCP.Object.CommonReport mainreport = this.GetSelectedReport();//原卡
                report.ID = ((Neusoft.HISFC.DCP.Object.CommonReport)this.tvPatientInfo.SelectedNode.Tag).ID;
                report.ReportNO = ((Neusoft.HISFC.DCP.Object.CommonReport)this.tvPatientInfo.SelectedNode.Tag).ReportNO;
                report.CorrectFlag = mainreport.CorrectFlag;
                report.CorrectReportNO = mainreport.CorrectReportNO;
                report.CorrectedReportNO = mainreport.CorrectedReportNO;
                report.ExtendInfo3 = mainreport.ExtendInfo3;
                if (this.diseaseMgr.UpdateCommonReport(report.Clone()) == -1)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    this.MyMessageBox("报告卡保存失败>>" + this.diseaseMgr.Err, "err");
                    return -1;
                }
                //附卡保存
                if (IsNeedAdd)
                {
                    this.UpdateAdditionInfo(this.operType, report.Clone());
                }

                #endregion
            }

            Neusoft.FrameWork.Management.PublicTrans.Commit();
            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
            if (this.patient != null && this.type != Neusoft.HISFC.DCP.Enum.PatientType.O && this.infectCode != "")
            {
                this.infectCode = "";
                this.reportOperResult = Neusoft.HISFC.DCP.Enum.ReportOperResult.OK;
            }
            #endregion

            #region 附加提示信息

            this.GetMessage(report);

            #endregion

            return 0;
        }

        /// <summary>
        /// 附加提示信息
        /// </summary>
        /// <param name="report">疾病编号</param>
        /// <returns>附加信息</returns>
        private void GetMessage(Neusoft.HISFC.DCP.Object.CommonReport report)
        {
            string message = "报告卡成功保存并上报!\n\n";
            string diseaseID = report.Disease.ID;

            if (this.hshNeedCheckedBlood.Contains(diseaseID))
            {
                message += "请通知护士采血，标本送越秀区CDC检测麻疹IgM抗体\n";
            }

            //周末电话开关

            ArrayList altemp = new ArrayList();
            altemp = this.commonProcess.QueryConstantList("SWITCH");
            string strtelephone = "";
            foreach (Neusoft.HISFC.Models.Base.Const conOb in altemp)
            {
                strtelephone += conOb.Memo + "\n";
            }

            //if (strtelephone == "") 取消节假日日开关的优先级 2011-3-9
            {
                //电话通知
                if (this.hshNeedTelInfect.Contains(diseaseID))
                {
                    ArrayList al = new ArrayList();
                    al = this.commonProcess.QueryConstantList("MESSAGE");
                    foreach (Neusoft.HISFC.Models.Base.Const con in al)
                    {
                        message += con.Memo + "\n";
                    }
                }
            }
            if (message != "" && message != null)
            {
                MessageBox.Show(this, message, "提示>>");
            }

            if (strtelephone != "")
            {
                MessageBox.Show(this, strtelephone, "温馨提示>>", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            int diseasecode = Neusoft.FrameWork.Function.NConvert.ToInt32(diseaseID);

            if (diseasecode == 1002 || diseasecode == 1003)
            {

            }
            if (diseasecode >= 1033 && diseasecode <= 1038)
            {
                this.MyMessageBox("请及时将患者转往【性病中心】诊治", "性病归口管理提示>>");
            }
            else if (diseasecode >= 7001 && diseasecode <= 7005)
            {
                this.MyMessageBox("请及时将患者转往【性病中心】诊治", "性病归口管理提示>>");
            }
            this.ShowMessageAfterSave(diseasecode.ToString());
        }


        /// <summary>
        /// 显示不分状态的报告卡
        /// </summary>
        /// <param name="al"></param>
        private void TreeViewAddReportsIgnorState(ArrayList al)
        {
            this.tvPatientInfo.Nodes.Clear();
            if (al == null || al.Count < 1)
            {
                return;
            }

            Array alState = Enum.GetValues(typeof(Neusoft.HISFC.DCP.Enum.ReportState));
            foreach (Neusoft.HISFC.DCP.Enum.ReportState reportState in alState)
            {
                ArrayList altemp = new ArrayList();
                foreach (Neusoft.HISFC.DCP.Object.CommonReport report in al)
                {
                    if (report.State == Function.ConvertState(reportState))
                    {
                        altemp.Add(report);
                    }
                }
                this.TreeViewAddReports(altemp);
            }
        }

        #endregion

        #region 删除

        /// <summary>
        /// 删除报告卡
        /// </summary>
        /// <param name="ID">编号</param>
        public int DeleteReport(string ID)
        {
            System.Windows.Forms.DialogResult dr = new DialogResult();
            dr = MessageBox.Show("确定要删除报告卡吗？\n删除后不能恢复！", "提示>>", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning, System.Windows.Forms.MessageBoxDefaultButton.Button2);

            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                int param = this.diseaseMgr.DeleteCommonReport( ID );
                if (param == 1)
                {
                    this.MyMessageBox("报告卡删除成功!", "提示>>");
                    return -1;                 
                }
                else if (param == 0)
                {
                    this.MyMessageBox( "报告卡已经过修订或审核 无法进行删除", "提示" );
                }
                else
                {
                    this.MyMessageBox( "报告卡删除失败!" + this.diseaseMgr.Err, "错误>>" );
                }

                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 删除报卡
        /// </summary>
        private void DeleteReport ()
        {
            if (this.TreeViewType == enumTreeViewType.PatientInfo || this.tvPatientInfo.SelectedNode == null)
            {
                return;
            }
            if (this.tvPatientInfo.SelectedNode.Tag.ToString() == "root")
            {
                return;
            }

            Neusoft.HISFC.DCP.Object.CommonReport report = new Neusoft.HISFC.DCP.Object.CommonReport();
            report = (Neusoft.HISFC.DCP.Object.CommonReport)this.tvPatientInfo.SelectedNode.Tag;
            if (report != null && report.ID != "")
            {
                if (this.DeleteReport( report.ID ) == 0)
                {
                    this.tvPatientInfo.Nodes.Remove( this.tvPatientInfo.SelectedNode );

                    //查询新加
                    ArrayList alTempReport = new ArrayList();
                    foreach (ArrayList al in this.AlReport)
                    {
                        ArrayList altemp = new ArrayList();
                        foreach (Neusoft.HISFC.DCP.Object.CommonReport rpt in al)
                        {
                            if (rpt.ID != report.ID)
                            {
                                altemp.Add(rpt);
                            }
                        }
                        if (altemp != null && altemp.Count > 0)
                        {
                            alTempReport.Add(altemp);
                        }
                    }
                    this.AlReport = alTempReport;
                    this.ReflashTreeView(this.AlReport);
                }
            }
        }      

        #endregion

        #region 打印

        /// <summary>
        /// 打印-需要先保存
        /// </summary>
        protected override int OnPrint(object sender, object neuObject)
        {
            if(string.IsNullOrEmpty(lbID.Text.Trim()))
            {
                return -1;
            }
            ucPrint uc = new ucPrint();
            Neusoft.FrameWork.WinForms.Classes.Print p = new Neusoft.FrameWork.WinForms.Classes.Print();
            uc.lbState.Text = this.lbState.Text;
            uc.lbID.Text = this.lbID.Text;
            uc.lbClinicNO.Text = this.txtClinicNO.Text;

            uc.lbPatientName.Text = this.txtPatientName.Text;
            uc.lbPatientParents.Text = this.txtPatientParents.Text;
            uc.lbPatientID.Text = this.txtPatientID.Text;
            uc.lbSex.Text = this.cmbSex.Text;
            uc.lbBirthday.Text = this.dtBirthDay.Value.ToShortDateString();
            uc.lbAge.Text = this.txtAge.Text;

            uc.rdbDay.Checked = this.rdbDay.Checked;
            uc.rdbMonth.Checked = this.rdbMonth.Checked;
            uc.rdbYear.Checked = this.rdbYear.Checked;

            uc.cbxHomeAearOne.Checked = this.cbxHomeAearOne.Checked;
            uc.cbxHomeAearTwo.Checked = this.cbxHomeAearTwo.Checked;
            uc.cbxHomeAearThree.Checked = this.cbxHomeAearThree.Checked;
            uc.cbxHomeAearFour.Checked = this.cbxHomeAearFour.Checked;
            uc.cbxHomeAearFive.Checked = this.cbxHomeAearFive.Checked;
            uc.cbxHomeAearSix.Checked = this.cbxHomeAearSix.Checked;

            uc.lbSpecificlAddress.Text = this.txtSpecialAddress.Text;
            uc.lbTelephone.Text = this.txtTelephone.Text;
            uc.lbProfession.Text = this.cmbProfession.Text;
            uc.lbWorkPlace.Text = this.txtWorkPlace.Text;

            uc.lbInfectionClass.Text = this.cmbInfectionClass.Text;
            uc.lbCaseClassOne.Text = this.cmbCaseClassOne.Text;
            uc.lbCaseClaseTwo.Text = this.cmbCaseClaseTwo.Text;

            uc.rdbInfectOtherYes.Checked = this.rdbInfectOtherYes.Checked;
            uc.rdbInfectionOtherNo.Checked = this.rdbInfectionOtherNo.Checked;

            uc.lbInfectionDate.Text = this.dtInfectionDate.Value.ToShortDateString();
            uc.lbDiaDate.Text = this.dtDiaDate.Value.ToString();

            //特殊处理
            if (!this.cbxDeadDate.Checked)
            {
                uc.lbDeadDate.Text = new DateTime(this.dtDeadDate.Value.Year, this.dtDeadDate.Value.Month,
                    this.dtDeadDate.Value.Day, 0, 0, 0).ToShortDateString();//死亡日期
            }
            else
            {
                uc.lbDeadDate.Text = "";
            }          

            uc.neuSpread1_Sheet1.Cells[0,0].Text = this.rtxtMemo.Text;
            uc.lbCase.Text = this.txtCase.Text;
            uc.lbReportDoctor.Text = this.cmbReportDoctor.Text;
            uc.lbDoctorDept.Text = this.cmbDoctorDept.Text;
            uc.lbReportTime.Text = this.lbReportTime.Text;

            p.PrintPage(0, 30, uc);
            return base.OnPrint(sender, neuObject);
        } 
       
        #endregion

        #region 信息管理

        /// <summary>
        /// MessageBox
        /// </summary>
        /// <param name="message">提示信息</param>
        /// <param name="type">err错误 其它作标题</param>
        private void MyMessageBox(string message, string type)
        {
            switch (type)
            {
                case "err":
                    MessageBox.Show(message, "提示", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    break;
                default:
                    MessageBox.Show(message, type, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    break;
            }
        }

        /// <summary>
        /// 判断状态是否变化决定时候继续操作
        /// </summary>
        /// <param name="ID">编号</param>
        /// <param name="state">状态</param>
        /// <returns>true 状态未变</returns>
        private bool CheckState(string ID, string reportState)
        {
            string tempstate = "";
            try
            {
                Neusoft.HISFC.DCP.Object.CommonReport report = this.diseaseMgr.GetCommonReportByID(ID);
                tempstate = report.State;
            }
            catch (Exception ex)
            {
                this.MyMessageBox("更新数据时转换报告卡状态失败！" + ex.Message, "err");
                return false;
            }
            if (reportState != tempstate)
            {
                if (reportState == "1" || tempstate == "0")
                {
                    //修改合格
                    return true;
                }
                this.MyMessageBox("操作失败：报告卡装态已发生变化\n按[确定]后系统自动刷新", "提示>>");
                return false;
            }
            return true;

        }

        /// <summary>
        /// 地址显示设置
        /// </summary>
        /// <param name="isDetail">true详细地址模式</param>
        public void SetAddressInfoVisible(bool isDetail)
        {
            this.label52.Visible = isDetail;
            this.label53.Visible = isDetail;
            this.label54.Visible = isDetail;
            this.txtCounty.Visible = isDetail;
            this.txtCity.Visible = isDetail;
            this.txtProvince.Visible = isDetail;
            this.dtDeadDate.Visible = isDetail;

            this.txtSpecialAddress.Visible = true;
        }   

        /// <summary>
        /// 地址信息是否从住院主表中加载
        /// </summary>
        private bool IsAddressLoad
        {
            get 
            {
                return this.isAddressLoad;
            }
            set
            {
                this.isAddressLoad = value;
                if (this.isAddressLoad)
                {
                    this.cbxHomeAearOne.Checked = true;
                }
                else
                {
                    this.cbxHomeAearOne.Checked = false;
                }

            }
        }

        /// <summary>
        /// 添加患者基本信息
        /// </summary>
        /// <param name="patient">实体Neusoft.HISFC.Models.RADT.Patient</param>
        public void ShowPatienInfo(Neusoft.HISFC.Models.RADT.Patient patient)
        {
            //排除初始加载时的脏读情况 --zl
            if (patient.ID == null || patient.ID == "")
            {
                return;
            }

            //在新加卡时候自动带出患者信息
            try
            {
                string patientID = patient.PID.CardNO;

                if (this.PatientType == Neusoft.HISFC.DCP.Enum.PatientType.I)          //住院患者 patientID 取 PatientNO
                {
                    patientID = patient.PID.PatientNO;
                }

                if (patientID == null)
                {
                    patientID = patient.ID;

                    if (patient.ID.IndexOf( "ZY" ) >= 0)
                    {
                        patientID = patientID.Remove( 0, 4 );
                    }
                }

                if (patientID.Length > 0) 
                {
                    this.txtClinicNO.Text = patientID.PadLeft(10, '0');
                }
                if (this.PatientType == Neusoft.HISFC.DCP.Enum.PatientType.I)
                {
                    this.txtSpecialAddress.Text = patient.AddressHome;
                    this.IsAddressLoad = true;
                    this.lbPatientNO.Text = "住 院 号";
                }
                else if (this.PatientType == Neusoft.HISFC.DCP.Enum.PatientType.C)
                {
                    this.IsAddressLoad = false;
                    this.lbPatientNO.Text = "门 诊 号";
                }
                this.txtPatientName.Text = patient.Name;
                this.txtPatientID.Text = patient.IDCard;
                this.cmbSex.Tag = patient.Sex.ID;

                //患者职业赋值 没有取到？？？

                //if (patient.Profession.ID == "" || patient.Profession == null)
                //{
                //    patient.ID;

                    
                //}

                

                try
                {
                    this.dtBirthDay.Value = patient.Birthday;
                }
                catch
                {
                    this.dtBirthDay.Value = this.diseaseMgr.GetDateTimeFromSysDateTime();
                    this.txtAge.Text = "";
                    this.rdbYear.Checked = true;
                }
                this.txtWorkPlace.Text = patient.AddressBusiness;//单位地址
                if (!string.IsNullOrEmpty( patient.Profession.ID ))
                {
                    this.cmbProfession.SelectedValue = patient.Profession.ID;//职业
                }
                this.txtTelephone.Text = patient.PhoneHome;//联系电话
                this.txtSpecialAddress.Text = patient.AddressHome;//家庭住址
                this.cmbReportDoctor.Tag = this.User.ID;
                this.cmbDoctorDept.Tag = this.User.Dept.ID;
                this.operType = OperType.保存;
            }
            catch (Exception ex)
            {
                this.MyMessageBox("添加患者基本信息失败" + ex.Message, "err");
            }
        }

        /// <summary>
        /// 判断状态
        /// </summary>
        /// <param name="report"></param>
        private void CheckState(string state)
        {
            if (state == "3" || state == "4")
            {
                this.lblState.Text = "1";
            }
            else
            {
                this.lblState.Text = "0";
            }
        }

        /// <summary>
        /// 根据报告卡显示其信息
        /// </summary>
        /// <param name="report"></param>
        public void ShowReportData(Neusoft.HISFC.DCP.Object.CommonReport report)
        {
            try
            {
                this.SetOperType(OperType.查询);

                //编号
                this.lbID.Text = report.ReportNO;

                if (report.CorrectFlag != null && report.CorrectFlag != "" && report.ID != "")
                {
                    this.lbState.Text = "订正报卡";
                    Neusoft.HISFC.DCP.Object.CommonReport tempReport = this.diseaseMgr.GetCommonReportByID(report.CorrectedReportNO);
                    try
                    {
                        this.lbState.Text += "(初报卡：" + tempReport.ExtendInfo1 + ")";
                    }
                    catch
                    { }
                }
                else
                {
                    this.lbState.Text = "初次报卡";
                    if (report.ExtendInfo3 != "")
                    {
                        Neusoft.HISFC.DCP.Object.CommonReport tempReport = this.diseaseMgr.GetCommonReportByNO(report.ID);
                        try
                        {
                            this.lbState.Text += "(订正卡：" + tempReport.ExtendInfo1 + ")";
                        }
                        catch
                        { }
                    }
                }

                //搞报告卡状态，赋值到页面
                this.CheckState(report.State);              


                //患者号
                this.txtClinicNO.Text = report.Patient.PID.CardNO;
                //患者姓名
                this.txtPatientName.Text = report.Patient.Name;
                //家长姓名
                this.txtPatientParents.Text = report.PatientParents;
                //身份证号
                this.txtPatientID.Text = report.Patient.IDCard;
                //性别
                this.cmbSex.Tag = report.Patient.Sex.ID;           
                //出生日期
                this.dtBirthDay.Value = report.Patient.Birthday;
                //年龄
                this.txtAge.Text = report.Patient.Age;
                //年龄单位
                switch (report.AgeUnit)
                {
                    case "0":
                        this.rdbYear.Checked = true;
                        break;
                    case "1":
                        this.rdbMonth.Checked = true;
                        break;
                    default:
                        this.rdbDay.Checked = true;
                        break;
                }
                //患者职业
                if (string.IsNullOrEmpty( report.Patient.Profession.ID ) == false)
                {
                    this.cmbProfession.Tag = report.Patient.Profession.ID;
                    this.cmbProfession.SelectedValue = report.Patient.Profession.ID;
                }                
 
                //患者工作单位
                this.txtWorkPlace.Text = report.Patient.CompanyName;
                //联系电话
                this.txtTelephone.Text = report.Patient.PhoneHome;
                //患者来源地
                this.SetHomeArea(Neusoft.FrameWork.Function.NConvert.ToInt32(report.HomeArea));
                this.txtSpecialAddress.Visible = true;

                if (string.IsNullOrEmpty( report.Patient.AddressHome ) == false)
                {
                    string[] householdNames = report.Patient.AddressHome.Split( ',' );
                    this.txtSpecialAddress.Text = householdNames[0];
                    this.cmbInfectionClass.SelectedValue = report.Disease.ID;
                }
                //xiwx 详细地址
                this.txtSpecialAddress.Text = report.ExtendInfo1;
                //发病日期
                if (report.InfectDate != DateTime.MinValue)
                {
                    this.dtInfectionDate.Value = report.InfectDate;
                }
                
                //诊断日期
                if (report.DiagnosisTime != DateTime.MinValue)
                {
                    this.dtDiaDate.Value = report.DiagnosisTime;
                }
                //死亡日期
                try
                {
                    //出异常说明死亡时间无效
                    this.dtDeadDate.Value = report.DeadDate;
                    this.cbxDeadDate.Checked = false;
                }
                catch
                {                    
                    this.cbxDeadDate.Checked = true;                     
                }
                //疾病名称
                if (report.Disease != null && string.IsNullOrEmpty(report.Disease.ID) == false)
                {
                    this.cmbInfectionClass.SelectedValue = report.Disease.ID;
                    this.cmbInfectionClass.Text = report.Disease.Name;
                }

                //病例分类
                if (report.CaseClass1 != null && string.IsNullOrEmpty(report.CaseClass1.ID) == false)
                {
                    this.cmbCaseClassOne.SelectedValue = report.CaseClass1.ID;
                }
                this.cmbCaseClaseTwo.SelectedValue = report.CaseClass2;
                //接触有无
                this.rdbInfectOtherYes.Checked = Neusoft.FrameWork.Function.NConvert.ToBoolean(report.InfectOtherFlag);
             
                //备注
                this.rtxtMemo.Text = report.Memo;

                //报告人 报告科室

                this.cmbReportDoctor.Tag = report.ReportDoctor.ID;
                this.cmbDoctorDept.Tag = report.DoctorDept.ID;
                //报告时间                
                this.lbReportTime.Text = report.ReportTime.ToString();

                //事由-退卡原因
                this.txtCase.Text = report.OperCase;
                
                //附卡
                if (IsNeedAdd)
                {
                    this.GetAdditionInfo(report);
                }

            }
            catch (Exception ex)
            {
                this.MyMessageBox("数据转换失败,信息可能不完全" + ex.Message, "err");
            }
        }

        /// <summary>
        /// 获取报告卡信息[含数据验证]
        /// </summary>
        /// <param name="report">传染病报卡</param>
        /// <param name="additionReport">传染病附卡</param>
        /// <param name="sexAdditionReport">性病附卡</param>
        /// <returns>-1 失败</returns>
        private int GetReportData(ref Neusoft.HISFC.DCP.Object.CommonReport report)
        {
            try
            {
                System.DateTime now = new DateTime();
                now = this.diseaseMgr.GetDateTimeFromSysDateTime();
                //修改的报告有虚拟编号、编号

                //如果是新卡记录报卡人
                if (this.operType == OperType.保存 || this.operType == OperType.订正)
                {
                    report.ReportTime = now;//报卡时间
                    report.DoctorDept.ID = this.User.Dept.ID;
                    report.ReportDoctor.ID = this.User.ID;//报卡医生
                }

                report.PatientType = Enum.GetName( typeof( Neusoft.HISFC.DCP.Enum.PatientType ), (int)this.PatientType );//类型
                if (this.txtClinicNO.Text.Trim() == null || this.txtClinicNO.Text.Trim() == "")
                {
                    this.MyMessageBox( "请填写病历号", "err" );
                    this.txtPatientName.Select();
                    this.txtPatientName.Focus();
                    return -1;
                }
                else
                {
                    report.Patient.PID.CardNO = this.txtClinicNO.Text;//患者号
                    report.PatientDept.ID = this.User.Dept.ID;
                }

                //姓名
                if (this.txtPatientName.Text.Trim() == null || this.txtPatientName.Text.Trim() == "")
                {
                    if (this.txtPatientParents.Text.Trim() == null || this.txtPatientParents.Text.Trim() == "")
                    {
                        this.MyMessageBox( "请填写姓名", "err" );
                        this.txtPatientName.Select();
                        this.txtPatientName.Focus();
                        return -1;
                    }
                }
                else
                {
                    report.Patient.Name = this.txtPatientName.Text.Trim();
                }
                report.PatientParents = this.txtPatientParents.Text.Trim();//家长姓名
             
                if (!string.IsNullOrEmpty( this.txtPatientID.Text ))
                {
                    string err = "";
                    if (Neusoft.FrameWork.WinForms.Classes.Function.CheckIDInfo( this.txtPatientID.Text, ref err ) == -1)
                    {
                        this.MyMessageBox( err, "err" );
                        this.txtPatientID.Select();
                        this.txtPatientID.Focus();
                        return -1;
                    }
                    else
                    {
                        string ID = this.txtPatientID.Text.Trim();
                        int year = 0;
                        int month = 0;
                        int day = 0;
                        DateTime dtBirth = System.DateTime.Now;
                        if (ID.Length == 15)
                        {
                            year = Convert.ToInt32( "19" + ID.Substring( 6, 2 ) );
                            month = Convert.ToInt32( ID.Substring( 8, 2 ) );
                            day = Convert.ToInt32( ID.Substring( 10, 2 ) );
                        }
                        else
                        {
                            year = Convert.ToInt32( ID.Substring( 6, 4 ) );
                            month = Convert.ToInt32( ID.Substring( 10, 2 ) );
                            day = Convert.ToInt32( ID.Substring( 12, 2 ) );
                        }
                        dtBirth = new DateTime( year, month, day );

                        report.Patient.IDCard = this.txtPatientID.Text.Trim();//身份证号
                    }
                }

                if (this.cmbSex.Tag != null && this.cmbSex.Tag.ToString() != "")
                {
                    report.Patient.Sex.ID = this.cmbSex.Tag;
                }
                else
                {
                    this.MyMessageBox( "请选择性别", "err" );
                    this.txtAge.Select();
                    this.txtAge.Focus();
                    return -1;
                }
                report.Patient.Birthday = new DateTime( this.dtBirthDay.Value.Year, this.dtBirthDay.Value.Month,
                    this.dtBirthDay.Value.Day, 0, 0, 0 );//出生日期

                //年龄
                string agemessage = "\n提示：您可以选择出生日期，系统会自动计算年龄";
                if (this.txtAge.Text.Trim() == null || this.txtAge.Text.Trim() == "")
                {
                    this.MyMessageBox( "请选择出生日期或填写年龄" + agemessage, "err" );
                    this.txtAge.Select();
                    this.txtAge.Focus();
                    return -1;
                }
                else
                {
                    for (int i = 0; i < this.txtAge.Text.Trim().Length; i++)
                    {
                        if (!Char.IsDigit( this.txtAge.Text.Trim(), i ))
                        {
                            this.MyMessageBox( "年龄应该为正整数" + agemessage, "err" );
                            this.txtAge.Select();
                            this.txtAge.Focus();
                            return -1;
                        }
                    }
                    report.Patient.Age = this.txtAge.Text.Trim();
                }

                //年龄单位
                report.AgeUnit = this.rdbYear.Checked ? "0" : (this.rdbMonth.Checked ? "1" : "2");
                if (report.AgeUnit == "1" || report.AgeUnit == "2"
                    || (report.AgeUnit == "0" && Neusoft.FrameWork.Function.NConvert.ToInt32( report.Patient.Age ) <= 14))
                {
                    if (this.txtPatientParents.Text.Trim() == null || this.txtPatientParents.Text.Trim() == "")
                    {
                        this.MyMessageBox( "14岁以下（含14岁）儿童，请填写家长姓名", "err" );
                        this.txtPatientParents.Select();
                        this.txtPatientParents.Focus();
                        return -1;
                    }
                }
                int intage = Neusoft.FrameWork.Function.NConvert.ToInt32( report.Patient.Age );
                if (this.rdbYear.Checked)
                {
                    if (this.diseaseMgr.GetAge( report.Patient.Birthday ) != report.Patient.Age + "年")
                    {
                        report.Patient.Birthday = new DateTime( now.AddYears( -intage ).Year, now.AddYears( -intage ).Month, now.AddYears( -intage ).Day, 0, 0, 0 );
                    }
                }
                if (this.rdbDay.Checked)
                {
                    if (intage == 31 && this.diseaseMgr.GetDateTimeFromSysDateTime().Day != 31)
                    {
                        intage = 100;
                    }
                    if (intage > 31)
                    {
                        this.MyMessageBox( "年龄天数大于一个月，请选择月份" + agemessage, "err" );
                        this.dtBirthDay.Select();
                        this.dtBirthDay.Focus();
                        return -1;
                    }
                }
                if (this.rdbMonth.Checked)
                {
                    if (intage > 12)
                    {
                        this.MyMessageBox( "年龄大于12个月，请填写周岁" + agemessage, "err" );
                        this.dtBirthDay.Select();
                        this.dtBirthDay.Focus();
                        return -1;
                    }
                    if (this.diseaseMgr.GetAge( report.Patient.Birthday ) != report.Patient.Age + "月")
                    {
                        report.Patient.Birthday = new DateTime( now.AddMonths( -intage ).Year, now.AddMonths( -intage ).Month, now.Day, 0, 0, 0 );
                    }
                }

                //患者来源
                string homearea = this.GetHomeArea();
                if (homearea == "7")
                {
                    this.MyMessageBox( "请选择现住地址", "err" );
                    this.cbxHomeAearOne.Select();
                    this.cbxHomeAearOne.Focus();
                    this.cbxHomeAearOne.Checked = false;
                    return -1;
                }
                report.HomeArea = homearea;

                //联系电话
                if (this.txtTelephone.Text.Trim() == "" || this.txtTelephone.Text.Trim() == null)
                {
                    this.MyMessageBox( "请填写联系电话", "err" );
                    this.txtTelephone.Select();
                    this.txtTelephone.Focus();
                    return -1;
                }
                report.Patient.PhoneHome = this.txtTelephone.Text.Trim();

                //职业代码
                if (this.cmbProfession.SelectedValue != null)
                {
                    string profession = this.cmbProfession.SelectedValue.ToString();
                    if (profession == "####")
                    {
                        this.MyMessageBox( "请选择患者职业", "err" );
                        this.cmbProfession.Select();
                        this.cmbProfession.Focus();
                        return -1;
                    }
                    report.Patient.Profession.ID = profession;
                }
                else
                {
                    this.MyMessageBox( "请选择患者职业", "err" );
                    this.cmbProfession.Select();
                    this.cmbProfession.Focus();
                    return -1;
                }

                //工作单位
                string workplace = "";
                workplace = this.txtWorkPlace.Text.Trim();
                //谁说工作单位必填呀？ 前面填写时已经给出填写提示了 2011-3-8
                //if (workplace == "" && this.hshStudent.Contains( report.Patient.Profession.ID ))
                //{
                //    this.MyMessageBox( "请在" + "\"" + "工作单位栏" + "\"" + "填写" + "\"" + this.hshStudent[report.Patient.Profession.ID].ToString() + "\"", "err" );
                //    this.txtWorkPlace.Select();
                //    this.txtWorkPlace.Focus();
                //    return -1;
                //}
                report.Patient.CompanyName = workplace;

                //report.PatientDept.ID = this.PatientDept.ID;
                //传染病
                //现住详细地址xiwx
                report.ExtendInfo1 = this.txtSpecialAddress.Text;

                Neusoft.FrameWork.Models.NeuObject disease = new Neusoft.FrameWork.Models.NeuObject();
                if (this.GetDisease( ref disease ) == -1)
                {
                    this.MyMessageBox( "请选择疾病名称", "err" );
                    this.cmbInfectionClass.Select();
                    this.cmbInfectionClass.Focus();
                    return -1;
                }
                report.Disease = disease;
                //新生儿破伤风处理
                if (this.hshLitteChild.Contains( report.Disease.ID ))
                {
                    if (report.AgeUnit != "2" || (Neusoft.FrameWork.Function.NConvert.ToInt32( report.Patient.Age ) > 28
                        && report.AgeUnit == "2"))
                    {
                        this.MyMessageBox( "新生儿破伤风年龄应小于28天，请核对诊断和年龄", "err" );
                        this.dtBirthDay.Select();
                        this.dtBirthDay.Focus();
                        return -1;
                    }
                }
                //病例分类 xiwx2011.2.22
                if (this.cmbCaseClassOne.SelectedValue == null || this.cmbCaseClassOne.SelectedValue.ToString() == "####")
                {
                    this.MyMessageBox("请选择病例分类", "err");
                    this.cmbCaseClassOne.Select();
                    this.cmbCaseClassOne.Focus();
                    return -1;
                }
                string caseclass = this.cmbCaseClassOne.SelectedValue.ToString();                
                report.CaseClass1.ID = caseclass;
                if (this.cmbCaseClaseTwo.Enabled)
                {
                    if (this.cmbCaseClaseTwo.SelectedValue == null)
                    {
                        this.MyMessageBox( "乙型肝炎/血吸虫病请选择病例分类2", "err" );
                        this.cmbCaseClaseTwo.Select();
                        this.cmbCaseClaseTwo.Focus();
                        return -1;
                    }
                    report.CaseClass2 = this.cmbCaseClaseTwo.SelectedValue.ToString();
                }
                else
                {
                    report.CaseClass2 = "";
                }
                //发病日期              
                report.InfectDate = new DateTime( this.dtInfectionDate.Value.Year, this.dtInfectionDate.Value.Month,
                        this.dtInfectionDate.Value.Day, 0, 0, 0 );//发病日期
              
               
                report.DiagnosisTime = new DateTime( this.dtDiaDate.Value.Year, this.dtDiaDate.Value.Month,
                    this.dtDiaDate.Value.Day, this.dtDiaDate.Value.Hour, 0, 0 );//诊断日期
                if (this.diseaseMgr.GetDateTimeFromSysDateTime().CompareTo( report.DiagnosisTime ) < 0)
                {
                    this.MyMessageBox( "诊断日期超过了今天", "err" );
                    this.dtInfectionDate.Select();
                    this.dtInfectionDate.Focus();
                    return -1;
                }
                //死亡日期赋值
                if (this.cbxDeadDate.Checked)
                {   
                    //忽略
                    report.DeadDate = new DateTime(1, 1, 1);                   
                }
                else
                {
                    report.DeadDate = new DateTime(this.dtDeadDate.Value.Year, this.dtDeadDate.Value.Month,
                       this.dtDeadDate.Value.Day, 0, 0, 0);//死亡日期
                }

                if (report.DiagnosisTime.CompareTo( report.InfectDate ) < 0)
                {
                    this.MyMessageBox( "诊断日期应大于发病日期", "err" );
                    this.dtInfectionDate.Select();
                    this.dtInfectionDate.Focus();
                    return -1;
                }
                //死亡日期
                if (!this.cbxDeadDate.Checked)
                {
                    if (report.DiagnosisTime.CompareTo( report.DeadDate ) < 0)
                    {
                        this.MyMessageBox( "诊断日期应大于死亡日期", "err" );
                        this.dtDeadDate.Select();
                        this.dtDeadDate.Focus();
                        return -1;
                    }
                    if (report.DeadDate.CompareTo( report.InfectDate ) < 0)
                    {
                        this.MyMessageBox( "死亡日期应大于发病日期", "err" );
                        this.dtDeadDate.Select();
                        this.dtDeadDate.Focus();
                        return -1;
                    }
                }
                //当含有附卡的报告修改为不含附卡的报告时一定要更改附卡标志


                //感染其他
                report.InfectOtherFlag = this.rdbInfectOtherYes.Checked ? "1" : (this.rdbInfectionOtherNo.Checked ? "0" : "");
                //事由
                //repor = this.txtCase.Text.Trim();
                //备注字数控制
                if (report.Memo.Length > 100)
                {
                    this.MyMessageBox( "报告卡保存失败\n\n备注：" + report.Memo + "\n\n字数过多，请控制在100字内\n", "err" );
                    this.rtxtMemo.Text = report.Memo;
                    return -1;
                }
                //备注
                report.Memo = this.rtxtMemo.Text.Trim();
                if (this.hshNeedMemo.Contains( report.Disease.ID ) && report.Memo == "")
                {
                    this.MyMessageBox( "请在备注中填写疾病名称", "err" );
                    return -1;
                }

                report.ModifyOper.ID = this.User.ID;
                report.ModifyTime = now;

                //操作信息
                report.Oper.ID = this.cmbReportDoctor.Tag.ToString();
                report.OperDept.ID = this.User.Dept.ID;
                report.OperTime = now;//this.myReport.GetDateTimeFromSysDateTime();
                if (this.operType == OperType.订正)
                {
                    this.RenewInfo( ref report );
                }
                report.State = Function.ConvertState( Neusoft.HISFC.DCP.Enum.ReportState.New );
            }
            catch (Exception ex)
            {
                this.MyMessageBox( "获取报告卡信息失败" + ex.Message, "err" );
                return -1;
            }
            return 0;
        }     

        /// <summary>
        /// 订正的时候要清除从原卡复制过来的部分信息
        /// </summary>
        /// <param name="report"></param>
        private void RenewInfo(ref Neusoft.HISFC.DCP.Object.CommonReport report)
        {
            if (report == null)
            {
                return;
            }
            System.DateTime dt = new DateTime(1, 1, 1);
            report.ModifyOper.ID = "";
            report.ModifyTime = dt;
            report.ApproveOper.ID = "";
            report.ApproveTime = dt;
        }

        /// <summary>
        /// 患者来源check
        /// </summary>
        /// <param name="index"></param>
        private void SetHomeArea(int index)
        {
            try
            {
                //已填写的报告地址合并显示
                this.cbxHomeAearOne.Checked = (index == 1 ? true : false);
                this.cbxHomeAearTwo.Checked = (index == 2 ? true : false);
                this.cbxHomeAearThree.Checked = (index == 3 ? true : false);
                this.cbxHomeAearFour.Checked = (index == 4 ? true : false);
                this.cbxHomeAearFive.Checked = (index == 5 ? true : false);
                this.cbxHomeAearSix.Checked = (index == 6 ? true : false);
                #region 不用
                //if (index == 5 || index == 6 || (this.lbID.Text != null && this.lbID.Text != ""))
                //{
                //    this.txtSpecialAddress.Visible = true;
                //    this.txtProvince.Clear();
                //    this.txtCity.Clear();
                //    this.txtCounty.Clear();
                //}
                //else
                //{
                //    //this.txtSpecialAddress.Visible = false;
                //    if (index == 1)
                //    {
                //        this.txtProvince.Text = this.myProvince;
                //        this.txtCity.Text = this.myCity;
                //        this.txtCounty.Text = this.myCounty;
                //    }
                //    else if (index == 2)
                //    {
                //        this.txtProvince.Text = this.myProvince;
                //        this.txtCity.Text = this.myCity;
                //        this.txtCounty.Clear();
                //    }
                //    else if (index == 3)
                //    {
                //        this.txtProvince.Text = this.myProvince;
                //        this.txtCity.Clear();
                //        this.txtCounty.Clear();
                //    }
                //    else
                //    {
                //        this.txtProvince.Clear();
                //        this.txtCity.Clear();
                //        this.txtCounty.Clear();
                //    }
                //}
                #endregion
            }
            catch
            {
                this.cbxHomeAearOne.Checked = true;
            }
            //			this.cmbprovince.Select();
            //			this.cmbprovince.Focus();
        }

        /// <summary>
        /// 获取患者来源
        /// </summary>
        /// <returns></returns>
        private string GetHomeArea()
        {
            try
            {
                if (this.cbxHomeAearOne.Checked)
                    return "1";
                else if (this.cbxHomeAearTwo.Checked)
                    return "2";
                else if (this.cbxHomeAearThree.Checked)
                    return "3";
                else if (this.cbxHomeAearFour.Checked)
                    return "4";
                else if (this.cbxHomeAearFive.Checked)
                    return "5";
                else if (this.cbxHomeAearSix.Checked)
                    return "6";
                else return "8";//不选择 错误
            }
            catch
            {
                return "7";//其它
            }
        }

        /// <summary>
        /// 获取疾病信息
        /// </summary>
        /// <param name="disease"></param>
        private int GetDisease(ref Neusoft.FrameWork.Models.NeuObject disease)
        {
            if (this.cmbInfectionClass.SelectedValue.ToString() == "####"
                || hshInfectClass.Contains(this.cmbInfectionClass.SelectedValue.ToString()))
            {
                return -1;
            }
            // if (this.rdbInfectionClass.Checked)
            {
                disease.ID = this.cmbInfectionClass.SelectedValue.ToString();
                string diseasename = (string)this.cmbInfectionClass.Text;
                if (diseasename != null && diseasename != "")
                {
                    disease.Name = diseasename;
                }
                else
                {
                    disease.Name = this.cmbInfectionClass.Text;
                }
            }
            disease.Memo = disease.ID.Substring(0, 1);
            return 0;
        }

        /// <summary>
        /// 设置llbPatientNO的可用
        /// </summary>
        /// <param name="patientType"></param>
        public void SetEnablellb(Neusoft.HISFC.DCP.Enum.PatientType patientType)
        {
            if (Neusoft.HISFC.DCP.Enum.PatientType.C == patientType)
            {
                this.llbPatienNO.Enabled = false;
                this.llbPatienNO.Text = "病 历 号";
                this.lbPatientName.Text = "门诊患者";
                this.LlbPatientType = Neusoft.HISFC.DCP.Enum.PatientType.C;
            }
            else if (Neusoft.HISFC.DCP.Enum.PatientType.I == patientType)
            {
                this.llbPatienNO.Enabled = false;
                this.llbPatienNO.Text = "住 院 号";
                this.lbPatientName.Text = "住院患者";
                this.LlbPatientType = Neusoft.HISFC.DCP.Enum.PatientType.I;
            }
            else
            {
                this.llbPatienNO.Enabled = true;
                this.llbPatienNO.Text = "住 院 号";
                this.lbPatientName.Text = "住院患者";
                this.LlbPatientType = Neusoft.HISFC.DCP.Enum.PatientType.I;
            }
        }

        #endregion

        #region 新建、清屏

        /// <summary>
        /// 清除所有报告信息
        /// </summary>
        /// <param name="isClearNo">false除患者号、Printpanel.Tag、报告编号、状态外都清除</param>
        public void ClearAll(bool isClearNo)
        {
            if (isClearNo)
            {
                this.lbID.Text = "";
                this.lbState.Text = "";
            }
            this.ClearAll();
        }

        /// <summary>
        /// 清除所有文本信息
        /// </summary>
        public void ClearAll()
        {
            try
            {   
                //默认忽略死亡时间
                cbxDeadDate.Checked = true;
                this.txtClinicNO.Text = "";
                this.lbID.Text = "";
                this.txtPatientName.Text = "";
                this.txtPatientParents.Clear();
                this.txtPatientID.Clear();

                this.txtAge.Clear();
                this.rdbYear.Checked = true;
                this.txtWorkPlace.Clear();
                this.txtTelephone.Clear();
                this.txtProvince.Clear();
                this.txtCity.Clear();
                this.txtCounty.Clear();
                this.cmbProfession.SelectedIndex = 0;
                this.cmbInfectionClass.SelectedIndex = 0;

                this.dtBirthDay.Value = this.diseaseMgr.GetDateTimeFromSysDateTime();
                this.txtAge.Text = "";
                this.rdbYear.Checked = true;

                this.dtInfectionDate.Value = this.dtBirthDay.Value;
                this.dtDiaDate.Value = this.dtBirthDay.Value;
                this.dtDeadDate.Value = this.dtBirthDay.Value;              

                this.cbxDeadDate.Checked = true;                

                this.cmbCaseClassOne.SelectedIndex = 0;
                this.rdbInfectionOtherNo.Checked = true;
                this.rtxtMemo.Clear();
                this.txtCase.Clear();
                this.tcReport.TabPages.RemoveByKey("tpAddition");
                this.IsNeedAdd = false;
                //报告人 报告科室
                this.cmbReportDoctor.SelectedText = this.employHelper.GetName(this.User.ID);
                this.cmbDoctorDept.SelectedText = this.deptHelper.GetName(this.User.Dept.ID);
                //报告时间
                this.lbReportTime.Text = this.diseaseMgr.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd HH:MM:ss");
                //xiwx详细地址
                this.txtSpecialAddress.Text = string.Empty;
                this.cbxHomeAearOne.Checked = true;
                this.rdbYear.Checked = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg(e.Message));
                return;
            }
        }

        #endregion

        #region 查询

        /// <summary>
        /// 查询
        /// </summary>
        private void Query()
        {
            this.ClearHeadInfo();

            if (this.cmbQueryContent.SelectedValue.ToString() == "ReportInfo")
            {
                #region 全院查询

                if (this.txtReportNo.Text.Trim() != "")
                {
                    this.QueryByReportNO();
                }
                else if (this.txtInPatienNo.Text.Trim() != "")
                {
                    this.QueryByPatientNO();
                }
                else if (this.txtName.Text != "")
                {
                    this.QueryByPatientName();
                }
                else if (this.txtDoctor.Text.Trim() != "")
                {
                    this.QueryByDoctorNO();
                }
                else
                {
                    this.QueryOldReport();
                }

                #endregion
            }
            else if (this.cmbQueryContent.SelectedValue.ToString() == "PatientInfo")
            {
                #region 患者查询

                if (this.txtInPatienNo.Text.Trim() != "")
                {
                    this.QueryByPatientNO();
                }
                else if (this.txtName.Text != "")
                {
                    this.QueryByPatientName();
                }
                else
                {
                    if (this.LlbPatientType == Neusoft.HISFC.DCP.Enum.PatientType.I)
                    {
                        this.QueryPatientByDeptIN();
                    }
                    else if(this.LlbPatientType == Neusoft.HISFC.DCP.Enum.PatientType.C)    //--修改
                    {
                        QueryPatientByDco();                    
                    }
                }

                #endregion
            }
            else if (this.cmbQueryContent.SelectedValue.ToString() == "DeptReport")
            {
                this.QueryDeptReport();
            }
            else if (this.cmbQueryContent.SelectedValue.ToString() == "DeptUnReport")
            {
                this.QueryDeptReportByReportState(Neusoft.HISFC.DCP.Enum.ReportState.UnEligible);
            }
            //else if (this.cmbQueryContent.SelectedValue.ToString() == "FeedBack")
            //{
            //    this.tvPatientInfo.Nodes.Clear();
            //    this.QueryFeedBackByDept();
            //}
        }

        /// <summary>
        /// 科室查询保告卡
        /// </summary>
        private void QueryDeptReport()
        {
            ArrayList al = this.diseaseMgr.GetCommonReportListByMore("report_date", this.dtBegin.Value.ToString(), this.dtEnd.Value.ToString(), "AAA", this.User.Dept.ID);
            this.TreeViewAddReports( al );
        }

        /// <summary>
        /// 科室查询保告卡
        /// </summary>
        private void QueryDeptReportByReportState(Neusoft.HISFC.DCP.Enum.ReportState reportState)
        {
            ArrayList al = this.diseaseMgr.GetCommonReportListByMore("report_date", this.dtBegin.Value.ToString(), this.dtEnd.Value.ToString(), ((int)reportState).ToString(), this.User.Dept.ID);
            this.TreeViewAddReports( al );
        } 

        #endregion

        #region 订正

        /// <summary>
        /// 验证主卡，附卡信息是否完整
        /// </summary>
        /// <param name="report">主卡信息</param>
        /// <returns>-1 不完整，1 完整</returns>
        public int AuthenticationInfo(ref Neusoft.HISFC.DCP.Object.CommonReport report)
        {
            if (this.GetReportData(ref report) == -1)
            {
                return -1;
            }

            //是否有附卡
            if (this.IsNeedAdd)
            {
                if (this.JudgeAdditionInfo() <= 0)
                {
                    return -1;
                }
            }
            return 1;
        }

        /// <summary>
        /// 插入订正卡
        /// </summary>
        /// <param name="report">主卡信息</param>
        /// <param name="reportState"></param>
        /// <returns></returns>
        public int InsertCorrectReport(ref Neusoft.HISFC.DCP.Object.CommonReport report)
        {
            Neusoft.HISFC.DCP.Object.CommonReport tempReport = report;
            tempReport.CorrectedReportNO = report.ID;
            //备注中加入原病名
            if (report.Memo.IndexOf("//原病名[" + tempReport.Disease.Name + "]") == -1)
            {
                report.Memo += "//原病名[" + tempReport.Disease.Name + "]";
            }

            if (diseaseMgr.InsertCommonReport(tempReport) == -1)
            {
                this.MyMessageBox("订正卡保存失败" + this.diseaseMgr.Err, "err");
                return -1;
            }
            report = tempReport;

            //附卡保存
            if (this.IsNeedAdd)
            {
                if (this.UpdateAdditionInfo(this.operType, report) == -1)
                {
                    MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("存储附卡信息失败"));
                    return -1;
                }
            }
            return 1;
        }

        /// <summary>
        /// 修改原卡
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public int UpdateCorrectedReport(Neusoft.HISFC.DCP.Object.CommonReport mainreport)
        {
            if (this.diseaseMgr.UpdateCommonReport(mainreport) != 1)
            {
                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                this.MyMessageBox("报告卡保存失败>>" + this.diseaseMgr.Err, "err");
                return -1;
            }

            //附卡保存
            if (this.IsNeedAdd)
            {
                if (this.UpdateAdditionInfo(this.operType, mainreport) == -1)
                {
                    MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("存储附卡信息失败"));
                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        /// 保存订正，更新原卡
        /// </summary>
        /// <returns></returns>
        public int SaveCorrectReport()
        {
            Neusoft.HISFC.DCP.Object.CommonReport mainreport = new Neusoft.HISFC.DCP.Object.CommonReport();//原卡
            Neusoft.HISFC.DCP.Object.CommonReport report = new Neusoft.HISFC.DCP.Object.CommonReport();//订正卡

            mainreport = this.GetSelectedReport();
            if (mainreport == null)
            {
                return -1;
            }

            //验证信息
            if (this.AuthenticationInfo(ref report) == -1)
            {
                return -1;
            }

            //提示信息

            //获取订正卡信息
            if (this.operType == OperType.订正)
            {
                if (mainreport.CorrectFlag == "1")
                {
                    if(!this.IsContinue("此卡以订正过，是否继续订正？"))
                    {
                        return -1;
                    }
                }

                //订正卡               
                report.CorrectedReportNO = mainreport.ID;
                report.ExtendInfo3 = "订正卡原卡为[" + mainreport.ReportNO + "]";
                //备注中加入原病名
                if (report.Memo.IndexOf("//原病名[" + mainreport.Disease.Name + "]") == -1)
                {
                    report.Memo += "//原病名[" + mainreport.Disease.Name + "]";
                }
                //原卡
                mainreport.ExtendInfo3 = "已订正";
                mainreport.CorrectFlag = "1";
            }

            Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在保存,请稍候....");
            Application.DoEvents();
            //插入订正卡
            if (this.InsertCorrectReport(ref report) == -1)
            {
                return -1;
            }

            //修改原卡
            mainreport.CorrectReportNO = report.ID;
            if (this.UpdateCorrectedReport(mainreport) == -1)
            {
                return -1;
            }

            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
            // 附加信息
            this.GetMessage(report);

            return 0;
        }

        #endregion

        #region 附卡
        
        /// <summary>
        /// 根据疾病代码检查是否需要添加附卡
        /// </summary>
        /// <param name="diseaseCode"></param>
        public void IsNeedAddition(string infectCode)
        {
            string msg = "";
            //需性病报卡
            if (hshNeedSexReport.Contains(infectCode))
            {
                DiseaseReport.UC.ucBaseAddition sexAddition = new UFC.DCP.DiseaseReport.UC.ucSexAddition();
                this.AddAddtion(sexAddition);
                msg += "性病附卡||";
            }
            //需附卡
            if (hshNeedAdd.Contains(infectCode))
            {
                DiseaseReport.UC.ucBaseAddition otherAddition = new UFC.DCP.DiseaseReport.UC.ucOtherAddition();
                this.AddAddtion(otherAddition);
                msg += "附卡";
            }

            if (msg != "")
            {
                if (this.IsNeedAdditionMeg)
                {
                    MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("此疾病需要") + msg);
                    this.tcReport.SelectedIndex = 1;
                    this.tcReport.TabPages["tpAddition"].Select();
                    this.tcReport.TabPages["tpAddition"].Focus();
                }
                this.tcReport.TabPages["tpAddition"].AutoScroll = true;
                this.IsNeedAdd = true;              
                this.tcReport.TabPages["tpAddition"].BackColor = Color.White;
            }
        }

        /// <summary>
        /// 取附卡信息
        /// </summary>
        public void GetAdditionInfo(Neusoft.HISFC.DCP.Object.CommonReport diseaseReport)
        {
            if (this.tcReport.TabPages.ContainsKey("tpAddition"))
            {
                this.iAdditionReport = new DiseaseReport.UC.ucBaseAddition();
                this.iAdditionReport.SetAdditionInfo(this.iAdditionReport.GetAdditionInfo(diseaseReport.ReportNO),this.tcReport.TabPages["tpAddition"]);
                
            }
        }

        /// <summary>
        /// 修改附卡信息
        /// </summary>
        /// <param name="operType"></param>
        /// <param name="patientNO"></param>
        /// <param name="patientName"></param>
        /// <param name="report"></param>
        /// <returns></returns>
        public int UpdateAdditionInfo(OperType operType,Neusoft.HISFC.DCP.Object.CommonReport report)
        {
            if (this.tcReport.TabPages.ContainsKey("tpAddition"))
            {
                Neusoft.HISFC.DCP.Object.AdditionReport additionReport = new Neusoft.HISFC.DCP.Object.AdditionReport();
                this.iAdditionReport = new DiseaseReport.UC.ucBaseAddition();
                this.iAdditionReport.PatientNO = report.Patient.ID;
                this.iAdditionReport.PatientName = report.Patient.Name;
                this.iAdditionReport.Report = report;
                additionReport = (Neusoft.HISFC.DCP.Object.AdditionReport)this.iAdditionReport.GetAdditionInfo(this.tcReport.TabPages["tpAddition"]);
                additionReport.PatientNO = report.Patient.PID.ID;
                additionReport.PatientName = report.Patient.Name;
                additionReport.Memo = report.Disease.ID;

                int state = 0;
                if (string.IsNullOrEmpty(report.ID) == true)
                {
                    return this.iAdditionReport.InsertAdditionInfo(additionReport);                 
                }
                else if (operType == OperType.订正)
                {
                    state = this.iAdditionReport.UpdateAdditionInfo(additionReport);
                    if (state <= 1)
                    {
                        state = this.iAdditionReport.InsertAdditionInfo(additionReport);
                    }
                    return state;
                }
                else
                {
                    return this.iAdditionReport.UpdateAdditionInfo(additionReport);
                }
            }
            return 1;
        }

        /// <summary>
        /// 验证附卡信息的完整
        /// </summary>
        /// <returns>-1,不完整 1,完整</returns>
        public int JudgeAdditionInfo()
        {
            string msg="";
            int i=0;
            if(this.tcReport.TabPages.ContainsKey("tpAddition"))
            {
                foreach(Control c in this.tcReport.TabPages["tpAddition"].Controls)
                {
                    if (c.GetType() == typeof(DiseaseReport.UC.ucSexAddition))
                    {
                        i = ((DiseaseReport.UC.ucSexAddition)c).IsValid(ref msg);
                    }
                    else
                    {
                        i = ((DiseaseReport.UC.ucBaseAddition)c).IsValid(ref msg);
                    }
                    if (i < 0)
                    {
                        msg = "以下信息：" + msg + "不完整";
                        MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg(msg), "错误", MessageBoxButtons.OK);
                        this.tcReport.SelectedIndex = 1;
                        return -1;
                    }
                }
            }
            return i;
        }

        /// <summary>
        /// 添加附卡卡片
        /// </summary>
        /// <param name="ucBaseControl">附卡用户控件</param>
        public void AddAddtion(DiseaseReport.UC.ucBaseAddition ucBaseAddition)
        {
            if (this.tcReport.TabPages.Count == 1)
            {
                this.tcReport.TabPages.Add("tpAddition", "附卡信息");
                this.tcReport.TabPages["tpAddition"].Controls.Add(ucBaseAddition);
                ucBaseAddition.Dock = DockStyle.Top;
            }
            else
            {
                this.tcReport.TabPages["tpAddition"].Controls.Add(ucBaseAddition);
                ucBaseAddition.Dock = DockStyle.Top;
            }
            this.IsNeedAdd = true; 
        }

	    #endregion       

        #endregion

        #region 排序

        public class CompareState : IComparer
        {

            #region IComparer 成员

            public int Compare (object x, object y)
            {
                Neusoft.HISFC.DCP.Object.CommonReport r1 = x as Neusoft.HISFC.DCP.Object.CommonReport;
                Neusoft.HISFC.DCP.Object.CommonReport r2 = y as Neusoft.HISFC.DCP.Object.CommonReport;

                string oX = r1.State + r1.ReportNO;
                string oY = r2.State + r2.ReportNO;

                int nComp = 1;

                if (oX == null)
                {
                    nComp = (oY != null) ? -1 : 0;
                }
                else if (oY == null)
                {
                    nComp = 1;
                }
                else
                {
                    nComp = string.Compare( oX.ToString(), oY );
                }

                return nComp;
            }

            #endregion
        }

        #endregion

        #region IPreArrange 成员

        public int PreArrange ()
        {
            this.InitPrivInformation();

            this.isCDCPriv = Function.CheckUserPriv( Neusoft.FrameWork.Management.Connection.Operator.ID, "8001" );

            if (Neusoft.FrameWork.Management.Connection.Operator.ID != "009999")
            {
                if (isCDCPriv == false)
                {
                    MessageBox.Show("您无预控审核权限，无法进行相应操作", "权限不足", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return -1;
                }
            }
            //2011-3-8
            this.txtCase.Enabled = true;

            return 1;
        }

        #endregion

        #region 打印

        /// <summary>
        /// 打印-需要先保存
        /// </summary>
        public void print()
        {
            this.print(true);
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="needSave">true 保存后才能打印</param>
        public void print(bool needSave)
        {
            if (needSave && this.lbID.Text == "")
            {
                this.MyMessageBox("请先保存", "提示>>");
                return;
            }
            //备注的边界消除
            this.rtxtMemo.BorderStyle = System.Windows.Forms.BorderStyle.None;

            //详细地址的隐藏
            this.SetAddressInfoVisible(!this.txtSpecialAddress.Visible);

            if (this.cbxDeadDate.Checked)
            {
                this.dtDeadDate.Visible = false;
            }

            Neusoft.FrameWork.WinForms.Classes.Print p = new Neusoft.FrameWork.WinForms.Classes.Print();
            System.Drawing.Printing.PaperSize size = new System.Drawing.Printing.PaperSize("Letter", 700, 920);
            p.SetPageSize(size);
            p.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.None;
            //p.PrintPreview(55, 0, this.Printpanel);
            p.IsDataAutoExtend = true;
            p.IsAutoFont = true;

            //恢复
            this.rtxtMemo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SetAddressInfoVisible(true);
        }

        #endregion

        private void cbxDeadDate_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxDeadDate.Checked)
            {
                dtDeadDate.CustomFormat = " ";
            }
            else
            {
                dtDeadDate.CustomFormat = "yyyy年MM月dd日";
            }
        }

        /// <summary>
        /// 回车事件那个  --修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtInPatienNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.Query();
            }
        }

        #region 报告卡状态枚举

        // 摘要:
        //     报告卡状态
        //public enum ReportState
        //{
            // 摘要:
            //     新填
            //New = 0,
            ////
            //// 摘要:
            ////     合格
            //Eligible = 1,
            ////
            //// 摘要:
            ////     不合格
            //UnEligible = 2,
            ////
            //// 摘要:
            ////     报告人作废
            //OwnCancel = 3,
            ////
            //// 摘要:
            ////     保健科作废
            //Cancel = 4,
        //}

        #endregion
    }
}
