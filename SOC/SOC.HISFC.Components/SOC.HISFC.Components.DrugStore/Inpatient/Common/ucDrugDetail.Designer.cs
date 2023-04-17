namespace FS.SOC.HISFC.Components.DrugStore.Inpatient.Common
{
    partial class ucDrugDetail
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
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.ComboBoxCellType comboBoxCellType1 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.TipAppearance tipAppearance3 = new FarPoint.Win.Spread.TipAppearance();
            this.neuTabControl1 = new FS.FrameWork.WinForms.Controls.NeuTabControl();
            this.tpDrugMessage = new System.Windows.Forms.TabPage();
            this.neuDrugMessageSpread = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuDrugMessageSpread_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.tpTotDrug = new System.Windows.Forms.TabPage();
            this.neuDrugTotSpread = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuDrugTotSpread_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.tpDetailDrug = new System.Windows.Forms.TabPage();
            this.neuDrugDetailSpread = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuDrugDetailSpread_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.tpTotBill = new System.Windows.Forms.TabPage();
            this.tpDetailBill = new System.Windows.Forms.TabPage();
            this.tpRecipeBill = new System.Windows.Forms.TabPage();
            this.neuTabControl1.SuspendLayout();
            this.tpDrugMessage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuDrugMessageSpread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuDrugMessageSpread_Sheet1)).BeginInit();
            this.tpTotDrug.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuDrugTotSpread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuDrugTotSpread_Sheet1)).BeginInit();
            this.tpDetailDrug.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuDrugDetailSpread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuDrugDetailSpread_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // neuTabControl1
            // 
            this.neuTabControl1.Controls.Add(this.tpDrugMessage);
            this.neuTabControl1.Controls.Add(this.tpTotDrug);
            this.neuTabControl1.Controls.Add(this.tpDetailDrug);
            this.neuTabControl1.Controls.Add(this.tpTotBill);
            this.neuTabControl1.Controls.Add(this.tpDetailBill);
            this.neuTabControl1.Controls.Add(this.tpRecipeBill);
            this.neuTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuTabControl1.Location = new System.Drawing.Point(0, 0);
            this.neuTabControl1.Name = "neuTabControl1";
            this.neuTabControl1.SelectedIndex = 0;
            this.neuTabControl1.Size = new System.Drawing.Size(860, 526);
            this.neuTabControl1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTabControl1.TabIndex = 4;
            // 
            // tpDrugMessage
            // 
            this.tpDrugMessage.Controls.Add(this.neuDrugMessageSpread);
            this.tpDrugMessage.Location = new System.Drawing.Point(4, 22);
            this.tpDrugMessage.Name = "tpDrugMessage";
            this.tpDrugMessage.Size = new System.Drawing.Size(852, 500);
            this.tpDrugMessage.TabIndex = 2;
            this.tpDrugMessage.Text = "摆药通知";
            this.tpDrugMessage.UseVisualStyleBackColor = true;
            // 
            // neuDrugMessageSpread
            // 
            this.neuDrugMessageSpread.About = "3.0.2004.2005";
            this.neuDrugMessageSpread.AccessibleDescription = "neuDrugMessageSpread, Sheet1, Row 0, Column 0, ";
            this.neuDrugMessageSpread.BackColor = System.Drawing.SystemColors.Control;
            this.neuDrugMessageSpread.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuDrugMessageSpread.FileName = "";
            this.neuDrugMessageSpread.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuDrugMessageSpread.IsAutoSaveGridStatus = false;
            this.neuDrugMessageSpread.IsCanCustomConfigColumn = false;
            this.neuDrugMessageSpread.Location = new System.Drawing.Point(0, 0);
            this.neuDrugMessageSpread.Name = "neuDrugMessageSpread";
            this.neuDrugMessageSpread.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuDrugMessageSpread.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuDrugMessageSpread_Sheet1});
            this.neuDrugMessageSpread.Size = new System.Drawing.Size(852, 500);
            this.neuDrugMessageSpread.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuDrugMessageSpread.TabIndex = 1;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuDrugMessageSpread.TextTipAppearance = tipAppearance1;
            this.neuDrugMessageSpread.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // neuDrugMessageSpread_Sheet1
            // 
            this.neuDrugMessageSpread_Sheet1.Reset();
            this.neuDrugMessageSpread_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuDrugMessageSpread_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuDrugMessageSpread_Sheet1.ColumnCount = 7;
            this.neuDrugMessageSpread_Sheet1.RowCount = 0;
            this.neuDrugMessageSpread_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247))))), System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(217)))), ((int)(((byte)(217))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, false);
            this.neuDrugMessageSpread_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "选中";
            this.neuDrugMessageSpread_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "是否打印";
            this.neuDrugMessageSpread_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "发送科室";
            this.neuDrugMessageSpread_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "摆药单类型";
            this.neuDrugMessageSpread_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "发送时间";
            this.neuDrugMessageSpread_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "发送人";
            this.neuDrugMessageSpread_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "发送类型";
            this.neuDrugMessageSpread_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.neuDrugMessageSpread_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuDrugMessageSpread_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.neuDrugMessageSpread_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuDrugMessageSpread_Sheet1.Columns.Get(0).CellType = checkBoxCellType1;
            this.neuDrugMessageSpread_Sheet1.Columns.Get(0).Label = "选中";
            this.neuDrugMessageSpread_Sheet1.Columns.Get(0).Width = 47F;
            comboBoxCellType1.ButtonAlign = FarPoint.Win.ButtonAlign.Right;
            comboBoxCellType1.Items = new string[] {
        "打印",
        "预览"};
            this.neuDrugMessageSpread_Sheet1.Columns.Get(1).CellType = comboBoxCellType1;
            this.neuDrugMessageSpread_Sheet1.Columns.Get(1).Label = "是否打印";
            this.neuDrugMessageSpread_Sheet1.Columns.Get(1).Width = 0F;
            this.neuDrugMessageSpread_Sheet1.Columns.Get(2).Label = "发送科室";
            this.neuDrugMessageSpread_Sheet1.Columns.Get(2).Locked = true;
            this.neuDrugMessageSpread_Sheet1.Columns.Get(2).Width = 150F;
            this.neuDrugMessageSpread_Sheet1.Columns.Get(3).Label = "摆药单类型";
            this.neuDrugMessageSpread_Sheet1.Columns.Get(3).Locked = true;
            this.neuDrugMessageSpread_Sheet1.Columns.Get(3).Width = 174F;
            this.neuDrugMessageSpread_Sheet1.Columns.Get(4).Label = "发送时间";
            this.neuDrugMessageSpread_Sheet1.Columns.Get(4).Locked = true;
            this.neuDrugMessageSpread_Sheet1.Columns.Get(4).Width = 159F;
            this.neuDrugMessageSpread_Sheet1.Columns.Get(5).Label = "发送人";
            this.neuDrugMessageSpread_Sheet1.Columns.Get(5).Locked = true;
            this.neuDrugMessageSpread_Sheet1.Columns.Get(5).Width = 79F;
            this.neuDrugMessageSpread_Sheet1.Columns.Get(6).Label = "发送类型";
            this.neuDrugMessageSpread_Sheet1.Columns.Get(6).Locked = true;
            this.neuDrugMessageSpread_Sheet1.Columns.Get(6).Width = 79F;
            this.neuDrugMessageSpread_Sheet1.DefaultStyle.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuDrugMessageSpread_Sheet1.DefaultStyle.Locked = false;
            this.neuDrugMessageSpread_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.neuDrugMessageSpread_Sheet1.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuDrugMessageSpread_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.neuDrugMessageSpread_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuDrugMessageSpread_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.neuDrugMessageSpread_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.neuDrugMessageSpread_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuDrugMessageSpread_Sheet1.RowHeader.Visible = false;
            this.neuDrugMessageSpread_Sheet1.Rows.Default.Height = 25F;
            this.neuDrugMessageSpread_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.neuDrugMessageSpread_Sheet1.SheetCornerStyle.Locked = false;
            this.neuDrugMessageSpread_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuDrugMessageSpread_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuDrugMessageSpread_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuDrugMessageSpread.SetActiveViewport(0, 1, 0);
            // 
            // tpTotDrug
            // 
            this.tpTotDrug.Controls.Add(this.neuDrugTotSpread);
            this.tpTotDrug.Location = new System.Drawing.Point(4, 22);
            this.tpTotDrug.Name = "tpTotDrug";
            this.tpTotDrug.Padding = new System.Windows.Forms.Padding(3);
            this.tpTotDrug.Size = new System.Drawing.Size(852, 500);
            this.tpTotDrug.TabIndex = 1;
            this.tpTotDrug.Text = "总量摆药";
            this.tpTotDrug.UseVisualStyleBackColor = true;
            // 
            // neuDrugTotSpread
            // 
            this.neuDrugTotSpread.About = "3.0.2004.2005";
            this.neuDrugTotSpread.AccessibleDescription = "neuDrugTotSpread, Sheet1, Row 0, Column 0, ";
            this.neuDrugTotSpread.BackColor = System.Drawing.Color.Transparent;
            this.neuDrugTotSpread.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuDrugTotSpread.FileName = "";
            this.neuDrugTotSpread.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuDrugTotSpread.IsAutoSaveGridStatus = false;
            this.neuDrugTotSpread.IsCanCustomConfigColumn = false;
            this.neuDrugTotSpread.Location = new System.Drawing.Point(3, 3);
            this.neuDrugTotSpread.Name = "neuDrugTotSpread";
            this.neuDrugTotSpread.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuDrugTotSpread.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuDrugTotSpread_Sheet1});
            this.neuDrugTotSpread.Size = new System.Drawing.Size(846, 494);
            this.neuDrugTotSpread.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuDrugTotSpread.TabIndex = 2;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuDrugTotSpread.TextTipAppearance = tipAppearance2;
            this.neuDrugTotSpread.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // neuDrugTotSpread_Sheet1
            // 
            this.neuDrugTotSpread_Sheet1.Reset();
            this.neuDrugTotSpread_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuDrugTotSpread_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuDrugTotSpread_Sheet1.ColumnCount = 0;
            this.neuDrugTotSpread_Sheet1.RowCount = 1;
            this.neuDrugTotSpread_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("FpDetail", System.Drawing.Color.Honeydew, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Honeydew, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(225)))), ((int)(((byte)(243))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, false);
            this.neuDrugTotSpread_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Honeydew;
            this.neuDrugTotSpread_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.neuDrugTotSpread_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuDrugTotSpread_Sheet1.DefaultStyle.Locked = true;
            this.neuDrugTotSpread_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.neuDrugTotSpread_Sheet1.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuDrugTotSpread_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.neuDrugTotSpread_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuDrugTotSpread_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.neuDrugTotSpread_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Honeydew;
            this.neuDrugTotSpread_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.neuDrugTotSpread_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuDrugTotSpread_Sheet1.RowHeader.Visible = false;
            this.neuDrugTotSpread_Sheet1.Rows.Default.Height = 23F;
            this.neuDrugTotSpread_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.MultiRange;
            this.neuDrugTotSpread_Sheet1.SelectionStyle = FarPoint.Win.Spread.SelectionStyles.SelectionColors;
            this.neuDrugTotSpread_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Honeydew;
            this.neuDrugTotSpread_Sheet1.SheetCornerStyle.Locked = false;
            this.neuDrugTotSpread_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuDrugTotSpread_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuDrugTotSpread_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuDrugTotSpread.SetActiveViewport(0, 0, 1);
            // 
            // tpDetailDrug
            // 
            this.tpDetailDrug.Controls.Add(this.neuDrugDetailSpread);
            this.tpDetailDrug.Location = new System.Drawing.Point(4, 22);
            this.tpDetailDrug.Name = "tpDetailDrug";
            this.tpDetailDrug.Padding = new System.Windows.Forms.Padding(3);
            this.tpDetailDrug.Size = new System.Drawing.Size(852, 500);
            this.tpDetailDrug.TabIndex = 0;
            this.tpDetailDrug.Text = "明细摆药";
            this.tpDetailDrug.UseVisualStyleBackColor = true;
            // 
            // neuDrugDetailSpread
            // 
            this.neuDrugDetailSpread.About = "3.0.2004.2005";
            this.neuDrugDetailSpread.AccessibleDescription = "neuDrugDetailSpread, Sheet1, Row 0, Column 0, ";
            this.neuDrugDetailSpread.BackColor = System.Drawing.Color.Transparent;
            this.neuDrugDetailSpread.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuDrugDetailSpread.FileName = "";
            this.neuDrugDetailSpread.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuDrugDetailSpread.IsAutoSaveGridStatus = false;
            this.neuDrugDetailSpread.IsCanCustomConfigColumn = false;
            this.neuDrugDetailSpread.Location = new System.Drawing.Point(3, 3);
            this.neuDrugDetailSpread.Name = "neuDrugDetailSpread";
            this.neuDrugDetailSpread.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuDrugDetailSpread.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuDrugDetailSpread_Sheet1});
            this.neuDrugDetailSpread.Size = new System.Drawing.Size(846, 494);
            this.neuDrugDetailSpread.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuDrugDetailSpread.TabIndex = 1;
            tipAppearance3.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance3.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuDrugDetailSpread.TextTipAppearance = tipAppearance3;
            this.neuDrugDetailSpread.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // neuDrugDetailSpread_Sheet1
            // 
            this.neuDrugDetailSpread_Sheet1.Reset();
            this.neuDrugDetailSpread_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuDrugDetailSpread_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuDrugDetailSpread_Sheet1.ColumnCount = 0;
            this.neuDrugDetailSpread_Sheet1.RowCount = 0;
            this.neuDrugDetailSpread_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("FpDetail", System.Drawing.Color.Honeydew, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Honeydew, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(225)))), ((int)(((byte)(243))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, false);
            this.neuDrugDetailSpread_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Honeydew;
            this.neuDrugDetailSpread_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.neuDrugDetailSpread_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuDrugDetailSpread_Sheet1.DefaultStyle.Locked = true;
            this.neuDrugDetailSpread_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.neuDrugDetailSpread_Sheet1.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuDrugDetailSpread_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.neuDrugDetailSpread_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuDrugDetailSpread_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Honeydew;
            this.neuDrugDetailSpread_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.neuDrugDetailSpread_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuDrugDetailSpread_Sheet1.RowHeader.Visible = false;
            this.neuDrugDetailSpread_Sheet1.Rows.Default.Height = 22F;
            this.neuDrugDetailSpread_Sheet1.SelectionStyle = FarPoint.Win.Spread.SelectionStyles.SelectionColors;
            this.neuDrugDetailSpread_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Honeydew;
            this.neuDrugDetailSpread_Sheet1.SheetCornerStyle.Locked = false;
            this.neuDrugDetailSpread_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuDrugDetailSpread_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuDrugDetailSpread_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuDrugDetailSpread.SetActiveViewport(0, 1, 1);
            // 
            // tpTotBill
            // 
            this.tpTotBill.Location = new System.Drawing.Point(4, 22);
            this.tpTotBill.Name = "tpTotBill";
            this.tpTotBill.Size = new System.Drawing.Size(852, 500);
            this.tpTotBill.TabIndex = 3;
            this.tpTotBill.Text = "总量摆药单";
            this.tpTotBill.UseVisualStyleBackColor = true;
            // 
            // tpDetailBill
            // 
            this.tpDetailBill.Location = new System.Drawing.Point(4, 22);
            this.tpDetailBill.Name = "tpDetailBill";
            this.tpDetailBill.Size = new System.Drawing.Size(852, 500);
            this.tpDetailBill.TabIndex = 4;
            this.tpDetailBill.Text = "明细摆药单";
            this.tpDetailBill.UseVisualStyleBackColor = true;
            // 
            // tpRecipeBill
            // 
            this.tpRecipeBill.Location = new System.Drawing.Point(4, 22);
            this.tpRecipeBill.Name = "tpRecipeBill";
            this.tpRecipeBill.Size = new System.Drawing.Size(852, 500);
            this.tpRecipeBill.TabIndex = 5;
            this.tpRecipeBill.Text = "住院处方";
            this.tpRecipeBill.UseVisualStyleBackColor = true;
            // 
            // ucDrugDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuTabControl1);
            this.Name = "ucDrugDetail";
            this.Size = new System.Drawing.Size(860, 526);
            this.neuTabControl1.ResumeLayout(false);
            this.tpDrugMessage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuDrugMessageSpread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuDrugMessageSpread_Sheet1)).EndInit();
            this.tpTotDrug.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuDrugTotSpread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuDrugTotSpread_Sheet1)).EndInit();
            this.tpDetailDrug.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuDrugDetailSpread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuDrugDetailSpread_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected FS.FrameWork.WinForms.Controls.NeuTabControl neuTabControl1;
        protected System.Windows.Forms.TabPage tpDetailDrug;
        protected FS.FrameWork.WinForms.Controls.NeuSpread neuDrugDetailSpread;
        protected FarPoint.Win.Spread.SheetView neuDrugDetailSpread_Sheet1;
        protected System.Windows.Forms.TabPage tpTotDrug;
        protected FS.FrameWork.WinForms.Controls.NeuSpread neuDrugTotSpread;
        protected FarPoint.Win.Spread.SheetView neuDrugTotSpread_Sheet1;
        protected System.Windows.Forms.TabPage tpDrugMessage;
        protected FS.FrameWork.WinForms.Controls.NeuSpread neuDrugMessageSpread;
        private FarPoint.Win.Spread.SheetView neuDrugMessageSpread_Sheet1;
        private System.Windows.Forms.TabPage tpTotBill;
        private System.Windows.Forms.TabPage tpDetailBill;
        private System.Windows.Forms.TabPage tpRecipeBill;
    }
}
