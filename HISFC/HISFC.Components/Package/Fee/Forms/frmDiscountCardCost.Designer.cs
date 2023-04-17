namespace HISFC.Components.Package.Fee.Forms
{
    partial class frmDiscountCardCost
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
            this.components = new System.ComponentModel.Container();
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.TextCellType currencyCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType currencyCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType currencyCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType currencyCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType5 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.TextCellType currencyCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.plTop = new System.Windows.Forms.Panel();
            this.cmbCardKind = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.txtCardNO = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbMedicine = new System.Windows.Forms.Label();
            this.plBottom = new System.Windows.Forms.Panel();
            this.lbCost = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.plAccountList = new System.Windows.Forms.Panel();
            this.FpAccount = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.FpAccount_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.tbMoeny = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.plTop.SuspendLayout();
            this.plBottom.SuspendLayout();
            this.plAccountList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FpAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpAccount_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // plTop
            // 
            this.plTop.Controls.Add(this.tbMoeny);
            this.plTop.Controls.Add(this.label2);
            this.plTop.Controls.Add(this.cmbCardKind);
            this.plTop.Controls.Add(this.txtCardNO);
            this.plTop.Controls.Add(this.label1);
            this.plTop.Controls.Add(this.lbMedicine);
            this.plTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.plTop.Location = new System.Drawing.Point(0, 0);
            this.plTop.Name = "plTop";
            this.plTop.Size = new System.Drawing.Size(660, 53);
            this.plTop.TabIndex = 0;
            // 
            // cmbCardKind
            // 
            this.cmbCardKind.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbCardKind.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbCardKind.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbCardKind.FormattingEnabled = true;
            this.cmbCardKind.IsEnter2Tab = false;
            this.cmbCardKind.IsFlat = false;
            this.cmbCardKind.IsLike = true;
            this.cmbCardKind.IsListOnly = false;
            this.cmbCardKind.IsPopForm = true;
            this.cmbCardKind.IsShowCustomerList = false;
            this.cmbCardKind.IsShowID = false;
            this.cmbCardKind.IsShowIDAndName = false;
            this.cmbCardKind.Location = new System.Drawing.Point(68, 12);
            this.cmbCardKind.Name = "cmbCardKind";
            this.cmbCardKind.ShowCustomerList = false;
            this.cmbCardKind.ShowID = false;
            this.cmbCardKind.Size = new System.Drawing.Size(105, 27);
            this.cmbCardKind.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbCardKind.TabIndex = 53;
            this.cmbCardKind.Tag = "";
            this.cmbCardKind.ToolBarUse = false;
            // 
            // txtCardNO
            // 
            this.txtCardNO.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtCardNO.Location = new System.Drawing.Point(243, 12);
            this.txtCardNO.Name = "txtCardNO";
            this.txtCardNO.Size = new System.Drawing.Size(159, 25);
            this.txtCardNO.TabIndex = 5;
            this.txtCardNO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCardNO_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(190, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "卡号:";
            // 
            // lbMedicine
            // 
            this.lbMedicine.AutoSize = true;
            this.lbMedicine.Location = new System.Drawing.Point(12, 20);
            this.lbMedicine.Name = "lbMedicine";
            this.lbMedicine.Size = new System.Drawing.Size(47, 12);
            this.lbMedicine.TabIndex = 2;
            this.lbMedicine.Text = "卡类型:";
            // 
            // plBottom
            // 
            this.plBottom.Controls.Add(this.lbCost);
            this.plBottom.Controls.Add(this.btnCancel);
            this.plBottom.Controls.Add(this.btnOK);
            this.plBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plBottom.Location = new System.Drawing.Point(0, 296);
            this.plBottom.Name = "plBottom";
            this.plBottom.Size = new System.Drawing.Size(660, 40);
            this.plBottom.TabIndex = 1;
            // 
            // lbCost
            // 
            this.lbCost.AutoSize = true;
            this.lbCost.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCost.ForeColor = System.Drawing.Color.Red;
            this.lbCost.Location = new System.Drawing.Point(12, 8);
            this.lbCost.Name = "lbCost";
            this.lbCost.Size = new System.Drawing.Size(80, 25);
            this.lbCost.TabIndex = 6;
            this.lbCost.Text = "￥0.00";
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
            this.plAccountList.Location = new System.Drawing.Point(0, 53);
            this.plAccountList.Name = "plAccountList";
            this.plAccountList.Size = new System.Drawing.Size(660, 243);
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
            this.FpAccount.Size = new System.Drawing.Size(660, 243);
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
            this.FpAccount_Sheet1.ColumnCount = 7;
            this.FpAccount_Sheet1.RowCount = 1;
            this.FpAccount_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "卡类型";
            this.FpAccount_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "卡类型名称";
            this.FpAccount_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "卡号";
            this.FpAccount_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "领卡客户";
            this.FpAccount_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "领卡时间";
            this.FpAccount_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "卡金额";
            this.FpAccount_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "发卡人员";
            this.FpAccount_Sheet1.Columns.Get(1).CellType = currencyCellType1;
            this.FpAccount_Sheet1.Columns.Get(1).Label = "卡类型名称";
            this.FpAccount_Sheet1.Columns.Get(1).Width = 90F;
            this.FpAccount_Sheet1.Columns.Get(2).CellType = currencyCellType2;
            this.FpAccount_Sheet1.Columns.Get(2).Label = "卡号";
            this.FpAccount_Sheet1.Columns.Get(3).CellType = currencyCellType3;
            this.FpAccount_Sheet1.Columns.Get(3).Label = "领卡客户";
            this.FpAccount_Sheet1.Columns.Get(3).Width = 90F;
            this.FpAccount_Sheet1.Columns.Get(4).CellType = currencyCellType4;
            this.FpAccount_Sheet1.Columns.Get(4).Label = "领卡时间";
            this.FpAccount_Sheet1.Columns.Get(4).Width = 150F;
            this.FpAccount_Sheet1.Columns.Get(5).CellType = currencyCellType5;
            this.FpAccount_Sheet1.Columns.Get(5).Label = "卡金额";
            this.FpAccount_Sheet1.Columns.Get(5).Width = 90F;
            this.FpAccount_Sheet1.Columns.Get(6).CellType = currencyCellType6;
            this.FpAccount_Sheet1.Columns.Get(6).Label = "发卡人员";
            this.FpAccount_Sheet1.Columns.Get(6).Width = 90F;
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
            // tbMoeny
            // 
            this.tbMoeny.Location = new System.Drawing.Point(502, 17);
            this.tbMoeny.Name = "tbMoeny";
            this.tbMoeny.ReadOnly = true;
            this.tbMoeny.Size = new System.Drawing.Size(90, 21);
            this.tbMoeny.TabIndex = 55;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(425, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 54;
            this.label2.Text = "待支付金额:";
            // 
            // frmDiscountCardCost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 336);
            this.Controls.Add(this.plAccountList);
            this.Controls.Add(this.plBottom);
            this.Controls.Add(this.plTop);
            this.Name = "frmDiscountCardCost";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "购物卡支付";
            this.plTop.ResumeLayout(false);
            this.plTop.PerformLayout();
            this.plBottom.ResumeLayout(false);
            this.plBottom.PerformLayout();
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
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lbCost;
        private FS.FrameWork.WinForms.Controls.NeuSpread FpAccount;
        private System.Windows.Forms.TextBox txtCardNO;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbMedicine;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbCardKind;
        private System.Windows.Forms.TextBox tbMoeny;
        private System.Windows.Forms.Label label2;
    }
}