namespace InterfaceInstanceDefault.IFeeOweMessage
{
    partial class frmGreenSwitch
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
            FS.FrameWork.WinForms.Controls.NeuLabel lblPact;
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            this.neuGroupBox3 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuGroupBox2 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.lbCurr = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuGroupBox4 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.lblInDept = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtAlarm = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblAlarm = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtLeft = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblLeft = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtPact = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtInDept = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtInTime = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lblInTime = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.ucQueryPatientInfo = new FS.HISFC.Components.Common.Controls.ucQueryInpatientNo();
            this.btnClose = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnOpen = new FS.FrameWork.WinForms.Controls.NeuButton();
            lblPact = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.neuGroupBox2.SuspendLayout();
            this.neuGroupBox4.SuspendLayout();
            this.neuPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPact
            // 
            lblPact.AutoSize = true;
            lblPact.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            lblPact.Location = new System.Drawing.Point(432, 24);
            lblPact.Name = "lblPact";
            lblPact.Size = new System.Drawing.Size(59, 13);
            lblPact.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            lblPact.TabIndex = 20;
            lblPact.Text = "合同单位";
            // 
            // neuGroupBox3
            // 
            this.neuGroupBox3.Controls.Add(this.neuSpread1);
            this.neuGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuGroupBox3.Location = new System.Drawing.Point(3, 142);
            this.neuGroupBox3.Name = "neuGroupBox3";
            this.neuGroupBox3.Size = new System.Drawing.Size(687, 409);
            this.neuGroupBox3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox3.TabIndex = 15;
            this.neuGroupBox3.TabStop = false;
            this.neuGroupBox3.Text = "流水记录";
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, ";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(3, 17);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(681, 389);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 1;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ColumnCount = 4;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "操作时间";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "操作人编码";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "操作人姓名";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "办理/终止";
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "操作时间";
            this.neuSpread1_Sheet1.Columns.Get(0).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 192F;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "操作人编码";
            this.neuSpread1_Sheet1.Columns.Get(1).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 149F;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "操作人姓名";
            this.neuSpread1_Sheet1.Columns.Get(2).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 114F;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "办理/终止";
            this.neuSpread1_Sheet1.Columns.Get(3).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 113F;
            this.neuSpread1_Sheet1.DataAutoCellTypes = false;
            this.neuSpread1_Sheet1.DataAutoHeadings = false;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuGroupBox2
            // 
            this.neuGroupBox2.Controls.Add(this.neuGroupBox3);
            this.neuGroupBox2.Controls.Add(this.lbCurr);
            this.neuGroupBox2.Controls.Add(this.neuGroupBox4);
            this.neuGroupBox2.Controls.Add(this.neuPanel1);
            this.neuGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuGroupBox2.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox2.Name = "neuGroupBox2";
            this.neuGroupBox2.Size = new System.Drawing.Size(693, 554);
            this.neuGroupBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox2.TabIndex = 15;
            this.neuGroupBox2.TabStop = false;
            this.neuGroupBox2.Text = "绿色通道";
            // 
            // lbCurr
            // 
            this.lbCurr.AutoSize = true;
            this.lbCurr.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbCurr.Location = new System.Drawing.Point(226, 30);
            this.lbCurr.Name = "lbCurr";
            this.lbCurr.Size = new System.Drawing.Size(0, 13);
            this.lbCurr.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbCurr.TabIndex = 19;
            // 
            // neuGroupBox4
            // 
            this.neuGroupBox4.Controls.Add(this.lblInDept);
            this.neuGroupBox4.Controls.Add(this.txtName);
            this.neuGroupBox4.Controls.Add(this.txtAlarm);
            this.neuGroupBox4.Controls.Add(this.lblAlarm);
            this.neuGroupBox4.Controls.Add(this.txtLeft);
            this.neuGroupBox4.Controls.Add(this.lblLeft);
            this.neuGroupBox4.Controls.Add(this.txtPact);
            this.neuGroupBox4.Controls.Add(lblPact);
            this.neuGroupBox4.Controls.Add(this.txtInDept);
            this.neuGroupBox4.Controls.Add(this.txtInTime);
            this.neuGroupBox4.Controls.Add(this.lblInTime);
            this.neuGroupBox4.Controls.Add(this.lbName);
            this.neuGroupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox4.Location = new System.Drawing.Point(3, 60);
            this.neuGroupBox4.Name = "neuGroupBox4";
            this.neuGroupBox4.Size = new System.Drawing.Size(687, 82);
            this.neuGroupBox4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox4.TabIndex = 16;
            this.neuGroupBox4.TabStop = false;
            this.neuGroupBox4.Text = "患者信息";
            // 
            // lblInDept
            // 
            this.lblInDept.AutoSize = true;
            this.lblInDept.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInDept.Location = new System.Drawing.Point(8, 51);
            this.lblInDept.Name = "lblInDept";
            this.lblInDept.Size = new System.Drawing.Size(59, 13);
            this.lblInDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblInDept.TabIndex = 29;
            this.lblInDept.Text = "住院科室";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.White;
            this.txtName.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtName.IsEnter2Tab = false;
            this.txtName.Location = new System.Drawing.Point(74, 19);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(137, 22);
            this.txtName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtName.TabIndex = 17;
            this.txtName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtAlarm
            // 
            this.txtAlarm.BackColor = System.Drawing.Color.White;
            this.txtAlarm.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAlarm.IsEnter2Tab = false;
            this.txtAlarm.Location = new System.Drawing.Point(288, 46);
            this.txtAlarm.Name = "txtAlarm";
            this.txtAlarm.ReadOnly = true;
            this.txtAlarm.Size = new System.Drawing.Size(137, 22);
            this.txtAlarm.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtAlarm.TabIndex = 26;
            this.txtAlarm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblAlarm
            // 
            this.lblAlarm.AutoSize = true;
            this.lblAlarm.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAlarm.Location = new System.Drawing.Point(236, 51);
            this.lblAlarm.Name = "lblAlarm";
            this.lblAlarm.Size = new System.Drawing.Size(46, 13);
            this.lblAlarm.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblAlarm.TabIndex = 25;
            this.lblAlarm.Text = "警戒线";
            // 
            // txtLeft
            // 
            this.txtLeft.BackColor = System.Drawing.Color.White;
            this.txtLeft.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLeft.IsEnter2Tab = false;
            this.txtLeft.Location = new System.Drawing.Point(497, 46);
            this.txtLeft.Name = "txtLeft";
            this.txtLeft.ReadOnly = true;
            this.txtLeft.Size = new System.Drawing.Size(137, 22);
            this.txtLeft.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtLeft.TabIndex = 28;
            this.txtLeft.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblLeft
            // 
            this.lblLeft.AutoSize = true;
            this.lblLeft.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLeft.Location = new System.Drawing.Point(458, 51);
            this.lblLeft.Name = "lblLeft";
            this.lblLeft.Size = new System.Drawing.Size(33, 13);
            this.lblLeft.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblLeft.TabIndex = 27;
            this.lblLeft.Text = "余额";
            // 
            // txtPact
            // 
            this.txtPact.BackColor = System.Drawing.Color.White;
            this.txtPact.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPact.IsEnter2Tab = false;
            this.txtPact.Location = new System.Drawing.Point(497, 19);
            this.txtPact.Name = "txtPact";
            this.txtPact.ReadOnly = true;
            this.txtPact.Size = new System.Drawing.Size(137, 22);
            this.txtPact.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtPact.TabIndex = 21;
            this.txtPact.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtInDept
            // 
            this.txtInDept.BackColor = System.Drawing.Color.White;
            this.txtInDept.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtInDept.IsEnter2Tab = false;
            this.txtInDept.Location = new System.Drawing.Point(74, 46);
            this.txtInDept.Name = "txtInDept";
            this.txtInDept.ReadOnly = true;
            this.txtInDept.Size = new System.Drawing.Size(137, 22);
            this.txtInDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtInDept.TabIndex = 22;
            this.txtInDept.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtInTime
            // 
            this.txtInTime.BackColor = System.Drawing.Color.White;
            this.txtInTime.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtInTime.IsEnter2Tab = false;
            this.txtInTime.Location = new System.Drawing.Point(288, 19);
            this.txtInTime.Name = "txtInTime";
            this.txtInTime.ReadOnly = true;
            this.txtInTime.Size = new System.Drawing.Size(137, 22);
            this.txtInTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtInTime.TabIndex = 19;
            this.txtInTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblInTime
            // 
            this.lblInTime.AutoSize = true;
            this.lblInTime.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInTime.Location = new System.Drawing.Point(223, 24);
            this.lblInTime.Name = "lblInTime";
            this.lblInTime.Size = new System.Drawing.Size(59, 13);
            this.lblInTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblInTime.TabIndex = 18;
            this.lblInTime.Text = "入院时间";
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbName.Location = new System.Drawing.Point(34, 24);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(33, 13);
            this.lbName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbName.TabIndex = 16;
            this.lbName.Text = "姓名";
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.ucQueryPatientInfo);
            this.neuPanel1.Controls.Add(this.btnClose);
            this.neuPanel1.Controls.Add(this.btnOpen);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(3, 17);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(687, 43);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 20;
            // 
            // ucQueryPatientInfo
            // 
            this.ucQueryPatientInfo.InputType = 0;
            this.ucQueryPatientInfo.Location = new System.Drawing.Point(10, 4);
            this.ucQueryPatientInfo.Name = "ucQueryPatientInfo";
            this.ucQueryPatientInfo.ShowState = FS.HISFC.Components.Common.Controls.enuShowState.AfterArrived;
            this.ucQueryPatientInfo.Size = new System.Drawing.Size(198, 27);
            this.ucQueryPatientInfo.TabIndex = 15;
            this.ucQueryPatientInfo.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(this.ucQueryPatientInfo_myEvent);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(575, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnClose.TabIndex = 18;
            this.btnClose.Text = "终止";
            this.btnClose.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpen.Location = new System.Drawing.Point(480, 6);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnOpen.TabIndex = 17;
            this.btnOpen.Text = "办理";
            this.btnOpen.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // frmGreenSwitch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 554);
            this.Controls.Add(this.neuGroupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmGreenSwitch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.neuGroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.neuGroupBox2.ResumeLayout(false);
            this.neuGroupBox2.PerformLayout();
            this.neuGroupBox4.ResumeLayout(false);
            this.neuGroupBox4.PerformLayout();
            this.neuPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox2;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox3;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox4;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtName;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtAlarm;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblAlarm;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtLeft;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblLeft;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtPact;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtInDept;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtInTime;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblInTime;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbName;
        private FS.HISFC.Components.Common.Controls.ucQueryInpatientNo ucQueryPatientInfo;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblInDept;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbCurr;
        private FS.FrameWork.WinForms.Controls.NeuButton btnClose;
        private FS.FrameWork.WinForms.Controls.NeuButton btnOpen;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
    }
}
