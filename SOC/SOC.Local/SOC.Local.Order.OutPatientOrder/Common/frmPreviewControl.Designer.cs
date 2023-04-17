namespace FS.SOC.Local.Order.OutPatientOrder.Common
{
    partial class frmPreviewControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPreviewControl));
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.clbPreview = new System.Windows.Forms.CheckedListBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tbAllPage = new System.Windows.Forms.ToolStripButton();
            this.tbCurrenPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tbFrontPage = new System.Windows.Forms.ToolStripButton();
            this.tbNextPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tbExit = new System.Windows.Forms.ToolStripButton();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.neuPanel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.printPreviewControl1 = new System.Windows.Forms.PrintPreviewControl();
            this.neuPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            this.neuPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.clbPreview);
            this.neuPanel1.Controls.Add(this.neuLabel1);
            this.neuPanel1.Controls.Add(this.toolStrip1);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(806, 55);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // clbPreview
            // 
            this.clbPreview.CheckOnClick = true;
            this.clbPreview.ColumnWidth = 100;
            this.clbPreview.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.clbPreview.FormattingEnabled = true;
            this.clbPreview.HorizontalScrollbar = true;
            this.clbPreview.Location = new System.Drawing.Point(485, 2);
            this.clbPreview.MultiColumn = true;
            this.clbPreview.Name = "clbPreview";
            this.clbPreview.Size = new System.Drawing.Size(318, 52);
            this.clbPreview.TabIndex = 8;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel1.Location = new System.Drawing.Point(351, 20);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(105, 19);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 6;
            this.neuLabel1.Text = "第1页共2页";
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Font = new System.Drawing.Font("宋体", 12.5F);
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbAllPage,
            this.tbCurrenPage,
            this.toolStripSeparator1,
            this.tbFrontPage,
            this.tbNextPage,
            this.toolStripSeparator2,
            this.tbExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(806, 54);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // tbAllPage
            // 
            this.tbAllPage.Image = ((System.Drawing.Image)(resources.GetObject("tbAllPage.Image")));
            this.tbAllPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbAllPage.Name = "tbAllPage";
            this.tbAllPage.Size = new System.Drawing.Size(80, 51);
            this.tbAllPage.Text = "打印勾选";
            this.tbAllPage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbCurrenPage
            // 
            this.tbCurrenPage.Image = ((System.Drawing.Image)(resources.GetObject("tbCurrenPage.Image")));
            this.tbCurrenPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbCurrenPage.Name = "tbCurrenPage";
            this.tbCurrenPage.Size = new System.Drawing.Size(80, 51);
            this.tbCurrenPage.Text = "打印当前";
            this.tbCurrenPage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 54);
            // 
            // tbFrontPage
            // 
            this.tbFrontPage.Image = ((System.Drawing.Image)(resources.GetObject("tbFrontPage.Image")));
            this.tbFrontPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbFrontPage.Name = "tbFrontPage";
            this.tbFrontPage.Size = new System.Drawing.Size(63, 51);
            this.tbFrontPage.Text = "上一页";
            this.tbFrontPage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tbNextPage
            // 
            this.tbNextPage.Image = ((System.Drawing.Image)(resources.GetObject("tbNextPage.Image")));
            this.tbNextPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbNextPage.Name = "tbNextPage";
            this.tbNextPage.Size = new System.Drawing.Size(63, 51);
            this.tbNextPage.Text = "下一页";
            this.tbNextPage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 54);
            // 
            // tbExit
            // 
            this.tbExit.Image = ((System.Drawing.Image)(resources.GetObject("tbExit.Image")));
            this.tbExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbExit.Name = "tbExit";
            this.tbExit.Size = new System.Drawing.Size(46, 51);
            this.tbExit.Text = "关闭";
            this.tbExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // neuPanel2
            // 
            this.neuPanel2.BackColor = System.Drawing.Color.White;
            this.neuPanel2.Controls.Add(this.checkedListBox1);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel2.Location = new System.Drawing.Point(0, 55);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(806, 55);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 1;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.ColumnWidth = 150;
            this.checkedListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBox1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(0, 0);
            this.checkedListBox1.MultiColumn = true;
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(806, 52);
            this.checkedListBox1.TabIndex = 0;
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // neuPanel3
            // 
            this.neuPanel3.Controls.Add(this.printPreviewControl1);
            this.neuPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel3.Location = new System.Drawing.Point(0, 110);
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size(806, 500);
            this.neuPanel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 2;
            // 
            // printPreviewControl1
            // 
            this.printPreviewControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.printPreviewControl1.Location = new System.Drawing.Point(0, 0);
            this.printPreviewControl1.Name = "printPreviewControl1";
            this.printPreviewControl1.Size = new System.Drawing.Size(806, 500);
            this.printPreviewControl1.TabIndex = 0;
            // 
            // frmPreviewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 610);
            this.Controls.Add(this.neuPanel3);
            this.Controls.Add(this.neuPanel2);
            this.Controls.Add(this.neuPanel1);
            this.Name = "frmPreviewControl";
            this.Text = "打印预览";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPreviewControl_Load);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.neuPanel2.ResumeLayout(false);
            this.neuPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tbAllPage;
        private System.Windows.Forms.ToolStripButton tbCurrenPage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tbFrontPage;
        private System.Windows.Forms.ToolStripButton tbNextPage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tbExit;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private System.Windows.Forms.PrintPreviewControl printPreviewControl1;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.CheckedListBox clbPreview;

    }
}