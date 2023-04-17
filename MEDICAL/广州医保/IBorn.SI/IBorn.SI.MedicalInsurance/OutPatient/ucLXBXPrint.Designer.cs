namespace IBorn.SI.MedicalInsurance.OutPatient
{
    partial class ucLXBXPrint
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
            FS.FrameWork.WinForms.Controls.NeuLabel lbDoct;
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.searchTxt = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dtpbegin = new System.Windows.Forms.DateTimePicker();
            this.dtpend = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDoct = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbRegDept = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lbRegDept = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ucOutPatientFeeDetailBill1 = new IBorn.SI.MedicalInsurance.BaseControls.ucOutPatientFeeLXBXDetailBill();
            lbDoct = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmbDoct);
            this.panel1.Controls.Add(this.cmbRegDept);
            this.panel1.Controls.Add(this.lbRegDept);
            this.panel1.Controls.Add(lbDoct);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dtpend);
            this.panel1.Controls.Add(this.dtpbegin);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.searchTxt);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(807, 63);
            this.panel1.TabIndex = 0;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(411, 15);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(120, 16);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "是否过滤零元项目";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10F);
            this.label2.Location = new System.Drawing.Point(4, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "检索患者:";
            // 
            // searchTxt
            // 
            this.searchTxt.Location = new System.Drawing.Point(76, 12);
            this.searchTxt.Name = "searchTxt";
            this.searchTxt.Size = new System.Drawing.Size(89, 21);
            this.searchTxt.TabIndex = 2;
            this.searchTxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchTxt_KeyDown);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 63);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(807, 537);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ucOutPatientFeeDetailBill1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(799, 511);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "清单";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dtpbegin
            // 
            this.dtpbegin.CustomFormat = "yyyy-MM-dd";
            this.dtpbegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpbegin.Location = new System.Drawing.Point(168, 12);
            this.dtpbegin.Name = "dtpbegin";
            this.dtpbegin.Size = new System.Drawing.Size(104, 21);
            this.dtpbegin.TabIndex = 5;
            // 
            // dtpend
            // 
            this.dtpend.CustomFormat = "yyyy-MM-dd";
            this.dtpend.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpend.Location = new System.Drawing.Point(297, 12);
            this.dtpend.Name = "dtpend";
            this.dtpend.Size = new System.Drawing.Size(99, 21);
            this.dtpend.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10F);
            this.label1.Location = new System.Drawing.Point(275, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 14);
            this.label1.TabIndex = 7;
            this.label1.Text = "至";
            // 
            // cmbDoct
            // 
            this.cmbDoct.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbDoct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbDoct.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbDoct.FormattingEnabled = true;
            this.cmbDoct.IsEnter2Tab = false;
            this.cmbDoct.IsFlat = false;
            this.cmbDoct.IsLike = true;
            this.cmbDoct.IsListOnly = false;
            this.cmbDoct.IsPopForm = true;
            this.cmbDoct.IsShowCustomerList = false;
            this.cmbDoct.IsShowID = false;
            this.cmbDoct.IsShowIDAndName = false;
            this.cmbDoct.Location = new System.Drawing.Point(258, 37);
            this.cmbDoct.Name = "cmbDoct";
            this.cmbDoct.ShowCustomerList = false;
            this.cmbDoct.ShowID = false;
            this.cmbDoct.Size = new System.Drawing.Size(111, 20);
            this.cmbDoct.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbDoct.TabIndex = 27;
            this.cmbDoct.Tag = "";
            this.cmbDoct.ToolBarUse = false;
            this.cmbDoct.Visible = false;
            // 
            // cmbRegDept
            // 
            this.cmbRegDept.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbRegDept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbRegDept.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbRegDept.FormattingEnabled = true;
            this.cmbRegDept.IsEnter2Tab = false;
            this.cmbRegDept.IsFlat = false;
            this.cmbRegDept.IsLike = true;
            this.cmbRegDept.IsListOnly = false;
            this.cmbRegDept.IsPopForm = true;
            this.cmbRegDept.IsShowCustomerList = false;
            this.cmbRegDept.IsShowID = false;
            this.cmbRegDept.IsShowIDAndName = false;
            this.cmbRegDept.Location = new System.Drawing.Point(76, 37);
            this.cmbRegDept.Name = "cmbRegDept";
            this.cmbRegDept.ShowCustomerList = false;
            this.cmbRegDept.ShowID = false;
            this.cmbRegDept.Size = new System.Drawing.Size(111, 20);
            this.cmbRegDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbRegDept.TabIndex = 29;
            this.cmbRegDept.Tag = "";
            this.cmbRegDept.ToolBarUse = false;
            // 
            // lbRegDept
            // 
            this.lbRegDept.AutoSize = true;
            this.lbRegDept.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbRegDept.ForeColor = System.Drawing.Color.Black;
            this.lbRegDept.Location = new System.Drawing.Point(11, 40);
            this.lbRegDept.Name = "lbRegDept";
            this.lbRegDept.Size = new System.Drawing.Size(59, 12);
            this.lbRegDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbRegDept.TabIndex = 26;
            this.lbRegDept.Text = "看诊科室:";
            // 
            // lbDoct
            // 
            lbDoct.AutoSize = true;
            lbDoct.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            lbDoct.ForeColor = System.Drawing.Color.Black;
            lbDoct.Location = new System.Drawing.Point(193, 39);
            lbDoct.Name = "lbDoct";
            lbDoct.Size = new System.Drawing.Size(59, 12);
            lbDoct.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            lbDoct.TabIndex = 28;
            lbDoct.Text = "开立医生:";
            lbDoct.Visible = false;
            // 
            // ucOutPatientFeeDetailBill1
            // 
            this.ucOutPatientFeeDetailBill1.BackColor = System.Drawing.Color.White;
            this.ucOutPatientFeeDetailBill1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucOutPatientFeeDetailBill1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucOutPatientFeeDetailBill1.IsFullConvertToHalf = true;
            this.ucOutPatientFeeDetailBill1.IsPrint = false;
            this.ucOutPatientFeeDetailBill1.Location = new System.Drawing.Point(3, 3);
            this.ucOutPatientFeeDetailBill1.Name = "ucOutPatientFeeDetailBill1";
            this.ucOutPatientFeeDetailBill1.ParentFormToolBar = null;
            this.ucOutPatientFeeDetailBill1.Size = new System.Drawing.Size(793, 505);
            this.ucOutPatientFeeDetailBill1.TabIndex = 0;
            // 
            // ucLXBXPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Name = "ucLXBXPrint";
            this.Size = new System.Drawing.Size(807, 600);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private BaseControls.ucOutPatientFeeLXBXDetailBill ucOutPatientFeeDetailBill1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox searchTxt;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.DateTimePicker dtpend;
        private System.Windows.Forms.DateTimePicker dtpbegin;
        private System.Windows.Forms.Label label1;
        protected FS.FrameWork.WinForms.Controls.NeuComboBox cmbDoct;
        protected FS.FrameWork.WinForms.Controls.NeuComboBox cmbRegDept;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbRegDept;
    }
}
