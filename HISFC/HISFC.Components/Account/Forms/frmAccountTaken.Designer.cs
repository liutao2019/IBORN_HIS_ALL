namespace FS.HISFC.Components.Account.Forms
{
    partial class frmAccountTaken
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
            this.components = new System.ComponentModel.Container();
            this.txtpay = new FS.FrameWork.WinForms.Controls.NeuNumericTextBox();
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbPayType = new FS.HISFC.Components.Common.Controls.cmbPayType(this.components);
            this.SuspendLayout();
            // 
            // txtpay
            // 
            this.txtpay.AllowNegative = false;
            this.txtpay.BackColor = System.Drawing.Color.White;
            this.txtpay.IsAutoRemoveDecimalZero = false;
            this.txtpay.IsEnter2Tab = false;
            this.txtpay.Location = new System.Drawing.Point(105, 27);
            this.txtpay.Name = "txtpay";
            this.txtpay.NumericPrecision = 11;
            this.txtpay.NumericScaleOnFocus = 2;
            this.txtpay.NumericScaleOnLostFocus = 2;
            this.txtpay.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtpay.SetRange = new System.Drawing.Size(-1, -1);
            this.txtpay.Size = new System.Drawing.Size(124, 21);
            this.txtpay.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtpay.TabIndex = 16;
            this.txtpay.Text = "0.00";
            this.txtpay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtpay.UseGroupSeperator = true;
            this.txtpay.ZeroIsValid = true;
            this.txtpay.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpay_KeyDown);
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel6.Location = new System.Drawing.Point(35, 31);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(65, 12);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 17;
            this.neuLabel6.Text = "取现金额：";
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(33, 82);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 23);
            this.cmdOK.TabIndex = 18;
            this.cmdOK.Text = "确定";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(147, 82);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 18;
            this.cmdCancel.Text = "取消";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(35, 59);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(65, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 20;
            this.neuLabel1.Text = "支付方式：";
            // 
            // cmbPayType
            // 
            this.cmbPayType.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbPayType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbPayType.FormattingEnabled = true;
            this.cmbPayType.IsEnter2Tab = false;
            this.cmbPayType.IsFlat = false;
            this.cmbPayType.IsLike = true;
            this.cmbPayType.IsListOnly = false;
            this.cmbPayType.IsPopForm = true;
            this.cmbPayType.IsShowCustomerList = false;
            this.cmbPayType.IsShowID = false;
            this.cmbPayType.IsShowIDAndName = false;
            this.cmbPayType.Location = new System.Drawing.Point(105, 56);
            this.cmbPayType.Name = "cmbPayType";
            this.cmbPayType.Pop = true;
            this.cmbPayType.ShowCustomerList = false;
            this.cmbPayType.ShowID = false;
            this.cmbPayType.Size = new System.Drawing.Size(124, 20);
            this.cmbPayType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbPayType.TabIndex = 21;
            this.cmbPayType.Tag = "";
            this.cmbPayType.ToolBarUse = false;
            this.cmbPayType.WorkUnit = "";
            // 
            // frmAccountTaken
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 122);
            this.Controls.Add(this.cmbPayType);
            this.Controls.Add(this.neuLabel1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.neuLabel6);
            this.Controls.Add(this.txtpay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAccountTaken";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "取现金额";
            this.Load += new System.EventHandler(this.frmAccountTaken_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuNumericTextBox txtpay;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.HISFC.Components.Common.Controls.cmbPayType cmbPayType;
    }
}