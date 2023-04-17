namespace FS.HISFC.Components.Operation
{
    partial class frmOperationRoom
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
            this.txtID = new FS.FrameWork.WinForms.Controls.NeuLabelTextBox();
            this.txtName = new FS.FrameWork.WinForms.Controls.NeuLabelTextBox();
            this.txtInputCode = new FS.FrameWork.WinForms.Controls.NeuLabelTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbValid = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.txtDept = new FS.FrameWork.WinForms.Controls.NeuLabelTextBox();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.btnOK = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnCancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.txtIpAddress = new FS.FrameWork.WinForms.Controls.NeuLabelTextBox();
            this.SuspendLayout();
            // 
            // txtID
            // 
            this.txtID.Label = "房间代码";
            this.txtID.LabelForeColor = System.Drawing.SystemColors.ControlText;
            this.txtID.Location = new System.Drawing.Point(25, 29);
            this.txtID.MaxLength = 32767;
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(247, 29);
            this.txtID.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtID.TabIndex = 0;
            this.txtID.TextBoxLeft = 82;
            this.txtID.TextChanged += new System.EventHandler(this.neuLabelTextBox1_TextChanged);
            // 
            // txtName
            // 
            this.txtName.Label = "房间名称";
            this.txtName.LabelForeColor = System.Drawing.Color.Red;
            this.txtName.Location = new System.Drawing.Point(25, 64);
            this.txtName.MaxLength = 15;
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = false;
            this.txtName.Size = new System.Drawing.Size(249, 29);
            this.txtName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtName.TabIndex = 1;
            this.txtName.TextBoxLeft = 82;
            // 
            // txtInputCode
            // 
            this.txtInputCode.Label = "助记码";
            this.txtInputCode.LabelForeColor = System.Drawing.SystemColors.ControlText;
            this.txtInputCode.Location = new System.Drawing.Point(25, 99);
            this.txtInputCode.MaxLength = 8;
            this.txtInputCode.Name = "txtInputCode";
            this.txtInputCode.ReadOnly = false;
            this.txtInputCode.Size = new System.Drawing.Size(251, 29);
            this.txtInputCode.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtInputCode.TabIndex = 2;
            this.txtInputCode.TextBoxLeft = 82;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(23, 151);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(53, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 1;
            this.neuLabel1.Text = "是否有效";
            // 
            // cmbValid
            // 
            this.cmbValid.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbValid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbValid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbValid.FormattingEnabled = true;
            this.cmbValid.IsEnter2Tab = false;
            this.cmbValid.IsFlat = false;
            this.cmbValid.IsLike = true;
            this.cmbValid.IsListOnly = false;
            this.cmbValid.IsPopForm = true;
            this.cmbValid.IsShowCustomerList = false;
            this.cmbValid.IsShowID = false;
            this.cmbValid.IsShowIDAndName = false;
            this.cmbValid.Items.AddRange(new object[] {
            "有效",
            "无效"});
            this.cmbValid.Location = new System.Drawing.Point(107, 141);
            this.cmbValid.Name = "cmbValid";
            this.cmbValid.ShowCustomerList = false;
            this.cmbValid.ShowID = false;
            this.cmbValid.Size = new System.Drawing.Size(157, 20);
            this.cmbValid.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbValid.TabIndex = 3;
            this.cmbValid.Tag = "";
            this.cmbValid.ToolBarUse = false;
            // 
            // txtDept
            // 
            this.txtDept.Label = "所属科室";
            this.txtDept.LabelForeColor = System.Drawing.SystemColors.ControlText;
            this.txtDept.Location = new System.Drawing.Point(25, 176);
            this.txtDept.MaxLength = 32767;
            this.txtDept.Name = "txtDept";
            this.txtDept.ReadOnly = true;
            this.txtDept.Size = new System.Drawing.Size(251, 29);
            this.txtDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtDept.TabIndex = 4;
            this.txtDept.TextBoxLeft = 82;
            // 
            // neuLabel2
            // 
            this.neuLabel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.neuLabel2.Location = new System.Drawing.Point(11, 257);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(452, 3);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 3;
            this.neuLabel2.Text = "neuLabel2";
            // 
            // btnOK
            // 
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(234, 281);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(93, 32);
            this.btnOK.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "确　定";
            this.btnOK.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(349, 281);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(93, 32);
            this.btnCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "取　消";
            this.btnCancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtIpAddress
            // 
            this.txtIpAddress.Label = "IP地址";
            this.txtIpAddress.LabelForeColor = System.Drawing.SystemColors.ControlText;
            this.txtIpAddress.Location = new System.Drawing.Point(24, 212);
            this.txtIpAddress.MaxLength = 50;
            this.txtIpAddress.Name = "txtIpAddress";
            this.txtIpAddress.ReadOnly = false;
            this.txtIpAddress.Size = new System.Drawing.Size(252, 29);
            this.txtIpAddress.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtIpAddress.TabIndex = 7;
            this.txtIpAddress.TextBoxLeft = 82;
            // 
            // frmOperationRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 321);
            this.Controls.Add(this.txtIpAddress);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.neuLabel2);
            this.Controls.Add(this.cmbValid);
            this.Controls.Add(this.neuLabel1);
            this.Controls.Add(this.txtInputCode);
            this.Controls.Add(this.txtDept);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtID);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOperationRoom";
            this.Text = "手术间维护";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuLabelTextBox txtID;
        private FS.FrameWork.WinForms.Controls.NeuLabelTextBox txtName;
        private FS.FrameWork.WinForms.Controls.NeuLabelTextBox txtInputCode;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbValid;
        private FS.FrameWork.WinForms.Controls.NeuLabelTextBox txtDept;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuButton btnOK;
        private FS.FrameWork.WinForms.Controls.NeuButton btnCancel;
        private FS.FrameWork.WinForms.Controls.NeuLabelTextBox txtIpAddress;
    }
}