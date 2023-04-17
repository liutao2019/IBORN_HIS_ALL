namespace FS.SOC.HISFC.Components.Pharmacy.Common.Check
{
    partial class frmAssignPartion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("zh-CN", false);
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType2 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.nbtSave = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.nbtAdd = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.nbtDelete = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.nbtReturn = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuTabControl1 = new FS.FrameWork.WinForms.Controls.NeuTabControl();
            this.tpCommonPlaceNO = new System.Windows.Forms.TabPage();
            this.fpSpread1 = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.tpSpecialDrug = new System.Windows.Forms.TabPage();
            this.fpSpread2 = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.fpSpread2_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.neuPanel1.SuspendLayout();
            this.neuTabControl1.SuspendLayout();
            this.tpCommonPlaceNO.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            this.tpSpecialDrug.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread2_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.panel5);
            this.neuPanel2.Controls.Add(this.panel1);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuPanel2.Location = new System.Drawing.Point(0, 396);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(397, 83);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 29;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel5.Controls.Add(this.nbtSave);
            this.panel5.Controls.Add(this.nbtAdd);
            this.panel5.Controls.Add(this.nbtDelete);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(397, 40);
            this.panel5.TabIndex = 27;
            // 
            // nbtSave
            // 
            this.nbtSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nbtSave.Location = new System.Drawing.Point(301, 6);
            this.nbtSave.Name = "nbtSave";
            this.nbtSave.Size = new System.Drawing.Size(75, 23);
            this.nbtSave.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nbtSave.TabIndex = 24;
            this.nbtSave.Text = "保存";
            this.nbtSave.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.nbtSave.UseVisualStyleBackColor = true;
            // 
            // nbtAdd
            // 
            this.nbtAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nbtAdd.Location = new System.Drawing.Point(115, 6);
            this.nbtAdd.Name = "nbtAdd";
            this.nbtAdd.Size = new System.Drawing.Size(75, 23);
            this.nbtAdd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nbtAdd.TabIndex = 23;
            this.nbtAdd.Text = "增加";
            this.nbtAdd.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.nbtAdd.UseVisualStyleBackColor = true;
            // 
            // nbtDelete
            // 
            this.nbtDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nbtDelete.Location = new System.Drawing.Point(208, 6);
            this.nbtDelete.Name = "nbtDelete";
            this.nbtDelete.Size = new System.Drawing.Size(75, 23);
            this.nbtDelete.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nbtDelete.TabIndex = 22;
            this.nbtDelete.Text = "删除";
            this.nbtDelete.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.nbtDelete.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.nbtReturn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(397, 43);
            this.panel1.TabIndex = 26;
            // 
            // nbtReturn
            // 
            this.nbtReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nbtReturn.Location = new System.Drawing.Point(301, 6);
            this.nbtReturn.Name = "nbtReturn";
            this.nbtReturn.Size = new System.Drawing.Size(75, 23);
            this.nbtReturn.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nbtReturn.TabIndex = 10;
            this.nbtReturn.Text = "返回";
            this.nbtReturn.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.nbtReturn.UseVisualStyleBackColor = true;
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.neuTabControl1);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(397, 396);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 30;
            // 
            // neuTabControl1
            // 
            this.neuTabControl1.Controls.Add(this.tpCommonPlaceNO);
            this.neuTabControl1.Controls.Add(this.tpSpecialDrug);
            this.neuTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuTabControl1.Location = new System.Drawing.Point(0, 0);
            this.neuTabControl1.Name = "neuTabControl1";
            this.neuTabControl1.SelectedIndex = 0;
            this.neuTabControl1.Size = new System.Drawing.Size(397, 396);
            this.neuTabControl1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTabControl1.TabIndex = 27;
            // 
            // tpCommonPlaceNO
            // 
            this.tpCommonPlaceNO.Controls.Add(this.fpSpread1);
            this.tpCommonPlaceNO.Location = new System.Drawing.Point(4, 22);
            this.tpCommonPlaceNO.Name = "tpCommonPlaceNO";
            this.tpCommonPlaceNO.Size = new System.Drawing.Size(389, 370);
            this.tpCommonPlaceNO.TabIndex = 3;
            this.tpCommonPlaceNO.Text = "货位号区间";
            this.tpCommonPlaceNO.UseVisualStyleBackColor = true;
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "3.0.2004.2005";
            this.fpSpread1.AccessibleDescription = "fpSpread3, Sheet1, Row 0, Column 0, A区A1101";
            this.fpSpread1.BackColor = System.Drawing.SystemColors.Control;
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.Location = new System.Drawing.Point(0, 0);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1});
            this.fpSpread1.Size = new System.Drawing.Size(389, 370);
            this.fpSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpSpread1.TabIndex = 26;
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
            this.fpSpread1_Sheet1.ColumnCount = 4;
            this.fpSpread1_Sheet1.RowCount = 1;
            this.fpSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247))))), System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(217)))), ((int)(((byte)(217))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, false);
            this.fpSpread1_Sheet1.Cells.Get(0, 0).Value = "A区A1101";
            this.fpSpread1_Sheet1.Cells.Get(0, 1).Value = "A区A9999";
            this.fpSpread1_Sheet1.Cells.Get(0, 2).CellType = textCellType1;
            this.fpSpread1_Sheet1.Cells.Get(0, 2).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.fpSpread1_Sheet1.Cells.Get(0, 2).ParseFormatInfo)).CurrencySymbol = "¥";
            ((System.Globalization.NumberFormatInfo)(this.fpSpread1_Sheet1.Cells.Get(0, 2).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.fpSpread1_Sheet1.Cells.Get(0, 2).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.fpSpread1_Sheet1.Cells.Get(0, 2).ParseFormatString = "n";
            this.fpSpread1_Sheet1.Cells.Get(0, 2).Value = "009999";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "开始货位号";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "结束货位号";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "负责人工号";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "应用";
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.Columns.Get(0).Label = "开始货位号";
            this.fpSpread1_Sheet1.Columns.Get(0).Width = 99F;
            this.fpSpread1_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.Columns.Get(1).Label = "结束货位号";
            this.fpSpread1_Sheet1.Columns.Get(1).Width = 104F;
            this.fpSpread1_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.Columns.Get(2).Label = "负责人工号";
            this.fpSpread1_Sheet1.Columns.Get(2).Width = 81F;
            this.fpSpread1_Sheet1.Columns.Get(3).CellType = checkBoxCellType1;
            this.fpSpread1_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.Columns.Get(3).Label = "应用";
            this.fpSpread1_Sheet1.Columns.Get(3).Width = 38F;
            this.fpSpread1_Sheet1.DefaultStyle.Locked = false;
            this.fpSpread1_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpSpread1_Sheet1.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.RowHeader.Visible = false;
            this.fpSpread1_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpSpread1_Sheet1.SelectionStyle = FarPoint.Win.Spread.SelectionStyles.Both;
            this.fpSpread1_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.fpSpread1_Sheet1.SheetCornerStyle.Locked = false;
            this.fpSpread1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // tpSpecialDrug
            // 
            this.tpSpecialDrug.Controls.Add(this.fpSpread2);
            this.tpSpecialDrug.Location = new System.Drawing.Point(4, 22);
            this.tpSpecialDrug.Name = "tpSpecialDrug";
            this.tpSpecialDrug.Padding = new System.Windows.Forms.Padding(3);
            this.tpSpecialDrug.Size = new System.Drawing.Size(389, 370);
            this.tpSpecialDrug.TabIndex = 1;
            this.tpSpecialDrug.Text = "特殊药品";
            this.tpSpecialDrug.UseVisualStyleBackColor = true;
            // 
            // fpSpread2
            // 
            this.fpSpread2.About = "3.0.2004.2005";
            this.fpSpread2.AccessibleDescription = "fpSpread4, Sheet1, Row 0, Column 0, ";
            this.fpSpread2.BackColor = System.Drawing.SystemColors.Control;
            this.fpSpread2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread2.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread2.Location = new System.Drawing.Point(3, 3);
            this.fpSpread2.Name = "fpSpread2";
            this.fpSpread2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread2.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread2_Sheet1});
            this.fpSpread2.Size = new System.Drawing.Size(383, 364);
            this.fpSpread2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpSpread2.TabIndex = 26;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread2.TextTipAppearance = tipAppearance2;
            this.fpSpread2.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpSpread2_Sheet1
            // 
            this.fpSpread2_Sheet1.Reset();
            this.fpSpread2_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread2_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread2_Sheet1.ColumnCount = 5;
            this.fpSpread2_Sheet1.RowCount = 2;
            this.fpSpread2_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247))))), System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(217)))), ((int)(((byte)(217))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, false);
            this.fpSpread2_Sheet1.Cells.Get(0, 1).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.fpSpread2_Sheet1.Cells.Get(0, 1).ParseFormatInfo)).CurrencySymbol = "¥";
            ((System.Globalization.NumberFormatInfo)(this.fpSpread2_Sheet1.Cells.Get(0, 1).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.fpSpread2_Sheet1.Cells.Get(0, 1).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.fpSpread2_Sheet1.Cells.Get(0, 1).ParseFormatString = "n";
            this.fpSpread2_Sheet1.Cells.Get(0, 1).Value = 1;
            this.fpSpread2_Sheet1.Cells.Get(0, 2).Value = "未分货位";
            this.fpSpread2_Sheet1.Cells.Get(0, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpSpread2_Sheet1.Cells.Get(0, 3).Value = "系统类别";
            this.fpSpread2_Sheet1.Cells.Get(1, 1).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.fpSpread2_Sheet1.Cells.Get(1, 1).ParseFormatInfo)).CurrencySymbol = "¥";
            ((System.Globalization.NumberFormatInfo)(this.fpSpread2_Sheet1.Cells.Get(1, 1).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.fpSpread2_Sheet1.Cells.Get(1, 1).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.fpSpread2_Sheet1.Cells.Get(1, 1).ParseFormatString = "n";
            this.fpSpread2_Sheet1.Cells.Get(1, 1).Value = 2;
            this.fpSpread2_Sheet1.Cells.Get(1, 2).Value = "零散货位";
            this.fpSpread2_Sheet1.Cells.Get(1, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpSpread2_Sheet1.Cells.Get(1, 3).Value = "系统类别";
            this.fpSpread2_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "编码";
            this.fpSpread2_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "类别代码";
            this.fpSpread2_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "名称";
            this.fpSpread2_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "说明";
            this.fpSpread2_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "应用";
            this.fpSpread2_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.fpSpread2_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread2_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.fpSpread2_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread2_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread2_Sheet1.Columns.Get(0).Label = "编码";
            this.fpSpread2_Sheet1.Columns.Get(0).Width = 0F;
            this.fpSpread2_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread2_Sheet1.Columns.Get(1).Label = "类别代码";
            this.fpSpread2_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpSpread2_Sheet1.Columns.Get(2).Label = "名称";
            this.fpSpread2_Sheet1.Columns.Get(2).Width = 113F;
            this.fpSpread2_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread2_Sheet1.Columns.Get(3).Label = "说明";
            this.fpSpread2_Sheet1.Columns.Get(3).Width = 155F;
            this.fpSpread2_Sheet1.Columns.Get(4).CellType = checkBoxCellType2;
            this.fpSpread2_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread2_Sheet1.Columns.Get(4).Label = "应用";
            this.fpSpread2_Sheet1.Columns.Get(4).Width = 40F;
            this.fpSpread2_Sheet1.DefaultStyle.Locked = false;
            this.fpSpread2_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpSpread2_Sheet1.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread2_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpSpread2_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpSpread2_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.fpSpread2_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.fpSpread2_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread2_Sheet1.RowHeader.Visible = false;
            this.fpSpread2_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpSpread2_Sheet1.SelectionStyle = FarPoint.Win.Spread.SelectionStyles.Both;
            this.fpSpread2_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpSpread2_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.fpSpread2_Sheet1.SheetCornerStyle.Locked = false;
            this.fpSpread2_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.fpSpread2_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpSpread2_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // frmAssignPartion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 479);
            this.Controls.Add(this.neuPanel1);
            this.Controls.Add(this.neuPanel2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAssignPartion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "区间维护";
            this.neuPanel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.neuPanel1.ResumeLayout(false);
            this.neuTabControl1.ResumeLayout(false);
            this.tpCommonPlaceNO.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            this.tpSpecialDrug.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread2_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuTabControl neuTabControl1;
        private System.Windows.Forms.TabPage tpCommonPlaceNO;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
        private System.Windows.Forms.TabPage tpSpecialDrug;
        private FarPoint.Win.Spread.SheetView fpSpread2_Sheet1;
        private System.Windows.Forms.Panel panel5;
        private FS.FrameWork.WinForms.Controls.NeuButton nbtSave;
        private FS.FrameWork.WinForms.Controls.NeuButton nbtAdd;
        private FS.FrameWork.WinForms.Controls.NeuButton nbtDelete;
        private System.Windows.Forms.Panel panel1;
        private FS.FrameWork.WinForms.Controls.NeuButton nbtReturn;
        private FS.SOC.Windows.Forms.FpSpread fpSpread1;
        private FS.SOC.Windows.Forms.FpSpread fpSpread2;
    }
}