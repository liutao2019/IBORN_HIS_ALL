﻿namespace FS.Report.MET.MetOpd
{
    partial class ucMetOpdPatientList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucMetOpdPatientList));
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
            this.plLeft.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.plLeft.Size = new System.Drawing.Size(0, 335);
            // 
            // plRight
            // 
            this.plRight.Location = new System.Drawing.Point(0, 5);
            this.plRight.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.plRight.Size = new System.Drawing.Size(747, 419);
            // 
            // plQueryCondition
            // 
            this.plQueryCondition.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.plQueryCondition.Size = new System.Drawing.Size(0, 33);
            // 
            // plTop
            // 
            this.plTop.Controls.Add(this.ucQueryInpatientNo1);
            this.plTop.Controls.SetChildIndex(this.dtpBeginTime, 0);
            this.plTop.Controls.SetChildIndex(this.neuLabel1, 0);
            this.plTop.Controls.SetChildIndex(this.neuLabel2, 0);
            this.plTop.Controls.SetChildIndex(this.dtpEndTime, 0);
            this.plTop.Controls.SetChildIndex(this.ucQueryInpatientNo1, 0);
            // 
            // slLeft
            // 
            this.slLeft.Location = new System.Drawing.Point(0, 4);
            this.slLeft.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            // 
            // plLeftControl
            // 
            this.plLeftControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.plLeftControl.Size = new System.Drawing.Size(0, 302);
            // 
            // plRightTop
            // 
            this.plRightTop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.plRightTop.Size = new System.Drawing.Size(747, 416);
            // 
            // slTop
            // 
            this.slTop.Location = new System.Drawing.Point(0, 416);
            this.slTop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.slTop.Size = new System.Drawing.Size(747, 3);
            // 
            // plRightBottom
            // 
            this.plRightBottom.Location = new System.Drawing.Point(0, 419);
            this.plRightBottom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.plRightBottom.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.plRightBottom.Size = new System.Drawing.Size(747, 0);
            // 
            // gbMid
            // 
            this.gbMid.Location = new System.Drawing.Point(5, 0);
            this.gbMid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbMid.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbMid.Size = new System.Drawing.Size(737, 38);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(941, 9);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            // 
            // lbText
            // 
            this.lbText.Location = new System.Drawing.Point(4, 18);
            this.lbText.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbText.Size = new System.Drawing.Size(485, 16);
            // 
            // dtpBeginTime
            // 
            this.dtpBeginTime.Visible = false;
            // 
            // neuLabel2
            // 
            this.neuLabel2.Location = new System.Drawing.Point(493, 17);
            this.neuLabel2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.neuLabel2.Visible = false;
            // 
            // neuLabel1
            // 
            this.neuLabel1.Visible = false;
            // 
            // dwMain
            // 
            this.dwMain.DataWindowObject = "d_met_opd_patientlist";
            this.dwMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dwMain.Icon = ((System.Drawing.Icon)(resources.GetObject("dwMain.Icon")));
            this.dwMain.LibraryList = "Report\\metopd.pbd;Report\\metopd.pbl";
            this.dwMain.Location = new System.Drawing.Point(0, 0);
            this.dwMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dwMain.Name = "dwMain";
            this.dwMain.ScrollBars = Sybase.DataWindow.DataWindowScrollBars.Both;
            this.dwMain.Size = new System.Drawing.Size(747, 416);
            this.dwMain.TabIndex = 0;
            this.dwMain.Text = "neuDataWindow1";
            // 
            // ucQueryInpatientNo1
            // 
            this.ucQueryInpatientNo1.InputType = 0;
            this.ucQueryInpatientNo1.Location = new System.Drawing.Point(235, 10);
            this.ucQueryInpatientNo1.Name = "ucQueryInpatientNo1";
            this.ucQueryInpatientNo1.ShowState = FS.HISFC.Components.Common.Controls.enuShowState.All;
            this.ucQueryInpatientNo1.Size = new System.Drawing.Size(198, 27);
            this.ucQueryInpatientNo1.TabIndex = 6;
            this.ucQueryInpatientNo1.Load += new System.EventHandler(this.ucQueryInpatientNo1_Load);
            this.ucQueryInpatientNo1.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(this.ucQueryInpatientNo1_myEvent);
            // 
            // ucMetOpdPatientList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.IsLeftVisible = false;
            this.MainDWDataObject = "d_met_opd_patientlist";
            this.MainDWLabrary = "Report\\metopd.pbd;Report\\metopd.pbl";
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ucMetOpdPatientList";
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

        private FS.HISFC.Components.Common.Controls.ucQueryInpatientNo ucQueryInpatientNo1;
    }
}
