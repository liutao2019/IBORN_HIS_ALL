namespace FS.SOC.Local.RADT.ZhuHai.ZDWY.Base.Inpatient
{
    partial class ucDayLimitTot
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
            components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtInpatientNo = new FS.HISFC.Components.Common.Controls.ucQueryInpatientNo();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtCost = new FS.FrameWork.WinForms.Controls.ValidatedTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtBirthday = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNurseStation = new System.Windows.Forms.TextBox();
            this.txtDoctor = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDateIn = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtBedNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDeptName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBalanceType = new System.Windows.Forms.TextBox();
            //this.ucPatientInfo = new FS.Common.Controls.ucPatientInfo();
            this.ucPatientInfo = new FS.HISFC.Components.Common.Controls.ucPatient();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtInpatientNo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.btnOK);
            this.groupBox1.Controls.Add(this.txtCost);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(736, 52);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // txtInpatientNo
            // 
            this.txtInpatientNo.InputType = 0;
            this.txtInpatientNo.Location = new System.Drawing.Point(8, 16);
            this.txtInpatientNo.Name = "txtInpatientNo";
            this.txtInpatientNo.ShowState = FS.HISFC.Components.Common.Controls.enuShowState.InhosBeforBalanced;
            this.txtInpatientNo.Size = new System.Drawing.Size(196, 28);
            this.txtInpatientNo.TabIndex = 0;
            this.txtInpatientNo.Load += new System.EventHandler(this.txtInpatientNo_Load);
            this.txtInpatientNo.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(this.txtInpatientNo_myEvent);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(208, 20);
            this.label1.Name = "label1";
            this.label1.TabIndex = 2;
            this.label1.Text = "日限额累计金额";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(644, 22);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "退出(&Q)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOK.Location = new System.Drawing.Point(560, 22);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 24);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确  定(&O)";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtCost
            // 
            this.txtCost.AllowNegative = false;
            this.txtCost.AutoPadRightZero = true;
            this.txtCost.Location = new System.Drawing.Point(312, 20);
            this.txtCost.MaxDigits = 2;
            this.txtCost.Name = "txtCost";
            this.txtCost.Size = new System.Drawing.Size(120, 21);
            this.txtCost.TabIndex = 3;
            this.txtCost.Text = "";
            this.txtCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCost.WillShowError = true;
            this.txtCost.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCost_KeyPress);
            //			this.txtCost.Leave += new System.EventHandler(this.txtCost_Leave);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.txtBirthday);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtNurseStation);
            this.groupBox2.Controls.Add(this.txtDoctor);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtDateIn);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtBedNo);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtDeptName);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtName);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtBalanceType);
            this.groupBox2.Controls.Add(this.ucPatientInfo);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 52);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(736, 84);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "患者信息";
            // 
            // txtBirthday
            // 
            this.txtBirthday.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtBirthday.Location = new System.Drawing.Point(610, 45);
            this.txtBirthday.Name = "txtBirthday";
            this.txtBirthday.ReadOnly = true;
            this.txtBirthday.Size = new System.Drawing.Size(106, 21);
            this.txtBirthday.TabIndex = 34;
            this.txtBirthday.Text = "";
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(546, 49);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(55, 17);
            this.label15.TabIndex = 33;
            this.label15.Text = "出生日期";
            this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(546, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 17);
            this.label9.TabIndex = 31;
            this.label9.Text = "所属病区";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtNurseStation
            // 
            this.txtNurseStation.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtNurseStation.Location = new System.Drawing.Point(610, 15);
            this.txtNurseStation.Name = "txtNurseStation";
            this.txtNurseStation.ReadOnly = true;
            this.txtNurseStation.Size = new System.Drawing.Size(106, 21);
            this.txtNurseStation.TabIndex = 32;
            this.txtNurseStation.Text = "";
            // 
            // txtDoctor
            // 
            this.txtDoctor.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtDoctor.Location = new System.Drawing.Point(258, 45);
            this.txtDoctor.Name = "txtDoctor";
            this.txtDoctor.ReadOnly = true;
            this.txtDoctor.Size = new System.Drawing.Size(107, 21);
            this.txtDoctor.TabIndex = 30;
            this.txtDoctor.Text = "";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(197, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 17);
            this.label8.TabIndex = 29;
            this.label8.Text = "住院医生";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtDateIn
            // 
            this.txtDateIn.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtDateIn.Location = new System.Drawing.Point(67, 45);
            this.txtDateIn.Name = "txtDateIn";
            this.txtDateIn.ReadOnly = true;
            this.txtDateIn.Size = new System.Drawing.Size(116, 21);
            this.txtDateIn.TabIndex = 28;
            this.txtDateIn.Text = "";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(8, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 17);
            this.label7.TabIndex = 27;
            this.label7.Text = "入院日期";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtBedNo
            // 
            this.txtBedNo.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtBedNo.Location = new System.Drawing.Point(437, 45);
            this.txtBedNo.Name = "txtBedNo";
            this.txtBedNo.ReadOnly = true;
            this.txtBedNo.Size = new System.Drawing.Size(106, 21);
            this.txtBedNo.TabIndex = 23;
            this.txtBedNo.Text = "";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(375, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 17);
            this.label4.TabIndex = 20;
            this.label4.Text = "床号";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(375, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 17);
            this.label6.TabIndex = 21;
            this.label6.Text = "住院科室";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtDeptName
            // 
            this.txtDeptName.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtDeptName.Location = new System.Drawing.Point(437, 15);
            this.txtDeptName.Name = "txtDeptName";
            this.txtDeptName.ReadOnly = true;
            this.txtDeptName.Size = new System.Drawing.Size(106, 21);
            this.txtDeptName.TabIndex = 24;
            this.txtDeptName.Text = "";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(7, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 17);
            this.label2.TabIndex = 19;
            this.label2.Text = "患者姓名";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtName.Location = new System.Drawing.Point(67, 15);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(116, 21);
            this.txtName.TabIndex = 22;
            this.txtName.Text = "";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(197, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 17);
            this.label5.TabIndex = 25;
            this.label5.Text = "合同单位";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtBalanceType
            // 
            this.txtBalanceType.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtBalanceType.Location = new System.Drawing.Point(258, 15);
            this.txtBalanceType.Name = "txtBalanceType";
            this.txtBalanceType.ReadOnly = true;
            this.txtBalanceType.Size = new System.Drawing.Size(107, 21);
            this.txtBalanceType.TabIndex = 26;
            this.txtBalanceType.Text = "";
            // 
            // ucPatientInfo
            // 
            this.ucPatientInfo.BackColor = System.Drawing.SystemColors.Control;
            this.ucPatientInfo.IsShowDetail = false;
            this.ucPatientInfo.Location = new System.Drawing.Point(748, 14);
            this.ucPatientInfo.Name = "ucPatientInfo";
            this.ucPatientInfo.PatientInfo = null;
            this.ucPatientInfo.Size = new System.Drawing.Size(15, 41);
            this.ucPatientInfo.TabIndex = 0;
            this.ucPatientInfo.Visible = false;
            // 
            // ucDayLimitTot
            // 
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ucDayLimitTot";
            this.Size = new System.Drawing.Size(736, 136);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        //private FS.Common.Controls.ucQueryInpatientNo txtInpatientNo;
        private FS.HISFC.Components.Common.Controls.ucQueryInpatientNo txtInpatientNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private FS.FrameWork.WinForms.Controls.ValidatedTextBox txtCost;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtBirthday;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtNurseStation;
        private System.Windows.Forms.TextBox txtDoctor;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDateIn;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtBedNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDeptName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBalanceType;
        //private FS.Common.Controls.ucPatientInfo ucPatientInfo;
        private FS.HISFC.Components.Common.Controls.ucPatient ucPatientInfo;
    }
}
