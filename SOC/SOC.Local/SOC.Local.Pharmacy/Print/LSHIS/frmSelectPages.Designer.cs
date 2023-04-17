namespace FS.SOC.Local.Pharmacy.Print.LSHIS
{
    partial class frmSelectPages
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
            this.lblTotPageNum = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.rbtnPrintAll = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.rbtnPageRange = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.btnPrint = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnCancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.txtFromPage = new FS.FrameWork.WinForms.Controls.NeuNumericTextBox();
            this.txtToPage = new FS.FrameWork.WinForms.Controls.NeuNumericTextBox();
            this.SuspendLayout();
            // 
            // lblTotPageNum
            // 
            this.lblTotPageNum.AutoSize = true;
            this.lblTotPageNum.Location = new System.Drawing.Point(12, 26);
            this.lblTotPageNum.Name = "lblTotPageNum";
            this.lblTotPageNum.Size = new System.Drawing.Size(47, 12);
            this.lblTotPageNum.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblTotPageNum.TabIndex = 0;
            this.lblTotPageNum.Text = "总共N页";
            // 
            // rbtnPrintAll
            // 
            this.rbtnPrintAll.AutoSize = true;
            this.rbtnPrintAll.Location = new System.Drawing.Point(12, 56);
            this.rbtnPrintAll.Name = "rbtnPrintAll";
            this.rbtnPrintAll.Size = new System.Drawing.Size(71, 16);
            this.rbtnPrintAll.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rbtnPrintAll.TabIndex = 1;
            this.rbtnPrintAll.TabStop = true;
            this.rbtnPrintAll.Text = "打印全部";
            this.rbtnPrintAll.UseVisualStyleBackColor = true;
            // 
            // rbtnPageRange
            // 
            this.rbtnPageRange.AutoSize = true;
            this.rbtnPageRange.Location = new System.Drawing.Point(12, 92);
            this.rbtnPageRange.Name = "rbtnPageRange";
            this.rbtnPageRange.Size = new System.Drawing.Size(59, 16);
            this.rbtnPageRange.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rbtnPageRange.TabIndex = 2;
            this.rbtnPageRange.TabStop = true;
            this.rbtnPageRange.Text = "打印第";
            this.rbtnPageRange.UseVisualStyleBackColor = true;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(112, 94);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(17, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 4;
            this.neuLabel1.Text = "至";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(179, 94);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(17, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 6;
            this.neuLabel2.Text = "页";
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(54, 129);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnPrint.TabIndex = 7;
            this.btnPrint.Text = "打印";
            this.btnPrint.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(150, 129);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "取消";
            this.btnCancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtFromPage
            // 
            this.txtFromPage.AllowNegative = false;
            this.txtFromPage.IsAutoRemoveDecimalZero = false;
            this.txtFromPage.IsEnter2Tab = false;
            this.txtFromPage.Location = new System.Drawing.Point(73, 87);
            this.txtFromPage.Name = "txtFromPage";
            this.txtFromPage.NumericPrecision = 100;
            this.txtFromPage.NumericScaleOnFocus = 0;
            this.txtFromPage.NumericScaleOnLostFocus = 0;
            this.txtFromPage.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtFromPage.SetRange = new System.Drawing.Size(-1, -1);
            this.txtFromPage.Size = new System.Drawing.Size(33, 21);
            this.txtFromPage.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtFromPage.TabIndex = 9;
            this.txtFromPage.Text = "0";
            this.txtFromPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtFromPage.UseGroupSeperator = true;
            this.txtFromPage.ZeroIsValid = false;
            // 
            // txtToPage
            // 
            this.txtToPage.AllowNegative = false;
            this.txtToPage.IsAutoRemoveDecimalZero = false;
            this.txtToPage.IsEnter2Tab = false;
            this.txtToPage.Location = new System.Drawing.Point(135, 85);
            this.txtToPage.Name = "txtToPage";
            this.txtToPage.NumericPrecision = 100;
            this.txtToPage.NumericScaleOnFocus = 0;
            this.txtToPage.NumericScaleOnLostFocus = 0;
            this.txtToPage.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtToPage.SetRange = new System.Drawing.Size(-1, -1);
            this.txtToPage.Size = new System.Drawing.Size(33, 21);
            this.txtToPage.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtToPage.TabIndex = 10;
            this.txtToPage.Text = "0";
            this.txtToPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtToPage.UseGroupSeperator = true;
            this.txtToPage.ZeroIsValid = false;
            // 
            // frmSelectPages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 169);
            this.Controls.Add(this.txtToPage);
            this.Controls.Add(this.txtFromPage);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.neuLabel2);
            this.Controls.Add(this.neuLabel1);
            this.Controls.Add(this.rbtnPageRange);
            this.Controls.Add(this.rbtnPrintAll);
            this.Controls.Add(this.lblTotPageNum);
            this.Name = "frmSelectPages";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "打印选项";
            this.Load += new System.EventHandler(this.frmSelectPages_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuLabel lblTotPageNum;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rbtnPrintAll;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rbtnPageRange;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuButton btnPrint;
        private FS.FrameWork.WinForms.Controls.NeuButton btnCancel;
        private FS.FrameWork.WinForms.Controls.NeuNumericTextBox txtFromPage;
        private FS.FrameWork.WinForms.Controls.NeuNumericTextBox txtToPage;
    }
}