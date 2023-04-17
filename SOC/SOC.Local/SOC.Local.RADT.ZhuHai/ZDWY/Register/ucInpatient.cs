using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;
using FS.FrameWork.Management;
using FS.FrameWork.WinForms.Forms;
using FS.HISFC.Models.RADT;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using FS.SOC.HISFC.BizProcess.CommonInterface.Attributes;
using FS.SOC.Local.RADT.ZhuHai.ZDWY.Interface;


namespace FS.SOC.Local.RADT.ZhuHai.ZDWY.Register
{
    /// <summary>
    /// 入院登记主界面
    /// 用接口方式实现 列表显示和登记界面
    /// </summary>
    public partial class ucInpatient : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucInpatient()
        {
            InitializeComponent();
            this.GetParameter();
        }

        #region 变量

        /// <summary>
        /// 患者入出转
        /// </summary>
        private FS.HISFC.BizLogic.Fee.InPatient inpatientManager = new FS.HISFC.BizLogic.Fee.InPatient();
        /// <summary>
        /// 账户管理
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();

        /// <summary>
        /// 入出转
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// 入出转
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 费用公用接口业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 参数控制类
        /// </summary>
        private FS.FrameWork.Management.ControlParam ctlMgr = new FS.FrameWork.Management.ControlParam();

        /// <summary>
        /// 列表 入院登记接口
        /// </summary>
        private FS.SOC.HISFC.RADT.Interface.Register.IPatientList IRegisterList = null;

        /// <summary>
        /// 弹出入院登记接口
        /// </summary>
        private FS.SOC.HISFC.RADT.Interface.Register.IInpatient IRegister = null;

        /// <summary>
        /// 是否启用住院登记数据节点模块
        /// </summary>
        private bool IsInpatientRegisterStatisticsPointInUse = false;

        private FS.SOC.HISFC.RADT.Interface.Patient.IQuery IQueryPatientInfo = null;

        private FS.SOC.HISFC.BizProcess.CommonInterface.Common.ISave<PatientInfo> ISave = null;

        private FS.HISFC.Models.RADT.PatientInfo patientInfo = new PatientInfo();

        private bool isModify = false;
        private string tempUpdatePatientID;
        private string defaultPactCode = "2";
        private bool isAllowModifyInDate = false;
        private bool isAllowModifyName = false;
        private bool isAllowModifyPatientNo = false;
        private bool isShowPrePay = true;
        #endregion

        #region 属性
        private bool isInputDiagnose = false;// {D59C1D74-CE65-424a-9CB3-7F9174664504}

        [Category("输入设置"), Description("门诊诊断是否可以为空"), DefaultValue(false)]
        [FSSetting()]
        public bool IsInputDiagnose
        {
            set
            {
                isInputDiagnose = value;
            }
            get
            {
               return isInputDiagnose;
            }
        }

        private bool isInputLinkMan = false;// {6BF1F99D-7307-4d05-B747-274D24174895}

        [Category("输入设置"), Description("联系人是否必填"), DefaultValue(false)]
        [FSSetting()]
        public bool IsInputLinkMan
        {
            set
            {
                isInputLinkMan = value;
            }
            get
            {
                return isInputLinkMan;
            }
        }
        [Category("A.参数设置"), Description("是否允许修改入院日期:true 允许 false 不允许"), DefaultValue(false)]
        [FSSetting()]
        public bool IsAllowModifyInDate
        {
            set
            {
                isAllowModifyInDate = value;
            }
            get
            {
                return isAllowModifyInDate;
            }
        }

        [Category("A.参数设置"), Description("是否允许修改患者名字:true 允许 false 不允许"), DefaultValue(false)]
        [FSSetting()]
        public bool IsAllowModifyName
        {
            set
            {
                isAllowModifyName = value;
            }
            get
            {
                return isAllowModifyName;
            }
        }
        [Category("A.参数设置"), Description("是否允许修改住院号:true 允许 false 不允许"), DefaultValue(false)]
        [FSSetting()]
        public bool IsAllowModifyPatientNo
        {
            set
            {
                isAllowModifyPatientNo = value;
            }
            get
            {
                return isAllowModifyPatientNo;
            }
        }
        [Category("A.参数设置"), Description("是否显示预交金充值信息"), DefaultValue(false)]
        [FSSetting()]
        public bool IsShowPrePay// {5B3B503C-8CF5-415b-89EB-C11A4FEE8A19}
        {
            set
            {
                isShowPrePay = value;
            }
            get
            {
                return isShowPrePay;
            }
        }
        /// <summary>
        /// 间隔小时
        /// </summary>
        private int intervalHour = 3;
        [Category("A.参数设置"), Description("患者显示内容的间隔小时")]
        public int IntervalHour
        {
            get
            {
                return intervalHour;
            }
            set
            {
                this.intervalHour = value;
            }
        }

        private int showDays = 1;
        [Category("A.参数设置"), Description("显示几天内患者列表")]
        public int ShowDays
        {
            get
            {
                return showDays;
            }
            set
            {
                this.showDays = value;
            }
        }

        private bool isArriveProcess = false;
        [Category("A.参数设置"), Description("是否启用接诊流程，True:启用:False:不启用")]
        public bool IsArriveProcess
        {
            get
            {

                return isArriveProcess;
            }
            set
            {
                isArriveProcess = value;
            }
        }

        /// <summary>
        /// 是否生成默认警戒线
        /// </summary>
        private bool isCreateMoneyAlert = false;
        [Category("A.参数设置"), Description("登记时是否按合同单位自动生成默认警戒线,True:默认的警戒线取常数维护中合同单位的备注，请将备注设置为数字！5.0改为在常数表中维护-id【MONEYALERT】")]
        public bool IsCreateMoneyAlert
        {
            get
            {
                return this.isCreateMoneyAlert;
            }
            set
            {
                this.isCreateMoneyAlert = value;
            }
        }

        /// <summary>
        /// 是否默认临时号
        /// </summary>
        private int istemp = 0;
        [Category("A.参数设置"), Description("是否默认临时号1启用临时号 0不启用")]
        public int IsTempNo
        {
            get
            {
                return istemp;
            }
            set
            {
                this.istemp = value;
            }
        }

        /// <summary>
        /// 是否提醒打印腕带
        /// </summary>
        private bool isPrintBracelet = false;

        /// <summary>
        /// 是否提醒打印腕带
        /// </summary>
        [Category("A.参数设置"), Description("是否提醒打印腕带")]
        public bool IsPrintBracelet
        {
            get
            {
                return isPrintBracelet;
            }
            set
            {
                this.isPrintBracelet = value;
            }
        }
        /// <summary>
        /// 查询时是否过滤临时住院号患者
        /// </summary>
        private bool isFiltTemPatient = false;

        /// <summary>
        /// 查询时是否过滤临时住院号患者
        /// </summary>
        [Category("A.参数设置"), Description("查询时是否过滤临时住院号患者")]
        public bool IsFiltTemPatient
        {
            get
            {
                return isFiltTemPatient;
            }
            set
            {
                this.isFiltTemPatient = value;
            }
        }
        /// <summary>
        /// 是否启用自动生成住院号
        /// </summary>
        private bool isAutoPatientNO = false;
        [Category("A.参数设置"), Description("是否启用自动生成住院号，True:启用:False:不启用")]
        public bool IsAutoPatientNO
        {
            get
            {
                return isAutoPatientNO;
            }
            set
            {
                isAutoPatientNO = value;
            }
        }
        /// <summary>
        /// 是否默认生殖住院号
        /// </summary>
        private bool isFertilityPatientNO = false;
        [Category("A.参数设置"), Description("是否启用自动生成生殖住院号，True:启用:False:不启用")]
        public bool IsFertilityPatientNO
        {
            get
            {
                return isFertilityPatientNO;
            }
            set
            {
                isFertilityPatientNO = value;
            }
        }

        [Category("A.参数设置"), Description("列表显示类型")]
        public View ViewState
        {
            get
            {
                return this.IRegisterList.ViewState;
            }
            set
            {
                this.IRegisterList.ViewState = value;
            }
        }

        [Category("A.参数设置"), Description("点击医保按钮后，默认的合同单位编码")]
        public string DefualtPact
        {
            get
            {
                return defaultPactCode;
            }
            set
            {
                defaultPactCode = value;
            }
        }
        #endregion

        #region 工具栏

        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();

        /// <summary>
        /// 增加ToolBar控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override ToolBarService OnInit(object sender, object neuObject, object param)
        {
            //需修改
            toolBarService.AddToolButton("确认保存", "保存录入的信息", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B保存, true, false, null);
            toolBarService.AddToolButton("清屏", "清除录入的信息", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
            toolBarService.AddToolButton("患者查询", "打开患者查询界面", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C查询, true, false, null);
            toolBarService.AddToolButton("预约患者", "打开预约患者界面", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Y预约, true, false, null);
            toolBarService.AddToolButton("刷新", "刷新患者信息", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S刷新, true, false, null);
            toolBarService.AddToolButton("医保", "获取医保患者", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Y医保, true, false, null);
            toolBarService.AddToolButton("珠海医保查询", "珠海医保查询", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C查找人员, true, false, null);
            toolBarService.AddToolButton("中山医保查询", "中山医保查询", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C查找人员, true, false, null);
            toolBarService.AddToolButton("刷卡", "刷卡", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B报警, true, false, null);

            // {C2BDD877-D276-4856-903C-2AB79EF199AF}
            toolBarService.AddToolButton("套餐查看", "打开套餐界面", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C查询, true, false, null);
            
            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "确认保存":
                    this.save();
                    break;
                case "清屏":
                    this.clear();
                    break;
                case "刷新":
                    this.refreshPatients();
                    this.UpdateInvoice();
                    break;
                case "患者查询":
                    this.queryPatientInfo(null, true);
                    break;
                case "预约患者":
                    this.PrepayPatient();
                    break;
                case "医保":
                    this.ReadCard();
                    break;
                case "珠海医保查询":
                    this.QueryZhuHaiSI();
                    break;
                case "中山医保查询":
                    this.QueryZhongShanSI();
                    break;
                case "刷卡": 
                    string mCardNo = "";
                    string error = "";
                    // {DC68D3DF-1CB9-4d58-93E0-4F85B58E1647}
                    if (Function.OperMCard(ref mCardNo, ref error) < 0)
                    {
                        MessageBox.Show("读卡失败，请确认是否正确放置诊疗卡！\n" + error);
                        return;
                    }
                    this.IRegister.CardNo = ";"+mCardNo;
                    break;

                // {C2BDD877-D276-4856-903C-2AB79EF199AF}
                case "套餐查看":
                    this.queryPackage();
                    break;
                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取设置参数
        /// </summary>
        private void GetParameter()
        {
            //是否启用挂号数据节点模块
            this.IsInpatientRegisterStatisticsPointInUse = (this.ctlMgr.QueryControlerInfo("CPP003") == "1");
        }

       /// <summary>
        /// // {C2BDD877-D276-4856-903C-2AB79EF199AF}s
        /// 套餐查看
        /// </summary>
        private void queryPackage()
        {

            if (this.IQueryPatientInfo.PatientInfo == null || string.IsNullOrEmpty(this.IQueryPatientInfo.PatientInfo.PID.CardNO))
            {
                MessageBox.Show("请先检索患者！");
                return;
            }
            
            FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();
            FS.HISFC.Components.Common.Forms.frmPackageQuery frmpackage = new FS.HISFC.Components.Common.Forms.frmPackageQuery();
            frmpackage.PatientInfo = accountMgr.GetPatientInfoByCardNO(this.IQueryPatientInfo.PatientInfo.PID.CardNO);
            frmpackage.ShowDialog();
            
        
        }


        /// <summary>
        /// 预约患者
        /// </summary>
        private void PrepayPatient()
        {
            ucPrepayInQuery control = new ucPrepayInQuery();
            control.PrepayinState = "0";
            DialogResult result = FS.FrameWork.WinForms.Classes.Function.PopShowControl(control);
            if (result == DialogResult.OK)
            {
                if (control.PatientInfo != null)
                {
                    string inpatientNO = string.Empty;

                    inpatientNO = radtIntegrate.GetMaxPatientNOByCardNO(control.PatientInfo.PID.CardNO);
                    //数据库出错!
                    if (inpatientNO == null)
                    {
                        return;
                    }
                    if (inpatientNO.Trim() == string.Empty)
                    {
                        patientInfo.User01 = control.PatientInfo.User01;
                        patientInfo.User02 = control.PatientInfo.User02;
                        control.PatientInfo.PVisit.InSource.ID = "1";
                        control.PatientInfo.DoctorReceiver.ID = control.PatientInfo.PVisit.AdmittingDoctor.ID;

                        //未找到上次住院信息,弹出患者信息查询界面筛选患者
                        this.queryPatientInfo(control.PatientInfo, true);

                        this.IRegister.SetPatientInfo(control.PatientInfo, true);
                    }
                    else//找到了上次住院信息 
                    {
                        FS.HISFC.Models.RADT.PatientInfo pInfo = this.radtIntegrate.GetPatientInfomation(inpatientNO);

                        //if (pInfo.PVisit.InState.ID.ToString() == "R" || pInfo.PVisit.InState.ID.ToString() == "P" || pInfo.PVisit.InState.ID.ToString() == "I") //|| pInfo.PVisit.InState.ID.ToString() == "B"
                        //{
                        //    MessageBox.Show(Language.Msg("此患者在院治疗"));

                        //    return;
                        //}

                        pInfo.InTimes++;

                        if (pInfo.PVisit.InState.ID.ToString() == "N")//如果上次住院是无费退院，则住院次数
                        {
                            pInfo.InTimes--;
                        }
                        pInfo.PVisit.PatientLocation.Bed.ID = string.Empty;
                        pInfo.PatientType = control.PatientInfo.PatientType;
                        pInfo.PVisit.InSource.ID = "1";//来源为门诊转住院
                        pInfo.PVisit.PatientLocation.Dept.ID = control.PatientInfo.PVisit.PatientLocation.Dept.ID;//预住科室
                        pInfo.DoctorReceiver.ID = control.PatientInfo.PVisit.AdmittingDoctor.ID;//预住医生
                        pInfo.ClinicDiagnose = control.PatientInfo.ClinicDiagnose; //门诊诊断;
                        pInfo.User01 = control.PatientInfo.User01; //序列号
                        pInfo.User02 = "0";
                        patientInfo.User01 = control.PatientInfo.User01;
                        patientInfo.User02 = control.PatientInfo.User02;
                        this.IRegister.SetPatientInfo(pInfo, true);

                    }
                }
            }
        }

        /// <summary>
        /// 提示方法
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="msgIcon"></param>
        private void myMessageBox(string msg, MessageBoxIcon msgIcon)
        {
            CommonController.CreateInstance().MessageBox(this, msg, MessageBoxButtons.OK, msgIcon);
        }

        private void UpdateInvoice()
        {
            this.IRegister.Init();
        }

        /// <summary>
        /// 患者列表
        /// </summary>
        private void refreshPatients()
        {
            DateTime beginTime = inpatientManager.GetDateTimeFromSysDateTime();
            ArrayList patients = radtIntegrate.QueryPatientsByDateTime(beginTime.AddDays(-this.showDays), beginTime);
            if (patients == null)
            {
                this.myMessageBox(Language.Msg("刷新已登记患者信息出错!") + radtIntegrate.Err, MessageBoxIcon.Warning);
                return;
            }

            this.IRegisterList.LoadPatient(patients);

        }

        /// <summary>
        /// 初始化接口信息
        /// </summary>
        /// <returns></returns>
        private int initInterface()
        {
            this.IRegisterList = InterfaceManager.GetIPatientList();
            this.IRegister = InterfaceManager.GetIInpatient();
            this.IQueryPatientInfo = InterfaceManager.GetIInpatientQuery();
            this.ISave = InterfaceManager.GetPatientInfoISave();

            this.IRegisterList.OnSelectPatientInfo += new FS.SOC.HISFC.RADT.Interface.Register.SelectPatientInfo(IRegisterListInterface_OnSelectPatientInfo);
            this.IRegisterList.OnRefresh += new EventHandler(IRegisterListInterface_OnRefresh);


            this.IRegister.OnSavePatientInfo += new EventHandler(IRegisterInterface_OnSavePatientInfo);
            this.IRegister.OnQueryPatientInfo += new FS.SOC.HISFC.RADT.Interface.Register.SelectPatientInfo(IRegisterInterface_OnQueryPatientInfo);
            this.IRegister.IsArriveProcess = this.isArriveProcess;
            this.IRegister.IsAutoPatientNO = this.isAutoPatientNO;
            if (this.IRegister is FS.SOC.Local.RADT.ZhuHai.ZDWY.Base.Inpatient.ucRegisterInfo)
            {
                (IRegister as FS.SOC.Local.RADT.ZhuHai.ZDWY.Base.Inpatient.ucRegisterInfo).IsFertilityPatientNO = this.isFertilityPatientNO;
            }
            this.IRegister.IsCanModifyInTime = isAllowModifyInDate;
            this.IRegister.FileName = FS.FrameWork.WinForms.Classes.Function.SettingPath + "\\SOC.Components.Radt.ucRegisterInfo.xml";
            this.IRegister.IsCanModifyPatientNo = isAllowModifyPatientNo;
            this.IRegister.IsShowPrePay = this.isShowPrePay;
            this.IRegister.IsInputDiagnose = this.isInputDiagnose;// {D59C1D74-CE65-424a-9CB3-7F9174664504}
            this.IRegister.IsInputLinkMan = this.isInputLinkMan;
            if (this.IRegisterList is Control)
            {
                this.gbPatientList.Controls.Clear();
                //加载界面
                ((Control)this.IRegisterList).Dock = DockStyle.Fill;
                this.gbPatientList.Controls.Add((Control)this.IRegisterList);
            }

            if (this.IRegister is Control)
            {
                Control c = (Control)this.IRegister;
                this.gbQuery.Controls.Clear();
                c.Dock = DockStyle.Fill;
                this.gbQuery.Height = this.IRegister.ControlSize.Height + 20;
                this.gbQuery.Controls.Add(c);
            }

            this.IRegister.Init();

            return 1;
        }

        /// <summary>
        /// 清空
        /// </summary>
        public void clear()
        {
            this.IRegister.Clear();
            this.patientInfo = new PatientInfo();
            this.isModify = false;
            this.tempUpdatePatientID = "";
        }

        /// <summary>
        /// 设置患者信息
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public int setPatientInfo(string patientNO)
        {
            if (patientNO.Trim() == string.Empty)
            {
                return -1;
            }
            patientNO = FS.FrameWork.Public.String.FillString(patientNO);

            FS.HISFC.Models.RADT.PatientInfo patient = new FS.HISFC.Models.RADT.PatientInfo();
            //没有输入住院号,说明为第一次入院.
            if (patientNO == string.Empty)
            {
                this.myMessageBox("患者住院号为空!", MessageBoxIcon.Warning);
                return -1;
            }
            else
            {
                if (this.radtIntegrate.CreateAutoInpatientNO(patientNO, ref patient) == -1)
                {
                    this.myMessageBox("获得住院号出错!" + this.radtIntegrate.Err, MessageBoxIcon.Warning);
                    return -1;
                }

                //以前住过院
                if (patient.PatientNOType == EnumPatientNOType.Second)
                {
                    //{67F0DA44-EB32-4556-96C5-88B60C59E157}
                    #region 旧代码屏蔽
                    ////判断在院状态
                    //if (patient.PVisit.InState.ID.ToString() == FS.HISFC.Models.RADT.EnumPatientType.R.ToString()
                    //   || patient.PVisit.InState.ID.ToString() == FS.HISFC.Models.RADT.EnumPatientType.I.ToString()
                    //   || patient.PVisit.InState.ID.ToString() == FS.HISFC.Models.RADT.EnumPatientType.P.ToString())
                    //// || patient.PVisit.InState.ID.ToString() == "B")
                    //{
                    //    //this.myMessageBox("此患者在院治疗!", MessageBoxIcon.Warning);
                    //    //this.clear();
                    //    //return -1;

                    //    // {5B3B503C-8CF5-415b-89EB-C11A4FEE8A19}
                    //}
                    //else//以前住过院目前不在院	
                    //{
                    //    //this.myMessageBox("此住院号有上次的住院信息！", MessageBoxIcon.Information);

                    //    //清空床号
                    //    patient.PVisit.PatientLocation.Bed.ID = string.Empty;
                    //    //住院次数加1
                    //    patient.InTimes += 1;
                    //    patient.PVisit.InTime = this.inpatientManager.GetDateTimeFromSysDateTime();//住院日期
                    //    this.patientInfo = patient;
                    //    //给界面赋值
                    //    this.IRegister.SetPatientInfo(patient, true);
                    //    return 1;
                    //}
                    #endregion

                    //清空床号
                    patient.PVisit.PatientLocation.Bed.ID = string.Empty;
                    patient.InTimes += 1;
                    patient.PVisit.InTime = this.inpatientManager.GetDateTimeFromSysDateTime();//住院日期
                }
                else
                {
                    patient.InTimes = 1;
                }

                this.patientInfo = patient;
                //给界面赋值
                this.IRegister.SetPatientInfo(patient, false);
                return 1;
            }
        }

        /// <summary>
        /// 设置旧系统患者信息
        /// </summary>
        /// <param name="oldPatientInfo"></param>
        private int setOldPatientInfo(FS.HISFC.Models.RADT.PatientInfo oldPatientInfo)
        {
            int i = this.setPatientInfo(oldPatientInfo.PID.PatientNO);
            if (i == 1)
            {
                //旧系统患者住院次数+1
                oldPatientInfo.InTimes = oldPatientInfo.InTimes + 1;
                this.patientInfo = oldPatientInfo;
                this.IRegister.SetPatientInfo(oldPatientInfo, false);
            }

            return i;
        }

        private int getAutoPatientNO(ref PatientInfo patient)
        {
            patient.PatientNOType = EnumPatientNOType.First;
            string patientNO = string.Empty;
            bool isRecycle = false;
            if (Function.GetAutoPatientNO(ref patientNO, ref isRecycle) == -1)
            {
                this.myMessageBox("获得自动生成住院号出错!" + this.radtIntegrate.Err, MessageBoxIcon.Error);
                return -1;
            }
            //默认第一次入院
            patient.PID.PatientNO = patientNO;
            patient.ID = "T001";
            patient.InTimes = 1;

            patient.PID.CardNO = Function.GetCardNOByPatientNO(patient.PID.CardNO, patientNO);
            patient.InTimes = 1;//如果第一次输入则赋值为1
            return 1;
        }

        /// <summary>
        /// 更新患者信息(目前只能更新科室)
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        protected virtual int updatePatientInfo()
        {
            if (this.patientInfo == null)
            {
                this.myMessageBox("获得患者基本信息失败!", MessageBoxIcon.Warning);
                return -1;
            }

            this.patientInfo.ID = this.tempUpdatePatientID;

            //重取患者住院信息
            this.patientInfo = this.radtIntegrate.GetPatientInfomation(this.patientInfo.ID);
            //如果患者不是登记状态,不允许更改任何信息
            if (this.patientInfo.PVisit.InState.ID.ToString() != FS.HISFC.Models.Base.EnumInState.R.ToString())
            {
                this.myMessageBox("该患者在院状态已经改变, 不能进行修改!", MessageBoxIcon.Warning);
                return -1;
            }

            //保存当前得患者科室信息,后面要插入变更记录
            FS.FrameWork.Models.NeuObject oldDept = new FS.FrameWork.Models.NeuObject();
            oldDept.ID = this.patientInfo.PVisit.PatientLocation.Dept.ID;
            oldDept.Name = this.patientInfo.PVisit.PatientLocation.Dept.Name;
           // {4D7D9C06-8A39-4890-8E8E-B79ED6C9CEB6} 提前用字段记录变更病区，用对象则会跟着改变
            FS.FrameWork.Models.NeuObject oldNurseCell = new FS.FrameWork.Models.NeuObject();
            string oldnurseId = this.patientInfo.PVisit.PatientLocation.NurseCell.ID;
            string oldNurseName = this.patientInfo.PVisit.PatientLocation.NurseCell.Name;
            oldNurseCell.ID = oldnurseId; //this.patientInfo.PVisit.PatientLocation.NurseCell.ID;
            oldNurseCell.Name = oldNurseName; //this.patientInfo.PVisit.PatientLocation.NurseCell.Name;

            //获得修改后得患者基本信息
            this.patientInfo = this.IRegister.GetPatientInfo(this.patientInfo);
            if (this.patientInfo == null)
            {
                return -1;
            }

            //重新在界面上取得了住院号啦，用原来的住院号更新他
            this.patientInfo.ID = this.tempUpdatePatientID;

            if (this.ISave.Saving(FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Update, this.patientInfo) == -1)
            {
                this.myMessageBox(this.ISave.Err, MessageBoxIcon.Stop);
                return -1;
            }

            //开始数据库事务
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.radtIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //更新科室
            if (this.radtIntegrate.UpdatePatientDept(this.patientInfo) <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.myMessageBox("更新患者在院科室失败!" + this.radtIntegrate.Err, MessageBoxIcon.Warning);
                return -1;
            }


            //添加变更记录表	
            if (this.radtIntegrate.InsertShiftData(this.patientInfo.ID, FS.HISFC.Models.Base.EnumShiftType.CD, "修改科室", oldDept,
                this.patientInfo.PVisit.PatientLocation.Dept) < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.myMessageBox("添加变更记录失败!" + this.radtIntegrate.Err, MessageBoxIcon.Error);
                return -1;
            }

            //保存当前得患者科室信息,后面要插入变更记录
         

            //更新患者护士站
            if (this.radtIntegrate.UpdatePatientNurse(this.patientInfo) <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.myMessageBox("更新患者在院病区失败!" + this.radtIntegrate.Err, MessageBoxIcon.Warning);
                return -1;
            }

            //添加变转病区更记录表
            if (this.radtIntegrate.InsertShiftData(this.patientInfo.ID, FS.HISFC.Models.Base.EnumShiftType.CN, "修改病区", oldNurseCell, this.patientInfo.PVisit.PatientLocation.NurseCell) < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.myMessageBox("添加变更记录失败!" + this.radtIntegrate.Err, MessageBoxIcon.Warning);
                return -1;
            }

            if (this.ISave.SaveCommitting(FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Update, this.patientInfo) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.myMessageBox(this.ISave.Err, MessageBoxIcon.Stop);
                return -1;
            }
            //FS.SOC.HISFC.RADT.BizLogic.ComPatient patientMgr = new FS.SOC.HISFC.RADT.BizLogic.ComPatient();
            //if (patientMgr.UpdatePatientForInpatient1(this.patientInfo) <= 0)//{59E9D317-38C8-4603-B963-4FB04A522F63}更改更新操作方法
            //{
            //    FS.FrameWork.Management.PublicTrans.RollBack();
            //    this.myMessageBox("更新患者基本信息出错!" + patientMgr.Err, MessageBoxIcon.Error);
            //    return -1;
            //}
            

            ////插入变更信息
            //if (this.radtIntegrate.InsertShiftData(this.patientInfo) == -1)
            //{
            //    FS.FrameWork.Management.PublicTrans.RollBack();
            //    MessageBox.Show(Language.Msg("插入变更信息出错!") + this.radtIntegrate.Err);
            //    return -1;
            //}



            //提交数据库
            FS.FrameWork.Management.PublicTrans.Commit();
            this.myMessageBox("更新成功!", MessageBoxIcon.Information);

            if (this.ISave.Saved(FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Update, this.patientInfo) == -1)
            {
                this.myMessageBox(this.ISave.Err, MessageBoxIcon.Stop);
            }

            //刷新显示患者列表
            this.refreshPatients();
            //清屏
            this.clear();
            return 1;
        }

        /// <summary>
        /// 插入患者登记信息
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        protected virtual int insertPatientInfo()
        {

            //验证有效性,如果有不符合录入,那么中止方法
            if (!this.IRegister.IsInputValid())
            {
                return -1;
            }
            FS.HISFC.Models.RADT.Patient p1 = new Patient();
           // FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();
           //this.patientInfo  = accountMgr.GetPatientInfoByCardNO(Patient.PID.CardNO);
            this.patientInfo = this.IRegister.GetPatientInfo(this.patientInfo);
            if (this.patientInfo == null)
            {
                return -1;
            }
            //{76454122-8056-4329-A55E-0E4ADB163ABB}
            string poptext = string.Format("当前客户的入院类型为'{0}',是否继续？？", this.patientInfo.PatientType.Name);
            if (MessageBox.Show(poptext,"请确认", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return -1;

            }

            //{8809E119-3FD4-49c7-A8F5-DADA18D5B803}
            //判断在院状态
            //if (patientInfo.PVisit.InState.ID.ToString() == "R" || patientInfo.PVisit.InState.ID.ToString() == "I"
            //    || patientInfo.PVisit.InState.ID.ToString() == "P"
            //    //|| patient.PVisit.InState.ID.ToString() == "B"
            //    )
            //{
            //    if (patientInfo.PatientType.ID == "Y")
            //    {
            //        MessageBox.Show("此患者已在院治疗!");
            //        return -1;
            //    }
            //    else if (patientInfo.PatientType.ID == "L" || patientInfo.PatientType.ID == "P")
            //    {
            //        MessageBox.Show("此患者已在院治疗!");

            //        return -1;
            //    }
            //}
            //if (patientInfo.Pact.ID == "2")
            //{
            //    patientInfo.SIMainInfo.PersonType.ID = SiPatient.SIMainInfo.PersonType.ID;
            //    patientInfo.SIMainInfo.MedicalType.ID = SiPatient.SIMainInfo.MedicalType.ID;
            //    patientInfo.SIMainInfo.InDiagnose.ID = SiPatient.SIMainInfo.InDiagnose.ID;
            //}

            //如果还没有输入住院号,那么自动生成住院号
            if (string.IsNullOrEmpty(this.patientInfo.PID.PatientNO))
            {
                if (this.isAutoPatientNO)
                {
                    //如果自动生成住院号失败,那么中止方法
                    if (this.getAutoPatientNO(ref this.patientInfo) == -1)
                    {
                        return -1;
                    }
                }
                else
                {
                    this.myMessageBox("没有输入住院号!", MessageBoxIcon.Warning);
                    return -1;
                }
            }

            if (this.ISave.Saving(FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Insert, this.patientInfo) == -1)
            {
                this.myMessageBox(this.ISave.Err, MessageBoxIcon.Stop);
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            this.radtIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.inpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.managerIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            string errorInfo = string.Empty;
            //并发锁表
            if (Function.LockPatientNO(this.patientInfo.PID.PatientNO, ref errorInfo) < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.myMessageBox(errorInfo, MessageBoxIcon.Stop);
                return -1;
            }

            #region 永远不会返回 -1，且patient对象后续都没用到，冗余代码屏蔽
            //{67F0DA44-EB32-4556-96C5-88B60C59E157}
            //判断并发，如果并发
            //PatientInfo patient = new PatientInfo();

            //if (this.radtIntegrate.GetInputPatientNO(this.patientInfo.PID.PatientNO, ref patient) == -1)
            //{
            //    //如果是自动获取住院号，则再重新获取，否则，报错！
            //    if (patient != null && patient.PatientNOType == EnumPatientNOType.Second)
            //    {
            //        FS.FrameWork.Management.PublicTrans.RollBack();
            //        this.myMessageBox("此住院号正在使用或此患者正在治疗！", MessageBoxIcon.Warning);
            //        return -1;
            //    }
            //    else if (this.isAutoPatientNO)
            //    {
            //        string patientNO = string.Empty;
            //        bool isRecycle = false;
            //        if (Function.GetAutoPatientNO(ref patientNO, ref isRecycle) == -1)
            //        {
            //            FS.FrameWork.Management.PublicTrans.RollBack();
            //            this.myMessageBox("获得自动生成住院号出错!" + this.radtIntegrate.Err, MessageBoxIcon.Error);
            //            return -1;
            //        }
            //        this.patientInfo.PID.PatientNO = patientNO;
            //        this.patientInfo.PID.CardNO = Function.GetCardNOByPatientNO(this.patientInfo.PID.CardNO, patientNO);
            //    }
            //}
            #endregion 

            //获取新的住院流水号：
            this.patientInfo.ID = this.radtIntegrate.GetNewInpatientNO();


            this.patientInfo.InTimes = Function.GetMaxIntimes(this.patientInfo.PID.PatientNO);

            if (this.patientInfo.InTimes == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("获取最大住院次数出错，请检查！");
                return -1;
            }

            //{23F37636-DC34-44a3-A13B-071376265450}添加院区
            FS.HISFC.Models.Base.Employee employee = FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            FS.HISFC.Models.Base.Department dept = employee.Dept as FS.HISFC.Models.Base.Department;
            string hospital_id = dept.HospitalID;
            string hospital_name = dept.HospitalName;
            patientInfo.Hospital_id = hospital_id;
            patientInfo.Hospital_name = hospital_name;
            
            //检测住院号、住院次数重复情况，保证插入住院号的唯一性
            // 查询患者住院号，住院次数的重复情况{4949C040-E8C9-49d9-9BC2-548F7892206B}
            if (this.radtIntegrate.VerifyInpatientInTimes(this.patientInfo.PID.PatientNO, this.patientInfo.InTimes+"") > 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(Language.Msg("获取患者住院号次数重复，请刷新重新进入界面！") + this.radtIntegrate.Err);
                return -1;
            }

            //插入住院主表
            if (this.radtIntegrate.RegisterPatient(this.patientInfo) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(this.radtIntegrate.Err);
                return -1;
            }
            //如果取的是废号更新住院号标志
            if (this.radtIntegrate.UpdatePreInPatientState(this.patientInfo.PID.CardNO,"2") < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(Language.Msg("更新预约信息出错！") + this.radtIntegrate.Err);

                return -1;
            }
            //如果取的是废号更新住院号标志
            if (this.radtIntegrate.UpdatePatientNOState(this.patientInfo.PID.PatientNO) < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(Language.Msg("更新住院号状态出错！") + this.radtIntegrate.Err);

                return -1;
            }

            // 更新登记时候的血滞纳金和公费日限额和日限额累计and生育保险电脑号and日限额超标金额
            if (this.radtIntegrate.UpdateFeePatientInfoForRegister(this.patientInfo) < -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(Language.Msg("更新公费信息出错！") + this.radtIntegrate.Err);
                return -1;
            }

            //插入患者基本信息
            //if (this.radtIntegrate.RegisterComPatient(this.patientInfo) == -1)
            //{
            //    FS.FrameWork.Management.PublicTrans.RollBack();
            //    this.myMessageBox("插入患者基本信息出错!" + this.radtIntegrate.Err, MessageBoxIcon.Error);
            //    return -1;
            //}
            //插入基本表
            FS.SOC.HISFC.RADT.BizLogic.ComPatient patientMgr = new FS.SOC.HISFC.RADT.BizLogic.ComPatient();
            patientMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            FS.HISFC.Models.RADT.PatientInfo pif = accountManager.GetPatientInfoByCardNO(this.patientInfo.PID.CardNO);
            if (pif == null)// {CDB01BF4-B40F-4cdc-9F0D-23F074290136}
            {
                string strMsg = string.Empty;
                if (!string.IsNullOrEmpty(patientInfo.Name) && !string.IsNullOrEmpty(patientInfo.PhoneHome) && patientInfo.PhoneHome != "-")
                {
                    List<FS.HISFC.Models.RADT.PatientInfo> list = new List<FS.HISFC.Models.RADT.PatientInfo>();
                    list = accountManager.QueryPatientInfoByWhere("", patientInfo.Name, patientInfo.PhoneHome, "");
                    if (list != null && list.Count > 0)
                    {
                        FS.HISFC.Models.RADT.PatientInfo ac = list[0];

                        strMsg = Language.Msg(string.Format("已存在相同 【姓名】和【联系电话】,病历号为：{0} \r\n请查询原来门诊信息办理登记！", ac.PID.CardNO));

                        MessageBox.Show(strMsg);
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return -1;

                    }
                }
                //证件类型
                if (!string.IsNullOrEmpty(patientInfo.IDCard) && patientInfo.IDCard != "-")
                {
                    List<FS.HISFC.Models.RADT.PatientInfo> list = new List<FS.HISFC.Models.RADT.PatientInfo>();
                    list = accountManager.QueryPatientInfoByWhere(patientInfo.IDCard, "", "", "");
                    if (list != null && list.Count > 0)
                    {
                        FS.HISFC.Models.RADT.PatientInfo ac = list[0];

                        strMsg = Language.Msg(string.Format("已存在相同 【证件号】 ,病历号为：{0} \r\n请查询原来门诊信息办理登记！", ac.PID.CardNO));

                        MessageBox.Show(strMsg);
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return -1;

                    }

                }
            }
            int patientInsertStatus = patientMgr.InsertPatient(this.patientInfo);
            if (patientInsertStatus < 0)
            {
                //patientInfo.Patient = this.patientInfo;
                //先查询
                FS.HISFC.Models.RADT.Patient p = this.patientInfo;
                if (patientMgr.UpdatePatientForInpatient1(this.patientInfo) <= 0)//{59E9D317-38C8-4603-B963-4FB04A522F63}更改更新操作方法
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.myMessageBox("插入患者基本信息出错!" + patientMgr.Err, MessageBoxIcon.Error);
                    return -1;
                }
            }

            /*
            if (patientInsertStatus >= 0)
            {
                //若成功创建了用户的基础信息表
                //则为该用户添加一个crm基础信息
                //并且识别其家里人的渠道信息
                //并且crm自动绑his
                //his自动绑crm

                FS.SOC.HISFC.RADT.BizProcess.ComPatient.postCreateCrm(this.patientInfo);
            }
             * */

            //插入变更信息
            if (this.radtIntegrate.InsertShiftData(this.patientInfo) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(Language.Msg("插入变更信息出错!") + this.radtIntegrate.Err);
                return -1;
            }

            ////更新扩展信息
            //if (this.registerExtend != null)
            //{
            //    if (this.registerExtend.UpdateOtherInfomation(this.patientInfomation, ref this.errText) == -1)
            //    {
            //        FS.FrameWork.Management.PublicTrans.RollBack();
            //        MessageBox.Show(Language.Msg(this.errText));

            //        return -1;
            //    }
            //}

            #region 生成默认警戒线

            if (isCreateMoneyAlert)
            {
                #region 合同单位中没有Memo这字段，转成把默认警戒线放在常数表中
                /*
                FS.FrameWork.Models.NeuObject conObj = FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().GetPact(this.patientInfo.Pact.ID);          
                if (conObj == null)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.myMessageBox("获取合同单位失败！", MessageBoxIcon.Error);
                    return -1;
                }
                if (FS.FrameWork.Public.String.IsNumeric(conObj.Memo))
                {
                    this.patientInfo.PVisit.MoneyAlert = FS.FrameWork.Function.NConvert.ToDecimal(conObj.Memo);
                }
                */
                #endregion

                FS.HISFC.BizLogic.Manager.Constant conStant = new FS.HISFC.BizLogic.Manager.Constant();
                FS.FrameWork.Models.NeuObject conStantObj = null;
                if (Function.IsContainYKDept())
                {
                    conStantObj = conStant.GetConstant("YKMONEYALERT", this.patientInfo.Pact.ID);

                    if (string.IsNullOrEmpty(conStantObj.ID))
                    {
                        conStantObj = conStant.GetConstant("YKMONEYALERT", this.patientInfo.Pact.PayKind.ID);
                    }
                }
                else
                {
                    conStantObj = conStant.GetConstant("MONEYALERT", this.patientInfo.Pact.ID);

                    if (string.IsNullOrEmpty(conStantObj.ID))
                    {
                        conStantObj = conStant.GetConstant("MONEYALERT", this.patientInfo.Pact.PayKind.ID);
                    }
                }
                if (conStantObj == null)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.myMessageBox("默认警戒线没有维护，请先维护！", MessageBoxIcon.Error);
                    return -1;
                }
                if (FS.FrameWork.Public.String.IsNumeric(conStantObj.Memo))
                {
                    this.patientInfo.PVisit.MoneyAlert = FS.FrameWork.Function.NConvert.ToDecimal(conStantObj.Memo);
                }
                else
                {
                    if (Function.IsContainYKDept())
                    {
                        this.patientInfo.PVisit.MoneyAlert = -5000m;
                    }
                    else
                    {
                        this.patientInfo.PVisit.MoneyAlert = 0m;
                    }
                }
                if (this.radtIntegrate.UpdatePatientAlert(this.patientInfo.ID, patientInfo.PVisit.MoneyAlert, FS.HISFC.Models.Base.EnumAlertType.M.ToString(), DateTime.MinValue, DateTime.MinValue) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.myMessageBox("更新警戒线失败！" + radtIntegrate.Err, MessageBoxIcon.Error);
                    return -1;
                }
            }
            #endregion

            # region 如果包含接诊流程，更新床的使用状态

            if (this.isArriveProcess)
            {
                FS.HISFC.Models.Base.Bed bedObjTemp = this.patientInfo.PVisit.PatientLocation.Bed;
                FS.HISFC.Models.Base.Bed bedObj = bedObjTemp.Clone();
                bedObj.Status.User03 = bedObjTemp.Status.ID.ToString();
                bedObj.Status.ID = FS.HISFC.Models.Base.EnumBedStatus.O;
                bedObj.InpatientNO = this.patientInfo.ID;

                if (managerIntegrate.SetBed(bedObj) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("更新床位状态失败！"));
                    return -1;
                }

                if (this.radtIntegrate.InsertRecievePatientShiftData(this.patientInfo) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("插入接诊变更信息出错!") + this.radtIntegrate.Err);

                    return -1;
                }
            }
            #endregion

            #region 担保信息

            //插入担保信息
            if (this.radtIntegrate.InsertSurty(this.patientInfo) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.myMessageBox("插入担保信息出错!" + this.radtIntegrate.Err, MessageBoxIcon.Error);

                return -1;
            }

            #endregion

            #region 预交金

            //预交金实体
            FS.HISFC.Models.Fee.Inpatient.Prepay prepay = this.IRegister.GetPrepay();

            //如果没有输入预交金,那么直接返回
            if (this.patientInfo.FT.PrepayCost > 0)
            {
                string invoiceNO = null;//发票号
                prepay.FT.PrepayCost = this.patientInfo.FT.PrepayCost;
                prepay.PrepayState = "0";
                prepay.BalanceState = "0";
                prepay.BalanceNO = 0;
                prepay.TransferPrepayState = "0";
                prepay.PrepayOper.OperTime = this.inpatientManager.GetDateTimeFromSysDateTime();
                prepay.PrepayOper.ID = this.inpatientManager.Operator.ID;
                invoiceNO = this.feeIntegrate.GetNewInvoiceNO("P");
                if (invoiceNO == null || invoiceNO == string.Empty)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.myMessageBox("获得预交金票据号失败!" + this.feeIntegrate.Err, MessageBoxIcon.Error);
                    return -1;
                }
                prepay.RecipeNO = invoiceNO;
                if (this.inpatientManager.PrepayManager(this.patientInfo, prepay) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.myMessageBox("插入预交金失败" + this.inpatientManager.Err, MessageBoxIcon.Error);
                    return -1;
                }
            }

            #endregion

            #region 医保接口


            long returnValue = 0;

            medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            returnValue = medcareInterfaceProxy.SetPactCode(this.patientInfo.Pact.ID);
            medcareInterfaceProxy.Trans = FS.FrameWork.Management.PublicTrans.Trans;
            if (returnValue != 1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                medcareInterfaceProxy.Rollback();
                this.myMessageBox("待遇接口获得合同单位失败!" + medcareInterfaceProxy.ErrMsg, MessageBoxIcon.Error);
                return -1;
            }
            returnValue = medcareInterfaceProxy.Connect();
            if (returnValue != 1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                medcareInterfaceProxy.Rollback();
                this.myMessageBox("待遇接口初始化失败" + medcareInterfaceProxy.ErrMsg, MessageBoxIcon.Error);
                return -1;
            }
            returnValue = medcareInterfaceProxy.UploadRegInfoInpatient(this.patientInfo);
            if (returnValue != 1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                medcareInterfaceProxy.Rollback();
                this.myMessageBox("待遇接口住院登记失败" + medcareInterfaceProxy.ErrMsg, MessageBoxIcon.Error);
                return -1;
            }

            medcareInterfaceProxy.Commit();
            returnValue = medcareInterfaceProxy.Disconnect();

            #endregion

            if (this.ISave.SaveCommitting(FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Insert, this.patientInfo) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.myMessageBox(this.ISave.Err, MessageBoxIcon.Warning);
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            if (this.ISave.Saved(FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Insert, this.patientInfo) == -1)
            {
                this.myMessageBox(this.ISave.Err, MessageBoxIcon.Warning);
            }
            
            string employeeId = employee.ID;

            //{11E484F5-B92A-4903-8C8A-4920908B4D0A}
            if (patientInsertStatus >= 0)
            {
                //{1EC9ECE5-0E63-4073-B653-E668031E2DF1}
                FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
                accountCard.Patient.PID.CardNO=patientInfo.PID.CardNO;
                accountCard.MarkNO=patientInfo.PID.CardNO;
                accountCard.MarkType.ID = "Card_No";
                accountCard.MarkStatus = FS.HISFC.Models.Account.MarkOperateTypes.Begin;
                accountCard.ReFlag="0";
                accountCard.CreateOper.ID = employeeId;
                accountCard.CreateOper.OperTime = DateTime.Now;
                accountCard.SecurityCode="";

                int status= accountManager.InsertAccountCardEX(accountCard);


                //若成功创建了用户的基础信息表
                //则为该用户添加一个crm基础信息
                //并且识别其家里人的渠道信息
                //并且crm自动绑his
                //his自动绑crm

                FS.SOC.HISFC.RADT.BizProcess.ComPatient.postCreateCrm(this.patientInfo);
            }

            this.myMessageBox("登记成功!住院号是：" + this.patientInfo.PID.PatientNO, MessageBoxIcon.Information);

            #region 住院登记数据统计节点业务
            //住院登记数据统计节点是否启用-CPP003
            if (this.IsInpatientRegisterStatisticsPointInUse)
            {
                FS.HISFC.BizProcess.Interface.StatisticsPoint.IStatisticsPoint iStatistics = new FS.HISFC.BizProcess.Integrate.StatisticsPoint.InpatientRegisterStatisticsPoint();
                //HISFC.Models.RADT.PatientInfo patientInfo = radtProcess.QueryComPatientInfo(regObj.PID.CardNO);
                iStatistics.SetPatient(patientInfo);
                iStatistics.DoStatistics();
            }
            #endregion


            //添加广州医保匹配
            if (Function.GetRegInfoInpatient(this.patientInfo, ref errorInfo) <= 0)
            {
                this.myMessageBox("匹配医保信息失败，" + errorInfo, MessageBoxIcon.Warning);
            }
            this.refreshPatients();
            //if (string.IsNullOrEmpty(this.patientInfo.SIMainInfo.RegNo) && (inpatientManager.IsGzSI(this.patientInfo.Pact.ID) ))
            //{
            //    if (DialogResult.OK == MessageBox.Show("请先去医保客户端进行医保登记，再点击【确认】！","提示",MessageBoxButtons.OKCancel))
            //    {
            //        FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //        medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //        returnValue = medcareInterfaceProxy.SetPactCode(this.patientInfo.Pact.ID);
            //        medcareInterfaceProxy.Trans = FS.FrameWork.Management.PublicTrans.Trans;
            //        if (returnValue != 1)
            //        {
            //            FS.FrameWork.Management.PublicTrans.RollBack();
            //            medcareInterfaceProxy.Rollback();
            //            this.myMessageBox("待遇接口获得合同单位失败!" + medcareInterfaceProxy.ErrMsg, MessageBoxIcon.Error);
            //            return -1;
            //        }
            //        returnValue = medcareInterfaceProxy.Connect();
            //        if (returnValue != 1)
            //        {
            //            FS.FrameWork.Management.PublicTrans.RollBack();
            //            medcareInterfaceProxy.Rollback();
            //            this.myMessageBox("待遇接口初始化失败！" + medcareInterfaceProxy.ErrMsg, MessageBoxIcon.Error);
            //            return -1;
            //        }
            //        returnValue = medcareInterfaceProxy.GetRegInfoInpatient(this.patientInfo);
            //        if (returnValue != 1)
            //        {
            //            FS.FrameWork.Management.PublicTrans.RollBack();
            //            medcareInterfaceProxy.Rollback();
            //            this.myMessageBox("匹配广州医保患者信息失败！" + medcareInterfaceProxy.ErrMsg, MessageBoxIcon.Error);
            //            return -1;
            //        }

            //        FS.FrameWork.Management.PublicTrans.Commit();
            //        this.myMessageBox("匹配广州医保患者信息成功!", MessageBoxIcon.Information);
            //    }
            //}

            if (isPrintBracelet)
            {
                if (MessageBox.Show("是否打印腕带？", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    ucPatientBracelet uc = new ucPatientBracelet();
                    uc.myPatientInfo = patientInfo;
                    uc.Print();
                }
            }

            this.clear();
            return 1;
        }

        private int save()
        {
            if (this.isModify)
            {
                return this.updatePatientInfo();
            }
            else
            {
                return this.insertPatientInfo();
            }
        }   

        private int queryPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo, bool isQueryShow)
        {
            if (patientInfo == null)// {5B3B503C-8CF5-415b-89EB-C11A4FEE8A19}
            {
                this.IQueryPatientInfo.Clear();
                this.IQueryPatientInfo.Show(this);
                return 1;
            }
            this.IQueryPatientInfo.Clear();
            this.IQueryPatientInfo.PatientInfo = patientInfo;

            this.IQueryPatientInfo.IsFiltTemPatient = this.isFiltTemPatient;
            if (isQueryShow == true && this.IQueryPatientInfo.Query(FS.SOC.HISFC.RADT.Interface.Patient.EnumQueryType.NameSex) == 1)
            {
                this.IQueryPatientInfo.Show(this);
            }

            if (!this.IQueryPatientInfo.IsSelect)
            {
                return 0;
            }

            //{67F0DA44-EB32-4556-96C5-88B60C59E157}
            return this.setPatientInfo(this.IQueryPatientInfo.PatientInfo.PID.PatientNO);
            //if (this.IQueryPatientInfo.IsOldSystem == false)
            //{
            //    return this.setPatientInfo(this.IQueryPatientInfo.PatientInfo.PID.PatientNO);
            //}
            //else
            //{
            //    return this.setOldPatientInfo(this.IQueryPatientInfo.PatientInfo);
            //}
        }

        #endregion

        #region 事件

        protected override void OnLoad(EventArgs e)
        {
            this.initInterface();

            this.refreshPatients();

            base.OnLoad(e);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            return save();
        }

        /// <summary>
        /// 选择患者
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        private int IRegisterListInterface_OnSelectPatientInfo(FS.HISFC.Models.RADT.PatientInfo pInfo)
        {
            this.clear();

            if (pInfo == null)
            {
                this.myMessageBox("没有选择患者，请先选择患者", MessageBoxIcon.Warning);
                return -1;
            }
            //重新查询患者信息

            if (pInfo == null || string.IsNullOrEmpty(pInfo.ID))
            {
                return -1;
            }

            pInfo = this.radtIntegrate.GetPatientInfomation(pInfo.ID);
            if (pInfo == null)
            {
                this.myMessageBox("获取患者住院信息失败！", MessageBoxIcon.Warning);
                return -1;
            }
            if (pInfo.PVisit.InState.ID.ToString() != FS.HISFC.Models.Base.EnumInState.R.ToString())
            {
                this.myMessageBox("该患者不是住院登记状态, 不能进行修改!", MessageBoxIcon.Warning);
                return -1;
            }

            this.IRegister.ModifyPatientInfo(pInfo);
            this.isModify = true;
            this.patientInfo = pInfo;
            this.tempUpdatePatientID = pInfo.ID;

            return 1;
        }

        private void IRegisterListInterface_OnRefresh(object sender, EventArgs e)
        {
            this.refreshPatients();
        }

        private void IRegisterInterface_OnSavePatientInfo(object sender, EventArgs e)
        {
            this.save();
        }

        /// <summary>
        /// 查询患者信息
        /// </summary>
        /// <param name="patientInfo"></param>
        private int IRegisterInterface_OnQueryPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            if (string.IsNullOrEmpty(patientInfo.Name))
            {
                return -1;
            }
            string numberCheck = "0123456789";
            if (!numberCheck.Contains(patientInfo.Name.Substring(0, 1)))
            {
                return this.queryPatientInfo(patientInfo, true);
            }

            return 0;
        }


        private int QueryZhuHaiSI()
        {
            string path = @"ZhuHaiSI.dll";
            string strType = "frmInpatientRegZhuHai";
            Form frm = null;
            if (this.LoadSiQueryForm(ref frm, path, strType) < 1)
            {
                MessageBox.Show("加载珠海医保查询功能失败，请退出程序重试！", "提示");
                return -1;
            }

            FS.HISFC.Models.RADT.PatientInfo patientInfo = new PatientInfo();
            try
            {
                this.IRegister.GetPatientInfo(patientInfo);
            }
            catch (Exception)
            {
            }


            string oldText = frm.Text;
            frm.Text = patientInfo.Name + "|" + patientInfo.SSN + "|" + patientInfo.IDCard;
            frm.Text = oldText;

            frm.ShowDialog();

            return 1;
        }

        private int QueryZhongShanSI()
        {
            string path = @"ZhongShanSI.dll";
            string strType = "frmInpatientRegZhongShan";
            Form frm = null;
            if (this.LoadSiQueryForm(ref frm, path, strType) < 1)
            {
                MessageBox.Show("加载中山医保查询功能失败，请退出程序重试！", "提示");
                return -1;
            }

            FS.HISFC.Models.RADT.PatientInfo patientInfo = new PatientInfo();
            try
            {
                this.IRegister.GetPatientInfo(patientInfo);
            }
            catch (Exception)
            {
            }


            string oldText = frm.Text;
            frm.Text = patientInfo.Name + "|" + patientInfo.SSN + "|" + patientInfo.IDCard;
            frm.Text = oldText;

            frm.ShowDialog();

            return 1;
        }

        private int LoadSiQueryForm(ref Form frm, string filePath, string strType)
        {
            string path = @".\Plugins\SI\" + filePath;
            Assembly ab = Assembly.LoadFrom(path);

            object myObject = null;
            //获取程序集类型
            //Type newObject = ab.GetType(nameSpaceClass);
            foreach (Type item in ab.GetExportedTypes())
            {
                if (item.Name == strType)
                {
                    //创建动态加载程序集的实例 
                    myObject = Activator.CreateInstance(item);
                    break;
                }
            }

            if (myObject != null)
            {
                frm = myObject as Form;

                return 1;
            }
            else
            {
                return -1;
            }
        }
        #endregion

        #region ISIReadCard 成员

        public int ReadCard()
        {
            //{FF419F26-D52C-404b-84BF-47A509BF5782} 读卡前先清空一下
            clear();
            FS.HISFC.Models.RADT.PatientInfo SiPatient = new PatientInfo();
            SiPatient.Pact.ID = defaultPactCode;
            Base.Inpatient.frmSiRegisterInfo frmSiRegisterInfo = new FS.SOC.Local.RADT.ZhuHai.ZDWY.Base.Inpatient.frmSiRegisterInfo();
            frmSiRegisterInfo.Init();
            frmSiRegisterInfo.DefaultPact = SiPatient.Pact.ID;
            frmSiRegisterInfo.ShowDialog(this);
            if (!frmSiRegisterInfo.IsOK)
            {
                return -1;
            }
            SiPatient.Pact.ID = frmSiRegisterInfo.DefaultPact;
            string errText = "";
            if (Function.GetRegInfoInpatient(SiPatient, ref errText) < 0 && !string.IsNullOrEmpty(errText))
            {
                this.myMessageBox(errText, MessageBoxIcon.Warning);
                return -1;
            }

            this.SetSIPatientInfo(SiPatient);
            return 1;
        }

        public int SetSIPatientInfo(FS.HISFC.Models.RADT.PatientInfo SiPatient)
        {
            ArrayList alPatientInfo = new ArrayList();
            FS.HISFC.BizProcess.Integrate.RADT radt = new FS.HISFC.BizProcess.Integrate.RADT();
            FS.HISFC.Models.RADT.PatientInfo p = null;

            alPatientInfo = radt.PatientQueryByMcardNO(SiPatient.SSN);
            if (alPatientInfo != null && alPatientInfo.Count != 0)
            {
                p = (FS.HISFC.Models.RADT.PatientInfo)alPatientInfo[0];
                SiPatient.PID.CardNO = p.PID.CardNO;
                SiPatient.PhoneHome = p.PhoneHome;
                SiPatient.AddressHome = p.AddressHome;
                SiPatient.PID.PatientNO = p.PID.PatientNO;
                SiPatient.Birthday = p.Birthday;
                SiPatient.IDCard = p.IDCard;
                SiPatient.IDCardType = p.IDCardType;
                SiPatient.Card.CardType = p.Card.CardType;
                SiPatient.Card.ID = p.Card.ID;
                SiPatient.Card.ICCard = p.Card.ICCard;

                SiPatient.SIMainInfo.PersonType.ID = SiPatient.SIMainInfo.PersonType.ID;
                SiPatient.SIMainInfo.MedicalType.ID = SiPatient.SIMainInfo.MedicalType.ID;
                SiPatient.SIMainInfo.InDiagnose.ID = SiPatient.SIMainInfo.InDiagnose.ID;
            }
            else
            {
                //this.getAutoPatientNO(ref SiPatient);
            }
            this.patientInfo = SiPatient;
            this.IRegister.SetPatientInfo(SiPatient, false);

            return 1;
        }

        #endregion

        private void gbQuery_Enter(object sender, EventArgs e)
        {

        }
    }
}
