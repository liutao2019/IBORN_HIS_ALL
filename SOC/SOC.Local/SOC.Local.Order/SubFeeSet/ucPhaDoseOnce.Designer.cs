namespace FS.SOC.Local.Order.SubFeeSet
{
    partial class ucPhaDoseOnce
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
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFileter = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, ";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(0, 60);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(966, 462);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 0;
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
            this.neuSpread1_Sheet1.ColumnCount = 10;
            this.neuSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "系统编码";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "药品编码";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "药品名称";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "规格";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "基本计量";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).ColumnSpan = 2;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "第二基本计量";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "诊查费";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "有效";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "操作员";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "操作时间";
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.ColumnHeader.Rows.Get(0).Height = 35F;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "药品编码";
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 63F;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "药品名称";
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 108F;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "规格";
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 48F;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "基本计量";
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 108F;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "第二基本计量";
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 95F;
            this.neuSpread1_Sheet1.Columns.Get(6).Label = "诊查费";
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 32F;
            this.neuSpread1_Sheet1.Columns.Get(7).CellType = checkBoxCellType1;
            this.neuSpread1_Sheet1.Columns.Get(7).Label = "有效";
            this.neuSpread1_Sheet1.Columns.Get(7).Width = 34F;
            this.neuSpread1_Sheet1.Columns.Get(8).CellType = textCellType1;
            this.neuSpread1_Sheet1.Columns.Get(8).Label = "操作员";
            this.neuSpread1_Sheet1.Columns.Get(8).Width = 66F;
            this.neuSpread1_Sheet1.Columns.Get(9).Label = "操作时间";
            this.neuSpread1_Sheet1.Columns.Get(9).Width = 123F;
            this.neuSpread1_Sheet1.DataAutoSizeColumns = false;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.Rows.Default.Height = 30F;
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.neuSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtFileter);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(966, 60);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(70, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(626, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "注意：只支持一人操作维护，两人同时维护会导致数据相互覆盖";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(24, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "过滤：";
            // 
            // txtFileter
            // 
            this.txtFileter.Location = new System.Drawing.Point(74, 31);
            this.txtFileter.Name = "txtFileter";
            this.txtFileter.Size = new System.Drawing.Size(270, 21);
            this.txtFileter.TabIndex = 2;
            this.txtFileter.TextChanged += new System.EventHandler(this.txtFileter_TextChanged);
            // 
            // ucPhaDoseOnce
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuSpread1);
            this.Controls.Add(this.panel1);
            this.Name = "ucPhaDoseOnce";
            this.Size = new System.Drawing.Size(966, 522);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFileter;
        private System.Windows.Forms.Label label2;
    }
}
