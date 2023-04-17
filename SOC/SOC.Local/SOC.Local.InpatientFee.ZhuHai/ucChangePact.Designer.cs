﻿namespace FS.SOC.Local.InpatientFee.ZhuHai
{
    partial class ucChangePact
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
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            this.pnlMain = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.pnlDown = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.pnlFeeInfo = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.tcFeeInfo = new FS.FrameWork.WinForms.Controls.NeuTabControl();
            this.tpUndrug = new System.Windows.Forms.TabPage();
            this.fpUndrug = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpUndrug_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.tpDrug = new System.Windows.Forms.TabPage();
            this.fpDrug = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpDrug_Sheet1 = new FarPoint.Win.Spread.SheetView();
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
            this.txtOldPact = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblNewPact = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblOldPact = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbNewPact = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.ucQueryInpatientNo1 = new FS.HISFC.Components.Common.Controls.ucQueryInpatientNo();
            this.gbPatientNO = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.pnlMain.SuspendLayout();
            this.pnlDown.SuspendLayout();
            this.pnlFeeInfo.SuspendLayout();
            this.tcFeeInfo.SuspendLayout();
            this.tpUndrug.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpUndrug)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpUndrug_Sheet1)).BeginInit();
            this.tpDrug.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpDrug)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpDrug_Sheet1)).BeginInit();
            this.pnlPatientInfo.SuspendLayout();
            this.pnlUP.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlMain.Controls.Add(this.pnlDown);
            this.pnlMain.Controls.Add(this.pnlUP);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(791, 518);
            this.pnlMain.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlMain.TabIndex = 0;
            // 
            // pnlDown
            // 
            this.pnlDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.pnlDown.Controls.Add(this.pnlFeeInfo);
            this.pnlDown.Controls.Add(this.pnlPatientInfo);
            this.pnlDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDown.Location = new System.Drawing.Point(0, 70);
            this.pnlDown.Name = "pnlDown";
            this.pnlDown.Size = new System.Drawing.Size(791, 448);
            this.pnlDown.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlDown.TabIndex = 1;
            // 
            // pnlFeeInfo
            // 
            this.pnlFeeInfo.Controls.Add(this.tcFeeInfo);
            this.pnlFeeInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFeeInfo.Location = new System.Drawing.Point(0, 114);
            this.pnlFeeInfo.Name = "pnlFeeInfo";
            this.pnlFeeInfo.Size = new System.Drawing.Size(791, 334);
            this.pnlFeeInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlFeeInfo.TabIndex = 1;
            // 
            // tcFeeInfo
            // 
            this.tcFeeInfo.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tcFeeInfo.Controls.Add(this.tpUndrug);
            this.tcFeeInfo.Controls.Add(this.tpDrug);
            this.tcFeeInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcFeeInfo.Location = new System.Drawing.Point(0, 0);
            this.tcFeeInfo.Name = "tcFeeInfo";
            this.tcFeeInfo.SelectedIndex = 0;
            this.tcFeeInfo.Size = new System.Drawing.Size(791, 334);
            this.tcFeeInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tcFeeInfo.TabIndex = 0;
            // 
            // tpUndrug
            // 
            this.tpUndrug.Controls.Add(this.fpUndrug);
            this.tpUndrug.Location = new System.Drawing.Point(4, 4);
            this.tpUndrug.Name = "tpUndrug";
            this.tpUndrug.Padding = new System.Windows.Forms.Padding(3);
            this.tpUndrug.Size = new System.Drawing.Size(783, 309);
            this.tpUndrug.TabIndex = 0;
            this.tpUndrug.Text = "非药品";
            this.tpUndrug.UseVisualStyleBackColor = true;
            // 
            // fpUndrug
            // 
            this.fpUndrug.About = "3.0.2004.2005";
            this.fpUndrug.AccessibleDescription = "fpUndrug, Sheet1, Row 0, Column 0, ";
            this.fpUndrug.BackColor = System.Drawing.Color.White;
            this.fpUndrug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpUndrug.FileName = "";
            this.fpUndrug.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpUndrug.IsAutoSaveGridStatus = false;
            this.fpUndrug.IsCanCustomConfigColumn = false;
            this.fpUndrug.Location = new System.Drawing.Point(3, 3);
            this.fpUndrug.Name = "fpUndrug";
            this.fpUndrug.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpUndrug_Sheet1});
            this.fpUndrug.Size = new System.Drawing.Size(777, 303);
            this.fpUndrug.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpUndrug.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpUndrug.TextTipAppearance = tipAppearance1;
            this.fpUndrug.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpUndrug_Sheet1
            // 
            this.fpUndrug_Sheet1.Reset();
            this.fpUndrug_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpUndrug_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpUndrug_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpUndrug_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpUndrug_Sheet1.RowHeader.Columns.Get(0).Width = 40F;
            this.fpUndrug_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // tpDrug
            // 
            this.tpDrug.Controls.Add(this.fpDrug);
            this.tpDrug.Location = new System.Drawing.Point(4, 4);
            this.tpDrug.Name = "tpDrug";
            this.tpDrug.Padding = new System.Windows.Forms.Padding(3);
            this.tpDrug.Size = new System.Drawing.Size(783, 309);
            this.tpDrug.TabIndex = 1;
            this.tpDrug.Text = "药品";
            this.tpDrug.UseVisualStyleBackColor = true;
            // 
            // fpDrug
            // 
            this.fpDrug.About = "3.0.2004.2005";
            this.fpDrug.AccessibleDescription = "fpDrug, Sheet1, Row 0, Column 0, ";
            this.fpDrug.BackColor = System.Drawing.Color.White;
            this.fpDrug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpDrug.FileName = "";
            this.fpDrug.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpDrug.IsAutoSaveGridStatus = false;
            this.fpDrug.IsCanCustomConfigColumn = false;
            this.fpDrug.Location = new System.Drawing.Point(3, 3);
            this.fpDrug.Name = "fpDrug";
            this.fpDrug.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpDrug_Sheet1});
            this.fpDrug.Size = new System.Drawing.Size(777, 303);
            this.fpDrug.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpDrug.TabIndex = 0;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpDrug.TextTipAppearance = tipAppearance2;
            this.fpDrug.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpDrug_Sheet1
            // 
            this.fpDrug_Sheet1.Reset();
            this.fpDrug_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpDrug_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpDrug_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpDrug_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpDrug_Sheet1.RowHeader.Columns.Get(0).Width = 40F;
            this.fpDrug_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
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
            this.pnlPatientInfo.Size = new System.Drawing.Size(791, 114);
            this.pnlPatientInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlPatientInfo.TabIndex = 0;
            // 
            // lblBirthday
            // 
            this.lblBirthday.AutoSize = true;
            this.lblBirthday.Location = new System.Drawing.Point(556, 70);
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
            this.txtBirthday.Location = new System.Drawing.Point(611, 68);
            this.txtBirthday.Name = "txtBirthday";
            this.txtBirthday.ReadOnly = true;
            this.txtBirthday.Size = new System.Drawing.Size(100, 21);
            this.txtBirthday.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtBirthday.TabIndex = 31;
            // 
            // lblBedNo
            // 
            this.lblBedNo.AutoSize = true;
            this.lblBedNo.Location = new System.Drawing.Point(405, 70);
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
            this.txtBedNo.Location = new System.Drawing.Point(446, 68);
            this.txtBedNo.Name = "txtBedNo";
            this.txtBedNo.ReadOnly = true;
            this.txtBedNo.Size = new System.Drawing.Size(100, 21);
            this.txtBedNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtBedNo.TabIndex = 29;
            // 
            // lblDoctor
            // 
            this.lblDoctor.AutoSize = true;
            this.lblDoctor.Location = new System.Drawing.Point(204, 70);
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
            this.txtDoctor.Location = new System.Drawing.Point(272, 68);
            this.txtDoctor.Name = "txtDoctor";
            this.txtDoctor.ReadOnly = true;
            this.txtDoctor.Size = new System.Drawing.Size(100, 21);
            this.txtDoctor.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtDoctor.TabIndex = 27;
            // 
            // lblDateIn
            // 
            this.lblDateIn.AutoSize = true;
            this.lblDateIn.Location = new System.Drawing.Point(29, 70);
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
            this.txtDateIn.Location = new System.Drawing.Point(86, 68);
            this.txtDateIn.Name = "txtDateIn";
            this.txtDateIn.ReadOnly = true;
            this.txtDateIn.Size = new System.Drawing.Size(100, 21);
            this.txtDateIn.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtDateIn.TabIndex = 25;
            // 
            // lblNurceCell
            // 
            this.lblNurceCell.AutoSize = true;
            this.lblNurceCell.Location = new System.Drawing.Point(556, 36);
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
            this.txtNurseStation.Location = new System.Drawing.Point(611, 33);
            this.txtNurseStation.Name = "txtNurseStation";
            this.txtNurseStation.ReadOnly = true;
            this.txtNurseStation.Size = new System.Drawing.Size(100, 21);
            this.txtNurseStation.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtNurseStation.TabIndex = 23;
            // 
            // lblDept
            // 
            this.lblDept.AutoSize = true;
            this.lblDept.Location = new System.Drawing.Point(381, 36);
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
            this.txtDept.Location = new System.Drawing.Point(446, 33);
            this.txtDept.Name = "txtDept";
            this.txtDept.ReadOnly = true;
            this.txtDept.Size = new System.Drawing.Size(100, 21);
            this.txtDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtDept.TabIndex = 21;
            // 
            // lblPact
            // 
            this.lblPact.AutoSize = true;
            this.lblPact.Location = new System.Drawing.Point(204, 36);
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
            this.txtPact.Location = new System.Drawing.Point(272, 33);
            this.txtPact.Name = "txtPact";
            this.txtPact.ReadOnly = true;
            this.txtPact.Size = new System.Drawing.Size(100, 21);
            this.txtPact.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtPact.TabIndex = 19;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(29, 36);
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
            this.txtName.Location = new System.Drawing.Point(86, 33);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(100, 21);
            this.txtName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtName.TabIndex = 17;
            // 
            // gbPatientInfo
            // 
            this.gbPatientInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbPatientInfo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gbPatientInfo.Location = new System.Drawing.Point(14, 14);
            this.gbPatientInfo.Name = "gbPatientInfo";
            this.gbPatientInfo.Size = new System.Drawing.Size(766, 86);
            this.gbPatientInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbPatientInfo.TabIndex = 33;
            this.gbPatientInfo.TabStop = false;
            this.gbPatientInfo.Text = "患者信息";
            // 
            // pnlUP
            // 
            this.pnlUP.BackColor = System.Drawing.SystemColors.Control;
            this.pnlUP.Controls.Add(this.txtOldPact);
            this.pnlUP.Controls.Add(this.lblNewPact);
            this.pnlUP.Controls.Add(this.lblOldPact);
            this.pnlUP.Controls.Add(this.cmbNewPact);
            this.pnlUP.Controls.Add(this.ucQueryInpatientNo1);
            this.pnlUP.Controls.Add(this.gbPatientNO);
            this.pnlUP.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlUP.Location = new System.Drawing.Point(0, 0);
            this.pnlUP.Name = "pnlUP";
            this.pnlUP.Size = new System.Drawing.Size(791, 70);
            this.pnlUP.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlUP.TabIndex = 0;
            // 
            // txtOldPact
            // 
            this.txtOldPact.IsEnter2Tab = false;
            this.txtOldPact.Location = new System.Drawing.Point(325, 30);
            this.txtOldPact.Name = "txtOldPact";
            this.txtOldPact.ReadOnly = true;
            this.txtOldPact.Size = new System.Drawing.Size(137, 21);
            this.txtOldPact.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtOldPact.TabIndex = 5;
            // 
            // lblNewPact
            // 
            this.lblNewPact.AutoSize = true;
            this.lblNewPact.Location = new System.Drawing.Point(488, 34);
            this.lblNewPact.Name = "lblNewPact";
            this.lblNewPact.Size = new System.Drawing.Size(65, 12);
            this.lblNewPact.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblNewPact.TabIndex = 4;
            this.lblNewPact.Text = "新合同单位";
            // 
            // lblOldPact
            // 
            this.lblOldPact.AutoSize = true;
            this.lblOldPact.Location = new System.Drawing.Point(240, 33);
            this.lblOldPact.Name = "lblOldPact";
            this.lblOldPact.Size = new System.Drawing.Size(65, 12);
            this.lblOldPact.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblOldPact.TabIndex = 3;
            this.lblOldPact.Text = "原合同单位";
            // 
            // cmbNewPact
            // 
            this.cmbNewPact.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbNewPact.FormattingEnabled = true;
            this.cmbNewPact.IsEnter2Tab = false;
            this.cmbNewPact.IsFlat = false;
            this.cmbNewPact.IsLike = true;
            this.cmbNewPact.IsListOnly = false;
            this.cmbNewPact.IsPopForm = true;
            this.cmbNewPact.IsShowCustomerList = false;
            this.cmbNewPact.IsShowID = false;
            this.cmbNewPact.Location = new System.Drawing.Point(565, 28);
            this.cmbNewPact.Name = "cmbNewPact";
            this.cmbNewPact.PopForm = null;
            this.cmbNewPact.ShowCustomerList = false;
            this.cmbNewPact.ShowID = false;
            this.cmbNewPact.Size = new System.Drawing.Size(121, 20);
            this.cmbNewPact.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbNewPact.TabIndex = 2;
            this.cmbNewPact.Tag = "";
            this.cmbNewPact.ToolBarUse = false;
            this.cmbNewPact.SelectedIndexChanged += new System.EventHandler(this.cmbNewPact_SelectedIndexChanged);
            // 
            // ucQueryInpatientNo1
            // 
            this.ucQueryInpatientNo1.DefaultInputType = 0;
            this.ucQueryInpatientNo1.InputType = 0;
            this.ucQueryInpatientNo1.Location = new System.Drawing.Point(18, 23);
            this.ucQueryInpatientNo1.Name = "ucQueryInpatientNo1";
            this.ucQueryInpatientNo1.PatientInState = "ALL";
            this.ucQueryInpatientNo1.ShowState = FS.HISFC.Components.Common.Controls.enuShowState.All;
            this.ucQueryInpatientNo1.Size = new System.Drawing.Size(198, 27);
            this.ucQueryInpatientNo1.TabIndex = 0;
            this.ucQueryInpatientNo1.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(this.ucQueryInpatientNo1_myEvent);
            // 
            // gbPatientNO
            // 
            this.gbPatientNO.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbPatientNO.Location = new System.Drawing.Point(0, 0);
            this.gbPatientNO.Name = "gbPatientNO";
            this.gbPatientNO.Size = new System.Drawing.Size(791, 67);
            this.gbPatientNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbPatientNO.TabIndex = 6;
            this.gbPatientNO.TabStop = false;
            // 
            // ucChangePact
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Name = "ucChangePact";
            this.Size = new System.Drawing.Size(791, 518);
            this.Load += new System.EventHandler(this.ucChangePact_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlDown.ResumeLayout(false);
            this.pnlFeeInfo.ResumeLayout(false);
            this.tcFeeInfo.ResumeLayout(false);
            this.tpUndrug.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpUndrug)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpUndrug_Sheet1)).EndInit();
            this.tpDrug.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpDrug)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpDrug_Sheet1)).EndInit();
            this.pnlPatientInfo.ResumeLayout(false);
            this.pnlPatientInfo.PerformLayout();
            this.pnlUP.ResumeLayout(false);
            this.pnlUP.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel pnlMain;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlUP;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlDown;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbNewPact;
        private FS.HISFC.Components.Common.Controls.ucQueryInpatientNo ucQueryInpatientNo1;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblNewPact;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblOldPact;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlPatientInfo;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtOldPact;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbPatientNO;
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
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlFeeInfo;
        private System.Windows.Forms.TabPage tpUndrug;
        private System.Windows.Forms.TabPage tpDrug;
        protected FS.FrameWork.WinForms.Controls.NeuTabControl tcFeeInfo;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpUndrug;
        private FarPoint.Win.Spread.SheetView fpUndrug_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpDrug;
        private FarPoint.Win.Spread.SheetView fpDrug_Sheet1;
    }
}
