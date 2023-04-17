namespace FS.SOC.Local.OutpatientFee.FuYou.Report
{
    partial class ucOutpatientFeeInfo
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
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuClinicCode = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.panelMain = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.panelTop = new System.Windows.Forms.Panel();
            this.neulblDept = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neulblClinicCode = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neulblRegDate = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neulblAge = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neulblSex = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neulblName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuGroupBox1.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.neuLabel1);
            this.neuGroupBox1.Controls.Add(this.neuClinicCode);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(750, 51);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 2;
            this.neuGroupBox1.TabStop = false;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(6, 21);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(53, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 1;
            this.neuLabel1.Text = "发票号：";
            // 
            // neuClinicCode
            // 
            this.neuClinicCode.IsEnter2Tab = false;
            this.neuClinicCode.Location = new System.Drawing.Point(65, 17);
            this.neuClinicCode.Name = "neuClinicCode";
            this.neuClinicCode.Size = new System.Drawing.Size(150, 21);
            this.neuClinicCode.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuClinicCode.TabIndex = 0;
            this.neuClinicCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.neuClinicCode_KeyPress);
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.White;
            this.panelMain.Controls.Add(this.neuSpread1);
            this.panelMain.Controls.Add(this.panelTop);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 51);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(750, 577);
            this.panelMain.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panelMain.TabIndex = 4;
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, 项目编号";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(0, 90);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(750, 487);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 6;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance2;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ColumnCount = 9;
            this.neuSpread1_Sheet1.ColumnHeader.RowCount = 0;
            this.neuSpread1_Sheet1.RowCount = 2;
            this.neuSpread1_Sheet1.RowHeader.ColumnCount = 0;
            this.neuSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.SystemColors.Control, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.neuSpread1_Sheet1.Cells.Get(0, 0).Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Cells.Get(0, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(0, 0).Locked = true;
            this.neuSpread1_Sheet1.Cells.Get(0, 0).Value = "项目编号";
            this.neuSpread1_Sheet1.Cells.Get(0, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            textCellType3.WordWrap = true;
            this.neuSpread1_Sheet1.Cells.Get(0, 1).CellType = textCellType3;
            this.neuSpread1_Sheet1.Cells.Get(0, 1).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Cells.Get(0, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(0, 1).Locked = true;
            this.neuSpread1_Sheet1.Cells.Get(0, 1).Value = "医疗服务价格项目、药品(通用名)或一次性医用耗材名称";
            this.neuSpread1_Sheet1.Cells.Get(0, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(0, 2).Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Cells.Get(0, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(0, 2).Locked = true;
            this.neuSpread1_Sheet1.Cells.Get(0, 2).Value = "规格";
            this.neuSpread1_Sheet1.Cells.Get(0, 2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(0, 3).Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Cells.Get(0, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(0, 3).Locked = true;
            this.neuSpread1_Sheet1.Cells.Get(0, 3).Value = "单价";
            this.neuSpread1_Sheet1.Cells.Get(0, 3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(0, 4).Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Cells.Get(0, 4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(0, 4).Locked = true;
            this.neuSpread1_Sheet1.Cells.Get(0, 4).Value = "数量";
            this.neuSpread1_Sheet1.Cells.Get(0, 4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(0, 5).Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Cells.Get(0, 5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(0, 5).Locked = true;
            this.neuSpread1_Sheet1.Cells.Get(0, 5).Value = "单位";
            this.neuSpread1_Sheet1.Cells.Get(0, 5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(0, 6).Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Cells.Get(0, 6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(0, 6).Locked = true;
            this.neuSpread1_Sheet1.Cells.Get(0, 6).Value = "金额";
            this.neuSpread1_Sheet1.Cells.Get(0, 6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(0, 7).Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Cells.Get(0, 7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(0, 7).Locked = true;
            this.neuSpread1_Sheet1.Cells.Get(0, 7).Value = "自费额";
            this.neuSpread1_Sheet1.Cells.Get(0, 7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(0, 8).Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Cells.Get(0, 8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(0, 8).Locked = true;
            this.neuSpread1_Sheet1.Cells.Get(0, 8).Value = "备注";
            this.neuSpread1_Sheet1.Cells.Get(0, 8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(1, 0).ColumnSpan = 2;
            this.neuSpread1_Sheet1.Cells.Get(1, 0).Locked = true;
            this.neuSpread1_Sheet1.Cells.Get(1, 0).Value = "说明：结算方式为 ";
            this.neuSpread1_Sheet1.Cells.Get(1, 1).Locked = true;
            this.neuSpread1_Sheet1.Cells.Get(1, 2).Locked = true;
            this.neuSpread1_Sheet1.Cells.Get(1, 2).Value = "总金额合计： ";
            this.neuSpread1_Sheet1.Cells.Get(1, 3).Locked = true;
            this.neuSpread1_Sheet1.Cells.Get(1, 4).ColumnSpan = 2;
            this.neuSpread1_Sheet1.Cells.Get(1, 4).Locked = true;
            this.neuSpread1_Sheet1.Cells.Get(1, 4).Value = "自费总金额：";
            this.neuSpread1_Sheet1.Cells.Get(1, 5).Locked = true;
            this.neuSpread1_Sheet1.Cells.Get(1, 6).Locked = true;
            this.neuSpread1_Sheet1.Cells.Get(1, 7).Locked = true;
            this.neuSpread1_Sheet1.Cells.Get(1, 7).Value = "优惠金额：";
            this.neuSpread1_Sheet1.Cells.Get(1, 8).Locked = true;
            this.neuSpread1_Sheet1.ColumnHeader.AutoText = FarPoint.Win.Spread.HeaderAutoText.Numbers;
            this.neuSpread1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 91F;
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 195F;
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 90F;
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 61F;
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 39F;
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 40F;
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 58F;
            this.neuSpread1_Sheet1.Columns.Get(7).Width = 65F;
            this.neuSpread1_Sheet1.Columns.Get(8).Width = 77F;
            this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.Rows.Get(0).Height = 48F;
            this.neuSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.neulblDept);
            this.panelTop.Controls.Add(this.neulblClinicCode);
            this.panelTop.Controls.Add(this.neulblRegDate);
            this.panelTop.Controls.Add(this.neulblAge);
            this.panelTop.Controls.Add(this.neulblSex);
            this.panelTop.Controls.Add(this.neulblName);
            this.panelTop.Controls.Add(this.neuLabel3);
            this.panelTop.Controls.Add(this.neuLabel2);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(750, 90);
            this.panelTop.TabIndex = 5;
            // 
            // neulblDept
            // 
            this.neulblDept.AutoSize = true;
            this.neulblDept.Location = new System.Drawing.Point(600, 69);
            this.neulblDept.Name = "neulblDept";
            this.neulblDept.Size = new System.Drawing.Size(41, 12);
            this.neulblDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neulblDept.TabIndex = 16;
            this.neulblDept.Text = "科室：";
            // 
            // neulblClinicCode
            // 
            this.neulblClinicCode.AutoSize = true;
            this.neulblClinicCode.Location = new System.Drawing.Point(471, 69);
            this.neulblClinicCode.Name = "neulblClinicCode";
            this.neulblClinicCode.Size = new System.Drawing.Size(53, 12);
            this.neulblClinicCode.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neulblClinicCode.TabIndex = 15;
            this.neulblClinicCode.Text = "病历号：";
            // 
            // neulblRegDate
            // 
            this.neulblRegDate.AutoSize = true;
            this.neulblRegDate.Location = new System.Drawing.Point(298, 69);
            this.neulblRegDate.Name = "neulblRegDate";
            this.neulblRegDate.Size = new System.Drawing.Size(41, 12);
            this.neulblRegDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neulblRegDate.TabIndex = 14;
            this.neulblRegDate.Text = "日期：";
            // 
            // neulblAge
            // 
            this.neulblAge.AutoSize = true;
            this.neulblAge.Location = new System.Drawing.Point(182, 69);
            this.neulblAge.Name = "neulblAge";
            this.neulblAge.Size = new System.Drawing.Size(41, 12);
            this.neulblAge.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neulblAge.TabIndex = 13;
            this.neulblAge.Text = "年龄：";
            // 
            // neulblSex
            // 
            this.neulblSex.AutoSize = true;
            this.neulblSex.Location = new System.Drawing.Point(108, 69);
            this.neulblSex.Name = "neulblSex";
            this.neulblSex.Size = new System.Drawing.Size(41, 12);
            this.neulblSex.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neulblSex.TabIndex = 12;
            this.neulblSex.Text = "性别：";
            // 
            // neulblName
            // 
            this.neulblName.AutoSize = true;
            this.neulblName.Location = new System.Drawing.Point(17, 69);
            this.neulblName.Name = "neulblName";
            this.neulblName.Size = new System.Drawing.Size(41, 12);
            this.neulblName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neulblName.TabIndex = 11;
            this.neulblName.Text = "姓名：";
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel3.Location = new System.Drawing.Point(268, 44);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(209, 19);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 10;
            this.neuLabel3.Text = "门诊病人费用明细清单";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel2.Location = new System.Drawing.Point(228, 16);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(289, 19);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 9;
            this.neuLabel2.Text = "广东佛山市顺德区妇幼保健医院";
            // 
            // ucOutpatientFeeInfo
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.neuGroupBox1);
            this.Name = "ucOutpatientFeeInfo";
            this.Size = new System.Drawing.Size(750, 628);
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox neuClinicCode;
        private FS.FrameWork.WinForms.Controls.NeuPanel panelMain;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private System.Windows.Forms.Panel panelTop;
        private FS.FrameWork.WinForms.Controls.NeuLabel neulblDept;
        private FS.FrameWork.WinForms.Controls.NeuLabel neulblClinicCode;
        private FS.FrameWork.WinForms.Controls.NeuLabel neulblRegDate;
        private FS.FrameWork.WinForms.Controls.NeuLabel neulblAge;
        private FS.FrameWork.WinForms.Controls.NeuLabel neulblSex;
        private FS.FrameWork.WinForms.Controls.NeuLabel neulblName;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;

    }
}