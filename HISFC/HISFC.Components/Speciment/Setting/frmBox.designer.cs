namespace FS.HISFC.Components.Speciment.Setting
{
    partial class frmBox
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
            this.btnSave = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnCancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.nudCol = new FS.FrameWork.WinForms.Controls.NeuNumericUpDown();
            this.nudRow = new FS.FrameWork.WinForms.Controls.NeuNumericUpDown();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbSpec = new System.Windows.Forms.ComboBox();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtComment = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudCol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRow)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(49, 341);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 31);
            this.btnSave.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnSave.TabIndex = 40;
            this.btnSave.Text = "添加";
            this.btnSave.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(212, 341);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 31);
            this.btnCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnCancel.TabIndex = 39;
            this.btnCancel.Text = "取消";
            this.btnCancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // nudCol
            // 
            this.nudCol.Location = new System.Drawing.Point(143, 224);
            this.nudCol.Margin = new System.Windows.Forms.Padding(4);
            this.nudCol.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCol.Name = "nudCol";
            this.nudCol.Size = new System.Drawing.Size(181, 26);
            this.nudCol.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nudCol.TabIndex = 37;
            this.nudCol.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudCol.ValueChanged += new System.EventHandler(this.nudCol_ValueChanged);
            // 
            // nudRow
            // 
            this.nudRow.Location = new System.Drawing.Point(143, 167);
            this.nudRow.Margin = new System.Windows.Forms.Padding(4);
            this.nudRow.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudRow.Name = "nudRow";
            this.nudRow.Size = new System.Drawing.Size(181, 26);
            this.nudRow.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nudRow.TabIndex = 36;
            this.nudRow.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(39, 229);
            this.neuLabel3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(80, 16);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 35;
            this.neuLabel3.Text = " 列  数：";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(39, 172);
            this.neuLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(80, 16);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 34;
            this.neuLabel2.Text = " 行  数：";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(143, 109);
            this.txtName.Margin = new System.Windows.Forms.Padding(4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(180, 26);
            this.txtName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtName.TabIndex = 33;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(47, 115);
            this.neuLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(72, 16);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 32;
            this.neuLabel1.Text = "名  称：";
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point(47, 57);
            this.neuLabel4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(88, 16);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 41;
            this.neuLabel4.Text = "规格名称：";
            // 
            // cmbSpec
            // 
            this.cmbSpec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpec.FormattingEnabled = true;
            this.cmbSpec.Location = new System.Drawing.Point(143, 53);
            this.cmbSpec.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSpec.Name = "cmbSpec";
            this.cmbSpec.Size = new System.Drawing.Size(180, 24);
            this.cmbSpec.TabIndex = 42;
            this.cmbSpec.SelectedIndexChanged += new System.EventHandler(this.cmbSpec_SelectedIndexChanged);
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Location = new System.Drawing.Point(47, 287);
            this.neuLabel5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(72, 16);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 43;
            this.neuLabel5.Text = "备　注：";
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(143, 283);
            this.txtComment.Margin = new System.Windows.Forms.Padding(4);
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(180, 26);
            this.txtComment.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtComment.TabIndex = 44;
            // 
            // frmBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(391, 425);
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.neuLabel5);
            this.Controls.Add(this.cmbSpec);
            this.Controls.Add(this.neuLabel4);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.nudCol);
            this.Controls.Add(this.nudRow);
            this.Controls.Add(this.neuLabel3);
            this.Controls.Add(this.neuLabel2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.neuLabel1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "标本盒规格设置";
            this.Load += new System.EventHandler(this.frmBox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudCol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuNumericUpDown nudCol;
        private FS.FrameWork.WinForms.Controls.NeuNumericUpDown nudRow;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtName;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuButton btnSave;
        private FS.FrameWork.WinForms.Controls.NeuButton btnCancel;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private System.Windows.Forms.ComboBox cmbSpec;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtComment;
    }
}