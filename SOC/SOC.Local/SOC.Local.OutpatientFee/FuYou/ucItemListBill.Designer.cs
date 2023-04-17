namespace FS.SOC.Local.OutpatientFee.FuYou
{
    partial class ucItemListBill
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
            this.nlbRePrint = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbTime = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbSex = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbAge = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbPatientName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.nlbFeeOper = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbDoctor = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuSpread1 = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel1.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.nlbPageNo);
            this.neuPanel1.Controls.Add(this.nlbRePrint);
            this.neuPanel1.Controls.Add(this.nlbTime);
            this.neuPanel1.Controls.Add(this.nlbSex);
            this.neuPanel1.Controls.Add(this.nlbAge);
            this.neuPanel1.Controls.Add(this.neuLabel2);
            this.neuPanel1.Controls.Add(this.nlbPatientName);
            this.neuPanel1.Controls.Add(this.neuLabel1);
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(701, 51);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // nlbPageNo
            // 
            this.nlbPageNo.AutoSize = true;
            this.nlbPageNo.Location = new System.Drawing.Point(553, 35);
            this.nlbPageNo.Name = "nlbPageNo";
            this.nlbPageNo.Size = new System.Drawing.Size(47, 12);
            this.nlbPageNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbPageNo.TabIndex = 8;
            this.nlbPageNo.Text = "页：1/1";
            // 
            // nlbRePrint
            // 
            this.nlbRePrint.AutoSize = true;
            this.nlbRePrint.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlbRePrint.Location = new System.Drawing.Point(438, 9);
            this.nlbRePrint.Name = "nlbRePrint";
            this.nlbRePrint.Size = new System.Drawing.Size(37, 14);
            this.nlbRePrint.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbRePrint.TabIndex = 9;
            this.nlbRePrint.Text = "补打";
            // 
            // nlbTime
            // 
            this.nlbTime.AutoSize = true;
            this.nlbTime.Location = new System.Drawing.Point(365, 35);
            this.nlbTime.Name = "nlbTime";
            this.nlbTime.Size = new System.Drawing.Size(41, 12);
            this.nlbTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbTime.TabIndex = 8;
            this.nlbTime.Text = "日期：";
            // 
            // nlbSex
            // 
            this.nlbSex.AutoSize = true;
            this.nlbSex.Location = new System.Drawing.Point(168, 35);
            this.nlbSex.Name = "nlbSex";
            this.nlbSex.Size = new System.Drawing.Size(41, 12);
            this.nlbSex.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbSex.TabIndex = 7;
            this.nlbSex.Text = "性别：";
            // 
            // nlbAge
            // 
            this.nlbAge.AutoSize = true;
            this.nlbAge.Location = new System.Drawing.Point(274, 35);
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
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel1.Location = new System.Drawing.Point(166, 3);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(240, 20);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "顺德妇幼保健医院治疗单";
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.nlbFeeOper);
            this.neuPanel2.Controls.Add(this.nlbDoctor);
            this.neuPanel2.Location = new System.Drawing.Point(0, 284);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(701, 25);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 4;
            // 
            // nlbFeeOper
            // 
            this.nlbFeeOper.AutoSize = true;
            this.nlbFeeOper.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlbFeeOper.Location = new System.Drawing.Point(219, 4);
            this.nlbFeeOper.Name = "nlbFeeOper";
            this.nlbFeeOper.Size = new System.Drawing.Size(72, 16);
            this.nlbFeeOper.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbFeeOper.TabIndex = 7;
            this.nlbFeeOper.Text = "收费员：";
            // 
            // nlbDoctor
            // 
            this.nlbDoctor.AutoSize = true;
            this.nlbDoctor.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlbDoctor.Location = new System.Drawing.Point(3, 4);
            this.nlbDoctor.Name = "nlbDoctor";
            this.nlbDoctor.Size = new System.Drawing.Size(56, 16);
            this.nlbDoctor.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbDoctor.TabIndex = 6;
            this.nlbDoctor.Text = "医生：";
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, ";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.neuSpread1.Font = new System.Drawing.Font("宋体", 9F);
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.Location = new System.Drawing.Point(0, 51);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(701, 232);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 5;
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
            this.neuSpread1_Sheet1.ColumnCount = 6;
            this.neuSpread1_Sheet1.RowCount = 7;
            this.neuSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin3", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Black, FarPoint.Win.Spread.GridLines.None, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, false);
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "项目名称";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "用法";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "次数";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "天数";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "备注";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "组合号";
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.CellType = textCellType1;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.ColumnHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.Black, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.White);
            this.neuSpread1_Sheet1.ColumnHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, System.Drawing.Color.Transparent, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.White);
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "项目名称";
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 300F;
            this.neuSpread1_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "用法";
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 133F;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "次数";
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 50F;
            this.neuSpread1_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "天数";
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 50F;
            this.neuSpread1_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "备注";
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 152F;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "组合号";
            this.neuSpread1_Sheet1.Columns.Get(5).Visible = false;
            this.neuSpread1_Sheet1.DefaultStyle.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.DefaultStyle.Locked = true;
            this.neuSpread1_Sheet1.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.RowHeader.Visible = false;
            this.neuSpread1_Sheet1.Rows.Default.Height = 26F;
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.SheetCornerStyle.Locked = false;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ucItemListBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.neuSpread1);
            this.Controls.Add(this.neuPanel2);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucItemListBill";
            this.Size = new System.Drawing.Size(701, 308);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.neuPanel2.ResumeLayout(false);
            this.neuPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbSex;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbAge;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbPatientName;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbTime;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbDoctor;
        protected FS.SOC.Windows.Forms.FpSpread neuSpread1;
        protected FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbFeeOper;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbRePrint;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbPageNo;
    }
}
