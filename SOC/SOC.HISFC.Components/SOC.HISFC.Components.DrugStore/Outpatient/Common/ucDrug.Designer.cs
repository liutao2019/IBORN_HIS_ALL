namespace FS.SOC.HISFC.Components.DrugStore.Outpatient.Common
{
    partial class ucDrug
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
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.ncbAutoPrintRegister = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ncbPauseRefresh = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.nlbWorkLoadInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbTermialInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel1.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            this.ngbRecipeDetail.SuspendLayout();
            this.ngbAdd.SuspendLayout();
            this.neuGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucDrugTree1
            // 
            this.ucDrugTree1.Size = new System.Drawing.Size(233, 592);
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.neuGroupBox1);
            this.neuPanel1.Controls.SetChildIndex(this.neuGroupBox1, 0);
            this.neuPanel1.Controls.SetChildIndex(this.ucDrugTree1, 0);
            // 
            // ngbAdd
            // 
            this.ngbAdd.Controls.Add(this.nlbWorkLoadInfo);
            this.ngbAdd.Controls.Add(this.nlbTermialInfo);
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.ncbAutoPrintRegister);
            this.neuGroupBox1.Controls.Add(this.ncbPauseRefresh);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 592);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(233, 50);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 2;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "程序设置";
            // 
            // ncbAutoPrintRegister
            // 
            this.ncbAutoPrintRegister.AutoSize = true;
            this.ncbAutoPrintRegister.Location = new System.Drawing.Point(129, 20);
            this.ncbAutoPrintRegister.Name = "ncbAutoPrintRegister";
            this.ncbAutoPrintRegister.Size = new System.Drawing.Size(96, 16);
            this.ncbAutoPrintRegister.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbAutoPrintRegister.TabIndex = 1;
            this.ncbAutoPrintRegister.Text = "自动打印注册";
            this.ncbAutoPrintRegister.UseVisualStyleBackColor = true;
            // 
            // ncbPauseRefresh
            // 
            this.ncbPauseRefresh.AutoSize = true;
            this.ncbPauseRefresh.Location = new System.Drawing.Point(20, 20);
            this.ncbPauseRefresh.Name = "ncbPauseRefresh";
            this.ncbPauseRefresh.Size = new System.Drawing.Size(96, 16);
            this.ncbPauseRefresh.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbPauseRefresh.TabIndex = 0;
            this.ncbPauseRefresh.Text = "暂停自动刷新";
            this.ncbPauseRefresh.UseVisualStyleBackColor = true;
            // 
            // nlbWorkLoadInfo
            // 
            this.nlbWorkLoadInfo.AutoSize = true;
            this.nlbWorkLoadInfo.ForeColor = System.Drawing.Color.Blue;
            this.nlbWorkLoadInfo.Location = new System.Drawing.Point(353, 20);
            this.nlbWorkLoadInfo.Name = "nlbWorkLoadInfo";
            this.nlbWorkLoadInfo.Size = new System.Drawing.Size(65, 12);
            this.nlbWorkLoadInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbWorkLoadInfo.TabIndex = 7;
            this.nlbWorkLoadInfo.Text = "工作量情况";
            // 
            // nlbTermialInfo
            // 
            this.nlbTermialInfo.AutoSize = true;
            this.nlbTermialInfo.ForeColor = System.Drawing.Color.Blue;
            this.nlbTermialInfo.Location = new System.Drawing.Point(16, 20);
            this.nlbTermialInfo.Name = "nlbTermialInfo";
            this.nlbTermialInfo.Size = new System.Drawing.Size(53, 12);
            this.nlbTermialInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbTermialInfo.TabIndex = 6;
            this.nlbTermialInfo.Text = "终端信息";
            // 
            // ucDrug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucDrug";
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel2.ResumeLayout(false);
            this.ngbRecipeDetail.ResumeLayout(false);
            this.ngbAdd.ResumeLayout(false);
            this.ngbAdd.PerformLayout();
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbAutoPrintRegister;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbPauseRefresh;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbWorkLoadInfo;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbTermialInfo;
    }
}
