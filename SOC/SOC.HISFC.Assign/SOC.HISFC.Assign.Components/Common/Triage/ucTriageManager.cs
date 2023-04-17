using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.SOC.HISFC.Assign.Interface.Components;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using System.Collections;
using FS.SOC.HISFC.Assign.Components.Common.Patient;
using FS.SOC.HISFC.Assign.Components.Base;

namespace FS.SOC.HISFC.Assign.Components.Common.Triage
{
    /// <summary>
    /// [功能描述: 门诊分诊]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    public partial class ucTriageManager : ucAssignBase, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucTriageManager()
        {
            InitializeComponent();
        }

        #region 域变量

        /// <summary>
        /// 待分诊患者
        /// </summary>
        private ITriaggingPatient ITriaggingPatient = null;

        /// <summary>
        /// 已分诊患者
        /// </summary>
        private ITriaggedPatient ITriageWaiting = null;

        /// <summary>
        /// 已进诊患者
        /// </summary>
        private ITriaggedPatient ITriageCalling = null;

        /// <summary>
        /// 已到诊患者
        /// </summary>
        private ITriaggedPatient ITriageArrive = null;

        /// <summary>
        /// 已看诊患者
        /// </summary>
        private ITriaggedPatient ITriageSee = null;

        /// <summary>
        /// 未看诊患者列表
        /// </summary>
        private ITriaggedPatient ITriageNoSee = null;

        /// <summary>
        /// 患者信息
        /// </summary>
        private IPatientInfo ITriagePatientInfo = null;

        /// <summary>
        /// 分诊队列信息
        /// </summary>
        private ITriageQueue ITriageQueue = null;

        /// <summary>
        /// 分诊屏显示接口
        /// </summary>
        private IAssignDisplay IAssignDisplay = null;

        /// <summary>
        /// 挂号信息修改
        /// </summary>
        private FS.SOC.HISFC.Assign.Interface.Components.IMaintenance<FS.HISFC.Models.Registration.Register> IRegPatient = null;

        /// <summary>
        /// 对已分诊的患者进行加锁，避免多线程同时进行资源的访问
        /// </summary>
        private object lockTriagged = new object();
        private object lockTriagging = new object();
        private object lockTriageQueue = new object();

        /// <summary>
        /// 分诊，进诊界面
        /// </summary>
        ucPatientTriage ucPatientTriage = null;
        ucPatientInRoom ucPatientInRoom = null;
        frmSelectRegister ucShow = null;

        /// <summary>
        /// 记录最后一次刷新的队列列表，用于取消进诊时判定队列的属性。
        /// </summary>
        /// {d4ea07b4-2559-4473-ac92-f8076d9ce3b4}
        private ArrayList currentRefleshQueueList = null;
        #endregion

        #region 设置变量及属性
        /// <summary>
        /// 时间间隔
        /// 保留加载非当天处方的功能
        /// </summary>
        private uint daySpan = 0;

        /// <summary>
        /// 刷新开始间隔与今天的间隔天数
        /// 保留加载非当天处方的功能
        /// </summary>
        [Description("刷新开始间隔与今天的间隔天数"), Category("设置"), Browsable(true)]
        public uint DaySpan
        {
            get
            {
                return daySpan;
            }
            set
            {
                daySpan = value;
            }
        }

        private bool isChooseDeptWhenCheckPrive = false;

        /// <summary>
        /// 权限检测时是否需要用户选择科室
        /// </summary>
        [Description("权限检测时是否需要用户选择科室"), Category("设置"), Browsable(true)]
        public bool IsChooseDeptWhenCheckPrive
        {
            get { return isChooseDeptWhenCheckPrive; }
            set { isChooseDeptWhenCheckPrive = value; }
        }

        private string trigeWhereFlag = "1";

        [Description("自动分诊的方式：1分诊到队列；2分诊到诊台"), Category("设置"), Browsable(true)]
        public string TrigeWhereFlag
        {
            get { return trigeWhereFlag; }
            set { trigeWhereFlag = value; }
        }

        private Function.EnumAssignOperType enumAssignOperType = Function.EnumAssignOperType.空闲;

        /// <summary>
        /// 操作状态
        /// 对于空闲状态才可以进行下一步操作
        /// </summary>
        [Description("操作状态"), Category("非设置"), Browsable(false)]
        public Function.EnumAssignOperType EnumAssignOperType
        {
            get
            {
                return enumAssignOperType;

            }
            set
            {
                enumAssignOperType = value;

                try
                {
                    ((FS.FrameWork.WinForms.Forms.frmBaseForm)this.FindForm()).statusBar1.Panels[1].Text = "           状态：" + value.ToString() + "中...        ";
                }
                catch { }
            }
        }

        /// <summary>
        /// 是否使用终端自动打印注册功能
        /// </summary>
        private bool isRegisterAutoPrint = true;

        /// <summary>
        /// 是否使用终端自动打印注册功能
        /// </summary>
        [Description("是否使用终端自动打印注册功能"), Category("设置"), Browsable(true)]
        public bool IsRegisterAutoPrint
        {
            get { return isRegisterAutoPrint; }
            set { isRegisterAutoPrint = value; }
        }

        #endregion

        #region 综合变量
        private FS.SOC.HISFC.Assign.BizProcess.Assign assignBiz = new FS.SOC.HISFC.Assign.BizProcess.Assign();
        private FS.SOC.HISFC.Assign.BizProcess.Call callBiz = new FS.SOC.HISFC.Assign.BizProcess.Call();
        private FS.SOC.HISFC.Assign.BizProcess.Queue queueBiz = new FS.SOC.HISFC.Assign.BizProcess.Queue();
        private FS.SOC.HISFC.Assign.BizProcess.OutPatientInfo outPatientInfoBiz = new FS.SOC.HISFC.Assign.BizProcess.OutPatientInfo();
        #endregion

        #region 工具栏

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("分诊", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Z执行, true, false, null);
            this.toolBarService.AddToolButton("取消分诊", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q取消, true, false, null);
            this.toolBarService.AddToolButton("刷新", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S刷新, true, false, null);
            this.toolBarService.AddToolButton("进诊", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X下一个, true, false, null);
            this.toolBarService.AddToolButton("取消进诊", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S上一个, true, false, null);
            this.toolBarService.AddToolButton("暂停", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Z注销, true, false, null);
            this.toolBarService.AddToolButton("打印标签", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            this.toolBarService.AddToolButton("屏显（开）", "", 0, true, false, null);

            this.toolBarService.AddToolButton("叫号", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B报警, true, false, null);
            this.toolBarService.AddToolButton("修改患者信息", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.H换医师, true, false, null);
            this.toolBarService.AddToolButton("成人腕带", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            this.toolBarService.AddToolButton("小孩腕带", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            this.toolBarService.AddToolButton("护理评估", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X修改, true, false, null);


            this.toolBarService.AddToolButton("取消到诊", "取消到诊状态", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);

            return this.toolBarService;
        }

        /// <summary>
        /// 菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {
                case "分诊":
                    this.triage();
                    break;
                case "取消分诊":
                    this.cancelTriage();
                    break;
                case "刷新":
                    this.EnumAssignOperType = Function.EnumAssignOperType.刷新;
                    try
                    {
                        this.Refresh();
                    }
                    finally
                    {
                        this.EnumAssignOperType = Function.EnumAssignOperType.空闲;
                    }
                    break;
                case "进诊":
                    this.In();
                    break;
                case "取消进诊":
                    this.cancelIn();
                    break;
                case "屏显（开）":
                    this.screen(true);
                    break;
                case "屏显（关）":
                    this.screen(false);
                    break;
                case "叫号":
                    this.callIn();
                    break;
                case "修改患者信息":
                    this.modifyPaitentInfo();
                    break;
                case "打印标签":
                    this.PrintBarCode();
                    break;
                case "成人腕带":
                    this.PrintEmergencyCode("1");
                    break;
                case "小孩腕带":
                    this.PrintEmergencyCode("0");
                    break;
                case "护理评估":
                    this.EditNurseCase();
                    break;
                case "取消到诊":
                    this.CancleArrive();
                    break;
                default:
                    break;
            }
        }


        #region 当前选中的患者
        private FS.HISFC.Models.Registration.Register currentRegister = null;
        #endregion


        /// <summary>
        /// 取消到诊状态
        /// </summary>
        private void CancleArrive()
        {
            if (this.currentRegister == null)
            {
                MessageBox.Show("当前未选中患者！");
                return;
            }

            string clinicCode = currentRegister.ID;

            FS.HISFC.BizProcess.Integrate.Manager managerAssign = new FS.HISFC.BizProcess.Integrate.Manager();
            FS.HISFC.Models.Nurse.Assign assign = managerAssign.QueryAssignByClinicID(clinicCode);

            //未找到分诊信息，直接退出
            if (assign == null)
            {
                MessageBox.Show("当前选中患者不是分诊患者！" + managerAssign.Err);
                return;
            }
            else if (assign.TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.Arrive)
            {
                if (MessageBox.Show("是否确认取消患者【" + assign.Register.Name + "】到诊状态？\r\n\r\n错误操作可能会导致医生看诊出现错误！", "警告", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }

                string errinfo = string.Empty;
                int rtn = 0;

                FS.SOC.HISFC.Assign.BizLogic.Assign assignLogic = new FS.SOC.HISFC.Assign.BizLogic.Assign();

                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                //已经叫过号的 更新为进诊状态
                if (!string.IsNullOrEmpty(assign.Queue.SRoom.ID))
                {
                    rtn = assignLogic.Update(FS.HISFC.Models.Nurse.EnuTriageStatus.In, clinicCode, false, false, assign.Queue.SRoom, assign.Queue.Console, assign.Queue.Doctor.ID);
                }
                //未叫过号的，更新为分诊状态
                else
                {
                    string sql = @"UPDATE met_nuo_assignrecord
                                   SET in_date = null,
                                       assign_flag = '1',
                                       oper_code='{1}',
                                       oper_date=sysdate
                                 WHERE clinic_code = '{0}'
                                   AND assign_flag in ( '4')";

                    sql = string.Format(sql, clinicCode, assignLogic.Operator.ID);

                    rtn = assignLogic.ExecNoQuery(sql);

                    //rtn = assignLogic.Update(FS.HISFC.Models.Nurse.EnuTriageStatus.Triage, currentRegister.ID, false, false, assign.Queue.SRoom, assign.Queue.Console, assign.Queue.Doctor.ID);
                }
                if (rtn == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(errinfo, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("处理成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                this.Refresh();
                return;
            }
            else
            {
                MessageBox.Show("当前患者不是到诊状态，无法继续操作！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void EditNurseCase()
        {
            //if (this.currentRegister == null)
            //{
            //    MessageBox.Show("当前未选中患者，无法编辑护理评估单！");
            //    return;
            //}

            //FS.SOC.HISFC.BizProcess.EMRInterface.Interface.INurseCase nurseCase = InterfaceManager.GetINurseCase();
            //nurseCase.EditNurseCase(this.currentRegister);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patientType">0小孩腕带；1 成人腕带;</param>
        private void PrintEmergencyCode(string patientType)
        {
            FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamManager = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

            bool isNewPrintType = controlParamManager.GetControlParam<bool>("BH0002", true, false);

            if (!isNewPrintType)
            {
                //FS.SOC.Local.Assign.ShenZhen.BinHai.PrintCode.ucPatientCodePrint patientCodePrint = new FS.SOC.Local.Assign.ShenZhen.BinHai.PrintCode.ucPatientCodePrint();
                //patientCodePrint.PatientType = patientType;

                if (currentRegister == null)
                {
                    MessageBox.Show("当前未选中患者！");
                }
                //patientCodePrint.PrintBar(currentRegister);
            }
            else
            {
                #region 新的打印腕带

                if (patientType == "1")
                {
                    //SOC.Local.Assign.ShenZhen.BinHai.PrintCode.NewBB.ucPrintCode patientBarCode = new FS.SOC.Local.Assign.ShenZhen.BinHai.PrintCode.NewBB.ucPrintCode();
                    //patientBarCode.PatientType = patientType;

                    //if (currentRegister == null)
                    //{
                    //    MessageBox.Show("当前未选中患者！");
                    //}
                    //patientBarCode.PrintBar(currentRegister, patientType);
                }
                else
                {
                    //SOC.Local.Assign.ShenZhen.BinHai.PrintCode.NewBB.ucPrintCodeForBaby patientbabyBarCode = new FS.SOC.Local.Assign.ShenZhen.BinHai.PrintCode.NewBB.ucPrintCodeForBaby();
                    //patientbabyBarCode.PatientType = patientType;

                    //if (currentRegister == null)
                    //{
                    //    MessageBox.Show("当前未选中患者！");
                    //}
                    //patientbabyBarCode.PrintBar(currentRegister, patientType);
                }
                #endregion
            }
        }

        #endregion

        #region 初始化
        public void Init()
        {
            this.initInterface();
            this.initDatas();
            this.initEvents();
            this.initDateTime();

            this.Refresh();
        }

        /// <summary>
        /// 初始化接口信息
        /// </summary>
        /// <returns></returns>
        private int initInterface()
        {
            IAssignDisplay = InterfaceManager.GetIAssignDisplay();

            #region IRegPatient
            IRegPatient = InterfaceManager.GetIRegPatient();
            this.IRegPatient.Init();
            this.IRegPatient.Clear();
            #endregion

            #region ITriagePatientInfo
            ITriagePatientInfo = InterfaceManager.GetITriagePatientInfo();
            if (this.ITriagePatientInfo is Control)
            {
                this.plPatientInfo.Controls.Clear();
                //加载界面
                ((Control)this.ITriagePatientInfo).Dock = DockStyle.Fill;
                this.plPatientInfo.Controls.Add((Control)this.ITriagePatientInfo);
            }
            this.ITriagePatientInfo.Clear();

            #endregion

            #region ITriaggingPatient
            ITriaggingPatient = InterfaceManager.GetITriaggingPatient();
            if (this.ITriaggingPatient is Control)
            {
                this.plTriaggingPatient.Controls.Clear();
                //加载界面
                ((Control)this.ITriaggingPatient).Dock = DockStyle.Fill;
                this.plTriaggingPatient.Controls.Add((Control)this.ITriaggingPatient);
            }

            ITriaggingPatient.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.None;
            this.ITriaggingPatient.Init();
            this.ITriaggingPatient.Clear();
            #endregion

            #region ITriageNoSee

            ITriageNoSee = InterfaceManager.GetITriageNoSee();
            if (this.ITriageNoSee is Control)
            {
                this.plNoseePatient.Controls.Clear();
                ((Control)this.ITriageNoSee).Dock = DockStyle.Fill;
                this.plNoseePatient.Controls.Add((Control)this.ITriageNoSee);
            }

            this.ITriageNoSee.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.Delay;
            this.ITriageNoSee.Init();
            this.ITriageNoSee.Clear();

            #endregion

            #region ITriageQueue

            ITriageQueue = InterfaceManager.GetITriageQueue();
            if (this.ITriageQueue is Control)
            {
                this.plTriggedQueue.Controls.Clear();
                ((Control)this.ITriageQueue).Dock = DockStyle.Fill;
                this.plTriggedQueue.Controls.Add((Control)this.ITriageQueue);
            }

            this.ITriageQueue.Init();
            this.ITriageQueue.Clear();

            #endregion

            #region ITriageCalling

            ITriageCalling = InterfaceManager.GetITriageCalling();
            if (this.ITriageCalling is Control)
            {
                this.plTriggedCalling.Controls.Clear();
                ((Control)this.ITriageCalling).Dock = DockStyle.Fill;
                this.plTriggedCalling.Controls.Add((Control)this.ITriageCalling);
            }

            this.ITriageCalling.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.In;
            this.ITriageCalling.Init();
            this.ITriageCalling.Clear();

            #endregion

            #region ITriageWaiting

            ITriageWaiting = InterfaceManager.GetITriageWaiting();
            if (this.ITriageWaiting is Control)
            {
                this.plTriggedWaiting.Controls.Clear();
                ((Control)this.ITriageWaiting).Dock = DockStyle.Fill;
                this.plTriggedWaiting.Controls.Add((Control)this.ITriageWaiting);
            }

            this.ITriageWaiting.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.Triage;
            this.ITriageWaiting.Init();
            this.ITriageWaiting.Clear();
            #endregion

            #region ITriageArrive

            ITriageArrive = InterfaceManager.GetITriageArrive();
            if (this.ITriageArrive is Control)
            {
                this.plTriggedArrive.Controls.Clear();
                ((Control)this.ITriageArrive).Dock = DockStyle.Fill;
                this.plTriggedArrive.Controls.Add((Control)this.ITriageArrive);
            }

            this.ITriageArrive.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.Arrive;
            this.ITriageArrive.Init();
            this.ITriageArrive.Clear();

            #endregion

            #region ITriageSee
            ITriageSee = InterfaceManager.GetITriageSee();

            if (this.ITriageSee is Control)
            {
                this.plTriggedSee.Controls.Clear();
                //加载界面
                ((Control)this.ITriageSee).Dock = DockStyle.Fill;
                this.plTriggedSee.Controls.Add((Control)this.ITriageSee);
            }
            this.ITriageSee.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.Out;
            this.ITriageSee.Init();
            this.ITriageSee.Clear();
            #endregion

            return 1;
        }

        /// <summary>
        /// 初始化事件
        /// </summary>
        /// <returns></returns>
        private int initEvents()
        {
            #region ITriaggingPatient

            //选择
            this.ITriaggingPatient.SelectedItemChange -= new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.HISFC.Models.Registration.Register>(ITriaggingPatient_SelectedRegisterChange);
            this.ITriaggingPatient.SelectedItemChange += new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.HISFC.Models.Registration.Register>(ITriaggingPatient_SelectedRegisterChange);
            //拖拽
            this.ITriaggingPatient.DragDrop -= new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Assign>(ITriaggingPatient_DragDrop);
            this.ITriaggingPatient.DragDrop += new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Assign>(ITriaggingPatient_DragDrop);

            this.ITriaggingPatient.DragDropRegister -= new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.HISFC.Models.Registration.Register>(ITriageNoSee_DragDrop);
            this.ITriaggingPatient.DragDropRegister += new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.HISFC.Models.Registration.Register>(ITriageNoSee_DragDrop);
            #endregion

            #region ITriageWaiting
            this.ITriageWaiting.SelectedItemChange -= new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Assign>(ITriageWaiting_SelectedAssignChange);
            this.ITriageWaiting.SelectedItemChange += new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Assign>(ITriageWaiting_SelectedAssignChange);
            //拖拽
            this.ITriageWaiting.DragDropAssign -= new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Assign>(ITriageWaiting_DragDrop);
            this.ITriageWaiting.DragDropAssign += new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Assign>(ITriageWaiting_DragDrop);
            //拖拽
            this.ITriageWaiting.DragDropRegister -= new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.HISFC.Models.Registration.Register>(ITriageWaiting_DragDrop);
            this.ITriageWaiting.DragDropRegister += new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.HISFC.Models.Registration.Register>(ITriageWaiting_DragDrop);
            #endregion

            #region ITriageWaiting
            this.ITriageSee.SelectedItemChange -= new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Assign>(ITriageWaiting_SelectedAssignChange);
            this.ITriageSee.SelectedItemChange += new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Assign>(ITriageWaiting_SelectedAssignChange);
            #endregion

            #region ITriageCalling
            this.ITriageCalling.SelectedItemChange -= new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Assign>(ITriageWaiting_SelectedAssignChange);
            this.ITriageCalling.SelectedItemChange += new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Assign>(ITriageWaiting_SelectedAssignChange);
            //拖拽
            this.ITriageCalling.DragDropAssign -= new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Assign>(ITriageCalling_DragDrop);
            this.ITriageCalling.DragDropAssign += new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Assign>(ITriageCalling_DragDrop);
            //拖拽
            this.ITriageCalling.DragDropRegister -= new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.HISFC.Models.Registration.Register>(ITriageCalling_DragDrop);
            this.ITriageCalling.DragDropRegister += new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.HISFC.Models.Registration.Register>(ITriageCalling_DragDrop);
            #endregion

            #region ITriageArrive
            this.ITriageArrive.SelectedItemChange -= new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Assign>(ITriageWaiting_SelectedAssignChange);
            this.ITriageArrive.SelectedItemChange += new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Assign>(ITriageWaiting_SelectedAssignChange);
            #endregion

            #region ITriageQueue
            this.ITriageQueue.SelectedQueueChange -= new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Queue>(ITriageQueue_SelectedQueueChange);
            this.ITriageQueue.SelectedQueueChange += new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Queue>(ITriageQueue_SelectedQueueChange);

            //拖拽
            this.ITriageQueue.DragDropAssign -= new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Assign>(ITriageQueue_DragDrop);
            this.ITriageQueue.DragDropAssign += new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Assign>(ITriageQueue_DragDrop);
            //拖拽
            this.ITriageQueue.DragDropRegister -= new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.HISFC.Models.Registration.Register>(ITriageQueue_DragDrop);
            this.ITriageQueue.DragDropRegister += new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.HISFC.Models.Registration.Register>(ITriageQueue_DragDrop);
            #endregion

            #region ITriageNoSee

            //选择
            this.ITriageNoSee.SelectedItemChange -= new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Assign>(ITriageWaiting_SelectedAssignChange);
            this.ITriageNoSee.SelectedItemChange += new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Assign>(ITriageWaiting_SelectedAssignChange);

            this.ITriageNoSee.DragDropAssign -= new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Assign>(ITriageNoSee_DragDrop);
            this.ITriageNoSee.DragDropAssign += new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Assign>(ITriageNoSee_DragDrop);

            this.ITriageNoSee.DragDropRegister -= new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.HISFC.Models.Registration.Register>(ITriageNoSee_DragDrop);
            this.ITriageNoSee.DragDropRegister += new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.HISFC.Models.Registration.Register>(ITriageNoSee_DragDrop);
            #endregion

            #region IRegPatient

            this.IRegPatient.SaveInfo -= new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.HISFC.Models.Registration.Register>(IRegPatient_SaveRegister);
            this.IRegPatient.SaveInfo += new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.HISFC.Models.Registration.Register>(IRegPatient_SaveRegister);

            #endregion

            //自动分诊注册
            this.ckAutoTriage.CheckedChanged -= new EventHandler(ckAutoTriage_CheckedChanged);
            this.ckAutoTriage.CheckedChanged += new EventHandler(ckAutoTriage_CheckedChanged);

            //刷新时间
            this.cmbRefreshTime.SelectedIndexChanged -= new EventHandler(cmbRefreshTime_SelectedIndexChanged);
            this.cmbRefreshTime.SelectedIndexChanged += new EventHandler(cmbRefreshTime_SelectedIndexChanged);

            this.lbPrivNurse.DoubleClick -= new EventHandler(lbPrivNurse_DoubleClick);
            this.lbPrivNurse.DoubleClick += new EventHandler(lbPrivNurse_DoubleClick);

            this.txtCardNO.KeyDown -= new KeyEventHandler(txtCardNO_KeyDown);
            this.txtCardNO.KeyDown += new KeyEventHandler(txtCardNO_KeyDown);

            this.notifyIcon1.MouseClick -= new MouseEventHandler(notifyIcon1_MouseClick);
            this.notifyIcon1.MouseClick += new MouseEventHandler(notifyIcon1_MouseClick);

            return 1;
        }

        /// <summary>
        /// 初始化时间
        /// </summary>
        /// <returns></returns>
        private int initDateTime()
        {
            try
            {
                //从零点开始刷新
                this.autoRefreshBeginTime = CommonController.CreateInstance().GetSystemTime().Date.AddDays(-this.DaySpan);
            }
            catch (Exception ex)
            {
                CommonController.CreateInstance().MessageBox(this, "初始化失败刷新开始时间失败：" + ex.Message, MessageBoxIcon.Error);
                this.autoRefreshBeginTime = DateTime.Now.Date;
            }

            return 1;
        }

        /// <summary>
        /// 初始化队列
        /// </summary>
        /// <returns></returns>
        private int initQueue()
        {
            lock (this.lockTriageQueue)
            {
                DateTime current = CommonController.CreateInstance().GetSystemTime();
                string error = "";
                this.currentRefleshQueueList = queueBiz.QueryQueue(this.priveNurse.ID, current, this.priveDept.ID, out error);
                ArrayList alQueueNum = queueBiz.QueryQueueData(this.priveNurse.ID, current, this.priveDept.ID, out error);
                this.ITriageQueue.AddQueue(this.currentRefleshQueueList);
                this.ITriageQueue.AddQueueNum(alQueueNum);
            }

            return 1;
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <returns></returns>
        private int initDatas()
        {
            ArrayList al = new ArrayList(new FS.FrameWork.Models.NeuObject[] { 
            new FS.FrameWork.Models.NeuObject("10", "10秒", ""),
            new FS.FrameWork.Models.NeuObject("20", "20秒", ""),
            new FS.FrameWork.Models.NeuObject("30", "30秒", ""),
            new FS.FrameWork.Models.NeuObject("45", "45秒", ""),
            new FS.FrameWork.Models.NeuObject("60", "60秒", "")
            });

            this.cmbRefreshTime.AddItems(al);

            return 1;
        }
        #endregion

        #region 方法

        protected override void OnLoad(EventArgs e)
        {
            this.Init();
            base.OnLoad(e);
            this.txtCardNO.Select();
            this.txtCardNO.Focus();
            this.BeginAutoRefresh();
        }

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }


            if (this.autoRefreshTimer != null)
            {
                this.autoRefreshTimer.Dispose();
            }

            if (this.ITriagePatientInfo != null)
            {
                this.ITriagePatientInfo.Dispose();
            }

            if (this.ITriageArrive != null)
            {
                this.ITriageArrive.Dispose();
            }

            if (this.ITriageCalling != null)
            {
                this.ITriageCalling.Dispose();
            }

            if (this.ITriageQueue != null)
            {
                this.ITriageQueue.Dispose();
            }

            if (this.ITriageSee != null)
            {
                this.ITriageSee.Dispose();
            }

            if (this.ITriageWaiting != null)
            {
                this.ITriageWaiting.Dispose();
            }

            if (this.ITriaggingPatient != null)
            {
                this.ITriaggingPatient.Dispose();
            }

            if (this.IAssignDisplay != null)
            {
                this.IAssignDisplay.Dispose();
            }

            if (this.ucShow != null)
            {
                this.ucShow.Close();
                this.ucShow.Dispose();
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// 刷新
        /// </summary>
        public override void Refresh()
        {
            this.refreshTriaggingPatient();
            this.refreshTriageNoSeePatient();
            this.initQueue();
            this.refreshTriaggedPatient(this.ITriageQueue.Queue);
        }

        /// <summary>
        /// 自动刷新
        /// </summary>
        public void AutoRefresh()
        {
            //if (this.EnumAssignOperType != Function.EnumAssignOperType.空闲)
            //{
            //    this.showStatusBarTip("正在" + this.EnumAssignOperType.ToString() + ",本次自动刷新已取消");
            //    return;
            //}

            this.EnumAssignOperType = Function.EnumAssignOperType.自动分诊;

            try
            {
                this.Refresh();

                if (this.ckPauseRefresh.Checked)
                {
                    return;
                }

                if (this.ckAutoTriage.Checked)
                {
                    this.autoTriage();
                }

                //处理急诊科半夜隔日队列看不到患者的情况
                if (CommonController.CreateInstance().GetSystemTime().Date != this.autoRefreshBeginTime.Date)
                {
                    initDateTime();
                }
            }
            finally
            {
                this.EnumAssignOperType = Function.EnumAssignOperType.空闲;
            }
        }

        /// <summary>
        /// 刷新待分诊的患者信息
        /// </summary>
        /// <returns></returns>
        private int refreshTriaggingPatient()
        {
            lock (this.lockTriagging)
            {
                ArrayList alTriaggingPatient = null;
                string error = "";
                if (this.priveDept.ID.Equals(this.priveNurse.ID))
                {
                    alTriaggingPatient = this.outPatientInfoBiz.QueryNoTriagebyNurse(this.autoRefreshBeginTime, this.priveNurse.ID, ref error);
                }
                else
                {
                    alTriaggingPatient = this.outPatientInfoBiz.QueryNoTriagebyDept(this.autoRefreshBeginTime, this.priveDept.ID, ref error);
                }

                if (alTriaggingPatient != null)
                {
                    this.ITriaggingPatient.AddRange(alTriaggingPatient);

                    //上面固定查询待分诊列表已经是昨天的了，所以下面的没有必要再处理了
                    //this.setAutoRefreshBeginTime(alTriaggingPatient);
                }
            }

            return 1;
        }

        /// <summary>
        /// 刷新未看诊患者
        /// </summary>
        /// <returns></returns>
        private int refreshTriageNoSeePatient()
        {
            //查询候诊患者
            string error = "";
            ArrayList alNoSeePatient = this.assignBiz.QueryNoSee(this.priveNurse.ID, this.autoRefreshBeginTime.Date, CommonController.CreateInstance().GetNoonID(CommonController.CreateInstance().GetSystemTime()), ref error);
            if (alNoSeePatient == null)
            {
                this.showStatusBarTip(error);
            }

            this.ITriageNoSee.AddRange(alNoSeePatient);

            return 1;
        }

        /// <summary>
        /// 刷新已分诊的患者信息
        /// </summary>
        /// <param name="queueID"></param>
        /// <returns></returns>
        private int refreshTriaggedPatient(FS.SOC.HISFC.Assign.Models.Queue queue)
        {
            lock (this.lockTriagged)
            {
                if (queue == null || string.IsNullOrEmpty(queue.ID))
                {
                    this.ITriageWaiting.Clear();
                    this.ITriageCalling.Clear();
                    this.ITriageArrive.Clear();
                    this.ITriageSee.Clear();
                }
                else
                {
                    this.refreshTriaggedPatient(this.ITriageWaiting, queue.ID);
                    this.refreshTriaggedPatient(this.ITriageCalling, queue.ID);
                    this.refreshTriaggedPatient(this.ITriageArrive, queue.ID);
                    this.refreshTriaggedPatient(this.ITriageSee, queue.ID);
                }
            }

            return 1;
        }

        /// <summary>
        /// 刷新已分诊的患者信息
        /// </summary>
        /// <param name="TriaggedPatient"></param>
        /// <param name="queueID"></param>
        /// <param name="triageStatus"></param>
        /// <returns></returns>
        private int refreshTriaggedPatient(ITriaggedPatient TriaggedPatient, string queueID)
        {
            //查询候诊患者
            string error = "";
            ArrayList alTriaggedPatient = this.assignBiz.QueryAssign(this.priveNurse.ID, this.autoRefreshBeginTime.Date, queueID, TriaggedPatient.TriageStatus, ref error);
            if (alTriaggedPatient == null)
            {
                this.showStatusBarTip(error);
            }

            TriaggedPatient.AddRange(alTriaggedPatient);
            #region 全部队列信息

            #endregion
            return 1;
        }

        /// <summary>
        /// 根据挂号信息设置自动刷新的开始时间
        /// 获取本次信息中挂号时间最小的作为开始时间然后回溯
        /// </summary>
        /// <param name="alRegInfo"></param>
        private void setAutoRefreshBeginTime(ArrayList alRegInfo)
        {
            //获取本次信息中挂号时间最小的作为开始时间，如果是取消分诊如何呢？
            foreach (FS.HISFC.Models.Registration.Register register in alRegInfo)
            {
                if (this.autoRefreshBeginTime > register.DoctorInfo.SeeDate)
                {
                    this.autoRefreshBeginTime = register.DoctorInfo.SeeDate;
                }
            }
        }

        /// <summary>
        /// 分诊
        /// </summary>
        private void triage()
        {
            FS.HISFC.Models.Registration.Register register = this.ITriaggingPatient.SelectItem == null ? this.ITriaggingPatient.FirstItem : this.ITriaggingPatient.SelectItem;

            this.triage(register, null);

        }

        /// <summary>
        /// 分诊未分诊过的患者
        /// </summary>
        /// <param name="register"></param>
        /// <param name="queue"></param>
        private FS.SOC.HISFC.Assign.Models.Assign triage(
            FS.HISFC.Models.Registration.Register register,
            FS.SOC.HISFC.Assign.Models.Queue queue)
        {
            if (register == null)
            {
                return null;
            }
            lock (this.lockTriagging)
            {
                this.EnumAssignOperType = Function.EnumAssignOperType.手工分诊;

                try
                {
                    //防止并发
                    if (this.outPatientInfoBiz.JudgeTrige(register.ID))
                    {
                        this.refreshTriaggingPatient();
                        CommonController.CreateInstance().MessageBox(this, "该患者已经分诊!", MessageBoxIcon.Asterisk);
                        return null;
                    }
                    //防止已经退号
                    if (this.outPatientInfoBiz.Judgeback(register.ID))
                    {
                        this.refreshTriaggingPatient();
                        CommonController.CreateInstance().MessageBox(this, "该患者已经退号!", MessageBoxIcon.Asterisk);
                        return null;
                    }
                    //初始化队列
                    DateTime current = CommonController.CreateInstance().GetSystemTime();
                    string error = "";

                    bool isOK = false;
                    FS.SOC.HISFC.Assign.Models.Assign assign = new FS.SOC.HISFC.Assign.Models.Assign();
                    if (queue == null)
                    {
                        ArrayList alQueue = queueBiz.QueryQueue(this.priveNurse.ID, current, this.priveDept.ID, out error);
                        if (alQueue == null || alQueue.Count <= 0)
                        {
                            CommonController.CreateInstance().MessageBox(this, "没有维护队列!", MessageBoxIcon.Asterisk);
                            return null;
                        }
                        //初始化界面
                        if (ucPatientTriage == null)
                        {
                            ucPatientTriage = new ucPatientTriage();
                        }
                        ucPatientTriage.PriveNurse = this.PriveDept.Clone();
                        ucPatientTriage.Clear();
                        ucPatientTriage.Init(alQueue);//重新初始化队列信息

                        ucPatientTriage.Register = register;
                        FS.FrameWork.WinForms.Classes.Function.ShowControl(ucPatientTriage);

                        assign = this.ucPatientTriage.Assign;
                        if (assign != null)
                        {
                            queue = assign.Queue as FS.SOC.HISFC.Assign.Models.Queue;
                            this.ITriageQueue.Queue = queue;
                        }
                        isOK = this.ucPatientTriage.IsOK;
                    }
                    else
                    {
                        assign.Register = register;
                        assign.Queue = queue;

                        isOK = true;
                    }

                    if (isOK)
                    {
                        //如果当前队列科室与挂号科室不相同，进行提示
                        //防止已经退号
                        if (!queue.AssignDept.ID.Equals(assign.Register.DoctorInfo.Templet.Dept.ID))
                        {
                            if (CommonController.CreateInstance().MessageBox(this, "挂号科室与分诊科室不相同，是否继续？", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                return null;
                            }
                        }
                        assign.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.Triage;
                        assign.TriageDept = this.priveNurse.ID;
                        assign.TirageTime = CommonController.CreateInstance().GetSystemTime();
                        assign.Oper.OperTime = assign.TirageTime;
                        assign.Oper.ID = FS.FrameWork.Management.Connection.Operator.ID;
                        //      assign.Queue.AssignDept = assign.Register.DoctorInfo.Templet.Dept;

                        if (this.assignBiz.Triage(assign, false, false, false, ref error) == -1)
                        {
                            CommonController.CreateInstance().MessageBox(this, error, MessageBoxIcon.Error);
                            return null;
                        }

                        //刷新
                        if (this.ITriageQueue.Queue != null && this.ITriageQueue.Queue.ID.Equals(assign.Queue.ID))
                        {
                            this.ITriageWaiting.Add(assign);
                        }
                        this.ITriaggingPatient.Remove(register);
                        this.ITriageNoSee.Remove(assign);
                        this.initQueue();

                    }


                    return assign;
                }
                catch (Exception e)
                {
                    this.showStatusBarTip("分诊失败，原因：" + e.Message);
                    return null;
                }
                finally
                {
                    this.EnumAssignOperType = Function.EnumAssignOperType.空闲;
                }
            }

        }

        /// <summary>
        /// 分诊已分诊的患者 - 为暂不看诊变化为分诊状态时使用。
        /// 更新时要根据队列类别，清空医师、诊室信息。
        /// </summary>
        /// <param name="assign"></param>
        /// <param name="queue"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        /// {7a47cdce-65fc-442d-81fa-f2a597af3e3a}
        private FS.SOC.HISFC.Assign.Models.Assign triageFromDelay(
           FS.SOC.HISFC.Assign.Models.Assign assign,
           FS.SOC.HISFC.Assign.Models.Queue queue,
           FS.HISFC.Models.Nurse.EnuTriageStatus status)
        {
            bool isClearDoctor = false;
            bool isClearRoom = false;
            this.GetChangeToTriageClearInfo(assign.Queue.ID, out isClearDoctor, out isClearRoom);

            return triageCore(assign, queue, status, isClearDoctor, isClearRoom);
        }

        /// <summary>
        /// 分诊已分诊的患者 - 为普通的操作时使用。
        /// </summary>
        /// <param name="assign"></param>
        /// <param name="queue"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        /// {7a47cdce-65fc-442d-81fa-f2a597af3e3a}
        private FS.SOC.HISFC.Assign.Models.Assign triageForCommon(
           FS.SOC.HISFC.Assign.Models.Assign assign,
           FS.SOC.HISFC.Assign.Models.Queue queue,
           FS.HISFC.Models.Nurse.EnuTriageStatus status)
        {
            return triageCore(assign, queue, status, false, false);
        }


        /// <summary>
        /// 分诊已分诊的患者
        /// </summary>
        /// <param name="assign"></param>
        /// <param name="queue"></param>
        /// <param name="status"></param>
        /// <param name="isClearDoctorInfo">当“待看诊(Delay)”改变为“分诊(Triage)”状态时，依据所属的队列设置值.</param>
        /// <param name="isClearRoomInfo">当“待看诊(Delay)”改变为“分诊(Triage)”状态时，依据所属的队列设置值.</param>
        /// <returns></returns>
        private FS.SOC.HISFC.Assign.Models.Assign triageCore(
            FS.SOC.HISFC.Assign.Models.Assign assign,
            FS.SOC.HISFC.Assign.Models.Queue queue,
            FS.HISFC.Models.Nurse.EnuTriageStatus status,
            bool isClearDoctorInfo,
            bool isClearRoomInfo)
        {
            if (assign == null || queue == null)
            {
                return null;
            }
            lock (this.lockTriagging)
            {
                this.EnumAssignOperType = Function.EnumAssignOperType.手工分诊;

                try
                {
                    //防止已经退号
                    if (this.outPatientInfoBiz.Judgeback(assign.Register.ID))
                    {
                        this.refreshTriaggingPatient();
                        CommonController.CreateInstance().MessageBox(this, "该患者已经退号!", MessageBoxIcon.Asterisk);
                        return null;
                    }
                    string error = "";

                    bool isOK = false;
                    if (queue == null)
                    {
                        DateTime current = CommonController.CreateInstance().GetSystemTime();
                        ArrayList alQueue = queueBiz.QueryQueue(this.priveNurse.ID, current, this.priveDept.ID, out error);
                        if (alQueue == null || alQueue.Count <= 0)
                        {
                            CommonController.CreateInstance().MessageBox(this, "没有维护队列!", MessageBoxIcon.Asterisk);
                            return null;
                        }
                        //初始化界面
                        if (ucPatientTriage == null)
                        {
                            ucPatientTriage = new ucPatientTriage();
                            ucPatientTriage.Init(alQueue);
                        }
                        ucPatientTriage.PriveNurse = this.PriveDept.Clone();
                        ucPatientTriage.Clear();

                        ucPatientTriage.Register = assign.Register;
                        FS.FrameWork.WinForms.Classes.Function.ShowControl(ucPatientTriage);

                        if (this.ucPatientTriage.Assign != null)
                        {
                            assign.Queue = this.ucPatientTriage.Assign.Queue;
                            queue = assign.Queue as FS.SOC.HISFC.Assign.Models.Queue;
                            this.ITriageQueue.Queue = queue;
                        }
                        isOK = this.ucPatientTriage.IsOK;
                    }
                    else
                    {
                        assign.Queue = queue;

                        isOK = true;
                    }

                    if (isOK)
                    {
                        //如果当前队列科室与挂号科室不相同，进行提示
                        //防止已经退号
                        if (!queue.AssignDept.ID.Equals(assign.Register.DoctorInfo.Templet.Dept.ID))
                        {
                            if (CommonController.CreateInstance().MessageBox(this, "挂号科室与分诊科室不相同，是否继续？", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                return null;
                            }
                        }
                        assign.TriageStatus = status;
                        assign.TriageDept = this.priveNurse.ID;
                        assign.TirageTime = CommonController.CreateInstance().GetSystemTime();
                        assign.Oper.OperTime = assign.TirageTime;
                        assign.Oper.ID = FS.FrameWork.Management.Connection.Operator.ID;
                        //  assign.Queue.AssignDept = assign.Register.DoctorInfo.Templet.Dept;

                        // {7a47cdce-65fc-442d-81fa-f2a597af3e3a}
                        if (this.assignBiz.Triage(
                            assign, status == FS.HISFC.Models.Nurse.EnuTriageStatus.Triage,
                            isClearDoctorInfo,
                            isClearRoomInfo,
                            ref error) == -1)
                        {
                            CommonController.CreateInstance().MessageBox(this, error, MessageBoxIcon.Error);
                            return null;
                        }

                        //刷新
                        if (this.ITriageQueue.Queue != null && this.ITriageQueue.Queue.ID.Equals(assign.Queue.ID))
                        {
                            if (status == FS.HISFC.Models.Nurse.EnuTriageStatus.Triage)
                            {
                                this.ITriageWaiting.Add(assign);
                            }
                            else if (status == FS.HISFC.Models.Nurse.EnuTriageStatus.In)
                            {
                                this.ITriageCalling.Add(assign);
                            }
                        }
                        this.ITriaggingPatient.Remove(assign.Register);
                        this.ITriageNoSee.Remove(assign);
                        this.initQueue();

                    }

                    return assign;
                }
                catch (Exception e)
                {
                    this.showStatusBarTip("分诊失败，原因：" + e.Message);
                    return null;
                }
                finally
                {
                    this.EnumAssignOperType = Function.EnumAssignOperType.空闲;
                }
            }

        }

        /// <summary>
        /// 进行自动分诊
        /// </summary>
        /// <param name="alRegInfo"></param>
        private void autoTriage()
        {
            while (this.ITriaggingPatient.FirstItem != null)
            {
                if (this.autoTriage(this.ITriaggingPatient.FirstItem, this.trigeWhereFlag) <= 0)
                {
                    break;
                }
            }
            this.initQueue();
        }

        /// <summary>
        /// 单个自动分诊
        /// </summary>
        /// <param name="register"></param>
        /// <param name="alQueue"></param>
        /// <param name="trigeWhereFlag"></param>
        private int autoTriage(FS.HISFC.Models.Registration.Register register, string trigeWhereFlag)
        {

            if (register == null)
            {
                return -1;
            }

            if (string.IsNullOrEmpty(trigeWhereFlag))
            {
                trigeWhereFlag = this.trigeWhereFlag;
            }

            lock (this.lockTriagging)
            {
                string error = "";
                FS.SOC.HISFC.Assign.Models.Assign assign = new FS.SOC.HISFC.Assign.Models.Assign();
                if (this.assignBiz.AutoTriage(this.priveNurse, CommonController.CreateInstance().GetSystemTime(), register, this.trigeWhereFlag, ref assign, ref error) <= 0)
                {
                    this.ShowBalloonTip(error);
                    return -1;
                }
                lock (this.lockTriagged)
                {
                    //刷新
                    if (this.ITriageQueue.Queue != null && this.ITriageQueue.Queue.ID.Equals(assign.Queue.ID))
                    {
                        if (this.trigeWhereFlag.Equals("2"))
                        {
                            this.ITriageCalling.Add(assign);
                        }
                        else
                        {
                            this.ITriageWaiting.Add(assign);
                        }
                    }
                }

                this.ITriaggingPatient.Remove(this.ITriaggingPatient.FirstItem);
            }

            return 1;
        }

        /// <summary>
        /// 取消分诊
        /// </summary>
        private void cancelTriage()
        {
            FS.SOC.HISFC.Assign.Models.Assign assign = this.ITriageWaiting.SelectItem == null ? this.ITriageWaiting.FirstItem : this.ITriageWaiting.SelectItem;
            if (assign != null)
            {
                // 判定在已到诊、已看诊状态下不允许取消进诊、取消分诊、暂不看诊。{b763437e-95d7-4ffd-bd3b-96de355ecff9}
                if (!this.ValidateCancelInOrTriageOrDelay(assign))
                {
                    return;
                }

                if (assign.TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.Triage)
                {
                    if (CommonController.CreateInstance().MessageBox(this, "是否要取消该分诊信息?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    } 
                }
            }
            this.cancelTriage(assign);
        }

        /// <summary>
        /// 判定在已到诊、已看诊状态下不允许取消进诊、取消分诊、暂不看诊。
        /// </summary>
        /// <param name="assign"></param>
        /// {b763437e-95d7-4ffd-bd3b-96de355ecff9}
        private bool ValidateCancelInOrTriageOrDelay(FS.SOC.HISFC.Assign.Models.Assign assign)
        {
            if (assign == null)
            {
                return true;
            }

            // 重新获取分诊对象，判定在已到诊、已看诊状态下不允许取消进诊。
            FS.SOC.HISFC.Assign.BizLogic.Assign assignLogic = new FS.SOC.HISFC.Assign.BizLogic.Assign();
            FS.SOC.HISFC.Assign.Models.Assign findAssign = assignLogic.QueryByClinicID(assign.Register.ID);
            if (findAssign == null ||
                findAssign.TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.Arrive ||
                findAssign.TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.Out)
            {
                CommonController.CreateInstance().MessageBox("患者的分诊信息发生变化，请刷新数据！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return false;
            }

            return true;
        }

        /// <summary>
        /// 取消分诊
        /// </summary>
        /// <param name="assign"></param>
        private void cancelTriage(FS.SOC.HISFC.Assign.Models.Assign assign)
        {
            if (assign == null)
            {
                return;
            }

            lock (this.lockTriagged)
            {
                this.EnumAssignOperType = Function.EnumAssignOperType.手工分诊;

                try
                {
                    string error = "";

                    if (this.assignBiz.CancelTriage(assign, ref error) == -1)
                    {
                        CommonController.CreateInstance().MessageBox(this, error, MessageBoxIcon.Error);
                        return;
                    }
                    FS.HISFC.Models.Registration.Register reigster = this.outPatientInfoBiz.Get(assign.Register.ID, ref error);
                    if (reigster != null && this.ITriageQueue.Queue != null)
                    {
                        //刷新
                        if (this.ITriaggingPatient.Add(reigster) > 0)
                        {
                            this.ITriageWaiting.Remove(assign);
                            this.ITriageNoSee.Remove(assign);
                            this.initQueue();
                        }
                    }
                }
                catch (Exception e)
                {
                    this.showStatusBarTip("取消分诊失败，原因：" + e.Message);
                }
                finally
                {
                    this.EnumAssignOperType = Function.EnumAssignOperType.空闲;
                }

            }
        }

        /// <summary>
        /// 进诊
        /// </summary>
        private void In()
        {
            FS.SOC.HISFC.Assign.Models.Assign assign = this.ITriageWaiting.SelectItem == null ? this.ITriageWaiting.FirstItem : this.ITriageWaiting.SelectItem;
            this.In(assign, null);
        }

        /// <summary>
        /// 进诊
        /// </summary>
        /// <param name="assign"></param>
        private void In(FS.SOC.HISFC.Assign.Models.Assign assign, FS.SOC.HISFC.Assign.Models.Queue queue)
        {
            if (assign == null)
            {
                return;
            }
            lock (this.lockTriagged)
            {
                this.EnumAssignOperType = Function.EnumAssignOperType.手工分诊;

                try
                {
                    //防止已经退号
                    if (this.outPatientInfoBiz.Judgeback(assign.Register.ID))
                    {
                        MessageBox.Show("该患者已经退号!请取消其分诊信息");
                        return;
                    }

                    bool isOK = false;

                    if (queue == null)
                    {
                        if (this.ucPatientInRoom == null)
                        {
                            this.ucPatientInRoom = new ucPatientInRoom();
                            this.ucPatientInRoom.Init(this.priveNurse.ID);
                        }

                        ucPatientInRoom.PriveNurse = this.PriveDept.Clone();
                        ucPatientInRoom.Clear();
                        this.ucPatientInRoom.Assign = assign;

                        FS.FrameWork.WinForms.Classes.Function.ShowControl(ucPatientInRoom);
                        assign = this.ucPatientInRoom.Assign;
                        if (assign != null)
                        {
                            queue = assign.Queue as FS.SOC.HISFC.Assign.Models.Queue;
                            this.ITriageQueue.Queue = queue;
                        }
                        isOK = this.ucPatientInRoom.IsOK;
                    }
                    else
                    {
                        assign.Queue = queue;

                        isOK = true;
                    }

                    if (isOK)
                    {
                        assign.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.In;
                        string error = "";
                        if (this.assignBiz.In(assign, assign.Queue.SRoom, assign.Queue.Console, ref error) <= 0)
                        {
                            CommonController.CreateInstance().MessageBox(this, error, MessageBoxIcon.Error);
                            return;
                        }

                        //刷新
                        if (this.ITriageQueue.Queue != null && this.ITriageQueue.Queue.ID.Equals(assign.Queue.ID))
                        {
                            this.ITriageCalling.Add(assign);
                        }
                        this.ITriageWaiting.Remove(assign);
                        this.ITriageNoSee.Remove(assign);
                        this.initQueue();
                    }
                }
                catch (Exception e)
                {
                    this.showStatusBarTip("进诊失败，原因：" + e.Message);
                }
                finally
                {
                    this.EnumAssignOperType = Function.EnumAssignOperType.空闲;
                }
            }
        }

        /// <summary>
        /// 取消进诊
        /// </summary>
        private void cancelIn()
        {
            FS.SOC.HISFC.Assign.Models.Assign assign = this.ITriageCalling.SelectItem == null ? this.ITriageCalling.FirstItem : this.ITriageCalling.SelectItem;
            if (assign != null)
            {
                // 判定在已到诊、已看诊状态下不允许取消进诊、取消分诊、暂不看诊。{b763437e-95d7-4ffd-bd3b-96de355ecff9}
                if (!this.ValidateCancelInOrTriageOrDelay(assign))
                {
                    return;
                }
                
                if (assign.TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.In)
                {
                    if (CommonController.CreateInstance().MessageBox(this, "是否要取消该进诊信息?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    } 
                }
            }
            this.cancelIn(assign);
        }

        /// <summary>
        /// 取消进诊
        /// </summary>
        private void cancelIn(FS.SOC.HISFC.Assign.Models.Assign assign)
        {
            if (assign == null)
            {
                return;
            }
            lock (this.lockTriagged)
            {
                this.EnumAssignOperType = Function.EnumAssignOperType.手工分诊;
                try
                {
                    // {d4ea07b4-2559-4473-ac92-f8076d9ce3b4}
                    bool isClearDoctorInfo;
                    bool isClearRoomInfo;
                    this.GetChangeToTriageClearInfo(assign.Queue.ID, out isClearDoctorInfo, out isClearRoomInfo);

                    string error = "";
                    assign.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.Triage;
                    if (this.assignBiz.CancelIn(assign, isClearDoctorInfo, isClearRoomInfo, ref error) <= 0)
                    {
                        CommonController.CreateInstance().MessageBox(this, error, MessageBoxIcon.Error);
                        return;
                    }
                    //刷新
                    if (this.ITriageQueue.Queue != null && this.ITriageQueue.Queue.ID.Equals(assign.Queue.ID))
                    {
                        this.ITriageWaiting.Add(assign);
                    }
                    this.ITriageCalling.Remove(assign);
                    this.initQueue();
                }
                catch (Exception e)
                {
                    this.showStatusBarTip("取消进诊失败，原因：" + e.Message);
                }
                finally
                {
                    this.EnumAssignOperType = Function.EnumAssignOperType.空闲;
                }
            }
        }

        /// <summary>
        /// 返回从进诊或从暂不看诊变换为分诊状态时，更新分诊记录时，清空医师、诊室的标志。
        /// </summary>
        /// <param name="queueId"></param>
        /// <param name="isClearDoctorInfo">清空医师</param>
        /// <param name="isClearRoomInfo">清空诊室</param>
        /// {d4ea07b4-2559-4473-ac92-f8076d9ce3b4}
        private void GetChangeToTriageClearInfo(string queueId, out bool isClearDoctorInfo, out bool isClearRoomInfo)
        {
            //
            // 根据不同的队列，清空诊室、医师信息，避免已“叫号”患者取消进诊后不能被其它医师“叫号”。
            // 清空的原则：
            // 医师队列，不清空诊室、医师。
            // 自定义队列，不清空诊室、清空医师。
            // 级别队列，清空诊室、医师。
            //

            isClearDoctorInfo = false;
            isClearRoomInfo = false;
            var queue = this.GetQueueInCurrent(queueId);
            if (queue != null)
            {
                if (queue.QueueType == FS.SOC.HISFC.Assign.Models.EnumQueueType.Custom)
                {
                    isClearDoctorInfo = true;
                    isClearRoomInfo = false;
                }
                else if (queue.QueueType == FS.SOC.HISFC.Assign.Models.EnumQueueType.Doctor)
                {
                    isClearDoctorInfo = false;
                    isClearRoomInfo = false;
                }
                else if (queue.QueueType == FS.SOC.HISFC.Assign.Models.EnumQueueType.RegLevel)
                {
                    isClearDoctorInfo = true;
                    isClearRoomInfo = true;
                }
            }
        }

        /// <summary>
        /// 返回指定队列Id的队列对象。
        /// </summary>
        /// <param name="queueId"></param>
        /// <returns></returns>
        private FS.SOC.HISFC.Assign.Models.Queue GetQueueInCurrent(string queueId)
        {
            if (this.currentRefleshQueueList != null)
            {
                foreach (FS.SOC.HISFC.Assign.Models.Queue queue in this.currentRefleshQueueList)
                {
                    if (queue.ID == queueId)
                    {
                        return queue;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 叫号
        /// </summary>
        private void callIn()
        {
            this.EnumAssignOperType = Function.EnumAssignOperType.叫号;
            try
            {
                FS.SOC.HISFC.Assign.Models.Assign assign = this.ITriageCalling.SelectItem ?? this.ITriageCalling.FirstItem;
                if (assign == null)
                {
                    return;
                }
                string error = "";

                if (this.callBiz.InsertQueue(this.priveNurse, assign, ref error) < 1)
                {
                    CommonController.CreateInstance().MessageBox(this, error, MessageBoxIcon.Error);
                }
            }
            catch (Exception e)
            {
                this.showStatusBarTip("叫号失败，原因：" + e.Message);
            }
            finally
            {
                this.EnumAssignOperType = Function.EnumAssignOperType.空闲;
            }
        }

        /// <summary>
        /// 暂不分诊
        /// </summary>
        private void Delay(FS.SOC.HISFC.Assign.Models.Assign assign)
        {
            if (assign == null)
            {
                return;
            }
            lock (this.lockTriagged)
            {
                // 判定在已到诊、已看诊状态下不允许取消进诊、取消分诊、暂不看诊。{b763437e-95d7-4ffd-bd3b-96de355ecff9}
                if (!this.ValidateCancelInOrTriageOrDelay(assign))
                {
                    return;
                }

                this.EnumAssignOperType = Function.EnumAssignOperType.手工分诊;
                try
                {
                    FS.HISFC.Models.Nurse.EnuTriageStatus status = assign.TriageStatus;

                    assign.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.Delay;
                    //恢复候诊状态
                    string error = "";
                    if (this.assignBiz.NoSee(assign, ref error) <= 0)
                    {
                        CommonController.CreateInstance().MessageBox(this, error, MessageBoxIcon.Error);
                        return;
                    }

                    if (status == FS.HISFC.Models.Nurse.EnuTriageStatus.In)
                    {
                        this.ITriageCalling.Remove(assign);
                    }
                    else if (status == FS.HISFC.Models.Nurse.EnuTriageStatus.Triage)
                    {
                        this.ITriageWaiting.Remove(assign);
                    }

                    this.ITriageNoSee.Add(assign);
                    this.initQueue();
                }
                catch (Exception e)
                {
                    this.showStatusBarTip("分诊失败，原因：" + e.Message);
                }
                finally
                {
                    this.EnumAssignOperType = Function.EnumAssignOperType.空闲;
                }
            }
        }

        /// <summary>
        /// 从Delay到其他状态
        /// </summary>
        /// <param name="assign"></param>
        private void noSeeToOther(FS.SOC.HISFC.Assign.Models.Assign assign, FS.HISFC.Models.Nurse.EnuTriageStatus status)
        {
            if (this.ITriageQueue.Queue == null || assign.Queue.ID.Equals(this.ITriageQueue.Queue.ID) == false)
            {
                if (CommonController.CreateInstance().MessageBox(this, "患者" + assign.Register.Name + "的队列与当前选择的队列不一致，是否继续？", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                if (status == FS.HISFC.Models.Nurse.EnuTriageStatus.Triage)
                {
                    this.cancelTriage(assign);
                    this.triage(assign.Register, this.ITriageQueue.Queue);
                }
                else if (status == FS.HISFC.Models.Nurse.EnuTriageStatus.In)
                {
                    this.cancelTriage(assign);
                    this.triageForCommon(assign, this.ITriageQueue.Queue, status);
                }
            }
            else
            {
                if (status == FS.HISFC.Models.Nurse.EnuTriageStatus.Triage)
                {
                    // {7a47cdce-65fc-442d-81fa-f2a597af3e3a}
                    this.triageFromDelay(assign, this.ITriageQueue.Queue, status);
                }
                else if (status == FS.HISFC.Models.Nurse.EnuTriageStatus.In)
                {
                    this.In(assign, this.ITriageQueue.Queue);
                }
            }

        }

        /// <summary>
        /// 选择挂号信息
        /// </summary>
        /// <param name="register"></param>
        private void selectRegister(FS.HISFC.Models.Registration.Register register)
        {
            if (register == null)
            {
                return;
            }

            this.EnumAssignOperType = Function.EnumAssignOperType.刷新;
            try
            {

                //显示患者信息
                this.ITriagePatientInfo.ShowPatientInfo(register);

                #region 记录当前患者
                currentRegister = register;
                #endregion

                //如果与本护士站匹配
                //进行搜索
                if (Function.CheckPatientRegDept(this.priveNurse, register))
                {
                    //判断是否分诊
                    if (this.outPatientInfoBiz.JudgeTrige(register.ID))
                    {
                        //查找分诊记录
                        FS.SOC.HISFC.Assign.BizLogic.Assign assignMgr = new FS.SOC.HISFC.Assign.BizLogic.Assign();
                        FS.SOC.HISFC.Assign.Models.Assign assign = assignMgr.QueryByClinicID(register.ID);
                        if (assign != null)
                        {
                            this.ITriagePatientInfo.ShowPatientInfo(assign);

                            #region 记录当前患者
                            currentRegister = assign.Register;
                            #endregion

                            //先找到队列
                            this.ITriageQueue.Queue = assign.Queue as FS.SOC.HISFC.Assign.Models.Queue;
                            switch (assign.TriageStatus)
                            {
                                case FS.HISFC.Models.Nurse.EnuTriageStatus.Triage:
                                    this.ITriageWaiting.Add(assign);
                                    break;
                                case FS.HISFC.Models.Nurse.EnuTriageStatus.In:
                                    this.ITriageCalling.Add(assign);
                                    break;
                                case FS.HISFC.Models.Nurse.EnuTriageStatus.Arrive:
                                    this.ITriageArrive.Add(assign);
                                    break;
                                case FS.HISFC.Models.Nurse.EnuTriageStatus.Out:
                                    this.ITriageSee.Add(assign);
                                    break;
                                case FS.HISFC.Models.Nurse.EnuTriageStatus.Delay:
                                    this.ITriageNoSee.Add(assign);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else
                    {
                        this.ITriaggingPatient.Add(register);
                    }
                }
                //如果与本护士站不匹配
                //进行提示 是否修改科室
                else
                { //判断是否分诊
                    if (this.outPatientInfoBiz.JudgeTrige(register.ID))
                    {
                        CommonController.CreateInstance().MessageBox(this, "患者：" + register.Name + "的挂号科室：" + register.DoctorInfo.Templet.Dept.Name + "已分诊！", MessageBoxIcon.Error);
                    }
                    else
                    {
                        //是否进行修改
                        if (CommonController.CreateInstance().MessageBox(this, "患者：" + register.Name + "的挂号科室：" + register.DoctorInfo.Templet.Dept.Name + "与当前分诊台对应的科室不匹配，是否修改挂号科室？", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }
                        if (this.IRegPatient == null)
                        {
                            this.IRegPatient = InterfaceManager.GetIRegPatient();
                            this.IRegPatient.Init();
                        }
                        this.IRegPatient.Modify(register);
                        this.IRegPatient.Load();
                    }
                }
            }
            catch (Exception e)
            {
                this.showStatusBarTip("刷新失败，原因：" + e.Message);
            }
            finally
            {
                this.EnumAssignOperType = Function.EnumAssignOperType.空闲;
            }

        }

        /// <summary>
        /// 修改患者信息
        /// </summary>
        private void modifyPaitentInfo()
        {
            this.EnumAssignOperType = Function.EnumAssignOperType.刷新;
            try
            {
                FS.HISFC.Models.Registration.Register register = this.ITriaggingPatient.SelectItem == null ? this.ITriaggingPatient.FirstItem : this.ITriaggingPatient.SelectItem;
                if (register == null)
                {
                    return;
                }
                if (this.IRegPatient == null)
                {
                    this.IRegPatient = InterfaceManager.GetIRegPatient();
                    this.IRegPatient.Init();
                }
                this.IRegPatient.Modify(register);
                this.IRegPatient.Load();


            }
            catch (Exception e)
            {
                this.showStatusBarTip("修改患者信息失败，原因：" + e.Message);
            }
            finally
            {
                this.EnumAssignOperType = Function.EnumAssignOperType.空闲;
            }
        }



        /// <summary>
        /// 打印条码
        /// </summary>
        /// <param name="regObj"></param>
        private void PrintBarCode()
        {

            FS.HISFC.Models.Registration.Register register = this.ITriaggingPatient.SelectItem == null ? this.ITriaggingPatient.FirstItem : this.ITriaggingPatient.SelectItem;
            if (register == null)
            {
                return;
            }
            if (this.IRegPatient == null)
            {
                this.IRegPatient = InterfaceManager.GetIRegPatient();
                this.IRegPatient.Init();
            }

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
        /// 屏显
        /// </summary>
        private void screen(bool isShowScreen)
        {
            ToolStripButton tsb = this.toolBarService.GetToolButton(isShowScreen ? "屏显（开）" : "屏显（关）");
            tsb.Text = isShowScreen ? "屏显（关）" : "屏显（开）";

            //开启这个窗口
            if (isShowScreen)
            {
                this.IAssignDisplay.Show();
            }
            else
            {
                this.IAssignDisplay.Close();
            }
        }

        /// <summary>
        /// 设置IP地址
        /// </summary>
        private bool setIp()
        {
            FS.HISFC.Models.Base.DepartmentStat temp = Function.GetNurseDepartmentStat(this.priveNurse);

            if (temp != null && string.IsNullOrEmpty(temp.Memo) == false)
            {
                this.txtIp.Text = temp.Memo;
                return true;
            }
            else
            {
                this.txtIp.Text = "";
                return false;
            }

        }

        /// <summary>
        /// 提供在状态栏第一panel显示信息的功能
        /// </summary>
        /// <param name="text">显示信息的文本</param>
        private void showStatusBarTip(string text)
        {
            if (this.ParentForm != null)
            {
                if (this.ParentForm is FS.FrameWork.WinForms.Forms.frmBaseForm)
                {
                    ((FS.FrameWork.WinForms.Forms.frmBaseForm)this.ParentForm).statusBar1.Panels[1].Text = "       " + text + "       ";
                }
            }
        }

        #endregion

        #region 提示信息

        void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            this.notifyIcon1.Visible = false;
        }

        /// <summary>
        /// 气泡提示信息
        /// </summary>
        protected void ShowBalloonTip(int timeOut, string title, string tipText, ToolTipIcon toolTipIcon)
        {
            if (this.DesignMode)
            {
                return;
            }
            if (this.ParentForm != null)
            {
                this.ParentForm.ShowInTaskbar = true;
                this.notifyIcon1.Visible = true;
                this.notifyIcon1.ShowBalloonTip(timeOut, title, tipText, toolTipIcon);
            }
        }

        /// <summary>
        /// 气泡提示信息
        /// </summary>
        protected void ShowBalloonTip(string tipText)
        {
            this.ShowBalloonTip(5, "提示：", tipText, ToolTipIcon.Info);
        }

        #endregion

        #region 自动刷新

        /// <summary>
        /// 自动刷新开始时间
        /// </summary>
        private DateTime autoRefreshBeginTime = DateTime.Now;

        /// <summary>
        /// 终端设置的刷新间隔
        /// </summary>
        private int refreshInterval = 10;

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

        #endregion

        #region 触发事件

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.F10)
            {
                this.txtCardNO.SelectAll();
                this.txtCardNO.Focus();
                return true;
            }
            if (keyData == Keys.F9)
            {
                if ((int)this.queryType + 1 == (int)EnumQueryType.End)
                {
                    this.queryType = (EnumQueryType)0;
                }
                else
                {
                    this.queryType = (EnumQueryType)((int)this.queryType + 1);
                }
                this.lbCardNO.Text = this.queryType.ToString() + "：";
            }

            return base.ProcessDialogKey(keyData);
        }

        void txtCardNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //查找患者
                string cardNO = this.txtCardNO.Text.Trim();
                string error = "";
                ArrayList al = new ArrayList();
                if ((int)this.queryType == 0)
                {
                    al = outPatientInfoBiz.GetCardNO(ref cardNO, this.autoRefreshBeginTime, ref error);
                }
                else if ((int)this.queryType == 1)
                {
                    al = outPatientInfoBiz.GetName(ref cardNO, this.autoRefreshBeginTime, ref error);
                }

                if (al == null)
                {
                    CommonController.CreateInstance().MessageBox(this, error, MessageBoxIcon.Error);
                    return;
                }

                if (al.Count == 0)
                {
                    CommonController.CreateInstance().MessageBox(this, "病历（卡）号：" + cardNO + "未找到有效的挂号记录", MessageBoxIcon.Information);
                    return;
                }

                //只找到一条挂号记录
                if (al.Count == 1)
                {
                    this.selectRegister(al[0] as FS.HISFC.Models.Registration.Register);
                }
                else
                {
                    //多条挂号记录，让收费员自己去选择
                    if (ucShow == null || ucShow.IsDisposed)
                    {
                        ucShow = new frmSelectRegister();
                        ucShow.SelectedRegister += new frmSelectRegister.GetRegister(ucShow_SelectedRegister);
                    }

                    ucShow.SetRegisterInfo(al);
                    ucShow.StartPosition = FormStartPosition.Manual;
                    ucShow.Location = new Point(this.PointToScreen(this.txtCardNO.Location).X, this.PointToScreen(this.txtCardNO.Location).Y + this.txtCardNO.Size.Height);
                    ucShow.Show(this);

                }

            }
        }

        void ucShow_SelectedRegister(FS.HISFC.Models.Registration.Register reg)
        {
            this.selectRegister(reg);
        }

        void ITriaggingPatient_SelectedRegisterChange(FS.HISFC.Models.Registration.Register register)
        {
            if (register == null)
            {
                this.ITriagePatientInfo.Clear();

                #region 记录当前患者
                currentRegister = null;
                #endregion
            }
            else
            {
                this.ITriagePatientInfo.ShowPatientInfo(register);

                #region 记录当前患者
                currentRegister = register;
                #endregion
            }
        }

        /// <summary>
        /// 取消分诊
        /// </summary>
        /// <param name="assign"></param>
        void ITriaggingPatient_DragDrop(FS.SOC.HISFC.Assign.Models.Assign assign)
        {
            if (assign == null)
            {
                return;
            }

            if (assign.TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.In)
            {
                this.cancelIn(assign);
            }
            this.cancelTriage(assign);
        }

        void ITriageWaiting_SelectedAssignChange(FS.SOC.HISFC.Assign.Models.Assign assign)
        {
            if (assign == null)
            {
                this.ITriagePatientInfo.Clear();

                #region 记录当前患者
                currentRegister = null;
                #endregion
            }
            else
            {
                this.ITriagePatientInfo.ShowPatientInfo(assign);

                #region 记录当前患者
                currentRegister = assign.Register;
                #endregion
            }
        }

        /// <summary>
        /// 取消进诊
        /// </summary>
        /// <param name="assign"></param>
        void ITriageWaiting_DragDrop(FS.SOC.HISFC.Assign.Models.Assign assign)
        {
            if (assign == null)
            {
                return;
            }

            //未看诊的重新分诊
            if (assign.TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.Delay)
            {
                this.noSeeToOther(assign, FS.HISFC.Models.Nurse.EnuTriageStatus.Triage);
            }
            else
            {
                this.cancelIn(assign);
            }
        }

        /// <summary>
        /// 分诊
        /// </summary>
        /// <param name="assign"></param>
        void ITriageWaiting_DragDrop(FS.HISFC.Models.Registration.Register register)
        {
            if (register == null)
            {
                return;
            }

            this.triage(register, this.ITriageQueue.Queue);
        }

        /// <summary>
        /// 暂不看诊
        /// </summary>
        /// <param name="assign"></param>
        void ITriageNoSee_DragDrop(FS.SOC.HISFC.Assign.Models.Assign assign)
        {
            if (assign == null)
            {
                return;
            }

            this.Delay(assign);
        }

        /// <summary>
        /// 暂不看诊
        /// </summary>
        /// <param name="assign"></param>
        void ITriageNoSee_DragDrop(FS.HISFC.Models.Registration.Register register)
        {
            if (register == null)
            {
                return;
            }
            FS.SOC.HISFC.Assign.Models.Assign assign = this.triage(register, null);
            if (assign == null)
            {
                return;
            }
            this.Delay(assign);



        }

        /// <summary>
        /// 进诊
        /// </summary>
        /// <param name="assign"></param>
        void ITriageCalling_DragDrop(FS.SOC.HISFC.Assign.Models.Assign assign)
        {
            if (assign == null)
            {
                return;
            }
            //如果与当前的队列不一致
            if (assign.TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.Delay)
            {
                this.noSeeToOther(assign, FS.HISFC.Models.Nurse.EnuTriageStatus.In);

            }
            else
            {
                this.In(assign, this.ITriageQueue.Queue);
            }
        }

        /// <summary>
        /// 先分诊，后进诊
        /// </summary>
        /// <param name="assign"></param>
        void ITriageCalling_DragDrop(FS.HISFC.Models.Registration.Register register)
        {
            if (register == null)
            {
                return;
            }

            //直接分诊到进诊队列
            this.In(this.triage(register, this.ITriageCalling.Queue), this.ITriageCalling.Queue);
        }

        void ITriageQueue_SelectedQueueChange(FS.SOC.HISFC.Assign.Models.Queue queue)
        {
            this.EnumAssignOperType = Function.EnumAssignOperType.队列显示;
            try
            {
                this.refreshTriaggedPatient(queue);
                this.ITriagePatientInfo.Clear();

                this.ITriageArrive.Queue = queue;
                this.ITriageCalling.Queue = queue;
                this.ITriageSee.Queue = queue;
                this.ITriageWaiting.Queue = queue;
            }
            finally
            {
                this.EnumAssignOperType = Function.EnumAssignOperType.空闲;
            }
        }

        /// <summary>
        /// 取消分诊
        /// </summary>
        /// <param name="assign"></param>
        void ITriageQueue_DragDrop(FS.SOC.HISFC.Assign.Models.Assign assign)
        {
            if (assign == null)
            {
                return;
            }

            //如果与原来的队列不同，
            //已分诊则先取消分诊，然后重新分诊，
            //已进诊先取消进诊，然后取消分诊，然后重新分诊
            //如果与原来的队列相同，那么就直接返回
            if (this.ITriageQueue.Queue == null)
            {
                return;
            }

            if (assign.Queue.ID.Equals(this.ITriageQueue.Queue.ID))
            {
                return;
            }

            if (assign.TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.Triage)
            {
                this.cancelTriage(assign);
                this.triage(assign.Register, this.ITriageQueue.Queue);
            }
            else if (assign.TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.In)
            {
                this.cancelIn(assign);
                this.cancelTriage(assign);
                this.triage(assign.Register, this.ITriageQueue.Queue);
            }
            else if (assign.TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.Delay)
            {
                this.noSeeToOther(assign, FS.HISFC.Models.Nurse.EnuTriageStatus.Triage);
            }
        }

        /// <summary>
        /// 分诊
        /// </summary>
        /// <param name="assign"></param>
        void ITriageQueue_DragDrop(FS.HISFC.Models.Registration.Register register)
        {
            if (register == null)
            {
                return;
            }

            this.triage(register, this.ITriageQueue.Queue);
        }

        /// <summary>
        /// 修改患者信息
        /// </summary>
        /// <param name="assign"></param>
        void IRegPatient_SaveRegister(FS.HISFC.Models.Registration.Register register)
        {
            if (register == null)
            {
                return;
            }
            if (Function.CheckPatientRegDept(this.priveNurse, register))
            {
                this.ITriaggingPatient.Add(register);
            }
            else
            {
                this.ITriaggingPatient.Remove(register);
            }
        }

        void ckAutoTriage_CheckedChanged(object sender, EventArgs e)
        {
            this.ckAutoTriage.CheckedChanged -= new EventHandler(ckAutoTriage_CheckedChanged);
            this.ckAutoTriage.Checked = !this.ckAutoTriage.Checked;

            if (!this.ckAutoTriage.Checked)
            {
                string info = "";
                //注册打印
                bool successful = Function.RegisterNurse(this.priveNurse, ref info);
                if (successful)
                {
                    this.AutoRefresh();
                }

                this.ckAutoTriage.Checked = successful;

                if (!string.IsNullOrEmpty(info))
                {
                    CommonController.CreateInstance().MessageBox(this, info, MessageBoxIcon.Asterisk);
                }
            }
            else
            {
                //需要权限进行取消
                if (Function.JugePrive("1401", "01"))
                {
                    string info = "";
                    bool successful = Function.CancelRegisterNurse(this.priveNurse, ref info);
                    this.ckAutoTriage.Checked = !successful;

                    if (!string.IsNullOrEmpty(info))
                    {
                        CommonController.CreateInstance().MessageBox(this, info, MessageBoxIcon.Asterisk);
                    }
                }
                else
                {
                    CommonController.CreateInstance().MessageBox(this, "取消自动分诊注册需要权限，如果您没有权限请与负责人联系！", MessageBoxIcon.Asterisk);
                    this.ckAutoTriage.Checked = true;
                }

            }

            this.setIp();
            this.ckAutoTriage.CheckedChanged += new EventHandler(ckAutoTriage_CheckedChanged);
        }

        void cmbRefreshTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbRefreshTime.SelectedItem != null)
            {
                this.refreshInterval = FS.FrameWork.Function.NConvert.ToInt32(this.cmbRefreshTime.SelectedItem.ID);
                if (this.refreshInterval <= 0)
                {
                    this.refreshInterval = 10;
                }
                if (this.autoRefreshTimer != null)
                {
                    this.autoRefreshTimer.Dispose();
                }
                this.BeginAutoRefresh();
            }
        }

        void lbPrivNurse_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                this.ckAutoTriage.CheckedChanged -= new EventHandler(ckAutoTriage_CheckedChanged);
                this.PreArrange();
                this.AutoRefresh();
            }
            finally
            {
                this.ckAutoTriage.CheckedChanged += new EventHandler(ckAutoTriage_CheckedChanged);
            }
        }

        #endregion

        #region IPreArrange 成员

        public int PreArrange()
        {
            if (this.DesignMode)
            {
                return 0;
            }

            #region 权限科室
            FS.HISFC.BizLogic.Manager.UserPowerDetailManager userPowerDetailManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
            this.PriveDept = ((FS.HISFC.Models.Base.Employee)userPowerDetailManager.Operator).Dept;

            if (this.isCheckPrivePower)
            {
                if (string.IsNullOrEmpty(this.privePowerString))
                {
                    privePowerString = "1401+01";
                }
                if (privePowerString.Split('+').Length < 2)
                {
                    privePowerString = privePowerString + "+01";
                }
                string[] prives = privePowerString.Split('+');

                if (this.isChooseDeptWhenCheckPrive)
                {
                    int param = FS.HISFC.Components.Common.Classes.Function.ChoosePrivDept(prives[0], prives[1], ref this.priveDept);
                    if (param == 0 || param == -1)
                    {
                        return -1;
                    }
                    this.gbAddInfo.Text = "附加信息： 特别提醒您正在分诊【" + this.priveDept.Name + "】的患者";
                }
                else
                {
                    List<FS.FrameWork.Models.NeuObject> alDept = userPowerDetailManager.QueryUserPriv(userPowerDetailManager.Operator.ID, prives[0], prives[1]);
                    if (alDept == null || alDept.Count == 0)
                    {
                        CommonController.CreateInstance().MessageBox(this, "您没有权限！", MessageBoxIcon.Information);
                        return -1;
                    }
                    foreach (FS.FrameWork.Models.NeuObject dept in alDept)
                    {
                        if (dept.ID == ((FS.HISFC.Models.Base.Employee)userPowerDetailManager.Operator).Dept.ID)
                        {
                            this.PriveDept = dept;
                            break;
                        }
                    }
                }

                if (this.PriveDept == null)
                {
                    CommonController.CreateInstance().MessageBox(this, "您没有权限！请重新登录！", MessageBoxIcon.Asterisk);
                    return -1;
                }
            }
            else
            {
                this.PriveDept = ((FS.HISFC.Models.Base.Employee)userPowerDetailManager.Operator).Dept;
            }


            //最后查找对应权限病区
            //如果权限科室本身就是护士站了，那就直接等于科室
            if (CommonController.CreateInstance().GetDepartment(this.PriveDept.ID).DeptType.ID.Equals(FS.HISFC.Models.Base.EnumDepartmentType.N.ToString()))
            {
                this.priveNurse = this.PriveDept.Clone();
            }
            else
            {
                FS.HISFC.BizLogic.Manager.DepartmentStatManager statManager = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
                ArrayList al = statManager.LoadByChildren("14", this.priveDept.ID);
                //查找权限科室对应的护士站
                if (al != null && al.Count > 0)
                {
                    this.priveNurse = CommonController.CreateInstance().GetDepartment((al[0] as FS.HISFC.Models.Base.DepartmentStat).PardepCode);
                }
                else
                {
                    this.priveNurse = ((FS.HISFC.Models.Base.Employee)userPowerDetailManager.Operator).Nurse;
                }
            }

            if (this.priveNurse != null)
            {
                this.lbPrivNurse.Text = "当前护士站：" + this.priveNurse.Name + "，双击进行改变";
            }

            #endregion

            #region 自动注册
            if (this.isRegisterAutoPrint)
            {
                this.ckAutoTriage.Checked = Function.CheckAutoAssignPrive(this.priveNurse);
            }

            this.setIp();

            #endregion

            return 1;
        }

        #endregion

        #region 查询类型
        /// <summary>
        /// 查找信息的查询类型
        /// </summary>
        enum EnumQueryType
        {
            病人号,
            姓名,
            End
        }

        /// <summary>
        /// 查询类型
        /// </summary>
        EnumQueryType queryType = EnumQueryType.病人号;
        #endregion
    }
}
