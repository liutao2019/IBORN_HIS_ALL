namespace FS.SOC.Local.DrugStore.NanZhuang.Inpatient
{
    partial class ucHerbalDrugBill
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
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.nlbFirstPrintTime = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbPrintTime = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbStockDeptName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlblBillNO = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbTitle = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.nlbFirstPrintTime);
            this.neuPanel1.Controls.Add(this.nlbPrintTime);
            this.neuPanel1.Controls.Add(this.nlbStockDeptName);
            this.neuPanel1.Controls.Add(this.nlblBillNO);
            this.neuPanel1.Controls.Add(this.lbTitle);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(731, 48);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // nlbFirstPrintTime
            // 
            this.nlbFirstPrintTime.AutoSize = true;
            this.nlbFirstPrintTime.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlbFirstPrintTime.Location = new System.Drawing.Point(467, 10);
            this.nlbFirstPrintTime.Name = "nlbFirstPrintTime";
            this.nlbFirstPrintTime.Size = new System.Drawing.Size(70, 12);
            this.nlbFirstPrintTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbFirstPrintTime.TabIndex = 40;
            this.nlbFirstPrintTime.Text = "首次打印：";
            // 
            // nlbPrintTime
            // 
            this.nlbPrintTime.AutoSize = true;
            this.nlbPrintTime.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlbPrintTime.Location = new System.Drawing.Point(467, 33);
            this.nlbPrintTime.Name = "nlbPrintTime";
            this.nlbPrintTime.Size = new System.Drawing.Size(203, 12);
            this.nlbPrintTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbPrintTime.TabIndex = 39;
            this.nlbPrintTime.Text = "打印时间：2010-12-27 00:00:00";
            // 
            // nlbStockDeptName
            // 
            this.nlbStockDeptName.AutoSize = true;
            this.nlbStockDeptName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlbStockDeptName.Location = new System.Drawing.Point(3, 33);
            this.nlbStockDeptName.Name = "nlbStockDeptName";
            this.nlbStockDeptName.Size = new System.Drawing.Size(70, 12);
            this.nlbStockDeptName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbStockDeptName.TabIndex = 38;
            this.nlbStockDeptName.Text = "发药科室：";
            // 
            // nlblBillNO
            // 
            this.nlblBillNO.AutoSize = true;
            this.nlblBillNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nlblBillNO.Location = new System.Drawing.Point(259, 33);
            this.nlblBillNO.Name = "nlblBillNO";
            this.nlblBillNO.Size = new System.Drawing.Size(44, 12);
            this.nlblBillNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlblBillNO.TabIndex = 37;
            this.nlblBillNO.Text = "单号：";
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTitle.Location = new System.Drawing.Point(237, 3);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(149, 19);
            this.lbTitle.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbTitle.TabIndex = 11;
            this.lbTitle.Text = "住院草药摆药单";
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, ";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(0, 48);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(731, 415);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 12;
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
            this.neuSpread1_Sheet1.ColumnCount = 8;
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin3", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.White, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, false, false);
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "编码";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "名称";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "每剂量";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "用法";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "编码";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "名称";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "每剂量";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "用法";
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "编码";
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 70F;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "名称";
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 132F;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "用法";
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 90F;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "编码";
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 70F;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "名称";
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 132F;
            this.neuSpread1_Sheet1.Columns.Get(7).Label = "用法";
            this.neuSpread1_Sheet1.Columns.Get(7).Width = 90F;
            this.neuSpread1_Sheet1.DefaultStyle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.RowHeader.Visible = false;
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.SheetCornerStyle.Locked = false;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuSpread1.SetActiveViewport(0, 1, 0);
            // 
            // ucHerbalDrugBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.neuSpread1);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucHerbalDrugBill";
            this.Size = new System.Drawing.Size(731, 463);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbTitle;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlblBillNO;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbStockDeptName;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbFirstPrintTime;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbPrintTime;

    }
}
