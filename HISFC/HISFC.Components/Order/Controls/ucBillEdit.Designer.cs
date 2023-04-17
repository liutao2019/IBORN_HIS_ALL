﻿namespace FS.HISFC.Components.Order.Controls
{
    partial class ucBillEdit
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
            this.lblItemBill = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.chkItemBill = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.btnCancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnOK = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.cmbStyle = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtMemo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtExecBillName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.SuspendLayout();
            // 
            // lblItemBill
            // 
            this.lblItemBill.AutoSize = true;
            this.lblItemBill.Location = new System.Drawing.Point(24, 75);
            this.lblItemBill.Name = "lblItemBill";
            this.lblItemBill.Size = new System.Drawing.Size(65, 12);
            this.lblItemBill.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblItemBill.TabIndex = 19;
            this.lblItemBill.Text = "项目执行单";
            // 
            // chkItemBill
            // 
            this.chkItemBill.AutoSize = true;
            this.chkItemBill.Location = new System.Drawing.Point(95, 75);
            this.chkItemBill.Name = "chkItemBill";
            this.chkItemBill.Size = new System.Drawing.Size(15, 14);
            this.chkItemBill.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chkItemBill.TabIndex = 18;
            this.chkItemBill.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(175, 115);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(61, 23);
            this.btnCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "取　消";
            this.btnCancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(95, 115);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(60, 23);
            this.btnOK.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "确　定";
            this.btnOK.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // cmbStyle
            // 
            this.cmbStyle.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbStyle.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbStyle.FormattingEnabled = true;
            this.cmbStyle.IsEnter2Tab = false;
            this.cmbStyle.IsFlat = false;
            this.cmbStyle.IsLike = true;
            this.cmbStyle.IsListOnly = false;
            this.cmbStyle.IsPopForm = true;
            this.cmbStyle.IsShowCustomerList = false;
            this.cmbStyle.IsShowID = false;
            this.cmbStyle.Items.AddRange(new object[] {
            "病人分组-无护理分组",
            "病人分组-按护理分组",
            "项目分组"});
            this.cmbStyle.Location = new System.Drawing.Point(175, 72);
            this.cmbStyle.Name = "cmbStyle";
            this.cmbStyle.PopForm = null;
            this.cmbStyle.ShowCustomerList = false;
            this.cmbStyle.ShowID = false;
            this.cmbStyle.Size = new System.Drawing.Size(61, 22);
            this.cmbStyle.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbStyle.TabIndex = 15;
            this.cmbStyle.Tag = "";
            this.cmbStyle.ToolBarUse = false;
            this.cmbStyle.Visible = false;
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(24, 75);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(65, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 14;
            this.neuLabel3.Text = "类      别";
            this.neuLabel3.Visible = false;
            // 
            // txtMemo
            // 
            this.txtMemo.IsEnter2Tab = false;
            this.txtMemo.Location = new System.Drawing.Point(95, 44);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(141, 21);
            this.txtMemo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtMemo.TabIndex = 13;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(24, 47);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(65, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 12;
            this.neuLabel2.Text = "备      注";
            // 
            // txtExecBillName
            // 
            this.txtExecBillName.IsEnter2Tab = false;
            this.txtExecBillName.Location = new System.Drawing.Point(95, 16);
            this.txtExecBillName.Name = "txtExecBillName";
            this.txtExecBillName.Size = new System.Drawing.Size(141, 21);
            this.txtExecBillName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtExecBillName.TabIndex = 11;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(24, 19);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(65, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 10;
            this.neuLabel1.Text = "执行单名称";
            // 
            // ucBillEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblItemBill);
            this.Controls.Add(this.chkItemBill);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cmbStyle);
            this.Controls.Add(this.neuLabel3);
            this.Controls.Add(this.txtMemo);
            this.Controls.Add(this.neuLabel2);
            this.Controls.Add(this.txtExecBillName);
            this.Controls.Add(this.neuLabel1);
            this.Name = "ucBillEdit";
            this.Size = new System.Drawing.Size(262, 159);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuLabel lblItemBill;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox chkItemBill;
        private FS.FrameWork.WinForms.Controls.NeuButton btnCancel;
        private FS.FrameWork.WinForms.Controls.NeuButton btnOK;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbStyle;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtMemo;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtExecBillName;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
    }
}
