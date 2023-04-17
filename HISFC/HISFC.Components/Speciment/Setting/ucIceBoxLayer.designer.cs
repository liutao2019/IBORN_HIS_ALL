namespace FS.HISFC.Components.Speciment.Setting
{
    partial class ucIceBoxLayer
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
            this.grpIcebox = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.cmbSpecType = new System.Windows.Forms.ComboBox();
            this.neuLabel7 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbOrgOrBlood = new System.Windows.Forms.ComboBox();
            this.neuLabel8 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbDisType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grpSpec = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.cmbSpec = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nudHeight = new FS.FrameWork.WinForms.Controls.NeuNumericUpDown();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nudRow = new FS.FrameWork.WinForms.Controls.NeuNumericUpDown();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nudCol = new FS.FrameWork.WinForms.Controls.NeuNumericUpDown();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.grpShelf = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuLabel9 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nudShelfCount = new FS.FrameWork.WinForms.Controls.NeuNumericUpDown();
            this.cmbShelf = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.rbtSpec = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.rbtShelf = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.flpChk = new System.Windows.Forms.FlowLayoutPanel();
            this.grpIcebox.SuspendLayout();
            this.grpSpec.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCol)).BeginInit();
            this.grpShelf.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudShelfCount)).BeginInit();
            this.SuspendLayout();
            // 
            // grpIcebox
            // 
            this.grpIcebox.Controls.Add(this.cmbSpecType);
            this.grpIcebox.Controls.Add(this.neuLabel7);
            this.grpIcebox.Controls.Add(this.cmbOrgOrBlood);
            this.grpIcebox.Controls.Add(this.neuLabel8);
            this.grpIcebox.Controls.Add(this.cmbDisType);
            this.grpIcebox.Controls.Add(this.label1);
            this.grpIcebox.Controls.Add(this.grpSpec);
            this.grpIcebox.Controls.Add(this.grpShelf);
            this.grpIcebox.Controls.Add(this.rbtSpec);
            this.grpIcebox.Controls.Add(this.rbtShelf);
            this.grpIcebox.Location = new System.Drawing.Point(8, 61);
            this.grpIcebox.Margin = new System.Windows.Forms.Padding(4);
            this.grpIcebox.Name = "grpIcebox";
            this.grpIcebox.Padding = new System.Windows.Forms.Padding(4);
            this.grpIcebox.Size = new System.Drawing.Size(445, 393);
            this.grpIcebox.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.grpIcebox.TabIndex = 2;
            this.grpIcebox.TabStop = false;
            this.grpIcebox.Text = "冰箱层设置";
            // 
            // cmbSpecType
            // 
            this.cmbSpecType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpecType.FormattingEnabled = true;
            this.cmbSpecType.Location = new System.Drawing.Point(308, 29);
            this.cmbSpecType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSpecType.Name = "cmbSpecType";
            this.cmbSpecType.Size = new System.Drawing.Size(132, 24);
            this.cmbSpecType.TabIndex = 18;
            // 
            // neuLabel7
            // 
            this.neuLabel7.AutoSize = true;
            this.neuLabel7.Location = new System.Drawing.Point(213, 33);
            this.neuLabel7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel7.Name = "neuLabel7";
            this.neuLabel7.Size = new System.Drawing.Size(88, 16);
            this.neuLabel7.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel7.TabIndex = 17;
            this.neuLabel7.Text = "标本类型：";
            // 
            // cmbOrgOrBlood
            // 
            this.cmbOrgOrBlood.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrgOrBlood.FormattingEnabled = true;
            this.cmbOrgOrBlood.Location = new System.Drawing.Point(100, 29);
            this.cmbOrgOrBlood.Margin = new System.Windows.Forms.Padding(4);
            this.cmbOrgOrBlood.Name = "cmbOrgOrBlood";
            this.cmbOrgOrBlood.Size = new System.Drawing.Size(84, 24);
            this.cmbOrgOrBlood.TabIndex = 16;
            this.cmbOrgOrBlood.SelectedIndexChanged += new System.EventHandler(this.cmbOrgOrBlood_SelectedIndexChanged);
            // 
            // neuLabel8
            // 
            this.neuLabel8.AutoSize = true;
            this.neuLabel8.Location = new System.Drawing.Point(8, 33);
            this.neuLabel8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel8.Name = "neuLabel8";
            this.neuLabel8.Size = new System.Drawing.Size(88, 16);
            this.neuLabel8.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel8.TabIndex = 15;
            this.neuLabel8.Text = "标本种类：";
            // 
            // cmbDisType
            // 
            this.cmbDisType.FormattingEnabled = true;
            this.cmbDisType.Location = new System.Drawing.Point(102, 70);
            this.cmbDisType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDisType.Name = "cmbDisType";
            this.cmbDisType.Size = new System.Drawing.Size(183, 24);
            this.cmbDisType.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 74);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "病种类型：";
            // 
            // grpSpec
            // 
            this.grpSpec.Controls.Add(this.cmbSpec);
            this.grpSpec.Controls.Add(this.neuLabel6);
            this.grpSpec.Controls.Add(this.nudHeight);
            this.grpSpec.Controls.Add(this.neuLabel5);
            this.grpSpec.Controls.Add(this.nudRow);
            this.grpSpec.Controls.Add(this.neuLabel3);
            this.grpSpec.Controls.Add(this.nudCol);
            this.grpSpec.Controls.Add(this.neuLabel4);
            this.grpSpec.Location = new System.Drawing.Point(11, 280);
            this.grpSpec.Margin = new System.Windows.Forms.Padding(4);
            this.grpSpec.Name = "grpSpec";
            this.grpSpec.Padding = new System.Windows.Forms.Padding(4);
            this.grpSpec.Size = new System.Drawing.Size(408, 101);
            this.grpSpec.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.grpSpec.TabIndex = 7;
            this.grpSpec.TabStop = false;
            // 
            // cmbSpec
            // 
            //this.cmbSpec.A = false;
            this.cmbSpec.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbSpec.FormattingEnabled = true;
            this.cmbSpec.IsFlat = true;
            this.cmbSpec.IsLike = true;
            this.cmbSpec.Location = new System.Drawing.Point(115, 12);
            this.cmbSpec.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSpec.Name = "cmbSpec";
            this.cmbSpec.PopForm = null;
            this.cmbSpec.ShowCustomerList = false;
            this.cmbSpec.ShowID = false;
            this.cmbSpec.Size = new System.Drawing.Size(241, 24);
            this.cmbSpec.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbSpec.TabIndex = 6;
            this.cmbSpec.Tag = "";
            this.cmbSpec.ToolBarUse = false;
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.Location = new System.Drawing.Point(16, 23);
            this.neuLabel6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(72, 16);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 10;
            this.neuLabel6.Text = "规　格：";
            // 
            // nudHeight
            // 
            this.nudHeight.Location = new System.Drawing.Point(335, 60);
            this.nudHeight.Margin = new System.Windows.Forms.Padding(4);
            this.nudHeight.Name = "nudHeight";
            this.nudHeight.Size = new System.Drawing.Size(53, 26);
            this.nudHeight.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nudHeight.TabIndex = 9;
            this.nudHeight.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Location = new System.Drawing.Point(264, 65);
            this.neuLabel5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(72, 16);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 8;
            this.neuLabel5.Text = "高　度：";
            // 
            // nudRow
            // 
            this.nudRow.Location = new System.Drawing.Point(80, 60);
            this.nudRow.Margin = new System.Windows.Forms.Padding(4);
            this.nudRow.Name = "nudRow";
            this.nudRow.Size = new System.Drawing.Size(51, 26);
            this.nudRow.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nudRow.TabIndex = 7;
            this.nudRow.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(13, 65);
            this.neuLabel3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(72, 16);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 6;
            this.neuLabel3.Text = "行　数：";
            // 
            // nudCol
            // 
            this.nudCol.Location = new System.Drawing.Point(204, 60);
            this.nudCol.Margin = new System.Windows.Forms.Padding(4);
            this.nudCol.Name = "nudCol";
            this.nudCol.Size = new System.Drawing.Size(52, 26);
            this.nudCol.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nudCol.TabIndex = 5;
            this.nudCol.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point(139, 65);
            this.neuLabel4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(72, 16);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 4;
            this.neuLabel4.Text = "列　数：";
            // 
            // grpShelf
            // 
            this.grpShelf.Controls.Add(this.neuLabel9);
            this.grpShelf.Controls.Add(this.neuLabel1);
            this.grpShelf.Controls.Add(this.nudShelfCount);
            this.grpShelf.Controls.Add(this.cmbShelf);
            this.grpShelf.Controls.Add(this.neuLabel2);
            this.grpShelf.Location = new System.Drawing.Point(11, 131);
            this.grpShelf.Margin = new System.Windows.Forms.Padding(4);
            this.grpShelf.Name = "grpShelf";
            this.grpShelf.Padding = new System.Windows.Forms.Padding(4);
            this.grpShelf.Size = new System.Drawing.Size(408, 113);
            this.grpShelf.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.grpShelf.TabIndex = 6;
            this.grpShelf.TabStop = false;
            // 
            // neuLabel9
            // 
            this.neuLabel9.AutoSize = true;
            this.neuLabel9.Location = new System.Drawing.Point(284, 28);
            this.neuLabel9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel9.Name = "neuLabel9";
            this.neuLabel9.Size = new System.Drawing.Size(104, 16);
            this.neuLabel9.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel9.TabIndex = 16;
            this.neuLabel9.Text = "(深度×高度)";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(15, 28);
            this.neuLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(72, 16);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 2;
            this.neuLabel1.Text = "规　格：";
            // 
            // nudShelfCount
            // 
            this.nudShelfCount.Location = new System.Drawing.Point(116, 65);
            this.nudShelfCount.Margin = new System.Windows.Forms.Padding(4);
            this.nudShelfCount.Name = "nudShelfCount";
            this.nudShelfCount.Size = new System.Drawing.Size(160, 26);
            this.nudShelfCount.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nudShelfCount.TabIndex = 5;
            this.nudShelfCount.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // cmbShelf
            // 
            //this.cmbShelf.A = false;
            this.cmbShelf.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbShelf.FormattingEnabled = true;
            this.cmbShelf.IsFlat = true;
            this.cmbShelf.IsLike = true;
            this.cmbShelf.Location = new System.Drawing.Point(116, 23);
            this.cmbShelf.Margin = new System.Windows.Forms.Padding(4);
            this.cmbShelf.Name = "cmbShelf";
            this.cmbShelf.PopForm = null;
            this.cmbShelf.ShowCustomerList = false;
            this.cmbShelf.ShowID = false;
            this.cmbShelf.Size = new System.Drawing.Size(160, 24);
            this.cmbShelf.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbShelf.TabIndex = 3;
            this.cmbShelf.Tag = "";
            this.cmbShelf.ToolBarUse = false;
            this.cmbShelf.SelectedIndexChanged += new System.EventHandler(this.cmbShelf_SelectedIndexChanged);
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(15, 71);
            this.neuLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(72, 16);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 4;
            this.neuLabel2.Text = "数　量：";
            // 
            // rbtSpec
            // 
            this.rbtSpec.AutoSize = true;
            this.rbtSpec.Enabled = false;
            this.rbtSpec.Location = new System.Drawing.Point(11, 252);
            this.rbtSpec.Margin = new System.Windows.Forms.Padding(4);
            this.rbtSpec.Name = "rbtSpec";
            this.rbtSpec.Size = new System.Drawing.Size(74, 20);
            this.rbtSpec.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rbtSpec.TabIndex = 1;
            this.rbtSpec.Text = "标本盒";
            this.rbtSpec.UseVisualStyleBackColor = true;
            // 
            // rbtShelf
            // 
            this.rbtShelf.AutoSize = true;
            this.rbtShelf.Checked = true;
            this.rbtShelf.Location = new System.Drawing.Point(11, 103);
            this.rbtShelf.Margin = new System.Windows.Forms.Padding(4);
            this.rbtShelf.Name = "rbtShelf";
            this.rbtShelf.Size = new System.Drawing.Size(74, 20);
            this.rbtShelf.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rbtShelf.TabIndex = 0;
            this.rbtShelf.TabStop = true;
            this.rbtShelf.Text = "冷冻架";
            this.rbtShelf.UseVisualStyleBackColor = true;
            this.rbtShelf.CheckedChanged += new System.EventHandler(this.rbtShelf_CheckedChanged);
            // 
            // flpChk
            // 
            this.flpChk.AutoScroll = true;
            this.flpChk.AutoSize = true;
            this.flpChk.Location = new System.Drawing.Point(8, 4);
            this.flpChk.Margin = new System.Windows.Forms.Padding(4);
            this.flpChk.Name = "flpChk";
            this.flpChk.Size = new System.Drawing.Size(560, 49);
            this.flpChk.TabIndex = 3;
            // 
            // ucIceBoxLayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flpChk);
            this.Controls.Add(this.grpIcebox);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ucIceBoxLayer";
            this.Size = new System.Drawing.Size(572, 464);
            this.Load += new System.EventHandler(this.ucIceBoxLayer_Load);
            this.grpIcebox.ResumeLayout(false);
            this.grpIcebox.PerformLayout();
            this.grpSpec.ResumeLayout(false);
            this.grpSpec.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCol)).EndInit();
            this.grpShelf.ResumeLayout(false);
            this.grpShelf.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudShelfCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox grpIcebox;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox grpSpec;
        private FS.FrameWork.WinForms.Controls.NeuNumericUpDown nudHeight;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.NeuNumericUpDown nudRow;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuNumericUpDown nudCol;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox grpShelf;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuNumericUpDown nudShelfCount;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbShelf;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rbtSpec;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rbtShelf;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbSpec;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        private System.Windows.Forms.FlowLayoutPanel flpChk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDisType;
        private System.Windows.Forms.ComboBox cmbSpecType;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel7;
        private System.Windows.Forms.ComboBox cmbOrgOrBlood;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel8;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel9;

    }
}
