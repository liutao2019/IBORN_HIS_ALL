namespace FS.HISFC.Components.Common.Forms
{
    partial class frmChoosePrinter
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
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.cmbPrinterList = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.btOK = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.cmbPrinterList);
            this.neuPanel1.Controls.Add(this.btOK);
            this.neuPanel1.Controls.Add(this.neuLabel1);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(309, 90);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // cmbPrinterList
            // 
            this.cmbPrinterList.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbPrinterList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbPrinterList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrinterList.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbPrinterList.FormattingEnabled = true;
            this.cmbPrinterList.IsEnter2Tab = false;
            this.cmbPrinterList.IsFlat = false;
            this.cmbPrinterList.IsLike = true;
            this.cmbPrinterList.IsListOnly = true;
            this.cmbPrinterList.IsPopForm = true;
            this.cmbPrinterList.IsShowCustomerList = false;
            this.cmbPrinterList.IsShowID = false;
            this.cmbPrinterList.IsShowIDAndName = false;
            this.cmbPrinterList.Location = new System.Drawing.Point(91, 17);
            this.cmbPrinterList.Name = "cmbPrinterList";
            this.cmbPrinterList.ShowCustomerList = false;
            this.cmbPrinterList.ShowID = false;
            this.cmbPrinterList.Size = new System.Drawing.Size(206, 22);
            this.cmbPrinterList.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbPrinterList.TabIndex = 8;
            this.cmbPrinterList.Tag = "";
            this.cmbPrinterList.ToolBarUse = false;
            // 
            // btOK
            // 
            this.btOK.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btOK.Location = new System.Drawing.Point(187, 53);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(110, 25);
            this.btOK.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btOK.TabIndex = 7;
            this.btOK.Text = "确定";
            this.btOK.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.BackColor = System.Drawing.Color.Transparent;
            this.neuLabel1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel1.Location = new System.Drawing.Point(17, 22);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(63, 14);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 1;
            this.neuLabel1.Text = "打印机：";
            // 
            // frmChoosePrinter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 90);
            this.Controls.Add(this.neuPanel1);
            this.KeyPreview = true;
            this.Name = "frmChoosePrinter";
            this.Text = "选择打印机";
            this.TransparencyKey = System.Drawing.Color.White;
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuButton btOK;
        public FS.FrameWork.WinForms.Controls.NeuComboBox cmbPrinterList;

    }
}