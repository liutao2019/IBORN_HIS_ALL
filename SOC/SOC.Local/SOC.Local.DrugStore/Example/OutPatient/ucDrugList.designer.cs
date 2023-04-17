namespace FS.SOC.Local.DrugStore.Example.Outpatient
{
    partial class ucDrugList
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
            FarPoint.Win.Spread.TipAppearance tipAppearance4 = new FarPoint.Win.Spread.TipAppearance();
            System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("zh-CN", false);
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType4 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.npbBarCode = new System.Windows.Forms.PictureBox();
            this.lbAge = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbCardNo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbReprint = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbInvoice = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbRecipe = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbDeptName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbDiagnose = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbSex = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbRecipeDate = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbDoctor = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSpread1 = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            ((System.ComponentModel.ISupportInitialize)(this.npbBarCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // npbBarCode
            // 
            this.npbBarCode.Location = new System.Drawing.Point(0, 0);
            this.npbBarCode.Name = "npbBarCode";
            this.npbBarCode.Size = new System.Drawing.Size(205, 43);
            this.npbBarCode.TabIndex = 23;
            this.npbBarCode.TabStop = false;
            // 
            // lbAge
            // 
            this.lbAge.AutoSize = true;
            this.lbAge.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbAge.Location = new System.Drawing.Point(447, 46);
            this.lbAge.Name = "lbAge";
            this.lbAge.Size = new System.Drawing.Size(53, 14);
            this.lbAge.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbAge.TabIndex = 22;
            this.lbAge.Text = "年龄 :";
            // 
            // lbCardNo
            // 
            this.lbCardNo.AutoSize = true;
            this.lbCardNo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbCardNo.Location = new System.Drawing.Point(151, 46);
            this.lbCardNo.Name = "lbCardNo";
            this.lbCardNo.Size = new System.Drawing.Size(83, 14);
            this.lbCardNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbCardNo.TabIndex = 21;
            this.lbCardNo.Text = "病历卡号 :";
            // 
            // lbReprint
            // 
            this.lbReprint.AutoSize = true;
            this.lbReprint.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbReprint.Location = new System.Drawing.Point(649, 10);
            this.lbReprint.Name = "lbReprint";
            this.lbReprint.Size = new System.Drawing.Size(60, 19);
            this.lbReprint.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbReprint.TabIndex = 20;
            this.lbReprint.Text = "补 打";
            // 
            // lbInvoice
            // 
            this.lbInvoice.AutoSize = true;
            this.lbInvoice.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbInvoice.Location = new System.Drawing.Point(596, 68);
            this.lbInvoice.Name = "lbInvoice";
            this.lbInvoice.Size = new System.Drawing.Size(76, 14);
            this.lbInvoice.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbInvoice.TabIndex = 19;
            this.lbInvoice.Text = "发票号 : ";
            // 
            // lbRecipe
            // 
            this.lbRecipe.AutoSize = true;
            this.lbRecipe.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbRecipe.Location = new System.Drawing.Point(431, 68);
            this.lbRecipe.Name = "lbRecipe";
            this.lbRecipe.Size = new System.Drawing.Size(76, 14);
            this.lbRecipe.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbRecipe.TabIndex = 18;
            this.lbRecipe.Text = "处方号 : ";
            // 
            // lbDeptName
            // 
            this.lbDeptName.AutoSize = true;
            this.lbDeptName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbDeptName.Location = new System.Drawing.Point(596, 46);
            this.lbDeptName.Name = "lbDeptName";
            this.lbDeptName.Size = new System.Drawing.Size(83, 14);
            this.lbDeptName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbDeptName.TabIndex = 17;
            this.lbDeptName.Text = "科室名称 :";
            // 
            // lbDiagnose
            // 
            this.lbDiagnose.AutoSize = true;
            this.lbDiagnose.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbDiagnose.Location = new System.Drawing.Point(3, 68);
            this.lbDiagnose.Name = "lbDiagnose";
            this.lbDiagnose.Size = new System.Drawing.Size(53, 14);
            this.lbDiagnose.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbDiagnose.TabIndex = 16;
            this.lbDiagnose.Text = "诊断 :";
            // 
            // lbSex
            // 
            this.lbSex.AutoSize = true;
            this.lbSex.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbSex.Location = new System.Drawing.Point(341, 46);
            this.lbSex.Name = "lbSex";
            this.lbSex.Size = new System.Drawing.Size(53, 14);
            this.lbSex.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbSex.TabIndex = 15;
            this.lbSex.Text = "性别 :";
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbName.Location = new System.Drawing.Point(3, 46);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(53, 14);
            this.lbName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbName.TabIndex = 14;
            this.lbName.Text = "姓名 :";
            // 
            // neuPanName
            // 
            this.neuPanName.AutoSize = true;
            this.neuPanName.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuPanName.Location = new System.Drawing.Point(254, 13);
            this.neuPanName.Name = "neuPanName";
            this.neuPanName.Size = new System.Drawing.Size(214, 21);
            this.neuPanName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanName.TabIndex = 13;
            this.neuPanName.Text = " 医 院 药 品 清 单";
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(391, 4);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(77, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 7;
            this.neuLabel3.Text = "输液执行人 :";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(242, 4);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(53, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 8;
            this.neuLabel2.Text = "核对人 :";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(110, 4);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(53, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 6;
            this.neuLabel1.Text = "配药人 :";
            // 
            // lbRecipeDate
            // 
            this.lbRecipeDate.AutoSize = true;
            this.lbRecipeDate.Location = new System.Drawing.Point(558, 4);
            this.lbRecipeDate.Name = "lbRecipeDate";
            this.lbRecipeDate.Size = new System.Drawing.Size(59, 12);
            this.lbRecipeDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbRecipeDate.TabIndex = 5;
            this.lbRecipeDate.Text = "处方日期:";
            // 
            // lbDoctor
            // 
            this.lbDoctor.AutoSize = true;
            this.lbDoctor.Location = new System.Drawing.Point(15, 4);
            this.lbDoctor.Name = "lbDoctor";
            this.lbDoctor.Size = new System.Drawing.Size(41, 12);
            this.lbDoctor.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbDoctor.TabIndex = 4;
            this.lbDoctor.Text = "医师 :";
            // 
            // neuPanel1
            // 
            this.neuPanel1.BackColor = System.Drawing.Color.Black;
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(851, 1);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 9;
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, 1";
            this.neuSpread1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.neuSpread1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            this.neuSpread1.Location = new System.Drawing.Point(0, 0);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(861, 238);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 2;
            tipAppearance4.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance4.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance4;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ColumnCount = 13;
            this.neuSpread1_Sheet1.RowCount = 8;
            this.neuSpread1_Sheet1.RowHeader.ColumnCount = 0;
            this.neuSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.Black, System.Drawing.Color.White, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Black, System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(93)))), ((int)(((byte)(90))))), System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.White, true, true, true, true, true);
            this.neuSpread1_Sheet1.Cells.Get(0, 0).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(0, 0).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(0, 0).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.neuSpread1_Sheet1.Cells.Get(0, 0).ParseFormatString = "n";
            this.neuSpread1_Sheet1.Cells.Get(0, 0).Value = 1;
            this.neuSpread1_Sheet1.Cells.Get(0, 1).Value = "急支糖浆";
            this.neuSpread1_Sheet1.Cells.Get(0, 3).Value = "20mg*10粒/盒";
            this.neuSpread1_Sheet1.Cells.Get(0, 4).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(0, 4).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(0, 4).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.neuSpread1_Sheet1.Cells.Get(0, 4).ParseFormatString = "n";
            this.neuSpread1_Sheet1.Cells.Get(0, 4).Value = 2;
            this.neuSpread1_Sheet1.Cells.Get(0, 5).Value = "盒";
            this.neuSpread1_Sheet1.Cells.Get(0, 6).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(0, 6).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(0, 6).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.neuSpread1_Sheet1.Cells.Get(0, 6).ParseFormatString = "n";
            this.neuSpread1_Sheet1.Cells.Get(0, 6).Value = 10;
            this.neuSpread1_Sheet1.Cells.Get(0, 7).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(0, 7).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(0, 7).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.neuSpread1_Sheet1.Cells.Get(0, 7).ParseFormatString = "n";
            this.neuSpread1_Sheet1.Cells.Get(0, 7).Value = 20;
            this.neuSpread1_Sheet1.Cells.Get(0, 8).Value = "P.O";
            this.neuSpread1_Sheet1.Cells.Get(0, 9).Value = "10mg";
            this.neuSpread1_Sheet1.Cells.Get(0, 10).Value = "每天三次";
            this.neuSpread1_Sheet1.Cells.Get(0, 11).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(0, 11).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(0, 11).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.neuSpread1_Sheet1.Cells.Get(0, 11).ParseFormatString = "n";
            this.neuSpread1_Sheet1.Cells.Get(0, 11).Value = 7;
            this.neuSpread1_Sheet1.Cells.Get(0, 12).Value = "遵医嘱";
            this.neuSpread1_Sheet1.Cells.Get(1, 0).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(1, 0).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(1, 0).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.neuSpread1_Sheet1.Cells.Get(1, 0).ParseFormatString = "n";
            this.neuSpread1_Sheet1.Cells.Get(1, 0).Value = 2;
            this.neuSpread1_Sheet1.Cells.Get(2, 0).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(2, 0).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(2, 0).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.neuSpread1_Sheet1.Cells.Get(2, 0).ParseFormatString = "n";
            this.neuSpread1_Sheet1.Cells.Get(2, 0).Value = 3;
            this.neuSpread1_Sheet1.Cells.Get(3, 0).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(3, 0).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(3, 0).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.neuSpread1_Sheet1.Cells.Get(3, 0).ParseFormatString = "n";
            this.neuSpread1_Sheet1.Cells.Get(3, 0).Value = 4;
            this.neuSpread1_Sheet1.Cells.Get(4, 0).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(4, 0).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(4, 0).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.neuSpread1_Sheet1.Cells.Get(4, 0).ParseFormatString = "n";
            this.neuSpread1_Sheet1.Cells.Get(4, 0).Value = 5;
            this.neuSpread1_Sheet1.Cells.Get(5, 0).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(5, 0).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(5, 0).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.neuSpread1_Sheet1.Cells.Get(5, 0).ParseFormatString = "n";
            this.neuSpread1_Sheet1.Cells.Get(5, 0).Value = 6;
            this.neuSpread1_Sheet1.Cells.Get(6, 0).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(6, 0).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(6, 0).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.neuSpread1_Sheet1.Cells.Get(6, 0).ParseFormatString = "n";
            this.neuSpread1_Sheet1.Cells.Get(6, 0).Value = 7;
            this.neuSpread1_Sheet1.Cells.Get(7, 0).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(7, 0).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.neuSpread1_Sheet1.Cells.Get(7, 0).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.neuSpread1_Sheet1.Cells.Get(7, 0).ParseFormatString = "n";
            this.neuSpread1_Sheet1.Cells.Get(7, 0).Value = 8;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "编码";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "药品名称";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "组";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "规格";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "数量";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "单位";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "单价";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "金额";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "用法";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "每次用量";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "频次";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "天数";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "备注";
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "编码";
            this.neuSpread1_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 55F;
            this.neuSpread1_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "药品名称";
            this.neuSpread1_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 165F;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "组";
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 26F;
            this.neuSpread1_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "规格";
            this.neuSpread1_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 100F;
            this.neuSpread1_Sheet1.Columns.Get(4).CellType = textCellType4;
            this.neuSpread1_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "数量";
            this.neuSpread1_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 40F;
            this.neuSpread1_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "单位";
            this.neuSpread1_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 35F;
            this.neuSpread1_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Columns.Get(6).Label = "单价";
            this.neuSpread1_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 45F;
            this.neuSpread1_Sheet1.Columns.Get(7).CellType = numberCellType4;
            this.neuSpread1_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Columns.Get(7).Label = "金额";
            this.neuSpread1_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(7).Width = 75F;
            this.neuSpread1_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(8).Label = "用法";
            this.neuSpread1_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(8).Width = 62F;
            this.neuSpread1_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Columns.Get(9).Label = "每次用量";
            this.neuSpread1_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(9).Width = 55F;
            this.neuSpread1_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(10).Label = "频次";
            this.neuSpread1_Sheet1.Columns.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(10).Width = 65F;
            this.neuSpread1_Sheet1.Columns.Get(11).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Columns.Get(11).Label = "天数";
            this.neuSpread1_Sheet1.Columns.Get(11).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(11).Width = 50F;
            this.neuSpread1_Sheet1.Columns.Get(12).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Columns.Get(12).Label = "备注";
            this.neuSpread1_Sheet1.Columns.Get(12).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(12).Width = 80F;
            this.neuSpread1_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.DefaultStyle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.Rows.Default.Height = 28F;
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.SheetCornerStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.neuSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ucDrugList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.Name = "ucDrugList";
            this.Size = new System.Drawing.Size(851, 367);
            ((System.ComponentModel.ISupportInitialize)(this.npbBarCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox npbBarCode;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbAge;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbCardNo;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbReprint;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbInvoice;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbRecipe;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbDeptName;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbDiagnose;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbSex;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbName;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuPanName;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbRecipeDate;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbDoctor;
        private FS.SOC.Windows.Forms.FpSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;

    }
}
