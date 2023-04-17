namespace FS.HISFC.Components.Account.Controls
{
    partial class ucEditPatientInfo
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
            FarPoint.Win.Spread.TipAppearance tipAppearance3 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.TextCellType textCellType13 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType14 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType15 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType16 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType17 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType18 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.txtMarkNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.spInfo = new FarPoint.Win.Spread.SheetView();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuGroupBox2 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.ucRegPatientInfo1 = new FS.HISFC.Components.Account.Controls.ucRegPatientInfo();
            this.neuPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spInfo)).BeginInit();
            this.neuGroupBox1.SuspendLayout();
            this.neuGroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMarkNO
            // 
            this.txtMarkNO.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtMarkNO.IsEnter2Tab = false;
            this.txtMarkNO.Location = new System.Drawing.Point(304, 14);
            this.txtMarkNO.Name = "txtMarkNO";
            this.txtMarkNO.Size = new System.Drawing.Size(120, 21);
            this.txtMarkNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtMarkNO.TabIndex = 1;
            this.txtMarkNO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMarkNO_KeyDown);
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(231, 18);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(53, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "就诊卡号";
            // 
            // neuPanel3
            // 
            this.neuPanel3.Controls.Add(this.neuSpread1);
            this.neuPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel3.Location = new System.Drawing.Point(0, 312);
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size(745, 201);
            this.neuPanel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 2;
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(0, 0);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.spInfo});
            this.neuSpread1.Size = new System.Drawing.Size(745, 201);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 0;
            tipAppearance3.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance3.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance3;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // spInfo
            // 
            this.spInfo.Reset();
            this.spInfo.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.spInfo.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.spInfo.ColumnCount = 6;
            this.spInfo.RowCount = 1;
            this.spInfo.ColumnHeader.Cells.Get(0, 0).Value = "编码";
            this.spInfo.ColumnHeader.Cells.Get(0, 1).Value = "名称";
            this.spInfo.ColumnHeader.Cells.Get(0, 2).Value = "编码";
            this.spInfo.ColumnHeader.Cells.Get(0, 3).Value = "名称";
            this.spInfo.ColumnHeader.Cells.Get(0, 4).Value = "编码";
            this.spInfo.ColumnHeader.Cells.Get(0, 5).Value = "名称";
            this.spInfo.Columns.Get(0).CellType = textCellType13;
            this.spInfo.Columns.Get(0).Label = "编码";
            this.spInfo.Columns.Get(0).Locked = true;
            this.spInfo.Columns.Get(0).Width = 100F;
            this.spInfo.Columns.Get(1).CellType = textCellType14;
            this.spInfo.Columns.Get(1).Label = "名称";
            this.spInfo.Columns.Get(1).Locked = true;
            this.spInfo.Columns.Get(1).Width = 170F;
            this.spInfo.Columns.Get(2).CellType = textCellType15;
            this.spInfo.Columns.Get(2).Label = "编码";
            this.spInfo.Columns.Get(2).Locked = true;
            this.spInfo.Columns.Get(2).Width = 100F;
            this.spInfo.Columns.Get(3).CellType = textCellType16;
            this.spInfo.Columns.Get(3).Label = "名称";
            this.spInfo.Columns.Get(3).Locked = true;
            this.spInfo.Columns.Get(3).Width = 170F;
            this.spInfo.Columns.Get(4).CellType = textCellType17;
            this.spInfo.Columns.Get(4).Label = "编码";
            this.spInfo.Columns.Get(4).Locked = true;
            this.spInfo.Columns.Get(4).Width = 100F;
            this.spInfo.Columns.Get(5).CellType = textCellType18;
            this.spInfo.Columns.Get(5).Label = "名称";
            this.spInfo.Columns.Get(5).Locked = true;
            this.spInfo.Columns.Get(5).Width = 170F;
            this.spInfo.RowHeader.Columns.Default.Resizable = false;
            this.spInfo.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.neuLabel4);
            this.neuGroupBox1.Controls.Add(this.neuLabel3);
            this.neuGroupBox1.Controls.Add(this.neuLabel2);
            this.neuGroupBox1.Controls.Add(this.txtName);
            this.neuGroupBox1.Controls.Add(this.neuLabel1);
            this.neuGroupBox1.Controls.Add(this.txtMarkNO);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(745, 48);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 4;
            this.neuGroupBox1.TabStop = false;
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.ForeColor = System.Drawing.Color.Red;
            this.neuLabel4.Location = new System.Drawing.Point(281, 18);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(17, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 0;
            this.neuLabel4.Text = "F2";
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.ForeColor = System.Drawing.Color.Red;
            this.neuLabel3.Location = new System.Drawing.Point(52, 18);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(17, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 0;
            this.neuLabel3.Text = "F1";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(25, 18);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(29, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 0;
            this.neuLabel2.Text = "姓名";
            // 
            // txtName
            // 
            this.txtName.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtName.IsEnter2Tab = false;
            this.txtName.Location = new System.Drawing.Point(75, 14);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(120, 21);
            this.txtName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtName.TabIndex = 1;
            this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
            // 
            // neuGroupBox2
            // 
            this.neuGroupBox2.Controls.Add(this.ucRegPatientInfo1);
            this.neuGroupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox2.Location = new System.Drawing.Point(0, 48);
            this.neuGroupBox2.Name = "neuGroupBox2";
            this.neuGroupBox2.Size = new System.Drawing.Size(745, 264);
            this.neuGroupBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox2.TabIndex = 5;
            this.neuGroupBox2.TabStop = false;
            this.neuGroupBox2.Text = "患者信息";
            // 
            // ucRegPatientInfo1
            // 
            this.ucRegPatientInfo1.AutoCardNo = "";
            this.ucRegPatientInfo1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.ucRegPatientInfo1.CardNO = "";
            this.ucRegPatientInfo1.CardWay = false;
            this.ucRegPatientInfo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucRegPatientInfo1.IMustInpubByOne = 0;
            this.ucRegPatientInfo1.IsAutoBuildCardNo = false;
            this.ucRegPatientInfo1.IsEditMode = false;
            this.ucRegPatientInfo1.IsEnableEntry = true;
            this.ucRegPatientInfo1.IsEnableIDENO = true;
            this.ucRegPatientInfo1.IsEnableIDEType = true;
            this.ucRegPatientInfo1.IsEnablePact = true;
            this.ucRegPatientInfo1.IsEnableSiNO = true;
            this.ucRegPatientInfo1.IsEnableVip = true;
            this.ucRegPatientInfo1.IsFullConvertToHalf = true;
            this.ucRegPatientInfo1.IsInputBirthDay = false;
            this.ucRegPatientInfo1.IsInputIDENO = false;
            this.ucRegPatientInfo1.IsInputIDEType = false;
            this.ucRegPatientInfo1.IsInputName = false;
            this.ucRegPatientInfo1.IsInputPact = false;
            this.ucRegPatientInfo1.IsInputSex = false;
            this.ucRegPatientInfo1.IsInputSiNo = false;
            this.ucRegPatientInfo1.IsJudgePact = false;
            this.ucRegPatientInfo1.IsJudgePactByIdno = false;
            this.ucRegPatientInfo1.IsJumpHomePhone = false;
            this.ucRegPatientInfo1.IsLocalOperation = true;
            this.ucRegPatientInfo1.IsMustInputTabIndex = false;
            this.ucRegPatientInfo1.IsMutilPactInfo = false;
            this.ucRegPatientInfo1.IsPrint = false;
            this.ucRegPatientInfo1.IsSelectPatientByNameIDCardByEnter = false;
            this.ucRegPatientInfo1.IsShowTitle = false;
            this.ucRegPatientInfo1.IsTreatment = false;
            this.ucRegPatientInfo1.IsValidHospitalStaff = "";
            this.ucRegPatientInfo1.Location = new System.Drawing.Point(3, 17);
            this.ucRegPatientInfo1.McardNO = "";
            this.ucRegPatientInfo1.Name = "ucRegPatientInfo1";
            this.ucRegPatientInfo1.ParentFormToolBar = null;
            this.ucRegPatientInfo1.Size = new System.Drawing.Size(739, 244);
            this.ucRegPatientInfo1.TabIndex = 0;
            // 
            // ucEditPatientInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuPanel3);
            this.Controls.Add(this.neuGroupBox2);
            this.Controls.Add(this.neuGroupBox1);
            this.Name = "ucEditPatientInfo";
            this.Size = new System.Drawing.Size(745, 513);
            this.Load += new System.EventHandler(this.ucEditPatientInfo_Load);
            this.neuPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spInfo)).EndInit();
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.neuGroupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuTextBox txtMarkNO;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel3;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView spInfo;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox2;
        private ucRegPatientInfo ucRegPatientInfo1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtName;
    }
}
