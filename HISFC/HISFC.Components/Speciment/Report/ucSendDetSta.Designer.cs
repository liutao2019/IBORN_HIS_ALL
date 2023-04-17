namespace FS.HISFC.Components.Speciment.Report
{
    partial class ucSendDetSta
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
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rbtBld = new System.Windows.Forms.RadioButton();
            this.rbtOrg = new System.Windows.Forms.RadioButton();
            this.rbtDoc = new System.Windows.Forms.RadioButton();
            this.rbtDept = new System.Windows.Forms.RadioButton();
            this.cmbDoc = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lblDoc = new System.Windows.Forms.Label();
            this.cmbDept = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lblDept = new System.Windows.Forms.Label();
            this.dtpEndTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpStartDate = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuSpread1_Sheet2 = new FarPoint.Win.Spread.SheetView();
            this.neuSpread1_Sheet3 = new FarPoint.Win.Spread.SheetView();
            this.neuSpread1_Sheet4 = new FarPoint.Win.Spread.SheetView();
            this.neuSpread1_sheetView2 = new FarPoint.Win.Spread.SheetView();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_sheetView2)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.rbtBld);
            this.splitContainer1.Panel1.Controls.Add(this.rbtOrg);
            this.splitContainer1.Panel1.Controls.Add(this.rbtDoc);
            this.splitContainer1.Panel1.Controls.Add(this.rbtDept);
            this.splitContainer1.Panel1.Controls.Add(this.cmbDoc);
            this.splitContainer1.Panel1.Controls.Add(this.lblDoc);
            this.splitContainer1.Panel1.Controls.Add(this.cmbDept);
            this.splitContainer1.Panel1.Controls.Add(this.lblDept);
            this.splitContainer1.Panel1.Controls.Add(this.dtpEndTime);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.dtpStartDate);
            this.splitContainer1.Panel1.Controls.Add(this.label11);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.neuSpread1);
            this.splitContainer1.Size = new System.Drawing.Size(839, 772);
            this.splitContainer1.SplitterDistance = 91;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(401, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(392, 16);
            this.label6.TabIndex = 96;
            this.label6.Text = "4、送存率2 = 实送数量/(应送数量+主动送存)*100%；";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(66, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(312, 16);
            this.label5.TabIndex = 95;
            this.label5.Text = "3、送 存 率 = 必须送取/应送数量*100%；";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(401, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(288, 16);
            this.label4.TabIndex = 94;
            this.label4.Text = "2、主动送取 = 实送数量 - 必须送取；";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(66, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(288, 16);
            this.label3.TabIndex = 93;
            this.label3.Text = "1、未送数量 = 应送数量 - 实收数量；";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 92;
            this.label2.Text = "备注：";
            // 
            // rbtBld
            // 
            this.rbtBld.AutoSize = true;
            this.rbtBld.Checked = true;
            this.rbtBld.Location = new System.Drawing.Point(404, 14);
            this.rbtBld.Name = "rbtBld";
            this.rbtBld.Size = new System.Drawing.Size(42, 20);
            this.rbtBld.TabIndex = 91;
            this.rbtBld.TabStop = true;
            this.rbtBld.Text = "血";
            this.rbtBld.UseVisualStyleBackColor = true;
            // 
            // rbtOrg
            // 
            this.rbtOrg.AutoSize = true;
            this.rbtOrg.Location = new System.Drawing.Point(463, 14);
            this.rbtOrg.Name = "rbtOrg";
            this.rbtOrg.Size = new System.Drawing.Size(58, 20);
            this.rbtOrg.TabIndex = 90;
            this.rbtOrg.Text = "组织";
            this.rbtOrg.UseVisualStyleBackColor = true;
            // 
            // rbtDoc
            // 
            this.rbtDoc.AutoSize = true;
            this.rbtDoc.Location = new System.Drawing.Point(778, 68);
            this.rbtDoc.Name = "rbtDoc";
            this.rbtDoc.Size = new System.Drawing.Size(58, 20);
            this.rbtDoc.TabIndex = 89;
            this.rbtDoc.Text = "医生";
            this.rbtDoc.UseVisualStyleBackColor = true;
            this.rbtDoc.Visible = false;
            // 
            // rbtDept
            // 
            this.rbtDept.AutoSize = true;
            this.rbtDept.Location = new System.Drawing.Point(714, 68);
            this.rbtDept.Name = "rbtDept";
            this.rbtDept.Size = new System.Drawing.Size(58, 20);
            this.rbtDept.TabIndex = 88;
            this.rbtDept.Text = "科室";
            this.rbtDept.UseVisualStyleBackColor = true;
            this.rbtDept.Visible = false;
            // 
            // cmbDoc
            // 
            //this.cmbDoc.A = false;
            this.cmbDoc.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbDoc.FormattingEnabled = true;
            this.cmbDoc.IsFlat = true;
            this.cmbDoc.IsLike = true;
            this.cmbDoc.Location = new System.Drawing.Point(825, 13);
            this.cmbDoc.Name = "cmbDoc";
            this.cmbDoc.PopForm = null;
            this.cmbDoc.ShowCustomerList = false;
            this.cmbDoc.ShowID = false;
            this.cmbDoc.Size = new System.Drawing.Size(135, 24);
            this.cmbDoc.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbDoc.TabIndex = 87;
            this.cmbDoc.Tag = "";
            this.cmbDoc.ToolBarUse = false;
            // 
            // lblDoc
            // 
            this.lblDoc.AutoSize = true;
            this.lblDoc.Location = new System.Drawing.Point(748, 16);
            this.lblDoc.Name = "lblDoc";
            this.lblDoc.Size = new System.Drawing.Size(48, 16);
            this.lblDoc.TabIndex = 86;
            this.lblDoc.Text = "医生:";
            // 
            // cmbDept
            // 
            //this.cmbDept.A = false;
            this.cmbDept.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbDept.FormattingEnabled = true;
            this.cmbDept.IsFlat = true;
            this.cmbDept.IsLike = true;
            this.cmbDept.Location = new System.Drawing.Point(581, 13);
            this.cmbDept.Name = "cmbDept";
            this.cmbDept.PopForm = null;
            this.cmbDept.ShowCustomerList = false;
            this.cmbDept.ShowID = false;
            this.cmbDept.Size = new System.Drawing.Size(144, 24);
            this.cmbDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbDept.TabIndex = 85;
            this.cmbDept.Tag = "";
            this.cmbDept.ToolBarUse = false;
            this.cmbDept.SelectedIndexChanged += new System.EventHandler(this.cmbDept_SelectedIndexChanged);
            // 
            // lblDept
            // 
            this.lblDept.AutoSize = true;
            this.lblDept.Location = new System.Drawing.Point(527, 16);
            this.lblDept.Name = "lblDept";
            this.lblDept.Size = new System.Drawing.Size(48, 16);
            this.lblDept.TabIndex = 84;
            this.lblDept.Text = "科室:";
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.Location = new System.Drawing.Point(263, 11);
            this.dtpEndTime.Margin = new System.Windows.Forms.Padding(4);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.Size = new System.Drawing.Size(134, 26);
            this.dtpEndTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpEndTime.TabIndex = 83;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 16);
            this.label1.TabIndex = 82;
            this.label1.Text = "统计时间段:";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(108, 11);
            this.dtpStartDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(137, 26);
            this.dtpStartDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpStartDate.TabIndex = 81;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(248, 16);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(16, 16);
            this.label11.TabIndex = 80;
            this.label11.Text = "-";
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "2.5.2007.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, 科室送存统计, Row 0, Column 0, ";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(0, 0);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1,
            this.neuSpread1_Sheet2,
            this.neuSpread1_Sheet3,
            this.neuSpread1_Sheet4,
            this.neuSpread1_sheetView2});
            this.neuSpread1.Size = new System.Drawing.Size(839, 676);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 7;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.ActiveSheetIndex = 4;
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "送标本详情";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ColumnHeader.RowCount = 2;
            this.neuSpread1_Sheet1.ActiveColumnIndex = 3;
            this.neuSpread1_Sheet1.ActiveRowIndex = 33;
            this.neuSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.SystemColors.Control, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.ColumnHeader.Rows.Get(0).Height = 40F;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.SheetCornerStyle.Locked = false;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuSpread1_Sheet2
            // 
            this.neuSpread1_Sheet2.Reset();
            this.neuSpread1_Sheet2.SheetName = "病区送存统计";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet2.ColumnHeader.RowCount = 2;
            this.neuSpread1_Sheet2.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.SystemColors.Control, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.neuSpread1_Sheet2.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.ColumnHeader.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet2.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet2.ColumnHeader.Rows.Get(0).Height = 50F;
            this.neuSpread1_Sheet2.ColumnHeader.Rows.Get(1).Height = 50F;
            this.neuSpread1_Sheet2.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet2.RowHeader.Columns.Get(0).Width = 37F;
            this.neuSpread1_Sheet2.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.RowHeader.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet2.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet2.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.SheetCornerStyle.Locked = false;
            this.neuSpread1_Sheet2.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuSpread1_Sheet3
            // 
            this.neuSpread1_Sheet3.Reset();
            this.neuSpread1_Sheet3.SheetName = "医生送存统计";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet3.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet3.ColumnHeader.RowCount = 2;
            this.neuSpread1_Sheet3.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.SystemColors.Control, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Black, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.neuSpread1_Sheet3.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet3.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet3.ColumnHeader.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet3.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet3.ColumnHeader.Rows.Get(0).Height = 40F;
            this.neuSpread1_Sheet3.ColumnHeader.Rows.Get(1).Height = 40F;
            this.neuSpread1_Sheet3.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet3.RowHeader.Columns.Get(0).Width = 37F;
            this.neuSpread1_Sheet3.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet3.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet3.RowHeader.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet3.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet3.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet3.SheetCornerStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet3.SheetCornerStyle.Locked = false;
            this.neuSpread1_Sheet3.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet3.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuSpread1_Sheet4
            // 
            this.neuSpread1_Sheet4.Reset();
            this.neuSpread1_Sheet4.SheetName = "按科室病区统计";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet4.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet4.ColumnHeader.RowCount = 2;
            this.neuSpread1_Sheet4.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.SystemColors.Control, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Black, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.neuSpread1_Sheet4.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet4.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet4.ColumnHeader.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet4.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet4.ColumnHeader.Rows.Get(0).Height = 40F;
            this.neuSpread1_Sheet4.ColumnHeader.Rows.Get(1).Height = 40F;
            this.neuSpread1_Sheet4.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet4.RowHeader.Columns.Get(0).Width = 37F;
            this.neuSpread1_Sheet4.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet4.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet4.RowHeader.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet4.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet4.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet4.SheetCornerStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet4.SheetCornerStyle.Locked = false;
            this.neuSpread1_Sheet4.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet4.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuSpread1_sheetView2
            // 
            this.neuSpread1_sheetView2.Reset();
            this.neuSpread1_sheetView2.SheetName = "科室送存统计";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_sheetView2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_sheetView2.ColumnHeader.RowCount = 2;
            this.neuSpread1_sheetView2.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.SystemColors.Control, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.neuSpread1_sheetView2.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_sheetView2.ColumnHeader.DefaultStyle.Locked = false;
            this.neuSpread1_sheetView2.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_sheetView2.ColumnHeader.Rows.Get(0).Height = 50F;
            this.neuSpread1_sheetView2.ColumnHeader.Rows.Get(1).Height = 50F;
            this.neuSpread1_sheetView2.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_sheetView2.RowHeader.Columns.Get(0).Width = 37F;
            this.neuSpread1_sheetView2.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_sheetView2.RowHeader.DefaultStyle.Locked = false;
            this.neuSpread1_sheetView2.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_sheetView2.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_sheetView2.SheetCornerStyle.Locked = false;
            this.neuSpread1_sheetView2.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuSpread1_sheetView2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ucSendDetSta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ucSendDetSta";
            this.Size = new System.Drawing.Size(839, 772);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_sheetView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDoc;
        private System.Windows.Forms.Label lblDoc;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDept;
        private System.Windows.Forms.Label lblDept;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpEndTime;
        private System.Windows.Forms.Label label1;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label11;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private System.Windows.Forms.RadioButton rbtDoc;
        private System.Windows.Forms.RadioButton rbtDept;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet2;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet3;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet4;
        private FarPoint.Win.Spread.SheetView neuSpread1_sheetView2;
        private System.Windows.Forms.RadioButton rbtBld;
        private System.Windows.Forms.RadioButton rbtOrg;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
    }
}
