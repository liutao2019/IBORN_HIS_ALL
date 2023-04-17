namespace API.GZSI.Print.uc4101
{
    partial class ucPayType
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
            this.lbHiPaymtd = new System.Windows.Forms.Label();
            this.panel26 = new System.Windows.Forms.Panel();
            this.label42 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbHiPaymtd
            // 
            this.lbHiPaymtd.AutoSize = true;
            this.lbHiPaymtd.Location = new System.Drawing.Point(116, 9);
            this.lbHiPaymtd.Name = "lbHiPaymtd";
            this.lbHiPaymtd.Size = new System.Drawing.Size(11, 12);
            this.lbHiPaymtd.TabIndex = 80;
            this.lbHiPaymtd.Text = "1";
            // 
            // panel26
            // 
            this.panel26.BackColor = System.Drawing.SystemColors.ControlText;
            this.panel26.Location = new System.Drawing.Point(103, 24);
            this.panel26.Name = "panel26";
            this.panel26.Size = new System.Drawing.Size(40, 1);
            this.panel26.TabIndex = 78;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(21, 9);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(89, 12);
            this.label42.TabIndex = 79;
            this.label42.Text = "医保支付方式：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(147, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(473, 12);
            this.label1.TabIndex = 81;
            this.label1.Text = "1.按项目 2.单病种 3.按病种分值 4.疾病诊断相关分组（DRG） 5.按床日 6.按人头……";
            // 
            // ucPayType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbHiPaymtd);
            this.Controls.Add(this.panel26);
            this.Controls.Add(this.label42);
            this.Name = "ucPayType";
            this.Size = new System.Drawing.Size(786, 30);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbHiPaymtd;
        private System.Windows.Forms.Panel panel26;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label1;
    }
}
