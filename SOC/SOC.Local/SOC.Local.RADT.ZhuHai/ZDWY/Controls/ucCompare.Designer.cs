namespace Neusoft.SOC.Local.RADT.ZhuHai.ZDWY.Controls
{
    partial class ucCompare
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
            this.cmbDoctor = new Neusoft.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lblDoctor = new Neusoft.FrameWork.WinForms.Controls.NeuLabel();
            this.neuGroupBox2 = new Neusoft.FrameWork.WinForms.Controls.NeuGroupBox();
            this.textinformation = new System.Windows.Forms.TextBox();
            this.cancel = new System.Windows.Forms.Button();
            this.Compare = new System.Windows.Forms.Button();
            this.readkey = new System.Windows.Forms.Button();
            this.textReadKey = new System.Windows.Forms.TextBox();
            this.neuGroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbDoctor
            // 
            this.cmbDoctor.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbDoctor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbDoctor.Font = new System.Drawing.Font("宋体", 9.75F);
            this.cmbDoctor.FormattingEnabled = true;
            this.cmbDoctor.IsEnter2Tab = false;
            this.cmbDoctor.IsFlat = false;
            this.cmbDoctor.IsLike = true;
            this.cmbDoctor.IsListOnly = false;
            this.cmbDoctor.IsPopForm = true;
            this.cmbDoctor.IsShowCustomerList = false;
            this.cmbDoctor.IsShowID = false;
            this.cmbDoctor.IsShowIDAndName = false;
            this.cmbDoctor.Location = new System.Drawing.Point(85, 34);
            this.cmbDoctor.Name = "cmbDoctor";
            this.cmbDoctor.ShowCustomerList = false;
            this.cmbDoctor.ShowID = false;
            this.cmbDoctor.Size = new System.Drawing.Size(120, 21);
            this.cmbDoctor.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbDoctor.TabIndex = 30;
            this.cmbDoctor.Tag = "";
            this.cmbDoctor.ToolBarUse = false;
            // 
            // lblDoctor
            // 
            this.lblDoctor.AutoSize = true;
            this.lblDoctor.Font = new System.Drawing.Font("宋体", 9.75F);
            this.lblDoctor.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblDoctor.Location = new System.Drawing.Point(12, 37);
            this.lblDoctor.Name = "lblDoctor";
            this.lblDoctor.Size = new System.Drawing.Size(40, 13);
            this.lblDoctor.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblDoctor.TabIndex = 0;
            this.lblDoctor.Text = "人员:";
            // 
            // neuGroupBox2
            // 
            this.neuGroupBox2.Controls.Add(this.textinformation);
            this.neuGroupBox2.Controls.Add(this.cancel);
            this.neuGroupBox2.Controls.Add(this.Compare);
            this.neuGroupBox2.Controls.Add(this.readkey);
            this.neuGroupBox2.Controls.Add(this.textReadKey);
            this.neuGroupBox2.Controls.Add(this.lblDoctor);
            this.neuGroupBox2.Controls.Add(this.cmbDoctor);
            this.neuGroupBox2.Location = new System.Drawing.Point(11, 18);
            this.neuGroupBox2.Name = "neuGroupBox2";
            this.neuGroupBox2.Size = new System.Drawing.Size(697, 166);
            this.neuGroupBox2.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox2.TabIndex = 102;
            this.neuGroupBox2.TabStop = false;
            this.neuGroupBox2.Text = "登记信息";
            // 
            // textinformation
            // 
            this.textinformation.Location = new System.Drawing.Point(236, 63);
            this.textinformation.Name = "textinformation";
            this.textinformation.Size = new System.Drawing.Size(120, 21);
            this.textinformation.TabIndex = 35;
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(346, 121);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 34;
            this.cancel.Text = "作废";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // Compare
            // 
            this.Compare.Location = new System.Drawing.Point(103, 122);
            this.Compare.Name = "Compare";
            this.Compare.Size = new System.Drawing.Size(75, 23);
            this.Compare.TabIndex = 33;
            this.Compare.Text = "对照";
            this.Compare.UseVisualStyleBackColor = true;
            this.Compare.Click += new System.EventHandler(this.Compare_Click);
            // 
            // readkey
            // 
            this.readkey.Location = new System.Drawing.Point(4, 62);
            this.readkey.Name = "readkey";
            this.readkey.Size = new System.Drawing.Size(75, 23);
            this.readkey.TabIndex = 32;
            this.readkey.Text = "读KEY";
            this.readkey.UseVisualStyleBackColor = true;
            this.readkey.Click += new System.EventHandler(this.readkey_Click);
            // 
            // textReadKey
            // 
            this.textReadKey.Location = new System.Drawing.Point(85, 63);
            this.textReadKey.Name = "textReadKey";
            this.textReadKey.Size = new System.Drawing.Size(120, 21);
            this.textReadKey.TabIndex = 31;
            // 
            // ucCompare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuGroupBox2);
            this.Name = "ucCompare";
            this.Size = new System.Drawing.Size(958, 544);
            this.Load += new System.EventHandler(this.ucCompare_Load);
            this.neuGroupBox2.ResumeLayout(false);
            this.neuGroupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Neusoft.FrameWork.WinForms.Controls.NeuComboBox cmbDoctor;
        private Neusoft.FrameWork.WinForms.Controls.NeuLabel lblDoctor;
        private Neusoft.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox2;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Button Compare;
        private System.Windows.Forms.Button readkey;
        private System.Windows.Forms.TextBox textReadKey;
        private System.Windows.Forms.TextBox textinformation;
    }
}
