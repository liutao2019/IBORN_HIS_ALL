namespace GZSI.Controls
{
    partial class frmUploadDetail : FS.FrameWork.WinForms.Forms.BaseForm
    {
        /// <summary>
        /// 必需的设计器变量。

        /// </summary>
        private FS.HISFC.Components.Common.Controls.ucQueryInpatientNo tbPatientNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblPatientInfo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox tbUndrugCount;
        private System.Windows.Forms.TextBox tbDrugCount;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtCost;
        private System.Windows.Forms.ToolBar toolBar1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ImageList imageList32;
        private System.Windows.Forms.ToolBarButton tbUpload;
        private System.Windows.Forms.ToolBarButton tbCancel;
        private System.Windows.Forms.ToolBarButton toolBarButton1;
        private System.Windows.Forms.ToolBarButton tbDelete;
        private System.Windows.Forms.ToolBarButton tbQuit;
        private System.Windows.Forms.ImageList imageList16;
        private System.Windows.Forms.Button btnChangedate;
        private System.Windows.Forms.DateTimePicker dtBegin;
        private System.Windows.Forms.DateTimePicker dtEnd;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ToolBarButton tbQurey;
        private System.ComponentModel.IContainer components;
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
        #region Windows 窗体设计器生成的代码
        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改

        /// 此方法的内容。

        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUploadDetail));
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            this.tbPatientNo = new FS.HISFC.Components.Common.Controls.ucQueryInpatientNo();
            this.label2 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btn90day = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dtEnd = new System.Windows.Forms.DateTimePicker();
            this.dtBegin = new System.Windows.Forms.DateTimePicker();
            this.btnChangedate = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.tbQurey = new System.Windows.Forms.ToolBarButton();
            this.tbUpload = new System.Windows.Forms.ToolBarButton();
            this.tbCancel = new System.Windows.Forms.ToolBarButton();
            this.tbDelete = new System.Windows.Forms.ToolBarButton();
            this.tbBalance = new System.Windows.Forms.ToolBarButton();
            this.tbAlter = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.tbQuit = new System.Windows.Forms.ToolBarButton();
            this.imageList32 = new System.Windows.Forms.ImageList(this.components);
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbUndrugCount = new System.Windows.Forms.TextBox();
            this.tbDrugCount = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCost = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPatientInfo = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbldiag = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // tbPatientNo
            // 
            this.tbPatientNo.DefaultInputType = 0;
            this.tbPatientNo.InputType = 0;
            this.tbPatientNo.IsDeptOnly = true;
            this.tbPatientNo.Location = new System.Drawing.Point(8, 56);
            this.tbPatientNo.Name = "tbPatientNo";
            this.tbPatientNo.PatientInState = "ALL";
            this.tbPatientNo.ShowState = FS.HISFC.Components.Common.Controls.enuShowState.All;
            this.tbPatientNo.Size = new System.Drawing.Size(160, 32);
            this.tbPatientNo.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(168, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "患者姓名:";
            this.label2.Visible = false;
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(248, 58);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(80, 21);
            this.tbName.TabIndex = 5;
            this.tbName.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btn90day);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.dtEnd);
            this.panel1.Controls.Add(this.dtBegin);
            this.panel1.Controls.Add(this.btnChangedate);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.toolBar1);
            this.panel1.Controls.Add(this.tbPatientNo);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tbName);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1028, 88);
            this.panel1.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(566, 57);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(49, 23);
            this.button1.TabIndex = 21;
            this.button1.Text = "30天";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn90day
            // 
            this.btn90day.Location = new System.Drawing.Point(511, 56);
            this.btn90day.Name = "btn90day";
            this.btn90day.Size = new System.Drawing.Size(49, 23);
            this.btn90day.TabIndex = 20;
            this.btn90day.Text = "90天";
            this.btn90day.UseVisualStyleBackColor = true;
            this.btn90day.Visible = false;
            this.btn90day.Click += new System.EventHandler(this.btn90day_Click);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(344, 56);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(16, 23);
            this.label10.TabIndex = 19;
            this.label10.Text = "至";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(168, 56);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(16, 23);
            this.label9.TabIndex = 18;
            this.label9.Text = "从";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtEnd
            // 
            this.dtEnd.CustomFormat = "yyy-MM-dd HH:mm:ss";
            this.dtEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEnd.Location = new System.Drawing.Point(368, 56);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(136, 21);
            this.dtEnd.TabIndex = 17;
            // 
            // dtBegin
            // 
            this.dtBegin.CustomFormat = "yyy-MM-dd HH:mm:ss";
            this.dtBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtBegin.Location = new System.Drawing.Point(192, 56);
            this.dtBegin.Name = "dtBegin";
            this.dtBegin.Size = new System.Drawing.Size(136, 21);
            this.dtBegin.TabIndex = 16;
            // 
            // btnChangedate
            // 
            this.btnChangedate.Location = new System.Drawing.Point(650, 56);
            this.btnChangedate.Name = "btnChangedate";
            this.btnChangedate.Size = new System.Drawing.Size(75, 23);
            this.btnChangedate.TabIndex = 15;
            this.btnChangedate.Text = "修改日期";
            this.btnChangedate.Click += new System.EventHandler(this.btnChangedate_Click);
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Location = new System.Drawing.Point(0, 51);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1028, 2);
            this.label8.TabIndex = 14;
            // 
            // toolBar1
            // 
            this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.tbQurey,
            this.tbUpload,
            this.tbCancel,
            this.tbDelete,
            this.tbBalance,
            this.tbAlter,
            this.toolBarButton1,
            this.tbQuit});
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.imageList32;
            this.toolBar1.Location = new System.Drawing.Point(0, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(1028, 51);
            this.toolBar1.TabIndex = 13;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // tbQurey
            // 
            this.tbQurey.ImageIndex = 1;
            this.tbQurey.Name = "tbQurey";
            this.tbQurey.Text = "查询";
            // 
            // tbUpload
            // 
            this.tbUpload.ImageIndex = 0;
            this.tbUpload.Name = "tbUpload";
            this.tbUpload.Text = "上传";
            // 
            // tbCancel
            // 
            this.tbCancel.ImageIndex = 2;
            this.tbCancel.Name = "tbCancel";
            this.tbCancel.Text = "取消上传";
            // 
            // tbDelete
            // 
            this.tbDelete.ImageIndex = 3;
            this.tbDelete.Name = "tbDelete";
            this.tbDelete.Text = "删除已上传";
            // 
            // tbBalance
            // 
            this.tbBalance.ImageIndex = 5;
            this.tbBalance.Name = "tbBalance";
            this.tbBalance.Text = "结算";
            // 
            // tbAlter
            // 
            this.tbAlter.ImageIndex = 5;
            this.tbAlter.Name = "tbAlter";
            this.tbAlter.Text = "重选医保患者";
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbQuit
            // 
            this.tbQuit.ImageIndex = 4;
            this.tbQuit.Name = "tbQuit";
            this.tbQuit.Text = "退出";
            // 
            // imageList32
            // 
            this.imageList32.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList32.ImageStream")));
            this.imageList32.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList32.Images.SetKeyName(0, "手动录入24.ico");
            this.imageList32.Images.SetKeyName(1, "查询.ico");
            this.imageList32.Images.SetKeyName(2, "取消.ico");
            this.imageList32.Images.SetKeyName(3, "删除.ico");
            this.imageList32.Images.SetKeyName(4, "退出.ico");
            this.imageList32.Images.SetKeyName(5, "Check.ico");
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(722, 56);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 9;
            this.btnOk.Text = "上传(&U)";
            this.btnOk.Visible = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(794, 56);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(112, 23);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "取消上传标记(&C)";
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(906, 56);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 23);
            this.btnDelete.TabIndex = 11;
            this.btnDelete.Text = "删除共享区记录(&D)";
            this.btnDelete.Visible = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(962, 56);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 10;
            this.btnExit.Text = "退出(&X)";
            this.btnExit.Visible = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbldiag);
            this.groupBox1.Controls.Add(this.txtFilter);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.tbUndrugCount);
            this.groupBox1.Controls.Add(this.tbDrugCount);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtCost);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblPatientInfo);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 88);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1028, 103);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "费用明细信息";
            // 
            // txtFilter
            // 
            this.txtFilter.BackColor = System.Drawing.Color.White;
            this.txtFilter.Location = new System.Drawing.Point(68, 78);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(329, 21);
            this.txtFilter.TabIndex = 13;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 81);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 12;
            this.label11.Text = "过滤条件";
            this.label11.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // tbUndrugCount
            // 
            this.tbUndrugCount.BackColor = System.Drawing.Color.White;
            this.tbUndrugCount.Location = new System.Drawing.Point(603, 77);
            this.tbUndrugCount.Name = "tbUndrugCount";
            this.tbUndrugCount.ReadOnly = true;
            this.tbUndrugCount.Size = new System.Drawing.Size(52, 21);
            this.tbUndrugCount.TabIndex = 7;
            // 
            // tbDrugCount
            // 
            this.tbDrugCount.BackColor = System.Drawing.Color.White;
            this.tbDrugCount.Location = new System.Drawing.Point(474, 77);
            this.tbDrugCount.Name = "tbDrugCount";
            this.tbDrugCount.ReadOnly = true;
            this.tbDrugCount.Size = new System.Drawing.Size(52, 21);
            this.tbDrugCount.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(532, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "非药品条目";
            this.label5.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(403, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "药 品 条目";
            this.label4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtCost
            // 
            this.txtCost.BackColor = System.Drawing.Color.White;
            this.txtCost.Location = new System.Drawing.Point(720, 77);
            this.txtCost.Name = "txtCost";
            this.txtCost.ReadOnly = true;
            this.txtCost.Size = new System.Drawing.Size(100, 21);
            this.txtCost.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(661, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "上传金额";
            this.label3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblPatientInfo
            // 
            this.lblPatientInfo.AutoSize = true;
            this.lblPatientInfo.Location = new System.Drawing.Point(12, 28);
            this.lblPatientInfo.Name = "lblPatientInfo";
            this.lblPatientInfo.Size = new System.Drawing.Size(65, 12);
            this.lblPatientInfo.TabIndex = 0;
            this.lblPatientInfo.Text = "患者信息：";
            this.lblPatientInfo.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.fpSpread1);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 191);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1028, 490);
            this.panel2.TabIndex = 8;
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "3.0.2004.2005";
            this.fpSpread1.AccessibleDescription = "fpSpread1, Sheet1, Row 0, Column 0, ";
            this.fpSpread1.BackColor = System.Drawing.Color.White;
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread1.Location = new System.Drawing.Point(0, 2);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1});
            this.fpSpread1.Size = new System.Drawing.Size(1028, 488);
            this.fpSpread1.TabIndex = 2;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread1.TextTipAppearance = tipAppearance1;
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.ColumnCount = 14;
            this.fpSpread1_Sheet1.RowCount = 0;
            this.fpSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "是否上传";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "住院号";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "医保登记号";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "项目代码";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "项目编码";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "项目名称";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "规格";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "项目价格";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "数量";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "金额";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "费用日期";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "对照?";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "拼音码";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "五笔码";
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.fpSpread1_Sheet1.Columns.Get(0).Label = "是否上传";
            this.fpSpread1_Sheet1.Columns.Get(0).Visible = false;
            this.fpSpread1_Sheet1.Columns.Get(0).Width = 35F;
            this.fpSpread1_Sheet1.Columns.Get(2).Label = "医保登记号";
            this.fpSpread1_Sheet1.Columns.Get(2).Width = 139F;
            this.fpSpread1_Sheet1.Columns.Get(3).AllowAutoSort = true;
            this.fpSpread1_Sheet1.Columns.Get(3).Label = "项目代码";
            this.fpSpread1_Sheet1.Columns.Get(3).Width = 81F;
            this.fpSpread1_Sheet1.Columns.Get(4).Label = "项目编码";
            this.fpSpread1_Sheet1.Columns.Get(4).Width = 82F;
            this.fpSpread1_Sheet1.Columns.Get(5).AllowAutoSort = true;
            this.fpSpread1_Sheet1.Columns.Get(5).Label = "项目名称";
            this.fpSpread1_Sheet1.Columns.Get(5).Width = 84F;
            this.fpSpread1_Sheet1.Columns.Get(11).AllowAutoSort = true;
            this.fpSpread1_Sheet1.Columns.Get(11).Label = "对照?";
            this.fpSpread1_Sheet1.Columns.Get(12).Label = "拼音码";
            this.fpSpread1_Sheet1.Columns.Get(12).Visible = false;
            this.fpSpread1_Sheet1.Columns.Get(13).Label = "五笔码";
            this.fpSpread1_Sheet1.Columns.Get(13).Visible = false;
            this.fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.Rows.Default.Height = 22F;
            this.fpSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSpread1_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.fpSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpSpread1.SetActiveViewport(0, 1, 0);
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1028, 2);
            this.panel3.TabIndex = 0;
            // 
            // lbldiag
            // 
            this.lbldiag.AutoSize = true;
            this.lbldiag.Location = new System.Drawing.Point(12, 52);
            this.lbldiag.Name = "lbldiag";
            this.lbldiag.Size = new System.Drawing.Size(53, 12);
            this.lbldiag.TabIndex = 14;
            this.lbldiag.Text = "诊断信息";
            this.lbldiag.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // frmUploadDetail
            // 
            this.ClientSize = new System.Drawing.Size(1028, 681);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "frmUploadDetail";
            this.Text = "医保明细上传";
            this.Load += new System.EventHandler(this.frmUploadDetail_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.ToolBarButton tbAlter;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btn90day;
        private FarPoint.Win.Spread.FpSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolBarButton tbBalance;
        private System.Windows.Forms.Label lbldiag;
    }
}