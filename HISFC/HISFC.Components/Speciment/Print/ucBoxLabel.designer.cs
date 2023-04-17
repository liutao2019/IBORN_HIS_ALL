namespace FS.HISFC.Components.Speciment.Print
{
    partial class ucBoxLabel
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
            this.lblLabel = new System.Windows.Forms.Label();
            this.lblBarCode = new System.Windows.Forms.Label();
            this.lblLoc = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblSpecNum = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblLabel
            // 
            this.lblLabel.Location = new System.Drawing.Point(8, 14);
            this.lblLabel.Name = "lblLabel";
            this.lblLabel.Size = new System.Drawing.Size(52, 45);
            this.lblLabel.TabIndex = 52;
            this.lblLabel.Text = "1";
            // 
            // lblBarCode
            // 
            this.lblBarCode.AutoSize = true;
            this.lblBarCode.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBarCode.Location = new System.Drawing.Point(64, 14);
            this.lblBarCode.Name = "lblBarCode";
            this.lblBarCode.Size = new System.Drawing.Size(16, 16);
            this.lblBarCode.TabIndex = 51;
            this.lblBarCode.Text = "1";
            // 
            // lblLoc
            // 
            this.lblLoc.AutoSize = true;
            this.lblLoc.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLoc.Location = new System.Drawing.Point(64, 43);
            this.lblLoc.Name = "lblLoc";
            this.lblLoc.Size = new System.Drawing.Size(17, 16);
            this.lblLoc.TabIndex = 50;
            this.lblLoc.Text = "1";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(325, 17);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(65, 12);
            this.lblDate.TabIndex = 54;
            this.lblDate.Text = "2010-02-25";
            // 
            // lblSpecNum
            // 
            this.lblSpecNum.AutoSize = true;
            this.lblSpecNum.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSpecNum.Location = new System.Drawing.Point(261, 14);
            this.lblSpecNum.Name = "lblSpecNum";
            this.lblSpecNum.Size = new System.Drawing.Size(56, 16);
            this.lblSpecNum.TabIndex = 56;
            this.lblSpecNum.Text = "003487";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(189, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 16);
            this.label4.TabIndex = 55;
            this.label4.Text = "标本号:";
            // 
            // ucBoxLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblSpecNum);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblLabel);
            this.Controls.Add(this.lblBarCode);
            this.Controls.Add(this.lblLoc);
            this.Name = "ucBoxLabel";
            this.Size = new System.Drawing.Size(411, 95);
            this.Load += new System.EventHandler(this.ucBoxLabel_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label lblLabel;
        private System.Windows.Forms.Label lblBarCode;
        private System.Windows.Forms.Label lblLoc;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblSpecNum;
        private System.Windows.Forms.Label label4;
    }
}
