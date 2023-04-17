namespace GJLocal.HISFC.Components.OpGuide.ClinicsBillPrint
{
    partial class ucClinicsBillPrint
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
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            this.lblPhaDoc = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.neuPanelItems = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.fpSpreadItems = new FarPoint.Win.Spread.FpSpread();
            this.fpSpreadItemsSheet = new FarPoint.Win.Spread.SheetView();
            this.chkOwn = new System.Windows.Forms.CheckBox();
            this.chkPub = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.chkPay = new System.Windows.Forms.CheckBox();
            this.chkOth = new System.Windows.Forms.CheckBox();
            this.labelSeeDate = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelPhoneAddr = new System.Windows.Forms.Label();
            this.lblSex = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblAge = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblCardNo = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblSeeDept = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelCost = new System.Windows.Forms.Label();
            this.fpSpreadFootSheet = new FarPoint.Win.Spread.SheetView();
            this.fpSpreadFoot = new FarPoint.Win.Spread.FpSpread();
            this.npbBarCode = new FS.FrameWork.WinForms.Controls.NeuPictureBox();
            this.lblPrintDate = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.neuPanelItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpreadItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpreadItemsSheet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpreadFootSheet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpreadFoot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.npbBarCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPhaDoc
            // 
            this.lblPhaDoc.AutoSize = true;
            this.lblPhaDoc.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPhaDoc.ForeColor = System.Drawing.Color.Black;
            this.lblPhaDoc.Location = new System.Drawing.Point(73, 585);
            this.lblPhaDoc.Name = "lblPhaDoc";
            this.lblPhaDoc.Size = new System.Drawing.Size(55, 14);
            this.lblPhaDoc.TabIndex = 52;
            this.lblPhaDoc.Text = "095124";
            this.lblPhaDoc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label22.ForeColor = System.Drawing.Color.Black;
            this.label22.Location = new System.Drawing.Point(8, 585);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(70, 14);
            this.label22.TabIndex = 25;
            this.label22.Text = "医生工号:";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.ForeColor = System.Drawing.Color.Black;
            this.panel3.Location = new System.Drawing.Point(4, 575);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(560, 1);
            this.panel3.TabIndex = 173;
            // 
            // neuPanelItems
            // 
            this.neuPanelItems.Controls.Add(this.fpSpreadItems);
            this.neuPanelItems.Location = new System.Drawing.Point(6, 217);
            this.neuPanelItems.Name = "neuPanelItems";
            this.neuPanelItems.Size = new System.Drawing.Size(549, 352);
            this.neuPanelItems.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanelItems.TabIndex = 186;
            // 
            // fpSpreadItems
            // 
            this.fpSpreadItems.About = "3.0.2004.2005";
            this.fpSpreadItems.AccessibleDescription = "fpSpreadItems, fpSpreadItemsSheet, Row 0, Column 0, 组号";
            this.fpSpreadItems.BackColor = System.Drawing.Color.White;
            this.fpSpreadItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.fpSpreadItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpreadItems.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpreadItems.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            this.fpSpreadItems.Location = new System.Drawing.Point(0, 0);
            this.fpSpreadItems.Name = "fpSpreadItems";
            this.fpSpreadItems.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpreadItems.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpreadItemsSheet});
            this.fpSpreadItems.Size = new System.Drawing.Size(549, 352);
            this.fpSpreadItems.TabIndex = 1;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpreadItems.TextTipAppearance = tipAppearance1;
            this.fpSpreadItems.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            // 
            // fpSpreadItemsSheet
            // 
            this.fpSpreadItemsSheet.Reset();
            this.fpSpreadItemsSheet.SheetName = "fpSpreadItemsSheet";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpreadItemsSheet.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpreadItemsSheet.ColumnCount = 6;
            this.fpSpreadItemsSheet.RowCount = 10;
            this.fpSpreadItemsSheet.RowHeader.ColumnCount = 0;
            this.fpSpreadItemsSheet.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.Black, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.None, System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.White, false, false, false, false, false);
            this.fpSpreadItemsSheet.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpreadItemsSheet.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.fpSpreadItemsSheet.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            textCellType1.Multiline = true;
            textCellType1.WordWrap = true;
            this.fpSpreadItemsSheet.Columns.Get(0).CellType = textCellType1;
            this.fpSpreadItemsSheet.Columns.Get(0).Width = 37F;
            this.fpSpreadItemsSheet.Columns.Get(1).CellType = textCellType1;
            this.fpSpreadItemsSheet.Columns.Get(1).Width = 230F;
            this.fpSpreadItemsSheet.Columns.Get(2).CellType = textCellType1;
            this.fpSpreadItemsSheet.Columns.Get(2).Width = 61F;
            this.fpSpreadItemsSheet.Columns.Get(3).CellType = textCellType1;
            this.fpSpreadItemsSheet.Columns.Get(3).Width = 64F;
            this.fpSpreadItemsSheet.Columns.Get(4).CellType = textCellType1;
            this.fpSpreadItemsSheet.Columns.Get(5).CellType = textCellType1;
            this.fpSpreadItemsSheet.Columns.Get(5).Width = 61F;
            this.fpSpreadItemsSheet.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpreadItemsSheet.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpreadItemsSheet.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpreadItemsSheet.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.fpSpreadItemsSheet.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpreadItemsSheet.RowHeader.Visible = false;
            this.fpSpreadItemsSheet.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.fpSpreadItemsSheet.SheetCornerStyle.ForeColor = System.Drawing.Color.White;
            this.fpSpreadItemsSheet.SheetCornerStyle.Parent = "CornerDefault";
            this.fpSpreadItemsSheet.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpSpreadItemsSheet.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // chkOwn
            // 
            this.chkOwn.AutoSize = true;
            this.chkOwn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkOwn.Font = new System.Drawing.Font("宋体", 10.5F);
            this.chkOwn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkOwn.Location = new System.Drawing.Point(126, 88);
            this.chkOwn.Name = "chkOwn";
            this.chkOwn.Size = new System.Drawing.Size(51, 18);
            this.chkOwn.TabIndex = 341;
            this.chkOwn.Text = "自费";
            this.chkOwn.Visible = false;
            // 
            // chkPub
            // 
            this.chkPub.AutoSize = true;
            this.chkPub.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkPub.Font = new System.Drawing.Font("宋体", 10.5F);
            this.chkPub.ForeColor = System.Drawing.Color.Black;
            this.chkPub.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkPub.Location = new System.Drawing.Point(67, 88);
            this.chkPub.Name = "chkPub";
            this.chkPub.Size = new System.Drawing.Size(51, 18);
            this.chkPub.TabIndex = 340;
            this.chkPub.Text = "公费";
            this.chkPub.Visible = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label14.Location = new System.Drawing.Point(8, 99);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(49, 14);
            this.label14.TabIndex = 339;
            this.label14.Text = "费别：";
            this.label14.Visible = false;
            // 
            // labelTitle
            // 
            this.labelTitle.Font = new System.Drawing.Font("楷体", 21.75F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.Black;
            this.labelTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelTitle.Location = new System.Drawing.Point(2, 80);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(549, 34);
            this.labelTitle.TabIndex = 338;
            this.labelTitle.Text = "贝利尔医疗治疗单";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkPay
            // 
            this.chkPay.AutoSize = true;
            this.chkPay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkPay.Font = new System.Drawing.Font("宋体", 10.5F);
            this.chkPay.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkPay.Location = new System.Drawing.Point(67, 108);
            this.chkPay.Name = "chkPay";
            this.chkPay.Size = new System.Drawing.Size(51, 18);
            this.chkPay.TabIndex = 342;
            this.chkPay.Text = "医保";
            this.chkPay.Visible = false;
            // 
            // chkOth
            // 
            this.chkOth.AutoSize = true;
            this.chkOth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkOth.Font = new System.Drawing.Font("宋体", 10.5F);
            this.chkOth.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkOth.Location = new System.Drawing.Point(126, 108);
            this.chkOth.Name = "chkOth";
            this.chkOth.Size = new System.Drawing.Size(51, 18);
            this.chkOth.TabIndex = 343;
            this.chkOth.Text = "其他";
            this.chkOth.Visible = false;
            // 
            // labelSeeDate
            // 
            this.labelSeeDate.AutoSize = true;
            this.labelSeeDate.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Underline);
            this.labelSeeDate.ForeColor = System.Drawing.Color.Black;
            this.labelSeeDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelSeeDate.Location = new System.Drawing.Point(460, 165);
            this.labelSeeDate.Name = "labelSeeDate";
            this.labelSeeDate.Size = new System.Drawing.Size(77, 14);
            this.labelSeeDate.TabIndex = 337;
            this.labelSeeDate.Text = "2014.05.08";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.ForeColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(4, 212);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(560, 1);
            this.panel1.TabIndex = 336;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.ForeColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(4, 130);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(560, 1);
            this.panel2.TabIndex = 335;
            // 
            // labelPhoneAddr
            // 
            this.labelPhoneAddr.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Underline);
            this.labelPhoneAddr.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelPhoneAddr.Location = new System.Drawing.Point(182, 191);
            this.labelPhoneAddr.Name = "labelPhoneAddr";
            this.labelPhoneAddr.Size = new System.Drawing.Size(248, 14);
            this.labelPhoneAddr.TabIndex = 334;
            this.labelPhoneAddr.Text = "0756-99999/花果山水帘洞";
            // 
            // lblSex
            // 
            this.lblSex.AutoSize = true;
            this.lblSex.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Underline);
            this.lblSex.ForeColor = System.Drawing.Color.Black;
            this.lblSex.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSex.Location = new System.Drawing.Point(280, 137);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(21, 14);
            this.lblSex.TabIndex = 333;
            this.lblSex.Text = "女";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(2, 191);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(168, 14);
            this.label9.TabIndex = 332;
            this.label9.Text = "电话Phone/地址Address：";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label31.ForeColor = System.Drawing.Color.Black;
            this.label31.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label31.Location = new System.Drawing.Point(349, 165);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(112, 14);
            this.label31.TabIndex = 330;
            this.label31.Text = "就诊日期 Date：";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Underline);
            this.lblName.ForeColor = System.Drawing.Color.Black;
            this.lblName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblName.Location = new System.Drawing.Point(85, 137);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(49, 14);
            this.lblName.TabIndex = 327;
            this.lblName.Text = "孙悟空";
            // 
            // lblAge
            // 
            this.lblAge.AutoSize = true;
            this.lblAge.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Underline);
            this.lblAge.ForeColor = System.Drawing.Color.Black;
            this.lblAge.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblAge.Location = new System.Drawing.Point(460, 137);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(35, 14);
            this.lblAge.TabIndex = 331;
            this.lblAge.Text = "35岁";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(349, 137);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 14);
            this.label8.TabIndex = 324;
            this.label8.Text = "年龄 Age：";
            // 
            // lblCardNo
            // 
            this.lblCardNo.AutoSize = true;
            this.lblCardNo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Underline);
            this.lblCardNo.ForeColor = System.Drawing.Color.Black;
            this.lblCardNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCardNo.Location = new System.Drawing.Point(100, 164);
            this.lblCardNo.Name = "lblCardNo";
            this.lblCardNo.Size = new System.Drawing.Size(63, 14);
            this.lblCardNo.TabIndex = 329;
            this.lblCardNo.Text = "00184023";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(194, 137);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 14);
            this.label7.TabIndex = 323;
            this.label7.Text = "性别 Sex：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(2, 137);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 14);
            this.label6.TabIndex = 322;
            this.label6.Text = "姓名 Name：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label12.Location = new System.Drawing.Point(2, 164);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(42, 14);
            this.label12.TabIndex = 326;
            this.label12.Text = "No.：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(194, 164);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(84, 14);
            this.label10.TabIndex = 325;
            this.label10.Text = "科别 Dept：";
            // 
            // lblSeeDept
            // 
            this.lblSeeDept.AutoSize = true;
            this.lblSeeDept.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Underline);
            this.lblSeeDept.ForeColor = System.Drawing.Color.Black;
            this.lblSeeDept.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSeeDept.Location = new System.Drawing.Point(280, 164);
            this.lblSeeDept.Name = "lblSeeDept";
            this.lblSeeDept.Size = new System.Drawing.Size(49, 14);
            this.lblSeeDept.TabIndex = 328;
            this.lblSeeDept.Text = "皮肤科";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(219, 585);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 14);
            this.label1.TabIndex = 344;
            this.label1.Text = "医生签名:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(426, 585);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 14);
            this.label2.TabIndex = 345;
            this.label2.Text = "金额:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelCost
            // 
            this.labelCost.AutoSize = true;
            this.labelCost.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCost.ForeColor = System.Drawing.Color.Black;
            this.labelCost.Location = new System.Drawing.Point(467, 585);
            this.labelCost.Name = "labelCost";
            this.labelCost.Size = new System.Drawing.Size(62, 14);
            this.labelCost.TabIndex = 346;
            this.labelCost.Text = "57.00元";
            this.labelCost.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fpSpreadFootSheet
            // 
            this.fpSpreadFootSheet.Reset();
            this.fpSpreadFootSheet.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpreadFootSheet.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpreadFootSheet.ColumnCount = 7;
            this.fpSpreadFootSheet.RowCount = 4;
            this.fpSpreadFootSheet.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.None, System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.White, System.Drawing.Color.White, false, false, false, false, false);
            this.fpSpreadFootSheet.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpreadFootSheet.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.fpSpreadFootSheet.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpreadFootSheet.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpreadFootSheet.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.fpSpreadFootSheet.PrintInfo.UseMax = false;
            this.fpSpreadFootSheet.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpreadFootSheet.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.fpSpreadFootSheet.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpreadFootSheet.RowHeader.Visible = false;
            this.fpSpreadFootSheet.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.fpSpreadFootSheet.SheetCornerStyle.ForeColor = System.Drawing.Color.White;
            this.fpSpreadFootSheet.SheetCornerStyle.Parent = "CornerDefault";
            this.fpSpreadFootSheet.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpSpreadFootSheet.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // fpSpreadFoot
            // 
            this.fpSpreadFoot.About = "3.0.2004.2005";
            this.fpSpreadFoot.AccessibleDescription = "fpSpreadFoot, Sheet1, Row 0, Column 0, 执行者";
            this.fpSpreadFoot.BackColor = System.Drawing.Color.White;
            this.fpSpreadFoot.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.fpSpreadFoot.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpreadFoot.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            this.fpSpreadFoot.Location = new System.Drawing.Point(10, 633);
            this.fpSpreadFoot.Name = "fpSpreadFoot";
            this.fpSpreadFoot.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpreadFoot.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpreadFootSheet});
            this.fpSpreadFoot.Size = new System.Drawing.Size(537, 121);
            this.fpSpreadFoot.TabIndex = 347;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpreadFoot.TextTipAppearance = tipAppearance2;
            this.fpSpreadFoot.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            // 
            // npbBarCode
            // 
            this.npbBarCode.Location = new System.Drawing.Point(345, 19);
            this.npbBarCode.Name = "npbBarCode";
            this.npbBarCode.Size = new System.Drawing.Size(150, 39);
            this.npbBarCode.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.npbBarCode.TabIndex = 349;
            this.npbBarCode.TabStop = false;
            this.npbBarCode.Visible = false;
            // 
            // lblPrintDate
            // 
            this.lblPrintDate.AutoSize = true;
            this.lblPrintDate.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPrintDate.ForeColor = System.Drawing.Color.Black;
            this.lblPrintDate.Location = new System.Drawing.Point(73, 610);
            this.lblPrintDate.Name = "lblPrintDate";
            this.lblPrintDate.Size = new System.Drawing.Size(133, 14);
            this.lblPrintDate.TabIndex = 352;
            this.lblPrintDate.Text = "2014-9-16 19:03:45";
            this.lblPrintDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(8, 610);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 14);
            this.label3.TabIndex = 351;
            this.label3.Text = "打印时间:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::GJLocal.HISFC.Components.OpGuide.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(43, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(85, 90);
            this.pictureBox1.TabIndex = 375;
            this.pictureBox1.TabStop = false;
            // 
            // ucClinicsBillPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.lblPrintDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.npbBarCode);
            this.Controls.Add(this.labelCost);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkOwn);
            this.Controls.Add(this.chkPub);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.fpSpreadFoot);
            this.Controls.Add(this.lblPhaDoc);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.chkPay);
            this.Controls.Add(this.chkOth);
            this.Controls.Add(this.labelSeeDate);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.labelPhoneAddr);
            this.Controls.Add(this.lblSex);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblAge);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblCardNo);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblSeeDept);
            this.Controls.Add(this.neuPanelItems);
            this.Controls.Add(this.panel3);
            this.Name = "ucClinicsBillPrint";
            this.Size = new System.Drawing.Size(550, 800);
            this.neuPanelItems.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpreadItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpreadItemsSheet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpreadFootSheet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpreadFoot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.npbBarCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Label lblPhaDoc;
        protected System.Windows.Forms.Label label22;
        protected System.Windows.Forms.Panel panel3;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanelItems;
        private FarPoint.Win.Spread.FpSpread fpSpreadItems;
        private FarPoint.Win.Spread.SheetView fpSpreadItemsSheet;
        private System.Windows.Forms.CheckBox chkOwn;
        private System.Windows.Forms.CheckBox chkPub;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.CheckBox chkPay;
        private System.Windows.Forms.CheckBox chkOth;
        private System.Windows.Forms.Label labelSeeDate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelPhoneAddr;
        private System.Windows.Forms.Label lblSex;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblCardNo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblSeeDept;
        protected System.Windows.Forms.Label label1;
        protected System.Windows.Forms.Label label2;
        protected System.Windows.Forms.Label labelCost;
        private FarPoint.Win.Spread.FpSpread fpSpreadFoot;
        private FarPoint.Win.Spread.SheetView fpSpreadFootSheet;
        protected FS.FrameWork.WinForms.Controls.NeuPictureBox npbBarCode;
        private System.Windows.Forms.Label lblPrintDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;

    }
}
