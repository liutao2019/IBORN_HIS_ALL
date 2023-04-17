using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SOC.Local.PacsInterface.GYSY
{
    
    partial class ucPacsApplyForClinic
    {
        #region 设计器

        # region 系统生成控件变量
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblNo;
        private System.Windows.Forms.Label lblPatientNo;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.Label lblDept;
        private System.Windows.Forms.Label lblSex;
        private System.Windows.Forms.Label lblPaykind;
        private System.Windows.Forms.Label lblDoc;
        public System.Windows.Forms.RichTextBox txt2;
        private System.Windows.Forms.Label lblApplyName;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label6;
        private FS.HISFC.Components.Common.Controls.ucUserText ucUserText2;
        private System.Windows.Forms.Label lblPacsBillID;
        //public event PacsApplyHandler ApplyEvent;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.RichTextBox txtResult;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblEmergency;
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
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Panel panelSpecail;
        private System.Windows.Forms.CheckBox chkJJ;
        private System.Windows.Forms.DateTimePicker dtJJ;
        private System.Windows.Forms.DateTimePicker dtSample;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbPacsCheckType4;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbPacsCheckType3;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbPacsCheckType2;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbPacsCheckType1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbPacsItem4;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbPacsItem3;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbPacsItem2;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbPacsItem1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbPacsCheckType0;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbPacsItem0;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblExeDept;
        private System.Windows.Forms.Label label28;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbMachine;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblAddressPhone;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label lblMedicalCardNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtHistory;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtDiagnose;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtMemo;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtAttention;
        # endregion

        #region 组件设计器生成的代码
        /// <summary> 
        /// 设计器支持所需的方法 - 不要使用代码编辑器 
        /// 修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtItems = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.npbBarCode = new FS.FrameWork.WinForms.Controls.NeuPictureBox();
            this.picbLogo = new System.Windows.Forms.PictureBox();
            this.txtAttention = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtMemo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtDiagnose = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtHistory = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbPacsCheckType4 = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbPacsCheckType2 = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbPacsCheckType1 = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbPacsItem4 = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbPacsItem3 = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbPacsItem2 = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbPacsItem1 = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbPacsCheckType0 = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbPacsItem0 = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label30 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblExeDept = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.cmbMachine = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label10 = new System.Windows.Forms.Label();
            this.panelSpecail = new System.Windows.Forms.Panel();
            this.chkJJ = new System.Windows.Forms.CheckBox();
            this.dtJJ = new System.Windows.Forms.DateTimePicker();
            this.label26 = new System.Windows.Forms.Label();
            this.dtSample = new System.Windows.Forms.DateTimePicker();
            this.label27 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.lblAge = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.lblSex = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblEmergency = new System.Windows.Forms.Label();
            this.txtResult = new System.Windows.Forms.RichTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lblPacsBillID = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.txt2 = new System.Windows.Forms.RichTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.lblPaykind = new System.Windows.Forms.Label();
            this.lblDept = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblPatientNo = new System.Windows.Forms.Label();
            this.lblNo = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.lblDoc = new System.Windows.Forms.Label();
            this.lblApplyName = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lblAddressPhone = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.lblMedicalCardNo = new System.Windows.Forms.Label();
            this.cmbPacsCheckType3 = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
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
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.txtItems);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.npbBarCode);
            this.panel1.Controls.Add(this.picbLogo);
            this.panel1.Controls.Add(this.txtAttention);
            this.panel1.Controls.Add(this.txtMemo);
            this.panel1.Controls.Add(this.txtDiagnose);
            this.panel1.Controls.Add(this.txtHistory);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cmbPacsCheckType4);
            this.panel1.Controls.Add(this.cmbPacsCheckType2);
            this.panel1.Controls.Add(this.cmbPacsCheckType1);
            this.panel1.Controls.Add(this.cmbPacsItem4);
            this.panel1.Controls.Add(this.cmbPacsItem3);
            this.panel1.Controls.Add(this.cmbPacsItem2);
            this.panel1.Controls.Add(this.cmbPacsItem1);
            this.panel1.Controls.Add(this.cmbPacsCheckType0);
            this.panel1.Controls.Add(this.cmbPacsItem0);
            this.panel1.Controls.Add(this.label30);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.lblExeDept);
            this.panel1.Controls.Add(this.label28);
            this.panel1.Controls.Add(this.cmbMachine);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.panelSpecail);
            this.panel1.Controls.Add(this.label24);
            this.panel1.Controls.Add(this.label23);
            this.panel1.Controls.Add(this.label22);
            this.panel1.Controls.Add(this.lblAge);
            this.panel1.Controls.Add(this.label21);
            this.panel1.Controls.Add(this.lblSex);
            this.panel1.Controls.Add(this.label20);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.lblDate);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.lblEmergency);
            this.panel1.Controls.Add(this.txtResult);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.lblPacsBillID);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.panel9);
            this.panel1.Controls.Add(this.txt2);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.lblPaykind);
            this.panel1.Controls.Add(this.lblDept);
            this.panel1.Controls.Add(this.lblName);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.lblPatientNo);
            this.panel1.Controls.Add(this.lblNo);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.lblDoc);
            this.panel1.Controls.Add(this.lblApplyName);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.lblAddressPhone);
            this.panel1.Controls.Add(this.label25);
            this.panel1.Controls.Add(this.lblMedicalCardNo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.panel1.Location = new System.Drawing.Point(3, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(614, 808);
            this.panel1.TabIndex = 0;
            // 
            // txtItems
            // 
            this.txtItems.BackColor = System.Drawing.Color.White;
            this.txtItems.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtItems.IsEnter2Tab = false;
            this.txtItems.Location = new System.Drawing.Point(97, 394);
            this.txtItems.Multiline = true;
            this.txtItems.Name = "txtItems";
            this.txtItems.Size = new System.Drawing.Size(472, 50);
            this.txtItems.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtItems.TabIndex = 148;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(373, 758);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(191, 12);
            this.label3.TabIndex = 145;
            this.label3.Text = "服务中心电话：39195566,39131330";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(11, 642);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 14);
            this.label2.TabIndex = 144;
            this.label2.Text = "注意事项:";
            // 
            // npbBarCode
            // 
            this.npbBarCode.Location = new System.Drawing.Point(400, 10);
            this.npbBarCode.Name = "npbBarCode";
            this.npbBarCode.Size = new System.Drawing.Size(150, 39);
            this.npbBarCode.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.npbBarCode.TabIndex = 143;
            this.npbBarCode.TabStop = false;
            // 
            // picbLogo
            // 
            this.picbLogo.Location = new System.Drawing.Point(68, 5);
            this.picbLogo.Name = "picbLogo";
            this.picbLogo.Size = new System.Drawing.Size(316, 49);
            this.picbLogo.TabIndex = 142;
            this.picbLogo.TabStop = false;
            // 
            // txtAttention
            // 
            this.txtAttention.BackColor = System.Drawing.SystemColors.Control;
            this.txtAttention.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAttention.IsEnter2Tab = false;
            this.txtAttention.Location = new System.Drawing.Point(92, 640);
            this.txtAttention.Multiline = true;
            this.txtAttention.Name = "txtAttention";
            this.txtAttention.Size = new System.Drawing.Size(477, 85);
            this.txtAttention.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtAttention.TabIndex = 113;
            // 
            // txtMemo
            // 
            this.txtMemo.BackColor = System.Drawing.SystemColors.Control;
            this.txtMemo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMemo.IsEnter2Tab = false;
            this.txtMemo.Location = new System.Drawing.Point(96, 460);
            this.txtMemo.Multiline = true;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(473, 22);
            this.txtMemo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtMemo.TabIndex = 112;
            // 
            // txtDiagnose
            // 
            this.txtDiagnose.BackColor = System.Drawing.SystemColors.Control;
            this.txtDiagnose.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDiagnose.IsEnter2Tab = false;
            this.txtDiagnose.Location = new System.Drawing.Point(96, 310);
            this.txtDiagnose.Multiline = true;
            this.txtDiagnose.Name = "txtDiagnose";
            this.txtDiagnose.Size = new System.Drawing.Size(473, 69);
            this.txtDiagnose.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtDiagnose.TabIndex = 111;
            // 
            // txtHistory
            // 
            this.txtHistory.BackColor = System.Drawing.Color.White;
            this.txtHistory.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtHistory.IsEnter2Tab = false;
            this.txtHistory.Location = new System.Drawing.Point(95, 191);
            this.txtHistory.Multiline = true;
            this.txtHistory.Name = "txtHistory";
            this.txtHistory.Size = new System.Drawing.Size(474, 112);
            this.txtHistory.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtHistory.TabIndex = 110;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(436, 116);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 17);
            this.label7.TabIndex = 109;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(350, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 14);
            this.label1.TabIndex = 108;
            this.label1.Text = "门诊号：";
            // 
            // cmbPacsCheckType4
            // 
            this.cmbPacsCheckType4.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbPacsCheckType4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPacsCheckType4.IsEnter2Tab = false;
            this.cmbPacsCheckType4.IsFlat = false;
            this.cmbPacsCheckType4.IsLike = true;
            this.cmbPacsCheckType4.IsListOnly = false;
            this.cmbPacsCheckType4.IsPopForm = true;
            this.cmbPacsCheckType4.IsShowCustomerList = false;
            this.cmbPacsCheckType4.IsShowID = false;
            this.cmbPacsCheckType4.Location = new System.Drawing.Point(507, 693);
            this.cmbPacsCheckType4.Name = "cmbPacsCheckType4";
            this.cmbPacsCheckType4.PopForm = null;
            this.cmbPacsCheckType4.ShowCustomerList = false;
            this.cmbPacsCheckType4.ShowID = false;
            this.cmbPacsCheckType4.Size = new System.Drawing.Size(203, 20);
            this.cmbPacsCheckType4.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbPacsCheckType4.TabIndex = 107;
            this.cmbPacsCheckType4.Tag = "";
            this.cmbPacsCheckType4.ToolBarUse = false;
            this.cmbPacsCheckType4.Visible = false;
            // 
            // cmbPacsCheckType2
            // 
            this.cmbPacsCheckType2.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbPacsCheckType2.BackColor = System.Drawing.SystemColors.Control;
            this.cmbPacsCheckType2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPacsCheckType2.IsEnter2Tab = false;
            this.cmbPacsCheckType2.IsFlat = false;
            this.cmbPacsCheckType2.IsLike = true;
            this.cmbPacsCheckType2.IsListOnly = false;
            this.cmbPacsCheckType2.IsPopForm = true;
            this.cmbPacsCheckType2.IsShowCustomerList = false;
            this.cmbPacsCheckType2.IsShowID = false;
            this.cmbPacsCheckType2.Location = new System.Drawing.Point(419, 572);
            this.cmbPacsCheckType2.Name = "cmbPacsCheckType2";
            this.cmbPacsCheckType2.PopForm = null;
            this.cmbPacsCheckType2.ShowCustomerList = false;
            this.cmbPacsCheckType2.ShowID = false;
            this.cmbPacsCheckType2.Size = new System.Drawing.Size(137, 20);
            this.cmbPacsCheckType2.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbPacsCheckType2.TabIndex = 103;
            this.cmbPacsCheckType2.Tag = "";
            this.cmbPacsCheckType2.ToolBarUse = false;
            // 
            // cmbPacsCheckType1
            // 
            this.cmbPacsCheckType1.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbPacsCheckType1.BackColor = System.Drawing.SystemColors.Control;
            this.cmbPacsCheckType1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPacsCheckType1.IsEnter2Tab = false;
            this.cmbPacsCheckType1.IsFlat = false;
            this.cmbPacsCheckType1.IsLike = true;
            this.cmbPacsCheckType1.IsListOnly = false;
            this.cmbPacsCheckType1.IsPopForm = true;
            this.cmbPacsCheckType1.IsShowCustomerList = false;
            this.cmbPacsCheckType1.IsShowID = false;
            this.cmbPacsCheckType1.Location = new System.Drawing.Point(419, 546);
            this.cmbPacsCheckType1.Name = "cmbPacsCheckType1";
            this.cmbPacsCheckType1.PopForm = null;
            this.cmbPacsCheckType1.ShowCustomerList = false;
            this.cmbPacsCheckType1.ShowID = false;
            this.cmbPacsCheckType1.Size = new System.Drawing.Size(137, 20);
            this.cmbPacsCheckType1.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbPacsCheckType1.TabIndex = 101;
            this.cmbPacsCheckType1.Tag = "";
            this.cmbPacsCheckType1.ToolBarUse = false;
            // 
            // cmbPacsItem4
            // 
            this.cmbPacsItem4.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbPacsItem4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPacsItem4.IsEnter2Tab = false;
            this.cmbPacsItem4.IsFlat = false;
            this.cmbPacsItem4.IsLike = true;
            this.cmbPacsItem4.IsListOnly = false;
            this.cmbPacsItem4.IsPopForm = true;
            this.cmbPacsItem4.IsShowCustomerList = false;
            this.cmbPacsItem4.IsShowID = false;
            this.cmbPacsItem4.Location = new System.Drawing.Point(178, 693);
            this.cmbPacsItem4.Name = "cmbPacsItem4";
            this.cmbPacsItem4.PopForm = null;
            this.cmbPacsItem4.ShowCustomerList = false;
            this.cmbPacsItem4.ShowID = false;
            this.cmbPacsItem4.Size = new System.Drawing.Size(312, 20);
            this.cmbPacsItem4.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbPacsItem4.TabIndex = 106;
            this.cmbPacsItem4.Tag = "";
            this.cmbPacsItem4.ToolBarUse = false;
            this.cmbPacsItem4.Visible = false;
            // 
            // cmbPacsItem3
            // 
            this.cmbPacsItem3.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbPacsItem3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPacsItem3.IsEnter2Tab = false;
            this.cmbPacsItem3.IsFlat = false;
            this.cmbPacsItem3.IsLike = true;
            this.cmbPacsItem3.IsListOnly = false;
            this.cmbPacsItem3.IsPopForm = true;
            this.cmbPacsItem3.IsShowCustomerList = false;
            this.cmbPacsItem3.IsShowID = false;
            this.cmbPacsItem3.Location = new System.Drawing.Point(178, 735);
            this.cmbPacsItem3.Name = "cmbPacsItem3";
            this.cmbPacsItem3.PopForm = null;
            this.cmbPacsItem3.ShowCustomerList = false;
            this.cmbPacsItem3.ShowID = false;
            this.cmbPacsItem3.Size = new System.Drawing.Size(312, 20);
            this.cmbPacsItem3.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbPacsItem3.TabIndex = 104;
            this.cmbPacsItem3.Tag = "";
            this.cmbPacsItem3.ToolBarUse = false;
            this.cmbPacsItem3.Visible = false;
            // 
            // cmbPacsItem2
            // 
            this.cmbPacsItem2.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbPacsItem2.BackColor = System.Drawing.SystemColors.Control;
            this.cmbPacsItem2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPacsItem2.IsEnter2Tab = false;
            this.cmbPacsItem2.IsFlat = false;
            this.cmbPacsItem2.IsLike = true;
            this.cmbPacsItem2.IsListOnly = false;
            this.cmbPacsItem2.IsPopForm = true;
            this.cmbPacsItem2.IsShowCustomerList = false;
            this.cmbPacsItem2.IsShowID = false;
            this.cmbPacsItem2.Location = new System.Drawing.Point(93, 572);
            this.cmbPacsItem2.Name = "cmbPacsItem2";
            this.cmbPacsItem2.PopForm = null;
            this.cmbPacsItem2.ShowCustomerList = false;
            this.cmbPacsItem2.ShowID = false;
            this.cmbPacsItem2.Size = new System.Drawing.Size(312, 20);
            this.cmbPacsItem2.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbPacsItem2.TabIndex = 102;
            this.cmbPacsItem2.Tag = "";
            this.cmbPacsItem2.ToolBarUse = false;
            // 
            // cmbPacsItem1
            // 
            this.cmbPacsItem1.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbPacsItem1.BackColor = System.Drawing.SystemColors.Control;
            this.cmbPacsItem1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPacsItem1.IsEnter2Tab = false;
            this.cmbPacsItem1.IsFlat = false;
            this.cmbPacsItem1.IsLike = true;
            this.cmbPacsItem1.IsListOnly = false;
            this.cmbPacsItem1.IsPopForm = true;
            this.cmbPacsItem1.IsShowCustomerList = false;
            this.cmbPacsItem1.IsShowID = false;
            this.cmbPacsItem1.Location = new System.Drawing.Point(93, 546);
            this.cmbPacsItem1.Name = "cmbPacsItem1";
            this.cmbPacsItem1.PopForm = null;
            this.cmbPacsItem1.ShowCustomerList = false;
            this.cmbPacsItem1.ShowID = false;
            this.cmbPacsItem1.Size = new System.Drawing.Size(312, 20);
            this.cmbPacsItem1.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbPacsItem1.TabIndex = 100;
            this.cmbPacsItem1.Tag = "";
            this.cmbPacsItem1.ToolBarUse = false;
            // 
            // cmbPacsCheckType0
            // 
            this.cmbPacsCheckType0.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbPacsCheckType0.BackColor = System.Drawing.SystemColors.Control;
            this.cmbPacsCheckType0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPacsCheckType0.IsEnter2Tab = false;
            this.cmbPacsCheckType0.IsFlat = false;
            this.cmbPacsCheckType0.IsLike = true;
            this.cmbPacsCheckType0.IsListOnly = false;
            this.cmbPacsCheckType0.IsPopForm = true;
            this.cmbPacsCheckType0.IsShowCustomerList = false;
            this.cmbPacsCheckType0.IsShowID = false;
            this.cmbPacsCheckType0.Location = new System.Drawing.Point(419, 517);
            this.cmbPacsCheckType0.Name = "cmbPacsCheckType0";
            this.cmbPacsCheckType0.PopForm = null;
            this.cmbPacsCheckType0.ShowCustomerList = false;
            this.cmbPacsCheckType0.ShowID = false;
            this.cmbPacsCheckType0.Size = new System.Drawing.Size(137, 20);
            this.cmbPacsCheckType0.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbPacsCheckType0.TabIndex = 99;
            this.cmbPacsCheckType0.Tag = "";
            this.cmbPacsCheckType0.ToolBarUse = false;
            // 
            // cmbPacsItem0
            // 
            this.cmbPacsItem0.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbPacsItem0.BackColor = System.Drawing.SystemColors.Control;
            this.cmbPacsItem0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPacsItem0.IsEnter2Tab = false;
            this.cmbPacsItem0.IsFlat = false;
            this.cmbPacsItem0.IsLike = true;
            this.cmbPacsItem0.IsListOnly = false;
            this.cmbPacsItem0.IsPopForm = true;
            this.cmbPacsItem0.IsShowCustomerList = false;
            this.cmbPacsItem0.IsShowID = false;
            this.cmbPacsItem0.Location = new System.Drawing.Point(93, 517);
            this.cmbPacsItem0.Name = "cmbPacsItem0";
            this.cmbPacsItem0.PopForm = null;
            this.cmbPacsItem0.ShowCustomerList = false;
            this.cmbPacsItem0.ShowID = false;
            this.cmbPacsItem0.Size = new System.Drawing.Size(312, 20);
            this.cmbPacsItem0.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbPacsItem0.TabIndex = 98;
            this.cmbPacsItem0.Tag = "";
            this.cmbPacsItem0.ToolBarUse = false;
            // 
            // label30
            // 
            this.label30.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label30.ForeColor = System.Drawing.Color.Blue;
            this.label30.Location = new System.Drawing.Point(105, 494);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(107, 16);
            this.label30.TabIndex = 97;
            this.label30.Text = "医技检查项目";
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.ForeColor = System.Drawing.Color.Blue;
            this.label9.Location = new System.Drawing.Point(434, 494);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(113, 16);
            this.label9.TabIndex = 96;
            this.label9.Text = "医技检查方法";
            // 
            // lblExeDept
            // 
            this.lblExeDept.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblExeDept.ForeColor = System.Drawing.Color.Blue;
            this.lblExeDept.Location = new System.Drawing.Point(12, 578);
            this.lblExeDept.Name = "lblExeDept";
            this.lblExeDept.Size = new System.Drawing.Size(80, 35);
            this.lblExeDept.TabIndex = 95;
            // 
            // label28
            // 
            this.label28.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label28.ForeColor = System.Drawing.Color.Blue;
            this.label28.Location = new System.Drawing.Point(12, 546);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(78, 18);
            this.label28.TabIndex = 94;
            this.label28.Text = "执行科室:";
            // 
            // cmbMachine
            // 
            this.cmbMachine.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbMachine.BackColor = System.Drawing.SystemColors.Control;
            this.cmbMachine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMachine.IsEnter2Tab = false;
            this.cmbMachine.IsFlat = false;
            this.cmbMachine.IsLike = true;
            this.cmbMachine.IsListOnly = false;
            this.cmbMachine.IsPopForm = true;
            this.cmbMachine.IsShowCustomerList = false;
            this.cmbMachine.IsShowID = false;
            this.cmbMachine.Location = new System.Drawing.Point(10, 517);
            this.cmbMachine.Name = "cmbMachine";
            this.cmbMachine.PopForm = null;
            this.cmbMachine.ShowCustomerList = false;
            this.cmbMachine.ShowID = false;
            this.cmbMachine.Size = new System.Drawing.Size(83, 20);
            this.cmbMachine.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbMachine.TabIndex = 93;
            this.cmbMachine.Tag = "";
            this.cmbMachine.ToolBarUse = false;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.ForeColor = System.Drawing.Color.Blue;
            this.label10.Location = new System.Drawing.Point(11, 494);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(81, 16);
            this.label10.TabIndex = 92;
            this.label10.Text = "设备类型";
            // 
            // panelSpecail
            // 
            this.panelSpecail.Controls.Add(this.chkJJ);
            this.panelSpecail.Controls.Add(this.dtJJ);
            this.panelSpecail.Controls.Add(this.label26);
            this.panelSpecail.Controls.Add(this.dtSample);
            this.panelSpecail.Controls.Add(this.label27);
            this.panelSpecail.Location = new System.Drawing.Point(121, 720);
            this.panelSpecail.Name = "panelSpecail";
            this.panelSpecail.Size = new System.Drawing.Size(649, 27);
            this.panelSpecail.TabIndex = 75;
            this.panelSpecail.Visible = false;
            // 
            // chkJJ
            // 
            this.chkJJ.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.chkJJ.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkJJ.Location = new System.Drawing.Point(560, 4);
            this.chkJJ.Name = "chkJJ";
            this.chkJJ.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkJJ.Size = new System.Drawing.Size(60, 21);
            this.chkJJ.TabIndex = 77;
            this.chkJJ.Text = "绝 经";
            // 
            // dtJJ
            // 
            this.dtJJ.CustomFormat = "yyyy年MM月dd日";
            this.dtJJ.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtJJ.Location = new System.Drawing.Point(375, 4);
            this.dtJJ.Name = "dtJJ";
            this.dtJJ.Size = new System.Drawing.Size(119, 21);
            this.dtJJ.TabIndex = 75;
            // 
            // label26
            // 
            this.label26.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label26.Location = new System.Drawing.Point(274, 4);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(107, 17);
            this.label26.TabIndex = 76;
            this.label26.Text = "末次绝经日期：";
            // 
            // dtSample
            // 
            this.dtSample.CustomFormat = "yyyy年MM月dd日";
            this.dtSample.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtSample.Location = new System.Drawing.Point(101, 4);
            this.dtSample.Name = "dtSample";
            this.dtSample.Size = new System.Drawing.Size(119, 21);
            this.dtSample.TabIndex = 73;
            // 
            // label27
            // 
            this.label27.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label27.Location = new System.Drawing.Point(3, 3);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(107, 17);
            this.label27.TabIndex = 74;
            this.label27.Text = "样本采集日期：";
            // 
            // label24
            // 
            this.label24.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label24.Location = new System.Drawing.Point(392, 601);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(85, 15);
            this.label24.TabIndex = 68;
            this.label24.Text = "申请医师：";
            // 
            // label23
            // 
            this.label23.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label23.Location = new System.Drawing.Point(393, 70);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(79, 13);
            this.label23.TabIndex = 67;
            this.label23.Text = "申请单号：";
            this.label23.Visible = false;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label22.Location = new System.Drawing.Point(338, 159);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(52, 14);
            this.label22.TabIndex = 66;
            this.label22.Text = "科室：";
            // 
            // lblAge
            // 
            this.lblAge.AutoSize = true;
            this.lblAge.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAge.Location = new System.Drawing.Point(295, 160);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(0, 14);
            this.lblAge.TabIndex = 12;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label21.Location = new System.Drawing.Point(243, 160);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(52, 14);
            this.label21.TabIndex = 65;
            this.label21.Text = "年龄：";
            // 
            // lblSex
            // 
            this.lblSex.AutoSize = true;
            this.lblSex.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSex.Location = new System.Drawing.Point(202, 160);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(0, 14);
            this.lblSex.TabIndex = 15;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label20.Location = new System.Drawing.Point(145, 160);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(52, 14);
            this.label20.TabIndex = 64;
            this.label20.Text = "性别：";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label19.Location = new System.Drawing.Point(13, 162);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(52, 14);
            this.label19.TabIndex = 63;
            this.label19.Text = "姓名：";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDate.Location = new System.Drawing.Point(92, 93);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(0, 14);
            this.lblDate.TabIndex = 8;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(12, 93);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(82, 14);
            this.label12.TabIndex = 60;
            this.label12.Text = "申请日期：";
            // 
            // lblEmergency
            // 
            this.lblEmergency.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblEmergency.ForeColor = System.Drawing.Color.Red;
            this.lblEmergency.Location = new System.Drawing.Point(12, 39);
            this.lblEmergency.Name = "lblEmergency";
            this.lblEmergency.Size = new System.Drawing.Size(89, 20);
            this.lblEmergency.TabIndex = 55;
            this.lblEmergency.Text = "加  急";
            this.lblEmergency.Visible = false;
            // 
            // txtResult
            // 
            this.txtResult.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtResult.Location = new System.Drawing.Point(29, 793);
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(531, 10);
            this.txtResult.TabIndex = 52;
            this.txtResult.Text = "";
            this.txtResult.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(231, 63);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(129, 16);
            this.label8.TabIndex = 42;
            this.label8.Text = "检 查 申 请 单";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPacsBillID
            // 
            this.lblPacsBillID.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPacsBillID.Location = new System.Drawing.Point(468, 71);
            this.lblPacsBillID.Name = "lblPacsBillID";
            this.lblPacsBillID.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblPacsBillID.Size = new System.Drawing.Size(101, 13);
            this.lblPacsBillID.TabIndex = 36;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(11, 465);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 14);
            this.label6.TabIndex = 34;
            this.label6.Text = "检查目的:";
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.Black;
            this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel9.Location = new System.Drawing.Point(7, 630);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(574, 1);
            this.panel9.TabIndex = 33;
            // 
            // txt2
            // 
            this.txt2.Location = new System.Drawing.Point(214, 765);
            this.txt2.Name = "txt2";
            this.txt2.Size = new System.Drawing.Size(68, 23);
            this.txt2.TabIndex = 22;
            this.txt2.Text = "";
            this.txt2.Visible = false;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(12, 312);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(106, 13);
            this.label13.TabIndex = 17;
            this.label13.Text = "诊    断:";
            // 
            // lblPaykind
            // 
            this.lblPaykind.AutoSize = true;
            this.lblPaykind.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPaykind.Location = new System.Drawing.Point(436, 92);
            this.lblPaykind.Name = "lblPaykind";
            this.lblPaykind.Size = new System.Drawing.Size(0, 14);
            this.lblPaykind.TabIndex = 16;
            // 
            // lblDept
            // 
            this.lblDept.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDept.Location = new System.Drawing.Point(397, 160);
            this.lblDept.Name = "lblDept";
            this.lblDept.Size = new System.Drawing.Size(39, 14);
            this.lblDept.TabIndex = 13;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblName.Location = new System.Drawing.Point(66, 160);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(0, 14);
            this.lblName.TabIndex = 11;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Black;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Location = new System.Drawing.Point(11, 184);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(574, 1);
            this.panel4.TabIndex = 9;
            // 
            // lblPatientNo
            // 
            this.lblPatientNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientNo.Location = new System.Drawing.Point(382, 10);
            this.lblPatientNo.Name = "lblPatientNo";
            this.lblPatientNo.Size = new System.Drawing.Size(205, 40);
            this.lblPatientNo.TabIndex = 6;
            this.lblPatientNo.Visible = false;
            // 
            // lblNo
            // 
            this.lblNo.Location = new System.Drawing.Point(589, 766);
            this.lblNo.Name = "lblNo";
            this.lblNo.Size = new System.Drawing.Size(176, 16);
            this.lblNo.TabIndex = 5;
            this.lblNo.Text = "检查（检验）号：";
            this.lblNo.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Location = new System.Drawing.Point(9, 59);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(574, 1);
            this.panel2.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(142, 761);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 31;
            this.label5.Text = "检查结果：";
            this.label5.Visible = false;
            // 
            // lblDoc
            // 
            this.lblDoc.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDoc.Location = new System.Drawing.Point(483, 599);
            this.lblDoc.Name = "lblDoc";
            this.lblDoc.Size = new System.Drawing.Size(98, 15);
            this.lblDoc.TabIndex = 20;
            // 
            // lblApplyName
            // 
            this.lblApplyName.AutoSize = true;
            this.lblApplyName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblApplyName.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblApplyName.Location = new System.Drawing.Point(89, 62);
            this.lblApplyName.Name = "lblApplyName";
            this.lblApplyName.Size = new System.Drawing.Size(76, 16);
            this.lblApplyName.TabIndex = 4;
            this.lblApplyName.Text = "项目名称";
            this.lblApplyName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblApplyName.Visible = false;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(10, 194);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(107, 31);
            this.label14.TabIndex = 18;
            this.label14.Text = "病史及特征：";
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.Location = new System.Drawing.Point(13, 397);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(84, 16);
            this.label15.TabIndex = 19;
            this.label15.Text = "检查项目：";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Black;
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel6.Location = new System.Drawing.Point(11, 387);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(574, 1);
            this.panel6.TabIndex = 37;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label18.Location = new System.Drawing.Point(349, 93);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(82, 14);
            this.label18.TabIndex = 62;
            this.label18.Text = "费用类别：";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.Location = new System.Drawing.Point(12, 140);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(90, 14);
            this.label17.TabIndex = 63;
            this.label17.Text = "住址/电话：";
            // 
            // lblAddressPhone
            // 
            this.lblAddressPhone.AutoSize = true;
            this.lblAddressPhone.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAddressPhone.Location = new System.Drawing.Point(105, 139);
            this.lblAddressPhone.Name = "lblAddressPhone";
            this.lblAddressPhone.Size = new System.Drawing.Size(0, 14);
            this.lblAddressPhone.TabIndex = 11;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label25.Location = new System.Drawing.Point(13, 117);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(135, 14);
            this.label25.TabIndex = 62;
            this.label25.Text = "医疗证/医保卡号：";
            // 
            // lblMedicalCardNo
            // 
            this.lblMedicalCardNo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMedicalCardNo.Location = new System.Drawing.Point(157, 116);
            this.lblMedicalCardNo.Name = "lblMedicalCardNo";
            this.lblMedicalCardNo.Size = new System.Drawing.Size(138, 14);
            this.lblMedicalCardNo.TabIndex = 6;
            // 
            // cmbPacsCheckType3
            // 
            this.cmbPacsCheckType3.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbPacsCheckType3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPacsCheckType3.IsEnter2Tab = false;
            this.cmbPacsCheckType3.IsFlat = false;
            this.cmbPacsCheckType3.IsLike = true;
            this.cmbPacsCheckType3.IsListOnly = false;
            this.cmbPacsCheckType3.IsPopForm = true;
            this.cmbPacsCheckType3.IsShowCustomerList = false;
            this.cmbPacsCheckType3.IsShowID = false;
            this.cmbPacsCheckType3.Location = new System.Drawing.Point(504, 816);
            this.cmbPacsCheckType3.Name = "cmbPacsCheckType3";
            this.cmbPacsCheckType3.PopForm = null;
            this.cmbPacsCheckType3.ShowCustomerList = false;
            this.cmbPacsCheckType3.ShowID = false;
            this.cmbPacsCheckType3.Size = new System.Drawing.Size(203, 20);
            this.cmbPacsCheckType3.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbPacsCheckType3.TabIndex = 105;
            this.cmbPacsCheckType3.Tag = "";
            this.cmbPacsCheckType3.ToolBarUse = false;
            this.cmbPacsCheckType3.Visible = false;
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
            this.label11.Text = "卡号:";
            // 
            // treeView1
            // 
            this.treeView1.AllowDrop = true;
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
            this.panel8.Size = new System.Drawing.Size(620, 828);
            this.panel8.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Controls.Add(this.cmbPacsCheckType3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(620, 828);
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
            // ucPacsApplyForClinic
            // 
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel7);
            this.Name = "ucPacsApplyForClinic";
            this.Size = new System.Drawing.Size(800, 828);
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



        private System.ComponentModel.IContainer components;

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public ucPacsApplyForClinic()
        {
            // 该调用是 Windows.Forms 窗体设计器所必需的。
            InitializeComponent();
            this.Load += new EventHandler(ucPacsApply_Load);
            // TODO: 在 InitializeComponent 调用后添加任何初始化

        }

        /// <summary>
        /// 构造函授
        /// </summary>
        /// <param name="item">项目信息</param>
        /// <param name="patientInfo">患者信息</param>
        public ucPacsApplyForClinic(ArrayList Items, FS.HISFC.Models.Registration.Register reg)
        {
            // 该调用是 Windows.Forms 窗体设计器所必需的。
            InitializeComponent();

            this.Load += new EventHandler(ucPacsApply_Load);
            this.reg = reg;
            this.alItems = Items;
        }

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #endregion

        private System.Windows.Forms.PictureBox picbLogo;
        protected FS.FrameWork.WinForms.Controls.NeuPictureBox npbBarCode;
        private System.Windows.Forms.Label label2;
        protected System.Windows.Forms.Label label3;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtItems;

    }
}
