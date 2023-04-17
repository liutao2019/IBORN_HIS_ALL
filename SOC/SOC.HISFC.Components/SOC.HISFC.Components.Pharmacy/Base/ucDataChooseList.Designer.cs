namespace FS.SOC.HISFC.Components.Pharmacy.Base
{
    partial class ucDataChooseList
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
            this.ngbChooseList = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuPaneFilter = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.ntxtFilter = new System.Windows.Forms.TextBox();
            this.ncmbDrugType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.nlbDrugType = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nlbFilter = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.nllReset = new FS.FrameWork.WinForms.Controls.NeuLinkLabel();
            this.neuDataListSpread = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.neuDataListSpread_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.ngbChooseList.SuspendLayout();
            this.neuPaneFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuDataListSpread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuDataListSpread_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // ngbChooseList
            // 
            this.ngbChooseList.Controls.Add(this.neuDataListSpread);
            this.ngbChooseList.Controls.Add(this.neuPaneFilter);
            this.ngbChooseList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ngbChooseList.Location = new System.Drawing.Point(0, 0);
            this.ngbChooseList.Name = "ngbChooseList";
            this.ngbChooseList.Size = new System.Drawing.Size(331, 570);
            this.ngbChooseList.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ngbChooseList.TabIndex = 0;
            this.ngbChooseList.TabStop = false;
            this.ngbChooseList.Text = "药品列表";
            // 
            // neuPaneFilter
            // 
            this.neuPaneFilter.Controls.Add(this.ntxtFilter);
            this.neuPaneFilter.Controls.Add(this.ncmbDrugType);
            this.neuPaneFilter.Controls.Add(this.nlbDrugType);
            this.neuPaneFilter.Controls.Add(this.nlbFilter);
            this.neuPaneFilter.Controls.Add(this.nllReset);
            this.neuPaneFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPaneFilter.Location = new System.Drawing.Point(3, 17);
            this.neuPaneFilter.Name = "neuPaneFilter";
            this.neuPaneFilter.Size = new System.Drawing.Size(325, 37);
            this.neuPaneFilter.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPaneFilter.TabIndex = 5;
            // 
            // ntxtFilter
            // 
            this.ntxtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ntxtFilter.Location = new System.Drawing.Point(62, 9);
            this.ntxtFilter.Name = "ntxtFilter";
            this.ntxtFilter.Size = new System.Drawing.Size(116, 21);
            this.ntxtFilter.TabIndex = 7;
            // 
            // ncmbDrugType
            // 
            this.ncmbDrugType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ncmbDrugType.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.ncmbDrugType.FormattingEnabled = true;
            this.ncmbDrugType.IsEnter2Tab = false;
            this.ncmbDrugType.IsFlat = false;
            this.ncmbDrugType.IsLike = true;
            this.ncmbDrugType.IsListOnly = false;
            this.ncmbDrugType.IsPopForm = true;
            this.ncmbDrugType.IsShowCustomerList = false;
            this.ncmbDrugType.IsShowID = false;
            this.ncmbDrugType.Location = new System.Drawing.Point(237, 10);
            this.ncmbDrugType.Name = "ncmbDrugType";
            this.ncmbDrugType.PopForm = null;
            this.ncmbDrugType.ShowCustomerList = false;
            this.ncmbDrugType.ShowID = false;
            this.ncmbDrugType.Size = new System.Drawing.Size(67, 20);
            this.ncmbDrugType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.ncmbDrugType.TabIndex = 6;
            this.ncmbDrugType.Tag = "";
            this.ncmbDrugType.ToolBarUse = false;
            // 
            // nlbDrugType
            // 
            this.nlbDrugType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nlbDrugType.AutoSize = true;
            this.nlbDrugType.Location = new System.Drawing.Point(190, 14);
            this.nlbDrugType.Name = "nlbDrugType";
            this.nlbDrugType.Size = new System.Drawing.Size(41, 12);
            this.nlbDrugType.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbDrugType.TabIndex = 4;
            this.nlbDrugType.Text = "类别：";
            // 
            // nlbFilter
            // 
            this.nlbFilter.AutoSize = true;
            this.nlbFilter.Location = new System.Drawing.Point(3, 13);
            this.nlbFilter.Name = "nlbFilter";
            this.nlbFilter.Size = new System.Drawing.Size(53, 12);
            this.nlbFilter.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nlbFilter.TabIndex = 0;
            this.nlbFilter.Text = "过  滤：";
            // 
            // nllReset
            // 
            this.nllReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nllReset.AutoSize = true;
            this.nllReset.Location = new System.Drawing.Point(310, 14);
            this.nllReset.Name = "nllReset";
            this.nllReset.Size = new System.Drawing.Size(11, 12);
            this.nllReset.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.nllReset.TabIndex = 20;
            this.nllReset.TabStop = true;
            this.nllReset.Text = "R";
            // 
            // neuDataListSpread
            // 
            this.neuDataListSpread.About = "3.0.2004.2005";
            this.neuDataListSpread.AccessibleDescription = "neuDataListSpread, Sheet1, Row 0, Column 0, ";
            this.neuDataListSpread.BackColor = System.Drawing.SystemColors.Control;
            this.neuDataListSpread.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuDataListSpread.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuDataListSpread.Location = new System.Drawing.Point(3, 54);
            this.neuDataListSpread.Name = "neuDataListSpread";
            this.neuDataListSpread.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuDataListSpread.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuDataListSpread_Sheet1});
            this.neuDataListSpread.Size = new System.Drawing.Size(325, 513);
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
            // ucDataChooseList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ngbChooseList);
            this.Name = "ucDataChooseList";
            this.Size = new System.Drawing.Size(331, 570);
            this.ngbChooseList.ResumeLayout(false);
            this.neuPaneFilter.ResumeLayout(false);
            this.neuPaneFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuDataListSpread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuDataListSpread_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected FS.FrameWork.WinForms.Controls.NeuGroupBox ngbChooseList;
        protected FS.FrameWork.WinForms.Controls.NeuPanel neuPaneFilter;
        protected System.Windows.Forms.TextBox ntxtFilter;
        protected FS.FrameWork.WinForms.Controls.NeuComboBox ncmbDrugType;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbDrugType;
        protected FS.FrameWork.WinForms.Controls.NeuLabel nlbFilter;
        private FS.FrameWork.WinForms.Controls.NeuLinkLabel nllReset;
        protected FS.SOC.Windows.Forms.FpSpread neuDataListSpread;
        protected FarPoint.Win.Spread.SheetView neuDataListSpread_Sheet1;

    }
}
