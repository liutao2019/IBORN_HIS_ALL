namespace FS.HISFC.Components.Common.Forms
{
    partial class frmSearchPatient
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。

        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSearchPatient));
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType7 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType8 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.neuToolBar1 = new FS.FrameWork.WinForms.Controls.NeuToolBar();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton4 = new System.Windows.Forms.ToolBarButton();
            this.imageList32 = new System.Windows.Forms.ImageList(this.components);
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuTabControl2 = new FS.FrameWork.WinForms.Controls.NeuTabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ucCustomQuery1 = new FS.HISFC.Components.HealthRecord.Search.ucQuery("MetCasBaseCols");
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.ucCustomQuery2 = new FS.HISFC.Components.HealthRecord.Search.ucQuery("MetDiagCols");
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.ucCustomQuery3 = new FS.HISFC.Components.HealthRecord.Search.ucQuery("MetOpsCols");
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuTabControl1 = new FS.FrameWork.WinForms.Controls.NeuTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel1.SuspendLayout();
            this.neuTabControl2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            this.neuTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // neuToolBar1
            // 
            this.neuToolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.toolBarButton1,
            this.toolBarButton2,
            this.toolBarButton4});
            this.neuToolBar1.DropDownArrows = true;
            this.neuToolBar1.ImageList = this.imageList32;
            this.neuToolBar1.Location = new System.Drawing.Point(0, 0);
            this.neuToolBar1.Name = "neuToolBar1";
            this.neuToolBar1.ShowToolTips = true;
            this.neuToolBar1.Size = new System.Drawing.Size(706, 57);
            this.neuToolBar1.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.neuToolBar1.TabIndex = 2;
            this.neuToolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.neuToolBar1_ButtonClick);
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.ImageIndex = 2;
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Text = "查询";
            // 
            // toolBarButton2
            // 
            this.toolBarButton2.ImageIndex = 1;
            this.toolBarButton2.Name = "toolBarButton2";
            this.toolBarButton2.Text = "重置";
            // 
            // toolBarButton4
            // 
            this.toolBarButton4.ImageIndex = 14;
            this.toolBarButton4.Name = "toolBarButton4";
            this.toolBarButton4.Text = "退出";
            // 
            // imageList32
            // 
            this.imageList32.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList32.ImageStream")));
            this.imageList32.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList32.Images.SetKeyName(0, "");
            this.imageList32.Images.SetKeyName(1, "");
            this.imageList32.Images.SetKeyName(2, "");
            this.imageList32.Images.SetKeyName(3, "");
            this.imageList32.Images.SetKeyName(4, "");
            this.imageList32.Images.SetKeyName(5, "");
            this.imageList32.Images.SetKeyName(6, "");
            this.imageList32.Images.SetKeyName(7, "");
            this.imageList32.Images.SetKeyName(8, "");
            this.imageList32.Images.SetKeyName(9, "");
            this.imageList32.Images.SetKeyName(10, "");
            this.imageList32.Images.SetKeyName(11, "");
            this.imageList32.Images.SetKeyName(12, "");
            this.imageList32.Images.SetKeyName(13, "");
            this.imageList32.Images.SetKeyName(14, "");
            this.imageList32.Images.SetKeyName(15, "");
            this.imageList32.Images.SetKeyName(16, "");
            this.imageList32.Images.SetKeyName(17, "");
            this.imageList32.Images.SetKeyName(18, "");
            this.imageList32.Images.SetKeyName(19, "");
            this.imageList32.Images.SetKeyName(20, "");
            this.imageList32.Images.SetKeyName(21, "");
            this.imageList32.Images.SetKeyName(22, "");
            this.imageList32.Images.SetKeyName(23, "");
            this.imageList32.Images.SetKeyName(24, "");
            this.imageList32.Images.SetKeyName(25, "");
            this.imageList32.Images.SetKeyName(26, "");
            this.imageList32.Images.SetKeyName(27, "");
            this.imageList32.Images.SetKeyName(28, "");
            this.imageList32.Images.SetKeyName(29, "");
            this.imageList32.Images.SetKeyName(30, "");
            this.imageList32.Images.SetKeyName(31, "");
            this.imageList32.Images.SetKeyName(32, "");
            this.imageList32.Images.SetKeyName(33, "");
            this.imageList32.Images.SetKeyName(34, "");
            this.imageList32.Images.SetKeyName(35, "");
            this.imageList32.Images.SetKeyName(36, "");
            this.imageList32.Images.SetKeyName(37, "");
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.neuTabControl2);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 57);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(706, 182);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 3;
            // 
            // neuTabControl2
            // 
            this.neuTabControl2.Controls.Add(this.tabPage2);
            this.neuTabControl2.Controls.Add(this.tabPage3);
            this.neuTabControl2.Controls.Add(this.tabPage4);
            this.neuTabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuTabControl2.Location = new System.Drawing.Point(0, 0);
            this.neuTabControl2.Name = "neuTabControl2";
            this.neuTabControl2.SelectedIndex = 0;
            this.neuTabControl2.Size = new System.Drawing.Size(706, 182);
            this.neuTabControl2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTabControl2.TabIndex = 2;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ucCustomQuery1);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(698, 157);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "基本信息查询条件";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ucCustomQuery1
            // 
            this.ucCustomQuery1.ConditionWidth = 150;
            this.ucCustomQuery1.Direction = FS.HISFC.Components.Common.Controls.enuDirection.H;
            this.ucCustomQuery1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucCustomQuery1.Location = new System.Drawing.Point(3, 3);
            this.ucCustomQuery1.Name = "ucCustomQuery1";
            this.ucCustomQuery1.Size = new System.Drawing.Size(692, 151);
            this.ucCustomQuery1.TabIndex = 0;
            this.ucCustomQuery1.ValueWidth = 150;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.ucCustomQuery2);
            this.tabPage3.Location = new System.Drawing.Point(4, 21);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(698, 157);
            this.tabPage3.TabIndex = 1;
            this.tabPage3.Text = "诊断查询条件";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // ucCustomQuery2
            // 
            this.ucCustomQuery2.ConditionWidth = 150;
            this.ucCustomQuery2.Direction = FS.HISFC.Components.Common.Controls.enuDirection.H;
            this.ucCustomQuery2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucCustomQuery2.Location = new System.Drawing.Point(3, 3);
            this.ucCustomQuery2.Name = "ucCustomQuery2";
            this.ucCustomQuery2.Size = new System.Drawing.Size(692, 151);
            this.ucCustomQuery2.TabIndex = 0;
            this.ucCustomQuery2.ValueWidth = 150;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.ucCustomQuery3);
            this.tabPage4.Location = new System.Drawing.Point(4, 21);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(698, 157);
            this.tabPage4.TabIndex = 2;
            this.tabPage4.Text = "手术查询条件";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // ucCustomQuery3
            // 
            this.ucCustomQuery3.ConditionWidth = 150;
            this.ucCustomQuery3.Direction = FS.HISFC.Components.Common.Controls.enuDirection.H;
            this.ucCustomQuery3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucCustomQuery3.Location = new System.Drawing.Point(0, 0);
            this.ucCustomQuery3.Name = "ucCustomQuery3";
            this.ucCustomQuery3.Size = new System.Drawing.Size(698, 157);
            this.ucCustomQuery3.TabIndex = 0;
            this.ucCustomQuery3.ValueWidth = 150;
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.neuTabControl1);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel2.Location = new System.Drawing.Point(0, 239);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(706, 300);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 4;
            // 
            // neuTabControl1
            // 
            this.neuTabControl1.Controls.Add(this.tabPage1);
            this.neuTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuTabControl1.Location = new System.Drawing.Point(0, 0);
            this.neuTabControl1.Name = "neuTabControl1";
            this.neuTabControl1.SelectedIndex = 0;
            this.neuTabControl1.Size = new System.Drawing.Size(706, 300);
            this.neuTabControl1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.fpSpread1);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(698, 275);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "患者基本信息";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "3.0.2004.2005";
            this.fpSpread1.AccessibleDescription = "fpSpread1, Sheet1";
            this.fpSpread1.BackColor = System.Drawing.SystemColors.Control;
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.Location = new System.Drawing.Point(3, 3);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1});
            this.fpSpread1.Size = new System.Drawing.Size(692, 269);
            this.fpSpread1.TabIndex = 1;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread1.TextTipAppearance = tipAppearance1;
            this.fpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpSpread1_CellDoubleClick);
            this.fpSpread1.DoubleClick += new System.EventHandler(this.fpSpread1_DoubleClick);
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.ColumnCount = 9;
            this.fpSpread1_Sheet1.RowCount = 0;
            this.fpSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.SystemColors.Control, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, false);
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "出院科室";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "出院日期";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "姓名";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "性别";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "住院流水号";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "病案号";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "住院号";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "次数";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "出院诊断";
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            textCellType1.ReadOnly = true;
            this.fpSpread1_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.fpSpread1_Sheet1.Columns.Get(0).Label = "出院科室";
            this.fpSpread1_Sheet1.Columns.Get(0).Width = 100F;
            textCellType2.ReadOnly = true;
            this.fpSpread1_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.fpSpread1_Sheet1.Columns.Get(1).Label = "出院日期";
            textCellType3.ReadOnly = true;
            this.fpSpread1_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.fpSpread1_Sheet1.Columns.Get(2).Label = "姓名";
            this.fpSpread1_Sheet1.Columns.Get(2).Width = 86F;
            textCellType4.ReadOnly = true;
            this.fpSpread1_Sheet1.Columns.Get(3).CellType = textCellType4;
            this.fpSpread1_Sheet1.Columns.Get(3).Label = "性别";
            textCellType5.ReadOnly = true;
            this.fpSpread1_Sheet1.Columns.Get(4).CellType = textCellType5;
            this.fpSpread1_Sheet1.Columns.Get(4).Label = "住院流水号";
            this.fpSpread1_Sheet1.Columns.Get(4).Width = 99F;
            textCellType6.ReadOnly = true;
            this.fpSpread1_Sheet1.Columns.Get(5).CellType = textCellType6;
            this.fpSpread1_Sheet1.Columns.Get(5).Label = "病案号";
            this.fpSpread1_Sheet1.Columns.Get(5).Width = 0F;
            textCellType7.ReadOnly = true;
            this.fpSpread1_Sheet1.Columns.Get(6).CellType = textCellType7;
            this.fpSpread1_Sheet1.Columns.Get(6).Label = "住院号";
            this.fpSpread1_Sheet1.Columns.Get(6).Width = 113F;
            textCellType8.ReadOnly = true;
            this.fpSpread1_Sheet1.Columns.Get(7).CellType = textCellType8;
            this.fpSpread1_Sheet1.Columns.Get(7).Label = "次数";
            this.fpSpread1_Sheet1.Columns.Get(8).CellType = textCellType8;
            this.fpSpread1_Sheet1.Columns.Get(8).Label = "出院诊断";
            this.fpSpread1_Sheet1.Columns.Get(8).Width = 713F;

            this.fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.RowHeader.Visible = false;
            this.fpSpread1_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpSpread1_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.SheetCornerStyle.Locked = false;
            this.fpSpread1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpSpread1.SetActiveViewport(0, 1, 0);
            // 
            // frmSearchPatient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(706, 539);
            this.Controls.Add(this.neuPanel2);
            this.Controls.Add(this.neuPanel1);
            this.Controls.Add(this.neuToolBar1);
            this.Name = "frmSearchPatient";
            this.Text = "患者基本信息查询";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmSearchPatient_Load);
            this.neuPanel1.ResumeLayout(false);
            this.neuTabControl2.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.neuPanel2.ResumeLayout(false);
            this.neuTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuToolBar neuToolBar1;
        private System.Windows.Forms.ToolBarButton toolBarButton1;
        private System.Windows.Forms.ToolBarButton toolBarButton2;
        private System.Windows.Forms.ToolBarButton toolBarButton4;
        private System.Windows.Forms.ImageList imageList32;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.HISFC.Components.HealthRecord.Search.ucQuery ucCustomQuery1;
        private FS.FrameWork.WinForms.Controls.NeuTabControl neuTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private FarPoint.Win.Spread.FpSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuTabControl neuTabControl2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private FS.HISFC.Components.HealthRecord.Search.ucQuery ucCustomQuery2;
        private FS.HISFC.Components.HealthRecord.Search.ucQuery ucCustomQuery3;
    }
}