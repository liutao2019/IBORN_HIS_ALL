namespace FS.HISFC.Components.Order.OutPatient.Controls
{
    partial class ucOrderHistory
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("历史");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucOrderHistory));
            FarPoint.Win.Spread.TipAppearance tipAppearance5 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.TipAppearance tipAppearance6 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.treeView1 = new FS.FrameWork.WinForms.Controls.NeuTreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.neuSplitter1 = new FS.FrameWork.WinForms.Controls.NeuSplitter();
            this.fpOrder = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpOrder_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.tcOrder = new System.Windows.Forms.TabControl();
            this.tpOrder = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.tpFee = new System.Windows.Forms.TabPage();
            this.fpFee = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpFee_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btPrint = new System.Windows.Forms.Button();
            this.btQuery = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lblPatientInfo = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCardNo = new System.Windows.Forms.TextBox();
            this.dtpBeginTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.dtpEndTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.fpOrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpOrder_Sheet1)).BeginInit();
            this.tcOrder.SuspendLayout();
            this.tpOrder.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tpFee.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpFee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpFee_Sheet1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView1.HideSelection = false;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "节点0";
            treeNode1.Text = "历史";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(199, 362);
            this.treeView1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.treeView1.TabIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "page.bmp");
            // 
            // neuSplitter1
            // 
            this.neuSplitter1.Location = new System.Drawing.Point(0, 0);
            this.neuSplitter1.Name = "neuSplitter1";
            this.neuSplitter1.Size = new System.Drawing.Size(3, 426);
            this.neuSplitter1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSplitter1.TabIndex = 1;
            this.neuSplitter1.TabStop = false;
            // 
            // fpOrder
            // 
            this.fpOrder.About = "3.0.2004.2005";
            this.fpOrder.AccessibleDescription = "fpOrder, Sheet1";
            this.fpOrder.BackColor = System.Drawing.Color.White;
            this.fpOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpOrder.FileName = "";
            this.fpOrder.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpOrder.IsAutoSaveGridStatus = false;
            this.fpOrder.IsCanCustomConfigColumn = false;
            this.fpOrder.Location = new System.Drawing.Point(3, 30);
            this.fpOrder.Name = "fpOrder";
            this.fpOrder.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpOrder.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpOrder_Sheet1});
            this.fpOrder.Size = new System.Drawing.Size(789, 303);
            this.fpOrder.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpOrder.TabIndex = 2;
            tipAppearance5.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance5.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpOrder.TextTipAppearance = tipAppearance5;
            this.fpOrder.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpOrder_Sheet1
            // 
            this.fpOrder_Sheet1.Reset();
            this.fpOrder_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpOrder_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpOrder_Sheet1.RowCount = 0;
            this.fpOrder_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpOrder_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpOrder_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpOrder_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.fpOrder_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.MultiSelect;
            this.fpOrder_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpOrder_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.fpOrder_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpOrder_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpOrder_Sheet1.Rows.Default.Height = 25F;
            this.fpOrder_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.MultiRange;
            this.fpOrder_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpOrder_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpOrder_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.fpOrder_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpOrder_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpOrder.SetActiveViewport(0, 1, 0);
            // 
            // tcOrder
            // 
            this.tcOrder.Controls.Add(this.tpOrder);
            this.tcOrder.Controls.Add(this.tpFee);
            this.tcOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcOrder.Location = new System.Drawing.Point(199, 0);
            this.tcOrder.Name = "tcOrder";
            this.tcOrder.SelectedIndex = 0;
            this.tcOrder.Size = new System.Drawing.Size(803, 362);
            this.tcOrder.TabIndex = 3;
            // 
            // tpOrder
            // 
            this.tpOrder.BackColor = System.Drawing.Color.White;
            this.tpOrder.Controls.Add(this.fpOrder);
            this.tpOrder.Controls.Add(this.panel4);
            this.tpOrder.Location = new System.Drawing.Point(4, 22);
            this.tpOrder.Name = "tpOrder";
            this.tpOrder.Padding = new System.Windows.Forms.Padding(3);
            this.tpOrder.Size = new System.Drawing.Size(795, 336);
            this.tpOrder.TabIndex = 0;
            this.tpOrder.Text = " 电子方";
            this.tpOrder.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(789, 27);
            this.panel4.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(6, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(263, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "电子处方的复制不包含附材信息！";
            // 
            // tpFee
            // 
            this.tpFee.BackColor = System.Drawing.Color.White;
            this.tpFee.Controls.Add(this.fpFee);
            this.tpFee.Controls.Add(this.panel1);
            this.tpFee.Location = new System.Drawing.Point(4, 22);
            this.tpFee.Name = "tpFee";
            this.tpFee.Padding = new System.Windows.Forms.Padding(3);
            this.tpFee.Size = new System.Drawing.Size(795, 336);
            this.tpFee.TabIndex = 1;
            this.tpFee.Text = "手工方";
            this.tpFee.UseVisualStyleBackColor = true;
            // 
            // fpFee
            // 
            this.fpFee.About = "3.0.2004.2005";
            this.fpFee.AccessibleDescription = "fpFee, Sheet1, Row 0, Column 0, ";
            this.fpFee.BackColor = System.Drawing.Color.White;
            this.fpFee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpFee.FileName = "";
            this.fpFee.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpFee.IsAutoSaveGridStatus = false;
            this.fpFee.IsCanCustomConfigColumn = false;
            this.fpFee.Location = new System.Drawing.Point(3, 30);
            this.fpFee.Name = "fpFee";
            this.fpFee.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpFee.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpFee_Sheet1});
            this.fpFee.Size = new System.Drawing.Size(789, 303);
            this.fpFee.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpFee.TabIndex = 3;
            tipAppearance6.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance6.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpFee.TextTipAppearance = tipAppearance6;
            this.fpFee.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpFee_Sheet1
            // 
            this.fpFee_Sheet1.Reset();
            this.fpFee_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpFee_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpFee_Sheet1.ColumnCount = 18;
            this.fpFee_Sheet1.RowCount = 0;
            this.fpFee_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpFee_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "项目编码";
            this.fpFee_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "编码";
            this.fpFee_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "项目";
            this.fpFee_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = ".";
            this.fpFee_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "单价";
            this.fpFee_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "数量";
            this.fpFee_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "单位";
            this.fpFee_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "付数/天数";
            this.fpFee_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "用量";
            this.fpFee_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "单位";
            this.fpFee_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "频次编码";
            this.fpFee_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "频次";
            this.fpFee_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "用法编码";
            this.fpFee_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "用法";
            this.fpFee_Sheet1.ColumnHeader.Cells.Get(0, 14).Value = "执行科室编码";
            this.fpFee_Sheet1.ColumnHeader.Cells.Get(0, 15).Value = "执行科室";
            this.fpFee_Sheet1.ColumnHeader.Cells.Get(0, 16).Value = "金额";
            this.fpFee_Sheet1.ColumnHeader.Cells.Get(0, 17).Value = "组合号";
            this.fpFee_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpFee_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpFee_Sheet1.ColumnHeader.Rows.Get(0).Height = 40F;
            this.fpFee_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpFee_Sheet1.Columns.Get(1).Label = "编码";
            this.fpFee_Sheet1.Columns.Get(1).Width = 67F;
            textCellType3.WordWrap = true;
            this.fpFee_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.fpFee_Sheet1.Columns.Get(2).Label = "项目";
            this.fpFee_Sheet1.Columns.Get(2).Width = 211F;
            this.fpFee_Sheet1.Columns.Get(3).Label = ".";
            this.fpFee_Sheet1.Columns.Get(3).Width = 15F;
            this.fpFee_Sheet1.Columns.Get(4).Label = "单价";
            this.fpFee_Sheet1.Columns.Get(4).Width = 63F;
            this.fpFee_Sheet1.Columns.Get(5).Label = "数量";
            this.fpFee_Sheet1.Columns.Get(5).Width = 38F;
            this.fpFee_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpFee_Sheet1.Columns.Get(6).Label = "单位";
            this.fpFee_Sheet1.Columns.Get(6).Width = 39F;
            this.fpFee_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpFee_Sheet1.Columns.Get(7).Label = "付数/天数";
            this.fpFee_Sheet1.Columns.Get(7).Width = 45F;
            this.fpFee_Sheet1.Columns.Get(8).Label = "用量";
            this.fpFee_Sheet1.Columns.Get(8).Width = 45F;
            this.fpFee_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpFee_Sheet1.Columns.Get(9).Label = "单位";
            this.fpFee_Sheet1.Columns.Get(9).Width = 39F;
            this.fpFee_Sheet1.Columns.Get(11).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpFee_Sheet1.Columns.Get(11).Label = "频次";
            this.fpFee_Sheet1.Columns.Get(11).Width = 51F;
            this.fpFee_Sheet1.Columns.Get(13).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpFee_Sheet1.Columns.Get(13).Label = "用法";
            this.fpFee_Sheet1.Columns.Get(13).Width = 44F;
            this.fpFee_Sheet1.Columns.Get(15).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpFee_Sheet1.Columns.Get(15).Label = "执行科室";
            this.fpFee_Sheet1.Columns.Get(15).Width = 85F;
            this.fpFee_Sheet1.Columns.Get(16).Label = "金额";
            this.fpFee_Sheet1.Columns.Get(16).Width = 62F;
            this.fpFee_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.MultiSelect;
            this.fpFee_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpFee_Sheet1.RowHeader.Columns.Get(0).Width = 32F;
            this.fpFee_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpFee_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpFee_Sheet1.Rows.Default.Height = 25F;
            this.fpFee_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.MultiRange;
            this.fpFee_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpFee_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpFee_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.fpFee_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpFee_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpFee.SetViewportLeftColumn(0, 0, 4);
            this.fpFee.SetActiveViewport(0, 1, 0);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(789, 27);
            this.panel1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(6, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "手工方不支持复制功能！";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.splitter1);
            this.panel2.Controls.Add(this.tcOrder);
            this.panel2.Controls.Add(this.treeView1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 64);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1002, 362);
            this.panel2.TabIndex = 4;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(199, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(8, 362);
            this.splitter1.TabIndex = 4;
            this.splitter1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1002, 64);
            this.panel3.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.btPrint);
            this.groupBox1.Controls.Add(this.btQuery);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.lblPatientInfo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtCardNo);
            this.groupBox1.Controls.Add(this.dtpBeginTime);
            this.groupBox1.Controls.Add(this.dtpEndTime);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1002, 64);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(191, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 18;
            this.label6.Text = "姓名：";
            // 
            // txtName
            // 
            this.txtName.ForeColor = System.Drawing.Color.Black;
            this.txtName.Location = new System.Drawing.Point(232, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(128, 21);
            this.txtName.TabIndex = 17;
            this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
            // 
            // btPrint
            // 
            this.btPrint.Location = new System.Drawing.Point(851, 36);
            this.btPrint.Name = "btPrint";
            this.btPrint.Size = new System.Drawing.Size(75, 25);
            this.btPrint.TabIndex = 16;
            this.btPrint.Text = "补打单据";
            this.btPrint.UseVisualStyleBackColor = true;
            this.btPrint.Click += new System.EventHandler(this.btPrint_Click);
            // 
            // btQuery
            // 
            this.btQuery.Location = new System.Drawing.Point(851, 12);
            this.btQuery.Name = "btQuery";
            this.btQuery.Size = new System.Drawing.Size(75, 23);
            this.btQuery.TabIndex = 8;
            this.btQuery.Text = "查  询";
            this.btQuery.UseVisualStyleBackColor = true;
            this.btQuery.Click += new System.EventHandler(this.btQuery_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(483, 36);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 25);
            this.button1.TabIndex = 7;
            this.button1.Text = "复制所有处方";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblPatientInfo
            // 
            this.lblPatientInfo.AutoSize = true;
            this.lblPatientInfo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPatientInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblPatientInfo.Location = new System.Drawing.Point(6, 43);
            this.lblPatientInfo.Name = "lblPatientInfo";
            this.lblPatientInfo.Size = new System.Drawing.Size(82, 14);
            this.lblPatientInfo.TabIndex = 6;
            this.lblPatientInfo.Text = "患者信息：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "卡号：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(622, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "截止时间：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(379, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "开始时间：";
            // 
            // txtCardNo
            // 
            this.txtCardNo.ForeColor = System.Drawing.Color.Black;
            this.txtCardNo.Location = new System.Drawing.Point(48, 12);
            this.txtCardNo.Name = "txtCardNo";
            this.txtCardNo.Size = new System.Drawing.Size(128, 21);
            this.txtCardNo.TabIndex = 3;
            this.txtCardNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCardNo_KeyDown);
            // 
            // dtpBeginTime
            // 
            this.dtpBeginTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpBeginTime.IsEnter2Tab = false;
            this.dtpBeginTime.Location = new System.Drawing.Point(445, 12);
            this.dtpBeginTime.Name = "dtpBeginTime";
            this.dtpBeginTime.Size = new System.Drawing.Size(145, 21);
            this.dtpBeginTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpBeginTime.TabIndex = 1;
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpEndTime.IsEnter2Tab = false;
            this.dtpEndTime.Location = new System.Drawing.Point(687, 12);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.Size = new System.Drawing.Size(145, 21);
            this.dtpEndTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpEndTime.TabIndex = 0;
            // 
            // ucOrderHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.neuSplitter1);
            this.Name = "ucOrderHistory";
            this.Size = new System.Drawing.Size(1005, 426);
            this.Load += new System.EventHandler(this.ucOrderHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fpOrder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpOrder_Sheet1)).EndInit();
            this.tcOrder.ResumeLayout(false);
            this.tpOrder.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.tpFee.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpFee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpFee_Sheet1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        protected FS.FrameWork.WinForms.Controls.NeuTreeView treeView1;
        protected FS.FrameWork.WinForms.Controls.NeuSplitter neuSplitter1;
        protected FS.FrameWork.WinForms.Controls.NeuSpread fpSpread1;
        protected FarPoint.Win.Spread.SheetView fpOrder_Sheet1;
        protected System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TabControl tcOrder;
        private System.Windows.Forms.TabPage tpOrder;
        private System.Windows.Forms.TabPage tpFee;
        protected FS.FrameWork.WinForms.Controls.NeuSpread fpOrder;
        protected FS.FrameWork.WinForms.Controls.NeuSpread fpFee;
        protected FarPoint.Win.Spread.SheetView fpFee_Sheet1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpEndTime;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpBeginTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCardNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblPatientInfo;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Button btQuery;
        private System.Windows.Forms.Button btPrint;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label6;
    }
}
