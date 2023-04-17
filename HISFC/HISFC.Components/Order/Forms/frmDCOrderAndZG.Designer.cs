namespace FS.HISFC.Components.Order.Forms
{
    partial class frmDCOrderAndZG
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
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbDC = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.dateTimeBox1 = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.btnOK = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnCancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.cmbZG = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbDiag = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cbxHealthCareType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.treamtype = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.SuspendLayout();
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(32, 33);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(41, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "时间：";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(32, 72);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(41, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 1;
            this.neuLabel2.Text = "原因：";
            // 
            // cmbDC
            // 
            this.cmbDC.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbDC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbDC.FormattingEnabled = true;
            this.cmbDC.IsEnter2Tab = false;
            this.cmbDC.IsFlat = false;
            this.cmbDC.IsLike = true;
            this.cmbDC.IsListOnly = false;
            this.cmbDC.IsPopForm = true;
            this.cmbDC.IsShowCustomerList = false;
            this.cmbDC.IsShowID = false;
            this.cmbDC.IsShowIDAndName = false;
            this.cmbDC.Location = new System.Drawing.Point(79, 69);
            this.cmbDC.Name = "cmbDC";
            this.cmbDC.ShowCustomerList = false;
            this.cmbDC.ShowID = false;
            this.cmbDC.Size = new System.Drawing.Size(160, 20);
            this.cmbDC.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbDC.TabIndex = 2;
            this.cmbDC.Tag = "";
            this.cmbDC.ToolBarUse = false;
            // 
            // dateTimeBox1
            // 
            this.dateTimeBox1.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dateTimeBox1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeBox1.IsEnter2Tab = false;
            this.dateTimeBox1.Location = new System.Drawing.Point(79, 29);
            this.dateTimeBox1.Name = "dateTimeBox1";
            this.dateTimeBox1.Size = new System.Drawing.Size(160, 21);
            this.dateTimeBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dateTimeBox1.TabIndex = 3;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(63, 248);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定(&O)";
            this.btnOK.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(164, 248);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cmbZG
            // 
            this.cmbZG.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbZG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbZG.FormattingEnabled = true;
            this.cmbZG.IsEnter2Tab = false;
            this.cmbZG.IsFlat = false;
            this.cmbZG.IsLike = true;
            this.cmbZG.IsListOnly = false;
            this.cmbZG.IsPopForm = true;
            this.cmbZG.IsShowCustomerList = false;
            this.cmbZG.IsShowID = false;
            this.cmbZG.IsShowIDAndName = false;
            this.cmbZG.Location = new System.Drawing.Point(79, 108);
            this.cmbZG.Name = "cmbZG";
            this.cmbZG.ShowCustomerList = false;
            this.cmbZG.ShowID = false;
            this.cmbZG.Size = new System.Drawing.Size(160, 20);
            this.cmbZG.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbZG.TabIndex = 2;
            this.cmbZG.Tag = "";
            this.cmbZG.ToolBarUse = false;
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(32, 111);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(41, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 1;
            this.neuLabel3.Text = "转归：";
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point(8, 150);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(65, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 6;
            this.neuLabel4.Text = "出院诊断：";
            // 
            // cmbDiag
            // 
            this.cmbDiag.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbDiag.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbDiag.FormattingEnabled = true;
            this.cmbDiag.IsEnter2Tab = false;
            this.cmbDiag.IsFlat = false;
            this.cmbDiag.IsLike = true;
            this.cmbDiag.IsListOnly = false;
            this.cmbDiag.IsPopForm = true;
            this.cmbDiag.IsShowCustomerList = false;
            this.cmbDiag.IsShowID = false;
            this.cmbDiag.IsShowIDAndName = false;
            this.cmbDiag.Location = new System.Drawing.Point(79, 147);
            this.cmbDiag.Name = "cmbDiag";
            this.cmbDiag.ShowCustomerList = false;
            this.cmbDiag.ShowID = false;
            this.cmbDiag.Size = new System.Drawing.Size(160, 20);
            this.cmbDiag.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbDiag.TabIndex = 7;
            this.cmbDiag.Tag = "";
            this.cmbDiag.ToolBarUse = false;
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Location = new System.Drawing.Point(8, 187);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(65, 12);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 8;
            this.neuLabel5.Text = "待遇类型：";
            // 
            // cbxHealthCareType
            // 
            this.cbxHealthCareType.ArrowBackColor = System.Drawing.Color.Silver;
            this.cbxHealthCareType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cbxHealthCareType.FormattingEnabled = true;
            this.cbxHealthCareType.IsEnter2Tab = false;
            this.cbxHealthCareType.IsFlat = false;
            this.cbxHealthCareType.IsLike = true;
            this.cbxHealthCareType.IsListOnly = false;
            this.cbxHealthCareType.IsPopForm = true;
            this.cbxHealthCareType.IsShowCustomerList = false;
            this.cbxHealthCareType.IsShowID = false;
            this.cbxHealthCareType.IsShowIDAndName = false;
            this.cbxHealthCareType.Location = new System.Drawing.Point(79, 187);
            this.cbxHealthCareType.Name = "cbxHealthCareType";
            this.cbxHealthCareType.ShowCustomerList = false;
            this.cbxHealthCareType.ShowID = false;
            this.cbxHealthCareType.Size = new System.Drawing.Size(160, 20);
            this.cbxHealthCareType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cbxHealthCareType.TabIndex = 9;
            this.cbxHealthCareType.Tag = "";
            this.cbxHealthCareType.ToolBarUse = false;
            // 
            // treamtype
            // 
            this.treamtype.AutoSize = true;
            this.treamtype.ForeColor = System.Drawing.Color.Red;
            this.treamtype.Location = new System.Drawing.Point(12, 219);
            this.treamtype.Name = "treamtype";
            this.treamtype.Size = new System.Drawing.Size(65, 12);
            this.treamtype.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.treamtype.TabIndex = 10;
            this.treamtype.Text = "待遇类型：";
            // 
            // frmDCOrderAndZG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(281, 286);
            this.Controls.Add(this.treamtype);
            this.Controls.Add(this.cbxHealthCareType);
            this.Controls.Add(this.neuLabel5);
            this.Controls.Add(this.cmbDiag);
            this.Controls.Add(this.neuLabel4);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.dateTimeBox1);
            this.Controls.Add(this.cmbZG);
            this.Controls.Add(this.cmbDC);
            this.Controls.Add(this.neuLabel3);
            this.Controls.Add(this.neuLabel2);
            this.Controls.Add(this.neuLabel1);
            this.Name = "frmDCOrderAndZG";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "停止／取消医嘱";
            this.Load += new System.EventHandler(this.frmDCOrder_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDC;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dateTimeBox1;
        private FS.FrameWork.WinForms.Controls.NeuButton btnOK;
        private FS.FrameWork.WinForms.Controls.NeuButton btnCancel;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbZG;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDiag;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cbxHealthCareType;
        private FS.FrameWork.WinForms.Controls.NeuLabel treamtype;
    }
}