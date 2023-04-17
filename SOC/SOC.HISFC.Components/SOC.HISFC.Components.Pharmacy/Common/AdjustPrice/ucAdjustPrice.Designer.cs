namespace FS.SOC.HISFC.Components.Pharmacy.Common.AdjustPrice
{
    partial class AdjustPrice
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
            this.ncbExecuteTime = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.nlbExcuteInfo1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ndtpExecuteTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbExcuteInfo2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbAdustInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbAdjustType = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtfilter = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
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
            // ucTreeViewChooseList
            // 
            this.ucTreeViewChooseList.Size = new System.Drawing.Size(302, 390);
            // 
            // ucDataDetail
            // 
            this.ucDataDetail.Size = new System.Drawing.Size(954, 442);
            // 
            // neuPanelLeft
            // 
            this.neuPanelLeft.Size = new System.Drawing.Size(302, 524);
            // 
            // neuSplitter1
            // 
            this.neuSplitter1.Size = new System.Drawing.Size(3, 524);
            // 
            // neuPanelMain
            // 
            this.neuPanelMain.Size = new System.Drawing.Size(954, 524);
            // 
            // ngbInfo
            // 
            this.ngbInfo.Controls.Add(this.nlbAdustInfo);
            this.ngbInfo.Controls.Add(this.nlbInfo);
            this.ngbInfo.Controls.Add(this.neuLabel4);
            this.ngbInfo.Location = new System.Drawing.Point(0, 442);
            this.ngbInfo.Size = new System.Drawing.Size(954, 82);
            this.ngbInfo.Controls.SetChildIndex(this.nlbInfo3, 0);
            this.ngbInfo.Controls.SetChildIndex(this.nlbInfo2, 0);
            this.ngbInfo.Controls.SetChildIndex(this.nlbInfo1, 0);
            this.ngbInfo.Controls.SetChildIndex(this.neuLabel4, 0);
            this.ngbInfo.Controls.SetChildIndex(this.nlbInfo, 0);
            this.ngbInfo.Controls.SetChildIndex(this.nlbAdustInfo, 0);
            // 
            // neuPanelData
            // 
            this.neuPanelData.Controls.Add(this.nlbAdjustType);
            this.neuPanelData.Controls.Add(this.nlbExcuteInfo2);
            this.neuPanelData.Controls.Add(this.ncbExecuteTime);
            this.neuPanelData.Controls.Add(this.nlbExcuteInfo1);
            this.neuPanelData.Controls.Add(this.ndtpExecuteTime);
            this.neuPanelData.Size = new System.Drawing.Size(954, 442);
            this.neuPanelData.Controls.SetChildIndex(this.ucDataDetail, 0);
            this.neuPanelData.Controls.SetChildIndex(this.ndtpExecuteTime, 0);
            this.neuPanelData.Controls.SetChildIndex(this.nlbExcuteInfo1, 0);
            this.neuPanelData.Controls.SetChildIndex(this.ncbExecuteTime, 0);
            this.neuPanelData.Controls.SetChildIndex(this.nlbExcuteInfo2, 0);
            this.neuPanelData.Controls.SetChildIndex(this.nlbAdjustType, 0);
            // 
            // ngbLeftInfo
            // 
            this.ngbLeftInfo.Controls.Add(this.neuLabel3);
            this.ngbLeftInfo.Controls.Add(this.txtfilter);
            this.ngbLeftInfo.Location = new System.Drawing.Point(0, 390);
            this.ngbLeftInfo.Size = new System.Drawing.Size(302, 134);
            this.ngbLeftInfo.Controls.SetChildIndex(this.neuLabel1, 0);
            this.ngbLeftInfo.Controls.SetChildIndex(this.ndtpBegin, 0);
            this.ngbLeftInfo.Controls.SetChildIndex(this.neuLabel2, 0);
            this.ngbLeftInfo.Controls.SetChildIndex(this.ndtpEnd, 0);
            this.ngbLeftInfo.Controls.SetChildIndex(this.txtfilter, 0);
            this.ngbLeftInfo.Controls.SetChildIndex(this.neuLabel3, 0);
            // 
            // neuPanelLeftChoose
            // 
            this.neuPanelLeftChoose.Size = new System.Drawing.Size(302, 390);
            // 
            // ncbExecuteTime
            // 
            this.ncbExecuteTime.AutoSize = true;
            this.ncbExecuteTime.ForeColor = System.Drawing.Color.Blue;
            this.ncbExecuteTime.Location = new System.Drawing.Point(366, 28);
            this.ncbExecuteTime.Name = "ncbExecuteTime";
            this.ncbExecuteTime.Size = new System.Drawing.Size(84, 16);
            this.ncbExecuteTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbExecuteTime.TabIndex = 15;
            this.ncbExecuteTime.Text = "本此调价于";
            this.ncbExecuteTime.UseVisualStyleBackColor = true;
            // 
            // nlbExcuteInfo1
            // 
            this.nlbExcuteInfo1.AutoSize = true;
            this.nlbExcuteInfo1.ForeColor = System.Drawing.Color.Blue;
            this.nlbExcuteInfo1.Location = new System.Drawing.Point(609, 31);
            this.nlbExcuteInfo1.Name = "nlbExcuteInfo1";
            this.nlbExcuteInfo1.Size = new System.Drawing.Size(29, 12);
            this.nlbExcuteInfo1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbExcuteInfo1.TabIndex = 17;
            this.nlbExcuteInfo1.Text = "执行";
            // 
            // ndtpExecuteTime
            // 
            this.ndtpExecuteTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.ndtpExecuteTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ndtpExecuteTime.IsEnter2Tab = false;
            this.ndtpExecuteTime.Location = new System.Drawing.Point(455, 24);
            this.ndtpExecuteTime.Name = "ndtpExecuteTime";
            this.ndtpExecuteTime.Size = new System.Drawing.Size(145, 21);
            this.ndtpExecuteTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ndtpExecuteTime.TabIndex = 16;
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
            // nlbExcuteInfo2
            // 
            this.nlbExcuteInfo2.AutoSize = true;
            this.nlbExcuteInfo2.ForeColor = System.Drawing.Color.Blue;
            this.nlbExcuteInfo2.Location = new System.Drawing.Point(644, 30);
            this.nlbExcuteInfo2.Name = "nlbExcuteInfo2";
            this.nlbExcuteInfo2.Size = new System.Drawing.Size(245, 12);
            this.nlbExcuteInfo2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbExcuteInfo2.TabIndex = 18;
            this.nlbExcuteInfo2.Text = "如果不设置执行时间则在保存时立即执行调价";
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
            // nlbAdjustType
            // 
            this.nlbAdjustType.AutoSize = true;
            this.nlbAdjustType.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlbAdjustType.ForeColor = System.Drawing.Color.Blue;
            this.nlbAdjustType.Location = new System.Drawing.Point(202, 24);
            this.nlbAdjustType.Name = "nlbAdjustType";
            this.nlbAdjustType.Size = new System.Drawing.Size(49, 19);
            this.nlbAdjustType.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbAdjustType.TabIndex = 19;
            this.nlbAdjustType.Text = "执行";
            // 
            // txtfilter
            // 
            this.txtfilter.IsEnter2Tab = false;
            this.txtfilter.Location = new System.Drawing.Point(108, 76);
            this.txtfilter.Name = "txtfilter";
            this.txtfilter.Size = new System.Drawing.Size(146, 21);
            this.txtfilter.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtfilter.TabIndex = 13;
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel3.Location = new System.Drawing.Point(35, 80);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(65, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 18;
            this.neuLabel3.Text = "过    滤：";
            // 
            // AdjustPrice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "AdjustPrice";
            this.Size = new System.Drawing.Size(1259, 524);
            this.neuPanelLeft.ResumeLayout(false);
            this.neuPanelMain.ResumeLayout(false);
            this.ngbInfo.ResumeLayout(false);
            this.ngbInfo.PerformLayout();
            this.neuPanelData.ResumeLayout(false);
            this.neuPanelData.PerformLayout();
            this.ngbLeftInfo.ResumeLayout(false);
            this.ngbLeftInfo.PerformLayout();
            this.neuPanelLeftChoose.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbExecuteTime;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbExcuteInfo1;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker ndtpExecuteTime;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbExcuteInfo2;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbInfo;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbAdustInfo;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbAdjustType;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtfilter;

    }
}
