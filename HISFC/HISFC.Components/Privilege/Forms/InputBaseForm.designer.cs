namespace FS.HISFC.Components.Privilege
{
    partial class InputBaseForm
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
            this.TitlePanel = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.BottomPanel = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.ContentPanel = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.nLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ContentPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 338);
            this.statusBar1.Size = new System.Drawing.Size(478, 25);
            // 
            // TitlePanel
            // 
            this.TitlePanel.BackColor = System.Drawing.Color.Transparent;
            this.TitlePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TitlePanel.Location = new System.Drawing.Point(0, 0);
            this.TitlePanel.Name = "TitlePanel";
            this.TitlePanel.Size = new System.Drawing.Size(478, 50);
            this.TitlePanel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.TitlePanel.TabIndex = 2;
            // 
            // BottomPanel
            // 
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 290);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(478, 48);
            this.BottomPanel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.BottomPanel.TabIndex = 1;
            // 
            // ContentPanel
            // 
            this.ContentPanel.Controls.Add(this.nLabel1);
            this.ContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContentPanel.Location = new System.Drawing.Point(0, 50);
            this.ContentPanel.Name = "ContentPanel";
            this.ContentPanel.Size = new System.Drawing.Size(478, 240);
            this.ContentPanel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ContentPanel.TabIndex = 0;
            // 
            // nLabel1
            // 
            this.nLabel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.nLabel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.nLabel1.Enabled = false;
            this.nLabel1.Location = new System.Drawing.Point(0, 239);
            this.nLabel1.Name = "nLabel1";
            this.nLabel1.Size = new System.Drawing.Size(478, 1);
            this.nLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nLabel1.TabIndex = 0;
            // 
            // InputBaseForm
            // 
            this.ClientSize = new System.Drawing.Size(478, 363);
            this.Controls.Add(this.ContentPanel);
            this.Controls.Add(this.BottomPanel);
            this.Controls.Add(this.TitlePanel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InputBaseForm";
            this.Text = "frmInputBase";
            this.Controls.SetChildIndex(this.statusBar1, 0);
            this.Controls.SetChildIndex(this.TitlePanel, 0);
            this.Controls.SetChildIndex(this.BottomPanel, 0);
            this.Controls.SetChildIndex(this.ContentPanel, 0);
            this.ContentPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private FrameWork.WinForms.Controls.NeuLabel nLabel1;
        protected FrameWork.WinForms.Controls.NeuPanel ContentPanel;
        protected FrameWork.WinForms.Controls.NeuPanel TitlePanel;
        protected FrameWork.WinForms.Controls.NeuPanel BottomPanel;
        
    }
}
