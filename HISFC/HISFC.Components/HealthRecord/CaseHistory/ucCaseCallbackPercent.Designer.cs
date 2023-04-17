namespace FS.HISFC.Components.HealthRecord.CaseHistory
{
    partial class ucCaseCallbackPercent
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
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.btExportDetial = new System.Windows.Forms.Button();
            this.neuBtnTimeOut = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuBtnExport = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuBtnExit = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuBtnPrint = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuBtnQuery = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuDateTimePicker2 = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuDateTimePicker1 = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.neuPanel2.SuspendLayout();
            this.neuPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.panel1);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel1.Location = new System.Drawing.Point(0, 47);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(776, 512);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(776, 512);
            this.panel1.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.fpSpread1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 353);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(776, 159);
            this.panel3.TabIndex = 1;
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "3.0.2004.2005";
            this.fpSpread1.AccessibleDescription = "fpSpread1, Sheet1";
            this.fpSpread1.BackColor = System.Drawing.SystemColors.Control;
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread1.Location = new System.Drawing.Point(0, 0);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1});
            this.fpSpread1.Size = new System.Drawing.Size(776, 159);
            this.fpSpread1.TabIndex = 1;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread1.TextTipAppearance = tipAppearance1;
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.ColumnCount = 7;
            this.fpSpread1_Sheet1.RowCount = 0;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "住院号";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "患者姓名";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "医生姓名";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "出院日期";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "回收人员";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "回收日期";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "回收率类型";
            this.fpSpread1_Sheet1.Columns.Get(3).Label = "出院日期";
            this.fpSpread1_Sheet1.Columns.Get(3).Width = 71F;
            this.fpSpread1_Sheet1.Columns.Get(5).Label = "回收日期";
            this.fpSpread1_Sheet1.Columns.Get(5).Width = 77F;
            this.fpSpread1_Sheet1.Columns.Get(6).Label = "回收率类型";
            this.fpSpread1_Sheet1.Columns.Get(6).Width = 67F;
            this.fpSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpSpread1.SetActiveViewport(0, 1, 0);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.neuSpread1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(776, 353);
            this.panel2.TabIndex = 0;
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1";
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
            this.neuSpread1.Size = new System.Drawing.Size(776, 353);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 0;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance2;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.neuSpread1_CellDoubleClick);
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ColumnCount = 8;
            this.neuSpread1_Sheet1.ColumnHeader.RowCount = 2;
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "科室";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "总病历数";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "7天回收率";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "超7天病历数及罚款金额";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "10天回收率";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "超10天病历数及罚款金额";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "总罚款金额";
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.CellType = textCellType1;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.ColumnHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.Black, System.Drawing.SystemColors.ControlLightLight, System.Drawing.SystemColors.ControlLightLight);
            this.neuSpread1_Sheet1.ColumnHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.Black, System.Drawing.SystemColors.ControlLightLight, System.Drawing.SystemColors.ActiveCaptionText);
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 155F;
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 68F;
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 139F;
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 72F;
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 149F;
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 71F;
            this.neuSpread1_Sheet1.Columns.Get(7).Visible = false;
            this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.neuSpread1_Sheet1.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.Black, System.Drawing.SystemColors.ControlLightLight, System.Drawing.SystemColors.ActiveCaptionText);
            this.neuSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 80F;
            this.neuSpread1_Sheet1.RowHeader.Visible = false;
            this.neuSpread1_Sheet1.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.Black, System.Drawing.SystemColors.ControlLightLight, System.Drawing.SystemColors.ActiveCaptionText);
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuSpread1.SetActiveViewport(0, 1, 0);
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.btExportDetial);
            this.neuPanel2.Controls.Add(this.neuBtnTimeOut);
            this.neuPanel2.Controls.Add(this.neuBtnExport);
            this.neuPanel2.Controls.Add(this.neuBtnExit);
            this.neuPanel2.Controls.Add(this.neuBtnPrint);
            this.neuPanel2.Controls.Add(this.neuBtnQuery);
            this.neuPanel2.Controls.Add(this.neuDateTimePicker2);
            this.neuPanel2.Controls.Add(this.neuDateTimePicker1);
            this.neuPanel2.Controls.Add(this.neuLabel1);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel2.Location = new System.Drawing.Point(0, 0);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(776, 47);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 1;
            // 
            // btExportDetial
            // 
            this.btExportDetial.Location = new System.Drawing.Point(625, 11);
            this.btExportDetial.Name = "btExportDetial";
            this.btExportDetial.Size = new System.Drawing.Size(98, 23);
            this.btExportDetial.TabIndex = 8;
            this.btExportDetial.Text = "导出超时明细";
            this.btExportDetial.UseVisualStyleBackColor = true;
            this.btExportDetial.Click += new System.EventHandler(this.btExportDetial_Click);
            // 
            // neuBtnTimeOut
            // 
            this.neuBtnTimeOut.Enabled = false;
            this.neuBtnTimeOut.Location = new System.Drawing.Point(354, 11);
            this.neuBtnTimeOut.Name = "neuBtnTimeOut";
            this.neuBtnTimeOut.Size = new System.Drawing.Size(75, 23);
            this.neuBtnTimeOut.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuBtnTimeOut.TabIndex = 7;
            this.neuBtnTimeOut.Text = "检索超时病历";
            this.neuBtnTimeOut.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuBtnTimeOut.UseVisualStyleBackColor = true;
            // 
            // neuBtnExport
            // 
            this.neuBtnExport.Location = new System.Drawing.Point(526, 11);
            this.neuBtnExport.Name = "neuBtnExport";
            this.neuBtnExport.Size = new System.Drawing.Size(93, 23);
            this.neuBtnExport.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuBtnExport.TabIndex = 6;
            this.neuBtnExport.Text = "导出到Excel";
            this.neuBtnExport.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuBtnExport.UseVisualStyleBackColor = true;
            this.neuBtnExport.Click += new System.EventHandler(this.neuBtnExport_Click);
            // 
            // neuBtnExit
            // 
            this.neuBtnExit.Enabled = false;
            this.neuBtnExit.Location = new System.Drawing.Point(698, 11);
            this.neuBtnExit.Name = "neuBtnExit";
            this.neuBtnExit.Size = new System.Drawing.Size(75, 23);
            this.neuBtnExit.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuBtnExit.TabIndex = 5;
            this.neuBtnExit.Text = "退出(Esc)";
            this.neuBtnExit.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuBtnExit.UseVisualStyleBackColor = true;
            this.neuBtnExit.Visible = false;
            this.neuBtnExit.Click += new System.EventHandler(this.neuBtnExit_Click);
            // 
            // neuBtnPrint
            // 
            this.neuBtnPrint.Location = new System.Drawing.Point(441, 11);
            this.neuBtnPrint.Name = "neuBtnPrint";
            this.neuBtnPrint.Size = new System.Drawing.Size(75, 23);
            this.neuBtnPrint.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuBtnPrint.TabIndex = 4;
            this.neuBtnPrint.Text = "打印";
            this.neuBtnPrint.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuBtnPrint.UseVisualStyleBackColor = true;
            this.neuBtnPrint.Click += new System.EventHandler(this.neuBtnPrint_Click);
            // 
            // neuBtnQuery
            // 
            this.neuBtnQuery.Location = new System.Drawing.Point(264, 11);
            this.neuBtnQuery.Name = "neuBtnQuery";
            this.neuBtnQuery.Size = new System.Drawing.Size(75, 23);
            this.neuBtnQuery.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuBtnQuery.TabIndex = 3;
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
            this.neuDateTimePicker2.Location = new System.Drawing.Point(156, 11);
            this.neuDateTimePicker2.Name = "neuDateTimePicker2";
            this.neuDateTimePicker2.Size = new System.Drawing.Size(89, 21);
            this.neuDateTimePicker2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuDateTimePicker2.TabIndex = 2;
            // 
            // neuDateTimePicker1
            // 
            this.neuDateTimePicker1.CustomFormat = "yyyy/MM/dd 00:00:00";
            this.neuDateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.neuDateTimePicker1.IsEnter2Tab = false;
            this.neuDateTimePicker1.Location = new System.Drawing.Point(68, 11);
            this.neuDateTimePicker1.Name = "neuDateTimePicker1";
            this.neuDateTimePicker1.Size = new System.Drawing.Size(87, 21);
            this.neuDateTimePicker1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuDateTimePicker1.TabIndex = 1;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(3, 15);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(59, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "出院日期:";
            // 
            // neuPanel3
            // 
            this.neuPanel3.Controls.Add(this.neuPanel1);
            this.neuPanel3.Controls.Add(this.neuPanel2);
            this.neuPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel3.Location = new System.Drawing.Point(0, 0);
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size(776, 559);
            this.neuPanel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 2;
            // 
            // ucCaseCallbackPercent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.neuPanel3);
            this.Name = "ucCaseCallbackPercent";
            this.Size = new System.Drawing.Size(776, 559);
            this.neuPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.neuPanel2.ResumeLayout(false);
            this.neuPanel2.PerformLayout();
            this.neuPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker neuDateTimePicker2;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker neuDateTimePicker1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuButton neuBtnExport;
        private FS.FrameWork.WinForms.Controls.NeuButton neuBtnExit;
        private FS.FrameWork.WinForms.Controls.NeuButton neuBtnPrint;
        private FS.FrameWork.WinForms.Controls.NeuButton neuBtnQuery;
        private FS.FrameWork.WinForms.Controls.NeuButton neuBtnTimeOut;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel3;
        private FarPoint.Win.Spread.FpSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btExportDetial;
    }
}
