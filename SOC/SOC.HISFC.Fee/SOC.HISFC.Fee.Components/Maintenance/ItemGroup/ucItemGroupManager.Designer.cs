namespace FS.SOC.HISFC.Fee.Components.Maintenance.ItemGroup
{
    partial class ucItemGroupManager
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

            if (this.IGroupQuery != null)
            {
                this.IGroupQuery.Dispose();
            }
            if (this.IGroupDetail != null)
            {
                this.IGroupDetail.Dispose();
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
            this.neuGroupBox2 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuLabel9 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.gbItemGroupQuery = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuSplitter1 = new FS.FrameWork.WinForms.Controls.NeuSplitter();
            this.gbItemGroupDetail = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.nTxtCustomCode = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuGroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuGroupBox2
            // 
            this.neuGroupBox2.Controls.Add(this.nTxtCustomCode);
            this.neuGroupBox2.Controls.Add(this.neuLabel9);
            this.neuGroupBox2.Controls.Add(this.nlbInfo);
            this.neuGroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuGroupBox2.Location = new System.Drawing.Point(0, 343);
            this.neuGroupBox2.Name = "neuGroupBox2";
            this.neuGroupBox2.Size = new System.Drawing.Size(665, 52);
            this.neuGroupBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox2.TabIndex = 3;
            this.neuGroupBox2.TabStop = false;
            this.neuGroupBox2.Text = "附加信息";
            // 
            // neuLabel9
            // 
            this.neuLabel9.AutoSize = true;
            this.neuLabel9.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel9.Location = new System.Drawing.Point(17, 25);
            this.neuLabel9.Name = "neuLabel9";
            this.neuLabel9.Size = new System.Drawing.Size(365, 12);
            this.neuLabel9.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel9.TabIndex = 24;
            this.neuLabel9.Text = "基本信息维护需要系统管理员授权，背景色为红色代表没有权限操作";
            // 
            // nlbInfo
            // 
            this.nlbInfo.AutoSize = true;
            this.nlbInfo.Location = new System.Drawing.Point(35, 26);
            this.nlbInfo.Name = "nlbInfo";
            this.nlbInfo.Size = new System.Drawing.Size(0, 12);
            this.nlbInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbInfo.TabIndex = 23;
            // 
            // gbItemGroupQuery
            // 
            this.gbItemGroupQuery.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbItemGroupQuery.Location = new System.Drawing.Point(0, 0);
            this.gbItemGroupQuery.Name = "gbItemGroupQuery";
            this.gbItemGroupQuery.Size = new System.Drawing.Size(252, 343);
            this.gbItemGroupQuery.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbItemGroupQuery.TabIndex = 4;
            this.gbItemGroupQuery.TabStop = false;
            this.gbItemGroupQuery.Text = "组套项目";
            // 
            // neuSplitter1
            // 
            this.neuSplitter1.Location = new System.Drawing.Point(252, 0);
            this.neuSplitter1.Name = "neuSplitter1";
            this.neuSplitter1.Size = new System.Drawing.Size(3, 343);
            this.neuSplitter1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSplitter1.TabIndex = 5;
            this.neuSplitter1.TabStop = false;
            // 
            // gbItemGroupDetail
            // 
            this.gbItemGroupDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbItemGroupDetail.Location = new System.Drawing.Point(255, 0);
            this.gbItemGroupDetail.Name = "gbItemGroupDetail";
            this.gbItemGroupDetail.Size = new System.Drawing.Size(410, 343);
            this.gbItemGroupDetail.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbItemGroupDetail.TabIndex = 6;
            this.gbItemGroupDetail.TabStop = false;
            this.gbItemGroupDetail.Text = "组套明细";
            // 
            // nTxtCustomCode
            // 
            this.nTxtCustomCode.IsEnter2Tab = false;
            this.nTxtCustomCode.Location = new System.Drawing.Point(388, 20);
            this.nTxtCustomCode.Name = "nTxtCustomCode";
            this.nTxtCustomCode.Size = new System.Drawing.Size(13, 21);
            this.nTxtCustomCode.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nTxtCustomCode.TabIndex = 25;
            // 
            // ucItemGroupManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbItemGroupDetail);
            this.Controls.Add(this.neuSplitter1);
            this.Controls.Add(this.gbItemGroupQuery);
            this.Controls.Add(this.neuGroupBox2);
            this.Name = "ucItemGroupManager";
            this.Size = new System.Drawing.Size(665, 395);
            this.neuGroupBox2.ResumeLayout(false);
            this.neuGroupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel9;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbInfo;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbItemGroupQuery;
        private FS.FrameWork.WinForms.Controls.NeuSplitter neuSplitter1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbItemGroupDetail;
        private FS.FrameWork.WinForms.Controls.NeuTextBox nTxtCustomCode;


    }
}
