namespace SOC.Fee.DayBalance.Outpatient
{
    partial class ucBalanceByMath
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
            this.pnlSumary = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dtpEnd = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.dtpStart = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.lbltitle = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cbsky = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lblSky = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.pnlSumary.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.neuPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSumary
            // 
            this.pnlSumary.BackColor = System.Drawing.Color.White;
            this.pnlSumary.Controls.Add(this.neuPanel2);
            this.pnlSumary.Controls.Add(this.neuPanel1);
            this.pnlSumary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSumary.Location = new System.Drawing.Point(0, 0);
            this.pnlSumary.Name = "pnlSumary";
            this.pnlSumary.Size = new System.Drawing.Size(862, 461);
            this.pnlSumary.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlSumary.TabIndex = 4;
            // 
            // neuPanel2
            // 
            this.neuPanel2.BackColor = System.Drawing.Color.White;
            this.neuPanel2.Controls.Add(this.neuSpread1);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel2.Location = new System.Drawing.Point(0, 71);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(862, 390);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 6;
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
            this.neuSpread1.Location = new System.Drawing.Point(0, 0);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(862, 390);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 5;
            tipAppearance3.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance3.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance3;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ColumnCount = 6;
            this.neuSpread1_Sheet1.RowCount = 1;
            this.neuSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "收退";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "总金额";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "实收金额";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "发票张数";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "发票起";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "发票止";
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "总金额";
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 164F;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "实收金额";
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 169F;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "发票张数";
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 127F;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "发票起";
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 146F;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "发票止";
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 131F;
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
            // neuPanel1
            // 
            this.neuPanel1.BackColor = System.Drawing.Color.White;
            this.neuPanel1.Controls.Add(this.cbsky);
            this.neuPanel1.Controls.Add(this.lblSky);
            this.neuPanel1.Controls.Add(this.neuLabel1);
            this.neuPanel1.Controls.Add(this.dtpEnd);
            this.neuPanel1.Controls.Add(this.dtpStart);
            this.neuPanel1.Controls.Add(this.lbltitle);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(862, 71);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 5;
            // 
            // neuLabel1
            // 
            this.neuLabel1.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel1.Location = new System.Drawing.Point(349, 22);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(27, 30);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 12;
            this.neuLabel1.Text = "至";
            this.neuLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.neuLabel1.UseCompatibleTextRendering = true;
            // 
            // dtpEnd
            // 
            this.dtpEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd.IsEnter2Tab = false;
            this.dtpEnd.Location = new System.Drawing.Point(409, 24);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(144, 21);
            this.dtpEnd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpEnd.TabIndex = 11;
            // 
            // dtpStart
            // 
            this.dtpStart.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStart.IsEnter2Tab = false;
            this.dtpStart.Location = new System.Drawing.Point(156, 24);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(146, 21);
            this.dtpStart.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpStart.TabIndex = 9;
            // 
            // lbltitle
            // 
            this.lbltitle.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbltitle.Location = new System.Drawing.Point(57, 24);
            this.lbltitle.Name = "lbltitle";
            this.lbltitle.Size = new System.Drawing.Size(93, 30);
            this.lbltitle.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbltitle.TabIndex = 3;
            this.lbltitle.Text = "统计时间：";
            this.lbltitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbltitle.UseCompatibleTextRendering = true;
            // 
            // cbsky
            // 
            this.cbsky.ArrowBackColor = System.Drawing.Color.Silver;
            this.cbsky.FormattingEnabled = true;
            this.cbsky.IsEnter2Tab = false;
            this.cbsky.IsFlat = false;
            this.cbsky.IsLike = true;
            this.cbsky.IsListOnly = false;
            this.cbsky.IsPopForm = true;
            this.cbsky.IsShowCustomerList = false;
            this.cbsky.IsShowID = false;
            this.cbsky.Location = new System.Drawing.Point(646, 23);
            this.cbsky.Name = "cbsky";
            this.cbsky.PopForm = null;
            this.cbsky.ShowCustomerList = false;
            this.cbsky.ShowID = false;
            this.cbsky.Size = new System.Drawing.Size(144, 20);
            this.cbsky.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cbsky.TabIndex = 14;
            this.cbsky.Tag = "";
            this.cbsky.ToolBarUse = false;
            // 
            // lblSky
            // 
            this.lblSky.AutoSize = true;
            this.lblSky.Location = new System.Drawing.Point(593, 28);
            this.lblSky.Name = "lblSky";
            this.lblSky.Size = new System.Drawing.Size(53, 12);
            this.lblSky.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblSky.TabIndex = 13;
            this.lblSky.Text = "收款员：";
            // 
            // ucBalanceByMath
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlSumary);
            this.Name = "ucBalanceByMath";
            this.Size = new System.Drawing.Size(862, 461);
            this.Load += new System.EventHandler(this.ucBalanceByMath_Load);
            this.pnlSumary.ResumeLayout(false);
            this.neuPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel pnlSumary;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbltitle;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpStart;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpEnd;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cbsky;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblSky;

    }
}
