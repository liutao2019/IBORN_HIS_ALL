namespace GJLocal.HISFC.Components.OpGuide.InDrugStore.InPatient
{
    partial class ucDetailDrugBill
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
            this.nlbPageNo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbReprint = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbNurseCell = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbRowCount = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbStockDept = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbBillNO = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbTitle = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuSpread1 = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.nlbPageNo);
            this.neuPanel1.Controls.Add(this.nlbReprint);
            this.neuPanel1.Controls.Add(this.nlbNurseCell);
            this.neuPanel1.Controls.Add(this.nlbRowCount);
            this.neuPanel1.Controls.Add(this.nlbStockDept);
            this.neuPanel1.Controls.Add(this.nlbBillNO);
            this.neuPanel1.Controls.Add(this.nlbTitle);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(811, 49);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 1;
            // 
            // nlbPageNo
            // 
            this.nlbPageNo.AutoSize = true;
            this.nlbPageNo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlbPageNo.Location = new System.Drawing.Point(679, 10);
            this.nlbPageNo.Name = "nlbPageNo";
            this.nlbPageNo.Size = new System.Drawing.Size(78, 12);
            this.nlbPageNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbPageNo.TabIndex = 47;
            this.nlbPageNo.Text = "记录数：120";
            // 
            // nlbReprint
            // 
            this.nlbReprint.AutoSize = true;
            this.nlbReprint.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlbReprint.Location = new System.Drawing.Point(26, 7);
            this.nlbReprint.Name = "nlbReprint";
            this.nlbReprint.Size = new System.Drawing.Size(60, 16);
            this.nlbReprint.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbReprint.TabIndex = 46;
            this.nlbReprint.Text = "(补打)";
            // 
            // nlbNurseCell
            // 
            this.nlbNurseCell.AutoSize = true;
            this.nlbNurseCell.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlbNurseCell.Location = new System.Drawing.Point(3, 33);
            this.nlbNurseCell.Name = "nlbNurseCell";
            this.nlbNurseCell.Size = new System.Drawing.Size(44, 12);
            this.nlbNurseCell.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbNurseCell.TabIndex = 16;
            this.nlbNurseCell.Text = "病区：";
            // 
            // nlbRowCount
            // 
            this.nlbRowCount.AutoSize = true;
            this.nlbRowCount.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlbRowCount.Location = new System.Drawing.Point(679, 33);
            this.nlbRowCount.Name = "nlbRowCount";
            this.nlbRowCount.Size = new System.Drawing.Size(78, 12);
            this.nlbRowCount.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbRowCount.TabIndex = 15;
            this.nlbRowCount.Text = "记录数：120";
            // 
            // nlbStockDept
            // 
            this.nlbStockDept.AutoSize = true;
            this.nlbStockDept.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlbStockDept.Location = new System.Drawing.Point(265, 33);
            this.nlbStockDept.Name = "nlbStockDept";
            this.nlbStockDept.Size = new System.Drawing.Size(70, 12);
            this.nlbStockDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbStockDept.TabIndex = 14;
            this.nlbStockDept.Text = "发药科室：";
            // 
            // nlbBillNO
            // 
            this.nlbBillNO.AutoSize = true;
            this.nlbBillNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlbBillNO.Location = new System.Drawing.Point(496, 33);
            this.nlbBillNO.Name = "nlbBillNO";
            this.nlbBillNO.Size = new System.Drawing.Size(120, 12);
            this.nlbBillNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbBillNO.TabIndex = 12;
            this.nlbBillNO.Text = "单据号：123456789";
            // 
            // nlbTitle
            // 
            this.nlbTitle.AutoSize = true;
            this.nlbTitle.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlbTitle.Location = new System.Drawing.Point(216, 4);
            this.nlbTitle.Name = "nlbTitle";
            this.nlbTitle.Size = new System.Drawing.Size(189, 19);
            this.nlbTitle.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbTitle.TabIndex = 10;
            this.nlbTitle.Text = "住院药房明细摆药单";
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, ";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.Font = new System.Drawing.Font("宋体", 9F);
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.Location = new System.Drawing.Point(0, 49);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(811, 363);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 2;
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
            this.neuSpread1_Sheet1.ColumnCount = 14;
            this.neuSpread1_Sheet1.RowCount = 1;
            this.neuSpread1_Sheet1.RowHeader.ColumnCount = 0;
            this.neuSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin3", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Black, FarPoint.Win.Spread.GridLines.None, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, false);
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "货位号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "编码";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "床号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "姓名";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "名称";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "规格";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "用法";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "频次";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "每次用量";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "总量";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "使用时间";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "备注";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "单价";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "金额";
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.CellType = textCellType1;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.ColumnHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.Black, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.White);
            this.neuSpread1_Sheet1.ColumnHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, System.Drawing.Color.Transparent, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.White);
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "货位号";
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 47F;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "编码";
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 0F;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "床号";
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 0F;
            this.neuSpread1_Sheet1.Columns.Get(3).Font = new System.Drawing.Font("宋体", 12F);
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "姓名";
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 51F;
            this.neuSpread1_Sheet1.Columns.Get(4).Font = new System.Drawing.Font("宋体", 12F);
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "名称";
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 158F;
            this.neuSpread1_Sheet1.Columns.Get(5).Font = new System.Drawing.Font("宋体", 12F);
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "规格";
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 97F;
            this.neuSpread1_Sheet1.Columns.Get(6).Font = new System.Drawing.Font("宋体", 12F);
            this.neuSpread1_Sheet1.Columns.Get(6).Label = "用法";
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 57F;
            this.neuSpread1_Sheet1.Columns.Get(7).Font = new System.Drawing.Font("宋体", 12F);
            this.neuSpread1_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(7).Label = "频次";
            this.neuSpread1_Sheet1.Columns.Get(7).Width = 56F;
            this.neuSpread1_Sheet1.Columns.Get(8).Font = new System.Drawing.Font("宋体", 12F);
            this.neuSpread1_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(8).Label = "每次用量";
            this.neuSpread1_Sheet1.Columns.Get(8).Width = 80F;
            this.neuSpread1_Sheet1.Columns.Get(9).Font = new System.Drawing.Font("宋体", 12F);
            this.neuSpread1_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(9).Label = "总量";
            this.neuSpread1_Sheet1.Columns.Get(9).Width = 61F;
            this.neuSpread1_Sheet1.Columns.Get(10).Font = new System.Drawing.Font("宋体", 12F);
            this.neuSpread1_Sheet1.Columns.Get(10).Label = "使用时间";
            this.neuSpread1_Sheet1.Columns.Get(10).Width = 116F;
            this.neuSpread1_Sheet1.Columns.Get(11).Font = new System.Drawing.Font("宋体", 12F);
            this.neuSpread1_Sheet1.Columns.Get(11).Label = "备注";
            this.neuSpread1_Sheet1.Columns.Get(11).Width = 64F;
            this.neuSpread1_Sheet1.Columns.Get(12).Font = new System.Drawing.Font("宋体", 12F);
            this.neuSpread1_Sheet1.Columns.Get(12).Label = "单价";
            this.neuSpread1_Sheet1.Columns.Get(12).Width = 0F;
            this.neuSpread1_Sheet1.Columns.Get(13).Font = new System.Drawing.Font("宋体", 12F);
            this.neuSpread1_Sheet1.Columns.Get(13).Label = "金额";
            this.neuSpread1_Sheet1.Columns.Get(13).Width = 0F;
            this.neuSpread1_Sheet1.DefaultStyle.Font = new System.Drawing.Font("宋体", 10.5F);
            this.neuSpread1_Sheet1.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.RowHeader.Visible = false;
            this.neuSpread1_Sheet1.Rows.Default.Height = 30F;
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.SheetCornerStyle.Locked = false;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ucDetailDrugBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.neuSpread1);
            this.Controls.Add(this.neuPanel1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "ucDetailDrugBill";
            this.Size = new System.Drawing.Size(811, 412);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbTitle;
        protected FS.SOC.Windows.Forms.FpSpread neuSpread1;
        protected FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbRowCount;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbStockDept;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbBillNO;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbNurseCell;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbReprint;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbPageNo;

    }
}
