namespace FS.HISFC.Components.Account.Controls.IBorn
{
    partial class ucCardVolume
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
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.lblCardVolumeState = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.ckSelect = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.txtMoney = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtMark = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel14 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dtEndTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.dtBegTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel13 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtBegVoluneNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtEndVolumeNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel16 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel15 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblOper = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbOper = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.txtInvoiceNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblInvoiceNo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.gbQueryCon = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.cmbCardVolumeState = new FS.HISFC.Components.Common.Controls.cmbPayType(this.components);
            this.txtCardNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblCardNo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuGroupBox2 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuTabControl1 = new FS.FrameWork.WinForms.Controls.NeuTabControl();
            this.tpBaseInfo = new System.Windows.Forms.TabPage();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.lblCardVolumeState.SuspendLayout();
            this.gbQueryCon.SuspendLayout();
            this.neuGroupBox2.SuspendLayout();
            this.neuTabControl1.SuspendLayout();
            this.tpBaseInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCardVolumeState
            // 
            this.lblCardVolumeState.BackColor = System.Drawing.Color.Transparent;
            this.lblCardVolumeState.Controls.Add(this.ckSelect);
            this.lblCardVolumeState.Controls.Add(this.txtMoney);
            this.lblCardVolumeState.Controls.Add(this.txtMark);
            this.lblCardVolumeState.Controls.Add(this.neuLabel14);
            this.lblCardVolumeState.Controls.Add(this.dtEndTime);
            this.lblCardVolumeState.Controls.Add(this.dtBegTime);
            this.lblCardVolumeState.Controls.Add(this.neuLabel13);
            this.lblCardVolumeState.Controls.Add(this.txtBegVoluneNo);
            this.lblCardVolumeState.Controls.Add(this.txtEndVolumeNo);
            this.lblCardVolumeState.Controls.Add(this.neuLabel16);
            this.lblCardVolumeState.Controls.Add(this.neuLabel15);
            this.lblCardVolumeState.Controls.Add(this.neuLabel6);
            this.lblCardVolumeState.Controls.Add(this.neuLabel1);
            this.lblCardVolumeState.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCardVolumeState.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCardVolumeState.Location = new System.Drawing.Point(0, 0);
            this.lblCardVolumeState.Name = "lblCardVolumeState";
            this.lblCardVolumeState.Size = new System.Drawing.Size(1316, 84);
            this.lblCardVolumeState.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblCardVolumeState.TabIndex = 1;
            this.lblCardVolumeState.TabStop = false;
            // 
            // ckSelect
            // 
            this.ckSelect.AutoSize = true;
            this.ckSelect.Location = new System.Drawing.Point(638, 18);
            this.ckSelect.Name = "ckSelect";
            this.ckSelect.Size = new System.Drawing.Size(120, 16);
            this.ckSelect.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ckSelect.TabIndex = 42;
            this.ckSelect.Text = "显示更多查询条件";
            this.ckSelect.UseVisualStyleBackColor = true;
            // 
            // txtMoney
            // 
            this.txtMoney.IsEnter2Tab = false;
            this.txtMoney.Location = new System.Drawing.Point(460, 18);
            this.txtMoney.Name = "txtMoney";
            this.txtMoney.Size = new System.Drawing.Size(119, 21);
            this.txtMoney.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtMoney.TabIndex = 3;
            this.txtMoney.Text = "0.00";
            // 
            // txtMark
            // 
            this.txtMark.IsEnter2Tab = false;
            this.txtMark.Location = new System.Drawing.Point(460, 53);
            this.txtMark.Name = "txtMark";
            this.txtMark.Size = new System.Drawing.Size(327, 21);
            this.txtMark.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtMark.TabIndex = 7;
            // 
            // neuLabel14
            // 
            this.neuLabel14.AutoSize = true;
            this.neuLabel14.Location = new System.Drawing.Point(230, 57);
            this.neuLabel14.Name = "neuLabel14";
            this.neuLabel14.Size = new System.Drawing.Size(17, 12);
            this.neuLabel14.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel14.TabIndex = 37;
            this.neuLabel14.Text = "—";
            // 
            // dtEndTime
            // 
            this.dtEndTime.IsEnter2Tab = false;
            this.dtEndTime.Location = new System.Drawing.Point(248, 53);
            this.dtEndTime.Name = "dtEndTime";
            this.dtEndTime.Size = new System.Drawing.Size(125, 21);
            this.dtEndTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtEndTime.TabIndex = 6;
            // 
            // dtBegTime
            // 
            this.dtBegTime.IsEnter2Tab = false;
            this.dtBegTime.Location = new System.Drawing.Point(102, 53);
            this.dtBegTime.Name = "dtBegTime";
            this.dtBegTime.Size = new System.Drawing.Size(126, 21);
            this.dtBegTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtBegTime.TabIndex = 5;
            // 
            // neuLabel13
            // 
            this.neuLabel13.AutoSize = true;
            this.neuLabel13.Location = new System.Drawing.Point(230, 22);
            this.neuLabel13.Name = "neuLabel13";
            this.neuLabel13.Size = new System.Drawing.Size(17, 12);
            this.neuLabel13.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel13.TabIndex = 34;
            this.neuLabel13.Text = "—";
            // 
            // txtBegVoluneNo
            // 
            this.txtBegVoluneNo.IsEnter2Tab = false;
            this.txtBegVoluneNo.Location = new System.Drawing.Point(102, 18);
            this.txtBegVoluneNo.Name = "txtBegVoluneNo";
            this.txtBegVoluneNo.Size = new System.Drawing.Size(126, 21);
            this.txtBegVoluneNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtBegVoluneNo.TabIndex = 1;
            // 
            // txtEndVolumeNo
            // 
            this.txtEndVolumeNo.IsEnter2Tab = false;
            this.txtEndVolumeNo.Location = new System.Drawing.Point(247, 18);
            this.txtEndVolumeNo.Name = "txtEndVolumeNo";
            this.txtEndVolumeNo.Size = new System.Drawing.Size(126, 21);
            this.txtEndVolumeNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtEndVolumeNo.TabIndex = 2;
            // 
            // neuLabel16
            // 
            this.neuLabel16.AutoSize = true;
            this.neuLabel16.Location = new System.Drawing.Point(395, 22);
            this.neuLabel16.Name = "neuLabel16";
            this.neuLabel16.Size = new System.Drawing.Size(65, 12);
            this.neuLabel16.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel16.TabIndex = 41;
            this.neuLabel16.Text = "卡券金额：";
            // 
            // neuLabel15
            // 
            this.neuLabel15.AutoSize = true;
            this.neuLabel15.Location = new System.Drawing.Point(395, 57);
            this.neuLabel15.Name = "neuLabel15";
            this.neuLabel15.Size = new System.Drawing.Size(65, 12);
            this.neuLabel15.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel15.TabIndex = 39;
            this.neuLabel15.Text = "备    注：";
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.Location = new System.Drawing.Point(31, 57);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(77, 12);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 32;
            this.neuLabel6.Text = "有  效  期：";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(31, 22);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(77, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 31;
            this.neuLabel1.Text = "卡券起始号：";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(207, 20);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(65, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 52;
            this.neuLabel2.Text = "卡卷状态：";
            // 
            // lblOper
            // 
            this.lblOper.AutoSize = true;
            this.lblOper.Location = new System.Drawing.Point(437, 20);
            this.lblOper.Name = "lblOper";
            this.lblOper.Size = new System.Drawing.Size(65, 12);
            this.lblOper.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblOper.TabIndex = 50;
            this.lblOper.Text = "操 作 员：";
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
            this.cmbOper.Location = new System.Drawing.Point(509, 17);
            this.cmbOper.Name = "cmbOper";
            this.cmbOper.ShowCustomerList = false;
            this.cmbOper.ShowID = false;
            this.cmbOper.Size = new System.Drawing.Size(117, 20);
            this.cmbOper.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbOper.TabIndex = 46;
            this.cmbOper.Tag = "";
            this.cmbOper.ToolBarUse = false;
            // 
            // txtInvoiceNo
            // 
            this.txtInvoiceNo.IsEnter2Tab = false;
            this.txtInvoiceNo.Location = new System.Drawing.Point(721, 16);
            this.txtInvoiceNo.Name = "txtInvoiceNo";
            this.txtInvoiceNo.Size = new System.Drawing.Size(119, 21);
            this.txtInvoiceNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtInvoiceNo.TabIndex = 45;
            // 
            // lblInvoiceNo
            // 
            this.lblInvoiceNo.AutoSize = true;
            this.lblInvoiceNo.Location = new System.Drawing.Point(650, 20);
            this.lblInvoiceNo.Name = "lblInvoiceNo";
            this.lblInvoiceNo.Size = new System.Drawing.Size(65, 12);
            this.lblInvoiceNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblInvoiceNo.TabIndex = 44;
            this.lblInvoiceNo.Text = "发 票 号：";
            // 
            // gbQueryCon
            // 
            this.gbQueryCon.Controls.Add(this.cmbCardVolumeState);
            this.gbQueryCon.Controls.Add(this.txtCardNo);
            this.gbQueryCon.Controls.Add(this.lblCardNo);
            this.gbQueryCon.Controls.Add(this.neuLabel2);
            this.gbQueryCon.Controls.Add(this.lblInvoiceNo);
            this.gbQueryCon.Controls.Add(this.lblOper);
            this.gbQueryCon.Controls.Add(this.txtInvoiceNo);
            this.gbQueryCon.Controls.Add(this.cmbOper);
            this.gbQueryCon.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbQueryCon.Location = new System.Drawing.Point(0, 84);
            this.gbQueryCon.Name = "gbQueryCon";
            this.gbQueryCon.Size = new System.Drawing.Size(1316, 48);
            this.gbQueryCon.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbQueryCon.TabIndex = 4;
            this.gbQueryCon.TabStop = false;
            this.gbQueryCon.Visible = false;
            // 
            // cmbCardVolumeState
            // 
            this.cmbCardVolumeState.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbCardVolumeState.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbCardVolumeState.FormattingEnabled = true;
            this.cmbCardVolumeState.IsEnter2Tab = false;
            this.cmbCardVolumeState.IsFlat = false;
            this.cmbCardVolumeState.IsLike = true;
            this.cmbCardVolumeState.IsListOnly = false;
            this.cmbCardVolumeState.IsPopForm = true;
            this.cmbCardVolumeState.IsShowCustomerList = false;
            this.cmbCardVolumeState.IsShowID = false;
            this.cmbCardVolumeState.IsShowIDAndName = false;
            this.cmbCardVolumeState.Location = new System.Drawing.Point(278, 17);
            this.cmbCardVolumeState.Name = "cmbCardVolumeState";
            this.cmbCardVolumeState.Pop = true;
            this.cmbCardVolumeState.ShowCustomerList = false;
            this.cmbCardVolumeState.ShowID = false;
            this.cmbCardVolumeState.Size = new System.Drawing.Size(127, 20);
            this.cmbCardVolumeState.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbCardVolumeState.TabIndex = 55;
            this.cmbCardVolumeState.Tag = "";
            this.cmbCardVolumeState.ToolBarUse = false;
            this.cmbCardVolumeState.WorkUnit = "";
            // 
            // txtCardNo
            // 
            this.txtCardNo.IsEnter2Tab = false;
            this.txtCardNo.Location = new System.Drawing.Point(79, 16);
            this.txtCardNo.Name = "txtCardNo";
            this.txtCardNo.Size = new System.Drawing.Size(119, 21);
            this.txtCardNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCardNo.TabIndex = 54;
            // 
            // lblCardNo
            // 
            this.lblCardNo.AutoSize = true;
            this.lblCardNo.Location = new System.Drawing.Point(8, 20);
            this.lblCardNo.Name = "lblCardNo";
            this.lblCardNo.Size = new System.Drawing.Size(65, 12);
            this.lblCardNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblCardNo.TabIndex = 53;
            this.lblCardNo.Text = "病 历 号：";
            // 
            // neuGroupBox2
            // 
            this.neuGroupBox2.Controls.Add(this.neuTabControl1);
            this.neuGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuGroupBox2.Location = new System.Drawing.Point(0, 132);
            this.neuGroupBox2.Name = "neuGroupBox2";
            this.neuGroupBox2.Size = new System.Drawing.Size(1316, 469);
            this.neuGroupBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox2.TabIndex = 5;
            this.neuGroupBox2.TabStop = false;
            // 
            // neuTabControl1
            // 
            this.neuTabControl1.Controls.Add(this.tpBaseInfo);
            this.neuTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuTabControl1.Location = new System.Drawing.Point(3, 17);
            this.neuTabControl1.Name = "neuTabControl1";
            this.neuTabControl1.SelectedIndex = 0;
            this.neuTabControl1.Size = new System.Drawing.Size(1310, 449);
            this.neuTabControl1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTabControl1.TabIndex = 3;
            this.neuTabControl1.TabStop = false;
            // 
            // tpBaseInfo
            // 
            this.tpBaseInfo.Controls.Add(this.neuSpread1);
            this.tpBaseInfo.Location = new System.Drawing.Point(4, 22);
            this.tpBaseInfo.Name = "tpBaseInfo";
            this.tpBaseInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpBaseInfo.Size = new System.Drawing.Size(1302, 423);
            this.tpBaseInfo.TabIndex = 0;
            this.tpBaseInfo.Text = "卡卷信息（双击设置停用或启用）";
            this.tpBaseInfo.UseVisualStyleBackColor = true;
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, 卡卷信息";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(3, 3);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(1296, 417);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 1;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance2;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.neuSpread1_CellDoubleClick);
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "卡卷信息";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ColumnCount = 11;
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "卡卷号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "开始时间";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "截止时间";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "卡卷金额";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "卡卷状态";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "卡卷用途";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "对应消费发票号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "使用人病历号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "备注";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "操作人";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "操作时间";
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.Columns.Get(0).AllowAutoSort = true;
            textCellType2.ReadOnly = true;
            this.neuSpread1_Sheet1.Columns.Get(0).CellType = textCellType2;
            this.neuSpread1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "卡卷号";
            this.neuSpread1_Sheet1.Columns.Get(0).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 150F;
            this.neuSpread1_Sheet1.Columns.Get(1).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "开始时间";
            this.neuSpread1_Sheet1.Columns.Get(1).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 122F;
            this.neuSpread1_Sheet1.Columns.Get(2).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get(2).CellType = textCellType2;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "截止时间";
            this.neuSpread1_Sheet1.Columns.Get(2).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 117F;
            this.neuSpread1_Sheet1.Columns.Get(3).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get(3).CellType = textCellType2;
            this.neuSpread1_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "卡卷金额";
            this.neuSpread1_Sheet1.Columns.Get(3).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 100F;
            this.neuSpread1_Sheet1.Columns.Get(4).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get(4).CellType = textCellType2;
            this.neuSpread1_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "卡卷状态";
            this.neuSpread1_Sheet1.Columns.Get(4).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 77F;
            this.neuSpread1_Sheet1.Columns.Get(5).CellType = textCellType2;
            this.neuSpread1_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "卡卷用途";
            this.neuSpread1_Sheet1.Columns.Get(5).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 163F;
            this.neuSpread1_Sheet1.Columns.Get(6).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get(6).CellType = textCellType2;
            this.neuSpread1_Sheet1.Columns.Get(6).Label = "对应消费发票号";
            this.neuSpread1_Sheet1.Columns.Get(6).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 110F;
            this.neuSpread1_Sheet1.Columns.Get(7).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get(7).CellType = textCellType2;
            this.neuSpread1_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(7).Label = "使用人病历号";
            this.neuSpread1_Sheet1.Columns.Get(7).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(7).Width = 101F;
            this.neuSpread1_Sheet1.Columns.Get(8).CellType = textCellType2;
            this.neuSpread1_Sheet1.Columns.Get(8).Label = "备注";
            this.neuSpread1_Sheet1.Columns.Get(8).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(8).Width = 173F;
            this.neuSpread1_Sheet1.Columns.Get(9).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get(9).CellType = textCellType2;
            this.neuSpread1_Sheet1.Columns.Get(9).Label = "操作人";
            this.neuSpread1_Sheet1.Columns.Get(9).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(9).Width = 100F;
            this.neuSpread1_Sheet1.Columns.Get(10).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get(10).Label = "操作时间";
            this.neuSpread1_Sheet1.Columns.Get(10).Width = 144F;
            this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.SheetCornerStyle.Locked = false;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuSpread1.SetActiveViewport(0, 1, 0);
            // 
            // ucCardVolume
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.neuGroupBox2);
            this.Controls.Add(this.gbQueryCon);
            this.Controls.Add(this.lblCardVolumeState);
            this.Name = "ucCardVolume";
            this.Size = new System.Drawing.Size(1316, 601);
            this.lblCardVolumeState.ResumeLayout(false);
            this.lblCardVolumeState.PerformLayout();
            this.gbQueryCon.ResumeLayout(false);
            this.gbQueryCon.PerformLayout();
            this.neuGroupBox2.ResumeLayout(false);
            this.neuTabControl1.ResumeLayout(false);
            this.tpBaseInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox lblCardVolumeState;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtEndVolumeNo;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtBegVoluneNo;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel13;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel15;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtMark;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel14;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtEndTime;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtBegTime;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtMoney;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel16;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbOper;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtInvoiceNo;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblInvoiceNo;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblOper;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbQueryCon;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox2;
        private FS.FrameWork.WinForms.Controls.NeuTabControl neuTabControl1;
        protected System.Windows.Forms.TabPage tpBaseInfo;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ckSelect;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtCardNo;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblCardNo;
        private FS.HISFC.Components.Common.Controls.cmbPayType cmbCardVolumeState;

    }
}
