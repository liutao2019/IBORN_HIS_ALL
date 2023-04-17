namespace ProvinceAcrossSI.Controls
{
    partial class ucLiqudDetail
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
            FarPoint.Win.LineBorder lineBorder2 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, true, false, false, true);
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.PanAll = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel11 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel38 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel10 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel36 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblSerialNo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblHosCode = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblHosDegree = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblHos = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel18 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel26 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel25 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.PanAll.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.neuPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanAll
            // 
            this.PanAll.Controls.Add(this.neuPanel2);
            this.PanAll.Controls.Add(this.neuPanel1);
            this.PanAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanAll.Location = new System.Drawing.Point(0, 0);
            this.PanAll.Name = "PanAll";
            this.PanAll.Size = new System.Drawing.Size(1477, 237);
            this.PanAll.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.PanAll.TabIndex = 0;
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.neuSpread1);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel2.Location = new System.Drawing.Point(0, 184);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(1477, 49);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 363;
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, 1";
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
            this.neuSpread1.Size = new System.Drawing.Size(1477, 49);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 361;
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
            this.neuSpread1_Sheet1.ColumnCount = 17;
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.SystemColors.AppWorkspace, System.Drawing.Color.White, System.Drawing.Color.Black, System.Drawing.Color.Black, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Black, System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(153))))), System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, true, true, false);
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "序号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "姓名";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "参保所属市";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "社会保障卡/身份证号码";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "住院号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "入院日期";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "出院日期";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "住院天数";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "结算日期";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "入院诊断";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "出院诊断";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "医疗总费用";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "个人自费金额";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "记账金额";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 14).Value = "大病保险";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 15).Value = "就诊类别";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 16).Value = "备注";
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Border = lineBorder2;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.ColumnHeader.Rows.Get(0).Height = 45F;
            this.neuSpread1_Sheet1.Columns.Get(0).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "序号";
            this.neuSpread1_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 24F;
            this.neuSpread1_Sheet1.Columns.Get(1).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "姓名";
            this.neuSpread1_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 62F;
            this.neuSpread1_Sheet1.Columns.Get(2).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "参保所属市";
            this.neuSpread1_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 78F;
            textCellType2.WordWrap = true;
            this.neuSpread1_Sheet1.Columns.Get(3).CellType = textCellType2;
            this.neuSpread1_Sheet1.Columns.Get(3).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "社会保障卡/身份证号码";
            this.neuSpread1_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 108F;
            this.neuSpread1_Sheet1.Columns.Get(4).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "住院号";
            this.neuSpread1_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 94F;
            this.neuSpread1_Sheet1.Columns.Get(5).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "入院日期";
            this.neuSpread1_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 91F;
            this.neuSpread1_Sheet1.Columns.Get(6).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Columns.Get(6).Label = "出院日期";
            this.neuSpread1_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 91F;
            this.neuSpread1_Sheet1.Columns.Get(7).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Columns.Get(7).Label = "住院天数";
            this.neuSpread1_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(7).Width = 42F;
            this.neuSpread1_Sheet1.Columns.Get(8).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Columns.Get(8).Label = "结算日期";
            this.neuSpread1_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(8).Width = 91F;
            this.neuSpread1_Sheet1.Columns.Get(9).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(9).Label = "入院诊断";
            this.neuSpread1_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(9).Width = 103F;
            this.neuSpread1_Sheet1.Columns.Get(10).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(10).Label = "出院诊断";
            this.neuSpread1_Sheet1.Columns.Get(10).Width = 100F;
            this.neuSpread1_Sheet1.Columns.Get(11).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(11).Label = "医疗总费用";
            this.neuSpread1_Sheet1.Columns.Get(11).Width = 100F;
            this.neuSpread1_Sheet1.Columns.Get(12).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(12).Label = "个人自费金额";
            this.neuSpread1_Sheet1.Columns.Get(12).Width = 100F;
            this.neuSpread1_Sheet1.Columns.Get(13).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(13).Label = "记账金额";
            this.neuSpread1_Sheet1.Columns.Get(13).Width = 100F;
            this.neuSpread1_Sheet1.Columns.Get(14).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(14).Label = "大病保险";
            this.neuSpread1_Sheet1.Columns.Get(14).Width = 100F;
            this.neuSpread1_Sheet1.Columns.Get(15).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(15).Label = "就诊类别";
            this.neuSpread1_Sheet1.Columns.Get(15).Width = 98F;
            this.neuSpread1_Sheet1.Columns.Get(16).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(16).Label = "备注";
            this.neuSpread1_Sheet1.Columns.Get(16).Width = 84F;
            this.neuSpread1_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.RowHeader.Visible = false;
            this.neuSpread1_Sheet1.Rows.Default.Height = 35F;
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.SheetCornerStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.neuSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuSpread1.SetActiveViewport(0, 1, 0);
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.neuLabel1);
            this.neuPanel1.Controls.Add(this.neuLabel11);
            this.neuPanel1.Controls.Add(this.neuLabel2);
            this.neuPanel1.Controls.Add(this.neuLabel38);
            this.neuPanel1.Controls.Add(this.neuLabel5);
            this.neuPanel1.Controls.Add(this.neuLabel10);
            this.neuPanel1.Controls.Add(this.neuLabel36);
            this.neuPanel1.Controls.Add(this.neuLabel3);
            this.neuPanel1.Controls.Add(this.lblSerialNo);
            this.neuPanel1.Controls.Add(this.lblHosCode);
            this.neuPanel1.Controls.Add(this.lblHosDegree);
            this.neuPanel1.Controls.Add(this.lblHos);
            this.neuPanel1.Controls.Add(this.neuLabel18);
            this.neuPanel1.Controls.Add(this.neuLabel26);
            this.neuPanel1.Controls.Add(this.neuLabel25);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(1477, 184);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 362;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel1.Location = new System.Drawing.Point(407, 50);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(560, 24);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 350;
            this.neuLabel1.Text = "广东省医疗保险异地就医医疗费用结算申报汇总表";
            // 
            // neuLabel11
            // 
            this.neuLabel11.AutoSize = true;
            this.neuLabel11.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel11.Location = new System.Drawing.Point(863, 161);
            this.neuLabel11.Name = "neuLabel11";
            this.neuLabel11.Size = new System.Drawing.Size(104, 16);
            this.neuLabel11.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel11.TabIndex = 352;
            this.neuLabel11.Text = "业务交接号：";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel2.Location = new System.Drawing.Point(880, 22);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(120, 16);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 352;
            this.neuLabel2.Text = "医疗机构编码：";
            // 
            // neuLabel38
            // 
            this.neuLabel38.AutoSize = true;
            this.neuLabel38.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel38.Location = new System.Drawing.Point(1371, 161);
            this.neuLabel38.Name = "neuLabel38";
            this.neuLabel38.Size = new System.Drawing.Size(72, 16);
            this.neuLabel38.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel38.TabIndex = 359;
            this.neuLabel38.Text = "单位：元";
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel5.Location = new System.Drawing.Point(600, 84);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(129, 19);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 360;
            this.neuLabel5.Text = "医疗结构用表";
            // 
            // neuLabel10
            // 
            this.neuLabel10.AutoSize = true;
            this.neuLabel10.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel10.Location = new System.Drawing.Point(973, 161);
            this.neuLabel10.Name = "neuLabel10";
            this.neuLabel10.Size = new System.Drawing.Size(136, 16);
            this.neuLabel10.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel10.TabIndex = 355;
            this.neuLabel10.Tag = "NULL";
            this.neuLabel10.Text = "B440600000012324";
            // 
            // neuLabel36
            // 
            this.neuLabel36.AutoSize = true;
            this.neuLabel36.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel36.Location = new System.Drawing.Point(713, 113);
            this.neuLabel36.Name = "neuLabel36";
            this.neuLabel36.Size = new System.Drawing.Size(28, 19);
            this.neuLabel36.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel36.TabIndex = 353;
            this.neuLabel36.Text = "至";
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel3.Location = new System.Drawing.Point(1004, 22);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(56, 16);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 355;
            this.neuLabel3.Tag = "NULL";
            this.neuLabel3.Text = "佛山市";
            // 
            // lblSerialNo
            // 
            this.lblSerialNo.AutoSize = true;
            this.lblSerialNo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSerialNo.Location = new System.Drawing.Point(620, 161);
            this.lblSerialNo.Name = "lblSerialNo";
            this.lblSerialNo.Size = new System.Drawing.Size(56, 16);
            this.lblSerialNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblSerialNo.TabIndex = 355;
            this.lblSerialNo.Tag = "NULL";
            this.lblSerialNo.Text = "佛山市";
            // 
            // lblHosCode
            // 
            this.lblHosCode.AutoSize = true;
            this.lblHosCode.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblHosCode.Location = new System.Drawing.Point(598, 113);
            this.lblHosCode.Name = "lblHosCode";
            this.lblHosCode.Size = new System.Drawing.Size(109, 19);
            this.lblHosCode.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblHosCode.TabIndex = 358;
            this.lblHosCode.Tag = "NULL";
            this.lblHosCode.Text = "2017-12-12";
            // 
            // lblHosDegree
            // 
            this.lblHosDegree.AutoSize = true;
            this.lblHosDegree.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblHosDegree.Location = new System.Drawing.Point(759, 113);
            this.lblHosDegree.Name = "lblHosDegree";
            this.lblHosDegree.Size = new System.Drawing.Size(109, 19);
            this.lblHosDegree.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblHosDegree.TabIndex = 356;
            this.lblHosDegree.Tag = "NULL";
            this.lblHosDegree.Text = "2017-12-31";
            // 
            // lblHos
            // 
            this.lblHos.AutoSize = true;
            this.lblHos.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblHos.Location = new System.Drawing.Point(338, 161);
            this.lblHos.Name = "lblHos";
            this.lblHos.Size = new System.Drawing.Size(168, 16);
            this.lblHos.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblHos.TabIndex = 354;
            this.lblHos.Tag = "NULL";
            this.lblHos.Text = "佛山市顺德区北滘医院";
            // 
            // neuLabel18
            // 
            this.neuLabel18.AutoSize = true;
            this.neuLabel18.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel18.Location = new System.Drawing.Point(467, 113);
            this.neuLabel18.Name = "neuLabel18";
            this.neuLabel18.Size = new System.Drawing.Size(142, 19);
            this.neuLabel18.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel18.TabIndex = 357;
            this.neuLabel18.Text = "申报结算日期：";
            // 
            // neuLabel26
            // 
            this.neuLabel26.AutoSize = true;
            this.neuLabel26.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel26.Location = new System.Drawing.Point(20, 161);
            this.neuLabel26.Name = "neuLabel26";
            this.neuLabel26.Size = new System.Drawing.Size(328, 16);
            this.neuLabel26.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel26.TabIndex = 351;
            this.neuLabel26.Text = "填报单位：省异地定点医疗机构名称（章）：";
            // 
            // neuLabel25
            // 
            this.neuLabel25.AutoSize = true;
            this.neuLabel25.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel25.Location = new System.Drawing.Point(527, 161);
            this.neuLabel25.Name = "neuLabel25";
            this.neuLabel25.Size = new System.Drawing.Size(104, 16);
            this.neuLabel25.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel25.TabIndex = 352;
            this.neuLabel25.Text = "就医所属市：";
            // 
            // ucLiqudDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PanAll);
            this.Name = "ucLiqudDetail";
            this.Size = new System.Drawing.Size(1477, 237);
            this.PanAll.ResumeLayout(false);
            this.neuPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel PanAll;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel38;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblHosCode;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel18;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblHosDegree;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblSerialNo;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblHos;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel36;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel25;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel26;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel11;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel10;
    }
}
