﻿namespace FS.SOC.Local.InpatientFee.GuangZhou
{
    partial class ucFinIpbPatientDayFeeByStatGZA5
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
            this.neuPlAll = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPlFp = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPlLeft = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuTvLeft = new FS.FrameWork.WinForms.Controls.NeuTreeView();
            this.neuPlTop = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuDateTime = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuPlAll.SuspendLayout();
            this.neuPlFp.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.neuPlLeft.SuspendLayout();
            this.neuPlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPlAll
            // 
            this.neuPlAll.Controls.Add(this.neuPlFp);
            this.neuPlAll.Controls.Add(this.neuPlLeft);
            this.neuPlAll.Controls.Add(this.neuPlTop);
            this.neuPlAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPlAll.Location = new System.Drawing.Point(0, 0);
            this.neuPlAll.Name = "neuPlAll";
            this.neuPlAll.Size = new System.Drawing.Size(865, 523);
            this.neuPlAll.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPlAll.TabIndex = 0;
            // 
            // neuPlFp
            // 
            this.neuPlFp.Controls.Add(this.panel1);
            this.neuPlFp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPlFp.Location = new System.Drawing.Point(225, 36);
            this.neuPlFp.Name = "neuPlFp";
            this.neuPlFp.Size = new System.Drawing.Size(640, 487);
            this.neuPlFp.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPlFp.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.neuSpread1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(640, 487);
            this.panel1.TabIndex = 2;
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, ";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(0, 0);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(640, 487);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 2;
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
            this.neuSpread1_Sheet1.RowCount = 1;
            this.neuSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.SystemColors.Control, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.None, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 140F;
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 140F;
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 140F;
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 140F;
            this.neuSpread1_Sheet1.DefaultStyle.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.Rows.Default.Height = 30F;
            this.neuSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuPlLeft
            // 
            this.neuPlLeft.Controls.Add(this.neuTvLeft);
            this.neuPlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.neuPlLeft.Location = new System.Drawing.Point(0, 36);
            this.neuPlLeft.Name = "neuPlLeft";
            this.neuPlLeft.Size = new System.Drawing.Size(225, 487);
            this.neuPlLeft.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPlLeft.TabIndex = 1;
            // 
            // neuTvLeft
            // 
            this.neuTvLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuTvLeft.HideSelection = false;
            this.neuTvLeft.Location = new System.Drawing.Point(0, 0);
            this.neuTvLeft.Name = "neuTvLeft";
            this.neuTvLeft.Size = new System.Drawing.Size(225, 487);
            this.neuTvLeft.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTvLeft.TabIndex = 0;
            this.neuTvLeft.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.neuTvLeft_AfterCheck);
            this.neuTvLeft.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.neuTvLeft_AfterSelect);
            // 
            // neuPlTop
            // 
            this.neuPlTop.Controls.Add(this.neuLabel1);
            this.neuPlTop.Controls.Add(this.neuDateTime);
            this.neuPlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPlTop.Location = new System.Drawing.Point(0, 0);
            this.neuPlTop.Name = "neuPlTop";
            this.neuPlTop.Size = new System.Drawing.Size(865, 36);
            this.neuPlTop.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPlTop.TabIndex = 0;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(14, 13);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(41, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 1;
            this.neuLabel1.Text = "日期：";
            // 
            // neuDateTime
            // 
            this.neuDateTime.CustomFormat = "yyyy-MM-dd";
            this.neuDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.neuDateTime.IsEnter2Tab = false;
            this.neuDateTime.Location = new System.Drawing.Point(61, 9);
            this.neuDateTime.Name = "neuDateTime";
            this.neuDateTime.Size = new System.Drawing.Size(102, 21);
            this.neuDateTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuDateTime.TabIndex = 0;
            // 
            // ucFinIpbPatientDayFeeByStatGZA5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.neuPlAll);
            this.Name = "ucFinIpbPatientDayFeeByStatGZA5";
            this.Size = new System.Drawing.Size(865, 523);
            this.neuPlAll.ResumeLayout(false);
            this.neuPlFp.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.neuPlLeft.ResumeLayout(false);
            this.neuPlTop.ResumeLayout(false);
            this.neuPlTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPlAll;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPlFp;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPlLeft;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPlTop;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker neuDateTime;
        private FS.FrameWork.WinForms.Controls.NeuTreeView neuTvLeft;
        private System.Windows.Forms.Panel panel1;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
    }
}
