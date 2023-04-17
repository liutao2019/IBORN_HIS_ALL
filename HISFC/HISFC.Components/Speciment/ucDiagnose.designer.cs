namespace FS.HISFC.Components.Speciment
{
    partial class ucDiagnose
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
            this.label16 = new System.Windows.Forms.Label();
            this.txtCure = new FS.FrameWork.WinForms.Controls.NeuListTextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.grpDiag = new System.Windows.Forms.GroupBox();
            this.txtM = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtN = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtT = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbMainDiag = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbMainMod = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.rbtInput = new System.Windows.Forms.RadioButton();
            this.txtM1 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtN1 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtT1 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtP1 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbMainDiag2 = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbDiagMod2 = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbMainDiag1 = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbDiagMod1 = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label15 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.grpDiag1 = new System.Windows.Forms.GroupBox();
            this.grpDiag2 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txtM2 = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtN2 = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.txtT2 = new System.Windows.Forms.TextBox();
            this.txtP2 = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.grpDiag.SuspendLayout();
            this.grpDiag1.SuspendLayout();
            this.grpDiag2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(16, 12);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(88, 16);
            this.label16.TabIndex = 86;
            this.label16.Text = "治疗情况：";
            // 
            // txtCure
            // 
            this.txtCure.EnterVisiable = false;
            this.txtCure.IsFind = false;
            //this.txtCure.IsSelctNone = true;
            //this.txtCure.IsSendToNext = false;
            //this.txtCure.IsShowID = false;
            //this.txtCure.ItemText = "";
            this.txtCure.ListBoxHeight = 100;
            //this.txtCure.ListBoxVisible = false;
            this.txtCure.ListBoxWidth = 100;
            this.txtCure.Location = new System.Drawing.Point(126, 7);
            this.txtCure.Margin = new System.Windows.Forms.Padding(4);
            this.txtCure.Name = "txtCure";
            this.txtCure.OmitFilter = true;
            this.txtCure.SelectedItem = null;
            //this.txtCure.SetListFont = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCure.ShowID = true;
            this.txtCure.Size = new System.Drawing.Size(100, 26);
            this.txtCure.TabIndex = 87;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(11, 31);
            this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(40, 16);
            this.label23.TabIndex = 6;
            this.label23.Text = "诊断";
            // 
            // grpDiag
            // 
            this.grpDiag.Controls.Add(this.txtM);
            this.grpDiag.Controls.Add(this.label10);
            this.grpDiag.Controls.Add(this.txtN);
            this.grpDiag.Controls.Add(this.label3);
            this.grpDiag.Controls.Add(this.txtT);
            this.grpDiag.Controls.Add(this.label2);
            this.grpDiag.Controls.Add(this.txtP);
            this.grpDiag.Controls.Add(this.label1);
            this.grpDiag.Controls.Add(this.cmbMainDiag);
            this.grpDiag.Controls.Add(this.cmbMainMod);
            this.grpDiag.Controls.Add(this.label23);
            this.grpDiag.Controls.Add(this.label4);
            this.grpDiag.Location = new System.Drawing.Point(21, 68);
            this.grpDiag.Margin = new System.Windows.Forms.Padding(4);
            this.grpDiag.Name = "grpDiag";
            this.grpDiag.Padding = new System.Windows.Forms.Padding(4);
            this.grpDiag.Size = new System.Drawing.Size(780, 110);
            this.grpDiag.TabIndex = 90;
            this.grpDiag.TabStop = false;
            this.grpDiag.Text = "主诊断";
            // 
            // txtM
            // 
            this.txtM.Location = new System.Drawing.Point(650, 64);
            this.txtM.MaxLength = 10;
            this.txtM.Name = "txtM";
            this.txtM.Size = new System.Drawing.Size(98, 26);
            this.txtM.TabIndex = 102;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(596, 69);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 16);
            this.label10.TabIndex = 101;
            this.label10.Text = "M分期";
            // 
            // txtN
            // 
            this.txtN.Location = new System.Drawing.Point(495, 64);
            this.txtN.MaxLength = 10;
            this.txtN.Name = "txtN";
            this.txtN.Size = new System.Drawing.Size(74, 26);
            this.txtN.TabIndex = 100;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(432, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 16);
            this.label3.TabIndex = 99;
            this.label3.Text = "N分期";
            // 
            // txtT
            // 
            this.txtT.Location = new System.Drawing.Point(258, 64);
            this.txtT.MaxLength = 10;
            this.txtT.Name = "txtT";
            this.txtT.Size = new System.Drawing.Size(73, 26);
            this.txtT.TabIndex = 98;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(197, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 97;
            this.label2.Text = "T分期";
            // 
            // txtP
            // 
            this.txtP.Location = new System.Drawing.Point(78, 64);
            this.txtP.MaxLength = 10;
            this.txtP.Name = "txtP";
            this.txtP.Size = new System.Drawing.Size(79, 26);
            this.txtP.TabIndex = 96;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 95;
            this.label1.Text = "P分期";
            // 
            // cmbMainDiag
            // 
            //this.cmbMainDiag.A = false;
            this.cmbMainDiag.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbMainDiag.FormattingEnabled = true;
            this.cmbMainDiag.IsFlat = true;
            this.cmbMainDiag.IsLike = true;
            this.cmbMainDiag.Location = new System.Drawing.Point(78, 27);
            this.cmbMainDiag.Margin = new System.Windows.Forms.Padding(4);
            this.cmbMainDiag.Name = "cmbMainDiag";
            this.cmbMainDiag.PopForm = null;
            this.cmbMainDiag.ShowCustomerList = false;
            this.cmbMainDiag.ShowID = false;
            this.cmbMainDiag.Size = new System.Drawing.Size(253, 24);
            this.cmbMainDiag.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbMainDiag.TabIndex = 92;
            this.cmbMainDiag.Tag = "";
            this.cmbMainDiag.ToolBarUse = false;
            // 
            // cmbMainMod
            // 
            //this.cmbMainMod.A = false;
            this.cmbMainMod.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbMainMod.FormattingEnabled = true;
            this.cmbMainMod.IsFlat = true;
            this.cmbMainMod.IsLike = true;
            this.cmbMainMod.Location = new System.Drawing.Point(495, 27);
            this.cmbMainMod.Margin = new System.Windows.Forms.Padding(4);
            this.cmbMainMod.Name = "cmbMainMod";
            this.cmbMainMod.PopForm = null;
            this.cmbMainMod.ShowCustomerList = false;
            this.cmbMainMod.ShowID = false;
            this.cmbMainMod.Size = new System.Drawing.Size(253, 24);
            this.cmbMainMod.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbMainMod.TabIndex = 92;
            this.cmbMainMod.Tag = "";
            this.cmbMainMod.ToolBarUse = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(427, 31);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "形态码";
            // 
            // rbtInput
            // 
            this.rbtInput.AutoSize = true;
            this.rbtInput.Location = new System.Drawing.Point(21, 40);
            this.rbtInput.Margin = new System.Windows.Forms.Padding(4);
            this.rbtInput.Name = "rbtInput";
            this.rbtInput.Size = new System.Drawing.Size(58, 20);
            this.rbtInput.TabIndex = 91;
            this.rbtInput.Text = "录入";
            this.rbtInput.UseVisualStyleBackColor = true;
            this.rbtInput.CheckedChanged += new System.EventHandler(this.rbtInput_CheckedChanged);
            // 
            // txtM1
            // 
            this.txtM1.Location = new System.Drawing.Point(652, 63);
            this.txtM1.MaxLength = 10;
            this.txtM1.Name = "txtM1";
            this.txtM1.Size = new System.Drawing.Size(98, 26);
            this.txtM1.TabIndex = 118;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(598, 68);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(48, 16);
            this.label14.TabIndex = 117;
            this.label14.Text = "M分期";
            // 
            // txtN1
            // 
            this.txtN1.Location = new System.Drawing.Point(497, 63);
            this.txtN1.MaxLength = 10;
            this.txtN1.Name = "txtN1";
            this.txtN1.Size = new System.Drawing.Size(74, 26);
            this.txtN1.TabIndex = 116;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(434, 68);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(48, 16);
            this.label13.TabIndex = 115;
            this.label13.Text = "N分期";
            // 
            // txtT1
            // 
            this.txtT1.Location = new System.Drawing.Point(261, 63);
            this.txtT1.MaxLength = 10;
            this.txtT1.Name = "txtT1";
            this.txtT1.Size = new System.Drawing.Size(73, 26);
            this.txtT1.TabIndex = 114;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(207, 68);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(48, 16);
            this.label12.TabIndex = 113;
            this.label12.Text = "T分期";
            // 
            // txtP1
            // 
            this.txtP1.Location = new System.Drawing.Point(80, 63);
            this.txtP1.MaxLength = 10;
            this.txtP1.Name = "txtP1";
            this.txtP1.Size = new System.Drawing.Size(79, 26);
            this.txtP1.TabIndex = 112;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(20, 68);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(48, 16);
            this.label11.TabIndex = 111;
            this.label11.Text = "P分期";
            // 
            // cmbMainDiag2
            // 
            //this.cmbMainDiag2.A = false;
            this.cmbMainDiag2.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbMainDiag2.FormattingEnabled = true;
            this.cmbMainDiag2.IsFlat = true;
            this.cmbMainDiag2.IsLike = true;
            this.cmbMainDiag2.Location = new System.Drawing.Point(80, 28);
            this.cmbMainDiag2.Margin = new System.Windows.Forms.Padding(4);
            this.cmbMainDiag2.Name = "cmbMainDiag2";
            this.cmbMainDiag2.PopForm = null;
            this.cmbMainDiag2.ShowCustomerList = false;
            this.cmbMainDiag2.ShowID = false;
            this.cmbMainDiag2.Size = new System.Drawing.Size(253, 24);
            this.cmbMainDiag2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbMainDiag2.TabIndex = 109;
            this.cmbMainDiag2.Tag = "";
            this.cmbMainDiag2.ToolBarUse = false;
            // 
            // cmbDiagMod2
            //
            this.cmbDiagMod2.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbDiagMod2.FormattingEnabled = true;
            this.cmbDiagMod2.IsFlat = true;
            this.cmbDiagMod2.IsLike = true;
            this.cmbDiagMod2.Location = new System.Drawing.Point(497, 28);
            this.cmbDiagMod2.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDiagMod2.Name = "cmbDiagMod2";
            this.cmbDiagMod2.PopForm = null;
            this.cmbDiagMod2.ShowCustomerList = false;
            this.cmbDiagMod2.ShowID = false;
            this.cmbDiagMod2.Size = new System.Drawing.Size(253, 24);
            this.cmbDiagMod2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbDiagMod2.TabIndex = 110;
            this.cmbDiagMod2.Tag = "";
            this.cmbDiagMod2.ToolBarUse = false;
            // 
            // cmbMainDiag1
            // 
            this.cmbMainDiag1.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbMainDiag1.FormattingEnabled = true;
            this.cmbMainDiag1.IsFlat = true;
            this.cmbMainDiag1.IsLike = true;
            this.cmbMainDiag1.Location = new System.Drawing.Point(80, 26);
            this.cmbMainDiag1.Margin = new System.Windows.Forms.Padding(4);
            this.cmbMainDiag1.Name = "cmbMainDiag1";
            this.cmbMainDiag1.PopForm = null;
            this.cmbMainDiag1.ShowCustomerList = false;
            this.cmbMainDiag1.ShowID = false;
            this.cmbMainDiag1.Size = new System.Drawing.Size(253, 24);
            this.cmbMainDiag1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbMainDiag1.TabIndex = 107;
            this.cmbMainDiag1.Tag = "";
            this.cmbMainDiag1.ToolBarUse = false;
            // 
            // cmbDiagMod1
            // 
            this.cmbDiagMod1.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbDiagMod1.FormattingEnabled = true;
            this.cmbDiagMod1.IsFlat = true;
            this.cmbDiagMod1.IsLike = true;
            this.cmbDiagMod1.Location = new System.Drawing.Point(497, 23);
            this.cmbDiagMod1.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDiagMod1.Name = "cmbDiagMod1";
            this.cmbDiagMod1.PopForm = null;
            this.cmbDiagMod1.ShowCustomerList = false;
            this.cmbDiagMod1.ShowID = false;
            this.cmbDiagMod1.Size = new System.Drawing.Size(253, 24);
            this.cmbDiagMod1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbDiagMod1.TabIndex = 108;
            this.cmbDiagMod1.Tag = "";
            this.cmbDiagMod1.ToolBarUse = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(17, 31);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(40, 16);
            this.label15.TabIndex = 105;
            this.label15.Text = "诊断";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(429, 31);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 16);
            this.label5.TabIndex = 106;
            this.label5.Text = "形态码";
            // 
            // grpDiag1
            // 
            this.grpDiag1.Controls.Add(this.label15);
            this.grpDiag1.Controls.Add(this.txtM1);
            this.grpDiag1.Controls.Add(this.label5);
            this.grpDiag1.Controls.Add(this.label14);
            this.grpDiag1.Controls.Add(this.cmbDiagMod1);
            this.grpDiag1.Controls.Add(this.txtN1);
            this.grpDiag1.Controls.Add(this.cmbMainDiag1);
            this.grpDiag1.Controls.Add(this.label13);
            this.grpDiag1.Controls.Add(this.label11);
            this.grpDiag1.Controls.Add(this.txtT1);
            this.grpDiag1.Controls.Add(this.txtP1);
            this.grpDiag1.Controls.Add(this.label12);
            this.grpDiag1.Location = new System.Drawing.Point(19, 192);
            this.grpDiag1.Name = "grpDiag1";
            this.grpDiag1.Size = new System.Drawing.Size(782, 100);
            this.grpDiag1.TabIndex = 119;
            this.grpDiag1.TabStop = false;
            this.grpDiag1.Text = "其他诊断1";
            // 
            // grpDiag2
            // 
            this.grpDiag2.Controls.Add(this.label9);
            this.grpDiag2.Controls.Add(this.label17);
            this.grpDiag2.Controls.Add(this.txtM2);
            this.grpDiag2.Controls.Add(this.label19);
            this.grpDiag2.Controls.Add(this.cmbMainDiag2);
            this.grpDiag2.Controls.Add(this.txtN2);
            this.grpDiag2.Controls.Add(this.cmbDiagMod2);
            this.grpDiag2.Controls.Add(this.label20);
            this.grpDiag2.Controls.Add(this.label21);
            this.grpDiag2.Controls.Add(this.txtT2);
            this.grpDiag2.Controls.Add(this.txtP2);
            this.grpDiag2.Controls.Add(this.label22);
            this.grpDiag2.Location = new System.Drawing.Point(19, 307);
            this.grpDiag2.Name = "grpDiag2";
            this.grpDiag2.Size = new System.Drawing.Size(782, 103);
            this.grpDiag2.TabIndex = 120;
            this.grpDiag2.TabStop = false;
            this.grpDiag2.Text = "其他诊断2";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(17, 31);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 16);
            this.label9.TabIndex = 119;
            this.label9.Text = "诊断";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(429, 31);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(56, 16);
            this.label17.TabIndex = 120;
            this.label17.Text = "形态码";
            // 
            // txtM2
            // 
            this.txtM2.Location = new System.Drawing.Point(652, 65);
            this.txtM2.MaxLength = 10;
            this.txtM2.Name = "txtM2";
            this.txtM2.Size = new System.Drawing.Size(98, 26);
            this.txtM2.TabIndex = 118;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(598, 70);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(48, 16);
            this.label19.TabIndex = 117;
            this.label19.Text = "M分期";
            // 
            // txtN2
            // 
            this.txtN2.Location = new System.Drawing.Point(497, 67);
            this.txtN2.MaxLength = 10;
            this.txtN2.Name = "txtN2";
            this.txtN2.Size = new System.Drawing.Size(74, 26);
            this.txtN2.TabIndex = 116;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(437, 70);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(48, 16);
            this.label20.TabIndex = 115;
            this.label20.Text = "N分期";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(17, 70);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(48, 16);
            this.label21.TabIndex = 111;
            this.label21.Text = "P分期";
            // 
            // txtT2
            // 
            this.txtT2.Location = new System.Drawing.Point(261, 65);
            this.txtT2.MaxLength = 10;
            this.txtT2.Name = "txtT2";
            this.txtT2.Size = new System.Drawing.Size(73, 26);
            this.txtT2.TabIndex = 114;
            // 
            // txtP2
            // 
            this.txtP2.Location = new System.Drawing.Point(80, 67);
            this.txtP2.MaxLength = 10;
            this.txtP2.Name = "txtP2";
            this.txtP2.Size = new System.Drawing.Size(79, 26);
            this.txtP2.TabIndex = 112;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(207, 70);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(48, 16);
            this.label22.TabIndex = 113;
            this.label22.Text = "T分期";
            // 
            // ucDiagnose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpDiag2);
            this.Controls.Add(this.grpDiag1);
            this.Controls.Add(this.rbtInput);
            this.Controls.Add(this.grpDiag);
            this.Controls.Add(this.txtCure);
            this.Controls.Add(this.label16);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ucDiagnose";
            this.Size = new System.Drawing.Size(805, 416);
            this.Load += new System.EventHandler(this.ucDiagnose_Load);
            this.grpDiag.ResumeLayout(false);
            this.grpDiag.PerformLayout();
            this.grpDiag1.ResumeLayout(false);
            this.grpDiag1.PerformLayout();
            this.grpDiag2.ResumeLayout(false);
            this.grpDiag2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label16;
        private FS.FrameWork.WinForms.Controls.NeuListTextBox txtCure;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.GroupBox grpDiag;
        private System.Windows.Forms.RadioButton rbtInput;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbMainDiag;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbMainMod;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtM;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtN;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtT;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtM1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtN1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtT1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtP1;
        private System.Windows.Forms.Label label11;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbMainDiag2;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDiagMod2;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbMainDiag1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDiagMod1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox grpDiag1;
        private System.Windows.Forms.GroupBox grpDiag2;
        private System.Windows.Forms.TextBox txtM2;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtN2;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtT2;
        private System.Windows.Forms.TextBox txtP2;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label17;
    }
}
