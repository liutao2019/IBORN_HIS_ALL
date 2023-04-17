namespace FS.SOC.Local.OutpatientFee.FoSi.Controls
{
    partial class ucPatientList
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
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuContextMenuStrip1 = new FS.FrameWork.WinForms.Controls.NeuContextMenuStrip();
            this.miPrintFeedetial = new System.Windows.Forms.ToolStripMenuItem();
            this.miReprintInvoice = new System.Windows.Forms.ToolStripMenuItem();
            this.miReprintInvoiceNoRacl = new System.Windows.Forms.ToolStripMenuItem();
            this.miDeleteRow = new System.Windows.Forms.ToolStripMenuItem();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.btnReFresh = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.miModifyPayMode = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            this.neuContextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1";
            this.neuSpread1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.ContextMenuStrip = this.neuContextMenuStrip1;
            this.neuSpread1.FileName = "";
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(3, 33);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(166, 390);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.neuSpread1_CellDoubleClick);
            this.neuSpread1.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.neuSpread1_CellClick);
            this.neuSpread1.Paint += new System.Windows.Forms.PaintEventHandler(this.neuSpread1_Paint);
            // 
            // neuContextMenuStrip1
            // 
            this.neuContextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miModifyPayMode,
            this.miPrintFeedetial,
            this.miReprintInvoice,
            this.miReprintInvoiceNoRacl,
            this.miDeleteRow});
            this.neuContextMenuStrip1.Name = "neuContextMenuStrip1";
            this.neuContextMenuStrip1.Size = new System.Drawing.Size(153, 136);
            this.neuContextMenuStrip1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            // 
            // miPrintFeedetial
            // 
            this.miPrintFeedetial.Name = "miPrintFeedetial";
            this.miPrintFeedetial.Size = new System.Drawing.Size(152, 22);
            this.miPrintFeedetial.Text = "清单打印";
            this.miPrintFeedetial.Click += new System.EventHandler(this.miPrintFeedetial_Click);
            // 
            // miReprintInvoice
            // 
            this.miReprintInvoice.Name = "miReprintInvoice";
            this.miReprintInvoice.Size = new System.Drawing.Size(152, 22);
            this.miReprintInvoice.Text = "发票重打";
            this.miReprintInvoice.Click += new System.EventHandler(this.miReprintInvoice_Click);
            // 
            // miReprintInvoiceNoRacl
            // 
            this.miReprintInvoiceNoRacl.Name = "miReprintInvoiceNoRacl";
            this.miReprintInvoiceNoRacl.Size = new System.Drawing.Size(152, 22);
            this.miReprintInvoiceNoRacl.Text = "发票补打";
            this.miReprintInvoiceNoRacl.ToolTipText = "发票补打不走号";
            this.miReprintInvoiceNoRacl.Click += new System.EventHandler(this.miReprintInvoiceNoRacl_Click);
            // 
            // miDeleteRow
            // 
            this.miDeleteRow.Name = "miDeleteRow";
            this.miDeleteRow.Size = new System.Drawing.Size(152, 22);
            this.miDeleteRow.Text = "删除";
            this.miDeleteRow.Click += new System.EventHandler(this.miDeleteRow_Click);
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ColumnCount = 7;
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "姓名";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "金额";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "收费日期";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "院内号";
            this.neuSpread1_Sheet1.ColumnHeader.Rows.Get(0).Height = 21F;
            this.neuSpread1_Sheet1.Columns.Get(0).Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "姓名";
            this.neuSpread1_Sheet1.Columns.Get(0).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(0).VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 70F;
            this.neuSpread1_Sheet1.Columns.Get(1).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "金额";
            this.neuSpread1_Sheet1.Columns.Get(1).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 65F;
            this.neuSpread1_Sheet1.Columns.Get(2).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "收费日期";
            this.neuSpread1_Sheet1.Columns.Get(2).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 117F;
            this.neuSpread1_Sheet1.Columns.Get(3).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "院内号";
            this.neuSpread1_Sheet1.Columns.Get(3).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 90F;
            this.neuSpread1_Sheet1.Columns.Get(4).Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns.Get(5).Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 43F;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderDefault";
            this.neuSpread1_Sheet1.Rows.Default.Height = 25F;
            this.neuSpread1_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuSpread1.SetActiveViewport(0, 1, 0);
            // 
            // btnReFresh
            // 
            this.btnReFresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReFresh.Location = new System.Drawing.Point(79, 4);
            this.btnReFresh.Name = "btnReFresh";
            this.btnReFresh.Size = new System.Drawing.Size(87, 23);
            this.btnReFresh.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnReFresh.TabIndex = 1;
            this.btnReFresh.Text = "刷新列表";
            this.btnReFresh.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnReFresh.UseVisualStyleBackColor = true;
            this.btnReFresh.Click += new System.EventHandler(this.btnReFresh_Click);
            // 
            // miModifyPayMode
            // 
            this.miModifyPayMode.Name = "miModifyPayMode";
            this.miModifyPayMode.Size = new System.Drawing.Size(152, 22);
            this.miModifyPayMode.Text = "修改支付方式";
            this.miModifyPayMode.Click += new System.EventHandler(this.miModifyPayMode_Click);
            // 
            // ucPatientList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnReFresh);
            this.Controls.Add(this.neuSpread1);
            this.Name = "ucPatientList";
            this.Size = new System.Drawing.Size(172, 426);
            this.Load += new System.EventHandler(this.ucPatientList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            this.neuContextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuButton btnReFresh;
        private FS.FrameWork.WinForms.Controls.NeuContextMenuStrip neuContextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miPrintFeedetial;
        private System.Windows.Forms.ToolStripMenuItem miReprintInvoice;
        private System.Windows.Forms.ToolStripMenuItem miReprintInvoiceNoRacl;
        private System.Windows.Forms.ToolStripMenuItem miDeleteRow;
        private System.Windows.Forms.ToolStripMenuItem miModifyPayMode;

    }
}
