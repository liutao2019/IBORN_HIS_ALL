namespace Neusoft.HISFC.Components.Manager.Controls
{
    partial class ucCaDepartmentManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucCaDepartmentManager));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("科室(0)");
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvDeptList1 = new Neusoft.HISFC.Components.Manager.Controls.tvCaDepartmentList(this.components);
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.neuTextBox1 = new Neusoft.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuButton1 = new Neusoft.FrameWork.WinForms.Controls.NeuButton();
            this.neuSpread1 = new Neusoft.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvDeptList1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(844, 493);
            this.splitContainer1.SplitterDistance = 199;
            this.splitContainer1.TabIndex = 0;
            // 
            // tvDeptList1
            // 
            this.tvDeptList1.alDepartments = ((System.Collections.ArrayList)(resources.GetObject("tvDeptList1.alDepartments")));
            this.tvDeptList1.Checked = Neusoft.HISFC.Components.Manager.Controls.tvCaDepartmentList.enuChecked.None;
            this.tvDeptList1.Direction = Neusoft.HISFC.Components.Manager.Controls.tvCaDepartmentList.enuShowDirection.Behind;
            this.tvDeptList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvDeptList1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tvDeptList1.ImageIndex = 0;
            this.tvDeptList1.IsShowCount = true;
            this.tvDeptList1.Location = new System.Drawing.Point(0, 0);
            this.tvDeptList1.Name = "tvDeptList1";
            treeNode1.Name = "";
            treeNode1.Text = "科室(0)";
            this.tvDeptList1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.tvDeptList1.SelectedImageIndex = 0;
            this.tvDeptList1.ShowNodeToolTips = true;
            this.tvDeptList1.ShowType = Neusoft.HISFC.Components.Manager.Controls.tvCaDepartmentList.enuShowType.None;
            this.tvDeptList1.Size = new System.Drawing.Size(199, 493);
            this.tvDeptList1.TabIndex = 0;
            this.tvDeptList1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvDeptList1_AfterSelect);
            this.tvDeptList1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvDeptList1_MouseDown);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.neuTextBox1);
            this.splitContainer2.Panel1.Controls.Add(this.neuButton1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.neuSpread1);
            this.splitContainer2.Size = new System.Drawing.Size(641, 493);
            this.splitContainer2.SplitterDistance = 38;
            this.splitContainer2.TabIndex = 1;
            // 
            // neuTextBox1
            // 
            this.neuTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.neuTextBox1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuTextBox1.IsEnter2Tab = false;
            this.neuTextBox1.Location = new System.Drawing.Point(3, 9);
            this.neuTextBox1.Name = "neuTextBox1";
            this.neuTextBox1.Size = new System.Drawing.Size(120, 26);
            this.neuTextBox1.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Flat;
            this.neuTextBox1.TabIndex = 2;
            // 
            // neuButton1
            // 
            this.neuButton1.BackColor = System.Drawing.SystemColors.Highlight;
            this.neuButton1.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.neuButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.neuButton1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuButton1.ForeColor = System.Drawing.Color.White;
            this.neuButton1.Location = new System.Drawing.Point(121, 9);
            this.neuButton1.Name = "neuButton1";
            this.neuButton1.Size = new System.Drawing.Size(80, 26);
            this.neuButton1.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Flat;
            this.neuButton1.TabIndex = 1;
            this.neuButton1.Text = "过滤一下";
            this.neuButton1.Type = Neusoft.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuButton1.UseVisualStyleBackColor = false;
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, ";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(0, 0);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(641, 451);
            this.neuSpread1.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.neuSpread1_CellDoubleClick);
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.SystemColors.Control, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.ColumnHeader.Rows.Get(0).Height = 37F;
            this.neuSpread1_Sheet1.Columns.Default.Width = 120F;
            this.neuSpread1_Sheet1.Columns.Get(0).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get(1).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get(2).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get(3).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get(4).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get(5).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get(6).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get(7).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get(8).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get(9).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get(10).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get(11).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get(12).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get(13).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get(14).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get(15).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get(16).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get(17).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get(18).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get(19).AllowAutoSort = true;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.neuSpread1_Sheet1.RowHeader.Columns.Get(0).AllowAutoSort = true;
            this.neuSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.RowHeader.Rows.Default.Resizable = false;
            this.neuSpread1_Sheet1.Rows.Default.Height = 40F;
            this.neuSpread1_Sheet1.Rows.Default.Resizable = false;
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.neuSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ucCaDepartmentManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ucCaDepartmentManager";
            this.Size = new System.Drawing.Size(844, 493);
            this.Load += new System.EventHandler(this.ucDepartmentManager_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        public Neusoft.HISFC.Components.Manager.Controls.tvCaDepartmentList tvDeptList1;
        public Neusoft.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        public FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private Neusoft.FrameWork.WinForms.Controls.NeuButton neuButton1;
        private Neusoft.FrameWork.WinForms.Controls.NeuTextBox neuTextBox1;
    }
}
