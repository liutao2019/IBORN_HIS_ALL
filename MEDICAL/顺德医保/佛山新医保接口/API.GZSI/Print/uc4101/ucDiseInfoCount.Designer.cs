namespace API.GZSI.Print.uc4101
{
    partial class ucDiseInfoCount
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
            this.lblDiagCodeCnt = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblDiagCodeCnt
            // 
            this.lblDiagCodeCnt.AutoSize = true;
            this.lblDiagCodeCnt.Location = new System.Drawing.Point(116, 9);
            this.lblDiagCodeCnt.Name = "lblDiagCodeCnt";
            this.lblDiagCodeCnt.Size = new System.Drawing.Size(11, 12);
            this.lblDiagCodeCnt.TabIndex = 77;
            this.lblDiagCodeCnt.Text = "1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlText;
            this.panel1.Location = new System.Drawing.Point(103, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(80, 1);
            this.panel1.TabIndex = 75;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 76;
            this.label2.Text = "诊断代码计数：";
            // 
            // ucDiseInfoCount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lblDiagCodeCnt);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Name = "ucDiseInfoCount";
            this.Size = new System.Drawing.Size(786, 30);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDiagCodeCnt;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;

    }
}
