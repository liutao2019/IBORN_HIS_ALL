namespace FS.SOC.HISFC.Fee.Components.Maintenance.CompareItem
{
    partial class ucComparedItem
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.neuSpread = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.fpPactList = new FarPoint.Win.Spread.SheetView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.ckAllPact = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.txtCustomCode = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPactList)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(737, 273);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "已对照项目信息";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.neuSpread);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 48);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(731, 222);
            this.panel2.TabIndex = 1;
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
            this.neuSpread.Size = new System.Drawing.Size(731, 222);
            this.neuSpread.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread.TabIndex = 9;
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
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.ckAllPact);
            this.panel1.Controls.Add(this.txtCustomCode);
            this.panel1.Controls.Add(this.neuLabel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(731, 31);
            this.panel1.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(605, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "取消对照";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ckAllPact
            // 
            this.ckAllPact.AutoSize = true;
            this.ckAllPact.Location = new System.Drawing.Point(11, 7);
            this.ckAllPact.Name = "ckAllPact";
            this.ckAllPact.Size = new System.Drawing.Size(48, 16);
            this.ckAllPact.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ckAllPact.TabIndex = 19;
            this.ckAllPact.Text = "全选";
            this.ckAllPact.UseVisualStyleBackColor = true;
            // 
            // txtCustomCode
            // 
            this.txtCustomCode.IsEnter2Tab = false;
            this.txtCustomCode.Location = new System.Drawing.Point(127, 3);
            this.txtCustomCode.Name = "txtCustomCode";
            this.txtCustomCode.Size = new System.Drawing.Size(134, 21);
            this.txtCustomCode.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCustomCode.TabIndex = 18;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(65, 7);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(65, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 17;
            this.neuLabel1.Text = "自定义码：";
            // 
            // ucComparedItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ucComparedItem";
            this.Size = new System.Drawing.Size(737, 273);
            this.groupBox1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPactList)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtCustomCode;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        protected FS.SOC.Windows.Forms.FpSpread neuSpread;
        private FarPoint.Win.Spread.SheetView fpPactList;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ckAllPact;
        private System.Windows.Forms.Button btnCancel;
    }
}
