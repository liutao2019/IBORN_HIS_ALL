namespace API.GZSI.UI
{
    partial class ucMatnRegister
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.btnCancelRegister = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnRegister = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.tbPregNumber = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblPatientInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbWeek = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.dtpEnd = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.lblCardType = new System.Windows.Forms.Label();
            this.dtpBegin = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.tbCardNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.cmbMatnType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.fpPrint = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpPrint_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel1.SuspendLayout();
            this.neuPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPrint_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.neuPanel1.Controls.Add(this.neuLabel6);
            this.neuPanel1.Controls.Add(this.btnCancelRegister);
            this.neuPanel1.Controls.Add(this.btnRegister);
            this.neuPanel1.Controls.Add(this.tbPregNumber);
            this.neuPanel1.Controls.Add(this.lblPatientInfo);
            this.neuPanel1.Controls.Add(this.tbWeek);
            this.neuPanel1.Controls.Add(this.dtpEnd);
            this.neuPanel1.Controls.Add(this.lblCardType);
            this.neuPanel1.Controls.Add(this.dtpBegin);
            this.neuPanel1.Controls.Add(this.tbCardNO);
            this.neuPanel1.Controls.Add(this.cmbMatnType);
            this.neuPanel1.Controls.Add(this.neuLabel1);
            this.neuPanel1.Controls.Add(this.neuLabel5);
            this.neuPanel1.Controls.Add(this.neuLabel2);
            this.neuPanel1.Controls.Add(this.neuLabel4);
            this.neuPanel1.Controls.Add(this.neuLabel3);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(1095, 93);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.ForeColor = System.Drawing.Color.Red;
            this.neuLabel6.Location = new System.Drawing.Point(181, 21);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(125, 12);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 18;
            this.neuLabel6.Text = "输入卡号检索患者信息";
            // 
            // btnCancelRegister
            // 
            this.btnCancelRegister.Location = new System.Drawing.Point(993, 48);
            this.btnCancelRegister.Name = "btnCancelRegister";
            this.btnCancelRegister.Size = new System.Drawing.Size(85, 26);
            this.btnCancelRegister.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnCancelRegister.TabIndex = 17;
            this.btnCancelRegister.Text = "取消登记";
            this.btnCancelRegister.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnCancelRegister.UseVisualStyleBackColor = true;
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(898, 48);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(89, 26);
            this.btnRegister.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnRegister.TabIndex = 16;
            this.btnRegister.Text = "生育登记";
            this.btnRegister.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnRegister.UseVisualStyleBackColor = true;
            // 
            // tbPregNumber
            // 
            this.tbPregNumber.IsEnter2Tab = false;
            this.tbPregNumber.Location = new System.Drawing.Point(256, 52);
            this.tbPregNumber.Name = "tbPregNumber";
            this.tbPregNumber.Size = new System.Drawing.Size(76, 21);
            this.tbPregNumber.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbPregNumber.TabIndex = 15;
            // 
            // lblPatientInfo
            // 
            this.lblPatientInfo.AutoSize = true;
            this.lblPatientInfo.Location = new System.Drawing.Point(312, 21);
            this.lblPatientInfo.Name = "lblPatientInfo";
            this.lblPatientInfo.Size = new System.Drawing.Size(65, 12);
            this.lblPatientInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblPatientInfo.TabIndex = 5;
            this.lblPatientInfo.Text = "患者信息：";
            // 
            // tbWeek
            // 
            this.tbWeek.IsEnter2Tab = false;
            this.tbWeek.Location = new System.Drawing.Point(89, 52);
            this.tbWeek.Name = "tbWeek";
            this.tbWeek.Size = new System.Drawing.Size(81, 21);
            this.tbWeek.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbWeek.TabIndex = 14;
            // 
            // dtpEnd
            // 
            this.dtpEnd.IsEnter2Tab = false;
            this.dtpEnd.Location = new System.Drawing.Point(793, 53);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(99, 21);
            this.dtpEnd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpEnd.TabIndex = 13;
            // 
            // lblCardType
            // 
            this.lblCardType.AutoSize = true;
            this.lblCardType.Location = new System.Drawing.Point(18, 21);
            this.lblCardType.Name = "lblCardType";
            this.lblCardType.Size = new System.Drawing.Size(65, 12);
            this.lblCardType.TabIndex = 4;
            this.lblCardType.Text = "门 诊 号：";
            // 
            // dtpBegin
            // 
            this.dtpBegin.IsEnter2Tab = false;
            this.dtpBegin.Location = new System.Drawing.Point(606, 53);
            this.dtpBegin.Name = "dtpBegin";
            this.dtpBegin.Size = new System.Drawing.Size(102, 21);
            this.dtpBegin.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpBegin.TabIndex = 12;
            // 
            // tbCardNO
            // 
            this.tbCardNO.IsEnter2Tab = false;
            this.tbCardNO.Location = new System.Drawing.Point(89, 18);
            this.tbCardNO.Name = "tbCardNO";
            this.tbCardNO.Size = new System.Drawing.Size(81, 21);
            this.tbCardNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbCardNO.TabIndex = 1;
            // 
            // cmbMatnType
            // 
            this.cmbMatnType.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbMatnType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbMatnType.FormattingEnabled = true;
            this.cmbMatnType.IsEnter2Tab = false;
            this.cmbMatnType.IsFlat = false;
            this.cmbMatnType.IsLike = true;
            this.cmbMatnType.IsListOnly = false;
            this.cmbMatnType.IsPopForm = true;
            this.cmbMatnType.IsShowCustomerList = false;
            this.cmbMatnType.IsShowID = false;
            this.cmbMatnType.IsShowIDAndName = false;
            this.cmbMatnType.Location = new System.Drawing.Point(418, 52);
            this.cmbMatnType.Name = "cmbMatnType";
            this.cmbMatnType.ShowCustomerList = false;
            this.cmbMatnType.ShowID = false;
            this.cmbMatnType.Size = new System.Drawing.Size(103, 20);
            this.cmbMatnType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbMatnType.TabIndex = 11;
            this.cmbMatnType.Tag = "";
            this.cmbMatnType.ToolBarUse = false;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(18, 58);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(65, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 6;
            this.neuLabel1.Text = "孕 周 数：";
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Location = new System.Drawing.Point(725, 58);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(65, 12);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 10;
            this.neuLabel5.Text = "结束时间：";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(181, 58);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(65, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 7;
            this.neuLabel2.Text = "患者胎次：";
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point(535, 58);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(65, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 9;
            this.neuLabel4.Text = "开始时间：";
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(347, 58);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(65, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 8;
            this.neuLabel3.Text = "生育类别：";
            // 
            // neuPanel2
            // 
            this.neuPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuPanel2.Location = new System.Drawing.Point(0, 917);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(1095, 1);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 1;
            // 
            // neuPanel3
            // 
            this.neuPanel3.Controls.Add(this.fpPrint);
            this.neuPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel3.Location = new System.Drawing.Point(0, 93);
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size(1095, 824);
            this.neuPanel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 2;
            // 
            // fpPrint
            // 
            this.fpPrint.About = "3.0.2004.2005";
            this.fpPrint.AccessibleDescription = "fpPrint, Sheet1, Row 0, Column 0, 佛山市职工生育保险就医确认单";
            this.fpPrint.BackColor = System.Drawing.Color.White;
            this.fpPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpPrint.FileName = "";
            this.fpPrint.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpPrint.IsAutoSaveGridStatus = false;
            this.fpPrint.IsCanCustomConfigColumn = false;
            this.fpPrint.Location = new System.Drawing.Point(0, 0);
            this.fpPrint.Name = "fpPrint";
            this.fpPrint.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpPrint.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpPrint_Sheet1});
            this.fpPrint.Size = new System.Drawing.Size(1095, 824);
            this.fpPrint.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpPrint.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpPrint.TextTipAppearance = tipAppearance1;
            this.fpPrint.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpPrint_Sheet1
            // 
            this.fpPrint_Sheet1.Reset();
            this.fpPrint_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpPrint_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpPrint_Sheet1.ColumnCount = 6;
            this.fpPrint_Sheet1.ColumnHeader.RowCount = 0;
            this.fpPrint_Sheet1.RowCount = 34;
            this.fpPrint_Sheet1.RowHeader.ColumnCount = 0;
            this.fpPrint_Sheet1.Cells.Get(0, 0).ColumnSpan = 6;
            this.fpPrint_Sheet1.Cells.Get(0, 0).Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.fpPrint_Sheet1.Cells.Get(0, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(0, 0).Value = "佛山市职工生育保险就医确认单";
            this.fpPrint_Sheet1.Cells.Get(0, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(0, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(0, 2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(0, 3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(0, 4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(0, 5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(1, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(1, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(1, 2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(1, 3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(1, 4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.fpPrint_Sheet1.Cells.Get(1, 4).Value = "就医确认号：";
            this.fpPrint_Sheet1.Cells.Get(1, 4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(1, 5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(2, 0).Value = "  参保人：";
            this.fpPrint_Sheet1.Cells.Get(2, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(2, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(2, 2).Value = "个人编号：";
            this.fpPrint_Sheet1.Cells.Get(2, 2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(2, 3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(2, 4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.fpPrint_Sheet1.Cells.Get(2, 4).Value = "证件号码：";
            this.fpPrint_Sheet1.Cells.Get(2, 4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(2, 5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(3, 0).ColumnSpan = 6;
            this.fpPrint_Sheet1.Cells.Get(3, 0).Value = "  预产期                  自愿选定                  顺德爱博恩妇产医院";
            this.fpPrint_Sheet1.Cells.Get(3, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(4, 0).ColumnSpan = 6;
            this.fpPrint_Sheet1.Cells.Get(4, 0).Value = "  作为本人生育保险定点医疗机构，于     年     月     日起在此医疗机构发生的符合";
            this.fpPrint_Sheet1.Cells.Get(4, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(5, 0).ColumnSpan = 6;
            this.fpPrint_Sheet1.Cells.Get(5, 0).Value = "  生育保险报销范围的产前检查医疗费用享受报销待遇。";
            this.fpPrint_Sheet1.Cells.Get(5, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(6, 0).ColumnSpan = 6;
            this.fpPrint_Sheet1.Cells.Get(6, 0).Value = "  说明：";
            this.fpPrint_Sheet1.Cells.Get(6, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(7, 0).ColumnSpan = 6;
            this.fpPrint_Sheet1.Cells.Get(7, 0).Value = "         1、在市内定点医疗机构就医时请您主动出示本人身份证明或社会保障卡直接办理";
            this.fpPrint_Sheet1.Cells.Get(7, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(8, 0).ColumnSpan = 6;
            this.fpPrint_Sheet1.Cells.Get(8, 0).Value = "  结算享受产前检查报销待遇。符合生育保险支付范围的医疗费用将由医疗机构记账，";
            this.fpPrint_Sheet1.Cells.Get(8, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(9, 0).ColumnSpan = 6;
            this.fpPrint_Sheet1.Cells.Get(9, 0).Value = "  其余费用由个人自费。";
            this.fpPrint_Sheet1.Cells.Get(9, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(10, 0).ColumnSpan = 6;
            this.fpPrint_Sheet1.Cells.Get(10, 0).Value = "       2、定点医院一经选定，原则上不予变更。如您因个人原因重新选定医疗机构的，  ";
            this.fpPrint_Sheet1.Cells.Get(10, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(11, 0).ColumnSpan = 6;
            this.fpPrint_Sheet1.Cells.Get(11, 0).Value = "  请您填写《佛山市职工生育保险就医确认变更申请表》（可网上下载，网址：http://";
            this.fpPrint_Sheet1.Cells.Get(11, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(12, 0).ColumnSpan = 6;
            this.fpPrint_Sheet1.Cells.Get(12, 0).Value = "  www.fssi.gov.cn）并持原《就医确认单》向参保所在地社会保险经办机构申请办理变";
            this.fpPrint_Sheet1.Cells.Get(12, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(13, 0).ColumnSpan = 6;
            this.fpPrint_Sheet1.Cells.Get(13, 0).Value = "  更手续。变更后的产前检查费用报销额度为产前检查的定额减去您在原选定医院已报";
            this.fpPrint_Sheet1.Cells.Get(13, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(14, 0).ColumnSpan = 6;
            this.fpPrint_Sheet1.Cells.Get(14, 0).Value = "  销的额度。";
            this.fpPrint_Sheet1.Cells.Get(14, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(15, 0).ColumnSpan = 6;
            this.fpPrint_Sheet1.Cells.Get(15, 0).Value = "         3、纳入报销的产前检查项目范围：";
            this.fpPrint_Sheet1.Cells.Get(15, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(16, 0).ColumnSpan = 6;
            this.fpPrint_Sheet1.Cells.Get(16, 0).Value = "         常规项目：产检、血常规、血型、血糖、尿常规、肝功能、肾功能、梅毒血清学检";
            this.fpPrint_Sheet1.Cells.Get(16, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(17, 0).ColumnSpan = 6;
            this.fpPrint_Sheet1.Cells.Get(17, 0).Value = "  测、乙肝表面抗原检测、HIV筛查、心电图、胎心监测、B超；";
            this.fpPrint_Sheet1.Cells.Get(17, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(18, 0).ColumnSpan = 6;
            this.fpPrint_Sheet1.Cells.Get(18, 0).Value = "         备查项目：非整倍体母体血清学筛查、丙型肝炎抗体筛查、血红蛋白电泳、抗D滴";
            this.fpPrint_Sheet1.Cells.Get(18, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(19, 0).ColumnSpan = 6;
            this.fpPrint_Sheet1.Cells.Get(19, 0).Value = "  度筛查（Rh阴性者）、甲状腺功能筛查、阴道分泌物检查、宫颈脱落细胞学检查、宫";
            this.fpPrint_Sheet1.Cells.Get(19, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(20, 0).ColumnSpan = 6;
            this.fpPrint_Sheet1.Cells.Get(20, 0).Value = "  颈分泌物检测淋球菌、宫颈分泌物检测沙眼衣原体。";
            this.fpPrint_Sheet1.Cells.Get(20, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(21, 0).ColumnSpan = 6;
            this.fpPrint_Sheet1.Cells.Get(21, 0).Value = "         4、分娩时您可随意选择任一医疗机构，但原则上建议您前往您选定的产前检查医";
            this.fpPrint_Sheet1.Cells.Get(21, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(22, 0).ColumnSpan = 6;
            this.fpPrint_Sheet1.Cells.Get(22, 0).Value = "  院，以减少不必要的重复检查。在市内定点医疗机构分娩的，符合生育保险支付范围";
            this.fpPrint_Sheet1.Cells.Get(22, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(23, 0).ColumnSpan = 6;
            this.fpPrint_Sheet1.Cells.Get(23, 0).Value = "  的医疗费用将由医疗机构记账，其余费用由个人自费。如您到市外的医疗机构分娩的，";
            this.fpPrint_Sheet1.Cells.Get(23, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(24, 0).ColumnSpan = 6;
            this.fpPrint_Sheet1.Cells.Get(24, 0).Value = "  按阴式分娩3000元、剖宫产5000元结算标准予以报销，低于结算标准按实报销，超出";
            this.fpPrint_Sheet1.Cells.Get(24, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(25, 0).ColumnSpan = 6;
            this.fpPrint_Sheet1.Cells.Get(25, 0).Value = "  部分不予报销。";
            this.fpPrint_Sheet1.Cells.Get(25, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(26, 0).ColumnSpan = 6;
            this.fpPrint_Sheet1.Cells.Get(26, 0).Value = "         5、参保职工因病情需要到市外就医的，须在入院前到参保所在地社保经办机构办理";
            this.fpPrint_Sheet1.Cells.Get(26, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(27, 0).ColumnSpan = 6;
            this.fpPrint_Sheet1.Cells.Get(27, 0).Value = "  核准手续。";
            this.fpPrint_Sheet1.Cells.Get(27, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(28, 0).ColumnSpan = 6;
            this.fpPrint_Sheet1.Cells.Get(28, 0).Value = "　       本人已阅读以上说明，并明确相关要求。";
            this.fpPrint_Sheet1.Cells.Get(28, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(29, 0).ColumnSpan = 6;
            this.fpPrint_Sheet1.Cells.Get(29, 0).Value = " （参保人抄写：）         ";
            this.fpPrint_Sheet1.Cells.Get(29, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(30, 0).ColumnSpan = 6;
            this.fpPrint_Sheet1.Cells.Get(30, 0).Value = "  参保人（签名）：                                    年      月     日";
            this.fpPrint_Sheet1.Cells.Get(30, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(31, 0).ColumnSpan = 6;
            this.fpPrint_Sheet1.Cells.Get(31, 0).Value = "  办理人（签名）：                                    年      月     日  ";
            this.fpPrint_Sheet1.Cells.Get(31, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(32, 0).ColumnSpan = 6;
            this.fpPrint_Sheet1.Cells.Get(32, 0).Value = "                                               就医确认专用（盖章）：";
            this.fpPrint_Sheet1.Cells.Get(32, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Cells.Get(33, 0).ColumnSpan = 6;
            this.fpPrint_Sheet1.Cells.Get(33, 0).Value = "  注：本单一式两份，参保人与社保经办机构各持一份。";
            this.fpPrint_Sheet1.Cells.Get(33, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpPrint_Sheet1.Columns.Get(0).Width = 85F;
            this.fpPrint_Sheet1.Columns.Get(1).Width = 91F;
            this.fpPrint_Sheet1.Columns.Get(2).Width = 67F;
            this.fpPrint_Sheet1.Columns.Get(3).Width = 133F;
            this.fpPrint_Sheet1.Columns.Get(4).Width = 99F;
            this.fpPrint_Sheet1.Columns.Get(5).Width = 222F;
            this.fpPrint_Sheet1.DefaultStyle.CellType = textCellType1;
            this.fpPrint_Sheet1.DefaultStyle.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpPrint_Sheet1.DefaultStyle.Locked = true;
            this.fpPrint_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.fpPrint_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpPrint_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpPrint_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpPrint_Sheet1.Rows.Get(0).Height = 34F;
            this.fpPrint_Sheet1.Rows.Get(29).Height = 22F;
            this.fpPrint_Sheet1.Rows.Get(30).Height = 22F;
            this.fpPrint_Sheet1.Rows.Get(31).Height = 22F;
            this.fpPrint_Sheet1.Rows.Get(32).Height = 22F;
            this.fpPrint_Sheet1.Rows.Get(33).Height = 35F;
            this.fpPrint_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpPrint_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ucMatnRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuPanel3);
            this.Controls.Add(this.neuPanel2);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucMatnRegister";
            this.Size = new System.Drawing.Size(1095, 918);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.neuPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPrint_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel3;
        private FS.FrameWork.WinForms.Controls.NeuTextBox tbCardNO;
        private System.Windows.Forms.Label lblCardType;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblPatientInfo;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox tbPregNumber;
        private FS.FrameWork.WinForms.Controls.NeuTextBox tbWeek;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpEnd;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpBegin;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbMatnType;
        private FS.FrameWork.WinForms.Controls.NeuButton btnRegister;
        private FS.FrameWork.WinForms.Controls.NeuButton btnCancelRegister;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpPrint;
        private FarPoint.Win.Spread.SheetView fpPrint_Sheet1;
    }
}
