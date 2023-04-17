using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.DCP.CancerReport
{
	/// <summary>
	/// ucCancerAddition 的摘要说明。
	/// </summary>
	public class ucCancerAddition : System.Windows.Forms.UserControl
	{
		public System.Windows.Forms.Label label5;
		public System.Windows.Forms.Label label8;
		public System.Windows.Forms.Panel panel1;
		public System.Windows.Forms.Label label9;
		public System.Windows.Forms.Label label4;
		public System.Windows.Forms.Label label13;
		public System.Windows.Forms.Label label11;
		public System.Windows.Forms.Label label91;
		public System.Windows.Forms.Label label50;
		public System.Windows.Forms.Label label51;
		public System.Windows.Forms.Label label52;
		public System.Windows.Forms.TextBox txtWorkPlace;
		public System.Windows.Forms.Label label58;
		public System.Windows.Forms.Label label23;
		public System.Windows.Forms.Label label22;
		public System.Windows.Forms.Label label21;
		public System.Windows.Forms.Label label20;
		public System.Windows.Forms.Label label25;
		public System.Windows.Forms.Label label45;
		public System.Windows.Forms.Label label43;
		public System.Windows.Forms.Label label42;
		public System.Windows.Forms.Label label40;
		public System.Windows.Forms.Label label38;
		public System.Windows.Forms.Label label36;
		public System.Windows.Forms.Label label35;
		public System.Windows.Forms.Label label34;
		public System.Windows.Forms.Label label33;
		public System.Windows.Forms.Label label31;
		public System.Windows.Forms.Label label32;
		public System.Windows.Forms.Label label30;
		public System.Windows.Forms.Label label29;
		public System.Windows.Forms.Label label28;
		public System.Windows.Forms.Label label27;
		public System.Windows.Forms.Label label26;
		public System.Windows.Forms.TextBox txtPatientType;
		public System.Windows.Forms.TextBox txtICD;
		public System.Windows.Forms.TextBox txtWorkType;
		public System.Windows.Forms.TextBox txtNation;
		public System.Windows.Forms.TextBox txtMedical_Card;
		public System.Windows.Forms.TextBox txtRegisterHouseNO;
		public System.Windows.Forms.TextBox txtRegisterTown;
		public System.Windows.Forms.TextBox txtRegisterCounty;
		public System.Windows.Forms.TextBox txtPost;
		public System.Windows.Forms.TextBox txtMarriage;
		public System.Windows.Forms.TextBox txtRegisterProvince;
		public System.Windows.Forms.TextBox txtRegisterCity;
		public System.Windows.Forms.Label label1;
		public System.Windows.Forms.Label label2;
		public System.Windows.Forms.TextBox txtRelationship;
		public System.Windows.Forms.TextBox txtContactPersonAddr;
		public System.Windows.Forms.TextBox txtContactPersonTel;
		public System.Windows.Forms.TextBox txtContactPerson;
		public System.Windows.Forms.TextBox txtClinicalM;
		public System.Windows.Forms.TextBox txtClinicalN;
		public System.Windows.Forms.TextBox txtClinicalT;
		public System.Windows.Forms.TextBox txtDiagnosesDemo;
		public System.Windows.Forms.CheckBox chbDiagnoses18;
		public System.Windows.Forms.CheckBox chbDiagnoses17;
		public System.Windows.Forms.CheckBox chbDiagnoses19;
		public System.Windows.Forms.CheckBox chbDiagnoses16;
		public System.Windows.Forms.CheckBox chbDiagnoses15;
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
		public System.Windows.Forms.TextBox txtICDO;
		public System.Windows.Forms.TextBox txtPathlogyDegree;
		public System.Windows.Forms.TextBox txtPathlogyType;
		public System.Windows.Forms.TextBox txtPathlogyNo;
		public System.Windows.Forms.CheckBox chbTreatment9;
		public System.Windows.Forms.CheckBox chbTreatment6;
		public System.Windows.Forms.CheckBox chbTreatment5;
		public System.Windows.Forms.CheckBox chbTreatment7;
		public System.Windows.Forms.CheckBox chbTreatment4;
		public System.Windows.Forms.CheckBox chbTreatment3;
		public System.Windows.Forms.CheckBox chbTreatment2;
		public System.Windows.Forms.CheckBox chbTreatment1;
		public System.Windows.Forms.CheckBox chbTreatment8;
		public System.Windows.Forms.TextBox txtDeathResion;
		public System.Windows.Forms.TextBox txtOldDiagnoses;
		public System.Windows.Forms.DateTimePicker dtOldDiagnosesTime;
		public  System.Windows.Forms.GroupBox groupBox1;
		public  System.Windows.Forms.RadioButton rdbTNM1;
		public  System.Windows.Forms.RadioButton rdbTNM2;
		public  System.Windows.Forms.RadioButton rdbTNM3;
		public  System.Windows.Forms.RadioButton rdbTNM4;
		public  System.Windows.Forms.GroupBox groupBox2;
		public  System.Windows.Forms.RadioButton rdbPathlogyY;
		public  System.Windows.Forms.RadioButton rdbPathlogyN;
		public  System.Windows.Forms.GroupBox groupBox3;
		public  System.Windows.Forms.RadioButton rdbTreatN;
		public  System.Windows.Forms.RadioButton rdbTreatY;
		public  System.Windows.Forms.GroupBox rdbReback;
		public  System.Windows.Forms.RadioButton rdbReback1;
		public  System.Windows.Forms.RadioButton rdbReback2;
		public  System.Windows.Forms.RadioButton rdbReback3;
		public  System.Windows.Forms.RadioButton rdbReback4;
		public  System.Windows.Forms.RadioButton rdbReback5;
		public System.Windows.Forms.TextBox txtTreatmentDemo;
		public System.Windows.Forms.TextBox txtRebackDemo;
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		public System.ComponentModel.Container components = null;

		public ucCancerAddition()
		{
			// 该调用是 Windows.Forms 窗体设计器所必需的。
			InitializeComponent();

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

		#region 组件设计器生成的代码
		/// <summary> 
		/// 设计器支持所需的方法 - 不要使用代码编辑器 
		/// 修改此方法的内容。
		/// </summary>
		public void InitializeComponent()
		{
			this.label5 = new System.Windows.Forms.Label();
			this.txtMedical_Card = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.txtICD = new System.Windows.Forms.TextBox();
			this.txtRegisterCounty = new System.Windows.Forms.TextBox();
			this.rdbReback = new System.Windows.Forms.GroupBox();
			this.rdbReback5 = new System.Windows.Forms.RadioButton();
			this.rdbReback4 = new System.Windows.Forms.RadioButton();
			this.rdbReback3 = new System.Windows.Forms.RadioButton();
			this.rdbReback2 = new System.Windows.Forms.RadioButton();
			this.rdbReback1 = new System.Windows.Forms.RadioButton();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.rdbPathlogyN = new System.Windows.Forms.RadioButton();
			this.rdbPathlogyY = new System.Windows.Forms.RadioButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.rdbTNM4 = new System.Windows.Forms.RadioButton();
			this.rdbTNM3 = new System.Windows.Forms.RadioButton();
			this.rdbTNM2 = new System.Windows.Forms.RadioButton();
			this.rdbTNM1 = new System.Windows.Forms.RadioButton();
			this.dtOldDiagnosesTime = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.txtRegisterCity = new System.Windows.Forms.TextBox();
			this.txtRegisterProvince = new System.Windows.Forms.TextBox();
			this.txtMarriage = new System.Windows.Forms.TextBox();
			this.txtNation = new System.Windows.Forms.TextBox();
			this.txtPatientType = new System.Windows.Forms.TextBox();
			this.txtDeathResion = new System.Windows.Forms.TextBox();
			this.label45 = new System.Windows.Forms.Label();
			this.label43 = new System.Windows.Forms.Label();
			this.txtOldDiagnoses = new System.Windows.Forms.TextBox();
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
			this.txtDiagnosesDemo = new System.Windows.Forms.TextBox();
			this.chbDiagnoses18 = new System.Windows.Forms.CheckBox();
			this.chbDiagnoses17 = new System.Windows.Forms.CheckBox();
			this.chbDiagnoses19 = new System.Windows.Forms.CheckBox();
			this.chbDiagnoses16 = new System.Windows.Forms.CheckBox();
			this.chbDiagnoses15 = new System.Windows.Forms.CheckBox();
			this.chbTreatment8 = new System.Windows.Forms.CheckBox();
			this.chbDiagnoses14 = new System.Windows.Forms.CheckBox();
			this.chbDiagnoses13 = new System.Windows.Forms.CheckBox();
			this.chbDiagnoses12 = new System.Windows.Forms.CheckBox();
			this.chbDiagnoses11 = new System.Windows.Forms.CheckBox();
			this.chbDiagnoses10 = new System.Windows.Forms.CheckBox();
			this.chbDiagnoses9 = new System.Windows.Forms.CheckBox();
			this.chbDiagnoses8 = new System.Windows.Forms.CheckBox();
			this.chbDiagnoses7 = new System.Windows.Forms.CheckBox();
			this.chbDiagnoses6 = new System.Windows.Forms.CheckBox();
			this.chbDiagnoses5 = new System.Windows.Forms.CheckBox();
			this.chbDiagnoses4 = new System.Windows.Forms.CheckBox();
			this.chbDiagnoses3 = new System.Windows.Forms.CheckBox();
			this.chbDiagnoses2 = new System.Windows.Forms.CheckBox();
			this.chbDiagnoses1 = new System.Windows.Forms.CheckBox();
			this.label42 = new System.Windows.Forms.Label();
			this.label38 = new System.Windows.Forms.Label();
			this.label36 = new System.Windows.Forms.Label();
			this.label35 = new System.Windows.Forms.Label();
			this.txtICDO = new System.Windows.Forms.TextBox();
			this.label34 = new System.Windows.Forms.Label();
			this.txtPathlogyDegree = new System.Windows.Forms.TextBox();
			this.label33 = new System.Windows.Forms.Label();
			this.txtPathlogyType = new System.Windows.Forms.TextBox();
			this.label31 = new System.Windows.Forms.Label();
			this.txtPathlogyNo = new System.Windows.Forms.TextBox();
			this.label32 = new System.Windows.Forms.Label();
			this.label30 = new System.Windows.Forms.Label();
			this.label29 = new System.Windows.Forms.Label();
			this.txtClinicalM = new System.Windows.Forms.TextBox();
			this.label28 = new System.Windows.Forms.Label();
			this.txtClinicalN = new System.Windows.Forms.TextBox();
			this.label27 = new System.Windows.Forms.Label();
			this.label26 = new System.Windows.Forms.Label();
			this.txtClinicalT = new System.Windows.Forms.TextBox();
			this.label25 = new System.Windows.Forms.Label();
			this.txtRelationship = new System.Windows.Forms.TextBox();
			this.label23 = new System.Windows.Forms.Label();
			this.txtContactPersonAddr = new System.Windows.Forms.TextBox();
			this.label22 = new System.Windows.Forms.Label();
			this.txtContactPersonTel = new System.Windows.Forms.TextBox();
			this.label21 = new System.Windows.Forms.Label();
			this.txtContactPerson = new System.Windows.Forms.TextBox();
			this.label20 = new System.Windows.Forms.Label();
			this.txtWorkPlace = new System.Windows.Forms.TextBox();
			this.label58 = new System.Windows.Forms.Label();
			this.label91 = new System.Windows.Forms.Label();
			this.txtRegisterHouseNO = new System.Windows.Forms.TextBox();
			this.txtRegisterTown = new System.Windows.Forms.TextBox();
			this.label50 = new System.Windows.Forms.Label();
			this.label51 = new System.Windows.Forms.Label();
			this.label52 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.txtPost = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.txtWorkType = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.rdbTreatN = new System.Windows.Forms.RadioButton();
			this.rdbTreatY = new System.Windows.Forms.RadioButton();
			this.label40 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.rdbReback.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label5.Location = new System.Drawing.Point(40, 8);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(80, 23);
			this.label5.TabIndex = 100059;
			this.label5.Text = "病人来源：";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtMedical_Card
			// 
			this.txtMedical_Card.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.txtMedical_Card.Location = new System.Drawing.Point(312, 8);
			this.txtMedical_Card.Name = "txtMedical_Card";
			this.txtMedical_Card.Size = new System.Drawing.Size(112, 23);
			this.txtMedical_Card.TabIndex = 100069;
			this.txtMedical_Card.Text = "";
			this.txtMedical_Card.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMedical_Card_KeyPress);
			// 
			// label8
			// 
			this.label8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label8.Location = new System.Drawing.Point(232, 8);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(72, 24);
			this.label8.TabIndex = 100070;
			this.label8.Text = "医保卡号:";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Window;
			this.panel1.Controls.Add(this.txtICD);
			this.panel1.Controls.Add(this.txtRegisterCounty);
			this.panel1.Controls.Add(this.txtMedical_Card);
			this.panel1.Controls.Add(this.rdbReback);
			this.panel1.Controls.Add(this.groupBox2);
			this.panel1.Controls.Add(this.groupBox1);
			this.panel1.Controls.Add(this.dtOldDiagnosesTime);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.txtRegisterCity);
			this.panel1.Controls.Add(this.txtRegisterProvince);
			this.panel1.Controls.Add(this.txtMarriage);
			this.panel1.Controls.Add(this.txtNation);
			this.panel1.Controls.Add(this.txtPatientType);
			this.panel1.Controls.Add(this.txtDeathResion);
			this.panel1.Controls.Add(this.label45);
			this.panel1.Controls.Add(this.label43);
			this.panel1.Controls.Add(this.txtOldDiagnoses);
			this.panel1.Controls.Add(this.txtRebackDemo);
			this.panel1.Controls.Add(this.txtTreatmentDemo);
			this.panel1.Controls.Add(this.chbTreatment9);
			this.panel1.Controls.Add(this.chbTreatment6);
			this.panel1.Controls.Add(this.chbTreatment5);
			this.panel1.Controls.Add(this.chbTreatment7);
			this.panel1.Controls.Add(this.chbTreatment4);
			this.panel1.Controls.Add(this.chbTreatment3);
			this.panel1.Controls.Add(this.chbTreatment2);
			this.panel1.Controls.Add(this.chbTreatment1);
			this.panel1.Controls.Add(this.txtDiagnosesDemo);
			this.panel1.Controls.Add(this.chbDiagnoses18);
			this.panel1.Controls.Add(this.chbDiagnoses17);
			this.panel1.Controls.Add(this.chbDiagnoses19);
			this.panel1.Controls.Add(this.chbDiagnoses16);
			this.panel1.Controls.Add(this.chbDiagnoses15);
			this.panel1.Controls.Add(this.chbTreatment8);
			this.panel1.Controls.Add(this.chbDiagnoses14);
			this.panel1.Controls.Add(this.chbDiagnoses13);
			this.panel1.Controls.Add(this.chbDiagnoses12);
			this.panel1.Controls.Add(this.chbDiagnoses11);
			this.panel1.Controls.Add(this.chbDiagnoses10);
			this.panel1.Controls.Add(this.chbDiagnoses9);
			this.panel1.Controls.Add(this.chbDiagnoses8);
			this.panel1.Controls.Add(this.chbDiagnoses7);
			this.panel1.Controls.Add(this.chbDiagnoses6);
			this.panel1.Controls.Add(this.chbDiagnoses5);
			this.panel1.Controls.Add(this.chbDiagnoses4);
			this.panel1.Controls.Add(this.chbDiagnoses3);
			this.panel1.Controls.Add(this.chbDiagnoses2);
			this.panel1.Controls.Add(this.chbDiagnoses1);
			this.panel1.Controls.Add(this.label42);
			this.panel1.Controls.Add(this.label38);
			this.panel1.Controls.Add(this.label36);
			this.panel1.Controls.Add(this.label35);
			this.panel1.Controls.Add(this.txtICDO);
			this.panel1.Controls.Add(this.label34);
			this.panel1.Controls.Add(this.txtPathlogyDegree);
			this.panel1.Controls.Add(this.label33);
			this.panel1.Controls.Add(this.txtPathlogyType);
			this.panel1.Controls.Add(this.label31);
			this.panel1.Controls.Add(this.txtPathlogyNo);
			this.panel1.Controls.Add(this.label32);
			this.panel1.Controls.Add(this.label30);
			this.panel1.Controls.Add(this.label29);
			this.panel1.Controls.Add(this.txtClinicalM);
			this.panel1.Controls.Add(this.label28);
			this.panel1.Controls.Add(this.txtClinicalN);
			this.panel1.Controls.Add(this.label27);
			this.panel1.Controls.Add(this.label26);
			this.panel1.Controls.Add(this.txtClinicalT);
			this.panel1.Controls.Add(this.label25);
			this.panel1.Controls.Add(this.txtRelationship);
			this.panel1.Controls.Add(this.label23);
			this.panel1.Controls.Add(this.txtContactPersonAddr);
			this.panel1.Controls.Add(this.label22);
			this.panel1.Controls.Add(this.txtContactPersonTel);
			this.panel1.Controls.Add(this.label21);
			this.panel1.Controls.Add(this.txtContactPerson);
			this.panel1.Controls.Add(this.label20);
			this.panel1.Controls.Add(this.txtWorkPlace);
			this.panel1.Controls.Add(this.label58);
			this.panel1.Controls.Add(this.label91);
			this.panel1.Controls.Add(this.txtRegisterHouseNO);
			this.panel1.Controls.Add(this.txtRegisterTown);
			this.panel1.Controls.Add(this.label50);
			this.panel1.Controls.Add(this.label51);
			this.panel1.Controls.Add(this.label52);
			this.panel1.Controls.Add(this.label11);
			this.panel1.Controls.Add(this.txtPost);
			this.panel1.Controls.Add(this.label13);
			this.panel1.Controls.Add(this.txtWorkType);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.label8);
			this.panel1.Controls.Add(this.label9);
			this.panel1.Controls.Add(this.groupBox3);
			this.panel1.Controls.Add(this.label40);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(768, 392);
			this.panel1.TabIndex = 100071;
			// 
			// txtICD
			// 
			this.txtICD.Location = new System.Drawing.Point(512, 8);
			this.txtICD.Name = "txtICD";
			this.txtICD.Size = new System.Drawing.Size(112, 21);
			this.txtICD.TabIndex = 100195;
			this.txtICD.Text = "";
			this.txtICD.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtICD_KeyPress);
			// 
			// txtRegisterCounty
			// 
			this.txtRegisterCounty.Location = new System.Drawing.Point(312, 56);
			this.txtRegisterCounty.Name = "txtRegisterCounty";
			this.txtRegisterCounty.Size = new System.Drawing.Size(104, 21);
			this.txtRegisterCounty.TabIndex = 100176;
			this.txtRegisterCounty.Text = "";
			this.txtRegisterCounty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRegisterCounty_KeyPress);
			// 
			// rdbReback
			// 
			this.rdbReback.Controls.Add(this.rdbReback5);
			this.rdbReback.Controls.Add(this.rdbReback4);
			this.rdbReback.Controls.Add(this.rdbReback3);
			this.rdbReback.Controls.Add(this.rdbReback2);
			this.rdbReback.Controls.Add(this.rdbReback1);
			this.rdbReback.Location = new System.Drawing.Point(80, 328);
			this.rdbReback.Name = "rdbReback";
			this.rdbReback.Size = new System.Drawing.Size(344, 32);
			this.rdbReback.TabIndex = 100280;
			this.rdbReback.TabStop = false;
			// 
			// rdbReback5
			// 
			this.rdbReback5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.rdbReback5.Location = new System.Drawing.Point(232, 8);
			this.rdbReback5.Name = "rdbReback5";
			this.rdbReback5.Size = new System.Drawing.Size(112, 24);
			this.rdbReback5.TabIndex = 4;
			this.rdbReback5.Text = "其他(请注明)";
			this.rdbReback5.CheckedChanged += new System.EventHandler(this.rdbReback5_CheckedChanged);
			// 
			// rdbReback4
			// 
			this.rdbReback4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.rdbReback4.Location = new System.Drawing.Point(168, 8);
			this.rdbReback4.Name = "rdbReback4";
			this.rdbReback4.Size = new System.Drawing.Size(56, 24);
			this.rdbReback4.TabIndex = 3;
			this.rdbReback4.Text = "死亡";
			// 
			// rdbReback3
			// 
			this.rdbReback3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.rdbReback3.Location = new System.Drawing.Point(112, 8);
			this.rdbReback3.Name = "rdbReback3";
			this.rdbReback3.Size = new System.Drawing.Size(56, 24);
			this.rdbReback3.TabIndex = 2;
			this.rdbReback3.Text = "未愈";
			// 
			// rdbReback2
			// 
			this.rdbReback2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.rdbReback2.Location = new System.Drawing.Point(56, 8);
			this.rdbReback2.Name = "rdbReback2";
			this.rdbReback2.Size = new System.Drawing.Size(56, 24);
			this.rdbReback2.TabIndex = 1;
			this.rdbReback2.Text = "好转";
			// 
			// rdbReback1
			// 
			this.rdbReback1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.rdbReback1.Location = new System.Drawing.Point(8, 8);
			this.rdbReback1.Name = "rdbReback1";
			this.rdbReback1.Size = new System.Drawing.Size(56, 24);
			this.rdbReback1.TabIndex = 0;
			this.rdbReback1.Text = "治愈";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.rdbPathlogyN);
			this.groupBox2.Controls.Add(this.rdbPathlogyY);
			this.groupBox2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.groupBox2.Location = new System.Drawing.Point(664, 144);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(88, 32);
			this.groupBox2.TabIndex = 100278;
			this.groupBox2.TabStop = false;
			// 
			// rdbPathlogyN
			// 
			this.rdbPathlogyN.Location = new System.Drawing.Point(48, 8);
			this.rdbPathlogyN.Name = "rdbPathlogyN";
			this.rdbPathlogyN.Size = new System.Drawing.Size(32, 24);
			this.rdbPathlogyN.TabIndex = 1;
			this.rdbPathlogyN.Text = "否";
			// 
			// rdbPathlogyY
			// 
			this.rdbPathlogyY.Location = new System.Drawing.Point(8, 8);
			this.rdbPathlogyY.Name = "rdbPathlogyY";
			this.rdbPathlogyY.Size = new System.Drawing.Size(32, 24);
			this.rdbPathlogyY.TabIndex = 0;
			this.rdbPathlogyY.Text = "是";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.rdbTNM4);
			this.groupBox1.Controls.Add(this.rdbTNM3);
			this.groupBox1.Controls.Add(this.rdbTNM2);
			this.groupBox1.Controls.Add(this.rdbTNM1);
			this.groupBox1.Location = new System.Drawing.Point(312, 144);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(216, 32);
			this.groupBox1.TabIndex = 100277;
			this.groupBox1.TabStop = false;
			// 
			// rdbTNM4
			// 
			this.rdbTNM4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.rdbTNM4.Location = new System.Drawing.Point(168, 8);
			this.rdbTNM4.Name = "rdbTNM4";
			this.rdbTNM4.Size = new System.Drawing.Size(56, 24);
			this.rdbTNM4.TabIndex = 3;
			this.rdbTNM4.Text = "IV期";
			// 
			// rdbTNM3
			// 
			this.rdbTNM3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.rdbTNM3.Location = new System.Drawing.Point(112, 8);
			this.rdbTNM3.Name = "rdbTNM3";
			this.rdbTNM3.Size = new System.Drawing.Size(64, 24);
			this.rdbTNM3.TabIndex = 2;
			this.rdbTNM3.Text = "III期";
			// 
			// rdbTNM2
			// 
			this.rdbTNM2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.rdbTNM2.Location = new System.Drawing.Point(64, 8);
			this.rdbTNM2.Name = "rdbTNM2";
			this.rdbTNM2.Size = new System.Drawing.Size(56, 24);
			this.rdbTNM2.TabIndex = 1;
			this.rdbTNM2.Text = "II期";
			// 
			// rdbTNM1
			// 
			this.rdbTNM1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.rdbTNM1.Location = new System.Drawing.Point(8, 8);
			this.rdbTNM1.Name = "rdbTNM1";
			this.rdbTNM1.Size = new System.Drawing.Size(64, 24);
			this.rdbTNM1.TabIndex = 0;
			this.rdbTNM1.Text = "0-I期";
			// 
			// dtOldDiagnosesTime
			// 
			this.dtOldDiagnosesTime.Location = new System.Drawing.Point(616, 368);
			this.dtOldDiagnosesTime.Name = "dtOldDiagnosesTime";
			this.dtOldDiagnosesTime.Size = new System.Drawing.Size(136, 21);
			this.dtOldDiagnosesTime.TabIndex = 100276;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label2.Location = new System.Drawing.Point(272, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(24, 23);
			this.label2.TabIndex = 100275;
			this.label2.Text = "市 ";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label1.Location = new System.Drawing.Point(184, 56);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(24, 23);
			this.label1.TabIndex = 100274;
			this.label1.Text = "省 ";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtRegisterCity
			// 
			this.txtRegisterCity.Location = new System.Drawing.Point(208, 56);
			this.txtRegisterCity.Name = "txtRegisterCity";
			this.txtRegisterCity.Size = new System.Drawing.Size(64, 21);
			this.txtRegisterCity.TabIndex = 100273;
			this.txtRegisterCity.Text = "";
			this.txtRegisterCity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRegisterCity_KeyPress);
			// 
			// txtRegisterProvince
			// 
			this.txtRegisterProvince.Location = new System.Drawing.Point(120, 56);
			this.txtRegisterProvince.Name = "txtRegisterProvince";
			this.txtRegisterProvince.Size = new System.Drawing.Size(64, 21);
			this.txtRegisterProvince.TabIndex = 100272;
			this.txtRegisterProvince.Text = "";
			this.txtRegisterProvince.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRegisterProvince_KeyPress);
			// 
			// txtMarriage
			// 
			this.txtMarriage.Location = new System.Drawing.Point(512, 32);
			this.txtMarriage.Name = "txtMarriage";
			this.txtMarriage.Size = new System.Drawing.Size(112, 21);
			this.txtMarriage.TabIndex = 100271;
			this.txtMarriage.Text = "";
			this.txtMarriage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMarriage_KeyPress);
			// 
			// txtNation
			// 
			this.txtNation.Location = new System.Drawing.Point(312, 32);
			this.txtNation.Name = "txtNation";
			this.txtNation.Size = new System.Drawing.Size(112, 21);
			this.txtNation.TabIndex = 100270;
			this.txtNation.Text = "";
			this.txtNation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNation_KeyPress);
			// 
			// txtPatientType
			// 
			this.txtPatientType.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.txtPatientType.Location = new System.Drawing.Point(120, 8);
			this.txtPatientType.MaxLength = 19;
			this.txtPatientType.Name = "txtPatientType";
			this.txtPatientType.Size = new System.Drawing.Size(87, 23);
			this.txtPatientType.TabIndex = 100269;
			this.txtPatientType.Text = "";
			// 
			// txtDeathResion
			// 
			this.txtDeathResion.Location = new System.Drawing.Point(632, 336);
			this.txtDeathResion.Name = "txtDeathResion";
			this.txtDeathResion.Size = new System.Drawing.Size(120, 21);
			this.txtDeathResion.TabIndex = 100267;
			this.txtDeathResion.TabStop = false;
			this.txtDeathResion.Text = "";
			// 
			// label45
			// 
			this.label45.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label45.Location = new System.Drawing.Point(568, 336);
			this.label45.Name = "label45";
			this.label45.Size = new System.Drawing.Size(80, 23);
			this.label45.TabIndex = 100266;
			this.label45.Text = "死亡原因：";
			this.label45.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label43
			// 
			this.label43.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label43.Location = new System.Drawing.Point(528, 368);
			this.label43.Name = "label43";
			this.label43.Size = new System.Drawing.Size(96, 23);
			this.label43.TabIndex = 100264;
			this.label43.Text = "原诊断日期：";
			this.label43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtOldDiagnoses
			// 
			this.txtOldDiagnoses.Location = new System.Drawing.Point(240, 368);
			this.txtOldDiagnoses.Name = "txtOldDiagnoses";
			this.txtOldDiagnoses.Size = new System.Drawing.Size(280, 21);
			this.txtOldDiagnoses.TabIndex = 100263;
			this.txtOldDiagnoses.Text = "";
			// 
			// txtRebackDemo
			// 
			this.txtRebackDemo.Enabled = false;
			this.txtRebackDemo.Location = new System.Drawing.Point(432, 336);
			this.txtRebackDemo.Name = "txtRebackDemo";
			this.txtRebackDemo.Size = new System.Drawing.Size(104, 21);
			this.txtRebackDemo.TabIndex = 100260;
			this.txtRebackDemo.Text = "";
			// 
			// txtTreatmentDemo
			// 
			this.txtTreatmentDemo.Enabled = false;
			this.txtTreatmentDemo.Location = new System.Drawing.Point(320, 312);
			this.txtTreatmentDemo.Name = "txtTreatmentDemo";
			this.txtTreatmentDemo.Size = new System.Drawing.Size(432, 21);
			this.txtTreatmentDemo.TabIndex = 100254;
			this.txtTreatmentDemo.Text = "";
			// 
			// chbTreatment9
			// 
			this.chbTreatment9.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbTreatment9.Location = new System.Drawing.Point(184, 312);
			this.chbTreatment9.Name = "chbTreatment9";
			this.chbTreatment9.Size = new System.Drawing.Size(128, 24);
			this.chbTreatment9.TabIndex = 100253;
			this.chbTreatment9.Text = "其他（请注明）";
			this.chbTreatment9.CheckedChanged += new System.EventHandler(this.chbTreatment9_CheckedChanged);
			// 
			// chbTreatment6
			// 
			this.chbTreatment6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbTreatment6.Location = new System.Drawing.Point(576, 288);
			this.chbTreatment6.Name = "chbTreatment6";
			this.chbTreatment6.Size = new System.Drawing.Size(56, 24);
			this.chbTreatment6.TabIndex = 100252;
			this.chbTreatment6.TabStop = false;
			this.chbTreatment6.Text = "介入";
			// 
			// chbTreatment5
			// 
			this.chbTreatment5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbTreatment5.Location = new System.Drawing.Point(512, 288);
			this.chbTreatment5.Name = "chbTreatment5";
			this.chbTreatment5.Size = new System.Drawing.Size(56, 24);
			this.chbTreatment5.TabIndex = 100251;
			this.chbTreatment5.TabStop = false;
			this.chbTreatment5.Text = "免疫";
			// 
			// chbTreatment7
			// 
			this.chbTreatment7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbTreatment7.Location = new System.Drawing.Point(632, 288);
			this.chbTreatment7.Name = "chbTreatment7";
			this.chbTreatment7.Size = new System.Drawing.Size(88, 24);
			this.chbTreatment7.TabIndex = 100250;
			this.chbTreatment7.TabStop = false;
			this.chbTreatment7.Text = "对症治疗";
			// 
			// chbTreatment4
			// 
			this.chbTreatment4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbTreatment4.Location = new System.Drawing.Point(464, 288);
			this.chbTreatment4.Name = "chbTreatment4";
			this.chbTreatment4.Size = new System.Drawing.Size(56, 24);
			this.chbTreatment4.TabIndex = 100249;
			this.chbTreatment4.TabStop = false;
			this.chbTreatment4.Text = "中药";
			// 
			// chbTreatment3
			// 
			this.chbTreatment3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbTreatment3.Location = new System.Drawing.Point(416, 288);
			this.chbTreatment3.Name = "chbTreatment3";
			this.chbTreatment3.Size = new System.Drawing.Size(56, 24);
			this.chbTreatment3.TabIndex = 100248;
			this.chbTreatment3.TabStop = false;
			this.chbTreatment3.Text = "放射";
			// 
			// chbTreatment2
			// 
			this.chbTreatment2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbTreatment2.Location = new System.Drawing.Point(352, 288);
			this.chbTreatment2.Name = "chbTreatment2";
			this.chbTreatment2.Size = new System.Drawing.Size(56, 24);
			this.chbTreatment2.TabIndex = 100247;
			this.chbTreatment2.TabStop = false;
			this.chbTreatment2.Text = "化疗";
			// 
			// chbTreatment1
			// 
			this.chbTreatment1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbTreatment1.Location = new System.Drawing.Point(296, 288);
			this.chbTreatment1.Name = "chbTreatment1";
			this.chbTreatment1.Size = new System.Drawing.Size(56, 24);
			this.chbTreatment1.TabIndex = 100246;
			this.chbTreatment1.Text = "手术";
			// 
			// txtDiagnosesDemo
			// 
			this.txtDiagnosesDemo.Enabled = false;
			this.txtDiagnosesDemo.Location = new System.Drawing.Point(224, 264);
			this.txtDiagnosesDemo.Name = "txtDiagnosesDemo";
			this.txtDiagnosesDemo.Size = new System.Drawing.Size(528, 21);
			this.txtDiagnosesDemo.TabIndex = 100244;
			this.txtDiagnosesDemo.Text = "";
			// 
			// chbDiagnoses18
			// 
			this.chbDiagnoses18.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbDiagnoses18.Location = new System.Drawing.Point(648, 240);
			this.chbDiagnoses18.Name = "chbDiagnoses18";
			this.chbDiagnoses18.Size = new System.Drawing.Size(56, 24);
			this.chbDiagnoses18.TabIndex = 100243;
			this.chbDiagnoses18.Text = "不详";
			// 
			// chbDiagnoses17
			// 
			this.chbDiagnoses17.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbDiagnoses17.Location = new System.Drawing.Point(544, 240);
			this.chbDiagnoses17.Name = "chbDiagnoses17";
			this.chbDiagnoses17.Size = new System.Drawing.Size(88, 24);
			this.chbDiagnoses17.TabIndex = 100242;
			this.chbDiagnoses17.TabStop = false;
			this.chbDiagnoses17.Text = "核磁共振";
			// 
			// chbDiagnoses19
			// 
			this.chbDiagnoses19.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbDiagnoses19.Location = new System.Drawing.Point(104, 264);
			this.chbDiagnoses19.Name = "chbDiagnoses19";
			this.chbDiagnoses19.Size = new System.Drawing.Size(128, 24);
			this.chbDiagnoses19.TabIndex = 100241;
			this.chbDiagnoses19.Text = "其他（请注明）";
			this.chbDiagnoses19.CheckStateChanged += new System.EventHandler(this.chbDiagnoses19_CheckStateChanged);
			// 
			// chbDiagnoses16
			// 
			this.chbDiagnoses16.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbDiagnoses16.Location = new System.Drawing.Point(448, 240);
			this.chbDiagnoses16.Name = "chbDiagnoses16";
			this.chbDiagnoses16.Size = new System.Drawing.Size(96, 24);
			this.chbDiagnoses16.TabIndex = 100240;
			this.chbDiagnoses16.TabStop = false;
			this.chbDiagnoses16.Text = "死亡补发病";
			// 
			// chbDiagnoses15
			// 
			this.chbDiagnoses15.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbDiagnoses15.Location = new System.Drawing.Point(336, 240);
			this.chbDiagnoses15.Name = "chbDiagnoses15";
			this.chbDiagnoses15.Size = new System.Drawing.Size(112, 24);
			this.chbDiagnoses15.TabIndex = 100238;
			this.chbDiagnoses15.TabStop = false;
			this.chbDiagnoses15.Text = "尸检(有病理)";
			// 
			// chbTreatment8
			// 
			this.chbTreatment8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbTreatment8.Location = new System.Drawing.Point(104, 312);
			this.chbTreatment8.Name = "chbTreatment8";
			this.chbTreatment8.Size = new System.Drawing.Size(88, 24);
			this.chbTreatment8.TabIndex = 100237;
			this.chbTreatment8.Text = "止痛治疗";
			// 
			// chbDiagnoses14
			// 
			this.chbDiagnoses14.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbDiagnoses14.Location = new System.Drawing.Point(248, 240);
			this.chbDiagnoses14.Name = "chbDiagnoses14";
			this.chbDiagnoses14.Size = new System.Drawing.Size(96, 24);
			this.chbDiagnoses14.TabIndex = 100236;
			this.chbDiagnoses14.TabStop = false;
			this.chbDiagnoses14.Text = "病理(原发)";
			// 
			// chbDiagnoses13
			// 
			this.chbDiagnoses13.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbDiagnoses13.Location = new System.Drawing.Point(160, 240);
			this.chbDiagnoses13.Name = "chbDiagnoses13";
			this.chbDiagnoses13.Size = new System.Drawing.Size(96, 24);
			this.chbDiagnoses13.TabIndex = 100235;
			this.chbDiagnoses13.Text = "病理(继发)";
			// 
			// chbDiagnoses12
			// 
			this.chbDiagnoses12.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbDiagnoses12.Location = new System.Drawing.Point(104, 240);
			this.chbDiagnoses12.Name = "chbDiagnoses12";
			this.chbDiagnoses12.Size = new System.Drawing.Size(56, 24);
			this.chbDiagnoses12.TabIndex = 100234;
			this.chbDiagnoses12.TabStop = false;
			this.chbDiagnoses12.Text = "血片";
			// 
			// chbDiagnoses11
			// 
			this.chbDiagnoses11.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbDiagnoses11.Location = new System.Drawing.Point(680, 216);
			this.chbDiagnoses11.Name = "chbDiagnoses11";
			this.chbDiagnoses11.Size = new System.Drawing.Size(72, 24);
			this.chbDiagnoses11.TabIndex = 100233;
			this.chbDiagnoses11.Text = "细胞学";
			// 
			// chbDiagnoses10
			// 
			this.chbDiagnoses10.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbDiagnoses10.Location = new System.Drawing.Point(632, 216);
			this.chbDiagnoses10.Name = "chbDiagnoses10";
			this.chbDiagnoses10.Size = new System.Drawing.Size(56, 24);
			this.chbDiagnoses10.TabIndex = 100232;
			this.chbDiagnoses10.TabStop = false;
			this.chbDiagnoses10.Text = "免疫";
			// 
			// chbDiagnoses9
			// 
			this.chbDiagnoses9.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbDiagnoses9.Location = new System.Drawing.Point(584, 216);
			this.chbDiagnoses9.Name = "chbDiagnoses9";
			this.chbDiagnoses9.Size = new System.Drawing.Size(56, 24);
			this.chbDiagnoses9.TabIndex = 100231;
			this.chbDiagnoses9.Text = "生化";
			// 
			// chbDiagnoses8
			// 
			this.chbDiagnoses8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbDiagnoses8.Location = new System.Drawing.Point(480, 216);
			this.chbDiagnoses8.Name = "chbDiagnoses8";
			this.chbDiagnoses8.Size = new System.Drawing.Size(112, 24);
			this.chbDiagnoses8.TabIndex = 100230;
			this.chbDiagnoses8.TabStop = false;
			this.chbDiagnoses8.Text = "尸检(无病理)";
			// 
			// chbDiagnoses7
			// 
			this.chbDiagnoses7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbDiagnoses7.Location = new System.Drawing.Point(424, 216);
			this.chbDiagnoses7.Name = "chbDiagnoses7";
			this.chbDiagnoses7.Size = new System.Drawing.Size(56, 24);
			this.chbDiagnoses7.TabIndex = 100229;
			this.chbDiagnoses7.Text = "手术";
			// 
			// chbDiagnoses6
			// 
			this.chbDiagnoses6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbDiagnoses6.Location = new System.Drawing.Point(376, 216);
			this.chbDiagnoses6.Name = "chbDiagnoses6";
			this.chbDiagnoses6.Size = new System.Drawing.Size(56, 24);
			this.chbDiagnoses6.TabIndex = 100228;
			this.chbDiagnoses6.TabStop = false;
			this.chbDiagnoses6.Text = "PET";
			// 
			// chbDiagnoses5
			// 
			this.chbDiagnoses5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbDiagnoses5.Location = new System.Drawing.Point(336, 216);
			this.chbDiagnoses5.Name = "chbDiagnoses5";
			this.chbDiagnoses5.Size = new System.Drawing.Size(48, 24);
			this.chbDiagnoses5.TabIndex = 100227;
			this.chbDiagnoses5.Text = "CT";
			// 
			// chbDiagnoses4
			// 
			this.chbDiagnoses4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbDiagnoses4.Location = new System.Drawing.Point(264, 216);
			this.chbDiagnoses4.Name = "chbDiagnoses4";
			this.chbDiagnoses4.Size = new System.Drawing.Size(72, 24);
			this.chbDiagnoses4.TabIndex = 100226;
			this.chbDiagnoses4.TabStop = false;
			this.chbDiagnoses4.Text = "内窥镜";
			// 
			// chbDiagnoses3
			// 
			this.chbDiagnoses3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbDiagnoses3.Location = new System.Drawing.Point(200, 216);
			this.chbDiagnoses3.Name = "chbDiagnoses3";
			this.chbDiagnoses3.Size = new System.Drawing.Size(72, 24);
			this.chbDiagnoses3.TabIndex = 100225;
			this.chbDiagnoses3.Tag = "3";
			this.chbDiagnoses3.Text = "超声波";
			// 
			// chbDiagnoses2
			// 
			this.chbDiagnoses2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbDiagnoses2.Location = new System.Drawing.Point(160, 216);
			this.chbDiagnoses2.Name = "chbDiagnoses2";
			this.chbDiagnoses2.Size = new System.Drawing.Size(56, 24);
			this.chbDiagnoses2.TabIndex = 100224;
			this.chbDiagnoses2.TabStop = false;
			this.chbDiagnoses2.Tag = "2";
			this.chbDiagnoses2.Text = "X光";
			// 
			// chbDiagnoses1
			// 
			this.chbDiagnoses1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.chbDiagnoses1.Location = new System.Drawing.Point(104, 216);
			this.chbDiagnoses1.Name = "chbDiagnoses1";
			this.chbDiagnoses1.Size = new System.Drawing.Size(72, 24);
			this.chbDiagnoses1.TabIndex = 100223;
			this.chbDiagnoses1.Tag = "1";
			this.chbDiagnoses1.Text = "临床";
			// 
			// label42
			// 
			this.label42.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label42.Location = new System.Drawing.Point(40, 368);
			this.label42.Name = "label42";
			this.label42.Size = new System.Drawing.Size(216, 16);
			this.label42.TabIndex = 100222;
			this.label42.Text = "原诊断(原报告诊断有误时填写):";
			// 
			// label38
			// 
			this.label38.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label38.Location = new System.Drawing.Point(224, 288);
			this.label38.Name = "label38";
			this.label38.Size = new System.Drawing.Size(72, 24);
			this.label38.TabIndex = 100220;
			this.label38.Text = "治疗方式:";
			this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label36
			// 
			this.label36.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label36.Location = new System.Drawing.Point(40, 288);
			this.label36.Name = "label36";
			this.label36.Size = new System.Drawing.Size(72, 24);
			this.label36.TabIndex = 100219;
			this.label36.Text = "是否治疗:";
			// 
			// label35
			// 
			this.label35.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label35.Location = new System.Drawing.Point(40, 216);
			this.label35.Name = "label35";
			this.label35.Size = new System.Drawing.Size(72, 24);
			this.label35.TabIndex = 100218;
			this.label35.Text = "诊断依据:";
			// 
			// txtICDO
			// 
			this.txtICDO.Location = new System.Drawing.Point(656, 184);
			this.txtICDO.Name = "txtICDO";
			this.txtICDO.Size = new System.Drawing.Size(96, 21);
			this.txtICDO.TabIndex = 100217;
			this.txtICDO.Text = "";
			// 
			// label34
			// 
			this.label34.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label34.Location = new System.Drawing.Point(568, 184);
			this.label34.Name = "label34";
			this.label34.Size = new System.Drawing.Size(88, 24);
			this.label34.TabIndex = 100216;
			this.label34.Text = "ICD-O编码 :";
			this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtPathlogyDegree
			// 
			this.txtPathlogyDegree.Location = new System.Drawing.Point(488, 184);
			this.txtPathlogyDegree.Name = "txtPathlogyDegree";
			this.txtPathlogyDegree.Size = new System.Drawing.Size(80, 21);
			this.txtPathlogyDegree.TabIndex = 100215;
			this.txtPathlogyDegree.Text = "";
			this.txtPathlogyDegree.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPathlogyDegree_KeyPress);
			// 
			// label33
			// 
			this.label33.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label33.Location = new System.Drawing.Point(416, 184);
			this.label33.Name = "label33";
			this.label33.Size = new System.Drawing.Size(72, 24);
			this.label33.TabIndex = 100214;
			this.label33.Text = "分化程度:";
			this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtPathlogyType
			// 
			this.txtPathlogyType.Location = new System.Drawing.Point(256, 184);
			this.txtPathlogyType.Name = "txtPathlogyType";
			this.txtPathlogyType.Size = new System.Drawing.Size(152, 21);
			this.txtPathlogyType.TabIndex = 100213;
			this.txtPathlogyType.Text = "";
			this.txtPathlogyType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPathlogyType_KeyPress);
			// 
			// label31
			// 
			this.label31.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label31.Location = new System.Drawing.Point(176, 184);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(88, 24);
			this.label31.TabIndex = 100212;
			this.label31.Text = "病理学分型:";
			this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtPathlogyNo
			// 
			this.txtPathlogyNo.Location = new System.Drawing.Point(88, 184);
			this.txtPathlogyNo.Name = "txtPathlogyNo";
			this.txtPathlogyNo.Size = new System.Drawing.Size(88, 21);
			this.txtPathlogyNo.TabIndex = 100211;
			this.txtPathlogyNo.Text = "";
			this.txtPathlogyNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPathlogyNo_KeyPress);
			// 
			// label32
			// 
			this.label32.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label32.Location = new System.Drawing.Point(40, 184);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(56, 24);
			this.label32.TabIndex = 100210;
			this.label32.Text = "病理号:";
			// 
			// label30
			// 
			this.label30.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label30.Location = new System.Drawing.Point(544, 152);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(136, 23);
			this.label30.TabIndex = 100208;
			this.label30.Text = "是否做病理学检查：";
			this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label29
			// 
			this.label29.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label29.Location = new System.Drawing.Point(272, 152);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(8, 24);
			this.label29.TabIndex = 100202;
			this.label29.Text = "M";
			this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtClinicalM
			// 
			this.txtClinicalM.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.txtClinicalM.Location = new System.Drawing.Point(280, 152);
			this.txtClinicalM.MaxLength = 3;
			this.txtClinicalM.Name = "txtClinicalM";
			this.txtClinicalM.Size = new System.Drawing.Size(32, 23);
			this.txtClinicalM.TabIndex = 100201;
			this.txtClinicalM.TabStop = false;
			this.txtClinicalM.Text = "";
			// 
			// label28
			// 
			this.label28.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label28.Location = new System.Drawing.Point(224, 152);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(16, 24);
			this.label28.TabIndex = 100200;
			this.label28.Text = "N";
			this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtClinicalN
			// 
			this.txtClinicalN.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.txtClinicalN.Location = new System.Drawing.Point(240, 152);
			this.txtClinicalN.MaxLength = 3;
			this.txtClinicalN.Name = "txtClinicalN";
			this.txtClinicalN.Size = new System.Drawing.Size(24, 23);
			this.txtClinicalN.TabIndex = 100199;
			this.txtClinicalN.TabStop = false;
			this.txtClinicalN.Text = "";
			this.txtClinicalN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtClinicalN_KeyPress);
			// 
			// label27
			// 
			this.label27.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label27.Location = new System.Drawing.Point(176, 152);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(16, 24);
			this.label27.TabIndex = 100198;
			this.label27.Text = "T";
			this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label26
			// 
			this.label26.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label26.Location = new System.Drawing.Point(40, 152);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(160, 24);
			this.label26.TabIndex = 100197;
			this.label26.Text = "临床分期（TNM分期）：";
			// 
			// txtClinicalT
			// 
			this.txtClinicalT.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.txtClinicalT.Location = new System.Drawing.Point(200, 152);
			this.txtClinicalT.MaxLength = 3;
			this.txtClinicalT.Name = "txtClinicalT";
			this.txtClinicalT.Size = new System.Drawing.Size(24, 23);
			this.txtClinicalT.TabIndex = 100196;
			this.txtClinicalT.TabStop = false;
			this.txtClinicalT.Text = "";
			this.txtClinicalT.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtClinicalT_KeyPress);
			// 
			// label25
			// 
			this.label25.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label25.Location = new System.Drawing.Point(432, 8);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(64, 24);
			this.label25.TabIndex = 100194;
			this.label25.Text = "ICD编码:";
			this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtRelationship
			// 
			this.txtRelationship.Location = new System.Drawing.Point(88, 120);
			this.txtRelationship.Name = "txtRelationship";
			this.txtRelationship.Size = new System.Drawing.Size(88, 21);
			this.txtRelationship.TabIndex = 100193;
			this.txtRelationship.Text = "";
			this.txtRelationship.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRelationship_KeyPress);
			// 
			// label23
			// 
			this.label23.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label23.Location = new System.Drawing.Point(40, 120);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(48, 24);
			this.label23.TabIndex = 100192;
			this.label23.Text = "关系:";
			// 
			// txtContactPersonAddr
			// 
			this.txtContactPersonAddr.Location = new System.Drawing.Point(432, 120);
			this.txtContactPersonAddr.Name = "txtContactPersonAddr";
			this.txtContactPersonAddr.Size = new System.Drawing.Size(320, 21);
			this.txtContactPersonAddr.TabIndex = 100191;
			this.txtContactPersonAddr.Text = "";
			this.txtContactPersonAddr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtContactPersonAddr_KeyPress);
			// 
			// label22
			// 
			this.label22.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label22.Location = new System.Drawing.Point(352, 120);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(72, 24);
			this.label22.TabIndex = 100190;
			this.label22.Text = "联系地址:";
			this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtContactPersonTel
			// 
			this.txtContactPersonTel.Location = new System.Drawing.Point(256, 120);
			this.txtContactPersonTel.Name = "txtContactPersonTel";
			this.txtContactPersonTel.Size = new System.Drawing.Size(88, 21);
			this.txtContactPersonTel.TabIndex = 100189;
			this.txtContactPersonTel.Text = "";
			this.txtContactPersonTel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtContactPersonTel_KeyPress);
			// 
			// label21
			// 
			this.label21.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label21.Location = new System.Drawing.Point(184, 120);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(72, 24);
			this.label21.TabIndex = 100188;
			this.label21.Text = "联系电话:";
			this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtContactPerson
			// 
			this.txtContactPerson.Location = new System.Drawing.Point(648, 88);
			this.txtContactPerson.Name = "txtContactPerson";
			this.txtContactPerson.Size = new System.Drawing.Size(104, 21);
			this.txtContactPerson.TabIndex = 100187;
			this.txtContactPerson.Text = "";
			this.txtContactPerson.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtContactPerson_KeyPress);
			// 
			// label20
			// 
			this.label20.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label20.Location = new System.Drawing.Point(568, 88);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(88, 24);
			this.label20.TabIndex = 100186;
			this.label20.Text = "联系人姓名:";
			this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtWorkPlace
			// 
			this.txtWorkPlace.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.txtWorkPlace.Location = new System.Drawing.Point(232, 88);
			this.txtWorkPlace.Name = "txtWorkPlace";
			this.txtWorkPlace.Size = new System.Drawing.Size(328, 23);
			this.txtWorkPlace.TabIndex = 100184;
			this.txtWorkPlace.Text = "";
			this.txtWorkPlace.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtWorkPlace_KeyPress);
			// 
			// label58
			// 
			this.label58.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label58.Location = new System.Drawing.Point(168, 88);
			this.label58.Name = "label58";
			this.label58.Size = new System.Drawing.Size(80, 23);
			this.label58.TabIndex = 100185;
			this.label58.Text = "单位地址：";
			this.label58.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label91
			// 
			this.label91.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label91.ForeColor = System.Drawing.Color.Black;
			this.label91.Location = new System.Drawing.Point(704, 56);
			this.label91.Name = "label91";
			this.label91.Size = new System.Drawing.Size(48, 23);
			this.label91.TabIndex = 100182;
			this.label91.Text = "门牌号";
			this.label91.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtRegisterHouseNO
			// 
			this.txtRegisterHouseNO.Location = new System.Drawing.Point(616, 56);
			this.txtRegisterHouseNO.Name = "txtRegisterHouseNO";
			this.txtRegisterHouseNO.Size = new System.Drawing.Size(88, 21);
			this.txtRegisterHouseNO.TabIndex = 100178;
			this.txtRegisterHouseNO.Text = "";
			this.txtRegisterHouseNO.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRegisterHouseNO_KeyPress);
			// 
			// txtRegisterTown
			// 
			this.txtRegisterTown.Location = new System.Drawing.Point(464, 56);
			this.txtRegisterTown.Name = "txtRegisterTown";
			this.txtRegisterTown.Size = new System.Drawing.Size(88, 21);
			this.txtRegisterTown.TabIndex = 100177;
			this.txtRegisterTown.Text = "";
			this.txtRegisterTown.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRegisterTown_KeyPress);
			// 
			// label50
			// 
			this.label50.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label50.Location = new System.Drawing.Point(40, 56);
			this.label50.Name = "label50";
			this.label50.Size = new System.Drawing.Size(80, 24);
			this.label50.TabIndex = 100181;
			this.label50.Text = "户口地址：";
			this.label50.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label51
			// 
			this.label51.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label51.Location = new System.Drawing.Point(552, 56);
			this.label51.Name = "label51";
			this.label51.Size = new System.Drawing.Size(64, 23);
			this.label51.TabIndex = 100180;
			this.label51.Text = "街道(镇)";
			this.label51.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label52
			// 
			this.label52.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label52.Location = new System.Drawing.Point(416, 56);
			this.label52.Name = "label52";
			this.label52.Size = new System.Drawing.Size(48, 23);
			this.label52.TabIndex = 100179;
			this.label52.Text = "区(县)";
			this.label52.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label11
			// 
			this.label11.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label11.Location = new System.Drawing.Point(432, 32);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(80, 23);
			this.label11.TabIndex = 100174;
			this.label11.Text = "婚姻状况：";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtPost
			// 
			this.txtPost.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.txtPost.Location = new System.Drawing.Point(104, 88);
			this.txtPost.Name = "txtPost";
			this.txtPost.Size = new System.Drawing.Size(64, 23);
			this.txtPost.TabIndex = 100082;
			this.txtPost.Text = "";
			this.txtPost.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPost_KeyPress);
			// 
			// label13
			// 
			this.label13.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label13.ForeColor = System.Drawing.Color.Black;
			this.label13.Location = new System.Drawing.Point(40, 88);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(72, 23);
			this.label13.TabIndex = 100081;
			this.label13.Text = "邮政编码:";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtWorkType
			// 
			this.txtWorkType.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.txtWorkType.Location = new System.Drawing.Point(120, 32);
			this.txtWorkType.MaxLength = 19;
			this.txtWorkType.Name = "txtWorkType";
			this.txtWorkType.Size = new System.Drawing.Size(88, 23);
			this.txtWorkType.TabIndex = 100071;
			this.txtWorkType.Text = "";
			this.txtWorkType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtWorkType_KeyPress);
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label4.Location = new System.Drawing.Point(40, 32);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(48, 23);
			this.label4.TabIndex = 100072;
			this.label4.Text = "工种：";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label9.Location = new System.Drawing.Point(240, 32);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(48, 23);
			this.label9.TabIndex = 100072;
			this.label9.Text = "民族：";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.rdbTreatN);
			this.groupBox3.Controls.Add(this.rdbTreatY);
			this.groupBox3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.groupBox3.Location = new System.Drawing.Point(104, 280);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(104, 32);
			this.groupBox3.TabIndex = 100279;
			this.groupBox3.TabStop = false;
			// 
			// rdbTreatN
			// 
			this.rdbTreatN.Location = new System.Drawing.Point(48, 8);
			this.rdbTreatN.Name = "rdbTreatN";
			this.rdbTreatN.Size = new System.Drawing.Size(32, 24);
			this.rdbTreatN.TabIndex = 1;
			this.rdbTreatN.Text = "否";
			// 
			// rdbTreatY
			// 
			this.rdbTreatY.Location = new System.Drawing.Point(8, 8);
			this.rdbTreatY.Name = "rdbTreatY";
			this.rdbTreatY.Size = new System.Drawing.Size(32, 24);
			this.rdbTreatY.TabIndex = 0;
			this.rdbTreatY.Text = "是";
			this.rdbTreatY.CheckedChanged += new System.EventHandler(this.rdbTreatY_CheckedChanged);
			// 
			// label40
			// 
			this.label40.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label40.Location = new System.Drawing.Point(40, 336);
			this.label40.Name = "label40";
			this.label40.Size = new System.Drawing.Size(48, 24);
			this.label40.TabIndex = 100221;
			this.label40.Text = "转归：";
			// 
			// ucCancerAddition
			// 
			this.BackColor = System.Drawing.SystemColors.Desktop;
			this.Controls.Add(this.panel1);
			this.Name = "ucCancerAddition";
			this.Size = new System.Drawing.Size(768, 392);
			this.panel1.ResumeLayout(false);
			this.rdbReback.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region 变量
		private FS.HISFC.DCP.Object.CancerAdd cancerAdd=new FS.HISFC.DCP.Object.CancerAdd();
		private FS.HISFC.DCP.Object.CancerAddExt cancerExt= new FS.HISFC.DCP.Object.CancerAddExt();
        private FS.SOC.HISFC.BizLogic.DCP.CancerAdd  cancerAddMrg=new FS.SOC.HISFC.BizLogic.DCP.CancerAdd();
		private FS.SOC.HISFC.BizLogic.DCP.CancerExt cancerExtMrg= new FS.SOC.HISFC.BizLogic.DCP.CancerExt();
		#endregion 
 

		#region   肿瘤报卡实体信息保存
		/// <summary>
		/// 肿瘤报卡实体信息保存
		/// </summary>
		/// <returns></returns>
		public  FS.HISFC.DCP.Object.CancerAdd  SaveCancerAddData( string Report_No )
		{
			FS.HISFC.DCP.Object.CancerAdd obj = new FS.HISFC.DCP.Object.CancerAdd();
			
			obj.REPORT_NO=Report_No;
			obj.MEIDCAL_CARD=this.txtMedical_Card.Text;
			obj.NATION=this.txtNation.Text;
			obj.WORK_TYPE=this.txtWorkType.Text;
			obj.MARRIAGE=this.txtMarriage.Text ;
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
			obj.OLD_DIAGNOSES_DATE = System.Convert .ToDateTime( this.dtOldDiagnosesTime.Text.ToString());
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
			foreach (Control ctl in this.panel1.Controls)
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
		#region   转归使能
		public void SetRebackEnable(bool status)
		{
			this.txtRebackDemo.Enabled=status;
			if(!status)
			{
				this.txtRebackDemo.Clear();
			 }
				
		
		}
		#endregion

		private void rdbReback5_CheckedChanged(object sender, System.EventArgs e)
		{
			this.SetRebackEnable(this.rdbReback5.Checked);
				
		}

		private void rdbTreatY_CheckedChanged(object sender, System.EventArgs e)
		{
		      this.SetTreatmetEnable(this.rdbTreatY.Checked);
		}

		private void chbDiagnoses19_CheckStateChanged(object sender, System.EventArgs e)
		{
			this.txtDiagnosesDemo.Enabled=this.chbDiagnoses19.Checked;
		}

		
		#region 装载肿瘤卡报表到界面层
		
		public void LoadCancer( string Report_No)
		{
			this.cancerAdd=this.cancerAddMrg.GetCancerAddByNO(Report_No);
			if (this.cancerAdd != null )

			{               
				this.txtMedical_Card.Text=this.cancerAdd.MEIDCAL_CARD ;     
				this.txtNation.Text      =this.cancerAdd.NATION               ;
				this.txtWorkType.Text    =this.cancerAdd.WORK_TYPE   ;          ;
				this.txtMarriage.Text    =this.cancerAdd.MARRIAGE           ;
				this.txtICD.Text         =this.cancerAdd.DIAGNOSTIC_ICD       ;
				this.txtRegisterProvince.Text=this.cancerAdd.REGISTER_PROVINCE   ;
				this.txtRegisterCity.Text    =this.cancerAdd.REGISTER_CITY        ;
				this.txtRegisterCounty.Text  =this.cancerAdd.REGISTER_DISTRICT    ;
				this.txtRegisterTown.Text    =this.cancerAdd.REGISTER_STREET       ;
				this.txtRegisterHouseNO.Text =this.cancerAdd.REGISTER_HOUSENUMBER  ;
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
						this.rdbTNM1.Checked=true;	
						break;
					case "否":
						this.rdbTNM2.Checked=true;	 
						break;
					default:
						break;
				}
							
				this.txtPathlogyNo.Text=this.cancerAdd.PATHOLOGY_NO          ;
				this.txtPathlogyType.Text=this.cancerAdd.PATHOLOGY_TYPE        ;
				this.txtPathlogyDegree.Text=this.cancerAdd.PATHOLOGY_DEGREE      ;
				this.txtICDO.Text=this.cancerAdd.ICD_O                 ;

				//this.cancerAdd.TREATMENT             ;
				switch(this.cancerAdd.TREATMENT.ToString ())
				{
					case "是":
						this.rdbTreatY.Checked=true;	
						break;
					case "否":
						this.rdbTreatY.Checked=true;	 
						break;
					default:
						break;
				}
				this.txtDeathResion.Text=this.cancerAdd.DEATH_REASON          ;
				this.txtOldDiagnoses.Text=this.cancerAdd.OLD_DIAGNOSES         ;
				this.dtOldDiagnosesTime.Text=this.cancerAdd.OLD_DIAGNOSES_DATE.ToString();
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
								this.chbTreatment1.Checked=true;
							}
							if (ca.Item_Code==  "2")
							{
								this.chbTreatment2.Checked=true;
							}
							if (ca.Item_Code==  "3")
							{
								this.chbTreatment3.Checked=true;
							}
							if (ca.Item_Code==  "4")
							{
								this.chbTreatment4.Checked=true;
							}
							if (ca.Item_Code==  "5")
							{
								this.chbTreatment5.Checked=true;
								this.txtTreatmentDemo.Text=ca.Item_Demo;
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
        
        private void txtMedical_Card_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
		
			if(e.KeyChar == (char)13)
			{
				this.txtICD.Select();
				this.txtICD.SelectAll();
				this.txtICD.Focus();
			}
		}

		private void txtNation_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
		
					
			if(e.KeyChar == (char)13)
			{
				this.txtMarriage.Select();
				this.txtMarriage.SelectAll();
				this.txtMarriage.Focus();
			}
		}

		private void txtMarriage_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
				
			if(e.KeyChar == (char)13)
			{
				this.txtRegisterProvince.Select();
				this.txtRegisterProvince.SelectAll();
				this.txtRegisterProvince.Focus();
			}
		}

		private void txtWorkType_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtNation.Select();
				this.txtNation.SelectAll();
				this.txtNation.Focus();
			}
		}

		private void txtICD_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtRegisterProvince.Select();
				this.txtRegisterProvince.SelectAll();
				this.txtRegisterProvince.Focus();
			}
		}

		private void txtRegisterProvince_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtRegisterCity.Select();
				this.txtRegisterCity.SelectAll();
				this.txtRegisterCity.Focus();
			}
		}

		private void txtRegisterCity_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtRegisterCounty.Select();
				this.txtRegisterCounty.SelectAll();
				this.txtRegisterCounty.Focus();
			}
		}

		private void txtRegisterCounty_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtRegisterTown.Select();
				this.txtRegisterTown.SelectAll();
				this.txtRegisterTown.Focus();
			}
		}

		private void txtRegisterTown_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtRegisterHouseNO.Select();
				this.txtRegisterHouseNO.SelectAll();
				this.txtRegisterHouseNO.Focus();
			}
		}

		private void txtRegisterHouseNO_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtPost.Select();
				this.txtPost.SelectAll();
				this.txtPost.Focus();
			}
		}

		private void txtPost_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtWorkPlace.Select();
				this.txtWorkPlace.SelectAll();
				this.txtWorkPlace.Focus();
			}
		}

		private void txtWorkPlace_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtContactPerson.Select();
				this.txtContactPerson.SelectAll();
				this.txtContactPerson.Focus();
			}
		}

		private void txtContactPerson_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtRelationship.Select();
				this.txtRelationship.SelectAll();
				this.txtRelationship.Focus();
			}
		}

		private void txtRelationship_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtContactPersonTel .Select();
				this.txtContactPersonTel.SelectAll();
				this.txtContactPersonTel.Focus();
			}
		}

		private void txtContactPersonTel_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtContactPersonAddr .Select();
				this.txtContactPersonAddr.SelectAll();
				this.txtContactPersonAddr.Focus();
			}
		}

		private void txtContactPersonAddr_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtClinicalT .Select();
				this.txtClinicalT.SelectAll();
				this.txtClinicalT.Focus();
			}
		}

		private void txtClinicalT_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtClinicalN .Select();
				this.txtClinicalN.SelectAll();
				this.txtClinicalN.Focus();
			}
		}

		private void txtClinicalN_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtClinicalM .Select();
				this.txtClinicalM.SelectAll();
				this.txtClinicalM.Focus();
			}
		}
		
		private void txtClinicalM_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtPathlogyNo .Select();
				this.txtPathlogyNo.SelectAll();
				this.txtPathlogyNo.Focus();
			}
		}
		
		private void txtPathlogyNo_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtPathlogyType.Select();
				this.txtPathlogyType.SelectAll();
				this.txtPathlogyType.Focus();
			}
		}

		private void txtPathlogyType_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtPathlogyDegree.Select();
				this.txtPathlogyDegree.SelectAll();
				this.txtPathlogyDegree.Focus();
			}
		}

		private void txtPathlogyDegree_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				this.txtICDO.Select();
				this.txtICDO.SelectAll();
				this.txtICDO.Focus();
			}
		}

		private void chbTreatment9_CheckedChanged(object sender, System.EventArgs e)
		{
			 this.txtTreatmentDemo.Enabled=this.chbTreatment9.Checked;
			if(!this.chbTreatment9.Checked)
			{
              this.txtTreatmentDemo.Clear();		
			}
		}

		
	}
}
