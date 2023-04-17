namespace FS.HISFC.Components.Operation
{
    partial class ucCancelApplicationApply
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
            this.nlbTitle = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbApplicationID = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbApplicationName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbApplyDate = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbExecDate = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ntxtCancelApplyReason = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nbtOK = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.nbtCancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.SuspendLayout();
            // 
            // nlbTitle
            // 
            this.nlbTitle.AutoSize = true;
            this.nlbTitle.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlbTitle.Location = new System.Drawing.Point(62, 18);
            this.nlbTitle.Name = "nlbTitle";
            this.nlbTitle.Size = new System.Drawing.Size(169, 19);
            this.nlbTitle.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbTitle.TabIndex = 0;
            this.nlbTitle.Text = "手术安排取消申请";
            // 
            // nlbApplicationID
            // 
            this.nlbApplicationID.AutoSize = true;
            this.nlbApplicationID.Location = new System.Drawing.Point(15, 47);
            this.nlbApplicationID.Name = "nlbApplicationID";
            this.nlbApplicationID.Size = new System.Drawing.Size(101, 12);
            this.nlbApplicationID.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbApplicationID.TabIndex = 1;
            this.nlbApplicationID.Text = "手术申请号:23010";
            // 
            // nlbApplicationName
            // 
            this.nlbApplicationName.AutoSize = true;
            this.nlbApplicationName.Location = new System.Drawing.Point(15, 73);
            this.nlbApplicationName.Name = "nlbApplicationName";
            this.nlbApplicationName.Size = new System.Drawing.Size(131, 12);
            this.nlbApplicationName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbApplicationName.TabIndex = 2;
            this.nlbApplicationName.Text = "手术名称:脑动脉穿刺术";
            // 
            // nlbApplyDate
            // 
            this.nlbApplyDate.AutoSize = true;
            this.nlbApplyDate.Location = new System.Drawing.Point(15, 101);
            this.nlbApplyDate.Name = "nlbApplyDate";
            this.nlbApplyDate.Size = new System.Drawing.Size(125, 12);
            this.nlbApplyDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbApplyDate.TabIndex = 3;
            this.nlbApplyDate.Text = "申请时间：2014-08-20";
            // 
            // nlbExecDate
            // 
            this.nlbExecDate.AutoSize = true;
            this.nlbExecDate.Location = new System.Drawing.Point(15, 130);
            this.nlbExecDate.Name = "nlbExecDate";
            this.nlbExecDate.Size = new System.Drawing.Size(137, 12);
            this.nlbExecDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbExecDate.TabIndex = 4;
            this.nlbExecDate.Text = "拟手术时间：2014-08-20";
            // 
            // ntxtCancelApplyReason
            // 
            this.ntxtCancelApplyReason.IsEnter2Tab = false;
            this.ntxtCancelApplyReason.Location = new System.Drawing.Point(86, 155);
            this.ntxtCancelApplyReason.Name = "ntxtCancelApplyReason";
            this.ntxtCancelApplyReason.Size = new System.Drawing.Size(175, 21);
            this.ntxtCancelApplyReason.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ntxtCancelApplyReason.TabIndex = 5;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel1.ForeColor = System.Drawing.Color.Red;
            this.neuLabel1.Location = new System.Drawing.Point(17, 158);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(64, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 6;
            this.neuLabel1.Text = "取消原因:";
            // 
            // nbtOK
            // 
            this.nbtOK.Location = new System.Drawing.Point(41, 201);
            this.nbtOK.Name = "nbtOK";
            this.nbtOK.Size = new System.Drawing.Size(75, 23);
            this.nbtOK.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nbtOK.TabIndex = 7;
            this.nbtOK.Text = "保存";
            this.nbtOK.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.nbtOK.UseVisualStyleBackColor = true;
            // 
            // nbtCancel
            // 
            this.nbtCancel.Location = new System.Drawing.Point(156, 201);
            this.nbtCancel.Name = "nbtCancel";
            this.nbtCancel.Size = new System.Drawing.Size(75, 23);
            this.nbtCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nbtCancel.TabIndex = 8;
            this.nbtCancel.Text = "取消";
            this.nbtCancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.nbtCancel.UseVisualStyleBackColor = true;
            // 
            // ucCancelApplicationApply
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.nbtCancel);
            this.Controls.Add(this.nbtOK);
            this.Controls.Add(this.neuLabel1);
            this.Controls.Add(this.ntxtCancelApplyReason);
            this.Controls.Add(this.nlbExecDate);
            this.Controls.Add(this.nlbApplyDate);
            this.Controls.Add(this.nlbApplicationName);
            this.Controls.Add(this.nlbApplicationID);
            this.Controls.Add(this.nlbTitle);
            this.Name = "ucCancelApplicationApply";
            this.Size = new System.Drawing.Size(292, 241);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuLabel nlbTitle;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbApplicationID;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbApplicationName;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbApplyDate;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbExecDate;
        private FS.FrameWork.WinForms.Controls.NeuTextBox ntxtCancelApplyReason;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuButton nbtOK;
        private FS.FrameWork.WinForms.Controls.NeuButton nbtCancel;
    }
}