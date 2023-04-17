namespace FS.HISFC.Components.Speciment.Setting
{
    partial class frmSpecShelf
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
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbSpecType = new System.Windows.Forms.ComboBox();
            this.neuLabel7 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbOrgOrBlood = new System.Windows.Forms.ComboBox();
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbShelf = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.btnAdd = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnSave = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuLabel9 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtLocate = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtBoxCode = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.btnPrint = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.cmbDiseaseType = new FS.FrameWork.WinForms.Controls.NeuComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtIceBoxType = new FS.FrameWork.WinForms.Controls.NeuListTextBox();
            this.btnBrowseLoc = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.SuspendLayout();
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(91, 36);
            this.neuLabel3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(88, 16);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 44;
            this.neuLabel3.Text = "冰箱类型：";
            // 
            // cmbSpecType
            // 
            this.cmbSpecType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpecType.FormattingEnabled = true;
            this.cmbSpecType.Location = new System.Drawing.Point(200, 120);
            this.cmbSpecType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSpecType.Name = "cmbSpecType";
            this.cmbSpecType.Size = new System.Drawing.Size(124, 24);
            this.cmbSpecType.TabIndex = 95;
            // 
            // neuLabel7
            // 
            this.neuLabel7.AutoSize = true;
            this.neuLabel7.ForeColor = System.Drawing.Color.Red;
            this.neuLabel7.Location = new System.Drawing.Point(91, 125);
            this.neuLabel7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel7.Name = "neuLabel7";
            this.neuLabel7.Size = new System.Drawing.Size(88, 16);
            this.neuLabel7.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel7.TabIndex = 94;
            this.neuLabel7.Text = "标本类型：";
            // 
            // cmbOrgOrBlood
            // 
            this.cmbOrgOrBlood.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrgOrBlood.FormattingEnabled = true;
            this.cmbOrgOrBlood.Location = new System.Drawing.Point(200, 76);
            this.cmbOrgOrBlood.Margin = new System.Windows.Forms.Padding(4);
            this.cmbOrgOrBlood.Name = "cmbOrgOrBlood";
            this.cmbOrgOrBlood.Size = new System.Drawing.Size(124, 24);
            this.cmbOrgOrBlood.TabIndex = 93;
            this.cmbOrgOrBlood.SelectedIndexChanged += new System.EventHandler(this.cmbOrgOrBlood_SelectedIndexChanged);
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.ForeColor = System.Drawing.Color.Red;
            this.neuLabel6.Location = new System.Drawing.Point(91, 80);
            this.neuLabel6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(88, 16);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 92;
            this.neuLabel6.Text = "标本种类：";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.ForeColor = System.Drawing.Color.Red;
            this.neuLabel1.Location = new System.Drawing.Point(91, 233);
            this.neuLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(104, 16);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 96;
            this.neuLabel1.Text = "冻存架规格：";
            // 
            // cmbShelf
            // 
            this.cmbShelf.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbShelf.FormattingEnabled = true;
            this.cmbShelf.IsFlat = true;
            this.cmbShelf.IsLike = true;
            this.cmbShelf.Location = new System.Drawing.Point(200, 228);
            this.cmbShelf.Margin = new System.Windows.Forms.Padding(4);
            this.cmbShelf.Name = "cmbShelf";
            this.cmbShelf.PopForm = null;
            this.cmbShelf.ShowCustomerList = false;
            this.cmbShelf.ShowID = false;
            this.cmbShelf.Size = new System.Drawing.Size(241, 24);
            this.cmbShelf.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbShelf.TabIndex = 97;
            this.cmbShelf.Tag = "";
            this.cmbShelf.ToolBarUse = false;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(93, 448);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 31);
            this.btnAdd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnAdd.TabIndex = 100;
            this.btnAdd.Text = "添加";
            this.btnAdd.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(260, 448);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 31);
            this.btnSave.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnSave.TabIndex = 101;
            this.btnSave.Text = "保存";
            this.btnSave.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // neuLabel9
            // 
            this.neuLabel9.AutoSize = true;
            this.neuLabel9.Location = new System.Drawing.Point(91, 403);
            this.neuLabel9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel9.Name = "neuLabel9";
            this.neuLabel9.Size = new System.Drawing.Size(72, 16);
            this.neuLabel9.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel9.TabIndex = 103;
            this.neuLabel9.Text = "备  注：";
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(199, 389);
            this.txtComment.Margin = new System.Windows.Forms.Padding(4);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(160, 43);
            this.txtComment.TabIndex = 102;
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point(91, 287);
            this.neuLabel4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(88, 16);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 105;
            this.neuLabel4.Text = "存放位置：";
            // 
            // txtLocate
            // 
            this.txtLocate.Location = new System.Drawing.Point(200, 283);
            this.txtLocate.Margin = new System.Windows.Forms.Padding(4);
            this.txtLocate.Name = "txtLocate";
            this.txtLocate.Size = new System.Drawing.Size(169, 26);
            this.txtLocate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtLocate.TabIndex = 104;
            // 
            // txtBoxCode
            // 
            this.txtBoxCode.Location = new System.Drawing.Point(200, 339);
            this.txtBoxCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtBoxCode.Name = "txtBoxCode";
            this.txtBoxCode.Size = new System.Drawing.Size(152, 26);
            this.txtBoxCode.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtBoxCode.TabIndex = 107;
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Location = new System.Drawing.Point(91, 343);
            this.neuLabel5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(72, 16);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 106;
            this.neuLabel5.Text = "条形码：";
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(361, 336);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(100, 31);
            this.btnPrint.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnPrint.TabIndex = 108;
            this.btnPrint.Text = "打印条码";
            this.btnPrint.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnPrint.UseVisualStyleBackColor = true;
            // 
            // cmbDiseaseType
            // 
            this.cmbDiseaseType.FormattingEnabled = true;
            this.cmbDiseaseType.Location = new System.Drawing.Point(199, 173);
            this.cmbDiseaseType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDiseaseType.Name = "cmbDiseaseType";
            this.cmbDiseaseType.Size = new System.Drawing.Size(183, 24);
            this.cmbDiseaseType.TabIndex = 110;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(91, 177);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 109;
            this.label1.Text = "病种类型：";
            // 
            // txtIceBoxType
            // 
            this.txtIceBoxType.EnterVisiable = false;
            this.txtIceBoxType.IsFind = false;
            //this.txtIceBoxType.IsSelctNone = true;
            //this.txtIceBoxType.IsSendToNext = false;
            //this.txtIceBoxType.IsShowID = false;
            //this.txtIceBoxType.ItemText = "";
            this.txtIceBoxType.ListBoxHeight = 100;
            //this.txtIceBoxType.ListBoxVisible = false;
            this.txtIceBoxType.ListBoxWidth = 150;
            this.txtIceBoxType.Location = new System.Drawing.Point(199, 32);
            this.txtIceBoxType.Margin = new System.Windows.Forms.Padding(4);
            this.txtIceBoxType.Name = "txtIceBoxType";
            this.txtIceBoxType.OmitFilter = true;
            this.txtIceBoxType.SelectedItem = null;
            //this.txtIceBoxType.SetListFont = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtIceBoxType.ShowID = true;
            this.txtIceBoxType.Size = new System.Drawing.Size(160, 26);
            this.txtIceBoxType.TabIndex = 111;
            // 
            // btnBrowseLoc
            // 
            this.btnBrowseLoc.Location = new System.Drawing.Point(387, 283);
            this.btnBrowseLoc.Margin = new System.Windows.Forms.Padding(4);
            this.btnBrowseLoc.Name = "btnBrowseLoc";
            this.btnBrowseLoc.Size = new System.Drawing.Size(100, 31);
            this.btnBrowseLoc.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnBrowseLoc.TabIndex = 113;
            this.btnBrowseLoc.Text = "位置浏览";
            this.btnBrowseLoc.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnBrowseLoc.UseVisualStyleBackColor = true;
            this.btnBrowseLoc.Visible = false;
            this.btnBrowseLoc.Click += new System.EventHandler(this.btnBrowseLoc_Click);
            // 
            // frmSpecShelf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(500, 507);
            this.Controls.Add(this.btnBrowseLoc);
            this.Controls.Add(this.txtIceBoxType);
            this.Controls.Add(this.cmbDiseaseType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.txtBoxCode);
            this.Controls.Add(this.neuLabel5);
            this.Controls.Add(this.neuLabel4);
            this.Controls.Add(this.txtLocate);
            this.Controls.Add(this.neuLabel9);
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.neuLabel1);
            this.Controls.Add(this.cmbShelf);
            this.Controls.Add(this.cmbSpecType);
            this.Controls.Add(this.neuLabel7);
            this.Controls.Add(this.cmbOrgOrBlood);
            this.Controls.Add(this.neuLabel6);
            this.Controls.Add(this.neuLabel3);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSpecShelf";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "冻存架添加";
            this.Load += new System.EventHandler(this.frmSpecShelf_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private System.Windows.Forms.ComboBox cmbSpecType;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel7;
        private System.Windows.Forms.ComboBox cmbOrgOrBlood;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbShelf;
        private FS.FrameWork.WinForms.Controls.NeuButton btnAdd;
        private FS.FrameWork.WinForms.Controls.NeuButton btnSave;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel9;
        private System.Windows.Forms.TextBox txtComment;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtLocate;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtBoxCode;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.NeuButton btnPrint;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDiseaseType;
        private System.Windows.Forms.Label label1;
        private FS.FrameWork.WinForms.Controls.NeuListTextBox txtIceBoxType;
        private FS.FrameWork.WinForms.Controls.NeuButton btnBrowseLoc;
    }
}