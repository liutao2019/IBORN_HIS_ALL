namespace HISFC.Components.Package.Fee.Forms
{
    partial class frmAccountCost
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
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType1 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType2 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType3 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType4 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType5 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            this.plTop = new System.Windows.Forms.Panel();
            this.btnCard = new System.Windows.Forms.Button();
            this.tbMoeny = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lbName = new System.Windows.Forms.Label();
            this.tbMedcineNO = new System.Windows.Forms.TextBox();
            this.lbMedicine = new System.Windows.Forms.Label();
            this.plBottom = new System.Windows.Forms.Panel();
            this.lbCost = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.plAccountList = new System.Windows.Forms.Panel();
            this.FpAccount = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.FpAccount_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.labfamily = new System.Windows.Forms.Label();
            this.cmbFamily = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.plTop.SuspendLayout();
            this.plBottom.SuspendLayout();
            this.plAccountList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FpAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FpAccount_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // plTop
            // 
            this.plTop.Controls.Add(this.labfamily);
            this.plTop.Controls.Add(this.cmbFamily);
            this.plTop.Controls.Add(this.btnCard);
            this.plTop.Controls.Add(this.tbMoeny);
            this.plTop.Controls.Add(this.label1);
            this.plTop.Controls.Add(this.tbName);
            this.plTop.Controls.Add(this.lbName);
            this.plTop.Controls.Add(this.tbMedcineNO);
            this.plTop.Controls.Add(this.lbMedicine);
            this.plTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.plTop.Location = new System.Drawing.Point(0, 0);
            this.plTop.Name = "plTop";
            this.plTop.Size = new System.Drawing.Size(593, 77);
            this.plTop.TabIndex = 0;
            // 
            // btnCard
            // 
            this.btnCard.Location = new System.Drawing.Point(480, 46);
            this.btnCard.Name = "btnCard";
            this.btnCard.Size = new System.Drawing.Size(75, 23);
            this.btnCard.TabIndex = 8;
            this.btnCard.Text = "刷卡";
            this.btnCard.UseVisualStyleBackColor = true;
            // 
            // tbMoeny
            // 
            this.tbMoeny.Location = new System.Drawing.Point(370, 48);
            this.tbMoeny.Name = "tbMoeny";
            this.tbMoeny.ReadOnly = true;
            this.tbMoeny.Size = new System.Drawing.Size(90, 21);
            this.tbMoeny.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(293, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "待支付金额:";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(197, 48);
            this.tbName.Name = "tbName";
            this.tbName.ReadOnly = true;
            this.tbName.Size = new System.Drawing.Size(90, 21);
            this.tbName.TabIndex = 3;
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Location = new System.Drawing.Point(156, 51);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(35, 12);
            this.lbName.TabIndex = 2;
            this.lbName.Text = "姓名:";
            // 
            // tbMedcineNO
            // 
            this.tbMedcineNO.Location = new System.Drawing.Point(60, 48);
            this.tbMedcineNO.Name = "tbMedcineNO";
            this.tbMedcineNO.ReadOnly = true;
            this.tbMedcineNO.Size = new System.Drawing.Size(90, 21);
            this.tbMedcineNO.TabIndex = 1;
            // 
            // lbMedicine
            // 
            this.lbMedicine.AutoSize = true;
            this.lbMedicine.Location = new System.Drawing.Point(7, 51);
            this.lbMedicine.Name = "lbMedicine";
            this.lbMedicine.Size = new System.Drawing.Size(47, 12);
            this.lbMedicine.TabIndex = 0;
            this.lbMedicine.Text = "病历号:";
            // 
            // plBottom
            // 
            this.plBottom.Controls.Add(this.lbCost);
            this.plBottom.Controls.Add(this.btnCancel);
            this.plBottom.Controls.Add(this.btnOK);
            this.plBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plBottom.Location = new System.Drawing.Point(0, 302);
            this.plBottom.Name = "plBottom";
            this.plBottom.Size = new System.Drawing.Size(593, 40);
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
            this.plAccountList.Location = new System.Drawing.Point(0, 77);
            this.plAccountList.Name = "plAccountList";
            this.plAccountList.Size = new System.Drawing.Size(593, 225);
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
            this.FpAccount.Size = new System.Drawing.Size(593, 225);
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
            // labfamily
            // 
            this.labfamily.AutoSize = true;
            this.labfamily.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labfamily.Location = new System.Drawing.Point(8, 13);
            this.labfamily.Name = "labfamily";
            this.labfamily.Size = new System.Drawing.Size(80, 16);
            this.labfamily.TabIndex = 55;
            this.labfamily.Text = "家庭成员:";
            // 
            // cmbFamily
            // 
            this.cmbFamily.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbFamily.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbFamily.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbFamily.FormattingEnabled = true;
            this.cmbFamily.IsEnter2Tab = false;
            this.cmbFamily.IsFlat = false;
            this.cmbFamily.IsLike = true;
            this.cmbFamily.IsListOnly = false;
            this.cmbFamily.IsPopForm = true;
            this.cmbFamily.IsShowCustomerList = false;
            this.cmbFamily.IsShowID = false;
            this.cmbFamily.IsShowIDAndName = false;
            this.cmbFamily.Location = new System.Drawing.Point(98, 9);
            this.cmbFamily.Name = "cmbFamily";
            this.cmbFamily.ShowCustomerList = false;
            this.cmbFamily.ShowID = false;
            this.cmbFamily.Size = new System.Drawing.Size(181, 27);
            this.cmbFamily.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbFamily.TabIndex = 54;
            this.cmbFamily.Tag = "";
            this.cmbFamily.ToolBarUse = false;
            this.cmbFamily.SelectedValueChanged += new System.EventHandler(this.cmbFamily_SelectedValueChanged);
            // 
            // frmAccountCost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 342);
            this.Controls.Add(this.plAccountList);
            this.Controls.Add(this.plBottom);
            this.Controls.Add(this.plTop);
            this.Name = "frmAccountCost";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "账户消费";
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
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.TextBox tbMedcineNO;
        private System.Windows.Forms.Label lbMedicine;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox tbMoeny;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbCost;
        private FS.FrameWork.WinForms.Controls.NeuSpread FpAccount;
        private System.Windows.Forms.Button btnCard;
        private System.Windows.Forms.Label labfamily;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbFamily;
    }
}