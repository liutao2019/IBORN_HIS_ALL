namespace FS.SOC.HISFC.Components.Pharmacy.Common.AdjustPrice
{
    partial class ucBaseAdjustPrice
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
            this.ndtpEnd = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ndtpBegin = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbInfo3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbInfo2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbInfo1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanelLeft.SuspendLayout();
            this.neuPanelMain.SuspendLayout();
            this.ngbInfo.SuspendLayout();
            this.neuPanelData.SuspendLayout();
            this.ngbLeftInfo.SuspendLayout();
            this.neuPanelLeftChoose.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucTreeViewChooseList
            // 
            this.ucTreeViewChooseList.Size = new System.Drawing.Size(302, 473);
            // 
            // ucDataDetail
            // 
            this.ucDataDetail.Size = new System.Drawing.Size(507, 473);
            // 
            // neuPanelLeft
            // 
            this.neuPanelLeft.Size = new System.Drawing.Size(302, 555);
            // 
            // neuSplitter1
            // 
            this.neuSplitter1.Location = new System.Drawing.Point(302, 0);
            // 
            // neuPanelMain
            // 
            this.neuPanelMain.Location = new System.Drawing.Point(305, 0);
            this.neuPanelMain.Size = new System.Drawing.Size(507, 555);
            // 
            // ngbInfo
            // 
            this.ngbInfo.Controls.Add(this.nlbInfo3);
            this.ngbInfo.Controls.Add(this.nlbInfo2);
            this.ngbInfo.Controls.Add(this.nlbInfo1);
            this.ngbInfo.Location = new System.Drawing.Point(0, 473);
            this.ngbInfo.Size = new System.Drawing.Size(507, 82);
            // 
            // neuPanelData
            // 
            this.neuPanelData.Size = new System.Drawing.Size(507, 473);
            // 
            // ngbLeftInfo
            // 
            this.ngbLeftInfo.Controls.Add(this.ndtpEnd);
            this.ngbLeftInfo.Controls.Add(this.neuLabel2);
            this.ngbLeftInfo.Controls.Add(this.ndtpBegin);
            this.ngbLeftInfo.Controls.Add(this.neuLabel1);
            this.ngbLeftInfo.Location = new System.Drawing.Point(0, 473);
            this.ngbLeftInfo.Size = new System.Drawing.Size(302, 82);
            this.ngbLeftInfo.Text = "查询时间设置(按制单时间)";
            // 
            // neuPanelLeftChoose
            // 
            this.neuPanelLeftChoose.Size = new System.Drawing.Size(302, 473);
            // 
            // ndtpEnd
            // 
            this.ndtpEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.ndtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ndtpEnd.IsEnter2Tab = false;
            this.ndtpEnd.Location = new System.Drawing.Point(108, 49);
            this.ndtpEnd.Name = "ndtpEnd";
            this.ndtpEnd.Size = new System.Drawing.Size(145, 21);
            this.ndtpEnd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ndtpEnd.TabIndex = 11;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(37, 53);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(65, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 10;
            this.neuLabel2.Text = "结束时间：";
            // 
            // ndtpBegin
            // 
            this.ndtpBegin.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.ndtpBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ndtpBegin.IsEnter2Tab = false;
            this.ndtpBegin.Location = new System.Drawing.Point(108, 22);
            this.ndtpBegin.Name = "ndtpBegin";
            this.ndtpBegin.Size = new System.Drawing.Size(145, 21);
            this.ndtpBegin.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ndtpBegin.TabIndex = 9;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(37, 26);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(65, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 8;
            this.neuLabel1.Text = "开始时间：";
            // 
            // nlbInfo3
            // 
            this.nlbInfo3.AutoSize = true;
            this.nlbInfo3.BackColor = System.Drawing.Color.Transparent;
            this.nlbInfo3.ForeColor = System.Drawing.Color.Black;
            this.nlbInfo3.Location = new System.Drawing.Point(453, 37);
            this.nlbInfo3.Name = "nlbInfo3";
            this.nlbInfo3.Size = new System.Drawing.Size(65, 12);
            this.nlbInfo3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbInfo3.TabIndex = 6;
            this.nlbInfo3.Text = "黑色无调亏";
            this.nlbInfo3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nlbInfo2
            // 
            this.nlbInfo2.AutoSize = true;
            this.nlbInfo2.BackColor = System.Drawing.Color.Transparent;
            this.nlbInfo2.ForeColor = System.Drawing.Color.Blue;
            this.nlbInfo2.Location = new System.Drawing.Point(241, 37);
            this.nlbInfo2.Name = "nlbInfo2";
            this.nlbInfo2.Size = new System.Drawing.Size(53, 12);
            this.nlbInfo2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbInfo2.TabIndex = 5;
            this.nlbInfo2.Text = "蓝色调盈";
            this.nlbInfo2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nlbInfo1
            // 
            this.nlbInfo1.AutoSize = true;
            this.nlbInfo1.BackColor = System.Drawing.Color.Transparent;
            this.nlbInfo1.ForeColor = System.Drawing.Color.Red;
            this.nlbInfo1.Location = new System.Drawing.Point(29, 37);
            this.nlbInfo1.Name = "nlbInfo1";
            this.nlbInfo1.Size = new System.Drawing.Size(53, 12);
            this.nlbInfo1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbInfo1.TabIndex = 4;
            this.nlbInfo1.Text = "红色调亏";
            this.nlbInfo1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucBaseAdjustPrice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucBaseAdjustPrice";
            this.neuPanelLeft.ResumeLayout(false);
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

        protected FS.FrameWork.WinForms.Controls.NeuDateTimePicker ndtpEnd;
        protected FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        protected FS.FrameWork.WinForms.Controls.NeuDateTimePicker ndtpBegin;
        protected FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbInfo3;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbInfo2;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbInfo1;
    }
}
