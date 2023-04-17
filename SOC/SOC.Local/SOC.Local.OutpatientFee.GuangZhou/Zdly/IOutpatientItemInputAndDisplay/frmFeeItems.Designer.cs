namespace FS.SOC.Local.OutpatientFee.GuangZhou.Zdly.IOutpatientItemInputAndDisplay
{
    partial class frmFeeItems
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType4 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType5 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.fpSelectedItem = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpSelectedItem_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSelectedItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSelectedItem_Sheet1)).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(798, 377);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 55);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(798, 322);
            this.panel3.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.fpSelectedItem);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(798, 322);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "项目列表";
            // 
            // fpSelectedItem
            // 
            this.fpSelectedItem.About = "3.0.2004.2005";
            this.fpSelectedItem.AccessibleDescription = "fpSelectedItem, Sheet1";
            this.fpSelectedItem.BackColor = System.Drawing.Color.White;
            this.fpSelectedItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSelectedItem.FileName = "";
            this.fpSelectedItem.Font = new System.Drawing.Font("宋体", 9F);
            this.fpSelectedItem.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSelectedItem.IsAutoSaveGridStatus = false;
            this.fpSelectedItem.IsCanCustomConfigColumn = false;
            this.fpSelectedItem.Location = new System.Drawing.Point(3, 17);
            this.fpSelectedItem.Name = "fpSelectedItem";
            this.fpSelectedItem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSelectedItem.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSelectedItem_Sheet1});
            this.fpSelectedItem.Size = new System.Drawing.Size(792, 302);
            this.fpSelectedItem.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpSelectedItem.TabIndex = 104;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSelectedItem.TextTipAppearance = tipAppearance1;
            this.fpSelectedItem.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpSelectedItem_Sheet1
            // 
            this.fpSelectedItem_Sheet1.Reset();
            this.fpSelectedItem_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSelectedItem_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSelectedItem_Sheet1.ColumnCount = 12;
            this.fpSelectedItem_Sheet1.RowCount = 0;
            this.fpSelectedItem_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpSelectedItem_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "项目编号";
            this.fpSelectedItem_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "项目名称";
            this.fpSelectedItem_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "规格";
            this.fpSelectedItem_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "等级";
            this.fpSelectedItem_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "费用类别";
            this.fpSelectedItem_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "单价";
            this.fpSelectedItem_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "数量";
            this.fpSelectedItem_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "费用金额";
            this.fpSelectedItem_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "自费金额";
            this.fpSelectedItem_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "自付金额";
            this.fpSelectedItem_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "公费金额";
            this.fpSelectedItem_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "优惠金额";
            this.fpSelectedItem_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSelectedItem_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSelectedItem_Sheet1.Columns.Get(0).Label = "项目编号";
            this.fpSelectedItem_Sheet1.Columns.Get(0).Width = 72F;
            this.fpSelectedItem_Sheet1.Columns.Get(1).CellType = textCellType1;
            this.fpSelectedItem_Sheet1.Columns.Get(1).Label = "项目名称";
            this.fpSelectedItem_Sheet1.Columns.Get(1).Width = 90F;
            this.fpSelectedItem_Sheet1.Columns.Get(2).CellType = textCellType2;
            this.fpSelectedItem_Sheet1.Columns.Get(2).Label = "规格";
            this.fpSelectedItem_Sheet1.Columns.Get(2).Width = 50F;
            this.fpSelectedItem_Sheet1.Columns.Get(2).Visible = false;

            this.fpSelectedItem_Sheet1.Columns.Get(3).Width = 30F;
            this.fpSelectedItem_Sheet1.Columns.Get(3).HorizontalAlignment =FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSelectedItem_Sheet1.Columns.Get(4).Label = "费用类别";
            this.fpSelectedItem_Sheet1.Columns.Get(4).Width = 40F;
            this.fpSelectedItem_Sheet1.Columns.Get(5).Label = "单价";
            this.fpSelectedItem_Sheet1.Columns.Get(5).Width = 57F;
            this.fpSelectedItem_Sheet1.Columns.Get(6).Label = "数量";
            this.fpSelectedItem_Sheet1.Columns.Get(6).Width = 20F;
            this.fpSelectedItem_Sheet1.Columns.Get(7).CellType = numberCellType1;
            this.fpSelectedItem_Sheet1.Columns.Get(7).Label = "费用金额";
            this.fpSelectedItem_Sheet1.Columns.Get(7).Width = 55F;
            this.fpSelectedItem_Sheet1.Columns.Get(8).CellType = numberCellType2;
            this.fpSelectedItem_Sheet1.Columns.Get(8).Label = "自费金额";
            this.fpSelectedItem_Sheet1.Columns.Get(8).Width =55F;
            this.fpSelectedItem_Sheet1.Columns.Get(9).CellType = numberCellType3;
            this.fpSelectedItem_Sheet1.Columns.Get(9).Label = "自付金额";
            this.fpSelectedItem_Sheet1.Columns.Get(9).Width = 55F;
            this.fpSelectedItem_Sheet1.Columns.Get(10).CellType = numberCellType4;
            this.fpSelectedItem_Sheet1.Columns.Get(10).Label = "公费金额";
            this.fpSelectedItem_Sheet1.Columns.Get(10).Width = 55F;
            this.fpSelectedItem_Sheet1.Columns.Get(11).CellType = numberCellType5;
            this.fpSelectedItem_Sheet1.Columns.Get(11).Label = "优惠金额";
            this.fpSelectedItem_Sheet1.Columns.Get(11).Width = 55F;
            this.fpSelectedItem_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpSelectedItem_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpSelectedItem_Sheet1.RowHeader.Columns.Get(0).Width = 15F;
            this.fpSelectedItem_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSelectedItem_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSelectedItem_Sheet1.Rows.Default.Height = 25F;
            this.fpSelectedItem_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpSelectedItem_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpSelectedItem_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSelectedItem_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.fpSelectedItem_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpSelectedItem_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpSelectedItem.SetActiveViewport(0, 1, 0);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(798, 55);
            this.panel2.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.txtInput);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(798, 55);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "费用过滤信息";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(708, 25);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "关 闭";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(76, 25);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(195, 21);
            this.txtInput.TabIndex = 3;
            this.txtInput.TextChanged += new System.EventHandler(this.txtInput_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "查询码：";
            // 
            // frmFeeItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 377);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmFeeItems";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "费用明细项目";
            this.Load += new System.EventHandler(this.frmFeeItems_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSelectedItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSelectedItem_Sheet1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Label label2;
        protected FS.FrameWork.WinForms.Controls.NeuSpread fpSelectedItem;
        private FarPoint.Win.Spread.SheetView fpSelectedItem_Sheet1;
        private System.Windows.Forms.Button button2;
    }
}