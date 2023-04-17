namespace FS.SOC.Local.DrugStore.LSYY.Outpatient
{
    partial class ucHerbalDrugList
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
            FarPoint.Win.Spread.TipAppearance tipAppearance4 = new FarPoint.Win.Spread.TipAppearance();
            System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("zh-CN", false);
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lbReprint = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbRecipe = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.nlbPrintTime = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbTotCost = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel1.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            this.neuPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.neuPanel1.Controls.Add(this.lbReprint);
            this.neuPanel1.Controls.Add(this.lbRecipe);
            this.neuPanel1.Controls.Add(this.lbName);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(396, 28);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // lbReprint
            // 
            this.lbReprint.AutoSize = true;
            this.lbReprint.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbReprint.Location = new System.Drawing.Point(346, 9);
            this.lbReprint.Name = "lbReprint";
            this.lbReprint.Size = new System.Drawing.Size(51, 16);
            this.lbReprint.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbReprint.TabIndex = 8;
            this.lbReprint.Text = "补 打";
            // 
            // lbRecipe
            // 
            this.lbRecipe.AutoSize = true;
            this.lbRecipe.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbRecipe.Location = new System.Drawing.Point(207, 11);
            this.lbRecipe.Name = "lbRecipe";
            this.lbRecipe.Size = new System.Drawing.Size(140, 14);
            this.lbRecipe.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbRecipe.TabIndex = 5;
            this.lbRecipe.Text = "处方号: 123456789";
            // 
            // lbName
            // 
            this.lbName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbName.Location = new System.Drawing.Point(4, 11);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(211, 14);
            this.lbName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbName.TabIndex = 1;
            this.lbName.Text = "刘德华  男  56岁 老年特定医保";
            // 
            // neuPanel2
            // 
            this.neuPanel2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.neuPanel2.Controls.Add(this.nlbPrintTime);
            this.neuPanel2.Controls.Add(this.nlbTotCost);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuPanel2.Location = new System.Drawing.Point(0, 336);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(396, 20);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 1;
            // 
            // nlbPrintTime
            // 
            this.nlbPrintTime.AutoSize = true;
            this.nlbPrintTime.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlbPrintTime.Location = new System.Drawing.Point(155, 4);
            this.nlbPrintTime.Name = "nlbPrintTime";
            this.nlbPrintTime.Size = new System.Drawing.Size(210, 14);
            this.nlbPrintTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbPrintTime.TabIndex = 3;
            this.nlbPrintTime.Text = "打印时间：2010-12.26 00:00:00";
            // 
            // nlbTotCost
            // 
            this.nlbTotCost.AutoSize = true;
            this.nlbTotCost.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlbTotCost.Location = new System.Drawing.Point(0, 4);
            this.nlbTotCost.Name = "nlbTotCost";
            this.nlbTotCost.Size = new System.Drawing.Size(119, 14);
            this.nlbTotCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbTotCost.TabIndex = 2;
            this.nlbTotCost.Text = "总药价：100.00元";
            // 
            // neuPanel3
            // 
            this.neuPanel3.AutoSize = true;
            this.neuPanel3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.neuPanel3.Controls.Add(this.neuSpread1);
            this.neuPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel3.Location = new System.Drawing.Point(0, 28);
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size(396, 308);
            this.neuPanel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 2;
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, ";
            this.neuSpread1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.neuSpread1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(0, 0);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(396, 308);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 1;
            tipAppearance4.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance4.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance4;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ColumnCount = 6;
            this.neuSpread1_Sheet1.RowCount = 13;
            this.neuSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Transparent, FarPoint.Win.Spread.GridLines.None, System.Drawing.Color.Empty, System.Drawing.Color.Black, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, false);
            this.neuSpread1_Sheet1.Cells.Get(0, 0).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(0, 0).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(0, 0).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.neuSpread1_Sheet1.Cells.Get(0, 0).ParseFormatString = "n";
            this.neuSpread1_Sheet1.Cells.Get(0, 0).Value = 1;
            this.neuSpread1_Sheet1.Cells.Get(1, 0).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(1, 0).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(1, 0).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.neuSpread1_Sheet1.Cells.Get(1, 0).ParseFormatString = "n";
            this.neuSpread1_Sheet1.Cells.Get(1, 0).Value = 2;
            this.neuSpread1_Sheet1.Cells.Get(2, 0).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(2, 0).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(2, 0).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.neuSpread1_Sheet1.Cells.Get(2, 0).ParseFormatString = "n";
            this.neuSpread1_Sheet1.Cells.Get(2, 0).Value = 3;
            this.neuSpread1_Sheet1.Cells.Get(3, 0).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(3, 0).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(3, 0).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.neuSpread1_Sheet1.Cells.Get(3, 0).ParseFormatString = "n";
            this.neuSpread1_Sheet1.Cells.Get(3, 0).Value = 4;
            this.neuSpread1_Sheet1.Cells.Get(4, 0).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(4, 0).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(4, 0).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.neuSpread1_Sheet1.Cells.Get(4, 0).ParseFormatString = "n";
            this.neuSpread1_Sheet1.Cells.Get(4, 0).Value = 5;
            this.neuSpread1_Sheet1.Cells.Get(5, 0).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(5, 0).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(5, 0).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.neuSpread1_Sheet1.Cells.Get(5, 0).ParseFormatString = "n";
            this.neuSpread1_Sheet1.Cells.Get(5, 0).Value = 6;
            this.neuSpread1_Sheet1.Cells.Get(6, 0).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(6, 0).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(6, 0).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.neuSpread1_Sheet1.Cells.Get(6, 0).ParseFormatString = "n";
            this.neuSpread1_Sheet1.Cells.Get(6, 0).Value = 7;
            this.neuSpread1_Sheet1.Cells.Get(7, 0).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(7, 0).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(7, 0).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.neuSpread1_Sheet1.Cells.Get(7, 0).ParseFormatString = "n";
            this.neuSpread1_Sheet1.Cells.Get(7, 0).Value = 8;
            this.neuSpread1_Sheet1.Cells.Get(8, 0).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(8, 0).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(8, 0).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.neuSpread1_Sheet1.Cells.Get(8, 0).ParseFormatString = "n";
            this.neuSpread1_Sheet1.Cells.Get(8, 0).Value = 9;
            this.neuSpread1_Sheet1.Cells.Get(9, 0).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(9, 0).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(9, 0).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.neuSpread1_Sheet1.Cells.Get(9, 0).ParseFormatString = "n";
            this.neuSpread1_Sheet1.Cells.Get(9, 0).Value = 10;
            this.neuSpread1_Sheet1.Cells.Get(10, 0).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(10, 0).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(10, 0).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.neuSpread1_Sheet1.Cells.Get(10, 0).ParseFormatString = "n";
            this.neuSpread1_Sheet1.Cells.Get(10, 0).Value = 11;
            this.neuSpread1_Sheet1.Cells.Get(11, 0).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(11, 0).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(11, 0).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.neuSpread1_Sheet1.Cells.Get(11, 0).ParseFormatString = "n";
            this.neuSpread1_Sheet1.Cells.Get(11, 0).Value = 12;
            this.neuSpread1_Sheet1.Cells.Get(12, 0).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(12, 0).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(12, 0).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.neuSpread1_Sheet1.Cells.Get(12, 0).ParseFormatString = "n";
            this.neuSpread1_Sheet1.Cells.Get(12, 0).Value = 13;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "名称";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "每付量";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "用法";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "名称";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "每付量";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "用法";
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.ColumnHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.Black);
            this.neuSpread1_Sheet1.ColumnHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, System.Drawing.Color.Transparent, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.White);
            this.neuSpread1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "名称";
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 82F;
            this.neuSpread1_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "每付量";
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 50F;
            this.neuSpread1_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "用法";
            this.neuSpread1_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "名称";
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 82F;
            this.neuSpread1_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "每付量";
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 50F;
            this.neuSpread1_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "用法";
            this.neuSpread1_Sheet1.DefaultStyle.Font = new System.Drawing.Font("宋体", 10F);
            this.neuSpread1_Sheet1.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.neuSpread1_Sheet1.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Bottom;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.RowHeader.Visible = false;
            this.neuSpread1_Sheet1.Rows.Default.Height = 22F;
            this.neuSpread1_Sheet1.SheetCornerStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.SheetCornerStyle.Locked = false;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ucHerbalDrugList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Controls.Add(this.neuPanel3);
            this.Controls.Add(this.neuPanel2);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucHerbalDrugList";
            this.Size = new System.Drawing.Size(396, 356);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.neuPanel2.ResumeLayout(false);
            this.neuPanel2.PerformLayout();
            this.neuPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel3;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbName;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbReprint;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbRecipe;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbPrintTime;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbTotCost;
    }
}
