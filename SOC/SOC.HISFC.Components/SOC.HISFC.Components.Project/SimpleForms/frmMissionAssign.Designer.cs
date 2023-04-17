namespace FS.SOC.HISFC.Components.Project
{
    partial class frmMissionAssign
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
            this.ndtpAccepterTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel9 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ncmbAccepter = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel7 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.nbtCancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.nbtOKAndNext = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.nbtOK = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.nbtOKAll = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.ucMissionInfo1 = new FS.SOC.HISFC.Components.Project.ucMissionInfo();
            this.neuGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ndtpAccepterTime
            // 
            this.ndtpAccepterTime.IsEnter2Tab = false;
            this.ndtpAccepterTime.Location = new System.Drawing.Point(362, 28);
            this.ndtpAccepterTime.Name = "ndtpAccepterTime";
            this.ndtpAccepterTime.Size = new System.Drawing.Size(125, 21);
            this.ndtpAccepterTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ndtpAccepterTime.TabIndex = 17;
            // 
            // neuLabel9
            // 
            this.neuLabel9.AutoSize = true;
            this.neuLabel9.Location = new System.Drawing.Point(257, 32);
            this.neuLabel9.Name = "neuLabel9";
            this.neuLabel9.Size = new System.Drawing.Size(77, 12);
            this.neuLabel9.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel9.TabIndex = 20;
            this.neuLabel9.Text = "协定完成时间";
            // 
            // ncmbAccepter
            // 
            this.ncmbAccepter.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.ncmbAccepter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ncmbAccepter.FormattingEnabled = true;
            this.ncmbAccepter.IsEnter2Tab = false;
            this.ncmbAccepter.IsFlat = false;
            this.ncmbAccepter.IsLike = true;
            this.ncmbAccepter.IsListOnly = false;
            this.ncmbAccepter.IsPopForm = true;
            this.ncmbAccepter.IsShowCustomerList = false;
            this.ncmbAccepter.IsShowID = false;
            this.ncmbAccepter.Location = new System.Drawing.Point(107, 28);
            this.ncmbAccepter.Name = "ncmbAccepter";
            this.ncmbAccepter.ShowCustomerList = false;
            this.ncmbAccepter.ShowID = false;
            this.ncmbAccepter.Size = new System.Drawing.Size(120, 20);
            this.ncmbAccepter.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.ncmbAccepter.TabIndex = 18;
            this.ncmbAccepter.Tag = "";
            this.ncmbAccepter.ToolBarUse = false;
            // 
            // neuLabel7
            // 
            this.neuLabel7.AutoSize = true;
            this.neuLabel7.Location = new System.Drawing.Point(32, 32);
            this.neuLabel7.Name = "neuLabel7";
            this.neuLabel7.Size = new System.Drawing.Size(53, 12);
            this.neuLabel7.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel7.TabIndex = 19;
            this.neuLabel7.Text = "受 任 人";
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.neuLabel9);
            this.neuGroupBox1.Controls.Add(this.ndtpAccepterTime);
            this.neuGroupBox1.Controls.Add(this.neuLabel7);
            this.neuGroupBox1.Controls.Add(this.ncmbAccepter);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 390);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(520, 64);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 21;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "分派任务";
            // 
            // nbtCancel
            // 
            this.nbtCancel.Location = new System.Drawing.Point(410, 472);
            this.nbtCancel.Name = "nbtCancel";
            this.nbtCancel.Size = new System.Drawing.Size(75, 23);
            this.nbtCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nbtCancel.TabIndex = 25;
            this.nbtCancel.Text = "取消";
            this.nbtCancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.nbtCancel.UseVisualStyleBackColor = true;
            this.nbtCancel.Click += new System.EventHandler(this.nbtCancel_Click);
            // 
            // nbtOKAndNext
            // 
            this.nbtOKAndNext.Location = new System.Drawing.Point(126, 472);
            this.nbtOKAndNext.Name = "nbtOKAndNext";
            this.nbtOKAndNext.Size = new System.Drawing.Size(129, 23);
            this.nbtOKAndNext.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nbtOKAndNext.TabIndex = 24;
            this.nbtOKAndNext.Text = "确定并分派下一个";
            this.nbtOKAndNext.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.nbtOKAndNext.UseVisualStyleBackColor = true;
            this.nbtOKAndNext.Click += new System.EventHandler(this.nbtOKAndNext_Click);
            // 
            // nbtOK
            // 
            this.nbtOK.Location = new System.Drawing.Point(34, 472);
            this.nbtOK.Name = "nbtOK";
            this.nbtOK.Size = new System.Drawing.Size(75, 23);
            this.nbtOK.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nbtOK.TabIndex = 23;
            this.nbtOK.Text = "确定";
            this.nbtOK.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.nbtOK.UseVisualStyleBackColor = true;
            this.nbtOK.Click += new System.EventHandler(this.nbtOK_Click);
            // 
            // nbtOKAll
            // 
            this.nbtOKAll.Location = new System.Drawing.Point(272, 472);
            this.nbtOKAll.Name = "nbtOKAll";
            this.nbtOKAll.Size = new System.Drawing.Size(121, 23);
            this.nbtOKAll.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nbtOKAll.TabIndex = 26;
            this.nbtOKAll.Text = "按模块批量分派";
            this.nbtOKAll.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.nbtOKAll.UseVisualStyleBackColor = true;
            this.nbtOKAll.Click += new System.EventHandler(this.nbtOKAll_Click);
            // 
            // ucMissionInfo1
            // 
            this.ucMissionInfo1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucMissionInfo1.Location = new System.Drawing.Point(0, 0);
            this.ucMissionInfo1.Name = "ucMissionInfo1";
            this.ucMissionInfo1.Size = new System.Drawing.Size(520, 390);
            this.ucMissionInfo1.TabIndex = 0;
            // 
            // frmMissionAssign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 507);
            this.Controls.Add(this.nbtOKAll);
            this.Controls.Add(this.nbtCancel);
            this.Controls.Add(this.nbtOKAndNext);
            this.Controls.Add(this.nbtOK);
            this.Controls.Add(this.neuGroupBox1);
            this.Controls.Add(this.ucMissionInfo1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMissionAssign";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "任务分派";
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ucMissionInfo ucMissionInfo1;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker ndtpAccepterTime;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel9;
        private FS.FrameWork.WinForms.Controls.NeuComboBox ncmbAccepter;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel7;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuButton nbtCancel;
        private FS.FrameWork.WinForms.Controls.NeuButton nbtOKAndNext;
        private FS.FrameWork.WinForms.Controls.NeuButton nbtOK;
        private FS.FrameWork.WinForms.Controls.NeuButton nbtOKAll;

    }
}