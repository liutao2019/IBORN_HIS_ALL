namespace FS.HISFC.Components.OutpatientFee.Forms
{
    partial class frmEleInvoice
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.fpElePTInvoice = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpElePTInvoice_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.fpEleYBInvoice = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpEleYBInvoice_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.lblDoctor = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbPactInfo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblPact = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ptbtn = new System.Windows.Forms.Button();
            this.ybbtn = new System.Windows.Forms.Button();
            this.gbPatientInfo = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuComboBox6 = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.tbPName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.txtPhone = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuTextBox4 = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuTextBox1 = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuTextBox3 = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.tbInvoiceDate = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbOperName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblDateIn = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbInvoiceNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpElePTInvoice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpElePTInvoice_Sheet1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpEleYBInvoice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpEleYBInvoice_Sheet1)).BeginInit();
            this.gbPatientInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.fpElePTInvoice);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 208);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(600, 445);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "普通发票费用信息";
            // 
            // fpElePTInvoice
            // 
            this.fpElePTInvoice.About = "3.0.2004.2005";
            this.fpElePTInvoice.AccessibleDescription = "fpElePTInvoice, Sheet1, Row 0, Column 0, ";
            this.fpElePTInvoice.BackColor = System.Drawing.Color.White;
            this.fpElePTInvoice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpElePTInvoice.FileName = "";
            this.fpElePTInvoice.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpElePTInvoice.IsAutoSaveGridStatus = false;
            this.fpElePTInvoice.IsCanCustomConfigColumn = false;
            this.fpElePTInvoice.Location = new System.Drawing.Point(3, 17);
            this.fpElePTInvoice.Name = "fpElePTInvoice";
            this.fpElePTInvoice.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpElePTInvoice.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpElePTInvoice_Sheet1});
            this.fpElePTInvoice.Size = new System.Drawing.Size(594, 425);
            this.fpElePTInvoice.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpElePTInvoice.TabIndex = 37;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpElePTInvoice.TextTipAppearance = tipAppearance1;
            this.fpElePTInvoice.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpElePTInvoice_Sheet1
            // 
            this.fpElePTInvoice_Sheet1.Reset();
            this.fpElePTInvoice_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpElePTInvoice_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpElePTInvoice_Sheet1.ColumnCount = 5;
            this.fpElePTInvoice_Sheet1.RowCount = 1;
            this.fpElePTInvoice_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpElePTInvoice_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "选择";
            this.fpElePTInvoice_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "费用科目";
            this.fpElePTInvoice_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "实收金额";
            this.fpElePTInvoice_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "开票比例金额";
            this.fpElePTInvoice_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "费用编码";
            this.fpElePTInvoice_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpElePTInvoice_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpElePTInvoice_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.fpElePTInvoice_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpElePTInvoice_Sheet1.Columns.Get(0).Label = "选择";
            this.fpElePTInvoice_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpElePTInvoice_Sheet1.Columns.Get(0).Width = 82F;
            this.fpElePTInvoice_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpElePTInvoice_Sheet1.Columns.Get(1).Label = "费用科目";
            this.fpElePTInvoice_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpElePTInvoice_Sheet1.Columns.Get(1).Width = 80F;
            this.fpElePTInvoice_Sheet1.Columns.Get(2).CellType = numberCellType1;
            this.fpElePTInvoice_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpElePTInvoice_Sheet1.Columns.Get(2).Label = "实收金额";
            this.fpElePTInvoice_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpElePTInvoice_Sheet1.Columns.Get(2).Width = 89F;
            this.fpElePTInvoice_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpElePTInvoice_Sheet1.Columns.Get(3).Label = "开票比例金额";
            this.fpElePTInvoice_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpElePTInvoice_Sheet1.Columns.Get(3).Width = 145F;
            this.fpElePTInvoice_Sheet1.Columns.Get(4).Label = "费用编码";
            this.fpElePTInvoice_Sheet1.Columns.Get(4).Visible = false;
            this.fpElePTInvoice_Sheet1.Columns.Get(4).Width = 92F;
            this.fpElePTInvoice_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpElePTInvoice_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.fpElePTInvoice_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpElePTInvoice_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpElePTInvoice_Sheet1.Rows.Default.Height = 25F;
            this.fpElePTInvoice_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpElePTInvoice_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.fpElePTInvoice_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpElePTInvoice_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.fpEleYBInvoice);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(600, 208);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(600, 445);
            this.groupBox2.TabIndex = 41;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "医保费用信息";
            // 
            // fpEleYBInvoice
            // 
            this.fpEleYBInvoice.About = "3.0.2004.2005";
            this.fpEleYBInvoice.AccessibleDescription = "fpEleYBInvoice, Sheet1, Row 0, Column 0, ";
            this.fpEleYBInvoice.BackColor = System.Drawing.Color.White;
            this.fpEleYBInvoice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpEleYBInvoice.FileName = "";
            this.fpEleYBInvoice.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpEleYBInvoice.IsAutoSaveGridStatus = false;
            this.fpEleYBInvoice.IsCanCustomConfigColumn = false;
            this.fpEleYBInvoice.Location = new System.Drawing.Point(3, 17);
            this.fpEleYBInvoice.Name = "fpEleYBInvoice";
            this.fpEleYBInvoice.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpEleYBInvoice.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpEleYBInvoice_Sheet1});
            this.fpEleYBInvoice.Size = new System.Drawing.Size(594, 425);
            this.fpEleYBInvoice.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpEleYBInvoice.TabIndex = 37;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpEleYBInvoice.TextTipAppearance = tipAppearance2;
            this.fpEleYBInvoice.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpEleYBInvoice_Sheet1
            // 
            this.fpEleYBInvoice_Sheet1.Reset();
            this.fpEleYBInvoice_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpEleYBInvoice_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpEleYBInvoice_Sheet1.ColumnCount = 3;
            this.fpEleYBInvoice_Sheet1.RowCount = 1;
            this.fpEleYBInvoice_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpEleYBInvoice_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "费用科目";
            this.fpEleYBInvoice_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "医保费用";
            this.fpEleYBInvoice_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "费用编码";
            this.fpEleYBInvoice_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpEleYBInvoice_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpEleYBInvoice_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.fpEleYBInvoice_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.fpEleYBInvoice_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpEleYBInvoice_Sheet1.Columns.Get(0).Label = "费用科目";
            this.fpEleYBInvoice_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpEleYBInvoice_Sheet1.Columns.Get(0).Width = 82F;
            this.fpEleYBInvoice_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpEleYBInvoice_Sheet1.Columns.Get(1).Label = "医保费用";
            this.fpEleYBInvoice_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpEleYBInvoice_Sheet1.Columns.Get(1).Width = 82F;
            this.fpEleYBInvoice_Sheet1.Columns.Get(2).Label = "费用编码";
            this.fpEleYBInvoice_Sheet1.Columns.Get(2).Width = 86F;
            this.fpEleYBInvoice_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpEleYBInvoice_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.fpEleYBInvoice_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpEleYBInvoice_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpEleYBInvoice_Sheet1.Rows.Default.Height = 25F;
            this.fpEleYBInvoice_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpEleYBInvoice_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.fpEleYBInvoice_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpEleYBInvoice_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // lblDoctor
            // 
            this.lblDoctor.AutoSize = true;
            this.lblDoctor.Location = new System.Drawing.Point(250, 60);
            this.lblDoctor.Name = "lblDoctor";
            this.lblDoctor.Size = new System.Drawing.Size(41, 12);
            this.lblDoctor.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblDoctor.TabIndex = 44;
            this.lblDoctor.Text = "收款员";
            // 
            // tbPactInfo
            // 
            this.tbPactInfo.BackColor = System.Drawing.Color.White;
            this.tbPactInfo.ForeColor = System.Drawing.Color.Black;
            this.tbPactInfo.IsEnter2Tab = false;
            this.tbPactInfo.Location = new System.Drawing.Point(82, 57);
            this.tbPactInfo.Name = "tbPactInfo";
            this.tbPactInfo.ReadOnly = true;
            this.tbPactInfo.Size = new System.Drawing.Size(100, 21);
            this.tbPactInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbPactInfo.TabIndex = 35;
            // 
            // lblPact
            // 
            this.lblPact.AutoSize = true;
            this.lblPact.Location = new System.Drawing.Point(27, 60);
            this.lblPact.Name = "lblPact";
            this.lblPact.Size = new System.Drawing.Size(53, 12);
            this.lblPact.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblPact.TabIndex = 36;
            this.lblPact.Text = "合同单位";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(27, 29);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(53, 12);
            this.lblName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblName.TabIndex = 34;
            this.lblName.Text = "患者姓名";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(27, 96);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(53, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 52;
            this.neuLabel1.Text = "电子邮箱";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(82, 93);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(139, 21);
            this.textBox1.TabIndex = 53;
            // 
            // ptbtn
            // 
            this.ptbtn.Location = new System.Drawing.Point(475, 134);
            this.ptbtn.Name = "ptbtn";
            this.ptbtn.Size = new System.Drawing.Size(116, 23);
            this.ptbtn.TabIndex = 54;
            this.ptbtn.Text = "生成普通电子发票";
            this.ptbtn.UseVisualStyleBackColor = true;
            this.ptbtn.Click += new System.EventHandler(this.ptbtn_Click);
            // 
            // ybbtn
            // 
            this.ybbtn.Location = new System.Drawing.Point(603, 134);
            this.ybbtn.Name = "ybbtn";
            this.ybbtn.Size = new System.Drawing.Size(124, 23);
            this.ybbtn.TabIndex = 55;
            this.ybbtn.Text = "生成医保电子发票";
            this.ybbtn.UseVisualStyleBackColor = true;
            this.ybbtn.Click += new System.EventHandler(this.ybbtn_Click);
            // 
            // gbPatientInfo
            // 
            this.gbPatientInfo.Controls.Add(this.neuComboBox6);
            this.gbPatientInfo.Controls.Add(this.tbPName);
            this.gbPatientInfo.Controls.Add(this.richTextBox1);
            this.gbPatientInfo.Controls.Add(this.neuLabel6);
            this.gbPatientInfo.Controls.Add(this.comboBox1);
            this.gbPatientInfo.Controls.Add(this.textBox2);
            this.gbPatientInfo.Controls.Add(this.txtPhone);
            this.gbPatientInfo.Controls.Add(this.neuLabel5);
            this.gbPatientInfo.Controls.Add(this.neuTextBox4);
            this.gbPatientInfo.Controls.Add(this.neuLabel3);
            this.gbPatientInfo.Controls.Add(this.neuTextBox1);
            this.gbPatientInfo.Controls.Add(this.neuLabel4);
            this.gbPatientInfo.Controls.Add(this.neuTextBox3);
            this.gbPatientInfo.Controls.Add(this.tbInvoiceDate);
            this.gbPatientInfo.Controls.Add(this.neuLabel2);
            this.gbPatientInfo.Controls.Add(this.ybbtn);
            this.gbPatientInfo.Controls.Add(this.ptbtn);
            this.gbPatientInfo.Controls.Add(this.textBox1);
            this.gbPatientInfo.Controls.Add(this.neuLabel1);
            this.gbPatientInfo.Controls.Add(this.lblName);
            this.gbPatientInfo.Controls.Add(this.lblPact);
            this.gbPatientInfo.Controls.Add(this.tbPactInfo);
            this.gbPatientInfo.Controls.Add(this.tbOperName);
            this.gbPatientInfo.Controls.Add(this.lblDoctor);
            this.gbPatientInfo.Controls.Add(this.lblDateIn);
            this.gbPatientInfo.Controls.Add(this.tbInvoiceNo);
            this.gbPatientInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbPatientInfo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gbPatientInfo.Location = new System.Drawing.Point(0, 0);
            this.gbPatientInfo.Name = "gbPatientInfo";
            this.gbPatientInfo.Size = new System.Drawing.Size(1203, 208);
            this.gbPatientInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbPatientInfo.TabIndex = 36;
            this.gbPatientInfo.TabStop = false;
            this.gbPatientInfo.Text = "患者信息";
            // 
            // neuComboBox6
            // 
            this.neuComboBox6.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.neuComboBox6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.neuComboBox6.FormattingEnabled = true;
            this.neuComboBox6.IsEnter2Tab = false;
            this.neuComboBox6.IsFlat = false;
            this.neuComboBox6.IsLike = true;
            this.neuComboBox6.IsListOnly = false;
            this.neuComboBox6.IsPopForm = true;
            this.neuComboBox6.IsShowCustomerList = false;
            this.neuComboBox6.IsShowID = false;
            this.neuComboBox6.IsShowIDAndName = false;
            this.neuComboBox6.Location = new System.Drawing.Point(519, 93);
            this.neuComboBox6.Margin = new System.Windows.Forms.Padding(4);
            this.neuComboBox6.Name = "neuComboBox6";
            this.neuComboBox6.ShowCustomerList = false;
            this.neuComboBox6.ShowID = false;
            this.neuComboBox6.Size = new System.Drawing.Size(136, 20);
            this.neuComboBox6.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.neuComboBox6.TabIndex = 95;
            this.neuComboBox6.Tag = "";
            this.neuComboBox6.ToolBarUse = false;
            // 
            // tbPName
            // 
            this.tbPName.BackColor = System.Drawing.Color.White;
            this.tbPName.ForeColor = System.Drawing.Color.Black;
            this.tbPName.IsEnter2Tab = false;
            this.tbPName.Location = new System.Drawing.Point(82, 26);
            this.tbPName.Name = "tbPName";
            this.tbPName.Size = new System.Drawing.Size(100, 21);
            this.tbPName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbPName.TabIndex = 77;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(82, 131);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(386, 71);
            this.richTextBox1.TabIndex = 76;
            this.richTextBox1.Text = "医保类型：在职人员，医保编号：，医保统筹基金支付：元，其他支付：0元，个人账户支付：0元，个人现金支付：0元，个人自付：0元，个人自费：0元。";
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.Location = new System.Drawing.Point(27, 134);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(53, 12);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 75;
            this.neuLabel6.Text = "备注信息";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(448, 93);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(53, 20);
            this.comboBox1.TabIndex = 74;
            this.comboBox1.Text = "电票";
            this.comboBox1.Visible = false;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(294, 93);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(122, 21);
            this.textBox2.TabIndex = 73;
            // 
            // txtPhone
            // 
            this.txtPhone.AutoSize = true;
            this.txtPhone.Location = new System.Drawing.Point(250, 96);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(41, 12);
            this.txtPhone.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtPhone.TabIndex = 72;
            this.txtPhone.Text = "手机号";
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Location = new System.Drawing.Point(401, 31);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(65, 12);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 71;
            this.neuLabel5.Text = "地址、电话";
            // 
            // neuTextBox4
            // 
            this.neuTextBox4.BackColor = System.Drawing.Color.White;
            this.neuTextBox4.ForeColor = System.Drawing.Color.Black;
            this.neuTextBox4.IsEnter2Tab = false;
            this.neuTextBox4.Location = new System.Drawing.Point(472, 26);
            this.neuTextBox4.Name = "neuTextBox4";
            this.neuTextBox4.Size = new System.Drawing.Size(100, 21);
            this.neuTextBox4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTextBox4.TabIndex = 70;
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(214, 29);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(77, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 67;
            this.neuLabel3.Text = "纳税人识别号";
            // 
            // neuTextBox1
            // 
            this.neuTextBox1.BackColor = System.Drawing.Color.White;
            this.neuTextBox1.ForeColor = System.Drawing.Color.Black;
            this.neuTextBox1.IsEnter2Tab = false;
            this.neuTextBox1.Location = new System.Drawing.Point(294, 26);
            this.neuTextBox1.Name = "neuTextBox1";
            this.neuTextBox1.Size = new System.Drawing.Size(100, 21);
            this.neuTextBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTextBox1.TabIndex = 66;
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point(596, 31);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(77, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 69;
            this.neuLabel4.Text = "开户行及账号";
            // 
            // neuTextBox3
            // 
            this.neuTextBox3.BackColor = System.Drawing.Color.White;
            this.neuTextBox3.ForeColor = System.Drawing.Color.Black;
            this.neuTextBox3.IsEnter2Tab = false;
            this.neuTextBox3.Location = new System.Drawing.Point(687, 26);
            this.neuTextBox3.Name = "neuTextBox3";
            this.neuTextBox3.Size = new System.Drawing.Size(100, 21);
            this.neuTextBox3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTextBox3.TabIndex = 68;
            // 
            // tbInvoiceDate
            // 
            this.tbInvoiceDate.BackColor = System.Drawing.Color.White;
            this.tbInvoiceDate.ForeColor = System.Drawing.Color.Black;
            this.tbInvoiceDate.IsEnter2Tab = false;
            this.tbInvoiceDate.Location = new System.Drawing.Point(473, 57);
            this.tbInvoiceDate.Name = "tbInvoiceDate";
            this.tbInvoiceDate.ReadOnly = true;
            this.tbInvoiceDate.Size = new System.Drawing.Size(99, 21);
            this.tbInvoiceDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbInvoiceDate.TabIndex = 57;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(413, 60);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(53, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 58;
            this.neuLabel2.Text = "发票日期";
            // 
            // tbOperName
            // 
            this.tbOperName.BackColor = System.Drawing.Color.White;
            this.tbOperName.ForeColor = System.Drawing.Color.Black;
            this.tbOperName.IsEnter2Tab = false;
            this.tbOperName.Location = new System.Drawing.Point(294, 55);
            this.tbOperName.Name = "tbOperName";
            this.tbOperName.ReadOnly = true;
            this.tbOperName.Size = new System.Drawing.Size(100, 21);
            this.tbOperName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbOperName.TabIndex = 43;
            // 
            // lblDateIn
            // 
            this.lblDateIn.AutoSize = true;
            this.lblDateIn.Location = new System.Drawing.Point(632, 60);
            this.lblDateIn.Name = "lblDateIn";
            this.lblDateIn.Size = new System.Drawing.Size(41, 12);
            this.lblDateIn.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblDateIn.TabIndex = 42;
            this.lblDateIn.Text = "发票号";
            // 
            // tbInvoiceNo
            // 
            this.tbInvoiceNo.BackColor = System.Drawing.Color.White;
            this.tbInvoiceNo.ForeColor = System.Drawing.Color.Black;
            this.tbInvoiceNo.IsEnter2Tab = false;
            this.tbInvoiceNo.Location = new System.Drawing.Point(687, 57);
            this.tbInvoiceNo.Name = "tbInvoiceNo";
            this.tbInvoiceNo.ReadOnly = true;
            this.tbInvoiceNo.Size = new System.Drawing.Size(100, 21);
            this.tbInvoiceNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbInvoiceNo.TabIndex = 41;
            // 
            // frmEleInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1203, 653);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbPatientInfo);
            this.Name = "frmEleInvoice";
            this.Text = "生成电子发票";
            this.Load += new System.EventHandler(this.frmEleInvoice_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpElePTInvoice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpElePTInvoice_Sheet1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpEleYBInvoice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpEleYBInvoice_Sheet1)).EndInit();
            this.gbPatientInfo.ResumeLayout(false);
            this.gbPatientInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpElePTInvoice;
        private FarPoint.Win.Spread.SheetView fpElePTInvoice_Sheet1;
        private System.Windows.Forms.GroupBox groupBox2;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpEleYBInvoice;
        private FarPoint.Win.Spread.SheetView fpEleYBInvoice_Sheet1;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblDoctor;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox tbPactInfo;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblPact;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblName;
        protected FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button ptbtn;
        private System.Windows.Forms.Button ybbtn;
        protected FS.FrameWork.WinForms.Controls.NeuGroupBox gbPatientInfo;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox tbOperName;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblDateIn;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox tbInvoiceNo;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox tbInvoiceDate;
        protected FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBox2;
        protected FS.FrameWork.WinForms.Controls.NeuLabel txtPhone;
        protected FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox neuTextBox4;
        protected FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox neuTextBox1;
        protected FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox neuTextBox3;
        private System.Windows.Forms.RichTextBox richTextBox1;
        protected FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox tbPName;
        private FS.FrameWork.WinForms.Controls.NeuComboBox neuComboBox6;
    }
}