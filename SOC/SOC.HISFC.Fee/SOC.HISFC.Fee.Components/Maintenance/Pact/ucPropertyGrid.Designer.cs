namespace FS.SOC.HISFC.Fee.Components.Maintenance.Pact
{
    partial class ucPropertyGrid
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
            this.neuTabControl1 = new FS.FrameWork.WinForms.Controls.NeuTabControl();
            this.tpPactInfo = new System.Windows.Forms.TabPage();
            this.tpItemInfo = new System.Windows.Forms.TabPage();
            this.propertyGridPact = new System.Windows.Forms.PropertyGrid();
            this.propertyGridItem = new System.Windows.Forms.PropertyGrid();
            this.neuTabControl1.SuspendLayout();
            this.tpPactInfo.SuspendLayout();
            this.tpItemInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuTabControl1
            // 
            this.neuTabControl1.Controls.Add(this.tpPactInfo);
            this.neuTabControl1.Controls.Add(this.tpItemInfo);
            this.neuTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuTabControl1.Location = new System.Drawing.Point(0, 0);
            this.neuTabControl1.Name = "neuTabControl1";
            this.neuTabControl1.SelectedIndex = 0;
            this.neuTabControl1.Size = new System.Drawing.Size(277, 504);
            this.neuTabControl1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTabControl1.TabIndex = 0;
            // 
            // tpPactInfo
            // 
            this.tpPactInfo.Controls.Add(this.propertyGridPact);
            this.tpPactInfo.Location = new System.Drawing.Point(4, 22);
            this.tpPactInfo.Name = "tpPactInfo";
            this.tpPactInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpPactInfo.Size = new System.Drawing.Size(269, 478);
            this.tpPactInfo.TabIndex = 0;
            this.tpPactInfo.Text = "合同单位属性";
            this.tpPactInfo.UseVisualStyleBackColor = true;
            // 
            // tpItemInfo
            // 
            this.tpItemInfo.Controls.Add(this.propertyGridItem);
            this.tpItemInfo.Location = new System.Drawing.Point(4, 22);
            this.tpItemInfo.Name = "tpItemInfo";
            this.tpItemInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpItemInfo.Size = new System.Drawing.Size(269, 478);
            this.tpItemInfo.TabIndex = 1;
            this.tpItemInfo.Text = "项目属性";
            this.tpItemInfo.UseVisualStyleBackColor = true;
            // 
            // propertyGridPact
            // 
            this.propertyGridPact.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridPact.Location = new System.Drawing.Point(3, 3);
            this.propertyGridPact.Name = "propertyGridPact";
            this.propertyGridPact.Size = new System.Drawing.Size(263, 472);
            this.propertyGridPact.TabIndex = 1;
            // 
            // propertyGridItem
            // 
            this.propertyGridItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridItem.Location = new System.Drawing.Point(3, 3);
            this.propertyGridItem.Name = "propertyGridItem";
            this.propertyGridItem.Size = new System.Drawing.Size(263, 472);
            this.propertyGridItem.TabIndex = 2;
            // 
            // ucPropertyGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuTabControl1);
            this.Name = "ucPropertyGrid";
            this.Size = new System.Drawing.Size(277, 504);
            this.neuTabControl1.ResumeLayout(false);
            this.tpPactInfo.ResumeLayout(false);
            this.tpItemInfo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuTabControl neuTabControl1;
        private System.Windows.Forms.TabPage tpPactInfo;
        private System.Windows.Forms.PropertyGrid propertyGridPact;
        private System.Windows.Forms.TabPage tpItemInfo;
        private System.Windows.Forms.PropertyGrid propertyGridItem;

    }
}
