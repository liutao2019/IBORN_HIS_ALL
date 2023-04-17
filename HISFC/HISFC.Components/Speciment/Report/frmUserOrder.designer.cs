namespace FS.HISFC.Components.Speciment.Report
{
    partial class frmUserOrder
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            FS.HISFC.Components.Speciment.Report.ucUserOrder.UserOrderObject userOrderObject2 = new FS.HISFC.Components.Speciment.Report.ucUserOrder.UserOrderObject();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.ucUserOrderControl = new FS.HISFC.Components.Speciment.Report.ucUserOrder();
            this.flowLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(479, 12);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 99;
            this.btnOK.Text = "确认";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.Location = new System.Drawing.Point(479, 52);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(75, 23);
            this.btnReturn.TabIndex = 100;
            this.btnReturn.Text = "返回";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Controls.Add(this.ucUserOrderControl);
            this.flowLayoutPanel.Location = new System.Drawing.Point(12, 12);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(461, 205);
            this.flowLayoutPanel.TabIndex = 0;
            // 
            // ucUserOrderControl
            // 
            this.ucUserOrderControl.Arrlist = null;
            this.ucUserOrderControl.Location = new System.Drawing.Point(3, 3);
            this.ucUserOrderControl.Name = "ucUserOrderControl";
            this.ucUserOrderControl.Size = new System.Drawing.Size(437, 26);
            this.ucUserOrderControl.TabIndex = 108;
            this.ucUserOrderControl.UserOrderOjectTrans = userOrderObject2;
            // 
            // frmUserOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 226);
            this.Controls.Add(this.flowLayoutPanel);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.btnOK);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUserOrder";
            this.Text = "多条件排序";
            this.Load += new System.EventHandler(this.frmUserOrder_Load);
            this.flowLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private ucUserOrder ucUserOrderControl;

    }
}