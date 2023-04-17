namespace FS.HISFC.Components.Nurse
{
    partial class ucUpdateRegInfo
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

            if (disposing)
            {
                if (this.fPopWin != null)
                {
                    this.fPopWin.Dispose();
                    this.fPopWin = null;
                }

                if (ucShow != null)
                {
                    ucShow.Dispose();
                    ucShow = null;
                }
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
            this.plTop = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.dtRegDate = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.txtIdNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lbRegDate = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbDept = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.dtBirthday = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.cmbDoctor = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.txtPhone = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lbDept = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lbDoct = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label20 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbRegLevel = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label16 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbRegLevel = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbUnit = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.txtAge = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label15 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtAddress = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label10 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label9 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtMcardNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label8 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbPayKind = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label7 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbSex = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtCardNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbCardType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.plBottom = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.btnCancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnSave = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuSplitter1 = new FS.FrameWork.WinForms.Controls.NeuSplitter();
            this.plTop.SuspendLayout();
            this.plBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // plTop
            // 
            this.plTop.Controls.Add(this.dtRegDate);
            this.plTop.Controls.Add(this.txtIdNO);
            this.plTop.Controls.Add(this.lbRegDate);
            this.plTop.Controls.Add(this.neuLabel1);
            this.plTop.Controls.Add(this.cmbDept);
            this.plTop.Controls.Add(this.dtBirthday);
            this.plTop.Controls.Add(this.cmbDoctor);
            this.plTop.Controls.Add(this.txtPhone);
            this.plTop.Controls.Add(this.lbDept);
            this.plTop.Controls.Add(this.txtName);
            this.plTop.Controls.Add(this.lbDoct);
            this.plTop.Controls.Add(this.label20);
            this.plTop.Controls.Add(this.cmbRegLevel);
            this.plTop.Controls.Add(this.label16);
            this.plTop.Controls.Add(this.lbRegLevel);
            this.plTop.Controls.Add(this.cmbUnit);
            this.plTop.Controls.Add(this.txtAge);
            this.plTop.Controls.Add(this.label15);
            this.plTop.Controls.Add(this.txtAddress);
            this.plTop.Controls.Add(this.label10);
            this.plTop.Controls.Add(this.label9);
            this.plTop.Controls.Add(this.txtMcardNo);
            this.plTop.Controls.Add(this.label8);
            this.plTop.Controls.Add(this.cmbPayKind);
            this.plTop.Controls.Add(this.label7);
            this.plTop.Controls.Add(this.cmbSex);
            this.plTop.Controls.Add(this.label6);
            this.plTop.Controls.Add(this.txtCardNo);
            this.plTop.Controls.Add(this.label4);
            this.plTop.Controls.Add(this.cmbCardType);
            this.plTop.Controls.Add(this.label5);
            this.plTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plTop.Location = new System.Drawing.Point(0, 0);
            this.plTop.Name = "plTop";
            this.plTop.Size = new System.Drawing.Size(472, 329);
            this.plTop.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plTop.TabIndex = 0;
            // 
            // dtRegDate
            // 
            this.dtRegDate.CalendarFont = new System.Drawing.Font("宋体", 11F);
            this.dtRegDate.CustomFormat = "yyyy-MM-dd";
            this.dtRegDate.Enabled = false;
            this.dtRegDate.Font = new System.Drawing.Font("宋体", 11F);
            this.dtRegDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtRegDate.IsEnter2Tab = false;
            this.dtRegDate.Location = new System.Drawing.Point(332, 293);
            this.dtRegDate.Name = "dtRegDate";
            this.dtRegDate.Size = new System.Drawing.Size(108, 24);
            this.dtRegDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtRegDate.TabIndex = 15;
            // 
            // txtIdNO
            // 
            this.txtIdNO.Font = new System.Drawing.Font("宋体", 11F);
            this.txtIdNO.IsEnter2Tab = false;
            this.txtIdNO.Location = new System.Drawing.Point(102, 52);
            this.txtIdNO.Name = "txtIdNO";
            this.txtIdNO.Size = new System.Drawing.Size(339, 24);
            this.txtIdNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtIdNO.TabIndex = 2;            
            // 
            // lbRegDate
            // 
            this.lbRegDate.AutoSize = true;
            this.lbRegDate.Font = new System.Drawing.Font("宋体", 11F);
            this.lbRegDate.Location = new System.Drawing.Point(253, 296);
            this.lbRegDate.Name = "lbRegDate";
            this.lbRegDate.Size = new System.Drawing.Size(82, 15);
            this.lbRegDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbRegDate.TabIndex = 30;
            this.lbRegDate.Text = "挂号日期：";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Font = new System.Drawing.Font("宋体", 11F);
            this.neuLabel1.Location = new System.Drawing.Point(25, 55);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(67, 15);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 18;
            this.neuLabel1.Text = "身份证号";
            // 
            // cmbDept
            // 
            this.cmbDept.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbDept.Enabled = false;
            this.cmbDept.Font = new System.Drawing.Font("宋体", 11F);
            this.cmbDept.IsEnter2Tab = false;
            this.cmbDept.IsFlat = false;
            this.cmbDept.IsLike = true;
            this.cmbDept.IsListOnly = false;
            this.cmbDept.IsPopForm = true;
            this.cmbDept.IsShowCustomerList = false;
            this.cmbDept.IsShowID = false;
            this.cmbDept.Location = new System.Drawing.Point(333, 261);
            this.cmbDept.Name = "cmbDept";
            this.cmbDept.PopForm = null;
            this.cmbDept.ShowCustomerList = false;
            this.cmbDept.ShowID = false;
            this.cmbDept.Size = new System.Drawing.Size(108, 23);
            this.cmbDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbDept.TabIndex = 13;
            this.cmbDept.Tag = "";
            this.cmbDept.ToolBarUse = false;
            // 
            // dtBirthday
            // 
            this.dtBirthday.CustomFormat = "yyyy-MM-dd";
            this.dtBirthday.Font = new System.Drawing.Font("宋体", 11F);
            this.dtBirthday.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtBirthday.IsEnter2Tab = false;
            this.dtBirthday.Location = new System.Drawing.Point(332, 86);
            this.dtBirthday.Name = "dtBirthday";
            this.dtBirthday.Size = new System.Drawing.Size(108, 24);
            this.dtBirthday.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtBirthday.TabIndex = 4;
            this.dtBirthday.Value = new System.DateTime(2006, 6, 12, 10, 52, 27, 812);
            this.dtBirthday.ValueChanged += new System.EventHandler(this.dtBirthday_ValueChanged);
            // 
            // cmbDoctor
            // 
            this.cmbDoctor.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbDoctor.Enabled = false;
            this.cmbDoctor.Font = new System.Drawing.Font("宋体", 11F);
            this.cmbDoctor.IsEnter2Tab = false;
            this.cmbDoctor.IsFlat = false;
            this.cmbDoctor.IsLike = true;
            this.cmbDoctor.IsListOnly = false;
            this.cmbDoctor.IsPopForm = true;
            this.cmbDoctor.IsShowCustomerList = false;
            this.cmbDoctor.IsShowID = false;
            this.cmbDoctor.Location = new System.Drawing.Point(103, 293);
            this.cmbDoctor.Name = "cmbDoctor";
            this.cmbDoctor.PopForm = null;
            this.cmbDoctor.ShowCustomerList = false;
            this.cmbDoctor.ShowID = false;
            this.cmbDoctor.Size = new System.Drawing.Size(108, 23);
            this.cmbDoctor.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbDoctor.TabIndex = 14;
            this.cmbDoctor.Tag = "";
            this.cmbDoctor.ToolBarUse = false;
            // 
            // txtPhone
            // 
            this.txtPhone.BackColor = System.Drawing.SystemColors.Window;
            this.txtPhone.Font = new System.Drawing.Font("宋体", 11F);
            this.txtPhone.IsEnter2Tab = false;
            this.txtPhone.Location = new System.Drawing.Point(332, 124);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(108, 24);
            this.txtPhone.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtPhone.TabIndex = 9;
            // 
            // lbDept
            // 
            this.lbDept.Font = new System.Drawing.Font("宋体", 11F);
            this.lbDept.Location = new System.Drawing.Point(253, 264);
            this.lbDept.Name = "lbDept";
            this.lbDept.Size = new System.Drawing.Size(100, 23);
            this.lbDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbDept.TabIndex = 28;
            this.lbDept.Text = "挂号科室：";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.SystemColors.Window;
            this.txtName.Font = new System.Drawing.Font("宋体", 11F);
            this.txtName.IsEnter2Tab = false;
            this.txtName.Location = new System.Drawing.Point(332, 19);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(108, 24);
            this.txtName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtName.TabIndex = 1;
            // 
            // lbDoct
            // 
            this.lbDoct.Font = new System.Drawing.Font("宋体", 11F);
            this.lbDoct.Location = new System.Drawing.Point(24, 296);
            this.lbDoct.Name = "lbDoct";
            this.lbDoct.Size = new System.Drawing.Size(100, 23);
            this.lbDoct.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbDoct.TabIndex = 29;
            this.lbDoct.Text = "教授代码：";
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("宋体", 11F);
            this.label20.Location = new System.Drawing.Point(253, 23);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(100, 23);
            this.label20.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label20.TabIndex = 17;
            this.label20.Text = "姓    名：";
            // 
            // cmbRegLevel
            // 
            this.cmbRegLevel.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbRegLevel.Enabled = false;
            this.cmbRegLevel.Font = new System.Drawing.Font("宋体", 11F);
            this.cmbRegLevel.IsEnter2Tab = false;
            this.cmbRegLevel.IsFlat = false;
            this.cmbRegLevel.IsLike = true;
            this.cmbRegLevel.IsListOnly = false;
            this.cmbRegLevel.IsPopForm = true;
            this.cmbRegLevel.IsShowCustomerList = false;
            this.cmbRegLevel.IsShowID = false;
            this.cmbRegLevel.Location = new System.Drawing.Point(103, 261);
            this.cmbRegLevel.Name = "cmbRegLevel";
            this.cmbRegLevel.PopForm = null;
            this.cmbRegLevel.ShowCustomerList = false;
            this.cmbRegLevel.ShowID = false;
            this.cmbRegLevel.Size = new System.Drawing.Size(108, 23);
            this.cmbRegLevel.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbRegLevel.TabIndex = 12;
            this.cmbRegLevel.Tag = "";
            this.cmbRegLevel.ToolBarUse = false;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("宋体", 11F);
            this.label16.Location = new System.Drawing.Point(253, 127);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(82, 15);
            this.label16.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label16.TabIndex = 24;
            this.label16.Text = "联系电话：";
            // 
            // lbRegLevel
            // 
            this.lbRegLevel.Font = new System.Drawing.Font("宋体", 11F);
            this.lbRegLevel.Location = new System.Drawing.Point(24, 264);
            this.lbRegLevel.Name = "lbRegLevel";
            this.lbRegLevel.Size = new System.Drawing.Size(100, 23);
            this.lbRegLevel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbRegLevel.TabIndex = 27;
            this.lbRegLevel.Text = "挂号级别：";
            // 
            // cmbUnit
            // 
            this.cmbUnit.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.Font = new System.Drawing.Font("宋体", 11F);
            this.cmbUnit.IsEnter2Tab = false;
            this.cmbUnit.IsFlat = false;
            this.cmbUnit.IsLike = true;
            this.cmbUnit.IsListOnly = false;
            this.cmbUnit.IsPopForm = true;
            this.cmbUnit.IsShowCustomerList = false;
            this.cmbUnit.IsShowID = false;
            this.cmbUnit.Items.AddRange(new object[] {
            "岁",
            "月",
            "天"});
            this.cmbUnit.Location = new System.Drawing.Point(154, 124);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.PopForm = null;
            this.cmbUnit.ShowCustomerList = false;
            this.cmbUnit.ShowID = false;
            this.cmbUnit.Size = new System.Drawing.Size(56, 23);
            this.cmbUnit.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbUnit.TabIndex = 8;
            this.cmbUnit.Tag = "";
            this.cmbUnit.ToolBarUse = false;
            this.cmbUnit.SelectedIndexChanged += new System.EventHandler(this.cmbUnit_SelectedIndexChanged);
            // 
            // txtAge
            // 
            this.txtAge.BackColor = System.Drawing.SystemColors.Window;
            this.txtAge.Font = new System.Drawing.Font("宋体", 11F);
            this.txtAge.IsEnter2Tab = false;
            this.txtAge.Location = new System.Drawing.Point(102, 124);
            this.txtAge.MaxLength = 3;
            this.txtAge.Name = "txtAge";
            this.txtAge.Size = new System.Drawing.Size(49, 24);
            this.txtAge.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtAge.TabIndex = 7;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("宋体", 11F);
            this.label15.Location = new System.Drawing.Point(253, 89);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(82, 15);
            this.label15.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label15.TabIndex = 20;
            this.label15.Text = "出生日期：";
            // 
            // txtAddress
            // 
            this.txtAddress.BackColor = System.Drawing.SystemColors.Window;
            this.txtAddress.Font = new System.Drawing.Font("宋体", 11F);
            this.txtAddress.IsEnter2Tab = false;
            this.txtAddress.Location = new System.Drawing.Point(102, 156);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(339, 24);
            this.txtAddress.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtAddress.TabIndex = 10;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("宋体", 11F);
            this.label10.Location = new System.Drawing.Point(24, 159);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 23);
            this.label10.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label10.TabIndex = 25;
            this.label10.Text = "地    址：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 11F);
            this.label9.Location = new System.Drawing.Point(24, 127);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(84, 15);
            this.label9.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label9.TabIndex = 23;
            this.label9.Text = "年    龄：";
            // 
            // txtMcardNo
            // 
            this.txtMcardNo.BackColor = System.Drawing.SystemColors.Window;
            this.txtMcardNo.Enabled = false;
            this.txtMcardNo.Font = new System.Drawing.Font("宋体", 11F);
            this.txtMcardNo.IsEnter2Tab = false;
            this.txtMcardNo.Location = new System.Drawing.Point(332, 225);
            this.txtMcardNo.Name = "txtMcardNo";
            this.txtMcardNo.Size = new System.Drawing.Size(108, 24);
            this.txtMcardNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtMcardNo.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 11F);
            this.label8.Location = new System.Drawing.Point(253, 228);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 15);
            this.label8.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label8.TabIndex = 22;
            this.label8.Text = "医疗证号：";
            // 
            // cmbPayKind
            // 
            this.cmbPayKind.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbPayKind.Enabled = false;
            this.cmbPayKind.Font = new System.Drawing.Font("宋体", 11F);
            this.cmbPayKind.FormattingEnabled = true;
            this.cmbPayKind.IsEnter2Tab = false;
            this.cmbPayKind.IsFlat = false;
            this.cmbPayKind.IsLike = true;
            this.cmbPayKind.IsListOnly = false;
            this.cmbPayKind.IsPopForm = true;
            this.cmbPayKind.IsShowCustomerList = false;
            this.cmbPayKind.IsShowID = false;
            this.cmbPayKind.Location = new System.Drawing.Point(102, 225);
            this.cmbPayKind.Name = "cmbPayKind";
            this.cmbPayKind.PopForm = null;
            this.cmbPayKind.ShowCustomerList = false;
            this.cmbPayKind.ShowID = false;
            this.cmbPayKind.Size = new System.Drawing.Size(108, 23);
            this.cmbPayKind.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbPayKind.TabIndex = 5;
            this.cmbPayKind.Tag = "";
            this.cmbPayKind.ToolBarUse = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 11F);
            this.label7.Location = new System.Drawing.Point(24, 228);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 15);
            this.label7.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label7.TabIndex = 21;
            this.label7.Text = "结算类别：";
            // 
            // cmbSex
            // 
            this.cmbSex.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbSex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSex.Font = new System.Drawing.Font("宋体", 11F);
            this.cmbSex.IsEnter2Tab = false;
            this.cmbSex.IsFlat = false;
            this.cmbSex.IsLike = true;
            this.cmbSex.IsListOnly = false;
            this.cmbSex.IsPopForm = true;
            this.cmbSex.IsShowCustomerList = false;
            this.cmbSex.IsShowID = false;
            this.cmbSex.Location = new System.Drawing.Point(102, 86);
            this.cmbSex.Name = "cmbSex";
            this.cmbSex.PopForm = null;
            this.cmbSex.ShowCustomerList = false;
            this.cmbSex.ShowID = false;
            this.cmbSex.Size = new System.Drawing.Size(108, 23);
            this.cmbSex.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbSex.TabIndex = 3;
            this.cmbSex.Tag = "";
            this.cmbSex.ToolBarUse = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 11F);
            this.label6.Location = new System.Drawing.Point(24, 89);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 15);
            this.label6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label6.TabIndex = 19;
            this.label6.Text = "性    别：";
            // 
            // txtCardNo
            // 
            this.txtCardNo.BackColor = System.Drawing.SystemColors.Window;
            this.txtCardNo.Font = new System.Drawing.Font("宋体", 11F);
            this.txtCardNo.IsEnter2Tab = false;
            this.txtCardNo.Location = new System.Drawing.Point(102, 19);
            this.txtCardNo.MaxLength = 10;
            this.txtCardNo.Name = "txtCardNo";
            this.txtCardNo.Size = new System.Drawing.Size(108, 24);
            this.txtCardNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCardNo.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("宋体", 11F);
            this.label4.Location = new System.Drawing.Point(24, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label4.TabIndex = 16;
            this.label4.Text = "病 历 号：";
            // 
            // cmbCardType
            // 
            this.cmbCardType.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbCardType.Enabled = false;
            this.cmbCardType.Font = new System.Drawing.Font("宋体", 11F);
            this.cmbCardType.IsEnter2Tab = false;
            this.cmbCardType.IsFlat = false;
            this.cmbCardType.IsLike = true;
            this.cmbCardType.IsListOnly = true;
            this.cmbCardType.IsPopForm = true;
            this.cmbCardType.IsShowCustomerList = false;
            this.cmbCardType.IsShowID = false;
            this.cmbCardType.ItemHeight = 15;
            this.cmbCardType.Location = new System.Drawing.Point(102, 190);
            this.cmbCardType.Name = "cmbCardType";
            this.cmbCardType.PopForm = null;
            this.cmbCardType.ShowCustomerList = false;
            this.cmbCardType.ShowID = false;
            this.cmbCardType.Size = new System.Drawing.Size(108, 23);
            this.cmbCardType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbCardType.TabIndex = 11;
            this.cmbCardType.Tag = "";
            this.cmbCardType.ToolBarUse = false;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("宋体", 11F);
            this.label5.Location = new System.Drawing.Point(24, 193);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 23);
            this.label5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label5.TabIndex = 26;
            this.label5.Text = "证件类别：";
            // 
            // plBottom
            // 
            this.plBottom.Controls.Add(this.btnCancel);
            this.plBottom.Controls.Add(this.btnSave);
            this.plBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plBottom.Location = new System.Drawing.Point(0, 332);
            this.plBottom.Name = "plBottom";
            this.plBottom.Size = new System.Drawing.Size(472, 40);
            this.plBottom.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plBottom.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(365, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.Cancel;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(256, 8);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.Save;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // neuSplitter1
            // 
            this.neuSplitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuSplitter1.Location = new System.Drawing.Point(0, 329);
            this.neuSplitter1.Name = "neuSplitter1";
            this.neuSplitter1.Size = new System.Drawing.Size(472, 3);
            this.neuSplitter1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSplitter1.TabIndex = 1;
            this.neuSplitter1.TabStop = false;
            // 
            // ucUpdateRegInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.plTop);
            this.Controls.Add(this.neuSplitter1);
            this.Controls.Add(this.plBottom);
            this.Name = "ucUpdateRegInfo";
            this.Size = new System.Drawing.Size(472, 372);
            this.plTop.ResumeLayout(false);
            this.plTop.PerformLayout();
            this.plBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel plTop;
        private FS.FrameWork.WinForms.Controls.NeuPanel plBottom;
        private FS.FrameWork.WinForms.Controls.NeuSplitter neuSplitter1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtIdNO;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtBirthday;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtPhone;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtName;
        private FS.FrameWork.WinForms.Controls.NeuLabel label20;
        private FS.FrameWork.WinForms.Controls.NeuLabel label16;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbUnit;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtAge;
        private FS.FrameWork.WinForms.Controls.NeuLabel label15;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtAddress;
        private FS.FrameWork.WinForms.Controls.NeuLabel label10;
        private FS.FrameWork.WinForms.Controls.NeuLabel label9;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtMcardNo;
        private FS.FrameWork.WinForms.Controls.NeuLabel label8;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbPayKind;
        private FS.FrameWork.WinForms.Controls.NeuLabel label7;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbSex;
        private FS.FrameWork.WinForms.Controls.NeuLabel label6;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtCardNo;
        private FS.FrameWork.WinForms.Controls.NeuLabel label4;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbCardType;
        private FS.FrameWork.WinForms.Controls.NeuLabel label5;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDept;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDoctor;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbDept;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbDoct;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbRegLevel;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbRegLevel;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbRegDate;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtRegDate;
        private FS.FrameWork.WinForms.Controls.NeuButton btnCancel;
        private FS.FrameWork.WinForms.Controls.NeuButton btnSave;
    }
}
