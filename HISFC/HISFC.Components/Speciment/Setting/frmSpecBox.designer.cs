namespace FS.HISFC.Components.Speciment.Setting
{
    partial class frmSpecBox
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
            this.components = new System.ComponentModel.Container();
            this.btnAdd = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.cmbSpecType = new System.Windows.Forms.ComboBox();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbOrgOrBlood = new System.Windows.Forms.ComboBox();
            this.neuLabel8 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbBoxSpec = new System.Windows.Forms.ComboBox();
            this.neuLabel7 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtLocation = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.btnSave = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.rbtShelf = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.rbtIceBox = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbDiseaseType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtComment = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.btnBrowseLoc = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.txtBoxCode = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.btnPrint = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnDet = new System.Windows.Forms.Button();
            this.chk863 = new System.Windows.Forms.CheckBox();
            this.chk115 = new System.Windows.Forms.CheckBox();
            this.btnProject = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.label1 = new System.Windows.Forms.Label();
            this.nudCopy = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudCopy)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(464, 63);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 31);
            this.btnAdd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "添加";
            this.btnAdd.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Visible = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // cmbSpecType
            // 
            this.cmbSpecType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpecType.FormattingEnabled = true;
            this.cmbSpecType.Location = new System.Drawing.Point(177, 106);
            this.cmbSpecType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSpecType.Name = "cmbSpecType";
            this.cmbSpecType.Size = new System.Drawing.Size(132, 24);
            this.cmbSpecType.TabIndex = 3;
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(85, 110);
            this.neuLabel3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(88, 16);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 90;
            this.neuLabel3.Text = "标本类型：";
            // 
            // cmbOrgOrBlood
            // 
            this.cmbOrgOrBlood.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrgOrBlood.FormattingEnabled = true;
            this.cmbOrgOrBlood.Location = new System.Drawing.Point(177, 63);
            this.cmbOrgOrBlood.Margin = new System.Windows.Forms.Padding(4);
            this.cmbOrgOrBlood.Name = "cmbOrgOrBlood";
            this.cmbOrgOrBlood.Size = new System.Drawing.Size(84, 24);
            this.cmbOrgOrBlood.TabIndex = 2;
            this.cmbOrgOrBlood.SelectedIndexChanged += new System.EventHandler(this.cmbOrgOrBlood_SelectedIndexChanged);
            // 
            // neuLabel8
            // 
            this.neuLabel8.AutoSize = true;
            this.neuLabel8.Location = new System.Drawing.Point(85, 67);
            this.neuLabel8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel8.Name = "neuLabel8";
            this.neuLabel8.Size = new System.Drawing.Size(88, 16);
            this.neuLabel8.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel8.TabIndex = 88;
            this.neuLabel8.Text = "标本种类：";
            // 
            // cmbBoxSpec
            // 
            this.cmbBoxSpec.FormattingEnabled = true;
            this.cmbBoxSpec.Location = new System.Drawing.Point(177, 21);
            this.cmbBoxSpec.Margin = new System.Windows.Forms.Padding(4);
            this.cmbBoxSpec.Name = "cmbBoxSpec";
            this.cmbBoxSpec.Size = new System.Drawing.Size(180, 24);
            this.cmbBoxSpec.TabIndex = 1;
            // 
            // neuLabel7
            // 
            this.neuLabel7.AutoSize = true;
            this.neuLabel7.Location = new System.Drawing.Point(85, 322);
            this.neuLabel7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel7.Name = "neuLabel7";
            this.neuLabel7.Size = new System.Drawing.Size(88, 16);
            this.neuLabel7.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel7.TabIndex = 86;
            this.neuLabel7.Text = "存放位置：";
            // 
            // txtLocation
            // 
            this.txtLocation.IsEnter2Tab = false;
            this.txtLocation.Location = new System.Drawing.Point(177, 317);
            this.txtLocation.Margin = new System.Windows.Forms.Padding(4);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(297, 26);
            this.txtLocation.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtLocation.TabIndex = 7;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(177, 439);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 31);
            this.btnSave.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "保存";
            this.btnSave.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // rbtShelf
            // 
            this.rbtShelf.AutoSize = true;
            this.rbtShelf.Checked = true;
            this.rbtShelf.Location = new System.Drawing.Point(538, 63);
            this.rbtShelf.Margin = new System.Windows.Forms.Padding(4);
            this.rbtShelf.Name = "rbtShelf";
            this.rbtShelf.Size = new System.Drawing.Size(74, 20);
            this.rbtShelf.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rbtShelf.TabIndex = 6;
            this.rbtShelf.TabStop = true;
            this.rbtShelf.Text = "冷冻架";
            this.rbtShelf.UseVisualStyleBackColor = true;
            this.rbtShelf.Visible = false;
            // 
            // rbtIceBox
            // 
            this.rbtIceBox.AutoSize = true;
            this.rbtIceBox.Location = new System.Drawing.Point(422, 66);
            this.rbtIceBox.Margin = new System.Windows.Forms.Padding(4);
            this.rbtIceBox.Name = "rbtIceBox";
            this.rbtIceBox.Size = new System.Drawing.Size(74, 20);
            this.rbtIceBox.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rbtIceBox.TabIndex = 5;
            this.rbtIceBox.Text = "冰箱层";
            this.rbtIceBox.UseVisualStyleBackColor = true;
            this.rbtIceBox.Visible = false;
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.Location = new System.Drawing.Point(312, 66);
            this.neuLabel6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(88, 16);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 80;
            this.neuLabel6.Text = "存放容器：";
            this.neuLabel6.Visible = false;
            // 
            // cmbDiseaseType
            // 
            this.cmbDiseaseType.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbDiseaseType.FormattingEnabled = true;
            this.cmbDiseaseType.IsEnter2Tab = false;
            this.cmbDiseaseType.IsFlat = false;
            this.cmbDiseaseType.IsLike = true;
            this.cmbDiseaseType.IsListOnly = false;
            this.cmbDiseaseType.IsPopForm = true;
            this.cmbDiseaseType.IsShowCustomerList = false;
            this.cmbDiseaseType.IsShowID = false;
            this.cmbDiseaseType.Location = new System.Drawing.Point(177, 150);
            this.cmbDiseaseType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDiseaseType.Name = "cmbDiseaseType";
            this.cmbDiseaseType.PopForm = null;
            this.cmbDiseaseType.ShowCustomerList = false;
            this.cmbDiseaseType.ShowID = false;
            this.cmbDiseaseType.Size = new System.Drawing.Size(180, 24);
            this.cmbDiseaseType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbDiseaseType.TabIndex = 4;
            this.cmbDiseaseType.Tag = "";
            this.cmbDiseaseType.ToolBarUse = false;
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Location = new System.Drawing.Point(85, 153);
            this.neuLabel5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(88, 16);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 78;
            this.neuLabel5.Text = "病 　 种：";
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point(85, 25);
            this.neuLabel4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(88, 16);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 77;
            this.neuLabel4.Text = "规    格：";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(85, 372);
            this.neuLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(88, 16);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 74;
            this.neuLabel2.Text = "条 形 码：";
            // 
            // txtComment
            // 
            this.txtComment.IsEnter2Tab = false;
            this.txtComment.Location = new System.Drawing.Point(177, 268);
            this.txtComment.Margin = new System.Windows.Forms.Padding(4);
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(180, 26);
            this.txtComment.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtComment.TabIndex = 9;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(85, 273);
            this.neuLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(88, 16);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 72;
            this.neuLabel1.Text = "名    称：";
            // 
            // btnBrowseLoc
            // 
            this.btnBrowseLoc.Location = new System.Drawing.Point(464, 110);
            this.btnBrowseLoc.Margin = new System.Windows.Forms.Padding(4);
            this.btnBrowseLoc.Name = "btnBrowseLoc";
            this.btnBrowseLoc.Size = new System.Drawing.Size(100, 31);
            this.btnBrowseLoc.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnBrowseLoc.TabIndex = 114;
            this.btnBrowseLoc.Text = "位置浏览";
            this.btnBrowseLoc.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnBrowseLoc.UseVisualStyleBackColor = true;
            this.btnBrowseLoc.Visible = false;
            this.btnBrowseLoc.Click += new System.EventHandler(this.btnBrowseLoc_Click);
            // 
            // txtBoxCode
            // 
            this.txtBoxCode.IsEnter2Tab = false;
            this.txtBoxCode.Location = new System.Drawing.Point(177, 368);
            this.txtBoxCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtBoxCode.Name = "txtBoxCode";
            this.txtBoxCode.Size = new System.Drawing.Size(180, 26);
            this.txtBoxCode.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtBoxCode.TabIndex = 8;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(388, 377);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(100, 31);
            this.btnPrint.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnPrint.TabIndex = 115;
            this.btnPrint.Text = "打印";
            this.btnPrint.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Visible = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnDet
            // 
            this.btnDet.Location = new System.Drawing.Point(351, 439);
            this.btnDet.Name = "btnDet";
            this.btnDet.Size = new System.Drawing.Size(137, 31);
            this.btnDet.TabIndex = 116;
            this.btnDet.Text = "标本盒位置详情";
            this.btnDet.UseVisualStyleBackColor = true;
            this.btnDet.Click += new System.EventHandler(this.btnDet_Click);
            // 
            // chk863
            // 
            this.chk863.AutoSize = true;
            this.chk863.ForeColor = System.Drawing.Color.Red;
            this.chk863.Location = new System.Drawing.Point(178, 231);
            this.chk863.Name = "chk863";
            this.chk863.Size = new System.Drawing.Size(83, 20);
            this.chk863.TabIndex = 117;
            this.chk863.Text = "863专用";
            this.chk863.UseVisualStyleBackColor = true;
            this.chk863.Visible = false;
            this.chk863.CheckedChanged += new System.EventHandler(this.chk863_CheckedChanged);
            // 
            // chk115
            // 
            this.chk115.AutoSize = true;
            this.chk115.ForeColor = System.Drawing.Color.Red;
            this.chk115.Location = new System.Drawing.Point(268, 231);
            this.chk115.Name = "chk115";
            this.chk115.Size = new System.Drawing.Size(83, 20);
            this.chk115.TabIndex = 118;
            this.chk115.Text = "115专用";
            this.chk115.UseVisualStyleBackColor = true;
            this.chk115.Visible = false;
            this.chk115.CheckedChanged += new System.EventHandler(this.chk115_CheckedChanged);
            // 
            // btnProject
            // 
            this.btnProject.Location = new System.Drawing.Point(300, 190);
            this.btnProject.Margin = new System.Windows.Forms.Padding(4);
            this.btnProject.Name = "btnProject";
            this.btnProject.Size = new System.Drawing.Size(100, 31);
            this.btnProject.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnProject.TabIndex = 119;
            this.btnProject.Text = "特定项目";
            this.btnProject.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnProject.UseVisualStyleBackColor = true;
            this.btnProject.Click += new System.EventHandler(this.btnProject_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(85, 192);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 121;
            this.label1.Text = "打印份数:";
            // 
            // nudCopy
            // 
            this.nudCopy.Location = new System.Drawing.Point(178, 190);
            this.nudCopy.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCopy.Name = "nudCopy";
            this.nudCopy.Size = new System.Drawing.Size(62, 26);
            this.nudCopy.TabIndex = 120;
            this.nudCopy.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // frmSpecBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(628, 496);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudCopy);
            this.Controls.Add(this.btnProject);
            this.Controls.Add(this.chk115);
            this.Controls.Add(this.chk863);
            this.Controls.Add(this.btnDet);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnBrowseLoc);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cmbSpecType);
            this.Controls.Add(this.neuLabel3);
            this.Controls.Add(this.cmbOrgOrBlood);
            this.Controls.Add(this.neuLabel8);
            this.Controls.Add(this.cmbBoxSpec);
            this.Controls.Add(this.neuLabel7);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.rbtShelf);
            this.Controls.Add(this.rbtIceBox);
            this.Controls.Add(this.neuLabel6);
            this.Controls.Add(this.cmbDiseaseType);
            this.Controls.Add(this.neuLabel5);
            this.Controls.Add(this.neuLabel4);
            this.Controls.Add(this.txtBoxCode);
            this.Controls.Add(this.neuLabel2);
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.neuLabel1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSpecBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加标本盒";
            this.Load += new System.EventHandler(this.frmSpecBox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudCopy)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuButton btnAdd;
        private System.Windows.Forms.ComboBox cmbSpecType;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private System.Windows.Forms.ComboBox cmbOrgOrBlood;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel8;
        private System.Windows.Forms.ComboBox cmbBoxSpec;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel7;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtLocation;
        private FS.FrameWork.WinForms.Controls.NeuButton btnSave;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rbtShelf;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rbtIceBox;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDiseaseType;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtComment;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuButton btnBrowseLoc;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtBoxCode;
        private FS.FrameWork.WinForms.Controls.NeuButton btnPrint;
        private System.Windows.Forms.Button btnDet;
        private System.Windows.Forms.CheckBox chk863;
        private System.Windows.Forms.CheckBox chk115;
        private FS.FrameWork.WinForms.Controls.NeuButton btnProject;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudCopy;

    }
}