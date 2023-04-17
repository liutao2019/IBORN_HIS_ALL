namespace API.GZSI.UI
{
    partial class ucInPatientYDUpload
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
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType1 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType2 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType3 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dtpMonth = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.fpList = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpList_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel2.SuspendLayout();
            this.neuPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpList_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(19, 28);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(65, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "清分年月：";
            // 
            // dtpMonth
            // 
            this.dtpMonth.CustomFormat = "yyyy-MM";
            this.dtpMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpMonth.IsEnter2Tab = false;
            this.dtpMonth.Location = new System.Drawing.Point(90, 22);
            this.dtpMonth.Name = "dtpMonth";
            this.dtpMonth.Size = new System.Drawing.Size(77, 21);
            this.dtpMonth.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpMonth.TabIndex = 1;
            // 
            // neuPanel2
            // 
            this.neuPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.neuPanel2.Controls.Add(this.dtpMonth);
            this.neuPanel2.Controls.Add(this.neuLabel1);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel2.Location = new System.Drawing.Point(0, 0);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(1067, 74);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 4;
            // 
            // neuPanel1
            // 
            this.neuPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.neuPanel1.Controls.Add(this.fpList);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel1.Location = new System.Drawing.Point(0, 74);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(1067, 337);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 5;
            // 
            // fpList
            // 
            this.fpList.About = "3.0.2004.2005";
            this.fpList.AccessibleDescription = "fpUploaded, Sheet1";
            this.fpList.BackColor = System.Drawing.Color.White;
            this.fpList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpList.FileName = "";
            this.fpList.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpList.IsAutoSaveGridStatus = false;
            this.fpList.IsCanCustomConfigColumn = false;
            this.fpList.Location = new System.Drawing.Point(0, 0);
            this.fpList.Name = "fpList";
            this.fpList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpList.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpList_Sheet1});
            this.fpList.Size = new System.Drawing.Size(1063, 333);
            this.fpList.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpList.TabIndex = 1;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpList.TextTipAppearance = tipAppearance1;
            this.fpList.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpList_Sheet1
            // 
            this.fpList_Sheet1.Reset();
            this.fpList_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpList_Sheet1.ColumnCount = 13;
            this.fpList_Sheet1.RowCount = 0;
            this.fpList_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "类别";
            this.fpList_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "病历号";
            this.fpList_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "姓名";
            this.fpList_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "证件号码";
            this.fpList_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "性别";
            this.fpList_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "结算类型";
            this.fpList_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "发票号";
            this.fpList_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "总金额";
            this.fpList_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "统筹金额";
            this.fpList_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "自费金额";
            this.fpList_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "结算员";
            this.fpList_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "出院时间";
            this.fpList_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "结算时间";
            this.fpList_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpList_Sheet1.Columns.Get(0).Label = "类别";
            this.fpList_Sheet1.Columns.Get(0).Visible = false;
            this.fpList_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpList_Sheet1.Columns.Get(1).Label = "病历号";
            this.fpList_Sheet1.Columns.Get(1).Width = 90F;
            this.fpList_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpList_Sheet1.Columns.Get(2).Label = "姓名";
            this.fpList_Sheet1.Columns.Get(2).Width = 150F;
            this.fpList_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpList_Sheet1.Columns.Get(3).Label = "证件号码";
            this.fpList_Sheet1.Columns.Get(3).Width = 150F;
            this.fpList_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpList_Sheet1.Columns.Get(4).Label = "性别";
            this.fpList_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpList_Sheet1.Columns.Get(5).Label = "结算类型";
            this.fpList_Sheet1.Columns.Get(5).Width = 90F;
            this.fpList_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpList_Sheet1.Columns.Get(6).Label = "发票号";
            this.fpList_Sheet1.Columns.Get(6).Width = 120F;
            this.fpList_Sheet1.Columns.Get(7).CellType = currencyCellType1;
            this.fpList_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.fpList_Sheet1.Columns.Get(7).Label = "总金额";
            this.fpList_Sheet1.Columns.Get(7).Width = 72F;
            this.fpList_Sheet1.Columns.Get(8).CellType = currencyCellType2;
            this.fpList_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.fpList_Sheet1.Columns.Get(8).Label = "统筹金额";
            this.fpList_Sheet1.Columns.Get(8).Width = 72F;
            this.fpList_Sheet1.Columns.Get(9).CellType = currencyCellType3;
            this.fpList_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.fpList_Sheet1.Columns.Get(9).Label = "自费金额";
            this.fpList_Sheet1.Columns.Get(9).Width = 72F;
            this.fpList_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpList_Sheet1.Columns.Get(10).Label = "结算员";
            this.fpList_Sheet1.Columns.Get(10).Width = 90F;
            this.fpList_Sheet1.Columns.Get(11).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpList_Sheet1.Columns.Get(11).Label = "出院时间";
            this.fpList_Sheet1.Columns.Get(11).Width = 150F;
            this.fpList_Sheet1.Columns.Get(12).Label = "结算时间";
            this.fpList_Sheet1.Columns.Get(12).Width = 150F;
            this.fpList_Sheet1.DefaultStyle.CellType = textCellType1;
            this.fpList_Sheet1.DefaultStyle.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpList_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpList_Sheet1.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpList_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
            this.fpList_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpList_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpList_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpList_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpList_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpList.SetActiveViewport(0, 1, 0);
            // 
            // ucInPatientYDUpload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuPanel1);
            this.Controls.Add(this.neuPanel2);
            this.Name = "ucInPatientYDUpload";
            this.Size = new System.Drawing.Size(1067, 411);
            this.neuPanel2.ResumeLayout(false);
            this.neuPanel2.PerformLayout();
            this.neuPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpList_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpMonth;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FarPoint.Win.Spread.SheetView fpList_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpList;


    }
}
