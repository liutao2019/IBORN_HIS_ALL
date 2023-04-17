namespace Neusoft.HISFC.Components.Operation
{
    partial class frmChangeApply
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
            this.btnOK = new Neusoft.FrameWork.WinForms.Controls.NeuButton();
            this.ucArrangementSpread1 = new Neusoft.HISFC.Components.Operation.ucArrangementSpread();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(4, 309);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(117, 37);
            this.btnOK.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnOK.TabIndex = 18;
            this.btnOK.Text = "确定(&O)";
            this.btnOK.Type = Neusoft.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // ucArrangementSpread1
            // 
            this.ucArrangementSpread1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucArrangementSpread1.Filter = Neusoft.HISFC.Components.Operation.ucArrangementSpread.EnumFilter.All;
            this.ucArrangementSpread1.Location = new System.Drawing.Point(0, 0);
            this.ucArrangementSpread1.Name = "ucArrangementSpread1";
            this.ucArrangementSpread1.Size = new System.Drawing.Size(1044, 292);
            this.ucArrangementSpread1.TabIndex = 19;
            // 
            // frmChangeApply
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1044, 351);
            this.Controls.Add(this.ucArrangementSpread1);
            this.Controls.Add(this.btnOK);
            this.KeyPreview = true;
            this.Name = "frmChangeApply";
            this.Text = "更新门诊手术记录";
            this.ResumeLayout(false);

        }

        #endregion

        private Neusoft.FrameWork.WinForms.Controls.NeuButton btnOK;
        private ucArrangementSpread ucArrangementSpread1;
    }
}