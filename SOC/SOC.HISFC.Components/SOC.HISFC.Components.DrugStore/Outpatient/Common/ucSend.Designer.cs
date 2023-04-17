namespace FS.SOC.HISFC.Components.DrugStore.Outpatient.Common
{
    partial class ucSend
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
            this.nlbWorkLoadInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbTermialInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.ncbPauseRefresh = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.cmbSendEmployee = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.nlbCurSendOper = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel1.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            this.ngbAdd.SuspendLayout();
            this.ngbRecipeDetail.SuspendLayout();
            this.neuGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucDrugTree1
            // 
            this.ucDrugTree1.Size = new System.Drawing.Size(233, 592);
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.neuGroupBox1);
            this.neuPanel1.Controls.SetChildIndex(this.neuGroupBox1, 0);
            this.neuPanel1.Controls.SetChildIndex(this.ucDrugTree1, 0);
            // 
            // ngbAdd
            // 
            this.ngbAdd.Controls.Add(this.nlbCurSendOper);
            this.ngbAdd.Controls.Add(this.cmbSendEmployee);
            this.ngbAdd.Controls.Add(this.nlbWorkLoadInfo);
            this.ngbAdd.Controls.Add(this.nlbTermialInfo);
            // 
            // nlbWorkLoadInfo
            // 
            this.nlbWorkLoadInfo.AutoSize = true;
            this.nlbWorkLoadInfo.ForeColor = System.Drawing.Color.Blue;
            this.nlbWorkLoadInfo.Location = new System.Drawing.Point(176, 13);
            this.nlbWorkLoadInfo.Name = "nlbWorkLoadInfo";
            this.nlbWorkLoadInfo.Size = new System.Drawing.Size(65, 12);
            this.nlbWorkLoadInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbWorkLoadInfo.TabIndex = 10;
            this.nlbWorkLoadInfo.Text = "工作量情况";
            // 
            // nlbTermialInfo
            // 
            this.nlbTermialInfo.AutoSize = true;
            this.nlbTermialInfo.ForeColor = System.Drawing.Color.Blue;
            this.nlbTermialInfo.Location = new System.Drawing.Point(176, 32);
            this.nlbTermialInfo.Name = "nlbTermialInfo";
            this.nlbTermialInfo.Size = new System.Drawing.Size(53, 12);
            this.nlbTermialInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbTermialInfo.TabIndex = 9;
            this.nlbTermialInfo.Text = "终端信息";
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.ncbPauseRefresh);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 592);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(233, 50);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 3;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "程序设置";
            // 
            // ncbPauseRefresh
            // 
            this.ncbPauseRefresh.AutoSize = true;
            this.ncbPauseRefresh.Location = new System.Drawing.Point(68, 20);
            this.ncbPauseRefresh.Name = "ncbPauseRefresh";
            this.ncbPauseRefresh.Size = new System.Drawing.Size(96, 16);
            this.ncbPauseRefresh.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbPauseRefresh.TabIndex = 0;
            this.ncbPauseRefresh.Text = "暂停自动刷新";
            this.ncbPauseRefresh.UseVisualStyleBackColor = true;
            // 
            // cmbSendEmployee
            // 
            this.cmbSendEmployee.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbSendEmployee.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbSendEmployee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSendEmployee.FormattingEnabled = true;
            this.cmbSendEmployee.IsEnter2Tab = false;
            this.cmbSendEmployee.IsFlat = false;
            this.cmbSendEmployee.IsLike = true;
            this.cmbSendEmployee.IsListOnly = false;
            this.cmbSendEmployee.IsPopForm = true;
            this.cmbSendEmployee.IsShowCustomerList = false;
            this.cmbSendEmployee.IsShowID = false;
            this.cmbSendEmployee.IsShowIDAndName = false;
            this.cmbSendEmployee.Location = new System.Drawing.Point(72, 21);
            this.cmbSendEmployee.Name = "cmbSendEmployee";
            this.cmbSendEmployee.ShowCustomerList = false;
            this.cmbSendEmployee.ShowID = false;
            this.cmbSendEmployee.Size = new System.Drawing.Size(94, 20);
            this.cmbSendEmployee.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbSendEmployee.TabIndex = 17;
            this.cmbSendEmployee.Tag = "";
            this.cmbSendEmployee.ToolBarUse = false;
            // 
            // nlbCurSendOper
            // 
            this.nlbCurSendOper.AutoSize = true;
            this.nlbCurSendOper.ForeColor = System.Drawing.Color.Blue;
            this.nlbCurSendOper.Location = new System.Drawing.Point(6, 24);
            this.nlbCurSendOper.Name = "nlbCurSendOper";
            this.nlbCurSendOper.Size = new System.Drawing.Size(65, 12);
            this.nlbCurSendOper.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbCurSendOper.TabIndex = 18;
            this.nlbCurSendOper.Text = "当前员工：";
            // 
            // ucSend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucSend";
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel2.ResumeLayout(false);
            this.ngbAdd.ResumeLayout(false);
            this.ngbAdd.PerformLayout();
            this.ngbRecipeDetail.ResumeLayout(false);
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuLabel nlbWorkLoadInfo;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbTermialInfo;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbPauseRefresh;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbCurSendOper;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbSendEmployee;
    }
}
