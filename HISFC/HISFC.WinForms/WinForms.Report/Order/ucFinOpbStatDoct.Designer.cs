namespace FS.WinForms.Report.Order
{
    partial class ucFinOpbStatDoct
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
            this.components = new System.ComponentModel.Container();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cboReportCode = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
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
            // plTop
            // 
            this.plTop.Controls.Add(this.neuLabel3);
            this.plTop.Controls.Add(this.cboReportCode);
            this.plTop.Controls.SetChildIndex(this.dtpBeginTime, 0);
            this.plTop.Controls.SetChildIndex(this.neuLabel1, 0);
            this.plTop.Controls.SetChildIndex(this.neuLabel2, 0);
            this.plTop.Controls.SetChildIndex(this.dtpEndTime, 0);
            this.plTop.Controls.SetChildIndex(this.cboReportCode, 0);
            this.plTop.Controls.SetChildIndex(this.neuLabel3, 0);
            // 
            // plRightTop
            // 
            this.plRightTop.Size = new System.Drawing.Size(544, 416);
            // 
            // slTop
            // 
            this.slTop.Location = new System.Drawing.Point(0, 416);
            // 
            // plRightBottom
            // 
            this.plRightBottom.Location = new System.Drawing.Point(0, 419);
            this.plRightBottom.Size = new System.Drawing.Size(544, 0);
            // 
            // dwMain
            // 
            this.dwMain.DataWindowObject = "d_fin_opb_stat_doct";
            this.dwMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dwMain.LibraryList = "Report\\fin_opb.pbd;Report\\fin_opb.pbl";
            this.dwMain.Location = new System.Drawing.Point(0, 0);
            this.dwMain.Name = "dwMain";
            this.dwMain.ScrollBars = Sybase.DataWindow.DataWindowScrollBars.Both;
            this.dwMain.Size = new System.Drawing.Size(544, 416);
            this.dwMain.TabIndex = 0;
            this.dwMain.Text = "neuDataWindow1";
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(454, 18);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(59, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 9;
            this.neuLabel3.Text = "ͳ�Ʒ���:";
            // 
            // cboReportCode
            // 
            this.cboReportCode.ArrowBackColor = System.Drawing.Color.Silver;
            this.cboReportCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboReportCode.IsFlat = true;
            this.cboReportCode.IsLike = false;
            this.cboReportCode.Location = new System.Drawing.Point(519, 13);
            this.cboReportCode.Name = "cboReportCode";
            this.cboReportCode.PopForm = null;
            this.cboReportCode.ShowCustomerList = false;
            this.cboReportCode.ShowID = false;
            this.cboReportCode.Size = new System.Drawing.Size(121, 20);
            this.cboReportCode.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cboReportCode.TabIndex = 8;
            this.cboReportCode.Tag = "";
            this.cboReportCode.ToolBarUse = false;
            this.cboReportCode.SelectedIndexChanged += new System.EventHandler(this.cboReportCode_SelectedIndexChanged);
            // 
            // ucFinOpbStatDoct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.LeftControl = Report.Common.ucQueryBaseForDataWindow.QueryControls.Tree;
            this.MainDWDataObject = "d_fin_opb_stat_doct";
            this.MainDWLabrary = "Report\\fin_opb.pbd;Report\\fin_ipb.pbl";
            this.Name = "ucFinOpbStatDoct";
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

        protected FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cboReportCode;
    }
}
