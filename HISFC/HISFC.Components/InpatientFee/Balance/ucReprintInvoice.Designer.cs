﻿namespace FS.HISFC.Components.InpatientFee.Balance
{
    partial class ucReprintInvoice
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
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            this.pnlMain = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.pnlDown = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.pnlUpMain = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.gbCost = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.txtTot = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtDraft = new FS.FrameWork.WinForms.Controls.NeuNumericTextBox();
            this.lblDraft = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtCheck = new FS.FrameWork.WinForms.Controls.NeuNumericTextBox();
            this.lblCheck = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtCash = new FS.FrameWork.WinForms.Controls.NeuNumericTextBox();
            this.lblCash = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.pnlPatientInfo = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lblBirthday = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtBirthday = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblBedNo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtBedNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblDoctor = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtDoctor = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblDateIn = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtDateIn = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblNurceCell = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtNurseStation = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblDept = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtDept = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblPact = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtPact = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.gbPatientInfo = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.pnlUP = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.gbPatientNo = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.ucQueryInpatientNo1 = new FS.HISFC.Components.Common.Controls.ucQueryInpatientNo();
            this.txtInvoice = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblInvoice = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.fpBalance_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.fpBalance = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.pnlMain.SuspendLayout();
            this.pnlDown.SuspendLayout();
            this.pnlUpMain.SuspendLayout();
            this.gbCost.SuspendLayout();
            this.pnlPatientInfo.SuspendLayout();
            this.pnlUP.SuspendLayout();
            this.gbPatientNo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpBalance_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpBalance)).BeginInit();
            this.neuPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.pnlMain.Controls.Add(this.pnlDown);
            this.pnlMain.Controls.Add(this.pnlUP);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(709, 498);
            this.pnlMain.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlMain.TabIndex = 0;
            // 
            // pnlDown
            // 
            this.pnlDown.BackColor = System.Drawing.Color.Fuchsia;
            this.pnlDown.Controls.Add(this.pnlUpMain);
            this.pnlDown.Controls.Add(this.pnlPatientInfo);
            this.pnlDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDown.Location = new System.Drawing.Point(0, 72);
            this.pnlDown.Name = "pnlDown";
            this.pnlDown.Size = new System.Drawing.Size(709, 426);
            this.pnlDown.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlDown.TabIndex = 1;
            // 
            // pnlUpMain
            // 
            this.pnlUpMain.BackColor = System.Drawing.SystemColors.Control;
            this.pnlUpMain.Controls.Add(this.gbCost);
            this.pnlUpMain.Controls.Add(this.neuPanel1);
            this.pnlUpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlUpMain.Location = new System.Drawing.Point(0, 107);
            this.pnlUpMain.Name = "pnlUpMain";
            this.pnlUpMain.Size = new System.Drawing.Size(709, 319);
            this.pnlUpMain.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlUpMain.TabIndex = 1;
            // 
            // gbCost
            // 
            this.gbCost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.gbCost.Controls.Add(this.txtTot);
            this.gbCost.Controls.Add(this.txtDraft);
            this.gbCost.Controls.Add(this.lblDraft);
            this.gbCost.Controls.Add(this.txtCheck);
            this.gbCost.Controls.Add(this.lblCheck);
            this.gbCost.Controls.Add(this.txtCash);
            this.gbCost.Controls.Add(this.lblCash);
            this.gbCost.Location = new System.Drawing.Point(0, 266);
            this.gbCost.Name = "gbCost";
            this.gbCost.Size = new System.Drawing.Size(709, 53);
            this.gbCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbCost.TabIndex = 1;
            this.gbCost.TabStop = false;
            this.gbCost.Text = " 返还金额";
            // 
            // txtTot
            // 
            this.txtTot.IsEnter2Tab = false;
            this.txtTot.Location = new System.Drawing.Point(618, 79);
            this.txtTot.Name = "txtTot";
            this.txtTot.Size = new System.Drawing.Size(100, 21);
            this.txtTot.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtTot.TabIndex = 20;
            this.txtTot.Visible = false;
            // 
            // txtDraft
            // 
            this.txtDraft.AllowNegative = false;
            this.txtDraft.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDraft.IsAutoRemoveDecimalZero = false;
            this.txtDraft.IsEnter2Tab = false;
            this.txtDraft.Location = new System.Drawing.Point(485, 20);
            this.txtDraft.Name = "txtDraft";
            this.txtDraft.NumericPrecision = 10;
            this.txtDraft.NumericScaleOnFocus = 2;
            this.txtDraft.NumericScaleOnLostFocus = 2;
            this.txtDraft.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtDraft.ReadOnly = true;
            this.txtDraft.SetRange = new System.Drawing.Size(-1, -1);
            this.txtDraft.Size = new System.Drawing.Size(100, 23);
            this.txtDraft.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtDraft.TabIndex = 19;
            this.txtDraft.Text = "0.00";
            this.txtDraft.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDraft.UseGroupSeperator = true;
            this.txtDraft.ZeroIsValid = true;
            // 
            // lblDraft
            // 
            this.lblDraft.AutoSize = true;
            this.lblDraft.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDraft.Location = new System.Drawing.Point(437, 26);
            this.lblDraft.Name = "lblDraft";
            this.lblDraft.Size = new System.Drawing.Size(47, 12);
            this.lblDraft.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblDraft.TabIndex = 18;
            this.lblDraft.Text = "汇   票";
            // 
            // txtCheck
            // 
            this.txtCheck.AllowNegative = false;
            this.txtCheck.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCheck.IsAutoRemoveDecimalZero = false;
            this.txtCheck.IsEnter2Tab = false;
            this.txtCheck.Location = new System.Drawing.Point(287, 20);
            this.txtCheck.Name = "txtCheck";
            this.txtCheck.NumericPrecision = 10;
            this.txtCheck.NumericScaleOnFocus = 2;
            this.txtCheck.NumericScaleOnLostFocus = 2;
            this.txtCheck.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtCheck.ReadOnly = true;
            this.txtCheck.SetRange = new System.Drawing.Size(-1, -1);
            this.txtCheck.Size = new System.Drawing.Size(100, 23);
            this.txtCheck.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCheck.TabIndex = 17;
            this.txtCheck.Text = "0.00";
            this.txtCheck.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCheck.UseGroupSeperator = true;
            this.txtCheck.ZeroIsValid = true;
            // 
            // lblCheck
            // 
            this.lblCheck.AutoSize = true;
            this.lblCheck.Location = new System.Drawing.Point(234, 26);
            this.lblCheck.Name = "lblCheck";
            this.lblCheck.Size = new System.Drawing.Size(47, 12);
            this.lblCheck.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblCheck.TabIndex = 16;
            this.lblCheck.Text = "支   票";
            // 
            // txtCash
            // 
            this.txtCash.AllowNegative = false;
            this.txtCash.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCash.IsAutoRemoveDecimalZero = false;
            this.txtCash.IsEnter2Tab = false;
            this.txtCash.Location = new System.Drawing.Point(84, 20);
            this.txtCash.Name = "txtCash";
            this.txtCash.NumericPrecision = 10;
            this.txtCash.NumericScaleOnFocus = 2;
            this.txtCash.NumericScaleOnLostFocus = 2;
            this.txtCash.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtCash.ReadOnly = true;
            this.txtCash.SetRange = new System.Drawing.Size(-1, -1);
            this.txtCash.Size = new System.Drawing.Size(100, 23);
            this.txtCash.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCash.TabIndex = 15;
            this.txtCash.Text = "0.00";
            this.txtCash.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCash.UseGroupSeperator = true;
            this.txtCash.ZeroIsValid = true;
            // 
            // lblCash
            // 
            this.lblCash.AutoSize = true;
            this.lblCash.Location = new System.Drawing.Point(24, 26);
            this.lblCash.Name = "lblCash";
            this.lblCash.Size = new System.Drawing.Size(47, 12);
            this.lblCash.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblCash.TabIndex = 14;
            this.lblCash.Text = "现   金";
            // 
            // pnlPatientInfo
            // 
            this.pnlPatientInfo.BackColor = System.Drawing.SystemColors.Control;
            this.pnlPatientInfo.Controls.Add(this.lblBirthday);
            this.pnlPatientInfo.Controls.Add(this.txtBirthday);
            this.pnlPatientInfo.Controls.Add(this.lblBedNo);
            this.pnlPatientInfo.Controls.Add(this.txtBedNo);
            this.pnlPatientInfo.Controls.Add(this.lblDoctor);
            this.pnlPatientInfo.Controls.Add(this.txtDoctor);
            this.pnlPatientInfo.Controls.Add(this.lblDateIn);
            this.pnlPatientInfo.Controls.Add(this.txtDateIn);
            this.pnlPatientInfo.Controls.Add(this.lblNurceCell);
            this.pnlPatientInfo.Controls.Add(this.txtNurseStation);
            this.pnlPatientInfo.Controls.Add(this.lblDept);
            this.pnlPatientInfo.Controls.Add(this.txtDept);
            this.pnlPatientInfo.Controls.Add(this.lblPact);
            this.pnlPatientInfo.Controls.Add(this.txtPact);
            this.pnlPatientInfo.Controls.Add(this.lblName);
            this.pnlPatientInfo.Controls.Add(this.txtName);
            this.pnlPatientInfo.Controls.Add(this.gbPatientInfo);
            this.pnlPatientInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPatientInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlPatientInfo.Name = "pnlPatientInfo";
            this.pnlPatientInfo.Size = new System.Drawing.Size(709, 107);
            this.pnlPatientInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlPatientInfo.TabIndex = 0;
            // 
            // lblBirthday
            // 
            this.lblBirthday.AutoSize = true;
            this.lblBirthday.BackColor = System.Drawing.SystemColors.Control;
            this.lblBirthday.Location = new System.Drawing.Point(541, 66);
            this.lblBirthday.Name = "lblBirthday";
            this.lblBirthday.Size = new System.Drawing.Size(53, 12);
            this.lblBirthday.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblBirthday.TabIndex = 32;
            this.lblBirthday.Text = "出生日期";
            // 
            // txtBirthday
            // 
            this.txtBirthday.BackColor = System.Drawing.Color.White;
            this.txtBirthday.ForeColor = System.Drawing.Color.Black;
            this.txtBirthday.IsEnter2Tab = false;
            this.txtBirthday.Location = new System.Drawing.Point(596, 64);
            this.txtBirthday.Name = "txtBirthday";
            this.txtBirthday.ReadOnly = true;
            this.txtBirthday.Size = new System.Drawing.Size(100, 21);
            this.txtBirthday.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtBirthday.TabIndex = 31;
            // 
            // lblBedNo
            // 
            this.lblBedNo.AutoSize = true;
            this.lblBedNo.BackColor = System.Drawing.SystemColors.Control;
            this.lblBedNo.Location = new System.Drawing.Point(390, 66);
            this.lblBedNo.Name = "lblBedNo";
            this.lblBedNo.Size = new System.Drawing.Size(29, 12);
            this.lblBedNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblBedNo.TabIndex = 30;
            this.lblBedNo.Text = "床号";
            // 
            // txtBedNo
            // 
            this.txtBedNo.BackColor = System.Drawing.Color.White;
            this.txtBedNo.ForeColor = System.Drawing.Color.Black;
            this.txtBedNo.IsEnter2Tab = false;
            this.txtBedNo.Location = new System.Drawing.Point(431, 64);
            this.txtBedNo.Name = "txtBedNo";
            this.txtBedNo.ReadOnly = true;
            this.txtBedNo.Size = new System.Drawing.Size(100, 21);
            this.txtBedNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtBedNo.TabIndex = 29;
            // 
            // lblDoctor
            // 
            this.lblDoctor.AutoSize = true;
            this.lblDoctor.BackColor = System.Drawing.SystemColors.Control;
            this.lblDoctor.Location = new System.Drawing.Point(189, 66);
            this.lblDoctor.Name = "lblDoctor";
            this.lblDoctor.Size = new System.Drawing.Size(53, 12);
            this.lblDoctor.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblDoctor.TabIndex = 28;
            this.lblDoctor.Text = "住院医生";
            // 
            // txtDoctor
            // 
            this.txtDoctor.BackColor = System.Drawing.Color.White;
            this.txtDoctor.ForeColor = System.Drawing.Color.Black;
            this.txtDoctor.IsEnter2Tab = false;
            this.txtDoctor.Location = new System.Drawing.Point(257, 64);
            this.txtDoctor.Name = "txtDoctor";
            this.txtDoctor.ReadOnly = true;
            this.txtDoctor.Size = new System.Drawing.Size(100, 21);
            this.txtDoctor.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtDoctor.TabIndex = 27;
            // 
            // lblDateIn
            // 
            this.lblDateIn.AutoSize = true;
            this.lblDateIn.BackColor = System.Drawing.SystemColors.Control;
            this.lblDateIn.Location = new System.Drawing.Point(14, 66);
            this.lblDateIn.Name = "lblDateIn";
            this.lblDateIn.Size = new System.Drawing.Size(53, 12);
            this.lblDateIn.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblDateIn.TabIndex = 26;
            this.lblDateIn.Text = "入院日期";
            // 
            // txtDateIn
            // 
            this.txtDateIn.BackColor = System.Drawing.Color.White;
            this.txtDateIn.ForeColor = System.Drawing.Color.Black;
            this.txtDateIn.IsEnter2Tab = false;
            this.txtDateIn.Location = new System.Drawing.Point(71, 64);
            this.txtDateIn.Name = "txtDateIn";
            this.txtDateIn.ReadOnly = true;
            this.txtDateIn.Size = new System.Drawing.Size(100, 21);
            this.txtDateIn.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtDateIn.TabIndex = 25;
            // 
            // lblNurceCell
            // 
            this.lblNurceCell.AutoSize = true;
            this.lblNurceCell.BackColor = System.Drawing.SystemColors.Control;
            this.lblNurceCell.Location = new System.Drawing.Point(541, 32);
            this.lblNurceCell.Name = "lblNurceCell";
            this.lblNurceCell.Size = new System.Drawing.Size(53, 12);
            this.lblNurceCell.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblNurceCell.TabIndex = 24;
            this.lblNurceCell.Text = "所属病区";
            // 
            // txtNurseStation
            // 
            this.txtNurseStation.BackColor = System.Drawing.Color.White;
            this.txtNurseStation.ForeColor = System.Drawing.Color.Black;
            this.txtNurseStation.IsEnter2Tab = false;
            this.txtNurseStation.Location = new System.Drawing.Point(596, 29);
            this.txtNurseStation.Name = "txtNurseStation";
            this.txtNurseStation.ReadOnly = true;
            this.txtNurseStation.Size = new System.Drawing.Size(100, 21);
            this.txtNurseStation.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtNurseStation.TabIndex = 23;
            // 
            // lblDept
            // 
            this.lblDept.AutoSize = true;
            this.lblDept.BackColor = System.Drawing.SystemColors.Control;
            this.lblDept.Location = new System.Drawing.Point(366, 32);
            this.lblDept.Name = "lblDept";
            this.lblDept.Size = new System.Drawing.Size(53, 12);
            this.lblDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblDept.TabIndex = 22;
            this.lblDept.Text = "住院科室";
            // 
            // txtDept
            // 
            this.txtDept.BackColor = System.Drawing.Color.White;
            this.txtDept.ForeColor = System.Drawing.Color.Black;
            this.txtDept.IsEnter2Tab = false;
            this.txtDept.Location = new System.Drawing.Point(431, 29);
            this.txtDept.Name = "txtDept";
            this.txtDept.ReadOnly = true;
            this.txtDept.Size = new System.Drawing.Size(100, 21);
            this.txtDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtDept.TabIndex = 21;
            // 
            // lblPact
            // 
            this.lblPact.AutoSize = true;
            this.lblPact.BackColor = System.Drawing.SystemColors.Control;
            this.lblPact.Location = new System.Drawing.Point(189, 32);
            this.lblPact.Name = "lblPact";
            this.lblPact.Size = new System.Drawing.Size(53, 12);
            this.lblPact.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblPact.TabIndex = 20;
            this.lblPact.Text = "合同单位";
            // 
            // txtPact
            // 
            this.txtPact.BackColor = System.Drawing.Color.White;
            this.txtPact.ForeColor = System.Drawing.Color.Black;
            this.txtPact.IsEnter2Tab = false;
            this.txtPact.Location = new System.Drawing.Point(257, 29);
            this.txtPact.Name = "txtPact";
            this.txtPact.ReadOnly = true;
            this.txtPact.Size = new System.Drawing.Size(100, 21);
            this.txtPact.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtPact.TabIndex = 19;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.BackColor = System.Drawing.SystemColors.Control;
            this.lblName.Location = new System.Drawing.Point(14, 32);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(53, 12);
            this.lblName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblName.TabIndex = 18;
            this.lblName.Text = "患者姓名";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.White;
            this.txtName.ForeColor = System.Drawing.Color.Black;
            this.txtName.IsEnter2Tab = false;
            this.txtName.Location = new System.Drawing.Point(71, 29);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(100, 21);
            this.txtName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtName.TabIndex = 17;
            // 
            // gbPatientInfo
            // 
            this.gbPatientInfo.BackColor = System.Drawing.SystemColors.Control;
            this.gbPatientInfo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gbPatientInfo.Location = new System.Drawing.Point(3, 10);
            this.gbPatientInfo.Name = "gbPatientInfo";
            this.gbPatientInfo.Size = new System.Drawing.Size(702, 86);
            this.gbPatientInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbPatientInfo.TabIndex = 33;
            this.gbPatientInfo.TabStop = false;
            this.gbPatientInfo.Text = "患者信息";
            // 
            // pnlUP
            // 
            this.pnlUP.BackColor = System.Drawing.SystemColors.Control;
            this.pnlUP.Controls.Add(this.gbPatientNo);
            this.pnlUP.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlUP.Location = new System.Drawing.Point(0, 0);
            this.pnlUP.Name = "pnlUP";
            this.pnlUP.Size = new System.Drawing.Size(709, 72);
            this.pnlUP.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlUP.TabIndex = 0;
            // 
            // gbPatientNo
            // 
            this.gbPatientNo.Controls.Add(this.ucQueryInpatientNo1);
            this.gbPatientNo.Controls.Add(this.txtInvoice);
            this.gbPatientNo.Controls.Add(this.lblInvoice);
            this.gbPatientNo.Location = new System.Drawing.Point(3, 3);
            this.gbPatientNo.Name = "gbPatientNo";
            this.gbPatientNo.Size = new System.Drawing.Size(703, 63);
            this.gbPatientNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbPatientNo.TabIndex = 1;
            this.gbPatientNo.TabStop = false;
            // 
            // ucQueryInpatientNo1
            // 
            this.ucQueryInpatientNo1.InputType = 0;
            this.ucQueryInpatientNo1.Location = new System.Drawing.Point(302, 20);
            this.ucQueryInpatientNo1.Name = "ucQueryInpatientNo1";
            this.ucQueryInpatientNo1.ShowState = FS.HISFC.Components.Common.Controls.enuShowState.All;
            this.ucQueryInpatientNo1.Size = new System.Drawing.Size(198, 27);
            this.ucQueryInpatientNo1.TabIndex = 1;
            this.ucQueryInpatientNo1.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(this.ucQueryInpatientNo1_myEvent);
            // 
            // txtInvoice
            // 
            this.txtInvoice.IsEnter2Tab = false;
            this.txtInvoice.Location = new System.Drawing.Point(81, 26);
            this.txtInvoice.Name = "txtInvoice";
            this.txtInvoice.Size = new System.Drawing.Size(125, 21);
            this.txtInvoice.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtInvoice.TabIndex = 0;
            this.txtInvoice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInvoice_KeyDown);
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
            // fpBalance_Sheet1
            // 
            this.fpBalance_Sheet1.Reset();
            this.fpBalance_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpBalance_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpBalance_Sheet1.ColumnCount = 4;
            this.fpBalance_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "费用科目";
            this.fpBalance_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "费用金额";
            this.fpBalance_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "结算操作员";
            this.fpBalance_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "结算时间";
            this.fpBalance_Sheet1.Columns.Get(0).Label = "费用科目";
            this.fpBalance_Sheet1.Columns.Get(0).Width = 96F;
            this.fpBalance_Sheet1.Columns.Get(1).CellType = numberCellType1;
            this.fpBalance_Sheet1.Columns.Get(1).Label = "费用金额";
            this.fpBalance_Sheet1.Columns.Get(1).Width = 91F;
            this.fpBalance_Sheet1.Columns.Get(2).Label = "结算操作员";
            this.fpBalance_Sheet1.Columns.Get(2).Width = 90F;
            this.fpBalance_Sheet1.Columns.Get(3).CellType = textCellType1;
            this.fpBalance_Sheet1.Columns.Get(3).Label = "结算时间";
            this.fpBalance_Sheet1.Columns.Get(3).Width = 177F;
            this.fpBalance_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpBalance_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.fpBalance_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
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
            this.fpBalance.Size = new System.Drawing.Size(709, 266);
            this.fpBalance.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpBalance.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpBalance.TextTipAppearance = tipAppearance1;
            this.fpBalance.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // neuPanel1
            // 
            this.neuPanel1.BackColor = System.Drawing.Color.Magenta;
            this.neuPanel1.Controls.Add(this.fpBalance);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(709, 266);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // ucReprintInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Name = "ucReprintInvoice";
            this.Size = new System.Drawing.Size(709, 498);
            this.Load += new System.EventHandler(this.ucBalanceRecall_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlDown.ResumeLayout(false);
            this.pnlUpMain.ResumeLayout(false);
            this.gbCost.ResumeLayout(false);
            this.gbCost.PerformLayout();
            this.pnlPatientInfo.ResumeLayout(false);
            this.pnlPatientInfo.PerformLayout();
            this.pnlUP.ResumeLayout(false);
            this.gbPatientNo.ResumeLayout(false);
            this.gbPatientNo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpBalance_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpBalance)).EndInit();
            this.neuPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel pnlMain;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlUP;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlDown;
        private FS.HISFC.Components.Common.Controls.ucQueryInpatientNo ucQueryInpatientNo1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbPatientNo;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblInvoice;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox txtInvoice;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlPatientInfo;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblBirthday;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox txtBirthday;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblBedNo;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox txtBedNo;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblDoctor;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox txtDoctor;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblDateIn;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox txtDateIn;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblNurceCell;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox txtNurseStation;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblDept;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox txtDept;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblPact;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox txtPact;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblName;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox txtName;
        protected FS.FrameWork.WinForms.Controls.NeuGroupBox gbPatientInfo;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlUpMain;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbCost;
        protected FS.FrameWork.WinForms.Controls.NeuNumericTextBox txtDraft;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblDraft;
        protected FS.FrameWork.WinForms.Controls.NeuNumericTextBox txtCheck;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblCheck;
        protected FS.FrameWork.WinForms.Controls.NeuNumericTextBox txtCash;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblCash;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtTot;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpBalance;
        private FarPoint.Win.Spread.SheetView fpBalance_Sheet1;
    }
}
