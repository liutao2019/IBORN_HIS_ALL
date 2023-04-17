namespace FS.HISFC.Components.OutpatientFee.Controls
{
    /// <summary>
    /// ucShowPatients<br></br>
    /// [功能描述: 输入的卡号多于一个患者选择患者UC]<br></br>
    /// [创 建 者: 王宇]<br></br>
    /// [创建时间: 2006-2-28]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    partial class ucPayingAgentMess
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
            this.txtTip = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel14 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtCardNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.ckSelect = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.cmbAccountType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel13 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtName = new System.Windows.Forms.Label();
            this.txtSex = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.Label();
            this.txtBaseVacancy = new System.Windows.Forms.Label();
            this.txtHome = new System.Windows.Forms.Label();
            this.txtDonateVacancy = new System.Windows.Forms.Label();
            this.txtAge = new System.Windows.Forms.Label();
            this.txtIDNO = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtTip
            // 
            this.txtTip.AutoSize = true;
            this.txtTip.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTip.ForeColor = System.Drawing.Color.Blue;
            this.txtTip.Location = new System.Drawing.Point(14, 36);
            this.txtTip.Name = "txtTip";
            this.txtTip.Size = new System.Drawing.Size(96, 15);
            this.txtTip.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtTip.TabIndex = 65;
            this.txtTip.Text = "代付人信息:";
            // 
            // neuLabel14
            // 
            this.neuLabel14.AutoSize = true;
            this.neuLabel14.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel14.Location = new System.Drawing.Point(261, 9);
            this.neuLabel14.Name = "neuLabel14";
            this.neuLabel14.Size = new System.Drawing.Size(128, 15);
            this.neuLabel14.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel14.TabIndex = 64;
            this.neuLabel14.Text = "代付人就诊卡号:";
            // 
            // txtCardNo
            // 
            this.txtCardNo.Enabled = false;
            this.txtCardNo.IsEnter2Tab = false;
            this.txtCardNo.Location = new System.Drawing.Point(395, 7);
            this.txtCardNo.Name = "txtCardNo";
            this.txtCardNo.Size = new System.Drawing.Size(114, 21);
            this.txtCardNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCardNo.TabIndex = 63;
            // 
            // ckSelect
            // 
            this.ckSelect.AutoSize = true;
            this.ckSelect.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckSelect.Location = new System.Drawing.Point(531, 8);
            this.ckSelect.Name = "ckSelect";
            this.ckSelect.Size = new System.Drawing.Size(76, 16);
            this.ckSelect.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ckSelect.TabIndex = 62;
            this.ckSelect.Text = "是否代付";
            this.ckSelect.UseVisualStyleBackColor = true;
            // 
            // cmbAccountType
            // 
            this.cmbAccountType.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbAccountType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbAccountType.FormattingEnabled = true;
            this.cmbAccountType.IsEnter2Tab = false;
            this.cmbAccountType.IsFlat = false;
            this.cmbAccountType.IsLike = true;
            this.cmbAccountType.IsListOnly = false;
            this.cmbAccountType.IsPopForm = true;
            this.cmbAccountType.IsShowCustomerList = false;
            this.cmbAccountType.IsShowID = false;
            this.cmbAccountType.IsShowIDAndName = false;
            this.cmbAccountType.Location = new System.Drawing.Point(95, 8);
            this.cmbAccountType.Name = "cmbAccountType";
            this.cmbAccountType.ShowCustomerList = false;
            this.cmbAccountType.ShowID = false;
            this.cmbAccountType.Size = new System.Drawing.Size(148, 20);
            this.cmbAccountType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbAccountType.TabIndex = 60;
            this.cmbAccountType.Tag = "";
            this.cmbAccountType.ToolBarUse = false;
            this.cmbAccountType.SelectedIndexChanged += new System.EventHandler(this.cmbAccountType_SelectedIndexChanged);
            // 
            // neuLabel13
            // 
            this.neuLabel13.AutoSize = true;
            this.neuLabel13.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel13.Location = new System.Drawing.Point(15, 9);
            this.neuLabel13.Name = "neuLabel13";
            this.neuLabel13.Size = new System.Drawing.Size(87, 15);
            this.neuLabel13.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel13.TabIndex = 61;
            this.neuLabel13.Text = "帐户类型：";
            // 
            // txtName
            // 
            this.txtName.AutoSize = true;
            this.txtName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtName.ForeColor = System.Drawing.Color.Blue;
            this.txtName.Location = new System.Drawing.Point(110, 37);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(49, 14);
            this.txtName.TabIndex = 66;
            this.txtName.Text = "刘德华";
            // 
            // txtSex
            // 
            this.txtSex.AutoSize = true;
            this.txtSex.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSex.ForeColor = System.Drawing.Color.Blue;
            this.txtSex.Location = new System.Drawing.Point(239, 37);
            this.txtSex.Name = "txtSex";
            this.txtSex.Size = new System.Drawing.Size(21, 14);
            this.txtSex.TabIndex = 67;
            this.txtSex.Text = "男";
            // 
            // txtPhone
            // 
            this.txtPhone.AutoSize = true;
            this.txtPhone.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPhone.ForeColor = System.Drawing.Color.Blue;
            this.txtPhone.Location = new System.Drawing.Point(180, 60);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(154, 14);
            this.txtPhone.TabIndex = 68;
            this.txtPhone.Text = "联系电话：12345678901";
            // 
            // txtBaseVacancy
            // 
            this.txtBaseVacancy.AutoSize = true;
            this.txtBaseVacancy.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBaseVacancy.ForeColor = System.Drawing.Color.Blue;
            this.txtBaseVacancy.Location = new System.Drawing.Point(282, 37);
            this.txtBaseVacancy.Name = "txtBaseVacancy";
            this.txtBaseVacancy.Size = new System.Drawing.Size(168, 14);
            this.txtBaseVacancy.TabIndex = 69;
            this.txtBaseVacancy.Text = "基本账户余额：100000.00";
            // 
            // txtHome
            // 
            this.txtHome.AutoSize = true;
            this.txtHome.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtHome.ForeColor = System.Drawing.Color.Blue;
            this.txtHome.Location = new System.Drawing.Point(337, 60);
            this.txtHome.Name = "txtHome";
            this.txtHome.Size = new System.Drawing.Size(329, 14);
            this.txtHome.TabIndex = 70;
            this.txtHome.Text = "现住址：广东省广州市天河区广州爱博恩妇产科医院";
            // 
            // txtDonateVacancy
            // 
            this.txtDonateVacancy.AutoSize = true;
            this.txtDonateVacancy.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDonateVacancy.ForeColor = System.Drawing.Color.Blue;
            this.txtDonateVacancy.Location = new System.Drawing.Point(468, 37);
            this.txtDonateVacancy.Name = "txtDonateVacancy";
            this.txtDonateVacancy.Size = new System.Drawing.Size(154, 14);
            this.txtDonateVacancy.TabIndex = 71;
            this.txtDonateVacancy.Text = "赠送账户余额：1000.00";
            // 
            // txtAge
            // 
            this.txtAge.AutoSize = true;
            this.txtAge.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAge.ForeColor = System.Drawing.Color.Blue;
            this.txtAge.Location = new System.Drawing.Point(174, 37);
            this.txtAge.Name = "txtAge";
            this.txtAge.Size = new System.Drawing.Size(35, 14);
            this.txtAge.TabIndex = 72;
            this.txtAge.Text = "50岁";
            // 
            // txtIDNO
            // 
            this.txtIDNO.AutoSize = true;
            this.txtIDNO.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtIDNO.ForeColor = System.Drawing.Color.Blue;
            this.txtIDNO.Location = new System.Drawing.Point(14, 60);
            this.txtIDNO.Name = "txtIDNO";
            this.txtIDNO.Size = new System.Drawing.Size(161, 14);
            this.txtIDNO.TabIndex = 73;
            this.txtIDNO.Text = "证件号：44090999933445";
            // 
            // ucPayingAgentMess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.txtIDNO);
            this.Controls.Add(this.txtAge);
            this.Controls.Add(this.txtDonateVacancy);
            this.Controls.Add(this.txtHome);
            this.Controls.Add(this.txtBaseVacancy);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.txtSex);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtTip);
            this.Controls.Add(this.neuLabel14);
            this.Controls.Add(this.txtCardNo);
            this.Controls.Add(this.ckSelect);
            this.Controls.Add(this.cmbAccountType);
            this.Controls.Add(this.neuLabel13);
            this.Name = "ucPayingAgentMess";
            this.Size = new System.Drawing.Size(682, 81);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected FS.FrameWork.WinForms.Controls.NeuLabel txtTip;
        protected FS.FrameWork.WinForms.Controls.NeuLabel neuLabel14;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtCardNo;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ckSelect;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbAccountType;
        protected FS.FrameWork.WinForms.Controls.NeuLabel neuLabel13;
        private System.Windows.Forms.Label txtName;
        private System.Windows.Forms.Label txtSex;
        private System.Windows.Forms.Label txtPhone;
        private System.Windows.Forms.Label txtBaseVacancy;
        private System.Windows.Forms.Label txtHome;
        private System.Windows.Forms.Label txtDonateVacancy;
        private System.Windows.Forms.Label txtAge;
        private System.Windows.Forms.Label txtIDNO;

    }
}
