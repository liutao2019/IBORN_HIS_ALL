namespace FS.SOC.HISFC.Components.Pharmacy.Common.AdjustPrice
{
    partial class frmRetailHistoryAdjust
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
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuGroupBox2 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.nbtCancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.nbtOK = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuDataListSpread = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.neuDataListSpread_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuGroupBox1.SuspendLayout();
            this.neuGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuDataListSpread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuDataListSpread_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.neuGroupBox1.Controls.Add(this.neuLabel1);
            this.neuGroupBox1.Controls.Add(this.nlbInfo);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.neuGroupBox1.Size = new System.Drawing.Size(392, 76);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 7;
            this.neuGroupBox1.TabStop = false;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel1.Location = new System.Drawing.Point(156, 50);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(77, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 7;
            this.neuLabel1.Text = "确认继续吗？";
            // 
            // nlbInfo
            // 
            this.nlbInfo.AutoSize = true;
            this.nlbInfo.ForeColor = System.Drawing.Color.Blue;
            this.nlbInfo.Location = new System.Drawing.Point(25, 26);
            this.nlbInfo.Name = "nlbInfo";
            this.nlbInfo.Size = new System.Drawing.Size(329, 12);
            this.nlbInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbInfo.TabIndex = 6;
            this.nlbInfo.Text = "系统检测到存在未执行的调价记录，请您确认是否继续调价？";
            // 
            // neuGroupBox2
            // 
            this.neuGroupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.neuGroupBox2.Controls.Add(this.nbtCancel);
            this.neuGroupBox2.Controls.Add(this.nbtOK);
            this.neuGroupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox2.Location = new System.Drawing.Point(0, 76);
            this.neuGroupBox2.Name = "neuGroupBox2";
            this.neuGroupBox2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.neuGroupBox2.Size = new System.Drawing.Size(392, 57);
            this.neuGroupBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox2.TabIndex = 8;
            this.neuGroupBox2.TabStop = false;
            // 
            // nbtCancel
            // 
            this.nbtCancel.Location = new System.Drawing.Point(279, 21);
            this.nbtCancel.Name = "nbtCancel";
            this.nbtCancel.Size = new System.Drawing.Size(75, 23);
            this.nbtCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nbtCancel.TabIndex = 1;
            this.nbtCancel.Text = "取消";
            this.nbtCancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.nbtCancel.UseVisualStyleBackColor = true;
            // 
            // nbtOK
            // 
            this.nbtOK.Location = new System.Drawing.Point(168, 21);
            this.nbtOK.Name = "nbtOK";
            this.nbtOK.Size = new System.Drawing.Size(75, 23);
            this.nbtOK.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nbtOK.TabIndex = 0;
            this.nbtOK.Text = "确定";
            this.nbtOK.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.nbtOK.UseVisualStyleBackColor = true;
            // 
            // neuDataListSpread
            // 
            this.neuDataListSpread.About = "3.0.2004.2005";
            this.neuDataListSpread.AccessibleDescription = "neuDataListSpread, Sheet1, Row 0, Column 0, ";
            this.neuDataListSpread.BackColor = System.Drawing.SystemColors.Control;
            this.neuDataListSpread.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuDataListSpread.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuDataListSpread.Location = new System.Drawing.Point(0, 133);
            this.neuDataListSpread.Name = "neuDataListSpread";
            this.neuDataListSpread.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuDataListSpread.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuDataListSpread_Sheet1});
            this.neuDataListSpread.Size = new System.Drawing.Size(392, 99);
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
            this.neuDataListSpread_Sheet1.ColumnCount = 4;
            this.neuDataListSpread_Sheet1.RowCount = 0;
            this.neuDataListSpread_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247))))), System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(217)))), ((int)(((byte)(217))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, false);
            this.neuDataListSpread_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "单号";
            this.neuDataListSpread_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "执行时间";
            this.neuDataListSpread_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "制单人";
            this.neuDataListSpread_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "制单时间";
            this.neuDataListSpread_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.neuDataListSpread_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuDataListSpread_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.neuDataListSpread_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuDataListSpread_Sheet1.Columns.Get(0).Label = "单号";
            this.neuDataListSpread_Sheet1.Columns.Get(0).Width = 72F;
            this.neuDataListSpread_Sheet1.Columns.Get(1).Label = "执行时间";
            this.neuDataListSpread_Sheet1.Columns.Get(1).Width = 128F;
            this.neuDataListSpread_Sheet1.Columns.Get(2).Label = "制单人";
            this.neuDataListSpread_Sheet1.Columns.Get(2).Width = 59F;
            this.neuDataListSpread_Sheet1.Columns.Get(3).Label = "制单时间";
            this.neuDataListSpread_Sheet1.Columns.Get(3).Width = 128F;
            this.neuDataListSpread_Sheet1.DefaultStyle.Locked = false;
            this.neuDataListSpread_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.neuDataListSpread_Sheet1.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuDataListSpread_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.neuDataListSpread_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuDataListSpread_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.neuDataListSpread_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.neuDataListSpread_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuDataListSpread_Sheet1.RowHeader.Visible = false;
            this.neuDataListSpread_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.neuDataListSpread_Sheet1.SelectionStyle = FarPoint.Win.Spread.SelectionStyles.Both;
            this.neuDataListSpread_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.neuDataListSpread_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.neuDataListSpread_Sheet1.SheetCornerStyle.Locked = false;
            this.neuDataListSpread_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuDataListSpread_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuDataListSpread_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuDataListSpread.SetActiveViewport(0, 1, 0);
            // 
            // frmRetailHistoryAdjust
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(392, 232);
            this.Controls.Add(this.neuDataListSpread);
            this.Controls.Add(this.neuGroupBox2);
            this.Controls.Add(this.neuGroupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRetailHistoryAdjust";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "未执行调价记录";
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.neuGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuDataListSpread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuDataListSpread_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbInfo;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox2;
        private FS.FrameWork.WinForms.Controls.NeuButton nbtCancel;
        private FS.FrameWork.WinForms.Controls.NeuButton nbtOK;
        private FS.SOC.Windows.Forms.FpSpread neuDataListSpread;
        private FarPoint.Win.Spread.SheetView neuDataListSpread_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;

    }
}
