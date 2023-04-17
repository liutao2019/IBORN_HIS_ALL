namespace FS.HISFC.Components.Order.Controls
{
    /// <summary>
    /// [功能描述: 医嘱执行档显示<br></br>
    /// [创 建 者: wolf]<br></br>
    /// [创建时间: 2004-10-12]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间=''
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class fpOrderExecBrowser
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
            this.fpSpread = new FarPoint.Win.Spread.FpSpread();
            this.fpSpread_Sheet1 = new FarPoint.Win.Spread.SheetView();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // fpSpread
            // 
            this.fpSpread.About = "3.0.2004.2005";
            this.fpSpread.AccessibleDescription = "fpSpread, Sheet1, Row 0, Column 0, ";
            this.fpSpread.BackColor = System.Drawing.SystemColors.Control;
            this.fpSpread.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread.Location = new System.Drawing.Point(0, 0);
            this.fpSpread.Name = "fpSpread";
            this.fpSpread.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread_Sheet1});
            this.fpSpread.Size = new System.Drawing.Size(560, 390);
            this.fpSpread.TabIndex = 0;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread.TextTipAppearance = tipAppearance2;
            // 
            // fpSpread_Sheet1
            // 
            this.fpSpread_Sheet1.Reset();
            this.fpSpread_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            
            this.fpSpread_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSpread_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread_Sheet1.ColumnHeader.Rows.Get(0).Height = 35F;
            this.fpSpread_Sheet1.Columns.Get(12).ForeColor = System.Drawing.Color.Red;
            this.fpSpread_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpSpread_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.fpSpread_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSpread_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread_Sheet1.Rows.Default.Height = 30F;
            this.fpSpread_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSpread_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.fpSpread_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpSpread_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // fpOrderExecBrowser
            // 
            this.Controls.Add(this.fpSpread);
            this.Name = "fpOrderExecBrowser";
            this.Size = new System.Drawing.Size(560, 390);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FarPoint.Win.Spread.SheetView fpSpread_Sheet1;
        public FarPoint.Win.Spread.FpSpread fpSpread;
    }
}
