namespace FS.SOC.HISFC.Components.Project
{
    partial class ucTransTraker
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
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.nlbAssignMission = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbLogionInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nllPreferredHeight = new FS.FrameWork.WinForms.Controls.NeuLinkLabel();
            this.nckAutoFitMission = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.neuGroupBox2 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuLinkLabel1 = new FS.FrameWork.WinForms.Controls.NeuLinkLabel();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuDateTimePicker1 = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.ncmbOper = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.ncmbState = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.ncmbModeName = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuGroupBox3 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.fpSpread1 = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.签出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.撤销签出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.签入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重置序号ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.missionCompletedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.fpSpread1_Sheet2 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel1.SuspendLayout();
            this.neuGroupBox1.SuspendLayout();
            this.neuGroupBox2.SuspendLayout();
            this.neuGroupBox3.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet2)).BeginInit();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.neuGroupBox1);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuPanel1.Location = new System.Drawing.Point(0, 511);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(981, 44);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 1;
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.nlbAssignMission);
            this.neuGroupBox1.Controls.Add(this.nlbLogionInfo);
            this.neuGroupBox1.Controls.Add(this.nllPreferredHeight);
            this.neuGroupBox1.Controls.Add(this.nckAutoFitMission);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(981, 44);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 0;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "附加信息";
            // 
            // nlbAssignMission
            // 
            this.nlbAssignMission.AutoSize = true;
            this.nlbAssignMission.ForeColor = System.Drawing.Color.Blue;
            this.nlbAssignMission.Location = new System.Drawing.Point(647, 21);
            this.nlbAssignMission.Name = "nlbAssignMission";
            this.nlbAssignMission.Size = new System.Drawing.Size(239, 12);
            this.nlbAssignMission.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbAssignMission.TabIndex = 5;
            this.nlbAssignMission.Text = "双击这里【制定任务计划】（角色PM，PSM）";
            // 
            // nlbLogionInfo
            // 
            this.nlbLogionInfo.AutoSize = true;
            this.nlbLogionInfo.ForeColor = System.Drawing.Color.Blue;
            this.nlbLogionInfo.Location = new System.Drawing.Point(289, 22);
            this.nlbLogionInfo.Name = "nlbLogionInfo";
            this.nlbLogionInfo.Size = new System.Drawing.Size(65, 12);
            this.nlbLogionInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbLogionInfo.TabIndex = 4;
            this.nlbLogionInfo.Text = "系统未登录";
            // 
            // nllPreferredHeight
            // 
            this.nllPreferredHeight.AutoSize = true;
            this.nllPreferredHeight.Location = new System.Drawing.Point(173, 21);
            this.nllPreferredHeight.Name = "nllPreferredHeight";
            this.nllPreferredHeight.Size = new System.Drawing.Size(53, 12);
            this.nllPreferredHeight.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nllPreferredHeight.TabIndex = 1;
            this.nllPreferredHeight.TabStop = true;
            this.nllPreferredHeight.Text = "最佳行高";
            // 
            // nckAutoFitMission
            // 
            this.nckAutoFitMission.AutoSize = true;
            this.nckAutoFitMission.Checked = true;
            this.nckAutoFitMission.CheckState = System.Windows.Forms.CheckState.Checked;
            this.nckAutoFitMission.Location = new System.Drawing.Point(6, 20);
            this.nckAutoFitMission.Name = "nckAutoFitMission";
            this.nckAutoFitMission.Size = new System.Drawing.Size(168, 16);
            this.nckAutoFitMission.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nckAutoFitMission.TabIndex = 0;
            this.nckAutoFitMission.Text = "查询后自动调整需求说明列";
            this.nckAutoFitMission.UseVisualStyleBackColor = true;
            // 
            // neuGroupBox2
            // 
            this.neuGroupBox2.Controls.Add(this.neuLinkLabel1);
            this.neuGroupBox2.Controls.Add(this.neuLabel4);
            this.neuGroupBox2.Controls.Add(this.neuDateTimePicker1);
            this.neuGroupBox2.Controls.Add(this.ncmbOper);
            this.neuGroupBox2.Controls.Add(this.ncmbState);
            this.neuGroupBox2.Controls.Add(this.ncmbModeName);
            this.neuGroupBox2.Controls.Add(this.neuLabel3);
            this.neuGroupBox2.Controls.Add(this.neuLabel2);
            this.neuGroupBox2.Controls.Add(this.neuLabel1);
            this.neuGroupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox2.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox2.Name = "neuGroupBox2";
            this.neuGroupBox2.Size = new System.Drawing.Size(981, 61);
            this.neuGroupBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox2.TabIndex = 5;
            this.neuGroupBox2.TabStop = false;
            this.neuGroupBox2.Text = "查询设置";
            // 
            // neuLinkLabel1
            // 
            this.neuLinkLabel1.AutoSize = true;
            this.neuLinkLabel1.Location = new System.Drawing.Point(946, 29);
            this.neuLinkLabel1.Name = "neuLinkLabel1";
            this.neuLinkLabel1.Size = new System.Drawing.Size(53, 12);
            this.neuLinkLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLinkLabel1.TabIndex = 11;
            this.neuLinkLabel1.TabStop = true;
            this.neuLinkLabel1.Text = "过滤设置";
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point(697, 29);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(77, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 10;
            this.neuLabel4.Text = "问题录入时间";
            // 
            // neuDateTimePicker1
            // 
            this.neuDateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.neuDateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.neuDateTimePicker1.IsEnter2Tab = false;
            this.neuDateTimePicker1.Location = new System.Drawing.Point(780, 25);
            this.neuDateTimePicker1.Name = "neuDateTimePicker1";
            this.neuDateTimePicker1.Size = new System.Drawing.Size(155, 21);
            this.neuDateTimePicker1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuDateTimePicker1.TabIndex = 9;
            // 
            // ncmbOper
            // 
            this.ncmbOper.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.ncmbOper.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ncmbOper.FormattingEnabled = true;
            this.ncmbOper.IsEnter2Tab = false;
            this.ncmbOper.IsFlat = false;
            this.ncmbOper.IsLike = true;
            this.ncmbOper.IsListOnly = false;
            this.ncmbOper.IsPopForm = true;
            this.ncmbOper.IsShowCustomerList = false;
            this.ncmbOper.IsShowID = false;
            this.ncmbOper.Location = new System.Drawing.Point(509, 26);
            this.ncmbOper.Name = "ncmbOper";
            this.ncmbOper.ShowCustomerList = false;
            this.ncmbOper.ShowID = false;
            this.ncmbOper.Size = new System.Drawing.Size(121, 20);
            this.ncmbOper.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.ncmbOper.TabIndex = 8;
            this.ncmbOper.Tag = "";
            this.ncmbOper.ToolBarUse = false;
            // 
            // ncmbState
            // 
            this.ncmbState.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.ncmbState.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ncmbState.FormattingEnabled = true;
            this.ncmbState.IsEnter2Tab = false;
            this.ncmbState.IsFlat = false;
            this.ncmbState.IsLike = true;
            this.ncmbState.IsListOnly = false;
            this.ncmbState.IsPopForm = true;
            this.ncmbState.IsShowCustomerList = false;
            this.ncmbState.IsShowID = false;
            this.ncmbState.Location = new System.Drawing.Point(291, 26);
            this.ncmbState.Name = "ncmbState";
            this.ncmbState.ShowCustomerList = false;
            this.ncmbState.ShowID = false;
            this.ncmbState.Size = new System.Drawing.Size(121, 20);
            this.ncmbState.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.ncmbState.TabIndex = 7;
            this.ncmbState.Tag = "";
            this.ncmbState.ToolBarUse = false;
            // 
            // ncmbModeName
            // 
            this.ncmbModeName.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.ncmbModeName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ncmbModeName.FormattingEnabled = true;
            this.ncmbModeName.IsEnter2Tab = false;
            this.ncmbModeName.IsFlat = false;
            this.ncmbModeName.IsLike = true;
            this.ncmbModeName.IsListOnly = false;
            this.ncmbModeName.IsPopForm = true;
            this.ncmbModeName.IsShowCustomerList = false;
            this.ncmbModeName.IsShowID = false;
            this.ncmbModeName.Location = new System.Drawing.Point(80, 26);
            this.ncmbModeName.Name = "ncmbModeName";
            this.ncmbModeName.ShowCustomerList = false;
            this.ncmbModeName.ShowID = false;
            this.ncmbModeName.Size = new System.Drawing.Size(121, 20);
            this.ncmbModeName.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.ncmbModeName.TabIndex = 6;
            this.ncmbModeName.Tag = "";
            this.ncmbModeName.ToolBarUse = false;
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(462, 30);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(41, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 5;
            this.neuLabel3.Text = "开发人";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(254, 30);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(29, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 3;
            this.neuLabel2.Text = "状态";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(21, 30);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(53, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 1;
            this.neuLabel1.Text = "模块名称";
            // 
            // neuGroupBox3
            // 
            this.neuGroupBox3.Controls.Add(this.textBox1);
            this.neuGroupBox3.Controls.Add(this.neuLabel5);
            this.neuGroupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox3.Location = new System.Drawing.Point(0, 61);
            this.neuGroupBox3.Name = "neuGroupBox3";
            this.neuGroupBox3.Size = new System.Drawing.Size(981, 61);
            this.neuGroupBox3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox3.TabIndex = 6;
            this.neuGroupBox3.TabStop = false;
            this.neuGroupBox3.Text = "过滤设置";
            this.neuGroupBox3.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(80, 26);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(120, 21);
            this.textBox1.TabIndex = 3;
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Location = new System.Drawing.Point(21, 29);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(53, 12);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 2;
            this.neuLabel5.Text = "模块名称";
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.fpSpread1);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel2.Location = new System.Drawing.Point(0, 122);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(981, 389);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 7;
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "3.0.2004.2005";
            this.fpSpread1.AccessibleDescription = "fpSpread1, Sheet2, Row 0, Column 0, ";
            this.fpSpread1.BackColor = System.Drawing.SystemColors.Control;
            this.fpSpread1.ContextMenuStrip = this.contextMenuStrip1;
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread1.Location = new System.Drawing.Point(0, 0);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1,
            this.fpSpread1_Sheet2});
            this.fpSpread1.Size = new System.Drawing.Size(981, 389);
            this.fpSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpSpread1.TabIndex = 0;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread1.TextTipAppearance = tipAppearance2;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.签出ToolStripMenuItem,
            this.撤销签出ToolStripMenuItem,
            this.签入ToolStripMenuItem,
            this.重置序号ToolStripMenuItem,
            this.missionCompletedToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(188, 114);
            // 
            // 签出ToolStripMenuItem
            // 
            this.签出ToolStripMenuItem.Name = "签出ToolStripMenuItem";
            this.签出ToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.签出ToolStripMenuItem.Text = "签出";
            this.签出ToolStripMenuItem.Click += new System.EventHandler(this.签出ToolStripMenuItem_Click);
            // 
            // 撤销签出ToolStripMenuItem
            // 
            this.撤销签出ToolStripMenuItem.Name = "撤销签出ToolStripMenuItem";
            this.撤销签出ToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.撤销签出ToolStripMenuItem.Text = "撤销签出";
            this.撤销签出ToolStripMenuItem.Click += new System.EventHandler(this.撤销签出ToolStripMenuItem_Click);
            // 
            // 签入ToolStripMenuItem
            // 
            this.签入ToolStripMenuItem.Name = "签入ToolStripMenuItem";
            this.签入ToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.签入ToolStripMenuItem.Text = "签入";
            this.签入ToolStripMenuItem.Click += new System.EventHandler(this.签入ToolStripMenuItem_Click);
            // 
            // 重置序号ToolStripMenuItem
            // 
            this.重置序号ToolStripMenuItem.Name = "重置序号ToolStripMenuItem";
            this.重置序号ToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.重置序号ToolStripMenuItem.Text = "重置序号";
            this.重置序号ToolStripMenuItem.Click += new System.EventHandler(this.重置序号ToolStripMenuItem_Click);
            // 
            // missionCompletedToolStripMenuItem
            // 
            this.missionCompletedToolStripMenuItem.Name = "missionCompletedToolStripMenuItem";
            this.missionCompletedToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.missionCompletedToolStripMenuItem.Text = "Mission completed";
            this.missionCompletedToolStripMenuItem.Click += new System.EventHandler(this.missionCompletedToolStripMenuItem_Click);
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.SystemColors.Control, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Black, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.ColumnHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, System.Drawing.Color.Black, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Black);
            this.fpSpread1_Sheet1.ColumnHeader.Rows.Get(0).Height = 38F;
            this.fpSpread1_Sheet1.ColumnHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, System.Drawing.Color.Black, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Black);
            this.fpSpread1_Sheet1.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.RowHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, System.Drawing.Color.Black, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Black);
            this.fpSpread1_Sheet1.RowHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, System.Drawing.Color.Black, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Black);
            this.fpSpread1_Sheet1.SheetCornerHorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, System.Drawing.Color.Black, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Black);
            this.fpSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.fpSpread1_Sheet1.SheetCornerVerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, System.Drawing.Color.Black, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Black);
            this.fpSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // fpSpread1_Sheet2
            // 
            this.fpSpread1_Sheet2.Reset();
            this.fpSpread1_Sheet2.SheetName = "Sheet2";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet2.RowHeader.Columns.Default.Resizable = false;
            this.fpSpread1_Sheet2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ucTransTraker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuPanel2);
            this.Controls.Add(this.neuGroupBox3);
            this.Controls.Add(this.neuGroupBox2);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucTransTraker";
            this.Size = new System.Drawing.Size(981, 555);
            this.neuPanel1.ResumeLayout(false);
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.neuGroupBox2.ResumeLayout(false);
            this.neuGroupBox2.PerformLayout();
            this.neuGroupBox3.ResumeLayout(false);
            this.neuGroupBox3.PerformLayout();
            this.neuPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuLinkLabel nllPreferredHeight;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox nckAutoFitMission;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker neuDateTimePicker1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox ncmbOper;
        private FS.FrameWork.WinForms.Controls.NeuComboBox ncmbState;
        private FS.FrameWork.WinForms.Controls.NeuComboBox ncmbModeName;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox3;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.SOC.Windows.Forms.FpSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 签出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 签入ToolStripMenuItem;
        private FS.FrameWork.WinForms.Controls.NeuLinkLabel neuLinkLabel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbLogionInfo;
        private System.Windows.Forms.ToolStripMenuItem 重置序号ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 撤销签出ToolStripMenuItem;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbAssignMission;
        private System.Windows.Forms.ToolStripMenuItem missionCompletedToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
    }
}
