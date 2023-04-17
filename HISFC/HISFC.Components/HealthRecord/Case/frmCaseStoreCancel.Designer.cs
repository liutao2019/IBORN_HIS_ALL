namespace FS.HISFC.Components.HealthRecord.Case
{
    partial class frmCaseStoreCancel
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btOK = new System.Windows.Forms.Button();
            this.btQuit = new System.Windows.Forms.Button();
            this.txtOldCaseNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtOldInTimes = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtNewCaseNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtNewInTimes = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label1.Location = new System.Drawing.Point(34, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "旧病案号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "旧住院次数";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label3.Location = new System.Drawing.Point(247, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "-->新病案号";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(234, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "-->新住院次数";
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(189, 217);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 4;
            this.btOK.Text = "确定";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btQuit
            // 
            this.btQuit.Location = new System.Drawing.Point(314, 217);
            this.btQuit.Name = "btQuit";
            this.btQuit.Size = new System.Drawing.Size(75, 23);
            this.btQuit.TabIndex = 5;
            this.btQuit.Text = "取消";
            this.btQuit.UseVisualStyleBackColor = true;
            this.btQuit.Click += new System.EventHandler(this.btQuit_Click);
            // 
            // txtOldCaseNO
            // 
            this.txtOldCaseNO.BackColor = System.Drawing.Color.Azure;
            this.txtOldCaseNO.IsEnter2Tab = false;
            this.txtOldCaseNO.Location = new System.Drawing.Point(113, 100);
            this.txtOldCaseNO.Name = "txtOldCaseNO";
            this.txtOldCaseNO.Size = new System.Drawing.Size(102, 21);
            this.txtOldCaseNO.Style =  FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtOldCaseNO.TabIndex = 102;
            this.txtOldCaseNO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOldCaseNO_KeyDown);
            // 
            // txtOldInTimes
            // 
            this.txtOldInTimes.BackColor = System.Drawing.Color.Azure;
            this.txtOldInTimes.IsEnter2Tab = false;
            this.txtOldInTimes.Location = new System.Drawing.Point(113, 140);
            this.txtOldInTimes.Name = "txtOldInTimes";
            this.txtOldInTimes.Size = new System.Drawing.Size(102, 21);
            this.txtOldInTimes.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtOldInTimes.TabIndex = 103;
            // 
            // txtNewCaseNO
            // 
            this.txtNewCaseNO.BackColor = System.Drawing.Color.Azure;
            this.txtNewCaseNO.IsEnter2Tab = false;
            this.txtNewCaseNO.Location = new System.Drawing.Point(324, 100);
            this.txtNewCaseNO.Name = "txtNewCaseNO";
            this.txtNewCaseNO.Size = new System.Drawing.Size(102, 21);
            this.txtNewCaseNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtNewCaseNO.TabIndex = 104;
            this.txtNewCaseNO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNewCaseNO_KeyDown);
            // 
            // txtNewInTimes
            // 
            this.txtNewInTimes.BackColor = System.Drawing.Color.Azure;
            this.txtNewInTimes.IsEnter2Tab = false;
            this.txtNewInTimes.Location = new System.Drawing.Point(324, 140);
            this.txtNewInTimes.Name = "txtNewInTimes";
            this.txtNewInTimes.Size = new System.Drawing.Size(102, 21);
            this.txtNewInTimes.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtNewInTimes.TabIndex = 105;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(25, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(383, 12);
            this.label5.TabIndex = 106;
            this.label5.Text = "操作说明：请在“旧病案号”处输入“病案号”+“住院次数”，回车；";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(83, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(323, 12);
            this.label6.TabIndex = 107;
            this.label6.Text = "请在“新病案号”处输入“病案号”+“住院次数”，回车；";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(83, 58);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 108;
            this.label7.Text = "单击“确定”";
            // 
            // frmCaseStoreCancel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 319);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtNewInTimes);
            this.Controls.Add(this.txtNewCaseNO);
            this.Controls.Add(this.txtOldInTimes);
            this.Controls.Add(this.txtOldCaseNO);
            this.Controls.Add(this.btQuit);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmCaseStoreCancel";
            this.Text = "修改住院号和住院次数";
            this.Load += new System.EventHandler(this.frmCaseStoreCancel_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btQuit;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtOldCaseNO;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtOldInTimes;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtNewCaseNO;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtNewInTimes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}