namespace FS.SOC.HISFC.Components.Project
{
    partial class frmMissionInput
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
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel4 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.nbtCancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.nbtOKAndNext = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.nbtOK = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.ucMissionInfo1 = new FS.SOC.HISFC.Components.Project.ucMissionInfo();
            this.neuPanel1.SuspendLayout();
            this.neuPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.ucMissionInfo1);
            this.neuPanel1.Controls.Add(this.neuPanel4);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(518, 508);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // neuPanel4
            // 
            this.neuPanel4.Controls.Add(this.nbtCancel);
            this.neuPanel4.Controls.Add(this.nbtOKAndNext);
            this.neuPanel4.Controls.Add(this.nbtOK);
            this.neuPanel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuPanel4.Location = new System.Drawing.Point(0, 451);
            this.neuPanel4.Name = "neuPanel4";
            this.neuPanel4.Size = new System.Drawing.Size(518, 57);
            this.neuPanel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel4.TabIndex = 2;
            // 
            // nbtCancel
            // 
            this.nbtCancel.Location = new System.Drawing.Point(428, 16);
            this.nbtCancel.Name = "nbtCancel";
            this.nbtCancel.Size = new System.Drawing.Size(75, 23);
            this.nbtCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nbtCancel.TabIndex = 22;
            this.nbtCancel.Text = "取消";
            this.nbtCancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.nbtCancel.UseVisualStyleBackColor = true;
            this.nbtCancel.Click += new System.EventHandler(this.nbtCancel_Click);
            // 
            // nbtOKAndNext
            // 
            this.nbtOKAndNext.Location = new System.Drawing.Point(270, 16);
            this.nbtOKAndNext.Name = "nbtOKAndNext";
            this.nbtOKAndNext.Size = new System.Drawing.Size(129, 23);
            this.nbtOKAndNext.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nbtOKAndNext.TabIndex = 21;
            this.nbtOKAndNext.Text = "完成并录入下一个";
            this.nbtOKAndNext.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.nbtOKAndNext.UseVisualStyleBackColor = true;
            this.nbtOKAndNext.Click += new System.EventHandler(this.nbtOKAndNext_Click);
            // 
            // nbtOK
            // 
            this.nbtOK.Location = new System.Drawing.Point(160, 16);
            this.nbtOK.Name = "nbtOK";
            this.nbtOK.Size = new System.Drawing.Size(75, 23);
            this.nbtOK.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nbtOK.TabIndex = 20;
            this.nbtOK.Text = "完成录入";
            this.nbtOK.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.nbtOK.UseVisualStyleBackColor = true;
            this.nbtOK.Click += new System.EventHandler(this.nbtOK_Click);
            // 
            // ucMissionInfo1
            // 
            this.ucMissionInfo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMissionInfo1.Location = new System.Drawing.Point(0, 0);
            this.ucMissionInfo1.Name = "ucMissionInfo1";
            this.ucMissionInfo1.Size = new System.Drawing.Size(518, 451);
            this.ucMissionInfo1.TabIndex = 3;
            // 
            // frmMissionInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 508);
            this.Controls.Add(this.neuPanel1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(534, 546);
            this.MinimizeBox = false;
            this.Name = "frmMissionInput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "问题录入";
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel4;
        private FS.FrameWork.WinForms.Controls.NeuButton nbtCancel;
        private FS.FrameWork.WinForms.Controls.NeuButton nbtOKAndNext;
        private FS.FrameWork.WinForms.Controls.NeuButton nbtOK;
        private ucMissionInfo ucMissionInfo1;
    }
}