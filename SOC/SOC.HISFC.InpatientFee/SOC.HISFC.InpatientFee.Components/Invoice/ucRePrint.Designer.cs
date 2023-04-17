namespace FS.SOC.HISFC.InpatientFee.Components.Invoice
{
    partial class ucRePrint
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
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.TipAppearance tipAppearance3 = new FarPoint.Win.Spread.TipAppearance();
            this.gbPatientNo = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.lblNextInvoiceNO = new System.Windows.Forms.Label();
            this.ucQueryInpatientNo1 = new FS.HISFC.Components.Common.Controls.ucQueryInpatientNo();
            this.txtInvoice = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblInvoice = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.gbPatientInfo = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.textBox1 = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.txtDateOut = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBirthday = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblBirthday = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtBedNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblBedNo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblPact = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtPact = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtDoctor = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblDoctor = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblDept = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtDept = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblDateIn = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblNurceCell = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtDateIn = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtNurseStation = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.gbInvoices = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.fpBalanceInvoice = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpBalanceInvoice_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.pnlRight = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.fpPrepay = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpPrepay_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.pnlLeft = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.fpBalance = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpBalance_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.gbPatientNo.SuspendLayout();
            this.gbPatientInfo.SuspendLayout();
            this.gbInvoices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpBalanceInvoice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpBalanceInvoice_Sheet1)).BeginInit();
            this.neuPanel1.SuspendLayout();
            this.pnlRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpPrepay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPrepay_Sheet1)).BeginInit();
            this.pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpBalance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpBalance_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // gbPatientNo
            // 
            this.gbPatientNo.BackColor = System.Drawing.Color.Transparent;
            this.gbPatientNo.Controls.Add(this.lblNextInvoiceNO);
            this.gbPatientNo.Controls.Add(this.ucQueryInpatientNo1);
            this.gbPatientNo.Controls.Add(this.txtInvoice);
            this.gbPatientNo.Controls.Add(this.lblInvoice);
            this.gbPatientNo.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbPatientNo.Location = new System.Drawing.Point(0, 0);
            this.gbPatientNo.Name = "gbPatientNo";
            this.gbPatientNo.Size = new System.Drawing.Size(822, 60);
            this.gbPatientNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbPatientNo.TabIndex = 2;
            this.gbPatientNo.TabStop = false;
            // 
            // lblNextInvoiceNO
            // 
            this.lblNextInvoiceNO.AutoSize = true;
            this.lblNextInvoiceNO.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNextInvoiceNO.ForeColor = System.Drawing.Color.Red;
            this.lblNextInvoiceNO.Location = new System.Drawing.Point(502, 23);
            this.lblNextInvoiceNO.Name = "lblNextInvoiceNO";
            this.lblNextInvoiceNO.Size = new System.Drawing.Size(42, 20);
            this.lblNextInvoiceNO.TabIndex = 13;
            this.lblNextInvoiceNO.Text = "ldd";
            // 
            // ucQueryInpatientNo1
            // 
            this.ucQueryInpatientNo1.DefaultInputType = 0;
            this.ucQueryInpatientNo1.InputType = 0;
            this.ucQueryInpatientNo1.IsDeptOnly = true;
            this.ucQueryInpatientNo1.Location = new System.Drawing.Point(257, 20);
            this.ucQueryInpatientNo1.Name = "ucQueryInpatientNo1";
            this.ucQueryInpatientNo1.PatientInState = "ALL";
            this.ucQueryInpatientNo1.ShowState = FS.HISFC.Components.Common.Controls.enuShowState.All;
            this.ucQueryInpatientNo1.Size = new System.Drawing.Size(198, 27);
            this.ucQueryInpatientNo1.TabIndex = 1;
            // 
            // txtInvoice
            // 
            this.txtInvoice.IsEnter2Tab = false;
            this.txtInvoice.Location = new System.Drawing.Point(81, 26);
            this.txtInvoice.Name = "txtInvoice";
            this.txtInvoice.Size = new System.Drawing.Size(125, 21);
            this.txtInvoice.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtInvoice.TabIndex = 0;
            // 
            // lblInvoice
            // 
            this.lblInvoice.AutoSize = true;
            this.lblInvoice.Location = new System.Drawing.Point(9, 30);
            this.lblInvoice.Name = "lblInvoice";
            this.lblInvoice.Size = new System.Drawing.Size(65, 12);
            this.lblInvoice.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblInvoice.TabIndex = 0;
            this.lblInvoice.Text = "票据号码：";
            // 
            // gbPatientInfo
            // 
            this.gbPatientInfo.Controls.Add(this.textBox1);
            this.gbPatientInfo.Controls.Add(this.label2);
            this.gbPatientInfo.Controls.Add(this.checkBox1);
            this.gbPatientInfo.Controls.Add(this.txtDateOut);
            this.gbPatientInfo.Controls.Add(this.label1);
            this.gbPatientInfo.Controls.Add(this.txtBirthday);
            this.gbPatientInfo.Controls.Add(this.lblBirthday);
            this.gbPatientInfo.Controls.Add(this.txtName);
            this.gbPatientInfo.Controls.Add(this.lblName);
            this.gbPatientInfo.Controls.Add(this.txtBedNo);
            this.gbPatientInfo.Controls.Add(this.lblBedNo);
            this.gbPatientInfo.Controls.Add(this.lblPact);
            this.gbPatientInfo.Controls.Add(this.txtPact);
            this.gbPatientInfo.Controls.Add(this.txtDoctor);
            this.gbPatientInfo.Controls.Add(this.lblDoctor);
            this.gbPatientInfo.Controls.Add(this.lblDept);
            this.gbPatientInfo.Controls.Add(this.txtDept);
            this.gbPatientInfo.Controls.Add(this.lblDateIn);
            this.gbPatientInfo.Controls.Add(this.lblNurceCell);
            this.gbPatientInfo.Controls.Add(this.txtDateIn);
            this.gbPatientInfo.Controls.Add(this.txtNurseStation);
            this.gbPatientInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbPatientInfo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gbPatientInfo.Location = new System.Drawing.Point(0, 60);
            this.gbPatientInfo.Name = "gbPatientInfo";
            this.gbPatientInfo.Size = new System.Drawing.Size(822, 132);
            this.gbPatientInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbPatientInfo.TabIndex = 34;
            this.gbPatientInfo.TabStop = false;
            this.gbPatientInfo.Text = "患者信息";
            this.gbPatientInfo.Enter += new System.EventHandler(this.gbPatientInfo_Enter);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.ForeColor = System.Drawing.Color.Black;
            this.textBox1.IsEnter2Tab = false;
            this.textBox1.Location = new System.Drawing.Point(257, 89);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.textBox1.TabIndex = 53;
            this.textBox1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(117, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 52;
            this.label2.Text = "label2";
            this.label2.Visible = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(14, 92);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(96, 16);
            this.checkBox1.TabIndex = 51;
            this.checkBox1.Text = "已开电子发票";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // txtDateOut
            // 
            this.txtDateOut.BackColor = System.Drawing.Color.White;
            this.txtDateOut.ForeColor = System.Drawing.Color.Black;
            this.txtDateOut.IsEnter2Tab = false;
            this.txtDateOut.Location = new System.Drawing.Point(431, 56);
            this.txtDateOut.Name = "txtDateOut";
            this.txtDateOut.ReadOnly = true;
            this.txtDateOut.Size = new System.Drawing.Size(99, 21);
            this.txtDateOut.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtDateOut.TabIndex = 50;
            this.txtDateOut.TextChanged += new System.EventHandler(this.txtOutDate_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(366, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 49;
            this.label1.Text = "出院日期";
            // 
            // txtBirthday
            // 
            this.txtBirthday.BackColor = System.Drawing.Color.White;
            this.txtBirthday.ForeColor = System.Drawing.Color.Black;
            this.txtBirthday.IsEnter2Tab = false;
            this.txtBirthday.Location = new System.Drawing.Point(596, 56);
            this.txtBirthday.Name = "txtBirthday";
            this.txtBirthday.ReadOnly = true;
            this.txtBirthday.Size = new System.Drawing.Size(100, 21);
            this.txtBirthday.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtBirthday.TabIndex = 47;
            // 
            // lblBirthday
            // 
            this.lblBirthday.AutoSize = true;
            this.lblBirthday.Location = new System.Drawing.Point(541, 59);
            this.lblBirthday.Name = "lblBirthday";
            this.lblBirthday.Size = new System.Drawing.Size(53, 12);
            this.lblBirthday.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblBirthday.TabIndex = 48;
            this.lblBirthday.Text = "出生日期";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.White;
            this.txtName.ForeColor = System.Drawing.Color.Black;
            this.txtName.IsEnter2Tab = false;
            this.txtName.Location = new System.Drawing.Point(73, 22);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(100, 21);
            this.txtName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtName.TabIndex = 33;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(14, 25);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(53, 12);
            this.lblName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblName.TabIndex = 34;
            this.lblName.Text = "患者姓名";
            // 
            // txtBedNo
            // 
            this.txtBedNo.BackColor = System.Drawing.Color.White;
            this.txtBedNo.ForeColor = System.Drawing.Color.Black;
            this.txtBedNo.IsEnter2Tab = false;
            this.txtBedNo.Location = new System.Drawing.Point(718, 50);
            this.txtBedNo.Name = "txtBedNo";
            this.txtBedNo.ReadOnly = true;
            this.txtBedNo.Size = new System.Drawing.Size(23, 21);
            this.txtBedNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtBedNo.TabIndex = 45;
            this.txtBedNo.Visible = false;
            // 
            // lblBedNo
            // 
            this.lblBedNo.AutoSize = true;
            this.lblBedNo.Location = new System.Drawing.Point(716, 31);
            this.lblBedNo.Name = "lblBedNo";
            this.lblBedNo.Size = new System.Drawing.Size(29, 12);
            this.lblBedNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblBedNo.TabIndex = 46;
            this.lblBedNo.Text = "床号";
            this.lblBedNo.Visible = false;
            // 
            // lblPact
            // 
            this.lblPact.AutoSize = true;
            this.lblPact.Location = new System.Drawing.Point(189, 25);
            this.lblPact.Name = "lblPact";
            this.lblPact.Size = new System.Drawing.Size(53, 12);
            this.lblPact.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblPact.TabIndex = 36;
            this.lblPact.Text = "合同单位";
            // 
            // txtPact
            // 
            this.txtPact.BackColor = System.Drawing.Color.White;
            this.txtPact.ForeColor = System.Drawing.Color.Black;
            this.txtPact.IsEnter2Tab = false;
            this.txtPact.Location = new System.Drawing.Point(257, 22);
            this.txtPact.Name = "txtPact";
            this.txtPact.ReadOnly = true;
            this.txtPact.Size = new System.Drawing.Size(100, 21);
            this.txtPact.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtPact.TabIndex = 35;
            // 
            // txtDoctor
            // 
            this.txtDoctor.BackColor = System.Drawing.Color.White;
            this.txtDoctor.ForeColor = System.Drawing.Color.Black;
            this.txtDoctor.IsEnter2Tab = false;
            this.txtDoctor.Location = new System.Drawing.Point(257, 56);
            this.txtDoctor.Name = "txtDoctor";
            this.txtDoctor.ReadOnly = true;
            this.txtDoctor.Size = new System.Drawing.Size(100, 21);
            this.txtDoctor.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtDoctor.TabIndex = 43;
            // 
            // lblDoctor
            // 
            this.lblDoctor.AutoSize = true;
            this.lblDoctor.Location = new System.Drawing.Point(189, 59);
            this.lblDoctor.Name = "lblDoctor";
            this.lblDoctor.Size = new System.Drawing.Size(53, 12);
            this.lblDoctor.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblDoctor.TabIndex = 44;
            this.lblDoctor.Text = "住院医生";
            // 
            // lblDept
            // 
            this.lblDept.AutoSize = true;
            this.lblDept.Location = new System.Drawing.Point(366, 25);
            this.lblDept.Name = "lblDept";
            this.lblDept.Size = new System.Drawing.Size(53, 12);
            this.lblDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblDept.TabIndex = 38;
            this.lblDept.Text = "住院科室";
            // 
            // txtDept
            // 
            this.txtDept.BackColor = System.Drawing.Color.White;
            this.txtDept.ForeColor = System.Drawing.Color.Black;
            this.txtDept.IsEnter2Tab = false;
            this.txtDept.Location = new System.Drawing.Point(431, 22);
            this.txtDept.Name = "txtDept";
            this.txtDept.ReadOnly = true;
            this.txtDept.Size = new System.Drawing.Size(100, 21);
            this.txtDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtDept.TabIndex = 37;
            // 
            // lblDateIn
            // 
            this.lblDateIn.AutoSize = true;
            this.lblDateIn.Location = new System.Drawing.Point(14, 59);
            this.lblDateIn.Name = "lblDateIn";
            this.lblDateIn.Size = new System.Drawing.Size(53, 12);
            this.lblDateIn.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblDateIn.TabIndex = 42;
            this.lblDateIn.Text = "入院日期";
            // 
            // lblNurceCell
            // 
            this.lblNurceCell.AutoSize = true;
            this.lblNurceCell.Location = new System.Drawing.Point(541, 25);
            this.lblNurceCell.Name = "lblNurceCell";
            this.lblNurceCell.Size = new System.Drawing.Size(53, 12);
            this.lblNurceCell.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblNurceCell.TabIndex = 40;
            this.lblNurceCell.Text = "所属病区";
            // 
            // txtDateIn
            // 
            this.txtDateIn.BackColor = System.Drawing.Color.White;
            this.txtDateIn.ForeColor = System.Drawing.Color.Black;
            this.txtDateIn.IsEnter2Tab = false;
            this.txtDateIn.Location = new System.Drawing.Point(73, 56);
            this.txtDateIn.Name = "txtDateIn";
            this.txtDateIn.ReadOnly = true;
            this.txtDateIn.Size = new System.Drawing.Size(100, 21);
            this.txtDateIn.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtDateIn.TabIndex = 41;
            // 
            // txtNurseStation
            // 
            this.txtNurseStation.BackColor = System.Drawing.Color.White;
            this.txtNurseStation.ForeColor = System.Drawing.Color.Black;
            this.txtNurseStation.IsEnter2Tab = false;
            this.txtNurseStation.Location = new System.Drawing.Point(596, 22);
            this.txtNurseStation.Name = "txtNurseStation";
            this.txtNurseStation.ReadOnly = true;
            this.txtNurseStation.Size = new System.Drawing.Size(100, 21);
            this.txtNurseStation.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtNurseStation.TabIndex = 39;
            // 
            // gbInvoices
            // 
            this.gbInvoices.Controls.Add(this.fpBalanceInvoice);
            this.gbInvoices.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbInvoices.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gbInvoices.Location = new System.Drawing.Point(0, 192);
            this.gbInvoices.Name = "gbInvoices";
            this.gbInvoices.Size = new System.Drawing.Size(822, 157);
            this.gbInvoices.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbInvoices.TabIndex = 35;
            this.gbInvoices.TabStop = false;
            this.gbInvoices.Text = "发票信息";
            // 
            // fpBalanceInvoice
            // 
            this.fpBalanceInvoice.About = "3.0.2004.2005";
            this.fpBalanceInvoice.AccessibleDescription = "fpBalanceInvoice, Sheet1, Row 0, Column 0, ";
            this.fpBalanceInvoice.BackColor = System.Drawing.Color.White;
            this.fpBalanceInvoice.Dock = System.Windows.Forms.DockStyle.Top;
            this.fpBalanceInvoice.FileName = "";
            this.fpBalanceInvoice.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpBalanceInvoice.IsAutoSaveGridStatus = false;
            this.fpBalanceInvoice.IsCanCustomConfigColumn = false;
            this.fpBalanceInvoice.Location = new System.Drawing.Point(3, 17);
            this.fpBalanceInvoice.Name = "fpBalanceInvoice";
            this.fpBalanceInvoice.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpBalanceInvoice.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpBalanceInvoice_Sheet1});
            this.fpBalanceInvoice.Size = new System.Drawing.Size(816, 137);
            this.fpBalanceInvoice.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpBalanceInvoice.TabIndex = 1;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpBalanceInvoice.TextTipAppearance = tipAppearance1;
            this.fpBalanceInvoice.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpBalanceInvoice_Sheet1
            // 
            this.fpBalanceInvoice_Sheet1.Reset();
            this.fpBalanceInvoice_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpBalanceInvoice_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpBalanceInvoice_Sheet1.ColumnCount = 19;
            this.fpBalanceInvoice_Sheet1.RowCount = 1;
            this.fpBalanceInvoice_Sheet1.Cells.Get(0, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
            this.fpBalanceInvoice_Sheet1.Cells.Get(0, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General;
            this.fpBalanceInvoice_Sheet1.Cells.Get(0, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
            this.fpBalanceInvoice_Sheet1.Cells.Get(0, 2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General;
            this.fpBalanceInvoice_Sheet1.Cells.Get(0, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
            this.fpBalanceInvoice_Sheet1.Cells.Get(0, 3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General;
            this.fpBalanceInvoice_Sheet1.Cells.Get(0, 4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
            this.fpBalanceInvoice_Sheet1.Cells.Get(0, 4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General;
            this.fpBalanceInvoice_Sheet1.Cells.Get(0, 5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
            this.fpBalanceInvoice_Sheet1.Cells.Get(0, 5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General;
            this.fpBalanceInvoice_Sheet1.Cells.Get(0, 6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
            this.fpBalanceInvoice_Sheet1.Cells.Get(0, 6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General;
            this.fpBalanceInvoice_Sheet1.Cells.Get(0, 7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
            this.fpBalanceInvoice_Sheet1.Cells.Get(0, 7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General;
            this.fpBalanceInvoice_Sheet1.Cells.Get(0, 8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
            this.fpBalanceInvoice_Sheet1.Cells.Get(0, 8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General;
            this.fpBalanceInvoice_Sheet1.Cells.Get(0, 9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
            this.fpBalanceInvoice_Sheet1.Cells.Get(0, 9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General;
            this.fpBalanceInvoice_Sheet1.Cells.Get(0, 10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
            this.fpBalanceInvoice_Sheet1.Cells.Get(0, 10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General;
            this.fpBalanceInvoice_Sheet1.Cells.Get(0, 11).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
            this.fpBalanceInvoice_Sheet1.Cells.Get(0, 11).Locked = true;
            this.fpBalanceInvoice_Sheet1.Cells.Get(0, 11).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General;
            this.fpBalanceInvoice_Sheet1.Cells.Get(0, 12).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
            this.fpBalanceInvoice_Sheet1.Cells.Get(0, 12).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General;
            this.fpBalanceInvoice_Sheet1.Cells.Get(0, 13).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
            this.fpBalanceInvoice_Sheet1.Cells.Get(0, 13).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General;
            this.fpBalanceInvoice_Sheet1.Cells.Get(0, 14).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
            this.fpBalanceInvoice_Sheet1.Cells.Get(0, 14).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General;
            this.fpBalanceInvoice_Sheet1.Cells.Get(0, 15).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
            this.fpBalanceInvoice_Sheet1.Cells.Get(0, 15).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General;
            this.fpBalanceInvoice_Sheet1.Cells.Get(0, 16).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
            this.fpBalanceInvoice_Sheet1.Cells.Get(0, 16).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General;
            this.fpBalanceInvoice_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "选择";
            this.fpBalanceInvoice_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "住院号";
            this.fpBalanceInvoice_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "姓名";
            this.fpBalanceInvoice_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "收据号";
            this.fpBalanceInvoice_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "收据编号";
            this.fpBalanceInvoice_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "收费时间";
            this.fpBalanceInvoice_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "住院起止日期";
            this.fpBalanceInvoice_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "结账方式";
            this.fpBalanceInvoice_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "应收金额";
            this.fpBalanceInvoice_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "预收金额";
            this.fpBalanceInvoice_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "实收金额";
            this.fpBalanceInvoice_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "欠费金额";
            this.fpBalanceInvoice_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "药费限额";
            this.fpBalanceInvoice_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "付款方式";
            this.fpBalanceInvoice_Sheet1.ColumnHeader.Cells.Get(0, 14).Value = "结账员";
            this.fpBalanceInvoice_Sheet1.ColumnHeader.Cells.Get(0, 15).Value = "作废员工";
            this.fpBalanceInvoice_Sheet1.ColumnHeader.Cells.Get(0, 16).Value = "作废时间";
            this.fpBalanceInvoice_Sheet1.ColumnHeader.Cells.Get(0, 17).Value = "日结时间";
            this.fpBalanceInvoice_Sheet1.ColumnHeader.Cells.Get(0, 18).Value = "红冲发票";
            this.fpBalanceInvoice_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpBalanceInvoice_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpBalanceInvoice_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.fpBalanceInvoice_Sheet1.Columns.Get(0).CellType = checkBoxCellType1;
            this.fpBalanceInvoice_Sheet1.Columns.Get(0).Label = "选择";
            this.fpBalanceInvoice_Sheet1.Columns.Get(0).Width = 31F;
            this.fpBalanceInvoice_Sheet1.Columns.Get(1).Label = "住院号";
            this.fpBalanceInvoice_Sheet1.Columns.Get(1).Width = 82F;
            this.fpBalanceInvoice_Sheet1.Columns.Get(3).Label = "收据号";
            this.fpBalanceInvoice_Sheet1.Columns.Get(3).Width = 101F;
            this.fpBalanceInvoice_Sheet1.Columns.Get(4).Label = "收据编号";
            this.fpBalanceInvoice_Sheet1.Columns.Get(4).Locked = true;
            this.fpBalanceInvoice_Sheet1.Columns.Get(4).Width = 86F;
            this.fpBalanceInvoice_Sheet1.Columns.Get(5).Label = "收费时间";
            this.fpBalanceInvoice_Sheet1.Columns.Get(5).Locked = true;
            this.fpBalanceInvoice_Sheet1.Columns.Get(5).Width = 115F;
            this.fpBalanceInvoice_Sheet1.Columns.Get(6).Label = "住院起止日期";
            this.fpBalanceInvoice_Sheet1.Columns.Get(6).Locked = true;
            this.fpBalanceInvoice_Sheet1.Columns.Get(6).Width = 121F;
            this.fpBalanceInvoice_Sheet1.Columns.Get(7).Label = "结账方式";
            this.fpBalanceInvoice_Sheet1.Columns.Get(7).Width = 63F;
            this.fpBalanceInvoice_Sheet1.Columns.Get(8).Label = "应收金额";
            this.fpBalanceInvoice_Sheet1.Columns.Get(8).Locked = true;
            this.fpBalanceInvoice_Sheet1.Columns.Get(8).Width = 64F;
            this.fpBalanceInvoice_Sheet1.Columns.Get(9).Label = "预收金额";
            this.fpBalanceInvoice_Sheet1.Columns.Get(9).Locked = true;
            this.fpBalanceInvoice_Sheet1.Columns.Get(9).Width = 68F;
            this.fpBalanceInvoice_Sheet1.Columns.Get(10).Label = "实收金额";
            this.fpBalanceInvoice_Sheet1.Columns.Get(10).Locked = true;
            this.fpBalanceInvoice_Sheet1.Columns.Get(10).Width = 63F;
            this.fpBalanceInvoice_Sheet1.Columns.Get(11).Label = "欠费金额";
            this.fpBalanceInvoice_Sheet1.Columns.Get(11).Width = 68F;
            this.fpBalanceInvoice_Sheet1.Columns.Get(16).Label = "作废时间";
            this.fpBalanceInvoice_Sheet1.Columns.Get(16).Width = 118F;
            this.fpBalanceInvoice_Sheet1.Columns.Get(17).Label = "日结时间";
            this.fpBalanceInvoice_Sheet1.Columns.Get(17).Width = 113F;
            this.fpBalanceInvoice_Sheet1.Columns.Get(18).Label = "红冲发票";
            this.fpBalanceInvoice_Sheet1.Columns.Get(18).Width = 81F;
            this.fpBalanceInvoice_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpBalanceInvoice_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpBalanceInvoice_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.fpBalanceInvoice_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpBalanceInvoice_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpBalanceInvoice_Sheet1.Rows.Default.Height = 25F;
            this.fpBalanceInvoice_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpBalanceInvoice_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.fpBalanceInvoice_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpBalanceInvoice_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.pnlRight);
            this.neuPanel1.Controls.Add(this.pnlLeft);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel1.Location = new System.Drawing.Point(0, 349);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(822, 97);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 36;
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.fpPrepay);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(400, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(422, 97);
            this.pnlRight.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlRight.TabIndex = 1;
            // 
            // fpPrepay
            // 
            this.fpPrepay.About = "3.0.2004.2005";
            this.fpPrepay.AccessibleDescription = "fpPrepay, Sheet1, Row 0, Column 0, ";
            this.fpPrepay.BackColor = System.Drawing.Color.White;
            this.fpPrepay.Dock = System.Windows.Forms.DockStyle.Top;
            this.fpPrepay.FileName = "";
            this.fpPrepay.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpPrepay.IsAutoSaveGridStatus = false;
            this.fpPrepay.IsCanCustomConfigColumn = false;
            this.fpPrepay.Location = new System.Drawing.Point(0, 0);
            this.fpPrepay.Name = "fpPrepay";
            this.fpPrepay.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpPrepay.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpPrepay_Sheet1});
            this.fpPrepay.Size = new System.Drawing.Size(422, 141);
            this.fpPrepay.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpPrepay.TabIndex = 0;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpPrepay.TextTipAppearance = tipAppearance2;
            this.fpPrepay.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpPrepay_Sheet1
            // 
            this.fpPrepay_Sheet1.Reset();
            this.fpPrepay_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpPrepay_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpPrepay_Sheet1.ColumnCount = 5;
            this.fpPrepay_Sheet1.RowCount = 1;
            this.fpPrepay_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "预交金票据号";
            this.fpPrepay_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "支付方式";
            this.fpPrepay_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "预交金额";
            this.fpPrepay_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "结算人";
            this.fpPrepay_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "结算时间";
            this.fpPrepay_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpPrepay_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpPrepay_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.fpPrepay_Sheet1.Columns.Get(0).Label = "预交金票据号";
            this.fpPrepay_Sheet1.Columns.Get(0).Width = 82F;
            this.fpPrepay_Sheet1.Columns.Get(1).Label = "支付方式";
            this.fpPrepay_Sheet1.Columns.Get(1).Width = 59F;
            this.fpPrepay_Sheet1.Columns.Get(2).Label = "预交金额";
            this.fpPrepay_Sheet1.Columns.Get(2).Width = 67F;
            this.fpPrepay_Sheet1.Columns.Get(4).Label = "结算时间";
            this.fpPrepay_Sheet1.Columns.Get(4).Width = 143F;
            this.fpPrepay_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpPrepay_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.fpPrepay_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpPrepay_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpPrepay_Sheet1.Rows.Default.Height = 25F;
            this.fpPrepay_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpPrepay_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.fpPrepay_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpPrepay_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.fpBalance);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(400, 97);
            this.pnlLeft.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlLeft.TabIndex = 0;
            // 
            // fpBalance
            // 
            this.fpBalance.About = "3.0.2004.2005";
            this.fpBalance.AccessibleDescription = "fpBalance, Sheet1, Row 0, Column 0, ";
            this.fpBalance.BackColor = System.Drawing.Color.White;
            this.fpBalance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpBalance.FileName = "";
            this.fpBalance.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpBalance.IsAutoSaveGridStatus = false;
            this.fpBalance.IsCanCustomConfigColumn = false;
            this.fpBalance.Location = new System.Drawing.Point(0, 0);
            this.fpBalance.Name = "fpBalance";
            this.fpBalance.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpBalance.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpBalance_Sheet1});
            this.fpBalance.Size = new System.Drawing.Size(400, 97);
            this.fpBalance.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpBalance.TabIndex = 0;
            tipAppearance3.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance3.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpBalance.TextTipAppearance = tipAppearance3;
            this.fpBalance.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpBalance_Sheet1
            // 
            this.fpBalance_Sheet1.Reset();
            this.fpBalance_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpBalance_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpBalance_Sheet1.ColumnCount = 4;
            this.fpBalance_Sheet1.RowCount = 1;
            this.fpBalance_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "费用科目";
            this.fpBalance_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "费用金额";
            this.fpBalance_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "结算操作员";
            this.fpBalance_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "结算时间";
            this.fpBalance_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpBalance_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpBalance_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.fpBalance_Sheet1.Columns.Get(0).Label = "费用科目";
            this.fpBalance_Sheet1.Columns.Get(0).Width = 64F;
            this.fpBalance_Sheet1.Columns.Get(1).Label = "费用金额";
            this.fpBalance_Sheet1.Columns.Get(1).Width = 68F;
            this.fpBalance_Sheet1.Columns.Get(2).Label = "结算操作员";
            this.fpBalance_Sheet1.Columns.Get(2).Width = 68F;
            this.fpBalance_Sheet1.Columns.Get(3).Label = "结算时间";
            this.fpBalance_Sheet1.Columns.Get(3).Width = 140F;
            this.fpBalance_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpBalance_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.fpBalance_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpBalance_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpBalance_Sheet1.Rows.Default.Height = 25F;
            this.fpBalance_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpBalance_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.fpBalance_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpBalance_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ucRePrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuPanel1);
            this.Controls.Add(this.gbInvoices);
            this.Controls.Add(this.gbPatientInfo);
            this.Controls.Add(this.gbPatientNo);
            this.Name = "ucRePrint";
            this.Size = new System.Drawing.Size(822, 446);
            this.gbPatientNo.ResumeLayout(false);
            this.gbPatientNo.PerformLayout();
            this.gbPatientInfo.ResumeLayout(false);
            this.gbPatientInfo.PerformLayout();
            this.gbInvoices.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpBalanceInvoice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpBalanceInvoice_Sheet1)).EndInit();
            this.neuPanel1.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpPrepay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPrepay_Sheet1)).EndInit();
            this.pnlLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpBalance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpBalance_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbPatientNo;
        private FS.HISFC.Components.Common.Controls.ucQueryInpatientNo ucQueryInpatientNo1;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox txtInvoice;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblInvoice;
        protected FS.FrameWork.WinForms.Controls.NeuGroupBox gbPatientInfo;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox txtBirthday;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblBirthday;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox txtName;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblName;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox txtBedNo;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblBedNo;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblPact;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox txtPact;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox txtDoctor;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblDoctor;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblDept;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox txtDept;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblDateIn;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblNurceCell;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox txtDateIn;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox txtNurseStation;
        protected FS.FrameWork.WinForms.Controls.NeuGroupBox gbInvoices;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpBalanceInvoice;
        private FarPoint.Win.Spread.SheetView fpBalanceInvoice_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlRight;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpPrepay;
        private FarPoint.Win.Spread.SheetView fpPrepay_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlLeft;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpBalance;
        private FarPoint.Win.Spread.SheetView fpBalance_Sheet1;
        private System.Windows.Forms.Label lblNextInvoiceNO;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox txtDateOut;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox1;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox textBox1;
    }
}
