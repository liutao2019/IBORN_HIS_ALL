namespace FS.HISFC.Components.Order.Controls
{
    partial class ucPharmacyDictionary
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
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.fpSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.txtFilter = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLinkLabel1 = new FS.FrameWork.WinForms.Controls.NeuLinkLabel();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.pnlBottom = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.fpStorage = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpStorage_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.pnlMain = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.tbPhaInfo = new System.Windows.Forms.TabControl();
            this.tpDescription = new System.Windows.Forms.TabPage();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.neutext = new System.Windows.Forms.TextBox();
            this.neuSplitter1 = new FS.FrameWork.WinForms.Controls.NeuSplitter();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            this.neuPanel1.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpStorage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpStorage_Sheet1)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tbPhaInfo.SuspendLayout();
            this.tpDescription.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(23, 18);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(41, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "过滤：";
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "3.0.2004.2005";
            this.fpSpread1.AccessibleDescription = "fpSpread1, Sheet1, Row 0, Column 0, ";
            this.fpSpread1.BackColor = System.Drawing.Color.MintCream;
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread1.FileName = "";
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.IsAutoSaveGridStatus = false;
            this.fpSpread1.IsCanCustomConfigColumn = false;
            this.fpSpread1.Location = new System.Drawing.Point(0, 0);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1});
            this.fpSpread1.Size = new System.Drawing.Size(595, 136);
            this.fpSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpSpread1.TabIndex = 1;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread1.TextTipAppearance = tipAppearance1;
            this.fpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpSpread1_CellClick);
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.Rows.Default.Height = 25F;
            this.fpSpread1_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpSpread1_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSpread1_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.fpSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // txtFilter
            // 
            this.txtFilter.IsEnter2Tab = false;
            this.txtFilter.Location = new System.Drawing.Point(70, 15);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(172, 21);
            this.txtFilter.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtFilter.TabIndex = 2;
            // 
            // neuLinkLabel1
            // 
            this.neuLinkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.neuLinkLabel1.AutoSize = true;
            this.neuLinkLabel1.Location = new System.Drawing.Point(550, 18);
            this.neuLinkLabel1.Name = "neuLinkLabel1";
            this.neuLinkLabel1.Size = new System.Drawing.Size(29, 12);
            this.neuLinkLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLinkLabel1.TabIndex = 3;
            this.neuLinkLabel1.TabStop = true;
            this.neuLinkLabel1.Text = "设置";
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.neuLabel1);
            this.neuPanel1.Controls.Add(this.neuLinkLabel1);
            this.neuPanel1.Controls.Add(this.txtFilter);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(595, 52);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 4;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.fpStorage);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 345);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(595, 83);
            this.pnlBottom.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlBottom.TabIndex = 5;
            // 
            // fpStorage
            // 
            this.fpStorage.About = "3.0.2004.2005";
            this.fpStorage.AccessibleDescription = "fpStorage, Sheet1, Row 0, Column 0, ";
            this.fpStorage.BackColor = System.Drawing.Color.MintCream;
            this.fpStorage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpStorage.FileName = "";
            this.fpStorage.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpStorage.IsAutoSaveGridStatus = false;
            this.fpStorage.IsCanCustomConfigColumn = false;
            this.fpStorage.Location = new System.Drawing.Point(0, 0);
            this.fpStorage.Name = "fpStorage";
            this.fpStorage.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpStorage_Sheet1});
            this.fpStorage.Size = new System.Drawing.Size(595, 83);
            this.fpStorage.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpStorage.TabIndex = 2;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpStorage.TextTipAppearance = tipAppearance2;
            this.fpStorage.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpStorage_Sheet1
            // 
            this.fpStorage_Sheet1.Reset();
            this.fpStorage_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpStorage_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpStorage_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.SystemColors.Control, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Azure, System.Drawing.Color.White, false, false, false, true, true);
            this.fpStorage_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpStorage_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpStorage_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.fpStorage_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpStorage_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpStorage_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpStorage_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpStorage_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Controls.Add(this.splitter1);
            this.pnlMain.Controls.Add(this.tbPhaInfo);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 52);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(595, 293);
            this.pnlMain.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlMain.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.fpSpread1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(595, 136);
            this.panel1.TabIndex = 6;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 136);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(595, 4);
            this.splitter1.TabIndex = 5;
            this.splitter1.TabStop = false;
            // 
            // tbPhaInfo
            // 
            this.tbPhaInfo.Controls.Add(this.tpDescription);
            this.tbPhaInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbPhaInfo.Location = new System.Drawing.Point(0, 140);
            this.tbPhaInfo.Name = "tbPhaInfo";
            this.tbPhaInfo.SelectedIndex = 0;
            this.tbPhaInfo.Size = new System.Drawing.Size(595, 153);
            this.tbPhaInfo.TabIndex = 4;
            this.tbPhaInfo.Visible = false;
            // 
            // tpDescription
            // 
            this.tpDescription.Controls.Add(this.splitter2);
            this.tpDescription.Controls.Add(this.neutext);
            this.tpDescription.Location = new System.Drawing.Point(4, 22);
            this.tpDescription.Name = "tpDescription";
            this.tpDescription.Padding = new System.Windows.Forms.Padding(3);
            this.tpDescription.Size = new System.Drawing.Size(587, 127);
            this.tpDescription.TabIndex = 0;
            this.tpDescription.Text = "药品信息";
            this.tpDescription.UseVisualStyleBackColor = true;
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter2.Location = new System.Drawing.Point(3, 120);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(581, 4);
            this.splitter2.TabIndex = 6;
            this.splitter2.TabStop = false;
            // 
            // neutext
            // 
            this.neutext.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neutext.Location = new System.Drawing.Point(3, 3);
            this.neutext.Multiline = true;
            this.neutext.Name = "neutext";
            this.neutext.Size = new System.Drawing.Size(581, 121);
            this.neutext.TabIndex = 1;
            // 
            // neuSplitter1
            // 
            this.neuSplitter1.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.neuSplitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuSplitter1.Location = new System.Drawing.Point(0, 341);
            this.neuSplitter1.Name = "neuSplitter1";
            this.neuSplitter1.Size = new System.Drawing.Size(595, 4);
            this.neuSplitter1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSplitter1.TabIndex = 7;
            this.neuSplitter1.TabStop = false;
            this.neuSplitter1.DoubleClick += new System.EventHandler(this.neuSplitter1_DoubleClick);
            // 
            // ucPharmacyDictionary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.neuSplitter1);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucPharmacyDictionary";
            this.Size = new System.Drawing.Size(595, 428);
            this.Load += new System.EventHandler(this.ucPharmacyDictionary_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpStorage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpStorage_Sheet1)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tbPhaInfo.ResumeLayout(false);
            this.tpDescription.ResumeLayout(false);
            this.tpDescription.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtFilter;
        private FS.FrameWork.WinForms.Controls.NeuLinkLabel neuLinkLabel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlBottom;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlMain;
        private FS.FrameWork.WinForms.Controls.NeuSplitter neuSplitter1;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpStorage;
        private FarPoint.Win.Spread.SheetView fpStorage_Sheet1;
        private System.Windows.Forms.TabControl tbPhaInfo;
        private System.Windows.Forms.TabPage tpDescription;
        private System.Windows.Forms.TextBox neutext;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Panel panel1;
    }
}
