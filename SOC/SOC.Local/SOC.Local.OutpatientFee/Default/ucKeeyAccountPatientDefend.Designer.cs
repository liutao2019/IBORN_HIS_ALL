namespace FS.SOC.Local.OutpatientFee.Default
{
    partial class ucKeeyAccountPatientDefend
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.tabPageMain = new FS.FrameWork.WinForms.Controls.NeuTabControl();
            this.tabPatientList = new System.Windows.Forms.TabPage();
            this.tabEditPatient = new System.Windows.Forms.TabPage();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.txtIDNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblIDNO = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.txtMarkNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuPanel4 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.txtEditName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtEditIdno = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.cmbSex = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblSex = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtSSDW = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.startDate = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.EndDate = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.tabPageMain.SuspendLayout();
            this.tabPatientList.SuspendLayout();
            this.tabEditPatient.SuspendLayout();
            this.neuPanel1.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.neuPanel4.SuspendLayout();
            this.neuGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPageMain
            // 
            this.tabPageMain.Controls.Add(this.tabPatientList);
            this.tabPageMain.Controls.Add(this.tabEditPatient);
            this.tabPageMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPageMain.Location = new System.Drawing.Point(0, 0);
            this.tabPageMain.Name = "tabPageMain";
            this.tabPageMain.SelectedIndex = 0;
            this.tabPageMain.Size = new System.Drawing.Size(670, 376);
            this.tabPageMain.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tabPageMain.TabIndex = 0;
            // 
            // tabPatientList
            // 
            this.tabPatientList.Controls.Add(this.neuPanel2);
            this.tabPatientList.Controls.Add(this.neuPanel1);
            this.tabPatientList.Location = new System.Drawing.Point(4, 22);
            this.tabPatientList.Name = "tabPatientList";
            this.tabPatientList.Padding = new System.Windows.Forms.Padding(3);
            this.tabPatientList.Size = new System.Drawing.Size(662, 350);
            this.tabPatientList.TabIndex = 0;
            this.tabPatientList.Text = "名单查询";
            this.tabPatientList.UseVisualStyleBackColor = true;
            // 
            // tabEditPatient
            // 
            this.tabEditPatient.Controls.Add(this.neuPanel4);
            this.tabEditPatient.Controls.Add(this.neuGroupBox1);
            this.tabEditPatient.Location = new System.Drawing.Point(4, 22);
            this.tabEditPatient.Name = "tabEditPatient";
            this.tabEditPatient.Padding = new System.Windows.Forms.Padding(3);
            this.tabEditPatient.Size = new System.Drawing.Size(662, 350);
            this.tabEditPatient.TabIndex = 1;
            this.tabEditPatient.Text = "登记、修改信息";
            this.tabEditPatient.UseVisualStyleBackColor = true;
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.txtName);
            this.neuPanel1.Controls.Add(this.lblName);
            this.neuPanel1.Controls.Add(this.txtIDNO);
            this.neuPanel1.Controls.Add(this.lblIDNO);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(3, 3);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(656, 58);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // txtIDNO
            // 
            this.txtIDNO.IsEnter2Tab = false;
            this.txtIDNO.Location = new System.Drawing.Point(78, 16);
            this.txtIDNO.Name = "txtIDNO";
            this.txtIDNO.Size = new System.Drawing.Size(160, 21);
            this.txtIDNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtIDNO.TabIndex = 18;
            // 
            // lblIDNO
            // 
            this.lblIDNO.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblIDNO.Location = new System.Drawing.Point(14, 20);
            this.lblIDNO.Name = "lblIDNO";
            this.lblIDNO.Size = new System.Drawing.Size(73, 13);
            this.lblIDNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblIDNO.TabIndex = 17;
            this.lblIDNO.Text = "身份证号:";
            // 
            // txtName
            // 
            this.txtName.IsEnter2Tab = false;
            this.txtName.Location = new System.Drawing.Point(332, 16);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(97, 21);
            this.txtName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtName.TabIndex = 20;
            // 
            // lblName
            // 
            this.lblName.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblName.ForeColor = System.Drawing.Color.Black;
            this.lblName.Location = new System.Drawing.Point(269, 20);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(73, 13);
            this.lblName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblName.TabIndex = 19;
            this.lblName.Text = "姓 　 名:";
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.neuSpread1);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel2.Location = new System.Drawing.Point(3, 61);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(656, 286);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 1;
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
            this.neuSpread1.Location = new System.Drawing.Point(0, 0);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(656, 286);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.neuSpread1_CellDoubleClick);
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ColumnCount = 7;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "编号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "CardNO";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "身份证号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "姓名";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "所属单位";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "有效时间";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "状态";
            this.neuSpread1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "编号";
            this.neuSpread1_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 81F;
            this.neuSpread1_Sheet1.Columns.Get(1).CellType = textCellType1;
            this.neuSpread1_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "CardNO";
            this.neuSpread1_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 62F;
            this.neuSpread1_Sheet1.Columns.Get(2).CellType = textCellType2;
            this.neuSpread1_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "身份证号";
            this.neuSpread1_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 134F;
            this.neuSpread1_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "姓名";
            this.neuSpread1_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 88F;
            this.neuSpread1_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "所属单位";
            this.neuSpread1_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 263F;
            this.neuSpread1_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "有效时间";
            this.neuSpread1_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 266F;
            this.neuSpread1_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(6).Label = "状态";
            this.neuSpread1_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // txtMarkNO
            // 
            this.txtMarkNO.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtMarkNO.IsEnter2Tab = false;
            this.txtMarkNO.Location = new System.Drawing.Point(75, 39);
            this.txtMarkNO.Name = "txtMarkNO";
            this.txtMarkNO.Size = new System.Drawing.Size(140, 21);
            this.txtMarkNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtMarkNO.TabIndex = 3;
            this.txtMarkNO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMarkNO_KeyDown);
            // 
            // neuPanel4
            // 
            this.neuPanel4.Controls.Add(this.EndDate);
            this.neuPanel4.Controls.Add(this.startDate);
            this.neuPanel4.Controls.Add(this.txtEditName);
            this.neuPanel4.Controls.Add(this.txtSSDW);
            this.neuPanel4.Controls.Add(this.txtEditIdno);
            this.neuPanel4.Controls.Add(this.cmbSex);
            this.neuPanel4.Controls.Add(this.neuLabel6);
            this.neuPanel4.Controls.Add(this.neuLabel2);
            this.neuPanel4.Controls.Add(this.neuLabel4);
            this.neuPanel4.Controls.Add(this.neuLabel1);
            this.neuPanel4.Controls.Add(this.neuLabel3);
            this.neuPanel4.Controls.Add(this.lblSex);
            this.neuPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel4.Location = new System.Drawing.Point(3, 88);
            this.neuPanel4.Name = "neuPanel4";
            this.neuPanel4.Size = new System.Drawing.Size(656, 259);
            this.neuPanel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel4.TabIndex = 2;
            // 
            // txtEditName
            // 
            this.txtEditName.IsEnter2Tab = false;
            this.txtEditName.Location = new System.Drawing.Point(77, 26);
            this.txtEditName.Name = "txtEditName";
            this.txtEditName.Size = new System.Drawing.Size(105, 21);
            this.txtEditName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtEditName.TabIndex = 20;
            // 
            // txtEditIdno
            // 
            this.txtEditIdno.IsEnter2Tab = false;
            this.txtEditIdno.Location = new System.Drawing.Point(453, 26);
            this.txtEditIdno.Name = "txtEditIdno";
            this.txtEditIdno.Size = new System.Drawing.Size(160, 21);
            this.txtEditIdno.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtEditIdno.TabIndex = 22;
            // 
            // cmbSex
            // 
            this.cmbSex.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbSex.FormattingEnabled = true;
            this.cmbSex.IsEnter2Tab = false;
            this.cmbSex.IsFlat = false;
            this.cmbSex.IsLike = true;
            this.cmbSex.IsListOnly = false;
            this.cmbSex.IsPopForm = true;
            this.cmbSex.IsShowCustomerList = false;
            this.cmbSex.IsShowID = false;
            this.cmbSex.Location = new System.Drawing.Point(274, 26);
            this.cmbSex.Name = "cmbSex";
            this.cmbSex.PopForm = null;
            this.cmbSex.ShowCustomerList = false;
            this.cmbSex.ShowID = false;
            this.cmbSex.Size = new System.Drawing.Size(100, 20);
            this.cmbSex.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbSex.TabIndex = 21;
            this.cmbSex.Tag = "";
            this.cmbSex.ToolBarUse = false;
            // 
            // neuLabel2
            // 
            this.neuLabel2.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel2.ForeColor = System.Drawing.Color.Black;
            this.neuLabel2.Location = new System.Drawing.Point(13, 30);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(73, 13);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 17;
            this.neuLabel2.Text = "姓 　 名:";
            // 
            // neuLabel3
            // 
            this.neuLabel3.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel3.Location = new System.Drawing.Point(389, 30);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(73, 13);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 18;
            this.neuLabel3.Text = "身份证号:";
            // 
            // lblSex
            // 
            this.lblSex.AutoSize = true;
            this.lblSex.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSex.ForeColor = System.Drawing.Color.Black;
            this.lblSex.Location = new System.Drawing.Point(210, 30);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(68, 13);
            this.lblSex.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblSex.TabIndex = 19;
            this.lblSex.Text = "性    别:";
            // 
            // neuLabel4
            // 
            this.neuLabel4.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel4.Location = new System.Drawing.Point(13, 77);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(73, 13);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 18;
            this.neuLabel4.Text = "所属单位:";
            // 
            // txtSSDW
            // 
            this.txtSSDW.IsEnter2Tab = false;
            this.txtSSDW.Location = new System.Drawing.Point(77, 74);
            this.txtSSDW.Name = "txtSSDW";
            this.txtSSDW.Size = new System.Drawing.Size(536, 21);
            this.txtSSDW.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtSSDW.TabIndex = 22;
            // 
            // neuLabel5
            // 
            this.neuLabel5.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel5.ForeColor = System.Drawing.Color.Black;
            this.neuLabel5.Location = new System.Drawing.Point(11, 43);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(73, 13);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 17;
            this.neuLabel5.Text = "就诊卡号:";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel1.ForeColor = System.Drawing.Color.Black;
            this.neuLabel1.Location = new System.Drawing.Point(210, 132);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(66, 13);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 19;
            this.neuLabel1.Text = "终止日期:";
            // 
            // neuLabel6
            // 
            this.neuLabel6.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel6.ForeColor = System.Drawing.Color.Black;
            this.neuLabel6.Location = new System.Drawing.Point(13, 132);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(73, 13);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 17;
            this.neuLabel6.Text = "起始日期:";
            // 
            // startDate
            // 
            this.startDate.IsEnter2Tab = false;
            this.startDate.Location = new System.Drawing.Point(77, 128);
            this.startDate.Name = "startDate";
            this.startDate.Size = new System.Drawing.Size(124, 21);
            this.startDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.startDate.TabIndex = 23;
            // 
            // EndDate
            // 
            this.EndDate.IsEnter2Tab = false;
            this.EndDate.Location = new System.Drawing.Point(274, 128);
            this.EndDate.Name = "EndDate";
            this.EndDate.Size = new System.Drawing.Size(124, 21);
            this.EndDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.EndDate.TabIndex = 23;
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.txtMarkNO);
            this.neuGroupBox1.Controls.Add(this.neuLabel5);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox1.Location = new System.Drawing.Point(3, 3);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(656, 85);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 3;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "刷卡登记";
            // 
            // ucKeeyAccountPatientDefend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabPageMain);
            this.Name = "ucKeeyAccountPatientDefend";
            this.Size = new System.Drawing.Size(670, 376);
            this.Load += new System.EventHandler(this.ucKeeyAccountPatientDefend_Load);
            this.tabPageMain.ResumeLayout(false);
            this.tabPatientList.ResumeLayout(false);
            this.tabEditPatient.ResumeLayout(false);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.neuPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.neuPanel4.ResumeLayout(false);
            this.neuPanel4.PerformLayout();
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuTabControl tabPageMain;
        private System.Windows.Forms.TabPage tabPatientList;
        private System.Windows.Forms.TabPage tabEditPatient;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtIDNO;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblIDNO;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtName;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblName;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtMarkNO;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel4;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtEditName;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtEditIdno;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbSex;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblSex;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtSSDW;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker EndDate;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker startDate;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
    }
}
