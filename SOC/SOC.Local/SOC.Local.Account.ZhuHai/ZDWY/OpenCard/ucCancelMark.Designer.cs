namespace FS.SOC.Local.Account.ZhuHai.ZDWY.OpenCard
{
    partial class ucCancelMark
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
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType2 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.ckAllStop = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.btOk = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btCancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpMarkSheet = new FarPoint.Win.Spread.SheetView();
            this.ckAllBack = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpMarkSheet)).BeginInit();
            this.SuspendLayout();
            // 
            // ckAllStop
            // 
            this.ckAllStop.AutoSize = true;
            this.ckAllStop.Location = new System.Drawing.Point(13, 245);
            this.ckAllStop.Name = "ckAllStop";
            this.ckAllStop.Size = new System.Drawing.Size(72, 16);
            this.ckAllStop.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ckAllStop.TabIndex = 1;
            this.ckAllStop.Text = "停用全选";
            this.ckAllStop.UseVisualStyleBackColor = true;
            this.ckAllStop.CheckedChanged += new System.EventHandler(this.ckAll_CheckedChanged);
            // 
            // btOk
            // 
            this.btOk.Location = new System.Drawing.Point(204, 240);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 26);
            this.btOk.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btOk.TabIndex = 2;
            this.btOk.Text = "确定(&O)";
            this.btOk.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(285, 240);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 26);
            this.btCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btCancel.TabIndex = 3;
            this.btCancel.Text = "退出(&C)";
            this.btCancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, ";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.FileName = "";
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(8, 7);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpMarkSheet});
            this.neuSpread1.Size = new System.Drawing.Size(366, 227);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 4;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // neuSpMarkSheet
            // 
            this.neuSpMarkSheet.Reset();
            this.neuSpMarkSheet.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpMarkSheet.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpMarkSheet.ColumnCount = 4;
            this.neuSpMarkSheet.RowCount = 3;
            this.neuSpMarkSheet.ColumnHeader.Cells.Get(0, 0).Value = "停用";
            this.neuSpMarkSheet.ColumnHeader.Cells.Get(0, 1).Value = "退卡";
            this.neuSpMarkSheet.ColumnHeader.Cells.Get(0, 2).Value = "就诊卡号";
            this.neuSpMarkSheet.ColumnHeader.Cells.Get(0, 3).Value = "卡类型";
            this.neuSpMarkSheet.Columns.Get(0).CellType = checkBoxCellType1;
            this.neuSpMarkSheet.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpMarkSheet.Columns.Get(0).Label = "停用";
            this.neuSpMarkSheet.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpMarkSheet.Columns.Get(0).Width = 45F;
            this.neuSpMarkSheet.Columns.Get(1).CellType = checkBoxCellType2;
            this.neuSpMarkSheet.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpMarkSheet.Columns.Get(1).Label = "退卡";
            this.neuSpMarkSheet.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpMarkSheet.Columns.Get(1).Width = 45F;
            this.neuSpMarkSheet.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpMarkSheet.Columns.Get(2).Label = "就诊卡号";
            this.neuSpMarkSheet.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpMarkSheet.Columns.Get(2).Width = 150F;
            this.neuSpMarkSheet.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpMarkSheet.Columns.Get(3).Label = "卡类型";
            this.neuSpMarkSheet.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpMarkSheet.Columns.Get(3).Width = 80F;
            this.neuSpMarkSheet.RowHeader.Columns.Default.Resizable = false;
            this.neuSpMarkSheet.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ckAllBack
            // 
            this.ckAllBack.AutoSize = true;
            this.ckAllBack.Location = new System.Drawing.Point(96, 245);
            this.ckAllBack.Name = "ckAllBack";
            this.ckAllBack.Size = new System.Drawing.Size(72, 16);
            this.ckAllBack.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ckAllBack.TabIndex = 1;
            this.ckAllBack.Text = "退卡全选";
            this.ckAllBack.UseVisualStyleBackColor = true;
            this.ckAllBack.CheckedChanged += new System.EventHandler(this.ckAllBack_CheckedChanged);
            // 
            // ucCancelMark
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuSpread1);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.ckAllBack);
            this.Controls.Add(this.ckAllStop);
            this.Name = "ucCancelMark";
            this.Size = new System.Drawing.Size(383, 273);
            this.Load += new System.EventHandler(this.ucCancelMark_Load);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpMarkSheet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuCheckBox ckAllStop;
        private FS.FrameWork.WinForms.Controls.NeuButton btOk;
        private FS.FrameWork.WinForms.Controls.NeuButton btCancel;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpMarkSheet;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ckAllBack;

    }
}
