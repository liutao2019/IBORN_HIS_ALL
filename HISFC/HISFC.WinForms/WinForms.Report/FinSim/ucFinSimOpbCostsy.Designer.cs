﻿namespace FS.WinForms.Report.FinSim
{
    partial class ucFinSimOpbCostsy
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
            this.components = new System.ComponentModel.Container ( );
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel ( );
            this.neuComboBox1 = new FS.FrameWork.WinForms.Controls.NeuComboBox ( this.components );
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
            this.plTop.Controls.Add ( this.neuLabel6 );
            this.plTop.Controls.Add ( this.neuComboBox1 );
            this.plTop.Controls.SetChildIndex ( this.dtpBeginTime , 0 );
            this.plTop.Controls.SetChildIndex ( this.neuLabel1 , 0 );
            this.plTop.Controls.SetChildIndex ( this.neuLabel2 , 0 );
            this.plTop.Controls.SetChildIndex ( this.dtpEndTime , 0 );
            this.plTop.Controls.SetChildIndex ( this.neuComboBox1 , 0 );
            this.plTop.Controls.SetChildIndex ( this.neuLabel6 , 0 );
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
            this.btnClose.Location = new System.Drawing.Point ( 722 , 9 );
            // 
            // dwMain
            // 
            this.dwMain.DataWindowObject = "d_fin_opb_costsy";
            this.dwMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dwMain.LibraryList = "Report\\fin_sim.pbl";
            this.dwMain.Location = new System.Drawing.Point ( 0 , 0 );
            this.dwMain.Name = "dwMain";
            this.dwMain.ScrollBars = Sybase.DataWindow.DataWindowScrollBars.Both;
            this.dwMain.Size = new System.Drawing.Size ( 747 , 416 );
            this.dwMain.TabIndex = 0;
            this.dwMain.Text = "neuDataWindow1";
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.Location = new System.Drawing.Point ( 449 , 17 );
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size ( 65 , 12 );
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 18;
            this.neuLabel6.Text = "市保类型：";
            // 
            // neuComboBox1
            // 
            this.neuComboBox1.ArrowBackColor = System.Drawing.Color.Silver;
            this.neuComboBox1.FormattingEnabled = true;
            this.neuComboBox1.IsEnter2Tab = false;
            this.neuComboBox1.IsFlat = true;
            this.neuComboBox1.IsLike = true;
            this.neuComboBox1.Location = new System.Drawing.Point ( 532 , 14 );
            this.neuComboBox1.Name = "neuComboBox1";
            this.neuComboBox1.PopForm = null;
            this.neuComboBox1.ShowCustomerList = false;
            this.neuComboBox1.ShowID = false;
            this.neuComboBox1.Size = new System.Drawing.Size ( 138 , 20 );
            this.neuComboBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuComboBox1.TabIndex = 17;
            this.neuComboBox1.Tag = "";
            this.neuComboBox1.ToolBarUse = false;
            this.neuComboBox1.SelectedIndexChanged += new System.EventHandler ( this.neuComboBox1_SelectedIndexChanged );
            // 
            // ucFinopbCostsy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF ( 6F , 12F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.IsLeftVisible = false;
            this.MainDWDataObject = "d_fin_opb_costsy";
            this.MainDWLabrary = "Report\\fin_sim.pbl";
            this.Name = "ucFinopbCostsy";
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

        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        private FS.FrameWork.WinForms.Controls.NeuComboBox neuComboBox1;
    }
}
