namespace FS.HISFC.Components.Pharmacy.Base
{
    partial class ucSpeDrugMaintenance
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
			FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
			this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
			this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.ucDrugList1 = new FS.HISFC.Components.Common.Controls.ucDrugList();
			this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
			this.cmbDrug = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
			this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
			((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.neuGroupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// neuSpread1
			// 
			this.neuSpread1.About = "2.5.2007.2005";
			this.neuSpread1.AccessibleDescription = "neuSpread1";
			this.neuSpread1.BackColor = System.Drawing.Color.White;
			this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.neuSpread1.FileName = "";
			this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
			this.neuSpread1.IsAutoSaveGridStatus = false;
			this.neuSpread1.IsCanCustomConfigColumn = false;
			this.neuSpread1.Location = new System.Drawing.Point(0, 66);
			this.neuSpread1.Name = "neuSpread1";
			this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
			this.neuSpread1.Size = new System.Drawing.Size(583, 364);
			this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
			this.neuSpread1.TabIndex = 0;
			tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
			tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
			this.neuSpread1.TextTipAppearance = tipAppearance1;
			this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
			this.neuSpread1.EditChange += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.neuSpread1_EditChange);
			// 
			// neuSpread1_Sheet1
			// 
			this.neuSpread1_Sheet1.Reset();
			this.neuSpread1_Sheet1.SheetName = "Sheet1";
			// Formulas and custom names must be loaded with R1C1 reference style
			this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
			this.neuSpread1_Sheet1.ColumnCount = 12;
			this.neuSpread1_Sheet1.RowCount = 0;
			this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "特限药品编码";
			this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "特限药品名称";
			this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "规格";
			this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "特限药品上限量";
			this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "特限药品消耗量";
			this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "工号";
			this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "时间";
			this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "特限药品追加量";
			this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "消耗时间";
			this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "备注";
			this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "拼音码";
			this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "flag";
			this.neuSpread1_Sheet1.ColumnHeader.Rows.Get(0).Height = 40F;
			this.neuSpread1_Sheet1.Columns.Get(0).Label = "特限药品编码";
			this.neuSpread1_Sheet1.Columns.Get(0).Width = 46F;
			this.neuSpread1_Sheet1.Columns.Get(1).Label = "特限药品名称";
			this.neuSpread1_Sheet1.Columns.Get(1).Width = 80F;
			this.neuSpread1_Sheet1.Columns.Get(2).Label = "规格";
			this.neuSpread1_Sheet1.Columns.Get(2).Width = 47F;
			this.neuSpread1_Sheet1.Columns.Get(3).Label = "特限药品上限量";
			this.neuSpread1_Sheet1.Columns.Get(3).Width = 78F;
			this.neuSpread1_Sheet1.Columns.Get(4).CellType = textCellType1;
			this.neuSpread1_Sheet1.Columns.Get(4).Label = "特限药品消耗量";
			this.neuSpread1_Sheet1.Columns.Get(4).Width = 74F;
			this.neuSpread1_Sheet1.Columns.Get(5).Label = "工号";
			this.neuSpread1_Sheet1.Columns.Get(5).Visible = false;
			this.neuSpread1_Sheet1.Columns.Get(6).Label = "时间";
			this.neuSpread1_Sheet1.Columns.Get(6).Visible = false;
			this.neuSpread1_Sheet1.Columns.Get(7).Label = "特限药品追加量";
			this.neuSpread1_Sheet1.Columns.Get(7).Width = 77F;
			this.neuSpread1_Sheet1.Columns.Get(9).Label = "备注";
			this.neuSpread1_Sheet1.Columns.Get(9).Width = 130F;
			this.neuSpread1_Sheet1.Columns.Get(10).Label = "拼音码";
			this.neuSpread1_Sheet1.Columns.Get(10).Visible = false;
			this.neuSpread1_Sheet1.Columns.Get(11).Label = "flag";
			this.neuSpread1_Sheet1.Columns.Get(11).Visible = false;
			this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
			this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
			this.neuSpread1.SetActiveViewport(1, 0);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.ucDrugList1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.neuSpread1);
			this.splitContainer1.Panel2.Controls.Add(this.neuGroupBox1);
			this.splitContainer1.Size = new System.Drawing.Size(753, 430);
			this.splitContainer1.SplitterDistance = 166;
			this.splitContainer1.TabIndex = 1;
			// 
			// ucDrugList1
			// 
			this.ucDrugList1.BackColor = System.Drawing.Color.White;
			this.ucDrugList1.Caption = "药品选择－全部药品";
			this.ucDrugList1.DataTable = null;
			this.ucDrugList1.Dock = System.Windows.Forms.DockStyle.Fill;
			//this.ucDrugList1.DrugCode = "";
			this.ucDrugList1.FilterField = null;
			this.ucDrugList1.ImeMode = System.Windows.Forms.ImeMode.Hangul;
			this.ucDrugList1.IsPrint = false;
			this.ucDrugList1.Location = new System.Drawing.Point(0, 0);
			this.ucDrugList1.Name = "ucDrugList1";
			this.ucDrugList1.ShowTreeView = false;
			this.ucDrugList1.Size = new System.Drawing.Size(166, 430);
			this.ucDrugList1.TabIndex = 0;
			this.ucDrugList1.ChooseDataEvent += new FS.HISFC.Components.Common.Controls.ucDrugList.ChooseDataHandler(this.ucDrugList1_ChooseDataEvent);
			// 
			// neuGroupBox1
			// 
			this.neuGroupBox1.Controls.Add(this.cmbDrug);
			this.neuGroupBox1.Controls.Add(this.neuLabel1);
			this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.neuGroupBox1.Location = new System.Drawing.Point(0, 0);
			this.neuGroupBox1.Name = "neuGroupBox1";
			this.neuGroupBox1.Size = new System.Drawing.Size(583, 66);
			this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
			this.neuGroupBox1.TabIndex = 2;
			this.neuGroupBox1.TabStop = false;
			// 
			// cmbDrug
			// 
			this.cmbDrug.ArrowBackColor = System.Drawing.Color.Silver;
			this.cmbDrug.FormattingEnabled = true;
			this.cmbDrug.IsEnter2Tab = false;
			this.cmbDrug.IsFlat = true;
			this.cmbDrug.IsLike = true;
			this.cmbDrug.Location = new System.Drawing.Point(59, 22);
			this.cmbDrug.Name = "cmbDrug";
			this.cmbDrug.PopForm = null;
			this.cmbDrug.ShowCustomerList = false;
			this.cmbDrug.ShowID = false;
			this.cmbDrug.Size = new System.Drawing.Size(296, 20);
			this.cmbDrug.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
			this.cmbDrug.TabIndex = 2;
			this.cmbDrug.Tag = "";
			this.cmbDrug.ToolBarUse = false;
			this.cmbDrug.SelectedIndexChanged += new System.EventHandler(this.cmbDrug_SelectedIndexChanged);
			this.cmbDrug.TextChanged += new System.EventHandler(this.cmbDrug_TextChanged);
			// 
			// neuLabel1
			// 
			this.neuLabel1.AutoSize = true;
			this.neuLabel1.ForeColor = System.Drawing.Color.Blue;
			this.neuLabel1.Location = new System.Drawing.Point(6, 27);
			this.neuLabel1.Name = "neuLabel1";
			this.neuLabel1.Size = new System.Drawing.Size(47, 12);
			this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
			this.neuLabel1.TabIndex = 1;
			this.neuLabel1.Text = "过 滤：";
			// 
			// ucSpeDrugMaintenance
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Name = "ucSpeDrugMaintenance";
			this.Size = new System.Drawing.Size(753, 430);
			((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.neuGroupBox1.ResumeLayout(false);
			this.neuGroupBox1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private FS.HISFC.Components.Common.Controls.ucDrugList ucDrugList1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDrug;
    }
}
