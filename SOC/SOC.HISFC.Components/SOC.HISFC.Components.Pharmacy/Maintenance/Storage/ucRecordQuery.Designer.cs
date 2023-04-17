namespace FS.SOC.HISFC.Components.Pharmacy.Maintenance
{

    partial class ucRecordQuery
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
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            this.neuPanelMain = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanelDetail = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSpreadDetail = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpreadDetail_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanelQuery = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.nbtExit = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.nbtExport = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.nbtQuery = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.ndtEnd = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.ndtBegin = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.nlbEnd = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbBegin = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanelMain.SuspendLayout();
            this.neuPanelDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpreadDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpreadDetail_Sheet1)).BeginInit();
            this.neuPanelQuery.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanelMain
            // 
            this.neuPanelMain.Controls.Add(this.neuPanelDetail);
            this.neuPanelMain.Controls.Add(this.neuPanelQuery);
            this.neuPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanelMain.Location = new System.Drawing.Point(0, 0);
            this.neuPanelMain.Name = "neuPanelMain";
            this.neuPanelMain.Size = new System.Drawing.Size(850, 450);
            this.neuPanelMain.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanelMain.TabIndex = 0;
            // 
            // neuPanelDetail
            // 
            this.neuPanelDetail.Controls.Add(this.neuSpreadDetail);
            this.neuPanelDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanelDetail.Location = new System.Drawing.Point(0, 61);
            this.neuPanelDetail.Name = "neuPanelDetail";
            this.neuPanelDetail.Size = new System.Drawing.Size(850, 389);
            this.neuPanelDetail.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanelDetail.TabIndex = 1;
            // 
            // neuSpreadDetail
            // 
            this.neuSpreadDetail.About = "3.0.2004.2005";
            this.neuSpreadDetail.AccessibleDescription = "neuSpreadDetail, Sheet1, Row 0, Column 0, ";
            this.neuSpreadDetail.BackColor = System.Drawing.Color.White;
            this.neuSpreadDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpreadDetail.FileName = "";
            this.neuSpreadDetail.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpreadDetail.IsAutoSaveGridStatus = false;
            this.neuSpreadDetail.IsCanCustomConfigColumn = false;
            this.neuSpreadDetail.Location = new System.Drawing.Point(0, 0);
            this.neuSpreadDetail.Name = "neuSpreadDetail";
            this.neuSpreadDetail.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpreadDetail.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpreadDetail_Sheet1});
            this.neuSpreadDetail.Size = new System.Drawing.Size(850, 389);
            this.neuSpreadDetail.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpreadDetail.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpreadDetail.TextTipAppearance = tipAppearance1;
            this.neuSpreadDetail.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // neuSpreadDetail_Sheet1
            // 
            this.neuSpreadDetail_Sheet1.Reset();
            this.neuSpreadDetail_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpreadDetail_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpreadDetail_Sheet1.ColumnCount = 0;
            this.neuSpreadDetail_Sheet1.ColumnHeader.RowCount = 0;
            this.neuSpreadDetail_Sheet1.RowCount = 0;
            this.neuSpreadDetail_Sheet1.RowHeader.ColumnCount = 0;
            this.neuSpreadDetail_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Black, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.neuSpreadDetail_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpreadDetail_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpreadDetail_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuSpreadDetail_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuSpreadDetail.SetActiveViewport(0, 1, 1);
            // 
            // neuPanelQuery
            // 
            this.neuPanelQuery.Controls.Add(this.nbtExit);
            this.neuPanelQuery.Controls.Add(this.nbtExport);
            this.neuPanelQuery.Controls.Add(this.nbtQuery);
            this.neuPanelQuery.Controls.Add(this.ndtEnd);
            this.neuPanelQuery.Controls.Add(this.ndtBegin);
            this.neuPanelQuery.Controls.Add(this.nlbEnd);
            this.neuPanelQuery.Controls.Add(this.nlbBegin);
            this.neuPanelQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanelQuery.Location = new System.Drawing.Point(0, 0);
            this.neuPanelQuery.Name = "neuPanelQuery";
            this.neuPanelQuery.Size = new System.Drawing.Size(850, 61);
            this.neuPanelQuery.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanelQuery.TabIndex = 0;
            // 
            // nbtExit
            // 
            this.nbtExit.Location = new System.Drawing.Point(181, 35);
            this.nbtExit.Name = "nbtExit";
            this.nbtExit.Size = new System.Drawing.Size(75, 23);
            this.nbtExit.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nbtExit.TabIndex = 6;
            this.nbtExit.Text = "退出";
            this.nbtExit.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.nbtExit.UseVisualStyleBackColor = true;
            this.nbtExit.Click += new System.EventHandler(this.nbtExit_Click);
            // 
            // nbtExport
            // 
            this.nbtExport.Location = new System.Drawing.Point(100, 35);
            this.nbtExport.Name = "nbtExport";
            this.nbtExport.Size = new System.Drawing.Size(75, 23);
            this.nbtExport.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nbtExport.TabIndex = 5;
            this.nbtExport.Text = "导出";
            this.nbtExport.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.nbtExport.UseVisualStyleBackColor = true;
            this.nbtExport.Click += new System.EventHandler(this.nbtExport_Click);
            // 
            // nbtQuery
            // 
            this.nbtQuery.Location = new System.Drawing.Point(19, 35);
            this.nbtQuery.Name = "nbtQuery";
            this.nbtQuery.Size = new System.Drawing.Size(75, 23);
            this.nbtQuery.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nbtQuery.TabIndex = 4;
            this.nbtQuery.Text = "查询";
            this.nbtQuery.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.nbtQuery.UseVisualStyleBackColor = true;
            this.nbtQuery.Click += new System.EventHandler(this.nbtQuery_Click);
            // 
            // ndtEnd
            // 
            this.ndtEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.ndtEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ndtEnd.IsEnter2Tab = false;
            this.ndtEnd.Location = new System.Drawing.Point(329, 7);
            this.ndtEnd.Name = "ndtEnd";
            this.ndtEnd.Size = new System.Drawing.Size(160, 21);
            this.ndtEnd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ndtEnd.TabIndex = 3;
            // 
            // ndtBegin
            // 
            this.ndtBegin.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.ndtBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ndtBegin.IsEnter2Tab = false;
            this.ndtBegin.Location = new System.Drawing.Point(78, 7);
            this.ndtBegin.Name = "ndtBegin";
            this.ndtBegin.Size = new System.Drawing.Size(160, 21);
            this.ndtBegin.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ndtBegin.TabIndex = 2;
            // 
            // nlbEnd
            // 
            this.nlbEnd.AutoSize = true;
            this.nlbEnd.Location = new System.Drawing.Point(267, 13);
            this.nlbEnd.Name = "nlbEnd";
            this.nlbEnd.Size = new System.Drawing.Size(65, 12);
            this.nlbEnd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbEnd.TabIndex = 1;
            this.nlbEnd.Text = "结束时间：";
            // 
            // nlbBegin
            // 
            this.nlbBegin.AutoSize = true;
            this.nlbBegin.Location = new System.Drawing.Point(17, 13);
            this.nlbBegin.Name = "nlbBegin";
            this.nlbBegin.Size = new System.Drawing.Size(65, 12);
            this.nlbBegin.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbBegin.TabIndex = 0;
            this.nlbBegin.Text = "开始时间：";
            // 
            // ucRecordQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuPanelMain);
            this.Name = "ucRecordQuery";
            this.Size = new System.Drawing.Size(850, 450);
            this.neuPanelMain.ResumeLayout(false);
            this.neuPanelDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpreadDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpreadDetail_Sheet1)).EndInit();
            this.neuPanelQuery.ResumeLayout(false);
            this.neuPanelQuery.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanelMain;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanelDetail;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanelQuery;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpreadDetail;
        private FarPoint.Win.Spread.SheetView neuSpreadDetail_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker ndtEnd;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker ndtBegin;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbEnd;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbBegin;
        private FS.FrameWork.WinForms.Controls.NeuButton nbtExport;
        private FS.FrameWork.WinForms.Controls.NeuButton nbtQuery;
        private FS.FrameWork.WinForms.Controls.NeuButton nbtExit;
    }
}