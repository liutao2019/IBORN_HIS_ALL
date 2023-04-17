namespace FS.SOC.HISFC.Components.Pharmacy.Common.Check
{
    partial class ucBaseCheck
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
            this.ngbInfo = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.nlbColorReset = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbInfo3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbInfo2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbInfo1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbPriveDept = new System.Windows.Forms.Label();
            this.npanelData = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.npanelDetail = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSplitter1 = new FS.FrameWork.WinForms.Controls.NeuSplitter();
            this.npanelChooseList = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.ncbApplyFilterSetting = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ngbInfo.SuspendLayout();
            this.npanelData.SuspendLayout();
            this.SuspendLayout();
            // 
            // ngbInfo
            // 
            this.ngbInfo.Controls.Add(this.ncbApplyFilterSetting);
            this.ngbInfo.Controls.Add(this.nlbColorReset);
            this.ngbInfo.Controls.Add(this.nlbInfo3);
            this.ngbInfo.Controls.Add(this.nlbInfo2);
            this.ngbInfo.Controls.Add(this.nlbInfo1);
            this.ngbInfo.Controls.Add(this.nlbPriveDept);
            this.ngbInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ngbInfo.Location = new System.Drawing.Point(0, 472);
            this.ngbInfo.Name = "ngbInfo";
            this.ngbInfo.Size = new System.Drawing.Size(811, 51);
            this.ngbInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ngbInfo.TabIndex = 11;
            this.ngbInfo.TabStop = false;
            this.ngbInfo.Text = "附加信息";
            // 
            // nlbColorReset
            // 
            this.nlbColorReset.AutoSize = true;
            this.nlbColorReset.BackColor = System.Drawing.Color.Transparent;
            this.nlbColorReset.ForeColor = System.Drawing.Color.Blue;
            this.nlbColorReset.Location = new System.Drawing.Point(348, 20);
            this.nlbColorReset.Name = "nlbColorReset";
            this.nlbColorReset.Size = new System.Drawing.Size(65, 12);
            this.nlbColorReset.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbColorReset.TabIndex = 4;
            this.nlbColorReset.Text = "回车重置：";
            this.nlbColorReset.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nlbInfo3
            // 
            this.nlbInfo3.AutoSize = true;
            this.nlbInfo3.BackColor = System.Drawing.Color.Transparent;
            this.nlbInfo3.ForeColor = System.Drawing.Color.Black;
            this.nlbInfo3.Location = new System.Drawing.Point(541, 20);
            this.nlbInfo3.Name = "nlbInfo3";
            this.nlbInfo3.Size = new System.Drawing.Size(65, 12);
            this.nlbInfo3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbInfo3.TabIndex = 3;
            this.nlbInfo3.Text = "黑色无盈亏";
            this.nlbInfo3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nlbInfo2
            // 
            this.nlbInfo2.AutoSize = true;
            this.nlbInfo2.BackColor = System.Drawing.Color.Transparent;
            this.nlbInfo2.ForeColor = System.Drawing.Color.Blue;
            this.nlbInfo2.Location = new System.Drawing.Point(480, 20);
            this.nlbInfo2.Name = "nlbInfo2";
            this.nlbInfo2.Size = new System.Drawing.Size(53, 12);
            this.nlbInfo2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbInfo2.TabIndex = 2;
            this.nlbInfo2.Text = "蓝色盘盈";
            this.nlbInfo2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nlbInfo1
            // 
            this.nlbInfo1.AutoSize = true;
            this.nlbInfo1.BackColor = System.Drawing.Color.Transparent;
            this.nlbInfo1.ForeColor = System.Drawing.Color.Red;
            this.nlbInfo1.Location = new System.Drawing.Point(419, 20);
            this.nlbInfo1.Name = "nlbInfo1";
            this.nlbInfo1.Size = new System.Drawing.Size(53, 12);
            this.nlbInfo1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbInfo1.TabIndex = 1;
            this.nlbInfo1.Text = "红色盘亏";
            this.nlbInfo1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nlbPriveDept
            // 
            this.nlbPriveDept.AutoSize = true;
            this.nlbPriveDept.ForeColor = System.Drawing.Color.Blue;
            this.nlbPriveDept.Location = new System.Drawing.Point(10, 20);
            this.nlbPriveDept.Name = "nlbPriveDept";
            this.nlbPriveDept.Size = new System.Drawing.Size(0, 12);
            this.nlbPriveDept.TabIndex = 0;
            // 
            // npanelData
            // 
            this.npanelData.Controls.Add(this.npanelDetail);
            this.npanelData.Controls.Add(this.neuSplitter1);
            this.npanelData.Controls.Add(this.npanelChooseList);
            this.npanelData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.npanelData.Location = new System.Drawing.Point(0, 0);
            this.npanelData.Name = "npanelData";
            this.npanelData.Size = new System.Drawing.Size(811, 472);
            this.npanelData.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.npanelData.TabIndex = 12;
            // 
            // npanelDetail
            // 
            this.npanelDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.npanelDetail.Location = new System.Drawing.Point(296, 0);
            this.npanelDetail.Name = "npanelDetail";
            this.npanelDetail.Size = new System.Drawing.Size(515, 472);
            this.npanelDetail.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.npanelDetail.TabIndex = 2;
            // 
            // neuSplitter1
            // 
            this.neuSplitter1.Location = new System.Drawing.Point(293, 0);
            this.neuSplitter1.Name = "neuSplitter1";
            this.neuSplitter1.Size = new System.Drawing.Size(3, 472);
            this.neuSplitter1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSplitter1.TabIndex = 1;
            this.neuSplitter1.TabStop = false;
            // 
            // npanelChooseList
            // 
            this.npanelChooseList.Dock = System.Windows.Forms.DockStyle.Left;
            this.npanelChooseList.Location = new System.Drawing.Point(0, 0);
            this.npanelChooseList.Name = "npanelChooseList";
            this.npanelChooseList.Size = new System.Drawing.Size(293, 472);
            this.npanelChooseList.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.npanelChooseList.TabIndex = 0;
            // 
            // ncbApplyFilterSetting
            // 
            this.ncbApplyFilterSetting.AutoSize = true;
            this.ncbApplyFilterSetting.Checked = true;
            this.ncbApplyFilterSetting.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ncbApplyFilterSetting.Location = new System.Drawing.Point(658, 20);
            this.ncbApplyFilterSetting.Name = "ncbApplyFilterSetting";
            this.ncbApplyFilterSetting.Size = new System.Drawing.Size(72, 16);
            this.ncbApplyFilterSetting.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbApplyFilterSetting.TabIndex = 5;
            this.ncbApplyFilterSetting.Text = "启用筛选";
            this.ncbApplyFilterSetting.UseVisualStyleBackColor = true;
            // 
            // ucBaseCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.npanelData);
            this.Controls.Add(this.ngbInfo);
            this.Name = "ucBaseCheck";
            this.Size = new System.Drawing.Size(811, 523);
            this.ngbInfo.ResumeLayout(false);
            this.ngbInfo.PerformLayout();
            this.npanelData.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected FS.FrameWork.WinForms.Controls.NeuGroupBox ngbInfo;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbInfo1;
        protected System.Windows.Forms.Label nlbPriveDept;
        protected FS.FrameWork.WinForms.Controls.NeuPanel npanelData;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbInfo3;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbInfo2;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbColorReset;
        private FS.FrameWork.WinForms.Controls.NeuPanel npanelDetail;
        private FS.FrameWork.WinForms.Controls.NeuSplitter neuSplitter1;
        private FS.FrameWork.WinForms.Controls.NeuPanel npanelChooseList;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbApplyFilterSetting;
    }
}
