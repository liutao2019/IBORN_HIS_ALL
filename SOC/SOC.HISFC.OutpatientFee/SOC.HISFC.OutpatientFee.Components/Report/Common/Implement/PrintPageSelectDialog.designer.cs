namespace FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Implement
{
    partial class PrintPageSelectDialog
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nrbtPrintRange = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.nrbtPintAll = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.nbtOK = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.nbtCancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.nntbFromPage = new FS.FrameWork.WinForms.Controls.NeuNumericTextBox();
            this.nntbToPage = new FS.FrameWork.WinForms.Controls.NeuNumericTextBox();
            this.SuspendLayout();
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(196, 78);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(17, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 21;
            this.neuLabel3.Text = "页";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(108, 78);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(41, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 19;
            this.neuLabel2.Text = "页到第";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(30, 78);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(29, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 18;
            this.neuLabel1.Text = "从第";
            // 
            // nrbtPrintRange
            // 
            this.nrbtPrintRange.AutoSize = true;
            this.nrbtPrintRange.Location = new System.Drawing.Point(32, 45);
            this.nrbtPrintRange.Name = "nrbtPrintRange";
            this.nrbtPrintRange.Size = new System.Drawing.Size(47, 16);
            this.nrbtPrintRange.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nrbtPrintRange.TabIndex = 2;
            this.nrbtPrintRange.Text = "范围";
            this.nrbtPrintRange.UseVisualStyleBackColor = true;
            // 
            // nrbtPintAll
            // 
            this.nrbtPintAll.AutoSize = true;
            this.nrbtPintAll.Checked = true;
            this.nrbtPintAll.Location = new System.Drawing.Point(32, 12);
            this.nrbtPintAll.Name = "nrbtPintAll";
            this.nrbtPintAll.Size = new System.Drawing.Size(47, 16);
            this.nrbtPintAll.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nrbtPintAll.TabIndex = 1;
            this.nrbtPintAll.TabStop = true;
            this.nrbtPintAll.Text = "全部";
            this.nrbtPintAll.UseVisualStyleBackColor = true;
            // 
            // neuPanel1
            // 
            this.neuPanel1.BackColor = System.Drawing.Color.PaleTurquoise;
            this.neuPanel1.Location = new System.Drawing.Point(3, 110);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(300, 1);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 22;
            // 
            // nbtOK
            // 
            this.nbtOK.Location = new System.Drawing.Point(32, 126);
            this.nbtOK.Name = "nbtOK";
            this.nbtOK.Size = new System.Drawing.Size(75, 23);
            this.nbtOK.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nbtOK.TabIndex = 5;
            this.nbtOK.Text = "确定";
            this.nbtOK.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.nbtOK.UseVisualStyleBackColor = true;
            // 
            // nbtCancel
            // 
            this.nbtCancel.Location = new System.Drawing.Point(152, 126);
            this.nbtCancel.Name = "nbtCancel";
            this.nbtCancel.Size = new System.Drawing.Size(75, 23);
            this.nbtCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nbtCancel.TabIndex = 6;
            this.nbtCancel.Text = "取消";
            this.nbtCancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.nbtCancel.UseVisualStyleBackColor = true;
            // 
            // nntbFromPage
            // 
            this.nntbFromPage.AllowNegative = false;
            this.nntbFromPage.Enabled = false;
            this.nntbFromPage.IsAutoRemoveDecimalZero = false;
            this.nntbFromPage.IsEnter2Tab = false;
            this.nntbFromPage.Location = new System.Drawing.Point(62, 74);
            this.nntbFromPage.Name = "nntbFromPage";
            this.nntbFromPage.NumericPrecision = 1000;
            this.nntbFromPage.NumericScaleOnFocus = 0;
            this.nntbFromPage.NumericScaleOnLostFocus = 0;
            this.nntbFromPage.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nntbFromPage.SetRange = new System.Drawing.Size(-1, -1);
            this.nntbFromPage.Size = new System.Drawing.Size(42, 21);
            this.nntbFromPage.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nntbFromPage.TabIndex = 3;
            this.nntbFromPage.Text = "0";
            this.nntbFromPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nntbFromPage.UseGroupSeperator = false;
            this.nntbFromPage.ZeroIsValid = false;
            // 
            // nntbToPage
            // 
            this.nntbToPage.AllowNegative = false;
            this.nntbToPage.Enabled = false;
            this.nntbToPage.IsAutoRemoveDecimalZero = false;
            this.nntbToPage.IsEnter2Tab = false;
            this.nntbToPage.Location = new System.Drawing.Point(152, 74);
            this.nntbToPage.Name = "nntbToPage";
            this.nntbToPage.NumericPrecision = 1000;
            this.nntbToPage.NumericScaleOnFocus = 0;
            this.nntbToPage.NumericScaleOnLostFocus = 0;
            this.nntbToPage.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nntbToPage.SetRange = new System.Drawing.Size(-1, -1);
            this.nntbToPage.Size = new System.Drawing.Size(42, 21);
            this.nntbToPage.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nntbToPage.TabIndex = 4;
            this.nntbToPage.Text = "0";
            this.nntbToPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nntbToPage.UseGroupSeperator = false;
            this.nntbToPage.ZeroIsValid = false;
            // 
            // PrintPageSelectDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(258, 162);
            this.ControlBox = false;
            this.Controls.Add(this.nntbToPage);
            this.Controls.Add(this.nntbFromPage);
            this.Controls.Add(this.nbtCancel);
            this.Controls.Add(this.nbtOK);
            this.Controls.Add(this.neuPanel1);
            this.Controls.Add(this.neuLabel3);
            this.Controls.Add(this.neuLabel2);
            this.Controls.Add(this.neuLabel1);
            this.Controls.Add(this.nrbtPrintRange);
            this.Controls.Add(this.nrbtPintAll);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PrintPageSelectDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "打印范围选择";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton nrbtPrintRange;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton nrbtPintAll;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuButton nbtOK;
        private FS.FrameWork.WinForms.Controls.NeuButton nbtCancel;
        private FS.FrameWork.WinForms.Controls.NeuNumericTextBox nntbFromPage;
        private FS.FrameWork.WinForms.Controls.NeuNumericTextBox nntbToPage;

    }
}