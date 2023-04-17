namespace FS.HISFC.Components.Account.Forms
{
    partial class frmCouponRule
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
            this.txtMoney = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtCoupon = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCouponRatio = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(251, 57);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 24);
            this.button1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.button1.TabIndex = 2;
            this.button1.Text = "确   定";
            this.button1.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtMoney
            // 
            this.txtMoney.IsEnter2Tab = false;
            this.txtMoney.Location = new System.Drawing.Point(47, 12);
            this.txtMoney.Name = "txtMoney";
            this.txtMoney.Size = new System.Drawing.Size(67, 21);
            this.txtMoney.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtMoney.TabIndex = 8;
            this.txtMoney.Text = "1.00";
            // 
            // txtCoupon
            // 
            this.txtCoupon.IsEnter2Tab = false;
            this.txtCoupon.Location = new System.Drawing.Point(218, 12);
            this.txtCoupon.Name = "txtCoupon";
            this.txtCoupon.Size = new System.Drawing.Size(67, 21);
            this.txtCoupon.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCoupon.TabIndex = 9;
            this.txtCoupon.Text = "1.00";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(120, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 14);
            this.label2.TabIndex = 10;
            this.label2.Text = "元，兑换积分";
            // 
            // lblCouponRatio
            // 
            this.lblCouponRatio.AutoSize = true;
            this.lblCouponRatio.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCouponRatio.ForeColor = System.Drawing.Color.Red;
            this.lblCouponRatio.Location = new System.Drawing.Point(44, 57);
            this.lblCouponRatio.Name = "lblCouponRatio";
            this.lblCouponRatio.Size = new System.Drawing.Size(83, 14);
            this.lblCouponRatio.TabIndex = 11;
            this.lblCouponRatio.Text = "积分比： 1";
            // 
            // frmCouponRule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 103);
            this.Controls.Add(this.lblCouponRatio);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCoupon);
            this.Controls.Add(this.txtMoney);
            this.Controls.Add(this.button1);
            this.Name = "frmCouponRule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "积分规则";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuButton button1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtMoney;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtCoupon;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCouponRatio;


    }
}