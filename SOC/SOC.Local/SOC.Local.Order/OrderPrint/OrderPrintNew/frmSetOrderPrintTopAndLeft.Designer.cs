namespace FS.SOC.Local.Order.OrderPrint.OrderPrintNew
{
    partial class frmSetOrderPrintTopAndLeft
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
            this.neuButton1 = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuButton2 = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuNumericTextBox1 = new FS.FrameWork.WinForms.Controls.NeuNumericTextBox();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuLabel7 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuNumericTextBox3 = new FS.FrameWork.WinForms.Controls.NeuNumericTextBox();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuGroupBox2 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuLabel8 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuNumericTextBox2 = new FS.FrameWork.WinForms.Controls.NeuNumericTextBox();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuNumericTextBox4 = new FS.FrameWork.WinForms.Controls.NeuNumericTextBox();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel9 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ntbLongPageY = new FS.FrameWork.WinForms.Controls.NeuNumericTextBox();
            this.ntbShortPageY = new FS.FrameWork.WinForms.Controls.NeuNumericTextBox();
            this.neuLabel10 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel11 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel12 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuGroupBox1.SuspendLayout();
            this.neuGroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuButton1
            // 
            this.neuButton1.Location = new System.Drawing.Point(80, 211);
            this.neuButton1.Name = "neuButton1";
            this.neuButton1.Size = new System.Drawing.Size(75, 23);
            this.neuButton1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuButton1.TabIndex = 0;
            this.neuButton1.Text = "确定";
            this.neuButton1.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuButton1.UseVisualStyleBackColor = true;
            this.neuButton1.Click += new System.EventHandler(this.neuButton1_Click);
            // 
            // neuButton2
            // 
            this.neuButton2.Location = new System.Drawing.Point(239, 211);
            this.neuButton2.Name = "neuButton2";
            this.neuButton2.Size = new System.Drawing.Size(75, 23);
            this.neuButton2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuButton2.TabIndex = 1;
            this.neuButton2.Text = "取消";
            this.neuButton2.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuButton2.UseVisualStyleBackColor = true;
            this.neuButton2.Click += new System.EventHandler(this.neuButton2_Click);
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(5, 40);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(53, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 2;
            this.neuLabel1.Text = "左边距：";
            // 
            // neuNumericTextBox1
            // 
            this.neuNumericTextBox1.AllowNegative = false;
            this.neuNumericTextBox1.IsAutoRemoveDecimalZero = false;
            this.neuNumericTextBox1.IsEnter2Tab = false;
            this.neuNumericTextBox1.Location = new System.Drawing.Point(81, 37);
            this.neuNumericTextBox1.Name = "neuNumericTextBox1";
            this.neuNumericTextBox1.NumericPrecision = 5;
            this.neuNumericTextBox1.NumericScaleOnFocus = 1;
            this.neuNumericTextBox1.NumericScaleOnLostFocus = 1;
            this.neuNumericTextBox1.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.neuNumericTextBox1.SetRange = new System.Drawing.Size(-1, -1);
            this.neuNumericTextBox1.Size = new System.Drawing.Size(100, 21);
            this.neuNumericTextBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuNumericTextBox1.TabIndex = 3;
            this.neuNumericTextBox1.Text = "0.0";
            this.neuNumericTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.neuNumericTextBox1.UseGroupSeperator = true;
            this.neuNumericTextBox1.ZeroIsValid = false;
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.neuLabel10);
            this.neuGroupBox1.Controls.Add(this.ntbLongPageY);
            this.neuGroupBox1.Controls.Add(this.neuLabel9);
            this.neuGroupBox1.Controls.Add(this.neuLabel7);
            this.neuGroupBox1.Controls.Add(this.neuLabel5);
            this.neuGroupBox1.Controls.Add(this.neuNumericTextBox3);
            this.neuGroupBox1.Controls.Add(this.neuLabel3);
            this.neuGroupBox1.Controls.Add(this.neuNumericTextBox1);
            this.neuGroupBox1.Controls.Add(this.neuLabel1);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(403, 101);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 4;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "长期医嘱";
            // 
            // neuLabel7
            // 
            this.neuLabel7.AutoSize = true;
            this.neuLabel7.Location = new System.Drawing.Point(380, 42);
            this.neuLabel7.Name = "neuLabel7";
            this.neuLabel7.Size = new System.Drawing.Size(17, 12);
            this.neuLabel7.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel7.TabIndex = 7;
            this.neuLabel7.Text = "cm";
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Location = new System.Drawing.Point(184, 42);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(17, 12);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 6;
            this.neuLabel5.Text = "cm";
            // 
            // neuNumericTextBox3
            // 
            this.neuNumericTextBox3.AllowNegative = false;
            this.neuNumericTextBox3.IsAutoRemoveDecimalZero = false;
            this.neuNumericTextBox3.IsEnter2Tab = false;
            this.neuNumericTextBox3.Location = new System.Drawing.Point(278, 37);
            this.neuNumericTextBox3.Name = "neuNumericTextBox3";
            this.neuNumericTextBox3.NumericPrecision = 5;
            this.neuNumericTextBox3.NumericScaleOnFocus = 1;
            this.neuNumericTextBox3.NumericScaleOnLostFocus = 1;
            this.neuNumericTextBox3.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.neuNumericTextBox3.SetRange = new System.Drawing.Size(-1, -1);
            this.neuNumericTextBox3.Size = new System.Drawing.Size(100, 21);
            this.neuNumericTextBox3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuNumericTextBox3.TabIndex = 5;
            this.neuNumericTextBox3.Text = "0.0";
            this.neuNumericTextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.neuNumericTextBox3.UseGroupSeperator = true;
            this.neuNumericTextBox3.ZeroIsValid = false;
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(217, 40);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(53, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 4;
            this.neuLabel3.Text = "上边距：";
            // 
            // neuGroupBox2
            // 
            this.neuGroupBox2.Controls.Add(this.ntbShortPageY);
            this.neuGroupBox2.Controls.Add(this.neuLabel12);
            this.neuGroupBox2.Controls.Add(this.neuLabel11);
            this.neuGroupBox2.Controls.Add(this.neuLabel8);
            this.neuGroupBox2.Controls.Add(this.neuLabel6);
            this.neuGroupBox2.Controls.Add(this.neuNumericTextBox2);
            this.neuGroupBox2.Controls.Add(this.neuLabel2);
            this.neuGroupBox2.Controls.Add(this.neuNumericTextBox4);
            this.neuGroupBox2.Controls.Add(this.neuLabel4);
            this.neuGroupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox2.Location = new System.Drawing.Point(0, 101);
            this.neuGroupBox2.Name = "neuGroupBox2";
            this.neuGroupBox2.Size = new System.Drawing.Size(403, 100);
            this.neuGroupBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox2.TabIndex = 5;
            this.neuGroupBox2.TabStop = false;
            this.neuGroupBox2.Text = "临时医嘱";
            // 
            // neuLabel8
            // 
            this.neuLabel8.AutoSize = true;
            this.neuLabel8.Location = new System.Drawing.Point(384, 44);
            this.neuLabel8.Name = "neuLabel8";
            this.neuLabel8.Size = new System.Drawing.Size(17, 12);
            this.neuLabel8.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel8.TabIndex = 10;
            this.neuLabel8.Text = "cm";
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.Location = new System.Drawing.Point(184, 44);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(17, 12);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 7;
            this.neuLabel6.Text = "cm";
            // 
            // neuNumericTextBox2
            // 
            this.neuNumericTextBox2.AllowNegative = false;
            this.neuNumericTextBox2.IsAutoRemoveDecimalZero = false;
            this.neuNumericTextBox2.IsEnter2Tab = false;
            this.neuNumericTextBox2.Location = new System.Drawing.Point(278, 38);
            this.neuNumericTextBox2.Name = "neuNumericTextBox2";
            this.neuNumericTextBox2.NumericPrecision = 5;
            this.neuNumericTextBox2.NumericScaleOnFocus = 1;
            this.neuNumericTextBox2.NumericScaleOnLostFocus = 1;
            this.neuNumericTextBox2.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.neuNumericTextBox2.SetRange = new System.Drawing.Size(-1, -1);
            this.neuNumericTextBox2.Size = new System.Drawing.Size(100, 21);
            this.neuNumericTextBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuNumericTextBox2.TabIndex = 9;
            this.neuNumericTextBox2.Text = "0.0";
            this.neuNumericTextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.neuNumericTextBox2.UseGroupSeperator = true;
            this.neuNumericTextBox2.ZeroIsValid = false;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(217, 41);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(53, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 8;
            this.neuLabel2.Text = "上边距：";
            // 
            // neuNumericTextBox4
            // 
            this.neuNumericTextBox4.AllowNegative = false;
            this.neuNumericTextBox4.IsAutoRemoveDecimalZero = false;
            this.neuNumericTextBox4.IsEnter2Tab = false;
            this.neuNumericTextBox4.Location = new System.Drawing.Point(81, 38);
            this.neuNumericTextBox4.Name = "neuNumericTextBox4";
            this.neuNumericTextBox4.NumericPrecision = 5;
            this.neuNumericTextBox4.NumericScaleOnFocus = 1;
            this.neuNumericTextBox4.NumericScaleOnLostFocus = 1;
            this.neuNumericTextBox4.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.neuNumericTextBox4.SetRange = new System.Drawing.Size(-1, -1);
            this.neuNumericTextBox4.Size = new System.Drawing.Size(100, 21);
            this.neuNumericTextBox4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuNumericTextBox4.TabIndex = 7;
            this.neuNumericTextBox4.Text = "0.0";
            this.neuNumericTextBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.neuNumericTextBox4.UseGroupSeperator = true;
            this.neuNumericTextBox4.ZeroIsValid = false;
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point(5, 41);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(53, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 6;
            this.neuLabel4.Text = "左边距：";
            // 
            // neuLabel9
            // 
            this.neuLabel9.AutoSize = true;
            this.neuLabel9.Location = new System.Drawing.Point(6, 69);
            this.neuLabel9.Name = "neuLabel9";
            this.neuLabel9.Size = new System.Drawing.Size(77, 12);
            this.neuLabel9.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel9.TabIndex = 8;
            this.neuLabel9.Text = "页码下边距：";
            // 
            // ntbLongPageY
            // 
            this.ntbLongPageY.AllowNegative = false;
            this.ntbLongPageY.IsAutoRemoveDecimalZero = false;
            this.ntbLongPageY.IsEnter2Tab = false;
            this.ntbLongPageY.Location = new System.Drawing.Point(80, 66);
            this.ntbLongPageY.Name = "ntbLongPageY";
            this.ntbLongPageY.NumericPrecision = 5;
            this.ntbLongPageY.NumericScaleOnFocus = 1;
            this.ntbLongPageY.NumericScaleOnLostFocus = 1;
            this.ntbLongPageY.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ntbLongPageY.SetRange = new System.Drawing.Size(-1, -1);
            this.ntbLongPageY.Size = new System.Drawing.Size(100, 21);
            this.ntbLongPageY.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ntbLongPageY.TabIndex = 9;
            this.ntbLongPageY.Text = "0.0";
            this.ntbLongPageY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ntbLongPageY.UseGroupSeperator = true;
            this.ntbLongPageY.ZeroIsValid = false;
            // 
            // ntbShortPageY
            // 
            this.ntbShortPageY.AllowNegative = false;
            this.ntbShortPageY.IsAutoRemoveDecimalZero = false;
            this.ntbShortPageY.IsEnter2Tab = false;
            this.ntbShortPageY.Location = new System.Drawing.Point(80, 66);
            this.ntbShortPageY.Name = "ntbShortPageY";
            this.ntbShortPageY.NumericPrecision = 5;
            this.ntbShortPageY.NumericScaleOnFocus = 1;
            this.ntbShortPageY.NumericScaleOnLostFocus = 1;
            this.ntbShortPageY.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ntbShortPageY.SetRange = new System.Drawing.Size(-1, -1);
            this.ntbShortPageY.Size = new System.Drawing.Size(100, 21);
            this.ntbShortPageY.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ntbShortPageY.TabIndex = 10;
            this.ntbShortPageY.Text = "0.0";
            this.ntbShortPageY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ntbShortPageY.UseGroupSeperator = true;
            this.ntbShortPageY.ZeroIsValid = false;
            // 
            // neuLabel10
            // 
            this.neuLabel10.AutoSize = true;
            this.neuLabel10.Location = new System.Drawing.Point(184, 69);
            this.neuLabel10.Name = "neuLabel10";
            this.neuLabel10.Size = new System.Drawing.Size(17, 12);
            this.neuLabel10.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel10.TabIndex = 10;
            this.neuLabel10.Text = "cm";
            // 
            // neuLabel11
            // 
            this.neuLabel11.AutoSize = true;
            this.neuLabel11.Location = new System.Drawing.Point(184, 69);
            this.neuLabel11.Name = "neuLabel11";
            this.neuLabel11.Size = new System.Drawing.Size(17, 12);
            this.neuLabel11.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel11.TabIndex = 11;
            this.neuLabel11.Text = "cm";
            // 
            // neuLabel12
            // 
            this.neuLabel12.AutoSize = true;
            this.neuLabel12.Location = new System.Drawing.Point(6, 69);
            this.neuLabel12.Name = "neuLabel12";
            this.neuLabel12.Size = new System.Drawing.Size(77, 12);
            this.neuLabel12.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel12.TabIndex = 12;
            this.neuLabel12.Text = "页码下边距：";
            // 
            // frmSetOrderPrintTopAndLeft
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 263);
            this.Controls.Add(this.neuGroupBox2);
            this.Controls.Add(this.neuGroupBox1);
            this.Controls.Add(this.neuButton2);
            this.Controls.Add(this.neuButton1);
            this.Name = "frmSetOrderPrintTopAndLeft";
            this.Text = "设置医嘱打印的上边距和左边距";
            this.Load += new System.EventHandler(this.frmSetOrderPrintTopAndLeft_Load);
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.neuGroupBox2.ResumeLayout(false);
            this.neuGroupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuButton neuButton1;
        private FS.FrameWork.WinForms.Controls.NeuButton neuButton2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuNumericTextBox neuNumericTextBox1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuNumericTextBox neuNumericTextBox3;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox2;
        private FS.FrameWork.WinForms.Controls.NeuNumericTextBox neuNumericTextBox2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuNumericTextBox neuNumericTextBox4;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel7;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel8;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel10;
        private FS.FrameWork.WinForms.Controls.NeuNumericTextBox ntbLongPageY;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel9;
        private FS.FrameWork.WinForms.Controls.NeuNumericTextBox ntbShortPageY;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel12;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel11;
    }
}