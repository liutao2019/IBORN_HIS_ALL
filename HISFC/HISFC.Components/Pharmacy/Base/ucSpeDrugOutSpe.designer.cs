namespace FS.HISFC.Components.Pharmacy.Base
{
	partial class ucSpeDrugOutSpe
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
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance( );
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType( );
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType( );
            FarPoint.Win.Spread.CellType.ComboBoxCellType comboBoxCellType1 = new FarPoint.Win.Spread.CellType.ComboBoxCellType( );
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType( );
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType( );
            FarPoint.Win.Spread.CellType.ComboBoxCellType comboBoxCellType2 = new FarPoint.Win.Spread.CellType.ComboBoxCellType( );
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread( );
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView( );
            this.neuSpread1_Sheet2 = new FarPoint.Win.Spread.SheetView( );
            ( ( System.ComponentModel.ISupportInitialize )( this.neuSpread1 ) ).BeginInit( );
            ( ( System.ComponentModel.ISupportInitialize )( this.neuSpread1_Sheet1 ) ).BeginInit( );
            ( ( System.ComponentModel.ISupportInitialize )( this.neuSpread1_Sheet2 ) ).BeginInit( );
            this.SuspendLayout( );
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "2.5.2007.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, 医生, Row 0, Column 0, ";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point( 0, 0 );
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.Sheets.AddRange( new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1,
            this.neuSpread1_Sheet2} );
            this.neuSpread1.Size = new System.Drawing.Size( 676, 488 );
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font( "宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte )( 134 ) ) );
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler( this.neuSpread1_CellDoubleClick );
            this.neuSpread1.ActiveSheetIndex = 1;
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset( );
            this.neuSpread1_Sheet1.SheetName = "科室";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ColumnCount = 6;
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin( "CustomSkin3", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Gray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, false );
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get( 0, 0 ).Value = "科室编码";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get( 0, 1 ).Value = "科室名称";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get( 0, 2 ).Value = "药品编码";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get( 0, 3 ).Value = "药品名称";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get( 0, 4 ).Value = "备注";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get( 0, 5 ).Value = "是否有效";
            this.neuSpread1_Sheet1.ColumnHeader.Rows.Get( 0 ).Height = 19F;
            this.neuSpread1_Sheet1.Columns.Get( 0 ).CellType = textCellType1;
            this.neuSpread1_Sheet1.Columns.Get( 0 ).Label = "科室编码";
            this.neuSpread1_Sheet1.Columns.Get( 0 ).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get( 1 ).CellType = textCellType2;
            this.neuSpread1_Sheet1.Columns.Get( 1 ).Label = "科室名称";
            this.neuSpread1_Sheet1.Columns.Get( 1 ).Width = 158F;
            this.neuSpread1_Sheet1.Columns.Get( 2 ).Label = "药品编码";
            this.neuSpread1_Sheet1.Columns.Get( 2 ).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get( 3 ).Label = "药品名称";
            this.neuSpread1_Sheet1.Columns.Get( 3 ).Width = 158F;
            this.neuSpread1_Sheet1.Columns.Get( 4 ).Label = "备注";
            this.neuSpread1_Sheet1.Columns.Get( 4 ).Width = 158F;
            comboBoxCellType1.ButtonAlign = FarPoint.Win.ButtonAlign.Right;
            comboBoxCellType1.ItemData = new string[] {
        "有效",
        "无效"};
            comboBoxCellType1.Items = new string[] {
        "有效",
        "无效"};
            this.neuSpread1_Sheet1.Columns.Get( 5 ).CellType = comboBoxCellType1;
            this.neuSpread1_Sheet1.Columns.Get( 5 ).Label = "是否有效";
            this.neuSpread1_Sheet1.Columns.Get( 5 ).Locked = false;
            this.neuSpread1_Sheet1.Columns.Get( 5 ).Width = 70F;
            this.neuSpread1_Sheet1.DefaultStyle.CellType = textCellType3;
            this.neuSpread1_Sheet1.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.Columns.Get( 0 ).Width = 37F;
            this.neuSpread1_Sheet1.RowHeader.Visible = false;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuSpread1.SetActiveViewport( 1, 0 );
            // 
            // neuSpread1_Sheet2
            // 
            this.neuSpread1_Sheet2.Reset( );
            this.neuSpread1_Sheet2.SheetName = "医生";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet2.ColumnCount = 6;
            this.neuSpread1_Sheet2.RowCount = 0;
            this.neuSpread1_Sheet2.ActiveSkin = new FarPoint.Win.Spread.SheetSkin( "CustomSkin2", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Gray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, false );
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get( 0, 0 ).Value = "医生编号";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get( 0, 1 ).Value = "医生姓名";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get( 0, 2 ).Value = "药品编码";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get( 0, 3 ).Value = "药品名称";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get( 0, 4 ).Value = "备注";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get( 0, 5 ).Value = "是否有效";
            this.neuSpread1_Sheet2.Columns.Get( 0 ).CellType = textCellType4;
            this.neuSpread1_Sheet2.Columns.Get( 0 ).Label = "医生编号";
            this.neuSpread1_Sheet2.Columns.Get( 0 ).Locked = true;
            this.neuSpread1_Sheet2.Columns.Get( 0 ).Width = 116F;
            this.neuSpread1_Sheet2.Columns.Get( 1 ).Label = "医生姓名";
            this.neuSpread1_Sheet2.Columns.Get( 1 ).Width = 114F;
            this.neuSpread1_Sheet2.Columns.Get( 2 ).Label = "药品编码";
            this.neuSpread1_Sheet2.Columns.Get( 2 ).Visible = false;
            this.neuSpread1_Sheet2.Columns.Get( 3 ).Label = "药品名称";
            this.neuSpread1_Sheet2.Columns.Get( 3 ).Width = 160F;
            this.neuSpread1_Sheet2.Columns.Get( 4 ).Label = "备注";
            this.neuSpread1_Sheet2.Columns.Get( 4 ).Width = 162F;
            comboBoxCellType2.ButtonAlign = FarPoint.Win.ButtonAlign.Right;
            comboBoxCellType2.ItemData = new string[] {
        "有效",
        "无效"};
            comboBoxCellType2.Items = new string[] {
        "有效",
        "无效"};
            this.neuSpread1_Sheet2.Columns.Get( 5 ).CellType = comboBoxCellType2;
            this.neuSpread1_Sheet2.Columns.Get( 5 ).Label = "是否有效";
            this.neuSpread1_Sheet2.Columns.Get( 5 ).Width = 70F;
            this.neuSpread1_Sheet2.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet2.RowHeader.Columns.Get( 0 ).Width = 37F;
            this.neuSpread1_Sheet2.RowHeader.Visible = false;
            this.neuSpread1_Sheet2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuSpread1.SetActiveViewport( 1, 1, 0 );
            // 
            // ucSpeDrugOutSpe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 12F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.neuSpread1 );
            this.Name = "ucSpeDrugOutSpe";
            this.Size = new System.Drawing.Size( 676, 488 );
            ( ( System.ComponentModel.ISupportInitialize )( this.neuSpread1 ) ).EndInit( );
            ( ( System.ComponentModel.ISupportInitialize )( this.neuSpread1_Sheet1 ) ).EndInit( );
            ( ( System.ComponentModel.ISupportInitialize )( this.neuSpread1_Sheet2 ) ).EndInit( );
            this.ResumeLayout( false );

		}

		#endregion

        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet2;
	}
}
