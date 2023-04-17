namespace FS.HISFC.Components.InpatientFee.Maintenance
{
    partial class ucPatientCardTicket
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
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.txtCardNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lbCardNO = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbSex = new System.Windows.Forms.TextBox();
            this.tbCardType = new System.Windows.Forms.TextBox();
            this.tbMedicalNO = new System.Windows.Forms.TextBox();
            this.lblMedicalNO = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbPhone = new System.Windows.Forms.TextBox();
            this.lblPhone = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbIDNO = new System.Windows.Forms.TextBox();
            this.tbAge = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lbName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbSex = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbAge = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbRegDept = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.fpSpread2 = new FarPoint.Win.Spread.FpSpread();
            this.fpSpread2_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnGetCardNo = new System.Windows.Forms.Button();
            this.lblTip = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNewCardNo = new System.Windows.Forms.TextBox();
            this.cmbOper = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCardKind = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            lbCardType = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.groupBox1.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread2_Sheet1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbCardType
            // 
            lbCardType.AutoSize = true;
            lbCardType.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            lbCardType.ForeColor = System.Drawing.Color.Black;
            lbCardType.Location = new System.Drawing.Point(29, 48);
            lbCardType.Name = "lbCardType";
            lbCardType.Size = new System.Drawing.Size(59, 12);
            lbCardType.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            lbCardType.TabIndex = 28;
            lbCardType.Text = "证件类型:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pnlTop);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(869, 90);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "个人信息";
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.pnlTop.Controls.Add(this.txtCardNO);
            this.pnlTop.Controls.Add(this.lbCardNO);
            this.pnlTop.Controls.Add(this.tbSex);
            this.pnlTop.Controls.Add(this.tbCardType);
            this.pnlTop.Controls.Add(this.tbMedicalNO);
            this.pnlTop.Controls.Add(this.lblMedicalNO);
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
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTop.Location = new System.Drawing.Point(3, 17);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(863, 70);
            this.pnlTop.TabIndex = 6;
            // 
            // txtCardNO
            // 
            this.txtCardNO.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCardNO.IsEnter2Tab = false;
            this.txtCardNO.Location = new System.Drawing.Point(110, 9);
            this.txtCardNO.Name = "txtCardNO";
            this.txtCardNO.Size = new System.Drawing.Size(133, 23);
            this.txtCardNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCardNO.TabIndex = 48;
            this.txtCardNO.TabStop = false;
            this.txtCardNO.Tag = "CARDNO";
            this.txtCardNO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCardNO_KeyDown);
            // 
            // lbCardNO
            // 
            this.lbCardNO.AutoSize = true;
            this.lbCardNO.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbCardNO.ForeColor = System.Drawing.Color.Blue;
            this.lbCardNO.Location = new System.Drawing.Point(1, 13);
            this.lbCardNO.Name = "lbCardNO";
            this.lbCardNO.Size = new System.Drawing.Size(105, 14);
            this.lbCardNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbCardNO.TabIndex = 47;
            this.lbCardNO.Text = "个人信息检索:";
            // 
            // tbSex
            // 
            this.tbSex.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tbSex.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbSex.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbSex.Location = new System.Drawing.Point(786, 11);
            this.tbSex.Name = "tbSex";
            this.tbSex.ReadOnly = true;
            this.tbSex.Size = new System.Drawing.Size(49, 14);
            this.tbSex.TabIndex = 44;
            this.tbSex.TabStop = false;
            // 
            // tbCardType
            // 
            this.tbCardType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tbCardType.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbCardType.Location = new System.Drawing.Point(110, 49);
            this.tbCardType.Name = "tbCardType";
            this.tbCardType.ReadOnly = true;
            this.tbCardType.Size = new System.Drawing.Size(86, 14);
            this.tbCardType.TabIndex = 43;
            this.tbCardType.TabStop = false;
            // 
            // tbMedicalNO
            // 
            this.tbMedicalNO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tbMedicalNO.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbMedicalNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbMedicalNO.Location = new System.Drawing.Point(305, 12);
            this.tbMedicalNO.Name = "tbMedicalNO";
            this.tbMedicalNO.ReadOnly = true;
            this.tbMedicalNO.Size = new System.Drawing.Size(95, 14);
            this.tbMedicalNO.TabIndex = 25;
            this.tbMedicalNO.TabStop = false;
            this.tbMedicalNO.Tag = "MEDNO";
            // 
            // lblMedicalNO
            // 
            this.lblMedicalNO.AutoSize = true;
            this.lblMedicalNO.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMedicalNO.ForeColor = System.Drawing.Color.Blue;
            this.lblMedicalNO.Location = new System.Drawing.Point(249, 13);
            this.lblMedicalNO.Name = "lblMedicalNO";
            this.lblMedicalNO.Size = new System.Drawing.Size(45, 14);
            this.lblMedicalNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblMedicalNO.TabIndex = 24;
            this.lblMedicalNO.Text = "卡号:";
            // 
            // tbPhone
            // 
            this.tbPhone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tbPhone.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbPhone.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbPhone.Location = new System.Drawing.Point(559, 43);
            this.tbPhone.Name = "tbPhone";
            this.tbPhone.ReadOnly = true;
            this.tbPhone.Size = new System.Drawing.Size(106, 14);
            this.tbPhone.TabIndex = 39;
            this.tbPhone.TabStop = false;
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPhone.ForeColor = System.Drawing.Color.Black;
            this.lblPhone.Location = new System.Drawing.Point(467, 44);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(59, 12);
            this.lblPhone.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblPhone.TabIndex = 38;
            this.lblPhone.Text = "联系电话:";
            // 
            // tbIDNO
            // 
            this.tbIDNO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tbIDNO.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbIDNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbIDNO.Location = new System.Drawing.Point(305, 46);
            this.tbIDNO.Name = "tbIDNO";
            this.tbIDNO.ReadOnly = true;
            this.tbIDNO.Size = new System.Drawing.Size(151, 14);
            this.tbIDNO.TabIndex = 31;
            this.tbIDNO.TabStop = false;
            // 
            // tbAge
            // 
            this.tbAge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tbAge.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbAge.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbAge.Location = new System.Drawing.Point(786, 44);
            this.tbAge.Name = "tbAge";
            this.tbAge.ReadOnly = true;
            this.tbAge.Size = new System.Drawing.Size(49, 14);
            this.tbAge.TabIndex = 35;
            this.tbAge.TabStop = false;
            // 
            // tbName
            // 
            this.tbName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tbName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbName.Location = new System.Drawing.Point(559, 13);
            this.tbName.Name = "tbName";
            this.tbName.ReadOnly = true;
            this.tbName.Size = new System.Drawing.Size(156, 14);
            this.tbName.TabIndex = 27;
            this.tbName.TabStop = false;
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbName.ForeColor = System.Drawing.Color.Black;
            this.lbName.Location = new System.Drawing.Point(472, 14);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(53, 12);
            this.lbName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbName.TabIndex = 26;
            this.lbName.Text = "姓   名:";
            // 
            // lbSex
            // 
            this.lbSex.AutoSize = true;
            this.lbSex.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbSex.ForeColor = System.Drawing.Color.Black;
            this.lbSex.Location = new System.Drawing.Point(734, 12);
            this.lbSex.Name = "lbSex";
            this.lbSex.Size = new System.Drawing.Size(35, 12);
            this.lbSex.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbSex.TabIndex = 32;
            this.lbSex.Text = "性别:";
            // 
            // lbAge
            // 
            this.lbAge.AutoSize = true;
            this.lbAge.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbAge.ForeColor = System.Drawing.Color.Black;
            this.lbAge.Location = new System.Drawing.Point(734, 45);
            this.lbAge.Name = "lbAge";
            this.lbAge.Size = new System.Drawing.Size(35, 12);
            this.lbAge.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbAge.TabIndex = 34;
            this.lbAge.Text = "年龄:";
            // 
            // lbRegDept
            // 
            this.lbRegDept.AutoSize = true;
            this.lbRegDept.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbRegDept.ForeColor = System.Drawing.Color.Black;
            this.lbRegDept.Location = new System.Drawing.Point(219, 47);
            this.lbRegDept.Name = "lbRegDept";
            this.lbRegDept.Size = new System.Drawing.Size(59, 12);
            this.lbRegDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbRegDept.TabIndex = 30;
            this.lbRegDept.Text = "证件号码:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 90);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(869, 392);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "购物卡发放";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.fpSpread2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 88);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(863, 301);
            this.panel2.TabIndex = 53;
            // 
            // fpSpread2
            // 
            this.fpSpread2.About = "3.0.2004.2005";
            this.fpSpread2.AccessibleDescription = "fpSpread2, Sheet1, Row 0, Column 0, ";
            this.fpSpread2.BackColor = System.Drawing.Color.Transparent;
            this.fpSpread2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread2.Location = new System.Drawing.Point(0, 0);
            this.fpSpread2.Name = "fpSpread2";
            this.fpSpread2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread2.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread2_Sheet1});
            this.fpSpread2.Size = new System.Drawing.Size(863, 301);
            this.fpSpread2.TabIndex = 1;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread2.TextTipAppearance = tipAppearance2;
            // 
            // fpSpread2_Sheet1
            // 
            this.fpSpread2_Sheet1.Reset();
            this.fpSpread2_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread2_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread2_Sheet1.ColumnCount = 14;
            this.fpSpread2_Sheet1.RowCount = 1;
            this.fpSpread2_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "卡号";
            this.fpSpread2_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "卡类型";
            this.fpSpread2_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "卡类名称";
            this.fpSpread2_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "领卡客户";
            this.fpSpread2_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "领卡客户门诊卡号";
            this.fpSpread2_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "领卡时间";
            this.fpSpread2_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "领卡客户联系号码";
            this.fpSpread2_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "发卡人员";
            this.fpSpread2_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "使用状态";
            this.fpSpread2_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "用卡客户";
            this.fpSpread2_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "用卡客户门诊卡号";
            this.fpSpread2_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "用卡客户联系号码";
            this.fpSpread2_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "用卡时间";
            this.fpSpread2_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "收卡人员";
            this.fpSpread2_Sheet1.Columns.Get(0).Label = "卡号";
            this.fpSpread2_Sheet1.Columns.Get(0).Width = 80F;
            this.fpSpread2_Sheet1.Columns.Get(1).Label = "卡类型";
            this.fpSpread2_Sheet1.Columns.Get(1).Width = 80F;
            this.fpSpread2_Sheet1.Columns.Get(2).Label = "卡类名称";
            this.fpSpread2_Sheet1.Columns.Get(2).Width = 100F;
            this.fpSpread2_Sheet1.Columns.Get(3).Label = "领卡客户";
            this.fpSpread2_Sheet1.Columns.Get(3).Width = 100F;
            this.fpSpread2_Sheet1.Columns.Get(4).Label = "领卡客户门诊卡号";
            this.fpSpread2_Sheet1.Columns.Get(4).Width = 160F;
            this.fpSpread2_Sheet1.Columns.Get(5).Label = "领卡时间";
            this.fpSpread2_Sheet1.Columns.Get(5).Width = 180F;
            this.fpSpread2_Sheet1.Columns.Get(6).Label = "领卡客户联系号码";
            this.fpSpread2_Sheet1.Columns.Get(6).Width = 160F;
            this.fpSpread2_Sheet1.Columns.Get(7).Label = "发卡人员";
            this.fpSpread2_Sheet1.Columns.Get(7).Width = 100F;
            this.fpSpread2_Sheet1.Columns.Get(8).Label = "使用状态";
            this.fpSpread2_Sheet1.Columns.Get(8).Width = 100F;
            this.fpSpread2_Sheet1.Columns.Get(9).Label = "用卡客户";
            this.fpSpread2_Sheet1.Columns.Get(9).Width = 100F;
            this.fpSpread2_Sheet1.Columns.Get(10).Label = "用卡客户门诊卡号";
            this.fpSpread2_Sheet1.Columns.Get(10).Width = 160F;
            this.fpSpread2_Sheet1.Columns.Get(11).Label = "用卡客户联系号码";
            this.fpSpread2_Sheet1.Columns.Get(11).Width = 160F;
            this.fpSpread2_Sheet1.Columns.Get(12).Label = "用卡时间";
            this.fpSpread2_Sheet1.Columns.Get(12).Width = 180F;
            this.fpSpread2_Sheet1.Columns.Get(13).Label = "收卡人员";
            this.fpSpread2_Sheet1.Columns.Get(13).Width = 100F;
            this.fpSpread2_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
            this.fpSpread2_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpSpread2_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnGetCardNo);
            this.panel1.Controls.Add(this.lblTip);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtNewCardNo);
            this.panel1.Controls.Add(this.cmbOper);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cmbCardKind);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(863, 71);
            this.panel1.TabIndex = 52;
            // 
            // btnGetCardNo
            // 
            this.btnGetCardNo.Location = new System.Drawing.Point(636, 7);
            this.btnGetCardNo.Name = "btnGetCardNo";
            this.btnGetCardNo.Size = new System.Drawing.Size(75, 23);
            this.btnGetCardNo.TabIndex = 53;
            this.btnGetCardNo.Text = "获取卡号";
            this.btnGetCardNo.UseVisualStyleBackColor = true;
            this.btnGetCardNo.Click += new System.EventHandler(this.btnGetCardNo_Click);
            // 
            // lblTip
            // 
            this.lblTip.AutoSize = true;
            this.lblTip.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTip.ForeColor = System.Drawing.Color.Red;
            this.lblTip.Location = new System.Drawing.Point(738, 10);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(70, 12);
            this.lblTip.TabIndex = 52;
            this.lblTip.Text = "无可用卡号";
            this.lblTip.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "收费员：";
            // 
            // txtNewCardNo
            // 
            this.txtNewCardNo.Location = new System.Drawing.Point(490, 7);
            this.txtNewCardNo.Name = "txtNewCardNo";
            this.txtNewCardNo.Size = new System.Drawing.Size(127, 21);
            this.txtNewCardNo.TabIndex = 51;
            // 
            // cmbOper
            // 
            this.cmbOper.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbOper.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbOper.FormattingEnabled = true;
            this.cmbOper.IsEnter2Tab = false;
            this.cmbOper.IsFlat = false;
            this.cmbOper.IsLike = true;
            this.cmbOper.IsListOnly = false;
            this.cmbOper.IsPopForm = true;
            this.cmbOper.IsShowCustomerList = false;
            this.cmbOper.IsShowID = false;
            this.cmbOper.IsShowIDAndName = false;
            this.cmbOper.Location = new System.Drawing.Point(85, 7);
            this.cmbOper.Name = "cmbOper";
            this.cmbOper.ShowCustomerList = false;
            this.cmbOper.ShowID = false;
            this.cmbOper.Size = new System.Drawing.Size(117, 20);
            this.cmbOper.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbOper.TabIndex = 47;
            this.cmbOper.Tag = "";
            this.cmbOper.ToolBarUse = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(448, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 50;
            this.label3.Text = "卡号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(242, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 48;
            this.label2.Text = "卡类型：";
            // 
            // cmbCardKind
            // 
            this.cmbCardKind.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbCardKind.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbCardKind.FormattingEnabled = true;
            this.cmbCardKind.IsEnter2Tab = false;
            this.cmbCardKind.IsFlat = false;
            this.cmbCardKind.IsLike = true;
            this.cmbCardKind.IsListOnly = false;
            this.cmbCardKind.IsPopForm = true;
            this.cmbCardKind.IsShowCustomerList = false;
            this.cmbCardKind.IsShowID = false;
            this.cmbCardKind.IsShowIDAndName = false;
            this.cmbCardKind.Location = new System.Drawing.Point(299, 7);
            this.cmbCardKind.Name = "cmbCardKind";
            this.cmbCardKind.ShowCustomerList = false;
            this.cmbCardKind.ShowID = false;
            this.cmbCardKind.Size = new System.Drawing.Size(117, 20);
            this.cmbCardKind.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbCardKind.TabIndex = 49;
            this.cmbCardKind.Tag = "";
            this.cmbCardKind.ToolBarUse = false;
            // 
            // ucPatientCardTicket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ucPatientCardTicket";
            this.Size = new System.Drawing.Size(869, 482);
            this.Load += new System.EventHandler(this.ucPatientCardTicket_Load);
            this.groupBox1.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread2_Sheet1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel pnlTop;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox txtCardNO;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbCardNO;
        private System.Windows.Forms.TextBox tbSex;
        private System.Windows.Forms.TextBox tbCardType;
        protected System.Windows.Forms.TextBox tbMedicalNO;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblMedicalNO;
        protected System.Windows.Forms.TextBox tbPhone;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblPhone;
        protected System.Windows.Forms.TextBox tbIDNO;
        protected System.Windows.Forms.TextBox tbAge;
        protected System.Windows.Forms.TextBox tbName;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbName;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbSex;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbAge;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbRegDept;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbCardKind;
        private System.Windows.Forms.Label label2;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbOper;
        private System.Windows.Forms.TextBox txtNewCardNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblTip;
        private System.Windows.Forms.Button btnGetCardNo;
        private FarPoint.Win.Spread.FpSpread fpSpread2;
        private FarPoint.Win.Spread.SheetView fpSpread2_Sheet1;
    }
}
