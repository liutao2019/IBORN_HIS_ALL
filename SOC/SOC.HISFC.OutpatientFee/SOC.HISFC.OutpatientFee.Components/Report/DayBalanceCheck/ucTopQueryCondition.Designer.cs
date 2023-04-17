namespace FS.SOC.HISFC.OutpatientFee.Components.Report.DayBalanceCheck
{
    partial class ucTopQueryCondition
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
            this.dtEndTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dtBeginTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.SuspendLayout();
            // 
            // dtEndTime
            // 
            this.dtEndTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtEndTime.Enabled = false;
            this.dtEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEndTime.IsEnter2Tab = false;
            this.dtEndTime.Location = new System.Drawing.Point(330, 4);
            this.dtEndTime.Name = "dtEndTime";
            this.dtEndTime.Size = new System.Drawing.Size(152, 21);
            this.dtEndTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtEndTime.TabIndex = 7;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(247, 8);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(77, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 6;
            this.neuLabel2.Text = "本次日结时间";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(6, 8);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(77, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 4;
            this.neuLabel1.Text = "上次日结时间";
            // 
            // dtBeginTime
            // 
            this.dtBeginTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtBeginTime.Enabled = false;
            this.dtBeginTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtBeginTime.IsEnter2Tab = false;
            this.dtBeginTime.Location = new System.Drawing.Point(89, 4);
            this.dtBeginTime.Name = "dtBeginTime";
            this.dtBeginTime.Size = new System.Drawing.Size(152, 21);
            this.dtBeginTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtBeginTime.TabIndex = 8;
            // 
            // ucTopQueryCondition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dtBeginTime);
            this.Controls.Add(this.dtEndTime);
            this.Controls.Add(this.neuLabel2);
            this.Controls.Add(this.neuLabel1);
            this.Name = "ucTopQueryCondition";
            this.Size = new System.Drawing.Size(507, 31);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtEndTime;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        public FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtBeginTime;
    }
}
