namespace FS.HISFC.Components.Order.Forms
{
    partial class frmDCTreatmentType
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
            this.cbxHealthCareType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.btnCancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnOK = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.SuspendLayout();
            // 
            // cbxHealthCareType
            // 
            this.cbxHealthCareType.ArrowBackColor = System.Drawing.Color.Silver;
            this.cbxHealthCareType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cbxHealthCareType.FormattingEnabled = true;
            this.cbxHealthCareType.IsEnter2Tab = false;
            this.cbxHealthCareType.IsFlat = false;
            this.cbxHealthCareType.IsLike = true;
            this.cbxHealthCareType.IsListOnly = false;
            this.cbxHealthCareType.IsPopForm = true;
            this.cbxHealthCareType.IsShowCustomerList = false;
            this.cbxHealthCareType.IsShowID = false;
            this.cbxHealthCareType.IsShowIDAndName = false;
            this.cbxHealthCareType.Location = new System.Drawing.Point(98, 26);
            this.cbxHealthCareType.Name = "cbxHealthCareType";
            this.cbxHealthCareType.ShowCustomerList = false;
            this.cbxHealthCareType.ShowID = false;
            this.cbxHealthCareType.Size = new System.Drawing.Size(160, 20);
            this.cbxHealthCareType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cbxHealthCareType.TabIndex = 13;
            this.cbxHealthCareType.Tag = "";
            this.cbxHealthCareType.ToolBarUse = false;
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Location = new System.Drawing.Point(27, 30);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(65, 12);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 12;
            this.neuLabel5.Text = "待遇类型：";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(183, 84);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(42, 84);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "确定(&O)";
            this.btnOK.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmDCTreatmentType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 143);
            this.Controls.Add(this.cbxHealthCareType);
            this.Controls.Add(this.neuLabel5);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Name = "frmDCTreatmentType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改待遇类型";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuComboBox cbxHealthCareType;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.NeuButton btnCancel;
        private FS.FrameWork.WinForms.Controls.NeuButton btnOK;
    }
}