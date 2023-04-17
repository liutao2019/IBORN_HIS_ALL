namespace FS.SOC.HISFC.Components.Pharmacy.Maintenance
{
    partial class ucPharmacyFunctionProperty
    {
        /// <summary> 
        /// 必需的设计器变量。

        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。

        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose( bool disposing )
        {
            if( disposing && ( components != null ) )
            {
                components.Dispose( );
            }
            base.Dispose( disposing );
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。

        /// </summary>
        private void InitializeComponent( )
        {
            this.components = new System.ComponentModel.Container();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.txtSortId = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel8 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.btnCancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnOk = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuLabel7 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.chkIsValid = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.txtMark = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtWBCode = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtSpellCode = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtCode = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.cmbparent = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.txtSortId);
            this.neuGroupBox1.Controls.Add(this.neuLabel8);
            this.neuGroupBox1.Controls.Add(this.btnCancel);
            this.neuGroupBox1.Controls.Add(this.btnOk);
            this.neuGroupBox1.Controls.Add(this.neuLabel7);
            this.neuGroupBox1.Controls.Add(this.neuLabel6);
            this.neuGroupBox1.Controls.Add(this.neuLabel4);
            this.neuGroupBox1.Controls.Add(this.neuLabel3);
            this.neuGroupBox1.Controls.Add(this.neuLabel2);
            this.neuGroupBox1.Controls.Add(this.neuLabel1);
            this.neuGroupBox1.Controls.Add(this.chkIsValid);
            this.neuGroupBox1.Controls.Add(this.txtMark);
            this.neuGroupBox1.Controls.Add(this.txtWBCode);
            this.neuGroupBox1.Controls.Add(this.txtSpellCode);
            this.neuGroupBox1.Controls.Add(this.txtName);
            this.neuGroupBox1.Controls.Add(this.txtCode);
            this.neuGroupBox1.Controls.Add(this.cmbparent);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(264, 337);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 0;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "药理作用维护";
            // 
            // txtSortId
            // 
            this.txtSortId.IsEnter2Tab = false;
            this.txtSortId.Location = new System.Drawing.Point(60, 178);
            this.txtSortId.MaxLength = 4;
            this.txtSortId.Name = "txtSortId";
            this.txtSortId.Size = new System.Drawing.Size(189, 21);
            this.txtSortId.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtSortId.TabIndex = 17;
            // 
            // neuLabel8
            // 
            this.neuLabel8.AutoSize = true;
            this.neuLabel8.Location = new System.Drawing.Point(15, 179);
            this.neuLabel8.Name = "neuLabel8";
            this.neuLabel8.Size = new System.Drawing.Size(41, 12);
            this.neuLabel8.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel8.TabIndex = 16;
            this.neuLabel8.Text = "顺序号";
            // 
            // btnCancel
            // 
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(174, 300);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOk.Location = new System.Drawing.Point(93, 300);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnOk.TabIndex = 14;
            this.btnOk.Text = "确定(&O)";
            this.btnOk.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // neuLabel7
            // 
            this.neuLabel7.AutoSize = true;
            this.neuLabel7.Location = new System.Drawing.Point(15, 226);
            this.neuLabel7.Name = "neuLabel7";
            this.neuLabel7.Size = new System.Drawing.Size(41, 12);
            this.neuLabel7.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel7.TabIndex = 13;
            this.neuLabel7.Text = "备  注";
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.Location = new System.Drawing.Point(15, 67);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(41, 12);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 12;
            this.neuLabel6.Text = "编  码";
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point(15, 151);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(41, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 10;
            this.neuLabel4.Text = "五笔码";
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(15, 123);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(41, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 9;
            this.neuLabel3.Text = "拼音码";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(15, 95);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(41, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 8;
            this.neuLabel2.Text = "名  称";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(15, 39);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(41, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 7;
            this.neuLabel1.Text = "父  级";
            // 
            // chkIsValid
            // 
            this.chkIsValid.AutoSize = true;
            this.chkIsValid.Location = new System.Drawing.Point(60, 205);
            this.chkIsValid.Name = "chkIsValid";
            this.chkIsValid.Size = new System.Drawing.Size(48, 16);
            this.chkIsValid.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chkIsValid.TabIndex = 5;
            this.chkIsValid.Text = "有效";
            this.chkIsValid.UseVisualStyleBackColor = true;
            // 
            // txtMark
            // 
            this.txtMark.IsEnter2Tab = false;
            this.txtMark.Location = new System.Drawing.Point(60, 227);
            this.txtMark.Multiline = true;
            this.txtMark.Name = "txtMark";
            this.txtMark.Size = new System.Drawing.Size(189, 67);
            this.txtMark.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtMark.TabIndex = 4;
            // 
            // txtWBCode
            // 
            this.txtWBCode.IsEnter2Tab = false;
            this.txtWBCode.Location = new System.Drawing.Point(60, 149);
            this.txtWBCode.Name = "txtWBCode";
            this.txtWBCode.Size = new System.Drawing.Size(189, 21);
            this.txtWBCode.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtWBCode.TabIndex = 4;
            // 
            // txtSpellCode
            // 
            this.txtSpellCode.IsEnter2Tab = false;
            this.txtSpellCode.Location = new System.Drawing.Point(60, 120);
            this.txtSpellCode.Name = "txtSpellCode";
            this.txtSpellCode.Size = new System.Drawing.Size(189, 21);
            this.txtSpellCode.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtSpellCode.TabIndex = 3;
            // 
            // txtName
            // 
            this.txtName.IsEnter2Tab = false;
            this.txtName.Location = new System.Drawing.Point(60, 91);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(189, 21);
            this.txtName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtName.TabIndex = 2;
            this.txtName.Leave += new System.EventHandler(this.txtName_Leave);
            // 
            // txtCode
            // 
            this.txtCode.IsEnter2Tab = false;
            this.txtCode.Location = new System.Drawing.Point(60, 62);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(189, 21);
            this.txtCode.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCode.TabIndex = 1;
            // 
            // cmbparent
            // 
            this.cmbparent.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbparent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbparent.FormattingEnabled = true;
            this.cmbparent.IsEnter2Tab = false;
            this.cmbparent.IsFlat = false;
            this.cmbparent.IsLike = true;
            this.cmbparent.IsListOnly = false;
            this.cmbparent.IsPopForm = true;
            this.cmbparent.IsShowCustomerList = false;
            this.cmbparent.IsShowID = false;
            this.cmbparent.Location = new System.Drawing.Point(60, 34);
            this.cmbparent.Name = "cmbparent";
            this.cmbparent.PopForm = null;
            this.cmbparent.ShowCustomerList = false;
            this.cmbparent.ShowID = false;
            this.cmbparent.Size = new System.Drawing.Size(189, 20);
            this.cmbparent.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbparent.TabIndex = 0;
            this.cmbparent.Tag = "";
            this.cmbparent.ToolBarUse = false;
            // 
            // ucPharmacyFunctionProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuGroupBox1);
            this.Name = "ucPharmacyFunctionProperty";
            this.Size = new System.Drawing.Size(264, 337);
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox chkIsValid;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtWBCode;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtSpellCode;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtName;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtCode;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbparent;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel7;
        private FS.FrameWork.WinForms.Controls.NeuButton btnCancel;
        private FS.FrameWork.WinForms.Controls.NeuButton btnOk;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtMark;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtSortId;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel8;
    }
}
