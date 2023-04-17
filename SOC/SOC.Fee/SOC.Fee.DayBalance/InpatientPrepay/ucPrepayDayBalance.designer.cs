namespace SOC.Fee.DayBalance.InpatientPrepay
{
    partial class ucPrepayDayBalance
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
            FarPoint.Win.Spread.CellType.DateTimeCellType dateTimeCellType1 = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucPrepayDayBalance));
            this.panel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.neuDateTimePicker2 = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuDateTimePicker1 = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.panel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuLblMakeTableName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblCA = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblQF = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblTOT = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblUper = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblPOS = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblYJ = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel16 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel15 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel14 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel13 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel12 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel11 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel10 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel9 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel8 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel7 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblInvoiceRand = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblDateRange = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblInvCount = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuDateTimePicker3 = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.neuPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.neuDateTimePicker2);
            this.panel1.Controls.Add(this.neuDateTimePicker1);
            this.panel1.Location = new System.Drawing.Point(284, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(575, 48);
            this.panel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(296, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "本次日结结束时间";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(18, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "本次日结开始时间";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // neuDateTimePicker2
            // 
            this.neuDateTimePicker2.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.neuDateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.neuDateTimePicker2.IsEnter2Tab = false;
            this.neuDateTimePicker2.Location = new System.Drawing.Point(411, 12);
            this.neuDateTimePicker2.Name = "neuDateTimePicker2";
            this.neuDateTimePicker2.Size = new System.Drawing.Size(146, 21);
            this.neuDateTimePicker2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuDateTimePicker2.TabIndex = 1;
            // 
            // neuDateTimePicker1
            // 
            this.neuDateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.neuDateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.neuDateTimePicker1.IsEnter2Tab = false;
            this.neuDateTimePicker1.Location = new System.Drawing.Point(133, 12);
            this.neuDateTimePicker1.Name = "neuDateTimePicker1";
            this.neuDateTimePicker1.Size = new System.Drawing.Size(141, 21);
            this.neuDateTimePicker1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuDateTimePicker1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.neuLblMakeTableName);
            this.panel2.Controls.Add(this.lblCA);
            this.panel2.Controls.Add(this.lblQF);
            this.panel2.Controls.Add(this.lblTOT);
            this.panel2.Controls.Add(this.lblUper);
            this.panel2.Controls.Add(this.lblPOS);
            this.panel2.Controls.Add(this.lblYJ);
            this.panel2.Controls.Add(this.neuLabel16);
            this.panel2.Controls.Add(this.neuLabel15);
            this.panel2.Controls.Add(this.neuLabel14);
            this.panel2.Controls.Add(this.neuLabel13);
            this.panel2.Controls.Add(this.neuLabel12);
            this.panel2.Controls.Add(this.neuLabel11);
            this.panel2.Controls.Add(this.neuLabel10);
            this.panel2.Controls.Add(this.neuLabel9);
            this.panel2.Controls.Add(this.neuLabel8);
            this.panel2.Controls.Add(this.neuLabel7);
            this.panel2.Controls.Add(this.neuLabel6);
            this.panel2.Controls.Add(this.lblInvoiceRand);
            this.panel2.Controls.Add(this.lblDateRange);
            this.panel2.Controls.Add(this.neuLabel4);
            this.panel2.Controls.Add(this.lblInvCount);
            this.panel2.Controls.Add(this.neuLabel2);
            this.panel2.Controls.Add(this.neuLabel1);
            this.panel2.Location = new System.Drawing.Point(284, 54);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(432, 310);
            this.panel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panel2.TabIndex = 1;
            // 
            // neuLblMakeTableName
            // 
            this.neuLblMakeTableName.AutoSize = true;
            this.neuLblMakeTableName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLblMakeTableName.Location = new System.Drawing.Point(103, 277);
            this.neuLblMakeTableName.Name = "neuLblMakeTableName";
            this.neuLblMakeTableName.Size = new System.Drawing.Size(56, 16);
            this.neuLblMakeTableName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLblMakeTableName.TabIndex = 20;
            this.neuLblMakeTableName.Text = "制表人";
            // 
            // lblCA
            // 
            this.lblCA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblCA.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCA.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCA.Location = new System.Drawing.Point(181, 235);
            this.lblCA.Name = "lblCA";
            this.lblCA.Size = new System.Drawing.Size(218, 25);
            this.lblCA.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblCA.TabIndex = 19;
            this.lblCA.Text = "项目";
            this.lblCA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblQF
            // 
            this.lblQF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblQF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblQF.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblQF.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblQF.Location = new System.Drawing.Point(181, 139);
            this.lblQF.Name = "lblQF";
            this.lblQF.Size = new System.Drawing.Size(218, 25);
            this.lblQF.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblQF.TabIndex = 18;
            this.lblQF.Text = "项目";
            this.lblQF.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTOT
            // 
            this.lblTOT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTOT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTOT.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTOT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTOT.Location = new System.Drawing.Point(181, 163);
            this.lblTOT.Name = "lblTOT";
            this.lblTOT.Size = new System.Drawing.Size(218, 25);
            this.lblTOT.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblTOT.TabIndex = 18;
            this.lblTOT.Text = "项目";
            this.lblTOT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUper
            // 
            this.lblUper.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUper.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblUper.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblUper.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblUper.Location = new System.Drawing.Point(181, 187);
            this.lblUper.Name = "lblUper";
            this.lblUper.Size = new System.Drawing.Size(218, 25);
            this.lblUper.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblUper.TabIndex = 18;
            this.lblUper.Text = "项目";
            this.lblUper.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPOS
            // 
            this.lblPOS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPOS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPOS.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPOS.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPOS.Location = new System.Drawing.Point(181, 211);
            this.lblPOS.Name = "lblPOS";
            this.lblPOS.Size = new System.Drawing.Size(218, 25);
            this.lblPOS.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblPOS.TabIndex = 18;
            this.lblPOS.Text = "项目";
            this.lblPOS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblYJ
            // 
            this.lblYJ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblYJ.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblYJ.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblYJ.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblYJ.Location = new System.Drawing.Point(181, 115);
            this.lblYJ.Name = "lblYJ";
            this.lblYJ.Size = new System.Drawing.Size(218, 25);
            this.lblYJ.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblYJ.TabIndex = 17;
            this.lblYJ.Text = "项目";
            this.lblYJ.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // neuLabel16
            // 
            this.neuLabel16.AutoSize = true;
            this.neuLabel16.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel16.Location = new System.Drawing.Point(276, 277);
            this.neuLabel16.Name = "neuLabel16";
            this.neuLabel16.Size = new System.Drawing.Size(88, 16);
            this.neuLabel16.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel16.TabIndex = 16;
            this.neuLabel16.Text = "收款员签名";
            // 
            // neuLabel15
            // 
            this.neuLabel15.AutoSize = true;
            this.neuLabel15.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel15.Location = new System.Drawing.Point(163, 277);
            this.neuLabel15.Name = "neuLabel15";
            this.neuLabel15.Size = new System.Drawing.Size(56, 16);
            this.neuLabel15.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel15.TabIndex = 15;
            this.neuLabel15.Text = "审核人";
            // 
            // neuLabel14
            // 
            this.neuLabel14.AutoSize = true;
            this.neuLabel14.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel14.Location = new System.Drawing.Point(49, 277);
            this.neuLabel14.Name = "neuLabel14";
            this.neuLabel14.Size = new System.Drawing.Size(56, 16);
            this.neuLabel14.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel14.TabIndex = 14;
            this.neuLabel14.Text = "制表人";
            // 
            // neuLabel13
            // 
            this.neuLabel13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.neuLabel13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.neuLabel13.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel13.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.neuLabel13.Location = new System.Drawing.Point(50, 235);
            this.neuLabel13.Name = "neuLabel13";
            this.neuLabel13.Size = new System.Drawing.Size(132, 25);
            this.neuLabel13.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel13.TabIndex = 13;
            this.neuLabel13.Text = "现金";
            this.neuLabel13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // neuLabel12
            // 
            this.neuLabel12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.neuLabel12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.neuLabel12.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel12.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.neuLabel12.Location = new System.Drawing.Point(50, 211);
            this.neuLabel12.Name = "neuLabel12";
            this.neuLabel12.Size = new System.Drawing.Size(132, 25);
            this.neuLabel12.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel12.TabIndex = 12;
            this.neuLabel12.Text = "银联";
            this.neuLabel12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // neuLabel11
            // 
            this.neuLabel11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.neuLabel11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.neuLabel11.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel11.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.neuLabel11.Location = new System.Drawing.Point(50, 187);
            this.neuLabel11.Name = "neuLabel11";
            this.neuLabel11.Size = new System.Drawing.Size(132, 25);
            this.neuLabel11.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel11.TabIndex = 11;
            this.neuLabel11.Text = "总计（大写）";
            this.neuLabel11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // neuLabel10
            // 
            this.neuLabel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.neuLabel10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.neuLabel10.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel10.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.neuLabel10.Location = new System.Drawing.Point(50, 163);
            this.neuLabel10.Name = "neuLabel10";
            this.neuLabel10.Size = new System.Drawing.Size(132, 25);
            this.neuLabel10.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel10.TabIndex = 10;
            this.neuLabel10.Text = "总计";
            this.neuLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // neuLabel9
            // 
            this.neuLabel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.neuLabel9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.neuLabel9.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel9.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.neuLabel9.Location = new System.Drawing.Point(50, 139);
            this.neuLabel9.Name = "neuLabel9";
            this.neuLabel9.Size = new System.Drawing.Size(132, 25);
            this.neuLabel9.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel9.TabIndex = 9;
            this.neuLabel9.Text = "退款";
            this.neuLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // neuLabel8
            // 
            this.neuLabel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.neuLabel8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.neuLabel8.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel8.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.neuLabel8.Location = new System.Drawing.Point(50, 115);
            this.neuLabel8.Name = "neuLabel8";
            this.neuLabel8.Size = new System.Drawing.Size(132, 25);
            this.neuLabel8.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel8.TabIndex = 8;
            this.neuLabel8.Text = "预交金";
            this.neuLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // neuLabel7
            // 
            this.neuLabel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.neuLabel7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.neuLabel7.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel7.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.neuLabel7.Location = new System.Drawing.Point(181, 91);
            this.neuLabel7.Name = "neuLabel7";
            this.neuLabel7.Size = new System.Drawing.Size(218, 25);
            this.neuLabel7.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel7.TabIndex = 7;
            this.neuLabel7.Text = "金额";
            this.neuLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // neuLabel6
            // 
            this.neuLabel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.neuLabel6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.neuLabel6.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel6.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.neuLabel6.Location = new System.Drawing.Point(50, 91);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(132, 25);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 6;
            this.neuLabel6.Text = "项目";
            this.neuLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInvoiceRand
            // 
            this.lblInvoiceRand.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInvoiceRand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblInvoiceRand.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInvoiceRand.Location = new System.Drawing.Point(50, 67);
            this.lblInvoiceRand.Name = "lblInvoiceRand";
            this.lblInvoiceRand.Size = new System.Drawing.Size(349, 25);
            this.lblInvoiceRand.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblInvoiceRand.TabIndex = 5;
            this.lblInvoiceRand.Text = "流水起止号：";
            this.lblInvoiceRand.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDateRange
            // 
            this.lblDateRange.AutoSize = true;
            this.lblDateRange.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDateRange.Location = new System.Drawing.Point(189, 43);
            this.lblDateRange.Name = "lblDateRange";
            this.lblDateRange.Size = new System.Drawing.Size(23, 12);
            this.lblDateRange.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblDateRange.TabIndex = 4;
            this.lblDateRange.Text = "100";
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel4.Location = new System.Drawing.Point(155, 41);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(40, 16);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 3;
            this.neuLabel4.Text = "时间";
            // 
            // lblInvCount
            // 
            this.lblInvCount.AutoSize = true;
            this.lblInvCount.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInvCount.Location = new System.Drawing.Point(127, 41);
            this.lblInvCount.Name = "lblInvCount";
            this.lblInvCount.Size = new System.Drawing.Size(32, 16);
            this.lblInvCount.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblInvCount.TabIndex = 2;
            this.lblInvCount.Text = "100";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel2.Location = new System.Drawing.Point(49, 41);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(80, 16);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 1;
            this.neuLabel2.Text = "单据张数:";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel1.Location = new System.Drawing.Point(121, 10);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(161, 19);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "住院预交金日报表";
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.neuDateTimePicker3);
            this.neuPanel1.Controls.Add(this.label4);
            this.neuPanel1.Controls.Add(this.neuSpread1);
            this.neuPanel1.Controls.Add(this.label3);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(223, 385);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 2;
            // 
            // neuDateTimePicker3
            // 
            this.neuDateTimePicker3.CustomFormat = "yyyy-MM";
            this.neuDateTimePicker3.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.neuDateTimePicker3.IsEnter2Tab = false;
            this.neuDateTimePicker3.Location = new System.Drawing.Point(123, 21);
            this.neuDateTimePicker3.Name = "neuDateTimePicker3";
            this.neuDateTimePicker3.Size = new System.Drawing.Size(97, 21);
            this.neuDateTimePicker3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuDateTimePicker3.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 21);
            this.label4.TabIndex = 4;
            this.label4.Text = "历时日结月份";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, ";
            this.neuSpread1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.FileName = "";
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(0, 40);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(223, 345);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 4;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.neuSpread1_CellDoubleClick);
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ColumnCount = 1;
            this.neuSpread1_Sheet1.RowCount = 1;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "历史日结结束时间";
            this.neuSpread1_Sheet1.ColumnHeader.Columns.Default.NoteIndicatorColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.ColumnHeader.Rows.Default.NoteIndicatorColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.Columns.Get(0).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            dateTimeCellType1.Calendar = ((System.Globalization.Calendar)(resources.GetObject("dateTimeCellType1.Calendar")));
            dateTimeCellType1.CalendarDayFont = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dateTimeCellType1.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText;
            dateTimeCellType1.DateDefault = new System.DateTime(2011, 1, 18, 9, 6, 9, 0);
            dateTimeCellType1.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.LongDateWithTime;
            dateTimeCellType1.TimeDefault = new System.DateTime(2011, 1, 18, 9, 6, 9, 0);
            this.neuSpread1_Sheet1.Columns.Get(0).CellType = dateTimeCellType1;
            this.neuSpread1_Sheet1.Columns.Get(0).ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "历史日结结束时间";
            this.neuSpread1_Sheet1.Columns.Get(0).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(0).NoteIndicatorColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 217F;
            this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.Visible = false;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(-2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(193, 21);
            this.label3.TabIndex = 3;
            this.label3.Text = "历时日结记录";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucPrepayDayBalance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuPanel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ucPrepayDayBalance";
            this.Size = new System.Drawing.Size(892, 385);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.neuPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel panel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel panel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblInvCount;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblInvoiceRand;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblDateRange;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel13;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel12;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel11;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel10;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel9;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel8;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel7;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker neuDateTimePicker1;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblCA;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblQF;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblTOT;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblUper;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblPOS;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblYJ;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel16;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel15;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel14;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker neuDateTimePicker2;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker neuDateTimePicker3;
        private System.Windows.Forms.Label label4;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLblMakeTableName;
    }
}
