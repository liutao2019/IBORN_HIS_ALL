﻿namespace FS.HISFC.Components.Nurse.Controls
{
    partial class ucPatientOut
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
            this.cmbZg = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.dtOutDate = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.txtCard = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtIndate = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtSex = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtBalKind = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtDept = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel7 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtBedNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel8 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtTotcost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel9 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtFreePay = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel10 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel11 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel12 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.btnPrint = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnSave = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.SuspendLayout();
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.ForeColor = System.Drawing.Color.Fuchsia;
            this.neuLabel1.Location = new System.Drawing.Point(51, 54);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(65, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "留 观 号：";
            // 
            // txtPatientNo
            // 
            this.txtPatientNo.Location = new System.Drawing.Point(122, 49);
            this.txtPatientNo.Name = "txtPatientNo";
            this.txtPatientNo.ReadOnly = true;
            this.txtPatientNo.Size = new System.Drawing.Size(119, 21);
            this.txtPatientNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtPatientNo.TabIndex = 1;
            // 
            // cmbZg
            // 
            this.cmbZg.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbZg.FormattingEnabled = true;
            this.cmbZg.IsFlat = true;
            this.cmbZg.IsLike = true;
            this.cmbZg.Location = new System.Drawing.Point(122, 349);
            this.cmbZg.Name = "cmbZg";
            this.cmbZg.PopForm = null;
            this.cmbZg.ShowCustomerList = false;
            this.cmbZg.ShowID = false;
            this.cmbZg.Size = new System.Drawing.Size(119, 20);
            this.cmbZg.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbZg.TabIndex = 2;
            this.cmbZg.Tag = "";
            this.cmbZg.ToolBarUse = false;
            // 
            // dtOutDate
            // 
            this.dtOutDate.Location = new System.Drawing.Point(122, 322);
            this.dtOutDate.Name = "dtOutDate";
            this.dtOutDate.Size = new System.Drawing.Size(119, 21);
            this.dtOutDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtOutDate.TabIndex = 3;
            // 
            // txtCard
            // 
            this.txtCard.Location = new System.Drawing.Point(122, 76);
            this.txtCard.Name = "txtCard";
            this.txtCard.ReadOnly = true;
            this.txtCard.Size = new System.Drawing.Size(119, 21);
            this.txtCard.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCard.TabIndex = 5;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.ForeColor = System.Drawing.Color.Fuchsia;
            this.neuLabel2.Location = new System.Drawing.Point(51, 81);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(65, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 4;
            this.neuLabel2.Text = "病 历 号：";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(122, 103);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(119, 21);
            this.txtName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtName.TabIndex = 7;
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.ForeColor = System.Drawing.Color.Fuchsia;
            this.neuLabel3.Location = new System.Drawing.Point(51, 108);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(65, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 6;
            this.neuLabel3.Text = "姓    名：";
            // 
            // txtIndate
            // 
            this.txtIndate.Location = new System.Drawing.Point(122, 130);
            this.txtIndate.Name = "txtIndate";
            this.txtIndate.ReadOnly = true;
            this.txtIndate.Size = new System.Drawing.Size(119, 21);
            this.txtIndate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtIndate.TabIndex = 9;
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.ForeColor = System.Drawing.Color.Fuchsia;
            this.neuLabel4.Location = new System.Drawing.Point(51, 135);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(65, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 8;
            this.neuLabel4.Text = "入院日期：";
            // 
            // txtSex
            // 
            this.txtSex.Location = new System.Drawing.Point(122, 157);
            this.txtSex.Name = "txtSex";
            this.txtSex.ReadOnly = true;
            this.txtSex.Size = new System.Drawing.Size(119, 21);
            this.txtSex.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtSex.TabIndex = 11;
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.ForeColor = System.Drawing.Color.Fuchsia;
            this.neuLabel5.Location = new System.Drawing.Point(51, 162);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(65, 12);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 10;
            this.neuLabel5.Text = "性    别：";
            // 
            // txtBalKind
            // 
            this.txtBalKind.Location = new System.Drawing.Point(122, 184);
            this.txtBalKind.Name = "txtBalKind";
            this.txtBalKind.ReadOnly = true;
            this.txtBalKind.Size = new System.Drawing.Size(119, 21);
            this.txtBalKind.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtBalKind.TabIndex = 13;
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.ForeColor = System.Drawing.Color.Fuchsia;
            this.neuLabel6.Location = new System.Drawing.Point(51, 189);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(65, 12);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 12;
            this.neuLabel6.Text = "结算类别：";
            // 
            // txtDept
            // 
            this.txtDept.Location = new System.Drawing.Point(122, 211);
            this.txtDept.Name = "txtDept";
            this.txtDept.ReadOnly = true;
            this.txtDept.Size = new System.Drawing.Size(119, 21);
            this.txtDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtDept.TabIndex = 15;
            // 
            // neuLabel7
            // 
            this.neuLabel7.AutoSize = true;
            this.neuLabel7.ForeColor = System.Drawing.Color.Fuchsia;
            this.neuLabel7.Location = new System.Drawing.Point(51, 216);
            this.neuLabel7.Name = "neuLabel7";
            this.neuLabel7.Size = new System.Drawing.Size(65, 12);
            this.neuLabel7.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel7.TabIndex = 14;
            this.neuLabel7.Text = "科    室：";
            // 
            // txtBedNo
            // 
            this.txtBedNo.Location = new System.Drawing.Point(122, 238);
            this.txtBedNo.Name = "txtBedNo";
            this.txtBedNo.ReadOnly = true;
            this.txtBedNo.Size = new System.Drawing.Size(119, 21);
            this.txtBedNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtBedNo.TabIndex = 17;
            // 
            // neuLabel8
            // 
            this.neuLabel8.AutoSize = true;
            this.neuLabel8.ForeColor = System.Drawing.Color.Fuchsia;
            this.neuLabel8.Location = new System.Drawing.Point(51, 243);
            this.neuLabel8.Name = "neuLabel8";
            this.neuLabel8.Size = new System.Drawing.Size(65, 12);
            this.neuLabel8.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel8.TabIndex = 16;
            this.neuLabel8.Text = "床    号：";
            // 
            // txtTotcost
            // 
            this.txtTotcost.Location = new System.Drawing.Point(122, 265);
            this.txtTotcost.Name = "txtTotcost";
            this.txtTotcost.ReadOnly = true;
            this.txtTotcost.Size = new System.Drawing.Size(119, 21);
            this.txtTotcost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtTotcost.TabIndex = 19;
            // 
            // neuLabel9
            // 
            this.neuLabel9.AutoSize = true;
            this.neuLabel9.ForeColor = System.Drawing.Color.Fuchsia;
            this.neuLabel9.Location = new System.Drawing.Point(51, 270);
            this.neuLabel9.Name = "neuLabel9";
            this.neuLabel9.Size = new System.Drawing.Size(65, 12);
            this.neuLabel9.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel9.TabIndex = 18;
            this.neuLabel9.Text = "总 费 用：";
            // 
            // txtFreePay
            // 
            this.txtFreePay.Location = new System.Drawing.Point(122, 292);
            this.txtFreePay.Name = "txtFreePay";
            this.txtFreePay.ReadOnly = true;
            this.txtFreePay.Size = new System.Drawing.Size(119, 21);
            this.txtFreePay.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtFreePay.TabIndex = 21;
            // 
            // neuLabel10
            // 
            this.neuLabel10.AutoSize = true;
            this.neuLabel10.ForeColor = System.Drawing.Color.Fuchsia;
            this.neuLabel10.Location = new System.Drawing.Point(51, 297);
            this.neuLabel10.Name = "neuLabel10";
            this.neuLabel10.Size = new System.Drawing.Size(65, 12);
            this.neuLabel10.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel10.TabIndex = 20;
            this.neuLabel10.Text = "余    额：";
            // 
            // neuLabel11
            // 
            this.neuLabel11.AutoSize = true;
            this.neuLabel11.Location = new System.Drawing.Point(51, 324);
            this.neuLabel11.Name = "neuLabel11";
            this.neuLabel11.Size = new System.Drawing.Size(65, 12);
            this.neuLabel11.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel11.TabIndex = 22;
            this.neuLabel11.Text = "出院日期：";
            // 
            // neuLabel12
            // 
            this.neuLabel12.AutoSize = true;
            this.neuLabel12.Location = new System.Drawing.Point(51, 351);
            this.neuLabel12.Name = "neuLabel12";
            this.neuLabel12.Size = new System.Drawing.Size(65, 12);
            this.neuLabel12.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel12.TabIndex = 24;
            this.neuLabel12.Text = "出院情况：";
            // 
            // btnPrint
            // 
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(53, 403);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnPrint.TabIndex = 25;
            this.btnPrint.Text = "出院通知单";
            this.btnPrint.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(166, 403);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnSave.TabIndex = 26;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.button1_Click);
            // 
            // ucPatientOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.neuLabel12);
            this.Controls.Add(this.neuLabel11);
            this.Controls.Add(this.txtFreePay);
            this.Controls.Add(this.neuLabel10);
            this.Controls.Add(this.txtTotcost);
            this.Controls.Add(this.neuLabel9);
            this.Controls.Add(this.txtBedNo);
            this.Controls.Add(this.neuLabel8);
            this.Controls.Add(this.txtDept);
            this.Controls.Add(this.neuLabel7);
            this.Controls.Add(this.txtBalKind);
            this.Controls.Add(this.neuLabel6);
            this.Controls.Add(this.txtSex);
            this.Controls.Add(this.neuLabel5);
            this.Controls.Add(this.txtIndate);
            this.Controls.Add(this.neuLabel4);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.neuLabel3);
            this.Controls.Add(this.txtCard);
            this.Controls.Add(this.neuLabel2);
            this.Controls.Add(this.dtOutDate);
            this.Controls.Add(this.cmbZg);
            this.Controls.Add(this.txtPatientNo);
            this.Controls.Add(this.neuLabel1);
            this.Name = "ucPatientOut";
            this.Size = new System.Drawing.Size(341, 537);
            this.Load += new System.EventHandler(this.ucPatientOut_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtPatientNo;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbZg;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtOutDate;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtCard;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtName;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtIndate;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtSex;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtBalKind;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtDept;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel7;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtBedNo;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel8;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtTotcost;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel9;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtFreePay;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel10;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel11;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel12;
        private FS.FrameWork.WinForms.Controls.NeuButton btnPrint;
        private FS.FrameWork.WinForms.Controls.NeuButton btnSave;
    }
}
