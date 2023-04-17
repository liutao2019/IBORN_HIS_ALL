using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;

namespace FS.HISFC.Components.Registration
{
    public partial class ucSchemaTemplet : UserControl
    {
        public ucSchemaTemplet()
        {
            InitializeComponent();

            this.fpSpread1.Change += new ChangeEventHandler(fpSpread1_Change);
            this.fpSpread1.EditModeOff += new EventHandler(fpSpread1_EditModeOff);

            this.fpSpread1.EditModeOn += new System.EventHandler(this.fpSpread1_EditModeOn);
            this.fpSpread1.EditChange += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.fpSpread1_EditChange);

            this.fpSpread1.CellDoubleClick += new CellClickEventHandler(fpSpread1_CellDoubleClick);

            fpSpread1.KeyDown += new KeyEventHandler(fpSpread1_KeyDown);
        }

        #region 域
        //--------------------------------------------------------------------
        /// <summary>
        /// 模板集合
        /// </summary>
        private DataTable dsItems;
        private DataView dv;
        /// <summary>
        /// 集合
        /// </summary>
        private ArrayList al;
        /// <summary>
        /// 出诊模板管理类
        /// </summary>
        protected FS.HISFC.BizLogic.Registration.SchemaTemplet TempletMgr = new FS.HISFC.BizLogic.Registration.SchemaTemplet();
        /// <summary>
        /// 科室管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager Mgr = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// 显示星期
        /// </summary>
        private DayOfWeek week = DayOfWeek.Monday;
        /// <summary>
        /// 显示星期
        /// </summary>
        public DayOfWeek Week
        {
            get { return week; }
            set { week = value; }
        }
        /// <summary>
        /// 搜索员工代码
        /// </summary>
        public string SearchEmployee
        {
            get
            {
                return this.searchEmpl;
            }

            set
            {
                this.searchEmpl = value;

                if (value != "" && value != null)
                {
                    for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                    {
                        if (this.fpSpread1_Sheet1.GetText(i, (int)cols.DoctID) == value)
                        {
                            this.fpSpread1.Focus();
                            this.fpSpread1_Sheet1.ActiveRowIndex = i;
                            this.fpSpread1_Sheet1.SetActiveCell(i, (int)cols.DoctName, false);
                            break;
                        }
                    }
                }
            }
        }

        private string searchEmpl;

        /// <summary>
        /// 模板类型
        /// </summary>
        public FS.HISFC.Models.Base.EnumSchemaType SchemaType = FS.HISFC.Models.Base.EnumSchemaType.Dept;
        /// <summary>
        /// 科室列表
        /// </summary>
        private FS.FrameWork.WinForms.Controls.NeuListBoxPopup lbDept = new FS.FrameWork.WinForms.Controls.NeuListBoxPopup();
        /// <summary>
        /// 午别列表
        /// </summary>
        private FS.FrameWork.WinForms.Controls.NeuListBoxPopup lbNoon = new FS.FrameWork.WinForms.Controls.NeuListBoxPopup();
        /// <summary>
        /// 医生列表
        /// </summary>
        private FS.FrameWork.WinForms.Controls.NeuListBoxPopup lbDoct = new FS.FrameWork.WinForms.Controls.NeuListBoxPopup();
        /// <summary>
        /// 医生类别
        /// </summary>
        private FS.FrameWork.WinForms.Controls.NeuListBoxPopup lbDoctType = new FS.FrameWork.WinForms.Controls.NeuListBoxPopup();
        /// <summary>
        /// 停诊原因列表
        /// </summary>
        private FS.FrameWork.WinForms.Controls.NeuListBoxPopup lbStopRn = new FS.FrameWork.WinForms.Controls.NeuListBoxPopup();
        /// <summary>
        /// 挂号级别列表
        /// </summary>
        private FS.FrameWork.WinForms.Controls.NeuListBoxPopup lbRegLevel = new FS.FrameWork.WinForms.Controls.NeuListBoxPopup();

        /// <summary>
        /// 当前科室
        /// </summary>
        public FS.HISFC.Models.Base.Department Dept
        {
            set
            {
                dept = value;
                if (dept == null)
                {
                    dept = new FS.HISFC.Models.Base.Department();
                }
            }
        }
        /// <summary>
        /// 当前科室
        /// </summary>
        private FS.HISFC.Models.Base.Department dept = new FS.HISFC.Models.Base.Department();

        /// <summary>
        /// 预约默认时间段
        /// </summary>
        private int timeZone = 0;
        /// <summary>
        /// 是否显示医生类别
        /// </summary>
        private bool IsShowDoctType = false;
        /// <summary>
        /// 是否显示挂号级别
        /// </summary>
        private bool IsShowRegLevel = false;
        #endregion
        #region 列
        protected enum cols
        {
            /// <summary>
            /// ID
            /// </summary>
            ID,
            /// <summary>
            /// 科室代码
            /// </summary>
            DeptID,
            /// <summary>
            /// 科室名称
            /// </summary>
            DeptName,
            /// <summary>
            /// 医生代码
            /// </summary>
            DoctID,
            /// <summary>
            /// 医生名称
            /// </summary>
            DoctName,
            /// <summary>
            /// 医生类别
            /// </summary>
            DoctType,
            /// <summary>
            /// 挂号级别代码
            /// </summary>
            RegLevlCode,
            /// <summary>
            /// 挂号级别名称
            /// </summary>
            RegLevlName,
            /// <summary>
            /// 午别代码
            /// </summary>
            Noon,
            /// <summary>
            /// 开始时间
            /// </summary>
            BeginTime,
            /// <summary>
            /// 结束时间
            /// </summary>
            EndTime,
            /// <summary>
            /// 挂号限额
            /// </summary>
            RegLmt,
            /// <summary>
            /// 预约限额
            /// </summary>
            TelLmt,
            /// <summary>
            /// 特诊限额
            /// </summary>
            SpeLmt,
            /// <summary>
            /// 是否加号
            /// </summary>
            IsAppend,
            /// <summary>
            /// 是否有效
            /// </summary>
            Valid,
            /// <summary>
            /// 备注
            /// </summary>
            Memo,
            /// <summary>
            /// 停诊原因
            /// </summary>
            StopReasonID,

            /// <summary>
            /// 停诊原因
            /// </summary>			
            StopReasonName,

            /// <summary>
            /// 诊室ID
            /// </summary>
            RoomID,

            /// <summary>
            /// 诊室名称
            /// </summary>
            RoomName,

            /// <summary>
            /// 诊台ID
            /// </summary>
            ConsoleID,

            /// <summary>
            /// 诊台名称
            /// </summary>
            ConsoleName
        }
        #endregion

        #region 初始化
        /// <summary>
        ///  初始化
        /// </summary>        
        /// <param name="w"></param>
        /// <param name="type"></param>
        public void Init(DayOfWeek w, FS.HISFC.Models.Base.EnumSchemaType type)
        {
            this.week = w;
            this.SchemaType = type;

            this.initDataSet();

            this.initNoon();
            this.initDept();
            //专家排班,初始化医生列表、医生类别列表
            if (type == FS.HISFC.Models.Base.EnumSchemaType.Doct)
            {
                this.initDoct();
                this.initDoctType();
                this.initStopRn();
            }
            this.initRegLevl();

            this.visible(false);

            this.initFp();

            //默认预约时间段
            FS.FrameWork.Management.ControlParam ctlMgr = new FS.FrameWork.Management.ControlParam();

            string rtn = ctlMgr.QueryControlerInfo("400018");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "0";
            this.timeZone = int.Parse(rtn);

            //排班是否显示医生类别
            rtn = ctlMgr.QueryControlerInfo("400025");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "0";
            this.IsShowDoctType = FS.FrameWork.Function.NConvert.ToBoolean(rtn);

            //医生排班是否到挂号级别
            rtn = ctlMgr.QueryControlerInfo("400030");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "0";
            this.IsShowRegLevel = FS.FrameWork.Function.NConvert.ToBoolean(rtn);
        }

        /// <summary>
        /// Init DataSet
        /// </summary>
        private void initDataSet()
        {
            dsItems = new DataTable("Templet");

            dsItems.Columns.AddRange(new DataColumn[]
			{
				new DataColumn("ID",System.Type.GetType("System.String")),
				new DataColumn("DeptID",System.Type.GetType("System.String")),
				new DataColumn("DeptName",System.Type.GetType("System.String")),
				new DataColumn("DoctID",System.Type.GetType("System.String")),
				new DataColumn("DoctName",System.Type.GetType("System.String")),
				new DataColumn("DoctType",System.Type.GetType("System.String")),
				new DataColumn("RegLevlCode",System.Type.GetType("System.String")),
				new DataColumn("RegLevlName",System.Type.GetType("System.String")),
				new DataColumn("Noon",System.Type.GetType("System.String")),
				new DataColumn("BeginTime",System.Type.GetType("System.DateTime")),
				new DataColumn("EndTime",System.Type.GetType("System.DateTime")),
				new DataColumn("RegLmt",System.Type.GetType("System.Decimal")),
				new DataColumn("TelLmt",System.Type.GetType("System.Decimal")),
				new DataColumn("SpeLmt",System.Type.GetType("System.Decimal")),
				new DataColumn("IsAppend",System.Type.GetType("System.Boolean")),
				new DataColumn("Valid",System.Type.GetType("System.Boolean")),						
				new DataColumn("Memo",System.Type.GetType("System.String")),
				new DataColumn("StopReasonID",System.Type.GetType("System.String")),
				new DataColumn("StopReasonName",System.Type.GetType("System.String")),

				new DataColumn("RoomID",System.Type.GetType("System.String")),
				new DataColumn("RoomName",System.Type.GetType("System.String")),
				new DataColumn("ConsoleID",System.Type.GetType("System.String")),
				new DataColumn("ConsoleName",System.Type.GetType("System.String"))
			});

        }
        /// <summary>
        /// 初始化farpoint
        /// </summary>
        private void initFp()
        {
            InputMap im;

            im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Down, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Up, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Escape, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.F2, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.F3, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
        }

        /// <summary>
        /// 初始化无别列表
        /// </summary>
        private void initNoon()
        {
            this.lbNoon.ItemSelected += new EventHandler(lbNoon_SelectItem);
            this.groupBox1.Controls.Add(this.lbNoon);
            this.lbNoon.BackColor = this.label1.BackColor;
            this.lbNoon.Font = new Font("宋体", 11F);
            this.lbNoon.BorderStyle = BorderStyle.None;
            this.lbNoon.Cursor = Cursors.Hand;
            this.lbNoon.Location = new Point(this.label1.Left + 1, this.label1.Top + 1);
            this.lbNoon.Size = new Size(this.label1.Width - 2, this.label1.Height - 2);

            FS.HISFC.BizLogic.Registration.Noon noonMgr =
                            new FS.HISFC.BizLogic.Registration.Noon();
            //得到午别
            al = noonMgr.Query();

            if (al == null)
            {
                MessageBox.Show("获取午别信息时出错!" + Mgr.Err, "提示");
                return;
            }

            this.lbNoon.AddItems(al);
        }
        /// <summary>
        /// 初始化出诊科室
        /// </summary>
        private void initDept()
        {
            FS.HISFC.Models.Base.Employee curEmployee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            FS.HISFC.Models.Base.Department curDepartment = curEmployee.Dept as FS.HISFC.Models.Base.Department;
            
            this.lbDept.ItemSelected += new EventHandler(lbDept_SelectItem);
            this.groupBox1.Controls.Add(this.lbDept);
            this.lbDept.BackColor = this.label1.BackColor;
            this.lbDept.Font = new Font("宋体", 11F);
            this.lbDept.BorderStyle = BorderStyle.None;
            this.lbDept.Cursor = Cursors.Hand;
            this.lbDept.Location = new Point(this.label1.Left + 1, this.label1.Top + 1);
            this.lbDept.Size = new Size(this.label1.Width - 2, this.label1.Height - 2);

            //得到科室列表
            this.al = Mgr.QueryRegDepartment();
            if (al == null)
            {
                MessageBox.Show("获取门诊科室列表时出错!" + Mgr.Err, "提示");
                return;
            }

            ArrayList newAll = new ArrayList();
            foreach (FS.HISFC.Models.Base.Department dept in al)
            {
                if (curDepartment.HospitalID == dept.HospitalID)// {63B27717-4D42-46d6-9AE3-CE89853E9B5E
                {
                    newAll.Add(dept);
                }
            }

            al = new ArrayList();
            al = newAll;
            this.lbDept.AddItems(al);
            this.lbDept.BringToFront();

        }


        /// <summary>
        /// 初始化出诊医生
        /// </summary>
        private void initDoct()
        {
            this.lbDoct.ItemSelected += new EventHandler(lbDoct_SelectItem);
            this.groupBox1.Controls.Add(this.lbDoct);
            this.lbDoct.BackColor = this.label1.BackColor;
            this.lbDoct.Font = new Font("宋体", 11F);
            this.lbDoct.BorderStyle = BorderStyle.None;
            this.lbDoct.Cursor = Cursors.Hand;
            this.lbDoct.Location = new Point(this.label1.Left + 1, this.label1.Top + 1);
            this.lbDoct.Size = new Size(this.label1.Width - 2, this.label1.Height - 2);

            //得到医生列表
            al = Mgr.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            if (al == null)
            {
                MessageBox.Show("获取门诊医生列表时出错!" + Mgr.Err, "提示");
                return;
            }

            this.lbDoct.AddItems(al);
            this.lbDoct.BringToFront();
        }

        /// <summary>
        /// 初始化医生类别
        /// </summary>
        private void initDoctType()
        {
            this.lbDoctType.ItemSelected += new EventHandler(lbDoctType_SelectItem);
            this.groupBox1.Controls.Add(this.lbDoctType);
            this.lbDoctType.BackColor = this.label1.BackColor;
            this.lbDoctType.Font = new Font("宋体", 11F);
            this.lbDoctType.BorderStyle = BorderStyle.None;
            this.lbDoctType.Cursor = Cursors.Hand;
            this.lbDoctType.Location = new Point(this.label1.Left + 1, this.label1.Top + 1);
            this.lbDoctType.Size = new Size(this.label1.Width - 2, this.label1.Height - 2);

            //得到医生类别
            al = Mgr.QueryConstantList("DoctType");
            if (al == null)
            {
                MessageBox.Show("获取医生类别时出错!" + Mgr.Err, "提示");
                return;
            }

            this.lbDoctType.AddItems(al);
        }

        /// <summary>
        /// 初始化停诊原因列表
        /// </summary>
        private void initStopRn()
        {
            this.lbStopRn.ItemSelected += new EventHandler(lbStopRn_SelectItem);
            this.groupBox1.Controls.Add(this.lbStopRn);
            this.lbStopRn.BackColor = this.label1.BackColor;
            this.lbStopRn.Font = new Font("宋体", 11F);
            this.lbStopRn.BorderStyle = BorderStyle.None;
            this.lbStopRn.Cursor = Cursors.Hand;
            this.lbStopRn.Location = new Point(this.label1.Left + 1, this.label1.Top + 1);
            this.lbStopRn.Size = new Size(this.label1.Width - 2, this.label1.Height - 2);

            //得到医生类别
            al = Mgr.QueryConstantList("StopReason");
            if (al == null)
            {
                MessageBox.Show("获取停诊原因时出错!" + Mgr.Err, "提示");
                return;
            }

            this.lbStopRn.AddItems(al);
        }
        /// <summary>
        /// 生成挂号级别列表
        /// </summary>
        private void initRegLevl()
        {
            this.lbRegLevel.ItemSelected += new EventHandler(lbRegLevel_SelectItem);
            this.groupBox1.Controls.Add(this.lbRegLevel);
            this.lbRegLevel.BackColor = this.label1.BackColor;
            this.lbRegLevel.Font = new Font("宋体", 11F);
            this.lbRegLevel.BorderStyle = BorderStyle.None;
            this.lbRegLevel.Cursor = Cursors.Hand;
            this.lbRegLevel.Location = new Point(this.label1.Left + 1, this.label1.Top + 1);
            this.lbRegLevel.Size = new Size(this.label1.Width - 2, this.label1.Height - 2);

            FS.HISFC.BizLogic.Registration.RegLevel reglevlMgr = new FS.HISFC.BizLogic.Registration.RegLevel();
            //得到挂号级别
            al = reglevlMgr.Query(true);

            this.lbRegLevel.AddItems(al);
        }
        #endregion

        #region 查找

        /// <summary>
        /// 根据午别代码得到午别名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string getNoonNameByID(string id)
        {
            foreach (FS.FrameWork.Models.NeuObject obj in this.lbNoon.NeuItems)
            {
                if (obj.ID == id) return obj.Name;
            }
            return id;
        }
        /// <summary>
        /// 根据午别名称得到午别代码
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string getNoonIDByName(string name)
        {
            foreach (FS.FrameWork.Models.NeuObject obj in this.lbNoon.NeuItems)
            {
                if (obj.Name == name) return obj.ID;
            }
            return "";
        }
        /// <summary>
        /// 获取午别结束时间
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private DateTime getNoonEndDate(string ID)
        {
            foreach (FS.FrameWork.Models.NeuObject obj in this.lbNoon.NeuItems)
            {
                if (obj.ID == ID) return (obj as FS.HISFC.Models.Base.Noon).EndTime;
            }
            return DateTime.MinValue;
        }
        /// <summary>
        /// 根据代码获得医生类别名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string getTypeNameByID(string id)
        {
            if (id == null || id.Trim() == "")
            {
                return "";
            }

            foreach (FS.FrameWork.Models.NeuObject obj in this.lbDoctType.NeuItems)
            {
                if (obj.ID == id) return obj.Name;
            }

            return id;
        }
        /// <summary>
        /// 根据名称获取医生类别代码
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        private string getTypeIDByName(string Name)
        {
            if (Name == null || Name.Trim() == "")
            {
                return "";
            }

            foreach (FS.FrameWork.Models.NeuObject obj in this.lbDoctType.NeuItems)
            {
                if (obj.Name == Name) return obj.ID;
            }

            return "";
        }

        #endregion

        #region FarPoint操作
        /// <summary>
        /// 设置下来列表位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_EditModeOn(object sender, System.EventArgs e)
        {

            fpSpread1.EditingControl.KeyDown += new KeyEventHandler(EditingControl_KeyDown);
            fpSpread1.EditingControl.DoubleClick += new EventHandler(EditingControl_DoubleClick);

            if (this.fpSpread1_Sheet1.ActiveColumnIndex == (int)cols.DeptName ||
                this.fpSpread1_Sheet1.ActiveColumnIndex == (int)cols.Noon ||
                this.fpSpread1_Sheet1.ActiveColumnIndex == (int)cols.DoctName ||
                this.fpSpread1_Sheet1.ActiveColumnIndex == (int)cols.DoctType ||
                this.fpSpread1_Sheet1.ActiveColumnIndex == (int)cols.StopReasonName ||
                this.fpSpread1_Sheet1.ActiveColumnIndex == (int)cols.RegLevlName)
            {
                this.setLocation();
                this.visible(false);
            }
            else
            { this.visible(false); }
        }


        /// <summary>
        /// 弹出常数选择
        /// </summary>
        private void PopItem(ArrayList al, int index)
        {
            FS.FrameWork.Models.NeuObject info = new FS.FrameWork.Models.NeuObject();
            if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(al, ref info) == 0)
            {
                return;
            }
            else
            {
                //诊室
                if (index == (int)cols.RoomName)
                {
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.RoomID].Value = info.ID;
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.RoomName].Value = info.Name;
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.ConsoleID].Value = null;
                    fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.ConsoleName].Value = null;
                }
                //诊台
                else if (index == (int)cols.ConsoleName)
                {
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.ConsoleID].Value = info.ID;
                    fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.ConsoleName].Value = info.Name;
                }
            }
        }

        /// <summary>
        /// 诊室列表
        /// </summary>
        private ArrayList alRoom = null;

        /// <summary>
        /// 诊台列表
        /// </summary>
        private ArrayList alConsole = null;

        void fpSpread1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                fpSpread1_CellDoubleClick(sender, null);
            }
        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.fpSpread1.ContainsFocus)
            {
                Classes.LocalManager localMgr = new FS.HISFC.Components.Registration.Classes.LocalManager();
                if (this.fpSpread1_Sheet1.ActiveColumnIndex == (int)cols.RoomName)
                {
                    if (alRoom == null)
                    {
                        alRoom = localMgr.GetAllRoom(this.SchemaType);//{0FBEA522-F50E-4fd2-9108-9A8FA8712890} 添加B超排班 类型为2
                        if (alRoom == null)
                        {
                            MessageBox.Show("获取诊室列表出错！\r\n" + localMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    this.PopItem(this.alRoom, (int)cols.RoomName);
                }
                else if (this.fpSpread1_Sheet1.ActiveColumnIndex == (int)cols.ConsoleName)
                {
                    alConsole = new ArrayList();
                    if (fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.RoomID].Value == null
                        || string.IsNullOrEmpty(fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.RoomID].Value.ToString()))
                    {
                        MessageBox.Show("请先选择诊室！");
                        return;
                    }
                    string strRoomID = fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.RoomID].Value.ToString();

                    alConsole = localMgr.GetConsole(strRoomID);
                    if (alConsole == null || alConsole.Count == 0)
                    {
                        MessageBox.Show("获取诊台列表出错！\r\n可能是未维护诊台，或诊台无效！\r\n" + localMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    this.PopItem(this.alConsole, (int)cols.ConsoleName);
                }
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_EditModeOff(object sender, EventArgs e)
        {
            try
            {
                this.fpSpread1.EditingControl.KeyDown -= new KeyEventHandler(EditingControl_KeyDown);
                this.fpSpread1.EditingControl.DoubleClick -= new EventHandler(EditingControl_DoubleClick);
            }
            catch { }
        }

        /// <summary>
        /// 检索下来列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_EditChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            int col = this.fpSpread1_Sheet1.ActiveColumnIndex;
            int row = this.fpSpread1_Sheet1.ActiveRowIndex;

            string text = this.fpSpread1_Sheet1.ActiveCell.Text.Trim();
            text = FS.FrameWork.Public.String.TakeOffSpecialChar(text, "#", "%", "[", "'", "]", ",", "$", "(", ")", "|", "\'", "\"", "\\");
            if (col == (int)cols.DeptName)
            {
                this.fpSpread1_Sheet1.Cells[row, (int)cols.DeptID].Text = "";
                this.lbDept.Filter(text);

                if (this.groupBox1.Visible == false) this.visible(true);
            }
            else if (col == (int)cols.Noon)
            {
                this.lbNoon.Filter(text);
                if (this.groupBox1.Visible == false) this.visible(true);
            }
            else if (col == (int)cols.DoctName)
            {
                this.fpSpread1_Sheet1.Cells[row, (int)cols.DoctID].Text = "";
                this.lbDoct.Filter(text);

                if (this.groupBox1.Visible == false) this.visible(true);
            }
            else if (col == (int)cols.DoctType)
            {
                this.lbDoctType.Filter(text);
                if (this.groupBox1.Visible == false) this.visible(true);
            }
            else if (col == (int)cols.RegLevlName)
            {
                this.fpSpread1_Sheet1.Cells[row, (int)cols.RegLevlCode].Text = "";
                this.lbRegLevel.Filter(text);

                if (this.groupBox1.Visible == false) this.visible(true);
            }
            else if (col == (int)cols.StopReasonName)
            {
                this.fpSpread1_Sheet1.Cells[row, (int)cols.StopReasonID].Text = "";
                this.lbStopRn.Filter(text);
                if (this.groupBox1.Visible == false) this.visible(true);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_Change(object sender, ChangeEventArgs e)
        {
            if (this.fpSpread1_Sheet1.ActiveColumnIndex == (int)cols.Valid)
            {
                string cellText = this.fpSpread1_Sheet1.GetText(this.fpSpread1_Sheet1.ActiveRowIndex, this.fpSpread1_Sheet1.ActiveColumnIndex);

                if (cellText.ToUpper() == "FALSE")
                {
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.BeginTime].BackColor = Color.MistyRose;
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.EndTime].BackColor = Color.MistyRose;
                }
                else
                {
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.BeginTime].BackColor = SystemColors.Window;
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.EndTime].BackColor = SystemColors.Window;
                }
            }
        }
        /// <summary>
        /// 回车处理
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                #region enter
                if (this.fpSpread1.ContainsFocus)
                {
                    int col = this.fpSpread1_Sheet1.ActiveColumnIndex;

                    if (col == (int)cols.DeptName)
                    {
                        if (this.selectDept() == -1) return false;

                        if (this.SchemaType == FS.HISFC.Models.Base.EnumSchemaType.Dept)
                        {
                            this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.Noon, false);
                        }
                        else
                        {
                            this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.DoctName, false);
                        }
                    }
                    else if (col == (int)cols.DoctName)
                    {
                        if (this.selectDoct() == -1) return false;

                        if (this.IsShowDoctType && this.SchemaType == FS.HISFC.Models.Base.EnumSchemaType.Doct)
                        {
                            this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.DoctType, false);
                        }
                        else
                        {
                            this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.Noon, false);
                        }
                    }
                    else if (col == (int)cols.DoctType)
                    {
                        if (this.selectDoctType() == -1) return false;

                        if (this.SchemaType == FS.HISFC.Models.Base.EnumSchemaType.Doct && this.IsShowRegLevel)
                        {
                            this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.RegLevlName, false);
                        }
                        else
                        {
                            this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.Noon, false);
                        }
                    }
                    else if (col == (int)cols.RegLevlName)
                    {
                        if (this.selectRegLevel() == -1) return false;

                        this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.Noon, false);
                    }
                    else if (col == (int)cols.Noon)
                    {
                        if (this.selectNoon() == -1) return false;
                        this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.BeginTime, false);
                    }
                    else if (col == (int)cols.BeginTime)
                    {
                        this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.EndTime, false);
                    }
                    else if (col == (int)cols.EndTime)
                    {
                        this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.RegLmt, false);
                    }
                    else if (col == (int)cols.RegLmt)
                    {
                        this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.TelLmt, false);
                    }
                    else if (col == (int)cols.TelLmt)
                    {
                        if (this.SchemaType == FS.HISFC.Models.Base.EnumSchemaType.Dept)
                        {
                            this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.IsAppend, false);
                        }
                        else
                        {
                            this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.SpeLmt, false);
                        }
                    }
                    else if (col == (int)cols.SpeLmt)
                    {
                        this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.IsAppend, false);
                    }
                    else if (col == (int)cols.IsAppend)
                    {
                        this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.Valid, false);
                    }
                    else if (col == (int)cols.Valid)
                    {
                        this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.Memo, false);
                    }
                    else if (col == (int)cols.Memo)
                    {
                        this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.StopReasonName, false);
                    }
                    else if (col == (int)cols.StopReasonName)
                    {
                        if (this.selectStopRn() == -1) return false;

                        if (this.fpSpread1_Sheet1.ActiveRowIndex != this.fpSpread1_Sheet1.RowCount - 1)
                        {
                            this.fpSpread1_Sheet1.ActiveRowIndex++;
                            this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.DeptName, false);
                        }
                        else
                        {
                            this.Add();
                        }
                    }
                    return true;
                }
                #endregion

            }
            else if (keyData == Keys.Up)
            {
                #region up
                if (this.fpSpread1.ContainsFocus)
                {
                    if (this.groupBox1.Visible)
                    { this.priorRow(); }
                    else
                    {
                        int CurrentRow = fpSpread1_Sheet1.ActiveRowIndex;
                        if (CurrentRow > 0)
                        {
                            fpSpread1_Sheet1.ActiveRowIndex = CurrentRow - 1;
                            fpSpread1_Sheet1.AddSelection(CurrentRow - 1, 0, 1, 0);
                        }
                    }
                    return true;
                }
                #endregion

            }
            else if (keyData == Keys.Down)
            {
                #region down
                if (this.fpSpread1.ContainsFocus)
                {
                    if (this.groupBox1.Visible)
                    { this.nextRow(); }
                    else
                    {
                        int CurrentRow = fpSpread1_Sheet1.ActiveRowIndex;
                        if (CurrentRow >= 0 && CurrentRow <= fpSpread1_Sheet1.RowCount - 2)
                        {
                            fpSpread1_Sheet1.ActiveRowIndex = CurrentRow + 1;
                            fpSpread1_Sheet1.AddSelection(CurrentRow + 1, 0, 1, 0);
                        }
                    }
                    return true;
                }
                #endregion

            }
            else if (keyData == Keys.Escape)
            {
                this.visible(false);
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        #endregion

        #region 下拉控件功能
        /// <summary>
        /// 设置groupBox1的显示位置
        /// </summary>
        /// <returns></returns>
        private void setLocation()
        {
            Control cell = fpSpread1.EditingControl;
            if (cell == null) return;

            int y = fpSpread1.Top + cell.Top + cell.Height + this.groupBox1.Height + 7;
            if (y <= this.Height)
            {
                if (fpSpread1.Left + cell.Left + this.groupBox1.Width + 20 <= this.Width)
                {
                    this.groupBox1.Location = new Point(fpSpread1.Left + cell.Left + 20, y - this.groupBox1.Height);
                }
                else
                {
                    this.groupBox1.Location = new Point(this.Width - this.groupBox1.Width - 10, y - this.groupBox1.Height);
                }
            }
            else
            {
                if (fpSpread1.Left + cell.Left + this.groupBox1.Width + 20 <= this.Width)
                {
                    this.groupBox1.Location = new Point(fpSpread1.Left + cell.Left + 20, fpSpread1.Top + cell.Top - this.groupBox1.Height - 7);
                }
                else
                {
                    this.groupBox1.Location = new Point(this.Width - this.groupBox1.Width - 10, fpSpread1.Top + cell.Top - this.groupBox1.Height - 7);
                }
            }
        }

        /// <summary>
        /// 设置控件是否可见
        /// </summary>
        /// <param name="visible"></param>
        private void visible(bool visible)
        {
            if (visible == false)
            { this.groupBox1.Visible = false; }
            else
            {
                int col = this.fpSpread1_Sheet1.ActiveColumnIndex;
                if (col == (int)cols.DeptName)
                {
                    this.lbDept.BringToFront();
                    this.groupBox1.Visible = true;
                }
                else if (col == (int)cols.Noon)
                {
                    this.lbNoon.BringToFront();
                    this.groupBox1.Visible = true;
                }
                else if (col == (int)cols.DoctName)
                {
                    this.lbDoct.BringToFront();
                    this.groupBox1.Visible = true;
                }
                else if (col == (int)cols.DoctType)
                {
                    this.lbDoctType.BringToFront();
                    this.groupBox1.Visible = true;
                }
                else if (col == (int)cols.StopReasonName)
                {
                    this.lbStopRn.BringToFront();
                    this.groupBox1.Visible = true;
                }
                else if (col == (int)cols.RegLevlName)
                {
                    this.lbRegLevel.BringToFront();
                    this.groupBox1.Visible = true;
                }
            }
        }
        /// <summary>
        /// 前一行
        /// </summary>
        private void nextRow()
        {
            int col = this.fpSpread1_Sheet1.ActiveColumnIndex;
            if (col == (int)cols.DeptName)
            {
                this.lbDept.NextRow();
            }
            else if (col == (int)cols.Noon)
            {
                this.lbNoon.NextRow();
            }
            else if (col == (int)cols.DoctName)
            {
                this.lbDoct.NextRow();
            }
            else if (col == (int)cols.DoctType)
            {
                this.lbDoctType.NextRow();
            }
            else if (col == (int)cols.StopReasonName)
            {
                this.lbStopRn.NextRow();
            }
            else if (col == (int)cols.RegLevlName)
            {
                this.lbRegLevel.NextRow();
            }
        }
        /// <summary>
        /// 上一行
        /// </summary>
        private void priorRow()
        {
            int col = this.fpSpread1_Sheet1.ActiveColumnIndex;
            if (col == (int)cols.DeptName)
            {
                this.lbDept.PriorRow();
            }
            else if (col == (int)cols.Noon)
            {
                this.lbNoon.PriorRow();
            }
            else if (col == (int)cols.DoctName)
            {
                this.lbDoct.PriorRow();
            }
            else if (col == (int)cols.DoctType)
            {
                this.lbDoctType.PriorRow();
            }
            else if (col == (int)cols.StopReasonName)
            {
                this.lbStopRn.PriorRow();
            }
            else if (col == (int)cols.RegLevlName)
            {
                this.lbRegLevel.PriorRow();
            }
        }
        /// <summary>
        /// 选择午别
        /// </summary>
        /// <returns></returns>
        private int selectNoon()
        {
            int row = this.fpSpread1_Sheet1.ActiveRowIndex;
            FS.FrameWork.Models.NeuObject obj;

            this.fpSpread1.StopCellEditing();

            if (this.groupBox1.Visible)
            {
                obj = new FS.FrameWork.Models.NeuObject();

                obj = this.lbNoon.GetSelectedItem();
                if (obj == null) return -1;

                this.fpSpread1_Sheet1.SetValue(row, (int)cols.Noon, obj.Name, false);

                if (this.timeZone == 0)
                {
                    //设定默认时间为午别的起始、结束时间
                    this.fpSpread1_Sheet1.SetValue(row, (int)cols.BeginTime,
                            (obj as FS.HISFC.Models.Base.Noon).StartTime, false);
                    this.fpSpread1_Sheet1.SetValue(row, (int)cols.EndTime,
                            (obj as FS.HISFC.Models.Base.Noon).EndTime, false);
                }
                else//医生模板默认从下找到的第一个结束时间为起始时间,结束时间+1
                {
                    this.SetTimeZone(row, (FS.HISFC.Models.Base.Noon)obj);
                }
                this.visible(false);
            }

            return 0;
        }
        /// <summary>
        /// 设置医生排班默认时间段
        /// </summary>
        /// <param name="currentRow"></param>
        /// <param name="noon"></param>
        /// <returns></returns>
        private int SetTimeZone(int currentRow, FS.HISFC.Models.Base.Noon noon)
        {
            string doctID = this.fpSpread1_Sheet1.GetText(currentRow, (int)cols.DoctID);
            string deptID = this.fpSpread1_Sheet1.GetText(currentRow, (int)cols.DeptID);
            DateTime begin = DateTime.MinValue;
            object obj;

            if (doctID == "") return 0;

            for (int i = currentRow; i >= 0; i--)
            {
                if (i == currentRow) continue;

                if (this.fpSpread1_Sheet1.GetText(i, (int)cols.DoctID) == doctID &&
                    this.fpSpread1_Sheet1.GetText(i, (int)cols.Noon) == noon.Name &&
                    this.fpSpread1_Sheet1.GetText(i, (int)cols.DeptID) == deptID)
                {
                    obj = this.fpSpread1_Sheet1.GetValue(i, (int)cols.EndTime);
                    if (obj == null) continue;

                    begin = DateTime.Parse(obj.ToString());
                    break;
                }
            }

            if (begin != DateTime.MinValue && begin.TimeOfDay < noon.EndTime.TimeOfDay)
            {
                this.fpSpread1_Sheet1.SetValue(currentRow, (int)cols.BeginTime, begin, false);
                begin = begin.AddHours(this.timeZone);

                if (begin.TimeOfDay > noon.EndTime.TimeOfDay)
                {
                    begin = noon.EndTime;
                }
                this.fpSpread1_Sheet1.SetValue(currentRow, (int)cols.EndTime, begin, false);
            }
            else
            {
                begin = noon.StartTime;
                this.fpSpread1_Sheet1.SetValue(currentRow, (int)cols.BeginTime, begin, false);

                begin = begin.AddHours(this.timeZone);
                if (begin.TimeOfDay > noon.EndTime.TimeOfDay)
                {
                    begin = noon.EndTime;
                }
                this.fpSpread1_Sheet1.SetValue(currentRow, (int)cols.EndTime, begin, false);
            }

            return 0;
        }
        /// <summary>
        /// 选择科室
        /// </summary>
        /// <returns></returns>
        private int selectDept()
        {
            int row = this.fpSpread1_Sheet1.ActiveRowIndex;
            FS.FrameWork.Models.NeuObject obj;

            this.fpSpread1.StopCellEditing();

            if (this.groupBox1.Visible)
            {
                obj = new FS.FrameWork.Models.NeuObject();
                obj = this.lbDept.GetSelectedItem();
                if (obj == null) return -1;

                this.fpSpread1_Sheet1.SetValue(row, (int)cols.DeptName, obj.Name, false);
                this.fpSpread1_Sheet1.SetValue(row, (int)cols.DeptID, obj.ID, false);
                this.visible(false);
            }
            else
            {
                string deptId = this.fpSpread1_Sheet1.GetText(row, (int)cols.DeptID);

                if (deptId == null || deptId == "")
                    this.fpSpread1_Sheet1.SetValue(row, (int)cols.DeptName, "", false);
            }

            return 0;
        }

        /// <summary>
        /// 选择医生
        /// </summary>
        /// <returns></returns>
        private int selectDoct()
        {
            int row = this.fpSpread1_Sheet1.ActiveRowIndex;
            FS.FrameWork.Models.NeuObject obj;

            this.fpSpread1.StopCellEditing();

            if (this.groupBox1.Visible)
            {
                obj = new FS.FrameWork.Models.NeuObject();
                obj = this.lbDoct.GetSelectedItem();
                if (obj == null) return -1;

                //判断所选医生的科室和选择的科室是否是相同
                if (this.ValidDept(row, (FS.HISFC.Models.Base.Employee)obj) == -1)
                {
                    this.fpSpread1.Focus();
                    return -1;
                }

                this.fpSpread1_Sheet1.SetValue(row, (int)cols.DoctName, obj.Name, false);
                this.fpSpread1_Sheet1.SetValue(row, (int)cols.DoctID, obj.ID, false);
                this.fpSpread1_Sheet1.Cells[row, (int)cols.DoctName].Text = obj.Name;
                this.fpSpread1_Sheet1.Cells[row, (int)cols.DoctID].Text = obj.ID;
                this.fpSpread1.Focus();
                this.visible(false);
            }
            else
            {
                string doctId = this.fpSpread1_Sheet1.GetText(row, (int)cols.DoctID);

                if (doctId == null || doctId == "")
                    this.fpSpread1_Sheet1.SetValue(row, (int)cols.DoctName, "", false);
            }

            return 0;
        }

        private int ValidDept(int CurrentRow, FS.HISFC.Models.Base.Employee p)
        {
            string deptID = this.fpSpread1_Sheet1.GetText(CurrentRow, (int)cols.DeptID);
            FS.HISFC.Models.Base.Department dept;

            if (deptID == null || deptID == "")//没有选择科室,直接默认医生所在的科室
            {
                dept = this.Mgr.GetDepartment(p.Dept.ID);
                if (dept == null) return 0;
                this.fpSpread1_Sheet1.SetValue(CurrentRow, (int)cols.DeptID, p.Dept.ID, false);
                this.fpSpread1_Sheet1.SetValue(CurrentRow, (int)cols.DeptName, dept.Name, false);
            }
            else
            {
                if (p.Dept.ID != deptID)
                {
                    dept = this.Mgr.GetDepartment(p.Dept.ID);

                    if (dept == null)
                    {
                        dept = new FS.HISFC.Models.Base.Department();
                        dept.Name = p.Dept.ID;
                    }

                    DialogResult dr = MessageBox.Show("选择医生的科室为:" + dept.Name +
                        ",与当前选择的科室不符,是否要继续?", "提示", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dr == DialogResult.No) return -1;
                }
            }

            return 0;
        }
        /// <summary>
        /// 选择医生类别
        /// </summary>
        /// <returns></returns>
        private int selectDoctType()
        {
            int row = this.fpSpread1_Sheet1.ActiveRowIndex;
            FS.FrameWork.Models.NeuObject obj;

            this.fpSpread1.StopCellEditing();

            if (this.groupBox1.Visible)
            {
                obj = new FS.FrameWork.Models.NeuObject();

                obj = this.lbDoctType.GetSelectedItem();
                if (obj == null) return -1;

                this.fpSpread1_Sheet1.SetValue(row, (int)cols.DoctType, obj.Name, false);
                //this.fpSpread1_Sheet1.SetTag(row,(int)cols.DoctType,obj.ID) ;

                this.visible(false);
            }

            return 0;
        }
        /// <summary>
        /// 选择停诊原因
        /// </summary>
        /// <returns></returns>
        private int selectStopRn()
        {
            int row = this.fpSpread1_Sheet1.ActiveRowIndex;
            FS.FrameWork.Models.NeuObject obj;

            this.fpSpread1.StopCellEditing();

            if (this.groupBox1.Visible)
            {
                obj = new FS.FrameWork.Models.NeuObject();
                obj = this.lbStopRn.GetSelectedItem();
                if (obj == null) return -1;

                this.fpSpread1_Sheet1.SetValue(row, (int)cols.StopReasonName, obj.Name, false);
                this.fpSpread1_Sheet1.SetValue(row, (int)cols.StopReasonID, obj.ID, false);
                this.visible(false);
            }
            else
            {
                string StopRnID = this.fpSpread1_Sheet1.GetText(row, (int)cols.StopReasonID);

                if (StopRnID == null || StopRnID == "")
                    this.fpSpread1_Sheet1.SetValue(row, (int)cols.StopReasonName, "", false);
            }

            return 0;
        }

        /// <summary>
        /// 选择挂号级别
        /// </summary>
        /// <returns></returns>
        private int selectRegLevel()
        {
            int row = this.fpSpread1_Sheet1.ActiveRowIndex;
            FS.FrameWork.Models.NeuObject obj;

            this.fpSpread1.StopCellEditing();

            if (this.groupBox1.Visible)
            {
                obj = new FS.FrameWork.Models.NeuObject();
                obj = this.lbRegLevel.GetSelectedItem();
                if (obj == null) return -1;

                this.fpSpread1_Sheet1.SetValue(row, (int)cols.RegLevlName, obj.Name, false);
                this.fpSpread1_Sheet1.SetValue(row, (int)cols.RegLevlCode, obj.ID, false);
                this.fpSpread1.Focus();
                this.visible(false);
            }
            else
            {
                string doctId = this.fpSpread1_Sheet1.GetText(row, (int)cols.RegLevlCode);

                if (doctId == null || doctId == "")
                    this.fpSpread1_Sheet1.SetValue(row, (int)cols.RegLevlName, "", false);
            }

            return 0;
        }

        /// <summary>
        /// 选择科室
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbDept_SelectItem(object sender, EventArgs e)
        {
            this.selectDept();
            this.visible(false);
            return;
        }

        /// <summary>
        /// 选择午别
        /// </summary>
        /// <param name="sender"></param>
        ///<param name="e"></param>
        private void lbNoon_SelectItem(object sender, EventArgs e)
        {
            this.selectNoon();
            this.visible(false);
            return;
        }

        /// <summary>
        /// 选择医生
        /// </summary>
        /// <param name="sender"></param>
        ///<param name="e"></param>
        private void lbDoct_SelectItem(object sender, EventArgs e)
        {
            this.selectDoct();
            this.visible(false);

            return;
        }
        /// <summary>
        /// 选择医生类别
        /// </summary>
        /// <param name="sender"></param>
        ///<param name="e"></param>
        private void lbDoctType_SelectItem(object sender, EventArgs e)
        {
            this.selectDoctType();
            this.visible(false);

            return;
        }

        private void lbStopRn_SelectItem(object sender, EventArgs e)
        {
            this.selectStopRn();
            this.visible(false);

            return;
        }
        private void lbRegLevel_SelectItem(object sender, EventArgs e)
        {
            this.selectRegLevel();
            this.visible(false);

            return;
        }
        #endregion

        private void EditingControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                FarPoint.Win.Spread.CellType.GeneralEditor t = fpSpread1.EditingControl as FarPoint.Win.Spread.CellType.GeneralEditor;
                if (t.SelectionStart == 0 && t.SelectionLength == 0)
                {
                    int row = 0, column = 0;
                    if (fpSpread1_Sheet1.ActiveColumnIndex == (int)cols.DeptName && fpSpread1_Sheet1.ActiveRowIndex != 0)
                    {
                        row = fpSpread1_Sheet1.ActiveRowIndex - 1;
                        column = (int)cols.StopReasonName;
                    }
                    else if (fpSpread1_Sheet1.ActiveColumnIndex != (int)cols.DeptName)
                    {
                        row = fpSpread1_Sheet1.ActiveRowIndex;
                        column = this.getPriorVisibleColumn(this.fpSpread1_Sheet1.ActiveColumnIndex);

                    }
                    if (column != -1)
                    {
                        //屏蔽压缩显示报错

                        if ((column == (int)cols.DeptName || column == (int)cols.DoctName ||
                            column == (int)cols.Noon) && dv[row].Row.RowState != DataRowState.Added)
                        {
                            return;
                        }

                        fpSpread1_Sheet1.SetActiveCell(row, column, true);
                    }
                }
            }
            if (e.KeyCode == Keys.Right)
            {
                FarPoint.Win.Spread.CellType.GeneralEditor t = fpSpread1.EditingControl as FarPoint.Win.Spread.CellType.GeneralEditor;

                if (t.Text == null || t.Text == "" || t.SelectionStart == t.Text.Length)
                {
                    int row = fpSpread1_Sheet1.RowCount - 1, column = fpSpread1_Sheet1.ColumnCount - 1;
                    if (fpSpread1_Sheet1.ActiveColumnIndex == column && fpSpread1_Sheet1.ActiveRowIndex != row)
                    {
                        row = fpSpread1_Sheet1.ActiveRowIndex + 1;

                        if (dv[row].Row.RowState == DataRowState.Added)
                        {
                            column = (int)cols.DeptName;
                        }
                        else
                        {
                            column = (int)cols.RegLmt;
                        }
                    }
                    else if (fpSpread1_Sheet1.ActiveColumnIndex != column)
                    {
                        row = fpSpread1_Sheet1.ActiveRowIndex;

                        column = this.getNextVisibleColumn(this.fpSpread1_Sheet1.ActiveColumnIndex);
                    }

                    if (column != -1)
                    {
                        //屏蔽压缩显示报错
                        if ((column == (int)cols.DeptName || column == (int)cols.DoctName ||
                            column == (int)cols.Noon) && dv[row].Row.RowState != DataRowState.Added)
                        {
                            return;
                        }

                        fpSpread1_Sheet1.SetActiveCell(row, column, true);
                    }
                }
            }
            if (e.KeyCode == Keys.Space)
            {
                fpSpread1_CellDoubleClick(sender, null);
            }
        }

        private void EditingControl_DoubleClick(object sender, EventArgs e)
        {
            int row = this.fpSpread1_Sheet1.ActiveRowIndex;

            if (row < 0) return;

            int col = this.fpSpread1_Sheet1.ActiveColumnIndex;

            if (col == (int)cols.BeginTime || col == (int)cols.EndTime)
            {
                //显示行状态
                this.showColor(row);
            }
            else if (col == (int)cols.Noon)
            {
                string deptId, doctId, noonID;

                deptId = this.fpSpread1_Sheet1.GetText(row, (int)cols.DeptID);
                doctId = this.fpSpread1_Sheet1.GetText(row, (int)cols.DoctID);
                noonID = this.fpSpread1_Sheet1.GetText(row, (int)cols.Noon);

                //将整个午别都置为相同状态
                for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                {
                    if (this.fpSpread1_Sheet1.GetText(i, (int)cols.DeptID) == deptId &&
                        this.fpSpread1_Sheet1.GetText(i, (int)cols.DoctID) == doctId &&
                        this.fpSpread1_Sheet1.GetText(i, (int)cols.Noon) == noonID)
                    {
                        //显示行状态
                        this.showColor(i);
                    }
                }
            }
        }
        /// <summary>
        /// 设置行显示颜色
        /// </summary>
        /// <param name="row"></param>
        private void showColor(int row)
        {
            //显示行状态
            if (this.fpSpread1_Sheet1.GetText(row, (int)cols.Valid).ToUpper() == "TRUE")
            {
                this.fpSpread1_Sheet1.SetValue(row, (int)cols.Valid, true, false);
                //this.fpSpread1_Sheet1.Cells[row, (int)cols.BeginTime].BackColor = Color.Red;
                //this.fpSpread1_Sheet1.Cells[row, (int)cols.EndTime].BackColor = Color.Red;
                this.fpSpread1_Sheet1.Cells[row, (int)cols.BeginTime].BackColor = SystemColors.Window;
                this.fpSpread1_Sheet1.Cells[row, (int)cols.EndTime].BackColor = SystemColors.Window;
            }
            else
            {
                this.fpSpread1_Sheet1.SetValue(row, (int)cols.Valid, false, false);
                //this.fpSpread1_Sheet1.Cells[row, (int)cols.BeginTime].BackColor = SystemColors.Window;
                //this.fpSpread1_Sheet1.Cells[row, (int)cols.EndTime].BackColor = SystemColors.Window;

                this.fpSpread1_Sheet1.Cells[row, (int)cols.BeginTime].BackColor = Color.MistyRose;
                this.fpSpread1_Sheet1.Cells[row, (int)cols.EndTime].BackColor = Color.MistyRose;
            }
        }
        /// <summary>
        /// 设置行显示颜色（全部）
        /// </summary>
        private void showColor()
        {
            int rowCount = this.fpSpread1_Sheet1.RowCount;
            if (rowCount > 0)
            {
                for (int i = 0; i < rowCount; i++)
                {
                    this.showColor(i);
                }
            }
        }

        private int getNextVisibleColumn(int col)
        {
            int count = this.fpSpread1_Sheet1.Columns.Count;

            while (col < count - 1)
            {
                col++;

                if (this.fpSpread1_Sheet1.Columns[col].Visible)
                {
                    return col;
                }
            }

            return -1;
        }

        private int getPriorVisibleColumn(int col)
        {
            while (col > 0)
            {
                col--;

                if (this.fpSpread1_Sheet1.Columns[col].Visible)
                {
                    return col;
                }
            }

            return -1;
        }

        #region 公有函数
        /// <summary>
        /// 按科室查询一日排班记录
        /// </summary>
        /// <param name="deptID"></param>
        public void Query(string deptID)
        {
            FS.HISFC.Models.Base.Employee curEmployee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            FS.HISFC.Models.Base.Department curDepartment = curEmployee.Dept as FS.HISFC.Models.Base.Department;
            
            this.al = this.TempletMgr.Query(this.SchemaType, week, deptID);
            if (al == null)
            {
                MessageBox.Show("查询排班模板信息出错!" + this.TempletMgr.Err, "提示");
                return;
            }

            ArrayList newAll = new ArrayList();
            foreach (FS.HISFC.Models.Registration.SchemaTemplet templet1 in al)
            {
                FS.HISFC.Models.Base.Department dept = FS.SOC.HISFC.BizProcess.Cache.Common.GetDept(templet1.Dept.ID);
                //schema.Templet.Dept.ID;
                if (dept.HospitalID == curDepartment.HospitalID)// {63B27717-4D42-46d6-9AE3-CE89853E9B5E
                {
                    newAll.Add(templet1);
                }
            }
            al = new ArrayList();
            al = newAll;

            dsItems.Rows.Clear();

            try
            {
                foreach (FS.HISFC.Models.Registration.SchemaTemplet templet in al)
                {

                    dsItems.Rows.Add(new object[]
					{
						templet.ID,						
						templet.Dept.ID,
						templet.Dept.Name,
						templet.Doct.ID,
						templet.Doct.Name,
						this.getTypeNameByID(templet.DoctType.ID),
						templet.RegLevel.ID,
						templet.RegLevel.Name,
						this.getNoonNameByID(templet.Noon.ID),
						templet.Begin,
						templet.End,
						templet.RegQuota,
						templet.TelQuota,
						templet.SpeQuota,
						templet.IsAppend,
						templet.IsValid,
						templet.Memo,
						templet.StopReason.ID,
						templet.StopReason.Name,
                        templet.Room.ID,
                        templet.Room.Name,
                        templet.Console.ID,
                        templet.Console.Name
                    });
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("查询排班信息生成DataSet时出错!" + e.Message, "提示");
                return;
            }
            dsItems.AcceptChanges();

            dv = dsItems.DefaultView;
            dv.AllowDelete = true;
            dv.AllowEdit = true;
            dv.AllowNew = true;
            //this.fpSpread1_Sheet1.Rows.Remove(0, this.fpSpread1_Sheet1.Rows.Count);
            this.fpSpread1_Sheet1.DataSource = dv;
            this.fpSpread1_Sheet1.DataMember = "Templet";

            this.SetFpFormat();

            //非中山
            if (!this.IsShowDoctType)
            {
                this.Span();
            }
            //
            if (this.fpSpread1_Sheet1.RowCount > 0)
            {
                for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                {
                    this.showColor(i);
                }
            }

        }

        /// <summary>
        /// 设置Farpoint显示格式
        /// </summary>
        private void SetFpFormat()
        {
            FarPoint.Win.Spread.CellType.NumberCellType numbCellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            numbCellType.DecimalPlaces = 0;
            numbCellType.MaximumValue = 9999;
            numbCellType.MinimumValue = 0;

            FarPoint.Win.Spread.CellType.DateTimeCellType dtCellType = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            dtCellType.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.UserDefined;
            dtCellType.UserDefinedFormat = "HH:mm";

            FarPoint.Win.Spread.CellType.TextCellType txtCellType = new FarPoint.Win.Spread.CellType.TextCellType();
            txtCellType.ReadOnly = true;

            //科室模板
            if (this.SchemaType == FS.HISFC.Models.Base.EnumSchemaType.Dept)
            {
                #region ""
                this.fpSpread1_Sheet1.Columns[(int)cols.ID].Visible = false;
                this.fpSpread1_Sheet1.Columns[(int)cols.DeptID].Visible = false;
                this.fpSpread1_Sheet1.Columns[(int)cols.DoctID].Visible = false;
                this.fpSpread1_Sheet1.Columns[(int)cols.DoctName].Visible = false;
                this.fpSpread1_Sheet1.Columns[(int)cols.DoctType].Visible = false;
                this.fpSpread1_Sheet1.Columns[(int)cols.RegLevlCode].Visible = false;
                //this.fpSpread1_Sheet1.Columns[(int)cols.RegLevlName].Visible = false;
                this.fpSpread1_Sheet1.Columns[(int)cols.SpeLmt].Visible = false;
                this.fpSpread1_Sheet1.Columns[(int)cols.StopReasonID].Visible = false;
                this.fpSpread1_Sheet1.Columns[(int)cols.StopReasonName].Visible = false;

                //没办法,要求压缩重复显示项，就不能再让人修改了，阉了它
                if (!this.IsShowDoctType)
                {
                    if (this.fpSpread1_Sheet1.RowCount > 0)
                    {
                        this.fpSpread1_Sheet1.Cells[0, (int)cols.DeptName, this.fpSpread1_Sheet1.RowCount - 1, (int)cols.DeptName].CellType = txtCellType;
                        this.fpSpread1_Sheet1.Cells[0, (int)cols.Noon, this.fpSpread1_Sheet1.RowCount - 1, (int)cols.Noon].CellType = txtCellType;
                    }
                }

                this.fpSpread1_Sheet1.Columns[(int)cols.RegLmt].CellType = numbCellType;
                this.fpSpread1_Sheet1.Columns[(int)cols.RegLmt].ForeColor = Color.Red;
                this.fpSpread1_Sheet1.Columns[(int)cols.RegLmt].Font = new Font("宋体", 9, FontStyle.Bold);
                this.fpSpread1_Sheet1.Columns[(int)cols.TelLmt].CellType = numbCellType;
                this.fpSpread1_Sheet1.Columns[(int)cols.TelLmt].ForeColor = Color.Blue;
                this.fpSpread1_Sheet1.Columns[(int)cols.TelLmt].Font = new Font("宋体", 9, FontStyle.Bold);

                this.fpSpread1_Sheet1.Columns[(int)cols.BeginTime].CellType = dtCellType;
                this.fpSpread1_Sheet1.Columns[(int)cols.EndTime].CellType = dtCellType;

                this.fpSpread1_Sheet1.Columns[(int)cols.DeptName].Width = 100F;
                this.fpSpread1_Sheet1.Columns[(int)cols.Noon].Width = 70F;
                this.fpSpread1_Sheet1.Columns[(int)cols.BeginTime].Width = 80F;
                this.fpSpread1_Sheet1.Columns[(int)cols.EndTime].Width = 80F;
                this.fpSpread1_Sheet1.Columns[(int)cols.RegLmt].Width = 75F;
                this.fpSpread1_Sheet1.Columns[(int)cols.TelLmt].Width = 75F;
                //this.fpSpread1_Sheet1.Columns[(int)cols.SpeLmt].Width = 50F;
                this.fpSpread1_Sheet1.Columns[(int)cols.IsAppend].Width = 40F;
                this.fpSpread1_Sheet1.Columns[(int)cols.Valid].Width = 40F;
                this.fpSpread1_Sheet1.Columns[(int)cols.Memo].Width = 160F;

                this.fpSpread1_Sheet1.Columns[(int)cols.RoomName].Width = 40F;
                this.fpSpread1_Sheet1.Columns[(int)cols.ConsoleName].Width = 40F;
                #endregion
            }
            else
            {
                #region ""
                this.fpSpread1_Sheet1.Columns[(int)cols.ID].Visible = false;
                this.fpSpread1_Sheet1.Columns[(int)cols.DeptID].Visible = false;
                this.fpSpread1_Sheet1.Columns[(int)cols.DoctID].Visible = false;
                this.fpSpread1_Sheet1.Columns[(int)cols.RegLevlCode].Visible = false;
                this.fpSpread1_Sheet1.Columns[(int)cols.StopReasonID].Visible = false;

                if (this.IsShowDoctType)
                {
                    this.fpSpread1_Sheet1.Columns[(int)cols.DoctType].Visible = true;
                }
                else
                {
                    this.fpSpread1_Sheet1.Columns[(int)cols.DoctType].Visible = false;
                }

                if (this.IsShowRegLevel)
                {
                    this.fpSpread1_Sheet1.Columns[(int)cols.RegLevlName].Visible = true;
                }
                else
                {
                    this.fpSpread1_Sheet1.Columns[(int)cols.RegLevlName].Visible = false;
                }
                //没办法,要求压缩重复显示项，就不能再让人修改了，阉了它
                if (!this.IsShowDoctType)
                {
                    if (this.fpSpread1_Sheet1.RowCount > 0)
                    {
                        this.fpSpread1_Sheet1.Cells[0, (int)cols.DeptName, this.fpSpread1_Sheet1.RowCount - 1, (int)cols.DeptName].CellType = txtCellType;
                        this.fpSpread1_Sheet1.Cells[0, (int)cols.DoctName, this.fpSpread1_Sheet1.RowCount - 1, (int)cols.DoctName].CellType = txtCellType;
                        this.fpSpread1_Sheet1.Cells[0, (int)cols.Noon, this.fpSpread1_Sheet1.RowCount - 1, (int)cols.Noon].CellType = txtCellType;
                        this.fpSpread1_Sheet1.Cells[0, (int)cols.DoctType, this.fpSpread1_Sheet1.RowCount - 1, (int)cols.DoctType].CellType = txtCellType;
                    }
                }

                this.fpSpread1_Sheet1.Columns[(int)cols.RegLmt].CellType = numbCellType;
                this.fpSpread1_Sheet1.Columns[(int)cols.RegLmt].ForeColor = Color.Red;
                this.fpSpread1_Sheet1.Columns[(int)cols.RegLmt].Font = new Font("宋体", 9, FontStyle.Bold);
                this.fpSpread1_Sheet1.Columns[(int)cols.TelLmt].CellType = numbCellType;
                this.fpSpread1_Sheet1.Columns[(int)cols.TelLmt].ForeColor = Color.Blue;
                this.fpSpread1_Sheet1.Columns[(int)cols.TelLmt].Font = new Font("宋体", 9, FontStyle.Bold);
                this.fpSpread1_Sheet1.Columns[(int)cols.SpeLmt].CellType = numbCellType;
                this.fpSpread1_Sheet1.Columns[(int)cols.SpeLmt].ForeColor = Color.Fuchsia;
                this.fpSpread1_Sheet1.Columns[(int)cols.SpeLmt].Font = new Font("宋体", 9, FontStyle.Bold);
                this.fpSpread1_Sheet1.Columns[(int)cols.BeginTime].CellType = dtCellType;
                this.fpSpread1_Sheet1.Columns[(int)cols.EndTime].CellType = dtCellType;

                this.fpSpread1_Sheet1.Columns[(int)cols.DeptName].Width = 90F;
                this.fpSpread1_Sheet1.Columns[(int)cols.DoctName].Width = 60F;
                this.fpSpread1_Sheet1.Columns[(int)cols.DoctType].Width = 60F;
                this.fpSpread1_Sheet1.Columns[(int)cols.RegLevlName].Width = 60F;
                this.fpSpread1_Sheet1.Columns[(int)cols.Noon].Width = 40F;
                this.fpSpread1_Sheet1.Columns[(int)cols.BeginTime].Width = 50F;
                this.fpSpread1_Sheet1.Columns[(int)cols.EndTime].Width = 50F;
                this.fpSpread1_Sheet1.Columns[(int)cols.RegLmt].Width = 40F;
                this.fpSpread1_Sheet1.Columns[(int)cols.TelLmt].Width = 40F;
                this.fpSpread1_Sheet1.Columns[(int)cols.SpeLmt].Width = 40F;
                this.fpSpread1_Sheet1.Columns[(int)cols.IsAppend].Width = 35F;
                this.fpSpread1_Sheet1.Columns[(int)cols.Valid].Width = 35F;
                this.fpSpread1_Sheet1.Columns[(int)cols.Memo].Width = 110F;
                this.fpSpread1_Sheet1.Columns[(int)cols.StopReasonID].Width = 80F;

                this.fpSpread1_Sheet1.Columns[(int)cols.RoomName].Width = 40F;
                this.fpSpread1_Sheet1.Columns[(int)cols.ConsoleName].Width = 40F;

                #endregion
            }
        }
        /// <summary>
        /// 抑止重复显示
        /// </summary>
        private void Span()
        {
            ///
            int colLastDept = 0;
            int colLastDoct = 0;
            int colLastNoon = 0;
            int rowCnt = this.fpSpread1_Sheet1.RowCount;

            FarPoint.Win.Spread.CellType.TextCellType txtCellType = new FarPoint.Win.Spread.CellType.TextCellType();
            txtCellType.ReadOnly = true;

            for (int i = 0; i < rowCnt; i++)
            {
                //显示行状态
                //if (this.fpSpread1_Sheet1.GetText(i, (int)cols.Valid).ToUpper() == "FALSE")
                //{
                //    this.fpSpread1_Sheet1.Cells[i, (int)cols.BeginTime].BackColor = Color.Red;
                //    this.fpSpread1_Sheet1.Cells[i, (int)cols.EndTime].BackColor = Color.Red;
                //}
                this.showColor(i);

                if (i > 0 && this.fpSpread1_Sheet1.GetText(i, (int)cols.DeptName) != this.fpSpread1_Sheet1.GetText(i - 1, (int)cols.DeptName))
                {
                    if (i - colLastDept > 1)
                    {
                        this.fpSpread1_Sheet1.Models.Span.Add(colLastDept, (int)cols.DeptName, i - colLastDept, 1);
                    }

                    colLastDept = i;
                }

                //最后一行处理
                if (i > 0 && i == rowCnt - 1 && this.fpSpread1_Sheet1.GetText(i, (int)cols.DeptName) == this.fpSpread1_Sheet1.GetText(i - 1, (int)cols.DeptName))
                {
                    this.fpSpread1_Sheet1.Models.Span.Add(colLastDept, (int)cols.DeptName, i - colLastDept + 1, 1);
                }

                ///医生
                ///
                if (this.SchemaType == FS.HISFC.Models.Base.EnumSchemaType.Doct &&
                    i > 0 &&
                    this.fpSpread1_Sheet1.GetText(i, (int)cols.DoctName) != this.fpSpread1_Sheet1.GetText(i - 1, (int)cols.DoctName))
                {
                    if (i - colLastDoct > 1)
                    {
                        this.fpSpread1_Sheet1.Models.Span.Add(colLastDoct, (int)cols.DoctName, i - colLastDoct, 1);
                    }
                    colLastDoct = i;
                }

                //最后一行
                if (this.SchemaType == FS.HISFC.Models.Base.EnumSchemaType.Doct &&
                    i > 0 &&
                    i == rowCnt - 1 && this.fpSpread1_Sheet1.GetText(i, (int)cols.DoctName) == this.fpSpread1_Sheet1.GetText(i - 1, (int)cols.DoctName))
                {
                    this.fpSpread1_Sheet1.Models.Span.Add(colLastDoct, (int)cols.DoctName, i - colLastDoct + 1, 1);
                }

                ///午别
                ///
                if (i > 0 &&
                    (this.fpSpread1_Sheet1.GetText(i, (int)cols.Noon) != this.fpSpread1_Sheet1.GetText(i - 1, (int)cols.Noon) ||
                    this.fpSpread1_Sheet1.GetText(i, (int)cols.DeptName) != this.fpSpread1_Sheet1.GetText(i - 1, (int)cols.DeptName) ||
                    this.fpSpread1_Sheet1.GetText(i, (int)cols.DoctName) != this.fpSpread1_Sheet1.GetText(i - 1, (int)cols.DoctName)))
                {
                    if (i - colLastNoon > 1)
                    {
                        this.fpSpread1_Sheet1.Models.Span.Add(colLastNoon, (int)cols.Noon, i - colLastNoon, 1);
                    }
                    colLastNoon = i;
                }
                //最后一行
                if (i > 0 && i == rowCnt - 1 &&
                    (this.fpSpread1_Sheet1.GetText(i, (int)cols.Noon) == this.fpSpread1_Sheet1.GetText(i - 1, (int)cols.Noon) ||
                    this.fpSpread1_Sheet1.GetText(i, (int)cols.DeptName) == this.fpSpread1_Sheet1.GetText(i - 1, (int)cols.DeptName) ||
                    this.fpSpread1_Sheet1.GetText(i, (int)cols.DoctName) == this.fpSpread1_Sheet1.GetText(i - 1, (int)cols.DoctName)))
                {
                    this.fpSpread1_Sheet1.Models.Span.Add(colLastNoon, (int)cols.Noon, i - colLastNoon + 1, 1);
                }

            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Add()
        {
            //			DataRowView dr = dsItems.Tables[0].DefaultView.AddNew();
            //
            //			dr["Valid"] = true;
            //			dr["OperID"] = var.User.ID;
            //			dr["OperDate"] = this.templetMgr.GetDateTimeFromSysDateTime();
            //MessageBox.Show(this.fpSpread1_Sheet1.RowCount.ToString());

            this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.RowCount, 1);

            this.fpSpread1_Sheet1.ActiveRowIndex = this.fpSpread1_Sheet1.RowCount - 1;
            int row = this.fpSpread1_Sheet1.ActiveRowIndex;

            DateTime current = this.TempletMgr.GetDateTimeFromSysDateTime().Date;

            this.fpSpread1_Sheet1.SetValue(row, (int)cols.ID, TempletMgr.GetSequence("Registration.DeptSchema.ID"), false);

            //默认同上一个科室
            if (row > 0)
            {
                this.fpSpread1_Sheet1.SetValue(row, (int)cols.DeptID,
                    this.fpSpread1_Sheet1.GetText(row - 1, (int)cols.DeptID), false);
                this.fpSpread1_Sheet1.SetValue(row, (int)cols.DeptName,
                    this.fpSpread1_Sheet1.GetText(row - 1, (int)cols.DeptName), false);

                if (this.SchemaType == FS.HISFC.Models.Base.EnumSchemaType.Doct)
                {
                    this.fpSpread1_Sheet1.SetValue(row, (int)cols.DoctID,
                        this.fpSpread1_Sheet1.GetText(row - 1, (int)cols.DoctID), false);
                    this.fpSpread1_Sheet1.SetValue(row, (int)cols.DoctName,
                        this.fpSpread1_Sheet1.GetText(row - 1, (int)cols.DoctName), false);
                    this.fpSpread1_Sheet1.SetValue(row, (int)cols.DoctType,
                        this.fpSpread1_Sheet1.GetText(row - 1, (int)cols.DoctType), false);
                    this.fpSpread1_Sheet1.SetValue(row, (int)cols.RegLevlCode,
                        this.fpSpread1_Sheet1.GetText(row - 1, (int)cols.RegLevlCode), false);
                    this.fpSpread1_Sheet1.SetValue(row, (int)cols.RegLevlName,
                        this.fpSpread1_Sheet1.GetText(row - 1, (int)cols.RegLevlName), false);
                }
            }
            else//row == 0 
            {
                this.fpSpread1_Sheet1.SetValue(row, (int)cols.DeptID, dept.ID, false);
                this.fpSpread1_Sheet1.SetValue(row, (int)cols.DeptName, dept.Name, false);
            }

            this.fpSpread1_Sheet1.SetValue(row, (int)cols.IsAppend, false, false);
            this.fpSpread1_Sheet1.SetValue(row, (int)cols.Valid, true, false);
            this.fpSpread1_Sheet1.SetValue(row, (int)cols.RegLmt, 0, false);
            this.fpSpread1_Sheet1.SetValue(row, (int)cols.TelLmt, 0, false);
            this.fpSpread1_Sheet1.SetValue(row, (int)cols.SpeLmt, 0, false);
            this.fpSpread1_Sheet1.SetValue(row, (int)cols.BeginTime, current, false);
            this.fpSpread1_Sheet1.SetValue(row, (int)cols.EndTime, current, false);

            if (this.SchemaType == FS.HISFC.Models.Base.EnumSchemaType.Dept)
            {
                this.fpSpread1_Sheet1.SetValue(row, (int)cols.DoctID, "None", false);
            }

            this.fpSpread1.Focus();

            string deptId = "";

            deptId = this.fpSpread1_Sheet1.GetText(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.DeptID);


            if (deptId == null || deptId == "")
            {
                this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.DeptName, false);
            }
            else
            {
                if (this.SchemaType == FS.HISFC.Models.Base.EnumSchemaType.Dept)
                {
                    this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.Noon, false);
                }
                else
                {
                    this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.DoctName, false);
                }
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        public void Del()
        {
            int row = this.fpSpread1_Sheet1.ActiveRowIndex;
            if (row < 0 || this.fpSpread1_Sheet1.RowCount == 0) return;

            if (MessageBox.Show("是否删除该条排班?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                this.fpSpread1.Focus();
                return;
            }

            this.fpSpread1.StopCellEditing();

            this.fpSpread1_Sheet1.Rows.Remove(row, 1);

            this.fpSpread1.Focus();
        }
        /// <summary>
        /// 删除全部
        /// </summary>
        public void DelAll()
        {
            if (this.fpSpread1_Sheet1.RowCount == 0) return;

            if (MessageBox.Show("是否删除全部排班?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                this.fpSpread1.Focus();
                return;
            }

            this.fpSpread1_Sheet1.Rows.Remove(0, this.fpSpread1_Sheet1.RowCount);

            this.fpSpread1.Focus();
        }
        /// <summary>
        /// 保存
        /// </summary>
        public int Save()
        {
            this.fpSpread1.StopCellEditing();

            if (fpSpread1_Sheet1.RowCount > 0)
            {
                dsItems.Rows[fpSpread1_Sheet1.ActiveRowIndex].EndEdit();
            }
            //{92A54BE2-EFAB-4a69-AA52-618B6250A326}
            foreach (DataRow dr in dsItems.Rows)
            {
                dr.EndEdit();

            }
            //增加
            DataTable dtAdd = dsItems.GetChanges(DataRowState.Added);
            //修改
            DataTable dtModify = dsItems.GetChanges(DataRowState.Modified);
            //删除
            DataTable dtDel = dsItems.GetChanges(DataRowState.Deleted);

            //验证
            if (Valid(dtAdd) == -1) return -1;
            if (Valid(dtModify) == -1) return -1;
            //转为实体集合
            ArrayList alAdd = this.GetChanges(dtAdd);
            if (alAdd == null) return -1;
            ArrayList alModify = this.GetChanges(dtModify);
            if (alModify == null) return -1;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //SQLCA.BeginTransaction();

            this.TempletMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            string rtnText = "";
            if (dtDel != null)
            {
                dtDel.RejectChanges();

                if (this.SaveDel(this.TempletMgr, dtDel, ref rtnText) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(rtnText, "提示");
                    return -1;
                }
            }

            if (this.SaveAdd(this.TempletMgr, alAdd, ref rtnText) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(rtnText, "提示");
                return -1;
            }

            if (this.SaveModify(this.TempletMgr, alModify, ref rtnText) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(rtnText, "提示");
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            dsItems.AcceptChanges();
            this.SetFpFormat();
            this.showColor();
            return 0;
        }
        /// <summary>
        /// 是否修改数据？
        /// </summary>
        /// <returns></returns>
        public bool IsChange()
        {
            this.fpSpread1.StopCellEditing();

            if (fpSpread1_Sheet1.RowCount > 0)
            {
                dsItems.Rows[fpSpread1_Sheet1.ActiveRowIndex].EndEdit();
            }

            DataTable dt = dsItems.GetChanges();

            if (dt == null || dt.Rows.Count == 0)
            {
                return false;
            }

            return true;
        }
        /// <summary>
        /// 保存增加的记录
        /// </summary>
        /// <param name="templetMgr"></param>
        /// <param name="alAdd"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int SaveAdd(FS.HISFC.BizLogic.Registration.SchemaTemplet templetMgr, ArrayList alAdd, ref string Err)
        {
            try
            {
                foreach (FS.HISFC.Models.Registration.SchemaTemplet templet in alAdd)
                {
                    if (templetMgr.Insert(templet) == -1)
                    {
                        Err = templetMgr.Err;
                        return -1;
                    }
                }
            }
            catch (Exception e)
            {
                Err = e.Message;
                return -1;
            }

            return 0;
        }
        /// <summary>
        /// 保存修改记录
        /// </summary>
        /// <param name="templetMgr"></param>
        /// <param name="alModify"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int SaveModify(FS.HISFC.BizLogic.Registration.SchemaTemplet templetMgr, ArrayList alModify, ref string Err)
        {
            try
            {
                foreach (FS.HISFC.Models.Registration.SchemaTemplet templet in alModify)
                {
                    int returnValue = 0;

                    returnValue = templetMgr.Delete(templet.ID);

                    if (returnValue == -1)
                    {
                        Err = templetMgr.Err;
                        return -1;
                    }

                    if (templetMgr.Insert(templet) == -1)
                    {
                        Err = templetMgr.Err;
                        return -1;
                    }
                }
            }
            catch (Exception e)
            {
                Err = e.Message;
                return -1;
            }

            return 0;
        }
        /// <summary>
        /// 保存删除记录
        /// </summary>
        /// <param name="templetMgr"></param>
        /// <param name="dvDel"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int SaveDel(FS.HISFC.BizLogic.Registration.SchemaTemplet templetMgr, DataTable dvDel, ref string Err)
        {
            try
            {
                foreach (DataRow row in dvDel.Rows)
                {
                    if (templetMgr.Delete(row["ID"].ToString()) == -1)
                    {
                        Err = templetMgr.Err;
                        return -1;
                    }
                }
            }
            catch (Exception e)
            {
                Err = e.Message;
                return -1;
            }

            return 0;
        }
        /// <summary>
        /// 将表中数据转换为实体集合
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private ArrayList GetChanges(DataTable dt)
        {
            this.al = new ArrayList();

            if (dt != null)
            {
                try
                {
                    DateTime current = DateTime.Parse("2000-01-01 00:00:00");
                    DateTime bookingTime;

                    foreach (DataRow row in dt.Rows)
                    {
                        FS.HISFC.Models.Registration.SchemaTemplet templet = new FS.HISFC.Models.Registration.SchemaTemplet();

                        templet.ID = row["ID"].ToString();
                        templet.Week = this.week;
                        templet.EnumSchemaType = this.SchemaType;
                        templet.Dept.ID = row["DeptID"].ToString();
                        templet.Dept.Name = row["DeptName"].ToString();
                        templet.Doct.ID = row["DoctID"].ToString();
                        templet.Doct.Name = row["DoctName"].ToString();
                        templet.DoctType.ID = this.getTypeIDByName(row["DoctType"].ToString());
                        templet.RegLevel.ID = row["RegLevlCode"].ToString();
                        templet.RegLevel.Name = row["RegLevlName"].ToString();
                        templet.Noon.ID = this.getNoonIDByName(row["Noon"].ToString());
                        templet.IsAppend = FS.FrameWork.Function.NConvert.ToBoolean(row["IsAppend"]);

                        //加号开始、结束时间为午别结束时间
                        if (templet.IsAppend)
                        {
                            templet.Begin = DateTime.Parse(current.ToString("yyyy-MM-dd") + " "
                                + this.getNoonEndDate(templet.Noon.ID).ToString("HH:mm:ss"));
                            templet.End = templet.Begin;
                        }
                        else
                        {
                            bookingTime = DateTime.Parse(row["BeginTime"].ToString());
                            templet.Begin = DateTime.Parse(current.ToString("yyyy-MM-dd") + " " + bookingTime.Hour +
                                ":" + bookingTime.Minute + ":00");

                            bookingTime = DateTime.Parse(row["EndTime"].ToString());
                            templet.End = DateTime.Parse(current.ToString("yyyy-MM-dd") + " " + bookingTime.Hour +
                                ":" + bookingTime.Minute + ":00");
                        }

                        templet.RegQuota = decimal.Parse(row["RegLmt"].ToString());
                        templet.TelQuota = decimal.Parse(row["TelLmt"].ToString());
                        templet.SpeQuota = decimal.Parse(row["SpeLmt"].ToString());

                        //不是加号必须要输入一个限额,不允许都为空
                        if (templet.RegQuota == 0 && templet.TelQuota == 0 && templet.SpeQuota == 0 && !templet.IsAppend)
                        {
                            MessageBox.Show("挂号限额不能全部为零,请重新输入!", "提示");
                            return null;
                        }

                        templet.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(row["Valid"]);
                        templet.Memo = row["Memo"].ToString();
                        templet.Oper.ID = this.TempletMgr.Operator.ID;
                        templet.Oper.OperTime = DateTime.Now;
                        templet.StopReason.ID = row["StopReasonID"].ToString();
                        templet.StopReason.Name = row["StopReasonName"].ToString();

                        #region 处理诊台诊室

                        if (row["RoomID"].ToString() != ""
                            && row["RoomName"].ToString() != "")
                        {
                            templet.Room.ID = row["RoomID"].ToString();
                            //if (string.IsNullOrEmpty(templet.Room.ID))
                            //{
                            //    MessageBox.Show(FS.FrameWork.Management.Language.Msg("诊室编码为空，请重新选择！\r\n\r\n不要直接复制名称！"), "提示");
                            //    return null;
                            //}
                            templet.Room.Name = row["RoomName"].ToString();
                        }
                        if (!string.IsNullOrEmpty(row["ConsoleID"].ToString())
                            && !string.IsNullOrEmpty(row["ConsoleName"].ToString()))
                        {
                            templet.Console.ID = row["ConsoleID"].ToString();
                            templet.Console.Name = row["ConsoleName"].ToString();
                        }

                        if (string.IsNullOrEmpty(templet.Room.ID))
                        {
                        }
                        else
                        {
                            string sql = @"select count(*) from fin_opr_schematemplet a
                                        where a.id!='{0}'
                                            and a.valid_flag='1'
                                        and a.week='{1}'
                                        and a.noon_code='{2}'
                                        and a.room_id='{3}'
                                        and a.to_char(a.begin_time,'hh24:mi:ss')='{4}'";

                            sql = string.Format(sql, templet.ID, ((Int32)templet.Week).ToString(), templet.Noon.ID, templet.Room.ID, templet.Begin.ToString("HH:mm:ss"));
                            string rev = TempletMgr.ExecSqlReturnOne(sql);
                            int count = FS.FrameWork.Function.NConvert.ToInt32(rev);
                            if (count > 0)
                            {
                                sql = @"select a.dept_name||'  '||a.room_name 
                                            from fin_opr_schematemplet a
                                            where a.id!='{0}'
                                            and a.valid_flag='1'
                                            and a.week='{1}'
                                            and a.noon_code='{2}'
                                            and a.room_id='{3}'
                                            and a.to_char(a.begin_time,'hh24:mi:ss')='{4}'";
                                sql = string.Format(sql, templet.ID, ((Int32)templet.Week).ToString(), templet.Noon.ID, templet.Room.ID, templet.Begin.ToString("HH:mm:ss"));
                                rev = TempletMgr.ExecSqlReturnOne(sql);

                                MessageBox.Show(FS.FrameWork.Management.Language.Msg(templet.Room.Name + "已在" + rev + "维护，不能重复维护！"), "提示");
                                return null;
                            }

                            if (string.IsNullOrEmpty(templet.Room.ID))
                            {
                                MessageBox.Show(FS.FrameWork.Management.Language.Msg("诊台编码为空，请重新选择！\r\n\r\n不要直接复制名称！"), "提示");
                                return null;
                            }
                        }

                        #endregion

                        this.al.Add(templet);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("生成实体集合时出错!" + e.Message, "提示");
                    return null;
                }
            }

            return al;
        }
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private int Valid(DataTable dt)
        {
            if (dt != null)
            {
                Dictionary<string, DataRow> hsRoomID = new Dictionary<string, DataRow>();
                Dictionary<string, DataRow> hsConsoleID = new Dictionary<string, DataRow>();    

                foreach (DataRow row in dt.Rows)
                {
                    string dept = "";
                    string doct = "";


                    if (row["DeptID"].ToString() == null || row["DeptID"].ToString() == "")
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("出诊科室不能为空"), "提示");
                        return -1;
                    }
                    dept = row["DeptName"].ToString();

                    if (this.SchemaType == FS.HISFC.Models.Base.EnumSchemaType.Doct)
                    {
                        if (row["DoctID"].ToString() == null || row["DoctID"].ToString() == "")
                        {
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("出诊专家不能为空"), "提示");
                            return -1;
                        }

                        if (this.IsShowRegLevel && (row["RegLevlCode"] == null || row["RegLevlCode"].ToString() == ""))
                        {
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("挂号级别不能为空"), "提示");
                            return -1;
                        }
                        doct = row["DoctName"].ToString();
                    }
                    if (row["BeginTime"] == null || row["BeginTime"].ToString().Trim() == "" ||
                        row["EndTime"] == null || row["EndTime"].ToString().Trim() == "")
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("请输入预约时间段"), "提示");
                        return -1;
                    }
                    if (DateTime.Parse(row["BeginTime"].ToString()).TimeOfDay > DateTime.Parse(row["EndTime"].ToString()).TimeOfDay)
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("开始时间不能大于结束时间"), "提示");
                        return -1;
                    }
                    if (row[14].ToString() != "True")
                    {
                        if (DateTime.Parse(row["BeginTime"].ToString()).TimeOfDay == DateTime.Parse(row["EndTime"].ToString()).TimeOfDay)
                        {
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("开始时间不能大于结束时间"), "提示");
                            return -1;
                        }
                    }

                    string noon = this.getNoonIDByName(row["Noon"].ToString());
                    if (noon == "")
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("出诊午别不能为空"), "提示");
                        return -1;
                    }
                    if (row["RegLmt"].ToString() == null || row["RegLmt"].ToString() == "")
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("挂号限额必须录入"), "提示");
                        return -1;
                    }
                    if (row["TelLmt"].ToString() == null || row["TelLmt"].ToString() == "")
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("预约限额必须录入"), "提示");
                        return -1;
                    }
                    if (row["SpeLmt"].ToString() == null || row["SpeLmt"].ToString() == "")
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("特诊限额必须录入"), "提示");
                        return -1;
                    }
                    if (FS.FrameWork.Public.String.ValidMaxLengh(row["Memo"].ToString(), 50) == false)
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("备注不能超过25个汉字"), "提示");
                        return -1;
                    }
                    row["Memo"] = FS.FrameWork.Public.String.TakeOffSpecialChar(row["Memo"].ToString(), "#", "%", "[", "'", "]", ",", "$", "(", ")", "|", "\'", "\"", "\\", "*", "^", "@", "!");

                    #region 处理诊室诊台信息

                    string keyRoom = "";
                    if (row["RoomID"].ToString() == "" || row["RoomName"].ToString() == "")
                    {
                        if (MessageBox.Show("存在诊室为空的情况，是否确认是临时排班？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("请选择诊室!"), "提示");
                            return -1;
                        }

                        row["RoomID"] = "";
                        row["RoomName"] = "";
                        row["ConsoleID"] = "";      //诊室为空的时候，诊台也为空
                        row["ConsoleName"] = "";
                    }
                    else
                    {
                        //星期+午别+诊室+有效性+开始时间//+结束时间
                        keyRoom = ((Int32)week).ToString() + row["Noon"].ToString() + row["RoomID"].ToString() + row["Valid"].ToString() + row["BeginTime"].ToString();
                        if (!hsRoomID.ContainsKey(keyRoom))
                        {
                            hsRoomID.Add(keyRoom, row);
                        }
                        else
                        {
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg(dept + "  " + doct + "不能保存！\r\n\r\n已存在排班到【" + hsRoomID[keyRoom]["RoomName"].ToString() + "】的记录，不允许重复排班！"), "提示");
                            return -1;
                        }
                    }

                    //诊室不为空的时候，才判断诊台
                    if (!string.IsNullOrEmpty(row["RoomID"].ToString()) && !string.IsNullOrEmpty(row["RoomName"].ToString()))
                    {

                        if (row["ConsoleID"].ToString() == "" || row["ConsoleName"].ToString() == "")
                        {
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("请选择诊台"), "提示");
                            return -1;
                        }
                        else
                        {
                            string keyConsole = ((Int32)week).ToString() + row["Noon"].ToString() + row["RoomID"].ToString() + row["ConsoleID"].ToString() + row["Valid"].ToString() + row["BeginTime"].ToString();
                            if (row["ConsoleID"].ToString() == null || row["ConsoleName"].ToString() == "")
                            {
                                MessageBox.Show(FS.FrameWork.Management.Language.Msg("请选择诊台"), "提示");
                            }

                            if (!hsConsoleID.ContainsKey(keyConsole))
                            {
                                hsConsoleID.Add(keyConsole, row);
                            }
                            else
                            {
                                MessageBox.Show(FS.FrameWork.Management.Language.Msg(dept + "  " + doct + "不能保存！\r\n\r\n已存在排班到【" + hsRoomID[keyRoom]["RoomName"].ToString() + hsConsoleID[keyConsole]["ConsoleName"].ToString() + "】的记录，不允许重复排班！"), "提示");
                                return -1;
                            }
                        }

                    }

                    #endregion
                }
                if (this.valid() < 0) return -1;

            }

            return 0;
        }
        /// <summary>
        /// 核对有没有重复时间段
        /// </summary>
        /// <returns></returns>
        private int valid()
        {

            if (this.fpSpread1_Sheet1.RowCount <= 0) return -1;
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount - 1; i++)
            {


                if (this.fpSpread1_Sheet1.GetValue(i, 14).ToString() != "True")
                {
                    DateTime beginDTi = FS.FrameWork.Function.NConvert.ToDateTime(this.fpSpread1_Sheet1.GetValue(i, 9));
                    DateTime endDTi = FS.FrameWork.Function.NConvert.ToDateTime(this.fpSpread1_Sheet1.GetValue(i, 10));
                    for (int j = i + 1; j < this.fpSpread1_Sheet1.RowCount; j++)
                    {
                        if (this.fpSpread1_Sheet1.GetValue(j, 14).ToString() != "True")
                        {
                            DateTime beginDTj = FS.FrameWork.Function.NConvert.ToDateTime(this.fpSpread1_Sheet1.GetValue(j, 9));
                            DateTime endDTj = FS.FrameWork.Function.NConvert.ToDateTime(this.fpSpread1_Sheet1.GetValue(j, 10));
                            if ((this.fpSpread1_Sheet1.GetValue(i, 1).ToString() == this.fpSpread1_Sheet1.GetValue(j, 1).ToString()) && (this.fpSpread1_Sheet1.GetValue(i, 3).ToString() == this.fpSpread1_Sheet1.GetValue(j, 3).ToString()) && ((beginDTj.TimeOfDay <= beginDTi.TimeOfDay && endDTj.TimeOfDay > beginDTi.TimeOfDay) || (beginDTj.TimeOfDay < endDTi.TimeOfDay && endDTj.TimeOfDay >= endDTi.TimeOfDay) || (beginDTj.TimeOfDay >= beginDTi.TimeOfDay && endDTj.TimeOfDay <= endDTi.TimeOfDay) || (beginDTj.TimeOfDay <= beginDTi.TimeOfDay && endDTj.TimeOfDay >= endDTi.TimeOfDay)))
                            {
                                MessageBox.Show("第" + (j + 1).ToString() + "行排班时间有误");
                                return -1;
                            }
                        }
                    }
                }

            }
            return 0;
        }

        #endregion
    }
}
