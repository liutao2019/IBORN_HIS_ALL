using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace Neusoft.HISFC.Components.Manager.Controls
{
    /// <summary>
    /// [��������: ��ԱCAǩ����Ϣ�б�]<br></br>
    /// [�� �� ��: ��ѩ��]<br></br>
    /// [����ʱ��: 2014��12��17]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucCaDepartmentManager : Neusoft.FrameWork.WinForms.Controls.ucBaseControl
    {
        //���ҹ�����
        Neusoft.HISFC.BizLogic.Manager.Department departmentManager = new Neusoft.HISFC.BizLogic.Manager.Department();
        //��Ա������
        Neusoft.HISFC.BizLogic.Manager.Person personManager = new Neusoft.HISFC.BizLogic.Manager.Person();
        private Neusoft.FrameWork.Public.ObjectHelper DutyHelper = new Neusoft.FrameWork.Public.ObjectHelper();
        private Neusoft.FrameWork.Public.ObjectHelper LevelHelper = new Neusoft.FrameWork.Public.ObjectHelper();
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


        public ucCaDepartmentManager()
        {
            InitializeComponent();
        }
        #region ���幤����

       protected Neusoft.FrameWork.WinForms.Forms.ToolBarService toolBarService = new Neusoft.FrameWork.WinForms.Forms.ToolBarService();

        #region ��ʼ��������

       protected override Neusoft.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("ǩ��", "ǩ����Ϣ", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.T��챨��, true, false, null);
            toolBarService.AddToolButton("CADemo", "CAǩ������", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.T��챨��, true, false, null);
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
            Neusoft.FrameWork.WinForms.Classes.Print pr = new Neusoft.FrameWork.WinForms.Classes.Print();
            pr.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.Border;
            pr.PrintPreview(this.splitContainer2.Panel2);
        }
        #endregion

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
                case "ǩ��":
                    CaEmployeeInfo();
                    break;
                case "CADemo":
                    CaDemo();
                    break;
                default :
                    break;
            }
        }
        #endregion
        
        /// <summary>
        /// ��ԱCAǩ����Ϣ
        /// </summary>
        public void CaEmployeeInfo()
        {   
            try
            {
                if (this.tvDeptList1.SelectedNode.Level == 2)
                {
                    if (this.neuSpread1_Sheet1.RowCount <= 0) return;
                    Neusoft.HISFC.Models.Base.Department dept = this.tvDeptList1.SelectedNode.Tag as Neusoft.HISFC.Models.Base.Department;
                    //��õ�ǰ������ID
                    string deptId = dept.ID;
                    Neusoft.HISFC.Models.Base.Department department = departmentManager.GetDeptmentById(deptId);
                    if (department == null)
                    {
                        MessageBox.Show("ѡ��Ŀ�����Ϣ�����ڣ�");
                        return;
                    }
                    //��ü����ж�Ӧ����Ա����
                    string personId = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 0].Text.Trim();
                    Neusoft.HISFC.Models.Base.Employee employee = personManager.GetPersonByID(personId);
                    if (employee == null)
                    {
                        MessageBox.Show("ѡ�����Ա��Ϣ�����ڣ�");
                        return; 
                    }

                    ucCaEmployeeInfoPanel ucCaEmployeeInfo = new ucCaEmployeeInfoPanel(employee);
                    Neusoft.FrameWork.WinForms.Classes.Function.PopForm.Text = "��Աǩ����Ϣ";
                    DialogResult dia = Neusoft.FrameWork.WinForms.Classes.Function.PopShowControl(ucCaEmployeeInfo, System.Windows.Forms.FormBorderStyle.None);
                    if (dia == DialogResult.OK)
                    {
                        //���ݵ�ǰ������ID����øÿ����µ���Ա
                        LoadPersons(deptId);
                    }
                }
                else if (this.tvDeptList1.SelectedNode.Text == "��Ա�б�")
                {
                    string personId = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 2].Text.Trim();
                    Neusoft.HISFC.Models.Base.Employee employee = personManager.GetPersonByID(personId);
                    if (employee == null)
                    {
                        MessageBox.Show("ѡ�����Ա��Ϣ�����ڣ�");
                        return;
                    }

                    ucCaEmployeeInfoPanel ucCaEmployeeInfo = new ucCaEmployeeInfoPanel(employee);
                    Neusoft.FrameWork.WinForms.Classes.Function.PopForm.Text = "��Աǩ����Ϣ";
                    DialogResult dia = Neusoft.FrameWork.WinForms.Classes.Function.PopShowControl(ucCaEmployeeInfo);
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
        /// CAǩ������
        /// </summary>
        public void CaDemo()
        {
            try
            {   
                ucCaDemo ucCaDemo = new ucCaDemo();
                DialogResult dia = Neusoft.FrameWork.WinForms.Classes.Function.PopShowControl(ucCaDemo, System.Windows.Forms.FormBorderStyle.None);
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
            parentTreeNode.Text = "������Ա";
            this.tvDeptList1.Nodes.Clear();
            parentTreeNode.ImageIndex = 0;
            this.tvDeptList1.Nodes.Add(parentTreeNode);
            deptTreeNode.Text = "�����б�";
            deptTreeNode.ImageIndex = 0;
            this.tvDeptList1.Nodes.Add(deptTreeNode);
            deptEmplTreeNode.Text = "��Ա�б�";
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
            FarPoint.Win.Spread.CellType.ImageCellType imageCellType = new FarPoint.Win.Spread.CellType.ImageCellType();
            imageCellType.Style = FarPoint.Win.RenderStyle.StretchAndScale;

            switch (flag)
            {
                case "person":
                    this.neuSpread1_Sheet1.Columns[0].Label = "��Ա���";
                    this.neuSpread1_Sheet1.Columns[0].Width = width * 5;
                    this.neuSpread1_Sheet1.Columns[1].Label = "��Ա����";
                    this.neuSpread1_Sheet1.Columns[1].Width = width * 5;
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
                    this.neuSpread1_Sheet1.Columns[7].Width = width * 3;
                    this.neuSpread1_Sheet1.Columns[8].Label = "״̬";
                    this.neuSpread1_Sheet1.Columns[8].Width = width * 3;
                    this.neuSpread1_Sheet1.Columns[9].Label = "ǩ��";
                    this.neuSpread1_Sheet1.Columns[9].Width = width * 5;
                    this.neuSpread1_Sheet1.Columns[9].CellType = imageCellType;

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
                    this.neuSpread1_Sheet1.Columns[1].Width = width * 5;
                    this.neuSpread1_Sheet1.Columns[2].Label = "�Ա�";
                    this.neuSpread1_Sheet1.Columns[2].Width = width * 3;
                    this.neuSpread1_Sheet1.Columns[3].Label = "��Ա���";
                    this.neuSpread1_Sheet1.Columns[3].Width = width * 5;
                    this.neuSpread1_Sheet1.Columns[4].Label = "������ʿվ";
                    this.neuSpread1_Sheet1.Columns[4].Width = width * 5;
                    this.neuSpread1_Sheet1.Columns[5].Label = "���";
                    this.neuSpread1_Sheet1.Columns[5].Width = width * 3;
                    this.neuSpread1_Sheet1.Columns[6].Label = "״̬";
                    this.neuSpread1_Sheet1.Columns[6].Width = width * 3;
                    break;
                case "deptEmpl":
                    this.neuSpread1_Sheet1.Columns[0].Label = "���ұ��";
                    this.neuSpread1_Sheet1.Columns[0].Width = width * 5;
                    this.neuSpread1_Sheet1.Columns[1].Label = "��������";
                    this.neuSpread1_Sheet1.Columns[1].Width = width * 7;
                    this.neuSpread1_Sheet1.Columns[2].Label = "��Ա���";
                    this.neuSpread1_Sheet1.Columns[2].Width = width * 5;
                    this.neuSpread1_Sheet1.Columns[3].Label = "��Ա����";
                    this.neuSpread1_Sheet1.Columns[3].Width = width * 5;
                    this.neuSpread1_Sheet1.Columns[4].Label = "�Ա�";
                    this.neuSpread1_Sheet1.Columns[4].Width = width * 3;
                    this.neuSpread1_Sheet1.Columns[5].Label = "��Ա���";
                    this.neuSpread1_Sheet1.Columns[5].Width = width * 5;
                    this.neuSpread1_Sheet1.Columns[6].Label = "������ʿվ";
                    this.neuSpread1_Sheet1.Columns[6].Width = width * 5;
                    this.neuSpread1_Sheet1.Columns[7].Label = "״̬";
                    this.neuSpread1_Sheet1.Columns[7].Width = width * 3;
                    this.neuSpread1_Sheet1.Columns[8].Label = "ǩ��";
                    this.neuSpread1_Sheet1.Columns[8].Width = width * 5;
                    this.neuSpread1_Sheet1.Columns[8].CellType = imageCellType;
                  
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
            //��������վ
            DataColumn column5 = new DataColumn("NURSE_CELL_CODE");
            column5.DataType = typeof(System.String);
            table.Columns.Add(column5);
            //���
            DataColumn column6 = new DataColumn("SORTID");
            column6.DataType = typeof(System.Int32);
            table.Columns.Add(column6);
            //�Ƿ�ͣ��
            DataColumn column7 = new DataColumn("VALID_STATE");
            column7.DataType = typeof(System.String);
            table.Columns.Add(column7);
            //ǩ��ͼƬ
            DataColumn column8 = new DataColumn("CA_SIGN");
            column8.DataType = typeof(System.Byte[]);
            table.Columns.Add(column8);

            dataSet.Tables.Add(table);

            return dataSet;
        }

        /// <summary>
        /// �������п���
        /// </summary>
        /// <returns></returns>
        public bool LoadDeptAll()
        {
            parentTreeNode.Nodes.Clear();
            Neusoft.HISFC.Models.Base.Department deptInfo = new Neusoft.HISFC.Models.Base.Department();

            ArrayList depts = departmentManager.GetDeptAllUserStopDisuse();
            if (depts == null || depts.Count < 1)
                return false;

            foreach (Neusoft.HISFC.Models.Base.Department info in depts)
            {
                //��������
                TreeNode kindnode = this.GetParentNode(info);
                //����
                TreeNode node = new TreeNode();
                node.Tag = info;
                node.Text = "��" + info.ID + "��" + info.Name;
                if (info.ValidState == Neusoft.HISFC.Models.Base.EnumValidState.Valid)//����
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

            //tvDeptList1.ExpandAll();
            return true;

        }


        /// <summary>
        /// ���ݴ���Ŀ���ʵ�壬�ҳ�����������
        /// </summary>
        /// <param name="info">����ʵ��</param>
        /// <returns>�������ͽڵ�</returns>
        private TreeNode GetParentNode(Neusoft.HISFC.Models.Base.Department info)
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

           Neusoft.HISFC.BizLogic.Manager.Constant con = new Neusoft.HISFC.BizLogic.Manager.Constant();
            LevelList = con.GetList(Neusoft.HISFC.Models.Base.EnumConstant.LEVEL);
            DutyList = con.GetList(Neusoft.HISFC.Models.Base.EnumConstant.POSITION);
            this.DutyHelper.ArrayObject = DutyList;
            this.LevelHelper.ArrayObject = LevelList;

          if(this.tvDeptList1.SelectedNode.Level == 2)
            {
                    this.neuSpread1_Sheet1.DataSource = personDataSet.Tables[0];
                    this.SetColumn("person");
                    this.neuSpread1_Sheet1.Columns[6].Visible = false;
                    Neusoft.HISFC.Models.Base.Department dept = this.tvDeptList1.SelectedNode.Tag as Neusoft.HISFC.Models.Base.Department;
                    LoadPersons(dept.ID);
                    this.ModifySortableColumn(this.neuSpread1_Sheet1.ColumnCount);

            }
            else if (e.Node.Text == "�����б�")
            {
                this.LoadDepts();
                this.ModifySortableColumn(this.neuSpread1_Sheet1.ColumnCount);
            }
            else if (e.Node.Text == "������Ա")
            {
                this.neuSpread1_Sheet1.DataSource = personDataSet.Tables[0];
                personDataSet.Tables[0].Rows.Clear();
                this.SetColumn("person");
                this.neuSpread1_Sheet1.Columns[6].Visible = false;
                this.neuSpread1_Sheet1.Columns[7].Visible = false;
            }
            else if (e.Node.Text == "��Ա�б�")
            {
                LoadDeptEmpl();
                this.ModifySortableColumn(this.neuSpread1_Sheet1.ColumnCount);
                this.ModifyFilterableColumn(1);
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
                                    new DataColumn("״̬"),
                                    new DataColumn("ǩ��")};
            dept.Columns.AddRange(colDept);
            dept.Columns["ǩ��"].DataType = typeof(System.Byte[]);
            
            dept.Rows.Clear();
                       
                ArrayList al = personManager.GetEmployeeAll();

                if (al == null)
                    return;
                Neusoft.FrameWork.Public.ObjectHelper helper = new Neusoft.FrameWork.Public.ObjectHelper(depts);
                int i = 1;
                foreach (Neusoft.HISFC.Models.Base.Employee pInfo in al)
                {
                    Neusoft.HISFC.Components.Manager.Function.ShowWaitForm("���ڲ�ѯǩ����Ϣ�����Ժ�...", i, al.Count, false);
                    Application.DoEvents();
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
                    string validState = string.Empty;
                    if (pInfo.ValidState == Neusoft.HISFC.Models.Base.EnumValidState.Valid)
                    {
                        validState = "����";
                    }
                    if (pInfo.ValidState == Neusoft.HISFC.Models.Base.EnumValidState.Invalid)
                    {
                        validState = "ͣ��";
                    }
                    if (pInfo.ValidState == Neusoft.HISFC.Models.Base.EnumValidState.Ignore)
                    {
                        validState = "����";
                    }
                    row["״̬"] = validState;
                    row["ǩ��"] = personManager.QueryEmplSignDataByEmplNo(pInfo.ID);

                    dept.Rows.Add(row);
                    i = i+1;
                }
                Neusoft.HISFC.Components.Manager.Function.HideWaitForm();
          
            this.neuSpread1_Sheet1.DataSource = dept;
            this.SetColumn("deptEmpl");
        }

        /// <summary>
        /// �����б�
        /// </summary>
        private void LoadDepts()
        {
            Neusoft.HISFC.Models.Base.Department obj = new Neusoft.HISFC.Models.Base.Department();
            
            //�õ����п���
            ArrayList depts = departmentManager.GetDeptAllUserStopDisuse();  
           
            DataSet deptAll = new DataSet();
            DataTable dept = new DataTable("dept");
         
            DataColumn[] colDept = {new DataColumn("���ұ���"),
								    new DataColumn("��������"),
                                    new DataColumn("��������")};
           
            dept.Columns.AddRange(colDept);
    
            dept.Rows.Clear();

            foreach (Neusoft.HISFC.Models.Base.Department deptInfo in depts)
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
            Neusoft.HISFC.Models.Base.Department obj = new Neusoft.HISFC.Models.Base.Department();

            //�õ����п���
            ArrayList depts = departmentManager.GetDeptmentByType(deptType);

            DataSet deptAll = new DataSet();
            DataTable dept = new DataTable("dept");

            DataColumn[] colDept = {new DataColumn("���ұ���"),
								    new DataColumn("��������"),
                                    new DataColumn("��������")};

            dept.Columns.AddRange(colDept);

            dept.Rows.Clear();

            foreach (Neusoft.HISFC.Models.Base.Department deptInfo in depts)
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
            int i = 1;
            foreach (Neusoft.HISFC.Models.Base.Employee info in list)
            {
                Neusoft.HISFC.Components.Manager.Function.ShowWaitForm("���ڲ�ѯǩ����Ϣ�����Ժ�...", i, list.Count, false);
                Application.DoEvents();
                string validState = string.Empty;
                if (info.ValidState == Neusoft.HISFC.Models.Base.EnumValidState.Valid)
                {
                    validState = "����";
                }
                if (info.ValidState == Neusoft.HISFC.Models.Base.EnumValidState.Invalid)
                {
                    validState = "ͣ��";
                }
                if (info.ValidState == Neusoft.HISFC.Models.Base.EnumValidState.Ignore)
                {
                    validState = "����";
                }
                table.Rows.Add(new Object[]{info.ID, info.Name ,info.Sex.Name, info.EmployeeType.Name,
											   DutyHelper.GetName(info.Duty.ID),LevelHelper.GetName(info.Level.ID),info.Nurse.Name, info.SortID,validState,personManager.QueryEmplSignDataByEmplNo(info.ID)});
                i = i + 1;

            }
            Neusoft.HISFC.Components.Manager.Function.HideWaitForm();

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
            CaEmployeeInfo();           
        }

        /// <summary>
        /// ����
        /// </summary>
        private void ModifySortableColumn(int rowcount)
        {
            for(int i=0;i < rowcount;i++)
            {
            this.neuSpread1_Sheet1.Columns.Get(i).AllowAutoSort = true;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        private void ModifyFilterableColumn(int column)
        {
            this.neuSpread1_Sheet1.Columns.Get(column).AllowAutoFilter = true;
        }
    }
}
