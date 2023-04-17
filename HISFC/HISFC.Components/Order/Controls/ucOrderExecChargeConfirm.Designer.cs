namespace FS.HISFC.Components.Order.Controls
{
    partial class ucOrderExecChargeConfirm
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
            FarPoint.Win.Spread.TipAppearance tipAppearance7 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType7 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.TipAppearance tipAppearance8 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType8 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.neuPanelDetail = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.tcExecOrder = new System.Windows.Forms.TabControl();
            this.tbLongExecOrder = new System.Windows.Forms.TabPage();
            this.fpLongExecOrder = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.fpLongExecOrder_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.tbShortExecOrder = new System.Windows.Forms.TabPage();
            this.fpShortExecOrder = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.fpShortExecOrder_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.ngbAdd = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.ngbQuerySet = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.lblPatientInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ncbLong = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ncbShort = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ncbValid = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ncbInvalid = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ncbToDay = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ncbNew = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ncbHide = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ncbAST = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ncmbType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ncbEmergency = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.neuPanelDetail.SuspendLayout();
            this.tcExecOrder.SuspendLayout();
            this.tbLongExecOrder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpLongExecOrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpLongExecOrder_Sheet1)).BeginInit();
            this.tbShortExecOrder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpShortExecOrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpShortExecOrder_Sheet1)).BeginInit();
            this.ngbAdd.SuspendLayout();
            this.ngbQuerySet.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanelDetail
            // 
            this.neuPanelDetail.Controls.Add(this.tcExecOrder);
            this.neuPanelDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanelDetail.Location = new System.Drawing.Point(0, 51);
            this.neuPanelDetail.Name = "neuPanelDetail";
            this.neuPanelDetail.Size = new System.Drawing.Size(884, 427);
            this.neuPanelDetail.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanelDetail.TabIndex = 26;
            // 
            // tcExecOrder
            // 
            this.tcExecOrder.Controls.Add(this.tbLongExecOrder);
            this.tcExecOrder.Controls.Add(this.tbShortExecOrder);
            this.tcExecOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcExecOrder.Location = new System.Drawing.Point(0, 0);
            this.tcExecOrder.Name = "tcExecOrder";
            this.tcExecOrder.SelectedIndex = 0;
            this.tcExecOrder.Size = new System.Drawing.Size(884, 427);
            this.tcExecOrder.TabIndex = 0;
            // 
            // tbLongExecOrder
            // 
            this.tbLongExecOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tbLongExecOrder.Controls.Add(this.fpLongExecOrder);
            this.tbLongExecOrder.Location = new System.Drawing.Point(4, 22);
            this.tbLongExecOrder.Name = "tbLongExecOrder";
            this.tbLongExecOrder.Padding = new System.Windows.Forms.Padding(3);
            this.tbLongExecOrder.Size = new System.Drawing.Size(876, 401);
            this.tbLongExecOrder.TabIndex = 0;
            this.tbLongExecOrder.Text = "长期医嘱";
            // 
            // fpLongExecOrder
            // 
            this.fpLongExecOrder.About = "3.0.2004.2005";
            this.fpLongExecOrder.AccessibleDescription = "fpLongExecOrder, Sheet1, Row 0, Column 0, ";
            this.fpLongExecOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.fpLongExecOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpLongExecOrder.Location = new System.Drawing.Point(3, 3);
            this.fpLongExecOrder.Name = "fpLongExecOrder";
            this.fpLongExecOrder.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpLongExecOrder.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpLongExecOrder_Sheet1});
            this.fpLongExecOrder.Size = new System.Drawing.Size(870, 395);
            this.fpLongExecOrder.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpLongExecOrder.TabIndex = 0;
            tipAppearance7.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance7.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpLongExecOrder.TextTipAppearance = tipAppearance7;
            // 
            // fpLongExecOrder_Sheet1
            // 
            this.fpLongExecOrder_Sheet1.Reset();
            this.fpLongExecOrder_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpLongExecOrder_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpLongExecOrder_Sheet1.ColumnCount = 10;
            this.fpLongExecOrder_Sheet1.RowCount = 0;
            this.fpLongExecOrder_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247))))), System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(217)))), ((int)(((byte)(217))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpLongExecOrder_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "选择";
            this.fpLongExecOrder_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "项目编码";
            this.fpLongExecOrder_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "项目名称";
            this.fpLongExecOrder_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "规格";
            this.fpLongExecOrder_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "数量";
            this.fpLongExecOrder_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "单位";
            this.fpLongExecOrder_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "用法";
            this.fpLongExecOrder_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "频次";
            this.fpLongExecOrder_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "使用时间";
            this.fpLongExecOrder_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "取药药房";
            this.fpLongExecOrder_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.fpLongExecOrder_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpLongExecOrder_Sheet1.ColumnHeader.Rows.Get(0).Height = 35F;
            this.fpLongExecOrder_Sheet1.Columns.Get(0).CellType = checkBoxCellType7;
            this.fpLongExecOrder_Sheet1.Columns.Get(0).Label = "选择";
            this.fpLongExecOrder_Sheet1.Columns.Get(0).Width = 50F;
            this.fpLongExecOrder_Sheet1.Columns.Get(2).Label = "项目名称";
            this.fpLongExecOrder_Sheet1.Columns.Get(2).Width = 172F;
            this.fpLongExecOrder_Sheet1.Columns.Get(8).Label = "使用时间";
            this.fpLongExecOrder_Sheet1.Columns.Get(8).Width = 134F;
            this.fpLongExecOrder_Sheet1.Columns.Get(9).Label = "取药药房";
            this.fpLongExecOrder_Sheet1.Columns.Get(9).Width = 89F;
            this.fpLongExecOrder_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpLongExecOrder_Sheet1.RowHeader.Columns.Get(0).Width = 20F;
            this.fpLongExecOrder_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.fpLongExecOrder_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpLongExecOrder_Sheet1.Rows.Default.Height = 30F;
            this.fpLongExecOrder_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.fpLongExecOrder_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.fpLongExecOrder_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpLongExecOrder_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpLongExecOrder.SetActiveViewport(0, 1, 0);
            // 
            // tbShortExecOrder
            // 
            this.tbShortExecOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tbShortExecOrder.Controls.Add(this.fpShortExecOrder);
            this.tbShortExecOrder.Location = new System.Drawing.Point(4, 22);
            this.tbShortExecOrder.Name = "tbShortExecOrder";
            this.tbShortExecOrder.Padding = new System.Windows.Forms.Padding(3);
            this.tbShortExecOrder.Size = new System.Drawing.Size(876, 401);
            this.tbShortExecOrder.TabIndex = 1;
            this.tbShortExecOrder.Text = "临时医嘱";
            // 
            // fpShortExecOrder
            // 
            this.fpShortExecOrder.About = "3.0.2004.2005";
            this.fpShortExecOrder.AccessibleDescription = "fpLongExecOrder, Sheet1, Row 0, Column 0, ";
            this.fpShortExecOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.fpShortExecOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpShortExecOrder.Location = new System.Drawing.Point(3, 3);
            this.fpShortExecOrder.Name = "fpShortExecOrder";
            this.fpShortExecOrder.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpShortExecOrder.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpShortExecOrder_Sheet1});
            this.fpShortExecOrder.Size = new System.Drawing.Size(870, 395);
            this.fpShortExecOrder.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpShortExecOrder.TabIndex = 1;
            tipAppearance8.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance8.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpShortExecOrder.TextTipAppearance = tipAppearance8;
            // 
            // fpShortExecOrder_Sheet1
            // 
            this.fpShortExecOrder_Sheet1.Reset();
            this.fpShortExecOrder_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpShortExecOrder_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpShortExecOrder_Sheet1.ColumnCount = 10;
            this.fpShortExecOrder_Sheet1.RowCount = 0;
            this.fpShortExecOrder_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247))))), System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(217)))), ((int)(((byte)(217))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpShortExecOrder_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "选择";
            this.fpShortExecOrder_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "项目编码";
            this.fpShortExecOrder_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "项目名称";
            this.fpShortExecOrder_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "规格";
            this.fpShortExecOrder_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "数量";
            this.fpShortExecOrder_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "单位";
            this.fpShortExecOrder_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "用法";
            this.fpShortExecOrder_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "频次";
            this.fpShortExecOrder_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "使用时间";
            this.fpShortExecOrder_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "取药药房";
            this.fpShortExecOrder_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.fpShortExecOrder_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpShortExecOrder_Sheet1.ColumnHeader.Rows.Get(0).Height = 35F;
            this.fpShortExecOrder_Sheet1.Columns.Get(0).CellType = checkBoxCellType8;
            this.fpShortExecOrder_Sheet1.Columns.Get(0).Label = "选择";
            this.fpShortExecOrder_Sheet1.Columns.Get(0).Width = 50F;
            this.fpShortExecOrder_Sheet1.Columns.Get(2).Label = "项目名称";
            this.fpShortExecOrder_Sheet1.Columns.Get(2).Width = 172F;
            this.fpShortExecOrder_Sheet1.Columns.Get(8).Label = "使用时间";
            this.fpShortExecOrder_Sheet1.Columns.Get(8).Width = 134F;
            this.fpShortExecOrder_Sheet1.Columns.Get(9).Label = "取药药房";
            this.fpShortExecOrder_Sheet1.Columns.Get(9).Width = 89F;
            this.fpShortExecOrder_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpShortExecOrder_Sheet1.RowHeader.Columns.Get(0).Width = 20F;
            this.fpShortExecOrder_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.fpShortExecOrder_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpShortExecOrder_Sheet1.Rows.Default.Height = 30F;
            this.fpShortExecOrder_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.fpShortExecOrder_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.fpShortExecOrder_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpShortExecOrder_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpShortExecOrder.SetActiveViewport(0, 1, 0);
            // 
            // ngbAdd
            // 
            this.ngbAdd.Controls.Add(this.lblPatientInfo);
            this.ngbAdd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ngbAdd.Location = new System.Drawing.Point(0, 478);
            this.ngbAdd.Name = "ngbAdd";
            this.ngbAdd.Size = new System.Drawing.Size(884, 54);
            this.ngbAdd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ngbAdd.TabIndex = 25;
            this.ngbAdd.TabStop = false;
            this.ngbAdd.Text = "附加信息";
            // 
            // ngbQuerySet
            // 
            this.ngbQuerySet.Controls.Add(this.ncbEmergency);
            this.ngbQuerySet.Controls.Add(this.neuLabel2);
            this.ngbQuerySet.Controls.Add(this.neuLabel1);
            this.ngbQuerySet.Controls.Add(this.ncmbType);
            this.ngbQuerySet.Controls.Add(this.ncbAST);
            this.ngbQuerySet.Controls.Add(this.ncbHide);
            this.ngbQuerySet.Controls.Add(this.ncbNew);
            this.ngbQuerySet.Controls.Add(this.ncbToDay);
            this.ngbQuerySet.Controls.Add(this.ncbInvalid);
            this.ngbQuerySet.Controls.Add(this.ncbValid);
            this.ngbQuerySet.Controls.Add(this.ncbShort);
            this.ngbQuerySet.Controls.Add(this.ncbLong);
            this.ngbQuerySet.Dock = System.Windows.Forms.DockStyle.Top;
            this.ngbQuerySet.Location = new System.Drawing.Point(0, 0);
            this.ngbQuerySet.Name = "ngbQuerySet";
            this.ngbQuerySet.Size = new System.Drawing.Size(884, 51);
            this.ngbQuerySet.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ngbQuerySet.TabIndex = 24;
            this.ngbQuerySet.TabStop = false;
            this.ngbQuerySet.Text = "查询设置";
            this.ngbQuerySet.Visible = false;
            // 
            // lblPatientInfo
            // 
            this.lblPatientInfo.AutoSize = true;
            this.lblPatientInfo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPatientInfo.ForeColor = System.Drawing.Color.Blue;
            this.lblPatientInfo.Location = new System.Drawing.Point(22, 24);
            this.lblPatientInfo.Name = "lblPatientInfo";
            this.lblPatientInfo.Size = new System.Drawing.Size(758, 14);
            this.lblPatientInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblPatientInfo.TabIndex = 12;
            this.lblPatientInfo.Text = "2001床 刘德华 男 50岁 居民医保 2011-07-20 00:00:00入院 目前住院1天 发生费用1000.00元 余额100.00元";
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
            this.ncbLong.Visible = false;
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
            this.ncbShort.Visible = false;
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
            this.ncbValid.Visible = false;
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
            this.ncbInvalid.Visible = false;
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
            this.ncbToDay.Visible = false;
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
            this.ncbNew.Visible = false;
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
            this.ncbHide.Visible = false;
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
            this.ncbAST.Visible = false;
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
            this.ncmbType.Visible = false;
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
            this.neuLabel1.Visible = false;
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
            this.neuLabel2.Visible = false;
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
            this.ncbEmergency.Visible = false;
            // 
            // ucOrderExecChargeConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.Controls.Add(this.neuPanelDetail);
            this.Controls.Add(this.ngbAdd);
            this.Controls.Add(this.ngbQuerySet);
            this.Name = "ucOrderExecChargeConfirm";
            this.Size = new System.Drawing.Size(884, 532);
            this.neuPanelDetail.ResumeLayout(false);
            this.tcExecOrder.ResumeLayout(false);
            this.tbLongExecOrder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpLongExecOrder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpLongExecOrder_Sheet1)).EndInit();
            this.tbShortExecOrder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpShortExecOrder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpShortExecOrder_Sheet1)).EndInit();
            this.ngbAdd.ResumeLayout(false);
            this.ngbAdd.PerformLayout();
            this.ngbQuerySet.ResumeLayout(false);
            this.ngbQuerySet.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        protected FS.FrameWork.WinForms.Controls.NeuPanel neuPanelDetail;
        protected FS.FrameWork.WinForms.Controls.NeuGroupBox ngbAdd;
        protected FS.FrameWork.WinForms.Controls.NeuGroupBox ngbQuerySet;
        private System.Windows.Forms.TabControl tcExecOrder;
        private System.Windows.Forms.TabPage tbLongExecOrder;
        private System.Windows.Forms.TabPage tbShortExecOrder;
        private FS.SOC.Windows.Forms.FpSpread fpLongExecOrder;
        private FarPoint.Win.Spread.SheetView fpLongExecOrder_Sheet1;
        private FS.SOC.Windows.Forms.FpSpread fpSpread1;
        private FS.SOC.Windows.Forms.FpSpread fpSpread2;
        private FarPoint.Win.Spread.SheetView fpShortExecOrder_Sheet1;
        private FS.SOC.Windows.Forms.FpSpread fpShortExecOrder;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblPatientInfo;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbEmergency;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox ncmbType;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbAST;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbHide;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbNew;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbToDay;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbInvalid;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbValid;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbShort;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ncbLong;
    }
}
