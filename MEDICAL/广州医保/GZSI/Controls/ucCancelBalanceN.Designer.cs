namespace GZSI.Controls
{
    partial class ucCancelBalanceN
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
            System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("zh-CN", false);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucCancelBalanceN));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            this.pnPatientInfo = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.dtEnd = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.dtBegin = new System.Windows.Forms.DateTimePicker();
            this.lblPInfo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCardNO = new System.Windows.Forms.Label();
            this.txtName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtCardNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDetailTitle = new System.Windows.Forms.Label();
            this.fpSpread1 = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.fpSpread2 = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.sheetView1 = new FarPoint.Win.Spread.SheetView();
            this.pnPatientInfo.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetView1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnPatientInfo
            // 
            this.pnPatientInfo.Controls.Add(this.label2);
            this.pnPatientInfo.Controls.Add(this.dtEnd);
            this.pnPatientInfo.Controls.Add(this.lblDate);
            this.pnPatientInfo.Controls.Add(this.dtBegin);
            this.pnPatientInfo.Controls.Add(this.lblPInfo);
            this.pnPatientInfo.Controls.Add(this.label1);
            this.pnPatientInfo.Controls.Add(this.lblCardNO);
            this.pnPatientInfo.Controls.Add(this.txtName);
            this.pnPatientInfo.Controls.Add(this.txtCardNO);
            this.pnPatientInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnPatientInfo.Location = new System.Drawing.Point(0, 0);
            this.pnPatientInfo.Name = "pnPatientInfo";
            this.pnPatientInfo.Size = new System.Drawing.Size(1093, 65);
            this.pnPatientInfo.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(599, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "-";
            // 
            // dtEnd
            // 
            this.dtEnd.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtEnd.Location = new System.Drawing.Point(617, 12);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(120, 21);
            this.dtEnd.TabIndex = 16;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.BackColor = System.Drawing.Color.Transparent;
            this.lblDate.ForeColor = System.Drawing.Color.Blue;
            this.lblDate.Location = new System.Drawing.Point(411, 17);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(53, 12);
            this.lblDate.TabIndex = 15;
            this.lblDate.Text = "时间范围";
            // 
            // dtBegin
            // 
            this.dtBegin.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtBegin.Location = new System.Drawing.Point(470, 12);
            this.dtBegin.Name = "dtBegin";
            this.dtBegin.Size = new System.Drawing.Size(120, 21);
            this.dtBegin.TabIndex = 8;
            // 
            // lblPInfo
            // 
            this.lblPInfo.AutoSize = true;
            this.lblPInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblPInfo.ForeColor = System.Drawing.Color.Black;
            this.lblPInfo.Location = new System.Drawing.Point(12, 44);
            this.lblPInfo.Name = "lblPInfo";
            this.lblPInfo.Size = new System.Drawing.Size(53, 12);
            this.lblPInfo.TabIndex = 14;
            this.lblPInfo.Text = "患者信息";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(218, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "姓名";
            // 
            // lblCardNO
            // 
            this.lblCardNO.AutoSize = true;
            this.lblCardNO.BackColor = System.Drawing.Color.Transparent;
            this.lblCardNO.ForeColor = System.Drawing.Color.Blue;
            this.lblCardNO.Location = new System.Drawing.Point(12, 17);
            this.lblCardNO.Name = "lblCardNO";
            this.lblCardNO.Size = new System.Drawing.Size(71, 12);
            this.lblCardNO.TabIndex = 12;
            this.lblCardNO.Text = "卡号\\病历号";
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtName.IsEnter2Tab = false;
            this.txtName.Location = new System.Drawing.Point(253, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(123, 23);
            this.txtName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtName.TabIndex = 11;
            this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
            // 
            // txtCardNO
            // 
            this.txtCardNO.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCardNO.IsEnter2Tab = false;
            this.txtCardNO.Location = new System.Drawing.Point(89, 12);
            this.txtCardNO.Name = "txtCardNO";
            this.txtCardNO.Size = new System.Drawing.Size(115, 23);
            this.txtCardNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCardNO.TabIndex = 1;
            this.txtCardNO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCardNO_KeyDown);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 65);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.fpSpread1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.fpSpread2);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(1093, 441);
            this.splitContainer1.SplitterDistance = 500;
            this.splitContainer1.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblDetailTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(589, 48);
            this.panel1.TabIndex = 6;
            // 
            // lblDetailTitle
            // 
            this.lblDetailTitle.AutoSize = true;
            this.lblDetailTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblDetailTitle.ForeColor = System.Drawing.Color.Black;
            this.lblDetailTitle.Location = new System.Drawing.Point(9, 8);
            this.lblDetailTitle.Name = "lblDetailTitle";
            this.lblDetailTitle.Size = new System.Drawing.Size(389, 12);
            this.lblDetailTitle.TabIndex = 18;
            this.lblDetailTitle.Text = "科室、看诊医生、结算时间、就医登记号、总金额、自费金额、记账金额";
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "3.0.2004.2005";
            this.fpSpread1.AccessibleDescription = "fpSpread1, Sheet1, Row 0, Column 0, 司马无敌";
            this.fpSpread1.BackColor = System.Drawing.SystemColors.Control;
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread1.Location = new System.Drawing.Point(0, 0);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1});
            this.fpSpread1.Size = new System.Drawing.Size(500, 441);
            this.fpSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpSpread1.TabIndex = 5;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread1.TextTipAppearance = tipAppearance1;
            this.fpSpread1.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(this.fpSpread1_SelectionChanged);
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.ColumnCount = 8;
            this.fpSpread1_Sheet1.RowCount = 1;
            this.fpSpread1_Sheet1.Cells.Get(0, 0).Value = "司马无敌";
            this.fpSpread1_Sheet1.Cells.Get(0, 1).Value = "爱博恩妇产科急诊";
            this.fpSpread1_Sheet1.Cells.Get(0, 5).ParseFormatInfo = ((System.Globalization.DateTimeFormatInfo)(cultureInfo.DateTimeFormat.Clone()));
            ((System.Globalization.DateTimeFormatInfo)(this.fpSpread1_Sheet1.Cells.Get(0, 5).ParseFormatInfo)).DateSeparator = "-";
            ((System.Globalization.DateTimeFormatInfo)(this.fpSpread1_Sheet1.Cells.Get(0, 5).ParseFormatInfo)).FirstDayOfWeek = System.DayOfWeek.Monday;
            ((System.Globalization.DateTimeFormatInfo)(this.fpSpread1_Sheet1.Cells.Get(0, 5).ParseFormatInfo)).FullDateTimePattern = "yyyy\'年\'M\'月\'d\'日\' HH:mm:ss";
            ((System.Globalization.DateTimeFormatInfo)(this.fpSpread1_Sheet1.Cells.Get(0, 5).ParseFormatInfo)).LongTimePattern = "HH:mm:ss";
            ((System.Globalization.DateTimeFormatInfo)(this.fpSpread1_Sheet1.Cells.Get(0, 5).ParseFormatInfo)).ShortDatePattern = "yyyy-MM-dd";
            this.fpSpread1_Sheet1.Cells.Get(0, 5).ParseFormatString = "yyyy\'年\'M\'月\'d\'日\'";
            this.fpSpread1_Sheet1.Cells.Get(0, 5).Value = new System.DateTime(2018, 7, 23, 0, 0, 0, 0);
            this.fpSpread1_Sheet1.Cells.Get(0, 6).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.fpSpread1_Sheet1.Cells.Get(0, 6).ParseFormatInfo)).CurrencySymbol = "¥";
            ((System.Globalization.NumberFormatInfo)(this.fpSpread1_Sheet1.Cells.Get(0, 6).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.fpSpread1_Sheet1.Cells.Get(0, 6).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.fpSpread1_Sheet1.Cells.Get(0, 6).ParseFormatString = "E";
            this.fpSpread1_Sheet1.Cells.Get(0, 6).Value = "0061811807361355";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "姓名";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "科室";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "总金额";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "报销金额";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "自费金额";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "结算日期";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "就医登记号";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "结算批次";
            this.fpSpread1_Sheet1.ColumnHeader.Rows.Get(0).Height = 25F;
            this.fpSpread1_Sheet1.Columns.Get(1).Label = "科室";
            this.fpSpread1_Sheet1.Columns.Get(1).Width = 106F;
            this.fpSpread1_Sheet1.Columns.Get(5).Label = "结算日期";
            this.fpSpread1_Sheet1.Columns.Get(5).Width = 100F;
            this.fpSpread1_Sheet1.Columns.Get(6).CellType = textCellType1;
            this.fpSpread1_Sheet1.Columns.Get(6).Label = "就医登记号";
            this.fpSpread1_Sheet1.Columns.Get(6).Width = 106F;
            this.fpSpread1_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpSpread1_Sheet1.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.Rows.Default.Height = 25F;
            this.fpSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // fpSpread2
            // 
            this.fpSpread2.About = "3.0.2004.2005";
            this.fpSpread2.AccessibleDescription = "fpSpread2, Sheet1, Row 0, Column 0, ";
            this.fpSpread2.BackColor = System.Drawing.SystemColors.Control;
            this.fpSpread2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread2.Location = new System.Drawing.Point(0, 48);
            this.fpSpread2.Name = "fpSpread2";
            this.fpSpread2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread2.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.sheetView1});
            this.fpSpread2.Size = new System.Drawing.Size(589, 393);
            this.fpSpread2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpSpread2.TabIndex = 5;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread2.TextTipAppearance = tipAppearance2;
            // 
            // sheetView1
            // 
            this.sheetView1.Reset();
            this.sheetView1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.sheetView1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.sheetView1.ColumnCount = 7;
            this.sheetView1.RowCount = 1;
            this.sheetView1.ColumnHeader.Cells.Get(0, 0).Value = "项目编号";
            this.sheetView1.ColumnHeader.Cells.Get(0, 1).Value = "项目编码";
            this.sheetView1.ColumnHeader.Cells.Get(0, 2).Value = "项目名称";
            this.sheetView1.ColumnHeader.Cells.Get(0, 3).Value = "规格";
            this.sheetView1.ColumnHeader.Cells.Get(0, 4).Value = "单价";
            this.sheetView1.ColumnHeader.Cells.Get(0, 5).Value = "数量";
            this.sheetView1.ColumnHeader.Cells.Get(0, 6).Value = "金额";
            this.sheetView1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.sheetView1.Columns.Get(0).Label = "项目编号";
            this.sheetView1.Columns.Get(0).Width = 73F;
            this.sheetView1.Columns.Get(1).Label = "项目编码";
            this.sheetView1.Columns.Get(1).Width = 89F;
            this.sheetView1.Columns.Get(2).Label = "项目名称";
            this.sheetView1.Columns.Get(2).Width = 198F;
            this.sheetView1.Columns.Get(3).Label = "规格";
            this.sheetView1.Columns.Get(3).Width = 85F;
            this.sheetView1.Columns.Get(5).Label = "数量";
            this.sheetView1.Columns.Get(5).Width = 62F;
            this.sheetView1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.sheetView1.RowHeader.Columns.Default.Resizable = true;
            this.sheetView1.RowHeader.Columns.Get(0).Width = 24F;
            this.sheetView1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.sheetView1.Rows.Default.Height = 25F;
            this.sheetView1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.sheetView1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.sheetView1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.sheetView1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ucCancelBalanceN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.pnPatientInfo);
            this.Name = "ucCancelBalanceN";
            this.Size = new System.Drawing.Size(1093, 506);
            this.pnPatientInfo.ResumeLayout(false);
            this.pnPatientInfo.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnPatientInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtEnd;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dtBegin;
        private System.Windows.Forms.Label lblPInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCardNO;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox txtName;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox txtCardNO;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private FS.SOC.Windows.Forms.FpSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
        private FS.SOC.Windows.Forms.FpSpread fpSpread2;
        private FarPoint.Win.Spread.SheetView sheetView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblDetailTitle;
    }
}
