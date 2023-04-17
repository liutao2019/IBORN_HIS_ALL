namespace FS.HISFC.Components.Account.Controls
{
    partial class ucHealthCardManagerGJ
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
            this.components = new System.ComponentModel.Container();
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.spcard = new FarPoint.Win.Spread.SheetView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.neuGroupBox2 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.ucRegPatientInfo1 = new FS.HISFC.Components.Account.Controls.ucRegPatientInfoGJ();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtMarkNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.cmbIdCardType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.txtIdCardNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuTabControl1 = new FS.FrameWork.WinForms.Controls.NeuTabControl();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spcard)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.neuGroupBox2.SuspendLayout();
            this.neuGroupBox1.SuspendLayout();
            this.neuTabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(0, 454);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.spcard});
            this.neuSpread1.Size = new System.Drawing.Size(959, 116);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 2;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // spcard
            // 
            this.spcard.Reset();
            this.spcard.SheetName = "就诊卡信息";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.spcard.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.spcard.ColumnCount = 3;
            this.spcard.RowCount = 0;
            this.spcard.ActiveColumnIndex = 1;
            this.spcard.ColumnHeader.Cells.Get(0, 0).Value = "就诊卡号";
            this.spcard.ColumnHeader.Cells.Get(0, 1).Value = "卡类型";
            this.spcard.ColumnHeader.Cells.Get(0, 2).Value = "状态";
            this.spcard.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.spcard.ColumnHeader.DefaultStyle.Locked = false;
            this.spcard.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.spcard.Columns.Get(0).CellType = textCellType1;
            this.spcard.Columns.Get(0).Label = "就诊卡号";
            this.spcard.Columns.Get(0).Locked = true;
            this.spcard.Columns.Get(0).Width = 102F;
            this.spcard.Columns.Get(1).CellType = textCellType2;
            this.spcard.Columns.Get(1).Label = "卡类型";
            this.spcard.Columns.Get(1).Locked = true;
            this.spcard.Columns.Get(1).Width = 114F;
            this.spcard.Columns.Get(2).CellType = checkBoxCellType1;
            this.spcard.Columns.Get(2).Label = "状态";
            this.spcard.Columns.Get(2).Locked = true;
            this.spcard.Columns.Get(2).Width = 124F;
            this.spcard.GrayAreaBackColor = System.Drawing.Color.White;
            this.spcard.RowHeader.Columns.Default.Resizable = false;
            this.spcard.RowHeader.Columns.Get(0).Width = 37F;
            this.spcard.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.spcard.RowHeader.DefaultStyle.Locked = false;
            this.spcard.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.spcard.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.spcard.SheetCornerStyle.Locked = false;
            this.spcard.SheetCornerStyle.Parent = "HeaderDefault";
            this.spcard.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuSpread1.SetActiveViewport(0, 1, 0);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.neuGroupBox2);
            this.tabPage2.Controls.Add(this.neuGroupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(951, 427);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "健康卡管理";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // neuGroupBox2
            // 
            this.neuGroupBox2.Controls.Add(this.ucRegPatientInfo1);
            this.neuGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuGroupBox2.Location = new System.Drawing.Point(3, 43);
            this.neuGroupBox2.Name = "neuGroupBox2";
            this.neuGroupBox2.Size = new System.Drawing.Size(945, 381);
            this.neuGroupBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox2.TabIndex = 6;
            this.neuGroupBox2.TabStop = false;
            // 
            // ucRegPatientInfo1
            // 
            this.ucRegPatientInfo1.AutoCardNo = "";
            this.ucRegPatientInfo1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.ucRegPatientInfo1.CardNO = "";
            this.ucRegPatientInfo1.CardWay = true;
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
            this.ucRegPatientInfo1.IsInputName = true;
            this.ucRegPatientInfo1.IsInputPact = true;
            this.ucRegPatientInfo1.IsInputPHONE = true;
            this.ucRegPatientInfo1.IsInputSex = true;
            this.ucRegPatientInfo1.IsInputSiNo = false;
            this.ucRegPatientInfo1.IsInSequentialOrder = true;
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
            this.ucRegPatientInfo1.Size = new System.Drawing.Size(939, 361);
            this.ucRegPatientInfo1.TabIndex = 0;
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.neuLabel1);
            this.neuGroupBox1.Controls.Add(this.txtMarkNO);
            this.neuGroupBox1.Controls.Add(this.cmbIdCardType);
            this.neuGroupBox1.Controls.Add(this.txtIdCardNO);
            this.neuGroupBox1.Controls.Add(this.neuLabel5);
            this.neuGroupBox1.Controls.Add(this.neuLabel3);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox1.Location = new System.Drawing.Point(3, 3);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(945, 40);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 5;
            this.neuGroupBox1.TabStop = false;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(407, 14);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(65, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 32;
            this.neuLabel1.Text = "就诊卡号：";
            this.neuLabel1.Visible = false;
            // 
            // txtMarkNO
            // 
            this.txtMarkNO.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtMarkNO.IsEnter2Tab = false;
            this.txtMarkNO.Location = new System.Drawing.Point(478, 10);
            this.txtMarkNO.Name = "txtMarkNO";
            this.txtMarkNO.Size = new System.Drawing.Size(140, 21);
            this.txtMarkNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtMarkNO.TabIndex = 33;
            this.txtMarkNO.Visible = false;
            // 
            // cmbIdCardType
            // 
            this.cmbIdCardType.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbIdCardType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbIdCardType.FormattingEnabled = true;
            this.cmbIdCardType.IsEnter2Tab = false;
            this.cmbIdCardType.IsFlat = false;
            this.cmbIdCardType.IsLike = true;
            this.cmbIdCardType.IsListOnly = false;
            this.cmbIdCardType.IsPopForm = true;
            this.cmbIdCardType.IsShowCustomerList = false;
            this.cmbIdCardType.IsShowID = false;
            this.cmbIdCardType.IsShowIDAndName = false;
            this.cmbIdCardType.Location = new System.Drawing.Point(78, 10);
            this.cmbIdCardType.Name = "cmbIdCardType";
            this.cmbIdCardType.ShowCustomerList = false;
            this.cmbIdCardType.ShowID = false;
            this.cmbIdCardType.Size = new System.Drawing.Size(117, 20);
            this.cmbIdCardType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbIdCardType.TabIndex = 31;
            this.cmbIdCardType.Tag = "";
            this.cmbIdCardType.ToolBarUse = false;
            // 
            // txtIdCardNO
            // 
            this.txtIdCardNO.IsEnter2Tab = false;
            this.txtIdCardNO.Location = new System.Drawing.Point(264, 10);
            this.txtIdCardNO.Name = "txtIdCardNO";
            this.txtIdCardNO.Size = new System.Drawing.Size(126, 21);
            this.txtIdCardNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtIdCardNO.TabIndex = 29;
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Location = new System.Drawing.Point(204, 15);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(65, 12);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 28;
            this.neuLabel5.Text = "证 件 号：";
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(17, 14);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(65, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 27;
            this.neuLabel3.Text = "证件类型：";
            // 
            // neuTabControl1
            // 
            this.neuTabControl1.Controls.Add(this.tabPage2);
            this.neuTabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuTabControl1.ItemSize = new System.Drawing.Size(72, 19);
            this.neuTabControl1.Location = new System.Drawing.Point(0, 0);
            this.neuTabControl1.Name = "neuTabControl1";
            this.neuTabControl1.SelectedIndex = 0;
            this.neuTabControl1.Size = new System.Drawing.Size(959, 454);
            this.neuTabControl1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTabControl1.TabIndex = 0;
            // 
            // ucHealthCardManagerGJ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuSpread1);
            this.Controls.Add(this.neuTabControl1);
            this.Name = "ucHealthCardManagerGJ";
            this.Size = new System.Drawing.Size(959, 570);
            this.Load += new System.EventHandler(this.ucHealthCardManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spcard)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.neuGroupBox2.ResumeLayout(false);
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.neuTabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView spcard;
        private System.Windows.Forms.TabPage tabPage2;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox2;
        private ucRegPatientInfoGJ ucRegPatientInfo1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuTabControl neuTabControl1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtIdCardNO;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbIdCardType;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtMarkNO;
    }
}
