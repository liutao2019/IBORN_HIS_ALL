namespace FS.HISFC.Components.Order.OutPatient.Controls
{
    partial class ucOutPatientOrder
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            this.pnDisplay = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.ucPatientLabel1 = new FS.HISFC.Components.Common.Controls.ucPatientLabel();
            this.txtInfo = new System.Windows.Forms.RichTextBox();
            this.pnPactInfo = new System.Windows.Forms.Panel();
            this.cmbPact = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.ucOutPatientItemSelect1 = new FS.HISFC.Components.Order.OutPatient.Controls.ucOutPatientItemSelect();
            this.pnTop = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.pnOrderPactInfo = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.rdPact4 = new System.Windows.Forms.RadioButton();
            this.rdPact3 = new System.Windows.Forms.RadioButton();
            this.rdPact2 = new System.Windows.Forms.RadioButton();
            this.rdPact1 = new System.Windows.Forms.RadioButton();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.txtItemInfo = new System.Windows.Forms.RichTextBox();
            this.pnItemInfo = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.pnDisplay.SuspendLayout();
            this.pnPactInfo.SuspendLayout();
            this.pnTop.SuspendLayout();
            this.pnOrderPactInfo.SuspendLayout();
            this.neuPanel1.SuspendLayout();
            this.pnItemInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnDisplay
            // 
            this.pnDisplay.Controls.Add(this.ucPatientLabel1);
            this.pnDisplay.Controls.Add(this.txtInfo);
            this.pnDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnDisplay.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pnDisplay.Location = new System.Drawing.Point(196, 0);
            this.pnDisplay.Name = "pnDisplay";
            this.pnDisplay.Size = new System.Drawing.Size(513, 60);
            this.pnDisplay.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnDisplay.TabIndex = 2;
            this.pnDisplay.Visible = false;
            // 
            // ucPatientLabel1
            // 
            this.ucPatientLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPatientLabel1.Location = new System.Drawing.Point(471, 0);
            this.ucPatientLabel1.Name = "ucPatientLabel1";
            this.ucPatientLabel1.Size = new System.Drawing.Size(42, 60);
            this.ucPatientLabel1.TabIndex = 1;
            // 
            // txtInfo
            // 
            this.txtInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.txtInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInfo.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtInfo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtInfo.Location = new System.Drawing.Point(0, 0);
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.ReadOnly = true;
            this.txtInfo.Size = new System.Drawing.Size(471, 60);
            this.txtInfo.TabIndex = 0;
            this.txtInfo.Text = "";
            // 
            // pnPactInfo
            // 
            this.pnPactInfo.Controls.Add(this.cmbPact);
            this.pnPactInfo.Controls.Add(this.label2);
            this.pnPactInfo.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnPactInfo.Location = new System.Drawing.Point(0, 0);
            this.pnPactInfo.Name = "pnPactInfo";
            this.pnPactInfo.Size = new System.Drawing.Size(196, 60);
            this.pnPactInfo.TabIndex = 7;
            // 
            // cmbPact
            // 
            this.cmbPact.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbPact.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbPact.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPact.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbPact.ForeColor = System.Drawing.Color.Blue;
            this.cmbPact.FormattingEnabled = true;
            this.cmbPact.IsEnter2Tab = false;
            this.cmbPact.IsFlat = false;
            this.cmbPact.IsLike = true;
            this.cmbPact.IsListOnly = false;
            this.cmbPact.IsPopForm = true;
            this.cmbPact.IsShowCustomerList = false;
            this.cmbPact.IsShowID = false;
            this.cmbPact.IsShowIDAndName = false;
            this.cmbPact.Location = new System.Drawing.Point(66, 1);
            this.cmbPact.Name = "cmbPact";
            this.cmbPact.ShowCustomerList = false;
            this.cmbPact.ShowID = false;
            this.cmbPact.Size = new System.Drawing.Size(127, 24);
            this.cmbPact.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbPact.TabIndex = 6;
            this.cmbPact.Tag = "";
            this.cmbPact.ToolBarUse = false;
            this.cmbPact.SelectedIndexChanged += new System.EventHandler(this.cmbPact_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F);
            this.label2.Location = new System.Drawing.Point(-3, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "合同单位：";
            // 
            // ucOutPatientItemSelect1
            // 
            this.ucOutPatientItemSelect1.BackColor = System.Drawing.Color.White;
            this.ucOutPatientItemSelect1.CurrOrder = null;
            this.ucOutPatientItemSelect1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucOutPatientItemSelect1.IsEditGroup = false;
            this.ucOutPatientItemSelect1.Location = new System.Drawing.Point(0, 60);
            this.ucOutPatientItemSelect1.Name = "ucOutPatientItemSelect1";
            this.ucOutPatientItemSelect1.Size = new System.Drawing.Size(709, 85);
            this.ucOutPatientItemSelect1.TabIndex = 0;
            // 
            // pnTop
            // 
            this.pnTop.Controls.Add(this.pnDisplay);
            this.pnTop.Controls.Add(this.pnPactInfo);
            this.pnTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTop.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pnTop.Location = new System.Drawing.Point(0, 0);
            this.pnTop.Name = "pnTop";
            this.pnTop.Size = new System.Drawing.Size(709, 60);
            this.pnTop.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnTop.TabIndex = 8;
            // 
            // pnOrderPactInfo
            // 
            this.pnOrderPactInfo.Controls.Add(this.rdPact4);
            this.pnOrderPactInfo.Controls.Add(this.rdPact3);
            this.pnOrderPactInfo.Controls.Add(this.rdPact2);
            this.pnOrderPactInfo.Controls.Add(this.rdPact1);
            this.pnOrderPactInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnOrderPactInfo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pnOrderPactInfo.Location = new System.Drawing.Point(0, 454);
            this.pnOrderPactInfo.Name = "pnOrderPactInfo";
            this.pnOrderPactInfo.Size = new System.Drawing.Size(709, 28);
            this.pnOrderPactInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnOrderPactInfo.TabIndex = 9;
            this.pnOrderPactInfo.Visible = false;
            // 
            // rdPact4
            // 
            this.rdPact4.AutoSize = true;
            this.rdPact4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdPact4.Location = new System.Drawing.Point(452, 6);
            this.rdPact4.Name = "rdPact4";
            this.rdPact4.Size = new System.Drawing.Size(55, 18);
            this.rdPact4.TabIndex = 3;
            this.rdPact4.TabStop = true;
            this.rdPact4.Text = "自费";
            this.rdPact4.UseVisualStyleBackColor = true;
            this.rdPact4.Visible = false;
            // 
            // rdPact3
            // 
            this.rdPact3.AutoSize = true;
            this.rdPact3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdPact3.Location = new System.Drawing.Point(310, 6);
            this.rdPact3.Name = "rdPact3";
            this.rdPact3.Size = new System.Drawing.Size(85, 18);
            this.rdPact3.TabIndex = 2;
            this.rdPact3.TabStop = true;
            this.rdPact3.Text = "顺德医保";
            this.rdPact3.UseVisualStyleBackColor = true;
            this.rdPact3.Visible = false;
            // 
            // rdPact2
            // 
            this.rdPact2.AutoSize = true;
            this.rdPact2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdPact2.Location = new System.Drawing.Point(192, 6);
            this.rdPact2.Name = "rdPact2";
            this.rdPact2.Size = new System.Drawing.Size(55, 18);
            this.rdPact2.TabIndex = 1;
            this.rdPact2.TabStop = true;
            this.rdPact2.Text = "自费";
            this.rdPact2.UseVisualStyleBackColor = true;
            this.rdPact2.Visible = false;
            // 
            // rdPact1
            // 
            this.rdPact1.AutoSize = true;
            this.rdPact1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdPact1.Location = new System.Drawing.Point(50, 6);
            this.rdPact1.Name = "rdPact1";
            this.rdPact1.Size = new System.Drawing.Size(85, 18);
            this.rdPact1.TabIndex = 0;
            this.rdPact1.TabStop = true;
            this.rdPact1.Text = "顺德医保";
            this.rdPact1.UseVisualStyleBackColor = true;
            this.rdPact1.Visible = false;
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.txtItemInfo);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuPanel1.Location = new System.Drawing.Point(0, 382);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(709, 72);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 10;
            // 
            // txtItemInfo
            // 
            this.txtItemInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtItemInfo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtItemInfo.ForeColor = System.Drawing.Color.Blue;
            this.txtItemInfo.Location = new System.Drawing.Point(0, 0);
            this.txtItemInfo.Name = "txtItemInfo";
            this.txtItemInfo.Size = new System.Drawing.Size(709, 72);
            this.txtItemInfo.TabIndex = 3;
            this.txtItemInfo.Text = "";
            // 
            // pnItemInfo
            // 
            this.pnItemInfo.Controls.Add(this.neuSpread1);
            this.pnItemInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnItemInfo.Location = new System.Drawing.Point(0, 145);
            this.pnItemInfo.Name = "pnItemInfo";
            this.pnItemInfo.Size = new System.Drawing.Size(709, 237);
            this.pnItemInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnItemInfo.TabIndex = 11;
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
            this.neuSpread1.Location = new System.Drawing.Point(0, 0);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(709, 237);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 3;
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
            this.neuSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.Rows.Default.Height = 25F;
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.neuSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ucOutPatientOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScroll = true;
            this.Controls.Add(this.pnItemInfo);
            this.Controls.Add(this.neuPanel1);
            this.Controls.Add(this.ucOutPatientItemSelect1);
            this.Controls.Add(this.pnOrderPactInfo);
            this.Controls.Add(this.pnTop);
            this.Name = "ucOutPatientOrder";
            this.Size = new System.Drawing.Size(709, 482);
            this.pnDisplay.ResumeLayout(false);
            this.pnPactInfo.ResumeLayout(false);
            this.pnPactInfo.PerformLayout();
            this.pnTop.ResumeLayout(false);
            this.pnOrderPactInfo.ResumeLayout(false);
            this.pnOrderPactInfo.PerformLayout();
            this.neuPanel1.ResumeLayout(false);
            this.pnItemInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ucOutPatientItemSelect ucOutPatientItemSelect1;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnDisplay;
        private System.Windows.Forms.Label label2;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbPact;
        private System.Windows.Forms.Panel pnPactInfo;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnTop;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnOrderPactInfo;
        private System.Windows.Forms.RadioButton rdPact2;
        private System.Windows.Forms.RadioButton rdPact1;
        private System.Windows.Forms.RadioButton rdPact4;
        private System.Windows.Forms.RadioButton rdPact3;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnItemInfo;
        public FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private System.Windows.Forms.RichTextBox txtItemInfo;
        private System.Windows.Forms.RichTextBox txtInfo;
        private FS.HISFC.Components.Common.Controls.ucPatientLabel ucPatientLabel1;


    }
}