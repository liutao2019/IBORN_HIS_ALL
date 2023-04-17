namespace FS.HISFC.Components.Speciment.Print
{
    partial class ucOperCard
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblBarCode = new System.Windows.Forms.Label();
            this.lbl39 = new System.Windows.Forms.Label();
            this.lblDiag = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblInHosNum = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lblBarCode);
            this.panel1.Controls.Add(this.lbl39);
            this.panel1.Controls.Add(this.lblDiag);
            this.panel1.Controls.Add(this.lblName);
            this.panel1.Controls.Add(this.lblInHosNum);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(198, 106);
            this.panel1.TabIndex = 0;
            // 
            // lblBarCode
            // 
            this.lblBarCode.AutoSize = true;
            this.lblBarCode.Location = new System.Drawing.Point(49, 20);
            this.lblBarCode.Name = "lblBarCode";
            this.lblBarCode.Size = new System.Drawing.Size(41, 12);
            this.lblBarCode.TabIndex = 11;
            this.lblBarCode.Text = "label3";
            // 
            // lbl39
            // 
            this.lbl39.AutoSize = true;
            this.lbl39.Font = new System.Drawing.Font("3 of 9 Barcode", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl39.Location = new System.Drawing.Point(16, 3);
            this.lbl39.Name = "lbl39";
            this.lbl39.Size = new System.Drawing.Size(73, 14);
            this.lbl39.TabIndex = 10;
            this.lbl39.Text = "label1";
            // 
            // lblDiag
            // 
            this.lblDiag.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDiag.Location = new System.Drawing.Point(47, 79);
            this.lblDiag.Name = "lblDiag";
            this.lblDiag.Size = new System.Drawing.Size(148, 37);
            this.lblDiag.TabIndex = 9;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(48, 58);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(41, 12);
            this.lblName.TabIndex = 6;
            this.lblName.Text = "label7";
            // 
            // lblInHosNum
            // 
            this.lblInHosNum.AutoSize = true;
            this.lblInHosNum.Location = new System.Drawing.Point(84, 37);
            this.lblInHosNum.Name = "lblInHosNum";
            this.lblInHosNum.Size = new System.Drawing.Size(41, 12);
            this.lblInHosNum.TabIndex = 5;
            this.lblInHosNum.Text = "label6";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 79);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "诊断:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "姓名:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "住院流水号:";
            // 
            // ucOperCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "ucOperCard";
            this.Size = new System.Drawing.Size(198, 106);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDiag;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblInHosNum;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbl39;
        private System.Windows.Forms.Label lblBarCode;
    }
}
