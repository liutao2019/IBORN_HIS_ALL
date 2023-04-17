using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.HISFC.Components.Manager.Controls
{
    /// <summary>
    /// [��������: ������Աά��]<br></br>
    /// [�� �� ��: Ѧռ��]<br></br>
    /// [����ʱ��: 2006��12��7]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucDepartmentManager : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        //���ҹ�����
        FS.HISFC.BizLogic.Manager.Department departmentManager = new FS.HISFC.BizLogic.Manager.Department();
        //��Ա������
        FS.HISFC.BizLogic.Manager.Person personManager = new FS.HISFC.BizLogic.Manager.Person();
        private FS.FrameWork.Public.ObjectHelper DutyHelper = new FS.FrameWork.Public.ObjectHelper();
        private FS.FrameWork.Public.ObjectHelper LevelHelper = new FS.FrameWork.Public.ObjectHelper();
        //ʵ��������ά�����
        private TreeNode parentTreeNode = new TreeNode();
        //ʵ���������б���
        private TreeNode deptTreeNode = new TreeNode();
        //ʵ����������Ա���
        private TreeNode deptEmplTreeNode = new TreeNode();

        private TreeNode emplTreeNode = new TreeNode();

        //��ǰ��ѡ����
        private TreeNode curSelectedNode;
        //������Ա���ݼ�
        private DataSet personDataSet = null;
        //��Ա��Ϣ����.
        private Hashtable personCache = new Hashtable();
        private Hashtable deptsCache = new Hashtable();

        private ArrayList DutyList = null;
        private ArrayList LevelList = null;


        public ucDepartmentManager()
        {
            InitializeComponent();
        }
        #region ���幤����

       protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        #region ��ʼ��������

       protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("��ӿ���", "��ӿ���", 0, true, false,null);
            toolBarService.AddToolButton("�޸Ŀ���", "�޸Ŀ���", 1, true, false,null);
            toolBarService.AddToolButton("�����Ա", "�����Ա", 2, true, false,null);
            toolBarService.AddToolButton("�޸���Ա", "�޸���Ա", 3, true, false,null);
            toolBarService.AddToolButton("����", "����", 4, true, false,null);
            toolBarService.AddToolButton("��ӡԱ����ǩ", "��ӡԱ����ǩ", 5, true, false, null);
            return toolBarService;
        }

       #endregion

        #region ��д��������ť����
        /// <summary>
        /// �˳�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Exit(object sender, object neuObject)
        {
            return base.Exit(sender, neuObject);
        }
        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Print(object sender, object neuObject)
        {
            PrintInfo();
            return base.Print(sender, neuObject);
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Export(object sender, object neuObject)
        {
            ExportInfo();
            return base.Export(sender, neuObject);
        }

        #region ��ӡ����
        /// <summary>
        /// ��ӡ����
        /// </summary>
        private void PrintInfo()
        {
            FS.FrameWork.WinForms.Classes.Print pr = new FS.FrameWork.WinForms.Classes.Print();
            pr.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;
            pr.PrintPreview(this.splitContainer1.Panel2);
        }
        #endregion

        //�޸�ʱ�䣺������������������
        //�޸��ˣ�·־��
        //�޸�Ŀ�ģ����Ƶ�������
        #region ��������
        private void ExportInfo()
        {
            bool tr = false;
            string fileName = "";
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "excel|*.xls";
            saveFile.Title = "������Excel";

            saveFile.FileName = "������Ա���� " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString().Replace(':', '-');

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                if (saveFile.FileName.Trim() != "")
                {
                    fileName = saveFile.FileName;
                    tr = this.neuSpread1.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
                }
                else
                {
                    MessageBox.Show("�ļ�������Ϊ��!");
                    return;
                }

                if (tr)
                {
                    MessageBox.Show("�����ɹ�!");
                }
                else
                {
                    MessageBox.Show("����ʧ��!");
                }
            }
        }

        #endregion
        #endregion

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch(e.ClickedItem.Text)
            {
                case "��ӿ���":
                    AddNewDepartment();
                    break;
                case "�޸Ŀ���":
                    ModifyDepartment();
                    break;
                case "�����Ա":
                    AddEmployee();
                    break;
                case "�޸���Ա":
                    if (this.tvDeptList1.SelectedNode.Parent != null && this.neuSpread1_Sheet1.RowCount > 0)
                    ModifyEmployee();
                    break;
                case "����":
                    FindInfo();
                    break;
                case "��ӡԱ����ǩ":
                    if (this.tvDeptList1.SelectedNode.Parent != null && this.neuSpread1_Sheet1.RowCount > 0)
                    PrintEmployeeBarCode();
                    break;
                default :
                    break;
            }
        }
        #endregion

        /// <summary>
        /// ���ҷ���
        /// </summary>
        public void FindInfo()
        {
            ucFindDeptAndEmployee ucFindDeptEmpl = new ucFindDeptAndEmployee();
            ucFindDeptEmpl.UcDeptMgr = this;


            FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "����";
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucFindDeptEmpl);
        }

        /// <summary>
        /// �����Ա
        /// </summary>
        public void AddEmployee()
        {
            try
            {
                ucEmployeeInfoPanel employeeInfo = new ucEmployeeInfoPanel();
                employeeInfo.IsModify = false;
                FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "�����Ա";
                DialogResult dia = FS.FrameWork.WinForms.Classes.Function.PopShowControl(employeeInfo);
                if (dia == DialogResult.OK)
                {
                                              
                }
                else if (dia == DialogResult.Cancel)
                {
                    if (employeeInfo.tr)
                    {
                       
                    }
                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }
            
        }

        /// <summary>
        /// ��ӡԱ����ǩ
        /// </summary>
        public void PrintEmployeeBarCode()
        {
            try
            {
                if (this.tvDeptList1.SelectedNode.Level == 2)
                {
                    if (this.neuSpread1_Sheet1.RowCount <= 0) return;
                    FS.HISFC.Models.Base.Department dept = this.tvDeptList1.SelectedNode.Tag as FS.HISFC.Models.Base.Department;
                    //��õ�ǰ������ID
                    string deptId = dept.ID;
                    FS.HISFC.Models.Base.Department department = departmentManager.GetDeptmentById(deptId);
                    if (department == null)
                    {
                        MessageBox.Show("ѡ��Ŀ�����Ϣ�����ڣ�");
                        return;
                    }
                    //��ü����ж�Ӧ����Ա����
                    string personId = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 0].Text.Trim();
                    FS.HISFC.Models.Base.Employee employee = personManager.GetPersonByID(personId);
                    if (employee == null)
                    {
                        MessageBox.Show("ѡ�����Ա��Ϣ�����ڣ�");
                        return;
                    }

                    ucEmployeeBarcode ucEmployeeBarCode = new ucEmployeeBarcode();
                    ucEmployeeBarCode.PrintEmpBarCode(employee);
                }
                else
                {
                    return;
                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }
            
        }
        
        /// <summary>
        /// �޸���Ա
        /// </summary>
        public void ModifyEmployee()
        {   
            try
            {
                if (this.tvDeptList1.SelectedNode.Level == 2)
                {
                    if (this.neuSpread1_Sheet1.RowCount <= 0) return;
                    FS.HISFC.Models.Base.Department dept = this.tvDeptList1.SelectedNode.Tag as FS.HISFC.Models.Base.Department;
                    //��õ�ǰ������ID
                    string deptId = dept.ID;
                    FS.HISFC.Models.Base.Department department = departmentManager.GetDeptmentById(deptId);
                    if (department == null)
                    {
                        MessageBox.Show("ѡ��Ŀ�����Ϣ�����ڣ�");
                        return;
                    }
                    //��ü����ж�Ӧ����Ա����
                    string personId = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 0].Text.Trim();
                    FS.HISFC.Models.Base.Employee employee = personManager.GetPersonByID(personId);
                    if (employee == null)
                    {
                        MessageBox.Show("ѡ�����Ա��Ϣ�����ڣ�");
                        return; 
                    }
                    
                    ucEmployeeInfoPanel ucEmployeeInfo = new ucEmployeeInfoPanel(employee);
                    ucEmployeeInfo.IsModify = true;
                    FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "�޸���Ա��Ϣ";
                    DialogResult dia = FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucEmployeeInfo);
                    if (dia == DialogResult.OK)
                    {
                        //���ݵ�ǰ������ID����øÿ����µ���Ա
                        LoadPersons(deptId);
                    }
                }
                else 
                {
                    return;
                }
            }
            catch(Exception a)
            {
                MessageBox.Show(a.Message);
            }
        }
      
        /// <summary>
        /// ��ӿ���
        /// </summary>
        public void AddNewDepartment()
        {
            try
            {

                ucDeptmentInfoPanel ucDeptInfo = new ucDeptmentInfoPanel();
                FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "��ӿ���";

                DialogResult di = FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucDeptInfo);
                if (di == DialogResult.OK)
                {
                    parentTreeNode.Nodes.Clear();
                    deptsCache = new Hashtable();
                    LoadDeptAll();
                }
                else if (di == DialogResult.Cancel)
                {
                    if (ucDeptInfo.tr)
                    {
                        parentTreeNode.Nodes.Clear();
                        deptsCache = new Hashtable();
                        LoadDeptAll();
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        /// <summary>
        /// �޸Ŀ���
        /// </summary>
        public void ModifyDepartment()
        {
            try
            {
                if (this.tvDeptList1.SelectedNode.Level == 2)
                {
                    FS.HISFC.Models.Base.Department dept = this.tvDeptList1.SelectedNode.Tag as FS.HISFC.Models.Base.Department;
                    //ͨ�����ұ����ÿ�������
                    FS.HISFC.Models.Base.Department department = departmentManager.GetDeptmentById(dept.ID);
                    if (department == null) MessageBox.Show("ѡ�еĿ��Ҳ����ڣ�");
                    ucDeptmentInfoPanel ucDeptInfo = new ucDeptmentInfoPanel(department);
                    FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "�޸Ŀ���";
                    DialogResult diaR = FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucDeptInfo);
                    if (diaR == DialogResult.OK)
                    {
                        parentTreeNode.Nodes.Clear();
                        deptsCache = new Hashtable();                        
                        LoadDeptAll();
                    }
                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }
        }
        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void BeforeLoad()
        {
            parentTreeNode.Text = "����ά��";
            this.tvDeptList1.Nodes.Clear();
            parentTreeNode.ImageIndex = 3;
            this.tvDeptList1.Nodes.Add(parentTreeNode);
            deptTreeNode.Text = "�����б�";
            deptTreeNode.ImageIndex = 2;
            this.tvDeptList1.Nodes.Add(deptTreeNode);
            deptEmplTreeNode.Text = "������Ա�б�";
            deptEmplTreeNode.ImageIndex = 0;
            this.tvDeptList1.Nodes.Add(deptEmplTreeNode);
            //����All����
            LoadDeptAll();
            //��ʼ����Ա���ݼ�
            personDataSet = InitPersonDataSet();
            this.neuSpread1.DataSource = personDataSet.Tables[0];
            //����FarPoint��ʾ��
            SetColumn("person");
            this.tvDeptList1.SelectedNode = this.tvDeptList1.Nodes[0];

        }
        /// <summary>
        /// ����FarPoint��ʾ��
        /// </summary>
        /// <param name="flag"></param>
        private void SetColumn(string flag)
        {
            int width = 20;

            switch (flag)
            {
                case "person":
                    this.neuSpread1_Sheet1.Columns[0].Label = "��Ա���";
                    this.neuSpread1_Sheet1.Columns[0].Width = width * 5;
                    this.neuSpread1_Sheet1.Columns[1].Label = "��Ա����";
                    this.neuSpread1_Sheet1.Columns[1].Width = width * 6;
                    this.neuSpread1_Sheet1.Columns[2].Label = "�Ա�";
                    this.neuSpread1_Sheet1.Columns[2].Width = width * 3;
                    this.neuSpread1_Sheet1.Columns[3].Label = "��Ա���";
                    this.neuSpread1_Sheet1.Columns[3].Width = width * 5;
                    this.neuSpread1_Sheet1.Columns[4].Label = "ְ��";
                    this.neuSpread1_Sheet1.Columns[4].Width = width * 5;
                    this.neuSpread1_Sheet1.Columns[5].Label = "ְ��";
                    this.neuSpread1_Sheet1.Columns[5].Width = width * 5;
                    this.neuSpread1_Sheet1.Columns[6].Label = "������ʿվ";
                    this.neuSpread1_Sheet1.Columns[6].Width = width * 5;
                    this.neuSpread1_Sheet1.Columns[7].Label = "���";
                    this.neuSpread1_Sheet1.Columns[7].Width = width * 4;
                    this.neuSpread1_Sheet1.Columns[8].Label = "�Ƿ�ͣ��";
                    this.neuSpread1_Sheet1.Columns[8].Width = width * 4;
                    this.neuSpread1_Sheet1.Columns[9].Label = "����ҽ������(ҽ��)";
                    this.neuSpread1_Sheet1.Columns[9].Width = width * 7;

                    break;
                case "dept":
                    this.neuSpread1_Sheet1.Columns[0].Label = "���ұ��";
                    this.neuSpread1_Sheet1.Columns[0].Width = width * 7;
                    this.neuSpread1_Sheet1.Columns[1].Label = "��������";
                    this.neuSpread1_Sheet1.Columns[1].Width = width * 7;
                    this.neuSpread1_Sheet1.Columns[2].Label = "��������";
                    this.neuSpread1_Sheet1.Columns[2].Width = width * 7;
                    break;
                case "empl":
                    this.neuSpread1_Sheet1.Columns[0].Label = "��Ա���";
                    this.neuSpread1_Sheet1.Columns[0].Width = width * 5;
                    this.neuSpread1_Sheet1.Columns[1].Label = "��Ա����";
                    this.neuSpread1_Sheet1.Columns[1].Width = width * 6;
                    this.neuSpread1_Sheet1.Columns[2].Label = "�Ա�";
                    this.neuSpread1_Sheet1.Columns[2].Width = width * 3;
                    this.neuSpread1_Sheet1.Columns[3].Label = "��Ա���";
                    this.neuSpread1_Sheet1.Columns[3].Width = width * 5;
                    this.neuSpread1_Sheet1.Columns[4].Label = "������ʿվ";
                    this.neuSpread1_Sheet1.Columns[4].Width = width * 5;
                    this.neuSpread1_Sheet1.Columns[5].Label = "���";
                    this.neuSpread1_Sheet1.Columns[5].Width = width * 4;
                    this.neuSpread1_Sheet1.Columns[6].Label = "�Ƿ�ͣ��";
                    this.neuSpread1_Sheet1.Columns[6].Width = width * 4;
                    break;
                case "deptEmpl":
                    this.neuSpread1_Sheet1.Columns[0].Label = "���ұ��";
                    this.neuSpread1_Sheet1.Columns[0].Width = width * 4;
                    this.neuSpread1_Sheet1.Columns[1].Label = "��������";
                    this.neuSpread1_Sheet1.Columns[1].Width = width * 5;
                    this.neuSpread1_Sheet1.Columns[2].Label = "��Ա���";
                    this.neuSpread1_Sheet1.Columns[2].Width = width * 3;
                    this.neuSpread1_Sheet1.Columns[3].Label = "��Ա����";
                    this.neuSpread1_Sheet1.Columns[3].Width = width * 4;
                    this.neuSpread1_Sheet1.Columns[4].Label = "�Ա�";
                    this.neuSpread1_Sheet1.Columns[4].Width = width * 2;
                    this.neuSpread1_Sheet1.Columns[5].Label = "��Ա���";
                    this.neuSpread1_Sheet1.Columns[5].Width = width * 4;
                    this.neuSpread1_Sheet1.Columns[6].Label = "������ʿվ";
                  
                    break;
            }
        }

        /// <summary>
        /// ��ʼ����Ա���ݼ�
        /// </summary>
        /// <returns></returns>
        private DataSet InitPersonDataSet()
        {
            DataSet dataSet = new DataSet();
            DataTable table = new DataTable();
            //��Ա����
            DataColumn column1 = new DataColumn("EMPL_CODE");
            column1.DataType = typeof(System.String);
            //column1.ReadOnly = true;
            table.Columns.Add(column1);
            //��Ա����
            DataColumn column2 = new DataColumn("EMPL_NAME");
            column2.DataType = typeof(System.String);
            table.Columns.Add(column2);
            //�Ա�
            DataColumn column3 = new DataColumn("SEX_CODE");
            column3.DataType = typeof(System.String);
            table.Columns.Add(column3);
            //��Ա���
            DataColumn column4 = new DataColumn("EMPL_TYPE");
            column4.DataType = typeof(System.String);
            table.Columns.Add(column4);
            //ְ��
            DataColumn column9 = new DataColumn("ְ��");
            column9.DataType = typeof(System.String);
            table.Columns.Add(column9);
            //ְ��
            DataColumn column10 = new DataColumn("ְ��");
            column10.DataType = typeof(System.String);
            table.Columns.Add(column10);

            //ְ��
            DataColumn column11 = new DataColumn("����ҽ������");
            column11.DataType = typeof(System.String);
            table.Columns.Add(column11);

            //��������վ
            DataColumn column5 = new DataColumn("NURSE_CELL_CODE");
            column5.DataType = typeof(System.String);
            table.Columns.Add(column5);
            //���
            DataColumn column6 = new DataColumn("SORTID");
            column6.DataType = typeof(System.String);
            table.Columns.Add(column6);
            //�Ƿ���
            DataColumn column7 = new DataColumn("VALID_STATE");
            //column7.DataType = typeof(System.Int32);
            column7.DataType = typeof(System.String);
            table.Columns.Add(column7);

            dataSet.Tables.Add(table);

            return dataSet;
        }

        /// <summary>
        /// �������п���
        /// </summary>
        /// <returns></returns>
        public bool LoadDeptAll()
        {

            //Insert Into TreeView. TreeNode Contains DeptID,DeptName,DeptType.
            parentTreeNode.Nodes.Clear();
            FS.HISFC.Models.Base.Department deptInfo = new FS.HISFC.Models.Base.Department();

            ArrayList depts = departmentManager.GetDeptAllUserStopDisuse();
            if (depts == null || depts.Count < 1)
                return false;

            foreach (FS.HISFC.Models.Base.Department info in depts)
            {
                //��������
                TreeNode kindnode = this.GetParentNode(info);
                //����
                TreeNode node = new TreeNode();
                node.Tag = info;
                node.Text = "(" + info.ID + ")" + info.Name;
                if (info.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid)//����
                {
                    node.BackColor = Color.White;
                }
                else
                {
                    node.BackColor = Color.Silver;
                }
                kindnode.Nodes.Add(node);

                deptsCache.Add(info.ID, info);
            }

            tvDeptList1.ExpandAll();
            return true;

        }


        /// <summary>
        /// ���ݴ���Ŀ���ʵ�壬�ҳ�����������
        /// </summary>
        /// <param name="info">����ʵ��</param>
        /// <returns>�������ͽڵ�</returns>
        private TreeNode GetParentNode(FS.HISFC.Models.Base.Department info)
        {
            //��һ���ڵ����ҿ��ҵ�����
            foreach (TreeNode node in this.parentTreeNode.Nodes)
            {
                if (node.Tag.ToString() == info.DeptType.ID.ToString())
                {
                    return node;
                }
            }

            //�����һ���ڵ����Ҳ������ҵ��������ͣ�������һ����������
            TreeNode kindnode = new TreeNode();
            kindnode.Tag = info.DeptType.ID;
            kindnode.Text = info.DeptType.Name;
            this.parentTreeNode.Nodes.Add(kindnode);
            return kindnode;
        }


        /// <summary>
        /// ѡ��������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvDeptList1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (curSelectedNode == e.Node)
                return;

           FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
            LevelList = con.GetList(FS.HISFC.Models.Base.EnumConstant.LEVEL);
            DutyList = con.GetList(FS.HISFC.Models.Base.EnumConstant.POSITION);
            this.DutyHelper.ArrayObject = DutyList;
            this.LevelHelper.ArrayObject = LevelList;

          if(this.tvDeptList1.SelectedNode.Level == 2)
            {
                    this.neuSpread1_Sheet1.DataSource = personDataSet.Tables[0];
                    this.SetColumn("person");
                    this.neuSpread1_Sheet1.Columns[6].Visible = false;
                    //this.neuSpread1_Sheet1.Columns[7].Visible = false;
                    //this.neuSpread1_Sheet1.Columns[8].Visible = false;
                    FS.HISFC.Models.Base.Department dept = this.tvDeptList1.SelectedNode.Tag as FS.HISFC.Models.Base.Department;
                    LoadPersons(dept.ID);
                    this.ModifySortableColumn(this.neuSpread1_Sheet1.ColumnCount);

            }
            else if (e.Node.Text == "�����б�")
            {
                this.LoadDepts();
                this.ModifySortableColumn(this.neuSpread1_Sheet1.ColumnCount);
            }
            else if (e.Node.Text == "����ά��")
            {
                this.neuSpread1_Sheet1.DataSource = personDataSet.Tables[0];
                personDataSet.Tables[0].Rows.Clear();
                this.SetColumn("person");
                this.neuSpread1_Sheet1.Columns[6].Visible = false;
            }
            else if (e.Node.Text == "������Ա�б�")
            {
                LoadDeptEmpl();
                this.ModifySortableColumn(this.neuSpread1_Sheet1.ColumnCount);
            }
            else
            {
                this.LoadDeptsByType(e.Node.Tag.ToString());
                this.ModifySortableColumn(this.neuSpread1_Sheet1.ColumnCount);
            }
            curSelectedNode = e.Node;
        }

        /// <summary>
        /// ������Ա�б�
        /// </summary>
        private void LoadDeptEmpl()
        {
            ArrayList depts = departmentManager.GetDeptmentAll();

            DataSet deptAll = new DataSet();
            DataTable dept = new DataTable("dept");

            DataColumn[] colDept = {new DataColumn("���ұ���"),
								    new DataColumn("��������"),
									new DataColumn("��Ա����"),
						            new DataColumn("��Ա����"),
						            new DataColumn("�Ա�"),
						            new DataColumn("��Ա����"),
						            new DataColumn("������ʿվ"),
                                    new DataColumn("����ҽ������(ҽ��)")};
            dept.Columns.AddRange(colDept);
            
            dept.Rows.Clear();
                       
                ArrayList al = personManager.GetEmployeeAll();

                if (al == null)
                    return;
                FS.FrameWork.Public.ObjectHelper helper = new FS.FrameWork.Public.ObjectHelper(depts);
                foreach (FS.HISFC.Models.Base.Employee pInfo in al)
                {
                    DataRow row = dept.NewRow();

                    row["���ұ���"] = pInfo.Dept.ID;
                    row["��������"] = helper.GetName(pInfo.Dept.ID);
                    row["��Ա����"] = pInfo.ID;
                    row["��Ա����"] = pInfo.Name;
                    row["�Ա�"] = pInfo.Sex.Name;
                    row["��Ա����"] = pInfo.EmployeeType.Name;
                    if (pInfo.Nurse.ID != "" || deptsCache.Contains(pInfo.Nurse.ID))

                        row["������ʿվ"] = deptsCache[pInfo.Nurse.ID].ToString();
                    else
                        row["������ʿվ"] = "";

                    row["����ҽ������(ҽ��)"] = "999";
                    dept.Rows.Add(row);
                }
          
            this.neuSpread1_Sheet1.DataSource = dept;
            this.SetColumn("deptEmpl");
        }

        /// <summary>
        /// �����б�
        /// </summary>
        private void LoadDepts()
        {
            FS.HISFC.Models.Base.Department obj = new FS.HISFC.Models.Base.Department();
            
            //�õ����п���
            ArrayList depts = departmentManager.GetDeptAllUserStopDisuse();  
           
            DataSet deptAll = new DataSet();
            DataTable dept = new DataTable("dept");
         
            DataColumn[] colDept = {new DataColumn("���ұ���"),
								    new DataColumn("��������"),
                                    new DataColumn("��������")};
           
            dept.Columns.AddRange(colDept);
    
            dept.Rows.Clear();

            foreach (FS.HISFC.Models.Base.Department deptInfo in depts)
            {
                DataRow row = dept.NewRow();
                row["���ұ���"] = deptInfo.ID.ToString().Trim();
                row["��������"] = deptInfo.Name;
                row["��������"] = deptInfo.DeptType.Name;

                dept.Rows.Add(row);
            }
            deptAll.Tables.Add(dept);

            this.neuSpread1_Sheet1.DataSource = deptAll;
            this.SetColumn("dept");
        }

        /// <summary>
        /// ����ĳһ���͵Ŀ���
        /// </summary>
        /// <param name="deptType">���ͱ���</param>
        private void LoadDeptsByType(string deptType)
        {
            FS.HISFC.Models.Base.Department obj = new FS.HISFC.Models.Base.Department();

            //�õ����п���
            ArrayList depts = departmentManager.GetDeptmentByType(deptType);

            DataSet deptAll = new DataSet();
            DataTable dept = new DataTable("dept");

            DataColumn[] colDept = {new DataColumn("���ұ���"),
								    new DataColumn("��������"),
                                    new DataColumn("��������")};

            dept.Columns.AddRange(colDept);

            dept.Rows.Clear();

            foreach (FS.HISFC.Models.Base.Department deptInfo in depts)
            {
                DataRow row = dept.NewRow();
                row["���ұ���"] = deptInfo.ID.ToString().Trim();
                row["��������"] = deptInfo.Name;
                row["��������"] = deptInfo.DeptType.Name;

                dept.Rows.Add(row);
            }
            deptAll.Tables.Add(dept);

            this.neuSpread1_Sheet1.DataSource = deptAll;
            this.SetColumn("dept");
        }

        /// <summary>
        /// ���ݽ����Ϣ������Ϣ
        /// </summary>
        /// <param name="deptID"></param>
        private void LoadPersons(string deptID)
        {   
            //���ݿ���ID���ȫ����Ա��Ϣ
            ArrayList list = personManager.GetPersonsByDeptIDAll(deptID);
            
            personDataSet.Tables[0].Clear();

            if (list != null && list.Count > 0)
            {
                AddPersonsToTable(list, personDataSet.Tables[0]);
            }
            
            string valid;

            //�����Ա����Ч�Բ�Ϊ"����",�� 0,����ʾ��ɫ��

            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; ++i)
            {
                valid = this.neuSpread1_Sheet1.GetValue(i, 8).ToString();
                if (valid != "����")
                    this.neuSpread1_Sheet1.Rows[i].BackColor = Color.MistyRose;
            }

            if (this.neuSpread1_Sheet1.Rows.Count > 0)
                this.neuSpread1_Sheet1.ActiveRowIndex = 0;
            this.SetColumn("person");
        }

        /// <summary>
        /// ����ArryList���DataTable
        /// </summary>
        /// <param name="list"></param>
        /// <param name="table"></param>
        private void AddPersonsToTable(ArrayList list, DataTable table)
        {

            foreach (FS.HISFC.Models.Base.Employee info in list)
            {
                /*[2007/02/05] info.ValidState�ֶ�ȡֵΪ:0(����),1(ͣ��),2(����)
                 *             ԭ����ʾ��������,���Խ����Ϊ���� 
                 * 
                 * table.Rows.Add(new Object[]{info.ID, info.Name ,info.Sex.Name, info.EmployeeType.Name,
				 *							   DutyHelper.GetName(info.Duty.ID),LevelHelper.GetName(info.Level.ID),info.Nurse.Name, info.SortID,info.ValidState});
                 * 
                 */
                string validState = string.Empty;
                if (info.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid)
                {
                    validState = "����";
                }
                if (info.ValidState == FS.HISFC.Models.Base.EnumValidState.Invalid)
                {
                    validState = "ͣ��";
                }
                if (info.ValidState == FS.HISFC.Models.Base.EnumValidState.Ignore)
                {
                    validState = "����";
                }
                //info.SortID
                table.Rows.Add(new Object[]{info.ID, info.Name ,info.Sex.Name, info.EmployeeType.Name,
											   DutyHelper.GetName(info.Duty.ID),LevelHelper.GetName(info.Level.ID),info.Nurse.Name,info.SortID,validState,info.UserCode});

            }

        }


        private void tvDeptList1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                this.tvDeptList1.SelectedNode = this.tvDeptList1.GetNodeAt(e.X, e.Y);
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }
        }

        private void ucDepartmentManager_Load(object sender, EventArgs e)
        {
            this.neuSpread1_Sheet1.Rows[-1].Locked = true;
            this.neuSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            BeforeLoad();
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            ModifyEmployee();
            
        }
        /// <summary>
        /// ����������
        /// </summary>
        private void ModifySortableColumn(int rowcount)
        {
            for(int i=0;i < rowcount;i++)
            {
            this.neuSpread1_Sheet1.Columns.Get(i).AllowAutoSort = true;
            }
        }
    }
}
