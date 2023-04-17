namespace SOC.Local.Order
{
    partial class ucDrugTransfusionPanel
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
            this.tbMain = new System.Windows.Forms.TabControl();
            this.tpBill = new System.Windows.Forms.TabPage();
            this.pnInfusion = new System.Windows.Forms.Panel();
            this.tpLabel = new System.Windows.Forms.TabPage();
            this.pnCard = new System.Windows.Forms.Panel();
            this.tbCard = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbMain.SuspendLayout();
            this.tpBill.SuspendLayout();
            this.tpLabel.SuspendLayout();
            this.tbCard.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbMain
            // 
            this.tbMain.Controls.Add(this.tpBill);
            this.tbMain.Controls.Add(this.tpLabel);
            this.tbMain.Controls.Add(this.tbCard);
            this.tbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbMain.Location = new System.Drawing.Point(0, 0);
            this.tbMain.Name = "tbMain";
            this.tbMain.SelectedIndex = 0;
            this.tbMain.Size = new System.Drawing.Size(565, 423);
            this.tbMain.TabIndex = 0;
            // 
            // tpBill
            // 
            this.tpBill.AutoScroll = true;
            this.tpBill.Controls.Add(this.pnInfusion);
            this.tpBill.Location = new System.Drawing.Point(4, 22);
            this.tpBill.Name = "tpBill";
            this.tpBill.Padding = new System.Windows.Forms.Padding(3);
            this.tpBill.Size = new System.Drawing.Size(557, 397);
            this.tpBill.TabIndex = 0;
            this.tpBill.Text = "输液卡";
            this.tpBill.UseVisualStyleBackColor = true;
            // 
            // pnInfusion
            // 
            this.pnInfusion.BackColor = System.Drawing.Color.White;
            this.pnInfusion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnInfusion.Location = new System.Drawing.Point(3, 3);
            this.pnInfusion.Name = "pnInfusion";
            this.pnInfusion.Size = new System.Drawing.Size(551, 391);
            this.pnInfusion.TabIndex = 0;
            // 
            // tpLabel
            // 
            this.tpLabel.AutoScroll = true;
            this.tpLabel.Controls.Add(this.pnCard);
            this.tpLabel.Location = new System.Drawing.Point(4, 22);
            this.tpLabel.Name = "tpLabel";
            this.tpLabel.Padding = new System.Windows.Forms.Padding(3);
            this.tpLabel.Size = new System.Drawing.Size(557, 397);
            this.tpLabel.TabIndex = 1;
            this.tpLabel.Text = "瓶贴";
            this.tpLabel.UseVisualStyleBackColor = true;
            // 
            // pnCard
            // 
            this.pnCard.AutoScroll = true;
            this.pnCard.AutoSize = true;
            this.pnCard.BackColor = System.Drawing.Color.White;
            this.pnCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnCard.Location = new System.Drawing.Point(3, 3);
            this.pnCard.Name = "pnCard";
            this.pnCard.Size = new System.Drawing.Size(551, 391);
            this.pnCard.TabIndex = 1;
            // 
            // tbCard
            // 
            this.tbCard.Controls.Add(this.panel1);
            this.tbCard.Location = new System.Drawing.Point(4, 22);
            this.tbCard.Name = "tbCard";
            this.tbCard.Padding = new System.Windows.Forms.Padding(3);
            this.tbCard.Size = new System.Drawing.Size(557, 397);
            this.tbCard.TabIndex = 2;
            this.tbCard.Text = "瓶签";
            this.tbCard.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoSize = true;
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(551, 391);
            this.panel1.TabIndex = 2;
            // 
            // ucDrugTransfusionPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.tbMain);
            this.Name = "ucDrugTransfusionPanel";
            this.Size = new System.Drawing.Size(565, 423);
            this.tbMain.ResumeLayout(false);
            this.tpBill.ResumeLayout(false);
            this.tpLabel.ResumeLayout(false);
            this.tpLabel.PerformLayout();
            this.tbCard.ResumeLayout(false);
            this.tbCard.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbMain;
        private System.Windows.Forms.TabPage tpBill;
        private System.Windows.Forms.TabPage tpLabel;
        private System.Windows.Forms.Panel pnInfusion;
        private System.Windows.Forms.Panel pnCard;
        private System.Windows.Forms.TabPage tbCard;
        private System.Windows.Forms.Panel panel1;
    }
}
