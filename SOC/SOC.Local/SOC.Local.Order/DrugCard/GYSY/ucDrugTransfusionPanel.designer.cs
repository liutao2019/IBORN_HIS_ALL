namespace FS.SOC.Local.Order.DrugCard.GYSY
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpBill = new System.Windows.Forms.TabPage();
            this.pnInfusion = new System.Windows.Forms.Panel();
            this.tpLabel = new System.Windows.Forms.TabPage();
            this.pnCard = new System.Windows.Forms.Panel();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.chkAll = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.tabControl1.SuspendLayout();
            this.tpBill.SuspendLayout();
            this.tpLabel.SuspendLayout();
            this.neuPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpBill);
            this.tabControl1.Controls.Add(this.tpLabel);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(565, 398);
            this.tabControl1.TabIndex = 0;
            // 
            // tpBill
            // 
            this.tpBill.AutoScroll = true;
            this.tpBill.Controls.Add(this.pnInfusion);
            this.tpBill.Location = new System.Drawing.Point(4, 21);
            this.tpBill.Name = "tpBill";
            this.tpBill.Padding = new System.Windows.Forms.Padding(3);
            this.tpBill.Size = new System.Drawing.Size(557, 373);
            this.tpBill.TabIndex = 0;
            this.tpBill.Text = "输液卡";
            this.tpBill.UseVisualStyleBackColor = true;
            // 
            // pnInfusion
            // 
            this.pnInfusion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnInfusion.Location = new System.Drawing.Point(3, 3);
            this.pnInfusion.Name = "pnInfusion";
            this.pnInfusion.Size = new System.Drawing.Size(551, 367);
            this.pnInfusion.TabIndex = 0;
            // 
            // tpLabel
            // 
            this.tpLabel.AutoScroll = true;
            this.tpLabel.Controls.Add(this.pnCard);
            this.tpLabel.Location = new System.Drawing.Point(4, 21);
            this.tpLabel.Name = "tpLabel";
            this.tpLabel.Padding = new System.Windows.Forms.Padding(3);
            this.tpLabel.Size = new System.Drawing.Size(557, 373);
            this.tpLabel.TabIndex = 1;
            this.tpLabel.Text = "瓶贴";
            this.tpLabel.UseVisualStyleBackColor = true;
            // 
            // pnCard
            // 
            this.pnCard.AutoScroll = true;
            this.pnCard.AutoSize = true;
            this.pnCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnCard.Location = new System.Drawing.Point(3, 3);
            this.pnCard.Name = "pnCard";
            this.pnCard.Size = new System.Drawing.Size(551, 367);
            this.pnCard.TabIndex = 1;
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.chkAll);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(565, 25);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 1;
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Checked = true;
            this.chkAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAll.Location = new System.Drawing.Point(26, 5);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(48, 16);
            this.chkAll.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chkAll.TabIndex = 0;
            this.chkAll.Text = "全选";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // ucDrugTransfusionPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucDrugTransfusionPanel";
            this.Size = new System.Drawing.Size(565, 423);
            this.tabControl1.ResumeLayout(false);
            this.tpBill.ResumeLayout(false);
            this.tpLabel.ResumeLayout(false);
            this.tpLabel.PerformLayout();
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpBill;
        private System.Windows.Forms.TabPage tpLabel;
        private System.Windows.Forms.Panel pnInfusion;
        private System.Windows.Forms.Panel pnCard;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox chkAll;
    }
}
