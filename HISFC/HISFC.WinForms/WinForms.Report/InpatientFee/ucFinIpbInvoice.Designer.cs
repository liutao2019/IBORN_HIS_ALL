namespace FS.WinForms.Report.InpatientFee
{
    partial class ucFinIpbInvoice
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucFinIpbInvoice));
            this.dwDetail = new FSDataWindow.Controls.FSDataWindow();
            this.cboCancelFlag = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel7 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbInvoiceNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.ucQueryInpatientNo1 = new FS.HISFC.Components.Common.Controls.ucQueryInpatientNo();
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
            this.plLeft.Size = new System.Drawing.Size(0, 386);
            // 
            // plRight
            // 
            this.plRight.Location = new System.Drawing.Point(0, 5);
            this.plRight.Margin = new System.Windows.Forms.Padding(2);
            this.plRight.Size = new System.Drawing.Size(747, 386);
            // 
            // plQueryCondition
            // 
            this.plQueryCondition.Margin = new System.Windows.Forms.Padding(2);
            this.plQueryCondition.Size = new System.Drawing.Size(0, 50);
            // 
            // plTop
            // 
            this.plTop.Controls.Add(this.ucQueryInpatientNo1);
            this.plTop.Controls.Add(this.cboCancelFlag);
            this.plTop.Controls.Add(this.neuLabel7);
            this.plTop.Controls.Add(this.neuLabel6);
            this.plTop.Controls.Add(this.tbInvoiceNo);
            this.plTop.Size = new System.Drawing.Size(747, 73);
            this.plTop.Controls.SetChildIndex(this.dtpBeginTime, 0);
            this.plTop.Controls.SetChildIndex(this.neuLabel1, 0);
            this.plTop.Controls.SetChildIndex(this.neuLabel2, 0);
            this.plTop.Controls.SetChildIndex(this.dtpEndTime, 0);
            this.plTop.Controls.SetChildIndex(this.tbInvoiceNo, 0);
            this.plTop.Controls.SetChildIndex(this.neuLabel6, 0);
            this.plTop.Controls.SetChildIndex(this.neuLabel7, 0);
            this.plTop.Controls.SetChildIndex(this.cboCancelFlag, 0);
            this.plTop.Controls.SetChildIndex(this.ucQueryInpatientNo1, 0);
            // 
            // plBottom
            // 
            this.plBottom.Location = new System.Drawing.Point(0, 73);
            this.plBottom.Size = new System.Drawing.Size(747, 391);
            // 
            // slLeft
            // 
            this.slLeft.Location = new System.Drawing.Point(0, 5);
            this.slLeft.Margin = new System.Windows.Forms.Padding(2);
            this.slLeft.Size = new System.Drawing.Size(3, 386);
            // 
            // plLeftControl
            // 
            this.plLeftControl.Location = new System.Drawing.Point(0, 50);
            this.plLeftControl.Margin = new System.Windows.Forms.Padding(2);
            this.plLeftControl.Size = new System.Drawing.Size(0, 336);
            // 
            // plRightTop
            // 
            this.plRightTop.Margin = new System.Windows.Forms.Padding(2);
            this.plRightTop.Size = new System.Drawing.Size(747, 82);
            // 
            // slTop
            // 
            this.slTop.Location = new System.Drawing.Point(0, 82);
            this.slTop.Margin = new System.Windows.Forms.Padding(2);
            this.slTop.Size = new System.Drawing.Size(747, 4);
            // 
            // plRightBottom
            // 
            this.plRightBottom.Controls.Add(this.dwDetail);
            this.plRightBottom.Location = new System.Drawing.Point(0, 86);
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
            this.btnClose.Location = new System.Drawing.Point(941, 9);
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
            this.dwMain.DataWindowObject = "d_zyinvoice_query";
            this.dwMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dwMain.Icon = ((System.Drawing.Icon)(resources.GetObject("dwMain.Icon")));
            this.dwMain.LibraryList = "Report\\fin_ipb.pbl";
            this.dwMain.Location = new System.Drawing.Point(0, 0);
            this.dwMain.Name = "dwMain";
            this.dwMain.ScrollBars = Sybase.DataWindow.DataWindowScrollBars.Both;
            this.dwMain.Size = new System.Drawing.Size(747, 82);
            this.dwMain.TabIndex = 0;
            this.dwMain.Text = "neuDataWindow1";
            this.dwMain.RowFocusChanged += new Sybase.DataWindow.RowFocusChangedEventHandler(this.dwMain_RowFocusChanged);
            // 
            // dwDetail
            // 
            this.dwDetail.DataWindowObject = "d_zyinvoice_detail";
            this.dwDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dwDetail.Icon = ((System.Drawing.Icon)(resources.GetObject("dwDetail.Icon")));
            this.dwDetail.LibraryList = "Report\\fin_ipb.pbl";
            this.dwDetail.Location = new System.Drawing.Point(3, 0);
            this.dwDetail.Margin = new System.Windows.Forms.Padding(2);
            this.dwDetail.Name = "dwDetail";
            this.dwDetail.ScrollBars = Sybase.DataWindow.DataWindowScrollBars.Both;
            this.dwDetail.Size = new System.Drawing.Size(741, 300);
            this.dwDetail.TabIndex = 1;
            this.dwDetail.Text = "neuDataWindow1";
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
            this.cboCancelFlag.Location = new System.Drawing.Point(294, 40);
            this.cboCancelFlag.Name = "cboCancelFlag";
            this.cboCancelFlag.PopForm = null;
            this.cboCancelFlag.ShowCustomerList = false;
            this.cboCancelFlag.ShowID = false;
            this.cboCancelFlag.Size = new System.Drawing.Size(143, 20);
            this.cboCancelFlag.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cboCancelFlag.TabIndex = 14;
            this.cboCancelFlag.Tag = "";
            this.cboCancelFlag.ToolBarUse = false;
            // 
            // neuLabel7
            // 
            this.neuLabel7.AutoSize = true;
            this.neuLabel7.Location = new System.Drawing.Point(228, 44);
            this.neuLabel7.Name = "neuLabel7";
            this.neuLabel7.Size = new System.Drawing.Size(59, 12);
            this.neuLabel7.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel7.TabIndex = 13;
            this.neuLabel7.Text = "发票状态:";
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.Location = new System.Drawing.Point(19, 44);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(47, 12);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 12;
            this.neuLabel6.Text = "发票号:";
            // 
            // tbInvoiceNo
            // 
            this.tbInvoiceNo.IsEnter2Tab = false;
            this.tbInvoiceNo.Location = new System.Drawing.Point(73, 40);
            this.tbInvoiceNo.Name = "tbInvoiceNo";
            this.tbInvoiceNo.Size = new System.Drawing.Size(143, 21);
            this.tbInvoiceNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbInvoiceNo.TabIndex = 15;
            // 
            // ucQueryInpatientNo1
            // 
            this.ucQueryInpatientNo1.InputType = 0;
            this.ucQueryInpatientNo1.Location = new System.Drawing.Point(456, 10);
            this.ucQueryInpatientNo1.Name = "ucQueryInpatientNo1";
            this.ucQueryInpatientNo1.ShowState = FS.HISFC.Components.Common.Controls.enuShowState.All;
            this.ucQueryInpatientNo1.Size = new System.Drawing.Size(182, 37);
            this.ucQueryInpatientNo1.TabIndex = 16;
            // 
            // ucFinIpbInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.IsLeftVisible = true;
            this.LeftControl = FS.WinForms.Report.Common.ucQueryBaseForDataWindow.QueryControls.Tree;
            this.IsShowDetail = true;
            this.MainDWDataObject = "d_zyinvoice_query";
            this.MainDWLabrary = "Report\\fin_ipb.pbl";
            this.Margin = new System.Windows.Forms.Padding(2);
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
        private FS.FrameWork.WinForms.Controls.NeuComboBox cboCancelFlag;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel7;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        private FS.FrameWork.WinForms.Controls.NeuTextBox tbInvoiceNo;
        private FS.HISFC.Components.Common.Controls.ucQueryInpatientNo ucQueryInpatientNo1;
    }
}
