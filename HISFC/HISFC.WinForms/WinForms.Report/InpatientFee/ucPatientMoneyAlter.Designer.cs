namespace FS.WinForms.Report.InpatientFee
{
    partial class ucPatientMoneyAlter
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
            this.nlbTitle = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlb姓名 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlb总费用 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlb剩余金额 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlb打印时间 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel7 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel8 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel9 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlb床号 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlb住院号 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlb补交金额 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.SuspendLayout();
            // 
            // nlbTitle
            // 
            this.nlbTitle.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlbTitle.Location = new System.Drawing.Point(207, 17);
            this.nlbTitle.Name = "nlbTitle";
            this.nlbTitle.Size = new System.Drawing.Size(139, 27);
            this.nlbTitle.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbTitle.TabIndex = 0;
            this.nlbTitle.Text = "催款通知单";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(46, 101);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(41, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 1;
            this.neuLabel1.Text = "您好：";
            // 
            // nlb姓名
            // 
            this.nlb姓名.AutoSize = true;
            this.nlb姓名.Font = new System.Drawing.Font("宋体", 10.5F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlb姓名.Location = new System.Drawing.Point(93, 70);
            this.nlb姓名.Name = "nlb姓名";
            this.nlb姓名.Size = new System.Drawing.Size(67, 14);
            this.nlb姓名.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlb姓名.TabIndex = 2;
            this.nlb姓名.Text = "患者姓名";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(370, 17);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(125, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 3;
            this.neuLabel2.Text = "您有未结算费用总计：";
            this.neuLabel2.Visible = false;
            // 
            // nlb总费用
            // 
            this.nlb总费用.Font = new System.Drawing.Font("宋体", 10.5F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlb总费用.Location = new System.Drawing.Point(416, 0);
            this.nlb总费用.Name = "nlb总费用";
            this.nlb总费用.Size = new System.Drawing.Size(93, 15);
            this.nlb总费用.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlb总费用.TabIndex = 4;
            this.nlb总费用.Text = "总费用";
            this.nlb总费用.Visible = false;
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(94, 122);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(113, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 5;
            this.neuLabel3.Text = "您的住院押金现剩余";
            // 
            // nlb剩余金额
            // 
            this.nlb剩余金额.AutoSize = true;
            this.nlb剩余金额.Font = new System.Drawing.Font("宋体", 10.5F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlb剩余金额.Location = new System.Drawing.Point(213, 122);
            this.nlb剩余金额.Name = "nlb剩余金额";
            this.nlb剩余金额.Size = new System.Drawing.Size(67, 14);
            this.nlb剩余金额.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlb剩余金额.TabIndex = 6;
            this.nlb剩余金额.Text = "剩余金额";
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point(324, 122);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(137, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 7;
            this.neuLabel4.Text = "请去住院处补交住院押金";
            // 
            // neuLabel5
            // 
            this.neuLabel5.Location = new System.Drawing.Point(4, 237);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(574, 12);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 8;
            this.neuLabel5.Text = "---------------------------------------------------------------------------------" +
                "--------------";
            // 
            // nlb打印时间
            // 
            this.nlb打印时间.Location = new System.Drawing.Point(46, 205);
            this.nlb打印时间.Name = "nlb打印时间";
            this.nlb打印时间.Size = new System.Drawing.Size(105, 15);
            this.nlb打印时间.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlb打印时间.TabIndex = 9;
            this.nlb打印时间.Text = "打印时间";
            // 
            // neuLabel6
            // 
            this.neuLabel6.Location = new System.Drawing.Point(279, 205);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(105, 15);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 10;
            this.neuLabel6.Text = "家属签字：";
            // 
            // neuLabel7
            // 
            this.neuLabel7.AutoSize = true;
            this.neuLabel7.Location = new System.Drawing.Point(46, 72);
            this.neuLabel7.Name = "neuLabel7";
            this.neuLabel7.Size = new System.Drawing.Size(41, 12);
            this.neuLabel7.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel7.TabIndex = 11;
            this.neuLabel7.Text = "姓名：";
            // 
            // neuLabel8
            // 
            this.neuLabel8.AutoSize = true;
            this.neuLabel8.Location = new System.Drawing.Point(175, 72);
            this.neuLabel8.Name = "neuLabel8";
            this.neuLabel8.Size = new System.Drawing.Size(41, 12);
            this.neuLabel8.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel8.TabIndex = 12;
            this.neuLabel8.Text = "床号：";
            // 
            // neuLabel9
            // 
            this.neuLabel9.AutoSize = true;
            this.neuLabel9.Location = new System.Drawing.Point(297, 72);
            this.neuLabel9.Name = "neuLabel9";
            this.neuLabel9.Size = new System.Drawing.Size(53, 12);
            this.neuLabel9.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel9.TabIndex = 13;
            this.neuLabel9.Text = "住院号：";
            // 
            // nlb床号
            // 
            this.nlb床号.AutoSize = true;
            this.nlb床号.Font = new System.Drawing.Font("宋体", 10.5F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlb床号.Location = new System.Drawing.Point(212, 70);
            this.nlb床号.Name = "nlb床号";
            this.nlb床号.Size = new System.Drawing.Size(37, 14);
            this.nlb床号.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlb床号.TabIndex = 14;
            this.nlb床号.Text = "床号";
            // 
            // nlb住院号
            // 
            this.nlb住院号.AutoSize = true;
            this.nlb住院号.Font = new System.Drawing.Font("宋体", 10.5F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlb住院号.Location = new System.Drawing.Point(346, 70);
            this.nlb住院号.Name = "nlb住院号";
            this.nlb住院号.Size = new System.Drawing.Size(52, 14);
            this.nlb住院号.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlb住院号.TabIndex = 15;
            this.nlb住院号.Text = "住院号";
            // 
            // nlb补交金额
            // 
            this.nlb补交金额.AutoSize = true;
            this.nlb补交金额.Font = new System.Drawing.Font("宋体", 10.5F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlb补交金额.Location = new System.Drawing.Point(461, 122);
            this.nlb补交金额.Name = "nlb补交金额";
            this.nlb补交金额.Size = new System.Drawing.Size(67, 14);
            this.nlb补交金额.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlb补交金额.TabIndex = 16;
            this.nlb补交金额.Text = "补交金额";
            // 
            // ucPatientMoneyAlter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.nlb补交金额);
            this.Controls.Add(this.nlb住院号);
            this.Controls.Add(this.nlb床号);
            this.Controls.Add(this.neuLabel9);
            this.Controls.Add(this.neuLabel8);
            this.Controls.Add(this.neuLabel7);
            this.Controls.Add(this.neuLabel6);
            this.Controls.Add(this.nlb打印时间);
            this.Controls.Add(this.neuLabel5);
            this.Controls.Add(this.neuLabel4);
            this.Controls.Add(this.nlb剩余金额);
            this.Controls.Add(this.neuLabel3);
            this.Controls.Add(this.nlb总费用);
            this.Controls.Add(this.neuLabel2);
            this.Controls.Add(this.nlb姓名);
            this.Controls.Add(this.neuLabel1);
            this.Controls.Add(this.nlbTitle);
            this.Name = "ucPatientMoneyAlter";
            this.Size = new System.Drawing.Size(574, 268);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuLabel nlbTitle;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlb姓名;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlb总费用;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlb剩余金额;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlb打印时间;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel7;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel8;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel9;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlb床号;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlb住院号;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlb补交金额;
    }
}
