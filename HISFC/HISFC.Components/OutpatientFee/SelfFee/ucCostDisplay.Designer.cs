﻿namespace FS.HISFC.Components.OutpatientFee.SelfFee
{
    partial class ucCostDisplay
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

            if (disposing)
            {
                if (this.dsItem != null)
                {
                    this.dsItem.Clear();
                    this.dsItem.Dispose();
                    this.dsItem = null;

                    System.GC.Collect();
                }
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
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbItemInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel4 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.tbTotCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel8 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbDrugSendInfo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbReturnCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.tbDrugCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.lbDrugCost = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbRealOwnCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel7 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbPayCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.tbPubCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbOwnCost = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.ucPatientLabel1 = new FS.HISFC.Components.Common.Controls.ucPatientLabel();
            this.neuPanel1.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            this.neuPanel3.SuspendLayout();
            this.neuPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.neuPanel2);
            this.neuPanel1.Controls.Add(this.lbItemInfo);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Padding = new System.Windows.Forms.Padding(2);
            this.neuPanel1.Size = new System.Drawing.Size(804, 45);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // neuPanel2
            // 
            this.neuPanel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.neuPanel2.Controls.Add(this.neuLabel1);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.neuPanel2.Location = new System.Drawing.Point(2, 2);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(79, 41);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 1;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Font = new System.Drawing.Font("宋体", 11.25F);
            this.neuLabel1.Location = new System.Drawing.Point(6, 6);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(75, 15);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "项目信息:";
            // 
            // lbItemInfo
            // 
            this.lbItemInfo.BackColor = System.Drawing.Color.White;
            this.lbItemInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbItemInfo.Location = new System.Drawing.Point(2, 2);
            this.lbItemInfo.Name = "lbItemInfo";
            this.lbItemInfo.Size = new System.Drawing.Size(800, 41);
            this.lbItemInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbItemInfo.TabIndex = 1;
            // 
            // neuPanel3
            // 
            this.neuPanel3.Controls.Add(this.neuPanel4);
            this.neuPanel3.Controls.Add(this.neuSpread1);
            this.neuPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel3.Location = new System.Drawing.Point(0, 45);
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size(804, 182);
            this.neuPanel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 1;
            // 
            // neuPanel4
            // 
            this.neuPanel4.Controls.Add(this.ucPatientLabel1);
            this.neuPanel4.Controls.Add(this.tbTotCost);
            this.neuPanel4.Controls.Add(this.neuLabel8);
            this.neuPanel4.Controls.Add(this.tbDrugSendInfo);
            this.neuPanel4.Controls.Add(this.neuLabel5);
            this.neuPanel4.Controls.Add(this.tbReturnCost);
            this.neuPanel4.Controls.Add(this.tbDrugCost);
            this.neuPanel4.Controls.Add(this.lbDrugCost);
            this.neuPanel4.Controls.Add(this.neuLabel6);
            this.neuPanel4.Controls.Add(this.tbRealOwnCost);
            this.neuPanel4.Controls.Add(this.neuLabel7);
            this.neuPanel4.Controls.Add(this.tbPayCost);
            this.neuPanel4.Controls.Add(this.tbPubCost);
            this.neuPanel4.Controls.Add(this.neuLabel4);
            this.neuPanel4.Controls.Add(this.neuLabel3);
            this.neuPanel4.Controls.Add(this.tbOwnCost);
            this.neuPanel4.Controls.Add(this.neuLabel2);
            this.neuPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel4.Location = new System.Drawing.Point(0, 0);
            this.neuPanel4.Name = "neuPanel4";
            this.neuPanel4.Size = new System.Drawing.Size(804, 142);
            this.neuPanel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel4.TabIndex = 1;
            // 
            // tbTotCost
            // 
            this.tbTotCost.BackColor = System.Drawing.Color.White;
            this.tbTotCost.Enabled = false;
            this.tbTotCost.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
            this.tbTotCost.IsEnter2Tab = false;
            this.tbTotCost.Location = new System.Drawing.Point(397, 90);
            this.tbTotCost.Name = "tbTotCost";
            this.tbTotCost.ReadOnly = true;
            this.tbTotCost.Size = new System.Drawing.Size(86, 29);
            this.tbTotCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbTotCost.TabIndex = 15;
            this.tbTotCost.Text = "0.00";
            this.tbTotCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // neuLabel8
            // 
            this.neuLabel8.AutoSize = true;
            this.neuLabel8.Font = new System.Drawing.Font("宋体", 11.25F);
            this.neuLabel8.Location = new System.Drawing.Point(353, 97);
            this.neuLabel8.Name = "neuLabel8";
            this.neuLabel8.Size = new System.Drawing.Size(45, 15);
            this.neuLabel8.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel8.TabIndex = 14;
            this.neuLabel8.Text = "总计:";
            // 
            // tbDrugSendInfo
            // 
            this.tbDrugSendInfo.BackColor = System.Drawing.Color.White;
            this.tbDrugSendInfo.Enabled = false;
            this.tbDrugSendInfo.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
            this.tbDrugSendInfo.IsEnter2Tab = false;
            this.tbDrugSendInfo.Location = new System.Drawing.Point(78, 91);
            this.tbDrugSendInfo.Name = "tbDrugSendInfo";
            this.tbDrugSendInfo.ReadOnly = true;
            this.tbDrugSendInfo.Size = new System.Drawing.Size(246, 29);
            this.tbDrugSendInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbDrugSendInfo.TabIndex = 13;
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Font = new System.Drawing.Font("宋体", 11.25F);
            this.neuLabel5.Location = new System.Drawing.Point(6, 97);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(75, 15);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 12;
            this.neuLabel5.Text = "发药药房:";
            // 
            // tbReturnCost
            // 
            this.tbReturnCost.BackColor = System.Drawing.Color.White;
            this.tbReturnCost.Enabled = false;
            this.tbReturnCost.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
            this.tbReturnCost.ForeColor = System.Drawing.Color.Red;
            this.tbReturnCost.IsEnter2Tab = false;
            this.tbReturnCost.Location = new System.Drawing.Point(238, 56);
            this.tbReturnCost.Name = "tbReturnCost";
            this.tbReturnCost.ReadOnly = true;
            this.tbReturnCost.Size = new System.Drawing.Size(86, 29);
            this.tbReturnCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbReturnCost.TabIndex = 9;
            this.tbReturnCost.Text = "0.00";
            this.tbReturnCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbDrugCost
            // 
            this.tbDrugCost.BackColor = System.Drawing.Color.White;
            this.tbDrugCost.Enabled = false;
            this.tbDrugCost.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
            this.tbDrugCost.IsEnter2Tab = false;
            this.tbDrugCost.Location = new System.Drawing.Point(397, 57);
            this.tbDrugCost.Name = "tbDrugCost";
            this.tbDrugCost.ReadOnly = true;
            this.tbDrugCost.Size = new System.Drawing.Size(86, 29);
            this.tbDrugCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbDrugCost.TabIndex = 11;
            this.tbDrugCost.Text = "0.00";
            this.tbDrugCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbDrugCost
            // 
            this.lbDrugCost.AutoSize = true;
            this.lbDrugCost.Font = new System.Drawing.Font("宋体", 11.25F);
            this.lbDrugCost.Location = new System.Drawing.Point(323, 64);
            this.lbDrugCost.Name = "lbDrugCost";
            this.lbDrugCost.Size = new System.Drawing.Size(75, 15);
            this.lbDrugCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbDrugCost.TabIndex = 10;
            this.lbDrugCost.Text = "药品总额:";
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.Font = new System.Drawing.Font("宋体", 11.25F);
            this.neuLabel6.Location = new System.Drawing.Point(165, 63);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(75, 15);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 8;
            this.neuLabel6.Text = "找零金额:";
            // 
            // tbRealOwnCost
            // 
            this.tbRealOwnCost.BackColor = System.Drawing.Color.White;
            this.tbRealOwnCost.Enabled = false;
            this.tbRealOwnCost.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
            this.tbRealOwnCost.IsEnter2Tab = false;
            this.tbRealOwnCost.Location = new System.Drawing.Point(78, 56);
            this.tbRealOwnCost.Name = "tbRealOwnCost";
            this.tbRealOwnCost.ReadOnly = true;
            this.tbRealOwnCost.Size = new System.Drawing.Size(86, 29);
            this.tbRealOwnCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbRealOwnCost.TabIndex = 7;
            this.tbRealOwnCost.Text = "0.00";
            this.tbRealOwnCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // neuLabel7
            // 
            this.neuLabel7.AutoSize = true;
            this.neuLabel7.Font = new System.Drawing.Font("宋体", 11.25F);
            this.neuLabel7.Location = new System.Drawing.Point(6, 63);
            this.neuLabel7.Name = "neuLabel7";
            this.neuLabel7.Size = new System.Drawing.Size(75, 15);
            this.neuLabel7.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel7.TabIndex = 6;
            this.neuLabel7.Text = "实收现金:";
            // 
            // tbPayCost
            // 
            this.tbPayCost.BackColor = System.Drawing.Color.White;
            this.tbPayCost.Enabled = false;
            this.tbPayCost.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
            this.tbPayCost.IsEnter2Tab = false;
            this.tbPayCost.Location = new System.Drawing.Point(238, 23);
            this.tbPayCost.Name = "tbPayCost";
            this.tbPayCost.ReadOnly = true;
            this.tbPayCost.Size = new System.Drawing.Size(86, 29);
            this.tbPayCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbPayCost.TabIndex = 3;
            this.tbPayCost.Text = "0.00";
            this.tbPayCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbPubCost
            // 
            this.tbPubCost.BackColor = System.Drawing.Color.White;
            this.tbPubCost.Enabled = false;
            this.tbPubCost.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
            this.tbPubCost.IsEnter2Tab = false;
            this.tbPubCost.Location = new System.Drawing.Point(397, 24);
            this.tbPubCost.Name = "tbPubCost";
            this.tbPubCost.ReadOnly = true;
            this.tbPubCost.Size = new System.Drawing.Size(86, 29);
            this.tbPubCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbPubCost.TabIndex = 5;
            this.tbPubCost.Text = "0.00";
            this.tbPubCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Font = new System.Drawing.Font("宋体", 11.25F);
            this.neuLabel4.Location = new System.Drawing.Point(323, 31);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(75, 15);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 4;
            this.neuLabel4.Text = "记账金额:";
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Font = new System.Drawing.Font("宋体", 11.25F);
            this.neuLabel3.Location = new System.Drawing.Point(165, 30);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(75, 15);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 2;
            this.neuLabel3.Text = "自付金额:";
            // 
            // tbOwnCost
            // 
            this.tbOwnCost.BackColor = System.Drawing.Color.White;
            this.tbOwnCost.Enabled = false;
            this.tbOwnCost.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
            this.tbOwnCost.IsEnter2Tab = false;
            this.tbOwnCost.Location = new System.Drawing.Point(78, 23);
            this.tbOwnCost.Name = "tbOwnCost";
            this.tbOwnCost.ReadOnly = true;
            this.tbOwnCost.Size = new System.Drawing.Size(86, 29);
            this.tbOwnCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbOwnCost.TabIndex = 1;
            this.tbOwnCost.Text = "0.00";
            this.tbOwnCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Font = new System.Drawing.Font("宋体", 11.25F);
            this.neuLabel2.Location = new System.Drawing.Point(6, 30);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(75, 15);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 0;
            this.neuLabel2.Text = "自费金额:";
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, ";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuSpread1.FileName = "";
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(0, 142);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(804, 40);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.Visible = false;
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ColumnCount = 2;
            this.neuSpread1_Sheet1.RowCount = 2;
            this.neuSpread1_Sheet1.RowHeader.ColumnCount = 0;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "ICD";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "诊断名称";
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "ICD";
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 115F;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "诊断名称";
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 345F;
            this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.LightGray, System.Drawing.SystemColors.ControlLightLight, System.Drawing.SystemColors.ControlDark, 0);
            this.neuSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.LightGray, System.Drawing.SystemColors.ControlLightLight, System.Drawing.SystemColors.ControlDark, 0);
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ucPatientLabel1
            // 
            this.ucPatientLabel1.Location = new System.Drawing.Point(498, 23);
            this.ucPatientLabel1.Name = "ucPatientLabel1";
            this.ucPatientLabel1.Size = new System.Drawing.Size(550, 39);
            this.ucPatientLabel1.TabIndex = 16;
            // 
            // ucCostDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuPanel3);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucCostDisplay";
            this.Size = new System.Drawing.Size(804, 227);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel2.ResumeLayout(false);
            this.neuPanel2.PerformLayout();
            this.neuPanel3.ResumeLayout(false);
            this.neuPanel4.ResumeLayout(false);
            this.neuPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbItemInfo;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel3;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel4;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox tbOwnCost;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuTextBox tbReturnCost;
        private FS.FrameWork.WinForms.Controls.NeuTextBox tbDrugCost;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbDrugCost;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        private FS.FrameWork.WinForms.Controls.NeuTextBox tbRealOwnCost;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel7;
        private FS.FrameWork.WinForms.Controls.NeuTextBox tbPubCost;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuTextBox tbDrugSendInfo;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.NeuTextBox tbTotCost;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel8;
        private FS.FrameWork.WinForms.Controls.NeuTextBox tbPayCost;
        private FS.HISFC.Components.Common.Controls.ucPatientLabel ucPatientLabel1;
    }
}
