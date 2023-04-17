namespace FS.HISFC.Components.Speciment.Report
{
    partial class ucCheckLoc
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
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            this.cmbDiseaseType = new System.Windows.Forms.ComboBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbSpecType = new System.Windows.Forms.ComboBox();
            this.neuLabel7 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.rbtAll = new System.Windows.Forms.RadioButton();
            this.rbtLocLast = new System.Windows.Forms.RadioButton();
            this.dtpEndTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpStartDate = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.tabResult = new System.Windows.Forms.TabControl();
            this.tpBox = new System.Windows.Forms.TabPage();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.tpSpec = new System.Windows.Forms.TabPage();
            this.neuSpread2 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread2_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabResult.SuspendLayout();
            this.tpBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.tpSpec.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread2_Sheet1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbDiseaseType
            // 
            this.cmbDiseaseType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDiseaseType.FormattingEnabled = true;
            this.cmbDiseaseType.Location = new System.Drawing.Point(60, 7);
            this.cmbDiseaseType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDiseaseType.Name = "cmbDiseaseType";
            this.cmbDiseaseType.Size = new System.Drawing.Size(83, 24);
            this.cmbDiseaseType.TabIndex = 97;
            this.cmbDiseaseType.SelectedIndexChanged += new System.EventHandler(this.cmbDiseaseType_SelectedIndexChanged);
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(4, 11);
            this.neuLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(48, 16);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 96;
            this.neuLabel1.Text = "病种:";
            // 
            // cmbSpecType
            // 
            this.cmbSpecType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpecType.FormattingEnabled = true;
            this.cmbSpecType.Location = new System.Drawing.Point(238, 7);
            this.cmbSpecType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSpecType.Name = "cmbSpecType";
            this.cmbSpecType.Size = new System.Drawing.Size(82, 24);
            this.cmbSpecType.TabIndex = 95;
            this.cmbSpecType.SelectedIndexChanged += new System.EventHandler(this.cmbSpecType_SelectedIndexChanged);
            // 
            // neuLabel7
            // 
            this.neuLabel7.AutoSize = true;
            this.neuLabel7.Location = new System.Drawing.Point(151, 11);
            this.neuLabel7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel7.Name = "neuLabel7";
            this.neuLabel7.Size = new System.Drawing.Size(80, 16);
            this.neuLabel7.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel7.TabIndex = 94;
            this.neuLabel7.Text = "标本类型:";
            // 
            // rbtAll
            // 
            this.rbtAll.AutoSize = true;
            this.rbtAll.Location = new System.Drawing.Point(908, 9);
            this.rbtAll.Name = "rbtAll";
            this.rbtAll.Size = new System.Drawing.Size(58, 20);
            this.rbtAll.TabIndex = 92;
            this.rbtAll.Text = "全部";
            this.rbtAll.UseVisualStyleBackColor = true;
            this.rbtAll.CheckedChanged += new System.EventHandler(this.rbtLocLast_CheckedChanged);
            // 
            // rbtLocLast
            // 
            this.rbtLocLast.AutoSize = true;
            this.rbtLocLast.Checked = true;
            this.rbtLocLast.Location = new System.Drawing.Point(716, 9);
            this.rbtLocLast.Name = "rbtLocLast";
            this.rbtLocLast.Size = new System.Drawing.Size(186, 20);
            this.rbtLocLast.TabIndex = 91;
            this.rbtLocLast.TabStop = true;
            this.rbtLocLast.Text = "所有病种最后一个标本";
            this.rbtLocLast.UseVisualStyleBackColor = true;
            this.rbtLocLast.CheckedChanged += new System.EventHandler(this.rbtLocLast_CheckedChanged);
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.Location = new System.Drawing.Point(573, 6);
            this.dtpEndTime.Margin = new System.Windows.Forms.Padding(5);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.Size = new System.Drawing.Size(135, 26);
            this.dtpEndTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpEndTime.TabIndex = 87;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(329, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 86;
            this.label1.Text = "统计时间:";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(409, 6);
            this.dtpStartDate.Margin = new System.Windows.Forms.Padding(5);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(143, 26);
            this.dtpStartDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpStartDate.TabIndex = 85;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(553, 11);
            this.label11.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(16, 16);
            this.label11.TabIndex = 84;
            this.label11.Text = "-";
            // 
            // tabResult
            // 
            this.tabResult.Controls.Add(this.tpBox);
            this.tabResult.Controls.Add(this.tpSpec);
            this.tabResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabResult.Location = new System.Drawing.Point(0, 49);
            this.tabResult.Name = "tabResult";
            this.tabResult.SelectedIndex = 0;
            this.tabResult.Size = new System.Drawing.Size(935, 746);
            this.tabResult.TabIndex = 2;
            // 
            // tpBox
            // 
            this.tpBox.Controls.Add(this.neuSpread1);
            this.tpBox.Location = new System.Drawing.Point(4, 25);
            this.tpBox.Name = "tpBox";
            this.tpBox.Padding = new System.Windows.Forms.Padding(3);
            this.tpBox.Size = new System.Drawing.Size(927, 717);
            this.tpBox.TabIndex = 0;
            this.tpBox.Text = "标本盒为中心显示";
            this.tpBox.UseVisualStyleBackColor = true;
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "2.5.2007.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(3, 3);
            this.neuSpread1.Margin = new System.Windows.Forms.Padding(4);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(921, 711);
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
            this.neuSpread1_Sheet1.ColumnCount = 0;
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.ActiveSkin = FarPoint.Win.Spread.DefaultSkins.Classic2;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(105)))), ((int)(((byte)(107)))));
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 40F;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(105)))), ((int)(((byte)(107)))));
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(105)))), ((int)(((byte)(107)))));
            this.neuSpread1_Sheet1.SheetCornerStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.SheetCornerStyle.Locked = false;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuSpread1.SetActiveViewport(1, 1);
            // 
            // tpSpec
            // 
            this.tpSpec.Controls.Add(this.neuSpread2);
            this.tpSpec.Location = new System.Drawing.Point(4, 25);
            this.tpSpec.Name = "tpSpec";
            this.tpSpec.Padding = new System.Windows.Forms.Padding(3);
            this.tpSpec.Size = new System.Drawing.Size(927, 717);
            this.tpSpec.TabIndex = 1;
            this.tpSpec.Text = "标本为中心显示";
            this.tpSpec.UseVisualStyleBackColor = true;
            // 
            // neuSpread2
            // 
            this.neuSpread2.About = "2.5.2007.2005";
            this.neuSpread2.AccessibleDescription = "neuSpread2";
            this.neuSpread2.BackColor = System.Drawing.Color.White;
            this.neuSpread2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread2.FileName = "";
            this.neuSpread2.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread2.IsAutoSaveGridStatus = false;
            this.neuSpread2.IsCanCustomConfigColumn = false;
            this.neuSpread2.Location = new System.Drawing.Point(3, 3);
            this.neuSpread2.Name = "neuSpread2";
            this.neuSpread2.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread2_Sheet1});
            this.neuSpread2.Size = new System.Drawing.Size(921, 711);
            this.neuSpread2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread2.TabIndex = 0;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread2.TextTipAppearance = tipAppearance2;
            this.neuSpread2.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // neuSpread2_Sheet1
            // 
            this.neuSpread2_Sheet1.Reset();
            this.neuSpread2_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread2_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread2_Sheet1.ColumnCount = 0;
            this.neuSpread2_Sheet1.RowCount = 0;
            this.neuSpread2_Sheet1.ActiveSkin = FarPoint.Win.Spread.DefaultSkins.Classic2;
            this.neuSpread2_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(105)))), ((int)(((byte)(107)))));
            this.neuSpread2_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread2_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread2_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.neuSpread2_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread2_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread2_Sheet1.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread2_Sheet1.DefaultStyle.Locked = false;
            this.neuSpread2_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.neuSpread2_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(105)))), ((int)(((byte)(107)))));
            this.neuSpread2_Sheet1.RowHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread2_Sheet1.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread2_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.neuSpread2_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread2_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(105)))), ((int)(((byte)(107)))));
            this.neuSpread2_Sheet1.SheetCornerStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread2_Sheet1.SheetCornerStyle.Locked = false;
            this.neuSpread2_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuSpread2_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuSpread2.SetActiveViewport(1, 1);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmbDiseaseType);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.neuLabel1);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.cmbSpecType);
            this.panel1.Controls.Add(this.dtpStartDate);
            this.panel1.Controls.Add(this.neuLabel7);
            this.panel1.Controls.Add(this.dtpEndTime);
            this.panel1.Controls.Add(this.rbtAll);
            this.panel1.Controls.Add(this.rbtLocLast);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(935, 49);
            this.panel1.TabIndex = 2;
            // 
            // ucCheckLoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabResult);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ucCheckLoc";
            this.Size = new System.Drawing.Size(935, 795);
            this.Load += new System.EventHandler(this.ucCheckLoc_Load);
            this.tabResult.ResumeLayout(false);
            this.tpBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.tpSpec.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread2_Sheet1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpEndTime;
        private System.Windows.Forms.Label label1;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.RadioButton rbtAll;
        private System.Windows.Forms.RadioButton rbtLocLast;
        private System.Windows.Forms.ComboBox cmbDiseaseType;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private System.Windows.Forms.ComboBox cmbSpecType;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel7;
        private System.Windows.Forms.TabControl tabResult;
        private System.Windows.Forms.TabPage tpBox;
        private System.Windows.Forms.TabPage tpSpec;
        private System.Windows.Forms.Panel panel1;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread2;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FarPoint.Win.Spread.SheetView neuSpread2_Sheet1;
    }
}
