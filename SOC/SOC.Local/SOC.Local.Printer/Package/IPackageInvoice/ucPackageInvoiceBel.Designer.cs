namespace SOC.Local.Printer.Package.IPackageInvoice
{
    partial class ucPackageInvoiceBel
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
            this.lblPayType = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbinvoiceNO = new System.Windows.Forms.Label();
            this.lbCost = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbPackageList = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblPayType
            // 
            this.lblPayType.AutoSize = true;
            this.lblPayType.Location = new System.Drawing.Point(102, 206);
            this.lblPayType.Name = "lblPayType";
            this.lblPayType.Size = new System.Drawing.Size(41, 12);
            this.lblPayType.TabIndex = 0;
            this.lblPayType.Text = "label1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(84, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(339, 35);
            this.label1.TabIndex = 1;
            this.label1.Text = "贝利尔套餐购买收据";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 179);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "套餐费：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 206);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "支付方式：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(42, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "发票号：";
            // 
            // lbinvoiceNO
            // 
            this.lbinvoiceNO.AutoSize = true;
            this.lbinvoiceNO.Location = new System.Drawing.Point(102, 101);
            this.lbinvoiceNO.Name = "lbinvoiceNO";
            this.lbinvoiceNO.Size = new System.Drawing.Size(41, 12);
            this.lbinvoiceNO.TabIndex = 5;
            this.lbinvoiceNO.Text = "001123";
            // 
            // lbCost
            // 
            this.lbCost.AutoSize = true;
            this.lbCost.Location = new System.Drawing.Point(102, 179);
            this.lbCost.Name = "lbCost";
            this.lbCost.Size = new System.Drawing.Size(41, 12);
            this.lbCost.TabIndex = 6;
            this.lbCost.Text = "label5";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "套餐内容：";
            // 
            // lbPackageList
            // 
            this.lbPackageList.AutoSize = true;
            this.lbPackageList.Location = new System.Drawing.Point(102, 144);
            this.lbPackageList.Name = "lbPackageList";
            this.lbPackageList.Size = new System.Drawing.Size(41, 12);
            this.lbPackageList.TabIndex = 8;
            this.lbPackageList.Text = "label5";
            // 
            // ucPackageInvoiceBel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lbPackageList);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbCost);
            this.Controls.Add(this.lbinvoiceNO);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblPayType);
            this.Name = "ucPackageInvoiceBel";
            this.Size = new System.Drawing.Size(568, 337);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPayType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbinvoiceNO;
        private System.Windows.Forms.Label lbCost;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbPackageList;
    }
}
