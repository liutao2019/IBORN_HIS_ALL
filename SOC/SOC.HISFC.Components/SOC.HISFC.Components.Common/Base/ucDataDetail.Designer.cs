namespace FS.SOC.HISFC.Components.Common.Base
{
    partial class ucDataDetail
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
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.nlbInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ntxtFilter = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.nlbFilter = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuDataListSpread = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.neuDataListSpread_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel1.SuspendLayout();
            this.neuGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuDataListSpread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuDataListSpread_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.nlbInfo);
            this.neuPanel1.Controls.Add(this.ntxtFilter);
            this.neuPanel1.Controls.Add(this.nlbFilter);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel1.Location = new System.Drawing.Point(3, 17);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(729, 35);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 6;
            // 
            // nlbInfo
            // 
            this.nlbInfo.AutoSize = true;
            this.nlbInfo.Location = new System.Drawing.Point(205, 12);
            this.nlbInfo.Name = "nlbInfo";
            this.nlbInfo.Size = new System.Drawing.Size(221, 12);
            this.nlbInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbInfo.TabIndex = 8;
            this.nlbInfo.Text = "当前录入品种，购入总金额，零售总金额";
            // 
            // ntxtFilter
            // 
            this.ntxtFilter.IsEnter2Tab = false;
            this.ntxtFilter.Location = new System.Drawing.Point(93, 8);
            this.ntxtFilter.Name = "ntxtFilter";
            this.ntxtFilter.Size = new System.Drawing.Size(93, 21);
            this.ntxtFilter.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ntxtFilter.TabIndex = 7;
            // 
            // nlbFilter
            // 
            this.nlbFilter.AutoSize = true;
            this.nlbFilter.Location = new System.Drawing.Point(19, 12);
            this.nlbFilter.Name = "nlbFilter";
            this.nlbFilter.Size = new System.Drawing.Size(65, 12);
            this.nlbFilter.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbFilter.TabIndex = 6;
            this.nlbFilter.Text = "过    滤：";
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.neuPanel1);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(735, 55);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 8;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "已录信息";
            // 
            // neuDataListSpread
            // 
            this.neuDataListSpread.About = "3.0.2004.2005";
            this.neuDataListSpread.AccessibleDescription = "neuDataListSpread, Sheet1, Row 0, Column 0, ";
            this.neuDataListSpread.BackColor = System.Drawing.SystemColors.Control;
            this.neuDataListSpread.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuDataListSpread.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuDataListSpread.Location = new System.Drawing.Point(0, 55);
            this.neuDataListSpread.Name = "neuDataListSpread";
            this.neuDataListSpread.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuDataListSpread.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuDataListSpread_Sheet1});
            this.neuDataListSpread.Size = new System.Drawing.Size(735, 493);
            this.neuDataListSpread.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuDataListSpread.TabIndex = 9;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuDataListSpread.TextTipAppearance = tipAppearance1;
            this.neuDataListSpread.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // neuDataListSpread_Sheet1
            // 
            this.neuDataListSpread_Sheet1.Reset();
            this.neuDataListSpread_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuDataListSpread_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuDataListSpread_Sheet1.ColumnCount = 0;
            this.neuDataListSpread_Sheet1.RowCount = 0;
            this.neuDataListSpread_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247))))), System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(217)))), ((int)(((byte)(217))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, false);
            this.neuDataListSpread_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.neuDataListSpread_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuDataListSpread_Sheet1.DefaultStyle.Locked = false;
            this.neuDataListSpread_Sheet1.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuDataListSpread_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.neuDataListSpread_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuDataListSpread_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.neuDataListSpread_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuDataListSpread_Sheet1.RowHeader.Visible = true;
            this.neuDataListSpread_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.neuDataListSpread_Sheet1.SelectionStyle = FarPoint.Win.Spread.SelectionStyles.Both;
            this.neuDataListSpread_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.neuDataListSpread_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.neuDataListSpread_Sheet1.SheetCornerStyle.Locked = false;
            this.neuDataListSpread_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuDataListSpread_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuDataListSpread_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuDataListSpread.SetActiveViewport(0, 1, 1);
            // 
            // ucDataDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuDataListSpread);
            this.Controls.Add(this.neuGroupBox1);
            this.Name = "ucDataDetail";
            this.Size = new System.Drawing.Size(735, 548);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.neuGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuDataListSpread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuDataListSpread_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbInfo;
        protected FS.FrameWork.WinForms.Controls.NeuTextBox ntxtFilter;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbFilter;
        protected FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        protected FS.SOC.Windows.Forms.FpSpread neuDataListSpread;
        protected FarPoint.Win.Spread.SheetView neuDataListSpread_Sheet1;

    }
}
