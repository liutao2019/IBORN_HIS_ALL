using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FarPoint.Win.Spread;

namespace FS.HISFC.Components.Operation
{
    public partial class ucTabular : UserControl
    {
        public ucTabular()
        {
            InitializeComponent();
        }
        private FS.HISFC.BizProcess.Integrate.Registration.Tabulation tabMgr = new FS.HISFC.BizProcess.Integrate.Registration.Tabulation();    

        private FS.HISFC.BizProcess.Integrate.Manager personMgr = new FS.HISFC.BizProcess.Integrate.Manager();

        private FS.FrameWork.Management.DataBaseManger dataManager = new FS.FrameWork.Management.DataBaseManger();
        
        private FS.HISFC.Models.Base.Employee var = null;

        private ArrayList al, persons, kinds;
        private FS.FrameWork.Models.NeuObject obj;
        private FS.HISFC.Models.Registration.Tabulation tabular = null;
        private FS.HISFC.Models.Registration.WorkType defaultType
        {
            get
            {
                FS.HISFC.Models.Registration.WorkType temp = new FS.HISFC.Models.Registration.WorkType();
                temp.ID = "001";
                temp.Name = "ȫ��";
                temp.ClassID = "01";

                return temp;
            }
        }

        /// <summary>
        /// ��ʼ��,�ص���
        /// </summary>
        /// <returns></returns>
        public int Init(FS.HISFC.Models.Base.Employee basevar)
        {
            InitFp();
            this.lbType.SelectItem += new FS.FrameWork.WinForms.Controls.PopUpListBox.MyDelegate(lbType_SelectItem);
            this.panelList.Visible = false;
            var = basevar;


            return 0;
        }

        /// <summary>
        /// ����FarPoint��ʽ
        /// </summary>
        /// <returns></returns>
        private int InitFp()
        {
            fpSpread1_Sheet1.ColumnHeader.RowCount = 2;
            fpSpread1_Sheet1.Models.ColumnHeaderSpan.Add(0, 0, 2, 1);
            fpSpread1_Sheet1.ColumnHeader.Cells[1, 0].Text = "���";

            fpSpread1_Sheet1.Models.ColumnHeaderSpan.Add(0, 1, 2, 1);
            fpSpread1_Sheet1.ColumnHeader.Cells[1, 1].Text = "����";

            //fpSpread1_Sheet1.ColumnHeader.Cells[0,2].Text="2005-12-31";
            fpSpread1_Sheet1.ColumnHeader.Cells[1, 2].Text = "����һ";

            fpSpread1_Sheet1.ColumnHeader.Cells[1, 3].Text = "���ڶ�";
            fpSpread1_Sheet1.ColumnHeader.Cells[1, 4].Text = "������";
            fpSpread1_Sheet1.ColumnHeader.Cells[1, 5].Text = "������";
            fpSpread1_Sheet1.ColumnHeader.Cells[1, 6].Text = "������";
            fpSpread1_Sheet1.ColumnHeader.Cells[0, 7, 1, 7].ForeColor = Color.Red;
            fpSpread1_Sheet1.ColumnHeader.Cells[1, 7].Text = "������";
            fpSpread1_Sheet1.ColumnHeader.Cells[0, 8, 1, 8].ForeColor = Color.Red;
            fpSpread1_Sheet1.ColumnHeader.Cells[1, 8].Text = "������";

            //fpSpread1_Sheet1.Columns[7].ForeColor=Color.Red;
            //fpSpread1_Sheet1.Columns[8].ForeColor=Color.Red;
            fpSpread1_Sheet1.Columns[0].Locked = true;
            fpSpread1_Sheet1.Columns[1].Locked = true;
            fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;

            InputMap im;

            im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Down, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Up, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Escape, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            return 0;
        }
        /// <summary>
        /// ��ȡ����һ������
        /// </summary>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        private DateTime GetNextMonday(DateTime currentDate)
        {
            DateTime nextDate = DateTime.MinValue;

            switch ((int)currentDate.DayOfWeek)
            {
                case 0://����
                    nextDate = currentDate.Date.AddDays(1);
                    break;
                case 1://��һ
                    nextDate = currentDate.Date;
                    break;
                case 2://�ܶ�
                    nextDate = currentDate.Date.AddDays(6);
                    break;
                case 3:
                    nextDate = currentDate.Date.AddDays(5);
                    break;
                case 4:
                    nextDate = currentDate.Date.AddDays(4);
                    break;
                case 5:
                    nextDate = currentDate.Date.AddDays(3);
                    break;
                case 6:
                    nextDate = currentDate.Date.AddDays(2);
                    break;
            }

            return nextDate;
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public int Clear()
        {
            DateTime current = this.dataManager.GetDateTimeFromSysDateTime();//��ǰʱ��
            DateTime next = GetNextMonday(current);

            if (fpSpread1_Sheet1.RowCount > 0) fpSpread1_Sheet1.Rows.Remove(0, fpSpread1_Sheet1.RowCount);

            fpSpread1_Sheet1.ColumnHeader.Cells[0, 2].Text = next.ToString("yyyy-MM-dd");
            fpSpread1_Sheet1.ColumnHeader.Cells[0, 3].Text = next.AddDays(1).ToString("yyyy-MM-dd");
            fpSpread1_Sheet1.ColumnHeader.Cells[0, 4].Text = next.AddDays(2).ToString("yyyy-MM-dd");
            fpSpread1_Sheet1.ColumnHeader.Cells[0, 5].Text = next.AddDays(3).ToString("yyyy-MM-dd");
            fpSpread1_Sheet1.ColumnHeader.Cells[0, 6].Text = next.AddDays(4).ToString("yyyy-MM-dd");
            fpSpread1_Sheet1.ColumnHeader.Cells[0, 7].Text = next.AddDays(5).ToString("yyyy-MM-dd");
            fpSpread1_Sheet1.ColumnHeader.Cells[0, 8].Text = next.AddDays(6).ToString("yyyy-MM-dd");

            this.lbDate.Text = "�Ű�ʱ��:" + current.ToString("yyyy-MM-dd");
            this.Tag = null;

            return 0;
        }
        /// <summary>
        /// ��ѯ��ǰ������һ�ܵ��Űల��
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public int QueryCurrent(string deptID)
        {
            Clear();
            Query(deptID);
            return 0;
        }
        /// <summary>
        /// ��ѯ�����Ű���Ϣ
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="deptID"></param>
        /// <returns></returns>
        private int Query(string deptID)
        {
            if (fpSpread1_Sheet1.RowCount > 0) fpSpread1_Sheet1.Rows.Remove(0, fpSpread1_Sheet1.RowCount);
            //�������
            string arrangeID = DateTime.Parse(fpSpread1_Sheet1.ColumnHeader.Cells[0, 2].Text).ToString("yyyyMMdd") +
                DateTime.Parse(fpSpread1_Sheet1.ColumnHeader.Cells[0, 8].Text).ToString("yyyyMMdd");
            try
            {
                al = tabMgr.Query(arrangeID, deptID);
                if (al == null)   
                {
                    MessageBox.Show(tabMgr.Err, "��ʾ");
                    return -1;
                }
                //��ѯ���Ƴ������
                kinds = tabMgr.Query(deptID);
                if (kinds == null) kinds = new ArrayList();
                this.lbType.AddItems(kinds);

                //û���Ű���Ϣ,��ʾ������Ա
                this.QueryPersonByDept(deptID);

                //��ʾ�Ű���Ϣ,û������ԭ���Ű�˳�����Ա
                AddTabular();
            }
            catch (Exception e)
            { MessageBox.Show(e.Message); }
            //��¼��ǰ����
            this.Tag = deptID;

            return 0;
        }
        /// <summary>
        /// ���Ű���Ų�ѯ���Ű���Ϣ
        /// </summary>
        /// <param name="arrangeID"></param>
        /// <param name="deptID"></param>
        public void Query(string arrangeID, string deptID)
        {
            if (fpSpread1_Sheet1.RowCount > 0) fpSpread1_Sheet1.Rows.Remove(0, fpSpread1_Sheet1.RowCount - 1);
            DateTime begin = DateTime.Parse(arrangeID.Substring(0, 4) + "-" + arrangeID.Substring(4, 2) + "-" +
                arrangeID.Substring(6, 2));

            fpSpread1_Sheet1.ColumnHeader.Cells[0, 2].Text = begin.ToString("yyyy-MM-dd");
            fpSpread1_Sheet1.ColumnHeader.Cells[0, 3].Text = begin.AddDays(1).ToString("yyyy-MM-dd");
            fpSpread1_Sheet1.ColumnHeader.Cells[0, 4].Text = begin.AddDays(2).ToString("yyyy-MM-dd");
            fpSpread1_Sheet1.ColumnHeader.Cells[0, 5].Text = begin.AddDays(3).ToString("yyyy-MM-dd");
            fpSpread1_Sheet1.ColumnHeader.Cells[0, 6].Text = begin.AddDays(4).ToString("yyyy-MM-dd");
            fpSpread1_Sheet1.ColumnHeader.Cells[0, 7].Text = begin.AddDays(5).ToString("yyyy-MM-dd");
            fpSpread1_Sheet1.ColumnHeader.Cells[0, 8].Text = begin.AddDays(6).ToString("yyyy-MM-dd");

            this.lbDate.Text = "�Ű�ʱ��:" + dataManager.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd");
            Query(deptID);
            this.Tag = deptID;
        }
        /// <summary>
        /// ��ӿ�������Ա
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        private int QueryPersonByDept(string deptID)
        {
            if (var.EmployeeType.ID.ToString() != "N")//�ǻ�ʿ���ǻ�ʿ��Ա�Ű�
            {
                persons = personMgr.QueryEmployeeExceptNurse( deptID );
            }
            else//��ʿֻ�ܸ���ʿ�Ű�
            {
                persons = personMgr.QueryEmployee( FS.HISFC.Models.Base.EnumEmployeeType.N, deptID );
            }

            if (persons != null)
            {
                foreach (FS.HISFC.Models.Base.Employee p in persons)
                {
                    fpSpread1_Sheet1.Rows.Add(fpSpread1_Sheet1.RowCount, 1);
                    int index = fpSpread1_Sheet1.RowCount - 1;
                    this.fpSpread1_Sheet1.Rows[index].Height = 23;
                    fpSpread1_Sheet1.SetValue(index, 0, index + 1, false);//���
                    fpSpread1_Sheet1.SetValue(index, 1, "   " + p.Name, false);//��Ա����
                    fpSpread1_Sheet1.SetTag(index, 1, p);
                    fpSpread1_Sheet1.SetValue(index, 9, 2 * index, false);//����˳��

                    for (int i = 2; i < 9; i++)
                    {
                        this.fpSpread1_Sheet1.SetValue(index, i, this.defaultType.Name, false);
                        string color = "";
                        color = this.getColor(this.defaultType.ID);
                        if (color != "")
                            fpSpread1_Sheet1.Cells[index, i].ForeColor = Color.FromArgb(int.Parse(color));

                        fpSpread1_Sheet1.SetTag(index, i, this.defaultType);
                    }
                }
            }

            return 0;
        }

        /// <summary>
        /// ����Ű���Ϣ
        /// </summary>
        /// <returns></returns>
        private int AddTabular()
        {
            int column;

            foreach (FS.HISFC.Models.Registration.Tabulation t in al)
            {
                for (int index = 0; index < fpSpread1_Sheet1.RowCount; index++)
                {
                    if ((fpSpread1_Sheet1.GetTag(index, 1) as FS.HISFC.Models.Base.Employee).ID == t.EmplID)
                    {
                        column = (int)t.Workdate.DayOfWeek;
                        if (column == 0) column = 7;
                        fpSpread1_Sheet1.SetValue(index, column + 1, t.Kind.Name, false);

                        string color = "";
                        color = this.getColor(t.Kind.ID);
                        if (color != "")
                            fpSpread1_Sheet1.Cells[index, column + 1].ForeColor = Color.FromArgb(int.Parse(color));

                        if (t.Memo != "") this.fpSpread1_Sheet1.SetNote(index, column + 1, t.Memo);
                        fpSpread1_Sheet1.SetTag(index, column + 1, t.Kind);
                        fpSpread1_Sheet1.SetValue(index, 9, t.SortID, false);

                        break;
                    }
                }
            }

            SortInfo[] sort = new SortInfo[1];
            sort[0] = new SortInfo(9, true, System.Collections.Comparer.Default);
            fpSpread1_Sheet1.SortRange(0, 1, fpSpread1_Sheet1.RowCount, 9, true, sort);

            return 0;
        }
        /// <summary>
        /// ��ȡ��ɫ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string getColor(string id)
        {
            if (this.lbType.Items == null)
                this.lbType.Items = new ArrayList();

            foreach (FS.HISFC.Models.Registration.WorkType w in this.lbType.Items)
            {
                if (w.ID == id)
                    return w.ForeColor;
            }
            return "";
        }

        #region public
        /// <summary>
        /// ��һ��
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public int NextWeek()
        {
            if (this.Tag == null || this.Tag.ToString() == "") return -1;

            DateTime end = DateTime.Parse(fpSpread1_Sheet1.ColumnHeader.Cells[0, 8].Text);
            end = end.AddDays(1);
            SetTitle(end);

            Query(this.Tag.ToString());

            return 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="begin"></param>
        private void SetTitle(DateTime begin)
        {
            fpSpread1_Sheet1.ColumnHeader.Cells[0, 2].Text = begin.ToString("yyyy-MM-dd");
            fpSpread1_Sheet1.ColumnHeader.Cells[0, 3].Text = begin.AddDays(1).ToString("yyyy-MM-dd");
            fpSpread1_Sheet1.ColumnHeader.Cells[0, 4].Text = begin.AddDays(2).ToString("yyyy-MM-dd");
            fpSpread1_Sheet1.ColumnHeader.Cells[0, 5].Text = begin.AddDays(3).ToString("yyyy-MM-dd");
            fpSpread1_Sheet1.ColumnHeader.Cells[0, 6].Text = begin.AddDays(4).ToString("yyyy-MM-dd");
            fpSpread1_Sheet1.ColumnHeader.Cells[0, 7].Text = begin.AddDays(5).ToString("yyyy-MM-dd");
            fpSpread1_Sheet1.ColumnHeader.Cells[0, 8].Text = begin.AddDays(6).ToString("yyyy-MM-dd");
        }
        /// <summary>
        /// ��һ��
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public int PriorWeek()
        {
            if (this.Tag == null || this.Tag.ToString() == "") return -1;

            DateTime end = DateTime.Parse(fpSpread1_Sheet1.ColumnHeader.Cells[0, 2].Text);
            end = end.AddDays(-7);
            SetTitle(end);

            Query(this.Tag.ToString());

            return 0;
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        public void Print()
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            p.PrintPreview(this);
        }
        /// <summary>
        /// ����
        /// </summary>
        public void Up()
        {
            int row = this.fpSpread1_Sheet1.ActiveRowIndex;
            if (row < 1) return;

            int priorSort = int.Parse(this.fpSpread1_Sheet1.GetText(row - 1, 9));
            int sortID = int.Parse(this.fpSpread1_Sheet1.GetText(row, 9));

            int temp = sortID;
            sortID = priorSort;
            priorSort = temp;

            fpSpread1_Sheet1.SetValue(row, 9, sortID, false);
            fpSpread1_Sheet1.SetValue(row - 1, 9, priorSort, false);
            //��������
            SortInfo[] sort = new SortInfo[1];
            sort[0] = new SortInfo(9, true, System.Collections.Comparer.Default);
            fpSpread1_Sheet1.SortRange(0, 1, fpSpread1_Sheet1.RowCount, 9, true, sort);

            fpSpread1_Sheet1.ActiveRowIndex = row - 1;

        }
        /// <summary>
        /// ����
        /// </summary>
        public void Down()
        {
            int row = this.fpSpread1_Sheet1.ActiveRowIndex;
            if (row > fpSpread1_Sheet1.RowCount - 2) return;

            int sortID = int.Parse(this.fpSpread1_Sheet1.GetText(row, 9));
            int nextSort = int.Parse(this.fpSpread1_Sheet1.GetText(row + 1, 9));

            int temp = sortID;
            sortID = nextSort;
            nextSort = temp;

            fpSpread1_Sheet1.SetValue(row, 9, sortID, false);
            fpSpread1_Sheet1.SetValue(row + 1, 9, nextSort, false);
            //��������
            SortInfo[] sort = new SortInfo[1];
            sort[0] = new SortInfo(9, true, System.Collections.Comparer.Default);
            fpSpread1_Sheet1.SortRange(0, 1, fpSpread1_Sheet1.RowCount, 9, true, sort);

            fpSpread1_Sheet1.ActiveRowIndex = row + 1;
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            //��֤
            if (valid() == -1) return -1;

            //�������
            string arrangeID = DateTime.Parse(fpSpread1_Sheet1.ColumnHeader.Cells[0, 2].Text).ToString("yyyyMMdd") +
                DateTime.Parse(fpSpread1_Sheet1.ColumnHeader.Cells[0, 8].Text).ToString("yyyyMMdd");
            string deptID = this.Tag.ToString();//���Ҵ���

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(this.personMgr.Connection);
            //t.BeginTransaction();

            tabMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {
                //ɾ���Ѿ�������Ű��¼
                if (tabMgr.DeleteTabular(arrangeID, deptID) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.tabMgr.Err, "��ʾ");
                    return -1;
                }
                //����Ű��¼
                for (int i = 0; i < fpSpread1_Sheet1.RowCount; i++)
                {
                    //Ա������
                    string emplID = (fpSpread1_Sheet1.GetTag(i, 1) as FS.HISFC.Models.Base.Employee).ID;

                    for (int j = 2; j < 9; j++)
                    {
                        if (fpSpread1_Sheet1.GetTag(i, j) != null)
                        {
                            //�Ű�����
                            DateTime workDate = DateTime.Parse(fpSpread1_Sheet1.ColumnHeader.Cells[0, j].Text);

                            tabular = new FS.HISFC.Models.Registration.Tabulation();
                            tabular.Kind = (FS.HISFC.Models.Registration.WorkType)fpSpread1_Sheet1.GetTag(i, j);
                            //
                            tabular.arrangeID = arrangeID;//�������
                            tabular.DeptID = deptID;//����
                            tabular.EmplID = emplID;
                            tabular.Workdate = workDate;
                            tabular.OperID = var.ID;
                            tabular.Memo = fpSpread1_Sheet1.GetNote(i, j);
                            tabular.SortID = 2 * i;

                            if (tabMgr.Insert(tabular) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show(tabMgr.Err, "��ʾ");
                                return -1;
                            }
                        }
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "��ʾ");
                return -1;
            }

            MessageBox.Show("���ųɹ�!", "��ʾ");

            return 0;
        }
        /// <summary>
        /// ��֤��Ч��
        /// </summary>
        /// <returns></returns>
        private int valid()
        {
            if (fpSpread1_Sheet1.RowCount == 0) return -1;
            if (this.Tag == null || this.Tag.ToString() == "")
            {
                MessageBox.Show("��ѡ���Ű����!", "��ʾ");
                return -1;
            }

            for (int i = 0; i < fpSpread1_Sheet1.RowCount; i++)
            {
                for (int j = 2; j < 9; j++)
                {
                    //���Ű��¼�����ܱ���
                    if (fpSpread1_Sheet1.GetTag(i, j) != null) return 0;
                }
            }

            MessageBox.Show("û���Ű��¼, ���ܱ���!", "��ʾ");

            return -1;
        }

        /// <summary>
        /// ��ע
        /// </summary>
        public void Memo()
        {
            if (this.Tag == null || this.Tag.ToString() == "")
            {
                MessageBox.Show("��ѡ���Ű����!", "��ʾ");
                return;
            }
            int row = fpSpread1_Sheet1.ActiveRowIndex;
            if (fpSpread1_Sheet1.RowCount == 0) return;


        }
        /// <summary>
        /// ��ȡ��ע
        /// </summary>
        /// <returns></returns>
        public string getMemo()
        {
            if (this.Tag == null || this.Tag.ToString() == "")
            {
                MessageBox.Show("��ѡ���Ű����!", "��ʾ");
                return "-1";
            }
            int row = fpSpread1_Sheet1.ActiveRowIndex;
            if (fpSpread1_Sheet1.RowCount == 0) return "-1";

            if (this.fpSpread1_Sheet1.GetTag(row, fpSpread1_Sheet1.ActiveColumnIndex) == null) return "-1";
            return this.fpSpread1_Sheet1.GetNote(row, this.fpSpread1_Sheet1.ActiveColumnIndex);
        }
        /// <summary>
        /// ���ñ�ע
        /// </summary>
        /// <param name="Text"></param>
        public void setMemo(string Text)
        {
            if (this.fpSpread1_Sheet1.RowCount == 0) return;
            this.fpSpread1_Sheet1.SetNote(fpSpread1_Sheet1.ActiveRowIndex, fpSpread1_Sheet1.ActiveColumnIndex, Text);
        }
        /// <summary>
        /// ��ȡ��ǰ�Ű���Ϣ
        /// </summary>
        /// <returns></returns>
        public ArrayList getTabular()
        {
            al = new ArrayList();
            //�������
            string arrangeID = DateTime.Parse(fpSpread1_Sheet1.ColumnHeader.Cells[0, 2].Text).ToString("yyyyMMdd") +
                DateTime.Parse(fpSpread1_Sheet1.ColumnHeader.Cells[0, 8].Text).ToString("yyyyMMdd");
            string deptID = this.Tag.ToString();//���Ҵ���

            for (int i = 0; i < fpSpread1_Sheet1.RowCount; i++)
            {
                //Ա������
                string emplID = (fpSpread1_Sheet1.GetTag(i, 1) as FS.HISFC.Models.Base.Employee).ID;

                for (int j = 2; j < 9; j++)
                {
                    if (fpSpread1_Sheet1.GetTag(i, j) != null)
                    {
                        //�Ű�����
                        DateTime workDate = DateTime.Parse(fpSpread1_Sheet1.ColumnHeader.Cells[0, j].Text);

                        tabular = new FS.HISFC.Models.Registration.Tabulation();
                        tabular.Kind = (FS.HISFC.Models.Registration.WorkType)fpSpread1_Sheet1.GetTag(i, j);
                        //
                        tabular.arrangeID = arrangeID;//�������
                        tabular.DeptID = deptID;//����
                        tabular.EmplID = emplID;
                        tabular.Workdate = workDate;
                        tabular.Memo = fpSpread1_Sheet1.GetNote(i, j);
                        tabular.SortID = i;

                        al.Add(tabular);
                    }
                }
            }
            return al;
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="tabulars"></param>
        public void LoadTemplate(ArrayList tabulars)
        {
            if (this.Tag == null || this.Tag.ToString() == "") return;
            if (fpSpread1_Sheet1.RowCount > 0) fpSpread1_Sheet1.Rows.Remove(0, fpSpread1_Sheet1.RowCount);
            //��ʾ������Ա
            this.QueryPersonByDept(this.Tag.ToString());
            al = tabulars;
            //��ʾ�Ű���Ϣ,û������ԭ���Ű�˳�����Ա
            AddTabular();
        }
        #endregion

        #region FarPoint����
        private void fpSpread1_EditModeOn(object sender, System.EventArgs e)
        {
            SetLocation();
            this.panelList.Visible = false;
        }
        private void fpSpread1_EditChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column >= 2 && e.Column <= 8)
            {
                string text = fpSpread1_Sheet1.ActiveCell.Text;
                fpSpread1_Sheet1.Cells[e.Row, e.Column].Tag = null;

                this.lbType.Filter(text.Trim());
                if (this.panelList.Visible == false) this.panelList.Visible = true;
            }
        }
        private void fpSpread1_EditModeOff(object sender, System.EventArgs e)
        {
            int row = fpSpread1.Sheets[0].ActiveRowIndex;
            int col = fpSpread1.Sheets[0].ActiveColumnIndex;

            string where = fpSpread1_Sheet1.GetText(row, col);
            if (this.fpSpread1_Sheet1.GetTag(row, col) == null)
            {
                this.fpSpread1_Sheet1.Cells[row, col].ForeColor = Color.FromKnownColor(System.Drawing.KnownColor.WindowText);

                if (where.Trim() == "")
                {
                    this.fpSpread1_Sheet1.SetValue(row, col, "", false);
                    this.fpSpread1_Sheet1.SetTag(row, col, null);
                }
                else
                {
                    if (this.lbType.GetSelectedItem(out obj) == -1)
                    {
                        this.fpSpread1_Sheet1.SetValue(row, col, "", false);
                        this.fpSpread1_Sheet1.SetTag(row, col, null);
                        return;
                    }
                    if (obj == null)
                    {
                        this.fpSpread1_Sheet1.SetValue(row, col, "", false);
                        this.fpSpread1_Sheet1.SetTag(row, col, null);
                    }
                    else
                    {
                        string color = this.getColor((obj as FS.HISFC.Models.Registration.WorkType).ID);
                        if (color != "")
                            this.fpSpread1_Sheet1.Cells[row, col].ForeColor = Color.FromArgb(int.Parse(color));

                        this.fpSpread1_Sheet1.SetValue(row, col, (obj as FS.HISFC.Models.Registration.WorkType).Name, false);
                        this.fpSpread1_Sheet1.SetTag(row, col, (FS.HISFC.Models.Registration.WorkType)obj);
                    }
                }
            }
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Up)
            {
                #region up
                if (fpSpread1.ContainsFocus)
                {
                    if (this.panelList.Visible)
                        this.lbType.PriorRow();
                    else
                    {
                        int CurrentRow = fpSpread1_Sheet1.ActiveRowIndex;
                        if (CurrentRow > 0)
                        {
                            fpSpread1_Sheet1.ActiveRowIndex = CurrentRow - 1;
                            //{0CD66D53-785C-4ba5-840B-885F01A31A42}
                            //fpSpread1_Sheet1.AddSelection(CurrentRow - 1, 0, 1, 0);
                            fpSpread1_Sheet1.AddSelection(CurrentRow - 1, 1, 1, 1);
                        }
                    }
                }
                #endregion
            }
            else if (keyData == Keys.Down)
            {
                #region down
                if (fpSpread1.ContainsFocus)
                {
                    if (this.panelList.Visible)
                        this.lbType.NextRow();
                    else
                    {
                        int CurrentRow = fpSpread1_Sheet1.ActiveRowIndex;
                        if (CurrentRow >= 0 && CurrentRow <= fpSpread1_Sheet1.RowCount - 2)
                        {
                            
                            fpSpread1_Sheet1.ActiveRowIndex = CurrentRow + 1;
                            ////{0CD66D53-785C-4ba5-840B-885F01A31A42}
                            //fpSpread1_Sheet1.AddSelection(CurrentRow + 1, 0, 1, 0);
                            fpSpread1_Sheet1.AddSelection(CurrentRow + 1, 1, 1, 1);
                        }
                    }
                }
                #endregion
            }
            else if (keyData == Keys.Escape)
            {
                if (this.panelList.Visible) this.panelList.Visible = false;
            }

            return base.ProcessDialogKey(keyData);
        }

        private int lbType_SelectItem(Keys key)
        {
            int row = fpSpread1.Sheets[0].ActiveRowIndex;
            int col = fpSpread1.Sheets[0].ActiveColumnIndex;

            if (this.lbType.GetSelectedItem(out obj) == -1) return -1;

            if (obj == null)
            {
                this.fpSpread1_Sheet1.SetValue(row, col, "", false);
                this.fpSpread1_Sheet1.SetTag(row, col, null);
            }
            else
            {
                this.fpSpread1_Sheet1.SetValue(row, col, (obj as FS.HISFC.Models.Registration.WorkType).Name, false);
                this.fpSpread1_Sheet1.SetTag(row, col, (FS.HISFC.Models.Registration.WorkType)obj);
            }
            this.panelList.Visible = false;

            return 0;
        }
        #endregion

        /// <summary>
        /// ����panelList����ʾλ��
        /// </summary>
        /// <returns></returns>
        private void SetLocation()
        {
            Control cell = fpSpread1.EditingControl;
            if (cell == null) return;

            if (fpSpread1_Sheet1.ActiveColumnIndex >= 2 && fpSpread1_Sheet1.ActiveColumnIndex <= 8)
            {
                int y = fpSpread1.Top + cell.Top + cell.Height + this.panelList.Height + 7;
                if (y <= this.Height)
                {
                    if (fpSpread1.Left + cell.Left + this.panelList.Width + 20 <= this.Width)
                    {
                        this.panelList.Location = new Point(fpSpread1.Left + cell.Left + 20, y - this.panelList.Height);
                    }
                    else
                    {
                        this.panelList.Location = new Point(this.Width - this.panelList.Width - 10, y - this.panelList.Height);
                    }
                }
                else
                {
                    if (fpSpread1.Left + cell.Left + this.panelList.Width + 20 <= this.Width)
                    {
                        this.panelList.Location = new Point(fpSpread1.Left + cell.Left + 20, fpSpread1.Top + cell.Top - this.panelList.Height - 7);
                    }
                    else
                    {
                        this.panelList.Location = new Point(this.Width - this.panelList.Width - 10, fpSpread1.Top + cell.Top - this.panelList.Height - 7);
                    }
                }
            }
        }

        #region drag


        private void fpSpread1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (dragBoxFromMouseDown != Rectangle.Empty &&
                    !dragBoxFromMouseDown.Contains(new Point(e.X, e.Y)))
                {
                    if (CurrentRow < 0) return;
                    //					this.lbTitle.Text = this.CurrentRow.ToString();
                    this.fpSpread1.DoDragDrop(CurrentRow, DragDropEffects.Move | DragDropEffects.Copy);
                }
            }
        }

        private void fpSpread1_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(System.Int32)))
            {
                e.Effect = DragDropEffects.Move;

                Point p = fpSpread1.PointToClient(new Point(e.X, e.Y));

                FarPoint.Win.Spread.Model.CellRange range = fpSpread1.GetCellFromPixel(0, 0, p.X, p.Y);
                if (range.RowCount == -1) return;

                if (range.Row == this.fpSpread1.GetViewportTopRow(0) &&
                    range.Row != 0)
                {

                    this.fpSpread1.SetViewportTopRow(0, range.Row - 1);
                    System.Threading.Thread.Sleep(100);
                }

                if (range.Row == this.fpSpread1.GetViewportBottomRow(0) &&
                    range.Row != this.fpSpread1_Sheet1.RowCount - 1)
                {
                    this.fpSpread1.SetViewportTopRow(0, this.fpSpread1.GetViewportTopRow(0) + 1);
                    System.Threading.Thread.Sleep(100);
                }

            }
        }

        private void fpSpread1_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(System.Int32)))
            {
                Point p = this.fpSpread1.PointToClient(new Point(e.X, e.Y));

                FarPoint.Win.Spread.Model.CellRange range = this.fpSpread1.GetCellFromPixel(0, 0, p.X, p.Y);
                if (range.RowCount == -1) return;

                int sourceRow = int.Parse(e.Data.GetData(typeof(System.Int32)).ToString());
                int objectRow = range.Row;

                //�ӵ�objectRowǰһ��

                int sortID = int.Parse(this.fpSpread1_Sheet1.GetText(objectRow, 9));

                int sourceSort = sortID - 1;
                this.fpSpread1_Sheet1.SetValue(sourceRow, 9, sourceSort, false);
                #region У��λ��  zhangjunyi@FS.com
                if (objectRow > this.fpSpread1_Sheet1.ActiveRowIndex)
                {
                    this.fpSpread1_Sheet1.ActiveRowIndex = objectRow - 1;
                }
                else
                {
                    this.fpSpread1_Sheet1.ActiveRowIndex = objectRow;
                }
                #endregion
                //��������
                SortInfo[] sort = new SortInfo[1];
                sort[0] = new SortInfo(9, true, System.Collections.Comparer.Default);
                fpSpread1_Sheet1.SortRange(0, 1, fpSpread1_Sheet1.RowCount, 9, true, sort);

                //���¸�ֵ�����
                for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                {
                    this.fpSpread1_Sheet1.SetValue(i, 9, 2 * i, false);
                }
            }
        }

        int CurrentRow = -1;

        private void fpSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Row < 0 || e.RowHeader || e.ColumnHeader || e.Column < 0) return;

            this.fpSpread1_Sheet1.ActiveRowIndex = e.Row;

            CurrentRow = e.Row;
        }

        Rectangle dragBoxFromMouseDown = Rectangle.Empty;
        //		private int ActiveRowIndex = 0;
        private void fpSpread1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {

            Size dragSize = SystemInformation.DragSize;
            dragSize.Height = 18;//this.fpSpread1_Sheet1.Rows.Default.Height;

            dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2),
                e.Y - (dragSize.Height / 2)), dragSize);
        }

        private void fpSpread1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            dragBoxFromMouseDown = Rectangle.Empty;
        }
        #endregion
    }
}
