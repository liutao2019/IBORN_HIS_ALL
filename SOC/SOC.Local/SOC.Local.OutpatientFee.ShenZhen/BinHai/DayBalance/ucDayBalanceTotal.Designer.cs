namespace FS.SOC.Local.OutpatientFee.ShenZhen.BinHai.DayBalance
{
    partial class ucDayBalanceTotal
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
            this.btnbalance = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnbalance
            // 
            this.btnbalance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnbalance.Location = new System.Drawing.Point(0, 0);
            this.btnbalance.Name = "btnbalance";
            this.btnbalance.Size = new System.Drawing.Size(100, 25);
            this.btnbalance.TabIndex = 0;
            this.btnbalance.Text = "操作员日结信息";
            this.btnbalance.UseVisualStyleBackColor = true;
            this.btnbalance.Click += new System.EventHandler(this.btnbalance_Click);
            // 
            // ucDayBalanceTotal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnbalance);
            this.Name = "ucDayBalanceTotal";
            this.Size = new System.Drawing.Size(100, 25);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnbalance;

    }
}
