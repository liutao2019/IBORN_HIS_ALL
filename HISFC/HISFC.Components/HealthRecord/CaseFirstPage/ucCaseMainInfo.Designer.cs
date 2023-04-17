namespace FS.HISFC.Components.HealthRecord.CaseFirstPage
{
    partial class ucCaseMainInfo
    {
        /// <summary> 
        /// 必需的设计器变量。


        /// </summary>
        private FS.FrameWork.WinForms.Controls.NeuPanel panel1;
        private FS.FrameWork.WinForms.Controls.NeuFpEnter fpEnter1;
        private FS.FrameWork.WinForms.Controls.NeuPanel panel3;
        private System.Windows.Forms.ImageList imageList32;
        private System.Windows.Forms.TabPage tabPage1;
        private CustomListBox txtRelation;
        private FS.FrameWork.WinForms.Controls.NeuLabel label97;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtXNumb;
        private FS.FrameWork.WinForms.Controls.NeuLabel label95;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtMriNumb;
        private FS.FrameWork.WinForms.Controls.NeuLabel label94;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.TabControl tab1;
        private HealthRecord.CaseFirstPage.ucTumourCard ucTumourCard2;
        private HealthRecord.CaseFirstPage.ucDiagNoseInput ucDiagNoseInput1;
        private HealthRecord.CaseFirstPage.ucBabyCardInput ucBabyCardInput1;
        //private HealthRecord.CaseFirstPage.ucDiagNoseInput ucBabyCardInput1;

        private HealthRecord.CaseFirstPage.ucOperation ucOperation1;
        private System.Windows.Forms.TabPage tabPage3;
        private HealthRecord.CaseFirstPage.ucChangeDept ucChangeDept1;
        private System.Windows.Forms.TabPage tabPage4;
        private HealthRecord.CaseFirstPage.ucFeeInfo ucFeeInfo1;
        private System.ComponentModel.IContainer components;

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
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            this.imageList32 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.panel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.tab1 = new System.Windows.Forms.TabControl();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.neuGroupBox10 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.btnSave = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuGroupBox9 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.cmsMain = new FS.FrameWork.WinForms.Controls.NeuContextMenuStrip();
            this.tsmAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSave = new System.Windows.Forms.ToolStripMenuItem();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuGroupBox8 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.dtpVisitTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel8 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.rdbStop = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.rdbNormal = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.lblState = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbResult = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.txtContent = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblContent = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbLinkType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lblLinkwayType = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtWritebackPerson = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblWritebackPerson = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabelTransferPosition = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuComboBoxTransferPosition = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbSymptom = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lblSymptom = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabelSequela = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuComboBoxSequela = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabelrecrudescdeTime = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuDateTimePickerRecrudesceTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuCheckBoxIsTtransfer = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.lbllResult = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuCheckBoxIsSequela = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.neuCheckBoxIsRecrudesce = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.neuComboBoxDeadReason = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabelDeadReason = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuDateTimePickerDeadTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabelDeadTime = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuCheckBoxIsDead = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.cmbCircs = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lblCircs = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.neuGroupBox11 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.txtRemark = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel14 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtCaseStus = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel9 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.txtPatientAge = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.cmbUnit = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.txtLinkmanAdd = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.labl = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtAreaCode = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtDIST = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtSSN = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtLinkmanTel = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label25 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label24 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtKin = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label23 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtBusinessZip = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtHomeZip = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label22 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtAddressHome = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label20 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label19 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtPhoneBusiness = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label18 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtAddressBusiness = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label17 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtIDNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label16 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label15 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtPatientName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label13 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label12 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label11 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label10 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label9 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dtPatientBirthday = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.label8 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label7 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtClinicNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtInTimes = new FS.FrameWork.WinForms.Controls.ValidatedTextBox();
            this.label2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtCaseNum = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtPhoneHome = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label21 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label14 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtCountry = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.txtProfession = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.txtNationality = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.txtMaritalStatus = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.txtPatientSex = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.txtPactKind = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.txtNomen = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label97 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtRelation = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.neuGroupBox2 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.txtOutRoom = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtInRoom = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label89 = new System.Windows.Forms.Label();
            this.txtComeTo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label86 = new System.Windows.Forms.Label();
            this.txtDeptChiefDoc = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.label49 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label61 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label60 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label42 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label41 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label40 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtPiDays = new FS.FrameWork.WinForms.Controls.ValidatedTextBox();
            this.label38 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtDiagDate = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.label37 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtDateOut = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.txtComeFrom = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label64 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label43 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label36 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label35 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtGraDocCode = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.label28 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dtDateIn = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.label27 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label26 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label63 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label62 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtDeptInHospital = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.txtDeptOut = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.txtCircs = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.txtRefresherDocd = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.txtAdmittingDoctor = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.txtAttendingDoctor = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.txtInAvenue = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.txtConsultingDoctor = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.txtPraDocCode = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.txtClinicDocd = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.txtRuyuanDiagNose = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtClinicDiag = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel17 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel16 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label39 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuGroupBox3 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.label30 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dtThird = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.label33 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label34 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dtSecond = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.label31 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label32 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dtFirstTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.label29 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtDeptThird = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.txtFirstDept = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.txtDeptSecond = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.neuGroupBox4 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuTxtPharmacyAllergic2 = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuTxtPharmacyAllergic1 = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtInjuryOrPoisoningCause = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.neuLabel10 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtInfectionPositionNew = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtInfectNum = new FS.FrameWork.WinForms.Controls.ValidatedTextBox();
            this.label44 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtPharmacyAllergic2 = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtPharmacyAllergic1 = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuGroupBox5 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.txtSuccTimes = new FS.FrameWork.WinForms.Controls.ValidatedTextBox();
            this.label56 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtSalvTimes = new FS.FrameWork.WinForms.Controls.ValidatedTextBox();
            this.label55 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label48 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label54 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label47 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label53 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label46 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label52 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label51 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtHivAb = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.label50 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtHcvAb = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.txtFsBl = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.txtHbsag = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.txtClPa = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.txtOpbOpa = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.txtPiPo = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.txtCePi = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.neuGroupBox6 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuLabel15 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtReactionTransfuse = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.textBox13 = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.textBox12 = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.textBox11 = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.textBox10 = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.textBox9 = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.textBox8 = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.textBox7 = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.textBox6 = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.textBox5 = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.textBox4 = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.textBox3 = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.textBox2 = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.textBox1 = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.cbYnFirst = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.txtPathNumb = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtCtNumb = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label92 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtSPecalNus = new FS.FrameWork.WinForms.Controls.ValidatedTextBox();
            this.label90 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtStrictNuss = new FS.FrameWork.WinForms.Controls.ValidatedTextBox();
            this.label91 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtIIINus = new FS.FrameWork.WinForms.Controls.ValidatedTextBox();
            this.txtIINus = new FS.FrameWork.WinForms.Controls.ValidatedTextBox();
            this.label88 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtINus = new FS.FrameWork.WinForms.Controls.ValidatedTextBox();
            this.label85 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtSuperNus = new FS.FrameWork.WinForms.Controls.ValidatedTextBox();
            this.label82 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtOutconNum = new FS.FrameWork.WinForms.Controls.ValidatedTextBox();
            this.label83 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtInconNum = new FS.FrameWork.WinForms.Controls.ValidatedTextBox();
            this.txtBloodOther = new FS.FrameWork.WinForms.Controls.ValidatedTextBox();
            this.label81 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtBloodWhole = new FS.FrameWork.WinForms.Controls.ValidatedTextBox();
            this.label80 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtBodyAnotomize = new FS.FrameWork.WinForms.Controls.ValidatedTextBox();
            this.label78 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtBloodPlatelet = new FS.FrameWork.WinForms.Controls.ValidatedTextBox();
            this.label77 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtBloodRed = new FS.FrameWork.WinForms.Controls.ValidatedTextBox();
            this.label76 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel12 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel11 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label75 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label74 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label73 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cbDisease30 = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.cbTechSerc = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.label72 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label71 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label70 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtVisiPeriYear = new FS.FrameWork.WinForms.Controls.ValidatedTextBox();
            this.txtVisiPeriMonth = new FS.FrameWork.WinForms.Controls.ValidatedTextBox();
            this.txtVisiPeriWeek = new FS.FrameWork.WinForms.Controls.ValidatedTextBox();
            this.label69 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cbVisiStat = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.cbBodyCheck = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.label87 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label84 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtFourDiseasesReport = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.txtInfectionDiseasesReport = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.txtReactionBlood = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.txtRhBlood = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.txtBloodType = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.txtPETNumb = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel7 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtECTNumb = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtMriNumb = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label94 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtXNumb = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label95 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtBC = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuGroupBox7 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.txtOperationCode = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.txtCodingCode = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.label45 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label96 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtCheckDate = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.label65 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label59 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label58 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label57 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label66 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label67 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtQcDocd = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.txtInputDoc = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.txtCoordinate = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.txtQcNucd = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.txtMrQual = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.ucDiagNoseInput1 = new FS.HISFC.Components.HealthRecord.CaseFirstPage.ucDiagNoseInput();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.ucOperation1 = new FS.HISFC.Components.HealthRecord.CaseFirstPage.ucOperation();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ucBabyCardInput1 = new FS.HISFC.Components.HealthRecord.CaseFirstPage.ucBabyCardInput();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.ucChangeDept1 = new FS.HISFC.Components.HealthRecord.CaseFirstPage.ucChangeDept();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.ucTumourCard2 = new FS.HISFC.Components.HealthRecord.CaseFirstPage.ucTumourCard();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.ucFeeInfo1 = new FS.HISFC.Components.HealthRecord.CaseFirstPage.ucFeeInfo();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNameSearch = new System.Windows.Forms.TextBox();
            this.txtCaseNOSearch = new System.Windows.Forms.TextBox();
            this.neuLabel13 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtPatientNOSearch = new System.Windows.Forms.TextBox();
            this.label79 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label68 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.fpEnter1 = new FS.FrameWork.WinForms.Controls.NeuFpEnter(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tab1.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.neuGroupBox10.SuspendLayout();
            this.neuGroupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            this.cmsMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.neuGroupBox8.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.neuGroupBox11.SuspendLayout();
            this.neuGroupBox1.SuspendLayout();
            this.neuGroupBox2.SuspendLayout();
            this.neuGroupBox3.SuspendLayout();
            this.neuGroupBox4.SuspendLayout();
            this.neuGroupBox5.SuspendLayout();
            this.neuGroupBox6.SuspendLayout();
            this.neuGroupBox7.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpEnter1)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList32
            // 
            this.imageList32.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList32.ImageSize = new System.Drawing.Size(32, 32);
            this.imageList32.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 600);
            this.panel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panel1.TabIndex = 5;
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.BackColor = System.Drawing.Color.Azure;
            this.panel3.Controls.Add(this.tab1);
            this.panel3.Controls.Add(this.splitter1);
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1000, 600);
            this.panel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panel3.TabIndex = 9;
            // 
            // tab1
            // 
            this.tab1.Controls.Add(this.tabPage8);
            this.tab1.Controls.Add(this.tabPage1);
            this.tab1.Controls.Add(this.tabPage5);
            this.tab1.Controls.Add(this.tabPage6);
            this.tab1.Controls.Add(this.tabPage2);
            this.tab1.Controls.Add(this.tabPage3);
            this.tab1.Controls.Add(this.tabPage7);
            this.tab1.Controls.Add(this.tabPage4);
            this.tab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab1.ItemSize = new System.Drawing.Size(60, 17);
            this.tab1.Location = new System.Drawing.Point(221, 0);
            this.tab1.Name = "tab1";
            this.tab1.SelectedIndex = 0;
            this.tab1.Size = new System.Drawing.Size(779, 600);
            this.tab1.TabIndex = 7;
            this.tab1.TabStop = false;
            this.tab1.SelectedIndexChanged += new System.EventHandler(this.tab1_SelectedIndexChanged);
            // 
            // tabPage8
            // 
            this.tabPage8.BackColor = System.Drawing.Color.Azure;
            this.tabPage8.Controls.Add(this.neuGroupBox10);
            this.tabPage8.Controls.Add(this.neuGroupBox9);
            this.tabPage8.Controls.Add(this.neuGroupBox8);
            this.tabPage8.Location = new System.Drawing.Point(4, 21);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage8.Size = new System.Drawing.Size(771, 575);
            this.tabPage8.TabIndex = 7;
            this.tabPage8.Text = "随访信息";
            // 
            // neuGroupBox10
            // 
            this.neuGroupBox10.Controls.Add(this.btnSave);
            this.neuGroupBox10.Location = new System.Drawing.Point(3, 514);
            this.neuGroupBox10.Name = "neuGroupBox10";
            this.neuGroupBox10.Size = new System.Drawing.Size(769, 57);
            this.neuGroupBox10.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox10.TabIndex = 504;
            this.neuGroupBox10.TabStop = false;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(687, 20);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "保存";
            this.btnSave.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // neuGroupBox9
            // 
            this.neuGroupBox9.Controls.Add(this.neuSpread1);
            this.neuGroupBox9.Location = new System.Drawing.Point(0, 311);
            this.neuGroupBox9.Name = "neuGroupBox9";
            this.neuGroupBox9.Size = new System.Drawing.Size(771, 197);
            this.neuGroupBox9.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox9.TabIndex = 503;
            this.neuGroupBox9.TabStop = false;
            this.neuGroupBox9.Text = "随访联系人(右键单击修改)";
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.ContextMenuStrip = this.cmsMain;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(3, 17);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(765, 177);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // cmsMain
            // 
            this.cmsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmAdd,
            this.tsmDel,
            this.tsmSave});
            this.cmsMain.Name = "cmsAdd";
            this.cmsMain.Size = new System.Drawing.Size(95, 70);
            this.cmsMain.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmsMain.Text = "增加";
            this.cmsMain.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cmsMain_ItemClicked);
            // 
            // tsmAdd
            // 
            this.tsmAdd.Name = "tsmAdd";
            this.tsmAdd.Size = new System.Drawing.Size(94, 22);
            this.tsmAdd.Text = "新增";
            // 
            // tsmDel
            // 
            this.tsmDel.Name = "tsmDel";
            this.tsmDel.Size = new System.Drawing.Size(94, 22);
            this.tsmDel.Text = "删除";
            // 
            // tsmSave
            // 
            this.tsmSave.Name = "tsmSave";
            this.tsmSave.Size = new System.Drawing.Size(94, 22);
            this.tsmSave.Text = "保存";
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ColumnCount = 7;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "选择";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "联系人";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "与患者关系";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "联系电话";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "电话状态";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "联系地址";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "电子邮件";
            this.neuSpread1_Sheet1.Columns.Get(0).CellType = checkBoxCellType1;
            this.neuSpread1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "选择";
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 48F;
            this.neuSpread1_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "联系人";
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 81F;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "与患者关系";
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 112F;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "联系电话";
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 105F;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "电话状态";
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 108F;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "联系地址";
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 106F;
            this.neuSpread1_Sheet1.Columns.Get(6).Label = "电子邮件";
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 114F;
            this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuGroupBox8
            // 
            this.neuGroupBox8.Controls.Add(this.dtpVisitTime);
            this.neuGroupBox8.Controls.Add(this.neuLabel8);
            this.neuGroupBox8.Controls.Add(this.rdbStop);
            this.neuGroupBox8.Controls.Add(this.rdbNormal);
            this.neuGroupBox8.Controls.Add(this.lblState);
            this.neuGroupBox8.Controls.Add(this.cmbResult);
            this.neuGroupBox8.Controls.Add(this.txtContent);
            this.neuGroupBox8.Controls.Add(this.lblContent);
            this.neuGroupBox8.Controls.Add(this.cmbLinkType);
            this.neuGroupBox8.Controls.Add(this.lblLinkwayType);
            this.neuGroupBox8.Controls.Add(this.txtWritebackPerson);
            this.neuGroupBox8.Controls.Add(this.lblWritebackPerson);
            this.neuGroupBox8.Controls.Add(this.neuLabelTransferPosition);
            this.neuGroupBox8.Controls.Add(this.neuComboBoxTransferPosition);
            this.neuGroupBox8.Controls.Add(this.cmbSymptom);
            this.neuGroupBox8.Controls.Add(this.lblSymptom);
            this.neuGroupBox8.Controls.Add(this.neuLabelSequela);
            this.neuGroupBox8.Controls.Add(this.neuComboBoxSequela);
            this.neuGroupBox8.Controls.Add(this.neuLabelrecrudescdeTime);
            this.neuGroupBox8.Controls.Add(this.neuDateTimePickerRecrudesceTime);
            this.neuGroupBox8.Controls.Add(this.neuCheckBoxIsTtransfer);
            this.neuGroupBox8.Controls.Add(this.lbllResult);
            this.neuGroupBox8.Controls.Add(this.neuCheckBoxIsSequela);
            this.neuGroupBox8.Controls.Add(this.neuCheckBoxIsRecrudesce);
            this.neuGroupBox8.Controls.Add(this.neuComboBoxDeadReason);
            this.neuGroupBox8.Controls.Add(this.neuLabelDeadReason);
            this.neuGroupBox8.Controls.Add(this.neuDateTimePickerDeadTime);
            this.neuGroupBox8.Controls.Add(this.neuLabelDeadTime);
            this.neuGroupBox8.Controls.Add(this.neuCheckBoxIsDead);
            this.neuGroupBox8.Controls.Add(this.cmbCircs);
            this.neuGroupBox8.Controls.Add(this.lblCircs);
            this.neuGroupBox8.Location = new System.Drawing.Point(0, 2);
            this.neuGroupBox8.Name = "neuGroupBox8";
            this.neuGroupBox8.Size = new System.Drawing.Size(771, 303);
            this.neuGroupBox8.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox8.TabIndex = 502;
            this.neuGroupBox8.TabStop = false;
            this.neuGroupBox8.Text = "随访信息录入";
            // 
            // dtpVisitTime
            // 
            this.dtpVisitTime.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtpVisitTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpVisitTime.IsEnter2Tab = false;
            this.dtpVisitTime.Location = new System.Drawing.Point(349, 25);
            this.dtpVisitTime.Name = "dtpVisitTime";
            this.dtpVisitTime.Size = new System.Drawing.Size(200, 21);
            this.dtpVisitTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpVisitTime.TabIndex = 84;
            // 
            // neuLabel8
            // 
            this.neuLabel8.AutoSize = true;
            this.neuLabel8.Location = new System.Drawing.Point(278, 29);
            this.neuLabel8.Name = "neuLabel8";
            this.neuLabel8.Size = new System.Drawing.Size(65, 12);
            this.neuLabel8.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel8.TabIndex = 83;
            this.neuLabel8.Text = "随访时间：";
            // 
            // rdbStop
            // 
            this.rdbStop.AutoSize = true;
            this.rdbStop.Location = new System.Drawing.Point(194, 56);
            this.rdbStop.Name = "rdbStop";
            this.rdbStop.Size = new System.Drawing.Size(71, 16);
            this.rdbStop.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rdbStop.TabIndex = 81;
            this.rdbStop.Text = "停止随访";
            this.rdbStop.UseVisualStyleBackColor = true;
            // 
            // rdbNormal
            // 
            this.rdbNormal.AutoSize = true;
            this.rdbNormal.Checked = true;
            this.rdbNormal.Location = new System.Drawing.Point(117, 56);
            this.rdbNormal.Name = "rdbNormal";
            this.rdbNormal.Size = new System.Drawing.Size(71, 16);
            this.rdbNormal.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rdbNormal.TabIndex = 82;
            this.rdbNormal.TabStop = true;
            this.rdbNormal.Text = "正常随访";
            this.rdbNormal.UseVisualStyleBackColor = true;
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Font = new System.Drawing.Font("宋体", 9F);
            this.lblState.Location = new System.Drawing.Point(23, 58);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(65, 12);
            this.lblState.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblState.TabIndex = 80;
            this.lblState.Text = "随访状态：";
            // 
            // cmbResult
            // 
            this.cmbResult.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbResult.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbResult.FormattingEnabled = true;
            this.cmbResult.IsEnter2Tab = false;
            this.cmbResult.IsFlat = false;
            this.cmbResult.IsLike = true;
            this.cmbResult.IsListOnly = false;
            this.cmbResult.IsPopForm = true;
            this.cmbResult.IsShowCustomerList = false;
            this.cmbResult.IsShowID = false;
            this.cmbResult.Location = new System.Drawing.Point(117, 109);
            this.cmbResult.Name = "cmbResult";
            this.cmbResult.PopForm = null;
            this.cmbResult.ShowCustomerList = false;
            this.cmbResult.ShowID = false;
            this.cmbResult.Size = new System.Drawing.Size(121, 20);
            this.cmbResult.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbResult.TabIndex = 67;
            this.cmbResult.Tag = "";
            this.cmbResult.ToolBarUse = false;
            // 
            // txtContent
            // 
            this.txtContent.IsEnter2Tab = false;
            this.txtContent.Location = new System.Drawing.Point(117, 82);
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(428, 21);
            this.txtContent.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtContent.TabIndex = 65;
            // 
            // lblContent
            // 
            this.lblContent.AutoSize = true;
            this.lblContent.Location = new System.Drawing.Point(23, 85);
            this.lblContent.Name = "lblContent";
            this.lblContent.Size = new System.Drawing.Size(65, 12);
            this.lblContent.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblContent.TabIndex = 61;
            this.lblContent.Text = "随访内容：";
            // 
            // cmbLinkType
            // 
            this.cmbLinkType.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbLinkType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLinkType.FormattingEnabled = true;
            this.cmbLinkType.IsEnter2Tab = false;
            this.cmbLinkType.IsFlat = false;
            this.cmbLinkType.IsLike = true;
            this.cmbLinkType.IsListOnly = false;
            this.cmbLinkType.IsPopForm = true;
            this.cmbLinkType.IsShowCustomerList = false;
            this.cmbLinkType.IsShowID = false;
            this.cmbLinkType.Location = new System.Drawing.Point(117, 26);
            this.cmbLinkType.Name = "cmbLinkType";
            this.cmbLinkType.PopForm = null;
            this.cmbLinkType.ShowCustomerList = false;
            this.cmbLinkType.ShowID = false;
            this.cmbLinkType.Size = new System.Drawing.Size(121, 20);
            this.cmbLinkType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbLinkType.TabIndex = 62;
            this.cmbLinkType.Tag = "";
            this.cmbLinkType.ToolBarUse = false;
            // 
            // lblLinkwayType
            // 
            this.lblLinkwayType.AutoSize = true;
            this.lblLinkwayType.Location = new System.Drawing.Point(23, 29);
            this.lblLinkwayType.Name = "lblLinkwayType";
            this.lblLinkwayType.Size = new System.Drawing.Size(65, 12);
            this.lblLinkwayType.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblLinkwayType.TabIndex = 59;
            this.lblLinkwayType.Text = "随访方式：";
            // 
            // txtWritebackPerson
            // 
            this.txtWritebackPerson.IsEnter2Tab = false;
            this.txtWritebackPerson.Location = new System.Drawing.Point(344, 109);
            this.txtWritebackPerson.Name = "txtWritebackPerson";
            this.txtWritebackPerson.Size = new System.Drawing.Size(201, 21);
            this.txtWritebackPerson.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtWritebackPerson.TabIndex = 68;
            // 
            // lblWritebackPerson
            // 
            this.lblWritebackPerson.AutoSize = true;
            this.lblWritebackPerson.Location = new System.Drawing.Point(290, 113);
            this.lblWritebackPerson.Name = "lblWritebackPerson";
            this.lblWritebackPerson.Size = new System.Drawing.Size(53, 12);
            this.lblWritebackPerson.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblWritebackPerson.TabIndex = 56;
            this.lblWritebackPerson.Text = "回信人：";
            // 
            // neuLabelTransferPosition
            // 
            this.neuLabelTransferPosition.AutoSize = true;
            this.neuLabelTransferPosition.Location = new System.Drawing.Point(133, 258);
            this.neuLabelTransferPosition.Name = "neuLabelTransferPosition";
            this.neuLabelTransferPosition.Size = new System.Drawing.Size(65, 12);
            this.neuLabelTransferPosition.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabelTransferPosition.TabIndex = 55;
            this.neuLabelTransferPosition.Text = "转移部位：";
            // 
            // neuComboBoxTransferPosition
            // 
            this.neuComboBoxTransferPosition.ArrowBackColor = System.Drawing.Color.Silver;
            this.neuComboBoxTransferPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.neuComboBoxTransferPosition.Enabled = false;
            this.neuComboBoxTransferPosition.FormattingEnabled = true;
            this.neuComboBoxTransferPosition.IsEnter2Tab = false;
            this.neuComboBoxTransferPosition.IsFlat = false;
            this.neuComboBoxTransferPosition.IsLike = true;
            this.neuComboBoxTransferPosition.IsListOnly = false;
            this.neuComboBoxTransferPosition.IsPopForm = true;
            this.neuComboBoxTransferPosition.IsShowCustomerList = false;
            this.neuComboBoxTransferPosition.IsShowID = false;
            this.neuComboBoxTransferPosition.Location = new System.Drawing.Point(199, 254);
            this.neuComboBoxTransferPosition.Name = "neuComboBoxTransferPosition";
            this.neuComboBoxTransferPosition.PopForm = null;
            this.neuComboBoxTransferPosition.ShowCustomerList = false;
            this.neuComboBoxTransferPosition.ShowID = false;
            this.neuComboBoxTransferPosition.Size = new System.Drawing.Size(121, 20);
            this.neuComboBoxTransferPosition.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.neuComboBoxTransferPosition.TabIndex = 79;
            this.neuComboBoxTransferPosition.Tag = "";
            this.neuComboBoxTransferPosition.ToolBarUse = false;
            // 
            // cmbSymptom
            // 
            this.cmbSymptom.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbSymptom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSymptom.FormattingEnabled = true;
            this.cmbSymptom.IsEnter2Tab = false;
            this.cmbSymptom.IsFlat = false;
            this.cmbSymptom.IsLike = true;
            this.cmbSymptom.IsListOnly = false;
            this.cmbSymptom.IsPopForm = true;
            this.cmbSymptom.IsShowCustomerList = false;
            this.cmbSymptom.IsShowID = false;
            this.cmbSymptom.Location = new System.Drawing.Point(344, 137);
            this.cmbSymptom.Name = "cmbSymptom";
            this.cmbSymptom.PopForm = null;
            this.cmbSymptom.ShowCustomerList = false;
            this.cmbSymptom.ShowID = false;
            this.cmbSymptom.Size = new System.Drawing.Size(201, 20);
            this.cmbSymptom.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbSymptom.TabIndex = 70;
            this.cmbSymptom.Tag = "";
            this.cmbSymptom.ToolBarUse = false;
            // 
            // lblSymptom
            // 
            this.lblSymptom.AutoSize = true;
            this.lblSymptom.Location = new System.Drawing.Point(278, 141);
            this.lblSymptom.Name = "lblSymptom";
            this.lblSymptom.Size = new System.Drawing.Size(65, 12);
            this.lblSymptom.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblSymptom.TabIndex = 54;
            this.lblSymptom.Text = "症状表现：";
            // 
            // neuLabelSequela
            // 
            this.neuLabelSequela.AutoSize = true;
            this.neuLabelSequela.Location = new System.Drawing.Point(145, 229);
            this.neuLabelSequela.Name = "neuLabelSequela";
            this.neuLabelSequela.Size = new System.Drawing.Size(53, 12);
            this.neuLabelSequela.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabelSequela.TabIndex = 53;
            this.neuLabelSequela.Text = "后遗症：";
            // 
            // neuComboBoxSequela
            // 
            this.neuComboBoxSequela.ArrowBackColor = System.Drawing.Color.Silver;
            this.neuComboBoxSequela.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.neuComboBoxSequela.Enabled = false;
            this.neuComboBoxSequela.FormattingEnabled = true;
            this.neuComboBoxSequela.IsEnter2Tab = false;
            this.neuComboBoxSequela.IsFlat = false;
            this.neuComboBoxSequela.IsLike = true;
            this.neuComboBoxSequela.IsListOnly = false;
            this.neuComboBoxSequela.IsPopForm = true;
            this.neuComboBoxSequela.IsShowCustomerList = false;
            this.neuComboBoxSequela.IsShowID = false;
            this.neuComboBoxSequela.Location = new System.Drawing.Point(199, 225);
            this.neuComboBoxSequela.Name = "neuComboBoxSequela";
            this.neuComboBoxSequela.PopForm = null;
            this.neuComboBoxSequela.ShowCustomerList = false;
            this.neuComboBoxSequela.ShowID = false;
            this.neuComboBoxSequela.Size = new System.Drawing.Size(121, 20);
            this.neuComboBoxSequela.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.neuComboBoxSequela.TabIndex = 77;
            this.neuComboBoxSequela.Tag = "";
            this.neuComboBoxSequela.ToolBarUse = false;
            // 
            // neuLabelrecrudescdeTime
            // 
            this.neuLabelrecrudescdeTime.AutoSize = true;
            this.neuLabelrecrudescdeTime.Location = new System.Drawing.Point(133, 200);
            this.neuLabelrecrudescdeTime.Name = "neuLabelrecrudescdeTime";
            this.neuLabelrecrudescdeTime.Size = new System.Drawing.Size(65, 12);
            this.neuLabelrecrudescdeTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabelrecrudescdeTime.TabIndex = 52;
            this.neuLabelrecrudescdeTime.Text = "复发时间：";
            // 
            // neuDateTimePickerRecrudesceTime
            // 
            this.neuDateTimePickerRecrudesceTime.Enabled = false;
            this.neuDateTimePickerRecrudesceTime.IsEnter2Tab = false;
            this.neuDateTimePickerRecrudesceTime.Location = new System.Drawing.Point(199, 196);
            this.neuDateTimePickerRecrudesceTime.Name = "neuDateTimePickerRecrudesceTime";
            this.neuDateTimePickerRecrudesceTime.Size = new System.Drawing.Size(121, 21);
            this.neuDateTimePickerRecrudesceTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuDateTimePickerRecrudesceTime.TabIndex = 75;
            // 
            // neuCheckBoxIsTtransfer
            // 
            this.neuCheckBoxIsTtransfer.AutoSize = true;
            this.neuCheckBoxIsTtransfer.Location = new System.Drawing.Point(23, 256);
            this.neuCheckBoxIsTtransfer.Name = "neuCheckBoxIsTtransfer";
            this.neuCheckBoxIsTtransfer.Size = new System.Drawing.Size(72, 16);
            this.neuCheckBoxIsTtransfer.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuCheckBoxIsTtransfer.TabIndex = 78;
            this.neuCheckBoxIsTtransfer.Text = "是否转移";
            this.neuCheckBoxIsTtransfer.UseVisualStyleBackColor = true;
            this.neuCheckBoxIsTtransfer.CheckedChanged += new System.EventHandler(this.neuCheckBoxIsTtransfer_CheckedChanged);
            // 
            // lbllResult
            // 
            this.lbllResult.AutoSize = true;
            this.lbllResult.Location = new System.Drawing.Point(23, 113);
            this.lbllResult.Name = "lbllResult";
            this.lbllResult.Size = new System.Drawing.Size(65, 12);
            this.lbllResult.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbllResult.TabIndex = 51;
            this.lbllResult.Text = "随访结果：";
            // 
            // neuCheckBoxIsSequela
            // 
            this.neuCheckBoxIsSequela.AutoSize = true;
            this.neuCheckBoxIsSequela.Location = new System.Drawing.Point(23, 227);
            this.neuCheckBoxIsSequela.Name = "neuCheckBoxIsSequela";
            this.neuCheckBoxIsSequela.Size = new System.Drawing.Size(96, 16);
            this.neuCheckBoxIsSequela.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuCheckBoxIsSequela.TabIndex = 76;
            this.neuCheckBoxIsSequela.Text = "是否有后遗症";
            this.neuCheckBoxIsSequela.UseVisualStyleBackColor = true;
            this.neuCheckBoxIsSequela.CheckedChanged += new System.EventHandler(this.neuCheckBoxIsSequela_CheckedChanged);
            // 
            // neuCheckBoxIsRecrudesce
            // 
            this.neuCheckBoxIsRecrudesce.AutoSize = true;
            this.neuCheckBoxIsRecrudesce.Location = new System.Drawing.Point(23, 198);
            this.neuCheckBoxIsRecrudesce.Name = "neuCheckBoxIsRecrudesce";
            this.neuCheckBoxIsRecrudesce.Size = new System.Drawing.Size(72, 16);
            this.neuCheckBoxIsRecrudesce.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuCheckBoxIsRecrudesce.TabIndex = 74;
            this.neuCheckBoxIsRecrudesce.Text = "是否复发";
            this.neuCheckBoxIsRecrudesce.UseVisualStyleBackColor = true;
            this.neuCheckBoxIsRecrudesce.CheckedChanged += new System.EventHandler(this.neuCheckBoxIsRecrudesce_CheckedChanged);
            // 
            // neuComboBoxDeadReason
            // 
            this.neuComboBoxDeadReason.ArrowBackColor = System.Drawing.Color.Silver;
            this.neuComboBoxDeadReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.neuComboBoxDeadReason.Enabled = false;
            this.neuComboBoxDeadReason.FormattingEnabled = true;
            this.neuComboBoxDeadReason.IsEnter2Tab = false;
            this.neuComboBoxDeadReason.IsFlat = false;
            this.neuComboBoxDeadReason.IsLike = true;
            this.neuComboBoxDeadReason.IsListOnly = false;
            this.neuComboBoxDeadReason.IsPopForm = true;
            this.neuComboBoxDeadReason.IsShowCustomerList = false;
            this.neuComboBoxDeadReason.IsShowID = false;
            this.neuComboBoxDeadReason.Location = new System.Drawing.Point(424, 167);
            this.neuComboBoxDeadReason.Name = "neuComboBoxDeadReason";
            this.neuComboBoxDeadReason.PopForm = null;
            this.neuComboBoxDeadReason.ShowCustomerList = false;
            this.neuComboBoxDeadReason.ShowID = false;
            this.neuComboBoxDeadReason.Size = new System.Drawing.Size(121, 20);
            this.neuComboBoxDeadReason.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.neuComboBoxDeadReason.TabIndex = 73;
            this.neuComboBoxDeadReason.Tag = "";
            this.neuComboBoxDeadReason.ToolBarUse = false;
            // 
            // neuLabelDeadReason
            // 
            this.neuLabelDeadReason.AutoSize = true;
            this.neuLabelDeadReason.Location = new System.Drawing.Point(361, 171);
            this.neuLabelDeadReason.Name = "neuLabelDeadReason";
            this.neuLabelDeadReason.Size = new System.Drawing.Size(65, 12);
            this.neuLabelDeadReason.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabelDeadReason.TabIndex = 49;
            this.neuLabelDeadReason.Text = "死亡原因：";
            // 
            // neuDateTimePickerDeadTime
            // 
            this.neuDateTimePickerDeadTime.Enabled = false;
            this.neuDateTimePickerDeadTime.IsEnter2Tab = false;
            this.neuDateTimePickerDeadTime.Location = new System.Drawing.Point(199, 167);
            this.neuDateTimePickerDeadTime.Name = "neuDateTimePickerDeadTime";
            this.neuDateTimePickerDeadTime.Size = new System.Drawing.Size(121, 21);
            this.neuDateTimePickerDeadTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuDateTimePickerDeadTime.TabIndex = 72;
            // 
            // neuLabelDeadTime
            // 
            this.neuLabelDeadTime.AutoSize = true;
            this.neuLabelDeadTime.Location = new System.Drawing.Point(133, 171);
            this.neuLabelDeadTime.Name = "neuLabelDeadTime";
            this.neuLabelDeadTime.Size = new System.Drawing.Size(65, 12);
            this.neuLabelDeadTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabelDeadTime.TabIndex = 48;
            this.neuLabelDeadTime.Text = "死亡时间：";
            // 
            // neuCheckBoxIsDead
            // 
            this.neuCheckBoxIsDead.AutoSize = true;
            this.neuCheckBoxIsDead.Location = new System.Drawing.Point(23, 169);
            this.neuCheckBoxIsDead.Name = "neuCheckBoxIsDead";
            this.neuCheckBoxIsDead.Size = new System.Drawing.Size(72, 16);
            this.neuCheckBoxIsDead.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuCheckBoxIsDead.TabIndex = 71;
            this.neuCheckBoxIsDead.Text = "是否死亡";
            this.neuCheckBoxIsDead.UseVisualStyleBackColor = true;
            this.neuCheckBoxIsDead.CheckedChanged += new System.EventHandler(this.neuCheckBoxIsDead_CheckedChanged);
            // 
            // cmbCircs
            // 
            this.cmbCircs.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbCircs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCircs.FormattingEnabled = true;
            this.cmbCircs.IsEnter2Tab = false;
            this.cmbCircs.IsFlat = false;
            this.cmbCircs.IsLike = true;
            this.cmbCircs.IsListOnly = false;
            this.cmbCircs.IsPopForm = true;
            this.cmbCircs.IsShowCustomerList = false;
            this.cmbCircs.IsShowID = false;
            this.cmbCircs.Location = new System.Drawing.Point(117, 137);
            this.cmbCircs.Name = "cmbCircs";
            this.cmbCircs.PopForm = null;
            this.cmbCircs.ShowCustomerList = false;
            this.cmbCircs.ShowID = false;
            this.cmbCircs.Size = new System.Drawing.Size(121, 20);
            this.cmbCircs.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbCircs.TabIndex = 69;
            this.cmbCircs.Tag = "";
            this.cmbCircs.ToolBarUse = false;
            // 
            // lblCircs
            // 
            this.lblCircs.AutoSize = true;
            this.lblCircs.Location = new System.Drawing.Point(23, 140);
            this.lblCircs.Name = "lblCircs";
            this.lblCircs.Size = new System.Drawing.Size(65, 12);
            this.lblCircs.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblCircs.TabIndex = 47;
            this.lblCircs.Text = "一般情况：";
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.BackColor = System.Drawing.Color.Azure;
            this.tabPage1.Controls.Add(this.neuGroupBox11);
            this.tabPage1.Controls.Add(this.neuGroupBox1);
            this.tabPage1.Controls.Add(this.neuGroupBox2);
            this.tabPage1.Controls.Add(this.neuGroupBox3);
            this.tabPage1.Controls.Add(this.neuGroupBox4);
            this.tabPage1.Controls.Add(this.neuGroupBox5);
            this.tabPage1.Controls.Add(this.neuGroupBox6);
            this.tabPage1.Controls.Add(this.neuGroupBox7);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(771, 575);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "基本信息(F2)";
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // neuGroupBox11
            // 
            this.neuGroupBox11.Controls.Add(this.txtRemark);
            this.neuGroupBox11.Controls.Add(this.neuLabel14);
            this.neuGroupBox11.Controls.Add(this.txtCaseStus);
            this.neuGroupBox11.Controls.Add(this.neuLabel9);
            this.neuGroupBox11.Location = new System.Drawing.Point(8, 6);
            this.neuGroupBox11.Name = "neuGroupBox11";
            this.neuGroupBox11.Size = new System.Drawing.Size(771, 12);
            this.neuGroupBox11.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox11.TabIndex = 508;
            this.neuGroupBox11.TabStop = false;
            this.neuGroupBox11.Text = "病案状态";
            this.neuGroupBox11.Visible = false;
            // 
            // txtRemark
            // 
            this.txtRemark.BackColor = System.Drawing.Color.White;
            this.txtRemark.IsEnter2Tab = false;
            this.txtRemark.Location = new System.Drawing.Point(301, 12);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ReadOnly = true;
            this.txtRemark.Size = new System.Drawing.Size(457, 21);
            this.txtRemark.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtRemark.TabIndex = 28;
            this.txtRemark.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNomen_KeyDown);
            // 
            // neuLabel14
            // 
            this.neuLabel14.AutoSize = true;
            this.neuLabel14.Location = new System.Drawing.Point(241, 17);
            this.neuLabel14.Name = "neuLabel14";
            this.neuLabel14.Size = new System.Drawing.Size(53, 12);
            this.neuLabel14.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel14.TabIndex = 0;
            this.neuLabel14.Text = "说    明";
            this.neuLabel14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCaseStus
            // 
            this.txtCaseStus.BackColor = System.Drawing.Color.White;
            this.txtCaseStus.IsEnter2Tab = false;
            this.txtCaseStus.Location = new System.Drawing.Point(62, 12);
            this.txtCaseStus.Name = "txtCaseStus";
            this.txtCaseStus.ReadOnly = true;
            this.txtCaseStus.Size = new System.Drawing.Size(166, 21);
            this.txtCaseStus.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCaseStus.TabIndex = 28;
            this.txtCaseStus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNomen_KeyDown);
            // 
            // neuLabel9
            // 
            this.neuLabel9.AutoSize = true;
            this.neuLabel9.Location = new System.Drawing.Point(8, 17);
            this.neuLabel9.Name = "neuLabel9";
            this.neuLabel9.Size = new System.Drawing.Size(53, 12);
            this.neuLabel9.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel9.TabIndex = 0;
            this.neuLabel9.Text = "病案状态";
            this.neuLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.txtPatientAge);
            this.neuGroupBox1.Controls.Add(this.cmbUnit);
            this.neuGroupBox1.Controls.Add(this.txtLinkmanAdd);
            this.neuGroupBox1.Controls.Add(this.labl);
            this.neuGroupBox1.Controls.Add(this.txtAreaCode);
            this.neuGroupBox1.Controls.Add(this.txtDIST);
            this.neuGroupBox1.Controls.Add(this.txtSSN);
            this.neuGroupBox1.Controls.Add(this.txtLinkmanTel);
            this.neuGroupBox1.Controls.Add(this.label25);
            this.neuGroupBox1.Controls.Add(this.label24);
            this.neuGroupBox1.Controls.Add(this.txtKin);
            this.neuGroupBox1.Controls.Add(this.label23);
            this.neuGroupBox1.Controls.Add(this.txtBusinessZip);
            this.neuGroupBox1.Controls.Add(this.txtHomeZip);
            this.neuGroupBox1.Controls.Add(this.label22);
            this.neuGroupBox1.Controls.Add(this.txtAddressHome);
            this.neuGroupBox1.Controls.Add(this.label20);
            this.neuGroupBox1.Controls.Add(this.label19);
            this.neuGroupBox1.Controls.Add(this.txtPhoneBusiness);
            this.neuGroupBox1.Controls.Add(this.label18);
            this.neuGroupBox1.Controls.Add(this.txtAddressBusiness);
            this.neuGroupBox1.Controls.Add(this.label17);
            this.neuGroupBox1.Controls.Add(this.txtIDNo);
            this.neuGroupBox1.Controls.Add(this.label16);
            this.neuGroupBox1.Controls.Add(this.label15);
            this.neuGroupBox1.Controls.Add(this.txtPatientName);
            this.neuGroupBox1.Controls.Add(this.label13);
            this.neuGroupBox1.Controls.Add(this.label12);
            this.neuGroupBox1.Controls.Add(this.label11);
            this.neuGroupBox1.Controls.Add(this.label10);
            this.neuGroupBox1.Controls.Add(this.label9);
            this.neuGroupBox1.Controls.Add(this.dtPatientBirthday);
            this.neuGroupBox1.Controls.Add(this.label8);
            this.neuGroupBox1.Controls.Add(this.label7);
            this.neuGroupBox1.Controls.Add(this.label6);
            this.neuGroupBox1.Controls.Add(this.txtClinicNo);
            this.neuGroupBox1.Controls.Add(this.label5);
            this.neuGroupBox1.Controls.Add(this.label4);
            this.neuGroupBox1.Controls.Add(this.label3);
            this.neuGroupBox1.Controls.Add(this.txtInTimes);
            this.neuGroupBox1.Controls.Add(this.label2);
            this.neuGroupBox1.Controls.Add(this.txtCaseNum);
            this.neuGroupBox1.Controls.Add(this.label1);
            this.neuGroupBox1.Controls.Add(this.txtPhoneHome);
            this.neuGroupBox1.Controls.Add(this.label21);
            this.neuGroupBox1.Controls.Add(this.label14);
            this.neuGroupBox1.Controls.Add(this.txtCountry);
            this.neuGroupBox1.Controls.Add(this.txtProfession);
            this.neuGroupBox1.Controls.Add(this.txtNationality);
            this.neuGroupBox1.Controls.Add(this.txtMaritalStatus);
            this.neuGroupBox1.Controls.Add(this.txtPatientSex);
            this.neuGroupBox1.Controls.Add(this.txtPactKind);
            this.neuGroupBox1.Controls.Add(this.txtNomen);
            this.neuGroupBox1.Controls.Add(this.label97);
            this.neuGroupBox1.Controls.Add(this.txtRelation);
            this.neuGroupBox1.Location = new System.Drawing.Point(8, 20);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(771, 206);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 501;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "（1）患者基本信息";
            // 
            // txtPatientAge
            // 
            this.txtPatientAge.BackColor = System.Drawing.Color.White;
            this.txtPatientAge.Enabled = false;
            this.txtPatientAge.IsEnter2Tab = false;
            this.txtPatientAge.Location = new System.Drawing.Point(494, 44);
            this.txtPatientAge.MaxLength = 10;
            this.txtPatientAge.Name = "txtPatientAge";
            this.txtPatientAge.ReadOnly = true;
            this.txtPatientAge.Size = new System.Drawing.Size(98, 21);
            this.txtPatientAge.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtPatientAge.TabIndex = 63;
            this.txtPatientAge.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PatientAge_KeyDown);
            // 
            // cmbUnit
            // 
            this.cmbUnit.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.cmbUnit.Location = new System.Drawing.Point(549, 43);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.PopForm = null;
            this.cmbUnit.ShowCustomerList = false;
            this.cmbUnit.ShowID = false;
            this.cmbUnit.Size = new System.Drawing.Size(46, 21);
            this.cmbUnit.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbUnit.TabIndex = 64;
            this.cmbUnit.Tag = "";
            this.cmbUnit.ToolBarUse = false;
            this.cmbUnit.Visible = false;
            this.cmbUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbUnit_KeyDown);
            // 
            // txtLinkmanAdd
            // 
            this.txtLinkmanAdd.IsEnter2Tab = false;
            this.txtLinkmanAdd.Location = new System.Drawing.Point(222, 164);
            this.txtLinkmanAdd.MaxLength = 50;
            this.txtLinkmanAdd.Name = "txtLinkmanAdd";
            this.txtLinkmanAdd.Size = new System.Drawing.Size(200, 21);
            this.txtLinkmanAdd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtLinkmanAdd.TabIndex = 80;
            this.txtLinkmanAdd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LinkmanAdd_KeyDown);
            // 
            // labl
            // 
            this.labl.Location = new System.Drawing.Point(158, 168);
            this.labl.Name = "labl";
            this.labl.Size = new System.Drawing.Size(72, 12);
            this.labl.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.labl.TabIndex = 48;
            this.labl.Text = "联系地址";
            this.labl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtAreaCode
            // 
            this.txtAreaCode.IsEnter2Tab = false;
            this.txtAreaCode.Location = new System.Drawing.Point(222, 68);
            this.txtAreaCode.MaxLength = 50;
            this.txtAreaCode.Name = "txtAreaCode";
            this.txtAreaCode.Size = new System.Drawing.Size(200, 21);
            this.txtAreaCode.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtAreaCode.TabIndex = 67;
            this.txtAreaCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AreaCode_KeyDown);
            // 
            // txtDIST
            // 
            this.txtDIST.IsEnter2Tab = false;
            this.txtDIST.Location = new System.Drawing.Point(62, 92);
            this.txtDIST.MaxLength = 50;
            this.txtDIST.Name = "txtDIST";
            this.txtDIST.Size = new System.Drawing.Size(88, 21);
            this.txtDIST.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtDIST.TabIndex = 70;
            this.txtDIST.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DIST_KeyDown);
            // 
            // txtSSN
            // 
            this.txtSSN.IsEnter2Tab = false;
            this.txtSSN.Location = new System.Drawing.Point(494, 20);
            this.txtSSN.MaxLength = 18;
            this.txtSSN.Name = "txtSSN";
            this.txtSSN.Size = new System.Drawing.Size(100, 21);
            this.txtSSN.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtSSN.TabIndex = 58;
            this.txtSSN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SSN_KeyDown);
            // 
            // txtLinkmanTel
            // 
            this.txtLinkmanTel.IsEnter2Tab = false;
            this.txtLinkmanTel.Location = new System.Drawing.Point(62, 164);
            this.txtLinkmanTel.MaxLength = 25;
            this.txtLinkmanTel.Name = "txtLinkmanTel";
            this.txtLinkmanTel.Size = new System.Drawing.Size(88, 21);
            this.txtLinkmanTel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtLinkmanTel.TabIndex = 79;
            this.txtLinkmanTel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LinkmanTel_KeyDown);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(6, 168);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(53, 12);
            this.label25.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label25.TabIndex = 49;
            this.label25.Text = "联系电话";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(598, 143);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(53, 12);
            this.label24.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label24.TabIndex = 54;
            this.label24.Text = "关    系";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtKin
            // 
            this.txtKin.IsEnter2Tab = false;
            this.txtKin.Location = new System.Drawing.Point(494, 140);
            this.txtKin.MaxLength = 10;
            this.txtKin.Name = "txtKin";
            this.txtKin.Size = new System.Drawing.Size(104, 21);
            this.txtKin.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtKin.TabIndex = 78;
            this.txtKin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Kin_KeyDown);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(430, 143);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(53, 12);
            this.label23.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label23.TabIndex = 53;
            this.label23.Text = "联 系 人";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBusinessZip
            // 
            this.txtBusinessZip.IsEnter2Tab = false;
            this.txtBusinessZip.Location = new System.Drawing.Point(62, 116);
            this.txtBusinessZip.MaxLength = 6;
            this.txtBusinessZip.Name = "txtBusinessZip";
            this.txtBusinessZip.Size = new System.Drawing.Size(88, 21);
            this.txtBusinessZip.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtBusinessZip.TabIndex = 73;
            this.txtBusinessZip.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BusinessZip_KeyDown);
            // 
            // txtHomeZip
            // 
            this.txtHomeZip.IsEnter2Tab = false;
            this.txtHomeZip.Location = new System.Drawing.Point(62, 140);
            this.txtHomeZip.MaxLength = 6;
            this.txtHomeZip.Name = "txtHomeZip";
            this.txtHomeZip.Size = new System.Drawing.Size(88, 21);
            this.txtHomeZip.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtHomeZip.TabIndex = 76;
            this.txtHomeZip.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HomeZip_KeyDown);
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(6, 140);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(64, 23);
            this.label22.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label22.TabIndex = 52;
            this.label22.Text = "家庭邮编";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtAddressHome
            // 
            this.txtAddressHome.IsEnter2Tab = false;
            this.txtAddressHome.Location = new System.Drawing.Point(494, 116);
            this.txtAddressHome.MaxLength = 50;
            this.txtAddressHome.Name = "txtAddressHome";
            this.txtAddressHome.Size = new System.Drawing.Size(264, 21);
            this.txtAddressHome.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtAddressHome.TabIndex = 75;
            this.txtAddressHome.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AddressHome_KeyDown);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(430, 120);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(53, 12);
            this.label20.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label20.TabIndex = 34;
            this.label20.Text = "户口地址";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 120);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(53, 12);
            this.label19.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label19.TabIndex = 35;
            this.label19.Text = "单位邮编";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPhoneBusiness
            // 
            this.txtPhoneBusiness.IsEnter2Tab = false;
            this.txtPhoneBusiness.Location = new System.Drawing.Point(222, 116);
            this.txtPhoneBusiness.MaxLength = 25;
            this.txtPhoneBusiness.Name = "txtPhoneBusiness";
            this.txtPhoneBusiness.Size = new System.Drawing.Size(200, 21);
            this.txtPhoneBusiness.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtPhoneBusiness.TabIndex = 74;
            this.txtPhoneBusiness.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PhoneBusiness_KeyDown);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(158, 121);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(53, 12);
            this.label18.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label18.TabIndex = 29;
            this.label18.Text = "单位电话";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtAddressBusiness
            // 
            this.txtAddressBusiness.IsEnter2Tab = false;
            this.txtAddressBusiness.Location = new System.Drawing.Point(494, 92);
            this.txtAddressBusiness.MaxLength = 50;
            this.txtAddressBusiness.Name = "txtAddressBusiness";
            this.txtAddressBusiness.Size = new System.Drawing.Size(264, 21);
            this.txtAddressBusiness.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtAddressBusiness.TabIndex = 72;
            this.txtAddressBusiness.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AddressBusiness_KeyDown);
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(430, 92);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(64, 23);
            this.label17.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label17.TabIndex = 31;
            this.label17.Text = "工作单位";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIDNo
            // 
            this.txtIDNo.IsEnter2Tab = false;
            this.txtIDNo.Location = new System.Drawing.Point(222, 92);
            this.txtIDNo.MaxLength = 18;
            this.txtIDNo.Name = "txtIDNo";
            this.txtIDNo.Size = new System.Drawing.Size(200, 21);
            this.txtIDNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtIDNo.TabIndex = 71;
            this.txtIDNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.IDNo_KeyDown);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(158, 96);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 12);
            this.label16.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label16.TabIndex = 41;
            this.label16.Text = "身 份 证";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 96);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 12);
            this.label15.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label15.TabIndex = 42;
            this.label15.Text = "籍    贯";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPatientName
            // 
            this.txtPatientName.IsEnter2Tab = false;
            this.txtPatientName.Location = new System.Drawing.Point(62, 44);
            this.txtPatientName.MaxLength = 20;
            this.txtPatientName.Name = "txtPatientName";
            this.txtPatientName.Size = new System.Drawing.Size(88, 21);
            this.txtPatientName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtPatientName.TabIndex = 60;
            this.txtPatientName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PatientName_KeyDown);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(430, 73);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label13.TabIndex = 46;
            this.label13.Text = "民    族";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(158, 68);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(64, 23);
            this.label12.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label12.TabIndex = 37;
            this.label12.Text = "出 生 地";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 72);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label11.TabIndex = 38;
            this.label11.Text = "职    业";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(598, 49);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label10.TabIndex = 36;
            this.label10.Text = "婚    姻";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(430, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label9.TabIndex = 39;
            this.label9.Text = "年    龄";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtPatientBirthday
            // 
            this.dtPatientBirthday.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtPatientBirthday.IsEnter2Tab = false;
            this.dtPatientBirthday.Location = new System.Drawing.Point(323, 44);
            this.dtPatientBirthday.Name = "dtPatientBirthday";
            this.dtPatientBirthday.Size = new System.Drawing.Size(97, 21);
            this.dtPatientBirthday.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtPatientBirthday.TabIndex = 62;
            this.dtPatientBirthday.Value = new System.DateTime(2005, 7, 19, 22, 6, 13, 468);
            this.dtPatientBirthday.ValueChanged += new System.EventHandler(this.dtPatientBirthday_ValueChanged);
            this.dtPatientBirthday.KeyDown += new System.Windows.Forms.KeyEventHandler(this.patientBirthday_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(266, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label8.TabIndex = 40;
            this.label8.Text = "生　　日";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(158, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label7.TabIndex = 30;
            this.label7.Text = "性    别";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label6.TabIndex = 32;
            this.label6.Text = "姓    名";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtClinicNo
            // 
            this.txtClinicNo.IsEnter2Tab = false;
            this.txtClinicNo.Location = new System.Drawing.Point(654, 20);
            this.txtClinicNo.MaxLength = 10;
            this.txtClinicNo.Name = "txtClinicNo";
            this.txtClinicNo.Size = new System.Drawing.Size(104, 21);
            this.txtClinicNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtClinicNo.TabIndex = 59;
            this.txtClinicNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.clinicNo_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(598, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label5.TabIndex = 33;
            this.label5.Text = "卡 　 号";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(430, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label4.TabIndex = 51;
            this.label4.Text = "医 保 号";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(266, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label3.TabIndex = 50;
            this.label3.Text = "结算类别";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtInTimes
            // 
            this.txtInTimes.AllowNegative = false;
            this.txtInTimes.AutoPadRightZero = false;
            this.txtInTimes.Location = new System.Drawing.Point(222, 20);
            this.txtInTimes.MaxDigits = 0;
            this.txtInTimes.MaxLength = 3;
            this.txtInTimes.Name = "txtInTimes";
            this.txtInTimes.Size = new System.Drawing.Size(40, 21);
            this.txtInTimes.TabIndex = 56;
            this.txtInTimes.WillShowError = false;
            this.txtInTimes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInTimes_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(158, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label2.TabIndex = 45;
            this.label2.Text = "次    数";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCaseNum
            // 
            this.txtCaseNum.IsEnter2Tab = false;
            this.txtCaseNum.Location = new System.Drawing.Point(62, 20);
            this.txtCaseNum.MaxLength = 10;
            this.txtCaseNum.Name = "txtCaseNum";
            this.txtCaseNum.Size = new System.Drawing.Size(88, 21);
            this.txtCaseNum.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCaseNum.TabIndex = 55;
            this.toolTip1.SetToolTip(this.txtCaseNum, "按F1键可以在病案号和住院号间切换");
            this.txtCaseNum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.caseNum_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label1.TabIndex = 44;
            this.label1.Text = "病 案 号";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPhoneHome
            // 
            this.txtPhoneHome.IsEnter2Tab = false;
            this.txtPhoneHome.Location = new System.Drawing.Point(222, 140);
            this.txtPhoneHome.MaxLength = 25;
            this.txtPhoneHome.Name = "txtPhoneHome";
            this.txtPhoneHome.Size = new System.Drawing.Size(200, 21);
            this.txtPhoneHome.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtPhoneHome.TabIndex = 77;
            this.txtPhoneHome.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PhoneHome_KeyDown);
            // 
            // label21
            // 
            this.label21.Location = new System.Drawing.Point(158, 140);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(64, 23);
            this.label21.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label21.TabIndex = 43;
            this.label21.Text = "家庭电话";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(598, 74);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label14.TabIndex = 47;
            this.label14.Text = "国    籍";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCountry
            // 
            this.txtCountry.EnterVisiable = true;
            this.txtCountry.IsFind = true;
            this.txtCountry.IsSelctNone = true;
            this.txtCountry.IsSendToNext = false;
            this.txtCountry.IsShowID = false;
            this.txtCountry.ItemText = "";
            this.txtCountry.ListBoxHeight = 100;
            this.txtCountry.ListBoxVisible = false;
            this.txtCountry.ListBoxWidth = 100;
            this.txtCountry.Location = new System.Drawing.Point(654, 68);
            this.txtCountry.MaxLength = 10;
            this.txtCountry.Name = "txtCountry";
            this.txtCountry.OmitFilter = true;
            this.txtCountry.SelectedItem = null;
            this.txtCountry.SelectNone = true;
            this.txtCountry.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCountry.ShowID = true;
            this.txtCountry.Size = new System.Drawing.Size(104, 21);
            this.txtCountry.TabIndex = 69;
            this.txtCountry.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Country_KeyDown);
            // 
            // txtProfession
            // 
            this.txtProfession.EnterVisiable = true;
            this.txtProfession.IsFind = true;
            this.txtProfession.IsSelctNone = true;
            this.txtProfession.IsSendToNext = false;
            this.txtProfession.IsShowID = false;
            this.txtProfession.ItemText = "";
            this.txtProfession.ListBoxHeight = 100;
            this.txtProfession.ListBoxVisible = false;
            this.txtProfession.ListBoxWidth = 100;
            this.txtProfession.Location = new System.Drawing.Point(62, 68);
            this.txtProfession.MaxLength = 10;
            this.txtProfession.Name = "txtProfession";
            this.txtProfession.OmitFilter = true;
            this.txtProfession.SelectedItem = null;
            this.txtProfession.SelectNone = true;
            this.txtProfession.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtProfession.ShowID = true;
            this.txtProfession.Size = new System.Drawing.Size(88, 21);
            this.txtProfession.TabIndex = 66;
            this.txtProfession.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Profession_KeyDown);
            // 
            // txtNationality
            // 
            this.txtNationality.EnterVisiable = true;
            this.txtNationality.IsFind = true;
            this.txtNationality.IsSelctNone = true;
            this.txtNationality.IsSendToNext = false;
            this.txtNationality.IsShowID = false;
            this.txtNationality.ItemText = "";
            this.txtNationality.ListBoxHeight = 100;
            this.txtNationality.ListBoxVisible = false;
            this.txtNationality.ListBoxWidth = 100;
            this.txtNationality.Location = new System.Drawing.Point(494, 68);
            this.txtNationality.MaxLength = 10;
            this.txtNationality.Name = "txtNationality";
            this.txtNationality.OmitFilter = true;
            this.txtNationality.SelectedItem = null;
            this.txtNationality.SelectNone = true;
            this.txtNationality.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtNationality.ShowID = true;
            this.txtNationality.Size = new System.Drawing.Size(100, 21);
            this.txtNationality.TabIndex = 68;
            this.txtNationality.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Nationality_KeyDown);
            // 
            // txtMaritalStatus
            // 
            this.txtMaritalStatus.EnterVisiable = true;
            this.txtMaritalStatus.IsFind = true;
            this.txtMaritalStatus.IsSelctNone = true;
            this.txtMaritalStatus.IsSendToNext = false;
            this.txtMaritalStatus.IsShowID = false;
            this.txtMaritalStatus.ItemText = "";
            this.txtMaritalStatus.ListBoxHeight = 100;
            this.txtMaritalStatus.ListBoxVisible = false;
            this.txtMaritalStatus.ListBoxWidth = 100;
            this.txtMaritalStatus.Location = new System.Drawing.Point(654, 44);
            this.txtMaritalStatus.MaxLength = 2;
            this.txtMaritalStatus.Name = "txtMaritalStatus";
            this.txtMaritalStatus.OmitFilter = true;
            this.txtMaritalStatus.SelectedItem = null;
            this.txtMaritalStatus.SelectNone = true;
            this.txtMaritalStatus.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMaritalStatus.ShowID = true;
            this.txtMaritalStatus.Size = new System.Drawing.Size(104, 21);
            this.txtMaritalStatus.TabIndex = 65;
            this.txtMaritalStatus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MaritalStatus_KeyDown);
            // 
            // txtPatientSex
            // 
            this.txtPatientSex.EnterVisiable = true;
            this.txtPatientSex.IsFind = true;
            this.txtPatientSex.IsSelctNone = true;
            this.txtPatientSex.IsSendToNext = false;
            this.txtPatientSex.IsShowID = false;
            this.txtPatientSex.ItemText = "";
            this.txtPatientSex.ListBoxHeight = 100;
            this.txtPatientSex.ListBoxVisible = false;
            this.txtPatientSex.ListBoxWidth = 100;
            this.txtPatientSex.Location = new System.Drawing.Point(222, 44);
            this.txtPatientSex.MaxLength = 2;
            this.txtPatientSex.Name = "txtPatientSex";
            this.txtPatientSex.OmitFilter = true;
            this.txtPatientSex.SelectedItem = null;
            this.txtPatientSex.SelectNone = true;
            this.txtPatientSex.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPatientSex.ShowID = true;
            this.txtPatientSex.Size = new System.Drawing.Size(40, 21);
            this.txtPatientSex.TabIndex = 61;
            this.txtPatientSex.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PatientSex_KeyDown);
            // 
            // txtPactKind
            // 
            this.txtPactKind.EnterVisiable = true;
            this.txtPactKind.IsFind = true;
            this.txtPactKind.IsSelctNone = true;
            this.txtPactKind.IsSendToNext = false;
            this.txtPactKind.IsShowID = false;
            this.txtPactKind.ItemText = "";
            this.txtPactKind.ListBoxHeight = 100;
            this.txtPactKind.ListBoxVisible = false;
            this.txtPactKind.ListBoxWidth = 100;
            this.txtPactKind.Location = new System.Drawing.Point(323, 20);
            this.txtPactKind.MaxLength = 8;
            this.txtPactKind.Name = "txtPactKind";
            this.txtPactKind.OmitFilter = true;
            this.txtPactKind.SelectedItem = null;
            this.txtPactKind.SelectNone = true;
            this.txtPactKind.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPactKind.ShowID = true;
            this.txtPactKind.Size = new System.Drawing.Size(97, 21);
            this.txtPactKind.TabIndex = 57;
            this.txtPactKind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pactKind_KeyDown);
            // 
            // txtNomen
            // 
            this.txtNomen.IsEnter2Tab = false;
            this.txtNomen.Location = new System.Drawing.Point(494, 165);
            this.txtNomen.Name = "txtNomen";
            this.txtNomen.Size = new System.Drawing.Size(104, 21);
            this.txtNomen.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtNomen.TabIndex = 28;
            this.txtNomen.Visible = false;
            this.txtNomen.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNomen_KeyDown);
            // 
            // label97
            // 
            this.label97.AutoSize = true;
            this.label97.Location = new System.Drawing.Point(433, 170);
            this.label97.Name = "label97";
            this.label97.Size = new System.Drawing.Size(53, 12);
            this.label97.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label97.TabIndex = 0;
            this.label97.Text = "曾 用 名";
            this.label97.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label97.Visible = false;
            // 
            // txtRelation
            // 
            this.txtRelation.BackColor = System.Drawing.SystemColors.Window;
            this.txtRelation.EnterVisiable = true;
            this.txtRelation.IsFind = true;
            this.txtRelation.IsSelctNone = true;
            this.txtRelation.IsSendToNext = false;
            this.txtRelation.IsShowID = false;
            this.txtRelation.ItemText = "";
            this.txtRelation.ListBoxHeight = 100;
            this.txtRelation.ListBoxVisible = false;
            this.txtRelation.ListBoxWidth = 100;
            this.txtRelation.Location = new System.Drawing.Point(653, 140);
            this.txtRelation.MaxLength = 5;
            this.txtRelation.Multiline = true;
            this.txtRelation.Name = "txtRelation";
            this.txtRelation.OmitFilter = true;
            this.txtRelation.SelectedItem = null;
            this.txtRelation.SelectNone = true;
            this.txtRelation.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRelation.ShowID = true;
            this.txtRelation.Size = new System.Drawing.Size(105, 21);
            this.txtRelation.TabIndex = 25;
            this.txtRelation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Relation_KeyDown);
            // 
            // neuGroupBox2
            // 
            this.neuGroupBox2.Controls.Add(this.txtOutRoom);
            this.neuGroupBox2.Controls.Add(this.txtInRoom);
            this.neuGroupBox2.Controls.Add(this.label89);
            this.neuGroupBox2.Controls.Add(this.txtComeTo);
            this.neuGroupBox2.Controls.Add(this.label86);
            this.neuGroupBox2.Controls.Add(this.txtDeptChiefDoc);
            this.neuGroupBox2.Controls.Add(this.label49);
            this.neuGroupBox2.Controls.Add(this.label61);
            this.neuGroupBox2.Controls.Add(this.label60);
            this.neuGroupBox2.Controls.Add(this.label42);
            this.neuGroupBox2.Controls.Add(this.label41);
            this.neuGroupBox2.Controls.Add(this.label40);
            this.neuGroupBox2.Controls.Add(this.txtPiDays);
            this.neuGroupBox2.Controls.Add(this.label38);
            this.neuGroupBox2.Controls.Add(this.txtDiagDate);
            this.neuGroupBox2.Controls.Add(this.label37);
            this.neuGroupBox2.Controls.Add(this.txtDateOut);
            this.neuGroupBox2.Controls.Add(this.txtComeFrom);
            this.neuGroupBox2.Controls.Add(this.label64);
            this.neuGroupBox2.Controls.Add(this.label43);
            this.neuGroupBox2.Controls.Add(this.label36);
            this.neuGroupBox2.Controls.Add(this.label35);
            this.neuGroupBox2.Controls.Add(this.txtGraDocCode);
            this.neuGroupBox2.Controls.Add(this.label28);
            this.neuGroupBox2.Controls.Add(this.dtDateIn);
            this.neuGroupBox2.Controls.Add(this.label27);
            this.neuGroupBox2.Controls.Add(this.label26);
            this.neuGroupBox2.Controls.Add(this.label63);
            this.neuGroupBox2.Controls.Add(this.label62);
            this.neuGroupBox2.Controls.Add(this.txtDeptInHospital);
            this.neuGroupBox2.Controls.Add(this.txtDeptOut);
            this.neuGroupBox2.Controls.Add(this.txtCircs);
            this.neuGroupBox2.Controls.Add(this.txtRefresherDocd);
            this.neuGroupBox2.Controls.Add(this.txtAdmittingDoctor);
            this.neuGroupBox2.Controls.Add(this.txtAttendingDoctor);
            this.neuGroupBox2.Controls.Add(this.txtInAvenue);
            this.neuGroupBox2.Controls.Add(this.txtConsultingDoctor);
            this.neuGroupBox2.Controls.Add(this.txtPraDocCode);
            this.neuGroupBox2.Controls.Add(this.txtClinicDocd);
            this.neuGroupBox2.Controls.Add(this.txtRuyuanDiagNose);
            this.neuGroupBox2.Controls.Add(this.txtClinicDiag);
            this.neuGroupBox2.Controls.Add(this.neuLabel17);
            this.neuGroupBox2.Controls.Add(this.neuLabel16);
            this.neuGroupBox2.Controls.Add(this.label39);
            this.neuGroupBox2.Location = new System.Drawing.Point(8, 227);
            this.neuGroupBox2.Name = "neuGroupBox2";
            this.neuGroupBox2.Size = new System.Drawing.Size(768, 145);
            this.neuGroupBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox2.TabIndex = 502;
            this.neuGroupBox2.TabStop = false;
            this.neuGroupBox2.Text = "（2）患者住院信息";
            // 
            // txtOutRoom
            // 
            this.txtOutRoom.BackColor = System.Drawing.SystemColors.Window;
            this.txtOutRoom.Enabled = false;
            this.txtOutRoom.IsEnter2Tab = false;
            this.txtOutRoom.Location = new System.Drawing.Point(359, 45);
            this.txtOutRoom.MaxLength = 20;
            this.txtOutRoom.Name = "txtOutRoom";
            this.txtOutRoom.Size = new System.Drawing.Size(77, 21);
            this.txtOutRoom.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtOutRoom.TabIndex = 81;
            // 
            // txtInRoom
            // 
            this.txtInRoom.BackColor = System.Drawing.SystemColors.Window;
            this.txtInRoom.Enabled = false;
            this.txtInRoom.IsEnter2Tab = false;
            this.txtInRoom.Location = new System.Drawing.Point(359, 21);
            this.txtInRoom.MaxLength = 20;
            this.txtInRoom.Name = "txtInRoom";
            this.txtInRoom.Size = new System.Drawing.Size(77, 21);
            this.txtInRoom.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtInRoom.TabIndex = 79;
            // 
            // label89
            // 
            this.label89.AutoSize = true;
            this.label89.Location = new System.Drawing.Point(9, 98);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(41, 12);
            this.label89.TabIndex = 78;
            this.label89.Text = "科主任";
            // 
            // txtComeTo
            // 
            this.txtComeTo.IsEnter2Tab = false;
            this.txtComeTo.Location = new System.Drawing.Point(649, 120);
            this.txtComeTo.MaxLength = 100;
            this.txtComeTo.Name = "txtComeTo";
            this.txtComeTo.Size = new System.Drawing.Size(87, 21);
            this.txtComeTo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtComeTo.TabIndex = 77;
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.Location = new System.Drawing.Point(573, 123);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(65, 12);
            this.label86.TabIndex = 76;
            this.label86.Text = "转往何医院";
            // 
            // txtDeptChiefDoc
            // 
            this.txtDeptChiefDoc.EnterVisiable = true;
            this.txtDeptChiefDoc.IsFind = true;
            this.txtDeptChiefDoc.IsSelctNone = true;
            this.txtDeptChiefDoc.IsSendToNext = false;
            this.txtDeptChiefDoc.IsShowID = false;
            this.txtDeptChiefDoc.ItemText = "";
            this.txtDeptChiefDoc.ListBoxHeight = 115;
            this.txtDeptChiefDoc.ListBoxVisible = false;
            this.txtDeptChiefDoc.ListBoxWidth = 130;
            this.txtDeptChiefDoc.Location = new System.Drawing.Point(62, 95);
            this.txtDeptChiefDoc.MaxLength = 16;
            this.txtDeptChiefDoc.Name = "txtDeptChiefDoc";
            this.txtDeptChiefDoc.OmitFilter = true;
            this.txtDeptChiefDoc.SelectedItem = null;
            this.txtDeptChiefDoc.SelectNone = true;
            this.txtDeptChiefDoc.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDeptChiefDoc.ShowID = true;
            this.txtDeptChiefDoc.Size = new System.Drawing.Size(86, 21);
            this.txtDeptChiefDoc.TabIndex = 75;
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(210, 124);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(53, 12);
            this.label49.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label49.TabIndex = 63;
            this.label49.Text = "实习医生";
            this.label49.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Location = new System.Drawing.Point(318, 96);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(53, 12);
            this.label61.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label61.TabIndex = 62;
            this.label61.Text = "主治医师";
            this.label61.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(163, 96);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(53, 12);
            this.label60.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label60.TabIndex = 59;
            this.label60.Text = "主任医师";
            this.label60.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(623, 26);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(53, 12);
            this.label42.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label42.TabIndex = 61;
            this.label42.Text = "病人来源";
            this.label42.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(623, 48);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(53, 12);
            this.label41.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label41.TabIndex = 58;
            this.label41.Text = "门诊医生";
            this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(341, 72);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(53, 12);
            this.label40.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label40.TabIndex = 56;
            this.label40.Text = "入院诊断";
            this.label40.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPiDays
            // 
            this.txtPiDays.AllowNegative = false;
            this.txtPiDays.AutoPadRightZero = false;
            this.txtPiDays.Location = new System.Drawing.Point(62, 69);
            this.txtPiDays.MaxDigits = 0;
            this.txtPiDays.Name = "txtPiDays";
            this.txtPiDays.Size = new System.Drawing.Size(88, 21);
            this.txtPiDays.TabIndex = 69;
            this.txtPiDays.Text = "0";
            this.txtPiDays.WillShowError = false;
            this.txtPiDays.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PiDays_KeyDown);
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(6, 72);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(53, 12);
            this.label38.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label38.TabIndex = 60;
            this.label38.Text = "住院天数";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDiagDate
            // 
            this.txtDiagDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtDiagDate.IsEnter2Tab = false;
            this.txtDiagDate.Location = new System.Drawing.Point(250, 69);
            this.txtDiagDate.Name = "txtDiagDate";
            this.txtDiagDate.Size = new System.Drawing.Size(86, 21);
            this.txtDiagDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtDiagDate.TabIndex = 70;
            this.txtDiagDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RuyuanDiagNose_KeyDown);
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(185, 72);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(53, 12);
            this.label37.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label37.TabIndex = 49;
            this.label37.Text = "确诊日期";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDateOut
            // 
            this.txtDateOut.CustomFormat = "yyyy-MM-dd HH时";
            this.txtDateOut.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtDateOut.IsEnter2Tab = false;
            this.txtDateOut.Location = new System.Drawing.Point(62, 45);
            this.txtDateOut.Name = "txtDateOut";
            this.txtDateOut.Size = new System.Drawing.Size(116, 21);
            this.txtDateOut.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtDateOut.TabIndex = 67;
            this.txtDateOut.ValueChanged += new System.EventHandler(this.txtDateOut_ValueChanged);
            this.txtDateOut.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Date_Out_KeyDown);
            // 
            // txtComeFrom
            // 
            this.txtComeFrom.IsEnter2Tab = false;
            this.txtComeFrom.Location = new System.Drawing.Point(459, 120);
            this.txtComeFrom.MaxLength = 100;
            this.txtComeFrom.Name = "txtComeFrom";
            this.txtComeFrom.Size = new System.Drawing.Size(87, 21);
            this.txtComeFrom.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtComeFrom.TabIndex = 46;
            this.txtComeFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ComeFrom_KeyDown);
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Location = new System.Drawing.Point(8, 124);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(89, 12);
            this.label64.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label64.TabIndex = 55;
            this.label64.Text = "研究生实习医师";
            this.label64.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(376, 124);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(77, 12);
            this.label43.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label43.TabIndex = 0;
            this.label43.Text = "由何医院转来";
            this.label43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(6, 49);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(53, 12);
            this.label36.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label36.TabIndex = 50;
            this.label36.Text = "出院时间";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(184, 49);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(53, 12);
            this.label35.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label35.TabIndex = 47;
            this.label35.Text = "出院科室";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtGraDocCode
            // 
            this.txtGraDocCode.EnterVisiable = true;
            this.txtGraDocCode.IsFind = true;
            this.txtGraDocCode.IsSelctNone = true;
            this.txtGraDocCode.IsSendToNext = false;
            this.txtGraDocCode.IsShowID = false;
            this.txtGraDocCode.ItemText = "";
            this.txtGraDocCode.ListBoxHeight = 115;
            this.txtGraDocCode.ListBoxVisible = false;
            this.txtGraDocCode.ListBoxWidth = 130;
            this.txtGraDocCode.Location = new System.Drawing.Point(108, 120);
            this.txtGraDocCode.MaxLength = 16;
            this.txtGraDocCode.Name = "txtGraDocCode";
            this.txtGraDocCode.OmitFilter = true;
            this.txtGraDocCode.SelectedItem = null;
            this.txtGraDocCode.SelectNone = true;
            this.txtGraDocCode.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtGraDocCode.ShowID = true;
            this.txtGraDocCode.Size = new System.Drawing.Size(86, 21);
            this.txtGraDocCode.TabIndex = 45;
            this.txtGraDocCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GraDocCode_KeyDown);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(474, 26);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(53, 12);
            this.label28.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label28.TabIndex = 48;
            this.label28.Text = "入院情况";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtDateIn
            // 
            this.dtDateIn.CustomFormat = "yyyy-MM-dd HH时";
            this.dtDateIn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDateIn.IsEnter2Tab = false;
            this.dtDateIn.Location = new System.Drawing.Point(62, 20);
            this.dtDateIn.Name = "dtDateIn";
            this.dtDateIn.Size = new System.Drawing.Size(117, 21);
            this.dtDateIn.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtDateIn.TabIndex = 64;
            this.dtDateIn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Date_In_KeyDown);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(6, 26);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(53, 12);
            this.label27.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label27.TabIndex = 51;
            this.label27.Text = "入院时间";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(184, 26);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(53, 12);
            this.label26.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label26.TabIndex = 54;
            this.label26.Text = "入院科室";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Location = new System.Drawing.Point(623, 96);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(53, 12);
            this.label63.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label63.TabIndex = 52;
            this.label63.Text = "进修医师";
            this.label63.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Location = new System.Drawing.Point(470, 96);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(53, 12);
            this.label62.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label62.TabIndex = 53;
            this.label62.Text = "住院医师";
            this.label62.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDeptInHospital
            // 
            this.txtDeptInHospital.EnterVisiable = true;
            this.txtDeptInHospital.IsFind = true;
            this.txtDeptInHospital.IsSelctNone = true;
            this.txtDeptInHospital.IsSendToNext = false;
            this.txtDeptInHospital.IsShowID = false;
            this.txtDeptInHospital.ItemText = "";
            this.txtDeptInHospital.ListBoxHeight = 100;
            this.txtDeptInHospital.ListBoxVisible = false;
            this.txtDeptInHospital.ListBoxWidth = 100;
            this.txtDeptInHospital.Location = new System.Drawing.Point(243, 20);
            this.txtDeptInHospital.MaxLength = 20;
            this.txtDeptInHospital.Name = "txtDeptInHospital";
            this.txtDeptInHospital.OmitFilter = true;
            this.txtDeptInHospital.SelectedItem = null;
            this.txtDeptInHospital.SelectNone = true;
            this.txtDeptInHospital.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDeptInHospital.ShowID = true;
            this.txtDeptInHospital.Size = new System.Drawing.Size(86, 21);
            this.txtDeptInHospital.TabIndex = 65;
            this.txtDeptInHospital.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DeptInHospital_KeyDown);
            // 
            // txtDeptOut
            // 
            this.txtDeptOut.EnterVisiable = true;
            this.txtDeptOut.IsFind = true;
            this.txtDeptOut.IsSelctNone = true;
            this.txtDeptOut.IsSendToNext = false;
            this.txtDeptOut.IsShowID = false;
            this.txtDeptOut.ItemText = "";
            this.txtDeptOut.ListBoxHeight = 100;
            this.txtDeptOut.ListBoxVisible = false;
            this.txtDeptOut.ListBoxWidth = 100;
            this.txtDeptOut.Location = new System.Drawing.Point(243, 45);
            this.txtDeptOut.MaxLength = 20;
            this.txtDeptOut.Name = "txtDeptOut";
            this.txtDeptOut.OmitFilter = true;
            this.txtDeptOut.SelectedItem = null;
            this.txtDeptOut.SelectNone = true;
            this.txtDeptOut.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDeptOut.ShowID = true;
            this.txtDeptOut.Size = new System.Drawing.Size(86, 21);
            this.txtDeptOut.TabIndex = 68;
            this.txtDeptOut.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ClinicDiag_KeyDown);
            // 
            // txtCircs
            // 
            this.txtCircs.EnterVisiable = true;
            this.txtCircs.IsFind = true;
            this.txtCircs.IsSelctNone = true;
            this.txtCircs.IsSendToNext = false;
            this.txtCircs.IsShowID = false;
            this.txtCircs.ItemText = "";
            this.txtCircs.ListBoxHeight = 100;
            this.txtCircs.ListBoxVisible = false;
            this.txtCircs.ListBoxWidth = 100;
            this.txtCircs.Location = new System.Drawing.Point(552, 20);
            this.txtCircs.MaxLength = 10;
            this.txtCircs.Name = "txtCircs";
            this.txtCircs.OmitFilter = true;
            this.txtCircs.SelectedItem = null;
            this.txtCircs.SelectNone = true;
            this.txtCircs.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCircs.ShowID = true;
            this.txtCircs.Size = new System.Drawing.Size(67, 21);
            this.txtCircs.TabIndex = 66;
            this.txtCircs.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Circs_KeyDown);
            // 
            // txtRefresherDocd
            // 
            this.txtRefresherDocd.EnterVisiable = true;
            this.txtRefresherDocd.IsFind = true;
            this.txtRefresherDocd.IsSelctNone = true;
            this.txtRefresherDocd.IsSendToNext = false;
            this.txtRefresherDocd.IsShowID = false;
            this.txtRefresherDocd.ItemText = "";
            this.txtRefresherDocd.ListBoxHeight = 115;
            this.txtRefresherDocd.ListBoxVisible = false;
            this.txtRefresherDocd.ListBoxWidth = 130;
            this.txtRefresherDocd.Location = new System.Drawing.Point(680, 95);
            this.txtRefresherDocd.MaxLength = 16;
            this.txtRefresherDocd.Name = "txtRefresherDocd";
            this.txtRefresherDocd.OmitFilter = true;
            this.txtRefresherDocd.SelectedItem = null;
            this.txtRefresherDocd.SelectNone = true;
            this.txtRefresherDocd.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRefresherDocd.ShowID = true;
            this.txtRefresherDocd.Size = new System.Drawing.Size(83, 21);
            this.txtRefresherDocd.TabIndex = 73;
            this.txtRefresherDocd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RefresherDocd_KeyDown);
            // 
            // txtAdmittingDoctor
            // 
            this.txtAdmittingDoctor.EnterVisiable = true;
            this.txtAdmittingDoctor.IsFind = true;
            this.txtAdmittingDoctor.IsSelctNone = true;
            this.txtAdmittingDoctor.IsSendToNext = false;
            this.txtAdmittingDoctor.IsShowID = false;
            this.txtAdmittingDoctor.ItemText = "";
            this.txtAdmittingDoctor.ListBoxHeight = 115;
            this.txtAdmittingDoctor.ListBoxVisible = false;
            this.txtAdmittingDoctor.ListBoxWidth = 130;
            this.txtAdmittingDoctor.Location = new System.Drawing.Point(530, 95);
            this.txtAdmittingDoctor.MaxLength = 16;
            this.txtAdmittingDoctor.Name = "txtAdmittingDoctor";
            this.txtAdmittingDoctor.OmitFilter = true;
            this.txtAdmittingDoctor.SelectedItem = null;
            this.txtAdmittingDoctor.SelectNone = true;
            this.txtAdmittingDoctor.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAdmittingDoctor.ShowID = true;
            this.txtAdmittingDoctor.Size = new System.Drawing.Size(86, 21);
            this.txtAdmittingDoctor.TabIndex = 72;
            this.txtAdmittingDoctor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AdmittingDoctor_KeyDown);
            // 
            // txtAttendingDoctor
            // 
            this.txtAttendingDoctor.EnterVisiable = true;
            this.txtAttendingDoctor.IsFind = true;
            this.txtAttendingDoctor.IsSelctNone = true;
            this.txtAttendingDoctor.IsSendToNext = false;
            this.txtAttendingDoctor.IsShowID = false;
            this.txtAttendingDoctor.ItemText = "";
            this.txtAttendingDoctor.ListBoxHeight = 115;
            this.txtAttendingDoctor.ListBoxVisible = false;
            this.txtAttendingDoctor.ListBoxWidth = 130;
            this.txtAttendingDoctor.Location = new System.Drawing.Point(376, 95);
            this.txtAttendingDoctor.MaxLength = 16;
            this.txtAttendingDoctor.Name = "txtAttendingDoctor";
            this.txtAttendingDoctor.OmitFilter = true;
            this.txtAttendingDoctor.SelectedItem = null;
            this.txtAttendingDoctor.SelectNone = true;
            this.txtAttendingDoctor.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAttendingDoctor.ShowID = true;
            this.txtAttendingDoctor.Size = new System.Drawing.Size(86, 21);
            this.txtAttendingDoctor.TabIndex = 71;
            this.txtAttendingDoctor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AttendingDoctor_KeyDown);
            // 
            // txtInAvenue
            // 
            this.txtInAvenue.EnterVisiable = true;
            this.txtInAvenue.IsFind = true;
            this.txtInAvenue.IsSelctNone = true;
            this.txtInAvenue.IsSendToNext = false;
            this.txtInAvenue.IsShowID = false;
            this.txtInAvenue.ItemText = "";
            this.txtInAvenue.ListBoxHeight = 100;
            this.txtInAvenue.ListBoxVisible = false;
            this.txtInAvenue.ListBoxWidth = 100;
            this.txtInAvenue.Location = new System.Drawing.Point(680, 20);
            this.txtInAvenue.Name = "txtInAvenue";
            this.txtInAvenue.OmitFilter = true;
            this.txtInAvenue.SelectedItem = null;
            this.txtInAvenue.SelectNone = true;
            this.txtInAvenue.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtInAvenue.ShowID = true;
            this.txtInAvenue.Size = new System.Drawing.Size(83, 21);
            this.txtInAvenue.TabIndex = 32;
            this.txtInAvenue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InAvenue_KeyDown);
            // 
            // txtConsultingDoctor
            // 
            this.txtConsultingDoctor.EnterVisiable = true;
            this.txtConsultingDoctor.IsFind = true;
            this.txtConsultingDoctor.IsSelctNone = true;
            this.txtConsultingDoctor.IsSendToNext = false;
            this.txtConsultingDoctor.IsShowID = false;
            this.txtConsultingDoctor.ItemText = "";
            this.txtConsultingDoctor.ListBoxHeight = 115;
            this.txtConsultingDoctor.ListBoxVisible = false;
            this.txtConsultingDoctor.ListBoxWidth = 130;
            this.txtConsultingDoctor.Location = new System.Drawing.Point(222, 95);
            this.txtConsultingDoctor.MaxLength = 16;
            this.txtConsultingDoctor.Name = "txtConsultingDoctor";
            this.txtConsultingDoctor.OmitFilter = true;
            this.txtConsultingDoctor.SelectedItem = null;
            this.txtConsultingDoctor.SelectNone = true;
            this.txtConsultingDoctor.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtConsultingDoctor.ShowID = true;
            this.txtConsultingDoctor.Size = new System.Drawing.Size(86, 21);
            this.txtConsultingDoctor.TabIndex = 40;
            this.txtConsultingDoctor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ConsultingDoctor_KeyDown);
            // 
            // txtPraDocCode
            // 
            this.txtPraDocCode.EnterVisiable = true;
            this.txtPraDocCode.IsFind = true;
            this.txtPraDocCode.IsSelctNone = true;
            this.txtPraDocCode.IsSendToNext = false;
            this.txtPraDocCode.IsShowID = false;
            this.txtPraDocCode.ItemText = "";
            this.txtPraDocCode.ListBoxHeight = 115;
            this.txtPraDocCode.ListBoxVisible = false;
            this.txtPraDocCode.ListBoxWidth = 130;
            this.txtPraDocCode.Location = new System.Drawing.Point(269, 120);
            this.txtPraDocCode.MaxLength = 16;
            this.txtPraDocCode.Name = "txtPraDocCode";
            this.txtPraDocCode.OmitFilter = true;
            this.txtPraDocCode.SelectedItem = null;
            this.txtPraDocCode.SelectNone = true;
            this.txtPraDocCode.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPraDocCode.ShowID = true;
            this.txtPraDocCode.Size = new System.Drawing.Size(86, 21);
            this.txtPraDocCode.TabIndex = 44;
            this.txtPraDocCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PraDocCode_KeyDown);
            // 
            // txtClinicDocd
            // 
            this.txtClinicDocd.EnterVisiable = true;
            this.txtClinicDocd.IsFind = false;
            this.txtClinicDocd.IsSelctNone = true;
            this.txtClinicDocd.IsSendToNext = false;
            this.txtClinicDocd.IsShowID = false;
            this.txtClinicDocd.ItemText = "";
            this.txtClinicDocd.ListBoxHeight = 100;
            this.txtClinicDocd.ListBoxVisible = false;
            this.txtClinicDocd.ListBoxWidth = 100;
            this.txtClinicDocd.Location = new System.Drawing.Point(680, 44);
            this.txtClinicDocd.Name = "txtClinicDocd";
            this.txtClinicDocd.OmitFilter = true;
            this.txtClinicDocd.SelectedItem = null;
            this.txtClinicDocd.SelectNone = true;
            this.txtClinicDocd.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtClinicDocd.ShowID = true;
            this.txtClinicDocd.Size = new System.Drawing.Size(83, 21);
            this.txtClinicDocd.TabIndex = 36;
            this.txtClinicDocd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ClinicDocd_KeyDown);
            // 
            // txtRuyuanDiagNose
            // 
            this.txtRuyuanDiagNose.BackColor = System.Drawing.Color.Gainsboro;
            this.txtRuyuanDiagNose.IsEnter2Tab = false;
            this.txtRuyuanDiagNose.Location = new System.Drawing.Point(402, 69);
            this.txtRuyuanDiagNose.MaxLength = 20;
            this.txtRuyuanDiagNose.Name = "txtRuyuanDiagNose";
            this.txtRuyuanDiagNose.Size = new System.Drawing.Size(217, 21);
            this.txtRuyuanDiagNose.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtRuyuanDiagNose.TabIndex = 39;
            this.txtRuyuanDiagNose.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RuyuanDiagNose_KeyDown);
            // 
            // txtClinicDiag
            // 
            this.txtClinicDiag.BackColor = System.Drawing.Color.Gainsboro;
            this.txtClinicDiag.IsEnter2Tab = false;
            this.txtClinicDiag.Location = new System.Drawing.Point(497, 45);
            this.txtClinicDiag.MaxLength = 20;
            this.txtClinicDiag.Name = "txtClinicDiag";
            this.txtClinicDiag.Size = new System.Drawing.Size(122, 21);
            this.txtClinicDiag.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtClinicDiag.TabIndex = 35;
            this.txtClinicDiag.TextChanged += new System.EventHandler(this.ClinicDiag_TextChanged);
            this.txtClinicDiag.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ClinicDiag_KeyDown);
            this.txtClinicDiag.Enter += new System.EventHandler(this.ClinicDiag_Enter);
            // 
            // neuLabel17
            // 
            this.neuLabel17.AutoSize = true;
            this.neuLabel17.Location = new System.Drawing.Point(328, 50);
            this.neuLabel17.Name = "neuLabel17";
            this.neuLabel17.Size = new System.Drawing.Size(29, 12);
            this.neuLabel17.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel17.TabIndex = 82;
            this.neuLabel17.Text = "病室";
            this.neuLabel17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // neuLabel16
            // 
            this.neuLabel16.AutoSize = true;
            this.neuLabel16.Location = new System.Drawing.Point(328, 26);
            this.neuLabel16.Name = "neuLabel16";
            this.neuLabel16.Size = new System.Drawing.Size(29, 12);
            this.neuLabel16.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel16.TabIndex = 80;
            this.neuLabel16.Text = "病室";
            this.neuLabel16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(440, 49);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(53, 12);
            this.label39.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label39.TabIndex = 57;
            this.label39.Text = "门诊诊断";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // neuGroupBox3
            // 
            this.neuGroupBox3.Controls.Add(this.label30);
            this.neuGroupBox3.Controls.Add(this.dtThird);
            this.neuGroupBox3.Controls.Add(this.label33);
            this.neuGroupBox3.Controls.Add(this.label34);
            this.neuGroupBox3.Controls.Add(this.dtSecond);
            this.neuGroupBox3.Controls.Add(this.label31);
            this.neuGroupBox3.Controls.Add(this.label32);
            this.neuGroupBox3.Controls.Add(this.dtFirstTime);
            this.neuGroupBox3.Controls.Add(this.label29);
            this.neuGroupBox3.Controls.Add(this.txtDeptThird);
            this.neuGroupBox3.Controls.Add(this.txtFirstDept);
            this.neuGroupBox3.Controls.Add(this.txtDeptSecond);
            this.neuGroupBox3.Location = new System.Drawing.Point(8, 372);
            this.neuGroupBox3.Name = "neuGroupBox3";
            this.neuGroupBox3.Size = new System.Drawing.Size(771, 81);
            this.neuGroupBox3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox3.TabIndex = 503;
            this.neuGroupBox3.TabStop = false;
            this.neuGroupBox3.Text = "（3）患者转科信息";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(4, 17);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(53, 12);
            this.label30.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label30.TabIndex = 54;
            this.label30.Text = "转入时间";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtThird
            // 
            this.dtThird.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtThird.IsEnter2Tab = false;
            this.dtThird.Location = new System.Drawing.Point(62, 58);
            this.dtThird.Name = "dtThird";
            this.dtThird.Size = new System.Drawing.Size(88, 21);
            this.dtThird.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtThird.TabIndex = 63;
            this.dtThird.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtThird_KeyDown);
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(4, 60);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(53, 12);
            this.label33.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label33.TabIndex = 56;
            this.label33.Text = "转入时间";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(158, 60);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(53, 12);
            this.label34.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label34.TabIndex = 57;
            this.label34.Text = "转入科室";
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtSecond
            // 
            this.dtSecond.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtSecond.IsEnter2Tab = false;
            this.dtSecond.Location = new System.Drawing.Point(62, 35);
            this.dtSecond.Name = "dtSecond";
            this.dtSecond.Size = new System.Drawing.Size(87, 21);
            this.dtSecond.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtSecond.TabIndex = 61;
            this.dtSecond.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker4_KeyDown);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(4, 39);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(53, 12);
            this.label31.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label31.TabIndex = 53;
            this.label31.Text = "转入时间";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(159, 39);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(53, 12);
            this.label32.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label32.TabIndex = 58;
            this.label32.Text = "转入科室";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtFirstTime
            // 
            this.dtFirstTime.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFirstTime.IsEnter2Tab = false;
            this.dtFirstTime.Location = new System.Drawing.Point(62, 13);
            this.dtFirstTime.Name = "dtFirstTime";
            this.dtFirstTime.Size = new System.Drawing.Size(88, 21);
            this.dtFirstTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtFirstTime.TabIndex = 59;
            this.dtFirstTime.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker3_KeyDown);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(158, 17);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(53, 12);
            this.label29.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label29.TabIndex = 55;
            this.label29.Text = "转入科室";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDeptThird
            // 
            this.txtDeptThird.EnterVisiable = true;
            this.txtDeptThird.IsFind = true;
            this.txtDeptThird.IsSelctNone = true;
            this.txtDeptThird.IsSendToNext = false;
            this.txtDeptThird.IsShowID = false;
            this.txtDeptThird.ItemText = "";
            this.txtDeptThird.ListBoxHeight = 100;
            this.txtDeptThird.ListBoxVisible = false;
            this.txtDeptThird.ListBoxWidth = 100;
            this.txtDeptThird.Location = new System.Drawing.Point(222, 58);
            this.txtDeptThird.MaxLength = 20;
            this.txtDeptThird.Name = "txtDeptThird";
            this.txtDeptThird.OmitFilter = true;
            this.txtDeptThird.SelectedItem = null;
            this.txtDeptThird.SelectNone = true;
            this.txtDeptThird.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDeptThird.ShowID = true;
            this.txtDeptThird.Size = new System.Drawing.Size(201, 21);
            this.txtDeptThird.TabIndex = 64;
            this.txtDeptThird.KeyDown += new System.Windows.Forms.KeyEventHandler(this.deptThird_KeyDown);
            // 
            // txtFirstDept
            // 
            this.txtFirstDept.EnterVisiable = true;
            this.txtFirstDept.IsFind = true;
            this.txtFirstDept.IsSelctNone = true;
            this.txtFirstDept.IsSendToNext = false;
            this.txtFirstDept.IsShowID = false;
            this.txtFirstDept.ItemText = "";
            this.txtFirstDept.ListBoxHeight = 100;
            this.txtFirstDept.ListBoxVisible = false;
            this.txtFirstDept.ListBoxWidth = 100;
            this.txtFirstDept.Location = new System.Drawing.Point(222, 13);
            this.txtFirstDept.MaxLength = 20;
            this.txtFirstDept.Name = "txtFirstDept";
            this.txtFirstDept.OmitFilter = true;
            this.txtFirstDept.SelectedItem = null;
            this.txtFirstDept.SelectNone = true;
            this.txtFirstDept.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtFirstDept.ShowID = true;
            this.txtFirstDept.Size = new System.Drawing.Size(201, 21);
            this.txtFirstDept.TabIndex = 60;
            this.txtFirstDept.KeyDown += new System.Windows.Forms.KeyEventHandler(this.firstDept_KeyDown);
            // 
            // txtDeptSecond
            // 
            this.txtDeptSecond.EnterVisiable = true;
            this.txtDeptSecond.IsFind = false;
            this.txtDeptSecond.IsSelctNone = true;
            this.txtDeptSecond.IsSendToNext = false;
            this.txtDeptSecond.IsShowID = false;
            this.txtDeptSecond.ItemText = "";
            this.txtDeptSecond.ListBoxHeight = 100;
            this.txtDeptSecond.ListBoxVisible = false;
            this.txtDeptSecond.ListBoxWidth = 100;
            this.txtDeptSecond.Location = new System.Drawing.Point(222, 35);
            this.txtDeptSecond.MaxLength = 20;
            this.txtDeptSecond.Name = "txtDeptSecond";
            this.txtDeptSecond.OmitFilter = true;
            this.txtDeptSecond.SelectedItem = null;
            this.txtDeptSecond.SelectNone = true;
            this.txtDeptSecond.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDeptSecond.ShowID = true;
            this.txtDeptSecond.Size = new System.Drawing.Size(201, 21);
            this.txtDeptSecond.TabIndex = 62;
            this.txtDeptSecond.KeyDown += new System.Windows.Forms.KeyEventHandler(this.deptSecond_KeyDown);
            // 
            // neuGroupBox4
            // 
            this.neuGroupBox4.Controls.Add(this.neuTxtPharmacyAllergic2);
            this.neuGroupBox4.Controls.Add(this.neuTxtPharmacyAllergic1);
            this.neuGroupBox4.Controls.Add(this.txtInjuryOrPoisoningCause);
            this.neuGroupBox4.Controls.Add(this.neuLabel10);
            this.neuGroupBox4.Controls.Add(this.txtInfectionPositionNew);
            this.neuGroupBox4.Controls.Add(this.neuLabel2);
            this.neuGroupBox4.Controls.Add(this.txtInfectNum);
            this.neuGroupBox4.Controls.Add(this.label44);
            this.neuGroupBox4.Controls.Add(this.txtPharmacyAllergic2);
            this.neuGroupBox4.Controls.Add(this.neuLabel5);
            this.neuGroupBox4.Controls.Add(this.txtPharmacyAllergic1);
            this.neuGroupBox4.Controls.Add(this.neuLabel3);
            this.neuGroupBox4.Location = new System.Drawing.Point(8, 457);
            this.neuGroupBox4.Name = "neuGroupBox4";
            this.neuGroupBox4.Size = new System.Drawing.Size(771, 69);
            this.neuGroupBox4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox4.TabIndex = 504;
            this.neuGroupBox4.TabStop = false;
            this.neuGroupBox4.Text = "（4）患者感染/过敏信息";
            // 
            // neuTxtPharmacyAllergic2
            // 
            this.neuTxtPharmacyAllergic2.IsEnter2Tab = false;
            this.neuTxtPharmacyAllergic2.Location = new System.Drawing.Point(372, 41);
            this.neuTxtPharmacyAllergic2.MaxLength = 20;
            this.neuTxtPharmacyAllergic2.Name = "neuTxtPharmacyAllergic2";
            this.neuTxtPharmacyAllergic2.Size = new System.Drawing.Size(218, 21);
            this.neuTxtPharmacyAllergic2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTxtPharmacyAllergic2.TabIndex = 72;
            // 
            // neuTxtPharmacyAllergic1
            // 
            this.neuTxtPharmacyAllergic1.IsEnter2Tab = false;
            this.neuTxtPharmacyAllergic1.Location = new System.Drawing.Point(82, 41);
            this.neuTxtPharmacyAllergic1.MaxLength = 20;
            this.neuTxtPharmacyAllergic1.Name = "neuTxtPharmacyAllergic1";
            this.neuTxtPharmacyAllergic1.Size = new System.Drawing.Size(166, 21);
            this.neuTxtPharmacyAllergic1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTxtPharmacyAllergic1.TabIndex = 71;
            // 
            // txtInjuryOrPoisoningCause
            // 
            this.txtInjuryOrPoisoningCause.EnterVisiable = true;
            this.txtInjuryOrPoisoningCause.IsFind =true ;
            this.txtInjuryOrPoisoningCause.IsSelctNone = true;
            this.txtInjuryOrPoisoningCause.IsSendToNext = false;
            this.txtInjuryOrPoisoningCause.IsShowID = false;
            this.txtInjuryOrPoisoningCause.ItemText = "";
            this.txtInjuryOrPoisoningCause.ListBoxHeight = 100;
            this.txtInjuryOrPoisoningCause.ListBoxVisible = false;
            this.txtInjuryOrPoisoningCause.ListBoxWidth = 100;
            this.txtInjuryOrPoisoningCause.Location = new System.Drawing.Point(372, 17);
            this.txtInjuryOrPoisoningCause.Name = "txtInjuryOrPoisoningCause";
            this.txtInjuryOrPoisoningCause.OmitFilter = true;
            this.txtInjuryOrPoisoningCause.SelectedItem = null;
            this.txtInjuryOrPoisoningCause.SelectNone = true;
            this.txtInjuryOrPoisoningCause.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtInjuryOrPoisoningCause.ShowID = true;
            this.txtInjuryOrPoisoningCause.Size = new System.Drawing.Size(218, 21);
            this.txtInjuryOrPoisoningCause.TabIndex = 70;
            this.txtInjuryOrPoisoningCause.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInjuryOrPoisoningCause_KeyDown);
            // 
            // neuLabel10
            // 
            this.neuLabel10.AutoSize = true;
            this.neuLabel10.Location = new System.Drawing.Point(257, 21);
            this.neuLabel10.Name = "neuLabel10";
            this.neuLabel10.Size = new System.Drawing.Size(113, 12);
            this.neuLabel10.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel10.TabIndex = 64;
            this.neuLabel10.Text = "损伤中毒的外部原因";
            this.neuLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtInfectionPositionNew
            // 
            this.txtInfectionPositionNew.IsEnter2Tab = false;
            this.txtInfectionPositionNew.Location = new System.Drawing.Point(81, 17);
            this.txtInfectionPositionNew.Name = "txtInfectionPositionNew";
            this.txtInfectionPositionNew.Size = new System.Drawing.Size(167, 21);
            this.txtInfectionPositionNew.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtInfectionPositionNew.TabIndex = 70;
            this.txtInfectionPositionNew.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInfectionPosition_KeyDown);
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(1, 21);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(77, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 64;
            this.neuLabel2.Text = "医院感染名称";
            this.neuLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtInfectNum
            // 
            this.txtInfectNum.AllowNegative = false;
            this.txtInfectNum.AutoPadRightZero = false;
            this.txtInfectNum.Location = new System.Drawing.Point(654, 17);
            this.txtInfectNum.MaxDigits = 0;
            this.txtInfectNum.MaxLength = 2;
            this.txtInfectNum.Name = "txtInfectNum";
            this.txtInfectNum.Size = new System.Drawing.Size(104, 21);
            this.txtInfectNum.TabIndex = 66;
            this.txtInfectNum.Text = "0";
            this.txtInfectNum.WillShowError = false;
            this.txtInfectNum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.infectNum_KeyDown);
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(596, 21);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(53, 12);
            this.label44.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label44.TabIndex = 61;
            this.label44.Text = "院感次数";
            this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPharmacyAllergic2
            // 
            this.txtPharmacyAllergic2.EnterVisiable = true;
            this.txtPharmacyAllergic2.IsFind = true;
            this.txtPharmacyAllergic2.IsSelctNone = true;
            this.txtPharmacyAllergic2.IsSendToNext = false;
            this.txtPharmacyAllergic2.IsShowID = false;
            this.txtPharmacyAllergic2.ItemText = "";
            this.txtPharmacyAllergic2.ListBoxHeight = 100;
            this.txtPharmacyAllergic2.ListBoxVisible = false;
            this.txtPharmacyAllergic2.ListBoxWidth = 100;
            this.txtPharmacyAllergic2.Location = new System.Drawing.Point(372, 41);
            this.txtPharmacyAllergic2.MaxLength = 20;
            this.txtPharmacyAllergic2.Name = "txtPharmacyAllergic2";
            this.txtPharmacyAllergic2.OmitFilter = true;
            this.txtPharmacyAllergic2.SelectedItem = null;
            this.txtPharmacyAllergic2.SelectNone = true;
            this.txtPharmacyAllergic2.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPharmacyAllergic2.ShowID = true;
            this.txtPharmacyAllergic2.Size = new System.Drawing.Size(218, 21);
            this.txtPharmacyAllergic2.TabIndex = 56;
            this.txtPharmacyAllergic2.Visible = false;
            this.txtPharmacyAllergic2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPharmacyAllergic2_KeyDown);
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Location = new System.Drawing.Point(299, 44);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(53, 12);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 0;
            this.neuLabel5.Text = "病理诊断";
            this.neuLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPharmacyAllergic1
            // 
            this.txtPharmacyAllergic1.EnterVisiable = true;
            this.txtPharmacyAllergic1.IsFind = true;
            this.txtPharmacyAllergic1.IsSelctNone = true;
            this.txtPharmacyAllergic1.IsSendToNext = false;
            this.txtPharmacyAllergic1.IsShowID = false;
            this.txtPharmacyAllergic1.ItemText = "";
            this.txtPharmacyAllergic1.ListBoxHeight = 100;
            this.txtPharmacyAllergic1.ListBoxVisible = false;
            this.txtPharmacyAllergic1.ListBoxWidth = 100;
            this.txtPharmacyAllergic1.Location = new System.Drawing.Point(82, 41);
            this.txtPharmacyAllergic1.MaxLength = 20;
            this.txtPharmacyAllergic1.Name = "txtPharmacyAllergic1";
            this.txtPharmacyAllergic1.OmitFilter = true;
            this.txtPharmacyAllergic1.SelectedItem = null;
            this.txtPharmacyAllergic1.SelectNone = true;
            this.txtPharmacyAllergic1.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPharmacyAllergic1.ShowID = true;
            this.txtPharmacyAllergic1.Size = new System.Drawing.Size(166, 21);
            this.txtPharmacyAllergic1.TabIndex = 55;
            this.txtPharmacyAllergic1.Visible = false;
            this.txtPharmacyAllergic1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPharmacyAllergic1_KeyDown);
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(1, 44);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(53, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 0;
            this.neuLabel3.Text = "过敏药物";
            this.neuLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // neuGroupBox5
            // 
            this.neuGroupBox5.Controls.Add(this.txtSuccTimes);
            this.neuGroupBox5.Controls.Add(this.label56);
            this.neuGroupBox5.Controls.Add(this.txtSalvTimes);
            this.neuGroupBox5.Controls.Add(this.label55);
            this.neuGroupBox5.Controls.Add(this.label48);
            this.neuGroupBox5.Controls.Add(this.label54);
            this.neuGroupBox5.Controls.Add(this.label47);
            this.neuGroupBox5.Controls.Add(this.label53);
            this.neuGroupBox5.Controls.Add(this.label46);
            this.neuGroupBox5.Controls.Add(this.label52);
            this.neuGroupBox5.Controls.Add(this.label51);
            this.neuGroupBox5.Controls.Add(this.txtHivAb);
            this.neuGroupBox5.Controls.Add(this.label50);
            this.neuGroupBox5.Controls.Add(this.txtHcvAb);
            this.neuGroupBox5.Controls.Add(this.txtFsBl);
            this.neuGroupBox5.Controls.Add(this.txtHbsag);
            this.neuGroupBox5.Controls.Add(this.txtClPa);
            this.neuGroupBox5.Controls.Add(this.txtOpbOpa);
            this.neuGroupBox5.Controls.Add(this.txtPiPo);
            this.neuGroupBox5.Controls.Add(this.txtCePi);
            this.neuGroupBox5.Location = new System.Drawing.Point(8, 529);
            this.neuGroupBox5.Name = "neuGroupBox5";
            this.neuGroupBox5.Size = new System.Drawing.Size(771, 79);
            this.neuGroupBox5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox5.TabIndex = 505;
            this.neuGroupBox5.TabStop = false;
            this.neuGroupBox5.Text = "（5）患者诊断符合情况";
            // 
            // txtSuccTimes
            // 
            this.txtSuccTimes.AllowNegative = false;
            this.txtSuccTimes.AutoPadRightZero = false;
            this.txtSuccTimes.Location = new System.Drawing.Point(736, 49);
            this.txtSuccTimes.MaxDigits = 0;
            this.txtSuccTimes.MaxLength = 2;
            this.txtSuccTimes.Name = "txtSuccTimes";
            this.txtSuccTimes.Size = new System.Drawing.Size(29, 21);
            this.txtSuccTimes.TabIndex = 80;
            this.txtSuccTimes.Text = "0";
            this.txtSuccTimes.WillShowError = false;
            this.txtSuccTimes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SuccTimes_KeyDown);
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(678, 53);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(53, 12);
            this.label56.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label56.TabIndex = 72;
            this.label56.Text = "成功次数";
            this.label56.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSalvTimes
            // 
            this.txtSalvTimes.AllowNegative = false;
            this.txtSalvTimes.AutoPadRightZero = false;
            this.txtSalvTimes.Location = new System.Drawing.Point(639, 49);
            this.txtSalvTimes.MaxDigits = 0;
            this.txtSalvTimes.MaxLength = 2;
            this.txtSalvTimes.Name = "txtSalvTimes";
            this.txtSalvTimes.Size = new System.Drawing.Size(29, 21);
            this.txtSalvTimes.TabIndex = 79;
            this.txtSalvTimes.Text = "0";
            this.txtSalvTimes.WillShowError = false;
            this.txtSalvTimes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SalvTimes_KeyDown);
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(580, 53);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(53, 12);
            this.label55.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label55.TabIndex = 70;
            this.label55.Text = "抢救次数";
            this.label55.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(241, 22);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(41, 12);
            this.label48.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label48.TabIndex = 62;
            this.label48.Text = "HIV-Ab";
            this.label48.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(464, 53);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(53, 12);
            this.label54.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label54.TabIndex = 67;
            this.label54.Text = "放射病理";
            this.label54.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(119, 22);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(47, 12);
            this.label47.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label47.TabIndex = 60;
            this.label47.Text = "HCV -Ab";
            this.label47.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(350, 53);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(53, 12);
            this.label53.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label53.TabIndex = 68;
            this.label53.Text = "临床病理";
            this.label53.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(9, 22);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(47, 12);
            this.label46.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label46.TabIndex = 63;
            this.label46.Text = "HbsAg  ";
            this.label46.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(232, 52);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(53, 12);
            this.label52.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label52.TabIndex = 71;
            this.label52.Text = "术前术后";
            this.label52.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(120, 53);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(53, 12);
            this.label51.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label51.TabIndex = 69;
            this.label51.Text = "入院出院";
            this.label51.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtHivAb
            // 
            this.txtHivAb.EnterVisiable = true;
            this.txtHivAb.IsFind = true;
            this.txtHivAb.IsSelctNone = true;
            this.txtHivAb.IsSendToNext = false;
            this.txtHivAb.IsShowID = false;
            this.txtHivAb.ItemText = "";
            this.txtHivAb.ListBoxHeight = 100;
            this.txtHivAb.ListBoxVisible = false;
            this.txtHivAb.ListBoxWidth = 100;
            this.txtHivAb.Location = new System.Drawing.Point(290, 20);
            this.txtHivAb.MaxLength = 5;
            this.txtHivAb.Name = "txtHivAb";
            this.txtHivAb.OmitFilter = true;
            this.txtHivAb.SelectedItem = null;
            this.txtHivAb.SelectNone = true;
            this.txtHivAb.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtHivAb.ShowID = true;
            this.txtHivAb.Size = new System.Drawing.Size(54, 21);
            this.txtHivAb.TabIndex = 69;
            this.txtHivAb.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HivAb_KeyDown);
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(5, 53);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(53, 12);
            this.label50.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label50.TabIndex = 73;
            this.label50.Text = "门诊出院";
            this.label50.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtHcvAb
            // 
            this.txtHcvAb.EnterVisiable = true;
            this.txtHcvAb.IsFind = true;
            this.txtHcvAb.IsSelctNone = true;
            this.txtHcvAb.IsSendToNext = false;
            this.txtHcvAb.IsShowID = false;
            this.txtHcvAb.ItemText = "";
            this.txtHcvAb.ListBoxHeight = 100;
            this.txtHcvAb.ListBoxVisible = false;
            this.txtHcvAb.ListBoxWidth = 100;
            this.txtHcvAb.Location = new System.Drawing.Point(174, 19);
            this.txtHcvAb.MaxLength = 5;
            this.txtHcvAb.Name = "txtHcvAb";
            this.txtHcvAb.OmitFilter = true;
            this.txtHcvAb.SelectedItem = null;
            this.txtHcvAb.SelectNone = true;
            this.txtHcvAb.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtHcvAb.ShowID = true;
            this.txtHcvAb.Size = new System.Drawing.Size(54, 21);
            this.txtHcvAb.TabIndex = 68;
            this.txtHcvAb.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HcvAb_KeyDown);
            // 
            // txtFsBl
            // 
            this.txtFsBl.EnterVisiable = true;
            this.txtFsBl.IsFind = true;
            this.txtFsBl.IsSelctNone = true;
            this.txtFsBl.IsSendToNext = false;
            this.txtFsBl.IsShowID = false;
            this.txtFsBl.ItemText = "";
            this.txtFsBl.ListBoxHeight = 100;
            this.txtFsBl.ListBoxVisible = false;
            this.txtFsBl.ListBoxWidth = 100;
            this.txtFsBl.Location = new System.Drawing.Point(520, 49);
            this.txtFsBl.MaxLength = 10;
            this.txtFsBl.Name = "txtFsBl";
            this.txtFsBl.OmitFilter = true;
            this.txtFsBl.SelectedItem = null;
            this.txtFsBl.SelectNone = true;
            this.txtFsBl.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtFsBl.ShowID = true;
            this.txtFsBl.Size = new System.Drawing.Size(54, 21);
            this.txtFsBl.TabIndex = 78;
            this.txtFsBl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FsBl_KeyDown);
            // 
            // txtHbsag
            // 
            this.txtHbsag.EnterVisiable = true;
            this.txtHbsag.IsFind = true;
            this.txtHbsag.IsSelctNone = true;
            this.txtHbsag.IsSendToNext = false;
            this.txtHbsag.IsShowID = false;
            this.txtHbsag.ItemText = "";
            this.txtHbsag.ListBoxHeight = 100;
            this.txtHbsag.ListBoxVisible = false;
            this.txtHbsag.ListBoxWidth = 100;
            this.txtHbsag.Location = new System.Drawing.Point(62, 19);
            this.txtHbsag.MaxLength = 5;
            this.txtHbsag.Name = "txtHbsag";
            this.txtHbsag.OmitFilter = true;
            this.txtHbsag.SelectedItem = null;
            this.txtHbsag.SelectNone = true;
            this.txtHbsag.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtHbsag.ShowID = true;
            this.txtHbsag.Size = new System.Drawing.Size(54, 21);
            this.txtHbsag.TabIndex = 67;
            this.txtHbsag.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Hbsag_KeyDown);
            // 
            // txtClPa
            // 
            this.txtClPa.EnterVisiable = true;
            this.txtClPa.IsFind = true;
            this.txtClPa.IsSelctNone = true;
            this.txtClPa.IsSendToNext = false;
            this.txtClPa.IsShowID = false;
            this.txtClPa.ItemText = "";
            this.txtClPa.ListBoxHeight = 100;
            this.txtClPa.ListBoxVisible = false;
            this.txtClPa.ListBoxWidth = 100;
            this.txtClPa.Location = new System.Drawing.Point(406, 49);
            this.txtClPa.MaxLength = 10;
            this.txtClPa.Name = "txtClPa";
            this.txtClPa.OmitFilter = true;
            this.txtClPa.SelectedItem = null;
            this.txtClPa.SelectNone = true;
            this.txtClPa.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtClPa.ShowID = true;
            this.txtClPa.Size = new System.Drawing.Size(54, 21);
            this.txtClPa.TabIndex = 77;
            this.txtClPa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ClPa_KeyDown);
            // 
            // txtOpbOpa
            // 
            this.txtOpbOpa.EnterVisiable = true;
            this.txtOpbOpa.IsFind = true;
            this.txtOpbOpa.IsSelctNone = true;
            this.txtOpbOpa.IsSendToNext = false;
            this.txtOpbOpa.IsShowID = false;
            this.txtOpbOpa.ItemText = "";
            this.txtOpbOpa.ListBoxHeight = 100;
            this.txtOpbOpa.ListBoxVisible = false;
            this.txtOpbOpa.ListBoxWidth = 100;
            this.txtOpbOpa.Location = new System.Drawing.Point(290, 48);
            this.txtOpbOpa.MaxLength = 10;
            this.txtOpbOpa.Name = "txtOpbOpa";
            this.txtOpbOpa.OmitFilter = true;
            this.txtOpbOpa.SelectedItem = null;
            this.txtOpbOpa.SelectNone = true;
            this.txtOpbOpa.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtOpbOpa.ShowID = true;
            this.txtOpbOpa.Size = new System.Drawing.Size(54, 21);
            this.txtOpbOpa.TabIndex = 76;
            this.txtOpbOpa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OpbOpa_KeyDown);
            // 
            // txtPiPo
            // 
            this.txtPiPo.EnterVisiable = true;
            this.txtPiPo.IsFind = true;
            this.txtPiPo.IsSelctNone = true;
            this.txtPiPo.IsSendToNext = false;
            this.txtPiPo.IsShowID = false;
            this.txtPiPo.ItemText = "";
            this.txtPiPo.ListBoxHeight = 100;
            this.txtPiPo.ListBoxVisible = false;
            this.txtPiPo.ListBoxWidth = 100;
            this.txtPiPo.Location = new System.Drawing.Point(174, 49);
            this.txtPiPo.MaxLength = 10;
            this.txtPiPo.Name = "txtPiPo";
            this.txtPiPo.OmitFilter = true;
            this.txtPiPo.SelectedItem = null;
            this.txtPiPo.SelectNone = true;
            this.txtPiPo.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPiPo.ShowID = true;
            this.txtPiPo.Size = new System.Drawing.Size(54, 21);
            this.txtPiPo.TabIndex = 75;
            this.txtPiPo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PiPo_KeyDown);
            // 
            // txtCePi
            // 
            this.txtCePi.EnterVisiable = true;
            this.txtCePi.IsFind = true;
            this.txtCePi.IsSelctNone = true;
            this.txtCePi.IsSendToNext = false;
            this.txtCePi.IsShowID = false;
            this.txtCePi.ItemText = "";
            this.txtCePi.ListBoxHeight = 100;
            this.txtCePi.ListBoxVisible = false;
            this.txtCePi.ListBoxWidth = 100;
            this.txtCePi.Location = new System.Drawing.Point(62, 49);
            this.txtCePi.MaxLength = 10;
            this.txtCePi.Name = "txtCePi";
            this.txtCePi.OmitFilter = true;
            this.txtCePi.SelectedItem = null;
            this.txtCePi.SelectNone = true;
            this.txtCePi.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCePi.ShowID = true;
            this.txtCePi.Size = new System.Drawing.Size(54, 21);
            this.txtCePi.TabIndex = 74;
            this.txtCePi.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CePi_KeyDown);
            // 
            // neuGroupBox6
            // 
            this.neuGroupBox6.Controls.Add(this.neuLabel15);
            this.neuGroupBox6.Controls.Add(this.txtReactionTransfuse);
            this.neuGroupBox6.Controls.Add(this.textBox13);
            this.neuGroupBox6.Controls.Add(this.textBox12);
            this.neuGroupBox6.Controls.Add(this.textBox11);
            this.neuGroupBox6.Controls.Add(this.textBox10);
            this.neuGroupBox6.Controls.Add(this.textBox9);
            this.neuGroupBox6.Controls.Add(this.textBox8);
            this.neuGroupBox6.Controls.Add(this.textBox7);
            this.neuGroupBox6.Controls.Add(this.textBox6);
            this.neuGroupBox6.Controls.Add(this.textBox5);
            this.neuGroupBox6.Controls.Add(this.textBox4);
            this.neuGroupBox6.Controls.Add(this.textBox3);
            this.neuGroupBox6.Controls.Add(this.textBox2);
            this.neuGroupBox6.Controls.Add(this.textBox1);
            this.neuGroupBox6.Controls.Add(this.cbYnFirst);
            this.neuGroupBox6.Controls.Add(this.txtPathNumb);
            this.neuGroupBox6.Controls.Add(this.txtCtNumb);
            this.neuGroupBox6.Controls.Add(this.label92);
            this.neuGroupBox6.Controls.Add(this.txtSPecalNus);
            this.neuGroupBox6.Controls.Add(this.label90);
            this.neuGroupBox6.Controls.Add(this.txtStrictNuss);
            this.neuGroupBox6.Controls.Add(this.label91);
            this.neuGroupBox6.Controls.Add(this.txtIIINus);
            this.neuGroupBox6.Controls.Add(this.txtIINus);
            this.neuGroupBox6.Controls.Add(this.label88);
            this.neuGroupBox6.Controls.Add(this.txtINus);
            this.neuGroupBox6.Controls.Add(this.label85);
            this.neuGroupBox6.Controls.Add(this.txtSuperNus);
            this.neuGroupBox6.Controls.Add(this.label82);
            this.neuGroupBox6.Controls.Add(this.txtOutconNum);
            this.neuGroupBox6.Controls.Add(this.label83);
            this.neuGroupBox6.Controls.Add(this.txtInconNum);
            this.neuGroupBox6.Controls.Add(this.txtBloodOther);
            this.neuGroupBox6.Controls.Add(this.label81);
            this.neuGroupBox6.Controls.Add(this.txtBloodWhole);
            this.neuGroupBox6.Controls.Add(this.label80);
            this.neuGroupBox6.Controls.Add(this.txtBodyAnotomize);
            this.neuGroupBox6.Controls.Add(this.label78);
            this.neuGroupBox6.Controls.Add(this.txtBloodPlatelet);
            this.neuGroupBox6.Controls.Add(this.label77);
            this.neuGroupBox6.Controls.Add(this.txtBloodRed);
            this.neuGroupBox6.Controls.Add(this.label76);
            this.neuGroupBox6.Controls.Add(this.neuLabel12);
            this.neuGroupBox6.Controls.Add(this.neuLabel11);
            this.neuGroupBox6.Controls.Add(this.label75);
            this.neuGroupBox6.Controls.Add(this.label74);
            this.neuGroupBox6.Controls.Add(this.label73);
            this.neuGroupBox6.Controls.Add(this.cbDisease30);
            this.neuGroupBox6.Controls.Add(this.cbTechSerc);
            this.neuGroupBox6.Controls.Add(this.label72);
            this.neuGroupBox6.Controls.Add(this.label71);
            this.neuGroupBox6.Controls.Add(this.label70);
            this.neuGroupBox6.Controls.Add(this.txtVisiPeriYear);
            this.neuGroupBox6.Controls.Add(this.txtVisiPeriMonth);
            this.neuGroupBox6.Controls.Add(this.txtVisiPeriWeek);
            this.neuGroupBox6.Controls.Add(this.label69);
            this.neuGroupBox6.Controls.Add(this.cbVisiStat);
            this.neuGroupBox6.Controls.Add(this.cbBodyCheck);
            this.neuGroupBox6.Controls.Add(this.label87);
            this.neuGroupBox6.Controls.Add(this.label84);
            this.neuGroupBox6.Controls.Add(this.txtFourDiseasesReport);
            this.neuGroupBox6.Controls.Add(this.txtInfectionDiseasesReport);
            this.neuGroupBox6.Controls.Add(this.txtReactionBlood);
            this.neuGroupBox6.Controls.Add(this.txtRhBlood);
            this.neuGroupBox6.Controls.Add(this.txtBloodType);
            this.neuGroupBox6.Controls.Add(this.txtPETNumb);
            this.neuGroupBox6.Controls.Add(this.neuLabel7);
            this.neuGroupBox6.Controls.Add(this.txtECTNumb);
            this.neuGroupBox6.Controls.Add(this.neuLabel6);
            this.neuGroupBox6.Controls.Add(this.neuLabel4);
            this.neuGroupBox6.Controls.Add(this.txtMriNumb);
            this.neuGroupBox6.Controls.Add(this.label94);
            this.neuGroupBox6.Controls.Add(this.txtXNumb);
            this.neuGroupBox6.Controls.Add(this.label95);
            this.neuGroupBox6.Controls.Add(this.txtBC);
            this.neuGroupBox6.Controls.Add(this.neuLabel1);
            this.neuGroupBox6.Location = new System.Drawing.Point(8, 614);
            this.neuGroupBox6.Name = "neuGroupBox6";
            this.neuGroupBox6.Size = new System.Drawing.Size(771, 170);
            this.neuGroupBox6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox6.TabIndex = 506;
            this.neuGroupBox6.TabStop = false;
            this.neuGroupBox6.Tag = "";
            this.neuGroupBox6.Text = "（6）其他信息";
            // 
            // neuLabel15
            // 
            this.neuLabel15.AutoSize = true;
            this.neuLabel15.Location = new System.Drawing.Point(429, 75);
            this.neuLabel15.Name = "neuLabel15";
            this.neuLabel15.Size = new System.Drawing.Size(53, 12);
            this.neuLabel15.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel15.TabIndex = 668;
            this.neuLabel15.Text = "输液反应";
            this.neuLabel15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtReactionTransfuse
            // 
            this.txtReactionTransfuse.EnterVisiable = true;
            this.txtReactionTransfuse.IsFind = true;
            this.txtReactionTransfuse.IsSelctNone = true;
            this.txtReactionTransfuse.IsSendToNext = false;
            this.txtReactionTransfuse.IsShowID = false;
            this.txtReactionTransfuse.ItemText = "";
            this.txtReactionTransfuse.ListBoxHeight = 100;
            this.txtReactionTransfuse.ListBoxVisible = false;
            this.txtReactionTransfuse.ListBoxWidth = 100;
            this.txtReactionTransfuse.Location = new System.Drawing.Point(495, 69);
            this.txtReactionTransfuse.MaxLength = 5;
            this.txtReactionTransfuse.Name = "txtReactionTransfuse";
            this.txtReactionTransfuse.OmitFilter = true;
            this.txtReactionTransfuse.SelectedItem = null;
            this.txtReactionTransfuse.SelectNone = true;
            this.txtReactionTransfuse.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtReactionTransfuse.ShowID = true;
            this.txtReactionTransfuse.Size = new System.Drawing.Size(87, 21);
            this.txtReactionTransfuse.TabIndex = 669;
            // 
            // textBox13
            // 
            this.textBox13.BackColor = System.Drawing.Color.White;
            this.textBox13.IsEnter2Tab = false;
            this.textBox13.Location = new System.Drawing.Point(124, 142);
            this.textBox13.MaxLength = 3;
            this.textBox13.Name = "textBox13";
            this.textBox13.ReadOnly = true;
            this.textBox13.Size = new System.Drawing.Size(32, 21);
            this.textBox13.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.textBox13.TabIndex = 660;
            this.textBox13.Text = "日";
            // 
            // textBox12
            // 
            this.textBox12.BackColor = System.Drawing.Color.White;
            this.textBox12.IsEnter2Tab = false;
            this.textBox12.Location = new System.Drawing.Point(116, 95);
            this.textBox12.MaxLength = 3;
            this.textBox12.Name = "textBox12";
            this.textBox12.ReadOnly = true;
            this.textBox12.Size = new System.Drawing.Size(32, 21);
            this.textBox12.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.textBox12.TabIndex = 662;
            this.textBox12.Text = "单位";
            // 
            // textBox11
            // 
            this.textBox11.BackColor = System.Drawing.Color.White;
            this.textBox11.IsEnter2Tab = false;
            this.textBox11.Location = new System.Drawing.Point(116, 119);
            this.textBox11.MaxLength = 3;
            this.textBox11.Name = "textBox11";
            this.textBox11.ReadOnly = true;
            this.textBox11.Size = new System.Drawing.Size(32, 21);
            this.textBox11.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.textBox11.TabIndex = 661;
            this.textBox11.Text = "次";
            // 
            // textBox10
            // 
            this.textBox10.BackColor = System.Drawing.Color.White;
            this.textBox10.IsEnter2Tab = false;
            this.textBox10.Location = new System.Drawing.Point(735, 119);
            this.textBox10.MaxLength = 3;
            this.textBox10.Name = "textBox10";
            this.textBox10.ReadOnly = true;
            this.textBox10.Size = new System.Drawing.Size(24, 21);
            this.textBox10.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.textBox10.TabIndex = 659;
            this.textBox10.Text = "日";
            // 
            // textBox9
            // 
            this.textBox9.BackColor = System.Drawing.Color.White;
            this.textBox9.IsEnter2Tab = false;
            this.textBox9.Location = new System.Drawing.Point(735, 95);
            this.textBox9.MaxLength = 3;
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            this.textBox9.Size = new System.Drawing.Size(24, 21);
            this.textBox9.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.textBox9.TabIndex = 658;
            this.textBox9.Text = "ml";
            // 
            // textBox8
            // 
            this.textBox8.BackColor = System.Drawing.Color.White;
            this.textBox8.IsEnter2Tab = false;
            this.textBox8.Location = new System.Drawing.Point(559, 119);
            this.textBox8.MaxLength = 3;
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.Size = new System.Drawing.Size(24, 21);
            this.textBox8.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.textBox8.TabIndex = 663;
            this.textBox8.Text = "日";
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.Color.White;
            this.textBox7.IsEnter2Tab = false;
            this.textBox7.Location = new System.Drawing.Point(559, 95);
            this.textBox7.MaxLength = 3;
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(24, 21);
            this.textBox7.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.textBox7.TabIndex = 664;
            this.textBox7.Text = "ml";
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.Color.White;
            this.textBox6.IsEnter2Tab = false;
            this.textBox6.Location = new System.Drawing.Point(423, 143);
            this.textBox6.MaxLength = 3;
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(32, 21);
            this.textBox6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.textBox6.TabIndex = 657;
            this.textBox6.Text = "日";
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.Color.White;
            this.textBox5.IsEnter2Tab = false;
            this.textBox5.Location = new System.Drawing.Point(423, 119);
            this.textBox5.MaxLength = 3;
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(32, 21);
            this.textBox5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.textBox5.TabIndex = 656;
            this.textBox5.Text = "小时";
            this.textBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.Color.White;
            this.textBox4.IsEnter2Tab = false;
            this.textBox4.Location = new System.Drawing.Point(423, 95);
            this.textBox4.MaxLength = 3;
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(32, 21);
            this.textBox4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.textBox4.TabIndex = 655;
            this.textBox4.Text = "ml";
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.White;
            this.textBox3.IsEnter2Tab = false;
            this.textBox3.Location = new System.Drawing.Point(279, 95);
            this.textBox3.MaxLength = 3;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(32, 21);
            this.textBox3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.textBox3.TabIndex = 665;
            this.textBox3.Text = "袋";
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.White;
            this.textBox2.IsEnter2Tab = false;
            this.textBox2.Location = new System.Drawing.Point(279, 119);
            this.textBox2.MaxLength = 3;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(32, 21);
            this.textBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.textBox2.TabIndex = 666;
            this.textBox2.Text = "次";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.IsEnter2Tab = false;
            this.textBox1.Location = new System.Drawing.Point(279, 143);
            this.textBox1.MaxLength = 3;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(32, 21);
            this.textBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.textBox1.TabIndex = 667;
            this.textBox1.Text = "小时";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cbYnFirst
            // 
            this.cbYnFirst.Location = new System.Drawing.Point(91, 45);
            this.cbYnFirst.Name = "cbYnFirst";
            this.cbYnFirst.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbYnFirst.Size = new System.Drawing.Size(248, 24);
            this.cbYnFirst.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cbYnFirst.TabIndex = 632;
            this.cbYnFirst.Text = "手术、治疗、检查、诊断、是否本院首例";
            this.cbYnFirst.KeyDown += new System.Windows.Forms.KeyEventHandler(this.YnFirst_KeyDown);
            // 
            // txtPathNumb
            // 
            this.txtPathNumb.IsEnter2Tab = false;
            this.txtPathNumb.Location = new System.Drawing.Point(176, 18);
            this.txtPathNumb.MaxLength = 10;
            this.txtPathNumb.Name = "txtPathNumb";
            this.txtPathNumb.Size = new System.Drawing.Size(54, 21);
            this.txtPathNumb.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtPathNumb.TabIndex = 630;
            this.txtPathNumb.Visible = false;
            this.txtPathNumb.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox54_KeyDown);
            // 
            // txtCtNumb
            // 
            this.txtCtNumb.IsEnter2Tab = false;
            this.txtCtNumb.Location = new System.Drawing.Point(62, 18);
            this.txtCtNumb.MaxLength = 10;
            this.txtCtNumb.Name = "txtCtNumb";
            this.txtCtNumb.Size = new System.Drawing.Size(54, 21);
            this.txtCtNumb.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCtNumb.TabIndex = 629;
            this.txtCtNumb.Visible = false;
            this.txtCtNumb.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CtNumb_KeyDown);
            // 
            // label92
            // 
            this.label92.AutoSize = true;
            this.label92.Location = new System.Drawing.Point(42, 21);
            this.label92.Name = "label92";
            this.label92.Size = new System.Drawing.Size(509, 12);
            this.label92.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label92.TabIndex = 613;
            this.label92.Text = "温馨提示：尸检、手术、治疗、检查、诊断、是否本院首例、随诊、示教病案“勾选”表示是！";
            this.label92.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSPecalNus
            // 
            this.txtSPecalNus.AllowNegative = false;
            this.txtSPecalNus.AutoPadRightZero = false;
            this.txtSPecalNus.Location = new System.Drawing.Point(383, 143);
            this.txtSPecalNus.MaxDigits = 0;
            this.txtSPecalNus.MaxLength = 3;
            this.txtSPecalNus.Name = "txtSPecalNus";
            this.txtSPecalNus.Size = new System.Drawing.Size(40, 21);
            this.txtSPecalNus.TabIndex = 654;
            this.txtSPecalNus.Text = "0";
            this.txtSPecalNus.WillShowError = false;
            this.txtSPecalNus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SPecalNus_KeyDown);
            // 
            // label90
            // 
            this.label90.AutoSize = true;
            this.label90.Location = new System.Drawing.Point(327, 143);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(53, 12);
            this.label90.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label90.TabIndex = 614;
            this.label90.Text = "特殊护理";
            this.label90.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtStrictNuss
            // 
            this.txtStrictNuss.AllowNegative = false;
            this.txtStrictNuss.AutoPadRightZero = false;
            this.txtStrictNuss.Location = new System.Drawing.Point(223, 143);
            this.txtStrictNuss.MaxDigits = 0;
            this.txtStrictNuss.MaxLength = 3;
            this.txtStrictNuss.Name = "txtStrictNuss";
            this.txtStrictNuss.Size = new System.Drawing.Size(56, 21);
            this.txtStrictNuss.TabIndex = 653;
            this.txtStrictNuss.Text = "0";
            this.txtStrictNuss.WillShowError = false;
            this.txtStrictNuss.KeyDown += new System.Windows.Forms.KeyEventHandler(this.StrictNuss_KeyDown);
            // 
            // label91
            // 
            this.label91.AutoSize = true;
            this.label91.Location = new System.Drawing.Point(167, 143);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(53, 12);
            this.label91.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label91.TabIndex = 608;
            this.label91.Text = "重症监护";
            this.label91.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIIINus
            // 
            this.txtIIINus.AllowNegative = false;
            this.txtIIINus.AutoPadRightZero = false;
            this.txtIIINus.Location = new System.Drawing.Point(68, 142);
            this.txtIIINus.MaxDigits = 0;
            this.txtIIINus.MaxLength = 3;
            this.txtIIINus.Name = "txtIIINus";
            this.txtIIINus.Size = new System.Drawing.Size(56, 21);
            this.txtIIINus.TabIndex = 652;
            this.txtIIINus.Text = "0";
            this.txtIIINus.WillShowError = false;
            this.txtIIINus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.IIINus_KeyDown);
            // 
            // txtIINus
            // 
            this.txtIINus.AllowNegative = false;
            this.txtIINus.AutoPadRightZero = false;
            this.txtIINus.Location = new System.Drawing.Point(655, 119);
            this.txtIINus.MaxDigits = 0;
            this.txtIINus.MaxLength = 3;
            this.txtIINus.Name = "txtIINus";
            this.txtIINus.Size = new System.Drawing.Size(80, 21);
            this.txtIINus.TabIndex = 651;
            this.txtIINus.Text = "0";
            this.txtIINus.WillShowError = false;
            this.txtIINus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.IINus_KeyDown);
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.Location = new System.Drawing.Point(591, 123);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(53, 12);
            this.label88.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label88.TabIndex = 619;
            this.label88.Text = "II级护理";
            this.label88.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtINus
            // 
            this.txtINus.AllowNegative = false;
            this.txtINus.AutoPadRightZero = false;
            this.txtINus.Location = new System.Drawing.Point(511, 119);
            this.txtINus.MaxDigits = 0;
            this.txtINus.MaxLength = 3;
            this.txtINus.Name = "txtINus";
            this.txtINus.Size = new System.Drawing.Size(48, 21);
            this.txtINus.TabIndex = 650;
            this.txtINus.Text = "0";
            this.txtINus.WillShowError = false;
            this.txtINus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.INus_KeyDown);
            // 
            // label85
            // 
            this.label85.AutoSize = true;
            this.label85.Location = new System.Drawing.Point(463, 123);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(47, 12);
            this.label85.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label85.TabIndex = 621;
            this.label85.Text = "I级护理";
            this.label85.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSuperNus
            // 
            this.txtSuperNus.AllowNegative = false;
            this.txtSuperNus.AutoPadRightZero = false;
            this.txtSuperNus.Location = new System.Drawing.Point(383, 119);
            this.txtSuperNus.MaxDigits = 0;
            this.txtSuperNus.MaxLength = 3;
            this.txtSuperNus.Name = "txtSuperNus";
            this.txtSuperNus.Size = new System.Drawing.Size(40, 21);
            this.txtSuperNus.TabIndex = 649;
            this.txtSuperNus.Text = "0";
            this.txtSuperNus.WillShowError = false;
            this.txtSuperNus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SuperNus_KeyDown);
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.Location = new System.Drawing.Point(327, 123);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(53, 12);
            this.label82.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label82.TabIndex = 615;
            this.label82.Text = "特级护理";
            this.label82.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtOutconNum
            // 
            this.txtOutconNum.AllowNegative = false;
            this.txtOutconNum.AutoPadRightZero = false;
            this.txtOutconNum.Location = new System.Drawing.Point(223, 119);
            this.txtOutconNum.MaxDigits = 0;
            this.txtOutconNum.MaxLength = 3;
            this.txtOutconNum.Name = "txtOutconNum";
            this.txtOutconNum.Size = new System.Drawing.Size(56, 21);
            this.txtOutconNum.TabIndex = 648;
            this.txtOutconNum.Text = "0";
            this.txtOutconNum.WillShowError = false;
            this.txtOutconNum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.outOutconNum_KeyDown);
            // 
            // label83
            // 
            this.label83.AutoSize = true;
            this.label83.Location = new System.Drawing.Point(167, 123);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(53, 12);
            this.label83.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label83.TabIndex = 627;
            this.label83.Text = "远程会诊";
            this.label83.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtInconNum
            // 
            this.txtInconNum.AllowNegative = false;
            this.txtInconNum.AutoPadRightZero = false;
            this.txtInconNum.Location = new System.Drawing.Point(60, 119);
            this.txtInconNum.MaxDigits = 0;
            this.txtInconNum.MaxLength = 3;
            this.txtInconNum.Name = "txtInconNum";
            this.txtInconNum.Size = new System.Drawing.Size(56, 21);
            this.txtInconNum.TabIndex = 647;
            this.txtInconNum.Text = "0";
            this.txtInconNum.WillShowError = false;
            this.txtInconNum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InconNum_KeyDown);
            // 
            // txtBloodOther
            // 
            this.txtBloodOther.AllowNegative = false;
            this.txtBloodOther.AutoPadRightZero = false;
            this.txtBloodOther.Location = new System.Drawing.Point(655, 95);
            this.txtBloodOther.MaxDigits = 0;
            this.txtBloodOther.MaxLength = 10;
            this.txtBloodOther.Name = "txtBloodOther";
            this.txtBloodOther.Size = new System.Drawing.Size(80, 21);
            this.txtBloodOther.TabIndex = 646;
            this.txtBloodOther.Text = "0";
            this.txtBloodOther.WillShowError = false;
            this.txtBloodOther.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BloodOther_KeyDown);
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.Location = new System.Drawing.Point(591, 99);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(53, 12);
            this.label81.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label81.TabIndex = 612;
            this.label81.Text = "其    他";
            this.label81.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBloodWhole
            // 
            this.txtBloodWhole.AllowNegative = false;
            this.txtBloodWhole.AutoPadRightZero = false;
            this.txtBloodWhole.Location = new System.Drawing.Point(511, 95);
            this.txtBloodWhole.MaxDigits = 0;
            this.txtBloodWhole.MaxLength = 10;
            this.txtBloodWhole.Name = "txtBloodWhole";
            this.txtBloodWhole.Size = new System.Drawing.Size(48, 21);
            this.txtBloodWhole.TabIndex = 645;
            this.txtBloodWhole.Text = "0";
            this.txtBloodWhole.WillShowError = false;
            this.txtBloodWhole.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BloodWhole_KeyDown);
            // 
            // label80
            // 
            this.label80.AutoSize = true;
            this.label80.Location = new System.Drawing.Point(463, 99);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(47, 12);
            this.label80.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label80.TabIndex = 617;
            this.label80.Text = "全   血";
            this.label80.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBodyAnotomize
            // 
            this.txtBodyAnotomize.AllowNegative = false;
            this.txtBodyAnotomize.AutoPadRightZero = false;
            this.txtBodyAnotomize.Location = new System.Drawing.Point(383, 95);
            this.txtBodyAnotomize.MaxDigits = 0;
            this.txtBodyAnotomize.MaxLength = 10;
            this.txtBodyAnotomize.Name = "txtBodyAnotomize";
            this.txtBodyAnotomize.Size = new System.Drawing.Size(40, 21);
            this.txtBodyAnotomize.TabIndex = 644;
            this.txtBodyAnotomize.Text = "0";
            this.txtBodyAnotomize.WillShowError = false;
            this.txtBodyAnotomize.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BodyAnotomize_KeyDown);
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.Location = new System.Drawing.Point(327, 98);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(47, 12);
            this.label78.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label78.TabIndex = 616;
            this.label78.Text = "血   浆";
            this.label78.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBloodPlatelet
            // 
            this.txtBloodPlatelet.AllowNegative = false;
            this.txtBloodPlatelet.AutoPadRightZero = false;
            this.txtBloodPlatelet.Location = new System.Drawing.Point(223, 95);
            this.txtBloodPlatelet.MaxDigits = 0;
            this.txtBloodPlatelet.MaxLength = 10;
            this.txtBloodPlatelet.Name = "txtBloodPlatelet";
            this.txtBloodPlatelet.Size = new System.Drawing.Size(56, 21);
            this.txtBloodPlatelet.TabIndex = 643;
            this.txtBloodPlatelet.Text = "0";
            this.txtBloodPlatelet.WillShowError = false;
            this.txtBloodPlatelet.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BloodPlatelet_KeyDown);
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.Location = new System.Drawing.Point(167, 100);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(53, 12);
            this.label77.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label77.TabIndex = 618;
            this.label77.Text = "血 小 板";
            this.label77.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBloodRed
            // 
            this.txtBloodRed.AllowNegative = false;
            this.txtBloodRed.AutoPadRightZero = false;
            this.txtBloodRed.Location = new System.Drawing.Point(60, 95);
            this.txtBloodRed.MaxDigits = 2;
            this.txtBloodRed.MaxLength = 10;
            this.txtBloodRed.Name = "txtBloodRed";
            this.txtBloodRed.Size = new System.Drawing.Size(56, 21);
            this.txtBloodRed.TabIndex = 642;
            this.txtBloodRed.Text = "0";
            this.txtBloodRed.WillShowError = false;
            this.txtBloodRed.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BloodRed_KeyDown);
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.Location = new System.Drawing.Point(4, 100);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(53, 12);
            this.label76.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label76.TabIndex = 620;
            this.label76.Text = "红 细 胞";
            this.label76.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // neuLabel12
            // 
            this.neuLabel12.AutoSize = true;
            this.neuLabel12.Location = new System.Drawing.Point(593, 148);
            this.neuLabel12.Name = "neuLabel12";
            this.neuLabel12.Size = new System.Drawing.Size(53, 12);
            this.neuLabel12.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel12.TabIndex = 610;
            this.neuLabel12.Text = "四病报告";
            this.neuLabel12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.neuLabel12.Visible = false;
            // 
            // neuLabel11
            // 
            this.neuLabel11.AutoSize = true;
            this.neuLabel11.Location = new System.Drawing.Point(463, 148);
            this.neuLabel11.Name = "neuLabel11";
            this.neuLabel11.Size = new System.Drawing.Size(65, 12);
            this.neuLabel11.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel11.TabIndex = 610;
            this.neuLabel11.Text = "传染病报告";
            this.neuLabel11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label75
            // 
            this.label75.AutoSize = true;
            this.label75.Location = new System.Drawing.Point(591, 75);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(53, 12);
            this.label75.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label75.TabIndex = 610;
            this.label75.Text = "输血反应";
            this.label75.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label74
            // 
            this.label74.Location = new System.Drawing.Point(315, 69);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(32, 23);
            this.label74.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label74.TabIndex = 609;
            this.label74.Text = "Rh";
            this.label74.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Location = new System.Drawing.Point(167, 75);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(53, 12);
            this.label73.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label73.TabIndex = 611;
            this.label73.Text = "血    型";
            this.label73.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbDisease30
            // 
            this.cbDisease30.Location = new System.Drawing.Point(14, 69);
            this.cbDisease30.Name = "cbDisease30";
            this.cbDisease30.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbDisease30.Size = new System.Drawing.Size(64, 24);
            this.cbDisease30.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cbDisease30.TabIndex = 638;
            this.cbDisease30.Text = "单病种";
            this.cbDisease30.KeyDown += new System.Windows.Forms.KeyEventHandler(this.checkBox8_KeyDown);
            // 
            // cbTechSerc
            // 
            this.cbTechSerc.Location = new System.Drawing.Point(671, 45);
            this.cbTechSerc.Name = "cbTechSerc";
            this.cbTechSerc.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbTechSerc.Size = new System.Drawing.Size(80, 24);
            this.cbTechSerc.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cbTechSerc.TabIndex = 637;
            this.cbTechSerc.Text = "示教病案";
            this.cbTechSerc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TechSerc_KeyDown);
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Location = new System.Drawing.Point(647, 50);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(17, 12);
            this.label72.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label72.TabIndex = 622;
            this.label72.Text = "年";
            this.label72.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Location = new System.Drawing.Point(583, 50);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(17, 12);
            this.label71.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label71.TabIndex = 623;
            this.label71.Text = "月";
            this.label71.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Location = new System.Drawing.Point(527, 50);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(17, 12);
            this.label70.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label70.TabIndex = 624;
            this.label70.Text = "周";
            this.label70.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtVisiPeriYear
            // 
            this.txtVisiPeriYear.AllowNegative = false;
            this.txtVisiPeriYear.AutoPadRightZero = false;
            this.txtVisiPeriYear.Location = new System.Drawing.Point(607, 45);
            this.txtVisiPeriYear.MaxDigits = 0;
            this.txtVisiPeriYear.MaxLength = 3;
            this.txtVisiPeriYear.Name = "txtVisiPeriYear";
            this.txtVisiPeriYear.Size = new System.Drawing.Size(32, 21);
            this.txtVisiPeriYear.TabIndex = 636;
            this.txtVisiPeriYear.Text = "0";
            this.txtVisiPeriYear.WillShowError = false;
            this.txtVisiPeriYear.KeyDown += new System.Windows.Forms.KeyEventHandler(this.VisiPeriYear_KeyDown);
            // 
            // txtVisiPeriMonth
            // 
            this.txtVisiPeriMonth.AllowNegative = false;
            this.txtVisiPeriMonth.AutoPadRightZero = false;
            this.txtVisiPeriMonth.Location = new System.Drawing.Point(551, 45);
            this.txtVisiPeriMonth.MaxDigits = 0;
            this.txtVisiPeriMonth.MaxLength = 3;
            this.txtVisiPeriMonth.Name = "txtVisiPeriMonth";
            this.txtVisiPeriMonth.Size = new System.Drawing.Size(32, 21);
            this.txtVisiPeriMonth.TabIndex = 635;
            this.txtVisiPeriMonth.Text = "0";
            this.txtVisiPeriMonth.WillShowError = false;
            this.txtVisiPeriMonth.KeyDown += new System.Windows.Forms.KeyEventHandler(this.VisiPeriMonth_KeyDown);
            // 
            // txtVisiPeriWeek
            // 
            this.txtVisiPeriWeek.AllowNegative = false;
            this.txtVisiPeriWeek.AutoPadRightZero = false;
            this.txtVisiPeriWeek.Location = new System.Drawing.Point(495, 45);
            this.txtVisiPeriWeek.MaxDigits = 0;
            this.txtVisiPeriWeek.MaxLength = 3;
            this.txtVisiPeriWeek.Name = "txtVisiPeriWeek";
            this.txtVisiPeriWeek.Size = new System.Drawing.Size(32, 21);
            this.txtVisiPeriWeek.TabIndex = 634;
            this.txtVisiPeriWeek.Text = "0";
            this.txtVisiPeriWeek.WillShowError = false;
            this.txtVisiPeriWeek.KeyDown += new System.Windows.Forms.KeyEventHandler(this.VisiPeriWeek_KeyDown);
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Location = new System.Drawing.Point(431, 50);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(53, 12);
            this.label69.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label69.TabIndex = 625;
            this.label69.Text = "随诊期限";
            this.label69.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbVisiStat
            // 
            this.cbVisiStat.AutoSize = true;
            this.cbVisiStat.Location = new System.Drawing.Point(356, 49);
            this.cbVisiStat.Name = "cbVisiStat";
            this.cbVisiStat.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbVisiStat.Size = new System.Drawing.Size(48, 16);
            this.cbVisiStat.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cbVisiStat.TabIndex = 633;
            this.cbVisiStat.Text = "随诊";
            this.cbVisiStat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.VisiStat_KeyDown);
            // 
            // cbBodyCheck
            // 
            this.cbBodyCheck.Location = new System.Drawing.Point(14, 45);
            this.cbBodyCheck.Name = "cbBodyCheck";
            this.cbBodyCheck.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbBodyCheck.Size = new System.Drawing.Size(64, 24);
            this.cbBodyCheck.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cbBodyCheck.TabIndex = 631;
            this.cbBodyCheck.Text = "尸  检";
            this.cbBodyCheck.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BodyCheck_KeyDown);
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.Location = new System.Drawing.Point(5, 146);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(59, 12);
            this.label87.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label87.TabIndex = 626;
            this.label87.Text = "III级护理";
            this.label87.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.Location = new System.Drawing.Point(5, 123);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(53, 12);
            this.label84.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label84.TabIndex = 628;
            this.label84.Text = "院际会诊";
            this.label84.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFourDiseasesReport
            // 
            this.txtFourDiseasesReport.EnterVisiable = true;
            this.txtFourDiseasesReport.IsFind = true;
            this.txtFourDiseasesReport.IsSelctNone = true;
            this.txtFourDiseasesReport.IsSendToNext = false;
            this.txtFourDiseasesReport.IsShowID = false;
            this.txtFourDiseasesReport.ItemText = "";
            this.txtFourDiseasesReport.ListBoxHeight = 100;
            this.txtFourDiseasesReport.ListBoxVisible = false;
            this.txtFourDiseasesReport.ListBoxWidth = 100;
            this.txtFourDiseasesReport.Location = new System.Drawing.Point(654, 143);
            this.txtFourDiseasesReport.MaxLength = 5;
            this.txtFourDiseasesReport.Name = "txtFourDiseasesReport";
            this.txtFourDiseasesReport.OmitFilter = true;
            this.txtFourDiseasesReport.SelectedItem = null;
            this.txtFourDiseasesReport.SelectNone = true;
            this.txtFourDiseasesReport.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtFourDiseasesReport.ShowID = true;
            this.txtFourDiseasesReport.Size = new System.Drawing.Size(105, 21);
            this.txtFourDiseasesReport.TabIndex = 641;
            this.txtFourDiseasesReport.Visible = false;
            this.txtFourDiseasesReport.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFourDiseasesReport_KeyDown);
            // 
            // txtInfectionDiseasesReport
            // 
            this.txtInfectionDiseasesReport.EnterVisiable = true;
            this.txtInfectionDiseasesReport.IsFind = true;
            this.txtInfectionDiseasesReport.IsSelctNone = true;
            this.txtInfectionDiseasesReport.IsSendToNext = false;
            this.txtInfectionDiseasesReport.IsShowID = false;
            this.txtInfectionDiseasesReport.ItemText = "";
            this.txtInfectionDiseasesReport.ListBoxHeight = 100;
            this.txtInfectionDiseasesReport.ListBoxVisible = false;
            this.txtInfectionDiseasesReport.ListBoxWidth = 100;
            this.txtInfectionDiseasesReport.Location = new System.Drawing.Point(533, 143);
            this.txtInfectionDiseasesReport.MaxLength = 5;
            this.txtInfectionDiseasesReport.Name = "txtInfectionDiseasesReport";
            this.txtInfectionDiseasesReport.OmitFilter = true;
            this.txtInfectionDiseasesReport.SelectedItem = null;
            this.txtInfectionDiseasesReport.SelectNone = true;
            this.txtInfectionDiseasesReport.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtInfectionDiseasesReport.ShowID = true;
            this.txtInfectionDiseasesReport.Size = new System.Drawing.Size(51, 21);
            this.txtInfectionDiseasesReport.TabIndex = 641;
            this.txtInfectionDiseasesReport.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInfectionDiseasesReport_KeyDown);
            // 
            // txtReactionBlood
            // 
            this.txtReactionBlood.EnterVisiable = true;
            this.txtReactionBlood.IsFind = true;
            this.txtReactionBlood.IsSelctNone = true;
            this.txtReactionBlood.IsSendToNext = false;
            this.txtReactionBlood.IsShowID = false;
            this.txtReactionBlood.ItemText = "";
            this.txtReactionBlood.ListBoxHeight = 100;
            this.txtReactionBlood.ListBoxVisible = false;
            this.txtReactionBlood.ListBoxWidth = 100;
            this.txtReactionBlood.Location = new System.Drawing.Point(655, 70);
            this.txtReactionBlood.MaxLength = 5;
            this.txtReactionBlood.Name = "txtReactionBlood";
            this.txtReactionBlood.OmitFilter = true;
            this.txtReactionBlood.SelectedItem = null;
            this.txtReactionBlood.SelectNone = true;
            this.txtReactionBlood.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtReactionBlood.ShowID = true;
            this.txtReactionBlood.Size = new System.Drawing.Size(104, 21);
            this.txtReactionBlood.TabIndex = 641;
            this.txtReactionBlood.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ReactionBlood_KeyDown);
            // 
            // txtRhBlood
            // 
            this.txtRhBlood.EnterVisiable = true;
            this.txtRhBlood.IsFind = true;
            this.txtRhBlood.IsSelctNone = true;
            this.txtRhBlood.IsSendToNext = false;
            this.txtRhBlood.IsShowID = false;
            this.txtRhBlood.ItemText = "";
            this.txtRhBlood.ListBoxHeight = 100;
            this.txtRhBlood.ListBoxVisible = false;
            this.txtRhBlood.ListBoxWidth = 100;
            this.txtRhBlood.Location = new System.Drawing.Point(351, 69);
            this.txtRhBlood.Name = "txtRhBlood";
            this.txtRhBlood.OmitFilter = true;
            this.txtRhBlood.SelectedItem = null;
            this.txtRhBlood.SelectNone = true;
            this.txtRhBlood.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRhBlood.ShowID = true;
            this.txtRhBlood.Size = new System.Drawing.Size(72, 21);
            this.txtRhBlood.TabIndex = 640;
            this.txtRhBlood.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RhBlood_KeyDown);
            // 
            // txtBloodType
            // 
            this.txtBloodType.EnterVisiable = true;
            this.txtBloodType.IsFind = true;
            this.txtBloodType.IsSelctNone = true;
            this.txtBloodType.IsSendToNext = false;
            this.txtBloodType.IsShowID = false;
            this.txtBloodType.ItemText = "";
            this.txtBloodType.ListBoxHeight = 100;
            this.txtBloodType.ListBoxVisible = false;
            this.txtBloodType.ListBoxWidth = 100;
            this.txtBloodType.Location = new System.Drawing.Point(223, 69);
            this.txtBloodType.MaxLength = 3;
            this.txtBloodType.Name = "txtBloodType";
            this.txtBloodType.OmitFilter = true;
            this.txtBloodType.SelectedItem = null;
            this.txtBloodType.SelectNone = true;
            this.txtBloodType.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBloodType.ShowID = true;
            this.txtBloodType.Size = new System.Drawing.Size(72, 21);
            this.txtBloodType.TabIndex = 639;
            this.txtBloodType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BloodType_KeyDown);
            // 
            // txtPETNumb
            // 
            this.txtPETNumb.IsEnter2Tab = false;
            this.txtPETNumb.Location = new System.Drawing.Point(705, 21);
            this.txtPETNumb.Name = "txtPETNumb";
            this.txtPETNumb.Size = new System.Drawing.Size(54, 21);
            this.txtPETNumb.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtPETNumb.TabIndex = 73;
            this.txtPETNumb.Visible = false;
            this.txtPETNumb.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPETNumb_KeyDown);
            // 
            // neuLabel7
            // 
            this.neuLabel7.AutoSize = true;
            this.neuLabel7.Location = new System.Drawing.Point(677, 26);
            this.neuLabel7.Name = "neuLabel7";
            this.neuLabel7.Size = new System.Drawing.Size(23, 12);
            this.neuLabel7.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel7.TabIndex = 0;
            this.neuLabel7.Text = "PET";
            this.neuLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.neuLabel7.Visible = false;
            // 
            // txtECTNumb
            // 
            this.txtECTNumb.IsEnter2Tab = false;
            this.txtECTNumb.Location = new System.Drawing.Point(603, 21);
            this.txtECTNumb.Name = "txtECTNumb";
            this.txtECTNumb.Size = new System.Drawing.Size(54, 21);
            this.txtECTNumb.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtECTNumb.TabIndex = 72;
            this.txtECTNumb.Visible = false;
            this.txtECTNumb.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtECTNumb_KeyDown);
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.Location = new System.Drawing.Point(578, 25);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(23, 12);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 0;
            this.neuLabel6.Text = "ECT";
            this.neuLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.neuLabel6.Visible = false;
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point(133, 22);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(41, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 0;
            this.neuLabel4.Text = "病理号";
            this.neuLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.neuLabel4.Visible = false;
            // 
            // txtMriNumb
            // 
            this.txtMriNumb.IsEnter2Tab = false;
            this.txtMriNumb.Location = new System.Drawing.Point(280, 18);
            this.txtMriNumb.MaxLength = 10;
            this.txtMriNumb.Name = "txtMriNumb";
            this.txtMriNumb.Size = new System.Drawing.Size(54, 21);
            this.txtMriNumb.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtMriNumb.TabIndex = 69;
            this.txtMriNumb.Visible = false;
            this.txtMriNumb.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MriNumb_KeyDown);
            // 
            // label94
            // 
            this.label94.AutoSize = true;
            this.label94.Location = new System.Drawing.Point(260, 21);
            this.label94.Name = "label94";
            this.label94.Size = new System.Drawing.Size(17, 12);
            this.label94.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label94.TabIndex = 0;
            this.label94.Text = "MR";
            this.label94.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label94.Visible = false;
            // 
            // txtXNumb
            // 
            this.txtXNumb.IsEnter2Tab = false;
            this.txtXNumb.Location = new System.Drawing.Point(388, 18);
            this.txtXNumb.MaxLength = 10;
            this.txtXNumb.Name = "txtXNumb";
            this.txtXNumb.Size = new System.Drawing.Size(54, 21);
            this.txtXNumb.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtXNumb.TabIndex = 70;
            this.txtXNumb.Visible = false;
            this.txtXNumb.KeyDown += new System.Windows.Forms.KeyEventHandler(this.XNumb_KeyDown);
            // 
            // label95
            // 
            this.label95.AutoSize = true;
            this.label95.Location = new System.Drawing.Point(360, 21);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(23, 12);
            this.label95.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label95.TabIndex = 0;
            this.label95.Text = "X光";
            this.label95.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label95.Visible = false;
            // 
            // txtBC
            // 
            this.txtBC.IsEnter2Tab = false;
            this.txtBC.Location = new System.Drawing.Point(495, 20);
            this.txtBC.Name = "txtBC";
            this.txtBC.Size = new System.Drawing.Size(54, 21);
            this.txtBC.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtBC.TabIndex = 71;
            this.txtBC.Visible = false;
            this.txtBC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBC_KeyDown);
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(466, 23);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(23, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "B超";
            this.neuLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.neuLabel1.Visible = false;
            // 
            // neuGroupBox7
            // 
            this.neuGroupBox7.Controls.Add(this.txtOperationCode);
            this.neuGroupBox7.Controls.Add(this.txtCodingCode);
            this.neuGroupBox7.Controls.Add(this.label45);
            this.neuGroupBox7.Controls.Add(this.label96);
            this.neuGroupBox7.Controls.Add(this.txtCheckDate);
            this.neuGroupBox7.Controls.Add(this.label65);
            this.neuGroupBox7.Controls.Add(this.label59);
            this.neuGroupBox7.Controls.Add(this.label58);
            this.neuGroupBox7.Controls.Add(this.label57);
            this.neuGroupBox7.Controls.Add(this.label66);
            this.neuGroupBox7.Controls.Add(this.label67);
            this.neuGroupBox7.Controls.Add(this.txtQcDocd);
            this.neuGroupBox7.Controls.Add(this.txtInputDoc);
            this.neuGroupBox7.Controls.Add(this.txtCoordinate);
            this.neuGroupBox7.Controls.Add(this.txtQcNucd);
            this.neuGroupBox7.Controls.Add(this.txtMrQual);
            this.neuGroupBox7.Location = new System.Drawing.Point(8, 790);
            this.neuGroupBox7.Name = "neuGroupBox7";
            this.neuGroupBox7.Size = new System.Drawing.Size(771, 73);
            this.neuGroupBox7.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox7.TabIndex = 507;
            this.neuGroupBox7.TabStop = false;
            this.neuGroupBox7.Text = "（7）病案质控信息";
            // 
            // txtOperationCode
            // 
            this.txtOperationCode.EnterVisiable = true;
            this.txtOperationCode.IsFind = true;
            this.txtOperationCode.IsSelctNone = true;
            this.txtOperationCode.IsSendToNext = false;
            this.txtOperationCode.IsShowID = false;
            this.txtOperationCode.ItemText = "";
            this.txtOperationCode.ListBoxHeight = 115;
            this.txtOperationCode.ListBoxVisible = false;
            this.txtOperationCode.ListBoxWidth = 130;
            this.txtOperationCode.Location = new System.Drawing.Point(372, 44);
            this.txtOperationCode.MaxLength = 16;
            this.txtOperationCode.Name = "txtOperationCode";
            this.txtOperationCode.OmitFilter = true;
            this.txtOperationCode.SelectedItem = null;
            this.txtOperationCode.SelectNone = true;
            this.txtOperationCode.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtOperationCode.ShowID = true;
            this.txtOperationCode.Size = new System.Drawing.Size(217, 21);
            this.txtOperationCode.TabIndex = 120;
            this.txtOperationCode.Visible = false;
            this.txtOperationCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OperationCOde_KeyDown);
            // 
            // txtCodingCode
            // 
            this.txtCodingCode.EnterVisiable = true;
            this.txtCodingCode.IsFind = true;
            this.txtCodingCode.IsSelctNone = true;
            this.txtCodingCode.IsSendToNext = false;
            this.txtCodingCode.IsShowID = false;
            this.txtCodingCode.ItemText = "";
            this.txtCodingCode.ListBoxHeight = 115;
            this.txtCodingCode.ListBoxVisible = false;
            this.txtCodingCode.ListBoxWidth = 130;
            this.txtCodingCode.Location = new System.Drawing.Point(683, 20);
            this.txtCodingCode.MaxLength = 16;
            this.txtCodingCode.Name = "txtCodingCode";
            this.txtCodingCode.OmitFilter = true;
            this.txtCodingCode.SelectedItem = null;
            this.txtCodingCode.SelectNone = true;
            this.txtCodingCode.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCodingCode.ShowID = true;
            this.txtCodingCode.Size = new System.Drawing.Size(73, 21);
            this.txtCodingCode.TabIndex = 118;
            this.txtCodingCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CodingCode_KeyDown);
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(308, 48);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(65, 12);
            this.label45.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label45.TabIndex = 112;
            this.label45.Text = "手术编码员";
            this.label45.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label45.Visible = false;
            // 
            // label96
            // 
            this.label96.AutoSize = true;
            this.label96.Location = new System.Drawing.Point(598, 48);
            this.label96.Name = "label96";
            this.label96.Size = new System.Drawing.Size(53, 12);
            this.label96.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label96.TabIndex = 111;
            this.label96.Text = "输 入 员";
            this.label96.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label96.Visible = false;
            // 
            // txtCheckDate
            // 
            this.txtCheckDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtCheckDate.IsEnter2Tab = false;
            this.txtCheckDate.Location = new System.Drawing.Point(62, 20);
            this.txtCheckDate.Name = "txtCheckDate";
            this.txtCheckDate.Size = new System.Drawing.Size(88, 21);
            this.txtCheckDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCheckDate.TabIndex = 114;
            this.txtCheckDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CheckDate_KeyDown);
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Location = new System.Drawing.Point(6, 24);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(53, 12);
            this.label65.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label65.TabIndex = 113;
            this.label65.Text = "质控日期";
            this.label65.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(462, 24);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(53, 12);
            this.label59.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label59.TabIndex = 110;
            this.label59.Text = "质控护士";
            this.label59.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(314, 24);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(53, 12);
            this.label58.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label58.TabIndex = 107;
            this.label58.Text = "质控医师";
            this.label58.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(158, 24);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(53, 12);
            this.label57.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label57.TabIndex = 106;
            this.label57.Text = "病历质量";
            this.label57.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Location = new System.Drawing.Point(609, 24);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(65, 12);
            this.label66.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label66.TabIndex = 109;
            this.label66.Text = "诊断编码员";
            this.label66.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Location = new System.Drawing.Point(158, 48);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(53, 12);
            this.label67.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label67.TabIndex = 108;
            this.label67.Text = "整 理 员";
            this.label67.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label67.Visible = false;
            // 
            // txtQcDocd
            // 
            this.txtQcDocd.EnterVisiable = true;
            this.txtQcDocd.IsFind = true;
            this.txtQcDocd.IsSelctNone = true;
            this.txtQcDocd.IsSendToNext = false;
            this.txtQcDocd.IsShowID = false;
            this.txtQcDocd.ItemText = "";
            this.txtQcDocd.ListBoxHeight = 115;
            this.txtQcDocd.ListBoxVisible = false;
            this.txtQcDocd.ListBoxWidth = 130;
            this.txtQcDocd.Location = new System.Drawing.Point(372, 20);
            this.txtQcDocd.Name = "txtQcDocd";
            this.txtQcDocd.OmitFilter = true;
            this.txtQcDocd.SelectedItem = null;
            this.txtQcDocd.SelectNone = true;
            this.txtQcDocd.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtQcDocd.ShowID = true;
            this.txtQcDocd.Size = new System.Drawing.Size(73, 21);
            this.txtQcDocd.TabIndex = 116;
            this.txtQcDocd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.QcDocd_KeyDown);
            // 
            // txtInputDoc
            // 
            this.txtInputDoc.EnterVisiable = true;
            this.txtInputDoc.IsFind = true;
            this.txtInputDoc.IsSelctNone = true;
            this.txtInputDoc.IsSendToNext = false;
            this.txtInputDoc.IsShowID = false;
            this.txtInputDoc.ItemText = "";
            this.txtInputDoc.ListBoxHeight = 115;
            this.txtInputDoc.ListBoxVisible = false;
            this.txtInputDoc.ListBoxWidth = 100;
            this.txtInputDoc.Location = new System.Drawing.Point(654, 44);
            this.txtInputDoc.Name = "txtInputDoc";
            this.txtInputDoc.OmitFilter = true;
            this.txtInputDoc.SelectedItem = null;
            this.txtInputDoc.SelectNone = true;
            this.txtInputDoc.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtInputDoc.ShowID = true;
            this.txtInputDoc.Size = new System.Drawing.Size(104, 21);
            this.txtInputDoc.TabIndex = 121;
            this.txtInputDoc.Visible = false;
            // 
            // txtCoordinate
            // 
            this.txtCoordinate.EnterVisiable = true;
            this.txtCoordinate.IsFind = true;
            this.txtCoordinate.IsSelctNone = true;
            this.txtCoordinate.IsSendToNext = false;
            this.txtCoordinate.IsShowID = false;
            this.txtCoordinate.ItemText = "";
            this.txtCoordinate.ListBoxHeight = 115;
            this.txtCoordinate.ListBoxVisible = false;
            this.txtCoordinate.ListBoxWidth = 130;
            this.txtCoordinate.Location = new System.Drawing.Point(222, 44);
            this.txtCoordinate.MaxLength = 16;
            this.txtCoordinate.Name = "txtCoordinate";
            this.txtCoordinate.OmitFilter = true;
            this.txtCoordinate.SelectedItem = null;
            this.txtCoordinate.SelectNone = true;
            this.txtCoordinate.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCoordinate.ShowID = true;
            this.txtCoordinate.Size = new System.Drawing.Size(86, 21);
            this.txtCoordinate.TabIndex = 119;
            this.txtCoordinate.Visible = false;
            this.txtCoordinate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox33_KeyDown);
            // 
            // txtQcNucd
            // 
            this.txtQcNucd.EnterVisiable = true;
            this.txtQcNucd.IsFind = true;
            this.txtQcNucd.IsSelctNone = true;
            this.txtQcNucd.IsSendToNext = false;
            this.txtQcNucd.IsShowID = false;
            this.txtQcNucd.ItemText = "";
            this.txtQcNucd.ListBoxHeight = 115;
            this.txtQcNucd.ListBoxVisible = false;
            this.txtQcNucd.ListBoxWidth = 130;
            this.txtQcNucd.Location = new System.Drawing.Point(522, 20);
            this.txtQcNucd.MaxLength = 16;
            this.txtQcNucd.Name = "txtQcNucd";
            this.txtQcNucd.OmitFilter = true;
            this.txtQcNucd.SelectedItem = null;
            this.txtQcNucd.SelectNone = true;
            this.txtQcNucd.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtQcNucd.ShowID = true;
            this.txtQcNucd.Size = new System.Drawing.Size(73, 21);
            this.txtQcNucd.TabIndex = 117;
            this.txtQcNucd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.QcNucd_KeyDown);
            // 
            // txtMrQual
            // 
            this.txtMrQual.EnterVisiable = true;
            this.txtMrQual.IsFind = true;
            this.txtMrQual.IsSelctNone = true;
            this.txtMrQual.IsSendToNext = false;
            this.txtMrQual.IsShowID = false;
            this.txtMrQual.ItemText = "";
            this.txtMrQual.ListBoxHeight = 100;
            this.txtMrQual.ListBoxVisible = false;
            this.txtMrQual.ListBoxWidth = 100;
            this.txtMrQual.Location = new System.Drawing.Point(222, 20);
            this.txtMrQual.MaxLength = 16;
            this.txtMrQual.Name = "txtMrQual";
            this.txtMrQual.OmitFilter = true;
            this.txtMrQual.SelectedItem = null;
            this.txtMrQual.SelectNone = true;
            this.txtMrQual.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMrQual.ShowID = true;
            this.txtMrQual.Size = new System.Drawing.Size(73, 21);
            this.txtMrQual.TabIndex = 115;
            this.txtMrQual.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MrQual_KeyDown);
            // 
            // tabPage5
            // 
            this.tabPage5.AutoScroll = true;
            this.tabPage5.Controls.Add(this.ucDiagNoseInput1);
            this.tabPage5.Location = new System.Drawing.Point(4, 21);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(771, 575);
            this.tabPage5.TabIndex = 2;
            this.tabPage5.Text = "诊断信息(F3)";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // ucDiagNoseInput1
            // 
            this.ucDiagNoseInput1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDiagNoseInput1.IsCas = true;
            this.ucDiagNoseInput1.IsDoubt = true;
            this.ucDiagNoseInput1.IsUseDeptICD = false;
            this.ucDiagNoseInput1.Location = new System.Drawing.Point(0, 0);
            this.ucDiagNoseInput1.Name = "ucDiagNoseInput1";
            this.ucDiagNoseInput1.Size = new System.Drawing.Size(771, 575);
            this.ucDiagNoseInput1.TabIndex = 0;
            // 
            // tabPage6
            // 
            this.tabPage6.AutoScroll = true;
            this.tabPage6.Controls.Add(this.ucOperation1);
            this.tabPage6.Location = new System.Drawing.Point(4, 21);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(771, 575);
            this.tabPage6.TabIndex = 3;
            this.tabPage6.Text = "手术信息(F4)";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // ucOperation1
            // 
            this.ucOperation1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucOperation1.Location = new System.Drawing.Point(0, 0);
            this.ucOperation1.Name = "ucOperation1";
            this.ucOperation1.Size = new System.Drawing.Size(771, 575);
            this.ucOperation1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.AutoScroll = true;
            this.tabPage2.Controls.Add(this.ucBabyCardInput1);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(771, 575);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "妇婴信息(F5)";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Visible = false;
            // 
            // ucBabyCardInput1
            // 
            this.ucBabyCardInput1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucBabyCardInput1.Location = new System.Drawing.Point(0, 0);
            this.ucBabyCardInput1.Name = "ucBabyCardInput1";
            this.ucBabyCardInput1.Size = new System.Drawing.Size(771, 575);
            this.ucBabyCardInput1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.AutoScroll = true;
            this.tabPage3.Controls.Add(this.ucChangeDept1);
            this.tabPage3.Location = new System.Drawing.Point(4, 21);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(771, 575);
            this.tabPage3.TabIndex = 5;
            this.tabPage3.Text = "转科信息(F6)";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // ucChangeDept1
            // 
            this.ucChangeDept1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucChangeDept1.Location = new System.Drawing.Point(0, 0);
            this.ucChangeDept1.Name = "ucChangeDept1";
            this.ucChangeDept1.Size = new System.Drawing.Size(771, 575);
            this.ucChangeDept1.TabIndex = 0;
            // 
            // tabPage7
            // 
            this.tabPage7.AutoScroll = true;
            this.tabPage7.Controls.Add(this.ucTumourCard2);
            this.tabPage7.Location = new System.Drawing.Point(4, 21);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(771, 575);
            this.tabPage7.TabIndex = 4;
            this.tabPage7.Text = "肿瘤信息(F7)";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // ucTumourCard2
            // 
            this.ucTumourCard2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTumourCard2.Location = new System.Drawing.Point(0, 0);
            this.ucTumourCard2.Name = "ucTumourCard2";
            this.ucTumourCard2.Size = new System.Drawing.Size(771, 575);
            this.ucTumourCard2.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.AutoScroll = true;
            this.tabPage4.Controls.Add(this.ucFeeInfo1);
            this.tabPage4.Location = new System.Drawing.Point(4, 21);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(771, 575);
            this.tabPage4.TabIndex = 6;
            this.tabPage4.Text = "费用信息(F8)";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // ucFeeInfo1
            // 
            this.ucFeeInfo1.BoolType = false;
            this.ucFeeInfo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucFeeInfo1.Location = new System.Drawing.Point(0, 0);
            this.ucFeeInfo1.Name = "ucFeeInfo1";
            this.ucFeeInfo1.Size = new System.Drawing.Size(771, 575);
            this.ucFeeInfo1.TabIndex = 0;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(218, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 600);
            this.splitter1.TabIndex = 9;
            this.splitter1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.treeView1);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(218, 600);
            this.panel2.TabIndex = 8;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 120);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(218, 480);
            this.treeView1.TabIndex = 1;
            this.treeView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNameSearch);
            this.groupBox1.Controls.Add(this.txtCaseNOSearch);
            this.groupBox1.Controls.Add(this.neuLabel13);
            this.groupBox1.Controls.Add(this.txtPatientNOSearch);
            this.groupBox1.Controls.Add(this.label79);
            this.groupBox1.Controls.Add(this.label68);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(218, 120);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // txtNameSearch
            // 
            this.txtNameSearch.Location = new System.Drawing.Point(85, 50);
            this.txtNameSearch.MaxLength = 10;
            this.txtNameSearch.Name = "txtNameSearch";
            this.txtNameSearch.Size = new System.Drawing.Size(103, 21);
            this.txtNameSearch.TabIndex = 1;
            this.txtNameSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNameSearch_KeyDown);
            this.txtNameSearch.Enter += new System.EventHandler(this.txtNameSearch_Enter);
            // 
            // txtCaseNOSearch
            // 
            this.txtCaseNOSearch.Location = new System.Drawing.Point(85, 77);
            this.txtCaseNOSearch.MaxLength = 10;
            this.txtCaseNOSearch.Name = "txtCaseNOSearch";
            this.txtCaseNOSearch.Size = new System.Drawing.Size(103, 21);
            this.txtCaseNOSearch.TabIndex = 1;
            this.txtCaseNOSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCaseNOSearch_KeyDown);
            this.txtCaseNOSearch.Enter += new System.EventHandler(this.txtCaseNOSearch_Enter);
            // 
            // neuLabel13
            // 
            this.neuLabel13.AutoSize = true;
            this.neuLabel13.Location = new System.Drawing.Point(26, 54);
            this.neuLabel13.Name = "neuLabel13";
            this.neuLabel13.Size = new System.Drawing.Size(53, 12);
            this.neuLabel13.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel13.TabIndex = 0;
            this.neuLabel13.Text = "姓    名";
            // 
            // txtPatientNOSearch
            // 
            this.txtPatientNOSearch.Location = new System.Drawing.Point(85, 23);
            this.txtPatientNOSearch.MaxLength = 10;
            this.txtPatientNOSearch.Name = "txtPatientNOSearch";
            this.txtPatientNOSearch.Size = new System.Drawing.Size(103, 21);
            this.txtPatientNOSearch.TabIndex = 1;
            this.txtPatientNOSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatientNOSearch_KeyDown);
            this.txtPatientNOSearch.Enter += new System.EventHandler(this.txtPatientNOSearch_Enter);
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.Location = new System.Drawing.Point(26, 81);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(53, 12);
            this.label79.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label79.TabIndex = 0;
            this.label79.Text = "病 案 号";
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Location = new System.Drawing.Point(26, 27);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(53, 12);
            this.label68.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label68.TabIndex = 0;
            this.label68.Text = "住 院 号";
            // 
            // fpEnter1
            // 
            this.fpEnter1.About = "3.0.2004.2005";
            this.fpEnter1.AccessibleDescription = "";
            this.fpEnter1.EditModePermanent = true;
            this.fpEnter1.EditModeReplace = true;
            this.fpEnter1.Location = new System.Drawing.Point(0, 0);
            this.fpEnter1.Name = "fpEnter1";
            this.fpEnter1.SelectNone = false;
            this.fpEnter1.ShowListWhenOfFocus = false;
            this.fpEnter1.Size = new System.Drawing.Size(200, 100);
            this.fpEnter1.TabIndex = 0;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpEnter1.TextTipAppearance = tipAppearance2;
            this.fpEnter1.ActiveSheetIndex = -1;
            // 
            // ucCaseMainInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.panel1);
            this.Name = "ucCaseMainInfo";
            this.Size = new System.Drawing.Size(1000, 600);
            this.Load += new System.EventHandler(this.ucCaseMainInfo_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tab1.ResumeLayout(false);
            this.tabPage8.ResumeLayout(false);
            this.neuGroupBox10.ResumeLayout(false);
            this.neuGroupBox9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            this.cmsMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.neuGroupBox8.ResumeLayout(false);
            this.neuGroupBox8.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.neuGroupBox11.ResumeLayout(false);
            this.neuGroupBox11.PerformLayout();
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.neuGroupBox2.ResumeLayout(false);
            this.neuGroupBox2.PerformLayout();
            this.neuGroupBox3.ResumeLayout(false);
            this.neuGroupBox3.PerformLayout();
            this.neuGroupBox4.ResumeLayout(false);
            this.neuGroupBox4.PerformLayout();
            this.neuGroupBox5.ResumeLayout(false);
            this.neuGroupBox5.PerformLayout();
            this.neuGroupBox6.ResumeLayout(false);
            this.neuGroupBox6.PerformLayout();
            this.neuGroupBox7.ResumeLayout(false);
            this.neuGroupBox7.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpEnter1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtCaseNOSearch;
        private System.Windows.Forms.TextBox txtPatientNOSearch;
        private FS.FrameWork.WinForms.Controls.NeuLabel label79;
        private FS.FrameWork.WinForms.Controls.NeuLabel label68;
        private System.Windows.Forms.TreeView treeView1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtBC;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox3;
        private CustomListBox txtPharmacyAllergic1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox4;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox5;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox6;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox7;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private CustomListBox txtPharmacyAllergic2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtPETNumb;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel7;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtECTNumb;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtNomen;
        private System.Windows.Forms.TabPage tabPage8;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox8;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox9;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuContextMenuStrip cmsMain;
        private System.Windows.Forms.ToolStripMenuItem tsmAdd;
        private System.Windows.Forms.ToolStripMenuItem tsmDel;
        private System.Windows.Forms.ToolStripMenuItem tsmSave;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox10;
        private FS.FrameWork.WinForms.Controls.NeuButton btnSave;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rdbStop;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton rdbNormal;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblState;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbResult;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtContent;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblContent;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbLinkType;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblLinkwayType;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtWritebackPerson;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblWritebackPerson;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabelTransferPosition;
        private FS.FrameWork.WinForms.Controls.NeuComboBox neuComboBoxTransferPosition;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbSymptom;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblSymptom;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabelSequela;
        private FS.FrameWork.WinForms.Controls.NeuComboBox neuComboBoxSequela;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabelrecrudescdeTime;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker neuDateTimePickerRecrudesceTime;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox neuCheckBoxIsTtransfer;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbllResult;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox neuCheckBoxIsSequela;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox neuCheckBoxIsRecrudesce;
        private FS.FrameWork.WinForms.Controls.NeuComboBox neuComboBoxDeadReason;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabelDeadReason;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker neuDateTimePickerDeadTime;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabelDeadTime;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox neuCheckBoxIsDead;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbCircs;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblCircs;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox2;
        private FS.FrameWork.WinForms.Controls.NeuLabel label49;
        private FS.FrameWork.WinForms.Controls.NeuLabel label61;
        private FS.FrameWork.WinForms.Controls.NeuLabel label60;
        private FS.FrameWork.WinForms.Controls.NeuLabel label42;
        private FS.FrameWork.WinForms.Controls.NeuLabel label41;
        private FS.FrameWork.WinForms.Controls.NeuLabel label40;
        private FS.FrameWork.WinForms.Controls.NeuLabel label39;
        private FS.FrameWork.WinForms.Controls.ValidatedTextBox txtPiDays;
        private FS.FrameWork.WinForms.Controls.NeuLabel label38;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker txtDiagDate;
        private FS.FrameWork.WinForms.Controls.NeuLabel label37;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker txtDateOut;
        private FS.FrameWork.WinForms.Controls.NeuLabel label36;
        private FS.FrameWork.WinForms.Controls.NeuLabel label35;
        private FS.FrameWork.WinForms.Controls.NeuLabel label28;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtDateIn;
        private FS.FrameWork.WinForms.Controls.NeuLabel label27;
        private FS.FrameWork.WinForms.Controls.NeuLabel label26;
        private FS.FrameWork.WinForms.Controls.NeuLabel label64;
        private FS.FrameWork.WinForms.Controls.NeuLabel label63;
        private FS.FrameWork.WinForms.Controls.NeuLabel label62;
        private CustomListBox txtDeptInHospital;
        private CustomListBox txtDeptOut;
        private CustomListBox txtCircs;
        private CustomListBox txtRefresherDocd;
        private CustomListBox txtAdmittingDoctor;
        private CustomListBox txtAttendingDoctor;
        private CustomListBox txtGraDocCode;
        private FS.FrameWork.WinForms.Controls.NeuLabel label43;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtComeFrom;
        private CustomListBox txtInAvenue;
        private CustomListBox txtConsultingDoctor;
        private CustomListBox txtPraDocCode;
        private CustomListBox txtClinicDocd;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtRuyuanDiagNose;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtClinicDiag;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbUnit;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtLinkmanAdd;
        private FS.FrameWork.WinForms.Controls.NeuLabel labl;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtAreaCode;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtDIST;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtSSN;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtLinkmanTel;
        private FS.FrameWork.WinForms.Controls.NeuLabel label25;
        private FS.FrameWork.WinForms.Controls.NeuLabel label24;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtKin;
        private FS.FrameWork.WinForms.Controls.NeuLabel label23;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtHomeZip;
        private FS.FrameWork.WinForms.Controls.NeuLabel label22;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtAddressHome;
        private FS.FrameWork.WinForms.Controls.NeuLabel label20;
        private FS.FrameWork.WinForms.Controls.NeuLabel label19;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtPhoneBusiness;
        private FS.FrameWork.WinForms.Controls.NeuLabel label18;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtAddressBusiness;
        private FS.FrameWork.WinForms.Controls.NeuLabel label17;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtIDNo;
        private FS.FrameWork.WinForms.Controls.NeuLabel label16;
        private FS.FrameWork.WinForms.Controls.NeuLabel label15;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtPatientName;
        private FS.FrameWork.WinForms.Controls.NeuLabel label13;
        private FS.FrameWork.WinForms.Controls.NeuLabel label12;
        private FS.FrameWork.WinForms.Controls.NeuLabel label11;
        private FS.FrameWork.WinForms.Controls.NeuLabel label10;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtPatientAge;
        private FS.FrameWork.WinForms.Controls.NeuLabel label9;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtPatientBirthday;
        private FS.FrameWork.WinForms.Controls.NeuLabel label8;
        private FS.FrameWork.WinForms.Controls.NeuLabel label7;
        private FS.FrameWork.WinForms.Controls.NeuLabel label6;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtClinicNo;
        private FS.FrameWork.WinForms.Controls.NeuLabel label5;
        private FS.FrameWork.WinForms.Controls.NeuLabel label4;
        private FS.FrameWork.WinForms.Controls.NeuLabel label3;
        private FS.FrameWork.WinForms.Controls.ValidatedTextBox txtInTimes;
        private FS.FrameWork.WinForms.Controls.NeuLabel label2;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtCaseNum;
        private FS.FrameWork.WinForms.Controls.NeuLabel label1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtPhoneHome;
        private FS.FrameWork.WinForms.Controls.NeuLabel label21;
        private FS.FrameWork.WinForms.Controls.NeuLabel label14;
        private CustomListBox txtCountry;
        private CustomListBox txtProfession;
        private CustomListBox txtNationality;
        private CustomListBox txtMaritalStatus;
        private CustomListBox txtPatientSex;
        private CustomListBox txtPactKind;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtThird;
        private FS.FrameWork.WinForms.Controls.NeuLabel label33;
        private FS.FrameWork.WinForms.Controls.NeuLabel label34;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtSecond;
        private FS.FrameWork.WinForms.Controls.NeuLabel label31;
        private FS.FrameWork.WinForms.Controls.NeuLabel label32;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtFirstTime;
        private FS.FrameWork.WinForms.Controls.NeuLabel label30;
        private FS.FrameWork.WinForms.Controls.NeuLabel label29;
        private CustomListBox txtDeptThird;
        private CustomListBox txtFirstDept;
        private CustomListBox txtDeptSecond;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel label48;
        private FS.FrameWork.WinForms.Controls.NeuLabel label47;
        private FS.FrameWork.WinForms.Controls.NeuLabel label46;
        private FS.FrameWork.WinForms.Controls.ValidatedTextBox txtInfectNum;
        private FS.FrameWork.WinForms.Controls.NeuLabel label44;
        private CustomListBox txtHivAb;
        private CustomListBox txtHcvAb;
        private CustomListBox txtHbsag;
        private FS.FrameWork.WinForms.Controls.ValidatedTextBox txtSuccTimes;
        private FS.FrameWork.WinForms.Controls.NeuLabel label56;
        private FS.FrameWork.WinForms.Controls.ValidatedTextBox txtSalvTimes;
        private FS.FrameWork.WinForms.Controls.NeuLabel label55;
        private FS.FrameWork.WinForms.Controls.NeuLabel label54;
        private FS.FrameWork.WinForms.Controls.NeuLabel label53;
        private FS.FrameWork.WinForms.Controls.NeuLabel label52;
        private FS.FrameWork.WinForms.Controls.NeuLabel label51;
        private FS.FrameWork.WinForms.Controls.NeuLabel label50;
        private CustomListBox txtFsBl;
        private CustomListBox txtClPa;
        private CustomListBox txtOpbOpa;
        private CustomListBox txtPiPo;
        private CustomListBox txtCePi;
        private FS.FrameWork.WinForms.Controls.NeuTextBox textBox13;
        private FS.FrameWork.WinForms.Controls.NeuTextBox textBox12;
        private FS.FrameWork.WinForms.Controls.NeuTextBox textBox11;
        private FS.FrameWork.WinForms.Controls.NeuTextBox textBox10;
        private FS.FrameWork.WinForms.Controls.NeuTextBox textBox9;
        private FS.FrameWork.WinForms.Controls.NeuTextBox textBox8;
        private FS.FrameWork.WinForms.Controls.NeuTextBox textBox7;
        private FS.FrameWork.WinForms.Controls.NeuTextBox textBox6;
        private FS.FrameWork.WinForms.Controls.NeuTextBox textBox5;
        private FS.FrameWork.WinForms.Controls.NeuTextBox textBox4;
        private FS.FrameWork.WinForms.Controls.NeuTextBox textBox3;
        private FS.FrameWork.WinForms.Controls.NeuTextBox textBox2;
        private FS.FrameWork.WinForms.Controls.NeuTextBox textBox1;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox cbYnFirst;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtPathNumb;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtCtNumb;
        private FS.FrameWork.WinForms.Controls.NeuLabel label92;
        private FS.FrameWork.WinForms.Controls.ValidatedTextBox txtSPecalNus;
        private FS.FrameWork.WinForms.Controls.NeuLabel label90;
        private FS.FrameWork.WinForms.Controls.ValidatedTextBox txtStrictNuss;
        private FS.FrameWork.WinForms.Controls.NeuLabel label91;
        private FS.FrameWork.WinForms.Controls.ValidatedTextBox txtIIINus;
        private FS.FrameWork.WinForms.Controls.ValidatedTextBox txtIINus;
        private FS.FrameWork.WinForms.Controls.NeuLabel label88;
        private FS.FrameWork.WinForms.Controls.ValidatedTextBox txtINus;
        private FS.FrameWork.WinForms.Controls.NeuLabel label85;
        private FS.FrameWork.WinForms.Controls.ValidatedTextBox txtSuperNus;
        private FS.FrameWork.WinForms.Controls.NeuLabel label82;
        private FS.FrameWork.WinForms.Controls.ValidatedTextBox txtOutconNum;
        private FS.FrameWork.WinForms.Controls.NeuLabel label83;
        private FS.FrameWork.WinForms.Controls.ValidatedTextBox txtInconNum;
        private FS.FrameWork.WinForms.Controls.ValidatedTextBox txtBloodOther;
        private FS.FrameWork.WinForms.Controls.NeuLabel label81;
        private FS.FrameWork.WinForms.Controls.ValidatedTextBox txtBloodWhole;
        private FS.FrameWork.WinForms.Controls.NeuLabel label80;
        private FS.FrameWork.WinForms.Controls.ValidatedTextBox txtBodyAnotomize;
        private FS.FrameWork.WinForms.Controls.NeuLabel label78;
        private FS.FrameWork.WinForms.Controls.ValidatedTextBox txtBloodPlatelet;
        private FS.FrameWork.WinForms.Controls.NeuLabel label77;
        private FS.FrameWork.WinForms.Controls.ValidatedTextBox txtBloodRed;
        private FS.FrameWork.WinForms.Controls.NeuLabel label76;
        private FS.FrameWork.WinForms.Controls.NeuLabel label75;
        private FS.FrameWork.WinForms.Controls.NeuLabel label74;
        private FS.FrameWork.WinForms.Controls.NeuLabel label73;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox cbDisease30;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox cbTechSerc;
        private FS.FrameWork.WinForms.Controls.NeuLabel label72;
        private FS.FrameWork.WinForms.Controls.NeuLabel label71;
        private FS.FrameWork.WinForms.Controls.NeuLabel label70;
        private FS.FrameWork.WinForms.Controls.ValidatedTextBox txtVisiPeriYear;
        private FS.FrameWork.WinForms.Controls.ValidatedTextBox txtVisiPeriMonth;
        private FS.FrameWork.WinForms.Controls.ValidatedTextBox txtVisiPeriWeek;
        private FS.FrameWork.WinForms.Controls.NeuLabel label69;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox cbVisiStat;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox cbBodyCheck;
        private FS.FrameWork.WinForms.Controls.NeuLabel label87;
        private FS.FrameWork.WinForms.Controls.NeuLabel label84;
        private CustomListBox txtReactionBlood;
        private CustomListBox txtRhBlood;
        private CustomListBox txtBloodType;
        private FS.FrameWork.WinForms.Controls.NeuLabel label45;
        private FS.FrameWork.WinForms.Controls.NeuLabel label96;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker txtCheckDate;
        private FS.FrameWork.WinForms.Controls.NeuLabel label65;
        private FS.FrameWork.WinForms.Controls.NeuLabel label59;
        private FS.FrameWork.WinForms.Controls.NeuLabel label58;
        private FS.FrameWork.WinForms.Controls.NeuLabel label57;
        private FS.FrameWork.WinForms.Controls.NeuLabel label66;
        private FS.FrameWork.WinForms.Controls.NeuLabel label67;
        private CustomListBox txtQcDocd;
        private CustomListBox txtOperationCode;
        private CustomListBox txtInputDoc;
        private CustomListBox txtCoordinate;
        private CustomListBox txtCodingCode;
        private CustomListBox txtQcNucd;
        private CustomListBox txtMrQual;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpVisitTime;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel8;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtCaseStus;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel9;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtInfectionPositionNew;
        private FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox txtInjuryOrPoisoningCause;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel10;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel12;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel11;
        private CustomListBox txtFourDiseasesReport;
        private CustomListBox txtInfectionDiseasesReport;
        private System.Windows.Forms.TextBox txtNameSearch;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel13;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtBusinessZip;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox11;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtRemark;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel14;
        private CustomListBox txtDeptChiefDoc;
        private System.Windows.Forms.Label label86;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtComeTo;
        private System.Windows.Forms.Label label89;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel15;
        private CustomListBox txtReactionTransfuse;
        private FS.FrameWork.WinForms.Controls.NeuTextBox neuTxtPharmacyAllergic2;
        private FS.FrameWork.WinForms.Controls.NeuTextBox neuTxtPharmacyAllergic1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel17;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtOutRoom;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel16;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtInRoom;
    }
}
