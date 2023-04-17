namespace API.GZSI.UI
{
    partial class frmQueryBaseInfoLocal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbCheckAll = new System.Windows.Forms.CheckBox();
            this.btCancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btOK = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btQuery = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.dtEndTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dtBeginTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.panel2 = new System.Windows.Forms.Panel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btOutPut = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btOutPut);
            this.panel1.Controls.Add(this.cbCheckAll);
            this.panel1.Controls.Add(this.btCancel);
            this.panel1.Controls.Add(this.btOK);
            this.panel1.Controls.Add(this.btQuery);
            this.panel1.Controls.Add(this.dtEndTime);
            this.panel1.Controls.Add(this.neuLabel4);
            this.panel1.Controls.Add(this.neuLabel3);
            this.panel1.Controls.Add(this.dtBeginTime);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(947, 93);
            this.panel1.TabIndex = 0;
            // 
            // cbCheckAll
            // 
            this.cbCheckAll.AutoSize = true;
            this.cbCheckAll.Location = new System.Drawing.Point(440, 14);
            this.cbCheckAll.Name = "cbCheckAll";
            this.cbCheckAll.Size = new System.Drawing.Size(48, 16);
            this.cbCheckAll.TabIndex = 23;
            this.cbCheckAll.Text = "全选";
            this.cbCheckAll.UseVisualStyleBackColor = true;
            this.cbCheckAll.CheckedChanged += new System.EventHandler(this.cbCheckAll_CheckedChanged);
            // 
            // btCancel
            // 
            this.btCancel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btCancel.Location = new System.Drawing.Point(232, 47);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(87, 31);
            this.btCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btCancel.TabIndex = 22;
            this.btCancel.Text = "取消";
            this.btCancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btOK
            // 
            this.btOK.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btOK.Location = new System.Drawing.Point(125, 46);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(87, 31);
            this.btOK.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btOK.TabIndex = 21;
            this.btOK.Text = "确定";
            this.btOK.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btQuery
            // 
            this.btQuery.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btQuery.Location = new System.Drawing.Point(16, 46);
            this.btQuery.Name = "btQuery";
            this.btQuery.Size = new System.Drawing.Size(87, 31);
            this.btQuery.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btQuery.TabIndex = 20;
            this.btQuery.Text = "查询";
            this.btQuery.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btQuery.UseVisualStyleBackColor = true;
            this.btQuery.Click += new System.EventHandler(this.btQuery_Click);
            // 
            // dtEndTime
            // 
            this.dtEndTime.CustomFormat = "yyyy-MM-dd";
            this.dtEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEndTime.IsEnter2Tab = false;
            this.dtEndTime.Location = new System.Drawing.Point(316, 9);
            this.dtEndTime.Name = "dtEndTime";
            this.dtEndTime.Size = new System.Drawing.Size(101, 21);
            this.dtEndTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtEndTime.TabIndex = 19;
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel4.Location = new System.Drawing.Point(225, 10);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(79, 19);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 18;
            this.neuLabel4.Text = "结束时间：";
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel3.Location = new System.Drawing.Point(12, 9);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(79, 19);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 17;
            this.neuLabel3.Text = "开始时间：";
            // 
            // dtBeginTime
            // 
            this.dtBeginTime.CustomFormat = "yyyy-MM-dd";
            this.dtBeginTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtBeginTime.IsEnter2Tab = false;
            this.dtBeginTime.Location = new System.Drawing.Point(97, 9);
            this.dtBeginTime.Name = "dtBeginTime";
            this.dtBeginTime.Size = new System.Drawing.Size(106, 21);
            this.dtBeginTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtBeginTime.TabIndex = 16;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.neuSpread1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 93);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(947, 247);
            this.panel2.TabIndex = 1;
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
            this.neuSpread1.Size = new System.Drawing.Size(947, 247);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 2;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            this.neuSpread1_Sheet1.ColumnCount = 11;
            this.neuSpread1_Sheet1.RowCount = 5;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "选择";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "证件类型";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "证件号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "姓名";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "就诊日期";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "检测次数";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "单价";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "医疗费用总额";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "核酸检测患者类型";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "项目信息";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "患者类型";
            this.neuSpread1_Sheet1.ColumnHeader.Rows.Get(0).Height = 33F;
            this.neuSpread1_Sheet1.Columns.Get(0).CellType = checkBoxCellType1;
            this.neuSpread1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "选择";
            this.neuSpread1_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 41F;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "证件类型";
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 65F;
            this.neuSpread1_Sheet1.Columns.Get(2).CellType = textCellType1;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "证件号";
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 134F;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "姓名";
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 75F;
            this.neuSpread1_Sheet1.Columns.Get(4).CellType = textCellType2;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "就诊日期";
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 121F;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "检测次数";
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 82F;
            this.neuSpread1_Sheet1.Columns.Get(6).Label = "单价";
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 91F;
            this.neuSpread1_Sheet1.Columns.Get(7).Label = "医疗费用总额";
            this.neuSpread1_Sheet1.Columns.Get(7).Width = 91F;
            this.neuSpread1_Sheet1.Columns.Get(8).Label = "核酸检测患者类型";
            this.neuSpread1_Sheet1.Columns.Get(8).Width = 105F;
            this.neuSpread1_Sheet1.Columns.Get(9).Label = "项目信息";
            this.neuSpread1_Sheet1.Columns.Get(9).Width = 65F;
            this.neuSpread1_Sheet1.Columns.Get(10).Label = "患者类型";
            this.neuSpread1_Sheet1.Columns.Get(10).Width = 65F;
            textCellType3.WordWrap = true;
            this.neuSpread1_Sheet1.DefaultStyle.CellType = textCellType3;
            this.neuSpread1_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 340);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(947, 15);
            this.panel3.TabIndex = 2;
            // 
            // btOutPut
            // 
            this.btOutPut.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btOutPut.Location = new System.Drawing.Point(512, 8);
            this.btOutPut.Name = "btOutPut";
            this.btOutPut.Size = new System.Drawing.Size(87, 31);
            this.btOutPut.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btOutPut.TabIndex = 24;
            this.btOutPut.Text = "导出";
            this.btOutPut.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btOutPut.UseVisualStyleBackColor = true;
            this.btOutPut.Click += new System.EventHandler(this.btOutPut_Click);
            // 
            // frmQueryBaseInfoLocal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(947, 355);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "frmQueryBaseInfoLocal";
            this.Text = "本地记录导入";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuButton btQuery;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtEndTime;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtBeginTime;
        private FS.FrameWork.WinForms.Controls.NeuButton btCancel;
        private FS.FrameWork.WinForms.Controls.NeuButton btOK;
        private System.Windows.Forms.CheckBox cbCheckAll;
        private FS.FrameWork.WinForms.Controls.NeuButton btOutPut;
    }
}