namespace HISFC.Components.Package.Fee.Forms
{
    partial class frmDeposit
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
            FS.FrameWork.WinForms.Controls.NeuLabel lbCardType;
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType1 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbMedicalNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblMedicalNO = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbLevel = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lblLevel = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbPhone = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblPhone = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbIDNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.tbAge = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.tbName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lbName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbSex = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbSex = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lbAge = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbRegDept = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbCardType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.fpPayMode = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpPayMode_Sheet1 = new FarPoint.Win.Spread.SheetView();
            lbCardType = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpPayMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPayMode_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbCardType
            // 
            lbCardType.AutoSize = true;
            lbCardType.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            lbCardType.ForeColor = System.Drawing.Color.Blue;
            lbCardType.Location = new System.Drawing.Point(18, 50);
            lbCardType.Name = "lbCardType";
            lbCardType.Size = new System.Drawing.Size(75, 14);
            lbCardType.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            lbCardType.TabIndex = 54;
            lbCardType.Text = "证件类型:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbMedicalNO);
            this.panel1.Controls.Add(this.lblMedicalNO);
            this.panel1.Controls.Add(this.cmbLevel);
            this.panel1.Controls.Add(this.lblLevel);
            this.panel1.Controls.Add(this.tbPhone);
            this.panel1.Controls.Add(this.lblPhone);
            this.panel1.Controls.Add(this.tbIDNO);
            this.panel1.Controls.Add(this.tbAge);
            this.panel1.Controls.Add(this.tbName);
            this.panel1.Controls.Add(this.lbName);
            this.panel1.Controls.Add(this.lbSex);
            this.panel1.Controls.Add(this.cmbSex);
            this.panel1.Controls.Add(this.lbAge);
            this.panel1.Controls.Add(this.lbRegDept);
            this.panel1.Controls.Add(this.cmbCardType);
            this.panel1.Controls.Add(lbCardType);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(627, 165);
            this.panel1.TabIndex = 0;
            // 
            // tbMedicalNO
            // 
            this.tbMedicalNO.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbMedicalNO.IsEnter2Tab = false;
            this.tbMedicalNO.Location = new System.Drawing.Point(110, 16);
            this.tbMedicalNO.Name = "tbMedicalNO";
            this.tbMedicalNO.ReadOnly = true;
            this.tbMedicalNO.Size = new System.Drawing.Size(187, 23);
            this.tbMedicalNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbMedicalNO.TabIndex = 51;
            this.tbMedicalNO.Tag = "MEDNO";
            // 
            // lblMedicalNO
            // 
            this.lblMedicalNO.AutoSize = true;
            this.lblMedicalNO.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMedicalNO.ForeColor = System.Drawing.Color.Red;
            this.lblMedicalNO.Location = new System.Drawing.Point(19, 19);
            this.lblMedicalNO.Name = "lblMedicalNO";
            this.lblMedicalNO.Size = new System.Drawing.Size(76, 14);
            this.lblMedicalNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblMedicalNO.TabIndex = 50;
            this.lblMedicalNO.Text = "病 历 号:";
            // 
            // cmbLevel
            // 
            this.cmbLevel.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbLevel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbLevel.Enabled = false;
            this.cmbLevel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbLevel.FormattingEnabled = true;
            this.cmbLevel.IsEnter2Tab = false;
            this.cmbLevel.IsFlat = false;
            this.cmbLevel.IsLike = true;
            this.cmbLevel.IsListOnly = false;
            this.cmbLevel.IsPopForm = true;
            this.cmbLevel.IsShowCustomerList = false;
            this.cmbLevel.IsShowID = false;
            this.cmbLevel.IsShowIDAndName = false;
            this.cmbLevel.Location = new System.Drawing.Point(398, 85);
            this.cmbLevel.Name = "cmbLevel";
            this.cmbLevel.ShowCustomerList = false;
            this.cmbLevel.ShowID = false;
            this.cmbLevel.Size = new System.Drawing.Size(189, 22);
            this.cmbLevel.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbLevel.TabIndex = 63;
            this.cmbLevel.Tag = "";
            this.cmbLevel.ToolBarUse = false;
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLevel.ForeColor = System.Drawing.Color.Blue;
            this.lblLevel.Location = new System.Drawing.Point(310, 88);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(75, 14);
            this.lblLevel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblLevel.TabIndex = 62;
            this.lblLevel.Text = "会员类型:";
            // 
            // tbPhone
            // 
            this.tbPhone.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbPhone.IsEnter2Tab = false;
            this.tbPhone.Location = new System.Drawing.Point(110, 121);
            this.tbPhone.Name = "tbPhone";
            this.tbPhone.ReadOnly = true;
            this.tbPhone.Size = new System.Drawing.Size(187, 23);
            this.tbPhone.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbPhone.TabIndex = 65;
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPhone.ForeColor = System.Drawing.Color.Blue;
            this.lblPhone.Location = new System.Drawing.Point(19, 124);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(75, 14);
            this.lblPhone.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblPhone.TabIndex = 64;
            this.lblPhone.Text = "联系电话:";
            // 
            // tbIDNO
            // 
            this.tbIDNO.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbIDNO.IsEnter2Tab = false;
            this.tbIDNO.Location = new System.Drawing.Point(398, 45);
            this.tbIDNO.Name = "tbIDNO";
            this.tbIDNO.ReadOnly = true;
            this.tbIDNO.Size = new System.Drawing.Size(189, 23);
            this.tbIDNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbIDNO.TabIndex = 57;
            // 
            // tbAge
            // 
            this.tbAge.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbAge.IsEnter2Tab = false;
            this.tbAge.Location = new System.Drawing.Point(231, 84);
            this.tbAge.Name = "tbAge";
            this.tbAge.ReadOnly = true;
            this.tbAge.Size = new System.Drawing.Size(66, 23);
            this.tbAge.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbAge.TabIndex = 61;
            // 
            // tbName
            // 
            this.tbName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbName.IsEnter2Tab = false;
            this.tbName.Location = new System.Drawing.Point(398, 16);
            this.tbName.Name = "tbName";
            this.tbName.ReadOnly = true;
            this.tbName.Size = new System.Drawing.Size(189, 23);
            this.tbName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbName.TabIndex = 53;
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbName.ForeColor = System.Drawing.Color.Blue;
            this.lbName.Location = new System.Drawing.Point(310, 19);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(75, 14);
            this.lbName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbName.TabIndex = 52;
            this.lbName.Text = "患者姓名:";
            // 
            // lbSex
            // 
            this.lbSex.AutoSize = true;
            this.lbSex.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbSex.ForeColor = System.Drawing.Color.Blue;
            this.lbSex.Location = new System.Drawing.Point(19, 87);
            this.lbSex.Name = "lbSex";
            this.lbSex.Size = new System.Drawing.Size(77, 14);
            this.lbSex.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbSex.TabIndex = 58;
            this.lbSex.Text = "性    别:";
            // 
            // cmbSex
            // 
            this.cmbSex.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbSex.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbSex.Enabled = false;
            this.cmbSex.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbSex.FormattingEnabled = true;
            this.cmbSex.IsEnter2Tab = false;
            this.cmbSex.IsFlat = false;
            this.cmbSex.IsLike = true;
            this.cmbSex.IsListOnly = false;
            this.cmbSex.IsPopForm = true;
            this.cmbSex.IsShowCustomerList = false;
            this.cmbSex.IsShowID = false;
            this.cmbSex.IsShowIDAndName = false;
            this.cmbSex.Location = new System.Drawing.Point(110, 84);
            this.cmbSex.Name = "cmbSex";
            this.cmbSex.ShowCustomerList = false;
            this.cmbSex.ShowID = false;
            this.cmbSex.Size = new System.Drawing.Size(62, 22);
            this.cmbSex.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbSex.TabIndex = 59;
            this.cmbSex.Tag = "";
            this.cmbSex.ToolBarUse = false;
            // 
            // lbAge
            // 
            this.lbAge.AutoSize = true;
            this.lbAge.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbAge.ForeColor = System.Drawing.Color.Blue;
            this.lbAge.Location = new System.Drawing.Point(180, 87);
            this.lbAge.Name = "lbAge";
            this.lbAge.Size = new System.Drawing.Size(45, 14);
            this.lbAge.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbAge.TabIndex = 60;
            this.lbAge.Text = "年龄:";
            // 
            // lbRegDept
            // 
            this.lbRegDept.AutoSize = true;
            this.lbRegDept.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbRegDept.ForeColor = System.Drawing.Color.Blue;
            this.lbRegDept.Location = new System.Drawing.Point(310, 50);
            this.lbRegDept.Name = "lbRegDept";
            this.lbRegDept.Size = new System.Drawing.Size(75, 14);
            this.lbRegDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbRegDept.TabIndex = 56;
            this.lbRegDept.Text = "证件号码:";
            // 
            // cmbCardType
            // 
            this.cmbCardType.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbCardType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbCardType.Enabled = false;
            this.cmbCardType.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbCardType.FormattingEnabled = true;
            this.cmbCardType.IsEnter2Tab = false;
            this.cmbCardType.IsFlat = false;
            this.cmbCardType.IsLike = true;
            this.cmbCardType.IsListOnly = false;
            this.cmbCardType.IsPopForm = true;
            this.cmbCardType.IsShowCustomerList = false;
            this.cmbCardType.IsShowID = false;
            this.cmbCardType.IsShowIDAndName = false;
            this.cmbCardType.Location = new System.Drawing.Point(110, 47);
            this.cmbCardType.Name = "cmbCardType";
            this.cmbCardType.ShowCustomerList = false;
            this.cmbCardType.ShowID = false;
            this.cmbCardType.Size = new System.Drawing.Size(187, 22);
            this.cmbCardType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbCardType.TabIndex = 55;
            this.cmbCardType.Tag = "";
            this.cmbCardType.ToolBarUse = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 414);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(627, 39);
            this.panel2.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(533, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 27);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.ForeColor = System.Drawing.Color.Blue;
            this.btnSave.Location = new System.Drawing.Point(448, 8);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 27);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "收取";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // fpPayMode
            // 
            this.fpPayMode.About = "3.0.2004.2005";
            this.fpPayMode.AccessibleDescription = "fpPayMode, Sheet1, Row 0, Column 0, ";
            this.fpPayMode.BackColor = System.Drawing.Color.White;
            this.fpPayMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpPayMode.EditModePermanent = true;
            this.fpPayMode.EditModeReplace = true;
            this.fpPayMode.FileName = "";
            this.fpPayMode.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpPayMode.IsAutoSaveGridStatus = false;
            this.fpPayMode.IsCanCustomConfigColumn = false;
            this.fpPayMode.Location = new System.Drawing.Point(0, 165);
            this.fpPayMode.Name = "fpPayMode";
            this.fpPayMode.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpPayMode.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpPayMode_Sheet1});
            this.fpPayMode.Size = new System.Drawing.Size(627, 249);
            this.fpPayMode.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpPayMode.TabIndex = 2;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpPayMode.TextTipAppearance = tipAppearance1;
            this.fpPayMode.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpPayMode_Sheet1
            // 
            this.fpPayMode_Sheet1.Reset();
            this.fpPayMode_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpPayMode_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpPayMode_Sheet1.ColumnCount = 3;
            this.fpPayMode_Sheet1.RowCount = 4;
            this.fpPayMode_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "缴纳方式";
            this.fpPayMode_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "缴纳金额";
            this.fpPayMode_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "备注";
            this.fpPayMode_Sheet1.Columns.Get(0).Label = "缴纳方式";
            this.fpPayMode_Sheet1.Columns.Get(0).Locked = true;
            this.fpPayMode_Sheet1.Columns.Get(0).Width = 120F;
            this.fpPayMode_Sheet1.Columns.Get(1).CellType = currencyCellType1;
            this.fpPayMode_Sheet1.Columns.Get(1).Label = "缴纳金额";
            this.fpPayMode_Sheet1.Columns.Get(1).Width = 120F;
            this.fpPayMode_Sheet1.Columns.Get(2).Label = "备注";
            this.fpPayMode_Sheet1.Columns.Get(2).Width = 338F;
            this.fpPayMode_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpPayMode_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpPayMode_Sheet1.RowHeader.Columns.Get(0).Width = 25F;
            this.fpPayMode_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpPayMode_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpPayMode_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpPayMode_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpPayMode_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // frmDeposit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 453);
            this.Controls.Add(this.fpPayMode);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmDeposit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "缴纳押金";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpPayMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPayMode_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox tbMedicalNO;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblMedicalNO;
        protected FS.FrameWork.WinForms.Controls.NeuComboBox cmbLevel;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblLevel;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox tbPhone;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lblPhone;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox tbIDNO;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox tbAge;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox tbName;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbName;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbSex;
        protected FS.FrameWork.WinForms.Controls.NeuComboBox cmbSex;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbAge;
        protected FS.FrameWork.WinForms.Controls.NeuLabel lbRegDept;
        protected FS.FrameWork.WinForms.Controls.NeuComboBox cmbCardType;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpPayMode;
        private FarPoint.Win.Spread.SheetView fpPayMode_Sheet1;
    }
}