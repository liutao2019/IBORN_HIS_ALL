﻿namespace FS.HISFC.Components.Material.Report
{
    partial class frmSetConfig
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。

        /// </summary>
        private void InitializeComponent()
        {
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType2 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.ComboBoxCellType comboBoxCellType2 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuGroupBox2 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.txtPath = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ckContinue = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.tpFpSet = new System.Windows.Forms.TabPage();
            this.btnClear = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnCancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnOK = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.tpConfig = new System.Windows.Forms.TabPage();
            this.txtSql = new FS.FrameWork.WinForms.Controls.RichTextBox();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ckSqlXml = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.txtFpPathName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtSqlIndex = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtReportID = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuTabControl1 = new FS.FrameWork.WinForms.Controls.NeuTabControl();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.neuGroupBox2.SuspendLayout();
            this.tpFpSet.SuspendLayout();
            this.tpConfig.SuspendLayout();
            this.neuTabControl1.SuspendLayout();
            this.neuGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "2.5.2007.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, ";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.EditModePermanent = true;
            this.neuSpread1.EditModeReplace = true;
            this.neuSpread1.FileName = "";
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(3, 42);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(677, 274);
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
            this.neuSpread1_Sheet1.ColumnCount = 5;
            this.neuSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin3", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "列标题";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "是否显示";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "列宽度";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "列格式";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "列参数";
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "列标题";
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 146F;
            this.neuSpread1_Sheet1.Columns.Get(1).CellType = checkBoxCellType2;
            this.neuSpread1_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "是否显示";
            this.neuSpread1_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 62F;
            numberCellType2.DecimalPlaces = 0;
            this.neuSpread1_Sheet1.Columns.Get(2).CellType = numberCellType2;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "列宽度";
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 78F;
            comboBoxCellType2.ButtonAlign = FarPoint.Win.ButtonAlign.Right;
            comboBoxCellType2.Items = new string[] {
        "文本",
        "数值",
        "日期"};
            this.neuSpread1_Sheet1.Columns.Get(3).CellType = comboBoxCellType2;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "列格式";
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 77F;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "列参数";
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 93F;
            this.neuSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.SheetCornerStyle.Locked = false;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuGroupBox2
            // 
            this.neuGroupBox2.Controls.Add(this.txtPath);
            this.neuGroupBox2.Controls.Add(this.neuLabel6);
            this.neuGroupBox2.Controls.Add(this.neuLabel5);
            this.neuGroupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox2.Location = new System.Drawing.Point(3, 3);
            this.neuGroupBox2.Name = "neuGroupBox2";
            this.neuGroupBox2.Size = new System.Drawing.Size(677, 39);
            this.neuGroupBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox2.TabIndex = 1;
            this.neuGroupBox2.TabStop = false;
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(71, 13);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(139, 21);
            this.txtPath.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtPath.TabIndex = 1;
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.Location = new System.Drawing.Point(233, 17);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(95, 12);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 0;
            this.neuLabel6.Text = "XML格式配置文件";
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Location = new System.Drawing.Point(6, 17);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(53, 12);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 0;
            this.neuLabel5.Text = "文件名称";
            // 
            // ckContinue
            // 
            this.ckContinue.AutoSize = true;
            this.ckContinue.Location = new System.Drawing.Point(242, 20);
            this.ckContinue.Name = "ckContinue";
            this.ckContinue.Size = new System.Drawing.Size(72, 16);
            this.ckContinue.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ckContinue.TabIndex = 1;
            this.ckContinue.Text = "继续录入";
            this.ckContinue.UseVisualStyleBackColor = true;
            // 
            // tpFpSet
            // 
            this.tpFpSet.Controls.Add(this.neuSpread1);
            this.tpFpSet.Controls.Add(this.neuGroupBox2);
            this.tpFpSet.Location = new System.Drawing.Point(4, 21);
            this.tpFpSet.Name = "tpFpSet";
            this.tpFpSet.Padding = new System.Windows.Forms.Padding(3);
            this.tpFpSet.Size = new System.Drawing.Size(683, 319);
            this.tpFpSet.TabIndex = 1;
            this.tpFpSet.Text = "Fp格式";
            this.tpFpSet.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClear.Location = new System.Drawing.Point(392, 16);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnClear.TabIndex = 0;
            this.btnClear.Text = "清   空";
            this.btnClear.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(113, 16);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取  消";
            this.btnCancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(12, 16);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确  定";
            this.btnOK.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // tpConfig
            // 
            this.tpConfig.Controls.Add(this.txtSql);
            this.tpConfig.Controls.Add(this.neuLabel3);
            this.tpConfig.Controls.Add(this.ckSqlXml);
            this.tpConfig.Controls.Add(this.txtFpPathName);
            this.tpConfig.Controls.Add(this.txtSqlIndex);
            this.tpConfig.Controls.Add(this.txtReportID);
            this.tpConfig.Controls.Add(this.neuLabel4);
            this.tpConfig.Controls.Add(this.neuLabel2);
            this.tpConfig.Controls.Add(this.neuLabel1);
            this.tpConfig.Location = new System.Drawing.Point(4, 21);
            this.tpConfig.Name = "tpConfig";
            this.tpConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tpConfig.Size = new System.Drawing.Size(683, 319);
            this.tpConfig.TabIndex = 0;
            this.tpConfig.Text = "Sql设置";
            this.tpConfig.UseVisualStyleBackColor = true;
            // 
            // txtSql
            // 
            this.txtSql.Font = new System.Drawing.Font("宋体", 10F);
            this.txtSql.HideSelection = false;
            this.txtSql.Location = new System.Drawing.Point(8, 80);
            this.txtSql.Name = "txtSql";
            this.txtSql.Size = new System.Drawing.Size(661, 233);
            this.txtSql.SuperText = "";
            this.txtSql.TabIndex = 5;
            this.txtSql.Text = "";
            this.txtSql.名称 = "richTextBox1";
            this.txtSql.是否组 = false;
            this.txtSql.组 = "无";
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(8, 65);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(47, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 3;
            this.neuLabel3.Text = "Sql语句";
            // 
            // ckSqlXml
            // 
            this.ckSqlXml.AutoSize = true;
            this.ckSqlXml.Location = new System.Drawing.Point(8, 39);
            this.ckSqlXml.Name = "ckSqlXml";
            this.ckSqlXml.Size = new System.Drawing.Size(186, 16);
            this.ckSqlXml.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ckSqlXml.TabIndex = 3;
            this.ckSqlXml.Text = "Sql位置(是否位于配置文件内)";
            this.ckSqlXml.UseVisualStyleBackColor = true;
            // 
            // txtFpPathName
            // 
            this.txtFpPathName.Location = new System.Drawing.Point(340, 40);
            this.txtFpPathName.Name = "txtFpPathName";
            this.txtFpPathName.Size = new System.Drawing.Size(331, 21);
            this.txtFpPathName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtFpPathName.TabIndex = 4;
            // 
            // txtSqlIndex
            // 
            this.txtSqlIndex.Location = new System.Drawing.Point(340, 9);
            this.txtSqlIndex.Name = "txtSqlIndex";
            this.txtSqlIndex.Size = new System.Drawing.Size(331, 21);
            this.txtSqlIndex.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtSqlIndex.TabIndex = 2;
            // 
            // txtReportID
            // 
            this.txtReportID.Location = new System.Drawing.Point(100, 9);
            this.txtReportID.Name = "txtReportID";
            this.txtReportID.Size = new System.Drawing.Size(127, 21);
            this.txtReportID.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtReportID.TabIndex = 1;
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point(233, 43);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(101, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 0;
            this.neuLabel4.Text = "格式文件(FpPath)";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(233, 13);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(95, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 0;
            this.neuLabel2.Text = "索    引(Index)";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(8, 13);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(89, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "主键(ReportID)";
            // 
            // neuTabControl1
            // 
            this.neuTabControl1.Controls.Add(this.tpConfig);
            this.neuTabControl1.Controls.Add(this.tpFpSet);
            this.neuTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuTabControl1.Location = new System.Drawing.Point(0, 0);
            this.neuTabControl1.Name = "neuTabControl1";
            this.neuTabControl1.SelectedIndex = 0;
            this.neuTabControl1.Size = new System.Drawing.Size(691, 344);
            this.neuTabControl1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTabControl1.TabIndex = 2;
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.ckContinue);
            this.neuGroupBox1.Controls.Add(this.btnClear);
            this.neuGroupBox1.Controls.Add(this.btnCancel);
            this.neuGroupBox1.Controls.Add(this.btnOK);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 344);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(691, 45);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 3;
            this.neuGroupBox1.TabStop = false;
            // 
            // frmSetConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 389);
            this.Controls.Add(this.neuTabControl1);
            this.Controls.Add(this.neuGroupBox1);
            this.Name = "frmSetConfig";
            this.Text = "frmSetConfig";
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.neuGroupBox2.ResumeLayout(false);
            this.neuGroupBox2.PerformLayout();
            this.tpFpSet.ResumeLayout(false);
            this.tpConfig.ResumeLayout(false);
            this.tpConfig.PerformLayout();
            this.neuTabControl1.ResumeLayout(false);
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox2;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtPath;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ckContinue;
        private System.Windows.Forms.TabPage tpFpSet;
        private FS.FrameWork.WinForms.Controls.NeuButton btnClear;
        private FS.FrameWork.WinForms.Controls.NeuButton btnCancel;
        private FS.FrameWork.WinForms.Controls.NeuButton btnOK;
        private System.Windows.Forms.TabPage tpConfig;
        private FS.FrameWork.WinForms.Controls.RichTextBox txtSql;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ckSqlXml;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtFpPathName;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtSqlIndex;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtReportID;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuTabControl neuTabControl1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
    }
}