namespace SOC.Local.DrugStore
{
    partial class ucDrugBag
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
            System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("zh-CN", false);
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.panel1 = new System.Windows.Forms.Panel();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbPatientNO = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbDate = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbTime = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbBed = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbDept = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lbPrintDate = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbPage = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPictureBox1 = new FS.FrameWork.WinForms.Controls.NeuPictureBox();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.panel1.SuspendLayout();
            this.neuPanel1.SuspendLayout();
            ( ( System.ComponentModel.ISupportInitialize ) ( this.neuPictureBox1 ) ).BeginInit();
            ( ( System.ComponentModel.ISupportInitialize ) ( this.neuSpread1 ) ).BeginInit();
            ( ( System.ComponentModel.ISupportInitialize ) ( this.neuSpread1_Sheet1 ) ).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.neuLabel4);
            this.panel1.Controls.Add(this.lbPatientNO);
            this.panel1.Controls.Add(this.lbDate);
            this.panel1.Controls.Add(this.lbTime);
            this.panel1.Controls.Add(this.lbName);
            this.panel1.Controls.Add(this.neuLabel2);
            this.panel1.Controls.Add(this.neuLabel1);
            this.panel1.Controls.Add(this.lbBed);
            this.panel1.Controls.Add(this.lbDept);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(295, 123);
            this.panel1.TabIndex = 0;
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 134 ) ));
            this.neuLabel4.Location = new System.Drawing.Point(116, 26);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(47, 19);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 4;
            this.neuLabel4.Text = "类别";
            // 
            // lbPatientNO
            // 
            this.lbPatientNO.AutoSize = true;
            this.lbPatientNO.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 134 ) ));
            this.lbPatientNO.ForeColor = System.Drawing.Color.Blue;
            this.lbPatientNO.Location = new System.Drawing.Point(31, 102);
            this.lbPatientNO.Name = "lbPatientNO";
            this.lbPatientNO.Size = new System.Drawing.Size(49, 14);
            this.lbPatientNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbPatientNO.TabIndex = 3;
            this.lbPatientNO.Text = "住院号";
            // 
            // lbDate
            // 
            this.lbDate.AutoSize = true;
            this.lbDate.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 134 ) ));
            this.lbDate.ForeColor = System.Drawing.Color.Blue;
            this.lbDate.Location = new System.Drawing.Point(155, 102);
            this.lbDate.Name = "lbDate";
            this.lbDate.Size = new System.Drawing.Size(63, 14);
            this.lbDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbDate.TabIndex = 3;
            this.lbDate.Text = "服药日期";
            // 
            // lbTime
            // 
            this.lbTime.AutoSize = true;
            this.lbTime.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 134 ) ));
            this.lbTime.ForeColor = System.Drawing.Color.Blue;
            this.lbTime.Location = new System.Drawing.Point(155, 81);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(63, 14);
            this.lbTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbTime.TabIndex = 3;
            this.lbTime.Text = "服药时间";
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 134 ) ));
            this.lbName.ForeColor = System.Drawing.Color.Blue;
            this.lbName.Location = new System.Drawing.Point(31, 81);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(49, 14);
            this.lbName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbName.TabIndex = 3;
            this.lbName.Text = "姓  名";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 134 ) ));
            this.neuLabel2.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel2.Location = new System.Drawing.Point(62, 59);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(203, 14);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 2;
            this.neuLabel2.Text = "广州肿瘤医院住院病人口服药袋";
            this.neuLabel2.Visible = false;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 134 ) ));
            this.neuLabel1.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel1.Location = new System.Drawing.Point(241, 32);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(28, 19);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 1;
            this.neuLabel1.Text = "床";
            this.neuLabel1.Visible = false;
            // 
            // lbBed
            // 
            this.lbBed.AutoSize = true;
            this.lbBed.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 134 ) ));
            this.lbBed.Location = new System.Drawing.Point(195, 29);
            this.lbBed.Name = "lbBed";
            this.lbBed.Size = new System.Drawing.Size(29, 19);
            this.lbBed.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbBed.TabIndex = 1;
            this.lbBed.Text = "20";
            // 
            // lbDept
            // 
            this.lbDept.AutoSize = true;
            this.lbDept.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 134 ) ));
            this.lbDept.Location = new System.Drawing.Point(4, 27);
            this.lbDept.Name = "lbDept";
            this.lbDept.Size = new System.Drawing.Size(85, 19);
            this.lbDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbDept.TabIndex = 0;
            this.lbDept.Text = "患者科室";
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.lbPrintDate);
            this.neuPanel1.Controls.Add(this.neuLabel3);
            this.neuPanel1.Controls.Add(this.lbPage);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuPanel1.Location = new System.Drawing.Point(0, 380);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(295, 42);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 1;
            // 
            // lbPrintDate
            // 
            this.lbPrintDate.AutoSize = true;
            this.lbPrintDate.Location = new System.Drawing.Point(6, 6);
            this.lbPrintDate.Name = "lbPrintDate";
            this.lbPrintDate.Size = new System.Drawing.Size(53, 12);
            this.lbPrintDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbPrintDate.TabIndex = 0;
            this.lbPrintDate.Text = "打印时间";
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 134 ) ));
            this.neuLabel3.ForeColor = System.Drawing.Color.Black;
            this.neuLabel3.Location = new System.Drawing.Point(240, 22);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(49, 14);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 4;
            this.neuLabel3.Text = "条码号";
            this.neuLabel3.Visible = false;
            // 
            // lbPage
            // 
            this.lbPage.AutoSize = true;
            this.lbPage.Location = new System.Drawing.Point(156, 6);
            this.lbPage.Name = "lbPage";
            this.lbPage.Size = new System.Drawing.Size(0, 12);
            this.lbPage.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbPage.TabIndex = 0;
            // 
            // neuPictureBox1
            // 
            this.neuPictureBox1.Location = new System.Drawing.Point(3, 333);
            this.neuPictureBox1.Name = "neuPictureBox1";
            this.neuPictureBox1.Size = new System.Drawing.Size(153, 43);
            this.neuPictureBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPictureBox1.TabIndex = 1;
            this.neuPictureBox1.TabStop = false;
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "2.5.2007.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, 氨溴素片(平坦之片)[30mg/片/20* 1盒";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 134 ) ));
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(0, 123);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(295, 257);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 2;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 134 ) ));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ColumnCount = 3;
            this.neuSpread1_Sheet1.RowCount = 6;
            this.neuSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Transparent, FarPoint.Win.Spread.GridLines.None, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, false, false);
            this.neuSpread1_Sheet1.Cells.Get(0, 0).Value = "氨溴素片(平坦之片)[30mg/片/20* 1盒";
            this.neuSpread1_Sheet1.Cells.Get(0, 1).ParseFormatInfo = ( ( System.Globalization.NumberFormatInfo ) ( cultureInfo.NumberFormat.Clone() ) );
            ( ( System.Globalization.NumberFormatInfo ) ( this.neuSpread1_Sheet1.Cells.Get(0, 1).ParseFormatInfo ) ).CurrencyDecimalDigits = 0;
            ( ( System.Globalization.NumberFormatInfo ) ( this.neuSpread1_Sheet1.Cells.Get(0, 1).ParseFormatInfo ) ).CurrencyNegativePattern = 1;
            ( ( System.Globalization.NumberFormatInfo ) ( this.neuSpread1_Sheet1.Cells.Get(0, 1).ParseFormatInfo ) ).NumberDecimalDigits = 0;
            ( ( System.Globalization.NumberFormatInfo ) ( this.neuSpread1_Sheet1.Cells.Get(0, 1).ParseFormatInfo ) ).NumberGroupSizes = new int[] {
        0};
            this.neuSpread1_Sheet1.Cells.Get(0, 1).ParseFormatString = "n";
            this.neuSpread1_Sheet1.Cells.Get(0, 1).Value = "100粒 100mg";
            this.neuSpread1_Sheet1.Cells.Get(0, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells.Get(0, 2).Value = "PB1-21";
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            textCellType1.Multiline = true;
            textCellType1.WordWrap = true;
            this.neuSpread1_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 151F;
            textCellType2.Multiline = true;
            textCellType2.WordWrap = true;
            this.neuSpread1_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 55F;
            this.neuSpread1_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.neuSpread1_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 81F;
            this.neuSpread1_Sheet1.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.neuSpread1_Sheet1.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.RowHeader.Visible = false;
            this.neuSpread1_Sheet1.Rows.Default.Height = 35F;
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.SheetCornerStyle.Locked = false;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 134 ) ));
            this.neuLabel5.ForeColor = System.Drawing.Color.Black;
            this.neuLabel5.Location = new System.Drawing.Point(162, 333);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(42, 14);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 5;
            this.neuLabel5.Text = "频次:";
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 134 ) ));
            this.neuLabel6.ForeColor = System.Drawing.Color.Black;
            this.neuLabel6.Location = new System.Drawing.Point(162, 352);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(56, 14);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 6;
            this.neuLabel6.Text = "每次量:";
            // 
            // ucDrugBag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.neuLabel6);
            this.Controls.Add(this.neuLabel5);
            this.Controls.Add(this.neuPictureBox1);
            this.Controls.Add(this.neuSpread1);
            this.Controls.Add(this.neuPanel1);
            this.Controls.Add(this.panel1);
            this.Name = "ucDrugBag";
            this.Size = new System.Drawing.Size(295, 422);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            ( ( System.ComponentModel.ISupportInitialize ) ( this.neuPictureBox1 ) ).EndInit();
            ( ( System.ComponentModel.ISupportInitialize ) ( this.neuSpread1 ) ).EndInit();
            ( ( System.ComponentModel.ISupportInitialize ) ( this.neuSpread1_Sheet1 ) ).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbPatientNO;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbDate;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbTime;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbName;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbBed;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbDept;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbPrintDate;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbPage;
        private FS.FrameWork.WinForms.Controls.NeuPictureBox neuPictureBox1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
    }
}
