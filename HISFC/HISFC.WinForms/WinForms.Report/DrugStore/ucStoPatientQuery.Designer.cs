namespace FS.WinForms.Report.DrugStore
{
    partial class ucStoPatientQuery
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
            this.plLeft.Size = new System.Drawing.Size(170, 541);
            // 
            // plRight
            // 
            this.plRight.Location = new System.Drawing.Point(174, 0);
            this.plRight.Size = new System.Drawing.Size(822, 541);
            // 
            // plQueryCondition
            // 
            this.plQueryCondition.Size = new System.Drawing.Size(170, 62);
            // 
            // slLeft
            // 
            this.slLeft.Location = new System.Drawing.Point(170, 0);
            // 
            // plLeftControl
            // 
            this.plLeftControl.Size = new System.Drawing.Size(170, 479);
            // 
            // plRightTop
            // 
            this.plRightTop.Size = new System.Drawing.Size(822, 537);
            // 
            // slTop
            // 
            this.slTop.Location = new System.Drawing.Point(0, 537);
            this.slTop.Size = new System.Drawing.Size(822, 4);
            // 
            // plRightBottom
            // 
            this.plRightBottom.Location = new System.Drawing.Point(0, 541);
            this.plRightBottom.Size = new System.Drawing.Size(822, 0);
            // 
            // gbMid
            // 
            this.gbMid.Size = new System.Drawing.Size(812, 48);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(781, 11);
            // 
            // dwMain
            // 
            this.dwMain.DataWindowObject = "d_sto_patient_usequery";
            this.dwMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dwMain.LibraryList = "Report\\pha.pbd;pha.pbl";
            this.dwMain.Location = new System.Drawing.Point(0, 0);
            this.dwMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dwMain.Name = "dwMain";
            this.dwMain.ScrollBars = Sybase.DataWindow.DataWindowScrollBars.Both;
            this.dwMain.Size = new System.Drawing.Size(822, 537);
            this.dwMain.TabIndex = 0;
            this.dwMain.Text = "neuDataWindow1";
            // 
            // ucStoPatientQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.LeftControl = Report.Common.ucQueryBaseForDataWindow.QueryControls.Tree;
            this.MainDWDataObject = "d_sto_patient_usequery";
            this.MainDWLabrary = "Report\\pha.pbd;pha.pbl";
            this.Name = "ucStoPatientQuery";
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
    }
}
