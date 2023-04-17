namespace DiseasePay
{
    partial class frmInsertSIFee
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPatientInfo = new System.Windows.Forms.RichTextBox();
            this.txtPatientNO = new System.Windows.Forms.TextBox();
            this.txtInTimes = new System.Windows.Forms.TextBox();
            this.txtMNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btQuery = new System.Windows.Forms.Button();
            this.btSave = new System.Windows.Forms.Button();
            this.btClear = new System.Windows.Forms.Button();
            this.btDel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "住院号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(208, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "住院次数：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "结算单号：";
            // 
            // lblPatientInfo
            // 
            this.lblPatientInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblPatientInfo.Enabled = false;
            this.lblPatientInfo.Location = new System.Drawing.Point(44, 153);
            this.lblPatientInfo.Name = "lblPatientInfo";
            this.lblPatientInfo.Size = new System.Drawing.Size(384, 165);
            this.lblPatientInfo.TabIndex = 3;
            this.lblPatientInfo.Text = "患者基本信息：";
            // 
            // txtPatientNO
            // 
            this.txtPatientNO.Location = new System.Drawing.Point(91, 18);
            this.txtPatientNO.Name = "txtPatientNO";
            this.txtPatientNO.Size = new System.Drawing.Size(100, 21);
            this.txtPatientNO.TabIndex = 4;
            // 
            // txtInTimes
            // 
            this.txtInTimes.Location = new System.Drawing.Point(268, 19);
            this.txtInTimes.Name = "txtInTimes";
            this.txtInTimes.Size = new System.Drawing.Size(100, 21);
            this.txtInTimes.TabIndex = 5;
            // 
            // txtMNo
            // 
            this.txtMNo.Location = new System.Drawing.Point(102, 103);
            this.txtMNo.Name = "txtMNo";
            this.txtMNo.Size = new System.Drawing.Size(160, 21);
            this.txtMNo.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(42, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "请输入结算单号后确定";
            // 
            // btQuery
            // 
            this.btQuery.Location = new System.Drawing.Point(388, 18);
            this.btQuery.Name = "btQuery";
            this.btQuery.Size = new System.Drawing.Size(75, 23);
            this.btQuery.TabIndex = 8;
            this.btQuery.Text = "查询";
            this.btQuery.UseVisualStyleBackColor = true;
            this.btQuery.Click += new System.EventHandler(this.btQuery_Click);
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(279, 101);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(75, 23);
            this.btSave.TabIndex = 9;
            this.btSave.Text = "确定";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btClear
            // 
            this.btClear.Location = new System.Drawing.Point(388, 47);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(75, 23);
            this.btClear.TabIndex = 10;
            this.btClear.Text = "清空";
            this.btClear.UseVisualStyleBackColor = true;
            this.btClear.Click += new System.EventHandler(this.btClear_Click);
            // 
            // btDel
            // 
            this.btDel.Location = new System.Drawing.Point(307, 47);
            this.btDel.Name = "btDel";
            this.btDel.Size = new System.Drawing.Size(75, 23);
            this.btDel.TabIndex = 11;
            this.btDel.Text = "删除";
            this.btDel.UseVisualStyleBackColor = true;
            this.btDel.Click += new System.EventHandler(this.btDel_Click);
            // 
            // frmInsertSIFee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 330);
            this.Controls.Add(this.btDel);
            this.Controls.Add(this.btClear);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.btQuery);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtMNo);
            this.Controls.Add(this.txtInTimes);
            this.Controls.Add(this.txtPatientNO);
            this.Controls.Add(this.lblPatientInfo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmInsertSIFee";
            this.Text = "自费患者手工更新医保信息";
            this.Load += new System.EventHandler(this.frmInsertSIFee_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox lblPatientInfo;
        private System.Windows.Forms.TextBox txtPatientNO;
        private System.Windows.Forms.TextBox txtInTimes;
        private System.Windows.Forms.TextBox txtMNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btQuery;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btClear;
        private System.Windows.Forms.Button btDel;
    }
}