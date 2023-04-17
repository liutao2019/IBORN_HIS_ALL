namespace FS.SOC.HISFC.Components.Project
{
    partial class frmLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ntxtName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.ntxtPassword = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nbtLogin = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.nbtCancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.SuspendLayout();
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(22, 21);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(41, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "用户名";
            // 
            // ntxtName
            // 
            this.ntxtName.IsEnter2Tab = true;
            this.ntxtName.Location = new System.Drawing.Point(88, 18);
            this.ntxtName.Name = "ntxtName";
            this.ntxtName.Size = new System.Drawing.Size(114, 21);
            this.ntxtName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ntxtName.TabIndex = 1;
            // 
            // ntxtPassword
            // 
            this.ntxtPassword.IsEnter2Tab = true;
            this.ntxtPassword.Location = new System.Drawing.Point(88, 50);
            this.ntxtPassword.Name = "ntxtPassword";
            this.ntxtPassword.Size = new System.Drawing.Size(114, 21);
            this.ntxtPassword.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ntxtPassword.TabIndex = 3;
            this.ntxtPassword.UseSystemPasswordChar = true;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(22, 53);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(41, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 2;
            this.neuLabel2.Text = "密  码";
            // 
            // nbtLogin
            // 
            this.nbtLogin.Location = new System.Drawing.Point(47, 94);
            this.nbtLogin.Name = "nbtLogin";
            this.nbtLogin.Size = new System.Drawing.Size(75, 23);
            this.nbtLogin.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nbtLogin.TabIndex = 4;
            this.nbtLogin.Text = "登录";
            this.nbtLogin.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.nbtLogin.UseVisualStyleBackColor = true;
            // 
            // nbtCancel
            // 
            this.nbtCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.nbtCancel.Location = new System.Drawing.Point(148, 94);
            this.nbtCancel.Name = "nbtCancel";
            this.nbtCancel.Size = new System.Drawing.Size(75, 23);
            this.nbtCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nbtCancel.TabIndex = 7;
            this.nbtCancel.Text = "取消";
            this.nbtCancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.nbtCancel.UseVisualStyleBackColor = true;
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.nbtCancel;
            this.ClientSize = new System.Drawing.Size(235, 133);
            this.Controls.Add(this.nbtCancel);
            this.Controls.Add(this.nbtLogin);
            this.Controls.Add(this.ntxtPassword);
            this.Controls.Add(this.neuLabel2);
            this.Controls.Add(this.ntxtName);
            this.Controls.Add(this.neuLabel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogin";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox ntxtName;
        private FS.FrameWork.WinForms.Controls.NeuTextBox ntxtPassword;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuButton nbtLogin;
        private FS.FrameWork.WinForms.Controls.NeuButton nbtCancel;
    }
}