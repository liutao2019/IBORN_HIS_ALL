using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using FS.SOC.HISFC.Assign.Interface.Components;
using FS.FrameWork.Public;

namespace FS.SOC.HISFC.Assign.Components.Maintenance.Queue
{
    /// <summary>
    /// [功能描述: 队列管理界面]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    public partial class ucQueueManager : Base.ucAssignBase, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucQueueManager()
        {
            InitializeComponent();
        }

        #region 接口变量

        IDataList INurseManager = null;

        IMaintenance<KeyValuePair<FS.SOC.HISFC.Assign.Models.Queue, Day>> IQueue = null;

        Dictionary<DateTime, ITriageQueue> IQueueDictionary = new Dictionary<DateTime, ITriageQueue>();

        #endregion

        #region 重载变量

        private new Dictionary<string, FS.FrameWork.Models.NeuObject> priveNurse = new Dictionary<string, FS.FrameWork.Models.NeuObject>();
        public new Dictionary<string, FS.FrameWork.Models.NeuObject> PriveNurse
        {
            get
            {
                return priveNurse;
            }
            set
            {
                priveNurse = value;
            }
        }

        #endregion

        #region 综合变量
        private FS.SOC.HISFC.Assign.BizProcess.Queue queueBiz = new FS.SOC.HISFC.Assign.BizProcess.Queue();
        #endregion

        #region 工具栏

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("刷新", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S刷新, true, false, null);
            this.toolBarService.AddToolButton("添加队列", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T添加, true, false, null);
            this.toolBarService.AddToolButton("修改队列", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X修改, true, false, null);
            this.toolBarService.AddToolButton("删除队列", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
            this.toolBarService.AddToolButton("复制模板", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.M模版, true, false, null);
            this.toolBarService.AddToolButton("生成队列", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q全选, true, false, null);
            this.toolBarService.AddToolButton("下周", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X下一个, true, false, null);
            this.toolBarService.AddToolButton("上周", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S上一个, true, false, null);
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
                case "刷新":
                    this.Refresh();
                    break;
                case "添加队列":
                    this.addQueue();
                    break;
                case "修改队列":
                    this.modifyQueue();
                    break;
                case "删除队列":
                    this.deleteQueue();
                    break;
                case "复制模板":
                    this.copyQueueTemplate();
                    break;
                case "生成队列":
                    this.autoCreateQueue();
                    break;
                case "上周":
                    this.lastWeek();
                    break;
                case "下周":
                    this.nextWeek();
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region 初始化

        public int Init()
        {
            this.initInterface();
            this.initDatas();
            this.initEvents();
            this.initMenus();

            DateTime dtNow = CommonController.CreateInstance().GetSystemTime().Date;
            if (this.neuTabControl1.TabPages.ContainsKey(dtNow.ToString("yyyy-MM-dd")))
            {
                this.neuTabControl1.SelectedTab = this.neuTabControl1.TabPages[dtNow.ToString("yyyy-MM-dd")];
            }
            return 1;
        }

        private int initInterface()
        {
            #region INurseManager

            INurseManager = InterfaceManager.GetINurseStations();
            INurseManager.Init();
            INurseManager.Clear();
            if (this.INurseManager is Control)
            {
                this.plNurseStation.Controls.Clear();
                //加载界面
                ((Control)this.INurseManager).Dock = DockStyle.Fill;
                this.plNurseStation.Controls.Add((Control)this.INurseManager);
            }
            #endregion
            
            DateTime dtNow = CommonController.CreateInstance().GetSystemTime();
            this.initIQueueDictionary(dtNow);

            return 1;
        }

        private int initIQueueDictionary(DateTime dtNow)
        {
            #region IQueueDictionary

            this.neuTabControl1.SelectedIndexChanged -= new EventHandler(neuTabControl1_SelectedIndexChanged);

            this.neuTabControl1.Controls.Clear();
            this.IQueueDictionary.Clear();
            Day weekday = EnumHelper.Current.GetEnum<Day>(dtNow.DayOfWeek.ToString());

            for (int i = 0; i < 7; i++)
            {
                DateTime dt = dtNow.Date.AddDays(i - (int)weekday);
                IQueueDictionary[dt] = InterfaceManager.GetITriageQueue();
                IQueueDictionary[dt].Init();
                IQueueDictionary[dt].Clear();
                if (this.IQueueDictionary[dt] is Control)
                {
                    //TabPage
                    TabPage page = new TabPage(dt.ToShortDateString() + "(" + System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(EnumHelper.Current.GetEnum<DayOfWeek>(((Day)i).ToString())) + ")");
                    page.Name = dt.ToString("yyyy-MM-dd");
                    ((Control)this.IQueueDictionary[dt]).Dock = DockStyle.Fill;
                    page.Controls.Add(((Control)this.IQueueDictionary[dt]));

                    this.neuTabControl1.Controls.Add(page);
                }
            }
            if (this.neuTabControl1.TabPages.ContainsKey(dtNow.ToString("yyyy-MM-dd")))
            {
                this.neuTabControl1.SelectedTab = this.neuTabControl1.TabPages[dtNow.ToString("yyyy-MM-dd")];
            }

            foreach (KeyValuePair<DateTime, ITriageQueue> de in this.IQueueDictionary)
            {
                de.Value.DoubleClickItem -= new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Queue>(IQueue_DoubleClickItem);
                de.Value.DoubleClickItem += new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Queue>(IQueue_DoubleClickItem);

                de.Value.SelectedQueueChange -= new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Queue>(IQueue_SelectedQueueChange);
                de.Value.SelectedQueueChange += new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Queue>(IQueue_SelectedQueueChange);
            }
            this.neuTabControl1.SelectedIndexChanged += new EventHandler(neuTabControl1_SelectedIndexChanged);
            #endregion

            return 1;
        }

        private int initDatas()
        {
            FS.HISFC.BizLogic.Manager.DepartmentStatManager statManager = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
            //取诊室、诊台
            if (this.PriveNurse != null)
            {
                ArrayList alDeptStat = new ArrayList();
                foreach (FS.FrameWork.Models.NeuObject nurse in this.PriveNurse.Values)
                {
                    //查找护士站下面的科室
                    ArrayList al = statManager.LoadByParent("14", nurse.ID);
                    if (al != null)
                    {
                        alDeptStat.AddRange(al);
                    }
                }

                this.INurseManager.AddRange(alDeptStat);

                if (alDeptStat.Count > 0)
                {
                    this.cmbDept.AddItems(alDeptStat);
                }
            }

            return 1;
        }

        private int initEvents()
        {
            this.INurseManager.SelectedDeptChange -= new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.FrameWork.Models.NeuObject, ArrayList>(INurse_SelectedDeptChange);
            this.INurseManager.SelectedDeptChange += new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.FrameWork.Models.NeuObject, ArrayList>(INurse_SelectedDeptChange);

            this.neuTabControl1.SelectedIndexChanged -= new EventHandler(neuTabControl1_SelectedIndexChanged);
            this.neuTabControl1.SelectedIndexChanged += new EventHandler(neuTabControl1_SelectedIndexChanged);

            this.cmbDept.SelectedIndexChanged -= new EventHandler(cmbDept_SelectedIndexChanged);
            this.cmbDept.SelectedIndexChanged += new EventHandler(cmbDept_SelectedIndexChanged);

            this.dtpDate.ValueChanged -= new EventHandler(dtpDate_ValueChanged);
            this.dtpDate.ValueChanged += new EventHandler(dtpDate_ValueChanged);

            return 1;
        }

        private int initMenus()
        {
            #region Queue
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            ToolStripMenuItem menu = new ToolStripMenuItem("添加队列");
            menu.Click += new EventHandler(menu_Click);
            contextMenu.Items.Add(menu);

            ToolStripSeparator spearator = new ToolStripSeparator();
            contextMenu.Items.Add(spearator);

            menu = new ToolStripMenuItem("修改队列");
            menu.Click += new EventHandler(menu_Click);
            contextMenu.Items.Add(menu);

            spearator = new ToolStripSeparator();
            contextMenu.Items.Add(spearator);

            menu = new ToolStripMenuItem("删除队列");
            menu.Click += new EventHandler(menu_Click);
            contextMenu.Items.Add(menu);

            spearator = new ToolStripSeparator();
            contextMenu.Items.Add(spearator);

            menu = new ToolStripMenuItem("复制模板");
            menu.Click += new EventHandler(menu_Click);
            contextMenu.Items.Add(menu);

            spearator = new ToolStripSeparator();
            contextMenu.Items.Add(spearator);

            menu = new ToolStripMenuItem("生成队列");
            menu.Click += new EventHandler(menu_Click);
            contextMenu.Items.Add(menu);

            spearator = new ToolStripSeparator();
            contextMenu.Items.Add(spearator);

            menu = new ToolStripMenuItem("刷新");
            menu.Click += new EventHandler(menu_Click);
            contextMenu.Items.Add(menu);

            this.neuTabControl1.ContextMenuStrip = contextMenu;

            #endregion
            return 1;
        }

        private int initIQueue()
        {
            if (this.IQueue == null)
            {
                IQueue = InterfaceManager.GetIQueue();
                IQueue.Init();
                this.IQueue.SaveInfo -= new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<KeyValuePair<FS.SOC.HISFC.Assign.Models.Queue, Day>>(IQueue_SaveInfo);
                this.IQueue.SaveInfo += new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<KeyValuePair<FS.SOC.HISFC.Assign.Models.Queue, Day>>(IQueue_SaveInfo);
                this.IQueue.Init(new ArrayList(this.PriveNurse.Values));
            }

            return 1;
        }

        #endregion

        #region 方法

        private int addQueue()
        {
            if (this.neuTabControl1.SelectedTab == null)
            {
                return -1;
            }

            FS.SOC.HISFC.Assign.Models.Queue queue = new FS.SOC.HISFC.Assign.Models.Queue();

            FS.HISFC.Models.Base.DepartmentStat deptStat = (this.INurseManager.SelectItem ?? this.INurseManager.FirstItem) as FS.HISFC.Models.Base.DepartmentStat;

            if (deptStat != null)
            {
                queue.AssignNurse.ID = deptStat.PardepCode;
                queue.AssignDept.ID = deptStat.ID;
            }

            queue.QueueDate = FS.FrameWork.Function.NConvert.ToDateTime(this.neuTabControl1.SelectedTab.Name);

            this.initIQueue();

            this.IQueue.Add(new KeyValuePair<FS.SOC.HISFC.Assign.Models.Queue, Day>(queue, Day.Default));

            if (this.IQueue is Control)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowControl((Control)this.IQueue, FormBorderStyle.FixedToolWindow, FormWindowState.Normal);
            }

            return 1;
        }

        private int modifyQueue()
        {
            return this.modifyQueue(null, FS.FrameWork.Function.NConvert.ToDateTime(this.neuTabControl1.SelectedTab.Name));
        }

        private int modifyQueue(FS.SOC.HISFC.Assign.Models.Queue queue, DateTime dt)
        {
            if (this.IQueueDictionary.ContainsKey(dt.Date)==false)
            {
                return -1;
            }

            queue = queue ?? this.IQueueDictionary[dt.Date].Queue;

            if (queue == null)
            {
                return -1;
            }

            this.initIQueue();

            this.IQueue.Modify(new KeyValuePair<FS.SOC.HISFC.Assign.Models.Queue, Day>(queue, Day.Default));

            if (this.IQueue is Control)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowControl((Control)this.IQueue, FormBorderStyle.FixedToolWindow, FormWindowState.Normal);
            }

            return 1;
        }

        private int deleteQueue()
        {
            if (this.neuTabControl1.SelectedTab == null)
            {
                return -1;
            }

            DateTime dt = FS.FrameWork.Function.NConvert.ToDateTime(this.neuTabControl1.SelectedTab.Name);

            if (this.IQueueDictionary.ContainsKey(dt.Date) == false)
            {
                return -1;
            }

            FS.SOC.HISFC.Assign.Models.Queue queue = this.IQueueDictionary[dt.Date].Queue as FS.SOC.HISFC.Assign.Models.Queue;

            if (queue == null)
            {
                return -1;
            }

            if (queue != null)
            {
                if (CommonController.CreateInstance().MessageBox(this, "是否要删除队列：" + queue.Name + "信息?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return -1;
                }


                string error = "";
                if (queueBiz.SaveQueue(queue, FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Delete, ref error) <= 0)
                {
                    CommonController.CreateInstance().MessageBox(this, error, MessageBoxIcon.Error);
                    return -1;
                }

                this.IQueueDictionary[FS.FrameWork.Function.NConvert.ToDateTime(this.neuTabControl1.SelectedTab.Name).Date].Remove(queue);
            }

            return 1;
        }

        /// <summary>
        /// 复制模板
        /// </summary>
        /// <returns></returns>
        private int copyQueueTemplate()
        {
            if (this.neuTabControl1.SelectedTab == null)
            {
                return -1;
            }

            DateTime dt = FS.FrameWork.Function.NConvert.ToDateTime(this.neuTabControl1.SelectedTab.Name);

            frmSelectWeek frmSelectWeek = new frmSelectWeek();
            frmSelectWeek.SelectedWeek = EnumHelper.Current.GetEnum<Day>(dt.DayOfWeek.ToString());
            frmSelectWeek.ShowDialog(this);
            if (frmSelectWeek.DialogResult == DialogResult.Yes)
            {
                return this.copyQueueTemplate(dt, frmSelectWeek.SelectedWeek);
            }

            return 1;
        }

        /// <summary>
        /// 复制模板
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        private int copyQueueTemplate(DateTime dt, Day day)
        {
            if (this.IQueueDictionary.ContainsKey(dt.Date) == false)
            {
                return -1;
            }
            FS.HISFC.Models.Base.DepartmentStat deptStat = (this.INurseManager.SelectItem ?? this.INurseManager.FirstItem) as FS.HISFC.Models.Base.DepartmentStat;
            if (deptStat == null)
            {
                return -1;
            }

            //查找模板
            FS.SOC.HISFC.Assign.BizLogic.QueueTemplate queueTemplateMgr = new FS.SOC.HISFC.Assign.BizLogic.QueueTemplate();
            ArrayList al = queueTemplateMgr.QueryValidByNurseID(deptStat.PardepCode, EnumHelper.Current.GetEnum<DayOfWeek>(day.ToString()));
            if (al == null)
            {
                CommonController.CreateInstance().MessageBox(this, queueTemplateMgr.Err, MessageBoxIcon.Error);
                return -1;
            }

            string error = "";
            ArrayList alQueue = new ArrayList();
            foreach (FS.SOC.HISFC.Assign.Models.QueueTemplate queue in al)
            {
                if (!queue.WeekDay.ToString().Equals(day.ToString()))
                {
                    continue;
                }
                queue.ID = "";
                queue.QueueDate = dt.Date;
                int ret = queueBiz.SaveQueue(queue, FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Insert, ref error);
                if (ret < 0)
                {
                    CommonController.CreateInstance().MessageBox(this, error, MessageBoxIcon.Error);
                    return -1;
                }
            }

            this.Refresh();

            return 1;
        }

        private int autoCreateQueue()
        {
            frmSelectDays frmSelectDays = new frmSelectDays();
            frmSelectDays.ShowDialog(this);
            if (frmSelectDays.DialogResult == DialogResult.Yes)
            {
                try
                {
                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在生成，请稍后...");
                    Application.DoEvents();
                    return this.autoCreateQueue(frmSelectDays.Days);
                }
                finally
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                }
            }

            return 1;
        }

        private int autoCreateQueue(int days)
        {
            DateTime dtNow = CommonController.CreateInstance().GetSystemTime();
            //查找模板
            FS.HISFC.Models.Base.DepartmentStat deptStat = (this.INurseManager.SelectItem ?? this.INurseManager.FirstItem) as FS.HISFC.Models.Base.DepartmentStat;
            if (deptStat == null)
            {
                return -1;
            }


            FS.SOC.HISFC.Assign.BizLogic.QueueTemplate queueTemplateMgr = new FS.SOC.HISFC.Assign.BizLogic.QueueTemplate();
            ArrayList al = queueTemplateMgr.QueryValidByNurseID(deptStat.PardepCode);
            if (al == null)
            {
                CommonController.CreateInstance().MessageBox(this, queueTemplateMgr.Err, MessageBoxIcon.Error);
                return -1;
            }
            Dictionary<DayOfWeek, ArrayList> dictionary = new Dictionary<DayOfWeek, ArrayList>();

            string error = "";
            foreach (FS.SOC.HISFC.Assign.Models.QueueTemplate queue in al)
            {
                if (dictionary.ContainsKey(queue.WeekDay))
                {
                    dictionary[queue.WeekDay].Add(queue);
                }
                else
                {
                    dictionary[queue.WeekDay] = new ArrayList();
                    dictionary[queue.WeekDay].Add(queue);
                }
            }
            ArrayList alQueue = new ArrayList();
            for (int i = 0; i < days; i++)
            {
                DateTime dt = dtNow.AddDays(i);
                if (dictionary.ContainsKey(dt.DayOfWeek))
                {
                    foreach (FS.SOC.HISFC.Assign.Models.QueueTemplate queue in dictionary[dt.DayOfWeek])
                    {
                        queue.ID = "";
                        queue.QueueDate = dt.Date;
                        int ret = queueBiz.SaveQueue(queue, FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Insert, ref error);
                        if (ret < 0)
                        {
                            CommonController.CreateInstance().MessageBox(this, error, MessageBoxIcon.Error);
                            return -1;
                        }
                    }
                }
            }

            this.Refresh();

            return 1;
        }

        private int lastWeek()
        {
            this.initIQueueDictionary(FS.FrameWork.Function.NConvert.ToDateTime(this.neuTabControl1.SelectedTab.Name).AddDays(-7));

            this.Refresh();

            return 1;
        }

        private int nextWeek()
        {
          this.initIQueueDictionary(FS.FrameWork.Function.NConvert.ToDateTime(this.neuTabControl1.SelectedTab.Name).AddDays(7));

          this.Refresh();

          return 1;
        }

        private void refresh(string nurseID, DateTime dt,ArrayList alDept)
        {
            FS.SOC.HISFC.Assign.BizLogic.Queue queueTemplateMgr = new FS.SOC.HISFC.Assign.BizLogic.Queue();
            ArrayList al = queueTemplateMgr.QueryAllByNurseID(nurseID, dt);
            if (al == null)
            {
                CommonController.CreateInstance().MessageBox(this, queueTemplateMgr.Err, MessageBoxIcon.Error);
                return;
            }

            ArrayList alQueue = new ArrayList();
            if (alDept != null)
            {
                Hashtable dictionary = new Hashtable();
                foreach (FS.FrameWork.Models.NeuObject obj in alDept)
                {
                    if (!dictionary.ContainsKey(obj.ID))
                    {
                        dictionary.Add(obj.ID, null);
                    }
                }


                foreach (FS.SOC.HISFC.Assign.Models.Queue queue in al)
                {
                    if (dictionary.ContainsKey(queue.AssignDept.ID))
                    {
                        alQueue.Add(queue);
                    }
                }
            }
            else
            {
                alQueue.AddRange(al);
            }

            if (this.IQueueDictionary.ContainsKey(dt.Date))
            {
                this.IQueueDictionary[dt.Date].AddQueue(alQueue);
            }
        }

        #endregion

        #region 触发事件

        protected override void OnLoad(EventArgs e)
        {
            this.Init();
            base.OnLoad(e);
        }

        void INurse_SelectedDeptChange(FS.FrameWork.Models.NeuObject nurse, ArrayList alDept)
        {
            //只需要科室就可以了
            if (nurse == null)
            {
                return;
            }

            if (this.IQueueDictionary.Count == 0)
            {
                return;
            }

            //查找护士站所有的模板信息

            DateTime dt = FS.FrameWork.Function.NConvert.ToDateTime(this.neuTabControl1.SelectedTab.Name);

            this.refresh(nurse.ID, dt,alDept);
        }

        void neuTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }

        void IQueue_SelectedQueueChange(FS.SOC.HISFC.Assign.Models.Queue o)
        {
            if (o == null)
            {
                return;
            }

            this.lbQueueType.Text = "队列类型：" + EnumHelper.Current.GetName(o.QueueType);
            this.lbReglevel.Text = "挂号级别：" + FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().GetRegLevelName(o.RegLevel.ID);
            this.lbSeeDoctor.Text = "看诊医生：" + CommonController.CreateInstance().GetEmployeeName(o.Doctor.ID);
            this.lbRoom.Text = "诊室：" + o.SRoom.Name;
            this.lbConsole.Text = "诊台：" + o.Console.Name;
        }

        void IQueue_DoubleClickItem(FS.SOC.HISFC.Assign.Models.Queue o)
        {
            if (o == null)
            {
                return;
            }

            this.modifyQueue(o,FS.FrameWork.Function.NConvert.ToDateTime(this.neuTabControl1.SelectedTab.Name));
        }

        void IQueue_SaveInfo(KeyValuePair<FS.SOC.HISFC.Assign.Models.Queue, Day> keyvalue)
        {
            if (keyvalue.Key == null)
            {
                return;
            }

            if (this.IQueueDictionary.ContainsKey(keyvalue.Key.QueueDate.Date))
            {
                this.IQueueDictionary[keyvalue.Key.QueueDate.Date].Add(keyvalue.Key);
            }
            else
            {
                this.Refresh();
            }
        }

        public override void Refresh()
        {
            if (this.neuTabControl1.SelectedTab == null)
            {
                return;
            }

            FS.HISFC.Models.Base.DepartmentStat deptStat = (this.INurseManager.SelectItem ?? this.INurseManager.FirstItem) as FS.HISFC.Models.Base.DepartmentStat;
            if (deptStat != null)
            {
                DateTime dt = FS.FrameWork.Function.NConvert.ToDateTime(this.neuTabControl1.SelectedTab.Name);
                this.refresh(deptStat.PardepCode, dt,null);
            }

            base.Refresh();
        }

        void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            string date = this.dtpDate.Value.Date.ToString("yyyy-MM-dd");
            if (this.neuTabControl1.TabPages.ContainsKey(date))
            {
                this.neuTabControl1.SelectedTab = this.neuTabControl1.TabPages[date];
            }
            else
            {
                this.initIQueueDictionary(this.dtpDate.Value);
                if (this.neuTabControl1.TabPages.ContainsKey(date))
                {
                    this.neuTabControl1.SelectedTab = this.neuTabControl1.TabPages[date];
                }
            }
        }

        void menu_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripItem)
            {
                this.ToolStrip_ItemClicked(sender, new ToolStripItemClickedEventArgs((ToolStripItem)sender));
            }
        }

        void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbDept.SelectedItem is FS.HISFC.Models.Base.DepartmentStat)
            {
                FS.HISFC.Models.Base.DepartmentStat deptStat = this.cmbDept.SelectedItem as FS.HISFC.Models.Base.DepartmentStat;
                FS.FrameWork.Models.NeuObject nurse = new FS.FrameWork.Models.NeuObject();
                nurse.ID = deptStat.PardepCode;
                nurse.Name = deptStat.PardepName;

                ArrayList al = new ArrayList();
                al.Add(this.cmbDept.SelectedItem);
                INurse_SelectedDeptChange(nurse, al);
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
            FS.HISFC.BizLogic.Manager.DepartmentStatManager statManager = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
            this.PriveDept = ((FS.HISFC.Models.Base.Employee)userPowerDetailManager.Operator).Dept;

            if (((FS.HISFC.Models.Base.Employee)userPowerDetailManager.Operator).IsManager)
            {
                ArrayList al = statManager.LoadByParent("14", "AAAA");
                //查找权限科室对应的护士站
                if (al != null && al.Count > 0)
                {
                    foreach (FS.FrameWork.Models.NeuObject obj in al)
                    {
                        if (!this.PriveNurse.ContainsKey(obj.ID))
                        {
                            this.PriveNurse.Add(obj.ID, obj);
                        }
                    }
                }
            }
            else if (this.isCheckPrivePower)
            {
                if (string.IsNullOrEmpty(this.privePowerString))
                {
                    privePowerString = "1401+02";
                }
                if (privePowerString.Split('+').Length < 2)
                {
                    privePowerString = privePowerString + "+02";
                }
                string[] prives = privePowerString.Split('+');


                List<FS.FrameWork.Models.NeuObject> alDept = userPowerDetailManager.QueryUserPriv(userPowerDetailManager.Operator.ID, prives[0], prives[1]);
                if (alDept == null || alDept.Count == 0)
                {
                    CommonController.CreateInstance().MessageBox(this, "您没有权限！", MessageBoxIcon.Information);
                    return -1;
                }

                foreach (FS.FrameWork.Models.NeuObject dept in alDept)
                {
                    this.PriveDept = dept;
                    //最后查找对应权限病区
                    //如果权限科室本身就是护士站了，那就直接等于科室
                    if (CommonController.CreateInstance().GetDepartment(this.PriveDept.ID).DeptType.ID.Equals(FS.HISFC.Models.Base.EnumDepartmentType.N))
                    {
                        if (!this.PriveNurse.ContainsKey(this.PriveDept.ID))
                        {
                            this.PriveNurse.Add(this.PriveDept.ID, this.PriveDept.Clone());
                        }
                    }
                    else
                    {
                        ArrayList al = statManager.LoadByChildren("14", this.priveDept.ID);
                        //查找权限科室对应的护士站
                        if (al != null && al.Count > 0)
                        {
                            FS.FrameWork.Models.NeuObject obj = al[0] as FS.FrameWork.Models.NeuObject;
                            if (!this.PriveNurse.ContainsKey(obj.ID))
                            {
                                this.PriveNurse.Add(obj.ID, obj);
                            }
                        }
                    }
                }
            }
            else
            {
                this.PriveDept = ((FS.HISFC.Models.Base.Employee)userPowerDetailManager.Operator).Dept;
                //最后查找对应权限病区
                //如果权限科室本身就是护士站了，那就直接等于科室
                if (CommonController.CreateInstance().GetDepartment(this.PriveDept.ID).DeptType.ID.Equals(FS.HISFC.Models.Base.EnumDepartmentType.N))
                {
                    if (!this.PriveNurse.ContainsKey(this.PriveDept.ID))
                    {
                        this.PriveNurse.Add(this.PriveDept.ID, this.PriveDept.Clone());
                    }
                }
                else
                {
                    ArrayList al = statManager.LoadByChildren("14", this.priveDept.ID);
                    //查找权限科室对应的护士站
                    if (al != null && al.Count > 0)
                    {
                        foreach (FS.FrameWork.Models.NeuObject obj in al)
                        {
                            if (!this.PriveNurse.ContainsKey(obj.ID))
                            {
                                this.PriveNurse.Add(obj.ID, obj);
                            }
                        }
                    }
                    else
                    {
                        if (!this.PriveNurse.ContainsKey(((FS.HISFC.Models.Base.Employee)userPowerDetailManager.Operator).Nurse.ID))
                        {
                            this.PriveNurse.Add(((FS.HISFC.Models.Base.Employee)userPowerDetailManager.Operator).Nurse.ID, ((FS.HISFC.Models.Base.Employee)userPowerDetailManager.Operator).Nurse);
                        }
                    }
                }
            }

            #endregion

            return 1;
        }

        #endregion
    }
}
