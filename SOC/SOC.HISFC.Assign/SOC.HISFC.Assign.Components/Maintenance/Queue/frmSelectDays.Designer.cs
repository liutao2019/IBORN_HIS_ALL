namespace FS.SOC.HISFC.Assign.Components.Maintenance.Queue
{
    partial class frmSelectDays
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
            this.panel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.label2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.groupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.panel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.label1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.panel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.groupBox2 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.button2 = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.button1 = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.numDays = new FS.FrameWork.WinForms.Controls.NeuNumericUpDown();
            this.dtpEndDate = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDays)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Window;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(283, 53);
            this.panel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panel2.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(14, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(260, 21);
            this.label2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label2.TabIndex = 1;
            this.label2.Text = "根据模板选择生成的结束日期或生成的天数";
            // 
            // groupBox1
            // 
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(283, 2);
            this.groupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dtpEndDate);
            this.panel1.Controls.Add(this.numDays);
            this.panel1.Controls.Add(this.neuLabel1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(283, 178);
            this.panel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(14, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 23);
            this.label1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label1.TabIndex = 3;
            this.label1.Text = "选择：";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Controls.Add(this.button2);
            this.panel3.Controls.Add(this.button1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 128);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(283, 50);
            this.panel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panel3.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(283, 3);
            this.groupBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.No;
            this.button2.Location = new System.Drawing.Point(187, 14);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.button2.TabIndex = 1;
            this.button2.Text = "取消(&X)";
            this.button2.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.button1.Location = new System.Drawing.Point(93, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.button1.TabIndex = 0;
            this.button1.Text = "确定(&O)";
            this.button1.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            // 
            // numDays
            // 
            this.numDays.Location = new System.Drawing.Point(224, 77);
            this.numDays.Name = "numDays";
            this.numDays.Size = new System.Drawing.Size(50, 21);
            this.numDays.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.numDays.TabIndex = 4;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.IsEnter2Tab = false;
            this.dtpEndDate.Location = new System.Drawing.Point(53, 77);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(115, 21);
            this.dtpEndDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpEndDate.TabIndex = 5;
            // 
            // neuLabel1
            // 
            this.neuLabel1.Location = new System.Drawing.Point(185, 80);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(49, 23);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 6;
            this.neuLabel1.Text = "天数：";
            // 
            // frmSelectDays
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 178);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSelectDays";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择生成的天数";
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numDays)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel panel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel label2;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox groupBox1;
        private FS.FrameWork.WinForms.Controls.NeuPanel panel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel label1;
        private FS.FrameWork.WinForms.Controls.NeuPanel panel3;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox groupBox2;
        private FS.FrameWork.WinForms.Controls.NeuButton button2;
        private FS.FrameWork.WinForms.Controls.NeuButton button1;
        private FS.FrameWork.WinForms.Controls.NeuNumericUpDown numDays;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpEndDate;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
    }
}