namespace FS.SOC.HISFC.Fee.Components.Maintenance.ItemGroup
{
    partial class ucChooseItem
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
            this.neuSpread = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.fpItemGroup = new FarPoint.Win.Spread.SheetView();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpItemGroup)).BeginInit();
            this.SuspendLayout();
            // 
            // neuSpread
            // 
            this.neuSpread.About = "3.0.2004.2005";
            this.neuSpread.AccessibleDescription = "neuSpread, Sheet1, Row 0, Column 0, ";
            this.neuSpread.BackColor = System.Drawing.SystemColors.Control;
            this.neuSpread.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread.Location = new System.Drawing.Point(0, 0);
            this.neuSpread.Name = "neuSpread";
            this.neuSpread.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpItemGroup});
            this.neuSpread.Size = new System.Drawing.Size(446, 264);
            this.neuSpread.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread.TabIndex = 7;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread.TextTipAppearance = tipAppearance1;
            this.neuSpread.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpItemGroup
            // 
            this.fpItemGroup.Reset();
            this.fpItemGroup.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpItemGroup.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpItemGroup.ColumnCount = 5;
            this.fpItemGroup.RowCount = 0;
            this.fpItemGroup.ColumnHeader.Cells.Get(0, 0).Value = "自定义码";
            this.fpItemGroup.ColumnHeader.Cells.Get(0, 1).Value = "编码";
            this.fpItemGroup.ColumnHeader.Cells.Get(0, 2).Value = "名称";
            this.fpItemGroup.ColumnHeader.Cells.Get(0, 3).Value = "拼音码";
            this.fpItemGroup.ColumnHeader.Cells.Get(0, 4).Value = "五笔码";
            this.fpItemGroup.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpItemGroup.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpItemGroup.ColumnHeader.Rows.Get(0).Height = 25F;
            this.fpItemGroup.Columns.Get(0).Label = "自定义码";
            this.fpItemGroup.Columns.Get(0).Locked = true;
            this.fpItemGroup.Columns.Get(0).Width = 85F;
            this.fpItemGroup.Columns.Get(1).Label = "编码";
            this.fpItemGroup.Columns.Get(1).Locked = true;
            this.fpItemGroup.Columns.Get(1).Width = 0F;
            this.fpItemGroup.Columns.Get(2).Label = "名称";
            this.fpItemGroup.Columns.Get(2).Locked = true;
            this.fpItemGroup.Columns.Get(2).Width = 183F;
            this.fpItemGroup.Columns.Get(3).Label = "拼音码";
            this.fpItemGroup.Columns.Get(3).Locked = true;
            this.fpItemGroup.Columns.Get(4).Label = "五笔码";
            this.fpItemGroup.Columns.Get(4).Locked = true;
            this.fpItemGroup.DataAutoSizeColumns = false;
            this.fpItemGroup.DefaultStyle.Locked = false;
            this.fpItemGroup.DefaultStyle.Parent = "DataAreaDefault";
            this.fpItemGroup.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpItemGroup.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpItemGroup.RowHeader.Columns.Default.Resizable = true;
            this.fpItemGroup.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpItemGroup.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpItemGroup.Rows.Default.Height = 22F;
            this.fpItemGroup.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpItemGroup.SheetCornerStyle.Locked = false;
            this.fpItemGroup.SheetCornerStyle.Parent = "HeaderDefault";
            this.fpItemGroup.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpItemGroup.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuSpread.SetActiveViewport(0, 1, 0);
            // 
            // ucChooseItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuSpread);
            this.Name = "ucChooseItem";
            this.Size = new System.Drawing.Size(446, 264);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpItemGroup)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected FS.SOC.Windows.Forms.FpSpread neuSpread;
        private FarPoint.Win.Spread.SheetView fpItemGroup;
    }
}
