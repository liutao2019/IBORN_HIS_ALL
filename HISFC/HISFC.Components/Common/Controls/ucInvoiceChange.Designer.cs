namespace FS.HISFC.Components.Common.Controls
{
    partial class ucInvoiceChange
    {
        /// <summary>
        /// ����������������
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// ������������ʹ�õ���Դ��
        /// </summary>
        /// <param name="disposing">���Ӧ�ͷ��й���Դ��Ϊ true������Ϊ false��</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows ������������ɵĴ���

        /// <summary>
        /// �����֧������ķ��� - ��Ҫ
        /// ʹ�ô���༭���޸Ĵ˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtUsedNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbInvoiceType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.btnExit = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnOK = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuPanel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel1.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            this.neuPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.neuLabel2);
            this.neuPanel1.Controls.Add(this.txtUsedNO);
            this.neuPanel1.Controls.Add(this.neuLabel1);
            this.neuPanel1.Controls.Add(this.cmbInvoiceType);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(604, 56);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(223, 22);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(77, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 3;
            this.neuLabel2.Text = "���÷�Ʊ�ţ�";
            // 
            // txtUsedNO
            // 
            this.txtUsedNO.Location = new System.Drawing.Point(306, 17);
            this.txtUsedNO.Name = "txtUsedNO";
            this.txtUsedNO.Size = new System.Drawing.Size(111, 21);
            this.txtUsedNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtUsedNO.TabIndex = 2;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(17, 22);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(53, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 1;
            this.neuLabel1.Text = "��Ʊ����";
            // 
            // cmbInvoiceType
            // 
            //this.cmbInvoiceType.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbInvoiceType.FormattingEnabled = true;
            this.cmbInvoiceType.IsFlat = true;
            this.cmbInvoiceType.IsLike = true;
            this.cmbInvoiceType.Location = new System.Drawing.Point(73, 17);
            this.cmbInvoiceType.Name = "cmbInvoiceType";
            this.cmbInvoiceType.PopForm = null;
            this.cmbInvoiceType.ShowCustomerList = false;
            this.cmbInvoiceType.ShowID = false;
            this.cmbInvoiceType.Size = new System.Drawing.Size(106, 20);
            this.cmbInvoiceType.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbInvoiceType.TabIndex = 0;
            this.cmbInvoiceType.Tag = "";
            this.cmbInvoiceType.ToolBarUse = false;
            this.cmbInvoiceType.SelectedIndexChanged += new System.EventHandler(this.cmbInvoiceType_SelectedIndexChanged);
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.btnExit);
            this.neuPanel2.Controls.Add(this.btnOK);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuPanel2.Location = new System.Drawing.Point(0, 200);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(604, 52);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 1;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExit.Location = new System.Drawing.Point(495, 15);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "�˳�";
            this.btnExit.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(411, 15);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "����";
            this.btnOK.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // neuPanel3
            // 
            this.neuPanel3.Controls.Add(this.neuSpread1);
            this.neuPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel3.Location = new System.Drawing.Point(0, 56);
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size(604, 144);
            this.neuPanel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 2;
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "2.5.2007.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1";
            this.neuSpread1.BackColor = System.Drawing.Color.Transparent;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(0, 0);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(604, 144);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.neuSpread1_CellClick);
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ColumnCount = 6;
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "��ȡ��";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "��Ʊ����";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "��ʼ��";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "���ú�";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "��ֹ��";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "��ȡʱ��";
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "��ȡ��";
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 70F;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "��Ʊ����";
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 75F;
            this.neuSpread1_Sheet1.Columns.Get(2).CellType = textCellType1;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "��ʼ��";
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 100F;
            this.neuSpread1_Sheet1.Columns.Get(3).CellType = textCellType2;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "���ú�";
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 100F;
            this.neuSpread1_Sheet1.Columns.Get(4).CellType = textCellType3;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "��ֹ��";
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 100F;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "��ȡʱ��";
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 120F;
            this.neuSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuSpread1.SetActiveViewport(1, 0);
            // 
            // ucInvoiceChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.neuPanel3);
            this.Controls.Add(this.neuPanel2);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucInvoiceChange";
            this.Size = new System.Drawing.Size(604, 252);
            this.Load += new System.EventHandler(this.ucInvoiceChange_Load);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.neuPanel2.ResumeLayout(false);
            this.neuPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel3;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbInvoiceType;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtUsedNO;
        private FS.FrameWork.WinForms.Controls.NeuButton btnOK;
        private FS.FrameWork.WinForms.Controls.NeuButton btnExit;
    }
}
