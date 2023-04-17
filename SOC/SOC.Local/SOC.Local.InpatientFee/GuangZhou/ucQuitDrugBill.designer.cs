namespace FS.SOC.Local.InpatientFee.GuangZhou
{
    partial class ucQuitDrugBill
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
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labReason = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.labRemark = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.labArea = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbCardNo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbDeptName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbSex = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.labPrintDate = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(674, 308);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 104);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(674, 204);
            this.panel3.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.neuSpread1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(674, 204);
            this.panel5.TabIndex = 3;
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
            this.neuSpread1.Location = new System.Drawing.Point(0, 0);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(674, 204);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 1;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance2;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ColumnCount = 5;
            this.neuSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "药品名称";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "规格";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "退药数量";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "单位";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "金额";
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.Columns.Get(0).BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "药品名称";
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 190F;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "规格";
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 139F;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "退药数量";
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 65F;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "单位";
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 57F;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "金额";
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 109F;
            this.neuSpread1_Sheet1.GroupBarBackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.neuSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.labPrintDate);
            this.panel2.Controls.Add(this.labReason);
            this.panel2.Controls.Add(this.labRemark);
            this.panel2.Controls.Add(this.labArea);
            this.panel2.Controls.Add(this.lbCardNo);
            this.panel2.Controls.Add(this.lbDeptName);
            this.panel2.Controls.Add(this.lbSex);
            this.panel2.Controls.Add(this.lbName);
            this.panel2.Controls.Add(this.neuPanName);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(674, 104);
            this.panel2.TabIndex = 0;
            // 
            // labReason
            // 
            this.labReason.AutoSize = true;
            this.labReason.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labReason.Location = new System.Drawing.Point(379, 34);
            this.labReason.Name = "labReason";
            this.labReason.Size = new System.Drawing.Size(77, 14);
            this.labReason.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.labReason.TabIndex = 20;
            this.labReason.Text = "退费原因 :";
            // 
            // labRemark
            // 
            this.labRemark.AutoSize = true;
            this.labRemark.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labRemark.Location = new System.Drawing.Point(3, 84);
            this.labRemark.Name = "labRemark";
            this.labRemark.Size = new System.Drawing.Size(567, 14);
            this.labRemark.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.labRemark.TabIndex = 19;
            this.labRemark.Text = "注：护长签名并盖章，备注为送药房的项目须药房签名盖章，检验、检查项目须拿回原验单";
            // 
            // labArea
            // 
            this.labArea.AutoSize = true;
            this.labArea.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labArea.Location = new System.Drawing.Point(3, 59);
            this.labArea.Name = "labArea";
            this.labArea.Size = new System.Drawing.Size(49, 14);
            this.labArea.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.labArea.TabIndex = 18;
            this.labArea.Text = "病区 :";
            // 
            // lbCardNo
            // 
            this.lbCardNo.AutoSize = true;
            this.lbCardNo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbCardNo.Location = new System.Drawing.Point(138, 34);
            this.lbCardNo.Name = "lbCardNo";
            this.lbCardNo.Size = new System.Drawing.Size(68, 14);
            this.lbCardNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbCardNo.TabIndex = 17;
            this.lbCardNo.Text = "住院号 :";
            // 
            // lbDeptName
            // 
            this.lbDeptName.AutoSize = true;
            this.lbDeptName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbDeptName.Location = new System.Drawing.Point(150, 59);
            this.lbDeptName.Name = "lbDeptName";
            this.lbDeptName.Size = new System.Drawing.Size(77, 14);
            this.lbDeptName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbDeptName.TabIndex = 14;
            this.lbDeptName.Text = "科室名称 :";
            // 
            // lbSex
            // 
            this.lbSex.AutoSize = true;
            this.lbSex.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbSex.Location = new System.Drawing.Point(299, 34);
            this.lbSex.Name = "lbSex";
            this.lbSex.Size = new System.Drawing.Size(49, 14);
            this.lbSex.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbSex.TabIndex = 12;
            this.lbSex.Text = "性别 :";
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbName.Location = new System.Drawing.Point(3, 34);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(53, 14);
            this.lbName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbName.TabIndex = 11;
            this.lbName.Text = "姓名 :";
            // 
            // neuPanName
            // 
            this.neuPanName.AutoSize = true;
            this.neuPanName.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuPanName.Location = new System.Drawing.Point(203, 0);
            this.neuPanName.Name = "neuPanName";
            this.neuPanName.Size = new System.Drawing.Size(185, 24);
            this.neuPanName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanName.TabIndex = 1;
            this.neuPanName.Text = "住院退费申请单";
            // 
            // labPrintDate
            // 
            this.labPrintDate.AutoSize = true;
            this.labPrintDate.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labPrintDate.Location = new System.Drawing.Point(379, 59);
            this.labPrintDate.Name = "labPrintDate";
            this.labPrintDate.Size = new System.Drawing.Size(77, 14);
            this.labPrintDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.labPrintDate.TabIndex = 21;
            this.labPrintDate.Text = "打印日期 :";
            // 
            // ucQuitDrugBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Name = "ucQuitDrugBill";
            this.Size = new System.Drawing.Size(674, 308);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbCardNo;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbDeptName;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbSex;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbName;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuPanName;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private System.Windows.Forms.Panel panel5;
        private FS.FrameWork.WinForms.Controls.NeuLabel labReason;
        private FS.FrameWork.WinForms.Controls.NeuLabel labRemark;
        private FS.FrameWork.WinForms.Controls.NeuLabel labArea;
        private FS.FrameWork.WinForms.Controls.NeuLabel labPrintDate;
    }
}
