namespace FS.HISFC.Components.HealthRecord.CaseHistory
{
    partial class ucCallDate
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
            this.neuDateTimePicker1 = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuBtnOK = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.SuspendLayout();
            // 
            // neuDateTimePicker1
            // 
            this.neuDateTimePicker1.CustomFormat = "yyyy-MM-dd 00:00:00";
            this.neuDateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.neuDateTimePicker1.IsEnter2Tab = false;
            this.neuDateTimePicker1.Location = new System.Drawing.Point(50, 18);
            this.neuDateTimePicker1.Name = "neuDateTimePicker1";
            this.neuDateTimePicker1.Size = new System.Drawing.Size(90, 21);
            this.neuDateTimePicker1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuDateTimePicker1.TabIndex = 0;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(3, 22);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(41, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 1;
            this.neuLabel1.Text = "时间：";
            // 
            // neuBtnOK
            // 
            this.neuBtnOK.Location = new System.Drawing.Point(65, 117);
            this.neuBtnOK.Name = "neuBtnOK";
            this.neuBtnOK.Size = new System.Drawing.Size(75, 23);
            this.neuBtnOK.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuBtnOK.TabIndex = 1;
            this.neuBtnOK.Text = "确定";
            this.neuBtnOK.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuBtnOK.UseVisualStyleBackColor = true;
            this.neuBtnOK.Click += new System.EventHandler(this.neuBtnOK_Click);
            // 
            // ucCallDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuBtnOK);
            this.Controls.Add(this.neuLabel1);
            this.Controls.Add(this.neuDateTimePicker1);
            this.Name = "ucCallDate";
            this.Size = new System.Drawing.Size(147, 143);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuButton neuBtnOK;
        public FS.FrameWork.WinForms.Controls.NeuDateTimePicker neuDateTimePicker1;
    }
}
