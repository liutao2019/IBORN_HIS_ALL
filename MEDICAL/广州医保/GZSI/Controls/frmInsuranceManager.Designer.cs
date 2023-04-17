namespace GZSI.Controls
{
    partial class frmInsuranceManager
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
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.btnOK = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.txtMemo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtEndCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtBeginCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtPartID = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtRate = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.cmbKind = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbPact = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel7 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.btnBack = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(20, 16);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(59, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "合同单位:";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(60, 250);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "保存";
            this.btnOK.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.txtMemo);
            this.neuPanel1.Controls.Add(this.txtEndCost);
            this.neuPanel1.Controls.Add(this.txtBeginCost);
            this.neuPanel1.Controls.Add(this.txtPartID);
            this.neuPanel1.Controls.Add(this.txtRate);
            this.neuPanel1.Controls.Add(this.cmbKind);
            this.neuPanel1.Controls.Add(this.cmbPact);
            this.neuPanel1.Controls.Add(this.neuLabel7);
            this.neuPanel1.Controls.Add(this.neuLabel6);
            this.neuPanel1.Controls.Add(this.neuLabel2);
            this.neuPanel1.Controls.Add(this.neuLabel1);
            this.neuPanel1.Controls.Add(this.neuLabel5);
            this.neuPanel1.Controls.Add(this.neuLabel3);
            this.neuPanel1.Controls.Add(this.neuLabel4);
            this.neuPanel1.Location = new System.Drawing.Point(12, 12);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(270, 226);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 2;
            // 
            // txtMemo
            // 
            this.txtMemo.IsEnter2Tab = false;
            this.txtMemo.Location = new System.Drawing.Point(85, 176);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(149, 21);
            this.txtMemo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtMemo.TabIndex = 2;
            // 
            // txtEndCost
            // 
            this.txtEndCost.IsEnter2Tab = false;
            this.txtEndCost.Location = new System.Drawing.Point(85, 148);
            this.txtEndCost.Name = "txtEndCost";
            this.txtEndCost.Size = new System.Drawing.Size(149, 21);
            this.txtEndCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtEndCost.TabIndex = 2;
            // 
            // txtBeginCost
            // 
            this.txtBeginCost.IsEnter2Tab = false;
            this.txtBeginCost.Location = new System.Drawing.Point(85, 121);
            this.txtBeginCost.Name = "txtBeginCost";
            this.txtBeginCost.Size = new System.Drawing.Size(149, 21);
            this.txtBeginCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtBeginCost.TabIndex = 2;
            // 
            // txtPartID
            // 
            this.txtPartID.IsEnter2Tab = false;
            this.txtPartID.Location = new System.Drawing.Point(85, 69);
            this.txtPartID.Name = "txtPartID";
            this.txtPartID.Size = new System.Drawing.Size(149, 21);
            this.txtPartID.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtPartID.TabIndex = 2;
            // 
            // txtRate
            // 
            this.txtRate.IsEnter2Tab = false;
            this.txtRate.Location = new System.Drawing.Point(85, 96);
            this.txtRate.Name = "txtRate";
            this.txtRate.Size = new System.Drawing.Size(149, 21);
            this.txtRate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtRate.TabIndex = 2;
            // 
            // cmbKind
            // 
            this.cmbKind.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbKind.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbKind.DropDownHeight = 200;
            this.cmbKind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKind.FormattingEnabled = true;
            this.cmbKind.IntegralHeight = false;
            this.cmbKind.IsEnter2Tab = false;
            this.cmbKind.IsFlat = false;
            this.cmbKind.IsLike = true;
            this.cmbKind.IsListOnly = false;
            this.cmbKind.IsPopForm = true;
            this.cmbKind.IsShowCustomerList = false;
            this.cmbKind.IsShowID = false;
            this.cmbKind.Location = new System.Drawing.Point(85, 40);
            this.cmbKind.Name = "cmbKind";
            this.cmbKind.ShowCustomerList = false;
            this.cmbKind.ShowID = false;
            this.cmbKind.Size = new System.Drawing.Size(149, 20);
            this.cmbKind.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbKind.TabIndex = 1;
            this.cmbKind.Tag = "";
            this.cmbKind.ToolBarUse = false;
            // 
            // cmbPact
            // 
            this.cmbPact.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbPact.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbPact.DropDownHeight = 200;
            this.cmbPact.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPact.FormattingEnabled = true;
            this.cmbPact.IntegralHeight = false;
            this.cmbPact.IsEnter2Tab = false;
            this.cmbPact.IsFlat = false;
            this.cmbPact.IsLike = true;
            this.cmbPact.IsListOnly = false;
            this.cmbPact.IsPopForm = true;
            this.cmbPact.IsShowCustomerList = false;
            this.cmbPact.IsShowID = false;
            this.cmbPact.Location = new System.Drawing.Point(85, 13);
            this.cmbPact.Name = "cmbPact";
            this.cmbPact.ShowCustomerList = false;
            this.cmbPact.ShowID = false;
            this.cmbPact.Size = new System.Drawing.Size(149, 20);
            this.cmbPact.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbPact.TabIndex = 1;
            this.cmbPact.Tag = "";
            this.cmbPact.ToolBarUse = false;
            // 
            // neuLabel7
            // 
            this.neuLabel7.AutoSize = true;
            this.neuLabel7.Location = new System.Drawing.Point(20, 179);
            this.neuLabel7.Name = "neuLabel7";
            this.neuLabel7.Size = new System.Drawing.Size(59, 12);
            this.neuLabel7.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel7.TabIndex = 0;
            this.neuLabel7.Text = "备    注:";
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.Location = new System.Drawing.Point(20, 152);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(59, 12);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 0;
            this.neuLabel6.Text = "区间结束:";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(20, 45);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(59, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 0;
            this.neuLabel2.Text = "类    别:";
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Location = new System.Drawing.Point(20, 124);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(59, 12);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 0;
            this.neuLabel5.Text = "区间开始:";
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(20, 74);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(59, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 0;
            this.neuLabel3.Text = "分段序号:";
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point(20, 99);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(59, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 0;
            this.neuLabel4.Text = "比    例:";
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(157, 250);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnBack.TabIndex = 1;
            this.btnBack.Text = "取消";
            this.btnBack.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // frmInsuranceManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 289);
            this.Controls.Add(this.neuPanel1);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnOK);
            this.Name = "frmInsuranceManager";
            this.Text = "title";
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuButton btnOK;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel7;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuButton btnBack;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtMemo;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtEndCost;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtBeginCost;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtRate;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbKind;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbPact;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtPartID;
    }
}