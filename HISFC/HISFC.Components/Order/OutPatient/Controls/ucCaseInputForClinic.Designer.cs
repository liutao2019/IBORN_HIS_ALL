namespace FS.HISFC.Components.Order.OutPatient.Controls
{
    partial class ucCaseInputForClinic
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
            FarPoint.Win.Spread.TipAppearance tipAppearance3 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.TipAppearance tipAppearance4 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType4 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType5 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType6 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.DateTimeCellType dateTimeCellType2 = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucCaseInputForClinic));
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.chkCurrentDeptCase = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ucDiagnose1 = new FS.HISFC.Components.Common.Controls.ucDiagnose();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btHistory = new System.Windows.Forms.Button();
            this.lklblHistory = new System.Windows.Forms.LinkLabel();
            this.fpHistory = new FarPoint.Win.Spread.FpSpread();
            this.fpHistory_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.btDelete = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.btSave = new System.Windows.Forms.Button();
            this.btAdd = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.fpDiag = new FS.FrameWork.WinForms.Controls.NeuFpEnter(this.components);
            this.fpDiag_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.ucModifyOutPatientHealthInfo2 = new FS.HISFC.Components.Common.Controls.ucModifyOutPatientHealthInfo();
            this.lblRegInfo = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.ucUserText1 = new FS.HISFC.Components.Common.Controls.ucUserText();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbxDeptDiag = new System.Windows.Forms.CheckBox();
            this.btInsert = new System.Windows.Forms.Button();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpHistory_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpDiag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpDiag_Sheet1)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkCurrentDeptCase
            // 
            this.chkCurrentDeptCase.AutoSize = true;
            this.chkCurrentDeptCase.Location = new System.Drawing.Point(91, 68);
            this.chkCurrentDeptCase.Name = "chkCurrentDeptCase";
            this.chkCurrentDeptCase.Size = new System.Drawing.Size(108, 16);
            this.chkCurrentDeptCase.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chkCurrentDeptCase.TabIndex = 12;
            this.chkCurrentDeptCase.Text = "仅当前科室诊断";
            this.chkCurrentDeptCase.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.ucDiagnose1);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.fpDiag);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 63);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(701, 557);
            this.panel3.TabIndex = 26;
            // 
            // ucDiagnose1
            // 
            this.ucDiagnose1.AlDiag = null;
            this.ucDiagnose1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ucDiagnose1.IsShowICDCategory = false;
            this.ucDiagnose1.Location = new System.Drawing.Point(212, 50);
            this.ucDiagnose1.Name = "ucDiagnose1";
            this.ucDiagnose1.Size = new System.Drawing.Size(536, 240);
            this.ucDiagnose1.TabIndex = 2;
            this.ucDiagnose1.Visible = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btInsert);
            this.panel4.Controls.Add(this.chkCurrentDeptCase);
            this.panel4.Controls.Add(this.btHistory);
            this.panel4.Controls.Add(this.lklblHistory);
            this.panel4.Controls.Add(this.fpHistory);
            this.panel4.Controls.Add(this.btDelete);
            this.panel4.Controls.Add(this.btCancel);
            this.panel4.Controls.Add(this.btSave);
            this.panel4.Controls.Add(this.btAdd);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 210);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(701, 347);
            this.panel4.TabIndex = 13;
            // 
            // btHistory
            // 
            this.btHistory.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btHistory.ForeColor = System.Drawing.Color.Black;
            this.btHistory.Location = new System.Drawing.Point(516, 8);
            this.btHistory.Name = "btHistory";
            this.btHistory.Size = new System.Drawing.Size(90, 28);
            this.btHistory.TabIndex = 7;
            this.btHistory.Text = "历史诊断";
            // 
            // lklblHistory
            // 
            this.lklblHistory.ForeColor = System.Drawing.Color.Black;
            this.lklblHistory.LinkColor = System.Drawing.Color.Black;
            this.lklblHistory.Location = new System.Drawing.Point(3, 69);
            this.lklblHistory.Name = "lklblHistory";
            this.lklblHistory.Size = new System.Drawing.Size(61, 15);
            this.lklblHistory.TabIndex = 9;
            this.lklblHistory.TabStop = true;
            this.lklblHistory.Text = "历史诊断:";
            // 
            // fpHistory
            // 
            this.fpHistory.About = "3.0.2004.2005";
            this.fpHistory.AccessibleDescription = "fpHistory, Sheet1, Row 0, Column 0, ";
            this.fpHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.fpHistory.BackColor = System.Drawing.Color.Honeydew;
            this.fpHistory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fpHistory.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            this.fpHistory.Location = new System.Drawing.Point(0, 90);
            this.fpHistory.Name = "fpHistory";
            this.fpHistory.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpHistory.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpHistory_Sheet1});
            this.fpHistory.Size = new System.Drawing.Size(701, 257);
            this.fpHistory.TabIndex = 8;
            tipAppearance3.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance3.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpHistory.TextTipAppearance = tipAppearance3;
            this.fpHistory.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpHistory_Sheet1
            // 
            this.fpHistory_Sheet1.Reset();
            this.fpHistory_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpHistory_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpHistory_Sheet1.ColumnCount = 9;
            this.fpHistory_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpHistory_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "诊断类型";
            this.fpHistory_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "描述";
            this.fpHistory_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "ICD10";
            this.fpHistory_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "诊断名称";
            this.fpHistory_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "疑诊";
            this.fpHistory_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "初诊";
            this.fpHistory_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "诊断医师";
            this.fpHistory_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "诊断日期";
            this.fpHistory_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "诊断分级";
            this.fpHistory_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpHistory_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpHistory_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.fpHistory_Sheet1.Columns.Get(0).Label = "诊断类型";
            this.fpHistory_Sheet1.Columns.Get(0).Width = 90F;
            this.fpHistory_Sheet1.Columns.Get(1).Label = "描述";
            this.fpHistory_Sheet1.Columns.Get(1).Width = 37F;
            this.fpHistory_Sheet1.Columns.Get(3).Label = "诊断名称";
            this.fpHistory_Sheet1.Columns.Get(3).Width = 176F;
            this.fpHistory_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpHistory_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpHistory_Sheet1.RowHeader.Columns.Get(0).Width = 27F;
            this.fpHistory_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpHistory_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpHistory_Sheet1.Rows.Default.Height = 25F;
            this.fpHistory_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpHistory_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpHistory_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpHistory_Sheet1.SheetCornerStyle.Locked = false;
            this.fpHistory_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.fpHistory_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpHistory_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // btDelete
            // 
            this.btDelete.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btDelete.ForeColor = System.Drawing.Color.Black;
            this.btDelete.Location = new System.Drawing.Point(210, 8);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(90, 28);
            this.btDelete.TabIndex = 5;
            this.btDelete.Text = "删 除";
            // 
            // btCancel
            // 
            this.btCancel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btCancel.ForeColor = System.Drawing.Color.Black;
            this.btCancel.Location = new System.Drawing.Point(312, 8);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(90, 28);
            this.btCancel.TabIndex = 10;
            this.btCancel.Text = "作 废";
            // 
            // btSave
            // 
            this.btSave.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btSave.ForeColor = System.Drawing.Color.Black;
            this.btSave.Location = new System.Drawing.Point(414, 8);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(90, 28);
            this.btSave.TabIndex = 6;
            this.btSave.Text = "保 存";
            // 
            // btAdd
            // 
            this.btAdd.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btAdd.ForeColor = System.Drawing.Color.Black;
            this.btAdd.Location = new System.Drawing.Point(6, 8);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(90, 28);
            this.btAdd.TabIndex = 4;
            this.btAdd.Text = "增 加";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(3, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(485, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "红色为已作废的诊断,诊断保存后一天内可以添加和修改,一旦保存,只可以作废,不可以删除";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fpDiag
            // 
            this.fpDiag.About = "3.0.2004.2005";
            this.fpDiag.AccessibleDescription = "fpDiag, Sheet1, Row 0, Column 0, ";
            this.fpDiag.AllowDragFill = true;
            this.fpDiag.AllowDrop = true;
            this.fpDiag.BackColor = System.Drawing.Color.Azure;
            this.fpDiag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fpDiag.Dock = System.Windows.Forms.DockStyle.Top;
            this.fpDiag.EditModePermanent = true;
            this.fpDiag.EditModeReplace = true;
            this.fpDiag.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpDiag.Location = new System.Drawing.Point(0, 0);
            this.fpDiag.Name = "fpDiag";
            this.fpDiag.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpDiag.SelectNone = false;
            this.fpDiag.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpDiag_Sheet1});
            this.fpDiag.ShowListWhenOfFocus = false;
            this.fpDiag.Size = new System.Drawing.Size(701, 210);
            this.fpDiag.TabIndex = 1;
            tipAppearance4.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance4.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpDiag.TextTipAppearance = tipAppearance4;
            this.fpDiag.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpDiag_Sheet1
            // 
            this.fpDiag_Sheet1.Reset();
            this.fpDiag_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpDiag_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpDiag_Sheet1.ColumnCount = 11;
            this.fpDiag_Sheet1.RowCount = 1;
            this.fpDiag_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpDiag_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "诊断类别";
            this.fpDiag_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "描述";
            this.fpDiag_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "ICD10";
            this.fpDiag_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "诊断名称";
            this.fpDiag_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "疑诊";
            this.fpDiag_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "初诊";
            this.fpDiag_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "诊断日期";
            this.fpDiag_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "诊断医师代码";
            this.fpDiag_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "诊断医师";
            this.fpDiag_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "诊断分期";
            this.fpDiag_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "诊断分级";
            this.fpDiag_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpDiag_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpDiag_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.fpDiag_Sheet1.Columns.Get(0).Label = "诊断类别";
            this.fpDiag_Sheet1.Columns.Get(0).Width = 69F;
            this.fpDiag_Sheet1.Columns.Get(1).CellType = checkBoxCellType4;
            this.fpDiag_Sheet1.Columns.Get(1).Label = "描述";
            this.fpDiag_Sheet1.Columns.Get(1).Width = 32F;
            this.fpDiag_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.fpDiag_Sheet1.Columns.Get(2).Label = "ICD10";
            this.fpDiag_Sheet1.Columns.Get(2).Width = 64F;
            this.fpDiag_Sheet1.Columns.Get(3).Label = "诊断名称";
            this.fpDiag_Sheet1.Columns.Get(3).Width = 227F;
            this.fpDiag_Sheet1.Columns.Get(4).CellType = checkBoxCellType5;
            this.fpDiag_Sheet1.Columns.Get(4).Label = "疑诊";
            this.fpDiag_Sheet1.Columns.Get(4).Width = 38F;
            this.fpDiag_Sheet1.Columns.Get(5).CellType = checkBoxCellType6;
            this.fpDiag_Sheet1.Columns.Get(5).Label = "初诊";
            this.fpDiag_Sheet1.Columns.Get(5).Width = 34F;
            dateTimeCellType2.Calendar = ((System.Globalization.Calendar)(resources.GetObject("dateTimeCellType2.Calendar")));
            dateTimeCellType2.CalendarDayFont = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dateTimeCellType2.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText;
            dateTimeCellType2.DateDefault = new System.DateTime(2008, 1, 3, 19, 50, 13, 0);
            dateTimeCellType2.TimeDefault = new System.DateTime(2008, 1, 3, 19, 50, 13, 0);
            this.fpDiag_Sheet1.Columns.Get(6).CellType = dateTimeCellType2;
            this.fpDiag_Sheet1.Columns.Get(6).Label = "诊断日期";
            this.fpDiag_Sheet1.Columns.Get(6).Width = 82F;
            this.fpDiag_Sheet1.Columns.Get(7).Label = "诊断医师代码";
            this.fpDiag_Sheet1.Columns.Get(7).Visible = false;
            this.fpDiag_Sheet1.Columns.Get(7).Width = 88F;
            this.fpDiag_Sheet1.Columns.Get(8).CellType = textCellType4;
            this.fpDiag_Sheet1.Columns.Get(8).Label = "诊断医师";
            this.fpDiag_Sheet1.Columns.Get(8).Width = 64F;
            this.fpDiag_Sheet1.Columns.Get(9).Label = "诊断分期";
            this.fpDiag_Sheet1.Columns.Get(9).Visible = false;
            this.fpDiag_Sheet1.Columns.Get(10).Label = "诊断分级";
            this.fpDiag_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpDiag_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpDiag_Sheet1.RowHeader.Columns.Get(0).Width = 40F;
            this.fpDiag_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpDiag_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpDiag_Sheet1.Rows.Default.Height = 25F;
            this.fpDiag_Sheet1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.fpDiag_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpDiag_Sheet1.SelectionStyle = FarPoint.Win.Spread.SelectionStyles.SelectionColors;
            this.fpDiag_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpDiag_Sheet1.SheetCornerStyle.Locked = false;
            this.fpDiag_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.fpDiag_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpDiag_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.ucModifyOutPatientHealthInfo2);
            this.panel5.Controls.Add(this.lblRegInfo);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(701, 63);
            this.panel5.TabIndex = 27;
            // 
            // ucModifyOutPatientHealthInfo2
            // 
            this.ucModifyOutPatientHealthInfo2.BackColor = System.Drawing.Color.White;
            this.ucModifyOutPatientHealthInfo2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucModifyOutPatientHealthInfo2.IsVisibleSave = true;
            this.ucModifyOutPatientHealthInfo2.Location = new System.Drawing.Point(0, 33);
            this.ucModifyOutPatientHealthInfo2.Name = "ucModifyOutPatientHealthInfo2";
            this.ucModifyOutPatientHealthInfo2.RegInfo = null;
            this.ucModifyOutPatientHealthInfo2.RememberHelthHistoryDays = 7;
            this.ucModifyOutPatientHealthInfo2.Size = new System.Drawing.Size(701, 30);
            this.ucModifyOutPatientHealthInfo2.TabIndex = 0;
            // 
            // lblRegInfo
            // 
            this.lblRegInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRegInfo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRegInfo.Location = new System.Drawing.Point(0, 0);
            this.lblRegInfo.Name = "lblRegInfo";
            this.lblRegInfo.Size = new System.Drawing.Size(701, 33);
            this.lblRegInfo.TabIndex = 12;
            this.lblRegInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(701, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(10, 620);
            this.splitter1.TabIndex = 24;
            this.splitter1.TabStop = false;
            // 
            // ucUserText1
            // 
            this.ucUserText1.AutoScroll = true;
            this.ucUserText1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ucUserText1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucUserText1.GroupInfo = null;
            this.ucUserText1.Location = new System.Drawing.Point(0, 0);
            this.ucUserText1.Name = "ucUserText1";
            this.ucUserText1.Size = new System.Drawing.Size(257, 620);
            this.ucUserText1.TabIndex = 13;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cbxDeptDiag);
            this.panel2.Controls.Add(this.ucUserText1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(711, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(257, 620);
            this.panel2.TabIndex = 25;
            // 
            // cbxDeptDiag
            // 
            this.cbxDeptDiag.AutoSize = true;
            this.cbxDeptDiag.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbxDeptDiag.ForeColor = System.Drawing.Color.Red;
            this.cbxDeptDiag.Location = new System.Drawing.Point(94, 7);
            this.cbxDeptDiag.Name = "cbxDeptDiag";
            this.cbxDeptDiag.Size = new System.Drawing.Size(84, 16);
            this.cbxDeptDiag.TabIndex = 14;
            this.cbxDeptDiag.Text = "科常用诊断";
            this.cbxDeptDiag.UseVisualStyleBackColor = true;
            // 
            // btInsert
            // 
            this.btInsert.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btInsert.ForeColor = System.Drawing.Color.Black;
            this.btInsert.Location = new System.Drawing.Point(108, 8);
            this.btInsert.Name = "btInsert";
            this.btInsert.Size = new System.Drawing.Size(90, 28);
            this.btInsert.TabIndex = 13;
            this.btInsert.Text = "插 入";
            // 
            // ucCaseInputForClinic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel2);
            this.Name = "ucCaseInputForClinic";
            this.Size = new System.Drawing.Size(968, 620);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpHistory_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpDiag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpDiag_Sheet1)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuCheckBox chkCurrentDeptCase;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btHistory;
        private System.Windows.Forms.LinkLabel lklblHistory;
        private FarPoint.Win.Spread.FpSpread fpHistory;
        private FarPoint.Win.Spread.SheetView fpHistory_Sheet1;
        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btAdd;
        private FS.HISFC.Components.Common.Controls.ucDiagnose ucDiagnose1;
        private FS.FrameWork.WinForms.Controls.NeuFpEnter fpDiag;
        private FarPoint.Win.Spread.SheetView fpDiag_Sheet1;
        private System.Windows.Forms.Panel panel5;
        private FS.HISFC.Components.Common.Controls.ucModifyOutPatientHealthInfo ucModifyOutPatientHealthInfo2;
        public System.Windows.Forms.Label lblRegInfo;
        private System.Windows.Forms.Splitter splitter1;
        private FS.HISFC.Components.Common.Controls.ucUserText ucUserText1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox cbxDeptDiag;
        private System.Windows.Forms.Button btInsert;

    }
}
