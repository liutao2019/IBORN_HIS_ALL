namespace FoShanDiseasePay
{
    partial class ucItemDZ
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
            this.components = new System.ComponentModel.Container();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.gbTop = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.cmbItemInfo = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lblSBH = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel9 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel7 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.tabMain = new FS.FrameWork.WinForms.Controls.NeuTabControl();
            this.tpPage = new System.Windows.Forms.TabPage();
            this.fpItemInfo = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpItemInfo_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.gbTop.SuspendLayout();
            this.neuGroupBox1.SuspendLayout();
            this.tabMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpItemInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpItemInfo_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // gbTop
            // 
            this.gbTop.Controls.Add(this.cmbItemInfo);
            this.gbTop.Controls.Add(this.lblSBH);
            this.gbTop.Controls.Add(this.neuLabel9);
            this.gbTop.Controls.Add(this.neuLabel7);
            this.gbTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbTop.Location = new System.Drawing.Point(0, 0);
            this.gbTop.Name = "gbTop";
            this.gbTop.Size = new System.Drawing.Size(1152, 86);
            this.gbTop.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbTop.TabIndex = 0;
            this.gbTop.TabStop = false;
            this.gbTop.Text = "操作信息";
            // 
            // cmbItemInfo
            // 
            this.cmbItemInfo.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbItemInfo.FormattingEnabled = true;
            this.cmbItemInfo.IsEnter2Tab = false;
            this.cmbItemInfo.IsFlat = false;
            this.cmbItemInfo.IsLike = true;
            this.cmbItemInfo.IsListOnly = false;
            this.cmbItemInfo.IsPopForm = true;
            this.cmbItemInfo.IsShowCustomerList = false;
            this.cmbItemInfo.IsShowID = false;
            this.cmbItemInfo.IsShowIDAndName = false;
            this.cmbItemInfo.Location = new System.Drawing.Point(140, 28);
            this.cmbItemInfo.Name = "cmbItemInfo";
            this.cmbItemInfo.PopForm = null;
            this.cmbItemInfo.ShowCustomerList = false;
            this.cmbItemInfo.ShowID = false;
            this.cmbItemInfo.Size = new System.Drawing.Size(204, 20);
            this.cmbItemInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbItemInfo.TabIndex = 31;
            this.cmbItemInfo.Tag = "";
            this.cmbItemInfo.ToolBarUse = false;
            // 
            // lblSBH
            // 
            this.lblSBH.IsEnter2Tab = false;
            this.lblSBH.Location = new System.Drawing.Point(461, 27);
            this.lblSBH.Name = "lblSBH";
            this.lblSBH.Size = new System.Drawing.Size(160, 21);
            this.lblSBH.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblSBH.TabIndex = 25;
            // 
            // neuLabel9
            // 
            this.neuLabel9.AutoSize = true;
            this.neuLabel9.Location = new System.Drawing.Point(69, 32);
            this.neuLabel9.Name = "neuLabel9";
            this.neuLabel9.Size = new System.Drawing.Size(77, 12);
            this.neuLabel9.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel9.TabIndex = 24;
            this.neuLabel9.Text = "本院的项目：";
            // 
            // neuLabel7
            // 
            this.neuLabel7.AutoSize = true;
            this.neuLabel7.Location = new System.Drawing.Point(381, 32);
            this.neuLabel7.Name = "neuLabel7";
            this.neuLabel7.Size = new System.Drawing.Size(89, 12);
            this.neuLabel7.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel7.TabIndex = 26;
            this.neuLabel7.Text = "社保项目编码：";
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.tabMain);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 86);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(1152, 727);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 1;
            this.neuGroupBox1.TabStop = false;
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tpPage);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(3, 17);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(1146, 507);
            this.tabMain.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tabMain.TabIndex = 2;
            // 
            // tpPage
            // 
            this.tpPage.Controls.Add(this.fpItemInfo);
            this.tpPage.ForeColor = System.Drawing.Color.Red;
            this.tpPage.Location = new System.Drawing.Point(4, 22);
            this.tpPage.Name = "tpPage";
            this.tpPage.Padding = new System.Windows.Forms.Padding(3);
            this.tpPage.Size = new System.Drawing.Size(1138, 481);
            this.tpPage.TabIndex = 0;
            this.tpPage.Text = "已对照的项目信息";
            this.tpPage.UseVisualStyleBackColor = true;
            // 
            // fpItemInfo
            // 
            this.fpItemInfo.About = "3.0.2004.2005";
            this.fpItemInfo.AccessibleDescription = "fpItemInfo, Sheet1, Row 0, Column 0, ";
            this.fpItemInfo.BackColor = System.Drawing.Color.White;
            this.fpItemInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpItemInfo.FileName = "";
            this.fpItemInfo.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpItemInfo.IsAutoSaveGridStatus = false;
            this.fpItemInfo.IsCanCustomConfigColumn = false;
            this.fpItemInfo.Location = new System.Drawing.Point(3, 3);
            this.fpItemInfo.Name = "fpItemInfo";
            this.fpItemInfo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpItemInfo.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpItemInfo_Sheet1});
            this.fpItemInfo.Size = new System.Drawing.Size(1138, 389);
            this.fpItemInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpItemInfo.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpItemInfo.TextTipAppearance = tipAppearance1;
            this.fpItemInfo.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpItemInfo_Sheet1
            // 
            this.fpItemInfo_Sheet1.Reset();
            this.fpItemInfo_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpItemInfo_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpItemInfo_Sheet1.ColumnCount = 10;
            this.fpItemInfo_Sheet1.RowCount = 1;
            this.fpItemInfo_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "项目编码";
            this.fpItemInfo_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "自定义码";
            this.fpItemInfo_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "国家编码";
            this.fpItemInfo_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "社保项目编码";
            this.fpItemInfo_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "项目名称";
            this.fpItemInfo_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "规格";
            this.fpItemInfo_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "基准价格";
            this.fpItemInfo_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "单位";
            this.fpItemInfo_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "批准文号";
            this.fpItemInfo_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "生产厂家";
            this.fpItemInfo_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.fpItemInfo_Sheet1.Columns.Get(0).Label = "项目编码";
            this.fpItemInfo_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General;
            this.fpItemInfo_Sheet1.Columns.Get(0).Width = 120F;
            this.fpItemInfo_Sheet1.Columns.Get(1).CellType = textCellType1;
            this.fpItemInfo_Sheet1.Columns.Get(1).Label = "自定义码";
            this.fpItemInfo_Sheet1.Columns.Get(1).Locked = true;
            this.fpItemInfo_Sheet1.Columns.Get(1).Width = 120F;
            this.fpItemInfo_Sheet1.Columns.Get(2).CellType = textCellType1;
            this.fpItemInfo_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpItemInfo_Sheet1.Columns.Get(2).Label = "国家编码";
            this.fpItemInfo_Sheet1.Columns.Get(2).Locked = true;
            this.fpItemInfo_Sheet1.Columns.Get(2).Width = 120F;
            this.fpItemInfo_Sheet1.Columns.Get(3).CellType = textCellType1;
            this.fpItemInfo_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpItemInfo_Sheet1.Columns.Get(3).Label = "社保项目编码";
            this.fpItemInfo_Sheet1.Columns.Get(3).Locked = true;
            this.fpItemInfo_Sheet1.Columns.Get(3).Width = 120F;
            this.fpItemInfo_Sheet1.Columns.Get(4).CellType = textCellType1;
            this.fpItemInfo_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpItemInfo_Sheet1.Columns.Get(4).Label = "项目名称";
            this.fpItemInfo_Sheet1.Columns.Get(4).Locked = true;
            this.fpItemInfo_Sheet1.Columns.Get(4).Width = 224F;
            this.fpItemInfo_Sheet1.Columns.Get(5).CellType = textCellType1;
            this.fpItemInfo_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpItemInfo_Sheet1.Columns.Get(5).Label = "规格";
            this.fpItemInfo_Sheet1.Columns.Get(5).Locked = true;
            this.fpItemInfo_Sheet1.Columns.Get(5).Width = 120F;
            this.fpItemInfo_Sheet1.Columns.Get(6).CellType = textCellType1;
            this.fpItemInfo_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpItemInfo_Sheet1.Columns.Get(6).Label = "基准价格";
            this.fpItemInfo_Sheet1.Columns.Get(6).Locked = true;
            this.fpItemInfo_Sheet1.Columns.Get(6).Width = 100F;
            this.fpItemInfo_Sheet1.Columns.Get(7).CellType = textCellType1;
            this.fpItemInfo_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpItemInfo_Sheet1.Columns.Get(7).Label = "单位";
            this.fpItemInfo_Sheet1.Columns.Get(7).Locked = true;
            this.fpItemInfo_Sheet1.Columns.Get(7).Width = 80F;
            this.fpItemInfo_Sheet1.Columns.Get(8).CellType = textCellType1;
            this.fpItemInfo_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpItemInfo_Sheet1.Columns.Get(8).Label = "批准文号";
            this.fpItemInfo_Sheet1.Columns.Get(8).Locked = true;
            this.fpItemInfo_Sheet1.Columns.Get(8).Width = 120F;
            this.fpItemInfo_Sheet1.Columns.Get(9).CellType = textCellType1;
            this.fpItemInfo_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpItemInfo_Sheet1.Columns.Get(9).Label = "生产厂家";
            this.fpItemInfo_Sheet1.Columns.Get(9).Locked = true;
            this.fpItemInfo_Sheet1.Columns.Get(9).Width = 220F;
            this.fpItemInfo_Sheet1.DefaultStyle.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpItemInfo_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpItemInfo_Sheet1.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpItemInfo_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpItemInfo_Sheet1.RowHeader.Columns.Get(0).Width = 30F;
            this.fpItemInfo_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ucItemDZ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuGroupBox1);
            this.Controls.Add(this.gbTop);
            this.Name = "ucItemDZ";
            this.Size = new System.Drawing.Size(1152, 648);
            this.gbTop.ResumeLayout(false);
            this.gbTop.PerformLayout();
            this.neuGroupBox1.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpItemInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpItemInfo_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbTop;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpItemInfo;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel9;
        private FS.FrameWork.WinForms.Controls.NeuTextBox lblSBH;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel7;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbItemInfo;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FarPoint.Win.Spread.SheetView fpItemInfo_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuTabControl tabMain;
        private System.Windows.Forms.TabPage tpPage;
    }
}
