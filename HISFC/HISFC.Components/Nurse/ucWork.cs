using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FarPoint.Win.Spread;

namespace FS.HISFC.Components.Nurse
{
    /// <summary>
    /// [��������: �Ű�����Ű�ؼ�]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-09-18]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucWork : UserControl
    {
        public ucWork()
        {
            InitializeComponent();
            this.SetFpFormat();
            this.neuSpread1.Change += new FarPoint.Win.Spread.ChangeEventHandler(neuSpread1_Change);
            this.neuSpread1.EditModeOn += new EventHandler(this.neuSpread1_EditModeOn);

            this.neuSpread1.EditModeOff += new EventHandler(this.neuSpread1_EditModeOff);
            this.neuSpread1.EditChange += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.neuSpread1_EditChange);
        }

        #region ��
        protected enum cols
        {

            /// <summary>
            /// ���
            /// </summary>
            ID,
            /// <summary>
            /// ���Ҵ���
            /// </summary>
            DeptID,
            /// <summary>
            /// ��������
            /// </summary>
            DeptName,
            /// <summary>
            /// ��Ա����
            /// </summary>
            EmplCode,
            /// <summary>
            /// ��Ա����
            /// </summary>
            EmplName,
            /// <summary>
            /// ������
            /// </summary>
            NoonID,
            /// <summary>
            /// �������
            /// </summary>
            NoonName,
            /// <summary>
            /// ��ʼʱ��
            /// </summary>
            BeginTime,
            /// <summary>
            /// ����ʱ��
            /// </summary>
            EndTime,
            /// <summary>
            /// ��Ա���ID
            /// </summary>
            EmplTypeID,
            /// <summary>
            /// ��Ա�������
            /// </summary>
            EmplTypeName,
            /// <summary>
            /// �Ƿ���Ч
            /// </summary>
            IsValid,
            /// <summary>
            /// ��ע
            /// </summary>
            Remark,
            /// <summary>
            /// ԭ�����
            /// </summary>
            ReasonID,
            /// <summary>
            /// ԭ������
            /// </summary>
            ReasonName
        }
        #endregion

        #region ��
        /// <summary>
        /// �Ű�ģ����
        /// </summary>
        protected FS.HISFC.BizLogic.Nurse.Work work = new FS.HISFC.BizLogic.Nurse.Work();
        /// <summary>
        /// ����б�
        /// </summary>
        private FS.FrameWork.WinForms.Controls.NeuListBoxPopup noonList = new FS.FrameWork.WinForms.Controls.NeuListBoxPopup();
        /// <summary>
        /// ԭ���б�
        /// </summary>
        private FS.FrameWork.WinForms.Controls.NeuListBoxPopup reasonList = new FS.FrameWork.WinForms.Controls.NeuListBoxPopup();
        /// <summary>
        /// ��������
        /// </summary>
        private string deptName;
        /// <summary>
        /// ģ�弯��
        /// </summary>
        private DataTable dsItems;
        private DataView dv;
        /// <summary>
        /// �б����ڵ�FP
        /// </summary>
        private FS.FrameWork.WinForms.Controls.NeuSpread fpSpread;
        /// <summary>
        /// ��ǰ��Ա
        /// </summary>
        private FS.HISFC.Models.Base.Employee currentPerson;
        /// <summary>
        /// �Ű�����
        /// </summary>
        private DateTime arrangeDate = DateTime.MinValue;
        /// <summary>
        /// ��ʾ����
        /// </summary>
        private DayOfWeek week = DayOfWeek.Monday;
        /// <summary>
        /// ��ǰ����
        /// </summary>
        private FS.HISFC.Models.Base.Department dept = new FS.HISFC.Models.Base.Department();
        /// <summary>
        /// ��ǰ��Ա����
        /// </summary>
        private FS.HISFC.Models.Base.EnumEmployeeType emplType = new FS.HISFC.Models.Base.EnumEmployeeType();
        /// <summary>
        /// ԤԼĬ��ʱ���
        /// </summary>
        private int timeZone = 0;
        /// ����
        /// </summary>
        private ArrayList al;
        /// <summary>
        /// ���ҹ�����
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager Mgr = new FS.HISFC.BizProcess.Integrate.Manager();

        #endregion

        #region ����
        /// <summary>
        /// ��ʾ����
        /// </summary>
        /// 
        public DayOfWeek Week
        {
            get { return week; }
            set { week = value; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string DeptName
        {
            get
            {
                return this.deptName;
            }
            set
            {
                this.deptName = value;
            }
        }
        /// <summary>
        /// ��ǰ����
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
        /// ��ǰ�Ű���Ա
        /// </summary>
        public FS.HISFC.Models.Base.Employee CurrentPerson
        {
            get
            {
                return this.currentPerson;
            }
            set
            {
                this.currentPerson = value;
            }
        }
        /// <summary>
        /// ��ǰfarpoint
        /// </summary>
        public FS.FrameWork.WinForms.Controls.NeuSpread FpSpread
        {
            get
            {
                return this.neuSpread1;
            }
        }
        /// <summary>
        /// �Ű�����
        /// </summary>
        public DateTime ArrangeDate
        {
            get { return arrangeDate; }
            set { arrangeDate = value.Date; }
        }
        
        #endregion

        #region ��ʼ��

        /// <summary>
        ///  ��ʼ��
        /// </summary>        
        /// <param name="w"></param>
        /// <param name="type"></param>
        public void Init(DayOfWeek w)
        {
            this.week = w;

            this.initDataSet();

            this.initNoon();
            this.initStopRn();

            this.visible(false);

            this.initFp();
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
				new DataColumn("EmplCode",System.Type.GetType("System.String")),
				new DataColumn("EmplName",System.Type.GetType("System.String")),
                new DataColumn("NoonID",System.Type.GetType("System.String")),
                new DataColumn("NoonName",System.Type.GetType("System.String")),
                new DataColumn("BeginTime",System.Type.GetType("System.DateTime")),
				new DataColumn("EndTime",System.Type.GetType("System.DateTime")),
                new DataColumn("EmplTypeID",System.Type.GetType("System.String")),
                new DataColumn("EmplTypeName",System.Type.GetType("System.String")),
                new DataColumn("IsValid",System.Type.GetType("System.Boolean")),
                new DataColumn("Remark",System.Type.GetType("System.String")),
                new DataColumn("ReasonID",System.Type.GetType("System.String")),
				new DataColumn("ReasonName",System.Type.GetType("System.String"))
			});
        }

        /// <summary>
        /// ��ʼ������б�
        /// </summary>
        private void initNoon()
        {
            this.noonList.ItemSelected += new EventHandler(noonList_SelectItem);
            this.groupBox1.Controls.Add(this.noonList);
            this.noonList.BackColor = this.label1.BackColor;
            this.noonList.Font = new Font("����", 11F);
            this.noonList.BorderStyle = BorderStyle.None;
            this.noonList.Cursor = Cursors.Hand;
            this.noonList.Location = new Point(this.label1.Left + 1, this.label1.Top + 1);
            this.noonList.Size = new Size(this.label1.Width - 2, this.label1.Height - 2);

            FS.HISFC.BizLogic.Registration.Noon noonMgr =
                            new FS.HISFC.BizLogic.Registration.Noon();
            //�õ����
            al = noonMgr.Query();

            if (al == null)
            {
                MessageBox.Show("��ȡ�����Ϣʱ����!" + Mgr.Err, "��ʾ");
                return;
            }

            this.noonList.AddItems(al);
        }

        /// <summary>
        /// ��ʼ��ԭ���б�
        /// </summary>
        private void initStopRn()
        {
            this.reasonList.ItemSelected += new EventHandler(lbStopRn_SelectItem);
            this.groupBox1.Controls.Add(this.reasonList);
            this.reasonList.BackColor = this.label1.BackColor;
            this.reasonList.Font = new Font("����", 11F);
            this.reasonList.BorderStyle = BorderStyle.None;
            this.reasonList.Cursor = Cursors.Hand;
            this.reasonList.Location = new Point(this.label1.Left + 1, this.label1.Top + 1);
            this.reasonList.Size = new Size(this.label1.Width - 2, this.label1.Height - 2);

            //�õ�ҽ�����
            al = Mgr.QueryConstantList("StopReason");
            if (al == null)
            {
                MessageBox.Show("��ȡԭ��ʱ����!" + Mgr.Err, "��ʾ");
                return;
            }

            this.reasonList.AddItems(al);
        }

        /// <summary>
        /// ��ʼ��farpoint
        /// </summary>
        private void initFp()
        {
            FarPoint.Win.Spread.InputMap im;

            im = this.neuSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Down, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.neuSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Up, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.neuSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.neuSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Escape, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.neuSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.F2, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.neuSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.F3, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
        }

        #endregion

        #region ����

        /// <summary>
        /// ����������õ��������
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string getNoonNameByID(string id)
        {
            foreach (FS.FrameWork.Models.NeuObject obj in this.noonList.NeuItems)
            {
                if (obj.ID == id) return obj.Name;
            }
            return id;
        }
        /// <summary>
        /// ����������Ƶõ�������
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string getNoonIDByName(string name)
        {
            foreach (FS.FrameWork.Models.NeuObject obj in this.noonList.NeuItems)
            {
                if (obj.Name == name) return obj.ID;
            }
            return "";
        }
        /// <summary>
        /// ��ȡ������ʱ��
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private DateTime getNoonEndDate(string ID)
        {
            foreach (FS.FrameWork.Models.NeuObject obj in this.noonList.NeuItems)
            {
                if (obj.ID == ID) return (obj as FS.HISFC.Models.Base.Noon).EndTime;
            }
            return DateTime.MinValue;
        }
        private string GetEmplTypeName(string emplTypeID)
        {
            FS.HISFC.Models.Base.EmployeeTypeEnumService emplTypeService = new FS.HISFC.Models.Base.EmployeeTypeEnumService();
            return emplTypeService.GetName((FS.HISFC.Models.Base.EnumEmployeeType)Enum.Parse(typeof(FS.HISFC.Models.Base.EnumEmployeeType), emplTypeID));
        }
        #endregion

        #region Farpoint����

        /// <summary>
        /// ���������б�λ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_EditModeOn(object sender, EventArgs e)
        {

            this.neuSpread1.EditingControl.KeyDown += new KeyEventHandler(EditingControl_KeyDown);
            this.neuSpread1.EditingControl.DoubleClick += new EventHandler(EditingControl_DoubleClick);

            if (this.neuSpread1_Sheet1.ActiveColumnIndex == (int)cols.NoonName ||
                this.neuSpread1_Sheet1.ActiveColumnIndex == (int)cols.ReasonName)
            {
                this.setLocation();
                this.visible(false);
            }
            else
            { this.visible(false); }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_Change(object sender, FarPoint.Win.Spread.ChangeEventArgs e)
        {

            string cellText = this.neuSpread1_Sheet1.GetText(this.neuSpread1_Sheet1.ActiveRowIndex, this.neuSpread1_Sheet1.ActiveColumnIndex);

            if (cellText.ToUpper() == "FALSE")
            {
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)cols.BeginTime].BackColor = Color.MistyRose;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)cols.EndTime].BackColor = Color.MistyRose;
            }
            else
            {
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)cols.BeginTime].BackColor = SystemColors.Window;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)cols.EndTime].BackColor = SystemColors.Window;
            }

        }

        /// <summary>
        /// �ͷ���Դ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_EditModeOff(object sender, EventArgs e)
        {
            try
            {
                this.neuSpread1.EditingControl.KeyDown -= new KeyEventHandler(EditingControl_KeyDown);
                this.neuSpread1.EditingControl.DoubleClick -= new EventHandler(EditingControl_DoubleClick);
            }
            catch { }

        }

        /// <summary>
        /// ���������б�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_EditChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {

            int col = this.neuSpread1_Sheet1.ActiveColumnIndex;
            int row = this.neuSpread1_Sheet1.ActiveRowIndex;

            string text = this.neuSpread1_Sheet1.ActiveCell.Text.Trim();
            text = FS.FrameWork.Public.String.TakeOffSpecialChar(text, "#", "%", "[", "'", "]", ",", "$", "(", ")", "|", "\'", "\"", "\\");
            if (col == (int)cols.NoonName)
            {
                this.noonList.Filter(text);
                if (this.groupBox1.Visible == false) this.visible(true);
            }
            else if (col == (int)cols.ReasonName)
            {
                this.neuSpread1_Sheet1.Cells[row, (int)cols.ReasonName].Text = "";
                this.reasonList.Filter(text);
                if (this.groupBox1.Visible == false) this.visible(true);
            }
        }

        #endregion

        #region ��������

        /// <summary>
        /// �س�����
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                #region enter
                if (this.neuSpread1.ContainsFocus)
                {
                    int col = this.neuSpread1_Sheet1.ActiveColumnIndex;

                    if (col == (int)cols.DeptName)
                    {
                        this.neuSpread1_Sheet1.SetActiveCell(this.neuSpread1_Sheet1.ActiveRowIndex, (int)cols.EmplName, false);
                    }
                    else if (col == (int)cols.EmplName)
                    {
                        this.neuSpread1_Sheet1.SetActiveCell(this.neuSpread1_Sheet1.ActiveRowIndex, (int)cols.NoonName, false);
                    }
                    else if (col == (int)cols.NoonName)
                    {
                        if (this.selectNoon() == -1) return false;
                        this.neuSpread1_Sheet1.SetActiveCell(this.neuSpread1_Sheet1.ActiveRowIndex, (int)cols.BeginTime, false);
                    }
                    else if (col == (int)cols.BeginTime)
                    {
                        this.neuSpread1_Sheet1.SetActiveCell(this.neuSpread1_Sheet1.ActiveRowIndex, (int)cols.EndTime, false);
                    }
                    else if (col == (int)cols.EndTime)
                    {
                        this.neuSpread1_Sheet1.SetActiveCell(this.neuSpread1_Sheet1.ActiveRowIndex, (int)cols.IsValid, false);
                    }
                    else if (col == (int)cols.IsValid)
                    {
                        this.neuSpread1_Sheet1.SetActiveCell(this.neuSpread1_Sheet1.ActiveRowIndex, (int)cols.Remark, false);
                    }
                    else if (col == (int)cols.Remark)
                    {
                        this.neuSpread1_Sheet1.SetActiveCell(this.neuSpread1_Sheet1.ActiveRowIndex, (int)cols.ReasonName, false);
                    }
                    else if (col == (int)cols.ReasonName)
                    {
                        if (this.selectStopRn() == -1) return false;

                        if (this.neuSpread1_Sheet1.ActiveRowIndex != this.neuSpread1_Sheet1.RowCount - 1)
                        {
                            this.neuSpread1_Sheet1.ActiveRowIndex++;
                            this.neuSpread1_Sheet1.SetActiveCell(this.neuSpread1_Sheet1.ActiveRowIndex, (int)cols.DeptName, false);
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
                if (this.neuSpread1.ContainsFocus)
                {
                    if (this.groupBox1.Visible)
                    { this.priorRow(); }
                    else
                    {
                        int CurrentRow = neuSpread1_Sheet1.ActiveRowIndex;
                        if (CurrentRow > 0)
                        {
                            neuSpread1_Sheet1.ActiveRowIndex = CurrentRow - 1;
                            neuSpread1_Sheet1.AddSelection(CurrentRow - 1, 0, 1, 0);
                        }
                    }
                    return true;
                }
                #endregion

            }
            else if (keyData == Keys.Down)
            {
                #region down
                if (this.neuSpread1.ContainsFocus)
                {
                    if (this.groupBox1.Visible)
                    { this.nextRow(); }
                    else
                    {
                        int CurrentRow = neuSpread1_Sheet1.ActiveRowIndex;
                        if (CurrentRow >= 0 && CurrentRow <= neuSpread1_Sheet1.RowCount - 2)
                        {
                            neuSpread1_Sheet1.ActiveRowIndex = CurrentRow + 1;
                            neuSpread1_Sheet1.AddSelection(CurrentRow + 1, 0, 1, 0);
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

        private void EditingControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                FarPoint.Win.Spread.CellType.GeneralEditor t = neuSpread1.EditingControl as FarPoint.Win.Spread.CellType.GeneralEditor;
                if (t.SelectionStart == 0 && t.SelectionLength == 0)
                {
                    int row = 0, column = 0;
                    if (neuSpread1_Sheet1.ActiveColumnIndex == (int)cols.DeptName && neuSpread1_Sheet1.ActiveRowIndex != 0)
                    {
                        row = neuSpread1_Sheet1.ActiveRowIndex - 1;
                        column = (int)cols.ReasonName;
                    }
                    else if (neuSpread1_Sheet1.ActiveColumnIndex != (int)cols.DeptName)
                    {
                        row = neuSpread1_Sheet1.ActiveRowIndex;
                        column = this.getPriorVisibleColumn(this.neuSpread1_Sheet1.ActiveColumnIndex);

                    }
                    if (column != -1)
                    {
                        //����ѹ����ʾ����

                        if ((column == (int)cols.DeptName || column == (int)cols.EmplName ||
                            column == (int)cols.NoonName) && dv[row].Row.RowState != DataRowState.Added)
                        {
                            return;
                        }

                        neuSpread1_Sheet1.SetActiveCell(row, column, true);
                    }
                }
            }
            if (e.KeyCode == Keys.Right)
            {
                FarPoint.Win.Spread.CellType.GeneralEditor t = neuSpread1.EditingControl as FarPoint.Win.Spread.CellType.GeneralEditor;

                if (t.Text == null || t.Text == "" || t.SelectionStart == t.Text.Length)
                {
                    int row = neuSpread1_Sheet1.RowCount - 1, column = neuSpread1_Sheet1.ColumnCount - 1;
                    if (neuSpread1_Sheet1.ActiveColumnIndex == column && neuSpread1_Sheet1.ActiveRowIndex != row)
                    {
                        row = neuSpread1_Sheet1.ActiveRowIndex + 1;

                        column = (int)cols.DeptName;

                    }
                    else if (neuSpread1_Sheet1.ActiveColumnIndex != column)
                    {
                        row = neuSpread1_Sheet1.ActiveRowIndex;

                        column = this.getNextVisibleColumn(this.neuSpread1_Sheet1.ActiveColumnIndex);
                    }

                    if (column != -1)
                    {
                        //����ѹ����ʾ����
                        if ((column == (int)cols.DeptName || column == (int)cols.EmplName ||
                            column == (int)cols.NoonName) && dv[row].Row.RowState != DataRowState.Added)
                        {
                            return;
                        }

                        neuSpread1_Sheet1.SetActiveCell(row, column, true);
                    }
                }
            }
        }

        private void EditingControl_DoubleClick(object sender, EventArgs e)
        {
            int row = this.neuSpread1_Sheet1.ActiveRowIndex;

            if (row < 0) return;

            int col = this.neuSpread1_Sheet1.ActiveColumnIndex;

            if (col == (int)cols.BeginTime || col == (int)cols.EndTime)
            {
                //��ʾ��״̬
                this.showColor(row);
            }
            else if (col == (int)cols.NoonName)
            {
                string deptId, emplCode, noonID;

                deptId = this.neuSpread1_Sheet1.GetText(row, (int)cols.DeptID);
                emplCode = this.neuSpread1_Sheet1.GetText(row, (int)cols.EmplCode);
                noonID = this.neuSpread1_Sheet1.GetText(row, (int)cols.NoonID);

                //�����������Ϊ��ͬ״̬
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    if (this.neuSpread1_Sheet1.GetText(i, (int)cols.DeptID) == deptId &&
                        this.neuSpread1_Sheet1.GetText(i, (int)cols.EmplCode) == emplCode &&
                        this.neuSpread1_Sheet1.GetText(i, (int)cols.NoonID) == noonID)
                    {
                        //��ʾ��״̬
                        this.showColor(i);
                    }
                }
            }
        }
        /// <summary>
        /// ��������ʾ��ɫ--
        /// </summary>
        /// <param name="row"></param>
        private void showColor(int row)
        {
            string empltype = this.neuSpread1_Sheet1.GetText(row, (int)cols.EmplTypeID);
            //��ʾ��״̬
            if (empltype == "C")
            {
                this.neuSpread1_Sheet1.Cells[row, (int)cols.EmplTypeID].BackColor = SystemColors.Window;
            }
            else if (empltype == "T")
            {
                this.neuSpread1_Sheet1.Cells[row, (int)cols.EmplTypeID].BackColor = Color.MistyRose;
            }
            else if (empltype == "P")
            {
                this.neuSpread1_Sheet1.Cells[row, (int)cols.EmplTypeID].BackColor = Color.MintCream;
            }
            else if (empltype == "F")
            {
                this.neuSpread1_Sheet1.Cells[row, (int)cols.EmplTypeID].BackColor = Color.Moccasin;
            }
            else if (empltype == "N")
            {
                this.neuSpread1_Sheet1.Cells[row, (int)cols.EmplTypeID].BackColor = Color.MidnightBlue;
            }
            else if (empltype == "D")
            {
                this.neuSpread1_Sheet1.Cells[row, (int)cols.EmplTypeID].BackColor = Color.MediumSpringGreen;
            }
            else if (empltype == "O")
            {
                this.neuSpread1_Sheet1.Cells[row, (int)cols.EmplTypeID].BackColor = Color.NavajoWhite;
            }
        }
        /// <summary>
        /// ��������ʾ��ɫ��ȫ����
        /// </summary>
        private void showColor()
        {
            int rowCount = this.neuSpread1_Sheet1.RowCount;
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
            int count = this.neuSpread1_Sheet1.Columns.Count;

            while (col < count - 1)
            {
                col++;

                if (this.neuSpread1_Sheet1.Columns[col].Visible)
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

                if (this.neuSpread1_Sheet1.Columns[col].Visible)
                {
                    return col;
                }
            }

            return -1;
        }


        #region �й����ĺ������¼�
        /// <summary>
        /// ѡ�����
        /// </summary>
        /// <param name="sender"></param>
        ///<param name="e"></param>
        private void noonList_SelectItem(object sender, EventArgs e)
        {
            this.selectNoon();
            this.visible(false);
            return;
        }

        #endregion

        #endregion

        #region �����ؼ�����
        /// <summary>
        /// ����groupBox1����ʾλ��
        /// </summary>
        /// <returns></returns>
        private void setLocation()
        {
            Control cell = this.neuSpread1.EditingControl;
            if (cell == null) return;

            int y = neuSpread1.Top + cell.Top + cell.Height + this.groupBox1.Height + 7;
            if (y <= this.Height)
            {
                if (neuSpread1.Left + cell.Left + this.groupBox1.Width + 20 <= this.Width)
                {
                    this.groupBox1.Location = new Point(neuSpread1.Left + cell.Left + 20, y - this.groupBox1.Height);
                }
                else
                {
                    this.groupBox1.Location = new Point(this.Width - this.groupBox1.Width - 10, y - this.groupBox1.Height);
                }
            }
            else
            {
                if (neuSpread1.Left + cell.Left + this.groupBox1.Width + 20 <= this.Width)
                {
                    this.groupBox1.Location = new Point(neuSpread1.Left + cell.Left + 20, neuSpread1.Top + cell.Top - this.groupBox1.Height - 7);
                }
                else
                {
                    this.groupBox1.Location = new Point(this.Width - this.groupBox1.Width - 10, neuSpread1.Top + cell.Top - this.groupBox1.Height - 7);
                }
            }
        }

        /// <summary>
        /// ���ÿؼ��Ƿ�ɼ�
        /// </summary>
        /// <param name="visible"></param>
        private void visible(bool visible)
        {
            if (visible == false)
            { this.groupBox1.Visible = false; }
            else
            {
                int col = this.neuSpread1_Sheet1.ActiveColumnIndex;
                if (col == (int)cols.NoonName)
                {
                    this.noonList.BringToFront();
                    this.groupBox1.Visible = true;
                }
                else if (col == (int)cols.ReasonName)
                {
                    this.reasonList.BringToFront();
                    this.groupBox1.Visible = true;
                }
            }
        }
        /// <summary>
        /// ǰһ��
        /// </summary>
        private void nextRow()
        {
            int col = this.neuSpread1_Sheet1.ActiveColumnIndex;
            if (col == (int)cols.NoonName)
            {
                this.noonList.NextRow();
            }
            else if (col == (int)cols.ReasonName)
            {
                this.reasonList.NextRow();
            }
        }
        /// <summary>
        /// ��һ��
        /// </summary>
        private void priorRow()
        {
            int col = this.neuSpread1_Sheet1.ActiveColumnIndex;
            if (col == (int)cols.NoonName)
            {
                this.noonList.PriorRow();
            }
            else if (col == (int)cols.ReasonName)
            {
                this.reasonList.PriorRow();
            }
        }
        /// <summary>
        /// ѡ�����
        /// </summary>
        /// <returns></returns>
        private int selectNoon()
        {
            int row = this.neuSpread1_Sheet1.ActiveRowIndex;
            FS.FrameWork.Models.NeuObject obj;

            this.neuSpread1.StopCellEditing();

            if (this.groupBox1.Visible)
            {
                obj = new FS.FrameWork.Models.NeuObject();

                obj = this.noonList.GetSelectedItem();
                if (obj == null) return -1;

                this.neuSpread1_Sheet1.SetValue(row, (int)cols.NoonID, obj.ID, false);
                this.neuSpread1_Sheet1.SetValue(row, (int)cols.NoonName, obj.Name, false);

                if (this.timeZone == 0)
                {
                    //�趨Ĭ��ʱ��Ϊ������ʼ������ʱ��
                    this.neuSpread1_Sheet1.SetValue(row, (int)cols.BeginTime,
                            (obj as FS.HISFC.Models.Base.Noon).StartTime, false);
                    this.neuSpread1_Sheet1.SetValue(row, (int)cols.EndTime,
                            (obj as FS.HISFC.Models.Base.Noon).EndTime, false);
                }
                else//ģ��Ĭ�ϴ����ҵ��ĵ�һ������ʱ��Ϊ��ʼʱ��,����ʱ��+1
                {
                    this.SetTimeZone(row, (FS.HISFC.Models.Base.Noon)obj);
                }
                this.visible(false);
            }

            return 0;
        }
        /// <summary>
        /// �����Ű�Ĭ��ʱ���
        /// </summary>
        /// <param name="currentRow"></param>
        /// <param name="noon"></param>
        /// <returns></returns>
        private int SetTimeZone(int currentRow, FS.HISFC.Models.Base.Noon noon)
        {
            string emplCode = this.neuSpread1_Sheet1.GetText(currentRow, (int)cols.EmplCode);
            string deptID = this.neuSpread1_Sheet1.GetText(currentRow, (int)cols.DeptID);
            DateTime begin = DateTime.MinValue;
            object obj;

            if (emplCode == "") return 0;

            for (int i = currentRow; i >= 0; i--)
            {
                if (i == currentRow) continue;

                if (this.neuSpread1_Sheet1.GetText(i, (int)cols.EmplCode) == emplCode &&
                    this.neuSpread1_Sheet1.GetText(i, (int)cols.NoonID) == noon.ID &&
                    this.neuSpread1_Sheet1.GetText(i, (int)cols.DeptID) == deptID)
                {
                    obj = this.neuSpread1_Sheet1.GetValue(i, (int)cols.EndTime);
                    if (obj == null) continue;

                    begin = DateTime.Parse(obj.ToString());
                    break;
                }
            }

            if (begin != DateTime.MinValue && begin.TimeOfDay < noon.EndTime.TimeOfDay)
            {
                this.neuSpread1_Sheet1.SetValue(currentRow, (int)cols.BeginTime, begin, false);
                begin = begin.AddHours(this.timeZone);

                if (begin.TimeOfDay > noon.EndTime.TimeOfDay)
                {
                    begin = noon.EndTime;
                }
                this.neuSpread1_Sheet1.SetValue(currentRow, (int)cols.EndTime, begin, false);
            }
            else
            {
                begin = noon.StartTime;
                this.neuSpread1_Sheet1.SetValue(currentRow, (int)cols.BeginTime, begin, false);

                begin = begin.AddHours(this.timeZone);
                if (begin.TimeOfDay > noon.EndTime.TimeOfDay)
                {
                    begin = noon.EndTime;
                }
                this.neuSpread1_Sheet1.SetValue(currentRow, (int)cols.EndTime, begin, false);
            }

            return 0;
        }

        /// <summary>
        /// ѡ���б�ԭ��
        /// </summary>
        /// <returns></returns>
        private int selectStopRn()
        {
            int row = this.neuSpread1_Sheet1.ActiveRowIndex;
            FS.FrameWork.Models.NeuObject obj;

            this.neuSpread1.StopCellEditing();

            if (this.groupBox1.Visible)
            {
                obj = new FS.FrameWork.Models.NeuObject();
                obj = this.reasonList.GetSelectedItem();
                if (obj == null) return -1;

                this.neuSpread1_Sheet1.SetValue(row, (int)cols.ReasonName, obj.Name, false);
                this.neuSpread1_Sheet1.SetValue(row, (int)cols.ReasonID, obj.ID, false);
                this.visible(false);
            }
            else
            {
                string StopRnID = this.neuSpread1_Sheet1.GetText(row, (int)cols.ReasonID);

                if (StopRnID == null || StopRnID == "")
                    this.neuSpread1_Sheet1.SetValue(row, (int)cols.ReasonName, "", false);
            }

            return 0;
        }

        /// <summary>
        /// ѡ�����
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
        /// ѡ��ԭ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbStopRn_SelectItem(object sender, EventArgs e)
        {
            this.selectStopRn();
            this.visible(false);

            return;
        }

        #endregion

        #region ���к���
        /// <summary>
        /// ���һ��ģ���¼
        /// </summary>
        /// <param name="templet"></param>
        public void Add(FS.HISFC.Models.Nurse.WorkTemplet templet)
        {
            DataRowView dr = dv.AddNew();

            dr["ID"] = this.work.GetSequence("Nurse.Work.ID");
            dr["DeptID"] = templet.Dept.ID;
            dr["DeptName"] = templet.Dept.Name;
            dr["EmplCode"] = templet.Employee.ID;
            dr["EmplName"] = templet.Employee.Name;
            dr["NoonID"] = templet.Noon.ID;
            dr["NoonName"] = this.getNoonNameByID(templet.Noon.ID);
            dr["BeginTime"] = templet.Begin;
            dr["EndTime"] = templet.End;
            dr["EmplTypeID"] = templet.EmplType.ID;
            dr["EmplTypeName"] = this.GetEmplTypeName(templet.EmplType.ID);
            dr["IsValid"] = templet.IsValid;
            dr["Remark"] = FS.FrameWork.Public.String.TakeOffSpecialChar(templet.Memo, "#", "%", "[", "'", "]", ",", "$", "(", ")", "|", "\'", "\"", "\\", "*", "^", "@", "!");
            dr["ReasonID"] = templet.Reason.ID;
            dr["ReasonName"] = FS.FrameWork.Public.String.TakeOffSpecialChar(templet.Reason.Name, "#", "%", "[", "'", "]", ",", "$", "(", ")", "|", "\'", "\"", "\\", "*", "^", "@", "!");

            dr.EndEdit();

        }
        /// <summary>
        /// ����Ű���Ϣ
        /// </summary>
        /// <param name="templet"></param>
        public void Add(FS.HISFC.Models.Nurse.Work templet)
        {
            DataRowView dr = dv.AddNew();

            dr["ID"] = this.work.GetSequence("Nurse.Work.ID");
            dr["DeptID"] = templet.Templet.Dept.ID;
            dr["DeptName"] = templet.Templet.Dept.Name;
            dr["EmplCode"] = templet.Templet.Employee.ID;
            dr["EmplName"] = templet.Templet.Employee.Name;
            dr["NoonID"] = templet.Templet.Noon.ID;
            dr["NoonName"] = this.getNoonNameByID(templet.Templet.Noon.ID);
            dr["BeginTime"] = templet.Templet.Begin;
            dr["EndTime"] = templet.Templet.End;
            dr["EmplTypeID"] = templet.Templet.EmplType.ID;
            dr["EmplTypeName"] = this.GetEmplTypeName(templet.Templet.EmplType.ID);
            dr["IsValid"] = templet.Templet.IsValid;
            dr["Remark"] = FS.FrameWork.Public.String.TakeOffSpecialChar(templet.Templet.Memo, "#", "%", "[", "'", "]", ",", "$", "(", ")", "|", "\'", "\"", "\\", "*", "^", "@", "!");
            dr["ReasonID"] = templet.Templet.Reason.ID;
            dr["ReasonName"] = FS.FrameWork.Public.String.TakeOffSpecialChar(templet.Templet.Reason.Name, "#", "%", "[", "'", "]", ",", "$", "(", ")", "|", "\'", "\"", "\\", "*", "^", "@", "!");

            dr.EndEdit();

        }
        /// <summary>
        /// �����Ҳ�ѯһ���Ű��¼
        /// </summary>
        /// <param name="deptID"></param>
        public void Query(string deptID)
        {
            this.al = this.work.Query(this.ArrangeDate, deptID);
            if (al == null)
            {
                MessageBox.Show("��ѯ�Ű�ģ����Ϣ����!" + this.work.Err, "��ʾ");
                return;
            }

            dsItems.Rows.Clear();

            try
            {
                foreach (FS.HISFC.Models.Nurse.Work tempWork in al)
                {

                    dsItems.Rows.Add(new object[]
					{
						tempWork.Templet.ID,						
						tempWork.Templet.Dept.ID,
						tempWork.Templet.Dept.Name,
						tempWork.Templet.Employee.ID,
						tempWork.Templet.Employee.Name,
                        tempWork.Templet.Noon.ID,
						this.getNoonNameByID(tempWork.Templet.Noon.ID),
						tempWork.Templet.Begin,
						tempWork.Templet.End,
						tempWork.Templet.EmplType.ID,
                        this.GetEmplTypeName(tempWork.Templet.EmplType.ID),//templet.EmplType.Name,
                        tempWork.Templet.IsValid,
						tempWork.Templet.Memo,
						tempWork.Templet.Reason.ID,
						tempWork.Templet.Reason.Name });
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("��ѯ�Ű���Ϣ����DataSetʱ����!" + e.Message, "��ʾ");
                return;
            }
            dsItems.AcceptChanges();

            dv = dsItems.DefaultView;
            dv.AllowDelete = true;
            dv.AllowEdit = true;
            dv.AllowNew = true;
            //this.fpSpread1_Sheet1.Rows.Remove(0, this.fpSpread1_Sheet1.Rows.Count);
            this.neuSpread1_Sheet1.DataSource = dv;
            this.neuSpread1_Sheet1.DataMember = "Templet";

            this.SetFpFormat();


            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    this.showColor(i);
                }
            }

        }

        /// <summary>
        /// ����Farpoint��ʾ��ʽ
        /// </summary>
        private void SetFpFormat()
        {
            FarPoint.Win.Spread.CellType.NumberCellType numbCellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            numbCellType.DecimalPlaces = 0;
            numbCellType.MaximumValue = 9999;
            numbCellType.MinimumValue = 0;

            FarPoint.Win.Spread.CellType.TextCellType txtOnly = new FarPoint.Win.Spread.CellType.TextCellType();
            txtOnly.ReadOnly = true;
            FarPoint.Win.Spread.CellType.DateTimeCellType dtCellType = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType cbCellType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            dtCellType.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.UserDefined;
            dtCellType.UserDefinedFormat = "HH:mm";

            FarPoint.Win.Spread.CellType.TextCellType txtCellType = new FarPoint.Win.Spread.CellType.TextCellType();
            txtCellType.ReadOnly = true;

            #region ""
            this.neuSpread1_Sheet1.Columns[(int)cols.ID].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)cols.DeptID].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)cols.NoonID].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)cols.ReasonID].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)cols.EmplCode].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)cols.EmplTypeID].Visible = false;

            this.neuSpread1_Sheet1.Columns[(int)cols.DeptName].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[(int)cols.EmplName].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[(int)cols.EmplTypeName].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[(int)cols.BeginTime].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[(int)cols.EndTime].CellType = txtOnly;

            this.neuSpread1_Sheet1.Columns[(int)cols.DeptName].BackColor = System.Drawing.Color.LightGray;
            this.neuSpread1_Sheet1.Columns[(int)cols.EmplName].BackColor = System.Drawing.Color.LightGray; 
            this.neuSpread1_Sheet1.Columns[(int)cols.EmplTypeName].BackColor = System.Drawing.Color.LightGray; 
            //this.neuSpread1_Sheet1.Columns[(int)cols.BeginTime].BackColor = System.Drawing.Color.LightGray; 
            //this.neuSpread1_Sheet1.Columns[(int)cols.EndTime].BackColor = System.Drawing.Color.LightGray; 

            this.neuSpread1_Sheet1.Columns[(int)cols.BeginTime].CellType = dtCellType;
            this.neuSpread1_Sheet1.Columns[(int)cols.EndTime].CellType = dtCellType;
            this.neuSpread1_Sheet1.Columns[(int)cols.IsValid].CellType = cbCellType;

            this.neuSpread1_Sheet1.Columns[(int)cols.DeptName].Width = 90F;
            this.neuSpread1_Sheet1.Columns[(int)cols.EmplName].Width = 60F;
            this.neuSpread1_Sheet1.Columns[(int)cols.EmplTypeName].Width = 60F;
            this.neuSpread1_Sheet1.Columns[(int)cols.NoonName].Width = 40F;
            this.neuSpread1_Sheet1.Columns[(int)cols.BeginTime].Width = 90F;
            this.neuSpread1_Sheet1.Columns[(int)cols.EndTime].Width = 90F;
            this.neuSpread1_Sheet1.Columns[(int)cols.Remark].Width = 40F;
            this.neuSpread1_Sheet1.Columns[(int)cols.ReasonName].Width = 80F;

            #endregion
        }
        /// <summary>
        /// ��ֹ�ظ���ʾ
        /// </summary>
        private void Span()
        {
            ///
            int colLastDept = 0;
            int colLastEmpl = 0;
            int colLastNoon = 0;
            int rowCnt = this.neuSpread1_Sheet1.RowCount;

            FarPoint.Win.Spread.CellType.TextCellType txtCellType = new FarPoint.Win.Spread.CellType.TextCellType();
            txtCellType.ReadOnly = true;

            for (int i = 0; i < rowCnt; i++)
            {
                this.showColor(i);

                if (i > 0 && this.neuSpread1_Sheet1.GetText(i, (int)cols.DeptName) != this.neuSpread1_Sheet1.GetText(i - 1, (int)cols.DeptName))
                {
                    if (i - colLastDept > 1)
                    {
                        this.neuSpread1_Sheet1.Models.Span.Add(colLastDept, (int)cols.DeptName, i - colLastDept, 1);
                    }

                    colLastDept = i;
                }

                //���һ�д���
                if (i > 0 && i == rowCnt - 1 && this.neuSpread1_Sheet1.GetText(i, (int)cols.DeptName) == this.neuSpread1_Sheet1.GetText(i - 1, (int)cols.DeptName))
                {
                    this.neuSpread1_Sheet1.Models.Span.Add(colLastDept, (int)cols.DeptName, i - colLastDept + 1, 1);
                }

                if (
                    i > 0 &&
                    this.neuSpread1_Sheet1.GetText(i, (int)cols.EmplName) != this.neuSpread1_Sheet1.GetText(i - 1, (int)cols.EmplName))
                {
                    if (i - colLastEmpl > 1)
                    {
                        this.neuSpread1_Sheet1.Models.Span.Add(colLastEmpl, (int)cols.EmplName, i - colLastEmpl, 1);
                    }
                    colLastEmpl = i;
                }

                //���һ��
                if (i > 0 &&
                    i == rowCnt - 1 && this.neuSpread1_Sheet1.GetText(i, (int)cols.EmplName) == this.neuSpread1_Sheet1.GetText(i - 1, (int)cols.EmplName))
                {
                    this.neuSpread1_Sheet1.Models.Span.Add(colLastEmpl, (int)cols.EmplName, i - colLastEmpl + 1, 1);
                }

                ///���
                ///
                if (i > 0 &&
                    (this.neuSpread1_Sheet1.GetText(i, (int)cols.NoonName) != this.neuSpread1_Sheet1.GetText(i - 1, (int)cols.NoonName) ||
                    this.neuSpread1_Sheet1.GetText(i, (int)cols.DeptName) != this.neuSpread1_Sheet1.GetText(i - 1, (int)cols.DeptName) ||
                    this.neuSpread1_Sheet1.GetText(i, (int)cols.EmplName) != this.neuSpread1_Sheet1.GetText(i - 1, (int)cols.EmplName)))
                {
                    if (i - colLastNoon > 1)
                    {
                        this.neuSpread1_Sheet1.Models.Span.Add(colLastNoon, (int)cols.NoonName, i - colLastNoon, 1);
                    }
                    colLastNoon = i;
                }
                //���һ��
                if (i > 0 && i == rowCnt - 1 &&
                    (this.neuSpread1_Sheet1.GetText(i, (int)cols.NoonName) == this.neuSpread1_Sheet1.GetText(i - 1, (int)cols.NoonName) ||
                    this.neuSpread1_Sheet1.GetText(i, (int)cols.DeptName) == this.neuSpread1_Sheet1.GetText(i - 1, (int)cols.DeptName) ||
                    this.neuSpread1_Sheet1.GetText(i, (int)cols.EmplName) == this.neuSpread1_Sheet1.GetText(i - 1, (int)cols.EmplName)))
                {
                    this.neuSpread1_Sheet1.Models.Span.Add(colLastNoon, (int)cols.NoonName, i - colLastNoon + 1, 1);
                }

            }
        }
        /// <summary>
        /// farpoint���һ�У�����ʼ��һЩֵ
        /// </summary>
        public void Add()
        {
            if (this.CurrentPerson == null)
            {
                MessageBox.Show("��ѡ���Ű���Ա");
                return;
            }

            this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);

            this.neuSpread1_Sheet1.ActiveRowIndex = this.neuSpread1_Sheet1.RowCount - 1;
            int row = this.neuSpread1_Sheet1.ActiveRowIndex;

            DateTime current = this.work.GetDateTimeFromSysDateTime().Date;

            this.neuSpread1_Sheet1.SetValue(row, (int)cols.ID, this.work.GetSequence("Nurse.Work.ID"), false);



            this.neuSpread1_Sheet1.SetValue(row, (int)cols.EmplCode, this.CurrentPerson.ID, false);
            this.neuSpread1_Sheet1.SetValue(row, (int)cols.EmplName, this.CurrentPerson.Name, false);
            this.neuSpread1_Sheet1.SetValue(row, (int)cols.EmplTypeID, this.CurrentPerson.EmployeeType.ID, false);
            this.neuSpread1_Sheet1.SetValue(row, (int)cols.EmplTypeName, this.CurrentPerson.EmployeeType.Name, false);

            this.neuSpread1_Sheet1.SetValue(row, (int)cols.DeptID, this.CurrentPerson.Dept.ID, false);
            this.neuSpread1_Sheet1.SetValue(row, (int)cols.DeptName, this.DeptName, false);


            this.neuSpread1_Sheet1.SetValue(row, (int)cols.NoonID, "", false);
            this.neuSpread1_Sheet1.SetValue(row, (int)cols.NoonName, "", false);
            this.neuSpread1_Sheet1.SetValue(row, (int)cols.BeginTime, current, false);
            this.neuSpread1_Sheet1.SetValue(row, (int)cols.EndTime, current, false);
            this.neuSpread1_Sheet1.SetValue(row, (int)cols.IsValid, true, false);
            this.neuSpread1_Sheet1.SetValue(row, (int)cols.Remark, "", false);
            this.neuSpread1_Sheet1.SetValue(row, (int)cols.ReasonID, "", false);
            this.neuSpread1_Sheet1.SetValue(row, (int)cols.ReasonName, "", false);

            this.neuSpread1.Focus();

            string deptId = "";

            deptId = this.neuSpread1_Sheet1.GetText(this.neuSpread1_Sheet1.ActiveRowIndex, (int)cols.DeptID);


            if (deptId == null || deptId == "")
            {
                this.neuSpread1_Sheet1.SetActiveCell(this.neuSpread1_Sheet1.ActiveRowIndex, (int)cols.DeptName, false);
            }
            else
            {

                this.neuSpread1_Sheet1.SetActiveCell(this.neuSpread1_Sheet1.ActiveRowIndex, (int)cols.EmplName, false);

            }
        }
        /// <summary>
        /// ɾ��
        /// </summary>
        public void Del()
        {
            int row = this.neuSpread1_Sheet1.ActiveRowIndex;
            if (row < 0 || this.neuSpread1_Sheet1.RowCount == 0) return;

            if (MessageBox.Show("�Ƿ�ɾ�������Ű�?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                this.neuSpread1.Focus();
                return;
            }

            this.neuSpread1.StopCellEditing();

            this.neuSpread1_Sheet1.Rows.Remove(row, 1);

            this.neuSpread1.Focus();
        }
        /// <summary>
        /// ɾ��ȫ��
        /// </summary>
        public void DelAll()
        {
            if (this.neuSpread1_Sheet1.RowCount == 0) return;

            if (MessageBox.Show("�Ƿ�ɾ��ȫ���Ű�?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                this.neuSpread1.Focus();
                return;
            }

            this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);

            this.neuSpread1.Focus();
        }
        /// <summary>
        /// ɾ��ȫ��
        /// </summary>
        public void DelAllNoMessageBox()
        {
            if (this.neuSpread1_Sheet1.RowCount == 0) return;

            this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);

            this.neuSpread1.Focus();
        }
        /// <summary>
        /// ����
        /// </summary>
        public int Save()
        {
            this.neuSpread1.StopCellEditing();

            if (neuSpread1_Sheet1.RowCount > 0)
            {
                dsItems.Rows[neuSpread1_Sheet1.ActiveRowIndex].EndEdit();
            }
            //����
            DataTable dtAdd = dsItems.GetChanges(DataRowState.Added);
            //�޸�
            DataTable dtModify = dsItems.GetChanges(DataRowState.Modified);
            //ɾ��
            DataTable dtDel = dsItems.GetChanges(DataRowState.Deleted);

            //��֤
            if (Valid(dtAdd) == -1) return -1;
            if (Valid(dtModify) == -1) return -1;
            //תΪʵ�弯��
            ArrayList alAdd = this.GetChanges(dtAdd);
            if (alAdd == null) return -1;
            ArrayList alModify = this.GetChanges(dtModify);
            if (alModify == null) return -1;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //SQLCA.BeginTransaction();

            this.work.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            string rtnText = "";
            if (dtDel != null)
            {
                dtDel.RejectChanges();

                if (this.SaveDel(this.work, dtDel, ref rtnText) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(rtnText, "��ʾ");
                    return -1;
                }
            }

            if (this.SaveAdd(this.work, alAdd, ref rtnText) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(rtnText, "��ʾ");
                return -1;
            }

            if (this.SaveModify(this.work, alModify, ref rtnText) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(rtnText, "��ʾ");
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            dsItems.AcceptChanges();
            this.SetFpFormat();
            this.showColor();
            return 0;
        }
        /// <summary>
        /// �Ƿ��޸����ݣ�
        /// </summary>
        /// <returns></returns>
        public bool IsChange()
        {
            this.neuSpread1.StopCellEditing();

            if (neuSpread1_Sheet1.RowCount > 0)
            {
                dsItems.Rows[neuSpread1_Sheet1.ActiveRowIndex].EndEdit();
            }

            DataTable dt = dsItems.GetChanges();

            if (dt == null || dt.Rows.Count == 0)
            {
                return false;
            }

            return true;
        }
        /// <summary>
        /// �������ӵļ�¼
        /// </summary>
        /// <param name="templetMgr"></param>
        /// <param name="alAdd"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int SaveAdd(FS.HISFC.BizLogic.Nurse.Work templetMgr, ArrayList alAdd, ref string Err)
        {
            try
            {
                foreach (FS.HISFC.Models.Nurse.Work tempWork in alAdd)
                {
                    if (templetMgr.Insert(tempWork) == -1)
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
        /// �����޸ļ�¼
        /// </summary>
        /// <param name="templetMgr"></param>
        /// <param name="alModify"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int SaveModify(FS.HISFC.BizLogic.Nurse.Work templetMgr, ArrayList alModify, ref string Err)
        {
            try
            {
                foreach (FS.HISFC.Models.Nurse.Work tempWork in alModify)
                {
                    int returnValue = 0;

                    returnValue = templetMgr.Delete(tempWork.Templet.ID);

                    if (returnValue == -1)
                    {
                        Err = templetMgr.Err;
                        return -1;
                    }

                    if (templetMgr.Insert(tempWork) == -1)
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
        /// ����ɾ����¼
        /// </summary>
        /// <param name="templetMgr"></param>
        /// <param name="dvDel"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int SaveDel(FS.HISFC.BizLogic.Nurse.Work templetMgr, DataTable dvDel, ref string Err)
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
        /// ����������ת��Ϊʵ�弯��
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
                        FS.HISFC.Models.Nurse.Work tempWork = new FS.HISFC.Models.Nurse.Work();

                        tempWork.Templet.ID = row["ID"].ToString();
                        tempWork.Templet.Week = this.week;
                        tempWork.WorkDate = this.ArrangeDate;
                        tempWork.Templet.Dept.ID = row["DeptID"].ToString();
                        tempWork.Templet.Dept.Name = row["DeptName"].ToString();
                        tempWork.Templet.Employee.ID = row["EmplCode"].ToString();
                        tempWork.Templet.Employee.Name = row["EmplName"].ToString();
                        tempWork.Templet.EmplType.ID = row["EmplTypeID"].ToString();
                        tempWork.Templet.Begin = (DateTime)row["BeginTime"];
                        tempWork.Templet.End = (DateTime)row["EndTime"];
                        tempWork.Templet.Memo = row["Remark"].ToString();
                        tempWork.Templet.Noon.ID = row["NoonID"].ToString();
                        tempWork.Templet.Noon.Name = row["NoonName"].ToString();
                        tempWork.Templet.Reason.ID = row["ReasonID"].ToString();
                        tempWork.Templet.Reason.Name = row["ReasonName"].ToString();
                        tempWork.Templet.IsValid = Boolean.Parse(row["IsValid"].ToString());
                        tempWork.Templet.Oper.ID = FS.FrameWork.Management.Connection.Operator.ID;
                        this.al.Add(tempWork);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("����ʵ�弯��ʱ����!" + e.Message, "��ʾ");
                    return null;
                }
            }

            return al;
        }
        /// <summary>
        /// ��֤
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private int Valid(DataTable dt)
        {
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row["DeptID"].ToString() == null || row["DeptID"].ToString() == "")
                    {
                        MessageBox.Show("�Ű���Ҳ���Ϊ��!", "��ʾ");
                        return -1;
                    }


                    if (row["EmplCode"].ToString() == null || row["EmplCode"].ToString() == "")
                    {
                        MessageBox.Show("��Ա����Ϊ��!", "��ʾ");
                        return -1;
                    }

                    if (row["BeginTime"] == null || row["BeginTime"].ToString().Trim() == "" ||
                        row["EndTime"] == null || row["EndTime"].ToString().Trim() == "")
                    {
                        MessageBox.Show("������ԤԼʱ���!", "��ʾ");
                        return -1;
                    }
                    if (DateTime.Parse(row["BeginTime"].ToString()).TimeOfDay > DateTime.Parse(row["EndTime"].ToString()).TimeOfDay)
                    {
                        MessageBox.Show("��ʼʱ�䲻�ܴ��ڽ���ʱ��!", "��ʾ");
                        return -1;
                    }
                    if (row["IsValid"].ToString() == "True")
                    {
                        if (DateTime.Parse(row["BeginTime"].ToString()).TimeOfDay == DateTime.Parse(row["EndTime"].ToString()).TimeOfDay)
                        {
                            MessageBox.Show("��ʼʱ�䲻�ܵ��ڽ���ʱ��!", "��ʾ");
                            return -1;
                        }
                    }

                    string noon = row["NoonName"].ToString();//this.getNoonIDByName(row["NoonID"].ToString());
                    if (noon == "")
                    {
                        MessageBox.Show("�Ű������Ϊ��!", "��ʾ");
                        return -1;
                    }
                    if (FS.FrameWork.Public.String.ValidMaxLengh(row["Remark"].ToString(), 50) == false)
                    {
                        MessageBox.Show("��ע���ܳ���25������!", "��ʾ");
                        return -1;
                    }
                    row["Remark"] = FS.FrameWork.Public.String.TakeOffSpecialChar(row["Remark"].ToString(), "#", "%", "[", "'", "]", ",", "$", "(", ")", "|", "\'", "\"", "\\", "*", "^", "@", "!");
                }
                if (this.valid() < 0) return -1;

            }

            return 0;
        }
        /// <summary>
        /// �˶���û���ظ�ʱ���
        /// </summary>
        /// <returns></returns>
        private int valid()
        {

            if (this.neuSpread1_Sheet1.RowCount <= 0) return -1;
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount - 1; i++)
            {


                //if (this.neuSpread1_Sheet1.GetValue(i, (int)cols.IsValid).ToString() == "True")
                //{
                DateTime beginDTi = FS.FrameWork.Function.NConvert.ToDateTime(this.neuSpread1_Sheet1.GetValue(i, (int)cols.BeginTime));
                DateTime endDTi = FS.FrameWork.Function.NConvert.ToDateTime(this.neuSpread1_Sheet1.GetValue(i, (int)cols.EndTime));
                for (int j = i + 1; j < this.neuSpread1_Sheet1.RowCount; j++)
                {
                    //if (this.neuSpread1_Sheet1.GetValue(j, (int)cols.IsValid).ToString() == "True")
                    //{
                    DateTime beginDTj = FS.FrameWork.Function.NConvert.ToDateTime(this.neuSpread1_Sheet1.GetValue(j, (int)cols.BeginTime));
                    DateTime endDTj = FS.FrameWork.Function.NConvert.ToDateTime(this.neuSpread1_Sheet1.GetValue(j, (int)cols.EndTime));
                    if ((this.neuSpread1_Sheet1.GetValue(i, (int)cols.EmplCode).ToString() == this.neuSpread1_Sheet1.GetValue(j, (int)cols.EmplCode).ToString())
                        //&& (this.neuSpread1_Sheet1.GetValue(i, (int)cols.NoonName).ToString() == this.neuSpread1_Sheet1.GetValue(j, (int)cols.NoonName).ToString()) 
                        && ((((beginDTj.TimeOfDay <= beginDTi.TimeOfDay && beginDTj.TimeOfDay >= endDTi.TimeOfDay)
                        || (beginDTj.TimeOfDay <= beginDTi.TimeOfDay && endDTj.TimeOfDay >= endDTi.TimeOfDay)
                        || (beginDTi.TimeOfDay <= beginDTj.TimeOfDay && beginDTi.TimeOfDay >= endDTj.TimeOfDay)
                        || (endDTi.TimeOfDay <= beginDTj.TimeOfDay && endDTi.TimeOfDay >= endDTj.TimeOfDay))
                        && this.neuSpread1_Sheet1.GetValue(i, (int)cols.IsValid).ToString() == this.neuSpread1_Sheet1.GetValue(j, (int)cols.IsValid).ToString()))
                        || ((this.neuSpread1_Sheet1.GetValue(i, (int)cols.IsValid).ToString() != "True" || "True" != this.neuSpread1_Sheet1.GetValue(j, (int)cols.IsValid).ToString()) && (this.neuSpread1_Sheet1.GetValue(i, (int)cols.NoonName).ToString() == this.neuSpread1_Sheet1.GetValue(j, (int)cols.NoonName).ToString())
                           )
                        )
                    {
                        MessageBox.Show("��" + (j + 1).ToString() + "���Ű�ʱ��������" + (i + 1) + "�г�ͻ��\r\n�����Ƿ���ʱ�佻�棬\r\n�����Ѵ��ڸ��Ű�,��������Ч�Ű�");
                        return -1;
                    }
                    //}
                }
                //}

            }
            return 0;
        }
        #endregion

    }
}
