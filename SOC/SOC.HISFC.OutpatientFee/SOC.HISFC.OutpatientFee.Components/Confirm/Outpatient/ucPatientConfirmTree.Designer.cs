namespace FS.SOC.HISFC.OutpatientFee.Components.Confirm.Outpatient
{
    partial class ucPatientConfirmTree
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
            this.tabConfirmPatient = new FS.FrameWork.WinForms.Controls.NeuTabControl();
            this.tpNoConfirm = new System.Windows.Forms.TabPage();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtCardNO1 = new FS.SOC.HISFC.OutpatientFee.Components.Common.Controls.txtCardNO();
            this.neuTreeView1 = new FS.FrameWork.WinForms.Controls.NeuTreeView();
            this.tabConfirmPatient.SuspendLayout();
            this.tpNoConfirm.SuspendLayout();
            this.neuPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabConfirmPatient
            // 
            this.tabConfirmPatient.Controls.Add(this.tpNoConfirm);
            this.tabConfirmPatient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabConfirmPatient.Location = new System.Drawing.Point(0, 0);
            this.tabConfirmPatient.Multiline = true;
            this.tabConfirmPatient.Name = "tabConfirmPatient";
            this.tabConfirmPatient.SelectedIndex = 0;
            this.tabConfirmPatient.Size = new System.Drawing.Size(279, 487);
            this.tabConfirmPatient.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabConfirmPatient.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tabConfirmPatient.TabIndex = 0;
            // 
            // tpNoConfirm
            // 
            this.tpNoConfirm.Controls.Add(this.neuTreeView1);
            this.tpNoConfirm.Controls.Add(this.neuPanel1);
            this.tpNoConfirm.Location = new System.Drawing.Point(4, 22);
            this.tpNoConfirm.Name = "tpNoConfirm";
            this.tpNoConfirm.Padding = new System.Windows.Forms.Padding(3);
            this.tpNoConfirm.Size = new System.Drawing.Size(271, 461);
            this.tpNoConfirm.TabIndex = 0;
            this.tpNoConfirm.Text = "待确认患者";
            this.tpNoConfirm.UseVisualStyleBackColor = true;
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.txtCardNO1);
            this.neuPanel1.Controls.Add(this.neuLabel1);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(3, 3);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(265, 51);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(17, 17);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(89, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "病历（卡）号：";
            // 
            // txtCardNO1
            // 
            this.txtCardNO1.IsEnter2Tab = false;
            this.txtCardNO1.Location = new System.Drawing.Point(106, 14);
            this.txtCardNO1.Name = "txtCardNO1";
            this.txtCardNO1.Size = new System.Drawing.Size(145, 21);
            this.txtCardNO1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCardNO1.TabIndex = 1;
            // 
            // neuTreeView1
            // 
            this.neuTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuTreeView1.HideSelection = false;
            this.neuTreeView1.Location = new System.Drawing.Point(3, 54);
            this.neuTreeView1.Name = "neuTreeView1";
            this.neuTreeView1.Size = new System.Drawing.Size(265, 404);
            this.neuTreeView1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTreeView1.TabIndex = 1;
            // 
            // ucPatientConfirmTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabConfirmPatient);
            this.Name = "ucPatientConfirmTree";
            this.Size = new System.Drawing.Size(279, 487);
            this.tabConfirmPatient.ResumeLayout(false);
            this.tpNoConfirm.ResumeLayout(false);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuTabControl tabConfirmPatient;
        private System.Windows.Forms.TabPage tpNoConfirm;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.SOC.HISFC.OutpatientFee.Components.Common.Controls.txtCardNO txtCardNO1;
        private FS.FrameWork.WinForms.Controls.NeuTreeView neuTreeView1;
    }
}
