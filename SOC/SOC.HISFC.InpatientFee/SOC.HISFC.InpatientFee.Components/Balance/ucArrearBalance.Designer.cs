namespace FS.SOC.HISFC.InpatientFee.Components.Balance
{
    partial class ucArrearBalance
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
            FarPoint.Win.Spread.TipAppearance tipAppearance4 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType4 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType22 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType23 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType24 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType25 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType26 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType27 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType28 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.neuTabControl1 = new FS.FrameWork.WinForms.Controls.NeuTabControl();
            this.tpDetail = new System.Windows.Forms.TabPage();
            this.spDetail = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.spDetail_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.dtpEnd = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dtpBegin = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.jie = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ucQueryInpatientNo = new FS.HISFC.Components.Common.Controls.ucQueryInpatientNo();
            this.neuTabControl1.SuspendLayout();
            this.tpDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spDetail_Sheet1)).BeginInit();
            this.neuPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuTabControl1
            // 
            this.neuTabControl1.Controls.Add(this.tpDetail);
            this.neuTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuTabControl1.Location = new System.Drawing.Point(0, 41);
            this.neuTabControl1.Name = "neuTabControl1";
            this.neuTabControl1.SelectedIndex = 0;
            this.neuTabControl1.Size = new System.Drawing.Size(660, 411);
            this.neuTabControl1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTabControl1.TabIndex = 5;
            // 
            // tpDetail
            // 
            this.tpDetail.Controls.Add(this.spDetail);
            this.tpDetail.Location = new System.Drawing.Point(4, 22);
            this.tpDetail.Name = "tpDetail";
            this.tpDetail.Padding = new System.Windows.Forms.Padding(3);
            this.tpDetail.Size = new System.Drawing.Size(652, 385);
            this.tpDetail.TabIndex = 0;
            this.tpDetail.Text = "欠费结算记录";
            this.tpDetail.UseVisualStyleBackColor = true;
            // 
            // spDetail
            // 
            this.spDetail.About = "3.0.2004.2005";
            this.spDetail.AccessibleDescription = "spDetail, Sheet1, Row 0, Column 0, ";
            this.spDetail.BackColor = System.Drawing.Color.White;
            this.spDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spDetail.FileName = "";
            this.spDetail.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.spDetail.IsAutoSaveGridStatus = false;
            this.spDetail.IsCanCustomConfigColumn = false;
            this.spDetail.Location = new System.Drawing.Point(3, 3);
            this.spDetail.Name = "spDetail";
            this.spDetail.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.spDetail.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.spDetail_Sheet1});
            this.spDetail.Size = new System.Drawing.Size(646, 379);
            this.spDetail.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.spDetail.TabIndex = 0;
            tipAppearance4.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance4.ForeColor = System.Drawing.SystemColors.InfoText;
            this.spDetail.TextTipAppearance = tipAppearance4;
            this.spDetail.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // spDetail_Sheet1
            // 
            this.spDetail_Sheet1.Reset();
            this.spDetail_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.spDetail_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.spDetail_Sheet1.ColumnCount = 9;
            this.spDetail_Sheet1.RowCount = 10;
            this.spDetail_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.spDetail_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "选择";
            this.spDetail_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "住院号";
            this.spDetail_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "姓名";
            this.spDetail_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "出院科室";
            this.spDetail_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "结算类型";
            this.spDetail_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "发票金额";
            this.spDetail_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "欠费金额";
            this.spDetail_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "结算时间";
            this.spDetail_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "发票打印";
            this.spDetail_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.spDetail_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.spDetail_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.spDetail_Sheet1.Columns.Get(0).CellType = checkBoxCellType4;
            this.spDetail_Sheet1.Columns.Get(0).Label = "选择";
            this.spDetail_Sheet1.Columns.Get(0).Width = 36F;
            this.spDetail_Sheet1.Columns.Get(1).CellType = textCellType22;
            this.spDetail_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.spDetail_Sheet1.Columns.Get(1).Label = "住院号";
            this.spDetail_Sheet1.Columns.Get(1).Width = 98F;
            this.spDetail_Sheet1.Columns.Get(2).CellType = textCellType23;
            this.spDetail_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.spDetail_Sheet1.Columns.Get(2).Label = "姓名";
            this.spDetail_Sheet1.Columns.Get(2).Width = 85F;
            this.spDetail_Sheet1.Columns.Get(3).CellType = textCellType24;
            this.spDetail_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.spDetail_Sheet1.Columns.Get(3).Label = "出院科室";
            this.spDetail_Sheet1.Columns.Get(3).Width = 86F;
            this.spDetail_Sheet1.Columns.Get(4).CellType = textCellType25;
            this.spDetail_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.spDetail_Sheet1.Columns.Get(4).Label = "结算类型";
            this.spDetail_Sheet1.Columns.Get(4).Width = 58F;
            this.spDetail_Sheet1.Columns.Get(5).CellType = textCellType26;
            this.spDetail_Sheet1.Columns.Get(5).Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spDetail_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.spDetail_Sheet1.Columns.Get(5).Label = "发票金额";
            this.spDetail_Sheet1.Columns.Get(5).Width = 79F;
            this.spDetail_Sheet1.Columns.Get(6).CellType = textCellType27;
            this.spDetail_Sheet1.Columns.Get(6).Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spDetail_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.spDetail_Sheet1.Columns.Get(6).Label = "欠费金额";
            this.spDetail_Sheet1.Columns.Get(6).Width = 92F;
            this.spDetail_Sheet1.Columns.Get(7).CellType = textCellType28;
            this.spDetail_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.spDetail_Sheet1.Columns.Get(7).Label = "结算时间";
            this.spDetail_Sheet1.Columns.Get(7).Width = 172F;
            this.spDetail_Sheet1.Columns.Get(8).Label = "发票打印";
            this.spDetail_Sheet1.Columns.Get(8).Width = 100F;
            this.spDetail_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.spDetail_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.spDetail_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.spDetail_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.spDetail_Sheet1.Rows.Default.Height = 25F;
            this.spDetail_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.spDetail_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.spDetail_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.spDetail_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.ucQueryInpatientNo);
            this.neuPanel2.Controls.Add(this.dtpEnd);
            this.neuPanel2.Controls.Add(this.neuLabel1);
            this.neuPanel2.Controls.Add(this.dtpBegin);
            this.neuPanel2.Controls.Add(this.jie);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel2.Location = new System.Drawing.Point(0, 0);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(660, 41);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 4;
            // 
            // dtpEnd
            // 
            this.dtpEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd.IsEnter2Tab = false;
            this.dtpEnd.Location = new System.Drawing.Point(421, 9);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(137, 21);
            this.dtpEnd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpEnd.TabIndex = 3;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(395, 13);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(23, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 2;
            this.neuLabel1.Text = "到:";
            // 
            // dtpBegin
            // 
            this.dtpBegin.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBegin.IsEnter2Tab = false;
            this.dtpBegin.Location = new System.Drawing.Point(250, 9);
            this.dtpBegin.Name = "dtpBegin";
            this.dtpBegin.Size = new System.Drawing.Size(139, 21);
            this.dtpBegin.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpBegin.TabIndex = 2;
            // 
            // jie
            // 
            this.jie.AutoSize = true;
            this.jie.Location = new System.Drawing.Point(193, 13);
            this.jie.Name = "jie";
            this.jie.Size = new System.Drawing.Size(59, 12);
            this.jie.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.jie.TabIndex = 0;
            this.jie.Text = "结算时间:";
            // 
            // ucQueryInpatientNo
            // 
            this.ucQueryInpatientNo.DefaultInputType = 0;
            this.ucQueryInpatientNo.Font = new System.Drawing.Font("宋体", 9F);
            this.ucQueryInpatientNo.InputType = 0;
            this.ucQueryInpatientNo.IsDeptOnly = true;
            this.ucQueryInpatientNo.Location = new System.Drawing.Point(8, 3);
            this.ucQueryInpatientNo.Name = "ucQueryInpatientNo";
            this.ucQueryInpatientNo.PatientInState = "ALL";
            this.ucQueryInpatientNo.ShowState = FS.HISFC.Components.Common.Controls.enuShowState.All;
            this.ucQueryInpatientNo.Size = new System.Drawing.Size(179, 27);
            this.ucQueryInpatientNo.TabIndex = 1;
            // 
            // ucArrearBalance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuTabControl1);
            this.Controls.Add(this.neuPanel2);
            this.Name = "ucArrearBalance";
            this.Size = new System.Drawing.Size(660, 452);
            this.neuTabControl1.ResumeLayout(false);
            this.tpDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spDetail_Sheet1)).EndInit();
            this.neuPanel2.ResumeLayout(false);
            this.neuPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuTabControl neuTabControl1;
        private System.Windows.Forms.TabPage tpDetail;
        private FS.FrameWork.WinForms.Controls.NeuSpread spDetail;
        private FarPoint.Win.Spread.SheetView spDetail_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpEnd;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpBegin;
        private FS.FrameWork.WinForms.Controls.NeuLabel jie;
        protected FS.HISFC.Components.Common.Controls.ucQueryInpatientNo ucQueryInpatientNo;



    }
}
