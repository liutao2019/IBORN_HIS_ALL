namespace FS.HISFC.Components.Speciment
{
    partial class ucColForOrg
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
            this.components = new System.ComponentModel.Container();
            this.chkOnlySelf = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nudCount = new System.Windows.Forms.NumericUpDown();
            this.chkName = new System.Windows.Forms.CheckBox();
            this.txtBarCode = new System.Windows.Forms.TextBox();
            this.lsbBarCode = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSetUseAlone = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.chk115 = new System.Windows.Forms.CheckBox();
            this.nud115 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.chk863 = new System.Windows.Forms.CheckBox();
            this.nud863 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.chkChip = new System.Windows.Forms.CheckBox();
            this.txtDocName = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.chkAutoGen = new System.Windows.Forms.CheckBox();
            this.btnProject = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudCount)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud115)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud863)).BeginInit();
            this.SuspendLayout();
            // 
            // chkOnlySelf
            // 
            this.chkOnlySelf.AutoSize = true;
            this.chkOnlySelf.Location = new System.Drawing.Point(128, -10);
            this.chkOnlySelf.Margin = new System.Windows.Forms.Padding(4);
            this.chkOnlySelf.Name = "chkOnlySelf";
            this.chkOnlySelf.Size = new System.Drawing.Size(91, 20);
            this.chkOnlySelf.TabIndex = 19;
            this.chkOnlySelf.Text = "指定使用";
            this.chkOnlySelf.UseVisualStyleBackColor = true;
            this.chkOnlySelf.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(172, 23);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 16);
            this.label2.TabIndex = 15;
            this.label2.Text = "份";
            // 
            // nudCount
            // 
            this.nudCount.Location = new System.Drawing.Point(111, 18);
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
            this.nudCount.TabIndex = 13;
            this.nudCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCount.ValueChanged += new System.EventHandler(this.nudCount_ValueChanged);
            // 
            // chkName
            // 
            this.chkName.AutoSize = true;
            this.chkName.Checked = true;
            this.chkName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkName.Location = new System.Drawing.Point(8, 21);
            this.chkName.Margin = new System.Windows.Forms.Padding(4);
            this.chkName.Name = "chkName";
            this.chkName.Size = new System.Drawing.Size(99, 20);
            this.chkName.TabIndex = 11;
            this.chkName.Text = "checkBox1";
            this.chkName.UseVisualStyleBackColor = true;
            this.chkName.CheckedChanged += new System.EventHandler(this.chkName_CheckedChanged);
            // 
            // txtBarCode
            // 
            this.txtBarCode.Location = new System.Drawing.Point(494, 48);
            this.txtBarCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtBarCode.Name = "txtBarCode";
            this.txtBarCode.Size = new System.Drawing.Size(129, 26);
            this.txtBarCode.TabIndex = 20;
            this.txtBarCode.TextChanged += new System.EventHandler(this.txtBarCode_TextChanged);
            this.txtBarCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarCode_KeyDown);
            // 
            // lsbBarCode
            // 
            this.lsbBarCode.FormattingEnabled = true;
            this.lsbBarCode.ItemHeight = 16;
            this.lsbBarCode.Location = new System.Drawing.Point(638, 12);
            this.lsbBarCode.Margin = new System.Windows.Forms.Padding(4);
            this.lsbBarCode.Name = "lsbBarCode";
            this.lsbBarCode.Size = new System.Drawing.Size(231, 68);
            this.lsbBarCode.TabIndex = 21;
            this.lsbBarCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbBarCode_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnProject);
            this.groupBox1.Controls.Add(this.btnSetUseAlone);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.chk115);
            this.groupBox1.Controls.Add(this.nud115);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.chk863);
            this.groupBox1.Controls.Add(this.nud863);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.chkChip);
            this.groupBox1.Controls.Add(this.txtDocName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.chkAutoGen);
            this.groupBox1.Controls.Add(this.chkName);
            this.groupBox1.Controls.Add(this.lsbBarCode);
            this.groupBox1.Controls.Add(this.nudCount);
            this.groupBox1.Controls.Add(this.txtBarCode);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.chkOnlySelf);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(898, 84);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            // 
            // btnSetUseAlone
            // 
            this.btnSetUseAlone.Location = new System.Drawing.Point(494, 16);
            this.btnSetUseAlone.Name = "btnSetUseAlone";
            this.btnSetUseAlone.Size = new System.Drawing.Size(129, 28);
            this.btnSetUseAlone.TabIndex = 34;
            this.btnSetUseAlone.Text = "设置专用";
            this.btnSetUseAlone.UseVisualStyleBackColor = true;
            this.btnSetUseAlone.Click += new System.EventHandler(this.btnSetUseAlone_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 53);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 16);
            this.label5.TabIndex = 33;
            this.label5.Text = "其中";
            // 
            // chk115
            // 
            this.chk115.AutoSize = true;
            this.chk115.Location = new System.Drawing.Point(212, 52);
            this.chk115.Margin = new System.Windows.Forms.Padding(4);
            this.chk115.Name = "chk115";
            this.chk115.Size = new System.Drawing.Size(51, 20);
            this.chk115.TabIndex = 30;
            this.chk115.Text = "115";
            this.chk115.UseVisualStyleBackColor = true;
            this.chk115.CheckedChanged += new System.EventHandler(this.chk115_CheckedChanged);
            // 
            // nud115
            // 
            this.nud115.Location = new System.Drawing.Point(266, 50);
            this.nud115.Margin = new System.Windows.Forms.Padding(4);
            this.nud115.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nud115.Name = "nud115";
            this.nud115.Size = new System.Drawing.Size(53, 26);
            this.nud115.TabIndex = 31;
            this.nud115.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud115.ValueChanged += new System.EventHandler(this.nud863_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(322, 54);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 16);
            this.label4.TabIndex = 32;
            this.label4.Text = "份";
            // 
            // chk863
            // 
            this.chk863.AutoSize = true;
            this.chk863.Location = new System.Drawing.Point(52, 52);
            this.chk863.Margin = new System.Windows.Forms.Padding(4);
            this.chk863.Name = "chk863";
            this.chk863.Size = new System.Drawing.Size(51, 20);
            this.chk863.TabIndex = 27;
            this.chk863.Text = "863";
            this.chk863.UseVisualStyleBackColor = true;
            this.chk863.CheckedChanged += new System.EventHandler(this.chk863_CheckedChanged);
            // 
            // nud863
            // 
            this.nud863.Location = new System.Drawing.Point(111, 49);
            this.nud863.Margin = new System.Windows.Forms.Padding(4);
            this.nud863.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nud863.Name = "nud863";
            this.nud863.Size = new System.Drawing.Size(53, 26);
            this.nud863.TabIndex = 28;
            this.nud863.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud863.ValueChanged += new System.EventHandler(this.nud863_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(172, 54);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 16);
            this.label3.TabIndex = 29;
            this.label3.Text = "份";
            // 
            // chkChip
            // 
            this.chkChip.AutoSize = true;
            this.chkChip.Location = new System.Drawing.Point(397, 19);
            this.chkChip.Name = "chkChip";
            this.chkChip.Size = new System.Drawing.Size(91, 20);
            this.chkChip.TabIndex = 26;
            this.chkChip.Text = "芯片处理";
            this.chkChip.UseVisualStyleBackColor = true;
            // 
            // txtDocName
            // 
            //this.txtDocName.A = false;
            this.txtDocName.ArrowBackColor = System.Drawing.Color.Silver;
            this.txtDocName.FormattingEnabled = true;
            this.txtDocName.IsFlat = true;
            this.txtDocName.IsLike = true;
            this.txtDocName.Location = new System.Drawing.Point(239, -12);
            this.txtDocName.Margin = new System.Windows.Forms.Padding(4);
            this.txtDocName.Name = "txtDocName";
            this.txtDocName.PopForm = null;
            this.txtDocName.ShowCustomerList = false;
            this.txtDocName.ShowID = false;
            this.txtDocName.Size = new System.Drawing.Size(128, 24);
            this.txtDocName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtDocName.TabIndex = 25;
            this.txtDocName.Tag = "";
            this.txtDocName.ToolBarUse = false;
            this.txtDocName.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(416, 54);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 24;
            this.label1.Text = "条形码：";
            // 
            // chkAutoGen
            // 
            this.chkAutoGen.AutoSize = true;
            this.chkAutoGen.Location = new System.Drawing.Point(212, 21);
            this.chkAutoGen.Margin = new System.Windows.Forms.Padding(4);
            this.chkAutoGen.Name = "chkAutoGen";
            this.chkAutoGen.Size = new System.Drawing.Size(123, 20);
            this.chkAutoGen.TabIndex = 22;
            this.chkAutoGen.Text = "自动获取条码";
            this.chkAutoGen.UseVisualStyleBackColor = true;
            this.chkAutoGen.CheckedChanged += new System.EventHandler(this.chkGetCode_CheckedChanged);
            // 
            // btnProject
            // 
            this.btnProject.Location = new System.Drawing.Point(353, 48);
            this.btnProject.Name = "btnProject";
            this.btnProject.Size = new System.Drawing.Size(56, 28);
            this.btnProject.TabIndex = 35;
            this.btnProject.Text = "项目";
            this.btnProject.UseVisualStyleBackColor = true;
            this.btnProject.Click += new System.EventHandler(this.btnProject_Click);
            // 
            // ucColForOrg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ucColForOrg";
            this.Size = new System.Drawing.Size(898, 84);
            this.Load += new System.EventHandler(this.ucColForOrg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudCount)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud115)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud863)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkOnlySelf;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudCount;
        private System.Windows.Forms.CheckBox chkName;
        private System.Windows.Forms.TextBox txtBarCode;
        private System.Windows.Forms.ListBox lsbBarCode;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkAutoGen;
        private System.Windows.Forms.Label label1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox txtDocName;
        private System.Windows.Forms.CheckBox chkChip;
        private System.Windows.Forms.CheckBox chk863;
        private System.Windows.Forms.NumericUpDown nud863;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chk115;
        private System.Windows.Forms.NumericUpDown nud115;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSetUseAlone;
        private System.Windows.Forms.Button btnProject;
    }
}
