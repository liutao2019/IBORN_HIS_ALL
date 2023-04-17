namespace FS.SOC.HISFC.InpatientFee.Components.Balance.SIBalance
{
    partial class ucBalance
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
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.TipAppearance tipAppearance3 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ucInPatientInfo1 = new FS.SOC.HISFC.InpatientFee.Components.Balance.SIBalance.ucInPatientInfo();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.fpPatientList = new FarPoint.Win.Spread.FpSpread();
            this.fpPatientList_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.fpUploadList = new FarPoint.Win.Spread.FpSpread();
            this.fpUploadList_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.neuPanel4 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUploadFeeDetailFilter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.fpUnuploadList = new FarPoint.Win.Spread.FpSpread();
            this.fpUnuploadList_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel5 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpPatientList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPatientList_Sheet1)).BeginInit();
            this.neuPanel1.SuspendLayout();
            this.neuPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpUploadList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpUploadList_Sheet1)).BeginInit();
            this.panel2.SuspendLayout();
            this.neuPanel4.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpUnuploadList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpUnuploadList_Sheet1)).BeginInit();
            this.neuPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ucInPatientInfo1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(994, 119);
            this.panel1.TabIndex = 0;
            // 
            // ucInPatientInfo1
            // 
            this.ucInPatientInfo1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.ucInPatientInfo1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucInPatientInfo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucInPatientInfo1.IsFullConvertToHalf = true;
            this.ucInPatientInfo1.IsPrint = false;
            this.ucInPatientInfo1.Location = new System.Drawing.Point(0, 0);
            this.ucInPatientInfo1.Name = "ucInPatientInfo1";
            this.ucInPatientInfo1.ParentFormToolBar = null;
            this.ucInPatientInfo1.Size = new System.Drawing.Size(994, 119);
            this.ucInPatientInfo1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.fpPatientList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.neuPanel1);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(1366, 524);
            this.splitContainer1.SplitterDistance = 368;
            this.splitContainer1.TabIndex = 2;
            // 
            // fpPatientList
            // 
            this.fpPatientList.About = "3.0.2004.2005";
            this.fpPatientList.AccessibleDescription = "fpPatientList, Sheet1, Row 0, Column 0, ";
            this.fpPatientList.BackColor = System.Drawing.SystemColors.Control;
            this.fpPatientList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpPatientList.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpPatientList.Location = new System.Drawing.Point(0, 0);
            this.fpPatientList.Name = "fpPatientList";
            this.fpPatientList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpPatientList.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpPatientList_Sheet1});
            this.fpPatientList.Size = new System.Drawing.Size(368, 524);
            this.fpPatientList.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpPatientList.TextTipAppearance = tipAppearance1;
            this.fpPatientList.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpPatientList.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpPatientList_CellDoubleClick);
            // 
            // fpPatientList_Sheet1
            // 
            this.fpPatientList_Sheet1.Reset();
            this.fpPatientList_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpPatientList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpPatientList_Sheet1.ColumnCount = 6;
            this.fpPatientList_Sheet1.RowCount = 0;
            this.fpPatientList_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "就诊流水号";
            this.fpPatientList_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "结算类型编码";
            this.fpPatientList_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "住院号";
            this.fpPatientList_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "姓名";
            this.fpPatientList_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "住院科室";
            this.fpPatientList_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "出院时间";
            this.fpPatientList_Sheet1.Columns.Get(0).Label = "就诊流水号";
            this.fpPatientList_Sheet1.Columns.Get(0).Visible = false;
            this.fpPatientList_Sheet1.Columns.Get(1).Label = "结算类型编码";
            this.fpPatientList_Sheet1.Columns.Get(1).Visible = false;
            this.fpPatientList_Sheet1.Columns.Get(2).Label = "住院号";
            this.fpPatientList_Sheet1.Columns.Get(2).Width = 75F;
            this.fpPatientList_Sheet1.Columns.Get(3).Label = "姓名";
            this.fpPatientList_Sheet1.Columns.Get(3).Width = 80F;
            this.fpPatientList_Sheet1.Columns.Get(4).Label = "住院科室";
            this.fpPatientList_Sheet1.Columns.Get(4).Width = 84F;
            this.fpPatientList_Sheet1.Columns.Get(5).Label = "出院时间";
            this.fpPatientList_Sheet1.Columns.Get(5).Width = 69F;
            this.fpPatientList_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
            this.fpPatientList_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpPatientList_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpPatientList_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpPatientList_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpPatientList_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpPatientList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpPatientList.SetActiveViewport(0, 1, 0);
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.neuPanel3);
            this.neuPanel1.Controls.Add(this.neuPanel2);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel1.Location = new System.Drawing.Point(0, 119);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(994, 405);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 1;
            // 
            // neuPanel3
            // 
            this.neuPanel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.neuPanel3.Controls.Add(this.fpUploadList);
            this.neuPanel3.Controls.Add(this.panel2);
            this.neuPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel3.Location = new System.Drawing.Point(0, 0);
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size(994, 183);
            this.neuPanel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 1;
            // 
            // fpUploadList
            // 
            this.fpUploadList.About = "3.0.2004.2005";
            this.fpUploadList.AccessibleDescription = "fpUploadList, Sheet1";
            this.fpUploadList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.fpUploadList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpUploadList.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpUploadList.Location = new System.Drawing.Point(0, 52);
            this.fpUploadList.Name = "fpUploadList";
            this.fpUploadList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpUploadList.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpUploadList_Sheet1});
            this.fpUploadList.Size = new System.Drawing.Size(992, 129);
            this.fpUploadList.TabIndex = 2;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpUploadList.TextTipAppearance = tipAppearance2;
            this.fpUploadList.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpUploadList.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpUploadList_CellDoubleClick);
            // 
            // fpUploadList_Sheet1
            // 
            this.fpUploadList_Sheet1.Reset();
            this.fpUploadList_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpUploadList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpUploadList_Sheet1.ColumnCount = 19;
            this.fpUploadList_Sheet1.RowCount = 0;
            this.fpUploadList_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "住院流水号";
            this.fpUploadList_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "结算发票号";
            this.fpUploadList_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "费用日期";
            this.fpUploadList_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "项目序号";
            this.fpUploadList_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "医院项目编码";
            this.fpUploadList_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "医院项目名称";
            this.fpUploadList_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "医保分类代码";
            this.fpUploadList_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "规格";
            this.fpUploadList_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "剂型";
            this.fpUploadList_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "单价";
            this.fpUploadList_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "数量";
            this.fpUploadList_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "金额";
            this.fpUploadList_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "操作时间";
            this.fpUploadList_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "备注2";
            this.fpUploadList_Sheet1.ColumnHeader.Cells.Get(0, 14).Value = "备注3";
            this.fpUploadList_Sheet1.ColumnHeader.Cells.Get(0, 15).Value = "读入标记";
            this.fpUploadList_Sheet1.ColumnHeader.Cells.Get(0, 16).Value = "药品来源";
            this.fpUploadList_Sheet1.ColumnHeader.Cells.Get(0, 17).Value = "费用类别编码";
            this.fpUploadList_Sheet1.ColumnHeader.Cells.Get(0, 18).Value = "上传标记";
            this.fpUploadList_Sheet1.Columns.Get(0).Label = "住院流水号";
            this.fpUploadList_Sheet1.Columns.Get(0).Visible = false;
            this.fpUploadList_Sheet1.Columns.Get(1).Label = "结算发票号";
            this.fpUploadList_Sheet1.Columns.Get(1).Width = 80F;
            this.fpUploadList_Sheet1.Columns.Get(2).Label = "费用日期";
            this.fpUploadList_Sheet1.Columns.Get(2).Width = 140F;
            this.fpUploadList_Sheet1.Columns.Get(3).Label = "项目序号";
            this.fpUploadList_Sheet1.Columns.Get(3).Width = 110F;
            this.fpUploadList_Sheet1.Columns.Get(4).Label = "医院项目编码";
            this.fpUploadList_Sheet1.Columns.Get(4).Width = 100F;
            this.fpUploadList_Sheet1.Columns.Get(5).Label = "医院项目名称";
            this.fpUploadList_Sheet1.Columns.Get(5).Width = 240F;
            this.fpUploadList_Sheet1.Columns.Get(6).Label = "医保分类代码";
            this.fpUploadList_Sheet1.Columns.Get(6).Visible = false;
            this.fpUploadList_Sheet1.Columns.Get(7).Label = "规格";
            this.fpUploadList_Sheet1.Columns.Get(7).Width = 129F;
            this.fpUploadList_Sheet1.Columns.Get(8).Label = "剂型";
            this.fpUploadList_Sheet1.Columns.Get(8).Width = 80F;
            this.fpUploadList_Sheet1.Columns.Get(11).Label = "金额";
            this.fpUploadList_Sheet1.Columns.Get(11).Width = 80F;
            this.fpUploadList_Sheet1.Columns.Get(12).Label = "操作时间";
            this.fpUploadList_Sheet1.Columns.Get(12).Width = 80F;
            this.fpUploadList_Sheet1.Columns.Get(13).Label = "备注2";
            this.fpUploadList_Sheet1.Columns.Get(13).Visible = false;
            this.fpUploadList_Sheet1.Columns.Get(14).Label = "备注3";
            this.fpUploadList_Sheet1.Columns.Get(14).Visible = false;
            this.fpUploadList_Sheet1.Columns.Get(15).Label = "读入标记";
            this.fpUploadList_Sheet1.Columns.Get(15).Visible = false;
            this.fpUploadList_Sheet1.Columns.Get(16).Label = "药品来源";
            this.fpUploadList_Sheet1.Columns.Get(16).Visible = false;
            this.fpUploadList_Sheet1.Columns.Get(17).Label = "费用类别编码";
            this.fpUploadList_Sheet1.Columns.Get(17).Visible = false;
            this.fpUploadList_Sheet1.Columns.Get(17).Width = 80F;
            this.fpUploadList_Sheet1.Columns.Get(18).Label = "上传标记";
            this.fpUploadList_Sheet1.Columns.Get(18).Width = 80F;
            this.fpUploadList_Sheet1.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpUploadList_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpUploadList_Sheet1.DefaultStyle.CellType = textCellType1;
            this.fpUploadList_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
            this.fpUploadList_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpUploadList_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpUploadList_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpUploadList_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpUploadList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpUploadList.SetViewportLeftColumn(0, 0, 1);
            this.fpUploadList.SetActiveViewport(0, 1, 0);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.neuPanel4);
            this.panel2.Controls.Add(this.txtUploadFeeDetailFilter);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(992, 52);
            this.panel2.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(574, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "总金额：0";
            // 
            // neuPanel4
            // 
            this.neuPanel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.neuPanel4.Controls.Add(this.label2);
            this.neuPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel4.Location = new System.Drawing.Point(0, 0);
            this.neuPanel4.Name = "neuPanel4";
            this.neuPanel4.Size = new System.Drawing.Size(992, 20);
            this.neuPanel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel4.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(3, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "待上传费用明细：";
            // 
            // txtUploadFeeDetailFilter
            // 
            this.txtUploadFeeDetailFilter.Location = new System.Drawing.Point(195, 25);
            this.txtUploadFeeDetailFilter.Name = "txtUploadFeeDetailFilter";
            this.txtUploadFeeDetailFilter.Size = new System.Drawing.Size(360, 21);
            this.txtUploadFeeDetailFilter.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(4, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "医院项目编码/医院项目名称过滤:";
            // 
            // neuPanel2
            // 
            this.neuPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.neuPanel2.Controls.Add(this.fpUnuploadList);
            this.neuPanel2.Controls.Add(this.neuPanel5);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuPanel2.Location = new System.Drawing.Point(0, 183);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(994, 222);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 0;
            // 
            // fpUnuploadList
            // 
            this.fpUnuploadList.About = "3.0.2004.2005";
            this.fpUnuploadList.AccessibleDescription = "fpUnuploadList, Sheet1";
            this.fpUnuploadList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.fpUnuploadList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpUnuploadList.Location = new System.Drawing.Point(0, 20);
            this.fpUnuploadList.Name = "fpUnuploadList";
            this.fpUnuploadList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpUnuploadList.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpUnuploadList_Sheet1});
            this.fpUnuploadList.Size = new System.Drawing.Size(992, 200);
            this.fpUnuploadList.TabIndex = 5;
            tipAppearance3.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance3.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpUnuploadList.TextTipAppearance = tipAppearance3;
            this.fpUnuploadList.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpUnuploadList.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpUnuploadList_CellDoubleClick);
            // 
            // fpUnuploadList_Sheet1
            // 
            this.fpUnuploadList_Sheet1.Reset();
            this.fpUnuploadList_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpUnuploadList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpUnuploadList_Sheet1.ColumnCount = 19;
            this.fpUnuploadList_Sheet1.RowCount = 0;
            this.fpUnuploadList_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "住院流水号";
            this.fpUnuploadList_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "结算发票号";
            this.fpUnuploadList_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "费用日期";
            this.fpUnuploadList_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "项目序号";
            this.fpUnuploadList_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "医院项目编码";
            this.fpUnuploadList_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "医院项目名称";
            this.fpUnuploadList_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "医保分类代码";
            this.fpUnuploadList_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "规格";
            this.fpUnuploadList_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "剂型";
            this.fpUnuploadList_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "单价";
            this.fpUnuploadList_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "数量";
            this.fpUnuploadList_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "金额";
            this.fpUnuploadList_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "操作时间";
            this.fpUnuploadList_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "备注2";
            this.fpUnuploadList_Sheet1.ColumnHeader.Cells.Get(0, 14).Value = "备注3";
            this.fpUnuploadList_Sheet1.ColumnHeader.Cells.Get(0, 15).Value = "读入标记";
            this.fpUnuploadList_Sheet1.ColumnHeader.Cells.Get(0, 16).Value = "药品来源";
            this.fpUnuploadList_Sheet1.ColumnHeader.Cells.Get(0, 17).Value = "费用类别编码";
            this.fpUnuploadList_Sheet1.ColumnHeader.Cells.Get(0, 18).Value = "上传标记";
            this.fpUnuploadList_Sheet1.Columns.Get(0).Label = "住院流水号";
            this.fpUnuploadList_Sheet1.Columns.Get(0).Visible = false;
            this.fpUnuploadList_Sheet1.Columns.Get(1).Label = "结算发票号";
            this.fpUnuploadList_Sheet1.Columns.Get(1).Width = 80F;
            this.fpUnuploadList_Sheet1.Columns.Get(2).Label = "费用日期";
            this.fpUnuploadList_Sheet1.Columns.Get(2).Width = 140F;
            this.fpUnuploadList_Sheet1.Columns.Get(3).Label = "项目序号";
            this.fpUnuploadList_Sheet1.Columns.Get(3).Width = 110F;
            this.fpUnuploadList_Sheet1.Columns.Get(4).Label = "医院项目编码";
            this.fpUnuploadList_Sheet1.Columns.Get(4).Width = 100F;
            this.fpUnuploadList_Sheet1.Columns.Get(5).Label = "医院项目名称";
            this.fpUnuploadList_Sheet1.Columns.Get(5).Width = 240F;
            this.fpUnuploadList_Sheet1.Columns.Get(6).Label = "医保分类代码";
            this.fpUnuploadList_Sheet1.Columns.Get(6).Visible = false;
            this.fpUnuploadList_Sheet1.Columns.Get(7).Label = "规格";
            this.fpUnuploadList_Sheet1.Columns.Get(7).Width = 120F;
            this.fpUnuploadList_Sheet1.Columns.Get(8).Label = "剂型";
            this.fpUnuploadList_Sheet1.Columns.Get(8).Width = 80F;
            this.fpUnuploadList_Sheet1.Columns.Get(11).Label = "金额";
            this.fpUnuploadList_Sheet1.Columns.Get(11).Width = 80F;
            this.fpUnuploadList_Sheet1.Columns.Get(12).Label = "操作时间";
            this.fpUnuploadList_Sheet1.Columns.Get(12).Width = 80F;
            this.fpUnuploadList_Sheet1.Columns.Get(13).Label = "备注2";
            this.fpUnuploadList_Sheet1.Columns.Get(13).Visible = false;
            this.fpUnuploadList_Sheet1.Columns.Get(14).Label = "备注3";
            this.fpUnuploadList_Sheet1.Columns.Get(14).Visible = false;
            this.fpUnuploadList_Sheet1.Columns.Get(15).Label = "读入标记";
            this.fpUnuploadList_Sheet1.Columns.Get(15).Visible = false;
            this.fpUnuploadList_Sheet1.Columns.Get(16).Label = "药品来源";
            this.fpUnuploadList_Sheet1.Columns.Get(16).Visible = false;
            this.fpUnuploadList_Sheet1.Columns.Get(17).Label = "费用类别编码";
            this.fpUnuploadList_Sheet1.Columns.Get(17).Visible = false;
            this.fpUnuploadList_Sheet1.Columns.Get(17).Width = 80F;
            this.fpUnuploadList_Sheet1.Columns.Get(18).Label = "上传标记";
            this.fpUnuploadList_Sheet1.Columns.Get(18).Width = 80F;
            this.fpUnuploadList_Sheet1.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpUnuploadList_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpUnuploadList_Sheet1.DefaultStyle.CellType = textCellType1;
            this.fpUnuploadList_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
            this.fpUnuploadList_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpUnuploadList_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpUnuploadList_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpUnuploadList_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpUnuploadList_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpUnuploadList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpUnuploadList.SetViewportLeftColumn(0, 0, 1);
            this.fpUnuploadList.SetActiveViewport(0, 1, 0);
            // 
            // neuPanel5
            // 
            this.neuPanel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.neuPanel5.Controls.Add(this.label3);
            this.neuPanel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel5.Location = new System.Drawing.Point(0, 0);
            this.neuPanel5.Name = "neuPanel5";
            this.neuPanel5.Size = new System.Drawing.Size(992, 20);
            this.neuPanel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel5.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(3, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "过滤不上传的费用明细：";
            // 
            // ucBalance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ucBalance";
            this.Size = new System.Drawing.Size(1366, 524);
            this.Load += new System.EventHandler(this.ucUpLoadFeeDetail_Load);
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpPatientList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPatientList_Sheet1)).EndInit();
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpUploadList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpUploadList_Sheet1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.neuPanel4.ResumeLayout(false);
            this.neuPanel4.PerformLayout();
            this.neuPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpUnuploadList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpUnuploadList_Sheet1)).EndInit();
            this.neuPanel5.ResumeLayout(false);
            this.neuPanel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private FarPoint.Win.Spread.FpSpread fpPatientList;
        private FarPoint.Win.Spread.SheetView fpPatientList_Sheet1;
        private ucInPatientInfo ucInPatientInfo1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel3;
        private FarPoint.Win.Spread.FpSpread fpUploadList;
        private FarPoint.Win.Spread.SheetView fpUploadList_Sheet1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtUploadFeeDetailFilter;
        private System.Windows.Forms.Label label1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel4;
        private System.Windows.Forms.Label label2;
        private FarPoint.Win.Spread.FpSpread fpUnuploadList;
        private FarPoint.Win.Spread.SheetView fpUnuploadList_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}
