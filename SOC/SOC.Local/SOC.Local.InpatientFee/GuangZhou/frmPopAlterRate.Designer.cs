namespace FS.SOC.Local.InpatientFee.GuangZhou
{
    partial class frmPopAlterRate
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtItem = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTot = new FS.FrameWork.WinForms.Controls.ValidatedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.txtPubCost = new FS.FrameWork.WinForms.Controls.ValidatedTextBox();
            this.txtPayCost = new FS.FrameWork.WinForms.Controls.ValidatedTextBox();
            this.txtOwnCost = new FS.FrameWork.WinForms.Controls.ValidatedTextBox();
            this.txtPayRate = new FS.FrameWork.WinForms.Controls.ValidatedTextBox();
            this.txtSelfRate = new FS.FrameWork.WinForms.Controls.ValidatedTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnQuit = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.lblMemo = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtRebate = new FS.FrameWork.WinForms.Controls.ValidatedTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtRebateRate = new FS.FrameWork.WinForms.Controls.ValidatedTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 50;
            this.label1.Text = "项目名称";
            // 
            // txtItem
            // 
            this.txtItem.BackColor = System.Drawing.Color.White;
            this.txtItem.Location = new System.Drawing.Point(68, 20);
            this.txtItem.Name = "txtItem";
            this.txtItem.ReadOnly = true;
            this.txtItem.Size = new System.Drawing.Size(173, 21);
            this.txtItem.TabIndex = 100;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(15, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 100;
            this.label2.Text = "自费比例";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(15, 193);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "项目金额";
            // 
            // txtTot
            // 
            this.txtTot.AllowNegative = false;
            this.txtTot.AutoPadRightZero = true;
            this.txtTot.BackColor = System.Drawing.Color.White;
            this.txtTot.Location = new System.Drawing.Point(77, 189);
            this.txtTot.MaxDigits = 2;
            this.txtTot.Name = "txtTot";
            this.txtTot.ReadOnly = true;
            this.txtTot.Size = new System.Drawing.Size(145, 21);
            this.txtTot.TabIndex = 4;
            this.txtTot.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTot.WillShowError = false;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(292, 195);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "记帐金额";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(292, 157);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 16);
            this.label5.TabIndex = 6;
            this.label5.Text = "自付金额";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(292, 121);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 16);
            this.label6.TabIndex = 7;
            this.label6.Text = "自费金额";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(15, 159);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 16);
            this.label7.TabIndex = 8;
            this.label7.Text = "自付比例";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(254, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 16);
            this.label8.TabIndex = 90;
            this.label8.Text = "数 量";
            // 
            // txtQty
            // 
            this.txtQty.BackColor = System.Drawing.Color.White;
            this.txtQty.Location = new System.Drawing.Point(298, 19);
            this.txtQty.Name = "txtQty";
            this.txtQty.ReadOnly = true;
            this.txtQty.Size = new System.Drawing.Size(86, 21);
            this.txtQty.TabIndex = 100;
            this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPubCost
            // 
            this.txtPubCost.AllowNegative = true;
            this.txtPubCost.AutoPadRightZero = false;
            this.txtPubCost.BackColor = System.Drawing.Color.White;
            this.txtPubCost.Location = new System.Drawing.Point(362, 187);
            this.txtPubCost.MaxDigits = 4;
            this.txtPubCost.Name = "txtPubCost";
            this.txtPubCost.ReadOnly = true;
            this.txtPubCost.Size = new System.Drawing.Size(127, 21);
            this.txtPubCost.TabIndex = 11;
            this.txtPubCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPubCost.WillShowError = false;
            // 
            // txtPayCost
            // 
            this.txtPayCost.AllowNegative = true;
            this.txtPayCost.AutoPadRightZero = false;
            this.txtPayCost.Location = new System.Drawing.Point(362, 152);
            this.txtPayCost.MaxDigits = 4;
            this.txtPayCost.Name = "txtPayCost";
            this.txtPayCost.Size = new System.Drawing.Size(127, 21);
            this.txtPayCost.TabIndex = 3;
            this.txtPayCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPayCost.WillShowError = false;
            // 
            // txtOwnCost
            // 
            this.txtOwnCost.AllowNegative = false;
            this.txtOwnCost.AutoPadRightZero = true;
            this.txtOwnCost.Location = new System.Drawing.Point(362, 117);
            this.txtOwnCost.MaxDigits = 2;
            this.txtOwnCost.Name = "txtOwnCost";
            this.txtOwnCost.Size = new System.Drawing.Size(127, 21);
            this.txtOwnCost.TabIndex = 1;
            this.txtOwnCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtOwnCost.WillShowError = false;
            // 
            // txtPayRate
            // 
            this.txtPayRate.AllowNegative = true;
            this.txtPayRate.AutoPadRightZero = false;
            this.txtPayRate.Location = new System.Drawing.Point(76, 155);
            this.txtPayRate.MaxDigits = 4;
            this.txtPayRate.Name = "txtPayRate";
            this.txtPayRate.Size = new System.Drawing.Size(145, 21);
            this.txtPayRate.TabIndex = 2;
            this.txtPayRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPayRate.WillShowError = false;
            // 
            // txtSelfRate
            // 
            this.txtSelfRate.AllowNegative = false;
            this.txtSelfRate.AutoPadRightZero = true;
            this.txtSelfRate.Location = new System.Drawing.Point(77, 119);
            this.txtSelfRate.MaxDigits = 2;
            this.txtSelfRate.Name = "txtSelfRate";
            this.txtSelfRate.Size = new System.Drawing.Size(145, 21);
            this.txtSelfRate.TabIndex = 0;
            this.txtSelfRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSelfRate.WillShowError = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(17, 88);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(488, 3);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(302, 311);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(85, 25);
            this.btnOk.TabIndex = 17;
            this.btnOk.Text = "保存(&S)";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.Location = new System.Drawing.Point(404, 311);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(85, 25);
            this.btnQuit.TabIndex = 18;
            this.btnQuit.Text = "退出(&Q)";
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(14, 318);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 16);
            this.label9.TabIndex = 101;
            this.label9.Text = "费用时间";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimePicker1.Enabled = false;
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(77, 313);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(147, 21);
            this.dateTimePicker1.TabIndex = 102;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(391, 23);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(138, 16);
            this.checkBox1.TabIndex = 103;
            this.checkBox1.Text = "特批(审批药/高检费)";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // lblMemo
            // 
            this.lblMemo.AutoSize = true;
            this.lblMemo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMemo.ForeColor = System.Drawing.Color.Red;
            this.lblMemo.Location = new System.Drawing.Point(12, 61);
            this.lblMemo.Name = "lblMemo";
            this.lblMemo.Size = new System.Drawing.Size(49, 14);
            this.lblMemo.TabIndex = 104;
            this.lblMemo.Text = "备注：";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(404, 57);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(85, 25);
            this.button2.TabIndex = 106;
            this.button2.Text = "取消特批";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(302, 57);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 25);
            this.button1.TabIndex = 107;
            this.button1.Text = "确认特批";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtRebate
            // 
            this.txtRebate.AllowNegative = true;
            this.txtRebate.AutoPadRightZero = false;
            this.txtRebate.BackColor = System.Drawing.Color.White;
            this.txtRebate.Location = new System.Drawing.Point(362, 271);
            this.txtRebate.MaxDigits = 4;
            this.txtRebate.Name = "txtRebate";
            this.txtRebate.Size = new System.Drawing.Size(127, 21);
            this.txtRebate.TabIndex = 109;
            this.txtRebate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtRebate.WillShowError = false;
            this.txtRebate.TextChanged += new System.EventHandler(this.validatedTextBox1_TextChanged);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(292, 275);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(54, 16);
            this.label10.TabIndex = 108;
            this.label10.Text = "优惠金额";
            // 
            // txtRebateRate
            // 
            this.txtRebateRate.AllowNegative = true;
            this.txtRebateRate.AutoPadRightZero = false;
            this.txtRebateRate.BackColor = System.Drawing.Color.White;
            this.txtRebateRate.Location = new System.Drawing.Point(77, 270);
            this.txtRebateRate.MaxDigits = 4;
            this.txtRebateRate.Name = "txtRebateRate";
            this.txtRebateRate.Size = new System.Drawing.Size(144, 21);
            this.txtRebateRate.TabIndex = 111;
            this.txtRebateRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtRebateRate.WillShowError = false;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(13, 273);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(54, 16);
            this.label11.TabIndex = 110;
            this.label11.Text = "优惠比例";
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(18, 227);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(488, 3);
            this.groupBox2.TabIndex = 112;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(12, 241);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(259, 14);
            this.label12.TabIndex = 113;
            this.label12.Text = "自费金额打折(在自费的金额上进行优惠)";
            // 
            // txtMemo
            // 
            this.txtMemo.BackColor = System.Drawing.Color.White;
            this.txtMemo.Location = new System.Drawing.Point(67, 60);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.ReadOnly = true;
            this.txtMemo.Size = new System.Drawing.Size(226, 21);
            this.txtMemo.TabIndex = 114;
            // 
            // frmPopAlterRate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 353);
            this.ControlBox = false;
            this.Controls.Add(this.txtMemo);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.txtRebateRate);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtRebate);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.lblMemo);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtSelfRate);
            this.Controls.Add(this.txtPayRate);
            this.Controls.Add(this.txtOwnCost);
            this.Controls.Add(this.txtPayCost);
            this.Controls.Add(this.txtPubCost);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.txtTot);
            this.Controls.Add(this.txtItem);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPopAlterRate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改比例";
            this.Load += new System.EventHandler(this.frmPopAlterRate_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private FS.FrameWork.WinForms.Controls.ValidatedTextBox txtTot;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtQty;
        private FS.FrameWork.WinForms.Controls.ValidatedTextBox txtPubCost;
        //private FS.FrameWork.WinForms.Controls.ValidatedTextBox txtPayCost;
        private FS.FrameWork.WinForms.Controls.ValidatedTextBox txtPayCost;
        private FS.FrameWork.WinForms.Controls.ValidatedTextBox txtOwnCost;
        private FS.FrameWork.WinForms.Controls.ValidatedTextBox txtPayRate;
        private FS.FrameWork.WinForms.Controls.ValidatedTextBox txtSelfRate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label lblMemo;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private FS.FrameWork.WinForms.Controls.ValidatedTextBox txtRebate;
        private System.Windows.Forms.Label label10;
        private FS.FrameWork.WinForms.Controls.ValidatedTextBox txtRebateRate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtMemo;
    }
}