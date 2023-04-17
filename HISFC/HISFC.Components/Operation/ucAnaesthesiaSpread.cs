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
    /// [功能描述: 麻醉安排表格控件]<br></br>
    /// [创 建 者: 王铁全]<br></br>
    /// [创建时间: 2006-12-11]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
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

        #region 字段
        /// <summary>
        /// 本科人员列表
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
        /// 是否按照手术间排序
        /// </summary>
        bool isSortByRoom = false;

        private ArrayList alRooms = new ArrayList();

        #endregion

        #region 属性
        public DateTime Date
        {
            set
            {
                this.date = value;
            }
        }

        private string anaesDocDept = string.Empty;
        /// <summary>
        /// 麻醉医生所在科室
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


        #region 方法

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

        //全选
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
            this.lbRoom.Font = new Font("宋体", 12);
            this.lbRoom.Size = new Size(220, 96);
            this.lbRoom.Hide();
            this.lbRoom.BorderStyle = BorderStyle.FixedSingle;
            this.lbRoom.BringToFront();
            //this.lbRoom.ItemSelected += new System.EventHandler(this.lbRoom_ItemSelected);

        }

        //生成手术间列表
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


        //刷新本科人员列表
        private int RefreshEmployeeList()
        {
            //treeView2.Nodes.Clear();
            //TreeNode root = new TreeNode();
            //root.Text = "本科人员";
            //root.ImageIndex = 22;
            //root.SelectedImageIndex = 22;
            //treeView2.Nodes.Add(root);

            //获取本科人员集
            //MessageBox.Show(EnumEmployeeType.D.ToString(),Environment.OperatorDeptID);
            ArrayList persons = new ArrayList();

            if (this.anaesDocDept == string.Empty)
            {
                 persons = Environment.IntegrateManager.QueryEmployee(EnumEmployeeType.D, Environment.OperatorDeptID);
                //有些医院麻醉医生属于麻醉科，不属于手术室，一般护士登陆是手术室，导致获取不到麻醉医生2012-7-11 chengym
            }
            else
            {
                if (persons == null || persons.Count == 0)
                {
                    persons = Environment.IntegrateManager.QueryEmployee(EnumEmployeeType.D, this.anaesDocDept);
                }
            }
            //ArrayList persons = Environment.IntegrateManager.QueryEmployeeAll();
            //添加到树形列表
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
            //生成医生listbox列表
            this.InitDoctListBox(persons);
            persons = null;

            return 0;
        }

        /// <summary>
        /// 添加医生listbox列表
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
        /// 生成麻醉类别列表
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
        /// 设置护士、手术台列表位置
        /// </summary>
        /// <returns></returns>
        private int SetLocation()
        {
            Control _cell = fpSpread1.EditingControl;
            if (_cell == null) return 0;

            //助手、主麻
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
        /// 选择医生
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
        /// 选择麻醉类型
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
        /// 添加手术申请信息
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

            //病区
            Department nursecell = Environment.IntegrateManager.GetDepartment(apply.PatientInfo.PVisit.PatientLocation.NurseCell.ID);
            if (nursecell != null)
            {
                fpSpread1_Sheet1.SetValue(row, (int)Cols.nurseCell, nursecell.Name, false);
            }

            //科室            
            Department dept = Environment.IntegrateManager.GetDepartment(apply.PatientInfo.PVisit.PatientLocation.Dept.ID);
            if (dept != null)
            {
                fpSpread1_Sheet1.SetValue(row, (int)Cols.nurseID, dept.Name, false);
            }
            //床号
            fpSpread1_Sheet1.SetValue(row, (int)Cols.bedID, apply.PatientInfo.PVisit.PatientLocation.Bed.Name, false);
            //患者姓名
            fpSpread1_Sheet1.SetValue(row, (int)Cols.Name, apply.PatientInfo.Name, false);
            //是否已安排
            if (apply.IsAnesth)
            {
                fpSpread1_Sheet1.Rows[row].BackColor = System.Drawing.Color.LightPink;
                fpSpread1_Sheet1.Cells[row, (int)Cols.Name].Note = "已安排";
            }
            else
            {
                fpSpread1_Sheet1.Cells[row, (int)Cols.Name].Note = "";
            }
            //性别
            fpSpread1_Sheet1.SetValue(row, (int)Cols.Sex, apply.PatientInfo.Sex.Name, false);
            //年龄
            int interval = DateTime.Now.Year - apply.PatientInfo.Birthday.Year;
            string age = interval + "岁";
            if (interval == 0)
            {
                interval = DateTime.Now.Month - apply.PatientInfo.Birthday.Month;
                age = interval + "月";
            }
            if (interval == 0)
            {
                interval = DateTime.Now.Day - apply.PatientInfo.Birthday.Day;
                age = interval + "天";
            }
            fpSpread1_Sheet1.SetValue(row, (int)Cols.age, age, false);
            //术前诊断
            //if (apply.DiagnoseAl != null && apply.DiagnoseAl.Count > 0)
            //{
            //    // TODO: 添加术前诊断

            //    fpSpread1_Sheet1.SetValue(row, (int)Cols.Diagnose, (apply.DiagnoseAl[0] as FS.HISFC.Models.HealthRecord.DiagnoseBase).ICD10.Name, false);
            //}
            //else
            //    fpSpread1_Sheet1.SetValue(row, (int)Cols.Diagnose, "", false);

            string forediagnose = string.Empty;
            for (int t = 0; t < apply.DiagnoseAl.Count; ++t)
            {
                forediagnose += "诊断" + (t + 1).ToString() + ":" + apply.DiagnoseAl[t].ToString() + "  ";
            }
            fpSpread1_Sheet1.SetValue(row, (int)Cols.Diagnose, forediagnose, false);

            //主手术名称
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

            //主刀医生
            fpSpread1_Sheet1.SetValue(row, (int)Cols.opDoctID, apply.OperationDoctor.Name, false);

            //麻醉方式
            //if (apply.AnesType.ID != null && apply.AnesType.ID != "")
            //{
            //    NeuObject obj = Environment.GetAnes(apply.AnesType.ID.ToString());

            //    if (obj != null)
            //    {
            //        fpSpread1_Sheet1.SetValue(row, (int)Cols.anaeType, obj.Name, false);
            //        fpSpread1_Sheet1.SetTag(row, (int)Cols.anaeType, obj);
            //    }
            //}
            ////此处的麻醉方式由麻醉师来填写，医生站申请手术的时候填的麻醉方式已经在手术申请中显示
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

            //是否急诊
            if (apply.OperateKind == "2")
            {
                fpSpread1_Sheet1.RowHeader.Cells[row, 0].BackColor = Color.Red;
                fpSpread1_Sheet1.RowHeader.Cells[row, 0].Text = "急";
                fpSpread1_Sheet1.RowHeader.Cells[row, 0].ForeColor = Color.Red;
            }
            fpSpread1_Sheet1.Cells[row, 0, row, 6].CellType = txtType;

            //{3B34DBA7-7740-46eb-93E7-5F2D7890B602}加选择框
            FarPoint.Win.Spread.CellType.CheckBoxCellType chkType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            fpSpread1_Sheet1.Cells[row, (int)Cols.isPrint].CellType = chkType;
            fpSpread1_Sheet1.Cells[row, (int)Cols.isPrint].Value = false;
            fpSpread1_Sheet1.Cells[row, (int)Cols.isPrint].Locked = false;

            if (apply.RoleAl != null && apply.RoleAl.Count != 0)
            {
                foreach (ArrangeRole role in apply.RoleAl)
                {
                    if (role.RoleType.ID.ToString() == EnumOperationRole.Anaesthetist.ToString())//主麻
                    {
                        string name = role.Name;
                        if (role.RoleOperKind.ID != null)
                        {
                            //直落
                            if (role.RoleOperKind.ID.ToString() == EnumRoleOperKind.ZL.ToString())
                                name = name + "|▲";
                            //接班
                            else if (role.RoleOperKind.ID.ToString() == EnumRoleOperKind.JB.ToString())
                                name = name + "|△";
                        }
                        fpSpread1_Sheet1.SetValue(row, (int)Cols.anaeDoct, name, false);
                        NeuObject obj = (NeuObject)role;
                        obj.Memo = role.RoleOperKind.ID.ToString();
                        fpSpread1_Sheet1.Cells[row, (int)Cols.anaeDoct].Tag = obj;
                    }
                    else if (role.RoleType.ID.ToString() == EnumOperationRole.AnaesthesiaHelper.ToString())//助手
                    {
                        string name = role.Name;
                        if (role.RoleOperKind.ID != null)
                        {
                            //直落
                            if (role.RoleOperKind.ID.ToString() == EnumRoleOperKind.ZL.ToString())
                                name = name + "|▲";
                            //接班
                            else if (role.RoleOperKind.ID.ToString() == EnumRoleOperKind.JB.ToString())
                                name = name + "|△";
                        }
                        fpSpread1_Sheet1.SetValue(row, (int)Cols.anaeHelper, name, false);
                        NeuObject obj = (NeuObject)role;
                        obj.Memo = role.RoleOperKind.ID.ToString();
                        fpSpread1_Sheet1.Cells[row, (int)Cols.anaeHelper].Tag = obj;
                    }
                    else if (role.RoleType.ID.ToString() == EnumOperationRole.AnaesthesiaHelper1.ToString())//助手1
                    {
                        string name = role.Name;
                        if (role.RoleOperKind.ID != null)
                        {
                            //直落
                            if (role.RoleOperKind.ID.ToString() == EnumRoleOperKind.ZL.ToString())
                                name = name + "|▲";
                            //接班
                            else if (role.RoleOperKind.ID.ToString() == EnumRoleOperKind.JB.ToString())
                                name = name + "|△";
                        }
                        fpSpread1_Sheet1.SetValue(row, (int)Cols.anaeHelper1, name, false);
                        NeuObject obj = (NeuObject)role;
                        obj.Memo = role.RoleOperKind.ID.ToString();
                        fpSpread1_Sheet1.Cells[row, (int)Cols.anaeHelper1].Tag = obj;
                    }
                    else if (role.RoleType.ID.ToString() == EnumOperationRole.AnaesthesiaHelper2.ToString())//助手2
                    {
                        string name = role.Name;
                        if (role.RoleOperKind.ID != null)
                        {
                            //直落
                            if (role.RoleOperKind.ID.ToString() == EnumRoleOperKind.ZL.ToString())
                                name = name + "|▲";
                            //接班
                            else if (role.RoleOperKind.ID.ToString() == EnumRoleOperKind.JB.ToString())
                                name = name + "|△";
                        }
                        fpSpread1_Sheet1.SetValue(row, (int)Cols.anaeHelper2, name, false);
                        NeuObject obj = (NeuObject)role;
                        obj.Memo = role.RoleOperKind.ID.ToString();
                        fpSpread1_Sheet1.Cells[row, (int)Cols.anaeHelper2].Tag = obj;
                    }

                    //接班人员
                    else if (role.RoleType.ID.ToString() == EnumOperationRole.OpsShiftDoctor.ToString())
                    {
                        string name = role.Name;
                        fpSpread1_Sheet1.SetValue(row, (int)Cols.ShiftDoctor, name, false);
                        NeuObject obj = (NeuObject)role;
                        fpSpread1_Sheet1.Cells[row, (int)Cols.ShiftDoctor].Tag = obj;
                    }

                    //主刀医生一助手
                    else if (role.RoleType.ID.ToString() == EnumOperationRole.Helper1.ToString())
                    {
                        string name = role.Name;
                        fpSpread1_Sheet1.SetValue(row, (int)Cols.helper1, name, false);
                        NeuObject obj = (NeuObject)role;
                        fpSpread1_Sheet1.Cells[row, (int)Cols.helper1].Tag = obj;
                    } 
                }
            }
            //手术间
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

            //手术台
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
        /// 清空
        /// </summary>
        public void Reset()
        {
            this.fpSpread1_Sheet1.RowCount = 0;
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
        /// 保存
        /// </summary>
        /// <returns></returns>
        public int Save()
        {

            //数据库事务
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction trans = new 
            //    FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //trans.BeginTransaction();

            List<int> succeed = new List<int>();        //成功安排的

            for (int i = 0; i < fpSpread1_Sheet1.RowCount; i++)
            {
                if ((bool)this.fpSpread1_Sheet1.Cells[i, (int)Cols.isPrint].Value == true)
                {
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
                    //    type2.Name = type1.Name + "|" + type2.Name;
                        type.Name = type1.Name + "|" + type2.Name;
                    }
                    else
                    {
                        type = type1.Clone();
                    }
                    //没有录入麻醉方式，不处理
                    if (type == null || type.ID.Length == 0)
                        continue;
                    //主麻
                    NeuObject anaeDoct = fpSpread1_Sheet1.GetTag(i, (int)Cols.anaeDoct) as NeuObject;
                    //没有主麻，不处理
                    if (anaeDoct == null || anaeDoct.ID.Length == 0)
                        continue;

                    NeuObject tt = fpSpread1_Sheet1.GetTag(i, (int)Cols.anaeHelper) as NeuObject;
                    if (tt != null && tt.ID != "" && anaeDoct.ID == tt.ID)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("主麻和助手不能是同一个人");
                        return -1;
                    }
                    //手术申请实体
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

                    //apply.IsAnesth = true;

                    //trans.BeginTransaction();
                    Environment.OperationManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                    try
                    {
                        //添加麻醉方式到手术实体
                        apply.AnesType = type;
                        //添加主麻、助手
                        this.AddRole(apply, i);
                        //标志为已安排麻醉
                        apply.IsAnesth=true;

                        if (Environment.OperationManager.UpdateApplication(apply) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("保存麻醉(" + apply.ID + ")安排信息失败！\n请与系统管理员联系。" + Environment.OperationManager.Err, "提示",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return -1;
                        }
                        FS.FrameWork.Management.PublicTrans.Commit();
                        succeed.Add(i);
                    }
                    catch (Exception e)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("保存麻醉(" + apply.ID + ")安排信息出错!" + e.Message, "提示");
                        return -1;
                    }
                    //更新界面显示
                    fpSpread1_Sheet1.Rows[i].Tag = apply;
                    fpSpread1_Sheet1.Rows[i].BackColor = System.Drawing.Color.LightPink;
                    //fpSpread1_Sheet1.Cells[i,(int)Cols.Name].Note="已安排";

                    //////////////////////////////////////////////////////////////////////////                
                    // Robin认为下面这个函数没有用
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
                //MessageBox.Show(string.Format("第{0}行手术安排成功。", temp.ToString()), "提示");
                MessageBox.Show("麻醉安排成功", "提示");
                fpSpread1.Focus();
            }
            else
            {
                MessageBox.Show("没有可安排的手术申请!", "提示");
                fpSpread1.Focus();
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
                this.isSortByRoom = false;
                for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                {
                    this.fpSpread1_Sheet1.Cells[i, (int)Cols.isPrint].Value = true;
                }
            }
            //所有手术间顺序打印
            if (obj.ID == "rb2")
            {
                this.isSortByRoom = true;
                for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                {
                    this.fpSpread1_Sheet1.Cells[i, (int)Cols.isPrint].Value = true;
                }
            }
            //单独手术间打印
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
            //单独病区打印
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
        /// 添加主麻、助手
        /// </summary>
        /// <param name="apply"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private int AddRole(OperationAppllication apply, int row)
        {
            ArrayList roles = new ArrayList();
            //先清空主麻、助手
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

            //添加主麻
            NeuObject obj = fpSpread1_Sheet1.GetTag(row, (int)Cols.anaeDoct) as NeuObject;
            if (obj != null)
            {
                ArrangeRole role = new ArrangeRole(obj);
                role.RoleType.ID = EnumOperationRole.Anaesthetist;//角色编码
                if (obj.Memo == "ZL")
                    role.RoleOperKind.ID = EnumRoleOperKind.ZL;
                else if (obj.Memo == "JB")
                    role.RoleOperKind.ID = EnumRoleOperKind.JB;

                role.OperationNo = apply.ID;
                role.ForeFlag = "0";//术前安排				
                roles.Add(role);//加入人员角色对象	
            }
            //添加助手
            obj = fpSpread1_Sheet1.GetTag(row, (int)Cols.anaeHelper) as NeuObject;
            if (obj != null)
            {
                ArrangeRole role = new ArrangeRole(obj);

                role.RoleType.ID = EnumOperationRole.AnaesthesiaHelper;//角色编码
                if (obj.Memo == "ZL")
                    role.RoleOperKind.ID = EnumRoleOperKind.ZL;
                else if (obj.Memo == "JB")
                    role.RoleOperKind.ID = EnumRoleOperKind.JB;

                role.OperationNo = apply.ID;
                role.ForeFlag = "0";//术前安排				
                roles.Add(role);//加入人员角色对象	
            }
            //添加助手1
            obj = fpSpread1_Sheet1.GetTag(row, (int)Cols.anaeHelper1) as NeuObject;
            if (obj != null)
            {
                ArrangeRole role = new ArrangeRole(obj);

                role.RoleType.ID = EnumOperationRole.AnaesthesiaHelper1;//角色编码
                if (obj.Memo == "ZL")
                    role.RoleOperKind.ID = EnumRoleOperKind.ZL;
                else if (obj.Memo == "JB")
                    role.RoleOperKind.ID = EnumRoleOperKind.JB;

                role.OperationNo = apply.ID;
                role.ForeFlag = "0";//术前安排				
                roles.Add(role);//加入人员角色对象	
            }
            //添加助手2
            obj = fpSpread1_Sheet1.GetTag(row, (int)Cols.anaeHelper2) as NeuObject;
            if (obj != null)
            {
                ArrangeRole role = new ArrangeRole(obj);

                role.RoleType.ID = EnumOperationRole.AnaesthesiaHelper2;//角色编码
                if (obj.Memo == "ZL")
                    role.RoleOperKind.ID = EnumRoleOperKind.ZL;
                else if (obj.Memo == "JB")
                    role.RoleOperKind.ID = EnumRoleOperKind.JB;

                role.OperationNo = apply.ID;
                role.ForeFlag = "0";//术前安排				
                roles.Add(role);//加入人员角色对象	
            }

            //添加接班人员
            obj = fpSpread1_Sheet1.GetTag(row, (int)Cols.ShiftDoctor) as NeuObject;
            if (obj != null)
            {
                ArrangeRole role = new ArrangeRole(obj);

                role.RoleType.ID = EnumOperationRole.OpsShiftDoctor; //角色编码
                role.OperationNo = apply.ID;
                role.ForeFlag = "0";//术前安排		
                roles.Add(role);//加入人员角色对象	
            }
            apply.RoleAl = roles;

            return 0;
        }
        //更新实体的安排标志
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
        /// 更新数据
        /// </summary>
        private OperationAppllication UpdateData(int rowIndex)
        {
            //麻醉方式
            
            //没有录入麻醉方式，不处理
            //if (type == null || type.ID.Length == 0)
            //    return null;
            //主麻
            //NeuObject anaeDoct = fpSpread1_Sheet1.GetTag(rowIndex, (int)Cols.anaeDoct) as NeuObject;
            //没有主麻，不处理
            //if (anaeDoct == null || anaeDoct.ID.Length == 0)
            //return null;

            //NeuObject tt = fpSpread1_Sheet1.GetTag(rowIndex, (int)Cols.anaeHelper) as NeuObject;
            //if (tt != null && tt.ID != "" && anaeDoct.ID == tt.ID)
            //{
            //    MessageBox.Show("主麻和助手不能是同一个人");
            //    return null;
            //}
            //手术申请实体
            OperationAppllication apply = fpSpread1_Sheet1.Rows[rowIndex].Tag as OperationAppllication;


            try
            {
                //添加手术房间
                NeuObject tab = fpSpread1_Sheet1.GetTag(rowIndex, (int)Cols.RoomID) as NeuObject;
                if (tab != null)
                {
                    apply.RoomID = tab.ID;
                    apply.OpePos.Memo = tab.Name;//add by chengym 打印用
                }
                else
                {
                    fpSpread1_Sheet1.SetValue(rowIndex, (int)Cols.RoomID, "", false);
                }
                //添加麻醉方式到手术实体
               // NeuObject type = fpSpread1_Sheet1.GetTag(rowIndex, (int)Cols.anaeType) as NeuObject;
               // apply.AnesType = type;
                //添加主麻、助手
                this.AddRole(apply, rowIndex);
            }
            catch
            {
                return null;
            }
            //更新界面显示
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
                    MessageBox.Show("获得接口IArrangePrint错误，请与系统管理员联系。");

                    return -1;
                }
            }

            this.arrangePrint.Title = "麻醉安排一览表";
            this.arrangePrint.Date = this.date;
            this.arrangePrint.ArrangeType = FS.HISFC.BizProcess.Interface.Operation.EnumArrangeType.Anaesthesia;
            this.arrangePrint.Reset();
            //for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            //{
            //    this.arrangePrint.AddAppliction(this.UpdateData(i));
            //}
            //return this.arrangePrint.PrintPreview();
            // modify by zhy 手术安排和手术麻醉分开来打印
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

        //全选
        public void AllSelect()
        {
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                this.fpSpread1_Sheet1.Cells[i, (int)Cols.isPrint].Value = true;
            }
        }

        //全不选
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
            /// 房号
            /// </summary>
            RoomID,
            /// <summary>
            /// 手术台
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
            //麻醉类型 这个是隐藏的，不让医生看到
            anaeWay,
            isSpecial,//是否特殊手术 add by zhy
            
            opDoctID,//主刀医生
            helper1,//一助手
            /// <summary>
            /// 麻醉方式
            /// </summary>
            anaeType,
            anaeType2,
            /// <summary>
            /// 主麻
            /// </summary>
            anaeDoct,
            /// <summary>
            /// 助手
            /// </summary>
            anaeHelper,
            /// <summary>
            /// 助手1
            /// </summary>
            anaeHelper1,
            /// <summary>
            /// 助手2
            /// </summary>
            anaeHelper2,
          
            /// <summary>
            /// 接班人员
            /// </summary>
            ShiftDoctor,
            /// <summary>
            /// 特殊说明
            /// </summary>
            specialNote
        }
        #endregion

        #region 事件
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
            ////麻醉方式
            //if (e.Column == (int)Cols.anaeType)
            //{
            //    _Text = fpSpread1_Sheet1.ActiveCell.Text;
            //    lbAnaetype.Filter(_Text);

            //    if (lbAnaetype.Visible == false) lbAnaetype.Visible = true;
            //    fpSpread1_Sheet1.SetTag(e.Row, e.Column, null);
            //}
            ////助手、主麻
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

        #region IInterfaceContainer 成员

        public Type[] InterfaceTypes
        {
            get { return new Type[] { typeof(FS.HISFC.BizProcess.Interface.Operation.IArrangePrint) }; }
        }

        #endregion

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
