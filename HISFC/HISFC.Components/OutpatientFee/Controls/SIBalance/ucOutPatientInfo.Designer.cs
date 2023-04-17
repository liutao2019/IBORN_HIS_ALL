namespace FS.HISFC.Components.OutpatientFee.Controls.SIBalance
{
    partial class ucOutPatientInfo
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
            FS.FrameWork.WinForms.Controls.NeuLabel lbPact;
            FS.FrameWork.WinForms.Controls.NeuLabel lbDoct;
            this.tbCardNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lbCardNO = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbAge = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.tbName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lbName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbSex = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbSex = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lbAge = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbPact = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lbRegDept = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbDoct = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbRegDept = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.txtIDCardNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbSIInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDiagnose = new System.Windows.Forms.TextBox();
            lbPact = new FS.FrameWork.WinForms.Controls.NeuLabel();
            lbDoct = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.SuspendLayout();
            // 
            // lbPact
            // 
            lbPact.AutoSize = true;
            lbPact.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            lbPact.ForeColor = System.Drawing.Color.Black;
            lbPact.Location = new System.Drawing.Point(373, 40);
            lbPact.Name = "lbPact";
            lbPact.Size = new System.Drawing.Size(59, 12);
            lbPact.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            lbPact.TabIndex = 26;
            lbPact.Text = "结算种类:";
            // 
            // lbDoct
            // 
            lbDoct.AutoSize = true;
            lbDoct.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            lbDoct.ForeColor = System.Drawing.Color.Black;
            lbDoct.Location = new System.Drawing.Point(191, 41);
            lbDoct.Name = "lbDoct";
            lbDoct.Size = new System.Drawing.Size(59, 12);
            lbDoct.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            lbDoct.TabIndex = 24;
            lbDoct.Text = "开立医生:";
            // 
            // tbCardNO
            // 
            this.tbCardNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbCardNO.IsEnter2Tab = false;
            this.tbCardNO.Location = new System.Drawing.Point(74, 7);
            this.tbCardNO.Name = "tbCardNO";
            this.tbCardNO.Size = new System.Drawing.Size(111, 21);
            this.tbCardNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbCardNO.TabIndex = 15;
            this.tbCardNO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbCardNO_KeyDown);
            // 
            // lbCardNO
            // 
            this.lbCardNO.AutoSize = true;
            this.lbCardNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbCardNO.ForeColor = System.Drawing.Color.Black;
            this.lbCardNO.Location = new System.Drawing.Point(9, 11);
            this.lbCardNO.Name = "lbCardNO";
            this.lbCardNO.Size = new System.Drawing.Size(59, 12);
            this.lbCardNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbCardNO.TabIndex = 14;
            this.lbCardNO.Text = "就诊卡号:";
            // 
            // tbAge
            // 
            this.tbAge.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbAge.IsEnter2Tab = false;
            this.tbAge.Location = new System.Drawing.Point(520, 6);
            this.tbAge.Name = "tbAge";
            this.tbAge.Size = new System.Drawing.Size(45, 21);
            this.tbAge.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbAge.TabIndex = 21;
            // 
            // tbName
            // 
            this.tbName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbName.IsEnter2Tab = false;
            this.tbName.Location = new System.Drawing.Point(256, 7);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(111, 21);
            this.tbName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbName.TabIndex = 17;
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbName.ForeColor = System.Drawing.Color.Black;
            this.lbName.Location = new System.Drawing.Point(191, 10);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(59, 12);
            this.lbName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbName.TabIndex = 16;
            this.lbName.Text = "患者姓名:";
            // 
            // lbSex
            // 
            this.lbSex.AutoSize = true;
            this.lbSex.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbSex.ForeColor = System.Drawing.Color.Black;
            this.lbSex.Location = new System.Drawing.Point(373, 10);
            this.lbSex.Name = "lbSex";
            this.lbSex.Size = new System.Drawing.Size(59, 12);
            this.lbSex.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbSex.TabIndex = 18;
            this.lbSex.Text = "患者性别:";
            // 
            // cmbSex
            // 
            this.cmbSex.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbSex.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbSex.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbSex.FormattingEnabled = true;
            this.cmbSex.IsEnter2Tab = false;
            this.cmbSex.IsFlat = false;
            this.cmbSex.IsLike = true;
            this.cmbSex.IsListOnly = false;
            this.cmbSex.IsPopForm = true;
            this.cmbSex.IsShowCustomerList = false;
            this.cmbSex.IsShowID = false;
            this.cmbSex.IsShowIDAndName = false;
            this.cmbSex.Location = new System.Drawing.Point(438, 7);
            this.cmbSex.Name = "cmbSex";
            this.cmbSex.ShowCustomerList = false;
            this.cmbSex.ShowID = false;
            this.cmbSex.Size = new System.Drawing.Size(49, 20);
            this.cmbSex.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbSex.TabIndex = 19;
            this.cmbSex.Tag = "";
            this.cmbSex.ToolBarUse = false;
            // 
            // lbAge
            // 
            this.lbAge.AutoSize = true;
            this.lbAge.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbAge.ForeColor = System.Drawing.Color.Black;
            this.lbAge.Location = new System.Drawing.Point(489, 11);
            this.lbAge.Name = "lbAge";
            this.lbAge.Size = new System.Drawing.Size(29, 12);
            this.lbAge.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbAge.TabIndex = 20;
            this.lbAge.Text = "年龄";
            // 
            // cmbPact
            // 
            this.cmbPact.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbPact.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbPact.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbPact.FormattingEnabled = true;
            this.cmbPact.IsEnter2Tab = false;
            this.cmbPact.IsFlat = false;
            this.cmbPact.IsLike = true;
            this.cmbPact.IsListOnly = false;
            this.cmbPact.IsPopForm = true;
            this.cmbPact.IsShowCustomerList = false;
            this.cmbPact.IsShowID = false;
            this.cmbPact.IsShowIDAndName = false;
            this.cmbPact.Location = new System.Drawing.Point(438, 38);
            this.cmbPact.Name = "cmbPact";
            this.cmbPact.ShowCustomerList = false;
            this.cmbPact.ShowID = false;
            this.cmbPact.Size = new System.Drawing.Size(127, 20);
            this.cmbPact.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbPact.TabIndex = 27;
            this.cmbPact.Tag = "";
            this.cmbPact.ToolBarUse = false;
            // 
            // lbRegDept
            // 
            this.lbRegDept.AutoSize = true;
            this.lbRegDept.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbRegDept.ForeColor = System.Drawing.Color.Black;
            this.lbRegDept.Location = new System.Drawing.Point(9, 42);
            this.lbRegDept.Name = "lbRegDept";
            this.lbRegDept.Size = new System.Drawing.Size(59, 12);
            this.lbRegDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbRegDept.TabIndex = 22;
            this.lbRegDept.Text = "看诊科室:";
            // 
            // cmbDoct
            // 
            this.cmbDoct.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbDoct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbDoct.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbDoct.FormattingEnabled = true;
            this.cmbDoct.IsEnter2Tab = false;
            this.cmbDoct.IsFlat = false;
            this.cmbDoct.IsLike = true;
            this.cmbDoct.IsListOnly = false;
            this.cmbDoct.IsPopForm = true;
            this.cmbDoct.IsShowCustomerList = false;
            this.cmbDoct.IsShowID = false;
            this.cmbDoct.IsShowIDAndName = false;
            this.cmbDoct.Location = new System.Drawing.Point(256, 39);
            this.cmbDoct.Name = "cmbDoct";
            this.cmbDoct.ShowCustomerList = false;
            this.cmbDoct.ShowID = false;
            this.cmbDoct.Size = new System.Drawing.Size(111, 20);
            this.cmbDoct.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbDoct.TabIndex = 23;
            this.cmbDoct.Tag = "";
            this.cmbDoct.ToolBarUse = false;
            // 
            // cmbRegDept
            // 
            this.cmbRegDept.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbRegDept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbRegDept.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbRegDept.FormattingEnabled = true;
            this.cmbRegDept.IsEnter2Tab = false;
            this.cmbRegDept.IsFlat = false;
            this.cmbRegDept.IsLike = true;
            this.cmbRegDept.IsListOnly = false;
            this.cmbRegDept.IsPopForm = true;
            this.cmbRegDept.IsShowCustomerList = false;
            this.cmbRegDept.IsShowID = false;
            this.cmbRegDept.IsShowIDAndName = false;
            this.cmbRegDept.Location = new System.Drawing.Point(74, 39);
            this.cmbRegDept.Name = "cmbRegDept";
            this.cmbRegDept.ShowCustomerList = false;
            this.cmbRegDept.ShowID = false;
            this.cmbRegDept.Size = new System.Drawing.Size(111, 20);
            this.cmbRegDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbRegDept.TabIndex = 25;
            this.cmbRegDept.Tag = "";
            this.cmbRegDept.ToolBarUse = false;
            // 
            // txtIDCardNO
            // 
            this.txtIDCardNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtIDCardNO.IsEnter2Tab = false;
            this.txtIDCardNO.Location = new System.Drawing.Point(645, 7);
            this.txtIDCardNO.Name = "txtIDCardNO";
            this.txtIDCardNO.Size = new System.Drawing.Size(147, 21);
            this.txtIDCardNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtIDCardNO.TabIndex = 29;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel1.ForeColor = System.Drawing.Color.Black;
            this.neuLabel1.Location = new System.Drawing.Point(580, 11);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(59, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 28;
            this.neuLabel1.Text = "身份证号:";
            // 
            // lbSIInfo
            // 
            this.lbSIInfo.AutoSize = true;
            this.lbSIInfo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbSIInfo.ForeColor = System.Drawing.Color.Red;
            this.lbSIInfo.Location = new System.Drawing.Point(9, 68);
            this.lbSIInfo.Name = "lbSIInfo";
            this.lbSIInfo.Size = new System.Drawing.Size(212, 12);
            this.lbSIInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbSIInfo.TabIndex = 30;
            this.lbSIInfo.Text = "医保登记号:47478748  已医保结算";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(580, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 31;
            this.label1.Text = "诊    断:";
            // 
            // txtDiagnose
            // 
            this.txtDiagnose.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDiagnose.Location = new System.Drawing.Point(645, 38);
            this.txtDiagnose.Name = "txtDiagnose";
            this.txtDiagnose.Size = new System.Drawing.Size(147, 21);
            this.txtDiagnose.TabIndex = 32;
            // 
            // ucOutPatientInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtDiagnose);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtIDCardNO);
            this.Controls.Add(this.tbCardNO);
            this.Controls.Add(this.tbAge);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.cmbSex);
            this.Controls.Add(this.cmbPact);
            this.Controls.Add(this.cmbDoct);
            this.Controls.Add(this.cmbRegDept);
            this.Controls.Add(this.lbSIInfo);
            this.Controls.Add(this.neuLabel1);
            this.Controls.Add(this.lbCardNO);
            this.Controls.Add(this.lbName);
            this.Controls.Add(this.lbSex);
            this.Controls.Add(this.lbAge);
            this.Controls.Add(lbPact);
            this.Controls.Add(this.lbRegDept);
            this.Controls.Add(lbDoct);
            this.Name = "ucOutPatientInfo";
            this.Size = new System.Drawing.Size(1000, 88);
            this.Load += new System.EventHandler(this.ucOutPatientInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected FS.FrameWork.WinForms.Controls.NeuTextBox tbCardNO;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbCardNO;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox tbAge;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox tbName;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbName;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbSex;
        protected FS.FrameWork.WinForms.Controls.NeuComboBox cmbSex;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbAge;
        protected FS.FrameWork.WinForms.Controls.NeuComboBox cmbPact;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbRegDept;
        protected FS.FrameWork.WinForms.Controls.NeuComboBox cmbDoct;
        protected FS.FrameWork.WinForms.Controls.NeuComboBox cmbRegDept;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox txtIDCardNO;
        protected FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbSIInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDiagnose;
    }
}
