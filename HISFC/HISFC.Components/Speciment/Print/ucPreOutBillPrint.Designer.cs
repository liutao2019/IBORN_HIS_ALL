namespace FS.HISFC.Components.Speciment.Print
{
    partial class ucPreOutBillPrint
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbDemand = new System.Windows.Forms.Label();
            this.lbDrugType = new System.Windows.Forms.Label();
            this.lbTitle = new System.Windows.Forms.Label();
            this.lbOperDate = new System.Windows.Forms.Label();
            this.lbTargetDept = new System.Windows.Forms.Label();
            this.lbOutListNO = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lbDemand);
            this.panel2.Controls.Add(this.lbDrugType);
            this.panel2.Controls.Add(this.lbTitle);
            this.panel2.Controls.Add(this.lbOperDate);
            this.panel2.Controls.Add(this.lbTargetDept);
            this.panel2.Controls.Add(this.lbOutListNO);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(775, 74);
            this.panel2.TabIndex = 1;
            // 
            // lbDemand
            // 
            this.lbDemand.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbDemand.Location = new System.Drawing.Point(3, 33);
            this.lbDemand.Name = "lbDemand";
            this.lbDemand.Size = new System.Drawing.Size(769, 16);
            this.lbDemand.TabIndex = 8;
            this.lbDemand.Text = "出库说明：";
            this.lbDemand.Visible = false;
            // 
            // lbDrugType
            // 
            this.lbDrugType.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbDrugType.Location = new System.Drawing.Point(3, 54);
            this.lbDrugType.Name = "lbDrugType";
            this.lbDrugType.Size = new System.Drawing.Size(176, 16);
            this.lbDrugType.TabIndex = 7;
            this.lbDrugType.Text = "申请人：XXX";
            // 
            // lbTitle
            // 
            this.lbTitle.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTitle.Location = new System.Drawing.Point(261, 4);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(270, 24);
            this.lbTitle.TabIndex = 6;
            this.lbTitle.Text = "标本库预出库单";
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbOperDate
            // 
            this.lbOperDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbOperDate.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbOperDate.Location = new System.Drawing.Point(513, 55);
            this.lbOperDate.Name = "lbOperDate";
            this.lbOperDate.Size = new System.Drawing.Size(259, 16);
            this.lbOperDate.TabIndex = 5;
            this.lbOperDate.Text = "申请日期：";
            // 
            // lbTargetDept
            // 
            this.lbTargetDept.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTargetDept.Location = new System.Drawing.Point(222, 55);
            this.lbTargetDept.Name = "lbTargetDept";
            this.lbTargetDept.Size = new System.Drawing.Size(272, 16);
            this.lbTargetDept.TabIndex = 3;
            this.lbTargetDept.Text = "申请科室/部门：XXX";
            this.lbTargetDept.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbOutListNO
            // 
            this.lbOutListNO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbOutListNO.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbOutListNO.Location = new System.Drawing.Point(602, 9);
            this.lbOutListNO.Name = "lbOutListNO";
            this.lbOutListNO.Size = new System.Drawing.Size(170, 16);
            this.lbOutListNO.TabIndex = 2;
            this.lbOutListNO.Text = "申请单号：";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.neuSpread1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 74);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(775, 300);
            this.panel1.TabIndex = 2;
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "2.5.2007.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, ";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(0, 0);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(775, 300);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 1;
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
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "标本条码";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "标本号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "行";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "列";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "位置";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "数量";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "病种";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "类型";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "性质";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "归还";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "备注";
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "标本条码";
            this.neuSpread1_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 51F;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "标本号";
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 100F;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "行";
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 37F;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "列";
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 65F;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "位置";
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 254F;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "数量";
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 30F;
            this.neuSpread1_Sheet1.Columns.Get(6).Label = "病种";
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 30F;
            this.neuSpread1_Sheet1.Columns.Get(7).Label = "类型";
            this.neuSpread1_Sheet1.Columns.Get(7).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(7).Width = 53F;
            this.neuSpread1_Sheet1.Columns.Get(8).Label = "性质";
            this.neuSpread1_Sheet1.Columns.Get(8).Width = 50F;
            this.neuSpread1_Sheet1.Columns.Get(9).Label = "归还";
            this.neuSpread1_Sheet1.Columns.Get(9).Width = 50F;
            this.neuSpread1_Sheet1.Columns.Get(10).Label = "备注";
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 42F;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.SheetCornerStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.SheetCornerStyle.Locked = false;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuSpread1.SetActiveViewport(1, 0);
            // 
            // ucPreOutBillPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "ucPreOutBillPrint";
            this.Size = new System.Drawing.Size(775, 374);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbDrugType;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Label lbOperDate;
        private System.Windows.Forms.Label lbTargetDept;
        private System.Windows.Forms.Label lbOutListNO;
        private System.Windows.Forms.Panel panel1;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private System.Windows.Forms.Label lbDemand;
    }
}
