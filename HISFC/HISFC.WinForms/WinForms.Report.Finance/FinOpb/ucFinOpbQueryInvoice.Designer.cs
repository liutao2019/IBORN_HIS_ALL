namespace FS.Report.Finance.FinOpb
{
    partial class ucFinOpbQueryInvoice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucFinOpbQueryInvoice));
            this.cboPersonCode = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dwDetail = new FSDataWindow.Controls.FSDataWindow();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel7 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cboCancelFlag = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.tbInvoiceNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.tbName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.tbCardNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
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
            this.plLeft.Margin = new System.Windows.Forms.Padding(2);
            this.plLeft.Size = new System.Drawing.Size(0, 383);
            // 
            // plRight
            // 
            this.plRight.Location = new System.Drawing.Point(0, 5);
            this.plRight.Margin = new System.Windows.Forms.Padding(2);
            this.plRight.Size = new System.Drawing.Size(747, 383);
            // 
            // plQueryCondition
            // 
            this.plQueryCondition.Margin = new System.Windows.Forms.Padding(2);
            this.plQueryCondition.Size = new System.Drawing.Size(0, 50);
            // 
            // plTop
            // 
            this.plTop.Controls.Add(this.tbCardNo);
            this.plTop.Controls.Add(this.tbName);
            this.plTop.Controls.Add(this.cboCancelFlag);
            this.plTop.Controls.Add(this.neuLabel7);
            this.plTop.Controls.Add(this.neuLabel6);
            this.plTop.Controls.Add(this.neuLabel5);
            this.plTop.Controls.Add(this.tbInvoiceNo);
            this.plTop.Controls.Add(this.neuLabel4);
            this.plTop.Controls.Add(this.neuLabel3);
            this.plTop.Controls.Add(this.cboPersonCode);
            this.plTop.Size = new System.Drawing.Size(747, 76);
            this.plTop.Controls.SetChildIndex(this.cboPersonCode, 0);
            this.plTop.Controls.SetChildIndex(this.neuLabel3, 0);
            this.plTop.Controls.SetChildIndex(this.neuLabel4, 0);
            this.plTop.Controls.SetChildIndex(this.tbInvoiceNo, 0);
            this.plTop.Controls.SetChildIndex(this.neuLabel5, 0);
            this.plTop.Controls.SetChildIndex(this.neuLabel6, 0);
            this.plTop.Controls.SetChildIndex(this.neuLabel7, 0);
            this.plTop.Controls.SetChildIndex(this.cboCancelFlag, 0);
            this.plTop.Controls.SetChildIndex(this.tbName, 0);
            this.plTop.Controls.SetChildIndex(this.tbCardNo, 0);
            this.plTop.Controls.SetChildIndex(this.neuLabel2, 0);
            this.plTop.Controls.SetChildIndex(this.dtpBeginTime, 0);
            this.plTop.Controls.SetChildIndex(this.neuLabel1, 0);
            this.plTop.Controls.SetChildIndex(this.dtpEndTime, 0);
            // 
            // plBottom
            // 
            this.plBottom.Location = new System.Drawing.Point(0, 76);
            this.plBottom.Size = new System.Drawing.Size(747, 388);
            // 
            // slLeft
            // 
            this.slLeft.Location = new System.Drawing.Point(0, 5);
            this.slLeft.Margin = new System.Windows.Forms.Padding(2);
            this.slLeft.Size = new System.Drawing.Size(3, 383);
            // 
            // plLeftControl
            // 
            this.plLeftControl.Location = new System.Drawing.Point(0, 50);
            this.plLeftControl.Margin = new System.Windows.Forms.Padding(2);
            this.plLeftControl.Size = new System.Drawing.Size(0, 333);
            // 
            // plRightTop
            // 
            this.plRightTop.Margin = new System.Windows.Forms.Padding(2);
            this.plRightTop.Size = new System.Drawing.Size(747, 79);
            // 
            // slTop
            // 
            this.slTop.Location = new System.Drawing.Point(0, 79);
            this.slTop.Margin = new System.Windows.Forms.Padding(2);
            this.slTop.Size = new System.Drawing.Size(747, 4);
            // 
            // plRightBottom
            // 
            this.plRightBottom.Controls.Add(this.dwDetail);
            this.plRightBottom.Location = new System.Drawing.Point(0, 83);
            this.plRightBottom.Margin = new System.Windows.Forms.Padding(2);
            this.plRightBottom.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.plRightBottom.Size = new System.Drawing.Size(747, 300);
            this.plRightBottom.Controls.SetChildIndex(this.dwDetail, 0);
            this.plRightBottom.Controls.SetChildIndex(this.gbMid, 0);
            // 
            // gbMid
            // 
            this.gbMid.Location = new System.Drawing.Point(3, 0);
            this.gbMid.Margin = new System.Windows.Forms.Padding(2);
            this.gbMid.Padding = new System.Windows.Forms.Padding(2);
            this.gbMid.Size = new System.Drawing.Size(741, 1);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(953, 9);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            // 
            // lbText
            // 
            this.lbText.Location = new System.Drawing.Point(2, 16);
            this.lbText.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbText.Size = new System.Drawing.Size(485, 0);
            // 
            // dwMain
            // 
            this.dwMain.DataWindowObject = "d_fin_opb_qur_invoice";
            this.dwMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dwMain.Icon = ((System.Drawing.Icon)(resources.GetObject("dwMain.Icon")));
            this.dwMain.LibraryList = "Report\\finopb.pbl";
            this.dwMain.Location = new System.Drawing.Point(0, 0);
            this.dwMain.Name = "dwMain";
            this.dwMain.ScrollBars = Sybase.DataWindow.DataWindowScrollBars.Both;
            this.dwMain.Size = new System.Drawing.Size(747, 79);
            this.dwMain.TabIndex = 0;
            this.dwMain.Text = "neuDataWindow1";
            this.dwMain.RowFocusChanged += new Sybase.DataWindow.RowFocusChangedEventHandler(this.dwMain_RowFocusChanged);
            // 
            // cboPersonCode
            // 
            this.cboPersonCode.ArrowBackColor = System.Drawing.Color.Silver;
            this.cboPersonCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPersonCode.IsEnter2Tab = false;
            this.cboPersonCode.IsFlat = false;
            this.cboPersonCode.IsLike = true;
            this.cboPersonCode.IsListOnly = false;
            this.cboPersonCode.IsPopForm = true;
            this.cboPersonCode.IsShowCustomerList = false;
            this.cboPersonCode.IsShowID = false;
            this.cboPersonCode.Location = new System.Drawing.Point(511, 12);
            this.cboPersonCode.Name = "cboPersonCode";
            this.cboPersonCode.PopForm = null;
            this.cboPersonCode.ShowCustomerList = false;
            this.cboPersonCode.ShowID = false;
            this.cboPersonCode.Size = new System.Drawing.Size(121, 20);
            this.cboPersonCode.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cboPersonCode.TabIndex = 4;
            this.cboPersonCode.Tag = "";
            this.cboPersonCode.ToolBarUse = false;
            this.cboPersonCode.SelectedIndexChanged += new System.EventHandler(this.cboPersonCode_SelectedIndexChanged);
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(453, 17);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(47, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 5;
            this.neuLabel3.Text = "�տ�Ա:";
            // 
            // dwDetail
            // 
            this.dwDetail.DataWindowObject = "d_fin_opb_qur_invoicedetail";
            this.dwDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dwDetail.Icon = ((System.Drawing.Icon)(resources.GetObject("dwDetail.Icon")));
            this.dwDetail.LibraryList = "Report\\finopb.pbl";
            this.dwDetail.Location = new System.Drawing.Point(3, 0);
            this.dwDetail.Margin = new System.Windows.Forms.Padding(2);
            this.dwDetail.Name = "dwDetail";
            this.dwDetail.ScrollBars = Sybase.DataWindow.DataWindowScrollBars.Both;
            this.dwDetail.Size = new System.Drawing.Size(741, 300);
            this.dwDetail.TabIndex = 1;
            this.dwDetail.Text = "neuDataWindow1";
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point(9, 50);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(59, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 6;
            this.neuLabel4.Text = "��������:";
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Location = new System.Drawing.Point(174, 50);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(47, 12);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 7;
            this.neuLabel5.Text = "������:";
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.Location = new System.Drawing.Point(357, 50);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(47, 12);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 8;
            this.neuLabel6.Text = "��Ʊ��:";
            // 
            // neuLabel7
            // 
            this.neuLabel7.AutoSize = true;
            this.neuLabel7.Location = new System.Drawing.Point(542, 50);
            this.neuLabel7.Name = "neuLabel7";
            this.neuLabel7.Size = new System.Drawing.Size(59, 12);
            this.neuLabel7.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel7.TabIndex = 9;
            this.neuLabel7.Text = "��Ʊ״̬:";
            // 
            // cboCancelFlag
            // 
            this.cboCancelFlag.ArrowBackColor = System.Drawing.Color.Silver;
            this.cboCancelFlag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCancelFlag.IsEnter2Tab = false;
            this.cboCancelFlag.IsFlat = false;
            this.cboCancelFlag.IsLike = true;
            this.cboCancelFlag.IsListOnly = false;
            this.cboCancelFlag.IsPopForm = true;
            this.cboCancelFlag.IsShowCustomerList = false;
            this.cboCancelFlag.IsShowID = false;
            this.cboCancelFlag.Location = new System.Drawing.Point(608, 46);
            this.cboCancelFlag.Name = "cboCancelFlag";
            this.cboCancelFlag.PopForm = null;
            this.cboCancelFlag.ShowCustomerList = false;
            this.cboCancelFlag.ShowID = false;
            this.cboCancelFlag.Size = new System.Drawing.Size(121, 20);
            this.cboCancelFlag.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cboCancelFlag.TabIndex = 10;
            this.cboCancelFlag.Tag = "";
            this.cboCancelFlag.ToolBarUse = false;
            // 
            // tbInvoiceNo
            // 
            this.tbInvoiceNo.IsEnter2Tab = false;
            this.tbInvoiceNo.Location = new System.Drawing.Point(411, 46);
            this.tbInvoiceNo.Name = "tbInvoiceNo";
            this.tbInvoiceNo.Size = new System.Drawing.Size(114, 21);
            this.tbInvoiceNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbInvoiceNo.TabIndex = 11;
            // 
            // tbName
            // 
            this.tbName.IsEnter2Tab = false;
            this.tbName.Location = new System.Drawing.Point(73, 46);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(88, 21);
            this.tbName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbName.TabIndex = 12;
            // 
            // tbCardNo
            // 
            this.tbCardNo.IsEnter2Tab = false;
            this.tbCardNo.Location = new System.Drawing.Point(228, 46);
            this.tbCardNo.Name = "tbCardNo";
            this.tbCardNo.Size = new System.Drawing.Size(114, 21);
            this.tbCardNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbCardNo.TabIndex = 13;
            // 
            // ucFinOpbQueryInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.IsLeftVisible = false;
            this.IsShowDetail = true;
            this.MainDWDataObject = "d_fin_opb_qur_invoice";
            this.MainDWLabrary = "Report\\finopb.pbl";
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ucFinOpbQueryInvoice";
            this.Load += new System.EventHandler(this.ucFinOpbQueryInvoice_Load);
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

        private FS.FrameWork.WinForms.Controls.NeuComboBox cboPersonCode;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FSDataWindow.Controls.FSDataWindow dwDetail;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel7;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cboCancelFlag;
        private FS.FrameWork.WinForms.Controls.NeuTextBox tbCardNo;
        private FS.FrameWork.WinForms.Controls.NeuTextBox tbName;
        private FS.FrameWork.WinForms.Controls.NeuTextBox tbInvoiceNo;
    }
}
