﻿namespace FS.HISFC.Components.Registration
{
    partial class frmFindEmployee
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.comboBox1 = new FS.FrameWork.WinForms.Controls.NeuComboBox();
            this.label1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.panel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.groupBox2 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.button2 = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.button1 = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.panel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.label2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.groupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(283, 178);
            this.panel1.TabIndex = 1;
            // 
            // comboBox1
            // 
            this.comboBox1.ArrowBackColor = System.Drawing.Color.Silver;
            this.comboBox1.Font = new System.Drawing.Font("宋体", 12F);
            this.comboBox1.IsFlat = false;
            this.comboBox1.IsLike = true;
            this.comboBox1.Location = new System.Drawing.Point(108, 76);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.PopForm = null;
            this.comboBox1.ShowCustomerList = false;
            this.comboBox1.ShowID = false;
            this.comboBox1.Size = new System.Drawing.Size(121, 24);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.Tag = "";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(46, 80);
            this.label1.Name = "label1";
            this.label1.TabIndex = 3;
            this.label1.Text = "查找员工：";
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
            this.panel3.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(283, 3);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.No;
            this.button2.Location = new System.Drawing.Point(187, 14);
            this.button2.Name = "button2";
            this.button2.TabIndex = 1;
            this.button2.Text = "取消(&X)";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.button1.Location = new System.Drawing.Point(93, 14);
            this.button1.Name = "button1";
            this.button1.TabIndex = 0;
            this.button1.Text = "查找(&O)";
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
            this.panel2.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(14, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(260, 32);
            this.label2.TabIndex = 1;
            this.label2.Text = "在下拉列表框中选择要查找的人员，然后单击查找按钮";
            // 
            // groupBox1
            // 
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(283, 2);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // frmFindEmployee
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(283, 178);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFindEmployee";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "医生查找...";
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel panel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel panel2;
        private FS.FrameWork.WinForms.Controls.NeuPanel panel3;
        private FS.FrameWork.WinForms.Controls.NeuButton button1;
        private FS.FrameWork.WinForms.Controls.NeuButton button2;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox groupBox1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox comboBox1;
        private FS.FrameWork.WinForms.Controls.NeuLabel label1;
        private FS.FrameWork.WinForms.Controls.NeuLabel label2;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox groupBox2;
    }
}