namespace FS.HISFC.Components.Common.Controls
{
    partial class ucPackageQuery
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
            FS.FrameWork.WinForms.Controls.NeuLabel lbCardType;
            FarPoint.Win.Spread.TipAppearance tipAppearance3 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.TipAppearance tipAppearance4 = new FarPoint.Win.Spread.TipAppearance();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.tbPact = new System.Windows.Forms.TextBox();
            this.tbLevel = new System.Windows.Forms.TextBox();
            this.tbSex = new System.Windows.Forms.TextBox();
            this.tbCardType = new System.Windows.Forms.TextBox();
            this.tbMedicalNO = new System.Windows.Forms.TextBox();
            this.lblMedicalNO = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblPact = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblLevel = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbPhone = new System.Windows.Forms.TextBox();
            this.lblPhone = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbIDNO = new System.Windows.Forms.TextBox();
            this.tbAge = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lbName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbSex = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbAge = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbRegDept = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.pnlPackageDetail = new System.Windows.Forms.Panel();
            this.fpPackageDetail = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpPackageDetail_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.pnlPackage = new System.Windows.Forms.Panel();
            this.fpPackage = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpPackage_Sheet1 = new FarPoint.Win.Spread.SheetView();
            lbCardType = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.pnlTop.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.pnlPackageDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpPackageDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPackageDetail_Sheet1)).BeginInit();
            this.pnlPackage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpPackage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPackage_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbCardType
            // 
            lbCardType.AutoSize = true;
            lbCardType.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            lbCardType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            lbCardType.Location = new System.Drawing.Point(548, 15);
            lbCardType.Name = "lbCardType";
            lbCardType.Size = new System.Drawing.Size(75, 14);
            lbCardType.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            lbCardType.TabIndex = 28;
            lbCardType.Text = "证件类型:";
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.pnlTop.Controls.Add(this.tbPact);
            this.pnlTop.Controls.Add(this.tbLevel);
            this.pnlTop.Controls.Add(this.tbSex);
            this.pnlTop.Controls.Add(this.tbCardType);
            this.pnlTop.Controls.Add(this.tbMedicalNO);
            this.pnlTop.Controls.Add(this.lblMedicalNO);
            this.pnlTop.Controls.Add(this.lblPact);
            this.pnlTop.Controls.Add(this.lblLevel);
            this.pnlTop.Controls.Add(this.tbPhone);
            this.pnlTop.Controls.Add(this.lblPhone);
            this.pnlTop.Controls.Add(this.tbIDNO);
            this.pnlTop.Controls.Add(this.tbAge);
            this.pnlTop.Controls.Add(this.tbName);
            this.pnlTop.Controls.Add(this.lbName);
            this.pnlTop.Controls.Add(this.lbSex);
            this.pnlTop.Controls.Add(this.lbAge);
            this.pnlTop.Controls.Add(this.lbRegDept);
            this.pnlTop.Controls.Add(lbCardType);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1100, 77);
            this.pnlTop.TabIndex = 0;
            // 
            // tbPact
            // 
            this.tbPact.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tbPact.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbPact.Location = new System.Drawing.Point(875, 45);
            this.tbPact.Name = "tbPact";
            this.tbPact.ReadOnly = true;
            this.tbPact.Size = new System.Drawing.Size(140, 14);
            this.tbPact.TabIndex = 46;
            this.tbPact.Text = "pact";
            // 
            // tbLevel
            // 
            this.tbLevel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tbLevel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbLevel.Location = new System.Drawing.Point(393, 45);
            this.tbLevel.Name = "tbLevel";
            this.tbLevel.ReadOnly = true;
            this.tbLevel.Size = new System.Drawing.Size(140, 14);
            this.tbLevel.TabIndex = 45;
            this.tbLevel.Text = "member";
            // 
            // tbSex
            // 
            this.tbSex.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tbSex.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbSex.Location = new System.Drawing.Point(110, 45);
            this.tbSex.Name = "tbSex";
            this.tbSex.ReadOnly = true;
            this.tbSex.Size = new System.Drawing.Size(49, 14);
            this.tbSex.TabIndex = 44;
            this.tbSex.Text = "sex";
            // 
            // tbCardType
            // 
            this.tbCardType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tbCardType.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbCardType.Location = new System.Drawing.Point(642, 16);
            this.tbCardType.Name = "tbCardType";
            this.tbCardType.ReadOnly = true;
            this.tbCardType.Size = new System.Drawing.Size(127, 14);
            this.tbCardType.TabIndex = 43;
            this.tbCardType.Text = "cardtype";
            // 
            // tbMedicalNO
            // 
            this.tbMedicalNO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tbMedicalNO.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbMedicalNO.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbMedicalNO.Location = new System.Drawing.Point(110, 14);
            this.tbMedicalNO.Name = "tbMedicalNO";
            this.tbMedicalNO.ReadOnly = true;
            this.tbMedicalNO.Size = new System.Drawing.Size(128, 16);
            this.tbMedicalNO.TabIndex = 25;
            this.tbMedicalNO.Tag = "MEDNO";
            this.tbMedicalNO.Text = "000000000";
            // 
            // lblMedicalNO
            // 
            this.lblMedicalNO.AutoSize = true;
            this.lblMedicalNO.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMedicalNO.ForeColor = System.Drawing.Color.Red;
            this.lblMedicalNO.Location = new System.Drawing.Point(16, 15);
            this.lblMedicalNO.Name = "lblMedicalNO";
            this.lblMedicalNO.Size = new System.Drawing.Size(76, 14);
            this.lblMedicalNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblMedicalNO.TabIndex = 24;
            this.lblMedicalNO.Text = "病 历 号:";
            // 
            // lblPact
            // 
            this.lblPact.AutoSize = true;
            this.lblPact.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPact.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lblPact.Location = new System.Drawing.Point(794, 46);
            this.lblPact.Name = "lblPact";
            this.lblPact.Size = new System.Drawing.Size(75, 14);
            this.lblPact.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblPact.TabIndex = 40;
            this.lblPact.Text = "合同单位:";
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLevel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lblLevel.Location = new System.Drawing.Point(312, 46);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(75, 14);
            this.lblLevel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblLevel.TabIndex = 36;
            this.lblLevel.Text = "会员类型:";
            // 
            // tbPhone
            // 
            this.tbPhone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tbPhone.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbPhone.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbPhone.Location = new System.Drawing.Point(642, 45);
            this.tbPhone.Name = "tbPhone";
            this.tbPhone.ReadOnly = true;
            this.tbPhone.Size = new System.Drawing.Size(127, 16);
            this.tbPhone.TabIndex = 39;
            this.tbPhone.Text = "phone";
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lblPhone.Location = new System.Drawing.Point(550, 46);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(75, 14);
            this.lblPhone.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblPhone.TabIndex = 38;
            this.lblPhone.Text = "联系电话:";
            // 
            // tbIDNO
            // 
            this.tbIDNO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tbIDNO.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbIDNO.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbIDNO.Location = new System.Drawing.Point(875, 14);
            this.tbIDNO.Name = "tbIDNO";
            this.tbIDNO.ReadOnly = true;
            this.tbIDNO.Size = new System.Drawing.Size(189, 16);
            this.tbIDNO.TabIndex = 31;
            this.tbIDNO.Text = "cardno";
            // 
            // tbAge
            // 
            this.tbAge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tbAge.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbAge.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbAge.Location = new System.Drawing.Point(234, 45);
            this.tbAge.Name = "tbAge";
            this.tbAge.ReadOnly = true;
            this.tbAge.Size = new System.Drawing.Size(66, 16);
            this.tbAge.TabIndex = 35;
            this.tbAge.Text = "age";
            // 
            // tbName
            // 
            this.tbName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tbName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbName.Location = new System.Drawing.Point(344, 14);
            this.tbName.Name = "tbName";
            this.tbName.ReadOnly = true;
            this.tbName.Size = new System.Drawing.Size(189, 16);
            this.tbName.TabIndex = 27;
            this.tbName.Text = "name";
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lbName.Location = new System.Drawing.Point(257, 15);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(77, 14);
            this.lbName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbName.TabIndex = 26;
            this.lbName.Text = "姓    名:";
            // 
            // lbSex
            // 
            this.lbSex.AutoSize = true;
            this.lbSex.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbSex.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lbSex.Location = new System.Drawing.Point(15, 46);
            this.lbSex.Name = "lbSex";
            this.lbSex.Size = new System.Drawing.Size(77, 14);
            this.lbSex.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbSex.TabIndex = 32;
            this.lbSex.Text = "性    别:";
            // 
            // lbAge
            // 
            this.lbAge.AutoSize = true;
            this.lbAge.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbAge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lbAge.Location = new System.Drawing.Point(179, 46);
            this.lbAge.Name = "lbAge";
            this.lbAge.Size = new System.Drawing.Size(45, 14);
            this.lbAge.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbAge.TabIndex = 34;
            this.lbAge.Text = "年龄:";
            // 
            // lbRegDept
            // 
            this.lbRegDept.AutoSize = true;
            this.lbRegDept.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbRegDept.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lbRegDept.Location = new System.Drawing.Point(794, 15);
            this.lbRegDept.Name = "lbRegDept";
            this.lbRegDept.Size = new System.Drawing.Size(75, 14);
            this.lbRegDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbRegDept.TabIndex = 30;
            this.lbRegDept.Text = "证件号码:";
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.pnlBottom.Controls.Add(this.btnOK);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 530);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1100, 49);
            this.pnlBottom.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(984, 10);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 30);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // pnlPackageDetail
            // 
            this.pnlPackageDetail.Controls.Add(this.fpPackageDetail);
            this.pnlPackageDetail.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlPackageDetail.Location = new System.Drawing.Point(315, 77);
            this.pnlPackageDetail.Name = "pnlPackageDetail";
            this.pnlPackageDetail.Size = new System.Drawing.Size(785, 453);
            this.pnlPackageDetail.TabIndex = 3;
            this.pnlPackageDetail.Visible = false;
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
            this.fpPackageDetail.Size = new System.Drawing.Size(785, 453);
            this.fpPackageDetail.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpPackageDetail.TabIndex = 1;
            tipAppearance3.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance3.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpPackageDetail.TextTipAppearance = tipAppearance3;
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
            this.fpPackageDetail_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "单位";
            this.fpPackageDetail_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "可开数量";
            this.fpPackageDetail_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "金额";
            this.fpPackageDetail_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "实付金额";
            this.fpPackageDetail_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "赠送金额";
            this.fpPackageDetail_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "优惠金额";
            this.fpPackageDetail_Sheet1.Columns.Get(0).Label = "项目";
            this.fpPackageDetail_Sheet1.Columns.Get(0).Locked = false;
            this.fpPackageDetail_Sheet1.Columns.Get(0).Width = 337F;
            this.fpPackageDetail_Sheet1.Columns.Get(1).Label = "总数量";
            this.fpPackageDetail_Sheet1.Columns.Get(1).Locked = false;
            this.fpPackageDetail_Sheet1.Columns.Get(1).Width = 91F;
            this.fpPackageDetail_Sheet1.Columns.Get(2).Label = "单位";
            this.fpPackageDetail_Sheet1.Columns.Get(2).Locked = false;
            this.fpPackageDetail_Sheet1.Columns.Get(2).Width = 55F;
            this.fpPackageDetail_Sheet1.Columns.Get(3).Label = "可开数量";
            this.fpPackageDetail_Sheet1.Columns.Get(3).Locked = false;
            this.fpPackageDetail_Sheet1.Columns.Get(3).Width = 75F;
            this.fpPackageDetail_Sheet1.Columns.Get(4).Label = "金额";
            this.fpPackageDetail_Sheet1.Columns.Get(4).Locked = false;
            this.fpPackageDetail_Sheet1.Columns.Get(4).Width = 75F;
            this.fpPackageDetail_Sheet1.Columns.Get(5).Label = "实付金额";
            this.fpPackageDetail_Sheet1.Columns.Get(5).Locked = false;
            this.fpPackageDetail_Sheet1.Columns.Get(5).Width = 77F;
            this.fpPackageDetail_Sheet1.Columns.Get(6).Label = "赠送金额";
            this.fpPackageDetail_Sheet1.Columns.Get(6).Locked = false;
            this.fpPackageDetail_Sheet1.Columns.Get(6).Width = 74F;
            this.fpPackageDetail_Sheet1.Columns.Get(7).Label = "优惠金额";
            this.fpPackageDetail_Sheet1.Columns.Get(7).Locked = true;
            this.fpPackageDetail_Sheet1.Columns.Get(7).Width = 78F;
            this.fpPackageDetail_Sheet1.DefaultStyle.Locked = false;
            this.fpPackageDetail_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpPackageDetail_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.fpPackageDetail_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
            this.fpPackageDetail_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpPackageDetail_Sheet1.RowHeader.Columns.Get(0).Width = 0F;
            this.fpPackageDetail_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpPackageDetail_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpPackageDetail_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpPackageDetail_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpPackageDetail_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpPackageDetail.SetActiveViewport(0, 1, 0);
            // 
            // pnlPackage
            // 
            this.pnlPackage.Controls.Add(this.fpPackage);
            this.pnlPackage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPackage.Location = new System.Drawing.Point(0, 77);
            this.pnlPackage.Name = "pnlPackage";
            this.pnlPackage.Size = new System.Drawing.Size(315, 453);
            this.pnlPackage.TabIndex = 4;
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
            this.fpPackage.Size = new System.Drawing.Size(315, 453);
            this.fpPackage.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpPackage.TabIndex = 1;
            tipAppearance4.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance4.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpPackage.TextTipAppearance = tipAppearance4;
            this.fpPackage.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpPackage_Sheet1
            // 
            this.fpPackage_Sheet1.Reset();
            this.fpPackage_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpPackage_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpPackage_Sheet1.ColumnCount = 5;
            this.fpPackage_Sheet1.RowCount = 0;
            this.fpPackage_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "套餐名称";
            this.fpPackage_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "总金额";
            this.fpPackage_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "实收金额";
            this.fpPackage_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "赠送金额";
            this.fpPackage_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "优惠金额";
            this.fpPackage_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.fpPackage_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpPackage_Sheet1.Columns.Get(0).Label = "套餐名称";
            this.fpPackage_Sheet1.Columns.Get(0).Width = 337F;
            this.fpPackage_Sheet1.Columns.Get(1).Label = "总金额";
            this.fpPackage_Sheet1.Columns.Get(1).Width = 70F;
            this.fpPackage_Sheet1.Columns.Get(2).Label = "实收金额";
            this.fpPackage_Sheet1.Columns.Get(2).Width = 70F;
            this.fpPackage_Sheet1.Columns.Get(3).Label = "赠送金额";
            this.fpPackage_Sheet1.Columns.Get(3).Width = 70F;
            this.fpPackage_Sheet1.Columns.Get(4).Label = "优惠金额";
            this.fpPackage_Sheet1.Columns.Get(4).Width = 70F;
            this.fpPackage_Sheet1.DefaultStyle.Locked = false;
            this.fpPackage_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpPackage_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.fpPackage_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
            this.fpPackage_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpPackage_Sheet1.RowHeader.Columns.Get(0).Width = 0F;
            this.fpPackage_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpPackage_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpPackage_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpPackage_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpPackage_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpPackage.SetActiveViewport(0, 1, 0);
            // 
            // frmPackageQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.ClientSize = new System.Drawing.Size(1100, 579);
            this.Controls.Add(this.pnlPackage);
            this.Controls.Add(this.pnlPackageDetail);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);
            this.Name = "ucPackageQuery";
            this.Text = "套餐操作";
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.pnlPackageDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpPackageDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPackageDetail_Sheet1)).EndInit();
            this.pnlPackage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpPackage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPackage_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Panel pnlPackageDetail;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpPackageDetail;
        private FarPoint.Win.Spread.SheetView fpPackageDetail_Sheet1;
        protected System.Windows.Forms.TextBox tbMedicalNO;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblMedicalNO;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblPact;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblLevel;
        protected System.Windows.Forms.TextBox tbPhone;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblPhone;
        protected System.Windows.Forms.TextBox tbIDNO;
        protected System.Windows.Forms.TextBox tbAge;
        protected System.Windows.Forms.TextBox tbName;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbName;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbSex;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbAge;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbRegDept;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox tbLevel;
        private System.Windows.Forms.TextBox tbSex;
        private System.Windows.Forms.TextBox tbCardType;
        private System.Windows.Forms.TextBox tbPact;
        private System.Windows.Forms.Panel pnlPackage;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpPackage;
        private FarPoint.Win.Spread.SheetView fpPackage_Sheet1;

    }
}