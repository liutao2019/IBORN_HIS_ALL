namespace FS.SOC.HISFC.RADT.Components.Administration
{
    partial class ucModifyInpatient
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
            this.neuGroupBox3 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.ucQueryInpatientNo1 = new FS.HISFC.Components.Common.Controls.ucQueryInpatientNo();
            this.gbQuery = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.txtOldPact = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblNewPact = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblOldPact = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbNewPact = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuGroupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuGroupBox3
            // 
            this.neuGroupBox3.Controls.Add(this.txtOldPact);
            this.neuGroupBox3.Controls.Add(this.lblNewPact);
            this.neuGroupBox3.Controls.Add(this.lblOldPact);
            this.neuGroupBox3.Controls.Add(this.cmbNewPact);
            this.neuGroupBox3.Controls.Add(this.ucQueryInpatientNo1);
            this.neuGroupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox3.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox3.Name = "neuGroupBox3";
            this.neuGroupBox3.Size = new System.Drawing.Size(720, 49);
            this.neuGroupBox3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox3.TabIndex = 101;
            this.neuGroupBox3.TabStop = false;
            // 
            // ucQueryInpatientNo1
            // 
            this.ucQueryInpatientNo1.DefaultInputType = 0;
            this.ucQueryInpatientNo1.InputType = 0;
            this.ucQueryInpatientNo1.Location = new System.Drawing.Point(21, 15);
            this.ucQueryInpatientNo1.Name = "ucQueryInpatientNo1";
            this.ucQueryInpatientNo1.PatientInState = "ALL";
            this.ucQueryInpatientNo1.ShowState = FS.HISFC.Components.Common.Controls.enuShowState.InhosBeforBalanced;
            this.ucQueryInpatientNo1.Size = new System.Drawing.Size(206, 27);
            this.ucQueryInpatientNo1.TabIndex = 1;
            // 
            // gbQuery
            // 
            this.gbQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbQuery.Location = new System.Drawing.Point(0, 49);
            this.gbQuery.Name = "gbQuery";
            this.gbQuery.Size = new System.Drawing.Size(720, 422);
            this.gbQuery.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbQuery.TabIndex = 102;
            this.gbQuery.TabStop = false;
            this.gbQuery.Text = "住院信息";
            // 
            // txtOldPact
            // 
            this.txtOldPact.IsEnter2Tab = false;
            this.txtOldPact.Location = new System.Drawing.Point(329, 20);
            this.txtOldPact.Name = "txtOldPact";
            this.txtOldPact.ReadOnly = true;
            this.txtOldPact.Size = new System.Drawing.Size(137, 21);
            this.txtOldPact.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtOldPact.TabIndex = 9;
            // 
            // lblNewPact
            // 
            this.lblNewPact.AutoSize = true;
            this.lblNewPact.Location = new System.Drawing.Point(492, 24);
            this.lblNewPact.Name = "lblNewPact";
            this.lblNewPact.Size = new System.Drawing.Size(71, 12);
            this.lblNewPact.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblNewPact.TabIndex = 8;
            this.lblNewPact.Text = "新合同单位:";
            // 
            // lblOldPact
            // 
            this.lblOldPact.AutoSize = true;
            this.lblOldPact.Location = new System.Drawing.Point(245, 24);
            this.lblOldPact.Name = "lblOldPact";
            this.lblOldPact.Size = new System.Drawing.Size(71, 12);
            this.lblOldPact.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblOldPact.TabIndex = 7;
            this.lblOldPact.Text = "原合同单位:";
            // 
            // cmbNewPact
            // 
            this.cmbNewPact.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbNewPact.FormattingEnabled = true;
            this.cmbNewPact.IsEnter2Tab = false;
            this.cmbNewPact.IsFlat = false;
            this.cmbNewPact.IsLike = true;
            this.cmbNewPact.IsListOnly = false;
            this.cmbNewPact.IsPopForm = true;
            this.cmbNewPact.IsShowCustomerList = false;
            this.cmbNewPact.IsShowID = false;
            this.cmbNewPact.Location = new System.Drawing.Point(574, 20);
            this.cmbNewPact.Name = "cmbNewPact";
            this.cmbNewPact.PopForm = null;
            this.cmbNewPact.ShowCustomerList = false;
            this.cmbNewPact.ShowID = false;
            this.cmbNewPact.Size = new System.Drawing.Size(121, 20);
            this.cmbNewPact.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbNewPact.TabIndex = 6;
            this.cmbNewPact.Tag = "";
            this.cmbNewPact.ToolBarUse = false;
            // 
            // ucModifyInpatient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbQuery);
            this.Controls.Add(this.neuGroupBox3);
            this.Name = "ucModifyInpatient";
            this.Size = new System.Drawing.Size(720, 471);
            this.neuGroupBox3.ResumeLayout(false);
            this.neuGroupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        protected FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox3;
        protected FS.HISFC.Components.Common.Controls.ucQueryInpatientNo ucQueryInpatientNo1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbQuery;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtOldPact;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblNewPact;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblOldPact;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbNewPact;
    }
}
