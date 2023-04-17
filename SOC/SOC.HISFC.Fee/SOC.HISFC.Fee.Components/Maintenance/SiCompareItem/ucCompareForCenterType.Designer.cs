namespace FS.SOC.HISFC.Fee.Components.Maintenance.SiCompareItem
{
    partial class ucCompareForCenterType
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.neuPayRate = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel9 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuUseCode = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel8 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel7 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuMemo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbGrade = new System.Windows.Forms.ComboBox();
            this.lbModifyInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.btnSave = new System.Windows.Forms.Button();
            this.neuCenerName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuCenterCode = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuItemName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuItemCode = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtCenterType = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.neuPayRate);
            this.groupBox1.Controls.Add(this.neuLabel9);
            this.groupBox1.Controls.Add(this.neuUseCode);
            this.groupBox1.Controls.Add(this.neuLabel8);
            this.groupBox1.Controls.Add(this.neuLabel7);
            this.groupBox1.Controls.Add(this.neuMemo);
            this.groupBox1.Controls.Add(this.neuLabel6);
            this.groupBox1.Controls.Add(this.cmbGrade);
            this.groupBox1.Controls.Add(this.lbModifyInfo);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.neuCenerName);
            this.groupBox1.Controls.Add(this.neuLabel4);
            this.groupBox1.Controls.Add(this.neuCenterCode);
            this.groupBox1.Controls.Add(this.neuLabel5);
            this.groupBox1.Controls.Add(this.neuItemName);
            this.groupBox1.Controls.Add(this.neuLabel3);
            this.groupBox1.Controls.Add(this.neuItemCode);
            this.groupBox1.Controls.Add(this.neuLabel2);
            this.groupBox1.Controls.Add(this.txtCenterType);
            this.groupBox1.Controls.Add(this.neuLabel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(766, 152);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "医保类型信息对照";
            // 
            // neuPayRate
            // 
            this.neuPayRate.IsEnter2Tab = false;
            this.neuPayRate.Location = new System.Drawing.Point(207, 110);
            this.neuPayRate.Name = "neuPayRate";
            this.neuPayRate.Size = new System.Drawing.Size(48, 21);
            this.neuPayRate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPayRate.TabIndex = 14;
            this.neuPayRate.Text = "0.00";
            this.neuPayRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.neuPayRate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.neuPayRate_KeyDown);
            // 
            // neuLabel9
            // 
            this.neuLabel9.AutoSize = true;
            this.neuLabel9.ForeColor = System.Drawing.Color.Red;
            this.neuLabel9.Location = new System.Drawing.Point(145, 113);
            this.neuLabel9.Name = "neuLabel9";
            this.neuLabel9.Size = new System.Drawing.Size(65, 12);
            this.neuLabel9.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel9.TabIndex = 23;
            this.neuLabel9.Text = "自付比例：";
            this.neuLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // neuUseCode
            // 
            this.neuUseCode.IsEnter2Tab = false;
            this.neuUseCode.Location = new System.Drawing.Point(559, 47);
            this.neuUseCode.Name = "neuUseCode";
            this.neuUseCode.ReadOnly = true;
            this.neuUseCode.Size = new System.Drawing.Size(75, 21);
            this.neuUseCode.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuUseCode.TabIndex = 22;
            // 
            // neuLabel8
            // 
            this.neuLabel8.AutoSize = true;
            this.neuLabel8.ForeColor = System.Drawing.Color.Black;
            this.neuLabel8.Location = new System.Drawing.Point(497, 51);
            this.neuLabel8.Name = "neuLabel8";
            this.neuLabel8.Size = new System.Drawing.Size(65, 12);
            this.neuLabel8.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel8.TabIndex = 21;
            this.neuLabel8.Text = "自定义码：";
            // 
            // neuLabel7
            // 
            this.neuLabel7.AutoSize = true;
            this.neuLabel7.ForeColor = System.Drawing.Color.Red;
            this.neuLabel7.Location = new System.Drawing.Point(267, 113);
            this.neuLabel7.Name = "neuLabel7";
            this.neuLabel7.Size = new System.Drawing.Size(41, 12);
            this.neuLabel7.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel7.TabIndex = 20;
            this.neuLabel7.Text = "备注：";
            this.neuLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // neuMemo
            // 
            this.neuMemo.IsEnter2Tab = false;
            this.neuMemo.Location = new System.Drawing.Point(313, 109);
            this.neuMemo.MaxLength = 50;
            this.neuMemo.Multiline = true;
            this.neuMemo.Name = "neuMemo";
            this.neuMemo.Size = new System.Drawing.Size(321, 37);
            this.neuMemo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuMemo.TabIndex = 15;
            this.neuMemo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.neuMemo_KeyDown);
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.ForeColor = System.Drawing.Color.Red;
            this.neuLabel6.Location = new System.Drawing.Point(8, 113);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(65, 12);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 18;
            this.neuLabel6.Text = "甲乙等级：";
            // 
            // cmbGrade
            // 
            this.cmbGrade.FormattingEnabled = true;
            this.cmbGrade.Items.AddRange(new object[] {
            "未知",
            "甲类",
            "乙类",
            "丙类"});
            this.cmbGrade.Location = new System.Drawing.Point(74, 110);
            this.cmbGrade.Name = "cmbGrade";
            this.cmbGrade.Size = new System.Drawing.Size(70, 20);
            this.cmbGrade.TabIndex = 13;
            this.cmbGrade.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbGrade_KeyDown);
            // 
            // lbModifyInfo
            // 
            this.lbModifyInfo.AutoSize = true;
            this.lbModifyInfo.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbModifyInfo.ForeColor = System.Drawing.Color.Red;
            this.lbModifyInfo.Location = new System.Drawing.Point(280, 22);
            this.lbModifyInfo.Name = "lbModifyInfo";
            this.lbModifyInfo.Size = new System.Drawing.Size(82, 14);
            this.lbModifyInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbModifyInfo.TabIndex = 16;
            this.lbModifyInfo.Text = "正在维护：";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(559, 78);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "对 照";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // neuCenerName
            // 
            this.neuCenerName.IsEnter2Tab = false;
            this.neuCenerName.Location = new System.Drawing.Point(311, 77);
            this.neuCenerName.Name = "neuCenerName";
            this.neuCenerName.Size = new System.Drawing.Size(180, 21);
            this.neuCenerName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuCenerName.TabIndex = 12;
            this.neuCenerName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.neuCenerName_KeyDown);
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.ForeColor = System.Drawing.Color.Red;
            this.neuLabel4.Location = new System.Drawing.Point(227, 81);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(89, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 12;
            this.neuLabel4.Text = "医保项目名称：";
            // 
            // neuCenterCode
            // 
            this.neuCenterCode.IsEnter2Tab = false;
            this.neuCenterCode.Location = new System.Drawing.Point(95, 77);
            this.neuCenterCode.Name = "neuCenterCode";
            this.neuCenterCode.Size = new System.Drawing.Size(131, 21);
            this.neuCenterCode.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuCenterCode.TabIndex = 11;
            this.neuCenterCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.neuCenterCode_KeyDown);
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.ForeColor = System.Drawing.Color.Red;
            this.neuLabel5.Location = new System.Drawing.Point(8, 81);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(89, 12);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 10;
            this.neuLabel5.Text = "医保项目代码：";
            // 
            // neuItemName
            // 
            this.neuItemName.IsEnter2Tab = false;
            this.neuItemName.Location = new System.Drawing.Point(311, 47);
            this.neuItemName.Name = "neuItemName";
            this.neuItemName.ReadOnly = true;
            this.neuItemName.Size = new System.Drawing.Size(180, 21);
            this.neuItemName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuItemName.TabIndex = 21;
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(227, 51);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(89, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 8;
            this.neuLabel3.Text = "本地项目名称：";
            // 
            // neuItemCode
            // 
            this.neuItemCode.IsEnter2Tab = false;
            this.neuItemCode.Location = new System.Drawing.Point(95, 47);
            this.neuItemCode.Name = "neuItemCode";
            this.neuItemCode.ReadOnly = true;
            this.neuItemCode.Size = new System.Drawing.Size(131, 21);
            this.neuItemCode.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuItemCode.TabIndex = 20;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(8, 51);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(89, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 6;
            this.neuLabel2.Text = "本地项目代码：";
            // 
            // txtCenterType
            // 
            this.txtCenterType.IsEnter2Tab = false;
            this.txtCenterType.Location = new System.Drawing.Point(70, 19);
            this.txtCenterType.Name = "txtCenterType";
            this.txtCenterType.ReadOnly = true;
            this.txtCenterType.Size = new System.Drawing.Size(200, 21);
            this.txtCenterType.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCenterType.TabIndex = 5;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(8, 23);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(65, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 4;
            this.neuLabel1.Text = "医保类型：";
            // 
            // ucCompareForCenterType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ucCompareForCenterType";
            this.Size = new System.Drawing.Size(766, 152);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox neuCenerName;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuTextBox neuCenterCode;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.NeuTextBox neuItemName;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuTextBox neuItemCode;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtCenterType;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private System.Windows.Forms.Button btnSave;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbModifyInfo;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel7;
        private FS.FrameWork.WinForms.Controls.NeuTextBox neuMemo;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        private System.Windows.Forms.ComboBox cmbGrade;
        private FS.FrameWork.WinForms.Controls.NeuTextBox neuUseCode;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel8;
        private FS.FrameWork.WinForms.Controls.NeuTextBox neuPayRate;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel9;
    }
}
