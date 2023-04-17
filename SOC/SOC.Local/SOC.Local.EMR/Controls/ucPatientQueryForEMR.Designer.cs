namespace FS.SOC.Local.EMR.Controls
{
    partial class ucPatientQueryForEMR
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
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.txtPactFilter = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtNameFilter = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtPatient = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.cmbDept = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.rbtnOPatient = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.rbtnIPatient = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.dtpTimeEnd = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.dtpTimeStart = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbQueryCon = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.dgvPatient = new System.Windows.Forms.DataGridView();
            this.neuPanel1.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatient)).BeginInit();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.txtPactFilter);
            this.neuPanel1.Controls.Add(this.txtNameFilter);
            this.neuPanel1.Controls.Add(this.neuLabel6);
            this.neuPanel1.Controls.Add(this.neuLabel4);
            this.neuPanel1.Controls.Add(this.neuLabel3);
            this.neuPanel1.Controls.Add(this.txtPatient);
            this.neuPanel1.Controls.Add(this.cmbDept);
            this.neuPanel1.Controls.Add(this.neuLabel1);
            this.neuPanel1.Controls.Add(this.rbtnOPatient);
            this.neuPanel1.Controls.Add(this.rbtnIPatient);
            this.neuPanel1.Controls.Add(this.dtpTimeEnd);
            this.neuPanel1.Controls.Add(this.dtpTimeStart);
            this.neuPanel1.Controls.Add(this.neuLabel2);
            this.neuPanel1.Controls.Add(this.lbQueryCon);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(1273, 47);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // txtPactFilter
            // 
            this.txtPactFilter.IsEnter2Tab = false;
            this.txtPactFilter.Location = new System.Drawing.Point(1145, 11);
            this.txtPactFilter.Name = "txtPactFilter";
            this.txtPactFilter.Size = new System.Drawing.Size(96, 21);
            this.txtPactFilter.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtPactFilter.TabIndex = 11;
            this.txtPactFilter.TextChanged += new System.EventHandler(this.txtPactFilter_TextChanged);
            // 
            // txtNameFilter
            // 
            this.txtNameFilter.IsEnter2Tab = false;
            this.txtNameFilter.Location = new System.Drawing.Point(972, 12);
            this.txtNameFilter.Name = "txtNameFilter";
            this.txtNameFilter.Size = new System.Drawing.Size(67, 21);
            this.txtNameFilter.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtNameFilter.TabIndex = 10;
            this.txtNameFilter.TextChanged += new System.EventHandler(this.txtNameFilter_TextChanged);
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.Location = new System.Drawing.Point(1045, 16);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(95, 12);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 9;
            this.neuLabel6.Text = "合同单位(过滤):";
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point(896, 16);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(71, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 9;
            this.neuLabel4.Text = "姓名(过滤):";
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(718, 17);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(83, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 9;
            this.neuLabel3.Text = "住院号(过滤):";
            // 
            // txtPatient
            // 
            this.txtPatient.IsEnter2Tab = false;
            this.txtPatient.Location = new System.Drawing.Point(806, 12);
            this.txtPatient.Name = "txtPatient";
            this.txtPatient.Size = new System.Drawing.Size(76, 21);
            this.txtPatient.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtPatient.TabIndex = 8;
            this.txtPatient.TextChanged += new System.EventHandler(this.txtPatient_TextChanged);
            // 
            // cmbDept
            // 
            this.cmbDept.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbDept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbDept.FormattingEnabled = true;
            this.cmbDept.IsEnter2Tab = false;
            this.cmbDept.IsFlat = false;
            this.cmbDept.IsLike = true;
            this.cmbDept.IsListOnly = false;
            this.cmbDept.IsPopForm = true;
            this.cmbDept.IsShowCustomerList = false;
            this.cmbDept.IsShowID = false;
            this.cmbDept.Location = new System.Drawing.Point(585, 13);
            this.cmbDept.Name = "cmbDept";
            this.cmbDept.ShowCustomerList = false;
            this.cmbDept.ShowID = false;
            this.cmbDept.Size = new System.Drawing.Size(121, 20);
            this.cmbDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbDept.TabIndex = 7;
            this.cmbDept.Tag = "";
            this.cmbDept.ToolBarUse = false;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(548, 17);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(35, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 6;
            this.neuLabel1.Text = "科室:";
            // 
            // rbtnOPatient
            // 
            this.rbtnOPatient.AutoSize = true;
            this.rbtnOPatient.Checked = true;
            this.rbtnOPatient.Location = new System.Drawing.Point(83, 15);
            this.rbtnOPatient.Name = "rbtnOPatient";
            this.rbtnOPatient.Size = new System.Drawing.Size(71, 16);
            this.rbtnOPatient.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rbtnOPatient.TabIndex = 5;
            this.rbtnOPatient.TabStop = true;
            this.rbtnOPatient.Text = "出院患者";
            this.rbtnOPatient.UseVisualStyleBackColor = true;
            this.rbtnOPatient.Click += new System.EventHandler(this.rbtnOPatient_Click);
            // 
            // rbtnIPatient
            // 
            this.rbtnIPatient.AutoSize = true;
            this.rbtnIPatient.Location = new System.Drawing.Point(10, 15);
            this.rbtnIPatient.Name = "rbtnIPatient";
            this.rbtnIPatient.Size = new System.Drawing.Size(71, 16);
            this.rbtnIPatient.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rbtnIPatient.TabIndex = 4;
            this.rbtnIPatient.Text = "在院患者";
            this.rbtnIPatient.UseVisualStyleBackColor = true;
            this.rbtnIPatient.Click += new System.EventHandler(this.rbtnOPatient_Click);
            // 
            // dtpTimeEnd
            // 
            this.dtpTimeEnd.CustomFormat = "yyyy-MM-dd 23:59:59";
            this.dtpTimeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTimeEnd.IsEnter2Tab = false;
            this.dtpTimeEnd.Location = new System.Drawing.Point(393, 13);
            this.dtpTimeEnd.Name = "dtpTimeEnd";
            this.dtpTimeEnd.Size = new System.Drawing.Size(139, 21);
            this.dtpTimeEnd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpTimeEnd.TabIndex = 3;
            // 
            // dtpTimeStart
            // 
            this.dtpTimeStart.CustomFormat = "yyyy-MM-dd 00:00:00";
            this.dtpTimeStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTimeStart.IsEnter2Tab = false;
            this.dtpTimeStart.Location = new System.Drawing.Point(229, 13);
            this.dtpTimeStart.Name = "dtpTimeStart";
            this.dtpTimeStart.Size = new System.Drawing.Size(139, 21);
            this.dtpTimeStart.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpTimeStart.TabIndex = 2;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(371, 17);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(17, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 1;
            this.neuLabel2.Text = "到";
            // 
            // lbQueryCon
            // 
            this.lbQueryCon.AutoSize = true;
            this.lbQueryCon.Location = new System.Drawing.Point(163, 17);
            this.lbQueryCon.Name = "lbQueryCon";
            this.lbQueryCon.Size = new System.Drawing.Size(65, 12);
            this.lbQueryCon.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbQueryCon.TabIndex = 0;
            this.lbQueryCon.Text = "出院日期：";
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.dgvPatient);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel2.Location = new System.Drawing.Point(0, 47);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(1273, 482);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 1;
            // 
            // dgvPatient
            // 
            this.dgvPatient.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPatient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPatient.Location = new System.Drawing.Point(0, 0);
            this.dgvPatient.Name = "dgvPatient";
            this.dgvPatient.RowTemplate.Height = 23;
            this.dgvPatient.Size = new System.Drawing.Size(1273, 482);
            this.dgvPatient.TabIndex = 0;
            this.dgvPatient.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPatient_CellDoubleClick);
            this.dgvPatient.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvPatient_RowPostPaint);
            // 
            // ucPatientQueryForEMR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuPanel2);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucPatientQueryForEMR";
            this.Size = new System.Drawing.Size(1273, 529);
            this.Load += new System.EventHandler(this.ucPatientQueryForEMR_Load);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.neuPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatient)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpTimeStart;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbQueryCon;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpTimeEnd;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rbtnOPatient;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rbtnIPatient;
        private System.Windows.Forms.DataGridView dgvPatient;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDept;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtPatient;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtPactFilter;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtNameFilter;
    }
}
