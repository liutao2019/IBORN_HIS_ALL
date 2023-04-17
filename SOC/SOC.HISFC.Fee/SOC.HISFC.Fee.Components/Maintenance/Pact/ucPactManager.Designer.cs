namespace FS.SOC.HISFC.Fee.Components.Maintenance.Pact
{
    partial class ucPactManager
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
            this.neuSplitter1 = new FS.FrameWork.WinForms.Controls.NeuSplitter();
            this.gbPropertyGrid = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gbFeeCodeRateDetail = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.gbPactInfo = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuSplitter1
            // 
            this.neuSplitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.neuSplitter1.Location = new System.Drawing.Point(517, 0);
            this.neuSplitter1.Name = "neuSplitter1";
            this.neuSplitter1.Size = new System.Drawing.Size(3, 500);
            this.neuSplitter1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSplitter1.TabIndex = 11;
            this.neuSplitter1.TabStop = false;
            // 
            // gbPropertyGrid
            // 
            this.gbPropertyGrid.Dock = System.Windows.Forms.DockStyle.Right;
            this.gbPropertyGrid.ForeColor = System.Drawing.Color.Blue;
            this.gbPropertyGrid.Location = new System.Drawing.Point(520, 0);
            this.gbPropertyGrid.Name = "gbPropertyGrid";
            this.gbPropertyGrid.Size = new System.Drawing.Size(287, 500);
            this.gbPropertyGrid.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbPropertyGrid.TabIndex = 10;
            this.gbPropertyGrid.TabStop = false;
            this.gbPropertyGrid.Text = "属性";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(517, 500);
            this.tabControl1.TabIndex = 12;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gbPactInfo);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(509, 474);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "合同单位";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.gbFeeCodeRateDetail);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(509, 474);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "项目明细";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // gbFeeCodeRateDetail
            // 
            this.gbFeeCodeRateDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbFeeCodeRateDetail.ForeColor = System.Drawing.Color.Blue;
            this.gbFeeCodeRateDetail.Location = new System.Drawing.Point(3, 3);
            this.gbFeeCodeRateDetail.Name = "gbFeeCodeRateDetail";
            this.gbFeeCodeRateDetail.Size = new System.Drawing.Size(503, 468);
            this.gbFeeCodeRateDetail.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbFeeCodeRateDetail.TabIndex = 18;
            this.gbFeeCodeRateDetail.TabStop = false;
            // 
            // gbPactInfo
            // 
            this.gbPactInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbPactInfo.ForeColor = System.Drawing.Color.Blue;
            this.gbPactInfo.Location = new System.Drawing.Point(3, 3);
            this.gbPactInfo.Name = "gbPactInfo";
            this.gbPactInfo.Size = new System.Drawing.Size(503, 468);
            this.gbPactInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbPactInfo.TabIndex = 15;
            this.gbPactInfo.TabStop = false;
            // 
            // ucPactManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.neuSplitter1);
            this.Controls.Add(this.gbPropertyGrid);
            this.Name = "ucPactManager";
            this.Size = new System.Drawing.Size(807, 500);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuSplitter neuSplitter1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbPropertyGrid;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbPactInfo;
        private System.Windows.Forms.TabPage tabPage2;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbFeeCodeRateDetail;


    }
}
