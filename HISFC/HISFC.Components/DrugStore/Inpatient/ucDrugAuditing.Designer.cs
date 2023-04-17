namespace FS.HISFC.Components.DrugStore.Inpatient
{
    partial class ucDrugAuditing
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
            FarPoint.Win.Spread.CellType.ComboBoxCellType comboBoxCellType1 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            this.pnlTop = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.pnlTopConditionTime = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cbbAuditingResult = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuButton1 = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.pnlTopCondition = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dtpBegin = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.chkIsQueryByTime = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.cbbStockDept = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cbbStates = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.dtpEnd = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.pnlMain = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpAuditingList = new FarPoint.Win.Spread.SheetView();
            this.pnlBottom = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.pnlTop.SuspendLayout();
            this.pnlTopConditionTime.SuspendLayout();
            this.pnlTopCondition.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpAuditingList)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.pnlTopConditionTime);
            this.pnlTop.Controls.Add(this.pnlTopCondition);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(800, 69);
            this.pnlTop.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlTop.TabIndex = 0;
            // 
            // pnlTopConditionTime
            // 
            this.pnlTopConditionTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlTopConditionTime.Controls.Add(this.neuLabel4);
            this.pnlTopConditionTime.Controls.Add(this.cbbAuditingResult);
            this.pnlTopConditionTime.Controls.Add(this.neuButton1);
            this.pnlTopConditionTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTopConditionTime.Location = new System.Drawing.Point(0, 39);
            this.pnlTopConditionTime.Name = "pnlTopConditionTime";
            this.pnlTopConditionTime.Size = new System.Drawing.Size(800, 30);
            this.pnlTopConditionTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlTopConditionTime.TabIndex = 12;
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point(7, 7);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(53, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 10;
            this.neuLabel4.Text = "状态选择";
            // 
            // cbbAuditingResult
            // 
            this.cbbAuditingResult.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cbbAuditingResult.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbAuditingResult.FormattingEnabled = true;
            this.cbbAuditingResult.IsEnter2Tab = false;
            this.cbbAuditingResult.IsFlat = false;
            this.cbbAuditingResult.IsLike = true;
            this.cbbAuditingResult.IsListOnly = false;
            this.cbbAuditingResult.IsPopForm = true;
            this.cbbAuditingResult.IsShowCustomerList = false;
            this.cbbAuditingResult.IsShowID = false;
            this.cbbAuditingResult.Location = new System.Drawing.Point(66, 3);
            this.cbbAuditingResult.Name = "cbbAuditingResult";
            this.cbbAuditingResult.PopForm = null;
            this.cbbAuditingResult.ShowCustomerList = false;
            this.cbbAuditingResult.ShowID = false;
            this.cbbAuditingResult.Size = new System.Drawing.Size(121, 20);
            this.cbbAuditingResult.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cbbAuditingResult.TabIndex = 9;
            this.cbbAuditingResult.Tag = "";
            this.cbbAuditingResult.ToolBarUse = false;
            // 
            // neuButton1
            // 
            this.neuButton1.Location = new System.Drawing.Point(196, 2);
            this.neuButton1.Name = "neuButton1";
            this.neuButton1.Size = new System.Drawing.Size(77, 23);
            this.neuButton1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuButton1.TabIndex = 7;
            this.neuButton1.Text = "批量审核";
            this.neuButton1.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuButton1.UseVisualStyleBackColor = true;
            this.neuButton1.Click += new System.EventHandler(this.neuButton1_Click);
            // 
            // pnlTopCondition
            // 
            this.pnlTopCondition.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlTopCondition.Controls.Add(this.neuLabel5);
            this.pnlTopCondition.Controls.Add(this.dtpBegin);
            this.pnlTopCondition.Controls.Add(this.chkIsQueryByTime);
            this.pnlTopCondition.Controls.Add(this.cbbStockDept);
            this.pnlTopCondition.Controls.Add(this.neuLabel3);
            this.pnlTopCondition.Controls.Add(this.cbbStates);
            this.pnlTopCondition.Controls.Add(this.dtpEnd);
            this.pnlTopCondition.Controls.Add(this.neuLabel1);
            this.pnlTopCondition.Controls.Add(this.neuLabel2);
            this.pnlTopCondition.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopCondition.Location = new System.Drawing.Point(0, 0);
            this.pnlTopCondition.Name = "pnlTopCondition";
            this.pnlTopCondition.Size = new System.Drawing.Size(800, 39);
            this.pnlTopCondition.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlTopCondition.TabIndex = 11;
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Location = new System.Drawing.Point(225, 11);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(29, 12);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 11;
            this.neuLabel5.Text = "时间";
            // 
            // dtpBegin
            // 
            this.dtpBegin.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBegin.IsEnter2Tab = false;
            this.dtpBegin.Location = new System.Drawing.Point(260, 7);
            this.dtpBegin.Name = "dtpBegin";
            this.dtpBegin.Size = new System.Drawing.Size(142, 21);
            this.dtpBegin.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpBegin.TabIndex = 2;
            // 
            // chkIsQueryByTime
            // 
            this.chkIsQueryByTime.AutoSize = true;
            this.chkIsQueryByTime.Location = new System.Drawing.Point(389, -8);
            this.chkIsQueryByTime.Name = "chkIsQueryByTime";
            this.chkIsQueryByTime.Size = new System.Drawing.Size(48, 16);
            this.chkIsQueryByTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chkIsQueryByTime.TabIndex = 8;
            this.chkIsQueryByTime.Text = "时间";
            this.chkIsQueryByTime.UseVisualStyleBackColor = true;
            this.chkIsQueryByTime.Visible = false;
            this.chkIsQueryByTime.CheckedChanged += new System.EventHandler(this.chkIsQueryByTime_CheckedChanged);
            // 
            // cbbStockDept
            // 
            this.cbbStockDept.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cbbStockDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbStockDept.FormattingEnabled = true;
            this.cbbStockDept.IsEnter2Tab = false;
            this.cbbStockDept.IsFlat = false;
            this.cbbStockDept.IsLike = true;
            this.cbbStockDept.IsListOnly = false;
            this.cbbStockDept.IsPopForm = true;
            this.cbbStockDept.IsShowCustomerList = false;
            this.cbbStockDept.IsShowID = false;
            this.cbbStockDept.Location = new System.Drawing.Point(66, -8);
            this.cbbStockDept.Name = "cbbStockDept";
            this.cbbStockDept.PopForm = null;
            this.cbbStockDept.ShowCustomerList = false;
            this.cbbStockDept.ShowID = false;
            this.cbbStockDept.Size = new System.Drawing.Size(121, 20);
            this.cbbStockDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cbbStockDept.TabIndex = 0;
            this.cbbStockDept.Tag = "";
            this.cbbStockDept.ToolBarUse = false;
            this.cbbStockDept.Visible = false;
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(408, 12);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(17, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 10;
            this.neuLabel3.Text = "至";
            // 
            // cbbStates
            // 
            this.cbbStates.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cbbStates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbStates.FormattingEnabled = true;
            this.cbbStates.IsEnter2Tab = false;
            this.cbbStates.IsFlat = false;
            this.cbbStates.IsLike = true;
            this.cbbStates.IsListOnly = false;
            this.cbbStates.IsPopForm = true;
            this.cbbStates.IsShowCustomerList = false;
            this.cbbStates.IsShowID = false;
            this.cbbStates.Location = new System.Drawing.Point(66, 8);
            this.cbbStates.Name = "cbbStates";
            this.cbbStates.PopForm = null;
            this.cbbStates.ShowCustomerList = false;
            this.cbbStates.ShowID = false;
            this.cbbStates.Size = new System.Drawing.Size(121, 20);
            this.cbbStates.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cbbStates.TabIndex = 1;
            this.cbbStates.Tag = "";
            this.cbbStates.ToolBarUse = false;
            this.cbbStates.SelectedIndexChanged += new System.EventHandler(this.cbbStates_SelectedIndexChanged);
            // 
            // dtpEnd
            // 
            this.dtpEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd.IsEnter2Tab = false;
            this.dtpEnd.Location = new System.Drawing.Point(431, 7);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(142, 21);
            this.dtpEnd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpEnd.TabIndex = 9;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(7, -4);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(53, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 3;
            this.neuLabel1.Text = "药房选择";
            this.neuLabel1.Visible = false;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(7, 12);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(53, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 4;
            this.neuLabel2.Text = "审核状态";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.neuSpread1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 69);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(800, 214);
            this.pnlMain.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlMain.TabIndex = 1;
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
            this.fpAuditingList});
            this.neuSpread1.Size = new System.Drawing.Size(800, 214);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpAuditingList
            // 
            this.fpAuditingList.Reset();
            this.fpAuditingList.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpAuditingList.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpAuditingList.ColumnCount = 13;
            this.fpAuditingList.RowCount = 1;
            this.fpAuditingList.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(66))))), System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(213)))), ((int)(((byte)(213))))), FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213))))), System.Drawing.Color.Black, System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(153)))), ((int)(((byte)(153))))), System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, true, true, true);
            this.fpAuditingList.ColumnHeader.Cells.Get(0, 0).Value = "患者姓名";
            this.fpAuditingList.ColumnHeader.Cells.Get(0, 1).Value = "科室";
            this.fpAuditingList.ColumnHeader.Cells.Get(0, 2).Value = "药品名称";
            this.fpAuditingList.ColumnHeader.Cells.Get(0, 3).Value = "规格";
            this.fpAuditingList.ColumnHeader.Cells.Get(0, 4).Value = "申请量";
            this.fpAuditingList.ColumnHeader.Cells.Get(0, 5).Value = "单位";
            this.fpAuditingList.ColumnHeader.Cells.Get(0, 6).Value = "频次";
            this.fpAuditingList.ColumnHeader.Cells.Get(0, 7).Value = "用法";
            this.fpAuditingList.ColumnHeader.Cells.Get(0, 8).Value = "状态";
            this.fpAuditingList.ColumnHeader.Cells.Get(0, 9).Value = "审核";
            this.fpAuditingList.ColumnHeader.Cells.Get(0, 10).Value = "备注";
            this.fpAuditingList.ColumnHeader.Cells.Get(0, 11).Value = "审核人";
            this.fpAuditingList.ColumnHeader.Cells.Get(0, 12).Value = "审核时间";
            this.fpAuditingList.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.fpAuditingList.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpAuditingList.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.fpAuditingList.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpAuditingList.Columns.Get(0).Label = "患者姓名";
            this.fpAuditingList.Columns.Get(0).Width = 70F;
            this.fpAuditingList.Columns.Get(1).Label = "科室";
            this.fpAuditingList.Columns.Get(1).Width = 80F;
            this.fpAuditingList.Columns.Get(2).Label = "药品名称";
            this.fpAuditingList.Columns.Get(2).Width = 120F;
            this.fpAuditingList.Columns.Get(4).Label = "申请量";
            this.fpAuditingList.Columns.Get(4).Width = 50F;
            this.fpAuditingList.Columns.Get(5).Label = "单位";
            this.fpAuditingList.Columns.Get(5).Width = 35F;
            this.fpAuditingList.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
            this.fpAuditingList.Columns.Get(8).Label = "状态";
            this.fpAuditingList.Columns.Get(8).Width = 50F;
            comboBoxCellType1.ButtonAlign = FarPoint.Win.ButtonAlign.Right;
            this.fpAuditingList.Columns.Get(9).CellType = comboBoxCellType1;
            this.fpAuditingList.Columns.Get(9).Label = "审核";
            this.fpAuditingList.Columns.Get(9).Width = 80F;
            this.fpAuditingList.Columns.Get(10).Label = "备注";
            this.fpAuditingList.Columns.Get(10).Width = 80F;
            this.fpAuditingList.Columns.Get(11).Label = "审核人";
            this.fpAuditingList.Columns.Get(11).Width = 50F;
            this.fpAuditingList.Columns.Get(12).Label = "审核时间";
            this.fpAuditingList.Columns.Get(12).Width = 110F;
            this.fpAuditingList.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpAuditingList.DefaultStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(66)))));
            this.fpAuditingList.RowHeader.Columns.Default.Resizable = false;
            this.fpAuditingList.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.fpAuditingList.RowHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpAuditingList.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.fpAuditingList.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpAuditingList.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.MultiRange;
            this.fpAuditingList.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpAuditingList.SheetCornerStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.fpAuditingList.SheetCornerStyle.ForeColor = System.Drawing.Color.Black;
            this.fpAuditingList.SheetCornerStyle.Parent = "CornerDefault";
            this.fpAuditingList.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpAuditingList.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuSpread1.SetViewportLeftColumn(0, 0, 2);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 283);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(800, 29);
            this.pnlBottom.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlBottom.TabIndex = 2;
            // 
            // ucDrugAuditing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);
            this.Name = "ucDrugAuditing";
            this.Size = new System.Drawing.Size(800, 312);
            this.pnlTop.ResumeLayout(false);
            this.pnlTopConditionTime.ResumeLayout(false);
            this.pnlTopConditionTime.PerformLayout();
            this.pnlTopCondition.ResumeLayout(false);
            this.pnlTopCondition.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpAuditingList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel pnlTop;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlMain;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlBottom;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpBegin;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cbbStates;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cbbStockDept;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlTopCondition;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpEnd;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox chkIsQueryByTime;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlTopConditionTime;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView fpAuditingList;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cbbAuditingResult;
        private FS.FrameWork.WinForms.Controls.NeuButton neuButton1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
    }
}
