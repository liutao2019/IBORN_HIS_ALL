namespace FS.SOC.HISFC.Fee.Components.Maintenance.ItemGroup
{
    partial class ucItemGroupDetail
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblUserCode = new System.Windows.Forms.Label();
            this.lbPrice = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lbItemGroupCustom = new System.Windows.Forms.Label();
            this.lbItemGroupName = new System.Windows.Forms.Label();
            this.nTxtCustomCode = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuSpread = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnLookUndrug = new System.Windows.Forms.ToolStripMenuItem();
            this.fpItemGroup = new FarPoint.Win.Spread.SheetView();
            this.label2 = new System.Windows.Forms.Label();
            this.lbChildPrice = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbSpecialPrice = new System.Windows.Forms.Label();
            this.neuPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpItemGroup)).BeginInit();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.label4);
            this.neuPanel1.Controls.Add(this.lbSpecialPrice);
            this.neuPanel1.Controls.Add(this.label2);
            this.neuPanel1.Controls.Add(this.lbChildPrice);
            this.neuPanel1.Controls.Add(this.label1);
            this.neuPanel1.Controls.Add(this.lblUserCode);
            this.neuPanel1.Controls.Add(this.lbPrice);
            this.neuPanel1.Controls.Add(this.lblName);
            this.neuPanel1.Controls.Add(this.lbItemGroupCustom);
            this.neuPanel1.Controls.Add(this.lbItemGroupName);
            this.neuPanel1.Controls.Add(this.nTxtCustomCode);
            this.neuPanel1.Controls.Add(this.neuLabel1);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(771, 66);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(31, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 16);
            this.label1.TabIndex = 18;
            this.label1.Text = "默认价：";
            // 
            // lblUserCode
            // 
            this.lblUserCode.AutoSize = true;
            this.lblUserCode.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblUserCode.ForeColor = System.Drawing.Color.Blue;
            this.lblUserCode.Location = new System.Drawing.Point(226, 10);
            this.lblUserCode.Name = "lblUserCode";
            this.lblUserCode.Size = new System.Drawing.Size(93, 16);
            this.lblUserCode.TabIndex = 17;
            this.lblUserCode.Text = "组套编码：";
            // 
            // lbPrice
            // 
            this.lbPrice.AutoSize = true;
            this.lbPrice.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbPrice.ForeColor = System.Drawing.Color.Red;
            this.lbPrice.Location = new System.Drawing.Point(106, 40);
            this.lbPrice.Name = "lbPrice";
            this.lbPrice.Size = new System.Drawing.Size(62, 16);
            this.lbPrice.TabIndex = 17;
            this.lbPrice.Text = "100.00";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblName.ForeColor = System.Drawing.Color.Blue;
            this.lblName.Location = new System.Drawing.Point(421, 10);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(93, 16);
            this.lblName.TabIndex = 16;
            this.lblName.Text = "组套名称：";
            // 
            // lbItemGroupCustom
            // 
            this.lbItemGroupCustom.AutoSize = true;
            this.lbItemGroupCustom.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbItemGroupCustom.ForeColor = System.Drawing.Color.Red;
            this.lbItemGroupCustom.Location = new System.Drawing.Point(320, 10);
            this.lbItemGroupCustom.Name = "lbItemGroupCustom";
            this.lbItemGroupCustom.Size = new System.Drawing.Size(62, 16);
            this.lbItemGroupCustom.TabIndex = 15;
            this.lbItemGroupCustom.Text = "/20035";
            // 
            // lbItemGroupName
            // 
            this.lbItemGroupName.AutoSize = true;
            this.lbItemGroupName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbItemGroupName.ForeColor = System.Drawing.Color.Red;
            this.lbItemGroupName.Location = new System.Drawing.Point(514, 10);
            this.lbItemGroupName.Name = "lbItemGroupName";
            this.lbItemGroupName.Size = new System.Drawing.Size(76, 16);
            this.lbItemGroupName.TabIndex = 14;
            this.lbItemGroupName.Text = "测试组套";
            // 
            // nTxtCustomCode
            // 
            this.nTxtCustomCode.IsEnter2Tab = false;
            this.nTxtCustomCode.Location = new System.Drawing.Point(74, 10);
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
            // neuSpread
            // 
            this.neuSpread.About = "3.0.2004.2005";
            this.neuSpread.AccessibleDescription = "neuSpread, Sheet1, Row 0, Column 0, ";
            this.neuSpread.BackColor = System.Drawing.SystemColors.Control;
            this.neuSpread.ContextMenuStrip = this.contextMenuStrip1;
            this.neuSpread.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread.Location = new System.Drawing.Point(0, 66);
            this.neuSpread.Name = "neuSpread";
            this.neuSpread.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpItemGroup});
            this.neuSpread.Size = new System.Drawing.Size(771, 371);
            this.neuSpread.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread.TabIndex = 7;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread.TextTipAppearance = tipAppearance1;
            this.neuSpread.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnLookUndrug});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(143, 26);
            // 
            // btnLookUndrug
            // 
            this.btnLookUndrug.Name = "btnLookUndrug";
            this.btnLookUndrug.Size = new System.Drawing.Size(142, 22);
            this.btnLookUndrug.Text = "查看物价属性";
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
            this.fpItemGroup.RowHeader.Columns.Default.Resizable = true;
            this.fpItemGroup.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpItemGroup.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpItemGroup.Rows.Default.Height = 22F;
            this.fpItemGroup.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpItemGroup.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpItemGroup.SheetCornerStyle.Locked = false;
            this.fpItemGroup.SheetCornerStyle.Parent = "HeaderDefault";
            this.fpItemGroup.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpItemGroup.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuSpread.SetActiveViewport(0, 1, 1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(242, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 16);
            this.label2.TabIndex = 20;
            this.label2.Text = "儿童价：";
            // 
            // lbChildPrice
            // 
            this.lbChildPrice.AutoSize = true;
            this.lbChildPrice.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbChildPrice.ForeColor = System.Drawing.Color.Red;
            this.lbChildPrice.Location = new System.Drawing.Point(318, 40);
            this.lbChildPrice.Name = "lbChildPrice";
            this.lbChildPrice.Size = new System.Drawing.Size(62, 16);
            this.lbChildPrice.TabIndex = 19;
            this.lbChildPrice.Text = "100.00";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(438, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 16);
            this.label4.TabIndex = 22;
            this.label4.Text = "特诊价：";
            // 
            // lbSpecialPrice
            // 
            this.lbSpecialPrice.AutoSize = true;
            this.lbSpecialPrice.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbSpecialPrice.ForeColor = System.Drawing.Color.Red;
            this.lbSpecialPrice.Location = new System.Drawing.Point(520, 40);
            this.lbSpecialPrice.Name = "lbSpecialPrice";
            this.lbSpecialPrice.Size = new System.Drawing.Size(62, 16);
            this.lbSpecialPrice.TabIndex = 21;
            this.lbSpecialPrice.Text = "100.00";
            // 
            // ucItemGroupDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuSpread);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucItemGroupDetail";
            this.Size = new System.Drawing.Size(771, 437);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpItemGroup)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox nTxtCustomCode;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private System.Windows.Forms.Label lblUserCode;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lbItemGroupCustom;
        private System.Windows.Forms.Label lbItemGroupName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbPrice;
        protected FS.SOC.Windows.Forms.FpSpread neuSpread;
        private FarPoint.Win.Spread.SheetView fpItemGroup;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btnLookUndrug;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbSpecialPrice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbChildPrice;
    }
}
