namespace FS.SOC.Local.Account.GuangZhou.Zdly.OpenCard
{
    partial class ucPactSelect
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
            this.components = new System.ComponentModel.Container();
            this.tvUnselected = new FS.HISFC.Components.Common.Controls.baseTreeView();
            this.gbUnSelect = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuSplitter1 = new FS.FrameWork.WinForms.Controls.NeuSplitter();
            this.gbSelected = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.tvSelected = new FS.HISFC.Components.Common.Controls.baseTreeView();
            this.gbUnSelect.SuspendLayout();
            this.gbSelected.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvUnselected
            // 
            this.tvUnselected.AllowDrop = true;
            this.tvUnselected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvUnselected.HideSelection = false;
            this.tvUnselected.Location = new System.Drawing.Point(3, 17);
            this.tvUnselected.Name = "tvUnselected";
            this.tvUnselected.Size = new System.Drawing.Size(159, 230);
            this.tvUnselected.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tvUnselected.TabIndex = 0;
            // 
            // gbUnSelect
            // 
            this.gbUnSelect.Controls.Add(this.tvUnselected);
            this.gbUnSelect.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbUnSelect.Location = new System.Drawing.Point(0, 0);
            this.gbUnSelect.Name = "gbUnSelect";
            this.gbUnSelect.Size = new System.Drawing.Size(165, 250);
            this.gbUnSelect.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbUnSelect.TabIndex = 2;
            this.gbUnSelect.TabStop = false;
            this.gbUnSelect.Text = "待选";
            // 
            // neuSplitter1
            // 
            this.neuSplitter1.Location = new System.Drawing.Point(165, 0);
            this.neuSplitter1.Name = "neuSplitter1";
            this.neuSplitter1.Size = new System.Drawing.Size(3, 250);
            this.neuSplitter1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSplitter1.TabIndex = 4;
            this.neuSplitter1.TabStop = false;
            // 
            // gbSelected
            // 
            this.gbSelected.Controls.Add(this.tvSelected);
            this.gbSelected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbSelected.Location = new System.Drawing.Point(168, 0);
            this.gbSelected.Name = "gbSelected";
            this.gbSelected.Size = new System.Drawing.Size(164, 250);
            this.gbSelected.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbSelected.TabIndex = 5;
            this.gbSelected.TabStop = false;
            this.gbSelected.Text = "已选";
            // 
            // tvSelected
            // 
            this.tvSelected.AllowDrop = true;
            this.tvSelected.CheckBoxes = true;
            this.tvSelected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvSelected.HideSelection = false;
            this.tvSelected.Location = new System.Drawing.Point(3, 17);
            this.tvSelected.Name = "tvSelected";
            this.tvSelected.Size = new System.Drawing.Size(158, 230);
            this.tvSelected.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tvSelected.TabIndex = 1;
            // 
            // ucPactSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbSelected);
            this.Controls.Add(this.neuSplitter1);
            this.Controls.Add(this.gbUnSelect);
            this.Name = "ucPactSelect";
            this.Size = new System.Drawing.Size(332, 250);
            this.gbUnSelect.ResumeLayout(false);
            this.gbSelected.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.HISFC.Components.Common.Controls.baseTreeView tvUnselected;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbUnSelect;
        private FS.FrameWork.WinForms.Controls.NeuSplitter neuSplitter1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbSelected;
        private FS.HISFC.Components.Common.Controls.baseTreeView tvSelected;
    }
}
