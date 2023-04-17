namespace FS.SOC.Local.Order.DrugCard.GYSY
{
    partial class ucInfusionLabel
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
            base.Dispose( disposing );
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.ComplexBorder complexBorder1 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder2 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder3 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder4 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.lbDept = new System.Windows.Forms.Label();
            this.lbTitle = new System.Windows.Forms.Label();
            this.lbBed = new System.Windows.Forms.Label();
            this.lbDate = new System.Windows.Forms.Label();
            this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.lbUsage = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl5Print = new System.Windows.Forms.Label();
            this.lblFrequency = new System.Windows.Forms.Label();
            this.lbCombo = new System.Windows.Forms.Label();
            this.lblPaitentNo = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblExeTime = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblIsShort = new FS.FrameWork.WinForms.Controls.NeuLabel();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbDept
            // 
            this.lbDept.AutoSize = true;
            this.lbDept.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbDept.Location = new System.Drawing.Point(13, 239);
            this.lbDept.Name = "lbDept";
            this.lbDept.Size = new System.Drawing.Size(35, 14);
            this.lbDept.TabIndex = 0;
            this.lbDept.Text = "科室";
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTitle.Location = new System.Drawing.Point(103, 5);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(82, 14);
            this.lbTitle.TabIndex = 0;
            this.lbTitle.Text = "住院瓶签卡";
            // 
            // lbBed
            // 
            this.lbBed.AutoSize = true;
            this.lbBed.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbBed.Location = new System.Drawing.Point(84, 29);
            this.lbBed.Name = "lbBed";
            this.lbBed.Size = new System.Drawing.Size(54, 14);
            this.lbBed.TabIndex = 0;
            this.lbBed.Text = "2013床";
            // 
            // lbDate
            // 
            this.lbDate.AutoSize = true;
            this.lbDate.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbDate.Location = new System.Drawing.Point(163, 222);
            this.lbDate.Name = "lbDate";
            this.lbDate.Size = new System.Drawing.Size(63, 14);
            this.lbDate.TabIndex = 0;
            this.lbDate.Text = "用药日期";
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "3.0.2004.2005";
            this.fpSpread1.AccessibleDescription = "fpSpread1, Sheet1, Row 0, Column 0, 药品名称";
            this.fpSpread1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.fpSpread1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.fpSpread1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.Location = new System.Drawing.Point(8, 51);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1});
            this.fpSpread1.Size = new System.Drawing.Size(339, 164);
            this.fpSpread1.TabIndex = 2;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread1.TextTipAppearance = tipAppearance1;
            this.fpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.ColumnCount = 4;
            this.fpSpread1_Sheet1.RowCount = 7;
            this.fpSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.None, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, false, false);
            this.fpSpread1_Sheet1.Cells.Get(0, 0).Border = complexBorder1;
            this.fpSpread1_Sheet1.Cells.Get(0, 0).ColumnSpan = 2;
            this.fpSpread1_Sheet1.Cells.Get(0, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.Cells.Get(0, 0).Value = "药品名称";
            this.fpSpread1_Sheet1.Cells.Get(0, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.Cells.Get(0, 1).Border = complexBorder2;
            this.fpSpread1_Sheet1.Cells.Get(0, 2).Border = complexBorder3;
            this.fpSpread1_Sheet1.Cells.Get(0, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.Cells.Get(0, 2).Value = "剂量";
            this.fpSpread1_Sheet1.Cells.Get(0, 2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.Cells.Get(0, 3).Border = complexBorder4;
            this.fpSpread1_Sheet1.Cells.Get(0, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.Cells.Get(0, 3).Value = "次数";
            this.fpSpread1_Sheet1.Cells.Get(0, 3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            textCellType1.WordWrap = true;
            this.fpSpread1_Sheet1.Cells.Get(1, 0).CellType = textCellType1;
            this.fpSpread1_Sheet1.Cells.Get(1, 0).ColumnSpan = 2;
            this.fpSpread1_Sheet1.Cells.Get(1, 0).Value = "我是淫荡邪恶猥琐的长度标咔咔又";
            this.fpSpread1_Sheet1.Cells.Get(1, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.Cells.Get(1, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            textCellType2.WordWrap = true;
            this.fpSpread1_Sheet1.Cells.Get(2, 0).CellType = textCellType2;
            this.fpSpread1_Sheet1.Cells.Get(2, 0).ColumnSpan = 2;
            this.fpSpread1_Sheet1.Cells.Get(2, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.Cells.Get(2, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            textCellType3.WordWrap = true;
            this.fpSpread1_Sheet1.Cells.Get(3, 0).CellType = textCellType3;
            this.fpSpread1_Sheet1.Cells.Get(3, 0).ColumnSpan = 2;
            this.fpSpread1_Sheet1.Cells.Get(3, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.Cells.Get(3, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            textCellType4.WordWrap = true;
            this.fpSpread1_Sheet1.Cells.Get(4, 0).CellType = textCellType4;
            this.fpSpread1_Sheet1.Cells.Get(4, 0).ColumnSpan = 2;
            this.fpSpread1_Sheet1.Cells.Get(4, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.Cells.Get(4, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.Cells.Get(5, 0).ColumnSpan = 2;
            textCellType5.WordWrap = true;
            this.fpSpread1_Sheet1.Cells.Get(6, 0).CellType = textCellType5;
            this.fpSpread1_Sheet1.Cells.Get(6, 0).ColumnSpan = 2;
            this.fpSpread1_Sheet1.Cells.Get(6, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.Cells.Get(6, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.Columns.Get(0).Width = 118F;
            this.fpSpread1_Sheet1.Columns.Get(1).Width = 151F;
            this.fpSpread1_Sheet1.Columns.Get(2).Width = 67F;
            this.fpSpread1_Sheet1.Columns.Get(3).Visible = false;
            this.fpSpread1_Sheet1.Columns.Get(3).Width = 28F;
            this.fpSpread1_Sheet1.DefaultStyle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 24F;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.RowHeader.Visible = false;
            this.fpSpread1_Sheet1.Rows.Default.Height = 30F;
            this.fpSpread1_Sheet1.Rows.Get(0).Height = 19F;
            this.fpSpread1_Sheet1.Rows.Get(1).Height = 20F;
            this.fpSpread1_Sheet1.Rows.Get(2).Height = 21F;
            this.fpSpread1_Sheet1.Rows.Get(3).Height = 21F;
            this.fpSpread1_Sheet1.Rows.Get(4).Height = 21F;
            this.fpSpread1_Sheet1.Rows.Get(5).Height = 22F;
            this.fpSpread1_Sheet1.Rows.Get(6).Height = 20F;
            this.fpSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.fpSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // lbUsage
            // 
            this.lbUsage.AutoSize = true;
            this.lbUsage.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbUsage.Location = new System.Drawing.Point(13, 222);
            this.lbUsage.Name = "lbUsage";
            this.lbUsage.Size = new System.Drawing.Size(35, 14);
            this.lbUsage.TabIndex = 0;
            this.lbUsage.Text = "用法";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(336, 171);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(13, 256);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 14);
            this.label2.TabIndex = 7;
            this.label2.Text = "配药时间/配药者：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(202, 255);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 14);
            this.label4.TabIndex = 8;
            this.label4.Text = "核对者：";
            // 
            // lbl5Print
            // 
            this.lbl5Print.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl5Print.Location = new System.Drawing.Point(334, 91);
            this.lbl5Print.Name = "lbl5Print";
            this.lbl5Print.Size = new System.Drawing.Size(17, 64);
            this.lbl5Print.TabIndex = 9;
            this.lbl5Print.Text = "打\r\n印";
            this.lbl5Print.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFrequency
            // 
            this.lblFrequency.AutoSize = true;
            this.lblFrequency.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblFrequency.Location = new System.Drawing.Point(288, 30);
            this.lblFrequency.Name = "lblFrequency";
            this.lblFrequency.Size = new System.Drawing.Size(37, 14);
            this.lblFrequency.TabIndex = 10;
            this.lblFrequency.Text = "频次";
            this.lblFrequency.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbCombo
            // 
            this.lbCombo.AutoSize = true;
            this.lbCombo.Location = new System.Drawing.Point(202, 5);
            this.lbCombo.Name = "lbCombo";
            this.lbCombo.Size = new System.Drawing.Size(63, 14);
            this.lbCombo.TabIndex = 0;
            this.lbCombo.Text = "(组合号)";
            this.lbCombo.Visible = false;
            // 
            // lblPaitentNo
            // 
            this.lblPaitentNo.AutoSize = true;
            this.lblPaitentNo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPaitentNo.Location = new System.Drawing.Point(170, 30);
            this.lblPaitentNo.Name = "lblPaitentNo";
            this.lblPaitentNo.Size = new System.Drawing.Size(52, 14);
            this.lblPaitentNo.TabIndex = 11;
            this.lblPaitentNo.Text = "病案号";
            this.lblPaitentNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblName.Location = new System.Drawing.Point(15, 29);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(37, 14);
            this.lblName.TabIndex = 13;
            this.lblName.Text = "姓名";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblExeTime
            // 
            this.lblExeTime.AutoSize = true;
            this.lblExeTime.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblExeTime.Location = new System.Drawing.Point(271, 6);
            this.lblExeTime.Name = "lblExeTime";
            this.lblExeTime.Size = new System.Drawing.Size(76, 16);
            this.lblExeTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblExeTime.TabIndex = 14;
            this.lblExeTime.Text = "执行时间";
            // 
            // lblIsShort
            // 
            this.lblIsShort.AutoSize = true;
            this.lblIsShort.Location = new System.Drawing.Point(13, 4);
            this.lblIsShort.Name = "lblIsShort";
            this.lblIsShort.Size = new System.Drawing.Size(21, 14);
            this.lblIsShort.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblIsShort.TabIndex = 14;
            this.lblIsShort.Text = "临";
            // 
            // ucInfusionLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lblIsShort);
            this.Controls.Add(this.lblExeTime);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblPaitentNo);
            this.Controls.Add(this.lblFrequency);
            this.Controls.Add(this.lbl5Print);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbDept);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.fpSpread1);
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.lbBed);
            this.Controls.Add(this.lbDate);
            this.Controls.Add(this.lbCombo);
            this.Controls.Add(this.lbUsage);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "ucInfusionLabel";
            this.Size = new System.Drawing.Size(350, 282);
            this.Load += new System.EventHandler(this.ucInfusionLabel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbDept;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Label lbBed;
        private System.Windows.Forms.Label lbDate;
        private FarPoint.Win.Spread.FpSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
        private System.Windows.Forms.Label lbUsage;
        internal System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl5Print;
        private System.Windows.Forms.Label lblFrequency;
        private System.Windows.Forms.Label lbCombo;
        private System.Windows.Forms.Label lblPaitentNo;
        private System.Windows.Forms.Label lblName;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblExeTime;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblIsShort;
    }
}
