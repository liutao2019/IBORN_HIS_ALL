namespace FS.SOC.Local.InpatientFee.GuangZhou
{
    partial class ucAlterFeeRate
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
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.fpFeeDetail = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpFeeDetail_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.fpFeeDetail_Sheet2 = new FarPoint.Win.Spread.SheetView();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btnModify = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtPayRate = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.cmbMinFee = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.txtBirthday = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNurseStation = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDoctor = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDateIn = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtBedNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDeptName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBalanceType = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtInpatientNo = new FS.HISFC.Components.Common.Controls.ucQueryInpatientNo();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkChangedate = new System.Windows.Forms.CheckBox();
            this.lbDate = new System.Windows.Forms.Label();
            this.dtBegin = new System.Windows.Forms.DateTimePicker();
            this.dtEnd = new System.Windows.Forms.DateTimePicker();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpFeeDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpFeeDetail_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpFeeDetail_Sheet2)).BeginInit();
            this.panel7.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(918, 526);
            this.panel1.TabIndex = 100;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 45);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(918, 481);
            this.panel3.TabIndex = 100;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Transparent;
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Controls.Add(this.panel7);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 113);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(918, 368);
            this.panel5.TabIndex = 1;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.fpFeeDetail);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(918, 355);
            this.panel6.TabIndex = 102;
            // 
            // fpFeeDetail
            // 
            this.fpFeeDetail.About = "3.0.2004.2005";
            this.fpFeeDetail.AccessibleDescription = "fpFeeDetail, 非药品";
            this.fpFeeDetail.BackColor = System.Drawing.Color.White;
            this.fpFeeDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpFeeDetail.FileName = "";
            this.fpFeeDetail.Font = new System.Drawing.Font("宋体", 9F);
            this.fpFeeDetail.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpFeeDetail.IsAutoSaveGridStatus = false;
            this.fpFeeDetail.IsCanCustomConfigColumn = false;
            this.fpFeeDetail.Location = new System.Drawing.Point(0, 0);
            this.fpFeeDetail.Name = "fpFeeDetail";
            this.fpFeeDetail.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpFeeDetail.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpFeeDetail_Sheet1,
            this.fpFeeDetail_Sheet2});
            this.fpFeeDetail.Size = new System.Drawing.Size(918, 355);
            this.fpFeeDetail.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpFeeDetail.TabIndex = 100;
            this.fpFeeDetail.TabStrip.ButtonPolicy = FarPoint.Win.Spread.TabStripButtonPolicy.AsNeeded;
            this.fpFeeDetail.TabStrip.DefaultSheetTab.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpFeeDetail.TabStrip.DefaultSheetTab.Size = -1;
            this.fpFeeDetail.TabStripPlacement = FarPoint.Win.Spread.TabStripPlacement.Top;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpFeeDetail.TextTipAppearance = tipAppearance1;
            this.fpFeeDetail.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpFeeDetail.ActiveSheetIndex = 1;
            // 
            // fpFeeDetail_Sheet1
            // 
            this.fpFeeDetail_Sheet1.Reset();
            this.fpFeeDetail_Sheet1.SheetName = "药品";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpFeeDetail_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpFeeDetail_Sheet1.ColumnCount = 23;
            this.fpFeeDetail_Sheet1.RowCount = 0;
            this.fpFeeDetail_Sheet1.ActiveColumnIndex = 13;
            this.fpFeeDetail_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpFeeDetail_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "自定义码";
            this.fpFeeDetail_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "药品名称";
            this.fpFeeDetail_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "规格";
            this.fpFeeDetail_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "分类";
            this.fpFeeDetail_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "价格";
            this.fpFeeDetail_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "数量";
            this.fpFeeDetail_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "单位";
            this.fpFeeDetail_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "总金额";
            this.fpFeeDetail_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "自费金额";
            this.fpFeeDetail_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "自费比例";
            this.fpFeeDetail_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "自付金额";
            this.fpFeeDetail_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "自付比例";
            this.fpFeeDetail_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "记账金额";
            this.fpFeeDetail_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "划价日期";
            this.fpFeeDetail_Sheet1.ColumnHeader.Cells.Get(0, 14).Value = "记账日期";
            this.fpFeeDetail_Sheet1.ColumnHeader.Cells.Get(0, 15).Value = "执行科室";
            this.fpFeeDetail_Sheet1.ColumnHeader.Cells.Get(0, 16).Value = "审批人";
            this.fpFeeDetail_Sheet1.ColumnHeader.Cells.Get(0, 17).Value = "审批项目";
            this.fpFeeDetail_Sheet1.ColumnHeader.Cells.Get(0, 18).Value = "医保等级";
            this.fpFeeDetail_Sheet1.ColumnHeader.Cells.Get(0, 19).Value = "处方号";
            this.fpFeeDetail_Sheet1.ColumnHeader.Cells.Get(0, 20).Value = "处方序列";
            this.fpFeeDetail_Sheet1.ColumnHeader.Cells.Get(0, 21).Value = "拼音码";
            this.fpFeeDetail_Sheet1.ColumnHeader.Cells.Get(0, 22).Value = "五笔码";
            this.fpFeeDetail_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpFeeDetail_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpFeeDetail_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.fpFeeDetail_Sheet1.Columns.Get(1).AllowAutoSort = true;
            this.fpFeeDetail_Sheet1.Columns.Get(1).Label = "药品名称";
            this.fpFeeDetail_Sheet1.Columns.Get(1).Locked = true;
            this.fpFeeDetail_Sheet1.Columns.Get(1).Width = 150F;
            this.fpFeeDetail_Sheet1.Columns.Get(2).AllowAutoSort = true;
            this.fpFeeDetail_Sheet1.Columns.Get(2).CellType = textCellType1;
            this.fpFeeDetail_Sheet1.Columns.Get(2).Label = "规格";
            this.fpFeeDetail_Sheet1.Columns.Get(2).Locked = true;
            this.fpFeeDetail_Sheet1.Columns.Get(2).Width = 66F;
            this.fpFeeDetail_Sheet1.Columns.Get(3).AllowAutoSort = true;
            this.fpFeeDetail_Sheet1.Columns.Get(3).Label = "分类";
            this.fpFeeDetail_Sheet1.Columns.Get(3).Locked = true;
            this.fpFeeDetail_Sheet1.Columns.Get(4).AllowAutoSort = true;
            this.fpFeeDetail_Sheet1.Columns.Get(4).CellType = numberCellType1;
            this.fpFeeDetail_Sheet1.Columns.Get(4).Label = "价格";
            this.fpFeeDetail_Sheet1.Columns.Get(4).Locked = true;
            this.fpFeeDetail_Sheet1.Columns.Get(4).Width = 39F;
            this.fpFeeDetail_Sheet1.Columns.Get(5).CellType = numberCellType2;
            this.fpFeeDetail_Sheet1.Columns.Get(5).Label = "数量";
            this.fpFeeDetail_Sheet1.Columns.Get(5).Locked = true;
            this.fpFeeDetail_Sheet1.Columns.Get(5).Width = 39F;
            this.fpFeeDetail_Sheet1.Columns.Get(6).Label = "单位";
            this.fpFeeDetail_Sheet1.Columns.Get(6).Locked = true;
            this.fpFeeDetail_Sheet1.Columns.Get(6).Width = 39F;
            this.fpFeeDetail_Sheet1.Columns.Get(7).AllowAutoSort = true;
            this.fpFeeDetail_Sheet1.Columns.Get(7).Label = "总金额";
            this.fpFeeDetail_Sheet1.Columns.Get(7).Locked = true;
            this.fpFeeDetail_Sheet1.Columns.Get(7).Width = 51F;
            this.fpFeeDetail_Sheet1.Columns.Get(8).Label = "自费金额";
            this.fpFeeDetail_Sheet1.Columns.Get(8).Locked = true;
            this.fpFeeDetail_Sheet1.Columns.Get(9).AllowAutoSort = true;
            this.fpFeeDetail_Sheet1.Columns.Get(9).Label = "自费比例";
            this.fpFeeDetail_Sheet1.Columns.Get(9).Locked = true;
            this.fpFeeDetail_Sheet1.Columns.Get(9).Width = 51F;
            this.fpFeeDetail_Sheet1.Columns.Get(10).Label = "自付金额";
            this.fpFeeDetail_Sheet1.Columns.Get(10).Locked = true;
            this.fpFeeDetail_Sheet1.Columns.Get(10).Width = 57F;
            this.fpFeeDetail_Sheet1.Columns.Get(11).AllowAutoSort = true;
            this.fpFeeDetail_Sheet1.Columns.Get(11).Label = "自付比例";
            this.fpFeeDetail_Sheet1.Columns.Get(11).Locked = true;
            this.fpFeeDetail_Sheet1.Columns.Get(11).Width = 52F;
            this.fpFeeDetail_Sheet1.Columns.Get(12).Label = "记账金额";
            this.fpFeeDetail_Sheet1.Columns.Get(12).Locked = true;
            this.fpFeeDetail_Sheet1.Columns.Get(14).Label = "记账日期";
            this.fpFeeDetail_Sheet1.Columns.Get(14).Locked = true;
            this.fpFeeDetail_Sheet1.Columns.Get(15).AllowAutoSort = true;
            this.fpFeeDetail_Sheet1.Columns.Get(15).Label = "执行科室";
            this.fpFeeDetail_Sheet1.Columns.Get(15).Locked = true;
            this.fpFeeDetail_Sheet1.Columns.Get(16).Label = "审批人";
            this.fpFeeDetail_Sheet1.Columns.Get(16).Locked = true;
            this.fpFeeDetail_Sheet1.Columns.Get(17).AllowAutoSort = true;
            this.fpFeeDetail_Sheet1.Columns.Get(17).Label = "审批项目";
            this.fpFeeDetail_Sheet1.Columns.Get(17).Locked = true;
            this.fpFeeDetail_Sheet1.Columns.Get(17).Width = 92F;
            this.fpFeeDetail_Sheet1.Columns.Get(18).AllowAutoSort = true;
            this.fpFeeDetail_Sheet1.Columns.Get(18).Label = "医保等级";
            this.fpFeeDetail_Sheet1.Columns.Get(18).Width = 54F;
            this.fpFeeDetail_Sheet1.Columns.Get(19).Label = "处方号";
            this.fpFeeDetail_Sheet1.Columns.Get(19).Locked = true;
            this.fpFeeDetail_Sheet1.Columns.Get(19).Width = 0F;
            this.fpFeeDetail_Sheet1.Columns.Get(20).Label = "处方序列";
            this.fpFeeDetail_Sheet1.Columns.Get(20).Locked = true;
            this.fpFeeDetail_Sheet1.Columns.Get(20).Width = 0F;
            this.fpFeeDetail_Sheet1.Columns.Get(21).Label = "拼音码";
            this.fpFeeDetail_Sheet1.Columns.Get(21).Visible = false;
            this.fpFeeDetail_Sheet1.Columns.Get(22).Label = "五笔码";
            this.fpFeeDetail_Sheet1.Columns.Get(22).Visible = false;
            this.fpFeeDetail_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpFeeDetail_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpFeeDetail_Sheet1.RowHeader.Columns.Get(0).Width = 36F;
            this.fpFeeDetail_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpFeeDetail_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpFeeDetail_Sheet1.Rows.Default.Height = 25F;
            this.fpFeeDetail_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpFeeDetail_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.fpFeeDetail_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpFeeDetail_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpFeeDetail.SetActiveViewport(0, 1, 0);
            // 
            // fpFeeDetail_Sheet2
            // 
            this.fpFeeDetail_Sheet2.Reset();
            this.fpFeeDetail_Sheet2.SheetName = "非药品";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpFeeDetail_Sheet2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpFeeDetail_Sheet2.ColumnCount = 23;
            this.fpFeeDetail_Sheet2.RowCount = 0;
            this.fpFeeDetail_Sheet2.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpFeeDetail_Sheet2.ColumnHeader.Cells.Get(0, 0).Value = "自定义码";
            this.fpFeeDetail_Sheet2.ColumnHeader.Cells.Get(0, 1).Value = "项目名称";
            this.fpFeeDetail_Sheet2.ColumnHeader.Cells.Get(0, 2).Value = "套餐";
            this.fpFeeDetail_Sheet2.ColumnHeader.Cells.Get(0, 3).Value = "分类";
            this.fpFeeDetail_Sheet2.ColumnHeader.Cells.Get(0, 4).Value = "价格";
            this.fpFeeDetail_Sheet2.ColumnHeader.Cells.Get(0, 5).Value = "数量";
            this.fpFeeDetail_Sheet2.ColumnHeader.Cells.Get(0, 6).Value = "单位";
            this.fpFeeDetail_Sheet2.ColumnHeader.Cells.Get(0, 7).Value = "总金额";
            this.fpFeeDetail_Sheet2.ColumnHeader.Cells.Get(0, 8).Value = "自费金额";
            this.fpFeeDetail_Sheet2.ColumnHeader.Cells.Get(0, 9).Value = "自费比例";
            this.fpFeeDetail_Sheet2.ColumnHeader.Cells.Get(0, 10).Value = "自付金额";
            this.fpFeeDetail_Sheet2.ColumnHeader.Cells.Get(0, 11).Value = "自付比例";
            this.fpFeeDetail_Sheet2.ColumnHeader.Cells.Get(0, 12).Value = "记账金额";
            this.fpFeeDetail_Sheet2.ColumnHeader.Cells.Get(0, 13).Value = "划价日期";
            this.fpFeeDetail_Sheet2.ColumnHeader.Cells.Get(0, 14).Value = "记账日期";
            this.fpFeeDetail_Sheet2.ColumnHeader.Cells.Get(0, 15).Value = "执行科室";
            this.fpFeeDetail_Sheet2.ColumnHeader.Cells.Get(0, 16).Value = "审批人";
            this.fpFeeDetail_Sheet2.ColumnHeader.Cells.Get(0, 17).Value = "审批项目";
            this.fpFeeDetail_Sheet2.ColumnHeader.Cells.Get(0, 18).Value = "医保等级";
            this.fpFeeDetail_Sheet2.ColumnHeader.Cells.Get(0, 19).Value = "处方号";
            this.fpFeeDetail_Sheet2.ColumnHeader.Cells.Get(0, 20).Value = "处方序列";
            this.fpFeeDetail_Sheet2.ColumnHeader.Cells.Get(0, 21).Value = "拼音码";
            this.fpFeeDetail_Sheet2.ColumnHeader.Cells.Get(0, 22).Value = "五笔码";
            this.fpFeeDetail_Sheet2.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpFeeDetail_Sheet2.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpFeeDetail_Sheet2.ColumnHeader.Rows.Get(0).Height = 30F;
            this.fpFeeDetail_Sheet2.Columns.Get(1).AllowAutoSort = true;
            this.fpFeeDetail_Sheet2.Columns.Get(1).Label = "项目名称";
            this.fpFeeDetail_Sheet2.Columns.Get(1).Locked = true;
            this.fpFeeDetail_Sheet2.Columns.Get(1).Width = 173F;
            this.fpFeeDetail_Sheet2.Columns.Get(2).AllowAutoSort = true;
            this.fpFeeDetail_Sheet2.Columns.Get(2).Label = "套餐";
            this.fpFeeDetail_Sheet2.Columns.Get(2).Locked = true;
            this.fpFeeDetail_Sheet2.Columns.Get(2).Width = 79F;
            this.fpFeeDetail_Sheet2.Columns.Get(3).AllowAutoSort = true;
            this.fpFeeDetail_Sheet2.Columns.Get(3).Label = "分类";
            this.fpFeeDetail_Sheet2.Columns.Get(3).Locked = true;
            this.fpFeeDetail_Sheet2.Columns.Get(3).Width = 55F;
            this.fpFeeDetail_Sheet2.Columns.Get(4).AllowAutoSort = true;
            this.fpFeeDetail_Sheet2.Columns.Get(4).Label = "价格";
            this.fpFeeDetail_Sheet2.Columns.Get(4).Locked = true;
            this.fpFeeDetail_Sheet2.Columns.Get(4).Width = 49F;
            this.fpFeeDetail_Sheet2.Columns.Get(5).Label = "数量";
            this.fpFeeDetail_Sheet2.Columns.Get(5).Locked = true;
            this.fpFeeDetail_Sheet2.Columns.Get(5).Width = 37F;
            this.fpFeeDetail_Sheet2.Columns.Get(6).Label = "单位";
            this.fpFeeDetail_Sheet2.Columns.Get(6).Locked = true;
            this.fpFeeDetail_Sheet2.Columns.Get(6).Width = 33F;
            this.fpFeeDetail_Sheet2.Columns.Get(7).AllowAutoSort = true;
            this.fpFeeDetail_Sheet2.Columns.Get(7).Label = "总金额";
            this.fpFeeDetail_Sheet2.Columns.Get(7).Locked = true;
            this.fpFeeDetail_Sheet2.Columns.Get(7).Width = 65F;
            this.fpFeeDetail_Sheet2.Columns.Get(8).Label = "自费金额";
            this.fpFeeDetail_Sheet2.Columns.Get(8).Locked = true;
            this.fpFeeDetail_Sheet2.Columns.Get(8).Width = 58F;
            this.fpFeeDetail_Sheet2.Columns.Get(9).AllowAutoSort = true;
            this.fpFeeDetail_Sheet2.Columns.Get(9).Label = "自费比例";
            this.fpFeeDetail_Sheet2.Columns.Get(9).Locked = true;
            this.fpFeeDetail_Sheet2.Columns.Get(9).Width = 58F;
            this.fpFeeDetail_Sheet2.Columns.Get(10).Label = "自付金额";
            this.fpFeeDetail_Sheet2.Columns.Get(10).Locked = true;
            this.fpFeeDetail_Sheet2.Columns.Get(10).Width = 56F;
            this.fpFeeDetail_Sheet2.Columns.Get(11).AllowAutoSort = true;
            this.fpFeeDetail_Sheet2.Columns.Get(11).Label = "自付比例";
            this.fpFeeDetail_Sheet2.Columns.Get(11).Locked = true;
            this.fpFeeDetail_Sheet2.Columns.Get(11).Width = 47F;
            this.fpFeeDetail_Sheet2.Columns.Get(12).Label = "记账金额";
            this.fpFeeDetail_Sheet2.Columns.Get(12).Locked = true;
            this.fpFeeDetail_Sheet2.Columns.Get(12).Width = 58F;
            this.fpFeeDetail_Sheet2.Columns.Get(14).Label = "记账日期";
            this.fpFeeDetail_Sheet2.Columns.Get(14).Locked = true;
            this.fpFeeDetail_Sheet2.Columns.Get(14).Width = 63F;
            this.fpFeeDetail_Sheet2.Columns.Get(15).AllowAutoSort = true;
            this.fpFeeDetail_Sheet2.Columns.Get(15).Label = "执行科室";
            this.fpFeeDetail_Sheet2.Columns.Get(15).Locked = true;
            this.fpFeeDetail_Sheet2.Columns.Get(15).Width = 63F;
            this.fpFeeDetail_Sheet2.Columns.Get(16).Label = "审批人";
            this.fpFeeDetail_Sheet2.Columns.Get(16).Locked = true;
            this.fpFeeDetail_Sheet2.Columns.Get(16).Width = 51F;
            this.fpFeeDetail_Sheet2.Columns.Get(17).AllowAutoSort = true;
            this.fpFeeDetail_Sheet2.Columns.Get(17).Label = "审批项目";
            this.fpFeeDetail_Sheet2.Columns.Get(17).Locked = true;
            this.fpFeeDetail_Sheet2.Columns.Get(17).Width = 81F;
            this.fpFeeDetail_Sheet2.Columns.Get(18).AllowAutoSort = true;
            this.fpFeeDetail_Sheet2.Columns.Get(18).Label = "医保等级";
            this.fpFeeDetail_Sheet2.Columns.Get(18).Width = 53F;
            this.fpFeeDetail_Sheet2.Columns.Get(19).Label = "处方号";
            this.fpFeeDetail_Sheet2.Columns.Get(19).Locked = true;
            this.fpFeeDetail_Sheet2.Columns.Get(19).Width = 0F;
            this.fpFeeDetail_Sheet2.Columns.Get(20).Label = "处方序列";
            this.fpFeeDetail_Sheet2.Columns.Get(20).Locked = true;
            this.fpFeeDetail_Sheet2.Columns.Get(20).Width = 0F;
            this.fpFeeDetail_Sheet2.Columns.Get(21).Label = "拼音码";
            this.fpFeeDetail_Sheet2.Columns.Get(21).Visible = false;
            this.fpFeeDetail_Sheet2.Columns.Get(22).Label = "五笔码";
            this.fpFeeDetail_Sheet2.Columns.Get(22).Visible = false;
            this.fpFeeDetail_Sheet2.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpFeeDetail_Sheet2.RowHeader.Columns.Default.Resizable = true;
            this.fpFeeDetail_Sheet2.RowHeader.Columns.Get(0).Width = 36F;
            this.fpFeeDetail_Sheet2.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpFeeDetail_Sheet2.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpFeeDetail_Sheet2.Rows.Default.Height = 25F;
            this.fpFeeDetail_Sheet2.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpFeeDetail_Sheet2.SheetCornerStyle.Parent = "CornerDefault";
            this.fpFeeDetail_Sheet2.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpFeeDetail_Sheet2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpFeeDetail.SetActiveViewport(1, 1, 0);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.btnModify);
            this.panel7.Controls.Add(this.btnExit);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel7.Location = new System.Drawing.Point(0, 355);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(918, 13);
            this.panel7.TabIndex = 101;
            // 
            // btnModify
            // 
            this.btnModify.Location = new System.Drawing.Point(488, 19);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(107, 25);
            this.btnModify.TabIndex = 3;
            this.btnModify.Text = "修改(&M)";
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(611, 18);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(107, 26);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "退出(&X)";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Controls.Add(this.groupBox3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(918, 113);
            this.panel4.TabIndex = 100;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.txtPayRate);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtFilter);
            this.groupBox3.Controls.Add(this.cmbMinFee);
            this.groupBox3.Controls.Add(this.txtBirthday);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txtNurseStation);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.txtDoctor);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.txtDateIn);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.txtBedNo);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txtDeptName);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtName);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.txtBalanceType);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(918, 113);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "患者信息";
            // 
            // txtPayRate
            // 
            this.txtPayRate.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtPayRate.Location = new System.Drawing.Point(810, 56);
            this.txtPayRate.Name = "txtPayRate";
            this.txtPayRate.ReadOnly = true;
            this.txtPayRate.Size = new System.Drawing.Size(38, 21);
            this.txtPayRate.TabIndex = 38;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(748, 60);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 17);
            this.label10.TabIndex = 37;
            this.label10.Text = "自付比例";
            // 
            // label3
            // 
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(565, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(215, 17);
            this.label3.TabIndex = 36;
            this.label3.Text = "红色字体表示与患者的自付比例不相同";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtFilter
            // 
            this.txtFilter.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtFilter.Location = new System.Drawing.Point(202, 86);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(343, 21);
            this.txtFilter.TabIndex = 35;
            // 
            // cmbMinFee
            // 
            this.cmbMinFee.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbMinFee.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbMinFee.FormattingEnabled = true;
            this.cmbMinFee.IsEnter2Tab = false;
            this.cmbMinFee.IsFlat = false;
            this.cmbMinFee.IsLike = true;
            this.cmbMinFee.IsListOnly = false;
            this.cmbMinFee.IsPopForm = true;
            this.cmbMinFee.IsShowCustomerList = false;
            this.cmbMinFee.IsShowID = false;
            this.cmbMinFee.IsShowIDAndName = false;
            this.cmbMinFee.Location = new System.Drawing.Point(67, 87);
            this.cmbMinFee.Name = "cmbMinFee";
            this.cmbMinFee.ShowCustomerList = false;
            this.cmbMinFee.ShowID = false;
            this.cmbMinFee.Size = new System.Drawing.Size(116, 20);
            this.cmbMinFee.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbMinFee.TabIndex = 18;
            this.cmbMinFee.Tag = "";
            this.cmbMinFee.ToolBarUse = false;
            // 
            // txtBirthday
            // 
            this.txtBirthday.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtBirthday.Location = new System.Drawing.Point(629, 56);
            this.txtBirthday.Name = "txtBirthday";
            this.txtBirthday.ReadOnly = true;
            this.txtBirthday.Size = new System.Drawing.Size(106, 21);
            this.txtBirthday.TabIndex = 34;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(565, 60);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(55, 17);
            this.label15.TabIndex = 33;
            this.label15.Text = "出生日期";
            this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(565, 28);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 17);
            this.label9.TabIndex = 31;
            this.label9.Text = "所属病区";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtNurseStation
            // 
            this.txtNurseStation.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtNurseStation.Location = new System.Drawing.Point(629, 24);
            this.txtNurseStation.Name = "txtNurseStation";
            this.txtNurseStation.ReadOnly = true;
            this.txtNurseStation.Size = new System.Drawing.Size(106, 21);
            this.txtNurseStation.TabIndex = 32;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(26, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "过滤:";
            // 
            // txtDoctor
            // 
            this.txtDoctor.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtDoctor.Location = new System.Drawing.Point(258, 56);
            this.txtDoctor.Name = "txtDoctor";
            this.txtDoctor.ReadOnly = true;
            this.txtDoctor.Size = new System.Drawing.Size(107, 21);
            this.txtDoctor.TabIndex = 30;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(197, 60);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 17);
            this.label8.TabIndex = 29;
            this.label8.Text = "住院医生";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtDateIn
            // 
            this.txtDateIn.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtDateIn.Location = new System.Drawing.Point(67, 56);
            this.txtDateIn.Name = "txtDateIn";
            this.txtDateIn.ReadOnly = true;
            this.txtDateIn.Size = new System.Drawing.Size(116, 21);
            this.txtDateIn.TabIndex = 28;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(8, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 17);
            this.label7.TabIndex = 27;
            this.label7.Text = "入院日期";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtBedNo
            // 
            this.txtBedNo.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtBedNo.Location = new System.Drawing.Point(439, 56);
            this.txtBedNo.Name = "txtBedNo";
            this.txtBedNo.ReadOnly = true;
            this.txtBedNo.Size = new System.Drawing.Size(106, 21);
            this.txtBedNo.TabIndex = 23;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(394, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 17);
            this.label4.TabIndex = 20;
            this.label4.Text = "床号";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(377, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 17);
            this.label6.TabIndex = 21;
            this.label6.Text = "住院科室";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtDeptName
            // 
            this.txtDeptName.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtDeptName.Location = new System.Drawing.Point(439, 24);
            this.txtDeptName.Name = "txtDeptName";
            this.txtDeptName.ReadOnly = true;
            this.txtDeptName.Size = new System.Drawing.Size(106, 21);
            this.txtDeptName.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 17);
            this.label1.TabIndex = 19;
            this.label1.Text = "患者姓名";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtName.Location = new System.Drawing.Point(67, 24);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(116, 21);
            this.txtName.TabIndex = 22;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(197, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 17);
            this.label5.TabIndex = 25;
            this.label5.Text = "合同单位";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtBalanceType
            // 
            this.txtBalanceType.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtBalanceType.Location = new System.Drawing.Point(258, 24);
            this.txtBalanceType.Name = "txtBalanceType";
            this.txtBalanceType.ReadOnly = true;
            this.txtBalanceType.Size = new System.Drawing.Size(107, 21);
            this.txtBalanceType.TabIndex = 26;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.txtInpatientNo);
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(918, 45);
            this.panel2.TabIndex = 100;
            // 
            // txtInpatientNo
            // 
            this.txtInpatientNo.BackColor = System.Drawing.Color.Transparent;
            this.txtInpatientNo.DefaultInputType = 0;
            this.txtInpatientNo.InputType = 0;
            this.txtInpatientNo.IsDeptOnly = true;
            this.txtInpatientNo.Location = new System.Drawing.Point(11, 13);
            this.txtInpatientNo.Name = "txtInpatientNo";
            this.txtInpatientNo.PatientInState = "ALL";
            this.txtInpatientNo.ShowState = FS.HISFC.Components.Common.Controls.enuShowState.InhosBeforBalanced;
            this.txtInpatientNo.Size = new System.Drawing.Size(179, 29);
            this.txtInpatientNo.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.chkChangedate);
            this.groupBox2.Controls.Add(this.lbDate);
            this.groupBox2.Controls.Add(this.dtBegin);
            this.groupBox2.Controls.Add(this.dtEnd);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(918, 45);
            this.groupBox2.TabIndex = 100;
            this.groupBox2.TabStop = false;
            // 
            // chkChangedate
            // 
            this.chkChangedate.Location = new System.Drawing.Point(503, 16);
            this.chkChangedate.Name = "chkChangedate";
            this.chkChangedate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkChangedate.Size = new System.Drawing.Size(110, 24);
            this.chkChangedate.TabIndex = 2;
            this.chkChangedate.Text = "不改变收费时间";
            this.chkChangedate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDate
            // 
            this.lbDate.Location = new System.Drawing.Point(338, 20);
            this.lbDate.Name = "lbDate";
            this.lbDate.Size = new System.Drawing.Size(27, 18);
            this.lbDate.TabIndex = 1;
            this.lbDate.Text = "至";
            this.lbDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtBegin
            // 
            this.dtBegin.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtBegin.Location = new System.Drawing.Point(202, 18);
            this.dtBegin.Name = "dtBegin";
            this.dtBegin.Size = new System.Drawing.Size(138, 21);
            this.dtBegin.TabIndex = 0;
            // 
            // dtEnd
            // 
            this.dtEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEnd.Location = new System.Drawing.Point(365, 18);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(138, 21);
            this.dtEnd.TabIndex = 0;
            // 
            // ucAlterFeeRate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "ucAlterFeeRate";
            this.Size = new System.Drawing.Size(918, 526);
            this.Load += new System.EventHandler(this.ucAlterFeeRate_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpFeeDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpFeeDetail_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpFeeDetail_Sheet2)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        //private FS.Common.Controls.ucQueryInpatientNo txtInpatientNo;
        private FS.HISFC.Components.Common.Controls.ucQueryInpatientNo txtInpatientNo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtBirthday;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtNurseStation;
        private System.Windows.Forms.TextBox txtDoctor;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDateIn;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtBedNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDeptName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBalanceType;
        //		private FS.Common.Controls.ucPatientInfo ucPatientInfo;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnModify;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.DateTimePicker dtBegin;
        private System.Windows.Forms.DateTimePicker dtEnd;
        private System.Windows.Forms.Label lbDate;
        private System.Windows.Forms.CheckBox chkChangedate;
        //private FS.neuFC.Interface.Controls.ComboBox cmbMinFee;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel6;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbMinFee;
        protected FS.FrameWork.WinForms.Controls.NeuSpread fpFeeDetail;
        protected FarPoint.Win.Spread.SheetView fpFeeDetail_Sheet2;
        private FarPoint.Win.Spread.SheetView fpFeeDetail_Sheet1;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPayRate;
        private System.Windows.Forms.Label label10;

    }
}
