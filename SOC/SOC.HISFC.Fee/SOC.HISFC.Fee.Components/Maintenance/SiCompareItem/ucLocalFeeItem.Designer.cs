namespace FS.SOC.HISFC.Fee.Components.Maintenance.SiCompareItem
{
    partial class ucLocalFeeItem
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
            FarPoint.Win.Spread.TipAppearance tipAppearance4 = new FarPoint.Win.Spread.TipAppearance();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSpread = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.fpItemGroup = new FarPoint.Win.Spread.SheetView();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.rbtnAll = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.rbtnPharmacy = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.rbtnItem = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.txtCustomCodeForItem = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpItemGroup)).BeginInit();
            this.neuPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.neuSpread);
            this.neuPanel1.Controls.Add(this.neuPanel2);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(589, 379);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // neuSpread
            // 
            this.neuSpread.About = "3.0.2004.2005";
            this.neuSpread.AccessibleDescription = "neuSpread, Sheet1, Row 0, Column 0, ";
            this.neuSpread.BackColor = System.Drawing.SystemColors.Control;
            this.neuSpread.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread.Location = new System.Drawing.Point(0, 55);
            this.neuSpread.Name = "neuSpread";
            this.neuSpread.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpItemGroup});
            this.neuSpread.Size = new System.Drawing.Size(589, 324);
            this.neuSpread.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread.TabIndex = 11;
            tipAppearance4.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance4.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread.TextTipAppearance = tipAppearance4;
            this.neuSpread.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpItemGroup
            // 
            this.fpItemGroup.Reset();
            this.fpItemGroup.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpItemGroup.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpItemGroup.ColumnCount = 0;
            this.fpItemGroup.RowCount = 0;
            this.fpItemGroup.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpItemGroup.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpItemGroup.ColumnHeader.Rows.Get(0).Height = 25F;
            this.fpItemGroup.DefaultStyle.Locked = false;
            this.fpItemGroup.DefaultStyle.Parent = "DataAreaDefault";
            this.fpItemGroup.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpItemGroup.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpItemGroup.RowHeader.Columns.Default.Resizable = true;
            this.fpItemGroup.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpItemGroup.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpItemGroup.Rows.Default.Height = 22F;
            this.fpItemGroup.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpItemGroup.SheetCornerStyle.Locked = false;
            this.fpItemGroup.SheetCornerStyle.Parent = "HeaderDefault";
            this.fpItemGroup.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpItemGroup.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuSpread.SetActiveViewport(0, 1, 1);
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.rbtnAll);
            this.neuPanel2.Controls.Add(this.rbtnPharmacy);
            this.neuPanel2.Controls.Add(this.rbtnItem);
            this.neuPanel2.Controls.Add(this.txtCustomCodeForItem);
            this.neuPanel2.Controls.Add(this.neuLabel1);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel2.Location = new System.Drawing.Point(0, 0);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(589, 55);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 10;
            // 
            // rbtnAll
            // 
            this.rbtnAll.AutoSize = true;
            this.rbtnAll.Checked = true;
            this.rbtnAll.Location = new System.Drawing.Point(140, 3);
            this.rbtnAll.Name = "rbtnAll";
            this.rbtnAll.Size = new System.Drawing.Size(47, 16);
            this.rbtnAll.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rbtnAll.TabIndex = 22;
            this.rbtnAll.TabStop = true;
            this.rbtnAll.Text = "全部";
            this.rbtnAll.UseVisualStyleBackColor = true;
            // 
            // rbtnPharmacy
            // 
            this.rbtnPharmacy.AutoSize = true;
            this.rbtnPharmacy.Location = new System.Drawing.Point(83, 3);
            this.rbtnPharmacy.Name = "rbtnPharmacy";
            this.rbtnPharmacy.Size = new System.Drawing.Size(47, 16);
            this.rbtnPharmacy.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rbtnPharmacy.TabIndex = 21;
            this.rbtnPharmacy.Text = "药品";
            this.rbtnPharmacy.UseVisualStyleBackColor = true;
            // 
            // rbtnItem
            // 
            this.rbtnItem.AutoSize = true;
            this.rbtnItem.Location = new System.Drawing.Point(11, 3);
            this.rbtnItem.Name = "rbtnItem";
            this.rbtnItem.Size = new System.Drawing.Size(59, 16);
            this.rbtnItem.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rbtnItem.TabIndex = 20;
            this.rbtnItem.Text = "非药品";
            this.rbtnItem.UseVisualStyleBackColor = true;
            // 
            // txtCustomCodeForItem
            // 
            this.txtCustomCodeForItem.IsEnter2Tab = false;
            this.txtCustomCodeForItem.Location = new System.Drawing.Point(83, 28);
            this.txtCustomCodeForItem.Name = "txtCustomCodeForItem";
            this.txtCustomCodeForItem.Size = new System.Drawing.Size(134, 21);
            this.txtCustomCodeForItem.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCustomCodeForItem.TabIndex = 3;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(12, 31);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(65, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 2;
            this.neuLabel1.Text = "自定义码：";
            // 
            // ucFeeItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucFeeItem";
            this.Size = new System.Drawing.Size(589, 379);
            this.neuPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpItemGroup)).EndInit();
            this.neuPanel2.ResumeLayout(false);
            this.neuPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        protected FS.SOC.Windows.Forms.FpSpread neuSpread;
        private FarPoint.Win.Spread.SheetView fpItemGroup;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtCustomCodeForItem;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        protected FS.SOC.Windows.Forms.FpSpread fpSpread1;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rbtnPharmacy;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rbtnItem;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rbtnAll;


    }
}
