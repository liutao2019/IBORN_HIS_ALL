using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.HISFC.Assign.Components.Common.Triage
{
    /// <summary>
    /// [功能描述: 门诊分诊队列信息显示]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    internal partial class ucTriageQueue : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.SOC.HISFC.Assign.Interface.Components.ITriageQueue
    {
        public ucTriageQueue()
        {
            InitializeComponent();
        }

        #region 域变量

        /// <summary>
        /// 用于存储科室分组
        /// </summary>
        private Hashtable hsGroup = new Hashtable();

        private FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Queue> selectedQueueChange;

        private FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Queue> doubleClick;

        private FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Assign> dragDropAssign;

        private FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.HISFC.Models.Registration.Register> dragDropRegister;
        
        /// <summary>
        /// 病情级别
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper patientConditionType = null;
        #endregion

        #region 初始化

        /// <summary>
        /// 初始化事件
        /// </summary>
        /// <returns></returns>
        private int initEvents()
        {
            this.lvTriageQueue.SelectedIndexChanged -= new EventHandler(lvTriageQueue_SelectedIndexChanged);
            this.lvTriageQueue.SelectedIndexChanged += new EventHandler(lvTriageQueue_SelectedIndexChanged);

            this.lvTriageQueue.DragEnter -= new DragEventHandler(tvTriaggingPatient_DragEnter);
            this.lvTriageQueue.DragEnter += new DragEventHandler(tvTriaggingPatient_DragEnter);

            this.lvTriageQueue.DragDrop -= new DragEventHandler(tvTriaggingPatient_DragDrop);
            this.lvTriageQueue.DragDrop += new DragEventHandler(tvTriaggingPatient_DragDrop);

            this.lvTriageQueue.DragOver -= new DragEventHandler(tvTriaggingPatient_DragOver);
            this.lvTriageQueue.DragOver += new DragEventHandler(tvTriaggingPatient_DragOver);

            this.lvTriageQueue.DoubleClick -= new EventHandler(lvTriageQueue_DoubleClick);
            this.lvTriageQueue.DoubleClick += new EventHandler(lvTriageQueue_DoubleClick);
            return 1;
        }

        #endregion

        #region 方法

        private int add(FS.SOC.HISFC.Assign.Models.Queue queue)
        {
            ListViewItem item = null;
            if (!this.lvTriageQueue.Items.ContainsKey(queue.ID))
            {
                item = new ListViewItem();
                this.lvTriageQueue.Items.Add(item);
            }
            else
            {
                item = this.lvTriageQueue.Items[queue.ID];
            }

            item.Tag = queue;
            item.Name = queue.ID;
            item.ImageKey = ((int)queue.QueueType).ToString();

            string conditionType=string.Empty;
            if(!string.IsNullOrEmpty(queue.PatientConditionType))
            {
                conditionType = this.patientConditionType.GetName(queue.PatientConditionType);
            }
            if (string.IsNullOrEmpty(conditionType))
            {
                item.Text =
                    queue.Name + "[" + queue.WaitingCount.ToString() + "人]"
                   + "\n"
                   + FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().GetNoonName(queue.Noon.ID) + (queue.IsExpert ? "[专]" : "");
            }
            else
            {
                item.Text =
                    queue.Name +"("+conditionType+")"+ "[" + queue.WaitingCount.ToString() + "人]"
                   + "\n"
                   + FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().GetNoonName(queue.Noon.ID) + (queue.IsExpert ? "[专]" : "");
            
            }
            if (queue.IsValid)
            {
                item.ForeColor = Color.Black;
            }
            else
            {
                item.ForeColor = Color.Red;
            }


            if (this.hsGroup.ContainsKey(queue.AssignDept.ID + queue.Noon.ID) == false)
            {
                ListViewGroup lv = new ListViewGroup();
                lv.Name = queue.AssignDept.ID;
                string noon = string.Empty;
                switch(queue.Noon.ID)
                {
                    case "1":
                        noon = "上午";
                        break;
                    case "2":
                        noon = "下午";
                        break;
                    case "3":
                        noon = "晚上";
                        break;
                    default:
                        break;
                }
                lv.Header = queue.AssignDept.Name + " " + noon;
                lv.Tag = queue.AssignDept;
                this.lvTriageQueue.Groups.Add(lv);
                this.hsGroup.Add(queue.AssignDept.ID + queue.Noon.ID, lv);
                item.Group = lv;
            }
            else
            {
                item.Group = this.hsGroup[queue.AssignDept.ID + queue.Noon.ID] as ListViewGroup;
            }

            return 1;
        }

        #endregion

        #region 触发事件

        void lvTriageQueue_SelectedIndexChanged(object sender, EventArgs e)
        {
            FS.SOC.HISFC.Assign.Models.Queue queue = null;
            if (this.lvTriageQueue.SelectedItems != null && this.lvTriageQueue.SelectedItems.Count > 0)
            {
                queue = this.lvTriageQueue.SelectedItems[0].Tag as FS.SOC.HISFC.Assign.Models.Queue;
            }

            if (this.selectedQueueChange != null)
            {
                this.selectedQueueChange(queue);
            }
        }

        void tvTriaggingPatient_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(FS.SOC.HISFC.Assign.Models.Assign)))
            {
                if (this.dragDropAssign != null)
                {
                    this.dragDropAssign(e.Data.GetData(typeof(FS.SOC.HISFC.Assign.Models.Assign)) as FS.SOC.HISFC.Assign.Models.Assign);
                }
            }
            else if (e.Data.GetDataPresent(typeof(FS.HISFC.Models.Registration.Register)))
            {
                if (this.dragDropRegister != null)
                {
                    this.dragDropRegister(e.Data.GetData(typeof(FS.HISFC.Models.Registration.Register)) as FS.HISFC.Models.Registration.Register);
                }
            }
        }

        void tvTriaggingPatient_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(FS.HISFC.Models.Registration.Register)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else if (e.Data.GetDataPresent(typeof(FS.SOC.HISFC.Assign.Models.Assign)))
            {
                FS.SOC.HISFC.Assign.Models.Assign assign = e.Data.GetData(typeof(FS.SOC.HISFC.Assign.Models.Assign)) as FS.SOC.HISFC.Assign.Models.Assign;
                if (assign == null )
                { 
                    e.Effect = DragDropEffects.None;
                }
                else if(assign.TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.In
                    || assign.TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.Triage
                    || assign.TriageStatus== FS.HISFC.Models.Nurse.EnuTriageStatus.Delay)
                {
                    e.Effect = DragDropEffects.Move;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        void tvTriaggingPatient_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(FS.HISFC.Models.Registration.Register))
                || e.Data.GetDataPresent(typeof(FS.SOC.HISFC.Assign.Models.Assign)))
            {
                ListViewItem item;

                Point p = this.lvTriageQueue.PointToClient(Cursor.Position);

                item = this.lvTriageQueue.GetItemAt(p.X, p.Y);

                if (item != null)
                {
                    item.Selected = true;
                    this.lvTriageQueue.Focus();
                }
            }
        }

        void lvTriageQueue_DoubleClick(object sender, EventArgs e)
        {
            if (this.lvTriageQueue.SelectedItems != null && this.lvTriageQueue.SelectedItems.Count > 0)
            {
                if (this.doubleClick != null)
                {
                    this.doubleClick(this.lvTriageQueue.SelectedItems[0].Tag as FS.SOC.HISFC.Assign.Models.Queue);
                }
            }
        }

        #endregion

        #region ITriageQueue 成员

        public int AddQueue(System.Collections.ArrayList alQueue)
        {
            if (alQueue != null)
            {
                foreach (FS.SOC.HISFC.Assign.Models.Queue queue in alQueue)
                {
                    this.add(queue);
                }

                ArrayList temp=new ArrayList();
                foreach (ListViewItem item in this.lvTriageQueue.Items)
                {
                    if (!alQueue.Contains(item.Tag))
                    {
                        temp.Add(item);
                    }
                }
                int i = 0;
                int num=temp.Count;;
                while (temp.Count > 0 && i < num * 2)
                {
                    this.lvTriageQueue.Items.Remove(temp[0] as ListViewItem);
                    temp.Remove(temp[0]);
                    i++;
                }
            }

            Application.DoEvents();
            return 1;
        }

        public int AddQueueNum(System.Collections.ArrayList alQueue)
        {
            if (alQueue != null)
            {
                foreach (FS.FrameWork.Models.NeuObject queue in alQueue)
                {
                    gbTriageQueue.Text = "队列信息 侯诊:" + queue.Name + " 进诊:" + queue.Memo + " 到诊:" + queue.User02 + " 已诊:" + queue.User01;
                }
            }

            Application.DoEvents();
            return 1;
        }

        public int Add(FS.SOC.HISFC.Assign.Models.Queue queue)
        {
            if (queue == null)
            {
                return -1;
            }

            this.add(queue);

            Application.DoEvents();
            return 1;
        }

        public FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Queue> SelectedQueueChange
        {
            get
            {
                return selectedQueueChange;
            }
            set
            {
                selectedQueueChange = value;
            }
        }

        public FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Queue> DoubleClickItem
        {
            get
            {
                return doubleClick;
            }
            set
            {
                doubleClick = value;
            }
        }

        public FS.SOC.HISFC.Assign.Models.Queue Queue
        {
            get
            {
                if (this.lvTriageQueue.SelectedItems != null && this.lvTriageQueue.SelectedItems.Count > 0)
                {
                    return this.lvTriageQueue.SelectedItems[0].Tag as FS.SOC.HISFC.Assign.Models.Queue;
                }

                return null;
            }
            set
            {
                if (value != null)
                {
                    if (lvTriageQueue.Items.ContainsKey(value.ID))
                    {
                        this.lvTriageQueue.Items[value.ID].Selected = true;
                    }
                }
            }
        }

        public FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.HISFC.Models.Registration.Register> DragDropRegister
        {
            get
            {
                return this.dragDropRegister;
            }
            set
            {
                dragDropRegister = value;
            }
        }

        public FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Assign> DragDropAssign
        {
            get
            {
                return this.dragDropAssign;
            }
            set
            {
                dragDropAssign = value;
            }
        }

        #endregion

        #region IInitialisable 成员

        public int Init()
        {
            this.initEvents();

            ImageList list = new ImageList();

            list.Images.Add("0", FS.FrameWork.WinForms.Properties.Resources.护士);
            list.ImageSize = new Size(50, 50);

            list.Images.Add("1", FS.FrameWork.WinForms.Properties.Resources.医生);
            list.ImageSize = new Size(50, 50);

            list.Images.Add("2", FS.FrameWork.WinForms.Properties.Resources.排序);
            list.ImageSize = new Size(50, 50);

            this.lvTriageQueue.LargeImageList = list;

            this.lvTriageQueue.AllowDrop = true;
            this.patientConditionType = new FS.FrameWork.Public.ObjectHelper(CommonController.CreateInstance().QueryConstant("PatientConditionType"));
            return 1;
        }

        #endregion

        #region ILoadable 成员

        public new int Load()
        {
            this.lvTriageQueue.View = View.LargeIcon;
            return 1;
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Load();
            base.OnLoad(e);
        }

        #endregion

        #region IClearable 成员

        public int Clear()
        {
            this.lvTriageQueue.Items.Clear();
            this.hsGroup.Clear();

            return 1;
        }

        #endregion

        #region ITriageQueue 成员

        public int Remove(FS.SOC.HISFC.Assign.Models.Queue queue)
        {
            if (this.lvTriageQueue.Items.ContainsKey(queue.ID))
            {
                this.lvTriageQueue.Items.RemoveByKey(queue.ID);
            }

            return 1;
        }

        #endregion
    }
}
