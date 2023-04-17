namespace FS.HISFC.Components.Manager.Items
{
    partial class ucUndrugFeeRule
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
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ucUnDrugItems1 = new FS.HISFC.Components.Manager.Items.ucUnDrugItems();
            this.neuSplitter1 = new FS.FrameWork.WinForms.Controls.NeuSplitter();
            this.neuSplitter2 = new FS.FrameWork.WinForms.Controls.NeuSplitter();
            this.neuPanel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            ((System.ComponentModel.ISupportInitialize)(this.dvUndrugItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpreadItems)).BeginInit();
            this.neuPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // neuSpreadItems
            // 
            this.neuSpreadItems.Size = new System.Drawing.Size(886, 120);
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point(704, 24);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(59, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 0;
            this.neuLabel4.Text = "neuLabel4";
            // 
            // ucUnDrugItems1
            // 
            this.ucUnDrugItems1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.ucUnDrugItems1.CanModifyPrice = false;
            this.ucUnDrugItems1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucUnDrugItems1.IsFullConvertToHalf = true;
            this.ucUnDrugItems1.IsPrint = false;
            this.ucUnDrugItems1.Location = new System.Drawing.Point(0, 0);
            this.ucUnDrugItems1.Name = "ucUnDrugItems1";
            this.ucUnDrugItems1.Size = new System.Drawing.Size(886, 69);
            this.ucUnDrugItems1.TabIndex = 1;
            // 
            // neuSplitter1
            // 
            this.neuSplitter1.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.neuSplitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuSplitter1.Location = new System.Drawing.Point(0, 159);
            this.neuSplitter1.Name = "neuSplitter1";
            this.neuSplitter1.Size = new System.Drawing.Size(886, 13);
            this.neuSplitter1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSplitter1.TabIndex = 3;
            this.neuSplitter1.TabStop = false;
            // 
            // neuSplitter2
            // 
            this.neuSplitter2.Location = new System.Drawing.Point(0, 39);
            this.neuSplitter2.Name = "neuSplitter2";
            this.neuSplitter2.Size = new System.Drawing.Size(3, 120);
            this.neuSplitter2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSplitter2.TabIndex = 4;
            this.neuSplitter2.TabStop = false;
            // 
            // neuPanel3
            // 
            this.neuPanel3.Controls.Add(this.neuSpread1);
            this.neuPanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuPanel3.Location = new System.Drawing.Point(0, 172);
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size(886, 162);
            this.neuPanel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 5;
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(0, 0);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(886, 162);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 7;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.neuSpread1_CellDoubleClick);
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ColumnCount = 11;
            this.neuSpread1_Sheet1.RowCount = 1;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "规则编码";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "项目编码";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "自定义码";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "项目名称";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "限制条件";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "限制类型";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "限制数量";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "限制项目";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "操作员";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "操作时间";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "备注";
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "项目编码";
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 71F;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "项目名称";
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 120F;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "限制类型";
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 121F;
            this.neuSpread1_Sheet1.Columns.Get(7).Label = "限制项目";
            this.neuSpread1_Sheet1.Columns.Get(7).Width = 181F;
            this.neuSpread1_Sheet1.Columns.Get(10).Label = "备注";
            this.neuSpread1_Sheet1.Columns.Get(10).Width = 113F;
            this.neuSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.neuSpread1_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ucUndrugFeeRule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuSplitter2);
            this.Controls.Add(this.neuSplitter1);
            this.Controls.Add(this.neuPanel3);
            this.Name = "ucUndrugFeeRule";
            this.Size = new System.Drawing.Size(886, 334);
            this.Controls.SetChildIndex(this.neuPanel3, 0);
            this.Controls.SetChildIndex(this.neuSplitter1, 0);
            this.Controls.SetChildIndex(this.neuSpreadItems, 0);
            this.Controls.SetChildIndex(this.neuSplitter2, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dvUndrugItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpreadItems)).EndInit();
            this.neuPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private ucUnDrugItems ucUnDrugItems1;
        private FS.FrameWork.WinForms.Controls.NeuSplitter neuSplitter1;
        private FS.FrameWork.WinForms.Controls.NeuSplitter neuSplitter2;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel3;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
    }
}
