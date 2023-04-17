using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Nurse
{
    /// <summary>
    /// [��������: �Ű�ģ�����]<br></br>
    /// [�� �� ��: ]<br></br>
    /// [����ʱ��: 2007-05-03]<br></br>
    /// <�޸ļ�¼
    ///		�޸���='������'
    ///		�޸�ʱ��='2007-09-18'
    ///		�޸�Ŀ��='���ƹ���'
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucEmplWorkTemplet : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucEmplWorkTemplet()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// �Ű�ؼ�����,�������
        /// </summary>
        protected Nurse.ucWorkTemplet[] controls;
        /// <summary>
        /// ��ǰ��Ա���
        /// </summary>
        FS.HISFC.Models.Base.EnumEmployeeType emplType = (FS.HISFC.Models.Base.EnumEmployeeType)Enum.Parse(typeof(FS.HISFC.Models.Base.EnumEmployeeType), ((FS.HISFC.Models.Base.Employee)(FS.FrameWork.Management.Connection.Operator)).EmployeeType.ID.ToString());
        /// <summary>
        /// ��ǰ��Ա���
        /// </summary>
        FS.HISFC.Models.Base.EmployeeTypeEnumService emplType2 = ((FS.HISFC.Models.Base.Employee)(FS.FrameWork.Management.Connection.Operator)).EmployeeType;
        /// <summary>
        /// ��ǰ���Ҵ���
        /// </summary>
        string deptCode = ((FS.HISFC.Models.Base.Employee)(FS.FrameWork.Management.Connection.Operator)).Dept.ID;
        /// <summary>
        /// ��ǰ��������
        /// </summary>
        string deptName = ((FS.HISFC.Models.Base.Employee)(FS.FrameWork.Management.Connection.Operator)).Dept.Name;
        /// <summary>
        /// �½����߰�ť
        /// </summary>
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        /// <summary>
        /// �����Ƿ���ʾȫ�����͵���Ա
        /// </summary>
        bool displayAllType = false;
        /// <summary>
        /// ����neuTabControl1 ѡ��ҳ���ǰһ��ѡ��
        /// </summary>
        private int tabPreSelected;
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
        private void ucEmplWorkTemplet_Load(object sender, EventArgs e)
        {
            this.Text = "��Ա�Ű�ģ��ά��";

            this.InitArray();

            this.InitControls();

            this.InitEmployee();

            this.controls[0].Query(this.deptCode);

        }
        /// <summary>
        /// ��ʼ������
        /// </summary>
        private void InitArray()
        {
            controls = new Nurse.ucWorkTemplet[7];
            controls[0] = this.ucWorkTemplet1;
            controls[1] = this.ucWorkTemplet2;
            controls[2] = this.ucWorkTemplet3;
            controls[3] = this.ucWorkTemplet4;
            controls[4] = this.ucWorkTemplet5;
            controls[5] = this.ucWorkTemplet6;
            controls[6] = this.ucWorkTemplet7;
        }

        /// <summary>
        /// ��ʼ��ģ��ؼ�
        /// </summary>
        private void InitControls()
        {
            Nurse.ucWorkTemplet obj;

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

            FS.HISFC.BizProcess.Integrate.Manager Mgr = new FS.HISFC.BizProcess.Integrate.Manager();

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

                foreach (FS.HISFC.Models.Base.Employee employee in emp)
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

                foreach (FS.HISFC.Models.Base.Employee employee in emp)
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
        /// ��ʼ�����߰�ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("����(+)", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T���, true, false, null);
            this.toolBarService.AddToolButton("ɾ��(-)", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            this.toolBarService.AddToolButton("ȫ��ɾ��", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Qȡ��, true, false, null);

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
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

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
            
            //�ڶ���Ϊ��Ա�б��
            if (node.Level == 2)
            {
                this.controls[Index].CurrentPerson = (FS.HISFC.Models.Base.Employee)node.Tag;
                this.controls[Index].DeptName = ((FS.HISFC.Models.Base.Employee)(FS.FrameWork.Management.Connection.Operator)).Dept.Name;
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
            this.controls[Index].Query(this.deptCode);
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
                    if (((FS.HISFC.Models.Base.Employee)childNode.Tag).ID == this.cmbEmp.Tag.ToString())
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
                cmbEmp_SelectedIndexChanged(sender,e);
            }
        }
        #endregion

        #endregion

        #region ����ɾ���ġ���ӡ�����ҡ���������

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

        /// <summary>
        /// ����
        /// </summary>
        private void Save()
        {
            int Index = this.neuTabControl1.SelectedIndex;

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
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
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
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
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

        #endregion

        #region delete method

        //private void baseTreeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        //{
        //    int Index = this.neuTabControl1.SelectedIndex;
        //    Index++;

        //    if (Index == 7) Index = 0;

        //    if (controls[Index].IsChange())
        //    {
        //        if (MessageBox.Show("�����Ѿ��ı�,�Ƿ񱣴�?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
        //            MessageBoxDefaultButton.Button2) == DialogResult.Yes)
        //        {
        //            if (controls[Index].Save() == -1)
        //            {
        //                e.Cancel = true;
        //                controls[Index].Focus();
        //            }
        //        }
        //    }

        //}

        #endregion

    }
}
