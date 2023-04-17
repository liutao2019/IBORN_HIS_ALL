namespace FS.SOC.Local.DrugStore.ZhuHai.ZDWY.InPatient
{
    partial class frmWorkLoad
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
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.nlbBillClassName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbDeptName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuGroupBox2 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.ntbSendEmplBarCode = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ntbCheckEmplBarCode = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuGroupBox1.SuspendLayout();
            this.neuGroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.nlbBillClassName);
            this.neuGroupBox1.Controls.Add(this.nlbDeptName);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(304, 63);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 0;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "摆药单信息";
            // 
            // nlbBillClassName
            // 
            this.nlbBillClassName.AutoSize = true;
            this.nlbBillClassName.Location = new System.Drawing.Point(167, 29);
            this.nlbBillClassName.Name = "nlbBillClassName";
            this.nlbBillClassName.Size = new System.Drawing.Size(53, 12);
            this.nlbBillClassName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbBillClassName.TabIndex = 1;
            this.nlbBillClassName.Text = "摆药单：";
            // 
            // nlbDeptName
            // 
            this.nlbDeptName.AutoSize = true;
            this.nlbDeptName.Location = new System.Drawing.Point(21, 30);
            this.nlbDeptName.Name = "nlbDeptName";
            this.nlbDeptName.Size = new System.Drawing.Size(41, 12);
            this.nlbDeptName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbDeptName.TabIndex = 0;
            this.nlbDeptName.Text = "科室：";
            // 
            // neuGroupBox2
            // 
            this.neuGroupBox2.Controls.Add(this.neuLabel3);
            this.neuGroupBox2.Controls.Add(this.neuLabel2);
            this.neuGroupBox2.Controls.Add(this.ntbCheckEmplBarCode);
            this.neuGroupBox2.Controls.Add(this.ntbSendEmplBarCode);
            this.neuGroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuGroupBox2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuGroupBox2.Location = new System.Drawing.Point(0, 93);
            this.neuGroupBox2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.neuGroupBox2.Name = "neuGroupBox2";
            this.neuGroupBox2.Size = new System.Drawing.Size(304, 90);
            this.neuGroupBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox2.TabIndex = 1;
            this.neuGroupBox2.TabStop = false;
            // 
            // ntbSendEmplBarCode
            // 
            this.ntbSendEmplBarCode.IsEnter2Tab = false;
            this.ntbSendEmplBarCode.Location = new System.Drawing.Point(78, 23);
            this.ntbSendEmplBarCode.Name = "ntbSendEmplBarCode";
            this.ntbSendEmplBarCode.Size = new System.Drawing.Size(204, 26);
            this.ntbSendEmplBarCode.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ntbSendEmplBarCode.TabIndex = 0;
            // 
            // neuLabel1
            // 
            this.neuLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuLabel1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel1.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel1.Location = new System.Drawing.Point(0, 63);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(304, 30);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 2;
            this.neuLabel1.Text = "请扫描工号条形码";
            this.neuLabel1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // ntbCheckEmplBarCode
            // 
            this.ntbCheckEmplBarCode.IsEnter2Tab = false;
            this.ntbCheckEmplBarCode.Location = new System.Drawing.Point(77, 56);
            this.ntbCheckEmplBarCode.Name = "ntbCheckEmplBarCode";
            this.ntbCheckEmplBarCode.Size = new System.Drawing.Size(204, 26);
            this.ntbCheckEmplBarCode.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ntbCheckEmplBarCode.TabIndex = 1;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel2.Location = new System.Drawing.Point(9, 28);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(67, 14);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 2;
            this.neuLabel2.Text = "发药人：";
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel3.Location = new System.Drawing.Point(9, 61);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(67, 14);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 3;
            this.neuLabel3.Text = "核对人：";
            // 
            // frmWorkLoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 183);
            this.Controls.Add(this.neuLabel1);
            this.Controls.Add(this.neuGroupBox2);
            this.Controls.Add(this.neuGroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmWorkLoad";
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.neuGroupBox2.ResumeLayout(false);
            this.neuGroupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox2;
        private FS.FrameWork.WinForms.Controls.NeuTextBox ntbSendEmplBarCode;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbBillClassName;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbDeptName;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuTextBox ntbCheckEmplBarCode;
    }
}