namespace HISFC.Components.Package.Fee.Controls
{
    partial class ucPackageSelector
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
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.EmptyBorder emptyBorder1 = new FarPoint.Win.EmptyBorder();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblClose = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.fpPackage = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpPackage_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpPackage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPackage_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblClose);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(798, 22);
            this.panel1.TabIndex = 0;
            // 
            // lblClose
            // 
            this.lblClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblClose.Font = new System.Drawing.Font("宋体", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblClose.ForeColor = System.Drawing.Color.Blue;
            this.lblClose.Location = new System.Drawing.Point(759, 0);
            this.lblClose.Name = "lblClose";
            this.lblClose.Size = new System.Drawing.Size(39, 22);
            this.lblClose.TabIndex = 1;
            this.lblClose.Text = "关闭";
            this.lblClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(4, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "可按汉字,拼音码,输入码检索";
            // 
            // fpPackage
            // 
            this.fpPackage.About = "3.0.2004.2005";
            this.fpPackage.AccessibleDescription = "fpPackage, Sheet1, Row 0, Column 0, ";
            this.fpPackage.BackColor = System.Drawing.Color.White;
            this.fpPackage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fpPackage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpPackage.FileName = "";
            this.fpPackage.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpPackage.IsAutoSaveGridStatus = false;
            this.fpPackage.IsCanCustomConfigColumn = false;
            this.fpPackage.Location = new System.Drawing.Point(1, 23);
            this.fpPackage.Name = "fpPackage";
            this.fpPackage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpPackage.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpPackage_Sheet1});
            this.fpPackage.Size = new System.Drawing.Size(798, 268);
            this.fpPackage.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpPackage.TabIndex = 2;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpPackage.TextTipAppearance = tipAppearance1;
            this.fpPackage.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpPackage_Sheet1
            // 
            this.fpPackage_Sheet1.Reset();
            this.fpPackage_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpPackage_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpPackage_Sheet1.ColumnCount = 9;
            this.fpPackage_Sheet1.RowCount = 2;
            this.fpPackage_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.SystemColors.Control, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpPackage_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "编码";
            this.fpPackage_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "名称";
            this.fpPackage_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "金额";
            this.fpPackage_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "类型";
            this.fpPackage_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "范围";
            this.fpPackage_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "拼音码";
            this.fpPackage_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "输入码";
            this.fpPackage_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "套餐级别";
            this.fpPackage_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "父套餐编码";
            this.fpPackage_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(217)))), ((int)(((byte)(234)))));
            this.fpPackage_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpPackage_Sheet1.Columns.Get(0).Label = "编码";
            this.fpPackage_Sheet1.Columns.Get(0).Width = 85F;
            this.fpPackage_Sheet1.Columns.Get(1).Label = "名称";
            this.fpPackage_Sheet1.Columns.Get(1).Width = 240F;
            numberCellType1.DecimalPlaces = 2;
            this.fpPackage_Sheet1.Columns.Get(2).CellType = numberCellType1;
            this.fpPackage_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.fpPackage_Sheet1.Columns.Get(2).Label = "金额";
            this.fpPackage_Sheet1.Columns.Get(2).Width = 100F;
            this.fpPackage_Sheet1.Columns.Get(3).Label = "类型";
            this.fpPackage_Sheet1.Columns.Get(3).Width = 80F;
            this.fpPackage_Sheet1.Columns.Get(4).Label = "范围";
            this.fpPackage_Sheet1.Columns.Get(4).Width = 90F;
            this.fpPackage_Sheet1.Columns.Get(5).Label = "拼音码";
            this.fpPackage_Sheet1.Columns.Get(5).Width = 71F;
            this.fpPackage_Sheet1.Columns.Get(6).Label = "输入码";
            this.fpPackage_Sheet1.Columns.Get(6).Width = 70F;
            this.fpPackage_Sheet1.Columns.Get(7).Label = "套餐级别";
            this.fpPackage_Sheet1.Columns.Get(7).Width = 70F;
            this.fpPackage_Sheet1.Columns.Get(8).Label = "父套餐编码";
            this.fpPackage_Sheet1.Columns.Get(8).Width = 70F;
            this.fpPackage_Sheet1.DataAutoSizeColumns = false;
            this.fpPackage_Sheet1.DefaultStyle.Border = emptyBorder1;
            this.fpPackage_Sheet1.DefaultStyle.Locked = true;
            this.fpPackage_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpPackage_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpPackage_Sheet1.RowHeader.Columns.Get(0).Width = 33F;
            this.fpPackage_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(217)))), ((int)(((byte)(234)))));
            this.fpPackage_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpPackage_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpPackage_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpPackage_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ucPackageSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.fpPackage);
            this.Controls.Add(this.panel1);
            this.Name = "ucPackageSelector";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Size = new System.Drawing.Size(800, 292);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpPackage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPackage_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpPackage;
        private FarPoint.Win.Spread.SheetView fpPackage_Sheet1;
        private System.Windows.Forms.Label lblClose;

    }
}
