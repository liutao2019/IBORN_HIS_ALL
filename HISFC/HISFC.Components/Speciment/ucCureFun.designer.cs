namespace UFC.Speciment
{
    partial class ucCureFun
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
            this.rbtBeforeCure = new System.Windows.Forms.RadioButton();
            this.rbtAfterCure = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOperPos = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.txtRadSch = new FS.FrameWork.WinForms.Controls.NeuListTextBox();
            this.txtMedSch = new FS.FrameWork.WinForms.Controls.NeuListTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "治疗阶段：";
            // 
            // rbtBeforeCure
            // 
            this.rbtBeforeCure.AutoSize = true;
            this.rbtBeforeCure.Checked = true;
            this.rbtBeforeCure.Location = new System.Drawing.Point(100, 15);
            this.rbtBeforeCure.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbtBeforeCure.Name = "rbtBeforeCure";
            this.rbtBeforeCure.Size = new System.Drawing.Size(74, 20);
            this.rbtBeforeCure.TabIndex = 1;
            this.rbtBeforeCure.TabStop = true;
            this.rbtBeforeCure.Text = "治疗前";
            this.rbtBeforeCure.UseVisualStyleBackColor = true;
            // 
            // rbtAfterCure
            // 
            this.rbtAfterCure.AutoSize = true;
            this.rbtAfterCure.Location = new System.Drawing.Point(187, 15);
            this.rbtAfterCure.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbtAfterCure.Name = "rbtAfterCure";
            this.rbtAfterCure.Size = new System.Drawing.Size(74, 20);
            this.rbtAfterCure.TabIndex = 2;
            this.rbtAfterCure.Text = "治疗后";
            this.rbtAfterCure.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(292, 17);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "手术部位：";
            // 
            // txtOperPos
            // 
            this.txtOperPos.Location = new System.Drawing.Point(387, 12);
            this.txtOperPos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtOperPos.Name = "txtOperPos";
            this.txtOperPos.Size = new System.Drawing.Size(132, 26);
            this.txtOperPos.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 56);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "放疗方案：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(271, 56);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "化疗方案：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 113);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "备注：";
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(115, 97);
            this.txtComment.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(404, 48);
            this.txtComment.TabIndex = 10;
            // 
            // txtRadSch
            // 
            this.txtRadSch.EnterVisiable = false;
            this.txtRadSch.IsFind = false;
            //this.txtRadSch.IsSelctNone = true;
            //this.txtRadSch.IsSendToNext = false;
            //this.txtRadSch.IsShowID = false;
            //this.txtRadSch.ItemText = "";
            this.txtRadSch.ListBoxHeight = 100;
            //this.txtRadSch.ListBoxVisible = false;
            this.txtRadSch.ListBoxWidth = 100;
            this.txtRadSch.Location = new System.Drawing.Point(100, 51);
            this.txtRadSch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtRadSch.Name = "txtRadSch";
            this.txtRadSch.OmitFilter = true;
            this.txtRadSch.SelectedItem = null;
            this.txtRadSch.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRadSch.ShowID = true;
            this.txtRadSch.Size = new System.Drawing.Size(132, 26);
            this.txtRadSch.TabIndex = 11;
            // 
            // txtMedSch
            // 
            this.txtMedSch.EnterVisiable = false;
            this.txtMedSch.IsFind = false;
            //this.txtMedSch.IsSelctNone = true;
            //this.txtMedSch.IsSendToNext = false;
            //this.txtMedSch.IsShowID = false;
            //this.txtMedSch.ItemText = "";
            this.txtMedSch.ListBoxHeight = 100;
            //this.txtMedSch.ListBoxVisible = false;
            this.txtMedSch.ListBoxWidth = 100;
            this.txtMedSch.Location = new System.Drawing.Point(365, 51);
            this.txtMedSch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtMedSch.Name = "txtMedSch";
            this.txtMedSch.OmitFilter = true;
            this.txtMedSch.SelectedItem = null;
            this.txtMedSch.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMedSch.ShowID = true;
            this.txtMedSch.Size = new System.Drawing.Size(132, 26);
            this.txtMedSch.TabIndex = 11;
            // 
            // ucCureFun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtMedSch);
            this.Controls.Add(this.txtRadSch);
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtOperPos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rbtAfterCure);
            this.Controls.Add(this.rbtBeforeCure);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ucCureFun";
            this.Size = new System.Drawing.Size(525, 149);
            this.Load += new System.EventHandler(this.ucCureFun_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbtBeforeCure;
        private System.Windows.Forms.RadioButton rbtAfterCure;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOperPos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtComment;
        private FS.FrameWork.WinForms.Controls.NeuListTextBox txtRadSch;
        private FS.FrameWork.WinForms.Controls.NeuListTextBox txtMedSch;
    }
}
