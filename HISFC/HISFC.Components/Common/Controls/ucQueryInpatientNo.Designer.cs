﻿namespace FS.HISFC.Components.Common.Controls
{
    partial class ucQueryInpatientNo
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
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtInputCode = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.label1.TabIndex = 0;
            this.label1.Text = "住院号：";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtInputCode
            // 
            this.txtInputCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInputCode.IsEnter2Tab = false;
            this.txtInputCode.Location = new System.Drawing.Point(75, 4);
            this.txtInputCode.Name = "txtInputCode";
            this.txtInputCode.Size = new System.Drawing.Size(123, 21);
            this.txtInputCode.Style = FS.FrameWork.WinForms.Controls.StyleType.VS2003;
            this.txtInputCode.TabIndex = 1;
            this.txtInputCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInputCode_KeyDown);
            // 
            // ucQueryInpatientNo
            // 
            this.Controls.Add(this.txtInputCode);
            this.Controls.Add(this.label1);
            this.Name = "ucQueryInpatientNo";
            this.Size = new System.Drawing.Size(198, 27);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public FS.FrameWork.WinForms.Controls.NeuLabel label1;
        public FS.FrameWork.WinForms.Controls.NeuTextBox txtInputCode;

    }
}
