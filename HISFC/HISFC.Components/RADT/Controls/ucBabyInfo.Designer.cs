namespace FS.HISFC.Components.RADT.Controls
{
    partial class ucBabyInfo
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
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtMomInfo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtInpatientNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtHeight = new FS.FrameWork.WinForms.Controls.NeuNumericTextBox();
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtWeight = new FS.FrameWork.WinForms.Controls.NeuNumericTextBox();
            this.neuLabel7 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel8 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel9 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel10 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.btAdd = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btSave = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btCancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btBabyInfo = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.cmbSex = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.dtBirthday = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.cmbBlood = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbDept = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.dtOperatedate = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.cmbResponsibleDoc = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel13 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtGestationalAge = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel12 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbDeliveryMode = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel11 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.btRevokCancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.ucQueryInpatientNo = new FS.HISFC.Components.Common.Controls.ucQueryInpatientNo();
            this.gpbPatientNO = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tvPatientList1 = new FS.HISFC.Components.Common.Controls.tvPatientList();
            this.neuGroupBox1.SuspendLayout();
            this.gpbPatientNO.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(25, 30);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(65, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "母亲信息：";
            // 
            // txtMomInfo
            // 
            this.txtMomInfo.IsEnter2Tab = false;
            this.txtMomInfo.Location = new System.Drawing.Point(98, 27);
            this.txtMomInfo.Name = "txtMomInfo";
            this.txtMomInfo.ReadOnly = true;
            this.txtMomInfo.Size = new System.Drawing.Size(139, 21);
            this.txtMomInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtMomInfo.TabIndex = 1;
            // 
            // txtInpatientNo
            // 
            this.txtInpatientNo.IsEnter2Tab = false;
            this.txtInpatientNo.Location = new System.Drawing.Point(98, 54);
            this.txtInpatientNo.Name = "txtInpatientNo";
            this.txtInpatientNo.ReadOnly = true;
            this.txtInpatientNo.Size = new System.Drawing.Size(139, 21);
            this.txtInpatientNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtInpatientNo.TabIndex = 3;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(25, 57);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(65, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 2;
            this.neuLabel2.Text = "婴儿序号：";
            // 
            // txtName
            // 
            this.txtName.IsEnter2Tab = false;
            this.txtName.Location = new System.Drawing.Point(98, 81);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(139, 21);
            this.txtName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtName.TabIndex = 5;
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.neuLabel3.Location = new System.Drawing.Point(25, 84);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(65, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 4;
            this.neuLabel3.Text = "姓    名：";
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.neuLabel4.Location = new System.Drawing.Point(25, 111);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(65, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 6;
            this.neuLabel4.Text = "性    别：";
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Location = new System.Drawing.Point(25, 138);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(65, 12);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 8;
            this.neuLabel5.Text = "出生时间：";
            // 
            // txtHeight
            // 
            this.txtHeight.AllowNegative = false;
            this.txtHeight.IsAutoRemoveDecimalZero = false;
            this.txtHeight.IsEnter2Tab = false;
            this.txtHeight.Location = new System.Drawing.Point(98, 162);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.NumericPrecision = 6;
            this.txtHeight.NumericScaleOnFocus = 2;
            this.txtHeight.NumericScaleOnLostFocus = 2;
            this.txtHeight.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtHeight.SetRange = new System.Drawing.Size(-1, -1);
            this.txtHeight.Size = new System.Drawing.Size(139, 21);
            this.txtHeight.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtHeight.TabIndex = 11;
            this.txtHeight.Text = "0.00";
            this.txtHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtHeight.UseGroupSeperator = true;
            this.txtHeight.ZeroIsValid = true;
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.Location = new System.Drawing.Point(25, 165);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(71, 12);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 10;
            this.neuLabel6.Text = "身 高(CM)：";
            // 
            // txtWeight
            // 
            this.txtWeight.AllowNegative = false;
            this.txtWeight.IsAutoRemoveDecimalZero = false;
            this.txtWeight.IsEnter2Tab = false;
            this.txtWeight.Location = new System.Drawing.Point(98, 189);
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.NumericPrecision = 6;
            this.txtWeight.NumericScaleOnFocus = 2;
            this.txtWeight.NumericScaleOnLostFocus = 2;
            this.txtWeight.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtWeight.SetRange = new System.Drawing.Size(-1, -1);
            this.txtWeight.Size = new System.Drawing.Size(139, 21);
            this.txtWeight.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtWeight.TabIndex = 13;
            this.txtWeight.Text = "0.00";
            this.txtWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtWeight.UseGroupSeperator = true;
            this.txtWeight.ZeroIsValid = true;
            // 
            // neuLabel7
            // 
            this.neuLabel7.AutoSize = true;
            this.neuLabel7.Location = new System.Drawing.Point(25, 192);
            this.neuLabel7.Name = "neuLabel7";
            this.neuLabel7.Size = new System.Drawing.Size(71, 12);
            this.neuLabel7.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel7.TabIndex = 12;
            this.neuLabel7.Text = "体 重(Kg)：";
            // 
            // neuLabel8
            // 
            this.neuLabel8.AutoSize = true;
            this.neuLabel8.Location = new System.Drawing.Point(25, 246);
            this.neuLabel8.Name = "neuLabel8";
            this.neuLabel8.Size = new System.Drawing.Size(65, 12);
            this.neuLabel8.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel8.TabIndex = 14;
            this.neuLabel8.Text = "血    型：";
            // 
            // neuLabel9
            // 
            this.neuLabel9.AutoSize = true;
            this.neuLabel9.Location = new System.Drawing.Point(25, 273);
            this.neuLabel9.Name = "neuLabel9";
            this.neuLabel9.Size = new System.Drawing.Size(65, 12);
            this.neuLabel9.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel9.TabIndex = 16;
            this.neuLabel9.Text = "住院科室：";
            // 
            // neuLabel10
            // 
            this.neuLabel10.AutoSize = true;
            this.neuLabel10.Location = new System.Drawing.Point(25, 300);
            this.neuLabel10.Name = "neuLabel10";
            this.neuLabel10.Size = new System.Drawing.Size(65, 12);
            this.neuLabel10.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel10.TabIndex = 18;
            this.neuLabel10.Text = "登记时间：";
            // 
            // btAdd
            // 
            this.btAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btAdd.Location = new System.Drawing.Point(18, 395);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(75, 23);
            this.btAdd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btAdd.TabIndex = 20;
            this.btAdd.Text = "添加(&A)";
            this.btAdd.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // btSave
            // 
            this.btSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btSave.Location = new System.Drawing.Point(99, 395);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(75, 23);
            this.btSave.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btSave.TabIndex = 21;
            this.btSave.Text = "保存(&S)";
            this.btSave.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btCancel
            // 
            this.btCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btCancel.Location = new System.Drawing.Point(180, 395);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btCancel.TabIndex = 22;
            this.btCancel.Text = "取消登记(&C)";
            this.btCancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btBabyInfo
            // 
            this.btBabyInfo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btBabyInfo.Location = new System.Drawing.Point(180, 424);
            this.btBabyInfo.Name = "btBabyInfo";
            this.btBabyInfo.Size = new System.Drawing.Size(75, 23);
            this.btBabyInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btBabyInfo.TabIndex = 23;
            this.btBabyInfo.Text = "出生证登记";
            this.btBabyInfo.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btBabyInfo.UseVisualStyleBackColor = true;
            this.btBabyInfo.Visible = false;
            // 
            // cmbSex
            // 
            this.cmbSex.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbSex.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbSex.FormattingEnabled = true;
            this.cmbSex.IsEnter2Tab = false;
            this.cmbSex.IsFlat = false;
            this.cmbSex.IsLike = true;
            this.cmbSex.IsListOnly = false;
            this.cmbSex.IsPopForm = true;
            this.cmbSex.IsShowCustomerList = false;
            this.cmbSex.IsShowID = false;
            this.cmbSex.IsShowIDAndName = false;
            this.cmbSex.Location = new System.Drawing.Point(98, 107);
            this.cmbSex.Name = "cmbSex";
            this.cmbSex.ShowCustomerList = false;
            this.cmbSex.ShowID = false;
            this.cmbSex.Size = new System.Drawing.Size(139, 20);
            this.cmbSex.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbSex.TabIndex = 24;
            this.cmbSex.Tag = "";
            this.cmbSex.ToolBarUse = false;
            this.cmbSex.SelectedIndexChanged += new System.EventHandler(this.cmbSex_SelectedIndexChanged);
            // 
            // dtBirthday
            // 
            this.dtBirthday.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtBirthday.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtBirthday.IsEnter2Tab = false;
            this.dtBirthday.Location = new System.Drawing.Point(98, 135);
            this.dtBirthday.Name = "dtBirthday";
            this.dtBirthday.Size = new System.Drawing.Size(139, 21);
            this.dtBirthday.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtBirthday.TabIndex = 25;
            // 
            // cmbBlood
            // 
            this.cmbBlood.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbBlood.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbBlood.FormattingEnabled = true;
            this.cmbBlood.IsEnter2Tab = false;
            this.cmbBlood.IsFlat = false;
            this.cmbBlood.IsLike = true;
            this.cmbBlood.IsListOnly = false;
            this.cmbBlood.IsPopForm = true;
            this.cmbBlood.IsShowCustomerList = false;
            this.cmbBlood.IsShowID = false;
            this.cmbBlood.IsShowIDAndName = false;
            this.cmbBlood.Location = new System.Drawing.Point(98, 242);
            this.cmbBlood.Name = "cmbBlood";
            this.cmbBlood.ShowCustomerList = false;
            this.cmbBlood.ShowID = false;
            this.cmbBlood.Size = new System.Drawing.Size(139, 20);
            this.cmbBlood.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbBlood.TabIndex = 26;
            this.cmbBlood.Tag = "";
            this.cmbBlood.ToolBarUse = false;
            // 
            // cmbDept
            // 
            this.cmbDept.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbDept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbDept.FormattingEnabled = true;
            this.cmbDept.IsEnter2Tab = false;
            this.cmbDept.IsFlat = false;
            this.cmbDept.IsLike = true;
            this.cmbDept.IsListOnly = false;
            this.cmbDept.IsPopForm = true;
            this.cmbDept.IsShowCustomerList = false;
            this.cmbDept.IsShowID = false;
            this.cmbDept.IsShowIDAndName = false;
            this.cmbDept.Location = new System.Drawing.Point(98, 270);
            this.cmbDept.Name = "cmbDept";
            this.cmbDept.ShowCustomerList = false;
            this.cmbDept.ShowID = false;
            this.cmbDept.Size = new System.Drawing.Size(139, 20);
            this.cmbDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbDept.TabIndex = 27;
            this.cmbDept.Tag = "";
            this.cmbDept.ToolBarUse = false;
            // 
            // dtOperatedate
            // 
            this.dtOperatedate.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtOperatedate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtOperatedate.IsEnter2Tab = false;
            this.dtOperatedate.Location = new System.Drawing.Point(98, 296);
            this.dtOperatedate.Name = "dtOperatedate";
            this.dtOperatedate.Size = new System.Drawing.Size(139, 21);
            this.dtOperatedate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtOperatedate.TabIndex = 28;
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.cmbResponsibleDoc);
            this.neuGroupBox1.Controls.Add(this.neuLabel13);
            this.neuGroupBox1.Controls.Add(this.txtGestationalAge);
            this.neuGroupBox1.Controls.Add(this.neuLabel12);
            this.neuGroupBox1.Controls.Add(this.cmbDeliveryMode);
            this.neuGroupBox1.Controls.Add(this.neuLabel11);
            this.neuGroupBox1.Controls.Add(this.btRevokCancel);
            this.neuGroupBox1.Controls.Add(this.neuLabel1);
            this.neuGroupBox1.Controls.Add(this.dtOperatedate);
            this.neuGroupBox1.Controls.Add(this.txtMomInfo);
            this.neuGroupBox1.Controls.Add(this.cmbDept);
            this.neuGroupBox1.Controls.Add(this.neuLabel2);
            this.neuGroupBox1.Controls.Add(this.cmbBlood);
            this.neuGroupBox1.Controls.Add(this.txtInpatientNo);
            this.neuGroupBox1.Controls.Add(this.dtBirthday);
            this.neuGroupBox1.Controls.Add(this.neuLabel3);
            this.neuGroupBox1.Controls.Add(this.cmbSex);
            this.neuGroupBox1.Controls.Add(this.txtName);
            this.neuGroupBox1.Controls.Add(this.btBabyInfo);
            this.neuGroupBox1.Controls.Add(this.neuLabel4);
            this.neuGroupBox1.Controls.Add(this.btCancel);
            this.neuGroupBox1.Controls.Add(this.neuLabel5);
            this.neuGroupBox1.Controls.Add(this.btSave);
            this.neuGroupBox1.Controls.Add(this.neuLabel6);
            this.neuGroupBox1.Controls.Add(this.btAdd);
            this.neuGroupBox1.Controls.Add(this.txtHeight);
            this.neuGroupBox1.Controls.Add(this.neuLabel10);
            this.neuGroupBox1.Controls.Add(this.neuLabel7);
            this.neuGroupBox1.Controls.Add(this.neuLabel9);
            this.neuGroupBox1.Controls.Add(this.txtWeight);
            this.neuGroupBox1.Controls.Add(this.neuLabel8);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 48);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(273, 428);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 29;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "婴儿信息";
            // 
            // cmbResponsibleDoc
            // 
            this.cmbResponsibleDoc.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbResponsibleDoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbResponsibleDoc.FormattingEnabled = true;
            this.cmbResponsibleDoc.IsEnter2Tab = false;
            this.cmbResponsibleDoc.IsFlat = false;
            this.cmbResponsibleDoc.IsLike = true;
            this.cmbResponsibleDoc.IsListOnly = false;
            this.cmbResponsibleDoc.IsPopForm = true;
            this.cmbResponsibleDoc.IsShowCustomerList = false;
            this.cmbResponsibleDoc.IsShowID = false;
            this.cmbResponsibleDoc.IsShowIDAndName = false;
            this.cmbResponsibleDoc.Location = new System.Drawing.Point(119, 352);
            this.cmbResponsibleDoc.Name = "cmbResponsibleDoc";
            this.cmbResponsibleDoc.ShowCustomerList = false;
            this.cmbResponsibleDoc.ShowID = false;
            this.cmbResponsibleDoc.Size = new System.Drawing.Size(117, 20);
            this.cmbResponsibleDoc.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbResponsibleDoc.TabIndex = 35;
            this.cmbResponsibleDoc.Tag = "";
            this.cmbResponsibleDoc.ToolBarUse = false;
            // 
            // neuLabel13
            // 
            this.neuLabel13.AutoSize = true;
            this.neuLabel13.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel13.Location = new System.Drawing.Point(24, 356);
            this.neuLabel13.Name = "neuLabel13";
            this.neuLabel13.Size = new System.Drawing.Size(89, 12);
            this.neuLabel13.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel13.TabIndex = 34;
            this.neuLabel13.Text = "儿科责任医生：";
            // 
            // txtGestationalAge
            // 
            this.txtGestationalAge.IsEnter2Tab = false;
            this.txtGestationalAge.Location = new System.Drawing.Point(98, 216);
            this.txtGestationalAge.Name = "txtGestationalAge";
            this.txtGestationalAge.Size = new System.Drawing.Size(139, 21);
            this.txtGestationalAge.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtGestationalAge.TabIndex = 33;
            // 
            // neuLabel12
            // 
            this.neuLabel12.AutoSize = true;
            this.neuLabel12.Location = new System.Drawing.Point(23, 219);
            this.neuLabel12.Name = "neuLabel12";
            this.neuLabel12.Size = new System.Drawing.Size(65, 12);
            this.neuLabel12.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel12.TabIndex = 32;
            this.neuLabel12.Text = "胎    龄：";
            // 
            // cmbDeliveryMode
            // 
            this.cmbDeliveryMode.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbDeliveryMode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbDeliveryMode.FormattingEnabled = true;
            this.cmbDeliveryMode.IsEnter2Tab = false;
            this.cmbDeliveryMode.IsFlat = false;
            this.cmbDeliveryMode.IsLike = true;
            this.cmbDeliveryMode.IsListOnly = false;
            this.cmbDeliveryMode.IsPopForm = true;
            this.cmbDeliveryMode.IsShowCustomerList = false;
            this.cmbDeliveryMode.IsShowID = false;
            this.cmbDeliveryMode.IsShowIDAndName = false;
            this.cmbDeliveryMode.Location = new System.Drawing.Point(98, 324);
            this.cmbDeliveryMode.Name = "cmbDeliveryMode";
            this.cmbDeliveryMode.ShowCustomerList = false;
            this.cmbDeliveryMode.ShowID = false;
            this.cmbDeliveryMode.Size = new System.Drawing.Size(139, 20);
            this.cmbDeliveryMode.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbDeliveryMode.TabIndex = 31;
            this.cmbDeliveryMode.Tag = "";
            this.cmbDeliveryMode.ToolBarUse = false;
            // 
            // neuLabel11
            // 
            this.neuLabel11.AutoSize = true;
            this.neuLabel11.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel11.Location = new System.Drawing.Point(25, 328);
            this.neuLabel11.Name = "neuLabel11";
            this.neuLabel11.Size = new System.Drawing.Size(65, 12);
            this.neuLabel11.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel11.TabIndex = 30;
            this.neuLabel11.Text = "分娩方式：";
            // 
            // btRevokCancel
            // 
            this.btRevokCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btRevokCancel.Location = new System.Drawing.Point(99, 424);
            this.btRevokCancel.Name = "btRevokCancel";
            this.btRevokCancel.Size = new System.Drawing.Size(75, 23);
            this.btRevokCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btRevokCancel.TabIndex = 29;
            this.btRevokCancel.Text = "还原有效";
            this.btRevokCancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btRevokCancel.UseVisualStyleBackColor = true;
            this.btRevokCancel.Visible = false;
            this.btRevokCancel.Click += new System.EventHandler(this.btRevokCancel_Click);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 476);
            this.splitter1.TabIndex = 30;
            this.splitter1.TabStop = false;
            // 
            // ucQueryInpatientNo
            // 
            this.ucQueryInpatientNo.DefaultInputType = 0;
            this.ucQueryInpatientNo.InputType = 0;
            this.ucQueryInpatientNo.IsDeptOnly = true;
            this.ucQueryInpatientNo.Location = new System.Drawing.Point(36, 15);
            this.ucQueryInpatientNo.Name = "ucQueryInpatientNo";
            this.ucQueryInpatientNo.PatientInState = "ALL";
            this.ucQueryInpatientNo.ShowState = FS.HISFC.Components.Common.Controls.enuShowState.All;
            this.ucQueryInpatientNo.Size = new System.Drawing.Size(190, 27);
            this.ucQueryInpatientNo.TabIndex = 34;
            this.ucQueryInpatientNo.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(this.ucQueryInpatientNo_myEvent);
            // 
            // gpbPatientNO
            // 
            this.gpbPatientNO.Controls.Add(this.ucQueryInpatientNo);
            this.gpbPatientNO.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpbPatientNO.Location = new System.Drawing.Point(0, 0);
            this.gpbPatientNO.Name = "gpbPatientNO";
            this.gpbPatientNO.Size = new System.Drawing.Size(273, 48);
            this.gpbPatientNO.TabIndex = 35;
            this.gpbPatientNO.TabStop = false;
            this.gpbPatientNO.Text = "母亲住院号";
            this.gpbPatientNO.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.neuGroupBox1);
            this.panel1.Controls.Add(this.gpbPatientNO);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(3, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(273, 476);
            this.panel1.TabIndex = 34;
            // 
            // tvPatientList1
            // 
            this.tvPatientList1.Checked = FS.HISFC.Components.Common.Controls.tvPatientList.enuChecked.None;
            this.tvPatientList1.Direction = FS.HISFC.Components.Common.Controls.tvPatientList.enuShowDirection.Ahead;
            this.tvPatientList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvPatientList1.Font = new System.Drawing.Font("Arial", 9F);
            this.tvPatientList1.HideSelection = false;
            this.tvPatientList1.ImageIndex = 0;
            this.tvPatientList1.IsShowContextMenu = true;
            this.tvPatientList1.IsShowCount = true;
            this.tvPatientList1.IsShowNewPatient = true;
            this.tvPatientList1.IsShowPatientNo = true;
            this.tvPatientList1.Location = new System.Drawing.Point(276, 0);
            this.tvPatientList1.Name = "tvPatientList1";
            this.tvPatientList1.SelectedImageIndex = 0;
            this.tvPatientList1.ShowType = FS.HISFC.Components.Common.Controls.tvPatientList.enuShowType.Bed;
            this.tvPatientList1.Size = new System.Drawing.Size(285, 476);
            this.tvPatientList1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tvPatientList1.TabIndex = 35;
            this.tvPatientList1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvPatientList1_AfterSelect);
            // 
            // ucBabyInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tvPatientList1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitter1);
            this.Name = "ucBabyInfo";
            this.Size = new System.Drawing.Size(561, 476);
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.gpbPatientNO.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtMomInfo;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtInpatientNo;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtName;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.NeuNumericTextBox txtHeight;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        private FS.FrameWork.WinForms.Controls.NeuNumericTextBox txtWeight;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel7;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel8;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel9;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel10;
        private FS.FrameWork.WinForms.Controls.NeuButton btAdd;
        private FS.FrameWork.WinForms.Controls.NeuButton btSave;
        private FS.FrameWork.WinForms.Controls.NeuButton btCancel;
        private FS.FrameWork.WinForms.Controls.NeuButton btBabyInfo;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbSex;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtBirthday;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbBlood;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDept;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtOperatedate;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private System.Windows.Forms.Splitter splitter1;
        private FS.HISFC.Components.Common.Controls.ucQueryInpatientNo ucQueryInpatientNo;
        private System.Windows.Forms.GroupBox gpbPatientNO;
        private System.Windows.Forms.Panel panel1;
        private FS.HISFC.Components.Common.Controls.tvPatientList tvPatientList1;
        private FS.FrameWork.WinForms.Controls.NeuButton btRevokCancel;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDeliveryMode;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel11;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel12;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtGestationalAge;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbResponsibleDoc;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel13;
    }
}
