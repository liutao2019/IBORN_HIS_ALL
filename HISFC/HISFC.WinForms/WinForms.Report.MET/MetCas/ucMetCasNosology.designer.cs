namespace FS.Report.MET.MetCas
{
    partial class ucMetCasNosology
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
            this.cboReportCode = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
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
            this.plLeft.Size = new System.Drawing.Size(0, 419);
            // 
            // plRight
            // 
            this.plRight.Location = new System.Drawing.Point(0, 5);
            this.plRight.Size = new System.Drawing.Size(747, 419);
            // 
            // plQueryCondition
            // 
            this.plQueryCondition.Size = new System.Drawing.Size(0, 33);
            // 
            // plTop
            // 
            this.plTop.Controls.Add(this.neuLabel3);
            this.plTop.Controls.Add(this.cboReportCode);
            this.plTop.Controls.SetChildIndex(this.neuLabel2, 0);
            this.plTop.Controls.SetChildIndex(this.cboReportCode, 0);
            this.plTop.Controls.SetChildIndex(this.dtpBeginTime, 0);
            this.plTop.Controls.SetChildIndex(this.neuLabel1, 0);
            this.plTop.Controls.SetChildIndex(this.dtpEndTime, 0);
            this.plTop.Controls.SetChildIndex(this.neuLabel3, 0);
            // 
            // slLeft
            // 
            this.slLeft.Location = new System.Drawing.Point(0, 5);
            // 
            // plLeftControl
            // 
            this.plLeftControl.Size = new System.Drawing.Size(0, 386);
            // 
            // plRightTop
            // 
            this.plRightTop.Size = new System.Drawing.Size(747, 416);
            // 
            // slTop
            // 
            this.slTop.Location = new System.Drawing.Point(0, 416);
            this.slTop.Size = new System.Drawing.Size(747, 3);
            // 
            // plRightBottom
            // 
            this.plRightBottom.Location = new System.Drawing.Point(0, 419);
            this.plRightBottom.Size = new System.Drawing.Size(747, 0);
            // 
            // gbMid
            // 
            this.gbMid.Size = new System.Drawing.Size(739, 38);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(728, 9);
            // 
            // dwMain
            // 
            this.dwMain.DataWindowObject = "d_met_cas_nosology";
            this.dwMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dwMain.LibraryList = "Report\\metcas.pbd;Report\\metcas.pbl";
            this.dwMain.Location = new System.Drawing.Point(0, 0);
            this.dwMain.Name = "dwMain";
            this.dwMain.ScrollBars = Sybase.DataWindow.DataWindowScrollBars.Both;
            this.dwMain.Size = new System.Drawing.Size(747, 416);
            this.dwMain.TabIndex = 0;
            this.dwMain.Text = "neuDataWindow1";
            // 
            // cboReportCode
            // 
            this.cboReportCode.ArrowBackColor = System.Drawing.Color.Silver;
            this.cboReportCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboReportCode.IsFlat = true;
            this.cboReportCode.IsLike = false;
            this.cboReportCode.Location = new System.Drawing.Point(518, 12);
            this.cboReportCode.Name = "cboReportCode";
            this.cboReportCode.PopForm = null;
            this.cboReportCode.ShowCustomerList = false;
            this.cboReportCode.ShowID = false;
            this.cboReportCode.Size = new System.Drawing.Size(121, 20);
            this.cboReportCode.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cboReportCode.TabIndex = 4;
            this.cboReportCode.Tag = "";
            this.cboReportCode.ToolBarUse = false;
            this.cboReportCode.SelectedIndexChanged += new System.EventHandler(this.cboReportCode_SelectedIndexChanged);
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(453, 17);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(59, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 5;
            this.neuLabel3.Text = "统计分类:";
            // 
            // ucMetCasNosology
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.IsLeftVisible = false;
            this.MainDWDataObject = "d_met_cas_nosology";
            this.MainDWLabrary = "Report\\metcas.pbd;Report\\metcas.pbl";
            this.Name = "ucMetCasNosology";
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

        private FS.FrameWork.WinForms.Controls.NeuComboBox cboReportCode ;
        protected FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
    }
}
