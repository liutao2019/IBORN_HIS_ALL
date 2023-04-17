namespace FS.HISFC.Components.Registration.NewRegister
{
    partial class frmCouponCost
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
            this.plTop = new System.Windows.Forms.Panel();
            this.btnCard = new System.Windows.Forms.Button();
            this.tbMoeny = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lbName = new System.Windows.Forms.Label();
            this.tbMedcineNO = new System.Windows.Forms.TextBox();
            this.lbMedicine = new System.Windows.Forms.Label();
            this.plBottom = new System.Windows.Forms.Panel();
            this.lbCost = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.plCoupon = new System.Windows.Forms.Panel();
            this.tbCostMoney = new FS.FrameWork.WinForms.Controls.NeuNumericTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.lbCouponMoney = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbCouponAmount = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.plTop.SuspendLayout();
            this.plBottom.SuspendLayout();
            this.plCoupon.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // plTop
            // 
            this.plTop.Controls.Add(this.btnCard);
            this.plTop.Controls.Add(this.tbMoeny);
            this.plTop.Controls.Add(this.label1);
            this.plTop.Controls.Add(this.tbName);
            this.plTop.Controls.Add(this.lbName);
            this.plTop.Controls.Add(this.tbMedcineNO);
            this.plTop.Controls.Add(this.lbMedicine);
            this.plTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.plTop.Location = new System.Drawing.Point(0, 0);
            this.plTop.Name = "plTop";
            this.plTop.Size = new System.Drawing.Size(593, 44);
            this.plTop.TabIndex = 0;
            // 
            // btnCard
            // 
            this.btnCard.Location = new System.Drawing.Point(480, 12);
            this.btnCard.Name = "btnCard";
            this.btnCard.Size = new System.Drawing.Size(75, 23);
            this.btnCard.TabIndex = 8;
            this.btnCard.Text = "刷卡";
            this.btnCard.UseVisualStyleBackColor = true;
            // 
            // tbMoeny
            // 
            this.tbMoeny.Location = new System.Drawing.Point(370, 12);
            this.tbMoeny.Name = "tbMoeny";
            this.tbMoeny.ReadOnly = true;
            this.tbMoeny.Size = new System.Drawing.Size(90, 21);
            this.tbMoeny.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(293, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "待支付金额:";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(197, 12);
            this.tbName.Name = "tbName";
            this.tbName.ReadOnly = true;
            this.tbName.Size = new System.Drawing.Size(90, 21);
            this.tbName.TabIndex = 3;
            this.tbName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbName_KeyDown);
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Location = new System.Drawing.Point(156, 15);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(35, 12);
            this.lbName.TabIndex = 2;
            this.lbName.Text = "姓名:";
            // 
            // tbMedcineNO
            // 
            this.tbMedcineNO.Location = new System.Drawing.Point(60, 12);
            this.tbMedcineNO.Name = "tbMedcineNO";
            this.tbMedcineNO.ReadOnly = true;
            this.tbMedcineNO.Size = new System.Drawing.Size(90, 21);
            this.tbMedcineNO.TabIndex = 1;
            // 
            // lbMedicine
            // 
            this.lbMedicine.AutoSize = true;
            this.lbMedicine.Location = new System.Drawing.Point(7, 15);
            this.lbMedicine.Name = "lbMedicine";
            this.lbMedicine.Size = new System.Drawing.Size(47, 12);
            this.lbMedicine.TabIndex = 0;
            this.lbMedicine.Text = "病历号:";
            // 
            // plBottom
            // 
            this.plBottom.Controls.Add(this.lbCost);
            this.plBottom.Controls.Add(this.btnCancel);
            this.plBottom.Controls.Add(this.btnOK);
            this.plBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plBottom.Location = new System.Drawing.Point(0, 209);
            this.plBottom.Name = "plBottom";
            this.plBottom.Size = new System.Drawing.Size(593, 40);
            this.plBottom.TabIndex = 1;
            // 
            // lbCost
            // 
            this.lbCost.AutoSize = true;
            this.lbCost.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCost.ForeColor = System.Drawing.Color.Red;
            this.lbCost.Location = new System.Drawing.Point(12, 8);
            this.lbCost.Name = "lbCost";
            this.lbCost.Size = new System.Drawing.Size(80, 25);
            this.lbCost.TabIndex = 6;
            this.lbCost.Text = "￥0.00";
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(493, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.ForeColor = System.Drawing.Color.Blue;
            this.btnOK.Location = new System.Drawing.Point(412, 6);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 30);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // plCoupon
            // 
            this.plCoupon.Controls.Add(this.tbCostMoney);
            this.plCoupon.Controls.Add(this.panel1);
            this.plCoupon.Controls.Add(this.label6);
            this.plCoupon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plCoupon.Location = new System.Drawing.Point(0, 44);
            this.plCoupon.Name = "plCoupon";
            this.plCoupon.Size = new System.Drawing.Size(593, 165);
            this.plCoupon.TabIndex = 2;
            // 
            // tbCostMoney
            // 
            this.tbCostMoney.AllowNegative = false;
            this.tbCostMoney.IsAutoRemoveDecimalZero = false;
            this.tbCostMoney.IsEnter2Tab = false;
            this.tbCostMoney.Location = new System.Drawing.Point(148, 83);
            this.tbCostMoney.Name = "tbCostMoney";
            this.tbCostMoney.NumericPrecision = 10;
            this.tbCostMoney.NumericScaleOnFocus = 2;
            this.tbCostMoney.NumericScaleOnLostFocus = 2;
            this.tbCostMoney.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.tbCostMoney.SetRange = new System.Drawing.Size(-1, -1);
            this.tbCostMoney.Size = new System.Drawing.Size(100, 21);
            this.tbCostMoney.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbCostMoney.TabIndex = 58;
            this.tbCostMoney.Text = "0.00";
            this.tbCostMoney.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbCostMoney.UseGroupSeperator = true;
            this.tbCostMoney.ZeroIsValid = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.lbCouponMoney);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lbCouponAmount);
            this.panel1.Controls.Add(this.lblInfo);
            this.panel1.Location = new System.Drawing.Point(17, 32);
            this.panel1.Margin = new System.Windows.Forms.Padding(6);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(555, 23);
            this.panel1.TabIndex = 57;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(165, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 19;
            this.label5.Text = "元。";
            // 
            // lbCouponMoney
            // 
            this.lbCouponMoney.AutoSize = true;
            this.lbCouponMoney.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbCouponMoney.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCouponMoney.ForeColor = System.Drawing.Color.Red;
            this.lbCouponMoney.Location = new System.Drawing.Point(149, 3);
            this.lbCouponMoney.Name = "lbCouponMoney";
            this.lbCouponMoney.Size = new System.Drawing.Size(16, 14);
            this.lbCouponMoney.TabIndex = 17;
            this.lbCouponMoney.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(84, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "，可抵扣：";
            // 
            // lbCouponAmount
            // 
            this.lbCouponAmount.AutoSize = true;
            this.lbCouponAmount.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbCouponAmount.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCouponAmount.ForeColor = System.Drawing.Color.Red;
            this.lbCouponAmount.Location = new System.Drawing.Point(68, 3);
            this.lbCouponAmount.Name = "lbCouponAmount";
            this.lbCouponAmount.Size = new System.Drawing.Size(16, 14);
            this.lbCouponAmount.TabIndex = 12;
            this.lbCouponAmount.Text = "0";
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblInfo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInfo.Location = new System.Drawing.Point(3, 3);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(65, 12);
            this.lblInfo.TabIndex = 7;
            this.lblInfo.Text = "可用积分：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "请输入抵用积分数量：";
            // 
            // frmCouponCost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 249);
            this.Controls.Add(this.plCoupon);
            this.Controls.Add(this.plBottom);
            this.Controls.Add(this.plTop);
            this.Name = "frmCouponCost";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "账户消费";
            this.plTop.ResumeLayout(false);
            this.plTop.PerformLayout();
            this.plBottom.ResumeLayout(false);
            this.plBottom.PerformLayout();
            this.plCoupon.ResumeLayout(false);
            this.plCoupon.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel plTop;
        private System.Windows.Forms.Panel plBottom;
        private System.Windows.Forms.Panel plCoupon;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.TextBox tbMedcineNO;
        private System.Windows.Forms.Label lbMedicine;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox tbMoeny;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbCost;
        private System.Windows.Forms.Button btnCard;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbCouponMoney;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbCouponAmount;
        private FS.FrameWork.WinForms.Controls.NeuNumericTextBox tbCostMoney;
    }
}