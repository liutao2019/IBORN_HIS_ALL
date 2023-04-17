namespace FS.SOC.Local.NurseWorkStation.QiaoTou
{
    partial class ucCircuitCardFP
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuContexMenu1 = new FS.FrameWork.WinForms.Controls.NeuContexMenu();
            this.pnPrint = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblTip = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.pnPrint.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.neuSpread1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(759, 457);
            this.panel1.TabIndex = 0;
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
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(759, 457);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 1;
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
            this.neuSpread1_Sheet1.ColumnCount = 13;
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.SystemColors.Control, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, false);
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "时间";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "床号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "姓名";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "组";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "医嘱内容";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "数量";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "频次";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "用法";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "每次量";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "应执行时间";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "滴速";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 11).BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "签名";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 12).BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "时间";
            this.neuSpread1_Sheet1.ColumnHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, System.Drawing.Color.LightGray, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.White);
            this.neuSpread1_Sheet1.ColumnHeader.Rows.Get(0).Height = 40F;
            this.neuSpread1_Sheet1.ColumnHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, System.Drawing.Color.LightGray, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.White);
            this.neuSpread1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "时间";
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 31F;
            this.neuSpread1_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "床号";
            this.neuSpread1_Sheet1.Columns.Get(1).MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.Always;
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 32F;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "姓名";
            this.neuSpread1_Sheet1.Columns.Get(2).MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.Always;
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 46F;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "组";
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 15F;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "医嘱内容";
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 269F;
            this.neuSpread1_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "数量";
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 36F;
            this.neuSpread1_Sheet1.Columns.Get(6).Label = "频次";
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 39F;
            this.neuSpread1_Sheet1.Columns.Get(7).Label = "用法";
            this.neuSpread1_Sheet1.Columns.Get(7).Width = 39F;
            this.neuSpread1_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Columns.Get(8).Label = "每次量";
            this.neuSpread1_Sheet1.Columns.Get(8).Width = 49F;
            this.neuSpread1_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Columns.Get(10).Label = "滴速";
            this.neuSpread1_Sheet1.Columns.Get(10).Width = 37F;
            this.neuSpread1_Sheet1.Columns.Get(11).Label = "签名";
            this.neuSpread1_Sheet1.Columns.Get(11).Width = 57F;
            this.neuSpread1_Sheet1.Columns.Get(12).Label = "时间";
            this.neuSpread1_Sheet1.Columns.Get(12).Width = 75F;
            this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.neuSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 7F;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.RowHeader.Visible = false;
            this.neuSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuSpread1.SetActiveViewport(0, 1, 0);
            // 
            // neuContexMenu1
            // 
            this.neuContexMenu1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            // 
            // pnPrint
            // 
            this.pnPrint.Controls.Add(this.panel1);
            this.pnPrint.Controls.Add(this.panel3);
            this.pnPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnPrint.Location = new System.Drawing.Point(0, 0);
            this.pnPrint.Name = "pnPrint";
            this.pnPrint.Size = new System.Drawing.Size(759, 495);
            this.pnPrint.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblTip);
            this.panel3.Controls.Add(this.lblTitle);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(759, 38);
            this.panel3.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.Location = new System.Drawing.Point(323, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(40, 16);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "标题";
            // 
            // lblTip
            // 
            this.lblTip.AutoSize = true;
            this.lblTip.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTip.Location = new System.Drawing.Point(652, 19);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(35, 14);
            this.lblTip.TabIndex = 1;
            this.lblTip.Text = "首日";
            // 
            // ucCircuitCardFP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnPrint);
            this.Name = "ucCircuitCardFP";
            this.Size = new System.Drawing.Size(759, 495);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.pnPrint.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuContexMenu neuContexMenu1;
        private System.Windows.Forms.Panel pnPrint;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblTip;
    }
}
