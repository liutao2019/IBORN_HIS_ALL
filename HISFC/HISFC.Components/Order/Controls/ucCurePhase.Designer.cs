namespace FS.HISFC.Components.Order.Controls
{
    partial class ucCurePhase
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
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.ckbVaild = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.lblName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtRemark = new FS.FrameWork.WinForms.Controls.NeuRichTextBox();
            this.dtEnd = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.dtStart = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.cmbDoct = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbCurePhase = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.fpCurePhase = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpCurePhase_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel1.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpCurePhase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpCurePhase_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.ckbVaild);
            this.neuPanel1.Controls.Add(this.lblName);
            this.neuPanel1.Controls.Add(this.neuLabel6);
            this.neuPanel1.Controls.Add(this.neuLabel5);
            this.neuPanel1.Controls.Add(this.neuLabel4);
            this.neuPanel1.Controls.Add(this.neuLabel3);
            this.neuPanel1.Controls.Add(this.neuLabel2);
            this.neuPanel1.Controls.Add(this.neuLabel1);
            this.neuPanel1.Controls.Add(this.txtRemark);
            this.neuPanel1.Controls.Add(this.dtEnd);
            this.neuPanel1.Controls.Add(this.dtStart);
            this.neuPanel1.Controls.Add(this.cmbDoct);
            this.neuPanel1.Controls.Add(this.cmbCurePhase);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(695, 172);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // ckbVaild
            // 
            this.ckbVaild.AutoSize = true;
            this.ckbVaild.Checked = true;
            this.ckbVaild.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbVaild.Location = new System.Drawing.Point(539, 69);
            this.ckbVaild.Name = "ckbVaild";
            this.ckbVaild.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ckbVaild.Size = new System.Drawing.Size(72, 16);
            this.ckbVaild.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ckbVaild.TabIndex = 12;
            this.ckbVaild.Text = "��Ч���";
            this.ckbVaild.UseVisualStyleBackColor = true;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(119, 30);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(0, 12);
            this.lblName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblName.TabIndex = 11;
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.Location = new System.Drawing.Point(48, 30);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(65, 12);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 10;
            this.neuLabel6.Text = "����������";
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Location = new System.Drawing.Point(60, 108);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(53, 12);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 9;
            this.neuLabel5.Text = "��  ע��";
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point(456, 30);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(65, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 8;
            this.neuLabel4.Text = "����ҽ����";
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(186, 30);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(65, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 7;
            this.neuLabel3.Text = "���ƽ׶Σ�";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(282, 70);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(89, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 6;
            this.neuLabel2.Text = "�׶ν���ʱ�䣺";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(24, 70);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(89, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 5;
            this.neuLabel1.Text = "�׶ο�ʼʱ�䣺";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(119, 108);
            this.txtRemark.MaxLength = 200;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(492, 48);
            this.txtRemark.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtRemark.TabIndex = 4;
            this.txtRemark.Text = "";
            // 
            // dtEnd
            // 
            this.dtEnd.CustomFormat = "yyyy��M��d�� HH:mm";
            this.dtEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEnd.Location = new System.Drawing.Point(377, 66);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(156, 21);
            this.dtEnd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtEnd.TabIndex = 3;
            // 
            // dtStart
            // 
            this.dtStart.CustomFormat = "yyyy��M��d�� HH:mm";
            this.dtStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtStart.Location = new System.Drawing.Point(119, 66);
            this.dtStart.Name = "dtStart";
            this.dtStart.Size = new System.Drawing.Size(146, 21);
            this.dtStart.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtStart.TabIndex = 2;
            // 
            // cmbDoct
            // 
            this.cmbDoct.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbDoct.FormattingEnabled = true;
            this.cmbDoct.IsFlat = true;
            this.cmbDoct.IsLike = true;
            this.cmbDoct.Location = new System.Drawing.Point(527, 27);
            this.cmbDoct.Name = "cmbDoct";
            this.cmbDoct.PopForm = null;
            this.cmbDoct.ShowCustomerList = false;
            this.cmbDoct.ShowID = false;
            this.cmbDoct.Size = new System.Drawing.Size(84, 20);
            this.cmbDoct.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbDoct.TabIndex = 1;
            this.cmbDoct.Tag = "";
            this.cmbDoct.ToolBarUse = false;
            this.cmbDoct.SelectedIndexChanged += new System.EventHandler(this.cmbDoct_SelectedIndexChanged);
            // 
            // cmbCurePhase
            // 
            this.cmbCurePhase.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbCurePhase.FormattingEnabled = true;
            this.cmbCurePhase.IsFlat = true;
            this.cmbCurePhase.IsLike = true;
            this.cmbCurePhase.Location = new System.Drawing.Point(255, 27);
            this.cmbCurePhase.Name = "cmbCurePhase";
            this.cmbCurePhase.PopForm = null;
            this.cmbCurePhase.ShowCustomerList = false;
            this.cmbCurePhase.ShowID = false;
            this.cmbCurePhase.Size = new System.Drawing.Size(195, 20);
            this.cmbCurePhase.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbCurePhase.TabIndex = 0;
            this.cmbCurePhase.Tag = "";
            this.cmbCurePhase.ToolBarUse = false;
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.fpCurePhase);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel2.Location = new System.Drawing.Point(0, 172);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(695, 336);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 1;
            // 
            // fpCurePhase
            // 
            this.fpCurePhase.About = "2.5.2007.2005";
            this.fpCurePhase.AccessibleDescription = "";
            this.fpCurePhase.BackColor = System.Drawing.Color.White;
            this.fpCurePhase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpCurePhase.FileName = "";
            this.fpCurePhase.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpCurePhase.IsAutoSaveGridStatus = false;
            this.fpCurePhase.IsCanCustomConfigColumn = false;
            this.fpCurePhase.Location = new System.Drawing.Point(0, 0);
            this.fpCurePhase.Name = "fpCurePhase";
            this.fpCurePhase.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpCurePhase_Sheet1});
            this.fpCurePhase.Size = new System.Drawing.Size(695, 336);
            this.fpCurePhase.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpCurePhase.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpCurePhase.TextTipAppearance = tipAppearance1;
            this.fpCurePhase.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpCurePhase.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpCurePhase_CellClick);
            // 
            // fpCurePhase_Sheet1
            // 
            this.fpCurePhase_Sheet1.Reset();
            this.fpCurePhase_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpCurePhase_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpCurePhase_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.fpCurePhase_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ucCurePhase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.neuPanel2);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucCurePhase";
            this.Size = new System.Drawing.Size(695, 508);
            this.Load += new System.EventHandler(this.ucCurePhase_Load);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.neuPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpCurePhase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpCurePhase_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbCurePhase;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtStart;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDoct;
        private FS.FrameWork.WinForms.Controls.NeuRichTextBox txtRemark;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtEnd;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpCurePhase;
        private FarPoint.Win.Spread.SheetView fpCurePhase_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblName;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ckbVaild;
        
    }
}
