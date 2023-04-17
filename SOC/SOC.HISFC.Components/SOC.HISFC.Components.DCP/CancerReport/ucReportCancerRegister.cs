using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.DCP.CancerReport
{
	/// <summary>
	/// ucReportCancerRegister 的摘要说明。
	/// </summary>
	public class ucReportCancerRegister : System.Windows.Forms.UserControl
	{
		#region
		private System.Windows.Forms.Label lbTitle;
		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.Panel panel7;
		public System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.HelpProvider helpProvider1;
		private System.Windows.Forms.Label label35;
		private System.Windows.Forms.Label label6;
		//private System.Windows.Forms.RichTextBox rtxtMemo;
		private System.Windows.Forms.Label label37;
		private System.Windows.Forms.Label label39;
		private System.Windows.Forms.Label label41;
//		private System.Windows.Forms.TextBox txtCase;
		private System.Windows.Forms.Panel Printpanel;
		private System.Windows.Forms.Panel panelPatientInfo;
		private System.Windows.Forms.Label lbDoctorDept;
		private System.Windows.Forms.Label lbReportTime;
		private System.Windows.Forms.Label lbReportDoctor;
		private System.Windows.Forms.Label label63;
		public System.Windows.Forms.TextBox txtRelationship;
		private FS.FrameWork.WinForms.Controls.NeuComboBox  cmbMarrige;
		//private System.Windows.Forms.ComboBox cmbNation;
		public FS.FrameWork.WinForms.Controls.NeuComboBox  cmbNation;
		public System.Windows.Forms.TextBox txtRegisterCity;
		public System.Windows.Forms.Label label1;
		public System.Windows.Forms.RadioButton rdbReback5;
		public System.Windows.Forms.RadioButton rdbReback4;
		public System.Windows.Forms.RadioButton rdbReback3;
		public System.Windows.Forms.RadioButton rdbReback2;
		public System.Windows.Forms.RadioButton rdbReback1;
		public System.Windows.Forms.RadioButton rdbPathlogyN;
		public System.Windows.Forms.RadioButton rdbPathlogyY;
		public System.Windows.Forms.RadioButton rdbTNM4;
		public System.Windows.Forms.RadioButton rdbTNM3;
		public System.Windows.Forms.RadioButton rdbTNM2;
		public System.Windows.Forms.RadioButton rdbTNM1;
		public System.Windows.Forms.DateTimePicker dtOldDiagnosesTime;
		public System.Windows.Forms.TextBox txtDeathResion;
		public System.Windows.Forms.Label label17;
		public System.Windows.Forms.Label label43;
		public System.Windows.Forms.TextBox txtOldDiagnoses;
		public System.Windows.Forms.TextBox txtRebackDemo;
		public System.Windows.Forms.TextBox txtTreatmentDemo;
		public System.Windows.Forms.CheckBox chbTreatment9;
		public System.Windows.Forms.CheckBox chbTreatment6;
		public System.Windows.Forms.CheckBox chbTreatment5;
		public System.Windows.Forms.CheckBox chbTreatment7;
		public System.Windows.Forms.CheckBox chbTreatment4;
		public System.Windows.Forms.CheckBox chbTreatment3;
		public System.Windows.Forms.CheckBox chbTreatment2;
		public System.Windows.Forms.CheckBox chbTreatment1;
		public System.Windows.Forms.TextBox txtDiagnosesDemo;
		public System.Windows.Forms.CheckBox chbDiagnoses18;
		public System.Windows.Forms.CheckBox chbDiagnoses17;
		public System.Windows.Forms.CheckBox chbDiagnoses19;
		public System.Windows.Forms.CheckBox chbDiagnoses16;
		public System.Windows.Forms.CheckBox chbDiagnoses15;
		public System.Windows.Forms.CheckBox chbTreatment8;
		public System.Windows.Forms.CheckBox chbDiagnoses14;
		public System.Windows.Forms.CheckBox chbDiagnoses13;
		public System.Windows.Forms.CheckBox chbDiagnoses12;
		public System.Windows.Forms.CheckBox chbDiagnoses11;
		public System.Windows.Forms.CheckBox chbDiagnoses10;
		public System.Windows.Forms.CheckBox chbDiagnoses9;
		public System.Windows.Forms.CheckBox chbDiagnoses8;
		public System.Windows.Forms.CheckBox chbDiagnoses7;
		public System.Windows.Forms.CheckBox chbDiagnoses6;
		public System.Windows.Forms.CheckBox chbDiagnoses5;
		public System.Windows.Forms.CheckBox chbDiagnoses4;
		public System.Windows.Forms.CheckBox chbDiagnoses3;
		public System.Windows.Forms.CheckBox chbDiagnoses2;
		public System.Windows.Forms.CheckBox chbDiagnoses1;
		public System.Windows.Forms.Label label18;
		public System.Windows.Forms.Label label38;
		public System.Windows.Forms.Label label36;
		public System.Windows.Forms.Label label19;
		public System.Windows.Forms.TextBox txtICDO;
		public System.Windows.Forms.Label label34;
		public System.Windows.Forms.TextBox txtPathlogyDegree;
		public System.Windows.Forms.Label label33;
		public System.Windows.Forms.TextBox txtPathlogyType;
		public System.Windows.Forms.Label label31;
		public System.Windows.Forms.TextBox txtPathlogyNo;
		public System.Windows.Forms.Label label32;
		public System.Windows.Forms.Label label30;
		public System.Windows.Forms.Label label29;
		public System.Windows.Forms.TextBox txtClinicalM;
		public System.Windows.Forms.Label label28;
		public System.Windows.Forms.TextBox txtClinicalN;
		public System.Windows.Forms.Label label26;
		public System.Windows.Forms.TextBox txtClinicalT;
		public System.Windows.Forms.RadioButton rdbTreatN;
		public System.Windows.Forms.RadioButton rdbTreatY;
		public System.Windows.Forms.Label label64;
		private System.Windows.Forms.DateTimePicker dateTimePicker1;
		private System.Windows.Forms.Label label16;
		public System.Windows.Forms.TextBox txtICD;
		public System.Windows.Forms.Label label25;
		public System.Windows.Forms.TextBox txtContactPerson;
		public System.Windows.Forms.Label label23;
		public System.Windows.Forms.TextBox txtContactPersonAddr;
		public System.Windows.Forms.Label label22;
		public System.Windows.Forms.TextBox txtContactPersonTel;
		public System.Windows.Forms.Label label21;
		public System.Windows.Forms.Label label20;
		public System.Windows.Forms.TextBox txtWorkPlace;
		public System.Windows.Forms.Label label14;
		public System.Windows.Forms.TextBox txtPost;
		public System.Windows.Forms.Label label15;
		public System.Windows.Forms.TextBox txtRegisterCounty;
		public System.Windows.Forms.Label label5;
		public System.Windows.Forms.Label label7;
		public System.Windows.Forms.TextBox txtRegisterProvince;
		public System.Windows.Forms.Label label8;
		public System.Windows.Forms.TextBox txtRegisterHouseNO;
		public System.Windows.Forms.TextBox txtRegisterTown;
		public System.Windows.Forms.Label label10;
		public System.Windows.Forms.Label label12;
		public System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox txtHandPhone;
		private System.Windows.Forms.Label label4;
	   //	private System.Windows.Forms.ComboBox cmbProfession;
		private FS.FrameWork.WinForms.Controls.NeuComboBox  cmbProfession;
		public System.Windows.Forms.Label label11;
		public System.Windows.Forms.Label label2;
		public System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox txtSpecialAddress;
		public System.Windows.Forms.Label label45;
		private System.Windows.Forms.Label lbSexStar;
		private System.Windows.Forms.Label label49;
		public System.Windows.Forms.Label label50;
		private System.Windows.Forms.TextBox txtTelephone;
		private System.Windows.Forms.Label label57;
		private System.Windows.Forms.RadioButton rdbDay;
		private System.Windows.Forms.RadioButton rdbMonth;
		private System.Windows.Forms.RadioButton rdbYear;
		private System.Windows.Forms.Label label59;
		private System.Windows.Forms.TextBox txtAge;
		private System.Windows.Forms.Label label60;
		private System.Windows.Forms.Label label61;
		private System.Windows.Forms.CheckBox cbxWomen;
		private System.Windows.Forms.CheckBox cbxMan;
		private System.Windows.Forms.Label lbSex;
		private System.Windows.Forms.TextBox txtPatientID;
		private System.Windows.Forms.TextBox txtPatientName;
		private System.Windows.Forms.Label label65;
		public System.Windows.Forms.TextBox txtWorkName;
		public System.Windows.Forms.Label label58;
		private System.Windows.Forms.DateTimePicker dtBirthDay;
		public System.Windows.Forms.Label label3;
		private FS.FrameWork.WinForms.Controls.NeuComboBox cmbInfectionClass;
		private System.Windows.Forms.Label label48;
		private System.Windows.Forms.Panel panelTitle;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.Label lbMadicalCardNo;
		private System.Windows.Forms.Label lbID;
		private System.Windows.Forms.Label lbState;
		public System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label66;
		private System.Windows.Forms.Label label67;
		private System.Windows.Forms.Label lbPatientDept;
		private System.Windows.Forms.Label label69;
		private System.Windows.Forms.Label label40;
		private System.Windows.Forms.Label label42;
		private System.Windows.Forms.Label label44;
		public System.Windows.Forms.TextBox txtDistrict;
		public System.Windows.Forms.Label label27;
		private System.Windows.Forms.Label lb_patientType;
		public System.Windows.Forms.TextBox txtCancer_No;
		private System.Windows.Forms.Label label46;
		private System.Windows.Forms.Label lbReportNO;
		private System.Windows.Forms.Label label51;
		private System.Windows.Forms.Label label47;
		private System.Windows.Forms.RichTextBox rtxtMemo;
		private System.Windows.Forms.TextBox txtCase;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Panel panel8;
		private System.Windows.Forms.Label label52;
		private System.Windows.Forms.Label label53;
		private System.Windows.Forms.Label label54;
		private System.Windows.Forms.Label label55;
		public System.Windows.Forms.Label label56;
		public System.Windows.Forms.Label label62;
		public System.Windows.Forms.Label label68;
		public System.Windows.Forms.Label label70;
		public System.Windows.Forms.Label label72;
		public System.Windows.Forms.Label label73;
		public System.Windows.Forms.Label label74;
		public System.Windows.Forms.Label label75;
		public System.Windows.Forms.Label label76;
		public System.Windows.Forms.Label label77;
		public System.Windows.Forms.Label label78;
		public System.Windows.Forms.Label label81;
		public System.Windows.Forms.Label label82;
		public System.Windows.Forms.Label label83;
		public System.Windows.Forms.Label label85;
		public System.Windows.Forms.Label label87;
		public System.Windows.Forms.Label label88;
		public System.Windows.Forms.Label label89;
		public System.Windows.Forms.Label label90;
		public System.Windows.Forms.Label label91;
		public System.Windows.Forms.Label label92;
		public System.Windows.Forms.Label label93;
		public System.Windows.Forms.Label label94;
		public System.Windows.Forms.Label label95;
		public System.Windows.Forms.Label label96;
		public System.Windows.Forms.TextBox textBox1;
		public System.Windows.Forms.TextBox textBox2;
		public System.Windows.Forms.Label label98;
		public System.Windows.Forms.Label label100;
		public System.Windows.Forms.TextBox textBox3;
		public System.Windows.Forms.TextBox textBox4;
		public System.Windows.Forms.TextBox textBox5;
		public System.Windows.Forms.Label label101;
		public System.Windows.Forms.Label label102;
		private System.Windows.Forms.Label label103;
		public System.Windows.Forms.DateTimePicker dateTimePicker2;
		public System.Windows.Forms.Label label104;
		public System.Windows.Forms.Label label105;
		private System.Windows.Forms.Label label79;
		public System.Windows.Forms.Label label80;
		private System.Windows.Forms.Label label84;
		private FS.FrameWork.WinForms.Controls.NeuComboBox cmbSource;
		public System.Windows.Forms.TextBox lbPatientNo;
		public System.Windows.Forms.Label label86;
		public System.Windows.Forms.Label label97;
		public System.Windows.Forms.Label label99;
		public System.Windows.Forms.Label label71;
		public System.Windows.Forms.Label label106;
		public System.Windows.Forms.TextBox txtWorkType;
		private System.ComponentModel.IContainer components;

		public ucReportCancerRegister()
		{
			// 该调用是 Windows.Forms 窗体设计器所必需的。
			InitializeComponent();
			//this.initEvent();
			// TODO: 在 InitializeComponent 调用后添加任何初始化

		}

		/// <summary> 
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion

		#region 组件设计器生成的代码
		/// <summary> 
		/// 设计器支持所需的方法 - 不要使用代码编辑器 
		/// 修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.lbTitle = new System.Windows.Forms.Label();
			this.panel6 = new System.Windows.Forms.Panel();
			this.panel7 = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.Printpanel = new System.Windows.Forms.Panel();
			this.panelTitle = new System.Windows.Forms.Panel();
			this.txtCancer_No = new System.Windows.Forms.TextBox();
			this.label27 = new System.Windows.Forms.Label();
			this.lbID = new System.Windows.Forms.Label();
			this.lbReportNO = new System.Windows.Forms.Label();
			this.lbState = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label66 = new System.Windows.Forms.Label();
			this.lbPatientDept = new System.Windows.Forms.Label();
			this.label69 = new System.Windows.Forms.Label();
			this.panelPatientInfo = new System.Windows.Forms.Panel();
			this.txtWorkType = new System.Windows.Forms.TextBox();
			this.label106 = new System.Windows.Forms.Label();
			this.label71 = new System.Windows.Forms.Label();
			this.label99 = new System.Windows.Forms.Label();
			this.label97 = new System.Windows.Forms.Label();
			this.label86 = new System.Windows.Forms.Label();
			this.lbPatientNo = new System.Windows.Forms.TextBox();
			this.cmbSource = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
			this.label84 = new System.Windows.Forms.Label();
			this.chbDiagnoses16 = new System.Windows.Forms.CheckBox();
			this.chbDiagnoses8 = new System.Windows.Forms.CheckBox();
			this.label80 = new System.Windows.Forms.Label();
			this.rtxtMemo = new System.Windows.Forms.RichTextBox();
			this.label79 = new System.Windows.Forms.Label();
			this.lbReportDoctor = new System.Windows.Forms.Label();
			this.label105 = new System.Windows.Forms.Label();
			this.label104 = new System.Windows.Forms.Label();
			this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
			this.label103 = new System.Windows.Forms.Label();
			this.label96 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label98 = new System.Windows.Forms.Label();
			this.label100 = new System.Windows.Forms.Label();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.label101 = new System.Windows.Forms.Label();
			this.label102 = new System.Windows.Forms.Label();
			this.label95 = new System.Windows.Forms.Label();
			this.label94 = new System.Windows.Forms.Label();
			this.label93 = new System.Windows.Forms.Label();
			this.label92 = new System.Windows.Forms.Label();
			this.label91 = new System.Windows.Forms.Label();
			this.label90 = new System.Windows.Forms.Label();
			this.label89 = new System.Windows.Forms.Label();
			this.label88 = new System.Windows.Forms.Label();
			this.label87 = new System.Windows.Forms.Label();
			this.label85 = new System.Windows.Forms.Label();
			this.label83 = new System.Windows.Forms.Label();
			this.label82 = new System.Windows.Forms.Label();
			this.label81 = new System.Windows.Forms.Label();
			this.label78 = new System.Windows.Forms.Label();
			this.label77 = new System.Windows.Forms.Label();
			this.label76 = new System.Windows.Forms.Label();
			this.label75 = new System.Windows.Forms.Label();
			this.label74 = new System.Windows.Forms.Label();
			this.label73 = new System.Windows.Forms.Label();
			this.label72 = new System.Windows.Forms.Label();
			this.label70 = new System.Windows.Forms.Label();
			this.label68 = new System.Windows.Forms.Label();
			this.label62 = new System.Windows.Forms.Label();
			this.label56 = new System.Windows.Forms.Label();
			this.label55 = new System.Windows.Forms.Label();
			this.label54 = new System.Windows.Forms.Label();
			this.label53 = new System.Windows.Forms.Label();
			this.label52 = new System.Windows.Forms.Label();
			this.panel8 = new System.Windows.Forms.Panel();
			this.rdbReback5 = new System.Windows.Forms.RadioButton();
			this.rdbReback4 = new System.Windows.Forms.RadioButton();
			this.rdbReback3 = new System.Windows.Forms.RadioButton();
			this.rdbReback2 = new System.Windows.Forms.RadioButton();
			this.rdbReback1 = new System.Windows.Forms.RadioButton();
			this.panel5 = new System.Windows.Forms.Panel();
			this.rdbTreatY = new System.Windows.Forms.RadioButton();
			this.rdbTreatN = new System.Windows.Forms.RadioButton();
			this.panel3 = new System.Windows.Forms.Panel();
			this.rdbPathlogyN = new System.Windows.Forms.RadioButton();
			this.rdbPathlogyY = new System.Windows.Forms.RadioButton();
			this.txtClinicalM = new System.Windows.Forms.TextBox();
			this.panel4 = new System.Windows.Forms.Panel();
			this.rdbTNM4 = new System.Windows.Forms.RadioButton();
			this.rdbTNM3 = new System.Windows.Forms.RadioButton();
			this.rdbTNM2 = new System.Windows.Forms.RadioButton();
			this.rdbTNM1 = new System.Windows.Forms.RadioButton();
			this.label47 = new System.Windows.Forms.Label();
			this.cmbMarrige = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
			this.label11 = new System.Windows.Forms.Label();
			this.txtPatientID = new System.Windows.Forms.TextBox();
			this.cmbProfession = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
			this.txtSpecialAddress = new System.Windows.Forms.TextBox();
			this.txtContactPerson = new System.Windows.Forms.TextBox();
			this.txtRelationship = new System.Windows.Forms.TextBox();
			this.txtContactPersonTel = new System.Windows.Forms.TextBox();
			this.txtContactPersonAddr = new System.Windows.Forms.TextBox();
			this.txtICD = new System.Windows.Forms.TextBox();
			this.txtPathlogyNo = new System.Windows.Forms.TextBox();
			this.txtPathlogyType = new System.Windows.Forms.TextBox();
			this.txtICDO = new System.Windows.Forms.TextBox();
			this.txtPathlogyDegree = new System.Windows.Forms.TextBox();
			this.txtOldDiagnoses = new System.Windows.Forms.TextBox();
			this.txtDiagnosesDemo = new System.Windows.Forms.TextBox();
			this.txtPatientName = new System.Windows.Forms.TextBox();
			this.label40 = new System.Windows.Forms.Label();
			this.label42 = new System.Windows.Forms.Label();
			this.label44 = new System.Windows.Forms.Label();
			this.lbDoctorDept = new System.Windows.Forms.Label();
			this.lbReportTime = new System.Windows.Forms.Label();
			this.label63 = new System.Windows.Forms.Label();
			this.cmbNation = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
			this.txtRegisterCity = new System.Windows.Forms.TextBox();
			this.dtOldDiagnosesTime = new System.Windows.Forms.DateTimePicker();
			this.txtDeathResion = new System.Windows.Forms.TextBox();
			this.label17 = new System.Windows.Forms.Label();
			this.label43 = new System.Windows.Forms.Label();
			this.txtRebackDemo = new System.Windows.Forms.TextBox();
			this.txtTreatmentDemo = new System.Windows.Forms.TextBox();
			this.chbTreatment9 = new System.Windows.Forms.CheckBox();
			this.chbTreatment6 = new System.Windows.Forms.CheckBox();
			this.chbTreatment5 = new System.Windows.Forms.CheckBox();
			this.chbTreatment7 = new System.Windows.Forms.CheckBox();
			this.chbTreatment4 = new System.Windows.Forms.CheckBox();
			this.chbTreatment3 = new System.Windows.Forms.CheckBox();
			this.chbTreatment2 = new System.Windows.Forms.CheckBox();
			this.chbTreatment1 = new System.Windows.Forms.CheckBox();
			this.chbDiagnoses18 = new System.Windows.Forms.CheckBox();
			this.chbDiagnoses17 = new System.Windows.Forms.CheckBox();
			this.chbDiagnoses19 = new System.Windows.Forms.CheckBox();
			this.chbDiagnoses15 = new System.Windows.Forms.CheckBox();
			this.chbTreatment8 = new System.Windows.Forms.CheckBox();
			this.chbDiagnoses14 = new System.Windows.Forms.CheckBox();
			this.chbDiagnoses13 = new System.Windows.Forms.CheckBox();
			this.chbDiagnoses12 = new System.Windows.Forms.CheckBox();
			this.chbDiagnoses11 = new System.Windows.Forms.CheckBox();
			this.chbDiagnoses10 = new System.Windows.Forms.CheckBox();
			this.chbDiagnoses9 = new System.Windows.Forms.CheckBox();
			this.chbDiagnoses7 = new System.Windows.Forms.CheckBox();
			this.chbDiagnoses6 = new System.Windows.Forms.CheckBox();
			this.chbDiagnoses5 = new System.Windows.Forms.CheckBox();
			this.chbDiagnoses4 = new System.Windows.Forms.CheckBox();
			this.chbDiagnoses3 = new System.Windows.Forms.CheckBox();
			this.chbDiagnoses2 = new System.Windows.Forms.CheckBox();
			this.chbDiagnoses1 = new System.Windows.Forms.CheckBox();
			this.label18 = new System.Windows.Forms.Label();
			this.label38 = new System.Windows.Forms.Label();
			this.label36 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.label34 = new System.Windows.Forms.Label();
			this.label33 = new System.Windows.Forms.Label();
			this.label31 = new System.Windows.Forms.Label();
			this.label32 = new System.Windows.Forms.Label();
			this.label29 = new System.Windows.Forms.Label();
			this.label28 = new System.Windows.Forms.Label();
			this.txtClinicalN = new System.Windows.Forms.TextBox();
			this.label26 = new System.Windows.Forms.Label();
			this.txtClinicalT = new System.Windows.Forms.TextBox();
			this.label64 = new System.Windows.Forms.Label();
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this.label16 = new System.Windows.Forms.Label();
			this.label25 = new System.Windows.Forms.Label();
			this.label23 = new System.Windows.Forms.Label();
			this.label22 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.txtPost = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.txtRegisterCounty = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.txtRegisterProvince = new System.Windows.Forms.TextBox();
			this.txtRegisterHouseNO = new System.Windows.Forms.TextBox();
			this.txtRegisterTown = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.txtHandPhone = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtDistrict = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label45 = new System.Windows.Forms.Label();
			this.lbSexStar = new System.Windows.Forms.Label();
			this.label49 = new System.Windows.Forms.Label();
			this.txtTelephone = new System.Windows.Forms.TextBox();
			this.label57 = new System.Windows.Forms.Label();
			this.label61 = new System.Windows.Forms.Label();
			this.cbxWomen = new System.Windows.Forms.CheckBox();
			this.cbxMan = new System.Windows.Forms.CheckBox();
			this.lbSex = new System.Windows.Forms.Label();
			this.label65 = new System.Windows.Forms.Label();
			this.dtBirthDay = new System.Windows.Forms.DateTimePicker();
			this.cmbInfectionClass = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
			this.label48 = new System.Windows.Forms.Label();
			this.label30 = new System.Windows.Forms.Label();
			this.lb_patientType = new System.Windows.Forms.Label();
			this.label46 = new System.Windows.Forms.Label();
			this.label67 = new System.Windows.Forms.Label();
			this.label24 = new System.Windows.Forms.Label();
			this.lbMadicalCardNo = new System.Windows.Forms.Label();
			this.rdbMonth = new System.Windows.Forms.RadioButton();
			this.rdbYear = new System.Windows.Forms.RadioButton();
			this.label59 = new System.Windows.Forms.Label();
			this.txtAge = new System.Windows.Forms.TextBox();
			this.label60 = new System.Windows.Forms.Label();
			this.label58 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.rdbDay = new System.Windows.Forms.RadioButton();
			this.label50 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.txtWorkPlace = new System.Windows.Forms.TextBox();
			this.label51 = new System.Windows.Forms.Label();
			this.txtCase = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.txtWorkName = new System.Windows.Forms.TextBox();
			this.label35 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label37 = new System.Windows.Forms.Label();
			this.label39 = new System.Windows.Forms.Label();
			this.label41 = new System.Windows.Forms.Label();
			this.helpProvider1 = new System.Windows.Forms.HelpProvider();
			this.panel2.SuspendLayout();
			this.Printpanel.SuspendLayout();
			this.panelTitle.SuspendLayout();
			this.panelPatientInfo.SuspendLayout();
			this.panel8.SuspendLayout();
			this.panel5.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel4.SuspendLayout();
			this.SuspendLayout();
			// 
			// lbTitle
			// 
			this.lbTitle.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.lbTitle.Location = new System.Drawing.Point(200, 40);
			this.lbTitle.Name = "lbTitle";
			this.lbTitle.Size = new System.Drawing.Size(281, 22);
			this.lbTitle.TabIndex = 4;
			this.lbTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// panel6
			// 
			this.panel6.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel6.Location = new System.Drawing.Point(0, 0);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(8, 1056);
			this.panel6.TabIndex = 2;
			// 
			// panel7
			// 
			this.panel7.Dock = System.Windows.Forms.DockStyle.Right;
			this.panel7.Location = new System.Drawing.Point(784, 0);
			this.panel7.Name = "panel7";
			this.panel7.Size = new System.Drawing.Size(8, 1056);
			this.panel7.TabIndex = 3;
			// 
			// panel1
			// 
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(8, 1048);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(776, 8);
			this.panel1.TabIndex = 4;
			// 
			// panel2
			// 
			this.panel2.AutoScroll = true;
			this.panel2.Controls.Add(this.Printpanel);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(8, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(776, 1048);
			this.panel2.TabIndex = 5;
			// 
			// Printpanel
			// 
			this.Printpanel.AutoScroll = true;
			this.Printpanel.BackColor = System.Drawing.Color.White;
			this.Printpanel.Controls.Add(this.panelTitle);
			this.Printpanel.Controls.Add(this.panelPatientInfo);
			this.Printpanel.Controls.Add(this.rdbMonth);
			this.Printpanel.Controls.Add(this.rdbYear);
			this.Printpanel.Controls.Add(this.label59);
			this.Printpanel.Controls.Add(this.txtAge);
			this.Printpanel.Controls.Add(this.label60);
			this.Printpanel.Controls.Add(this.label58);
			this.Printpanel.Controls.Add(this.label3);
			this.Printpanel.Controls.Add(this.rdbDay);
			this.Printpanel.Controls.Add(this.label50);
			this.Printpanel.Controls.Add(this.label1);
			this.Printpanel.Controls.Add(this.txtWorkPlace);
			this.Printpanel.Controls.Add(this.label51);
			this.Printpanel.Controls.Add(this.txtCase);
			this.Printpanel.Controls.Add(this.label8);
			this.Printpanel.Controls.Add(this.txtWorkName);
			this.Printpanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Printpanel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.Printpanel.Location = new System.Drawing.Point(0, 0);
			this.Printpanel.Name = "Printpanel";
			this.Printpanel.Size = new System.Drawing.Size(776, 1048);
			this.Printpanel.TabIndex = 6;
			// 
			// panelTitle
			// 
			this.panelTitle.BackColor = System.Drawing.Color.White;
			this.panelTitle.Controls.Add(this.txtCancer_No);
			this.panelTitle.Controls.Add(this.label27);
			this.panelTitle.Controls.Add(this.lbID);
			this.panelTitle.Controls.Add(this.lbReportNO);
			this.panelTitle.Controls.Add(this.lbState);
			this.panelTitle.Controls.Add(this.groupBox2);
			this.panelTitle.Controls.Add(this.label66);
			this.panelTitle.Controls.Add(this.lbPatientDept);
			this.panelTitle.Controls.Add(this.label69);
			this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelTitle.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.panelTitle.Location = new System.Drawing.Point(0, 0);
			this.panelTitle.Name = "panelTitle";
			this.panelTitle.Size = new System.Drawing.Size(784, 88);
			this.panelTitle.TabIndex = 39;
			// 
			// txtCancer_No
			// 
			this.txtCancer_No.Enabled = false;
			this.txtCancer_No.Location = new System.Drawing.Point(32, 64);
			this.txtCancer_No.Name = "txtCancer_No";
			this.txtCancer_No.Size = new System.Drawing.Size(128, 23);
			this.txtCancer_No.TabIndex = 100378;
			this.txtCancer_No.Text = "";
			// 
			// label27
			// 
			this.label27.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label27.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(128)), ((System.Byte)(128)));
			this.label27.Location = new System.Drawing.Point(0, 64);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(48, 23);
			this.label27.TabIndex = 100378;
			this.label27.Text = "NO：";
			this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lbID
			// 
			this.lbID.Location = new System.Drawing.Point(312, 64);
			this.lbID.Name = "lbID";
			this.lbID.Size = new System.Drawing.Size(120, 23);
			this.lbID.TabIndex = 35;
			this.lbID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lbReportNO
			// 
			this.lbReportNO.Location = new System.Drawing.Point(248, 64);
			this.lbReportNO.Name = "lbReportNO";
			this.lbReportNO.Size = new System.Drawing.Size(80, 23);
			this.lbReportNO.TabIndex = 36;
			this.lbReportNO.Text = "卡片编号：";
			this.lbReportNO.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lbState
			// 
			this.lbState.BackColor = System.Drawing.Color.White;
			this.lbState.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.lbState.Location = new System.Drawing.Point(512, 64);
			this.lbState.Name = "lbState";
			this.lbState.Size = new System.Drawing.Size(208, 23);
			this.lbState.TabIndex = 19;
			this.lbState.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// groupBox2
			// 
			this.groupBox2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.groupBox2.Location = new System.Drawing.Point(26, 88);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(700, 2);
			this.groupBox2.TabIndex = 34;
			this.groupBox2.TabStop = false;
			// 
			// label66
			// 
			this.label66.BackColor = System.Drawing.Color.White;
			this.label66.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label66.Location = new System.Drawing.Point(432, 64);
			this.label66.Name = "label66";
			this.label66.Size = new System.Drawing.Size(80, 23);
			this.label66.TabIndex = 21;
			this.label66.Text = "报卡类型: ";
			this.label66.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// lbPatientDept
			// 
			this.lbPatientDept.Location = new System.Drawing.Point(576, 16);
			this.lbPatientDept.Name = "lbPatientDept";
			this.lbPatientDept.Size = new System.Drawing.Size(160, 23);
			this.lbPatientDept.TabIndex = 37;
			// 
			// label69
			// 
			this.label69.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label69.Location = new System.Drawing.Point(236, 16);
			this.label69.Name = "label69";
			this.label69.Size = new System.Drawing.Size(281, 24);
			this.label69.TabIndex = 4;
			this.label69.Text = "广州市肿瘤报告卡";
			this.label69.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panelPatientInfo
			// 
			this.panelPatientInfo.BackColor = System.Drawing.Color.White;
			this.panelPatientInfo.Controls.Add(this.txtWorkType);
			this.panelPatientInfo.Controls.Add(this.label106);
			this.panelPatientInfo.Controls.Add(this.label71);
			this.panelPatientInfo.Controls.Add(this.label99);
			this.panelPatientInfo.Controls.Add(this.label97);
			this.panelPatientInfo.Controls.Add(this.label86);
			this.panelPatientInfo.Controls.Add(this.lbPatientNo);
			this.panelPatientInfo.Controls.Add(this.cmbSource);
			this.panelPatientInfo.Controls.Add(this.label84);
			this.panelPatientInfo.Controls.Add(this.chbDiagnoses16);
			this.panelPatientInfo.Controls.Add(this.chbDiagnoses8);
			this.panelPatientInfo.Controls.Add(this.label80);
			this.panelPatientInfo.Controls.Add(this.rtxtMemo);
			this.panelPatientInfo.Controls.Add(this.label79);
			this.panelPatientInfo.Controls.Add(this.lbReportDoctor);
			this.panelPatientInfo.Controls.Add(this.label105);
			this.panelPatientInfo.Controls.Add(this.label104);
			this.panelPatientInfo.Controls.Add(this.dateTimePicker2);
			this.panelPatientInfo.Controls.Add(this.label103);
			this.panelPatientInfo.Controls.Add(this.label96);
			this.panelPatientInfo.Controls.Add(this.textBox1);
			this.panelPatientInfo.Controls.Add(this.textBox2);
			this.panelPatientInfo.Controls.Add(this.label98);
			this.panelPatientInfo.Controls.Add(this.label100);
			this.panelPatientInfo.Controls.Add(this.textBox3);
			this.panelPatientInfo.Controls.Add(this.textBox4);
			this.panelPatientInfo.Controls.Add(this.textBox5);
			this.panelPatientInfo.Controls.Add(this.label101);
			this.panelPatientInfo.Controls.Add(this.label102);
			this.panelPatientInfo.Controls.Add(this.label95);
			this.panelPatientInfo.Controls.Add(this.label94);
			this.panelPatientInfo.Controls.Add(this.label93);
			this.panelPatientInfo.Controls.Add(this.label92);
			this.panelPatientInfo.Controls.Add(this.label91);
			this.panelPatientInfo.Controls.Add(this.label90);
			this.panelPatientInfo.Controls.Add(this.label89);
			this.panelPatientInfo.Controls.Add(this.label88);
			this.panelPatientInfo.Controls.Add(this.label87);
			this.panelPatientInfo.Controls.Add(this.label85);
			this.panelPatientInfo.Controls.Add(this.label83);
			this.panelPatientInfo.Controls.Add(this.label82);
			this.panelPatientInfo.Controls.Add(this.label81);
			this.panelPatientInfo.Controls.Add(this.label78);
			this.panelPatientInfo.Controls.Add(this.label77);
			this.panelPatientInfo.Controls.Add(this.label76);
			this.panelPatientInfo.Controls.Add(this.label75);
			this.panelPatientInfo.Controls.Add(this.label74);
			this.panelPatientInfo.Controls.Add(this.label73);
			this.panelPatientInfo.Controls.Add(this.label72);
			this.panelPatientInfo.Controls.Add(this.label70);
			this.panelPatientInfo.Controls.Add(this.label68);
			this.panelPatientInfo.Controls.Add(this.label62);
			this.panelPatientInfo.Controls.Add(this.label56);
			this.panelPatientInfo.Controls.Add(this.label55);
			this.panelPatientInfo.Controls.Add(this.label54);
			this.panelPatientInfo.Controls.Add(this.label53);
			this.panelPatientInfo.Controls.Add(this.label52);
			this.panelPatientInfo.Controls.Add(this.panel8);
			this.panelPatientInfo.Controls.Add(this.panel5);
			this.panelPatientInfo.Controls.Add(this.panel3);
			this.panelPatientInfo.Controls.Add(this.txtClinicalM);
			this.panelPatientInfo.Controls.Add(this.panel4);
			this.panelPatientInfo.Controls.Add(this.label47);
			this.panelPatientInfo.Controls.Add(this.cmbMarrige);
			this.panelPatientInfo.Controls.Add(this.label11);
			this.panelPatientInfo.Controls.Add(this.txtPatientID);
			this.panelPatientInfo.Controls.Add(this.cmbProfession);
			this.panelPatientInfo.Controls.Add(this.txtSpecialAddress);
			this.panelPatientInfo.Controls.Add(this.txtContactPerson);
			this.panelPatientInfo.Controls.Add(this.txtRelationship);
			this.panelPatientInfo.Controls.Add(this.txtContactPersonTel);
			this.panelPatientInfo.Controls.Add(this.txtContactPersonAddr);
			this.panelPatientInfo.Controls.Add(this.txtICD);
			this.panelPatientInfo.Controls.Add(this.txtPathlogyNo);
			this.panelPatientInfo.Controls.Add(this.txtPathlogyType);
			this.panelPatientInfo.Controls.Add(this.txtICDO);
			this.panelPatientInfo.Controls.Add(this.txtPathlogyDegree);
			this.panelPatientInfo.Controls.Add(this.txtOldDiagnoses);
			this.panelPatientInfo.Controls.Add(this.txtDiagnosesDemo);
			this.panelPatientInfo.Controls.Add(this.txtPatientName);
			this.panelPatientInfo.Controls.Add(this.label40);
			this.panelPatientInfo.Controls.Add(this.label42);
			this.panelPatientInfo.Controls.Add(this.label44);
			this.panelPatientInfo.Controls.Add(this.lbDoctorDept);
			this.panelPatientInfo.Controls.Add(this.lbReportTime);
			this.panelPatientInfo.Controls.Add(this.label63);
			this.panelPatientInfo.Controls.Add(this.cmbNation);
			this.panelPatientInfo.Controls.Add(this.txtRegisterCity);
			this.panelPatientInfo.Controls.Add(this.dtOldDiagnosesTime);
			this.panelPatientInfo.Controls.Add(this.txtDeathResion);
			this.panelPatientInfo.Controls.Add(this.label17);
			this.panelPatientInfo.Controls.Add(this.label43);
			this.panelPatientInfo.Controls.Add(this.txtRebackDemo);
			this.panelPatientInfo.Controls.Add(this.txtTreatmentDemo);
			this.panelPatientInfo.Controls.Add(this.chbTreatment9);
			this.panelPatientInfo.Controls.Add(this.chbTreatment6);
			this.panelPatientInfo.Controls.Add(this.chbTreatment5);
			this.panelPatientInfo.Controls.Add(this.chbTreatment7);
			this.panelPatientInfo.Controls.Add(this.chbTreatment4);
			this.panelPatientInfo.Controls.Add(this.chbTreatment3);
			this.panelPatientInfo.Controls.Add(this.chbTreatment2);
			this.panelPatientInfo.Controls.Add(this.chbTreatment1);
			this.panelPatientInfo.Controls.Add(this.chbDiagnoses18);
			this.panelPatientInfo.Controls.Add(this.chbDiagnoses17);
			this.panelPatientInfo.Controls.Add(this.chbDiagnoses19);
			this.panelPatientInfo.Controls.Add(this.chbDiagnoses15);
			this.panelPatientInfo.Controls.Add(this.chbTreatment8);
			this.panelPatientInfo.Controls.Add(this.chbDiagnoses14);
			this.panelPatientInfo.Controls.Add(this.chbDiagnoses13);
			this.panelPatientInfo.Controls.Add(this.chbDiagnoses12);
			this.panelPatientInfo.Controls.Add(this.chbDiagnoses11);
			this.panelPatientInfo.Controls.Add(this.chbDiagnoses10);
			this.panelPatientInfo.Controls.Add(this.chbDiagnoses9);
			this.panelPatientInfo.Controls.Add(this.chbDiagnoses7);
			this.panelPatientInfo.Controls.Add(this.chbDiagnoses6);
			this.panelPatientInfo.Controls.Add(this.chbDiagnoses5);
			this.panelPatientInfo.Controls.Add(this.chbDiagnoses4);
			this.panelPatientInfo.Controls.Add(this.chbDiagnoses3);
			this.panelPatientInfo.Controls.Add(this.chbDiagnoses2);
			this.panelPatientInfo.Controls.Add(this.chbDiagnoses1);
			this.panelPatientInfo.Controls.Add(this.label18);
			this.panelPatientInfo.Controls.Add(this.label38);
			this.panelPatientInfo.Controls.Add(this.label36);
			this.panelPatientInfo.Controls.Add(this.label19);
			this.panelPatientInfo.Controls.Add(this.label34);
			this.panelPatientInfo.Controls.Add(this.label33);
			this.panelPatientInfo.Controls.Add(this.label31);
			this.panelPatientInfo.Controls.Add(this.label32);
			this.panelPatientInfo.Controls.Add(this.label29);
			this.panelPatientInfo.Controls.Add(this.label28);
			this.panelPatientInfo.Controls.Add(this.txtClinicalN);
			this.panelPatientInfo.Controls.Add(this.label26);
			this.panelPatientInfo.Controls.Add(this.txtClinicalT);
			this.panelPatientInfo.Controls.Add(this.label64);
			this.panelPatientInfo.Controls.Add(this.dateTimePicker1);
			this.panelPatientInfo.Controls.Add(this.label16);
			this.panelPatientInfo.Controls.Add(this.label25);
			this.panelPatientInfo.Controls.Add(this.label23);
			this.panelPatientInfo.Controls.Add(this.label22);
			this.panelPatientInfo.Controls.Add(this.label21);
			this.panelPatientInfo.Controls.Add(this.label20);
			this.panelPatientInfo.Controls.Add(this.label14);
			this.panelPatientInfo.Controls.Add(this.txtPost);
			this.panelPatientInfo.Controls.Add(this.label15);
			this.panelPatientInfo.Controls.Add(this.txtRegisterCounty);
			this.panelPatientInfo.Controls.Add(this.label5);
			this.panelPatientInfo.Controls.Add(this.label7);
			this.panelPatientInfo.Controls.Add(this.txtRegisterProvince);
			this.panelPatientInfo.Controls.Add(this.txtRegisterHouseNO);
			this.panelPatientInfo.Controls.Add(this.txtRegisterTown);
			this.panelPatientInfo.Controls.Add(this.label10);
			this.panelPatientInfo.Controls.Add(this.label12);
			this.panelPatientInfo.Controls.Add(this.label13);
			this.panelPatientInfo.Controls.Add(this.txtHandPhone);
			this.panelPatientInfo.Controls.Add(this.label4);
			this.panelPatientInfo.Controls.Add(this.txtDistrict);
			this.panelPatientInfo.Controls.Add(this.label2);
			this.panelPatientInfo.Controls.Add(this.label9);
			this.panelPatientInfo.Controls.Add(this.label45);
			this.panelPatientInfo.Controls.Add(this.lbSexStar);
			this.panelPatientInfo.Controls.Add(this.label49);
			this.panelPatientInfo.Controls.Add(this.txtTelephone);
			this.panelPatientInfo.Controls.Add(this.label57);
			this.panelPatientInfo.Controls.Add(this.label61);
			this.panelPatientInfo.Controls.Add(this.cbxWomen);
			this.panelPatientInfo.Controls.Add(this.cbxMan);
			this.panelPatientInfo.Controls.Add(this.lbSex);
			this.panelPatientInfo.Controls.Add(this.label65);
			this.panelPatientInfo.Controls.Add(this.dtBirthDay);
			this.panelPatientInfo.Controls.Add(this.cmbInfectionClass);
			this.panelPatientInfo.Controls.Add(this.label48);
			this.panelPatientInfo.Controls.Add(this.label30);
			this.panelPatientInfo.Controls.Add(this.lb_patientType);
			this.panelPatientInfo.Controls.Add(this.label46);
			this.panelPatientInfo.Controls.Add(this.label67);
			this.panelPatientInfo.Controls.Add(this.label24);
			this.panelPatientInfo.Controls.Add(this.lbMadicalCardNo);
			this.panelPatientInfo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.panelPatientInfo.Location = new System.Drawing.Point(-16, 88);
			this.panelPatientInfo.Name = "panelPatientInfo";
			this.panelPatientInfo.Size = new System.Drawing.Size(800, 920);
			this.panelPatientInfo.TabIndex = 38;
			// 
			// txtWorkType
			// 
			this.txtWorkType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtWorkType.Enabled = false;
			this.txtWorkType.Location = new System.Drawing.Point(584, 248);
			this.txtWorkType.Name = "txtWorkType";
			this.txtWorkType.Size = new System.Drawing.Size(176, 23);
			this.txtWorkType.TabIndex = 100439;
			this.txtWorkType.Text = "";
			// 
			// label106
			// 
			this.label106.ForeColor = System.Drawing.Color.Red;
			this.label106.Location = new System.Drawing.Point(568, 248);
			this.label106.Name = "label106";
			this.label106.Size = new System.Drawing.Size(8, 16);
			this.label106.TabIndex = 100438;
			this.label106.Text = "*";
			this.label106.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label71
			// 
			this.label71.ForeColor = System.Drawing.Color.Red;
			this.label71.Location = new System.Drawing.Point(16, 448);
			this.label71.Name = "label71";
			this.label71.Size = new System.Drawing.Size(8, 23);
			this.label71.TabIndex = 100437;
			this.label71.Text = "*";
			this.label71.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label99
			// 
			this.label99.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label99.Location = new System.Drawing.Point(32, 448);
			this.label99.Name = "label99";
			this.label99.Size = new System.Drawing.Size(72, 23);
			this.label99.TabIndex = 100436;
			this.label99.Text = "门 牌 号:";
			this.label99.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label97
			// 
			this.label97.ForeColor = System.Drawing.Color.Red;
			this.label97.Location = new System.Drawing.Point(16, 536);
			this.label97.Name = "label97";
			this.label97.Size = new System.Drawing.Size(8, 23);
			this.label97.TabIndex = 100435;
			this.label97.Text = "*";
			this.label97.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label86
			// 
			this.label86.ForeColor = System.Drawing.Color.Red;
			this.label86.Location = new System.Drawing.Point(16, 560);
			this.label86.Name = "label86";
			this.label86.Size = new System.Drawing.Size(8, 23);
			this.label86.TabIndex = 100434;
			this.label86.Text = "*";
			this.label86.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lbPatientNo
			// 
			this.lbPatientNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lbPatientNo.Location = new System.Drawing.Point(112, 48);
			this.lbPatientNo.Name = "lbPatientNo";
			this.lbPatientNo.Size = new System.Drawing.Size(192, 23);
			this.lbPatientNo.TabIndex = 100433;
			this.lbPatientNo.Text = "";
			this.lbPatientNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbPatientNo_KeyDown);
			// 
			// cmbSource
			// 
			this.cmbSource.ArrowBackColor = System.Drawing.Color.Silver;
			this.cmbSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbSource.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.cmbSource.IsFlat = false;
			this.cmbSource.IsLike = true;
			this.cmbSource.ItemHeight = 14;
			this.cmbSource.Items.AddRange(new object[] {
														   "门诊",
														   "住院"});
			this.cmbSource.Location = new System.Drawing.Point(112, 16);
			this.cmbSource.MaxDropDownItems = 21;
			this.cmbSource.Name = "cmbSource";
			this.cmbSource.PopForm = null;
			this.cmbSource.ShowCustomerList = false;
			this.cmbSource.ShowID = false;
			this.cmbSource.Size = new System.Drawing.Size(192, 22);
			this.cmbSource.TabIndex = 100432;
			this.cmbSource.Tag = "";
			this.cmbSource.SelectedIndexChanged += new System.EventHandler(this.cmbSource_SelectedIndexChanged);
			// 
			// label84
			// 
			this.label84.BackColor = System.Drawing.Color.White;
			this.label84.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label84.Location = new System.Drawing.Point(32, 744);
			this.label84.Name = "label84";
			this.label84.Size = new System.Drawing.Size(72, 23);
			this.label84.TabIndex = 100431;
			this.label84.Text = "批意见:";
			this.label84.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// chbDiagnoses16
			// 
			this.chbDiagnoses16.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbDiagnoses16.Location = new System.Drawing.Point(696, 384);
			this.chbDiagnoses16.Name = "chbDiagnoses16";
			this.chbDiagnoses16.Size = new System.Drawing.Size(88, 24);
			this.chbDiagnoses16.TabIndex = 100348;
			this.chbDiagnoses16.TabStop = false;
			this.chbDiagnoses16.Text = "死亡补发病";
			// 
			// chbDiagnoses8
			// 
			this.chbDiagnoses8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbDiagnoses8.Location = new System.Drawing.Point(600, 384);
			this.chbDiagnoses8.Name = "chbDiagnoses8";
			this.chbDiagnoses8.Size = new System.Drawing.Size(105, 24);
			this.chbDiagnoses8.TabIndex = 100339;
			this.chbDiagnoses8.TabStop = false;
			this.chbDiagnoses8.Text = "尸检(无病理)";
			// 
			// label80
			// 
			this.label80.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label80.Location = new System.Drawing.Point(576, 296);
			this.label80.Name = "label80";
			this.label80.Size = new System.Drawing.Size(80, 16);
			this.label80.TabIndex = 100430;
			this.label80.Text = "学编码:";
			this.label80.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// rtxtMemo
			// 
			this.rtxtMemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.rtxtMemo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.rtxtMemo.Location = new System.Drawing.Point(112, 720);
			this.rtxtMemo.Name = "rtxtMemo";
			this.rtxtMemo.Size = new System.Drawing.Size(656, 144);
			this.rtxtMemo.TabIndex = 100379;
			this.rtxtMemo.TabStop = false;
			this.rtxtMemo.Text = "";
			// 
			// label79
			// 
			this.label79.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label79.Location = new System.Drawing.Point(112, 720);
			this.label79.Name = "label79";
			this.label79.Size = new System.Drawing.Size(656, 144);
			this.label79.TabIndex = 100429;
			// 
			// lbReportDoctor
			// 
			this.lbReportDoctor.BackColor = System.Drawing.Color.White;
			this.lbReportDoctor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lbReportDoctor.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.lbReportDoctor.Location = new System.Drawing.Point(424, 688);
			this.lbReportDoctor.Name = "lbReportDoctor";
			this.lbReportDoctor.Size = new System.Drawing.Size(144, 24);
			this.lbReportDoctor.TabIndex = 1;
			this.lbReportDoctor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label105
			// 
			this.label105.ForeColor = System.Drawing.Color.Red;
			this.label105.Location = new System.Drawing.Point(568, 216);
			this.label105.Name = "label105";
			this.label105.Size = new System.Drawing.Size(8, 16);
			this.label105.TabIndex = 100428;
			this.label105.Text = "*";
			this.label105.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label104
			// 
			this.label104.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label104.Location = new System.Drawing.Point(584, 216);
			this.label104.Name = "label104";
			this.label104.Size = new System.Drawing.Size(112, 24);
			this.label104.TabIndex = 100427;
			this.label104.Text = "病理诊断中文:";
			this.label104.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dateTimePicker2
			// 
			this.dateTimePicker2.Location = new System.Drawing.Point(648, 688);
			this.dateTimePicker2.Name = "dateTimePicker2";
			this.dateTimePicker2.Size = new System.Drawing.Size(120, 23);
			this.dateTimePicker2.TabIndex = 100426;
			this.dateTimePicker2.Value = new System.DateTime(2009, 12, 1, 10, 41, 17, 609);
			// 
			// label103
			// 
			this.label103.BackColor = System.Drawing.Color.White;
			this.label103.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label103.Location = new System.Drawing.Point(576, 688);
			this.label103.Name = "label103";
			this.label103.Size = new System.Drawing.Size(72, 23);
			this.label103.TabIndex = 100425;
			this.label103.Text = "出院日期:";
			this.label103.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label96
			// 
			this.label96.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label96.Location = new System.Drawing.Point(32, 560);
			this.label96.Name = "label96";
			this.label96.Size = new System.Drawing.Size(72, 23);
			this.label96.TabIndex = 100424;
			this.label96.Text = "门 牌 号:";
			this.label96.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textBox1
			// 
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox1.Location = new System.Drawing.Point(200, 512);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(64, 23);
			this.textBox1.TabIndex = 100421;
			this.textBox1.Text = "";
			// 
			// textBox2
			// 
			this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox2.Location = new System.Drawing.Point(112, 536);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(64, 23);
			this.textBox2.TabIndex = 100415;
			this.textBox2.Text = "";
			// 
			// label98
			// 
			this.label98.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label98.Location = new System.Drawing.Point(280, 512);
			this.label98.Name = "label98";
			this.label98.Size = new System.Drawing.Size(24, 23);
			this.label98.TabIndex = 100423;
			this.label98.Text = "市 ";
			this.label98.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label100
			// 
			this.label100.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label100.Location = new System.Drawing.Point(176, 512);
			this.label100.Name = "label100";
			this.label100.Size = new System.Drawing.Size(24, 23);
			this.label100.TabIndex = 100422;
			this.label100.Text = "省 ";
			this.label100.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textBox3
			// 
			this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox3.Location = new System.Drawing.Point(112, 512);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(64, 23);
			this.textBox3.TabIndex = 100420;
			this.textBox3.Text = "";
			// 
			// textBox4
			// 
			this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox4.Location = new System.Drawing.Point(112, 560);
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(192, 23);
			this.textBox4.TabIndex = 100417;
			this.textBox4.Text = "";
			// 
			// textBox5
			// 
			this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox5.Location = new System.Drawing.Point(224, 536);
			this.textBox5.Name = "textBox5";
			this.textBox5.Size = new System.Drawing.Size(48, 23);
			this.textBox5.TabIndex = 100416;
			this.textBox5.Text = "";
			// 
			// label101
			// 
			this.label101.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label101.Location = new System.Drawing.Point(272, 536);
			this.label101.Name = "label101";
			this.label101.Size = new System.Drawing.Size(64, 23);
			this.label101.TabIndex = 100419;
			this.label101.Text = "街道(镇)";
			this.label101.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label102
			// 
			this.label102.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label102.Location = new System.Drawing.Point(176, 536);
			this.label102.Name = "label102";
			this.label102.Size = new System.Drawing.Size(48, 23);
			this.label102.TabIndex = 100418;
			this.label102.Text = "区(县)";
			this.label102.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label95
			// 
			this.label95.ForeColor = System.Drawing.Color.Red;
			this.label95.Location = new System.Drawing.Point(16, 512);
			this.label95.Name = "label95";
			this.label95.Size = new System.Drawing.Size(8, 23);
			this.label95.TabIndex = 100414;
			this.label95.Text = "*";
			this.label95.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label94
			// 
			this.label94.ForeColor = System.Drawing.Color.Red;
			this.label94.Location = new System.Drawing.Point(336, 16);
			this.label94.Name = "label94";
			this.label94.Size = new System.Drawing.Size(8, 16);
			this.label94.TabIndex = 100413;
			this.label94.Text = "*";
			this.label94.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label93
			// 
			this.label93.ForeColor = System.Drawing.Color.Red;
			this.label93.Location = new System.Drawing.Point(336, 48);
			this.label93.Name = "label93";
			this.label93.Size = new System.Drawing.Size(8, 16);
			this.label93.TabIndex = 100412;
			this.label93.Text = "*";
			this.label93.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label92
			// 
			this.label92.ForeColor = System.Drawing.Color.Red;
			this.label92.Location = new System.Drawing.Point(336, 80);
			this.label92.Name = "label92";
			this.label92.Size = new System.Drawing.Size(8, 16);
			this.label92.TabIndex = 100411;
			this.label92.Text = "*";
			this.label92.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label91
			// 
			this.label91.ForeColor = System.Drawing.Color.Red;
			this.label91.Location = new System.Drawing.Point(336, 112);
			this.label91.Name = "label91";
			this.label91.Size = new System.Drawing.Size(8, 16);
			this.label91.TabIndex = 100410;
			this.label91.Text = "*";
			this.label91.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label90
			// 
			this.label90.ForeColor = System.Drawing.Color.Red;
			this.label90.Location = new System.Drawing.Point(336, 184);
			this.label90.Name = "label90";
			this.label90.Size = new System.Drawing.Size(8, 16);
			this.label90.TabIndex = 100409;
			this.label90.Text = "*";
			this.label90.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label89
			// 
			this.label89.ForeColor = System.Drawing.Color.Red;
			this.label89.Location = new System.Drawing.Point(336, 216);
			this.label89.Name = "label89";
			this.label89.Size = new System.Drawing.Size(8, 16);
			this.label89.TabIndex = 100408;
			this.label89.Text = "*";
			this.label89.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label88
			// 
			this.label88.ForeColor = System.Drawing.Color.Red;
			this.label88.Location = new System.Drawing.Point(336, 248);
			this.label88.Name = "label88";
			this.label88.Size = new System.Drawing.Size(8, 16);
			this.label88.TabIndex = 100407;
			this.label88.Text = "*";
			this.label88.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label87
			// 
			this.label87.ForeColor = System.Drawing.Color.Red;
			this.label87.Location = new System.Drawing.Point(568, 280);
			this.label87.Name = "label87";
			this.label87.Size = new System.Drawing.Size(8, 16);
			this.label87.TabIndex = 100406;
			this.label87.Text = "*";
			this.label87.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label85
			// 
			this.label85.ForeColor = System.Drawing.Color.Red;
			this.label85.Location = new System.Drawing.Point(336, 280);
			this.label85.Name = "label85";
			this.label85.Size = new System.Drawing.Size(8, 16);
			this.label85.TabIndex = 100405;
			this.label85.Text = "*";
			this.label85.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label83
			// 
			this.label83.ForeColor = System.Drawing.Color.Red;
			this.label83.Location = new System.Drawing.Point(568, 656);
			this.label83.Name = "label83";
			this.label83.Size = new System.Drawing.Size(8, 16);
			this.label83.TabIndex = 100404;
			this.label83.Text = "*";
			this.label83.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label82
			// 
			this.label82.ForeColor = System.Drawing.Color.Red;
			this.label82.Location = new System.Drawing.Point(336, 656);
			this.label82.Name = "label82";
			this.label82.Size = new System.Drawing.Size(8, 16);
			this.label82.TabIndex = 100403;
			this.label82.Text = "*";
			this.label82.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label81
			// 
			this.label81.ForeColor = System.Drawing.Color.Red;
			this.label81.Location = new System.Drawing.Point(336, 592);
			this.label81.Name = "label81";
			this.label81.Size = new System.Drawing.Size(8, 16);
			this.label81.TabIndex = 100402;
			this.label81.Text = "*";
			this.label81.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label78
			// 
			this.label78.ForeColor = System.Drawing.Color.Red;
			this.label78.Location = new System.Drawing.Point(336, 496);
			this.label78.Name = "label78";
			this.label78.Size = new System.Drawing.Size(8, 16);
			this.label78.TabIndex = 100401;
			this.label78.Text = "*";
			this.label78.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label77
			// 
			this.label77.ForeColor = System.Drawing.Color.Red;
			this.label77.Location = new System.Drawing.Point(336, 472);
			this.label77.Name = "label77";
			this.label77.Size = new System.Drawing.Size(8, 16);
			this.label77.TabIndex = 100400;
			this.label77.Text = "*";
			this.label77.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label76
			// 
			this.label76.ForeColor = System.Drawing.Color.Red;
			this.label76.Location = new System.Drawing.Point(336, 312);
			this.label76.Name = "label76";
			this.label76.Size = new System.Drawing.Size(8, 16);
			this.label76.TabIndex = 100399;
			this.label76.Text = "*";
			this.label76.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label75
			// 
			this.label75.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label75.Location = new System.Drawing.Point(352, 632);
			this.label75.Name = "label75";
			this.label75.Size = new System.Drawing.Size(72, 16);
			this.label75.TabIndex = 100398;
			this.label75.Text = "(请注明):";
			this.label75.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label74
			// 
			this.label74.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label74.Location = new System.Drawing.Point(352, 560);
			this.label74.Name = "label74";
			this.label74.Size = new System.Drawing.Size(72, 16);
			this.label74.TabIndex = 100397;
			this.label74.Text = "(请注明):";
			this.label74.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label73
			// 
			this.label73.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label73.Location = new System.Drawing.Point(352, 440);
			this.label73.Name = "label73";
			this.label73.Size = new System.Drawing.Size(72, 16);
			this.label73.TabIndex = 100396;
			this.label73.Text = "(请注明):";
			this.label73.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label72
			// 
			this.label72.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label72.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label72.Location = new System.Drawing.Point(224, 480);
			this.label72.Name = "label72";
			this.label72.Size = new System.Drawing.Size(96, 23);
			this.label72.TabIndex = 100395;
			this.label72.Text = "复制到常用地址";
			this.label72.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label72.Click += new System.EventHandler(this.label72_Click);
			// 
			// label70
			// 
			this.label70.ForeColor = System.Drawing.Color.Red;
			this.label70.Location = new System.Drawing.Point(16, 400);
			this.label70.Name = "label70";
			this.label70.Size = new System.Drawing.Size(8, 23);
			this.label70.TabIndex = 100393;
			this.label70.Text = "*";
			this.label70.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label68
			// 
			this.label68.ForeColor = System.Drawing.Color.Red;
			this.label68.Location = new System.Drawing.Point(16, 336);
			this.label68.Name = "label68";
			this.label68.Size = new System.Drawing.Size(8, 23);
			this.label68.TabIndex = 100392;
			this.label68.Text = "*";
			this.label68.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label62
			// 
			this.label62.ForeColor = System.Drawing.Color.Red;
			this.label62.Location = new System.Drawing.Point(16, 304);
			this.label62.Name = "label62";
			this.label62.Size = new System.Drawing.Size(8, 23);
			this.label62.TabIndex = 100391;
			this.label62.Text = "*";
			this.label62.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label56
			// 
			this.label56.ForeColor = System.Drawing.Color.Red;
			this.label56.Location = new System.Drawing.Point(16, 272);
			this.label56.Name = "label56";
			this.label56.Size = new System.Drawing.Size(8, 23);
			this.label56.TabIndex = 100390;
			this.label56.Text = "*";
			this.label56.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label55
			// 
			this.label55.ForeColor = System.Drawing.Color.Red;
			this.label55.Location = new System.Drawing.Point(16, 176);
			this.label55.Name = "label55";
			this.label55.Size = new System.Drawing.Size(8, 23);
			this.label55.TabIndex = 100389;
			this.label55.Text = "*";
			this.label55.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label54
			// 
			this.label54.ForeColor = System.Drawing.Color.Red;
			this.label54.Location = new System.Drawing.Point(16, 112);
			this.label54.Name = "label54";
			this.label54.Size = new System.Drawing.Size(8, 23);
			this.label54.TabIndex = 100388;
			this.label54.Text = "*";
			this.label54.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label53
			// 
			this.label53.ForeColor = System.Drawing.Color.Red;
			this.label53.Location = new System.Drawing.Point(16, 48);
			this.label53.Name = "label53";
			this.label53.Size = new System.Drawing.Size(8, 23);
			this.label53.TabIndex = 100387;
			this.label53.Text = "*";
			this.label53.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label52
			// 
			this.label52.ForeColor = System.Drawing.Color.Red;
			this.label52.Location = new System.Drawing.Point(16, 16);
			this.label52.Name = "label52";
			this.label52.Size = new System.Drawing.Size(8, 23);
			this.label52.TabIndex = 100386;
			this.label52.Text = "*";
			this.label52.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// panel8
			// 
			this.panel8.Controls.Add(this.rdbReback5);
			this.panel8.Controls.Add(this.rdbReback4);
			this.panel8.Controls.Add(this.rdbReback3);
			this.panel8.Controls.Add(this.rdbReback2);
			this.panel8.Controls.Add(this.rdbReback1);
			this.panel8.Location = new System.Drawing.Point(424, 592);
			this.panel8.Name = "panel8";
			this.panel8.Size = new System.Drawing.Size(336, 24);
			this.panel8.TabIndex = 100385;
			// 
			// rdbReback5
			// 
			this.rdbReback5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.rdbReback5.Location = new System.Drawing.Point(224, 0);
			this.rdbReback5.Name = "rdbReback5";
			this.rdbReback5.Size = new System.Drawing.Size(112, 24);
			this.rdbReback5.TabIndex = 4;
			this.rdbReback5.Text = "其他(请注明)";
			this.rdbReback5.CheckedChanged += new System.EventHandler(this.rdbReback5_CheckedChanged);
			// 
			// rdbReback4
			// 
			this.rdbReback4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.rdbReback4.Location = new System.Drawing.Point(168, 0);
			this.rdbReback4.Name = "rdbReback4";
			this.rdbReback4.Size = new System.Drawing.Size(56, 24);
			this.rdbReback4.TabIndex = 3;
			this.rdbReback4.Text = "死亡";
			this.rdbReback4.CheckedChanged += new System.EventHandler(this.rdbReback4_CheckedChanged);
			// 
			// rdbReback3
			// 
			this.rdbReback3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.rdbReback3.Location = new System.Drawing.Point(112, 0);
			this.rdbReback3.Name = "rdbReback3";
			this.rdbReback3.Size = new System.Drawing.Size(56, 24);
			this.rdbReback3.TabIndex = 2;
			this.rdbReback3.Text = "未愈";
			// 
			// rdbReback2
			// 
			this.rdbReback2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.rdbReback2.Location = new System.Drawing.Point(56, 0);
			this.rdbReback2.Name = "rdbReback2";
			this.rdbReback2.Size = new System.Drawing.Size(56, 24);
			this.rdbReback2.TabIndex = 1;
			this.rdbReback2.Text = "好转";
			// 
			// rdbReback1
			// 
			this.rdbReback1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.rdbReback1.Location = new System.Drawing.Point(0, 0);
			this.rdbReback1.Name = "rdbReback1";
			this.rdbReback1.Size = new System.Drawing.Size(56, 24);
			this.rdbReback1.TabIndex = 0;
			this.rdbReback1.Text = "治愈";
			// 
			// panel5
			// 
			this.panel5.Controls.Add(this.rdbTreatY);
			this.panel5.Controls.Add(this.rdbTreatN);
			this.panel5.Location = new System.Drawing.Point(424, 464);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(104, 32);
			this.panel5.TabIndex = 100384;
			// 
			// rdbTreatY
			// 
			this.rdbTreatY.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.rdbTreatY.Location = new System.Drawing.Point(8, 8);
			this.rdbTreatY.Name = "rdbTreatY";
			this.rdbTreatY.Size = new System.Drawing.Size(32, 16);
			this.rdbTreatY.TabIndex = 0;
			this.rdbTreatY.Text = "是";
			this.rdbTreatY.CheckedChanged += new System.EventHandler(this.rdbTreatY_CheckedChanged);
			// 
			// rdbTreatN
			// 
			this.rdbTreatN.Checked = true;
			this.rdbTreatN.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.rdbTreatN.Location = new System.Drawing.Point(56, 8);
			this.rdbTreatN.Name = "rdbTreatN";
			this.rdbTreatN.Size = new System.Drawing.Size(32, 16);
			this.rdbTreatN.TabIndex = 1;
			this.rdbTreatN.TabStop = true;
			this.rdbTreatN.Text = "否";
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.rdbPathlogyN);
			this.panel3.Controls.Add(this.rdbPathlogyY);
			this.panel3.Location = new System.Drawing.Point(456, 176);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(104, 32);
			this.panel3.TabIndex = 100383;
			// 
			// rdbPathlogyN
			// 
			this.rdbPathlogyN.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.rdbPathlogyN.Checked = true;
			this.rdbPathlogyN.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.rdbPathlogyN.Location = new System.Drawing.Point(56, 8);
			this.rdbPathlogyN.Name = "rdbPathlogyN";
			this.rdbPathlogyN.Size = new System.Drawing.Size(40, 16);
			this.rdbPathlogyN.TabIndex = 1;
			this.rdbPathlogyN.TabStop = true;
			this.rdbPathlogyN.Text = "否";
			// 
			// rdbPathlogyY
			// 
			this.rdbPathlogyY.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.rdbPathlogyY.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.rdbPathlogyY.Location = new System.Drawing.Point(8, 8);
			this.rdbPathlogyY.Name = "rdbPathlogyY";
			this.rdbPathlogyY.Size = new System.Drawing.Size(40, 16);
			this.rdbPathlogyY.TabIndex = 0;
			this.rdbPathlogyY.Text = "是";
			this.rdbPathlogyY.CheckedChanged += new System.EventHandler(this.rdbPathlogyY_CheckedChanged);
			// 
			// txtClinicalM
			// 
			this.txtClinicalM.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.txtClinicalM.Location = new System.Drawing.Point(528, 144);
			this.txtClinicalM.MaxLength = 3;
			this.txtClinicalM.Name = "txtClinicalM";
			this.txtClinicalM.Size = new System.Drawing.Size(16, 23);
			this.txtClinicalM.TabIndex = 100316;
			this.txtClinicalM.TabStop = false;
			this.txtClinicalM.Text = "";
			this.txtClinicalM.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtClinicalM_KeyPress);
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.rdbTNM4);
			this.panel4.Controls.Add(this.rdbTNM3);
			this.panel4.Controls.Add(this.rdbTNM2);
			this.panel4.Controls.Add(this.rdbTNM1);
			this.panel4.Location = new System.Drawing.Point(544, 144);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(224, 32);
			this.panel4.TabIndex = 100382;
			// 
			// rdbTNM4
			// 
			this.rdbTNM4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.rdbTNM4.Location = new System.Drawing.Point(168, 8);
			this.rdbTNM4.Name = "rdbTNM4";
			this.rdbTNM4.Size = new System.Drawing.Size(48, 16);
			this.rdbTNM4.TabIndex = 3;
			this.rdbTNM4.Text = "IV期";
			// 
			// rdbTNM3
			// 
			this.rdbTNM3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.rdbTNM3.Location = new System.Drawing.Point(112, 8);
			this.rdbTNM3.Name = "rdbTNM3";
			this.rdbTNM3.Size = new System.Drawing.Size(64, 16);
			this.rdbTNM3.TabIndex = 2;
			this.rdbTNM3.Text = "III期";
			// 
			// rdbTNM2
			// 
			this.rdbTNM2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.rdbTNM2.Location = new System.Drawing.Point(64, 8);
			this.rdbTNM2.Name = "rdbTNM2";
			this.rdbTNM2.Size = new System.Drawing.Size(56, 16);
			this.rdbTNM2.TabIndex = 1;
			this.rdbTNM2.Text = "II期";
			// 
			// rdbTNM1
			// 
			this.rdbTNM1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.rdbTNM1.Location = new System.Drawing.Point(8, 8);
			this.rdbTNM1.Name = "rdbTNM1";
			this.rdbTNM1.Size = new System.Drawing.Size(64, 16);
			this.rdbTNM1.TabIndex = 0;
			this.rdbTNM1.Text = "0-I期";
			// 
			// label47
			// 
			this.label47.BackColor = System.Drawing.Color.White;
			this.label47.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label47.Location = new System.Drawing.Point(32, 720);
			this.label47.Name = "label47";
			this.label47.Size = new System.Drawing.Size(72, 23);
			this.label47.TabIndex = 100378;
			this.label47.Text = "上一次审";
			this.label47.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cmbMarrige
			// 
			this.cmbMarrige.ArrowBackColor = System.Drawing.Color.Silver;
			this.cmbMarrige.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbMarrige.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.cmbMarrige.IsFlat = false;
			this.cmbMarrige.IsLike = true;
			this.cmbMarrige.ItemHeight = 14;
			this.cmbMarrige.Location = new System.Drawing.Point(112, 240);
			this.cmbMarrige.MaxDropDownItems = 21;
			this.cmbMarrige.Name = "cmbMarrige";
			this.cmbMarrige.PopForm = null;
			this.cmbMarrige.ShowCustomerList = false;
			this.cmbMarrige.ShowID = false;
			this.cmbMarrige.Size = new System.Drawing.Size(192, 22);
			this.cmbMarrige.TabIndex = 100374;
			this.cmbMarrige.Tag = "";
			this.cmbMarrige.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbMarrige_KeyPress);
			// 
			// label11
			// 
			this.label11.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label11.Location = new System.Drawing.Point(32, 240);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(72, 23);
			this.label11.TabIndex = 100277;
			this.label11.Text = "婚姻状况:";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtPatientID
			// 
			this.txtPatientID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtPatientID.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.txtPatientID.Location = new System.Drawing.Point(112, 208);
			this.txtPatientID.MaxLength = 19;
			this.txtPatientID.Name = "txtPatientID";
			this.txtPatientID.Size = new System.Drawing.Size(192, 23);
			this.txtPatientID.TabIndex = 3;
			this.txtPatientID.Text = "";
			this.txtPatientID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPatientID_KeyPress);
			// 
			// cmbProfession
			// 
			this.cmbProfession.ArrowBackColor = System.Drawing.Color.Silver;
			this.cmbProfession.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbProfession.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.cmbProfession.IsFlat = false;
			this.cmbProfession.IsLike = true;
			this.cmbProfession.ItemHeight = 14;
			this.cmbProfession.Location = new System.Drawing.Point(112, 272);
			this.cmbProfession.MaxDropDownItems = 21;
			this.cmbProfession.Name = "cmbProfession";
			this.cmbProfession.PopForm = null;
			this.cmbProfession.ShowCustomerList = false;
			this.cmbProfession.ShowID = false;
			this.cmbProfession.Size = new System.Drawing.Size(192, 22);
			this.cmbProfession.TabIndex = 13;
			this.cmbProfession.Tag = "";
			this.cmbProfession.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbProfession_KeyPress);
			// 
			// txtSpecialAddress
			// 
			this.txtSpecialAddress.Location = new System.Drawing.Point(640, 720);
			this.txtSpecialAddress.MaxLength = 100;
			this.txtSpecialAddress.Name = "txtSpecialAddress";
			this.txtSpecialAddress.Size = new System.Drawing.Size(96, 23);
			this.txtSpecialAddress.TabIndex = 7;
			this.txtSpecialAddress.Text = "";
			this.txtSpecialAddress.Visible = false;
			this.txtSpecialAddress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSpecialAddress_KeyPress);
			// 
			// txtContactPerson
			// 
			this.txtContactPerson.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtContactPerson.Location = new System.Drawing.Point(112, 592);
			this.txtContactPerson.Name = "txtContactPerson";
			this.txtContactPerson.Size = new System.Drawing.Size(192, 23);
			this.txtContactPerson.TabIndex = 100297;
			this.txtContactPerson.Text = "";
			this.txtContactPerson.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtContactPerson_KeyPress);
			// 
			// txtRelationship
			// 
			this.txtRelationship.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtRelationship.Location = new System.Drawing.Point(112, 624);
			this.txtRelationship.Name = "txtRelationship";
			this.txtRelationship.Size = new System.Drawing.Size(192, 23);
			this.txtRelationship.TabIndex = 100303;
			this.txtRelationship.Text = "";
			this.txtRelationship.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRelationship_KeyPress);
			// 
			// txtContactPersonTel
			// 
			this.txtContactPersonTel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtContactPersonTel.Location = new System.Drawing.Point(112, 656);
			this.txtContactPersonTel.Name = "txtContactPersonTel";
			this.txtContactPersonTel.Size = new System.Drawing.Size(192, 23);
			this.txtContactPersonTel.TabIndex = 100299;
			this.txtContactPersonTel.Text = "";
			this.txtContactPersonTel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtContactPersonTel_KeyPress);
			// 
			// txtContactPersonAddr
			// 
			this.txtContactPersonAddr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtContactPersonAddr.Location = new System.Drawing.Point(112, 688);
			this.txtContactPersonAddr.Name = "txtContactPersonAddr";
			this.txtContactPersonAddr.Size = new System.Drawing.Size(192, 23);
			this.txtContactPersonAddr.TabIndex = 100301;
			this.txtContactPersonAddr.Text = "";
			this.txtContactPersonAddr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtContactPersonAddr_KeyPress);
			// 
			// txtICD
			// 
			this.txtICD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtICD.Location = new System.Drawing.Point(456, 80);
			this.txtICD.Name = "txtICD";
			this.txtICD.Size = new System.Drawing.Size(304, 23);
			this.txtICD.TabIndex = 100305;
			this.txtICD.Text = "";
			this.txtICD.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtICD_KeyPress);
			// 
			// txtPathlogyNo
			// 
			this.txtPathlogyNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtPathlogyNo.Enabled = false;
			this.txtPathlogyNo.Location = new System.Drawing.Point(456, 216);
			this.txtPathlogyNo.Name = "txtPathlogyNo";
			this.txtPathlogyNo.Size = new System.Drawing.Size(104, 23);
			this.txtPathlogyNo.TabIndex = 100320;
			this.txtPathlogyNo.Text = "";
			this.txtPathlogyNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPathlogyNo_KeyPress);
			// 
			// txtPathlogyType
			// 
			this.txtPathlogyType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtPathlogyType.Enabled = false;
			this.txtPathlogyType.Location = new System.Drawing.Point(456, 248);
			this.txtPathlogyType.Name = "txtPathlogyType";
			this.txtPathlogyType.Size = new System.Drawing.Size(104, 23);
			this.txtPathlogyType.TabIndex = 100322;
			this.txtPathlogyType.Text = "";
			this.txtPathlogyType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPathlogyType_KeyPress);
			// 
			// txtICDO
			// 
			this.txtICDO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtICDO.Enabled = false;
			this.txtICDO.Location = new System.Drawing.Point(664, 280);
			this.txtICDO.Name = "txtICDO";
			this.txtICDO.Size = new System.Drawing.Size(96, 23);
			this.txtICDO.TabIndex = 100326;
			this.txtICDO.Text = "";
			// 
			// txtPathlogyDegree
			// 
			this.txtPathlogyDegree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtPathlogyDegree.Enabled = false;
			this.txtPathlogyDegree.Location = new System.Drawing.Point(456, 280);
			this.txtPathlogyDegree.Name = "txtPathlogyDegree";
			this.txtPathlogyDegree.Size = new System.Drawing.Size(104, 23);
			this.txtPathlogyDegree.TabIndex = 100324;
			this.txtPathlogyDegree.Text = "";
			this.txtPathlogyDegree.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPathlogyDegree_KeyPress);
			// 
			// txtOldDiagnoses
			// 
			this.txtOldDiagnoses.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtOldDiagnoses.Location = new System.Drawing.Point(456, 16);
			this.txtOldDiagnoses.MaxLength = 100;
			this.txtOldDiagnoses.Name = "txtOldDiagnoses";
			this.txtOldDiagnoses.Size = new System.Drawing.Size(304, 23);
			this.txtOldDiagnoses.TabIndex = 100363;
			this.txtOldDiagnoses.Text = "";
			// 
			// txtDiagnosesDemo
			// 
			this.txtDiagnosesDemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtDiagnosesDemo.Enabled = false;
			this.txtDiagnosesDemo.Location = new System.Drawing.Point(424, 432);
			this.txtDiagnosesDemo.MaxLength = 100;
			this.txtDiagnosesDemo.Name = "txtDiagnosesDemo";
			this.txtDiagnosesDemo.Size = new System.Drawing.Size(344, 23);
			this.txtDiagnosesDemo.TabIndex = 100352;
			this.txtDiagnosesDemo.Text = "";
			// 
			// txtPatientName
			// 
			this.txtPatientName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtPatientName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.txtPatientName.Location = new System.Drawing.Point(112, 112);
			this.txtPatientName.Name = "txtPatientName";
			this.txtPatientName.Size = new System.Drawing.Size(192, 23);
			this.txtPatientName.TabIndex = 1;
			this.txtPatientName.Text = "";
			this.txtPatientName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPatientName_KeyPress);
			// 
			// label40
			// 
			this.label40.BackColor = System.Drawing.Color.White;
			this.label40.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label40.Location = new System.Drawing.Point(512, 896);
			this.label40.Name = "label40";
			this.label40.Size = new System.Drawing.Size(80, 16);
			this.label40.TabIndex = 100377;
			this.label40.Text = "填卡时间：";
			this.label40.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label42
			// 
			this.label42.BackColor = System.Drawing.Color.White;
			this.label42.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label42.Location = new System.Drawing.Point(32, 896);
			this.label42.Name = "label42";
			this.label42.Size = new System.Drawing.Size(80, 16);
			this.label42.TabIndex = 100376;
			this.label42.Text = "报告科室：";
			this.label42.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label44
			// 
			this.label44.BackColor = System.Drawing.Color.White;
			this.label44.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label44.Location = new System.Drawing.Point(352, 688);
			this.label44.Name = "label44";
			this.label44.Size = new System.Drawing.Size(80, 23);
			this.label44.TabIndex = 100375;
			this.label44.Text = "报告医生:";
			this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lbDoctorDept
			// 
			this.lbDoctorDept.BackColor = System.Drawing.Color.White;
			this.lbDoctorDept.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.lbDoctorDept.Location = new System.Drawing.Point(112, 896);
			this.lbDoctorDept.Name = "lbDoctorDept";
			this.lbDoctorDept.Size = new System.Drawing.Size(136, 16);
			this.lbDoctorDept.TabIndex = 3;
			this.lbDoctorDept.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lbReportTime
			// 
			this.lbReportTime.BackColor = System.Drawing.Color.White;
			this.lbReportTime.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.lbReportTime.Location = new System.Drawing.Point(600, 896);
			this.lbReportTime.Name = "lbReportTime";
			this.lbReportTime.Size = new System.Drawing.Size(176, 16);
			this.lbReportTime.TabIndex = 5;
			this.lbReportTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label63
			// 
			this.label63.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label63.Location = new System.Drawing.Point(32, 112);
			this.label63.Name = "label63";
			this.label63.Size = new System.Drawing.Size(72, 23);
			this.label63.TabIndex = 26;
			this.label63.Text = "患者姓名:";
			this.label63.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cmbNation
			// 
			this.cmbNation.ArrowBackColor = System.Drawing.Color.Silver;
			this.cmbNation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbNation.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.cmbNation.IsFlat = false;
			this.cmbNation.IsLike = true;
			this.cmbNation.ItemHeight = 14;
			this.cmbNation.Location = new System.Drawing.Point(240, 144);
			this.cmbNation.MaxDropDownItems = 21;
			this.cmbNation.Name = "cmbNation";
			this.cmbNation.PopForm = null;
			this.cmbNation.ShowCustomerList = false;
			this.cmbNation.ShowID = false;
			this.cmbNation.Size = new System.Drawing.Size(64, 22);
			this.cmbNation.TabIndex = 100373;
			this.cmbNation.Tag = "";
			this.cmbNation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbNation_KeyPress);
			// 
			// txtRegisterCity
			// 
			this.txtRegisterCity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtRegisterCity.Location = new System.Drawing.Point(200, 400);
			this.txtRegisterCity.Name = "txtRegisterCity";
			this.txtRegisterCity.Size = new System.Drawing.Size(64, 23);
			this.txtRegisterCity.TabIndex = 100289;
			this.txtRegisterCity.Text = "";
			this.txtRegisterCity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRegisterCity_KeyPress);
			// 
			// dtOldDiagnosesTime
			// 
			this.dtOldDiagnosesTime.Location = new System.Drawing.Point(424, 656);
			this.dtOldDiagnosesTime.Name = "dtOldDiagnosesTime";
			this.dtOldDiagnosesTime.Size = new System.Drawing.Size(144, 23);
			this.dtOldDiagnosesTime.TabIndex = 100367;
			this.dtOldDiagnosesTime.Value = new System.DateTime(2012, 1, 1, 0, 0, 0, 0);
			this.dtOldDiagnosesTime.Visible = false;
			// 
			// txtDeathResion
			// 
			this.txtDeathResion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtDeathResion.Location = new System.Drawing.Point(648, 656);
			this.txtDeathResion.MaxLength = 100;
			this.txtDeathResion.Name = "txtDeathResion";
			this.txtDeathResion.Size = new System.Drawing.Size(120, 23);
			this.txtDeathResion.TabIndex = 100366;
			this.txtDeathResion.TabStop = false;
			this.txtDeathResion.Text = "";
			// 
			// label17
			// 
			this.label17.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label17.Location = new System.Drawing.Point(576, 656);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(72, 23);
			this.label17.TabIndex = 100365;
			this.label17.Text = "死亡原因:";
			this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label43
			// 
			this.label43.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label43.Location = new System.Drawing.Point(352, 656);
			this.label43.Name = "label43";
			this.label43.Size = new System.Drawing.Size(72, 23);
			this.label43.TabIndex = 100364;
			this.label43.Text = "死亡日期:";
			this.label43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtRebackDemo
			// 
			this.txtRebackDemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtRebackDemo.Enabled = false;
			this.txtRebackDemo.Location = new System.Drawing.Point(424, 624);
			this.txtRebackDemo.MaxLength = 100;
			this.txtRebackDemo.Name = "txtRebackDemo";
			this.txtRebackDemo.Size = new System.Drawing.Size(344, 23);
			this.txtRebackDemo.TabIndex = 100362;
			this.txtRebackDemo.Text = "";
			// 
			// txtTreatmentDemo
			// 
			this.txtTreatmentDemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtTreatmentDemo.Enabled = false;
			this.txtTreatmentDemo.Location = new System.Drawing.Point(424, 560);
			this.txtTreatmentDemo.MaxLength = 100;
			this.txtTreatmentDemo.Name = "txtTreatmentDemo";
			this.txtTreatmentDemo.Size = new System.Drawing.Size(344, 23);
			this.txtTreatmentDemo.TabIndex = 100361;
			this.txtTreatmentDemo.Text = "";
			// 
			// chbTreatment9
			// 
			this.chbTreatment9.Enabled = false;
			this.chbTreatment9.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbTreatment9.Location = new System.Drawing.Point(648, 528);
			this.chbTreatment9.Name = "chbTreatment9";
			this.chbTreatment9.Size = new System.Drawing.Size(128, 24);
			this.chbTreatment9.TabIndex = 100360;
			this.chbTreatment9.Text = "其他（请注明）";
			this.chbTreatment9.CheckedChanged += new System.EventHandler(this.chbTreatment9_CheckedChanged);
			// 
			// chbTreatment6
			// 
			this.chbTreatment6.Enabled = false;
			this.chbTreatment6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbTreatment6.Location = new System.Drawing.Point(712, 496);
			this.chbTreatment6.Name = "chbTreatment6";
			this.chbTreatment6.Size = new System.Drawing.Size(56, 24);
			this.chbTreatment6.TabIndex = 100359;
			this.chbTreatment6.TabStop = false;
			this.chbTreatment6.Text = "介入";
			// 
			// chbTreatment5
			// 
			this.chbTreatment5.Enabled = false;
			this.chbTreatment5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbTreatment5.Location = new System.Drawing.Point(648, 496);
			this.chbTreatment5.Name = "chbTreatment5";
			this.chbTreatment5.Size = new System.Drawing.Size(56, 24);
			this.chbTreatment5.TabIndex = 100358;
			this.chbTreatment5.TabStop = false;
			this.chbTreatment5.Text = "免疫";
			// 
			// chbTreatment7
			// 
			this.chbTreatment7.Enabled = false;
			this.chbTreatment7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbTreatment7.Location = new System.Drawing.Point(424, 528);
			this.chbTreatment7.Name = "chbTreatment7";
			this.chbTreatment7.Size = new System.Drawing.Size(88, 24);
			this.chbTreatment7.TabIndex = 100357;
			this.chbTreatment7.TabStop = false;
			this.chbTreatment7.Text = "对症治疗";
			// 
			// chbTreatment4
			// 
			this.chbTreatment4.Enabled = false;
			this.chbTreatment4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbTreatment4.Location = new System.Drawing.Point(592, 496);
			this.chbTreatment4.Name = "chbTreatment4";
			this.chbTreatment4.Size = new System.Drawing.Size(56, 24);
			this.chbTreatment4.TabIndex = 100356;
			this.chbTreatment4.TabStop = false;
			this.chbTreatment4.Text = "中药";
			// 
			// chbTreatment3
			// 
			this.chbTreatment3.Enabled = false;
			this.chbTreatment3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbTreatment3.Location = new System.Drawing.Point(536, 496);
			this.chbTreatment3.Name = "chbTreatment3";
			this.chbTreatment3.Size = new System.Drawing.Size(56, 24);
			this.chbTreatment3.TabIndex = 100355;
			this.chbTreatment3.TabStop = false;
			this.chbTreatment3.Text = "放射";
			// 
			// chbTreatment2
			// 
			this.chbTreatment2.Enabled = false;
			this.chbTreatment2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbTreatment2.Location = new System.Drawing.Point(480, 496);
			this.chbTreatment2.Name = "chbTreatment2";
			this.chbTreatment2.Size = new System.Drawing.Size(56, 24);
			this.chbTreatment2.TabIndex = 100354;
			this.chbTreatment2.TabStop = false;
			this.chbTreatment2.Text = "化疗";
			// 
			// chbTreatment1
			// 
			this.chbTreatment1.Enabled = false;
			this.chbTreatment1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbTreatment1.Location = new System.Drawing.Point(424, 496);
			this.chbTreatment1.Name = "chbTreatment1";
			this.chbTreatment1.Size = new System.Drawing.Size(56, 24);
			this.chbTreatment1.TabIndex = 100353;
			this.chbTreatment1.Text = "手术";
			// 
			// chbDiagnoses18
			// 
			this.chbDiagnoses18.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbDiagnoses18.Location = new System.Drawing.Point(512, 408);
			this.chbDiagnoses18.Name = "chbDiagnoses18";
			this.chbDiagnoses18.Size = new System.Drawing.Size(56, 24);
			this.chbDiagnoses18.TabIndex = 100351;
			this.chbDiagnoses18.Text = "不详";
			// 
			// chbDiagnoses17
			// 
			this.chbDiagnoses17.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbDiagnoses17.Location = new System.Drawing.Point(424, 408);
			this.chbDiagnoses17.Name = "chbDiagnoses17";
			this.chbDiagnoses17.Size = new System.Drawing.Size(88, 24);
			this.chbDiagnoses17.TabIndex = 100350;
			this.chbDiagnoses17.TabStop = false;
			this.chbDiagnoses17.Text = "核磁共振";
			// 
			// chbDiagnoses19
			// 
			this.chbDiagnoses19.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbDiagnoses19.Location = new System.Drawing.Point(600, 408);
			this.chbDiagnoses19.Name = "chbDiagnoses19";
			this.chbDiagnoses19.Size = new System.Drawing.Size(125, 24);
			this.chbDiagnoses19.TabIndex = 100349;
			this.chbDiagnoses19.Text = "其他（请注明）";
			this.chbDiagnoses19.CheckedChanged += new System.EventHandler(this.chbDiagnoses19_CheckedChanged);
			// 
			// chbDiagnoses15
			// 
			this.chbDiagnoses15.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbDiagnoses15.Location = new System.Drawing.Point(512, 384);
			this.chbDiagnoses15.Name = "chbDiagnoses15";
			this.chbDiagnoses15.Size = new System.Drawing.Size(112, 24);
			this.chbDiagnoses15.TabIndex = 100347;
			this.chbDiagnoses15.TabStop = false;
			this.chbDiagnoses15.Text = "尸检(有病理)";
			this.chbDiagnoses15.CheckedChanged += new System.EventHandler(this.chbDiagnoses15_CheckedChanged);
			// 
			// chbTreatment8
			// 
			this.chbTreatment8.Enabled = false;
			this.chbTreatment8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbTreatment8.Location = new System.Drawing.Point(536, 528);
			this.chbTreatment8.Name = "chbTreatment8";
			this.chbTreatment8.Size = new System.Drawing.Size(88, 24);
			this.chbTreatment8.TabIndex = 100346;
			this.chbTreatment8.Text = "止痛治疗";
			// 
			// chbDiagnoses14
			// 
			this.chbDiagnoses14.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbDiagnoses14.Location = new System.Drawing.Point(424, 384);
			this.chbDiagnoses14.Name = "chbDiagnoses14";
			this.chbDiagnoses14.Size = new System.Drawing.Size(96, 24);
			this.chbDiagnoses14.TabIndex = 100345;
			this.chbDiagnoses14.TabStop = false;
			this.chbDiagnoses14.Text = "病理(原发)";
			this.chbDiagnoses14.CheckedChanged += new System.EventHandler(this.chbDiagnoses14_CheckedChanged);
			// 
			// chbDiagnoses13
			// 
			this.chbDiagnoses13.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbDiagnoses13.Location = new System.Drawing.Point(696, 360);
			this.chbDiagnoses13.Name = "chbDiagnoses13";
			this.chbDiagnoses13.Size = new System.Drawing.Size(88, 24);
			this.chbDiagnoses13.TabIndex = 100344;
			this.chbDiagnoses13.Text = "病理(继发)";
			this.chbDiagnoses13.CheckedChanged += new System.EventHandler(this.chbDiagnoses13_CheckedChanged);
			// 
			// chbDiagnoses12
			// 
			this.chbDiagnoses12.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbDiagnoses12.Location = new System.Drawing.Point(600, 360);
			this.chbDiagnoses12.Name = "chbDiagnoses12";
			this.chbDiagnoses12.Size = new System.Drawing.Size(56, 24);
			this.chbDiagnoses12.TabIndex = 100343;
			this.chbDiagnoses12.TabStop = false;
			this.chbDiagnoses12.Text = "血片";
			// 
			// chbDiagnoses11
			// 
			this.chbDiagnoses11.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbDiagnoses11.Location = new System.Drawing.Point(512, 360);
			this.chbDiagnoses11.Name = "chbDiagnoses11";
			this.chbDiagnoses11.Size = new System.Drawing.Size(72, 24);
			this.chbDiagnoses11.TabIndex = 100342;
			this.chbDiagnoses11.Text = "细胞学";
			// 
			// chbDiagnoses10
			// 
			this.chbDiagnoses10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbDiagnoses10.Location = new System.Drawing.Point(424, 360);
			this.chbDiagnoses10.Name = "chbDiagnoses10";
			this.chbDiagnoses10.Size = new System.Drawing.Size(56, 24);
			this.chbDiagnoses10.TabIndex = 100341;
			this.chbDiagnoses10.TabStop = false;
			this.chbDiagnoses10.Text = "免疫";
			// 
			// chbDiagnoses9
			// 
			this.chbDiagnoses9.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbDiagnoses9.Location = new System.Drawing.Point(696, 336);
			this.chbDiagnoses9.Name = "chbDiagnoses9";
			this.chbDiagnoses9.Size = new System.Drawing.Size(56, 24);
			this.chbDiagnoses9.TabIndex = 100340;
			this.chbDiagnoses9.Text = "生化";
			// 
			// chbDiagnoses7
			// 
			this.chbDiagnoses7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbDiagnoses7.Location = new System.Drawing.Point(600, 336);
			this.chbDiagnoses7.Name = "chbDiagnoses7";
			this.chbDiagnoses7.Size = new System.Drawing.Size(56, 24);
			this.chbDiagnoses7.TabIndex = 100338;
			this.chbDiagnoses7.Text = "手术";
			// 
			// chbDiagnoses6
			// 
			this.chbDiagnoses6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbDiagnoses6.Location = new System.Drawing.Point(512, 336);
			this.chbDiagnoses6.Name = "chbDiagnoses6";
			this.chbDiagnoses6.Size = new System.Drawing.Size(56, 24);
			this.chbDiagnoses6.TabIndex = 100337;
			this.chbDiagnoses6.TabStop = false;
			this.chbDiagnoses6.Text = "PET";
			// 
			// chbDiagnoses5
			// 
			this.chbDiagnoses5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbDiagnoses5.Location = new System.Drawing.Point(424, 336);
			this.chbDiagnoses5.Name = "chbDiagnoses5";
			this.chbDiagnoses5.Size = new System.Drawing.Size(48, 24);
			this.chbDiagnoses5.TabIndex = 100336;
			this.chbDiagnoses5.Text = "CT";
			// 
			// chbDiagnoses4
			// 
			this.chbDiagnoses4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbDiagnoses4.Location = new System.Drawing.Point(696, 312);
			this.chbDiagnoses4.Name = "chbDiagnoses4";
			this.chbDiagnoses4.Size = new System.Drawing.Size(72, 24);
			this.chbDiagnoses4.TabIndex = 100335;
			this.chbDiagnoses4.TabStop = false;
			this.chbDiagnoses4.Text = "内窥镜";
			// 
			// chbDiagnoses3
			// 
			this.chbDiagnoses3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbDiagnoses3.Location = new System.Drawing.Point(600, 312);
			this.chbDiagnoses3.Name = "chbDiagnoses3";
			this.chbDiagnoses3.Size = new System.Drawing.Size(72, 24);
			this.chbDiagnoses3.TabIndex = 100334;
			this.chbDiagnoses3.Tag = "3";
			this.chbDiagnoses3.Text = "超声波";
			// 
			// chbDiagnoses2
			// 
			this.chbDiagnoses2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbDiagnoses2.Location = new System.Drawing.Point(512, 312);
			this.chbDiagnoses2.Name = "chbDiagnoses2";
			this.chbDiagnoses2.Size = new System.Drawing.Size(56, 24);
			this.chbDiagnoses2.TabIndex = 100333;
			this.chbDiagnoses2.TabStop = false;
			this.chbDiagnoses2.Tag = "2";
			this.chbDiagnoses2.Text = "X光";
			// 
			// chbDiagnoses1
			// 
			this.chbDiagnoses1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbDiagnoses1.Location = new System.Drawing.Point(424, 312);
			this.chbDiagnoses1.Name = "chbDiagnoses1";
			this.chbDiagnoses1.Size = new System.Drawing.Size(72, 24);
			this.chbDiagnoses1.TabIndex = 100332;
			this.chbDiagnoses1.Tag = "1";
			this.chbDiagnoses1.Text = "临床";
			// 
			// label18
			// 
			this.label18.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label18.Location = new System.Drawing.Point(352, 16);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(112, 23);
			this.label18.TabIndex = 100331;
			this.label18.Text = "临床医生诊断:";
			this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label38
			// 
			this.label38.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label38.Location = new System.Drawing.Point(352, 496);
			this.label38.Name = "label38";
			this.label38.Size = new System.Drawing.Size(72, 24);
			this.label38.TabIndex = 100329;
			this.label38.Text = "治疗方式:";
			this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label36
			// 
			this.label36.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label36.Location = new System.Drawing.Point(352, 472);
			this.label36.Name = "label36";
			this.label36.Size = new System.Drawing.Size(72, 16);
			this.label36.TabIndex = 100328;
			this.label36.Text = "是否治疗:";
			// 
			// label19
			// 
			this.label19.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label19.Location = new System.Drawing.Point(352, 312);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(72, 24);
			this.label19.TabIndex = 100327;
			this.label19.Text = "诊断依据:";
			// 
			// label34
			// 
			this.label34.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label34.Location = new System.Drawing.Point(584, 280);
			this.label34.Name = "label34";
			this.label34.Size = new System.Drawing.Size(72, 16);
			this.label34.TabIndex = 100325;
			this.label34.Text = "ICD-O-3形态";
			this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label33
			// 
			this.label33.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label33.Location = new System.Drawing.Point(352, 280);
			this.label33.Name = "label33";
			this.label33.Size = new System.Drawing.Size(72, 24);
			this.label33.TabIndex = 100323;
			this.label33.Text = "分化程度:";
			this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label31
			// 
			this.label31.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label31.Location = new System.Drawing.Point(352, 248);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(88, 24);
			this.label31.TabIndex = 100321;
			this.label31.Text = "病理学分型:";
			this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label32
			// 
			this.label32.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label32.Location = new System.Drawing.Point(352, 216);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(56, 24);
			this.label32.TabIndex = 100319;
			this.label32.Text = "病理号:";
			this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label29
			// 
			this.label29.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label29.Location = new System.Drawing.Point(512, 144);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(16, 24);
			this.label29.TabIndex = 100317;
			this.label29.Text = "M";
			this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label28
			// 
			this.label28.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label28.Location = new System.Drawing.Point(472, 144);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(16, 24);
			this.label28.TabIndex = 100315;
			this.label28.Text = "N";
			this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtClinicalN
			// 
			this.txtClinicalN.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.txtClinicalN.Location = new System.Drawing.Point(488, 144);
			this.txtClinicalN.MaxLength = 3;
			this.txtClinicalN.Name = "txtClinicalN";
			this.txtClinicalN.Size = new System.Drawing.Size(21, 23);
			this.txtClinicalN.TabIndex = 100314;
			this.txtClinicalN.TabStop = false;
			this.txtClinicalN.Text = "";
			this.txtClinicalN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtClinicalN_KeyPress);
			// 
			// label26
			// 
			this.label26.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label26.Location = new System.Drawing.Point(352, 144);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(104, 24);
			this.label26.TabIndex = 100312;
			this.label26.Text = "临床分期:  T";
			this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtClinicalT
			// 
			this.txtClinicalT.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.txtClinicalT.Location = new System.Drawing.Point(456, 144);
			this.txtClinicalT.MaxLength = 3;
			this.txtClinicalT.Name = "txtClinicalT";
			this.txtClinicalT.Size = new System.Drawing.Size(16, 23);
			this.txtClinicalT.TabIndex = 100311;
			this.txtClinicalT.TabStop = false;
			this.txtClinicalT.Text = "";
			this.txtClinicalT.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtClinicalT_KeyPress);
			// 
			// label64
			// 
			this.label64.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label64.Location = new System.Drawing.Point(352, 592);
			this.label64.Name = "label64";
			this.label64.Size = new System.Drawing.Size(72, 23);
			this.label64.TabIndex = 100330;
			this.label64.Text = "转    归:";
			this.label64.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dateTimePicker1
			// 
			this.dateTimePicker1.CustomFormat = "yyyy年MM月dd日";
			this.dateTimePicker1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePicker1.Location = new System.Drawing.Point(456, 112);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.Size = new System.Drawing.Size(152, 23);
			this.dateTimePicker1.TabIndex = 100307;
			this.dateTimePicker1.TabStop = false;
			this.dateTimePicker1.Value = new System.DateTime(2009, 12, 1, 10, 41, 17, 843);
			this.dateTimePicker1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dateTimePicker1_KeyPress);
			// 
			// label16
			// 
			this.label16.BackColor = System.Drawing.Color.White;
			this.label16.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label16.Location = new System.Drawing.Point(352, 112);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(80, 23);
			this.label16.TabIndex = 100306;
			this.label16.Text = "确诊时间：";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label25
			// 
			this.label25.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label25.Location = new System.Drawing.Point(352, 80);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(64, 24);
			this.label25.TabIndex = 100304;
			this.label25.Text = "ICD编码:";
			this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label23
			// 
			this.label23.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label23.Location = new System.Drawing.Point(32, 624);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(72, 23);
			this.label23.TabIndex = 100302;
			this.label23.Text = "关    系:";
			this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label22
			// 
			this.label22.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label22.Location = new System.Drawing.Point(32, 512);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(72, 24);
			this.label22.TabIndex = 100300;
			this.label22.Text = "常用地址:";
			this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label21
			// 
			this.label21.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label21.Location = new System.Drawing.Point(32, 336);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(72, 24);
			this.label21.TabIndex = 100298;
			this.label21.Text = "联系电话:";
			this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label20
			// 
			this.label20.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label20.Location = new System.Drawing.Point(32, 592);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(72, 23);
			this.label20.TabIndex = 100296;
			this.label20.Text = "联 系 人:";
			this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label14
			// 
			this.label14.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label14.Location = new System.Drawing.Point(32, 688);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(72, 23);
			this.label14.TabIndex = 100295;
			this.label14.Text = "联系地址:";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtPost
			// 
			this.txtPost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtPost.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.txtPost.Location = new System.Drawing.Point(112, 480);
			this.txtPost.Name = "txtPost";
			this.txtPost.Size = new System.Drawing.Size(104, 23);
			this.txtPost.TabIndex = 100293;
			this.txtPost.Text = "";
			this.txtPost.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPost_KeyPress);
			// 
			// label15
			// 
			this.label15.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label15.ForeColor = System.Drawing.Color.Black;
			this.label15.Location = new System.Drawing.Point(32, 480);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(72, 23);
			this.label15.TabIndex = 100292;
			this.label15.Text = "邮政编码:";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtRegisterCounty
			// 
			this.txtRegisterCounty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtRegisterCounty.Location = new System.Drawing.Point(112, 424);
			this.txtRegisterCounty.Name = "txtRegisterCounty";
			this.txtRegisterCounty.Size = new System.Drawing.Size(64, 23);
			this.txtRegisterCounty.TabIndex = 100281;
			this.txtRegisterCounty.Text = "";
			this.txtRegisterCounty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRegisterCounty_KeyPress);
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label5.Location = new System.Drawing.Point(280, 400);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(24, 23);
			this.label5.TabIndex = 100291;
			this.label5.Text = "市 ";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label7.Location = new System.Drawing.Point(176, 400);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(24, 23);
			this.label7.TabIndex = 100290;
			this.label7.Text = "省 ";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtRegisterProvince
			// 
			this.txtRegisterProvince.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtRegisterProvince.Location = new System.Drawing.Point(112, 400);
			this.txtRegisterProvince.Name = "txtRegisterProvince";
			this.txtRegisterProvince.Size = new System.Drawing.Size(64, 23);
			this.txtRegisterProvince.TabIndex = 100288;
			this.txtRegisterProvince.Text = "";
			this.txtRegisterProvince.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRegisterProvince_KeyPress);
			// 
			// txtRegisterHouseNO
			// 
			this.txtRegisterHouseNO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtRegisterHouseNO.Location = new System.Drawing.Point(112, 448);
			this.txtRegisterHouseNO.Name = "txtRegisterHouseNO";
			this.txtRegisterHouseNO.Size = new System.Drawing.Size(192, 23);
			this.txtRegisterHouseNO.TabIndex = 100283;
			this.txtRegisterHouseNO.Text = "";
			this.txtRegisterHouseNO.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRegisterHouseNO_KeyPress);
			// 
			// txtRegisterTown
			// 
			this.txtRegisterTown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtRegisterTown.Location = new System.Drawing.Point(224, 424);
			this.txtRegisterTown.Name = "txtRegisterTown";
			this.txtRegisterTown.Size = new System.Drawing.Size(48, 23);
			this.txtRegisterTown.TabIndex = 100282;
			this.txtRegisterTown.Text = "";
			this.txtRegisterTown.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRegisterTown_KeyPress);
			// 
			// label10
			// 
			this.label10.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label10.Location = new System.Drawing.Point(32, 400);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(72, 24);
			this.label10.TabIndex = 100286;
			this.label10.Text = "户口地址:";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label12
			// 
			this.label12.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label12.Location = new System.Drawing.Point(272, 424);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(64, 23);
			this.label12.TabIndex = 100285;
			this.label12.Text = "街道(镇)";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label13
			// 
			this.label13.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label13.Location = new System.Drawing.Point(176, 424);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(48, 23);
			this.label13.TabIndex = 100284;
			this.label13.Text = "区(县)";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtHandPhone
			// 
			this.txtHandPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtHandPhone.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.txtHandPhone.Location = new System.Drawing.Point(112, 368);
			this.txtHandPhone.MaxLength = 20;
			this.txtHandPhone.Name = "txtHandPhone";
			this.txtHandPhone.Size = new System.Drawing.Size(192, 23);
			this.txtHandPhone.TabIndex = 100279;
			this.txtHandPhone.Text = "";
			this.txtHandPhone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtHandPhone_KeyPress);
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label4.Location = new System.Drawing.Point(32, 368);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(72, 23);
			this.label4.TabIndex = 100280;
			this.label4.Text = "手机号码:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtDistrict
			// 
			this.txtDistrict.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtDistrict.Location = new System.Drawing.Point(112, 176);
			this.txtDistrict.Name = "txtDistrict";
			this.txtDistrict.Size = new System.Drawing.Size(192, 23);
			this.txtDistrict.TabIndex = 100274;
			this.txtDistrict.Text = "";
			this.txtDistrict.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDistrict_KeyPress);
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label2.Location = new System.Drawing.Point(32, 176);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 23);
			this.label2.TabIndex = 100273;
			this.label2.Text = "籍    贯:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label9.Location = new System.Drawing.Point(200, 144);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(48, 23);
			this.label9.TabIndex = 100271;
			this.label9.Text = "民族：";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label45
			// 
			this.label45.ForeColor = System.Drawing.Color.Red;
			this.label45.Location = new System.Drawing.Point(16, 240);
			this.label45.Name = "label45";
			this.label45.Size = new System.Drawing.Size(8, 23);
			this.label45.TabIndex = 81;
			this.label45.Text = "*";
			this.label45.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lbSexStar
			// 
			this.lbSexStar.ForeColor = System.Drawing.Color.Red;
			this.lbSexStar.Location = new System.Drawing.Point(16, 144);
			this.lbSexStar.Name = "lbSexStar";
			this.lbSexStar.Size = new System.Drawing.Size(8, 23);
			this.lbSexStar.TabIndex = 79;
			this.lbSexStar.Text = "*";
			this.lbSexStar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label49
			// 
			this.label49.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label49.Location = new System.Drawing.Point(32, 272);
			this.label49.Name = "label49";
			this.label49.Size = new System.Drawing.Size(72, 23);
			this.label49.TabIndex = 72;
			this.label49.Text = "职    业:";
			this.label49.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtTelephone
			// 
			this.txtTelephone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtTelephone.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.txtTelephone.Location = new System.Drawing.Point(112, 336);
			this.txtTelephone.MaxLength = 34;
			this.txtTelephone.Name = "txtTelephone";
			this.txtTelephone.Size = new System.Drawing.Size(192, 23);
			this.txtTelephone.TabIndex = 14;
			this.txtTelephone.Text = "";
			this.txtTelephone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTelephone_KeyPress);
			// 
			// label57
			// 
			this.label57.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label57.Location = new System.Drawing.Point(32, 656);
			this.label57.Name = "label57";
			this.label57.Size = new System.Drawing.Size(72, 23);
			this.label57.TabIndex = 52;
			this.label57.Text = "联系电话:";
			this.label57.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label61
			// 
			this.label61.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label61.Location = new System.Drawing.Point(32, 304);
			this.label61.Name = "label61";
			this.label61.Size = new System.Drawing.Size(72, 23);
			this.label61.TabIndex = 41;
			this.label61.Text = "出生日期:";
			this.label61.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cbxWomen
			// 
			this.cbxWomen.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.cbxWomen.Location = new System.Drawing.Point(152, 144);
			this.cbxWomen.Name = "cbxWomen";
			this.cbxWomen.Size = new System.Drawing.Size(40, 24);
			this.cbxWomen.TabIndex = 40;
			this.cbxWomen.TabStop = false;
			this.cbxWomen.Text = "女";
			// 
			// cbxMan
			// 
			this.cbxMan.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.cbxMan.Location = new System.Drawing.Point(112, 144);
			this.cbxMan.Name = "cbxMan";
			this.cbxMan.Size = new System.Drawing.Size(40, 24);
			this.cbxMan.TabIndex = 4;
			this.cbxMan.Text = "男";
			// 
			// lbSex
			// 
			this.lbSex.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.lbSex.Location = new System.Drawing.Point(32, 144);
			this.lbSex.Name = "lbSex";
			this.lbSex.Size = new System.Drawing.Size(72, 24);
			this.lbSex.TabIndex = 38;
			this.lbSex.Text = "性    别:";
			this.lbSex.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label65
			// 
			this.label65.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label65.Location = new System.Drawing.Point(32, 208);
			this.label65.Name = "label65";
			this.label65.Size = new System.Drawing.Size(72, 23);
			this.label65.TabIndex = 30;
			this.label65.Text = "身份证号:";
			this.label65.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dtBirthDay
			// 
			this.dtBirthDay.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.dtBirthDay.Location = new System.Drawing.Point(112, 304);
			this.dtBirthDay.Name = "dtBirthDay";
			this.dtBirthDay.Size = new System.Drawing.Size(192, 23);
			this.dtBirthDay.TabIndex = 5;
			this.dtBirthDay.Value = new System.DateTime(2009, 12, 1, 10, 41, 18, 78);
			this.dtBirthDay.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtBirthDay_KeyPress);
			this.dtBirthDay.ValueChanged += new System.EventHandler(this.dtBirthDay_ValueChanged);
			// 
			// cmbInfectionClass
			// 
			this.cmbInfectionClass.ArrowBackColor = System.Drawing.Color.Silver;
			this.cmbInfectionClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbInfectionClass.DropDownWidth = 200;
			this.cmbInfectionClass.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.cmbInfectionClass.IsFlat = false;
			this.cmbInfectionClass.IsLike = true;
			this.cmbInfectionClass.ItemHeight = 14;
			this.cmbInfectionClass.Items.AddRange(new object[] {
																   "ds"});
			this.cmbInfectionClass.Location = new System.Drawing.Point(456, 48);
			this.cmbInfectionClass.MaxDropDownItems = 20;
			this.cmbInfectionClass.Name = "cmbInfectionClass";
			this.cmbInfectionClass.PopForm = null;
			this.cmbInfectionClass.ShowCustomerList = false;
			this.cmbInfectionClass.ShowID = false;
			this.cmbInfectionClass.Size = new System.Drawing.Size(304, 22);
			this.cmbInfectionClass.TabIndex = 16;
			this.cmbInfectionClass.TabStop = false;
			this.cmbInfectionClass.Tag = "";
			this.cmbInfectionClass.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbInfectionClass_KeyPress);
			this.cmbInfectionClass.SelectedValueChanged += new System.EventHandler(this.cmbInfectionClass_SelectedValueChanged);
			// 
			// label48
			// 
			this.label48.BackColor = System.Drawing.Color.White;
			this.label48.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label48.Location = new System.Drawing.Point(352, 48);
			this.label48.Name = "label48";
			this.label48.Size = new System.Drawing.Size(80, 23);
			this.label48.TabIndex = 5;
			this.label48.Text = "疾病名称：";
			this.label48.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label30
			// 
			this.label30.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label30.Location = new System.Drawing.Point(352, 176);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(64, 32);
			this.label30.TabIndex = 100318;
			this.label30.Text = "是否做病理学检查:";
			this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lb_patientType
			// 
			this.lb_patientType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lb_patientType.Location = new System.Drawing.Point(112, 16);
			this.lb_patientType.Name = "lb_patientType";
			this.lb_patientType.Size = new System.Drawing.Size(192, 23);
			this.lb_patientType.TabIndex = 100379;
			this.lb_patientType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label46
			// 
			this.label46.Location = new System.Drawing.Point(32, 16);
			this.label46.Name = "label46";
			this.label46.Size = new System.Drawing.Size(72, 23);
			this.label46.TabIndex = 100380;
			this.label46.Text = "病人来源:";
			this.label46.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label67
			// 
			this.label67.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label67.Location = new System.Drawing.Point(32, 48);
			this.label67.Name = "label67";
			this.label67.Size = new System.Drawing.Size(80, 24);
			this.label67.TabIndex = 22;
			this.label67.Text = "患 者 号:";
			this.label67.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label24
			// 
			this.label24.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label24.Location = new System.Drawing.Point(32, 80);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(72, 23);
			this.label24.TabIndex = 39;
			this.label24.Text = "医保卡号:";
			this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lbMadicalCardNo
			// 
			this.lbMadicalCardNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lbMadicalCardNo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.lbMadicalCardNo.Location = new System.Drawing.Point(112, 80);
			this.lbMadicalCardNo.Name = "lbMadicalCardNo";
			this.lbMadicalCardNo.Size = new System.Drawing.Size(192, 23);
			this.lbMadicalCardNo.TabIndex = 38;
			this.lbMadicalCardNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// rdbMonth
			// 
			this.rdbMonth.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.rdbMonth.Location = new System.Drawing.Point(592, 144);
			this.rdbMonth.Name = "rdbMonth";
			this.rdbMonth.Size = new System.Drawing.Size(32, 24);
			this.rdbMonth.TabIndex = 48;
			this.rdbMonth.Text = "月";
			this.rdbMonth.Visible = false;
			// 
			// rdbYear
			// 
			this.rdbYear.Checked = true;
			this.rdbYear.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.rdbYear.Location = new System.Drawing.Point(560, 144);
			this.rdbYear.Name = "rdbYear";
			this.rdbYear.Size = new System.Drawing.Size(32, 24);
			this.rdbYear.TabIndex = 47;
			this.rdbYear.TabStop = true;
			this.rdbYear.Text = "岁";
			this.rdbYear.Visible = false;
			// 
			// label59
			// 
			this.label59.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label59.Location = new System.Drawing.Point(488, 144);
			this.label59.Name = "label59";
			this.label59.Size = new System.Drawing.Size(80, 23);
			this.label59.TabIndex = 45;
			this.label59.Text = "年龄单位：";
			this.label59.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label59.Visible = false;
			// 
			// txtAge
			// 
			this.txtAge.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.txtAge.Location = new System.Drawing.Point(440, 144);
			this.txtAge.MaxLength = 3;
			this.txtAge.Name = "txtAge";
			this.txtAge.Size = new System.Drawing.Size(56, 23);
			this.txtAge.TabIndex = 44;
			this.txtAge.TabStop = false;
			this.txtAge.Text = "";
			this.txtAge.Visible = false;
			this.txtAge.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAge_KeyPress);
			// 
			// label60
			// 
			this.label60.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label60.Location = new System.Drawing.Point(232, 144);
			this.label60.Name = "label60";
			this.label60.Size = new System.Drawing.Size(200, 23);
			this.label60.TabIndex = 43;
			this.label60.Text = "（如生日不详，请填写年龄：";
			this.label60.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label60.Visible = false;
			// 
			// label58
			// 
			this.label58.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label58.Location = new System.Drawing.Point(408, 128);
			this.label58.Name = "label58";
			this.label58.Size = new System.Drawing.Size(80, 23);
			this.label58.TabIndex = 50;
			this.label58.Text = "工作单位：";
			this.label58.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label58.Visible = false;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label3.Location = new System.Drawing.Point(288, 144);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 23);
			this.label3.TabIndex = 100275;
			this.label3.Text = "工种：";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label3.Visible = false;
			// 
			// rdbDay
			// 
			this.rdbDay.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.rdbDay.Location = new System.Drawing.Point(632, 144);
			this.rdbDay.Name = "rdbDay";
			this.rdbDay.Size = new System.Drawing.Size(56, 24);
			this.rdbDay.TabIndex = 100000;
			this.rdbDay.Text = "天）";
			this.rdbDay.Visible = false;
			// 
			// label50
			// 
			this.label50.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label50.Location = new System.Drawing.Point(80, 120);
			this.label50.Name = "label50";
			this.label50.Size = new System.Drawing.Size(128, 24);
			this.label50.TabIndex = 70;
			this.label50.Text = "常住地址(如与户口";
			this.label50.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label50.Visible = false;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label1.Location = new System.Drawing.Point(80, 136);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(128, 24);
			this.label1.TabIndex = 100372;
			this.label1.Text = "所在地不同者填写)";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label1.Visible = false;
			// 
			// txtWorkPlace
			// 
			this.txtWorkPlace.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.txtWorkPlace.Location = new System.Drawing.Point(240, 96);
			this.txtWorkPlace.Name = "txtWorkPlace";
			this.txtWorkPlace.Size = new System.Drawing.Size(192, 23);
			this.txtWorkPlace.TabIndex = 100294;
			this.txtWorkPlace.Text = "";
			this.txtWorkPlace.Visible = false;
			this.txtWorkPlace.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtWorkPlace_KeyPress);
			// 
			// label51
			// 
			this.label51.Location = new System.Drawing.Point(576, 96);
			this.label51.Name = "label51";
			this.label51.Size = new System.Drawing.Size(80, 23);
			this.label51.TabIndex = 100381;
			this.label51.Text = "退卡原因：";
			this.label51.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label51.Visible = false;
			// 
			// txtCase
			// 
			this.txtCase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtCase.Enabled = false;
			this.txtCase.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.txtCase.Location = new System.Drawing.Point(368, 176);
			this.txtCase.Name = "txtCase";
			this.txtCase.Size = new System.Drawing.Size(184, 23);
			this.txtCase.TabIndex = 100380;
			this.txtCase.TabStop = false;
			this.txtCase.Text = "";
			this.txtCase.Visible = false;
			// 
			// label8
			// 
			this.label8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label8.ForeColor = System.Drawing.Color.Black;
			this.label8.Location = new System.Drawing.Point(72, 96);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(48, 23);
			this.label8.TabIndex = 100287;
			this.label8.Text = "门牌号";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label8.Visible = false;
			// 
			// txtWorkName
			// 
			this.txtWorkName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtWorkName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.txtWorkName.Location = new System.Drawing.Point(544, 112);
			this.txtWorkName.MaxLength = 100;
			this.txtWorkName.Name = "txtWorkName";
			this.txtWorkName.Size = new System.Drawing.Size(176, 23);
			this.txtWorkName.TabIndex = 15;
			this.txtWorkName.Text = "";
			this.txtWorkName.Visible = false;
			this.txtWorkName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtWorkName_KeyPress);
			// 
			// label35
			// 
			this.label35.Location = new System.Drawing.Point(48, 88);
			this.label35.Name = "label35";
			this.label35.Size = new System.Drawing.Size(80, 23);
			this.label35.TabIndex = 42;
			this.label35.Text = "退卡原因：";
			this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label6
			// 
			this.label6.BackColor = System.Drawing.Color.White;
			this.label6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label6.Location = new System.Drawing.Point(40, 112);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(48, 23);
			this.label6.TabIndex = 37;
			this.label6.Text = "备注：";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label37
			// 
			this.label37.BackColor = System.Drawing.Color.White;
			this.label37.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label37.Location = new System.Drawing.Point(424, 8);
			this.label37.Name = "label37";
			this.label37.Size = new System.Drawing.Size(80, 23);
			this.label37.TabIndex = 4;
			this.label37.Text = "填卡时间：";
			this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label39
			// 
			this.label39.BackColor = System.Drawing.Color.White;
			this.label39.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label39.Location = new System.Drawing.Point(216, 8);
			this.label39.Name = "label39";
			this.label39.Size = new System.Drawing.Size(80, 23);
			this.label39.TabIndex = 2;
			this.label39.Text = "报告科室：";
			this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label41
			// 
			this.label41.BackColor = System.Drawing.Color.DimGray;
			this.label41.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label41.Location = new System.Drawing.Point(6, 7);
			this.label41.Name = "label41";
			this.label41.Size = new System.Drawing.Size(80, 23);
			this.label41.TabIndex = 0;
			this.label41.Text = "报告医生：";
			this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ucReportCancerRegister
			// 
			this.AutoScroll = true;
			this.BackColor = System.Drawing.Color.LightBlue;
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.panel7);
			this.Controls.Add(this.panel6);
			this.Name = "ucReportCancerRegister";
			this.Size = new System.Drawing.Size(792, 1056);
			this.panel2.ResumeLayout(false);
			this.Printpanel.ResumeLayout(false);
			this.panelTitle.ResumeLayout(false);
			this.panelPatientInfo.ResumeLayout(false);
			this.panel8.ResumeLayout(false);
			this.panel5.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region 变量属性

		//DCP.ucCancerAddition uc ;

		private string type;
		/// <summary>
		/// 类型（C门诊 I住院 O其它）
		/// </summary>
		public string Type
		{
			get
			{
				return type;
			}
			set
			{
				type = value;
				switch(value)
				{
					case "C":
						this.label67.Text = "门诊卡号：";
						this.lb_patientType.Text="门诊";
						this.cmbSource.Text = "门诊";
						break;
					case "I":
						this.label67.Text = "住 院 号：";
						this.lb_patientType.Text="住院";
						this.cmbSource.Text = "住院";
						break;
					case "O":
						this.label67.Text = "患 者 号：";
						this.lb_patientType.Text="其他";
						this.cmbSource.Text = "其他";
						break;
					default:
						break;
				}
			}
		}

		private string state;
		/// <summary>
		/// 报告卡状态
		/// </summary>
		public string State
		{
			get
			{
				return state;
			}
			set
			{
				state = value;				
				this.lbState.Text = "初次报卡";					
			}
		}

		private string infectCode;
		/// <summary>
		/// 传染病编号 根据诊断传入
		/// </summary>
		public string InfectCode
		{
			get
			{
				return infectCode;
			}
			set
			{
				this.infectCode = value;
				if(value == null || value == "")
				{
					this.cmbInfectionClass.Enabled = true;
				}
			}
		}
		private bool isRenew = false;
		/// <summary>
		/// 是否订正 如果是订正是在copy原卡的基础上insert新卡
		/// 如果是修改在在原卡基础上update
		/// </summary>
		public bool IsRenew
		{
			get
			{
				return isRenew;
			}
			set
			{
				isRenew = value;
			}
		}
		private bool isLisResult = false;

		/// <summary>
		/// 是否lis阳性结果报告
		/// </summary>
		public bool IsLisResult
		{
			get
			{
				return this.isLisResult;
			}
			set
			{
				this.isLisResult = value;
			}
		}

		//		private FS.HISFC.Models.HealthCare.LisResult lisResultObject = new FS.HISFC.Models.HealthCare.LisResult();
//		
//		/// <summary>
//		/// lis结果提示信息
//		/// </summary>
//		public FS.HISFC.Models.HealthCare.LisResult LisResultObject
//		{
//			get
//			{
//				return lisResultObject;
//			}
//			set
//			{
//				lisResultObject = value;
//			}
//		}

		/// <summary>
		/// uc状态[0初始化]
		/// </summary>
		//private int ucState = 0;
		private string diagNose = "";
		/// <summary>
		/// 诊断
		/// </summary>
		public string DiagNose
		{
			get
			{
				return this.diagNose;
			}
			set
			{
				this.diagNose = value;
			}
		}
		/// <summary>
		/// 所有传染病，用于弹出窗口
		/// </summary>
		private ArrayList alInfectItem = new ArrayList();

		/// <summary>
		/// 所有传染病，用于下拉
		/// </summary>
		private ArrayList alinfection = new ArrayList();

		/// <summary>
		/// 需要附卡的疾病
		/// </summary>
		private System.Collections.Hashtable hshNeedAdd;
		/// <summary>
		/// 传染病的类型[甲乙丙等]，检测选择传染病时是否选择了类型
		/// </summary>
		private System.Collections.Hashtable hshInfectClass;
		/// <summary>
		/// 需要报告性病卡的疾病
		/// </summary>
		private System.Collections.Hashtable hshNeedSexReport;
		/// <summary>
		/// 需要采血送检
		/// </summary>
		private System.Collections.Hashtable hshNeedCheckedBlood;
		/// <summary>
		/// 需要二级病例分类
		/// </summary>
		private System.Collections.Hashtable hshNeedCaseTwo;
		/// <summary>
		/// 需要电话报告的疾病
		/// </summary>
		private System.Collections.Hashtable hshNeedTelInfect;
		/// <summary>
		/// 需要结核病转诊单的疾病
		/// </summary>
		private System.Collections.Hashtable hshNeedBill;
		/// <summary>
		/// 需要备注的疾病
		/// </summary>
		private System.Collections.Hashtable hshNeedMemo;
		/// <summary>
		/// 新生儿破伤风
		/// </summary>
		private System.Collections.Hashtable hshLitteChild;
		/// <summary>
		/// 患者职业为学生[应提示填写学校机构之类]
		/// </summary>
		private System.Collections.Hashtable hshStudent;
		/// <summary>
		/// 需要二级名称的性病
		/// </summary>
		private System.Collections.Hashtable hshSexNeedGradeTwo;
		/// <summary>
		/// 需要描述的人群分类
		/// </summary>
		private System.Collections.Hashtable hshPatientTyepNeedDesc;


		private FS.FrameWork.Models.NeuObject myPriDept = new FS.FrameWork.Models.NeuObject();	
		/// <summary>
		/// 权限科室
		/// </summary>
		public FS.FrameWork.Models.NeuObject MyPriDept
		{
			get
			{
				//				if(myPriDept.ID == "" || myPriDept.ID == null)
				//				{
				//					myPriDept = ((FS.HISFC.Models.Base.Employee)this.myReport.Operator).Dept;
				//				}
				return myPriDept;
			}
			set
			{
				myPriDept = value;
			}
		}
		private bool isAdvance = false;
        public FS.SOC.HISFC.Components.DCP.CancerReport.ucCancerAddition uc = new FS.SOC.HISFC.Components.DCP.CancerReport.ucCancerAddition();
		/// <summary>
		/// 是否有高级权限，保健科都有
		/// </summary>
		public bool IsAdvance
		{
			get
			{
				return isAdvance;
			}
			set
			{
				this.isAdvance = value;
			}
		}

		/// <summary>
		/// 患者科室
		/// </summary>
		public FS.FrameWork.Models.NeuObject PatientDept = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// 操作员
		/// </summary>
		public FS.FrameWork.Models.NeuObject PriOper = new FS.FrameWork.Models.NeuObject();
		private FS.SOC.HISFC.BizLogic.DCP.DiseaseReport myReport = new FS.SOC.HISFC.BizLogic.DCP.DiseaseReport();

		private FS.SOC.HISFC.BizLogic.DCP.CancerAdd cancerAddMrg = new FS.SOC.HISFC.BizLogic.DCP.CancerAdd();
		private FS.SOC.HISFC.BizLogic.DCP.CancerExt cancerExtMrg = new FS.SOC.HISFC.BizLogic.DCP.CancerExt();
		
		/// <summary>
		/// 科室
		/// </summary>
		private FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();
		/// <summary>
		/// 员工
		/// </summary>
		private FS.FrameWork.Public.ObjectHelper employHelper = new FS.FrameWork.Public.ObjectHelper();
		
		private ArrayList alCity = new ArrayList();
		private ArrayList alCouty = new ArrayList();
		private ArrayList alTown = new ArrayList();

//		private string myProvince = "广东省";
//		private string myCity = "广州市";
//		private string myCounty = "越秀区";

        private FS.SOC.HISFC.Components.DCP.Classes.Function function = new  FS.SOC.HISFC.Components.DCP.Classes.Function();

		#endregion
		private FS.HISFC.DCP.Object.CancerAdd cancerAdd=new FS.HISFC.DCP.Object.CancerAdd();
        private FS.HISFC.DCP.Object.CancerAddExt cancerExt = new FS.HISFC.DCP.Object.CancerAddExt();
        //private  FS.HISFC.BizLogic.Case.ICDDML  diagnoseICDDML = new FS.HISFC.BizLogic.Case.ICDDML();
		
		
		/// <summary>
		/// 患者管理类
		/// </summary>
		private FS.SOC.HISFC.BizProcess.DCP.Patient patientMgr=new  FS.SOC.HISFC.BizProcess.DCP.Patient();
		
		#region 基本方法

		#region 初始化
		/// <summary>
		/// 初始化疾病
		/// </summary>
		private void initInfections()
		{
			FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();
			//传染病的类型
			ArrayList alInfectClass = new ArrayList();
			
			alInfectClass.AddRange(conMgr.GetList("INFECTCLASS"));
			if(alInfectClass.Count < 1)
			{
				return;
			}
			//需要附卡的传染病
			 this.hshNeedAdd = new Hashtable();
			//类型
			this.hshInfectClass = new Hashtable();
			//需要报性病卡的疾病
			this.hshNeedSexReport = new Hashtable();
			//需要采血送检的疾病
			this.hshNeedCheckedBlood = new Hashtable();
			//需要二级病例的疾病
			this.hshNeedCaseTwo = new Hashtable();
			//需要电话报告的疾病
			this.hshNeedTelInfect = new Hashtable();
			//需要填写结核病转诊单的疾病
			this.hshNeedBill = new Hashtable();
			//新生儿破伤风
			this.hshLitteChild = new Hashtable();
			//需要二级名称的性病
			this.hshSexNeedGradeTwo = new Hashtable();
			//需要人群分类描述
			this.hshPatientTyepNeedDesc = new Hashtable();
			//需要备注
			this.hshNeedMemo = new Hashtable();

			//根据类型获取传染病
			int index = 1;
			foreach(FS.HISFC.Models.Base.Const infectclass in alInfectClass)
			{
				ArrayList al = new ArrayList();
				ArrayList alItem = new ArrayList();
			

				infectclass.Name = "--"+infectclass.Name+"--";
				infectclass.Name = infectclass.Name.PadLeft(13,' ');
				al.Add(infectclass);
				if(index == 1)
				{
					FS.HISFC.Models.Base.Const o = new FS.HISFC.Models.Base.Const();
					o.ID = "####";
					o.Name = "--请选择--";
					al.Insert(0,o);
					index++;
				}
				alItem = conMgr.GetList(infectclass.ID);

				al.AddRange(alItem);
				alInfectItem.AddRange(alItem);

				hshInfectClass.Add(infectclass.ID,null);
				foreach(FS.HISFC.Models.Base.Const infect in al)
				{
					//名称过长，维护在备注里，在此交换
					if(infect.Name.IndexOf("备注",0) != -1)
					{
						infect.Name = infect.Memo;
						infect .Memo = "";
					}
					if(infect.Memo.IndexOf("需附卡",0) != -1)
					{
						hshNeedAdd.Add(infect.ID,null);
					}
					if(infect.Memo.IndexOf("性病",0) != -1)
					{
						hshNeedSexReport.Add(infect.ID,null);
					}
					if(infect.Memo.IndexOf("需备注") != -1)
					{
						hshNeedMemo.Add(infect.ID,null);
					}
					//性病二级名称
					if(infect.Memo.IndexOf("二级名称",0) != -1)
					{
						hshSexNeedGradeTwo.Add(infect.ID,null);
					}
					if(infect.Memo.IndexOf("需采血送检",0) != -1)
					{
						hshNeedCheckedBlood.Add(infect.ID,null);
					}
					//二级病例分类
					if(infect.Memo.IndexOf("病例分类",0) != -1)
					{
						hshNeedCaseTwo.Add(infect.ID,null);
					}
					//电话通知
					if(infect.Memo.IndexOf("电话通知",0) != -1)
					{
						hshNeedTelInfect.Add(infect.ID,null);
					}
					//结核病转诊单
					if(infect.Memo.IndexOf("需转诊单",0) != -1)
					{
						hshNeedBill.Add(infect.ID,null);
					}
					if(infect.Memo.IndexOf("新生儿破伤风",0) != -1 || infect.Name.IndexOf("新生儿破伤风",0) != -1)
					{
						hshLitteChild.Add(infect.ID,null);
					}
					
				}
				alinfection.AddRange(al);
				FS.FrameWork.Models.NeuObject ob = new FS.FrameWork.Models.NeuObject();
				ob.ID = "####";
				ob.Name = "    ";
				alinfection.Add(ob);
			}	
			this.cmbInfectionClass.AddItems(alInfectItem);
			this.cmbInfectionClass.DataSource = alinfection;
			this.cmbInfectionClass.DisplayMember = "Name";
			this.cmbInfectionClass.ValueMember = "ID";	
		}




		/// <summary>
		/// 初始化疾病
		/// </summary>
		private void initInfectionsCancer()
		{

			FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();
			//传染病的类型
           
			ArrayList alInfectClass = new ArrayList();//ooooooo
            //alInfectClass.AddRange(this.diagnoseICDDML.Query(FS.HISFC.Models.HealthRecord.EnumServer.ICDTypes.ICD10,FS.HISFC.Models.HealthRecord.EnumServer.QueryTypes.Valid));
			//alInfectClass.AddRange(conMgr.GetList("INFECTCLASS"));
			if(alInfectClass.Count < 1)
			{
				return;
			}
//			//需要附卡的传染病
//			this.hshNeedAdd = new Hashtable();
//			//类型
//			this.hshInfectClass = new Hashtable();
//			//需要报性病卡的疾病
//			this.hshNeedSexReport = new Hashtable();
//			//需要采血送检的疾病
//			this.hshNeedCheckedBlood = new Hashtable();
//			//需要二级病例的疾病
//			this.hshNeedCaseTwo = new Hashtable();
//			//需要电话报告的疾病
//			this.hshNeedTelInfect = new Hashtable();
//			//需要填写结核病转诊单的疾病
//			this.hshNeedBill = new Hashtable();
//			//新生儿破伤风
//			this.hshLitteChild = new Hashtable();
//			//需要二级名称的性病
//			this.hshSexNeedGradeTwo = new Hashtable();
//			//需要人群分类描述
//			this.hshPatientTyepNeedDesc = new Hashtable();
//			//需要备注
//			this.hshNeedMemo = new Hashtable();

			//根据类型获取传染病
//			int index = 1;
//			foreach(FS.HISFC.Models.Base.Const infectclass in alInfectClass)
//			{
//				ArrayList al = new ArrayList();
//				ArrayList alItem = new ArrayList();
//				
//				infectclass.Name = "--"+infectclass.Name+"--";
//				infectclass.Name = infectclass.Name.PadLeft(13,' ');
//				al.Add(infectclass);
//				if(index == 1)
//				{
//					FS.HISFC.Models.Base.Const o = new FS.HISFC.Models.Base.Const();
//					o.ID = "####";
//					o.Name = "--请选择--";
//					al.Insert(0,o);
//					index++;
//				}
				//alItem = conMgr.GetList(infectclass.ID);
//               // alItem = diagnoseICDDML.GetList(infectclass.ID);
//				al.AddRange(alItem);
//				alInfectItem.AddRange(alItem);
//
//				hshInfectClass.Add(infectclass.ID,null);
//				foreach(FS.HISFC.Models.Base.Const infect in al)
//				{
//					//名称过长，维护在备注里，在此交换
//					if(infect.Name.IndexOf("备注",0) != -1)
//					{
//						infect.Name = infect.Memo;
//						infect .Memo = "";
//					}
//					if(infect.Memo.IndexOf("需附卡",0) != -1)
//					{
//						hshNeedAdd.Add(infect.ID,null);
//					}
//					if(infect.Memo.IndexOf("性病",0) != -1)
//					{
//						hshNeedSexReport.Add(infect.ID,null);
//					}
//					if(infect.Memo.IndexOf("需备注") != -1)
//					{
//						hshNeedMemo.Add(infect.ID,null);
//					}
//					//性病二级名称
//					if(infect.Memo.IndexOf("二级名称",0) != -1)
//					{
//						hshSexNeedGradeTwo.Add(infect.ID,null);
//					}
//					if(infect.Memo.IndexOf("需采血送检",0) != -1)
//					{
//						hshNeedCheckedBlood.Add(infect.ID,null);
//					}
//					//二级病例分类
//					if(infect.Memo.IndexOf("病例分类",0) != -1)
//					{
//						hshNeedCaseTwo.Add(infect.ID,null);
//					}
//					//电话通知
//					if(infect.Memo.IndexOf("电话通知",0) != -1)
//					{
//						hshNeedTelInfect.Add(infect.ID,null);
//					}
//					//结核病转诊单
//					if(infect.Memo.IndexOf("需转诊单",0) != -1)
//					{
//						hshNeedBill.Add(infect.ID,null);
//					}
//					if(infect.Memo.IndexOf("新生儿破伤风",0) != -1 || infect.Name.IndexOf("新生儿破伤风",0) != -1)
//					{
//						hshLitteChild.Add(infect.ID,null);
//					}
//					
//				}
//				alinfection.AddRange(al);
//				FS.FrameWork.Models.NeuObject ob = new FS.FrameWork.Models.NeuObject();
//				ob.ID = "####";
//				ob.Name = "    ";
//				alinfection.Add(ob);
//			}	
			FS.HISFC.Models.Base.Const o = new FS.HISFC.Models.Base.Const();
			o.ID = "####";
			o.Name = "--请选择--";
			alInfectClass.Insert(0,o);
			this.cmbInfectionClass.AddItems(alInfectClass);
			this.cmbInfectionClass.DataSource = alInfectClass;
			this.cmbInfectionClass.DisplayMember = "Name";
			this.cmbInfectionClass.ValueMember = "ID";	
		}




		/// <summary>
		/// 初始化地址
		/// </summary>
		private void initAddress()
		{
			//			FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();
			//			
			//			FS.HISFC.Models.Base.Const obj = new FS.HISFC.Models.Base.Const();
			//			ArrayList alprovince = new ArrayList();
			//			obj.ID = "####";
			//			obj.Name ="--请选择--";

			//FS.HISFC.BizLogic.Manager.Controler contrMgr = new FS.HISFC.BizLogic.Manager.Controler();
			//默认省市区
			//			this.myProvince = contrMgr.QueryControlerInfo("YB0002");
			//			this.myCity = contrMgr.QueryControlerInfo("YB0003");
			//			this.myCounty = contrMgr.QueryControlerInfo("YB0004");

			//			//省
			//			alprovince.Add(obj);
			//			alprovince.AddRange(conMgr.GetList("PROVINCE"));			
			//			this.cmbprovince.AddItems(alprovince);
			//			this.cmbprovince.DataSource = alprovince;
			//			this.cmbprovince.ValueMember = "ID";
			//			this.cmbprovince.DisplayMember = "Name";
			//
			//			//市
			//			ArrayList alcity = new ArrayList();
			//			alcity.Add(obj);
			//			alcity.AddRange(conMgr.GetList("CITY"));		
			//			this.cmbCity.AddItems(alcity);
			//			this.cmbCity.DataSource = alcity;
			//			this.cmbCity.DisplayMember = "Name";
			//			this.cmbCity.ValueMember = "ID";
			//			//this.alCity = alcity;
			//
			//			//县
			//			ArrayList alcouty = new ArrayList();
			//			alcouty.Add(obj);
			//			alcouty.AddRange(conMgr.GetList("COUNTY"));			
			//			this.cmbCouty.AddItems(alcouty);
			//			this.cmbCouty.DataSource = alcouty;
			//			this.cmbCouty.DisplayMember = "Name";
			//			this.cmbCouty.ValueMember = "ID";
			//			//this.alCouty = alcouty;
			//
			//			//镇
			//			ArrayList altown = new ArrayList();
			//			altown.Add(obj);
			//			altown.AddRange(conMgr.GetList("TOWN"));			
			//			this.cmbTown.AddItems(altown);
			//			this.cmbTown.DataSource = altown;
			//			this.cmbTown.DisplayMember = "Name";
			//			this.cmbTown.ValueMember = "ID";
			//this.alTown = altown;

		}

		/// <summary>
		/// 初始化职业
		/// </summary>
		private void initProfession()
		{
			FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();

			FS.HISFC.Models.Base.Const obj = new FS.HISFC.Models.Base.Const();
			ArrayList alpro = new ArrayList();
			obj.ID = "####";
			obj.Name ="--请选择--";

			//职业
			alpro.Add(obj);
			//alpro.AddRange(conMgr.GetList("PATIENTJOB"));
			alpro.AddRange(conMgr.GetList("PROFESSION"));
			this.hshStudent = new Hashtable();

			foreach(FS.HISFC.Models.Base.Const con in alpro)
			{
				if(con.Name.IndexOf("幼托",0) != -1 || con.Memo.IndexOf("儿童",0) != -1)
				{
					hshStudent.Add(con.ID,"托幼机构及班级名称");
				}
				if(con.Name.IndexOf("学生",0) != -1 || con.Memo.IndexOf("学生",0) != -1)
				{
					hshStudent.Add(con.ID,"学校及班级名称");
				}
			}
			//this.cmbProfession.DataSource = alpro;
			this.cmbProfession.AddItems(alpro);
			this.cmbProfession.ValueMember = "ID";
			this.cmbProfession.DisplayMember = "Name";
			this.cmbProfession.DataSource=alpro;
		}

		#region 
        /// <summary>
        /// 初始化民族
        /// </summary>
		private void initNation()
		{
			FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();

			FS.HISFC.Models.Base.Const obj = new FS.HISFC.Models.Base.Const();
			ArrayList alpro = new ArrayList();
			obj.ID = "####";
			obj.Name ="--请选择--";

			//民族
			alpro.Add(obj);
			alpro.AddRange(conMgr.GetList("NATION"));
			this.hshStudent = new Hashtable();
//        	this.cmbNation.DataSource = alpro;
			this.cmbNation.AddItems(alpro);
			this.cmbNation.ValueMember = "ID";
			this.cmbNation.DisplayMember = "Name";
			this.cmbNation.DataSource = alpro;
		}

		#endregion

		#region 
		
		/// <summary>
		/// 初始化婚姻
		/// </summary>

		private void initMarriage()
		{
			FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();

			FS.HISFC.Models.Base.Const obj = new FS.HISFC.Models.Base.Const();
			ArrayList alpro = new ArrayList();
			obj.ID = "####";
			obj.Name ="--请选择--";

			//婚姻
			alpro.Add(obj);
			alpro.AddRange(conMgr.GetList("MARRIAGY"));
			this.hshStudent = new Hashtable();
			this.cmbMarrige.AddItems(alpro);
			this.cmbMarrige.ValueMember = "ID";
			this.cmbMarrige.DisplayMember = "Name";
			this.cmbMarrige.DataSource=alpro;
		}

		#endregion

		


		/// <summary>
		/// 初始化忽略信息，处理一些必填项的初始化
		/// </summary>
		private void initIgnoreInfo()
		{
			
		}
		/// <summary>
		/// 初始化
		/// </summary>
		public void init()
		{
			FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在初始化数据,请稍候...");
			Application.DoEvents();
			
			//this.panelAdditionMain.Click += new EventHandler(panelAdditionMain_Click);
			try
			{
				//this.IsDesigner = false;

				//this.initInfections();
                this.initInfectionsCancer();
				this.initAddress();
				this.initProfession();
				this.initNation();
				this.initMarriage();
				
				//this.initCaseClass();
				//this.initAddtionReport();
				//科室、员工帮助类 便于快速获取保卡人信息
				FS.HISFC.BizLogic.Manager.Department dpt = new FS.HISFC.BizLogic.Manager.Department();
				this.deptHelper.ArrayObject = dpt.GetDeptmentAll();
				FS.HISFC.BizLogic.Manager.Person p = new FS.HISFC.BizLogic.Manager.Person();
				this.employHelper.ArrayObject = p.GetEmployeeAll();
				//发病、诊断、死亡时间				
				//this.dtDiaDate.Value  = dpt.GetDateTimeFromSysDateTime();
				this.cmbSource.Text = "门诊";
			}
			catch(Exception ex)
			{
				this.showMyMessageBox("初始化失败！" + ex.Message,"err");
			}
			finally
			{
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();  
			}
		}
		#endregion


		#region 保存[新加或者修改]
		/// <summary>
		/// 保存
		/// </summary>
		/// <returns>-1失败</returns>
		public int SaveReport()
		{
			#region 报卡信息获取、验证
				FS.HISFC.DCP.Object.CommonReport mainreport = new FS.HISFC.DCP.Object.CommonReport();
			FS.HISFC.DCP.Object.CommonReport report = new FS.HISFC.DCP.Object.CommonReport();
			if(this.myGetReportData(ref report) == -1)
			{
				return -1;
			}
			//重复卡检测
			if(report.ID == null || report.ID == "")
			{
				ArrayList al = new ArrayList();
				al = this.myReport.GetPatientReportedCommonReport(report.Patient, report.Disease.ID);
				if(al == null)
				{
					this.showMyMessageBox("检测是否重复报卡时获取历史报卡信息失败","err");
					return -1;
				}
				if(al.Count > 0)
				{
					string info = "本年度该患者可能已经报卡：";
					foreach(FS.HISFC.DCP.Object.CommonReport r in al)
					{
						if(r.ID == report.ID)
						{
							continue;
						}
						info += "\n    报卡编号：" + r.ReportNO;
						info += "\n    疾病名称：" + r.Disease.Name;
						info += "\n    报卡日期：" + r.ReportTime.ToString();
						info += "\n";
					}
					info += "\n是否继续上报？";
					if(!this.showMyMessageBox(info, "info"))
					{
						return 0;
					}
				}
			}
			
			#endregion

			#region 用户确认
			if(cmbInfectionClass.Enabled == true 
				&& MessageBox.Show(this,"您选择了【"+report.Disease.Name+"】\n保存后电子报卡由系统自动上传【预防保健科】\n确认保存吗？","温馨提示>>",System.Windows.Forms.MessageBoxButtons.YesNo,System.Windows.Forms.MessageBoxIcon.Information)
				== System.Windows.Forms.DialogResult.No)
			{
				return -1;
			}
			#endregion

			#region 订正时原卡信息获取
			if(this.IsRenew)
			{
				mainreport = this.myReport.GetCommonReportByID(report.ID);
				report.State = "0";
				report.CorrectedReportNO = mainreport.ReportNO;
				//备注中加入原病名
				if(report.Memo.IndexOf("//原病名["+mainreport.Disease.Name + "]") == -1)
				{
					report.Memo += "//原病名["+mainreport.Disease.Name + "]";
				}
				 
				mainreport.ExtendInfo3 = "已订正";
				mainreport.CorrectFlag = "1";
			}
			#endregion

			#region 数据保存
//			if(this.hshNeedBill.Contains(report.Disease.ID))
//			{
//				if(report.Memo != string.Empty)
//				{
//					if(report.Memo.IndexOf("已转诊") == -1)
//					{
//						report.Memo = "已转诊\\\\"+report.Memo;
//					}
//				}
//				else
//				{
//					report.Memo = "已转诊\\\\";
//				}
//			}
			//
			


			//备注字数控制
			if(report.Memo.Length > 100)
			{
				this.showMyMessageBox("报告卡保存失败\n\n备注：" + report.Memo + "\n\n字数过多，请控制在100字内\n","err");
				this.rtxtMemo.Text = report.Memo;
				return -1;
			}
			FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在保存,请稍候....");
			Application.DoEvents();

			
			FS.FrameWork.Management.PublicTrans.BeginTransaction();
			this.myReport.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
			this.cancerAddMrg.SetTrans(FS.FrameWork.Management.PublicTrans.Trans); 
			this.cancerExtMrg.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
			
			//报告编号为空 作为新卡插入数据库
			if(report.ID == null || report.ID == "" || this.IsRenew)
			{
				#region 新卡插入处理
				
				if(this.myReport.InsertCommonReport(report) == -1)
				{
                    FS.FrameWork.Management.PublicTrans.RollBack();
					FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
					this.showMyMessageBox("报告卡保存失败>>" + this.myReport.Err,"err");
					return -1;
				}
                
				//肿瘤报卡插入
				if ( report.Cancer_Flag=="1" )
				{
					
					FS.HISFC.DCP.Object.CancerAdd cancerAdd = this.SaveCancerAddData(report.ReportNO);
					////先插入肿瘤报卡扩展多选的内容
					if(cancerAdd.ArrayListExt.Count>0)
					{    
						

						 
						foreach( FS.HISFC.DCP.Object.CancerAddExt c in cancerAdd.ArrayListExt )
						{   
							if(this.cancerExtMrg.InsertCancerExt(c) == -1)
							{
                                FS.FrameWork.Management.PublicTrans.RollBack();
								FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
								this.showMyMessageBox("肿瘤扩展报告卡保存失败>>" + this.cancerExtMrg.Err,"err");
								return -1;
							}
						}
						
					}

					if(this.cancerAddMrg.InsertCancerAdd(cancerAdd) == -1)
					{
                        FS.FrameWork.Management.PublicTrans.RollBack();
						FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
						this.showMyMessageBox("肿瘤报告卡保存失败>>" + this.cancerAddMrg.Err,"err");
						return -1;
					}
					 
				}




				#endregion
				//如果是订正 需要更新原卡
				if(this.IsRenew)
				{
					mainreport.CorrectReportNO = report.ReportNO;
					if(this.myReport.UpdateCommonReport(mainreport) != 1)
					{
						FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        FS.FrameWork.Management.PublicTrans.RollBack();
						this.showMyMessageBox("报告卡保存失败>>" + this.myReport.Err,"err");
						return -1;
					}
				}
			}
			else
			{
				#region 旧卡更新处理

				if(this.myReport.UpdateCommonReport(report) == -1)
				{
                    FS.FrameWork.Management.PublicTrans.RollBack();
					FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
					this.showMyMessageBox("报告卡保存失败>>"+this.myReport.Err,"err");
					return -1;
				}	
				#endregion
                
				#region//旧卡附卡更新处理
				////更新前先删除肿瘤报卡扩展多选的内容
				this.cancerAddMrg.DeleteCancerAdd(report.ReportNO);
				this.cancerExtMrg.DeleteCancerAddExtReport(report.ReportNO);
					#region 更新肿瘤报卡(先删除后插入不使用Update)
						FS.HISFC.DCP.Object.CancerAdd cancerAdd = this.SaveCancerAddData(report.ReportNO);
					
						if(cancerAdd.ArrayListExt.Count>0)
						{    
							foreach( FS.HISFC.DCP.Object.CancerAddExt c in cancerAdd.ArrayListExt )
							{   
								if(this.cancerExtMrg.InsertCancerExt(c) == -1)
								{
                                    FS.FrameWork.Management.PublicTrans.RollBack();
									FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
									this.showMyMessageBox("肿瘤扩展报告卡CancerExt更新时插入保存到表中失败>>" + this.cancerExtMrg.Err,"err");
									return -1;
								}
							}
						
						}

						if(this.cancerAddMrg.InsertCancerAdd(cancerAdd) == -1)
						{
                            FS.FrameWork.Management.PublicTrans.RollBack();
							FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
							this.showMyMessageBox("肿瘤报告卡CancerAdd更新时插入保存到表中失败>>" + this.cancerAddMrg.Err,"err");
							return -1;
						}
					 
					

					#endregion

		
				#endregion
			}







            FS.FrameWork.Management.PublicTrans.Commit();
			FS.FrameWork.WinForms.Classes.Function.HideWaitForm();	
			#endregion
			
			#region 附加提示信息
			this.myGetMessage(report);	
			//this.print(false);
			#endregion

			return 0;
		}

		/// <summary>
		/// 附加提示信息
		/// </summary>
		/// <param name="report">疾病编号</param>
		/// <returns>附加信息</returns>
		private void myGetMessage(FS.HISFC.DCP.Object.CommonReport  report)
		{
			string message = "报告卡成功保存并上报!\n\n";
//			string diseaseID = report.Disease.ID;
//			FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();
//			
//			//周末电话开关
//			ArrayList altemp = new ArrayList();
//			altemp = conMgr.GetAllList("SWITCH");
//			string strtelephone = "";
//			int count = 0;
//			foreach(FS.HISFC.Models.Base.Const conOb in altemp)
//			{
//				count++;
//				if(conOb.ValidState == "0")
//				{
//					if(count == altemp.Count)
//					{
//						strtelephone += conOb.Memo;
//						strtelephone += conOb.Name;
//					}
//					else
//					{
//						strtelephone += conOb.Memo+"\n";
//					}
//				}
//			}
//			
//			if(strtelephone == "")
//			{
//				//电话通知
//				if(this.hshNeedTelInfect.Contains(diseaseID))
//				{
//					ArrayList al = new ArrayList();
//					al = conMgr.GetAllList("MESSAGE");
//					foreach(FS.HISFC.Models.Base.Const con in al)
//					{
//						message += con.Memo + "\n";
//					}
//				}
//			}
			if(message != "" && message != null)
			{
				MessageBox.Show(this,message,"提示>>");
				//return;
			}
		
//			if(strtelephone != "")
//			{
//				MessageBox.Show(this,strtelephone,"温馨提示>>",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Information);				
//			}

		}

		private int haveReport(string patientNO)
		{
			ArrayList al = new ArrayList();
			//al = this.myReport.ge(patientNO);
			return al.Count;
		}

		

		#endregion

		#region 审核作废

		/// <summary>
		/// 报告卡审核
		/// </summary>
		/// <param name="report">实体</param>
		/// <param name="state">状态</param>
		public void ApproveReport(FS.HISFC.DCP.Object.CommonReport report,string state)
		{
			System.DateTime dt = new DateTime();
			dt = this.myReport.GetDateTimeFromSysDateTime();
			frmCaseInput frmCaseInput = new frmCaseInput();
			switch(state)
			{
				case "0"://恢复
					//
					break;
				case "1":
					report.ApproveOper.ID = this.PriOper.ID;
					report.OperCase = "";
					report.ApproveTime = dt;
					break;
				case "2"://审核
					report.ApproveOper.ID = this.PriOper.ID;
					report.ApproveTime = dt;//this.myReport.GetDateTimeFromSysDateTime();
					frmCaseInput.StrTitle = "不合格原因：";
					frmCaseInput.ShowDialog(this);
					if(frmCaseInput.State == 1)
						return;
					report.OperCase = frmCaseInput.StrCase;
					break;
				case "3":
				case "4"://作废
					report.CancelOper.ID = this.PriOper.ID;
					report.CancelTime = dt;//this.myReport.GetDateTimeFromSysDateTime();
					frmCaseInput.StrTitle = "作废原因：";
					frmCaseInput.ShowDialog(this);
					if(frmCaseInput.State == 1)
						return;
					report.Memo += "//"+frmCaseInput.StrCase;
					break;
				default:
					break;
			}

			//操作信息
			report.Oper.ID = this.PriOper.ID;
			report.OperDept.ID = this.MyPriDept.ID;
			report.OperTime = dt;//this.myReport.GetDateTimeFromSysDateTime();
		
			//状态变化后返回 在更改期间有其他人操作
//			if(!this.chechState(report.ID,report.State))
//			{
//				return;
//			}
			//新的状态
			report.State = state;

			//更新数据库
			FS.FrameWork.Management.PublicTrans.BeginTransaction();
			this.myReport.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
			
			if(this.myReport.UpdateCommonReport(report) != -1)
			{
                FS.FrameWork.Management.PublicTrans.Commit();
				this.showMyMessageBox("操作成功！","提示>>");
				if(this.state == "2")
				{
					//发送消息
					//FS.Common.Class.Message.SendMessage("有不合格的传染病报告卡,请查收并修改 编号:"+report.ExtendInfo1,report.DoctorDept.ID);
				}
			}
			else
			{
				this.showMyMessageBox("操作失败！" + this.myReport.Err,"err");
                FS.FrameWork.Management.PublicTrans.RollBack();
			}

		}
		#endregion

		#region 删除
		/// <summary>
		/// 删除报告卡
		/// </summary>
		/// <param name="ID">编号</param>
		public int DeleteReport(string ID)
		{
			System.Windows.Forms.DialogResult dr = new DialogResult();
			dr = MessageBox.Show("确定要删除报告卡吗？\n删除后不能恢复！","提示>>",System.Windows.Forms.MessageBoxButtons.YesNo,System.Windows.Forms.MessageBoxIcon.Warning,System.Windows.Forms.MessageBoxDefaultButton.Button2);
			
			if(dr == System.Windows.Forms.DialogResult.Yes)
			{
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
				this.myReport.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
				if(this.myReport.DeleteCommonReport(ID) != -1)
				{
                    FS.FrameWork.Management.PublicTrans.Commit();
					this.showMyMessageBox("报告卡删除成功!","提示>>");
					return 0;
				}
				else
				{
                    FS.FrameWork.Management.PublicTrans.RollBack();
					this.showMyMessageBox("报告卡删除失败!"+this.myReport.Err,"错误>>");
					return -1;
				}
			}
			return 1;
		}
		#endregion

		#region 打印

		/// <summary>
		/// 打印-需要先保存
		/// </summary>
		public void print()
		{
			this.print(true);
		}

		/// <summary>
		/// 打印
		/// </summary>
		/// <param name="needSave">true 保存后才能打印</param>
		public void print(bool needSave)
		{
			if(needSave && this.lbID.Text == "")				
			{
				this.showMyMessageBox("请先保存","提示>>");
				return;
			}
			//备注的边界消除
			this.rtxtMemo.BorderStyle = System.Windows.Forms.BorderStyle.None;	
			//详细地址的隐藏
			//this.SetAddressInfoVisible(!this.txtSpecialAddress.Visible);
			if (!rdbReback4.Checked)
			{
				this.dtOldDiagnosesTime.Visible = false;
			}
			if (lb_patientType.Text.Trim() == "门诊")
			{
				dateTimePicker2.Visible = false;
			}
		    FS.FrameWork.WinForms.Classes.Print p = new  FS.FrameWork.WinForms.Classes.Print();
			System.Drawing.Printing.PaperSize size = new System.Drawing.Printing.PaperSize("A4",700,1180);
			p.SetPageSize(size);
			//p.ControlBorder = FS.neuFC.Interface.Classes.enuControlBorder.None;
			p.PrintPreview(20,0,this.Printpanel);
			p.IsDataAutoExtend = true;
			p.IsAutoFont = true ;

			//恢复
			this.rtxtMemo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			//this.SetAddressInfoVisible(true);
			
		
		}
		#endregion
		#endregion		
		
		/// <summary>
		/// 报告卡可修改性控制
		/// </summary>
		/// <param name="enable">true Enabled=true</param>
		public void SetEnable(bool enable)
		{

			if(enable)
			{
                FS.SOC.HISFC.Components.DCP.UnionManager un=new UnionManager();
                enable = un.AllowReport();
			}
 			this.panelTitle.Enabled = enable;
 			this.panelPatientInfo.Enabled = enable;
//			this.panelCaseClass.Enabled = enable;
//			this.panelInfectionName.Enabled = enable;
//			this.panelMemo.Enabled = enable;
//			this.panelCaseClass.Enabled = enable;
//			this.panelAddtion.Enabled = enable;
			this.txtCase.Enabled = enable & this.IsAdvance;
		}
		/// <summary>
		/// 清除所有报告信息
		/// </summary>
		/// <param name="isClearNo">false除患者号、Printpanel.Tag、报告编号、状态外都清除</param>
		public void ClearAll(bool isClearNo)
		{
			if(isClearNo)
			{
				this.Printpanel.Tag = null;
				this.lbPatientNo.Text = "";
				this.lbMadicalCardNo.Text="";
				this.lbID.Text = "";
				this.lbState.Text = "";
			}
			this.ClearAll();

			//报告人 报告科室
			this.lbReportDoctor.Text = this.employHelper.GetName(this.PriOper.ID);
			this.lbDoctorDept.Text = this.deptHelper.GetName(this.MyPriDept.ID);
			//报告时间
			this.lbReportTime.Text = this.myReport.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd HH:MM:ss");
			
		}

		/// <summary>
		/// 清除所有文本信息
		/// </summary>
		public void ClearAll()
		{
			try
			{   this.txtCancer_No.Clear();
				this.lbPatientNo.Text=null;

				this.txtPatientName.Clear();
				this.Printpanel.Tag = null;
				this.lbPatientDept.Tag = "";
				this.txtSpecialAddress.Clear();
				this.lbID.Text = "";
				this.txtPatientName.Text = "";
				this.txtPatientID.Clear();

				//性别
				//this.ucState = 0;
				this.cbxMan.Checked = false;
				this.cbxWomen.Checked = false;
				//this.ucState = 1;

				//民族
				this.cmbNation.SelectedIndex=0;
				 //籍贯
				this.txtDistrict.Clear ();
				//工种
				this.txtWorkType.Clear();
				//职业
				this.cmbProfession.SelectedIndex=0;
				//身份证
			    this.txtPatientID.Clear();
				this.cmbMarrige.SelectedIndex=0;

				this.dtBirthDay.Value = this.myReport.GetDateTimeFromSysDateTime();

				this.txtAge.Clear();
				this.rdbYear.Checked = true;
				this.txtWorkPlace.Clear();
				this.txtTelephone.Clear();
				this.txtHandPhone.Clear();
				this.txtDistrict.Clear();
				this.txtRegisterProvince.Clear();
				this.txtRegisterCity.Clear();
				this.txtRegisterCounty.Clear();
				this.txtRegisterTown.Clear();
				this.txtRegisterHouseNO.Clear();
				this.txtPost.Clear();
				this.txtSpecialAddress.Clear ();
				this.txtWorkName.Clear();
				this.txtWorkPlace.Clear ();
				this.txtContactPerson.Clear();
				this.txtContactPersonAddr.Clear();
				this.txtContactPersonTel.Clear();
				this.txtRelationship.Clear();
				this.cmbInfectionClass.SelectedIndex=0;
				this.txtICD.Clear();
				this.dateTimePicker1.Value = this.myReport.GetDateTimeFromSysDateTime();
				this.txtClinicalT.Clear();
                this.txtClinicalN.Clear();
				this.txtClinicalM.Clear();
				this.rdbTNM1.Checked=false;
				this.rdbTNM2.Checked=false;
				this.rdbTNM3.Checked=false;
				this.rdbPathlogyY.Checked=false;
                //this.rdbPathlogyN.Checked=false;
				this.txtPathlogyNo.Clear();
				this.txtPathlogyType.Clear();
				this.txtPathlogyDegree.Clear();
				this.txtICDO.Clear();
				this.chbDiagnoses1.Checked=false;
				this.chbDiagnoses2.Checked=false;
				this.chbDiagnoses3.Checked=false;
				this.chbDiagnoses4.Checked=false;
                this.chbDiagnoses5.Checked=false;
				this.chbDiagnoses6.Checked=false;
				this.chbDiagnoses7.Checked=false;
				this.chbDiagnoses8.Checked=false;
				this.chbDiagnoses9.Checked=false;
				this.chbDiagnoses10.Checked=false;
				this.chbDiagnoses11.Checked=false;
				this.chbDiagnoses12.Checked=false;
				this.chbDiagnoses13.Checked=false;
				this.chbDiagnoses14.Checked=false;
				this.chbDiagnoses15.Checked=false;
                this.chbDiagnoses16.Checked=false;
				this.chbDiagnoses17.Checked=false;
				this.chbDiagnoses18.Checked=false;
				this.chbDiagnoses19.Checked=false;
				this.txtDiagnosesDemo.Clear();
				
				this.rdbTreatY.Checked=false;
                //this.rdbTreatN.Checked=false;
				this.chbTreatment1.Checked=false;
                this.chbTreatment2.Checked=false;
				this.chbTreatment3.Checked=false;
				this.chbTreatment4.Checked=false;
				this.chbTreatment5.Checked=false;
				this.chbTreatment6.Checked=false;
				this.chbTreatment7.Checked=false;
				this.chbTreatment8.Checked=false;
				this.chbTreatment9.Checked=false;
				this.txtTreatmentDemo.Clear();
				this.rdbReback1.Checked=false;
				this.rdbReback2.Checked=false;
				this.rdbReback3.Checked=false;
				this.rdbReback4.Checked=false;
				this.rdbReback5.Checked=false;
				this.txtRebackDemo.Clear();
				this.txtDeathResion.Clear();
				this.txtOldDiagnoses.Clear();
				this.dtOldDiagnosesTime.Value= this.myReport.GetDateTimeFromSysDateTime();
				this.rtxtMemo.Clear();
				this.txtCase.Clear();
				//报告人 报告科室
				this.lbReportDoctor.Text = this.employHelper.GetName(this.PriOper.ID);
				this.lbDoctorDept.Text = this.deptHelper.GetName(this.MyPriDept.ID);
				//报告时间
				this.lbReportTime.Text = this.myReport.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd HH:MM:ss");
			}
			catch
			{
				//
			}
		}


		/// <summary>
		/// 添加患者基本信息
		/// </summary>
		/// <param name="patient">实体FS.HISFC.Models.RADT.Patient</param>
		public void ShowPatienInfo(FS.HISFC.Models.RADT.Patient patient)
		{
			//在新加卡时候自动带出患者信息
			try
			{
				this.DiagNose = patient.ClinicDiagnose;
				//this.txtSpecialAddress.Visible = false;
				if(Type == "I")
				{
					this.lbPatientNo.Text = patient.ID;
				}
				else if(Type == "C")
				{
					this.lbPatientNo.Text = patient.PID.CardNO;
				}
				else
				{
					if(patient.PID.CardNO != "")
					{
						this.lbPatientNo.Text = patient.PID.CardNO;
					}
				}
				this.State = "0";
				this.txtPatientName.Text = patient.Name;
				this.txtPatientID.Text = patient.IDCard;
				this.cbxWomen.Checked = patient.Sex.ID.ToString() == "F"?true:false;
				this.cbxMan.Checked = patient.Sex.ID.ToString() == "M"?true:false;
				this.txtAge.Text = patient.Age;
				try
				{
					this.dtBirthDay.Value = patient.Birthday;
				}
				catch
				{
					this.dtBirthDay.Value = this.myReport.GetDateTimeFromSysDateTime();
					this.txtAge.Text = "";
					this.rdbYear.Checked = true;
				}

                this.txtSpecialAddress.Text=patient.AddressHome;
				this.txtWorkName.Text=patient.CompanyName;
				this.txtDistrict.Text=patient.DIST;
				this.lbMadicalCardNo.Text=patient.SSN;
				//this.txtContactPerson.Text=patient.

				//显示地址
				//this.txtWorkPlace.Text = patient.AddressBusiness;
				this.txtWorkPlace.Text = patient.CompanyName;
				this.txtTelephone.Text = patient.PhoneHome;
				this.lbReportDoctor.Text = this.PriOper.Name;
				this.lbDoctorDept.Text = this.MyPriDept.Name;
				this.lbPatientDept.Tag = patient.User01;

//				if(this.InfectCode != null && this.InfectCode != "")
//				{
//					//医生站诊断对应
//					//如果有指定的传染病编号，传染病不可以更改
//					string[] infectCodes = this.infectCode.Split(',');
//					if(infectCodes.Length == 1)
//					{
//						this.cmbInfectionClass.SelectedValue = this.InfectCode;
//						this.cmbInfectionClass.Enabled = false;
//						if(this.cmbInfectionClass.Text == null || this.cmbInfectionClass.Text == "")
//						{
//							//维护的常数可能对应不上
//							//诊断的自定义码和传染病的编码不对应
//							this.cmbInfectionClass.Enabled = true;
//						}
//						if(this.isNeedAdditoner(this.InfectCode))
//						{
//							//
//						}
//						//二级分类
//						this.cmbCaseClaseTwo.Enabled = this.hshNeedCaseTwo.Contains(this.InfectCode);
//					}
//					else
//					{
//						this.cmbInfectionClass.Enabled = true;
//					}
//				}
//				else
//				{
//					this.cmbInfectionClass.Enabled = true;
//				}

			}
			catch(Exception ex)
			{
				this.showMyMessageBox("添加患者基本信息失败"+ex.Message,"err");
			}
		}

		public void ShowInfo(FS.HISFC.Models.RADT.PatientInfo p)
		{
			this.txtContactPerson.Text=p.Kin.RelationLink;
			this.txtContactPersonAddr.Text=p.Kin.RelationAddress;
			this.txtContactPersonTel.Text=p.Kin.RelationPhone;
			this.txtRelationship.Text=p.Kin.Relation.Name;
			this.cmbNation.SelectedValue=p.Patient.Nationality.ID;
			this.cmbMarrige.SelectedValue=p.Patient.MaritalStatus.ID.ToString();
			this.cmbProfession.SelectedValue=p.Patient.Profession.ID;
			if (p.Patient.ID.ToString().IndexOf("ZY") >= 0)
			{
				cmbSource.Text = "住院";
			}

		}

		private bool showMyMessageBox(string message, string type)
		{
			System.Windows.Forms.DialogResult dr = new DialogResult();
			switch(type)
			{
				case "err":
					dr = MessageBox.Show(message,"错误>>",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
					break;
				case "info":
					dr = MessageBox.Show(message,type,System.Windows.Forms.MessageBoxButtons.YesNo,System.Windows.Forms.MessageBoxIcon.Information);
					break;
				default:
					dr = MessageBox.Show(message,type,System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Information);
					break;
			}
			if(dr == System.Windows.Forms.DialogResult.Yes)
			{
				return true;
			}
			return false;
		}
		

		/// <summary>
		/// 订正的时候要清除从原卡复制过来的部分信息
		/// </summary>
		/// <param name="report"></param>
		private void RenewInfo(ref FS.HISFC.DCP.Object.CommonReport report)
		{
			if(!this.IsRenew || report == null)
			{
				return;
			}
			System.DateTime dt = new DateTime(1,1,1);
			report.ModifyOper.ID = "";
			report.ModifyTime = dt;
			report.ApproveOper.ID = "";
			report.ApproveTime = dt;
		}
		/// <summary>
		/// 获取报告卡信息[含数据验证]
		/// </summary>
		/// <param name="report">传染病报卡</param>
		/// <param name="additionReport">传染病附卡</param>
		/// <param name="sexAdditionReport">性病附卡</param>
		/// <returns>-1 失败</returns>
		private int myGetReportData(ref FS.HISFC.DCP.Object.CommonReport report)
		{
			try
			{
				System.DateTime dt = new DateTime();
				dt = this.myReport.GetDateTimeFromSysDateTime();
				//修改的报告有虚拟编号、编号
				if(this.lbID.Text.Trim() != null && this.lbID.Text.Trim() != "")
				{
					FS.HISFC.DCP.Object.CommonReport oldreport = this.Printpanel.Tag as FS.HISFC.DCP.Object.CommonReport;
					report = oldreport;
				}

				#region 肿瘤报卡号1
//				if(this.txtCancer_No.Text.Trim() == null || this.txtCancer_No.Text.Trim() == "")
//				{
//					this.showMyMessageBox("请填写肿瘤报卡号","err");
//					this.txtCancer_No.Select();
//					this.txtCancer_No.Focus();
//					return -1;
//					
//				}
//				else
//				{
//					report.Cancer_No = this.txtCancer_No.Text.Trim();
//				}
				#endregion
				#region 2
				if (this.cmbSource.Text.Trim() == null || this.cmbSource.Text.Trim() == "")
				{
					this.showMyMessageBox("请填写患者来源","err");
					this.cmbSource.SelectAll();
					this.cmbSource.Focus();
					return -1;
				}
				#endregion
				#region 3
				if (this.lbPatientNo.Text.Trim() == string.Empty || this.lbPatientNo.Text.Trim() == "")
				{
					this.showMyMessageBox("请填写住院号或者门诊号按回车","err");
					return -1;
				}
				#endregion
				#region 4
				//姓名
				if(this.txtPatientName.Text.Trim() == null || this.txtPatientName.Text.Trim() == "")
				{
					//					if(this.txtPatientParents.Text.Trim() == null || this.txtPatientParents.Text.Trim() == "")
					//					{
					this.showMyMessageBox("请填写姓名","err");
					this.txtPatientName.Select();
					this.txtPatientName.Focus();
					return -1;
					//					}
				}
				else
				{
					report.Patient.Name = this.txtPatientName.Text.Trim();
				}
				//report.PatientParents = this.txtPatientParents.Text.Trim();//家长姓名
				report.Patient.IDCard = this.txtPatientID.Text.Trim();//身份证号
				//性别
				if(this.cbxMan.Checked || this.cbxWomen.Checked)
				{
					report.Patient.Sex.ID = this.cbxMan.Checked?"M":"F";
				}
				else
				{
					this.showMyMessageBox("请选择性别","err");
					this.txtAge.Select();
					this.txtAge.Focus();
					return -1;
				}

				//
				if (this.cmbNation.SelectedItem.ID != null)
				{
					string nation = this.cmbNation.SelectedItem.ID.ToString();
					if(nation == "####")
					{
						this.showMyMessageBox("请选择患者民族","err");
						this.cmbNation.Select();
						this.cmbNation.Focus();
						return -1;
					}
				}
				else
				{
					this.showMyMessageBox("患者民族为空","err");
					this.cmbNation.Select();
					this.cmbNation.Focus();
					return -1;
				}
				report.Patient.Birthday = new DateTime(this.dtBirthDay.Value.Year,this.dtBirthDay.Value.Month,
					this.dtBirthDay.Value.Day,0,0,0);//出生日期

				//年龄
				string agemessage = "\n提示：您可以选择出生日期，系统会自动计算年龄";
				if(this.txtAge.Text.Trim() == null || this.txtAge.Text.Trim() == "")
				{
					this.showMyMessageBox("请选择出生日期或填写年龄"+agemessage,"err");
					this.txtAge.Select();
					this.txtAge.Focus();
					return -1;
				}
				else
				{
					for(int i = 0; i < this.txtAge.Text.Trim().Length; i++)
					{
						if(!Char.IsDigit(this.txtAge.Text.Trim(), i))
						{
							this.showMyMessageBox("年龄应该为正整数"+agemessage,"err");
							this.txtAge.Select();
							this.txtAge.Focus();
							return -1;
						}
					}					
					report.Patient.Age = this.txtAge.Text.Trim();
				}

				//年龄单位
				report.AgeUnit = this.rdbYear.Checked?"岁":(this.rdbMonth.Checked?"月":"天");
				if(report.AgeUnit == "月" || report.AgeUnit == "天" 
					|| (report.AgeUnit == "岁" && FS.FrameWork.Function.NConvert.ToInt32(report.Patient.Age) <= 14))
				{
																																																				
				}
				int intage = FS.FrameWork.Function.NConvert.ToInt32(report.Patient.Age);
				if(this.rdbYear.Checked)
				{
					if(this.myReport.GetAge(report.Patient.Birthday) != report.Patient.Age+"年")
					{
						report.Patient.Birthday = new DateTime(dt.AddYears(-intage).Year,dt.AddYears(-intage).Month,dt.AddYears(-intage).Day,0,0,0);
					}
				}
				if(this.rdbDay.Checked)
				{
					if(intage == 31 && this.myReport.GetDateTimeFromSysDateTime().Day != 31)
					{
						intage = 100;
					}
					if(intage > 31)
					{
						this.showMyMessageBox("年龄天数大于一个月，请选择月份"+agemessage,"err");
						this.dtBirthDay.Select();
						this.dtBirthDay.Focus();
						return -1;
					}
				}
				if(this.rdbMonth.Checked)
				{
					if(intage > 12)
					{
						this.showMyMessageBox("年龄大于12个月，请填写周岁"+agemessage,"err");
						this.dtBirthDay.Select();
						this.dtBirthDay.Focus();
						return -1;
					}
					if(this.myReport.GetAge(report.Patient.Birthday) != report.Patient.Age+"月")
					{
						report.Patient.Birthday = new DateTime(dt.AddMonths(-intage).Year,dt.AddMonths(-intage).Month,dt.Day,0,0,0);
					}
				}				
				#endregion
				#region 5
				if (this.txtDistrict.Text.Trim() == null || this.txtDistrict.Text.Trim() == "")
				{
					this.showMyMessageBox("请填写籍贯","err");
					return -1;
				}
				if (this.cmbMarrige.Text.Trim() == string.Empty)
				{
					this.showMyMessageBox("请填写婚姻状况","err");
					return -1;
				}
				if (cmbMarrige.SelectedItem.ID != null)
				{
					string marrige = this.cmbMarrige.SelectedItem.ID.ToString();
					if(marrige == "####")
					{
						this.showMyMessageBox("请选择婚姻状况","err");
						this.cmbMarrige.Select();
						this.cmbMarrige.Focus();
						return -1;
					}
				}
				else
				{
					this.showMyMessageBox("婚姻状况为空","err");
					this.cmbMarrige.Select();
					this.cmbMarrige.Focus();
					return -1;
				}
				if (this.cmbProfession.Text.Trim() == string.Empty)
				{
					this.showMyMessageBox("请填写职业","err");
					return -1;
				}
				//职业代码
				if (this.cmbProfession.SelectedItem.ID != null)
				{
					string profession = this.cmbProfession.SelectedItem.ID.ToString();
					if(profession == "####")
					{
						this.showMyMessageBox("请选择患者职业","err");
						this.cmbProfession.Select();
						this.cmbProfession.Focus();
						return -1;
					}
					report.Patient.Profession.ID = profession;
				}
				else
				{
					this.showMyMessageBox("患者职业为空","err");
					this.cmbProfession.Select();
					this.cmbProfession.Focus();
					return -1;
				}
				if(this.txtTelephone.Text.Trim() == "" || this.txtTelephone.Text.Trim() == null)
				{
					this.showMyMessageBox("请填写联系电话","err");
					this.txtTelephone.Select();
					this.txtTelephone.Focus();
					return -1;
				}
				#endregion
				#region 6
				if (this.textBox3.Text.Trim() == string.Empty)
				{
					this.showMyMessageBox("请填写常用地址[省份]","err");
					return -1;
				}
				if (this.textBox1.Text.Trim() == string.Empty)
				{
					this.showMyMessageBox("请填写常用地址[市]","err");
					return -1;
				}
				if (this.textBox2.Text.Trim() == string.Empty)
				{
					this.showMyMessageBox("请填写常用地址[区]","err");
					return -1;
				}
				if (this.textBox5.Text.Trim() == string.Empty)
				{
					this.showMyMessageBox("请填写常用地址[街道(镇)]","err");
					return -1;
				}
				if (this.textBox4.Text.Trim() == string.Empty)
				{
					this.showMyMessageBox("请填写常用地址[门牌号]","err");
					return -1;
				}
				if (this.txtRegisterProvince.Text.Trim() == string.Empty)
				{
					this.showMyMessageBox("请填写户口地址[省份]","err");
					return -1;
				}
				if (this.txtRegisterCity.Text.Trim() == string.Empty)
				{
					this.showMyMessageBox("请填写户口地址[市]","err");
					return -1;
				}
				if (this.txtRegisterCounty.Text.Trim() == string.Empty)
				{
					this.showMyMessageBox("请填写户口地址[区]","err");
					return -1;
				}
				if (this.txtRegisterTown.Text.Trim() == string.Empty)
				{
					this.showMyMessageBox("请填写户口地址[街道(镇)]","err");
					return -1;
				}
				if (this.txtRegisterHouseNO.Text.Trim() == string.Empty)
				{
					this.showMyMessageBox("请填写户口地址[门牌号]","err");
					return -1;
				}
				#endregion
				#region 7
				if (txtOldDiagnoses.Text.Trim() == string.Empty)
				{
					this.showMyMessageBox("请填写临床医生诊断","err");
					return -1;
				}
				if (cmbInfectionClass.Text.Trim() == string.Empty)
				{
					this.showMyMessageBox("请填写疾病名称","err");
					return -1;
				}
				if (txtICD.Text.Trim() == string.Empty)
				{
					this.showMyMessageBox("请填写ICD编码","err");
					return -1;
				}
				#endregion
				#region 8
				if (rdbPathlogyY.Checked)
				{
					if (txtPathlogyNo.Text.Trim() == string.Empty)
					{
						this.showMyMessageBox("请填写病理号","err");
						return -1;
					}
					if (txtICDO.Text.Trim() == string.Empty)
					{
						this.showMyMessageBox("请填写ICD-O-3形态学编码","err");
						return -1;
					}
					if (txtWorkName.Text.Trim() == string.Empty)
					{
						this.showMyMessageBox("请填写病理诊断中文","err");
						return -1;
					}
					if (txtPathlogyType.Text.Trim() == string.Empty)
					{
						this.showMyMessageBox("请填写病理学分型","err");
						return -1;
					}
					if (txtPathlogyDegree.Text.Trim() == string.Empty)
					{
						this.showMyMessageBox("请填写分化程度","err");
						return -1;
					}
				}
				if (chbTreatment9.Checked)
				{
					if (txtTreatmentDemo.Text.Trim() == string.Empty)
					{
						this.showMyMessageBox("请填写治疗方式其他","err");
						return -1;
					}
				}
				if (chbDiagnoses19.Checked)
				{
					if (txtDiagnosesDemo.Text.Trim() == string.Empty)
					{
						this.showMyMessageBox("请填写诊断依据其他","err");
						return -1;
					}
				}
				if (rdbReback5.Checked)
				{
					if (txtRebackDemo.Text.Trim() == string.Empty)
					{
						this.showMyMessageBox("请填写转归其他","err");
						return -1;
					}
				}
				if (rdbPathlogyY.Checked == false)
				{
					if (chbDiagnoses14.Checked || chbDiagnoses15.Checked)
					{
						this.showMyMessageBox("诊断依据有病理但上边无病理学检查","err");
						return -1;
					}
				}
				
				if (!chbDiagnoses19.Checked && !chbDiagnoses18.Checked && !chbDiagnoses17.Checked &&
					!chbDiagnoses16.Checked && !chbDiagnoses8.Checked && !chbDiagnoses15.Checked &&
					!chbDiagnoses14.Checked && !chbDiagnoses13.Checked && !chbDiagnoses12.Checked &&
					!chbDiagnoses11.Checked && !chbDiagnoses10.Checked && !chbDiagnoses9.Checked &&
					!chbDiagnoses7.Checked && !chbDiagnoses6.Checked && !chbDiagnoses5.Checked &&
					!chbDiagnoses4.Checked && !chbDiagnoses3.Checked && !chbDiagnoses2.Checked && !chbDiagnoses1.Checked)
				{
					this.showMyMessageBox("请选择诊断依据","err");
					return -1;
				}

				if (rdbTreatY.Checked)
				{
					if (!chbTreatment1.Checked && !chbTreatment2.Checked && !chbTreatment3.Checked &&
						!chbTreatment4.Checked && !chbTreatment5.Checked && !chbTreatment6.Checked &&
						!chbTreatment7.Checked && !chbTreatment8.Checked && !chbTreatment9.Checked)
					{
						this.showMyMessageBox("请选择治疗方式","err");
						return -1;
					}
				}

				if (!rdbReback5.Checked && !rdbReback4.Checked && !rdbReback3.Checked &&
					!rdbReback2.Checked && !rdbReback1.Checked)
				{
					this.showMyMessageBox("请选择转归状态","err");
					return -1;
				}
				if (rdbReback4.Checked)
				{
					if (txtDeathResion.Text.Trim() == string.Empty)
					{
						this.showMyMessageBox("请填写死亡原因","err");
						return -1;
					}
				}
				#endregion
				//如果是新卡记录报卡人
				if(report.ID == null || report.ID == "" || this.IsRenew)
				{
					report.DoctorDept.ID = this.MyPriDept.ID;//报卡科室
					report.ReportDoctor.ID = this.PriOper.ID;//报卡医生
					report.ReportTime = dt;//报卡时间
					this.State = "0";
				}			
				//虚拟编号显示出来
				report.ExtendInfo1 = this.lbID.Text.Trim();//虚拟编号
				report.State = this.State;//状态
				if(this.State == "2")
				{
					//report.State = "0";
					report.State="5";  //不合格修改后重新保存  --和原来的新填区分
				}
				report.PatientType = this.Type;//类型
				report.Patient.ID = this.lbPatientNo.Text;//患者号
				report.Patient.PID.CardNO = this.lbPatientNo.Text;//患者卡号
				try
				{
					report.PatientDept.ID = this.lbPatientDept.Tag.ToString();//患者科室
				}
				catch
				{
					report.PatientDept.ID = this.MyPriDept.ID;
				}	

                 //现住地址
				report.Patient.AddressHome = this.txtSpecialAddress.Text.Trim();
					
				

				//联系电话
				report.Patient.PhoneHome = this.txtTelephone.Text.Trim();

				//工作单位
				string workName = "";
				workName = this.txtWorkName.Text.Trim();
//				if(workName == "" && this.hshStudent.Contains(report.Patient.Profession.ID))
//				{
//					this.showMyMessageBox("请在"+"\""+"工作单位栏"+"\""+"填写"+"\""+this.hshStudent[report.Patient.Profession.ID].ToString()+"\"","err");
//					this.txtWorkName.Select();
//					this.txtWorkName.Focus();
//					return -1;
//				}
				report.Patient.CompanyName = workName;				

				//患者科室
				//report.PatientDept.ID = this.PatientDept.ID;
				//传染病
				FS.FrameWork.Models.NeuObject disease = new FS.FrameWork.Models.NeuObject();
				if(this.myGetDisease(ref disease) == -1)
				{
					this.showMyMessageBox("请选择传染病","err");
					this.cmbInfectionClass.Select();
					this.cmbInfectionClass.Focus();
					return -1;
				}
				report.Disease = disease;
				//新生儿破伤风处理
				
				//肿瘤报卡标志 
                report.Cancer_Flag="1";

    			//备注
				report.Memo = this.rtxtMemo.Text.Trim();
//				if(this.hshNeedMemo.Contains(report.Disease.ID) && report.Memo == "")
//				{
//					this.showMyMessageBox("请在备注中填写疾病名称","err");
//					return -1;
//				}

				if(report.ID != null && report.ID != "")
				{
					report.ModifyOper.ID = this.PriOper.ID;
					report.ModifyTime = dt;//this.myReport.GetDateTimeFromSysDateTime();
				}
				//操作信息
				report.Oper.ID = this.PriOper.ID;
				report.OperDept.ID = this.MyPriDept.ID;
				report.OperTime = dt;//this.myReport.GetDateTimeFromSysDateTime();
				//诊断
				if(this.DiagNose != null && this.DiagNose != "")
				{
					report.ExtendInfo1 = report.Patient.ClinicDiagnose;
				}

				//this.RenewInfo(ref report);
			}
			catch(Exception ex)
			{
				this.showMyMessageBox("获取报告卡信息失败" + ex.Message, "err");
				return -1;
			}
			return 0;
		}
	
		/// <summary>
		/// 获取疾病信息
		/// </summary>
		/// <param name="disease"></param>
		private int myGetDisease(ref FS.FrameWork.Models.NeuObject disease)
		{
			if(this.cmbInfectionClass.SelectedValue.ToString() == "####")
			{
				return -1;
			}

			disease.ID = this.cmbInfectionClass.SelectedValue.ToString();
			disease.Name = (string)this.cmbInfectionClass.Text;
			return 0;
		}
	

		#region   肿瘤报卡实体信息保存
		/// <summary>
		/// 肿瘤报卡实体信息保存
		/// </summary>
		/// <returns></returns>
		public  FS.HISFC.DCP.Object.CancerAdd  SaveCancerAddData( string Report_No )
		{
			FS.HISFC.DCP.Object.CancerAdd obj = new FS.HISFC.DCP.Object.CancerAdd();
			
			obj.REPORT_NO=Report_No;
			obj.MEIDCAL_CARD=this.lbMadicalCardNo.Text;
			obj.NATION=this.cmbNation.SelectedItem.ID.ToString();
			obj.WORK_TYPE=this.txtWorkType.Text;
			obj.MARRIAGE=this.cmbMarrige.SelectedItem.ID.ToString() ;
			obj.DIAGNOSTIC_ICD=this.txtICD.Text ;
			obj.REGISTER_PROVINCE   =this.txtRegisterProvince.Text ;
			obj.REGISTER_CITY       =this.txtRegisterCity.Text;
			obj.REGISTER_DISTRICT    =this.txtRegisterCounty.Text ;
			obj.REGISTER_STREET       =this.txtRegisterTown.Text ;
			obj.REGISTER_HOUSENUMBER  =this.txtRegisterHouseNO.Text ;
			obj.REGISTER_POST         =this.txtPost.Text ;
			obj.WORK_PLACE            =this.txtWorkPlace.Text ;
			obj.CONTACT_PERSON        =this.txtContactPerson.Text ;
			obj.RELATIONSHIP          =this.txtRelationship.Text ;
			obj.CONTACT_PERSON_TEL    =this.txtContactPersonTel.Text ;
			obj.CONTACT_PERSON_ADDR   =this.txtContactPersonAddr.Text;
			obj.CLINICAL_T            =this.txtClinicalT.Text;
			obj.CLINICAL_N            =this.txtClinicalN.Text;
			obj.CLINICAL_M            =this.txtClinicalM.Text;

			if (this.rdbTNM1.Checked==true)
			{
				obj.TERM_TNM=this.rdbTNM1 .Text ;
			}
			else if (this.rdbTNM2.Checked==true)
			{
				obj.TERM_TNM=this.rdbTNM1 .Text ;
			}
			else if (this.rdbTNM3.Checked==true )
			{
				obj.TERM_TNM=this.rdbTNM3 .Text ;
			}
			else if (this.rdbTNM4.Checked==true)
			{  
				obj.TERM_TNM=this.rdbTNM4.Text ;
			}

			if (this.rdbPathlogyY.Checked==true )
			{
				obj.PATHOLOGY_CHECK =rdbPathlogyY.Text ;
			}
			else if (this.rdbPathlogyN.Checked==true)
			{ 
				obj.PATHOLOGY_CHECK =rdbPathlogyN.Text ;
			}
			obj.PATHOLOGY_NO          =this.txtPathlogyNo.Text ;	
			obj.PATHOLOGY_TYPE        =this.txtPathlogyType.Text;
			obj.PATHOLOGY_DEGREE      =this.txtPathlogyDegree.Text;
			obj.ICD_O                 =this.txtICDO.Text ;

			if (this.rdbTreatN.Checked==true)
			{
				obj.TREATMENT  =this.rdbTreatN.Text ;
			}
			else if (this.rdbTreatY.Checked==true)
			{
				obj.TREATMENT             =this.rdbTreatY.Text ;
			}
			obj.DEATH_REASON          =this.txtDeathResion.Text ;
			obj.OLD_DIAGNOSES         =this.txtOldDiagnoses.Text ;
			obj.OLD_DIAGNOSES_DATE = System.Convert.ToDateTime( this.dateTimePicker1.Value);
            obj.District=this.txtDistrict.Text;
			obj.HandPhone=this.txtHandPhone.Text;
			obj.ArrayListExt= this.cancerAddExtObject(Report_No);					
			return obj;
		}
		#endregion

		#region  肿瘤报卡扩展(多选部分内容)的信息保存到实体数组
		/// <summary>
		/// 肿瘤报卡扩展(多选部分内容)的信息获取到实体数组
		/// </summary>
		/// <returns></returns>
		public  System.Collections.ArrayList cancerAddExtObject(string Report_No)

		{   
			System.Collections.ArrayList al =new ArrayList();
			int index=0;
			foreach (Control ctl in this.panelPatientInfo.Controls)
			{
			   
				//诊断依据
				if (ctl is CheckBox && ctl.Name.IndexOf("chbDiagnoses")>=0)
				{    // x++;
					index=FS.FrameWork.Function.NConvert.ToInt32(ctl.Name.Substring(12,ctl.Name.Length-12));
					CheckBox cbx = (CheckBox)ctl;
					if(cbx.Checked)
					{
						FS.HISFC.DCP.Object.CancerAddExt cancerAddExt = new FS.HISFC.DCP.Object.CancerAddExt();
						cancerAddExt.Report_No=Report_No;
						cancerAddExt.Class_Code="DIAGNOSTIC";
						cancerAddExt.Class_Name="诊断依据";
						cancerAddExt.Item_Code=index.ToString();
						cancerAddExt.Item_Name=ctl.Text;
						if (index==19)  //其他按钮
						{  
							cancerAddExt.Item_Demo=cbx.Text;
						}
						al.Add(cancerAddExt);
						
					}
					
				}
                 
					//治疗方式
				else if(ctl is CheckBox && ctl.Name.IndexOf("chbTreatment")>=0)
			   
				{

					index=FS.FrameWork.Function.NConvert.ToInt32(ctl.Name.Substring(12,ctl.Name.Length-12));
					CheckBox cbx = (CheckBox)ctl;
					if(cbx.Checked)
					{
						FS.HISFC.DCP.Object.CancerAddExt cancerAddExt = new FS.HISFC.DCP.Object.CancerAddExt();
						cancerAddExt.Report_No=Report_No;
						cancerAddExt.Class_Code="TREATMENT";
						cancerAddExt.Class_Name="治疗方式";
						cancerAddExt.Item_Code=index.ToString();
						cancerAddExt.Item_Name=ctl.Text;
						if (index==9)  //其他按钮
						{  
							cancerAddExt.Item_Demo=this.txtTreatmentDemo.Text;
						}
						al.Add(cancerAddExt);
						
							 
					}
				}
				
					
					
			}
         
				
           
			//转归
			//cancerAddExt1.Report_No=Report_No;
			FS.HISFC.DCP.Object.CancerAddExt cancerAddExt1 = new FS.HISFC.DCP.Object.CancerAddExt();
			if (this.rdbReback1.Checked)
			{
				cancerAddExt1.Item_Code="1";
				cancerAddExt1.Item_Name=this.rdbReback1.Text;
			 
			}
			else if(this.rdbReback2.Checked)
			{
				cancerAddExt1.Item_Code="2";
				cancerAddExt1.Item_Name=this.rdbReback2.Text;
			}
			else if (this.rdbReback3.Checked)
			{
				cancerAddExt1.Item_Code="3";
				cancerAddExt1.Item_Name=this.rdbReback3.Text;
			}
			else if(this.rdbReback4.Checked)
			{
				cancerAddExt1.Item_Code="4";
				cancerAddExt1.Item_Name=this.rdbReback4.Text;
			}
			else if(this.rdbReback5.Checked)
			{
				cancerAddExt1.Item_Code="5";
				cancerAddExt1.Item_Name=this.rdbReback5.Text;
				cancerAddExt1.Item_Demo=this.txtRebackDemo.Text;
			}

			if (cancerAddExt1.Item_Code!=null && cancerAddExt1.Item_Code!="")
			{
				cancerAddExt1.Class_Code="REBACK";
				cancerAddExt1.Class_Name="转归";
				cancerAddExt1.Report_No=Report_No;
				al.Add(cancerAddExt1);
			}
         
			return al;
		}

		#endregion

		/// <summary>
		/// 根据报告卡显示其信息
		/// </summary>
		/// <param name="report"></param>
		public void ShowReportData(FS.HISFC.DCP.Object.CommonReport report)
		{
			try
			{
				this.SetEnable(false);
				this.IsRenew = false;
				//便于修改报告时取到固定值 如报告人等
				this.Printpanel.Tag = report;
				//肿瘤卡编号
				this.txtCancer_No.Text=report.Cancer_No;
				//报卡编号
				this.lbID.Text = report.ReportNO;
				//状态
				this.State = report.State;
				
				//订正卡的概念：疑似病例改为确诊病例时对原来的卡进行订正
				//用户自行判断是否订正，不update原卡，copy原卡后由用户修改后insert
				if(report.CorrectedReportNO != null && report.CorrectedReportNO != "" && report.ID != "")
				{
					this.lbState.Text = "订正报卡";
					try
					{
						this.lbState.Text += "(初报卡：" + report.CorrectedReportNO + ")";							
					}
					catch
					{}
				}
				else
				{
					this.lbState.Text = "初次报卡";
					if(report.CorrectReportNO != "" && report.CorrectReportNO != null)
					{
						try
						{
							this.lbState.Text += "(订正卡：" + report.CorrectReportNO + ")";							
						}
						catch
						{}
					}
				}
				this.diagNose = report.ExtendInfo1;
				this.Type = report.PatientType;
				//患者号
				this.lbPatientNo.Text = report.Patient.PID.CardNO;
				//患者姓名
				this.txtPatientName.Text = report.Patient.Name;
				//家长姓名
				//this.txtPatientParents.Text = report.PatientParents;
				//身份证号
				this.txtPatientID.Text  = report.Patient.IDCard;
				//性别
				if(report.Patient.Sex.ID.ToString() == "M")
				{
					this.cbxMan.Checked = true;
				}
				else
				{
					this.cbxWomen.Checked = true;
				}
				//出生日期
				this.dtBirthDay.Value = report.Patient.Birthday;
				//年龄
				this.txtAge.Text = report.Patient.Age;
				//年龄单位
				switch(report.AgeUnit)
				{
					case "岁":
						this.rdbYear.Checked = true;
						break;
					case "月":
						this.rdbMonth.Checked = true;
						break;
					default:
						this.rdbDay.Checked = true;
						break;
				}
				//患者职业
				//this.cmbProfession.Text = report.Patient.Profession.ID;
				this.cmbProfession.SelectedValue=report.Patient.Profession.ID;
				//患者工作单位
				this.txtWorkName.Text = report.Patient.CompanyName;
				//联系电话
				this.txtTelephone.Text = report.Patient.PhoneHome;
				//患者来源地
				//this.mySetHomeArea(FS.FrameWork.Function.NConvert.ToInt32(report.HomeArea)+1);
				//省市县镇
				//				this.cmbprovince.SelectedValue = report.HomeProvince.ID;
				//				this.cmbCity.SelectedValue = report.HomeCity.ID;
				//				this.cmbCouty.SelectedValue = report.HomeCouty.ID;
				//				this.cmbTown.SelectedValue = report.HomeTown.ID;
				//详细地址
				//				this.txtHomeAddress.Text = report.Patient.AddressHome;
				//this.txtSpecialAddress.Visible = true;
				this.txtSpecialAddress.Text = report.Patient.AddressHome;
				//患者科室 不显示
				this.lbPatientDept.Tag = report.PatientDept.ID;

				//疾病分类 不显示

				//疾病
				if(report.Disease.ID.Substring(0,1) == "A")
				{
//					this.rdbInfectionOtherClass.Checked = true;
//					this.cmbInfectionOtherClass.SelectedValue = report.Disease.ID;
				}
				else
				{
//					this.rdbInfectionClass.Checked = true;
					this.cmbInfectionClass.SelectedValue = report.Disease.ID;
				}				
				//发病日期
//				this.dtInfectionDate.Value = report.InfectDate;
//				this.txtInfectDate.Text = "1";
//				this.txtInfectDate.Visible = false;
				//诊断日期
//				this.dtDiaDate.Value = report.DiagnosisTime;
				//死亡日期
				try
				{
//					this.dtDeadDate.Value = report.DeadDate;
//					this.txtDeadDate.Text = "1";
//					this.txtDeadDate.Visible = false;
				}
				catch
				{
//					this.txtDeadDate.Visible = true;
//					this.cbxDeadDate.Checked = true;
//					this.txtDeadDate.Text = "";
				}
				//病例分类
				//this.cmbCaseClassOne.SelectedValue = report.CaseClass1.ID;
				//this.cmbCaseClaseTwo.SelectedValue = report.CaseClass2;
				//如果前一个报卡有疾病分类2，后一个没有，会受其影响，连续两次赋值可以解决
				//this.cmbCaseClaseTwo.SelectedValue = report.CaseClass2;

				//接触有无
				//this.rdbInfectOtherYes.Checked = FS.FrameWork.Function.NConvert.ToBoolean(report.InfectOtherFlag);	
				//状态已经赋值
				//附卡
				//if(report.AddtionFlag == "1")
				//{
					//this.panelAddtion.Visible = true;
					////肿瘤报卡显示
//					if(report.Cancer_Flag=="1")
//					{    
//						this.panelAdditionMain.Height= uc.Height;
//						this.panelAdditionMain.Controls.Add(this.uc);
     			this.LoadCancer(report.ReportNO);
//					}
//					else 
//					{
//						this.ShowAdditioner(report.ReportNO);
//
//						if(this.panelAdditionMain .Controls .Contains( uc))
//						{
//							this.panelAdditionMain.Controls.Remove(uc);
//						}
//					}
//				}
//				else
//				{
//					this.panelAddtion.Visible = false;
//
//				}
				//备注
				this.rtxtMemo.Text = report.Memo;

				//报告人 报告科室
				this.lbReportDoctor.Text = this.employHelper.GetName(report.ReportDoctor.ID);
				this.lbDoctorDept.Text = this.deptHelper.GetName(report.DoctorDept.ID);
				//报告时间
				this.lbReportTime.Text = report.ReportTime.ToString();

				//事由-退卡原因
				this.txtCase.Text = report.OperCase;
			}
			catch(Exception ex)
			{
				this.showMyMessageBox("数据转换失败,信息可能不完全"+ex.Message,"err");
			}
		}		


		#region   按钮治疗方式使能
		public  void  SetTreatmetEnable(bool status)
		{    
			this.chbTreatment1.Checked=false;
			this.chbTreatment2.Checked=false;
			this.chbTreatment3.Checked=false;
			this.chbTreatment4.Checked=false;
			this.chbTreatment5.Checked=false;
			this.chbTreatment6.Checked=false;
			this.chbTreatment7.Checked=false;
			this.chbTreatment8.Checked=false;
			this.chbTreatment9.Checked=false;

			this.chbTreatment1.Enabled=status;			
			this.chbTreatment2.Enabled=status;			
			this.chbTreatment3.Enabled=status;			
			this.chbTreatment4.Enabled=status;			
			this.chbTreatment5.Enabled=status;			
			this.chbTreatment6.Enabled=status;			
			this.chbTreatment7.Enabled=status;			
			this.chbTreatment8.Enabled=status;			
			this.chbTreatment9.Enabled=status;			
			if (!status  )
			{  
				this.txtTreatmentDemo.Clear();
				
			}
		  
		}

		#endregion
		#region 装载肿瘤卡报表到界面层
		
		public void LoadCancer( string Report_No)
		{
			this.cancerAdd=this.cancerAddMrg.GetCancerAddByNO(Report_No);
			if (this.cancerAdd != null )

			{               
				//this.txtMedical_Card.Text=this.cancerAdd.MEIDCAL_CARD ;  
//				    string sql ="SELECT name FROM COM_DICTIONARY c WHERE c.TYPE='NATION' AND code='{0}'";
//				sql =string.Format(sql,this.cancerAdd.NATION);
//				this.myReport.ExecSqlReturnOne(sql);
				
//				this.cmbNation.SelectedText = this.myReport.ExecSqlReturnOne(sql);
				//this.cmbNation.Text = "369";
				//this.panelPatientInfo.Enabled = true;
				//this.panelTitle.Enabled = true;
				this.cmbNation.SelectedValue=this.cancerAdd.NATION;
				this.txtWorkType.Text    =this.cancerAdd.WORK_TYPE   ;          ;
				this.cmbMarrige.SelectedValue    =this.cancerAdd.MARRIAGE ;
				this.txtICD.Text         =this.cancerAdd.DIAGNOSTIC_ICD       ;
				this.txtRegisterProvince.Text=this.cancerAdd.REGISTER_PROVINCE   ;
				this.txtRegisterCity.Text    =this.cancerAdd.REGISTER_CITY        ;
				this.txtRegisterCounty.Text  =this.cancerAdd.REGISTER_DISTRICT    ;
				this.txtRegisterTown.Text    =this.cancerAdd.REGISTER_STREET       ;
				this.txtRegisterHouseNO.Text =this.cancerAdd.REGISTER_HOUSENUMBER  ;
				this.lbMadicalCardNo.Text = this.cancerAdd.MEIDCAL_CARD;

				this.textBox3.Text=this.cancerAdd.REGISTER_PROVINCE   ;
				this.textBox1.Text    =this.cancerAdd.REGISTER_CITY        ;
				this.textBox2.Text  =this.cancerAdd.REGISTER_DISTRICT    ;
				this.textBox5.Text    =this.cancerAdd.REGISTER_STREET       ;
				this.textBox4.Text =this.cancerAdd.REGISTER_HOUSENUMBER  ;

				this.txtPost.Text            =this.cancerAdd.REGISTER_POST         ;
				this.txtWorkPlace.Text       =this.cancerAdd.WORK_PLACE           ;
				this.txtContactPerson.Text   =this.cancerAdd.CONTACT_PERSON       ;
				this.txtRelationship.Text    =this.cancerAdd.RELATIONSHIP          ;
				this.txtContactPersonTel.Text =this.cancerAdd.CONTACT_PERSON_TEL    ;
				this.txtContactPersonAddr.Text =this.cancerAdd.CONTACT_PERSON_ADDR   ;
				this.txtClinicalT.Text=this.cancerAdd.CLINICAL_T            ;
				this.txtClinicalN.Text=this.cancerAdd.CLINICAL_N            ;
				this.txtClinicalM.Text =this.cancerAdd.CLINICAL_M            ;
				           
				//cancerAdd.TERM_TNM
				switch(this.cancerAdd.TERM_TNM.ToString ())
				{
					case "0-I期":
						this.rdbTNM1.Checked=true;	
						break;
					case "II期":
						this.rdbTNM2.Checked=true;	 
						break;
					case "III期": 
						this.rdbTNM3.Checked=true;
						break;
					case "IV期":
						this.rdbTNM4.Checked=true;
						break;
					default:
						break;
				}
				 
				//cancerAdd.PATHOLOGY_CHECK       ;
				switch(this.cancerAdd.PATHOLOGY_CHECK.ToString ())
				{
					case "是":
						this.rdbPathlogyY.Checked=true;	
						break;
					case "否":
						this.rdbPathlogyN.Checked=true;	 
						break;
					default:
						break;
				}
							
				this.txtPathlogyNo.Text=this.cancerAdd.PATHOLOGY_NO          ;
				this.txtPathlogyType.Text=this.cancerAdd.PATHOLOGY_TYPE        ;
				this.txtPathlogyDegree.Text=this.cancerAdd.PATHOLOGY_DEGREE      ;
				this.txtICDO.Text=this.cancerAdd.ICD_O                 ;
				if (this.cancerAdd.PATHOLOGY_NO != string.Empty)
				{
					rdbPathlogyY.Checked = true;
				}
				//this.cancerAdd.TREATMENT             ;
				switch(this.cancerAdd.TREATMENT.ToString ())
				{
					case "是":
						this.rdbTreatY.Checked=true;	
						break;
					case "否":
						this.rdbTreatN.Checked=true;	 
						break;
					default:
						break;
				}
				this.txtDeathResion.Text=this.cancerAdd.DEATH_REASON          ;
				this.txtOldDiagnoses.Text=this.cancerAdd.OLD_DIAGNOSES         ;
				this.dtOldDiagnosesTime.Text=this.cancerAdd.OLD_DIAGNOSES_DATE.ToString();
				this.txtDistrict.Text=this.cancerAdd.District;
				this.txtHandPhone.Text=this.cancerAdd .HandPhone;
				//肿瘤报卡扩展部分赋值到界面层
				this.cancerAdd.ArrayListExt=this.cancerExtMrg.myGetCancerAddExtReport(Report_No);
                 
              
				foreach( FS.HISFC.DCP.Object.CancerAddExt ca in cancerAdd.ArrayListExt    )
				{  
					switch (ca.Class_Code)
					{
						case "DIAGNOSTIC" :
							#region 
							if(ca.Item_Code=="1")
							{
								chbDiagnoses1.Checked=true;
							}		
							else if(ca.Item_Code=="2")
							{ 
								chbDiagnoses2.Checked=true;
							}
							else if(ca.Item_Code== "3" )
							{
								chbDiagnoses3.Checked=true;
							}
							else if(ca.Item_Code== "4" )
							{
								chbDiagnoses4.Checked=true;
							}
							else if(ca.Item_Code== "5" )
							{
								chbDiagnoses5.Checked=true;
							}
							else if(ca.Item_Code== "6" )
							{
								chbDiagnoses6.Checked=true;
							}
							else if (ca.Item_Code=="7")
							{
								chbDiagnoses7.Checked=true;
							}
							else if (ca.Item_Code== "8" )
							{
								chbDiagnoses8.Checked=true;
							}
							else if (ca.Item_Code== "9")
							{
								chbDiagnoses9.Checked=true;
							}
								
							else if (ca.Item_Code=="10" )
							{
								chbDiagnoses10.Checked=true;
							}
							else if (ca.Item_Code== "11" )
							{
								chbDiagnoses11.Checked=true;
							}
							else if (ca.Item_Code=="12" )
							{
								chbDiagnoses12.Checked=true;
							}
							else if (ca.Item_Code== "13")
							{
								chbDiagnoses13.Checked=true;
							}
							else if (ca.Item_Code== "14")
							{
								chbDiagnoses14.Checked=true;
							}
							else if (ca.Item_Code== "15")
							{
								chbDiagnoses15.Checked=true;
							}
							else if (ca.Item_Code=="16" )
							{
								chbDiagnoses16.Checked=true;
							}
							else if (ca.Item_Code=="17")
							{
								chbDiagnoses17.Checked=true;
							}
							else if (ca.Item_Code== "18" )
							{
								chbDiagnoses18.Checked=true;
							}
							else if (ca.Item_Code== "19" )
							{
								this.chbDiagnoses19.Checked=true;
								this.txtDiagnosesDemo.Text=ca.Memo;
							}
								

							#endregion 
							break;
						case "TREATMENT"  :

							#region
							if (ca.Item_Code== "1" )
							{
								this.chbTreatment1.Checked=true;
							}
							else if (ca.Item_Code== "2" )
							{
								this.chbTreatment2.Checked=true;
							}
							if (ca.Item_Code== "3" )
							{
								this.chbTreatment3.Checked=true;
							}
							if (ca.Item_Code== "4" )
							{
								this.chbTreatment4.Checked=true;
							}
							if (ca.Item_Code== "5" )
							{
								this.chbTreatment5.Checked=true;
							}
							if (ca.Item_Code== "6" )
							{
								this.chbTreatment6.Checked=true;
							}
							if (ca.Item_Code== "7" )
							{
								this.chbTreatment7.Checked=true;
							}
							if (ca.Item_Code== "8" )
							{
								this.chbTreatment8.Checked=true;
							}
							if (ca.Item_Code== "9" )
							{
								this.chbTreatment9.Checked=true;
								this.txtTreatmentDemo.Text =ca.Item_Demo;
							}
						
						
							#endregion 
							break;
						case "REBACK"   :
							#region 
							if (ca.Item_Code==  "1")
							{
								this.rdbReback1.Checked=true;
							}
							if (ca.Item_Code==  "2")
							{
								this.rdbReback2.Checked=true;
							}
							if (ca.Item_Code==  "3")
							{
								this.rdbReback3.Checked=true;
							}
							if (ca.Item_Code==  "4")
							{
								this.rdbReback4.Checked=true;
							}
							if (ca.Item_Code==  "5")
							{
								this.rdbReback5.Checked=true;
								this.txtRebackDemo.Text=ca.Item_Demo;
							}
							break;
					
						
							#endregion 									
						
						default :
							break;
					}

				}
			
			}

		}

		#endregion

		#region 帮助
		/// <summary>
		/// 帮助
		/// </summary>
		public void Help()
		{
			try
			{
				System.Diagnostics.Process.Start(Application.StartupPath + @"\EpidemicHelp.CHM");   
			}
			catch(Exception ex)
			{
				this.showMyMessageBox("帮助无法使用>>\n" + Application.StartupPath + @"\EpidemicHelp.CHM\n" + ex.Message, "err");
			}
		}

		#endregion

		private void txtPatientName_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.cmbNation.Select();
				this.cmbNation.Focus();
			}
		}

		private void cmbNation_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtDistrict.Select();
				this.txtDistrict.Focus();
			}
		}

		private void txtDistrict_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtWorkType.Select();
				this.txtWorkType.Focus();
			}
		
		}

		private void txtWorkType_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.cmbProfession.Select();
				this.cmbProfession.Focus();
			}
		}

		private void cmbProfession_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtPatientID.Select();
				this.txtPatientID.Focus();
			}
		}

		private void txtPatientID_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.cmbMarrige.Select();
				this.cmbMarrige.Focus();
			}
		}

		private void cmbMarrige_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.dtBirthDay.Select();
				this.dtBirthDay.Focus();
			}
		}

		private void dtBirthDay_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtAge.Select();
				this.txtAge.Focus();
			}
		}

		private void txtAge_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtTelephone.Select();
				this.txtTelephone.Focus();
			}
		}

		private void txtTelephone_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtHandPhone.Select();
				this.txtHandPhone.Focus();
			}
		}

		private void txtHandPhone_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtRegisterProvince.Select();
				this.txtRegisterProvince.Focus();
			}
		}

		private void txtRegisterProvince_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtRegisterCity.Select();
				this.txtRegisterCity.Focus();
			}
		}

		private void txtRegisterCity_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtRegisterCounty.Select();
				this.txtRegisterCounty.Focus();
			}
		}

		private void txtRegisterCounty_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtRegisterTown.Select();
				this.txtRegisterTown.Focus();
			}
		}

		private void txtRegisterTown_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtRegisterHouseNO.Select();
				this.txtRegisterHouseNO.Focus();
			}
		}

		private void txtRegisterHouseNO_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				if (txtRegisterHouseNO.Text.Trim() == string.Empty)
				{
					txtRegisterHouseNO.Select();
					txtRegisterHouseNO.Focus();
				}
				else
				{
					this.txtPost.Select();
					this.txtPost.Focus();
				}
			}
		}

		private void txtPost_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtSpecialAddress.Select();
				this.txtSpecialAddress.Focus();
			}
		}

		private void txtSpecialAddress_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtWorkName.Select();
				this.txtWorkName.Focus();
			}
		}

		private void txtWorkName_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtWorkPlace.Select();
				this.txtWorkPlace.Focus();
			}
		}

		private void txtWorkPlace_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtContactPerson.Select();
				this.txtContactPerson.Focus();
			}
		}

		private void txtContactPerson_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtRelationship.Select();
				this.txtRelationship.Focus();
			}
		}

		private void txtRelationship_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtContactPersonTel.Select();
				this.txtContactPersonTel.Focus();
			}
		}

		private void txtContactPersonTel_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtContactPersonAddr.Select();
				this.txtContactPersonAddr.Focus();
			}
		}

		private void txtContactPersonAddr_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.cmbInfectionClass.Select();
				this.cmbInfectionClass.Focus();
			}
		}

		private void cmbInfectionClass_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtICD.Select();
				this.txtICD.Focus();
			}
		}

		private void txtICD_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.dateTimePicker1.Select();
				this.dateTimePicker1.Focus();
			}
		}

		private void dateTimePicker1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtClinicalT.Select();
				this.txtClinicalT.Focus();
			}
		}

		private void txtClinicalT_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtClinicalN.Select();
				this.txtClinicalN.Focus();
			}
		}

		private void txtClinicalN_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtClinicalM.Select();
				this.txtClinicalM.Focus();
			}
		}

		private void txtClinicalM_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtPathlogyNo.Select();
				this.txtPathlogyNo.Focus();
			}
		}

		private void txtPathlogyNo_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtPathlogyType.Select();
				this.txtPathlogyType.Focus();
			}
		}

		private void txtPathlogyType_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtPathlogyDegree.Select();
				this.txtPathlogyDegree.Focus();
			}
		}

		private void txtPathlogyDegree_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtICDO.Select();
				this.txtICDO.Focus();
			}
		}

		private void dtBirthDay_ValueChanged(object sender, System.EventArgs e)
		{
			string age = this.myReport.GetAge(this.dtBirthDay.Value,this.myReport.GetDateTimeFromSysDateTime());
			try
			{
				int length = age.Length;
				this.txtAge.Text = age.Substring(0,length-1);
				if(age.Substring(length-1) == "岁")
				{
					this.rdbYear.Checked = true;
				}
				else if(age.Substring(length-1) == "月")
				{
					this.rdbMonth.Checked = true;
				}
				else
				{
					this.rdbDay.Checked = true;
				}
			}
			catch
			{
			}
		}

		private void rdbTreatY_CheckedChanged(object sender, System.EventArgs e)
		{
			this.SetTreatmetEnable(this.rdbTreatY.Checked);
		}

		private void chbTreatment9_CheckedChanged(object sender, System.EventArgs e)
		{
			this.txtTreatmentDemo.Enabled=this.chbTreatment9.Checked;
		}

		private void rdbReback5_CheckedChanged(object sender, System.EventArgs e)
		{
			this.txtRebackDemo.Enabled=this.rdbReback5.Checked;
		}

		private void chbDiagnoses19_CheckedChanged(object sender, System.EventArgs e)
		{
			this.txtDiagnosesDemo.Enabled=this.chbDiagnoses19.Checked;
		}

		private void cmbInfectionClass_SelectedValueChanged(object sender, System.EventArgs e)
		{
			this.txtICD.Text=this.cmbInfectionClass.SelectedItem.ID;
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.cmbNation.Enabled = !this.cmbNation.Enabled;
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			this.cmbNation.Text = "其他";
		}

		private void label72_Click(object sender, System.EventArgs e)
		{
			if (this.textBox4.Text.Trim() == string.Empty)
			{
				this.textBox4.Text = txtRegisterHouseNO.Text;
			}
			if (this.textBox5.Text.Trim() == string.Empty)
			{
				this.textBox5.Text = txtRegisterTown.Text;
			}
			if (this.textBox2.Text.Trim() == string.Empty)
			{
				this.textBox2.Text = txtRegisterCounty.Text;
			}
			if (this.textBox1.Text.Trim() == string.Empty)
			{
				this.textBox1.Text = txtRegisterCity.Text;
			}
			if (this.textBox3.Text.Trim() == string.Empty)
			{
				this.textBox3.Text = txtRegisterProvince.Text;
			}
		}

		private void rdbReback4_CheckedChanged(object sender, System.EventArgs e)
		{
			if (rdbReback4.Checked)
			{
				this.dtOldDiagnosesTime.Visible = true;
				this.txtDeathResion.Enabled = true;
			}
			else
			{
				this.dtOldDiagnosesTime.Visible = false;
				this.txtDeathResion.Enabled = false;
			}
		}

		private void cmbSource_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (cmbSource.Text == "门诊")
			{
				this.label67.Text = "门诊卡号：";
			}
			else
			{
				this.label67.Text = "住 院 号：";
			}

		}

		private void lbPatientNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				if (this.lbPatientNo.Text.Trim() != string.Empty)
				{
					ArrayList pIn = this.patientMgr.GetPatientInfoByPatientNOAll(this.lbPatientNo.Text.Trim().PadLeft(10,'0'));
					FS.HISFC.Models.RADT.Patient ptInfo = pIn[0] as FS.HISFC.Models.RADT.Patient;
					ShowPatienInfo(ptInfo);
				}
			}
		}

		private void rdbPathlogyY_CheckedChanged(object sender, System.EventArgs e)
		{
			if (rdbPathlogyY.Checked)
			{
				txtPathlogyNo.Enabled = true;
				txtPathlogyType.Enabled = true;
				txtWorkType.Enabled = true;
				txtPathlogyDegree.Enabled = true;
				txtICDO.Enabled = true;
			}
			else
			{
				txtPathlogyNo.Enabled = false;
				txtPathlogyType.Enabled = false;
				txtWorkType.Enabled = false;
				txtPathlogyDegree.Enabled = false;
				txtICDO.Enabled = false;
			}
		}

		private void chbDiagnoses14_CheckedChanged(object sender, System.EventArgs e)
		{
			if (chbDiagnoses14.Checked && rdbPathlogyN.Checked && (string.Empty == txtPathlogyNo.Text))
			{
				this.showMyMessageBox("选择病理诊断依据必须填写上边病理相关信息！\n 否则无法保存","err");
			}
		}

		private void chbDiagnoses15_CheckedChanged(object sender, System.EventArgs e)
		{
			if (chbDiagnoses15.Checked && rdbPathlogyN.Checked && (string.Empty == txtPathlogyNo.Text))
			{
				this.showMyMessageBox("选择病理诊断依据必须填写上边病理相关信息！\n 否则无法保存","err");
			}
		}

		private void chbDiagnoses13_CheckedChanged(object sender, System.EventArgs e)
		{
			if (chbDiagnoses13.Checked && rdbPathlogyN.Checked && (string.Empty == txtPathlogyNo.Text))
			{
				this.showMyMessageBox("选择病理诊断依据必须填写上边病理相关信息！\n 否则无法保存","err");
			}
		}
	}
}
