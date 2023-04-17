namespace FS.SOC.HISFC.Components.DCP.Controls
{
    partial class ucDiseaseReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDiseaseReport));
            this.neuSplitter1 = new FS.FrameWork.WinForms.Controls.NeuSplitter();
            this.tcReport = new FS.FrameWork.WinForms.Controls.NeuTabControl();
            this.tpMainReport = new System.Windows.Forms.TabPage();
            this.ucReportButtom1 = new FS.SOC.HISFC.Components.DCP.Controls.ucReportButtom();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.ucDiseaseInfo1 = new FS.SOC.HISFC.Components.DCP.Controls.ucDiseaseInfo();
            this.ucPatientInfo1 = new FS.SOC.HISFC.Components.DCP.Controls.ucPatientInfo();
            this.ucReportTop1 = new FS.SOC.HISFC.Components.DCP.Controls.ucReportTop();
            this.ucDiseaseQuery1 = new FS.SOC.HISFC.Components.DCP.Controls.ucDiseaseQuery();
            this.tcReport.SuspendLayout();
            this.tpMainReport.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuSplitter1
            // 
            this.neuSplitter1.BackColor = System.Drawing.Color.Gainsboro;
            this.neuSplitter1.Location = new System.Drawing.Point(236, 0);
            this.neuSplitter1.Name = "neuSplitter1";
            this.neuSplitter1.Size = new System.Drawing.Size(5, 658);
            this.neuSplitter1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSplitter1.TabIndex = 1;
            this.neuSplitter1.TabStop = false;
            // 
            // tcReport
            // 
            this.tcReport.Controls.Add(this.tpMainReport);
            this.tcReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcReport.Location = new System.Drawing.Point(241, 0);
            this.tcReport.Name = "tcReport";
            this.tcReport.SelectedIndex = 0;
            this.tcReport.Size = new System.Drawing.Size(778, 658);
            this.tcReport.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tcReport.TabIndex = 3;
            // 
            // tpMainReport
            // 
            this.tpMainReport.AutoScroll = true;
            this.tpMainReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(177)))), ((int)(((byte)(201)))));
            this.tpMainReport.Controls.Add(this.ucReportButtom1);
            this.tpMainReport.Controls.Add(this.neuPanel1);
            this.tpMainReport.Controls.Add(this.ucDiseaseInfo1);
            this.tpMainReport.Controls.Add(this.ucPatientInfo1);
            this.tpMainReport.Controls.Add(this.ucReportTop1);
            this.tpMainReport.Location = new System.Drawing.Point(4, 22);
            this.tpMainReport.Name = "tpMainReport";
            this.tpMainReport.Padding = new System.Windows.Forms.Padding(3);
            this.tpMainReport.Size = new System.Drawing.Size(770, 632);
            this.tpMainReport.TabIndex = 0;
            this.tpMainReport.Text = "报告卡信息";
            this.tpMainReport.UseVisualStyleBackColor = true;
            // 
            // ucReportButtom1
            // 
            this.ucReportButtom1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(177)))), ((int)(((byte)(201)))));
            this.ucReportButtom1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucReportButtom1.IsFullConvertToHalf = true;
            this.ucReportButtom1.IsPrint = false;
            this.ucReportButtom1.Location = new System.Drawing.Point(3, 658);
            this.ucReportButtom1.Name = "ucReportButtom1";
            this.ucReportButtom1.Size = new System.Drawing.Size(747, 105);
            this.ucReportButtom1.TabIndex = 9;
            // 
            // neuPanel1
            // 
            this.neuPanel1.BackColor = System.Drawing.Color.Transparent;
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(3, 658);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(747, 0);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 8;
            // 
            // ucDiseaseInfo1
            // 
            this.ucDiseaseInfo1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(177)))), ((int)(((byte)(201)))));
            this.ucDiseaseInfo1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucDiseaseInfo1.InfectCode = "";
            this.ucDiseaseInfo1.IsFullConvertToHalf = true;
            this.ucDiseaseInfo1.IsPrint = false;
            this.ucDiseaseInfo1.Location = new System.Drawing.Point(3, 440);
            this.ucDiseaseInfo1.Name = "ucDiseaseInfo1";
            this.ucDiseaseInfo1.Size = new System.Drawing.Size(747, 218);
            this.ucDiseaseInfo1.TabIndex = 7;
            // 
            // ucPatientInfo1
            // 
            this.ucPatientInfo1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(177)))), ((int)(((byte)(201)))));
            this.ucPatientInfo1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucPatientInfo1.HomeArea = "本县区";
            this.ucPatientInfo1.IsFullConvertToHalf = true;
            this.ucPatientInfo1.IsPrint = false;
            this.ucPatientInfo1.Location = new System.Drawing.Point(3, 80);
            this.ucPatientInfo1.Name = "ucPatientInfo1";
            this.ucPatientInfo1.Size = new System.Drawing.Size(747, 360);
            this.ucPatientInfo1.TabIndex = 6;
            // 
            // ucReportTop1
            // 
            this.ucReportTop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(177)))), ((int)(((byte)(201)))));
            this.ucReportTop1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucReportTop1.IsFullConvertToHalf = true;
            this.ucReportTop1.IsPrint = false;
            this.ucReportTop1.Location = new System.Drawing.Point(3, 3);
            this.ucReportTop1.Name = "ucReportTop1";
            this.ucReportTop1.PatientNO = "";
            this.ucReportTop1.ReportNO = "";
            this.ucReportTop1.ReportType = "";
            this.ucReportTop1.Size = new System.Drawing.Size(747, 77);
            this.ucReportTop1.TabIndex = 5;
            // 
            // ucDiseaseQuery1
            // 
            this.ucDiseaseQuery1.AlReport = ((System.Collections.ArrayList)(resources.GetObject("ucDiseaseQuery1.AlReport")));
            this.ucDiseaseQuery1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(177)))), ((int)(((byte)(201)))));
            this.ucDiseaseQuery1.CommonReport = null;
            this.ucDiseaseQuery1.Days = 5;
            this.ucDiseaseQuery1.Dock = System.Windows.Forms.DockStyle.Left;
            this.ucDiseaseQuery1.IsFullConvertToHalf = true;
            this.ucDiseaseQuery1.IsPrint = false;
            this.ucDiseaseQuery1.Location = new System.Drawing.Point(0, 0);
            this.ucDiseaseQuery1.Name = "ucDiseaseQuery1";
            this.ucDiseaseQuery1.Patient = null;
            this.ucDiseaseQuery1.PatientType = FS.SOC.HISFC.DCP.Enum.PatientType.O;
            this.ucDiseaseQuery1.Size = new System.Drawing.Size(236, 658);
            this.ucDiseaseQuery1.TabIndex = 0;
            // 
            // ucDiseaseReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(177)))), ((int)(((byte)(201)))));
            this.Controls.Add(this.tcReport);
            this.Controls.Add(this.neuSplitter1);
            this.Controls.Add(this.ucDiseaseQuery1);
            this.Name = "ucDiseaseReport";
            this.Size = new System.Drawing.Size(1019, 658);
            this.tcReport.ResumeLayout(false);
            this.tpMainReport.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ucDiseaseQuery ucDiseaseQuery1;
        private FS.FrameWork.WinForms.Controls.NeuSplitter neuSplitter1;
        private FS.FrameWork.WinForms.Controls.NeuTabControl tcReport;
        private ucDiseaseInfo ucDiseaseInfo1;
        private System.Windows.Forms.TabPage tpMainReport;
        private ucPatientInfo ucPatientInfo1;
        private ucReportTop ucReportTop1;
        private ucReportButtom ucReportButtom1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
    }
}
