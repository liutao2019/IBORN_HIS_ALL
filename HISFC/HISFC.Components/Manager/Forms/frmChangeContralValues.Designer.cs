namespace FS.HISFC.Components.Manager.Forms
{
    partial class frmChangeContralValues
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
            this.chk1 = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.chk2 = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.chk3 = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.btn_ok = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btn_cancle = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.btn_cancle);
            this.neuGroupBox1.Controls.Add(this.btn_ok);
            this.neuGroupBox1.Controls.Add(this.chk3);
            this.neuGroupBox1.Controls.Add(this.chk2);
            this.neuGroupBox1.Controls.Add(this.chk1);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(430, 332);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 0;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "基本信息维护控制参数";
            // 
            // chk1
            // 
            this.chk1.AutoSize = true;
            this.chk1.ForeColor = System.Drawing.Color.Blue;
            this.chk1.Location = new System.Drawing.Point(40, 44);
            this.chk1.Name = "chk1";
            this.chk1.Size = new System.Drawing.Size(192, 16);
            this.chk1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chk1.TabIndex = 0;
            this.chk1.Text = "项目是否需要维护开立科室限制";
            this.chk1.UseVisualStyleBackColor = true;
            // 
            // chk2
            // 
            this.chk2.AutoSize = true;
            this.chk2.ForeColor = System.Drawing.Color.Blue;
            this.chk2.Location = new System.Drawing.Point(40, 84);
            this.chk2.Name = "chk2";
            this.chk2.Size = new System.Drawing.Size(240, 16);
            this.chk2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chk2.TabIndex = 1;
            this.chk2.Text = "是否维护打印医嘱单属性限制医嘱单打印";
            this.chk2.UseVisualStyleBackColor = true;
            // 
            // chk3
            // 
            this.chk3.AutoSize = true;
            this.chk3.ForeColor = System.Drawing.Color.Blue;
            this.chk3.Location = new System.Drawing.Point(40, 125);
            this.chk3.Name = "chk3";
            this.chk3.Size = new System.Drawing.Size(192, 16);
            this.chk3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chk3.TabIndex = 2;
            this.chk3.Text = "是否显示物价费用类别并且过滤";
            this.chk3.UseVisualStyleBackColor = true;
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(108, 203);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(75, 23);
            this.btn_ok.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btn_ok.TabIndex = 3;
            this.btn_ok.Text = "保存";
            this.btn_ok.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // btn_cancle
            // 
            this.btn_cancle.Location = new System.Drawing.Point(223, 203);
            this.btn_cancle.Name = "btn_cancle";
            this.btn_cancle.Size = new System.Drawing.Size(75, 23);
            this.btn_cancle.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btn_cancle.TabIndex = 4;
            this.btn_cancle.Text = "返回";
            this.btn_cancle.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btn_cancle.UseVisualStyleBackColor = true;
            this.btn_cancle.Click += new System.EventHandler(this.btn_cancle_Click);
            // 
            // frmChangeContralValues
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 332);
            this.Controls.Add(this.neuGroupBox1);
            this.MaximizeBox = false;
            this.Name = "frmChangeContralValues";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "参数设置";
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuButton btn_cancle;
        private FS.FrameWork.WinForms.Controls.NeuButton btn_ok;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox chk3;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox chk2;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox chk1;
    }
}