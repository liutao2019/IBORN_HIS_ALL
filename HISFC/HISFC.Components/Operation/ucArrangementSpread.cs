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
    /// [��������: ��������������Ϣ]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2006-12-04]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
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

        #region �ֶ�

        public event ApplicationSelectedEventHandler applictionSelected;
        FS.HISFC.BizProcess.Integrate.Manager deptManager = new FS.HISFC.BizProcess.Integrate.Manager();

        FS.FrameWork.Management.DataBaseManger dataManager = new FS.FrameWork.Management.DataBaseManger();

        /// <summary>
        /// �����б�
        /// </summary>
        private ArrayList alApplys = new ArrayList();
        private ArrayList alRooms = new ArrayList();
        //private ArrayList alAnes;   //����ʽ�б�

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
        /// �Ƿ�������������
        /// </summary>
        bool isSortByRoom = false;

        string SortFlag = string.Empty;

        private FS.FrameWork.Public.ObjectHelper OperationOrderHelper = new FS.FrameWork.Public.ObjectHelper();
        #endregion

        #region ����
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
            /// ȫ��
            /// </summary>
            All,
            /// <summary>
            /// δ����
            /// </summary>
            NotYet,
            /// <summary>
            /// �Ѱ���
            /// </summary>
            Already
        }

        #endregion

        #region ����

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
            //{A4764DE2-685E-4bb4-8B92-A77214904644}feng.ch�������Ž�������޸�����ʽ
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
        /// ��ӻ�ʿlistbox�б�
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
            lbNurse.Font = new Font("����", 12);
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
            this.lbRoom.Font = new Font("����", 12);
            this.lbRoom.Size = new Size(220, 96);
            this.lbRoom.Hide();
            this.lbRoom.BorderStyle = BorderStyle.FixedSingle;
            this.lbRoom.BringToFront();
            this.lbRoom.ItemSelected += new System.EventHandler(this.lbRoom_ItemSelected);

        }
        //�����������б�
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
                    //��Ϊtable��û��ʵ��ISpell�ӿڣ����Խ���department��
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
            this.lbTable.Font = new Font("����", 12);
            this.lbTable.Size = new Size(220, 96);
            this.lbTable.Hide();
            this.lbTable.BorderStyle = BorderStyle.FixedSingle;
            this.lbTable.BringToFront();
            this.lbTable.ItemSelected += new System.EventHandler(this.lbTable_ItemSelected);

        }
        //�������̨listbox�б�
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
                    //��Ϊtable��û��ʵ��ISpell�ӿڣ����Խ���department��
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
        ///  ѡ��ʿ
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

        //ѡ�񷿺�
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

        //ѡ������̨
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
        /// ���û�ʿ������̨�б�λ��
        /// </summary>
        /// <returns></returns>
        private int SetLocation()
        {

            Control _cell = fpSpread1.EditingControl;
            if (_cell == null) return 0;

            //ϴ�֡�Ѳ�ػ�ʿ
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
        /// �������������Ϣ
        /// </summary>
        /// <param name="apply"></param>
        /// <returns></returns>
        public int AddOperationApplication(FS.HISFC.Models.Operation.OperationAppllication apply)
        {
            //���붯̬����
            this.alApplys.Add(apply);

            fpSpread1_Sheet1.Rows.Add(fpSpread1_Sheet1.RowCount, 1);
            int row = fpSpread1_Sheet1.RowCount - 1;


            FarPoint.Win.Spread.CellType.TextCellType txtType = new FarPoint.Win.Spread.CellType.TextCellType();
            txtType.StringTrim = System.Drawing.StringTrimming.EllipsisWord;
            txtType.ReadOnly = true;
            fpSpread1_Sheet1.Rows[row].Tag = apply;

            #region �����������������Ϣ������
            FS.HISFC.Models.Operation.OperationAppllication applicationTmp = Environment.OperationManager.QueryCancelApplyByAppID(apply.ID);
            if (applicationTmp != null && !string.IsNullOrEmpty(applicationTmp.ID))
            {
                fpSpread1_Sheet1.RowHeader.Cells[row,0].Text = "��������";
                fpSpread1_Sheet1.SetValue(row, (int)Cols.ArrangeNote, applicationTmp.Memo, false);
            }
            #endregion
            //��ʿվ
            if (deptManager == null)
                deptManager = new FS.HISFC.BizProcess.Integrate.Manager();

            FS.HISFC.Models.Base.Department dept = deptManager.GetDepartment(apply.PatientInfo.PVisit.PatientLocation.Dept.ID);
            apply.PatientInfo.PVisit.PatientLocation.Name = dept.Name;
            if (dept != null)
            {
                fpSpread1_Sheet1.SetValue(row, (int)Cols.nurseID, dept.Name, false);
            }

            //���� //���ﲻһ����
            FS.HISFC.Models.Base.Department nursecell = deptManager.GetDepartment(apply.PatientInfo.PVisit.PatientLocation.NurseCell.ID);
            if (nursecell != null)
            {
                apply.PatientInfo.PVisit.PatientLocation.NurseCell.Name = nursecell.Name;
                fpSpread1_Sheet1.SetValue(row, (int)Cols.nurseCell, nursecell.Name, false);
            }

            //���� add by zhy
            fpSpread1_Sheet1.SetValue(row, (int)Cols.bedID, apply.PatientInfo.PVisit.PatientLocation.Bed.Name, false);
            //סԺ��
            fpSpread1_Sheet1.SetValue(row, (int)Cols.patientNO, apply.PatientInfo.PID.PatientNO, false);
            //��������
            fpSpread1_Sheet1.SetValue(row, (int)Cols.Name, apply.PatientInfo.Name, false);
            //�Ա�
            fpSpread1_Sheet1.SetValue(row, (int)Cols.Sex, apply.PatientInfo.Sex.Name, false);
            //����
            int age = this.dataManager.GetDateTimeFromSysDateTime().Year - apply.PatientInfo.Birthday.Year;
            if (age == 0) age = 1;
            fpSpread1_Sheet1.SetValue(row, (int)Cols.Age, age.ToString(), false);
            fpSpread1_Sheet1.SetValue(row, (int)Cols.applyDate, apply.ApplyDate, false);
            //�Ƿ��Ѱ���
            if (apply.ExecStatus == "3"||apply.ExecStatus == "4")
            {
                fpSpread1_Sheet1.Cells[row, (int)Cols.Name].Note = "�Ѱ���";
                //this.fpSpread1_Sheet1.Rows[row].BackColor = System.Drawing.Color.LightBlue;
                this.fpSpread1_Sheet1.Rows[row].BackColor = System.Drawing.Color.LightPink;
            }
            else
            {
                fpSpread1_Sheet1.Cells[row, (int)Cols.Name].Note = "";
            }
            //̨��
            //fpSpread1_Sheet1.SetValue(row, (int)Cols.Order, apply.BloodUnit, false);
            fpSpread1_Sheet1.SetValue(row, (int)Cols.Order, OperationOrderHelper.GetObjectFromName(apply.BloodUnit).ID, false);
            //����̨����
            switch (apply.TableType)
            {
                case "1":
                    fpSpread1_Sheet1.SetValue(row, (int)Cols.Desk, "��̨", false);
                    break;
                case "2":
                    fpSpread1_Sheet1.SetValue(row, (int)Cols.Desk, "��̨", false);
                    break;
                case "3":
                    fpSpread1_Sheet1.SetValue(row, (int)Cols.Desk, "��̨", false);
                    break;
            }

            //����������            
            fpSpread1_Sheet1.SetValue(row, (int)Cols.opItemName, apply.MainOperationName, false);
            //��ǰ���     
            // fpSpread1_Sheet1.SetValue(row, (int)Cols.foreDiagnose, apply.DiagnoseAl[0], false);
            string forediagnose = string.Empty;

            for (int t = 0; t < apply.DiagnoseAl.Count; ++t)
            {
                forediagnose += "���" + (t + 1).ToString() + ":" + apply.DiagnoseAl[t].ToString() + "  ";
            }
            //forediagnose = apply.DiagnoseAl[0] + "\r\n" + apply.DiagnoseAl[1] + "\r\n" + apply.DiagnoseAl[2];
            fpSpread1_Sheet1.SetValue(row, (int)Cols.foreDiagnose, forediagnose, false);


            //�Ƿ���������
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
                    fpSpread1_Sheet1.SetValue(row, (int)Cols.isSpecial, "����", false);
                }
            }
            else
            {
                fpSpread1_Sheet1.SetValue(row, (int)Cols.isSpecial, "��", false);
            }

            //����˵��
            fpSpread1_Sheet1.SetValue(row, (int)Cols.specialNote, apply.ApplyNote, false);

            FS.FrameWork.Models.NeuObject obj = null;
            // FS.FrameWork.Models.NeuObject obj2 = null;
            string obj2 = string.Empty;


            //��������ʽ���� ����Ҳ������
            fpSpread1_Sheet1.Cells[row, (int)Cols.anaeWay].CellType = txtType;
            fpSpread1_Sheet1.Cells[row, (int)Cols.TableID].CellType = txtType;

            //����ʽ
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
                        apply.AnesType.Name = obj.Name + "��";
                    }
                    obj = Environment.GetAnes(apply.AnesType.ID.Substring(le + 1));
                    if (obj != null)
                    {
                        // obj2.Name = obj.Name;
                        obj2 = obj.Name;
                        fpSpread1_Sheet1.SetValue(row, (int)Cols.anaeType2, "��" + obj.Name, false);
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
                fpSpread1_Sheet1.Cells[row, (int)Cols.anaeType].Note = "δ��������";
            }

            //����ҽ��
            fpSpread1_Sheet1.SetValue(row, (int)Cols.opDoctID, apply.OperationDoctor.Name, false);
            //�Ƿ���
            if (apply.OperateKind == "2")
            {
                fpSpread1_Sheet1.RowHeader.Cells[row, 0].BackColor = Color.Red;
                fpSpread1_Sheet1.RowHeader.Cells[row, 0].Text = fpSpread1_Sheet1.RowHeader.Cells[row, 0].Text +  "������";
                //fpSpread1_Sheet1.RowHeader.Cells[row, 0].ForeColor = Color.Red;
            }
            //����{BD5A9D98-A2BA-4e14-9FE4-D8CF322E5B60}
            if (apply.PatientSouce == "1")
            {
                fpSpread1_Sheet1.RowHeader.Cells[row, 0].BackColor = Color.PeachPuff;
                fpSpread1_Sheet1.RowHeader.Cells[row, 0].Text = "����";
                fpSpread1_Sheet1.RowHeader.Cells[row, 0].ForeColor = Color.Red;
                //�Ƿ���
                if (apply.OperateKind == "2")
                {
                    fpSpread1_Sheet1.RowHeader.Cells[row, 0].BackColor = Color.Red;
                    fpSpread1_Sheet1.RowHeader.Cells[row, 0].Text = "���Ｑ";
                    fpSpread1_Sheet1.RowHeader.Cells[row, 0].ForeColor = Color.Red;
                }
            }
            fpSpread1_Sheet1.Cells[row, 0, row, 7].CellType = txtType;
            //{3B34DBA7-7740-46eb-93E7-5F2D7890B602}��ѡ���
            FarPoint.Win.Spread.CellType.CheckBoxCellType chkType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            fpSpread1_Sheet1.Cells[row, (int)Cols.isPrint].CellType = chkType;
            fpSpread1_Sheet1.Cells[row, (int)Cols.isPrint].Value = false;
            fpSpread1_Sheet1.Cells[row, (int)Cols.isPrint].Locked = false;
            //����ʱ��
            fpSpread1_Sheet1.SetValue(row, (int)Cols.opDate, apply.PreDate, false);
            #region ��ʿ
            if (apply.RoleAl != null && apply.RoleAl.Count != 0)
            {
                foreach (FS.HISFC.Models.Operation.ArrangeRole role in apply.RoleAl)
                {
                    #region  ����
                    //if (role.RoleType.ID.ToString() == EnumOperationRole.WashingHandNurse.ToString()) //ϴ�ֻ�ʿ                        
                    //{
                    //    if (fpSpread1_Sheet1.Cells[row, (int)Cols.WNR].Text == "") //��һϴ�ֻ�ʿ
                    //    {
                    //        fpSpread1_Sheet1.SetValue(row, (int)Cols.WNR, role.Name, false);
                    //        obj = (FS.FrameWork.Models.NeuObject)role;
                    //        fpSpread1_Sheet1.SetTag(row, (int)Cols.WNR, obj);
                    //    }
                    //    else
                    //    {   //�ڶ�ϴ�ֻ�ʿ
                    //        fpSpread1_Sheet1.SetValue(row, (int)Cols.WNR2, role.Name, false);
                    //        obj = (NeuObject)role;
                    //        fpSpread1_Sheet1.SetTag(row, (int)Cols.WNR2, obj);
                    //    }
                    //}
                    //else if (role.RoleType.ID.ToString() == EnumOperationRole.ItinerantNurse.ToString())//Ѳ�ػ�ʿ
                    //{
                    //    if (fpSpread1_Sheet1.Cells[row, (int)Cols.INR].Text == "")
                    //    {   //��һѲ�ػ�ʿ 
                    //        fpSpread1_Sheet1.SetValue(row, (int)Cols.INR, role.Name, false);
                    //        obj = (NeuObject)role;
                    //        fpSpread1_Sheet1.SetTag(row, (int)Cols.INR, obj);
                    //    }
                    //    else
                    //    {
                    //        //�ڶ�Ѳ�ػ�ʿ
                    //        fpSpread1_Sheet1.SetValue(row, (int)Cols.INR2, role.Name, false);
                    //        obj = (NeuObject)role;
                    //        fpSpread1_Sheet1.SetTag(row, (int)Cols.INR2, obj);
                    //    }

                    //}
                    #endregion
                    if (role.RoleType.ID.ToString() == EnumOperationRole.WashingHandNurse1.ToString()) //ϴ�ֻ�ʿ                        
                    {
                        //��һϴ�ֻ�ʿ
                        fpSpread1_Sheet1.SetValue(row, (int)Cols.WNR, role.Name, false);
                        obj = (FS.FrameWork.Models.NeuObject)role;
                        fpSpread1_Sheet1.SetTag(row, (int)Cols.WNR, obj);
                    }
                    else if (role.RoleType.ID.ToString() == EnumOperationRole.WashingHandNurse2.ToString())
                    {
                        //�ڶ�ϴ�ֻ�ʿ
                        fpSpread1_Sheet1.SetValue(row, (int)Cols.WNR2, role.Name, false);
                        obj = (NeuObject)role;
                        fpSpread1_Sheet1.SetTag(row, (int)Cols.WNR2, obj);
                    }
                    else if (role.RoleType.ID.ToString() == EnumOperationRole.ItinerantNurse1.ToString())//Ѳ�ػ�ʿ
                    {
                        //��һѲ�ػ�ʿ 
                        fpSpread1_Sheet1.SetValue(row, (int)Cols.INR, role.Name, false);
                        obj = (NeuObject)role;
                        fpSpread1_Sheet1.SetTag(row, (int)Cols.INR, obj);
                    }
                    else if (role.RoleType.ID.ToString() == EnumOperationRole.ItinerantNurse2.ToString())
                    {
                        //�ڶ�Ѳ�ػ�ʿ
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

                    //һ����
                    else if (role.RoleType.ID.ToString() == EnumOperationRole.Helper1.ToString())
                    {
                        fpSpread1_Sheet1.SetValue(row, (int)Cols.helper1, role.Name, false);
                        obj = (NeuObject)role;
                        fpSpread1_Sheet1.SetTag(row, (int)Cols.helper1, obj);
                    }
                    //������
                    else if (role.RoleType.ID.ToString() == EnumOperationRole.Helper2.ToString())
                    {
                        fpSpread1_Sheet1.SetValue(row, (int)Cols.helper2, role.Name, false);
                        obj = (NeuObject)role;
                        fpSpread1_Sheet1.SetTag(row, (int)Cols.helper2, obj);
                    }
                    //�Ӱ���Ա
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
            //������
            FarPoint.Win.Spread.CellType.NumberCellType numType = new FarPoint.Win.Spread.CellType.NumberCellType();
            numType.DecimalPlaces = 0;
            fpSpread1_Sheet1.Cells[row, (int)Cols.RoomID].CellType = numType;
            if (apply.RoomID != null && apply.RoomID != "")
            {

                obj = GetRoom(apply.RoomID);
                //fpSpread1_Sheet1.SetValue(row, (int)Cols.RoomID, Convert.ToDecimal(obj.Name), false); //ת��һ�£�Ϊ������
                fpSpread1_Sheet1.SetValue(row, (int)Cols.RoomID, obj.Name, false);
                fpSpread1_Sheet1.SetTag(row, (int)Cols.RoomID, obj);

                // fpSpread1_Sheet1.SetValue(row, (int)Cols.RoomID, apply.RoomID, false);
            }
            #region ����̨
            if (apply.OpsTable.ID != null && apply.OpsTable.ID != "")
            {
                fpSpread1_Sheet1.SetValue(row, (int)Cols.TableID, apply.OpsTable.Name, false);
                obj = new NeuObject();
                obj.ID = apply.OpsTable.ID;
                obj.Name = apply.OpsTable.Name;
                fpSpread1_Sheet1.SetTag(row, (int)Cols.TableID, obj);
            }



            //����һ�����������ŵ�ԭ�� add by zhy
            //fpSpread1_Sheet1.SetValue(row, (int)Cols.ArrangeNote, apply.Memo, false);

            #endregion

            return 0;
        }

        /// <summary>
        /// ���
        /// </summary>
        public void Reset()
        {
            this.fpSpread1_Sheet1.RowCount = 0;
            this.alApplys.Clear();
        }
        /// <summary>
        /// ��ȡ��������
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
            obj.Name = "��";
            return obj;
        }



        /// <summary>
        /// У������ 
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
                        MessageBox.Show("ϴ�ֻ�ʿ�����ظ�");
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
                        MessageBox.Show("Ѳ�ػ�ʿ�����ظ�");
                        return -1;
                    }
                }
                if (obj3 != null && obj5 != null)
                {
                    if (obj3.ID == obj5.ID)
                    {
                        MessageBox.Show("Ѳ�ػ�ʿ�����ظ�");
                        return -1;
                    }
                } if (obj4 != null && obj5 != null)
                {
                    if (obj4.ID == obj5.ID)
                    {
                        MessageBox.Show("Ѳ�ػ�ʿ�����ظ�");
                        return -1;
                    }
                }
                NeuObject tabRoom = fpSpread1_Sheet1.GetTag(row, (int)Cols.RoomID) as NeuObject;
                NeuObject tabTable = fpSpread1_Sheet1.GetTag(row, (int)Cols.TableID) as NeuObject;
                #region {42CDE890-24B3-4d6f-A52B-988F62E226B8}
                if (obj != null || obj2 != null || obj3 != null || obj4 != null || tabRoom != null || tabTable != null)
                {

                    //û��¼������̨��������
                    if (tabRoom == null)
                    {
                        MessageBox.Show("��ѡ����������");
                        return -1;
                    }

                    //û��¼������̨��������
                    if (tabTable == null)
                    {
                        MessageBox.Show("��ѡ������̨��");
                        return -1;
                    }
                }
                #endregion
            }
            return 0;
        }


        /// <summary>
        /// ��Ӹ��໤ʿ
        /// </summary>
        /// <param name="apply"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private int AddRole(OperationAppllication apply, int row)
        {
            ArrayList roles = new ArrayList();
            //����ջ�ʿ
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




            //���ϴ�ֻ�ʿ
            NeuObject obj = fpSpread1_Sheet1.GetTag(row, (int)Cols.WNR) as NeuObject;
            if (obj != null)
            {
                ArrangeRole role = new ArrangeRole(obj);
                role.RoleType.ID = EnumOperationRole.WashingHandNurse1;//��ɫ����
                role.OperationNo = apply.ID;
                role.ForeFlag = "0";//��ǰ����				
                roles.Add(role);//������Ա��ɫ����	
                //�ڶ�ϴ�ֻ�ʿ  ���Բ���
                if (fpSpread1_Sheet1.Cells[row, (int)Cols.WNR2].Tag != null)
                {
                    NeuObject obj2 = fpSpread1_Sheet1.GetTag(row, (int)Cols.WNR2) as NeuObject;
                    if (obj2 != null)
                    {
                        ArrangeRole role2 = new ArrangeRole(obj2);
                        role2.RoleType.ID = EnumOperationRole.WashingHandNurse2;//��ɫ����
                        role2.OperationNo = apply.ID;
                        role2.ForeFlag = "0";//��ǰ����				
                        roles.Add(role2);//������Ա��ɫ����	
                    }
                }
            }
            //���Ѳ�ػ�ʿ
            NeuObject obj3 = fpSpread1_Sheet1.GetTag(row, (int)Cols.INR) as NeuObject;
            if (obj3 != null)
            {
                ArrangeRole role = new ArrangeRole(obj3);
                role.RoleType.ID = EnumOperationRole.ItinerantNurse1;//��ɫ����
                role.OperationNo = apply.ID;
                role.ForeFlag = "0";//��ǰ����				
                roles.Add(role);//������Ա��ɫ����
                if (fpSpread1_Sheet1.Cells[row, (int)Cols.INR2].Tag != null)
                {
                    //��ӵڶ�Ѳ�ػ�ʿ
                    NeuObject obj4 = fpSpread1_Sheet1.GetTag(row, (int)Cols.INR2) as NeuObject;
                    if (obj4 != null)
                    {
                        ArrangeRole role2 = new ArrangeRole(obj4);
                        role2.RoleType.ID = EnumOperationRole.ItinerantNurse2; ;//��ɫ����
                        role2.OperationNo = apply.ID;
                        role2.ForeFlag = "0";//��ǰ����				
                        roles.Add(role2);//������Ա��ɫ����
                    }
                }
                if (fpSpread1_Sheet1.Cells[row, (int)Cols.MAININR].Tag != null)
                {
                    //�����Ѳ�ػ�ʿ
                    NeuObject obj5 = fpSpread1_Sheet1.GetTag(row, (int)Cols.MAININR) as NeuObject;
                    if (obj5 != null)
                    {
                        ArrangeRole role3 = new ArrangeRole(obj5);
                        role3.RoleType.ID = EnumOperationRole.ItinerantNurse3; ;//��ɫ����
                        role3.OperationNo = apply.ID;
                        role3.ForeFlag = "0";//��ǰ����				
                        roles.Add(role3);//������Ա��ɫ����
                    }
                }
            }
            //��ӽӰ���Ա
            NeuObject obj6 = fpSpread1_Sheet1.GetTag(row, (int)Cols.ShiftNurse) as NeuObject;
            if (obj6 != null)
            {
                ArrangeRole role4 = new ArrangeRole(obj6);
                role4.RoleType.ID = EnumOperationRole.OpsShiftNurse; //��ɫ����
                role4.OperationNo = apply.ID;
                role4.ForeFlag = "0";//��ǰ����
                roles.Add(role4);//������Ա��ɫ����
            }


            apply.RoleAl = roles;

            return 0;
        }


        /// <summary>
        /// ������໤ʿ����������������
        /// </summary>
        /// <param name="apply"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private int AddRoleUnArrange(OperationAppllication apply, int row)
        {
            ArrayList roles = new ArrayList();
            //����ջ�ʿ
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
        /// ����ʵ��İ��ű�־
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
        /// ��������
        /// </summary>
        private OperationAppllication UpdateData(int rowIndex)
        {
            NeuObject tab = fpSpread1_Sheet1.GetTag(rowIndex, (int)Cols.TableID) as NeuObject;
            //û��¼������̨��������
            if (tab == null)
                return null;

            OperationAppllication apply = fpSpread1_Sheet1.Rows[rowIndex].Tag as OperationAppllication;

            try
            {
                //�������̨
                OpsTable table = new OpsTable();
                table.ID = tab.ID;
                table.Name = tab.Name;
                apply.OpsTable = table;
                //�����������
                tab = fpSpread1_Sheet1.GetTag(rowIndex, (int)Cols.RoomID) as NeuObject;
                if (tab != null)
                {
                    apply.RoomID = tab.ID;
                    apply.OpePos.Memo = tab.Name;//add by chengym ��ӡ��
                }
                else
                {
                    fpSpread1_Sheet1.SetValue(rowIndex, (int)Cols.RoomID, "", false);
                }

                //����ʱ��
                string dt = fpSpread1_Sheet1.GetText(rowIndex, (int)Cols.opDate);
                //dt = apply.PreDate.Year.ToString() + "-" + apply.PreDate.Month.ToString() + "-" + apply.PreDate.Day.ToString() + " " + dt;
                apply.PreDate = DateTime.Parse(dt);
                //��Ӹ��໤ʿ
                this.AddRole(apply, rowIndex);

            }
            catch (Exception e)
            {

                return null;
            }

            return apply;
        }

        /// <summary>
        /// ������������״̬����ExecStatus=3���Ѱ���״̬��ΪExecStatus=6��������δ�շ�״̬����״̬ΪҽԺҪ�����
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
            //        MessageBox.Show("��������(" + Already.ID + ")״̬ʧ�ܣ�\n����ϵͳ����Ա��ϵ��" + Environment.OperationManager.Err, "��ʾ",
            //            MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return -1;
            //    }

            //    FS.FrameWork.Management.PublicTrans.Commit();
            //}
            //catch (Exception e)
            //{
            //    FS.FrameWork.Management.PublicTrans.RollBack();
            //    MessageBox.Show("��������(" + Already.ID + ")״̬ʧ��!" + e.Message, "��ʾ");
            //    return -1;
            //}

            for (int i = 0; i < fpSpread1_Sheet1.RowCount; i++)
            {
                //if ((bool)this.fpSpread1_Sheet1.Cells[i, (int)Cols.isPrint].Value == true)
                //{
                OperationAppllication AlreadyOriginal = fpSpread1_Sheet1.Rows[i].Tag as OperationAppllication;
                if (AlreadyOriginal == null)
                {
                    MessageBox.Show("ʵ��ת������");
                    return -1;
                }
                OperationAppllication already = Environment.OperationManager.GetOpsApp(AlreadyOriginal.ID);
                if (already == null)
                {
                    MessageBox.Show("��ȡ������Ϣ����");
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
                                    MessageBox.Show("��������(" + already.ID + ")״̬ʧ�ܣ�\n����ϵͳ����Ա��ϵ��" + Environment.OperationManager.Err, "��ʾ",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return -1;
                                }

                                FS.FrameWork.Management.PublicTrans.Commit();
                                MessageBox.Show("�����Ǽǳɹ���", "��ʾ");
                                //}
                                //else if (already.IsAnesth == false)
                                //{
                                //    MessageBox.Show("����δ����������Ǽǣ�","��ʾ");
                                //}

                            }
                        }
                        else
                        {
                            MessageBox.Show("����δ���ţ�������Ǽǣ�", "��ʾ");
                            continue;
                        }

                    }
                    catch (Exception e)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("��������(" + already.ID + ")״̬ʧ��!" + e.Message, "��ʾ");
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
            //    MessageBox.Show("��ѡ���ߣ�");
            //}

            //}

            return 0;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            if (this.ValueState() == -1)
            {
                return -1;

            }

            //���ݿ�����
            //FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);


            List<int> succeed = new List<int>();        //�ɹ����ŵ�
            for (int i = 0; i < fpSpread1_Sheet1.RowCount; i++)
            {
                if ((bool)this.fpSpread1_Sheet1.Cells[i, (int)Cols.isPrint].Value == true)
                {
                    if (this.fpSpread1_Sheet1.Cells[i, (int)Cols.ArrangeNote].Text.Trim() == "")
                    {
                        NeuObject tab = fpSpread1_Sheet1.GetTag(i, (int)Cols.TableID) as NeuObject;
                        //{A4764DE2-685E-4bb4-8B92-A77214904644}
                        //����ʽ
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


                        //û��¼������̨��������
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
                            MessageBox.Show("ʵ��ת������");
                            return -1;
                        }
                        OperationAppllication apply = Environment.OperationManager.GetOpsApp(applyOriginal.ID);
                        if (apply == null)
                        {
                            MessageBox.Show("��ȡ������Ϣ����");
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
                            //�������̨
                            OpsTable table = new OpsTable();
                            //�������ʽ������ʵ��
                            //{A4764DE2-685E-4bb4-8B92-A77214904644}
                            if (type != null)
                            {
                                apply.AnesType = type;
                            }
                            table.ID = tab.ID;
                            table.Name = tab.Name;
                            apply.OpsTable = table;
                            //�����������
                            tab = fpSpread1_Sheet1.GetTag(i, (int)Cols.RoomID) as NeuObject;
                            if (tab != null)
                                apply.RoomID = tab.ID;
                            else
                                fpSpread1_Sheet1.SetValue(i, (int)Cols.RoomID, "", false);

                            //����ʱ��
                            string dt = fpSpread1_Sheet1.GetText(i, (int)Cols.opDate);
                            //dt = apply.PreDate.Year.ToString() + "-" + apply.PreDate.Month.ToString() + "-" + apply.PreDate.Day.ToString() + " " + dt;
                            apply.PreDate = DateTime.Parse(dt);
                            //��Ӹ��໤ʿ
                            this.AddRole(apply, i);
                            //��־Ϊ�Ѱ���
                            apply.ExecStatus = "3";
                            apply.Memo = this.fpSpread1_Sheet1.Cells[i, (int)Cols.ArrangeNote].Text.Trim();

                            if (Environment.OperationManager.UpdateApplication(apply) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("��������(" + apply.ID + ")������Ϣʧ�ܣ�\n����ϵͳ����Ա��ϵ��" + Environment.OperationManager.Err, "��ʾ",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return -1;
                            }
                            succeed.Add(i);
                            FS.FrameWork.Management.PublicTrans.Commit();
                        }
                        catch (Exception e)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("��������(" + apply.ID + ")������Ϣ����!" + e.Message, "��ʾ");
                            return -1;
                        }
                        //���½�����ʾ
                        fpSpread1_Sheet1.Rows[i].Tag = apply;
                        fpSpread1_Sheet1.Cells[i, (int)Cols.Name].Note = "�Ѱ���";
                        this.fpSpread1_Sheet1.Rows[i].BackColor = System.Drawing.Color.LightPink;
                        this.UpdateFlag(apply);
                    }
                    else
                    {

                        //{3DC153BD-1E9B-40c4-AAFC-3C27607A8945}
                        OperationAppllication applyOriginal = fpSpread1_Sheet1.Rows[i].Tag as OperationAppllication;
                        if (applyOriginal == null)
                        {
                            MessageBox.Show("ʵ��ת������");
                            return -1;
                        }
                        OperationAppllication apply = Environment.OperationManager.GetOpsApp(applyOriginal.ID);
                        if (apply == null)
                        {
                            MessageBox.Show("��ȡ������Ϣ����");
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
                            //��־Ϊ�����ţ�����������״̬
                            apply.ExecStatus = "1";
                            apply.Memo = this.fpSpread1_Sheet1.Cells[i, (int)Cols.ArrangeNote].Text.Trim();

                            if (Environment.OperationManager.UpdateApplication(apply) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("��������(" + apply.ID + ")������Ϣʧ�ܣ�\n����ϵͳ����Ա��ϵ��" + Environment.OperationManager.Err, "��ʾ",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return -1;
                            }
                            succeed.Add(i);
                            FS.FrameWork.Management.PublicTrans.Commit();
                        }
                        catch (Exception e)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("��������(" + apply.ID + ")������Ϣ����!" + e.Message, "��ʾ");
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
                MessageBox.Show(string.Format("�������ųɹ���", line), "��ʾ");
                fpSpread1.Focus();
                if (lbTable != null)
                {
                    lbTable.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("û��¼�밲����Ϣ�����豣��");
            }

            return 0;
        }
        //{2D9FB02D-2AE2-42ac-AD5A-E2B12DC841CE}feng.ch
        /// <summary>
        /// ���ô�ӡģʽ
        /// </summary>
        /// <param name="obj">����ʵ��</param>
        /// <returns></returns>
        public int SetPrintMode(FS.FrameWork.Models.NeuObject obj)
        {
            //Ĭ�ϴ�ӡģʽ��ȫ��ѡ��
            if (obj.ID == "rb1")
            {
                SortFlag = "rb1";
                this.isSortByRoom = false;
                for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                {
                    this.fpSpread1_Sheet1.Cells[i, (int)Cols.isPrint].Value = true;
                }
            }
            //����������˳���ӡ
            if (obj.ID == "rb2")
            {
                SortFlag = "rb2";
                this.isSortByRoom = true;
                for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                {
                    this.fpSpread1_Sheet1.Cells[i, (int)Cols.isPrint].Value = true;
                }
            }
            //�����������ӡ
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
            //����������ӡ
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
                    MessageBox.Show("��ýӿ�IArrangePrint��������ϵͳ����Ա��ϵ��");

                    return -1;
                }
            }
            this.arrangePrint.Title = "��������һ����";
            this.arrangePrint.Date = this.date;
            this.arrangePrint.ArrangeType = FS.HISFC.BizProcess.Interface.Operation.EnumArrangeType.Operation;
            this.arrangePrint.Reset();
            //{2D9FB02D-2AE2-42ac-AD5A-E2B12DC841CE}feng.ch
            ArrayList applyList = new ArrayList();
            RoomSort sort = new RoomSort();
            RoomTableSort sorttable = new RoomTableSort();
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                //ֻ��ӡ���ŵ�
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

        //ȫѡ
        public void AllSelect()
        {
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                this.fpSpread1_Sheet1.Cells[i, (int)Cols.isPrint].Value = true;
            }
        }

        //ȫ��ѡ
        public void AllNotSelect()
        {
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                this.fpSpread1_Sheet1.Cells[i, (int)Cols.isPrint].Value = false;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public void SetFilter()
        {
            this.Filter = this.filter;
        }
        #endregion


        #region �¼�
        protected override bool ProcessDialogKey(Keys keyData)
        {

            if (keyData == Keys.Enter)
            {
                #region enter
                if (fpSpread1.ContainsFocus)
                {
                    //ϴ��
                    if (fpSpread1_Sheet1.ActiveColumnIndex == (int)Cols.WNR)
                    {
                        if (lbNurse.Visible)
                            SelectNurse((int)Cols.WNR);

                        fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, (int)Cols.INR, false);
                    }
                    else if (fpSpread1_Sheet1.ActiveColumnIndex == (int)Cols.WNR2) //ϴ�ֻ�ʿ2 
                    {
                        if (lbNurse.Visible)
                            SelectNurse((int)Cols.WNR2);
                        fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, (int)Cols.INR2, false);
                    }
                    //Ѳ��
                    else if (fpSpread1_Sheet1.ActiveColumnIndex == (int)Cols.INR)
                    {
                        if (lbNurse.Visible)
                            SelectNurse((int)Cols.INR);
                        fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, (int)Cols.RoomID, false);
                    }
                    else if (fpSpread1_Sheet1.ActiveColumnIndex == (int)Cols.INR2) //Ѳ�ػ�ʿ2 
                    {
                        if (lbNurse.Visible)
                            SelectNurse((int)Cols.INR2);
                        fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, (int)Cols.RoomID, false);
                    }
                    else if (fpSpread1_Sheet1.ActiveColumnIndex == (int)Cols.MAININR) //��Ѳ��ʿ 
                    {
                        if (lbNurse.Visible)
                            SelectNurse((int)Cols.MAININR);
                        fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, (int)Cols.RoomID, false);
                    }
                    else if (fpSpread1_Sheet1.ActiveColumnIndex == (int)Cols.ShiftNurse) //��Ѳ��ʿ 
                    {
                        if (lbNurse.Visible)
                            SelectNurse((int)Cols.ShiftNurse);
                        fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, (int)Cols.RoomID, false);
                    }
                    //����
                    else if (fpSpread1_Sheet1.ActiveColumnIndex == (int)Cols.RoomID)
                    {
                        if (lbRoom.Visible) SelectRoom();
                        fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, (int)Cols.TableID, false);
                    }
                    //����̨
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
            //ϴ�֡�ѭ����ʿ
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
                //TODO: ˢ�»�ʿ�б�
                //this.RefreshNurseList(this.alTabulars);
            }
        }
        //���Ҽ�ʵ�ֹ����cell����ת
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
            /// �Ƿ��ӡ
            /// </summary>
            isPrint,//0

            nurseCell,//add by zhy//1
            /// <summary>
            /// ����
            /// </summary>
            RoomID,//2
            /// <summary>
            /// ����̨
            /// </summary>
            TableID,//3
            /// <summary>
            /// ̨��
            /// </summary>
            Order,//4
            /// <summary>
            /// �Ƿ���̨
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
            /// ��ǰ���
            /// </summary>
            foreDiagnose,//13
            /// <summary>
            /// �Ƿ���������
            /// </summary>
            isSpecial,//14

            opDoctID,//15
            helper1, //һ���ֶ����� 16
            helper2,//17

            ansDocID,
            ansHelper1,

            /// <summary>
            /// ����ʽ
            /// </summary>
            anaeWay,//18
            anaeType,//19
            anaeType2,//20

            /// <summary>
            /// ϴ�ֻ�ʿ
            /// </summary>
            WNR,//21
            /// <summary>
            /// ϴ�ֻ�ʿ2
            /// </summary>
            WNR2,//22

            /// <summary>
            /// ����ҽ���鿴
            /// </summary>
            AllowDoctorView,//23

            /// <summary>
            /// Ѳ�ػ�ʿ
            /// </summary>
            INR,//24
            /// <summary>
            /// Ѳ�ػ�ʿ2 
            /// </summary>
            INR2,//29

            /// <summary>
            /// ��Ѳ��
            /// </summary>
            MAININR,//25

            /// <summary>
            /// ���������ŵ�ԭ��2 
            /// </summary>
            ArrangeNote,//26


            /// <summary>
            /// �Ӱ���Ա
            /// </summary>
            ShiftNurse,//27

            /// <summary>
            /// ����˵��
            /// </summary>
            specialNote,//28
            //{B9DDCC10-3380-4212-99E5-BB909643F11B}


         
        

            applyDate//��������ʱ�� add by zhy//30
        }

        #endregion

        private void fpSpread1_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {
            //if (this.applictionSelected != null)
            //{
            //    this.applictionSelected(this, this.fpSpread1_Sheet1.Rows[e.Row].Tag as OperationAppllication);
            //}
        }


        #region IInterfaceContainer ��Ա

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
        /// ������������
        /// </summary>
        /// <returns></returns>
        public int CancelOperation()
        {
           
                int row = fpSpread1_Sheet1.ActiveRowIndex;
                if (!fpSpread1_Sheet1.RowHeader.Cells[row, 0].Text.Contains("��������"))
                {
                    MessageBox.Show("��ѡ����Ч����������������Ϣ��");
                    return -1;
                }
                if (fpSpread1_Sheet1.RowCount == 0) return -1;

                //FS.HISFC.Object.Operator.OpsApplication apply = new FS.HISFC.Object.Operator.OpsApplication();
                FS.HISFC.Models.Operation.OperationAppllication apply = new OperationAppllication();
                apply = fpSpread1_Sheet1.Rows[row].Tag as FS.HISFC.Models.Operation.OperationAppllication;
                if (((DialogResult)MessageBox.Show("�Ƿ�����ѡ����������", "��ʾ", MessageBoxButtons.YesNo)) == DialogResult.Yes)
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    Environment.OperationManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    if (Environment.OperationManager.CancelApplication(apply) < 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return -1;
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                    MessageBox.Show("���ϳɹ�");
                }
                return 1;
        }

        /// <summary>
        /// �������
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
            if (((DialogResult)MessageBox.Show("�Ƿ����ѡ����������", "��ʾ", MessageBoxButtons.YesNo)) == DialogResult.Yes)
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
                    MessageBox.Show("�����շѱ��ʧ�ܣ�" + Environment.OperationManager.Err);
                    return -1;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("���������");
            }
            return 1;
        }



        /// <summary>
        ///����������
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
                string strOldOpsRoom = apply.OperateRoom.ID;//ִ�п���

                frmChangeOpsRoom dlg = new frmChangeOpsRoom(apply);
                //dlg.m_objOpsApp = apply;
                dlg.InitWin();
                DialogResult result = dlg.ShowDialog();
                //���ڵ��ˡ�ȷ������ť
                if (result == DialogResult.OK)
                {
                    //����ֵ��ͬ����������������
                    if (dlg.strNewOpsRoomID != strOldOpsRoom)
                    {
                        //ˢ�´��ڵĿؼ��б�(ͨ�����Ĳ�ѯ����ʱ���ֵ��������¼��ﵽ���Ŀ��)
                        //�������������ҵ����뵥���б�����ʧ��ʾ��
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

        #region ����
        /// <summary>
        /// ��������������
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
        /// ��������̨������
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