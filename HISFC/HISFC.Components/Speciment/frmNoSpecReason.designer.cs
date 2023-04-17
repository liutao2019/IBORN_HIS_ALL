namespace FS.HISFC.Components.Speciment
{
    partial class frmNoSpecReason
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
            this.rbtNoAgree = new System.Windows.Forms.RadioButton();
            this.rbtOther = new System.Windows.Forms.RadioButton();
            this.rbtNoNeed = new System.Windows.Forms.RadioButton();
            this.rbtToSmall = new System.Windows.Forms.RadioButton();
            this.lblReason = new System.Windows.Forms.Label();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPatient = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rbtNoAgree
            // 
            this.rbtNoAgree.AutoSize = true;
            this.rbtNoAgree.Checked = true;
            this.rbtNoAgree.Location = new System.Drawing.Point(12, 86);
            this.rbtNoAgree.Name = "rbtNoAgree";
            this.rbtNoAgree.Size = new System.Drawing.Size(83, 16);
            this.rbtNoAgree.TabIndex = 0;
            this.rbtNoAgree.TabStop = true;
            this.rbtNoAgree.Text = "病人不同意";
            this.rbtNoAgree.UseVisualStyleBackColor = true;
            this.rbtNoAgree.CheckedChanged += new System.EventHandler(this.rbt_CheckedChanged);
            // 
            // rbtOther
            // 
            this.rbtOther.AutoSize = true;
            this.rbtOther.Location = new System.Drawing.Point(177, 86);
            this.rbtOther.Name = "rbtOther";
            this.rbtOther.Size = new System.Drawing.Size(47, 16);
            this.rbtOther.TabIndex = 1;
            this.rbtOther.Text = "其他";
            this.rbtOther.UseVisualStyleBackColor = true;
            this.rbtOther.CheckedChanged += new System.EventHandler(this.rbt_CheckedChanged);
            // 
            // rbtNoNeed
            // 
            this.rbtNoNeed.AutoSize = true;
            this.rbtNoNeed.Location = new System.Drawing.Point(101, 86);
            this.rbtNoNeed.Name = "rbtNoNeed";
            this.rbtNoNeed.Size = new System.Drawing.Size(59, 16);
            this.rbtNoNeed.TabIndex = 2;
            this.rbtNoNeed.Text = "没必要";
            this.rbtNoNeed.UseVisualStyleBackColor = true;
            this.rbtNoNeed.CheckedChanged += new System.EventHandler(this.rbt_CheckedChanged);
            // 
            // rbtToSmall
            // 
            this.rbtToSmall.AutoSize = true;
            this.rbtToSmall.Location = new System.Drawing.Point(250, 86);
            this.rbtToSmall.Name = "rbtToSmall";
            this.rbtToSmall.Size = new System.Drawing.Size(71, 16);
            this.rbtToSmall.TabIndex = 3;
            this.rbtToSmall.Text = "组织太小";
            this.rbtToSmall.UseVisualStyleBackColor = true;
            this.rbtToSmall.CheckedChanged += new System.EventHandler(this.rbt_CheckedChanged);
            // 
            // lblReason
            // 
            this.lblReason.AutoSize = true;
            this.lblReason.Location = new System.Drawing.Point(24, 136);
            this.lblReason.Name = "lblReason";
            this.lblReason.Size = new System.Drawing.Size(29, 12);
            this.lblReason.TabIndex = 4;
            this.lblReason.Text = "原因";
            this.lblReason.Visible = false;
            // 
            // txtReason
            // 
            this.txtReason.Location = new System.Drawing.Point(78, 117);
            this.txtReason.Multiline = true;
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(208, 53);
            this.txtReason.TabIndex = 5;
            this.txtReason.Visible = false;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(65, 187);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(193, 187);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblDateTime
            // 
            this.lblDateTime.AutoSize = true;
            this.lblDateTime.Location = new System.Drawing.Point(76, 23);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(71, 12);
            this.lblDateTime.TabIndex = 8;
            this.lblDateTime.Text = "lblDateTime";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(166, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "病人：";
            // 
            // lblPatient
            // 
            this.lblPatient.AutoSize = true;
            this.lblPatient.Location = new System.Drawing.Point(213, 23);
            this.lblPatient.Name = "lblPatient";
            this.lblPatient.Size = new System.Drawing.Size(65, 12);
            this.lblPatient.TabIndex = 10;
            this.lblPatient.Text = "lblPatient";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(76, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(149, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "标本未留取，请填写原因！";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "日期：";
            // 
            // frmNoSpecReason
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(341, 236);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblPatient);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblDateTime);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtReason);
            this.Controls.Add(this.lblReason);
            this.Controls.Add(this.rbtToSmall);
            this.Controls.Add(this.rbtNoNeed);
            this.Controls.Add(this.rbtOther);
            this.Controls.Add(this.rbtNoAgree);
            this.Name = "frmNoSpecReason";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "未留标本原因";
            this.Load += new System.EventHandler(this.frmNoSpecReason_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbtNoAgree;
        private System.Windows.Forms.RadioButton rbtOther;
        private System.Windows.Forms.RadioButton rbtNoNeed;
        private System.Windows.Forms.RadioButton rbtToSmall;
        private System.Windows.Forms.Label lblReason;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPatient;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;

    }
}