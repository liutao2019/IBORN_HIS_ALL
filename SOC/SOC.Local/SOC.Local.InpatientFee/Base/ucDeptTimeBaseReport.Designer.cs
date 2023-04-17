namespace FS.SOC.Local.InpatientFee.Base
{
    partial class ucDeptTimeBaseReport
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
            this.panelAll = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuGroupBox2 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.panelPrint = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.panelDataView = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.fpSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.fpSpread1_Sheet2 = new FarPoint.Win.Spread.SheetView();
            this.panelAdditionTitle = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lbAdditionTitleLeft = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbAdditionTitleRight = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbAdditionTitleMid = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.panelTitle = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lbMainTitle = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nGroupBoxQueryCondition = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.panelQueryConditions = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.panelFilter = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.txtFilter = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.panelTime = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.ncmbTime = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.dtStart = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.dtEnd = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.label2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbTime = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.panelDept = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.cmbDept = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lbDept = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.panelAll.SuspendLayout();
            this.neuGroupBox2.SuspendLayout();
            this.panelPrint.SuspendLayout();
            this.panelDataView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet2)).BeginInit();
            this.panelAdditionTitle.SuspendLayout();
            this.panelTitle.SuspendLayout();
            this.nGroupBoxQueryCondition.SuspendLayout();
            this.panelQueryConditions.SuspendLayout();
            this.panelFilter.SuspendLayout();
            this.panelTime.SuspendLayout();
            this.panelDept.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelAll
            // 
            this.panelAll.Controls.Add(this.neuGroupBox2);
            this.panelAll.Controls.Add(this.nGroupBoxQueryCondition);
            this.panelAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAll.Location = new System.Drawing.Point(0, 0);
            this.panelAll.Name = "panelAll";
            this.panelAll.Size = new System.Drawing.Size(781, 348);
            this.panelAll.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panelAll.TabIndex = 10;
            // 
            // neuGroupBox2
            // 
            this.neuGroupBox2.Controls.Add(this.panelPrint);
            this.neuGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuGroupBox2.Location = new System.Drawing.Point(0, 64);
            this.neuGroupBox2.Name = "neuGroupBox2";
            this.neuGroupBox2.Size = new System.Drawing.Size(781, 284);
            this.neuGroupBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox2.TabIndex = 5;
            this.neuGroupBox2.TabStop = false;
            this.neuGroupBox2.Text = "查询结果";
            // 
            // panelPrint
            // 
            this.panelPrint.Controls.Add(this.panelDataView);
            this.panelPrint.Controls.Add(this.panelAdditionTitle);
            this.panelPrint.Controls.Add(this.panelTitle);
            this.panelPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPrint.Location = new System.Drawing.Point(3, 17);
            this.panelPrint.Name = "panelPrint";
            this.panelPrint.Size = new System.Drawing.Size(775, 264);
            this.panelPrint.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panelPrint.TabIndex = 5;
            // 
            // panelDataView
            // 
            this.panelDataView.Controls.Add(this.fpSpread1);
            this.panelDataView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDataView.Location = new System.Drawing.Point(0, 62);
            this.panelDataView.Name = "panelDataView";
            this.panelDataView.Size = new System.Drawing.Size(775, 202);
            this.panelDataView.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panelDataView.TabIndex = 7;
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "3.0.2004.2005";
            this.fpSpread1.AccessibleDescription = "fpSpread1";
            this.fpSpread1.BackColor = System.Drawing.Color.White;
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread1.FileName = "";
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.IsAutoSaveGridStatus = false;
            this.fpSpread1.IsCanCustomConfigColumn = false;
            this.fpSpread1.Location = new System.Drawing.Point(0, 0);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1,
            this.fpSpread1_Sheet2});
            this.fpSpread1.Size = new System.Drawing.Size(775, 202);
            this.fpSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpSpread1.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread1.TextTipAppearance = tipAppearance1;
            this.fpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "汇总";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.Gray, System.Drawing.Color.White, System.Drawing.Color.Black, System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(223)))), ((int)(((byte)(222))))), FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Black, System.Drawing.SystemColors.ActiveCaption, System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.White, true, true, true, true, true);
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet1.DefaultStyle.Locked = false;
            this.fpSpread1_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 40F;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.SheetCornerStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet1.SheetCornerStyle.Locked = false;
            this.fpSpread1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // fpSpread1_Sheet2
            // 
            this.fpSpread1_Sheet2.Reset();
            this.fpSpread1_Sheet2.SheetName = "明细";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet2.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.SystemColors.AppWorkspace, System.Drawing.Color.White, System.Drawing.Color.Black, System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(223)))), ((int)(((byte)(222))))), FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Black, System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(93)))), ((int)(((byte)(90))))), System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.White, true, true, true, true, true);
            this.fpSpread1_Sheet2.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet2.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpSpread1_Sheet2.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet2.ColumnHeader.DefaultStyle.Locked = false;
            this.fpSpread1_Sheet2.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet2.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet2.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet2.DefaultStyle.Locked = false;
            this.fpSpread1_Sheet2.DefaultStyle.Parent = "DataAreaDefault";
            this.fpSpread1_Sheet2.RowHeader.Columns.Default.Resizable = true;
            this.fpSpread1_Sheet2.RowHeader.Columns.Get(0).Width = 40F;
            this.fpSpread1_Sheet2.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet2.RowHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpSpread1_Sheet2.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet2.RowHeader.DefaultStyle.Locked = false;
            this.fpSpread1_Sheet2.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet2.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet2.SheetCornerStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet2.SheetCornerStyle.Locked = false;
            this.fpSpread1_Sheet2.SheetCornerStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet2.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpSpread1_Sheet2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // panelAdditionTitle
            // 
            this.panelAdditionTitle.BackColor = System.Drawing.Color.White;
            this.panelAdditionTitle.Controls.Add(this.lbAdditionTitleLeft);
            this.panelAdditionTitle.Controls.Add(this.lbAdditionTitleRight);
            this.panelAdditionTitle.Controls.Add(this.lbAdditionTitleMid);
            this.panelAdditionTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelAdditionTitle.Location = new System.Drawing.Point(0, 47);
            this.panelAdditionTitle.Name = "panelAdditionTitle";
            this.panelAdditionTitle.Size = new System.Drawing.Size(775, 15);
            this.panelAdditionTitle.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panelAdditionTitle.TabIndex = 6;
            // 
            // lbAdditionTitleLeft
            // 
            this.lbAdditionTitleLeft.AutoSize = true;
            this.lbAdditionTitleLeft.BackColor = System.Drawing.Color.White;
            this.lbAdditionTitleLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbAdditionTitleLeft.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbAdditionTitleLeft.Location = new System.Drawing.Point(0, 0);
            this.lbAdditionTitleLeft.Name = "lbAdditionTitleLeft";
            this.lbAdditionTitleLeft.Size = new System.Drawing.Size(67, 14);
            this.lbAdditionTitleLeft.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbAdditionTitleLeft.TabIndex = 2;
            this.lbAdditionTitleLeft.Text = "统计时间";
            this.lbAdditionTitleLeft.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbAdditionTitleRight
            // 
            this.lbAdditionTitleRight.AutoSize = true;
            this.lbAdditionTitleRight.BackColor = System.Drawing.Color.White;
            this.lbAdditionTitleRight.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbAdditionTitleRight.Location = new System.Drawing.Point(756, 0);
            this.lbAdditionTitleRight.Name = "lbAdditionTitleRight";
            this.lbAdditionTitleRight.Size = new System.Drawing.Size(0, 14);
            this.lbAdditionTitleRight.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbAdditionTitleRight.TabIndex = 3;
            this.lbAdditionTitleRight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbAdditionTitleMid
            // 
            this.lbAdditionTitleMid.AutoSize = true;
            this.lbAdditionTitleMid.BackColor = System.Drawing.Color.White;
            this.lbAdditionTitleMid.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbAdditionTitleMid.Location = new System.Drawing.Point(262, 0);
            this.lbAdditionTitleMid.Name = "lbAdditionTitleMid";
            this.lbAdditionTitleMid.Size = new System.Drawing.Size(0, 14);
            this.lbAdditionTitleMid.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbAdditionTitleMid.TabIndex = 3;
            this.lbAdditionTitleMid.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelTitle
            // 
            this.panelTitle.BackColor = System.Drawing.Color.White;
            this.panelTitle.Controls.Add(this.lbMainTitle);
            this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitle.Location = new System.Drawing.Point(0, 0);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Size = new System.Drawing.Size(775, 47);
            this.panelTitle.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panelTitle.TabIndex = 3;
            // 
            // lbMainTitle
            // 
            this.lbMainTitle.AutoSize = true;
            this.lbMainTitle.BackColor = System.Drawing.Color.White;
            this.lbMainTitle.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbMainTitle.Location = new System.Drawing.Point(251, 13);
            this.lbMainTitle.Name = "lbMainTitle";
            this.lbMainTitle.Size = new System.Drawing.Size(49, 19);
            this.lbMainTitle.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbMainTitle.TabIndex = 2;
            this.lbMainTitle.Text = "标题";
            this.lbMainTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nGroupBoxQueryCondition
            // 
            this.nGroupBoxQueryCondition.Controls.Add(this.panelQueryConditions);
            this.nGroupBoxQueryCondition.Dock = System.Windows.Forms.DockStyle.Top;
            this.nGroupBoxQueryCondition.Location = new System.Drawing.Point(0, 0);
            this.nGroupBoxQueryCondition.Name = "nGroupBoxQueryCondition";
            this.nGroupBoxQueryCondition.Size = new System.Drawing.Size(781, 64);
            this.nGroupBoxQueryCondition.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nGroupBoxQueryCondition.TabIndex = 4;
            this.nGroupBoxQueryCondition.TabStop = false;
            this.nGroupBoxQueryCondition.Text = "查询条件";
            // 
            // panelQueryConditions
            // 
            this.panelQueryConditions.Controls.Add(this.panelFilter);
            this.panelQueryConditions.Controls.Add(this.panelTime);
            this.panelQueryConditions.Controls.Add(this.panelDept);
            this.panelQueryConditions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelQueryConditions.Location = new System.Drawing.Point(3, 17);
            this.panelQueryConditions.Name = "panelQueryConditions";
            this.panelQueryConditions.Size = new System.Drawing.Size(775, 44);
            this.panelQueryConditions.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panelQueryConditions.TabIndex = 3;
            // 
            // panelFilter
            // 
            this.panelFilter.Controls.Add(this.txtFilter);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelFilter.Location = new System.Drawing.Point(612, 0);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Size = new System.Drawing.Size(156, 44);
            this.panelFilter.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panelFilter.TabIndex = 12;
            // 
            // txtFilter
            // 
            this.txtFilter.IsEnter2Tab = false;
            this.txtFilter.Location = new System.Drawing.Point(6, 11);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(138, 21);
            this.txtFilter.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtFilter.TabIndex = 0;
            // 
            // panelTime
            // 
            this.panelTime.Controls.Add(this.ncmbTime);
            this.panelTime.Controls.Add(this.dtStart);
            this.panelTime.Controls.Add(this.dtEnd);
            this.panelTime.Controls.Add(this.label2);
            this.panelTime.Controls.Add(this.lbTime);
            this.panelTime.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelTime.Location = new System.Drawing.Point(205, 0);
            this.panelTime.Name = "panelTime";
            this.panelTime.Size = new System.Drawing.Size(407, 44);
            this.panelTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panelTime.TabIndex = 11;
            // 
            // ncmbTime
            // 
            this.ncmbTime.ArrowBackColor = System.Drawing.Color.Silver;
            this.ncmbTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ncmbTime.Font = new System.Drawing.Font("宋体", 10F);
            this.ncmbTime.IsEnter2Tab = false;
            this.ncmbTime.IsFlat = false;
            this.ncmbTime.IsLike = true;
            this.ncmbTime.IsListOnly = false;
            this.ncmbTime.IsPopForm = true;
            this.ncmbTime.IsShowCustomerList = false;
            this.ncmbTime.IsShowID = false;
            this.ncmbTime.Location = new System.Drawing.Point(50, 11);
            this.ncmbTime.Name = "ncmbTime";
            this.ncmbTime.PopForm = null;
            this.ncmbTime.ShowCustomerList = false;
            this.ncmbTime.ShowID = false;
            this.ncmbTime.Size = new System.Drawing.Size(349, 21);
            this.ncmbTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.ncmbTime.TabIndex = 23;
            this.ncmbTime.Tag = "";
            this.ncmbTime.ToolBarUse = false;
            // 
            // dtStart
            // 
            this.dtStart.CustomFormat = "yyyy年MM月dd日 HH:mm:ss";
            this.dtStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtStart.IsEnter2Tab = false;
            this.dtStart.Location = new System.Drawing.Point(50, 11);
            this.dtStart.Name = "dtStart";
            this.dtStart.Size = new System.Drawing.Size(165, 21);
            this.dtStart.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtStart.TabIndex = 10;
            // 
            // dtEnd
            // 
            this.dtEnd.CustomFormat = "yyyy年MM月dd日 HH:mm:ss";
            this.dtEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEnd.IsEnter2Tab = false;
            this.dtEnd.Location = new System.Drawing.Point(237, 11);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(162, 21);
            this.dtEnd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtEnd.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(210, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 21);
            this.label2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label2.TabIndex = 12;
            this.label2.Text = "到";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbTime
            // 
            this.lbTime.ForeColor = System.Drawing.Color.Black;
            this.lbTime.Location = new System.Drawing.Point(2, 11);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(48, 21);
            this.lbTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbTime.TabIndex = 11;
            this.lbTime.Text = "时间：";
            this.lbTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelDept
            // 
            this.panelDept.Controls.Add(this.cmbDept);
            this.panelDept.Controls.Add(this.lbDept);
            this.panelDept.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelDept.Location = new System.Drawing.Point(0, 0);
            this.panelDept.Name = "panelDept";
            this.panelDept.Size = new System.Drawing.Size(205, 44);
            this.panelDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panelDept.TabIndex = 10;
            // 
            // cmbDept
            // 
            this.cmbDept.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDept.IsEnter2Tab = false;
            this.cmbDept.IsFlat = false;
            this.cmbDept.IsLike = true;
            this.cmbDept.IsListOnly = false;
            this.cmbDept.IsPopForm = true;
            this.cmbDept.IsShowCustomerList = false;
            this.cmbDept.IsShowID = false;
            this.cmbDept.Location = new System.Drawing.Point(61, 12);
            this.cmbDept.Name = "cmbDept";
            this.cmbDept.PopForm = null;
            this.cmbDept.ShowCustomerList = false;
            this.cmbDept.ShowID = false;
            this.cmbDept.Size = new System.Drawing.Size(136, 20);
            this.cmbDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbDept.TabIndex = 5;
            this.cmbDept.Tag = "";
            this.cmbDept.ToolBarUse = false;
            // 
            // lbDept
            // 
            this.lbDept.ForeColor = System.Drawing.Color.Black;
            this.lbDept.Location = new System.Drawing.Point(8, 12);
            this.lbDept.Name = "lbDept";
            this.lbDept.Size = new System.Drawing.Size(48, 20);
            this.lbDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbDept.TabIndex = 4;
            this.lbDept.Text = "科室：";
            this.lbDept.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // ucDeptTimeBaseReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelAll);
            this.Name = "ucDeptTimeBaseReport";
            this.Size = new System.Drawing.Size(781, 348);
            this.panelAll.ResumeLayout(false);
            this.neuGroupBox2.ResumeLayout(false);
            this.panelPrint.ResumeLayout(false);
            this.panelDataView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet2)).EndInit();
            this.panelAdditionTitle.ResumeLayout(false);
            this.panelAdditionTitle.PerformLayout();
            this.panelTitle.ResumeLayout(false);
            this.panelTitle.PerformLayout();
            this.nGroupBoxQueryCondition.ResumeLayout(false);
            this.panelQueryConditions.ResumeLayout(false);
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            this.panelTime.ResumeLayout(false);
            this.panelDept.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public FS.FrameWork.WinForms.Controls.NeuPanel panelAll;
        public FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox2;
        public FS.FrameWork.WinForms.Controls.NeuPanel panelPrint;
        public FS.FrameWork.WinForms.Controls.NeuPanel panelTitle;
        public FS.FrameWork.WinForms.Controls.NeuLabel lbMainTitle;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        public FS.FrameWork.WinForms.Controls.NeuPanel panelAdditionTitle;
        public FS.FrameWork.WinForms.Controls.NeuLabel lbAdditionTitleLeft;
        public FS.FrameWork.WinForms.Controls.NeuLabel lbAdditionTitleRight;
        public FS.FrameWork.WinForms.Controls.NeuLabel lbAdditionTitleMid;
        public FS.FrameWork.WinForms.Controls.NeuPanel panelDataView;
        public FS.FrameWork.WinForms.Controls.NeuSpread fpSpread1;
        public FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
        public FarPoint.Win.Spread.SheetView fpSpread1_Sheet2;
        public FS.FrameWork.WinForms.Controls.NeuGroupBox nGroupBoxQueryCondition;
        public FS.FrameWork.WinForms.Controls.NeuPanel panelQueryConditions;
        public FS.FrameWork.WinForms.Controls.NeuPanel panelFilter;
        public FS.FrameWork.WinForms.Controls.NeuTextBox txtFilter;
        public FS.FrameWork.WinForms.Controls.NeuPanel panelTime;
        public FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtStart;
        public FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtEnd;
        public FS.FrameWork.WinForms.Controls.NeuLabel label2;
        public FS.FrameWork.WinForms.Controls.NeuLabel lbTime;
        public FS.FrameWork.WinForms.Controls.NeuPanel panelDept;
        public FS.FrameWork.WinForms.Controls.NeuComboBox cmbDept;
        public FS.FrameWork.WinForms.Controls.NeuLabel lbDept;
        public FS.FrameWork.WinForms.Controls.NeuComboBox ncmbTime;
    }
}
