namespace API.GZSI.UI
{
    partial class frmEleInvoiceQuery
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
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.search = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.fpEleInvoice = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpEleInvoice_sheetView1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpEleInvoice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpEleInvoice_sheetView1)).BeginInit();
            this.SuspendLayout();
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.search);
            this.neuPanel2.Controls.Add(this.txtName);
            this.neuPanel2.Controls.Add(this.neuLabel2);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel2.Location = new System.Drawing.Point(0, 0);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(1080, 41);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 105;
            // 
            // search
            // 
            this.search.Location = new System.Drawing.Point(229, 9);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(75, 23);
            this.search.TabIndex = 8;
            this.search.Text = "查询";
            this.search.UseVisualStyleBackColor = true;
            this.search.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(53, 9);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(149, 21);
            this.txtName.TabIndex = 5;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(12, 15);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(35, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 4;
            this.neuLabel2.Text = "姓名:";
            // 
            // fpEleInvoice
            // 
            this.fpEleInvoice.About = "3.0.2004.2005";
            this.fpEleInvoice.AccessibleDescription = "fpEleInvoice, Sheet1, Row 0, Column 0, ";
            this.fpEleInvoice.BackColor = System.Drawing.Color.White;
            this.fpEleInvoice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpEleInvoice.EditModeReplace = true;
            this.fpEleInvoice.FileName = "";
            this.fpEleInvoice.Font = new System.Drawing.Font("宋体", 9F);
            this.fpEleInvoice.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpEleInvoice.IsAutoSaveGridStatus = false;
            this.fpEleInvoice.IsCanCustomConfigColumn = false;
            this.fpEleInvoice.Location = new System.Drawing.Point(0, 41);
            this.fpEleInvoice.Name = "fpEleInvoice";
            this.fpEleInvoice.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpEleInvoice.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpEleInvoice_sheetView1});
            this.fpEleInvoice.Size = new System.Drawing.Size(1080, 417);
            this.fpEleInvoice.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpEleInvoice.TabIndex = 106;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpEleInvoice.TextTipAppearance = tipAppearance2;
            this.fpEleInvoice.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpEleInvoice_sheetView1
            // 
            this.fpEleInvoice_sheetView1.Reset();
            this.fpEleInvoice_sheetView1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpEleInvoice_sheetView1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpEleInvoice_sheetView1.ColumnCount = 9;
            this.fpEleInvoice_sheetView1.RowCount = 1;
            this.fpEleInvoice_sheetView1.ColumnHeader.Cells.Get(0, 0).Value = "创建时间";
            this.fpEleInvoice_sheetView1.ColumnHeader.Cells.Get(0, 1).Value = "订单编号";
            this.fpEleInvoice_sheetView1.ColumnHeader.Cells.Get(0, 2).Value = "邮箱地址";
            this.fpEleInvoice_sheetView1.ColumnHeader.Cells.Get(0, 3).Value = "发票代码/发票号码";
            this.fpEleInvoice_sheetView1.ColumnHeader.Cells.Get(0, 4).Value = "发票抬头";
            this.fpEleInvoice_sheetView1.ColumnHeader.Cells.Get(0, 5).Value = "含税金额";
            this.fpEleInvoice_sheetView1.ColumnHeader.Cells.Get(0, 6).Value = "开票状态";
            this.fpEleInvoice_sheetView1.ColumnHeader.Cells.Get(0, 7).Value = "序列号";
            this.fpEleInvoice_sheetView1.ColumnHeader.Cells.Get(0, 8).Value = "重新获取";
            this.fpEleInvoice_sheetView1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpEleInvoice_sheetView1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpEleInvoice_sheetView1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.fpEleInvoice_sheetView1.Columns.Default.NoteIndicatorColor = System.Drawing.Color.White;
            this.fpEleInvoice_sheetView1.Columns.Get(0).BackColor = System.Drawing.Color.White;
            this.fpEleInvoice_sheetView1.Columns.Get(0).Label = "创建时间";
            this.fpEleInvoice_sheetView1.Columns.Get(0).Width = 98F;
            this.fpEleInvoice_sheetView1.Columns.Get(1).BackColor = System.Drawing.Color.White;
            this.fpEleInvoice_sheetView1.Columns.Get(1).Label = "订单编号";
            this.fpEleInvoice_sheetView1.Columns.Get(1).Locked = false;
            this.fpEleInvoice_sheetView1.Columns.Get(1).Width = 96F;
            this.fpEleInvoice_sheetView1.Columns.Get(2).BackColor = System.Drawing.Color.White;
            this.fpEleInvoice_sheetView1.Columns.Get(2).Label = "邮箱地址";
            this.fpEleInvoice_sheetView1.Columns.Get(2).Locked = false;
            this.fpEleInvoice_sheetView1.Columns.Get(2).Width = 120F;
            this.fpEleInvoice_sheetView1.Columns.Get(3).BackColor = System.Drawing.Color.White;
            this.fpEleInvoice_sheetView1.Columns.Get(3).Label = "发票代码/发票号码";
            this.fpEleInvoice_sheetView1.Columns.Get(3).Locked = false;
            this.fpEleInvoice_sheetView1.Columns.Get(3).Width = 166F;
            this.fpEleInvoice_sheetView1.Columns.Get(4).BackColor = System.Drawing.Color.White;
            this.fpEleInvoice_sheetView1.Columns.Get(4).Label = "发票抬头";
            this.fpEleInvoice_sheetView1.Columns.Get(4).Locked = false;
            this.fpEleInvoice_sheetView1.Columns.Get(4).Width = 115F;
            this.fpEleInvoice_sheetView1.Columns.Get(5).BackColor = System.Drawing.Color.White;
            this.fpEleInvoice_sheetView1.Columns.Get(5).Label = "含税金额";
            this.fpEleInvoice_sheetView1.Columns.Get(5).Locked = false;
            this.fpEleInvoice_sheetView1.Columns.Get(5).Width = 134F;
            this.fpEleInvoice_sheetView1.Columns.Get(6).Label = "开票状态";
            this.fpEleInvoice_sheetView1.Columns.Get(6).Width = 116F;
            this.fpEleInvoice_sheetView1.Columns.Get(7).Label = "序列号";
            this.fpEleInvoice_sheetView1.Columns.Get(7).Width = 110F;
            this.fpEleInvoice_sheetView1.Columns.Get(8).Label = "重新获取";
            this.fpEleInvoice_sheetView1.Columns.Get(8).Width = 109F;
            this.fpEleInvoice_sheetView1.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpEleInvoice_sheetView1.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.White;
            this.fpEleInvoice_sheetView1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpEleInvoice_sheetView1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpEleInvoice_sheetView1.RowHeader.Columns.Default.Resizable = true;
            this.fpEleInvoice_sheetView1.RowHeader.Columns.Get(0).Width = 37F;
            this.fpEleInvoice_sheetView1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpEleInvoice_sheetView1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpEleInvoice_sheetView1.Rows.Default.Height = 25F;
            this.fpEleInvoice_sheetView1.Rows.Default.NoteIndicatorColor = System.Drawing.Color.White;
            this.fpEleInvoice_sheetView1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpEleInvoice_sheetView1.SheetCornerStyle.Parent = "CornerDefault";
            this.fpEleInvoice_sheetView1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpEleInvoice_sheetView1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // frmEleInvoiceQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 458);
            this.Controls.Add(this.fpEleInvoice);
            this.Controls.Add(this.neuPanel2);
            this.Name = "frmEleInvoiceQuery";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "重新获取发票";
            this.neuPanel2.ResumeLayout(false);
            this.neuPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpEleInvoice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpEleInvoice_sheetView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private System.Windows.Forms.Button search;
        private System.Windows.Forms.TextBox txtName;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        protected FS.FrameWork.WinForms.Controls.NeuSpread fpEleInvoice;
        protected FarPoint.Win.Spread.SheetView fpEleInvoice_sheetView1;
    }
}