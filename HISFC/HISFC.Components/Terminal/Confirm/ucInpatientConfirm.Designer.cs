namespace FS.HISFC.Components.Terminal.Confirm
{
    partial class ucInpatientConfirm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            FarPoint.Win.Spread.TipAppearance tipAppearance4 = new FarPoint.Win.Spread.TipAppearance();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpItemInfo = new System.Windows.Forms.TabPage();
            this.fpExecOrder = new FS.FrameWork.WinForms.Controls.NeuFpEnter(this.components);
            this.fpExecOrder_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtFeeDetail = new System.Windows.Forms.TextBox();
            this.tpWebEMR = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lblTotCost = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ucQueryInpatientNo1 = new FS.HISFC.Components.Common.Controls.ucQueryInpatientNo();
            this.ckNone = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.ckCancel = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.ckComplete = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.lbBedName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbNurseCellName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblFreeCost = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblDept = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblPatientNO = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbDept = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuPanel1.SuspendLayout();
            this.neuPanel3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpItemInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpExecOrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpExecOrder_Sheet1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tpWebEMR.SuspendLayout();
            this.panel1.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.neuPanel3);
            this.neuPanel1.Controls.Add(this.neuPanel2);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(918, 474);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // neuPanel3
            // 
            this.neuPanel3.Controls.Add(this.tabControl1);
            this.neuPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel3.Location = new System.Drawing.Point(0, 90);
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size(918, 384);
            this.neuPanel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpItemInfo);
            this.tabControl1.Controls.Add(this.tpWebEMR);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(918, 384);
            this.tabControl1.TabIndex = 3;
            // 
            // tpItemInfo
            // 
            this.tpItemInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tpItemInfo.Controls.Add(this.fpExecOrder);
            this.tpItemInfo.Controls.Add(this.groupBox1);
            this.tpItemInfo.Location = new System.Drawing.Point(4, 26);
            this.tpItemInfo.Name = "tpItemInfo";
            this.tpItemInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpItemInfo.Size = new System.Drawing.Size(910, 354);
            this.tpItemInfo.TabIndex = 0;
            this.tpItemInfo.Text = "项目信息";
            this.tpItemInfo.UseVisualStyleBackColor = true;
            // 
            // fpExecOrder
            // 
            this.fpExecOrder.About = "3.0.2004.2005";
            this.fpExecOrder.AccessibleDescription = "fpExecOrder, Sheet1, Row 0, Column 0, ";
            this.fpExecOrder.BackColor = System.Drawing.Color.White;
            this.fpExecOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpExecOrder.EditModePermanent = true;
            this.fpExecOrder.EditModeReplace = true;
            this.fpExecOrder.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpExecOrder.Location = new System.Drawing.Point(3, 3);
            this.fpExecOrder.Name = "fpExecOrder";
            this.fpExecOrder.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpExecOrder.SelectNone = false;
            this.fpExecOrder.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpExecOrder_Sheet1});
            this.fpExecOrder.ShowListWhenOfFocus = false;
            this.fpExecOrder.Size = new System.Drawing.Size(904, 154);
            this.fpExecOrder.TabIndex = 0;
            tipAppearance4.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance4.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpExecOrder.TextTipAppearance = tipAppearance4;
            this.fpExecOrder.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpExecOrder.EditChange += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.fpExecOrder_EditChange);
            this.fpExecOrder.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpExecOrder_CellDoubleClick);
            this.fpExecOrder.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpExecOrder_CellClick);
            // 
            // fpExecOrder_Sheet1
            // 
            this.fpExecOrder_Sheet1.Reset();
            this.fpExecOrder_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpExecOrder_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpExecOrder_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpExecOrder_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpExecOrder_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpExecOrder_Sheet1.ColumnHeader.Rows.Get(0).Height = 35F;
            this.fpExecOrder_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpExecOrder_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpExecOrder_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.fpExecOrder_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpExecOrder_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpExecOrder_Sheet1.Rows.Default.Height = 25F;
            this.fpExecOrder_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpExecOrder_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.fpExecOrder_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpExecOrder_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtFeeDetail);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(3, 157);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(904, 194);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "费用明细";
            // 
            // txtFeeDetail
            // 
            this.txtFeeDetail.BackColor = System.Drawing.Color.White;
            this.txtFeeDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFeeDetail.Location = new System.Drawing.Point(3, 22);
            this.txtFeeDetail.Multiline = true;
            this.txtFeeDetail.Name = "txtFeeDetail";
            this.txtFeeDetail.Size = new System.Drawing.Size(898, 169);
            this.txtFeeDetail.TabIndex = 0;
            // 
            // tpWebEMR
            // 
            this.tpWebEMR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.tpWebEMR.Controls.Add(this.panel1);
            this.tpWebEMR.Location = new System.Drawing.Point(4, 26);
            this.tpWebEMR.Name = "tpWebEMR";
            this.tpWebEMR.Padding = new System.Windows.Forms.Padding(3);
            this.tpWebEMR.Size = new System.Drawing.Size(784, 354);
            this.tpWebEMR.TabIndex = 1;
            this.tpWebEMR.Text = "病历信息";
            this.tpWebEMR.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.webBrowser1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(778, 348);
            this.panel1.TabIndex = 0;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(778, 348);
            this.webBrowser1.TabIndex = 0;
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.cmbDept);
            this.neuPanel2.Controls.Add(this.neuLabel4);
            this.neuPanel2.Controls.Add(this.lblTotCost);
            this.neuPanel2.Controls.Add(this.neuLabel3);
            this.neuPanel2.Controls.Add(this.neuLabel2);
            this.neuPanel2.Controls.Add(this.neuLabel1);
            this.neuPanel2.Controls.Add(this.ucQueryInpatientNo1);
            this.neuPanel2.Controls.Add(this.ckNone);
            this.neuPanel2.Controls.Add(this.ckCancel);
            this.neuPanel2.Controls.Add(this.ckComplete);
            this.neuPanel2.Controls.Add(this.lbBedName);
            this.neuPanel2.Controls.Add(this.lbNurseCellName);
            this.neuPanel2.Controls.Add(this.lblFreeCost);
            this.neuPanel2.Controls.Add(this.lblDept);
            this.neuPanel2.Controls.Add(this.lblPatientNO);
            this.neuPanel2.Controls.Add(this.lblName);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel2.Location = new System.Drawing.Point(0, 0);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(918, 90);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 0;
            // 
            // lblTotCost
            // 
            this.lblTotCost.AutoSize = true;
            this.lblTotCost.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTotCost.ForeColor = System.Drawing.Color.Blue;
            this.lblTotCost.Location = new System.Drawing.Point(6, 64);
            this.lblTotCost.Name = "lblTotCost";
            this.lblTotCost.Size = new System.Drawing.Size(69, 19);
            this.lblTotCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblTotCost.TabIndex = 21;
            this.lblTotCost.Text = "金额：";
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(461, 43);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(41, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 20;
            this.neuLabel3.Text = "性别：";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(306, 43);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(65, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 19;
            this.neuLabel2.Text = "合同单位：";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(209, 43);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(41, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 18;
            this.neuLabel1.Text = "年龄：";
            // 
            // ucQueryInpatientNo1
            // 
            this.ucQueryInpatientNo1.DefaultInputType = 0;
            this.ucQueryInpatientNo1.InputType = 0;
            this.ucQueryInpatientNo1.IsDeptOnly = true;
            this.ucQueryInpatientNo1.Location = new System.Drawing.Point(3, 6);
            this.ucQueryInpatientNo1.Name = "ucQueryInpatientNo1";
            this.ucQueryInpatientNo1.PatientInState = "ALL";
            this.ucQueryInpatientNo1.ShowState = FS.HISFC.Components.Common.Controls.enuShowState.All;
            this.ucQueryInpatientNo1.Size = new System.Drawing.Size(176, 27);
            this.ucQueryInpatientNo1.TabIndex = 17;
            // 
            // ckNone
            // 
            this.ckNone.AutoSize = true;
            this.ckNone.Checked = true;
            this.ckNone.ForeColor = System.Drawing.Color.Black;
            this.ckNone.Location = new System.Drawing.Point(682, 68);
            this.ckNone.Name = "ckNone";
            this.ckNone.Size = new System.Drawing.Size(167, 16);
            this.ckNone.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ckNone.TabIndex = 8;
            this.ckNone.TabStop = true;
            this.ckNone.Text = "暂不处理：表示暂时不确认";
            this.ckNone.UseVisualStyleBackColor = true;
            // 
            // ckCancel
            // 
            this.ckCancel.AutoSize = true;
            this.ckCancel.ForeColor = System.Drawing.Color.Red;
            this.ckCancel.Location = new System.Drawing.Point(434, 68);
            this.ckCancel.Name = "ckCancel";
            this.ckCancel.Size = new System.Drawing.Size(191, 16);
            this.ckCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ckCancel.TabIndex = 7;
            this.ckCancel.Text = "不收费：表示确认不做，不收费";
            this.ckCancel.UseVisualStyleBackColor = true;
            // 
            // ckComplete
            // 
            this.ckComplete.AutoSize = true;
            this.ckComplete.ForeColor = System.Drawing.Color.Blue;
            this.ckComplete.Location = new System.Drawing.Point(184, 68);
            this.ckComplete.Name = "ckComplete";
            this.ckComplete.Size = new System.Drawing.Size(191, 16);
            this.ckComplete.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ckComplete.TabIndex = 6;
            this.ckComplete.Text = "收费：表示已经完成，确认收费";
            this.ckComplete.UseVisualStyleBackColor = true;
            // 
            // lbBedName
            // 
            this.lbBedName.AutoSize = true;
            this.lbBedName.Location = new System.Drawing.Point(724, 14);
            this.lbBedName.Name = "lbBedName";
            this.lbBedName.Size = new System.Drawing.Size(41, 12);
            this.lbBedName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbBedName.TabIndex = 5;
            this.lbBedName.Text = "床号：";
            // 
            // lbNurseCellName
            // 
            this.lbNurseCellName.AutoSize = true;
            this.lbNurseCellName.Location = new System.Drawing.Point(554, 14);
            this.lbNurseCellName.Name = "lbNurseCellName";
            this.lbNurseCellName.Size = new System.Drawing.Size(41, 12);
            this.lbNurseCellName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbNurseCellName.TabIndex = 4;
            this.lbNurseCellName.Text = "病区：";
            // 
            // lblFreeCost
            // 
            this.lblFreeCost.AutoSize = true;
            this.lblFreeCost.Location = new System.Drawing.Point(437, 14);
            this.lblFreeCost.Name = "lblFreeCost";
            this.lblFreeCost.Size = new System.Drawing.Size(65, 12);
            this.lblFreeCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblFreeCost.TabIndex = 3;
            this.lblFreeCost.Text = "可用余额：";
            // 
            // lblDept
            // 
            this.lblDept.AutoSize = true;
            this.lblDept.Location = new System.Drawing.Point(306, 14);
            this.lblDept.Name = "lblDept";
            this.lblDept.Size = new System.Drawing.Size(65, 12);
            this.lblDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblDept.TabIndex = 2;
            this.lblDept.Text = "住院科室：";
            // 
            // lblPatientNO
            // 
            this.lblPatientNO.AutoSize = true;
            this.lblPatientNO.Location = new System.Drawing.Point(23, 14);
            this.lblPatientNO.Name = "lblPatientNO";
            this.lblPatientNO.Size = new System.Drawing.Size(53, 12);
            this.lblPatientNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblPatientNO.TabIndex = 1;
            this.lblPatientNO.Text = "住院号：";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(185, 14);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(65, 12);
            this.lblName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblName.TabIndex = 0;
            this.lblName.Text = "患者姓名：";
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point(17, 43);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(41, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 22;
            this.neuLabel4.Text = "科室：";
            // 
            // cmbDept
            // 
            this.cmbDept.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbDept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbDept.Font = new System.Drawing.Font("宋体", 11F);
            this.cmbDept.IsEnter2Tab = false;
            this.cmbDept.IsFlat = false;
            this.cmbDept.IsLike = true;
            this.cmbDept.IsListOnly = false;
            this.cmbDept.IsPopForm = true;
            this.cmbDept.IsShowCustomerList = false;
            this.cmbDept.IsShowID = false;
            this.cmbDept.IsShowIDAndName = false;
            this.cmbDept.Location = new System.Drawing.Point(52, 39);
            this.cmbDept.Name = "cmbDept";
            this.cmbDept.ShowCustomerList = false;
            this.cmbDept.ShowID = false;
            this.cmbDept.Size = new System.Drawing.Size(127, 23);
            this.cmbDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbDept.TabIndex = 23;
            this.cmbDept.Tag = "";
            this.cmbDept.ToolBarUse = false;
            // 
            // ucInpatientConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucInpatientConfirm";
            this.Size = new System.Drawing.Size(918, 474);
            this.Load += new System.EventHandler(this.ucInpatientConfirm_Load);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpItemInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpExecOrder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpExecOrder_Sheet1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tpWebEMR.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.neuPanel2.ResumeLayout(false);
            this.neuPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel3;
        private FS.FrameWork.WinForms.Controls.NeuFpEnter fpExecOrder;
        private FarPoint.Win.Spread.SheetView fpExecOrder_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblPatientNO;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblName;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblFreeCost;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblDept;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbBedName;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbNurseCellName;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton ckCancel;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton ckComplete;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton ckNone;
        public FS.HISFC.Components.Common.Controls.ucQueryInpatientNo ucQueryInpatientNo1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtFeeDetail;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblTotCost;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpItemInfo;
        private System.Windows.Forms.TabPage tpWebEMR;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDept;
    }
}
