namespace Neusoft.SOC.HISFC.InpatientFee.Components.Balance
{
    partial class frmPreBalanceItem
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
            this.FpItemList = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.FpItemList_Sheet1 = new FarPoint.Win.Spread.SheetView();
            ((System.ComponentModel.ISupportInitialize)(this.FpItemList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpItemList_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // FpItemList
            // 
            this.FpItemList.About = "3.0.2004.2005";
            this.FpItemList.AccessibleDescription = "FpItemList, Sheet1, Row 0, Column 0, ";
            this.FpItemList.BackColor = System.Drawing.Color.White;
            this.FpItemList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FpItemList.FileName = "";
            this.FpItemList.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.FpItemList.IsAutoSaveGridStatus = false;
            this.FpItemList.IsCanCustomConfigColumn = false;
            this.FpItemList.Location = new System.Drawing.Point(0, 0);
            this.FpItemList.Name = "FpItemList";
            this.FpItemList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.FpItemList.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FpItemList_Sheet1});
            this.FpItemList.Size = new System.Drawing.Size(639, 300);
            this.FpItemList.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.FpItemList.TabIndex = 2;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FpItemList.TextTipAppearance = tipAppearance1;
            this.FpItemList.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // FpItemList_Sheet1
            // 
            this.FpItemList_Sheet1.Reset();
            this.FpItemList_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FpItemList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FpItemList_Sheet1.ColumnCount = 6;
            this.FpItemList_Sheet1.RowCount = 5;
            this.FpItemList_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "项目名称";
            this.FpItemList_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "规格/复合项目";
            this.FpItemList_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "单价";
            this.FpItemList_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "计价单位";
            this.FpItemList_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "数量";
            this.FpItemList_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "单位";
            this.FpItemList_Sheet1.Columns.Get(0).Label = "项目名称";
            this.FpItemList_Sheet1.Columns.Get(0).Locked = true;
            this.FpItemList_Sheet1.Columns.Get(0).Width = 200F;
            this.FpItemList_Sheet1.Columns.Get(1).Label = "规格/复合项目";
            this.FpItemList_Sheet1.Columns.Get(1).Locked = true;
            this.FpItemList_Sheet1.Columns.Get(1).Width = 160F;
            this.FpItemList_Sheet1.Columns.Get(2).Label = "单价";
            this.FpItemList_Sheet1.Columns.Get(2).Width = 54F;
            this.FpItemList_Sheet1.Columns.Get(3).Label = "计价单位";
            this.FpItemList_Sheet1.Columns.Get(3).Width = 56F;
            this.FpItemList_Sheet1.Columns.Get(5).Label = "单位";
            this.FpItemList_Sheet1.Columns.Get(5).Locked = true;
            this.FpItemList_Sheet1.Columns.Get(5).Width = 56F;
            this.FpItemList_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.FpItemList_Sheet1.RowHeader.Columns.Get(0).Width = 29F;
            this.FpItemList_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.FpItemList_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.FpItemList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // frmPreBalanceItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 300);
            this.Controls.Add(this.FpItemList);
            this.Name = "frmPreBalanceItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "预结算明细";
            ((System.ComponentModel.ISupportInitialize)(this.FpItemList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpItemList_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuSpread FpItemList;
        private FarPoint.Win.Spread.SheetView FpItemList_Sheet1;
    }
}