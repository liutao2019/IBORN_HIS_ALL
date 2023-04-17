namespace FS.SOC.Local.OutpatientFee.NanZhuang
{
    partial class ucCooperatePatientMaintenance
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
            FarPoint.Win.Spread.CellType.DateTimeCellType dateTimeCellType1 = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucCooperatePatientMaintenance));
            FarPoint.Win.Spread.CellType.DateTimeCellType dateTimeCellType2 = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            FarPoint.Win.Spread.CellType.DateTimeCellType dateTimeCellType3 = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbDepartment = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbStateFlag = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.dtpBeginDate = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.cmbSex = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.dtpEndDate = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.dtpBrithday = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSiNO = new System.Windows.Forms.TextBox();
            this.lbsiNo = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lbPact = new System.Windows.Forms.Label();
            this.txtIdenNO = new System.Windows.Forms.TextBox();
            this.lbIDenNO = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lbName = new System.Windows.Forms.Label();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Panel1.Controls.Add(this.neuLabel1);
            this.splitContainer1.Panel1.Controls.Add(this.cmbDepartment);
            this.splitContainer1.Panel1.Controls.Add(this.cmbStateFlag);
            this.splitContainer1.Panel1.Controls.Add(this.dtpBeginDate);
            this.splitContainer1.Panel1.Controls.Add(this.cmbSex);
            this.splitContainer1.Panel1.Controls.Add(this.dtpEndDate);
            this.splitContainer1.Panel1.Controls.Add(this.dtpBrithday);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.txtSiNO);
            this.splitContainer1.Panel1.Controls.Add(this.lbsiNo);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.txtAddress);
            this.splitContainer1.Panel1.Controls.Add(this.lbPact);
            this.splitContainer1.Panel1.Controls.Add(this.txtIdenNO);
            this.splitContainer1.Panel1.Controls.Add(this.lbIDenNO);
            this.splitContainer1.Panel1.Controls.Add(this.txtName);
            this.splitContainer1.Panel1.Controls.Add(this.lbName);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.neuSpread1);
            this.splitContainer1.Size = new System.Drawing.Size(773, 507);
            this.splitContainer1.SplitterDistance = 206;
            this.splitContainer1.TabIndex = 0;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.ForeColor = System.Drawing.Color.Red;
            this.neuLabel1.Location = new System.Drawing.Point(27, 12);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(443, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 62;
            this.neuLabel1.Text = "温馨提示：保存时蓝色为必选项   查询时以姓名和身份证号查询，为空时查询全部";
            // 
            // cmbDepartment
            // 
            this.cmbDepartment.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbDepartment.FormattingEnabled = true;
            this.cmbDepartment.IsEnter2Tab = false;
            this.cmbDepartment.IsFlat = false;
            this.cmbDepartment.IsLike = true;
            this.cmbDepartment.IsListOnly = false;
            this.cmbDepartment.IsPopForm = true;
            this.cmbDepartment.IsShowCustomerList = false;
            this.cmbDepartment.IsShowID = false;
            this.cmbDepartment.Location = new System.Drawing.Point(624, 37);
            this.cmbDepartment.Name = "cmbDepartment";
            this.cmbDepartment.PopForm = null;
            this.cmbDepartment.ShowCustomerList = false;
            this.cmbDepartment.ShowID = false;
            this.cmbDepartment.Size = new System.Drawing.Size(120, 20);
            this.cmbDepartment.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbDepartment.TabIndex = 3;
            this.cmbDepartment.Tag = "";
            this.cmbDepartment.ToolBarUse = false;
            // 
            // cmbStateFlag
            // 
            this.cmbStateFlag.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbStateFlag.FormattingEnabled = true;
            this.cmbStateFlag.IsEnter2Tab = false;
            this.cmbStateFlag.IsFlat = false;
            this.cmbStateFlag.IsLike = true;
            this.cmbStateFlag.IsListOnly = false;
            this.cmbStateFlag.IsPopForm = true;
            this.cmbStateFlag.IsShowCustomerList = false;
            this.cmbStateFlag.IsShowID = false;
            this.cmbStateFlag.ItemHeight = 12;
            this.cmbStateFlag.Location = new System.Drawing.Point(624, 176);
            this.cmbStateFlag.Name = "cmbStateFlag";
            this.cmbStateFlag.PopForm = null;
            this.cmbStateFlag.ShowCustomerList = false;
            this.cmbStateFlag.ShowID = false;
            this.cmbStateFlag.Size = new System.Drawing.Size(120, 20);
            this.cmbStateFlag.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbStateFlag.TabIndex = 8;
            this.cmbStateFlag.Tag = "";
            this.cmbStateFlag.ToolBarUse = false;
            // 
            // dtpBeginDate
            // 
            this.dtpBeginDate.CustomFormat = "yyyy-MM-dd";
            this.dtpBeginDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBeginDate.IsEnter2Tab = false;
            this.dtpBeginDate.Location = new System.Drawing.Point(98, 174);
            this.dtpBeginDate.Name = "dtpBeginDate";
            this.dtpBeginDate.Size = new System.Drawing.Size(91, 21);
            this.dtpBeginDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpBeginDate.TabIndex = 6;
            // 
            // cmbSex
            // 
            this.cmbSex.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbSex.FormattingEnabled = true;
            this.cmbSex.IsEnter2Tab = false;
            this.cmbSex.IsFlat = false;
            this.cmbSex.IsLike = true;
            this.cmbSex.IsListOnly = false;
            this.cmbSex.IsPopForm = true;
            this.cmbSex.IsShowCustomerList = false;
            this.cmbSex.IsShowID = false;
            this.cmbSex.ItemHeight = 12;
            this.cmbSex.Items.AddRange(new object[] {
            "男",
            "女",
            "未知"});
            this.cmbSex.Location = new System.Drawing.Point(359, 85);
            this.cmbSex.Name = "cmbSex";
            this.cmbSex.PopForm = null;
            this.cmbSex.ShowCustomerList = false;
            this.cmbSex.ShowID = false;
            this.cmbSex.Size = new System.Drawing.Size(146, 20);
            this.cmbSex.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbSex.TabIndex = 9;
            this.cmbSex.Tag = "";
            this.cmbSex.ToolBarUse = false;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CustomFormat = "yyyy-MM-dd";
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.IsEnter2Tab = false;
            this.dtpEndDate.Location = new System.Drawing.Point(358, 174);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(92, 21);
            this.dtpEndDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpEndDate.TabIndex = 7;
            // 
            // dtpBrithday
            // 
            this.dtpBrithday.CustomFormat = "yyyy-MM-dd";
            this.dtpBrithday.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBrithday.IsEnter2Tab = false;
            this.dtpBrithday.Location = new System.Drawing.Point(625, 80);
            this.dtpBrithday.Name = "dtpBrithday";
            this.dtpBrithday.Size = new System.Drawing.Size(119, 21);
            this.dtpBrithday.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpBrithday.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(287, 179);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 60;
            this.label7.Text = "结束时间：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 179);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 59;
            this.label6.Text = "开始时间：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(288, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 54;
            this.label5.Text = "性    别：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(554, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 58;
            this.label4.Text = "出生日期：";
            // 
            // txtSiNO
            // 
            this.txtSiNO.Location = new System.Drawing.Point(98, 80);
            this.txtSiNO.Name = "txtSiNO";
            this.txtSiNO.Size = new System.Drawing.Size(129, 21);
            this.txtSiNO.TabIndex = 4;
            // 
            // lbsiNo
            // 
            this.lbsiNo.AutoSize = true;
            this.lbsiNo.Location = new System.Drawing.Point(27, 83);
            this.lbsiNo.Name = "lbsiNo";
            this.lbsiNo.Size = new System.Drawing.Size(65, 12);
            this.lbsiNo.TabIndex = 56;
            this.lbsiNo.Text = "社 保 号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(553, 179);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 61;
            this.label2.Text = "状    态：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(553, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 57;
            this.label1.Text = "所属单位：";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(99, 127);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(645, 21);
            this.txtAddress.TabIndex = 5;
            // 
            // lbPact
            // 
            this.lbPact.AutoSize = true;
            this.lbPact.Location = new System.Drawing.Point(27, 130);
            this.lbPact.Name = "lbPact";
            this.lbPact.Size = new System.Drawing.Size(65, 12);
            this.lbPact.TabIndex = 55;
            this.lbPact.Text = "住    址：";
            // 
            // txtIdenNO
            // 
            this.txtIdenNO.Location = new System.Drawing.Point(359, 35);
            this.txtIdenNO.Name = "txtIdenNO";
            this.txtIdenNO.Size = new System.Drawing.Size(146, 21);
            this.txtIdenNO.TabIndex = 2;
            // 
            // lbIDenNO
            // 
            this.lbIDenNO.AutoSize = true;
            this.lbIDenNO.ForeColor = System.Drawing.Color.Blue;
            this.lbIDenNO.Location = new System.Drawing.Point(288, 39);
            this.lbIDenNO.Name = "lbIDenNO";
            this.lbIDenNO.Size = new System.Drawing.Size(65, 12);
            this.lbIDenNO.TabIndex = 52;
            this.lbIDenNO.Text = "身份证号：";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(98, 36);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(129, 21);
            this.txtName.TabIndex = 1;
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.ForeColor = System.Drawing.Color.Blue;
            this.lbName.Location = new System.Drawing.Point(28, 39);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(65, 12);
            this.lbName.TabIndex = 50;
            this.lbName.Text = "姓    名：";
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, ";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(0, 0);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(773, 297);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.neuSpread1_CellDoubleClick);
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ColumnCount = 10;
            this.neuSpread1_Sheet1.RowCount = 1;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "姓名";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "身份证";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "社保号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "出生日期";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "性别";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "开始时间";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "结束时间";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "状态";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "地址";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "所属单位";
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "身份证";
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 122F;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "社保号";
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 73F;
            dateTimeCellType1.Calendar = ((System.Globalization.Calendar)(resources.GetObject("dateTimeCellType1.Calendar")));
            dateTimeCellType1.CalendarDayFont = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dateTimeCellType1.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText;
            dateTimeCellType1.DateDefault = new System.DateTime(2011, 12, 21, 17, 2, 10, 0);
            dateTimeCellType1.TimeDefault = new System.DateTime(2011, 12, 21, 17, 2, 10, 0);
            this.neuSpread1_Sheet1.Columns.Get(3).CellType = dateTimeCellType1;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "出生日期";
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 78F;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "性别";
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 36F;
            dateTimeCellType2.Calendar = ((System.Globalization.Calendar)(resources.GetObject("dateTimeCellType2.Calendar")));
            dateTimeCellType2.CalendarDayFont = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dateTimeCellType2.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText;
            dateTimeCellType2.DateDefault = new System.DateTime(2011, 12, 21, 17, 2, 23, 0);
            dateTimeCellType2.TimeDefault = new System.DateTime(2011, 12, 21, 17, 2, 23, 0);
            this.neuSpread1_Sheet1.Columns.Get(5).CellType = dateTimeCellType2;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "开始时间";
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 73F;
            dateTimeCellType3.Calendar = ((System.Globalization.Calendar)(resources.GetObject("dateTimeCellType3.Calendar")));
            dateTimeCellType3.CalendarDayFont = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dateTimeCellType3.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText;
            dateTimeCellType3.DateDefault = new System.DateTime(2011, 12, 21, 17, 2, 35, 0);
            dateTimeCellType3.TimeDefault = new System.DateTime(2011, 12, 21, 17, 2, 35, 0);
            this.neuSpread1_Sheet1.Columns.Get(6).CellType = dateTimeCellType3;
            this.neuSpread1_Sheet1.Columns.Get(6).Label = "结束时间";
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 80F;
            this.neuSpread1_Sheet1.Columns.Get(7).Label = "状态";
            this.neuSpread1_Sheet1.Columns.Get(7).Width = 35F;
            this.neuSpread1_Sheet1.Columns.Get(8).Label = "地址";
            this.neuSpread1_Sheet1.Columns.Get(8).Width = 157F;
            this.neuSpread1_Sheet1.DefaultStyle.Locked = true;
            this.neuSpread1_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ucCooperatePatientMaintenance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ucCooperatePatientMaintenance";
            this.Size = new System.Drawing.Size(773, 507);
            this.Load += new System.EventHandler(this.ucCooperatePatientMaintenance_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lbPact;
        private System.Windows.Forms.TextBox txtIdenNO;
        private System.Windows.Forms.Label lbIDenNO;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSiNO;
        private System.Windows.Forms.Label lbsiNo;
        private System.Windows.Forms.Label label2;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpBrithday;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpEndDate;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbSex;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpBeginDate;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbStateFlag;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDepartment;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;


    }
}
