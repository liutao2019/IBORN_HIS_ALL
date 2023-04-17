namespace FS.SOC.Local.DrugStore.ChaoYang.Inpatient
{
    partial class ucNormalDrugBill
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
            FarPoint.Win.LineBorder lineBorder1 = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 2, false, true, false, false);
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.nlblPrinted = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlblRowCount = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlblBillNO = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbTitle = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlblBedNO = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlblAge = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlblSex = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbPationNO = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlblPatientDept = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
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
            this.neuPanel1.Controls.Add(this.nlblPrinted);
            this.neuPanel1.Controls.Add(this.nlblRowCount);
            this.neuPanel1.Controls.Add(this.nlblBillNO);
            this.neuPanel1.Controls.Add(this.lbTitle);
            this.neuPanel1.Controls.Add(this.nlblBedNO);
            this.neuPanel1.Controls.Add(this.nlblAge);
            this.neuPanel1.Controls.Add(this.nlblSex);
            this.neuPanel1.Controls.Add(this.lbName);
            this.neuPanel1.Controls.Add(this.lbPationNO);
            this.neuPanel1.Controls.Add(this.nlblPatientDept);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(778, 54);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 2;
            // 
            // nlblPrinted
            // 
            this.nlblPrinted.AutoSize = true;
            this.nlblPrinted.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlblPrinted.Location = new System.Drawing.Point(628, 9);
            this.nlblPrinted.Name = "nlblPrinted";
            this.nlblPrinted.Size = new System.Drawing.Size(0, 12);
            this.nlblPrinted.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlblPrinted.TabIndex = 32;
            // 
            // nlblRowCount
            // 
            this.nlblRowCount.AutoSize = true;
            this.nlblRowCount.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlblRowCount.Location = new System.Drawing.Point(3, 13);
            this.nlblRowCount.Name = "nlblRowCount";
            this.nlblRowCount.Size = new System.Drawing.Size(53, 12);
            this.nlblRowCount.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlblRowCount.TabIndex = 31;
            this.nlblRowCount.Text = "记录数：";
            // 
            // nlblBillNO
            // 
            this.nlblBillNO.AutoSize = true;
            this.nlblBillNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlblBillNO.Location = new System.Drawing.Point(634, 13);
            this.nlblBillNO.Name = "nlblBillNO";
            this.nlblBillNO.Size = new System.Drawing.Size(41, 12);
            this.nlblBillNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlblBillNO.TabIndex = 30;
            this.nlblBillNO.Text = "单号：";
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTitle.Location = new System.Drawing.Point(268, 6);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(211, 19);
            this.lbTitle.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbTitle.TabIndex = 29;
            this.lbTitle.Text = "住院处方单(出院带药)";
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nlblBedNO
            // 
            this.nlblBedNO.AutoSize = true;
            this.nlblBedNO.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlblBedNO.Location = new System.Drawing.Point(178, 36);
            this.nlblBedNO.Name = "nlblBedNO";
            this.nlblBedNO.Size = new System.Drawing.Size(77, 14);
            this.nlblBedNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlblBedNO.TabIndex = 28;
            this.nlblBedNO.Text = "床号：0123";
            // 
            // nlblAge
            // 
            this.nlblAge.AutoSize = true;
            this.nlblAge.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlblAge.Location = new System.Drawing.Point(664, 35);
            this.nlblAge.Name = "nlblAge";
            this.nlblAge.Size = new System.Drawing.Size(84, 14);
            this.nlblAge.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlblAge.TabIndex = 27;
            this.nlblAge.Text = "年龄：100岁";
            // 
            // nlblSex
            // 
            this.nlblSex.AutoSize = true;
            this.nlblSex.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlblSex.Location = new System.Drawing.Point(592, 35);
            this.nlblSex.Name = "nlblSex";
            this.nlblSex.Size = new System.Drawing.Size(63, 14);
            this.nlblSex.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlblSex.TabIndex = 26;
            this.nlblSex.Text = "性别：女";
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbName.Location = new System.Drawing.Point(480, 35);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(91, 14);
            this.lbName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbName.TabIndex = 25;
            this.lbName.Text = "姓名：张月和";
            // 
            // lbPationNO
            // 
            this.lbPationNO.AutoSize = true;
            this.lbPationNO.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbPationNO.Location = new System.Drawing.Point(276, 35);
            this.lbPationNO.Name = "lbPationNO";
            this.lbPationNO.Size = new System.Drawing.Size(189, 14);
            this.lbPationNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbPationNO.TabIndex = 23;
            this.lbPationNO.Text = "住院流水号：ZY010000041046";
            // 
            // nlblPatientDept
            // 
            this.nlblPatientDept.AutoSize = true;
            this.nlblPatientDept.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlblPatientDept.Location = new System.Drawing.Point(2, 35);
            this.nlblPatientDept.Name = "nlblPatientDept";
            this.nlblPatientDept.Size = new System.Drawing.Size(119, 14);
            this.nlblPatientDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlblPatientDept.TabIndex = 24;
            this.nlblPatientDept.Text = "科室：普济肛肠科";
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.neuSpread1);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel2.Location = new System.Drawing.Point(0, 54);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(778, 36);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 3;
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, ";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.Location = new System.Drawing.Point(0, 0);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(778, 36);
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
            this.neuSpread1_Sheet1.ColumnCount = 10;
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin3", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, false);
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "药品名称";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "规格";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "数量";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "货位号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "使用方法";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "服药次数";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "每次用量";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "金额";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "开单医生";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "用药说明";
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Border = lineBorder1;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.ColumnHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, System.Drawing.Color.Transparent, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Transparent, 3);
            this.neuSpread1_Sheet1.ColumnHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, System.Drawing.Color.Transparent, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Transparent, 2);
            this.neuSpread1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "药品名称";
            this.neuSpread1_Sheet1.Columns.Get(0).Locked = false;
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 148F;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "规格";
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 100F;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "数量";
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 46F;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "货位号";
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 52F;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "使用方法";
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 67F;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "服药次数";
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 66F;
            this.neuSpread1_Sheet1.Columns.Get(7).Label = "金额";
            this.neuSpread1_Sheet1.Columns.Get(7).Width = 64F;
            this.neuSpread1_Sheet1.Columns.Get(8).Label = "开单医生";
            this.neuSpread1_Sheet1.Columns.Get(8).Width = 59F;
            this.neuSpread1_Sheet1.Columns.Get(9).Label = "用药说明";
            this.neuSpread1_Sheet1.Columns.Get(9).Width = 102F;
            this.neuSpread1_Sheet1.DefaultStyle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.Transparent, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Transparent);
            this.neuSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.RowHeader.Visible = false;
            this.neuSpread1_Sheet1.Rows.Default.Height = 28F;
            this.neuSpread1_Sheet1.SheetCornerHorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, System.Drawing.Color.Transparent, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Transparent);
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.SheetCornerStyle.Locked = false;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.Transparent, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Transparent);
            this.neuSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuSpread1.SetActiveViewport(0, 1, 0);
            // 
            // ucNormalDrugBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.neuPanel2);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucNormalDrugBill";
            this.Size = new System.Drawing.Size(778, 90);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.neuPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlblPrinted;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlblRowCount;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlblBillNO;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbTitle;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlblBedNO;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlblAge;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlblSex;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbName;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbPationNO;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlblPatientDept;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.SOC.Windows.Forms.FpSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;


    }
}
