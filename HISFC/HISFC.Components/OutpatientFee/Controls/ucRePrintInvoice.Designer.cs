﻿namespace FS.HISFC.Components.OutpatientFee.Controls
{
    partial class ucRePrintInvoice
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.ucTrvInvoice = new FS.HISFC.Components.OutpatientFee.Controls.ucInvoiceTree();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.fpSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.lblNextInvoiceNO = new System.Windows.Forms.Label();
            this.tbInvoiceDate = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel7 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbPactInfo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel8 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbPubCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel9 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbPayCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbOwnCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbTotCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbOperName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbPName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbInvoiceNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucTrvInvoice
            // 
            this.ucTrvInvoice.Dock = System.Windows.Forms.DockStyle.Left;
            this.ucTrvInvoice.Location = new System.Drawing.Point(0, 0);
            this.ucTrvInvoice.Name = "ucTrvInvoice";
            this.ucTrvInvoice.Size = new System.Drawing.Size(218, 512);
            this.ucTrvInvoice.TabIndex = 0;
            this.ucTrvInvoice.UcWidth = 220;
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.fpSpread1);
            this.pnlRight.Controls.Add(this.groupBox1);
            this.pnlRight.Controls.Add(this.panel1);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(218, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(690, 512);
            this.pnlRight.TabIndex = 1;
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "3.0.2004.2005";
            this.fpSpread1.AccessibleDescription = "fpSpread1, Sheet1, Row 0, Column 0, ";
            this.fpSpread1.BackColor = System.Drawing.Color.White;
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread1.FileName = "";
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.IsAutoSaveGridStatus = false;
            this.fpSpread1.IsCanCustomConfigColumn = false;
            this.fpSpread1.Location = new System.Drawing.Point(5, 120);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1});
            this.fpSpread1.Size = new System.Drawing.Size(685, 392);
            this.fpSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpSpread1.TabIndex = 3;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread1.TextTipAppearance = tipAppearance1;
            this.fpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.ColumnCount = 5;
            this.fpSpread1_Sheet1.RowCount = 5;
            this.fpSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "费用名称";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "自费金额";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "自付金额";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "记帐金额";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "总金额";
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.fpSpread1_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.fpSpread1_Sheet1.Columns.Get(0).Label = "费用名称";
            this.fpSpread1_Sheet1.Columns.Get(0).Width = 138F;
            this.fpSpread1_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.fpSpread1_Sheet1.Columns.Get(1).Label = "自费金额";
            this.fpSpread1_Sheet1.Columns.Get(1).Width = 72F;
            this.fpSpread1_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.fpSpread1_Sheet1.Columns.Get(2).Label = "自付金额";
            this.fpSpread1_Sheet1.Columns.Get(2).Width = 72F;
            this.fpSpread1_Sheet1.Columns.Get(3).CellType = textCellType4;
            this.fpSpread1_Sheet1.Columns.Get(3).Label = "记帐金额";
            this.fpSpread1_Sheet1.Columns.Get(3).Width = 72F;
            this.fpSpread1_Sheet1.Columns.Get(4).CellType = textCellType5;
            this.fpSpread1_Sheet1.Columns.Get(4).Label = "总金额";
            this.fpSpread1_Sheet1.Columns.Get(4).Width = 110F;
            this.fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.Rows.Default.Height = 30F;
            this.fpSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSpread1_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.fpSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblNextInvoiceNO);
            this.groupBox1.Controls.Add(this.tbInvoiceDate);
            this.groupBox1.Controls.Add(this.neuLabel7);
            this.groupBox1.Controls.Add(this.tbPactInfo);
            this.groupBox1.Controls.Add(this.neuLabel8);
            this.groupBox1.Controls.Add(this.tbPubCost);
            this.groupBox1.Controls.Add(this.neuLabel9);
            this.groupBox1.Controls.Add(this.tbPayCost);
            this.groupBox1.Controls.Add(this.neuLabel4);
            this.groupBox1.Controls.Add(this.tbOwnCost);
            this.groupBox1.Controls.Add(this.neuLabel5);
            this.groupBox1.Controls.Add(this.tbTotCost);
            this.groupBox1.Controls.Add(this.neuLabel6);
            this.groupBox1.Controls.Add(this.tbOperName);
            this.groupBox1.Controls.Add(this.neuLabel3);
            this.groupBox1.Controls.Add(this.tbPName);
            this.groupBox1.Controls.Add(this.neuLabel2);
            this.groupBox1.Controls.Add(this.tbInvoiceNo);
            this.groupBox1.Controls.Add(this.neuLabel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(5, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(685, 120);
            this.groupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "发票信息";
            // 
            // lblNextInvoiceNO
            // 
            this.lblNextInvoiceNO.AutoSize = true;
            this.lblNextInvoiceNO.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNextInvoiceNO.ForeColor = System.Drawing.Color.Red;
            this.lblNextInvoiceNO.Location = new System.Drawing.Point(640, 46);
            this.lblNextInvoiceNO.Name = "lblNextInvoiceNO";
            this.lblNextInvoiceNO.Size = new System.Drawing.Size(88, 24);
            this.lblNextInvoiceNO.TabIndex = 19;
            this.lblNextInvoiceNO.Text = "label1";
            // 
            // tbInvoiceDate
            // 
            this.tbInvoiceDate.Font = new System.Drawing.Font("宋体", 10F);
            this.tbInvoiceDate.IsEnter2Tab = false;
            this.tbInvoiceDate.Location = new System.Drawing.Point(462, 74);
            this.tbInvoiceDate.Name = "tbInvoiceDate";
            this.tbInvoiceDate.Size = new System.Drawing.Size(156, 23);
            this.tbInvoiceDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbInvoiceDate.TabIndex = 17;
            this.tbInvoiceDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // neuLabel7
            // 
            this.neuLabel7.AutoSize = true;
            this.neuLabel7.Font = new System.Drawing.Font("宋体", 11.25F);
            this.neuLabel7.Location = new System.Drawing.Point(380, 78);
            this.neuLabel7.Name = "neuLabel7";
            this.neuLabel7.Size = new System.Drawing.Size(75, 15);
            this.neuLabel7.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel7.TabIndex = 18;
            this.neuLabel7.Text = "发票日期:";
            // 
            // tbPactInfo
            // 
            this.tbPactInfo.Font = new System.Drawing.Font("宋体", 10F);
            this.tbPactInfo.IsEnter2Tab = false;
            this.tbPactInfo.Location = new System.Drawing.Point(274, 74);
            this.tbPactInfo.Name = "tbPactInfo";
            this.tbPactInfo.Size = new System.Drawing.Size(100, 23);
            this.tbPactInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbPactInfo.TabIndex = 15;
            this.tbPactInfo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // neuLabel8
            // 
            this.neuLabel8.AutoSize = true;
            this.neuLabel8.Font = new System.Drawing.Font("宋体", 11.25F);
            this.neuLabel8.Location = new System.Drawing.Point(193, 78);
            this.neuLabel8.Name = "neuLabel8";
            this.neuLabel8.Size = new System.Drawing.Size(75, 15);
            this.neuLabel8.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel8.TabIndex = 16;
            this.neuLabel8.Text = "合同单位:";
            // 
            // tbPubCost
            // 
            this.tbPubCost.Font = new System.Drawing.Font("宋体", 10F);
            this.tbPubCost.IsEnter2Tab = false;
            this.tbPubCost.Location = new System.Drawing.Point(87, 74);
            this.tbPubCost.Name = "tbPubCost";
            this.tbPubCost.Size = new System.Drawing.Size(100, 23);
            this.tbPubCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbPubCost.TabIndex = 13;
            this.tbPubCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // neuLabel9
            // 
            this.neuLabel9.AutoSize = true;
            this.neuLabel9.Font = new System.Drawing.Font("宋体", 11.25F);
            this.neuLabel9.Location = new System.Drawing.Point(5, 78);
            this.neuLabel9.Name = "neuLabel9";
            this.neuLabel9.Size = new System.Drawing.Size(75, 15);
            this.neuLabel9.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel9.TabIndex = 14;
            this.neuLabel9.Text = "记帐支付:";
            // 
            // tbPayCost
            // 
            this.tbPayCost.Font = new System.Drawing.Font("宋体", 10F);
            this.tbPayCost.IsEnter2Tab = false;
            this.tbPayCost.Location = new System.Drawing.Point(462, 46);
            this.tbPayCost.Name = "tbPayCost";
            this.tbPayCost.Size = new System.Drawing.Size(156, 23);
            this.tbPayCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbPayCost.TabIndex = 11;
            this.tbPayCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Font = new System.Drawing.Font("宋体", 11.25F);
            this.neuLabel4.Location = new System.Drawing.Point(380, 50);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(75, 15);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 12;
            this.neuLabel4.Text = "自付金额:";
            // 
            // tbOwnCost
            // 
            this.tbOwnCost.Font = new System.Drawing.Font("宋体", 10F);
            this.tbOwnCost.IsEnter2Tab = false;
            this.tbOwnCost.Location = new System.Drawing.Point(274, 46);
            this.tbOwnCost.Name = "tbOwnCost";
            this.tbOwnCost.Size = new System.Drawing.Size(100, 23);
            this.tbOwnCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbOwnCost.TabIndex = 9;
            this.tbOwnCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Font = new System.Drawing.Font("宋体", 11.25F);
            this.neuLabel5.Location = new System.Drawing.Point(193, 50);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(75, 15);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 10;
            this.neuLabel5.Text = "自费金额:";
            // 
            // tbTotCost
            // 
            this.tbTotCost.Font = new System.Drawing.Font("宋体", 10F);
            this.tbTotCost.IsEnter2Tab = false;
            this.tbTotCost.Location = new System.Drawing.Point(87, 46);
            this.tbTotCost.Name = "tbTotCost";
            this.tbTotCost.Size = new System.Drawing.Size(100, 23);
            this.tbTotCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbTotCost.TabIndex = 7;
            this.tbTotCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.Font = new System.Drawing.Font("宋体", 11.25F);
            this.neuLabel6.Location = new System.Drawing.Point(5, 50);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(77, 15);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 8;
            this.neuLabel6.Text = "总    额:";
            // 
            // tbOperName
            // 
            this.tbOperName.Font = new System.Drawing.Font("宋体", 10F);
            this.tbOperName.IsEnter2Tab = false;
            this.tbOperName.Location = new System.Drawing.Point(463, 15);
            this.tbOperName.Name = "tbOperName";
            this.tbOperName.Size = new System.Drawing.Size(155, 23);
            this.tbOperName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbOperName.TabIndex = 5;
            this.tbOperName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Font = new System.Drawing.Font("宋体", 11.25F);
            this.neuLabel3.Location = new System.Drawing.Point(381, 19);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(76, 15);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 6;
            this.neuLabel3.Text = "收 款 员:";
            // 
            // tbPName
            // 
            this.tbPName.Font = new System.Drawing.Font("宋体", 10F);
            this.tbPName.IsEnter2Tab = false;
            this.tbPName.Location = new System.Drawing.Point(275, 15);
            this.tbPName.Name = "tbPName";
            this.tbPName.Size = new System.Drawing.Size(100, 23);
            this.tbPName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbPName.TabIndex = 3;
            this.tbPName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Font = new System.Drawing.Font("宋体", 11.25F);
            this.neuLabel2.Location = new System.Drawing.Point(194, 19);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(75, 15);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 4;
            this.neuLabel2.Text = "患者姓名:";
            // 
            // tbInvoiceNo
            // 
            this.tbInvoiceNo.Font = new System.Drawing.Font("宋体", 10F);
            this.tbInvoiceNo.IsEnter2Tab = false;
            this.tbInvoiceNo.Location = new System.Drawing.Point(88, 15);
            this.tbInvoiceNo.Name = "tbInvoiceNo";
            this.tbInvoiceNo.Size = new System.Drawing.Size(100, 23);
            this.tbInvoiceNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbInvoiceNo.TabIndex = 1;
            this.tbInvoiceNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbInvoiceNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbInvoiceNo_KeyDown);
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Font = new System.Drawing.Font("宋体", 11.25F);
            this.neuLabel1.Location = new System.Drawing.Point(6, 19);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(76, 15);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 2;
            this.neuLabel1.Text = "发 票 号:";
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(5, 512);
            this.panel1.TabIndex = 4;
            // 
            // ucRePrintInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.ucTrvInvoice);
            this.Name = "ucRePrintInvoice";
            this.Size = new System.Drawing.Size(908, 512);
            this.Load += new System.EventHandler(this.ucRePrintInvoice_Load);
            this.pnlRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ucInvoiceTree ucTrvInvoice;
        private System.Windows.Forms.Panel pnlRight;
        public FS.FrameWork.WinForms.Controls.NeuGroupBox groupBox1;
        public FS.FrameWork.WinForms.Controls.NeuTextBox tbInvoiceDate;
        public FS.FrameWork.WinForms.Controls.NeuLabel neuLabel7;
        public FS.FrameWork.WinForms.Controls.NeuTextBox tbPactInfo;
        public FS.FrameWork.WinForms.Controls.NeuLabel neuLabel8;
        public FS.FrameWork.WinForms.Controls.NeuTextBox tbPubCost;
        public FS.FrameWork.WinForms.Controls.NeuLabel neuLabel9;
        public FS.FrameWork.WinForms.Controls.NeuTextBox tbPayCost;
        public FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        public FS.FrameWork.WinForms.Controls.NeuTextBox tbOwnCost;
        public FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        public FS.FrameWork.WinForms.Controls.NeuTextBox tbTotCost;
        public FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        public FS.FrameWork.WinForms.Controls.NeuTextBox tbOperName;
        public FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        public FS.FrameWork.WinForms.Controls.NeuTextBox tbPName;
        public FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        public FS.FrameWork.WinForms.Controls.NeuTextBox tbInvoiceNo;
        public FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        public FS.FrameWork.WinForms.Controls.NeuSpread fpSpread1;
        public FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblNextInvoiceNO;
    }
}
