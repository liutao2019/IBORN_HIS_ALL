namespace SOC.Local.Operation
{
    partial class frmOpsLEDShow
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
            this.components = new System.ComponentModel.Container();
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.nPanelMain = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuOperationSpread = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuOperationSpread_汇总 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.nlbDate = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.lblWindow = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.nPanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuOperationSpread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuOperationSpread_汇总)).BeginInit();
            this.neuPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 3000;
            // 
            // nPanelMain
            // 
            this.nPanelMain.BackColor = System.Drawing.Color.Transparent;
            this.nPanelMain.Controls.Add(this.neuOperationSpread);
            this.nPanelMain.Controls.Add(this.neuPanel1);
            this.nPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nPanelMain.ForeColor = System.Drawing.Color.Transparent;
            this.nPanelMain.Location = new System.Drawing.Point(0, 0);
            this.nPanelMain.Name = "nPanelMain";
            this.nPanelMain.Size = new System.Drawing.Size(1256, 809);
            this.nPanelMain.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nPanelMain.TabIndex = 0;
            // 
            // neuOperationSpread
            // 
            this.neuOperationSpread.About = "3.0.2004.2005";
            this.neuOperationSpread.AccessibleDescription = "neuOperationSpread, 汇总, Row 0, Column 0, ";
            this.neuOperationSpread.BackColor = System.Drawing.SystemColors.Control;
            this.neuOperationSpread.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuOperationSpread.FileName = "";
            this.neuOperationSpread.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuOperationSpread.IsAutoSaveGridStatus = false;
            this.neuOperationSpread.IsCanCustomConfigColumn = false;
            this.neuOperationSpread.Location = new System.Drawing.Point(0, 87);
            this.neuOperationSpread.Name = "neuOperationSpread";
            this.neuOperationSpread.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuOperationSpread.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuOperationSpread_汇总});
            this.neuOperationSpread.Size = new System.Drawing.Size(1256, 722);
            this.neuOperationSpread.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuOperationSpread.TabIndex = 6;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuOperationSpread.TextTipAppearance = tipAppearance1;
            this.neuOperationSpread.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // neuOperationSpread_汇总
            // 
            this.neuOperationSpread_汇总.Reset();
            this.neuOperationSpread_汇总.SheetName = "汇总";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuOperationSpread_汇总.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuOperationSpread_汇总.ColumnCount = 7;
            this.neuOperationSpread_汇总.RowCount = 1;
            this.neuOperationSpread_汇总.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247))))), System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(217)))), ((int)(((byte)(217))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247))))), false, false, false, true, true);
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 0).Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 0).Value = "手术间";
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 1).Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 1).Value = "科室";
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 2).Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 2).Value = "床号";
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 3).Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 3).Value = "姓名";
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 4).Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 4).Value = "性别";
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 5).Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 5).Value = "住院号";
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 6).Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 6).Value = "状态";
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.neuOperationSpread_汇总.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuOperationSpread_汇总.ColumnHeader.Rows.Get(0).Height = 65F;
            this.neuOperationSpread_汇总.Columns.Get(0).Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(0).Label = "手术间";
            this.neuOperationSpread_汇总.Columns.Get(0).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(0).Width = 104F;
            this.neuOperationSpread_汇总.Columns.Get(1).Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuOperationSpread_汇总.Columns.Get(1).Label = "科室";
            this.neuOperationSpread_汇总.Columns.Get(1).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(1).Width = 275F;
            this.neuOperationSpread_汇总.Columns.Get(2).Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(2).Label = "床号";
            this.neuOperationSpread_汇总.Columns.Get(2).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(2).Width = 85F;
            this.neuOperationSpread_汇总.Columns.Get(3).Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(3).Label = "姓名";
            this.neuOperationSpread_汇总.Columns.Get(3).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(3).Width = 167F;
            this.neuOperationSpread_汇总.Columns.Get(4).Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(4).Label = "性别";
            this.neuOperationSpread_汇总.Columns.Get(4).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(4).Width = 73F;
            this.neuOperationSpread_汇总.Columns.Get(5).Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(5).Label = "住院号";
            this.neuOperationSpread_汇总.Columns.Get(5).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(5).Width = 235F;
            this.neuOperationSpread_汇总.Columns.Get(6).Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(6).Label = "状态";
            this.neuOperationSpread_汇总.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(6).Width = 314F;
            this.neuOperationSpread_汇总.DefaultStyle.Locked = false;
            this.neuOperationSpread_汇总.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.neuOperationSpread_汇总.RowHeader.Columns.Default.Resizable = true;
            this.neuOperationSpread_汇总.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.neuOperationSpread_汇总.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuOperationSpread_汇总.Rows.Get(0).Height = 19F;
            this.neuOperationSpread_汇总.SheetCornerStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.neuOperationSpread_汇总.SheetCornerStyle.Locked = false;
            this.neuOperationSpread_汇总.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuOperationSpread_汇总.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuOperationSpread_汇总.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuPanel1
            // 
            this.neuPanel1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.neuPanel1.Controls.Add(this.nlbDate);
            this.neuPanel1.Controls.Add(this.button1);
            this.neuPanel1.Controls.Add(this.lblWindow);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(1256, 87);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // nlbDate
            // 
            this.nlbDate.AutoSize = true;
            this.nlbDate.Font = new System.Drawing.Font("华文中宋", 22F, System.Drawing.FontStyle.Bold);
            this.nlbDate.ForeColor = System.Drawing.Color.Navy;
            this.nlbDate.Location = new System.Drawing.Point(21, 47);
            this.nlbDate.Name = "nlbDate";
            this.nlbDate.Size = new System.Drawing.Size(294, 34);
            this.nlbDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbDate.TabIndex = 3;
            this.nlbDate.Text = "日期：2014-12-02";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1059, 58);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(35, 23);
            this.button1.TabIndex = 1;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // lblWindow
            // 
            this.lblWindow.AutoSize = true;
            this.lblWindow.Font = new System.Drawing.Font("华文中宋", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWindow.ForeColor = System.Drawing.Color.Navy;
            this.lblWindow.Location = new System.Drawing.Point(340, 9);
            this.lblWindow.Name = "lblWindow";
            this.lblWindow.Size = new System.Drawing.Size(427, 64);
            this.lblWindow.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblWindow.TabIndex = 0;
            this.lblWindow.Text = "手术状态一览表";
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 3000;
            // 
            // frmOpsLEDShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1256, 809);
            this.Controls.Add(this.nPanelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmOpsLEDShow";
            this.Text = "屏幕显示";
            this.Load += new System.EventHandler(this.frmDisplay_Load);
            this.DoubleClick += new System.EventHandler(this.frmDisplay_DoubleClick);
            this.nPanelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuOperationSpread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuOperationSpread_汇总)).EndInit();
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private System.Windows.Forms.Timer timer1;
        private FS.FrameWork.WinForms.Controls.NeuPanel nPanelMain;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblWindow;
        protected FS.FrameWork.WinForms.Controls.NeuSpread neuOperationSpread;
        private FarPoint.Win.Spread.SheetView neuOperationSpread_汇总;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timer2;
        private FS.FrameWork.WinForms.Controls.NeuLabel nlbDate;
    }
}