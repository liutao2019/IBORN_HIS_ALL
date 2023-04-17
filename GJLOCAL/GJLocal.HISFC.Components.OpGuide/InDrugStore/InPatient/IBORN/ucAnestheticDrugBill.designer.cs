namespace GJLocal.HISFC.Components.OpGuide.InDrugStore.InPatient.IBORN
{
    partial class ucAnestheticDrugBill
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
            FarPoint.Win.LineBorder lineBorder1 = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, true, true, false, false);
            FarPoint.Win.LineBorder lineBorder2 = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, true, true, true, false);
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.nlbTitle = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblTitleName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblOrderDate = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbPageNo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbReprint = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbNurseCell = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbRowCount = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbBillNO = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuSpread1 = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.nlbTitle);
            this.neuPanel1.Controls.Add(this.lblTitleName);
            this.neuPanel1.Controls.Add(this.lblOrderDate);
            this.neuPanel1.Controls.Add(this.nlbPageNo);
            this.neuPanel1.Controls.Add(this.nlbReprint);
            this.neuPanel1.Controls.Add(this.nlbNurseCell);
            this.neuPanel1.Controls.Add(this.nlbRowCount);
            this.neuPanel1.Controls.Add(this.nlbBillNO);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(770, 81);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 1;
            // 
            // nlbTitle
            // 
            this.nlbTitle.AutoSize = true;
            this.nlbTitle.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlbTitle.Location = new System.Drawing.Point(264, 7);
            this.nlbTitle.Name = "nlbTitle";
            this.nlbTitle.Size = new System.Drawing.Size(230, 21);
            this.nlbTitle.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbTitle.TabIndex = 238;
            this.nlbTitle.Text = "广州爱博恩妇产科医院";
            // 
            // lblTitleName
            // 
            this.lblTitleName.AutoSize = true;
            this.lblTitleName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitleName.Location = new System.Drawing.Point(294, 34);
            this.lblTitleName.Name = "lblTitleName";
            this.lblTitleName.Size = new System.Drawing.Size(153, 16);
            this.lblTitleName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblTitleName.TabIndex = 239;
            this.lblTitleName.Text = "针剂摆药单(明细）";
            // 
            // lblOrderDate
            // 
            this.lblOrderDate.AutoSize = true;
            this.lblOrderDate.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOrderDate.Location = new System.Drawing.Point(246, 60);
            this.lblOrderDate.Name = "lblOrderDate";
            this.lblOrderDate.Size = new System.Drawing.Size(250, 15);
            this.lblOrderDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblOrderDate.TabIndex = 229;
            this.lblOrderDate.Text = "医嘱时间：2017.09.13-2017.09.14";
            // 
            // nlbPageNo
            // 
            this.nlbPageNo.AutoSize = true;
            this.nlbPageNo.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlbPageNo.Location = new System.Drawing.Point(575, 4);
            this.nlbPageNo.Name = "nlbPageNo";
            this.nlbPageNo.Size = new System.Drawing.Size(76, 15);
            this.nlbPageNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbPageNo.TabIndex = 47;
            this.nlbPageNo.Text = "页码：1/1";
            // 
            // nlbReprint
            // 
            this.nlbReprint.AutoSize = true;
            this.nlbReprint.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlbReprint.Location = new System.Drawing.Point(26, 7);
            this.nlbReprint.Name = "nlbReprint";
            this.nlbReprint.Size = new System.Drawing.Size(57, 15);
            this.nlbReprint.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbReprint.TabIndex = 46;
            this.nlbReprint.Text = "(补打)";
            // 
            // nlbNurseCell
            // 
            this.nlbNurseCell.AutoSize = true;
            this.nlbNurseCell.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlbNurseCell.Location = new System.Drawing.Point(0, 60);
            this.nlbNurseCell.Name = "nlbNurseCell";
            this.nlbNurseCell.Size = new System.Drawing.Size(52, 15);
            this.nlbNurseCell.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbNurseCell.TabIndex = 16;
            this.nlbNurseCell.Text = "病区：";
            // 
            // nlbRowCount
            // 
            this.nlbRowCount.AutoSize = true;
            this.nlbRowCount.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlbRowCount.Location = new System.Drawing.Point(683, 4);
            this.nlbRowCount.Name = "nlbRowCount";
            this.nlbRowCount.Size = new System.Drawing.Size(91, 15);
            this.nlbRowCount.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbRowCount.TabIndex = 15;
            this.nlbRowCount.Text = "记录数：120";
            // 
            // nlbBillNO
            // 
            this.nlbBillNO.AutoSize = true;
            this.nlbBillNO.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlbBillNO.Location = new System.Drawing.Point(613, 60);
            this.nlbBillNO.Name = "nlbBillNO";
            this.nlbBillNO.Size = new System.Drawing.Size(154, 15);
            this.nlbBillNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbBillNO.TabIndex = 12;
            this.nlbBillNO.Text = "摆药单号：123456789";
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, ";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.Font = new System.Drawing.Font("宋体", 9F);
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.Location = new System.Drawing.Point(0, 81);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(770, 1019);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 2;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ColumnCount = 13;
            this.neuSpread1_Sheet1.RowCount = 10;
            this.neuSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin3", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Black, FarPoint.Win.Spread.GridLines.None, System.Drawing.Color.White, System.Drawing.Color.Black, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, true, true, false);
            this.neuSpread1_Sheet1.Cells.Get(0, 0).CellType = textCellType1;
            this.neuSpread1_Sheet1.Cells.Get(0, 0).Value = 1;
            this.neuSpread1_Sheet1.Cells.Get(0, 1).Value = "1";
            this.neuSpread1_Sheet1.Cells.Get(0, 2).Value = "2";
            this.neuSpread1_Sheet1.Cells.Get(0, 3).Value = "刘德华";
            this.neuSpread1_Sheet1.Cells.Get(0, 4).Value = "男";
            this.neuSpread1_Sheet1.Cells.Get(0, 5).Value = "45岁";
            this.neuSpread1_Sheet1.Cells.Get(0, 6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Cells.Get(0, 6).Value = "440999199290902345";
            this.neuSpread1_Sheet1.Cells.Get(0, 7).ParseFormatString = "G";
            this.neuSpread1_Sheet1.Cells.Get(0, 7).Value = "1哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈";
            this.neuSpread1_Sheet1.Cells.Get(1, 0).Value = 4;
            this.neuSpread1_Sheet1.Cells.Get(1, 1).Value = "1";
            this.neuSpread1_Sheet1.Cells.Get(1, 2).Value = "2";
            this.neuSpread1_Sheet1.Cells.Get(1, 3).Value = "刘德华";
            this.neuSpread1_Sheet1.Cells.Get(1, 4).Value = "男";
            this.neuSpread1_Sheet1.Cells.Get(1, 5).Value = "45岁";
            this.neuSpread1_Sheet1.Cells.Get(1, 6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Cells.Get(1, 6).Value = "440999199290902345";
            this.neuSpread1_Sheet1.Cells.Get(2, 0).Value = 3;
            this.neuSpread1_Sheet1.Cells.Get(2, 1).Value = "1";
            this.neuSpread1_Sheet1.Cells.Get(2, 2).Value = "2";
            this.neuSpread1_Sheet1.Cells.Get(2, 3).Value = "刘德华";
            this.neuSpread1_Sheet1.Cells.Get(2, 4).Value = "男";
            this.neuSpread1_Sheet1.Cells.Get(2, 5).Value = "45岁";
            this.neuSpread1_Sheet1.Cells.Get(2, 6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Cells.Get(2, 6).Value = "440999199290902345";
            this.neuSpread1_Sheet1.Cells.Get(3, 7).RowSpan = 2;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Border = lineBorder1;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "序号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Border = lineBorder1;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "床号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Border = lineBorder1;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "住院号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Border = lineBorder1;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "姓名";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Border = lineBorder1;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "性别";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Border = lineBorder1;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "年龄";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Border = lineBorder1;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "身份证号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Border = lineBorder1;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "药品名称";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Border = lineBorder1;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "规格";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Border = lineBorder1;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "数量";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).Border = lineBorder1;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "处方号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 11).Border = lineBorder1;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "诊断";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 12).Border = lineBorder2;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "处方医生";
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.ColumnHeader.Rows.Default.Height = 30F;
            this.neuSpread1_Sheet1.Columns.Get(0).CellType = textCellType2;
            this.neuSpread1_Sheet1.Columns.Get(0).Font = new System.Drawing.Font("宋体", 9.75F);
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "序号";
            this.neuSpread1_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top;
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 23F;
            textCellType3.WordWrap = true;
            this.neuSpread1_Sheet1.Columns.Get(1).CellType = textCellType3;
            this.neuSpread1_Sheet1.Columns.Get(1).Font = new System.Drawing.Font("宋体", 9.75F);
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "床号";
            this.neuSpread1_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 32F;
            this.neuSpread1_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.neuSpread1_Sheet1.Columns.Get(2).Font = new System.Drawing.Font("宋体", 9.75F);
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "住院号";
            this.neuSpread1_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 35F;
            this.neuSpread1_Sheet1.Columns.Get(3).CellType = textCellType3;
            this.neuSpread1_Sheet1.Columns.Get(3).Font = new System.Drawing.Font("宋体", 9.75F);
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "姓名";
            this.neuSpread1_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 62F;
            this.neuSpread1_Sheet1.Columns.Get(4).CellType = textCellType3;
            this.neuSpread1_Sheet1.Columns.Get(4).Font = new System.Drawing.Font("宋体", 9.75F);
            this.neuSpread1_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "性别";
            this.neuSpread1_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 23F;
            this.neuSpread1_Sheet1.Columns.Get(5).CellType = textCellType3;
            this.neuSpread1_Sheet1.Columns.Get(5).Font = new System.Drawing.Font("宋体", 9.75F);
            this.neuSpread1_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "年龄";
            this.neuSpread1_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 45F;
            this.neuSpread1_Sheet1.Columns.Get(6).CellType = textCellType3;
            this.neuSpread1_Sheet1.Columns.Get(6).Font = new System.Drawing.Font("宋体", 9.75F);
            this.neuSpread1_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Columns.Get(6).Label = "身份证号";
            this.neuSpread1_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 53F;
            this.neuSpread1_Sheet1.Columns.Get(7).CellType = textCellType3;
            this.neuSpread1_Sheet1.Columns.Get(7).Font = new System.Drawing.Font("宋体", 9.75F);
            this.neuSpread1_Sheet1.Columns.Get(7).Label = "药品名称";
            this.neuSpread1_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top;
            this.neuSpread1_Sheet1.Columns.Get(7).Width = 136F;
            this.neuSpread1_Sheet1.Columns.Get(8).CellType = textCellType3;
            this.neuSpread1_Sheet1.Columns.Get(8).Font = new System.Drawing.Font("宋体", 9.75F);
            this.neuSpread1_Sheet1.Columns.Get(8).Label = "规格";
            this.neuSpread1_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top;
            this.neuSpread1_Sheet1.Columns.Get(8).Width = 84F;
            this.neuSpread1_Sheet1.Columns.Get(9).CellType = textCellType3;
            this.neuSpread1_Sheet1.Columns.Get(9).Font = new System.Drawing.Font("宋体", 9.75F);
            this.neuSpread1_Sheet1.Columns.Get(9).Label = "数量";
            this.neuSpread1_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top;
            this.neuSpread1_Sheet1.Columns.Get(9).Width = 50F;
            textCellType4.WordWrap = true;
            this.neuSpread1_Sheet1.Columns.Get(10).CellType = textCellType4;
            this.neuSpread1_Sheet1.Columns.Get(10).Font = new System.Drawing.Font("宋体", 9.75F);
            this.neuSpread1_Sheet1.Columns.Get(10).Label = "处方号";
            this.neuSpread1_Sheet1.Columns.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top;
            this.neuSpread1_Sheet1.Columns.Get(10).Width = 49F;
            this.neuSpread1_Sheet1.Columns.Get(11).CellType = textCellType4;
            this.neuSpread1_Sheet1.Columns.Get(11).Font = new System.Drawing.Font("宋体", 9.75F);
            this.neuSpread1_Sheet1.Columns.Get(11).Label = "诊断";
            this.neuSpread1_Sheet1.Columns.Get(11).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top;
            this.neuSpread1_Sheet1.Columns.Get(11).Width = 100F;
            this.neuSpread1_Sheet1.Columns.Get(12).CellType = textCellType4;
            this.neuSpread1_Sheet1.Columns.Get(12).Font = new System.Drawing.Font("宋体", 9.75F);
            this.neuSpread1_Sheet1.Columns.Get(12).Label = "处方医生";
            this.neuSpread1_Sheet1.Columns.Get(12).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top;
            this.neuSpread1_Sheet1.Columns.Get(12).Width = 55F;
            this.neuSpread1_Sheet1.DefaultStyle.Font = new System.Drawing.Font("宋体", 9.75F);
            this.neuSpread1_Sheet1.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top;
            this.neuSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.RowHeader.Visible = false;
            this.neuSpread1_Sheet1.Rows.Default.Height = 45F;
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.SheetCornerStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.SheetCornerStyle.Locked = false;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ucAnestheticByPatientDrugBillIBORN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.neuSpread1);
            this.Controls.Add(this.neuPanel1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "ucAnestheticByPatientDrugBillIBORN";
            this.Size = new System.Drawing.Size(770, 1100);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        protected FS.SOC.Windows.Forms.FpSpread neuSpread1;
        protected FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbRowCount;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbBillNO;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbNurseCell;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbReprint;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbPageNo;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblOrderDate;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbTitle;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblTitleName;

    }
}
