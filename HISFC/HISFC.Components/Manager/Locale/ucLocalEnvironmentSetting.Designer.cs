namespace FS.HISFC.Components.Manager.Locale
{
    partial class ucLocalEnvironmentSetting
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
            base.Dispose( disposing );
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvHosList = new FS.FrameWork.WinForms.Controls.NeuTreeView();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.lbInformation = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.neuGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point( 0, 0 );
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add( this.tvHosList );
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add( this.neuGroupBox1 );
            this.splitContainer1.Size = new System.Drawing.Size( 672, 397 );
            this.splitContainer1.SplitterDistance = 193;
            this.splitContainer1.TabIndex = 0;
            // 
            // tvHosList
            // 
            this.tvHosList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvHosList.HideSelection = false;
            this.tvHosList.Location = new System.Drawing.Point( 0, 0 );
            this.tvHosList.Name = "tvHosList";
            this.tvHosList.Size = new System.Drawing.Size( 193, 397 );
            this.tvHosList.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tvHosList.TabIndex = 0;
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add( this.lbInformation );
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox1.Location = new System.Drawing.Point( 0, 0 );
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size( 475, 42 );
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 1;
            this.neuGroupBox1.TabStop = false;
            // 
            // lbInformation
            // 
            this.lbInformation.AutoSize = true;
            this.lbInformation.Location = new System.Drawing.Point( 16, 17 );
            this.lbInformation.Name = "lbInformation";
            this.lbInformation.Size = new System.Drawing.Size( 143, 12 );
            this.lbInformation.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbInformation.TabIndex = 0;
            this.lbInformation.Text = "HosName  控制参数共计：";
            // 
            // ucLocalControlParamSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 12F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.splitContainer1 );
            this.Name = "ucLocalControlParamSetting";
            this.Size = new System.Drawing.Size( 672, 397 );
            this.splitContainer1.Panel1.ResumeLayout( false );
            this.splitContainer1.Panel2.ResumeLayout( false );
            this.splitContainer1.ResumeLayout( false );
            this.neuGroupBox1.ResumeLayout( false );
            this.neuGroupBox1.PerformLayout();
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private FS.FrameWork.WinForms.Controls.NeuTreeView tvHosList;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbInformation;
    }
}
