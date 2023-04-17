namespace FS.SOC.Local.OutpatientFee.GuangZhou.Zdly.IOutpatientItemInputAndDisplay
{
    partial class ucModifyRate
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
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType5 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType6 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType7 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType8 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.fpSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.btnConfirm = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnCancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuLabel12 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbRebateInfo = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.fpSpread1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(729, 309);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "打折设置";
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "3.0.2004.2005";
            this.fpSpread1.AccessibleDescription = "fpSpread1, Sheet1, Row 0, Column 0, ";
            this.fpSpread1.BackColor = System.Drawing.Color.White;
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread1.EditModeReplace = true;
            this.fpSpread1.FileName = "";
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.IsAutoSaveGridStatus = false;
            this.fpSpread1.IsCanCustomConfigColumn = false;
            this.fpSpread1.Location = new System.Drawing.Point(3, 17);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1});
            this.fpSpread1.Size = new System.Drawing.Size(723, 289);
            this.fpSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpSpread1.TabIndex = 1;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread1.TextTipAppearance = tipAppearance2;
            this.fpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.ColumnCount = 7;
            this.fpSpread1_Sheet1.RowCount = 8;
            this.fpSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "名称";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "数量";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "单价";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "金额";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "打折比例%";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "实际优惠比例";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "打折金额";
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.fpSpread1_Sheet1.Columns.Get(0).CellType = textCellType4;
            this.fpSpread1_Sheet1.Columns.Get(0).Font = new System.Drawing.Font("宋体", 11.5F);
            this.fpSpread1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpSpread1_Sheet1.Columns.Get(0).Label = "名称";
            this.fpSpread1_Sheet1.Columns.Get(0).Locked = true;
            this.fpSpread1_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.Columns.Get(0).Width = 265F;
            this.fpSpread1_Sheet1.Columns.Get(1).CellType = textCellType5;
            this.fpSpread1_Sheet1.Columns.Get(1).Font = new System.Drawing.Font("宋体", 11.5F);
            this.fpSpread1_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.fpSpread1_Sheet1.Columns.Get(1).Label = "数量";
            this.fpSpread1_Sheet1.Columns.Get(1).Locked = true;
            this.fpSpread1_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.Columns.Get(1).Width = 64F;
            numberCellType5.DecimalPlaces = 4;
            this.fpSpread1_Sheet1.Columns.Get(2).CellType = numberCellType5;
            this.fpSpread1_Sheet1.Columns.Get(2).Font = new System.Drawing.Font("宋体", 11.5F);
            this.fpSpread1_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.fpSpread1_Sheet1.Columns.Get(2).Label = "单价";
            this.fpSpread1_Sheet1.Columns.Get(2).Locked = true;
            this.fpSpread1_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.Columns.Get(2).Width = 96F;
            this.fpSpread1_Sheet1.Columns.Get(3).CellType = textCellType6;
            this.fpSpread1_Sheet1.Columns.Get(3).Font = new System.Drawing.Font("宋体", 11.5F);
            this.fpSpread1_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.fpSpread1_Sheet1.Columns.Get(3).Label = "金额";
            this.fpSpread1_Sheet1.Columns.Get(3).Locked = true;
            this.fpSpread1_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.Columns.Get(3).Width = 85F;
            numberCellType6.DecimalPlaces = 0;
            numberCellType6.MaximumValue = 100;
            numberCellType6.MinimumValue = 0;
            this.fpSpread1_Sheet1.Columns.Get(4).CellType = numberCellType6;
            this.fpSpread1_Sheet1.Columns.Get(4).Font = new System.Drawing.Font("宋体", 11.5F);
            this.fpSpread1_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.fpSpread1_Sheet1.Columns.Get(4).Label = "打折比例%";
            this.fpSpread1_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.Columns.Get(4).Width = 70F;
            this.fpSpread1_Sheet1.Columns.Get(5).CellType = numberCellType7;
            this.fpSpread1_Sheet1.Columns.Get(5).Font = new System.Drawing.Font("宋体", 11.5F);
            this.fpSpread1_Sheet1.Columns.Get(5).Label = "实际优惠比例";
            this.fpSpread1_Sheet1.Columns.Get(5).Locked = true;
            this.fpSpread1_Sheet1.Columns.Get(5).Visible = false;
            this.fpSpread1_Sheet1.Columns.Get(5).Width = 27F;
            numberCellType8.DecimalPlaces = 2;
            numberCellType8.MinimumValue = 0;
            this.fpSpread1_Sheet1.Columns.Get(6).CellType = numberCellType8;
            this.fpSpread1_Sheet1.Columns.Get(6).Font = new System.Drawing.Font("宋体", 11.5F);
            this.fpSpread1_Sheet1.Columns.Get(6).Label = "打折金额";
            this.fpSpread1_Sheet1.Columns.Get(6).Locked = true;
            this.fpSpread1_Sheet1.Columns.Get(6).Width = 94F;
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 41F;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.Rows.Default.Height = 23F;
            this.fpSpread1_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.MultiRange;
            this.fpSpread1_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSpread1_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.fpSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Font = new System.Drawing.Font("宋体", 12F);
            this.btnConfirm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConfirm.Location = new System.Drawing.Point(476, 315);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(96, 27);
            this.btnConfirm.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnConfirm.TabIndex = 4;
            this.btnConfirm.Text = "确定(&O)";
            this.btnConfirm.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnConfirm.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("宋体", 12F);
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(593, 315);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(96, 27);
            this.btnCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // neuLabel12
            // 
            this.neuLabel12.AutoSize = true;
            this.neuLabel12.Font = new System.Drawing.Font("宋体", 11.25F);
            this.neuLabel12.Location = new System.Drawing.Point(7, 321);
            this.neuLabel12.Name = "neuLabel12";
            this.neuLabel12.Size = new System.Drawing.Size(75, 15);
            this.neuLabel12.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel12.TabIndex = 9;
            this.neuLabel12.Text = "打折说明:";
            // 
            // cmbRebateInfo
            // 
            this.cmbRebateInfo.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbRebateInfo.Font = new System.Drawing.Font("宋体", 11.25F);
            this.cmbRebateInfo.FormattingEnabled = true;
            this.cmbRebateInfo.IsEnter2Tab = false;
            this.cmbRebateInfo.IsFlat = false;
            this.cmbRebateInfo.IsLike = true;
            this.cmbRebateInfo.IsListOnly = false;
            this.cmbRebateInfo.IsPopForm = true;
            this.cmbRebateInfo.IsShowCustomerList = false;
            this.cmbRebateInfo.IsShowID = false;
            this.cmbRebateInfo.Location = new System.Drawing.Point(88, 318);
            this.cmbRebateInfo.Name = "cmbRebateInfo";
            this.cmbRebateInfo.PopForm = null;
            this.cmbRebateInfo.ShowCustomerList = false;
            this.cmbRebateInfo.ShowID = false;
            this.cmbRebateInfo.Size = new System.Drawing.Size(275, 23);
            this.cmbRebateInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbRebateInfo.TabIndex = 10;
            this.cmbRebateInfo.Tag = "";
            this.cmbRebateInfo.ToolBarUse = false;
            // 
            // ucModifyRate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbRebateInfo);
            this.Controls.Add(this.neuLabel12);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.groupBox1);
            this.Name = "ucModifyRate";
            this.Size = new System.Drawing.Size(729, 351);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        protected FS.FrameWork.WinForms.Controls.NeuSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
        protected FS.FrameWork.WinForms.Controls.NeuButton btnConfirm;
        protected FS.FrameWork.WinForms.Controls.NeuButton btnCancel;
        protected FS.FrameWork.WinForms.Controls.NeuLabel neuLabel12;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbRebateInfo;
    }
}
