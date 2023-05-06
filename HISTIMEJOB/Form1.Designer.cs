namespace HISTIMEJOB
{
    /// <summary>
    /// frmHisTimeJob<br></br>
    /// [功能描述: his定时作业任务]<br></br>
    /// [创 建 者: 王儒超]<br></br>
    /// [创建时间: 2007-1-9]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    partial class frmHisTimeJob
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。

        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.DateTimeCellType dateTimeCellType1 = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHisTimeJob));
            FarPoint.Win.Spread.CellType.DateTimeCellType dateTimeCellType2 = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            this.pnlMain = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.pnlDown = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnExit = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnHide = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnSave = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.rtbLogo = new FS.FrameWork.WinForms.Controls.NeuRichTextBox();
            this.pnlUP = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.fpJob = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpJob_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.lblCurrDateTime = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.timerJob = new System.Windows.Forms.Timer(this.components);
            this.pnlMain.SuspendLayout();
            this.pnlDown.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            this.neuPanel1.SuspendLayout();
            this.pnlUP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpJob)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpJob_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.MistyRose;
            this.pnlMain.Controls.Add(this.pnlDown);
            this.pnlMain.Controls.Add(this.pnlUP);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(706, 464);
            this.pnlMain.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlMain.TabIndex = 0;
            // 
            // pnlDown
            // 
            this.pnlDown.BackColor = System.Drawing.Color.Cyan;
            this.pnlDown.Controls.Add(this.neuPanel2);
            this.pnlDown.Controls.Add(this.neuPanel1);
            this.pnlDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDown.Location = new System.Drawing.Point(0, 214);
            this.pnlDown.Name = "pnlDown";
            this.pnlDown.Size = new System.Drawing.Size(706, 250);
            this.pnlDown.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlDown.TabIndex = 1;
            // 
            // neuPanel2
            // 
            this.neuPanel2.BackColor = System.Drawing.SystemColors.Control;
            this.neuPanel2.Controls.Add(this.button2);
            this.neuPanel2.Controls.Add(this.button1);
            this.neuPanel2.Controls.Add(this.btnExit);
            this.neuPanel2.Controls.Add(this.btnHide);
            this.neuPanel2.Controls.Add(this.btnSave);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel2.Location = new System.Drawing.Point(0, 202);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(706, 48);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(236, 14);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(93, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "固定费用测试";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(115, 15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "医保上传测试";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            //this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnExit
            // 
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExit.Location = new System.Drawing.Point(621, 15);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(80, 23);
            this.btnExit.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "退出(&X)";
            this.btnExit.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnHide
            // 
            this.btnHide.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHide.Location = new System.Drawing.Point(527, 15);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(80, 23);
            this.btnHide.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnHide.TabIndex = 1;
            this.btnHide.Text = "最小化(&H)";
            this.btnHide.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnHide.UseVisualStyleBackColor = true;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // btnSave
            // 
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(427, 15);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 23);
            this.btnSave.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "保存设置(&S)";
            this.btnSave.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // neuPanel1
            // 
            this.neuPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.neuPanel1.Controls.Add(this.rtbLogo);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(706, 202);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // rtbLogo
            // 
            this.rtbLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbLogo.Location = new System.Drawing.Point(3, 6);
            this.rtbLogo.Name = "rtbLogo";
            this.rtbLogo.Size = new System.Drawing.Size(691, 190);
            this.rtbLogo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.rtbLogo.TabIndex = 1;
            this.rtbLogo.Text = "";
            // 
            // pnlUP
            // 
            this.pnlUP.BackColor = System.Drawing.SystemColors.Control;
            this.pnlUP.Controls.Add(this.fpJob);
            this.pnlUP.Controls.Add(this.lblCurrDateTime);
            this.pnlUP.Controls.Add(this.neuLabel1);
            this.pnlUP.Controls.Add(this.neuGroupBox1);
            this.pnlUP.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlUP.Location = new System.Drawing.Point(0, 0);
            this.pnlUP.Name = "pnlUP";
            this.pnlUP.Size = new System.Drawing.Size(706, 214);
            this.pnlUP.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlUP.TabIndex = 0;
            // 
            // fpJob
            // 
            this.fpJob.About = "3.0.2004.2005";
            this.fpJob.AccessibleDescription = "fpJob, Sheet1, Row 0, Column 0, ";
            this.fpJob.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.fpJob.BackColor = System.Drawing.Color.White;
            this.fpJob.FileName = "";
            this.fpJob.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpJob.IsAutoSaveGridStatus = false;
            this.fpJob.IsCanCustomConfigColumn = false;
            this.fpJob.Location = new System.Drawing.Point(12, 40);
            this.fpJob.Name = "fpJob";
            this.fpJob.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpJob_Sheet1});
            this.fpJob.Size = new System.Drawing.Size(662, 149);
            this.fpJob.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpJob.TabIndex = 2;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpJob.TextTipAppearance = tipAppearance1;
            this.fpJob.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpJob_Sheet1
            // 
            this.fpJob_Sheet1.Reset();
            this.fpJob_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpJob_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpJob_Sheet1.ColumnCount = 8;
            this.fpJob_Sheet1.RowCount = 6;
            this.fpJob_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "代码";
            this.fpJob_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "程序名称";
            this.fpJob_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "状态";
            this.fpJob_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "下次执行时间";
            this.fpJob_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "上次执行时间";
            this.fpJob_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "间隔";
            this.fpJob_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "类型";
            this.fpJob_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "备注";
            this.fpJob_Sheet1.Columns.Get(0).Label = "代码";
            this.fpJob_Sheet1.Columns.Get(0).Locked = true;
            this.fpJob_Sheet1.Columns.Get(0).Width = 103F;
            this.fpJob_Sheet1.Columns.Get(1).Label = "程序名称";
            this.fpJob_Sheet1.Columns.Get(1).Locked = true;
            this.fpJob_Sheet1.Columns.Get(1).Width = 111F;
            this.fpJob_Sheet1.Columns.Get(2).Label = "状态";
            this.fpJob_Sheet1.Columns.Get(2).Locked = false;
            this.fpJob_Sheet1.Columns.Get(2).Width = 79F;
            dateTimeCellType1.Calendar = ((System.Globalization.Calendar)(resources.GetObject("dateTimeCellType1.Calendar")));
            dateTimeCellType1.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText;
            dateTimeCellType1.DateDefault = new System.DateTime(2007, 1, 5, 21, 34, 11, 0);
            dateTimeCellType1.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.ShortDateWithTime;
            dateTimeCellType1.TimeDefault = new System.DateTime(2007, 1, 5, 21, 34, 11, 0);
            this.fpJob_Sheet1.Columns.Get(3).CellType = dateTimeCellType1;
            this.fpJob_Sheet1.Columns.Get(3).Label = "下次执行时间";
            this.fpJob_Sheet1.Columns.Get(3).Width = 85F;
            dateTimeCellType2.Calendar = ((System.Globalization.Calendar)(resources.GetObject("dateTimeCellType2.Calendar")));
            dateTimeCellType2.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText;
            dateTimeCellType2.DateDefault = new System.DateTime(2007, 1, 5, 21, 37, 10, 0);
            dateTimeCellType2.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.ShortDateWithTime;
            dateTimeCellType2.TimeDefault = new System.DateTime(2007, 1, 5, 21, 37, 10, 0);
            this.fpJob_Sheet1.Columns.Get(4).CellType = dateTimeCellType2;
            this.fpJob_Sheet1.Columns.Get(4).Label = "上次执行时间";
            this.fpJob_Sheet1.Columns.Get(4).Width = 89F;
            this.fpJob_Sheet1.Columns.Get(5).Label = "间隔";
            this.fpJob_Sheet1.Columns.Get(5).Width = 53F;
            this.fpJob_Sheet1.Columns.Get(6).Label = "类型";
            this.fpJob_Sheet1.Columns.Get(6).Width = 57F;
            this.fpJob_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.fpJob_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpJob_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // lblCurrDateTime
            // 
            this.lblCurrDateTime.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCurrDateTime.Location = new System.Drawing.Point(83, 6);
            this.lblCurrDateTime.Name = "lblCurrDateTime";
            this.lblCurrDateTime.Size = new System.Drawing.Size(195, 12);
            this.lblCurrDateTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblCurrDateTime.TabIndex = 1;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel1.Location = new System.Drawing.Point(12, 6);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(65, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "当前时间：";
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.neuGroupBox1.ForeColor = System.Drawing.Color.Black;
            this.neuGroupBox1.Location = new System.Drawing.Point(3, 24);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(691, 179);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 3;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "定时运行程序列表";
            // 
            // trayIcon
            // 
            this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
            this.trayIcon.Text = "HIS定时任务";
            this.trayIcon.Visible = true;
            this.trayIcon.DoubleClick += new System.EventHandler(this.trayIcon_DoubleClick);
            // 
            // timerJob
            // 
            this.timerJob.Enabled = true;
            this.timerJob.Interval = 1000;
            this.timerJob.Tick += new System.EventHandler(this.timerJob_Tick);
            // 
            // frmHisTimeJob
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 464);
            this.Controls.Add(this.pnlMain);
            this.KeyPreview = true;
            this.Name = "frmHisTimeJob";
            this.Text = "HIS定时运行程序";
            this.Load += new System.EventHandler(this.frmHisTimeJob_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmHisTimeJob_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmHisTimeJob_FormClosing);
            this.pnlMain.ResumeLayout(false);
            this.pnlDown.ResumeLayout(false);
            this.neuPanel2.ResumeLayout(false);
            this.neuPanel1.ResumeLayout(false);
            this.pnlUP.ResumeLayout(false);
            this.pnlUP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpJob)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpJob_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel pnlMain;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlDown;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlUP;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblCurrDateTime;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpJob;
        private FarPoint.Win.Spread.SheetView fpJob_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuRichTextBox rtbLogo;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuButton btnExit;
        private FS.FrameWork.WinForms.Controls.NeuButton btnHide;
        private FS.FrameWork.WinForms.Controls.NeuButton btnSave;
        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.Timer timerJob;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

