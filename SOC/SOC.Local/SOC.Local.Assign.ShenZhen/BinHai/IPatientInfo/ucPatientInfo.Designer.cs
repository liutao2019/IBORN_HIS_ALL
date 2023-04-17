namespace SOC.Local.Assign.ShenZhen.BinHai.IPatientInfo
{
    partial class ucPatientInfo
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nlbRecipDoctor = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbRegDate = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbRecipeDept = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbCardNO = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbPatientName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nlbRecipDoctor);
            this.groupBox1.Controls.Add(this.nlbRegDate);
            this.groupBox1.Controls.Add(this.nlbRecipeDept);
            this.groupBox1.Controls.Add(this.nlbCardNO);
            this.groupBox1.Controls.Add(this.nlbPatientName);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(844, 56);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "患者信息";
            // 
            // nlbRecipDoctor
            // 
            this.nlbRecipDoctor.AutoSize = true;
            this.nlbRecipDoctor.ForeColor = System.Drawing.Color.Black;
            this.nlbRecipDoctor.Location = new System.Drawing.Point(450, 24);
            this.nlbRecipDoctor.Name = "nlbRecipDoctor";
            this.nlbRecipDoctor.Size = new System.Drawing.Size(77, 12);
            this.nlbRecipDoctor.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbRecipDoctor.TabIndex = 26;
            this.nlbRecipDoctor.Text = "医生：张学友";
            // 
            // nlbRegDate
            // 
            this.nlbRegDate.AutoSize = true;
            this.nlbRegDate.ForeColor = System.Drawing.Color.Black;
            this.nlbRegDate.Location = new System.Drawing.Point(531, 24);
            this.nlbRegDate.Name = "nlbRegDate";
            this.nlbRegDate.Size = new System.Drawing.Size(155, 12);
            this.nlbRegDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbRegDate.TabIndex = 25;
            this.nlbRegDate.Text = "挂号：2010-12-30 00:00:00";
            // 
            // nlbRecipeDept
            // 
            this.nlbRecipeDept.AutoSize = true;
            this.nlbRecipeDept.ForeColor = System.Drawing.Color.Black;
            this.nlbRecipeDept.Location = new System.Drawing.Point(354, 24);
            this.nlbRecipeDept.Name = "nlbRecipeDept";
            this.nlbRecipeDept.Size = new System.Drawing.Size(89, 12);
            this.nlbRecipeDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbRecipeDept.TabIndex = 24;
            this.nlbRecipeDept.Text = "科室：儿科门诊";
            // 
            // nlbCardNO
            // 
            this.nlbCardNO.AutoSize = true;
            this.nlbCardNO.ForeColor = System.Drawing.Color.Black;
            this.nlbCardNO.Location = new System.Drawing.Point(6, 24);
            this.nlbCardNO.Name = "nlbCardNO";
            this.nlbCardNO.Size = new System.Drawing.Size(113, 12);
            this.nlbCardNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbCardNO.TabIndex = 23;
            this.nlbCardNO.Text = "病例号：0123456789";
            // 
            // nlbPatientName
            // 
            this.nlbPatientName.AutoSize = true;
            this.nlbPatientName.ForeColor = System.Drawing.Color.Black;
            this.nlbPatientName.Location = new System.Drawing.Point(123, 24);
            this.nlbPatientName.Name = "nlbPatientName";
            this.nlbPatientName.Size = new System.Drawing.Size(125, 12);
            this.nlbPatientName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbPatientName.TabIndex = 20;
            this.nlbPatientName.Text = "患者：刘德华 男 20岁";
            // 
            // ucPatientInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ucPatientInfo";
            this.Size = new System.Drawing.Size(844, 56);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbRecipDoctor;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbRegDate;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbRecipeDept;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbCardNO;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbPatientName;
    }
}
