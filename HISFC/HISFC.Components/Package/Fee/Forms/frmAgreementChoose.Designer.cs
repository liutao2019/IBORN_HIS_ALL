namespace HISFC.Components.Package.Fee.Forms
{
    partial class frmAgreementChoose
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
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType1 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType2 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.fpAgreement = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpAgreement_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpAgreement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpAgreement_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(756, 38);
            this.panel1.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(756, 38);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "请勾选需要进行付费的合同条目";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnOK);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 340);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(756, 46);
            this.panel2.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(669, 9);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 28);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(586, 9);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 28);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Visible = false;
            // 
            // fpAgreement
            // 
            this.fpAgreement.About = "3.0.2004.2005";
            this.fpAgreement.AccessibleDescription = "fpAgreement, Sheet1, Row 0, Column 0, ";
            this.fpAgreement.BackColor = System.Drawing.Color.White;
            this.fpAgreement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpAgreement.EditModePermanent = true;
            this.fpAgreement.FileName = "";
            this.fpAgreement.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpAgreement.IsAutoSaveGridStatus = false;
            this.fpAgreement.IsCanCustomConfigColumn = false;
            this.fpAgreement.Location = new System.Drawing.Point(0, 38);
            this.fpAgreement.Name = "fpAgreement";
            this.fpAgreement.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpAgreement.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpAgreement_Sheet1});
            this.fpAgreement.Size = new System.Drawing.Size(756, 302);
            this.fpAgreement.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpAgreement.TabIndex = 2;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpAgreement.TextTipAppearance = tipAppearance1;
            this.fpAgreement.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpAgreement_Sheet1
            // 
            this.fpAgreement_Sheet1.Reset();
            this.fpAgreement_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpAgreement_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpAgreement_Sheet1.ColumnCount = 5;
            this.fpAgreement_Sheet1.RowCount = 5;
            this.fpAgreement_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = " ";
            this.fpAgreement_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "套餐名称";
            this.fpAgreement_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "套餐原价";
            this.fpAgreement_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "成交金额";
            this.fpAgreement_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "签订日期";
            this.fpAgreement_Sheet1.Columns.Get(0).CellType = checkBoxCellType1;
            this.fpAgreement_Sheet1.Columns.Get(0).Label = " ";
            this.fpAgreement_Sheet1.Columns.Get(0).Width = 21F;
            this.fpAgreement_Sheet1.Columns.Get(1).Label = "套餐名称";
            this.fpAgreement_Sheet1.Columns.Get(1).Width = 300F;
            currencyCellType1.DecimalPlaces = 2;
            this.fpAgreement_Sheet1.Columns.Get(2).CellType = currencyCellType1;
            this.fpAgreement_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.fpAgreement_Sheet1.Columns.Get(2).Label = "套餐原价";
            this.fpAgreement_Sheet1.Columns.Get(2).Width = 100F;
            currencyCellType2.DecimalPlaces = 2;
            this.fpAgreement_Sheet1.Columns.Get(3).CellType = currencyCellType2;
            this.fpAgreement_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.fpAgreement_Sheet1.Columns.Get(3).Label = "成交金额";
            this.fpAgreement_Sheet1.Columns.Get(3).Width = 100F;
            this.fpAgreement_Sheet1.Columns.Get(4).Label = "签订日期";
            this.fpAgreement_Sheet1.Columns.Get(4).Width = 184F;
            this.fpAgreement_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpAgreement_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpAgreement_Sheet1.RowHeader.Columns.Get(0).Width = 28F;
            this.fpAgreement_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpAgreement_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpAgreement_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpAgreement_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpAgreement_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // frmAgreementChoose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 386);
            this.Controls.Add(this.fpAgreement);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "frmAgreementChoose";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "已签订合同";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpAgreement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpAgreement_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private FarPoint.Win.Spread.SheetView fpAgreement_Sheet1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpAgreement;
    }
}