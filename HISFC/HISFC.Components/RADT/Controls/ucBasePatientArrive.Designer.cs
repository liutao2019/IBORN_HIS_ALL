namespace FS.HISFC.Components.RADT.Controls
{
    partial class ucBasePatientArrive
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
            this.txtPatientNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbBedNo = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblDoct = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbDoct = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lblAttendingDoc = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbAttendingDoc = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lblConsultingDoc = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbConsultingDoc = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.btOK = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.lblAdmittingNur = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbAdmittingNur = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.txtSex = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbDirector = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lblDirector = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbDiet = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lblTend = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbTend = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lblDiet = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.btOutBillPrint = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.cbxAddAllDoct = new System.Windows.Forms.CheckBox();
            this.cbxAddAllNurse = new System.Windows.Forms.CheckBox();
            this.neuLabel12 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbBedLevl = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lblBedLevlFee = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel13 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbBloodType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel14 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel15 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtPatientDept = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtPatientNueseCell = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.dtpInTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblResponsibeDoc = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbResponsibleDoc = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.SuspendLayout();
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(135)));
            this.neuLabel1.ForeColor = System.Drawing.Color.Black;
            this.neuLabel1.Location = new System.Drawing.Point(52, 30);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(71, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "住 院 号：";
            // 
            // txtPatientNo
            // 
            this.txtPatientNo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPatientNo.IsEnter2Tab = false;
            this.txtPatientNo.Location = new System.Drawing.Point(130, 25);
            this.txtPatientNo.Name = "txtPatientNo";
            this.txtPatientNo.ReadOnly = true;
            this.txtPatientNo.Size = new System.Drawing.Size(148, 21);
            this.txtPatientNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtPatientNo.TabIndex = 1;
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtName.IsEnter2Tab = false;
            this.txtName.Location = new System.Drawing.Point(130, 62);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(147, 21);
            this.txtName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtName.TabIndex = 3;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(135)));
            this.neuLabel2.ForeColor = System.Drawing.Color.Black;
            this.neuLabel2.Location = new System.Drawing.Point(52, 66);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(72, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 2;
            this.neuLabel2.Text = "姓    名：";
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(135)));
            this.neuLabel3.Location = new System.Drawing.Point(52, 102);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(72, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 4;
            this.neuLabel3.Text = "性    别：";
            // 
            // cmbBedNo
            // 
            this.cmbBedNo.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbBedNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbBedNo.FormattingEnabled = true;
            this.cmbBedNo.IsEnter2Tab = false;
            this.cmbBedNo.IsFlat = false;
            this.cmbBedNo.IsLike = true;
            this.cmbBedNo.IsListOnly = false;
            this.cmbBedNo.IsPopForm = true;
            this.cmbBedNo.IsShowCustomerList = false;
            this.cmbBedNo.IsShowID = false;
            this.cmbBedNo.IsShowIDAndName = false;
            this.cmbBedNo.Location = new System.Drawing.Point(130, 163);
            this.cmbBedNo.Name = "cmbBedNo";
            this.cmbBedNo.ShowCustomerList = false;
            this.cmbBedNo.ShowID = false;
            this.cmbBedNo.Size = new System.Drawing.Size(147, 20);
            this.cmbBedNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbBedNo.TabIndex = 6;
            this.cmbBedNo.Tag = "";
            this.cmbBedNo.ToolBarUse = false;
            this.cmbBedNo.SelectedIndexChanged += new System.EventHandler(this.cmbBedNo_SelectedIndexChanged);
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(135)));
            this.neuLabel4.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel4.Location = new System.Drawing.Point(52, 165);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(71, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 7;
            this.neuLabel4.Text = "病 床 号：";
            // 
            // lblDoct
            // 
            this.lblDoct.AutoSize = true;
            this.lblDoct.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(135)));
            this.lblDoct.Location = new System.Drawing.Point(52, 235);
            this.lblDoct.Name = "lblDoct";
            this.lblDoct.Size = new System.Drawing.Size(96, 12);
            this.lblDoct.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblDoct.TabIndex = 9;
            this.lblDoct.Text = "产科责任医师：";
            // 
            // cmbDoct
            // 
            this.cmbDoct.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbDoct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbDoct.FormattingEnabled = true;
            this.cmbDoct.IsEnter2Tab = false;
            this.cmbDoct.IsFlat = false;
            this.cmbDoct.IsLike = true;
            this.cmbDoct.IsListOnly = false;
            this.cmbDoct.IsPopForm = true;
            this.cmbDoct.IsShowCustomerList = false;
            this.cmbDoct.IsShowID = false;
            this.cmbDoct.IsShowIDAndName = false;
            this.cmbDoct.Location = new System.Drawing.Point(151, 235);
            this.cmbDoct.Name = "cmbDoct";
            this.cmbDoct.ShowCustomerList = false;
            this.cmbDoct.ShowID = false;
            this.cmbDoct.Size = new System.Drawing.Size(126, 20);
            this.cmbDoct.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbDoct.TabIndex = 13;
            this.cmbDoct.Tag = "";
            this.cmbDoct.ToolBarUse = false;
            // 
            // lblAttendingDoc
            // 
            this.lblAttendingDoc.AutoSize = true;
            this.lblAttendingDoc.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(135)));
            this.lblAttendingDoc.ForeColor = System.Drawing.Color.Blue;
            this.lblAttendingDoc.Location = new System.Drawing.Point(52, 271);
            this.lblAttendingDoc.Name = "lblAttendingDoc";
            this.lblAttendingDoc.Size = new System.Drawing.Size(70, 12);
            this.lblAttendingDoc.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblAttendingDoc.TabIndex = 11;
            this.lblAttendingDoc.Text = "主治医师：";
            // 
            // cmbAttendingDoc
            // 
            this.cmbAttendingDoc.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbAttendingDoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbAttendingDoc.FormattingEnabled = true;
            this.cmbAttendingDoc.IsEnter2Tab = false;
            this.cmbAttendingDoc.IsFlat = false;
            this.cmbAttendingDoc.IsLike = true;
            this.cmbAttendingDoc.IsListOnly = false;
            this.cmbAttendingDoc.IsPopForm = true;
            this.cmbAttendingDoc.IsShowCustomerList = false;
            this.cmbAttendingDoc.IsShowID = false;
            this.cmbAttendingDoc.IsShowIDAndName = false;
            this.cmbAttendingDoc.Location = new System.Drawing.Point(130, 271);
            this.cmbAttendingDoc.Name = "cmbAttendingDoc";
            this.cmbAttendingDoc.ShowCustomerList = false;
            this.cmbAttendingDoc.ShowID = false;
            this.cmbAttendingDoc.Size = new System.Drawing.Size(147, 20);
            this.cmbAttendingDoc.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbAttendingDoc.TabIndex = 14;
            this.cmbAttendingDoc.Tag = "";
            this.cmbAttendingDoc.ToolBarUse = false;
            // 
            // lblConsultingDoc
            // 
            this.lblConsultingDoc.AutoSize = true;
            this.lblConsultingDoc.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(135)));
            this.lblConsultingDoc.Location = new System.Drawing.Point(52, 308);
            this.lblConsultingDoc.Name = "lblConsultingDoc";
            this.lblConsultingDoc.Size = new System.Drawing.Size(70, 12);
            this.lblConsultingDoc.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblConsultingDoc.TabIndex = 13;
            this.lblConsultingDoc.Text = "主任医师：";
            // 
            // cmbConsultingDoc
            // 
            this.cmbConsultingDoc.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbConsultingDoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbConsultingDoc.FormattingEnabled = true;
            this.cmbConsultingDoc.IsEnter2Tab = false;
            this.cmbConsultingDoc.IsFlat = false;
            this.cmbConsultingDoc.IsLike = true;
            this.cmbConsultingDoc.IsListOnly = false;
            this.cmbConsultingDoc.IsPopForm = true;
            this.cmbConsultingDoc.IsShowCustomerList = false;
            this.cmbConsultingDoc.IsShowID = false;
            this.cmbConsultingDoc.IsShowIDAndName = false;
            this.cmbConsultingDoc.Location = new System.Drawing.Point(130, 307);
            this.cmbConsultingDoc.Name = "cmbConsultingDoc";
            this.cmbConsultingDoc.ShowCustomerList = false;
            this.cmbConsultingDoc.ShowID = false;
            this.cmbConsultingDoc.Size = new System.Drawing.Size(147, 20);
            this.cmbConsultingDoc.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbConsultingDoc.TabIndex = 15;
            this.cmbConsultingDoc.Tag = "";
            this.cmbConsultingDoc.ToolBarUse = false;
            // 
            // btOK
            // 
            this.btOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btOK.Location = new System.Drawing.Point(151, 640);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(74, 26);
            this.btOK.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btOK.TabIndex = 14;
            this.btOK.Text = "确定(&O)";
            this.btOK.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.neuButton1_Click);
            // 
            // lblAdmittingNur
            // 
            this.lblAdmittingNur.AutoSize = true;
            this.lblAdmittingNur.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(135)));
            this.lblAdmittingNur.Location = new System.Drawing.Point(52, 368);
            this.lblAdmittingNur.Name = "lblAdmittingNur";
            this.lblAdmittingNur.Size = new System.Drawing.Size(70, 12);
            this.lblAdmittingNur.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblAdmittingNur.TabIndex = 16;
            this.lblAdmittingNur.Text = "责任护士：";
            // 
            // cmbAdmittingNur
            // 
            this.cmbAdmittingNur.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbAdmittingNur.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbAdmittingNur.FormattingEnabled = true;
            this.cmbAdmittingNur.IsEnter2Tab = false;
            this.cmbAdmittingNur.IsFlat = false;
            this.cmbAdmittingNur.IsLike = true;
            this.cmbAdmittingNur.IsListOnly = false;
            this.cmbAdmittingNur.IsPopForm = true;
            this.cmbAdmittingNur.IsShowCustomerList = false;
            this.cmbAdmittingNur.IsShowID = false;
            this.cmbAdmittingNur.IsShowIDAndName = false;
            this.cmbAdmittingNur.Location = new System.Drawing.Point(130, 366);
            this.cmbAdmittingNur.Name = "cmbAdmittingNur";
            this.cmbAdmittingNur.ShowCustomerList = false;
            this.cmbAdmittingNur.ShowID = false;
            this.cmbAdmittingNur.Size = new System.Drawing.Size(147, 20);
            this.cmbAdmittingNur.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbAdmittingNur.TabIndex = 16;
            this.cmbAdmittingNur.Tag = "";
            this.cmbAdmittingNur.ToolBarUse = false;
            // 
            // txtSex
            // 
            this.txtSex.ArrowBackColor = System.Drawing.Color.Silver;
            this.txtSex.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtSex.Enabled = false;
            this.txtSex.FormattingEnabled = true;
            this.txtSex.IsEnter2Tab = false;
            this.txtSex.IsFlat = false;
            this.txtSex.IsLike = true;
            this.txtSex.IsListOnly = false;
            this.txtSex.IsPopForm = true;
            this.txtSex.IsShowCustomerList = false;
            this.txtSex.IsShowID = false;
            this.txtSex.IsShowIDAndName = false;
            this.txtSex.Location = new System.Drawing.Point(130, 101);
            this.txtSex.Name = "txtSex";
            this.txtSex.ShowCustomerList = false;
            this.txtSex.ShowID = false;
            this.txtSex.Size = new System.Drawing.Size(147, 20);
            this.txtSex.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.txtSex.TabIndex = 11;
            this.txtSex.Tag = "";
            this.txtSex.ToolBarUse = false;
            // 
            // cmbDirector
            // 
            this.cmbDirector.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbDirector.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbDirector.FormattingEnabled = true;
            this.cmbDirector.IsEnter2Tab = false;
            this.cmbDirector.IsFlat = false;
            this.cmbDirector.IsLike = true;
            this.cmbDirector.IsListOnly = false;
            this.cmbDirector.IsPopForm = true;
            this.cmbDirector.IsShowCustomerList = false;
            this.cmbDirector.IsShowID = false;
            this.cmbDirector.IsShowIDAndName = false;
            this.cmbDirector.Location = new System.Drawing.Point(128, 574);
            this.cmbDirector.Name = "cmbDirector";
            this.cmbDirector.ShowCustomerList = false;
            this.cmbDirector.ShowID = false;
            this.cmbDirector.Size = new System.Drawing.Size(149, 20);
            this.cmbDirector.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbDirector.TabIndex = 19;
            this.cmbDirector.Tag = "";
            this.cmbDirector.ToolBarUse = false;
            this.cmbDirector.Visible = false;
            // 
            // lblDirector
            // 
            this.lblDirector.AutoSize = true;
            this.lblDirector.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(135)));
            this.lblDirector.Location = new System.Drawing.Point(51, 579);
            this.lblDirector.Name = "lblDirector";
            this.lblDirector.Size = new System.Drawing.Size(71, 12);
            this.lblDirector.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblDirector.TabIndex = 19;
            this.lblDirector.Text = "科 主 任：";
            this.lblDirector.Visible = false;
            // 
            // cmbDiet
            // 
            this.cmbDiet.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbDiet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbDiet.FormattingEnabled = true;
            this.cmbDiet.IsEnter2Tab = false;
            this.cmbDiet.IsFlat = false;
            this.cmbDiet.IsLike = true;
            this.cmbDiet.IsListOnly = false;
            this.cmbDiet.IsPopForm = true;
            this.cmbDiet.IsShowCustomerList = false;
            this.cmbDiet.IsShowID = false;
            this.cmbDiet.IsShowIDAndName = false;
            this.cmbDiet.Location = new System.Drawing.Point(130, 436);
            this.cmbDiet.Name = "cmbDiet";
            this.cmbDiet.ShowCustomerList = false;
            this.cmbDiet.ShowID = false;
            this.cmbDiet.Size = new System.Drawing.Size(148, 20);
            this.cmbDiet.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbDiet.TabIndex = 18;
            this.cmbDiet.Tag = "";
            this.cmbDiet.ToolBarUse = false;
            // 
            // lblTend
            // 
            this.lblTend.AutoSize = true;
            this.lblTend.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(135)));
            this.lblTend.Location = new System.Drawing.Point(52, 402);
            this.lblTend.Name = "lblTend";
            this.lblTend.Size = new System.Drawing.Size(70, 12);
            this.lblTend.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblTend.TabIndex = 21;
            this.lblTend.Text = "护理级别：";
            // 
            // cmbTend
            // 
            this.cmbTend.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbTend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbTend.FormattingEnabled = true;
            this.cmbTend.IsEnter2Tab = false;
            this.cmbTend.IsFlat = false;
            this.cmbTend.IsLike = true;
            this.cmbTend.IsListOnly = false;
            this.cmbTend.IsPopForm = true;
            this.cmbTend.IsShowCustomerList = false;
            this.cmbTend.IsShowID = false;
            this.cmbTend.IsShowIDAndName = false;
            this.cmbTend.Location = new System.Drawing.Point(130, 400);
            this.cmbTend.Name = "cmbTend";
            this.cmbTend.ShowCustomerList = false;
            this.cmbTend.ShowID = false;
            this.cmbTend.Size = new System.Drawing.Size(147, 20);
            this.cmbTend.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbTend.TabIndex = 17;
            this.cmbTend.Tag = "";
            this.cmbTend.ToolBarUse = false;
            // 
            // lblDiet
            // 
            this.lblDiet.AutoSize = true;
            this.lblDiet.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(135)));
            this.lblDiet.Location = new System.Drawing.Point(52, 439);
            this.lblDiet.Name = "lblDiet";
            this.lblDiet.Size = new System.Drawing.Size(72, 12);
            this.lblDiet.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblDiet.TabIndex = 23;
            this.lblDiet.Text = "饮    食：";
            // 
            // btOutBillPrint
            // 
            this.btOutBillPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btOutBillPrint.Location = new System.Drawing.Point(48, 640);
            this.btOutBillPrint.Name = "btOutBillPrint";
            this.btOutBillPrint.Size = new System.Drawing.Size(74, 26);
            this.btOutBillPrint.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btOutBillPrint.TabIndex = 24;
            this.btOutBillPrint.Text = "出院通知单";
            this.btOutBillPrint.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btOutBillPrint.UseVisualStyleBackColor = true;
            this.btOutBillPrint.Visible = false;
            this.btOutBillPrint.Click += new System.EventHandler(this.btOutBillPrint_Click);
            // 
            // cbxAddAllDoct
            // 
            this.cbxAddAllDoct.AutoSize = true;
            this.cbxAddAllDoct.ForeColor = System.Drawing.Color.Blue;
            this.cbxAddAllDoct.Location = new System.Drawing.Point(48, 607);
            this.cbxAddAllDoct.Name = "cbxAddAllDoct";
            this.cbxAddAllDoct.Size = new System.Drawing.Size(96, 16);
            this.cbxAddAllDoct.TabIndex = 25;
            this.cbxAddAllDoct.Text = "显示所有医生";
            this.cbxAddAllDoct.UseVisualStyleBackColor = true;
            // 
            // cbxAddAllNurse
            // 
            this.cbxAddAllNurse.AutoSize = true;
            this.cbxAddAllNurse.ForeColor = System.Drawing.Color.Blue;
            this.cbxAddAllNurse.Location = new System.Drawing.Point(174, 607);
            this.cbxAddAllNurse.Name = "cbxAddAllNurse";
            this.cbxAddAllNurse.Size = new System.Drawing.Size(96, 16);
            this.cbxAddAllNurse.TabIndex = 26;
            this.cbxAddAllNurse.Text = "显示所有护士";
            this.cbxAddAllNurse.UseVisualStyleBackColor = true;
            // 
            // neuLabel12
            // 
            this.neuLabel12.AutoSize = true;
            this.neuLabel12.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(135)));
            this.neuLabel12.Location = new System.Drawing.Point(52, 200);
            this.neuLabel12.Name = "neuLabel12";
            this.neuLabel12.Size = new System.Drawing.Size(70, 12);
            this.neuLabel12.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel12.TabIndex = 28;
            this.neuLabel12.Text = "床位等级：";
            // 
            // cmbBedLevl
            // 
            this.cmbBedLevl.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbBedLevl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbBedLevl.FormattingEnabled = true;
            this.cmbBedLevl.IsEnter2Tab = false;
            this.cmbBedLevl.IsFlat = false;
            this.cmbBedLevl.IsLike = true;
            this.cmbBedLevl.IsListOnly = false;
            this.cmbBedLevl.IsPopForm = true;
            this.cmbBedLevl.IsShowCustomerList = false;
            this.cmbBedLevl.IsShowID = false;
            this.cmbBedLevl.IsShowIDAndName = false;
            this.cmbBedLevl.Location = new System.Drawing.Point(130, 199);
            this.cmbBedLevl.Name = "cmbBedLevl";
            this.cmbBedLevl.ShowCustomerList = false;
            this.cmbBedLevl.ShowID = false;
            this.cmbBedLevl.Size = new System.Drawing.Size(147, 20);
            this.cmbBedLevl.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbBedLevl.TabIndex = 12;
            this.cmbBedLevl.Tag = "";
            this.cmbBedLevl.ToolBarUse = false;
            // 
            // lblBedLevlFee
            // 
            this.lblBedLevlFee.AutoSize = true;
            this.lblBedLevlFee.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBedLevlFee.ForeColor = System.Drawing.Color.Red;
            this.lblBedLevlFee.Location = new System.Drawing.Point(286, 202);
            this.lblBedLevlFee.Name = "lblBedLevlFee";
            this.lblBedLevlFee.Size = new System.Drawing.Size(70, 12);
            this.lblBedLevlFee.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblBedLevlFee.TabIndex = 29;
            this.lblBedLevlFee.Text = "床位等级：";
            // 
            // neuLabel13
            // 
            this.neuLabel13.AutoSize = true;
            this.neuLabel13.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(135)));
            this.neuLabel13.Location = new System.Drawing.Point(52, 133);
            this.neuLabel13.Name = "neuLabel13";
            this.neuLabel13.Size = new System.Drawing.Size(72, 12);
            this.neuLabel13.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel13.TabIndex = 4;
            this.neuLabel13.Text = "血    型：";
            // 
            // cmbBloodType
            // 
            this.cmbBloodType.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbBloodType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbBloodType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBloodType.FormattingEnabled = true;
            this.cmbBloodType.IsEnter2Tab = false;
            this.cmbBloodType.IsFlat = false;
            this.cmbBloodType.IsLike = true;
            this.cmbBloodType.IsListOnly = false;
            this.cmbBloodType.IsPopForm = true;
            this.cmbBloodType.IsShowCustomerList = false;
            this.cmbBloodType.IsShowID = false;
            this.cmbBloodType.IsShowIDAndName = false;
            this.cmbBloodType.Location = new System.Drawing.Point(130, 130);
            this.cmbBloodType.Name = "cmbBloodType";
            this.cmbBloodType.ShowCustomerList = false;
            this.cmbBloodType.ShowID = false;
            this.cmbBloodType.Size = new System.Drawing.Size(148, 20);
            this.cmbBloodType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbBloodType.TabIndex = 6;
            this.cmbBloodType.Tag = "";
            this.cmbBloodType.ToolBarUse = false;
            // 
            // neuLabel14
            // 
            this.neuLabel14.AutoSize = true;
            this.neuLabel14.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(135)));
            this.neuLabel14.Location = new System.Drawing.Point(53, 476);
            this.neuLabel14.Name = "neuLabel14";
            this.neuLabel14.Size = new System.Drawing.Size(70, 12);
            this.neuLabel14.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel14.TabIndex = 30;
            this.neuLabel14.Text = "所在科室：";
            // 
            // neuLabel15
            // 
            this.neuLabel15.AutoSize = true;
            this.neuLabel15.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(135)));
            this.neuLabel15.Location = new System.Drawing.Point(52, 514);
            this.neuLabel15.Name = "neuLabel15";
            this.neuLabel15.Size = new System.Drawing.Size(70, 12);
            this.neuLabel15.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel15.TabIndex = 31;
            this.neuLabel15.Text = "所在病区：";
            // 
            // txtPatientDept
            // 
            this.txtPatientDept.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPatientDept.IsEnter2Tab = false;
            this.txtPatientDept.Location = new System.Drawing.Point(129, 473);
            this.txtPatientDept.Name = "txtPatientDept";
            this.txtPatientDept.ReadOnly = true;
            this.txtPatientDept.Size = new System.Drawing.Size(148, 21);
            this.txtPatientDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtPatientDept.TabIndex = 32;
            // 
            // txtPatientNueseCell
            // 
            this.txtPatientNueseCell.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPatientNueseCell.IsEnter2Tab = false;
            this.txtPatientNueseCell.Location = new System.Drawing.Point(128, 511);
            this.txtPatientNueseCell.Name = "txtPatientNueseCell";
            this.txtPatientNueseCell.ReadOnly = true;
            this.txtPatientNueseCell.Size = new System.Drawing.Size(149, 21);
            this.txtPatientNueseCell.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtPatientNueseCell.TabIndex = 33;
            // 
            // dtpInTime
            // 
            this.dtpInTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpInTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpInTime.IsEnter2Tab = false;
            this.dtpInTime.Location = new System.Drawing.Point(128, 539);
            this.dtpInTime.Name = "dtpInTime";
            this.dtpInTime.Size = new System.Drawing.Size(150, 21);
            this.dtpInTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpInTime.TabIndex = 129;
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(135)));
            this.neuLabel5.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel5.Location = new System.Drawing.Point(51, 545);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(70, 12);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 130;
            this.neuLabel5.Text = "接诊时间：";
            // 
            // lblResponsibeDoc
            // 
            this.lblResponsibeDoc.AutoSize = true;
            this.lblResponsibeDoc.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(135)));
            this.lblResponsibeDoc.Location = new System.Drawing.Point(50, 341);
            this.lblResponsibeDoc.Name = "lblResponsibeDoc";
            this.lblResponsibeDoc.Size = new System.Drawing.Size(96, 12);
            this.lblResponsibeDoc.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblResponsibeDoc.TabIndex = 131;
            this.lblResponsibeDoc.Text = "儿科责任医师：";
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
            this.cmbResponsibleDoc.Location = new System.Drawing.Point(151, 338);
            this.cmbResponsibleDoc.Name = "cmbResponsibleDoc";
            this.cmbResponsibleDoc.ShowCustomerList = false;
            this.cmbResponsibleDoc.ShowID = false;
            this.cmbResponsibleDoc.Size = new System.Drawing.Size(126, 20);
            this.cmbResponsibleDoc.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbResponsibleDoc.TabIndex = 132;
            this.cmbResponsibleDoc.Tag = "";
            this.cmbResponsibleDoc.ToolBarUse = false;
            // 
            // ucBasePatientArrive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lblResponsibeDoc);
            this.Controls.Add(this.cmbResponsibleDoc);
            this.Controls.Add(this.neuLabel5);
            this.Controls.Add(this.dtpInTime);
            this.Controls.Add(this.txtPatientNueseCell);
            this.Controls.Add(this.txtPatientDept);
            this.Controls.Add(this.neuLabel15);
            this.Controls.Add(this.neuLabel14);
            this.Controls.Add(this.lblBedLevlFee);
            this.Controls.Add(this.neuLabel12);
            this.Controls.Add(this.cmbBedLevl);
            this.Controls.Add(this.cbxAddAllNurse);
            this.Controls.Add(this.cbxAddAllDoct);
            this.Controls.Add(this.btOutBillPrint);
            this.Controls.Add(this.lblDiet);
            this.Controls.Add(this.cmbDiet);
            this.Controls.Add(this.lblTend);
            this.Controls.Add(this.cmbTend);
            this.Controls.Add(this.lblDirector);
            this.Controls.Add(this.cmbDirector);
            this.Controls.Add(this.txtSex);
            this.Controls.Add(this.lblAdmittingNur);
            this.Controls.Add(this.cmbAdmittingNur);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.lblConsultingDoc);
            this.Controls.Add(this.cmbConsultingDoc);
            this.Controls.Add(this.lblAttendingDoc);
            this.Controls.Add(this.cmbAttendingDoc);
            this.Controls.Add(this.lblDoct);
            this.Controls.Add(this.cmbDoct);
            this.Controls.Add(this.neuLabel4);
            this.Controls.Add(this.cmbBloodType);
            this.Controls.Add(this.cmbBedNo);
            this.Controls.Add(this.neuLabel13);
            this.Controls.Add(this.neuLabel3);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.neuLabel2);
            this.Controls.Add(this.txtPatientNo);
            this.Controls.Add(this.neuLabel1);
            this.Name = "ucBasePatientArrive";
            this.Size = new System.Drawing.Size(376, 680);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtPatientNo;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtName;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbBedNo;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblDoct;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDoct;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblAttendingDoc;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbAttendingDoc;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblConsultingDoc;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbConsultingDoc;
        private FS.FrameWork.WinForms.Controls.NeuButton btOK;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblAdmittingNur;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbAdmittingNur;
        private FS.FrameWork.WinForms.Controls.NeuComboBox txtSex;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDirector;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblDirector;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDiet;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblTend;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbTend;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblDiet;
        private FS.FrameWork.WinForms.Controls.NeuButton btOutBillPrint;
        private System.Windows.Forms.CheckBox cbxAddAllDoct;
        private System.Windows.Forms.CheckBox cbxAddAllNurse;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel12;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbBedLevl;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblBedLevlFee;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel13;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbBloodType;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel14;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel15;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtPatientDept;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtPatientNueseCell;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpInTime;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblResponsibeDoc;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbResponsibleDoc;
    }
}
