namespace IBorn.SI.GuangZhou.Controls
{
    partial class ucGetSiRegInfoInPatient
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
            this.pnTop = new System.Windows.Forms.Panel();
            this.btCanceL = new System.Windows.Forms.Button();
            this.btOK = new System.Windows.Forms.Button();
            this.btQuery = new System.Windows.Forms.Button();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.dtBeginTime = new System.Windows.Forms.DateTimePicker();
            this.cmbQueryType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.pnMain = new System.Windows.Forms.Panel();
            this.fpSpread = new FarPoint.Win.Spread.FpSpread();
            this.fpSpread_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.pnTop.SuspendLayout();
            this.pnMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnTop
            // 
            this.pnTop.Controls.Add(this.btCanceL);
            this.pnTop.Controls.Add(this.btOK);
            this.pnTop.Controls.Add(this.btQuery);
            this.pnTop.Controls.Add(this.txtInput);
            this.pnTop.Controls.Add(this.dtBeginTime);
            this.pnTop.Controls.Add(this.cmbQueryType);
            this.pnTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTop.Location = new System.Drawing.Point(0, 0);
            this.pnTop.Name = "pnTop";
            this.pnTop.Size = new System.Drawing.Size(1004, 40);
            this.pnTop.TabIndex = 0;
            // 
            // btCanceL
            // 
            this.btCanceL.Location = new System.Drawing.Point(672, 8);
            this.btCanceL.Name = "btCanceL";
            this.btCanceL.Size = new System.Drawing.Size(60, 25);
            this.btCanceL.TabIndex = 5;
            this.btCanceL.Text = "取消";
            this.btCanceL.Click += new System.EventHandler(this.btCanceL_Click);
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(597, 8);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(60, 25);
            this.btOK.TabIndex = 4;
            this.btOK.Text = "确定";
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btQuery
            // 
            this.btQuery.Location = new System.Drawing.Point(522, 8);
            this.btQuery.Name = "btQuery";
            this.btQuery.Size = new System.Drawing.Size(60, 25);
            this.btQuery.TabIndex = 3;
            this.btQuery.Text = "查询";
            this.btQuery.Click += new System.EventHandler(this.btQuery_Click);
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(255, 10);
            this.txtInput.MaxLength = 0;
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(139, 21);
            this.txtInput.TabIndex = 2;
            this.txtInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInput_KeyDown);
            // 
            // dtBeginTime
            // 
            this.dtBeginTime.CustomFormat = "yyyy-MM-dd";
            this.dtBeginTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtBeginTime.Location = new System.Drawing.Point(15, 9);
            this.dtBeginTime.Name = "dtBeginTime";
            this.dtBeginTime.Size = new System.Drawing.Size(102, 21);
            this.dtBeginTime.TabIndex = 1;
            // 
            // cmbQueryType
            // 
            this.cmbQueryType.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbQueryType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbQueryType.IsEnter2Tab = false;
            this.cmbQueryType.IsFlat = false;
            this.cmbQueryType.IsLike = true;
            this.cmbQueryType.IsListOnly = false;
            this.cmbQueryType.IsPopForm = true;
            this.cmbQueryType.IsShowCustomerList = true;
            this.cmbQueryType.IsShowID = false;
            this.cmbQueryType.IsShowIDAndName = true;
            this.cmbQueryType.Location = new System.Drawing.Point(149, 10);
            this.cmbQueryType.Name = "cmbQueryType";
            this.cmbQueryType.ShowCustomerList = true;
            this.cmbQueryType.ShowID = false;
            this.cmbQueryType.Size = new System.Drawing.Size(100, 20);
            this.cmbQueryType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbQueryType.TabIndex = 0;
            this.cmbQueryType.Tag = "";
            this.cmbQueryType.ToolBarUse = false;
            // 
            // pnMain
            // 
            this.pnMain.Controls.Add(this.fpSpread);
            this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnMain.Location = new System.Drawing.Point(0, 40);
            this.pnMain.Name = "pnMain";
            this.pnMain.Size = new System.Drawing.Size(1004, 360);
            this.pnMain.TabIndex = 1;
            // 
            // fpSpread
            // 
            this.fpSpread.About = "3.0.2004.2005";
            this.fpSpread.AccessibleDescription = "fpSpread, Sheet1";
            this.fpSpread.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.fpSpread.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread.Location = new System.Drawing.Point(0, 0);
            this.fpSpread.Name = "fpSpread";
            this.fpSpread.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread_Sheet1});
            this.fpSpread.Size = new System.Drawing.Size(1004, 360);
            this.fpSpread.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread.TextTipAppearance = tipAppearance1;
            this.fpSpread.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpSpread_CellDoubleClick);
            this.fpSpread.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fpSpread_KeyDown);
            // 
            // fpSpread_Sheet1
            // 
            this.fpSpread_Sheet1.Reset();
            this.fpSpread_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread_Sheet1.ColumnCount = 14;
            this.fpSpread_Sheet1.RowCount = 0;
            this.fpSpread_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "就医登记号";
            this.fpSpread_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "医院编号";
            this.fpSpread_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "证件号";
            this.fpSpread_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "患者姓名";
            this.fpSpread_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "单位";
            this.fpSpread_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "性别";
            this.fpSpread_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "出生日期";
            this.fpSpread_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "人员类别";
            this.fpSpread_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "门诊号";
            this.fpSpread_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "就诊类别";
            this.fpSpread_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "登记时间";
            this.fpSpread_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "登记诊断";
            this.fpSpread_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "ICD码";
            this.fpSpread_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "看诊科室";
            this.fpSpread_Sheet1.Columns.Get(0).Label = "就医登记号";
            this.fpSpread_Sheet1.Columns.Get(0).Locked = true;
            this.fpSpread_Sheet1.Columns.Get(0).Width = 120F;
            this.fpSpread_Sheet1.Columns.Get(1).Label = "医院编号";
            this.fpSpread_Sheet1.Columns.Get(1).Locked = true;
            this.fpSpread_Sheet1.Columns.Get(1).Width = 40F;
            this.fpSpread_Sheet1.Columns.Get(2).Label = "证件号";
            this.fpSpread_Sheet1.Columns.Get(2).Locked = true;
            this.fpSpread_Sheet1.Columns.Get(2).Width = 120F;
            this.fpSpread_Sheet1.Columns.Get(3).Label = "患者姓名";
            this.fpSpread_Sheet1.Columns.Get(3).Locked = true;
            this.fpSpread_Sheet1.Columns.Get(3).Width = 72F;
            this.fpSpread_Sheet1.Columns.Get(4).Label = "单位";
            this.fpSpread_Sheet1.Columns.Get(4).Locked = true;
            this.fpSpread_Sheet1.Columns.Get(4).Width = 169F;
            this.fpSpread_Sheet1.Columns.Get(5).Label = "性别";
            this.fpSpread_Sheet1.Columns.Get(5).Locked = true;
            this.fpSpread_Sheet1.Columns.Get(6).Label = "出生日期";
            this.fpSpread_Sheet1.Columns.Get(6).Locked = true;
            this.fpSpread_Sheet1.Columns.Get(6).Width = 83F;
            this.fpSpread_Sheet1.Columns.Get(7).Label = "人员类别";
            this.fpSpread_Sheet1.Columns.Get(7).Locked = true;
            this.fpSpread_Sheet1.Columns.Get(7).Width = 74F;
            this.fpSpread_Sheet1.Columns.Get(8).Label = "门诊号";
            this.fpSpread_Sheet1.Columns.Get(8).Locked = true;
            this.fpSpread_Sheet1.Columns.Get(8).Width = 68F;
            this.fpSpread_Sheet1.Columns.Get(9).Label = "就诊类别";
            this.fpSpread_Sheet1.Columns.Get(9).Locked = true;
            this.fpSpread_Sheet1.Columns.Get(10).Label = "登记时间";
            this.fpSpread_Sheet1.Columns.Get(10).Locked = true;
            this.fpSpread_Sheet1.Columns.Get(10).Width = 100F;
            this.fpSpread_Sheet1.Columns.Get(11).Label = "登记诊断";
            this.fpSpread_Sheet1.Columns.Get(11).Locked = true;
            this.fpSpread_Sheet1.Columns.Get(11).Width = 148F;
            this.fpSpread_Sheet1.Columns.Get(12).Label = "ICD码";
            this.fpSpread_Sheet1.Columns.Get(12).Locked = true;
            this.fpSpread_Sheet1.Columns.Get(12).Width = 74F;
            this.fpSpread_Sheet1.Columns.Get(13).Label = "看诊科室";
            this.fpSpread_Sheet1.Columns.Get(13).Locked = true;
            this.fpSpread_Sheet1.Columns.Get(13).Width = 79F;
            this.fpSpread_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpSpread_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpSpread.SetActiveViewport(0, 1, 0);
            // 
            // ucGetSiRegInfoOutPatient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnMain);
            this.Controls.Add(this.pnTop);
            this.Name = "ucGetSiRegInfoOutPatient";
            this.Size = new System.Drawing.Size(1004, 400);
            this.Load += new System.EventHandler(this.ucGetSiRegInfo_Load);
            this.pnTop.ResumeLayout(false);
            this.pnTop.PerformLayout();
            this.pnMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnTop;
        private System.Windows.Forms.Panel pnMain;        
        private System.Windows.Forms.DateTimePicker dtBeginTime;
        private System.Windows.Forms.TextBox txtInput;          
        private System.Windows.Forms.Button btCanceL;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btQuery;
        private FarPoint.Win.Spread.FpSpread fpSpread;
        private FarPoint.Win.Spread.SheetView fpSpread_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbQueryType;
    }
}
