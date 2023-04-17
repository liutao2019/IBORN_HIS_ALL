namespace FS.HISFC.Components.Pharmacy.Base
{
    partial class ucCommonPopQuery
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
            this.pnlTop = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.pnlTopRight = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.btnClose = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.lblTopInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpMainList = new FarPoint.Win.Spread.SheetView();
            this.pnlMain = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.pnlBottom = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lblBottomInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.pnlTopCondition = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.btnQuery = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dtpEnd = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.dtpBegin = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.pnlTop.SuspendLayout();
            this.pnlTopRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpMainList)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.pnlTopCondition.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlTop.Controls.Add(this.pnlTopRight);
            this.pnlTop.Controls.Add(this.lblTopInfo);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(500, 30);
            this.pnlTop.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlTop.TabIndex = 1;
            // 
            // pnlTopRight
            // 
            this.pnlTopRight.Controls.Add(this.btnClose);
            this.pnlTopRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlTopRight.Location = new System.Drawing.Point(408, 0);
            this.pnlTopRight.Name = "pnlTopRight";
            this.pnlTopRight.Size = new System.Drawing.Size(88, 26);
            this.pnlTopRight.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlTopRight.TabIndex = 4;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(6, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "关闭页面";
            this.btnClose.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.neuButton1_Click);
            // 
            // lblTopInfo
            // 
            this.lblTopInfo.AutoSize = true;
            this.lblTopInfo.Location = new System.Drawing.Point(11, 9);
            this.lblTopInfo.Name = "lblTopInfo";
            this.lblTopInfo.Size = new System.Drawing.Size(89, 12);
            this.lblTopInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblTopInfo.TabIndex = 2;
            this.lblTopInfo.Text = "no information";
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, ";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(0, 0);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpMainList});
            this.neuSpread1.Size = new System.Drawing.Size(500, 210);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpMainList
            // 
            this.fpMainList.Reset();
            this.fpMainList.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpMainList.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpMainList.ColumnCount = 3;
            this.fpMainList.RowCount = 1;
            this.fpMainList.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.Navy, System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(213)))), ((int)(((byte)(213))))), FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213))))), System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0))))), System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(153))))), System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, true, true, true);
            this.fpMainList.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.fpMainList.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpMainList.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.fpMainList.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpMainList.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpMainList.DefaultStyle.ForeColor = System.Drawing.Color.Navy;
            this.fpMainList.RowHeader.Columns.Default.Resizable = false;
            this.fpMainList.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.fpMainList.RowHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpMainList.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.fpMainList.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpMainList.SheetCornerStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.fpMainList.SheetCornerStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.fpMainList.SheetCornerStyle.Parent = "CornerDefault";
            this.fpMainList.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpMainList.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.neuSpread1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 60);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(500, 210);
            this.pnlMain.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlMain.TabIndex = 4;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.lblBottomInfo);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 270);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(500, 30);
            this.pnlBottom.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlBottom.TabIndex = 5;
            // 
            // lblBottomInfo
            // 
            this.lblBottomInfo.AutoSize = true;
            this.lblBottomInfo.Location = new System.Drawing.Point(9, 9);
            this.lblBottomInfo.Name = "lblBottomInfo";
            this.lblBottomInfo.Size = new System.Drawing.Size(89, 12);
            this.lblBottomInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblBottomInfo.TabIndex = 2;
            this.lblBottomInfo.Text = "no information";
            // 
            // pnlTopCondition
            // 
            this.pnlTopCondition.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlTopCondition.Controls.Add(this.btnQuery);
            this.pnlTopCondition.Controls.Add(this.neuLabel2);
            this.pnlTopCondition.Controls.Add(this.neuLabel1);
            this.pnlTopCondition.Controls.Add(this.dtpEnd);
            this.pnlTopCondition.Controls.Add(this.dtpBegin);
            this.pnlTopCondition.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopCondition.Location = new System.Drawing.Point(0, 30);
            this.pnlTopCondition.Name = "pnlTopCondition";
            this.pnlTopCondition.Size = new System.Drawing.Size(500, 30);
            this.pnlTopCondition.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlTopCondition.TabIndex = 6;
            this.pnlTopCondition.Visible = false;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(379, 3);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnQuery.TabIndex = 5;
            this.btnQuery.Text = "查询";
            this.btnQuery.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.neuButton2_Click);
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(212, 10);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(17, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 4;
            this.neuLabel2.Text = "—";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(11, 10);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(53, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 3;
            this.neuLabel1.Text = "时间范围";
            // 
            // dtpEnd
            // 
            this.dtpEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd.IsEnter2Tab = false;
            this.dtpEnd.Location = new System.Drawing.Point(234, 4);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(139, 21);
            this.dtpEnd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpEnd.TabIndex = 1;
            // 
            // dtpBegin
            // 
            this.dtpBegin.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBegin.IsEnter2Tab = false;
            this.dtpBegin.Location = new System.Drawing.Point(70, 4);
            this.dtpBegin.Name = "dtpBegin";
            this.dtpBegin.Size = new System.Drawing.Size(139, 21);
            this.dtpBegin.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpBegin.TabIndex = 0;
            // 
            // ucCommonPopQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlTopCondition);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);
            this.Name = "ucCommonPopQuery";
            this.Size = new System.Drawing.Size(500, 300);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlTopRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpMainList)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.pnlTopCondition.ResumeLayout(false);
            this.pnlTopCondition.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel pnlTop;
        private FarPoint.Win.Spread.SheetView fpMainList;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblTopInfo;
        private FS.FrameWork.WinForms.Controls.NeuButton btnClose;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlMain;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlBottom;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblBottomInfo;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlTopRight;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlTopCondition;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuButton btnQuery;
        public FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpEnd;
        public FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpBegin;
        public FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
    }
}
