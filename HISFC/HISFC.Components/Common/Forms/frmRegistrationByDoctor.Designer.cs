namespace FS.HISFC.Components.Common.Forms
{
    partial class frmRegistrationByDoctor
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
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.btShuaKa = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.txtAddress = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtPhone = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel9 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel8 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblTip = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtIDCard = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel7 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbRegLevl = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.btAutoCardNo = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnCaecel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnOK = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.cmbPact = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dtPickerBirth = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbSex = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtCardNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbClass1Desease = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel10 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbClass2Desease = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel11 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.cmbClass2Desease);
            this.neuPanel1.Controls.Add(this.neuLabel11);
            this.neuPanel1.Controls.Add(this.cmbClass1Desease);
            this.neuPanel1.Controls.Add(this.neuLabel10);
            this.neuPanel1.Controls.Add(this.btShuaKa);
            this.neuPanel1.Controls.Add(this.txtAddress);
            this.neuPanel1.Controls.Add(this.txtPhone);
            this.neuPanel1.Controls.Add(this.neuLabel9);
            this.neuPanel1.Controls.Add(this.neuLabel8);
            this.neuPanel1.Controls.Add(this.lblTip);
            this.neuPanel1.Controls.Add(this.txtIDCard);
            this.neuPanel1.Controls.Add(this.neuLabel7);
            this.neuPanel1.Controls.Add(this.cmbRegLevl);
            this.neuPanel1.Controls.Add(this.neuLabel6);
            this.neuPanel1.Controls.Add(this.btAutoCardNo);
            this.neuPanel1.Controls.Add(this.btnCaecel);
            this.neuPanel1.Controls.Add(this.btnOK);
            this.neuPanel1.Controls.Add(this.cmbPact);
            this.neuPanel1.Controls.Add(this.neuLabel5);
            this.neuPanel1.Controls.Add(this.dtPickerBirth);
            this.neuPanel1.Controls.Add(this.neuLabel4);
            this.neuPanel1.Controls.Add(this.cmbSex);
            this.neuPanel1.Controls.Add(this.neuLabel3);
            this.neuPanel1.Controls.Add(this.txtName);
            this.neuPanel1.Controls.Add(this.neuLabel2);
            this.neuPanel1.Controls.Add(this.txtCardNo);
            this.neuPanel1.Controls.Add(this.neuLabel1);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(448, 256);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 5;
            // 
            // btShuaKa
            // 
            this.btShuaKa.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btShuaKa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btShuaKa.Location = new System.Drawing.Point(143, 223);
            this.btShuaKa.Name = "btShuaKa";
            this.btShuaKa.Size = new System.Drawing.Size(75, 23);
            this.btShuaKa.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btShuaKa.TabIndex = 23;
            this.btShuaKa.Text = "ˢ��";
            this.btShuaKa.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btShuaKa.UseVisualStyleBackColor = true;
            this.btShuaKa.Click += new System.EventHandler(this.btShuaKa_Click);
            // 
            // txtAddress
            // 
            this.txtAddress.Enabled = false;
            this.txtAddress.IsEnter2Tab = false;
            this.txtAddress.Location = new System.Drawing.Point(311, 92);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(117, 21);
            this.txtAddress.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtAddress.TabIndex = 22;
            // 
            // txtPhone
            // 
            this.txtPhone.Enabled = false;
            this.txtPhone.IsEnter2Tab = false;
            this.txtPhone.Location = new System.Drawing.Point(76, 92);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(126, 21);
            this.txtPhone.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtPhone.TabIndex = 21;
            // 
            // neuLabel9
            // 
            this.neuLabel9.AutoSize = true;
            this.neuLabel9.Location = new System.Drawing.Point(249, 98);
            this.neuLabel9.Name = "neuLabel9";
            this.neuLabel9.Size = new System.Drawing.Size(65, 12);
            this.neuLabel9.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel9.TabIndex = 20;
            this.neuLabel9.Text = "��ͥ��ַ��";
            // 
            // neuLabel8
            // 
            this.neuLabel8.AutoSize = true;
            this.neuLabel8.Location = new System.Drawing.Point(11, 98);
            this.neuLabel8.Name = "neuLabel8";
            this.neuLabel8.Size = new System.Drawing.Size(65, 12);
            this.neuLabel8.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel8.TabIndex = 19;
            this.neuLabel8.Text = "��ϵ�绰��";
            // 
            // lblTip
            // 
            this.lblTip.AutoSize = true;
            this.lblTip.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTip.ForeColor = System.Drawing.Color.Red;
            this.lblTip.Location = new System.Drawing.Point(57, 199);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(304, 12);
            this.lblTip.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblTip.TabIndex = 18;
            this.lblTip.Text = "��ʾ��Ϣ��ϵͳ���ñ�����ֻ�ܹҺ��ԷѺ�ͬ��λ��";
            // 
            // txtIDCard
            // 
            this.txtIDCard.Enabled = false;
            this.txtIDCard.IsEnter2Tab = false;
            this.txtIDCard.Location = new System.Drawing.Point(311, 56);
            this.txtIDCard.Name = "txtIDCard";
            this.txtIDCard.Size = new System.Drawing.Size(117, 21);
            this.txtIDCard.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtIDCard.TabIndex = 17;
            // 
            // neuLabel7
            // 
            this.neuLabel7.AutoSize = true;
            this.neuLabel7.Location = new System.Drawing.Point(249, 61);
            this.neuLabel7.Name = "neuLabel7";
            this.neuLabel7.Size = new System.Drawing.Size(65, 12);
            this.neuLabel7.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel7.TabIndex = 16;
            this.neuLabel7.Text = "���֤�ţ�";
            // 
            // cmbRegLevl
            // 
            this.cmbRegLevl.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbRegLevl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbRegLevl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRegLevl.FormattingEnabled = true;
            this.cmbRegLevl.IsEnter2Tab = false;
            this.cmbRegLevl.IsFlat = false;
            this.cmbRegLevl.IsLike = true;
            this.cmbRegLevl.IsListOnly = false;
            this.cmbRegLevl.IsPopForm = true;
            this.cmbRegLevl.IsShowCustomerList = false;
            this.cmbRegLevl.IsShowID = false;
            this.cmbRegLevl.IsShowIDAndName = false;
            this.cmbRegLevl.Location = new System.Drawing.Point(76, 128);
            this.cmbRegLevl.Name = "cmbRegLevl";
            this.cmbRegLevl.ShowCustomerList = false;
            this.cmbRegLevl.ShowID = false;
            this.cmbRegLevl.Size = new System.Drawing.Size(126, 20);
            this.cmbRegLevl.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbRegLevl.TabIndex = 15;
            this.cmbRegLevl.Tag = "";
            this.cmbRegLevl.ToolBarUse = false;
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.Location = new System.Drawing.Point(11, 135);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(65, 12);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 14;
            this.neuLabel6.Text = "�Һż���";
            // 
            // btAutoCardNo
            // 
            this.btAutoCardNo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btAutoCardNo.Location = new System.Drawing.Point(9, 223);
            this.btAutoCardNo.Name = "btAutoCardNo";
            this.btAutoCardNo.Size = new System.Drawing.Size(85, 23);
            this.btAutoCardNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btAutoCardNo.TabIndex = 13;
            this.btAutoCardNo.Text = "����Һ�";
            this.btAutoCardNo.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btAutoCardNo.UseVisualStyleBackColor = true;
            this.btAutoCardNo.Click += new System.EventHandler(this.btAutoCardNo_Click);
            // 
            // btnCaecel
            // 
            this.btnCaecel.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCaecel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCaecel.Location = new System.Drawing.Point(353, 223);
            this.btnCaecel.Name = "btnCaecel";
            this.btnCaecel.Size = new System.Drawing.Size(75, 23);
            this.btnCaecel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnCaecel.TabIndex = 12;
            this.btnCaecel.Text = "ȡ��";
            this.btnCaecel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnCaecel.UseVisualStyleBackColor = true;
            this.btnCaecel.Click += new System.EventHandler(this.btnCaecel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(256, 223);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "ȷ��";
            this.btnOK.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // cmbPact
            // 
            this.cmbPact.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbPact.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbPact.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPact.FormattingEnabled = true;
            this.cmbPact.IsEnter2Tab = false;
            this.cmbPact.IsFlat = false;
            this.cmbPact.IsLike = true;
            this.cmbPact.IsListOnly = false;
            this.cmbPact.IsPopForm = true;
            this.cmbPact.IsShowCustomerList = false;
            this.cmbPact.IsShowID = false;
            this.cmbPact.IsShowIDAndName = false;
            this.cmbPact.Location = new System.Drawing.Point(311, 128);
            this.cmbPact.Name = "cmbPact";
            this.cmbPact.ShowCustomerList = false;
            this.cmbPact.ShowID = false;
            this.cmbPact.Size = new System.Drawing.Size(117, 20);
            this.cmbPact.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbPact.TabIndex = 10;
            this.cmbPact.Tag = "";
            this.cmbPact.ToolBarUse = false;
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Location = new System.Drawing.Point(249, 135);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(65, 12);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 9;
            this.neuLabel5.Text = "��ͬ��λ��";
            // 
            // dtPickerBirth
            // 
            this.dtPickerBirth.IsEnter2Tab = false;
            this.dtPickerBirth.Location = new System.Drawing.Point(76, 56);
            this.dtPickerBirth.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtPickerBirth.Name = "dtPickerBirth";
            this.dtPickerBirth.Size = new System.Drawing.Size(126, 21);
            this.dtPickerBirth.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtPickerBirth.TabIndex = 8;
            this.dtPickerBirth.Value = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point(11, 61);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(65, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 7;
            this.neuLabel4.Text = "�������ڣ�";
            // 
            // cmbSex
            // 
            this.cmbSex.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbSex.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbSex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSex.FormattingEnabled = true;
            this.cmbSex.IsEnter2Tab = false;
            this.cmbSex.IsFlat = false;
            this.cmbSex.IsLike = true;
            this.cmbSex.IsListOnly = false;
            this.cmbSex.IsPopForm = true;
            this.cmbSex.IsShowCustomerList = false;
            this.cmbSex.IsShowID = false;
            this.cmbSex.IsShowIDAndName = false;
            this.cmbSex.Location = new System.Drawing.Point(379, 21);
            this.cmbSex.Name = "cmbSex";
            this.cmbSex.ShowCustomerList = false;
            this.cmbSex.ShowID = false;
            this.cmbSex.Size = new System.Drawing.Size(49, 20);
            this.cmbSex.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbSex.TabIndex = 6;
            this.cmbSex.Tag = "";
            this.cmbSex.ToolBarUse = false;
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(339, 24);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(41, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 5;
            this.neuLabel3.Text = "�Ա�";
            // 
            // txtName
            // 
            this.txtName.Enabled = false;
            this.txtName.IsEnter2Tab = false;
            this.txtName.Location = new System.Drawing.Point(245, 20);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(86, 21);
            this.txtName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtName.TabIndex = 4;
            this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(208, 24);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(41, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 3;
            this.neuLabel2.Text = "������";
            // 
            // txtCardNo
            // 
            this.txtCardNo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtCardNo.IsEnter2Tab = false;
            this.txtCardNo.Location = new System.Drawing.Point(92, 20);
            this.txtCardNo.Name = "txtCardNo";
            this.txtCardNo.Size = new System.Drawing.Size(98, 21);
            this.txtCardNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCardNo.TabIndex = 2;
            this.txtCardNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCardNo_KeyDown);
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel1.Location = new System.Drawing.Point(7, 24);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(90, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 1;
            this.neuLabel1.Text = "�����/������";
            // 
            // cmbClass1Desease
            // 
            this.cmbClass1Desease.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbClass1Desease.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbClass1Desease.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClass1Desease.FormattingEnabled = true;
            this.cmbClass1Desease.IsEnter2Tab = false;
            this.cmbClass1Desease.IsFlat = false;
            this.cmbClass1Desease.IsLike = true;
            this.cmbClass1Desease.IsListOnly = false;
            this.cmbClass1Desease.IsPopForm = true;
            this.cmbClass1Desease.IsShowCustomerList = false;
            this.cmbClass1Desease.IsShowID = false;
            this.cmbClass1Desease.IsShowIDAndName = false;
            this.cmbClass1Desease.Location = new System.Drawing.Point(76, 164);
            this.cmbClass1Desease.Name = "cmbClass1Desease";
            this.cmbClass1Desease.ShowCustomerList = false;
            this.cmbClass1Desease.ShowID = false;
            this.cmbClass1Desease.Size = new System.Drawing.Size(126, 20);
            this.cmbClass1Desease.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbClass1Desease.TabIndex = 25;
            this.cmbClass1Desease.Tag = "";
            this.cmbClass1Desease.ToolBarUse = false;
            // 
            // neuLabel10
            // 
            this.neuLabel10.AutoSize = true;
            this.neuLabel10.Location = new System.Drawing.Point(11, 171);
            this.neuLabel10.Name = "neuLabel10";
            this.neuLabel10.Size = new System.Drawing.Size(65, 12);
            this.neuLabel10.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel10.TabIndex = 24;
            this.neuLabel10.Text = "һ�����֣�";
            // 
            // cmbClass2Desease
            // 
            this.cmbClass2Desease.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbClass2Desease.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbClass2Desease.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClass2Desease.FormattingEnabled = true;
            this.cmbClass2Desease.IsEnter2Tab = false;
            this.cmbClass2Desease.IsFlat = false;
            this.cmbClass2Desease.IsLike = true;
            this.cmbClass2Desease.IsListOnly = false;
            this.cmbClass2Desease.IsPopForm = true;
            this.cmbClass2Desease.IsShowCustomerList = false;
            this.cmbClass2Desease.IsShowID = false;
            this.cmbClass2Desease.IsShowIDAndName = false;
            this.cmbClass2Desease.Location = new System.Drawing.Point(311, 164);
            this.cmbClass2Desease.Name = "cmbClass2Desease";
            this.cmbClass2Desease.ShowCustomerList = false;
            this.cmbClass2Desease.ShowID = false;
            this.cmbClass2Desease.Size = new System.Drawing.Size(117, 20);
            this.cmbClass2Desease.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbClass2Desease.TabIndex = 27;
            this.cmbClass2Desease.Tag = "";
            this.cmbClass2Desease.ToolBarUse = false;
            // 
            // neuLabel11
            // 
            this.neuLabel11.AutoSize = true;
            this.neuLabel11.Location = new System.Drawing.Point(246, 171);
            this.neuLabel11.Name = "neuLabel11";
            this.neuLabel11.Size = new System.Drawing.Size(65, 12);
            this.neuLabel11.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel11.TabIndex = 26;
            this.neuLabel11.Text = "�������֣�";
            // 
            // frmRegistrationByDoctor
            // 
            this.ClientSize = new System.Drawing.Size(448, 256);
            this.ControlBox = false;
            this.Controls.Add(this.neuPanel1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRegistrationByDoctor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "�����뻼����Ϣ";
            this.Load += new System.EventHandler(this.frmRegistrationByDoctor_Load);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuButton btnOK;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbPact;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtPickerBirth;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbSex;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtName;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtCardNo;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuButton btnCaecel;
        private FS.FrameWork.WinForms.Controls.NeuButton btAutoCardNo;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbRegLevl;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtIDCard;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel7;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblTip;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtAddress;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtPhone;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel9;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel8;
        private FS.FrameWork.WinForms.Controls.NeuButton btShuaKa;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbClass2Desease;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel11;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbClass1Desease;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel10;
    }
}
