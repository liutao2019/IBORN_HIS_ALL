namespace FoShanSporadicUpload.Control
{
    partial class frmChangePassWord
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
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.txtOldPassWord = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtNewPassWord = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtConfirmPassWord = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.btSave = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btCancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.btCancel);
            this.neuPanel1.Controls.Add(this.btSave);
            this.neuPanel1.Controls.Add(this.txtConfirmPassWord);
            this.neuPanel1.Controls.Add(this.neuLabel2);
            this.neuPanel1.Controls.Add(this.txtNewPassWord);
            this.neuPanel1.Controls.Add(this.neuLabel1);
            this.neuPanel1.Controls.Add(this.txtOldPassWord);
            this.neuPanel1.Controls.Add(this.neuLabel3);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(331, 169);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // txtOldPassWord
            // 
            this.txtOldPassWord.IsEnter2Tab = false;
            this.txtOldPassWord.Location = new System.Drawing.Point(82, 22);
            this.txtOldPassWord.Name = "txtOldPassWord";
            this.txtOldPassWord.PasswordChar = '*';
            this.txtOldPassWord.Size = new System.Drawing.Size(128, 21);
            this.txtOldPassWord.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtOldPassWord.TabIndex = 7;
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(29, 25);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(53, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 6;
            this.neuLabel3.Text = "原密码：";
            // 
            // txtNewPassWord
            // 
            this.txtNewPassWord.IsEnter2Tab = false;
            this.txtNewPassWord.Location = new System.Drawing.Point(82, 57);
            this.txtNewPassWord.Name = "txtNewPassWord";
            this.txtNewPassWord.PasswordChar = '*';
            this.txtNewPassWord.Size = new System.Drawing.Size(128, 21);
            this.txtNewPassWord.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtNewPassWord.TabIndex = 9;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(29, 60);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(53, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 8;
            this.neuLabel1.Text = "新密码：";
            // 
            // txtConfirmPassWord
            // 
            this.txtConfirmPassWord.IsEnter2Tab = false;
            this.txtConfirmPassWord.Location = new System.Drawing.Point(82, 92);
            this.txtConfirmPassWord.Name = "txtConfirmPassWord";
            this.txtConfirmPassWord.PasswordChar = '*';
            this.txtConfirmPassWord.Size = new System.Drawing.Size(128, 21);
            this.txtConfirmPassWord.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtConfirmPassWord.TabIndex = 11;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(17, 95);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(65, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 10;
            this.neuLabel2.Text = "确认密码：";
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(53, 134);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(75, 23);
            this.btSave.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btSave.TabIndex = 12;
            this.btSave.Text = "确定";
            this.btSave.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(168, 134);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btCancel.TabIndex = 13;
            this.btCancel.Text = "取消";
            this.btCancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // frmChangePassWord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(331, 169);
            this.Controls.Add(this.neuPanel1);
            this.KeyPreview = true;
            this.Name = "frmChangePassWord";
            this.Text = "修改密码";
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtNewPassWord;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtOldPassWord;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtConfirmPassWord;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuButton btCancel;
        private FS.FrameWork.WinForms.Controls.NeuButton btSave;
    }
}