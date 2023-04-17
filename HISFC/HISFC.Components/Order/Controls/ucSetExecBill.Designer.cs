namespace FS.HISFC.Components.Order.Controls
{
    partial class ucSetExecBill : FS.FrameWork.WinForms.Controls.ucBaseControl
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
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.tabItemType = new FS.FrameWork.WinForms.Controls.NeuTabControl();
            this.tabdrug = new System.Windows.Forms.TabPage();
            this.tvDrug = new FS.FrameWork.WinForms.Controls.NeuTreeView();
            this.tabUndrag = new System.Windows.Forms.TabPage();
            this.tvUndrug = new FS.FrameWork.WinForms.Controls.NeuTreeView();
            this.tabUndrugItem = new System.Windows.Forms.TabPage();
            this.tvUndrugItem = new FS.FrameWork.WinForms.Controls.NeuTreeView();
            this.neuSplitter1 = new FS.FrameWork.WinForms.Controls.NeuSplitter();
            this.grpExecBillName = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.filetype = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.lblExecBillName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.grpExecBillD = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel1.SuspendLayout();
            this.tabItemType.SuspendLayout();
            this.tabdrug.SuspendLayout();
            this.tabUndrag.SuspendLayout();
            this.tabUndrugItem.SuspendLayout();
            this.grpExecBillName.SuspendLayout();
            this.grpExecBillD.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.neuPanel2.SuspendLayout();
            this.neuPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.tabItemType);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(200, 508);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // tabItemType
            // 
            this.tabItemType.Controls.Add(this.tabdrug);
            this.tabItemType.Controls.Add(this.tabUndrag);
            this.tabItemType.Controls.Add(this.tabUndrugItem);
            this.tabItemType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabItemType.Location = new System.Drawing.Point(0, 0);
            this.tabItemType.Name = "tabItemType";
            this.tabItemType.SelectedIndex = 0;
            this.tabItemType.Size = new System.Drawing.Size(200, 508);
            this.tabItemType.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tabItemType.TabIndex = 0;
            // 
            // tabdrug
            // 
            this.tabdrug.Controls.Add(this.tvDrug);
            this.tabdrug.Location = new System.Drawing.Point(4, 22);
            this.tabdrug.Name = "tabdrug";
            this.tabdrug.Padding = new System.Windows.Forms.Padding(3);
            this.tabdrug.Size = new System.Drawing.Size(192, 482);
            this.tabdrug.TabIndex = 0;
            this.tabdrug.Text = "药物执行单";
            this.tabdrug.UseVisualStyleBackColor = true;
            // 
            // tvDrug
            // 
            this.tvDrug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvDrug.HideSelection = false;
            this.tvDrug.Location = new System.Drawing.Point(3, 3);
            this.tvDrug.Name = "tvDrug";
            this.tvDrug.Size = new System.Drawing.Size(186, 476);
            this.tvDrug.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tvDrug.TabIndex = 0;
            this.tvDrug.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvDrug_NodeMouseDoubleClick);
            // 
            // tabUndrag
            // 
            this.tabUndrag.Controls.Add(this.tvUndrug);
            this.tabUndrag.Location = new System.Drawing.Point(4, 22);
            this.tabUndrag.Name = "tabUndrag";
            this.tabUndrag.Padding = new System.Windows.Forms.Padding(3);
            this.tabUndrag.Size = new System.Drawing.Size(192, 482);
            this.tabUndrag.TabIndex = 1;
            this.tabUndrag.Text = "非药物执行单";
            this.tabUndrag.UseVisualStyleBackColor = true;
            // 
            // tvUndrug
            // 
            this.tvUndrug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvUndrug.HideSelection = false;
            this.tvUndrug.Location = new System.Drawing.Point(3, 3);
            this.tvUndrug.Name = "tvUndrug";
            this.tvUndrug.Size = new System.Drawing.Size(186, 476);
            this.tvUndrug.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tvUndrug.TabIndex = 0;
            this.tvUndrug.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvUndrug_NodeMouseDoubleClick);
            // 
            // tabUndrugItem
            // 
            this.tabUndrugItem.Controls.Add(this.tvUndrugItem);
            this.tabUndrugItem.Location = new System.Drawing.Point(4, 22);
            this.tabUndrugItem.Name = "tabUndrugItem";
            this.tabUndrugItem.Size = new System.Drawing.Size(192, 482);
            this.tabUndrugItem.TabIndex = 2;
            this.tabUndrugItem.Text = "非药物项目执行单";
            this.tabUndrugItem.UseVisualStyleBackColor = true;
            // 
            // tvUndrugItem
            // 
            this.tvUndrugItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvUndrugItem.HideSelection = false;
            this.tvUndrugItem.Location = new System.Drawing.Point(0, 0);
            this.tvUndrugItem.Name = "tvUndrugItem";
            this.tvUndrugItem.Size = new System.Drawing.Size(192, 482);
            this.tvUndrugItem.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tvUndrugItem.TabIndex = 1;
            this.tvUndrugItem.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvUndrugItem_NodeMouseDoubleClick);
            // 
            // neuSplitter1
            // 
            this.neuSplitter1.Location = new System.Drawing.Point(200, 0);
            this.neuSplitter1.Name = "neuSplitter1";
            this.neuSplitter1.Size = new System.Drawing.Size(4, 508);
            this.neuSplitter1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSplitter1.TabIndex = 1;
            this.neuSplitter1.TabStop = false;
            // 
            // grpExecBillName
            // 
            this.grpExecBillName.Controls.Add(this.filetype);
            this.grpExecBillName.Controls.Add(this.txtFilter);
            this.grpExecBillName.Controls.Add(this.lblExecBillName);
            this.grpExecBillName.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpExecBillName.Location = new System.Drawing.Point(0, 0);
            this.grpExecBillName.Name = "grpExecBillName";
            this.grpExecBillName.Size = new System.Drawing.Size(540, 59);
            this.grpExecBillName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.grpExecBillName.TabIndex = 2;
            this.grpExecBillName.TabStop = false;
            // 
            // filetype
            // 
            this.filetype.AutoSize = true;
            this.filetype.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.filetype.ForeColor = System.Drawing.Color.Blue;
            this.filetype.Location = new System.Drawing.Point(394, 37);
            this.filetype.Name = "filetype";
            this.filetype.Size = new System.Drawing.Size(35, 14);
            this.filetype.TabIndex = 3;
            this.filetype.Text = "类别";
            this.filetype.Click += new System.EventHandler(this.filetype_Click);
            // 
            // txtFilter
            // 
            this.txtFilter.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtFilter.Location = new System.Drawing.Point(434, 32);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(100, 23);
            this.txtFilter.TabIndex = 2;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // lblExecBillName
            // 
            this.lblExecBillName.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblExecBillName.Location = new System.Drawing.Point(37, 11);
            this.lblExecBillName.Name = "lblExecBillName";
            this.lblExecBillName.Size = new System.Drawing.Size(349, 31);
            this.lblExecBillName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblExecBillName.TabIndex = 0;
            this.lblExecBillName.Text = "执行单名称";
            this.lblExecBillName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpExecBillD
            // 
            this.grpExecBillD.Controls.Add(this.neuSpread1);
            this.grpExecBillD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpExecBillD.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpExecBillD.Location = new System.Drawing.Point(0, 0);
            this.grpExecBillD.Name = "grpExecBillD";
            this.grpExecBillD.Size = new System.Drawing.Size(540, 449);
            this.grpExecBillD.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.grpExecBillD.TabIndex = 3;
            this.grpExecBillD.TabStop = false;
            this.grpExecBillD.Text = "执行单明细";
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, ";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(3, 17);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(534, 429);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.ActiveSheetChanged += new System.EventHandler(this.neuSpread1_ActiveSheetChanged);
            this.neuSpread1.ActiveSheetChanging += new FarPoint.Win.Spread.ActiveSheetChangingEventHandler(this.neuSpread1_ActiveSheetChanging);
            this.neuSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.neuSpread1_CellDoubleClick);
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.neuSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ExtendedSelect;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.neuSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.Rows.Default.Height = 25F;
            this.neuSpread1_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.MultiRange;
            this.neuSpread1_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.neuSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.neuPanel3);
            this.neuPanel2.Controls.Add(this.grpExecBillName);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel2.Location = new System.Drawing.Point(204, 0);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(540, 508);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 4;
            // 
            // neuPanel3
            // 
            this.neuPanel3.Controls.Add(this.grpExecBillD);
            this.neuPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel3.Location = new System.Drawing.Point(0, 59);
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size(540, 449);
            this.neuPanel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 4;
            // 
            // ucSetExecBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuPanel2);
            this.Controls.Add(this.neuSplitter1);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucSetExecBill";
            this.Size = new System.Drawing.Size(744, 508);
            this.Load += new System.EventHandler(this.ucSetExecBill_Load);
            this.neuPanel1.ResumeLayout(false);
            this.tabItemType.ResumeLayout(false);
            this.tabdrug.ResumeLayout(false);
            this.tabUndrag.ResumeLayout(false);
            this.tabUndrugItem.ResumeLayout(false);
            this.grpExecBillName.ResumeLayout(false);
            this.grpExecBillName.PerformLayout();
            this.grpExecBillD.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.neuPanel2.ResumeLayout(false);
            this.neuPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuSplitter neuSplitter1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox grpExecBillName;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblExecBillName;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox grpExecBillD;
        private FS.FrameWork.WinForms.Controls.NeuTabControl tabItemType;
        private System.Windows.Forms.TabPage tabdrug;
        private System.Windows.Forms.TabPage tabUndrag;
        private FS.FrameWork.WinForms.Controls.NeuTreeView tvDrug;
        private FS.FrameWork.WinForms.Controls.NeuTreeView tvUndrug;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel3;
        private System.Windows.Forms.TabPage tabUndrugItem;
        private FS.FrameWork.WinForms.Controls.NeuTreeView tvUndrugItem;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label filetype;
    }
}
