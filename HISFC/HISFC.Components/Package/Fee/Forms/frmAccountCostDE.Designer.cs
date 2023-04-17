namespace HISFC.Components.Package.Fee.Forms
{
    partial class frmAccountCostDE
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
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType1 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType2 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType3 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType4 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType5 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            this.plTop = new System.Windows.Forms.Panel();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lbName = new System.Windows.Forms.Label();
            this.tbMedcineNO = new System.Windows.Forms.TextBox();
            this.lbMedicine = new System.Windows.Forms.Label();
            this.plBottom = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.plAccountList = new System.Windows.Forms.Panel();
            this.FpAccount = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.FpAccount_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.plTop.SuspendLayout();
            this.plBottom.SuspendLayout();
            this.plAccountList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FpAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpAccount_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // plTop
            // 
            this.plTop.Controls.Add(this.tbName);
            this.plTop.Controls.Add(this.lbName);
            this.plTop.Controls.Add(this.tbMedcineNO);
            this.plTop.Controls.Add(this.lbMedicine);
            this.plTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.plTop.Location = new System.Drawing.Point(0, 0);
            this.plTop.Name = "plTop";
            this.plTop.Size = new System.Drawing.Size(593, 44);
            this.plTop.TabIndex = 0;
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(197, 12);
            this.tbName.Name = "tbName";
            this.tbName.ReadOnly = true;
            this.tbName.Size = new System.Drawing.Size(90, 21);
            this.tbName.TabIndex = 3;
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Location = new System.Drawing.Point(156, 15);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(35, 12);
            this.lbName.TabIndex = 2;
            this.lbName.Text = "姓名:";
            // 
            // tbMedcineNO
            // 
            this.tbMedcineNO.Location = new System.Drawing.Point(60, 12);
            this.tbMedcineNO.Name = "tbMedcineNO";
            this.tbMedcineNO.ReadOnly = true;
            this.tbMedcineNO.Size = new System.Drawing.Size(90, 21);
            this.tbMedcineNO.TabIndex = 1;
            // 
            // lbMedicine
            // 
            this.lbMedicine.AutoSize = true;
            this.lbMedicine.Location = new System.Drawing.Point(7, 15);
            this.lbMedicine.Name = "lbMedicine";
            this.lbMedicine.Size = new System.Drawing.Size(47, 12);
            this.lbMedicine.TabIndex = 0;
            this.lbMedicine.Text = "病历号:";
            // 
            // plBottom
            // 
            this.plBottom.Controls.Add(this.btnCancel);
            this.plBottom.Controls.Add(this.btnOK);
            this.plBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plBottom.Location = new System.Drawing.Point(0, 302);
            this.plBottom.Name = "plBottom";
            this.plBottom.Size = new System.Drawing.Size(593, 40);
            this.plBottom.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(493, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.ForeColor = System.Drawing.Color.Blue;
            this.btnOK.Location = new System.Drawing.Point(412, 6);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 30);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // plAccountList
            // 
            this.plAccountList.Controls.Add(this.FpAccount);
            this.plAccountList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plAccountList.Location = new System.Drawing.Point(0, 44);
            this.plAccountList.Name = "plAccountList";
            this.plAccountList.Size = new System.Drawing.Size(593, 258);
            this.plAccountList.TabIndex = 2;
            // 
            // FpAccount
            // 
            this.FpAccount.About = "3.0.2004.2005";
            this.FpAccount.AccessibleDescription = "FpAccount, Sheet1, Row 0, Column 0, ";
            this.FpAccount.BackColor = System.Drawing.Color.White;
            this.FpAccount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FpAccount.EditModePermanent = true;
            this.FpAccount.EditModeReplace = true;
            this.FpAccount.FileName = "";
            this.FpAccount.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.FpAccount.IsAutoSaveGridStatus = false;
            this.FpAccount.IsCanCustomConfigColumn = false;
            this.FpAccount.Location = new System.Drawing.Point(0, 0);
            this.FpAccount.Name = "FpAccount";
            this.FpAccount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.FpAccount.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FpAccount_Sheet1});
            this.FpAccount.Size = new System.Drawing.Size(593, 258);
            this.FpAccount.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.FpAccount.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FpAccount.TextTipAppearance = tipAppearance1;
            this.FpAccount.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // FpAccount_Sheet1
            // 
            this.FpAccount_Sheet1.Reset();
            this.FpAccount_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FpAccount_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FpAccount_Sheet1.ColumnCount = 6;
            this.FpAccount_Sheet1.RowCount = 2;
            this.FpAccount_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "账户类型";
            this.FpAccount_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "消费总金额";
            this.FpAccount_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "账户消费";
            this.FpAccount_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "赠送消费";
            this.FpAccount_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "账户余额";
            this.FpAccount_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "赠送余额";
            this.FpAccount_Sheet1.Columns.Get(0).Label = "账户类型";
            this.FpAccount_Sheet1.Columns.Get(0).Width = 100F;
            this.FpAccount_Sheet1.Columns.Get(1).CellType = currencyCellType1;
            this.FpAccount_Sheet1.Columns.Get(1).Label = "消费总金额";
            this.FpAccount_Sheet1.Columns.Get(1).Width = 90F;
            this.FpAccount_Sheet1.Columns.Get(2).CellType = currencyCellType2;
            this.FpAccount_Sheet1.Columns.Get(2).Label = "账户消费";
            this.FpAccount_Sheet1.Columns.Get(2).Width = 90F;
            this.FpAccount_Sheet1.Columns.Get(3).CellType = currencyCellType3;
            this.FpAccount_Sheet1.Columns.Get(3).Label = "赠送消费";
            this.FpAccount_Sheet1.Columns.Get(3).Width = 90F;
            this.FpAccount_Sheet1.Columns.Get(4).CellType = currencyCellType4;
            this.FpAccount_Sheet1.Columns.Get(4).Label = "账户余额";
            this.FpAccount_Sheet1.Columns.Get(4).Width = 90F;
            this.FpAccount_Sheet1.Columns.Get(5).CellType = currencyCellType5;
            this.FpAccount_Sheet1.Columns.Get(5).Label = "赠送余额";
            this.FpAccount_Sheet1.Columns.Get(5).Width = 90F;
            this.FpAccount_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.FpAccount_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.FpAccount_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.FpAccount_Sheet1.RowHeader.Columns.Get(0).Width = 21F;
            this.FpAccount_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.FpAccount_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.FpAccount_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.FpAccount_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.FpAccount_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // frmAccountCostDE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 342);
            this.Controls.Add(this.plAccountList);
            this.Controls.Add(this.plBottom);
            this.Controls.Add(this.plTop);
            this.Name = "frmAccountCostDE";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "账户消费";
            this.plTop.ResumeLayout(false);
            this.plTop.PerformLayout();
            this.plBottom.ResumeLayout(false);
            this.plAccountList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FpAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpAccount_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel plTop;
        private System.Windows.Forms.Panel plBottom;
        private System.Windows.Forms.Panel plAccountList;
        private FarPoint.Win.Spread.SheetView FpAccount_Sheet1;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.TextBox tbMedcineNO;
        private System.Windows.Forms.Label lbMedicine;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private FS.FrameWork.WinForms.Controls.NeuSpread FpAccount;
    }
}