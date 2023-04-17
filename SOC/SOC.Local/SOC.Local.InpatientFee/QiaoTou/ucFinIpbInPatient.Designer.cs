namespace FS.SOC.Local.InpatientFee.QiaoTou
{
    partial class ucFinIpbInPatient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucFinIpbInPatient));
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbDept = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbFeeState = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
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
            this.plLeft.Size = new System.Drawing.Size(0, 417);
            // 
            // plRight
            // 
            this.plRight.Location = new System.Drawing.Point(0, 5);
            this.plRight.Margin = new System.Windows.Forms.Padding(2);
            this.plRight.Size = new System.Drawing.Size(934, 417);
            // 
            // plQueryCondition
            // 
            this.plQueryCondition.Margin = new System.Windows.Forms.Padding(2);
            this.plQueryCondition.Size = new System.Drawing.Size(0, 50);
            // 
            // plMain
            // 
            this.plMain.Size = new System.Drawing.Size(934, 464);
            // 
            // plTop
            // 
            this.plTop.Controls.Add(this.cmbFeeState);
            this.plTop.Controls.Add(this.neuLabel5);
            this.plTop.Controls.Add(this.cmbDept);
            this.plTop.Controls.Add(this.neuLabel3);
            this.plTop.Size = new System.Drawing.Size(934, 42);
            this.plTop.Controls.SetChildIndex(this.neuLabel3, 0);
            this.plTop.Controls.SetChildIndex(this.dtpBeginTime, 0);
            this.plTop.Controls.SetChildIndex(this.neuLabel1, 0);
            this.plTop.Controls.SetChildIndex(this.neuLabel2, 0);
            this.plTop.Controls.SetChildIndex(this.dtpEndTime, 0);
            this.plTop.Controls.SetChildIndex(this.cmbDept, 0);
            this.plTop.Controls.SetChildIndex(this.neuLabel5, 0);
            this.plTop.Controls.SetChildIndex(this.cmbFeeState, 0);
            // 
            // plBottom
            // 
            this.plBottom.Location = new System.Drawing.Point(0, 42);
            this.plBottom.Size = new System.Drawing.Size(934, 422);
            // 
            // slLeft
            // 
            this.slLeft.Location = new System.Drawing.Point(0, 5);
            this.slLeft.Margin = new System.Windows.Forms.Padding(2);
            this.slLeft.Size = new System.Drawing.Size(3, 417);
            // 
            // plLeftControl
            // 
            this.plLeftControl.Location = new System.Drawing.Point(0, 50);
            this.plLeftControl.Margin = new System.Windows.Forms.Padding(2);
            this.plLeftControl.Size = new System.Drawing.Size(0, 367);
            // 
            // plRightTop
            // 
            this.plRightTop.Margin = new System.Windows.Forms.Padding(2);
            this.plRightTop.Size = new System.Drawing.Size(934, 414);
            // 
            // slTop
            // 
            this.slTop.Location = new System.Drawing.Point(0, 414);
            this.slTop.Margin = new System.Windows.Forms.Padding(2);
            this.slTop.Size = new System.Drawing.Size(934, 3);
            // 
            // plRightBottom
            // 
            this.plRightBottom.Location = new System.Drawing.Point(0, 417);
            this.plRightBottom.Margin = new System.Windows.Forms.Padding(2);
            this.plRightBottom.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.plRightBottom.Size = new System.Drawing.Size(934, 0);
            // 
            // gbMid
            // 
            this.gbMid.Location = new System.Drawing.Point(3, 0);
            this.gbMid.Margin = new System.Windows.Forms.Padding(2);
            this.gbMid.Padding = new System.Windows.Forms.Padding(2);
            this.gbMid.Size = new System.Drawing.Size(928, 38);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(1285, 9);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            // 
            // lbText
            // 
            this.lbText.Location = new System.Drawing.Point(2, 16);
            this.lbText.Size = new System.Drawing.Size(485, 20);
            // 
            // dtpBeginTime
            // 
            this.dtpBeginTime.Size = new System.Drawing.Size(153, 21);
            // 
            // neuLabel2
            // 
            this.neuLabel2.Location = new System.Drawing.Point(229, 17);
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.Location = new System.Drawing.Point(295, 13);
            this.dtpEndTime.Size = new System.Drawing.Size(158, 21);
            // 
            // dwMain
            // 
            this.dwMain.DataWindowObject = "d_fin_ipb_patient_info";
            this.dwMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dwMain.Icon = ((System.Drawing.Icon)(resources.GetObject("dwMain.Icon")));
            this.dwMain.LibraryList = "Report\\finipb.pbd;Report\\finipb.pbl";
            this.dwMain.Location = new System.Drawing.Point(0, 0);
            this.dwMain.Name = "dwMain";
            this.dwMain.ScrollBars = Sybase.DataWindow.DataWindowScrollBars.Both;
            this.dwMain.Size = new System.Drawing.Size(934, 414);
            this.dwMain.TabIndex = 0;
            this.dwMain.Text = "neuDataWindow1";
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(486, 18);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(41, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 4;
            this.neuLabel3.Text = "科室：";
            this.neuLabel3.Visible = false;
            // 
            // cmbDept
            // 
            this.cmbDept.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbDept.FormattingEnabled = true;
            this.cmbDept.IsEnter2Tab = false;
            this.cmbDept.IsFlat = false;
            this.cmbDept.IsLike = true;
            this.cmbDept.IsListOnly = false;
            this.cmbDept.IsPopForm = true;
            this.cmbDept.IsShowCustomerList = false;
            this.cmbDept.IsShowID = false;
            this.cmbDept.Items.AddRange(new object[] {
            "全部"});
            this.cmbDept.Location = new System.Drawing.Point(523, 14);
            this.cmbDept.Name = "cmbDept";
            this.cmbDept.PopForm = null;
            this.cmbDept.ShowCustomerList = false;
            this.cmbDept.ShowID = false;
            this.cmbDept.Size = new System.Drawing.Size(81, 20);
            this.cmbDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbDept.TabIndex = 5;
            this.cmbDept.Tag = "";
            this.cmbDept.ToolBarUse = false;
            this.cmbDept.Visible = false;
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Location = new System.Drawing.Point(638, 18);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(65, 12);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 8;
            this.neuLabel5.Text = "结算状态：";
            this.neuLabel5.Visible = false;
            // 
            // cmbFeeState
            // 
            this.cmbFeeState.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbFeeState.FormattingEnabled = true;
            this.cmbFeeState.IsEnter2Tab = false;
            this.cmbFeeState.IsFlat = false;
            this.cmbFeeState.IsLike = true;
            this.cmbFeeState.IsListOnly = false;
            this.cmbFeeState.IsPopForm = true;
            this.cmbFeeState.IsShowCustomerList = false;
            this.cmbFeeState.IsShowID = false;
            this.cmbFeeState.Location = new System.Drawing.Point(697, 13);
            this.cmbFeeState.Name = "cmbFeeState";
            this.cmbFeeState.PopForm = null;
            this.cmbFeeState.ShowCustomerList = false;
            this.cmbFeeState.ShowID = false;
            this.cmbFeeState.Size = new System.Drawing.Size(65, 20);
            this.cmbFeeState.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbFeeState.TabIndex = 9;
            this.cmbFeeState.Tag = "";
            this.cmbFeeState.ToolBarUse = false;
            this.cmbFeeState.Visible = false;
            // 
            // ucFinIpbInPatient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.IsLeftVisible = false;
            this.MainDWDataObject = "d_fin_ipb_patient_info";
            this.MainDWLabrary = "Report\\finipb.pbd;Report\\finipb.pbl";
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ucFinIpbInPatient";
            this.Size = new System.Drawing.Size(934, 464);
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

        private FrameWork.WinForms.Controls.NeuComboBox cmbFeeState;
        private FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FrameWork.WinForms.Controls.NeuComboBox cmbDept;
        private FrameWork.WinForms.Controls.NeuLabel neuLabel3;
    }
}
