﻿namespace FS.HISFC.Components.HealthRecord.Case
{
    partial class ucCaseChangeConfirm
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
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuGroupBox2 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.btnOK = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.tbCancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbCard = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuGroupBox1.SuspendLayout();
            this.neuGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.tbCard);
            this.neuGroupBox1.Controls.Add(this.neuLabel1);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(832, 67);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 0;
            this.neuGroupBox1.TabStop = false;
            // 
            // neuGroupBox2
            // 
            this.neuGroupBox2.Controls.Add(this.neuSpread1);
            this.neuGroupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox2.Location = new System.Drawing.Point(0, 67);
            this.neuGroupBox2.Name = "neuGroupBox2";
            this.neuGroupBox2.Size = new System.Drawing.Size(832, 298);
            this.neuGroupBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox2.TabIndex = 1;
            this.neuGroupBox2.TabStop = false;
            this.neuGroupBox2.Text = "申请记录";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(334, 382);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(82, 23);
            this.btnOK.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确  定";
            this.btnOK.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tbCancel
            // 
            this.tbCancel.Location = new System.Drawing.Point(476, 382);
            this.tbCancel.Name = "tbCancel";
            this.tbCancel.Size = new System.Drawing.Size(82, 23);
            this.tbCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbCancel.TabIndex = 3;
            this.tbCancel.Text = "取  消";
            this.tbCancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.tbCancel.UseVisualStyleBackColor = true;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(89, 33);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(65, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "原始病历号";
            // 
            // tbCard
            // 
            this.tbCard.Location = new System.Drawing.Point(160, 29);
            this.tbCard.Name = "tbCard";
            this.tbCard.Size = new System.Drawing.Size(207, 21);
            this.tbCard.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbCard.TabIndex = 0;
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "2.5.2007.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, ";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(3, 17);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(826, 278);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 0;
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
            this.neuSpread1_Sheet1.ColumnCount = 7;
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "ID";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "旧病历号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "新病历号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "医生病码";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "医生姓名";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "申请时间";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "申请说明";
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "旧病历号";
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 113F;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "新病历号";
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 113F;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "申请时间";
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 118F;
            this.neuSpread1_Sheet1.Columns.Get(6).Label = "申请说明";
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 227F;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ucCaseChangeConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.neuGroupBox2);
            this.Controls.Add(this.neuGroupBox1);
            this.Name = "ucCaseChangeConfirm";
            this.Size = new System.Drawing.Size(832, 414);
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.neuGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox tbCard;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox2;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuButton btnOK;
        private FS.FrameWork.WinForms.Controls.NeuButton tbCancel;
    }
}
