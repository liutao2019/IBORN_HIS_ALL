namespace FS.HISFC.Components.Common.Forms
{
    partial class frmWriteMCardNO
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
            this.button1 = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMarkNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(235, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 24);
            this.button1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.button1.TabIndex = 99;
            this.button1.Text = "确   定";
            this.button1.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(2, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 14);
            this.label1.TabIndex = 99;
            this.label1.Text = "请输入卡面号：";
            // 
            // txtMarkNo
            // 
            this.txtMarkNo.IsEnter2Tab = false;
            this.txtMarkNo.Location = new System.Drawing.Point(101, 14);
            this.txtMarkNo.Name = "txtMarkNo";
            this.txtMarkNo.Size = new System.Drawing.Size(118, 21);
            this.txtMarkNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtMarkNo.TabIndex = 1;
            // 
            // frmWriteMCardNO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 62);
            this.Controls.Add(this.txtMarkNo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "frmWriteMCardNO";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "输入卡面号";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuButton button1;
        private System.Windows.Forms.Label label1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtMarkNo;


    }
}