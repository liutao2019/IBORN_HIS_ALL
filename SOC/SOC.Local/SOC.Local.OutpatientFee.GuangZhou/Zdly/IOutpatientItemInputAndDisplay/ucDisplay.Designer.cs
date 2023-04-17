﻿namespace FS.SOC.Local.OutpatientFee.GuangZhou.Zdly.IOutpatientItemInputAndDisplay
{
    partial class ucDisplay
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
                    dsItem.Clear();
                    dsItem.Dispose();
                    dvItem.Dispose();

                    dvItem = null;
                    dsItem = null;

                    this.fpSheetItem.Dispose();
                    this.fpSheetItem.DataSource = null;
                    this.fpSheetItem = null;
                }
                if (outpatientManager != null)
                {
                    outpatientManager.Dispose();
                    outpatientManager = null;
                }
            }

            base.Dispose(disposing);

            if (disposing)
            {
                System.GC.Collect();
            }
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
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType7 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType8 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType9 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType10 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType11 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType12 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType13 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType14 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType15 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType16 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType17 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType18 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType19 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType20 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType21 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType22 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.fpSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsMenuRebateRate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuFeeItems = new System.Windows.Forms.ToolStripMenuItem();
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "3.0.2004.2005";
            this.fpSpread1.AccessibleDescription = "fpSpread1, Sheet1, Row 0, Column 0, ";
            this.fpSpread1.BackColor = System.Drawing.Color.White;
            this.fpSpread1.ContextMenuStrip = this.contextMenuStrip1;
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread1.EditModePermanent = true;
            this.fpSpread1.EditModeReplace = true;
            this.fpSpread1.FileName = "";
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.IsAutoSaveGridStatus = false;
            this.fpSpread1.IsCanCustomConfigColumn = false;
            this.fpSpread1.Location = new System.Drawing.Point(0, 0);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1});
            this.fpSpread1.Size = new System.Drawing.Size(1008, 474);
            this.fpSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpSpread1.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread1.TextTipAppearance = tipAppearance1;
            this.fpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.fpSpread1_ButtonClicked);
            this.fpSpread1.Leave += new System.EventHandler(this.fpSpread1_Leave);
            this.fpSpread1.EditModeOn += new System.EventHandler(this.fpSpread1_EditModeOn);
            this.fpSpread1.EditChange += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.fpSpread1_EditChange);
            this.fpSpread1.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpSpread1_CellClick);
            this.fpSpread1.ComboSelChange += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.fpSpread1_ComboSelChange);
            this.fpSpread1.Enter += new System.EventHandler(this.fpSpread1_Enter);
            this.fpSpread1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(this.fpSpread1_ColumnWidthChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMenuRebateRate,
            this.tsMenuFeeItems});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 70);
            // 
            // tsMenuRebateRate
            // 
            this.tsMenuRebateRate.Name = "tsMenuRebateRate";
            this.tsMenuRebateRate.Size = new System.Drawing.Size(152, 22);
            this.tsMenuRebateRate.Text = "设置打折";
            // 
            // tsMenuFeeItems
            // 
            this.tsMenuFeeItems.Name = "tsMenuFeeItems";
            this.tsMenuFeeItems.Size = new System.Drawing.Size(152, 22);
            this.tsMenuFeeItems.Text = "费用明细";
            this.tsMenuFeeItems.Click += new System.EventHandler(this.tsMenuFeeItems_Click);
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.ColumnCount = 26;
            this.fpSpread1_Sheet1.RowCount = 1;
            this.fpSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = " ";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "编码";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "名称";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = ".";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "数量";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "单位";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "付数/天数";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "用量";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "单位";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "组";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "频次";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "用法";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "执行科室";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "自付比例";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 14).Value = "医保类型";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 15).Value = "公费类型";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 16).Value = "设备号";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 17).Value = "金额";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 18).Value = "结算";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 19).Value = "小计";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 20).Value = "单价";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 21).Value = "备注";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 22).Value = "feecode";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 23).Value = "itemtype";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 24).Value = "itemcode";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 25).Value = "change";
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.fpSpread1_Sheet1.Columns.Get(0).CellType = checkBoxCellType1;
            this.fpSpread1_Sheet1.Columns.Get(0).Label = " ";
            this.fpSpread1_Sheet1.Columns.Get(0).Width = 20F;
            this.fpSpread1_Sheet1.Columns.Get(1).CellType = textCellType1;
            this.fpSpread1_Sheet1.Columns.Get(1).Font = new System.Drawing.Font("宋体", 14F);
            this.fpSpread1_Sheet1.Columns.Get(1).Label = "编码";
            this.fpSpread1_Sheet1.Columns.Get(1).Width = 50F;
            this.fpSpread1_Sheet1.Columns.Get(2).CellType = textCellType2;
            this.fpSpread1_Sheet1.Columns.Get(2).Font = new System.Drawing.Font("宋体", 14F);
            this.fpSpread1_Sheet1.Columns.Get(2).Label = "名称";
            this.fpSpread1_Sheet1.Columns.Get(2).Locked = true;
            this.fpSpread1_Sheet1.Columns.Get(2).Width = 190F;
            this.fpSpread1_Sheet1.Columns.Get(3).CellType = textCellType3;
            this.fpSpread1_Sheet1.Columns.Get(3).Font = new System.Drawing.Font("宋体", 14F);
            this.fpSpread1_Sheet1.Columns.Get(3).Label = ".";
            this.fpSpread1_Sheet1.Columns.Get(3).Locked = true;
            this.fpSpread1_Sheet1.Columns.Get(3).Width = 18F;
            this.fpSpread1_Sheet1.Columns.Get(4).CellType = textCellType4;
            this.fpSpread1_Sheet1.Columns.Get(4).Font = new System.Drawing.Font("宋体", 14F);
            this.fpSpread1_Sheet1.Columns.Get(4).Label = "数量";
            this.fpSpread1_Sheet1.Columns.Get(5).CellType = textCellType5;
            this.fpSpread1_Sheet1.Columns.Get(5).Font = new System.Drawing.Font("宋体", 14F);
            this.fpSpread1_Sheet1.Columns.Get(5).Label = "单位";
            this.fpSpread1_Sheet1.Columns.Get(5).Locked = true;
            this.fpSpread1_Sheet1.Columns.Get(5).Width = 43F;
            this.fpSpread1_Sheet1.Columns.Get(6).CellType = textCellType6;
            this.fpSpread1_Sheet1.Columns.Get(6).Font = new System.Drawing.Font("宋体", 14F);
            this.fpSpread1_Sheet1.Columns.Get(6).Label = "付数/天数";
            this.fpSpread1_Sheet1.Columns.Get(6).Width = 40F;
            this.fpSpread1_Sheet1.Columns.Get(7).CellType = textCellType7;
            this.fpSpread1_Sheet1.Columns.Get(7).Font = new System.Drawing.Font("宋体", 14F);
            this.fpSpread1_Sheet1.Columns.Get(7).Label = "用量";
            this.fpSpread1_Sheet1.Columns.Get(7).Width = 22F;
            this.fpSpread1_Sheet1.Columns.Get(8).CellType = textCellType8;
            this.fpSpread1_Sheet1.Columns.Get(8).Font = new System.Drawing.Font("宋体", 14F);
            this.fpSpread1_Sheet1.Columns.Get(8).Label = "单位";
            this.fpSpread1_Sheet1.Columns.Get(8).Locked = true;
            this.fpSpread1_Sheet1.Columns.Get(8).Width = 23F;
            this.fpSpread1_Sheet1.Columns.Get(9).CellType = textCellType9;
            this.fpSpread1_Sheet1.Columns.Get(9).Font = new System.Drawing.Font("宋体", 14F);
            this.fpSpread1_Sheet1.Columns.Get(9).Label = "组";
            this.fpSpread1_Sheet1.Columns.Get(9).Width = 16F;
            this.fpSpread1_Sheet1.Columns.Get(10).CellType = textCellType10;
            this.fpSpread1_Sheet1.Columns.Get(10).Font = new System.Drawing.Font("宋体", 14F);
            this.fpSpread1_Sheet1.Columns.Get(10).Label = "频次";
            this.fpSpread1_Sheet1.Columns.Get(10).Width = 23F;
            this.fpSpread1_Sheet1.Columns.Get(11).CellType = textCellType11;
            this.fpSpread1_Sheet1.Columns.Get(11).Font = new System.Drawing.Font("宋体", 14F);
            this.fpSpread1_Sheet1.Columns.Get(11).Label = "用法";
            this.fpSpread1_Sheet1.Columns.Get(11).Width = 21F;
            this.fpSpread1_Sheet1.Columns.Get(12).CellType = textCellType12;
            this.fpSpread1_Sheet1.Columns.Get(12).Font = new System.Drawing.Font("宋体", 14F);
            this.fpSpread1_Sheet1.Columns.Get(12).Label = "执行科室";
            this.fpSpread1_Sheet1.Columns.Get(12).Width = 98F;
            this.fpSpread1_Sheet1.Columns.Get(13).CellType = numberCellType1;
            this.fpSpread1_Sheet1.Columns.Get(13).Font = new System.Drawing.Font("宋体", 14F);
            this.fpSpread1_Sheet1.Columns.Get(13).Label = "自付比例";
            this.fpSpread1_Sheet1.Columns.Get(13).Width = 58F;
            this.fpSpread1_Sheet1.Columns.Get(14).CellType = textCellType13;
            this.fpSpread1_Sheet1.Columns.Get(14).Font = new System.Drawing.Font("宋体", 14F);
            this.fpSpread1_Sheet1.Columns.Get(14).Label = "医保类型";
            this.fpSpread1_Sheet1.Columns.Get(14).Width = 59F;
            this.fpSpread1_Sheet1.Columns.Get(15).CellType = textCellType14;
            this.fpSpread1_Sheet1.Columns.Get(15).Font = new System.Drawing.Font("宋体", 14F);
            this.fpSpread1_Sheet1.Columns.Get(15).Label = "公费类型";
            this.fpSpread1_Sheet1.Columns.Get(16).CellType = textCellType15;
            this.fpSpread1_Sheet1.Columns.Get(16).Font = new System.Drawing.Font("宋体", 14F);
            this.fpSpread1_Sheet1.Columns.Get(16).Label = "设备号";
            this.fpSpread1_Sheet1.Columns.Get(17).CellType = numberCellType2;
            this.fpSpread1_Sheet1.Columns.Get(17).Font = new System.Drawing.Font("宋体", 14F);
            this.fpSpread1_Sheet1.Columns.Get(17).Label = "金额";
            this.fpSpread1_Sheet1.Columns.Get(17).Locked = true;
            this.fpSpread1_Sheet1.Columns.Get(17).Width = 80F;
            this.fpSpread1_Sheet1.Columns.Get(18).CellType = textCellType16;
            this.fpSpread1_Sheet1.Columns.Get(18).Font = new System.Drawing.Font("宋体", 14F);
            this.fpSpread1_Sheet1.Columns.Get(18).Label = "结算";
            this.fpSpread1_Sheet1.Columns.Get(18).Width = 45F;
            this.fpSpread1_Sheet1.Columns.Get(19).CellType = textCellType17;
            this.fpSpread1_Sheet1.Columns.Get(19).Font = new System.Drawing.Font("宋体", 14F);
            this.fpSpread1_Sheet1.Columns.Get(19).Label = "小计";
            this.fpSpread1_Sheet1.Columns.Get(19).Locked = true;
            this.fpSpread1_Sheet1.Columns.Get(20).CellType = numberCellType3;
            this.fpSpread1_Sheet1.Columns.Get(20).Font = new System.Drawing.Font("宋体", 14F);
            this.fpSpread1_Sheet1.Columns.Get(20).Label = "单价";
            this.fpSpread1_Sheet1.Columns.Get(20).Width = 75F;
            this.fpSpread1_Sheet1.Columns.Get(21).CellType = textCellType18;
            this.fpSpread1_Sheet1.Columns.Get(21).Font = new System.Drawing.Font("宋体", 14F);
            this.fpSpread1_Sheet1.Columns.Get(21).Label = "备注";
            this.fpSpread1_Sheet1.Columns.Get(22).CellType = textCellType19;
            this.fpSpread1_Sheet1.Columns.Get(22).Label = "feecode";
            this.fpSpread1_Sheet1.Columns.Get(22).Locked = true;
            this.fpSpread1_Sheet1.Columns.Get(22).Visible = false;
            this.fpSpread1_Sheet1.Columns.Get(23).CellType = textCellType20;
            this.fpSpread1_Sheet1.Columns.Get(23).Label = "itemtype";
            this.fpSpread1_Sheet1.Columns.Get(23).Visible = false;
            this.fpSpread1_Sheet1.Columns.Get(24).CellType = textCellType21;
            this.fpSpread1_Sheet1.Columns.Get(24).Label = "itemcode";
            this.fpSpread1_Sheet1.Columns.Get(24).Visible = false;
            this.fpSpread1_Sheet1.Columns.Get(25).CellType = textCellType22;
            this.fpSpread1_Sheet1.Columns.Get(25).Label = "change";
            this.fpSpread1_Sheet1.Columns.Get(25).Visible = false;
            this.fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 41F;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.Rows.Default.Height = 22F;
            this.fpSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSpread1_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.fpSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpSpread1_Sheet1.CellChanged += new FarPoint.Win.Spread.SheetViewEventHandler(this.fpSpread1_Sheet1_CellChanged);
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ucDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fpSpread1);
            this.Name = "ucDisplay";
            this.Size = new System.Drawing.Size(1008, 474);
            this.Enter += new System.EventHandler(this.ucDisplay_Enter);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected FS.FrameWork.WinForms.Controls.NeuSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsMenuRebateRate;
        private System.Windows.Forms.ToolStripMenuItem tsMenuFeeItems;
    }
}
