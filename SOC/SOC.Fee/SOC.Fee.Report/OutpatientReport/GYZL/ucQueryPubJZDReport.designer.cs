namespace SOC.Fee.Report.OutpatientReport.GYZL
{
    partial class ucQueryPubJZDReport
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
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.checkBox1 = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.pnlPatientName = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.checkBox3 = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.cmbPatientName = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.pnlPatientType = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.checkBox2 = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.cmbPatientType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.endDate = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.beginDate = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lblInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblTitle = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPrint = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuGroupBox1.SuspendLayout();
            this.pnlPatientName.SuspendLayout();
            this.pnlPatientType.SuspendLayout();
            this.neuPanel1.SuspendLayout();
            this.neuPrint.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.BackColor = System.Drawing.Color.White;
            this.neuGroupBox1.Controls.Add(this.neuLabel2);
            this.neuGroupBox1.Controls.Add(this.checkBox1);
            this.neuGroupBox1.Controls.Add(this.pnlPatientName);
            this.neuGroupBox1.Controls.Add(this.pnlPatientType);
            this.neuGroupBox1.Controls.Add(this.endDate);
            this.neuGroupBox1.Controls.Add(this.beginDate);
            this.neuGroupBox1.Location = new System.Drawing.Point(3, 3);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(1121, 100);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 0;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "查询条件";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(41, 61);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(53, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 16;
            this.neuLabel2.Text = "结束日期";
            this.neuLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBox1
            // 
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(25, 29);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(75, 24);
            this.checkBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.checkBox1.TabIndex = 15;
            this.checkBox1.Text = "开始日期";
            // 
            // pnlPatientName
            // 
            this.pnlPatientName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pnlPatientName.Controls.Add(this.checkBox3);
            this.pnlPatientName.Controls.Add(this.cmbPatientName);
            this.pnlPatientName.Location = new System.Drawing.Point(550, 27);
            this.pnlPatientName.Name = "pnlPatientName";
            this.pnlPatientName.Size = new System.Drawing.Size(211, 49);
            this.pnlPatientName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlPatientName.TabIndex = 13;
            // 
            // checkBox3
            // 
            this.checkBox3.Location = new System.Drawing.Point(12, 4);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(79, 24);
            this.checkBox3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.checkBox3.TabIndex = 10;
            this.checkBox3.Text = "病人姓名";
            // 
            // cmbPatientName
            // 
            this.cmbPatientName.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbPatientName.FormattingEnabled = true;
            this.cmbPatientName.IsEnter2Tab = false;
            this.cmbPatientName.IsFlat = false;
            this.cmbPatientName.IsLike = true;
            this.cmbPatientName.IsListOnly = false;
            this.cmbPatientName.IsPopForm = true;
            this.cmbPatientName.IsShowCustomerList = false;
            this.cmbPatientName.IsShowID = false;
            this.cmbPatientName.Location = new System.Drawing.Point(97, 6);
            this.cmbPatientName.Name = "cmbPatientName";
            this.cmbPatientName.PopForm = null;
            this.cmbPatientName.ShowCustomerList = false;
            this.cmbPatientName.ShowID = false;
            this.cmbPatientName.Size = new System.Drawing.Size(101, 20);
            this.cmbPatientName.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbPatientName.TabIndex = 3;
            this.cmbPatientName.Tag = "";
            this.cmbPatientName.ToolBarUse = false;
            // 
            // pnlPatientType
            // 
            this.pnlPatientType.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pnlPatientType.Controls.Add(this.checkBox2);
            this.pnlPatientType.Controls.Add(this.cmbPatientType);
            this.pnlPatientType.Location = new System.Drawing.Point(293, 27);
            this.pnlPatientType.Name = "pnlPatientType";
            this.pnlPatientType.Size = new System.Drawing.Size(211, 49);
            this.pnlPatientType.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlPatientType.TabIndex = 13;
            // 
            // checkBox2
            // 
            this.checkBox2.Location = new System.Drawing.Point(12, 4);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(79, 24);
            this.checkBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.checkBox2.TabIndex = 10;
            this.checkBox2.Text = "病人类别";
            // 
            // cmbPatientType
            // 
            this.cmbPatientType.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbPatientType.FormattingEnabled = true;
            this.cmbPatientType.IsEnter2Tab = false;
            this.cmbPatientType.IsFlat = false;
            this.cmbPatientType.IsLike = true;
            this.cmbPatientType.IsListOnly = false;
            this.cmbPatientType.IsPopForm = true;
            this.cmbPatientType.IsShowCustomerList = false;
            this.cmbPatientType.IsShowID = false;
            this.cmbPatientType.Location = new System.Drawing.Point(97, 6);
            this.cmbPatientType.Name = "cmbPatientType";
            this.cmbPatientType.PopForm = null;
            this.cmbPatientType.ShowCustomerList = false;
            this.cmbPatientType.ShowID = false;
            this.cmbPatientType.Size = new System.Drawing.Size(101, 20);
            this.cmbPatientType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbPatientType.TabIndex = 3;
            this.cmbPatientType.Tag = "";
            this.cmbPatientType.ToolBarUse = false;
            // 
            // endDate
            // 
            this.endDate.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.endDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endDate.IsEnter2Tab = false;
            this.endDate.Location = new System.Drawing.Point(106, 57);
            this.endDate.Name = "endDate";
            this.endDate.Size = new System.Drawing.Size(140, 21);
            this.endDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.endDate.TabIndex = 1;
            // 
            // beginDate
            // 
            this.beginDate.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.beginDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.beginDate.IsEnter2Tab = false;
            this.beginDate.Location = new System.Drawing.Point(106, 30);
            this.beginDate.Name = "beginDate";
            this.beginDate.Size = new System.Drawing.Size(140, 21);
            this.beginDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.beginDate.TabIndex = 0;
            // 
            // neuPanel1
            // 
            this.neuPanel1.BackColor = System.Drawing.Color.White;
            this.neuPanel1.Controls.Add(this.lblInfo);
            this.neuPanel1.Controls.Add(this.lblTitle);
            this.neuPanel1.Location = new System.Drawing.Point(3, 3);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(1114, 48);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(7, 30);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(77, 12);
            this.lblInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "查询条件信息";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(544, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(29, 12);
            this.lblTitle.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "标题";
            // 
            // neuPrint
            // 
            this.neuPrint.AutoScroll = true;
            this.neuPrint.Controls.Add(this.neuSpread1);
            this.neuPrint.Controls.Add(this.neuPanel1);
            this.neuPrint.Location = new System.Drawing.Point(4, 109);
            this.neuPrint.Name = "neuPrint";
            this.neuPrint.Size = new System.Drawing.Size(1120, 429);
            this.neuPrint.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPrint.TabIndex = 4;
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, ";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.FileName = "";
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(3, 57);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(1114, 346);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 4;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance2;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(this.neuSpread1_ColumnWidthChanged);
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ColumnCount = 10;
            this.neuSpread1_Sheet1.RowCount = 10;
            this.neuSpread1_Sheet1.RowHeader.ColumnCount = 0;
            this.neuSpread1_Sheet1.Cells.Get(0, 0).BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ucQueryPubJZDReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.neuPrint);
            this.Controls.Add(this.neuGroupBox1);
            this.Name = "ucQueryPubJZDReport";
            this.Size = new System.Drawing.Size(1127, 541);
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.pnlPatientName.ResumeLayout(false);
            this.pnlPatientType.ResumeLayout(false);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.neuPrint.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker beginDate;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker endDate;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblInfo;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblTitle;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPrint;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        public FS.FrameWork.WinForms.Controls.NeuPanel pnlPatientType;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbPatientType;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox checkBox1;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox checkBox2;
        public FS.FrameWork.WinForms.Controls.NeuPanel pnlPatientName;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox checkBox3;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbPatientName;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
    }
}
