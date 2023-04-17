namespace FS.SOC.HISFC.InpatientFee.Components.Balance
{
    partial class frmSplitFeeInfo
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
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType2 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType4 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType5 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType6 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.gbPatientInfo = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.ckCombine = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnConfrim = new System.Windows.Forms.Button();
            this.ckMinFee = new System.Windows.Forms.CheckBox();
            this.txtName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel8 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtPatientNo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel10 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.gbBedGrade = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.fpCost = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpCost_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpSplitCost_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.ckNoSplit = new System.Windows.Forms.CheckBox();
            this.gbPatientInfo.SuspendLayout();
            this.gbBedGrade.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpCost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpCost_Sheet1)).BeginInit();
            this.neuGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSplitCost_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // gbPatientInfo
            // 
            this.gbPatientInfo.Controls.Add(this.ckNoSplit);
            this.gbPatientInfo.Controls.Add(this.ckCombine);
            this.gbPatientInfo.Controls.Add(this.label1);
            this.gbPatientInfo.Controls.Add(this.btnCancel);
            this.gbPatientInfo.Controls.Add(this.btnConfrim);
            this.gbPatientInfo.Controls.Add(this.ckMinFee);
            this.gbPatientInfo.Controls.Add(this.txtName);
            this.gbPatientInfo.Controls.Add(this.neuLabel8);
            this.gbPatientInfo.Controls.Add(this.txtPatientNo);
            this.gbPatientInfo.Controls.Add(this.neuLabel10);
            this.gbPatientInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbPatientInfo.Location = new System.Drawing.Point(0, 0);
            this.gbPatientInfo.Name = "gbPatientInfo";
            this.gbPatientInfo.Size = new System.Drawing.Size(829, 86);
            this.gbPatientInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbPatientInfo.TabIndex = 17;
            this.gbPatientInfo.TabStop = false;
            this.gbPatientInfo.Text = "患者信息";
            // 
            // ckCombine
            // 
            this.ckCombine.AutoSize = true;
            this.ckCombine.Location = new System.Drawing.Point(164, 57);
            this.ckCombine.Name = "ckCombine";
            this.ckCombine.Size = new System.Drawing.Size(48, 16);
            this.ckCombine.TabIndex = 18;
            this.ckCombine.Text = "合并";
            this.ckCombine.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(547, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 12);
            this.label1.TabIndex = 17;
            this.label1.Text = "注意事项：费用拆分后不可还原！";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(731, 53);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnConfrim
            // 
            this.btnConfrim.Location = new System.Drawing.Point(640, 53);
            this.btnConfrim.Name = "btnConfrim";
            this.btnConfrim.Size = new System.Drawing.Size(75, 23);
            this.btnConfrim.TabIndex = 15;
            this.btnConfrim.Text = "确认(&O)";
            this.btnConfrim.UseVisualStyleBackColor = true;
            // 
            // ckMinFee
            // 
            this.ckMinFee.AutoSize = true;
            this.ckMinFee.Location = new System.Drawing.Point(98, 57);
            this.ckMinFee.Name = "ckMinFee";
            this.ckMinFee.Size = new System.Drawing.Size(48, 16);
            this.ckMinFee.TabIndex = 12;
            this.ckMinFee.Text = "拆分";
            this.ckMinFee.UseVisualStyleBackColor = true;
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.SystemColors.Window;
            this.txtName.CausesValidation = false;
            this.txtName.IsEnter2Tab = false;
            this.txtName.Location = new System.Drawing.Point(335, 23);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(147, 21);
            this.txtName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtName.TabIndex = 11;
            // 
            // neuLabel8
            // 
            this.neuLabel8.AutoSize = true;
            this.neuLabel8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel8.ForeColor = System.Drawing.Color.Black;
            this.neuLabel8.Location = new System.Drawing.Point(257, 26);
            this.neuLabel8.Name = "neuLabel8";
            this.neuLabel8.Size = new System.Drawing.Size(72, 12);
            this.neuLabel8.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel8.TabIndex = 10;
            this.neuLabel8.Text = "姓    名：";
            // 
            // txtPatientNo
            // 
            this.txtPatientNo.BackColor = System.Drawing.SystemColors.Window;
            this.txtPatientNo.CausesValidation = false;
            this.txtPatientNo.IsEnter2Tab = false;
            this.txtPatientNo.Location = new System.Drawing.Point(98, 23);
            this.txtPatientNo.Name = "txtPatientNo";
            this.txtPatientNo.ReadOnly = true;
            this.txtPatientNo.Size = new System.Drawing.Size(147, 21);
            this.txtPatientNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtPatientNo.TabIndex = 7;
            // 
            // neuLabel10
            // 
            this.neuLabel10.AutoSize = true;
            this.neuLabel10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabel10.ForeColor = System.Drawing.Color.Black;
            this.neuLabel10.Location = new System.Drawing.Point(20, 26);
            this.neuLabel10.Name = "neuLabel10";
            this.neuLabel10.Size = new System.Drawing.Size(71, 12);
            this.neuLabel10.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel10.TabIndex = 6;
            this.neuLabel10.Text = "住 院 号：";
            // 
            // gbBedGrade
            // 
            this.gbBedGrade.Controls.Add(this.fpCost);
            this.gbBedGrade.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbBedGrade.Location = new System.Drawing.Point(0, 86);
            this.gbBedGrade.Name = "gbBedGrade";
            this.gbBedGrade.Size = new System.Drawing.Size(422, 350);
            this.gbBedGrade.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbBedGrade.TabIndex = 20;
            this.gbBedGrade.TabStop = false;
            this.gbBedGrade.Text = "操作前费用";
            // 
            // fpCost
            // 
            this.fpCost.About = "3.0.2004.2005";
            this.fpCost.AccessibleDescription = "fpCost, Sheet1";
            this.fpCost.BackColor = System.Drawing.Color.White;
            this.fpCost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpCost.FileName = "";
            this.fpCost.Font = new System.Drawing.Font("宋体", 9F);
            this.fpCost.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpCost.IsAutoSaveGridStatus = false;
            this.fpCost.IsCanCustomConfigColumn = false;
            this.fpCost.Location = new System.Drawing.Point(3, 17);
            this.fpCost.Name = "fpCost";
            this.fpCost.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpCost.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpCost_Sheet1});
            this.fpCost.Size = new System.Drawing.Size(416, 330);
            this.fpCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpCost.TabIndex = 100;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpCost.TextTipAppearance = tipAppearance1;
            this.fpCost.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpCost_Sheet1
            // 
            this.fpCost_Sheet1.Reset();
            this.fpCost_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpCost_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpCost_Sheet1.ColumnCount = 7;
            this.fpCost_Sheet1.RowCount = 0;
            this.fpCost_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpCost_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "选择";
            this.fpCost_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "费用科目";
            this.fpCost_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "原始金额";
            this.fpCost_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "未结金额";
            this.fpCost_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "结算金额";
            this.fpCost_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "记账";
            this.fpCost_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "自费";
            this.fpCost_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpCost_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpCost_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.fpCost_Sheet1.Columns.Get(0).CellType = checkBoxCellType1;
            this.fpCost_Sheet1.Columns.Get(0).Label = "选择";
            this.fpCost_Sheet1.Columns.Get(0).Width = 30F;
            this.fpCost_Sheet1.Columns.Get(1).CellType = textCellType1;
            this.fpCost_Sheet1.Columns.Get(1).Label = "费用科目";
            this.fpCost_Sheet1.Columns.Get(1).Locked = true;
            this.fpCost_Sheet1.Columns.Get(1).Width = 106F;
            this.fpCost_Sheet1.Columns.Get(2).CellType = numberCellType1;
            this.fpCost_Sheet1.Columns.Get(2).Label = "原始金额";
            this.fpCost_Sheet1.Columns.Get(3).CellType = numberCellType2;
            this.fpCost_Sheet1.Columns.Get(3).Label = "未结金额";
            this.fpCost_Sheet1.Columns.Get(3).Locked = true;
            this.fpCost_Sheet1.Columns.Get(3).Width = 65F;
            this.fpCost_Sheet1.Columns.Get(4).CellType = numberCellType3;
            this.fpCost_Sheet1.Columns.Get(4).Label = "结算金额";
            this.fpCost_Sheet1.Columns.Get(4).Width = 0F;
            this.fpCost_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpCost_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpCost_Sheet1.RowHeader.Columns.Get(0).Width = 25F;
            this.fpCost_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpCost_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpCost_Sheet1.Rows.Default.Height = 25F;
            this.fpCost_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpCost_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.fpCost_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpCost_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpCost.SetActiveViewport(0, 1, 0);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(422, 86);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 350);
            this.splitter1.TabIndex = 21;
            this.splitter1.TabStop = false;
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.neuSpread1);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuGroupBox1.Location = new System.Drawing.Point(425, 86);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(404, 350);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 22;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "操作后费用";
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.Font = new System.Drawing.Font("宋体", 9F);
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(3, 17);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSplitCost_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(398, 330);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 100;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance2;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpSplitCost_Sheet1
            // 
            this.fpSplitCost_Sheet1.Reset();
            this.fpSplitCost_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSplitCost_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSplitCost_Sheet1.ColumnCount = 7;
            this.fpSplitCost_Sheet1.RowCount = 0;
            this.fpSplitCost_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpSplitCost_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "选择";
            this.fpSplitCost_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "费用科目";
            this.fpSplitCost_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "原始金额";
            this.fpSplitCost_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "未结金额";
            this.fpSplitCost_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "结算金额";
            this.fpSplitCost_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "记账";
            this.fpSplitCost_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "自费";
            this.fpSplitCost_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSplitCost_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSplitCost_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.fpSplitCost_Sheet1.Columns.Get(0).CellType = checkBoxCellType2;
            this.fpSplitCost_Sheet1.Columns.Get(0).Label = "选择";
            this.fpSplitCost_Sheet1.Columns.Get(0).Width = 30F;
            this.fpSplitCost_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.fpSplitCost_Sheet1.Columns.Get(1).Label = "费用科目";
            this.fpSplitCost_Sheet1.Columns.Get(1).Locked = true;
            this.fpSplitCost_Sheet1.Columns.Get(1).Width = 150F;
            this.fpSplitCost_Sheet1.Columns.Get(2).CellType = numberCellType4;
            this.fpSplitCost_Sheet1.Columns.Get(2).Label = "原始金额";
            this.fpSplitCost_Sheet1.Columns.Get(2).Width = 0F;
            this.fpSplitCost_Sheet1.Columns.Get(3).CellType = numberCellType5;
            this.fpSplitCost_Sheet1.Columns.Get(3).Label = "未结金额";
            this.fpSplitCost_Sheet1.Columns.Get(3).Locked = true;
            this.fpSplitCost_Sheet1.Columns.Get(3).Width = 65F;
            this.fpSplitCost_Sheet1.Columns.Get(4).CellType = numberCellType6;
            this.fpSplitCost_Sheet1.Columns.Get(4).Label = "结算金额";
            this.fpSplitCost_Sheet1.Columns.Get(4).Width = 0F;
            this.fpSplitCost_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpSplitCost_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpSplitCost_Sheet1.RowHeader.Columns.Get(0).Width = 25F;
            this.fpSplitCost_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSplitCost_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSplitCost_Sheet1.Rows.Default.Height = 25F;
            this.fpSplitCost_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSplitCost_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.fpSplitCost_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpSplitCost_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuSpread1.SetActiveViewport(0, 1, 0);
            // 
            // ckNoSplit
            // 
            this.ckNoSplit.AutoSize = true;
            this.ckNoSplit.Location = new System.Drawing.Point(22, 57);
            this.ckNoSplit.Name = "ckNoSplit";
            this.ckNoSplit.Size = new System.Drawing.Size(60, 16);
            this.ckNoSplit.TabIndex = 19;
            this.ckNoSplit.Text = "不处理";
            this.ckNoSplit.UseVisualStyleBackColor = true;
            // 
            // frmSplitFeeInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 436);
            this.Controls.Add(this.neuGroupBox1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.gbBedGrade);
            this.Controls.Add(this.gbPatientInfo);
            this.KeyPreview = true;
            this.Name = "frmSplitFeeInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "拆分费用";
            this.gbPatientInfo.ResumeLayout(false);
            this.gbPatientInfo.PerformLayout();
            this.gbBedGrade.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpCost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpCost_Sheet1)).EndInit();
            this.neuGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSplitCost_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbPatientInfo;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtName;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel8;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtPatientNo;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel10;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbBedGrade;
        private System.Windows.Forms.Splitter splitter1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnConfrim;
        private System.Windows.Forms.CheckBox ckMinFee;
        protected FS.FrameWork.WinForms.Controls.NeuSpread fpCost;
        protected FarPoint.Win.Spread.SheetView fpCost_Sheet1;
        protected FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        protected FarPoint.Win.Spread.SheetView fpSplitCost_Sheet1;
        private System.Windows.Forms.CheckBox ckCombine;
        private System.Windows.Forms.CheckBox ckNoSplit;
    }
}