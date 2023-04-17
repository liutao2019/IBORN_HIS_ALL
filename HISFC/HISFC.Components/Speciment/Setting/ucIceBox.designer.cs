namespace FS.HISFC.Components.Speciment.Setting
{
    partial class ucIceBox
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
            this.gpBaseInfo = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.cmbSpecType = new System.Windows.Forms.ComboBox();
            this.neuLabel7 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbOrgOrBlood = new System.Windows.Forms.ComboBox();
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel9 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.txtIceBoxType = new FS.FrameWork.WinForms.Controls.NeuListTextBox();
            this.neuLabel8 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtName = new System.Windows.Forms.TextBox();
            this.cmbIceBox = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nudLayerSpec = new FS.FrameWork.WinForms.Controls.NeuNumericUpDown();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nudLayerNum = new FS.FrameWork.WinForms.Controls.NeuNumericUpDown();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tlpLayer = new System.Windows.Forms.TableLayoutPanel();
            this.gpBaseInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLayerSpec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLayerNum)).BeginInit();
            this.SuspendLayout();
            // 
            // gpBaseInfo
            // 
            this.gpBaseInfo.Controls.Add(this.cmbSpecType);
            this.gpBaseInfo.Controls.Add(this.neuLabel7);
            this.gpBaseInfo.Controls.Add(this.cmbOrgOrBlood);
            this.gpBaseInfo.Controls.Add(this.neuLabel6);
            this.gpBaseInfo.Controls.Add(this.neuLabel9);
            this.gpBaseInfo.Controls.Add(this.txtComment);
            this.gpBaseInfo.Controls.Add(this.txtIceBoxType);
            this.gpBaseInfo.Controls.Add(this.neuLabel8);
            this.gpBaseInfo.Controls.Add(this.txtName);
            this.gpBaseInfo.Controls.Add(this.cmbIceBox);
            this.gpBaseInfo.Controls.Add(this.btnOK);
            this.gpBaseInfo.Controls.Add(this.neuLabel4);
            this.gpBaseInfo.Controls.Add(this.nudLayerSpec);
            this.gpBaseInfo.Controls.Add(this.neuLabel5);
            this.gpBaseInfo.Controls.Add(this.neuLabel3);
            this.gpBaseInfo.Controls.Add(this.nudLayerNum);
            this.gpBaseInfo.Controls.Add(this.neuLabel2);
            this.gpBaseInfo.Controls.Add(this.neuLabel1);
            this.gpBaseInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpBaseInfo.Location = new System.Drawing.Point(0, 0);
            this.gpBaseInfo.Margin = new System.Windows.Forms.Padding(4);
            this.gpBaseInfo.Name = "gpBaseInfo";
            this.gpBaseInfo.Padding = new System.Windows.Forms.Padding(4);
            this.gpBaseInfo.Size = new System.Drawing.Size(985, 167);
            this.gpBaseInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gpBaseInfo.TabIndex = 1;
            this.gpBaseInfo.TabStop = false;
            this.gpBaseInfo.Text = "基本信息";
            // 
            // cmbSpecType
            // 
            this.cmbSpecType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpecType.FormattingEnabled = true;
            this.cmbSpecType.Location = new System.Drawing.Point(298, 79);
            this.cmbSpecType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSpecType.Name = "cmbSpecType";
            this.cmbSpecType.Size = new System.Drawing.Size(132, 24);
            this.cmbSpecType.TabIndex = 91;
            this.cmbSpecType.SelectedIndexChanged += new System.EventHandler(this.cmbSpecType_SelectedIndexChanged);
            // 
            // neuLabel7
            // 
            this.neuLabel7.AutoSize = true;
            this.neuLabel7.Location = new System.Drawing.Point(216, 83);
            this.neuLabel7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel7.Name = "neuLabel7";
            this.neuLabel7.Size = new System.Drawing.Size(80, 16);
            this.neuLabel7.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel7.TabIndex = 90;
            this.neuLabel7.Text = "标本类型:";
            // 
            // cmbOrgOrBlood
            // 
            this.cmbOrgOrBlood.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrgOrBlood.FormattingEnabled = true;
            this.cmbOrgOrBlood.Location = new System.Drawing.Point(103, 79);
            this.cmbOrgOrBlood.Margin = new System.Windows.Forms.Padding(4);
            this.cmbOrgOrBlood.Name = "cmbOrgOrBlood";
            this.cmbOrgOrBlood.Size = new System.Drawing.Size(100, 24);
            this.cmbOrgOrBlood.TabIndex = 89;
            this.cmbOrgOrBlood.SelectedIndexChanged += new System.EventHandler(this.cmbOrgOrBlood_SelectedIndexChanged);
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.Location = new System.Drawing.Point(21, 83);
            this.neuLabel6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(80, 16);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 88;
            this.neuLabel6.Text = "标本种类:";
            // 
            // neuLabel9
            // 
            this.neuLabel9.AutoSize = true;
            this.neuLabel9.Location = new System.Drawing.Point(21, 128);
            this.neuLabel9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel9.Name = "neuLabel9";
            this.neuLabel9.Size = new System.Drawing.Size(80, 16);
            this.neuLabel9.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel9.TabIndex = 87;
            this.neuLabel9.Text = "备    注:";
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(103, 125);
            this.txtComment.Margin = new System.Windows.Forms.Padding(4);
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(528, 26);
            this.txtComment.TabIndex = 86;
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
            this.txtIceBoxType.Location = new System.Drawing.Point(103, 31);
            this.txtIceBoxType.Margin = new System.Windows.Forms.Padding(4);
            this.txtIceBoxType.Name = "txtIceBoxType";
            this.txtIceBoxType.OmitFilter = true;
            this.txtIceBoxType.SelectedItem = null;
            this.txtIceBoxType.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtIceBoxType.ShowID = true;
            this.txtIceBoxType.Size = new System.Drawing.Size(100, 26);
            this.txtIceBoxType.TabIndex = 85;
            this.txtIceBoxType.TextChanged += new System.EventHandler(this.txtIceBoxType_TextChanged);
            // 
            // neuLabel8
            // 
            this.neuLabel8.AutoSize = true;
            this.neuLabel8.Location = new System.Drawing.Point(21, 36);
            this.neuLabel8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel8.Name = "neuLabel8";
            this.neuLabel8.Size = new System.Drawing.Size(80, 16);
            this.neuLabel8.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel8.TabIndex = 15;
            this.neuLabel8.Text = "冰箱类型:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(440, 31);
            this.txtName.Margin = new System.Windows.Forms.Padding(4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(113, 26);
            this.txtName.TabIndex = 10;
            // 
            // cmbIceBox
            // 
            this.cmbIceBox.FormattingEnabled = true;
            this.cmbIceBox.Location = new System.Drawing.Point(298, 31);
            this.cmbIceBox.Margin = new System.Windows.Forms.Padding(4);
            this.cmbIceBox.Name = "cmbIceBox";
            this.cmbIceBox.Size = new System.Drawing.Size(134, 24);
            this.cmbIceBox.TabIndex = 9;
            this.cmbIceBox.SelectedIndexChanged += new System.EventHandler(this.cmbIceBox_SelectedIndexChanged);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(657, 125);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 31);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point(881, 36);
            this.neuLabel4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(56, 16);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 7;
            this.neuLabel4.Text = "种规格";
            // 
            // nudLayerSpec
            // 
            this.nudLayerSpec.Location = new System.Drawing.Point(812, 31);
            this.nudLayerSpec.Margin = new System.Windows.Forms.Padding(4);
            this.nudLayerSpec.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.nudLayerSpec.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLayerSpec.Name = "nudLayerSpec";
            this.nudLayerSpec.Size = new System.Drawing.Size(64, 26);
            this.nudLayerSpec.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nudLayerSpec.TabIndex = 6;
            this.nudLayerSpec.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Location = new System.Drawing.Point(723, 36);
            this.neuLabel5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(72, 16);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 5;
            this.neuLabel5.Text = "冰箱中有";
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(672, 36);
            this.neuLabel3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(24, 16);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 4;
            this.neuLabel3.Text = "层";
            // 
            // nudLayerNum
            // 
            this.nudLayerNum.Location = new System.Drawing.Point(605, 31);
            this.nudLayerNum.Margin = new System.Windows.Forms.Padding(4);
            this.nudLayerNum.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.nudLayerNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLayerNum.Name = "nudLayerNum";
            this.nudLayerNum.Size = new System.Drawing.Size(64, 26);
            this.nudLayerNum.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nudLayerNum.TabIndex = 3;
            this.nudLayerNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(580, 36);
            this.neuLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(24, 16);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 2;
            this.neuLabel2.Text = "有";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(216, 36);
            this.neuLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(80, 16);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "名　  称:";
            // 
            // tlpLayer
            // 
            this.tlpLayer.AutoScroll = true;
            this.tlpLayer.ColumnCount = 1;
            this.tlpLayer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpLayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpLayer.Location = new System.Drawing.Point(0, 167);
            this.tlpLayer.Margin = new System.Windows.Forms.Padding(4);
            this.tlpLayer.Name = "tlpLayer";
            this.tlpLayer.RowCount = 1;
            this.tlpLayer.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpLayer.Size = new System.Drawing.Size(985, 726);
            this.tlpLayer.TabIndex = 2;
            this.tlpLayer.Scroll += new System.Windows.Forms.ScrollEventHandler(this.tlpLayer_Scroll);
            // 
            // ucIceBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpLayer);
            this.Controls.Add(this.gpBaseInfo);
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ucIceBox";
            this.Size = new System.Drawing.Size(985, 893);
            this.Load += new System.EventHandler(this.ucIceBox_Load);
            this.gpBaseInfo.ResumeLayout(false);
            this.gpBaseInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLayerSpec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLayerNum)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox gpBaseInfo;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuNumericUpDown nudLayerNum;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuNumericUpDown nudLayerSpec;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TableLayoutPanel tlpLayer;
        private System.Windows.Forms.ComboBox cmbIceBox;
        private System.Windows.Forms.TextBox txtName;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel8;
        private FS.FrameWork.WinForms.Controls.NeuListTextBox txtIceBoxType;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel9;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.ComboBox cmbSpecType;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel7;
        private System.Windows.Forms.ComboBox cmbOrgOrBlood;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
    }
}
