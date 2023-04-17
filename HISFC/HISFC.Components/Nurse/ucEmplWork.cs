using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Neusoft.HISFC.Components.Nurse
{
    /// <summary>
    /// [��������: �Ű����]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-09-18]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucEmplWork : Neusoft.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucEmplWork()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// �Ű�ؼ�����,�������
        /// </summary>
        protected Nurse.ucWork[] controls;
        /// <summary>
        /// ��ǰ��Ա���
        /// </summary>
        Neusoft.HISFC.Models.Base.EnumEmployeeType emplType = (Neusoft.HISFC.Models.Base.EnumEmployeeType)Enum.Parse(typeof(Neusoft.HISFC.Models.Base.EnumEmployeeType), ((Neusoft.HISFC.Models.Base.Employee)(Neusoft.FrameWork.Management.Connection.Operator)).EmployeeType.ID.ToString());
        /// <summary>
        /// ��ǰ��Ա���
        /// </summary>
        Neusoft.HISFC.Models.Base.EmployeeTypeEnumService emplType2 = ((Neusoft.HISFC.Models.Base.Employee)(Neusoft.FrameWork.Management.Connection.Operator)).EmployeeType;
        /// <summary>
        /// ��ǰ���Ҵ���
        /// </summary>
        string deptCode = ((Neusoft.HISFC.Models.Base.Employee)(Neusoft.FrameWork.Management.Connection.Operator)).Dept.ID;
        /// <summary>
        /// ��ǰ��������
        /// </summary>
        string deptName = ((Neusoft.HISFC.Models.Base.Employee)(Neusoft.FrameWork.Management.Connection.Operator)).Dept.Name;
        /// <summary>
        /// �½����߰�ť
        /// </summary>
        private Neusoft.FrameWork.WinForms.Forms.ToolBarService toolBarService = new Neusoft.FrameWork.WinForms.Forms.ToolBarService();
        /// <summary>
        /// �Ű���ʷ
        /// </summary>
        private Hashtable htHistory = new Hashtable();
        /// <summary>
        ///����neuTabControl1 ѡ��ҳ���ǰһ��ѡ��
        /// </summary>
        private int tabPreSelected;
        /// <summary>
        /// �����Ƿ���ʾȫ�����͵���Ա
        /// </summary>
        bool displayAllType = false;
        /// <summary>
        /// ���ڵ㶯̬����
        /// </summary>
        ArrayList emp = new ArrayList();

        #endregion

        #region ����

        public bool DisplayAllType
        {
            get { return displayAllType; }
            set { displayAllType = value; }
        }

        
        #endregion

        #region ����

        #region ��ʼ������

        /// <summary>
        /// ��ʼ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucEmplWork_Load(object sender, EventArgs e)
        {
            this.Text = "��Ա�Ű�ģ��ά��";

            this.InitTab();

            this.InitArray();

            this.InitControls();

            this.InitEmployee();

            this.QueryTodayWork();

            this.LoadHistory();

        }
        /// <summary>
        /// �����Ű�����
        /// </summary>
        private void InitTab()
        {
            Neusoft.HISFC.BizLogic.Registration.Schema sMgr = new Neusoft.HISFC.BizLogic.Registration.Schema();
            DateTime Current = sMgr.GetDateTimeFromSysDateTime();

            DateTime Monday = this.GetMonday(Current);

            this.setWeek(Monday);
        }
        private void setWeek(DateTime Monday)
        {
            string[] Week = new string[] { "һ", "��", "��", "��", "��", "��", "��" };

            for (int i = 0; i < 7; i++)
            {
                this.neuTabControl1.TabPages[i].Tag = Monday.AddDays(i);
                this.neuTabControl1.TabPages[i].Text = Monday.AddDays(i).ToString("yyyy-MM-dd") + "  " + Week[i];
            }
        }
        /// <summary>
        /// ��ʼ������
        /// </summary>
        private void InitArray()
        {
            controls = new Nurse.ucWork[7];
            controls[0] = this.ucWork1;
            controls[1] = this.ucWork2;
            controls[2] = this.ucWork3;
            controls[3] = this.ucWork4;
            controls[4] = this.ucWork5;
            controls[5] = this.ucWork6;
            controls[6] = this.ucWork7;
        }

        /// <summary>
        /// ��ʼ��ģ��ؼ�
        /// </summary>
        private void InitControls()
        {
            Nurse.ucWork obj;

            for (int i = 0; i < 7; i++)
            {
                obj = controls[i];
                obj.Init((DayOfWeek)((i + 1) == 7 ? 0 : (i + 1)));
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        private void InitEmployee()
        {
            //���ø��ڵ�Ϊ��ǰ��¼����
            this.baseTreeView1.Nodes.Clear();
            TreeNode parent = new TreeNode(deptName);
            this.baseTreeView1.ImageList = this.baseTreeView1.deptImageList;
            parent.ImageIndex = 5;
            parent.SelectedImageIndex = 5;
            this.baseTreeView1.Nodes.Add(parent);

            Neusoft.HISFC.BizProcess.Integrate.Manager Mgr = new Neusoft.HISFC.BizProcess.Integrate.Manager();

            //Ĭ�������һ���ӽڵ� Ϊ��¼��Ա����Ա����
            TreeNode empTypeNode = new TreeNode();
            empTypeNode.Text = emplType2.Name;
            empTypeNode.ImageIndex = 0;
            empTypeNode.SelectedImageIndex = 1;
            parent.Nodes.Add(empTypeNode);

            //��ʼ���������Ľڵ�����
            Hashtable htSonNode = new Hashtable();

            foreach (TreeNode node in parent.Nodes)
            {
                htSonNode.Add(node.Text, node);
            }

            //�ж��Ƿ��г��������ҵ��������͵���Ա
            if (this.displayAllType)
            {
                emp = Mgr.QueryEmployeeByDeptID(deptCode);

                if (emp == null)
                {
                    MessageBox.Show("��ȡ��Ա�б�ʱ����!" + Mgr.Err, "��ʾ");
                    return;
                }

                foreach (Neusoft.HISFC.Models.Base.Employee employee in emp)
                {
                    //�ж���û�и���Ա���͵Ľڵ�
                    if (!htSonNode.ContainsKey(employee.EmployeeType.Name))
                    {
                        //���û��������Ա���͵Ľڵ�
                        TreeNode empTypeNode2 = new TreeNode();
                        empTypeNode2.Text = employee.EmployeeType.Name;
                        empTypeNode2.ImageIndex = 0;
                        empTypeNode2.SelectedImageIndex = 1;
                        parent.Nodes.Add(empTypeNode2);
                        htSonNode.Add(employee.EmployeeType.Name, empTypeNode2);

                        //��Ӹ���Ա���͵���һ�ڵ�
                        TreeNode empNode = new TreeNode();
                        empNode.Text = employee.Name;
                        empNode.ImageIndex = 4;
                        empNode.SelectedImageIndex = 3;
                        empNode.Tag = employee;
                        empTypeNode2.Nodes.Add(empNode);

                    }
                    else //������ڸ���Ա���͵Ľڵ�
                    {

                        TreeNode empNode = new TreeNode();
                        empNode.Text = employee.Name;
                        empNode.ImageIndex = 4;
                        empNode.SelectedImageIndex = 3;
                        empNode.Tag = employee;
                        ((TreeNode)htSonNode[employee.EmployeeType.Name]).Nodes.Add(empNode);
                    }

                }
            }
            else
            {
                emp = Mgr.QueryEmployee(emplType, deptCode);

                if (emp == null)
                {
                    MessageBox.Show("��ȡ��Ա�б�ʱ����!" + Mgr.Err, "��ʾ");
                    return;
                }

                foreach (Neusoft.HISFC.Models.Base.Employee employee in emp)
                {
                    TreeNode empNode = new TreeNode();
                    empNode.Tag = employee;
                    empNode.Text = employee.Name;
                    empNode.ImageIndex = 4;
                    empNode.SelectedImageIndex = 3;
                    empTypeNode.Nodes.Add(empNode);
                }
            }
            this.cmbEmp.AddItems(emp);
            parent.ExpandAll();
        }
        /// <summary>
        /// ������ʷ�Ű��б�
        /// </summary>
        private void LoadHistory()
        {
            this.htHistory.Clear();
            int Index = this.neuTabControl1.SelectedIndex;
            DayOfWeek historyWeek = this.controls[Index].Week;
            string selectedDate = this.controls[Index].ArrangeDate.ToString("yyyy-MM-dd");
            Neusoft.HISFC.BizLogic.Nurse.Work workMgr = new Neusoft.HISFC.BizLogic.Nurse.Work();
            ArrayList al = workMgr.QueryHistory(Index + 1, this.deptCode);

            //���ø��ڵ�Ϊ��ǰ��¼����
            this.baseTreeView2.Nodes.Clear();
            TreeNode parent = new TreeNode(GetChineseWeek(historyWeek) + " �Ű���ʷ");
            this.baseTreeView2.ImageList = this.baseTreeView2.deptImageList;
            parent.ImageIndex = 5;
            parent.SelectedImageIndex = 5;
            this.baseTreeView2.Nodes.Add(parent);
            StringBuilder dateString = new StringBuilder("#");
            ArrayList alDate = new ArrayList();
            int index = 1;
            string preDate = string.Empty;
            foreach (Neusoft.HISFC.Models.Nurse.Work tempWork in al)
            {
                if (dateString.ToString().IndexOf("#" + tempWork.WorkDate.Date.ToString("yyyy-MM-dd") + "#") == -1)
                {
                    if (alDate.Count > 0)
                    {
                        htHistory.Add(preDate, alDate);

                    }
                    alDate = new ArrayList();
                    dateString.Append(tempWork.WorkDate.Date.ToString("yyyy-MM-dd") + "#");
                    //����ʾ��ǰѡ�е�����
                    if (selectedDate != tempWork.WorkDate.Date.ToString("yyyy-MM-dd"))
                    {
                        TreeNode historyNode = new TreeNode();
                        historyNode.Tag = null;
                        historyNode.Text = tempWork.WorkDate.Date.ToString("yyyy-MM-dd");
                        historyNode.ImageIndex = 4;
                        historyNode.SelectedImageIndex = 3;
                        parent.Nodes.Add(historyNode);
                    }
                }
                alDate.Add(tempWork);
                preDate = tempWork.WorkDate.Date.ToString("yyyy-MM-dd");
                //����һ���Ű���Ϣ��ӵ���ϣ�б���
                if ((index++ == al.Count) && alDate.Count > 0)
                {
                    htHistory.Add(preDate, alDate);
                }

            }

            parent.ExpandAll();

        }
        /// <summary>
        /// ��ʼ�����߰�ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override Neusoft.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("����(+)", "", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.T���, true, false, null);
            toolBarService.AddToolButton("ɾ��(-)", "", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            toolBarService.AddToolButton("����ģ��", "", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.F����, true, false, null);
            toolBarService.AddToolButton("����", "", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.X��һ��, true, false, null);
            toolBarService.AddToolButton("����", "", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.S��һ��, true, false, null);
            toolBarService.AddToolButton("ȫ��ɾ��", "", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.Qȫ��, true, false, null);

            return this.toolBarService;
        }

        /// <summary>
        /// ��ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "����(+)":
                    this.Add();
                    break;
                case "ɾ��(-)":
                    this.Del();
                    break;
                case "ȫ��ɾ��":
                    this.DelAll();
                    break;
                case "����ģ��":
                    this.LoadTemplet();
                    break;
                case "����":
                    Prior();
                    break;
                case "����":
                    Next();
                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        /// <summary>
        /// �����ȼ�
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Subtract || keyData == Keys.OemMinus)
            {
                this.Del();
                return true;
            }
            else if (keyData == Keys.Add || keyData == Keys.Oemplus)
            {
                this.Add();
                return true;
            }
            else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.S.GetHashCode())
            {
                this.Save();
                return true;
            }
            else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.X.GetHashCode())
            {
                this.FindForm().Close();
            }
            else if (keyData == Keys.F1)
            {
                this.cmbEmp.Focus();
                return true;
            }
            else if (keyData == Keys.F3)
            {
                this.Find();
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }
        #endregion

        #region �����¼�����
        /// <summary>
        /// ��Ա��ѡ�к󷽷����÷����ж�ѡ�е� tabpage �е� CurrentPerson ��  DeptName ��ֵ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void baseTreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = this.baseTreeView1.SelectedNode;

            if (node == null) return;

            int Index = this.neuTabControl1.SelectedIndex;

            if (node.Level == 2)
            {
                this.controls[Index].CurrentPerson = (Neusoft.HISFC.Models.Base.Employee)node.Tag;
                this.controls[Index].DeptName = ((Neusoft.HISFC.Models.Base.Employee)(Neusoft.FrameWork.Management.Connection.Operator)).Dept.Name;
            }
            else
            {
                this.controls[Index].CurrentPerson = null;
            }

        }

        /// <summary>
        /// tabpage �л�ʱ�����ǰ���ҳ���и��ģ�����ʾ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuTabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (controls[this.tabPreSelected].IsChange())
            {
                if (MessageBox.Show("�����Ѿ��޸�,�Ƿ񱣴�䶯?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    if (controls[this.tabPreSelected].Save() == -1)
                    {
                        return;
                    }
                }
            }

        }
        /// <summary>
        /// tabpage ѡ�к�����Ű���Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuTabControl1_Selected(object sender, TabControlEventArgs e)
        {
            object obj = this.neuTabControl1.TabPages[this.neuTabControl1.SelectedIndex].Tag;

            if (obj == null || obj.ToString() == "")
            {
                this.baseTreeView1.SelectedNode = this.baseTreeView1.Nodes[0];
                this.baseTreeView1_AfterSelect(new object(), new System.Windows.Forms.TreeViewEventArgs(new TreeNode(), System.Windows.Forms.TreeViewAction.Unknown));
                this.neuTabControl1.TabPages[this.neuTabControl1.SelectedIndex].Tag = "Has Retrieve";
            }
            this.baseTreeView1.SelectedNode = null;
            int Index = this.neuTabControl1.SelectedIndex;
            this.controls[Index].ArrangeDate = DateTime.Parse(this.neuTabControl1.TabPages[Index].Tag.ToString()); ;
            this.controls[Index].Query(this.deptCode);
            this.LoadHistory();
            this.tabPreSelected = this.neuTabControl1.SelectedIndex;
        }
        /// <summary>
        ///��������Ա�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (TreeNode node in this.baseTreeView1.Nodes[0].Nodes)
            {
                foreach (TreeNode childNode in node.Nodes)
                {
                    if (((Neusoft.HISFC.Models.Base.Employee)childNode.Tag).ID == this.cmbEmp.Tag.ToString())
                    {
                        this.baseTreeView1.SelectedNode = childNode;
                        this.baseTreeView1.Focus();
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// ���������Ա��combox����ֵ���س�������Ա����ѡ�и���Ա
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbEmp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int Index = this.neuTabControl1.SelectedIndex;

                controls[Index].Focus();
                cmbEmp_SelectedIndexChanged(sender, e);
            }
        }
        /// <summary>
        /// ����Ű���ʷ����ǰ�Ű�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void baseTreeView2_DoubleClick(object sender, EventArgs e)
        {
            if(this.baseTreeView2.SelectedNode.Level != 1)
            {
                return;
            }
            if (MessageBox.Show("�Ƿ������ʷ�Űൽ��ǰ���Ű��У�\r\n�ò�����ɾ��ԭ�����Ű࣡", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
            {
                this.DelAllNoMessageBox();
                string selectedDate = this.baseTreeView2.SelectedNode.Text;
                ArrayList al = (ArrayList)this.htHistory[selectedDate];
                int Index = this.neuTabControl1.SelectedIndex;
                foreach (Neusoft.HISFC.Models.Nurse.Work tempWork in al)
                {
                    this.controls[Index].Add(tempWork);
                }
            }
        }
        #endregion

        #region �Ű���ط���

        /// <summary>
        /// Ĭ����ʾ�����Ű�
        /// </summary>
        private void QueryTodayWork()
        {
            Neusoft.HISFC.BizLogic.Registration.Schema SchemaMgr = new Neusoft.HISFC.BizLogic.Registration.Schema();

            DateTime today = SchemaMgr.GetDateTimeFromSysDateTime();

            int Index = (int)today.DayOfWeek;
            Index--;
            if (Index == -1) Index = 6;

            this.neuTabControl1.SelectedIndex = Index;
            this.controls[Index].ArrangeDate = today;
            this.controls[Index].Query(this.deptCode);
        }
        /// <summary>
        /// ��ȡ��ǰ�����������ڵ�����һ
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        private DateTime GetMonday(DateTime current)
        {
            DayOfWeek today = current.DayOfWeek;

            int interval = 1 - (int)today;

            if (interval == 1)//������
            {
                interval = -6;//�������չ鵽������
            }

            return current.AddDays(interval);
        }

        /// <summary>
        /// ��һ����
        /// </summary>
        private void Next()
        {
            DateTime Monday;

            Monday = this.GetMonday(DateTime.Parse(this.tabPage7.Tag.ToString()).AddDays(2));

            this.setWeek(Monday);

            for (int i = 0; i < 7; i++)
            {
                this.controls[i].Tag = null;
                this.controls[i].ArrangeDate = DateTime.Parse(this.neuTabControl1.TabPages[i].Tag.ToString());
            }


            this.controls[this.neuTabControl1.SelectedIndex].Query("ALL");
            this.LoadHistory();
        }
        /// <summary>
        /// ��һ����
        /// </summary>
        private void Prior()
        {
            DateTime Monday;

            Monday = this.GetMonday(DateTime.Parse(this.tabPage1.Tag.ToString()).AddDays(-3));

            this.setWeek(Monday);

            for (int i = 0; i < 7; i++)
            {
                this.controls[i].Tag = null;
                this.controls[i].ArrangeDate = DateTime.Parse(this.neuTabControl1.TabPages[i].Tag.ToString());
            }
            this.controls[this.neuTabControl1.SelectedIndex].Query("ALL");
            this.LoadHistory();
        }
        /// <summary>
        /// ����ģ��
        /// </summary>
        private void LoadTemplet()
        {
            Neusoft.HISFC.Components.Registration.frmSelectWeek f = new Neusoft.HISFC.Components.Registration.frmSelectWeek();

            DateTime week = DateTime.Parse(this.neuTabControl1.SelectedTab.Tag.ToString());
            f.SelectedWeek = week.DayOfWeek;
            if (f.ShowDialog() == DialogResult.Yes)
            {
                Neusoft.HISFC.BizLogic.Nurse.WorkTemplet templetMgr = new Neusoft.HISFC.BizLogic.Nurse.WorkTemplet();
                //��ȡȫ��ģ����Ϣ
                ArrayList al = templetMgr.Query(f.SelectedWeek, this.deptCode);
                if (al == null)
                {
                    MessageBox.Show("��ѯģ����Ϣʱ����!" + templetMgr.Err, "��ʾ");
                    return;
                }

                foreach (Neusoft.HISFC.Models.Nurse.WorkTemplet templet in al)
                {
                    controls[this.neuTabControl1.SelectedIndex].Add(templet);
                }

                controls[this.neuTabControl1.SelectedIndex].Focus();
                f.Dispose();
            }
        }
        #endregion

        #region ����ɾ���ġ���ӡ�����ҡ���������
        /// <summary>
        /// ���ر��水ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();

            return 0;
        }
        /// <summary>
        /// ����
        /// </summary>
        private void Add()
        {
            int Index = this.neuTabControl1.SelectedIndex;

            controls[Index].Add();
        }
        /// <summary>
        /// ɾ��
        /// </summary>
        private void Del()
        {
            int Index = this.neuTabControl1.SelectedIndex;

            controls[Index].Del();
        }

        /// <summary>
        /// ɾ��ȫ��
        /// </summary>
        private void DelAll()
        {
            int Index = this.neuTabControl1.SelectedIndex;

            controls[Index].DelAll();
        }
        private void DelAllNoMessageBox()
        {
            int Index = this.neuTabControl1.SelectedIndex;

            controls[Index].DelAllNoMessageBox();
        
        }
        /// <summary>
        /// ����
        /// </summary>
        private void Save()
        {
            int Index = this.neuTabControl1.SelectedIndex;
            this.controls[Index].ArrangeDate = DateTime.Parse(this.neuTabControl1.TabPages[Index].Tag.ToString());

            if (controls[Index].Save() == -1)
            {
                controls[Index].Focus();
                return;
            }

            MessageBox.Show("����ɹ�!", "��ʾ");

            controls[Index].Focus();
        }

        /// <summary>
        /// ������Ա
        /// </summary>
        private void Find()
        {
            int Index = this.neuTabControl1.SelectedIndex;

            if (Index == 6)
            {
                Index = 0;
            }
            else
            {
                Index = Index + 1;
            }

            this.cmbEmp.Focus();
        }
        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrint(object sender, object neuObject)
        {
            this.Print();
            return 1;
        }
        private void Print()
        {
            int Index = this.neuTabControl1.SelectedIndex;
            Neusoft.FrameWork.WinForms.Classes.Print p = new Neusoft.FrameWork.WinForms.Classes.Print();
            p.PrintPage(0, 0, this.controls[Index].FpSpread);
        }
        /// <summary>
        /// ��ӡԤ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrintPreview(object sender, object neuObject)
        {
            int Index = this.neuTabControl1.SelectedIndex;
            Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();
            print.PrintPreview(0, 0, this.controls[Index].FpSpread);
            return base.OnPrintPreview(sender, neuObject);
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Export(object sender, object neuObject)
        {
            this.Export();
            return 1;
        }
        private void Export()
        {
            try
            {
                string fileName = "";
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.DefaultExt = ".xls";
                dlg.Filter = "Microsoft Excel (*.xls)|*.xls";
                DialogResult result = dlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    fileName = dlg.FileName;
                    int Index = this.neuTabControl1.SelectedIndex;
                    this.controls[Index].FpSpread.SaveExcel(fileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        /// <summary>
        /// ��ȡӢ�����ڶ�Ӧ�����ı��ʽ
        /// </summary>
        /// <param name="week"></param>
        /// <returns></returns>
        private string GetChineseWeek(DayOfWeek week)
        {
            switch (week)
            {
                case DayOfWeek.Monday:
                    return "����һ";
                case DayOfWeek.Tuesday:
                    return "���ڶ�";
                case DayOfWeek.Wednesday:
                    return "������";
                case DayOfWeek.Thursday:
                    return "������";
                case DayOfWeek.Friday:
                    return "������";
                case DayOfWeek.Saturday:
                    return "������";
                case DayOfWeek.Sunday:
                    return "������";
                default:
                    return "";
            }
        }
        #endregion

        #region ɾ������
        //private void baseTreeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        //{
        //    //int Index = this.neuTabControl1.SelectedIndex;
        //    //Index++;

        //    //if (Index == 7) Index = 0;

        //    //if (controls[Index].IsChange())
        //    //{
        //    //    if (MessageBox.Show("�����Ѿ��ı�,�Ƿ񱣴�?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
        //    //        MessageBoxDefaultButton.Button2) == DialogResult.Yes)
        //    //    {
        //    //        if (controls[Index].Save() == -1)
        //    //        {
        //    //            e.Cancel = true;
        //    //            controls[Index].Focus();
        //    //        }
        //    //    }
        //    //}

        //}
        #endregion

        #endregion

    }
}
