namespace FS.HISFC.Components.Order.OutPatient.Forms
{
    partial class frmSelectRoom
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
            this.neuListBox1 = new FS.FrameWork.WinForms.Controls.NeuListBox();
            this.btnOK = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.cancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuButton1 = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.SuspendLayout();
            // 
            // neuListBox1
            // 
            this.neuListBox1.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuListBox1.FormattingEnabled = true;
            this.neuListBox1.ItemHeight = 16;
            this.neuListBox1.Location = new System.Drawing.Point(12, 14);
            this.neuListBox1.Name = "neuListBox1";
            this.neuListBox1.Size = new System.Drawing.Size(356, 228);
            this.neuListBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuListBox1.TabIndex = 0;
            this.neuListBox1.DoubleClick += new System.EventHandler(this.neuListBox1_DoubleClick);
            this.neuListBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.neuListBox1_KeyDown);
            // 
            // btnOK
            // 
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(24, 257);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "ȷ��";
            this.btnOK.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // cancel
            // 
            this.cancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cancel.Location = new System.Drawing.Point(135, 257);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cancel.TabIndex = 2;
            this.cancel.Text = "ȡ��";
            this.cancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // neuButton1
            // 
            this.neuButton1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.neuButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.neuButton1.Location = new System.Drawing.Point(250, 257);
            this.neuButton1.Name = "neuButton1";
            this.neuButton1.Size = new System.Drawing.Size(75, 23);
            this.neuButton1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuButton1.TabIndex = 3;
            this.neuButton1.Text = "��ʱ����";
            this.neuButton1.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuButton1.UseVisualStyleBackColor = true;
            this.neuButton1.Click += new System.EventHandler(this.neuButton1_Click);
            // 
            // frmSelectRoom
            // 
            this.ClientSize = new System.Drawing.Size(380, 292);
            this.Controls.Add(this.neuButton1);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.neuListBox1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSelectRoom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ѡ���������";
            this.Load += new System.EventHandler(this.frmSelectRoom_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuListBox neuListBox1;
        private FS.FrameWork.WinForms.Controls.NeuButton btnOK;
        private FS.FrameWork.WinForms.Controls.NeuButton cancel;
        private FS.FrameWork.WinForms.Controls.NeuButton neuButton1;
    }
}
