namespace FS.SOC.Local.DrugStore.ShenZhen.BinHai.Outpatient
{
    partial class ucDrugList
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
            this.paneltitle = new System.Windows.Forms.Panel();
            this.lbInvoiceNO = new System.Windows.Forms.Label();
            this.lbRecipeNO = new System.Windows.Forms.Label();
            this.lbRePringFlag = new System.Windows.Forms.Label();
            this.lbDiagnose = new System.Windows.Forms.Label();
            this.lbRecipeDoctName = new System.Windows.Forms.Label();
            this.lbRecipeDeptName = new System.Windows.Forms.Label();
            this.lbPatientInfo = new System.Windows.Forms.Label();
            this.lbPageNO = new System.Windows.Forms.Label();
            this.lbTitle = new System.Windows.Forms.Label();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.lbCost = new System.Windows.Forms.Label();
            this.lbWindowName = new System.Windows.Forms.Label();
            this.lbPrintTime = new System.Windows.Forms.Label();
            this.lbSendOperName = new System.Windows.Forms.Label();
            this.lbApprovedOperName = new System.Windows.Forms.Label();
            this.lbDrugedOperName = new System.Windows.Forms.Label();
            this.fpSpread1 = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.paneltitle.SuspendLayout();
            this.panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // paneltitle
            // 
            this.paneltitle.Controls.Add(this.lbInvoiceNO);
            this.paneltitle.Controls.Add(this.lbRecipeNO);
            this.paneltitle.Controls.Add(this.lbRePringFlag);
            this.paneltitle.Controls.Add(this.lbDiagnose);
            this.paneltitle.Controls.Add(this.lbRecipeDoctName);
            this.paneltitle.Controls.Add(this.lbRecipeDeptName);
            this.paneltitle.Controls.Add(this.lbPatientInfo);
            this.paneltitle.Controls.Add(this.lbPageNO);
            this.paneltitle.Controls.Add(this.lbTitle);
            this.paneltitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.paneltitle.Location = new System.Drawing.Point(0, 0);
            this.paneltitle.Name = "paneltitle";
            this.paneltitle.Size = new System.Drawing.Size(800, 94);
            this.paneltitle.TabIndex = 0;
            // 
            // lbInvoiceNO
            // 
            this.lbInvoiceNO.AutoSize = true;
            this.lbInvoiceNO.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbInvoiceNO.Location = new System.Drawing.Point(647, 25);
            this.lbInvoiceNO.Name = "lbInvoiceNO";
            this.lbInvoiceNO.Size = new System.Drawing.Size(52, 14);
            this.lbInvoiceNO.TabIndex = 8;
            this.lbInvoiceNO.Text = "发票：";
            // 
            // lbRecipeNO
            // 
            this.lbRecipeNO.AutoSize = true;
            this.lbRecipeNO.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbRecipeNO.Location = new System.Drawing.Point(15, 25);
            this.lbRecipeNO.Name = "lbRecipeNO";
            this.lbRecipeNO.Size = new System.Drawing.Size(52, 14);
            this.lbRecipeNO.TabIndex = 7;
            this.lbRecipeNO.Text = "方号：";
            // 
            // lbRePringFlag
            // 
            this.lbRePringFlag.AutoSize = true;
            this.lbRePringFlag.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbRePringFlag.Location = new System.Drawing.Point(560, 18);
            this.lbRePringFlag.Name = "lbRePringFlag";
            this.lbRePringFlag.Size = new System.Drawing.Size(37, 14);
            this.lbRePringFlag.TabIndex = 6;
            this.lbRePringFlag.Text = "补打";
            // 
            // lbDiagnose
            // 
            this.lbDiagnose.AutoSize = true;
            this.lbDiagnose.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbDiagnose.Location = new System.Drawing.Point(15, 76);
            this.lbDiagnose.Name = "lbDiagnose";
            this.lbDiagnose.Size = new System.Drawing.Size(67, 14);
            this.lbDiagnose.TabIndex = 5;
            this.lbDiagnose.Text = "诊断：无";
            // 
            // lbRecipeDoctName
            // 
            this.lbRecipeDoctName.AutoSize = true;
            this.lbRecipeDoctName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbRecipeDoctName.Location = new System.Drawing.Point(647, 51);
            this.lbRecipeDoctName.Name = "lbRecipeDoctName";
            this.lbRecipeDoctName.Size = new System.Drawing.Size(97, 14);
            this.lbRecipeDoctName.TabIndex = 4;
            this.lbRecipeDoctName.Text = "医生：刘德华";
            // 
            // lbRecipeDeptName
            // 
            this.lbRecipeDeptName.AutoSize = true;
            this.lbRecipeDeptName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbRecipeDeptName.Location = new System.Drawing.Point(356, 51);
            this.lbRecipeDeptName.Name = "lbRecipeDeptName";
            this.lbRecipeDeptName.Size = new System.Drawing.Size(112, 14);
            this.lbRecipeDeptName.TabIndex = 3;
            this.lbRecipeDeptName.Text = "科室：内科门诊";
            // 
            // lbPatientInfo
            // 
            this.lbPatientInfo.AutoSize = true;
            this.lbPatientInfo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbPatientInfo.Location = new System.Drawing.Point(15, 51);
            this.lbPatientInfo.Name = "lbPatientInfo";
            this.lbPatientInfo.Size = new System.Drawing.Size(284, 14);
            this.lbPatientInfo.TabIndex = 2;
            this.lbPatientInfo.Text = "患者：刘德华 男 20岁6月         自费";
            // 
            // lbPageNO
            // 
            this.lbPageNO.AutoSize = true;
            this.lbPageNO.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbPageNO.Location = new System.Drawing.Point(647, 72);
            this.lbPageNO.Name = "lbPageNO";
            this.lbPageNO.Size = new System.Drawing.Size(52, 14);
            this.lbPageNO.TabIndex = 1;
            this.lbPageNO.Text = "页码：";
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTitle.Location = new System.Drawing.Point(263, 18);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(274, 21);
            this.lbTitle.TabIndex = 0;
            this.lbTitle.Text = "香港大学深圳医院配药清单";
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.lbCost);
            this.panelBottom.Controls.Add(this.lbWindowName);
            this.panelBottom.Controls.Add(this.lbPrintTime);
            this.panelBottom.Controls.Add(this.lbSendOperName);
            this.panelBottom.Controls.Add(this.lbApprovedOperName);
            this.panelBottom.Controls.Add(this.lbDrugedOperName);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 321);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(800, 46);
            this.panelBottom.TabIndex = 2;
            // 
            // lbCost
            // 
            this.lbCost.AutoSize = true;
            this.lbCost.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbCost.Location = new System.Drawing.Point(424, 4);
            this.lbCost.Name = "lbCost";
            this.lbCost.Size = new System.Drawing.Size(44, 12);
            this.lbCost.TabIndex = 7;
            this.lbCost.Text = "总价：";
            // 
            // lbWindowName
            // 
            this.lbWindowName.AutoSize = true;
            this.lbWindowName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbWindowName.Location = new System.Drawing.Point(553, 4);
            this.lbWindowName.Name = "lbWindowName";
            this.lbWindowName.Size = new System.Drawing.Size(44, 12);
            this.lbWindowName.TabIndex = 6;
            this.lbWindowName.Text = "窗口：";
            // 
            // lbPrintTime
            // 
            this.lbPrintTime.AutoSize = true;
            this.lbPrintTime.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbPrintTime.Location = new System.Drawing.Point(648, 3);
            this.lbPrintTime.Name = "lbPrintTime";
            this.lbPrintTime.Size = new System.Drawing.Size(70, 12);
            this.lbPrintTime.TabIndex = 3;
            this.lbPrintTime.Text = "打印时间：";
            // 
            // lbSendOperName
            // 
            this.lbSendOperName.AutoSize = true;
            this.lbSendOperName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbSendOperName.Location = new System.Drawing.Point(285, 4);
            this.lbSendOperName.Name = "lbSendOperName";
            this.lbSendOperName.Size = new System.Drawing.Size(44, 12);
            this.lbSendOperName.TabIndex = 2;
            this.lbSendOperName.Text = "发药：";
            // 
            // lbApprovedOperName
            // 
            this.lbApprovedOperName.AutoSize = true;
            this.lbApprovedOperName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbApprovedOperName.Location = new System.Drawing.Point(146, 4);
            this.lbApprovedOperName.Name = "lbApprovedOperName";
            this.lbApprovedOperName.Size = new System.Drawing.Size(44, 12);
            this.lbApprovedOperName.TabIndex = 1;
            this.lbApprovedOperName.Text = "核对：";
            // 
            // lbDrugedOperName
            // 
            this.lbDrugedOperName.AutoSize = true;
            this.lbDrugedOperName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbDrugedOperName.Location = new System.Drawing.Point(15, 4);
            this.lbDrugedOperName.Name = "lbDrugedOperName";
            this.lbDrugedOperName.Size = new System.Drawing.Size(44, 12);
            this.lbDrugedOperName.TabIndex = 0;
            this.lbDrugedOperName.Text = "配药：";
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "3.0.2004.2005";
            this.fpSpread1.AccessibleDescription = "fpSpread1, Sheet1, Row 0, Column 0, ";
            this.fpSpread1.BackColor = System.Drawing.Color.White;
            this.fpSpread1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.Location = new System.Drawing.Point(0, 94);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1});
            this.fpSpread1.Size = new System.Drawing.Size(800, 227);
            this.fpSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpSpread1.TabIndex = 4;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread1.TextTipAppearance = tipAppearance1;
            this.fpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.ColumnCount = 11;
            this.fpSpread1_Sheet1.RowCount = 9;
            this.fpSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin3", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Black, FarPoint.Win.Spread.GridLines.None, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, false);
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "编码";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "名称";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "组";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "规格";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "频次";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "每次用量";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "天数";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "总量";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "备注";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "货位号";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "组合号";
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.ColumnHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.Black);
            this.fpSpread1_Sheet1.ColumnHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Raised, System.Drawing.Color.Black, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.White);
            this.fpSpread1_Sheet1.Columns.Get(0).Label = "编码";
            this.fpSpread1_Sheet1.Columns.Get(0).Width = 92F;
            this.fpSpread1_Sheet1.Columns.Get(1).Label = "名称";
            this.fpSpread1_Sheet1.Columns.Get(1).Width = 194F;
            this.fpSpread1_Sheet1.Columns.Get(2).Label = "组";
            this.fpSpread1_Sheet1.Columns.Get(2).Width = 27F;
            this.fpSpread1_Sheet1.Columns.Get(3).Label = "规格";
            this.fpSpread1_Sheet1.Columns.Get(3).Width = 122F;
            this.fpSpread1_Sheet1.Columns.Get(10).Label = "组合号";
            this.fpSpread1_Sheet1.Columns.Get(10).Width = 1F;
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.RowHeader.Visible = false;
            this.fpSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.fpSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ucDrugList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.fpSpread1);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.paneltitle);
            this.Name = "ucDrugList";
            this.Size = new System.Drawing.Size(800, 367);
            this.paneltitle.ResumeLayout(false);
            this.paneltitle.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel paneltitle;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Label lbPageNO;
        private System.Windows.Forms.Label lbPrintTime;
        private System.Windows.Forms.Label lbSendOperName;
        private System.Windows.Forms.Label lbApprovedOperName;
        private System.Windows.Forms.Label lbDrugedOperName;
        private FS.SOC.Windows.Forms.FpSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
        private System.Windows.Forms.Label lbPatientInfo;
        private System.Windows.Forms.Label lbDiagnose;
        private System.Windows.Forms.Label lbRecipeDoctName;
        private System.Windows.Forms.Label lbRecipeDeptName;
        private System.Windows.Forms.Label lbWindowName;
        private System.Windows.Forms.Label lbInvoiceNO;
        private System.Windows.Forms.Label lbRecipeNO;
        private System.Windows.Forms.Label lbRePringFlag;
        private System.Windows.Forms.Label lbCost;

    }
   
}
