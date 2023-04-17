namespace FS.HISFC.Components.Operation
{
    partial class ucCancelArrangement
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
            this.neuPanelMain = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.ucArrangementSpread1 = new FS.HISFC.Components.Operation.ucArrangementSpread();
            this.neuPanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanelMain
            // 
            this.neuPanelMain.Controls.Add(this.ucArrangementSpread1);
            this.neuPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanelMain.Location = new System.Drawing.Point(0, 0);
            this.neuPanelMain.Name = "neuPanelMain";
            this.neuPanelMain.Size = new System.Drawing.Size(1132, 611);
            this.neuPanelMain.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanelMain.TabIndex = 0;
            // 
            // ucArrangementSpread1
            // 
            this.ucArrangementSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucArrangementSpread1.Filter = FS.HISFC.Components.Operation.ucArrangementSpread.EnumFilter.All;
            this.ucArrangementSpread1.Location = new System.Drawing.Point(0, 0);
            this.ucArrangementSpread1.Name = "ucArrangementSpread1";
            this.ucArrangementSpread1.Size = new System.Drawing.Size(1132, 611);
            this.ucArrangementSpread1.TabIndex = 0;
            // 
            // ucCancelArrangement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.neuPanelMain);
            this.Name = "ucCancelArrangement";
            this.Size = new System.Drawing.Size(1132, 611);
            this.neuPanelMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanelMain;
        private ucArrangementSpread ucArrangementSpread1;
    }
}