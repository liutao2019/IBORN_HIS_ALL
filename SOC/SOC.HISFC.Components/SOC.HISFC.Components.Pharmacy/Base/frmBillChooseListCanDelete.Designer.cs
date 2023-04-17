namespace FS.SOC.HISFC.Components.Pharmacy.Base
{
    partial class frmBillChooseListCanDelete
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
            this.nbtDelete = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.nbtQuery = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ncmbState = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuDateTimePicker2 = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuDateTimePicker1 = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
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
            this.neuGroupBox1.Controls.Add(this.nbtDelete);
            this.neuGroupBox1.Controls.Add(this.nbtQuery);
            this.neuGroupBox1.Controls.Add(this.neuLabel3);
            this.neuGroupBox1.Controls.Add(this.neuLabel2);
            this.neuGroupBox1.Controls.Add(this.neuLabel1);
            this.neuGroupBox1.Controls.Add(this.ncmbState);
            this.neuGroupBox1.Controls.Add(this.neuDateTimePicker2);
            this.neuGroupBox1.Controls.Add(this.neuDateTimePicker1);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(638, 57);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 0;
            this.neuGroupBox1.TabStop = false;
            // 
            // nbtDelete
            // 
            this.nbtDelete.Location = new System.Drawing.Point(540, 23);
            this.nbtDelete.Name = "nbtDelete";
            this.nbtDelete.Size = new System.Drawing.Size(75, 23);
            this.nbtDelete.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nbtDelete.TabIndex = 6;
            this.nbtDelete.Text = "删除";
            this.nbtDelete.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.nbtDelete.UseVisualStyleBackColor = true;
            // 
            // nbtQuery
            // 
            this.nbtQuery.Location = new System.Drawing.Point(443, 23);
            this.nbtQuery.Name = "nbtQuery";
            this.nbtQuery.Size = new System.Drawing.Size(75, 23);
            this.nbtQuery.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nbtQuery.TabIndex = 3;
            this.nbtQuery.Text = "查询";
            this.nbtQuery.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.nbtQuery.UseVisualStyleBackColor = true;
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(161, 28);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(11, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 5;
            this.neuLabel3.Text = "-";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(295, 28);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(41, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 4;
            this.neuLabel2.Text = "状态：";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(13, 28);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(41, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 3;
            this.neuLabel1.Text = "时间：";
            // 
            // ncmbState
            // 
            this.ncmbState.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.ncmbState.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ncmbState.FormattingEnabled = true;
            this.ncmbState.IsEnter2Tab = false;
            this.ncmbState.IsFlat = false;
            this.ncmbState.IsLike = true;
            this.ncmbState.IsListOnly = false;
            this.ncmbState.IsPopForm = true;
            this.ncmbState.IsShowCustomerList = false;
            this.ncmbState.IsShowID = false;
            this.ncmbState.IsShowIDAndName = false;
            this.ncmbState.Location = new System.Drawing.Point(342, 24);
            this.ncmbState.Name = "ncmbState";
            this.ncmbState.ShowCustomerList = false;
            this.ncmbState.ShowID = false;
            this.ncmbState.Size = new System.Drawing.Size(77, 20);
            this.ncmbState.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.ncmbState.TabIndex = 2;
            this.ncmbState.Tag = "";
            this.ncmbState.ToolBarUse = false;
            // 
            // neuDateTimePicker2
            // 
            this.neuDateTimePicker2.CustomFormat = "yyyy-MM-dd";
            this.neuDateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.neuDateTimePicker2.IsEnter2Tab = false;
            this.neuDateTimePicker2.Location = new System.Drawing.Point(178, 24);
            this.neuDateTimePicker2.Name = "neuDateTimePicker2";
            this.neuDateTimePicker2.Size = new System.Drawing.Size(94, 21);
            this.neuDateTimePicker2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuDateTimePicker2.TabIndex = 1;
            // 
            // neuDateTimePicker1
            // 
            this.neuDateTimePicker1.CustomFormat = "yyyy-MM-dd";
            this.neuDateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.neuDateTimePicker1.IsEnter2Tab = false;
            this.neuDateTimePicker1.Location = new System.Drawing.Point(60, 24);
            this.neuDateTimePicker1.Name = "neuDateTimePicker1";
            this.neuDateTimePicker1.Size = new System.Drawing.Size(95, 21);
            this.neuDateTimePicker1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuDateTimePicker1.TabIndex = 0;
            // 
            // neuGroupBox2
            // 
            this.neuGroupBox2.Controls.Add(this.nbtCancel);
            this.neuGroupBox2.Controls.Add(this.nbtOK);
            this.neuGroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuGroupBox2.Location = new System.Drawing.Point(0, 271);
            this.neuGroupBox2.Name = "neuGroupBox2";
            this.neuGroupBox2.Size = new System.Drawing.Size(638, 60);
            this.neuGroupBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox2.TabIndex = 1;
            this.neuGroupBox2.TabStop = false;
            // 
            // nbtCancel
            // 
            this.nbtCancel.Location = new System.Drawing.Point(446, 25);
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
            this.nbtOK.Location = new System.Drawing.Point(327, 25);
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
            this.neuDataListSpread.Location = new System.Drawing.Point(0, 57);
            this.neuDataListSpread.Name = "neuDataListSpread";
            this.neuDataListSpread.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuDataListSpread.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuDataListSpread_Sheet1});
            this.neuDataListSpread.Size = new System.Drawing.Size(638, 214);
            this.neuDataListSpread.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuDataListSpread.TabIndex = 7;
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
            this.neuDataListSpread_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247))))), System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(217)))), ((int)(((byte)(217))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, false);
            this.neuDataListSpread_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.neuDataListSpread_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuDataListSpread_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.neuDataListSpread_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuDataListSpread_Sheet1.DefaultStyle.Locked = false;
            this.neuDataListSpread_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.neuDataListSpread_Sheet1.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuDataListSpread_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
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
            this.neuDataListSpread.SetActiveViewport(0, 1, 1);
            // 
            // frmBillChooseListCanDelete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 331);
            this.Controls.Add(this.neuDataListSpread);
            this.Controls.Add(this.neuGroupBox2);
            this.Controls.Add(this.neuGroupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBillChooseListCanDelete";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "单据列表";
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.neuGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuDataListSpread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuDataListSpread_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox2;
        private FS.FrameWork.WinForms.Controls.NeuComboBox ncmbState;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker neuDateTimePicker2;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker neuDateTimePicker1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuButton nbtCancel;
        private FS.FrameWork.WinForms.Controls.NeuButton nbtOK;
        private FS.FrameWork.WinForms.Controls.NeuButton nbtQuery;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.SOC.Windows.Forms.FpSpread neuDataListSpread;
        private FarPoint.Win.Spread.SheetView neuDataListSpread_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuButton nbtDelete;
    }
}
