namespace FS.Report.Finance.FinIpb
{
    partial class ucFinIpbInvoice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucFinIpbInvoice));
            this.dwDetail = new FSDataWindow.Controls.FSDataWindow();
            this.plLeft.SuspendLayout();
            this.plRight.SuspendLayout();
            this.plMain.SuspendLayout();
            this.plTop.SuspendLayout();
            this.plBottom.SuspendLayout();
            this.plRightTop.SuspendLayout();
            this.plRightBottom.SuspendLayout();
            this.gbMid.SuspendLayout();
            this.SuspendLayout();
            // 
            // plLeft
            // 
            this.plLeft.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.plLeft.Size = new System.Drawing.Size(0, 335);
            // 
            // plRight
            // 
            this.plRight.Location = new System.Drawing.Point(0, 5);
            this.plRight.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.plRight.Size = new System.Drawing.Size(747, 419);
            // 
            // plQueryCondition
            // 
            this.plQueryCondition.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.plQueryCondition.Size = new System.Drawing.Size(0, 50);
            // 
            // slLeft
            // 
            this.slLeft.Location = new System.Drawing.Point(0, 4);
            this.slLeft.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            // 
            // plLeftControl
            // 
            this.plLeftControl.Location = new System.Drawing.Point(0, 50);
            this.plLeftControl.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.plLeftControl.Size = new System.Drawing.Size(0, 285);
            // 
            // plRightTop
            // 
            this.plRightTop.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.plRightTop.Size = new System.Drawing.Size(747, 175);
            // 
            // slTop
            // 
            this.slTop.Location = new System.Drawing.Point(0, 175);
            this.slTop.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.slTop.Size = new System.Drawing.Size(747, 4);
            // 
            // plRightBottom
            // 
            this.plRightBottom.Controls.Add(this.dwDetail);
            this.plRightBottom.Location = new System.Drawing.Point(0, 179);
            this.plRightBottom.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.plRightBottom.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.plRightBottom.Size = new System.Drawing.Size(747, 240);
            this.plRightBottom.Controls.SetChildIndex(this.dwDetail, 0);
            this.plRightBottom.Controls.SetChildIndex(this.gbMid, 0);
            // 
            // gbMid
            // 
            this.gbMid.Location = new System.Drawing.Point(3, 0);
            this.gbMid.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbMid.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbMid.Size = new System.Drawing.Size(741, 1);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(935, 9);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            // 
            // lbText
            // 
            this.lbText.Location = new System.Drawing.Point(2, 16);
            this.lbText.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbText.Size = new System.Drawing.Size(485, 0);
            // 
            // dwMain
            // 
            this.dwMain.DataWindowObject = "d_fin_ipb_invoice_query";
            this.dwMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dwMain.Icon = ((System.Drawing.Icon)(resources.GetObject("dwMain.Icon")));
            this.dwMain.LibraryList = "Report\\finipb.pbd;finipb.pbl";
            this.dwMain.Location = new System.Drawing.Point(0, 0);
            this.dwMain.Name = "dwMain";
            this.dwMain.ScrollBars = Sybase.DataWindow.DataWindowScrollBars.Both;
            this.dwMain.Size = new System.Drawing.Size(747, 175);
            this.dwMain.TabIndex = 0;
            this.dwMain.Text = "neuDataWindow1";
            this.dwMain.RowFocusChanged += new Sybase.DataWindow.RowFocusChangedEventHandler(this.dwMain_RowFocusChanged);
            // 
            // dwDetail
            // 
            this.dwDetail.DataWindowObject = "d_fin_ipb_invoice_detail";
            this.dwDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dwDetail.Icon = ((System.Drawing.Icon)(resources.GetObject("dwDetail.Icon")));
            this.dwDetail.LibraryList = "Report\\finipb.pbd;finipb.pbl";
            this.dwDetail.Location = new System.Drawing.Point(3, 0);
            this.dwDetail.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dwDetail.Name = "dwDetail";
            this.dwDetail.ScrollBars = Sybase.DataWindow.DataWindowScrollBars.Both;
            this.dwDetail.Size = new System.Drawing.Size(741, 240);
            this.dwDetail.TabIndex = 1;
            this.dwDetail.Text = "neuDataWindow1";
            // 
            // ucFinIpbInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.IsLeftVisible = false;
            this.IsShowDetail = true;
            this.MainDWDataObject = "d_fin_ipb_invoice_query";
            this.MainDWLabrary = "Report\\finipb.pbd;finipb.pbl";
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ucFinIpbInvoice";
            this.plLeft.ResumeLayout(false);
            this.plRight.ResumeLayout(false);
            this.plMain.ResumeLayout(false);
            this.plTop.ResumeLayout(false);
            this.plTop.PerformLayout();
            this.plBottom.ResumeLayout(false);
            this.plRightTop.ResumeLayout(false);
            this.plRightBottom.ResumeLayout(false);
            this.gbMid.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FSDataWindow.Controls.FSDataWindow dwDetail;
    }
}
