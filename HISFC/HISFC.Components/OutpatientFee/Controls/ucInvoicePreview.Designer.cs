﻿namespace FS.HISFC.Components.OutpatientFee.Controls
{
    partial class ucInvoicePreview
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
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.tbInvoiceNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.tbRealInvoiceNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.btnUpdate = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lbHosName = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lblTotCost = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.fpSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel1.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.neuPanel1.Controls.Add(this.tbInvoiceNO);
            this.neuPanel1.Controls.Add(this.tbRealInvoiceNO);
            this.neuPanel1.Controls.Add(this.btnUpdate);
            this.neuPanel1.Controls.Add(this.neuLabel1);
            this.neuPanel1.Controls.Add(this.lblName);
            this.neuPanel1.Controls.Add(this.lbHosName);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(3, 3);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(544, 38);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // tbInvoiceNO
            // 
            this.tbInvoiceNO.BackColor = System.Drawing.Color.White;
            this.tbInvoiceNO.Enabled = false;
            this.tbInvoiceNO.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbInvoiceNO.IsEnter2Tab = false;
            this.tbInvoiceNO.Location = new System.Drawing.Point(182, 10);
            this.tbInvoiceNO.MaxLength = 12;
            this.tbInvoiceNO.Name = "tbInvoiceNO";
            this.tbInvoiceNO.Size = new System.Drawing.Size(100, 22);
            this.tbInvoiceNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbInvoiceNO.TabIndex = 8;
            // 
            // tbRealInvoiceNO
            // 
            this.tbRealInvoiceNO.BackColor = System.Drawing.Color.White;
            this.tbRealInvoiceNO.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRealInvoiceNO.IsEnter2Tab = false;
            this.tbRealInvoiceNO.Location = new System.Drawing.Point(354, 10);
            this.tbRealInvoiceNO.MaxLength = 10;
            this.tbRealInvoiceNO.Name = "tbRealInvoiceNO";
            this.tbRealInvoiceNO.Size = new System.Drawing.Size(100, 22);
            this.tbRealInvoiceNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbRealInvoiceNO.TabIndex = 5;
            this.tbRealInvoiceNO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbInvoiceNo_KeyDown);
            // 
            // btnUpdate
            // 
            this.btnUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUpdate.Location = new System.Drawing.Point(459, 9);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnUpdate.TabIndex = 4;
            this.btnUpdate.Text = "更新(&U)";
            this.btnUpdate.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Font = new System.Drawing.Font("宋体", 10F);
            this.neuLabel1.Location = new System.Drawing.Point(300, 15);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(56, 14);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 2;
            this.neuLabel1.Text = "印刷号:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("宋体", 10F);
            this.lblName.Location = new System.Drawing.Point(129, 15);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(56, 14);
            this.lblName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblName.TabIndex = 1;
            this.lblName.Text = "发票号:";
            // 
            // lbHosName
            // 
            this.lbHosName.AutoSize = true;
            this.lbHosName.Font = new System.Drawing.Font("宋体", 12F);
            this.lbHosName.Location = new System.Drawing.Point(6, 13);
            this.lbHosName.Name = "lbHosName";
            this.lbHosName.Size = new System.Drawing.Size(104, 16);
            this.lbHosName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbHosName.TabIndex = 0;
            this.lbHosName.Text = "门诊收据预览";
            // 
            // neuPanel2
            // 
            this.neuPanel2.BackColor = System.Drawing.SystemColors.Control;
            this.neuPanel2.Controls.Add(this.lblTotCost);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuPanel2.Location = new System.Drawing.Point(3, 205);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(544, 35);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 1;
            // 
            // lblTotCost
            // 
            this.lblTotCost.BackColor = System.Drawing.Color.White;
            this.lblTotCost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotCost.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTotCost.Location = new System.Drawing.Point(0, 0);
            this.lblTotCost.Margin = new System.Windows.Forms.Padding(3);
            this.lblTotCost.Name = "lblTotCost";
            this.lblTotCost.Size = new System.Drawing.Size(544, 35);
            this.lblTotCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblTotCost.TabIndex = 0;
            this.lblTotCost.Text = "          ";
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "3.0.2004.2005";
            this.fpSpread1.AccessibleDescription = "fpSpread1, Sheet1, Row 0, Column 0, ";
            this.fpSpread1.BackColor = System.Drawing.SystemColors.Control;
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread1.FileName = "";
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.IsAutoSaveGridStatus = false;
            this.fpSpread1.IsCanCustomConfigColumn = false;
            this.fpSpread1.Location = new System.Drawing.Point(3, 41);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1});
            this.fpSpread1.Size = new System.Drawing.Size(544, 164);
            this.fpSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpSpread1.TabIndex = 2;
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
            this.fpSpread1_Sheet1.ColumnCount = 6;
            this.fpSpread1_Sheet1.ColumnHeader.RowCount = 0;
            this.fpSpread1_Sheet1.RowCount = 8;
            this.fpSpread1_Sheet1.RowHeader.ColumnCount = 0;
            this.fpSpread1_Sheet1.Columns.Get(0).Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold);
            this.fpSpread1_Sheet1.Columns.Get(0).Width = 90F;
            this.fpSpread1_Sheet1.Columns.Get(1).Font = new System.Drawing.Font("Arial", 9.75F);
            this.fpSpread1_Sheet1.Columns.Get(1).Width = 70F;
            this.fpSpread1_Sheet1.Columns.Get(2).Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold);
            this.fpSpread1_Sheet1.Columns.Get(2).Width = 90F;
            this.fpSpread1_Sheet1.Columns.Get(3).Font = new System.Drawing.Font("Arial", 9.75F);
            this.fpSpread1_Sheet1.Columns.Get(3).Width = 70F;
            this.fpSpread1_Sheet1.Columns.Get(4).Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold);
            this.fpSpread1_Sheet1.Columns.Get(4).Width = 90F;
            this.fpSpread1_Sheet1.Columns.Get(5).Font = new System.Drawing.Font("Arial", 9.75F);
            this.fpSpread1_Sheet1.Columns.Get(5).Width = 70F;
            this.fpSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.LightGray, System.Drawing.SystemColors.ControlLightLight, System.Drawing.SystemColors.ControlDark, 0);
            this.fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpSpread1_Sheet1.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.LightGray, System.Drawing.SystemColors.ControlLightLight, System.Drawing.SystemColors.ControlDark, 0);
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ucInvoicePreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.fpSpread1);
            this.Controls.Add(this.neuPanel2);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucInvoicePreview";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Size = new System.Drawing.Size(550, 243);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.neuPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblName;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbHosName;
        private FS.FrameWork.WinForms.Controls.NeuButton btnUpdate;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblTotCost;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox tbRealInvoiceNO;
        private FS.FrameWork.WinForms.Controls.NeuTextBox tbInvoiceNO;
    }
}
