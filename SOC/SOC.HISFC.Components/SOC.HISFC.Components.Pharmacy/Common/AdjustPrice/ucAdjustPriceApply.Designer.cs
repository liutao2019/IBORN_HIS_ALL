namespace FS.SOC.HISFC.Components.Pharmacy.Common.AdjustPrice
{
    partial class ucAdjustPriceApply
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
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbAdustInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblEndTime = new System.Windows.Forms.Label();
            this.lblBeginTime = new System.Windows.Forms.Label();
            this.ndtpExecuteEndTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.ndtpExecuteBeginTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuPanelLeft.SuspendLayout();
            this.neuPanelMain.SuspendLayout();
            this.ngbInfo.SuspendLayout();
            this.neuPanelData.SuspendLayout();
            this.ngbLeftInfo.SuspendLayout();
            this.neuPanelLeftChoose.SuspendLayout();
            this.SuspendLayout();
            // 
            // ndtpEnd
            // 
            this.ndtpEnd.Value = new System.DateTime(2011, 12, 20, 23, 59, 59, 0);
            // 
            // ndtpBegin
            // 
            this.ndtpBegin.Value = new System.DateTime(2011, 12, 17, 0, 0, 0, 0);
            // 
            // nlbInfo3
            // 
            this.nlbInfo3.Location = new System.Drawing.Point(204, 26);
            // 
            // nlbInfo2
            // 
            this.nlbInfo2.Location = new System.Drawing.Point(115, 26);
            // 
            // nlbInfo1
            // 
            this.nlbInfo1.Location = new System.Drawing.Point(26, 26);
            // 
            // neuPanelLeft
            // 
            this.neuPanelLeft.Controls.Add(this.lblEndTime);
            this.neuPanelLeft.Controls.Add(this.ndtpExecuteEndTime);
            this.neuPanelLeft.Controls.Add(this.lblBeginTime);
            this.neuPanelLeft.Controls.Add(this.ndtpExecuteBeginTime);
            this.neuPanelLeft.Size = new System.Drawing.Size(833, 555);
            this.neuPanelLeft.Controls.SetChildIndex(this.ndtpExecuteBeginTime, 0);
            this.neuPanelLeft.Controls.SetChildIndex(this.lblBeginTime, 0);
            this.neuPanelLeft.Controls.SetChildIndex(this.ndtpExecuteEndTime, 0);
            this.neuPanelLeft.Controls.SetChildIndex(this.lblEndTime, 0);
            this.neuPanelLeft.Controls.SetChildIndex(this.neuPanelLeftChoose, 0);
            this.neuPanelLeft.Controls.SetChildIndex(this.ngbLeftInfo, 0);
            // 
            // neuSplitter1
            // 
            this.neuSplitter1.Location = new System.Drawing.Point(833, 0);
            // 
            // neuPanelMain
            // 
            this.neuPanelMain.Location = new System.Drawing.Point(836, 0);
            this.neuPanelMain.Size = new System.Drawing.Size(324, 555);
            // 
            // ngbInfo
            // 
            this.ngbInfo.Controls.Add(this.nlbAdustInfo);
            this.ngbInfo.Controls.Add(this.nlbInfo);
            this.ngbInfo.Controls.Add(this.neuLabel4);
            this.ngbInfo.Size = new System.Drawing.Size(324, 82);
            this.ngbInfo.Controls.SetChildIndex(this.nlbInfo3, 0);
            this.ngbInfo.Controls.SetChildIndex(this.nlbInfo2, 0);
            this.ngbInfo.Controls.SetChildIndex(this.nlbInfo1, 0);
            this.ngbInfo.Controls.SetChildIndex(this.neuLabel4, 0);
            this.ngbInfo.Controls.SetChildIndex(this.nlbInfo, 0);
            this.ngbInfo.Controls.SetChildIndex(this.nlbAdustInfo, 0);
            // 
            // neuPanelData
            // 
            this.neuPanelData.Size = new System.Drawing.Size(324, 473);
            // 
            // ngbLeftInfo
            // 
            this.ngbLeftInfo.Size = new System.Drawing.Size(833, 82);
            // 
            // neuPanelLeftChoose
            // 
            this.neuPanelLeftChoose.Dock = System.Windows.Forms.DockStyle.None;
            this.neuPanelLeftChoose.Location = new System.Drawing.Point(0, 41);
            this.neuPanelLeftChoose.Size = new System.Drawing.Size(833, 863);
            // 
            // ucTreeViewChooseList
            // 
            this.ucTreeViewChooseList.Dock = System.Windows.Forms.DockStyle.None;
            this.ucTreeViewChooseList.Location = new System.Drawing.Point(0, 41);
            this.ucTreeViewChooseList.Size = new System.Drawing.Size(833, 396);
            // 
            // ucDataDetail
            // 
            this.ucDataDetail.Size = new System.Drawing.Size(324, 473);
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel4.Location = new System.Drawing.Point(342, 26);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(245, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 16;
            this.neuLabel4.Text = "未执行的调价单如果要更改双击左侧列表节点";
            // 
            // nlbInfo
            // 
            this.nlbInfo.AutoSize = true;
            this.nlbInfo.ForeColor = System.Drawing.Color.Blue;
            this.nlbInfo.Location = new System.Drawing.Point(26, 53);
            this.nlbInfo.Name = "nlbInfo";
            this.nlbInfo.Size = new System.Drawing.Size(53, 12);
            this.nlbInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbInfo.TabIndex = 17;
            this.nlbInfo.Text = "全院调价";
            // 
            // nlbAdustInfo
            // 
            this.nlbAdustInfo.AutoSize = true;
            this.nlbAdustInfo.ForeColor = System.Drawing.Color.Blue;
            this.nlbAdustInfo.Location = new System.Drawing.Point(342, 53);
            this.nlbAdustInfo.Name = "nlbAdustInfo";
            this.nlbAdustInfo.Size = new System.Drawing.Size(77, 12);
            this.nlbAdustInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbAdustInfo.TabIndex = 18;
            this.nlbAdustInfo.Text = "单号，调价员";
            // 
            // lblEndTime
            // 
            this.lblEndTime.AutoSize = true;
            this.lblEndTime.Location = new System.Drawing.Point(259, 14);
            this.lblEndTime.Name = "lblEndTime";
            this.lblEndTime.Size = new System.Drawing.Size(77, 12);
            this.lblEndTime.TabIndex = 24;
            this.lblEndTime.Text = "监控结束时间";
            // 
            // lblBeginTime
            // 
            this.lblBeginTime.AutoSize = true;
            this.lblBeginTime.Location = new System.Drawing.Point(25, 14);
            this.lblBeginTime.Name = "lblBeginTime";
            this.lblBeginTime.Size = new System.Drawing.Size(77, 12);
            this.lblBeginTime.TabIndex = 23;
            this.lblBeginTime.Text = "监控起始时间";
            // 
            // ndtpExecuteEndTime
            // 
            this.ndtpExecuteEndTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.ndtpExecuteEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ndtpExecuteEndTime.IsEnter2Tab = false;
            this.ndtpExecuteEndTime.Location = new System.Drawing.Point(344, 11);
            this.ndtpExecuteEndTime.Name = "ndtpExecuteEndTime";
            this.ndtpExecuteEndTime.Size = new System.Drawing.Size(145, 21);
            this.ndtpExecuteEndTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ndtpExecuteEndTime.TabIndex = 22;
            // 
            // ndtpExecuteBeginTime
            // 
            this.ndtpExecuteBeginTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.ndtpExecuteBeginTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ndtpExecuteBeginTime.IsEnter2Tab = false;
            this.ndtpExecuteBeginTime.Location = new System.Drawing.Point(108, 11);
            this.ndtpExecuteBeginTime.Name = "ndtpExecuteBeginTime";
            this.ndtpExecuteBeginTime.Size = new System.Drawing.Size(145, 21);
            this.ndtpExecuteBeginTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ndtpExecuteBeginTime.TabIndex = 21;
            // 
            // ucAdjustPriceApply
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucAdjustPriceApply";
            this.Size = new System.Drawing.Size(1160, 555);
            this.neuPanelLeft.ResumeLayout(false);
            this.neuPanelLeft.PerformLayout();
            this.neuPanelMain.ResumeLayout(false);
            this.ngbInfo.ResumeLayout(false);
            this.ngbInfo.PerformLayout();
            this.neuPanelData.ResumeLayout(false);
            this.ngbLeftInfo.ResumeLayout(false);
            this.ngbLeftInfo.PerformLayout();
            this.neuPanelLeftChoose.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbInfo;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbAdustInfo;
        private System.Windows.Forms.Label lblEndTime;
        private System.Windows.Forms.Label lblBeginTime;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker ndtpExecuteEndTime;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker ndtpExecuteBeginTime;

    }
}
