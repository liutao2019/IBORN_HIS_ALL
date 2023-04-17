using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace SOC.Local.PacsInterface.GYSY.InPaitent
{
	public delegate void PacsApplyHandler(string CheckPart,string memo);
	/// <summary>
	/// ucPacsApply ��ժҪ˵����
	/// </summary>
	public class ucPacsApply : System.Windows.Forms.UserControl
	{
		# region ϵͳ���ɿؼ�����
        private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label lblNo;
		private System.Windows.Forms.Label lblPatientNo;
		private System.Windows.Forms.Label lblClincNo;
		private System.Windows.Forms.Label lblDate;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Label lblAge;
		private System.Windows.Forms.Label lblNurseStation;
		private System.Windows.Forms.Label lblSex;
		private System.Windows.Forms.Label lblPaykind;
        private System.Windows.Forms.Label lblDoc;
		private System.Windows.Forms.Label lblApplyName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel panel7;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Panel panel8;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Panel panel9;
		private System.Windows.Forms.Label label6;
		private FS.HISFC.Components.Common.Controls.ucUserText ucUserText2;
		private System.Windows.Forms.Label lblPacsBillID;
		public event PacsApplyHandler ApplyEvent;
        private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.RichTextBox txtItems;
		private System.Windows.Forms.RichTextBox txtDiagnose;
		private System.Windows.Forms.RichTextBox txt1;
		private System.Windows.Forms.RichTextBox txtMemo;
		private System.Windows.Forms.RichTextBox txtAttention;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox tbCardNo;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Label lblBed;
		private System.Windows.Forms.Label label28;
		private System.Windows.Forms.Label lblExeDept;
		private System.Windows.Forms.Label label29;
		private System.Windows.Forms.Label label30;
		private FS.FrameWork.WinForms.Controls.NeuComboBox cmbPacsItem0;
		private FS.FrameWork.WinForms.Controls.NeuComboBox cmbPacsCheckType0;
		private FS.FrameWork.WinForms.Controls.NeuComboBox cmbPacsItem1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbPacsItem2;
		private FS.FrameWork.WinForms.Controls.NeuComboBox cmbPacsCheckType1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbPacsCheckType2;
		private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblbq;
        private System.Windows.Forms.Label reprint;
        private System.Windows.Forms.Label label33;
		private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label35;
        private TextBox bodyPost;
        private Label label34;
        private ComboBox Antibiotic2;
        private ComboBox SpecimenType;
        private Label exec_sqn;
        private Label label32;
        private TextBox Temperature;
        private ComboBox Antibiotic1;
        private Label label31;
        private Label label25;
        private Label label9;
        private Panel panelSpecail;
        private CheckBox chkJJ;
        private DateTimePicker dtJJ;
        private Label label26;
        private DateTimePicker dtSample;
        private Label label27;
        private PictureBox picbLogo;
        protected FS.FrameWork.WinForms.Controls.NeuPictureBox npbBarCode;
        private Label label1;
		private FS.FrameWork.WinForms.Controls.NeuComboBox cmbMachine;
		# endregion
		#region �����������ɵĴ���
		/// <summary> 
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭�� 
		/// �޸Ĵ˷��������ݡ�
		/// </summary>
		private void InitializeComponent() 
		{
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.npbBarCode = new FS.FrameWork.WinForms.Controls.NeuPictureBox();
            this.picbLogo = new System.Windows.Forms.PictureBox();
            this.label35 = new System.Windows.Forms.Label();
            this.bodyPost = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.Antibiotic2 = new System.Windows.Forms.ComboBox();
            this.SpecimenType = new System.Windows.Forms.ComboBox();
            this.exec_sqn = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.reprint = new System.Windows.Forms.Label();
            this.Temperature = new System.Windows.Forms.TextBox();
            this.Antibiotic1 = new System.Windows.Forms.ComboBox();
            this.label31 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblbq = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbPacsCheckType2 = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbPacsCheckType1 = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbPacsItem2 = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbPacsItem1 = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbPacsCheckType0 = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbPacsItem0 = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label30 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.lblExeDept = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.lblNurseStation = new System.Windows.Forms.Label();
            this.lblBed = new System.Windows.Forms.Label();
            this.panelSpecail = new System.Windows.Forms.Panel();
            this.chkJJ = new System.Windows.Forms.CheckBox();
            this.dtJJ = new System.Windows.Forms.DateTimePicker();
            this.label26 = new System.Windows.Forms.Label();
            this.dtSample = new System.Windows.Forms.DateTimePicker();
            this.label27 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.lblAge = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.lblSex = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbMachine = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.txtAttention = new System.Windows.Forms.RichTextBox();
            this.txtMemo = new System.Windows.Forms.RichTextBox();
            this.txt1 = new System.Windows.Forms.RichTextBox();
            this.txtDiagnose = new System.Windows.Forms.RichTextBox();
            this.txtItems = new System.Windows.Forms.RichTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lblPacsBillID = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblPaykind = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblClincNo = new System.Windows.Forms.Label();
            this.lblPatientNo = new System.Windows.Forms.Label();
            this.lblNo = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblDoc = new System.Windows.Forms.Label();
            this.lblApplyName = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tbCardNo = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.ucUserText2 = new FS.HISFC.Components.Common.Controls.ucUserText();
            this.panel8 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.npbBarCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbLogo)).BeginInit();
            this.panelSpecail.SuspendLayout();
            this.panel7.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel8.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.npbBarCode);
            this.panel1.Controls.Add(this.picbLogo);
            this.panel1.Controls.Add(this.label35);
            this.panel1.Controls.Add(this.bodyPost);
            this.panel1.Controls.Add(this.label34);
            this.panel1.Controls.Add(this.label33);
            this.panel1.Controls.Add(this.Antibiotic2);
            this.panel1.Controls.Add(this.SpecimenType);
            this.panel1.Controls.Add(this.exec_sqn);
            this.panel1.Controls.Add(this.label32);
            this.panel1.Controls.Add(this.reprint);
            this.panel1.Controls.Add(this.Temperature);
            this.panel1.Controls.Add(this.Antibiotic1);
            this.panel1.Controls.Add(this.label31);
            this.panel1.Controls.Add(this.label25);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.lblbq);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cmbPacsCheckType2);
            this.panel1.Controls.Add(this.cmbPacsCheckType1);
            this.panel1.Controls.Add(this.cmbPacsItem2);
            this.panel1.Controls.Add(this.cmbPacsItem1);
            this.panel1.Controls.Add(this.cmbPacsCheckType0);
            this.panel1.Controls.Add(this.cmbPacsItem0);
            this.panel1.Controls.Add(this.label30);
            this.panel1.Controls.Add(this.label29);
            this.panel1.Controls.Add(this.lblExeDept);
            this.panel1.Controls.Add(this.label28);
            this.panel1.Controls.Add(this.lblNurseStation);
            this.panel1.Controls.Add(this.lblBed);
            this.panel1.Controls.Add(this.panelSpecail);
            this.panel1.Controls.Add(this.label24);
            this.panel1.Controls.Add(this.label23);
            this.panel1.Controls.Add(this.lblAge);
            this.panel1.Controls.Add(this.label21);
            this.panel1.Controls.Add(this.lblSex);
            this.panel1.Controls.Add(this.label20);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.lblDate);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.cmbMachine);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtAttention);
            this.panel1.Controls.Add(this.txtMemo);
            this.panel1.Controls.Add(this.txt1);
            this.panel1.Controls.Add(this.txtDiagnose);
            this.panel1.Controls.Add(this.txtItems);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.lblPacsBillID);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.panel9);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.lblPaykind);
            this.panel1.Controls.Add(this.lblName);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.lblClincNo);
            this.panel1.Controls.Add(this.lblPatientNo);
            this.panel1.Controls.Add(this.lblNo);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.lblDoc);
            this.panel1.Controls.Add(this.lblApplyName);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.label22);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(714, 828);
            this.panel1.TabIndex = 0;
            // 
            // npbBarCode
            // 
            this.npbBarCode.Location = new System.Drawing.Point(534, 22);
            this.npbBarCode.Name = "npbBarCode";
            this.npbBarCode.Size = new System.Drawing.Size(150, 39);
            this.npbBarCode.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.npbBarCode.TabIndex = 144;
            this.npbBarCode.TabStop = false;
            // 
            // picbLogo
            // 
            this.picbLogo.Location = new System.Drawing.Point(207, 11);
            this.picbLogo.Name = "picbLogo";
            this.picbLogo.Size = new System.Drawing.Size(316, 49);
            this.picbLogo.TabIndex = 143;
            this.picbLogo.TabStop = false;
            // 
            // label35
            // 
            this.label35.Font = new System.Drawing.Font("����", 10.5F);
            this.label35.Location = new System.Drawing.Point(185, 135);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(83, 13);
            this.label35.TabIndex = 108;
            // 
            // bodyPost
            // 
            this.bodyPost.Location = new System.Drawing.Point(147, 725);
            this.bodyPost.Name = "bodyPost";
            this.bodyPost.Size = new System.Drawing.Size(100, 21);
            this.bodyPost.TabIndex = 107;
            this.bodyPost.Visible = false;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label34.Location = new System.Drawing.Point(100, 725);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(49, 14);
            this.label34.TabIndex = 106;
            this.label34.Text = "��λ��";
            this.label34.Visible = false;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(200, 785);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(17, 12);
            this.label33.TabIndex = 105;
            this.label33.Text = "��";
            this.label33.Visible = false;
            // 
            // Antibiotic2
            // 
            this.Antibiotic2.Location = new System.Drawing.Point(247, 802);
            this.Antibiotic2.Name = "Antibiotic2";
            this.Antibiotic2.Size = new System.Drawing.Size(162, 20);
            this.Antibiotic2.TabIndex = 104;
            this.Antibiotic2.Visible = false;
            this.Antibiotic2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Antibiotic2_KeyDown);
            // 
            // SpecimenType
            // 
            this.SpecimenType.Location = new System.Drawing.Point(310, 782);
            this.SpecimenType.Name = "SpecimenType";
            this.SpecimenType.Size = new System.Drawing.Size(99, 20);
            this.SpecimenType.TabIndex = 103;
            this.SpecimenType.Visible = false;
            this.SpecimenType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SpecimenType_KeyDown);
            // 
            // exec_sqn
            // 
            this.exec_sqn.Location = new System.Drawing.Point(506, 804);
            this.exec_sqn.Name = "exec_sqn";
            this.exec_sqn.Size = new System.Drawing.Size(100, 23);
            this.exec_sqn.TabIndex = 102;
            this.exec_sqn.Visible = false;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label32.Location = new System.Drawing.Point(440, 783);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(77, 14);
            this.label32.TabIndex = 101;
            this.label32.Text = "���鵥�ţ�";
            this.label32.Visible = false;
            // 
            // reprint
            // 
            this.reprint.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.reprint.ForeColor = System.Drawing.Color.Red;
            this.reprint.Location = new System.Drawing.Point(101, 41);
            this.reprint.Name = "reprint";
            this.reprint.Size = new System.Drawing.Size(59, 23);
            this.reprint.TabIndex = 100;
            this.reprint.Text = "��  ��";
            this.reprint.Visible = false;
            // 
            // Temperature
            // 
            this.Temperature.Location = new System.Drawing.Point(127, 782);
            this.Temperature.Name = "Temperature";
            this.Temperature.Size = new System.Drawing.Size(67, 21);
            this.Temperature.TabIndex = 99;
            this.Temperature.Visible = false;
            // 
            // Antibiotic1
            // 
            this.Antibiotic1.Location = new System.Drawing.Point(104, 801);
            this.Antibiotic1.Name = "Antibiotic1";
            this.Antibiotic1.Size = new System.Drawing.Size(124, 20);
            this.Antibiotic1.TabIndex = 97;
            this.Antibiotic1.Visible = false;
            this.Antibiotic1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Antibiotic1_KeyDown);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label31.Location = new System.Drawing.Point(240, 785);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(77, 14);
            this.label31.TabIndex = 96;
            this.label31.Text = "�걾���ͣ�";
            this.label31.Visible = false;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label25.Location = new System.Drawing.Point(34, 783);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(91, 14);
            this.label25.TabIndex = 95;
            this.label25.Text = "��Ѫʱ���£�";
            this.label25.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(44, 807);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 14);
            this.label9.TabIndex = 94;
            this.label9.Text = "�����أ�";
            this.label9.Visible = false;
            // 
            // lblbq
            // 
            this.lblbq.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblbq.Location = new System.Drawing.Point(319, 135);
            this.lblbq.Name = "lblbq";
            this.lblbq.Size = new System.Drawing.Size(100, 16);
            this.lblbq.TabIndex = 93;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(276, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 16);
            this.label5.TabIndex = 92;
            this.label5.Text = "������";
            // 
            // cmbPacsCheckType2
            // 
            this.cmbPacsCheckType2.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbPacsCheckType2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPacsCheckType2.IsEnter2Tab = false;
            this.cmbPacsCheckType2.IsFlat = false;
            this.cmbPacsCheckType2.IsLike = true;
            this.cmbPacsCheckType2.IsListOnly = false;
            this.cmbPacsCheckType2.IsPopForm = true;
            this.cmbPacsCheckType2.IsShowCustomerList = false;
            this.cmbPacsCheckType2.IsShowID = false;
            this.cmbPacsCheckType2.Location = new System.Drawing.Point(491, 606);
            this.cmbPacsCheckType2.Name = "cmbPacsCheckType2";
            this.cmbPacsCheckType2.PopForm = null;
            this.cmbPacsCheckType2.ShowCustomerList = false;
            this.cmbPacsCheckType2.ShowID = false;
            this.cmbPacsCheckType2.Size = new System.Drawing.Size(170, 20);
            this.cmbPacsCheckType2.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbPacsCheckType2.TabIndex = 87;
            this.cmbPacsCheckType2.Tag = "";
            this.cmbPacsCheckType2.ToolBarUse = false;
            // 
            // cmbPacsCheckType1
            // 
            this.cmbPacsCheckType1.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbPacsCheckType1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPacsCheckType1.IsEnter2Tab = false;
            this.cmbPacsCheckType1.IsFlat = false;
            this.cmbPacsCheckType1.IsLike = true;
            this.cmbPacsCheckType1.IsListOnly = false;
            this.cmbPacsCheckType1.IsPopForm = true;
            this.cmbPacsCheckType1.IsShowCustomerList = false;
            this.cmbPacsCheckType1.IsShowID = false;
            this.cmbPacsCheckType1.Location = new System.Drawing.Point(491, 581);
            this.cmbPacsCheckType1.Name = "cmbPacsCheckType1";
            this.cmbPacsCheckType1.PopForm = null;
            this.cmbPacsCheckType1.ShowCustomerList = false;
            this.cmbPacsCheckType1.ShowID = false;
            this.cmbPacsCheckType1.Size = new System.Drawing.Size(170, 20);
            this.cmbPacsCheckType1.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbPacsCheckType1.TabIndex = 85;
            this.cmbPacsCheckType1.Tag = "";
            this.cmbPacsCheckType1.ToolBarUse = false;
            // 
            // cmbPacsItem2
            // 
            this.cmbPacsItem2.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbPacsItem2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPacsItem2.IsEnter2Tab = false;
            this.cmbPacsItem2.IsFlat = false;
            this.cmbPacsItem2.IsLike = true;
            this.cmbPacsItem2.IsListOnly = false;
            this.cmbPacsItem2.IsPopForm = true;
            this.cmbPacsItem2.IsShowCustomerList = false;
            this.cmbPacsItem2.IsShowID = false;
            this.cmbPacsItem2.Location = new System.Drawing.Point(233, 606);
            this.cmbPacsItem2.Name = "cmbPacsItem2";
            this.cmbPacsItem2.PopForm = null;
            this.cmbPacsItem2.ShowCustomerList = false;
            this.cmbPacsItem2.ShowID = false;
            this.cmbPacsItem2.Size = new System.Drawing.Size(251, 20);
            this.cmbPacsItem2.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbPacsItem2.TabIndex = 86;
            this.cmbPacsItem2.Tag = "";
            this.cmbPacsItem2.ToolBarUse = false;
            this.cmbPacsItem2.SelectedIndexChanged += new System.EventHandler(this.cmbPacsItem0_SelectedIndexChanged);
            // 
            // cmbPacsItem1
            // 
            this.cmbPacsItem1.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbPacsItem1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPacsItem1.IsEnter2Tab = false;
            this.cmbPacsItem1.IsFlat = false;
            this.cmbPacsItem1.IsLike = true;
            this.cmbPacsItem1.IsListOnly = false;
            this.cmbPacsItem1.IsPopForm = true;
            this.cmbPacsItem1.IsShowCustomerList = false;
            this.cmbPacsItem1.IsShowID = false;
            this.cmbPacsItem1.Location = new System.Drawing.Point(233, 581);
            this.cmbPacsItem1.Name = "cmbPacsItem1";
            this.cmbPacsItem1.PopForm = null;
            this.cmbPacsItem1.ShowCustomerList = false;
            this.cmbPacsItem1.ShowID = false;
            this.cmbPacsItem1.Size = new System.Drawing.Size(251, 20);
            this.cmbPacsItem1.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbPacsItem1.TabIndex = 84;
            this.cmbPacsItem1.Tag = "";
            this.cmbPacsItem1.ToolBarUse = false;
            this.cmbPacsItem1.SelectedIndexChanged += new System.EventHandler(this.cmbPacsItem0_SelectedIndexChanged);
            // 
            // cmbPacsCheckType0
            // 
            this.cmbPacsCheckType0.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbPacsCheckType0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPacsCheckType0.IsEnter2Tab = false;
            this.cmbPacsCheckType0.IsFlat = false;
            this.cmbPacsCheckType0.IsLike = true;
            this.cmbPacsCheckType0.IsListOnly = false;
            this.cmbPacsCheckType0.IsPopForm = true;
            this.cmbPacsCheckType0.IsShowCustomerList = false;
            this.cmbPacsCheckType0.IsShowID = false;
            this.cmbPacsCheckType0.Location = new System.Drawing.Point(491, 558);
            this.cmbPacsCheckType0.Name = "cmbPacsCheckType0";
            this.cmbPacsCheckType0.PopForm = null;
            this.cmbPacsCheckType0.ShowCustomerList = false;
            this.cmbPacsCheckType0.ShowID = false;
            this.cmbPacsCheckType0.Size = new System.Drawing.Size(170, 20);
            this.cmbPacsCheckType0.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbPacsCheckType0.TabIndex = 83;
            this.cmbPacsCheckType0.Tag = "";
            this.cmbPacsCheckType0.ToolBarUse = false;
            // 
            // cmbPacsItem0
            // 
            this.cmbPacsItem0.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbPacsItem0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPacsItem0.IsEnter2Tab = false;
            this.cmbPacsItem0.IsFlat = false;
            this.cmbPacsItem0.IsLike = true;
            this.cmbPacsItem0.IsListOnly = false;
            this.cmbPacsItem0.IsPopForm = true;
            this.cmbPacsItem0.IsShowCustomerList = false;
            this.cmbPacsItem0.IsShowID = false;
            this.cmbPacsItem0.Location = new System.Drawing.Point(233, 558);
            this.cmbPacsItem0.Name = "cmbPacsItem0";
            this.cmbPacsItem0.PopForm = null;
            this.cmbPacsItem0.ShowCustomerList = false;
            this.cmbPacsItem0.ShowID = false;
            this.cmbPacsItem0.Size = new System.Drawing.Size(251, 20);
            this.cmbPacsItem0.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbPacsItem0.TabIndex = 82;
            this.cmbPacsItem0.Tag = "";
            this.cmbPacsItem0.ToolBarUse = false;
            this.cmbPacsItem0.SelectedIndexChanged += new System.EventHandler(this.cmbPacsItem0_SelectedIndexChanged);
            // 
            // label30
            // 
            this.label30.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label30.ForeColor = System.Drawing.Color.Blue;
            this.label30.Location = new System.Drawing.Point(232, 536);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(107, 16);
            this.label30.TabIndex = 81;
            this.label30.Text = "ҽ�������Ŀ";
            // 
            // label29
            // 
            this.label29.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label29.ForeColor = System.Drawing.Color.Blue;
            this.label29.Location = new System.Drawing.Point(492, 536);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(113, 16);
            this.label29.TabIndex = 80;
            this.label29.Text = "ҽ����鷽��";
            // 
            // lblExeDept
            // 
            this.lblExeDept.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblExeDept.ForeColor = System.Drawing.Color.Blue;
            this.lblExeDept.Location = new System.Drawing.Point(128, 607);
            this.lblExeDept.Name = "lblExeDept";
            this.lblExeDept.Size = new System.Drawing.Size(76, 21);
            this.lblExeDept.TabIndex = 78;
            // 
            // label28
            // 
            this.label28.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label28.ForeColor = System.Drawing.Color.Blue;
            this.label28.Location = new System.Drawing.Point(128, 586);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(78, 16);
            this.label28.TabIndex = 77;
            this.label28.Text = "ִ�п���:";
            // 
            // lblNurseStation
            // 
            this.lblNurseStation.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNurseStation.Location = new System.Drawing.Point(554, 159);
            this.lblNurseStation.Name = "lblNurseStation";
            this.lblNurseStation.Size = new System.Drawing.Size(106, 16);
            this.lblNurseStation.TabIndex = 13;
            // 
            // lblBed
            // 
            this.lblBed.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBed.Location = new System.Drawing.Point(425, 136);
            this.lblBed.Name = "lblBed";
            this.lblBed.Size = new System.Drawing.Size(100, 17);
            this.lblBed.TabIndex = 76;
            this.lblBed.Text = "����:";
            // 
            // panelSpecail
            // 
            this.panelSpecail.Controls.Add(this.chkJJ);
            this.panelSpecail.Controls.Add(this.dtJJ);
            this.panelSpecail.Controls.Add(this.label26);
            this.panelSpecail.Controls.Add(this.dtSample);
            this.panelSpecail.Controls.Add(this.label27);
            this.panelSpecail.Location = new System.Drawing.Point(33, 750);
            this.panelSpecail.Name = "panelSpecail";
            this.panelSpecail.Size = new System.Drawing.Size(533, 27);
            this.panelSpecail.TabIndex = 75;
            this.panelSpecail.Visible = false;
            // 
            // chkJJ
            // 
            this.chkJJ.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.chkJJ.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkJJ.Location = new System.Drawing.Point(463, 4);
            this.chkJJ.Name = "chkJJ";
            this.chkJJ.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkJJ.Size = new System.Drawing.Size(60, 21);
            this.chkJJ.TabIndex = 77;
            this.chkJJ.Text = "�� ��";
            this.chkJJ.Visible = false;
            // 
            // dtJJ
            // 
            this.dtJJ.Location = new System.Drawing.Point(343, 4);
            this.dtJJ.Name = "dtJJ";
            this.dtJJ.Size = new System.Drawing.Size(108, 21);
            this.dtJJ.TabIndex = 75;
            this.dtJJ.Visible = false;
            // 
            // label26
            // 
            this.label26.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label26.Location = new System.Drawing.Point(242, 4);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(107, 17);
            this.label26.TabIndex = 76;
            this.label26.Text = "ĩ�ξ������ڣ�";
            this.label26.Visible = false;
            // 
            // dtSample
            // 
            this.dtSample.Location = new System.Drawing.Point(114, 4);
            this.dtSample.Name = "dtSample";
            this.dtSample.Size = new System.Drawing.Size(107, 21);
            this.dtSample.TabIndex = 73;
            this.dtSample.Visible = false;
            // 
            // label27
            // 
            this.label27.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label27.Location = new System.Drawing.Point(6, 5);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(107, 17);
            this.label27.TabIndex = 74;
            this.label27.Text = "�����ɼ����ڣ�";
            this.label27.Visible = false;
            // 
            // label24
            // 
            this.label24.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label24.Location = new System.Drawing.Point(481, 636);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(80, 14);
            this.label24.TabIndex = 68;
            this.label24.Text = "����ҽʦ��";
            // 
            // label23
            // 
            this.label23.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label23.Location = new System.Drawing.Point(442, 808);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(78, 13);
            this.label23.TabIndex = 67;
            this.label23.Text = "���뵥�ţ�";
            this.label23.Visible = false;
            // 
            // lblAge
            // 
            this.lblAge.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAge.Location = new System.Drawing.Point(445, 159);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(39, 16);
            this.lblAge.TabIndex = 12;
            // 
            // label21
            // 
            this.label21.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label21.Location = new System.Drawing.Point(399, 159);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(56, 16);
            this.label21.TabIndex = 65;
            this.label21.Text = "���䣺";
            // 
            // lblSex
            // 
            this.lblSex.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSex.Location = new System.Drawing.Point(309, 159);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(37, 16);
            this.lblSex.TabIndex = 15;
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label20.Location = new System.Drawing.Point(265, 159);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(50, 16);
            this.label20.TabIndex = 64;
            this.label20.Text = "�Ա�";
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label19.Location = new System.Drawing.Point(127, 159);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(49, 16);
            this.label19.TabIndex = 63;
            this.label19.Text = "������";
            // 
            // lblDate
            // 
            this.lblDate.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDate.Location = new System.Drawing.Point(205, 113);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(86, 14);
            this.lblDate.TabIndex = 8;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(127, 114);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 14);
            this.label12.TabIndex = 60;
            this.label12.Text = "�������ڣ�";
            // 
            // cmbMachine
            // 
            this.cmbMachine.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbMachine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMachine.IsEnter2Tab = false;
            this.cmbMachine.IsFlat = false;
            this.cmbMachine.IsLike = true;
            this.cmbMachine.IsListOnly = false;
            this.cmbMachine.IsPopForm = true;
            this.cmbMachine.IsShowCustomerList = false;
            this.cmbMachine.IsShowID = false;
            this.cmbMachine.Location = new System.Drawing.Point(126, 558);
            this.cmbMachine.Name = "cmbMachine";
            this.cmbMachine.PopForm = null;
            this.cmbMachine.ShowCustomerList = false;
            this.cmbMachine.ShowID = false;
            this.cmbMachine.Size = new System.Drawing.Size(104, 20);
            this.cmbMachine.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbMachine.TabIndex = 59;
            this.cmbMachine.Tag = "";
            this.cmbMachine.ToolBarUse = false;
            this.cmbMachine.SelectedIndexChanged += new System.EventHandler(this.cmbMachine_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(100, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 20);
            this.label4.TabIndex = 55;
            this.label4.Text = "��  ��";
            this.label4.Visible = false;
            // 
            // txtAttention
            // 
            this.txtAttention.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAttention.Location = new System.Drawing.Point(233, 678);
            this.txtAttention.Name = "txtAttention";
            this.txtAttention.Size = new System.Drawing.Size(432, 43);
            this.txtAttention.TabIndex = 54;
            this.txtAttention.Text = "";
            // 
            // txtMemo
            // 
            this.txtMemo.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMemo.Location = new System.Drawing.Point(232, 503);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(432, 21);
            this.txtMemo.TabIndex = 53;
            this.txtMemo.Text = "";
            // 
            // txt1
            // 
            this.txt1.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt1.Location = new System.Drawing.Point(232, 185);
            this.txt1.Name = "txt1";
            this.txt1.Size = new System.Drawing.Size(432, 144);
            this.txt1.TabIndex = 51;
            this.txt1.Text = "";
            // 
            // txtDiagnose
            // 
            this.txtDiagnose.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDiagnose.Location = new System.Drawing.Point(232, 335);
            this.txtDiagnose.Name = "txtDiagnose";
            this.txtDiagnose.Size = new System.Drawing.Size(432, 52);
            this.txtDiagnose.TabIndex = 50;
            this.txtDiagnose.Text = "";
            // 
            // txtItems
            // 
            this.txtItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtItems.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtItems.Location = new System.Drawing.Point(232, 400);
            this.txtItems.Name = "txtItems";
            this.txtItems.ReadOnly = true;
            this.txtItems.Size = new System.Drawing.Size(432, 91);
            this.txtItems.TabIndex = 49;
            this.txtItems.Text = "";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(333, 76);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(113, 26);
            this.label8.TabIndex = 42;
            this.label8.Text = "������뵥";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPacsBillID
            // 
            this.lblPacsBillID.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPacsBillID.Location = new System.Drawing.Point(518, 808);
            this.lblPacsBillID.Name = "lblPacsBillID";
            this.lblPacsBillID.Size = new System.Drawing.Size(71, 13);
            this.lblPacsBillID.TabIndex = 36;
            this.lblPacsBillID.Visible = false;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(127, 506);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 14);
            this.label6.TabIndex = 34;
            this.label6.Text = "���Ŀ�ģ�";
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.Black;
            this.panel9.Location = new System.Drawing.Point(126, 663);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(550, 1);
            this.panel9.TabIndex = 33;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(469, 733);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(204, 18);
            this.label1.TabIndex = 26;
            this.label1.Text = "�������ĵ绰��39195566,39131330";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(129, 678);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 18);
            this.label2.TabIndex = 26;
            this.label2.Text = "ע�����";
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(127, 338);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(73, 17);
            this.label13.TabIndex = 17;
            this.label13.Text = "��   �ϣ�";
            // 
            // lblPaykind
            // 
            this.lblPaykind.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPaykind.Location = new System.Drawing.Point(515, 113);
            this.lblPaykind.Name = "lblPaykind";
            this.lblPaykind.Size = new System.Drawing.Size(89, 14);
            this.lblPaykind.TabIndex = 16;
            // 
            // lblName
            // 
            this.lblName.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblName.Location = new System.Drawing.Point(187, 160);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(76, 16);
            this.lblName.TabIndex = 11;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Black;
            this.panel4.Location = new System.Drawing.Point(126, 178);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(550, 1);
            this.panel4.TabIndex = 9;
            // 
            // lblClincNo
            // 
            this.lblClincNo.Location = new System.Drawing.Point(285, 725);
            this.lblClincNo.Name = "lblClincNo";
            this.lblClincNo.Size = new System.Drawing.Size(150, 12);
            this.lblClincNo.TabIndex = 7;
            this.lblClincNo.Text = "����ţ�";
            this.lblClincNo.Visible = false;
            // 
            // lblPatientNo
            // 
            this.lblPatientNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientNo.Location = new System.Drawing.Point(564, 41);
            this.lblPatientNo.Name = "lblPatientNo";
            this.lblPatientNo.Size = new System.Drawing.Size(113, 28);
            this.lblPatientNo.TabIndex = 6;
            this.lblPatientNo.Visible = false;
            // 
            // lblNo
            // 
            this.lblNo.Location = new System.Drawing.Point(308, 738);
            this.lblNo.Name = "lblNo";
            this.lblNo.Size = new System.Drawing.Size(176, 16);
            this.lblNo.TabIndex = 5;
            this.lblNo.Text = "��飨���飩�ţ�";
            this.lblNo.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(126, 69);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(550, 1);
            this.panel2.TabIndex = 3;
            // 
            // lblDoc
            // 
            this.lblDoc.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDoc.Location = new System.Drawing.Point(563, 636);
            this.lblDoc.Name = "lblDoc";
            this.lblDoc.Size = new System.Drawing.Size(98, 14);
            this.lblDoc.TabIndex = 20;
            // 
            // lblApplyName
            // 
            this.lblApplyName.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblApplyName.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblApplyName.Location = new System.Drawing.Point(65, 699);
            this.lblApplyName.Name = "lblApplyName";
            this.lblApplyName.Size = new System.Drawing.Size(77, 26);
            this.lblApplyName.TabIndex = 4;
            this.lblApplyName.Text = "��Ŀ����";
            this.lblApplyName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblApplyName.Visible = false;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(127, 189);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(93, 20);
            this.label14.TabIndex = 18;
            this.label14.Text = "��ʷ��������";
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.Location = new System.Drawing.Point(127, 407);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(109, 16);
            this.label15.TabIndex = 19;
            this.label15.Text = "�����Ŀ��";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Black;
            this.panel6.Location = new System.Drawing.Point(126, 393);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(550, 1);
            this.panel6.TabIndex = 37;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.ForeColor = System.Drawing.Color.Blue;
            this.label10.Location = new System.Drawing.Point(127, 535);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(81, 16);
            this.label10.TabIndex = 58;
            this.label10.Text = "�豸����";
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.White;
            this.label16.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.Location = new System.Drawing.Point(126, 136);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(64, 14);
            this.label16.TabIndex = 61;
            this.label16.Text = "סԺ�ţ�";
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label18.Location = new System.Drawing.Point(448, 114);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(77, 14);
            this.label18.TabIndex = 62;
            this.label18.Text = "�������";
            // 
            // label22
            // 
            this.label22.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label22.Location = new System.Drawing.Point(514, 158);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(50, 16);
            this.label22.TabIndex = 66;
            this.label22.Text = "���ң�";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.groupBox1);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(180, 828);
            this.panel7.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Controls.Add(this.treeView1);
            this.groupBox1.Controls.Add(this.ucUserText2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(180, 828);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tbCardNo);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Location = new System.Drawing.Point(-3, 7);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(172, 32);
            this.panel3.TabIndex = 1;
            // 
            // tbCardNo
            // 
            this.tbCardNo.Location = new System.Drawing.Point(43, 6);
            this.tbCardNo.Name = "tbCardNo";
            this.tbCardNo.Size = new System.Drawing.Size(123, 21);
            this.tbCardNo.TabIndex = 1;
            this.tbCardNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbCardNo_KeyDown);
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(8, 10);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 15);
            this.label11.TabIndex = 0;
            this.label11.Text = "����:";
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(1, 39);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(175, 686);
            this.treeView1.TabIndex = 1;
            this.treeView1.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
            // 
            // ucUserText2
            // 
            this.ucUserText2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucUserText2.GroupInfo = null;
            this.ucUserText2.Location = new System.Drawing.Point(3, 17);
            this.ucUserText2.Name = "ucUserText2";
            this.ucUserText2.Size = new System.Drawing.Size(174, 808);
            this.ucUserText2.TabIndex = 0;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.groupBox2);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(180, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(714, 828);
            this.panel8.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(714, 828);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(180, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 828);
            this.splitter1.TabIndex = 6;
            this.splitter1.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ucPacsApply
            // 
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel7);
            this.Name = "ucPacsApply";
            this.Size = new System.Drawing.Size(894, 828);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.npbBarCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbLogo)).EndInit();
            this.panelSpecail.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
		# region �Զ���ؼ�����
		
		private FS.HISFC.Models.Order.PacsBill pacsbill = null;			
		private FS.HISFC.Models.RADT.PatientInfo myPatient = null;
		FS.HISFC.BizLogic.Manager.Person p = new FS.HISFC.BizLogic.Manager.Person();
        FS.HISFC.BizLogic.Fee.Item itemManager = new FS.HISFC.BizLogic.Fee.Item();
        FS.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
        FS.HISFC.BizLogic.Fee.UndrugPackAge ztManager = new FS.HISFC.BizLogic.Fee.UndrugPackAge();
        FS.HISFC.BizLogic.Manager.Constant cnstManager = new FS.HISFC.BizLogic.Manager.Constant();

        FS.HISFC.BizLogic.Order.PacsBill PacsBillManager = new FS.HISFC.BizLogic.Order.PacsBill();
        //FS.HISFC.Management.Order.PacsCompare compare = new FS.HISFC.Management.Order.PacsCompare();
        FS.HISFC.BizProcess.Integrate.Common.ControlParam myCtrl = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        FS.HISFC.BizLogic.RADT.InPatient myInpatient = new FS.HISFC.BizLogic.RADT.InPatient();
        //FS.HISFC.Management.EPR.EMR myEmr = new FS.HISFC.Management.EPR.EMR();
        FS.HISFC.BizLogic.Order.Order orderMgr = new FS.HISFC.BizLogic.Order.Order();

        FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        //FS.neuFC.Interface.BaseVar var = null;
		private ArrayList alitems;//������Ŀ
		private ArrayList al;//���
		string itemCode;//��Ŀ����
		decimal tot_cost = 0m;//�ܼ�Ǯ
		DataSet dsPacsItems = new DataSet();
		Hashtable hsPatient = new Hashtable();
		DateTime dtBegin;
		DateTime  dtend;
		string billNo;
		System.Data.DataSet dsEMRNodes = new DataSet();
		
		# endregion
		private System.ComponentModel.IContainer components;
		
		/// <summary>
		/// Ĭ�ϵĹ��캯��
		/// </summary>
		public ucPacsApply()
		{
			// �õ����� Windows.Forms ���������������ġ�
			InitializeComponent();
			this.Load+=new EventHandler(ucPacsApply_Load);
			// TODO: �� InitializeComponent ���ú�����κγ�ʼ��

		}
		
		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="item">��Ŀ��Ϣ</param>
		/// <param name="patientInfo">������Ϣ</param>
		public ucPacsApply(ArrayList Items,FS.HISFC.Models.RADT.PatientInfo patientInfo) 
		{
			// �õ����� Windows.Forms ���������������ġ�
			InitializeComponent();
			this.Load+=new EventHandler(ucPacsApply_Load);
			this.PatientInfo = patientInfo;
			this.alItems = Items;
		}
		
		/// <summary> 
		/// ������������ʹ�õ���Դ��
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
		/// <summary>
		/// ����
		/// </summary>
		public FS.HISFC.Models.RADT.PatientInfo PatientInfo
		{
		
			get
			{
				return this.myPatient;
			}
			set
			{
				if(value ==null) return;
				this.myPatient = value;
                this.myPatient.Diagnoses = this.diagManager.QueryMainDiagnose(value.ID, true, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
				this.al = this.myPatient.Diagnoses ;
				this.lblAge.Text += this.myPatient.Age;								//����
				try
				{
					this.lblBed.Text +=this.myPatient.PVisit.PatientLocation.Bed.ID.Substring(4);			//����
				}
				catch
				{
					this.lblBed.Text +=this.myPatient.PVisit.PatientLocation.Bed.ID;			//����
				}
                this.lblClincNo.Text += this.myPatient.PID.CardNO;					//�����
				this.lblName.Text +=this.myPatient.Name;									//����
				//this.lblNo.Text +=this.myPatient.ID;
				this.lblNurseStation.Text += this.myPatient.PVisit.PatientLocation.Dept.Name;	//����
				this.lblPatientNo.Text +="*" + this.myPatient.PID.PatientNO + "*";						//סԺ��
			   this.label35.Text += this.myPatient.PID.PatientNO; //סԺ�� 20111201 by lou
				this.lblPaykind.Text += this.myPatient.Pact.Name;						//�������
				this.lblSex.Text += this.myPatient.Sex.Name;						//�Ա�	
                this.lblbq.Text += this.myPatient.PVisit.PatientLocation.NurseCell.Name;

                this.npbBarCode.Image = this.CreateBarCode(this.myPatient.PID.PatientNO);

				if(this.al != null && this.al.Count > 0)
				{
                    foreach (FS.HISFC.Models.HealthRecord.Diagnose d in al)
					{
						this.txtDiagnose.Text = d.DiagInfo.ICD10.Name + "\n";
					}
				}
				//����...
				//������Ŀ��
				//this.txtItems.Text += this.myPatient.User03;
				//ҽ����ע

			}
		}

		/// <summary>
		/// ��ǰ������Ϣ
		/// </summary>
        //public FS.neuFC.Interface.BaseVar Var 
        //{
        //    set
        //    {
        //        this.var = value;
        //        if(this.var != null)
        //        {
        //            InitPacsInfo();
        //        }
        //    }
        //}


        /// <summary>
        /// ����ҽ��
        /// </summary>
        protected FS.HISFC.Models.Base.Employee reciptDoct = null;

        public FS.HISFC.Models.Base.Employee GetReciptDoct()
        {
            try
            {
                if (this.reciptDoct == null)
                    this.reciptDoct = this.myInpatient.Operator.Clone() as FS.HISFC.Models.Base.Employee;

            }
            catch { }
            return this.reciptDoct;
        }

        /// <summary>
        /// ��ȡҽԺ���ƺ�ҽԺLOGO
        /// </summary>
        private void GetHospLogo()
        {
            string erro = "����";
            string imgpath = FS.FrameWork.WinForms.Classes.Function.CurrentPath + SOC.Local.PacsInterface.GYSY.PacsCompareFunction.GetHospitalLogo("Xml\\HospitalLogoInfo.xml", "Hospital", "Logo", erro);
            picbLogo.Image = Image.FromFile(imgpath);
        }


		/// <summary>
		/// ��Ŀ��Ϣ
		/// </summary>
		public ArrayList alItems
		{
			get
			{
				return this.alitems;	 
			}
			set
			{
				if (value == null) return;
				this.alitems = value;
				ArrayList alMachineType = this.cnstManager.GetAllList("MACHINETYPE");
				this.cmbMachine.SelectedIndexChanged -=new EventHandler(cmbMachine_SelectedIndexChanged);
				this.cmbMachine.AddItems(alMachineType);
				this.cmbMachine.SelectedIndexChanged +=new EventHandler(cmbMachine_SelectedIndexChanged);
                FS.HISFC.Models.Fee.Item.Undrug myItem = null;//����ʵ��
                FS.HISFC.Models.Fee.Item.UndrugComb ztItem = null;//������Ŀʵ��
				FS.HISFC.Models.Order.Order order = value[0] as FS.HISFC.Models.Order.Order;
				this.pacsbill = PacsBillManager.QueryPacsBill(order.Combo.ID.ToString());//��ѯ
				this.lblExeDept.Text = order.ExeDept.Name;
				this.lblbq.Text = myPatient.PVisit.PatientLocation.NurseCell.Name ; //���ڲ���
				string queryItemCode = "";

				for(int i=value.Count -1;i>=0;i--) 
				{
					queryItemCode += (value[i] as FS.HISFC.Models.Order.Order).Item.ID +"','";
				}

				if(queryItemCode.Length > 0)
				{
					queryItemCode = queryItemCode.Remove(queryItemCode.Length - 3,3);
				}

				/* 
				 * ��ʱ����Pacs���ж���
				 * 
				DataSet ds = new DataSet();
				int iReturn = this.compare.GetPacsItemByHisID(queryItemCode,ref ds);
				if(iReturn == -1)
				{
					MessageBox.Show("���ݱ�����PACS��Ŀ����ʧ�ܣ�","��ʾ");
					return;
				}
				if(ds != null)
				{
					DataView dv = new DataView(ds.Tables[0]);

					if(ds.Tables[0].Rows.Count > 0)
					{
						
					}
					
					ArrayList al = new ArrayList();

					Hashtable hs = new Hashtable(); 

					foreach(DataRow row in ds.Tables[0].Rows)
					{
						if(!hs.Contains(row["machinetype"].ToString()))
						{
							FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
							obj.Name = row["machinetype"].ToString();
							al.Add(obj);
						}
					}

					if(ds.Tables[0].Rows.Count == 0)
					{
						this.compare.GetCompareList(ref ds);

						ds.Tables[0].Columns.Remove("hisid");
						ds.Tables[0].Columns.Remove("hisname");
						dv = new DataView(ds.Tables[0]);
		
					}
					else
					{
						this.cmbMachine.ClearItems();
						this.cmbMachine.AddItems(al);
					}
					if(al.Count == 1)
					{
						this.cmbMachine.SelectedIndex = 0;
					}
				}	
				*/

				FS.HISFC.Models.Order.Order tempOrder = value[0] as FS.HISFC.Models.Order.Order;

			//	this.cmbMachine.SelectedIndexChanged -=new EventHandler(cmbMachine_SelectedIndexChanged);
				try
				{
					ArrayList pacscompare=this.cnstManager.GetAllList("PacsItemCompare");
					string tempinputcode=tempOrder.Item.UserCode;
					foreach(FS.HISFC.Models.Base.Const tempconst in pacscompare)
					{
						if(tempinputcode.StartsWith(tempconst.Name))
						{
							this.cmbMachine.Text=tempconst.Memo;
							break;
						}
					}
				}
				catch(Exception ex)
				{}
				//this.cmbMachine.SelectedIndexChanged +=new EventHandler(cmbMachine_SelectedIndexChanged);
				string specialDept = this.myCtrl.GetControlParam<string>("200031");
				if(specialDept == null || specialDept == "" || tempOrder.ExeDept.ID.IndexOf(specialDept) < 0)
				{
					this.panelSpecail.Visible = false;
					this.txtItems.Height = this.txtItems.Height + this.panelSpecail.Height;
				}

				if( pacsbill == null) //�µ�
				{ 
					//��ȡ���鵥��
					if(order.Item.SysClass.ID.ToString()=="UL")
					{
						//��Ч��������
						this.txtItems.Size= new System.Drawing.Size(531,48);
						this.label9.Visible=true;
						this.Antibiotic1.Visible=true;
						this.Antibiotic2.Visible=true;
						this.label25.Visible=true;
						this.Temperature.Visible=true;
						this.label31.Visible=true;
						this.SpecimenType.Visible=true;
						this.exec_sqn.Visible=true;
						this.label32.Visible=true;
                        this.label33.Visible=true;
						this.label34.Visible=true;
						this.bodyPost.Visible=true;
						ArrayList  execSqnAl= new ArrayList();
						execSqnAl=orderMgr.QueryExecOrderByOneOrder(order.ID,"2");
						if(execSqnAl.Count>0)
						{
							foreach(FS.HISFC.Models.Order.ExecOrder execSqnInfo in execSqnAl)
							{
								this.exec_sqn.Text=execSqnInfo.ID;//ִ�е���
								this.SpecimenType.Text=execSqnInfo.Order.Sample.Name;//��������
							}
						}						
					}
					this.txt1.Text = "";//��ʷ������
					this.billNo = order.Combo.ID;
					this.lblPacsBillID.Text += order.Combo.ID.ToString();//���뵥��
					this.txtMemo.Text = "";//��ע
					this.txtAttention.Text = "";//ע������
					if((value[0] as FS.HISFC.Models.Order.Order).IsEmergency)
						this.label4.Visible = true;//�Ӽ�
					this.lblDate.Text+= cnstManager.GetSysDate();	//��������

					this.lblDoc.Text += this.p.Operator;//����ҽʦ
					#region ��Ŀ����
					for(int i=value.Count -1;i>=0;i--) 
					{
						//������Ŀ
						if((value[i] as FS.HISFC.Models.Order.Order).Item.PriceUnit.IndexOf("������")!= -1) 
						{
							//�����Ŀ

                            //ztItem = feeIntegrate.GetItem((value[i] as FS.HISFC.Models.Order.Order).Item.ID);

                            //if(ztItem == null) 
                            //{
                            //    MessageBox.Show("û�л�øø�����Ŀ��Ϣ!","SORRY");    
                            //}
							
							//�õ��۸�
                            decimal price = this.ztManager.GetUndrugCombPrice((value[i] as FS.HISFC.Models.Order.Order).Item.ID);

                            if ((value[i] as FS.HISFC.Models.Order.Order).Item.Name == "0")
                            {
                                //
                            }
                            else
                            {
                                this.txtItems.Text += (value[i] as FS.HISFC.Models.Order.Order).Item.Name + "\n";
                            }
                            
//							this.txtItems.Text  += ztItem.Name + " ���:"+price.ToString()+"\n";
//							this.itemCode += ztItem.ID+"|";
//							this.tot_cost+=this.GetUndrugZtPrice(ztItem.ID)*(value[i] as FS.HISFC.Models.Order.Order).QTY;
                            
							
						}
						else 
						{
                            myItem = (value[i] as FS.HISFC.Models.Order.Order).Item as FS.HISFC.Models.Fee.Item.Undrug;
							if(myItem==null) return;
							myItem = itemManager.GetValidItemByUndrugCode((value[i] as FS.HISFC.Models.Order.Order).Item.ID);
							if(myItem == null) 
							{
								MessageBox.Show("û�л�ø���Ŀ��Ϣ!","SORRY");
							}
							this.txtItems.Text  += myItem.Name=="0"?"":myItem.Name +"\n";
//							this.txtItems.Text  += myItem.Name + " ���:"+myItem.Price.ToString() +"\n";
//							this.itemCode += myItem.ID+"|";
//							this.tot_cost+=myItem.Price*(value[i] as FS.HISFC.Models.Order.Order).QTY;//�ܷ���
							

						}
					}

//					this.txtItems.Text += "���ù���:"+this.tot_cost.ToString();
					#endregion
					if((value[0] as FS.HISFC.Models.Order.Order).Item.PriceUnit.IndexOf("������")!= -1) 
					{
						//��Ŀ����
						this.lblApplyName.Text = ztItem.Mark4;
						//ע������
                        this.txtAttention.Text = ztItem.Mark3 == "0" ? "" : ztItem.Mark3;
						//��ʷ�����
						this.txt1.Text = ztItem.Mark1 == "0" ? "" : ztItem.Mark1;
						//���Ҫ��
						this.txtItems.Text += ztItem.Mark2 == "0" ? "" : ztItem.Mark2;
					}
					else 
					{
						//��Ŀ����
                        this.lblApplyName.Text = myItem.SpecialFlag4;
						//ע������
                        this.txtAttention.Text = myItem.SpecialFlag3 == "0" ? "" : myItem.SpecialFlag3;
						//��ʷ�����
                        this.txt1.Text = myItem.SpecialFlag1 == "0" ? "" : myItem.SpecialFlag1;
						//���Ҫ��
                        this.txtItems.Text += myItem.SpecialFlag2 == "0" ? "" : myItem.SpecialFlag2; 	 
					}
                    string mySlefTall ="" ;
					string myHistory ="" ;
					string mySpecCheck="" ;
					this.txtDiagnose.Text = "" ;
					//dsEMRNodes = this.myEmr.QueryEMRByNode(" index1 = '" + this.myPatient.ID + "'");

					if(dsEMRNodes != null && dsEMRNodes.Tables.Count != 0 && dsEMRNodes.Tables[0].Rows.Count != 0)
					{
						DataTable tableNode = dsEMRNodes.Tables[0];
						foreach(DataRow rowNode in tableNode.Rows)
						{
							if ( rowNode["�ڵ�����"].ToString() =="����") 
							{
								mySlefTall = rowNode["�ڵ�����"].ToString()+ ":  " + rowNode["��ֵ"].ToString().Replace("\n","");	
							}
							if (rowNode["�ڵ�����"].ToString() =="�ֲ�ʷ" )
							{
								myHistory = rowNode["�ڵ�����"].ToString()+ ":  " + rowNode["��ֵ"].ToString().Replace("\n","") ;	
							}
							if(rowNode["�ڵ�����"].ToString() =="ר�Ƽ��" )
							{
								mySpecCheck = rowNode["�ڵ�����"].ToString()+ ":  " + rowNode["��ֵ"].ToString().Replace("\n","") ;
							}
							if(rowNode["�ڵ�����"].ToString() =="�������")
							{
								this.txtDiagnose.Text =rowNode["�ڵ�����"].ToString()+ ":  " + rowNode["��ֵ"].ToString().Replace("\n","") ;
							}
						}
						this.txt1.Text =mySlefTall +"\n" + myHistory +"\n"+ mySpecCheck  ;
						
					}
					
				}
				else 
				{
					//2010-6-1
					if(order.Item.SysClass.ID.ToString()=="UL")
					{
						//��Ч��������
						this.txtItems.Size= new System.Drawing.Size(531,48);
						this.label9.Visible=true;
						this.Antibiotic1.Visible=true;
						this.Antibiotic2.Visible=true;
						this.label25.Visible=true;
						this.Temperature.Visible=true;
						this.label31.Visible=true;
						this.SpecimenType.Visible=true;
						this.exec_sqn.Visible=true;
						this.label32.Visible=true;
						this.label33.Visible=true;
						this.label34.Visible=true;
						this.bodyPost.Visible=true;

						this.reprint.Visible=true;
						this.exec_sqn.Text=pacsbill.Exec_sqn;
						this.Antibiotic1.Text=pacsbill.Antiviotic1;
						this.Antibiotic2.Text=pacsbill.Antiviotic2;
						this.Temperature.Text=pacsbill.Temperature;
						this.SpecimenType.Text=pacsbill.SpecimenType;
					}
					this.billNo = pacsbill.ComboNO;
					this.tot_cost = pacsbill.TotCost;//�ܷ���
					foreach(FS.FrameWork.Models.NeuObject obj in this.cmbMachine.alItems)
					{
						if(obj.Name == pacsbill.MachineType)
							this.cmbMachine.Tag = obj.ID;//�豸����
						    
					}
					this.cmbMachine.Text = pacsbill.MachineType ;

					if(order.Item.SysClass.ID.ToString()=="UL")//������Ŀpacsitem�沿λ
					{
						this.bodyPost.Text=pacsbill.PacsItem;
					}
					else
					{
						this.cmbPacsCheckType0.Items.Clear();
						this.cmbPacsCheckType1.Items.Clear();
						this.cmbPacsCheckType2.Items.Clear();

						this.cmbPacsItem0.Items.Clear();
						this.cmbPacsItem1.Items.Clear();
						this.cmbPacsItem2.Items.Clear();
                    
						this.dsPacsItems = new DataSet();
                        SOC.Local.PacsInterface.GYSY.PacsCompareFunction.GetPacsItemList(ref this.dsPacsItems);
						Hashtable hsItems = new Hashtable();
						ArrayList alPacsItems = new ArrayList();

						foreach(DataRow row in this.dsPacsItems.Tables[0].Rows)
						{
							if(row["MACHINE_TYPE"].ToString() == this.cmbMachine.Text)
							{
								if(!hsItems.Contains(row["ITEM_NAME"].ToString()))
								{
									hsItems.Add(row["ITEM_NAME"].ToString(),null);

									FS.HISFC.Models.Base.Spell obj = new FS.HISFC.Models.Base.Spell();
						
									obj.ID = row["ITEM_CODE"].ToString();
									obj.Name = row["ITEM_NAME"].ToString();
									obj.SpellCode = row["SPELL_CODE"].ToString();

									alPacsItems.Add(obj);
								}
							}
						}
						this.cmbPacsItem0.SelectedIndexChanged -= new System.EventHandler(this.cmbPacsItem0_SelectedIndexChanged);
						this.cmbPacsItem1.SelectedIndexChanged -= new System.EventHandler(this.cmbPacsItem0_SelectedIndexChanged);
						this.cmbPacsItem2.SelectedIndexChanged -= new System.EventHandler(this.cmbPacsItem0_SelectedIndexChanged);
			
						this.cmbPacsItem0.AddItems(alPacsItems);
						this.cmbPacsItem1.AddItems(alPacsItems);;
						this.cmbPacsItem2.AddItems(alPacsItems);

						this.cmbPacsItem0.SelectedIndexChanged += new System.EventHandler(this.cmbPacsItem0_SelectedIndexChanged);
						this.cmbPacsItem1.SelectedIndexChanged += new System.EventHandler(this.cmbPacsItem0_SelectedIndexChanged);
						this.cmbPacsItem2.SelectedIndexChanged += new System.EventHandler(this.cmbPacsItem0_SelectedIndexChanged);
                    
						string []  myPacsBill=pacsbill.PacsItem.Split('|');
						int i = myPacsBill.Length;
						if (i==2)
						{
							this.cmbPacsItem0.Text=myPacsBill[0] ;
							this.cmbPacsCheckType0.Text=myPacsBill[1] ;
						}
						if(i==4)
						{
							this.cmbPacsItem0.Text=myPacsBill[0] ;
							this.cmbPacsCheckType0.Text=myPacsBill[1] ;
							this.cmbPacsItem1.Text=myPacsBill[2] ;
							this.cmbPacsCheckType1.Text=myPacsBill[3] ;
						}
						if (i == 6) 
						{
							this.cmbPacsItem0.Text=myPacsBill[0] ;
							this.cmbPacsCheckType0.Text=myPacsBill[1] ;
							this.cmbPacsItem1.Text=myPacsBill[2] ;
							this.cmbPacsCheckType1.Text=myPacsBill[3] ;
							this.cmbPacsItem2.Text=myPacsBill[4] ;
							this.cmbPacsCheckType2.Text=myPacsBill[5] ;
						}
						if(i == 8) 
						{
							this.cmbPacsItem0.Text=myPacsBill[0] ;
							this.cmbPacsCheckType0.Text=myPacsBill[1] ;
							this.cmbPacsItem1.Text=myPacsBill[2] ;
							this.cmbPacsCheckType1.Text=myPacsBill[3] ;
							this.cmbPacsItem2.Text=myPacsBill[4] ;
							this.cmbPacsCheckType2.Text=myPacsBill[5] ;
						}
						if(i==10)
						{
							this.cmbPacsItem0.Text=myPacsBill[0] ;
							this.cmbPacsCheckType0.Text=myPacsBill[1] ;
							this.cmbPacsItem1.Text=myPacsBill[2] ;
							this.cmbPacsCheckType1.Text=myPacsBill[3] ;
							this.cmbPacsItem2.Text=myPacsBill[4] ;
							this.cmbPacsCheckType2.Text=myPacsBill[5] ;
						}

					
						foreach(FS.FrameWork.Models.NeuObject obj in this.cmbPacsItem0.alItems)
						{
							if(obj.Name == this.cmbPacsItem0.Text)
								this.cmbPacsItem0.Tag= obj.ID;
						}  
					}
					this.txtMemo.Text = pacsbill.Memo;//��ע
					if(pacsbill.Caution != "")//ע������
					{
						if(pacsbill.Caution.IndexOf("True") != -1) 
						{
                            this.txtAttention.Text = pacsbill.Caution.Substring(4) == "0" ? "" : pacsbill.Caution.Substring(4);
							this.label4.Visible = true;
						}
						else
                            this.txtAttention.Text = pacsbill.Caution.Substring(5) == "0" ? "" : pacsbill.Caution.Substring(5);
					}
					this.txtDiagnose.Text = "";
					if(pacsbill.Diagnose1!=null&&pacsbill.Diagnose1!="")//���1
					{
						this.txtDiagnose.Text += "���1��"+pacsbill.Diagnose1+"\n";;
					}

					if(pacsbill.Diagnose2!=null&&pacsbill.Diagnose2!="")//���2
					{
						this.txtDiagnose.Text += "���2��"+pacsbill.Diagnose2+"\n";;
					}
					
					if(pacsbill.Diagnose3!=null&&pacsbill.Diagnose3!="")//���3
					{
						this.txtDiagnose.Text += "���3��"+pacsbill.Diagnose3;
					}
					this.lblDate.Text = pacsbill.ApplyDate;//��������
					this.txtItems.Text = pacsbill.CheckOrder.Substring(0,pacsbill.CheckOrder.Length - 1);//�����Ŀ
					
					
					this.txt1.Text = pacsbill.IllHistory == "0" ? "" : pacsbill.IllHistory;//��ʷ
					this.itemCode = pacsbill.ItemCode;//��Ŀ����
					this.lblPacsBillID.Text = pacsbill.ComboNO;//���뵥��
					this.lblApplyName.Text = pacsbill.BillName;//���뵥����
					this.lblDoc.Text = pacsbill.Doctor.ID;//ҽ������
					this.dtSample.Value = FS.FrameWork.Function.NConvert.ToDateTime(pacsbill.SampleDate);
                    //this.dtJJ.Value = FS.FrameWork.Function.NConvert.ToDateTime(pacsbill.YJDate);
                    //this.chkJJ.Checked = pacsbill.JJ;
				}

			}
		}
		/// <summary>
		/// ��ǰ���뵥
		/// </summary>
		public FS.HISFC.Models.Order.PacsBill PacsBill
		{
			set
			{
				this.pacsbill = value;
				this.SetPacsBill();
			}
		}
		/// <summary>
		/// ��ʼ����
		/// </summary>
		public DateTime DtBegin
		{
			get
			{
				return this.dtBegin;
			}
			set
			{
				this.dtBegin = value;
			}
		}
		/// <summary>
		/// ��ֹ����
		/// </summary>
		public DateTime DtEnd
		{
			get
			{
				return this.dtend;
			}
			set
			{
				this.dtend = value;
			}
		}

		/// <summary>
		/// ��ʾ���뵥
		/// </summary>
		private void SetPacsBill()
		{
			this.txtDiagnose.Text = "";
			this.billNo = pacsbill.ComboNO;
			this.tot_cost = pacsbill.TotCost;//�ܷ���
		
			this.cmbMachine.Text = pacsbill.MachineType;//�豸����
			this.txtMemo.Text = pacsbill.Memo;//��ע
			if(pacsbill.Caution != "")//ע������
			{
				if(pacsbill.Caution.IndexOf("True") != -1) 
				{
					this.txtAttention.Text = pacsbill.Caution.Substring(4) == "0" ? "" : pacsbill.Caution.Substring(4);
					this.label4.Visible = true;
				}
				else
                    this.txtAttention.Text = pacsbill.Caution.Substring(5) == "0" ? "" : pacsbill.Caution.Substring(5);
			}
			if(pacsbill.Diagnose1!=null&&pacsbill.Diagnose1!="")//���1
			{
				this.txtDiagnose.Text += "���1��"+pacsbill.Diagnose1+"\n";
			}

			if(pacsbill.Diagnose2!=null&&pacsbill.Diagnose2!="")//���2
			{
				this.txtDiagnose.Text += "���2��"+pacsbill.Diagnose2+"\n";
			}
					
			if(pacsbill.Diagnose3!=null&&pacsbill.Diagnose3!="")//���3
			{
				this.txtDiagnose.Text += "���3��"+pacsbill.Diagnose3;
			}
            this.lblDate.Text = pacsbill.ApplyDate;//��������
			this.txtItems.Text = pacsbill.CheckOrder.Substring(0,pacsbill.CheckOrder.Length - 1);//�����Ŀ
			this.txt1.Text = pacsbill.IllHistory == "0" ? "" : pacsbill.IllHistory;//��ʷ
			this.itemCode = pacsbill.ItemCode;//��Ŀ����
			this.lblPacsBillID.Text = pacsbill.ComboNO;//���뵥��
			this.lblApplyName.Text = pacsbill.BillName;//���뵥����
            this.lblDoc.Text = pacsbill.Doctor.Name;;//ҽ������



            this.dtSample.Value = FS.FrameWork.Function.NConvert.ToDateTime(pacsbill.SampleDate);
            //this.dtJJ.Value = FS.FrameWork.Function.NConvert.ToDateTime(pacsbill.YJDate);
            //this.chkJJ.Checked = pacsbill.JJ;
			this.exec_sqn.Text=pacsbill.Exec_sqn;//ִ�е���ˮ��
			this.Antibiotic1.Text=pacsbill.Antiviotic1;//������1
			this.Antibiotic2.Text=pacsbill.Antiviotic2;//������2
			this.Temperature.Text=pacsbill.Temperature;//����
			this.SpecimenType.Text=pacsbill.SpecimenType;//��������
		}
		/// <summary>
		/// �������
		/// </summary>
		/// <param name="dtBirthday"></param>
		/// <returns></returns>
		private string GetAge(DateTime dtBirthday)
		{
            FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();

			DateTime systime = orderManager.GetDateTimeFromSysDateTime();
			orderManager = null;
			TimeSpan span = new TimeSpan(systime.Ticks - dtBirthday.Ticks);
			string   strAge = "";
			if(span.Days/365<=0)
			{
				if(span.Days/30<=0)
				{
					strAge = span.Days.ToString()+"��"; 
				}
				else
				{
					strAge = System.Convert.ToString(span.Days/30)+"��";
				}
			}
			else
			{
				strAge = System.Convert.ToString(span.Days/365)+"��";
				if(span.Days/365<5)
				{
					int diff = span.Days - (span.Days/365)*365;
					if(diff>30)
					{
						strAge = strAge + System.Convert.ToString(span.Days/30)+"��";
					}
					else
					{
						strAge = strAge + System.Convert.ToString(diff) + "��";
					}
				}
			}
			return strAge;
		}
		/// <summary>
		/// ����
		/// </summary>
		/// <returns></returns>
		public void Save()
		{

			int i = 0;
			if(this.pacsbill!= null&&this.pacsbill.IsValid)
			{
				MessageBox.Show("��������Ϣ�Ѿ����ϣ������ٱ���","��ʾ");
				return;
			}
			try
			{
				if(this.FindForm().Tag != null && this.FindForm().Tag.ToString() == "1")
				{
					this.pacsbill.Oper.ID = this.PacsBillManager.Operator.ID;
				}
				else
				{
					this.pacsbill = this.GetPacsBillInfo();
				}
				if(this.CheckPacsBill(this.pacsbill) == -1)
				{
					return;
				}
				i = this.PacsBillManager.SetPacsBill(pacsbill);
			}
			catch
			{
				i = -1;
			}
			if(i == -1)
				MessageBox.Show("��鵥����ʧ�ܣ�","��ʾ");
			else
				MessageBox.Show("��鵥����ɹ���","��ʾ");
		}
		/// <summary>
		/// ������뵥
		/// </summary>
		/// <param name="bill"></param>
		/// <returns></returns>
		private int CheckPacsBill(FS.HISFC.Models.Order.PacsBill bill)
		{
			if(bill == null)
				return -1;
            //if(bill.MachineType == "" && bill.SpecimenType.ToString().Trim()=="")
            //{
            //    MessageBox.Show("��ѡ���豸����");
            //    this.cmbMachine.Focus();
            //    return -1;
            //}
            //if(bill.Exec_sqn.ToString()!=""&&bill.Antiviotic1.ToString()=="")
            //{
            //    MessageBox.Show("��ѡ������1");
            //    this.Antibiotic1.Focus();
            //    return -1;
            //}
            //if(bill.Exec_sqn.ToString()!=""&&bill.Temperature.ToString()=="")
            //{
            //    MessageBox.Show("����������");
            //    this.Temperature.Focus();
            //    return -1;
            //}
            //if(bill.Exec_sqn.ToString()!=""&&bill.SpecimenType.ToString()=="")
            //{
            //    MessageBox.Show("��ѡ����������");
            //    this.SpecimenType.Focus();
            //    return -1;
            //}
			if(bill.IllHistory.Length>499)
			{
				MessageBox.Show("��ʷ����������500�֣����޸ģ�");
				this.txt1.Focus();
				return -1;
			}
			if(bill.IllHistory.IndexOf("'")>0)
			{
				MessageBox.Show("��ʷ���������ڡ������š����ܱ��棬���޸ģ�");
				this.txt1.Focus();
				return -1;
			}
			return 1;
		}
		/// <summary>
		/// ����ǰ��ü�鵥ʵ����Ϣ
		/// </summary>
		private FS.HISFC.Models.Order.PacsBill GetPacsBillInfo() 
		{

            FS.HISFC.Models.Order.PacsBill p = new FS.HISFC.Models.Order.PacsBill();
            FS.HISFC.Models.Base.Employee person = this.PacsBillManager.Operator as FS.HISFC.Models.Base.Employee;
			string billName = "";
			p.ClinicCode = this.myPatient.ID;
            p.PatientType = FS.HISFC.Models.Order.PatientType.InPatient;//�������
			if(this.pacsbill==null)
			{
                p.Doctor.ID = person.ID;//����ҽʦ����
                p.Doctor.Name = person.Name;//����ҽʦ����
			}
			else
			{
                p.Doctor.ID = this.pacsbill.Doctor.ID;
                p.Doctor.Name = this.pacsbill.Doctor.Name;
			}
			p.ComboNO = this.lblPacsBillID.Text.Trim();//���뵥��
			p.PatientNO = this.myPatient.PID.CardNO;//���߿���
            p.Dept.ID = person.Dept.ID;//�������
            p.Dept.Name = person.Dept.Name;//��������
			billName =  this.lblApplyName.Text;//���뵥����
//			if(this.itemCode.IndexOf("|") >0)
//			{
//				this.itemCode = itemCode.Substring(0,this.itemCode.Length-1);
//			}
			p.ItemCode = this.itemCode;//��Ŀ����
			if(billName == "")
				p.BillName = "������뵥";
			else
				p.BillName = billName;
			p.TotCost = this.tot_cost;//��Ŀ�ܷ���
			if(this.myPatient.ID.Substring(0,2)=="ZY")
			{
				p.Diagnose1 = this.txtDiagnose.Text ;// ���
			}
			else
			{
				if(this.al !=null)
				{
					if(this.al.Count==1)
					{
                        p.Diagnose1 = (al[0] as FS.HISFC.Models.HealthRecord.Diagnose).DiagInfo.ICD10.Name;
					}
					else if(al.Count==2)
					{
                        p.Diagnose1 = (al[0] as FS.HISFC.Models.HealthRecord.Diagnose).DiagInfo.ICD10.Name;
                        p.Diagnose2 = (al[1] as FS.HISFC.Models.HealthRecord.Diagnose).DiagInfo.ICD10.Name;
					}
					else if(al.Count==3)
					{
                        p.Diagnose1 = (al[0] as FS.HISFC.Models.HealthRecord.Diagnose).DiagInfo.ICD10.Name;//���1
                        p.Diagnose2 = (al[1] as FS.HISFC.Models.HealthRecord.Diagnose).DiagInfo.ICD10.Name;//���2
                        p.Diagnose3 = (al[2] as FS.HISFC.Models.HealthRecord.Diagnose).DiagInfo.ICD10.Name;//���3
					}
				}
			}
			p.MachineType = this.cmbMachine.Text;//�豸����
			
			p.Memo = this.txtMemo.Text.Trim();//��ע
			if(alitems != null)//�Ӽ���־
			p.Caution = (alitems[0] as FS.HISFC.Models.Order.Order).IsEmergency.ToString()+this.txtAttention.Text.Trim();
			p.ExeDept = (alitems[0] as FS.HISFC.Models.Order.Order).ExeDept.ID;
			p.IllHistory = this.txt1.Text.Trim();//��ʷ������
			p.CheckOrder = this.txtItems.Text.Trim();//����
            p.Oper.ID = this.p.Operator.ID;//����Ա
            if (p.ApplyDate == null || p.ApplyDate.Length <= 0)
			{
                p.ApplyDate = this.cnstManager.GetSysDateTime();//��������
			}
			else
			{
                p.ApplyDate = this.pacsbill.ApplyDate;
			}
			if(this.exec_sqn.Enabled==false && this.SpecimenType.Enabled==false)
			{
				string lPacsItem0 ="" ;
				if (this.cmbPacsItem0.Text != "" && this.cmbPacsItem0.Text !=null )
				{
					lPacsItem0 = this.cmbPacsItem0.Text  ;
				}
				string lPacsItem1="" ;
				if (this.cmbPacsItem1.Text != "" && this.cmbPacsItem1.Text !=null)
				{
					lPacsItem1="|" +this.cmbPacsItem1.Text;
				}
				string lPacsItem2="" ;
				if (this.cmbPacsItem2.Text != "" && this.cmbPacsItem2.Text !=null)
				{
					lPacsItem2="|" +this.cmbPacsItem2.Text ;
				}
				string lcmbPacsCheckType0="" ;
				if (this.cmbPacsCheckType0.Text != null && this.cmbPacsCheckType0.Text != ""  )
				{
					lcmbPacsCheckType0="|" +this.cmbPacsCheckType0.Text ;
				}
				string lcmbPacsCheckType1="" ;
				if (this.cmbPacsCheckType1.Text != null && this.cmbPacsCheckType1.Text != ""  )
				{
					lcmbPacsCheckType1="|" +this.cmbPacsCheckType1.Text ;
				}
				string lcmbPacsCheckType2="" ;
				if (this.cmbPacsCheckType2.Text != null && this.cmbPacsCheckType2.Text != ""   )
				{
					lcmbPacsCheckType2="|" +this.cmbPacsCheckType2.Text  ;
				}

				string s =  lPacsItem0+lcmbPacsCheckType0 + lPacsItem1+lcmbPacsCheckType1 +lPacsItem2 + lcmbPacsCheckType2;
				p.PacsItem = s ;
			}
			else
			{
				p.PacsItem=this.bodyPost.Text;//��λϸ������Ŀ2010-6-3
			}
				
			p.SampleDate = this.dtSample.Value.ToString();
            //p.YJDate = this.dtJJ.Value.ToString();
            //p.JJ = this.chkJJ.Checked;

			//2010-6-1
			p.Exec_sqn=this.exec_sqn.Text.ToString();//ִ�е���
			p.Antiviotic1=this.Antibiotic1.Text;//������1
			p.Antiviotic2=this.Antibiotic2.Text;//������2
			p.Temperature=this.Temperature.Text;//����
			p.SpecimenType=this.SpecimenType.Text;//������������

			return p;
		}
		/// <summary>
		/// �˳�
		/// </summary>
		public void Exit()
		{
			DialogResult r;

			FS.HISFC.Models.Order.PacsBill pp = this.PacsBillManager.QueryPacsBill(this.billNo);

			if(pp == null)
			{
				r = MessageBox.Show("�����뵥��Ϣ��δ���棡ȷ��Ҫ�˳���","��ʾ",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button2);

				if(r == DialogResult.Cancel)
				{
					return;
				}
			}
			this.FindForm().Close();
		}
		
		/// <summary>
		/// ��ӡ
		/// </summary>
		public void Print()
		{
			//this.RemoveBorder();
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

            foreach (Control contr in panel1.Controls)
            {
                contr.BackColor = System.Drawing.Color.White;
            }

			p.PrintPage(0,10,this.panel1);
		}
		/// <summary>
		/// ��ӡԤ��
		/// </summary>
		public void PrintPreview()
		{
            
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();

            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;//ȥ�����б߿�

			Font ft = this.txt1.Font;
			
			int line = 1;

			bool over = false;
			
			line = this.txt1.GetLineFromCharIndex(this.txt1.Text.Length-1);
			
			double space = 0;
			int asc = 0;
			double desc =0;

			space = ft.FontFamily.GetLineSpacing(System.Drawing.FontStyle.Regular);
			asc   = ft.FontFamily.GetEmHeight(System.Drawing.FontStyle.Regular);

			double bs = ft.Height*space/asc;

			if(txt1.Height < (line+1)*bs)
			{
				over = true;
				desc = (line+1)*bs - txt1.Height;
			}

			Hashtable hsPos = new Hashtable();

			if(over)
			{
				foreach(System.Windows.Forms.Control c in this.panel1.Controls)
				{
					if(c.Location.Y > this.txt1.Location.Y + this.txt1.Height)
					{
						hsPos.Add(c.Name,c.Location.X.ToString()+","+c.Location.Y.ToString());
						c.Location = new Point(c.Location.X,c.Location.Y+FS.FrameWork.Function.NConvert.ToInt32(System.Math.Ceiling(desc)));
					}
				}
			}

            this.pacsbill = PacsBillManager.QueryPacsBill(this.lblPacsBillID.Text.ToString());//��ѯ
			if(this.pacsbill==null)
			{
				MessageBox.Show("���ȱ����ٴ�ӡ������","��ʾ");
				return ;
			}
			else
			{
                this.RemoveBorder();
				p.PrintPreview(0,10,this.panel1);
                this.regainBorder();
			}
			foreach(System.Windows.Forms.Control c in this.panel1.Controls)
			{
				if(hsPos.Contains(c.Name))
				{
					c.Location = new Point(int.Parse(hsPos[c.Name].ToString().Split(',')[0]),int.Parse(hsPos[c.Name].ToString().Split(',')[1]));
				}
			}
		}
		/// <summary>
		/// ��ӡʱȥ���߿�
		/// </summary>
		private void RemoveBorder() 
		{
            this.txtMemo.BorderStyle = BorderStyle.None;
			this.txt1.BorderStyle = BorderStyle.None;
			this.txtDiagnose.BorderStyle = BorderStyle.None;
            this.txtAttention.BorderStyle = BorderStyle.None;
		}
		/// <summary>
		/// ��ӡ��ָ��߿�
		/// </summary>
		private void regainBorder() 
		{
            this.txtMemo.BorderStyle = BorderStyle.Fixed3D;;
            this.txt1.BorderStyle = BorderStyle.Fixed3D;;
            this.txtDiagnose.BorderStyle = BorderStyle.Fixed3D;;
            this.txtAttention.BorderStyle = BorderStyle.Fixed3D;;
		}
		/// <summary>
		/// ���¼�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mnuAdd_Click(object sender, System.EventArgs e) 
		{
			//if(this.richTextBox2.SelectedText =="") return;
			//this.ucUserText2.Add(this.richTextBox1.SelectedText,this.richTextBox1.SelectedRtf);
		}

		/// <summary>
		/// Load����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ucPacsApply_Load(object sender, EventArgs e) 
		{
			
			try
			{
				//components = new Container();
				components.Add(this.txt1);//��ʷ������
				//components.Add(this.txt2);
				components.Add(this.txtAttention);//ע������
				components.Add(this.txtDiagnose);//���
				components.Add(this.txtItems);//��Ŀ
				components.Add(this.txtMemo);//��ע
				this.ucUserText2.SetControl(this.components);

				if(this.FindForm().Tag != null && this.FindForm().Tag.ToString() == "1")
				{
					this.panel3.Visible = true;
					this.treeView1.Visible = true;
					this.treeView1.BringToFront();
					this.ucUserText2.Visible = false;

					if(this.dtend == DateTime.MinValue)
					{
						this.dtend = this.PacsBillManager.GetDateTimeFromSysDateTime();
						this.dtBegin = this.dtend.AddDays(-1);
					}

					
					this.cmbMachine.Enabled = false;
					this.txt1.ReadOnly = true;
					this.txtMemo.ReadOnly = true;
					this.txtAttention.ReadOnly = true;
	
					this.timer1.Start();
				}
				else
				{
					this.treeView1.Visible = false;
					this.ucUserText2.Visible = true;
					this.ucUserText2.BringToFront();
					this.panel3.Visible = false;
				}
				this.dsPacsItems = new DataSet();
                SOC.Local.PacsInterface.GYSY.PacsCompareFunction.GetPacsItemList(ref this.dsPacsItems);

                GetHospLogo();

				this.cmbMachine.Focus() ;
			}

			catch{}
		}
		/// <summary>
		/// ��ʼ�����鵥��Ϣ
		/// </summary>
		public void InitPacsInfo()
		{
			if(this.treeView1.Nodes.Count > 0)
			{
				this.treeView1.Nodes.Clear();
			}

			string deptCode = "";
			
			TreeNode deptNode = new TreeNode();
            ArrayList alPacs = this.PacsBillManager.QueryPacsBillByDept(this.GetReciptDoct().Dept.ID, this.dtBegin, this.dtend);

			if(alPacs != null)
			{
				foreach(FS.HISFC.Models.Order.PacsBill p in alPacs)
				{
					if(p.Dept.ID != deptCode)
					{
						deptNode = new TreeNode();

						this.treeView1.Nodes.Add(deptNode);

						deptCode = p.Dept.ID;

						deptNode.Text = p.Dept.Name;

						TreeNode node = new TreeNode();

						this.myPatient = this.myInpatient.QueryPatientInfoByInpatientNO(p.ClinicCode);//.Query(p.PatientNO,this.dtend.AddDays(-30));
	
						if(myPatient == null)
						{
							MessageBox.Show("û���ҵ�����Ϊ��"+p.PatientNO+"�Ļ��ߵ���Ч�Һ���Ϣ","��ʾ");

							continue;
						}

						node.Text = "["+this.myPatient.Name+"]"+p.BillName;

						node.Tag = p;

						deptNode.Nodes.Add(node);

						if(!this.hsPatient.Contains(p.ClinicCode))
						{
							hsPatient.Add(p.ClinicCode,this.myPatient);
						}
					}
					else
					{
						TreeNode node = new TreeNode();

						this.myPatient = this.myInpatient.QueryPatientInfoByInpatientNO(p.ClinicCode);//(p.PatientNO,this.dtend.AddDays(-30));
	
						if(myPatient == null)
						{
							MessageBox.Show("û���ҵ�����Ϊ��"+p.PatientNO+"�Ļ��ߵ���Ч�Һ���Ϣ","��ʾ");

							continue;
						}

						node.Text = "["+myPatient.Name+"]"+p.BillName;

						node.Tag = p;

						deptNode.Nodes.Add(node);

						if(!this.hsPatient.Contains(p.ClinicCode))
						{
							hsPatient.Add(p.ClinicCode,myPatient);
						}
					}
				}
			}

			this.treeView1.ExpandAll();

		}
		/// <summary>
		/// ����
		/// </summary>
		/// <returns></returns>
		public int SetUnvalid()
		{
			if(this.pacsbill!=null)
			{
				int iReturn =  this.PacsBillManager.UpdatePacsBillState(this.pacsbill);
				this.pacsbill = this.PacsBillManager.QueryPacsBill(this.pacsbill.ComboNO);
				return iReturn;
			}
			else 
				return 0;
		}
		/// <summary>
		/// ���
		/// </summary>
		private void Clear()
		{
			
		}
		/// <summary>
		/// ��ø�����Ŀ�۸�
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
		public decimal GetUndrugZtPrice(string ID)
		{
            //if(ID == "")
            //{
            //    return 0m;
            //}

            //ArrayList al = null;
            //FS.HISFC.BizLogic.Fee.UndrugPackAge ztManager = new FS.HISFC.BizLogic.Fee.UndrugPackAge();

            //al = ztManager.GetUndrugCombValidByCode(ID);
            //if(al == null)
            //{
            //    return 0m;
            //}
			
            //FS.HISFC.BizLogic.Fee.Item itemManager = new FS.HISFC.BizLogic.Fee.Item();
            //decimal tot_cost = 0m;
            //for(int i=0;i<al.Count;i++)
            //{
            //    FS.HISFC.Models.Fee.Item.UndrugComb info = al[i] as FS.HISFC.Models.Fee.Item.UndrugComb;
            //    if(info == null||info.ValidState=="1")
            //    {
            //        continue;
            //    }
            //    FS.HISFC.Models.Fee.Item.UndrugComb item = itemManager.GetValidItemByUndrugCode(info.itemCode);
            //    if(item == null)
            //    {
            //        continue;
            //    }
            //    tot_cost += info.Qty*item.Price;
            //}
            //return tot_cost;

            return 0;
		}

		/// <summary>
		/// ˫��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void treeView1_DoubleClick(object sender, System.EventArgs e)
		{
			if(this.treeView1.SelectedNode == null)
			{
				return;
			}

			if(this.treeView1.SelectedNode.Tag == null)
			{
				return;
			}

			FS.HISFC.Models.Order.PacsBill p = this.treeView1.SelectedNode.Tag as FS.HISFC.Models.Order.PacsBill;

			this.PacsBill = p;

			if(this.hsPatient.Contains(p.ClinicCode))
			{
				this.myPatient = this.hsPatient[p.ClinicCode] as FS.HISFC.Models.RADT.PatientInfo;
			}
			else
			{
				this.myPatient = this.myInpatient.QueryPatientInfoByInpatientNO(p.ClinicCode);
			}
		}

		/// <summary>
		/// ���Żس�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tbCardNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
            //if(e.KeyCode == Keys.Enter)
            //{
            //    ArrayList alPacs = this.PacsBillManager.QueryPacsBillByCardNo(this.tbCardNo.Text.Trim(),this.dtBegin,this.dtend);

            //    if(alPacs != null && alPacs.Count > 0)
            //    {
            //        OutPatient.frmPopPacsInfo frm = new pacsInterface.OutPatient.frmPopPacsInfo();

            //        frm.AlPacsBill = alPacs;

            //        frm.ShowDialog();

            //        if(frm.P != null)
            //        {
            //            this.PacsBill = frm.P;

            //            if(this.hsPatient.Contains(this.pacsbill.ClinicCode))
            //            {
            //                this.myPatient = this.hsPatient[this.pacsbill.ClinicCode] as FS.HISFC.Models.RADT.PatientInfo;
            //            }
            //            else
            //            {
            //                this.myPatient = this.myInpatient.QueryPatientInfoByInpatientNO(this.pacsbill.ClinicCode);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("û���ҵ��û��ߵ���Ч�����Ϣ�����޸�ʱ�����¼�����");
            //        return;
            //    }
            //}
		}
		/// <summary>
		/// ʱ����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timer1_Tick(object sender, System.EventArgs e)
		{
			this.dtend = this.myInpatient.GetDateTimeFromSysDateTime();

			this.dtBegin = this.dtend.AddDays(-1);

			this.InitPacsInfo();
		}
		/// <summary>
		/// ˢ��
		/// </summary>
		public void Refresh(bool isRefresh)
		{
			if(isRefresh)
			{
				this.timer1.Start();
			}
			else
			{
				this.timer1.Stop();
			}
		}

		/// <summary>
		/// �ı��豸����ʱ����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmbMachine_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.cmbPacsCheckType0.Items.Clear();
			this.cmbPacsCheckType1.Items.Clear();
			this.cmbPacsCheckType2.Items.Clear();

			this.cmbPacsItem0.Items.Clear();
			this.cmbPacsItem1.Items.Clear();
			this.cmbPacsItem2.Items.Clear();

			Hashtable hsItems = new Hashtable();
			ArrayList alPacsItems = new ArrayList();

			foreach(DataRow row in this.dsPacsItems.Tables[0].Rows)
			{
				if(row["MACHINE_TYPE"].ToString() == this.cmbMachine.Text)
				{
					if(!hsItems.Contains(row["ITEM_NAME"].ToString()))
					{
						hsItems.Add(row["ITEM_NAME"].ToString(),null);

                        FS.HISFC.Models.Base.Spell obj = new FS.HISFC.Models.Base.Spell();
						
						obj.ID = row["ITEM_CODE"].ToString();
						obj.Name = row["ITEM_NAME"].ToString();
						obj.SpellCode = row["SPELL_CODE"].ToString();

						alPacsItems.Add(obj);
					}
				}
			}
			this.cmbPacsItem0.SelectedIndexChanged -= new System.EventHandler(this.cmbPacsItem0_SelectedIndexChanged);
			this.cmbPacsItem1.SelectedIndexChanged -= new System.EventHandler(this.cmbPacsItem0_SelectedIndexChanged);
			this.cmbPacsItem2.SelectedIndexChanged -= new System.EventHandler(this.cmbPacsItem0_SelectedIndexChanged);
			
			this.cmbPacsItem0.AddItems(alPacsItems);
			this.cmbPacsItem1.AddItems(alPacsItems);
			this.cmbPacsItem2.AddItems(alPacsItems);

			this.cmbPacsItem0.SelectedIndexChanged += new System.EventHandler(this.cmbPacsItem0_SelectedIndexChanged);
			this.cmbPacsItem1.SelectedIndexChanged += new System.EventHandler(this.cmbPacsItem0_SelectedIndexChanged);
			this.cmbPacsItem2.SelectedIndexChanged += new System.EventHandler(this.cmbPacsItem0_SelectedIndexChanged);
		}

		/// <summary>
		/// �л���Ŀʱ����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmbPacsItem0_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(sender == this.cmbPacsItem0)
			{
				this.cmbPacsCheckType0.alItems = new ArrayList();
			}
			if(sender == this.cmbPacsItem1)
			{
				this.cmbPacsCheckType1.alItems = new ArrayList();
			}
			if(sender == this.cmbPacsItem2)
			{
				this.cmbPacsCheckType2.alItems = new ArrayList();
			}
            
			ArrayList alPacsCheckType = new ArrayList();
			foreach(DataRow row in this.dsPacsItems.Tables[0].Rows)
			{
				if(row["MACHINE_TYPE"].ToString() == this.cmbMachine.Text && (sender as FS.FrameWork.WinForms.Controls.NeuComboBox).Text == row["ITEM_NAME"].ToString())
				{
//					FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
					FS.HISFC.Models.Base.Spell obj = new FS.HISFC.Models.Base.Spell();
					obj.Name = row["CHECK_BODY"].ToString();
                    
                    alPacsCheckType.Add(obj) ;
				}
			}

			if(sender == this.cmbPacsItem0)
			{
				this.cmbPacsCheckType0.AddItems(alPacsCheckType) ;
			}
			if(sender == this.cmbPacsItem1)
			{
				this.cmbPacsCheckType1.AddItems(alPacsCheckType) ;
			}
			if(sender == this.cmbPacsItem2)
			{
				this.cmbPacsCheckType2.AddItems(alPacsCheckType) ;
			}
		}

		/// <summary>
		/// �ո񵯳���������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SpecimenType_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyData==Keys.Space)
			{
				FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();

				ArrayList SpecimenTypeList = this.cnstManager.GetAllList("LABSAMPLE");

				if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(SpecimenTypeList,ref obj) <= 0) 
				{
					return;
				}

				if(obj != null)
				{
					this.SpecimenType.Tag = obj.ID;
					this.SpecimenType.Text = obj.Name;
				}
			}
		}
        /// <summary>
        /// ����������ҩƷ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void Antibiotic1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyData==Keys.Space)
			{
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
//                FS.HISFC.Management.Pharmacy.Item  itemMgr = new FS.HISFC.Management.Pharmacy.Item();
				ArrayList phaAl =this.cnstManager.GetList("KSSDRUG");               //itemMgr.GetItemAvailableList();

                if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(phaAl, ref obj) <= 0) 
				{
					return;
				}

				if(obj != null)
				{
					this.Antibiotic1.Tag = obj.ID;
					this.Antibiotic1.Text = obj.Name;
				}
			}
		}
		/// <summary>
		/// ����������ҩƷ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Antibiotic2_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyData==Keys.Space)
			{
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
				//                FS.HISFC.Management.Pharmacy.Item  itemMgr = new FS.HISFC.Management.Pharmacy.Item();
				ArrayList phaAl =this.cnstManager.GetList("KSSDRUG");               //itemMgr.GetItemAvailableList();

				if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(phaAl,ref obj) <= 0) 
				{
					return;
				}

				if(obj != null)
				{
					this.Antibiotic2.Tag = obj.ID;
					this.Antibiotic2.Text = obj.Name;
				}
			}
		}

        /// <summary>
        /// ���������뷽��
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private Image CreateBarCode(string code)
        {
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            BarcodeLib.TYPE type = BarcodeLib.TYPE.CODE128;
            BarcodeLib.AlignmentPositions Align = BarcodeLib.AlignmentPositions.CENTER;
            b.IncludeLabel = true;
            b.Alignment = Align;
            return b.Encode(type, code, System.Drawing.Color.Black, System.Drawing.Color.White, this.npbBarCode.Size.Width, this.npbBarCode.Height);
        }
     

        private void label17_Click(object sender, EventArgs e)
        {

        }

	}
}
