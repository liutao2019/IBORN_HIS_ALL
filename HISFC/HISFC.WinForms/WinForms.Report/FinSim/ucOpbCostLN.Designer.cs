﻿namespace FS.WinForms.Report.FinSim
{
    partial class ucOpbCostLN
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
            this.plLeft.Size = new System.Drawing.Size(0, 541);
            // 
            // plRight
            // 
            this.plRight.Location = new System.Drawing.Point(0, 0);
            this.plRight.Size = new System.Drawing.Size(996, 541);
            // 
            // plQueryCondition
            // 
            this.plQueryCondition.Size = new System.Drawing.Size(0, 62);
            // 
            // slLeft
            // 
            this.slLeft.Location = new System.Drawing.Point(0, 0);
            // 
            // plLeftControl
            // 
            this.plLeftControl.Size = new System.Drawing.Size(0, 479);
            // 
            // plRightTop
            // 
            this.plRightTop.Size = new System.Drawing.Size(996, 236);
            // 
            // slTop
            // 
            this.slTop.Location = new System.Drawing.Point(0, 236);
            this.slTop.Size = new System.Drawing.Size(996, 5);
            // 
            // plRightBottom
            // 
            this.plRightBottom.Controls.Add(this.dwDetail);
            this.plRightBottom.Location = new System.Drawing.Point(0, 241);
            this.plRightBottom.Size = new System.Drawing.Size(996, 300);
            this.plRightBottom.Controls.SetChildIndex(this.dwDetail, 0);
            this.plRightBottom.Controls.SetChildIndex(this.gbMid, 0);
            // 
            // gbMid
            // 
            this.gbMid.Size = new System.Drawing.Size(986, 1);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(995, 11);
            // 
            // lbText
            // 
            this.lbText.Size = new System.Drawing.Size(647, 0);
            // 
            // dwMain
            // 
            this.dwMain.DataWindowObject = "d_fin_sim_opbcost_ln";
            this.dwMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dwMain.LibraryList = "Report\\fin_sim.pbd;fin_sim.pbl;";
            this.dwMain.Location = new System.Drawing.Point(0, 0);
            this.dwMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dwMain.Name = "dwMain";
            this.dwMain.ScrollBars = Sybase.DataWindow.DataWindowScrollBars.Both;
            this.dwMain.Size = new System.Drawing.Size(996, 236);
            this.dwMain.TabIndex = 0;
            this.dwMain.Text = "neuDataWindow1";
            // 
            // dwDetail
            // 
            this.dwDetail.DataWindowObject = "d_fin_sim_opbcost_detail_ln";
            this.dwDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dwDetail.LibraryList = "Report\\fin_sim.pbd;fin_sim.pbl";
            this.dwDetail.Location = new System.Drawing.Point(5, 0);
            this.dwDetail.Name = "dwDetail";
            this.dwDetail.ScrollBars = Sybase.DataWindow.DataWindowScrollBars.Both;
            this.dwDetail.Size = new System.Drawing.Size(986, 300);
            this.dwDetail.TabIndex = 1;
            this.dwDetail.Text = "neuDataWindow1";
            // 
            // ucFinIpbInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.IsLeftVisible = false;
            this.IsShowDetail = true;
            this.MainDWDataObject = "d_fin_sim_opbcost_ln";
            this.MainDWLabrary = "Report\\fin_opb.pbd;fin_ipb.pbl";
            this.Name = "ucOpbCostLN";
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