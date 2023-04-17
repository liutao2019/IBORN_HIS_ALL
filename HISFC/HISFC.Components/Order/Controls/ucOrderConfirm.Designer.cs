namespace FS.HISFC.Components.Order.Controls
{
    partial class ucOrderConfirm
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
            this.fpSpread = new FarPoint.Win.Spread.FpSpread();
            this.fpSpread_sheet1 = new FarPoint.Win.Spread.SheetView();
            this.fpSpread_sheet2 = new FarPoint.Win.Spread.SheetView();
            this.panelMain = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread_sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread_sheet2)).BeginInit();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // fpSpread
            // 
            this.fpSpread.About = "3.0.2004.2005";
            this.fpSpread.AccessibleDescription = "fpSpread, 临时医嘱, Row 0, Column 0, ";
            this.fpSpread.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.fpSpread.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread.Location = new System.Drawing.Point(0, 0);
            this.fpSpread.Name = "fpSpread";
            this.fpSpread.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread_sheet1,
            this.fpSpread_sheet2});
            this.fpSpread.Size = new System.Drawing.Size(620, 464);
            this.fpSpread.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread.TextTipAppearance = tipAppearance1;
            // 
            // fpSpread_sheet1
            // 
            this.fpSpread_sheet1.Reset();
            this.fpSpread_sheet1.SheetName = "长期医嘱";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread_sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread_sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpSpread_sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSpread_sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread_sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpSpread_sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.fpSpread_sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSpread_sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread_sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSpread_sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.fpSpread_sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpSpread_sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // fpSpread_sheet2
            // 
            this.fpSpread_sheet2.Reset();
            this.fpSpread_sheet2.SheetName = "临时医嘱";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread_sheet2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread_sheet2.RowHeader.Columns.Get(0).Width = 37F;
            this.fpSpread_sheet2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.fpSpread);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(620, 464);
            this.panelMain.TabIndex = 1;
            // 
            // ucOrderConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelMain);
            this.Name = "ucOrderConfirm";
            this.Size = new System.Drawing.Size(620, 464);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread_sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread_sheet2)).EndInit();
            this.panelMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FarPoint.Win.Spread.FpSpread fpSpread;
        private FarPoint.Win.Spread.SheetView fpSpread_sheet1;
        private FarPoint.Win.Spread.SheetView fpSpread_sheet2;
        private System.Windows.Forms.Panel panelMain;
    }
}
