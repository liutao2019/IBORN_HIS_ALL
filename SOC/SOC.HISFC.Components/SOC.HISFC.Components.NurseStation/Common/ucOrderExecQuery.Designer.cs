namespace FS.SOC.HISFC.Components.NurseStation.Common
{
    partial class ucOrderExecQuery
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
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ngbPatientInfo = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.nlbInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ncbEmergency = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ncmbType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuPanelDetail = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.gbOrderExec = new System.Windows.Forms.GroupBox();
            this.fpOrderExec = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.fpOrderExec_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.ncbAST = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ncbHide = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.gbQuerySet = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.ncbNew = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ncbToDay = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ncbInvalid = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ncbValid = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ncbShort = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ncbLong = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.gbButton = new System.Windows.Forms.GroupBox();
            this.ngbPatientInfo.SuspendLayout();
            this.neuPanelDetail.SuspendLayout();
            this.gbOrderExec.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpOrderExec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpOrderExec_Sheet1)).BeginInit();
            this.gbQuerySet.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(190, 22);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(53, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 18;
            this.neuLabel2.Text = "仅查询：";
            // 
            // ngbPatientInfo
            // 
            this.ngbPatientInfo.Controls.Add(this.nlbInfo);
            this.ngbPatientInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.ngbPatientInfo.Location = new System.Drawing.Point(0, 0);
            this.ngbPatientInfo.Name = "ngbPatientInfo";
            this.ngbPatientInfo.Size = new System.Drawing.Size(826, 39);
            this.ngbPatientInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ngbPatientInfo.TabIndex = 25;
            this.ngbPatientInfo.TabStop = false;
            this.ngbPatientInfo.Text = "患者信息";
            // 
            // nlbInfo
            // 
            this.nlbInfo.AutoSize = true;
            this.nlbInfo.ForeColor = System.Drawing.Color.Blue;
            this.nlbInfo.Location = new System.Drawing.Point(22, 20);
            this.nlbInfo.Name = "nlbInfo";
            this.nlbInfo.Size = new System.Drawing.Size(587, 12);
            this.nlbInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbInfo.TabIndex = 12;
            this.nlbInfo.Text = "2001床 刘德华 男 50岁 居民医保 2011-07-20 00:00:00入院 目前住院1天 发生费用1000.00元 余额100.00元";
            // 
            // ncbEmergency
            // 
            this.ncbEmergency.AutoSize = true;
            this.ncbEmergency.Location = new System.Drawing.Point(665, 20);
            this.ncbEmergency.Name = "ncbEmergency";
            this.ncbEmergency.Size = new System.Drawing.Size(48, 16);
            this.ncbEmergency.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbEmergency.TabIndex = 19;
            this.ncbEmergency.Text = "加急";
            this.ncbEmergency.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.ncbEmergency.UseVisualStyleBackColor = true;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(22, 22);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(41, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 17;
            this.neuLabel1.Text = "类别：";
            // 
            // ncmbType
            // 
            this.ncmbType.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.ncmbType.FormattingEnabled = true;
            this.ncmbType.IsEnter2Tab = false;
            this.ncmbType.IsFlat = false;
            this.ncmbType.IsLike = true;
            this.ncmbType.IsListOnly = false;
            this.ncmbType.IsPopForm = true;
            this.ncmbType.IsShowCustomerList = false;
            this.ncmbType.IsShowID = false;
            this.ncmbType.Location = new System.Drawing.Point(69, 18);
            this.ncmbType.Name = "ncmbType";
            this.ncmbType.PopForm = null;
            this.ncmbType.ShowCustomerList = false;
            this.ncmbType.ShowID = false;
            this.ncmbType.Size = new System.Drawing.Size(116, 20);
            this.ncmbType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.ncmbType.TabIndex = 16;
            this.ncmbType.Tag = "";
            this.ncmbType.ToolBarUse = false;
            // 
            // neuPanelDetail
            // 
            this.neuPanelDetail.Controls.Add(this.gbOrderExec);
            this.neuPanelDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanelDetail.Location = new System.Drawing.Point(0, 90);
            this.neuPanelDetail.Name = "neuPanelDetail";
            this.neuPanelDetail.Size = new System.Drawing.Size(826, 284);
            this.neuPanelDetail.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanelDetail.TabIndex = 26;
            // 
            // gbOrderExec
            // 
            this.gbOrderExec.Controls.Add(this.fpOrderExec);
            this.gbOrderExec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbOrderExec.Location = new System.Drawing.Point(0, 0);
            this.gbOrderExec.Name = "gbOrderExec";
            this.gbOrderExec.Size = new System.Drawing.Size(826, 284);
            this.gbOrderExec.TabIndex = 0;
            this.gbOrderExec.TabStop = false;
            this.gbOrderExec.Text = "医嘱执行明细";
            // 
            // fpOrderExec
            // 
            this.fpOrderExec.About = "3.0.2004.2005";
            this.fpOrderExec.AccessibleDescription = "fpOrderExec, Sheet1";
            this.fpOrderExec.AllowColumnMove = true;
            this.fpOrderExec.BackColor = System.Drawing.SystemColors.Control;
            this.fpOrderExec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpOrderExec.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpOrderExec.Location = new System.Drawing.Point(3, 17);
            this.fpOrderExec.Name = "fpOrderExec";
            this.fpOrderExec.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpOrderExec.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpOrderExec_Sheet1});
            this.fpOrderExec.Size = new System.Drawing.Size(820, 264);
            this.fpOrderExec.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpOrderExec.TabIndex = 4;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpOrderExec.TextTipAppearance = tipAppearance1;
            this.fpOrderExec.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpOrderExec.ColumnDragMoveCompleted += new FarPoint.Win.Spread.DragMoveCompletedEventHandler(this.fpOrderExec_ColumnDragMoveCompleted);
            // 
            // fpOrderExec_Sheet1
            // 
            this.fpOrderExec_Sheet1.Reset();
            this.fpOrderExec_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpOrderExec_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpOrderExec_Sheet1.ColumnCount = 33;
            this.fpOrderExec_Sheet1.RowCount = 0;
            this.fpOrderExec_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247))))), System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(217)))), ((int)(((byte)(217))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpOrderExec_Sheet1.AllowGroup = true;
            this.fpOrderExec_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "标记";
            this.fpOrderExec_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "医嘱类型";
            this.fpOrderExec_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "开始时间";
            this.fpOrderExec_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "名称";
            this.fpOrderExec_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "组";
            this.fpOrderExec_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "备注";
            this.fpOrderExec_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "用法";
            this.fpOrderExec_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "频次";
            this.fpOrderExec_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "每次量";
            this.fpOrderExec_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "结束时间";
            this.fpOrderExec_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "首日次数";
            this.fpOrderExec_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "末日次数";
            this.fpOrderExec_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "预分解到(时间)";
            this.fpOrderExec_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "理论总次数";
            this.fpOrderExec_Sheet1.ColumnHeader.Cells.Get(0, 14).Value = "已分解到(时间)";
            this.fpOrderExec_Sheet1.ColumnHeader.Cells.Get(0, 15).Value = "执行次数差";
            this.fpOrderExec_Sheet1.ColumnHeader.Cells.Get(0, 16).Value = "已收费到(时间)";
            this.fpOrderExec_Sheet1.ColumnHeader.Cells.Get(0, 17).Value = "收费次数差";
            this.fpOrderExec_Sheet1.ColumnHeader.Cells.Get(0, 18).Value = "已领药申请到(时间)";
            this.fpOrderExec_Sheet1.ColumnHeader.Cells.Get(0, 19).Value = "领药申请差";
            this.fpOrderExec_Sheet1.ColumnHeader.Cells.Get(0, 20).Value = "实际发药差";
            this.fpOrderExec_Sheet1.ColumnHeader.Cells.Get(0, 21).Value = "开立医生";
            this.fpOrderExec_Sheet1.ColumnHeader.Cells.Get(0, 22).Value = "开立时间";
            this.fpOrderExec_Sheet1.ColumnHeader.Cells.Get(0, 23).Value = "开立科室";
            this.fpOrderExec_Sheet1.ColumnHeader.Cells.Get(0, 24).Value = "扣库科室";
            this.fpOrderExec_Sheet1.ColumnHeader.Cells.Get(0, 25).Value = "审核人";
            this.fpOrderExec_Sheet1.ColumnHeader.Cells.Get(0, 26).Value = "停止人";
            this.fpOrderExec_Sheet1.ColumnHeader.Cells.Get(0, 27).Value = "停止时间";
            this.fpOrderExec_Sheet1.ColumnHeader.Cells.Get(0, 28).Value = "状态";
            this.fpOrderExec_Sheet1.ColumnHeader.Cells.Get(0, 29).Value = "皮试信息";
            this.fpOrderExec_Sheet1.ColumnHeader.Cells.Get(0, 30).Value = "加急";
            this.fpOrderExec_Sheet1.ColumnHeader.Cells.Get(0, 31).Value = "类别";
            this.fpOrderExec_Sheet1.ColumnHeader.Cells.Get(0, 32).Value = "扩展";
            this.fpOrderExec_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.fpOrderExec_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpOrderExec_Sheet1.ColumnHeader.Rows.Default.Height = 34F;
            this.fpOrderExec_Sheet1.Columns.Get(2).Label = "开始时间";
            this.fpOrderExec_Sheet1.Columns.Get(2).Width = 105F;
            this.fpOrderExec_Sheet1.Columns.Get(3).Label = "名称";
            this.fpOrderExec_Sheet1.Columns.Get(3).Width = 153F;
            this.fpOrderExec_Sheet1.Columns.Get(4).Label = "组";
            this.fpOrderExec_Sheet1.Columns.Get(4).Width = 24F;
            this.fpOrderExec_Sheet1.Columns.Get(7).Label = "频次";
            this.fpOrderExec_Sheet1.Columns.Get(7).Width = 56F;
            this.fpOrderExec_Sheet1.Columns.Get(9).Label = "结束时间";
            this.fpOrderExec_Sheet1.Columns.Get(9).Width = 101F;
            this.fpOrderExec_Sheet1.Columns.Get(10).Label = "首日次数";
            this.fpOrderExec_Sheet1.Columns.Get(10).Width = 34F;
            this.fpOrderExec_Sheet1.Columns.Get(11).Label = "末日次数";
            this.fpOrderExec_Sheet1.Columns.Get(11).Width = 35F;
            this.fpOrderExec_Sheet1.Columns.Get(12).Label = "预分解到(时间)";
            this.fpOrderExec_Sheet1.Columns.Get(12).Width = 92F;
            this.fpOrderExec_Sheet1.Columns.Get(13).Label = "理论总次数";
            this.fpOrderExec_Sheet1.Columns.Get(13).Width = 45F;
            this.fpOrderExec_Sheet1.Columns.Get(14).Label = "已分解到(时间)";
            this.fpOrderExec_Sheet1.Columns.Get(14).Width = 111F;
            this.fpOrderExec_Sheet1.Columns.Get(15).Label = "执行次数差";
            this.fpOrderExec_Sheet1.Columns.Get(15).Width = 52F;
            this.fpOrderExec_Sheet1.Columns.Get(16).Label = "已收费到(时间)";
            this.fpOrderExec_Sheet1.Columns.Get(16).Width = 103F;
            this.fpOrderExec_Sheet1.Columns.Get(17).Label = "收费次数差";
            this.fpOrderExec_Sheet1.Columns.Get(17).Width = 50F;
            this.fpOrderExec_Sheet1.Columns.Get(18).Label = "已领药申请到(时间)";
            this.fpOrderExec_Sheet1.Columns.Get(18).Width = 117F;
            this.fpOrderExec_Sheet1.Columns.Get(19).Label = "领药申请差";
            this.fpOrderExec_Sheet1.Columns.Get(19).Width = 47F;
            this.fpOrderExec_Sheet1.Columns.Get(20).Label = "实际发药差";
            this.fpOrderExec_Sheet1.Columns.Get(20).Width = 45F;
            this.fpOrderExec_Sheet1.DefaultStyle.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpOrderExec_Sheet1.DefaultStyle.Locked = false;
            this.fpOrderExec_Sheet1.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpOrderExec_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpOrderExec_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpOrderExec_Sheet1.RowHeader.Columns.Get(0).Width = 26F;
            this.fpOrderExec_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.fpOrderExec_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpOrderExec_Sheet1.Rows.Default.Height = 25F;
            this.fpOrderExec_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.fpOrderExec_Sheet1.SheetCornerStyle.Locked = false;
            this.fpOrderExec_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.fpOrderExec_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpOrderExec_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpOrderExec.SetActiveViewport(0, 1, 0);
            // 
            // ncbAST
            // 
            this.ncbAST.AutoSize = true;
            this.ncbAST.Location = new System.Drawing.Point(607, 20);
            this.ncbAST.Name = "ncbAST";
            this.ncbAST.Size = new System.Drawing.Size(48, 16);
            this.ncbAST.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbAST.TabIndex = 15;
            this.ncbAST.Text = "皮试";
            this.ncbAST.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.ncbAST.UseVisualStyleBackColor = true;
            // 
            // ncbHide
            // 
            this.ncbHide.AutoSize = true;
            this.ncbHide.Location = new System.Drawing.Point(728, 20);
            this.ncbHide.Name = "ncbHide";
            this.ncbHide.Size = new System.Drawing.Size(48, 16);
            this.ncbHide.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbHide.TabIndex = 14;
            this.ncbHide.Text = "重整";
            this.ncbHide.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.ncbHide.UseVisualStyleBackColor = true;
            // 
            // gbQuerySet
            // 
            this.gbQuerySet.Controls.Add(this.ncbEmergency);
            this.gbQuerySet.Controls.Add(this.neuLabel2);
            this.gbQuerySet.Controls.Add(this.neuLabel1);
            this.gbQuerySet.Controls.Add(this.ncmbType);
            this.gbQuerySet.Controls.Add(this.ncbAST);
            this.gbQuerySet.Controls.Add(this.ncbHide);
            this.gbQuerySet.Controls.Add(this.ncbNew);
            this.gbQuerySet.Controls.Add(this.ncbToDay);
            this.gbQuerySet.Controls.Add(this.ncbInvalid);
            this.gbQuerySet.Controls.Add(this.ncbValid);
            this.gbQuerySet.Controls.Add(this.ncbShort);
            this.gbQuerySet.Controls.Add(this.ncbLong);
            this.gbQuerySet.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbQuerySet.Location = new System.Drawing.Point(0, 39);
            this.gbQuerySet.Name = "gbQuerySet";
            this.gbQuerySet.Size = new System.Drawing.Size(826, 51);
            this.gbQuerySet.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbQuerySet.TabIndex = 24;
            this.gbQuerySet.TabStop = false;
            this.gbQuerySet.Text = "查询设置";
            // 
            // ncbNew
            // 
            this.ncbNew.AutoSize = true;
            this.ncbNew.Location = new System.Drawing.Point(549, 20);
            this.ncbNew.Name = "ncbNew";
            this.ncbNew.Size = new System.Drawing.Size(48, 16);
            this.ncbNew.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbNew.TabIndex = 13;
            this.ncbNew.Text = "新开";
            this.ncbNew.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.ncbNew.UseVisualStyleBackColor = true;
            // 
            // ncbToDay
            // 
            this.ncbToDay.AutoSize = true;
            this.ncbToDay.Location = new System.Drawing.Point(488, 20);
            this.ncbToDay.Name = "ncbToDay";
            this.ncbToDay.Size = new System.Drawing.Size(48, 16);
            this.ncbToDay.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbToDay.TabIndex = 12;
            this.ncbToDay.Text = "当天";
            this.ncbToDay.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.ncbToDay.UseVisualStyleBackColor = true;
            // 
            // ncbInvalid
            // 
            this.ncbInvalid.AutoSize = true;
            this.ncbInvalid.Checked = true;
            this.ncbInvalid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ncbInvalid.Location = new System.Drawing.Point(427, 20);
            this.ncbInvalid.Name = "ncbInvalid";
            this.ncbInvalid.Size = new System.Drawing.Size(48, 16);
            this.ncbInvalid.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbInvalid.TabIndex = 11;
            this.ncbInvalid.Text = "作废";
            this.ncbInvalid.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.ncbInvalid.UseVisualStyleBackColor = true;
            // 
            // ncbValid
            // 
            this.ncbValid.AutoSize = true;
            this.ncbValid.Checked = true;
            this.ncbValid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ncbValid.Location = new System.Drawing.Point(366, 20);
            this.ncbValid.Name = "ncbValid";
            this.ncbValid.Size = new System.Drawing.Size(48, 16);
            this.ncbValid.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbValid.TabIndex = 10;
            this.ncbValid.Text = "有效";
            this.ncbValid.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.ncbValid.UseVisualStyleBackColor = true;
            // 
            // ncbShort
            // 
            this.ncbShort.AutoSize = true;
            this.ncbShort.Checked = true;
            this.ncbShort.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ncbShort.Location = new System.Drawing.Point(305, 20);
            this.ncbShort.Name = "ncbShort";
            this.ncbShort.Size = new System.Drawing.Size(48, 16);
            this.ncbShort.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbShort.TabIndex = 9;
            this.ncbShort.Text = "临时";
            this.ncbShort.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.ncbShort.UseVisualStyleBackColor = true;
            // 
            // ncbLong
            // 
            this.ncbLong.AutoSize = true;
            this.ncbLong.Checked = true;
            this.ncbLong.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ncbLong.Location = new System.Drawing.Point(244, 20);
            this.ncbLong.Name = "ncbLong";
            this.ncbLong.Size = new System.Drawing.Size(48, 16);
            this.ncbLong.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ncbLong.TabIndex = 8;
            this.ncbLong.Text = "长期";
            this.ncbLong.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.ncbLong.UseVisualStyleBackColor = true;
            // 
            // gbButton
            // 
            this.gbButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbButton.Location = new System.Drawing.Point(0, 374);
            this.gbButton.Name = "gbButton";
            this.gbButton.Size = new System.Drawing.Size(826, 53);
            this.gbButton.TabIndex = 27;
            this.gbButton.TabStop = false;
            this.gbButton.Text = "附加信息";
            this.gbButton.Visible = false;
            // 
            // ucOrderExecQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuPanelDetail);
            this.Controls.Add(this.gbButton);
            this.Controls.Add(this.gbQuerySet);
            this.Controls.Add(this.ngbPatientInfo);
            this.Name = "ucOrderExecQuery";
            this.Size = new System.Drawing.Size(826, 427);
            this.ngbPatientInfo.ResumeLayout(false);
            this.ngbPatientInfo.PerformLayout();
            this.neuPanelDetail.ResumeLayout(false);
            this.gbOrderExec.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpOrderExec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpOrderExec_Sheet1)).EndInit();
            this.gbQuerySet.ResumeLayout(false);
            this.gbQuerySet.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        protected FS.FrameWork.WinForms.Controls.NeuGroupBox ngbPatientInfo;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbInfo;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbEmergency;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox ncmbType;
        protected FS.FrameWork.WinForms.Controls.NeuPanel neuPanelDetail;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbAST;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbHide;
        protected FS.FrameWork.WinForms.Controls.NeuGroupBox gbQuerySet;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbNew;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbToDay;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbInvalid;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbValid;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbShort;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbLong;
        private System.Windows.Forms.GroupBox gbButton;
        private System.Windows.Forms.GroupBox gbOrderExec;
        protected FS.SOC.Windows.Forms.FpSpread socFpEnter1;
        private FarPoint.Win.Spread.SheetView fpOrderExec_Sheet1;
        protected FS.SOC.Windows.Forms.FpSpread fpOrderExec;
    }
}
