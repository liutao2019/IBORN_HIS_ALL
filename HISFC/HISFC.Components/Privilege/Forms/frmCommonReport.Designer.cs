namespace FS.HISFC.Components.Privilege.Forms
{
    partial class frmCommonReport
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
                controls.Clear();
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCommonReport));
            this.TvReports = new System.Windows.Forms.TreeView();
            this.groupImageList = new System.Windows.Forms.ImageList(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelTree.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTree
            // 
            this.panelTree.Controls.Add(this.TvReports);
            // 
            // neuTextBox1
            // 
            this.neuTextBox1.BackColor = System.Drawing.Color.White;
            // 
            // TvReports
            // 
            this.TvReports.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TvReports.ImageIndex = 2;
            this.TvReports.ImageList = this.groupImageList;
            this.TvReports.Location = new System.Drawing.Point(0, 0);
            this.TvReports.Name = "TvReports";
            this.TvReports.SelectedImageKey = "group5.ico";
            this.TvReports.Size = new System.Drawing.Size(206, 359);
            this.TvReports.TabIndex = 2;
            this.TvReports.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TvReports_AfterSelect);
            // 
            // groupImageList
            // 
            this.groupImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("groupImageList.ImageStream")));
            this.groupImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.groupImageList.Images.SetKeyName(0, "group1.ICO");
            this.groupImageList.Images.SetKeyName(1, "group2.ICO");
            this.groupImageList.Images.SetKeyName(2, "group3.ico");
            this.groupImageList.Images.SetKeyName(3, "group4.ico");
            this.groupImageList.Images.SetKeyName(4, "group5.ico");
            // 
            // frmCommonReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 486);
            this.Name = "frmCommonReport";
            this.Text = "frmCommonReport";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelTree.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView TvReports;
        public System.Windows.Forms.ImageList groupImageList;
    }
}