using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace FS.HISFC.Components.Manager.Controls
{
    /// <summary>
    /// UcSysFunction ��ժҪ˵����
    /// </summary>
    public class ucSysFunction : System.Windows.Forms.UserControl,
        FS.FrameWork.WinForms.Forms.IMaintenanceControlable
    {
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.TreeView treeView;

        private DataTable table;
        private DataView view;
        bool isDirty = false;

        private Hashtable modelCache = new Hashtable();
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuContextMenuStrip neuContextMenuStrip1;
        private ToolStripMenuItem mnuShow;
        private ToolStripMenuItem sQLά��ToolStripMenuItem;
        private System.ComponentModel.IContainer components;

        public ucSysFunction()
        {
            // �õ����� Windows.Forms ���������������ġ�
            InitializeComponent();
            this.Initialize();
            // TODO: �� InitializeComponent ���ú�����κγ�ʼ��
          

        }

        /// <summary> 
        /// ������������ʹ�õ���Դ��
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        private FarPoint.Win.Spread.CellType.ComboBoxCellType comboBoxCellType1 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
        private FarPoint.Win.Spread.CellType.ComboBoxCellType comboBoxCellType2 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();

        #region �����������ɵĴ���
        /// <summary> 
        /// �����֧������ķ��� - ��Ҫʹ�ô���༭�� 
        /// �޸Ĵ˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            this.treeView = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.fpSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.neuContextMenuStrip1 = new FS.FrameWork.WinForms.Controls.NeuContextMenuStrip();
            this.mnuShow = new System.Windows.Forms.ToolStripMenuItem();
            this.sQLά��ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            this.neuContextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView.HideSelection = false;
            this.treeView.ImageIndex = 0;
            this.treeView.ImageList = this.imageList1;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            this.treeView.SelectedImageIndex = 0;
            this.treeView.Size = new System.Drawing.Size(188, 416);
            this.treeView.TabIndex = 0;
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            this.treeView.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView_BeforeSelect);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(188, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(4, 416);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.fpSpread1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(192, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(780, 416);
            this.panel1.TabIndex = 3;
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "2.5.2007.2005";
            this.fpSpread1.AccessibleDescription = "";
            this.fpSpread1.BackColor = System.Drawing.Color.White;
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread1.FileName = "";
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.IsAutoSaveGridStatus = false;
            this.fpSpread1.IsCanCustomConfigColumn = true;
            this.fpSpread1.Location = new System.Drawing.Point(0, 0);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1});
            this.fpSpread1.Size = new System.Drawing.Size(780, 416);
            this.fpSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpSpread1.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread1.TextTipAppearance = tipAppearance1;
            this.fpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpSpread1_CellDoubleClick);
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // menuItem1
            // 
            this.menuItem1.Index = -1;
            this.menuItem1.Text = "";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = -1;
            this.menuItem2.Text = "";
            // 
            // neuContextMenuStrip1
            // 
            this.neuContextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuShow,
            this.sQLά��ToolStripMenuItem});
            this.neuContextMenuStrip1.Name = "neuContextMenuStrip1";
            this.neuContextMenuStrip1.Size = new System.Drawing.Size(153, 70);
            this.neuContextMenuStrip1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            // 
            // mnuShow
            // 
            this.mnuShow.Name = "mnuShow";
            this.mnuShow.Size = new System.Drawing.Size(152, 22);
            this.mnuShow.Text = "��ʾ����";
            this.mnuShow.Click += new System.EventHandler(this.mnuShow_Click_1);
            // 
            // sQLά��ToolStripMenuItem
            // 
            this.sQLά��ToolStripMenuItem.Name = "sQLά��ToolStripMenuItem";
            this.sQLά��ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.sQLά��ToolStripMenuItem.Text = "SQLά��";
            this.sQLά��ToolStripMenuItem.Click += new System.EventHandler(this.����ά��ToolStripMenuItem_Click);
            // 
            // ucSysFunction
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.treeView);
            this.Name = "ucSysFunction";
            this.Size = new System.Drawing.Size(972, 416);
            this.Load += new System.EventHandler(this.ucSysFunction_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            this.neuContextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion


        #region IToolBar ��Ա

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <returns></returns>
        public int Del()
        {
            // TODO:  ��� UcSysFunction.Del ʵ��
            DeleteData();
            return 0;
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <returns></returns>
        public int Print()
        {
            // TODO:  ��� UcSysFunction.Print ʵ��
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;
            p.PrintPreview(panel1);
            return 0;
        }

        /// <summary>
        /// ���
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            // TODO:  ��� UcSysFunction.Add ʵ��
            AddData();
            return 0;
        }

        /// <summary>
        /// �˳�
        /// </summary>
        /// <returns></returns>
        public int Exit()
        {
            // TODO:  ��� UcSysFunction.Exit ʵ��
            this.fpSpread1.StopCellEditing();
            this.FindForm().Close();
            return 0;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            // TODO:  ��� UcSysFunction.Save ʵ��
            SaveData();
            return 0;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public int  Export()
        {
            bool ret = false;
            //��������
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.Filter = "Excel|.xls";
                saveFileDialog1.FileName = "ϵͳ����ģ��";

                saveFileDialog1.Title = "��������";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    //��Excel ����ʽ��������
                    ret = fpSpread1.SaveExcel(saveFileDialog1.FileName, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
                    if (ret)
                    {
                        MessageBox.Show("�����ɹ���");
                        return 0;
                    }
                }
            }
            catch (Exception ee)
            {
                //������
                MessageBox.Show(ee.Message);
            }
            return -1;
        }
        #endregion

        public void Initialize()
        {
            
            comboBoxCellType1.Items = new string[] { "MDI", "Form", "FormDialog" };
            comboBoxCellType2.Items = new string[] { "Form", "Control", "Report" };

            TreeNode node = new TreeNode("ϵͳģ���б�");
            node.Tag = "";
            this.treeView.Nodes.Add(node);

            FS.HISFC.BizLogic.Manager.SysModelManager modelMgr = new FS.HISFC.BizLogic.Manager.SysModelManager();
            ArrayList modelList = modelMgr.LoadAll();
            foreach (FS.HISFC.Models.Admin.SysModel model in modelList)
            {
                AddTreeNode(node, model);
            }

            node.ExpandAll();
            this.treeView.SelectedNode = node;

            #region ����ģ�鹦����Ϣ --�޸�

            initDataSet();

            FS.HISFC.BizLogic.Manager.SysModelFunctionManager funMgr = new FS.HISFC.BizLogic.Manager.SysModelFunctionManager();
            ArrayList funList = funMgr.QuerySysModelFunction();
            foreach (FS.HISFC.Models.Admin.SysModelFunction fun in funList)
            {
                table.Rows.Add(new object[] { 
                    fun.SysCode, 
                    fun.FunName, 
                    fun.WinName,
                    fun.DllName,
                    fun.FormShowType,
                    fun.FormType,
                    fun.Memo, 
                    fun.Param,
                    fun.TreeControl.DllName,
                    fun.TreeControl.WinName,
                    fun.TreeControl.Param,
                    fun.ID,
                    fun.SortID,
                    "old" });
            }
        
            view = table.DefaultView;
          
            view.RowFilter = "";

            this.fpSpread1_Sheet1.DataSource = view;
            this.table.AcceptChanges();
            this.fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;

            #endregion

            SetCellType();

            this.fpSpread1_Sheet1.DataAutoSizeColumns = false;
            this.fpSpread1_Sheet1.Columns[2].Width = 200;
            this.fpSpread1_Sheet1.Columns[2].BackColor = Color.MistyRose;
            this.fpSpread1_Sheet1.Columns[2].ForeColor = Color.Black;
            this.fpSpread1.ContextMenuStrip = this.neuContextMenuStrip1;
            

        }

        private void AddTreeNode(TreeNode node, FS.HISFC.Models.Admin.SysModel model)
        {
            TreeNode child = new TreeNode(model.SysCode);
            child.Text = model.SysName;
            child.Tag = model.SysCode;
            modelCache.Add(model.SysCode, model.SysName);
            node.Nodes.Add(child);
        }

        private void initDataSet()
        {
            table = new DataTable("ModelFunction");
            DataColumn c0 = new DataColumn("SysCode");
            //c0.Caption = "ϵͳ��";
            c0.DataType = typeof(System.String);
            table.Columns.Add(c0);

            DataColumn c1 = new DataColumn("��������");//��������
            c1.DataType = typeof(System.String);
            //c1.Caption = "��������";
            table.Columns.Add(c1);

            DataColumn c2 = new DataColumn("�ؼ�/��������");//�ؼ�/��������
            c2.DataType = typeof(System.String);
            //c2.Caption = "�ؼ�/��������";
            table.Columns.Add(c2);

            DataColumn c3 = new DataColumn("Dll����");//dll����
            c3.DataType = typeof(System.String);
            //c3.Caption = "Dll����";
            table.Columns.Add(c3);

            DataColumn c4 = new DataColumn("������ʾ����");//������ʾ����
            c4.DataType = typeof(System.String);
            //c4.Caption = "������ʾ����";
            table.Columns.Add(c4);

            DataColumn c5 = new DataColumn("��������");//��������
            c5.DataType = typeof(System.String);
            //c5.Caption = "��������";
            table.Columns.Add(c5);

            DataColumn c6 = new DataColumn("���ڱ�ע");//���ڱ�ע
            c6.DataType = typeof(System.String);
            //c6.Caption = "���ڱ�ע";
            table.Columns.Add(c6);

            DataColumn c7 = new DataColumn("����Tag");//����Tag
            c7.DataType = typeof(System.String);
            //c7.Caption = "����Tag";
            table.Columns.Add(c7);

            DataColumn c8 = new DataColumn("����Dll����");//TreeDllName
            c8.DataType = typeof(System.String);
            //c8.Caption = "����Dll����";
            table.Columns.Add(c8);

            DataColumn c9 = new DataColumn("TreeWinName");//TreeWinName
            c9.DataType = typeof(System.String);
            //c9.Caption = "���Ĺ�������";
            table.Columns.Add(c9);

            DataColumn c10 = new DataColumn("����Tag");//TreeTag
            c10.DataType = typeof(System.String);
            //c10.Caption = "����Tag";
            table.Columns.Add(c10);
            
            DataColumn c11 = new DataColumn("ID");//����
            c11.DataType = typeof(System.String);
            c11.Caption = "ID";
            table.Columns.Add(c11);

            DataColumn c12 = new DataColumn("����");//����
            c12.DataType = typeof(System.String);
            c12.Caption = "����";
            table.Columns.Add(c12);

            DataColumn c13 = new DataColumn("״̬"); //���Ϊold �򴰿����� ���ܸ��� Ϊnew  �����޸�
            c13.DataType = typeof(System.String);
            table.Columns.Add(c13);
            table.AcceptChanges();
        }
        private FS.FrameWork.WinForms.Forms.IMaintenanceForm iMaintenaceForm = null;

        private void treeView_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            if (e.Node == this.treeView.Nodes[0])
                view.RowFilter = "";
            else
            {
                string filter = "SysCode = '" + e.Node.Tag.ToString() + "'";
                view.RowFilter = filter;
            }
            this.fpSpread1_Sheet1.Columns.Get(4).CellType = comboBoxCellType1;
            this.fpSpread1_Sheet1.Columns.Get(5).CellType = comboBoxCellType2;
            SetCellType();
        }

        private FS.HISFC.BizLogic.Manager.SysModelFunctionManager manager = new FS.HISFC.BizLogic.Manager.SysModelFunctionManager();
        private void AddData()
        {
            if (this.treeView.SelectedNode == this.treeView.Nodes[0])
            {
                MessageBox.Show("����ѡ��ϵͳģ�飡", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }
            this.view.AllowNew = true;
            DataRowView rowView = this.view.AddNew();
            rowView["SysCode"] = this.treeView.SelectedNode.Tag.ToString();
            rowView["ID"] = manager.GetNewID();
            rowView["����"] = "0";
            rowView["״̬"] = "new";
          
            
            this.fpSpread1_Sheet1.Columns.Get(4).CellType = comboBoxCellType1;
            this.fpSpread1_Sheet1.Columns.Get(5).CellType = comboBoxCellType2;

            IsDirty = true;

        }

        private void treeView_BeforeSelect(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
        {
            if (IsDirty)
            {
                if (!ValidateValue())
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// У��������ȷ��
        /// </summary>
        /// <returns></returns>
        private bool ValidateValue()
        {
            if (this.fpSpread1_Sheet1.Rows.Count > 0)
            {
                this.fpSpread1.StopCellEditing();
                for (int i = 0; i < this.fpSpread1_Sheet1.Rows.Count; i++)
                {
                    string temp = this.fpSpread1_Sheet1.GetText(i, 1).ToString();
                    if (temp == "")
                    {

                        MessageBox.Show("�������Ʋ���Ϊ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                    else if (!FS.FrameWork.Public.String.ValidMaxLengh(temp, 50))
                    {
                        MessageBox.Show("�������ƹ���");
                        return false;
                    }
                    string Gongneng = this.fpSpread1_Sheet1.GetText(i, 2).ToString();
                    if (Gongneng == "")
                    {
                        MessageBox.Show("�������Ʋ���Ϊ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                    else if (!FS.FrameWork.Public.String.ValidMaxLengh(Gongneng, 100))
                    {
                        MessageBox.Show("�������ƹ���");
                        return false;
                    }
                    if (!FS.FrameWork.Public.String.ValidMaxLengh(this.fpSpread1_Sheet1.GetText(i, 3).ToString(), 50))
                    {
                        MessageBox.Show("�������ƹ���");
                        return false;
                    }
                    string FormShowType = fpSpread1_Sheet1.GetText(i, 4).ToString();
                  
                    if (FormShowType == "" || FormShowType == "MDI" || FormShowType == "Form" || FormShowType == "FormDialog" )
                    {

                    }
                    else
                    {
                        MessageBox.Show("��ʾģʽ ����");
                        return false;
                    }
                    string FormType = fpSpread1_Sheet1.GetText(i, 5).ToString();
                    

                    string Mark = this.fpSpread1_Sheet1.GetText(i, 6).ToString();
                    if (!FS.FrameWork.Public.String.ValidMaxLengh(Mark, 1000))
                    {
                        MessageBox.Show("tag����");
                        return false;
                    }
                    string SortId = this.fpSpread1_Sheet1.GetText(i, 7).ToString();
                    if (!FS.FrameWork.Public.String.ValidMaxLengh(SortId, 6))
                    {
                        MessageBox.Show("˳��Ź���");
                        return false;
                    }

                }
                return true;

            }
            else
                return true;
        }

        private bool SaveData()
        {
            if (IsDirty == false || ValidateValue() == false)
            {
                
                return false;
            }

            this.fpSpread1.StopCellEditing();
            for(int i=0;i<this.view.Count;i++)
                this.view[i].EndEdit();

            DataTable added = table.GetChanges(DataRowState.Added);
            DataTable modified = table.GetChanges(DataRowState.Modified);
            DataTable deleted = table.GetChanges(DataRowState.Deleted);
            if (added == null && deleted == null && modified == null)
            {
                IsDirty = false;
                MessageBox.Show("�ޱ仯��");
                return true;
            }
            FS.HISFC.BizLogic.Manager.SysModelFunctionManager funMgr = new FS.HISFC.BizLogic.Manager.SysModelFunctionManager();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction(funMgr.Connection);

            bool saved = true;

            FS.HISFC.Models.Admin.SysModelFunction errorFun = null;
            try
            {
                //trans.BeginTransaction();

                funMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                if (deleted != null)
                {
                    deleted.RejectChanges();
                    ArrayList entities = GetEntityFromTable(deleted);
                    if (entities != null)
                    {
                        foreach (FS.HISFC.Models.Admin.SysModelFunction fun in entities)
                        {
                            if (funMgr.DeleteSysModelFunction(fun) < 0)
                            {
                                saved = false;
                                break;
                            }
                        }
                    }
                }
                if (added != null && saved)
                {
                    //added.AcceptChanges();
                    ArrayList entities = GetEntityFromTable(added);
                    if (entities != null)
                    {
                        foreach (FS.HISFC.Models.Admin.SysModelFunction fun in entities)
                        {
                            if (funMgr.InsertSysModelFunction(fun) < 0)
                            {
                                errorFun = fun;

                                saved = false;
                                break;

                            }
                        }
                    }
                }
                if (modified != null && saved)
                {
                
                    ArrayList entities = GetEntityFromTable(modified);
                    if (entities != null)
                    {
                        foreach (FS.HISFC.Models.Admin.SysModelFunction fun in entities)
                        {
                            if (funMgr.UpdateSysModelFunction(fun) < 0)
                            {
                                errorFun = fun;
                                saved = false;
                                break;

                            }
                        }
                    }
                }

                if (saved)
                {
                    FS.FrameWork.Management.PublicTrans.Commit();
                }
            }
            catch
            {

                saved = false;
            }
            finally
            {
                if (!saved)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();

                    string error = string.Empty;

                    if (funMgr.DBErrCode == 1 && errorFun != null)
                        error = "ϵͳģ��\"" + modelCache[errorFun.SysCode].ToString() + "\"�д������� \"" + errorFun.WinName + "\" ���ظ���";
                    else
                        error = funMgr.Err;

                    SetCellType();
                    MessageBox.Show("���ݱ���ʧ��!" + error, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    table.AcceptChanges(); //��Ϊ�õ���DataView ,�����ȵ��ñ�־λ��ȥ�����Ѿ�ɾ������
                    foreach (DataRow row in table.Rows)
                    {
                        row["״̬"] = "old";
                    }
                    table.AcceptChanges();
                    IsDirty = false;
                    SetCellType();

                }

            }
            if (saved)
            {

                MessageBox.Show("���ݱ���ɹ�!" , "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                IsDirty = false;
                return true;
            }
            else
            {
                IsDirty = true;
                return false;
            }





        }

        private void DeleteData()
        {
            if (this.fpSpread1_Sheet1.Rows.Count < 1)
            {
                MessageBox.Show("û�����ݿ�ɾ��");
                return;
            }
            if (this.fpSpread1_Sheet1.ActiveRowIndex >= 0)
            {
                this.fpSpread1.StopCellEditing();
                this.fpSpread1_Sheet1.Rows.Remove(this.fpSpread1_Sheet1.ActiveRowIndex , 1);
                IsDirty = true;
            }
        }

        /// <summary>
        /// ���ʵ������ݱ�
        /// </summary>
        /// <param name="changes"></param>
        /// <returns></returns>
        private ArrayList GetEntityFromTable(DataTable changes)
        {
            if (changes == null || changes.Rows.Count <= 0)
                return null;
            ArrayList entities = new ArrayList();

            foreach (DataRow row in changes.Rows)
            {
                FS.HISFC.Models.Admin.SysModelFunction fun = new FS.HISFC.Models.Admin.SysModelFunction();
                  fun.SysCode = row[0].ToString();
                    fun.FunName = row[1].ToString();
                    fun.WinName = row[2].ToString();
                    fun.DllName = row[3].ToString();
                    fun.FormShowType = row[4].ToString();
                    fun.FormType = row[5].ToString();
                    fun.Memo = row[6].ToString();
                    fun.Param = row[7].ToString();
                    fun.TreeControl.DllName = row[8].ToString();
                    fun.TreeControl.WinName = row[9].ToString();
                    fun.TreeControl.Param = row[10].ToString();
                    fun.ID = row[11].ToString();
                    fun.SortID = FS.FrameWork.Function.NConvert.ToInt32(row[12].ToString());
                  entities.Add(fun);
            }
            return entities;
        }

        /// <summary>
        /// �ر�
        /// </summary>
        /// <returns></returns>
        public bool Close()
        {
            if (IsDirty)
            {
                DataTable changes = table.GetChanges();
                if (changes == null)
                {
                    return true;
                }
                else
                {


                    DialogResult dlg = MessageBox.Show("�����Ѿ����޸ģ��Ƿ񱣴棿", "��ʾ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (dlg == DialogResult.Yes)
                    {
                        if (SaveData())
                        {
                            return true;
                        }
                        else
                            return false;
                    }
                    else if (dlg == DialogResult.Cancel)
                    {
                        return false;

                    }
                }

            }

            return true;
        }

        private void fpSpread1_Sheet1_CellChanged(object sender, FarPoint.Win.Spread.SheetViewEventArgs e)
        {
            if (e.Column == 3)
            {

                string dllname = this.fpSpread1_Sheet1.Cells[e.Row, 3].Text;
                if (dllname == "") return;
                try
                {
                    System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFrom(dllname + ".dll");
                    Type[] type = assembly.GetTypes();
                    FarPoint.Win.Spread.CellType.ComboBoxCellType funCellType = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
                    string[] ss = new string[type.Length];
                    int i = 0;
                    foreach (Type mytype in type)
                    {
                        if (mytype.IsPublic && mytype.IsClass)
                        {
                            ss[i] = mytype.ToString();
                            i++;
                        }
                    }
                    funCellType.Editable = true;
                    funCellType.Items = ss;
                    this.fpSpread1_Sheet1.Cells[e.Row, 2].CellType = funCellType;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (e.Column == 8)
            {
                string dllname = this.fpSpread1_Sheet1.Cells[e.Row, 8].Text;
                if (dllname == "") return;
                try
                {
                    System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFrom(dllname + ".dll");
                    Type[] type = assembly.GetTypes();
                    FarPoint.Win.Spread.CellType.ComboBoxCellType funCellType = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
                    string[] ss = new string[type.Length];
                    int i = 0;
                    foreach (Type mytype in type)
                    {
                        if (mytype.IsPublic && mytype.IsClass && mytype.IsSubclassOf(typeof(System.Windows.Forms.TreeView)) )
                        {
                            ss[i] = mytype.ToString();
                            i++;
                        }
                    }
                    funCellType.Editable = true;
                    funCellType.Items = ss;
                    this.fpSpread1_Sheet1.Cells[e.Row, 9].CellType = funCellType;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            IsDirty = true;
        }

        private void SetCellType()
        {
            FarPoint.Win.Spread.CellType.NumberCellType num = new FarPoint.Win.Spread.CellType.NumberCellType();
            num.MaximumValue = 999;
            num.MinimumValue = 0;
            fpSpread1_Sheet1.Columns[4].CellType = comboBoxCellType1;
            fpSpread1_Sheet1.Columns[5].CellType = comboBoxCellType2;
            fpSpread1_Sheet1.Columns[12].CellType = num;
            for (int i = 0; i < fpSpread1_Sheet1.Rows.Count; i++)
            {
                if (fpSpread1_Sheet1.Cells[i, 13].Text == "old")
                {
                    fpSpread1_Sheet1.Cells[i, 2].Locked = true;
                }
                else
                {
                    fpSpread1_Sheet1.Cells[i, 2].Locked = false;
                }
            }
            this.fpSpread1_Sheet1.SetColumnVisible(0, false);
            this.fpSpread1_Sheet1.SetColumnVisible(11, false);
            this.fpSpread1_Sheet1.SetColumnVisible(13, false);
        }

        private void fpSpread1_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData.GetHashCode() == Keys.A.GetHashCode() + Keys.Alt.GetHashCode())
            {
                //����
                Add();
            }
            if (keyData.GetHashCode() == Keys.D.GetHashCode() + Keys.Alt.GetHashCode())
            {
                //ɾ��
                Del();
            }
            if (keyData.GetHashCode() == Keys.S.GetHashCode() + Keys.Alt.GetHashCode())
            {
                //����
                Save();
            }
            if (keyData.GetHashCode() == Keys.E.GetHashCode() + Keys.Alt.GetHashCode())
            {
                //����
                Export();
            }
            if (keyData.GetHashCode() == Keys.P.GetHashCode() + Keys.Alt.GetHashCode())
            {
                //��ӡ
                Print();
            }
            if (keyData.GetHashCode() == Keys.X.GetHashCode() + Keys.Alt.GetHashCode())
            {
                //�˳�
                this.FindForm().Close();
            }

            return base.ProcessDialogKey(keyData);
        }


        #region IQueryControl ��Ա

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Delete()
        {
            this.DeleteData();
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDirty
        {
            get
            {
                return this.isDirty;
            }
            set
            {
                this.isDirty = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Query()
        {
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public FS.FrameWork.WinForms.Forms.IMaintenanceForm QueryForm
        {
            get
            {
                return iMaintenaceForm;
            }
            set
            {
                iMaintenaceForm = value;
                if (iMaintenaceForm == null) return;
                iMaintenaceForm.ShowCopyButton = false;
                iMaintenaceForm.ShowCutButton = false;
                iMaintenaceForm.ShowExportButton = false;
                iMaintenaceForm.ShowImportButton = false;
                iMaintenaceForm.ShowModifyButton = false;
                iMaintenaceForm.ShowNextRowButton = false;
                iMaintenaceForm.ShowPasteButton = false;
                iMaintenaceForm.ShowPreRowButton = false;
                iMaintenaceForm.ShowPrintButton = false;
                iMaintenaceForm.ShowPrintConfigButton= false;
                iMaintenaceForm.ShowPrintPreviewButton = false;


            }
        }

        #endregion

        private void ucSysFunction_Load(object sender, EventArgs e)
        {
            this.fpSpread1.IsAutoSaveGridStatus = false;
            this.fpSpread1.IsCanCustomConfigColumn = false;
            if(iMaintenaceForm != null) iMaintenaceForm.ShowExportButton = false;
            this.fpSpread1_Sheet1.CellChanged += new FarPoint.Win.Spread.SheetViewEventHandler(fpSpread1_Sheet1_CellChanged);
        }

        /// <summary>
        /// ˫���򿪴���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

        }

       

        private void mnuShow_Click_1(object sender, EventArgs e)
        {
            if (this.fpSpread1_Sheet1.ActiveRowIndex < 0) return;

            int row = this.fpSpread1_Sheet1.ActiveRowIndex;
            if (this.fpSpread1_Sheet1.Cells[row, 5].Text == "Form" || this.fpSpread1_Sheet1.Cells[row, 5].Text == "")
            {
                FS.HISFC.Models.Admin.SysMenu obj = new FS.HISFC.Models.Admin.SysMenu();
                obj.ModelFuntion.DllName = this.fpSpread1_Sheet1.Cells[row, 3].Text;
                obj.ModelFuntion.WinName = this.fpSpread1_Sheet1.Cells[row, 2].Text;
                obj.MenuParm = this.fpSpread1_Sheet1.Cells[row, 7].Text;
                obj.MenuName = this.fpSpread1_Sheet1.Cells[row, 1].Text;
                obj.ModelFuntion.FormShowType = this.fpSpread1_Sheet1.Cells[row, 4].Text;
                obj.ModelFuntion.TreeControl.WinName = this.fpSpread1_Sheet1.Cells[row, 9].Text;
                obj.ModelFuntion.TreeControl.DllName = this.fpSpread1_Sheet1.Cells[row, 8].Text;
                obj.ModelFuntion.TreeControl.Param = this.fpSpread1_Sheet1.Cells[row, 10].Text;
                obj.MenuWin = this.fpSpread1_Sheet1.Cells[row, 11].Text;
                Classes.Function.ShowForm(obj);
            }
        }

        #region IMaintenanceControlable ��Ա


        public int Copy()
        {
            return 0;
        }

        public int Cut()
        {
            return 0;
        }

        public int Import()
        {
            return 0;
        }

        public int Init()
        {
            return 0;
        }

        public int Modify()
        {
            return 0;
        }

        public int NextRow()
        {
            return 0;
        }

        public int Paste()
        {
            return 0;
        }

        public int PreRow()
        {
            return 0;
        }

        public int PrintConfig()
        {
            return 0;
        }

        public int PrintPreview()
        {
            return 0;
        }

        #endregion

        private void ����ά��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string id = this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, 11].Text;
            FS.FrameWork.WinForms.Controls.ucMaintenanceXML m = new FS.FrameWork.WinForms.Controls.ucMaintenanceXML(id);
            FS.FrameWork.WinForms.Forms.frmQuery f = new FS.FrameWork.WinForms.Forms.frmQuery(m);
            f.ShowDialog();
            
        }
    }
}
