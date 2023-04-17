namespace FS.SOC.Local.GYZL.PubReport.Components
{
    partial class ucPublicBill
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
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType2 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType12 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType13 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType14 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType15 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType16 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType17 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType18 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType19 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType20 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType21 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType22 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.tabControl1 = new Crownwood.Magic.Controls.TabControl();
            this.tabPatient = new Crownwood.Magic.Controls.TabPage();
            this.fpSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.tabBill = new Crownwood.Magic.Controls.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtZLF = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtGJF = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbQuery = new System.Windows.Forms.Button();
            this.ucQueryInpatientNo1 = new FS.HISFC.Components.Common.Controls.ucQueryInpatientNo();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpBegin = new System.Windows.Forms.DateTimePicker();
            this.tabControl1.SuspendLayout();
            this.tabPatient.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            this.tabBill.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.IDEPixelArea = true;
            this.tabControl1.Location = new System.Drawing.Point(0, 48);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.PositionTop = true;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.SelectedTab = this.tabPatient;
            this.tabControl1.Size = new System.Drawing.Size(952, 544);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.TabPages.AddRange(new Crownwood.Magic.Controls.TabPage[] {
            this.tabPatient,
            this.tabBill});
            this.tabControl1.SelectionChanged += new System.EventHandler(this.tabControl1_SelectionChanged);
            // 
            // tabPatient
            // 
            this.tabPatient.Controls.Add(this.fpSpread1);
            this.tabPatient.Location = new System.Drawing.Point(0, 25);
            this.tabPatient.Name = "tabPatient";
            this.tabPatient.Size = new System.Drawing.Size(952, 519);
            this.tabPatient.TabIndex = 3;
            this.tabPatient.Title = "患者信息";
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "3.0.2004.2005";
            this.fpSpread1.AccessibleDescription = "";
            this.fpSpread1.BackColor = System.Drawing.Color.White;
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread1.FileName = "";
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.IsAutoSaveGridStatus = false;
            this.fpSpread1.IsCanCustomConfigColumn = false;
            this.fpSpread1.Location = new System.Drawing.Point(0, 0);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1});
            this.fpSpread1.Size = new System.Drawing.Size(952, 519);
            this.fpSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpSpread1.TabIndex = 0;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread1.TextTipAppearance = tipAppearance2;
            this.fpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpSpread1_CellDoubleClick);
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.ColumnCount = 13;
            this.fpSpread1_Sheet1.RowCount = 15;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "选择";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "住院号";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "姓名";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "科室";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "公费卡号";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "合同单位";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "在院状态";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "入院登记时间";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "结算时间";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "统计开始时间";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "统计结束时间";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "已统计";
            this.fpSpread1_Sheet1.ColumnHeader.Rows.Get(0).Height = 33F;
            this.fpSpread1_Sheet1.Columns.Get(0).CellType = checkBoxCellType2;
            this.fpSpread1_Sheet1.Columns.Get(0).Label = "选择";
            this.fpSpread1_Sheet1.Columns.Get(0).ShowSortIndicator = false;
            this.fpSpread1_Sheet1.Columns.Get(0).Width = 57F;
            textCellType12.ReadOnly = true;
            this.fpSpread1_Sheet1.Columns.Get(1).CellType = textCellType12;
            this.fpSpread1_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.fpSpread1_Sheet1.Columns.Get(1).Label = "住院号";
            this.fpSpread1_Sheet1.Columns.Get(1).ShowSortIndicator = false;
            this.fpSpread1_Sheet1.Columns.Get(1).Width = 73F;
            textCellType13.ReadOnly = true;
            this.fpSpread1_Sheet1.Columns.Get(2).CellType = textCellType13;
            this.fpSpread1_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.fpSpread1_Sheet1.Columns.Get(2).Label = "姓名";
            this.fpSpread1_Sheet1.Columns.Get(2).ShowSortIndicator = false;
            textCellType14.ReadOnly = true;
            this.fpSpread1_Sheet1.Columns.Get(3).CellType = textCellType14;
            this.fpSpread1_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.fpSpread1_Sheet1.Columns.Get(3).Label = "科室";
            this.fpSpread1_Sheet1.Columns.Get(3).ShowSortIndicator = false;
            this.fpSpread1_Sheet1.Columns.Get(3).SortIndicator = FarPoint.Win.Spread.Model.SortIndicator.Ascending;
            this.fpSpread1_Sheet1.Columns.Get(3).Width = 77F;
            textCellType15.ReadOnly = true;
            this.fpSpread1_Sheet1.Columns.Get(4).CellType = textCellType15;
            this.fpSpread1_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.fpSpread1_Sheet1.Columns.Get(4).Label = "公费卡号";
            this.fpSpread1_Sheet1.Columns.Get(4).SortIndicator = FarPoint.Win.Spread.Model.SortIndicator.Ascending;
            this.fpSpread1_Sheet1.Columns.Get(4).Width = 82F;
            textCellType16.ReadOnly = true;
            this.fpSpread1_Sheet1.Columns.Get(5).CellType = textCellType16;
            this.fpSpread1_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.fpSpread1_Sheet1.Columns.Get(5).Label = "合同单位";
            this.fpSpread1_Sheet1.Columns.Get(5).Width = 80F;
            textCellType17.ReadOnly = true;
            this.fpSpread1_Sheet1.Columns.Get(6).CellType = textCellType17;
            this.fpSpread1_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.fpSpread1_Sheet1.Columns.Get(6).Label = "在院状态";
            this.fpSpread1_Sheet1.Columns.Get(6).Visible = false;
            this.fpSpread1_Sheet1.Columns.Get(6).Width = 82F;
            textCellType18.ReadOnly = true;
            this.fpSpread1_Sheet1.Columns.Get(7).CellType = textCellType18;
            this.fpSpread1_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.fpSpread1_Sheet1.Columns.Get(7).Label = "入院登记时间";
            this.fpSpread1_Sheet1.Columns.Get(7).Width = 120F;
            textCellType19.ReadOnly = true;
            this.fpSpread1_Sheet1.Columns.Get(8).CellType = textCellType19;
            this.fpSpread1_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.fpSpread1_Sheet1.Columns.Get(8).Label = "结算时间";
            this.fpSpread1_Sheet1.Columns.Get(8).Visible = false;
            this.fpSpread1_Sheet1.Columns.Get(8).Width = 120F;
            textCellType20.ReadOnly = true;
            this.fpSpread1_Sheet1.Columns.Get(9).CellType = textCellType20;
            this.fpSpread1_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.fpSpread1_Sheet1.Columns.Get(9).Label = "统计开始时间";
            this.fpSpread1_Sheet1.Columns.Get(9).Width = 120F;
            textCellType21.ReadOnly = true;
            this.fpSpread1_Sheet1.Columns.Get(10).CellType = textCellType21;
            this.fpSpread1_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.fpSpread1_Sheet1.Columns.Get(10).Label = "统计结束时间";
            this.fpSpread1_Sheet1.Columns.Get(10).Width = 120F;
            textCellType22.ReadOnly = true;
            this.fpSpread1_Sheet1.Columns.Get(11).CellType = textCellType22;
            this.fpSpread1_Sheet1.Columns.Get(11).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.fpSpread1_Sheet1.Columns.Get(11).Label = "已统计";
            this.fpSpread1_Sheet1.Columns.Get(11).Width = 68F;
            this.fpSpread1_Sheet1.Columns.Get(12).Width = 0F;
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // tabBill
            // 
            this.tabBill.Controls.Add(this.panel1);
            this.tabBill.Location = new System.Drawing.Point(0, 25);
            this.tabBill.Name = "tabBill";
            this.tabBill.Selected = false;
            this.tabBill.Size = new System.Drawing.Size(952, 519);
            this.tabBill.TabIndex = 4;
            this.tabBill.Title = "托收单信息";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(952, 519);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtZLF);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtGJF);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbQuery);
            this.groupBox1.Controls.Add(this.ucQueryInpatientNo1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpEnd);
            this.groupBox1.Controls.Add(this.dtpBegin);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(952, 48);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // txtZLF
            // 
            this.txtZLF.Location = new System.Drawing.Point(904, 16);
            this.txtZLF.Name = "txtZLF";
            this.txtZLF.Size = new System.Drawing.Size(56, 21);
            this.txtZLF.TabIndex = 9;
            this.txtZLF.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(824, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "审批肿瘤药费:";
            this.label4.Visible = false;
            // 
            // txtGJF
            // 
            this.txtGJF.Location = new System.Drawing.Point(760, 16);
            this.txtGJF.Name = "txtGJF";
            this.txtGJF.Size = new System.Drawing.Size(56, 21);
            this.txtGJF.TabIndex = 7;
            this.txtGJF.WordWrap = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(712, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "高检费:";
            this.label3.Visible = false;
            // 
            // tbQuery
            // 
            this.tbQuery.Location = new System.Drawing.Point(624, 15);
            this.tbQuery.Name = "tbQuery";
            this.tbQuery.Size = new System.Drawing.Size(75, 23);
            this.tbQuery.TabIndex = 5;
            this.tbQuery.Text = "查询(&Q)";
            this.tbQuery.Click += new System.EventHandler(this.tbQuery_Click);
            // 
            // ucQueryInpatientNo1
            // 
            this.ucQueryInpatientNo1.DefaultInputType = 0;
            this.ucQueryInpatientNo1.InputType = 0;
            this.ucQueryInpatientNo1.IsDeptOnly = true;
            this.ucQueryInpatientNo1.Location = new System.Drawing.Point(432, 11);
            this.ucQueryInpatientNo1.Name = "ucQueryInpatientNo1";
            this.ucQueryInpatientNo1.PatientInState = "ALL";
            this.ucQueryInpatientNo1.ShowState = FS.HISFC.Components.Common.Controls.enuShowState.All;
            this.ucQueryInpatientNo1.Size = new System.Drawing.Size(190, 32);
            this.ucQueryInpatientNo1.TabIndex = 4;
            this.ucQueryInpatientNo1.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(this.ucQueryInpatientNo1_myEvent);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(240, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "至:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "统计时间从:";
            // 
            // dtpEnd
            // 
            this.dtpEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd.Location = new System.Drawing.Point(264, 16);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(152, 21);
            this.dtpEnd.TabIndex = 1;
            // 
            // dtpBegin
            // 
            this.dtpBegin.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBegin.Location = new System.Drawing.Point(80, 16);
            this.dtpBegin.Name = "dtpBegin";
            this.dtpBegin.Size = new System.Drawing.Size(152, 21);
            this.dtpBegin.TabIndex = 0;
            // 
            // ucPublicBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Name = "ucPublicBill";
            this.Size = new System.Drawing.Size(952, 592);
            this.tabControl1.ResumeLayout(false);
            this.tabPatient.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            this.tabBill.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Crownwood.Magic.Controls.TabControl tabControl1;
        private Crownwood.Magic.Controls.TabPage tabPatient;
        private Crownwood.Magic.Controls.TabPage tabBill;
        //private FarPoint.Win.Spread.FpSpread fpSpread1;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpBegin;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        public FS.HISFC.Components.Common.Controls.ucQueryInpatientNo ucQueryInpatientNo1;
        private System.Windows.Forms.Button tbQuery;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtGJF;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtZLF;
    }
}
