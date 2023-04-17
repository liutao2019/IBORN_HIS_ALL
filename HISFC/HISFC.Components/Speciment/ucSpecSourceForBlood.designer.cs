namespace FS.HISFC.Components.Speciment
{
    partial class ucSpecSourceForBlood
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
            this.chkName = new System.Windows.Forms.CheckBox();
            this.nudCapcity = new System.Windows.Forms.NumericUpDown();
            this.nudCount = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nudUse = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.chkOnlySelf = new System.Windows.Forms.CheckBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.grpType = new System.Windows.Forms.GroupBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.lsbBarCode = new System.Windows.Forms.ListBox();
            this.chkAutoGen = new System.Windows.Forms.CheckBox();
            this.txtBarCode = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudCapcity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudUse)).BeginInit();
            this.grpType.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkName
            // 
            this.chkName.AutoSize = true;
            this.chkName.Checked = true;
            this.chkName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkName.Location = new System.Drawing.Point(8, 20);
            this.chkName.Margin = new System.Windows.Forms.Padding(4);
            this.chkName.Name = "chkName";
            this.chkName.Size = new System.Drawing.Size(99, 20);
            this.chkName.TabIndex = 0;
            this.chkName.Text = "checkBox1";
            this.chkName.UseVisualStyleBackColor = true;
            // 
            // nudCapcity
            // 
            this.nudCapcity.DecimalPlaces = 2;
            this.nudCapcity.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.nudCapcity.Location = new System.Drawing.Point(113, 17);
            this.nudCapcity.Margin = new System.Windows.Forms.Padding(4);
            this.nudCapcity.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudCapcity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudCapcity.Name = "nudCapcity";
            this.nudCapcity.Size = new System.Drawing.Size(65, 26);
            this.nudCapcity.TabIndex = 1;
            this.nudCapcity.Value = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            // 
            // nudCount
            // 
            this.nudCount.Location = new System.Drawing.Point(207, 17);
            this.nudCount.Margin = new System.Windows.Forms.Padding(4);
            this.nudCount.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudCount.Name = "nudCount";
            this.nudCount.Size = new System.Drawing.Size(53, 26);
            this.nudCount.TabIndex = 2;
            this.nudCount.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudCount.ValueChanged += new System.EventHandler(this.nudCount_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(184, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "ml";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(265, 22);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "支";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(300, 22);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "预留";
            // 
            // nudUse
            // 
            this.nudUse.Location = new System.Drawing.Point(347, 17);
            this.nudUse.Margin = new System.Windows.Forms.Padding(4);
            this.nudUse.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudUse.Name = "nudUse";
            this.nudUse.Size = new System.Drawing.Size(53, 26);
            this.nudUse.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(403, 20);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "支";
            // 
            // chkOnlySelf
            // 
            this.chkOnlySelf.AutoSize = true;
            this.chkOnlySelf.Location = new System.Drawing.Point(8, 62);
            this.chkOnlySelf.Margin = new System.Windows.Forms.Padding(4);
            this.chkOnlySelf.Name = "chkOnlySelf";
            this.chkOnlySelf.Size = new System.Drawing.Size(91, 20);
            this.chkOnlySelf.TabIndex = 20;
            this.chkOnlySelf.Text = "指定使用";
            this.chkOnlySelf.UseVisualStyleBackColor = true;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(112, 60);
            this.txtName.Margin = new System.Windows.Forms.Padding(4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(205, 26);
            this.txtName.TabIndex = 21;
            // 
            // grpType
            // 
            this.grpType.Controls.Add(this.btnPrint);
            this.grpType.Controls.Add(this.lsbBarCode);
            this.grpType.Controls.Add(this.chkAutoGen);
            this.grpType.Controls.Add(this.txtBarCode);
            this.grpType.Controls.Add(this.label5);
            this.grpType.Controls.Add(this.chkName);
            this.grpType.Controls.Add(this.txtName);
            this.grpType.Controls.Add(this.nudCapcity);
            this.grpType.Controls.Add(this.chkOnlySelf);
            this.grpType.Controls.Add(this.nudCount);
            this.grpType.Controls.Add(this.label4);
            this.grpType.Controls.Add(this.label1);
            this.grpType.Controls.Add(this.nudUse);
            this.grpType.Controls.Add(this.label2);
            this.grpType.Controls.Add(this.label3);
            this.grpType.Location = new System.Drawing.Point(4, 0);
            this.grpType.Margin = new System.Windows.Forms.Padding(4);
            this.grpType.Name = "grpType";
            this.grpType.Padding = new System.Windows.Forms.Padding(4);
            this.grpType.Size = new System.Drawing.Size(828, 98);
            this.grpType.TabIndex = 22;
            this.grpType.TabStop = false;
            this.grpType.Text = "groupBox1";
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(723, 38);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(100, 31);
            this.btnPrint.TabIndex = 26;
            this.btnPrint.Text = "打印标签";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // lsbBarCode
            // 
            this.lsbBarCode.FormattingEnabled = true;
            this.lsbBarCode.ItemHeight = 16;
            this.lsbBarCode.Location = new System.Drawing.Point(561, 11);
            this.lsbBarCode.Margin = new System.Windows.Forms.Padding(4);
            this.lsbBarCode.Name = "lsbBarCode";
            this.lsbBarCode.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lsbBarCode.Size = new System.Drawing.Size(159, 84);
            this.lsbBarCode.TabIndex = 25;
            this.lsbBarCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lsbBarCode_KeyDown);
            // 
            // chkAutoGen
            // 
            this.chkAutoGen.AutoSize = true;
            this.chkAutoGen.Checked = true;
            this.chkAutoGen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoGen.Location = new System.Drawing.Point(433, 18);
            this.chkAutoGen.Margin = new System.Windows.Forms.Padding(4);
            this.chkAutoGen.Name = "chkAutoGen";
            this.chkAutoGen.Size = new System.Drawing.Size(123, 20);
            this.chkAutoGen.TabIndex = 24;
            this.chkAutoGen.Text = "自动获取条码";
            this.chkAutoGen.UseVisualStyleBackColor = true;
            this.chkAutoGen.CheckedChanged += new System.EventHandler(this.chkAutoGen_CheckedChanged);
            // 
            // txtBarCode
            // 
            this.txtBarCode.Location = new System.Drawing.Point(405, 60);
            this.txtBarCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtBarCode.Name = "txtBarCode";
            this.txtBarCode.Size = new System.Drawing.Size(152, 26);
            this.txtBarCode.TabIndex = 23;
            this.txtBarCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarCode_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(331, 68);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 16);
            this.label5.TabIndex = 22;
            this.label5.Text = "条形码：";
            // 
            // ucSpecSourceForBlood
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpType);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ucSpecSourceForBlood";
            this.Size = new System.Drawing.Size(836, 102);
            this.Load += new System.EventHandler(this.ucSpecSourceForBlood_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudCapcity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudUse)).EndInit();
            this.grpType.ResumeLayout(false);
            this.grpType.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkName;
        private System.Windows.Forms.NumericUpDown nudCapcity;
        private System.Windows.Forms.NumericUpDown nudCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudUse;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkOnlySelf;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.GroupBox grpType;
        private System.Windows.Forms.ListBox lsbBarCode;
        private System.Windows.Forms.CheckBox chkAutoGen;
        private System.Windows.Forms.TextBox txtBarCode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnPrint;
    }
}
