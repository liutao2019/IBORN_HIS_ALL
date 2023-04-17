namespace FS.HISFC.Components.Order.Medical.Controls
{
    partial class ucPopedomManagement
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucPopedomManagement));
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.ComboBoxCellType comboBoxCellType1 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.ComboBoxCellType comboBoxCellType2 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("手术权限");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("处方权限");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("组套权限");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("会诊权限");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("大型仪器");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("权限列表", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5});
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("医生列表");
            this.neuContextMenuStrip1 = new FS.FrameWork.WinForms.Controls.NeuContextMenuStrip();
            this.item0 = new System.Windows.Forms.ToolStripMenuItem();
            this.DoctorImageList = new System.Windows.Forms.ImageList(this.components);
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpPopedom = new FarPoint.Win.Spread.SheetView();
            this.neuSplitter1 = new FS.FrameWork.WinForms.Controls.NeuSplitter();
            this.neuTabControl1 = new FS.FrameWork.WinForms.Controls.NeuTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tvPopedom = new FS.FrameWork.WinForms.Controls.NeuTreeView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtCheck = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.SpreadCheck = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.SpreadCheck_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuSplitter2 = new FS.FrameWork.WinForms.Controls.NeuSplitter();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.tvDoctor = new FS.FrameWork.WinForms.Controls.NeuTreeView();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtFind = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuContextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPopedom)).BeginInit();
            this.neuTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SpreadCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpreadCheck_Sheet1)).BeginInit();
            this.neuGroupBox1.SuspendLayout();
            this.neuPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuContextMenuStrip1
            // 
            this.neuContextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.item0});
            this.neuContextMenuStrip1.Name = "neuContextMenuStrip1";
            this.neuContextMenuStrip1.Size = new System.Drawing.Size(95, 26);
            this.neuContextMenuStrip1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuContextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.neuContextMenuStrip1_ItemClicked);
            // 
            // item0
            // 
            this.item0.Name = "item0";
            this.item0.Size = new System.Drawing.Size(94, 22);
            this.item0.Text = "查找";
            // 
            // DoctorImageList
            // 
            this.DoctorImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("DoctorImageList.ImageStream")));
            this.DoctorImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.DoctorImageList.Images.SetKeyName(0, "窗口.png");
            this.DoctorImageList.Images.SetKeyName(1, "窗口.png");
            this.DoctorImageList.Images.SetKeyName(2, "窗口.png");
            this.DoctorImageList.Images.SetKeyName(3, "窗口.png");
            this.DoctorImageList.Images.SetKeyName(4, "窗口.png");
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, ";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(167, 0);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpPopedom});
            this.neuSpread1.Size = new System.Drawing.Size(312, 402);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpPopedom
            // 
            this.fpPopedom.Reset();
            this.fpPopedom.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpPopedom.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpPopedom.ColumnCount = 9;
            this.fpPopedom.RowCount = 1;
            this.fpPopedom.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247))))), System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(217)))), ((int)(((byte)(217))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpPopedom.Cells.Get(0, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            comboBoxCellType1.ButtonAlign = FarPoint.Win.ButtonAlign.Right;
            comboBoxCellType1.Items = new string[] {
        "否",
        "是"};
            this.fpPopedom.Cells.Get(0, 4).CellType = comboBoxCellType1;
            this.fpPopedom.ColumnHeader.Cells.Get(0, 0).Value = "员工号";
            this.fpPopedom.ColumnHeader.Cells.Get(0, 1).Value = "医生姓名";
            this.fpPopedom.ColumnHeader.Cells.Get(0, 2).Value = "权限分类";
            this.fpPopedom.ColumnHeader.Cells.Get(0, 3).Value = "权限内容";
            this.fpPopedom.ColumnHeader.Cells.Get(0, 4).Value = "审核标志";
            this.fpPopedom.ColumnHeader.Cells.Get(0, 5).Value = "序号";
            this.fpPopedom.ColumnHeader.Cells.Get(0, 6).Value = "权限代码";
            this.fpPopedom.ColumnHeader.Cells.Get(0, 7).Value = "分类代码";
            this.fpPopedom.ColumnHeader.Cells.Get(0, 8).Value = "新增标志";
            this.fpPopedom.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.fpPopedom.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpPopedom.ColumnHeader.Rows.Get(0).Height = 25F;
            this.fpPopedom.Columns.Get(0).AllowAutoFilter = true;
            this.fpPopedom.Columns.Get(0).AllowAutoSort = true;
            this.fpPopedom.Columns.Get(0).CellType = textCellType1;
            this.fpPopedom.Columns.Get(0).Label = "员工号";
            this.fpPopedom.Columns.Get(0).Locked = true;
            this.fpPopedom.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPopedom.Columns.Get(0).Width = 95F;
            this.fpPopedom.Columns.Get(1).AllowAutoFilter = true;
            this.fpPopedom.Columns.Get(1).AllowAutoSort = true;
            this.fpPopedom.Columns.Get(1).CellType = textCellType2;
            this.fpPopedom.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpPopedom.Columns.Get(1).Label = "医生姓名";
            this.fpPopedom.Columns.Get(1).Locked = true;
            this.fpPopedom.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPopedom.Columns.Get(1).Width = 93F;
            this.fpPopedom.Columns.Get(2).AllowAutoFilter = true;
            this.fpPopedom.Columns.Get(2).AllowAutoSort = true;
            this.fpPopedom.Columns.Get(2).CellType = textCellType3;
            this.fpPopedom.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpPopedom.Columns.Get(2).Label = "权限分类";
            this.fpPopedom.Columns.Get(2).Locked = true;
            this.fpPopedom.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPopedom.Columns.Get(2).Width = 89F;
            this.fpPopedom.Columns.Get(3).AllowAutoFilter = true;
            this.fpPopedom.Columns.Get(3).AllowAutoSort = true;
            this.fpPopedom.Columns.Get(3).CellType = textCellType4;
            this.fpPopedom.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpPopedom.Columns.Get(3).Label = "权限内容";
            this.fpPopedom.Columns.Get(3).Locked = true;
            this.fpPopedom.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPopedom.Columns.Get(3).Width = 120F;
            this.fpPopedom.Columns.Get(4).AllowAutoFilter = true;
            this.fpPopedom.Columns.Get(4).AllowAutoSort = true;
            comboBoxCellType2.ButtonAlign = FarPoint.Win.ButtonAlign.Right;
            comboBoxCellType2.Items = new string[] {
        "否",
        "是"};
            this.fpPopedom.Columns.Get(4).CellType = comboBoxCellType2;
            this.fpPopedom.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpPopedom.Columns.Get(4).Label = "审核标志";
            this.fpPopedom.Columns.Get(4).Locked = true;
            this.fpPopedom.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPopedom.Columns.Get(4).Width = 91F;
            this.fpPopedom.Columns.Get(5).Label = "序号";
            this.fpPopedom.Columns.Get(5).Visible = false;
            this.fpPopedom.Columns.Get(6).Label = "权限代码";
            this.fpPopedom.Columns.Get(6).Visible = false;
            this.fpPopedom.Columns.Get(7).Label = "分类代码";
            this.fpPopedom.Columns.Get(7).Visible = false;
            this.fpPopedom.Columns.Get(8).Label = "新增标志";
            this.fpPopedom.Columns.Get(8).Visible = false;
            this.fpPopedom.OperationMode = FarPoint.Win.Spread.OperationMode.ExtendedSelect;
            this.fpPopedom.RowHeader.Columns.Default.Resizable = true;
            this.fpPopedom.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.fpPopedom.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpPopedom.Rows.Default.Height = 25F;
            this.fpPopedom.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.MultiRange;
            this.fpPopedom.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpPopedom.SheetCornerStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.fpPopedom.SheetCornerStyle.Parent = "CornerDefault";
            this.fpPopedom.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpPopedom.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuSplitter1
            // 
            this.neuSplitter1.Location = new System.Drawing.Point(0, 0);
            this.neuSplitter1.Name = "neuSplitter1";
            this.neuSplitter1.Size = new System.Drawing.Size(5, 402);
            this.neuSplitter1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSplitter1.TabIndex = 3;
            this.neuSplitter1.TabStop = false;
            // 
            // neuTabControl1
            // 
            this.neuTabControl1.Controls.Add(this.tabPage1);
            this.neuTabControl1.Controls.Add(this.tabPage2);
            this.neuTabControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.neuTabControl1.Location = new System.Drawing.Point(482, 0);
            this.neuTabControl1.Name = "neuTabControl1";
            this.neuTabControl1.SelectedIndex = 0;
            this.neuTabControl1.Size = new System.Drawing.Size(264, 402);
            this.neuTabControl1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tvPopedom);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(256, 376);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "权限类别1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tvPopedom
            // 
            this.tvPopedom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvPopedom.HideSelection = false;
            this.tvPopedom.ImageIndex = 0;
            this.tvPopedom.ImageList = this.DoctorImageList;
            this.tvPopedom.Location = new System.Drawing.Point(3, 3);
            this.tvPopedom.Name = "tvPopedom";
            treeNode1.ImageIndex = 3;
            treeNode1.Name = "节点4";
            treeNode1.Text = "手术权限";
            treeNode2.ImageIndex = 3;
            treeNode2.Name = "节点5";
            treeNode2.Tag = "";
            treeNode2.Text = "处方权限";
            treeNode2.ToolTipText = "RECIPE";
            treeNode3.ImageIndex = 3;
            treeNode3.Name = "节点6";
            treeNode3.Text = "组套权限";
            treeNode4.ImageIndex = 3;
            treeNode4.Name = "节点7";
            treeNode4.Text = "会诊权限";
            treeNode4.ToolTipText = "权限";
            treeNode5.ImageIndex = 3;
            treeNode5.Name = "节点8";
            treeNode5.Text = "大型仪器";
            treeNode6.Name = "节点0";
            treeNode6.Text = "权限列表";
            this.tvPopedom.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode6});
            this.tvPopedom.SelectedImageIndex = 0;
            this.tvPopedom.Size = new System.Drawing.Size(250, 370);
            this.tvPopedom.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tvPopedom.TabIndex = 2;
            this.tvPopedom.DoubleClick += new System.EventHandler(this.tvPopedom_DoubleClick);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(256, 376);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "权限类别2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.neuLabel1);
            this.splitContainer1.Panel1.Controls.Add(this.txtCheck);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.SpreadCheck);
            this.splitContainer1.Size = new System.Drawing.Size(250, 370);
            this.splitContainer1.SplitterDistance = 29;
            this.splitContainer1.TabIndex = 0;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(7, 12);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(47, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 1;
            this.neuLabel1.Text = "过滤框:";
            // 
            // txtCheck
            // 
            this.txtCheck.IsEnter2Tab = false;
            this.txtCheck.Location = new System.Drawing.Point(60, 7);
            this.txtCheck.Name = "txtCheck";
            this.txtCheck.Size = new System.Drawing.Size(100, 21);
            this.txtCheck.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCheck.TabIndex = 0;
            this.txtCheck.TextChanged += new System.EventHandler(this.txtCheck_TextChanged);
            // 
            // SpreadCheck
            // 
            this.SpreadCheck.About = "3.0.2004.2005";
            this.SpreadCheck.AccessibleDescription = "";
            this.SpreadCheck.BackColor = System.Drawing.Color.White;
            this.SpreadCheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SpreadCheck.FileName = "";
            this.SpreadCheck.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.SpreadCheck.IsAutoSaveGridStatus = false;
            this.SpreadCheck.IsCanCustomConfigColumn = false;
            this.SpreadCheck.Location = new System.Drawing.Point(0, 0);
            this.SpreadCheck.Name = "SpreadCheck";
            this.SpreadCheck.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.SpreadCheck_Sheet1});
            this.SpreadCheck.Size = new System.Drawing.Size(250, 337);
            this.SpreadCheck.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.SpreadCheck.TabIndex = 0;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.SpreadCheck.TextTipAppearance = tipAppearance2;
            this.SpreadCheck.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.SpreadCheck.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.neuSpreadCheck_CellDoubleClick);
            // 
            // SpreadCheck_Sheet1
            // 
            this.SpreadCheck_Sheet1.Reset();
            this.SpreadCheck_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.SpreadCheck_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.SpreadCheck_Sheet1.GrayAreaBackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.SpreadCheck_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.SpreadCheck_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuSplitter2
            // 
            this.neuSplitter2.Dock = System.Windows.Forms.DockStyle.Right;
            this.neuSplitter2.Location = new System.Drawing.Point(479, 0);
            this.neuSplitter2.Name = "neuSplitter2";
            this.neuSplitter2.Size = new System.Drawing.Size(3, 402);
            this.neuSplitter2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSplitter2.TabIndex = 5;
            this.neuSplitter2.TabStop = false;
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.tvDoctor);
            this.neuGroupBox1.Controls.Add(this.neuPanel1);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.neuGroupBox1.Location = new System.Drawing.Point(5, 0);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(162, 402);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 6;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "列表";
            // 
            // tvDoctor
            // 
            this.tvDoctor.ContextMenuStrip = this.neuContextMenuStrip1;
            this.tvDoctor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvDoctor.HideSelection = false;
            this.tvDoctor.ImageIndex = 2;
            this.tvDoctor.ImageList = this.DoctorImageList;
            this.tvDoctor.Location = new System.Drawing.Point(3, 49);
            this.tvDoctor.Name = "tvDoctor";
            treeNode7.Name = "节点0";
            treeNode7.Text = "医生列表";
            this.tvDoctor.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode7});
            this.tvDoctor.SelectedImageIndex = 0;
            this.tvDoctor.Size = new System.Drawing.Size(156, 350);
            this.tvDoctor.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tvDoctor.TabIndex = 0;
            this.tvDoctor.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvDoctor_AfterSelect);
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.neuLabel2);
            this.neuPanel1.Controls.Add(this.txtFind);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(3, 17);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(156, 32);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 7;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(3, 9);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(41, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 7;
            this.neuLabel2.Text = "查找：";
            // 
            // txtFind
            // 
            this.txtFind.IsEnter2Tab = false;
            this.txtFind.Location = new System.Drawing.Point(47, 5);
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(100, 21);
            this.txtFind.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtFind.TabIndex = 7;
            this.txtFind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFind_KeyDown);
            // 
            // ucPopedomManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuSpread1);
            this.Controls.Add(this.neuGroupBox1);
            this.Controls.Add(this.neuSplitter2);
            this.Controls.Add(this.neuTabControl1);
            this.Controls.Add(this.neuSplitter1);
            this.Name = "ucPopedomManagement";
            this.Size = new System.Drawing.Size(746, 402);
            this.Load += new System.EventHandler(this.ucPopedomManagement_Load);
            this.neuContextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPopedom)).EndInit();
            this.neuTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SpreadCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpreadCheck_Sheet1)).EndInit();
            this.neuGroupBox1.ResumeLayout(false);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList DoctorImageList;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView fpPopedom;
        private FS.FrameWork.WinForms.Controls.NeuContextMenuStrip neuContextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem item0;
        private FS.FrameWork.WinForms.Controls.NeuSplitter neuSplitter1;
        private FS.FrameWork.WinForms.Controls.NeuTabControl neuTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private FS.FrameWork.WinForms.Controls.NeuTreeView tvPopedom;
        private System.Windows.Forms.TabPage tabPage2;
        private FS.FrameWork.WinForms.Controls.NeuSplitter neuSplitter2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtCheck;
        private FS.FrameWork.WinForms.Controls.NeuSpread SpreadCheck;
        private FarPoint.Win.Spread.SheetView SpreadCheck_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuTreeView tvDoctor;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtFind;
    }
}
