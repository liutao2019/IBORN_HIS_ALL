namespace FS.HISFC.Components.Privilege
{
    partial class PermissionBaseForm
    {
        /// <summary>
        /// ����������������
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// ������������ʹ�õ���Դ��
        /// </summary>
        /// <param name="disposing">���Ӧ�ͷ��й���Դ��Ϊ true������Ϊ false��</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows ������������ɵĴ���

        /// <summary>
        /// �����֧������ķ��� - ��Ҫ
        /// ʹ�ô���༭���޸Ĵ˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
            this.MainToolStrip = new System.Windows.Forms.ToolStrip();
            this.SuspendLayout();
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 378);
            this.statusBar1.Size = new System.Drawing.Size(529, 24);
            // 
            // MainToolStrip
            // 
            this.MainToolStrip.AutoSize = false;
            this.MainToolStrip.BackColor = System.Drawing.Color.Transparent;
            this.MainToolStrip.ImageScalingSize = new System.Drawing.Size(36, 36);
            this.MainToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.MainToolStrip.Location = new System.Drawing.Point(0, 0);
            this.MainToolStrip.Name = "MainToolStrip";
            this.MainToolStrip.Size = new System.Drawing.Size(529, 52);
            this.MainToolStrip.Stretch = true;
            this.MainToolStrip.TabIndex = 1;
            this.MainToolStrip.Text = "nToolStrip1";
            // 
            // PermissionBaseForm
            // 
            this.ClientSize = new System.Drawing.Size(529, 402);
            this.Controls.Add(this.MainToolStrip);
            this.Name = "PermissionBaseForm";
            this.Text = "frmPermissionBase";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PermissionBaseForm_Load);
            this.Controls.SetChildIndex(this.statusBar1, 0);
            this.Controls.SetChildIndex(this.MainToolStrip, 0);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.ToolStrip MainToolStrip;

    }
}
