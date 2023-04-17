namespace FS.HISFC.Components.Operation
{
    partial class ucArrangmentQuery
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
            FarPoint.Win.Spread.CellType.DateTimeCellType dateTimeCellType1 = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucArrangmentQuery));
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.tvQueryList = new FS.FrameWork.WinForms.Controls.NeuTreeView();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dtEnd = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.cmbQuery = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dtBegin = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuPanel1.SuspendLayout();
            this.neuPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.neuPanel2.SuspendLayout();
            this.neuGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.neuPanel3);
            this.neuPanel1.Controls.Add(this.neuPanel2);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(953, 557);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // neuPanel3
            // 
            this.neuPanel3.Controls.Add(this.neuSpread1);
            this.neuPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel3.Location = new System.Drawing.Point(200, 0);
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size(753, 557);
            this.neuPanel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 1;
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1";
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
            this.neuSpread1.Size = new System.Drawing.Size(753, 557);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 0;
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
            this.neuSpread1_Sheet1.ColumnCount = 19;
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.AllowNoteEdit = false;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "病区";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "床号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "患者姓名";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "性别";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "年龄";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "手术诊断";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "手术名称";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "手术时间";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "手术医生";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "洗手护士";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "一助";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "二助";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "巡回护士";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "麻醉医生";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 14).Value = "麻醉助手";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 15).Value = "麻醉方式";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 16).Value = "房间号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 17).Value = "手术台";
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "病区";
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 70F;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "床号";
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 61F;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "患者姓名";
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 86F;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "性别";
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 47F;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "年龄";
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 50F;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "手术诊断";
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 171F;
            this.neuSpread1_Sheet1.Columns.Get(6).Label = "手术名称";
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 113F;
            this.neuSpread1_Sheet1.Columns.Get(7).Label = "手术时间";
            this.neuSpread1_Sheet1.Columns.Get(7).Width = 104F;
            this.neuSpread1_Sheet1.Columns.Get(8).Label = "手术医生";
            this.neuSpread1_Sheet1.Columns.Get(8).Width = 76F;
            this.neuSpread1_Sheet1.Columns.Get(9).Label = "洗手护士";
            this.neuSpread1_Sheet1.Columns.Get(9).Width = 0F;
            this.neuSpread1_Sheet1.Columns.Get(12).Label = "巡回护士";
            this.neuSpread1_Sheet1.Columns.Get(12).Width = 0F;
            this.neuSpread1_Sheet1.Columns.Get(13).Label = "麻醉医生";
            this.neuSpread1_Sheet1.Columns.Get(13).Width = 61F;
            this.neuSpread1_Sheet1.Columns.Get(14).Label = "麻醉助手";
            this.neuSpread1_Sheet1.Columns.Get(14).Width = 55F;
            this.neuSpread1_Sheet1.Columns.Get(15).Label = "麻醉方式";
            this.neuSpread1_Sheet1.Columns.Get(15).Width = 97F;
            this.neuSpread1_Sheet1.Columns.Get(16).Label = "房间号";
            this.neuSpread1_Sheet1.Columns.Get(16).Width = 144F;
            dateTimeCellType1.Calendar = ((System.Globalization.Calendar)(resources.GetObject("dateTimeCellType1.Calendar")));
            dateTimeCellType1.CalendarDayFont = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dateTimeCellType1.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText;
            dateTimeCellType1.DateDefault = new System.DateTime(2011, 3, 29, 18, 14, 53, 0);
            dateTimeCellType1.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.UserDefined;
            dateTimeCellType1.TimeDefault = new System.DateTime(2011, 3, 29, 18, 14, 53, 0);
            this.neuSpread1_Sheet1.Columns.Get(17).CellType = dateTimeCellType1;
            this.neuSpread1_Sheet1.Columns.Get(17).Label = "手术台";
            this.neuSpread1_Sheet1.Columns.Get(17).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(17).Width = 170F;
            this.neuSpread1_Sheet1.Columns.Get(18).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(18).Width = 52F;
            this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 2F;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuSpread1.SetViewportLeftColumn(0, 0, 3);
            this.neuSpread1.SetActiveViewport(0, 1, 0);
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.tvQueryList);
            this.neuPanel2.Controls.Add(this.neuGroupBox1);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.neuPanel2.Location = new System.Drawing.Point(0, 0);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(200, 557);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 0;
            // 
            // tvQueryList
            // 
            this.tvQueryList.HideSelection = false;
            this.tvQueryList.Location = new System.Drawing.Point(3, 100);
            this.tvQueryList.Name = "tvQueryList";
            this.tvQueryList.Size = new System.Drawing.Size(194, 454);
            this.tvQueryList.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tvQueryList.TabIndex = 1;
            this.tvQueryList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvQueryList_AfterSelect);
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.neuLabel3);
            this.neuGroupBox1.Controls.Add(this.dtEnd);
            this.neuGroupBox1.Controls.Add(this.cmbQuery);
            this.neuGroupBox1.Controls.Add(this.neuLabel2);
            this.neuGroupBox1.Controls.Add(this.neuLabel1);
            this.neuGroupBox1.Controls.Add(this.dtBegin);
            this.neuGroupBox1.Location = new System.Drawing.Point(3, 3);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(194, 91);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 0;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "查询条件";
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(6, 44);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(65, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 5;
            this.neuLabel3.Text = "结束日期：";
            // 
            // dtEnd
            // 
            this.dtEnd.IsEnter2Tab = false;
            this.dtEnd.Location = new System.Drawing.Point(77, 40);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(111, 21);
            this.dtEnd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtEnd.TabIndex = 4;
            // 
            // cmbQuery
            // 
            this.cmbQuery.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbQuery.FormattingEnabled = true;
            this.cmbQuery.IsEnter2Tab = false;
            this.cmbQuery.IsFlat = false;
            this.cmbQuery.IsLike = true;
            this.cmbQuery.IsListOnly = false;
            this.cmbQuery.IsPopForm = true;
            this.cmbQuery.IsShowCustomerList = false;
            this.cmbQuery.IsShowID = false;
            this.cmbQuery.Location = new System.Drawing.Point(77, 66);
            this.cmbQuery.Name = "cmbQuery";
            this.cmbQuery.PopForm = null;
            this.cmbQuery.ShowCustomerList = false;
            this.cmbQuery.ShowID = false;
            this.cmbQuery.Size = new System.Drawing.Size(111, 20);
            this.cmbQuery.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbQuery.TabIndex = 3;
            this.cmbQuery.Tag = "";
            this.cmbQuery.ToolBarUse = false;
            this.cmbQuery.SelectedIndexChanged += new System.EventHandler(this.cmbQuery_SelectedIndexChanged);
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(6, 69);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(65, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 2;
            this.neuLabel2.Text = "检索条件：";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(6, 17);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(65, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 1;
            this.neuLabel1.Text = "开始日期：";
            // 
            // dtBegin
            // 
            this.dtBegin.IsEnter2Tab = false;
            this.dtBegin.Location = new System.Drawing.Point(77, 13);
            this.dtBegin.Name = "dtBegin";
            this.dtBegin.Size = new System.Drawing.Size(111, 21);
            this.dtBegin.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtBegin.TabIndex = 0;
            // 
            // ucArrangmentQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucArrangmentQuery";
            this.Size = new System.Drawing.Size(953, 557);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.neuPanel2.ResumeLayout(false);
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel3;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuTreeView tvQueryList;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtBegin;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbQuery;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtEnd;
    }
}
