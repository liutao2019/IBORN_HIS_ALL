namespace FS.SOC.HISFC.Components.DCP.Controls
{
    partial class ucDiseaseQuery
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
            this.gbQueryCondition = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.llbPatientNO = new FS.FrameWork.WinForms.Controls.NeuLinkLabel();
            this.lbDocterNO = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbPatientName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbReportNO = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbEndTime = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbBeginTime = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbQueryType = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dtpEndTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.dtpBeginTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.cmbQueryContent = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.txtDocterNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtPatientName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtPatientNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtReportNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuTabControl1 = new FS.FrameWork.WinForms.Controls.NeuTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tvReport = new FS.HISFC.Components.Common.Controls.baseTreeView();
            this.gbQueryCondition.SuspendLayout();
            this.neuTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbQueryCondition
            // 
            this.gbQueryCondition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbQueryCondition.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.gbQueryCondition.Controls.Add(this.llbPatientNO);
            this.gbQueryCondition.Controls.Add(this.lbDocterNO);
            this.gbQueryCondition.Controls.Add(this.lbPatientName);
            this.gbQueryCondition.Controls.Add(this.lbReportNO);
            this.gbQueryCondition.Controls.Add(this.lbEndTime);
            this.gbQueryCondition.Controls.Add(this.lbBeginTime);
            this.gbQueryCondition.Controls.Add(this.lbQueryType);
            this.gbQueryCondition.Controls.Add(this.dtpEndTime);
            this.gbQueryCondition.Controls.Add(this.dtpBeginTime);
            this.gbQueryCondition.Controls.Add(this.cmbQueryContent);
            this.gbQueryCondition.Controls.Add(this.txtDocterNO);
            this.gbQueryCondition.Controls.Add(this.txtPatientName);
            this.gbQueryCondition.Controls.Add(this.txtPatientNO);
            this.gbQueryCondition.Controls.Add(this.txtReportNO);
            this.gbQueryCondition.Font = new System.Drawing.Font("宋体", 1F);
            this.gbQueryCondition.Location = new System.Drawing.Point(3, 3);
            this.gbQueryCondition.Name = "gbQueryCondition";
            this.gbQueryCondition.Size = new System.Drawing.Size(232, 216);
            this.gbQueryCondition.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbQueryCondition.TabIndex = 0;
            this.gbQueryCondition.TabStop = false;
            // 
            // llbPatientNO
            // 
            this.llbPatientNO.AutoSize = true;
            this.llbPatientNO.Font = new System.Drawing.Font("宋体", 9F);
            this.llbPatientNO.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.llbPatientNO.Location = new System.Drawing.Point(8, 128);
            this.llbPatientNO.Name = "llbPatientNO";
            this.llbPatientNO.Size = new System.Drawing.Size(65, 12);
            this.llbPatientNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.llbPatientNO.TabIndex = 14;
            this.llbPatientNO.TabStop = true;
            this.llbPatientNO.Text = "门诊卡号：";
            this.llbPatientNO.Click += new System.EventHandler(this.llbPatientNO_Click);
            // 
            // lbDocterNO
            // 
            this.lbDocterNO.AutoSize = true;
            this.lbDocterNO.Font = new System.Drawing.Font("宋体", 9F);
            this.lbDocterNO.Location = new System.Drawing.Point(8, 183);
            this.lbDocterNO.Name = "lbDocterNO";
            this.lbDocterNO.Size = new System.Drawing.Size(65, 12);
            this.lbDocterNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbDocterNO.TabIndex = 13;
            this.lbDocterNO.Text = "医生工号：";
            // 
            // lbPatientName
            // 
            this.lbPatientName.AutoSize = true;
            this.lbPatientName.Font = new System.Drawing.Font("宋体", 9F);
            this.lbPatientName.Location = new System.Drawing.Point(8, 156);
            this.lbPatientName.Name = "lbPatientName";
            this.lbPatientName.Size = new System.Drawing.Size(65, 12);
            this.lbPatientName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbPatientName.TabIndex = 12;
            this.lbPatientName.Text = "患者姓名：";
            // 
            // lbReportNO
            // 
            this.lbReportNO.AutoSize = true;
            this.lbReportNO.Font = new System.Drawing.Font("宋体", 9F);
            this.lbReportNO.Location = new System.Drawing.Point(8, 100);
            this.lbReportNO.Name = "lbReportNO";
            this.lbReportNO.Size = new System.Drawing.Size(65, 12);
            this.lbReportNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbReportNO.TabIndex = 10;
            this.lbReportNO.Text = "报告卡号：";
            // 
            // lbEndTime
            // 
            this.lbEndTime.AutoSize = true;
            this.lbEndTime.Font = new System.Drawing.Font("宋体", 9F);
            this.lbEndTime.Location = new System.Drawing.Point(8, 72);
            this.lbEndTime.Name = "lbEndTime";
            this.lbEndTime.Size = new System.Drawing.Size(65, 12);
            this.lbEndTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbEndTime.TabIndex = 9;
            this.lbEndTime.Text = "结束时间：";
            // 
            // lbBeginTime
            // 
            this.lbBeginTime.AutoSize = true;
            this.lbBeginTime.Font = new System.Drawing.Font("宋体", 9F);
            this.lbBeginTime.Location = new System.Drawing.Point(8, 44);
            this.lbBeginTime.Name = "lbBeginTime";
            this.lbBeginTime.Size = new System.Drawing.Size(65, 12);
            this.lbBeginTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbBeginTime.TabIndex = 8;
            this.lbBeginTime.Text = "起始时间：";
            // 
            // lbQueryType
            // 
            this.lbQueryType.AutoSize = true;
            this.lbQueryType.Font = new System.Drawing.Font("宋体", 9F);
            this.lbQueryType.Location = new System.Drawing.Point(8, 16);
            this.lbQueryType.Name = "lbQueryType";
            this.lbQueryType.Size = new System.Drawing.Size(65, 12);
            this.lbQueryType.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbQueryType.TabIndex = 7;
            this.lbQueryType.Text = "查询类型：";
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.Font = new System.Drawing.Font("宋体", 9F);
            this.dtpEndTime.IsEnter2Tab = true;
            this.dtpEndTime.Location = new System.Drawing.Point(90, 69);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.Size = new System.Drawing.Size(133, 21);
            this.dtpEndTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpEndTime.TabIndex = 6;
            // 
            // dtpBeginTime
            // 
            this.dtpBeginTime.Font = new System.Drawing.Font("宋体", 9F);
            this.dtpBeginTime.IsEnter2Tab = true;
            this.dtpBeginTime.Location = new System.Drawing.Point(90, 41);
            this.dtpBeginTime.Name = "dtpBeginTime";
            this.dtpBeginTime.Size = new System.Drawing.Size(133, 21);
            this.dtpBeginTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpBeginTime.TabIndex = 5;
            // 
            // cmbQueryContent
            // 
            this.cmbQueryContent.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbQueryContent.Font = new System.Drawing.Font("宋体", 9F);
            this.cmbQueryContent.FormattingEnabled = true;
            this.cmbQueryContent.IsEnter2Tab = false;
            this.cmbQueryContent.IsFlat = false;
            this.cmbQueryContent.IsLike = true;
            this.cmbQueryContent.IsListOnly = false;
            this.cmbQueryContent.IsPopForm = true;
            this.cmbQueryContent.IsShowCustomerList = false;
            this.cmbQueryContent.IsShowID = false;
            this.cmbQueryContent.Location = new System.Drawing.Point(90, 12);
            this.cmbQueryContent.Name = "cmbQueryContent";
            this.cmbQueryContent.PopForm = null;
            this.cmbQueryContent.ShowCustomerList = false;
            this.cmbQueryContent.ShowID = false;
            this.cmbQueryContent.Size = new System.Drawing.Size(133, 20);
            this.cmbQueryContent.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbQueryContent.TabIndex = 4;
            this.cmbQueryContent.Tag = "";
            this.cmbQueryContent.ToolBarUse = false;
            this.cmbQueryContent.SelectedValueChanged += new System.EventHandler(this.cmbQueryContent_SelectedValueChanged);
            // 
            // txtDocterNO
            // 
            this.txtDocterNO.Font = new System.Drawing.Font("宋体", 9F);
            this.txtDocterNO.IsEnter2Tab = false;
            this.txtDocterNO.Location = new System.Drawing.Point(90, 180);
            this.txtDocterNO.Name = "txtDocterNO";
            this.txtDocterNO.Size = new System.Drawing.Size(133, 21);
            this.txtDocterNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtDocterNO.TabIndex = 3;
            // 
            // txtPatientName
            // 
            this.txtPatientName.Font = new System.Drawing.Font("宋体", 9F);
            this.txtPatientName.IsEnter2Tab = false;
            this.txtPatientName.Location = new System.Drawing.Point(90, 153);
            this.txtPatientName.Name = "txtPatientName";
            this.txtPatientName.Size = new System.Drawing.Size(133, 21);
            this.txtPatientName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtPatientName.TabIndex = 2;
            // 
            // txtPatientNO
            // 
            this.txtPatientNO.Font = new System.Drawing.Font("宋体", 9F);
            this.txtPatientNO.IsEnter2Tab = false;
            this.txtPatientNO.Location = new System.Drawing.Point(90, 125);
            this.txtPatientNO.Name = "txtPatientNO";
            this.txtPatientNO.Size = new System.Drawing.Size(133, 21);
            this.txtPatientNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtPatientNO.TabIndex = 1;
            this.txtPatientNO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatientNO_KeyDown);
            // 
            // txtReportNO
            // 
            this.txtReportNO.Font = new System.Drawing.Font("宋体", 9F);
            this.txtReportNO.IsEnter2Tab = false;
            this.txtReportNO.Location = new System.Drawing.Point(90, 97);
            this.txtReportNO.Name = "txtReportNO";
            this.txtReportNO.Size = new System.Drawing.Size(133, 21);
            this.txtReportNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtReportNO.TabIndex = 0;
            // 
            // neuTabControl1
            // 
            this.neuTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.neuTabControl1.Controls.Add(this.tabPage1);
            this.neuTabControl1.Location = new System.Drawing.Point(3, 225);
            this.neuTabControl1.Name = "neuTabControl1";
            this.neuTabControl1.SelectedIndex = 0;
            this.neuTabControl1.Size = new System.Drawing.Size(232, 303);
            this.neuTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.neuTabControl1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.tabPage1.Controls.Add(this.tvReport);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(224, 277);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "患者信息";
            // 
            // tvReport
            // 
            this.tvReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.tvReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvReport.HideSelection = false;
            this.tvReport.Location = new System.Drawing.Point(3, 3);
            this.tvReport.Name = "tvReport";
            this.tvReport.Size = new System.Drawing.Size(218, 271);
            this.tvReport.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tvReport.TabIndex = 0;
            // 
            // ucDiseaseQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(177)))), ((int)(((byte)(201)))));
            this.Controls.Add(this.neuTabControl1);
            this.Controls.Add(this.gbQueryCondition);
            this.Name = "ucDiseaseQuery";
            this.Size = new System.Drawing.Size(238, 531);
            this.gbQueryCondition.ResumeLayout(false);
            this.gbQueryCondition.PerformLayout();
            this.neuTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbQueryCondition;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbDocterNO;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbPatientName;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbReportNO;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbEndTime;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbBeginTime;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbQueryType;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpEndTime;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbQueryContent;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtDocterNO;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtPatientName;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtPatientNO;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtReportNO;
        private FS.FrameWork.WinForms.Controls.NeuLinkLabel llbPatientNO;
        private FS.FrameWork.WinForms.Controls.NeuTabControl neuTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private FS.HISFC.Components.Common.Controls.baseTreeView tvReport;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpBeginTime;
    }
}
