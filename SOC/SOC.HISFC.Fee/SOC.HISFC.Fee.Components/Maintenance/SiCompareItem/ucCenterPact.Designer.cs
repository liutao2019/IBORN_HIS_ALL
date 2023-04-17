namespace FS.SOC.HISFC.Fee.Components.Maintenance.SiCompareItem
{
    partial class ucCenterPact
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
            this.ckAllPact = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.txtCustomCode = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuSpread = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.fpPactList = new FarPoint.Win.Spread.SheetView();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuCenterType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.neuPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPactList)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.ckAllPact);
            this.neuPanel1.Controls.Add(this.txtCustomCode);
            this.neuPanel1.Controls.Add(this.neuLabel1);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(3, 17);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(513, 27);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // ckAllPact
            // 
            this.ckAllPact.AutoSize = true;
            this.ckAllPact.Location = new System.Drawing.Point(11, 6);
            this.ckAllPact.Name = "ckAllPact";
            this.ckAllPact.Size = new System.Drawing.Size(48, 16);
            this.ckAllPact.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ckAllPact.TabIndex = 16;
            this.ckAllPact.Text = "全选";
            this.ckAllPact.UseVisualStyleBackColor = true;
            // 
            // txtCustomCode
            // 
            this.txtCustomCode.IsEnter2Tab = false;
            this.txtCustomCode.Location = new System.Drawing.Point(127, 2);
            this.txtCustomCode.Name = "txtCustomCode";
            this.txtCustomCode.Size = new System.Drawing.Size(134, 21);
            this.txtCustomCode.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCustomCode.TabIndex = 3;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(65, 6);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(65, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 2;
            this.neuLabel1.Text = "自定义码：";
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
            this.fpPactList});
            this.neuSpread.Size = new System.Drawing.Size(513, 101);
            this.neuSpread.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread.TabIndex = 8;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread.TextTipAppearance = tipAppearance1;
            this.neuSpread.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
           
            // 
            // fpPactList
            // 
            this.fpPactList.Reset();
            this.fpPactList.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpPactList.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpPactList.ColumnCount = 0;
            this.fpPactList.RowCount = 0;
            this.fpPactList.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpPactList.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpPactList.ColumnHeader.Rows.Get(0).Height = 25F;
            this.fpPactList.DefaultStyle.Locked = false;
            this.fpPactList.DefaultStyle.Parent = "DataAreaDefault";
            this.fpPactList.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPactList.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpPactList.RowHeader.Columns.Default.Resizable = true;
            this.fpPactList.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpPactList.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpPactList.Rows.Default.Height = 22F;
            this.fpPactList.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpPactList.SheetCornerStyle.Locked = false;
            this.fpPactList.SheetCornerStyle.Parent = "HeaderDefault";
            this.fpPactList.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpPactList.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuSpread.SetActiveViewport(0, 1, 1);
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(6, 23);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(65, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 17;
            this.neuLabel2.Text = "医保类型：";
            // 
            // neuCenterType
            // 
            this.neuCenterType.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.neuCenterType.FormattingEnabled = true;
            this.neuCenterType.IsEnter2Tab = false;
            this.neuCenterType.IsFlat = false;
            this.neuCenterType.IsLike = true;
            this.neuCenterType.IsListOnly = false;
            this.neuCenterType.IsPopForm = true;
            this.neuCenterType.IsShowCustomerList = false;
            this.neuCenterType.IsShowID = false;
            this.neuCenterType.Location = new System.Drawing.Point(69, 20);
            this.neuCenterType.Name = "neuCenterType";
            this.neuCenterType.PopForm = null;
            this.neuCenterType.ShowCustomerList = false;
            this.neuCenterType.ShowID = false;
            this.neuCenterType.Size = new System.Drawing.Size(195, 20);
            this.neuCenterType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.neuCenterType.TabIndex = 18;
            this.neuCenterType.Tag = "";
            this.neuCenterType.ToolBarUse = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.neuCenterType);
            this.groupBox1.Controls.Add(this.neuLabel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(519, 45);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Controls.Add(this.neuPanel1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 45);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(519, 148);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "合同单位";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.neuSpread);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 44);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(513, 101);
            this.panel1.TabIndex = 9;
            // 
            // ucCenterPact
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ucCenterPact";
            this.Size = new System.Drawing.Size(519, 193);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPactList)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        protected FS.SOC.Windows.Forms.FpSpread neuSpread;
        private FarPoint.Win.Spread.SheetView fpPactList;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtCustomCode;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ckAllPact;
        private System.Windows.Forms.GroupBox groupBox1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox neuCenterType;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel1;
    }
}
