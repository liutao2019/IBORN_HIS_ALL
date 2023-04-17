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
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.HISFC.Assign.Components.Maintenance.Room
{
    /// <summary>
    /// [功能描述: 分诊诊室列表界面]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    public partial class ucRoomManager : FS.FrameWork.WinForms.Controls.ucBaseControl,IDataList
    {
        public ucRoomManager()
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
            this.lvAssignRoom.SelectedIndexChanged -= new EventHandler(lvNurseStation_SelectedIndexChanged);
            this.lvAssignRoom.SelectedIndexChanged += new EventHandler(lvNurseStation_SelectedIndexChanged);

            this.lvAssignRoom.DoubleClick -= new EventHandler(lvAssignRoom_DoubleClick);
            this.lvAssignRoom.DoubleClick += new EventHandler(lvAssignRoom_DoubleClick);

            this.lvAssignRoom.MouseDown -= new MouseEventHandler(lvAssignRoom_MouseDown);
            this.lvAssignRoom.MouseDown += new MouseEventHandler(lvAssignRoom_MouseDown);

            this.lvAssignRoom.MouseUp -= new MouseEventHandler(lvAssignRoom_MouseUp);
            this.lvAssignRoom.MouseUp += new MouseEventHandler(lvAssignRoom_MouseUp);
            return 1;
        }


        private int initImageList()
        {
            ImageList list = new ImageList();
            list.Images.Add("0", FS.FrameWork.WinForms.Properties.Resources.房间);
            list.ImageSize = new Size(50, 50);
            this.lvAssignRoom.LargeImageList = list;
            return 1;
        }

        #endregion

        #region 方法

        private int add(FS.SOC.HISFC.Assign.Models.Room room)
        {
            ListViewItem item = null;
            if (!this.lvAssignRoom.Items.ContainsKey(room.ID))
            {
                item = new ListViewItem();
                this.lvAssignRoom.Items.Add(item);
            }
            else
            {
                item = this.lvAssignRoom.Items[room.ID];
            }

            item.Tag = room;
            item.Name = room.ID;
            item.ImageIndex = 0;
            item.Text = room.Name;

            if (FS.FrameWork.Function.NConvert.ToBoolean(room.IsValid))
            {
                item.ForeColor = Color.Black;
            }
            else
            {
                item.ForeColor = Color.Red;
            }

            ListViewGroup lv = new ListViewGroup();
            if (this.hsGroup.ContainsKey(room.Dept.ID) == false)
            {
                lv.Name = room.Dept.ID;
                lv.Header = CommonController.CreateInstance().GetDepartmentName(room.Dept.ID);
                lv.Tag = room.Dept;
                this.lvAssignRoom.Groups.Add(lv);
                this.hsGroup.Add(room.Dept.ID, lv);
            }
            else
            {
                lv = this.hsGroup[room.Dept.ID] as ListViewGroup;
                lv.Name = room.Dept.ID;
                lv.Header = CommonController.CreateInstance().GetDepartmentName(room.Dept.ID);
                lv.Tag = room.Dept;
            }

            item.Group = lv;

            return 1;
        }

        #endregion

        #region 触发事件

        bool isSelected = false;

        void lvNurseStation_SelectedIndexChanged(object sender, EventArgs e)
        {
            isSelected = true;

            if (isMouseDown)
            {
                return;
            }

            if (this.lvAssignRoom.SelectedItems != null && this.lvAssignRoom.SelectedItems.Count > 0)
            {
                FS.FrameWork.Models.NeuObject obj = this.lvAssignRoom.SelectedItems[0].Group.Tag as FS.FrameWork.Models.NeuObject;

                ArrayList al = new ArrayList();
                foreach (ListViewItem item in this.lvAssignRoom.SelectedItems)
                {
                    al.Add(item.Tag as FS.SOC.HISFC.Assign.Models.Room);
                }

                if (this.selectedDeptChange != null)
                {
                    this.selectedDeptChange(obj, al);
                }
            }
            isSelected = false;
        }

        void lvAssignRoom_DoubleClick(object sender, EventArgs e)
        {
            if (this.lvAssignRoom.SelectedItems != null && this.lvAssignRoom.SelectedItems.Count > 0)
            {
                if (this.doubleClickItem != null)
                {
                    this.doubleClickItem(this.lvAssignRoom.SelectedItems[0].Tag);
                }
            }
        }

        bool isMouseDown = false;

        void lvAssignRoom_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;

            if (isSelected)
            {
                lvNurseStation_SelectedIndexChanged(sender, e);
            }
        }

        void lvAssignRoom_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.Clicks == 1)
                {
                    isMouseDown = true;
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
            this.lvAssignRoom.Clear();
            this.hsGroup.Clear();
            return 1;
        }

        #endregion

        #region IAssignNurseStation 成员

        public int AddRange(System.Collections.ArrayList alNurseStation)
        {
            int currentSelectedIndex = 0;
            if (this.lvAssignRoom.SelectedItems != null && this.lvAssignRoom.SelectedItems.Count > 0)
            {
                currentSelectedIndex = this.lvAssignRoom.SelectedItems[0].Index;
            }

            if (alNurseStation == null)
            {
                return -1;
            }

            foreach (FS.SOC.HISFC.Assign.Models.Room room in alNurseStation)
            {
                this.add(room);
            }

            //删除未包含的内容
            ArrayList temp = new ArrayList();
            foreach (ListViewItem item in this.lvAssignRoom.Items)
            {
                if (!alNurseStation.Contains(item.Tag))
                {
                    temp.Add(item);
                }
            }
            int count = 0;
            int num = temp.Count; ;
            while (temp.Count > 0 && count < num * 2)
            {
                this.lvAssignRoom.Items.Remove(temp[0] as ListViewItem);
                temp.Remove(temp[0]);
                
                count++;
            }
            if (this.lvAssignRoom.Items.Count > 0&&this.lvAssignRoom.Items.Count>=currentSelectedIndex+1)
            {
                this.lvAssignRoom.EnsureVisible(currentSelectedIndex);
                this.lvAssignRoom.SelectedItems.Clear();
                this.lvAssignRoom.Items[currentSelectedIndex].Selected = true;
                this.lvAssignRoom.Items[currentSelectedIndex].Focused = true;
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

        public int Add(object o)
        {
            FS.SOC.HISFC.Assign.Models.Room room = o as FS.SOC.HISFC.Assign.Models.Room;
            if (room != null)
            {
                this.add(room);

                if (this.lvAssignRoom.Items.ContainsKey(room.ID))
                {
                    this.lvAssignRoom.Items[room.ID].Selected = true;
                }

                Application.DoEvents();
            }

            return 1;
        }

        public object SelectItem
        {
            get
            {

                if (this.lvAssignRoom.SelectedItems != null && this.lvAssignRoom.SelectedItems.Count > 0)
                {
                    return this.lvAssignRoom.SelectedItems[0].Tag;
                }

                return null;
            }
        }

        public object FirstItem
        {
            get {
                if (this.lvAssignRoom.Items != null && this.lvAssignRoom.Items.Count > 0)
                {
                    return this.lvAssignRoom.Items[0].Tag;
                }

                return null;
            }
        }

        public int Remove(object info)
        {
            if (info is FS.SOC.HISFC.Assign.Models.Room)
            {
                FS.SOC.HISFC.Assign.Models.Room room = info as FS.SOC.HISFC.Assign.Models.Room;
                if (this.lvAssignRoom.Items.ContainsKey(room.ID))
                {
                    this.lvAssignRoom.Items.RemoveByKey(room.ID);
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
