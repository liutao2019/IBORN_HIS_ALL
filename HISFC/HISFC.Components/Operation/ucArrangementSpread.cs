using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using FS.HISFC.Models.Operation;
using FS.FrameWork.Models;
using FS.FrameWork;

namespace FS.HISFC.Components.Operation
{
    public delegate void ApplicationSelectedEventHandler(object sender, OperationAppllication e);
    /// <summary>
    /// [功能描述: 手术安排设置信息]<br></br>
    /// [创 建 者: 王铁全]<br></br>
    /// [创建时间: 2006-12-04]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucArrangementSpread : UserControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucArrangementSpread()
        {
            InitializeComponent();
            if (!Environment.DesignMode)
            {
                this.Init();
                this.InitNurseListBox();
                this.InitRoomListBox();
                this.InitTableListBox();
            }
        }

        #region 字段

        public event ApplicationSelectedEventHandler applictionSelected;
        FS.HISFC.BizProcess.Integrate.Manager deptManager = new FS.HISFC.BizProcess.Integrate.Manager();

        FS.FrameWork.Management.DataBaseManger dataManager = new FS.FrameWork.Management.DataBaseManger();

        /// <summary>
        /// 手术列表
        /// </summary>
        private ArrayList alApplys = new ArrayList();
        private ArrayList alRooms = new ArrayList();
        //private ArrayList alAnes;   //麻醉方式列表

        private FS.FrameWork.WinForms.Controls.NeuListBoxPopup lbNurse = new FS.FrameWork.WinForms.Controls.NeuListBoxPopup();
        private FS.FrameWork.WinForms.Controls.NeuListBoxPopup lbRoom = new FS.FrameWork.WinForms.Controls.NeuListBoxPopup();
        private FS.FrameWork.WinForms.Controls.NeuListBoxPopup lbTable = new FS.FrameWork.WinForms.Controls.NeuListBoxPopup();
        //{A4764DE2-685E-4bb4-8B92-A77214904644}
        private FS.FrameWork.WinForms.Controls.NeuListBoxPopup lbAnaetype = new FS.FrameWork.WinForms.Controls.NeuListBoxPopup();
        FS.FrameWork.Public.ObjectHelper anaeTypeHelper = new FS.FrameWork.Public.ObjectHelper();
        private FS.HISFC.BizProcess.Interface.Operation.IArrangePrint arrangePrint;
        private DateTime date;

        private EnumFilter filter = EnumFilter.All;
        private int rowindex;

        //{B9DDCC10-3380-4212-99E5-BB909643F11B}
        FS.FrameWork.Public.ObjectHelper anneObjectHelper = new FS.FrameWork.Public.ObjectHelper();

        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// 是否按照手术间排序
        /// </summary>
        bool isSortByRoom = false;

        string SortFlag = string.Empty;

        private FS.FrameWork.Public.ObjectHelper OperationOrderHelper = new FS.FrameWork.Public.ObjectHelper();
        #endregion

        #region 属性
        public DateTime Date
        {
            set
            {
                this.date = value;
            }
        }

        public EnumFilter Filter
        {
            get
            {
                return this.filter;
            }
            set
            {
                this.filter = value;
                if (value == EnumFilter.All)
                {
                    for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                    {
                        this.fpSpread1_Sheet1.Rows[i].Visible = true;
                    }
                }
                else if (value == EnumFilter.NotYet)
                {
                    for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                    {
                        if ((this.fpSpread1_Sheet1.Rows[i].Tag as OperationAppllication).ExecStatus != "3" && (this.fpSpread1_Sheet1.Rows[i].Tag as OperationAppllication).ExecStatus != "4")
                            this.fpSpread1_Sheet1.Rows[i].Visible = true;
                        else
                            this.fpSpread1_Sheet1.Rows[i].Visible = false;
                    }
                }
                else
                {
                    for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                    {
                        if ((this.fpSpread1_Sheet1.Rows[i].Tag as OperationAppllication).ExecStatus == "3"||(this.fpSpread1_Sheet1.Rows[i].Tag as OperationAppllication).ExecStatus == "4")
                            this.fpSpread1_Sheet1.Rows[i].Visible = true;
                        else
                            this.fpSpread1_Sheet1.Rows[i].Visible = false;
                    }
                }
            }
        }
        public enum EnumFilter
        {
            /// <summary>
            /// 全部
            /// </summary>
            All,
            /// <summary>
            /// 未安排
            /// </summary>
            NotYet,
            /// <summary>
            /// 已安排
            /// </summary>
            Already
        }

        #endregion

        #region 方法

        private void Init()
        {
            ArrayList alOperatoinOrder = Environment.IntegrateManager.GetConstantList("OperatoinOrder");
            OperationOrderHelper.ArrayObject = alOperatoinOrder;
            this.fpSpread1.SetInputMap();
            //this.fpSpread1.AddListBoxPopup(lbNurse, 10);
            //this.fpSpread1.AddListBoxPopup(lbNurse, 11);
            //this.fpSpread1.AddListBoxPopup(this.lbRoom, 12);
            //this.fpSpread1.AddListBoxPopup(lbTable, 13);
            //this.fpSpread1.AddListBoxPopup(lbNurse, 15);
            //this.fpSpread1.AddListBoxPopup(lbNurse, 16);
            //{A4764DE2-685E-4bb4-8B92-A77214904644}feng.ch手术安排界面可以修改麻醉方式
            ArrayList types = Environment.IntegrateManager.GetConstantList("CASEANESTYPE");//(FS.HISFC.Models.Base.EnumConstant.ANESTYPE);
            lbAnaetype.AddItems(types);
            this.anaeTypeHelper.ArrayObject = types;
            this.fpSpread1.AddListBoxPopup(lbAnaetype, (int)Cols.anaeType);
            this.fpSpread1.AddListBoxPopup(lbAnaetype, (int)Cols.anaeType2);
            this.fpSpread1_Sheet1.Columns[(int)Cols.anaeType].Locked = false;
            this.fpSpread1_Sheet1.Columns[(int)Cols.anaeType2].Locked = false;
            this.fpSpread1.AddListBoxPopup(lbNurse, (int)Cols.WNR);
            this.fpSpread1.AddListBoxPopup(lbNurse, (int)Cols.INR);
            this.fpSpread1.AddListBoxPopup(this.lbRoom, (int)Cols.RoomID);
            this.fpSpread1.AddListBoxPopup(lbTable, (int)Cols.TableID);
            this.fpSpread1.AddListBoxPopup(lbNurse, (int)Cols.WNR2);
            this.fpSpread1.AddListBoxPopup(lbNurse, (int)Cols.INR2);
            this.fpSpread1.AddListBoxPopup(lbNurse, (int)Cols.MAININR);
            this.fpSpread1.AddListBoxPopup(lbNurse, (int)Cols.ShiftNurse);
            //FS.FrameWork.WinForms.Classes.MarkCellType.DateTimeCellType dtCellType = new FS.FrameWork.WinForms.Classes.MarkCellType.DateTimeCellType();
            //dtCellType.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.UserDefined;
            //dtCellType.UserDefinedFormat = "yyyy-mm-dd HH:mm:ss";
            //this.fpSpread1_Sheet1.Columns[(int)Cols.opDate].CellType = dtCellType;

            //{B9DDCC10-3380-4212-99E5-BB909643F11B}
            System.Collections.ArrayList al = this.managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.ANESWAY);
            this.anneObjectHelper.ArrayObject = al;
        }
        /// <summary>
        /// 添加护士listbox列表
        /// </summary>
        /// <returns></returns>
        private int InitNurseListBox()
        {
            ArrayList nurses = Environment.IntegrateManager.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.N, Environment.OperatorDeptID);

            //ArrayList al = new ArrayList();
            //if (nurses != null)
            //{
            //    foreach (FS.HISFC.Object.RADT.Person nurse in nurses)
            //    {
            //        NeuObject obj = (NeuObject)nurse;
            //        al.Add(obj);
            //    }
            //}
            lbNurse.AddItems(nurses);
            lbNurse.Font = new Font("宋体", 12);
            lbNurse.Size = new Size(220, 96);
            this.Controls.Add(lbNurse);
            this.lbNurse.Hide();
            this.lbNurse.BorderStyle = BorderStyle.FixedSingle;
            this.lbNurse.BringToFront();
            this.lbNurse.ItemSelected += new System.EventHandler(this.lbNurse_ItemSelected);

            return 0;
        }

        private void InitRoomListBox()
        {
            this.RefreshRoomListBox();

            this.Controls.Add(this.lbRoom);
            this.lbRoom.Font = new Font("宋体", 12);
            this.lbRoom.Size = new Size(220, 96);
            this.lbRoom.Hide();
            this.lbRoom.BorderStyle = BorderStyle.FixedSingle;
            this.lbRoom.BringToFront();
            this.lbRoom.ItemSelected += new System.EventHandler(this.lbRoom_ItemSelected);

        }
        //生成手术间列表
        private void RefreshRoomListBox()
        {
            ArrayList al = new ArrayList();

            ArrayList rooms = Environment.TableManager.GetRoomsByDept(Environment.OperatorDeptID);
            if (rooms != null)
            {
                foreach (FS.HISFC.Models.Operation.OpsRoom room in rooms)
                {
                    alRooms.Add(room);
                    if (room.IsValid == false)
                        continue;
                    //因为table类没有实现ISpell接口，所以借用department类
                    FS.HISFC.Models.Base.Department dept = new FS.HISFC.Models.Base.Department();
                    dept.ID = room.ID;
                    dept.Name = room.Name;
                    dept.SpellCode = room.InputCode;
                    al.Add(dept);
                }
            }
            lbRoom.AddItems(al);
        }

        private void InitTableListBox()
        {


            this.Controls.Add(this.lbTable);
            this.lbTable.Font = new Font("宋体", 12);
            this.lbTable.Size = new Size(220, 96);
            this.lbTable.Hide();
            this.lbTable.BorderStyle = BorderStyle.FixedSingle;
            this.lbTable.BringToFront();
            this.lbTable.ItemSelected += new System.EventHandler(this.lbTable_ItemSelected);

        }
        //添加手术台listbox列表
        private int RefreshTableListBox(string roomID)
        {
            ArrayList al = Environment.TableManager.GetOpsTable(roomID);
            ArrayList tables = new ArrayList();
            lbTable.Items.Clear();
            if (al != null)
            {
                foreach (OpsTable table in al)
                {
                    if (table.IsValid == false) continue;
                    //因为table类没有实现ISpell接口，所以借用department类
                    FS.HISFC.Models.Base.Department dept = new FS.HISFC.Models.Base.Department();
                    dept.ID = table.ID;
                    dept.Name = table.Name;
                    dept.SpellCode = table.InputCode;
                    tables.Add(dept);
                }
            }

            lbTable.AddItems(tables);

            return 0;
        }
        /// <summary>
        ///  选择护士
        /// </summary>
        /// <param name="Column"></param>
        /// <returns></returns>
        private int SelectNurse(int Column)
        {
            int CurrentRow = fpSpread1_Sheet1.ActiveRowIndex;
            if (CurrentRow < 0) return 0;

            fpSpread1.StopCellEditing();
            NeuObject item = this.lbNurse.GetSelectedItem();

            if (item == null)
                return -1;

            fpSpread1_Sheet1.Cells[CurrentRow, Column].Tag = item;
            fpSpread1_Sheet1.SetValue(CurrentRow, Column, item.Name, false);

            lbNurse.Visible = false;

            return 0;
        }

        //选择房号
        private int SelectRoom()
        {
            int CurrentRow = fpSpread1_Sheet1.ActiveRowIndex;
            if (CurrentRow < 0) return 0;

            fpSpread1.StopCellEditing();
            NeuObject item = null;
            item = lbRoom.GetSelectedItem();

            if (item == null) return -1;

            fpSpread1_Sheet1.Cells[CurrentRow, (int)Cols.RoomID].Tag = item;
            fpSpread1_Sheet1.SetValue(CurrentRow, (int)Cols.RoomID, item.Name, false);

            //this.refreshTableListBox(item.ID);

            lbRoom.Visible = false;

            return 0;
        }

        //选择手术台
        private int SelectTable()
        {
            int CurrentRow = fpSpread1_Sheet1.ActiveRowIndex;

            if (CurrentRow < 0) return 0;

            fpSpread1.StopCellEditing();
            NeuObject item = null;
            item = lbTable.GetSelectedItem();

            if (item == null) return -1;

            fpSpread1_Sheet1.Cells[CurrentRow, (int)Cols.TableID].Tag = item;
            fpSpread1_Sheet1.SetValue(CurrentRow, (int)Cols.TableID, item.Name, false);

            lbTable.Visible = false;

            return 0;
        }


        /// <summary>
        /// 设置护士、手术台列表位置
        /// </summary>
        /// <returns></returns>
        private int SetLocation()
        {

            Control _cell = fpSpread1.EditingControl;
            if (_cell == null) return 0;

            //洗手、巡回护士
            int Column = fpSpread1_Sheet1.ActiveColumnIndex;
            if (Column == (int)Cols.WNR || Column == (int)Cols.INR || Column == (int)Cols.WNR2)
            {
                lbNurse.Location = new Point(_cell.Location.X,
                    _cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2);
                lbNurse.Size = new Size(150, 150);
            }
            else if (Column == (int)Cols.INR2)
            {
                lbNurse.Location = new Point(_cell.Location.X + _cell.Width - 110,
                     _cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2);
                lbNurse.Size = new Size(150, 150);
            }
            else if (Column == (int)Cols.MAININR)
            {
                lbNurse.Location = new Point(_cell.Location.X + _cell.Width - 110,
                     _cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2);
                lbNurse.Size = new Size(150, 150);
            }
            else if (Column == (int)Cols.ShiftNurse)
            {
                lbNurse.Location = new Point(_cell.Location.X + _cell.Width - 110,
                     _cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2);
                lbNurse.Size = new Size(150, 150);
            }
            else if (fpSpread1_Sheet1.ActiveColumnIndex == (int)Cols.TableID)
            {
                if (_cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2 + 150 > this.fpSpread1.Height)
                {
                    lbTable.Location = new Point(_cell.Location.X,
                       _cell.Location.Y - 150);
                }
                else
                {
                    lbTable.Location = new Point(_cell.Location.X,
                        _cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2);
                }
                lbTable.Size = new Size(150, 150);
            }
            else if (fpSpread1_Sheet1.ActiveColumnIndex == (int)Cols.RoomID)
            {
                if (_cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2 + 150 > this.fpSpread1.Height)
                {
                    lbRoom.Location = new Point(_cell.Location.X,
                       _cell.Location.Y - 150);
                }
                else
                {
                    lbRoom.Location = new Point(_cell.Location.X,
                        _cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2);
                }
                lbRoom.Size = new Size(150, 150);
            }
            else if (fpSpread1_Sheet1.ActiveColumnIndex == (int)Cols.anaeType2)
            {
                lbAnaetype.Location = new Point(_cell.Location.X,
                    _cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2);
                lbAnaetype.Size = new Size(150, 150);
            }
            else if (fpSpread1_Sheet1.ActiveColumnIndex == (int)Cols.anaeType)
            {
                lbAnaetype.Location = new Point(_cell.Location.X,
                    _cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2);
                lbAnaetype.Size = new Size(150, 150);
            }

            return 0;
        }


        /// <summary>
        /// 添加手术申请信息
        /// </summary>
        /// <param name="apply"></param>
        /// <returns></returns>
        public int AddOperationApplication(FS.HISFC.Models.Operation.OperationAppllication apply)
        {
            //放入动态数组
            this.alApplys.Add(apply);

            fpSpread1_Sheet1.Rows.Add(fpSpread1_Sheet1.RowCount, 1);
            int row = fpSpread1_Sheet1.RowCount - 1;


            FarPoint.Win.Spread.CellType.TextCellType txtType = new FarPoint.Win.Spread.CellType.TextCellType();
            txtType.StringTrim = System.Drawing.StringTrimming.EllipsisWord;
            txtType.ReadOnly = true;
            fpSpread1_Sheet1.Rows[row].Tag = apply;

            #region 有作废申请的手术信息给予标记
            FS.HISFC.Models.Operation.OperationAppllication applicationTmp = Environment.OperationManager.QueryCancelApplyByAppID(apply.ID);
            if (applicationTmp != null && !string.IsNullOrEmpty(applicationTmp.ID))
            {
                fpSpread1_Sheet1.RowHeader.Cells[row,0].Text = "申请作废";
                fpSpread1_Sheet1.SetValue(row, (int)Cols.ArrangeNote, applicationTmp.Memo, false);
            }
            #endregion
            //护士站
            if (deptManager == null)
                deptManager = new FS.HISFC.BizProcess.Integrate.Manager();

            FS.HISFC.Models.Base.Department dept = deptManager.GetDepartment(apply.PatientInfo.PVisit.PatientLocation.Dept.ID);
            apply.PatientInfo.PVisit.PatientLocation.Name = dept.Name;
            if (dept != null)
            {
                fpSpread1_Sheet1.SetValue(row, (int)Cols.nurseID, dept.Name, false);
            }

            //病区 //门诊不一定有
            FS.HISFC.Models.Base.Department nursecell = deptManager.GetDepartment(apply.PatientInfo.PVisit.PatientLocation.NurseCell.ID);
            if (nursecell != null)
            {
                apply.PatientInfo.PVisit.PatientLocation.NurseCell.Name = nursecell.Name;
                fpSpread1_Sheet1.SetValue(row, (int)Cols.nurseCell, nursecell.Name, false);
            }

            //床号 add by zhy
            fpSpread1_Sheet1.SetValue(row, (int)Cols.bedID, apply.PatientInfo.PVisit.PatientLocation.Bed.Name, false);
            //住院号
            fpSpread1_Sheet1.SetValue(row, (int)Cols.patientNO, apply.PatientInfo.PID.PatientNO, false);
            //患者姓名
            fpSpread1_Sheet1.SetValue(row, (int)Cols.Name, apply.PatientInfo.Name, false);
            //性别
            fpSpread1_Sheet1.SetValue(row, (int)Cols.Sex, apply.PatientInfo.Sex.Name, false);
            //年龄
            int age = this.dataManager.GetDateTimeFromSysDateTime().Year - apply.PatientInfo.Birthday.Year;
            if (age == 0) age = 1;
            fpSpread1_Sheet1.SetValue(row, (int)Cols.Age, age.ToString(), false);
            fpSpread1_Sheet1.SetValue(row, (int)Cols.applyDate, apply.ApplyDate, false);
            //是否已安排
            if (apply.ExecStatus == "3"||apply.ExecStatus == "4")
            {
                fpSpread1_Sheet1.Cells[row, (int)Cols.Name].Note = "已安排";
                //this.fpSpread1_Sheet1.Rows[row].BackColor = System.Drawing.Color.LightBlue;
                this.fpSpread1_Sheet1.Rows[row].BackColor = System.Drawing.Color.LightPink;
            }
            else
            {
                fpSpread1_Sheet1.Cells[row, (int)Cols.Name].Note = "";
            }
            //台序
            //fpSpread1_Sheet1.SetValue(row, (int)Cols.Order, apply.BloodUnit, false);
            fpSpread1_Sheet1.SetValue(row, (int)Cols.Order, OperationOrderHelper.GetObjectFromName(apply.BloodUnit).ID, false);
            //手术台类型
            switch (apply.TableType)
            {
                case "1":
                    fpSpread1_Sheet1.SetValue(row, (int)Cols.Desk, "正台", false);
                    break;
                case "2":
                    fpSpread1_Sheet1.SetValue(row, (int)Cols.Desk, "接台", false);
                    break;
                case "3":
                    fpSpread1_Sheet1.SetValue(row, (int)Cols.Desk, "点台", false);
                    break;
            }

            //主手术名称            
            fpSpread1_Sheet1.SetValue(row, (int)Cols.opItemName, apply.MainOperationName, false);
            //术前诊断     
            // fpSpread1_Sheet1.SetValue(row, (int)Cols.foreDiagnose, apply.DiagnoseAl[0], false);
            string forediagnose = string.Empty;

            for (int t = 0; t < apply.DiagnoseAl.Count; ++t)
            {
                forediagnose += "诊断" + (t + 1).ToString() + ":" + apply.DiagnoseAl[t].ToString() + "  ";
            }
            //forediagnose = apply.DiagnoseAl[0] + "\r\n" + apply.DiagnoseAl[1] + "\r\n" + apply.DiagnoseAl[2];
            fpSpread1_Sheet1.SetValue(row, (int)Cols.foreDiagnose, forediagnose, false);


            //是否特殊手术
            if (apply.IsSpecial)
            {

                if (apply.BloodNum.ToString() == "1")
                {
                    fpSpread1_Sheet1.SetValue(row, (int)Cols.isSpecial, "HAV", false);
                }
                if (apply.BloodNum.ToString() == "2")
                {
                    fpSpread1_Sheet1.SetValue(row, (int)Cols.isSpecial, "HBV", false);
                }
                if (apply.BloodNum.ToString() == "3")
                {
                    fpSpread1_Sheet1.SetValue(row, (int)Cols.isSpecial, "HCV", false);
                }
                if (apply.BloodNum.ToString() == "4")
                {
                    fpSpread1_Sheet1.SetValue(row, (int)Cols.isSpecial, "HDV", false);
                }
                if (apply.BloodNum.ToString() == "5")
                {
                    fpSpread1_Sheet1.SetValue(row, (int)Cols.isSpecial, "HIV", false);
                }
                if (apply.BloodNum.ToString() == "6")
                {
                    fpSpread1_Sheet1.SetValue(row, (int)Cols.isSpecial, "其他", false);
                }
            }
            else
            {
                fpSpread1_Sheet1.SetValue(row, (int)Cols.isSpecial, "否", false);
            }

            //特殊说明
            fpSpread1_Sheet1.SetValue(row, (int)Cols.specialNote, apply.ApplyNote, false);

            FS.FrameWork.Models.NeuObject obj = null;
            // FS.FrameWork.Models.NeuObject obj2 = null;
            string obj2 = string.Empty;


            //麻醉类别格式设置 不用也可以哇
            fpSpread1_Sheet1.Cells[row, (int)Cols.anaeWay].CellType = txtType;
            fpSpread1_Sheet1.Cells[row, (int)Cols.TableID].CellType = txtType;

            //麻醉方式
            if (apply.AnesType.ID != null && apply.AnesType.ID.Length != 0)
            {
                int le = apply.AnesType.ID.IndexOf("|");
                if (le > 0)
                {
                    obj = Environment.GetAnes(apply.AnesType.ID.Substring(0, le));
                    if (obj != null)
                    {
                        fpSpread1_Sheet1.SetValue(row, (int)Cols.anaeType, obj.Name, false);
                        fpSpread1_Sheet1.Columns[(int)Cols.anaeType].Locked = false;
                        apply.AnesType.Name = obj.Name + "、";
                    }
                    obj = Environment.GetAnes(apply.AnesType.ID.Substring(le + 1));
                    if (obj != null)
                    {
                        // obj2.Name = obj.Name;
                        obj2 = obj.Name;
                        fpSpread1_Sheet1.SetValue(row, (int)Cols.anaeType2, "、" + obj.Name, false);
                        fpSpread1_Sheet1.Columns[(int)Cols.anaeType2].Locked = false;
                        apply.AnesType.Name += obj.Name;
                        fpSpread1_Sheet1.SetValue(row, (int)Cols.anaeType2, obj2, false);
                    }
                }
                else
                {
                    obj = Environment.GetAnes(apply.AnesType.ID);
                    if (obj != null)
                    {
                        fpSpread1_Sheet1.SetValue(row, (int)Cols.anaeType, obj.Name, false);
                        fpSpread1_Sheet1.Columns[(int)Cols.anaeType].Locked = false;
                        apply.AnesType.Name = obj.Name;
                    }
                }

                fpSpread1_Sheet1.SetValue(row, (int)Cols.anaeWay, apply.AnesType.Name);
            }
            else
            {
                fpSpread1_Sheet1.Cells[row, (int)Cols.anaeType].Note = "未安排麻醉";
            }

            //手术医生
            fpSpread1_Sheet1.SetValue(row, (int)Cols.opDoctID, apply.OperationDoctor.Name, false);
            //是否急诊
            if (apply.OperateKind == "2")
            {
                fpSpread1_Sheet1.RowHeader.Cells[row, 0].BackColor = Color.Red;
                fpSpread1_Sheet1.RowHeader.Cells[row, 0].Text = fpSpread1_Sheet1.RowHeader.Cells[row, 0].Text +  "（急）";
                //fpSpread1_Sheet1.RowHeader.Cells[row, 0].ForeColor = Color.Red;
            }
            //门诊{BD5A9D98-A2BA-4e14-9FE4-D8CF322E5B60}
            if (apply.PatientSouce == "1")
            {
                fpSpread1_Sheet1.RowHeader.Cells[row, 0].BackColor = Color.PeachPuff;
                fpSpread1_Sheet1.RowHeader.Cells[row, 0].Text = "门诊";
                fpSpread1_Sheet1.RowHeader.Cells[row, 0].ForeColor = Color.Red;
                //是否急诊
                if (apply.OperateKind == "2")
                {
                    fpSpread1_Sheet1.RowHeader.Cells[row, 0].BackColor = Color.Red;
                    fpSpread1_Sheet1.RowHeader.Cells[row, 0].Text = "门诊急";
                    fpSpread1_Sheet1.RowHeader.Cells[row, 0].ForeColor = Color.Red;
                }
            }
            fpSpread1_Sheet1.Cells[row, 0, row, 7].CellType = txtType;
            //{3B34DBA7-7740-46eb-93E7-5F2D7890B602}加选择框
            FarPoint.Win.Spread.CellType.CheckBoxCellType chkType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            fpSpread1_Sheet1.Cells[row, (int)Cols.isPrint].CellType = chkType;
            fpSpread1_Sheet1.Cells[row, (int)Cols.isPrint].Value = false;
            fpSpread1_Sheet1.Cells[row, (int)Cols.isPrint].Locked = false;
            //手术时间
            fpSpread1_Sheet1.SetValue(row, (int)Cols.opDate, apply.PreDate, false);
            #region 护士
            if (apply.RoleAl != null && apply.RoleAl.Count != 0)
            {
                foreach (FS.HISFC.Models.Operation.ArrangeRole role in apply.RoleAl)
                {
                    #region  屏蔽
                    //if (role.RoleType.ID.ToString() == EnumOperationRole.WashingHandNurse.ToString()) //洗手护士                        
                    //{
                    //    if (fpSpread1_Sheet1.Cells[row, (int)Cols.WNR].Text == "") //第一洗手护士
                    //    {
                    //        fpSpread1_Sheet1.SetValue(row, (int)Cols.WNR, role.Name, false);
                    //        obj = (FS.FrameWork.Models.NeuObject)role;
                    //        fpSpread1_Sheet1.SetTag(row, (int)Cols.WNR, obj);
                    //    }
                    //    else
                    //    {   //第二洗手护士
                    //        fpSpread1_Sheet1.SetValue(row, (int)Cols.WNR2, role.Name, false);
                    //        obj = (NeuObject)role;
                    //        fpSpread1_Sheet1.SetTag(row, (int)Cols.WNR2, obj);
                    //    }
                    //}
                    //else if (role.RoleType.ID.ToString() == EnumOperationRole.ItinerantNurse.ToString())//巡回护士
                    //{
                    //    if (fpSpread1_Sheet1.Cells[row, (int)Cols.INR].Text == "")
                    //    {   //第一巡回护士 
                    //        fpSpread1_Sheet1.SetValue(row, (int)Cols.INR, role.Name, false);
                    //        obj = (NeuObject)role;
                    //        fpSpread1_Sheet1.SetTag(row, (int)Cols.INR, obj);
                    //    }
                    //    else
                    //    {
                    //        //第二巡回护士
                    //        fpSpread1_Sheet1.SetValue(row, (int)Cols.INR2, role.Name, false);
                    //        obj = (NeuObject)role;
                    //        fpSpread1_Sheet1.SetTag(row, (int)Cols.INR2, obj);
                    //    }

                    //}
                    #endregion
                    if (role.RoleType.ID.ToString() == EnumOperationRole.WashingHandNurse1.ToString()) //洗手护士                        
                    {
                        //第一洗手护士
                        fpSpread1_Sheet1.SetValue(row, (int)Cols.WNR, role.Name, false);
                        obj = (FS.FrameWork.Models.NeuObject)role;
                        fpSpread1_Sheet1.SetTag(row, (int)Cols.WNR, obj);
                    }
                    else if (role.RoleType.ID.ToString() == EnumOperationRole.WashingHandNurse2.ToString())
                    {
                        //第二洗手护士
                        fpSpread1_Sheet1.SetValue(row, (int)Cols.WNR2, role.Name, false);
                        obj = (NeuObject)role;
                        fpSpread1_Sheet1.SetTag(row, (int)Cols.WNR2, obj);
                    }
                    else if (role.RoleType.ID.ToString() == EnumOperationRole.ItinerantNurse1.ToString())//巡回护士
                    {
                        //第一巡回护士 
                        fpSpread1_Sheet1.SetValue(row, (int)Cols.INR, role.Name, false);
                        obj = (NeuObject)role;
                        fpSpread1_Sheet1.SetTag(row, (int)Cols.INR, obj);
                    }
                    else if (role.RoleType.ID.ToString() == EnumOperationRole.ItinerantNurse2.ToString())
                    {
                        //第二巡回护士
                        fpSpread1_Sheet1.SetValue(row, (int)Cols.INR2, role.Name, false);
                        obj = (NeuObject)role;
                        fpSpread1_Sheet1.SetTag(row, (int)Cols.INR2, obj);
                    }
                    else if (role.RoleType.ID.ToString() == EnumOperationRole.ItinerantNurse3.ToString())
                    {
                        fpSpread1_Sheet1.SetValue(row, (int)Cols.MAININR, role.Name, false);
                        obj = (NeuObject)role;
                        fpSpread1_Sheet1.SetTag(row, (int)Cols.MAININR, obj);
                    }

                    //一助手
                    else if (role.RoleType.ID.ToString() == EnumOperationRole.Helper1.ToString())
                    {
                        fpSpread1_Sheet1.SetValue(row, (int)Cols.helper1, role.Name, false);
                        obj = (NeuObject)role;
                        fpSpread1_Sheet1.SetTag(row, (int)Cols.helper1, obj);
                    }
                    //二助手
                    else if (role.RoleType.ID.ToString() == EnumOperationRole.Helper2.ToString())
                    {
                        fpSpread1_Sheet1.SetValue(row, (int)Cols.helper2, role.Name, false);
                        obj = (NeuObject)role;
                        fpSpread1_Sheet1.SetTag(row, (int)Cols.helper2, obj);
                    }
                    //接班人员
                    else if (role.RoleType.ID.ToString() == EnumOperationRole.OpsShiftNurse.ToString())
                    {
                        fpSpread1_Sheet1.SetValue(row, (int)Cols.ShiftNurse, role.Name, false);
                        obj = (NeuObject)role;
                        fpSpread1_Sheet1.SetTag(row, (int)Cols.ShiftNurse, obj);
                    }

                    else if (role.RoleType.ID.ToString() == EnumOperationRole.Anaesthetist.ToString())
                    {
                        fpSpread1_Sheet1.SetValue(row, (int)Cols.ansDocID, role.Name, false);
                        obj = (NeuObject)role;
                        fpSpread1_Sheet1.SetTag(row, (int)Cols.ansDocID, obj);
                    }
                    else if (role.RoleType.ID.ToString() == EnumOperationRole.AnaesthesiaHelper.ToString())
                    {
                        fpSpread1_Sheet1.SetValue(row, (int)Cols.ansHelper1, role.Name, false);
                        obj = (NeuObject)role;
                        fpSpread1_Sheet1.SetTag(row, (int)Cols.ansHelper1, obj);
                    }
                }
            }
            #endregion
            //手术间
            FarPoint.Win.Spread.CellType.NumberCellType numType = new FarPoint.Win.Spread.CellType.NumberCellType();
            numType.DecimalPlaces = 0;
            fpSpread1_Sheet1.Cells[row, (int)Cols.RoomID].CellType = numType;
            if (apply.RoomID != null && apply.RoomID != "")
            {

                obj = GetRoom(apply.RoomID);
                //fpSpread1_Sheet1.SetValue(row, (int)Cols.RoomID, Convert.ToDecimal(obj.Name), false); //转换一下，为了排序
                fpSpread1_Sheet1.SetValue(row, (int)Cols.RoomID, obj.Name, false);
                fpSpread1_Sheet1.SetTag(row, (int)Cols.RoomID, obj);

                // fpSpread1_Sheet1.SetValue(row, (int)Cols.RoomID, apply.RoomID, false);
            }
            #region 手术台
            if (apply.OpsTable.ID != null && apply.OpsTable.ID != "")
            {
                fpSpread1_Sheet1.SetValue(row, (int)Cols.TableID, apply.OpsTable.Name, false);
                obj = new NeuObject();
                obj.ID = apply.OpsTable.ID;
                obj.Name = apply.OpsTable.Name;
                fpSpread1_Sheet1.SetTag(row, (int)Cols.TableID, obj);
            }



            //增加一列手术不安排的原因 add by zhy
            //fpSpread1_Sheet1.SetValue(row, (int)Cols.ArrangeNote, apply.Memo, false);

            #endregion

            return 0;
        }

        /// <summary>
        /// 清空
        /// </summary>
        public void Reset()
        {
            this.fpSpread1_Sheet1.RowCount = 0;
            this.alApplys.Clear();
        }
        /// <summary>
        /// 获取手术房间
        /// </summary>
        /// <param name="roomID"></param>
        /// <returns></returns>
        private NeuObject GetRoom(string roomID)
        {
            NeuObject obj = new NeuObject();
            foreach (OpsRoom room in alRooms)
            {
                if (roomID == room.ID)
                {
                    obj.ID = room.ID;
                    obj.Name = room.Name;
                    return obj;
                }
            }
            obj.ID = roomID;
            obj.Name = "无";
            return obj;
        }



        /// <summary>
        /// 校对数据 
        /// </summary>
        /// <returns></returns>
        private int ValueState()
        {
            for (int row = 0; row < this.fpSpread1_Sheet1.RowCount; row++)
            {
                NeuObject obj = fpSpread1_Sheet1.GetTag(row, (int)Cols.WNR) as NeuObject;
                NeuObject obj2 = fpSpread1_Sheet1.GetTag(row, (int)Cols.WNR2) as NeuObject;
                if (obj != null && obj2 != null)
                {
                    if (obj.ID == obj2.ID)
                    {
                        MessageBox.Show("洗手护士不能重复");
                        return -1;
                    }
                }
                NeuObject obj3 = fpSpread1_Sheet1.GetTag(row, (int)Cols.INR) as NeuObject;
                NeuObject obj4 = fpSpread1_Sheet1.GetTag(row, (int)Cols.INR2) as NeuObject;
                NeuObject obj5 = fpSpread1_Sheet1.GetTag(row, (int)Cols.MAININR) as NeuObject;
                if (obj3 != null && obj4 != null)
                {
                    if (obj3.ID == obj4.ID)
                    {
                        MessageBox.Show("巡回护士不能重复");
                        return -1;
                    }
                }
                if (obj3 != null && obj5 != null)
                {
                    if (obj3.ID == obj5.ID)
                    {
                        MessageBox.Show("巡回护士不能重复");
                        return -1;
                    }
                } if (obj4 != null && obj5 != null)
                {
                    if (obj4.ID == obj5.ID)
                    {
                        MessageBox.Show("巡回护士不能重复");
                        return -1;
                    }
                }
                NeuObject tabRoom = fpSpread1_Sheet1.GetTag(row, (int)Cols.RoomID) as NeuObject;
                NeuObject tabTable = fpSpread1_Sheet1.GetTag(row, (int)Cols.TableID) as NeuObject;
                #region {42CDE890-24B3-4d6f-A52B-988F62E226B8}
                if (obj != null || obj2 != null || obj3 != null || obj4 != null || tabRoom != null || tabTable != null)
                {

                    //没有录入手术台，不处理
                    if (tabRoom == null)
                    {
                        MessageBox.Show("请选择手术房间");
                        return -1;
                    }

                    //没有录入手术台，不处理
                    if (tabTable == null)
                    {
                        MessageBox.Show("请选择手术台号");
                        return -1;
                    }
                }
                #endregion
            }
            return 0;
        }


        /// <summary>
        /// 添加各类护士
        /// </summary>
        /// <param name="apply"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private int AddRole(OperationAppllication apply, int row)
        {
            ArrayList roles = new ArrayList();
            //先清空护士
            for (int i = 0; i < apply.RoleAl.Count; i++)
            {
                ArrangeRole role = apply.RoleAl[i] as ArrangeRole;
                if (role.RoleType.ID.ToString() != EnumOperationRole.WashingHandNurse1.ToString() &&
                    role.RoleType.ID.ToString() != EnumOperationRole.WashingHandNurse2.ToString() &&
                    role.RoleType.ID.ToString() != EnumOperationRole.ItinerantNurse1.ToString() &&
                    role.RoleType.ID.ToString() != EnumOperationRole.ItinerantNurse2.ToString() &&
                    role.RoleType.ID.ToString() != EnumOperationRole.ItinerantNurse3.ToString() &&
                    role.RoleType.ID.ToString() != EnumOperationRole.OpsShiftNurse.ToString())
                {
                    roles.Add(role.Clone());
                }
            }




            //添加洗手护士
            NeuObject obj = fpSpread1_Sheet1.GetTag(row, (int)Cols.WNR) as NeuObject;
            if (obj != null)
            {
                ArrangeRole role = new ArrangeRole(obj);
                role.RoleType.ID = EnumOperationRole.WashingHandNurse1;//角色编码
                role.OperationNo = apply.ID;
                role.ForeFlag = "0";//术前安排				
                roles.Add(role);//加入人员角色对象	
                //第二洗手护士  可以不填
                if (fpSpread1_Sheet1.Cells[row, (int)Cols.WNR2].Tag != null)
                {
                    NeuObject obj2 = fpSpread1_Sheet1.GetTag(row, (int)Cols.WNR2) as NeuObject;
                    if (obj2 != null)
                    {
                        ArrangeRole role2 = new ArrangeRole(obj2);
                        role2.RoleType.ID = EnumOperationRole.WashingHandNurse2;//角色编码
                        role2.OperationNo = apply.ID;
                        role2.ForeFlag = "0";//术前安排				
                        roles.Add(role2);//加入人员角色对象	
                    }
                }
            }
            //添加巡回护士
            NeuObject obj3 = fpSpread1_Sheet1.GetTag(row, (int)Cols.INR) as NeuObject;
            if (obj3 != null)
            {
                ArrangeRole role = new ArrangeRole(obj3);
                role.RoleType.ID = EnumOperationRole.ItinerantNurse1;//角色编码
                role.OperationNo = apply.ID;
                role.ForeFlag = "0";//术前安排				
                roles.Add(role);//加入人员角色对象
                if (fpSpread1_Sheet1.Cells[row, (int)Cols.INR2].Tag != null)
                {
                    //添加第二巡回护士
                    NeuObject obj4 = fpSpread1_Sheet1.GetTag(row, (int)Cols.INR2) as NeuObject;
                    if (obj4 != null)
                    {
                        ArrangeRole role2 = new ArrangeRole(obj4);
                        role2.RoleType.ID = EnumOperationRole.ItinerantNurse2; ;//角色编码
                        role2.OperationNo = apply.ID;
                        role2.ForeFlag = "0";//术前安排				
                        roles.Add(role2);//加入人员角色对象
                    }
                }
                if (fpSpread1_Sheet1.Cells[row, (int)Cols.MAININR].Tag != null)
                {
                    //添加总巡回护士
                    NeuObject obj5 = fpSpread1_Sheet1.GetTag(row, (int)Cols.MAININR) as NeuObject;
                    if (obj5 != null)
                    {
                        ArrangeRole role3 = new ArrangeRole(obj5);
                        role3.RoleType.ID = EnumOperationRole.ItinerantNurse3; ;//角色编码
                        role3.OperationNo = apply.ID;
                        role3.ForeFlag = "0";//术前安排				
                        roles.Add(role3);//加入人员角色对象
                    }
                }
            }
            //添加接班人员
            NeuObject obj6 = fpSpread1_Sheet1.GetTag(row, (int)Cols.ShiftNurse) as NeuObject;
            if (obj6 != null)
            {
                ArrangeRole role4 = new ArrangeRole(obj6);
                role4.RoleType.ID = EnumOperationRole.OpsShiftNurse; //角色编码
                role4.OperationNo = apply.ID;
                role4.ForeFlag = "0";//术前安排
                roles.Add(role4);//加入人员角色对象
            }


            apply.RoleAl = roles;

            return 0;
        }


        /// <summary>
        /// 情况各类护士，用于手术不安排
        /// </summary>
        /// <param name="apply"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private int AddRoleUnArrange(OperationAppllication apply, int row)
        {
            ArrayList roles = new ArrayList();
            //先清空护士
            for (int i = 0; i < apply.RoleAl.Count; i++)
            {
                ArrangeRole role = apply.RoleAl[i] as ArrangeRole;
                if (role.RoleType.ID.ToString() != EnumOperationRole.WashingHandNurse1.ToString() &&
                    role.RoleType.ID.ToString() != EnumOperationRole.WashingHandNurse2.ToString() &&
                    role.RoleType.ID.ToString() != EnumOperationRole.ItinerantNurse1.ToString() &&
                    role.RoleType.ID.ToString() != EnumOperationRole.ItinerantNurse2.ToString() &&
                    role.RoleType.ID.ToString() != EnumOperationRole.ItinerantNurse3.ToString() &&
                    role.RoleType.ID.ToString() != EnumOperationRole.OpsShiftNurse.ToString())
                {
                    roles.Add(role.Clone());
                }
            }

            //  roles.Clear();
            apply.RoleAl = roles;

            return 0;
        }

        /// <summary>
        /// 更新实体的安排标志
        /// </summary>
        /// <param name="apply"></param>
        /// <returns></returns>
        private int UpdateFlag(OperationAppllication apply)
        {
            for (int index = 0; index < alApplys.Count; index++)
            {
                OperationAppllication obj = alApplys[index] as OperationAppllication;
                if (obj.ID == apply.ID)
                {
                    alApplys.Remove(obj);
                    alApplys.Insert(index, apply);
                    break;
                }
            }
            return 0;
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        private OperationAppllication UpdateData(int rowIndex)
        {
            NeuObject tab = fpSpread1_Sheet1.GetTag(rowIndex, (int)Cols.TableID) as NeuObject;
            //没有录入手术台，不处理
            if (tab == null)
                return null;

            OperationAppllication apply = fpSpread1_Sheet1.Rows[rowIndex].Tag as OperationAppllication;

            try
            {
                //添加手术台
                OpsTable table = new OpsTable();
                table.ID = tab.ID;
                table.Name = tab.Name;
                apply.OpsTable = table;
                //添加手术房间
                tab = fpSpread1_Sheet1.GetTag(rowIndex, (int)Cols.RoomID) as NeuObject;
                if (tab != null)
                {
                    apply.RoomID = tab.ID;
                    apply.OpePos.Memo = tab.Name;//add by chengym 打印用
                }
                else
                {
                    fpSpread1_Sheet1.SetValue(rowIndex, (int)Cols.RoomID, "", false);
                }

                //手术时间
                string dt = fpSpread1_Sheet1.GetText(rowIndex, (int)Cols.opDate);
                //dt = apply.PreDate.Year.ToString() + "-" + apply.PreDate.Month.ToString() + "-" + apply.PreDate.Day.ToString() + " " + dt;
                apply.PreDate = DateTime.Parse(dt);
                //添加各类护士
                this.AddRole(apply, rowIndex);

            }
            catch (Exception e)
            {

                return null;
            }

            return apply;
        }

        /// <summary>
        /// 更改手术患者状态，从ExecStatus=3的已安排状态改为ExecStatus=6的已手术未收费状态，此状态为医院要求添加
        /// </summary>
        /// <param name="apply"></param>
        /// <returns></returns>
        public int ChangeState()
        {
            //FS.FrameWork.Management.PublicTrans.BeginTransaction();

            ////trans.BeginTransaction();
            //Environment.OperationManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //try
            //{
            //    Already.ExecStatus='6'.ToString();
            //    if (Environment.OperationManager.UpdateAlreadyOperation(Already) == -1)
            //    {
            //        FS.FrameWork.Management.PublicTrans.RollBack();
            //        MessageBox.Show("更改手术(" + Already.ID + ")状态失败！\n请与系统管理员联系。" + Environment.OperationManager.Err, "提示",
            //            MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return -1;
            //    }

            //    FS.FrameWork.Management.PublicTrans.Commit();
            //}
            //catch (Exception e)
            //{
            //    FS.FrameWork.Management.PublicTrans.RollBack();
            //    MessageBox.Show("更改手术(" + Already.ID + ")状态失败!" + e.Message, "提示");
            //    return -1;
            //}

            for (int i = 0; i < fpSpread1_Sheet1.RowCount; i++)
            {
                //if ((bool)this.fpSpread1_Sheet1.Cells[i, (int)Cols.isPrint].Value == true)
                //{
                OperationAppllication AlreadyOriginal = fpSpread1_Sheet1.Rows[i].Tag as OperationAppllication;
                if (AlreadyOriginal == null)
                {
                    MessageBox.Show("实体转换出错！");
                    return -1;
                }
                OperationAppllication already = Environment.OperationManager.GetOpsApp(AlreadyOriginal.ID);
                if (already == null)
                {
                    MessageBox.Show("获取手术信息出错！");
                    return -1;
                }
                if (already.ID == "")
                {
                    continue;
                }

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                //trans.BeginTransaction();
                Environment.OperationManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                if ((bool)this.fpSpread1_Sheet1.Cells[i, (int)Cols.isPrint].Value == true)
                {
                    try
                    {
                        if (already.ExecStatus == "3")
                        {

                            {
                                //if (already.IsAnesth == true)
                                //{
                                already.ExecStatus = "6";
                                if (Environment.OperationManager.UpdateAlreadyOperation(already) == -1)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("更改手术(" + already.ID + ")状态失败！\n请与系统管理员联系。" + Environment.OperationManager.Err, "提示",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return -1;
                                }

                                FS.FrameWork.Management.PublicTrans.Commit();
                                MessageBox.Show("手术登记成功！", "提示");
                                //}
                                //else if (already.IsAnesth == false)
                                //{
                                //    MessageBox.Show("手术未麻醉，不允许登记！","提示");
                                //}

                            }
                        }
                        else
                        {
                            MessageBox.Show("手术未安排，不允许登记！", "提示");
                            continue;
                        }

                    }
                    catch (Exception e)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更改手术(" + already.ID + ")状态失败!" + e.Message, "提示");
                        return -1;
                    }
                }
                else
                {
                    continue;
                }
            }

            //else
            //{
            //    MessageBox.Show("请选择患者！");
            //}

            //}

            return 0;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            if (this.ValueState() == -1)
            {
                return -1;

            }

            //数据库事务
            //FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);


            List<int> succeed = new List<int>();        //成功安排的
            for (int i = 0; i < fpSpread1_Sheet1.RowCount; i++)
            {
                if ((bool)this.fpSpread1_Sheet1.Cells[i, (int)Cols.isPrint].Value == true)
                {
                    if (this.fpSpread1_Sheet1.Cells[i, (int)Cols.ArrangeNote].Text.Trim() == "")
                    {
                        NeuObject tab = fpSpread1_Sheet1.GetTag(i, (int)Cols.TableID) as NeuObject;
                        //{A4764DE2-685E-4bb4-8B92-A77214904644}
                        //麻醉方式
                        //NeuObject type = fpSpread1_Sheet1.GetTag(i, (int)Cols.anaeType) as NeuObject;
                        NeuObject type1 = new NeuObject();
                        type1.ID = anaeTypeHelper.GetID(this.fpSpread1_Sheet1.Cells[i, (int)Cols.anaeType].Text.Trim());
                        type1.Name = this.fpSpread1_Sheet1.Cells[i, (int)Cols.anaeType].Text.Trim();
                        NeuObject type2 = new NeuObject();
                        type2.ID = anaeTypeHelper.GetID(this.fpSpread1_Sheet1.Cells[i, (int)Cols.anaeType2].Text.Trim());
                        type2.Name = this.fpSpread1_Sheet1.Cells[i, (int)Cols.anaeType2].Text.Trim();
                        NeuObject type = new NeuObject();
                        if (type2 != null && type2.ID != "")
                        {
                            type.ID = type1.ID + "|" + type2.ID;
                            type2.Name = type1.Name + "|" + type2.Name;
                        }
                        else
                        {
                            type = type1.Clone();
                        }


                        //没有录入手术台，不处理
                        if (tab == null)
                        {
                            fpSpread1_Sheet1.Cells[i, (int)Cols.TableID].Text = "";
                            continue;
                        }
                        //OperationAppllication apply = fpSpread1_Sheet1.Rows[i].Tag as OperationAppllication;
                        //{3DC153BD-1E9B-40c4-AAFC-3C27607A8945}
                        OperationAppllication applyOriginal = fpSpread1_Sheet1.Rows[i].Tag as OperationAppllication;
                        if (applyOriginal == null)
                        {
                            MessageBox.Show("实体转换出错！");
                            return -1;
                        }
                        OperationAppllication apply = Environment.OperationManager.GetOpsApp(applyOriginal.ID);
                        if (apply == null)
                        {
                            MessageBox.Show("获取手术信息出错！");
                            return -1;
                        }
                        if (apply.ID == "")
                        {
                            continue;
                        }


                        FS.FrameWork.Management.PublicTrans.BeginTransaction();

                        //trans.BeginTransaction();
                        Environment.OperationManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                        try
                        {
                            //添加手术台
                            OpsTable table = new OpsTable();
                            //添加麻醉方式到手术实体
                            //{A4764DE2-685E-4bb4-8B92-A77214904644}
                            if (type != null)
                            {
                                apply.AnesType = type;
                            }
                            table.ID = tab.ID;
                            table.Name = tab.Name;
                            apply.OpsTable = table;
                            //添加手术房间
                            tab = fpSpread1_Sheet1.GetTag(i, (int)Cols.RoomID) as NeuObject;
                            if (tab != null)
                                apply.RoomID = tab.ID;
                            else
                                fpSpread1_Sheet1.SetValue(i, (int)Cols.RoomID, "", false);

                            //手术时间
                            string dt = fpSpread1_Sheet1.GetText(i, (int)Cols.opDate);
                            //dt = apply.PreDate.Year.ToString() + "-" + apply.PreDate.Month.ToString() + "-" + apply.PreDate.Day.ToString() + " " + dt;
                            apply.PreDate = DateTime.Parse(dt);
                            //添加各类护士
                            this.AddRole(apply, i);
                            //标志为已安排
                            apply.ExecStatus = "3";
                            apply.Memo = this.fpSpread1_Sheet1.Cells[i, (int)Cols.ArrangeNote].Text.Trim();

                            if (Environment.OperationManager.UpdateApplication(apply) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("保存手术(" + apply.ID + ")安排信息失败！\n请与系统管理员联系。" + Environment.OperationManager.Err, "提示",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return -1;
                            }
                            succeed.Add(i);
                            FS.FrameWork.Management.PublicTrans.Commit();
                        }
                        catch (Exception e)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("保存手术(" + apply.ID + ")安排信息出错!" + e.Message, "提示");
                            return -1;
                        }
                        //更新界面显示
                        fpSpread1_Sheet1.Rows[i].Tag = apply;
                        fpSpread1_Sheet1.Cells[i, (int)Cols.Name].Note = "已安排";
                        this.fpSpread1_Sheet1.Rows[i].BackColor = System.Drawing.Color.LightPink;
                        this.UpdateFlag(apply);
                    }
                    else
                    {

                        //{3DC153BD-1E9B-40c4-AAFC-3C27607A8945}
                        OperationAppllication applyOriginal = fpSpread1_Sheet1.Rows[i].Tag as OperationAppllication;
                        if (applyOriginal == null)
                        {
                            MessageBox.Show("实体转换出错！");
                            return -1;
                        }
                        OperationAppllication apply = Environment.OperationManager.GetOpsApp(applyOriginal.ID);
                        if (apply == null)
                        {
                            MessageBox.Show("获取手术信息出错！");
                            return -1;
                        }
                        if (apply.ID == "")
                        {
                            continue;
                        }


                        FS.FrameWork.Management.PublicTrans.BeginTransaction();

                        //trans.BeginTransaction();
                        Environment.OperationManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                        try
                        {


                            apply.RoomID = "";


                            apply.OpsTable.ID = "";

                            //this.AddRole(apply, i);
                            this.AddRoleUnArrange(apply, i);
                            //标志为不安排，即还是申请状态
                            apply.ExecStatus = "1";
                            apply.Memo = this.fpSpread1_Sheet1.Cells[i, (int)Cols.ArrangeNote].Text.Trim();

                            if (Environment.OperationManager.UpdateApplication(apply) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("保存手术(" + apply.ID + ")安排信息失败！\n请与系统管理员联系。" + Environment.OperationManager.Err, "提示",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return -1;
                            }
                            succeed.Add(i);
                            FS.FrameWork.Management.PublicTrans.Commit();
                        }
                        catch (Exception e)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("保存手术(" + apply.ID + ")安排信息出错!" + e.Message, "提示");
                            return -1;
                        }
                        fpSpread1_Sheet1.Rows[i].Tag = apply;
                        this.UpdateFlag(apply);
                    }
                }
                else
                {

                }


            }

            if (succeed.Count > 0)
            {
                string line = string.Empty;
                for (int i = 0; i < succeed.Count; i++)
                {
                    line += i.ToString() + ",";
                }
                line = line.Substring(0, line.Length - 1);
                MessageBox.Show(string.Format("手术安排成功。", line), "提示");
                fpSpread1.Focus();
                if (lbTable != null)
                {
                    lbTable.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("没有录入安排信息，无需保存");
            }

            return 0;
        }
        //{2D9FB02D-2AE2-42ac-AD5A-E2B12DC841CE}feng.ch
        /// <summary>
        /// 设置打印模式
        /// </summary>
        /// <param name="obj">传入实体</param>
        /// <returns></returns>
        public int SetPrintMode(FS.FrameWork.Models.NeuObject obj)
        {
            //默认打印模式：全部选中
            if (obj.ID == "rb1")
            {
                SortFlag = "rb1";
                this.isSortByRoom = false;
                for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                {
                    this.fpSpread1_Sheet1.Cells[i, (int)Cols.isPrint].Value = true;
                }
            }
            //所有手术间顺序打印
            if (obj.ID == "rb2")
            {
                SortFlag = "rb2";
                this.isSortByRoom = true;
                for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                {
                    this.fpSpread1_Sheet1.Cells[i, (int)Cols.isPrint].Value = true;
                }
            }
            //单独手术间打印
            if (obj.ID == "rb3")
            {
                SortFlag = "rb3";
                this.isSortByRoom = false;
                for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                {
                    if (obj.Memo == this.fpSpread1_Sheet1.Cells[i, (int)Cols.RoomID].Text)
                    {
                        this.fpSpread1_Sheet1.Cells[i, (int)Cols.isPrint].Value = true;
                    }
                    else
                    {
                        this.fpSpread1_Sheet1.Cells[i, (int)Cols.isPrint].Value = false;
                    }
                }
            }
            //单独病区打印
            if (obj.ID == "rb4")
            {
                SortFlag = "rb4";
                this.isSortByRoom = false;
                for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                {
                    if (obj.Memo == this.fpSpread1_Sheet1.Cells[i, (int)Cols.nurseID].Text)
                    {
                        this.fpSpread1_Sheet1.Cells[i, (int)Cols.isPrint].Value = true;
                    }
                    else
                    {
                        this.fpSpread1_Sheet1.Cells[i, (int)Cols.isPrint].Value = false;
                    }
                }
            }
            return 1;
        }
        public int Print()
        {
            if (this.arrangePrint == null)
            {
                this.arrangePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Operation.IArrangePrint)) as FS.HISFC.BizProcess.Interface.Operation.IArrangePrint;
                if (this.arrangePrint == null)
                {
                    MessageBox.Show("获得接口IArrangePrint错误，请与系统管理员联系。");

                    return -1;
                }
            }
            this.arrangePrint.Title = "手术安排一览表";
            this.arrangePrint.Date = this.date;
            this.arrangePrint.ArrangeType = FS.HISFC.BizProcess.Interface.Operation.EnumArrangeType.Operation;
            this.arrangePrint.Reset();
            //{2D9FB02D-2AE2-42ac-AD5A-E2B12DC841CE}feng.ch
            ArrayList applyList = new ArrayList();
            RoomSort sort = new RoomSort();
            RoomTableSort sorttable = new RoomTableSort();
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                //只打印安排的
                if (this.fpSpread1_Sheet1.Cells[i, (int)Cols.ArrangeNote].Text.Trim() == "" && this.fpSpread1_Sheet1.Cells[i, (int)Cols.RoomID].Text.Trim() != "")
                {
                    //{3B34DBA7-7740-46eb-93E7-5F2D7890B602}
                    if ((bool)this.fpSpread1_Sheet1.Cells[i, (int)Cols.isPrint].Value == true)
                    {
                        applyList.Add(this.UpdateData(i));
                    }
                }
                else
                {

                }

            }
            if (this.isSortByRoom)
            {
                applyList.Sort(sort);
                applyList.Sort(sorttable);
            }
            if (SortFlag == "rb3")
            {
                applyList.Sort(sort);
                applyList.Sort(sorttable);
            }
            if (SortFlag == "rb4")
            {
                applyList.Sort(sort);
                applyList.Sort(sorttable);
            }
            foreach (OperationAppllication apply in applyList)
            {
                this.arrangePrint.AddAppliction(apply);
            }
            //{3B34DBA7-7740-46eb-93E7-5F2D7890B602}
            if (this.arrangePrint == null)
            {
                return -1;
            }
            return this.arrangePrint.PrintPreview();

        }

        //全选
        public void AllSelect()
        {
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                this.fpSpread1_Sheet1.Cells[i, (int)Cols.isPrint].Value = true;
            }
        }

        //全部选
        public void AllNotSelect()
        {
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                this.fpSpread1_Sheet1.Cells[i, (int)Cols.isPrint].Value = false;
            }
        }

        /// <summary>
        /// 过滤
        /// </summary>
        public void SetFilter()
        {
            this.Filter = this.filter;
        }
        #endregion


        #region 事件
        protected override bool ProcessDialogKey(Keys keyData)
        {

            if (keyData == Keys.Enter)
            {
                #region enter
                if (fpSpread1.ContainsFocus)
                {
                    //洗手
                    if (fpSpread1_Sheet1.ActiveColumnIndex == (int)Cols.WNR)
                    {
                        if (lbNurse.Visible)
                            SelectNurse((int)Cols.WNR);

                        fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, (int)Cols.INR, false);
                    }
                    else if (fpSpread1_Sheet1.ActiveColumnIndex == (int)Cols.WNR2) //洗手护士2 
                    {
                        if (lbNurse.Visible)
                            SelectNurse((int)Cols.WNR2);
                        fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, (int)Cols.INR2, false);
                    }
                    //巡回
                    else if (fpSpread1_Sheet1.ActiveColumnIndex == (int)Cols.INR)
                    {
                        if (lbNurse.Visible)
                            SelectNurse((int)Cols.INR);
                        fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, (int)Cols.RoomID, false);
                    }
                    else if (fpSpread1_Sheet1.ActiveColumnIndex == (int)Cols.INR2) //巡回护士2 
                    {
                        if (lbNurse.Visible)
                            SelectNurse((int)Cols.INR2);
                        fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, (int)Cols.RoomID, false);
                    }
                    else if (fpSpread1_Sheet1.ActiveColumnIndex == (int)Cols.MAININR) //总巡护士 
                    {
                        if (lbNurse.Visible)
                            SelectNurse((int)Cols.MAININR);
                        fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, (int)Cols.RoomID, false);
                    }
                    else if (fpSpread1_Sheet1.ActiveColumnIndex == (int)Cols.ShiftNurse) //总巡护士 
                    {
                        if (lbNurse.Visible)
                            SelectNurse((int)Cols.ShiftNurse);
                        fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, (int)Cols.RoomID, false);
                    }
                    //房号
                    else if (fpSpread1_Sheet1.ActiveColumnIndex == (int)Cols.RoomID)
                    {
                        if (lbRoom.Visible) SelectRoom();
                        fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, (int)Cols.TableID, false);
                    }
                    //手术台
                    else if (fpSpread1_Sheet1.ActiveColumnIndex == (int)Cols.TableID)
                    {
                        if (lbTable.Visible) SelectTable();
                        if (fpSpread1_Sheet1.RowCount != fpSpread1_Sheet1.ActiveRowIndex + 1)
                        {
                            fpSpread1_Sheet1.ActiveRowIndex++;
                            fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, (int)Cols.opDate, true);
                            FarPoint.Win.Spread.LeaveCellEventArgs e = new FarPoint.Win.Spread.LeaveCellEventArgs
                                (new FarPoint.Win.Spread.SpreadView(fpSpread1), 0, 0,
                                fpSpread1_Sheet1.ActiveRowIndex, fpSpread1_Sheet1.ActiveColumnIndex);
                            fpSpread1_LeaveCell(fpSpread1, e);
                        }
                    }
                    else if (fpSpread1_Sheet1.ActiveColumnIndex == (int)Cols.opDate)
                    {
                        fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, (int)Cols.WNR, false);
                    }
                }
                #endregion
            }

            else if (keyData == Keys.Up)
            {

                //#region Up
                //if (fpSpread1.ContainsFocus)
                //{                  

                //    if (lbNurse.Visible)
                //    {                                       
                //        lbNurse.PriorRow();
                //    }
                //    else if (lbTable.Visible)
                //    {
                //        lbTable.PriorRow();

                //    }
                //    else if (lbRoom.Visible)
                //    {
                //        lbRoom.PriorRow();
                //    }
                //    else
                //    {
                //        int CurrentRow = fpSpread1_Sheet1.ActiveRowIndex;
                //        if (CurrentRow > 0)
                //        {
                //            fpSpread1_Sheet1.ActiveRowIndex = CurrentRow - 1;
                //            fpSpread1_Sheet1.AddSelection(CurrentRow - 1, 0, 1, 0);
                //            FarPoint.Win.Spread.LeaveCellEventArgs e = new FarPoint.Win.Spread.LeaveCellEventArgs
                //                (new FarPoint.Win.Spread.SpreadView(fpSpread1), 0, 0, CurrentRow - 1, fpSpread1_Sheet1.ActiveColumnIndex);
                //            fpSpread1_LeaveCell(fpSpread1, e);
                //        }
                //        //fpSpread1_Sheet1.ActiveRowIndex++;
                //    }
                //    return true;
                //}
                //#endregion
            }
            else if (keyData == Keys.Down)
            {
                //#region Down
                //if (fpSpread1.ContainsFocus)
                //{
                //    if (lbNurse.Visible)
                //    {
                //        lbNurse.NextRow();

                //    }
                //    else if (lbTable.Visible)
                //    {
                //        lbTable.NextRow();
                //    }
                //    else if (lbRoom.Visible)
                //    {
                //        lbRoom.NextRow();
                //    }
                //    else
                //    {
                //        int CurrentRow = fpSpread1_Sheet1.ActiveRowIndex;
                //        if (CurrentRow < fpSpread1_Sheet1.RowCount - 1)
                //        {
                //            fpSpread1_Sheet1.ActiveRowIndex = CurrentRow + 1;
                //            fpSpread1_Sheet1.AddSelection(CurrentRow + 1, 0, 1, 0);
                //            FarPoint.Win.Spread.LeaveCellEventArgs e = new FarPoint.Win.Spread.LeaveCellEventArgs
                //                (new FarPoint.Win.Spread.SpreadView(fpSpread1), 0, 0, CurrentRow + 1, fpSpread1_Sheet1.ActiveColumnIndex);
                //            fpSpread1_LeaveCell(fpSpread1, e);
                //        }
                //        fpSpread1_Sheet1.ActiveRowIndex--;

                //        //int CurrentRow = fpSpread1_Sheet1.ActiveRowIndex;
                //        //if (CurrentRow >= 0 && CurrentRow <= fpSpread1_Sheet1.RowCount - 2)
                //        //{
                //        //    fpSpread1_Sheet1.ActiveRowIndex = CurrentRow + 1;
                //        //    fpSpread1_Sheet1.AddSelection(CurrentRow + 1, 0, 1, 0);
                //        //}
                //    }
                //    return true;
                //}
                //#endregion
            }
            else if (keyData == Keys.Escape)
            {
                if (lbNurse.Visible)
                    lbNurse.Visible = false;
                if (lbTable.Visible)
                    lbTable.Visible = false;
                if (lbRoom.Visible)
                    lbRoom.Visible = false;
            }

            return base.ProcessDialogKey(keyData);
        }

        private void fpSpread1_EditModeOn(object sender, System.EventArgs e)
        {
            fpSpread1.EditingControl.KeyDown += new KeyEventHandler(EditingControl_KeyDown);
            SetLocation();

            if (fpSpread1_Sheet1.ActiveColumnIndex == (int)Cols.TableID)
            {
                string roomid = "";

                if (this.fpSpread1_Sheet1.GetTag(this.fpSpread1_Sheet1.ActiveRowIndex, (int)Cols.RoomID)
                    != null)
                {
                    roomid = (this.fpSpread1_Sheet1.GetTag(this.fpSpread1_Sheet1.ActiveRowIndex,
                        (int)Cols.RoomID) as NeuObject).ID;
                }
                else
                {
                    roomid = "no_room";
                }
                this.RefreshTableListBox(roomid);
            }
            try
            {
                if (fpSpread1_Sheet1.ActiveColumnIndex != (int)Cols.TableID)
                {
                    lbTable.Visible = false;
                }
                else
                {
                    lbTable.Visible = true;
                    //lbTable.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                }
                int ColumnIndex = fpSpread1_Sheet1.ActiveColumnIndex;
                if (ColumnIndex != (int)Cols.WNR && ColumnIndex != (int)Cols.INR && ColumnIndex != (int)Cols.WNR2 && ColumnIndex != (int)Cols.INR2 && ColumnIndex != (int)Cols.MAININR && ColumnIndex != (int)Cols.ShiftNurse)
                {
                    lbNurse.Visible = false;
                }
                else
                {
                    lbNurse.Visible = true;
                    lbNurse.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                }

                if (fpSpread1_Sheet1.ActiveColumnIndex != (int)Cols.RoomID)
                {
                    lbRoom.Visible = false;
                }
                else
                {
                    lbRoom.Visible = true;
                    lbRoom.Filter(fpSpread1_Sheet1.ActiveCell.Text);

                }
            }
            catch { }
        }

        private void fpSpread1_EditChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {

            string _Text;
            if (e.Column == (int)Cols.TableID)
            {
                _Text = fpSpread1_Sheet1.ActiveCell.Text;
                lbTable.Filter(_Text);

                if (lbTable.Visible == false) lbTable.Visible = true;
                fpSpread1_Sheet1.SetTag(e.Row, e.Column, null);
            }
            //洗手、循环护士
            else if (e.Column == (int)Cols.INR || e.Column == (int)Cols.INR2 || e.Column == (int)Cols.WNR2 ||
                e.Column == (int)Cols.WNR || e.Column == (int)Cols.MAININR || e.Column == (int)Cols.ShiftNurse)
            {
                _Text = fpSpread1_Sheet1.ActiveCell.Text;
                lbNurse.Filter(_Text);

                if (lbNurse.Visible == false)
                    lbNurse.Visible = true;
                fpSpread1_Sheet1.SetTag(e.Row, e.Column, null);
            }
            else if (e.Column == (int)Cols.RoomID)
            {
                _Text = fpSpread1_Sheet1.ActiveCell.Text;
                lbRoom.Filter(_Text);

                if (lbRoom.Visible == false) lbRoom.Visible = true;
                fpSpread1_Sheet1.SetTag(e.Row, e.Column, null);
            }
            //{A4764DE2-685E-4bb4-8B92-A77214904644}
            else if (e.Column == (int)Cols.anaeType)
            {
                _Text = fpSpread1_Sheet1.ActiveCell.Text;
                this.lbAnaetype.Filter(_Text);

                if (this.lbAnaetype.Visible == false) this.lbAnaetype.Visible = true;
                fpSpread1_Sheet1.SetTag(e.Row, e.Column, null);
            }
            else if (e.Column == (int)Cols.anaeType2)
            {
                // _Text = fpSpread1_Sheet1.ActiveCell.Text.Trim();
                _Text = fpSpread1_Sheet1.ActiveCell.Text;
                this.lbAnaetype.Filter(_Text);

                if (this.lbAnaetype.Visible == false) this.lbAnaetype.Visible = true;
                fpSpread1_Sheet1.SetTag(e.Row, e.Column, null);
            }
        }

        private void fpSpread1_EditModeOff(object sender, System.EventArgs e)
        {
            int col = this.fpSpread1_Sheet1.ActiveColumnIndex;
            if (col == (int)Cols.WNR || col == (int)Cols.INR || col == (int)Cols.WNR2 || col == (int)Cols.INR2)
            {
                //TODO: 刷新护士列表
                //this.RefreshNurseList(this.alTabulars);
            }
        }
        //左、右键实现光标在cell间跳转
        private void EditingControl_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Left)
            //{
            //    FarPoint.Win.Spread.CellType.GeneralEditor t = fpSpread1.EditingControl as FarPoint.Win.Spread.CellType.GeneralEditor;
            //    if (t.SelectionStart == 0 && t.SelectionLength == 0)
            //    {
            //        int row = 0, column = 0;
            //        if (fpSpread1_Sheet1.ActiveColumnIndex == 0 && fpSpread1_Sheet1.ActiveRowIndex != 0)
            //        {
            //            row = fpSpread1_Sheet1.ActiveRowIndex - 1;
            //            column = fpSpread1_Sheet1.Columns.Count - 1;
            //        }
            //        else if (fpSpread1_Sheet1.ActiveColumnIndex != 0)
            //        {
            //            row = fpSpread1_Sheet1.ActiveRowIndex;
            //            column = fpSpread1_Sheet1.ActiveColumnIndex - 1;
            //        }
            //        fpSpread1_Sheet1.SetActiveCell(row, column, true);
            //    }
            //}
            //if (e.KeyCode == Keys.Right)
            //{
            //    FarPoint.Win.Spread.CellType.GeneralEditor t = fpSpread1.EditingControl as FarPoint.Win.Spread.CellType.GeneralEditor;

            //    if (t.Text == null || t.Text == "" || t.SelectionStart == t.Text.Length)
            //    {
            //        int row = fpSpread1_Sheet1.RowCount - 1, column = fpSpread1_Sheet1.ColumnCount - 1;
            //        if (fpSpread1_Sheet1.ActiveColumnIndex == column && fpSpread1_Sheet1.ActiveRowIndex != row)
            //        {
            //            row = fpSpread1_Sheet1.ActiveRowIndex + 1;
            //            column = 0;
            //        }
            //        else if (fpSpread1_Sheet1.ActiveColumnIndex != column)
            //        {
            //            row = fpSpread1_Sheet1.ActiveRowIndex;
            //            column = fpSpread1_Sheet1.ActiveColumnIndex + 1;
            //        }
            //        fpSpread1_Sheet1.SetActiveCell(row, column, true);
            //    }
            //}
        }

        private void lbNurse_ItemSelected(object sender, System.EventArgs e)
        {
            this.SelectNurse(fpSpread1_Sheet1.ActiveColumnIndex);
        }
        private void lbTable_ItemSelected(object sender, System.EventArgs e)
        {
            SelectTable();

        }

        private void lbRoom_ItemSelected(object sender, System.EventArgs e)
        {
            SelectRoom();

        }
        #endregion

        #region columns
        private enum Cols
        {
            //{3B34DBA7-7740-46eb-93E7-5F2D7890B602}feng.ch
            /// <summary>
            /// 是否打印
            /// </summary>
            isPrint,//0

            nurseCell,//add by zhy//1
            /// <summary>
            /// 房号
            /// </summary>
            RoomID,//2
            /// <summary>
            /// 手术台
            /// </summary>
            TableID,//3
            /// <summary>
            /// 台序
            /// </summary>
            Order,//4
            /// <summary>
            /// 是否正台
            /// </summary>
            Desk,//5

            nurseID,//6
            bedID,//7
            patientNO,
            Name,//8
            Sex,//9
            Age,//10
            opDate,//11
            
          
            opItemName,//12
            /// <summary>
            /// 术前诊断
            /// </summary>
            foreDiagnose,//13
            /// <summary>
            /// 是否特殊手术
            /// </summary>
            isSpecial,//14

            opDoctID,//15
            helper1, //一助手二助手 16
            helper2,//17

            ansDocID,
            ansHelper1,

            /// <summary>
            /// 麻醉方式
            /// </summary>
            anaeWay,//18
            anaeType,//19
            anaeType2,//20

            /// <summary>
            /// 洗手护士
            /// </summary>
            WNR,//21
            /// <summary>
            /// 洗手护士2
            /// </summary>
            WNR2,//22

            /// <summary>
            /// 允许医生查看
            /// </summary>
            AllowDoctorView,//23

            /// <summary>
            /// 巡回护士
            /// </summary>
            INR,//24
            /// <summary>
            /// 巡回护士2 
            /// </summary>
            INR2,//29

            /// <summary>
            /// 总巡逻
            /// </summary>
            MAININR,//25

            /// <summary>
            /// 手术不安排的原因2 
            /// </summary>
            ArrangeNote,//26


            /// <summary>
            /// 接班人员
            /// </summary>
            ShiftNurse,//27

            /// <summary>
            /// 特殊说明
            /// </summary>
            specialNote,//28
            //{B9DDCC10-3380-4212-99E5-BB909643F11B}


         
        

            applyDate//手术申请时间 add by zhy//30
        }

        #endregion

        private void fpSpread1_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {
            //if (this.applictionSelected != null)
            //{
            //    this.applictionSelected(this, this.fpSpread1_Sheet1.Rows[e.Row].Tag as OperationAppllication);
            //}
        }


        #region IInterfaceContainer 成员

        public Type[] InterfaceTypes
        {
            get { return new Type[] { typeof(FS.HISFC.BizProcess.Interface.Operation.IArrangePrint) }; }
        }

        #endregion



        private void fpSpread1_CellClick(object sender, CellClickEventArgs e)
        {
            if (this.applictionSelected != null)
            {
                this.applictionSelected(this, this.fpSpread1_Sheet1.Rows[e.Row].Tag as OperationAppllication);
            }
        }


        /// <summary>
        /// 作废手术申请
        /// </summary>
        /// <returns></returns>
        public int CancelOperation()
        {
           
                int row = fpSpread1_Sheet1.ActiveRowIndex;
                if (!fpSpread1_Sheet1.RowHeader.Cells[row, 0].Text.Contains("申请作废"))
                {
                    MessageBox.Show("请选中有效的作废申请手术信息！");
                    return -1;
                }
                if (fpSpread1_Sheet1.RowCount == 0) return -1;

                //FS.HISFC.Object.Operator.OpsApplication apply = new FS.HISFC.Object.Operator.OpsApplication();
                FS.HISFC.Models.Operation.OperationAppllication apply = new OperationAppllication();
                apply = fpSpread1_Sheet1.Rows[row].Tag as FS.HISFC.Models.Operation.OperationAppllication;
                if (((DialogResult)MessageBox.Show("是否作废选中手术安排", "提示", MessageBoxButtons.YesNo)) == DialogResult.Yes)
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    Environment.OperationManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    if (Environment.OperationManager.CancelApplication(apply) < 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return -1;
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                    MessageBox.Show("作废成功");
                }
                return 1;
        }

        /// <summary>
        /// 完成手术
        /// //{2310330B-FDB2-42bf-9838-D52FA88CE529}
        /// </summary>
        /// <returns></returns>
        public int FinishOperation()
        {

            int row = fpSpread1_Sheet1.ActiveRowIndex;
            
            if (fpSpread1_Sheet1.RowCount == 0) return -1;

            //FS.HISFC.Object.Operator.OpsApplication apply = new FS.HISFC.Object.Operator.OpsApplication();
            FS.HISFC.Models.Operation.OperationAppllication apply = new OperationAppllication();
            apply = fpSpread1_Sheet1.Rows[row].Tag as FS.HISFC.Models.Operation.OperationAppllication;
            if (((DialogResult)MessageBox.Show("是否完成选中手术安排", "提示", MessageBoxButtons.YesNo)) == DialogResult.Yes)
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                Environment.OperationManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                //if (Environment.OperationManager.CancelApplication(apply) < 1)
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack();
                //    return -1;
                //}
                if (Environment.OperationManager.DoAnaeRecord(apply.ID, "4") < 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("更新收费标记失败！" + Environment.OperationManager.Err);
                    return -1;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("手术已完毕");
            }
            return 1;
        }



        /// <summary>
        ///更换手术室
        /// </summary>
        /// <returns></returns>
        public int ChangeDept()
        {

            try
            {
                int row = fpSpread1_Sheet1.ActiveRowIndex;
                if (fpSpread1_Sheet1.RowCount == 0) return -1;

                //FS.HISFC.Object.Operator.OpsApplication apply = new FS.HISFC.Object.Operator.OpsApplication();
                FS.HISFC.Models.Operation.OperationAppllication apply = new OperationAppllication();
                apply = fpSpread1_Sheet1.Rows[row].Tag as FS.HISFC.Models.Operation.OperationAppllication;
                string strOldOpsRoom = apply.OperateRoom.ID;//执行科室

                frmChangeOpsRoom dlg = new frmChangeOpsRoom(apply);
                //dlg.m_objOpsApp = apply;
                dlg.InitWin();
                DialogResult result = dlg.ShowDialog();
                //窗口点了“确定”按钮
                if (result == DialogResult.OK)
                {
                    //两个值不同表明更换了手术室
                    if (dlg.strNewOpsRoomID != strOldOpsRoom)
                    {
                        //刷新窗口的控件列表(通过更改查询截至时间的值触发相关事件达到这个目的)
                        //将更换了手术室的申请单从列表中消失显示。
                        //RefreshApplys();
                        dlg.Dispose();
                    }
                }
                else
                {
                    return -1;
                }
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        #region 排序
        /// <summary>
        /// 按照手术间排序
        /// </summary>
        class RoomSort : System.Collections.IComparer
        {
            public int Compare(object x, object y)
            {
                OperationAppllication o1 = x as OperationAppllication;
                OperationAppllication o2 = y as OperationAppllication;
                int str1 = o1.RoomID.Length;
                int str2 = o2.RoomID.Length;
                return str1 - str2;
            }
        }

        /// <summary>
        /// 按照手术台序排序
        /// </summary>
        class RoomTableSort : System.Collections.IComparer
        {
            public int Compare(object j, object k)
            {
                OperationAppllication o3 = j as OperationAppllication;
                OperationAppllication o4 = k as OperationAppllication;
                // int str3 = o3.OpsTable.ID; 
                // int str4 = o4.OpsTable.ID;
                int str3 = Int32.Parse(o3.OpsTable.ID);
                int str4 = Int32.Parse(o4.OpsTable.ID);
                return str3 - str4;
            }
        }
        #endregion


    }




}