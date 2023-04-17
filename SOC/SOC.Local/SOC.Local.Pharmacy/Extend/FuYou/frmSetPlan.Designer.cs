namespace FS.SOC.Local.Pharmacy.Extend.FuYou
{
    partial class frmSetPlan
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.neuTabControl1 = new FS.FrameWork.WinForms.Controls.NeuTabControl();
            this.tpConsume = new System.Windows.Forms.TabPage();
            this.neuGroupBox3 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuLabel12 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel11 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel10 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel9 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel8 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuGroupBox2 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.ncmbDrugStencilConsume = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.ncmbDrugTypeConsume = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.ndtpEndTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel7 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ntxtUpDays = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ndtpBeginTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.ntxtLowDays = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tpWarning = new System.Windows.Forms.TabPage();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel13 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuGroupBox4 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.tpFormula = new System.Windows.Forms.TabPage();
            this.neuLabel14 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nbtOK = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.nbtCancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuLabel15 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel16 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel17 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel18 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ncmbDrugStencilWarning = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.ncmbDrugTypeWarning = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuTabControl1.SuspendLayout();
            this.tpConsume.SuspendLayout();
            this.neuGroupBox3.SuspendLayout();
            this.neuGroupBox2.SuspendLayout();
            this.tpWarning.SuspendLayout();
            this.neuGroupBox1.SuspendLayout();
            this.neuGroupBox4.SuspendLayout();
            this.tpFormula.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuTabControl1
            // 
            this.neuTabControl1.Controls.Add(this.tpConsume);
            this.neuTabControl1.Controls.Add(this.tpWarning);
            this.neuTabControl1.Controls.Add(this.tpFormula);
            this.neuTabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuTabControl1.Location = new System.Drawing.Point(0, 0);
            this.neuTabControl1.Name = "neuTabControl1";
            this.neuTabControl1.SelectedIndex = 0;
            this.neuTabControl1.Size = new System.Drawing.Size(473, 427);
            this.neuTabControl1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTabControl1.TabIndex = 0;
            // 
            // tpConsume
            // 
            this.tpConsume.Controls.Add(this.neuGroupBox3);
            this.tpConsume.Controls.Add(this.neuGroupBox2);
            this.tpConsume.Location = new System.Drawing.Point(4, 21);
            this.tpConsume.Name = "tpConsume";
            this.tpConsume.Size = new System.Drawing.Size(465, 402);
            this.tpConsume.TabIndex = 2;
            this.tpConsume.Text = "日消耗";
            this.tpConsume.UseVisualStyleBackColor = true;
            // 
            // neuGroupBox3
            // 
            this.neuGroupBox3.Controls.Add(this.neuLabel12);
            this.neuGroupBox3.Controls.Add(this.neuLabel11);
            this.neuGroupBox3.Controls.Add(this.neuLabel10);
            this.neuGroupBox3.Controls.Add(this.neuLabel9);
            this.neuGroupBox3.Controls.Add(this.neuLabel8);
            this.neuGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuGroupBox3.Location = new System.Drawing.Point(0, 227);
            this.neuGroupBox3.Name = "neuGroupBox3";
            this.neuGroupBox3.Size = new System.Drawing.Size(465, 175);
            this.neuGroupBox3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox3.TabIndex = 3;
            this.neuGroupBox3.TabStop = false;
            this.neuGroupBox3.Text = "说明";
            // 
            // neuLabel12
            // 
            this.neuLabel12.AutoSize = true;
            this.neuLabel12.Location = new System.Drawing.Point(40, 129);
            this.neuLabel12.Name = "neuLabel12";
            this.neuLabel12.Size = new System.Drawing.Size(173, 12);
            this.neuLabel12.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel12.TabIndex = 6;
            this.neuLabel12.Text = "计划量 = 上限量 - 当前库存量";
            // 
            // neuLabel11
            // 
            this.neuLabel11.AutoSize = true;
            this.neuLabel11.Location = new System.Drawing.Point(223, 129);
            this.neuLabel11.Name = "neuLabel11";
            this.neuLabel11.Size = new System.Drawing.Size(149, 12);
            this.neuLabel11.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel11.TabIndex = 5;
            this.neuLabel11.Text = "(当本科库存量小于下限时)";
            // 
            // neuLabel10
            // 
            this.neuLabel10.AutoSize = true;
            this.neuLabel10.Location = new System.Drawing.Point(40, 97);
            this.neuLabel10.Name = "neuLabel10";
            this.neuLabel10.Size = new System.Drawing.Size(161, 12);
            this.neuLabel10.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel10.TabIndex = 4;
            this.neuLabel10.Text = "上限量 = 日消耗 * 上限天数";
            // 
            // neuLabel9
            // 
            this.neuLabel9.AutoSize = true;
            this.neuLabel9.Location = new System.Drawing.Point(40, 66);
            this.neuLabel9.Name = "neuLabel9";
            this.neuLabel9.Size = new System.Drawing.Size(161, 12);
            this.neuLabel9.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel9.TabIndex = 3;
            this.neuLabel9.Text = "下限量 = 日消耗 * 下限天数";
            // 
            // neuLabel8
            // 
            this.neuLabel8.AutoSize = true;
            this.neuLabel8.Location = new System.Drawing.Point(40, 34);
            this.neuLabel8.Name = "neuLabel8";
            this.neuLabel8.Size = new System.Drawing.Size(233, 12);
            this.neuLabel8.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel8.TabIndex = 2;
            this.neuLabel8.Text = "日消耗 = 消耗日期内消耗总量 / 消耗天数";
            // 
            // neuGroupBox2
            // 
            this.neuGroupBox2.Controls.Add(this.neuLabel16);
            this.neuGroupBox2.Controls.Add(this.neuLabel15);
            this.neuGroupBox2.Controls.Add(this.ncmbDrugStencilConsume);
            this.neuGroupBox2.Controls.Add(this.ncmbDrugTypeConsume);
            this.neuGroupBox2.Controls.Add(this.ndtpEndTime);
            this.neuGroupBox2.Controls.Add(this.neuLabel7);
            this.neuGroupBox2.Controls.Add(this.neuLabel4);
            this.neuGroupBox2.Controls.Add(this.ntxtUpDays);
            this.neuGroupBox2.Controls.Add(this.neuLabel5);
            this.neuGroupBox2.Controls.Add(this.ndtpBeginTime);
            this.neuGroupBox2.Controls.Add(this.ntxtLowDays);
            this.neuGroupBox2.Controls.Add(this.neuLabel6);
            this.neuGroupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox2.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox2.Name = "neuGroupBox2";
            this.neuGroupBox2.Size = new System.Drawing.Size(465, 227);
            this.neuGroupBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox2.TabIndex = 1;
            this.neuGroupBox2.TabStop = false;
            this.neuGroupBox2.Text = "设置";
            // 
            // ncmbDrugStencilConsume
            // 
            this.ncmbDrugStencilConsume.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.ncmbDrugStencilConsume.FormattingEnabled = true;
            this.ncmbDrugStencilConsume.IsEnter2Tab = false;
            this.ncmbDrugStencilConsume.IsFlat = false;
            this.ncmbDrugStencilConsume.IsLike = true;
            this.ncmbDrugStencilConsume.IsListOnly = false;
            this.ncmbDrugStencilConsume.IsPopForm = true;
            this.ncmbDrugStencilConsume.IsShowCustomerList = false;
            this.ncmbDrugStencilConsume.IsShowID = false;
            this.ncmbDrugStencilConsume.Location = new System.Drawing.Point(110, 183);
            this.ncmbDrugStencilConsume.Name = "ncmbDrugStencilConsume";
            this.ncmbDrugStencilConsume.PopForm = null;
            this.ncmbDrugStencilConsume.ShowCustomerList = false;
            this.ncmbDrugStencilConsume.ShowID = false;
            this.ncmbDrugStencilConsume.Size = new System.Drawing.Size(121, 20);
            this.ncmbDrugStencilConsume.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.ncmbDrugStencilConsume.TabIndex = 11;
            this.ncmbDrugStencilConsume.Tag = "";
            this.ncmbDrugStencilConsume.ToolBarUse = false;
            // 
            // ncmbDrugTypeConsume
            // 
            this.ncmbDrugTypeConsume.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.ncmbDrugTypeConsume.FormattingEnabled = true;
            this.ncmbDrugTypeConsume.IsEnter2Tab = false;
            this.ncmbDrugTypeConsume.IsFlat = false;
            this.ncmbDrugTypeConsume.IsLike = true;
            this.ncmbDrugTypeConsume.IsListOnly = false;
            this.ncmbDrugTypeConsume.IsPopForm = true;
            this.ncmbDrugTypeConsume.IsShowCustomerList = false;
            this.ncmbDrugTypeConsume.IsShowID = false;
            this.ncmbDrugTypeConsume.Location = new System.Drawing.Point(110, 145);
            this.ncmbDrugTypeConsume.Name = "ncmbDrugTypeConsume";
            this.ncmbDrugTypeConsume.PopForm = null;
            this.ncmbDrugTypeConsume.ShowCustomerList = false;
            this.ncmbDrugTypeConsume.ShowID = false;
            this.ncmbDrugTypeConsume.Size = new System.Drawing.Size(121, 20);
            this.ncmbDrugTypeConsume.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.ncmbDrugTypeConsume.TabIndex = 9;
            this.ncmbDrugTypeConsume.Tag = "";
            this.ncmbDrugTypeConsume.ToolBarUse = false;
            // 
            // ndtpEndTime
            // 
            this.ndtpEndTime.IsEnter2Tab = false;
            this.ndtpEndTime.Location = new System.Drawing.Point(265, 28);
            this.ndtpEndTime.Name = "ndtpEndTime";
            this.ndtpEndTime.Size = new System.Drawing.Size(121, 21);
            this.ndtpEndTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ndtpEndTime.TabIndex = 3;
            // 
            // neuLabel7
            // 
            this.neuLabel7.AutoSize = true;
            this.neuLabel7.Location = new System.Drawing.Point(237, 32);
            this.neuLabel7.Name = "neuLabel7";
            this.neuLabel7.Size = new System.Drawing.Size(29, 12);
            this.neuLabel7.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel7.TabIndex = 7;
            this.neuLabel7.Text = "到：";
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point(41, 107);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(65, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 6;
            this.neuLabel4.Text = "上限天数：";
            // 
            // ntxtUpDays
            // 
            this.ntxtUpDays.IsEnter2Tab = true;
            this.ntxtUpDays.Location = new System.Drawing.Point(110, 103);
            this.ntxtUpDays.Name = "ntxtUpDays";
            this.ntxtUpDays.Size = new System.Drawing.Size(121, 21);
            this.ntxtUpDays.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ntxtUpDays.TabIndex = 5;
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Location = new System.Drawing.Point(41, 70);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(65, 12);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 4;
            this.neuLabel5.Text = "下限天数：";
            // 
            // ndtpBeginTime
            // 
            this.ndtpBeginTime.IsEnter2Tab = false;
            this.ndtpBeginTime.Location = new System.Drawing.Point(110, 28);
            this.ndtpBeginTime.Name = "ndtpBeginTime";
            this.ndtpBeginTime.Size = new System.Drawing.Size(121, 21);
            this.ndtpBeginTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ndtpBeginTime.TabIndex = 2;
            // 
            // ntxtLowDays
            // 
            this.ntxtLowDays.IsEnter2Tab = true;
            this.ntxtLowDays.Location = new System.Drawing.Point(110, 66);
            this.ntxtLowDays.Name = "ntxtLowDays";
            this.ntxtLowDays.Size = new System.Drawing.Size(121, 21);
            this.ntxtLowDays.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ntxtLowDays.TabIndex = 4;
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.Location = new System.Drawing.Point(41, 32);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(65, 12);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 0;
            this.neuLabel6.Text = "消耗日期：";
            // 
            // tpWarning
            // 
            this.tpWarning.Controls.Add(this.neuGroupBox1);
            this.tpWarning.Controls.Add(this.neuGroupBox4);
            this.tpWarning.Location = new System.Drawing.Point(4, 21);
            this.tpWarning.Name = "tpWarning";
            this.tpWarning.Padding = new System.Windows.Forms.Padding(3);
            this.tpWarning.Size = new System.Drawing.Size(465, 402);
            this.tpWarning.TabIndex = 0;
            this.tpWarning.Text = "警戒线";
            this.tpWarning.UseVisualStyleBackColor = true;
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.neuLabel1);
            this.neuGroupBox1.Controls.Add(this.neuLabel2);
            this.neuGroupBox1.Controls.Add(this.neuLabel3);
            this.neuGroupBox1.Controls.Add(this.neuLabel13);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuGroupBox1.Location = new System.Drawing.Point(3, 119);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(459, 280);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 7;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "说明";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(40, 129);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(173, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 6;
            this.neuLabel1.Text = "计划量 = 上限量 - 当前库存量";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(222, 129);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(149, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 5;
            this.neuLabel2.Text = "(当本科库存量小于下限时)";
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(40, 83);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(215, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 4;
            this.neuLabel3.Text = "上限量 = 最高库存(库存管理界面维护)";
            // 
            // neuLabel13
            // 
            this.neuLabel13.AutoSize = true;
            this.neuLabel13.Location = new System.Drawing.Point(40, 37);
            this.neuLabel13.Name = "neuLabel13";
            this.neuLabel13.Size = new System.Drawing.Size(215, 12);
            this.neuLabel13.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel13.TabIndex = 3;
            this.neuLabel13.Text = "下限量 = 最低库存(库存管理界面维护)";
            // 
            // neuGroupBox4
            // 
            this.neuGroupBox4.Controls.Add(this.neuLabel17);
            this.neuGroupBox4.Controls.Add(this.neuLabel18);
            this.neuGroupBox4.Controls.Add(this.ncmbDrugStencilWarning);
            this.neuGroupBox4.Controls.Add(this.ncmbDrugTypeWarning);
            this.neuGroupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox4.Location = new System.Drawing.Point(3, 3);
            this.neuGroupBox4.Name = "neuGroupBox4";
            this.neuGroupBox4.Size = new System.Drawing.Size(459, 116);
            this.neuGroupBox4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox4.TabIndex = 6;
            this.neuGroupBox4.TabStop = false;
            this.neuGroupBox4.Text = "设置";
            // 
            // tpFormula
            // 
            this.tpFormula.Controls.Add(this.neuLabel14);
            this.tpFormula.Location = new System.Drawing.Point(4, 21);
            this.tpFormula.Name = "tpFormula";
            this.tpFormula.Padding = new System.Windows.Forms.Padding(3);
            this.tpFormula.Size = new System.Drawing.Size(465, 402);
            this.tpFormula.TabIndex = 1;
            this.tpFormula.Text = "自定义公式";
            this.tpFormula.UseVisualStyleBackColor = true;
            // 
            // neuLabel14
            // 
            this.neuLabel14.AutoSize = true;
            this.neuLabel14.Location = new System.Drawing.Point(116, 170);
            this.neuLabel14.Name = "neuLabel14";
            this.neuLabel14.Size = new System.Drawing.Size(233, 12);
            this.neuLabel14.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel14.TabIndex = 0;
            this.neuLabel14.Text = "智能计划管理功能，请与系统供应商联系！";
            // 
            // nbtOK
            // 
            this.nbtOK.Location = new System.Drawing.Point(217, 438);
            this.nbtOK.Name = "nbtOK";
            this.nbtOK.Size = new System.Drawing.Size(75, 23);
            this.nbtOK.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nbtOK.TabIndex = 6;
            this.nbtOK.Text = "确定";
            this.nbtOK.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.nbtOK.UseVisualStyleBackColor = true;
            // 
            // nbtCancel
            // 
            this.nbtCancel.Location = new System.Drawing.Point(333, 438);
            this.nbtCancel.Name = "nbtCancel";
            this.nbtCancel.Size = new System.Drawing.Size(75, 23);
            this.nbtCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nbtCancel.TabIndex = 7;
            this.nbtCancel.Text = "取消";
            this.nbtCancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.nbtCancel.UseVisualStyleBackColor = true;
            // 
            // neuLabel15
            // 
            this.neuLabel15.AutoSize = true;
            this.neuLabel15.Location = new System.Drawing.Point(41, 149);
            this.neuLabel15.Name = "neuLabel15";
            this.neuLabel15.Size = new System.Drawing.Size(65, 12);
            this.neuLabel15.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel15.TabIndex = 12;
            this.neuLabel15.Text = "药品类别：";
            // 
            // neuLabel16
            // 
            this.neuLabel16.AutoSize = true;
            this.neuLabel16.Location = new System.Drawing.Point(41, 187);
            this.neuLabel16.Name = "neuLabel16";
            this.neuLabel16.Size = new System.Drawing.Size(65, 12);
            this.neuLabel16.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel16.TabIndex = 13;
            this.neuLabel16.Text = "计划模板：";
            // 
            // neuLabel17
            // 
            this.neuLabel17.AutoSize = true;
            this.neuLabel17.Location = new System.Drawing.Point(40, 78);
            this.neuLabel17.Name = "neuLabel17";
            this.neuLabel17.Size = new System.Drawing.Size(65, 12);
            this.neuLabel17.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel17.TabIndex = 17;
            this.neuLabel17.Text = "计划模板：";
            // 
            // neuLabel18
            // 
            this.neuLabel18.AutoSize = true;
            this.neuLabel18.Location = new System.Drawing.Point(40, 40);
            this.neuLabel18.Name = "neuLabel18";
            this.neuLabel18.Size = new System.Drawing.Size(65, 12);
            this.neuLabel18.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel18.TabIndex = 16;
            this.neuLabel18.Text = "药品类别：";
            // 
            // ncmbDrugStencilWarning
            // 
            this.ncmbDrugStencilWarning.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.ncmbDrugStencilWarning.FormattingEnabled = true;
            this.ncmbDrugStencilWarning.IsEnter2Tab = false;
            this.ncmbDrugStencilWarning.IsFlat = false;
            this.ncmbDrugStencilWarning.IsLike = true;
            this.ncmbDrugStencilWarning.IsListOnly = false;
            this.ncmbDrugStencilWarning.IsPopForm = true;
            this.ncmbDrugStencilWarning.IsShowCustomerList = false;
            this.ncmbDrugStencilWarning.IsShowID = false;
            this.ncmbDrugStencilWarning.Location = new System.Drawing.Point(109, 74);
            this.ncmbDrugStencilWarning.Name = "ncmbDrugStencilWarning";
            this.ncmbDrugStencilWarning.PopForm = null;
            this.ncmbDrugStencilWarning.ShowCustomerList = false;
            this.ncmbDrugStencilWarning.ShowID = false;
            this.ncmbDrugStencilWarning.Size = new System.Drawing.Size(121, 20);
            this.ncmbDrugStencilWarning.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.ncmbDrugStencilWarning.TabIndex = 15;
            this.ncmbDrugStencilWarning.Tag = "";
            this.ncmbDrugStencilWarning.ToolBarUse = false;
            // 
            // ncmbDrugTypeWarning
            // 
            this.ncmbDrugTypeWarning.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.ncmbDrugTypeWarning.FormattingEnabled = true;
            this.ncmbDrugTypeWarning.IsEnter2Tab = false;
            this.ncmbDrugTypeWarning.IsFlat = false;
            this.ncmbDrugTypeWarning.IsLike = true;
            this.ncmbDrugTypeWarning.IsListOnly = false;
            this.ncmbDrugTypeWarning.IsPopForm = true;
            this.ncmbDrugTypeWarning.IsShowCustomerList = false;
            this.ncmbDrugTypeWarning.IsShowID = false;
            this.ncmbDrugTypeWarning.Location = new System.Drawing.Point(109, 36);
            this.ncmbDrugTypeWarning.Name = "ncmbDrugTypeWarning";
            this.ncmbDrugTypeWarning.PopForm = null;
            this.ncmbDrugTypeWarning.ShowCustomerList = false;
            this.ncmbDrugTypeWarning.ShowID = false;
            this.ncmbDrugTypeWarning.Size = new System.Drawing.Size(121, 20);
            this.ncmbDrugTypeWarning.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.ncmbDrugTypeWarning.TabIndex = 14;
            this.ncmbDrugTypeWarning.Tag = "";
            this.ncmbDrugTypeWarning.ToolBarUse = false;
            // 
            // frmSetPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 472);
            this.ControlBox = false;
            this.Controls.Add(this.nbtCancel);
            this.Controls.Add(this.nbtOK);
            this.Controls.Add(this.neuTabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmSetPlan";
            this.Text = "计划生成设置";
            this.neuTabControl1.ResumeLayout(false);
            this.tpConsume.ResumeLayout(false);
            this.neuGroupBox3.ResumeLayout(false);
            this.neuGroupBox3.PerformLayout();
            this.neuGroupBox2.ResumeLayout(false);
            this.neuGroupBox2.PerformLayout();
            this.tpWarning.ResumeLayout(false);
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.neuGroupBox4.ResumeLayout(false);
            this.neuGroupBox4.PerformLayout();
            this.tpFormula.ResumeLayout(false);
            this.tpFormula.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuTabControl neuTabControl1;
        private System.Windows.Forms.TabPage tpFormula;
        private System.Windows.Forms.TabPage tpConsume;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuTextBox ntxtUpDays;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker ndtpEndTime;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker ndtpBeginTime;
        private FS.FrameWork.WinForms.Controls.NeuTextBox ntxtLowDays;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        private System.Windows.Forms.TabPage tpWarning;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel7;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel8;
        private FS.FrameWork.WinForms.Controls.NeuButton nbtOK;
        private FS.FrameWork.WinForms.Controls.NeuButton nbtCancel;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox3;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel10;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel9;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel12;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel11;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel14;
        private FS.FrameWork.WinForms.Controls.NeuComboBox ncmbDrugStencilConsume;
        private FS.FrameWork.WinForms.Controls.NeuComboBox ncmbDrugTypeConsume;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel13;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox4;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel16;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel15;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel17;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel18;
        private FS.FrameWork.WinForms.Controls.NeuComboBox ncmbDrugStencilWarning;
        private FS.FrameWork.WinForms.Controls.NeuComboBox ncmbDrugTypeWarning;
    }
}