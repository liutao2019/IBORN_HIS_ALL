namespace FS.HISFC.Components.Order.Forms
{
    partial class frmFirstDayChange
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
            this.txtFirstDay = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.btnOk = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnCancle = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.SuspendLayout();
            // 
            // txtFirstDay
            // 
            this.txtFirstDay.IsEnter2Tab = false;
            this.txtFirstDay.Location = new System.Drawing.Point(113, 38);
            this.txtFirstDay.Name = "txtFirstDay";
            this.txtFirstDay.Size = new System.Drawing.Size(100, 21);
            this.txtFirstDay.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtFirstDay.TabIndex = 0;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(37, 42);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(53, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 1;
            this.neuLabel1.Text = "首日量：";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(37, 94);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "确定(&O)";
            this.btnOk.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancle
            // 
            this.btnCancle.Location = new System.Drawing.Point(144, 94);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(75, 23);
            this.btnCancle.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnCancle.TabIndex = 3;
            this.btnCancle.Text = "取消(&C)";
            this.btnCancle.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnCancle.UseVisualStyleBackColor = true;
            // 
            // frmFirstDayChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(226)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(263, 160);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.neuLabel1);
            this.Controls.Add(this.txtFirstDay);
            this.Name = "frmFirstDayChange";
            this.Text = "修改首日量";
            this.Load += new System.EventHandler(this.frmDCOrder_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuTextBox txtFirstDay;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuButton btnOk;
        private FS.FrameWork.WinForms.Controls.NeuButton btnCancle;
    }
}