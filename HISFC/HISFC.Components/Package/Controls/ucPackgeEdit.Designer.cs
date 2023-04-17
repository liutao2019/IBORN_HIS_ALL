namespace HISFC.Components.Package.Controls
{
    partial class ucPackgeEdit
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
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdPhysicalExam = new System.Windows.Forms.RadioButton();
            this.rdInpatient = new System.Windows.Forms.RadioButton();
            this.rdOutPatient = new System.Windows.Forms.RadioButton();
            this.rdAll = new System.Windows.Forms.RadioButton();
            this.chkValid = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.cmbpackageType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.txtUserCode = new System.Windows.Forms.TextBox();
            this.lblMoney = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(14, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "套餐名称：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(14, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 19);
            this.label3.TabIndex = 3;
            this.label3.Text = "套餐类型：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdPhysicalExam);
            this.groupBox1.Controls.Add(this.rdInpatient);
            this.groupBox1.Controls.Add(this.rdOutPatient);
            this.groupBox1.Controls.Add(this.rdAll);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(408, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(228, 37);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // rdPhysicalExam
            // 
            this.rdPhysicalExam.AutoSize = true;
            this.rdPhysicalExam.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdPhysicalExam.ForeColor = System.Drawing.Color.Black;
            this.rdPhysicalExam.Location = new System.Drawing.Point(173, 15);
            this.rdPhysicalExam.Name = "rdPhysicalExam";
            this.rdPhysicalExam.Size = new System.Drawing.Size(47, 16);
            this.rdPhysicalExam.TabIndex = 3;
            this.rdPhysicalExam.Text = "体检";
            this.rdPhysicalExam.UseVisualStyleBackColor = true;
            // 
            // rdInpatient
            // 
            this.rdInpatient.AutoSize = true;
            this.rdInpatient.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdInpatient.ForeColor = System.Drawing.Color.Black;
            this.rdInpatient.Location = new System.Drawing.Point(120, 15);
            this.rdInpatient.Name = "rdInpatient";
            this.rdInpatient.Size = new System.Drawing.Size(47, 16);
            this.rdInpatient.TabIndex = 2;
            this.rdInpatient.Text = "住院";
            this.rdInpatient.UseVisualStyleBackColor = true;
            // 
            // rdOutPatient
            // 
            this.rdOutPatient.AutoSize = true;
            this.rdOutPatient.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdOutPatient.ForeColor = System.Drawing.Color.Black;
            this.rdOutPatient.Location = new System.Drawing.Point(67, 15);
            this.rdOutPatient.Name = "rdOutPatient";
            this.rdOutPatient.Size = new System.Drawing.Size(47, 16);
            this.rdOutPatient.TabIndex = 1;
            this.rdOutPatient.Text = "门诊";
            this.rdOutPatient.UseVisualStyleBackColor = true;
            // 
            // rdAll
            // 
            this.rdAll.AutoSize = true;
            this.rdAll.Checked = true;
            this.rdAll.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdAll.ForeColor = System.Drawing.Color.Black;
            this.rdAll.Location = new System.Drawing.Point(14, 15);
            this.rdAll.Name = "rdAll";
            this.rdAll.Size = new System.Drawing.Size(47, 16);
            this.rdAll.TabIndex = 0;
            this.rdAll.TabStop = true;
            this.rdAll.Text = "全部";
            this.rdAll.UseVisualStyleBackColor = true;
            // 
            // chkValid
            // 
            this.chkValid.AutoSize = true;
            this.chkValid.Checked = true;
            this.chkValid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkValid.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkValid.ForeColor = System.Drawing.Color.Red;
            this.chkValid.Location = new System.Drawing.Point(546, 5);
            this.chkValid.Name = "chkValid";
            this.chkValid.Size = new System.Drawing.Size(84, 24);
            this.chkValid.TabIndex = 6;
            this.chkValid.Text = "是否有效";
            this.chkValid.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(328, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 19);
            this.label4.TabIndex = 8;
            this.label4.Text = "自定义码：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(14, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 19);
            this.label5.TabIndex = 9;
            this.label5.Text = "套餐备注：";
            // 
            // txtMemo
            // 
            this.txtMemo.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMemo.Location = new System.Drawing.Point(94, 72);
            this.txtMemo.Multiline = true;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(542, 35);
            this.txtMemo.TabIndex = 10;
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtName.Location = new System.Drawing.Point(94, 6);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(210, 25);
            this.txtName.TabIndex = 11;
            // 
            // cmbpackageType
            // 
            this.cmbpackageType.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbpackageType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbpackageType.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbpackageType.FormattingEnabled = true;
            this.cmbpackageType.IsEnter2Tab = false;
            this.cmbpackageType.IsFlat = false;
            this.cmbpackageType.IsLike = true;
            this.cmbpackageType.IsListOnly = false;
            this.cmbpackageType.IsPopForm = true;
            this.cmbpackageType.IsShowCustomerList = false;
            this.cmbpackageType.IsShowID = false;
            this.cmbpackageType.IsShowIDAndName = false;
            this.cmbpackageType.Location = new System.Drawing.Point(94, 38);
            this.cmbpackageType.Name = "cmbpackageType";
            this.cmbpackageType.ShowCustomerList = false;
            this.cmbpackageType.ShowID = false;
            this.cmbpackageType.Size = new System.Drawing.Size(210, 27);
            this.cmbpackageType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbpackageType.TabIndex = 12;
            this.cmbpackageType.Tag = "";
            this.cmbpackageType.ToolBarUse = false;
            // 
            // txtUserCode
            // 
            this.txtUserCode.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUserCode.Location = new System.Drawing.Point(408, 5);
            this.txtUserCode.Name = "txtUserCode";
            this.txtUserCode.Size = new System.Drawing.Size(130, 25);
            this.txtUserCode.TabIndex = 13;
            // 
            // lblMoney
            // 
            this.lblMoney.AutoSize = true;
            this.lblMoney.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.lblMoney.ForeColor = System.Drawing.Color.Red;
            this.lblMoney.Location = new System.Drawing.Point(636, 6);
            this.lblMoney.Name = "lblMoney";
            this.lblMoney.Size = new System.Drawing.Size(136, 20);
            this.lblMoney.TabIndex = 14;
            this.lblMoney.Text = "套餐金额：￥500.00";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(328, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 19);
            this.label2.TabIndex = 15;
            this.label2.Text = "套餐范围：";
            // 
            // ucPackgeEdit
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblMoney);
            this.Controls.Add(this.txtUserCode);
            this.Controls.Add(this.cmbpackageType);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtMemo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chkValid);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "ucPackgeEdit";
            this.Size = new System.Drawing.Size(960, 118);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdPhysicalExam;
        private System.Windows.Forms.RadioButton rdInpatient;
        private System.Windows.Forms.RadioButton rdOutPatient;
        private System.Windows.Forms.RadioButton rdAll;
        private System.Windows.Forms.CheckBox chkValid;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMemo;
        private System.Windows.Forms.TextBox txtName;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbpackageType;
        private System.Windows.Forms.TextBox txtUserCode;
        private System.Windows.Forms.Label lblMoney;
        private System.Windows.Forms.Label label2;
    }
}
