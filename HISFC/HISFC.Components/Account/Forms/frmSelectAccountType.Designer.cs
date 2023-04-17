namespace FS.HISFC.Components.Account.Forms
{
    partial class frmSelectAccountType
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
            this.button1 = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbAccountType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(267, 54);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 34);
            this.button1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.button1.TabIndex = 2;
            this.button1.Text = "确   定";
            this.button1.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 14);
            this.label1.TabIndex = 3;
            this.label1.Text = "请选择账户类型：";
            // 
            // cmbAccountType
            // 
            this.cmbAccountType.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbAccountType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbAccountType.FormattingEnabled = true;
            this.cmbAccountType.IsEnter2Tab = false;
            this.cmbAccountType.IsFlat = false;
            this.cmbAccountType.IsLike = true;
            this.cmbAccountType.IsListOnly = false;
            this.cmbAccountType.IsPopForm = true;
            this.cmbAccountType.IsShowCustomerList = false;
            this.cmbAccountType.IsShowID = false;
            this.cmbAccountType.IsShowIDAndName = false;
            this.cmbAccountType.Location = new System.Drawing.Point(137, 20);
            this.cmbAccountType.Name = "cmbAccountType";
            this.cmbAccountType.ShowCustomerList = false;
            this.cmbAccountType.ShowID = false;
            this.cmbAccountType.Size = new System.Drawing.Size(151, 20);
            this.cmbAccountType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbAccountType.TabIndex = 31;
            this.cmbAccountType.Tag = "";
            this.cmbAccountType.ToolBarUse = false;
            // 
            // frmSelectAccountType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 100);
            this.Controls.Add(this.cmbAccountType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "frmSelectAccountType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新建账户";
            this.Load += new System.EventHandler(this.frmSelectAccountType_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuButton button1;
        private System.Windows.Forms.Label label1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbAccountType;


    }
}