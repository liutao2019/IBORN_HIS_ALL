namespace FS.SOC.HISFC.Fee.Components.Maintenance.Item
{
    partial class ucItemManager
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
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.ncbitemFlag = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ncmbSystemType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.ntxtFilter = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.ncbMutiQuery = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ncbUnitFlag = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ncbStop = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ncbValid = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ncmbFeeType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuGroupBox2 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuLabel9 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ngbQuerySet = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.ckRightFilter = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ckLeftFilter = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ckAllFilter = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSpread = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.sheetView1 = new FarPoint.Win.Spread.SheetView();
            this.neuGroupBox1.SuspendLayout();
            this.neuGroupBox2.SuspendLayout();
            this.ngbQuerySet.SuspendLayout();
            this.neuPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetView1)).BeginInit();
            this.SuspendLayout();
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.ncbitemFlag);
            this.neuGroupBox1.Controls.Add(this.ncmbSystemType);
            this.neuGroupBox1.Controls.Add(this.ntxtFilter);
            this.neuGroupBox1.Controls.Add(this.ncbMutiQuery);
            this.neuGroupBox1.Controls.Add(this.ncbUnitFlag);
            this.neuGroupBox1.Controls.Add(this.ncbStop);
            this.neuGroupBox1.Controls.Add(this.neuLabel2);
            this.neuGroupBox1.Controls.Add(this.ncbValid);
            this.neuGroupBox1.Controls.Add(this.ncmbFeeType);
            this.neuGroupBox1.Controls.Add(this.neuLabel5);
            this.neuGroupBox1.Controls.Add(this.neuLabel1);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(1071, 49);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 0;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "查询设置";
            // 
            // ncbitemFlag
            // 
            this.ncbitemFlag.AutoSize = true;
            this.ncbitemFlag.Location = new System.Drawing.Point(777, 23);
            this.ncbitemFlag.Name = "ncbitemFlag";
            this.ncbitemFlag.Size = new System.Drawing.Size(60, 16);
            this.ncbitemFlag.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbitemFlag.TabIndex = 13;
            this.ncbitemFlag.Text = "非组套";
            this.ncbitemFlag.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.ncbitemFlag.UseVisualStyleBackColor = true;
            // 
            // ncmbSystemType
            // 
            this.ncmbSystemType.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.ncmbSystemType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ncmbSystemType.FormattingEnabled = true;
            this.ncmbSystemType.IsEnter2Tab = false;
            this.ncmbSystemType.IsFlat = false;
            this.ncmbSystemType.IsLike = true;
            this.ncmbSystemType.IsListOnly = false;
            this.ncmbSystemType.IsPopForm = true;
            this.ncmbSystemType.IsShowCustomerList = false;
            this.ncmbSystemType.IsShowID = false;
            this.ncmbSystemType.IsShowIDAndName = false;
            this.ncmbSystemType.Location = new System.Drawing.Point(307, 21);
            this.ncmbSystemType.Name = "ncmbSystemType";
            this.ncmbSystemType.ShowCustomerList = false;
            this.ncmbSystemType.ShowID = false;
            this.ncmbSystemType.Size = new System.Drawing.Size(102, 20);
            this.ncmbSystemType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.ncmbSystemType.TabIndex = 4;
            this.ncmbSystemType.Tag = "";
            this.ncmbSystemType.ToolBarUse = false;
            // 
            // ntxtFilter
            // 
            this.ntxtFilter.ForeColor = System.Drawing.Color.Black;
            this.ntxtFilter.IsEnter2Tab = false;
            this.ntxtFilter.Location = new System.Drawing.Point(114, 21);
            this.ntxtFilter.Name = "ntxtFilter";
            this.ntxtFilter.Size = new System.Drawing.Size(111, 21);
            this.ntxtFilter.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ntxtFilter.TabIndex = 2;
            // 
            // ncbMutiQuery
            // 
            this.ncbMutiQuery.AutoSize = true;
            this.ncbMutiQuery.Location = new System.Drawing.Point(847, 24);
            this.ncbMutiQuery.Name = "ncbMutiQuery";
            this.ncbMutiQuery.Size = new System.Drawing.Size(120, 16);
            this.ncbMutiQuery.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbMutiQuery.TabIndex = 12;
            this.ncbMutiQuery.Text = "显示更多查询条件";
            this.ncbMutiQuery.UseVisualStyleBackColor = true;
            // 
            // ncbUnitFlag
            // 
            this.ncbUnitFlag.AutoSize = true;
            this.ncbUnitFlag.Location = new System.Drawing.Point(723, 24);
            this.ncbUnitFlag.Name = "ncbUnitFlag";
            this.ncbUnitFlag.Size = new System.Drawing.Size(48, 16);
            this.ncbUnitFlag.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbUnitFlag.TabIndex = 11;
            this.ncbUnitFlag.Text = "组套";
            this.ncbUnitFlag.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.ncbUnitFlag.UseVisualStyleBackColor = true;
            // 
            // ncbStop
            // 
            this.ncbStop.AutoSize = true;
            this.ncbStop.Checked = true;
            this.ncbStop.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ncbStop.Location = new System.Drawing.Point(615, 24);
            this.ncbStop.Name = "ncbStop";
            this.ncbStop.Size = new System.Drawing.Size(48, 16);
            this.ncbStop.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbStop.TabIndex = 9;
            this.ncbStop.Text = "停用";
            this.ncbStop.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.ncbStop.UseVisualStyleBackColor = true;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(424, 25);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(65, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 5;
            this.neuLabel2.Text = "费用类别：";
            // 
            // ncbValid
            // 
            this.ncbValid.AutoSize = true;
            this.ncbValid.Checked = true;
            this.ncbValid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ncbValid.Location = new System.Drawing.Point(669, 24);
            this.ncbValid.Name = "ncbValid";
            this.ncbValid.Size = new System.Drawing.Size(48, 16);
            this.ncbValid.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbValid.TabIndex = 10;
            this.ncbValid.Text = "在用";
            this.ncbValid.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.ncbValid.UseVisualStyleBackColor = true;
            // 
            // ncmbFeeType
            // 
            this.ncmbFeeType.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.ncmbFeeType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ncmbFeeType.FormattingEnabled = true;
            this.ncmbFeeType.IsEnter2Tab = false;
            this.ncmbFeeType.IsFlat = false;
            this.ncmbFeeType.IsLike = true;
            this.ncmbFeeType.IsListOnly = false;
            this.ncmbFeeType.IsPopForm = true;
            this.ncmbFeeType.IsShowCustomerList = false;
            this.ncmbFeeType.IsShowID = false;
            this.ncmbFeeType.IsShowIDAndName = false;
            this.ncmbFeeType.Location = new System.Drawing.Point(495, 22);
            this.ncmbFeeType.Name = "ncmbFeeType";
            this.ncmbFeeType.ShowCustomerList = false;
            this.ncmbFeeType.ShowID = false;
            this.ncmbFeeType.Size = new System.Drawing.Size(102, 20);
            this.ncmbFeeType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.ncmbFeeType.TabIndex = 6;
            this.ncmbFeeType.Tag = "";
            this.ncmbFeeType.ToolBarUse = false;
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel5.Location = new System.Drawing.Point(17, 25);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(89, 12);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 1;
            this.neuLabel5.Text = "名称编码过滤：";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(236, 25);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(65, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 3;
            this.neuLabel1.Text = "系统类别：";
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
            this.ngbQuerySet.Controls.Add(this.ckRightFilter);
            this.ngbQuerySet.Controls.Add(this.ckLeftFilter);
            this.ngbQuerySet.Controls.Add(this.ckAllFilter);
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
            // ckRightFilter
            // 
            this.ckRightFilter.AutoSize = true;
            this.ckRightFilter.Location = new System.Drawing.Point(208, 20);
            this.ckRightFilter.Name = "ckRightFilter";
            this.ckRightFilter.Size = new System.Drawing.Size(60, 16);
            this.ckRightFilter.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ckRightFilter.TabIndex = 12;
            this.ckRightFilter.Text = "右匹配";
            this.ckRightFilter.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.ckRightFilter.UseVisualStyleBackColor = true;
            // 
            // ckLeftFilter
            // 
            this.ckLeftFilter.AutoSize = true;
            this.ckLeftFilter.Location = new System.Drawing.Point(124, 20);
            this.ckLeftFilter.Name = "ckLeftFilter";
            this.ckLeftFilter.Size = new System.Drawing.Size(60, 16);
            this.ckLeftFilter.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ckLeftFilter.TabIndex = 11;
            this.ckLeftFilter.Text = "左匹配";
            this.ckLeftFilter.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.ckLeftFilter.UseVisualStyleBackColor = true;
            // 
            // ckAllFilter
            // 
            this.ckAllFilter.AutoSize = true;
            this.ckAllFilter.Checked = true;
            this.ckAllFilter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckAllFilter.Location = new System.Drawing.Point(46, 20);
            this.ckAllFilter.Name = "ckAllFilter";
            this.ckAllFilter.Size = new System.Drawing.Size(60, 16);
            this.ckAllFilter.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ckAllFilter.TabIndex = 10;
            this.ckAllFilter.Text = "全匹配";
            this.ckAllFilter.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.ckAllFilter.UseVisualStyleBackColor = true;
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
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread.TextTipAppearance = tipAppearance2;
            this.neuSpread.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // sheetView1
            // 
            this.sheetView1.Reset();
            this.sheetView1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.sheetView1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.sheetView1.ColumnCount = 10;
            this.sheetView1.RowCount = 0;
            this.sheetView1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.sheetView1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.sheetView1.ColumnHeader.Rows.Get(0).Height = 25F;
            this.sheetView1.Columns.Get(0).AllowAutoSort = true;
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
            this.neuSpread.SetActiveViewport(0, 1, 0);
            // 
            // ucItemManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuPanel1);
            this.Controls.Add(this.ngbQuerySet);
            this.Controls.Add(this.neuGroupBox2);
            this.Controls.Add(this.neuGroupBox1);
            this.Name = "ucItemManager";
            this.Size = new System.Drawing.Size(1071, 687);
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.neuGroupBox2.ResumeLayout(false);
            this.neuGroupBox2.PerformLayout();
            this.ngbQuerySet.ResumeLayout(false);
            this.ngbQuerySet.PerformLayout();
            this.neuPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox2;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbMutiQuery;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.NeuTextBox ntxtFilter;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbStop;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbValid;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbUnitFlag;
        protected FS.FrameWork.WinForms.Controls.NeuGroupBox ngbQuerySet;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuComboBox ncmbFeeType;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox ncmbSystemType;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        protected FS.SOC.Windows.Forms.FpSpread neuSpread;
        private FarPoint.Win.Spread.SheetView sheetView1;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbInfo;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel9;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ckAllFilter;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ckRightFilter;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ckLeftFilter;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbitemFlag;
    }
}
