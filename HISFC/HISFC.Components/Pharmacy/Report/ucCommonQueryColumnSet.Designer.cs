namespace FS.HISFC.Components.Pharmacy.Report
{
    partial class ucCommonQueryColumnSet
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
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType2 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.pnlMainList = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpDataView = new FarPoint.Win.Spread.SheetView();
            this.pnlEdit = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.chkShowTotal = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.chkShowColumn = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtColumnWidth = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtColumnName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cbbColumnType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.pnlBottom = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuButton4 = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuButton3 = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuButton2 = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuButton1 = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.pnlMainList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpDataView)).BeginInit();
            this.pnlEdit.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMainList
            // 
            this.pnlMainList.Controls.Add(this.neuSpread1);
            this.pnlMainList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainList.Location = new System.Drawing.Point(0, 0);
            this.pnlMainList.Name = "pnlMainList";
            this.pnlMainList.Size = new System.Drawing.Size(312, 216);
            this.pnlMainList.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlMainList.TabIndex = 0;
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
            this.fpDataView});
            this.neuSpread1.Size = new System.Drawing.Size(312, 216);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 1;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.neuSpread1_CellDoubleClick);
            // 
            // fpDataView
            // 
            this.fpDataView.Reset();
            this.fpDataView.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpDataView.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpDataView.ColumnCount = 5;
            this.fpDataView.RowCount = 1;
            this.fpDataView.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.Navy, System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255))))), FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239))))), System.Drawing.Color.Black, System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(153))))), System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, true, true, true);
            this.fpDataView.ColumnHeader.Cells.Get(0, 0).Value = "列名";
            this.fpDataView.ColumnHeader.Cells.Get(0, 1).Value = "宽度";
            this.fpDataView.ColumnHeader.Cells.Get(0, 2).Value = "显示";
            this.fpDataView.ColumnHeader.Cells.Get(0, 3).Value = "合计项";
            this.fpDataView.ColumnHeader.Cells.Get(0, 4).Value = "列类型";
            this.fpDataView.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.fpDataView.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpDataView.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.fpDataView.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpDataView.Columns.Get(0).CellType = textCellType1;
            this.fpDataView.Columns.Get(0).Label = "列名";
            this.fpDataView.Columns.Get(0).Locked = true;
            this.fpDataView.Columns.Get(0).Width = 70F;
            this.fpDataView.Columns.Get(1).Label = "宽度";
            this.fpDataView.Columns.Get(1).Locked = true;
            this.fpDataView.Columns.Get(1).Width = 35F;
            this.fpDataView.Columns.Get(2).CellType = checkBoxCellType1;
            this.fpDataView.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpDataView.Columns.Get(2).Label = "显示";
            this.fpDataView.Columns.Get(2).Locked = true;
            this.fpDataView.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpDataView.Columns.Get(2).Width = 35F;
            this.fpDataView.Columns.Get(3).CellType = checkBoxCellType2;
            this.fpDataView.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpDataView.Columns.Get(3).Label = "合计项";
            this.fpDataView.Columns.Get(3).Locked = true;
            this.fpDataView.Columns.Get(3).Width = 50F;
            this.fpDataView.Columns.Get(4).Label = "列类型";
            this.fpDataView.Columns.Get(4).Locked = true;
            this.fpDataView.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpDataView.DefaultStyle.ForeColor = System.Drawing.Color.Navy;
            this.fpDataView.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpDataView.RowHeader.Columns.Default.Resizable = false;
            this.fpDataView.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.fpDataView.RowHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpDataView.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.fpDataView.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpDataView.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpDataView.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpDataView.SheetCornerStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.fpDataView.SheetCornerStyle.ForeColor = System.Drawing.Color.Black;
            this.fpDataView.SheetCornerStyle.Parent = "CornerDefault";
            this.fpDataView.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpDataView.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // pnlEdit
            // 
            this.pnlEdit.Controls.Add(this.chkShowTotal);
            this.pnlEdit.Controls.Add(this.neuLabel4);
            this.pnlEdit.Controls.Add(this.chkShowColumn);
            this.pnlEdit.Controls.Add(this.neuLabel3);
            this.pnlEdit.Controls.Add(this.txtColumnWidth);
            this.pnlEdit.Controls.Add(this.neuLabel2);
            this.pnlEdit.Controls.Add(this.txtColumnName);
            this.pnlEdit.Controls.Add(this.neuLabel1);
            this.pnlEdit.Controls.Add(this.cbbColumnType);
            this.pnlEdit.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlEdit.Location = new System.Drawing.Point(0, 216);
            this.pnlEdit.Name = "pnlEdit";
            this.pnlEdit.Size = new System.Drawing.Size(312, 82);
            this.pnlEdit.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlEdit.TabIndex = 1;
            // 
            // chkShowTotal
            // 
            this.chkShowTotal.AutoSize = true;
            this.chkShowTotal.Location = new System.Drawing.Point(225, 49);
            this.chkShowTotal.Name = "chkShowTotal";
            this.chkShowTotal.Size = new System.Drawing.Size(60, 16);
            this.chkShowTotal.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chkShowTotal.TabIndex = 8;
            this.chkShowTotal.Text = "统计项";
            this.chkShowTotal.UseVisualStyleBackColor = true;
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point(256, 20);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(29, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 7;
            this.neuLabel4.Text = "像素";
            // 
            // chkShowColumn
            // 
            this.chkShowColumn.AutoSize = true;
            this.chkShowColumn.Checked = true;
            this.chkShowColumn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowColumn.Location = new System.Drawing.Point(165, 49);
            this.chkShowColumn.Name = "chkShowColumn";
            this.chkShowColumn.Size = new System.Drawing.Size(48, 16);
            this.chkShowColumn.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chkShowColumn.TabIndex = 6;
            this.chkShowColumn.Text = "显示";
            this.chkShowColumn.UseVisualStyleBackColor = true;
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(9, 52);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(29, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 5;
            this.neuLabel3.Text = "类型";
            // 
            // txtColumnWidth
            // 
            this.txtColumnWidth.IsEnter2Tab = false;
            this.txtColumnWidth.Location = new System.Drawing.Point(198, 15);
            this.txtColumnWidth.Name = "txtColumnWidth";
            this.txtColumnWidth.Size = new System.Drawing.Size(58, 21);
            this.txtColumnWidth.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtColumnWidth.TabIndex = 4;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(163, 20);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(29, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 3;
            this.neuLabel2.Text = "宽度";
            // 
            // txtColumnName
            // 
            this.txtColumnName.IsEnter2Tab = false;
            this.txtColumnName.Location = new System.Drawing.Point(44, 15);
            this.txtColumnName.Name = "txtColumnName";
            this.txtColumnName.Size = new System.Drawing.Size(100, 21);
            this.txtColumnName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtColumnName.TabIndex = 2;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(9, 20);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(29, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 1;
            this.neuLabel1.Text = "列名";
            // 
            // cbbColumnType
            // 
            this.cbbColumnType.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cbbColumnType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbColumnType.FormattingEnabled = true;
            this.cbbColumnType.IsEnter2Tab = false;
            this.cbbColumnType.IsFlat = false;
            this.cbbColumnType.IsLike = true;
            this.cbbColumnType.IsListOnly = false;
            this.cbbColumnType.IsPopForm = true;
            this.cbbColumnType.IsShowCustomerList = false;
            this.cbbColumnType.IsShowID = false;
            this.cbbColumnType.Items.AddRange(new object[] {
            "系统默认",
            "文本型",
            "数值型",
            "布尔型"});
            this.cbbColumnType.Location = new System.Drawing.Point(44, 47);
            this.cbbColumnType.Name = "cbbColumnType";
            this.cbbColumnType.PopForm = null;
            this.cbbColumnType.ShowCustomerList = false;
            this.cbbColumnType.ShowID = false;
            this.cbbColumnType.Size = new System.Drawing.Size(100, 20);
            this.cbbColumnType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cbbColumnType.TabIndex = 0;
            this.cbbColumnType.Tag = "";
            this.cbbColumnType.ToolBarUse = false;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.neuButton4);
            this.pnlBottom.Controls.Add(this.neuButton3);
            this.pnlBottom.Controls.Add(this.neuButton2);
            this.pnlBottom.Controls.Add(this.neuButton1);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 298);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(312, 41);
            this.pnlBottom.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlBottom.TabIndex = 2;
            // 
            // neuButton4
            // 
            this.neuButton4.Location = new System.Drawing.Point(242, 9);
            this.neuButton4.Name = "neuButton4";
            this.neuButton4.Size = new System.Drawing.Size(60, 23);
            this.neuButton4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuButton4.TabIndex = 3;
            this.neuButton4.Text = "关闭";
            this.neuButton4.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuButton4.UseVisualStyleBackColor = true;
            this.neuButton4.Click += new System.EventHandler(this.neuButton4_Click);
            // 
            // neuButton3
            // 
            this.neuButton3.Location = new System.Drawing.Point(176, 9);
            this.neuButton3.Name = "neuButton3";
            this.neuButton3.Size = new System.Drawing.Size(60, 23);
            this.neuButton3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuButton3.TabIndex = 2;
            this.neuButton3.Text = "保存";
            this.neuButton3.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuButton3.UseVisualStyleBackColor = true;
            this.neuButton3.Click += new System.EventHandler(this.neuButton3_Click);
            // 
            // neuButton2
            // 
            this.neuButton2.Location = new System.Drawing.Point(110, 9);
            this.neuButton2.Name = "neuButton2";
            this.neuButton2.Size = new System.Drawing.Size(60, 23);
            this.neuButton2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuButton2.TabIndex = 1;
            this.neuButton2.Text = "删除";
            this.neuButton2.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuButton2.UseVisualStyleBackColor = true;
            this.neuButton2.Click += new System.EventHandler(this.neuButton2_Click);
            // 
            // neuButton1
            // 
            this.neuButton1.Location = new System.Drawing.Point(44, 9);
            this.neuButton1.Name = "neuButton1";
            this.neuButton1.Size = new System.Drawing.Size(60, 23);
            this.neuButton1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuButton1.TabIndex = 0;
            this.neuButton1.Text = "添加";
            this.neuButton1.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuButton1.UseVisualStyleBackColor = true;
            this.neuButton1.Click += new System.EventHandler(this.neuButton1_Click);
            // 
            // ucCommonQueryColumnSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMainList);
            this.Controls.Add(this.pnlEdit);
            this.Controls.Add(this.pnlBottom);
            this.Name = "ucCommonQueryColumnSet";
            this.Size = new System.Drawing.Size(312, 339);
            this.pnlMainList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpDataView)).EndInit();
            this.pnlEdit.ResumeLayout(false);
            this.pnlEdit.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel pnlMainList;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlEdit;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlBottom;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView fpDataView;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtColumnName;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cbbColumnType;
        private FS.FrameWork.WinForms.Controls.NeuButton neuButton4;
        private FS.FrameWork.WinForms.Controls.NeuButton neuButton3;
        private FS.FrameWork.WinForms.Controls.NeuButton neuButton2;
        private FS.FrameWork.WinForms.Controls.NeuButton neuButton1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox chkShowColumn;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtColumnWidth;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox chkShowTotal;
    }
}
