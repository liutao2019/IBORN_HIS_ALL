namespace FS.SOC.Local.Pharmacy.ShenZhen.Common.Plan
{
    partial class ucPlan
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
            this.nlbInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ndtpEnd = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ndtpBegin = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nrbNoSort = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nrbSortByCustomNO = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
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
            this.ucTreeViewChooseList.Size = new System.Drawing.Size(308, 473);
            // 
            // ucDataDetail
            // 
            this.ucDataDetail.Size = new System.Drawing.Size(824, 473);
            // 
            // neuPanelLeft
            // 
            this.neuPanelLeft.Size = new System.Drawing.Size(308, 555);
            // 
            // neuSplitter1
            // 
            this.neuSplitter1.Location = new System.Drawing.Point(308, 0);
            // 
            // neuPanelMain
            // 
            this.neuPanelMain.Location = new System.Drawing.Point(311, 0);
            this.neuPanelMain.Size = new System.Drawing.Size(824, 555);
            // 
            // ngbInfo
            // 
            this.ngbInfo.Controls.Add(this.nrbSortByCustomNO);
            this.ngbInfo.Controls.Add(this.neuLabel3);
            this.ngbInfo.Controls.Add(this.nrbNoSort);
            this.ngbInfo.Controls.Add(this.nlbInfo);
            this.ngbInfo.Location = new System.Drawing.Point(0, 473);
            this.ngbInfo.Size = new System.Drawing.Size(824, 82);
            // 
            // neuPanelData
            // 
            this.neuPanelData.Size = new System.Drawing.Size(824, 473);
            // 
            // ngbLeftInfo
            // 
            this.ngbLeftInfo.Controls.Add(this.ndtpEnd);
            this.ngbLeftInfo.Controls.Add(this.neuLabel2);
            this.ngbLeftInfo.Controls.Add(this.ndtpBegin);
            this.ngbLeftInfo.Controls.Add(this.neuLabel1);
            this.ngbLeftInfo.Location = new System.Drawing.Point(0, 473);
            this.ngbLeftInfo.Size = new System.Drawing.Size(308, 82);
            this.ngbLeftInfo.Text = "已封账计划时间";
            // 
            // neuPanelLeftChoose
            // 
            this.neuPanelLeftChoose.Size = new System.Drawing.Size(308, 473);
            // 
            // nlbInfo
            // 
            this.nlbInfo.AutoSize = true;
            this.nlbInfo.Location = new System.Drawing.Point(22, 30);
            this.nlbInfo.Name = "nlbInfo";
            this.nlbInfo.Size = new System.Drawing.Size(89, 12);
            this.nlbInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbInfo.TabIndex = 0;
            this.nlbInfo.Text = "您选择的是【】";
            // 
            // ndtpEnd
            // 
            this.ndtpEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.ndtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ndtpEnd.IsEnter2Tab = false;
            this.ndtpEnd.Location = new System.Drawing.Point(110, 50);
            this.ndtpEnd.Name = "ndtpEnd";
            this.ndtpEnd.Size = new System.Drawing.Size(145, 21);
            this.ndtpEnd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ndtpEnd.TabIndex = 7;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(39, 54);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(65, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 6;
            this.neuLabel2.Text = "结束时间：";
            // 
            // ndtpBegin
            // 
            this.ndtpBegin.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.ndtpBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ndtpBegin.IsEnter2Tab = false;
            this.ndtpBegin.Location = new System.Drawing.Point(110, 23);
            this.ndtpBegin.Name = "ndtpBegin";
            this.ndtpBegin.Size = new System.Drawing.Size(145, 21);
            this.ndtpBegin.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ndtpBegin.TabIndex = 5;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(39, 27);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(65, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 4;
            this.neuLabel1.Text = "开始时间：";
            // 
            // nrbNoSort
            // 
            this.nrbNoSort.AutoSize = true;
            this.nrbNoSort.Checked = true;
            this.nrbNoSort.Location = new System.Drawing.Point(126, 55);
            this.nrbNoSort.Name = "nrbNoSort";
            this.nrbNoSort.Size = new System.Drawing.Size(71, 16);
            this.nrbNoSort.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nrbNoSort.TabIndex = 1;
            this.nrbNoSort.TabStop = true;
            this.nrbNoSort.Text = "物理顺序";
            this.nrbNoSort.UseVisualStyleBackColor = true;
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(22, 57);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(77, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 2;
            this.neuLabel3.Text = "数据排序方式";
            // 
            // nrbSortByCustomNO
            // 
            this.nrbSortByCustomNO.AutoSize = true;
            this.nrbSortByCustomNO.Location = new System.Drawing.Point(227, 55);
            this.nrbSortByCustomNO.Name = "nrbSortByCustomNO";
            this.nrbSortByCustomNO.Size = new System.Drawing.Size(95, 16);
            this.nrbSortByCustomNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nrbSortByCustomNO.TabIndex = 3;
            this.nrbSortByCustomNO.Text = "自定义码顺序";
            this.nrbSortByCustomNO.UseVisualStyleBackColor = true;
            // 
            // ucPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucPlan";
            this.Size = new System.Drawing.Size(1135, 555);
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

        private FS.FrameWork.WinForms.Controls.NeuLabel nlbInfo;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker ndtpEnd;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker ndtpBegin;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton nrbSortByCustomNO;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton nrbNoSort;
    }
}
