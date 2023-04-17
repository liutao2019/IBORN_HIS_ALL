namespace SOC.Fee.DayBalance.Outpatient.FoSi
{
    partial class ucBalanceDetialByBankByDay
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
            FarPoint.Win.Spread.TipAppearance tipAppearance3 = new FarPoint.Win.Spread.TipAppearance();
            this.pnlCondition = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.pnlDayBalance = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.cmdDayBalance = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.rdbDate = new System.Windows.Forms.RadioButton();
            this.rdbDayBalance = new System.Windows.Forms.RadioButton();
            this.pnlTime = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.dtpStart = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.dtpEnd = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.pnlSumary = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.lblReportInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbltitle = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.pnlCondition.SuspendLayout();
            this.pnlDayBalance.SuspendLayout();
            this.neuPanel1.SuspendLayout();
            this.pnlTime.SuspendLayout();
            this.pnlSumary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.pnlCondition.Controls.Add(this.pnlDayBalance);
            this.pnlCondition.Controls.Add(this.neuPanel1);
            this.pnlCondition.Controls.Add(this.pnlTime);
            this.pnlCondition.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCondition.Location = new System.Drawing.Point(0, 0);
            this.pnlCondition.Name = "pnlCondition";
            this.pnlCondition.Size = new System.Drawing.Size(773, 49);
            this.pnlCondition.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlCondition.TabIndex = 1;
            // 
            // pnlDayBalance
            // 
            this.pnlDayBalance.Controls.Add(this.cmdDayBalance);
            this.pnlDayBalance.Controls.Add(this.neuLabel1);
            this.pnlDayBalance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDayBalance.Location = new System.Drawing.Point(277, 0);
            this.pnlDayBalance.Name = "pnlDayBalance";
            this.pnlDayBalance.Size = new System.Drawing.Size(496, 49);
            this.pnlDayBalance.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlDayBalance.TabIndex = 11;
            // 
            // cmdDayBalance
            // 
            this.cmdDayBalance.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmdDayBalance.FormattingEnabled = true;
            this.cmdDayBalance.IsEnter2Tab = false;
            this.cmdDayBalance.IsFlat = false;
            this.cmdDayBalance.IsLike = true;
            this.cmdDayBalance.IsListOnly = false;
            this.cmdDayBalance.IsPopForm = true;
            this.cmdDayBalance.IsShowCustomerList = false;
            this.cmdDayBalance.IsShowID = false;
            this.cmdDayBalance.Location = new System.Drawing.Point(76, 14);
            this.cmdDayBalance.Name = "cmdDayBalance";
            this.cmdDayBalance.PopForm = null;
            this.cmdDayBalance.ShowCustomerList = false;
            this.cmdDayBalance.ShowID = false;
            this.cmdDayBalance.Size = new System.Drawing.Size(346, 20);
            this.cmdDayBalance.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmdDayBalance.TabIndex = 8;
            this.cmdDayBalance.Tag = "";
            this.cmdDayBalance.ToolBarUse = false;
            this.cmdDayBalance.SelectedIndexChanged += new System.EventHandler(this.cmdDayBalance_SelectedIndexChanged);
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(5, 18);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(65, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 7;
            this.neuLabel1.Text = "日结记录：";
            this.neuLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.radioButton1);
            this.neuPanel1.Controls.Add(this.rdbDate);
            this.neuPanel1.Controls.Add(this.rdbDayBalance);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(277, 49);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 12;
            // 
            // rdbDate
            // 
            this.rdbDate.AutoSize = true;
            this.rdbDate.Location = new System.Drawing.Point(155, 16);
            this.rdbDate.Name = "rdbDate";
            this.rdbDate.Size = new System.Drawing.Size(119, 16);
            this.rdbDate.TabIndex = 13;
            this.rdbDate.Text = "按时间（已日结）";
            this.rdbDate.UseVisualStyleBackColor = true;
            this.rdbDate.CheckedChanged += new System.EventHandler(this.rdbDate_CheckedChanged);
            // 
            // rdbDayBalance
            // 
            this.rdbDayBalance.AutoSize = true;
            this.rdbDayBalance.Location = new System.Drawing.Point(14, 16);
            this.rdbDayBalance.Name = "rdbDayBalance";
            this.rdbDayBalance.Size = new System.Drawing.Size(59, 16);
            this.rdbDayBalance.TabIndex = 13;
            this.rdbDayBalance.Text = "按日结";
            this.rdbDayBalance.UseVisualStyleBackColor = true;
            this.rdbDayBalance.CheckedChanged += new System.EventHandler(this.rdbDate_CheckedChanged);
            // 
            // pnlTime
            // 
            this.pnlTime.Controls.Add(this.dtpStart);
            this.pnlTime.Controls.Add(this.dtpEnd);
            this.pnlTime.Controls.Add(this.neuLabel2);
            this.pnlTime.Controls.Add(this.neuLabel3);
            this.pnlTime.Location = new System.Drawing.Point(363, 6);
            this.pnlTime.Name = "pnlTime";
            this.pnlTime.Size = new System.Drawing.Size(449, 40);
            this.pnlTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlTime.TabIndex = 11;
            // 
            // dtpStart
            // 
            this.dtpStart.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStart.IsEnter2Tab = false;
            this.dtpStart.Location = new System.Drawing.Point(66, 14);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(146, 21);
            this.dtpStart.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpStart.TabIndex = 8;
            // 
            // dtpEnd
            // 
            this.dtpEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd.IsEnter2Tab = false;
            this.dtpEnd.Location = new System.Drawing.Point(286, 14);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(144, 21);
            this.dtpEnd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpEnd.TabIndex = 10;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(5, 18);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(65, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 7;
            this.neuLabel2.Text = "开始时间：";
            this.neuLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(225, 18);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(65, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 9;
            this.neuLabel3.Text = "结束时间：";
            this.neuLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlSumary
            // 
            this.pnlSumary.BackColor = System.Drawing.Color.White;
            this.pnlSumary.Controls.Add(this.neuSpread1);
            this.pnlSumary.Controls.Add(this.lblReportInfo);
            this.pnlSumary.Controls.Add(this.lbltitle);
            this.pnlSumary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSumary.Location = new System.Drawing.Point(0, 49);
            this.pnlSumary.Name = "pnlSumary";
            this.pnlSumary.Size = new System.Drawing.Size(773, 451);
            this.pnlSumary.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlSumary.TabIndex = 2;
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, ";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(0, 54);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(773, 397);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 4;
            tipAppearance3.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance3.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance3;
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
            this.neuSpread1_Sheet1.RowCount = 1;
            this.neuSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.neuSpread1_Sheet1.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
            this.neuSpread1_Sheet1.RowHeader.AutoText = FarPoint.Win.Spread.HeaderAutoText.Blank;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.neuSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 15F;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.Rows.Default.Height = 30F;
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.neuSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // lblReportInfo
            // 
            this.lblReportInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblReportInfo.Location = new System.Drawing.Point(0, 28);
            this.lblReportInfo.Name = "lblReportInfo";
            this.lblReportInfo.Size = new System.Drawing.Size(773, 26);
            this.lblReportInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblReportInfo.TabIndex = 3;
            // 
            // lbltitle
            // 
            this.lbltitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbltitle.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbltitle.Location = new System.Drawing.Point(0, 0);
            this.lbltitle.Name = "lbltitle";
            this.lbltitle.Size = new System.Drawing.Size(773, 28);
            this.lbltitle.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbltitle.TabIndex = 2;
            this.lbltitle.Text = "银行卡结算对照";
            this.lbltitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbltitle.UseCompatibleTextRendering = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(78, 16);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(71, 16);
            this.radioButton1.TabIndex = 14;
            this.radioButton1.Text = "按未日结";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // ucBalanceDetialByBankByDay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlSumary);
            this.Controls.Add(this.pnlCondition);
            this.Name = "ucBalanceDetialByBankByDay";
            this.Size = new System.Drawing.Size(773, 500);
            this.Load += new System.EventHandler(this.ucBalanceDetialByBankByDay_Load);
            this.pnlCondition.ResumeLayout(false);
            this.pnlDayBalance.ResumeLayout(false);
            this.pnlDayBalance.PerformLayout();
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.pnlTime.ResumeLayout(false);
            this.pnlTime.PerformLayout();
            this.pnlSumary.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected FS.FrameWork.WinForms.Controls.NeuPanel pnlCondition;
        public FS.FrameWork.WinForms.Controls.NeuPanel pnlTime;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpStart;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpEnd;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlSumary;
        public FS.FrameWork.WinForms.Controls.NeuLabel lblReportInfo;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbltitle;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private System.Windows.Forms.RadioButton rdbDate;
        private System.Windows.Forms.RadioButton rdbDayBalance;
        public FS.FrameWork.WinForms.Controls.NeuPanel pnlDayBalance;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmdDayBalance;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private System.Windows.Forms.RadioButton radioButton1;
    }
}
