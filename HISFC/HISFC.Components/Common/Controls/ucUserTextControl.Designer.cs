﻿namespace FS.HISFC.Components.Common.Controls
{
    partial class ucUserTextControl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// {B0D93A7B-BAA4-467f-BC87-3100AC53777F}
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.radioButton1 = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.radioButton2 = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.textBox1 = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtMemo = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.richTextBox1 = new FS.FrameWork.EPRControl.emrMultiLineTextBox();
            this.btnSave = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnExit = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.cmb = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.SuspendLayout();
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(28, 13);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(47, 16);
            this.radioButton1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "科室";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(94, 13);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(47, 16);
            this.radioButton2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "人员";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(16, 41);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(41, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 2;
            this.neuLabel1.Text = "名称：";
            // 
            // textBox1
            // 
            this.textBox1.IsEnter2Tab = false;
            this.textBox1.Location = new System.Drawing.Point(63, 38);
            this.textBox1.MaxLength = 25;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(179, 21);
            this.textBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.textBox1.TabIndex = 3;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(16, 74);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(41, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 4;
            this.neuLabel2.Text = "组别：";
            // 
            // txtMemo
            // 
            this.txtMemo.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.txtMemo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtMemo.FormattingEnabled = true;
            this.txtMemo.IsEnter2Tab = false;
            this.txtMemo.IsFlat = false;
            this.txtMemo.IsLike = true;
            this.txtMemo.IsListOnly = false;
            this.txtMemo.IsPopForm = true;
            this.txtMemo.IsShowCustomerList = false;
            this.txtMemo.IsShowID = false;
            this.txtMemo.IsShowIDAndName = false;
            this.txtMemo.Location = new System.Drawing.Point(63, 71);
            this.txtMemo.MaxLength = 14;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.ShowCustomerList = false;
            this.txtMemo.ShowID = false;
            this.txtMemo.Size = new System.Drawing.Size(179, 20);
            this.txtMemo.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.txtMemo.TabIndex = 5;
            this.txtMemo.Tag = "";
            this.txtMemo.ToolBarUse = false;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Enabled = false;
            this.richTextBox1.HideSelection = false;
            this.richTextBox1.IsDrawLine = false;
            this.richTextBox1.IsExactSearch = false;
            this.richTextBox1.IsShowModify = false;
            this.richTextBox1.KeyWordIndex = 0;
            this.richTextBox1.Location = new System.Drawing.Point(18, 99);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.SearchTable = null;
            this.richTextBox1.SelectText = "";
            this.richTextBox1.Size = new System.Drawing.Size(318, 225);
            this.richTextBox1.TabIndex = 7;
            this.richTextBox1.Text = "";
            this.richTextBox1.名称 = null;
            this.richTextBox1.是否组 = false;
            this.richTextBox1.组 = "无";
            // 
            // btnSave
            // 
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(261, 36);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExit.Location = new System.Drawing.Point(261, 70);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnExit.TabIndex = 9;
            this.btnExit.Text = "退出(&X)";
            this.btnExit.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // cmb
            // 
            this.cmb.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmb.FormattingEnabled = true;
            this.cmb.IsEnter2Tab = false;
            this.cmb.IsFlat = false;
            this.cmb.IsLike = true;
            this.cmb.IsListOnly = false;
            this.cmb.IsPopForm = true;
            this.cmb.IsShowCustomerList = false;
            this.cmb.IsShowID = false;
            this.cmb.IsShowIDAndName = false;
            this.cmb.Items.AddRange(new object[] {
            "符号信息",
            "文本信息",
            "相关信息"});
            this.cmb.Location = new System.Drawing.Point(135, 3);
            this.cmb.Name = "cmb";
            this.cmb.ShowCustomerList = false;
            this.cmb.ShowID = false;
            this.cmb.Size = new System.Drawing.Size(179, 20);
            this.cmb.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmb.TabIndex = 6;
            this.cmb.Tag = "";
            this.cmb.ToolBarUse = false;
            this.cmb.Visible = false;
            // 
            // ucUserTextControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.cmb);
            this.Controls.Add(this.txtMemo);
            this.Controls.Add(this.neuLabel2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.neuLabel1);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Name = "ucUserTextControl";
            this.Size = new System.Drawing.Size(348, 336);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuRadioButton radioButton1;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton radioButton2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox textBox1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuComboBox txtMemo;
        private FS.FrameWork.WinForms.Controls.NeuButton btnSave;
        private FS.FrameWork.WinForms.Controls.NeuButton btnExit;
        public FS.FrameWork.EPRControl.emrMultiLineTextBox richTextBox1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmb;
    }
}
