namespace FS.SOC.HISFC.Components.DrugStore.Compound
{
    partial class ucCompoundConfirmFee
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
            FarPoint.Win.Spread.TipAppearance tipAppearance3 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType2 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.TipAppearance tipAppearance4 = new FarPoint.Win.Spread.TipAppearance();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbDept = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.ckDrugBill = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtGroup = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtDrugedBill = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.dtStart = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dtEnd = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSplitter1 = new FS.FrameWork.WinForms.Controls.NeuSplitter();
            this.neuSpread2 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.sheetView1 = new FarPoint.Win.Spread.SheetView();
            this.fpCompoundSelect = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpCompoundSelect_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel1.SuspendLayout();
            this.neuGroupBox1.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpCompoundSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpCompoundSelect_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.BackColor = System.Drawing.Color.White;
            this.neuPanel1.Controls.Add(this.neuGroupBox1);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(800, 71);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.neuLabel1);
            this.neuGroupBox1.Controls.Add(this.cmbDept);
            this.neuGroupBox1.Controls.Add(this.ckDrugBill);
            this.neuGroupBox1.Controls.Add(this.neuLabel4);
            this.neuGroupBox1.Controls.Add(this.txtGroup);
            this.neuGroupBox1.Controls.Add(this.txtDrugedBill);
            this.neuGroupBox1.Controls.Add(this.dtStart);
            this.neuGroupBox1.Controls.Add(this.neuLabel3);
            this.neuGroupBox1.Controls.Add(this.dtEnd);
            this.neuGroupBox1.Controls.Add(this.neuLabel2);
            this.neuGroupBox1.Controls.Add(this.lbInfo);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(800, 71);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 0;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "查询条件";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel1.Location = new System.Drawing.Point(626, 22);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(53, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 20;
            this.neuLabel1.Text = "申请科室";
            // 
            // cmbDept
            // 
            this.cmbDept.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbDept.FormattingEnabled = true;
            this.cmbDept.IsEnter2Tab = false;
            this.cmbDept.IsFlat = false;
            //this.cmbDept.IsLeftPad = false;
            this.cmbDept.IsLike = true;
            this.cmbDept.IsListOnly = false;
            this.cmbDept.IsPopForm = true;
            this.cmbDept.IsShowCustomerList = false;
            this.cmbDept.IsShowID = false;
            this.cmbDept.Location = new System.Drawing.Point(688, 18);
            this.cmbDept.Name = "cmbDept";
            this.cmbDept.PopForm = null;
            this.cmbDept.ShowCustomerList = false;
            this.cmbDept.ShowID = false;
            this.cmbDept.Size = new System.Drawing.Size(91, 20);
            this.cmbDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbDept.TabIndex = 19;
            this.cmbDept.Tag = "";
            this.cmbDept.ToolBarUse = false;
            // 
            // ckDrugBill
            // 
            this.ckDrugBill.AutoSize = true;
            this.ckDrugBill.ForeColor = System.Drawing.Color.Blue;
            this.ckDrugBill.Location = new System.Drawing.Point(455, 20);
            this.ckDrugBill.Name = "ckDrugBill";
            this.ckDrugBill.Size = new System.Drawing.Size(60, 16);
            this.ckDrugBill.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ckDrugBill.TabIndex = 18;
            this.ckDrugBill.Text = "单据号";
            this.ckDrugBill.UseVisualStyleBackColor = true;
            this.ckDrugBill.CheckedChanged += new System.EventHandler(this.ckDrugBill_CheckedChanged);
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel4.Location = new System.Drawing.Point(16, 49);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(41, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 17;
            this.neuLabel4.Text = "批次号";
            // 
            // txtGroup
            // 
            //this.txtGroup.Interval = 1500;
            this.txtGroup.IsEnter2Tab = false;
            //this.txtGroup.IsStopPaste = false;
            //this.txtGroup.IsTimerDel = false;
            //this.txtGroup.IsUseTimer = false;
            this.txtGroup.Location = new System.Drawing.Point(73, 45);
            this.txtGroup.Name = "txtGroup";
            //this.txtGroup.SendKey = System.Windows.Forms.Keys.Return;
            this.txtGroup.Size = new System.Drawing.Size(156, 21);
            this.txtGroup.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtGroup.TabIndex = 16;
            this.txtGroup.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGroup_KeyPress);
            // 
            // txtDrugedBill
            // 
            //this.txtDrugedBill.Interval = 1500;
            this.txtDrugedBill.IsEnter2Tab = false;
            //this.txtDrugedBill.IsStopPaste = false;
            //this.txtDrugedBill.IsTimerDel = false;
            //this.txtDrugedBill.IsUseTimer = false;
            this.txtDrugedBill.Location = new System.Drawing.Point(520, 18);
            this.txtDrugedBill.Name = "txtDrugedBill";
            //this.txtDrugedBill.SendKey = System.Windows.Forms.Keys.Return;
            this.txtDrugedBill.Size = new System.Drawing.Size(100, 21);
            this.txtDrugedBill.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtDrugedBill.TabIndex = 14;
            // 
            // dtStart
            // 
            this.dtStart.Cursor = System.Windows.Forms.Cursors.Default;
            this.dtStart.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtStart.IsEnter2Tab = false;
            this.dtStart.Location = new System.Drawing.Point(73, 18);
            this.dtStart.Name = "dtStart";
            this.dtStart.Size = new System.Drawing.Size(156, 21);
            this.dtStart.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtStart.TabIndex = 13;
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel3.Location = new System.Drawing.Point(16, 23);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(53, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 12;
            this.neuLabel3.Text = "开始时间";
            // 
            // dtEnd
            // 
            this.dtEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEnd.IsEnter2Tab = false;
            this.dtEnd.Location = new System.Drawing.Point(294, 18);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(152, 21);
            this.dtEnd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtEnd.TabIndex = 11;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel2.Location = new System.Drawing.Point(234, 22);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(53, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 10;
            this.neuLabel2.Text = "截至时间";
            // 
            // lbInfo
            // 
            this.lbInfo.AutoSize = true;
            this.lbInfo.ForeColor = System.Drawing.Color.Blue;
            this.lbInfo.Location = new System.Drawing.Point(229, 23);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(0, 12);
            this.lbInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbInfo.TabIndex = 9;
            this.lbInfo.Visible = false;
            // 
            // neuPanel2
            // 
            this.neuPanel2.BackColor = System.Drawing.Color.White;
            this.neuPanel2.Controls.Add(this.neuSplitter1);
            this.neuPanel2.Controls.Add(this.neuSpread2);
            this.neuPanel2.Controls.Add(this.fpCompoundSelect);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel2.Location = new System.Drawing.Point(0, 71);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(800, 529);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 1;
            // 
            // neuSplitter1
            // 
            this.neuSplitter1.Location = new System.Drawing.Point(418, 0);
            this.neuSplitter1.Name = "neuSplitter1";
            this.neuSplitter1.Size = new System.Drawing.Size(4, 529);
            this.neuSplitter1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSplitter1.TabIndex = 19;
            this.neuSplitter1.TabStop = false;
            // 
            // neuSpread2
            // 
            this.neuSpread2.About = "3.0.2004.2005";
            this.neuSpread2.AccessibleDescription = "neuSpread2, Sheet1";
            this.neuSpread2.BackColor = System.Drawing.Color.White;
            this.neuSpread2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread2.FileName = "";
            this.neuSpread2.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread2.IsAutoSaveGridStatus = false;
            this.neuSpread2.IsCanCustomConfigColumn = false;
            this.neuSpread2.Location = new System.Drawing.Point(418, 0);
            this.neuSpread2.Name = "neuSpread2";
            this.neuSpread2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread2.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.sheetView1});
            this.neuSpread2.Size = new System.Drawing.Size(382, 529);
            this.neuSpread2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread2.TabIndex = 1;
            tipAppearance3.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance3.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread2.TextTipAppearance = tipAppearance3;
            this.neuSpread2.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // sheetView1
            // 
            this.sheetView1.Reset();
            this.sheetView1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.sheetView1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.sheetView1.ColumnCount = 9;
            this.sheetView1.RowCount = 0;
            this.sheetView1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.sheetView1.ColumnHeader.Cells.Get(0, 0).Value = "批次号";
            this.sheetView1.ColumnHeader.Cells.Get(0, 1).Value = "药品名称";
            this.sheetView1.ColumnHeader.Cells.Get(0, 2).Value = "数量";
            this.sheetView1.ColumnHeader.Cells.Get(0, 3).Value = "用量";
            this.sheetView1.ColumnHeader.Cells.Get(0, 4).Value = "用药时间";
            this.sheetView1.ColumnHeader.Cells.Get(0, 5).Value = "附加费用";
            this.sheetView1.ColumnHeader.Cells.Get(0, 6).Value = "单价";
            this.sheetView1.ColumnHeader.Cells.Get(0, 7).Value = "数量";
            this.sheetView1.ColumnHeader.Cells.Get(0, 8).Value = "是否收费";
            this.sheetView1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.sheetView1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.sheetView1.Columns.Get(0).Label = "批次号";
            this.sheetView1.Columns.Get(0).MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.Always;
            this.sheetView1.Columns.Get(0).Width = 90F;
            this.sheetView1.Columns.Get(1).Label = "药品名称";
            this.sheetView1.Columns.Get(1).Width = 120F;
            this.sheetView1.Columns.Get(4).Label = "用药时间";
            this.sheetView1.Columns.Get(4).Width = 73F;
            this.sheetView1.Columns.Get(5).Label = "附加费用";
            this.sheetView1.Columns.Get(5).Width = 96F;
            this.sheetView1.Columns.Get(8).CellType = checkBoxCellType2;
            this.sheetView1.Columns.Get(8).Label = "是否收费";
            this.sheetView1.Columns.Get(8).Width = 56F;
            this.sheetView1.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.sheetView1.RowHeader.Columns.Default.Resizable = false;
            this.sheetView1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.sheetView1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.sheetView1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.sheetView1.SheetCornerStyle.Parent = "CornerDefault";
            this.sheetView1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.sheetView1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuSpread2.SetActiveViewport(0, 1, 0);
            // 
            // fpCompoundSelect
            // 
            this.fpCompoundSelect.About = "3.0.2004.2005";
            this.fpCompoundSelect.AccessibleDescription = "fpCompoundSelect, Sheet1, Row 0, Column 0, ";
            this.fpCompoundSelect.BackColor = System.Drawing.Color.White;
            this.fpCompoundSelect.Dock = System.Windows.Forms.DockStyle.Left;
            this.fpCompoundSelect.FileName = "";
            this.fpCompoundSelect.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpCompoundSelect.IsAutoSaveGridStatus = false;
            this.fpCompoundSelect.IsCanCustomConfigColumn = false;
            this.fpCompoundSelect.Location = new System.Drawing.Point(0, 0);
            this.fpCompoundSelect.Name = "fpCompoundSelect";
            this.fpCompoundSelect.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpCompoundSelect.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpCompoundSelect_Sheet1});
            this.fpCompoundSelect.Size = new System.Drawing.Size(418, 529);
            this.fpCompoundSelect.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpCompoundSelect.TabIndex = 0;
            tipAppearance4.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance4.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpCompoundSelect.TextTipAppearance = tipAppearance4;
            this.fpCompoundSelect.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpCompoundSelect_Sheet1
            // 
            this.fpCompoundSelect_Sheet1.Reset();
            this.fpCompoundSelect_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpCompoundSelect_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpCompoundSelect_Sheet1.ColumnCount = 5;
            this.fpCompoundSelect_Sheet1.RowCount = 0;
            this.fpCompoundSelect_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpCompoundSelect_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "批次号";
            this.fpCompoundSelect_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "药品名称";
            this.fpCompoundSelect_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "数量";
            this.fpCompoundSelect_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "用量";
            this.fpCompoundSelect_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "用药时间";
            this.fpCompoundSelect_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpCompoundSelect_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpCompoundSelect_Sheet1.Columns.Get(0).Label = "批次号";
            this.fpCompoundSelect_Sheet1.Columns.Get(0).MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.Always;
            this.fpCompoundSelect_Sheet1.Columns.Get(1).Label = "药品名称";
            this.fpCompoundSelect_Sheet1.Columns.Get(1).Width = 139F;
            this.fpCompoundSelect_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpCompoundSelect_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpCompoundSelect_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpCompoundSelect_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpCompoundSelect_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.fpCompoundSelect_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.fpCompoundSelect_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpCompoundSelect_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpCompoundSelect.SetActiveViewport(0, 1, 0);
            // 
            // ucCompoundConfirmFee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuPanel2);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucCompoundConfirmFee";
            this.Size = new System.Drawing.Size(800, 600);
            this.neuPanel1.ResumeLayout(false);
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.neuPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpCompoundSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpCompoundSelect_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtStart;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtEnd;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbInfo;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtDrugedBill;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtGroup;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView fpCompoundSelect_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ckDrugBill;
        private FS.FrameWork.WinForms.Controls.NeuSplitter neuSplitter1;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread2;
        private FarPoint.Win.Spread.SheetView sheetView1;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpCompoundSelect;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDept;
    }
}
