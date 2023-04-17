namespace GJLocal.HISFC.Components.OpGuide.DrugStore
{
    partial class ucCallQueueManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lbDeptName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbShow = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblTitle = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dtpAutoPrintBegin = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.isAutoRefresh = new System.Windows.Forms.CheckBox();
            this.lbState = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbDept = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel1.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.neuPanel3);
            this.neuPanel1.Controls.Add(this.neuPanel2);
            this.neuPanel1.Location = new System.Drawing.Point(3, 3);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(732, 440);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // neuPanel3
            // 
            this.neuPanel3.Location = new System.Drawing.Point(3, 109);
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size(726, 328);
            this.neuPanel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 1;
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.lbDept);
            this.neuPanel2.Controls.Add(this.lbState);
            this.neuPanel2.Controls.Add(this.lbDeptName);
            this.neuPanel2.Controls.Add(this.lbShow);
            this.neuPanel2.Controls.Add(this.lblTitle);
            this.neuPanel2.Controls.Add(this.dtpAutoPrintBegin);
            this.neuPanel2.Controls.Add(this.isAutoRefresh);
            this.neuPanel2.Location = new System.Drawing.Point(3, 3);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(726, 108);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 0;
            // 
            // lbDeptName
            // 
            this.lbDeptName.AutoSize = true;
            this.lbDeptName.Location = new System.Drawing.Point(579, 71);
            this.lbDeptName.Name = "lbDeptName";
            this.lbDeptName.Size = new System.Drawing.Size(77, 12);
            this.lbDeptName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbDeptName.TabIndex = 5;
            this.lbDeptName.Text = "当前叫号科室";
            // 
            // lbShow
            // 
            this.lbShow.AutoSize = true;
            this.lbShow.Location = new System.Drawing.Point(381, 71);
            this.lbShow.Name = "lbShow";
            this.lbShow.Size = new System.Drawing.Size(53, 12);
            this.lbShow.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbShow.TabIndex = 4;
            this.lbShow.Text = "正在叫号";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblTitle.Location = new System.Drawing.Point(296, 14);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(93, 20);
            this.lblTitle.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "药房叫号";
            // 
            // dtpAutoPrintBegin
            // 
            this.dtpAutoPrintBegin.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpAutoPrintBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpAutoPrintBegin.IsEnter2Tab = false;
            this.dtpAutoPrintBegin.Location = new System.Drawing.Point(12, 62);
            this.dtpAutoPrintBegin.Name = "dtpAutoPrintBegin";
            this.dtpAutoPrintBegin.Size = new System.Drawing.Size(162, 21);
            this.dtpAutoPrintBegin.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpAutoPrintBegin.TabIndex = 2;
            this.dtpAutoPrintBegin.Value = new System.DateTime(2012, 6, 30, 15, 18, 1, 0);
            // 
            // isAutoRefresh
            // 
            this.isAutoRefresh.AutoSize = true;
            this.isAutoRefresh.Location = new System.Drawing.Point(12, 40);
            this.isAutoRefresh.Name = "isAutoRefresh";
            this.isAutoRefresh.Size = new System.Drawing.Size(96, 16);
            this.isAutoRefresh.TabIndex = 0;
            this.isAutoRefresh.Text = "自动刷新注册";
            this.isAutoRefresh.UseVisualStyleBackColor = true;
            this.isAutoRefresh.CheckedChanged += new System.EventHandler(this.isAutoRefresh_CheckedChanged);
            // 
            // lbState
            // 
            this.lbState.AutoSize = true;
            this.lbState.Location = new System.Drawing.Point(316, 71);
            this.lbState.Name = "lbState";
            this.lbState.Size = new System.Drawing.Size(59, 12);
            this.lbState.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbState.TabIndex = 6;
            this.lbState.Text = "当前状态:";
            // 
            // lbDept
            // 
            this.lbDept.AutoSize = true;
            this.lbDept.Location = new System.Drawing.Point(538, 71);
            this.lbDept.Name = "lbDept";
            this.lbDept.Size = new System.Drawing.Size(35, 12);
            this.lbDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbDept.TabIndex = 7;
            this.lbDept.Text = "科室:";
            // 
            // ucCallQueueManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucCallQueueManager";
            this.Size = new System.Drawing.Size(738, 443);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel2.ResumeLayout(false);
            this.neuPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel3;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private System.Windows.Forms.CheckBox isAutoRefresh;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpAutoPrintBegin;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblTitle;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbShow;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbDeptName;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbDept;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbState;
    }
}