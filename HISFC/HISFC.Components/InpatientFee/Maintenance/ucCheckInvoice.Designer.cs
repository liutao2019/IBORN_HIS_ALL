﻿namespace FS.HISFC.Components.InpatientFee.Maintenance
{
    partial class ucCheckInvoice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucCheckInvoice));
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType7 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType8 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.pltrv = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.tree = new FS.FrameWork.WinForms.Controls.NeuTreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.neuSplitter1 = new FS.FrameWork.WinForms.Controls.NeuSplitter();
            this.plMain = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.plDate = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.cbsky = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.lblSky = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dtEnd = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.lblEnd = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dtBegin = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.lblBegin = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.pltrv.SuspendLayout();
            this.plMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.plDate.SuspendLayout();
            this.SuspendLayout();
            // 
            // pltrv
            // 
            this.pltrv.Controls.Add(this.tree);
            this.pltrv.Dock = System.Windows.Forms.DockStyle.Left;
            this.pltrv.Location = new System.Drawing.Point(0, 0);
            this.pltrv.Name = "pltrv";
            this.pltrv.Size = new System.Drawing.Size(200, 527);
            this.pltrv.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pltrv.TabIndex = 0;
            // 
            // tree
            // 
            this.tree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tree.HideSelection = false;
            this.tree.ImageIndex = 2;
            this.tree.ImageList = this.imageList;
            this.tree.Location = new System.Drawing.Point(0, 0);
            this.tree.Name = "tree";
            this.tree.SelectedImageIndex = 2;
            this.tree.Size = new System.Drawing.Size(200, 527);
            this.tree.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tree.TabIndex = 0;
            this.tree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tree_NodeMouseClick);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "G2 Folder Blue.ico");
            this.imageList.Images.SetKeyName(1, "G2 Folder Grey.ico");
            this.imageList.Images.SetKeyName(2, "hourse.bmp");
            this.imageList.Images.SetKeyName(3, "hourse1.bmp");
            this.imageList.Images.SetKeyName(4, "36-3.bmp");
            this.imageList.Images.SetKeyName(5, "36-2.bmp");
            this.imageList.Images.SetKeyName(6, "47-1.gif");
            this.imageList.Images.SetKeyName(7, "47-2.gif");
            // 
            // neuSplitter1
            // 
            this.neuSplitter1.BackColor = System.Drawing.SystemColors.Desktop;
            this.neuSplitter1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.neuSplitter1.Location = new System.Drawing.Point(200, 0);
            this.neuSplitter1.Name = "neuSplitter1";
            this.neuSplitter1.Size = new System.Drawing.Size(3, 527);
            this.neuSplitter1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSplitter1.TabIndex = 1;
            this.neuSplitter1.TabStop = false;
            // 
            // plMain
            // 
            this.plMain.Controls.Add(this.neuSpread1);
            this.plMain.Controls.Add(this.plDate);
            this.plMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plMain.Location = new System.Drawing.Point(203, 0);
            this.plMain.Name = "plMain";
            this.plMain.Size = new System.Drawing.Size(689, 527);
            this.plMain.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plMain.TabIndex = 2;
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
            this.neuSpread1.Location = new System.Drawing.Point(0, 48);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(689, 479);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 1;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ColumnCount = 9;
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "是否核销";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "发票流水号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "印刷号";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "发票状态";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "金额";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "结算人";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "结算人姓名";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "结算时间";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "缴款时间";
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.Columns.Get(0).CellType = checkBoxCellType1;
            this.neuSpread1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "是否核销";
            this.neuSpread1_Sheet1.Columns.Get(0).Locked = false;
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 55F;
            this.neuSpread1_Sheet1.Columns.Get(1).CellType = textCellType1;
            this.neuSpread1_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "发票流水号";
            this.neuSpread1_Sheet1.Columns.Get(1).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 85F;
            this.neuSpread1_Sheet1.Columns.Get(2).CellType = textCellType2;
            this.neuSpread1_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "印刷号";
            this.neuSpread1_Sheet1.Columns.Get(2).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 85F;
            this.neuSpread1_Sheet1.Columns.Get(3).CellType = textCellType3;
            this.neuSpread1_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "发票状态";
            this.neuSpread1_Sheet1.Columns.Get(3).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 55F;
            this.neuSpread1_Sheet1.Columns.Get(4).CellType = textCellType4;
            this.neuSpread1_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "金额";
            this.neuSpread1_Sheet1.Columns.Get(4).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 71F;
            this.neuSpread1_Sheet1.Columns.Get(5).CellType = textCellType5;
            this.neuSpread1_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "结算人";
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 59F;
            this.neuSpread1_Sheet1.Columns.Get(6).CellType = textCellType6;
            this.neuSpread1_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(6).Label = "结算人姓名";
            this.neuSpread1_Sheet1.Columns.Get(6).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 71F;
            this.neuSpread1_Sheet1.Columns.Get(7).CellType = textCellType7;
            this.neuSpread1_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(7).Label = "结算时间";
            this.neuSpread1_Sheet1.Columns.Get(7).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(7).Width = 120F;
            this.neuSpread1_Sheet1.Columns.Get(8).CellType = textCellType8;
            this.neuSpread1_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(8).Label = "缴款时间";
            this.neuSpread1_Sheet1.Columns.Get(8).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get(8).Width = 120F;
            this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.SheetCornerStyle.Locked = false;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuSpread1.SetActiveViewport(0, 1, 0);
            // 
            // plDate
            // 
            this.plDate.Controls.Add(this.cbsky);
            this.plDate.Controls.Add(this.lblSky);
            this.plDate.Controls.Add(this.dtEnd);
            this.plDate.Controls.Add(this.lblEnd);
            this.plDate.Controls.Add(this.dtBegin);
            this.plDate.Controls.Add(this.lblBegin);
            this.plDate.Dock = System.Windows.Forms.DockStyle.Top;
            this.plDate.Location = new System.Drawing.Point(0, 0);
            this.plDate.Name = "plDate";
            this.plDate.Size = new System.Drawing.Size(689, 48);
            this.plDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plDate.TabIndex = 0;
            // 
            // cbsky
            // 
            this.cbsky.ArrowBackColor = System.Drawing.Color.Silver;
            this.cbsky.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cbsky.FormattingEnabled = true;
            this.cbsky.IsEnter2Tab = false;
            this.cbsky.IsFlat = false;
            this.cbsky.IsLike = true;
            this.cbsky.IsListOnly = false;
            this.cbsky.IsPopForm = true;
            this.cbsky.IsShowCustomerList = false;
            this.cbsky.IsShowID = false;
            this.cbsky.IsShowIDAndName = false;
            this.cbsky.Location = new System.Drawing.Point(509, 15);
            this.cbsky.Name = "cbsky";
            this.cbsky.ShowCustomerList = false;
            this.cbsky.ShowID = false;
            this.cbsky.Size = new System.Drawing.Size(144, 20);
            this.cbsky.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cbsky.TabIndex = 5;
            this.cbsky.Tag = "";
            this.cbsky.ToolBarUse = false;
            this.cbsky.SelectedIndexChanged += new System.EventHandler(this.cbsky_SelectedIndexChanged);
            // 
            // lblSky
            // 
            this.lblSky.AutoSize = true;
            this.lblSky.Location = new System.Drawing.Point(456, 20);
            this.lblSky.Name = "lblSky";
            this.lblSky.Size = new System.Drawing.Size(53, 12);
            this.lblSky.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblSky.TabIndex = 4;
            this.lblSky.Text = "收款员：";
            // 
            // dtEnd
            // 
            this.dtEnd.IsEnter2Tab = false;
            this.dtEnd.Location = new System.Drawing.Point(297, 15);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(134, 21);
            this.dtEnd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtEnd.TabIndex = 3;
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new System.Drawing.Point(227, 21);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(65, 12);
            this.lblEnd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblEnd.TabIndex = 2;
            this.lblEnd.Text = "终止时间：";
            // 
            // dtBegin
            // 
            this.dtBegin.IsEnter2Tab = false;
            this.dtBegin.Location = new System.Drawing.Point(70, 15);
            this.dtBegin.Name = "dtBegin";
            this.dtBegin.Size = new System.Drawing.Size(134, 21);
            this.dtBegin.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtBegin.TabIndex = 1;
            // 
            // lblBegin
            // 
            this.lblBegin.AutoSize = true;
            this.lblBegin.Location = new System.Drawing.Point(6, 21);
            this.lblBegin.Name = "lblBegin";
            this.lblBegin.Size = new System.Drawing.Size(65, 12);
            this.lblBegin.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblBegin.TabIndex = 0;
            this.lblBegin.Text = "开始时间：";
            // 
            // ucCheckInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.plMain);
            this.Controls.Add(this.neuSplitter1);
            this.Controls.Add(this.pltrv);
            this.Name = "ucCheckInvoice";
            this.Size = new System.Drawing.Size(892, 527);
            this.Load += new System.EventHandler(this.ucCheckInvoice_Load);
            this.pltrv.ResumeLayout(false);
            this.plMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.plDate.ResumeLayout(false);
            this.plDate.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel pltrv;
        private FS.FrameWork.WinForms.Controls.NeuTreeView tree;
        private FS.FrameWork.WinForms.Controls.NeuSplitter neuSplitter1;
        private FS.FrameWork.WinForms.Controls.NeuPanel plMain;
        private FS.FrameWork.WinForms.Controls.NeuPanel plDate;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtEnd;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblEnd;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtBegin;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblBegin;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        protected System.Windows.Forms.ImageList imageList;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cbsky;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblSky;
    }
}
