using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Assign.Components.Base
{
    /// <summary>
    /// [功能描述: 分诊护士站列表界面]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    public partial class ucAssignNurse : FS.FrameWork.WinForms.Controls.ucBaseControl,FS.SOC.HISFC.Assign.Interface.Components.IDataList
    {
        public ucAssignNurse()
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
            this.lvNurseStation.SelectedIndexChanged -= new EventHandler(lvNurseStation_SelectedIndexChanged);
            this.lvNurseStation.SelectedIndexChanged += new EventHandler(lvNurseStation_SelectedIndexChanged);

            this.lvNurseStation.MouseDown -= new MouseEventHandler(lvNurseStation_MouseDown);
            this.lvNurseStation.MouseDown += new MouseEventHandler(lvNurseStation_MouseDown);

            this.lvNurseStation.MouseUp -= new MouseEventHandler(lvNurseStation_MouseUp);
            this.lvNurseStation.MouseUp += new MouseEventHandler(lvNurseStation_MouseUp);

            return 1;

        }

        private int initImageList()
        {
            ImageList list = new ImageList();
            list.Images.Add("0", FS.FrameWork.WinForms.Properties.Resources.科室);
            list.ImageSize = new Size(50, 50);
            this.lvNurseStation.LargeImageList = list;
            return 1;
        }

        #endregion

        #region 方法

        private int add(FS.HISFC.Models.Base.DepartmentStat deptStat)
        {
            ListViewItem item = null;
            if (!this.lvNurseStation.Items.ContainsKey(deptStat.ID))
            {
                item = new ListViewItem();
                this.lvNurseStation.Items.Add(item);
            }
            else
            {
                item = this.lvNurseStation.Items[deptStat.ID];
            }

            item.Tag = deptStat;
            item.Name = deptStat.ID;
            item.ImageIndex = 0;
            item.Text = deptStat.Name;


            if (this.hsGroup.ContainsKey(deptStat.PardepCode) == false)
            {
                ListViewGroup lv = new ListViewGroup();
                lv.Name = deptStat.PardepCode;
                lv.Header = deptStat.PardepName;
                lv.Tag = new FS.FrameWork.Models.NeuObject(deptStat.PardepCode, deptStat.PardepName, deptStat.Memo);
                this.lvNurseStation.Groups.Add(lv);
                this.hsGroup.Add(deptStat.PardepCode, lv);
                item.Group = lv;
            }
            else
            {
                item.Group = this.hsGroup[deptStat.PardepCode] as ListViewGroup;
            }

            return 1;
        }

        private int add(FS.FrameWork.Models.NeuObject dept)
        {
            ListViewItem item = null;
            if (!this.lvNurseStation.Items.ContainsKey(dept.ID))
            {
                item = new ListViewItem();
                this.lvNurseStation.Items.Add(item);
            }
            else
            {
                item = this.lvNurseStation.Items[dept.ID];
            }

            item.Tag = dept;
            item.Name = dept.ID;
            item.ImageIndex = 0;
            item.Text = dept.Name;


            if (this.hsGroup.ContainsKey("Defualt") == false)
            {
                ListViewGroup lv = new ListViewGroup();
                lv.Name = "Defualt";
                lv.Header = "默认";
                lv.Tag = null;
                this.lvNurseStation.Groups.Add(lv);
                this.hsGroup.Add("Defualt", lv);
                item.Group = lv;
            }
            else
            {
                item.Group = this.hsGroup["Defualt"] as ListViewGroup;
            }
            return 1;
        }

        #endregion

        #region 触发事件

        private bool isSelected = false;

        void lvNurseStation_SelectedIndexChanged(object sender, EventArgs e)
        {
            isSelected = true;

            if (isMouseDown)
            {
                return;
            }

            if (this.lvNurseStation.SelectedItems != null && this.lvNurseStation.SelectedItems.Count > 0)
            {
                FS.FrameWork.Models.NeuObject obj = this.lvNurseStation.SelectedItems[0].Group.Tag as FS.FrameWork.Models.NeuObject;

                ArrayList al = new ArrayList();
                foreach (ListViewItem item in this.lvNurseStation.SelectedItems)
                {
                    al.Add(item.Tag as FS.FrameWork.Models.NeuObject);
                }

                if (this.selectedDeptChange != null)
                {
                    this.selectedDeptChange(obj, al);
                }
            }

            isSelected = false;
        }

        void lvNurseStation_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;

            if (isSelected)
            {
                lvNurseStation_SelectedIndexChanged(sender, e);
            }
        }

        private bool isMouseDown = false;

        void lvNurseStation_MouseDown(object sender, MouseEventArgs e)
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
            this.lvNurseStation.Clear();
            this.hsGroup.Clear();
            return 1;
        }

        #endregion

        #region IAssignNurseStation 成员

        public int AddRange(System.Collections.ArrayList alNurseStation)
        {
            int currentSelectedIndex = 0;//当前选择项索引
            if (this.lvNurseStation.SelectedItems != null && this.lvNurseStation.SelectedItems.Count > 0)
            {
                currentSelectedIndex = this.lvNurseStation.SelectedItems[0].Index;
            }

            if (alNurseStation == null)
            {
                return -1;
            }


            for (int i = 0; i < alNurseStation.Count; i++)
            {
                if (alNurseStation[i] is FS.HISFC.Models.Base.DepartmentStat)
                {
                    this.add(alNurseStation[i] as FS.HISFC.Models.Base.DepartmentStat);
                }
                else
                {
                    this.add(alNurseStation[i] as FS.FrameWork.Models.NeuObject);
                }
            }

            //删除未包含的内容
            ArrayList temp = new ArrayList();
            foreach (ListViewItem item in this.lvNurseStation.Items)
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
                this.lvNurseStation.Items.Remove(temp[0] as ListViewItem);
                temp.Remove(temp[0]);
                count++;
            }
            if (this.lvNurseStation.Items.Count > 0&&this.lvNurseStation.Items.Count>currentSelectedIndex+1)
            {
                this.lvNurseStation.EnsureVisible(currentSelectedIndex);
                this.lvNurseStation.SelectedItems.Clear();
                this.lvNurseStation.Items[currentSelectedIndex].Selected = true;
                this.lvNurseStation.Items[currentSelectedIndex].Focused = true;
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

            Application.DoEvents();
            return 1;
        }

        #endregion

        #region IAssignNurseStation 成员


        public object SelectItem
        {
            get
            {

                if (this.lvNurseStation.SelectedItems != null && this.lvNurseStation.SelectedItems.Count > 0)
                {
                    return this.lvNurseStation.SelectedItems[0].Tag;
                }

                return null;
            }
        }

        public object FirstItem
        {
            get
            {
                if (this.lvNurseStation.Items != null && this.lvNurseStation.Items.Count > 0)
                {
                    return this.lvNurseStation.Items[0].Tag;
                }

                return null;
            }
        }

        public int Remove(object info)
        {
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
