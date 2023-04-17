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

namespace FS.SOC.HISFC.Assign.Components.Maintenance.Room
{
    /// <summary>
    /// [功能描述: 诊室、诊台管理界面]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    public partial class ucRoomAndConsoleManager : Base.ucAssignBase, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucRoomAndConsoleManager()
        {
            InitializeComponent();
        }

        #region 接口变量

        /// <summary>
        /// 分诊台管理
        /// </summary>
        IDataList INurseManager = null;
        /// <summary>
        /// 诊室管理
        /// </summary>
        IDataList IRoomManager = null;
        /// <summary>
        /// 诊台管理
        /// </summary>
        IDataList IConsoleManager = null;

        /// <summary>
        /// 诊室实例
        /// </summary>
        IMaintenance<FS.SOC.HISFC.Assign.Models.Room> IRoom = null;
        /// <summary>
        /// 诊台实例
        /// </summary>
        IMaintenance<FS.HISFC.Models.Nurse.Seat> IConsole = null;

        #endregion

        #region 重载变量

        private new Dictionary<string, FS.FrameWork.Models.NeuObject> priveNurse = new Dictionary<string, FS.FrameWork.Models.NeuObject>();
        public new Dictionary<string,FS.FrameWork.Models.NeuObject> PriveNurse
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
        /// <summary>
        /// 诊室管理
        /// </summary>
        private FS.SOC.HISFC.Assign.BizProcess.Room roomMgr = new FS.SOC.HISFC.Assign.BizProcess.Room();
        /// <summary>
        /// 诊台管理
        /// </summary>
        private FS.SOC.HISFC.Assign.BizProcess.Console consoleMgr = new FS.SOC.HISFC.Assign.BizProcess.Console();
        #endregion

        #region 工具栏

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("刷新", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S刷新, true, false, null);
            this.toolBarService.AddToolButton("添加诊室", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C窗口添加, true, false, null);
            this.toolBarService.AddToolButton("修改诊室", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C窗口, true, false, null);
            this.toolBarService.AddToolButton("删除诊室", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C窗口删除, true, false, null);
            this.toolBarService.AddToolButton("添加诊台", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S设备添加, true, false, null);
            this.toolBarService.AddToolButton("修改诊台", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S设备, true, false, null);
            this.toolBarService.AddToolButton("删除诊台", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S设备删除, true, false, null);
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
                case "添加诊室":
                    this.addRoom();
                    break;
                case "修改诊室":
                    this.modifyRoom();
                    break;
                case "删除诊室":
                    this.deleteRoom();
                    break;
                case "添加诊台":
                    this.addConsole();
                    break;
                case "修改诊台":
                    this.modifyConsole();
                    break;
                case "删除诊台":
                    this.deleteConsole();
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
            this.initEvents();
            this.initMenus();
            this.initDatas();
            return 1;
        }
        /// <summary>
        /// 接口初始化
        /// </summary>
        /// <returns></returns>
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

            #region IRoomManager

            IRoomManager = InterfaceManager.GetINurseRoomsManager();
            IRoomManager.Init();
            IRoomManager.Clear();
            if (this.IRoomManager is Control)
            {
                this.plRoom.Controls.Clear();
                //加载界面
                ((Control)this.IRoomManager).Dock = DockStyle.Fill;
                this.plRoom.Controls.Add((Control)this.IRoomManager);
            }
            #endregion

            #region IConsoleManager

            IConsoleManager = InterfaceManager.GetINurseConsolesManager();
            IConsoleManager.Init();
            IConsoleManager.Clear();
            if (this.IConsoleManager is Control)
            {
                this.plConsole.Controls.Clear();
                //加载界面
                ((Control)this.IConsoleManager).Dock = DockStyle.Fill;
                this.plConsole.Controls.Add((Control)this.IConsoleManager);
            }
            #endregion

            return 1;
        }
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <returns></returns>
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

                this.initIRoom();

                this.cmbDept.AddItems(alDeptStat);
            }

            return 1;
        }
        /// <summary>
        /// 初始化事件
        /// </summary>
        /// <returns></returns>
        private int initEvents()
        {
            this.INurseManager.SelectedDeptChange -= new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.FrameWork.Models.NeuObject, ArrayList>(INurse_SelectedDeptChange);
            this.INurseManager.SelectedDeptChange += new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.FrameWork.Models.NeuObject, ArrayList>(INurse_SelectedDeptChange);

            this.IRoomManager.SelectedDeptChange -= new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.FrameWork.Models.NeuObject, ArrayList>(IRoom_SelectedDeptChange);
            this.IRoomManager.SelectedDeptChange += new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.FrameWork.Models.NeuObject, ArrayList>(IRoom_SelectedDeptChange);

            this.IConsoleManager.SelectedDeptChange -= new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.FrameWork.Models.NeuObject, ArrayList>(IConsole_SelectedDeptChange);
            this.IConsoleManager.SelectedDeptChange += new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.FrameWork.Models.NeuObject, ArrayList>(IConsole_SelectedDeptChange);

            this.IRoomManager.DoubleClickItem -= new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<Object>(IRoom_DoubleClickItem);
            this.IRoomManager.DoubleClickItem += new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<Object>(IRoom_DoubleClickItem);

            this.IConsoleManager.DoubleClickItem -= new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<Object>(IRoom_DoubleClickItem);
            this.IConsoleManager.DoubleClickItem += new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<Object>(IRoom_DoubleClickItem);

            this.cmbDept.SelectedIndexChanged -= new EventHandler(cmbDept_SelectedIndexChanged);
            this.cmbDept.SelectedIndexChanged += new EventHandler(cmbDept_SelectedIndexChanged);
            return 1;
        }

        /// <summary>
        /// 初始化右键菜单
        /// </summary>
        /// <returns></returns>
        private int initMenus()
        {
            #region Room
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            ToolStripMenuItem menu = new ToolStripMenuItem("添加诊室");
            menu.Click += new EventHandler(menu_Click);
            contextMenu.Items.Add(menu);

            ToolStripSeparator spearator = new ToolStripSeparator();
            contextMenu.Items.Add(spearator);

            menu = new ToolStripMenuItem("修改诊室");
            menu.Click += new EventHandler(menu_Click);
            contextMenu.Items.Add(menu);

            spearator = new ToolStripSeparator();
            contextMenu.Items.Add(spearator);


            menu = new ToolStripMenuItem("删除诊室");
            menu.Click += new EventHandler(menu_Click);
            contextMenu.Items.Add(menu);

            if (this.IRoomManager is Control)
            {
                ((Control)this.IRoomManager).ContextMenuStrip = contextMenu;
            }
            #endregion

            #region Console

            contextMenu = new ContextMenuStrip();
            menu = new ToolStripMenuItem("添加诊台");
            menu.Click += new EventHandler(menu_Click);
            contextMenu.Items.Add(menu);

            spearator = new ToolStripSeparator();
            contextMenu.Items.Add(spearator);

            menu = new ToolStripMenuItem("修改诊台");
            menu.Click += new EventHandler(menu_Click);
            contextMenu.Items.Add(menu);

            spearator = new ToolStripSeparator();
            contextMenu.Items.Add(spearator);


            menu = new ToolStripMenuItem("删除诊台");
            menu.Click += new EventHandler(menu_Click);
            contextMenu.Items.Add(menu);

            if (this.IConsoleManager is Control)
            {
                ((Control)this.IConsoleManager).ContextMenuStrip = contextMenu;
            }
            #endregion

            return 1;
        }

        /// <summary>
        /// 初始化诊室实例
        /// </summary>
        /// <returns></returns>
        private int initIRoom()
        {
            if (this.IRoom == null)
            {
                IRoom = InterfaceManager.GetINurseRoom();
                IRoom.Init();
            }
            this.IRoom.SaveInfo -= new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Room>(IRoom_SaveInfo);
            this.IRoom.SaveInfo += new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Room>(IRoom_SaveInfo);
            this.IRoom.Init(new ArrayList(this.PriveNurse.Values));

            return 1;
        }

        /// <summary>
        /// 初始化诊台实例
        /// </summary>
        /// <returns></returns>
        private int initIConsole()
        {
            if (this.IConsole == null)
            {
                IConsole = InterfaceManager.GetINurseConsole();
                IConsole.Init();
                this.IConsole.SaveInfo -= new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.HISFC.Models.Nurse.Seat>(IConsole_SaveInfo);
                this.IConsole.SaveInfo += new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.HISFC.Models.Nurse.Seat>(IConsole_SaveInfo);
            }

            return 1;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 添加诊室
        /// </summary>
        /// <returns></returns>
        private int addRoom()
        {
            FS.SOC.HISFC.Assign.Models.Room room = new FS.SOC.HISFC.Assign.Models.Room();

            FS.HISFC.Models.Base.DepartmentStat deptStat = (this.INurseManager.SelectItem ?? this.INurseManager.FirstItem) as FS.HISFC.Models.Base.DepartmentStat;

            if (deptStat != null)
            {
                room.Nurse.ID = deptStat.PardepCode;
                room.Dept.ID = deptStat.ID;
            }

            this.initIRoom();

            this.IRoom.Add(room);

            if (this.IRoom is Control)
            {
                FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "诊室信息添加";
                FS.FrameWork.WinForms.Classes.Function.ShowControl((Control)this.IRoom, FormBorderStyle.FixedDialog, FormWindowState.Normal);
            }
            
            return 1;
        }

        /// <summary>
        /// 修改诊室
        /// </summary>
        /// <returns></returns>
        private int modifyRoom()
        {
            FS.SOC.HISFC.Assign.Models.Room room = (this.IRoomManager.SelectItem ?? this.IRoomManager.FirstItem) as FS.SOC.HISFC.Assign.Models.Room;

            if (room == null)
            {
                return -1;
            }

            this.initIRoom();

            this.IRoom.Modify(room);

            if (this.IRoom is Control)
            {
                FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "诊室信息修改--" + room.Name;
                FS.FrameWork.WinForms.Classes.Function.ShowControl((Control)this.IRoom, FormBorderStyle.FixedDialog, FormWindowState.Normal);
            }

            return 1;
        }

        /// <summary>
        /// 删除诊室
        /// </summary>
        /// <returns></returns>
        private int deleteRoom()
        {
            FS.SOC.HISFC.Assign.Models.Room room = (this.IRoomManager.SelectItem ?? this.IRoomManager.FirstItem) as FS.SOC.HISFC.Assign.Models.Room;

            if (room == null)
            {
                return -1;
            }
            if (room != null )
            {
                if (CommonController.CreateInstance().MessageBox(this, "是否要删除诊室：" + room.Name + "信息?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return -1;
                }

                //查找对应的诊室
                FS.SOC.HISFC.Assign.BizLogic.Console consoleMgr = new FS.SOC.HISFC.Assign.BizLogic.Console();
                ArrayList al = consoleMgr.QueryValid(room.ID);
                if (al != null && al.Count > 0)
                {
                    if (CommonController.CreateInstance().MessageBox(this, "是否要删除诊室：" + room.Name + "下的所有诊台信息?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return -1;
                    }
                }

                string error = "";
                if (roomMgr.SaveRoom(room, FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Delete, ref error) <= 0)
                {
                    CommonController.CreateInstance().MessageBox(this, error, MessageBoxIcon.Error);
                    return -1;
                }

                this.IRoomManager.Remove(room);
            }

            return 1;
        }

        /// <summary>
        /// 添加诊台
        /// </summary>
        /// <returns></returns>
        private int addConsole()
        {
            FS.HISFC.Models.Nurse.Seat console = new FS.HISFC.Models.Nurse.Seat();

            FS.SOC.HISFC.Assign.Models.Room room = (this.IRoomManager.SelectItem ?? this.IRoomManager.FirstItem) as FS.SOC.HISFC.Assign.Models.Room;

            this.initIConsole();

            if (room != null)
            {
                console.PRoom.ID = room.ID;

                //获取所有的诊室信息
                FS.SOC.HISFC.Assign.BizLogic.Room roomMgr = new FS.SOC.HISFC.Assign.BizLogic.Room();
                ArrayList al = roomMgr.QueryRoomsByDept(room.Dept.ID);
                this.IConsole.Init(al);
            }

            this.IConsole.Add(console);

            if (this.IConsole is Control)
            {
                FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "诊台信息添加";
                FS.FrameWork.WinForms.Classes.Function.ShowControl((Control)this.IConsole, FormBorderStyle.FixedDialog, FormWindowState.Normal);
            }

            return 1;
        }

        /// <summary>
        /// 修改诊台
        /// </summary>
        /// <returns></returns>
        private int modifyConsole()
        {
            FS.HISFC.Models.Nurse.Seat console = (this.IConsoleManager.SelectItem ?? this.IConsoleManager.FirstItem) as FS.HISFC.Models.Nurse.Seat;

            if (console == null)
            {
                return -1;
            }


            this.initIConsole();

            this.IConsole.Modify(console);

            if (this.IConsole is Control)
            {
                FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "诊台信息修改--"+console.Name;
                FS.FrameWork.WinForms.Classes.Function.ShowControl((Control)this.IConsole, FormBorderStyle.FixedDialog, FormWindowState.Normal);
            }

            return 1;
        }

        /// <summary>
        /// 删除诊台
        /// </summary>
        /// <returns></returns>
        private int deleteConsole()
        {
            FS.HISFC.Models.Nurse.Seat console = (this.IConsoleManager.SelectItem ?? this.IConsoleManager.FirstItem) as FS.HISFC.Models.Nurse.Seat;

            if (console == null)
            {
                return -1;
            }
            if (console != null)
            {
                if (CommonController.CreateInstance().MessageBox(this, "是否要删除诊台：" + console.Name + "信息?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return -1;
                }
            }

            string error = "";
            if (consoleMgr.SaveConsole(console, FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Delete, ref error) <= 0)
            {
                CommonController.CreateInstance().MessageBox(this, error, MessageBoxIcon.Error);
                return -1;
            }

            this.IConsoleManager.Remove(console);

            return 1;
        }

        /// <summary>
        /// 显示诊室信息
        /// </summary>
        /// <param name="room"></param>
        private void setRoomInfo(FS.SOC.HISFC.Assign.Models.Room room)
        {
            if (room != null)
            {
                this.lbNurse.Text = "护士站：" + CommonController.CreateInstance().GetDepartmentName(room.Nurse.ID);
                this.lbDept.Text = "科室：" + CommonController.CreateInstance().GetDepartmentName(room.Dept.ID);
                this.lbRoom.Text = "诊室：" + room.Name;
                this.lbConsole.Text = "";
                this.lbConsoleCustom.Text = "";
            }
        }

        /// <summary>
        /// 显诊台信息
        /// </summary>
        /// <param name="console"></param>
        private void setConsoleInfo(FS.HISFC.Models.Nurse.Seat console)
        {
            if (console != null)
            {
                this.lbNurse.Text = "护士站：" + CommonController.CreateInstance().GetDepartmentName(console.PRoom.Nurse.ID);
                this.lbDept.Text = "科室：" + CommonController.CreateInstance().GetDepartmentName(console.PRoom.Dept.ID);
                this.lbRoom.Text = "诊室：" + console.PRoom.Name;
                this.lbConsole.Text = "诊台：" + console.Name;
                this.lbConsoleCustom.Text = "IP：" + console.PRoom.InputCode;
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        public override void Refresh()
        {
            //刷新数据
            this.initDatas();
            base.Refresh();
        }

        #endregion

        #region 触发事件

        protected override void OnLoad(EventArgs e)
        {
            this.Init();
            base.OnLoad(e);
        }

        /// <summary>
        /// 分诊台选择事件
        /// </summary>
        /// <param name="nurse"></param>
        /// <param name="alDept"></param>
        void INurse_SelectedDeptChange(FS.FrameWork.Models.NeuObject nurse, ArrayList alDept)
        {
            //只需要科室就可以了
            if (alDept != null)
            {
                FS.SOC.HISFC.Assign.BizLogic.Room roomMgr = new FS.SOC.HISFC.Assign.BizLogic.Room();
                ArrayList alRooms = new ArrayList();
                foreach (FS.FrameWork.Models.NeuObject obj in alDept)
                {
                    //查找对应的诊室
                    ArrayList al=roomMgr.QueryRoomsByDept(obj.ID);
                    if(al!=null)
                    {
                        alRooms.AddRange(al);
                    }
                }
                this.IConsoleManager.Clear();
                this.IRoomManager.AddRange(alRooms);

                this.initIRoom();

                this.initIConsole();
                this.IConsole.Init(alRooms);
            }
        }

        /// <summary>
        /// 诊室选择事件
        /// </summary>
        /// <param name="nurse"></param>
        /// <param name="alRooms"></param>
        void IRoom_SelectedDeptChange(FS.FrameWork.Models.NeuObject nurse, ArrayList alRooms)
        {
            //只需要科室就可以了
            if (alRooms != null)
            {
                FS.SOC.HISFC.Assign.BizLogic.Console consoleMgr = new FS.SOC.HISFC.Assign.BizLogic.Console();
                ArrayList alSeats = new ArrayList();
                foreach (FS.FrameWork.Models.NeuObject obj in alRooms)
                {
                    //查找对应的诊室
                    ArrayList al = consoleMgr.Query(obj.ID);
                    if (al != null)
                    {
                        alSeats.AddRange(al);
                    } 
                }
                this.IConsoleManager.AddRange(alSeats);

                if (alRooms.Count > 0)
                {
                    FS.SOC.HISFC.Assign.Models.Room room = alRooms[0] as FS.SOC.HISFC.Assign.Models.Room;
                    this.setRoomInfo(room);
                }
            }
        }

        /// <summary>
        /// 诊台选择事件
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="alConsoles"></param>
        void IConsole_SelectedDeptChange(FS.FrameWork.Models.NeuObject obj, ArrayList alConsoles)
        {
            if (obj is FS.HISFC.Models.Nurse.Seat)
            {
                FS.HISFC.Models.Nurse.Seat console = obj as FS.HISFC.Models.Nurse.Seat;
                this.setConsoleInfo(console);
            }
        }

        /// <summary>
        /// 诊室双击事件
        /// </summary>
        /// <param name="o"></param>
        void IRoom_DoubleClickItem(Object o)
        {
            if (o == null)
            {
                return;
            }
            else if (o is FS.SOC.HISFC.Assign.Models.Room)
            {
                this.modifyRoom();
            }
            else if (o is FS.HISFC.Models.Nurse.Seat)
            {
                this.modifyConsole();
            }
        }

        void IRoom_SaveInfo(FS.SOC.HISFC.Assign.Models.Room room)
        {
            if (room == null)
            {
                return;
            }

            this.IRoomManager.Add(room);
        }

        void IConsole_SaveInfo(FS.HISFC.Models.Nurse.Seat console)
        {
            if (console == null)
            {
                return;
            }

            this.IConsoleManager.Add(console);
            this.setConsoleInfo(console);
        }

        /// <summary>
        /// 右键事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void menu_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripItem)
            {
                this.ToolStrip_ItemClicked(sender, new ToolStripItemClickedEventArgs((ToolStripItem)sender));
                this.Refresh();//刷新
            }
        }

        /// <summary>
        /// 科室选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbDept.SelectedItem != null)
            {
                ArrayList al = new ArrayList();
                al.Add(this.cmbDept.SelectedItem);
                INurse_SelectedDeptChange(null, al);
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
