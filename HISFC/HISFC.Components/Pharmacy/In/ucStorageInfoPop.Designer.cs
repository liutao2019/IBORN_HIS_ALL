namespace FS.HISFC.Components.Pharmacy.In
{
    partial class ucStorageInfoPop
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
            this.gb1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpStorageList1 = new FarPoint.Win.Spread.SheetView();
            this.pnlStorage1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lblStorageInfo1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.pnlTop = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lblDrugInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuButton1 = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.gb2 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuSpread2 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpStorageList2 = new FarPoint.Win.Spread.SheetView();
            this.pnlStorage2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lblStorageInfo2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.gb1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpStorageList1)).BeginInit();
            this.pnlStorage1.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.gb2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpStorageList2)).BeginInit();
            this.pnlStorage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb1
            // 
            this.gb1.Controls.Add(this.neuSpread1);
            this.gb1.Controls.Add(this.pnlStorage1);
            this.gb1.Dock = System.Windows.Forms.DockStyle.Top;
            this.gb1.Location = new System.Drawing.Point(0, 36);
            this.gb1.Name = "gb1";
            this.gb1.Size = new System.Drawing.Size(500, 156);
            this.gb1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gb1.TabIndex = 0;
            this.gb1.TabStop = false;
            this.gb1.Text = "【科室1】库存信息";
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
            this.neuSpread1.Location = new System.Drawing.Point(3, 50);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpStorageList1});
            this.neuSpread1.Size = new System.Drawing.Size(494, 103);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 1;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpStorageList1
            // 
            this.fpStorageList1.Reset();
            this.fpStorageList1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpStorageList1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpStorageList1.ColumnCount = 5;
            this.fpStorageList1.RowCount = 0;
            this.fpStorageList1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64))))), System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(213)))), ((int)(((byte)(213))))), FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(213)))), ((int)(((byte)(213))))), System.Drawing.Color.Navy, System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(153))))), System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, true, true, true);
            this.fpStorageList1.ColumnHeader.Cells.Get(0, 0).Value = "库存数量";
            this.fpStorageList1.ColumnHeader.Cells.Get(0, 1).Value = "单位";
            this.fpStorageList1.ColumnHeader.Cells.Get(0, 2).Value = "有效期";
            this.fpStorageList1.ColumnHeader.Cells.Get(0, 3).Value = "批号";
            this.fpStorageList1.ColumnHeader.Cells.Get(0, 4).Value = "状态";
            this.fpStorageList1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(213)))), ((int)(((byte)(213)))));
            this.fpStorageList1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpStorageList1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Navy;
            this.fpStorageList1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpStorageList1.Columns.Get(0).Label = "库存数量";
            this.fpStorageList1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpStorageList1.Columns.Get(0).Width = 110F;
            this.fpStorageList1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpStorageList1.Columns.Get(1).Label = "单位";
            this.fpStorageList1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpStorageList1.Columns.Get(1).Width = 50F;
            this.fpStorageList1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpStorageList1.Columns.Get(2).Label = "有效期";
            this.fpStorageList1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpStorageList1.Columns.Get(2).Width = 63F;
            this.fpStorageList1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpStorageList1.Columns.Get(3).Label = "批号";
            this.fpStorageList1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpStorageList1.Columns.Get(3).Width = 90F;
            this.fpStorageList1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpStorageList1.Columns.Get(4).Label = "状态";
            this.fpStorageList1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpStorageList1.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpStorageList1.DefaultStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.fpStorageList1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpStorageList1.RowHeader.Columns.Default.Resizable = false;
            this.fpStorageList1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(213)))), ((int)(((byte)(213)))));
            this.fpStorageList1.RowHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpStorageList1.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.Navy;
            this.fpStorageList1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpStorageList1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpStorageList1.SheetCornerStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(213)))), ((int)(((byte)(213)))));
            this.fpStorageList1.SheetCornerStyle.ForeColor = System.Drawing.Color.Navy;
            this.fpStorageList1.SheetCornerStyle.Parent = "CornerDefault";
            this.fpStorageList1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpStorageList1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuSpread1.SetActiveViewport(0, 1, 0);
            // 
            // pnlStorage1
            // 
            this.pnlStorage1.Controls.Add(this.lblStorageInfo1);
            this.pnlStorage1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStorage1.Location = new System.Drawing.Point(3, 17);
            this.pnlStorage1.Name = "pnlStorage1";
            this.pnlStorage1.Size = new System.Drawing.Size(494, 33);
            this.pnlStorage1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlStorage1.TabIndex = 0;
            // 
            // lblStorageInfo1
            // 
            this.lblStorageInfo1.AutoSize = true;
            this.lblStorageInfo1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblStorageInfo1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblStorageInfo1.Location = new System.Drawing.Point(9, 9);
            this.lblStorageInfo1.Name = "lblStorageInfo1";
            this.lblStorageInfo1.Size = new System.Drawing.Size(91, 14);
            this.lblStorageInfo1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblStorageInfo1.TabIndex = 2;
            this.lblStorageInfo1.Text = "StorageInfo1";
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.lblDrugInfo);
            this.pnlTop.Controls.Add(this.neuButton1);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(500, 36);
            this.pnlTop.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlTop.TabIndex = 1;
            // 
            // lblDrugInfo
            // 
            this.lblDrugInfo.AutoSize = true;
            this.lblDrugInfo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDrugInfo.ForeColor = System.Drawing.Color.Navy;
            this.lblDrugInfo.Location = new System.Drawing.Point(12, 11);
            this.lblDrugInfo.Name = "lblDrugInfo";
            this.lblDrugInfo.Size = new System.Drawing.Size(71, 14);
            this.lblDrugInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblDrugInfo.TabIndex = 1;
            this.lblDrugInfo.Text = "DrugInfo";
            // 
            // neuButton1
            // 
            this.neuButton1.Location = new System.Drawing.Point(413, 7);
            this.neuButton1.Name = "neuButton1";
            this.neuButton1.Size = new System.Drawing.Size(75, 23);
            this.neuButton1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuButton1.TabIndex = 0;
            this.neuButton1.Text = "关闭窗口";
            this.neuButton1.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuButton1.UseVisualStyleBackColor = true;
            this.neuButton1.Click += new System.EventHandler(this.neuButton1_Click);
            // 
            // gb2
            // 
            this.gb2.Controls.Add(this.neuSpread2);
            this.gb2.Controls.Add(this.pnlStorage2);
            this.gb2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gb2.Location = new System.Drawing.Point(0, 192);
            this.gb2.Name = "gb2";
            this.gb2.Size = new System.Drawing.Size(500, 198);
            this.gb2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gb2.TabIndex = 2;
            this.gb2.TabStop = false;
            this.gb2.Text = "【科室2】库存信息";
            // 
            // neuSpread2
            // 
            this.neuSpread2.About = "3.0.2004.2005";
            this.neuSpread2.AccessibleDescription = "neuSpread2, Sheet1";
            this.neuSpread2.BackColor = System.Drawing.Color.White;
            this.neuSpread2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread2.FileName = "";
            this.neuSpread2.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread2.IsAutoSaveGridStatus = false;
            this.neuSpread2.IsCanCustomConfigColumn = false;
            this.neuSpread2.Location = new System.Drawing.Point(3, 50);
            this.neuSpread2.Name = "neuSpread2";
            this.neuSpread2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread2.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpStorageList2});
            this.neuSpread2.Size = new System.Drawing.Size(494, 145);
            this.neuSpread2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread2.TabIndex = 2;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread2.TextTipAppearance = tipAppearance2;
            this.neuSpread2.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpStorageList2
            // 
            this.fpStorageList2.Reset();
            this.fpStorageList2.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpStorageList2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpStorageList2.ColumnCount = 5;
            this.fpStorageList2.RowCount = 0;
            this.fpStorageList2.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64))))), System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(213)))), ((int)(((byte)(213))))), FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(213)))), ((int)(((byte)(213))))), System.Drawing.Color.Navy, System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(153))))), System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, true, true, true);
            this.fpStorageList2.ColumnHeader.Cells.Get(0, 0).Value = "库存数量";
            this.fpStorageList2.ColumnHeader.Cells.Get(0, 1).Value = "单位";
            this.fpStorageList2.ColumnHeader.Cells.Get(0, 2).Value = "有效期";
            this.fpStorageList2.ColumnHeader.Cells.Get(0, 3).Value = "批号";
            this.fpStorageList2.ColumnHeader.Cells.Get(0, 4).Value = "状态";
            this.fpStorageList2.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(213)))), ((int)(((byte)(213)))));
            this.fpStorageList2.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpStorageList2.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Navy;
            this.fpStorageList2.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpStorageList2.Columns.Get(0).Label = "库存数量";
            this.fpStorageList2.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpStorageList2.Columns.Get(0).Width = 110F;
            this.fpStorageList2.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpStorageList2.Columns.Get(1).Label = "单位";
            this.fpStorageList2.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpStorageList2.Columns.Get(1).Width = 50F;
            this.fpStorageList2.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpStorageList2.Columns.Get(2).Label = "有效期";
            this.fpStorageList2.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpStorageList2.Columns.Get(2).Width = 63F;
            this.fpStorageList2.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpStorageList2.Columns.Get(3).Label = "批号";
            this.fpStorageList2.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpStorageList2.Columns.Get(3).Width = 90F;
            this.fpStorageList2.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpStorageList2.Columns.Get(4).Label = "状态";
            this.fpStorageList2.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpStorageList2.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpStorageList2.DefaultStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.fpStorageList2.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpStorageList2.RowHeader.Columns.Default.Resizable = false;
            this.fpStorageList2.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(213)))), ((int)(((byte)(213)))));
            this.fpStorageList2.RowHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpStorageList2.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.Navy;
            this.fpStorageList2.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpStorageList2.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpStorageList2.SheetCornerStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(213)))), ((int)(((byte)(213)))));
            this.fpStorageList2.SheetCornerStyle.ForeColor = System.Drawing.Color.Navy;
            this.fpStorageList2.SheetCornerStyle.Parent = "CornerDefault";
            this.fpStorageList2.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpStorageList2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuSpread2.SetActiveViewport(0, 1, 0);
            // 
            // pnlStorage2
            // 
            this.pnlStorage2.Controls.Add(this.lblStorageInfo2);
            this.pnlStorage2.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStorage2.Location = new System.Drawing.Point(3, 17);
            this.pnlStorage2.Name = "pnlStorage2";
            this.pnlStorage2.Size = new System.Drawing.Size(494, 33);
            this.pnlStorage2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlStorage2.TabIndex = 1;
            // 
            // lblStorageInfo2
            // 
            this.lblStorageInfo2.AutoSize = true;
            this.lblStorageInfo2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblStorageInfo2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblStorageInfo2.Location = new System.Drawing.Point(9, 9);
            this.lblStorageInfo2.Name = "lblStorageInfo2";
            this.lblStorageInfo2.Size = new System.Drawing.Size(91, 14);
            this.lblStorageInfo2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblStorageInfo2.TabIndex = 3;
            this.lblStorageInfo2.Text = "StorageInfo2";
            // 
            // ucStorageInfoPop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.Controls.Add(this.gb2);
            this.Controls.Add(this.gb1);
            this.Controls.Add(this.pnlTop);
            this.Name = "ucStorageInfoPop";
            this.Size = new System.Drawing.Size(500, 390);
            this.gb1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpStorageList1)).EndInit();
            this.pnlStorage1.ResumeLayout(false);
            this.pnlStorage1.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.gb2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpStorageList2)).EndInit();
            this.pnlStorage2.ResumeLayout(false);
            this.pnlStorage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox gb1;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlTop;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox gb2;
        private FS.FrameWork.WinForms.Controls.NeuButton neuButton1;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlStorage1;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlStorage2;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView fpStorageList1;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread2;
        private FarPoint.Win.Spread.SheetView fpStorageList2;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblStorageInfo1;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblDrugInfo;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblStorageInfo2;
    }
}
