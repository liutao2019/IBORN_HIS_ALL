namespace FS.WinForms.Report.OutpatientFee.Query
{
    partial class ucFeedetailPrintNew
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
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuCheckBox1 = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.endDate = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.beginDate = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.txtInvoiceNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.cbQueryType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuGroupBox1.SuspendLayout();
            this.neuPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.neuLabel1);
            this.neuGroupBox1.Controls.Add(this.neuCheckBox1);
            this.neuGroupBox1.Controls.Add(this.endDate);
            this.neuGroupBox1.Controls.Add(this.beginDate);
            this.neuGroupBox1.Controls.Add(this.txtInvoiceNo);
            this.neuGroupBox1.Controls.Add(this.cbQueryType);
            this.neuGroupBox1.Location = new System.Drawing.Point(3, 3);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(962, 86);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 0;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "查询条件";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(619, 37);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(17, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 5;
            this.neuLabel1.Text = "--";
            // 
            // neuCheckBox1
            // 
            this.neuCheckBox1.AutoSize = true;
            this.neuCheckBox1.Location = new System.Drawing.Point(441, 36);
            this.neuCheckBox1.Name = "neuCheckBox1";
            this.neuCheckBox1.Size = new System.Drawing.Size(15, 14);
            this.neuCheckBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuCheckBox1.TabIndex = 4;
            this.neuCheckBox1.UseVisualStyleBackColor = true;
            this.neuCheckBox1.CheckedChanged += new System.EventHandler(this.neuCheckBox1_CheckedChanged);
            // 
            // endDate
            // 
            this.endDate.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.endDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endDate.IsEnter2Tab = false;
            this.endDate.Location = new System.Drawing.Point(645, 32);
            this.endDate.Name = "endDate";
            this.endDate.Size = new System.Drawing.Size(139, 21);
            this.endDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.endDate.TabIndex = 3;
            // 
            // beginDate
            // 
            this.beginDate.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.beginDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.beginDate.IsEnter2Tab = false;
            this.beginDate.Location = new System.Drawing.Point(471, 32);
            this.beginDate.Name = "beginDate";
            this.beginDate.Size = new System.Drawing.Size(140, 21);
            this.beginDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.beginDate.TabIndex = 2;
            // 
            // txtInvoiceNo
            // 
            this.txtInvoiceNo.IsEnter2Tab = false;
            this.txtInvoiceNo.Location = new System.Drawing.Point(165, 32);
            this.txtInvoiceNo.Name = "txtInvoiceNo";
            this.txtInvoiceNo.Size = new System.Drawing.Size(148, 21);
            this.txtInvoiceNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtInvoiceNo.TabIndex = 1;
            this.txtInvoiceNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInvoiceNo_KeyDown);
            // 
            // cbQueryType
            // 
            this.cbQueryType.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cbQueryType.AutoCompleteCustomSource.AddRange(new string[] {
            "按发票",
            "病历号",
            "姓名"});
            this.cbQueryType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbQueryType.FormattingEnabled = true;
            this.cbQueryType.IsEnter2Tab = false;
            this.cbQueryType.IsFlat = false;
            this.cbQueryType.IsLike = true;
            this.cbQueryType.IsListOnly = false;
            this.cbQueryType.IsPopForm = true;
            this.cbQueryType.IsShowCustomerList = false;
            this.cbQueryType.IsShowID = false;
            this.cbQueryType.Items.AddRange(new object[] {
            "按发票",
            "病历号",
            "姓名"});
            this.cbQueryType.Location = new System.Drawing.Point(28, 32);
            this.cbQueryType.Name = "cbQueryType";
            this.cbQueryType.PopForm = null;
            this.cbQueryType.ShowCustomerList = false;
            this.cbQueryType.ShowID = false;
            this.cbQueryType.Size = new System.Drawing.Size(121, 20);
            this.cbQueryType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cbQueryType.TabIndex = 0;
            this.cbQueryType.Tag = "";
            this.cbQueryType.ToolBarUse = false;
            this.cbQueryType.SelectedIndexChanged += new System.EventHandler(this.cbQueryType_SelectedIndexChanged);
            // 
            // neuPanel1
            // 
            this.neuPanel1.BackColor = System.Drawing.Color.White;
            this.neuPanel1.Controls.Add(this.neuSpread1);
            this.neuPanel1.Location = new System.Drawing.Point(3, 95);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(799, 310);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 1;
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
            this.neuSpread1.Size = new System.Drawing.Size(799, 310);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ColumnCount = 7;
            this.neuSpread1_Sheet1.ColumnHeader.RowCount = 0;
            this.neuSpread1_Sheet1.RowCount = 10;
            this.neuSpread1_Sheet1.RowHeader.ColumnCount = 0;
            this.neuSpread1_Sheet1.Cells.Get(0, 0).BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.AutoText = FarPoint.Win.Spread.HeaderAutoText.Blank;
            this.neuSpread1_Sheet1.Columns.Get(0).BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.Columns.Get(0).Font = new System.Drawing.Font("宋体", 9F);
            this.neuSpread1_Sheet1.Columns.Get(0).NoteIndicatorColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 119F;
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 156F;
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 92F;
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 91F;
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 112F;
            this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.GroupBarBackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ucFeedetailPrintNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.neuPanel1);
            this.Controls.Add(this.neuGroupBox1);
            this.Name = "ucFeedetailPrintNew";
            this.Size = new System.Drawing.Size(968, 512);
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.neuPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cbQueryType;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtInvoiceNo;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker endDate;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker beginDate;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox neuCheckBox1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
    }
}
