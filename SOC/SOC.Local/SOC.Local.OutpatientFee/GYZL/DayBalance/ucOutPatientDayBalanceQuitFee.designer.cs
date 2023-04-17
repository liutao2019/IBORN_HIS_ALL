namespace SOC.Fee.Report.OutpatientReport.GYZL
{
    partial class ucOutPatientDayBalanceQuitFee
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
            this.pnlEmployee = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.cmbFeeOper = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.endDate = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.beginDate = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lblInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblTitle = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPrint = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuSpread1_Sheet2 = new FarPoint.Win.Spread.SheetView();
            this.neuGroupBox1.SuspendLayout();
            this.pnlEmployee.SuspendLayout();
            this.neuPanel1.SuspendLayout();
            this.neuPrint.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet2)).BeginInit();
            this.SuspendLayout();
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.BackColor = System.Drawing.Color.White;
            this.neuGroupBox1.Controls.Add(this.pnlEmployee);
            this.neuGroupBox1.Controls.Add(this.neuLabel1);
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
            // pnlEmployee
            // 
            this.pnlEmployee.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pnlEmployee.Controls.Add(this.cmbFeeOper);
            this.pnlEmployee.Controls.Add(this.neuLabel2);
            this.pnlEmployee.Location = new System.Drawing.Point(463, 30);
            this.pnlEmployee.Name = "pnlEmployee";
            this.pnlEmployee.Size = new System.Drawing.Size(185, 49);
            this.pnlEmployee.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlEmployee.TabIndex = 13;
            this.pnlEmployee.Visible = false;
            // 
            // cmbFeeOper
            // 
            this.cmbFeeOper.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbFeeOper.FormattingEnabled = true;
            this.cmbFeeOper.IsEnter2Tab = false;
            this.cmbFeeOper.IsFlat = false;
            this.cmbFeeOper.IsLike = true;
            this.cmbFeeOper.IsListOnly = false;
            this.cmbFeeOper.IsPopForm = true;
            this.cmbFeeOper.IsShowCustomerList = false;
            this.cmbFeeOper.IsShowID = false;
            this.cmbFeeOper.Location = new System.Drawing.Point(63, 14);
            this.cmbFeeOper.Name = "cmbFeeOper";
            this.cmbFeeOper.PopForm = null;
            this.cmbFeeOper.ShowCustomerList = false;
            this.cmbFeeOper.ShowID = false;
            this.cmbFeeOper.Size = new System.Drawing.Size(101, 20);
            this.cmbFeeOper.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbFeeOper.TabIndex = 3;
            this.cmbFeeOper.Tag = "";
            this.cmbFeeOper.ToolBarUse = false;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(2, 18);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(53, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 9;
            this.neuLabel2.Text = "收费员：";
            this.neuLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(212, 46);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(17, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 2;
            this.neuLabel1.Text = "--";
            // 
            // endDate
            // 
            this.endDate.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.endDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endDate.IsEnter2Tab = false;
            this.endDate.Location = new System.Drawing.Point(236, 42);
            this.endDate.Name = "endDate";
            this.endDate.Size = new System.Drawing.Size(168, 21);
            this.endDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.endDate.TabIndex = 1;
            // 
            // beginDate
            // 
            this.beginDate.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.beginDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.beginDate.IsEnter2Tab = false;
            this.beginDate.Location = new System.Drawing.Point(33, 42);
            this.beginDate.Name = "beginDate";
            this.beginDate.Size = new System.Drawing.Size(168, 21);
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
            this.lblInfo.Location = new System.Drawing.Point(426, 31);
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
            this.neuSpread1.AccessibleDescription = "neuSpread1, 明细报表, Row 0, Column 0, ";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.FileName = "";
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(3, 57);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1,
            this.neuSpread1_Sheet2});
            this.neuSpread1.Size = new System.Drawing.Size(1114, 346);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 4;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.neuSpread1_CellDoubleClick);
            this.neuSpread1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(this.neuSpread1_ColumnWidthChanged);
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "退费缴款报表";
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
            // neuSpread1_Sheet2
            // 
            this.neuSpread1_Sheet2.Reset();
            this.neuSpread1_Sheet2.SheetName = "明细报表";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet2.RowHeader.Columns.Default.Resizable = true;
            this.neuSpread1_Sheet2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ucOutPatientDayBalanceQuitFee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.neuPrint);
            this.Controls.Add(this.neuGroupBox1);
            this.Name = "ucOutPatientDayBalanceQuitFee";
            this.Size = new System.Drawing.Size(1127, 541);
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.pnlEmployee.ResumeLayout(false);
            this.pnlEmployee.PerformLayout();
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.neuPrint.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker beginDate;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker endDate;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblInfo;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblTitle;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPrint;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        public FS.FrameWork.WinForms.Controls.NeuPanel pnlEmployee;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbFeeOper;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet2;
    }
}
