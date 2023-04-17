namespace FS.HISFC.Components.Order.Controls
{
    partial class ucLevelOrder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucLevelOrder));
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.neuContextMenuStrip1 = new FS.FrameWork.WinForms.Controls.NeuContextMenuStrip();
            this.miUnlock = new System.Windows.Forms.ToolStripMenuItem();
            this.miRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tvItemLevel1 = new FS.HISFC.Components.Common.Controls.tvItemLevel(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbItemClass = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.panel4 = new System.Windows.Forms.Panel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.panel6 = new System.Windows.Forms.Panel();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbLabSample = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.cmbExecDept = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbCheckPart = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.neuContextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.panel6.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuContextMenuStrip1
            // 
            this.neuContextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miUnlock,
            this.miRefresh});
            this.neuContextMenuStrip1.Name = "neuContextMenuStrip1";
            this.neuContextMenuStrip1.Size = new System.Drawing.Size(95, 48);
            this.neuContextMenuStrip1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            // 
            // miUnlock
            // 
            this.miUnlock.Name = "miUnlock";
            this.miUnlock.Size = new System.Drawing.Size(94, 22);
            this.miUnlock.Text = "解锁";
            // 
            // miRefresh
            // 
            this.miRefresh.Name = "miRefresh";
            this.miRefresh.Size = new System.Drawing.Size(94, 22);
            this.miRefresh.Text = "刷新";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(272, 592);
            this.panel1.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tvItemLevel1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 37);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(272, 555);
            this.panel3.TabIndex = 1;
            // 
            // tvItemLevel1
            // 
            this.tvItemLevel1.AllowDrop = true;
            this.tvItemLevel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvItemLevel1.HideSelection = false;
            this.tvItemLevel1.ImageIndex = 0;
            this.tvItemLevel1.InOutType = 0;
            this.tvItemLevel1.IsEdit = false;
            this.tvItemLevel1.LevelClass = ((FS.FrameWork.Models.NeuObject)(resources.GetObject("tvItemLevel1.LevelClass")));
            this.tvItemLevel1.Location = new System.Drawing.Point(0, 0);
            this.tvItemLevel1.Name = "tvItemLevel1";
            this.tvItemLevel1.SelectedImageIndex = 0;
            this.tvItemLevel1.Size = new System.Drawing.Size(272, 555);
            this.tvItemLevel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tvItemLevel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.cmbItemClass);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(272, 37);
            this.panel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "分类";
            // 
            // cmbItemClass
            // 
            this.cmbItemClass.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbItemClass.FormattingEnabled = true;
            this.cmbItemClass.IsEnter2Tab = false;
            this.cmbItemClass.IsFlat = false;
            this.cmbItemClass.IsLike = true;
            this.cmbItemClass.IsListOnly = false;
            this.cmbItemClass.IsPopForm = true;
            this.cmbItemClass.IsShowCustomerList = false;
            this.cmbItemClass.IsShowID = false;
            this.cmbItemClass.Location = new System.Drawing.Point(60, 9);
            this.cmbItemClass.Name = "cmbItemClass";
            this.cmbItemClass.PopForm = null;
            this.cmbItemClass.ShowCustomerList = false;
            this.cmbItemClass.ShowID = false;
            this.cmbItemClass.Size = new System.Drawing.Size(121, 20);
            this.cmbItemClass.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbItemClass.TabIndex = 2;
            this.cmbItemClass.Tag = "";
            this.cmbItemClass.ToolBarUse = false;
            this.cmbItemClass.SelectedValueChanged += new System.EventHandler(this.cmbItemClass_SelectedValueChanged);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.neuSpread1);
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(690, 592);
            this.panel4.TabIndex = 2;
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
            this.neuSpread1.Location = new System.Drawing.Point(0, 75);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(690, 517);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 5;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.neuSpread1_ButtonClicked);
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ColumnCount = 9;
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "选择";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "项目编码";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "项目名称";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "规格";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "价格";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "单位";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "序号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "拼音码";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "五笔码";
            this.neuSpread1_Sheet1.Columns.Get(0).CellType = checkBoxCellType1;
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "选择";
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 31F;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "项目编码";
            this.neuSpread1_Sheet1.Columns.Get(1).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 66F;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "项目名称";
            this.neuSpread1_Sheet1.Columns.Get(2).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 191F;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "规格";
            this.neuSpread1_Sheet1.Columns.Get(3).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 89F;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "价格";
            this.neuSpread1_Sheet1.Columns.Get(4).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 52F;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "单位";
            this.neuSpread1_Sheet1.Columns.Get(5).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 36F;
            this.neuSpread1_Sheet1.Columns.Get(6).Label = "序号";
            this.neuSpread1_Sheet1.Columns.Get(6).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 37F;
            this.neuSpread1_Sheet1.Columns.Get(7).Label = "拼音码";
            this.neuSpread1_Sheet1.Columns.Get(7).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(8).Label = "五笔码";
            this.neuSpread1_Sheet1.Columns.Get(8).Locked = true;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.neuSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 22F;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuSpread1.SetActiveViewport(0, 1, 0);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.txtMemo);
            this.panel6.Controls.Add(this.label7);
            this.panel6.Controls.Add(this.cmbCheckPart);
            this.panel6.Controls.Add(this.label6);
            this.panel6.Controls.Add(this.txtFilter);
            this.panel6.Controls.Add(this.label5);
            this.panel6.Controls.Add(this.btnCancel);
            this.panel6.Controls.Add(this.btnOK);
            this.panel6.Controls.Add(this.txtQty);
            this.panel6.Controls.Add(this.label4);
            this.panel6.Controls.Add(this.cmbLabSample);
            this.panel6.Controls.Add(this.label3);
            this.panel6.Controls.Add(this.cmbExecDept);
            this.panel6.Controls.Add(this.label2);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(690, 75);
            this.panel6.TabIndex = 4;
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(48, 44);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(245, 21);
            this.txtFilter.TabIndex = 11;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "过滤";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(569, 42);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(92, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(569, 7);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(92, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(251, 10);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(42, 21);
            this.txtQty.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(216, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "数量";
            // 
            // cmbLabSample
            // 
            this.cmbLabSample.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbLabSample.FormattingEnabled = true;
            this.cmbLabSample.IsEnter2Tab = false;
            this.cmbLabSample.IsFlat = false;
            this.cmbLabSample.IsLike = true;
            this.cmbLabSample.IsListOnly = false;
            this.cmbLabSample.IsPopForm = true;
            this.cmbLabSample.IsShowCustomerList = false;
            this.cmbLabSample.IsShowID = false;
            this.cmbLabSample.Location = new System.Drawing.Point(357, 11);
            this.cmbLabSample.Name = "cmbLabSample";
            this.cmbLabSample.PopForm = null;
            this.cmbLabSample.ShowCustomerList = false;
            this.cmbLabSample.ShowID = false;
            this.cmbLabSample.Size = new System.Drawing.Size(66, 20);
            this.cmbLabSample.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbLabSample.TabIndex = 5;
            this.cmbLabSample.Tag = "";
            this.cmbLabSample.ToolBarUse = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(322, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "标本";
            // 
            // cmbExecDept
            // 
            this.cmbExecDept.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbExecDept.FormattingEnabled = true;
            this.cmbExecDept.IsEnter2Tab = false;
            this.cmbExecDept.IsFlat = false;
            this.cmbExecDept.IsLike = true;
            this.cmbExecDept.IsListOnly = false;
            this.cmbExecDept.IsPopForm = true;
            this.cmbExecDept.IsShowCustomerList = false;
            this.cmbExecDept.IsShowID = false;
            this.cmbExecDept.Location = new System.Drawing.Point(72, 9);
            this.cmbExecDept.Name = "cmbExecDept";
            this.cmbExecDept.PopForm = null;
            this.cmbExecDept.ShowCustomerList = false;
            this.cmbExecDept.ShowID = false;
            this.cmbExecDept.Size = new System.Drawing.Size(121, 20);
            this.cmbExecDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbExecDept.TabIndex = 3;
            this.cmbExecDept.Tag = "";
            this.cmbExecDept.ToolBarUse = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "执行科室";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel4);
            this.splitContainer1.Size = new System.Drawing.Size(966, 592);
            this.splitContainer1.SplitterDistance = 272;
            this.splitContainer1.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(442, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "部位";
            // 
            // cmbCheckPart
            // 
            this.cmbCheckPart.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbCheckPart.FormattingEnabled = true;
            this.cmbCheckPart.IsEnter2Tab = false;
            this.cmbCheckPart.IsFlat = false;
            this.cmbCheckPart.IsLike = true;
            this.cmbCheckPart.IsListOnly = false;
            this.cmbCheckPart.IsPopForm = true;
            this.cmbCheckPart.IsShowCustomerList = false;
            this.cmbCheckPart.IsShowID = false;
            this.cmbCheckPart.Location = new System.Drawing.Point(477, 11);
            this.cmbCheckPart.Name = "cmbCheckPart";
            this.cmbCheckPart.PopForm = null;
            this.cmbCheckPart.ShowCustomerList = false;
            this.cmbCheckPart.ShowID = false;
            this.cmbCheckPart.Size = new System.Drawing.Size(66, 20);
            this.cmbCheckPart.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbCheckPart.TabIndex = 13;
            this.cmbCheckPart.Tag = "";
            this.cmbCheckPart.ToolBarUse = false;
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(357, 44);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(186, 21);
            this.txtMemo.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(322, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "备注";
            // 
            // ucLevelOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ucLevelOrder";
            this.Size = new System.Drawing.Size(966, 592);
            this.neuContextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuContextMenuStrip neuContextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miUnlock;
        private System.Windows.Forms.ToolStripMenuItem miRefresh;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private FS.HISFC.Components.Common.Controls.tvItemLevel tvItemLevel1;
        private System.Windows.Forms.Panel panel2;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbItemClass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel6;
        public FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        public FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Label label4;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbLabSample;
        private System.Windows.Forms.Label label3;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbExecDept;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label label5;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbCheckPart;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtMemo;
        private System.Windows.Forms.Label label7;
    }
}
