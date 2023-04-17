namespace FS.SOC.HISFC.RADT.Components.Base.Inpatient
{
    partial class frmSiRegisterInfo
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
            this.lblNewPact = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbNewPact = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.btnCancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnOK = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.SuspendLayout();
            // 
            // lblNewPact
            // 
            this.lblNewPact.AutoSize = true;
            this.lblNewPact.Font = new System.Drawing.Font("宋体", 10F);
            this.lblNewPact.Location = new System.Drawing.Point(21, 19);
            this.lblNewPact.Name = "lblNewPact";
            this.lblNewPact.Size = new System.Drawing.Size(112, 14);
            this.lblNewPact.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblNewPact.TabIndex = 10;
            this.lblNewPact.Text = "请选择合同单位:";
            // 
            // cmbNewPact
            // 
            this.cmbNewPact.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbNewPact.Font = new System.Drawing.Font("宋体", 10F);
            this.cmbNewPact.FormattingEnabled = true;
            this.cmbNewPact.IsEnter2Tab = false;
            this.cmbNewPact.IsFlat = false;
            this.cmbNewPact.IsLike = true;
            this.cmbNewPact.IsListOnly = false;
            this.cmbNewPact.IsPopForm = true;
            this.cmbNewPact.IsShowCustomerList = false;
            this.cmbNewPact.IsShowID = false;
            this.cmbNewPact.Location = new System.Drawing.Point(139, 16);
            this.cmbNewPact.Name = "cmbNewPact";
            this.cmbNewPact.PopForm = null;
            this.cmbNewPact.ShowCustomerList = false;
            this.cmbNewPact.ShowID = false;
            this.cmbNewPact.Size = new System.Drawing.Size(121, 21);
            this.cmbNewPact.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbNewPact.TabIndex = 9;
            this.cmbNewPact.Tag = "";
            this.cmbNewPact.ToolBarUse = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("宋体", 11F);
            this.btnCancel.Location = new System.Drawing.Point(173, 55);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 32);
            this.btnCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.Cancel;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("宋体", 11F);
            this.btnOK.Location = new System.Drawing.Point(70, 55);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(87, 32);
            this.btnOK.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "确定(&O)";
            this.btnOK.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.OK;
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // frmSiRegisterInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 99);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblNewPact);
            this.Controls.Add(this.cmbNewPact);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSiRegisterInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "合同单位选择";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuLabel lblNewPact;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbNewPact;
        private FS.FrameWork.WinForms.Controls.NeuButton btnCancel;
        private FS.FrameWork.WinForms.Controls.NeuButton btnOK;
    }
}