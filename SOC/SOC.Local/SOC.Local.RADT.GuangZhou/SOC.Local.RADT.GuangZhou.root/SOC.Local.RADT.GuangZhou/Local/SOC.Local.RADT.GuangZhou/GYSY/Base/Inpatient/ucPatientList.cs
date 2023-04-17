using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Neusoft.HISFC.Models.Base;
using Neusoft.SOC.HISFC.BizProcess.CommonInterface;

namespace Neusoft.SOC.Local.RADT.GuangZhou.GYSY.Base.Inpatient
{
    /// <summary>
    /// 住院登记患者列表（默认）
    /// </summary>
    public partial class ucPatientList : Neusoft.FrameWork.WinForms.Controls.ucBaseControl, Neusoft.SOC.HISFC.RADT.Interface.Register.IPatientList
    {
        public ucPatientList()
        {
            InitializeComponent();
            this.lvPatientList.MouseDoubleClick += new MouseEventHandler(lvPatientList_MouseDoubleClick);
            this.initImageList();
        }

        #region 成员变量

        private Hashtable hsGroup = new Hashtable();

        private EnumGroup group = EnumGroup.InDate;

        #endregion

        #region 私有方法

        private void initImageList()
        {
            ImageList list = new ImageList();

            list.Images.Add("0",Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.R人物_成人_男));
            list.Images.Add("1", Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.R人物_成人_女));

            list.Images.Add("2", Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.R人物_儿童_男));
            list.Images.Add("3", Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.R人物_儿童_女));

            list.Images.Add("4", Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.R人物_老人_男));
            list.Images.Add("5", Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.R人物_老人_女));

            list.Images.Add("6", Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.R人物_婴儿_男));
            list.Images.Add("7", Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.R人物_婴儿_女));

            list.Images.Add("8", Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.G顾客));
            list.ImageSize = new Size(40, 40);
            this.lvPatientList.LargeImageList = list;

            list = new ImageList();
            list.Images.Add("0", Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.R人物_成人_男));
            list.Images.Add("1", Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.R人物_成人_女));

            list.Images.Add("2", Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.R人物_儿童_男));
            list.Images.Add("3", Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.R人物_儿童_女));

            list.Images.Add("4", Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.R人物_老人_男));
            list.Images.Add("5", Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.R人物_老人_女));

            list.Images.Add("6", Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.R人物_婴儿_男));
            list.Images.Add("7", Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.R人物_婴儿_女));

            list.Images.Add("8", Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.G顾客));
            list.ImageSize = new Size(20, 20);
            this.lvPatientList.SmallImageList = list;

        }

        private ListViewGroup getGroup(int hour)
        {
            ListViewGroup lv = null;
            if (hsGroup.ContainsKey(hour) == false)
            {
                lv = new ListViewGroup();
                lv.Header = string.Format("{0}小时以内入院患者", hour);
                lv.Name = hour.ToString();
                this.lvPatientList.Groups.Add(lv);
                hsGroup.Add(hour, lv);
            }
            else
            {
                lv = hsGroup[hour] as ListViewGroup;
            }

            return lv;
        }

        private int getImageKey(DateTime now, Neusoft.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            if (patientInfo == null)
            {
                return 0;
            }

            int year = 0;
            int month = 0;
            int day = 0;
            this.getAge(now, patientInfo.Birthday, ref year, ref month, ref day);

            patientInfo.Age = string.Format("{0}岁{1}月{2}天", year, month, day);

            int imageKey = 8;

            //如果大于60岁 则用老人图片 
            if (year >= 60)
            {
                imageKey = 4;
            }
            else if (year < 1)//婴儿
            {
                imageKey = 6;
            }
            else if (year < 15)//15岁以下 儿童
            {
                imageKey = 2;
            }
            else//成年
            {
                imageKey = 0;
            }

            if (patientInfo.Sex.ID.ToString() == Neusoft.HISFC.Models.Base.EnumSex.M.ToString())
            {
                return imageKey;
            }
            else if (patientInfo.Sex.ID.ToString() == Neusoft.HISFC.Models.Base.EnumSex.F.ToString())
            {
                return imageKey + 1;
            }

            return 8;
        }

        private void getAge(DateTime sysdate,DateTime birthday,ref int iYear,ref int iMonth,ref int iDay)
        {
            if (sysdate > birthday)
            {
                iYear = sysdate.Year - birthday.Year;
                if (iYear < 0)
                {
                    iYear = 0;
                }
                iMonth = sysdate.Month - birthday.Month;
                if (iMonth < 0)
                {
                    if (iYear > 0)
                    {
                        iYear = iYear - 1;
                        DateTime dt = new DateTime(birthday.Year + 1, 1, 1);
                        iMonth = dt.AddMonths(-1).Month + iMonth;//用当前的月份减
                    }

                    if (iMonth < 0)
                    {
                        iMonth = 0;
                    }
                }
                iDay = sysdate.Day - birthday.Day;
                if (iDay < 0)
                {
                    if (iMonth > 0)
                    {
                        iMonth = iMonth - 1;
                        DateTime dt = new DateTime(birthday.Year, birthday.Month, 1).AddMonths(1);
                        iDay = dt.AddDays(-1).Day + iDay;
                    }
                    else if (iYear > 0)
                    {
                        iYear = iYear - 1;
                        DateTime dt = new DateTime(birthday.Year + 1, 1, 1);
                        iMonth = dt.AddMonths(-1).Month - 1;
                        dt = new DateTime(birthday.Year, birthday.Month, 1).AddMonths(1);
                        iDay = dt.AddDays(-1).Day + iDay;
                    }
                    else
                    {
                        iDay = 0;
                    }
                }
            }
        }

        private void addGroupItem(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo, DateTime dtNow)
        {
            ListViewItem item = new ListViewItem();
            item.Tag = patientInfo;
            item.Name = patientInfo.ID;
            
            switch (ViewState)
            {
                case View.Tile:
                    item.SubItems.AddRange(new string[] { 
                        patientInfo.PID.PatientNO,
                        patientInfo.Name+ "(" + patientInfo.Sex.Name + ")",
                       this.getStateName(patientInfo.PVisit.InState.ID.ToString())
                    });

                    item.SubItems[0].ForeColor = this.getStateColor(patientInfo.PVisit.InState.ID.ToString());
                    item.SubItems[1].ForeColor = item.SubItems[0].ForeColor;
                    item.SubItems[2].ForeColor = item.SubItems[0].ForeColor;
                    item.SubItems[3].ForeColor = item.SubItems[0].ForeColor;
                    break;
                case View.Details:
                case View.LargeIcon:
                case View.List:
                case View.SmallIcon:
                    item.Text = patientInfo.Name + "(" + patientInfo.Sex.Name + ")"
                         + "\n"
                         + this.getStateName(patientInfo.PVisit.InState.ID.ToString());
                    item.SubItems.AddRange(new string[] { 
                        patientInfo.PID.PatientNO,
                        patientInfo.Name,
                        patientInfo.Sex.Name,
                        patientInfo.PVisit.PatientLocation.Dept.Name,
                        string.IsNullOrEmpty(patientInfo.IDCard)?"无":patientInfo.IDCard,
                        string.IsNullOrEmpty(patientInfo.AddressHome)?"无":patientInfo.AddressHome,
                        patientInfo.PVisit.InTime.ToString(),
                        patientInfo.PVisit.InState.Name,
                        patientInfo.Pact.Name
                    });
                    break;
                default :
                    break;
            }

            item.ForeColor = this.getStateColor(patientInfo.PVisit.InState.ID.ToString());
            item.UseItemStyleForSubItems = true;

            if (this.group == EnumGroup.InDate)
            {
                int days = ((TimeSpan)(dtNow - patientInfo.PVisit.InTime)).Days;
                if (days == 0)
                {
                    int hour = ((TimeSpan)(dtNow - patientInfo.PVisit.InTime)).Hours;
                    if (hour <= 1) //一小时内入院患者
                    {
                        ListViewGroup lv = this.getGroup(1);
                        item.Group = lv;
                    }
                    else if (hour <= 3)//三小时内入院患者
                    {
                        ListViewGroup lv = this.getGroup(3);
                        item.Group = lv;
                    }
                    else if (hour <= 6)//六小时内入院患者
                    {
                        ListViewGroup lv = this.getGroup(6);
                        item.Group = lv;
                    }
                    else if (hour <= 12)//12小时内入院患者
                    {
                        ListViewGroup lv = this.getGroup(12);
                        item.Group = lv;
                    }
                    else if (hour <= 24)//24小时内入院患者
                    {
                        ListViewGroup lv = this.getGroup(24);
                        item.Group = lv;
                    }
                    else//其他
                    {
                        ListViewGroup lv = this.getGroup(48);
                        item.Group = lv;
                    }
                }
                else
                {
                    ListViewGroup lv = this.getGroup(48);
                    item.Group = lv;
                }
            }
            else if (this.group == EnumGroup.InState)
            {
                if (this.hsGroup.ContainsKey(patientInfo.PVisit.InState.ID.ToString()) == false)
                {
                    ListViewGroup lv = new ListViewGroup();
                    lv.Name = patientInfo.PVisit.InState.ID.ToString();
                    lv.Header = patientInfo.PVisit.InState.Name.ToString();
                    this.lvPatientList.Groups.Add(lv);
                    this.hsGroup.Add(patientInfo.PVisit.InState.ID.ToString(), lv);
                    item.Group = lv;
                }
                else
                {
                    item.Group = this.hsGroup[patientInfo.PVisit.InState.ID.ToString()] as ListViewGroup;
                }
            }
            else if (this.group == EnumGroup.Dept)
            {
                if (this.hsGroup.ContainsKey(patientInfo.PVisit.PatientLocation.Dept.ID) == false)
                {
                    ListViewGroup lv = new ListViewGroup();
                    lv.Name = patientInfo.PVisit.PatientLocation.Dept.ID;
                    lv.Header = patientInfo.PVisit.PatientLocation.Dept.Name;
                    this.lvPatientList.Groups.Add(lv);
                    this.hsGroup.Add(patientInfo.PVisit.PatientLocation.Dept.ID, lv);
                    item.Group = lv;
                }
                else
                {
                    item.Group = this.hsGroup[patientInfo.PVisit.PatientLocation.Dept.ID] as ListViewGroup;
                }
            }

            item.ImageIndex = this.getImageKey(dtNow, patientInfo);

            item.ToolTipText =
                "住院号：" + patientInfo.PID.PatientNO + "\n" +
                "姓名：" + patientInfo.Name + "\n" +
                "性别：" + patientInfo.Sex.Name + "\n" +
                "年龄：" + patientInfo.Age + "\n" +
                "住院日期：" + patientInfo.PVisit.InTime.ToString() + "\n" +
                "状态：" + patientInfo.PVisit.InState.Name;

            this.lvPatientList.Items.Add(item);
        }

        private string getStateName(string inState)
        {
            if (inState == EnumInState.R.ToString())
            {
                return "住院登记";
            }
            else if (inState == EnumInState.I.ToString())
            {
                return "住院接诊";
            }
            else if (inState == EnumInState.O.ToString())
            {
                return "出院已结";
            }
            else if (inState == EnumInState.N.ToString())
            {
                return "无费退院";
            }
            else if (inState == EnumInState.B.ToString())
            {
                return "出院未结";
            }
            else if (inState == EnumInState.C.ToString())
            {
                return "住院封账";
            }
            else if (inState == EnumInState.P.ToString())
            {
                return "预约出院";
            }
            else if (inState == EnumInState.E.ToString())
            {
                return "转住院";
            }

            return "其他";
        }

        private void refreshGroupItem()
        {
            DateTime dtNow = CommonController.CreateInstance().GetSystemTime();
            ArrayList list = this.lvPatientList.Tag as ArrayList;
            this.lvPatientList.Items.Clear();
            this.hsGroup.Clear();
            this.lvPatientList.Groups.Clear();

            foreach (Neusoft.HISFC.Models.RADT.PatientInfo item in list)
            {
                this.addGroupItem(item, dtNow);
            }
        }

        private Color getStateColor(string inState)
        {
            if (inState == Neusoft.HISFC.Models.Base.EnumInState.R.ToString())
            {
                return Color.Blue;
            }
            else if (inState == Neusoft.HISFC.Models.Base.EnumInState.N.ToString())
            {
                return Color.Red;
            }
            else
            {
                return Color.Black;
            }
        }

        #endregion

        #region 事件

        private void lvPatientList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem item = this.lvPatientList.GetItemAt(e.X, e.Y);
            if (item != null && item.Tag != null && item.Tag is Neusoft.HISFC.Models.RADT.PatientInfo)
            {
                if (OnSelectPatientInfo != null)
                {
                    OnSelectPatientInfo(item.Tag as Neusoft.HISFC.Models.RADT.PatientInfo);
                }
            }
        }

        private void lvPatientList_Select(object sender, EventArgs e)
        {
            if (this.lvPatientList.SelectedItems != null && this.lvPatientList.SelectedItems.Count > 0)
            {
                if (OnSelectPatientInfo != null)
                {
                    OnSelectPatientInfo(this.lvPatientList.SelectedItems[0].Tag as Neusoft.HISFC.Models.RADT.PatientInfo);
                }
            }

        }

        private void lvPatientList_Refresh(object sender, EventArgs e)
        {
            if (this.OnRefresh != null)
            {
                this.OnRefresh(sender, e);
            }
        }

        private void lvPatientList_Title(object sender, EventArgs e)
        {
            this.ViewState = View.Tile;
            this.refreshGroupItem();
        }

        private void lvPatientList_Detail(object sender, EventArgs e)
        {
            this.ViewState = View.Details;
            this.refreshGroupItem();
        }

        private void lvPatientList_LargeIcon(object sender, EventArgs e)
        {
            this.ViewState = View.LargeIcon;
            this.refreshGroupItem();
        }

        private void lvPatientList_List(object sender, EventArgs e)
        {
            this.ViewState = View.List;
            this.refreshGroupItem();
        }

        private void lvPatientList_IsGroup(object sender, EventArgs e)
        {
            this.lvPatientList.ShowGroups = !this.lvPatientList.ShowGroups;
            if (sender is ToolStripMenuItem)
            {
                ((ToolStripMenuItem)sender).Checked = this.lvPatientList.ShowGroups;
            }
        }

        private void lvPatientList_DeptGroup(object sender, EventArgs e)
        {
            this.group = EnumGroup.Dept;
            this.refreshGroupItem();
        }

        private void lvPatientList_InDateGroup(object sender, EventArgs e)
        {
            this.group = EnumGroup.InDate;
            this.refreshGroupItem();
        }

        private void lvPatientList_InStateGroup(object sender, EventArgs e)
        {
            this.group = EnumGroup.InState;
            this.refreshGroupItem();
        }

        protected override void OnLoad(EventArgs e)
        {
            ContextMenuStrip contextMenu = new ContextMenuStrip();

            ToolStripMenuItem menu = new ToolStripMenuItem("选择");
            menu.Click += new EventHandler(lvPatientList_Select);
            contextMenu.Items.Add(menu);

            ToolStripSeparator spearator = new ToolStripSeparator();
            contextMenu.Items.Add(spearator);

            menu = new ToolStripMenuItem("刷新");
            menu.Click += new EventHandler(lvPatientList_Refresh);
            contextMenu.Items.Add(menu);


            menu = new ToolStripMenuItem("查看");

            ToolStripMenuItem subMenu = new ToolStripMenuItem("平铺");
            subMenu.Click += new EventHandler(lvPatientList_Title);
            menu.DropDownItems.Add(subMenu);

            subMenu = new ToolStripMenuItem("图标");
            subMenu.Click += new EventHandler(lvPatientList_LargeIcon);
            menu.DropDownItems.Add(subMenu);

            subMenu = new ToolStripMenuItem("列表");
            subMenu.Click += new EventHandler(lvPatientList_List);
            menu.DropDownItems.Add(subMenu);

            subMenu = new ToolStripMenuItem("详细信息");
            subMenu.Click += new EventHandler(lvPatientList_Detail);
            menu.DropDownItems.Add(subMenu);
            contextMenu.Items.Add(menu);

            spearator = new ToolStripSeparator();
            contextMenu.Items.Add(spearator);

            menu = new ToolStripMenuItem("排列");

            subMenu = new ToolStripMenuItem("按入院时间");
            subMenu.Click += new EventHandler(lvPatientList_InDateGroup);
            menu.DropDownItems.Add(subMenu);

            subMenu = new ToolStripMenuItem("按入院状态");
            subMenu.Click += new EventHandler(lvPatientList_InStateGroup);
            menu.DropDownItems.Add(subMenu);

            subMenu = new ToolStripMenuItem("按住院科室");
            subMenu.Click += new EventHandler(lvPatientList_DeptGroup);
            menu.DropDownItems.Add(subMenu);

            spearator = new ToolStripSeparator();
            menu.DropDownItems.Add(spearator);

            subMenu = new ToolStripMenuItem("按组排列");
            subMenu.Click += new EventHandler(lvPatientList_IsGroup);
            subMenu.Checked = this.lvPatientList.ShowGroups;
            menu.DropDownItems.Add(subMenu);

            contextMenu.Items.Add(menu);

            this.lvPatientList.ContextMenuStrip = contextMenu;
            base.OnLoad(e);
        }

        #endregion

        #region IRegisterListInterface 成员

        public int LoadPatient(System.Collections.ArrayList al)
        {
            if (al == null)
            {
                return -1;
            }
            DateTime dtNow = CommonController.CreateInstance().GetSystemTime();

            this.lvPatientList.Items.Clear();
            this.lvPatientList.Groups.Clear();
            hsGroup.Clear();
            this.lvPatientList.Tag = null;


            foreach (Neusoft.HISFC.Models.RADT.PatientInfo patientInfo in al)
            {
                this.addGroupItem(patientInfo, dtNow);
            }

            this.lvPatientList.Tag = al;
            this.lvPatientList.GridLines = true;
            this.lvPatientList.HideSelection = true;
            this.lvPatientList.Sorting = SortOrder.Descending;
            return 1;
        }

        public event Neusoft.SOC.HISFC.RADT.Interface.Register.SelectPatientInfo OnSelectPatientInfo;

        public View ViewState
        {
            get
            {
                return this.lvPatientList.View;
            }
            set
            {
                this.lvPatientList.View = value;
            }
        }

        #endregion

        #region IRegisterListInterface 成员

        public new event EventHandler OnRefresh;

        #endregion

        enum EnumGroup
        {
            Dept,
            InDate,
            InState
        }
    }
}
