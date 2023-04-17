namespace FS.HISFC.Components.Account.Controls.IBorn
{
    partial class ucPatientPayItems
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
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType2 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.plLeft = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.plLeftBottom = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.fpChildPackage = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpChildPackage_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.plLeftTop = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.fpPackage = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpPackage_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.plLeft.SuspendLayout();
            this.plLeftBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpChildPackage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpChildPackage_Sheet1)).BeginInit();
            this.plLeftTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpPackage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPackage_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // plLeft
            // 
            this.plLeft.Controls.Add(this.plLeftBottom);
            this.plLeft.Controls.Add(this.plLeftTop);
            this.plLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plLeft.Location = new System.Drawing.Point(0, 0);
            this.plLeft.Name = "plLeft";
            this.plLeft.Size = new System.Drawing.Size(976, 326);
            this.plLeft.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plLeft.TabIndex = 6;
            // 
            // plLeftBottom
            // 
            this.plLeftBottom.Controls.Add(this.fpChildPackage);
            this.plLeftBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plLeftBottom.Location = new System.Drawing.Point(0, 190);
            this.plLeftBottom.Name = "plLeftBottom";
            this.plLeftBottom.Size = new System.Drawing.Size(976, 136);
            this.plLeftBottom.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plLeftBottom.TabIndex = 7;
            // 
            // fpChildPackage
            // 
            this.fpChildPackage.About = "3.0.2004.2005";
            this.fpChildPackage.AccessibleDescription = "fpChildPackage, Sheet1, Row 0, Column 0, ";
            this.fpChildPackage.BackColor = System.Drawing.Color.White;
            this.fpChildPackage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpChildPackage.EditModePermanent = true;
            this.fpChildPackage.EditModeReplace = true;
            this.fpChildPackage.FileName = "";
            this.fpChildPackage.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpChildPackage.IsAutoSaveGridStatus = false;
            this.fpChildPackage.IsCanCustomConfigColumn = false;
            this.fpChildPackage.Location = new System.Drawing.Point(0, 0);
            this.fpChildPackage.Name = "fpChildPackage";
            this.fpChildPackage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpChildPackage.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpChildPackage_Sheet1});
            this.fpChildPackage.Size = new System.Drawing.Size(976, 136);
            this.fpChildPackage.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpChildPackage.TabIndex = 9;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpChildPackage.TextTipAppearance = tipAppearance1;
            this.fpChildPackage.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpChildPackage_Sheet1
            // 
            this.fpChildPackage_Sheet1.Reset();
            this.fpChildPackage_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpChildPackage_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpChildPackage_Sheet1.ColumnCount = 7;
            this.fpChildPackage_Sheet1.RowCount = 1;
            this.fpChildPackage_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = " ";
            this.fpChildPackage_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "编码";
            this.fpChildPackage_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "项目名称";
            this.fpChildPackage_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "次数";
            this.fpChildPackage_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "子项目编码";
            this.fpChildPackage_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "子项目名称";
            this.fpChildPackage_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "金额";
            this.fpChildPackage_Sheet1.ColumnHeader.Columns.Default.VisualStyles = FarPoint.Win.VisualStyles.Auto;
            this.fpChildPackage_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(217)))), ((int)(((byte)(234)))));
            this.fpChildPackage_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F);
            this.fpChildPackage_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpChildPackage_Sheet1.ColumnHeader.Rows.Default.VisualStyles = FarPoint.Win.VisualStyles.Auto;
            this.fpChildPackage_Sheet1.ColumnHeader.Rows.Get(0).Height = 22F;
            this.fpChildPackage_Sheet1.Columns.Get(0).CellType = checkBoxCellType1;
            this.fpChildPackage_Sheet1.Columns.Get(0).Label = " ";
            this.fpChildPackage_Sheet1.Columns.Get(0).Locked = false;
            this.fpChildPackage_Sheet1.Columns.Get(0).Width = 30F;
            this.fpChildPackage_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpChildPackage_Sheet1.Columns.Get(1).Label = "编码";
            this.fpChildPackage_Sheet1.Columns.Get(1).Locked = true;
            this.fpChildPackage_Sheet1.Columns.Get(1).Width = 122F;
            this.fpChildPackage_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpChildPackage_Sheet1.Columns.Get(2).Label = "项目名称";
            this.fpChildPackage_Sheet1.Columns.Get(2).Width = 180F;
            this.fpChildPackage_Sheet1.Columns.Get(3).Label = "次数";
            this.fpChildPackage_Sheet1.Columns.Get(3).Locked = false;
            this.fpChildPackage_Sheet1.Columns.Get(3).Width = 96F;
            this.fpChildPackage_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.fpChildPackage_Sheet1.Columns.Get(4).Label = "子项目编码";
            this.fpChildPackage_Sheet1.Columns.Get(4).Locked = true;
            this.fpChildPackage_Sheet1.Columns.Get(4).Width = 132F;
            this.fpChildPackage_Sheet1.Columns.Get(5).Label = "子项目名称";
            this.fpChildPackage_Sheet1.Columns.Get(5).Width = 122F;
            this.fpChildPackage_Sheet1.Columns.Get(6).Label = "金额";
            this.fpChildPackage_Sheet1.Columns.Get(6).Width = 99F;
            this.fpChildPackage_Sheet1.DefaultStyle.Font = new System.Drawing.Font("宋体", 10.5F);
            this.fpChildPackage_Sheet1.DefaultStyle.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpChildPackage_Sheet1.DefaultStyle.Locked = true;
            this.fpChildPackage_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpChildPackage_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.fpChildPackage_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpChildPackage_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpChildPackage_Sheet1.RowHeader.Columns.Get(0).Width = 19F;
            this.fpChildPackage_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(217)))), ((int)(((byte)(234)))));
            this.fpChildPackage_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpChildPackage_Sheet1.Rows.Default.Height = 22F;
            this.fpChildPackage_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpChildPackage_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // plLeftTop
            // 
            this.plLeftTop.Controls.Add(this.fpPackage);
            this.plLeftTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.plLeftTop.Location = new System.Drawing.Point(0, 0);
            this.plLeftTop.Name = "plLeftTop";
            this.plLeftTop.Size = new System.Drawing.Size(976, 190);
            this.plLeftTop.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plLeftTop.TabIndex = 6;
            // 
            // fpPackage
            // 
            this.fpPackage.About = "3.0.2004.2005";
            this.fpPackage.AccessibleDescription = "fpPackage, Sheet1, Row 0, Column 0, ";
            this.fpPackage.BackColor = System.Drawing.Color.White;
            this.fpPackage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpPackage.EditModePermanent = true;
            this.fpPackage.EditModeReplace = true;
            this.fpPackage.FileName = "";
            this.fpPackage.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpPackage.IsAutoSaveGridStatus = false;
            this.fpPackage.IsCanCustomConfigColumn = false;
            this.fpPackage.Location = new System.Drawing.Point(0, 0);
            this.fpPackage.Name = "fpPackage";
            this.fpPackage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpPackage.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpPackage_Sheet1});
            this.fpPackage.Size = new System.Drawing.Size(976, 190);
            this.fpPackage.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpPackage.TabIndex = 7;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpPackage.TextTipAppearance = tipAppearance2;
            this.fpPackage.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpPackage_Sheet1
            // 
            this.fpPackage_Sheet1.Reset();
            this.fpPackage_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpPackage_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpPackage_Sheet1.ColumnCount = 5;
            this.fpPackage_Sheet1.RowCount = 1;
            this.fpPackage_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = " ";
            this.fpPackage_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "编码";
            this.fpPackage_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "名称";
            this.fpPackage_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "类别";
            this.fpPackage_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "金额";
            this.fpPackage_Sheet1.ColumnHeader.Columns.Default.VisualStyles = FarPoint.Win.VisualStyles.Auto;
            this.fpPackage_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(217)))), ((int)(((byte)(234)))));
            this.fpPackage_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F);
            this.fpPackage_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpPackage_Sheet1.ColumnHeader.Rows.Default.VisualStyles = FarPoint.Win.VisualStyles.Auto;
            this.fpPackage_Sheet1.ColumnHeader.Rows.Get(0).Height = 22F;
            this.fpPackage_Sheet1.Columns.Get(0).CellType = checkBoxCellType2;
            this.fpPackage_Sheet1.Columns.Get(0).Label = " ";
            this.fpPackage_Sheet1.Columns.Get(0).Locked = false;
            this.fpPackage_Sheet1.Columns.Get(0).Width = 30F;
            this.fpPackage_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpPackage_Sheet1.Columns.Get(1).Label = "编码";
            this.fpPackage_Sheet1.Columns.Get(1).Locked = false;
            this.fpPackage_Sheet1.Columns.Get(1).Width = 104F;
            this.fpPackage_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpPackage_Sheet1.Columns.Get(2).Label = "名称";
            this.fpPackage_Sheet1.Columns.Get(2).Width = 200F;
            this.fpPackage_Sheet1.Columns.Get(3).Label = "类别";
            this.fpPackage_Sheet1.Columns.Get(3).Width = 74F;
            this.fpPackage_Sheet1.Columns.Get(4).Label = "金额";
            this.fpPackage_Sheet1.Columns.Get(4).Width = 121F;
            this.fpPackage_Sheet1.DefaultStyle.Font = new System.Drawing.Font("宋体", 10.5F);
            this.fpPackage_Sheet1.DefaultStyle.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpPackage_Sheet1.DefaultStyle.Locked = true;
            this.fpPackage_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpPackage_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.fpPackage_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpPackage_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpPackage_Sheet1.RowHeader.Columns.Get(0).Width = 19F;
            this.fpPackage_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(217)))), ((int)(((byte)(234)))));
            this.fpPackage_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpPackage_Sheet1.Rows.Default.Height = 22F;
            this.fpPackage_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpPackage_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ucPatientPayItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.plLeft);
            this.Name = "ucPatientPayItems";
            this.Size = new System.Drawing.Size(976, 326);
            this.plLeft.ResumeLayout(false);
            this.plLeftBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpChildPackage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpChildPackage_Sheet1)).EndInit();
            this.plLeftTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpPackage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPackage_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel plLeft;
        private FS.FrameWork.WinForms.Controls.NeuPanel plLeftBottom;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpChildPackage;
        private FarPoint.Win.Spread.SheetView fpChildPackage_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuPanel plLeftTop;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpPackage;
        private FarPoint.Win.Spread.SheetView fpPackage_Sheet1;


    }
}
