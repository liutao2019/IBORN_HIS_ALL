﻿namespace FS.Report.Finance.FinIpb
{
    partial class ucFinIpbGeneral1
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。

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
            // plLeft
            // 
            this.plLeft.Size = new System.Drawing.Size(0, 419);
            // 
            // plRight
            // 
            this.plRight.Location = new System.Drawing.Point(0, 5);
            this.plRight.Size = new System.Drawing.Size(738, 382);
            // 
            // plQueryCondition
            // 
            this.plQueryCondition.Size = new System.Drawing.Size(0, 33);
            // 
            // plMain
            // 
            this.plMain.Size = new System.Drawing.Size(738, 427);
            // 
            // plTop
            // 
            this.plTop.Controls.Add(this.neuLabel3);
            this.plTop.Controls.Add(this.cboReportCode);
            this.plTop.Size = new System.Drawing.Size(738, 40);
            this.plTop.Controls.SetChildIndex(this.cboReportCode, 0);
            this.plTop.Controls.SetChildIndex(this.neuLabel3, 0);
            this.plTop.Controls.SetChildIndex(this.dtpBeginTime, 0);
            this.plTop.Controls.SetChildIndex(this.neuLabel1, 0);
            this.plTop.Controls.SetChildIndex(this.neuLabel2, 0);
            this.plTop.Controls.SetChildIndex(this.dtpEndTime, 0);
            // 
            // plBottom
            // 
            this.plBottom.Size = new System.Drawing.Size(738, 387);
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
            this.plRightTop.Size = new System.Drawing.Size(738, 379);
            // 
            // slTop
            // 
            this.slTop.Location = new System.Drawing.Point(0, 379);
            this.slTop.Size = new System.Drawing.Size(738, 3);
            // 
            // plRightBottom
            // 
            this.plRightBottom.Location = new System.Drawing.Point(0, 382);
            this.plRightBottom.Size = new System.Drawing.Size(738, 0);
            // 
            // gbMid
            // 
            this.gbMid.Size = new System.Drawing.Size(730, 38);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(686, 9);
            // 
            // dwMain
            // 
            this.dwMain.DataWindowObject = "";
            this.dwMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dwMain.LibraryList = "";
            this.dwMain.Location = new System.Drawing.Point(0, 0);
            this.dwMain.Name = "dwMain";
            this.dwMain.ScrollBars = Sybase.DataWindow.DataWindowScrollBars.Both;
            this.dwMain.Size = new System.Drawing.Size(738, 379);
            this.dwMain.TabIndex = 0;
            this.dwMain.Text = "neuDataWindow1";
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(453, 18);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(59, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 4;
            this.neuLabel3.Text = "统计分类:";
            // 
            // cboReportCode
            // 
            this.cboReportCode.ArrowBackColor = System.Drawing.Color.Silver;
            this.cboReportCode.FormattingEnabled = true;
            this.cboReportCode.IsFlat = true;
            this.cboReportCode.IsLike = true;
            this.cboReportCode.Location = new System.Drawing.Point(520, 14);
            this.cboReportCode.Name = "cboReportCode";
            this.cboReportCode.PopForm = null;
            this.cboReportCode.ShowCustomerList = false;
            this.cboReportCode.ShowID = false;
            this.cboReportCode.Size = new System.Drawing.Size(121, 20);
            this.cboReportCode.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cboReportCode.TabIndex = 5;
            this.cboReportCode.Tag = "";
            this.cboReportCode.ToolBarUse = false;
            this.cboReportCode.SelectedIndexChanged += new System.EventHandler(this.cboReportCode_SelectedIndexChanged);
            // 
            // ucFinIpbGeneral1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.IsLeftVisible = false;
            this.MainDWDataObject = "d_fin_ipb_general_1";
            this.MainDWLabrary = "Report\\finipb.pbd;Report\\finipb.pbl";
            this.Name = "ucFinIpbGeneral1";
            this.Size = new System.Drawing.Size(738, 427);
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

        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cboReportCode;
    }
}
