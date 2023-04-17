namespace FS.HISFC.Components.Order.Medical.Controls
{
    partial class ucAllergyIn
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
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lblPatientInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbHappenNo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbInpatientNo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbPatientKind = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lbPatientKind = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbDrug = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbAllergyType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbAllergyDegree = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuButton1 = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuCheckBox1 = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.neuTextBox1 = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbAllergyType = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbAllergyDegree = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbDrug = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuGroupBox1.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            this.neuPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.BackColor = System.Drawing.Color.White;
            this.neuGroupBox1.Controls.Add(this.neuPanel2);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(720, 181);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 2;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "患者基本信息";
            // 
            // neuPanel2
            // 
            this.neuPanel2.BackColor = System.Drawing.Color.White;
            this.neuPanel2.Controls.Add(this.lblPatientInfo);
            this.neuPanel2.Controls.Add(this.lbHappenNo);
            this.neuPanel2.Controls.Add(this.lbInpatientNo);
            this.neuPanel2.Controls.Add(this.cmbPatientKind);
            this.neuPanel2.Controls.Add(this.lbPatientKind);
            this.neuPanel2.Controls.Add(this.neuLabel2);
            this.neuPanel2.Controls.Add(this.cmbDrug);
            this.neuPanel2.Controls.Add(this.cmbAllergyType);
            this.neuPanel2.Controls.Add(this.cmbAllergyDegree);
            this.neuPanel2.Controls.Add(this.neuButton1);
            this.neuPanel2.Controls.Add(this.neuCheckBox1);
            this.neuPanel2.Controls.Add(this.neuTextBox1);
            this.neuPanel2.Controls.Add(this.neuLabel1);
            this.neuPanel2.Controls.Add(this.lbAllergyType);
            this.neuPanel2.Controls.Add(this.lbAllergyDegree);
            this.neuPanel2.Controls.Add(this.lbDrug);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel2.Location = new System.Drawing.Point(3, 19);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(714, 159);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 1;
            // 
            // lblPatientInfo
            // 
            this.lblPatientInfo.AutoSize = true;
            this.lblPatientInfo.ForeColor = System.Drawing.Color.Blue;
            this.lblPatientInfo.Location = new System.Drawing.Point(240, 14);
            this.lblPatientInfo.Name = "lblPatientInfo";
            this.lblPatientInfo.Size = new System.Drawing.Size(63, 14);
            this.lblPatientInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblPatientInfo.TabIndex = 23;
            this.lblPatientInfo.Text = "患者信息";
            // 
            // lbHappenNo
            // 
            this.lbHappenNo.AutoSize = true;
            this.lbHappenNo.Location = new System.Drawing.Point(144, 133);
            this.lbHappenNo.Name = "lbHappenNo";
            this.lbHappenNo.Size = new System.Drawing.Size(63, 14);
            this.lbHappenNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbHappenNo.TabIndex = 22;
            this.lbHappenNo.Text = "发生序号";
            this.lbHappenNo.Visible = false;
            // 
            // lbInpatientNo
            // 
            this.lbInpatientNo.AutoSize = true;
            this.lbInpatientNo.ForeColor = System.Drawing.Color.Blue;
            this.lbInpatientNo.Location = new System.Drawing.Point(561, 16);
            this.lbInpatientNo.Name = "lbInpatientNo";
            this.lbInpatientNo.Size = new System.Drawing.Size(70, 14);
            this.lbInpatientNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbInpatientNo.TabIndex = 19;
            this.lbInpatientNo.Text = "neuLabel3";
            // 
            // cmbPatientKind
            // 
            this.cmbPatientKind.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbPatientKind.FormattingEnabled = true;
            this.cmbPatientKind.IsEnter2Tab = false;
            this.cmbPatientKind.IsFlat = false;
            this.cmbPatientKind.IsLike = true;
            this.cmbPatientKind.IsListOnly = false;
            this.cmbPatientKind.IsPopForm = true;
            this.cmbPatientKind.IsShowCustomerList = false;
            this.cmbPatientKind.IsShowID = false;
            this.cmbPatientKind.Location = new System.Drawing.Point(79, 11);
            this.cmbPatientKind.Name = "cmbPatientKind";
            this.cmbPatientKind.PopForm = null;
            this.cmbPatientKind.ShowCustomerList = false;
            this.cmbPatientKind.ShowID = false;
            this.cmbPatientKind.Size = new System.Drawing.Size(121, 22);
            this.cmbPatientKind.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbPatientKind.TabIndex = 16;
            this.cmbPatientKind.Tag = "";
            this.cmbPatientKind.ToolBarUse = false;
            this.cmbPatientKind.SelectedIndexChanged += new System.EventHandler(this.cmbPatientKind_SelectedIndexChanged);
            // 
            // lbPatientKind
            // 
            this.lbPatientKind.AutoSize = true;
            this.lbPatientKind.ForeColor = System.Drawing.Color.Blue;
            this.lbPatientKind.Location = new System.Drawing.Point(8, 14);
            this.lbPatientKind.Name = "lbPatientKind";
            this.lbPatientKind.Size = new System.Drawing.Size(70, 14);
            this.lbPatientKind.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbPatientKind.TabIndex = 15;
            this.lbPatientKind.Text = "患者类型:";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel2.Location = new System.Drawing.Point(378, 133);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(147, 14);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 14;
            this.neuLabel2.Text = "双击下面信息可做修改";
            // 
            // cmbDrug
            // 
            this.cmbDrug.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbDrug.FormattingEnabled = true;
            this.cmbDrug.IsEnter2Tab = false;
            this.cmbDrug.IsFlat = false;
            this.cmbDrug.IsLike = false;
            this.cmbDrug.IsListOnly = false;
            this.cmbDrug.IsPopForm = true;
            this.cmbDrug.IsShowCustomerList = false;
            this.cmbDrug.IsShowID = false;
            this.cmbDrug.Location = new System.Drawing.Point(549, 49);
            this.cmbDrug.Name = "cmbDrug";
            this.cmbDrug.PopForm = null;
            this.cmbDrug.ShowCustomerList = false;
            this.cmbDrug.ShowID = false;
            this.cmbDrug.Size = new System.Drawing.Size(145, 22);
            this.cmbDrug.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbDrug.TabIndex = 13;
            this.cmbDrug.Tag = "";
            this.cmbDrug.ToolBarUse = false;
            // 
            // cmbAllergyType
            // 
            this.cmbAllergyType.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbAllergyType.FormattingEnabled = true;
            this.cmbAllergyType.IsEnter2Tab = false;
            this.cmbAllergyType.IsFlat = false;
            this.cmbAllergyType.IsLike = true;
            this.cmbAllergyType.IsListOnly = false;
            this.cmbAllergyType.IsPopForm = true;
            this.cmbAllergyType.IsShowCustomerList = false;
            this.cmbAllergyType.IsShowID = false;
            this.cmbAllergyType.Location = new System.Drawing.Point(312, 49);
            this.cmbAllergyType.Name = "cmbAllergyType";
            this.cmbAllergyType.PopForm = null;
            this.cmbAllergyType.ShowCustomerList = false;
            this.cmbAllergyType.ShowID = false;
            this.cmbAllergyType.Size = new System.Drawing.Size(130, 22);
            this.cmbAllergyType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbAllergyType.TabIndex = 12;
            this.cmbAllergyType.Tag = "";
            this.cmbAllergyType.ToolBarUse = false;
            this.cmbAllergyType.SelectedIndexChanged += new System.EventHandler(this.cmbAllergyType_SelectedIndexChanged);
            // 
            // cmbAllergyDegree
            // 
            this.cmbAllergyDegree.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbAllergyDegree.FormattingEnabled = true;
            this.cmbAllergyDegree.IsEnter2Tab = false;
            this.cmbAllergyDegree.IsFlat = false;
            this.cmbAllergyDegree.IsLike = true;
            this.cmbAllergyDegree.IsListOnly = false;
            this.cmbAllergyDegree.IsPopForm = true;
            this.cmbAllergyDegree.IsShowCustomerList = false;
            this.cmbAllergyDegree.IsShowID = false;
            this.cmbAllergyDegree.Location = new System.Drawing.Point(79, 49);
            this.cmbAllergyDegree.Name = "cmbAllergyDegree";
            this.cmbAllergyDegree.PopForm = null;
            this.cmbAllergyDegree.ShowCustomerList = false;
            this.cmbAllergyDegree.ShowID = false;
            this.cmbAllergyDegree.Size = new System.Drawing.Size(121, 22);
            this.cmbAllergyDegree.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbAllergyDegree.TabIndex = 11;
            this.cmbAllergyDegree.Tag = "";
            this.cmbAllergyDegree.ToolBarUse = false;
            // 
            // neuButton1
            // 
            this.neuButton1.Location = new System.Drawing.Point(592, 126);
            this.neuButton1.Name = "neuButton1";
            this.neuButton1.Size = new System.Drawing.Size(102, 25);
            this.neuButton1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuButton1.TabIndex = 10;
            this.neuButton1.Text = "保   存";
            this.neuButton1.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuButton1.UseVisualStyleBackColor = true;
            this.neuButton1.Click += new System.EventHandler(this.neuButton1_Click);
            // 
            // neuCheckBox1
            // 
            this.neuCheckBox1.AutoSize = true;
            this.neuCheckBox1.Location = new System.Drawing.Point(11, 133);
            this.neuCheckBox1.Name = "neuCheckBox1";
            this.neuCheckBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.neuCheckBox1.Size = new System.Drawing.Size(82, 18);
            this.neuCheckBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuCheckBox1.TabIndex = 9;
            this.neuCheckBox1.Text = "是否作废";
            this.neuCheckBox1.UseVisualStyleBackColor = true;
            // 
            // neuTextBox1
            // 
            this.neuTextBox1.IsEnter2Tab = false;
            this.neuTextBox1.Location = new System.Drawing.Point(80, 89);
            this.neuTextBox1.MaxLength = 100;
            this.neuTextBox1.Name = "neuTextBox1";
            this.neuTextBox1.Size = new System.Drawing.Size(614, 23);
            this.neuTextBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTextBox1.TabIndex = 7;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(9, 92);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(77, 14);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 6;
            this.neuLabel1.Text = "备    注：";
            // 
            // lbAllergyType
            // 
            this.lbAllergyType.AutoSize = true;
            this.lbAllergyType.ForeColor = System.Drawing.Color.Blue;
            this.lbAllergyType.Location = new System.Drawing.Point(240, 52);
            this.lbAllergyType.Name = "lbAllergyType";
            this.lbAllergyType.Size = new System.Drawing.Size(70, 14);
            this.lbAllergyType.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbAllergyType.TabIndex = 4;
            this.lbAllergyType.Text = "过敏类型:";
            // 
            // lbAllergyDegree
            // 
            this.lbAllergyDegree.AutoSize = true;
            this.lbAllergyDegree.ForeColor = System.Drawing.Color.Blue;
            this.lbAllergyDegree.Location = new System.Drawing.Point(8, 52);
            this.lbAllergyDegree.Name = "lbAllergyDegree";
            this.lbAllergyDegree.Size = new System.Drawing.Size(70, 14);
            this.lbAllergyDegree.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbAllergyDegree.TabIndex = 2;
            this.lbAllergyDegree.Text = "过敏症状:";
            // 
            // lbDrug
            // 
            this.lbDrug.AutoSize = true;
            this.lbDrug.Location = new System.Drawing.Point(493, 52);
            this.lbDrug.Name = "lbDrug";
            this.lbDrug.Size = new System.Drawing.Size(56, 14);
            this.lbDrug.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbDrug.TabIndex = 1;
            this.lbDrug.Text = "过敏源:";
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.neuSpread1);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel1.Location = new System.Drawing.Point(0, 181);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(720, 213);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 3;
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
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(720, 213);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 1;
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
            this.neuSpread1_Sheet1.ColumnCount = 14;
            this.neuSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.Columns.Get(0).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(1).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 96F;
            this.neuSpread1_Sheet1.Columns.Get(2).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 40F;
            this.neuSpread1_Sheet1.Columns.Get(3).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 87F;
            this.neuSpread1_Sheet1.Columns.Get(4).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 48F;
            this.neuSpread1_Sheet1.Columns.Get(5).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 47F;
            this.neuSpread1_Sheet1.Columns.Get(6).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 106F;
            this.neuSpread1_Sheet1.Columns.Get(7).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(7).Width = 56F;
            this.neuSpread1_Sheet1.Columns.Get(8).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(8).Width = 76F;
            this.neuSpread1_Sheet1.Columns.Get(9).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(9).Width = 65F;
            this.neuSpread1_Sheet1.Columns.Get(10).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(10).Width = 70F;
            this.neuSpread1_Sheet1.Columns.Get(11).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(11).Width = 72F;
            this.neuSpread1_Sheet1.Columns.Get(12).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(12).Width = 109F;
            this.neuSpread1_Sheet1.Columns.Get(13).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(13).Width = 75F;
            this.neuSpread1_Sheet1.DefaultStyle.Locked = true;
            this.neuSpread1_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.neuSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.SheetCornerStyle.Locked = false;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ucAllergyIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Controls.Add(this.neuPanel1);
            this.Controls.Add(this.neuGroupBox1);
            this.Name = "ucAllergyIn";
            this.Size = new System.Drawing.Size(720, 394);
            this.Load += new System.EventHandler(this.ucAllergyIn_Load);
            this.neuGroupBox1.ResumeLayout(false);
            this.neuPanel2.ResumeLayout(false);
            this.neuPanel2.PerformLayout();
            this.neuPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbPatientKind;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDrug;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbAllergyType;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbAllergyDegree;
        private FS.FrameWork.WinForms.Controls.NeuButton neuButton1;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox neuCheckBox1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox neuTextBox1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbAllergyType;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbAllergyDegree;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbDrug;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbPatientKind;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbInpatientNo;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbHappenNo;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblPatientInfo;
    }
}
