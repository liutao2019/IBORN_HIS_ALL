using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.SOC.HISFC.Assign.Interface.Components;

namespace FS.SOC.HISFC.Assign.Components.Maintenance.Room
{
    /// <summary>
    /// [功能描述: 分诊诊台列表界面]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    public partial class ucConsoleManager : FS.FrameWork.WinForms.Controls.ucBaseControl, IDataList
    {
        public ucConsoleManager()
        {
            InitializeComponent();
        }

        #region 域变量

        /// <summary>
        /// 用于存储科室分组
        /// </summary>
        private Hashtable hsGroup = new Hashtable();

        FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.FrameWork.Models.NeuObject, System.Collections.ArrayList> selectedDeptChange = null;
        FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<object> doubleClickItem = null;
        #endregion

        #region 初始化

        private int initEvents()
        {
            this.lvAssignConsole.SelectedIndexChanged -= new EventHandler(lvNurseStation_SelectedIndexChanged);
            this.lvAssignConsole.SelectedIndexChanged += new EventHandler(lvNurseStation_SelectedIndexChanged);

            this.lvAssignConsole.DoubleClick -= new EventHandler(lvNurseStation_DoubleClick);
            this.lvAssignConsole.DoubleClick += new EventHandler(lvNurseStation_DoubleClick);
            return 1;
        }

        private int initImageList()
        {
            ImageList list = new ImageList();
            list.Images.Add("0", FS.FrameWork.WinForms.Properties.Resources.设备);
            list.ImageSize = new Size(50, 50);
            this.lvAssignConsole.LargeImageList = list;
            return 1;
        }

        #endregion

        #region 方法

        private int add(FS.HISFC.Models.Nurse.Seat console)
        {
            ListViewItem item = null;
            if (!this.lvAssignConsole.Items.ContainsKey(console.ID))
            {
                item = new ListViewItem();
                this.lvAssignConsole.Items.Add(item);
            }
            else
            {
                item = this.lvAssignConsole.Items[console.ID];
            }

            item.Tag = console;
            item.Name = console.ID;
            item.ImageIndex = 0;
            item.Text = console.Name;

            if (FS.FrameWork.Function.NConvert.ToBoolean(console.PRoom.IsValid))
            {
                item.ForeColor = Color.Black;
            }
            else
            {
                item.ForeColor = Color.Red;
            }

            if (this.hsGroup.ContainsKey(console.PRoom.ID) == false)
            {
                ListViewGroup lv = new ListViewGroup();
                lv.Name = console.PRoom.ID;
                lv.Header = console.PRoom.Name;
                lv.Tag = console.PRoom;
                this.lvAssignConsole.Groups.Add(lv);
                this.hsGroup.Add(console.PRoom.ID, lv);
                item.Group = lv;
            }
            else
            {
                item.Group = this.hsGroup[console.PRoom.ID] as ListViewGroup;
            }

            return 1;
        }

        #endregion

        #region 触发事件

        void lvNurseStation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lvAssignConsole.SelectedItems != null && this.lvAssignConsole.SelectedItems.Count > 0)
            {
                FS.HISFC.Models.Nurse.Seat seat = this.lvAssignConsole.SelectedItems[0].Tag as FS.HISFC.Models.Nurse.Seat;

                if (this.selectedDeptChange != null)
                {
                    this.selectedDeptChange(seat, null);
                }
            }


        }

        void lvNurseStation_DoubleClick(object sender, EventArgs e)
        {
            if (this.lvAssignConsole.SelectedItems != null && this.lvAssignConsole.SelectedItems.Count > 0)
            {
                if (this.doubleClickItem != null)
                {
                    this.doubleClickItem(this.lvAssignConsole.SelectedItems[0].Tag);
                }
            }
        }
        #endregion

        #region IInitialisable 成员

        public int Init()
        {
            this.initImageList();
            this.initEvents();

            return 1;
        }

        #endregion

        #region ILoadable 成员

        public new int Load()
        {
            return 1;
        }

        #endregion

        #region IClearable 成员

        public int Clear()
        {
            this.lvAssignConsole.Clear();
            this.hsGroup.Clear();
            return 1;
        }

        #endregion

        #region IAssignNurseStation 成员

        public int AddRange(System.Collections.ArrayList alConsole)
        {
            if (alConsole == null)
            {
                return -1;
            }

            foreach (FS.HISFC.Models.Nurse.Seat room in alConsole)
            {
                this.add(room);
            }

            //删除未包含的内容
            ArrayList temp = new ArrayList();
            foreach (ListViewItem item in this.lvAssignConsole.Items)
            {
                if (!alConsole.Contains(item.Tag))
                {
                    temp.Add(item);
                }
            }
            int count = 0;
            int num = temp.Count; ;
            while (temp.Count > 0 && count < num * 2)
            {
                this.lvAssignConsole.Items.Remove(temp[0] as ListViewItem);
                temp.Remove(temp[0]);
                count++;
            }

            Application.DoEvents();
            return 1;
        }

        public FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.FrameWork.Models.NeuObject, System.Collections.ArrayList> SelectedDeptChange
        {
            get
            {
                return this.selectedDeptChange;
            }
            set
            {
                this.selectedDeptChange = value;
            }
        }

        #endregion

        #region IAssignNurseStation 成员


        public int Add(object o)
        {
            FS.HISFC.Models.Nurse.Seat console = o as FS.HISFC.Models.Nurse.Seat;
            if (console != null)
            {
                this.add(console);

                if (this.lvAssignConsole.Items.ContainsKey(console.ID))
                {
                    this.lvAssignConsole.Items[console.ID].Selected = true;
                }
            }

            Application.DoEvents();
            return 1;
        }

        #endregion

        #region IAssignNurseStation 成员


        public object SelectItem
        {
            get
            {

                if (this.lvAssignConsole.SelectedItems != null && this.lvAssignConsole.SelectedItems.Count > 0)
                {
                    return this.lvAssignConsole.SelectedItems[0].Tag;
                }

                return null;
            }
        }

        public object FirstItem
        {
            get
            {
                if (this.lvAssignConsole.Items != null && this.lvAssignConsole.Items.Count > 0)
                {
                    return this.lvAssignConsole.Items[0].Tag;
                }

                return null;
            }
        }

        public int Remove(object info)
        {
            if (info is FS.HISFC.Models.Nurse.Seat)
            {
                FS.HISFC.Models.Nurse.Seat console = info as FS.HISFC.Models.Nurse.Seat;
                if (this.lvAssignConsole.Items.ContainsKey(console.ID))
                {
                    this.lvAssignConsole.Items.RemoveByKey(console.ID);
                    Application.DoEvents();
                }
            }

            return 1;
        }

        public FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<object> DoubleClickItem
        {
            get
            {
                return doubleClickItem;
            }
            set
            {
                doubleClickItem = value;
            }
        }

        #endregion
    }
}
