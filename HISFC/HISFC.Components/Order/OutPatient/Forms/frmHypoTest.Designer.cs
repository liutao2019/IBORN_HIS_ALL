﻿namespace FS.HISFC.Components.Order.OutPatient.Forms
{
    partial class frmHypoTest
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.btnOK = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.lblDrugName = new System.Windows.Forms.Label();
            this.rb4 = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.rb3 = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.rb2 = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.rb1 = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.neuPanel2.SuspendLayout();
            this.neuPanel1.SuspendLayout();
            this.neuGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanel2
            // 
            this.neuPanel2.BackColor = System.Drawing.Color.AliceBlue;
            this.neuPanel2.Controls.Add(this.btnOK);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuPanel2.Location = new System.Drawing.Point(0, 102);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(196, 36);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(108, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.neuGroupBox1);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(196, 102);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 2;
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.BackColor = System.Drawing.Color.AliceBlue;
            this.neuGroupBox1.Controls.Add(this.lblDrugName);
            this.neuGroupBox1.Controls.Add(this.rb4);
            this.neuGroupBox1.Controls.Add(this.rb3);
            this.neuGroupBox1.Controls.Add(this.rb2);
            this.neuGroupBox1.Controls.Add(this.rb1);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(196, 102);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 0;
            this.neuGroupBox1.TabStop = false;
            // 
            // lblDrugName
            // 
            this.lblDrugName.AutoSize = true;
            this.lblDrugName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDrugName.ForeColor = System.Drawing.Color.Red;
            this.lblDrugName.Location = new System.Drawing.Point(6, 17);
            this.lblDrugName.Name = "lblDrugName";
            this.lblDrugName.Size = new System.Drawing.Size(57, 12);
            this.lblDrugName.TabIndex = 4;
            this.lblDrugName.Text = "药品名称";
            // 
            // rb4
            // 
            this.rb4.AutoSize = true;
            this.rb4.Location = new System.Drawing.Point(117, 67);
            this.rb4.Name = "rb4";
            this.rb4.Size = new System.Drawing.Size(71, 16);
            this.rb4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rb4.TabIndex = 3;
            this.rb4.TabStop = true;
            this.rb4.Text = "皮试阳性";
            this.rb4.UseVisualStyleBackColor = true;
            // 
            // rb3
            // 
            this.rb3.AutoSize = true;
            this.rb3.Location = new System.Drawing.Point(12, 45);
            this.rb3.Name = "rb3";
            this.rb3.Size = new System.Drawing.Size(59, 16);
            this.rb3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rb3.TabIndex = 2;
            this.rb3.TabStop = true;
            this.rb3.Text = "需皮试";
            this.rb3.UseVisualStyleBackColor = true;
            // 
            // rb2
            // 
            this.rb2.AutoSize = true;
            this.rb2.Location = new System.Drawing.Point(12, 67);
            this.rb2.Name = "rb2";
            this.rb2.Size = new System.Drawing.Size(71, 16);
            this.rb2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rb2.TabIndex = 1;
            this.rb2.TabStop = true;
            this.rb2.Text = "皮试阴性";
            this.rb2.UseVisualStyleBackColor = true;
            // 
            // rb1
            // 
            this.rb1.AutoSize = true;
            this.rb1.Location = new System.Drawing.Point(117, 45);
            this.rb1.Name = "rb1";
            this.rb1.Size = new System.Drawing.Size(71, 16);
            this.rb1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rb1.TabIndex = 0;
            this.rb1.TabStop = true;
            this.rb1.Text = "免试";
            this.rb1.UseVisualStyleBackColor = true;
            // 
            // frmHypoTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(196, 138);
            this.ControlBox = false;
            this.Controls.Add(this.neuPanel1);
            this.Controls.Add(this.neuPanel2);
            this.KeyPreview = true;
            this.Name = "frmHypoTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "皮试";
            this.neuPanel2.ResumeLayout(false);
            this.neuPanel1.ResumeLayout(false);
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuButton btnOK;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        //{55BBD9DB-F5C9-4e0a-94E5-9F7FCB121350}
        public FS.FrameWork.WinForms.Controls.NeuRadioButton rb2;
        public FS.FrameWork.WinForms.Controls.NeuRadioButton rb1;
        public FS.FrameWork.WinForms.Controls.NeuRadioButton rb3;
        public FS.FrameWork.WinForms.Controls.NeuRadioButton rb4;
        private System.Windows.Forms.Label lblDrugName;
    }
}