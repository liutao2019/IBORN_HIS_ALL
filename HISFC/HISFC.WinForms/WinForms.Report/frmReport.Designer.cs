﻿namespace FS.Report
{
    partial class frmReport
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        //private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.ucPatientDayList1 = new Report.InpatientFee.ucPatientDayList();
            this.SuspendLayout();
            // 
            // ucPatientDayList1
            // 
            this.ucPatientDayList1.alPatientInfo = null;
            this.ucPatientDayList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPatientDayList1.Location = new System.Drawing.Point(0, 0);
            this.ucPatientDayList1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ucPatientDayList1.Name = "ucPatientDayList1";
            this.ucPatientDayList1.Size = new System.Drawing.Size(542, 384);
            this.ucPatientDayList1.TabIndex = 0;
            // 
            // frmReport
            // 
            this.ClientSize = new System.Drawing.Size(542, 384);
            this.Controls.Add(this.ucPatientDayList1);
            this.Name = "frmReport";
            this.Text = "报表";
            this.ResumeLayout(false);

        }

        #endregion

        private Neusoft.Report.InpatientFee.ucPatientDayList ucPatientDayList1;
    }
}