namespace FS.HISFC.Components.Order.Controls
{
    partial class ucOrderExecConfirm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucOrderExecConfirm));
            this.TabControl1 = new FS.FrameWork.WinForms.Controls.NeuTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.fpOrderExecBrowser1 = new FS.HISFC.Components.Order.Controls.fpOrderExecBrowser();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.fpOrderExecBrowser2 = new FS.HISFC.Components.Order.Controls.fpOrderExecBrowser();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtDays = new FS.FrameWork.WinForms.Controls.NeuNumericUpDown();
            this.chkAll = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.bbtnCalculate = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.btnSave = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.txtName = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.txtDrugDeptName = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dtpBeginTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.dtpEndTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.btnSelect = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.chkUnSp = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.TabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDays)).BeginInit();
            this.SuspendLayout();
            // 
            // TabControl1
            // 
            this.TabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TabControl1.Controls.Add(this.tabPage1);
            this.TabControl1.Controls.Add(this.tabPage2);
            this.TabControl1.ImageList = this.imageList1;
            this.TabControl1.Location = new System.Drawing.Point(4, 70);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(787, 365);
            this.TabControl1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.TabControl1.TabIndex = 0;
            this.TabControl1.SelectedIndexChanged += new System.EventHandler(this.TabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.fpOrderExecBrowser1);
            this.tabPage1.ImageIndex = 0;
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(779, 338);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "药品";
            this.tabPage1.ToolTipText = "执行药品";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // fpOrderExecBrowser1
            // 
            this.fpOrderExecBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpOrderExecBrowser1.IsShowBed = true;
            this.fpOrderExecBrowser1.Location = new System.Drawing.Point(3, 3);
            this.fpOrderExecBrowser1.Name = "fpOrderExecBrowser1";
            this.fpOrderExecBrowser1.Size = new System.Drawing.Size(773, 332);
            this.fpOrderExecBrowser1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.fpOrderExecBrowser2);
            this.tabPage2.ImageIndex = 1;
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(779, 338);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "非药品";
            this.tabPage2.ToolTipText = "执行非药品";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // fpOrderExecBrowser2
            // 
            this.fpOrderExecBrowser2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpOrderExecBrowser2.IsShowBed = true;
            this.fpOrderExecBrowser2.Location = new System.Drawing.Point(3, 3);
            this.fpOrderExecBrowser2.Name = "fpOrderExecBrowser2";
            this.fpOrderExecBrowser2.Size = new System.Drawing.Size(773, 332);
            this.fpOrderExecBrowser2.TabIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "药品24.ico");
            this.imageList1.Images.SetKeyName(1, "收费项目24.ico");
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel1.Location = new System.Drawing.Point(23, 19);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(38, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 1;
            this.neuLabel1.Text = "天数:";
            // 
            // txtDays
            // 
            this.txtDays.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDays.Location = new System.Drawing.Point(64, 14);
            this.txtDays.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.txtDays.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtDays.Name = "txtDays";
            this.txtDays.Size = new System.Drawing.Size(35, 21);
            this.txtDays.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtDays.TabIndex = 2;
            this.txtDays.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtDays.ValueChanged += new System.EventHandler(this.txtDays_ValueChanged);
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(205, 18);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(48, 16);
            this.chkAll.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chkAll.TabIndex = 3;
            this.chkAll.Text = "全选";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // bbtnCalculate
            // 
            this.bbtnCalculate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bbtnCalculate.Location = new System.Drawing.Point(111, 14);
            this.bbtnCalculate.Name = "bbtnCalculate";
            this.bbtnCalculate.Size = new System.Drawing.Size(68, 23);
            this.bbtnCalculate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.bbtnCalculate.TabIndex = 4;
            this.bbtnCalculate.Text = "重新计算";
            this.bbtnCalculate.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.bbtnCalculate.UseVisualStyleBackColor = true;
            this.bbtnCalculate.Click += new System.EventHandler(this.bbtnCalculate_Click);
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(2, 50);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(59, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 5;
            this.neuLabel2.Text = "药品过滤:";
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.neuLabel3.Location = new System.Drawing.Point(382, 49);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(89, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 6;
            this.neuLabel3.Text = "兰色代表首日量";
            this.neuLabel3.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(642, 46);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(89, 23);
            this.btnSave.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "保存";
            this.btnSave.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Visible = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtName
            // 
            this.txtName.ArrowBackColor = System.Drawing.Color.Silver;
            this.txtName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtName.FormattingEnabled = true;
            this.txtName.IsEnter2Tab = false;
            this.txtName.IsFlat = false;
            this.txtName.IsLike = true;
            this.txtName.IsListOnly = false;
            this.txtName.IsPopForm = true;
            this.txtName.IsShowCustomerList = false;
            this.txtName.IsShowID = false;
            this.txtName.IsShowIDAndName = false;
            this.txtName.Location = new System.Drawing.Point(62, 46);
            this.txtName.Name = "txtName";
            this.txtName.ShowCustomerList = false;
            this.txtName.ShowID = false;
            this.txtName.Size = new System.Drawing.Size(115, 20);
            this.txtName.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.txtName.TabIndex = 9;
            this.txtName.Tag = "";
            this.txtName.ToolBarUse = false;
            this.toolTip1.SetToolTip(this.txtName, "选择标识要查看的项目");
            this.txtName.SelectedIndexChanged += new System.EventHandler(this.txtName_SelectedIndexChanged);
            // 
            // txtDrugDeptName
            // 
            this.txtDrugDeptName.ArrowBackColor = System.Drawing.Color.Silver;
            this.txtDrugDeptName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtDrugDeptName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtDrugDeptName.FormattingEnabled = true;
            this.txtDrugDeptName.IsEnter2Tab = false;
            this.txtDrugDeptName.IsFlat = false;
            this.txtDrugDeptName.IsLike = true;
            this.txtDrugDeptName.IsListOnly = false;
            this.txtDrugDeptName.IsPopForm = true;
            this.txtDrugDeptName.IsShowCustomerList = false;
            this.txtDrugDeptName.IsShowID = false;
            this.txtDrugDeptName.IsShowIDAndName = false;
            this.txtDrugDeptName.Location = new System.Drawing.Point(267, 46);
            this.txtDrugDeptName.Name = "txtDrugDeptName";
            this.txtDrugDeptName.ShowCustomerList = false;
            this.txtDrugDeptName.ShowID = false;
            this.txtDrugDeptName.Size = new System.Drawing.Size(109, 20);
            this.txtDrugDeptName.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.txtDrugDeptName.TabIndex = 11;
            this.txtDrugDeptName.Tag = "";
            this.txtDrugDeptName.ToolBarUse = false;
            this.toolTip1.SetToolTip(this.txtDrugDeptName, "选择标识要查看的项目");
            this.txtDrugDeptName.Visible = false;
            this.txtDrugDeptName.SelectedIndexChanged += new System.EventHandler(this.txtDrugDeptName_SelectedIndexChanged);
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point(203, 49);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(59, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 10;
            this.neuLabel4.Text = "取药科室:";
            this.neuLabel4.Visible = false;
            // 
            // dtpBeginTime
            // 
            this.dtpBeginTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpBeginTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBeginTime.IsEnter2Tab = false;
            this.dtpBeginTime.Location = new System.Drawing.Point(332, 15);
            this.dtpBeginTime.Name = "dtpBeginTime";
            this.dtpBeginTime.Size = new System.Drawing.Size(139, 21);
            this.dtpBeginTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpBeginTime.TabIndex = 12;
            this.dtpBeginTime.Visible = false;
            this.dtpBeginTime.ValueChanged += new System.EventHandler(this.dtpBeginTime_ValueChanged);
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndTime.IsEnter2Tab = false;
            this.dtpEndTime.Location = new System.Drawing.Point(497, 15);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.Size = new System.Drawing.Size(139, 21);
            this.dtpEndTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpEndTime.TabIndex = 13;
            this.dtpEndTime.Visible = false;
            this.dtpEndTime.ValueChanged += new System.EventHandler(this.dtpEndTime_ValueChanged);
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Location = new System.Drawing.Point(477, 19);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(17, 12);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 14;
            this.neuLabel5.Text = "至";
            this.neuLabel5.Visible = false;
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.Location = new System.Drawing.Point(265, 19);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(71, 12);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 15;
            this.neuLabel6.Text = "按使用时间:";
            this.neuLabel6.Visible = false;
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(642, 14);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(89, 23);
            this.btnSelect.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnSelect.TabIndex = 16;
            this.btnSelect.Text = "选择";
            this.btnSelect.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Visible = false;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // chkUnSp
            // 
            this.chkUnSp.AutoSize = true;
            this.chkUnSp.Location = new System.Drawing.Point(205, 48);
            this.chkUnSp.Name = "chkUnSp";
            this.chkUnSp.Size = new System.Drawing.Size(84, 16);
            this.chkUnSp.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chkUnSp.TabIndex = 3;
            this.chkUnSp.Text = "反选化疗药";
            this.chkUnSp.UseVisualStyleBackColor = true;
            this.chkUnSp.Visible = false;
            this.chkUnSp.CheckedChanged += new System.EventHandler(this.chkUnSp_CheckedChanged);
            // 
            // ucOrderExecConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.neuLabel5);
            this.Controls.Add(this.dtpEndTime);
            this.Controls.Add(this.dtpBeginTime);
            this.Controls.Add(this.txtDrugDeptName);
            this.Controls.Add(this.neuLabel4);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.neuLabel3);
            this.Controls.Add(this.neuLabel2);
            this.Controls.Add(this.bbtnCalculate);
            this.Controls.Add(this.chkUnSp);
            this.Controls.Add(this.chkAll);
            this.Controls.Add(this.txtDays);
            this.Controls.Add(this.neuLabel1);
            this.Controls.Add(this.TabControl1);
            this.Controls.Add(this.neuLabel6);
            this.Name = "ucOrderExecConfirm";
            this.Size = new System.Drawing.Size(794, 435);
            this.TabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtDays)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuTabControl TabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuNumericUpDown txtDays;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox chkAll;
        private FS.FrameWork.WinForms.Controls.NeuButton bbtnCalculate;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuButton btnSave;
        private System.Windows.Forms.ImageList imageList1;
        private fpOrderExecBrowser fpOrderExecBrowser1;
        private fpOrderExecBrowser fpOrderExecBrowser2;
        private FS.FrameWork.WinForms.Controls.NeuComboBox txtName;
        private System.Windows.Forms.ToolTip toolTip1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuComboBox txtDrugDeptName;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpBeginTime;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpEndTime;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        private FS.FrameWork.WinForms.Controls.NeuButton btnSelect;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox chkUnSp;
    }
}
