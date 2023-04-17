namespace API.GZSI.UI
{
    partial class frmDiagnoseInput
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
            this.cmbDiagnose = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmbDiagnose
            // 
            this.cmbDiagnose.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbDiagnose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbDiagnose.FormattingEnabled = true;
            this.cmbDiagnose.IsEnter2Tab = false;
            this.cmbDiagnose.IsFlat = false;
            this.cmbDiagnose.IsLike = true;
            this.cmbDiagnose.IsListOnly = false;
            this.cmbDiagnose.IsPopForm = true;
            this.cmbDiagnose.IsShowCustomerList = false;
            this.cmbDiagnose.IsShowID = false;
            this.cmbDiagnose.IsShowIDAndName = false;
            this.cmbDiagnose.Location = new System.Drawing.Point(87, 45);
            this.cmbDiagnose.Name = "cmbDiagnose";
            this.cmbDiagnose.ShowCustomerList = false;
            this.cmbDiagnose.ShowID = false;
            this.cmbDiagnose.Size = new System.Drawing.Size(248, 20);
            this.cmbDiagnose.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbDiagnose.TabIndex = 3;
            this.cmbDiagnose.Tag = "";
            this.cmbDiagnose.ToolBarUse = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(22, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "入院诊断:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(250, 106);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(160, 106);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmDiagnoseInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 170);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cmbDiagnose);
            this.Controls.Add(this.label2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDiagnoseInput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "住院诊断录入";
            this.Load += new System.EventHandler(this.frmInpatientInput_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDiagnose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}