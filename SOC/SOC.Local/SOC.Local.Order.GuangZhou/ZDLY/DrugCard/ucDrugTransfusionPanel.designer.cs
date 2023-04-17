namespace FS.SOC.Local.Order.GuangZhou.ZDLY.DrugCard
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
            this.tpLabel = new System.Windows.Forms.TabPage();
            this.pnCardTop = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.chkAll = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.pnCard = new System.Windows.Forms.Panel();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.tabControl1.SuspendLayout();
            this.tpLabel.SuspendLayout();
            this.pnCardTop.SuspendLayout();
            this.neuPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpLabel);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(565, 401);
            this.tabControl1.TabIndex = 0;
            // 
            // tpLabel
            // 
            this.tpLabel.AutoScroll = true;
            this.tpLabel.Controls.Add(this.pnCard);
            this.tpLabel.Location = new System.Drawing.Point(4, 21);
            this.tpLabel.Name = "tpLabel";
            this.tpLabel.Padding = new System.Windows.Forms.Padding(3);
            this.tpLabel.Size = new System.Drawing.Size(557, 376);
            this.tpLabel.TabIndex = 1;
            this.tpLabel.Text = "瓶贴";
            this.tpLabel.UseVisualStyleBackColor = true;
            // 
            // pnCardTop
            // 
            this.pnCardTop.Controls.Add(this.chkAll);
            this.pnCardTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnCardTop.Location = new System.Drawing.Point(0, 0);
            this.pnCardTop.Name = "pnCardTop";
            this.pnCardTop.Size = new System.Drawing.Size(565, 22);
            this.pnCardTop.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnCardTop.TabIndex = 2;
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(3, 3);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(48, 16);
            this.chkAll.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chkAll.TabIndex = 0;
            this.chkAll.Text = "全选";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // pnCard
            // 
            this.pnCard.AutoScroll = true;
            this.pnCard.AutoSize = true;
            this.pnCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnCard.Location = new System.Drawing.Point(3, 3);
            this.pnCard.Name = "pnCard";
            this.pnCard.Size = new System.Drawing.Size(551, 370);
            this.pnCard.TabIndex = 1;
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.tabControl1);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel1.Location = new System.Drawing.Point(0, 22);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(565, 401);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 3;
            // 
            // ucDrugTransfusionPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.neuPanel1);
            this.Controls.Add(this.pnCardTop);
            this.Name = "ucDrugTransfusionPanel";
            this.Size = new System.Drawing.Size(565, 423);
            this.tabControl1.ResumeLayout(false);
            this.tpLabel.ResumeLayout(false);
            this.tpLabel.PerformLayout();
            this.pnCardTop.ResumeLayout(false);
            this.pnCardTop.PerformLayout();
            this.neuPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpLabel;
        private System.Windows.Forms.Panel pnCard;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnCardTop;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox chkAll;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
    }
}
