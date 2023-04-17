namespace HISFC.Components.Package.Fee.Controls
{
    partial class ucUncharge
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
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType2 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.pnTree = new System.Windows.Forms.Panel();
            this.pnQuitInfo = new System.Windows.Forms.Panel();
            this.gbQuitInfo = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.lblSSWR = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel13 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbPayModes = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.tbQuitTotFee = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.tbQuitOther = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbQuitGift = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel10 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbQuitAccount = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel11 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel12 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.gbInvoiceInfo = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.lblPayInfo = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbTotCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.tbETCCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel9 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbGiftCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel8 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbRealCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel7 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbCardNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbInvoiceNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.fpPackage = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpPackage_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.fpPackageDetail = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpPackageDetail_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.pnQuitInfo.SuspendLayout();
            this.gbQuitInfo.SuspendLayout();
            this.gbInvoiceInfo.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpPackage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPackage_Sheet1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpPackageDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPackageDetail_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnTree
            // 
            this.pnTree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnTree.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnTree.Location = new System.Drawing.Point(0, 0);
            this.pnTree.Name = "pnTree";
            this.pnTree.Size = new System.Drawing.Size(218, 476);
            this.pnTree.TabIndex = 0;
            // 
            // pnQuitInfo
            // 
            this.pnQuitInfo.Controls.Add(this.gbQuitInfo);
            this.pnQuitInfo.Controls.Add(this.gbInvoiceInfo);
            this.pnQuitInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnQuitInfo.Location = new System.Drawing.Point(218, 0);
            this.pnQuitInfo.Name = "pnQuitInfo";
            this.pnQuitInfo.Size = new System.Drawing.Size(1031, 225);
            this.pnQuitInfo.TabIndex = 2;
            // 
            // gbQuitInfo
            // 
            this.gbQuitInfo.Controls.Add(this.lblSSWR);
            this.gbQuitInfo.Controls.Add(this.neuLabel13);
            this.gbQuitInfo.Controls.Add(this.label2);
            this.gbQuitInfo.Controls.Add(this.neuLabel4);
            this.gbQuitInfo.Controls.Add(this.cmbPayModes);
            this.gbQuitInfo.Controls.Add(this.label1);
            this.gbQuitInfo.Controls.Add(this.tbQuitTotFee);
            this.gbQuitInfo.Controls.Add(this.tbQuitOther);
            this.gbQuitInfo.Controls.Add(this.neuLabel5);
            this.gbQuitInfo.Controls.Add(this.tbQuitGift);
            this.gbQuitInfo.Controls.Add(this.neuLabel10);
            this.gbQuitInfo.Controls.Add(this.tbQuitAccount);
            this.gbQuitInfo.Controls.Add(this.neuLabel11);
            this.gbQuitInfo.Controls.Add(this.neuLabel12);
            this.gbQuitInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbQuitInfo.Location = new System.Drawing.Point(0, 121);
            this.gbQuitInfo.Name = "gbQuitInfo";
            this.gbQuitInfo.Size = new System.Drawing.Size(1031, 104);
            this.gbQuitInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbQuitInfo.TabIndex = 2;
            this.gbQuitInfo.TabStop = false;
            this.gbQuitInfo.Text = "退费信息";
            // 
            // lblSSWR
            // 
            this.lblSSWR.AutoSize = true;
            this.lblSSWR.Font = new System.Drawing.Font("Arial", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSSWR.ForeColor = System.Drawing.Color.Red;
            this.lblSSWR.Location = new System.Drawing.Point(654, 16);
            this.lblSSWR.Name = "lblSSWR";
            this.lblSSWR.Size = new System.Drawing.Size(28, 16);
            this.lblSSWR.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblSSWR.TabIndex = 24;
            this.lblSSWR.Text = "0.0";
            // 
            // neuLabel13
            // 
            this.neuLabel13.AutoSize = true;
            this.neuLabel13.Font = new System.Drawing.Font("宋体", 9.75F);
            this.neuLabel13.ForeColor = System.Drawing.Color.Red;
            this.neuLabel13.Location = new System.Drawing.Point(569, 18);
            this.neuLabel13.Name = "neuLabel13";
            this.neuLabel13.Size = new System.Drawing.Size(79, 13);
            this.neuLabel13.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel13.TabIndex = 23;
            this.neuLabel13.Text = "四舍五入费:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(596, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(425, 12);
            this.label2.TabIndex = 22;
            this.label2.Text = "退费方式仅对非账户支付金额和赠送金额有效，账户金额和赠送金额将返回账户";
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Font = new System.Drawing.Font("宋体", 9.75F);
            this.neuLabel4.Location = new System.Drawing.Point(793, 48);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(66, 13);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 21;
            this.neuLabel4.Text = "退费方式:";
            this.neuLabel4.Visible = false;
            // 
            // cmbPayModes
            // 
            this.cmbPayModes.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbPayModes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbPayModes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPayModes.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbPayModes.FormattingEnabled = true;
            this.cmbPayModes.IsEnter2Tab = false;
            this.cmbPayModes.IsFlat = false;
            this.cmbPayModes.IsLike = true;
            this.cmbPayModes.IsListOnly = false;
            this.cmbPayModes.IsPopForm = true;
            this.cmbPayModes.IsShowCustomerList = false;
            this.cmbPayModes.IsShowID = false;
            this.cmbPayModes.IsShowIDAndName = false;
            this.cmbPayModes.Location = new System.Drawing.Point(865, 42);
            this.cmbPayModes.Name = "cmbPayModes";
            this.cmbPayModes.ShowCustomerList = false;
            this.cmbPayModes.ShowID = false;
            this.cmbPayModes.Size = new System.Drawing.Size(135, 24);
            this.cmbPayModes.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbPayModes.TabIndex = 20;
            this.cmbPayModes.Tag = "";
            this.cmbPayModes.ToolBarUse = false;
            this.cmbPayModes.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(9, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(365, 12);
            this.label1.TabIndex = 19;
            this.label1.Text = "套餐退费将优先使用购买套餐时使用的账户支付的费用进行费用支付";
            // 
            // tbQuitTotFee
            // 
            this.tbQuitTotFee.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
            this.tbQuitTotFee.IsEnter2Tab = false;
            this.tbQuitTotFee.Location = new System.Drawing.Point(79, 39);
            this.tbQuitTotFee.Name = "tbQuitTotFee";
            this.tbQuitTotFee.ReadOnly = true;
            this.tbQuitTotFee.Size = new System.Drawing.Size(100, 29);
            this.tbQuitTotFee.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbQuitTotFee.TabIndex = 12;
            // 
            // tbQuitOther
            // 
            this.tbQuitOther.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
            this.tbQuitOther.IsEnter2Tab = false;
            this.tbQuitOther.Location = new System.Drawing.Point(657, 39);
            this.tbQuitOther.Name = "tbQuitOther";
            this.tbQuitOther.ReadOnly = true;
            this.tbQuitOther.Size = new System.Drawing.Size(109, 29);
            this.tbQuitOther.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbQuitOther.TabIndex = 18;
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Font = new System.Drawing.Font("宋体", 9.75F);
            this.neuLabel5.Location = new System.Drawing.Point(595, 48);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(53, 13);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 17;
            this.neuLabel5.Text = "请退费:";
            // 
            // tbQuitGift
            // 
            this.tbQuitGift.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
            this.tbQuitGift.IsEnter2Tab = false;
            this.tbQuitGift.Location = new System.Drawing.Point(463, 39);
            this.tbQuitGift.Name = "tbQuitGift";
            this.tbQuitGift.ReadOnly = true;
            this.tbQuitGift.Size = new System.Drawing.Size(100, 29);
            this.tbQuitGift.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbQuitGift.TabIndex = 16;
            // 
            // neuLabel10
            // 
            this.neuLabel10.AutoSize = true;
            this.neuLabel10.Font = new System.Drawing.Font("宋体", 9.75F);
            this.neuLabel10.Location = new System.Drawing.Point(400, 48);
            this.neuLabel10.Name = "neuLabel10";
            this.neuLabel10.Size = new System.Drawing.Size(66, 13);
            this.neuLabel10.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel10.TabIndex = 15;
            this.neuLabel10.Text = "赠送金额:";
            // 
            // tbQuitAccount
            // 
            this.tbQuitAccount.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
            this.tbQuitAccount.IsEnter2Tab = false;
            this.tbQuitAccount.Location = new System.Drawing.Point(265, 39);
            this.tbQuitAccount.Name = "tbQuitAccount";
            this.tbQuitAccount.ReadOnly = true;
            this.tbQuitAccount.Size = new System.Drawing.Size(100, 29);
            this.tbQuitAccount.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbQuitAccount.TabIndex = 14;
            // 
            // neuLabel11
            // 
            this.neuLabel11.AutoSize = true;
            this.neuLabel11.Font = new System.Drawing.Font("宋体", 9.75F);
            this.neuLabel11.Location = new System.Drawing.Point(201, 48);
            this.neuLabel11.Name = "neuLabel11";
            this.neuLabel11.Size = new System.Drawing.Size(66, 13);
            this.neuLabel11.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel11.TabIndex = 13;
            this.neuLabel11.Text = "账户金额:";
            // 
            // neuLabel12
            // 
            this.neuLabel12.AutoSize = true;
            this.neuLabel12.Font = new System.Drawing.Font("宋体", 9.75F);
            this.neuLabel12.Location = new System.Drawing.Point(20, 48);
            this.neuLabel12.Name = "neuLabel12";
            this.neuLabel12.Size = new System.Drawing.Size(53, 13);
            this.neuLabel12.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel12.TabIndex = 11;
            this.neuLabel12.Text = "总金额:";
            // 
            // gbInvoiceInfo
            // 
            this.gbInvoiceInfo.Controls.Add(this.lblPayInfo);
            this.gbInvoiceInfo.Controls.Add(this.label3);
            this.gbInvoiceInfo.Controls.Add(this.tbTotCost);
            this.gbInvoiceInfo.Controls.Add(this.tbETCCost);
            this.gbInvoiceInfo.Controls.Add(this.neuLabel9);
            this.gbInvoiceInfo.Controls.Add(this.tbGiftCost);
            this.gbInvoiceInfo.Controls.Add(this.neuLabel8);
            this.gbInvoiceInfo.Controls.Add(this.tbRealCost);
            this.gbInvoiceInfo.Controls.Add(this.neuLabel7);
            this.gbInvoiceInfo.Controls.Add(this.neuLabel6);
            this.gbInvoiceInfo.Controls.Add(this.tbName);
            this.gbInvoiceInfo.Controls.Add(this.neuLabel3);
            this.gbInvoiceInfo.Controls.Add(this.tbCardNo);
            this.gbInvoiceInfo.Controls.Add(this.neuLabel2);
            this.gbInvoiceInfo.Controls.Add(this.tbInvoiceNO);
            this.gbInvoiceInfo.Controls.Add(this.neuLabel1);
            this.gbInvoiceInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbInvoiceInfo.Location = new System.Drawing.Point(0, 0);
            this.gbInvoiceInfo.Name = "gbInvoiceInfo";
            this.gbInvoiceInfo.Size = new System.Drawing.Size(1031, 121);
            this.gbInvoiceInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbInvoiceInfo.TabIndex = 1;
            this.gbInvoiceInfo.TabStop = false;
            this.gbInvoiceInfo.Text = "患者基本信息";
            // 
            // lblPayInfo
            // 
            this.lblPayInfo.AutoSize = true;
            this.lblPayInfo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPayInfo.ForeColor = System.Drawing.Color.Blue;
            this.lblPayInfo.Location = new System.Drawing.Point(76, 87);
            this.lblPayInfo.Name = "lblPayInfo";
            this.lblPayInfo.Size = new System.Drawing.Size(0, 16);
            this.lblPayInfo.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 19;
            this.label3.Text = "支付信息：";
            // 
            // tbTotCost
            // 
            this.tbTotCost.Font = new System.Drawing.Font("宋体", 9.75F);
            this.tbTotCost.IsEnter2Tab = false;
            this.tbTotCost.Location = new System.Drawing.Point(79, 50);
            this.tbTotCost.Name = "tbTotCost";
            this.tbTotCost.ReadOnly = true;
            this.tbTotCost.Size = new System.Drawing.Size(100, 22);
            this.tbTotCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbTotCost.TabIndex = 12;
            // 
            // tbETCCost
            // 
            this.tbETCCost.Font = new System.Drawing.Font("宋体", 9.75F);
            this.tbETCCost.IsEnter2Tab = false;
            this.tbETCCost.Location = new System.Drawing.Point(726, 50);
            this.tbETCCost.Name = "tbETCCost";
            this.tbETCCost.ReadOnly = true;
            this.tbETCCost.Size = new System.Drawing.Size(110, 22);
            this.tbETCCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbETCCost.TabIndex = 18;
            // 
            // neuLabel9
            // 
            this.neuLabel9.AutoSize = true;
            this.neuLabel9.Font = new System.Drawing.Font("宋体", 9.75F);
            this.neuLabel9.Location = new System.Drawing.Point(654, 53);
            this.neuLabel9.Name = "neuLabel9";
            this.neuLabel9.Size = new System.Drawing.Size(66, 13);
            this.neuLabel9.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel9.TabIndex = 17;
            this.neuLabel9.Text = "优惠金额:";
            // 
            // tbGiftCost
            // 
            this.tbGiftCost.Font = new System.Drawing.Font("宋体", 9.75F);
            this.tbGiftCost.IsEnter2Tab = false;
            this.tbGiftCost.Location = new System.Drawing.Point(463, 49);
            this.tbGiftCost.Name = "tbGiftCost";
            this.tbGiftCost.ReadOnly = true;
            this.tbGiftCost.Size = new System.Drawing.Size(146, 22);
            this.tbGiftCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbGiftCost.TabIndex = 16;
            // 
            // neuLabel8
            // 
            this.neuLabel8.AutoSize = true;
            this.neuLabel8.Font = new System.Drawing.Font("宋体", 9.75F);
            this.neuLabel8.Location = new System.Drawing.Point(400, 53);
            this.neuLabel8.Name = "neuLabel8";
            this.neuLabel8.Size = new System.Drawing.Size(66, 13);
            this.neuLabel8.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel8.TabIndex = 15;
            this.neuLabel8.Text = "赠送金额:";
            // 
            // tbRealCost
            // 
            this.tbRealCost.Font = new System.Drawing.Font("宋体", 9.75F);
            this.tbRealCost.IsEnter2Tab = false;
            this.tbRealCost.Location = new System.Drawing.Point(265, 49);
            this.tbRealCost.Name = "tbRealCost";
            this.tbRealCost.ReadOnly = true;
            this.tbRealCost.Size = new System.Drawing.Size(100, 22);
            this.tbRealCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbRealCost.TabIndex = 14;
            // 
            // neuLabel7
            // 
            this.neuLabel7.AutoSize = true;
            this.neuLabel7.Font = new System.Drawing.Font("宋体", 9.75F);
            this.neuLabel7.Location = new System.Drawing.Point(201, 53);
            this.neuLabel7.Name = "neuLabel7";
            this.neuLabel7.Size = new System.Drawing.Size(66, 13);
            this.neuLabel7.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel7.TabIndex = 13;
            this.neuLabel7.Text = "实付金额:";
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.Font = new System.Drawing.Font("宋体", 9.75F);
            this.neuLabel6.Location = new System.Drawing.Point(20, 53);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(53, 13);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 11;
            this.neuLabel6.Text = "总金额:";
            // 
            // tbName
            // 
            this.tbName.Font = new System.Drawing.Font("宋体", 9.75F);
            this.tbName.IsEnter2Tab = false;
            this.tbName.Location = new System.Drawing.Point(463, 21);
            this.tbName.Name = "tbName";
            this.tbName.ReadOnly = true;
            this.tbName.Size = new System.Drawing.Size(146, 22);
            this.tbName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbName.TabIndex = 5;
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Font = new System.Drawing.Font("宋体", 9.75F);
            this.neuLabel3.Location = new System.Drawing.Point(426, 26);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(40, 13);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 4;
            this.neuLabel3.Text = "姓名:";
            // 
            // tbCardNo
            // 
            this.tbCardNo.Font = new System.Drawing.Font("宋体", 9.75F);
            this.tbCardNo.IsEnter2Tab = false;
            this.tbCardNo.Location = new System.Drawing.Point(265, 21);
            this.tbCardNo.Name = "tbCardNo";
            this.tbCardNo.ReadOnly = true;
            this.tbCardNo.Size = new System.Drawing.Size(100, 22);
            this.tbCardNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbCardNo.TabIndex = 3;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Font = new System.Drawing.Font("宋体", 9.75F);
            this.neuLabel2.Location = new System.Drawing.Point(201, 26);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(66, 13);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 2;
            this.neuLabel2.Text = "就诊卡号:";
            // 
            // tbInvoiceNO
            // 
            this.tbInvoiceNO.Font = new System.Drawing.Font("宋体", 9.75F);
            this.tbInvoiceNO.IsEnter2Tab = false;
            this.tbInvoiceNO.Location = new System.Drawing.Point(79, 21);
            this.tbInvoiceNO.Name = "tbInvoiceNO";
            this.tbInvoiceNO.Size = new System.Drawing.Size(100, 22);
            this.tbInvoiceNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbInvoiceNO.TabIndex = 1;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel1.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel1.Location = new System.Drawing.Point(20, 26);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(57, 13);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "发票号:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.fpPackage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(218, 225);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(488, 251);
            this.panel1.TabIndex = 3;
            // 
            // fpPackage
            // 
            this.fpPackage.About = "3.0.2004.2005";
            this.fpPackage.AccessibleDescription = "fpPackage, Sheet1, Row 0, Column 0, ";
            this.fpPackage.BackColor = System.Drawing.Color.White;
            this.fpPackage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpPackage.FileName = "";
            this.fpPackage.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpPackage.IsAutoSaveGridStatus = false;
            this.fpPackage.IsCanCustomConfigColumn = false;
            this.fpPackage.Location = new System.Drawing.Point(0, 0);
            this.fpPackage.Name = "fpPackage";
            this.fpPackage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpPackage.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpPackage_Sheet1});
            this.fpPackage.Size = new System.Drawing.Size(488, 251);
            this.fpPackage.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpPackage.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpPackage.TextTipAppearance = tipAppearance1;
            this.fpPackage.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpPackage_Sheet1
            // 
            this.fpPackage_Sheet1.Reset();
            this.fpPackage_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpPackage_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpPackage_Sheet1.ColumnCount = 7;
            this.fpPackage_Sheet1.RowCount = 0;
            this.fpPackage_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "选择";
            this.fpPackage_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "特殊折扣";
            this.fpPackage_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "套餐名称";
            this.fpPackage_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "总金额";
            this.fpPackage_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "实收金额";
            this.fpPackage_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "赠送金额";
            this.fpPackage_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "优惠金额";
            this.fpPackage_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.fpPackage_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpPackage_Sheet1.Columns.Get(0).CellType = checkBoxCellType1;
            this.fpPackage_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpPackage_Sheet1.Columns.Get(0).Label = "选择";
            this.fpPackage_Sheet1.Columns.Get(0).Locked = false;
            this.fpPackage_Sheet1.Columns.Get(0).Width = 34F;
            this.fpPackage_Sheet1.Columns.Get(1).CellType = checkBoxCellType2;
            this.fpPackage_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpPackage_Sheet1.Columns.Get(1).Label = "特殊折扣";
            this.fpPackage_Sheet1.Columns.Get(1).Width = 66F;
            this.fpPackage_Sheet1.Columns.Get(2).Label = "套餐名称";
            this.fpPackage_Sheet1.Columns.Get(2).Width = 197F;
            this.fpPackage_Sheet1.Columns.Get(3).Label = "总金额";
            this.fpPackage_Sheet1.Columns.Get(3).Width = 70F;
            this.fpPackage_Sheet1.Columns.Get(4).Label = "实收金额";
            this.fpPackage_Sheet1.Columns.Get(4).Width = 70F;
            this.fpPackage_Sheet1.Columns.Get(5).Label = "赠送金额";
            this.fpPackage_Sheet1.Columns.Get(5).Width = 70F;
            this.fpPackage_Sheet1.Columns.Get(6).Label = "优惠金额";
            this.fpPackage_Sheet1.Columns.Get(6).Width = 70F;
            this.fpPackage_Sheet1.DefaultStyle.Locked = true;
            this.fpPackage_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpPackage_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.fpPackage_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpPackage_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpPackage_Sheet1.RowHeader.Columns.Get(0).Width = 27F;
            this.fpPackage_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpPackage_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpPackage_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpPackage_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpPackage_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpPackage.SetActiveViewport(0, 1, 0);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.fpPackageDetail);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(706, 225);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(543, 251);
            this.panel2.TabIndex = 4;
            // 
            // fpPackageDetail
            // 
            this.fpPackageDetail.About = "3.0.2004.2005";
            this.fpPackageDetail.AccessibleDescription = "fpPackageDetail, Sheet1, Row 0, Column 0, ";
            this.fpPackageDetail.BackColor = System.Drawing.Color.White;
            this.fpPackageDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpPackageDetail.EditModePermanent = true;
            this.fpPackageDetail.EditModeReplace = true;
            this.fpPackageDetail.FileName = "";
            this.fpPackageDetail.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpPackageDetail.IsAutoSaveGridStatus = false;
            this.fpPackageDetail.IsCanCustomConfigColumn = false;
            this.fpPackageDetail.Location = new System.Drawing.Point(0, 0);
            this.fpPackageDetail.Name = "fpPackageDetail";
            this.fpPackageDetail.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpPackageDetail.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpPackageDetail_Sheet1});
            this.fpPackageDetail.Size = new System.Drawing.Size(543, 251);
            this.fpPackageDetail.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpPackageDetail.TabIndex = 0;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpPackageDetail.TextTipAppearance = tipAppearance2;
            this.fpPackageDetail.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpPackageDetail_Sheet1
            // 
            this.fpPackageDetail_Sheet1.Reset();
            this.fpPackageDetail_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpPackageDetail_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpPackageDetail_Sheet1.ColumnCount = 8;
            this.fpPackageDetail_Sheet1.RowCount = 0;
            this.fpPackageDetail_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "项目";
            this.fpPackageDetail_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "总数量";
            this.fpPackageDetail_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "可退数量";
            this.fpPackageDetail_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "退费数量";
            this.fpPackageDetail_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "金额";
            this.fpPackageDetail_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "实付金额";
            this.fpPackageDetail_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "赠送金额";
            this.fpPackageDetail_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "优惠金额";
            this.fpPackageDetail_Sheet1.Columns.Get(0).Label = "项目";
            this.fpPackageDetail_Sheet1.Columns.Get(0).Locked = true;
            this.fpPackageDetail_Sheet1.Columns.Get(0).Width = 296F;
            this.fpPackageDetail_Sheet1.Columns.Get(1).Label = "总数量";
            this.fpPackageDetail_Sheet1.Columns.Get(1).Locked = true;
            this.fpPackageDetail_Sheet1.Columns.Get(1).Width = 55F;
            this.fpPackageDetail_Sheet1.Columns.Get(2).Label = "可退数量";
            this.fpPackageDetail_Sheet1.Columns.Get(2).Locked = true;
            this.fpPackageDetail_Sheet1.Columns.Get(2).Width = 55F;
            numberCellType1.FixedPoint = false;
            this.fpPackageDetail_Sheet1.Columns.Get(3).CellType = numberCellType1;
            this.fpPackageDetail_Sheet1.Columns.Get(3).Label = "退费数量";
            this.fpPackageDetail_Sheet1.Columns.Get(3).Width = 55F;
            this.fpPackageDetail_Sheet1.Columns.Get(4).Label = "金额";
            this.fpPackageDetail_Sheet1.Columns.Get(4).Locked = true;
            this.fpPackageDetail_Sheet1.Columns.Get(4).Width = 70F;
            this.fpPackageDetail_Sheet1.Columns.Get(5).Label = "实付金额";
            this.fpPackageDetail_Sheet1.Columns.Get(5).Locked = true;
            this.fpPackageDetail_Sheet1.Columns.Get(5).Width = 70F;
            this.fpPackageDetail_Sheet1.Columns.Get(6).Label = "赠送金额";
            this.fpPackageDetail_Sheet1.Columns.Get(6).Locked = true;
            this.fpPackageDetail_Sheet1.Columns.Get(6).Width = 70F;
            this.fpPackageDetail_Sheet1.Columns.Get(7).Label = "优惠金额";
            this.fpPackageDetail_Sheet1.Columns.Get(7).Locked = true;
            this.fpPackageDetail_Sheet1.Columns.Get(7).Width = 70F;
            this.fpPackageDetail_Sheet1.DefaultStyle.Locked = false;
            this.fpPackageDetail_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpPackageDetail_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.fpPackageDetail_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpPackageDetail_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpPackageDetail_Sheet1.RowHeader.Columns.Get(0).Width = 32F;
            this.fpPackageDetail_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpPackageDetail_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpPackageDetail_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpPackageDetail.SetActiveViewport(0, 1, 0);
            // 
            // ucUncharge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnQuitInfo);
            this.Controls.Add(this.pnTree);
            this.Name = "ucUncharge";
            this.Size = new System.Drawing.Size(1249, 476);
            this.pnQuitInfo.ResumeLayout(false);
            this.gbQuitInfo.ResumeLayout(false);
            this.gbQuitInfo.PerformLayout();
            this.gbInvoiceInfo.ResumeLayout(false);
            this.gbInvoiceInfo.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpPackage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPackage_Sheet1)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpPackageDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPackageDetail_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnTree;
        private System.Windows.Forms.Panel pnQuitInfo;
        protected FS.FrameWork.WinForms.Controls.NeuGroupBox gbInvoiceInfo;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox tbTotCost;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox tbETCCost;
        protected FS.FrameWork.WinForms.Controls.NeuLabel neuLabel9;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox tbGiftCost;
        protected FS.FrameWork.WinForms.Controls.NeuLabel neuLabel8;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox tbRealCost;
        protected FS.FrameWork.WinForms.Controls.NeuLabel neuLabel7;
        protected FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox tbName;
        protected FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox tbCardNo;
        protected FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox tbInvoiceNO;
        protected FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        protected FS.FrameWork.WinForms.Controls.NeuGroupBox gbQuitInfo;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox tbQuitTotFee;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox tbQuitOther;
        protected FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox tbQuitGift;
        protected FS.FrameWork.WinForms.Controls.NeuLabel neuLabel10;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox tbQuitAccount;
        protected FS.FrameWork.WinForms.Controls.NeuLabel neuLabel11;
        protected FS.FrameWork.WinForms.Controls.NeuLabel neuLabel12;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpPackage;
        private FarPoint.Win.Spread.SheetView fpPackage_Sheet1;
        private System.Windows.Forms.Panel panel2;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpPackageDetail;
        private FarPoint.Win.Spread.SheetView fpPackageDetail_Sheet1;
        protected FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbPayModes;
        private System.Windows.Forms.Label label2;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblSSWR;
        protected FS.FrameWork.WinForms.Controls.NeuLabel neuLabel13;
        private System.Windows.Forms.Label lblPayInfo;
        private System.Windows.Forms.Label label3;

    }
}
