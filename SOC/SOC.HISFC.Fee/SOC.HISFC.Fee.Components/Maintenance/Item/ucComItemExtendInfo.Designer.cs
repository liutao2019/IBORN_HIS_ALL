namespace FS.SOC.HISFC.Fee.Components.Maintenance.Item
{
    partial class ucComItemExtendInfo
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
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.btnAddItem = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.rbtnAll = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.rbtnPharmacy = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.rbtnItem = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.ntxtFilter = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuGroupBox2 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuLabel9 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ngbQuerySet = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSpread = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.sheetView1 = new FarPoint.Win.Spread.SheetView();
            this.neuGroupBox1.SuspendLayout();
            this.neuGroupBox2.SuspendLayout();
            this.neuPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetView1)).BeginInit();
            this.SuspendLayout();
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.btnAddItem);
            this.neuGroupBox1.Controls.Add(this.rbtnAll);
            this.neuGroupBox1.Controls.Add(this.rbtnPharmacy);
            this.neuGroupBox1.Controls.Add(this.rbtnItem);
            this.neuGroupBox1.Controls.Add(this.ntxtFilter);
            this.neuGroupBox1.Controls.Add(this.neuLabel5);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(1071, 49);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 0;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "查询设置";
            // 
            // btnAddItem
            // 
            this.btnAddItem.Location = new System.Drawing.Point(433, 16);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(75, 23);
            this.btnAddItem.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnAddItem.TabIndex = 27;
            this.btnAddItem.Text = "添加项目";
            this.btnAddItem.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnAddItem.UseVisualStyleBackColor = true;
            // 
            // rbtnAll
            // 
            this.rbtnAll.AutoSize = true;
            this.rbtnAll.Checked = true;
            this.rbtnAll.Location = new System.Drawing.Point(370, 20);
            this.rbtnAll.Name = "rbtnAll";
            this.rbtnAll.Size = new System.Drawing.Size(47, 16);
            this.rbtnAll.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rbtnAll.TabIndex = 26;
            this.rbtnAll.TabStop = true;
            this.rbtnAll.Text = "全部";
            this.rbtnAll.UseVisualStyleBackColor = true;
            this.rbtnAll.CheckedChanged += new System.EventHandler(this.rbtnAll_CheckedChanged);
            // 
            // rbtnPharmacy
            // 
            this.rbtnPharmacy.AutoSize = true;
            this.rbtnPharmacy.Location = new System.Drawing.Point(320, 20);
            this.rbtnPharmacy.Name = "rbtnPharmacy";
            this.rbtnPharmacy.Size = new System.Drawing.Size(47, 16);
            this.rbtnPharmacy.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rbtnPharmacy.TabIndex = 25;
            this.rbtnPharmacy.Text = "药品";
            this.rbtnPharmacy.UseVisualStyleBackColor = true;
            this.rbtnPharmacy.CheckedChanged += new System.EventHandler(this.rbtnPharmacy_CheckedChanged);
            // 
            // rbtnItem
            // 
            this.rbtnItem.AutoSize = true;
            this.rbtnItem.Location = new System.Drawing.Point(243, 20);
            this.rbtnItem.Name = "rbtnItem";
            this.rbtnItem.Size = new System.Drawing.Size(83, 16);
            this.rbtnItem.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rbtnItem.TabIndex = 24;
            this.rbtnItem.Text = "非药品项目";
            this.rbtnItem.UseVisualStyleBackColor = true;
            this.rbtnItem.CheckedChanged += new System.EventHandler(this.rbtnItem_CheckedChanged);
            // 
            // ntxtFilter
            // 
            this.ntxtFilter.ForeColor = System.Drawing.Color.Black;
            this.ntxtFilter.IsEnter2Tab = false;
            this.ntxtFilter.Location = new System.Drawing.Point(114, 17);
            this.ntxtFilter.Name = "ntxtFilter";
            this.ntxtFilter.Size = new System.Drawing.Size(111, 21);
            this.ntxtFilter.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ntxtFilter.TabIndex = 2;
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel5.Location = new System.Drawing.Point(17, 21);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(89, 12);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 1;
            this.neuLabel5.Text = "名称编码过滤：";
            // 
            // neuGroupBox2
            // 
            this.neuGroupBox2.Controls.Add(this.neuLabel9);
            this.neuGroupBox2.Controls.Add(this.nlbInfo);
            this.neuGroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuGroupBox2.Location = new System.Drawing.Point(0, 635);
            this.neuGroupBox2.Name = "neuGroupBox2";
            this.neuGroupBox2.Size = new System.Drawing.Size(1071, 52);
            this.neuGroupBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox2.TabIndex = 2;
            this.neuGroupBox2.TabStop = false;
            this.neuGroupBox2.Text = "附加信息";
            // 
            // neuLabel9
            // 
            this.neuLabel9.AutoSize = true;
            this.neuLabel9.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel9.Location = new System.Drawing.Point(17, 25);
            this.neuLabel9.Name = "neuLabel9";
            this.neuLabel9.Size = new System.Drawing.Size(185, 12);
            this.neuLabel9.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel9.TabIndex = 24;
            this.neuLabel9.Text = "基本信息维护需要系统管理员授权";
            // 
            // nlbInfo
            // 
            this.nlbInfo.AutoSize = true;
            this.nlbInfo.Location = new System.Drawing.Point(35, 26);
            this.nlbInfo.Name = "nlbInfo";
            this.nlbInfo.Size = new System.Drawing.Size(0, 12);
            this.nlbInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbInfo.TabIndex = 23;
            // 
            // ngbQuerySet
            // 
            this.ngbQuerySet.Dock = System.Windows.Forms.DockStyle.Top;
            this.ngbQuerySet.Location = new System.Drawing.Point(0, 49);
            this.ngbQuerySet.Name = "ngbQuerySet";
            this.ngbQuerySet.Size = new System.Drawing.Size(1071, 46);
            this.ngbQuerySet.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ngbQuerySet.TabIndex = 17;
            this.ngbQuerySet.TabStop = false;
            this.ngbQuerySet.Text = "查询设置";
            this.ngbQuerySet.Visible = false;
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.neuSpread);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel1.Location = new System.Drawing.Point(0, 95);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(1071, 540);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 18;
            // 
            // neuSpread
            // 
            this.neuSpread.About = "3.0.2004.2005";
            this.neuSpread.AccessibleDescription = "neuSpread, Sheet1, Row 0, Column 0, ";
            this.neuSpread.BackColor = System.Drawing.SystemColors.Control;
            this.neuSpread.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread.Location = new System.Drawing.Point(0, 0);
            this.neuSpread.Name = "neuSpread";
            this.neuSpread.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.sheetView1});
            this.neuSpread.Size = new System.Drawing.Size(1071, 540);
            this.neuSpread.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread.TabIndex = 5;
            tipAppearance3.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance3.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread.TextTipAppearance = tipAppearance3;
            this.neuSpread.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // sheetView1
            // 
            this.sheetView1.Reset();
            this.sheetView1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.sheetView1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.sheetView1.ColumnCount = 0;
            this.sheetView1.RowCount = 0;
            this.sheetView1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.sheetView1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.sheetView1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.sheetView1.DefaultStyle.Locked = false;
            this.sheetView1.DefaultStyle.Parent = "DataAreaDefault";
            this.sheetView1.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetView1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.sheetView1.RowHeader.Columns.Default.Resizable = true;
            this.sheetView1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.sheetView1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.sheetView1.Rows.Default.Height = 22F;
            this.sheetView1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.sheetView1.SheetCornerStyle.Locked = false;
            this.sheetView1.SheetCornerStyle.Parent = "HeaderDefault";
            this.sheetView1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.sheetView1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuSpread.SetActiveViewport(0, 1, 1);
            // 
            // ucComItemExtendInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuPanel1);
            this.Controls.Add(this.ngbQuerySet);
            this.Controls.Add(this.neuGroupBox2);
            this.Controls.Add(this.neuGroupBox1);
            this.Name = "ucComItemExtendInfo";
            this.Size = new System.Drawing.Size(1071, 687);
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.neuGroupBox2.ResumeLayout(false);
            this.neuGroupBox2.PerformLayout();
            this.neuPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.NeuTextBox ntxtFilter;
        protected FS.FrameWork.WinForms.Controls.NeuGroupBox ngbQuerySet;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        protected FS.SOC.Windows.Forms.FpSpread neuSpread;
        private FarPoint.Win.Spread.SheetView sheetView1;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbInfo;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel9;
        private FS.FrameWork.WinForms.Controls.NeuButton btnAddItem;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rbtnAll;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rbtnPharmacy;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rbtnItem;
    }
}
