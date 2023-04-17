using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;
using FS.HISFC.Models.Operation;

namespace FS.HISFC.Components.Operation
{
    /// <summary>
    /// [��������: �����ű��ؼ�]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2006-12-11]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucAnaesthesiaSpread : UserControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucAnaesthesiaSpread()
        {
            InitializeComponent();
            if (!Environment.DesignMode)
            {
                this.Init();
                this.InitRoomListBox();
            }
            this.fpSpread1.EditModeOff += new EventHandler(fpSpread1_EditModeOff);
            this.fpSpread1.EditModeOn +=new EventHandler(fpSpread1_EditModeOn);
        }

        void fpSpread1_EditModeOff(object sender, EventArgs e)
        {
            this.SetLocation();
        }

        #region �ֶ�
        /// <summary>
        /// ������Ա�б�
        /// </summary>
        private FS.FrameWork.WinForms.Controls.NeuListBoxPopup lbDoctor = new FS.FrameWork.WinForms.Controls.NeuListBoxPopup();
        private FS.FrameWork.WinForms.Controls.NeuListBoxPopup lbAnaetype = new FS.FrameWork.WinForms.Controls.NeuListBoxPopup();
        private FS.FrameWork.WinForms.Controls.NeuListBoxPopup lbRoom = new FS.FrameWork.WinForms.Controls.NeuListBoxPopup();
        FS.FrameWork.Public.ObjectHelper anaeTypeHelper = new FS.FrameWork.Public.ObjectHelper();
        private FS.HISFC.BizProcess.Interface.Operation.IArrangePrint arrangePrint;
        private DateTime date;

        public event ApplicationSelectedEventHandler applictionSelected;

        //{B9DDCC10-3380-4212-99E5-BB909643F11B}
        FS.FrameWork.Public.ObjectHelper anneObjectHelper = new FS.FrameWork.Public.ObjectHelper();

        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        FS.HISFC.BizLogic.Manager.DepartmentStatManager deptStatManager = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();

        /// <summary>
        /// �Ƿ�������������
        /// </summary>
        bool isSortByRoom = false;

        private ArrayList alRooms = new ArrayList();

        #endregion

        #region ����
        public DateTime Date
        {
            set
            {
                this.date = value;
            }
        }

        private string anaesDocDept = string.Empty;
        /// <summary>
        /// ����ҽ�����ڿ���
        /// </summary>
        public string AnaesDocDept
        {
            set { this.anaesDocDept = value;
            this.RefreshEmployeeList();
            }
        }

        private EnumFilter filter = EnumFilter.NotYet;

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
                        if ((this.fpSpread1_Sheet1.Rows[i].Tag as OperationAppllication).IsAnesth)
                            this.fpSpread1_Sheet1.Rows[i].Visible = false;
                        else
                            this.fpSpread1_Sheet1.Rows[i].Visible = true;
                    }
                }
                else
                {
                    for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                    {
                        if ((this.fpSpread1_Sheet1.Rows[i].Tag as OperationAppllication).IsAnesth)
                            this.fpSpread1_Sheet1.Rows[i].Visible = true;
                        else
                            this.fpSpread1_Sheet1.Rows[i].Visible = false;
                    }
                }
            }
        }
        #endregion


        #region ����

        private void Init()
        {
            this.fpSpread1.SetInputMap();
            this.RefreshEmployeeList();
            this.InitTypeList();
            this.fpSpread1.AddListBoxPopup(this.lbDoctor, (int)Cols.anaeDoct);
            this.fpSpread1.AddListBoxPopup(this.lbDoctor, (int)Cols.anaeHelper);
            this.fpSpread1.AddListBoxPopup(this.lbDoctor, (int)Cols.anaeHelper1);
            this.fpSpread1.AddListBoxPopup(this.lbDoctor, (int)Cols.anaeHelper2);
            //this.fpSpread1.AddListBoxPopup(this.lbAnaetype, (int)Cols.anaeType);
            this.fpSpread1.AddListBoxPopup(this.lbAnaetype, (int)Cols.anaeType2);
            this.fpSpread1.AddListBoxPopup(this.lbDoctor, (int)Cols.ShiftDoctor);

            //{B9DDCC10-3380-4212-99E5-BB909643F11B}
           System.Collections.ArrayList al= this.managerIntegrate.GetConstantList(EnumConstant.ANESWAY);
           this.anneObjectHelper.ArrayObject = al;

          
            
        }

        //ȫѡ
        public void AllSelect(bool isSelect)
        {
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                this.fpSpread1_Sheet1.Cells[i, (int)Cols.isPrint].Value = isSelect;
            }
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
            //this.lbRoom.ItemSelected += new System.EventHandler(this.lbRoom_ItemSelected);

        }

        //�����������б�
        private void RefreshRoomListBox()
        {
            ArrayList al = new ArrayList();

            ArrayList alOperationDepts = deptStatManager.LoadByChildren("10", Environment.OperatorDeptID);

            string operationDept = string.Empty;
            foreach (FS.HISFC.Models.Base.DepartmentStat deptStat in alOperationDepts)
            {
                operationDept = deptStat.PardepCode;
            }

            ArrayList rooms = Environment.TableManager.GetRoomsByDept(operationDept);
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


        //ˢ�±�����Ա�б�
        private int RefreshEmployeeList()
        {
            //treeView2.Nodes.Clear();
            //TreeNode root = new TreeNode();
            //root.Text = "������Ա";
            //root.ImageIndex = 22;
            //root.SelectedImageIndex = 22;
            //treeView2.Nodes.Add(root);

            //��ȡ������Ա��
            //MessageBox.Show(EnumEmployeeType.D.ToString(),Environment.OperatorDeptID);
            ArrayList persons = new ArrayList();

            if (this.anaesDocDept == string.Empty)
            {
                 persons = Environment.IntegrateManager.QueryEmployee(EnumEmployeeType.D, Environment.OperatorDeptID);
                //��ЩҽԺ����ҽ����������ƣ������������ң�һ�㻤ʿ��½�������ң����»�ȡ��������ҽ��2012-7-11 chengym
            }
            else
            {
                if (persons == null || persons.Count == 0)
                {
                    persons = Environment.IntegrateManager.QueryEmployee(EnumEmployeeType.D, this.anaesDocDept);
                }
            }
            //ArrayList persons = Environment.IntegrateManager.QueryEmployeeAll();
            //��ӵ������б�
            //if (persons != null)
            //{
            //    foreach (FS.HISFC.Models.Base.Employee person in persons)
            //    {
            //        TreeNode node = new TreeNode();
            //        node.Tag = person;
            //        node.Text = "[" + person.ID + "]" + person.Name;
            //        node.ImageIndex = 25;
            //        node.SelectedImageIndex = 25;
            //        root.Nodes.Add(node);
            //    }
            //}
            //root.Expand();
            //����ҽ��listbox�б�
            this.InitDoctListBox(persons);
            persons = null;

            return 0;
        }

        /// <summary>
        /// ���ҽ��listbox�б�
        /// </summary>
        /// <param name="doctors"></param>
        /// <returns></returns>
        private int InitDoctListBox(ArrayList doctors)
        {
            //ArrayList al = new ArrayList();
            //if (doctors != null)
            //{
            //    foreach (FS.HISFC.Models.Base.Employee doctor in doctors)
            //    {
            //        FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            //        obj = (FS.FrameWork.Models.NeuObject)doctor;
            //        al.Add(obj);
            //    }
            //}
            lbDoctor.AddItems(doctors);

            //this.Controls.Add(this.lbDoctor);
            //this.lbDoctor.Visible = false;
            //lbDoctor.BorderStyle = BorderStyle.FixedSingle;
            //lbDoctor.BringToFront();
            //lbDoctor.ItemSelected += new EventHandler(lbDoctor_ItemSelected);
            return 0;
        }

        /// <summary>
        /// ������������б�
        /// </summary>
        /// <returns></returns>
        private int InitTypeList()
        {
            ArrayList types = Environment.IntegrateManager.GetConstantList("CASEANESTYPE");//(FS.HISFC.Models.Base.EnumConstant.ANESTYPE);

            lbAnaetype.AddItems(types);
            anaeTypeHelper.ArrayObject = types;
            //this.Controls.Add(this.lbAnaetype);
            //this.lbAnaetype.Visible = false;
            //this.lbAnaetype.BorderStyle = BorderStyle.FixedSingle;
            //this.lbAnaetype.BringToFront();
            //this.lbAnaetype.ItemSelected+=new EventHandler(lbAnaetype_ItemSelected);
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

            //���֡�����
            if (fpSpread1_Sheet1.ActiveColumnIndex == (int)Cols.anaeDoct ||
                fpSpread1_Sheet1.ActiveColumnIndex == (int)Cols.anaeHelper)
            {
                lbDoctor.Location = new Point(_cell.Location.X,
                    _cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2);
                lbDoctor.Size = new Size(110, 150);
            }
            else if (fpSpread1_Sheet1.ActiveColumnIndex == (int)Cols.anaeType)
            {
                if (_cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2 +150 < this.fpSpread1.Height)
                {
                    lbAnaetype.Location = new Point(_cell.Location.X,
                    _cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2);

                }
                else
                {
                    lbAnaetype.Location = new Point(_cell.Location.X,
                    _cell.Location.Y - 150);
                }
                lbAnaetype.Size = new Size(110, 150);

            }
            return 0;
        }
        /// <summary>
        /// ѡ��ҽ��
        /// </summary>
        /// <param name="Column"></param>
        /// <returns></returns>
        //private int SelectDoctor(int Column)
        //{
        //    int CurrentRow = fpSpread1_Sheet1.ActiveRowIndex;
        //    if (CurrentRow < 0) return 0;

        //    fpSpread1.StopCellEditing();
        //    NeuObject item = null;
        //    item = lbDoctor.GetSelectedItem();

        //    if (item == null) return -1;

        //    fpSpread1_Sheet1.Cells[CurrentRow, Column].Tag = item;
        //    fpSpread1_Sheet1.SetValue(CurrentRow, Column, item.Name, false);

        //    lbDoctor.Visible = false;

        //    return 0;
        //}

        /// <summary>
        /// ѡ����������
        /// </summary>
        /// <returns></returns>
        //private int SelectType()
        //{
        //    int CurrentRow = fpSpread1_Sheet1.ActiveRowIndex;
        //    if (CurrentRow < 0) return 0;

        //    fpSpread1.StopCellEditing();
        //    NeuObject item = null;
        //    item = lbAnaetype.GetSelectedItem();

        //    if (item == null) return -1;

        //    fpSpread1_Sheet1.Cells[CurrentRow, (int)Cols.anaeType].Tag= item;
        //    fpSpread1_Sheet1.SetValue(CurrentRow, (int)Cols.anaeType, item.Name, false);

        //    lbAnaetype.Visible = false;

        //    return 0;
        //}
        /// <summary>
        /// �������������Ϣ
        /// </summary>
        /// <param name="apply"></param>
        /// <returns></returns>
        public int AddOperationApplication(FS.HISFC.Models.Operation.OperationAppllication apply)
        {
            this.fpSpread1_Sheet1.Rows.Add(fpSpread1_Sheet1.RowCount, 1);
            int row = fpSpread1_Sheet1.RowCount - 1;

            FarPoint.Win.Spread.CellType.TextCellType txtType = new FarPoint.Win.Spread.CellType.TextCellType();
            txtType.StringTrim = System.Drawing.StringTrimming.EllipsisWord;
            txtType.ReadOnly = true;
            fpSpread1_Sheet1.Rows[row].Tag = apply;

            //����
            Department nursecell = Environment.IntegrateManager.GetDepartment(apply.PatientInfo.PVisit.PatientLocation.NurseCell.ID);
            if (nursecell != null)
            {
                fpSpread1_Sheet1.SetValue(row, (int)Cols.nurseCell, nursecell.Name, false);
            }

            //����            
            Department dept = Environment.IntegrateManager.GetDepartment(apply.PatientInfo.PVisit.PatientLocation.Dept.ID);
            if (dept != null)
            {
                fpSpread1_Sheet1.SetValue(row, (int)Cols.nurseID, dept.Name, false);
            }
            //����
            fpSpread1_Sheet1.SetValue(row, (int)Cols.bedID, apply.PatientInfo.PVisit.PatientLocation.Bed.Name, false);
            //��������
            fpSpread1_Sheet1.SetValue(row, (int)Cols.Name, apply.PatientInfo.Name, false);
            //�Ƿ��Ѱ���
            if (apply.IsAnesth)
            {
                fpSpread1_Sheet1.Rows[row].BackColor = System.Drawing.Color.LightPink;
                fpSpread1_Sheet1.Cells[row, (int)Cols.Name].Note = "�Ѱ���";
            }
            else
            {
                fpSpread1_Sheet1.Cells[row, (int)Cols.Name].Note = "";
            }
            //�Ա�
            fpSpread1_Sheet1.SetValue(row, (int)Cols.Sex, apply.PatientInfo.Sex.Name, false);
            //����
            int interval = DateTime.Now.Year - apply.PatientInfo.Birthday.Year;
            string age = interval + "��";
            if (interval == 0)
            {
                interval = DateTime.Now.Month - apply.PatientInfo.Birthday.Month;
                age = interval + "��";
            }
            if (interval == 0)
            {
                interval = DateTime.Now.Day - apply.PatientInfo.Birthday.Day;
                age = interval + "��";
            }
            fpSpread1_Sheet1.SetValue(row, (int)Cols.age, age, false);
            //��ǰ���
            //if (apply.DiagnoseAl != null && apply.DiagnoseAl.Count > 0)
            //{
            //    // TODO: �����ǰ���

            //    fpSpread1_Sheet1.SetValue(row, (int)Cols.Diagnose, (apply.DiagnoseAl[0] as FS.HISFC.Models.HealthRecord.DiagnoseBase).ICD10.Name, false);
            //}
            //else
            //    fpSpread1_Sheet1.SetValue(row, (int)Cols.Diagnose, "", false);

            string forediagnose = string.Empty;
            for (int t = 0; t < apply.DiagnoseAl.Count; ++t)
            {
                forediagnose += "���" + (t + 1).ToString() + ":" + apply.DiagnoseAl[t].ToString() + "  ";
            }
            fpSpread1_Sheet1.SetValue(row, (int)Cols.Diagnose, forediagnose, false);

            //����������
            string opName = "";
            if (apply.OperationInfos != null && apply.OperationInfos.Count > 0)
            {
                foreach (OperationInfo item in apply.OperationInfos)
                {
                    if (item.IsMainFlag)
                    {
                        opName = item.OperationItem.Name;
                        break;
                    }
                }
                if (opName == "")
                    opName = (apply.OperationInfos[0] as OperationInfo).OperationItem.Name;
            }
            fpSpread1_Sheet1.SetValue(row, (int)Cols.opItemName, opName, false);

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

            //����ҽ��
            fpSpread1_Sheet1.SetValue(row, (int)Cols.opDoctID, apply.OperationDoctor.Name, false);

            //����ʽ
            //if (apply.AnesType.ID != null && apply.AnesType.ID != "")
            //{
            //    NeuObject obj = Environment.GetAnes(apply.AnesType.ID.ToString());

            //    if (obj != null)
            //    {
            //        fpSpread1_Sheet1.SetValue(row, (int)Cols.anaeType, obj.Name, false);
            //        fpSpread1_Sheet1.SetTag(row, (int)Cols.anaeType, obj);
            //    }
            //}
            ////�˴�������ʽ������ʦ����д��ҽ��վ����������ʱ���������ʽ�Ѿ���������������ʾ
            if (apply.AnesType.ID != null && apply.AnesType.ID != "")
            {
                NeuObject obj = new NeuObject();
                int le = apply.AnesType.ID.IndexOf("|");
                if (le > 0)
                {
                    obj = Environment.GetAnes(apply.AnesType.ID.Substring(0, le));
                    if (obj != null)
                    {
                        fpSpread1_Sheet1.SetValue(row, (int)Cols.anaeType, obj.Name, false);
                        fpSpread1_Sheet1.Columns[(int)Cols.anaeType].Locked = false;
                    }
                    obj = Environment.GetAnes(apply.AnesType.ID.Substring(le + 1));
                    if (obj != null)
                    {
                        fpSpread1_Sheet1.SetValue(row, (int)Cols.anaeType2, obj.Name, false);
                        fpSpread1_Sheet1.Columns[(int)Cols.anaeType2].Locked = false;
                    }
                }
                else
                {
                    obj = Environment.GetAnes(apply.AnesType.ID.ToString());
                    if (obj != null)
                    {
                        fpSpread1_Sheet1.SetValue(row, (int)Cols.anaeType, obj.Name, false);
                        fpSpread1_Sheet1.SetTag(row, (int)Cols.anaeType, obj);
                    }
                }
            }

            //�Ƿ���
            if (apply.OperateKind == "2")
            {
                fpSpread1_Sheet1.RowHeader.Cells[row, 0].BackColor = Color.Red;
                fpSpread1_Sheet1.RowHeader.Cells[row, 0].Text = "��";
                fpSpread1_Sheet1.RowHeader.Cells[row, 0].ForeColor = Color.Red;
            }
            fpSpread1_Sheet1.Cells[row, 0, row, 6].CellType = txtType;

            //{3B34DBA7-7740-46eb-93E7-5F2D7890B602}��ѡ���
            FarPoint.Win.Spread.CellType.CheckBoxCellType chkType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            fpSpread1_Sheet1.Cells[row, (int)Cols.isPrint].CellType = chkType;
            fpSpread1_Sheet1.Cells[row, (int)Cols.isPrint].Value = false;
            fpSpread1_Sheet1.Cells[row, (int)Cols.isPrint].Locked = false;

            if (apply.RoleAl != null && apply.RoleAl.Count != 0)
            {
                foreach (ArrangeRole role in apply.RoleAl)
                {
                    if (role.RoleType.ID.ToString() == EnumOperationRole.Anaesthetist.ToString())//����
                    {
                        string name = role.Name;
                        if (role.RoleOperKind.ID != null)
                        {
                            //ֱ��
                            if (role.RoleOperKind.ID.ToString() == EnumRoleOperKind.ZL.ToString())
                                name = name + "|��";
                            //�Ӱ�
                            else if (role.RoleOperKind.ID.ToString() == EnumRoleOperKind.JB.ToString())
                                name = name + "|��";
                        }
                        fpSpread1_Sheet1.SetValue(row, (int)Cols.anaeDoct, name, false);
                        NeuObject obj = (NeuObject)role;
                        obj.Memo = role.RoleOperKind.ID.ToString();
                        fpSpread1_Sheet1.Cells[row, (int)Cols.anaeDoct].Tag = obj;
                    }
                    else if (role.RoleType.ID.ToString() == EnumOperationRole.AnaesthesiaHelper.ToString())//����
                    {
                        string name = role.Name;
                        if (role.RoleOperKind.ID != null)
                        {
                            //ֱ��
                            if (role.RoleOperKind.ID.ToString() == EnumRoleOperKind.ZL.ToString())
                                name = name + "|��";
                            //�Ӱ�
                            else if (role.RoleOperKind.ID.ToString() == EnumRoleOperKind.JB.ToString())
                                name = name + "|��";
                        }
                        fpSpread1_Sheet1.SetValue(row, (int)Cols.anaeHelper, name, false);
                        NeuObject obj = (NeuObject)role;
                        obj.Memo = role.RoleOperKind.ID.ToString();
                        fpSpread1_Sheet1.Cells[row, (int)Cols.anaeHelper].Tag = obj;
                    }
                    else if (role.RoleType.ID.ToString() == EnumOperationRole.AnaesthesiaHelper1.ToString())//����1
                    {
                        string name = role.Name;
                        if (role.RoleOperKind.ID != null)
                        {
                            //ֱ��
                            if (role.RoleOperKind.ID.ToString() == EnumRoleOperKind.ZL.ToString())
                                name = name + "|��";
                            //�Ӱ�
                            else if (role.RoleOperKind.ID.ToString() == EnumRoleOperKind.JB.ToString())
                                name = name + "|��";
                        }
                        fpSpread1_Sheet1.SetValue(row, (int)Cols.anaeHelper1, name, false);
                        NeuObject obj = (NeuObject)role;
                        obj.Memo = role.RoleOperKind.ID.ToString();
                        fpSpread1_Sheet1.Cells[row, (int)Cols.anaeHelper1].Tag = obj;
                    }
                    else if (role.RoleType.ID.ToString() == EnumOperationRole.AnaesthesiaHelper2.ToString())//����2
                    {
                        string name = role.Name;
                        if (role.RoleOperKind.ID != null)
                        {
                            //ֱ��
                            if (role.RoleOperKind.ID.ToString() == EnumRoleOperKind.ZL.ToString())
                                name = name + "|��";
                            //�Ӱ�
                            else if (role.RoleOperKind.ID.ToString() == EnumRoleOperKind.JB.ToString())
                                name = name + "|��";
                        }
                        fpSpread1_Sheet1.SetValue(row, (int)Cols.anaeHelper2, name, false);
                        NeuObject obj = (NeuObject)role;
                        obj.Memo = role.RoleOperKind.ID.ToString();
                        fpSpread1_Sheet1.Cells[row, (int)Cols.anaeHelper2].Tag = obj;
                    }

                    //�Ӱ���Ա
                    else if (role.RoleType.ID.ToString() == EnumOperationRole.OpsShiftDoctor.ToString())
                    {
                        string name = role.Name;
                        fpSpread1_Sheet1.SetValue(row, (int)Cols.ShiftDoctor, name, false);
                        NeuObject obj = (NeuObject)role;
                        fpSpread1_Sheet1.Cells[row, (int)Cols.ShiftDoctor].Tag = obj;
                    }

                    //����ҽ��һ����
                    else if (role.RoleType.ID.ToString() == EnumOperationRole.Helper1.ToString())
                    {
                        string name = role.Name;
                        fpSpread1_Sheet1.SetValue(row, (int)Cols.helper1, name, false);
                        NeuObject obj = (NeuObject)role;
                        fpSpread1_Sheet1.Cells[row, (int)Cols.helper1].Tag = obj;
                    } 
                }
            }
            //������
            if (apply.RoomID != null && apply.RoomID != "")
            {
                NeuObject obj = new NeuObject();
                obj = GetRoom(apply.RoomID);
                //fpSpread1_Sheet1.SetValue(row, obj.Name, false);
                fpSpread1_Sheet1.SetText(row, (int)Cols.RoomID, obj.Name);
                fpSpread1_Sheet1.SetTag(row, (int)Cols.RoomID, obj);


                //fpSpread1_Sheet1.SetValue(row, (int)Cols.RoomID, apply.RoomID, false);
                //NeuObject obj = new NeuObject();
                //obj.Name = apply.RoomID;
                //fpSpread1_Sheet1.Cells[row, (int)Cols.RoomID].Tag = obj;
            }

            //����̨
            if (apply.OpsTable != null)
            {
                fpSpread1_Sheet1.SetValue(row, (int)Cols.TableID, apply.OpsTable.Name, false);
                NeuObject obj = new NeuObject();
                obj.ID = apply.OpsTable.ID;
                obj.Name = apply.OpsTable.Name;
                fpSpread1_Sheet1.Cells[row, (int)Cols.TableID].Tag = obj;
            }

            //{B9DDCC10-3380-4212-99E5-BB909643F11B}
            fpSpread1_Sheet1.Cells[row, (int)Cols.anaeWay].CellType = txtType;
            fpSpread1_Sheet1.SetValue(row, (int)Cols.anaeWay, this.anneObjectHelper.GetName(apply.AnesWay));
            fpSpread1_Sheet1.Cells[row, (int)Cols.TableID].CellType = txtType;

            return 0;
        }

        /// <summary>
        /// ���
        /// </summary>
        public void Reset()
        {
            this.fpSpread1_Sheet1.RowCount = 0;
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
        /// ����
        /// </summary>
        /// <returns></returns>
        public int Save()
        {

            //���ݿ�����
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction trans = new 
            //    FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //trans.BeginTransaction();

            List<int> succeed = new List<int>();        //�ɹ����ŵ�

            for (int i = 0; i < fpSpread1_Sheet1.RowCount; i++)
            {
                if ((bool)this.fpSpread1_Sheet1.Cells[i, (int)Cols.isPrint].Value == true)
                {
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
                    //    type2.Name = type1.Name + "|" + type2.Name;
                        type.Name = type1.Name + "|" + type2.Name;
                    }
                    else
                    {
                        type = type1.Clone();
                    }
                    //û��¼������ʽ��������
                    if (type == null || type.ID.Length == 0)
                        continue;
                    //����
                    NeuObject anaeDoct = fpSpread1_Sheet1.GetTag(i, (int)Cols.anaeDoct) as NeuObject;
                    //û�����飬������
                    if (anaeDoct == null || anaeDoct.ID.Length == 0)
                        continue;

                    NeuObject tt = fpSpread1_Sheet1.GetTag(i, (int)Cols.anaeHelper) as NeuObject;
                    if (tt != null && tt.ID != "" && anaeDoct.ID == tt.ID)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("��������ֲ�����ͬһ����");
                        return -1;
                    }
                    //��������ʵ��
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

                    //apply.IsAnesth = true;

                    //trans.BeginTransaction();
                    Environment.OperationManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                    try
                    {
                        //�������ʽ������ʵ��
                        apply.AnesType = type;
                        //������顢����
                        this.AddRole(apply, i);
                        //��־Ϊ�Ѱ�������
                        apply.IsAnesth=true;

                        if (Environment.OperationManager.UpdateApplication(apply) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("��������(" + apply.ID + ")������Ϣʧ�ܣ�\n����ϵͳ����Ա��ϵ��" + Environment.OperationManager.Err, "��ʾ",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return -1;
                        }
                        FS.FrameWork.Management.PublicTrans.Commit();
                        succeed.Add(i);
                    }
                    catch (Exception e)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("��������(" + apply.ID + ")������Ϣ����!" + e.Message, "��ʾ");
                        return -1;
                    }
                    //���½�����ʾ
                    fpSpread1_Sheet1.Rows[i].Tag = apply;
                    fpSpread1_Sheet1.Rows[i].BackColor = System.Drawing.Color.LightPink;
                    //fpSpread1_Sheet1.Cells[i,(int)Cols.Name].Note="�Ѱ���";

                    //////////////////////////////////////////////////////////////////////////                
                    // Robin��Ϊ�����������û����
                    //this.UpdateFlag(apply);
                    //////////////////////////////////////////////////////////////////////////
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
                //temp = FS.FrameWork.Function.NConvert.ToInt32 (line) + 1;
                //MessageBox.Show(string.Format("��{0}���������ųɹ���", temp.ToString()), "��ʾ");
                MessageBox.Show("�����ųɹ�", "��ʾ");
                fpSpread1.Focus();
            }
            else
            {
                MessageBox.Show("û�пɰ��ŵ���������!", "��ʾ");
                fpSpread1.Focus();
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
                this.isSortByRoom = false;
                for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                {
                    this.fpSpread1_Sheet1.Cells[i, (int)Cols.isPrint].Value = true;
                }
            }
            //����������˳���ӡ
            if (obj.ID == "rb2")
            {
                this.isSortByRoom = true;
                for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                {
                    this.fpSpread1_Sheet1.Cells[i, (int)Cols.isPrint].Value = true;
                }
            }
            //�����������ӡ
            if (obj.ID == "rb3")
            {
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

        /// <summary>
        /// ������顢����
        /// </summary>
        /// <param name="apply"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private int AddRole(OperationAppllication apply, int row)
        {
            ArrayList roles = new ArrayList();
            //��������顢����
            for (int i = 0; i < apply.RoleAl.Count; i++)
            {
                ArrangeRole role = apply.RoleAl[i] as ArrangeRole;
                if (role.RoleType.ID.ToString() != EnumOperationRole.Anaesthetist.ToString() &&
                    role.RoleType.ID.ToString() != EnumOperationRole.AnaesthesiaHelper.ToString() &&
                    role.RoleType.ID.ToString() != EnumOperationRole.AnaesthesiaHelper1.ToString() &&
                    role.RoleType.ID.ToString() != EnumOperationRole.AnaesthesiaHelper2.ToString() &&
                    role.RoleType.ID.ToString()!=EnumOperationRole.OpsShiftDoctor.ToString())
                {
                    roles.Add(role.Clone());
                }
            }

            //�������
            NeuObject obj = fpSpread1_Sheet1.GetTag(row, (int)Cols.anaeDoct) as NeuObject;
            if (obj != null)
            {
                ArrangeRole role = new ArrangeRole(obj);
                role.RoleType.ID = EnumOperationRole.Anaesthetist;//��ɫ����
                if (obj.Memo == "ZL")
                    role.RoleOperKind.ID = EnumRoleOperKind.ZL;
                else if (obj.Memo == "JB")
                    role.RoleOperKind.ID = EnumRoleOperKind.JB;

                role.OperationNo = apply.ID;
                role.ForeFlag = "0";//��ǰ����				
                roles.Add(role);//������Ա��ɫ����	
            }
            //�������
            obj = fpSpread1_Sheet1.GetTag(row, (int)Cols.anaeHelper) as NeuObject;
            if (obj != null)
            {
                ArrangeRole role = new ArrangeRole(obj);

                role.RoleType.ID = EnumOperationRole.AnaesthesiaHelper;//��ɫ����
                if (obj.Memo == "ZL")
                    role.RoleOperKind.ID = EnumRoleOperKind.ZL;
                else if (obj.Memo == "JB")
                    role.RoleOperKind.ID = EnumRoleOperKind.JB;

                role.OperationNo = apply.ID;
                role.ForeFlag = "0";//��ǰ����				
                roles.Add(role);//������Ա��ɫ����	
            }
            //�������1
            obj = fpSpread1_Sheet1.GetTag(row, (int)Cols.anaeHelper1) as NeuObject;
            if (obj != null)
            {
                ArrangeRole role = new ArrangeRole(obj);

                role.RoleType.ID = EnumOperationRole.AnaesthesiaHelper1;//��ɫ����
                if (obj.Memo == "ZL")
                    role.RoleOperKind.ID = EnumRoleOperKind.ZL;
                else if (obj.Memo == "JB")
                    role.RoleOperKind.ID = EnumRoleOperKind.JB;

                role.OperationNo = apply.ID;
                role.ForeFlag = "0";//��ǰ����				
                roles.Add(role);//������Ա��ɫ����	
            }
            //�������2
            obj = fpSpread1_Sheet1.GetTag(row, (int)Cols.anaeHelper2) as NeuObject;
            if (obj != null)
            {
                ArrangeRole role = new ArrangeRole(obj);

                role.RoleType.ID = EnumOperationRole.AnaesthesiaHelper2;//��ɫ����
                if (obj.Memo == "ZL")
                    role.RoleOperKind.ID = EnumRoleOperKind.ZL;
                else if (obj.Memo == "JB")
                    role.RoleOperKind.ID = EnumRoleOperKind.JB;

                role.OperationNo = apply.ID;
                role.ForeFlag = "0";//��ǰ����				
                roles.Add(role);//������Ա��ɫ����	
            }

            //��ӽӰ���Ա
            obj = fpSpread1_Sheet1.GetTag(row, (int)Cols.ShiftDoctor) as NeuObject;
            if (obj != null)
            {
                ArrangeRole role = new ArrangeRole(obj);

                role.RoleType.ID = EnumOperationRole.OpsShiftDoctor; //��ɫ����
                role.OperationNo = apply.ID;
                role.ForeFlag = "0";//��ǰ����		
                roles.Add(role);//������Ա��ɫ����	
            }
            apply.RoleAl = roles;

            return 0;
        }
        //����ʵ��İ��ű�־
        //private int UpdateFlag(OpsApplication apply)
        //{
        //    for (int index = 0; index < alApplys.Count; index++)
        //    {
        //        FS.HISFC.Object.Operator.OpsApplication obj = alApplys[index] as FS.HISFC.Object.Operator.OpsApplication;
        //        if (obj.OperationNo == apply.OperationNo)
        //        {
        //            alApplys.Remove(obj);
        //            alApplys.Insert(index, apply);
        //            break;
        //        }
        //    }
        //    return 0;
        //}
        /// <summary>
        /// ��������
        /// </summary>
        private OperationAppllication UpdateData(int rowIndex)
        {
            //����ʽ
            
            //û��¼������ʽ��������
            //if (type == null || type.ID.Length == 0)
            //    return null;
            //����
            //NeuObject anaeDoct = fpSpread1_Sheet1.GetTag(rowIndex, (int)Cols.anaeDoct) as NeuObject;
            //û�����飬������
            //if (anaeDoct == null || anaeDoct.ID.Length == 0)
            //return null;

            //NeuObject tt = fpSpread1_Sheet1.GetTag(rowIndex, (int)Cols.anaeHelper) as NeuObject;
            //if (tt != null && tt.ID != "" && anaeDoct.ID == tt.ID)
            //{
            //    MessageBox.Show("��������ֲ�����ͬһ����");
            //    return null;
            //}
            //��������ʵ��
            OperationAppllication apply = fpSpread1_Sheet1.Rows[rowIndex].Tag as OperationAppllication;


            try
            {
                //�����������
                NeuObject tab = fpSpread1_Sheet1.GetTag(rowIndex, (int)Cols.RoomID) as NeuObject;
                if (tab != null)
                {
                    apply.RoomID = tab.ID;
                    apply.OpePos.Memo = tab.Name;//add by chengym ��ӡ��
                }
                else
                {
                    fpSpread1_Sheet1.SetValue(rowIndex, (int)Cols.RoomID, "", false);
                }
                //�������ʽ������ʵ��
               // NeuObject type = fpSpread1_Sheet1.GetTag(rowIndex, (int)Cols.anaeType) as NeuObject;
               // apply.AnesType = type;
                //������顢����
                this.AddRole(apply, rowIndex);
            }
            catch
            {
                return null;
            }
            //���½�����ʾ
            fpSpread1_Sheet1.Rows[rowIndex].Tag = apply;

            return apply;
        }
        public int Print()
        {
            if (this.arrangePrint == null)
            {
                //this.arrangePrint = new ucArrangePrint();
                this.arrangePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Operation.IArrangePrint),1) as FS.HISFC.BizProcess.Interface.Operation.IArrangePrint;
                if (this.arrangePrint == null)
                {
                    MessageBox.Show("��ýӿ�IArrangePrint��������ϵͳ����Ա��ϵ��");

                    return -1;
                }
            }

            this.arrangePrint.Title = "������һ����";
            this.arrangePrint.Date = this.date;
            this.arrangePrint.ArrangeType = FS.HISFC.BizProcess.Interface.Operation.EnumArrangeType.Anaesthesia;
            this.arrangePrint.Reset();
            //for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            //{
            //    this.arrangePrint.AddAppliction(this.UpdateData(i));
            //}
            //return this.arrangePrint.PrintPreview();
            // modify by zhy �������ź���������ֿ�����ӡ
            ArrayList applyList = new ArrayList();
            RoomSort sort = new RoomSort();
            RoomTableSort sorttable = new RoomTableSort();
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                //{3B34DBA7-7740-46eb-93E7-5F2D7890B602}
                //if ((bool)this.fpSpread1_Sheet1.Cells[i, (int)Cols.isPrint].Value == true && this.fpSpread1_Sheet1.Cells[i,(int)Cols.anaeDoct].Text.ToString()!="")
                if ((bool)this.fpSpread1_Sheet1.Cells[i, (int)Cols.isPrint].Value == true)
                {
                    applyList.Add(this.UpdateData(i));
                }
            }
            if (this.isSortByRoom)
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

        public int Export()
        {
            return this.fpSpread1.Export();
        }
        #endregion

        #region columns
        private enum Cols
        {
            isPrint,
            /// <summary>
            /// ����
            /// </summary>
            RoomID,
            /// <summary>
            /// ����̨
            /// </summary>
            TableID,
            nurseCell,//add by zhy
            nurseID,
            bedID,
            Name,
            Sex,
            age,
            Diagnose,
            opItemName,            
            //�������� ��������صģ�����ҽ������
            anaeWay,
            isSpecial,//�Ƿ��������� add by zhy
            
            opDoctID,//����ҽ��
            helper1,//һ����
            /// <summary>
            /// ����ʽ
            /// </summary>
            anaeType,
            anaeType2,
            /// <summary>
            /// ����
            /// </summary>
            anaeDoct,
            /// <summary>
            /// ����
            /// </summary>
            anaeHelper,
            /// <summary>
            /// ����1
            /// </summary>
            anaeHelper1,
            /// <summary>
            /// ����2
            /// </summary>
            anaeHelper2,
          
            /// <summary>
            /// �Ӱ���Ա
            /// </summary>
            ShiftDoctor,
            /// <summary>
            /// ����˵��
            /// </summary>
            specialNote
        }
        #endregion

        #region �¼�
        private void fpSpread1_EditModeOn(object sender, EventArgs e)
        {
            //fpSpread1.EditingControl.KeyDown += new KeyEventHandler(EditingControl_KeyDown);
            this.SetLocation();
            if (fpSpread1_Sheet1.ActiveColumnIndex == (int)Cols.anaeType)
                this.lbAnaetype.Visible = true;
            else
                lbAnaetype.Visible = false;

            //if (fpSpread1_Sheet1.ActiveColumnIndex == (int)Cols.anaeDoct ||
            //    fpSpread1_Sheet1.ActiveColumnIndex == (int)Cols.anaeHelper)
            //    this.lbDoctor.Visible = true;
            //else
            //    lbDoctor.Visible = false;
        }


        private void fpSpread1_EditChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            //string _Text;
            ////����ʽ
            //if (e.Column == (int)Cols.anaeType)
            //{
            //    _Text = fpSpread1_Sheet1.ActiveCell.Text;
            //    lbAnaetype.Filter(_Text);

            //    if (lbAnaetype.Visible == false) lbAnaetype.Visible = true;
            //    fpSpread1_Sheet1.SetTag(e.Row, e.Column, null);
            //}
            ////���֡�����
            //else if (e.Column == (int)Cols.anaeDoct || e.Column == (int)Cols.anaeHelper)
            //{
            //    //if (IsChange) return;
            //    _Text = fpSpread1_Sheet1.ActiveCell.Text;
            //    lbDoctor.Filter(_Text);

            //    if (lbDoctor.Visible == false) 
            //        lbDoctor.Visible = true;
            //    fpSpread1_Sheet1.SetTag(e.Row, e.Column, null);
            //}
        }
        #endregion

        private void fpSpread1_EnterCell(object sender, FarPoint.Win.Spread.EnterCellEventArgs e)
        {
            if (this.applictionSelected != null)
            {
                this.applictionSelected(this, this.fpSpread1_Sheet1.Rows[e.Row].Tag as OperationAppllication);
            }
        }

        //private void lbDoctor_ItemSelected(object sender, System.EventArgs e)
        //{
        //    this.SelectDoctor(fpSpread1_Sheet1.ActiveColumnIndex);

        //}

        //private void lbAnaetype_ItemSelected(object sender, System.EventArgs e)
        //{
        //    this.SelectType();
        //}

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get { return new Type[] { typeof(FS.HISFC.BizProcess.Interface.Operation.IArrangePrint) }; }
        }

        #endregion

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
