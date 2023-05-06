namespace FS.SOC.HISFC.InpatientFee.Components.Balance
{
    partial class frmBalancePayAccount
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
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.gbBalanceInfo = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.btnCancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnReCompute = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnOK = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.txtPay = new FS.FrameWork.WinForms.Controls.NeuNumericTextBox();
            this.lbReturnCost = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtCharge = new FS.FrameWork.WinForms.Controls.NeuNumericTextBox();
            this.lbTransPrepayCost = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtShouldPay = new FS.FrameWork.WinForms.Controls.NeuNumericTextBox();
            this.lblShouldPay = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.gbPayMode = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.fpPayType = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpPayType_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.pnAccount = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.sycoupon = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblCoupon = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.btCoupon = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.tbCouponCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblPackageInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblEmpwoerInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblAccountInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.btPackage = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btEmpower = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btAccount = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.tbPackageCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.tbEmpowerCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.tbAccountCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.gbBalanceInfo.SuspendLayout();
            this.gbPayMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpPayType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPayType_Sheet1)).BeginInit();
            this.pnAccount.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbBalanceInfo
            // 
            this.gbBalanceInfo.Controls.Add(this.btnCancel);
            this.gbBalanceInfo.Controls.Add(this.btnReCompute);
            this.gbBalanceInfo.Controls.Add(this.btnOK);
            this.gbBalanceInfo.Controls.Add(this.txtPay);
            this.gbBalanceInfo.Controls.Add(this.lbReturnCost);
            this.gbBalanceInfo.Controls.Add(this.txtCharge);
            this.gbBalanceInfo.Controls.Add(this.lbTransPrepayCost);
            this.gbBalanceInfo.Controls.Add(this.txtShouldPay);
            this.gbBalanceInfo.Controls.Add(this.lblShouldPay);
            this.gbBalanceInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbBalanceInfo.Font = new System.Drawing.Font("宋体", 1F);
            this.gbBalanceInfo.Location = new System.Drawing.Point(0, 0);
            this.gbBalanceInfo.Name = "gbBalanceInfo";
            this.gbBalanceInfo.Size = new System.Drawing.Size(842, 79);
            this.gbBalanceInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbBalanceInfo.TabIndex = 3;
            this.gbBalanceInfo.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("宋体", 11F);
            this.btnCancel.Location = new System.Drawing.Point(542, 42);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 32);
            this.btnCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.Cancel;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnReCompute
            // 
            this.btnReCompute.Font = new System.Drawing.Font("宋体", 11F);
            this.btnReCompute.Location = new System.Drawing.Point(296, 42);
            this.btnReCompute.Name = "btnReCompute";
            this.btnReCompute.Size = new System.Drawing.Size(87, 32);
            this.btnReCompute.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnReCompute.TabIndex = 7;
            this.btnReCompute.Text = "重新计算";
            this.btnReCompute.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnReCompute.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("宋体", 11F);
            this.btnOK.Location = new System.Drawing.Point(431, 42);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(87, 32);
            this.btnOK.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "确定(&O)";
            this.btnOK.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.OK;
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // txtPay
            // 
            this.txtPay.AllowNegative = false;
            this.txtPay.BackColor = System.Drawing.Color.White;
            this.txtPay.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPay.IsAutoRemoveDecimalZero = false;
            this.txtPay.IsEnter2Tab = false;
            this.txtPay.Location = new System.Drawing.Point(262, 8);
            this.txtPay.Name = "txtPay";
            this.txtPay.NumericPrecision = 10;
            this.txtPay.NumericScaleOnFocus = 2;
            this.txtPay.NumericScaleOnLostFocus = 2;
            this.txtPay.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtPay.SetRange = new System.Drawing.Size(-1, -1);
            this.txtPay.Size = new System.Drawing.Size(119, 23);
            this.txtPay.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtPay.TabIndex = 1;
            this.txtPay.Text = "0.00";
            this.txtPay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPay.UseGroupSeperator = true;
            this.txtPay.ZeroIsValid = true;
            // 
            // lbReturnCost
            // 
            this.lbReturnCost.AutoSize = true;
            this.lbReturnCost.Font = new System.Drawing.Font("宋体", 9F);
            this.lbReturnCost.Location = new System.Drawing.Point(229, 13);
            this.lbReturnCost.Name = "lbReturnCost";
            this.lbReturnCost.Size = new System.Drawing.Size(29, 12);
            this.lbReturnCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbReturnCost.TabIndex = 101;
            this.lbReturnCost.Text = "实收";
            // 
            // txtCharge
            // 
            this.txtCharge.AllowNegative = false;
            this.txtCharge.BackColor = System.Drawing.Color.White;
            this.txtCharge.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCharge.IsAutoRemoveDecimalZero = false;
            this.txtCharge.IsEnter2Tab = false;
            this.txtCharge.Location = new System.Drawing.Point(486, 8);
            this.txtCharge.Name = "txtCharge";
            this.txtCharge.NumericPrecision = 10;
            this.txtCharge.NumericScaleOnFocus = 2;
            this.txtCharge.NumericScaleOnLostFocus = 2;
            this.txtCharge.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtCharge.ReadOnly = true;
            this.txtCharge.SetRange = new System.Drawing.Size(-1, -1);
            this.txtCharge.Size = new System.Drawing.Size(90, 23);
            this.txtCharge.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCharge.TabIndex = 2;
            this.txtCharge.Text = "0.00";
            this.txtCharge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCharge.UseGroupSeperator = true;
            this.txtCharge.ZeroIsValid = true;
            // 
            // lbTransPrepayCost
            // 
            this.lbTransPrepayCost.AutoSize = true;
            this.lbTransPrepayCost.Font = new System.Drawing.Font("宋体", 9F);
            this.lbTransPrepayCost.Location = new System.Drawing.Point(427, 13);
            this.lbTransPrepayCost.Name = "lbTransPrepayCost";
            this.lbTransPrepayCost.Size = new System.Drawing.Size(53, 12);
            this.lbTransPrepayCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbTransPrepayCost.TabIndex = 16;
            this.lbTransPrepayCost.Text = "找零金额";
            // 
            // txtShouldPay
            // 
            this.txtShouldPay.AllowNegative = false;
            this.txtShouldPay.BackColor = System.Drawing.Color.White;
            this.txtShouldPay.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtShouldPay.ForeColor = System.Drawing.Color.Red;
            this.txtShouldPay.IsAutoRemoveDecimalZero = false;
            this.txtShouldPay.IsEnter2Tab = false;
            this.txtShouldPay.Location = new System.Drawing.Point(73, 8);
            this.txtShouldPay.Name = "txtShouldPay";
            this.txtShouldPay.NumericPrecision = 10;
            this.txtShouldPay.NumericScaleOnFocus = 2;
            this.txtShouldPay.NumericScaleOnLostFocus = 2;
            this.txtShouldPay.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtShouldPay.ReadOnly = true;
            this.txtShouldPay.SetRange = new System.Drawing.Size(-1, -1);
            this.txtShouldPay.Size = new System.Drawing.Size(119, 23);
            this.txtShouldPay.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtShouldPay.TabIndex = 0;
            this.txtShouldPay.TabStop = false;
            this.txtShouldPay.Text = "0.00";
            this.txtShouldPay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtShouldPay.UseGroupSeperator = true;
            this.txtShouldPay.ZeroIsValid = true;
            // 
            // lblShouldPay
            // 
            this.lblShouldPay.AutoSize = true;
            this.lblShouldPay.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.lblShouldPay.ForeColor = System.Drawing.Color.Red;
            this.lblShouldPay.Location = new System.Drawing.Point(4, 13);
            this.lblShouldPay.Name = "lblShouldPay";
            this.lblShouldPay.Size = new System.Drawing.Size(71, 12);
            this.lblShouldPay.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblShouldPay.TabIndex = 14;
            this.lblShouldPay.Text = "应收(自费)";
            // 
            // gbPayMode
            // 
            this.gbPayMode.Controls.Add(this.fpPayType);
            this.gbPayMode.Controls.Add(this.pnAccount);
            this.gbPayMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbPayMode.Font = new System.Drawing.Font("宋体", 1F);
            this.gbPayMode.Location = new System.Drawing.Point(0, 79);
            this.gbPayMode.Name = "gbPayMode";
            this.gbPayMode.Size = new System.Drawing.Size(842, 367);
            this.gbPayMode.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbPayMode.TabIndex = 4;
            this.gbPayMode.TabStop = false;
            // 
            // fpPayType
            // 
            this.fpPayType.About = "3.0.2004.2005";
            this.fpPayType.AccessibleDescription = "fpPayType, Sheet1, Row 0, Column 0, ";
            this.fpPayType.BackColor = System.Drawing.Color.White;
            this.fpPayType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpPayType.EditModeReplace = true;
            this.fpPayType.FileName = "";
            this.fpPayType.Font = new System.Drawing.Font("宋体", 9F);
            this.fpPayType.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpPayType.IsAutoSaveGridStatus = false;
            this.fpPayType.IsCanCustomConfigColumn = false;
            this.fpPayType.Location = new System.Drawing.Point(3, 154);
            this.fpPayType.Name = "fpPayType";
            this.fpPayType.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpPayType.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpPayType_Sheet1});
            this.fpPayType.Size = new System.Drawing.Size(836, 210);
            this.fpPayType.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpPayType.TabIndex = 99;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpPayType.TextTipAppearance = tipAppearance1;
            this.fpPayType.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpPayType_Sheet1
            // 
            this.fpPayType_Sheet1.Reset();
            this.fpPayType_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpPayType_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpPayType_Sheet1.ColumnCount = 7;
            this.fpPayType_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpPayType_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "支付方式";
            this.fpPayType_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "支付金额";
            this.fpPayType_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "开户银行";
            this.fpPayType_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "账号";
            this.fpPayType_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "开据单位";
            this.fpPayType_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "支票/交易流水号";
            this.fpPayType_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "备注信息";
            this.fpPayType_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpPayType_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpPayType_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.fpPayType_Sheet1.Columns.Default.NoteIndicatorColor = System.Drawing.Color.White;
            this.fpPayType_Sheet1.Columns.Get(0).BackColor = System.Drawing.Color.White;
            this.fpPayType_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.fpPayType_Sheet1.Columns.Get(0).Label = "支付方式";
            this.fpPayType_Sheet1.Columns.Get(0).Width = 88F;
            this.fpPayType_Sheet1.Columns.Get(1).BackColor = System.Drawing.Color.White;
            this.fpPayType_Sheet1.Columns.Get(1).CellType = numberCellType1;
            this.fpPayType_Sheet1.Columns.Get(1).Label = "支付金额";
            this.fpPayType_Sheet1.Columns.Get(1).Locked = false;
            this.fpPayType_Sheet1.Columns.Get(1).Width = 73F;
            this.fpPayType_Sheet1.Columns.Get(2).BackColor = System.Drawing.Color.White;
            this.fpPayType_Sheet1.Columns.Get(2).CellType = textCellType2;
            this.fpPayType_Sheet1.Columns.Get(2).Label = "开户银行";
            this.fpPayType_Sheet1.Columns.Get(2).Locked = false;
            this.fpPayType_Sheet1.Columns.Get(2).Width = 101F;
            this.fpPayType_Sheet1.Columns.Get(3).BackColor = System.Drawing.Color.White;
            this.fpPayType_Sheet1.Columns.Get(3).Label = "账号";
            this.fpPayType_Sheet1.Columns.Get(3).Locked = false;
            this.fpPayType_Sheet1.Columns.Get(3).Width = 99F;
            this.fpPayType_Sheet1.Columns.Get(4).BackColor = System.Drawing.Color.White;
            this.fpPayType_Sheet1.Columns.Get(4).Label = "开据单位";
            this.fpPayType_Sheet1.Columns.Get(4).Locked = false;
            this.fpPayType_Sheet1.Columns.Get(4).Width = 93F;
            this.fpPayType_Sheet1.Columns.Get(5).BackColor = System.Drawing.Color.White;
            this.fpPayType_Sheet1.Columns.Get(5).CellType = textCellType3;
            this.fpPayType_Sheet1.Columns.Get(5).Label = "支票/交易流水号";
            this.fpPayType_Sheet1.Columns.Get(5).Locked = false;
            this.fpPayType_Sheet1.Columns.Get(5).Width = 115F;
            this.fpPayType_Sheet1.Columns.Get(6).Label = "备注信息";
            this.fpPayType_Sheet1.Columns.Get(6).Width = 213F;
            this.fpPayType_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpPayType_Sheet1.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.White;
            this.fpPayType_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpPayType_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpPayType_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.fpPayType_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpPayType_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpPayType_Sheet1.Rows.Default.NoteIndicatorColor = System.Drawing.Color.White;
            this.fpPayType_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpPayType_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.fpPayType_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpPayType_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // pnAccount
            // 
            this.pnAccount.BackColor = System.Drawing.Color.White;
            this.pnAccount.Controls.Add(this.sycoupon);
            this.pnAccount.Controls.Add(this.neuLabel1);
            this.pnAccount.Controls.Add(this.lblCoupon);
            this.pnAccount.Controls.Add(this.btCoupon);
            this.pnAccount.Controls.Add(this.tbCouponCost);
            this.pnAccount.Controls.Add(this.lblPackageInfo);
            this.pnAccount.Controls.Add(this.lblEmpwoerInfo);
            this.pnAccount.Controls.Add(this.lblAccountInfo);
            this.pnAccount.Controls.Add(this.btPackage);
            this.pnAccount.Controls.Add(this.btEmpower);
            this.pnAccount.Controls.Add(this.btAccount);
            this.pnAccount.Controls.Add(this.tbPackageCost);
            this.pnAccount.Controls.Add(this.tbEmpowerCost);
            this.pnAccount.Controls.Add(this.tbAccountCost);
            this.pnAccount.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnAccount.Location = new System.Drawing.Point(3, 5);
            this.pnAccount.Name = "pnAccount";
            this.pnAccount.Size = new System.Drawing.Size(836, 149);
            this.pnAccount.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnAccount.TabIndex = 100;
            // 
            // sycoupon
            // 
            this.sycoupon.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold);
            this.sycoupon.IsEnter2Tab = false;
            this.sycoupon.Location = new System.Drawing.Point(473, 8);
            this.sycoupon.Name = "sycoupon";
            this.sycoupon.ReadOnly = true;
            this.sycoupon.Size = new System.Drawing.Size(100, 25);
            this.sycoupon.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.sycoupon.TabIndex = 104;
            this.sycoupon.Text = "0.00";
            this.sycoupon.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Font = new System.Drawing.Font("宋体", 12F);
            this.neuLabel1.Location = new System.Drawing.Point(383, 14);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(87, 16);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 103;
            this.neuLabel1.Text = "剩余积分：";
            // 
            // lblCoupon
            // 
            this.lblCoupon.AutoSize = true;
            this.lblCoupon.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCoupon.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblCoupon.Location = new System.Drawing.Point(204, 116);
            this.lblCoupon.Name = "lblCoupon";
            this.lblCoupon.Size = new System.Drawing.Size(57, 12);
            this.lblCoupon.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblCoupon.TabIndex = 11;
            this.lblCoupon.Text = "积分支付";
            // 
            // btCoupon
            // 
            this.btCoupon.Font = new System.Drawing.Font("宋体", 9F);
            this.btCoupon.Location = new System.Drawing.Point(8, 112);
            this.btCoupon.Name = "btCoupon";
            this.btCoupon.Size = new System.Drawing.Size(75, 23);
            this.btCoupon.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btCoupon.TabIndex = 9;
            this.btCoupon.Text = "积分支付";
            this.btCoupon.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btCoupon.UseVisualStyleBackColor = true;
            // 
            // tbCouponCost
            // 
            this.tbCouponCost.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold);
            this.tbCouponCost.IsEnter2Tab = false;
            this.tbCouponCost.Location = new System.Drawing.Point(89, 110);
            this.tbCouponCost.Name = "tbCouponCost";
            this.tbCouponCost.ReadOnly = true;
            this.tbCouponCost.Size = new System.Drawing.Size(100, 25);
            this.tbCouponCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbCouponCost.TabIndex = 10;
            this.tbCouponCost.Text = "0.00";
            this.tbCouponCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPackageInfo
            // 
            this.lblPackageInfo.AutoSize = true;
            this.lblPackageInfo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPackageInfo.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblPackageInfo.Location = new System.Drawing.Point(203, 81);
            this.lblPackageInfo.Name = "lblPackageInfo";
            this.lblPackageInfo.Size = new System.Drawing.Size(57, 12);
            this.lblPackageInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblPackageInfo.TabIndex = 8;
            this.lblPackageInfo.Text = "套餐结算";
            // 
            // lblEmpwoerInfo
            // 
            this.lblEmpwoerInfo.AutoSize = true;
            this.lblEmpwoerInfo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblEmpwoerInfo.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblEmpwoerInfo.Location = new System.Drawing.Point(203, 46);
            this.lblEmpwoerInfo.Name = "lblEmpwoerInfo";
            this.lblEmpwoerInfo.Size = new System.Drawing.Size(57, 12);
            this.lblEmpwoerInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblEmpwoerInfo.TabIndex = 7;
            this.lblEmpwoerInfo.Text = "会员代付";
            // 
            // lblAccountInfo
            // 
            this.lblAccountInfo.AutoSize = true;
            this.lblAccountInfo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAccountInfo.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblAccountInfo.Location = new System.Drawing.Point(203, 14);
            this.lblAccountInfo.Name = "lblAccountInfo";
            this.lblAccountInfo.Size = new System.Drawing.Size(57, 12);
            this.lblAccountInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblAccountInfo.TabIndex = 6;
            this.lblAccountInfo.Text = "会员支付";
            // 
            // btPackage
            // 
            this.btPackage.Font = new System.Drawing.Font("宋体", 9F);
            this.btPackage.Location = new System.Drawing.Point(7, 77);
            this.btPackage.Name = "btPackage";
            this.btPackage.Size = new System.Drawing.Size(75, 23);
            this.btPackage.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btPackage.TabIndex = 4;
            this.btPackage.Text = "套餐结算";
            this.btPackage.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btPackage.UseVisualStyleBackColor = true;
            // 
            // btEmpower
            // 
            this.btEmpower.Font = new System.Drawing.Font("宋体", 9F);
            this.btEmpower.Location = new System.Drawing.Point(7, 42);
            this.btEmpower.Name = "btEmpower";
            this.btEmpower.Size = new System.Drawing.Size(75, 23);
            this.btEmpower.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btEmpower.TabIndex = 2;
            this.btEmpower.Text = "会员代付";
            this.btEmpower.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btEmpower.UseVisualStyleBackColor = true;
            // 
            // btAccount
            // 
            this.btAccount.Font = new System.Drawing.Font("宋体", 9F);
            this.btAccount.Location = new System.Drawing.Point(7, 9);
            this.btAccount.Name = "btAccount";
            this.btAccount.Size = new System.Drawing.Size(75, 23);
            this.btAccount.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btAccount.TabIndex = 0;
            this.btAccount.Text = "会员结算";
            this.btAccount.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btAccount.UseVisualStyleBackColor = true;
            // 
            // tbPackageCost
            // 
            this.tbPackageCost.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold);
            this.tbPackageCost.IsEnter2Tab = false;
            this.tbPackageCost.Location = new System.Drawing.Point(88, 75);
            this.tbPackageCost.Name = "tbPackageCost";
            this.tbPackageCost.ReadOnly = true;
            this.tbPackageCost.Size = new System.Drawing.Size(100, 25);
            this.tbPackageCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbPackageCost.TabIndex = 5;
            this.tbPackageCost.Text = "0.00";
            this.tbPackageCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbEmpowerCost
            // 
            this.tbEmpowerCost.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold);
            this.tbEmpowerCost.IsEnter2Tab = false;
            this.tbEmpowerCost.Location = new System.Drawing.Point(88, 40);
            this.tbEmpowerCost.Name = "tbEmpowerCost";
            this.tbEmpowerCost.ReadOnly = true;
            this.tbEmpowerCost.Size = new System.Drawing.Size(100, 25);
            this.tbEmpowerCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbEmpowerCost.TabIndex = 3;
            this.tbEmpowerCost.Text = "0.00";
            this.tbEmpowerCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbAccountCost
            // 
            this.tbAccountCost.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold);
            this.tbAccountCost.IsEnter2Tab = false;
            this.tbAccountCost.Location = new System.Drawing.Point(88, 6);
            this.tbAccountCost.Name = "tbAccountCost";
            this.tbAccountCost.ReadOnly = true;
            this.tbAccountCost.Size = new System.Drawing.Size(100, 25);
            this.tbAccountCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbAccountCost.TabIndex = 1;
            this.tbAccountCost.Text = "0.00";
            this.tbAccountCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // frmBalancePayAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.ClientSize = new System.Drawing.Size(842, 446);
            this.Controls.Add(this.gbPayMode);
            this.Controls.Add(this.gbBalanceInfo);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBalancePayAccount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "结算支付方式";
            this.gbBalanceInfo.ResumeLayout(false);
            this.gbBalanceInfo.PerformLayout();
            this.gbPayMode.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpPayType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPayType_Sheet1)).EndInit();
            this.pnAccount.ResumeLayout(false);
            this.pnAccount.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbBalanceInfo;
        protected FS.FrameWork.WinForms.Controls.NeuNumericTextBox txtPay;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbReturnCost;
        protected FS.FrameWork.WinForms.Controls.NeuNumericTextBox txtCharge;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbTransPrepayCost;
        protected FS.FrameWork.WinForms.Controls.NeuNumericTextBox txtShouldPay;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblShouldPay;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbPayMode;
        protected FarPoint.Win.Spread.SheetView fpPayType_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuButton btnOK;
        private FS.FrameWork.WinForms.Controls.NeuButton btnCancel;
        protected FS.FrameWork.WinForms.Controls.NeuSpread fpPayType;
        private FS.FrameWork.WinForms.Controls.NeuButton btnReCompute;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnAccount;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblPackageInfo;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblEmpwoerInfo;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblAccountInfo;
        private FS.FrameWork.WinForms.Controls.NeuButton btPackage;
        private FS.FrameWork.WinForms.Controls.NeuButton btEmpower;
        private FS.FrameWork.WinForms.Controls.NeuButton btAccount;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox tbPackageCost;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox tbEmpowerCost;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox tbAccountCost;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblCoupon;
        private FS.FrameWork.WinForms.Controls.NeuButton btCoupon;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox tbCouponCost;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox sycoupon;
        protected FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
    }
}