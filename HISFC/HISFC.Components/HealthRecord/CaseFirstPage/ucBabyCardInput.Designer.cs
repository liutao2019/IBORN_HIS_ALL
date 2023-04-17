namespace FS.HISFC.Components.HealthRecord.CaseFirstPage
{
    partial class ucBabyCardInput
    {
        /// <summary> 
        /// 必需的设计器变量。

        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private FS.FrameWork.WinForms.Controls.NeuFpEnter fpEnter1;
        private FarPoint.Win.Spread.SheetView fpEnter1_Sheet1;
        private System.Windows.Forms.ContextMenu contextMenu1;
        private System.Windows.Forms.MenuItem menuItem2;

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
            this.fpEnter1 = new FS.FrameWork.WinForms.Controls.NeuFpEnter(this.components);
            this.fpEnter1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cmbIsHaveBaby = new FS.HISFC.Components.HealthRecord.CaseFirstPage.CustomListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btDelete = new System.Windows.Forms.Button();
            this.btAdd = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.fpEnter1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpEnter1_Sheet1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // fpEnter1
            // 
            this.fpEnter1.About = "3.0.2004.2005";
            this.fpEnter1.AccessibleDescription = "fpEnter1, Sheet1, Row 0, Column 0, ";
            this.fpEnter1.BackColor = System.Drawing.SystemColors.Control;
            this.fpEnter1.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.fpEnter1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpEnter1.EditModePermanent = true;
            this.fpEnter1.EditModeReplace = true;
            this.fpEnter1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpEnter1.Location = new System.Drawing.Point(0, 0);
            this.fpEnter1.Name = "fpEnter1";
            this.fpEnter1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpEnter1.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            this.fpEnter1.SelectNone = false;
            this.fpEnter1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpEnter1_Sheet1});
            this.fpEnter1.ShowListWhenOfFocus = false;
            this.fpEnter1.Size = new System.Drawing.Size(800, 543);
            this.fpEnter1.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpEnter1.TextTipAppearance = tipAppearance1;
            this.fpEnter1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpEnter1_Sheet1
            // 
            this.fpEnter1_Sheet1.Reset();
            this.fpEnter1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpEnter1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpEnter1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.SystemColors.Control, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, true, true, true);
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "性别";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "分娩结果";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "体重(g)";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "转归";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "呼吸";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "院内感染次数";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "医院感染名称";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "ICD-10编码";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "抢救次数";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "抢救成功次数";
            this.fpEnter1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpEnter1_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpEnter1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpEnter1_Sheet1.ColumnHeader.Rows.Get(0).Height = 41F;
            this.fpEnter1_Sheet1.Columns.Get(2).Label = "体重(g)";
            this.fpEnter1_Sheet1.Columns.Get(2).Width = 62F;
            this.fpEnter1_Sheet1.Columns.Get(5).Label = "院内感染次数";
            this.fpEnter1_Sheet1.Columns.Get(5).Width = 113F;
            this.fpEnter1_Sheet1.Columns.Get(6).Label = "医院感染名称";
            this.fpEnter1_Sheet1.Columns.Get(6).Width = 85F;
            this.fpEnter1_Sheet1.Columns.Get(7).Label = "ICD-10编码";
            this.fpEnter1_Sheet1.Columns.Get(7).Width = 105F;
            this.fpEnter1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpEnter1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpEnter1_Sheet1.RowHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpEnter1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpEnter1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.fpEnter1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.fpEnter1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpEnter1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // contextMenu1
            // 
            this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2});
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 0;
            this.menuItem2.Text = "删除";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.cmbIsHaveBaby);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.btDelete);
            this.splitContainer1.Panel1.Controls.Add(this.btAdd);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.fpEnter1);
            this.splitContainer1.Size = new System.Drawing.Size(800, 600);
            this.splitContainer1.SplitterDistance = 53;
            this.splitContainer1.TabIndex = 1;
            // 
            // cmbIsHaveBaby
            // 
            this.cmbIsHaveBaby.EnterVisiable = true;
            this.cmbIsHaveBaby.IsFind = true;
            this.cmbIsHaveBaby.IsSelctNone = true;
            this.cmbIsHaveBaby.IsSendToNext = false;
            this.cmbIsHaveBaby.IsShowID = false;
            this.cmbIsHaveBaby.ItemText = "";
            this.cmbIsHaveBaby.ListBoxHeight = 100;
            this.cmbIsHaveBaby.ListBoxVisible = false;
            this.cmbIsHaveBaby.ListBoxWidth = 100;
            this.cmbIsHaveBaby.Location = new System.Drawing.Point(109, 17);
            this.cmbIsHaveBaby.MaxLength = 5;
            this.cmbIsHaveBaby.Name = "cmbIsHaveBaby";
            this.cmbIsHaveBaby.OmitFilter = true;
            this.cmbIsHaveBaby.SelectedItem = null;
            this.cmbIsHaveBaby.SelectNone = true;
            this.cmbIsHaveBaby.SetListFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbIsHaveBaby.ShowID = true;
            this.cmbIsHaveBaby.Size = new System.Drawing.Size(34, 21);
            this.cmbIsHaveBaby.TabIndex = 644;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(13, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 643;
            this.label1.Text = "有无妇婴卡信息";
            // 
            // btDelete
            // 
            this.btDelete.Location = new System.Drawing.Point(292, 14);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(77, 30);
            this.btDelete.TabIndex = 1;
            this.btDelete.Text = "删除行";
            this.btDelete.UseVisualStyleBackColor = true;
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // btAdd
            // 
            this.btAdd.Location = new System.Drawing.Point(389, 14);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(77, 30);
            this.btAdd.TabIndex = 0;
            this.btAdd.Text = "插入行";
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(195, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(77, 30);
            this.button1.TabIndex = 645;
            this.button1.Text = "增加行";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ucBabyCardInput
            // 
            this.ContextMenu = this.contextMenu1;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ucBabyCardInput";
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.ucBabyCardInput_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fpEnter1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpEnter1_Sheet1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.Button btAdd;
        private CustomListBox cmbIsHaveBaby;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}
