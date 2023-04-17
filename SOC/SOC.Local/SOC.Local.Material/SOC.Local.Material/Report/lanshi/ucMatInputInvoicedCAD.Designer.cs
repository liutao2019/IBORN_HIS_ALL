namespace FS.SOC.Local.Material.Report.lanshi
{
    partial class ucMatInputInvoicedCAD
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
            this.lblSto = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbStorage = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.plLeft.SuspendLayout();
            this.plRight.SuspendLayout();
            this.plQueryCondition.SuspendLayout();
            this.plMain.SuspendLayout();
            this.plTop.SuspendLayout();
            this.plBottom.SuspendLayout();
            this.plRightTop.SuspendLayout();
            this.plRightBottom.SuspendLayout();
            this.gbMid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // plLeft
            // 
            this.plLeft.Size = new System.Drawing.Size(0, 552);
            // 
            // plRight
            // 
            this.plRight.Location = new System.Drawing.Point(3, 5);
            this.plRight.Size = new System.Drawing.Size(838, 552);
            // 
            // plQueryCondition
            // 
            this.plQueryCondition.Size = new System.Drawing.Size(0, 33);
            // 
            // plTop
            // 
            this.plTop.Controls.Add(this.lblSto);
            this.plTop.Controls.Add(this.cmbStorage);
            this.plTop.Controls.SetChildIndex(this.cmbStorage, 0);
            this.plTop.Controls.SetChildIndex(this.lblSto, 0);
            this.plTop.Controls.SetChildIndex(this.dtpBeginTime, 0);
            this.plTop.Controls.SetChildIndex(this.neuLabel1, 0);
            this.plTop.Controls.SetChildIndex(this.neuLabel2, 0);
            this.plTop.Controls.SetChildIndex(this.dtpEndTime, 0);
            // 
            // slLeft
            // 
            this.slLeft.Location = new System.Drawing.Point(0, 5);
            // 
            // plLeftControl
            // 
            this.plLeftControl.Size = new System.Drawing.Size(0, 519);
            // 
            // plRightTop
            // 
            this.plRightTop.Size = new System.Drawing.Size(838, 409);
            // 
            // slTop
            // 
            this.slTop.Size = new System.Drawing.Size(838, 3);
            // 
            // plRightBottom
            // 
            this.plRightBottom.Size = new System.Drawing.Size(838, 140);
            // 
            // gbMid
            // 
            this.gbMid.Size = new System.Drawing.Size(830, 38);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(810, 9);
            // 
            // dtpBeginTime
            // 
            this.dtpBeginTime.Location = new System.Drawing.Point(73, 12);
            // 
            // neuLabel2
            // 
            this.neuLabel2.Location = new System.Drawing.Point(228, 16);
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.Location = new System.Drawing.Point(294, 12);
            // 
            // neuLabel1
            // 
            this.neuLabel1.Location = new System.Drawing.Point(9, 16);
            // 
            // neuSpread1
            // 
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, ";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(838, 409);
            // 
            // neuSpread2
            // 
            this.neuSpread2.Size = new System.Drawing.Size(830, 102);
            this.neuSpread2.ActiveSheetIndex = -1;
            // 
            // lblSto
            // 
            this.lblSto.AutoSize = true;
            this.lblSto.Location = new System.Drawing.Point(462, 16);
            this.lblSto.Name = "lblSto";
            this.lblSto.Size = new System.Drawing.Size(35, 12);
            this.lblSto.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblSto.TabIndex = 4;
            this.lblSto.Text = "库房:";
            // 
            // cmbStorage
            // 
            this.cmbStorage.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbStorage.FormattingEnabled = true;
            this.cmbStorage.IsEnter2Tab = false;
            this.cmbStorage.IsFlat = false;
            this.cmbStorage.IsLike = true;
            this.cmbStorage.IsListOnly = false;
            this.cmbStorage.IsPopForm = true;
            this.cmbStorage.IsShowCustomerList = false;
            this.cmbStorage.IsShowID = false;
            this.cmbStorage.Location = new System.Drawing.Point(503, 12);
            this.cmbStorage.Name = "cmbStorage";
            this.cmbStorage.PopForm = null;
            this.cmbStorage.ShowCustomerList = false;
            this.cmbStorage.ShowID = false;
            this.cmbStorage.Size = new System.Drawing.Size(121, 20);
            this.cmbStorage.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbStorage.TabIndex = 5;
            this.cmbStorage.Tag = "";
            this.cmbStorage.ToolBarUse = false;
            this.cmbStorage.SelectedIndexChanged += new System.EventHandler(this.cmbStorage_SelectedIndexChanged);
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.Cells.Get(0, 0).ColumnSpan = 12;
            this.neuSpread1_Sheet1.Cells.Get(0, 0).Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Cells.Get(0, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(0, 0).Value = "已付款分类汇总表";
            this.neuSpread1_Sheet1.Cells.Get(0, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(1, 0).ColumnSpan = 4;
            this.neuSpread1_Sheet1.Cells.Get(1, 4).ColumnSpan = 7;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.Columns.Default.Width = 80F;
            this.neuSpread1_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.Rows.Get(0).Height = 45F;
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.SheetCornerStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.neuSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ucMatInputInvoicedCAD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucMatInputInvoicedCAD";
            this.SvMain = this.neuSpread1_Sheet1;
            this.plLeft.ResumeLayout(false);
            this.plRight.ResumeLayout(false);
            this.plQueryCondition.ResumeLayout(false);
            this.plMain.ResumeLayout(false);
            this.plTop.ResumeLayout(false);
            this.plTop.PerformLayout();
            this.plBottom.ResumeLayout(false);
            this.plRightTop.ResumeLayout(false);
            this.plRightBottom.ResumeLayout(false);
            this.gbMid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuLabel lblSto;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbStorage;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
    }
}
