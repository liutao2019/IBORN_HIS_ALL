namespace FS.SOC.Local.Pharmacy.ZhuHai.Print.ZDWY
{
    partial class ucPlanBill
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
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lblCurPur = new System.Windows.Forms.Label();
            this.lblCurDif = new System.Windows.Forms.Label();
            this.lblCurRet = new System.Windows.Forms.Label();
            this.lblOper = new System.Windows.Forms.Label();
            this.neuPanel5 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuFpEnter1 = new FS.FrameWork.WinForms.Controls.NeuFpEnter(this.components);
            this.neuFpEnter1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel4 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lblCheckDate = new System.Windows.Forms.Label();
            this.lblPage = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.neuPanel1.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            this.neuPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuFpEnter1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuFpEnter1_Sheet1)).BeginInit();
            this.neuPanel3.SuspendLayout();
            this.neuPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.BackColor = System.Drawing.Color.White;
            this.neuPanel1.Controls.Add(this.neuPanel2);
            this.neuPanel1.Controls.Add(this.neuPanel5);
            this.neuPanel1.Controls.Add(this.neuPanel3);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(819, 124);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 7;
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.lblCurPur);
            this.neuPanel2.Controls.Add(this.lblCurDif);
            this.neuPanel2.Controls.Add(this.lblCurRet);
            this.neuPanel2.Controls.Add(this.lblOper);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel2.Location = new System.Drawing.Point(0, 92);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(819, 32);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 17;
            // 
            // lblCurPur
            // 
            this.lblCurPur.AutoSize = true;
            this.lblCurPur.Font = new System.Drawing.Font("宋体", 10F);
            this.lblCurPur.Location = new System.Drawing.Point(12, 3);
            this.lblCurPur.Name = "lblCurPur";
            this.lblCurPur.Size = new System.Drawing.Size(77, 14);
            this.lblCurPur.TabIndex = 11;
            this.lblCurPur.Text = "买入金额：";
            this.lblCurPur.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCurDif
            // 
            this.lblCurDif.AutoSize = true;
            this.lblCurDif.Font = new System.Drawing.Font("宋体", 10F);
            this.lblCurDif.Location = new System.Drawing.Point(413, 3);
            this.lblCurDif.Name = "lblCurDif";
            this.lblCurDif.Size = new System.Drawing.Size(63, 14);
            this.lblCurDif.TabIndex = 12;
            this.lblCurDif.Text = "购零差：";
            this.lblCurDif.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCurDif.Visible = false;
            // 
            // lblCurRet
            // 
            this.lblCurRet.AutoSize = true;
            this.lblCurRet.Font = new System.Drawing.Font("宋体", 10F);
            this.lblCurRet.Location = new System.Drawing.Point(202, 3);
            this.lblCurRet.Name = "lblCurRet";
            this.lblCurRet.Size = new System.Drawing.Size(77, 14);
            this.lblCurRet.TabIndex = 13;
            this.lblCurRet.Text = "零售金额：";
            this.lblCurRet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCurRet.Visible = false;
            // 
            // lblOper
            // 
            this.lblOper.AutoSize = true;
            this.lblOper.Font = new System.Drawing.Font("宋体", 10F);
            this.lblOper.Location = new System.Drawing.Point(586, 3);
            this.lblOper.Name = "lblOper";
            this.lblOper.Size = new System.Drawing.Size(63, 14);
            this.lblOper.TabIndex = 8;
            this.lblOper.Text = "制单人：";
            this.lblOper.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // neuPanel5
            // 
            this.neuPanel5.Controls.Add(this.neuFpEnter1);
            this.neuPanel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel5.Location = new System.Drawing.Point(0, 68);
            this.neuPanel5.Name = "neuPanel5";
            this.neuPanel5.Size = new System.Drawing.Size(819, 24);
            this.neuPanel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel5.TabIndex = 16;
            // 
            // neuFpEnter1
            // 
            this.neuFpEnter1.About = "3.0.2004.2005";
            this.neuFpEnter1.AccessibleDescription = "neuFpEnter1, Sheet1, Row 0, Column 0, ";
            this.neuFpEnter1.BackColor = System.Drawing.Color.White;
            this.neuFpEnter1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuFpEnter1.EditModePermanent = true;
            this.neuFpEnter1.EditModeReplace = true;
            this.neuFpEnter1.Font = new System.Drawing.Font("宋体", 10F);
            this.neuFpEnter1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            this.neuFpEnter1.Location = new System.Drawing.Point(0, 0);
            this.neuFpEnter1.Name = "neuFpEnter1";
            this.neuFpEnter1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuFpEnter1.SelectNone = false;
            this.neuFpEnter1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuFpEnter1_Sheet1});
            this.neuFpEnter1.ShowListWhenOfFocus = false;
            this.neuFpEnter1.Size = new System.Drawing.Size(819, 24);
            this.neuFpEnter1.TabIndex = 8;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuFpEnter1.TextTipAppearance = tipAppearance1;
            this.neuFpEnter1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // neuFpEnter1_Sheet1
            // 
            this.neuFpEnter1_Sheet1.Reset();
            this.neuFpEnter1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuFpEnter1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuFpEnter1_Sheet1.ColumnCount = 11;
            this.neuFpEnter1_Sheet1.RowCount = 6;
            this.neuFpEnter1_Sheet1.RowHeader.ColumnCount = 0;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 0).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "药品代码";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 1).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "项目ID";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 2).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "名称";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 3).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "规格";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 4).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "库存数量";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 5).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "单位";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 6).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "买入价";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 7).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "零售价";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 8).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "预购量";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 9).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "预购金额";
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 10).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "审批量";
            this.neuFpEnter1_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuFpEnter1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuFpEnter1_Sheet1.Columns.Get(0).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.Columns.Get(0).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuFpEnter1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuFpEnter1_Sheet1.Columns.Get(0).Label = "药品代码";
            this.neuFpEnter1_Sheet1.Columns.Get(0).Width = 85F;
            this.neuFpEnter1_Sheet1.Columns.Get(1).BackColor = System.Drawing.Color.White;
            this.neuFpEnter1_Sheet1.Columns.Get(1).Font = new System.Drawing.Font("宋体", 10.5F);
            this.neuFpEnter1_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuFpEnter1_Sheet1.Columns.Get(1).Label = "项目ID";
            this.neuFpEnter1_Sheet1.Columns.Get(1).Width = 0F;
            this.neuFpEnter1_Sheet1.Columns.Get(2).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuFpEnter1_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuFpEnter1_Sheet1.Columns.Get(2).Label = "名称";
            this.neuFpEnter1_Sheet1.Columns.Get(2).Width = 218F;
            this.neuFpEnter1_Sheet1.Columns.Get(3).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuFpEnter1_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuFpEnter1_Sheet1.Columns.Get(3).Label = "规格";
            this.neuFpEnter1_Sheet1.Columns.Get(3).Width = 101F;
            this.neuFpEnter1_Sheet1.Columns.Get(4).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuFpEnter1_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuFpEnter1_Sheet1.Columns.Get(4).Label = "库存数量";
            this.neuFpEnter1_Sheet1.Columns.Get(4).Width = 70F;
            this.neuFpEnter1_Sheet1.Columns.Get(5).Font = new System.Drawing.Font("宋体", 10.5F);
            this.neuFpEnter1_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuFpEnter1_Sheet1.Columns.Get(5).Label = "单位";
            this.neuFpEnter1_Sheet1.Columns.Get(5).Width = 33F;
            this.neuFpEnter1_Sheet1.Columns.Get(6).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuFpEnter1_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuFpEnter1_Sheet1.Columns.Get(6).Label = "买入价";
            this.neuFpEnter1_Sheet1.Columns.Get(6).Width = 59F;
            this.neuFpEnter1_Sheet1.Columns.Get(7).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuFpEnter1_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuFpEnter1_Sheet1.Columns.Get(7).Label = "零售价";
            this.neuFpEnter1_Sheet1.Columns.Get(7).Visible = false;
            this.neuFpEnter1_Sheet1.Columns.Get(7).Width = 57F;
            this.neuFpEnter1_Sheet1.Columns.Get(8).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuFpEnter1_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuFpEnter1_Sheet1.Columns.Get(8).Label = "预购量";
            this.neuFpEnter1_Sheet1.Columns.Get(8).Width = 61F;
            this.neuFpEnter1_Sheet1.Columns.Get(9).Font = new System.Drawing.Font("宋体", 10.5F);
            this.neuFpEnter1_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuFpEnter1_Sheet1.Columns.Get(9).Label = "预购金额";
            this.neuFpEnter1_Sheet1.Columns.Get(9).Width = 81F;
            this.neuFpEnter1_Sheet1.Columns.Get(10).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuFpEnter1_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuFpEnter1_Sheet1.Columns.Get(10).Label = "审批量";
            this.neuFpEnter1_Sheet1.Columns.Get(10).Width = 69F;
            this.neuFpEnter1_Sheet1.DefaultStyle.Font = new System.Drawing.Font("宋体", 8F);
            this.neuFpEnter1_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.neuFpEnter1_Sheet1.PrintInfo.Footer = "";
            this.neuFpEnter1_Sheet1.PrintInfo.Header = "";
            this.neuFpEnter1_Sheet1.PrintInfo.JobName = "";
            this.neuFpEnter1_Sheet1.PrintInfo.Printer = "";
            this.neuFpEnter1_Sheet1.PrintInfo.UseMax = false;
            this.neuFpEnter1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.neuFpEnter1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuFpEnter1_Sheet1.RowHeader.Visible = false;
            this.neuFpEnter1_Sheet1.Rows.Default.Height = 25F;
            this.neuFpEnter1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuFpEnter1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuPanel3
            // 
            this.neuPanel3.Controls.Add(this.neuPanel4);
            this.neuPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel3.Location = new System.Drawing.Point(0, 0);
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size(819, 68);
            this.neuPanel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 15;
            // 
            // neuPanel4
            // 
            this.neuPanel4.Controls.Add(this.lblCheckDate);
            this.neuPanel4.Controls.Add(this.lblPage);
            this.neuPanel4.Controls.Add(this.lblTitle);
            this.neuPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel4.Location = new System.Drawing.Point(0, 0);
            this.neuPanel4.Name = "neuPanel4";
            this.neuPanel4.Size = new System.Drawing.Size(819, 67);
            this.neuPanel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel4.TabIndex = 0;
            // 
            // lblCheckDate
            // 
            this.lblCheckDate.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCheckDate.Location = new System.Drawing.Point(621, 46);
            this.lblCheckDate.Name = "lblCheckDate";
            this.lblCheckDate.Size = new System.Drawing.Size(181, 19);
            this.lblCheckDate.TabIndex = 38;
            this.lblCheckDate.Text = "日期:2010-05-01";
            this.lblCheckDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPage
            // 
            this.lblPage.Font = new System.Drawing.Font("宋体", 10F);
            this.lblPage.Location = new System.Drawing.Point(7, 18);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(69, 17);
            this.lblPage.TabIndex = 42;
            this.lblPage.Text = "/";
            this.lblPage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.Location = new System.Drawing.Point(229, 11);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(165, 16);
            this.lblTitle.TabIndex = 35;
            this.lblTitle.Text = "{0}药品计划单({1})";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucPlanBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucPlanBill";
            this.Size = new System.Drawing.Size(819, 124);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel2.ResumeLayout(false);
            this.neuPanel2.PerformLayout();
            this.neuPanel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuFpEnter1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuFpEnter1_Sheet1)).EndInit();
            this.neuPanel3.ResumeLayout(false);
            this.neuPanel4.ResumeLayout(false);
            this.neuPanel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel3;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel4;
        private System.Windows.Forms.Label lblPage;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCheckDate;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private System.Windows.Forms.Label lblCurPur;
        private System.Windows.Forms.Label lblCurDif;
        private System.Windows.Forms.Label lblCurRet;
        private System.Windows.Forms.Label lblOper;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel5;
        private FS.FrameWork.WinForms.Controls.NeuFpEnter neuFpEnter1;
        private FarPoint.Win.Spread.SheetView neuFpEnter1_Sheet1;
    }
}
