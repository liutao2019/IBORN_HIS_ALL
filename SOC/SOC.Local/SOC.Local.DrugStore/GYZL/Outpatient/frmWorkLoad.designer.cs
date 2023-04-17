namespace FS.SOC.Local.DrugStore.GYZL.Outpatient
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
            this.neuGroupBox2 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.ntbEmplNOBarCode = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbRecipeNO = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbPatientName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuGroupBox1.SuspendLayout();
            this.neuGroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.nlbPatientName);
            this.neuGroupBox1.Controls.Add(this.nlbRecipeNO);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(304, 63);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 0;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "处方信息";
            // 
            // neuGroupBox2
            // 
            this.neuGroupBox2.Controls.Add(this.ntbEmplNOBarCode);
            this.neuGroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuGroupBox2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuGroupBox2.Location = new System.Drawing.Point(0, 101);
            this.neuGroupBox2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.neuGroupBox2.Name = "neuGroupBox2";
            this.neuGroupBox2.Size = new System.Drawing.Size(304, 60);
            this.neuGroupBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox2.TabIndex = 1;
            this.neuGroupBox2.TabStop = false;
            // 
            // ntbEmplNOBarCode
            // 
            this.ntbEmplNOBarCode.IsEnter2Tab = false;
            this.ntbEmplNOBarCode.Location = new System.Drawing.Point(22, 17);
            this.ntbEmplNOBarCode.Name = "ntbEmplNOBarCode";
            this.ntbEmplNOBarCode.Size = new System.Drawing.Size(260, 26);
            this.ntbEmplNOBarCode.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ntbEmplNOBarCode.TabIndex = 0;
            // 
            // neuLabel1
            // 
            this.neuLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuLabel1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel1.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel1.Location = new System.Drawing.Point(0, 63);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(304, 38);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 2;
            this.neuLabel1.Text = "请扫描工号条形码";
            this.neuLabel1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // nlbRecipeNO
            // 
            this.nlbRecipeNO.AutoSize = true;
            this.nlbRecipeNO.Location = new System.Drawing.Point(21, 30);
            this.nlbRecipeNO.Name = "nlbRecipeNO";
            this.nlbRecipeNO.Size = new System.Drawing.Size(53, 12);
            this.nlbRecipeNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbRecipeNO.TabIndex = 0;
            this.nlbRecipeNO.Text = "处方号：";
            // 
            // nlbPatientName
            // 
            this.nlbPatientName.AutoSize = true;
            this.nlbPatientName.Location = new System.Drawing.Point(167, 29);
            this.nlbPatientName.Name = "nlbPatientName";
            this.nlbPatientName.Size = new System.Drawing.Size(65, 12);
            this.nlbPatientName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbPatientName.TabIndex = 1;
            this.nlbPatientName.Text = "患者姓名：";
            // 
            // frmWorkLoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 161);
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
        private FS.FrameWork.WinForms.Controls.NeuTextBox ntbEmplNOBarCode;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbPatientName;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbRecipeNO;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
    }
}