namespace FS.SOC.HISFC.Components.DCP.Controls
{
    partial class ucReportButtom
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
            this.components = new System.ComponentModel.Container();
            this.gbReportButtom = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.txtCase = new System.Windows.Forms.TextBox();
            this.dtpReportTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.cmbReportDoctor = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label35 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.rtxtMemo = new System.Windows.Forms.RichTextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.cmbDoctorDept = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label39 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.gbReportButtom.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbReportButtom
            // 
            this.gbReportButtom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbReportButtom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.gbReportButtom.Controls.Add(this.txtCase);
            this.gbReportButtom.Controls.Add(this.dtpReportTime);
            this.gbReportButtom.Controls.Add(this.cmbReportDoctor);
            this.gbReportButtom.Controls.Add(this.label35);
            this.gbReportButtom.Controls.Add(this.label9);
            this.gbReportButtom.Controls.Add(this.rtxtMemo);
            this.gbReportButtom.Controls.Add(this.label37);
            this.gbReportButtom.Controls.Add(this.cmbDoctorDept);
            this.gbReportButtom.Controls.Add(this.label39);
            this.gbReportButtom.Controls.Add(this.label41);
            this.gbReportButtom.Font = new System.Drawing.Font("宋体", 1F);
            this.gbReportButtom.Location = new System.Drawing.Point(3, 3);
            this.gbReportButtom.Name = "gbReportButtom";
            this.gbReportButtom.Size = new System.Drawing.Size(842, 96);
            this.gbReportButtom.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbReportButtom.TabIndex = 0;
            this.gbReportButtom.TabStop = false;
            // 
            // txtCase
            // 
            this.txtCase.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCase.Location = new System.Drawing.Point(234, 28);
            this.txtCase.Name = "txtCase";
            this.txtCase.Size = new System.Drawing.Size(587, 23);
            this.txtCase.TabIndex = 126;
            // 
            // dtpReportTime
            // 
            this.dtpReportTime.Enabled = false;
            this.dtpReportTime.Font = new System.Drawing.Font("宋体", 9F);
            this.dtpReportTime.IsEnter2Tab = false;
            this.dtpReportTime.Location = new System.Drawing.Point(621, 56);
            this.dtpReportTime.Name = "dtpReportTime";
            this.dtpReportTime.Size = new System.Drawing.Size(200, 21);
            this.dtpReportTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpReportTime.TabIndex = 125;
            // 
            // cmbReportDoctor
            // 
            this.cmbReportDoctor.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbReportDoctor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbReportDoctor.Enabled = false;
            this.cmbReportDoctor.Font = new System.Drawing.Font("宋体", 10.5F);
            this.cmbReportDoctor.IsEnter2Tab = false;
            this.cmbReportDoctor.IsFlat = false;
            this.cmbReportDoctor.IsLike = true;
            this.cmbReportDoctor.IsListOnly = false;
            this.cmbReportDoctor.IsPopForm = true;
            this.cmbReportDoctor.IsShowCustomerList = false;
            this.cmbReportDoctor.IsShowID = false;
            this.cmbReportDoctor.IsShowIDAndName = false;
            this.cmbReportDoctor.Location = new System.Drawing.Point(234, 57);
            this.cmbReportDoctor.Name = "cmbReportDoctor";
            this.cmbReportDoctor.ShowCustomerList = false;
            this.cmbReportDoctor.ShowID = false;
            this.cmbReportDoctor.Size = new System.Drawing.Size(112, 22);
            this.cmbReportDoctor.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbReportDoctor.TabIndex = 120;
            this.cmbReportDoctor.Tag = "";
            this.cmbReportDoctor.ToolBarUse = false;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("宋体", 9F);
            this.label35.Location = new System.Drawing.Point(167, 33);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(65, 12);
            this.label35.TabIndex = 124;
            this.label35.Text = "退卡原因：";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("宋体", 9F);
            this.label9.Location = new System.Drawing.Point(166, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 115;
            this.label9.Text = "备    注：";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rtxtMemo
            // 
            this.rtxtMemo.Font = new System.Drawing.Font("宋体", 10.5F);
            this.rtxtMemo.Location = new System.Drawing.Point(235, 4);
            this.rtxtMemo.Name = "rtxtMemo";
            this.rtxtMemo.Size = new System.Drawing.Size(586, 24);
            this.rtxtMemo.TabIndex = 123;
            this.rtxtMemo.TabStop = false;
            this.rtxtMemo.Text = "";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.BackColor = System.Drawing.Color.Transparent;
            this.label37.Font = new System.Drawing.Font("宋体", 9F);
            this.label37.Location = new System.Drawing.Point(547, 60);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(65, 12);
            this.label37.TabIndex = 119;
            this.label37.Text = "填卡时间：";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbDoctorDept
            // 
            this.cmbDoctorDept.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbDoctorDept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbDoctorDept.Enabled = false;
            this.cmbDoctorDept.Font = new System.Drawing.Font("宋体", 10.5F);
            this.cmbDoctorDept.IsEnter2Tab = false;
            this.cmbDoctorDept.IsFlat = false;
            this.cmbDoctorDept.IsLike = true;
            this.cmbDoctorDept.IsListOnly = false;
            this.cmbDoctorDept.IsPopForm = true;
            this.cmbDoctorDept.IsShowCustomerList = false;
            this.cmbDoctorDept.IsShowID = false;
            this.cmbDoctorDept.IsShowIDAndName = false;
            this.cmbDoctorDept.Location = new System.Drawing.Point(429, 57);
            this.cmbDoctorDept.Name = "cmbDoctorDept";
            this.cmbDoctorDept.ShowCustomerList = false;
            this.cmbDoctorDept.ShowID = false;
            this.cmbDoctorDept.Size = new System.Drawing.Size(112, 22);
            this.cmbDoctorDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbDoctorDept.TabIndex = 121;
            this.cmbDoctorDept.Tag = "";
            this.cmbDoctorDept.ToolBarUse = false;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.BackColor = System.Drawing.Color.Transparent;
            this.label39.Font = new System.Drawing.Font("宋体", 9F);
            this.label39.Location = new System.Drawing.Point(358, 60);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(65, 12);
            this.label39.TabIndex = 117;
            this.label39.Text = "报告科室：";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.BackColor = System.Drawing.Color.Transparent;
            this.label41.Font = new System.Drawing.Font("宋体", 9F);
            this.label41.Location = new System.Drawing.Point(166, 58);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(65, 12);
            this.label41.TabIndex = 116;
            this.label41.Text = "报告医生：";
            this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucReportButtom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(177)))), ((int)(((byte)(201)))));
            this.Controls.Add(this.gbReportButtom);
            this.Name = "ucReportButtom";
            this.Size = new System.Drawing.Size(848, 102);
            this.gbReportButtom.ResumeLayout(false);
            this.gbReportButtom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbReportButtom;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbReportDoctor;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RichTextBox rtxtMemo;
        private System.Windows.Forms.Label label37;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDoctorDept;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label41;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpReportTime;
        private System.Windows.Forms.TextBox txtCase;
    }
}
