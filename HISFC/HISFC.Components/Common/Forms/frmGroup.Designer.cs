namespace FS.HISFC.Components.Common.Forms
{
    partial class frmGroup
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
            this.components = new System.ComponentModel.Container();
            this.tvGroup = new FS.HISFC.Components.Common.Controls.baseTreeView();
            this.SuspendLayout();
            // 
            // tvGroup
            // 
            this.tvGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvGroup.HideSelection = false;
            this.tvGroup.Location = new System.Drawing.Point(0, 0);
            this.tvGroup.Name = "tvGroup";
            this.tvGroup.Size = new System.Drawing.Size(284, 262);
            this.tvGroup.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D; 
            this.tvGroup.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvGroup_NodeMouseDoubleClick);           
            this.tvGroup.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.tvGroup);
            this.Name = "Form1";
            this.Text = "组套";
            this.ResumeLayout(false);

        }

        #endregion

        private FS.HISFC.Components.Common.Controls.baseTreeView tvGroup;
    }
}