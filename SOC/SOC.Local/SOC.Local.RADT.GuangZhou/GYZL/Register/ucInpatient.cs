using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;
using FS.FrameWork.WinForms.Forms;
using FS.HISFC.Models.RADT;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using FS.SOC.HISFC.BizProcess.CommonInterface.Attributes;
using FS.SOC.Local.RADT.GuangZhou.GYZL.Interface;

namespace FS.SOC.Local.RADT.GuangZhou.GYZL.Register
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
        }

        #region 变量

        /// <summary>
        /// 患者入出转
        /// </summary>
        private FS.HISFC.BizLogic.Fee.InPatient inpatientManager = new FS.HISFC.BizLogic.Fee.InPatient();

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
        /// 列表 入院登记接口
        /// </summary>
        private FS.SOC.HISFC.RADT.Interface.Register.IPatientList IRegisterList = null;

        /// <summary>
        /// 弹出入院登记接口
        /// </summary>
        private FS.SOC.HISFC.RADT.Interface.Register.IInpatient IRegister = null;

        private FS.SOC.HISFC.RADT.Interface.Patient.IQuery IQueryPatientInfo = null;

        private FS.SOC.HISFC.BizProcess.CommonInterface.Common.ISave<PatientInfo> ISave = null;

        private FS.HISFC.Models.RADT.PatientInfo patientInfo = new PatientInfo();

        private bool isModify = false;
        private string tempUpdatePatientID;
        private string defaultPactCode = "2";
        private bool isAllowModifyInDate = false;
        #endregion

        #region 属性
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
            toolBarService.AddToolButton("刷新", "刷新患者信息", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S刷新, true, false, null);
            toolBarService.AddToolButton("医保", "获取医保患者", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Y医保, true, false, null);
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
                    break;
                case "患者查询":
                    this.queryPatientInfo(new PatientInfo(),false);
                    break;
                case "医保":
                    this.ReadCard();
                    break;
                    
                default :
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        #endregion

        #region 方法

        /// <summary>
        /// 提示方法
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="msgIcon"></param>
        private void myMessageBox(string msg, MessageBoxIcon msgIcon)
        {
            CommonController.CreateInstance().MessageBox(this, msg, MessageBoxButtons.OK, msgIcon);
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
            this.IRegister.IsCanModifyInTime = isAllowModifyInDate;
            this.IRegister.FileName = FS.FrameWork.WinForms.Classes.Function.SettingPath + "\\SOC.Components.Radt.ucRegisterInfo.xml";

          

            if (this.IRegisterList is Control)
            {
                this.gbPatientList.Controls.Clear();
                //加载界面
                ((Control)this.IRegisterList).Dock = DockStyle.Fill;
                this.gbPatientList.Controls.Add((Control)this.IRegisterList);
            }

            if (this.IRegister is Control)
            {
                Control c=(Control)this.IRegister;
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
            this.patientInfo = null;
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
                if (patient.PatientNOType== EnumPatientNOType.Second)
                {
                    //判断在院状态
                    if (patient.PVisit.InState.ID.ToString() == FS.HISFC.Models.RADT.EnumPatientType.R.ToString()
                       || patient.PVisit.InState.ID.ToString() == FS.HISFC.Models.RADT.EnumPatientType.I.ToString()
                       || patient.PVisit.InState.ID.ToString() == FS.HISFC.Models.RADT.EnumPatientType.P.ToString())
                    // || patient.PVisit.InState.ID.ToString() == "B")
                    {
                        this.myMessageBox("此患者在院治疗!", MessageBoxIcon.Warning);
                        this.clear();
                        return -1;
                    }
                    else//以前住过院目前不在院	
                    {
                        this.myMessageBox("此住院号有上次的住院信息！", MessageBoxIcon.Information);
                        //清空床号
                        patient.PVisit.PatientLocation.Bed.ID = string.Empty;
                        //住院次数加1
                        patient.InTimes += 1;
                        patient.PVisit.InTime = this.inpatientManager.GetDateTimeFromSysDateTime();//住院日期
                        this.patientInfo = patient;
                        //给界面赋值
                        this.IRegister.SetPatientInfo(patient, false);
                        return 1;
                    }
                }
            }



            return 1;
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
            if (this.radtIntegrate.GetAutoPatientNO(ref patientNO, ref isRecycle) == -1)
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
            FS.FrameWork.Models.NeuObject oldNurseCell = new FS.FrameWork.Models.NeuObject();
            oldNurseCell.ID = this.patientInfo.PVisit.PatientLocation.NurseCell.ID;
            oldNurseCell.Name = this.patientInfo.PVisit.PatientLocation.NurseCell.Name;

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
            this.patientInfo = this.IRegister.GetPatientInfo(this.patientInfo);
            if (this.patientInfo == null)
            {
                return -1;
            }
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

            //判断并发，如果并发
            PatientInfo patient = new PatientInfo();

            if (this.radtIntegrate.GetInputPatientNO(this.patientInfo.PID.PatientNO, ref patient) == -1)
            {
                //如果是自动获取住院号，则再重新获取，否则，报错！
                if (patient != null && patient.PatientNOType == EnumPatientNOType.Second)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.myMessageBox("此住院号正在使用或此患者正在治疗！", MessageBoxIcon.Warning);
                    return -1;
                }
                else if (this.isAutoPatientNO)
                {
                    string patientNO = string.Empty;
                    bool isRecycle = false;
                    if (this.radtIntegrate.GetAutoPatientNO(ref patientNO, ref isRecycle) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.myMessageBox("获得自动生成住院号出错!" + this.radtIntegrate.Err, MessageBoxIcon.Error);
                        return -1;
                    }
                    this.patientInfo.PID.PatientNO = patientNO;
                    this.patientInfo.PID.CardNO = Function.GetCardNOByPatientNO(this.patientInfo.PID.CardNO,patientNO);
                }
            }

            //获取新的住院流水号：
            this.patientInfo.ID = this.radtIntegrate.GetNewInpatientNO();

            //插入住院主表
            if (this.radtIntegrate.RegisterPatient(this.patientInfo) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(this.radtIntegrate.Err);
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

            if (patientMgr.InsertPatient(this.patientInfo) < 0)
            {
                //先查询
                if (patientMgr.UpdatePatientForInpatient(this.patientInfo)<= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.myMessageBox("插入患者基本信息出错!" + patientMgr.Err, MessageBoxIcon.Error);
                    return -1;
                }
            }

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
                FS.FrameWork.Models.NeuObject conStantObj = conStant.GetConstant("MONEYALERT", this.patientInfo.Pact.PayKind.ID);
                if (conStantObj==null)
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
                    this.patientInfo.PVisit.MoneyAlert = 0m;
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
                this.myMessageBox("待遇接口初始化失败"+ medcareInterfaceProxy.ErrMsg, MessageBoxIcon.Error);
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

            this.myMessageBox("登记成功!住院号是：" + this.patientInfo.PID.PatientNO, MessageBoxIcon.Information);
            this.refreshPatients();


            //添加广州医保匹配
            if (this.patientInfo.Pact.PayKind.ID == "02" && string.IsNullOrEmpty(this.patientInfo.SIMainInfo.RegNo))
            {
                if (DialogResult.OK == MessageBox.Show("请先去医保客户端进行医保登记，再点击【确认】！","提示",MessageBoxButtons.OKCancel))
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();

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
                        this.myMessageBox("待遇接口初始化失败！" + medcareInterfaceProxy.ErrMsg, MessageBoxIcon.Error);
                        return -1;
                    }
                    returnValue = medcareInterfaceProxy.GetRegInfoInpatient(this.patientInfo);
                    if (returnValue != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        medcareInterfaceProxy.Rollback();
                        this.myMessageBox("匹配广州医保患者信息失败！" + medcareInterfaceProxy.ErrMsg, MessageBoxIcon.Error);
                        return -1;
                    }

                    FS.FrameWork.Management.PublicTrans.Commit();
                    this.myMessageBox("匹配广州医保患者信息成功!", MessageBoxIcon.Information);
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

        private int queryPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo,bool isQueryShow)
        {
            this.IQueryPatientInfo.Clear();
            this.IQueryPatientInfo.PatientInfo = patientInfo;
            if (isQueryShow == false || this.IQueryPatientInfo.Query(FS.SOC.HISFC.RADT.Interface.Patient.EnumQueryType.NameSex) != -1)
            {
                this.IQueryPatientInfo.Show(this);
            }

            if (!this.IQueryPatientInfo.IsSelect)
            {
                return 0;
            }

            if (this.IQueryPatientInfo.IsOldSystem == false)
            {
                return this.setPatientInfo(this.IQueryPatientInfo.PatientInfo.PID.PatientNO);
            }
            else
            {
                return this.setOldPatientInfo(this.IQueryPatientInfo.PatientInfo);
            }
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
                return this.queryPatientInfo(patientInfo,true);
            }

            return 0;
        }

        #endregion

        #region ISIReadCard 成员

        public int ReadCard()
        {
            //{FF419F26-D52C-404b-84BF-47A509BF5782} 读卡前先清空一下
            clear();
         FS.HISFC.Models.RADT.PatientInfo SiPatient = new PatientInfo();
         SiPatient.Pact.ID = defaultPactCode;
         Base.Inpatient.frmSiRegisterInfo frmSiRegisterInfo = new FS.SOC.Local.RADT.GuangZhou.GYZL.Base.Inpatient.frmSiRegisterInfo();
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
            if (alPatientInfo != null&&alPatientInfo.Count != 0 )
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
    }
}
