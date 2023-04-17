namespace FS.SOC.HISFC.Assign.Components.Common.Triage
{
    partial class ucTriageManager
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

       

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucTriageManager));
            this.gbAddInfo = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.txtIp = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.cmbRefreshTime = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbPrivNurse = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ckAutoTriage = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ckPauseRefresh = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.panel = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.plTriaggingPatient = new System.Windows.Forms.Panel();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.gbPatientQuery = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.lbF10 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtCardNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lbCardNO = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuSplitter1 = new FS.FrameWork.WinForms.Controls.NeuSplitter();
            this.plPatientInfo = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.plTriggedPatient = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.plTriggedQueue = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.plTriggedWaiting = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.plTriggedCalling = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.plNoseePatient = new System.Windows.Forms.Panel();
            this.plTriggedArrive = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.plTriggedSee = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.gbAddInfo.SuspendLayout();
            this.panel.SuspendLayout();
            this.neuPanel1.SuspendLayout();
            this.gbPatientQuery.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            this.plTriggedPatient.SuspendLayout();
            this.neuPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbAddInfo
            // 
            this.gbAddInfo.Controls.Add(this.txtIp);
            this.gbAddInfo.Controls.Add(this.cmbRefreshTime);
            this.gbAddInfo.Controls.Add(this.neuLabel2);
            this.gbAddInfo.Controls.Add(this.lbPrivNurse);
            this.gbAddInfo.Controls.Add(this.ckAutoTriage);
            this.gbAddInfo.Controls.Add(this.ckPauseRefresh);
            this.gbAddInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbAddInfo.Location = new System.Drawing.Point(0, 448);
            this.gbAddInfo.Name = "gbAddInfo";
            this.gbAddInfo.Size = new System.Drawing.Size(1155, 62);
            this.gbAddInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbAddInfo.TabIndex = 0;
            this.gbAddInfo.TabStop = false;
            this.gbAddInfo.Text = "附加信息";
            // 
            // txtIp
            // 
            this.txtIp.IsEnter2Tab = false;
            this.txtIp.Location = new System.Drawing.Point(111, 24);
            this.txtIp.Name = "txtIp";
            this.txtIp.ReadOnly = true;
            this.txtIp.Size = new System.Drawing.Size(108, 21);
            this.txtIp.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtIp.TabIndex = 5;
            // 
            // cmbRefreshTime
            // 
            this.cmbRefreshTime.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbRefreshTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbRefreshTime.FormattingEnabled = true;
            this.cmbRefreshTime.IsEnter2Tab = false;
            this.cmbRefreshTime.IsFlat = false;
            this.cmbRefreshTime.IsLike = true;
            this.cmbRefreshTime.IsListOnly = false;
            this.cmbRefreshTime.IsPopForm = true;
            this.cmbRefreshTime.IsShowCustomerList = false;
            this.cmbRefreshTime.IsShowID = false;
            this.cmbRefreshTime.IsShowIDAndName = false;
            this.cmbRefreshTime.Location = new System.Drawing.Point(427, 23);
            this.cmbRefreshTime.Name = "cmbRefreshTime";
            this.cmbRefreshTime.ShowCustomerList = false;
            this.cmbRefreshTime.ShowID = false;
            this.cmbRefreshTime.Size = new System.Drawing.Size(100, 20);
            this.cmbRefreshTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbRefreshTime.TabIndex = 4;
            this.cmbRefreshTime.Tag = "";
            this.cmbRefreshTime.ToolBarUse = false;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(356, 27);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(65, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 3;
            this.neuLabel2.Text = "刷新时间：";
            // 
            // lbPrivNurse
            // 
            this.lbPrivNurse.AutoSize = true;
            this.lbPrivNurse.ForeColor = System.Drawing.Color.Blue;
            this.lbPrivNurse.Location = new System.Drawing.Point(574, 27);
            this.lbPrivNurse.Name = "lbPrivNurse";
            this.lbPrivNurse.Size = new System.Drawing.Size(77, 12);
            this.lbPrivNurse.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbPrivNurse.TabIndex = 2;
            this.lbPrivNurse.Text = "当前护士站：";
            // 
            // ckAutoTriage
            // 
            this.ckAutoTriage.AutoSize = true;
            this.ckAutoTriage.Location = new System.Drawing.Point(20, 26);
            this.ckAutoTriage.Name = "ckAutoTriage";
            this.ckAutoTriage.Size = new System.Drawing.Size(96, 16);
            this.ckAutoTriage.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ckAutoTriage.TabIndex = 1;
            this.ckAutoTriage.Text = "自动分诊注册";
            this.ckAutoTriage.UseVisualStyleBackColor = true;
            // 
            // ckPauseRefresh
            // 
            this.ckPauseRefresh.AutoSize = true;
            this.ckPauseRefresh.Location = new System.Drawing.Point(254, 26);
            this.ckPauseRefresh.Name = "ckPauseRefresh";
            this.ckPauseRefresh.Size = new System.Drawing.Size(96, 16);
            this.ckPauseRefresh.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ckPauseRefresh.TabIndex = 0;
            this.ckPauseRefresh.Text = "暂停自动分诊";
            this.ckPauseRefresh.UseVisualStyleBackColor = true;
            // 
            // panel
            // 
            this.panel.Controls.Add(this.plTriaggingPatient);
            this.panel.Controls.Add(this.neuPanel1);
            this.panel.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(230, 448);
            this.panel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panel.TabIndex = 1;
            // 
            // plTriaggingPatient
            // 
            this.plTriaggingPatient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plTriaggingPatient.Location = new System.Drawing.Point(0, 56);
            this.plTriaggingPatient.Name = "plTriaggingPatient";
            this.plTriaggingPatient.Size = new System.Drawing.Size(230, 392);
            this.plTriaggingPatient.TabIndex = 1;
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.gbPatientQuery);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(230, 56);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // gbPatientQuery
            // 
            this.gbPatientQuery.Controls.Add(this.lbF10);
            this.gbPatientQuery.Controls.Add(this.txtCardNO);
            this.gbPatientQuery.Controls.Add(this.lbCardNO);
            this.gbPatientQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbPatientQuery.ForeColor = System.Drawing.Color.Blue;
            this.gbPatientQuery.Location = new System.Drawing.Point(0, 0);
            this.gbPatientQuery.Name = "gbPatientQuery";
            this.gbPatientQuery.Size = new System.Drawing.Size(230, 56);
            this.gbPatientQuery.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbPatientQuery.TabIndex = 2;
            this.gbPatientQuery.TabStop = false;
            this.gbPatientQuery.Text = "病人查找(F9切换)";
            // 
            // lbF10
            // 
            this.lbF10.AutoSize = true;
            this.lbF10.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbF10.Location = new System.Drawing.Point(232, 28);
            this.lbF10.Name = "lbF10";
            this.lbF10.Size = new System.Drawing.Size(23, 12);
            this.lbF10.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbF10.TabIndex = 2;
            this.lbF10.Text = "F10";
            // 
            // txtCardNO
            // 
            this.txtCardNO.IsEnter2Tab = false;
            this.txtCardNO.Location = new System.Drawing.Point(71, 25);
            this.txtCardNO.Name = "txtCardNO";
            this.txtCardNO.Size = new System.Drawing.Size(134, 21);
            this.txtCardNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCardNO.TabIndex = 1;
            // 
            // lbCardNO
            // 
            this.lbCardNO.AutoSize = true;
            this.lbCardNO.Location = new System.Drawing.Point(10, 28);
            this.lbCardNO.Name = "lbCardNO";
            this.lbCardNO.Size = new System.Drawing.Size(53, 12);
            this.lbCardNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbCardNO.TabIndex = 0;
            this.lbCardNO.Text = "病人号：";
            // 
            // neuSplitter1
            // 
            this.neuSplitter1.Location = new System.Drawing.Point(230, 0);
            this.neuSplitter1.Name = "neuSplitter1";
            this.neuSplitter1.Size = new System.Drawing.Size(3, 448);
            this.neuSplitter1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSplitter1.TabIndex = 3;
            this.neuSplitter1.TabStop = false;
            // 
            // plPatientInfo
            // 
            this.plPatientInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.plPatientInfo.Location = new System.Drawing.Point(233, 0);
            this.plPatientInfo.Name = "plPatientInfo";
            this.plPatientInfo.Size = new System.Drawing.Size(922, 56);
            this.plPatientInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plPatientInfo.TabIndex = 5;
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.plTriggedPatient);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel2.Location = new System.Drawing.Point(233, 56);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(922, 392);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 8;
            // 
            // plTriggedPatient
            // 
            this.plTriggedPatient.Controls.Add(this.plTriggedQueue);
            this.plTriggedPatient.Controls.Add(this.neuPanel3);
            this.plTriggedPatient.Controls.Add(this.plTriggedArrive);
            this.plTriggedPatient.Controls.Add(this.plTriggedSee);
            this.plTriggedPatient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plTriggedPatient.Location = new System.Drawing.Point(0, 0);
            this.plTriggedPatient.Name = "plTriggedPatient";
            this.plTriggedPatient.Size = new System.Drawing.Size(922, 392);
            this.plTriggedPatient.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plTriggedPatient.TabIndex = 1;
            // 
            // plTriggedQueue
            // 
            this.plTriggedQueue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plTriggedQueue.Location = new System.Drawing.Point(0, 0);
            this.plTriggedQueue.Name = "plTriggedQueue";
            this.plTriggedQueue.Size = new System.Drawing.Size(122, 392);
            this.plTriggedQueue.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plTriggedQueue.TabIndex = 5;
            // 
            // neuPanel3
            // 
            this.neuPanel3.Controls.Add(this.plTriggedWaiting);
            this.neuPanel3.Controls.Add(this.plTriggedCalling);
            this.neuPanel3.Controls.Add(this.plNoseePatient);
            this.neuPanel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.neuPanel3.Location = new System.Drawing.Point(122, 0);
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size(400, 392);
            this.neuPanel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 2;
            // 
            // plTriggedWaiting
            // 
            this.plTriggedWaiting.Dock = System.Windows.Forms.DockStyle.Right;
            this.plTriggedWaiting.Location = new System.Drawing.Point(0, 0);
            this.plTriggedWaiting.Name = "plTriggedWaiting";
            this.plTriggedWaiting.Size = new System.Drawing.Size(200, 192);
            this.plTriggedWaiting.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plTriggedWaiting.TabIndex = 4;
            // 
            // plTriggedCalling
            // 
            this.plTriggedCalling.Dock = System.Windows.Forms.DockStyle.Right;
            this.plTriggedCalling.Location = new System.Drawing.Point(200, 0);
            this.plTriggedCalling.Name = "plTriggedCalling";
            this.plTriggedCalling.Size = new System.Drawing.Size(200, 192);
            this.plTriggedCalling.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plTriggedCalling.TabIndex = 3;
            // 
            // plNoseePatient
            // 
            this.plNoseePatient.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plNoseePatient.Location = new System.Drawing.Point(0, 192);
            this.plNoseePatient.Name = "plNoseePatient";
            this.plNoseePatient.Size = new System.Drawing.Size(400, 200);
            this.plNoseePatient.TabIndex = 2;
            // 
            // plTriggedArrive
            // 
            this.plTriggedArrive.Dock = System.Windows.Forms.DockStyle.Right;
            this.plTriggedArrive.Location = new System.Drawing.Point(522, 0);
            this.plTriggedArrive.Name = "plTriggedArrive";
            this.plTriggedArrive.Size = new System.Drawing.Size(200, 392);
            this.plTriggedArrive.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plTriggedArrive.TabIndex = 1;
            // 
            // plTriggedSee
            // 
            this.plTriggedSee.Dock = System.Windows.Forms.DockStyle.Right;
            this.plTriggedSee.Location = new System.Drawing.Point(722, 0);
            this.plTriggedSee.Name = "plTriggedSee";
            this.plTriggedSee.Size = new System.Drawing.Size(200, 392);
            this.plTriggedSee.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plTriggedSee.TabIndex = 0;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // ucTriageManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuPanel2);
            this.Controls.Add(this.plPatientInfo);
            this.Controls.Add(this.neuSplitter1);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.gbAddInfo);
            this.Name = "ucTriageManager";
            this.Size = new System.Drawing.Size(1155, 510);
            this.gbAddInfo.ResumeLayout(false);
            this.gbAddInfo.PerformLayout();
            this.panel.ResumeLayout(false);
            this.neuPanel1.ResumeLayout(false);
            this.gbPatientQuery.ResumeLayout(false);
            this.gbPatientQuery.PerformLayout();
            this.neuPanel2.ResumeLayout(false);
            this.plTriggedPatient.ResumeLayout(false);
            this.neuPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbAddInfo;
        private FS.FrameWork.WinForms.Controls.NeuPanel panel;
        private FS.FrameWork.WinForms.Controls.NeuSplitter neuSplitter1;
        private FS.FrameWork.WinForms.Controls.NeuPanel plPatientInfo;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuPanel plTriggedPatient;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ckAutoTriage;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ckPauseRefresh;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbPrivNurse;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbRefreshTime;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtIp;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbPatientQuery;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbF10;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtCardNO;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbCardNO;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private FS.FrameWork.WinForms.Controls.NeuPanel plTriggedSee;
        private FS.FrameWork.WinForms.Controls.NeuPanel plTriggedArrive;
        private System.Windows.Forms.Panel plTriaggingPatient;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel3;
        private System.Windows.Forms.Panel plNoseePatient;
        private FS.FrameWork.WinForms.Controls.NeuPanel plTriggedWaiting;
        private FS.FrameWork.WinForms.Controls.NeuPanel plTriggedCalling;
        private FS.FrameWork.WinForms.Controls.NeuPanel plTriggedQueue;
    }
}
