namespace Neusoft.HISFC.Components.Manager
{
    partial class ucSubtblManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucSubtblManager));
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            this.imageList16 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvPatientList1 = new Neusoft.FrameWork.WinForms.Controls.NeuTreeView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.neuTabControl1 = new Neusoft.FrameWork.WinForms.Controls.NeuTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ucInputItem1 = new Neusoft.HISFC.Components.Common.Controls.ucInputItem();
            this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.neuTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList16
            // 
            this.imageList16.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList16.ImageStream")));
            this.imageList16.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList16.Images.SetKeyName(0, "");
            this.imageList16.Images.SetKeyName(1, "");
            this.imageList16.Images.SetKeyName(2, "");
            this.imageList16.Images.SetKeyName(3, "");
            this.imageList16.Images.SetKeyName(4, "");
            this.imageList16.Images.SetKeyName(5, "");
            this.imageList16.Images.SetKeyName(6, "");
            this.imageList16.Images.SetKeyName(7, "");
            this.imageList16.Images.SetKeyName(8, "");
            this.imageList16.Images.SetKeyName(9, "");
            this.imageList16.Images.SetKeyName(10, "");
            this.imageList16.Images.SetKeyName(11, "");
            this.imageList16.Images.SetKeyName(12, "");
            this.imageList16.Images.SetKeyName(13, "");
            this.imageList16.Images.SetKeyName(14, "");
            this.imageList16.Images.SetKeyName(15, "");
            this.imageList16.Images.SetKeyName(16, "");
            this.imageList16.Images.SetKeyName(17, "");
            this.imageList16.Images.SetKeyName(18, "");
            this.imageList16.Images.SetKeyName(19, "");
            this.imageList16.Images.SetKeyName(20, "");
            this.imageList16.Images.SetKeyName(21, "");
            this.imageList16.Images.SetKeyName(22, "");
            this.imageList16.Images.SetKeyName(23, "");
            this.imageList16.Images.SetKeyName(24, "");
            this.imageList16.Images.SetKeyName(25, "");
            this.imageList16.Images.SetKeyName(26, "");
            this.imageList16.Images.SetKeyName(27, "");
            this.imageList16.Images.SetKeyName(28, "");
            this.imageList16.Images.SetKeyName(29, "");
            this.imageList16.Images.SetKeyName(30, "");
            this.imageList16.Images.SetKeyName(31, "");
            this.imageList16.Images.SetKeyName(32, "");
            this.imageList16.Images.SetKeyName(33, "");
            this.imageList16.Images.SetKeyName(34, "");
            this.imageList16.Images.SetKeyName(35, "");
            this.imageList16.Images.SetKeyName(36, "");
            this.imageList16.Images.SetKeyName(37, "");
            this.imageList16.Images.SetKeyName(38, "");
            this.imageList16.Images.SetKeyName(39, "");
            this.imageList16.Images.SetKeyName(40, "");
            this.imageList16.Images.SetKeyName(41, "");
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel1.Controls.Add(this.tvPatientList1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(649, 437);
            this.splitContainer1.SplitterDistance = 160;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 1;
            // 
            // tvPatientList1
            // 
            this.tvPatientList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvPatientList1.HideSelection = false;
            this.tvPatientList1.ImageIndex = 0;
            this.tvPatientList1.ImageList = this.imageList16;
            this.tvPatientList1.Location = new System.Drawing.Point(0, 0);
            this.tvPatientList1.Name = "tvPatientList1";
            this.tvPatientList1.SelectedImageIndex = 0;
            this.tvPatientList1.Size = new System.Drawing.Size(156, 433);
            this.tvPatientList1.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tvPatientList1.TabIndex = 0;
            this.tvPatientList1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvPatientList1_AfterSelect);
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.neuTabControl1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.fpSpread1);
            this.splitContainer2.Size = new System.Drawing.Size(488, 437);
            this.splitContainer2.SplitterDistance = 266;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 0;
            // 
            // neuTabControl1
            // 
            this.neuTabControl1.Controls.Add(this.tabPage1);
            this.neuTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuTabControl1.Location = new System.Drawing.Point(0, 0);
            this.neuTabControl1.Name = "neuTabControl1";
            this.neuTabControl1.SelectedIndex = 0;
            this.neuTabControl1.Size = new System.Drawing.Size(484, 262);
            this.neuTabControl1.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ucInputItem1);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(476, 237);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "用法";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ucInputItem1
            // 
            this.ucInputItem1.AlCatagory = ((System.Collections.ArrayList)(resources.GetObject("ucInputItem1.AlCatagory")));
            this.ucInputItem1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucInputItem1.DrugSendType = "A";
            this.ucInputItem1.FeeItem = ((Neusoft.FrameWork.Models.NeuObject)(resources.GetObject("ucInputItem1.FeeItem")));
            this.ucInputItem1.FontSize = null;
            this.ucInputItem1.InputType = 0;
            this.ucInputItem1.IsDeptUsedFlag = false;
            this.ucInputItem1.IsIncludeMat = false;
            this.ucInputItem1.IsListShowAlways = true;
            this.ucInputItem1.IsShowCategory = false;
            this.ucInputItem1.IsShowInput = true;
            this.ucInputItem1.IsShowSelfMark = true;
            this.ucInputItem1.Location = new System.Drawing.Point(3, 3);
            this.ucInputItem1.Name = "ucInputItem1";
            this.ucInputItem1.Patient = null;
            this.ucInputItem1.ShowCategory = Neusoft.HISFC.Components.Common.Controls.EnumCategoryType.SysClass;
            this.ucInputItem1.ShowItemType = Neusoft.HISFC.Components.Common.Controls.EnumShowItemType.Undrug;
            this.ucInputItem1.Size = new System.Drawing.Size(470, 231);
            this.ucInputItem1.TabIndex = 0;
            this.ucInputItem1.UndrugApplicabilityarea = Neusoft.HISFC.Components.Common.Controls.EnumUndrugApplicabilityarea.All;
            this.ucInputItem1.SelectedItem += new Neusoft.FrameWork.WinForms.Forms.SelectedItemHandler(this.ucInputItem1_SelectedItem);
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "3.0.2004.2005";
            this.fpSpread1.AccessibleDescription = "fpSpread1, Sheet1, Row 0, Column 0, ";
            this.fpSpread1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread1.Location = new System.Drawing.Point(0, 0);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1});
            this.fpSpread1.Size = new System.Drawing.Size(484, 166);
            this.fpSpread1.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread1.TextTipAppearance = tipAppearance1;
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.ColumnCount = 9;
            this.fpSpread1_Sheet1.RowCount = 1;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "范围";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "科室";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "项目";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "数量";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "组范围";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "收取规则";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "限制";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "操作员";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "操作时间";
            this.fpSpread1_Sheet1.Columns.Get(0).Label = "范围";
            this.fpSpread1_Sheet1.Columns.Get(0).Width = 40F;
            this.fpSpread1_Sheet1.Columns.Get(1).Label = "科室";
            this.fpSpread1_Sheet1.Columns.Get(1).Width = 95F;
            this.fpSpread1_Sheet1.Columns.Get(2).Label = "项目";
            this.fpSpread1_Sheet1.Columns.Get(2).Width = 235F;
            this.fpSpread1_Sheet1.Columns.Get(4).Label = "组范围";
            this.fpSpread1_Sheet1.Columns.Get(4).Width = 99F;
            this.fpSpread1_Sheet1.Columns.Get(5).Label = "收取规则";
            this.fpSpread1_Sheet1.Columns.Get(5).Width = 99F;
            this.fpSpread1_Sheet1.Columns.Get(7).Label = "操作员";
            this.fpSpread1_Sheet1.Columns.Get(7).Width = 64F;
            this.fpSpread1_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpSpread1_Sheet1.Columns.Get(8).Label = "操作时间";
            this.fpSpread1_Sheet1.Columns.Get(8).Width = 125F;
            this.fpSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 21F;
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ucSubtblManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ucSubtblManager";
            this.Size = new System.Drawing.Size(649, 437);
            this.Load += new System.EventHandler(this.ucSubtblManager_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.neuTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList16;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private Neusoft.FrameWork.WinForms.Controls.NeuTreeView tvPatientList1;
        private Neusoft.FrameWork.WinForms.Controls.NeuTabControl neuTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private Neusoft.HISFC.Components.Common.Controls.ucInputItem ucInputItem1;
        private FarPoint.Win.Spread.FpSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
    }
}
