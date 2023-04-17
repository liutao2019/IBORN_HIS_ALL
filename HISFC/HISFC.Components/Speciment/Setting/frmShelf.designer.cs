namespace FS.HISFC.Components.Speciment.Setting
{
    partial class frmShelf
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
            this.cmbBoxSpec = new System.Windows.Forms.ComboBox();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.btnSave = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.nudHeight = new FS.FrameWork.WinForms.Controls.NeuNumericUpDown();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nudRow = new FS.FrameWork.WinForms.Controls.NeuNumericUpDown();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.btnAdd = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuLabel9 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.txtIceBoxType = new FS.FrameWork.WinForms.Controls.NeuListTextBox();
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRow)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbBoxSpec
            // 
            this.cmbBoxSpec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxSpec.FormattingEnabled = true;
            this.cmbBoxSpec.Location = new System.Drawing.Point(191, 231);
            this.cmbBoxSpec.Margin = new System.Windows.Forms.Padding(4);
            this.cmbBoxSpec.Name = "cmbBoxSpec";
            this.cmbBoxSpec.Size = new System.Drawing.Size(160, 24);
            this.cmbBoxSpec.TabIndex = 5;
            this.cmbBoxSpec.SelectedIndexChanged += new System.EventHandler(this.cmbBoxSpec_SelectedIndexChanged);
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.ForeColor = System.Drawing.Color.Red;
            this.neuLabel5.Location = new System.Drawing.Point(80, 236);
            this.neuLabel5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(104, 16);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 39;
            this.neuLabel5.Text = "标本盒规格：";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(291, 343);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 31);
            this.btnSave.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "保存";
            this.btnSave.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // nudHeight
            // 
            this.nudHeight.Location = new System.Drawing.Point(191, 181);
            this.nudHeight.Margin = new System.Windows.Forms.Padding(4);
            this.nudHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudHeight.Name = "nudHeight";
            this.nudHeight.Size = new System.Drawing.Size(160, 26);
            this.nudHeight.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nudHeight.TabIndex = 4;
            this.nudHeight.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.ForeColor = System.Drawing.Color.Red;
            this.neuLabel4.Location = new System.Drawing.Point(80, 184);
            this.neuLabel4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(72, 16);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 32;
            this.neuLabel4.Text = "高  度：";
            // 
            // nudRow
            // 
            this.nudRow.Location = new System.Drawing.Point(189, 125);
            this.nudRow.Margin = new System.Windows.Forms.Padding(4);
            this.nudRow.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudRow.Name = "nudRow";
            this.nudRow.Size = new System.Drawing.Size(161, 26);
            this.nudRow.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nudRow.TabIndex = 3;
            this.nudRow.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.ForeColor = System.Drawing.Color.Red;
            this.neuLabel2.Location = new System.Drawing.Point(80, 128);
            this.neuLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(72, 16);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 2;
            this.neuLabel2.Text = "深　度：";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(189, 75);
            this.txtName.Margin = new System.Windows.Forms.Padding(4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(160, 26);
            this.txtName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtName.TabIndex = 2;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(80, 79);
            this.neuLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(72, 16);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "名  称：";
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(80, 29);
            this.neuLabel3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(88, 16);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 42;
            this.neuLabel3.Text = "冰箱类型：";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(119, 343);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 31);
            this.btnAdd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "添加";
            this.btnAdd.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // neuLabel9
            // 
            this.neuLabel9.AutoSize = true;
            this.neuLabel9.Location = new System.Drawing.Point(81, 287);
            this.neuLabel9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel9.Name = "neuLabel9";
            this.neuLabel9.Size = new System.Drawing.Size(56, 16);
            this.neuLabel9.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel9.TabIndex = 89;
            this.neuLabel9.Text = "备注：";
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(189, 275);
            this.txtComment.Margin = new System.Windows.Forms.Padding(4);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(160, 43);
            this.txtComment.TabIndex = 88;
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
            this.txtIceBoxType.Location = new System.Drawing.Point(189, 17);
            this.txtIceBoxType.Margin = new System.Windows.Forms.Padding(4);
            this.txtIceBoxType.Name = "txtIceBoxType";
            this.txtIceBoxType.OmitFilter = true;
            this.txtIceBoxType.SelectedItem = null;
            this.txtIceBoxType.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtIceBoxType.ShowID = true;
            this.txtIceBoxType.Size = new System.Drawing.Size(160, 26);
            this.txtIceBoxType.TabIndex = 112;
            this.txtIceBoxType.TextChanged += new System.EventHandler(this.txtIceBoxType_TextChanged);
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.Location = new System.Drawing.Point(359, 234);
            this.neuLabel6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(72, 16);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 113;
            this.neuLabel6.Text = "(行×列)";
            // 
            // frmShelf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(467, 403);
            this.Controls.Add(this.neuLabel6);
            this.Controls.Add(this.txtIceBoxType);
            this.Controls.Add(this.neuLabel9);
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.neuLabel3);
            this.Controls.Add(this.cmbBoxSpec);
            this.Controls.Add(this.neuLabel5);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.nudHeight);
            this.Controls.Add(this.neuLabel4);
            this.Controls.Add(this.nudRow);
            this.Controls.Add(this.neuLabel2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.neuLabel1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmShelf";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "冻存架规格设置";
            this.Load += new System.EventHandler(this.frmShelf_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtName;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuNumericUpDown nudRow;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuNumericUpDown nudHeight;
        private FS.FrameWork.WinForms.Controls.NeuButton btnSave;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private System.Windows.Forms.ComboBox cmbBoxSpec;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuButton btnAdd;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel9;
        private System.Windows.Forms.TextBox txtComment;
        private FS.FrameWork.WinForms.Controls.NeuListTextBox txtIceBoxType;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
    }
}