namespace FS.SOC.HISFC.Fee.Components.Maintenance.Pact
{
    partial class ucFeeCodeRateDetail
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
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSpread = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.fpItemGroup = new FarPoint.Win.Spread.SheetView();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.btnAddItem = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.rbtnAll = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.rbtnPharmacy = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.rbtnItem = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.rbtnMinFee = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.txtCustomCodeForItem = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuSplitter1 = new FS.FrameWork.WinForms.Controls.NeuSplitter();
            this.neuPanel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.btnSavePact = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnAddPact = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnDeletePact = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.lbModifyInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ckAllPact = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.txtCustomCodeForPact = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuSpreadPact = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.fpItemPact = new FarPoint.Win.Spread.SheetView();
            this.neuPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpItemGroup)).BeginInit();
            this.neuPanel2.SuspendLayout();
            this.neuPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpreadPact)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpItemPact)).BeginInit();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.neuSpread);
            this.neuPanel1.Controls.Add(this.neuPanel2);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(317, 379);
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
            this.neuSpread.Location = new System.Drawing.Point(0, 61);
            this.neuSpread.Name = "neuSpread";
            this.neuSpread.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpItemGroup});
            this.neuSpread.Size = new System.Drawing.Size(317, 318);
            this.neuSpread.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread.TabIndex = 11;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread.TextTipAppearance = tipAppearance1;
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
            this.neuPanel2.Controls.Add(this.btnAddItem);
            this.neuPanel2.Controls.Add(this.rbtnAll);
            this.neuPanel2.Controls.Add(this.rbtnPharmacy);
            this.neuPanel2.Controls.Add(this.rbtnItem);
            this.neuPanel2.Controls.Add(this.rbtnMinFee);
            this.neuPanel2.Controls.Add(this.txtCustomCodeForItem);
            this.neuPanel2.Controls.Add(this.neuLabel1);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel2.Location = new System.Drawing.Point(0, 0);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(317, 61);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 10;
            // 
            // btnAddItem
            // 
            this.btnAddItem.Location = new System.Drawing.Point(228, 5);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(75, 23);
            this.btnAddItem.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnAddItem.TabIndex = 23;
            this.btnAddItem.Text = "添加项目";
            this.btnAddItem.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnAddItem.UseVisualStyleBackColor = true;
            // 
            // rbtnAll
            // 
            this.rbtnAll.AutoSize = true;
            this.rbtnAll.Checked = true;
            this.rbtnAll.Location = new System.Drawing.Point(223, 33);
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
            this.rbtnPharmacy.Location = new System.Drawing.Point(173, 33);
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
            this.rbtnItem.Location = new System.Drawing.Point(96, 33);
            this.rbtnItem.Name = "rbtnItem";
            this.rbtnItem.Size = new System.Drawing.Size(71, 16);
            this.rbtnItem.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rbtnItem.TabIndex = 20;
            this.rbtnItem.Text = "物价项目";
            this.rbtnItem.UseVisualStyleBackColor = true;
            // 
            // rbtnMinFee
            // 
            this.rbtnMinFee.AutoSize = true;
            this.rbtnMinFee.Location = new System.Drawing.Point(19, 33);
            this.rbtnMinFee.Name = "rbtnMinFee";
            this.rbtnMinFee.Size = new System.Drawing.Size(71, 16);
            this.rbtnMinFee.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rbtnMinFee.TabIndex = 19;
            this.rbtnMinFee.Text = "最小费用";
            this.rbtnMinFee.UseVisualStyleBackColor = true;
            // 
            // txtCustomCodeForItem
            // 
            this.txtCustomCodeForItem.IsEnter2Tab = false;
            this.txtCustomCodeForItem.Location = new System.Drawing.Point(88, 6);
            this.txtCustomCodeForItem.Name = "txtCustomCodeForItem";
            this.txtCustomCodeForItem.Size = new System.Drawing.Size(134, 21);
            this.txtCustomCodeForItem.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCustomCodeForItem.TabIndex = 3;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(17, 9);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(65, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 2;
            this.neuLabel1.Text = "自定义码：";
            // 
            // neuSplitter1
            // 
            this.neuSplitter1.Location = new System.Drawing.Point(317, 0);
            this.neuSplitter1.Name = "neuSplitter1";
            this.neuSplitter1.Size = new System.Drawing.Size(3, 379);
            this.neuSplitter1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSplitter1.TabIndex = 1;
            this.neuSplitter1.TabStop = false;
            // 
            // neuPanel3
            // 
            this.neuPanel3.Controls.Add(this.btnSavePact);
            this.neuPanel3.Controls.Add(this.btnAddPact);
            this.neuPanel3.Controls.Add(this.btnDeletePact);
            this.neuPanel3.Controls.Add(this.lbModifyInfo);
            this.neuPanel3.Controls.Add(this.ckAllPact);
            this.neuPanel3.Controls.Add(this.txtCustomCodeForPact);
            this.neuPanel3.Controls.Add(this.neuLabel2);
            this.neuPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel3.Location = new System.Drawing.Point(320, 0);
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size(568, 61);
            this.neuPanel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 11;
            // 
            // btnSavePact
            // 
            this.btnSavePact.Location = new System.Drawing.Point(390, 4);
            this.btnSavePact.Name = "btnSavePact";
            this.btnSavePact.Size = new System.Drawing.Size(75, 23);
            this.btnSavePact.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnSavePact.TabIndex = 27;
            this.btnSavePact.Text = "保存";
            this.btnSavePact.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnSavePact.UseVisualStyleBackColor = true;
            // 
            // btnAddPact
            // 
            this.btnAddPact.Location = new System.Drawing.Point(228, 6);
            this.btnAddPact.Name = "btnAddPact";
            this.btnAddPact.Size = new System.Drawing.Size(75, 23);
            this.btnAddPact.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnAddPact.TabIndex = 26;
            this.btnAddPact.Text = "添加";
            this.btnAddPact.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnAddPact.UseVisualStyleBackColor = true;
            // 
            // btnDeletePact
            // 
            this.btnDeletePact.Location = new System.Drawing.Point(309, 5);
            this.btnDeletePact.Name = "btnDeletePact";
            this.btnDeletePact.Size = new System.Drawing.Size(75, 23);
            this.btnDeletePact.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnDeletePact.TabIndex = 25;
            this.btnDeletePact.Text = "删除";
            this.btnDeletePact.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnDeletePact.UseVisualStyleBackColor = true;
            // 
            // lbModifyInfo
            // 
            this.lbModifyInfo.AutoSize = true;
            this.lbModifyInfo.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbModifyInfo.ForeColor = System.Drawing.Color.Red;
            this.lbModifyInfo.Location = new System.Drawing.Point(73, 35);
            this.lbModifyInfo.Name = "lbModifyInfo";
            this.lbModifyInfo.Size = new System.Drawing.Size(82, 14);
            this.lbModifyInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbModifyInfo.TabIndex = 16;
            this.lbModifyInfo.Text = "正在维护：";
            // 
            // ckAllPact
            // 
            this.ckAllPact.AutoSize = true;
            this.ckAllPact.Location = new System.Drawing.Point(19, 35);
            this.ckAllPact.Name = "ckAllPact";
            this.ckAllPact.Size = new System.Drawing.Size(48, 16);
            this.ckAllPact.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ckAllPact.TabIndex = 15;
            this.ckAllPact.Text = "全选";
            this.ckAllPact.UseVisualStyleBackColor = true;
            // 
            // txtCustomCodeForPact
            // 
            this.txtCustomCodeForPact.IsEnter2Tab = false;
            this.txtCustomCodeForPact.Location = new System.Drawing.Point(88, 6);
            this.txtCustomCodeForPact.Name = "txtCustomCodeForPact";
            this.txtCustomCodeForPact.Size = new System.Drawing.Size(134, 21);
            this.txtCustomCodeForPact.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCustomCodeForPact.TabIndex = 3;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(17, 9);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(65, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 2;
            this.neuLabel2.Text = "自定义码：";
            // 
            // neuSpreadPact
            // 
            this.neuSpreadPact.About = "3.0.2004.2005";
            this.neuSpreadPact.AccessibleDescription = "neuSpreadPact, Sheet1, Row 0, Column 0, ";
            this.neuSpreadPact.BackColor = System.Drawing.SystemColors.Control;
            this.neuSpreadPact.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpreadPact.EditModeReplace = true;
            this.neuSpreadPact.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpreadPact.Location = new System.Drawing.Point(320, 61);
            this.neuSpreadPact.Name = "neuSpreadPact";
            this.neuSpreadPact.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpreadPact.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpItemPact});
            this.neuSpreadPact.Size = new System.Drawing.Size(568, 318);
            this.neuSpreadPact.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpreadPact.TabIndex = 12;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpreadPact.TextTipAppearance = tipAppearance2;
            this.neuSpreadPact.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpItemPact
            // 
            this.fpItemPact.Reset();
            this.fpItemPact.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpItemPact.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpItemPact.ColumnCount = 0;
            this.fpItemPact.RowCount = 0;
            this.fpItemPact.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpItemPact.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpItemPact.ColumnHeader.Rows.Get(0).Height = 25F;
            this.fpItemPact.DefaultStyle.Locked = false;
            this.fpItemPact.DefaultStyle.Parent = "DataAreaDefault";
            this.fpItemPact.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpItemPact.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpItemPact.RowHeader.Columns.Default.Resizable = true;
            this.fpItemPact.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpItemPact.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpItemPact.Rows.Default.Height = 22F;
            this.fpItemPact.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpItemPact.SheetCornerStyle.Locked = false;
            this.fpItemPact.SheetCornerStyle.Parent = "HeaderDefault";
            this.fpItemPact.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpItemPact.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuSpreadPact.SetActiveViewport(0, 1, 1);
            // 
            // ucFeeCodeRateDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuSpreadPact);
            this.Controls.Add(this.neuPanel3);
            this.Controls.Add(this.neuSplitter1);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucFeeCodeRateDetail";
            this.Size = new System.Drawing.Size(888, 379);
            this.neuPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpItemGroup)).EndInit();
            this.neuPanel2.ResumeLayout(false);
            this.neuPanel2.PerformLayout();
            this.neuPanel3.ResumeLayout(false);
            this.neuPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpreadPact)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpItemPact)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        protected FS.SOC.Windows.Forms.FpSpread neuSpread;
        private FarPoint.Win.Spread.SheetView fpItemGroup;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtCustomCodeForItem;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuSplitter neuSplitter1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel3;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtCustomCodeForPact;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        protected FS.SOC.Windows.Forms.FpSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView fpItemPact;
        protected FS.SOC.Windows.Forms.FpSpread neuSpreadPact;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ckAllPact;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbModifyInfo;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rbtnPharmacy;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rbtnItem;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rbtnMinFee;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rbtnAll;
        private FS.FrameWork.WinForms.Controls.NeuButton btnAddItem;
        private FS.FrameWork.WinForms.Controls.NeuButton btnAddPact;
        private FS.FrameWork.WinForms.Controls.NeuButton btnDeletePact;
        private FS.FrameWork.WinForms.Controls.NeuButton btnSavePact;


    }
}
