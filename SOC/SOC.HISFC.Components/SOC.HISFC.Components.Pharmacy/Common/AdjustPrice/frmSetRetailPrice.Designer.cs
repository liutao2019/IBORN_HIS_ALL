namespace FS.SOC.HISFC.Components.Pharmacy.Common.AdjustPrice
{
    partial class frmSetRetailPrice
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
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType2 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.panel1 = new System.Windows.Forms.Panel();
            this.nbtReturn = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.panel5 = new System.Windows.Forms.Panel();
            this.nbtSave = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.nbtAdd = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.nbtDelete = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuTabControl1 = new FS.FrameWork.WinForms.Controls.NeuTabControl();
            this.tpTypeLimit = new System.Windows.Forms.TabPage();
            this.fpSpread3 = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.sheetView3 = new FarPoint.Win.Spread.SheetView();
            this.tpSpecialDrug = new System.Windows.Forms.TabPage();
            this.fpSpread4 = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.sheetView4 = new FarPoint.Win.Spread.SheetView();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.neuTabControl1.SuspendLayout();
            this.tpTypeLimit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetView3)).BeginInit();
            this.tpSpecialDrug.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetView4)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.nbtReturn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 476);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(505, 43);
            this.panel1.TabIndex = 19;
            // 
            // nbtReturn
            // 
            this.nbtReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nbtReturn.Location = new System.Drawing.Point(409, 6);
            this.nbtReturn.Name = "nbtReturn";
            this.nbtReturn.Size = new System.Drawing.Size(75, 23);
            this.nbtReturn.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nbtReturn.TabIndex = 10;
            this.nbtReturn.Text = "返回";
            this.nbtReturn.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.nbtReturn.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel5.Controls.Add(this.nbtSave);
            this.panel5.Controls.Add(this.nbtAdd);
            this.panel5.Controls.Add(this.nbtDelete);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 436);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(505, 40);
            this.panel5.TabIndex = 25;
            // 
            // nbtSave
            // 
            this.nbtSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nbtSave.Location = new System.Drawing.Point(409, 6);
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
            this.nbtAdd.Location = new System.Drawing.Point(223, 6);
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
            this.nbtDelete.Location = new System.Drawing.Point(316, 6);
            this.nbtDelete.Name = "nbtDelete";
            this.nbtDelete.Size = new System.Drawing.Size(75, 23);
            this.nbtDelete.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nbtDelete.TabIndex = 22;
            this.nbtDelete.Text = "删除";
            this.nbtDelete.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.nbtDelete.UseVisualStyleBackColor = true;
            // 
            // neuTabControl1
            // 
            this.neuTabControl1.Controls.Add(this.tpTypeLimit);
            this.neuTabControl1.Controls.Add(this.tpSpecialDrug);
            this.neuTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuTabControl1.Location = new System.Drawing.Point(0, 0);
            this.neuTabControl1.Name = "neuTabControl1";
            this.neuTabControl1.SelectedIndex = 0;
            this.neuTabControl1.Size = new System.Drawing.Size(505, 436);
            this.neuTabControl1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTabControl1.TabIndex = 26;
            // 
            // tpTypeLimit
            // 
            this.tpTypeLimit.Controls.Add(this.fpSpread3);
            this.tpTypeLimit.Location = new System.Drawing.Point(4, 22);
            this.tpTypeLimit.Name = "tpTypeLimit";
            this.tpTypeLimit.Size = new System.Drawing.Size(497, 410);
            this.tpTypeLimit.TabIndex = 3;
            this.tpTypeLimit.Text = "类别区限";
            this.tpTypeLimit.UseVisualStyleBackColor = true;
            // 
            // fpSpread3
            // 
            this.fpSpread3.About = "3.0.2004.2005";
            this.fpSpread3.AccessibleDescription = "fpSpread3, Sheet1, Row 0, Column 0, P";
            this.fpSpread3.BackColor = System.Drawing.SystemColors.Control;
            this.fpSpread3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread3.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread3.Location = new System.Drawing.Point(0, 0);
            this.fpSpread3.Name = "fpSpread3";
            this.fpSpread3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread3.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.sheetView3});
            this.fpSpread3.Size = new System.Drawing.Size(497, 410);
            this.fpSpread3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpSpread3.TabIndex = 26;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread3.TextTipAppearance = tipAppearance1;
            this.fpSpread3.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // sheetView3
            // 
            this.sheetView3.Reset();
            this.sheetView3.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.sheetView3.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.sheetView3.ColumnCount = 8;
            this.sheetView3.RowCount = 1;
            this.sheetView3.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247))))), System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(217)))), ((int)(((byte)(217))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, false);
            this.sheetView3.Cells.Get(0, 0).Value = "P";
            this.sheetView3.Cells.Get(0, 1).Value = "西药";
            this.sheetView3.Cells.Get(0, 2).Value = "购入价";
            this.sheetView3.Cells.Get(0, 3).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.sheetView3.Cells.Get(0, 3).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.sheetView3.Cells.Get(0, 3).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.sheetView3.Cells.Get(0, 3).ParseFormatString = "n";
            this.sheetView3.Cells.Get(0, 3).Value = 0;
            this.sheetView3.Cells.Get(0, 4).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.sheetView3.Cells.Get(0, 4).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.sheetView3.Cells.Get(0, 4).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.sheetView3.Cells.Get(0, 4).ParseFormatString = "n";
            this.sheetView3.Cells.Get(0, 4).Value = 100000;
            this.sheetView3.Cells.Get(0, 5).Value = "购入价*1.15";
            this.sheetView3.Cells.Get(0, 6).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.sheetView3.Cells.Get(0, 6).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.sheetView3.Cells.Get(0, 6).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.sheetView3.Cells.Get(0, 6).ParseFormatString = "n";
            this.sheetView3.Cells.Get(0, 6).Value = 100000;
            this.sheetView3.ColumnHeader.Cells.Get(0, 0).Value = "类别编码";
            this.sheetView3.ColumnHeader.Cells.Get(0, 1).Value = "类别名称";
            this.sheetView3.ColumnHeader.Cells.Get(0, 2).Value = "价格形式";
            this.sheetView3.ColumnHeader.Cells.Get(0, 3).Value = "价格下限";
            this.sheetView3.ColumnHeader.Cells.Get(0, 4).Value = "价格上限";
            this.sheetView3.ColumnHeader.Cells.Get(0, 5).Value = "公式";
            this.sheetView3.ColumnHeader.Cells.Get(0, 6).Value = "基药类型";
            this.sheetView3.ColumnHeader.Cells.Get(0, 7).Value = "应用";
            this.sheetView3.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.sheetView3.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sheetView3.ColumnHeader.DefaultStyle.Locked = false;
            this.sheetView3.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.sheetView3.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView3.Columns.Get(0).Label = "类别编码";
            this.sheetView3.Columns.Get(0).Width = 55F;
            this.sheetView3.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView3.Columns.Get(1).Label = "类别名称";
            this.sheetView3.Columns.Get(1).Width = 57F;
            this.sheetView3.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView3.Columns.Get(2).Label = "价格形式";
            this.sheetView3.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView3.Columns.Get(3).Label = "价格下限";
            this.sheetView3.Columns.Get(3).Width = 59F;
            this.sheetView3.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView3.Columns.Get(4).Label = "价格上限";
            this.sheetView3.Columns.Get(4).Width = 59F;
            this.sheetView3.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView3.Columns.Get(5).Label = "公式";
            this.sheetView3.Columns.Get(5).Width = 105F;
            this.sheetView3.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView3.Columns.Get(6).Label = "基药类型";
            this.sheetView3.Columns.Get(6).Width = 59F;
            this.sheetView3.Columns.Get(7).CellType = checkBoxCellType1;
            this.sheetView3.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView3.Columns.Get(7).Label = "应用";
            this.sheetView3.Columns.Get(7).Width = 38F;
            this.sheetView3.DefaultStyle.Locked = false;
            this.sheetView3.DefaultStyle.Parent = "DataAreaDefault";
            this.sheetView3.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetView3.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.sheetView3.RowHeader.Columns.Default.Resizable = false;
            this.sheetView3.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.sheetView3.RowHeader.DefaultStyle.Locked = false;
            this.sheetView3.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.sheetView3.RowHeader.Visible = false;
            this.sheetView3.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.sheetView3.SelectionStyle = FarPoint.Win.Spread.SelectionStyles.Both;
            this.sheetView3.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.sheetView3.SheetCornerStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.sheetView3.SheetCornerStyle.Locked = false;
            this.sheetView3.SheetCornerStyle.Parent = "HeaderDefault";
            this.sheetView3.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.sheetView3.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // tpSpecialDrug
            // 
            this.tpSpecialDrug.Controls.Add(this.fpSpread4);
            this.tpSpecialDrug.Location = new System.Drawing.Point(4, 22);
            this.tpSpecialDrug.Name = "tpSpecialDrug";
            this.tpSpecialDrug.Padding = new System.Windows.Forms.Padding(3);
            this.tpSpecialDrug.Size = new System.Drawing.Size(474, 410);
            this.tpSpecialDrug.TabIndex = 1;
            this.tpSpecialDrug.Text = "特殊药品";
            this.tpSpecialDrug.UseVisualStyleBackColor = true;
            // 
            // fpSpread4
            // 
            this.fpSpread4.About = "3.0.2004.2005";
            this.fpSpread4.AccessibleDescription = "fpSpread4, Sheet1, Row 0, Column 0, ";
            this.fpSpread4.BackColor = System.Drawing.SystemColors.Control;
            this.fpSpread4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread4.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread4.Location = new System.Drawing.Point(3, 3);
            this.fpSpread4.Name = "fpSpread4";
            this.fpSpread4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread4.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.sheetView4});
            this.fpSpread4.Size = new System.Drawing.Size(468, 404);
            this.fpSpread4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpSpread4.TabIndex = 26;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread4.TextTipAppearance = tipAppearance2;
            this.fpSpread4.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // sheetView4
            // 
            this.sheetView4.Reset();
            this.sheetView4.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.sheetView4.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.sheetView4.ColumnCount = 5;
            this.sheetView4.RowCount = 0;
            this.sheetView4.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247))))), System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(217)))), ((int)(((byte)(217))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, false);
            this.sheetView4.ColumnHeader.Cells.Get(0, 0).Value = "编码";
            this.sheetView4.ColumnHeader.Cells.Get(0, 1).Value = "自定义";
            this.sheetView4.ColumnHeader.Cells.Get(0, 2).Value = "名称[规格]";
            this.sheetView4.ColumnHeader.Cells.Get(0, 3).Value = "公式";
            this.sheetView4.ColumnHeader.Cells.Get(0, 4).Value = "应用";
            this.sheetView4.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.sheetView4.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sheetView4.ColumnHeader.DefaultStyle.Locked = false;
            this.sheetView4.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.sheetView4.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView4.Columns.Get(0).Label = "编码";
            this.sheetView4.Columns.Get(0).Width = 0F;
            this.sheetView4.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView4.Columns.Get(1).Label = "自定义";
            this.sheetView4.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.sheetView4.Columns.Get(2).Label = "名称[规格]";
            this.sheetView4.Columns.Get(2).Width = 220F;
            this.sheetView4.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView4.Columns.Get(3).Label = "公式";
            this.sheetView4.Columns.Get(3).Width = 106F;
            this.sheetView4.Columns.Get(4).CellType = checkBoxCellType2;
            this.sheetView4.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView4.Columns.Get(4).Label = "应用";
            this.sheetView4.Columns.Get(4).Width = 40F;
            this.sheetView4.DefaultStyle.Locked = false;
            this.sheetView4.DefaultStyle.Parent = "DataAreaDefault";
            this.sheetView4.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetView4.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.sheetView4.RowHeader.Columns.Default.Resizable = false;
            this.sheetView4.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.sheetView4.RowHeader.DefaultStyle.Locked = false;
            this.sheetView4.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.sheetView4.RowHeader.Visible = false;
            this.sheetView4.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.sheetView4.SelectionStyle = FarPoint.Win.Spread.SelectionStyles.Both;
            this.sheetView4.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.sheetView4.SheetCornerStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.sheetView4.SheetCornerStyle.Locked = false;
            this.sheetView4.SheetCornerStyle.Parent = "HeaderDefault";
            this.sheetView4.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.sheetView4.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpSpread4.SetActiveViewport(0, 1, 0);
            // 
            // frmSetRetailPrice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 519);
            this.Controls.Add(this.neuTabControl1);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetRetailPrice";
            this.Text = "零售价调价公式设置";
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.neuTabControl1.ResumeLayout(false);
            this.tpTypeLimit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetView3)).EndInit();
            this.tpSpecialDrug.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetView4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel5;
        private FS.FrameWork.WinForms.Controls.NeuButton nbtAdd;
        private FS.FrameWork.WinForms.Controls.NeuButton nbtDelete;
        private FS.FrameWork.WinForms.Controls.NeuTabControl neuTabControl1;
        private System.Windows.Forms.TabPage tpTypeLimit;
        private FS.SOC.Windows.Forms.FpSpread fpSpread3;
        private FarPoint.Win.Spread.SheetView sheetView3;
        private System.Windows.Forms.TabPage tpSpecialDrug;
        private FS.SOC.Windows.Forms.FpSpread fpSpread4;
        private FarPoint.Win.Spread.SheetView sheetView4;
        private FS.FrameWork.WinForms.Controls.NeuButton nbtSave;
        private FS.FrameWork.WinForms.Controls.NeuButton nbtReturn;
    }
}