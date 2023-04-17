namespace IBorn.SI.MedicalInsurance.OutPatient
{
    partial class ucBalance
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ucOutPatientInfo1 = new IBorn.SI.MedicalInsurance.BaseControls.ucOutPatientInfo();
            this.panel2 = new System.Windows.Forms.Panel();
            this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.fpSpread2 = new FarPoint.Win.Spread.FpSpread();
            this.fpSpread2_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread2_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ucOutPatientInfo1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(851, 93);
            this.panel1.TabIndex = 0;
            // 
            // ucOutPatientInfo1
            // 
            this.ucOutPatientInfo1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.ucOutPatientInfo1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucOutPatientInfo1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucOutPatientInfo1.IsFullConvertToHalf = true;
            this.ucOutPatientInfo1.IsPrint = false;
            this.ucOutPatientInfo1.Location = new System.Drawing.Point(0, 0);
            this.ucOutPatientInfo1.Name = "ucOutPatientInfo1";
            this.ucOutPatientInfo1.ParentFormToolBar = null;
            this.ucOutPatientInfo1.Size = new System.Drawing.Size(851, 91);
            this.ucOutPatientInfo1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.fpSpread1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 93);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(851, 431);
            this.panel2.TabIndex = 1;
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "3.0.2004.2005";
            this.fpSpread1.AccessibleDescription = "fpSpread1, Sheet1, Row 0, Column 0, ";
            this.fpSpread1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread1.Location = new System.Drawing.Point(0, 0);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1});
            this.fpSpread1.Size = new System.Drawing.Size(851, 431);
            this.fpSpread1.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread1.TextTipAppearance = tipAppearance1;
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.ColumnCount = 17;
            this.fpSpread1_Sheet1.RowCount = 0;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "住院流水号";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "结算发票号";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "费用日期";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "项目序号";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "医院项目编码";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "医院项目名称（有截取）";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "医保分类代码";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "规格";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "剂型";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "单价";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "数量";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "金额";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "操作时间";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "备注2";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 14).Value = "备注3";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 15).Value = "读入标记";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 16).Value = "药品来源";
            this.fpSpread1_Sheet1.Columns.Get(0).Label = "住院流水号";
            this.fpSpread1_Sheet1.Columns.Get(0).Visible = false;
            this.fpSpread1_Sheet1.Columns.Get(1).Label = "结算发票号";
            this.fpSpread1_Sheet1.Columns.Get(1).Width = 80F;
            this.fpSpread1_Sheet1.Columns.Get(2).Label = "费用日期";
            this.fpSpread1_Sheet1.Columns.Get(2).Width = 86F;
            this.fpSpread1_Sheet1.Columns.Get(3).CellType = textCellType1;
            this.fpSpread1_Sheet1.Columns.Get(3).Label = "项目序号";
            this.fpSpread1_Sheet1.Columns.Get(3).Width = 81F;
            this.fpSpread1_Sheet1.Columns.Get(4).Label = "医院项目编码";
            this.fpSpread1_Sheet1.Columns.Get(4).Width = 87F;
            this.fpSpread1_Sheet1.Columns.Get(5).Label = "医院项目名称（有截取）";
            this.fpSpread1_Sheet1.Columns.Get(5).Width = 229F;
            this.fpSpread1_Sheet1.Columns.Get(6).Label = "医保分类代码";
            this.fpSpread1_Sheet1.Columns.Get(6).Visible = false;
            this.fpSpread1_Sheet1.Columns.Get(7).Label = "规格";
            this.fpSpread1_Sheet1.Columns.Get(7).Width = 119F;
            this.fpSpread1_Sheet1.Columns.Get(8).Label = "剂型";
            this.fpSpread1_Sheet1.Columns.Get(8).Width = 66F;
            this.fpSpread1_Sheet1.Columns.Get(11).Label = "金额";
            this.fpSpread1_Sheet1.Columns.Get(11).Width = 82F;
            this.fpSpread1_Sheet1.Columns.Get(12).Label = "操作时间";
            this.fpSpread1_Sheet1.Columns.Get(12).Visible = false;
            this.fpSpread1_Sheet1.Columns.Get(12).Width = 70F;
            this.fpSpread1_Sheet1.Columns.Get(13).Label = "备注2";
            this.fpSpread1_Sheet1.Columns.Get(13).Visible = false;
            this.fpSpread1_Sheet1.Columns.Get(14).Label = "备注3";
            this.fpSpread1_Sheet1.Columns.Get(14).Visible = false;
            this.fpSpread1_Sheet1.Columns.Get(15).Label = "读入标记";
            this.fpSpread1_Sheet1.Columns.Get(15).Visible = false;
            this.fpSpread1_Sheet1.Columns.Get(16).Label = "药品来源";
            this.fpSpread1_Sheet1.Columns.Get(16).Visible = false;
            this.fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpSpread1_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpSpread1.SetViewportLeftColumn(0, 0, 1);
            this.fpSpread1.SetActiveViewport(0, 1, 0);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.fpSpread2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(1243, 524);
            this.splitContainer1.SplitterDistance = 388;
            this.splitContainer1.TabIndex = 2;
            // 
            // fpSpread2
            // 
            this.fpSpread2.About = "3.0.2004.2005";
            this.fpSpread2.AccessibleDescription = "fpSpread2, Sheet1, Row 0, Column 0, ";
            this.fpSpread2.BackColor = System.Drawing.SystemColors.Control;
            this.fpSpread2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread2.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread2.Location = new System.Drawing.Point(0, 0);
            this.fpSpread2.Name = "fpSpread2";
            this.fpSpread2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread2.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread2_Sheet1});
            this.fpSpread2.Size = new System.Drawing.Size(388, 524);
            this.fpSpread2.TabIndex = 0;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread2.TextTipAppearance = tipAppearance2;
            this.fpSpread2.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread2.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpSpread2_CellDoubleClick);
            // 
            // fpSpread2_Sheet1
            // 
            this.fpSpread2_Sheet1.Reset();
            this.fpSpread2_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread2_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread2_Sheet1.ColumnCount = 7;
            this.fpSpread2_Sheet1.RowCount = 0;
            this.fpSpread2_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "就诊流水号";
            this.fpSpread2_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "结算编码";
            this.fpSpread2_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "结算类别编码";
            this.fpSpread2_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "姓名";
            this.fpSpread2_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "看诊科室";
            this.fpSpread2_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "看诊时间";
            this.fpSpread2_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "看诊医生";
            this.fpSpread2_Sheet1.Columns.Get(0).Label = "就诊流水号";
            this.fpSpread2_Sheet1.Columns.Get(0).Visible = false;
            this.fpSpread2_Sheet1.Columns.Get(1).Label = "结算编码";
            this.fpSpread2_Sheet1.Columns.Get(1).Visible = false;
            this.fpSpread2_Sheet1.Columns.Get(2).Label = "结算类别编码";
            this.fpSpread2_Sheet1.Columns.Get(2).Visible = false;
            this.fpSpread2_Sheet1.Columns.Get(3).Label = "姓名";
            this.fpSpread2_Sheet1.Columns.Get(3).Width = 72F;
            this.fpSpread2_Sheet1.Columns.Get(4).Label = "看诊科室";
            this.fpSpread2_Sheet1.Columns.Get(4).Width = 91F;
            this.fpSpread2_Sheet1.Columns.Get(5).Label = "看诊时间";
            this.fpSpread2_Sheet1.Columns.Get(5).Width = 90F;
            this.fpSpread2_Sheet1.Columns.Get(6).Label = "看诊医生";
            this.fpSpread2_Sheet1.Columns.Get(6).Width = 75F;
            this.fpSpread2_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
            this.fpSpread2_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpSpread2_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread2_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpSpread2_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpSpread2_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpSpread2_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpSpread2.SetActiveViewport(0, 1, 0);
            // 
            // ucBalance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ucBalance";
            this.Size = new System.Drawing.Size(1243, 524);
            this.Load += new System.EventHandler(this.ucUpLoadFeeDetail_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread2_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private BaseControls.ucOutPatientInfo ucOutPatientInfo1;
        private FarPoint.Win.Spread.FpSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private FarPoint.Win.Spread.FpSpread fpSpread2;
        private FarPoint.Win.Spread.SheetView fpSpread2_Sheet1;
    }
}
