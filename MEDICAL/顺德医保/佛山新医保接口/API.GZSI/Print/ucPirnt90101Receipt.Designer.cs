namespace API.GZSI.Print
{
    partial class ucPirnt90101Receipt
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
            FarPoint.Win.ComplexBorder complexBorder1 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(System.Drawing.Color.White));
            FarPoint.Win.ComplexBorder complexBorder2 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(System.Drawing.Color.White), new FarPoint.Win.ComplexBorderSide(System.Drawing.Color.White), new FarPoint.Win.ComplexBorderSide(System.Drawing.Color.White), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder3 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(System.Drawing.Color.White), new FarPoint.Win.ComplexBorderSide(System.Drawing.Color.White), new FarPoint.Win.ComplexBorderSide(System.Drawing.Color.White), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder4 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(System.Drawing.Color.White), new FarPoint.Win.ComplexBorderSide(System.Drawing.Color.White), new FarPoint.Win.ComplexBorderSide(System.Drawing.Color.White), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder5 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(System.Drawing.Color.White), new FarPoint.Win.ComplexBorderSide(System.Drawing.Color.White), new FarPoint.Win.ComplexBorderSide(System.Drawing.Color.White), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder6 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(System.Drawing.Color.White));
            FarPoint.Win.ComplexBorder complexBorder7 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(System.Drawing.Color.White), new FarPoint.Win.ComplexBorderSide(System.Drawing.Color.White), new FarPoint.Win.ComplexBorderSide(System.Drawing.SystemColors.WindowText), new FarPoint.Win.ComplexBorderSide(System.Drawing.Color.White));
            FarPoint.Win.ComplexBorder complexBorder8 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(System.Drawing.Color.White));
            FarPoint.Win.ComplexBorder complexBorder9 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(System.Drawing.SystemColors.WindowText, 0), new FarPoint.Win.ComplexBorderSide(System.Drawing.SystemColors.WindowText, 0), new FarPoint.Win.ComplexBorderSide(System.Drawing.SystemColors.WindowText), new FarPoint.Win.ComplexBorderSide(System.Drawing.SystemColors.WindowText));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.ComplexBorder complexBorder10 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(System.Drawing.Color.White));
            FarPoint.Win.ComplexBorder complexBorder11 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(System.Drawing.Color.White));
            FarPoint.Win.ComplexBorder complexBorder12 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(System.Drawing.Color.White));
            this.fpPrint = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpPrint_Sheet1 = new FarPoint.Win.Spread.SheetView();
            ((System.ComponentModel.ISupportInitialize)(this.fpPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPrint_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // fpPrint
            // 
            this.fpPrint.About = "3.0.2004.2005";
            this.fpPrint.AccessibleDescription = "fpPrint, Sheet1, Row 0, Column 0, ";
            this.fpPrint.BackColor = System.Drawing.Color.White;
            this.fpPrint.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.fpPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpPrint.FileName = "";
            this.fpPrint.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            this.fpPrint.IsAutoSaveGridStatus = false;
            this.fpPrint.IsCanCustomConfigColumn = false;
            this.fpPrint.Location = new System.Drawing.Point(8, 8);
            this.fpPrint.Name = "fpPrint";
            this.fpPrint.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpPrint.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpPrint_Sheet1});
            this.fpPrint.Size = new System.Drawing.Size(544, 794);
            this.fpPrint.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpPrint.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpPrint.TextTipAppearance = tipAppearance1;
            this.fpPrint.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            // 
            // fpPrint_Sheet1
            // 
            this.fpPrint_Sheet1.Reset();
            this.fpPrint_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpPrint_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpPrint_Sheet1.ColumnCount = 6;
            this.fpPrint_Sheet1.ColumnHeader.RowCount = 0;
            this.fpPrint_Sheet1.RowCount = 13;
            this.fpPrint_Sheet1.RowHeader.ColumnCount = 0;
            this.fpPrint_Sheet1.Cells.Get(0, 0).Border = complexBorder1;
            this.fpPrint_Sheet1.Cells.Get(0, 1).Border = complexBorder2;
            this.fpPrint_Sheet1.Cells.Get(0, 1).ColumnSpan = 4;
            this.fpPrint_Sheet1.Cells.Get(0, 1).Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpPrint_Sheet1.Cells.Get(0, 1).Value = "生育备案登记打印回执单";
            this.fpPrint_Sheet1.Cells.Get(0, 2).Border = complexBorder3;
            this.fpPrint_Sheet1.Cells.Get(0, 3).Border = complexBorder4;
            this.fpPrint_Sheet1.Cells.Get(0, 4).Border = complexBorder5;
            this.fpPrint_Sheet1.Cells.Get(0, 5).Border = complexBorder6;
            this.fpPrint_Sheet1.Cells.Get(1, 1).Value = "姓名";
            this.fpPrint_Sheet1.Cells.Get(1, 2).Tag = "psn_name";
            this.fpPrint_Sheet1.Cells.Get(1, 3).Value = "证件号码";
            this.fpPrint_Sheet1.Cells.Get(1, 4).Tag = "certno";
            this.fpPrint_Sheet1.Cells.Get(2, 1).Value = "手机号";
            this.fpPrint_Sheet1.Cells.Get(2, 2).Tag = "tel";
            this.fpPrint_Sheet1.Cells.Get(2, 3).Value = "定点医院";
            this.fpPrint_Sheet1.Cells.Get(2, 4).Tag = "fixmedins_name";
            this.fpPrint_Sheet1.Cells.Get(3, 1).Value = "孕周数";
            this.fpPrint_Sheet1.Cells.Get(3, 2).Tag = "geso_val";
            this.fpPrint_Sheet1.Cells.Get(3, 3).Value = "胎次";
            this.fpPrint_Sheet1.Cells.Get(3, 4).Tag = "fetts";
            this.fpPrint_Sheet1.Cells.Get(4, 1).Value = "报销类型";
            this.fpPrint_Sheet1.Cells.Get(4, 2).Tag = "matn_type";
            this.fpPrint_Sheet1.Cells.Get(4, 3).Value = "预计生育日期";
            this.fpPrint_Sheet1.Cells.Get(4, 4).Tag = "plan_matn_date";
            this.fpPrint_Sheet1.Cells.Get(5, 1).Value = "开始日期";
            this.fpPrint_Sheet1.Cells.Get(5, 2).Tag = "begndate";
            this.fpPrint_Sheet1.Cells.Get(5, 3).Value = "结束日期";
            this.fpPrint_Sheet1.Cells.Get(5, 4).Tag = "enddate";
            this.fpPrint_Sheet1.Cells.Get(6, 1).Value = "地址";
            this.fpPrint_Sheet1.Cells.Get(6, 2).ColumnSpan = 3;
            this.fpPrint_Sheet1.Cells.Get(6, 2).Tag = "addr";
            this.fpPrint_Sheet1.Cells.Get(6, 2).Value = "无登记信息";
            this.fpPrint_Sheet1.Cells.Get(7, 1).ColumnSpan = 4;
            this.fpPrint_Sheet1.Cells.Get(7, 1).Value = "代办人信息";
            this.fpPrint_Sheet1.Cells.Get(8, 1).Value = "代办人姓名";
            this.fpPrint_Sheet1.Cells.Get(8, 2).Tag = "agnter_name";
            this.fpPrint_Sheet1.Cells.Get(8, 3).Value = "证件号码";
            this.fpPrint_Sheet1.Cells.Get(8, 4).Tag = "agnter_certno";
            this.fpPrint_Sheet1.Cells.Get(9, 1).Value = "联系方式";
            this.fpPrint_Sheet1.Cells.Get(9, 2).Tag = "agnter_tel";
            this.fpPrint_Sheet1.Cells.Get(9, 3).Value = "联系地址";
            this.fpPrint_Sheet1.Cells.Get(9, 4).Tag = "agnter_addr";
            this.fpPrint_Sheet1.Cells.Get(10, 1).ColumnSpan = 2;
            this.fpPrint_Sheet1.Cells.Get(10, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpPrint_Sheet1.Cells.Get(10, 1).Tag = "oper";
            this.fpPrint_Sheet1.Cells.Get(10, 1).Value = "经办人：";
            this.fpPrint_Sheet1.Cells.Get(10, 2).Tag = "oper";
            this.fpPrint_Sheet1.Cells.Get(10, 3).ColumnSpan = 2;
            this.fpPrint_Sheet1.Cells.Get(10, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpPrint_Sheet1.Cells.Get(10, 3).Tag = "operDate";
            this.fpPrint_Sheet1.Cells.Get(10, 3).Value = "经办日期：";
            this.fpPrint_Sheet1.Cells.Get(10, 4).Tag = "oper";
            this.fpPrint_Sheet1.Cells.Get(11, 1).ColumnSpan = 4;
            this.fpPrint_Sheet1.Cells.Get(11, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpPrint_Sheet1.Cells.Get(11, 1).Value = "备注：参加生育保险累计满12个月的次月1日起开始享受生育保险待遇。";
            this.fpPrint_Sheet1.Columns.Default.Width = 124F;
            this.fpPrint_Sheet1.Columns.Get(0).Border = complexBorder7;
            this.fpPrint_Sheet1.Columns.Get(0).Width = 25F;
            this.fpPrint_Sheet1.Columns.Get(5).Border = complexBorder8;
            this.fpPrint_Sheet1.Columns.Get(5).Width = 25F;
            this.fpPrint_Sheet1.DefaultStyle.Border = complexBorder9;
            textCellType1.WordWrap = true;
            this.fpPrint_Sheet1.DefaultStyle.CellType = textCellType1;
            this.fpPrint_Sheet1.DefaultStyle.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpPrint_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpPrint_Sheet1.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.fpPrint_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpPrint_Sheet1.Rows.Default.Height = 32F;
            this.fpPrint_Sheet1.Rows.Get(10).Border = complexBorder10;
            this.fpPrint_Sheet1.Rows.Get(11).Border = complexBorder11;
            this.fpPrint_Sheet1.Rows.Get(12).Border = complexBorder12;
            this.fpPrint_Sheet1.Rows.Get(12).Height = 8F;
            this.fpPrint_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ucPirnt90101Receipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.fpPrint);
            this.Name = "ucPirnt90101Receipt";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.Size = new System.Drawing.Size(560, 810);
            ((System.ComponentModel.ISupportInitialize)(this.fpPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPrint_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FarPoint.Win.Spread.SheetView fpPrint_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpPrint;
    }
}
