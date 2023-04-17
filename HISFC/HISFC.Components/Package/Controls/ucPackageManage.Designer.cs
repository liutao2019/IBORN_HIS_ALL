namespace HISFC.Components.Package.Controls
{
    partial class ucPackageManage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucPackageManage));
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkIsShowInvalid = new System.Windows.Forms.CheckBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.ucPackgeDetail1 = new HISFC.Components.Package.Controls.ucPackgeDetail();
            this.tvPackageNew = new HISFC.Components.Package.Components.WTreeListWinEx();
            this.tvCategoryNew = new HISFC.Components.Package.Components.WTreeListWinEx();
            this.pnlLeft.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.tvPackageNew);
            this.pnlLeft.Controls.Add(this.panel2);
            this.pnlLeft.Controls.Add(this.splitter3);
            this.pnlLeft.Controls.Add(this.tvCategoryNew);
            this.pnlLeft.Controls.Add(this.pnlTop);
            this.pnlLeft.Controls.Add(this.panel1);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(3);
            this.pnlLeft.Size = new System.Drawing.Size(244, 585);
            this.pnlLeft.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 278);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(238, 22);
            this.panel2.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(236, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "套餐列表";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitter3
            // 
            this.splitter3.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.splitter3.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter3.Location = new System.Drawing.Point(3, 275);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(238, 3);
            this.splitter3.TabIndex = 14;
            this.splitter3.TabStop = false;
            // 
            // pnlTop
            // 
            this.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTop.Controls.Add(this.label1);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(3, 3);
            this.pnlTop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(238, 24);
            this.pnlTop.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(236, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "套餐分类";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.chkIsShowInvalid);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 549);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(238, 33);
            this.panel1.TabIndex = 4;
            // 
            // chkIsShowInvalid
            // 
            this.chkIsShowInvalid.AutoSize = true;
            this.chkIsShowInvalid.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkIsShowInvalid.ForeColor = System.Drawing.Color.Red;
            this.chkIsShowInvalid.Location = new System.Drawing.Point(3, 4);
            this.chkIsShowInvalid.Name = "chkIsShowInvalid";
            this.chkIsShowInvalid.Size = new System.Drawing.Size(140, 23);
            this.chkIsShowInvalid.TabIndex = 0;
            this.chkIsShowInvalid.Text = "是否显示作废套餐";
            this.chkIsShowInvalid.UseVisualStyleBackColor = true;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.splitter1.Location = new System.Drawing.Point(244, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 585);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.ucPackgeDetail1);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(247, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(3);
            this.pnlRight.Size = new System.Drawing.Size(710, 585);
            this.pnlRight.TabIndex = 2;
            // 
            // ucPackgeDetail1
            // 
            this.ucPackgeDetail1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.ucPackgeDetail1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucPackgeDetail1.CurrentDetail = null;
            this.ucPackgeDetail1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPackgeDetail1.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucPackgeDetail1.IsFullConvertToHalf = true;
            this.ucPackgeDetail1.IsPrint = false;
            this.ucPackgeDetail1.Location = new System.Drawing.Point(3, 3);
            this.ucPackgeDetail1.Name = "ucPackgeDetail1";
            this.ucPackgeDetail1.ParentFormToolBar = null;
            this.ucPackgeDetail1.Size = new System.Drawing.Size(704, 579);
            this.ucPackgeDetail1.TabIndex = 0;
            this.ucPackgeDetail1.TotFee = new decimal(new int[] {
            0,
            0,
            0,
            0});
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
            this.tvPackageNew.Location = new System.Drawing.Point(3, 300);
            this.tvPackageNew.Name = "tvPackageNew";
            this.tvPackageNew.SelectedImageIndex = 0;
            this.tvPackageNew.ShowLines = false;
            this.tvPackageNew.Size = new System.Drawing.Size(238, 249);
            this.tvPackageNew.TabIndex = 16;
            // 
            // tvCategoryNew
            // 
            this.tvCategoryNew.Dock = System.Windows.Forms.DockStyle.Top;
            this.tvCategoryNew.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.tvCategoryNew.FullRowSelect = true;
            this.tvCategoryNew.HideSelection = false;
            this.tvCategoryNew.HotTracking = true;
            this.tvCategoryNew.ImageIndex = 0;
            this.tvCategoryNew.Indent = 19;
            this.tvCategoryNew.ItemHeight = 20;
            this.tvCategoryNew.Location = new System.Drawing.Point(3, 27);
            this.tvCategoryNew.Name = "tvCategoryNew";
            this.tvCategoryNew.SelectedImageIndex = 0;
            this.tvCategoryNew.ShowLines = false;
            this.tvCategoryNew.Size = new System.Drawing.Size(238, 248);
            this.tvCategoryNew.TabIndex = 13;
            // 
            // ucPackageManage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.pnlLeft);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ucPackageManage";
            this.Size = new System.Drawing.Size(957, 585);
            this.pnlLeft.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlRight.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkIsShowInvalid;
        public HISFC.Components.Package.Components.WTreeListWinEx tvPackageNew;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Splitter splitter3;
        public HISFC.Components.Package.Components.WTreeListWinEx tvCategoryNew;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label label1;
        private ucPackgeDetail ucPackgeDetail1;
    }
}
