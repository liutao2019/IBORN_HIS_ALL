﻿namespace FS.WinForms.Report.FinSim
{
    partial class ucFinSimIpbFeeByDate
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
            this.metComboBox1 = new FS.FrameWork.WinForms.Controls.NeuComboBox ( );
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel ( );
            this.plLeft.SuspendLayout ( );
            this.plRight.SuspendLayout ( );
            this.plMain.SuspendLayout ( );
            this.plTop.SuspendLayout ( );
            this.plBottom.SuspendLayout ( );
            this.plRightTop.SuspendLayout ( );
            this.plRightBottom.SuspendLayout ( );
            this.gbMid.SuspendLayout ( );
            this.SuspendLayout ( );
            // 
            // plLeft
            // 
            this.plLeft.Size = new System.Drawing.Size ( 0 , 419 );
            // 
            // plRight
            // 
            this.plRight.Location = new System.Drawing.Point ( 0 , 5 );
            this.plRight.Size = new System.Drawing.Size ( 747 , 419 );
            // 
            // plQueryCondition
            // 
            this.plQueryCondition.Size = new System.Drawing.Size ( 0 , 33 );
            // 
            // plTop
            // 
            this.plTop.Controls.Add ( this.metComboBox1 );
            this.plTop.Controls.Add ( this.neuLabel3 );
            this.plTop.Controls.SetChildIndex ( this.dtpBeginTime , 0 );
            this.plTop.Controls.SetChildIndex ( this.neuLabel1 , 0 );
            this.plTop.Controls.SetChildIndex ( this.neuLabel2 , 0 );
            this.plTop.Controls.SetChildIndex ( this.dtpEndTime , 0 );
            this.plTop.Controls.SetChildIndex ( this.neuLabel3 , 0 );
            this.plTop.Controls.SetChildIndex ( this.metComboBox1 , 0 );
            // 
            // slLeft
            // 
            this.slLeft.Location = new System.Drawing.Point ( 0 , 5 );
            // 
            // plLeftControl
            // 
            this.plLeftControl.Size = new System.Drawing.Size ( 0 , 386 );
            // 
            // plRightTop
            // 
            this.plRightTop.Size = new System.Drawing.Size ( 747 , 416 );
            // 
            // slTop
            // 
            this.slTop.Location = new System.Drawing.Point ( 0 , 416 );
            this.slTop.Size = new System.Drawing.Size ( 747 , 3 );
            // 
            // plRightBottom
            // 
            this.plRightBottom.Location = new System.Drawing.Point ( 0 , 419 );
            this.plRightBottom.Size = new System.Drawing.Size ( 747 , 0 );
            // 
            // gbMid
            // 
            this.gbMid.Size = new System.Drawing.Size ( 739 , 38 );
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point ( 734 , 9 );
            // 
            // dwMain
            // 
            this.dwMain.DataWindowObject = "d_fin_sim_ipb_feebydate";
            this.dwMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dwMain.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dwMain.LibraryList = "Report\\fin_sim.pbl";
            this.dwMain.Location = new System.Drawing.Point ( 0 , 0 );
            this.dwMain.Name = "dwMain";
            this.dwMain.ScrollBars = Sybase.DataWindow.DataWindowScrollBars.Both;
            this.dwMain.Size = new System.Drawing.Size ( 747 , 416 );
            this.dwMain.TabIndex = 0;
            this.dwMain.Text = "neuDataWindow1";
            // 
            // metComboBox1
            // 
            this.metComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.metComboBox1.FormattingEnabled = true;
            this.metComboBox1.IsEnter2Tab = false;
            this.metComboBox1.IsFlat = true;
            this.metComboBox1.Location = new System.Drawing.Point ( 547 , 14 );
            this.metComboBox1.Name = "metComboBox1";
            this.metComboBox1.Size = new System.Drawing.Size ( 133 , 22 );
            this.metComboBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.metComboBox1.TabIndex = 10;
            this.metComboBox1.ToolBarUse = false;
            this.metComboBox1.SelectedIndexChanged += new System.EventHandler ( this.metComboBox1_SelectedIndexChanged );
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point ( 481 , 19 );
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size ( 65 , 12 );
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 11;
            this.neuLabel3.Text = "医保分类：";
            // 
            // ucFinSimIpbFeeByDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF ( 6F , 12F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.IsLeftVisible = false;
            this.MainDWDataObject = "d_fin_sim_ipb_feebydate";
            this.MainDWLabrary = "Report\\fin_sim.pbl";
            this.Name = "ucFinSimIpbFeeByDate";
            this.plLeft.ResumeLayout ( false );
            this.plRight.ResumeLayout ( false );
            this.plMain.ResumeLayout ( false );
            this.plTop.ResumeLayout ( false );
            this.plTop.PerformLayout ( );
            this.plBottom.ResumeLayout ( false );
            this.plRightTop.ResumeLayout ( false );
            this.plRightBottom.ResumeLayout ( false );
            this.gbMid.ResumeLayout ( false );
            this.ResumeLayout ( false );

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuComboBox metComboBox1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
    }
}
