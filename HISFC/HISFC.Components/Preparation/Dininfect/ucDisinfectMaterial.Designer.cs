﻿namespace FS.HISFC.Components.Preparation.Disinfect
{
    partial class ucDisinfectMaterial
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
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.neuTabControl1 = new FS.FrameWork.WinForms.Controls.NeuTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.fsMaterial = new FS.HISFC.Components.Preparation.FPItem(this.components);
            this.fsMaterial_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fsMaterial)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fsMaterial_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // neuTabControl1
            // 
            this.neuTabControl1.Controls.Add(this.tabPage1);
            this.neuTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuTabControl1.Location = new System.Drawing.Point(0, 0);
            this.neuTabControl1.Name = "neuTabControl1";
            this.neuTabControl1.SelectedIndex = 0;
            this.neuTabControl1.Size = new System.Drawing.Size(689, 330);
            this.neuTabControl1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.fsMaterial);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(681, 305);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "生产原料";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // fsMaterial
            // 
            this.fsMaterial.About = "2.5.2007.2005";
            this.fsMaterial.AccessibleDescription = "fsMaterial, 生产原料, Row 0, Column 0, ";
            this.fsMaterial.BackColor = System.Drawing.Color.White;
            this.fsMaterial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fsMaterial.DrugType = "";
            this.fsMaterial.EditModePermanent = true;
            this.fsMaterial.FileName = "";
            this.fsMaterial.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fsMaterial.IsAutoSaveGridStatus = false;
            this.fsMaterial.IsCanCustomConfigColumn = false;
            this.fsMaterial.Location = new System.Drawing.Point(0, 0);
            this.fsMaterial.Name = "fsMaterial";
            this.fsMaterial.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fsMaterial_Sheet1});
            this.fsMaterial.Size = new System.Drawing.Size(681, 305);
            this.fsMaterial.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fsMaterial.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fsMaterial.TextTipAppearance = tipAppearance1;
            this.fsMaterial.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fsMaterial.SelectItem += new System.EventHandler(this.fsMaterial_SelectItem);
            // 
            // fsMaterial_Sheet1
            // 
            this.fsMaterial_Sheet1.Reset();
            this.fsMaterial_Sheet1.SheetName = "生产原料";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fsMaterial_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fsMaterial_Sheet1.ColumnCount = 7;
            this.fsMaterial_Sheet1.RowCount = 0;
            this.fsMaterial_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.fsMaterial_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "原料编码";
            this.fsMaterial_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "原料名称";
            this.fsMaterial_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "规格";
            this.fsMaterial_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "单价";
            this.fsMaterial_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "处方量";
            this.fsMaterial_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "单位";
            this.fsMaterial_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "备注";
            this.fsMaterial_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fsMaterial_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.fsMaterial_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fsMaterial_Sheet1.Columns.Get(0).Label = "原料编码";
            this.fsMaterial_Sheet1.Columns.Get(0).Visible = false;
            this.fsMaterial_Sheet1.Columns.Get(1).CellType = textCellType1;
            this.fsMaterial_Sheet1.Columns.Get(1).Label = "原料名称";
            this.fsMaterial_Sheet1.Columns.Get(1).Locked = false;
            this.fsMaterial_Sheet1.Columns.Get(1).Width = 349F;
            this.fsMaterial_Sheet1.Columns.Get(2).Label = "规格";
            this.fsMaterial_Sheet1.Columns.Get(2).Width = 80F;
            this.fsMaterial_Sheet1.Columns.Get(3).BackColor = System.Drawing.Color.Transparent;
            this.fsMaterial_Sheet1.Columns.Get(3).CellType = numberCellType1;
            this.fsMaterial_Sheet1.Columns.Get(3).Label = "单价";
            this.fsMaterial_Sheet1.Columns.Get(3).Width = 80F;
            this.fsMaterial_Sheet1.Columns.Get(4).BackColor = System.Drawing.Color.SeaShell;
            this.fsMaterial_Sheet1.Columns.Get(4).CellType = numberCellType2;
            this.fsMaterial_Sheet1.Columns.Get(4).Label = "处方量";
            this.fsMaterial_Sheet1.Columns.Get(4).Locked = false;
            this.fsMaterial_Sheet1.Columns.Get(4).Width = 89F;
            this.fsMaterial_Sheet1.Columns.Get(6).BackColor = System.Drawing.Color.SeaShell;
            this.fsMaterial_Sheet1.Columns.Get(6).Label = "备注";
            this.fsMaterial_Sheet1.Columns.Get(6).Locked = false;
            this.fsMaterial_Sheet1.Columns.Get(6).Width = 179F;
            this.fsMaterial_Sheet1.DefaultStyle.Locked = true;
            this.fsMaterial_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fsMaterial_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fsMaterial_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fsMaterial_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.fsMaterial_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fsMaterial_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.fsMaterial_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fsMaterial_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.fsMaterial_Sheet1.SheetCornerStyle.Locked = false;
            this.fsMaterial_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.fsMaterial_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fsMaterial.SetActiveViewport(1, 0);
            // 
            // ucDisinfectMaterial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.neuTabControl1);
            this.Name = "ucDisinfectMaterial";
            this.Size = new System.Drawing.Size(689, 330);
            this.neuTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fsMaterial)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fsMaterial_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuTabControl neuTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private FPItem fsMaterial;
        private FarPoint.Win.Spread.SheetView fsMaterial_Sheet1;

    }
}