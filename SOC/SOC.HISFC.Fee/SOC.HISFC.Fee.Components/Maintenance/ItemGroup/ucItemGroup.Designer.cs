namespace FS.SOC.HISFC.Fee.Components.Maintenance.ItemGroup
{
    partial class ucItemGroup
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
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.nTxtCustomCode = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ncmbSystemType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ncbMutiQuery = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ngbQuerySet = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.ncmbFeeType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSpread = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.fpItemGroup = new FarPoint.Win.Spread.SheetView();
            this.neuPanel1.SuspendLayout();
            this.ngbQuerySet.SuspendLayout();
            this.neuPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpItemGroup)).BeginInit();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.nTxtCustomCode);
            this.neuPanel1.Controls.Add(this.neuLabel1);
            this.neuPanel1.Controls.Add(this.ncmbSystemType);
            this.neuPanel1.Controls.Add(this.neuLabel2);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(379, 40);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // nTxtCustomCode
            // 
            this.nTxtCustomCode.IsEnter2Tab = false;
            this.nTxtCustomCode.Location = new System.Drawing.Point(63, 10);
            this.nTxtCustomCode.Name = "nTxtCustomCode";
            this.nTxtCustomCode.Size = new System.Drawing.Size(134, 21);
            this.nTxtCustomCode.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nTxtCustomCode.TabIndex = 1;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(3, 13);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(65, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "自定义码：";
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
            this.ncmbSystemType.Location = new System.Drawing.Point(264, 10);
            this.ncmbSystemType.Name = "ncmbSystemType";
            this.ncmbSystemType.ShowCustomerList = false;
            this.ncmbSystemType.ShowID = false;
            this.ncmbSystemType.Size = new System.Drawing.Size(94, 20);
            this.ncmbSystemType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.ncmbSystemType.TabIndex = 6;
            this.ncmbSystemType.Tag = "";
            this.ncmbSystemType.ToolBarUse = false;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(204, 13);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(65, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 5;
            this.neuLabel2.Text = "系统类别：";
            // 
            // ncbMutiQuery
            // 
            this.ncbMutiQuery.AutoSize = true;
            this.ncbMutiQuery.Location = new System.Drawing.Point(206, 9);
            this.ncbMutiQuery.Name = "ncbMutiQuery";
            this.ncbMutiQuery.Size = new System.Drawing.Size(48, 16);
            this.ncbMutiQuery.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbMutiQuery.TabIndex = 13;
            this.ncbMutiQuery.Text = "更多";
            this.ncbMutiQuery.UseVisualStyleBackColor = true;
            // 
            // ngbQuerySet
            // 
            this.ngbQuerySet.Controls.Add(this.ncbMutiQuery);
            this.ngbQuerySet.Controls.Add(this.ncmbFeeType);
            this.ngbQuerySet.Controls.Add(this.neuLabel3);
            this.ngbQuerySet.Dock = System.Windows.Forms.DockStyle.Top;
            this.ngbQuerySet.Location = new System.Drawing.Point(0, 40);
            this.ngbQuerySet.Name = "ngbQuerySet";
            this.ngbQuerySet.Size = new System.Drawing.Size(379, 35);
            this.ngbQuerySet.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ngbQuerySet.TabIndex = 1;
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
            this.ncmbFeeType.Location = new System.Drawing.Point(63, 6);
            this.ncmbFeeType.Name = "ncmbFeeType";
            this.ncmbFeeType.ShowCustomerList = false;
            this.ncmbFeeType.ShowID = false;
            this.ncmbFeeType.Size = new System.Drawing.Size(134, 20);
            this.ncmbFeeType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.ncmbFeeType.TabIndex = 8;
            this.ncmbFeeType.Tag = "";
            this.ncmbFeeType.ToolBarUse = false;
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(3, 9);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(65, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 7;
            this.neuLabel3.Text = "费用类别：";
            // 
            // neuPanel3
            // 
            this.neuPanel3.Controls.Add(this.neuSpread);
            this.neuPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel3.Location = new System.Drawing.Point(0, 75);
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size(379, 379);
            this.neuPanel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 2;
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
            this.fpItemGroup});
            this.neuSpread.Size = new System.Drawing.Size(379, 379);
            this.neuSpread.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread.TabIndex = 6;
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
            // ucItemGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuPanel3);
            this.Controls.Add(this.ngbQuerySet);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucItemGroup";
            this.Size = new System.Drawing.Size(379, 454);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.ngbQuerySet.ResumeLayout(false);
            this.ngbQuerySet.PerformLayout();
            this.neuPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpItemGroup)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox nTxtCustomCode;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbMutiQuery;
        private FS.FrameWork.WinForms.Controls.NeuPanel ngbQuerySet;
        private FS.FrameWork.WinForms.Controls.NeuComboBox ncmbSystemType;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuComboBox ncmbFeeType;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel3;
        protected FS.SOC.Windows.Forms.FpSpread neuSpread;
        private FarPoint.Win.Spread.SheetView fpItemGroup;
    }
}
