namespace FS.SOC.Local.InpatientFee.Base
{
    partial class ucWorkLoadQueryManagerMaintenance
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType7 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType8 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.panLeft = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.tvItemQuery = new FS.FrameWork.WinForms.Controls.NeuTreeView();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.txtQueryFilter = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuSplitter1 = new FS.FrameWork.WinForms.Controls.NeuSplitter();
            this.panMain = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSpread2 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread2_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.panTop = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lbTitle = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtResultFilter = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.grpMain = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.panLeft.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            this.neuPanel1.SuspendLayout();
            this.panMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread2_Sheet1)).BeginInit();
            this.panTop.SuspendLayout();
            this.grpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panLeft
            // 
            this.panLeft.Controls.Add(this.neuPanel2);
            this.panLeft.Controls.Add(this.neuPanel1);
            this.panLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panLeft.Location = new System.Drawing.Point(0, 0);
            this.panLeft.Name = "panLeft";
            this.panLeft.Size = new System.Drawing.Size(295, 619);
            this.panLeft.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panLeft.TabIndex = 1;
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.tvItemQuery);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel2.Location = new System.Drawing.Point(0, 55);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(295, 564);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 2;
            // 
            // tvItemQuery
            // 
            this.tvItemQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvItemQuery.HideSelection = false;
            this.tvItemQuery.Location = new System.Drawing.Point(0, 0);
            this.tvItemQuery.Name = "tvItemQuery";
            this.tvItemQuery.Size = new System.Drawing.Size(295, 564);
            this.tvItemQuery.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tvItemQuery.TabIndex = 0;
            this.tvItemQuery.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvItemQuery_AfterSelect);
            // 
            // neuPanel1
            // 
            this.neuPanel1.BackColor = System.Drawing.Color.Azure;
            this.neuPanel1.Controls.Add(this.txtQueryFilter);
            this.neuPanel1.Controls.Add(this.neuLabel1);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(295, 55);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 1;
            // 
            // txtQueryFilter
            // 
            this.txtQueryFilter.IsEnter2Tab = false;
            this.txtQueryFilter.Location = new System.Drawing.Point(114, 17);
            this.txtQueryFilter.Name = "txtQueryFilter";
            this.txtQueryFilter.Size = new System.Drawing.Size(153, 21);
            this.txtQueryFilter.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtQueryFilter.TabIndex = 1;
            this.txtQueryFilter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtQueryFilter_KeyUp);
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel1.Location = new System.Drawing.Point(19, 21);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(89, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "查询类型过滤：";
            // 
            // neuSplitter1
            // 
            this.neuSplitter1.BackColor = System.Drawing.Color.Azure;
            this.neuSplitter1.Location = new System.Drawing.Point(295, 0);
            this.neuSplitter1.Name = "neuSplitter1";
            this.neuSplitter1.Size = new System.Drawing.Size(3, 619);
            this.neuSplitter1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSplitter1.TabIndex = 2;
            this.neuSplitter1.TabStop = false;
            // 
            // panMain
            // 
            this.panMain.Controls.Add(this.neuSpread2);
            this.panMain.Controls.Add(this.panTop);
            this.panMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panMain.Location = new System.Drawing.Point(298, 55);
            this.panMain.Name = "panMain";
            this.panMain.Size = new System.Drawing.Size(823, 564);
            this.panMain.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panMain.TabIndex = 3;
            // 
            // neuSpread2
            // 
            this.neuSpread2.About = "3.0.2004.2005";
            this.neuSpread2.AccessibleDescription = "neuSpread2, Sheet1, Row 0, Column 0, ";
            this.neuSpread2.BackColor = System.Drawing.Color.White;
            this.neuSpread2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread2.FileName = "";
            this.neuSpread2.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread2.IsAutoSaveGridStatus = false;
            this.neuSpread2.IsCanCustomConfigColumn = false;
            this.neuSpread2.Location = new System.Drawing.Point(0, 55);
            this.neuSpread2.Name = "neuSpread2";
            this.neuSpread2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread2.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread2_Sheet1});
            this.neuSpread2.Size = new System.Drawing.Size(823, 509);
            this.neuSpread2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread2.TabIndex = 1;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread2.TextTipAppearance = tipAppearance1;
            this.neuSpread2.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread2.EditChange += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.neuSpread2_EditChange);
            // 
            // neuSpread2_Sheet1
            // 
            this.neuSpread2_Sheet1.Reset();
            this.neuSpread2_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread2_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread2_Sheet1.ColumnCount = 8;
            this.neuSpread2_Sheet1.RowCount = 0;
            this.neuSpread2_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin5", System.Drawing.SystemColors.Control, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Silver, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Azure, System.Drawing.Color.Black, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "编号";
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "查询类型";
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "科室编码";
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "科室";
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "项目代码";
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "项目名称";
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "项目内码";
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "顺序号";
            this.neuSpread2_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Azure;
            this.neuSpread2_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread2_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.neuSpread2_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread2_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.neuSpread2_Sheet1.Columns.Get(0).Label = "编号";
            this.neuSpread2_Sheet1.Columns.Get(1).AllowAutoSort = true;
            this.neuSpread2_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.neuSpread2_Sheet1.Columns.Get(1).Label = "查询类型";
            this.neuSpread2_Sheet1.Columns.Get(1).Width = 108F;
            this.neuSpread2_Sheet1.Columns.Get(2).AllowAutoSort = true;
            this.neuSpread2_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.neuSpread2_Sheet1.Columns.Get(2).Label = "科室编码";
            this.neuSpread2_Sheet1.Columns.Get(2).Width = 79F;
            this.neuSpread2_Sheet1.Columns.Get(3).CellType = textCellType4;
            this.neuSpread2_Sheet1.Columns.Get(3).Label = "科室";
            this.neuSpread2_Sheet1.Columns.Get(3).Width = 93F;
            this.neuSpread2_Sheet1.Columns.Get(4).AllowAutoSort = true;
            this.neuSpread2_Sheet1.Columns.Get(4).CellType = textCellType5;
            this.neuSpread2_Sheet1.Columns.Get(4).Label = "项目代码";
            this.neuSpread2_Sheet1.Columns.Get(4).Width = 104F;
            this.neuSpread2_Sheet1.Columns.Get(5).AllowAutoSort = true;
            this.neuSpread2_Sheet1.Columns.Get(5).CellType = textCellType6;
            this.neuSpread2_Sheet1.Columns.Get(5).Label = "项目名称";
            this.neuSpread2_Sheet1.Columns.Get(5).Width = 236F;
            this.neuSpread2_Sheet1.Columns.Get(6).CellType = textCellType7;
            this.neuSpread2_Sheet1.Columns.Get(6).Label = "项目内码";
            this.neuSpread2_Sheet1.Columns.Get(6).Width = 130F;
            this.neuSpread2_Sheet1.Columns.Get(7).CellType = textCellType8;
            this.neuSpread2_Sheet1.Columns.Get(7).Label = "顺序号";
            this.neuSpread2_Sheet1.Columns.Get(7).Width = 82F;
            this.neuSpread2_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread2_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.neuSpread2_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Azure;
            this.neuSpread2_Sheet1.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread2_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.neuSpread2_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread2_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Azure;
            this.neuSpread2_Sheet1.SheetCornerStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread2_Sheet1.SheetCornerStyle.Locked = false;
            this.neuSpread2_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuSpread2_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuSpread2_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuSpread2.SetActiveViewport(0, 1, 0);
            // 
            // panTop
            // 
            this.panTop.BackColor = System.Drawing.Color.Azure;
            this.panTop.Controls.Add(this.lbTitle);
            this.panTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panTop.Location = new System.Drawing.Point(0, 0);
            this.panTop.Name = "panTop";
            this.panTop.Size = new System.Drawing.Size(823, 55);
            this.panTop.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panTop.TabIndex = 0;
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTitle.Location = new System.Drawing.Point(312, 12);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(136, 21);
            this.lbTitle.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbTitle.TabIndex = 0;
            this.lbTitle.Text = "项目查询维护";
            // 
            // txtResultFilter
            // 
            this.txtResultFilter.IsEnter2Tab = false;
            this.txtResultFilter.Location = new System.Drawing.Point(83, 20);
            this.txtResultFilter.Name = "txtResultFilter";
            this.txtResultFilter.Size = new System.Drawing.Size(154, 21);
            this.txtResultFilter.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtResultFilter.TabIndex = 1;
            this.txtResultFilter.TextChanged += new System.EventHandler(this.txtResultFilter_TextChanged);
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel3.Location = new System.Drawing.Point(30, 24);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(47, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 0;
            this.neuLabel3.Text = "过 滤 :";
            // 
            // grpMain
            // 
            this.grpMain.BackColor = System.Drawing.Color.Azure;
            this.grpMain.Controls.Add(this.txtResultFilter);
            this.grpMain.Controls.Add(this.neuLabel3);
            this.grpMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpMain.Location = new System.Drawing.Point(298, 0);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(823, 55);
            this.grpMain.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.grpMain.TabIndex = 4;
            this.grpMain.TabStop = false;
            this.grpMain.Text = "过滤条件设定";
            // 
            // ucWorkLoadQueryManagerMaintenance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Controls.Add(this.panMain);
            this.Controls.Add(this.grpMain);
            this.Controls.Add(this.neuSplitter1);
            this.Controls.Add(this.panLeft);
            this.Name = "ucWorkLoadQueryManagerMaintenance";
            this.Size = new System.Drawing.Size(1121, 619);
            this.Load += new System.EventHandler(this.ucItemQueryManagerNew_Load);
            this.panLeft.ResumeLayout(false);
            this.neuPanel2.ResumeLayout(false);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.panMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread2_Sheet1)).EndInit();
            this.panTop.ResumeLayout(false);
            this.panTop.PerformLayout();
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel panLeft;
        private FS.FrameWork.WinForms.Controls.NeuSplitter neuSplitter1;
        private FS.FrameWork.WinForms.Controls.NeuPanel panMain;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox grpMain;
        private FS.FrameWork.WinForms.Controls.NeuPanel panTop;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtResultFilter;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread2;
        private FarPoint.Win.Spread.SheetView neuSpread2_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbTitle;
        private FS.FrameWork.WinForms.Controls.NeuTreeView tvItemQuery;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtQueryFilter;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
    }
}
