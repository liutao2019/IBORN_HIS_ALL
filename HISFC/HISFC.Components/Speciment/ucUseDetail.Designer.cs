namespace UFC.Speciment
{
    partial class ucUseDetail
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblDept = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblUseCount = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblSpecType = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblPercent = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "科室：";
            // 
            // lblDept
            // 
            this.lblDept.AutoSize = true;
            this.lblDept.Location = new System.Drawing.Point(50, 9);
            this.lblDept.Name = "lblDept";
            this.lblDept.Size = new System.Drawing.Size(41, 12);
            this.lblDept.TabIndex = 1;
            this.lblDept.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(401, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "取用比例：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(330, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "剩余数量：";
            // 
            // lblUseCount
            // 
            this.lblUseCount.AutoSize = true;
            this.lblUseCount.Location = new System.Drawing.Point(283, 9);
            this.lblUseCount.Name = "lblUseCount";
            this.lblUseCount.Size = new System.Drawing.Size(41, 12);
            this.lblUseCount.TabIndex = 7;
            this.lblUseCount.Text = "label2";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(224, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "取用数量：";
            // 
            // lblSpecType
            // 
            this.lblSpecType.AutoSize = true;
            this.lblSpecType.Location = new System.Drawing.Point(176, 9);
            this.lblSpecType.Name = "lblSpecType";
            this.lblSpecType.Size = new System.Drawing.Size(41, 12);
            this.lblSpecType.TabIndex = 9;
            this.lblSpecType.Text = "label2";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(114, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 8;
            this.label9.Text = "病种类型：";
            // 
            // lblPercent
            // 
            this.lblPercent.AutoSize = true;
            this.lblPercent.Location = new System.Drawing.Point(463, 9);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.Size = new System.Drawing.Size(29, 12);
            this.lblPercent.TabIndex = 10;
            this.lblPercent.Text = "取用";
            // 
            // ucUseDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblPercent);
            this.Controls.Add(this.lblSpecType);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblUseCount);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblDept);
            this.Controls.Add(this.label1);
            this.Name = "ucUseDetail";
            this.Size = new System.Drawing.Size(496, 26);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDept;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblUseCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblSpecType;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblPercent;
    }
}
