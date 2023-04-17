namespace FS.HISFC.Components.Registration.Forms
{
    partial class frmInputSerialNo
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
            this.btnCancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnOK = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.panel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.label2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.groupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.tbSerialNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(181, 111);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "取消";
            this.btnCancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(46, 111);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnOK.TabIndex = 14;
            this.btnOK.Text = "确定";
            this.btnOK.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Window;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(321, 53);
            this.panel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panel2.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(59, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(203, 32);
            this.label2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label2.TabIndex = 1;
            this.label2.Text = "请输入预约号";
            // 
            // groupBox1
            // 
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(321, 2);
            this.groupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // tbSerialNo
            // 
            this.tbSerialNo.IsEnter2Tab = false;
            this.tbSerialNo.Location = new System.Drawing.Point(131, 64);
            this.tbSerialNo.Name = "tbSerialNo";
            this.tbSerialNo.Size = new System.Drawing.Size(109, 21);
            this.tbSerialNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbSerialNo.TabIndex = 8;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(72, 68);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(53, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 7;
            this.neuLabel1.Text = "预约号：";
            // 
            // frmInputSerialNo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 163);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.tbSerialNo);
            this.Controls.Add(this.neuLabel1);
            this.Name = "frmInputSerialNo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "取消预约";
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuButton btnCancel;
        private FS.FrameWork.WinForms.Controls.NeuButton btnOK;
        private FS.FrameWork.WinForms.Controls.NeuPanel panel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel label2;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox groupBox1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox tbSerialNo;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
    }
}