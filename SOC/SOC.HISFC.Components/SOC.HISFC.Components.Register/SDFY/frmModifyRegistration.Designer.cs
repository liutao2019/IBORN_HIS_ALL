namespace FS.SOC.HISFC.Components.Register.SDFY
{
    partial class frmModifyRegistration
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
            this.panel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.panel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.txtAge = new FS.FrameWork.WinForms.Controls.NeuMaskedEdit();
            this.dtpBirthDay = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.lblBirthDay = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label9 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.groupBox3 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.label6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label8 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtRecipeNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtCardNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtSeeNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.groupBox2 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.cmbSex = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.txtPhone = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.dtBirthday1 = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.txtAdress = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.button1 = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.button2 = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.label2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.panel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.groupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.label7 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(472, 303);
            this.panel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panel1.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtAge);
            this.panel3.Controls.Add(this.dtpBirthDay);
            this.panel3.Controls.Add(this.lblBirthDay);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.groupBox3);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.txtRecipeNo);
            this.panel3.Controls.Add(this.txtCardNo);
            this.panel3.Controls.Add(this.txtSeeNo);
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Controls.Add(this.cmbSex);
            this.panel3.Controls.Add(this.txtPhone);
            this.panel3.Controls.Add(this.dtBirthday1);
            this.panel3.Controls.Add(this.txtAdress);
            this.panel3.Controls.Add(this.txtName);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.button2);
            this.panel3.Controls.Add(this.neuLabel1);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 46);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(472, 257);
            this.panel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panel3.TabIndex = 1;
            // 
            // txtAge
            // 
            this.txtAge.ErrorInvalid = false;
            this.txtAge.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAge.InputChar = '_';
            this.txtAge.InputMask = "999岁99月99天";
            this.txtAge.IsEnter2Tab = false;
            this.txtAge.Location = new System.Drawing.Point(331, 108);
            this.txtAge.MaxLength = 10;
            this.txtAge.Name = "txtAge";
            this.txtAge.Size = new System.Drawing.Size(114, 26);
            this.txtAge.StdInputMask = FS.FrameWork.WinForms.Controls.NeuMaskedEdit.InputMaskType.Custom;
            this.txtAge.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtAge.TabIndex = 26;
            // 
            // dtpBirthDay
            // 
            this.dtpBirthDay.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpBirthDay.IsEnter2Tab = false;
            this.dtpBirthDay.Location = new System.Drawing.Point(102, 108);
            this.dtpBirthDay.Name = "dtpBirthDay";
            this.dtpBirthDay.Size = new System.Drawing.Size(128, 26);
            this.dtpBirthDay.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpBirthDay.TabIndex = 25;
            // 
            // lblBirthDay
            // 
            this.lblBirthDay.AutoSize = true;
            this.lblBirthDay.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBirthDay.Location = new System.Drawing.Point(19, 113);
            this.lblBirthDay.Name = "lblBirthDay";
            this.lblBirthDay.Size = new System.Drawing.Size(80, 16);
            this.lblBirthDay.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblBirthDay.TabIndex = 24;
            this.lblBirthDay.Text = "出生日期:";
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("宋体", 12F);
            this.label9.Location = new System.Drawing.Point(245, 250);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 23);
            this.label9.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label9.TabIndex = 23;
            this.label9.Text = "看诊序号:";
            this.label9.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Font = new System.Drawing.Font("宋体", 2F);
            this.groupBox3.Location = new System.Drawing.Point(12, 45);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(448, 3);
            this.groupBox3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("宋体", 12F);
            this.label6.Location = new System.Drawing.Point(19, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 23);
            this.label6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label6.TabIndex = 18;
            this.label6.Text = "病 历 号:";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("宋体", 12F);
            this.label8.Location = new System.Drawing.Point(24, 250);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 23);
            this.label8.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label8.TabIndex = 21;
            this.label8.Text = "处 方 号:";
            this.label8.Visible = false;
            // 
            // txtRecipeNo
            // 
            this.txtRecipeNo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtRecipeNo.Font = new System.Drawing.Font("宋体", 12F);
            this.txtRecipeNo.IsEnter2Tab = false;
            this.txtRecipeNo.Location = new System.Drawing.Point(107, 245);
            this.txtRecipeNo.Name = "txtRecipeNo";
            this.txtRecipeNo.ReadOnly = true;
            this.txtRecipeNo.Size = new System.Drawing.Size(112, 26);
            this.txtRecipeNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtRecipeNo.TabIndex = 20;
            this.txtRecipeNo.Visible = false;
            // 
            // txtCardNo
            // 
            this.txtCardNo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtCardNo.Font = new System.Drawing.Font("宋体", 12F);
            this.txtCardNo.IsEnter2Tab = false;
            this.txtCardNo.Location = new System.Drawing.Point(102, 11);
            this.txtCardNo.Name = "txtCardNo";
            this.txtCardNo.Size = new System.Drawing.Size(112, 26);
            this.txtCardNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCardNo.TabIndex = 0;
            // 
            // txtSeeNo
            // 
            this.txtSeeNo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtSeeNo.Font = new System.Drawing.Font("宋体", 12F);
            this.txtSeeNo.IsEnter2Tab = false;
            this.txtSeeNo.Location = new System.Drawing.Point(328, 245);
            this.txtSeeNo.Name = "txtSeeNo";
            this.txtSeeNo.ReadOnly = true;
            this.txtSeeNo.Size = new System.Drawing.Size(122, 26);
            this.txtSeeNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtSeeNo.TabIndex = 22;
            this.txtSeeNo.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Font = new System.Drawing.Font("宋体", 2F);
            this.groupBox2.Location = new System.Drawing.Point(10, 213);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(448, 3);
            this.groupBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            // 
            // cmbSex
            // 
            this.cmbSex.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbSex.Font = new System.Drawing.Font("宋体", 12F);
            this.cmbSex.IsEnter2Tab = false;
            this.cmbSex.IsFlat = false;
            this.cmbSex.IsLike = true;
            this.cmbSex.IsListOnly = false;
            this.cmbSex.IsPopForm = true;
            this.cmbSex.IsShowCustomerList = false;
            this.cmbSex.IsShowID = false;
            this.cmbSex.Location = new System.Drawing.Point(323, 61);
            this.cmbSex.Name = "cmbSex";
            this.cmbSex.PopForm = null;
            this.cmbSex.ShowCustomerList = false;
            this.cmbSex.ShowID = false;
            this.cmbSex.Size = new System.Drawing.Size(122, 24);
            this.cmbSex.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbSex.TabIndex = 2;
            this.cmbSex.Tag = "";
            this.cmbSex.ToolBarUse = false;
            // 
            // txtPhone
            // 
            this.txtPhone.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtPhone.Font = new System.Drawing.Font("宋体", 12F);
            this.txtPhone.IsEnter2Tab = false;
            this.txtPhone.Location = new System.Drawing.Point(333, 159);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(112, 26);
            this.txtPhone.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtPhone.TabIndex = 4;
            // 
            // dtBirthday1
            // 
            this.dtBirthday1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dtBirthday1.CalendarFont = new System.Drawing.Font("宋体", 9F);
            this.dtBirthday1.CustomFormat = "yyyy-MM-dd";
            this.dtBirthday1.Font = new System.Drawing.Font("宋体", 12F);
            this.dtBirthday1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtBirthday1.IsEnter2Tab = false;
            this.dtBirthday1.Location = new System.Drawing.Point(102, 219);
            this.dtBirthday1.Name = "dtBirthday1";
            this.dtBirthday1.ShowUpDown = true;
            this.dtBirthday1.Size = new System.Drawing.Size(112, 26);
            this.dtBirthday1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtBirthday1.TabIndex = 3;
            this.dtBirthday1.Visible = false;
            // 
            // txtAdress
            // 
            this.txtAdress.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtAdress.Font = new System.Drawing.Font("宋体", 12F);
            this.txtAdress.IsEnter2Tab = false;
            this.txtAdress.Location = new System.Drawing.Point(102, 158);
            this.txtAdress.Name = "txtAdress";
            this.txtAdress.Size = new System.Drawing.Size(145, 26);
            this.txtAdress.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtAdress.TabIndex = 5;
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtName.Font = new System.Drawing.Font("宋体", 12F);
            this.txtName.IsEnter2Tab = false;
            this.txtName.Location = new System.Drawing.Point(102, 61);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(112, 26);
            this.txtName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtName.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("宋体", 12F);
            this.label5.Location = new System.Drawing.Point(16, 162);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 23);
            this.label5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label5.TabIndex = 15;
            this.label5.Text = "联系地址:";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("宋体", 12F);
            this.label4.Location = new System.Drawing.Point(249, 163);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 23);
            this.label4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label4.TabIndex = 14;
            this.label4.Text = "联系电话:";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("宋体", 12F);
            this.label3.Location = new System.Drawing.Point(19, 223);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 23);
            this.label3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label3.TabIndex = 13;
            this.label3.Text = "出生日期:";
            this.label3.Visible = false;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(263, 222);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.button1.TabIndex = 6;
            this.button1.Text = "确定(&O)";
            this.button1.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(368, 222);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.button2.TabIndex = 7;
            this.button2.Text = "退出(&X)";
            this.button2.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("宋体", 12F);
            this.label2.Location = new System.Drawing.Point(240, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 23);
            this.label2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label2.TabIndex = 8;
            this.label2.Text = "性    别:";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.Location = new System.Drawing.Point(18, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 23);
            this.label1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label1.TabIndex = 6;
            this.label1.Text = "姓    名:";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Window;
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(472, 46);
            this.panel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panel2.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Font = new System.Drawing.Font("宋体", 2F);
            this.groupBox1.Location = new System.Drawing.Point(0, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(472, 3);
            this.groupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(16, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(430, 27);
            this.label7.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label7.TabIndex = 0;
            this.label7.Text = "    处方单重打可调整患者挂号信息,然后确定保存。系统自动将前一张处方单作废,并重新打印一张新的处方单";
            // 
            // neuLabel1
            // 
            this.neuLabel1.Font = new System.Drawing.Font("宋体", 12F);
            this.neuLabel1.Location = new System.Drawing.Point(242, 110);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(85, 23);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 8;
            this.neuLabel1.Text = "年   龄:";
            // 
            // frmModifyRegistration
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(472, 328);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmModifyRegistration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "处方单重打";
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel panel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel panel2;
        private FS.FrameWork.WinForms.Controls.NeuPanel panel3;
        private FS.FrameWork.WinForms.Controls.NeuLabel label1;
        private FS.FrameWork.WinForms.Controls.NeuLabel label2;
        private FS.FrameWork.WinForms.Controls.NeuButton button2;
        private FS.FrameWork.WinForms.Controls.NeuButton button1;
        private FS.FrameWork.WinForms.Controls.NeuLabel label3;
        private FS.FrameWork.WinForms.Controls.NeuLabel label4;
        private FS.FrameWork.WinForms.Controls.NeuLabel label5;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbSex;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtAdress;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtPhone;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtName;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtBirthday1;
        private FS.FrameWork.WinForms.Controls.NeuLabel label7;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox groupBox1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox groupBox2;
        private FS.FrameWork.WinForms.Controls.NeuLabel label6;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox groupBox3;
        private FS.FrameWork.WinForms.Controls.NeuLabel label8;
        private FS.FrameWork.WinForms.Controls.NeuLabel label9;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtRecipeNo;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtSeeNo;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtCardNo;
        private FS.FrameWork.WinForms.Controls.NeuMaskedEdit txtAge;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpBirthDay;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblBirthDay;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
    }
}