﻿namespace FS.HISFC.Components.Nurse.Controls
{
    partial class ucPatientShiftOut
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
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtPatientNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.cmbNewDept = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.txtName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtSex = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbBedNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtOldDept = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtNote = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel7 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label8 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.btnSave = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.SuspendLayout();
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(24, 27);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(65, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "住 院 号：";
            // 
            // txtPatientNo
            // 
            this.txtPatientNo.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtPatientNo.Location = new System.Drawing.Point(89, 24);
            this.txtPatientNo.Name = "txtPatientNo";
            this.txtPatientNo.ReadOnly = true;
            this.txtPatientNo.Size = new System.Drawing.Size(129, 21);
            this.txtPatientNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtPatientNo.TabIndex = 1;
            // 
            // cmbNewDept
            // 
            this.cmbNewDept.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbNewDept.FormattingEnabled = true;
            this.cmbNewDept.IsFlat = true;
            this.cmbNewDept.IsLike = true;
            this.cmbNewDept.Location = new System.Drawing.Point(89, 159);
            this.cmbNewDept.Name = "cmbNewDept";
            this.cmbNewDept.PopForm = null;
            this.cmbNewDept.ShowCustomerList = false;
            this.cmbNewDept.ShowID = false;
            this.cmbNewDept.Size = new System.Drawing.Size(129, 20);
            this.cmbNewDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbNewDept.TabIndex = 2;
            this.cmbNewDept.Tag = "";
            this.cmbNewDept.ToolBarUse = false;
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtName.Location = new System.Drawing.Point(89, 51);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(129, 21);
            this.txtName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtName.TabIndex = 4;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(24, 54);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(65, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 3;
            this.neuLabel2.Text = "姓    名：";
            // 
            // txtSex
            // 
            this.txtSex.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtSex.Location = new System.Drawing.Point(89, 78);
            this.txtSex.Name = "txtSex";
            this.txtSex.ReadOnly = true;
            this.txtSex.Size = new System.Drawing.Size(129, 21);
            this.txtSex.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtSex.TabIndex = 6;
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(24, 81);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(65, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 5;
            this.neuLabel3.Text = "性    别：";
            // 
            // cmbBedNo
            // 
            this.cmbBedNo.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cmbBedNo.Location = new System.Drawing.Point(89, 105);
            this.cmbBedNo.Name = "cmbBedNo";
            this.cmbBedNo.ReadOnly = true;
            this.cmbBedNo.Size = new System.Drawing.Size(129, 21);
            this.cmbBedNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbBedNo.TabIndex = 8;
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point(24, 108);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(65, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 7;
            this.neuLabel4.Text = "病 床 号：";
            // 
            // txtOldDept
            // 
            this.txtOldDept.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtOldDept.Location = new System.Drawing.Point(89, 132);
            this.txtOldDept.Name = "txtOldDept";
            this.txtOldDept.ReadOnly = true;
            this.txtOldDept.Size = new System.Drawing.Size(129, 21);
            this.txtOldDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtOldDept.TabIndex = 10;
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Location = new System.Drawing.Point(24, 135);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(65, 12);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 9;
            this.neuLabel5.Text = "原 科 室：";
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.Location = new System.Drawing.Point(24, 162);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(65, 12);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 11;
            this.neuLabel6.Text = "目标科室：";
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(89, 186);
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(129, 21);
            this.txtNote.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtNote.TabIndex = 14;
            // 
            // neuLabel7
            // 
            this.neuLabel7.AutoSize = true;
            this.neuLabel7.Location = new System.Drawing.Point(24, 189);
            this.neuLabel7.Name = "neuLabel7";
            this.neuLabel7.Size = new System.Drawing.Size(65, 12);
            this.neuLabel7.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel7.TabIndex = 13;
            this.neuLabel7.Text = "备    注：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Blue;
            this.label8.Location = new System.Drawing.Point(30, 229);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 12);
            this.label8.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label8.TabIndex = 15;
            this.label8.Text = "未生效的转科申请";
            // 
            // btnSave
            // 
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(143, 224);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ucPatientShiftOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtNote);
            this.Controls.Add(this.neuLabel7);
            this.Controls.Add(this.neuLabel6);
            this.Controls.Add(this.txtOldDept);
            this.Controls.Add(this.neuLabel5);
            this.Controls.Add(this.cmbBedNo);
            this.Controls.Add(this.neuLabel4);
            this.Controls.Add(this.txtSex);
            this.Controls.Add(this.neuLabel3);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.neuLabel2);
            this.Controls.Add(this.cmbNewDept);
            this.Controls.Add(this.txtPatientNo);
            this.Controls.Add(this.neuLabel1);
            this.Name = "ucPatientShiftOut";
            this.Size = new System.Drawing.Size(255, 313);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtPatientNo;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbNewDept;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtName;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtSex;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuTextBox cmbBedNo;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtOldDept;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtNote;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel7;
        private FS.FrameWork.WinForms.Controls.NeuLabel label8;
        private FS.FrameWork.WinForms.Controls.NeuButton btnSave;
    }
}
