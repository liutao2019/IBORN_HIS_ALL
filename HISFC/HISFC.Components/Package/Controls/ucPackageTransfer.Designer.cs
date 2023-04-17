namespace HISFC.Components.Package.Controls
{
    partial class ucPackageTransfer
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
            FS.FrameWork.WinForms.Controls.NeuLabel lbCardType;
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType2 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.tbPact = new System.Windows.Forms.TextBox();
            this.tbLevel = new System.Windows.Forms.TextBox();
            this.tbSex = new System.Windows.Forms.TextBox();
            this.tbCardType = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblMedicalNO = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbMedicalNO = new System.Windows.Forms.TextBox();
            this.lblPact = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblLevel = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.tbPhone = new System.Windows.Forms.TextBox();
            this.lblPhone = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbIDNO = new System.Windows.Forms.TextBox();
            this.tbAge = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lbName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbSex = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbAge = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.cmbdept2 = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbdept = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.txtCardNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lbCardNO = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbRegDept = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.pnlPackage = new System.Windows.Forms.Panel();
            this.fpPackageDetail = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpPackageDetail_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.fpPackage = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpPackage_Sheet1 = new FarPoint.Win.Spread.SheetView();
            lbCardType = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.pnlBottom.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.pnlPackage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpPackageDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPackageDetail_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPackage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPackage_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbCardType
            // 
            lbCardType.AutoSize = true;
            lbCardType.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            lbCardType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            lbCardType.Location = new System.Drawing.Point(609, 15);
            lbCardType.Name = "lbCardType";
            lbCardType.Size = new System.Drawing.Size(75, 14);
            lbCardType.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            lbCardType.TabIndex = 28;
            lbCardType.Text = "证件类型:";
            // 
            // tbPact
            // 
            this.tbPact.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tbPact.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbPact.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbPact.Location = new System.Drawing.Point(1021, 15);
            this.tbPact.Name = "tbPact";
            this.tbPact.ReadOnly = true;
            this.tbPact.Size = new System.Drawing.Size(70, 16);
            this.tbPact.TabIndex = 46;
            this.tbPact.Text = "________";
            // 
            // tbLevel
            // 
            this.tbLevel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tbLevel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbLevel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbLevel.Location = new System.Drawing.Point(1021, 53);
            this.tbLevel.Name = "tbLevel";
            this.tbLevel.ReadOnly = true;
            this.tbLevel.Size = new System.Drawing.Size(76, 16);
            this.tbLevel.TabIndex = 45;
            this.tbLevel.Text = "________";
            // 
            // tbSex
            // 
            this.tbSex.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tbSex.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbSex.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbSex.Location = new System.Drawing.Point(890, 53);
            this.tbSex.Name = "tbSex";
            this.tbSex.ReadOnly = true;
            this.tbSex.Size = new System.Drawing.Size(49, 16);
            this.tbSex.TabIndex = 44;
            this.tbSex.Text = "________";
            // 
            // tbCardType
            // 
            this.tbCardType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tbCardType.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbCardType.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbCardType.Location = new System.Drawing.Point(687, 15);
            this.tbCardType.Name = "tbCardType";
            this.tbCardType.ReadOnly = true;
            this.tbCardType.Size = new System.Drawing.Size(92, 16);
            this.tbCardType.TabIndex = 43;
            this.tbCardType.Text = "________";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(1061, 10);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 30);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "核销";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Visible = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblMedicalNO
            // 
            this.lblMedicalNO.AutoSize = true;
            this.lblMedicalNO.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMedicalNO.ForeColor = System.Drawing.Color.Red;
            this.lblMedicalNO.Location = new System.Drawing.Point(205, 15);
            this.lblMedicalNO.Name = "lblMedicalNO";
            this.lblMedicalNO.Size = new System.Drawing.Size(77, 14);
            this.lblMedicalNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblMedicalNO.TabIndex = 24;
            this.lblMedicalNO.Text = "卡    号:";
            // 
            // tbMedicalNO
            // 
            this.tbMedicalNO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tbMedicalNO.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbMedicalNO.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbMedicalNO.Location = new System.Drawing.Point(285, 15);
            this.tbMedicalNO.Name = "tbMedicalNO";
            this.tbMedicalNO.ReadOnly = true;
            this.tbMedicalNO.Size = new System.Drawing.Size(121, 16);
            this.tbMedicalNO.TabIndex = 25;
            this.tbMedicalNO.Tag = "MEDNO";
            this.tbMedicalNO.Text = "___________________________";
            // 
            // lblPact
            // 
            this.lblPact.AutoSize = true;
            this.lblPact.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPact.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lblPact.Location = new System.Drawing.Point(943, 15);
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
            this.lblLevel.Location = new System.Drawing.Point(943, 52);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(75, 14);
            this.lblLevel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblLevel.TabIndex = 36;
            this.lblLevel.Text = "会员类型:";
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.pnlBottom.Controls.Add(this.btnOK);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 488);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1177, 49);
            this.pnlBottom.TabIndex = 6;
            // 
            // tbPhone
            // 
            this.tbPhone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tbPhone.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbPhone.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbPhone.Location = new System.Drawing.Point(504, 50);
            this.tbPhone.Name = "tbPhone";
            this.tbPhone.ReadOnly = true;
            this.tbPhone.Size = new System.Drawing.Size(106, 16);
            this.tbPhone.TabIndex = 39;
            this.tbPhone.Text = "________";
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lblPhone.Location = new System.Drawing.Point(421, 52);
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
            this.tbIDNO.Location = new System.Drawing.Point(687, 52);
            this.tbIDNO.Name = "tbIDNO";
            this.tbIDNO.ReadOnly = true;
            this.tbIDNO.Size = new System.Drawing.Size(103, 16);
            this.tbIDNO.TabIndex = 31;
            this.tbIDNO.Text = "________";
            // 
            // tbAge
            // 
            this.tbAge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tbAge.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbAge.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbAge.Location = new System.Drawing.Point(890, 15);
            this.tbAge.Name = "tbAge";
            this.tbAge.ReadOnly = true;
            this.tbAge.Size = new System.Drawing.Size(66, 16);
            this.tbAge.TabIndex = 35;
            this.tbAge.Text = "_______";
            // 
            // tbName
            // 
            this.tbName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tbName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbName.Location = new System.Drawing.Point(504, 15);
            this.tbName.Name = "tbName";
            this.tbName.ReadOnly = true;
            this.tbName.Size = new System.Drawing.Size(72, 16);
            this.tbName.TabIndex = 27;
            this.tbName.Text = "________";
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lbName.Location = new System.Drawing.Point(421, 15);
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
            this.lbSex.Location = new System.Drawing.Point(803, 52);
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
            this.lbAge.Location = new System.Drawing.Point(803, 15);
            this.lbAge.Name = "lbAge";
            this.lbAge.Size = new System.Drawing.Size(77, 14);
            this.lbAge.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbAge.TabIndex = 34;
            this.lbAge.Text = "年    龄:";
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.pnlTop.Controls.Add(this.cmbdept2);
            this.pnlTop.Controls.Add(this.neuLabel2);
            this.pnlTop.Controls.Add(this.cmbdept);
            this.pnlTop.Controls.Add(this.txtCardNO);
            this.pnlTop.Controls.Add(this.lbCardNO);
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
            this.pnlTop.Controls.Add(this.neuLabel1);
            this.pnlTop.Controls.Add(this.lbName);
            this.pnlTop.Controls.Add(this.lbSex);
            this.pnlTop.Controls.Add(this.lbAge);
            this.pnlTop.Controls.Add(this.lbRegDept);
            this.pnlTop.Controls.Add(lbCardType);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1177, 84);
            this.pnlTop.TabIndex = 5;
            // 
            // cmbdept2
            // 
            this.cmbdept2.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbdept2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbdept2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbdept2.FormattingEnabled = true;
            this.cmbdept2.IsEnter2Tab = false;
            this.cmbdept2.IsFlat = false;
            this.cmbdept2.IsLike = true;
            this.cmbdept2.IsListOnly = false;
            this.cmbdept2.IsPopForm = true;
            this.cmbdept2.IsShowCustomerList = false;
            this.cmbdept2.IsShowID = false;
            this.cmbdept2.IsShowIDAndName = false;
            this.cmbdept2.Location = new System.Drawing.Point(87, 47);
            this.cmbdept2.Name = "cmbdept2";
            this.cmbdept2.ShowCustomerList = false;
            this.cmbdept2.ShowID = false;
            this.cmbdept2.Size = new System.Drawing.Size(114, 22);
            this.cmbdept2.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbdept2.TabIndex = 51;
            this.cmbdept2.Tag = "";
            this.cmbdept2.ToolBarUse = false;
            this.cmbdept2.SelectedValueChanged += new System.EventHandler(this.cmbdept2_SelectedValueChanged);
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.neuLabel2.Location = new System.Drawing.Point(9, 52);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(75, 14);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 50;
            this.neuLabel2.Text = "套餐科室:";
            // 
            // cmbdept
            // 
            this.cmbdept.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbdept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbdept.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbdept.FormattingEnabled = true;
            this.cmbdept.IsEnter2Tab = false;
            this.cmbdept.IsFlat = false;
            this.cmbdept.IsLike = true;
            this.cmbdept.IsListOnly = false;
            this.cmbdept.IsPopForm = true;
            this.cmbdept.IsShowCustomerList = false;
            this.cmbdept.IsShowID = false;
            this.cmbdept.IsShowIDAndName = false;
            this.cmbdept.Location = new System.Drawing.Point(283, 47);
            this.cmbdept.Name = "cmbdept";
            this.cmbdept.ShowCustomerList = false;
            this.cmbdept.ShowID = false;
            this.cmbdept.Size = new System.Drawing.Size(123, 22);
            this.cmbdept.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbdept.TabIndex = 49;
            this.cmbdept.Tag = "";
            this.cmbdept.ToolBarUse = false;
            this.cmbdept.SelectedValueChanged += new System.EventHandler(this.cmbdept_SelectedValueChanged);
            // 
            // txtCardNO
            // 
            this.txtCardNO.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCardNO.IsEnter2Tab = false;
            this.txtCardNO.Location = new System.Drawing.Point(88, 8);
            this.txtCardNO.Name = "txtCardNO";
            this.txtCardNO.Size = new System.Drawing.Size(113, 23);
            this.txtCardNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCardNO.TabIndex = 48;
            this.txtCardNO.Tag = "CARDNO";
            this.txtCardNO.TextChanged += new System.EventHandler(this.txtCardNO_TextChanged);
            this.txtCardNO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCardNO_KeyDown);
            // 
            // lbCardNO
            // 
            this.lbCardNO.AutoSize = true;
            this.lbCardNO.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbCardNO.ForeColor = System.Drawing.Color.Red;
            this.lbCardNO.Location = new System.Drawing.Point(10, 15);
            this.lbCardNO.Name = "lbCardNO";
            this.lbCardNO.Size = new System.Drawing.Size(75, 14);
            this.lbCardNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbCardNO.TabIndex = 47;
            this.lbCardNO.Text = "信息检索:";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.neuLabel1.Location = new System.Drawing.Point(207, 52);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(75, 14);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 26;
            this.neuLabel1.Text = "核销科室:";
            // 
            // lbRegDept
            // 
            this.lbRegDept.AutoSize = true;
            this.lbRegDept.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbRegDept.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lbRegDept.Location = new System.Drawing.Point(606, 52);
            this.lbRegDept.Name = "lbRegDept";
            this.lbRegDept.Size = new System.Drawing.Size(75, 14);
            this.lbRegDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbRegDept.TabIndex = 30;
            this.lbRegDept.Text = "证件号码:";
            // 
            // pnlPackage
            // 
            this.pnlPackage.Controls.Add(this.fpPackageDetail);
            this.pnlPackage.Controls.Add(this.fpPackage);
            this.pnlPackage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPackage.Location = new System.Drawing.Point(0, 84);
            this.pnlPackage.Name = "pnlPackage";
            this.pnlPackage.Size = new System.Drawing.Size(1177, 404);
            this.pnlPackage.TabIndex = 8;
            // 
            // fpPackageDetail
            // 
            this.fpPackageDetail.About = "3.0.2004.2005";
            this.fpPackageDetail.AccessibleDescription = "fpPackageDetail, Sheet1, Row 0, Column 0, ";
            this.fpPackageDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.fpPackageDetail.BackColor = System.Drawing.Color.White;
            this.fpPackageDetail.EditModePermanent = true;
            this.fpPackageDetail.EditModeReplace = true;
            this.fpPackageDetail.FileName = "";
            this.fpPackageDetail.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpPackageDetail.IsAutoSaveGridStatus = false;
            this.fpPackageDetail.IsCanCustomConfigColumn = false;
            this.fpPackageDetail.Location = new System.Drawing.Point(529, 6);
            this.fpPackageDetail.Name = "fpPackageDetail";
            this.fpPackageDetail.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpPackageDetail.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpPackageDetail_Sheet1});
            this.fpPackageDetail.Size = new System.Drawing.Size(645, 392);
            this.fpPackageDetail.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpPackageDetail.TabIndex = 1;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpPackageDetail.TextTipAppearance = tipAppearance1;
            this.fpPackageDetail.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpPackageDetail.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpPackageDetail_CellClick);
            // 
            // fpPackageDetail_Sheet1
            // 
            this.fpPackageDetail_Sheet1.Reset();
            this.fpPackageDetail_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpPackageDetail_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpPackageDetail_Sheet1.ColumnCount = 9;
            this.fpPackageDetail_Sheet1.RowCount = 0;
            this.fpPackageDetail_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "全选";
            this.fpPackageDetail_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "项目";
            this.fpPackageDetail_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "总数量";
            this.fpPackageDetail_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "单位";
            this.fpPackageDetail_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "可开数量";
            this.fpPackageDetail_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "金额";
            this.fpPackageDetail_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "实付金额";
            this.fpPackageDetail_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "赠送金额";
            this.fpPackageDetail_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "优惠金额";
            this.fpPackageDetail_Sheet1.Columns.Get(0).CellType = checkBoxCellType1;
            this.fpPackageDetail_Sheet1.Columns.Get(0).Label = "全选";
            this.fpPackageDetail_Sheet1.Columns.Get(1).Label = "项目";
            this.fpPackageDetail_Sheet1.Columns.Get(1).Locked = false;
            this.fpPackageDetail_Sheet1.Columns.Get(1).Width = 337F;
            this.fpPackageDetail_Sheet1.Columns.Get(2).Label = "总数量";
            this.fpPackageDetail_Sheet1.Columns.Get(2).Locked = false;
            this.fpPackageDetail_Sheet1.Columns.Get(2).Width = 91F;
            this.fpPackageDetail_Sheet1.Columns.Get(3).Label = "单位";
            this.fpPackageDetail_Sheet1.Columns.Get(3).Locked = false;
            this.fpPackageDetail_Sheet1.Columns.Get(3).Width = 55F;
            this.fpPackageDetail_Sheet1.Columns.Get(4).Label = "可开数量";
            this.fpPackageDetail_Sheet1.Columns.Get(4).Locked = false;
            this.fpPackageDetail_Sheet1.Columns.Get(4).Width = 75F;
            this.fpPackageDetail_Sheet1.Columns.Get(5).Label = "金额";
            this.fpPackageDetail_Sheet1.Columns.Get(5).Locked = false;
            this.fpPackageDetail_Sheet1.Columns.Get(5).Width = 75F;
            this.fpPackageDetail_Sheet1.Columns.Get(6).Label = "实付金额";
            this.fpPackageDetail_Sheet1.Columns.Get(6).Locked = false;
            this.fpPackageDetail_Sheet1.Columns.Get(6).Width = 77F;
            this.fpPackageDetail_Sheet1.Columns.Get(7).Label = "赠送金额";
            this.fpPackageDetail_Sheet1.Columns.Get(7).Locked = false;
            this.fpPackageDetail_Sheet1.Columns.Get(7).Width = 74F;
            this.fpPackageDetail_Sheet1.Columns.Get(8).Label = "优惠金额";
            this.fpPackageDetail_Sheet1.Columns.Get(8).Locked = true;
            this.fpPackageDetail_Sheet1.Columns.Get(8).Width = 78F;
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
            // fpPackage
            // 
            this.fpPackage.About = "3.0.2004.2005";
            this.fpPackage.AccessibleDescription = "fpPackage, Sheet1, Row 0, Column 0, ";
            this.fpPackage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.fpPackage.BackColor = System.Drawing.Color.White;
            this.fpPackage.FileName = "";
            this.fpPackage.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpPackage.IsAutoSaveGridStatus = false;
            this.fpPackage.IsCanCustomConfigColumn = false;
            this.fpPackage.Location = new System.Drawing.Point(3, 6);
            this.fpPackage.Name = "fpPackage";
            this.fpPackage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpPackage.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpPackage_Sheet1});
            this.fpPackage.Size = new System.Drawing.Size(526, 395);
            this.fpPackage.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpPackage.TabIndex = 1;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpPackage.TextTipAppearance = tipAppearance2;
            this.fpPackage.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpPackage.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpPackage_CellClick);
            this.fpPackage.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(this.fpPackage_SelectionChanged);
            // 
            // fpPackage_Sheet1
            // 
            this.fpPackage_Sheet1.Reset();
            this.fpPackage_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpPackage_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpPackage_Sheet1.ColumnCount = 7;
            this.fpPackage_Sheet1.RowCount = 0;
            this.fpPackage_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "全选";
            this.fpPackage_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "套餐名称";
            this.fpPackage_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "总金额";
            this.fpPackage_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "实收金额";
            this.fpPackage_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "购买时间";
            this.fpPackage_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "赠送金额";
            this.fpPackage_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "优惠金额";
            this.fpPackage_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.fpPackage_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpPackage_Sheet1.Columns.Get(0).CellType = checkBoxCellType2;
            this.fpPackage_Sheet1.Columns.Get(0).Label = "全选";
            this.fpPackage_Sheet1.Columns.Get(0).Width = 36F;
            this.fpPackage_Sheet1.Columns.Get(1).Label = "套餐名称";
            this.fpPackage_Sheet1.Columns.Get(1).Width = 256F;
            this.fpPackage_Sheet1.Columns.Get(2).Label = "总金额";
            this.fpPackage_Sheet1.Columns.Get(2).Visible = false;
            this.fpPackage_Sheet1.Columns.Get(2).Width = 70F;
            this.fpPackage_Sheet1.Columns.Get(3).Label = "实收金额";
            this.fpPackage_Sheet1.Columns.Get(3).Width = 69F;
            this.fpPackage_Sheet1.Columns.Get(4).Label = "购买时间";
            this.fpPackage_Sheet1.Columns.Get(4).Width = 160F;
            this.fpPackage_Sheet1.Columns.Get(5).Label = "赠送金额";
            this.fpPackage_Sheet1.Columns.Get(5).Visible = false;
            this.fpPackage_Sheet1.Columns.Get(5).Width = 70F;
            this.fpPackage_Sheet1.Columns.Get(6).Label = "优惠金额";
            this.fpPackage_Sheet1.Columns.Get(6).Visible = false;
            this.fpPackage_Sheet1.Columns.Get(6).Width = 70F;
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
            // ucPackageTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlPackage);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);
            this.Name = "ucPackageTransfer";
            this.Size = new System.Drawing.Size(1177, 537);
            this.Load += new System.EventHandler(this.ucPackageTransfer_Load);
            this.pnlBottom.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlPackage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpPackageDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPackageDetail_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPackage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPackage_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbPact;
        private System.Windows.Forms.TextBox tbLevel;
        private System.Windows.Forms.TextBox tbSex;
        private System.Windows.Forms.TextBox tbCardType;
        private System.Windows.Forms.Button btnOK;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblMedicalNO;
        protected System.Windows.Forms.TextBox tbMedicalNO;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblPact;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblLevel;
        private System.Windows.Forms.Panel pnlBottom;
        protected System.Windows.Forms.TextBox tbPhone;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblPhone;
        protected System.Windows.Forms.TextBox tbIDNO;
        protected System.Windows.Forms.TextBox tbAge;
        protected System.Windows.Forms.TextBox tbName;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbName;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbSex;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbAge;
        private System.Windows.Forms.Panel pnlTop;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbRegDept;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox txtCardNO;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbCardNO;
        private System.Windows.Forms.Panel pnlPackage;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpPackage;
        private FarPoint.Win.Spread.SheetView fpPackage_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpPackageDetail;
        private FarPoint.Win.Spread.SheetView fpPackageDetail_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbdept;
        protected FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbdept2;
        protected FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;




    }
}
