namespace FS.SOC.Local.InpatientFee.ZhuHai.IBackFeeApplyPrint
{
    partial class ucQuitDrugBill
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
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("zh-CN", false);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucQuitDrugBill));
            FarPoint.Win.LineBorder lineBorder5 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame);
            FarPoint.Win.LineBorder lineBorder6 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, false, true, true, true);
            FarPoint.Win.LineBorder lineBorder7 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, true, false, true, true);
            FarPoint.Win.LineBorder lineBorder8 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, false, false, true, true);
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.nlbPageNO = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbTotCost = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbTotCount = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbTotLabel = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.labRemark = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblBedNO = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbRePrint = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.labPrintDate = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.labReason = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.labArea = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbCardNo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbSex = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(751, 550);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 115);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(751, 435);
            this.panel3.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.neuSpread1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(751, 387);
            this.panel4.TabIndex = 4;
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, ";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(0, 0);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(751, 387);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 1;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance2;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ColumnCount = 9;
            this.neuSpread1_Sheet1.RowCount = 1;
            this.neuSpread1_Sheet1.RowHeader.ColumnCount = 0;
            this.neuSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Black, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.neuSpread1_Sheet1.Cells.Get(0, 0).ParseFormatInfo = ((System.Globalization.DateTimeFormatInfo)(cultureInfo.DateTimeFormat.Clone()));
            ((System.Globalization.DateTimeFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(0, 0).ParseFormatInfo)).DateSeparator = "-";
            ((System.Globalization.DateTimeFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(0, 0).ParseFormatInfo)).FullDateTimePattern = "yyyy\'年\'M\'月\'d\'日\' HH:mm:ss";
            ((System.Globalization.DateTimeFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(0, 0).ParseFormatInfo)).LongTimePattern = "HH:mm:ss";
            ((System.Globalization.DateTimeFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(0, 0).ParseFormatInfo)).ShortDatePattern = "yyyy-MM-dd";
            this.neuSpread1_Sheet1.Cells.Get(0, 0).ParseFormatString = "yyyy-MM-dd";
            this.neuSpread1_Sheet1.Cells.Get(0, 0).Value = new System.DateTime(2014, 10, 30, 0, 0, 0, 0);
            this.neuSpread1_Sheet1.Cells.Get(0, 1).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(0, 1).ParseFormatInfo)).CurrencySymbol = "¥";
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(0, 1).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(0, 1).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.neuSpread1_Sheet1.Cells.Get(0, 1).ParseFormatString = "n";
            this.neuSpread1_Sheet1.Cells.Get(0, 1).Value = 11802007;
            this.neuSpread1_Sheet1.Cells.Get(0, 3).Value = "500ml/袋";
            this.neuSpread1_Sheet1.Cells.Get(0, 6).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(0, 6).ParseFormatInfo)).CurrencySymbol = "¥";
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(0, 6).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.neuSpread1_Sheet1.Cells.Get(0, 6).ParseFormatString = "N";
            this.neuSpread1_Sheet1.Cells.Get(0, 6).Value = 8800.62;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Border = lineBorder5;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "申请日期";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Border = lineBorder6;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "项目代码";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Border = lineBorder6;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "项目名称";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Border = lineBorder6;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "规格";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Border = lineBorder6;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "数量";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Border = lineBorder6;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "单位";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Border = lineBorder6;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "金额";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Border = lineBorder6;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "药房";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Border = lineBorder6;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "来源";
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.Columns.Get(0).Border = lineBorder7;
            this.neuSpread1_Sheet1.Columns.Get(0).Font = new System.Drawing.Font("宋体", 9F);
            this.neuSpread1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "申请日期";
            this.neuSpread1_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 71F;
            this.neuSpread1_Sheet1.Columns.Get(1).Border = lineBorder8;
            this.neuSpread1_Sheet1.Columns.Get(1).Font = new System.Drawing.Font("宋体", 9F);
            this.neuSpread1_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "项目代码";
            this.neuSpread1_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 59F;
            this.neuSpread1_Sheet1.Columns.Get(2).BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.Columns.Get(2).Border = lineBorder8;
            this.neuSpread1_Sheet1.Columns.Get(2).Font = new System.Drawing.Font("宋体", 9F);
            this.neuSpread1_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "项目名称";
            this.neuSpread1_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 300F;
            this.neuSpread1_Sheet1.Columns.Get(3).Border = lineBorder8;
            this.neuSpread1_Sheet1.Columns.Get(3).Font = new System.Drawing.Font("宋体", 9F);
            this.neuSpread1_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "规格";
            this.neuSpread1_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 150F;
            this.neuSpread1_Sheet1.Columns.Get(4).Border = lineBorder8;
            this.neuSpread1_Sheet1.Columns.Get(4).Font = new System.Drawing.Font("宋体", 9F);
            this.neuSpread1_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "数量";
            this.neuSpread1_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 36F;
            this.neuSpread1_Sheet1.Columns.Get(5).Border = lineBorder8;
            this.neuSpread1_Sheet1.Columns.Get(5).Font = new System.Drawing.Font("宋体", 9F);
            this.neuSpread1_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "单位";
            this.neuSpread1_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 36F;
            this.neuSpread1_Sheet1.Columns.Get(6).Border = lineBorder8;
            this.neuSpread1_Sheet1.Columns.Get(6).Font = new System.Drawing.Font("宋体", 9F);
            this.neuSpread1_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Columns.Get(6).Label = "金额";
            this.neuSpread1_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 68F;
            this.neuSpread1_Sheet1.Columns.Get(7).Border = lineBorder8;
            this.neuSpread1_Sheet1.Columns.Get(7).Font = new System.Drawing.Font("宋体", 9F);
            this.neuSpread1_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(7).Label = "药房";
            this.neuSpread1_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(7).Width = 67F;
            this.neuSpread1_Sheet1.Columns.Get(8).Font = new System.Drawing.Font("宋体", 9F);
            this.neuSpread1_Sheet1.Columns.Get(8).Label = "来源";
            this.neuSpread1_Sheet1.Columns.Get(8).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(8).Width = 108F;
            this.neuSpread1_Sheet1.GroupBarBackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.Rows.Default.Height = 30F;
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.neuSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.nlbPageNO);
            this.panel5.Controls.Add(this.nlbTotCost);
            this.panel5.Controls.Add(this.nlbTotCount);
            this.panel5.Controls.Add(this.nlbTotLabel);
            this.panel5.Controls.Add(this.labRemark);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 387);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(751, 48);
            this.panel5.TabIndex = 3;
            // 
            // nlbPageNO
            // 
            this.nlbPageNO.AutoSize = true;
            this.nlbPageNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlbPageNO.Location = new System.Drawing.Point(7, 3);
            this.nlbPageNO.Name = "nlbPageNO";
            this.nlbPageNO.Size = new System.Drawing.Size(89, 12);
            this.nlbPageNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbPageNO.TabIndex = 27;
            this.nlbPageNO.Text = "第一页，共一页";
            // 
            // nlbTotCost
            // 
            this.nlbTotCost.AutoSize = true;
            this.nlbTotCost.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlbTotCost.Location = new System.Drawing.Point(379, 3);
            this.nlbTotCost.Name = "nlbTotCost";
            this.nlbTotCost.Size = new System.Drawing.Size(41, 12);
            this.nlbTotCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbTotCost.TabIndex = 26;
            this.nlbTotCost.Text = "-99.67";
            // 
            // nlbTotCount
            // 
            this.nlbTotCount.AutoSize = true;
            this.nlbTotCount.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlbTotCount.Location = new System.Drawing.Point(279, 3);
            this.nlbTotCount.Name = "nlbTotCount";
            this.nlbTotCount.Size = new System.Drawing.Size(17, 12);
            this.nlbTotCount.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbTotCount.TabIndex = 25;
            this.nlbTotCount.Text = "-8";
            this.nlbTotCount.Visible = false;
            // 
            // nlbTotLabel
            // 
            this.nlbTotLabel.AutoSize = true;
            this.nlbTotLabel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlbTotLabel.Location = new System.Drawing.Point(332, 3);
            this.nlbTotLabel.Name = "nlbTotLabel";
            this.nlbTotLabel.Size = new System.Drawing.Size(41, 12);
            this.nlbTotLabel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbTotLabel.TabIndex = 24;
            this.nlbTotLabel.Text = "合计：";
            // 
            // labRemark
            // 
            this.labRemark.AutoSize = true;
            this.labRemark.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labRemark.Location = new System.Drawing.Point(7, 29);
            this.labRemark.Name = "labRemark";
            this.labRemark.Size = new System.Drawing.Size(485, 12);
            this.labRemark.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.labRemark.TabIndex = 19;
            this.labRemark.Text = "注：护长签名并盖章，备注为送药房的项目须药房签名盖章，检验、检查项目须拿回原验单";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.lblBedNO);
            this.panel2.Controls.Add(this.neuLabel2);
            this.panel2.Controls.Add(this.neuLabel1);
            this.panel2.Controls.Add(this.lbRePrint);
            this.panel2.Controls.Add(this.labPrintDate);
            this.panel2.Controls.Add(this.labReason);
            this.panel2.Controls.Add(this.labArea);
            this.panel2.Controls.Add(this.lbCardNo);
            this.panel2.Controls.Add(this.lbSex);
            this.panel2.Controls.Add(this.lbName);
            this.panel2.Controls.Add(this.neuPanName);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(751, 115);
            this.panel2.TabIndex = 0;
            // 
            // lblBedNO
            // 
            this.lblBedNO.AutoSize = true;
            this.lblBedNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBedNO.Location = new System.Drawing.Point(3, 90);
            this.lblBedNO.Name = "lblBedNO";
            this.lblBedNO.Size = new System.Drawing.Size(53, 12);
            this.lblBedNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblBedNO.TabIndex = 25;
            this.lblBedNO.Text = "床号 :43";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel2.Location = new System.Drawing.Point(506, 67);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(71, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 24;
            this.neuLabel2.Text = "护士长签名:";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel1.Location = new System.Drawing.Point(225, 67);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(71, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 23;
            this.neuLabel1.Text = "科主任签名:";
            // 
            // lbRePrint
            // 
            this.lbRePrint.AutoSize = true;
            this.lbRePrint.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold);
            this.lbRePrint.Location = new System.Drawing.Point(5, 12);
            this.lbRePrint.Name = "lbRePrint";
            this.lbRePrint.Size = new System.Drawing.Size(29, 19);
            this.lbRePrint.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbRePrint.TabIndex = 22;
            this.lbRePrint.Text = "补";
            // 
            // labPrintDate
            // 
            this.labPrintDate.AutoSize = true;
            this.labPrintDate.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labPrintDate.Location = new System.Drawing.Point(506, 40);
            this.labPrintDate.Name = "labPrintDate";
            this.labPrintDate.Size = new System.Drawing.Size(125, 12);
            this.labPrintDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.labPrintDate.TabIndex = 21;
            this.labPrintDate.Text = "打印日期 :2014-04-05";
            // 
            // labReason
            // 
            this.labReason.AutoSize = true;
            this.labReason.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labReason.Location = new System.Drawing.Point(224, 90);
            this.labReason.Name = "labReason";
            this.labReason.Size = new System.Drawing.Size(65, 12);
            this.labReason.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.labReason.TabIndex = 20;
            this.labReason.Text = "退费原因 :";
            // 
            // labArea
            // 
            this.labArea.AutoSize = true;
            this.labArea.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labArea.Location = new System.Drawing.Point(3, 67);
            this.labArea.Name = "labArea";
            this.labArea.Size = new System.Drawing.Size(125, 12);
            this.labArea.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.labArea.TabIndex = 18;
            this.labArea.Text = "病区 :中西医一科病区";
            // 
            // lbCardNo
            // 
            this.lbCardNo.AutoSize = true;
            this.lbCardNo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbCardNo.Location = new System.Drawing.Point(3, 40);
            this.lbCardNo.Name = "lbCardNo";
            this.lbCardNo.Size = new System.Drawing.Size(113, 12);
            this.lbCardNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbCardNo.TabIndex = 17;
            this.lbCardNo.Text = "住院号 :0000002001";
            // 
            // lbSex
            // 
            this.lbSex.AutoSize = true;
            this.lbSex.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbSex.Location = new System.Drawing.Point(379, 40);
            this.lbSex.Name = "lbSex";
            this.lbSex.Size = new System.Drawing.Size(53, 12);
            this.lbSex.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbSex.TabIndex = 12;
            this.lbSex.Text = "性别 :男";
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbName.Location = new System.Drawing.Point(225, 40);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(83, 12);
            this.lbName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbName.TabIndex = 11;
            this.lbName.Text = "姓名:刘德华华";
            // 
            // neuPanName
            // 
            this.neuPanName.AutoSize = true;
            this.neuPanName.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuPanName.Location = new System.Drawing.Point(260, 7);
            this.neuPanName.Name = "neuPanName";
            this.neuPanName.Size = new System.Drawing.Size(185, 24);
            this.neuPanName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanName.TabIndex = 1;
            this.neuPanName.Text = "住院退费申请单";
            // 
            // ucQuitDrugBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Name = "ucQuitDrugBill";
            this.Size = new System.Drawing.Size(751, 550);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbCardNo;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbSex;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbName;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuPanName;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private System.Windows.Forms.Panel panel5;
        private FS.FrameWork.WinForms.Controls.NeuLabel labReason;
        private FS.FrameWork.WinForms.Controls.NeuLabel labRemark;
        private FS.FrameWork.WinForms.Controls.NeuLabel labArea;
        private FS.FrameWork.WinForms.Controls.NeuLabel labPrintDate;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbRePrint;
        private System.Windows.Forms.Panel panel4;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbTotCost;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbTotCount;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbTotLabel;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbPageNO;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblBedNO;
    }
}
