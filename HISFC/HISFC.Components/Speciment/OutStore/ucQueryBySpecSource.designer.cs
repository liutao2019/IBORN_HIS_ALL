namespace FS.HISFC.Components.Speciment.OutStore
{
    partial class ucQueryBySpecSource
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
            this.cmbDisType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.chkHIS = new System.Windows.Forms.CheckBox();
            this.dtpEndTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpStartDate = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbDept = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.txtBarCode = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblBoxCode = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbOrgType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lblOrgOrBlood = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbSpecType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lblSpecType = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.rbtOr = new System.Windows.Forms.RadioButton();
            this.rbtAnd = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // cmbDisType
            // 
            //this.cmbDisType.A = false;
            this.cmbDisType.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbDisType.FormattingEnabled = true;
            this.cmbDisType.IsFlat = true;
            this.cmbDisType.IsLike = true;
            this.cmbDisType.Location = new System.Drawing.Point(609, 55);
            this.cmbDisType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDisType.Name = "cmbDisType";
            this.cmbDisType.PopForm = null;
            this.cmbDisType.ShowCustomerList = false;
            this.cmbDisType.ShowID = false;
            this.cmbDisType.Size = new System.Drawing.Size(144, 24);
            this.cmbDisType.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbDisType.TabIndex = 82;
            this.cmbDisType.Tag = "";
            this.cmbDisType.ToolBarUse = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(553, 59);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 16);
            this.label3.TabIndex = 81;
            this.label3.Text = "病种:";
            // 
            // chkHIS
            // 
            this.chkHIS.AutoSize = true;
            this.chkHIS.Checked = true;
            this.chkHIS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHIS.Location = new System.Drawing.Point(758, 57);
            this.chkHIS.Margin = new System.Windows.Forms.Padding(4);
            this.chkHIS.Name = "chkHIS";
            this.chkHIS.Size = new System.Drawing.Size(51, 20);
            this.chkHIS.TabIndex = 80;
            this.chkHIS.Text = "HIS";
            this.chkHIS.UseVisualStyleBackColor = true;
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.Location = new System.Drawing.Point(773, 7);
            this.dtpEndTime.Margin = new System.Windows.Forms.Padding(4);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.Size = new System.Drawing.Size(144, 26);
            this.dtpEndTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpEndTime.TabIndex = 79;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(512, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 16);
            this.label1.TabIndex = 78;
            this.label1.Text = "入库时间段:";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(609, 7);
            this.dtpStartDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(144, 26);
            this.dtpStartDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpStartDate.TabIndex = 77;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(754, 12);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(16, 16);
            this.label11.TabIndex = 76;
            this.label11.Text = "-";
            // 
            // cmbDept
            // 
            //this.cmbDept.A = false;
            this.cmbDept.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbDept.FormattingEnabled = true;
            this.cmbDept.IsFlat = true;
            this.cmbDept.IsLike = true;
            this.cmbDept.Location = new System.Drawing.Point(345, 55);
            this.cmbDept.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDept.Name = "cmbDept";
            this.cmbDept.PopForm = null;
            this.cmbDept.ShowCustomerList = false;
            this.cmbDept.ShowID = false;
            this.cmbDept.Size = new System.Drawing.Size(160, 24);
            this.cmbDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbDept.TabIndex = 75;
            this.cmbDept.Tag = "";
            this.cmbDept.ToolBarUse = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(259, 59);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.label2.TabIndex = 74;
            this.label2.Text = "送存科室:";
            // 
            // txtBarCode
            // 
            this.txtBarCode.Location = new System.Drawing.Point(93, 54);
            this.txtBarCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtBarCode.Name = "txtBarCode";
            this.txtBarCode.Size = new System.Drawing.Size(160, 26);
            this.txtBarCode.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtBarCode.TabIndex = 73;
            // 
            // lblBoxCode
            // 
            this.lblBoxCode.AutoSize = true;
            this.lblBoxCode.Location = new System.Drawing.Point(5, 59);
            this.lblBoxCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBoxCode.Name = "lblBoxCode";
            this.lblBoxCode.Size = new System.Drawing.Size(64, 16);
            this.lblBoxCode.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblBoxCode.TabIndex = 72;
            this.lblBoxCode.Text = "条形码:";
            // 
            // cmbOrgType
            // 
            //this.cmbOrgType.A = false;
            this.cmbOrgType.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbOrgType.FormattingEnabled = true;
            this.cmbOrgType.IsFlat = true;
            this.cmbOrgType.IsLike = true;
            this.cmbOrgType.Location = new System.Drawing.Point(93, 8);
            this.cmbOrgType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbOrgType.Name = "cmbOrgType";
            this.cmbOrgType.PopForm = null;
            this.cmbOrgType.ShowCustomerList = false;
            this.cmbOrgType.ShowID = false;
            this.cmbOrgType.Size = new System.Drawing.Size(160, 24);
            this.cmbOrgType.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbOrgType.TabIndex = 71;
            this.cmbOrgType.Tag = "";
            this.cmbOrgType.ToolBarUse = false;
            this.cmbOrgType.SelectedIndexChanged += new System.EventHandler(this.cmbOrgType_SelectedIndexChanged);
            // 
            // lblOrgOrBlood
            // 
            this.lblOrgOrBlood.AutoSize = true;
            this.lblOrgOrBlood.Location = new System.Drawing.Point(5, 12);
            this.lblOrgOrBlood.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOrgOrBlood.Name = "lblOrgOrBlood";
            this.lblOrgOrBlood.Size = new System.Drawing.Size(80, 16);
            this.lblOrgOrBlood.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblOrgOrBlood.TabIndex = 70;
            this.lblOrgOrBlood.Text = "标本种类:";
            // 
            // cmbSpecType
            // 
            //this.cmbSpecType.A = false;
            this.cmbSpecType.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbSpecType.FormattingEnabled = true;
            this.cmbSpecType.IsFlat = true;
            this.cmbSpecType.IsLike = true;
            this.cmbSpecType.Location = new System.Drawing.Point(345, 8);
            this.cmbSpecType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSpecType.Name = "cmbSpecType";
            this.cmbSpecType.PopForm = null;
            this.cmbSpecType.ShowCustomerList = false;
            this.cmbSpecType.ShowID = false;
            this.cmbSpecType.Size = new System.Drawing.Size(160, 24);
            this.cmbSpecType.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbSpecType.TabIndex = 69;
            this.cmbSpecType.Tag = "";
            this.cmbSpecType.ToolBarUse = false;
            // 
            // lblSpecType
            // 
            this.lblSpecType.AutoSize = true;
            this.lblSpecType.Location = new System.Drawing.Point(259, 12);
            this.lblSpecType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSpecType.Name = "lblSpecType";
            this.lblSpecType.Size = new System.Drawing.Size(80, 16);
            this.lblSpecType.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblSpecType.TabIndex = 68;
            this.lblSpecType.Text = "标本类型:";
            // 
            // rbtOr
            // 
            this.rbtOr.AutoSize = true;
            this.rbtOr.Location = new System.Drawing.Point(872, 57);
            this.rbtOr.Margin = new System.Windows.Forms.Padding(4);
            this.rbtOr.Name = "rbtOr";
            this.rbtOr.Size = new System.Drawing.Size(42, 20);
            this.rbtOr.TabIndex = 184;
            this.rbtOr.Text = "Or";
            this.rbtOr.UseVisualStyleBackColor = true;
            // 
            // rbtAnd
            // 
            this.rbtAnd.AutoSize = true;
            this.rbtAnd.Checked = true;
            this.rbtAnd.Location = new System.Drawing.Point(817, 57);
            this.rbtAnd.Margin = new System.Windows.Forms.Padding(4);
            this.rbtAnd.Name = "rbtAnd";
            this.rbtAnd.Size = new System.Drawing.Size(50, 20);
            this.rbtAnd.TabIndex = 183;
            this.rbtAnd.TabStop = true;
            this.rbtAnd.Text = "And";
            this.rbtAnd.UseVisualStyleBackColor = true;
            // 
            // ucQueryBySpecSource
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rbtOr);
            this.Controls.Add(this.rbtAnd);
            this.Controls.Add(this.cmbDisType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkHIS);
            this.Controls.Add(this.dtpEndTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cmbDept);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBarCode);
            this.Controls.Add(this.lblBoxCode);
            this.Controls.Add(this.cmbOrgType);
            this.Controls.Add(this.lblOrgOrBlood);
            this.Controls.Add(this.cmbSpecType);
            this.Controls.Add(this.lblSpecType);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ucQueryBySpecSource";
            this.Size = new System.Drawing.Size(919, 87);
            this.Load += new System.EventHandler(this.ucQueryBySpecSource_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDisType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkHIS;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpEndTime;
        private System.Windows.Forms.Label label1;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label11;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDept;
        private System.Windows.Forms.Label label2;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtBarCode;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblBoxCode;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbOrgType;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblOrgOrBlood;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbSpecType;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblSpecType;
        private System.Windows.Forms.RadioButton rbtOr;
        private System.Windows.Forms.RadioButton rbtAnd;
    }
}
