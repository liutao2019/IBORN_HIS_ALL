namespace HISFC.Components.Package.Controls
{
    partial class ucPackageTree
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
            this.plTitle1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel4 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.chkIsShowInvalid = new System.Windows.Forms.CheckBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.plTitle2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.tvPackageNew = new HISFC.Components.Package.Components.WTreeListWinEx();
            this.tvCategoryNew = new HISFC.Components.Package.Components.WTreeListWinEx();
            this.plTitle1.SuspendLayout();
            this.neuPanel1.SuspendLayout();
            this.neuPanel4.SuspendLayout();
            this.neuPanel3.SuspendLayout();
            this.plTitle2.SuspendLayout();
            this.SuspendLayout();
            // 
            // plTitle1
            // 
            this.plTitle1.Controls.Add(this.label1);
            this.plTitle1.Dock = System.Windows.Forms.DockStyle.Top;
            this.plTitle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plTitle1.Location = new System.Drawing.Point(3, 3);
            this.plTitle1.Name = "plTitle1";
            this.plTitle1.Size = new System.Drawing.Size(309, 26);
            this.plTitle1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plTitle1.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(309, 26);
            this.label1.TabIndex = 13;
            this.label1.Text = "套餐分类：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.tvCategoryNew);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuPanel1.Location = new System.Drawing.Point(3, 29);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(309, 253);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 22;
            // 
            // neuPanel4
            // 
            this.neuPanel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.neuPanel4.Controls.Add(this.chkIsShowInvalid);
            this.neuPanel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuPanel4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuPanel4.Location = new System.Drawing.Point(3, 538);
            this.neuPanel4.Name = "neuPanel4";
            this.neuPanel4.Size = new System.Drawing.Size(309, 30);
            this.neuPanel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel4.TabIndex = 25;
            // 
            // neuPanel3
            // 
            this.neuPanel3.Controls.Add(this.tvPackageNew);
            this.neuPanel3.Controls.Add(this.plTitle2);
            this.neuPanel3.Controls.Add(this.splitter1);
            this.neuPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuPanel3.Location = new System.Drawing.Point(3, 282);
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size(309, 256);
            this.neuPanel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 26;
            // 
            // chkIsShowInvalid
            // 
            this.chkIsShowInvalid.AutoSize = true;
            this.chkIsShowInvalid.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkIsShowInvalid.ForeColor = System.Drawing.Color.Red;
            this.chkIsShowInvalid.Location = new System.Drawing.Point(3, 4);
            this.chkIsShowInvalid.Name = "chkIsShowInvalid";
            this.chkIsShowInvalid.Size = new System.Drawing.Size(140, 23);
            this.chkIsShowInvalid.TabIndex = 1;
            this.chkIsShowInvalid.Text = "是否显示作废套餐";
            this.chkIsShowInvalid.UseVisualStyleBackColor = true;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.Window;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(309, 3);
            this.splitter1.TabIndex = 24;
            this.splitter1.TabStop = false;
            // 
            // plTitle2
            // 
            this.plTitle2.Controls.Add(this.label2);
            this.plTitle2.Dock = System.Windows.Forms.DockStyle.Top;
            this.plTitle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plTitle2.Location = new System.Drawing.Point(0, 3);
            this.plTitle2.Name = "plTitle2";
            this.plTitle2.Size = new System.Drawing.Size(309, 26);
            this.plTitle2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plTitle2.TabIndex = 26;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(309, 26);
            this.label2.TabIndex = 20;
            this.label2.Text = "套餐列表：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tvPackageNew
            // 
            this.tvPackageNew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvPackageNew.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.tvPackageNew.FullRowSelect = true;
            this.tvPackageNew.HideSelection = false;
            this.tvPackageNew.HotTracking = true;
            this.tvPackageNew.ImageIndex = 0;
            this.tvPackageNew.Indent = 19;
            this.tvPackageNew.ItemHeight = 20;
            this.tvPackageNew.Location = new System.Drawing.Point(0, 29);
            this.tvPackageNew.Name = "tvPackageNew";
            this.tvPackageNew.SelectedImageIndex = 0;
            this.tvPackageNew.ShowLines = false;
            this.tvPackageNew.Size = new System.Drawing.Size(309, 227);
            this.tvPackageNew.TabIndex = 25;
            // 
            // tvCategoryNew
            // 
            this.tvCategoryNew.BackColor = System.Drawing.SystemColors.Window;
            this.tvCategoryNew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvCategoryNew.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.tvCategoryNew.FullRowSelect = true;
            this.tvCategoryNew.HideSelection = false;
            this.tvCategoryNew.HotTracking = true;
            this.tvCategoryNew.ImageIndex = 0;
            this.tvCategoryNew.Indent = 19;
            this.tvCategoryNew.ItemHeight = 20;
            this.tvCategoryNew.Location = new System.Drawing.Point(0, 0);
            this.tvCategoryNew.Name = "tvCategoryNew";
            this.tvCategoryNew.SelectedImageIndex = 0;
            this.tvCategoryNew.ShowLines = false;
            this.tvCategoryNew.Size = new System.Drawing.Size(309, 253);
            this.tvCategoryNew.TabIndex = 17;
            // 
            // ucPackageTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.neuPanel3);
            this.Controls.Add(this.neuPanel4);
            this.Controls.Add(this.neuPanel1);
            this.Controls.Add(this.plTitle1);
            this.Name = "ucPackageTree";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Size = new System.Drawing.Size(315, 571);
            this.plTitle1.ResumeLayout(false);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel4.ResumeLayout(false);
            this.neuPanel4.PerformLayout();
            this.neuPanel3.ResumeLayout(false);
            this.plTitle2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public HISFC.Components.Package.Components.WTreeListWinEx tvCategoryNew;
        private FS.FrameWork.WinForms.Controls.NeuPanel plTitle1;
        private System.Windows.Forms.Label label1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel4;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel3;
        private System.Windows.Forms.CheckBox chkIsShowInvalid;
        public HISFC.Components.Package.Components.WTreeListWinEx tvPackageNew;
        private FS.FrameWork.WinForms.Controls.NeuPanel plTitle2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Splitter splitter1;
    }
}
