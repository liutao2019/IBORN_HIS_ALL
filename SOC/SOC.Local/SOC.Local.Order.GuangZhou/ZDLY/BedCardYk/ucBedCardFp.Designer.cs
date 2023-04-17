namespace FS.SOC.Local.Order.GuangZhou.ZDLY.BedCardYk
{
    partial class ucBedCardFp
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
            FarPoint.Win.LineBorder lineBorder1 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, false, false, false, true);
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.LineBorder lineBorder2 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, false, false, false, true);
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.LineBorder lineBorder3 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, false, false, false, true);
            FarPoint.Win.ComplexBorder complexBorder1 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None, System.Drawing.Color.White), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None, System.Drawing.Color.White), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None, System.Drawing.Color.White), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine, System.Drawing.Color.White));
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.fpBedCard = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpBedCard_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpBedCard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpBedCard_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.neuPanel1.Controls.Add(this.fpBedCard);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(145, 106);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // fpBedCard
            // 
            this.fpBedCard.About = "3.0.2004.2005";
            this.fpBedCard.AccessibleDescription = "fpBedCard, Sheet1, Row 0, Column 0, ";
            this.fpBedCard.BackColor = System.Drawing.Color.White;
            this.fpBedCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fpBedCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpBedCard.FileName = "";
            this.fpBedCard.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            this.fpBedCard.IsAutoSaveGridStatus = false;
            this.fpBedCard.IsCanCustomConfigColumn = false;
            this.fpBedCard.Location = new System.Drawing.Point(0, 0);
            this.fpBedCard.Margin = new System.Windows.Forms.Padding(0);
            this.fpBedCard.Name = "fpBedCard";
            this.fpBedCard.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpBedCard.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpBedCard_Sheet1});
            this.fpBedCard.Size = new System.Drawing.Size(143, 104);
            this.fpBedCard.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpBedCard.TabIndex = 12;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpBedCard.TextTipAppearance = tipAppearance1;
            this.fpBedCard.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            // 
            // fpBedCard_Sheet1
            // 
            this.fpBedCard_Sheet1.Reset();
            this.fpBedCard_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpBedCard_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpBedCard_Sheet1.ColumnCount = 2;
            this.fpBedCard_Sheet1.ColumnHeader.RowCount = 0;
            this.fpBedCard_Sheet1.RowCount = 5;
            this.fpBedCard_Sheet1.RowHeader.ColumnCount = 0;
            this.fpBedCard_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.SystemColors.Control, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.White, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpBedCard_Sheet1.Cells.Get(0, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpBedCard_Sheet1.Cells.Get(0, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpBedCard_Sheet1.Cells.Get(1, 0).Border = lineBorder1;
            this.fpBedCard_Sheet1.Cells.Get(1, 0).CellType = textCellType1;
            this.fpBedCard_Sheet1.Cells.Get(1, 0).Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpBedCard_Sheet1.Cells.Get(1, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpBedCard_Sheet1.Cells.Get(1, 0).Value = "张三";
            this.fpBedCard_Sheet1.Cells.Get(1, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General;
            this.fpBedCard_Sheet1.Cells.Get(2, 0).Border = lineBorder2;
            this.fpBedCard_Sheet1.Cells.Get(2, 0).CellType = textCellType2;
            this.fpBedCard_Sheet1.Cells.Get(2, 0).Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpBedCard_Sheet1.Cells.Get(2, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpBedCard_Sheet1.Cells.Get(2, 0).Value = "V0011";
            this.fpBedCard_Sheet1.Cells.Get(2, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Bottom;
            this.fpBedCard_Sheet1.Cells.Get(3, 0).Border = lineBorder3;
            this.fpBedCard_Sheet1.Cells.Get(3, 0).Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpBedCard_Sheet1.Cells.Get(3, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpBedCard_Sheet1.Cells.Get(3, 0).Value = "主管医生";
            this.fpBedCard_Sheet1.Cells.Get(3, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpBedCard_Sheet1.Cells.Get(4, 0).Border = complexBorder1;
            this.fpBedCard_Sheet1.Cells.Get(4, 0).Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpBedCard_Sheet1.Cells.Get(4, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpBedCard_Sheet1.Cells.Get(4, 0).Value = "责任护士";
            this.fpBedCard_Sheet1.Cells.Get(4, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Bottom;
            this.fpBedCard_Sheet1.Columns.Get(0).Width = 133F;
            this.fpBedCard_Sheet1.Columns.Get(1).Width = 0F;
            this.fpBedCard_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
            this.fpBedCard_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpBedCard_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpBedCard_Sheet1.Rows.Get(0).Height = 0F;
            this.fpBedCard_Sheet1.Rows.Get(2).Height = 27F;
            this.fpBedCard_Sheet1.Rows.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpBedCard_Sheet1.Rows.Get(3).Height = 27F;
            this.fpBedCard_Sheet1.Rows.Get(4).Height = 22F;
            this.fpBedCard_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpBedCard_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ucBedCardFp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.neuPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ucBedCardFp";
            this.Size = new System.Drawing.Size(145, 106);
            this.neuPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpBedCard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpBedCard_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpBedCard;
        private FarPoint.Win.Spread.SheetView fpBedCard_Sheet1;
    }
}
