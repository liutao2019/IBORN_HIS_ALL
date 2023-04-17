namespace FS.HISFC.Components.Nurse.Print
{
    partial class ucPrintTreatment
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
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.nlbPageNo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbTime = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbSex = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbAge = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbPatientName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel1.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.neuPanel1.Controls.Add(this.nlbPageNo);
            this.neuPanel1.Controls.Add(this.nlbTime);
            this.neuPanel1.Controls.Add(this.nlbSex);
            this.neuPanel1.Controls.Add(this.nlbAge);
            this.neuPanel1.Controls.Add(this.neuLabel2);
            this.neuPanel1.Controls.Add(this.nlbPatientName);
            this.neuPanel1.Controls.Add(this.neuLabel1);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(480, 51);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 1;
            // 
            // nlbPageNo
            // 
            this.nlbPageNo.AutoSize = true;
            this.nlbPageNo.Location = new System.Drawing.Point(3, 10);
            this.nlbPageNo.Name = "nlbPageNo";
            this.nlbPageNo.Size = new System.Drawing.Size(47, 12);
            this.nlbPageNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbPageNo.TabIndex = 8;
            this.nlbPageNo.Text = "页：1/1";
            this.nlbPageNo.Visible = false;
            // 
            // nlbTime
            // 
            this.nlbTime.AutoSize = true;
            this.nlbTime.Location = new System.Drawing.Point(308, 35);
            this.nlbTime.Name = "nlbTime";
            this.nlbTime.Size = new System.Drawing.Size(41, 12);
            this.nlbTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbTime.TabIndex = 8;
            this.nlbTime.Text = "日期：";
            // 
            // nlbSex
            // 
            this.nlbSex.AutoSize = true;
            this.nlbSex.Location = new System.Drawing.Point(118, 35);
            this.nlbSex.Name = "nlbSex";
            this.nlbSex.Size = new System.Drawing.Size(41, 12);
            this.nlbSex.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbSex.TabIndex = 7;
            this.nlbSex.Text = "性别：";
            // 
            // nlbAge
            // 
            this.nlbAge.AutoSize = true;
            this.nlbAge.Location = new System.Drawing.Point(208, 35);
            this.nlbAge.Name = "nlbAge";
            this.nlbAge.Size = new System.Drawing.Size(41, 12);
            this.nlbAge.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbAge.TabIndex = 6;
            this.nlbAge.Text = "年龄：";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(3, 35);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(41, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 5;
            this.neuLabel2.Text = "姓名：";
            // 
            // nlbPatientName
            // 
            this.nlbPatientName.AutoSize = true;
            this.nlbPatientName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlbPatientName.Location = new System.Drawing.Point(47, 35);
            this.nlbPatientName.Name = "nlbPatientName";
            this.nlbPatientName.Size = new System.Drawing.Size(0, 12);
            this.nlbPatientName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbPatientName.TabIndex = 4;
            // 
            // neuLabel1
            // 
            this.neuLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel1.Location = new System.Drawing.Point(166, 3);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(72, 20);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "治疗单";
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.neuSpread1);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel2.Location = new System.Drawing.Point(0, 51);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(480, 329);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 2;
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
            this.neuSpread1.Location = new System.Drawing.Point(0, 0);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(480, 329);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 3;
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
            this.neuSpread1_Sheet1.ColumnCount = 9;
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "项目名称";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "组合号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "组合";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "频次";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "用法";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "天数";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "数量";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "备注";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "执行时间/执行人签名";
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "项目名称";
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 127F;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "组合号";
            this.neuSpread1_Sheet1.Columns.Get(1).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "组合";
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 21F;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "频次";
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 30F;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "用法";
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 31F;
            this.neuSpread1_Sheet1.Columns.Get(5).CellType = numberCellType1;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "天数";
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 29F;
            this.neuSpread1_Sheet1.Columns.Get(6).CellType = numberCellType2;
            this.neuSpread1_Sheet1.Columns.Get(6).Label = "数量";
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 35F;
            this.neuSpread1_Sheet1.Columns.Get(7).Label = "备注";
            this.neuSpread1_Sheet1.Columns.Get(7).Width = 59F;
            this.neuSpread1_Sheet1.Columns.Get(8).Label = "执行时间/执行人签名";
            this.neuSpread1_Sheet1.Columns.Get(8).Width = 94F;
            this.neuSpread1_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.neuSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuSpread1.SetActiveViewport(0, 1, 0);
            // 
            // ucPrintTreatment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuPanel2);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucPrintTreatment";
            this.Size = new System.Drawing.Size(480, 380);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.neuPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbPageNo;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbTime;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbSex;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbAge;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbPatientName;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
    }
}
