namespace FS.SOC.HISFC.Components.NurseStation.Base
{
    partial class ucOrderExecFeeDetail
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
            this.ngbDetail = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.socFpEnter1 = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.socFpEnter1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.ngbDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.socFpEnter1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.socFpEnter1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // ngbDetail
            // 
            this.ngbDetail.Controls.Add(this.socFpEnter1);
            this.ngbDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ngbDetail.Location = new System.Drawing.Point(0, 0);
            this.ngbDetail.Name = "ngbDetail";
            this.ngbDetail.Size = new System.Drawing.Size(832, 498);
            this.ngbDetail.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ngbDetail.TabIndex = 20;
            this.ngbDetail.TabStop = false;
            this.ngbDetail.Text = "医嘱明细";
            // 
            // socFpEnter1
            // 
            this.socFpEnter1.About = "3.0.2004.2005";
            this.socFpEnter1.AccessibleDescription = "socFpEnter1, Sheet1, Row 0, Column 0, ";
            this.socFpEnter1.BackColor = System.Drawing.SystemColors.Control;
            this.socFpEnter1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.socFpEnter1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.socFpEnter1.Location = new System.Drawing.Point(3, 17);
            this.socFpEnter1.Name = "socFpEnter1";
            this.socFpEnter1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.socFpEnter1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.socFpEnter1_Sheet1});
            this.socFpEnter1.Size = new System.Drawing.Size(826, 478);
            this.socFpEnter1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.socFpEnter1.TabIndex = 3;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.socFpEnter1.TextTipAppearance = tipAppearance1;
            this.socFpEnter1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // socFpEnter1_Sheet1
            // 
            this.socFpEnter1_Sheet1.Reset();
            this.socFpEnter1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.socFpEnter1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.socFpEnter1_Sheet1.ColumnCount = 32;
            this.socFpEnter1_Sheet1.RowCount = 0;
            this.socFpEnter1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247))))), System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(217)))), ((int)(((byte)(217))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.socFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "医嘱类型";
            this.socFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "开始时间";
            this.socFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "名称";
            this.socFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "组";
            this.socFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "备注";
            this.socFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "用法";
            this.socFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "频次";
            this.socFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "每次量";
            this.socFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "结束时间";
            this.socFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "首日次数";
            this.socFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "末日次数";
            this.socFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "预分解到(时间)";
            this.socFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "理论总次数";
            this.socFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "已分解到(时间)";
            this.socFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 14).Value = "执行次数差";
            this.socFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 15).Value = "已收费到(时间)";
            this.socFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 16).Value = "收费次数差";
            this.socFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 17).Value = "已领药申请到(时间)";
            this.socFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 18).Value = "领药申请差";
            this.socFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 19).Value = "实际发药差";
            this.socFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 20).Value = "开立医生";
            this.socFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 21).Value = "开立时间";
            this.socFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 22).Value = "开立科室";
            this.socFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 23).Value = "扣库科室";
            this.socFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 24).Value = "审核人";
            this.socFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 25).Value = "停止人";
            this.socFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 26).Value = "停止时间";
            this.socFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 27).Value = "状态";
            this.socFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 28).Value = "皮试信息";
            this.socFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 29).Value = "加急";
            this.socFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 30).Value = "类别";
            this.socFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 31).Value = "扩展";
            this.socFpEnter1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.socFpEnter1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.socFpEnter1_Sheet1.ColumnHeader.Rows.Default.Height = 34F;
            this.socFpEnter1_Sheet1.Columns.Get(1).Label = "开始时间";
            this.socFpEnter1_Sheet1.Columns.Get(1).Width = 105F;
            this.socFpEnter1_Sheet1.Columns.Get(2).Label = "名称";
            this.socFpEnter1_Sheet1.Columns.Get(2).Width = 153F;
            this.socFpEnter1_Sheet1.Columns.Get(3).Label = "组";
            this.socFpEnter1_Sheet1.Columns.Get(3).Width = 24F;
            this.socFpEnter1_Sheet1.Columns.Get(6).Label = "频次";
            this.socFpEnter1_Sheet1.Columns.Get(6).Width = 56F;
            this.socFpEnter1_Sheet1.Columns.Get(8).Label = "结束时间";
            this.socFpEnter1_Sheet1.Columns.Get(8).Width = 101F;
            this.socFpEnter1_Sheet1.Columns.Get(9).Label = "首日次数";
            this.socFpEnter1_Sheet1.Columns.Get(9).Width = 34F;
            this.socFpEnter1_Sheet1.Columns.Get(10).Label = "末日次数";
            this.socFpEnter1_Sheet1.Columns.Get(10).Width = 35F;
            this.socFpEnter1_Sheet1.Columns.Get(11).Label = "预分解到(时间)";
            this.socFpEnter1_Sheet1.Columns.Get(11).Width = 92F;
            this.socFpEnter1_Sheet1.Columns.Get(12).Label = "理论总次数";
            this.socFpEnter1_Sheet1.Columns.Get(12).Width = 45F;
            this.socFpEnter1_Sheet1.Columns.Get(13).Label = "已分解到(时间)";
            this.socFpEnter1_Sheet1.Columns.Get(13).Width = 111F;
            this.socFpEnter1_Sheet1.Columns.Get(14).Label = "执行次数差";
            this.socFpEnter1_Sheet1.Columns.Get(14).Width = 52F;
            this.socFpEnter1_Sheet1.Columns.Get(15).Label = "已收费到(时间)";
            this.socFpEnter1_Sheet1.Columns.Get(15).Width = 103F;
            this.socFpEnter1_Sheet1.Columns.Get(16).Label = "收费次数差";
            this.socFpEnter1_Sheet1.Columns.Get(16).Width = 50F;
            this.socFpEnter1_Sheet1.Columns.Get(17).Label = "已领药申请到(时间)";
            this.socFpEnter1_Sheet1.Columns.Get(17).Width = 117F;
            this.socFpEnter1_Sheet1.Columns.Get(18).Label = "领药申请差";
            this.socFpEnter1_Sheet1.Columns.Get(18).Width = 47F;
            this.socFpEnter1_Sheet1.Columns.Get(19).Label = "实际发药差";
            this.socFpEnter1_Sheet1.Columns.Get(19).Width = 45F;
            this.socFpEnter1_Sheet1.DefaultStyle.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.socFpEnter1_Sheet1.DefaultStyle.Locked = false;
            this.socFpEnter1_Sheet1.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.socFpEnter1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.socFpEnter1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.socFpEnter1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.socFpEnter1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.socFpEnter1_Sheet1.Rows.Default.Height = 25F;
            this.socFpEnter1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.socFpEnter1_Sheet1.SheetCornerStyle.Locked = false;
            this.socFpEnter1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.socFpEnter1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.socFpEnter1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.socFpEnter1.SetActiveViewport(0, 1, 0);
            // 
            // ucOrderExecFeeDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ngbDetail);
            this.Name = "ucOrderExecFeeDetail";
            this.Size = new System.Drawing.Size(832, 498);
            this.ngbDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.socFpEnter1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.socFpEnter1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected FS.FrameWork.WinForms.Controls.NeuGroupBox ngbDetail;
        protected FS.SOC.Windows.Forms.FpSpread socFpEnter1;
        private FarPoint.Win.Spread.SheetView socFpEnter1_Sheet1;

    }
}
