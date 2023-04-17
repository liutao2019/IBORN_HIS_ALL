namespace HISFC.Components.Package.Fee.Controls
{
    /// <summary>
    /// ucPopSelected<br></br>
    /// [功能描述: 门诊患者基本信息UC]<br></br>
    /// [创 建 者: 王宇]<br></br>
    /// [创建时间: 2006-2-28]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    partial class ucPatientInfo
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
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.fpRecipe = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuContexMenu1 = new FS.FrameWork.WinForms.Controls.NeuContexMenu();
            this.addRecipe = new System.Windows.Forms.MenuItem();
            this.delRecipe = new System.Windows.Forms.MenuItem();
            this.fpRecipe_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.lbAge = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbSex = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lbSex = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.tbAge = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lbCardNO = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbCardNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblPhone = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbPhone = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblConsultant = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbConsultant = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lblDept = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbDept = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbMemo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.chkBL = new System.Windows.Forms.CheckBox();
            this.txtVacancy = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.pnlPatientInfo = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.patientMemo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbAccountInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.chkLevelDiscount = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.fpRecipe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpRecipe_Sheet1)).BeginInit();
            this.pnlPatientInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // fpRecipe
            // 
            this.fpRecipe.About = "3.0.2004.2005";
            this.fpRecipe.AccessibleDescription = "fpRecipe, Sheet1";
            this.fpRecipe.BackColor = System.Drawing.Color.White;
            this.fpRecipe.ContextMenu = this.neuContexMenu1;
            this.fpRecipe.Dock = System.Windows.Forms.DockStyle.Left;
            this.fpRecipe.EditModePermanent = true;
            this.fpRecipe.EditModeReplace = true;
            this.fpRecipe.FileName = "";
            this.fpRecipe.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpRecipe.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpRecipe.IsAutoSaveGridStatus = false;
            this.fpRecipe.IsCanCustomConfigColumn = false;
            this.fpRecipe.Location = new System.Drawing.Point(1240, 5);
            this.fpRecipe.Name = "fpRecipe";
            this.fpRecipe.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpRecipe.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpRecipe_Sheet1});
            this.fpRecipe.Size = new System.Drawing.Size(259, 99);
            this.fpRecipe.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpRecipe.TabIndex = 26;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpRecipe.TextTipAppearance = tipAppearance1;
            this.fpRecipe.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            // 
            // neuContexMenu1
            // 
            this.neuContexMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.addRecipe,
            this.delRecipe});
            this.neuContexMenu1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            // 
            // addRecipe
            // 
            this.addRecipe.Index = 0;
            this.addRecipe.Text = "增加划价单";
            // 
            // delRecipe
            // 
            this.delRecipe.Index = 1;
            this.delRecipe.Text = "删除划价单";
            // 
            // fpRecipe_Sheet1
            // 
            this.fpRecipe_Sheet1.Reset();
            this.fpRecipe_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpRecipe_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpRecipe_Sheet1.ColumnCount = 3;
            this.fpRecipe_Sheet1.RowCount = 0;
            this.fpRecipe_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = " ";
            this.fpRecipe_Sheet1.ColumnHeader.Cells.Get(0, 1).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpRecipe_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "单据号";
            this.fpRecipe_Sheet1.ColumnHeader.Cells.Get(0, 2).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpRecipe_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "金额";
            this.fpRecipe_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(217)))), ((int)(((byte)(234)))));
            this.fpRecipe_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpRecipe_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpRecipe_Sheet1.ColumnHeader.Rows.Get(0).Height = 18F;
            this.fpRecipe_Sheet1.Columns.Get(0).CellType = checkBoxCellType1;
            this.fpRecipe_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpRecipe_Sheet1.Columns.Get(0).Label = " ";
            this.fpRecipe_Sheet1.Columns.Get(0).Locked = false;
            this.fpRecipe_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpRecipe_Sheet1.Columns.Get(0).Width = 33F;
            this.fpRecipe_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpRecipe_Sheet1.Columns.Get(1).Label = "单据号";
            this.fpRecipe_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpRecipe_Sheet1.Columns.Get(1).Width = 104F;
            this.fpRecipe_Sheet1.Columns.Get(2).CellType = numberCellType1;
            this.fpRecipe_Sheet1.Columns.Get(2).Label = "金额";
            this.fpRecipe_Sheet1.Columns.Get(2).Width = 108F;
            this.fpRecipe_Sheet1.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpRecipe_Sheet1.DefaultStyle.Locked = true;
            this.fpRecipe_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpRecipe_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.fpRecipe_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpRecipe_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpRecipe_Sheet1.RowHeader.Columns.Get(0).Width = 0F;
            this.fpRecipe_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpRecipe_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpRecipe_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpRecipe.SetActiveViewport(0, 1, 0);
            // 
            // lbAge
            // 
            this.lbAge.AutoSize = true;
            this.lbAge.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbAge.ForeColor = System.Drawing.Color.Blue;
            this.lbAge.Location = new System.Drawing.Point(346, 42);
            this.lbAge.Name = "lbAge";
            this.lbAge.Size = new System.Drawing.Size(41, 12);
            this.lbAge.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbAge.TabIndex = 14;
            this.lbAge.Text = "年龄：";
            // 
            // cmbSex
            // 
            this.cmbSex.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbSex.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbSex.Enabled = false;
            this.cmbSex.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbSex.FormattingEnabled = true;
            this.cmbSex.IsEnter2Tab = false;
            this.cmbSex.IsFlat = false;
            this.cmbSex.IsLike = true;
            this.cmbSex.IsListOnly = false;
            this.cmbSex.IsPopForm = true;
            this.cmbSex.IsShowCustomerList = false;
            this.cmbSex.IsShowID = false;
            this.cmbSex.IsShowIDAndName = false;
            this.cmbSex.Location = new System.Drawing.Point(291, 38);
            this.cmbSex.Name = "cmbSex";
            this.cmbSex.ShowCustomerList = false;
            this.cmbSex.ShowID = false;
            this.cmbSex.Size = new System.Drawing.Size(52, 20);
            this.cmbSex.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbSex.TabIndex = 13;
            this.cmbSex.Tag = "";
            this.cmbSex.ToolBarUse = false;
            // 
            // lbSex
            // 
            this.lbSex.AutoSize = true;
            this.lbSex.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbSex.ForeColor = System.Drawing.Color.Blue;
            this.lbSex.Location = new System.Drawing.Point(220, 43);
            this.lbSex.Name = "lbSex";
            this.lbSex.Size = new System.Drawing.Size(65, 12);
            this.lbSex.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbSex.TabIndex = 12;
            this.lbSex.Text = "性    别：";
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbName.ForeColor = System.Drawing.Color.Blue;
            this.lbName.Location = new System.Drawing.Point(220, 12);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(65, 12);
            this.lbName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbName.TabIndex = 6;
            this.lbName.Text = "患者姓名：";
            // 
            // tbName
            // 
            this.tbName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbName.IsEnter2Tab = false;
            this.tbName.Location = new System.Drawing.Point(291, 7);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(171, 21);
            this.tbName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbName.TabIndex = 7;
            // 
            // tbAge
            // 
            this.tbAge.BackColor = System.Drawing.Color.White;
            this.tbAge.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbAge.IsEnter2Tab = false;
            this.tbAge.Location = new System.Drawing.Point(387, 37);
            this.tbAge.Name = "tbAge";
            this.tbAge.ReadOnly = true;
            this.tbAge.Size = new System.Drawing.Size(75, 21);
            this.tbAge.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbAge.TabIndex = 15;
            // 
            // lbCardNO
            // 
            this.lbCardNO.AutoSize = true;
            this.lbCardNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbCardNO.ForeColor = System.Drawing.Color.Red;
            this.lbCardNO.Location = new System.Drawing.Point(17, 12);
            this.lbCardNO.Name = "lbCardNO";
            this.lbCardNO.Size = new System.Drawing.Size(65, 12);
            this.lbCardNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbCardNO.TabIndex = 2;
            this.lbCardNO.Text = "就诊卡号：";
            // 
            // tbCardNO
            // 
            this.tbCardNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbCardNO.IsEnter2Tab = false;
            this.tbCardNO.Location = new System.Drawing.Point(88, 7);
            this.tbCardNO.Name = "tbCardNO";
            this.tbCardNO.Size = new System.Drawing.Size(125, 21);
            this.tbCardNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbCardNO.TabIndex = 3;
            this.tbCardNO.Tag = "CARDNO";
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPhone.ForeColor = System.Drawing.Color.Blue;
            this.lblPhone.Location = new System.Drawing.Point(17, 41);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(65, 12);
            this.lblPhone.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblPhone.TabIndex = 18;
            this.lblPhone.Text = "联系电话：";
            // 
            // tbPhone
            // 
            this.tbPhone.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbPhone.IsEnter2Tab = false;
            this.tbPhone.Location = new System.Drawing.Point(88, 37);
            this.tbPhone.Name = "tbPhone";
            this.tbPhone.Size = new System.Drawing.Size(125, 21);
            this.tbPhone.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbPhone.TabIndex = 19;
            // 
            // lblConsultant
            // 
            this.lblConsultant.AutoSize = true;
            this.lblConsultant.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblConsultant.ForeColor = System.Drawing.Color.Blue;
            this.lblConsultant.Location = new System.Drawing.Point(17, 72);
            this.lblConsultant.Name = "lblConsultant";
            this.lblConsultant.Size = new System.Drawing.Size(65, 12);
            this.lblConsultant.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblConsultant.TabIndex = 22;
            this.lblConsultant.Text = "签单客服：";
            // 
            // cmbConsultant
            // 
            this.cmbConsultant.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbConsultant.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbConsultant.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbConsultant.FormattingEnabled = true;
            this.cmbConsultant.IsEnter2Tab = false;
            this.cmbConsultant.IsFlat = false;
            this.cmbConsultant.IsLike = true;
            this.cmbConsultant.IsListOnly = false;
            this.cmbConsultant.IsPopForm = true;
            this.cmbConsultant.IsShowCustomerList = false;
            this.cmbConsultant.IsShowID = false;
            this.cmbConsultant.IsShowIDAndName = false;
            this.cmbConsultant.Location = new System.Drawing.Point(88, 68);
            this.cmbConsultant.Name = "cmbConsultant";
            this.cmbConsultant.ShowCustomerList = false;
            this.cmbConsultant.ShowID = false;
            this.cmbConsultant.Size = new System.Drawing.Size(125, 20);
            this.cmbConsultant.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbConsultant.TabIndex = 23;
            this.cmbConsultant.Tag = "";
            this.cmbConsultant.ToolBarUse = false;
            // 
            // lblDept
            // 
            this.lblDept.AutoSize = true;
            this.lblDept.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDept.ForeColor = System.Drawing.Color.Blue;
            this.lblDept.Location = new System.Drawing.Point(220, 72);
            this.lblDept.Name = "lblDept";
            this.lblDept.Size = new System.Drawing.Size(65, 12);
            this.lblDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblDept.TabIndex = 24;
            this.lblDept.Text = "套餐科室：";
            // 
            // cmbDept
            // 
            this.cmbDept.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbDept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbDept.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbDept.FormattingEnabled = true;
            this.cmbDept.IsEnter2Tab = false;
            this.cmbDept.IsFlat = false;
            this.cmbDept.IsLike = true;
            this.cmbDept.IsListOnly = false;
            this.cmbDept.IsPopForm = true;
            this.cmbDept.IsShowCustomerList = false;
            this.cmbDept.IsShowID = false;
            this.cmbDept.IsShowIDAndName = false;
            this.cmbDept.Location = new System.Drawing.Point(291, 68);
            this.cmbDept.Name = "cmbDept";
            this.cmbDept.ShowCustomerList = false;
            this.cmbDept.ShowID = false;
            this.cmbDept.Size = new System.Drawing.Size(171, 20);
            this.cmbDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbDept.TabIndex = 25;
            this.cmbDept.Tag = "";
            this.cmbDept.ToolBarUse = false;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel1.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel1.Location = new System.Drawing.Point(484, 72);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(65, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 26;
            this.neuLabel1.Text = "发票备注：";
            // 
            // tbMemo
            // 
            this.tbMemo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbMemo.IsEnter2Tab = false;
            this.tbMemo.Location = new System.Drawing.Point(555, 67);
            this.tbMemo.Name = "tbMemo";
            this.tbMemo.Size = new System.Drawing.Size(268, 21);
            this.tbMemo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbMemo.TabIndex = 27;
            // 
            // chkBL
            // 
            this.chkBL.AutoSize = true;
            this.chkBL.BackColor = System.Drawing.Color.Transparent;
            this.chkBL.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkBL.ForeColor = System.Drawing.Color.Red;
            this.chkBL.Location = new System.Drawing.Point(782, 2);
            this.chkBL.Name = "chkBL";
            this.chkBL.Size = new System.Drawing.Size(76, 16);
            this.chkBL.TabIndex = 28;
            this.chkBL.Text = "补录套餐";
            this.chkBL.UseVisualStyleBackColor = false;
            // 
            // txtVacancy
            // 
            this.txtVacancy.AutoSize = true;
            this.txtVacancy.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtVacancy.Location = new System.Drawing.Point(483, 9);
            this.txtVacancy.Name = "txtVacancy";
            this.txtVacancy.Size = new System.Drawing.Size(179, 17);
            this.txtVacancy.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtVacancy.TabIndex = 109;
            this.txtVacancy.Text = "账户余额：0.00,赠送余额：0.00";
            // 
            // pnlPatientInfo
            // 
            this.pnlPatientInfo.Controls.Add(this.patientMemo);
            this.pnlPatientInfo.Controls.Add(this.neuLabel2);
            this.pnlPatientInfo.Controls.Add(this.lbAccountInfo);
            this.pnlPatientInfo.Controls.Add(this.chkLevelDiscount);
            this.pnlPatientInfo.Controls.Add(this.txtVacancy);
            this.pnlPatientInfo.Controls.Add(this.chkBL);
            this.pnlPatientInfo.Controls.Add(this.tbMemo);
            this.pnlPatientInfo.Controls.Add(this.neuLabel1);
            this.pnlPatientInfo.Controls.Add(this.cmbDept);
            this.pnlPatientInfo.Controls.Add(this.lblDept);
            this.pnlPatientInfo.Controls.Add(this.cmbConsultant);
            this.pnlPatientInfo.Controls.Add(this.lblConsultant);
            this.pnlPatientInfo.Controls.Add(this.tbPhone);
            this.pnlPatientInfo.Controls.Add(this.lblPhone);
            this.pnlPatientInfo.Controls.Add(this.tbCardNO);
            this.pnlPatientInfo.Controls.Add(this.lbCardNO);
            this.pnlPatientInfo.Controls.Add(this.tbAge);
            this.pnlPatientInfo.Controls.Add(this.tbName);
            this.pnlPatientInfo.Controls.Add(this.lbName);
            this.pnlPatientInfo.Controls.Add(this.lbSex);
            this.pnlPatientInfo.Controls.Add(this.cmbSex);
            this.pnlPatientInfo.Controls.Add(this.lbAge);
            this.pnlPatientInfo.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlPatientInfo.Font = new System.Drawing.Font("仿宋", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pnlPatientInfo.Location = new System.Drawing.Point(5, 5);
            this.pnlPatientInfo.Name = "pnlPatientInfo";
            this.pnlPatientInfo.Size = new System.Drawing.Size(1235, 99);
            this.pnlPatientInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlPatientInfo.TabIndex = 1;
            // 
            // patientMemo
            // 
            this.patientMemo.Enabled = false;
            this.patientMemo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.patientMemo.IsEnter2Tab = false;
            this.patientMemo.Location = new System.Drawing.Point(555, 37);
            this.patientMemo.Name = "patientMemo";
            this.patientMemo.Size = new System.Drawing.Size(219, 21);
            this.patientMemo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.patientMemo.TabIndex = 113;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel2.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel2.Location = new System.Drawing.Point(484, 42);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(65, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 112;
            this.neuLabel2.Text = "患者备注：";
            // 
            // lbAccountInfo
            // 
            this.lbAccountInfo.AutoSize = true;
            this.lbAccountInfo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbAccountInfo.ForeColor = System.Drawing.Color.Red;
            this.lbAccountInfo.Location = new System.Drawing.Point(780, 42);
            this.lbAccountInfo.Name = "lbAccountInfo";
            this.lbAccountInfo.Size = new System.Drawing.Size(83, 12);
            this.lbAccountInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbAccountInfo.TabIndex = 111;
            this.lbAccountInfo.Text = "会员折扣信息";
            // 
            // chkLevelDiscount
            // 
            this.chkLevelDiscount.AutoSize = true;
            this.chkLevelDiscount.BackColor = System.Drawing.Color.Transparent;
            this.chkLevelDiscount.Checked = true;
            this.chkLevelDiscount.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLevelDiscount.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkLevelDiscount.ForeColor = System.Drawing.Color.Red;
            this.chkLevelDiscount.Location = new System.Drawing.Point(782, 22);
            this.chkLevelDiscount.Name = "chkLevelDiscount";
            this.chkLevelDiscount.Size = new System.Drawing.Size(110, 17);
            this.chkLevelDiscount.TabIndex = 110;
            this.chkLevelDiscount.Text = "使用会员折扣";
            this.chkLevelDiscount.UseVisualStyleBackColor = false;
            // 
            // ucPatientInfo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.fpRecipe);
            this.Controls.Add(this.pnlPatientInfo);
            this.Font = new System.Drawing.Font("宋体", 10F);
            this.Name = "ucPatientInfo";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(1518, 109);
            ((System.ComponentModel.ISupportInitialize)(this.fpRecipe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpRecipe_Sheet1)).EndInit();
            this.pnlPatientInfo.ResumeLayout(false);
            this.pnlPatientInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuSpread fpRecipe;
        private FarPoint.Win.Spread.SheetView fpRecipe_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuContexMenu neuContexMenu1;
        private System.Windows.Forms.MenuItem addRecipe;
        private System.Windows.Forms.MenuItem delRecipe;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbAge;
        protected FS.FrameWork.WinForms.Controls.NeuComboBox cmbSex;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbSex;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbName;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox tbName;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox tbAge;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbCardNO;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox tbCardNO;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblPhone;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox tbPhone;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblConsultant;
        protected FS.FrameWork.WinForms.Controls.NeuComboBox cmbConsultant;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblDept;
        protected FS.FrameWork.WinForms.Controls.NeuComboBox cmbDept;
        protected FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox tbMemo;
        private System.Windows.Forms.CheckBox chkBL;
        private FS.FrameWork.WinForms.Controls.NeuLabel txtVacancy;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlPatientInfo;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbAccountInfo;
        private System.Windows.Forms.CheckBox chkLevelDiscount;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox patientMemo;
        protected FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;


    }
}
