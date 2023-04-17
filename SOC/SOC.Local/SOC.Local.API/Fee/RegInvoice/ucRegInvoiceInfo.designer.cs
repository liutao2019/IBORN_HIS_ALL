namespace FS.SOC.Local.API.Fee.RegInvoice
{
    partial class ucQueryInvoiceInfoByStartEnd
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
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.rbInvoice = new System.Windows.Forms.RadioButton();
            this.rdTime = new System.Windows.Forms.RadioButton();
            this.tbInvoiceEnd = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.tbInvoiceStart = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.endDate = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.beginDate = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuPrint = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            this.neuPrint.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.BackColor = System.Drawing.Color.White;
            this.neuGroupBox1.Controls.Add(this.rbInvoice);
            this.neuGroupBox1.Controls.Add(this.rdTime);
            this.neuGroupBox1.Controls.Add(this.tbInvoiceEnd);
            this.neuGroupBox1.Controls.Add(this.tbInvoiceStart);
            this.neuGroupBox1.Controls.Add(this.neuLabel4);
            this.neuGroupBox1.Controls.Add(this.neuLabel1);
            this.neuGroupBox1.Controls.Add(this.endDate);
            this.neuGroupBox1.Controls.Add(this.beginDate);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(800, 100);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 0;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "查询条件";
            // 
            // rbInvoice
            // 
            this.rbInvoice.AutoSize = true;
            this.rbInvoice.Location = new System.Drawing.Point(30, 62);
            this.rbInvoice.Name = "rbInvoice";
            this.rbInvoice.Size = new System.Drawing.Size(95, 16);
            this.rbInvoice.TabIndex = 19;
            this.rbInvoice.Text = "发票起止号：";
            this.rbInvoice.UseVisualStyleBackColor = true;
            // 
            // rdTime
            // 
            this.rdTime.AutoSize = true;
            this.rdTime.Checked = true;
            this.rdTime.Location = new System.Drawing.Point(30, 29);
            this.rdTime.Name = "rdTime";
            this.rdTime.Size = new System.Drawing.Size(83, 16);
            this.rdTime.TabIndex = 18;
            this.rdTime.TabStop = true;
            this.rdTime.Text = "起止时间：";
            this.rdTime.UseVisualStyleBackColor = true;
            // 
            // tbInvoiceEnd
            // 
            this.tbInvoiceEnd.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbInvoiceEnd.IsEnter2Tab = false;
            this.tbInvoiceEnd.Location = new System.Drawing.Point(261, 58);
            this.tbInvoiceEnd.Name = "tbInvoiceEnd";
            this.tbInvoiceEnd.Size = new System.Drawing.Size(100, 21);
            this.tbInvoiceEnd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbInvoiceEnd.TabIndex = 2;
            // 
            // tbInvoiceStart
            // 
            this.tbInvoiceStart.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbInvoiceStart.IsEnter2Tab = false;
            this.tbInvoiceStart.Location = new System.Drawing.Point(132, 58);
            this.tbInvoiceStart.Name = "tbInvoiceStart";
            this.tbInvoiceStart.Size = new System.Drawing.Size(100, 21);
            this.tbInvoiceStart.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbInvoiceStart.TabIndex = 1;
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point(238, 62);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(17, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 2;
            this.neuLabel4.Text = "--";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(238, 28);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(17, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 2;
            this.neuLabel1.Text = "--";
            this.neuLabel1.Visible = false;
            // 
            // endDate
            // 
            this.endDate.CustomFormat = "yyyy-MM-dd 23:59:59";
            this.endDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endDate.IsEnter2Tab = false;
            this.endDate.Location = new System.Drawing.Point(261, 24);
            this.endDate.Name = "endDate";
            this.endDate.Size = new System.Drawing.Size(100, 21);
            this.endDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.endDate.TabIndex = 1;
            // 
            // beginDate
            // 
            this.beginDate.CustomFormat = "yyyy-MM-dd 00:00:00";
            this.beginDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.beginDate.IsEnter2Tab = false;
            this.beginDate.Location = new System.Drawing.Point(132, 24);
            this.beginDate.Name = "beginDate";
            this.beginDate.Size = new System.Drawing.Size(100, 21);
            this.beginDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.beginDate.TabIndex = 0;
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
            this.neuSpread1.Size = new System.Drawing.Size(800, 300);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 4;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(this.neuSpread1_ColumnWidthChanged);
            // 
            // neuPrint
            // 
            this.neuPrint.AutoScroll = true;
            this.neuPrint.Controls.Add(this.neuSpread1);
            this.neuPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPrint.Location = new System.Drawing.Point(0, 100);
            this.neuPrint.Name = "neuPrint";
            this.neuPrint.Size = new System.Drawing.Size(800, 300);
            this.neuPrint.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPrint.TabIndex = 4;
            // 
            // ucQueryInvoiceInfoByStartEnd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.neuPrint);
            this.Controls.Add(this.neuGroupBox1);
            this.Name = "ucQueryInvoiceInfoByStartEnd";
            this.Size = new System.Drawing.Size(800, 400);
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            this.neuPrint.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker beginDate;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker endDate;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox tbInvoiceStart;
        private FS.FrameWork.WinForms.Controls.NeuTextBox tbInvoiceEnd;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPrint;
        private System.Windows.Forms.RadioButton rdTime;
        private System.Windows.Forms.RadioButton rbInvoice;
    }
}
