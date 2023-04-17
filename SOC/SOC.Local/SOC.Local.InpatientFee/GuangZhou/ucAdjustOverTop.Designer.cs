namespace FS.SOC.Local.InpatientFee.GuangZhou
{
    partial class ucAdjustOverTop
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtPatient = new FS.HISFC.Components.Common.Controls.ucQueryInpatientNo();
            this.txtOverTop = new System.Windows.Forms.TextBox();
            this.txtTot = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtPact = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtPatient
            // 
            this.txtPatient.DefaultInputType = 0;
            this.txtPatient.InputType = 0;
            this.txtPatient.Location = new System.Drawing.Point(8, 8);
            this.txtPatient.Name = "txtPatient";
            this.txtPatient.PatientInState = "ALL";
            this.txtPatient.ShowState = FS.HISFC.Components.Common.Controls.enuShowState.All;
            this.txtPatient.Size = new System.Drawing.Size(168, 32);
            this.txtPatient.TabIndex = 3;
            this.txtPatient.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(this.textBox1_myEvent);
            // 
            // txtOverTop
            // 
            this.txtOverTop.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtOverTop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtOverTop.Location = new System.Drawing.Point(304, 16);
            this.txtOverTop.Name = "txtOverTop";
            this.txtOverTop.Size = new System.Drawing.Size(100, 21);
            this.txtOverTop.TabIndex = 1;
            // 
            // txtTot
            // 
            this.txtTot.Location = new System.Drawing.Point(504, 19);
            this.txtTot.Name = "txtTot";
            this.txtTot.Size = new System.Drawing.Size(100, 21);
            this.txtTot.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(216, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 32);
            this.label1.TabIndex = 4;
            this.label1.Text = "日限额超标";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(416, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 23);
            this.label2.TabIndex = 5;
            this.label2.Text = "日限额总额";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.Honeydew;
            this.richTextBox1.Location = new System.Drawing.Point(0, 72);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(680, 136);
            this.richTextBox1.TabIndex = 6;
            this.richTextBox1.Text = "";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(624, 16);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(48, 23);
            this.btnOk.TabIndex = 7;
            this.btnOk.Text = "确定";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(-8, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 18);
            this.label3.TabIndex = 8;
            this.label3.Text = "姓名";
            this.label3.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(232, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 18);
            this.label4.TabIndex = 9;
            this.label4.Text = "合同单位";
            this.label4.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtName.Location = new System.Drawing.Point(64, 48);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(100, 21);
            this.txtName.TabIndex = 10;
            // 
            // txtPact
            // 
            this.txtPact.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtPact.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtPact.Location = new System.Drawing.Point(304, 48);
            this.txtPact.Name = "txtPact";
            this.txtPact.ReadOnly = true;
            this.txtPact.Size = new System.Drawing.Size(100, 21);
            this.txtPact.TabIndex = 11;
            // 
            // ucAdjustOverTop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtPact);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTot);
            this.Controls.Add(this.txtOverTop);
            this.Controls.Add(this.txtPatient);
            this.Name = "ucAdjustOverTop";
            this.Size = new System.Drawing.Size(688, 216);
            this.Load += new System.EventHandler(this.ucAdjustOverTop_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        //private FS.Common.Controls.ucQueryInpatientNo txtPatient;
        private FS.HISFC.Components.Common.Controls.ucQueryInpatientNo txtPatient;
        private System.Windows.Forms.TextBox txtOverTop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox txtTot;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtPact;
    }
}
