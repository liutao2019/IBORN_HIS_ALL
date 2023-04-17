namespace SOC.Local.Operation
{
    partial class ucOperationStateShow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            FarPoint.Win.Spread.TipAppearance tipAppearance3 = new FarPoint.Win.Spread.TipAppearance();
            this.neuPanelMain = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanelBottom = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuOperationSpread = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuOperationSpread_汇总 = new FarPoint.Win.Spread.SheetView();
            this.neuPanelTop = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuOperationDate = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.neuPanelMain.SuspendLayout();
            this.neuPanelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuOperationSpread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuOperationSpread_汇总)).BeginInit();
            this.neuPanelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanelMain
            // 
            this.neuPanelMain.BackColor = System.Drawing.Color.Transparent;
            this.neuPanelMain.Controls.Add(this.neuPanelBottom);
            this.neuPanelMain.Controls.Add(this.neuPanelTop);
            this.neuPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanelMain.Location = new System.Drawing.Point(0, 0);
            this.neuPanelMain.Name = "neuPanelMain";
            this.neuPanelMain.Size = new System.Drawing.Size(932, 495);
            this.neuPanelMain.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanelMain.TabIndex = 0;
            // 
            // neuPanelBottom
            // 
            this.neuPanelBottom.Controls.Add(this.neuOperationSpread);
            this.neuPanelBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanelBottom.Location = new System.Drawing.Point(0, 52);
            this.neuPanelBottom.Name = "neuPanelBottom";
            this.neuPanelBottom.Size = new System.Drawing.Size(932, 443);
            this.neuPanelBottom.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanelBottom.TabIndex = 1;
            // 
            // neuOperationSpread
            // 
            this.neuOperationSpread.About = "3.0.2004.2005";
            this.neuOperationSpread.AccessibleDescription = "neuOperationSpread, 汇总";
            this.neuOperationSpread.BackColor = System.Drawing.SystemColors.Control;
            this.neuOperationSpread.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuOperationSpread.FileName = "";
            this.neuOperationSpread.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuOperationSpread.IsAutoSaveGridStatus = false;
            this.neuOperationSpread.IsCanCustomConfigColumn = false;
            this.neuOperationSpread.Location = new System.Drawing.Point(0, 0);
            this.neuOperationSpread.Name = "neuOperationSpread";
            this.neuOperationSpread.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuOperationSpread.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuOperationSpread_汇总});
            this.neuOperationSpread.Size = new System.Drawing.Size(932, 443);
            this.neuOperationSpread.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuOperationSpread.TabIndex = 5;
            tipAppearance3.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance3.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuOperationSpread.TextTipAppearance = tipAppearance3;
            this.neuOperationSpread.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // neuOperationSpread_汇总
            // 
            this.neuOperationSpread_汇总.Reset();
            this.neuOperationSpread_汇总.SheetName = "汇总";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuOperationSpread_汇总.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuOperationSpread_汇总.ColumnCount = 7;
            this.neuOperationSpread_汇总.RowCount = 0;
            this.neuOperationSpread_汇总.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247))))), System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(217)))), ((int)(((byte)(217))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247))))), false, false, false, true, true);
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 0).Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 0).Value = "手术间";
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 1).Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 1).Value = "科室";
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 2).Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 2).Value = "床号";
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 3).Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 3).Value = "姓名";
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 4).Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 4).Value = "性别";
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 5).Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 5).Value = "住院号";
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 6).Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 6).Value = "状态";
            this.neuOperationSpread_汇总.ColumnHeader.Cells.Get(0, 6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.neuOperationSpread_汇总.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuOperationSpread_汇总.ColumnHeader.Rows.Get(0).Height = 34F;
            this.neuOperationSpread_汇总.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(0).Label = "手术间";
            this.neuOperationSpread_汇总.Columns.Get(0).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(0).Width = 76F;
            this.neuOperationSpread_汇总.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuOperationSpread_汇总.Columns.Get(1).Label = "科室";
            this.neuOperationSpread_汇总.Columns.Get(1).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(1).Width = 153F;
            this.neuOperationSpread_汇总.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(2).Label = "床号";
            this.neuOperationSpread_汇总.Columns.Get(2).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(2).Width = 55F;
            this.neuOperationSpread_汇总.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(3).Label = "姓名";
            this.neuOperationSpread_汇总.Columns.Get(3).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(3).Width = 82F;
            this.neuOperationSpread_汇总.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(4).Label = "性别";
            this.neuOperationSpread_汇总.Columns.Get(4).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(4).Width = 57F;
            this.neuOperationSpread_汇总.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(5).Label = "住院号";
            this.neuOperationSpread_汇总.Columns.Get(5).Locked = true;
            this.neuOperationSpread_汇总.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(5).Width = 75F;
            this.neuOperationSpread_汇总.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(6).Label = "状态";
            this.neuOperationSpread_汇总.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.Columns.Get(6).Width = 104F;
            this.neuOperationSpread_汇总.DefaultStyle.Locked = false;
            this.neuOperationSpread_汇总.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuOperationSpread_汇总.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.neuOperationSpread_汇总.RowHeader.Columns.Default.Resizable = true;
            this.neuOperationSpread_汇总.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.neuOperationSpread_汇总.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuOperationSpread_汇总.SheetCornerStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.neuOperationSpread_汇总.SheetCornerStyle.Locked = false;
            this.neuOperationSpread_汇总.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuOperationSpread_汇总.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuOperationSpread_汇总.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuOperationSpread.SetActiveViewport(0, 1, 0);
            // 
            // neuPanelTop
            // 
            this.neuPanelTop.Controls.Add(this.neuOperationDate);
            this.neuPanelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanelTop.Location = new System.Drawing.Point(0, 0);
            this.neuPanelTop.Name = "neuPanelTop";
            this.neuPanelTop.Size = new System.Drawing.Size(932, 52);
            this.neuPanelTop.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanelTop.TabIndex = 0;
            // 
            // neuOperationDate
            // 
            this.neuOperationDate.CustomFormat = "yyyy-MM-dd";
            this.neuOperationDate.Font = new System.Drawing.Font("宋体", 11F);
            this.neuOperationDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.neuOperationDate.IsEnter2Tab = false;
            this.neuOperationDate.Location = new System.Drawing.Point(20, 15);
            this.neuOperationDate.Name = "neuOperationDate";
            this.neuOperationDate.Size = new System.Drawing.Size(122, 24);
            this.neuOperationDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuOperationDate.TabIndex = 1;
            // 
            // timer1
            // 
            this.timer1.Interval = 50000;
            // 
            // ucOperationStateShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.neuPanelMain);
            this.Name = "ucOperationStateShow";
            this.Size = new System.Drawing.Size(932, 495);
            this.neuPanelMain.ResumeLayout(false);
            this.neuPanelBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuOperationSpread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuOperationSpread_汇总)).EndInit();
            this.neuPanelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanelMain;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanelBottom;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanelTop;
        protected FS.FrameWork.WinForms.Controls.NeuSpread neuOperationSpread;
        private FarPoint.Win.Spread.SheetView neuOperationSpread_汇总;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker neuOperationDate;
        private System.Windows.Forms.Timer timer1;

    }
}