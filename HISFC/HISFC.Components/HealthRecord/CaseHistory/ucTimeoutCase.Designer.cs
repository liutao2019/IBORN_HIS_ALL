namespace FS.HISFC.Components.HealthRecord.CaseHistory
{
    partial class ucTimeoutCase
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
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuBtnExit = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuBtnPrint = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuBtnQuery = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuDateTimePicker2 = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuComDept = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuDateTimePicker1 = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel1.SuspendLayout();
            this.neuPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.neuPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.neuPanel3);
            this.neuPanel1.Controls.Add(this.neuPanel2);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(725, 443);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // neuPanel3
            // 
            this.neuPanel3.Controls.Add(this.neuSpread1);
            this.neuPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel3.Location = new System.Drawing.Point(0, 57);
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size(725, 386);
            this.neuPanel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 4;
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "2.5.2007.2005";
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
            this.neuSpread1.Size = new System.Drawing.Size(725, 386);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 3;
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
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "住院号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "姓名";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "出院日期";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "医生";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "回收日期";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "病区";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "回收天数";
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.ColumnHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.Black);
            this.neuSpread1_Sheet1.ColumnHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.Black);
            this.neuSpread1_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "住院号";
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 73F;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "姓名";
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 83F;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "出院日期";
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 86F;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "医生";
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 80F;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "回收日期";
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 80F;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "病区";
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 140F;
            this.neuSpread1_Sheet1.Columns.Get(6).Label = "回收天数";
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 68F;
            this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.Black);
            this.neuSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.neuSpread1_Sheet1.RowHeader.Visible = false;
            this.neuSpread1_Sheet1.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.Black);
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuSpread1.SetActiveViewport(1, 0);
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.neuBtnExit);
            this.neuPanel2.Controls.Add(this.neuBtnPrint);
            this.neuPanel2.Controls.Add(this.neuBtnQuery);
            this.neuPanel2.Controls.Add(this.neuDateTimePicker2);
            this.neuPanel2.Controls.Add(this.neuLabel2);
            this.neuPanel2.Controls.Add(this.neuComDept);
            this.neuPanel2.Controls.Add(this.neuDateTimePicker1);
            this.neuPanel2.Controls.Add(this.neuLabel1);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel2.Location = new System.Drawing.Point(0, 0);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(725, 57);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 2;
            // 
            // neuBtnExit
            // 
            this.neuBtnExit.Enabled = false;
            this.neuBtnExit.Location = new System.Drawing.Point(638, 31);
            this.neuBtnExit.Name = "neuBtnExit";
            this.neuBtnExit.Size = new System.Drawing.Size(75, 23);
            this.neuBtnExit.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuBtnExit.TabIndex = 8;
            this.neuBtnExit.Text = "退出(Esc)";
            this.neuBtnExit.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuBtnExit.UseVisualStyleBackColor = true;
            this.neuBtnExit.Visible = false;
            this.neuBtnExit.Click += new System.EventHandler(this.neuBtnExit_Click);
            // 
            // neuBtnPrint
            // 
            this.neuBtnPrint.Location = new System.Drawing.Point(347, 31);
            this.neuBtnPrint.Name = "neuBtnPrint";
            this.neuBtnPrint.Size = new System.Drawing.Size(75, 23);
            this.neuBtnPrint.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuBtnPrint.TabIndex = 7;
            this.neuBtnPrint.Text = "打印(P)";
            this.neuBtnPrint.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuBtnPrint.UseVisualStyleBackColor = true;
            this.neuBtnPrint.Click += new System.EventHandler(this.neuBtnPrint_Click);
            // 
            // neuBtnQuery
            // 
            this.neuBtnQuery.Location = new System.Drawing.Point(266, 31);
            this.neuBtnQuery.Name = "neuBtnQuery";
            this.neuBtnQuery.Size = new System.Drawing.Size(75, 23);
            this.neuBtnQuery.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuBtnQuery.TabIndex = 6;
            this.neuBtnQuery.Text = "查询";
            this.neuBtnQuery.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuBtnQuery.UseVisualStyleBackColor = true;
            this.neuBtnQuery.Click += new System.EventHandler(this.neuBtnQuery_Click);
            // 
            // neuDateTimePicker2
            // 
            this.neuDateTimePicker2.CustomFormat = "yyyy/MM/dd 00:00:00";
            this.neuDateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.neuDateTimePicker2.IsEnter2Tab = false;
            this.neuDateTimePicker2.Location = new System.Drawing.Point(155, 32);
            this.neuDateTimePicker2.Name = "neuDateTimePicker2";
            this.neuDateTimePicker2.Size = new System.Drawing.Size(85, 21);
            this.neuDateTimePicker2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuDateTimePicker2.TabIndex = 5;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(3, 36);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(53, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 4;
            this.neuLabel2.Text = "出院日期";
            // 
            // neuComDept
            // 
            this.neuComDept.ArrowBackColor = System.Drawing.Color.Silver;
            this.neuComDept.FormattingEnabled = true;
            this.neuComDept.IsEnter2Tab = false;
            this.neuComDept.IsFlat = true;
            this.neuComDept.IsLike = true;
            this.neuComDept.Location = new System.Drawing.Point(61, 4);
            this.neuComDept.Name = "neuComDept";
            this.neuComDept.PopForm = null;
            this.neuComDept.ShowCustomerList = false;
            this.neuComDept.ShowID = false;
            this.neuComDept.Size = new System.Drawing.Size(179, 20);
            this.neuComDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuComDept.TabIndex = 3;
            this.neuComDept.Tag = "";
            this.neuComDept.ToolBarUse = false;
            // 
            // neuDateTimePicker1
            // 
            this.neuDateTimePicker1.CustomFormat = "yyyy/MM/dd 00:00:00";
            this.neuDateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.neuDateTimePicker1.IsEnter2Tab = false;
            this.neuDateTimePicker1.Location = new System.Drawing.Point(61, 32);
            this.neuDateTimePicker1.Name = "neuDateTimePicker1";
            this.neuDateTimePicker1.Size = new System.Drawing.Size(88, 21);
            this.neuDateTimePicker1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuDateTimePicker1.TabIndex = 2;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(26, 7);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(29, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 1;
            this.neuLabel1.Text = "科室";
            // 
            // ucTimeoutCase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucTimeoutCase";
            this.Size = new System.Drawing.Size(725, 443);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.neuPanel2.ResumeLayout(false);
            this.neuPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker neuDateTimePicker2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuComboBox neuComDept;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker neuDateTimePicker1;
        private FS.FrameWork.WinForms.Controls.NeuButton neuBtnQuery;
        private FS.FrameWork.WinForms.Controls.NeuButton neuBtnExit;
        private FS.FrameWork.WinForms.Controls.NeuButton neuBtnPrint;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel3;
    }
}
