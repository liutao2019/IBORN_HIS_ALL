namespace FS.SOC.HISFC.Components.Common.Base
{
    partial class frmCheckPassWord
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
            this.nlbBatchNO = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ntxtEmployNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ntxtPassword = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.nbtOK = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.nbtCancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.SuspendLayout();
            // 
            // nlbBatchNO
            // 
            this.nlbBatchNO.AutoSize = true;
            this.nlbBatchNO.Location = new System.Drawing.Point(48, 36);
            this.nlbBatchNO.Name = "nlbBatchNO";
            this.nlbBatchNO.Size = new System.Drawing.Size(53, 12);
            this.nlbBatchNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbBatchNO.TabIndex = 11;
            this.nlbBatchNO.Text = "工  号：";
            // 
            // ntxtEmployNO
            // 
            this.ntxtEmployNO.IsEnter2Tab = true;
            this.ntxtEmployNO.Location = new System.Drawing.Point(107, 32);
            this.ntxtEmployNO.Name = "ntxtEmployNO";
            this.ntxtEmployNO.Size = new System.Drawing.Size(109, 21);
            this.ntxtEmployNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ntxtEmployNO.TabIndex = 1;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(48, 73);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(53, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 13;
            this.neuLabel1.Text = "密  码：";
            // 
            // ntxtPassword
            // 
            this.ntxtPassword.IsEnter2Tab = true;
            this.ntxtPassword.Location = new System.Drawing.Point(107, 69);
            this.ntxtPassword.Name = "ntxtPassword";
            this.ntxtPassword.PasswordChar = '*';
            this.ntxtPassword.Size = new System.Drawing.Size(109, 21);
            this.ntxtPassword.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ntxtPassword.TabIndex = 2;
            // 
            // nbtOK
            // 
            this.nbtOK.Location = new System.Drawing.Point(67, 127);
            this.nbtOK.Name = "nbtOK";
            this.nbtOK.Size = new System.Drawing.Size(75, 23);
            this.nbtOK.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nbtOK.TabIndex = 3;
            this.nbtOK.Text = "确定";
            this.nbtOK.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.nbtOK.UseVisualStyleBackColor = true;
            // 
            // nbtCancel
            // 
            this.nbtCancel.Location = new System.Drawing.Point(167, 127);
            this.nbtCancel.Name = "nbtCancel";
            this.nbtCancel.Size = new System.Drawing.Size(75, 23);
            this.nbtCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nbtCancel.TabIndex = 4;
            this.nbtCancel.Text = "取消";
            this.nbtCancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.nbtCancel.UseVisualStyleBackColor = true;
            // 
            // frmCheckPassWord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(265, 162);
            this.Controls.Add(this.nbtCancel);
            this.Controls.Add(this.nbtOK);
            this.Controls.Add(this.neuLabel1);
            this.Controls.Add(this.ntxtPassword);
            this.Controls.Add(this.nlbBatchNO);
            this.Controls.Add(this.ntxtEmployNO);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCheckPassWord";
            this.Text = "用户验证";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuLabel nlbBatchNO;
        private FS.FrameWork.WinForms.Controls.NeuTextBox ntxtEmployNO;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox ntxtPassword;
        private FS.FrameWork.WinForms.Controls.NeuButton nbtOK;
        private FS.FrameWork.WinForms.Controls.NeuButton nbtCancel;
    }
}