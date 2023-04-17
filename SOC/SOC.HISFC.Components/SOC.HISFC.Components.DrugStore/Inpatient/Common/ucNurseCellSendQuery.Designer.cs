namespace FS.SOC.HISFC.Components.DrugStore.Inpatient.Common
{
    partial class ucNurseCellSendQuery
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
            this.neuPanelMain.SuspendLayout();
            this.neuPanelDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpreadDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpreadDetail_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // neuPanelMain
            // 
            this.neuPanelMain.Controls.Add(this.neuPanelDetail);
            this.neuPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanelMain.Location = new System.Drawing.Point(0, 0);
            this.neuPanelMain.Name = "neuPanelMain";
            this.neuPanelMain.Size = new System.Drawing.Size(812, 454);
            this.neuPanelMain.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanelMain.TabIndex = 0;
            // 
            // neuPanelDetail
            // 
            this.neuPanelDetail.Controls.Add(this.neuSpreadDetail);
            this.neuPanelDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanelDetail.Location = new System.Drawing.Point(0, 0);
            this.neuPanelDetail.Name = "neuPanelDetail";
            this.neuPanelDetail.Size = new System.Drawing.Size(812, 454);
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
            this.neuSpreadDetail.Size = new System.Drawing.Size(812, 454);
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
            // ucNurseCellSendQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuPanelMain);
            this.Name = "ucNurseCellSendQuery";
            this.Size = new System.Drawing.Size(812, 454);
            this.neuPanelMain.ResumeLayout(false);
            this.neuPanelDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpreadDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpreadDetail_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanelMain;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanelDetail;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpreadDetail;
        private FarPoint.Win.Spread.SheetView neuSpreadDetail_Sheet1;
    }
}