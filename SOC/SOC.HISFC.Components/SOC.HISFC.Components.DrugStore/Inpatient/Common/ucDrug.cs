using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.DrugStore.Inpatient.Common
{
    /// <summary>
    /// [功能描述: 住院药房发药]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-12]<br></br>
    /// 说明：
    /// 1、去掉药柜管理功能
    /// </summary>
    public partial class ucDrug : ucDrugBase, FS.FrameWork.WinForms.Classes.IPreArrange
    {

        public ucDrug()
        {
            InitializeComponent();
            this.ntbDrugControl.DrawItem += new DrawItemEventHandler(ntbDrugControl_DrawItem);
        }

        #region 属性变量

        /// <summary>
        /// 查询设置的Dock属性
        /// </summary>
        [Description("查询设置的Dock属性"), Category("设置"), Browsable(true)]
        public DockStyle QuerySetDock
        {
            get 
            {
                return this.ngbQuerySet.Dock;
            }
            set
            {
                this.ngbQuerySet.Dock = value;
                this.neuSplitter1.Dock = value;
            }

        }

        private string hightLightDept = "";
        [Category("设置"), Description("特殊科室摆药单高亮显示")]
        public string HightLightDept
        {
            get
            {
                return this.hightLightDept;
            }
            set
            {
                this.hightLightDept = value;
            }
        }

        private bool isDrugClassBillFirst = false;

        /// <summary>
        /// 摆药单分类优先
        /// </summary>
        [Description("摆药单分类优先"), Category("设置"), Browsable(true)]
        public bool IsDrugClassBillFirst
        {
            get { return isDrugClassBillFirst; }
            set { isDrugClassBillFirst = value; }
        }

        private bool isQueryOnLoad = true;

        /// <summary>
        /// 界面打开时是否查询列表
        /// </summary>
        [Description("界面打开时是否查询列表"), Category("设置"), Browsable(true)]
        public bool IsQueryOnLoad
        {
            get { return isQueryOnLoad; }
            set { isQueryOnLoad = value; }
        }

        private bool isExpand = true;

        /// <summary>
        /// 列表查询后是否展开树节点
        /// </summary>
        [Description("列表查询后是否展开树节点"), Category("设置"), Browsable(true)]
        public bool IsExpand
        {
            get { return isExpand; }
            set { isExpand = value; }
        }

        /// <summary>
        /// 是否以tab列表形式显示发药台
        /// </summary>
        private bool isShowDrugControlByTab = false;
        [Category("设置"), Description("是否以tab列表形式显示发药台")]
        public bool IsShowDrugControlByTab
        {
            get { return this.isShowDrugControlByTab; }
            set { this.isShowDrugControlByTab = value; }
        }

        private Function.EnumInpatintDrugOperType enumInpatintDrugOperType = Function.EnumInpatintDrugOperType.空闲;

        /// <summary>
        /// 操作状态
        /// 对于空闲状态才可以进行下一步操作
        /// </summary>
        [Description("操作状态"), Category("非设置"), Browsable(false)]
        public Function.EnumInpatintDrugOperType EnumInpatintDrugOperType
        {
            get
            {

                return Function.EnumInpatintDrugOperType.空闲;
            }
            set
            {
                enumInpatintDrugOperType = value;                
            }
        }

        FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore drugStoreMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore();
        //FS.SOC.HISFC.BizLogic.Pharmacy.Item drugStoreMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// 当前选中的摆药通知
        /// </summary>
        FS.HISFC.Models.Pharmacy.DrugMessage curDrugMessage;

        private Function.EnumInpatintDrugApplyType curInpatintDrugApplyType = Function.EnumInpatintDrugApplyType.临时发送;

        /// <summary>
        /// 住院发药申请方式
        /// </summary>
        [Description("住院发药申请方式"), Category("设置"), Browsable(true)]
        public Function.EnumInpatintDrugApplyType InpatintDrugApplyType
        {
            get { return curInpatintDrugApplyType; }
            set { curInpatintDrugApplyType = value; }
        }

        private int curApplyOutValidDays = 30;

        /// <summary>
        /// 形成摆药通知的发药申请有效天数
        /// </summary>
        [Category("设置"), Browsable(true), Description("形成摆药通知的发药申请有效天数")]
        public int ApplyOutValidDays
        {
            get { return curApplyOutValidDays; }
            set { curApplyOutValidDays = value; }
        }

        private bool isRegisterAutoPrint = true;

        /// <summary>
        /// 是否自动打印注册
        /// </summary>
        [Category("设置"), Browsable(true), Description("是否自动打印注册")]
        public bool IsRegisterAutoPrint
        {
            get { return isRegisterAutoPrint; }
            set { isRegisterAutoPrint = value; }
        }


        private string curUnPrintBillOnSave = "";

        /// <summary>
        /// 保存时不打印的摆药单：R退药单，O出院带药
        /// </summary>
        [Description("保存时不打印的摆药单：R退药单，O出院带药"), Category("设置"), Browsable(true)]
        public string UnPrintBillOnSave
        {
            get { return curUnPrintBillOnSave; }
            set { curUnPrintBillOnSave = value; }
        }

        /// <summary>
        /// 保存时提示是否打印的摆药单R
        /// </summary>
        private string curWariningPrintBillOnSave = "";
        [Description("保存时提示是否打印的摆药单：R退药单"), Category("设置"), Browsable(true)]
        public string WarningPrintBillOnSave
        {
            get { return curWariningPrintBillOnSave; }
            set { curWariningPrintBillOnSave = value; }
        }

        private string curNotCheckAllBillCode = "";


        private string curSplitPatientBillClassNO = "R,O";

        /// <summary>
        /// 保存时必须按照患者分单摆药单：R退药单，O出院带药
        /// </summary>
        [Description("保存时必须按照患者分单摆药单：R退药单，O出院带药"), Category("设置"), Browsable(true)]
        public string SplitPatientBillClassNO
        {
            get { return curSplitPatientBillClassNO; }
            set { curSplitPatientBillClassNO = value; }
        }

        /// <summary>
        /// 默认不全选的摆药单：R退药单，O出院带药
        /// </summary>
        [Description("默认不全选的摆药单：R退药单，O出院带药"), Category("设置"), Browsable(true)]
        public string NotCheckAllBillCode
        {
            get { return curNotCheckAllBillCode; }
            set { curNotCheckAllBillCode = value; }
        }

        private Function.EnumSendDrugTypeOnLackStock curEnumSendDrugTypeOnLackStock = Function.EnumSendDrugTypeOnLackStock.手工处理;

        /// <summary>
        /// 库存不足时摆药方式,即系统选择数据方式
        /// </summary>
        [Description("默认不全选的摆库存不足时摆药方式,即系统选择数据方式"), Category("设置"), Browsable(true)]
        public Function.EnumSendDrugTypeOnLackStock EnumSendDrugTypeOnLackStock
        {
            get { return curEnumSendDrugTypeOnLackStock; }
            set { curEnumSendDrugTypeOnLackStock = value; }
        }

        private string deptNeedRemind = string.Empty;

        /// <summary>
        /// 界面打开时是否查询列表
        /// </summary>
        [Description("自动刷新时有新摆药单需要进行提示的药房"), Category("设置"), Browsable(true)]
        public string DeptNeedRemind
        {
            get { return deptNeedRemind; }
            set { deptNeedRemind = value; }
        }

        private FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientWorkLoad iInpatientWordLoad = null;

        #endregion

        #region IPreArrange 成员

        public int PreArrange()
        {
            #region 权限科室
            FS.HISFC.BizLogic.Manager.UserPowerDetailManager userPowerDetailManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
            if (this.IsCheckPrivePower)
            {
                if (string.IsNullOrEmpty(PrivePowerString))
                {
                    PrivePowerString = "0320+Z1";
                }
                if (PrivePowerString.Split('+').Length < 2)
                {
                    PrivePowerString = PrivePowerString + "+Z1";
                }
                string[] prives = PrivePowerString.Split('+');
                List<FS.FrameWork.Models.NeuObject> alDept = userPowerDetailManager.QueryUserPriv(userPowerDetailManager.Operator.ID, prives[0], prives[1]);
                if (alDept == null || alDept.Count == 0)
                {
                    this.ShowMessage("您没有权限！");
                    return -1;
                }
                foreach (FS.FrameWork.Models.NeuObject dept in alDept)
                {
                    if (dept.ID == ((FS.HISFC.Models.Base.Employee)userPowerDetailManager.Operator).Dept.ID)
                    {
                        this.PriveDept = dept;
                        this.StockDept = PriveDept.Clone();
                        break;
                    }
                }

                if (this.IsOtherDeptDrug)
                {
                    int param = Function.ChoosePriveDept(prives[0], prives[1], ref this.priveDept);
                    if (param == 0 || param == -1)
                    {
                        return -1;
                    }
                }

                if (this.PriveDept == null)
                {
                    this.ShowMessage("您没有权限！请重新登录！");
                    return -1;
                }
            }
            else
            {
                this.PriveDept = ((FS.HISFC.Models.Base.Employee)userPowerDetailManager.Operator).Dept;
                this.StockDept = PriveDept.Clone();
            }
            #endregion

            #region 摆药台

            this.DrugControl = Function.ControlSelect(this.PriveDept);
            if (this.DrugControl == null)
            {
                return -1;
            }

            this.ncbAutoPrintRegister.Visible = this.DrugControl.IsAutoPrint;

            //自动打印生效要看本机IP地址是否和终端备注维护相同，这样可以避免多台电脑打开同一窗口
            if (this.isRegisterAutoPrint)
            {
                this.ncbAutoPrintRegister.Checked = Function.CheckAutoPrintPrive(this.DrugControl);
            }


            #endregion

            this.nlbInfo.Text = "您当前选择的是：【" + this.priveDept.Name + "】的【" + this.DrugControl.Name + "】    需要选择其他摆药台时请退出窗口后重新进入";
            
            return 1;
        }

        #endregion

        #region 自动刷新功能

        /// <summary>
        /// 自动刷新启动延迟时间-秒
        /// </summary>
        private uint refreshDueTime = 30;

        /// <summary>
        /// 自动刷新启动延迟时间-秒
        /// </summary>
        [Description("自动刷新启动延迟时间-秒"), Category("设置"), Browsable(true)]
        public uint RefreshDueTime
        {
            get { return refreshDueTime; }
            set { refreshDueTime = value; }
        }

        /// <summary>
        /// 自动刷新间隔
        /// </summary>
        private int refreshInterval = -1;

        /// <summary>
        /// 自动刷新间隔-秒
        /// </summary>
        [Description("自动刷新间隔-秒"), Category("设置"), Browsable(true)]
        public int RefreshInterval
        {
            get { return refreshInterval; }
            set { refreshInterval = value; }
        }

        private string curUnAutoSaveDrugBillNO = "R,P,O,";

        /// <summary>
        /// 不自动保存的摆药单：R退药单，P非医嘱，O出院带药
        /// </summary>
        [Description("不自动保存的摆药单：R退药单，P非医嘱，O出院带药"), Category("设置"), Browsable(true)]
        public string UnAutoSaveDrugBillNO
        {
            get { return curUnAutoSaveDrugBillNO; }
            set { curUnAutoSaveDrugBillNO = value; }
        }

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
            this.autoRefreshTimer = new System.Threading.Timer(this.autoRefreshCallBack, null, refreshDueTime * 1000, this.refreshInterval * 1000);
        }

        /// <summary>
        /// 刷新处方列表
        /// </summary>
        /// <param name="param">参数（没有使用）</param>
        /// <returns></returns>
        private void AutoRefreshTimerCallback(object param)
        {
            if (this.ncbPauseRefresh.Checked)
            {
                return;
            }

            if (this.autoRefreshEven == null)
            {
                autoRefreshEven = new autoRefreshHandler(this.AutoRefresh);
            }
            if (this.ParentForm != null)
            {
                this.ParentForm.Invoke(this.autoRefreshEven);
            }

        }
        #endregion

        #region 方法
        
        /// <summary>
        /// 自动刷新，改动前三思
        /// </summary>
        private void AutoRefresh()
        {
          
            if (this.EnumInpatintDrugOperType != Function.EnumInpatintDrugOperType.空闲)
            {
                this.ShowStatusBarTip("正在" + this.EnumInpatintDrugOperType.ToString() + ",本次自动刷新已取消");
                return;
            }
            this.EnumInpatintDrugOperType = Function.EnumInpatintDrugOperType.自动刷新;
            if (this.DrugControl.IsAutoPrint)
            {
                if (!this.ncbAutoPrintRegister.Checked && this.IsRegisterAutoPrint)
                {
                    this.AutoQuery();
                    return;
                }
                this.AutoSave();
            }
            else
            {
                this.AutoQuery();
            }
            this.EnumInpatintDrugOperType = Function.EnumInpatintDrugOperType.空闲;

        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        private int Query()
        {
            if (this.EnumInpatintDrugOperType != Function.EnumInpatintDrugOperType.空闲 && this.EnumInpatintDrugOperType != Function.EnumInpatintDrugOperType.保存)
            {
                this.ShowStatusBarTip("正在" + this.EnumInpatintDrugOperType.ToString() + ",本次查询已取消");
                return -1;
            }
            this.EnumInpatintDrugOperType = Function.EnumInpatintDrugOperType.查询;

            bool isDrugReturn = false;
            string deptNo = string.Empty;
            if (this.curDrugMessage != null && this.curDrugMessage.DrugBillClass.ID == "R")
            {
                isDrugReturn = true;
                deptNo = curDrugMessage.ApplyDept.ID;
            }
            this.ucDrugDetail1.Clear();
            this.tvMessageBaseTree1.Clear();
            this.curDrugMessage = null;


            #region 以摆药通知形式处理
            ArrayList alDrugMessage = new ArrayList();
            if (this.InpatintDrugApplyType == Function.EnumInpatintDrugApplyType.全区发送)
            {
                alDrugMessage = this.drugStoreMgr.QueryDrugMessageListAfterConcentratedSend(this.DrugControl, this.ApplyOutValidDays);
            }
            else if(this.InpatintDrugApplyType== Function.EnumInpatintDrugApplyType.临时发送)
            {
                alDrugMessage = this.drugStoreMgr.QueryDrugMessageList(this.DrugControl, this.ApplyOutValidDays);
            }

            if (alDrugMessage == null)
            {
                this.ShowMessage("查询摆药通知失败：" + this.drugStoreMgr.Err, MessageBoxIcon.Error);
                return -1;
            }


            if (this.InpatintDrugApplyType == Function.EnumInpatintDrugApplyType.全区发送)
            {
                ArrayList alEmergency = this.drugStoreMgr.QueryEmergencyDrugMessageList(this.DrugControl, this.ApplyOutValidDays);
                if (alEmergency == null)
                {
                    this.ShowMessage("查询摆药通知失败：" + this.drugStoreMgr.Err, MessageBoxIcon.Error);
                    return -1;
                }
                alDrugMessage.AddRange(alEmergency);
            }


            if (alDrugMessage.Count > 0)
            {
                if (this.DrugControl.ShowLevel > 1)
                {
                    ArrayList alInpatient = this.drugStoreMgr.QueryDrugMessageInpatientList(this.DrugControl, this.ApplyOutValidDays);
                    if (alInpatient == null)
                    {
                        this.ShowMessage("查询摆药通知失败：" + this.drugStoreMgr.Err, MessageBoxIcon.Error);
                        return -1;
                    }
                    this.tvMessageBaseTree1.ShowDrugMessage(alDrugMessage, this.DrugControl, this.IsDrugClassBillFirst, alInpatient, this.isExpand, this.InpatintDrugApplyType == Function.EnumInpatintDrugApplyType.全区发送);

                }
                else
                {
                    this.tvMessageBaseTree1.ShowDrugMessage(alDrugMessage, this.DrugControl, this.IsDrugClassBillFirst, null, this.isExpand, this.InpatintDrugApplyType == Function.EnumInpatintDrugApplyType.全区发送);
                }
            }
            #endregion


            if (this.InpatintDrugApplyType == Function.EnumInpatintDrugApplyType.按单发送)
            {
                ArrayList alSendDrugBill = this.drugStoreMgr.QuerySendedDrugBill(this.DrugControl, this.ApplyOutValidDays);

                if (alSendDrugBill == null)
                {
                    this.ShowMessage("查询发药申请单失败：" + this.drugStoreMgr.Err, MessageBoxIcon.Error);
                    return -1;
                }
                if (alSendDrugBill.Count > 0)
                {
                    this.tvMessageBaseTree1.ShowDrugBillClass(alSendDrugBill, this.isExpand);
                }
            }

            //{CC289C41-7C69-42e2-9945-972F3EFAB663}
            this.tvMessageBaseTree1.SelectDefaultNodeEx(deptNo);

            this.EnumInpatintDrugOperType = Function.EnumInpatintDrugOperType.空闲;

            return 1;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        private int AutoQuery()
        {
            if (this.EnumInpatintDrugOperType != Function.EnumInpatintDrugOperType.空闲 && this.EnumInpatintDrugOperType == Function.EnumInpatintDrugOperType.保存)
            {
                this.ShowStatusBarTip("正在" + this.EnumInpatintDrugOperType.ToString() + ",本次查询已取消");
                return -1;
            }
            this.EnumInpatintDrugOperType = Function.EnumInpatintDrugOperType.查询;

            //this.ucDrugDetail1.Clear();
            //this.tvMessageBaseTree1.Clear();
            this.curDrugMessage = null;

            #region 以摆药通知形式处理
            ArrayList alDrugMessage = new ArrayList();
            if (this.InpatintDrugApplyType == Function.EnumInpatintDrugApplyType.全区发送)
            {
                alDrugMessage = this.drugStoreMgr.QueryDrugMessageListAfterConcentratedSend(this.DrugControl, this.ApplyOutValidDays);
            }
            else if(this.InpatintDrugApplyType == Function.EnumInpatintDrugApplyType.临时发送)
            {
                alDrugMessage = this.drugStoreMgr.QueryDrugMessageList(this.DrugControl, this.ApplyOutValidDays);
            }

            if (alDrugMessage == null)
            {
                this.ShowStatusBarTip("查询摆药通知失败：" + this.drugStoreMgr.Err);
                return -1;
            }
            if (this.InpatintDrugApplyType == Function.EnumInpatintDrugApplyType.全区发送)
            {
                ArrayList alEmergency = this.drugStoreMgr.QueryEmergencyDrugMessageList(this.DrugControl, this.ApplyOutValidDays);
                if (alEmergency == null)
                {
                    this.ShowStatusBarTip("查询摆药通知失败：" + this.drugStoreMgr.Err);
                    return -1;
                }
                alDrugMessage.AddRange(alEmergency);
            }
            if (alDrugMessage.Count > 0)
            {
                if (this.DrugControl.ShowLevel > 1)
                {
                    ArrayList alInpatient = this.drugStoreMgr.QueryDrugMessageInpatientList(this.DrugControl, this.ApplyOutValidDays);
                    if (alInpatient == null)
                    {
                        this.ShowStatusBarTip("查询摆药通知失败：" + this.drugStoreMgr.Err);
                        return -1;
                    }
                    this.tvMessageBaseTree1.ShowDrugMessage(alDrugMessage, this.DrugControl, this.IsDrugClassBillFirst, alInpatient, this.isExpand, this.InpatintDrugApplyType == Function.EnumInpatintDrugApplyType.全区发送);

                    //{163E9555-00CC-4c09-B329-A4718A9D2C2B}
                    if (alInpatient.Count > 0 && this.deptNeedRemind.Contains(this.PriveDept.ID))
                    {
                        FrameWork.WinForms.Classes.Function.ShowBalloonTip(this.refreshInterval * 1000, "摆药提醒", "有新的摆药单，请注意查看！", ToolTipIcon.Info);
                    }
                }
                else
                {
                    this.tvMessageBaseTree1.ShowDrugMessage(alDrugMessage, this.DrugControl, this.IsDrugClassBillFirst, null, this.isExpand, this.InpatintDrugApplyType == Function.EnumInpatintDrugApplyType.全区发送);
                    this.ShowDetail(this.tvMessageBaseTree1.SelectedNode);
                }

            }
            #endregion

            if (this.InpatintDrugApplyType == Function.EnumInpatintDrugApplyType.按单发送)
            {
                ArrayList alSendDrugBill = this.drugStoreMgr.QuerySendedDrugBill(this.DrugControl, this.ApplyOutValidDays);

                if (alSendDrugBill == null)
                {
                    this.ShowMessage("查询发药申请单失败：" + this.drugStoreMgr.Err, MessageBoxIcon.Error);
                    return -1;
                }
                if (alSendDrugBill.Count > 0)
                {
                    this.tvMessageBaseTree1.ShowDrugBillClass(alSendDrugBill, this.IsExpand);
                }
            }
            return 1;
        }



        private void ShowDetail(TreeNode node)
        {
            if (node == null)
            {
                return;
            }
            if (this.EnumInpatintDrugOperType != Function.EnumInpatintDrugOperType.空闲)
            {
                this.ShowStatusBarTip("正在" + this.EnumInpatintDrugOperType.ToString() + ",本次查询已取消");
                return;
            }

            this.EnumInpatintDrugOperType = Function.EnumInpatintDrugOperType.查询;

            if (node.Level == 0)
            {
                if (node.Tag == null)
                {
                    this.EnumInpatintDrugOperType = Function.EnumInpatintDrugOperType.空闲;
                    return;
                }
                if (node.Tag is FS.HISFC.Models.Pharmacy.DrugMessage)
                {
                    FS.HISFC.Models.Pharmacy.DrugMessage drugMessage = node.Tag as FS.HISFC.Models.Pharmacy.DrugMessage;
                    this.curDrugMessage = drugMessage.Clone();

                }
                else if (node.Tag is FS.HISFC.Models.Pharmacy.DrugBillClass)
                {
                    FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass = node.Tag as FS.HISFC.Models.Pharmacy.DrugBillClass;
                    if (this.curDrugMessage == null)
                    {
                        this.curDrugMessage = new FS.HISFC.Models.Pharmacy.DrugMessage();
                    }
                    this.curDrugMessage.ApplyDept.ID = drugBillClass.ApplyDept.ID;
                    this.curDrugMessage.ApplyDept.Name = drugBillClass.ApplyDept.Name;
                    this.curDrugMessage.DrugBillClass = drugBillClass.Clone();
                    this.curDrugMessage.StockDept.ID = this.PriveDept.ID;
                    this.curDrugMessage.DrugBillClass.Memo = drugBillClass.DrugBillNO;
                    this.curDrugMessage.ID = drugBillClass.Oper.ID;
                    this.curDrugMessage.Name = drugBillClass.Oper.Name;
                }
                this.ShowDrugMessage(this.tvMessageBaseTree1.GetDrugMessageList(node));
                this.ShowDrugBill(this.tvMessageBaseTree1.GetDrugBill(node), true);
            }
            else
            {
                if (node.Tag == null)
                {
                    this.EnumInpatintDrugOperType = Function.EnumInpatintDrugOperType.空闲;
                    return;
                }
                if (node.Tag is FS.HISFC.Models.Pharmacy.DrugMessage)
                {
                    FS.HISFC.Models.Pharmacy.DrugMessage drugMessage = node.Tag as FS.HISFC.Models.Pharmacy.DrugMessage;
                    this.curDrugMessage = drugMessage.Clone();
                    this.ShowApplyOutDetail(drugMessage);
                    if (Function.Contains(this.NotCheckAllBillCode, drugMessage.DrugBillClass.ID))
                    {
                        this.SelectAllDetailData(false);
                    }
                   
                }
                else if (node.Tag is FS.HISFC.Models.Pharmacy.DrugBillClass)
                {
                    FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass = node.Tag as FS.HISFC.Models.Pharmacy.DrugBillClass;
                    if (this.curDrugMessage == null)
                    {
                        this.curDrugMessage = new FS.HISFC.Models.Pharmacy.DrugMessage();
                    }
                    this.curDrugMessage.ApplyDept.ID = drugBillClass.ApplyDept.ID;
                    this.curDrugMessage.ApplyDept.Name = drugBillClass.ApplyDept.Name;
                    this.curDrugMessage.DrugBillClass = drugBillClass.Clone();
                    this.curDrugMessage.DrugBillClass.Memo = drugBillClass.DrugBillNO;
                    this.curDrugMessage.StockDept.ID = this.PriveDept.ID;
                    this.curDrugMessage.ID = drugBillClass.Oper.ID;
                    this.curDrugMessage.Name = drugBillClass.Oper.Name;
                    this.ShowApplyOutDetail(drugBillClass);
                }
            }
            this.EnumInpatintDrugOperType = Function.EnumInpatintDrugOperType.空闲;
        }

        /// <summary>
        /// 显示摆药通知信息
        /// </summary>
        /// <param name="drugMessage">摆药通知信息</param>
        private void ShowDrugMessage(ArrayList alDrugMessage)
        {
            if (alDrugMessage == null)
            {
                this.ShowMessage("查询摆药通知发生错误：" + drugStoreMgr.Err, MessageBoxIcon.Error);
            }
            this.ucDrugDetail1.Clear();
            this.ucDrugDetail1.ShowDrugMessage(alDrugMessage, this.IsAutoSelected);
            this.ucDrugDetail1.SetTabPageVisible(true, false, false);
        }

        /// <summary>
        /// 显示摆药通知信息
        /// </summary>
        /// <param name="alDrugBill">摆药单数组</param>
        /// <param name="isAdd">是否追加到界面</param>
        private void ShowDrugBill(ArrayList alDrugBill, bool isAdd)
        {
            if (alDrugBill == null)
            {
                this.ShowMessage("查询摆药单发生错误：" + drugStoreMgr.Err, MessageBoxIcon.Error);
            }
            if (!isAdd)
            {
                this.ucDrugDetail1.Clear();
            }
            this.ucDrugDetail1.ShowDrugClassBill(alDrugBill, this.IsAutoSelected, isAdd);
            if (!isAdd)
            {
                this.ucDrugDetail1.SetTabPageVisible(true, false, false);
            }
        }

        /// <summary>
        /// 显示摆药通知信息
        /// </summary>
        /// <param name="drugMessage">摆药通知信息</param>
        private void ShowApplyOutDetail(FS.HISFC.Models.Pharmacy.DrugMessage drugMessage)
        {
            ArrayList alApplyOut = new ArrayList();
            if (string.IsNullOrEmpty(drugMessage.User01))
            {
                alApplyOut = this.drugStoreMgr.QueryApplyOutList(drugMessage, this.ApplyOutValidDays);
            }
            else
            {
                alApplyOut = this.drugStoreMgr.QueryApplyOutListByPatient(drugMessage, this.ApplyOutValidDays);
            }
            if (alApplyOut == null)
            {
                this.ShowMessage("查询摆药通知发生错误：" + drugStoreMgr.Err, MessageBoxIcon.Error);
                return;
            }

            this.ucDrugDetail1.Clear();
            this.ucDrugDetail1.ShowDetail(alApplyOut, true, this.EnumQtyShowType, false, Function.EnumInpatintDrugApplyType.临时发送, true);

            if (drugMessage.DrugBillClass.ID != "R")
            {
                this.SetCheck(alApplyOut);
            }

            this.ucDrugDetail1.SetTabPageVisible(false, true, true);
            FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass = this.drugStoreMgr.GetDrugBillClass(drugMessage.DrugBillClass.ID);
            if (drugBillClass != null)
            {
                if (drugBillClass.PrintType.ID.ToString() != "1")
                {
                    this.ucDrugDetail1.SelectTabPage(2);
                }

            }
            else
            {
                //维护数据被删除了，非严重错误
                this.ShowMessage("查询摆药单分类信息出错：" + drugStoreMgr.Err, MessageBoxIcon.Error);
                return;
            }


        }

        /// <summary>
        /// 显示摆药通知信息
        /// </summary>
        /// <param name="drugMessage">摆药通知信息</param>
        private void ShowApplyOutDetail(FS.HISFC.Models.Pharmacy.DrugBillClass drugBill)
        {
            ArrayList alApplyOut = this.drugStoreMgr.QueryApplyOutListByBill(drugBill.DrugBillNO, "0");

            if (alApplyOut == null)
            {
                this.ShowMessage("查询摆药单发生错误：" + drugStoreMgr.Err, MessageBoxIcon.Error);
                return;
            }

            this.ucDrugDetail1.Clear();
            this.ucDrugDetail1.ShowDetail(alApplyOut, true, this.EnumQtyShowType, false,Function.EnumInpatintDrugApplyType.临时发送,false);
            this.ucDrugDetail1.SetTabPageVisible(false, true, true);
            FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass = this.drugStoreMgr.GetDrugBillClass(drugBill.ID);
            if (drugBillClass != null)
            {
                if (drugBillClass.PrintType.ID.ToString() != "1")
                {
                    this.ucDrugDetail1.SelectTabPage(2);
                }

            }
            else
            {
                //维护数据被删除了，非严重错误
                this.ShowMessage("查询摆药单分类信息出错：" + drugStoreMgr.Err, MessageBoxIcon.Error);
                return;
            }
        }

        private void SetCheck(ArrayList alApplyOut)
        {
            Hashtable hsCheck = new Hashtable();
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alApplyOut)
            {
                if (hsCheck.Contains(applyOut.Item.ID))
                {
                    decimal qty = (decimal)hsCheck[applyOut.Item.ID];
                    qty = qty + applyOut.Operation.ApplyQty;
                    hsCheck[applyOut.Item.ID] = qty;
                }
                else
                {
                    hsCheck.Add(applyOut.Item.ID, applyOut.Operation.ApplyQty);
                }
            }
            Hashtable hsStock = new Hashtable();
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在检查库存...");
            Application.DoEvents();

            foreach(string drugNO in hsCheck.Keys)
            {
                FS.HISFC.Models.Pharmacy.Storage s= this.drugStoreMgr.GetStockInfoByDrugCode(this.StockDept.ID, drugNO);
                if (s == null)
                {
                    s = new FS.HISFC.Models.Pharmacy.Storage();
                }
                hsStock.Add(drugNO, s.StoreQty);
            }

            this.ucDrugDetail1.SelectDataAsStock(hsStock);
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        /// <summary>
        /// 发药保存
        /// </summary>
        /// <returns></returns>
        private int Save()
        {
            if (this.EnumInpatintDrugOperType != Function.EnumInpatintDrugOperType.空闲)
            {
                this.ShowStatusBarTip("正在" + this.EnumInpatintDrugOperType.ToString() + ",本次保存已取消");
                return -1;
            }
            this.EnumInpatintDrugOperType = Function.EnumInpatintDrugOperType.保存;

            int param = 0;
            ArrayList alSelectData = this.ucDrugDetail1.GetSelectData();
            if (alSelectData != null && alSelectData.Count > 0)
            {
                if (alSelectData[0] is FS.HISFC.Models.Pharmacy.DrugMessage)
                {
                    param = this.SaveDrugMessage(alSelectData);
                }
                else if(alSelectData[0] is FS.HISFC.Models.Pharmacy.ApplyOut)
                {
                    //对明细数据进行库存判断,
                    if (this.curEnumSendDrugTypeOnLackStock == Function.EnumSendDrugTypeOnLackStock.系统处理)
                    {
 
                    }
                    param = this.SaveApplyOut(alSelectData);
                }              
            }
            ArrayList alSendBill = this.ucDrugDetail1.GetSelectDrugBill();
            foreach (FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass in alSendBill)
            {
                //检索科室摆药申请明细数据
                ArrayList al = this.drugStoreMgr.QueryApplyOutListByBill(drugBillClass.DrugBillNO, "0");
                if (al == null)
                {
                    return -1;
                }
                if (al.Count > 0)
                {
                    //单号赋值后在保存时不再获取
                    this.curDrugMessage.Memo = drugBillClass.DrugBillNO;
                    if (this.curDrugMessage.Memo == "0")
                    {
                        this.curDrugMessage.Memo = "";
                    }
                    param = this.SaveApplyOut(al);
                }
            }
            //保存完后刷新数据
            if (param > 0)
            {
                this.Query();
            }
            this.EnumInpatintDrugOperType = Function.EnumInpatintDrugOperType.空闲;
            return param;
        }

        private int AutoSave()
        {
            if (this.EnumInpatintDrugOperType != Function.EnumInpatintDrugOperType.空闲 && this.EnumInpatintDrugOperType != Function.EnumInpatintDrugOperType.保存)
            {
                this.ShowStatusBarTip("正在" + this.EnumInpatintDrugOperType.ToString() + ",本次查询已取消");
                return -1;
            }
            this.EnumInpatintDrugOperType = Function.EnumInpatintDrugOperType.查询;

            this.ucDrugDetail1.Clear();
            this.tvMessageBaseTree1.Clear();
            this.curDrugMessage = null;

            #region 摆药通知形式数据处理
            ArrayList alDrugMessage = new ArrayList();
            if (this.InpatintDrugApplyType == Function.EnumInpatintDrugApplyType.全区发送)
            {
                alDrugMessage = this.drugStoreMgr.QueryDrugMessageListAfterConcentratedSend(this.DrugControl, this.ApplyOutValidDays);
            }
            else if(this.InpatintDrugApplyType == Function.EnumInpatintDrugApplyType.临时发送)
            {
                alDrugMessage = this.drugStoreMgr.QueryDrugMessageList(this.DrugControl, this.ApplyOutValidDays);
            }

            if (alDrugMessage == null)
            {
                this.ShowStatusBarTip("查询摆药通知失败：" + this.drugStoreMgr.Err);
                this.EnumInpatintDrugOperType = Function.EnumInpatintDrugOperType.空闲;

                return -1;
            }
            if (this.InpatintDrugApplyType == Function.EnumInpatintDrugApplyType.全区发送)
            {
                ArrayList alEmergency = this.drugStoreMgr.QueryEmergencyDrugMessageList(this.DrugControl, this.ApplyOutValidDays);
                if (alEmergency == null)
                {
                    this.ShowStatusBarTip("查询摆药通知失败：" + this.drugStoreMgr.Err);
                    this.EnumInpatintDrugOperType = Function.EnumInpatintDrugOperType.空闲;
                    return -1;
                }
                alDrugMessage.AddRange(alEmergency);
            }

            //保留不能保存或者保存不成功的摆药通知，显示到界面
            ArrayList alShowMessage = new ArrayList();

            string info = "";
            foreach (FS.HISFC.Models.Pharmacy.DrugMessage message in alDrugMessage)
            {

                //检索科室摆药申请明细数据
                ArrayList al = this.drugStoreMgr.QueryApplyOutList(message);
                if (al == null)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    this.ShowStatusBarTip("根据摆药通知信息获取摆药申请明细信息发生错误 " + this.drugStoreMgr.Err);
                    this.EnumInpatintDrugOperType = Function.EnumInpatintDrugOperType.空闲;
                    return -1;
                }
                if (Function.Contains(this.curUnAutoSaveDrugBillNO,message.DrugBillClass.ID))
                {
                    alShowMessage.Add(message);
                }
                else
                {
                    //第三个参数是药柜科室，传入null表示不处理药柜流程
                    if (Function.DrugConfirm(al, message, null, this.StockDept, ref info) != 1)
                    {
                        alShowMessage.Add(message);
                        this.ShowStatusBarTip(info);
                        continue;
                    }
                    else
                    {
                        //打印数据
                        this.Print(al, message, message.DrugBillClass.Memo, this.StockDept);
                    }
                }

            }
            if (alShowMessage.Count > 0)
            {
                if (this.DrugControl.ShowLevel > 1)
                {
                    ArrayList alInpatient = this.drugStoreMgr.QueryDrugMessageInpatientList(this.DrugControl, this.ApplyOutValidDays);
                    if (alInpatient == null)
                    {
                        this.ShowMessage("查询摆药通知失败：" + this.drugStoreMgr.Err, MessageBoxIcon.Error);
                        this.EnumInpatintDrugOperType = Function.EnumInpatintDrugOperType.空闲;
                        return -1;
                    }
                    this.tvMessageBaseTree1.ShowDrugMessage(alShowMessage, this.DrugControl, this.IsDrugClassBillFirst, alInpatient, this.isExpand, this.InpatintDrugApplyType == Function.EnumInpatintDrugApplyType.全区发送);

                    this.EnumInpatintDrugOperType = Function.EnumInpatintDrugOperType.空闲;
                    return 1;
                }

                this.tvMessageBaseTree1.ShowDrugMessage(alShowMessage, this.DrugControl, this.IsDrugClassBillFirst, null, this.isExpand, this.InpatintDrugApplyType == Function.EnumInpatintDrugApplyType.全区发送);
            }
            #endregion

            #region 摆药单形式的数据处理

            if (this.InpatintDrugApplyType == Function.EnumInpatintDrugApplyType.按单发送)
            {
                ArrayList alSendDrugBill = this.drugStoreMgr.QuerySendedDrugBill(this.DrugControl, this.ApplyOutValidDays);

                if (alSendDrugBill == null)
                {
                    this.ShowMessage("查询发药申请单失败：" + this.drugStoreMgr.Err, MessageBoxIcon.Error);
                    return -1;
                }

                ArrayList alShowBill = new ArrayList();

                info = "";
                foreach (FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass in alSendDrugBill)
                {

                    //检索科室摆药申请明细数据
                    ArrayList al = this.drugStoreMgr.QueryApplyOutListByBill(drugBillClass.DrugBillNO, "0");
                    if (al == null)
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.ShowStatusBarTip("根据摆药通知信息获取摆药申请明细信息发生错误 " + this.drugStoreMgr.Err);
                        this.EnumInpatintDrugOperType = Function.EnumInpatintDrugOperType.空闲;
                        return -1;
                    }
                    if (al.Count > 0)
                    {
                        if (Function.Contains(this.curUnAutoSaveDrugBillNO,drugBillClass.ID))
                        {
                            alShowBill.Add(drugBillClass);
                        }
                        else
                        {
                            //第三个参数是药柜科室，传入null表示不处理药柜流程
                            FS.HISFC.Models.Pharmacy.DrugMessage drugMessage = new FS.HISFC.Models.Pharmacy.DrugMessage();
                            drugMessage.ApplyDept.ID = drugBillClass.ApplyDept.ID;
                            drugMessage.ApplyDept.Name = drugBillClass.ApplyDept.Name;
                            drugMessage.DrugBillClass = drugBillClass.Clone();
                            drugMessage.DrugBillClass.Memo = drugBillClass.DrugBillNO;
                            drugMessage.StockDept.ID = this.PriveDept.ID;
                            drugMessage.ID = drugBillClass.Oper.ID;
                            drugMessage.Name = drugBillClass.Oper.Name;

                            if (drugMessage.DrugBillClass.ID == "R")
                            {
                                if (Function.DrugReturnConfirm(al, drugMessage, null, this.StockDept) != 1)
                                {
                                    alShowBill.Add(drugBillClass);
                                    this.ShowStatusBarTip(info);
                                    continue;
                                }
                                else
                                {
                                    //打印数据
                                    this.Print(al, drugMessage, drugMessage.DrugBillClass.Memo, this.StockDept);
                                }
                            }
                            else
                            {
                                if (Function.DrugConfirm(al, drugMessage, null, this.StockDept, ref info) != 1)
                                {
                                    alShowBill.Add(drugBillClass);
                                    this.ShowStatusBarTip(info);
                                    continue;
                                }
                                else
                                {
                                    //打印数据
                                    this.Print(al, drugMessage, drugMessage.DrugBillClass.Memo, this.StockDept);
                                }
                            }

                          
                          
                        }
                    }
                }
                if (alShowBill.Count > 0)
                {
                    this.tvMessageBaseTree1.ShowDrugBillClass(alShowBill, this.IsExpand);
                }
            }
            #endregion

            this.EnumInpatintDrugOperType = Function.EnumInpatintDrugOperType.空闲;
            return 1;
        }

        /// <summary>
        /// 保存摆药通知
        /// </summary>
        /// <param name="alData">摆药通知实体数组</param>
        /// <returns></returns>
        private int SaveDrugMessage(ArrayList alData)
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在保存...");
            Application.DoEvents();
            foreach (FS.HISFC.Models.Pharmacy.DrugMessage message in alData)
            {
                #region 对选中的申请数据进行保存

                message.SendFlag = 1;                     //摆药通知中的数据全部被核准SendFlag=1，更新摆药通知信息。
                //message.SendType = drugMessage.SendType; //处理此摆药通知中的摆药申请数据时，取摆药台的发送类型。
                message.SendType = this.DrugControl.SendType;

                //检索科室摆药申请明细数据
                ArrayList al = this.drugStoreMgr.QueryApplyOutList(message, this.ApplyOutValidDays);
                if (al == null)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    this.ShowMessage("根据摆药通知信息获取摆药申请明细信息发生错误 " + this.drugStoreMgr.Err, MessageBoxIcon.Error);
                    return -1;
                }

                #region 需要根据患者分开单据的申请
                if (Function.Contains(this.SplitPatientBillClassNO,message.DrugBillClass.ID))
                {
                    ArrayList alSampePatient = new ArrayList();
                    Hashtable hsPatientApply = new Hashtable();
                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in al)
                    {
                        if (hsPatientApply.Contains(applyOut.PatientNO))
                        {
                            ((ArrayList)hsPatientApply[applyOut.PatientNO]).Add(applyOut);
                        }
                        else
                        {
                            ArrayList alTmp = new ArrayList();
                            alTmp.Add(applyOut);
                            hsPatientApply.Add(applyOut.PatientNO, alTmp);
                        }
                    }

                    foreach (ArrayList alSamePatient in hsPatientApply.Values)
                    {
                        this.SaveOneBillApplyOut(alSamePatient, message);
                    }

                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    return 1;
                }

                #endregion

                this.SaveOneBillApplyOut(al, message);

                #endregion
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            return 1;
        }

        /// <summary>
        /// 保存摆药申请
        /// </summary>
        /// <returns></returns>
        private int SaveApplyOut(ArrayList alData)
        {
            if (this.curDrugMessage == null || string.IsNullOrEmpty(this.curDrugMessage.ID))
            {
                return 0;
            }
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在保存...");
            Application.DoEvents();

            #region 需要根据患者分开单据的申请
            if (Function.Contains(this.SplitPatientBillClassNO,this.curDrugMessage.DrugBillClass.ID))
            {
                ArrayList alSampePatient = new ArrayList();
                Hashtable hsPatientApply = new Hashtable();
                foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alData)
                {
                    if (hsPatientApply.Contains(applyOut.PatientNO))
                    {
                        ((ArrayList)hsPatientApply[applyOut.PatientNO]).Add(applyOut);
                    }
                    else
                    {
                        ArrayList alTmp = new ArrayList();
                        alTmp.Add(applyOut);
                        hsPatientApply.Add(applyOut.PatientNO, alTmp);
                    }
                }

                foreach (ArrayList alSamePatient in hsPatientApply.Values)
                {
                    this.SaveOneBillApplyOut(alSamePatient,this.curDrugMessage);
                }

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                return 1;
            }

            #endregion

            return this.SaveOneBillApplyOut(alData, this.curDrugMessage);
        }

       
        /// <summary>
        /// 保存摆药申请
        /// </summary>
        /// <param name="alData">发药申请数据，必须是可以编成同一个单号的数据</param>
        /// <param name="drugMessage">发药通知</param>
        /// <returns></returns>
        private int SaveOneBillApplyOut(ArrayList alData, FS.HISFC.Models.Pharmacy.DrugMessage drugMessage)
        {

            //正常在综合业务层会获取单号，赋值到message.DrugBillClass.Memo;
            //综合业务层对message.DrugBillClass.Memo为空的重新获取单号
            //避免引用类型在综合业务层复制后影响后面的数据保存
            string billNO = drugMessage.DrugBillClass.Memo;
            string info = "";
            if (drugMessage.DrugBillClass.ID == "R")
            {
                if (Function.DrugReturnConfirm(alData, drugMessage, null, this.StockDept) == -1)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    drugMessage.DrugBillClass.Memo = billNO;
                    return -1;
                }
            }
            else
            {
                if (Function.DrugConfirm(alData, drugMessage, null, this.StockDept, ref info) == -1)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    drugMessage.DrugBillClass.Memo = billNO;
                    this.ShowMessage("请与系统管理员联系并报告错误：" + info);
                    return -1;
                }
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            #region 工作量判断
            if (iInpatientWordLoad != null)
            {
                try
                {
                    iInpatientWordLoad.Set(this.StockDept, string.Empty, drugMessage.DrugBillClass, alData);
                }
                catch (Exception e)
                {
                    this.ShowMessage("工作量记录失败,请与系统管理员联系并报告错误：");
                }
            }
            #endregion

            //打印数据
            if (Function.Contains(this.UnPrintBillOnSave,drugMessage.DrugBillClass.ID))
            {
               
            }
            else if(Function.Contains(this.WarningPrintBillOnSave,drugMessage.DrugBillClass.ID))
            {
                if ((DialogResult)MessageBox.Show("保存成功！是否打印单据?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this.Print(alData, drugMessage, drugMessage.DrugBillClass.Memo, this.StockDept);
                }
            }
            else
            {
                this.Print(alData, drugMessage, drugMessage.DrugBillClass.Memo, this.StockDept);
            }


            drugMessage.DrugBillClass.Memo = billNO;

            return 1;
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="alPrintData">applyout实体数组</param>
        /// <param name="drugMessage">摆药通知信息，主要是取得摆药单分类名称</param>
        /// <param name="billNO">摆药单号，drugMessage的备注中有，提出来看得明白一些</param>
        /// <param name="stockDept">实际发药科室</param>
        /// <returns></returns>
        private int Print(ArrayList alPrintData, FS.HISFC.Models.Pharmacy.DrugMessage drugMessage, string billNO, FS.FrameWork.Models.NeuObject stockDept)
        {
            if (this.IInpatientDrug == null)
            {
                this.ShowMessage("没有实现接口：FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientDrug", MessageBoxIcon.Error);
                return -1;
            }
            return this.IInpatientDrug.OnSavePrint(alPrintData, drugMessage, billNO, stockDept);
            return 1;
        }

        private void SelectAllDetailData(bool isSelectAll)
        {
            this.ucDrugDetail1.SelectAllData(isSelectAll);
        }

        private void SelectDetailDataWithTime()
        {
            FS.FrameWork.WinForms.Forms.frmChooseDate frmChooseDate = new FS.FrameWork.WinForms.Forms.frmChooseDate();
            frmChooseDate.Init();
            try
            {
                string time = SOC.Public.XML.SettingFile.ReadSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InpatientDrugStore.xml", "CurDrugBeginUsetime", "Dept" + this.PriveDept.ID, frmChooseDate.DateBegin.ToString("HH:mm:ss"));
                DateTime dt = DateTime.Parse(frmChooseDate.DateBegin.ToString("yyyy-MM-dd " + time));
                frmChooseDate.DateBegin = dt;

                time = SOC.Public.XML.SettingFile.ReadSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InpatientDrugStore.xml", "CurDrugEndUsetime", "Dept" + this.PriveDept.ID, frmChooseDate.DateEnd.ToString("HH:mm:ss"));
                dt = DateTime.Parse(frmChooseDate.DateEnd.ToString("yyyy-MM-dd " + time));
                frmChooseDate.DateEnd = dt;
            }
            catch { }

            frmChooseDate.ShowDialog(this);
            
            SOC.Public.XML.SettingFile.SaveSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InpatientDrugStore.xml", "CurDrugBeginUsetime", "Dept" + this.PriveDept.ID, frmChooseDate.DateBegin.ToString("HH:mm:ss"));
            SOC.Public.XML.SettingFile.SaveSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InpatientDrugStore.xml", "CurDrugEndUsetime", "Dept" + this.PriveDept.ID, frmChooseDate.DateEnd.ToString("HH:mm:ss"));
            this.ucDrugDetail1.SelectData(frmChooseDate.DateBegin, frmChooseDate.DateEnd);
        }

        #endregion

        #region 事件
        protected override int OnQuery(object sender, object neuObject)
        {
            if (this.EnumInpatintDrugOperType != Function.EnumInpatintDrugOperType.空闲)
            {
                this.ShowMessage("正在" + this.EnumInpatintDrugOperType.ToString() + ",请稍后再试...", MessageBoxIcon.Information);
                return 0;
            }
            this.EnumInpatintDrugOperType = Function.EnumInpatintDrugOperType.手工刷新;

            int param = this.Query();

            this.EnumInpatintDrugOperType = Function.EnumInpatintDrugOperType.空闲;

            return param;
        }

        protected override int OnSave(object sender, object neuObject)
        {
            if (this.EnumInpatintDrugOperType != Function.EnumInpatintDrugOperType.空闲)
            {
                this.ShowMessage("正在" + this.EnumInpatintDrugOperType.ToString() + ",请稍后再试...", MessageBoxIcon.Information);
                return 0;
            }
            this.EnumInpatintDrugOperType = Function.EnumInpatintDrugOperType.保存;

            int param = this.Save();
           

            this.EnumInpatintDrugOperType = Function.EnumInpatintDrugOperType.空闲;

            return param;
        }

        protected override void OnLoad(EventArgs e)
        {
            this.InitInterface();
            if (this.IsShowDrugControlByTab)
            {
                this.InitDrugControlInTab();
                this.ntbDrugControl.SelectedIndexChanged += new EventHandler(ntbDrugControl_SelectedIndexChanged);
            }
            this.tvMessageBaseTree1.AfterSelect += new TreeViewEventHandler(tvMessageBaseTree1_AfterSelect);
            //this.ucQueryInpatientNo1.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(ucQueryInpatientNo1_myEvent);
            this.ucQueryInpatientNO.txtInputCode.KeyUp += new KeyEventHandler(txtInputCode_KeyUp);
            this.nlbInfo.DoubleClick += new EventHandler(nlbInfo_DoubleClick);
            this.ncbPauseRefresh.CheckedChanged += new EventHandler(ncbPauseRefresh_CheckedChanged);
            this.ncbAutoPrintRegister.CheckedChanged+=new EventHandler(ncbAutoPrintRegister_CheckedChanged);

            this.ucDrugDetail1.HightLightDept = this.HightLightDept;
            if (this.IsQueryOnLoad)
            {
                this.Query();
            }

            //自动刷新不一定自动打印，自动打印肯定要自动刷新，自动打印还要自动保存
            //自动打印默认需要打印注册，界面属性IsRegisterAutoPrint设置
            //自动刷新是在界面属性里设置的RefreshInterval
            if (this.RefreshInterval > 0 || this.DrugControl.IsAutoPrint)
            {
                this.ncbPauseRefresh.Visible = true;
                if (this.refreshInterval <= 0)//设置了自动刷新，必须设置间隔时间， 默认30秒
                {
                    this.refreshInterval = 300;
                }
                this.BeginAutoRefresh();

                string curPauseRefreshCheckState = "True";
                curPauseRefreshCheckState = SOC.Public.XML.SettingFile.ReadSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InpatientDrugStore.xml", "CurPauseRefresh", "Dept" + this.PriveDept.ID, curPauseRefreshCheckState);
                this.ncbPauseRefresh.Checked = FS.FrameWork.Function.NConvert.ToBoolean(curPauseRefreshCheckState);
            }
            else
            {
                this.ncbPauseRefresh.Visible = false;
                this.nlbInfo.Location = new Point(this.ncbPauseRefresh.Location.X, this.ncbPauseRefresh.Location.Y);
            }
            base.OnLoad(e);
        }

        private void InitDrugControlInTab()
        {
            this.ntbDrugControl.Visible = true;

            ArrayList allDrugControl = this.drugStoreMgr.QueryDrugControlList(this.StockDept.ID);

            this.ntbDrugControl.TabPages.Clear();

            if (allDrugControl == null || allDrugControl.Count == 0)
            {
                MessageBox.Show("没有找到相应的配药台信息，请及时进行维护！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.ntbDrugControl.Width = 50;

            int selectIndex = 0;
            int irow = 0;
            foreach (FS.HISFC.Models.Pharmacy.DrugControl drugControl in allDrugControl)
            {
                System.Windows.Forms.TabPage tabPage = new System.Windows.Forms.TabPage();
                tabPage.Text = drugControl.Name + "(F" + (irow + 1) + ")";
                tabPage.Tag = drugControl;
                tabPage.Width = 100;
                tabPage.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                this.ntbDrugControl.Controls.Add(tabPage);
                this.ntbDrugControl.Width += tabPage.Width;
                if (this.DrugControl.ID == drugControl.ID)
                {
                    selectIndex = irow;
                }
                irow++;
            }
            this.ntbDrugControl.SelectedIndex = selectIndex;
        }

        void ntbDrugControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.EnumInpatintDrugOperType != Function.EnumInpatintDrugOperType.空闲)
            {
                this.ShowStatusBarTip("正在" + this.EnumInpatintDrugOperType.ToString() + ",本次操作已取消");
                return;
            }
            this.EnumInpatintDrugOperType = Function.EnumInpatintDrugOperType.查询;
            //记录原来选择摆药台，用户选择取消时则不更改摆药台
            FS.HISFC.Models.Pharmacy.DrugControl myDrugControl = this.DrugControl;
            this.DrugControl = this.ntbDrugControl.SelectedTab.Tag as FS.HISFC.Models.Pharmacy.DrugControl;
            //用户可能关闭窗口或者选择取消
            if (this.DrugControl == null)
            {
                this.DrugControl = myDrugControl;
            }

            this.Query();

            this.nlbInfo.Text = "您当前选择的是：【" + this.priveDept.Name + "】的【" + this.DrugControl.Name + "】    需要选择其他摆药台时请退出窗口后重新进入";

            this.EnumInpatintDrugOperType = Function.EnumInpatintDrugOperType.空闲;
        }

        void ntbDrugControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            Font fntTab;
            Brush bshBack;
            Brush bshFore;
            if (e.Index == this.ntbDrugControl.SelectedIndex)    //当前Tab页的样式
            {
                fntTab = new Font(e.Font,FontStyle.Underline);
                bshBack = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, System.Drawing.Color.Yellow, System.Drawing.Color.Yellow, System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal);
                bshFore = Brushes.Red;
            }
            else    //其余Tab页的样式
            {
                fntTab = new Font(e.Font, FontStyle.Regular);
                bshBack = new SolidBrush(System.Drawing.SystemColors.Control);
                bshFore = new SolidBrush(Color.Black);
            }
            //画样式
            string tabName = this.ntbDrugControl.TabPages[e.Index].Text;
            StringFormat sftTab = new StringFormat();
            e.Graphics.FillRectangle(bshBack, e.Bounds);
            Rectangle recTab = e.Bounds;
            recTab = new Rectangle(recTab.X, recTab.Y + 4, recTab.Width, recTab.Height - 4);
            e.Graphics.DrawString(tabName, fntTab, bshFore, recTab, sftTab);
        }

        private void InitInterface()
        {
            iInpatientWordLoad = (FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientWorkLoad)FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.SOC.HISFC.Components.DrugStore.Inpatient.Common.ucDrug), typeof(FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientWorkLoad));
        }

        void ncbPauseRefresh_CheckedChanged(object sender, EventArgs e)
        {
            SOC.Public.XML.SettingFile.SaveSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InpatientDrugStore.xml", "CurPauseRefresh", "Dept" + this.PriveDept.ID, ncbPauseRefresh.Checked.ToString());
        }

        void nlbInfo_DoubleClick(object sender, EventArgs e)
        {
            if (this.EnumInpatintDrugOperType != Function.EnumInpatintDrugOperType.空闲)
            {
                this.ShowStatusBarTip("正在" + this.EnumInpatintDrugOperType.ToString() + ",本次操作已取消");
                return;
            }
            this.EnumInpatintDrugOperType = Function.EnumInpatintDrugOperType.查询;

            //记录原来选择摆药台，用户选择取消时则不更改摆药台
            FS.HISFC.Models.Pharmacy.DrugControl myDrugControl = this.DrugControl;
            this.DrugControl = Function.ControlSelect(this.PriveDept);
            
            //用户可能关闭窗口或者选择取消
            if (this.DrugControl == null)
            {
                this.DrugControl = myDrugControl;
            }

            this.nlbInfo.Text = "您当前选择的是：【" + this.priveDept.Name + "】的【" + this.DrugControl.Name + "】    需要选择其他摆药台时请退出窗口后重新进入";

            this.Query();

            this.EnumInpatintDrugOperType = Function.EnumInpatintDrugOperType.空闲;
        }

        void txtInputCode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (this.EnumInpatintDrugOperType != Function.EnumInpatintDrugOperType.空闲)
                {
                    this.ShowStatusBarTip("正在" + this.EnumInpatintDrugOperType.ToString() + ",本次查询已取消");
                    return;
                }

                this.tvMessageBaseTree1.FindPatient(this.ucQueryInpatientNO.InpatientNo, this.DrugControl, this.IsDrugClassBillFirst);
            }
        }

        void ucQueryInpatientNo1_myEvent()
        {
            if (this.EnumInpatintDrugOperType != Function.EnumInpatintDrugOperType.空闲)
            {
                this.ShowStatusBarTip("正在" + this.EnumInpatintDrugOperType.ToString() + ",本次查询已取消");
                return;
            }

            this.tvMessageBaseTree1.FindPatient(this.ucQueryInpatientNO.InpatientNo, this.DrugControl, this.IsDrugClassBillFirst);
        }

        void tvMessageBaseTree1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.ShowDetail(e.Node);
        }

        void ncbAutoPrintRegister_CheckedChanged(object sender, EventArgs e)
        {
            this.ncbAutoPrintRegister.CheckedChanged -= new EventHandler(ncbAutoPrintRegister_CheckedChanged);
            this.ncbAutoPrintRegister.Checked = !this.ncbAutoPrintRegister.Checked;
            if (!this.DrugControl.IsAutoPrint)
            {
                this.ShowMessage("程序没有设置自动打印功能！");
                this.ncbAutoPrintRegister.CheckedChanged += new EventHandler(ncbAutoPrintRegister_CheckedChanged);
                return;
            }
            if (!this.ncbAutoPrintRegister.Checked)
            {
                //注册打印
                bool successful = Function.RegisterControlAutoPrint(this.DrugControl);

                this.ncbAutoPrintRegister.Checked = successful;
            }
            else
            {
                this.ShowMessage("取消自动打印注册请在摆药台维护中操作，如果您没有权限请与药房负责人联系！");
                this.ncbAutoPrintRegister.Checked = true;

            }
            this.ncbAutoPrintRegister.CheckedChanged += new EventHandler(ncbAutoPrintRegister_CheckedChanged);
        }

        #endregion

        #region 工具栏

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            ToolBarService.AddToolButton("病区发送情况", "病区发送情况", FS.FrameWork.WinForms.Classes.EnumImageList.C查找, true, false, null);
            ToolBarService.AddToolButton("全选", "选择所有数据", FS.FrameWork.WinForms.Classes.EnumImageList.Q全选, true, false, null);
            ToolBarService.AddToolButton("全不选", "取消所有数据选择", FS.FrameWork.WinForms.Classes.EnumImageList.Q全不选, true, false, null);
            ToolBarService.AddToolButton("用药时间", "按照用药时间选择数据", FS.FrameWork.WinForms.Classes.EnumImageList.R日期, true, false, null);
          
            base.OnInit(sender, neuObject, param);
            return this.ToolBarService;
        }

        /// <summary>
        /// 快捷键事件
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            string keyName = keyData.ToString();
            if (keyName.Contains("F"))
            {
                foreach (TabPage tp in this.ntbDrugControl.TabPages)
                {
                    if (tp.Text.Contains(keyName))
                    { 
                        this.ntbDrugControl.SelectedIndex = FS.FrameWork.Function.NConvert.ToInt32(keyName.Replace('F',' ').Trim()) - 1;
                    }
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "全选" || e.ClickedItem.Text == "全    选")
            {
                this.SelectAllDetailData(true);
            }
            else if (e.ClickedItem.Text == "全不选" || e.ClickedItem.Text == "全 不 选")
            {
                this.SelectAllDetailData(false);
            }
            else if (e.ClickedItem.Text == "用药时间")
            {
                this.SelectDetailDataWithTime();
            }
            else if (e.ClickedItem.Text == "病区发送情况")
            {
                this.ShowNurseCellSend();
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        /// <summary>
        /// 查看病区发送情况
        /// </summary>
        private void ShowNurseCellSend()
        {
            ucNurseCellSendQuery ucNurseCellSendQuery = new ucNurseCellSendQuery();
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucNurseCellSendQuery);
        }

        #endregion

    }
}
