namespace FS.HISFC.Components.Order.Controls
{
    partial class ucEMRQuery
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
            this.fpPatientNO = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpPatientNO_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.fpPatientNO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPatientNO_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // fpPatientNO
            // 
            this.fpPatientNO.About = "3.0.2004.2005";
            this.fpPatientNO.AccessibleDescription = "fpPatientNO, Sheet1, Row 0, Column 0, ";
            this.fpPatientNO.BackColor = System.Drawing.Color.White;
            this.fpPatientNO.Dock = System.Windows.Forms.DockStyle.Left;
            this.fpPatientNO.FileName = "";
            this.fpPatientNO.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpPatientNO.IsAutoSaveGridStatus = false;
            this.fpPatientNO.IsCanCustomConfigColumn = false;
            this.fpPatientNO.Location = new System.Drawing.Point(0, 0);
            this.fpPatientNO.Name = "fpPatientNO";
            this.fpPatientNO.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpPatientNO.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpPatientNO_Sheet1});
            this.fpPatientNO.Size = new System.Drawing.Size(145, 375);
            this.fpPatientNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpPatientNO.TabIndex = 2;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpPatientNO.TextTipAppearance = tipAppearance1;
            this.fpPatientNO.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpPatientNO_Sheet1
            // 
            this.fpPatientNO_Sheet1.Reset();
            this.fpPatientNO_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpPatientNO_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpPatientNO_Sheet1.ColumnCount = 1;
            this.fpPatientNO_Sheet1.RowCount = 2;
            this.fpPatientNO_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "住院号列表";
            this.fpPatientNO_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.fpPatientNO_Sheet1.Columns.Get(0).Label = "住院号列表";
            this.fpPatientNO_Sheet1.Columns.Get(0).Width = 106F;
            this.fpPatientNO_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
            this.fpPatientNO_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpPatientNO_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpPatientNO_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpPatientNO_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpPatientNO_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpPatientNO_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(145, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(628, 375);
            this.webBrowser1.TabIndex = 3;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(631, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(112, 23);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "进入新电子病历";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // ucEMRQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.fpPatientNO);
            this.Name = "ucEMRQuery";
            this.Size = new System.Drawing.Size(773, 375);
            ((System.ComponentModel.ISupportInitialize)(this.fpPatientNO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPatientNO_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuSpread fpPatientNO;
        private FarPoint.Win.Spread.SheetView fpPatientNO_Sheet1;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button btnRefresh;

    }
}
