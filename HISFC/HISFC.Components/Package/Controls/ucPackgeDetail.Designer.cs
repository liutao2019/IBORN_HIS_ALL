namespace HISFC.Components.Package.Controls
{
    partial class ucPackgeDetail
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucPackgeDetail));
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.plTop = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.plBottomLeft = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.dgbChildPackage = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.spBottom = new FS.FrameWork.WinForms.Controls.NeuSplitter();
            this.plBottomRight = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.dgbPackageDetail = new System.Windows.Forms.DataGridView();
            this.DetailColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DetailColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DetailColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DetailColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DetailColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DetailColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DetailColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DetailColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DetailColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DetailColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DetailColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DetailColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DetailColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plBottom = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnNewChildPackage = new System.Windows.Forms.Button();
            this.btnDeleteDetail = new System.Windows.Forms.Button();
            this.ucPackageInfoEdit1 = new HISFC.Components.Package.Controls.ucPackageInfoEdit();
            this.ucPackageInfo1 = new HISFC.Components.Package.Controls.ucPackageInfo();
            this.pnlTop.SuspendLayout();
            this.plTop.SuspendLayout();
            this.plBottomLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgbChildPackage)).BeginInit();
            this.plBottomRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgbPackageDetail)).BeginInit();
            this.plBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1285, 26);
            this.pnlTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1285, 26);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "套餐详情";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // plTop
            // 
            this.plTop.Controls.Add(this.ucPackageInfo1);
            this.plTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.plTop.Location = new System.Drawing.Point(0, 26);
            this.plTop.Name = "plTop";
            this.plTop.Size = new System.Drawing.Size(1285, 125);
            this.plTop.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plTop.TabIndex = 2;
            // 
            // plBottomLeft
            // 
            this.plBottomLeft.Controls.Add(this.dgbChildPackage);
            this.plBottomLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.plBottomLeft.Location = new System.Drawing.Point(0, 151);
            this.plBottomLeft.Name = "plBottomLeft";
            this.plBottomLeft.Size = new System.Drawing.Size(301, 307);
            this.plBottomLeft.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plBottomLeft.TabIndex = 3;
            // 
            // dgbChildPackage
            // 
            this.dgbChildPackage.AllowUserToAddRows = false;
            this.dgbChildPackage.AllowUserToDeleteRows = false;
            this.dgbChildPackage.AllowUserToResizeColumns = false;
            this.dgbChildPackage.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgbChildPackage.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgbChildPackage.BackgroundColor = System.Drawing.Color.White;
            this.dgbChildPackage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgbChildPackage.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgbChildPackage.ColumnHeadersHeight = 30;
            this.dgbChildPackage.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgbChildPackage.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgbChildPackage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgbChildPackage.EnableHeadersVisualStyles = false;
            this.dgbChildPackage.Location = new System.Drawing.Point(0, 0);
            this.dgbChildPackage.MultiSelect = false;
            this.dgbChildPackage.Name = "dgbChildPackage";
            this.dgbChildPackage.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgbChildPackage.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgbChildPackage.RowHeadersVisible = false;
            this.dgbChildPackage.RowTemplate.Height = 30;
            this.dgbChildPackage.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgbChildPackage.Size = new System.Drawing.Size(301, 307);
            this.dgbChildPackage.TabIndex = 16;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column1.Frozen = true;
            this.Column1.HeaderText = "序号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 40;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column2.Frozen = true;
            this.Column2.HeaderText = "默认";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 40;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.HeaderText = "名称";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // spBottom
            // 
            this.spBottom.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.spBottom.Location = new System.Drawing.Point(301, 151);
            this.spBottom.Name = "spBottom";
            this.spBottom.Size = new System.Drawing.Size(3, 307);
            this.spBottom.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.spBottom.TabIndex = 4;
            this.spBottom.TabStop = false;
            // 
            // plBottomRight
            // 
            this.plBottomRight.Controls.Add(this.dgbPackageDetail);
            this.plBottomRight.Controls.Add(this.plBottom);
            this.plBottomRight.Controls.Add(this.ucPackageInfoEdit1);
            this.plBottomRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plBottomRight.Location = new System.Drawing.Point(304, 151);
            this.plBottomRight.Name = "plBottomRight";
            this.plBottomRight.Size = new System.Drawing.Size(981, 307);
            this.plBottomRight.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plBottomRight.TabIndex = 5;
            // 
            // dgbPackageDetail
            // 
            this.dgbPackageDetail.AllowUserToAddRows = false;
            this.dgbPackageDetail.AllowUserToDeleteRows = false;
            this.dgbPackageDetail.AllowUserToResizeColumns = false;
            this.dgbPackageDetail.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgbPackageDetail.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgbPackageDetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgbPackageDetail.BackgroundColor = System.Drawing.Color.White;
            this.dgbPackageDetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgbPackageDetail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgbPackageDetail.ColumnHeadersHeight = 30;
            this.dgbPackageDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DetailColumn1,
            this.DetailColumn2,
            this.DetailColumn3,
            this.DetailColumn4,
            this.DetailColumn5,
            this.DetailColumn6,
            this.DetailColumn7,
            this.DetailColumn8,
            this.DetailColumn9,
            this.DetailColumn10,
            this.DetailColumn11,
            this.DetailColumn12,
            this.DetailColumn13});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgbPackageDetail.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgbPackageDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgbPackageDetail.EnableHeadersVisualStyles = false;
            this.dgbPackageDetail.Location = new System.Drawing.Point(0, 171);
            this.dgbPackageDetail.MultiSelect = false;
            this.dgbPackageDetail.Name = "dgbPackageDetail";
            this.dgbPackageDetail.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgbPackageDetail.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgbPackageDetail.RowHeadersVisible = false;
            this.dgbPackageDetail.RowTemplate.Height = 30;
            this.dgbPackageDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgbPackageDetail.Size = new System.Drawing.Size(981, 80);
            this.dgbPackageDetail.TabIndex = 15;
            // 
            // DetailColumn1
            // 
            this.DetailColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DetailColumn1.DefaultCellStyle = dataGridViewCellStyle7;
            this.DetailColumn1.Frozen = true;
            this.DetailColumn1.HeaderText = "序号";
            this.DetailColumn1.Name = "DetailColumn1";
            this.DetailColumn1.ReadOnly = true;
            this.DetailColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DetailColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DetailColumn1.Width = 40;
            // 
            // DetailColumn2
            // 
            this.DetailColumn2.HeaderText = "项目编码";
            this.DetailColumn2.Name = "DetailColumn2";
            this.DetailColumn2.ReadOnly = true;
            this.DetailColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DetailColumn2.Width = 59;
            // 
            // DetailColumn3
            // 
            this.DetailColumn3.HeaderText = "项目名称";
            this.DetailColumn3.Name = "DetailColumn3";
            this.DetailColumn3.ReadOnly = true;
            this.DetailColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DetailColumn3.Width = 59;
            // 
            // DetailColumn4
            // 
            this.DetailColumn4.HeaderText = "执行科室";
            this.DetailColumn4.Name = "DetailColumn4";
            this.DetailColumn4.ReadOnly = true;
            this.DetailColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DetailColumn4.Width = 59;
            // 
            // DetailColumn5
            // 
            this.DetailColumn5.HeaderText = "项目单价";
            this.DetailColumn5.Name = "DetailColumn5";
            this.DetailColumn5.ReadOnly = true;
            this.DetailColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DetailColumn5.Width = 59;
            // 
            // DetailColumn6
            // 
            this.DetailColumn6.HeaderText = "项目数量";
            this.DetailColumn6.Name = "DetailColumn6";
            this.DetailColumn6.ReadOnly = true;
            this.DetailColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DetailColumn6.Width = 59;
            // 
            // DetailColumn7
            // 
            this.DetailColumn7.HeaderText = "项目单位";
            this.DetailColumn7.Name = "DetailColumn7";
            this.DetailColumn7.ReadOnly = true;
            this.DetailColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DetailColumn7.Width = 59;
            // 
            // DetailColumn8
            // 
            this.DetailColumn8.HeaderText = "操作人";
            this.DetailColumn8.Name = "DetailColumn8";
            this.DetailColumn8.ReadOnly = true;
            this.DetailColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DetailColumn8.Width = 47;
            // 
            // DetailColumn9
            // 
            this.DetailColumn9.HeaderText = "操作日期";
            this.DetailColumn9.Name = "DetailColumn9";
            this.DetailColumn9.ReadOnly = true;
            this.DetailColumn9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DetailColumn9.Width = 59;
            // 
            // DetailColumn10
            // 
            this.DetailColumn10.HeaderText = "创建人";
            this.DetailColumn10.Name = "DetailColumn10";
            this.DetailColumn10.ReadOnly = true;
            this.DetailColumn10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DetailColumn10.Width = 47;
            // 
            // DetailColumn11
            // 
            this.DetailColumn11.HeaderText = "创建日期";
            this.DetailColumn11.Name = "DetailColumn11";
            this.DetailColumn11.ReadOnly = true;
            this.DetailColumn11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DetailColumn11.Width = 59;
            // 
            // DetailColumn12
            // 
            this.DetailColumn12.HeaderText = "备注";
            this.DetailColumn12.Name = "DetailColumn12";
            this.DetailColumn12.ReadOnly = true;
            this.DetailColumn12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DetailColumn12.Width = 35;
            // 
            // DetailColumn13
            // 
            this.DetailColumn13.HeaderText = "费用类别";
            this.DetailColumn13.Name = "DetailColumn13";
            this.DetailColumn13.ReadOnly = true;
            this.DetailColumn13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DetailColumn13.Width = 59;
            // 
            // plBottom
            // 
            this.plBottom.Controls.Add(this.btnSave);
            this.plBottom.Controls.Add(this.btnNewChildPackage);
            this.plBottom.Controls.Add(this.btnDeleteDetail);
            this.plBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plBottom.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plBottom.Location = new System.Drawing.Point(0, 251);
            this.plBottom.Name = "plBottom";
            this.plBottom.Size = new System.Drawing.Size(981, 56);
            this.plBottom.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plBottom.TabIndex = 14;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.ForeColor = System.Drawing.Color.Blue;
            this.btnSave.Location = new System.Drawing.Point(243, 11);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(113, 35);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "保存项目包";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnNewChildPackage
            // 
            this.btnNewChildPackage.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnNewChildPackage.ForeColor = System.Drawing.Color.Green;
            this.btnNewChildPackage.Location = new System.Drawing.Point(16, 11);
            this.btnNewChildPackage.Name = "btnNewChildPackage";
            this.btnNewChildPackage.Size = new System.Drawing.Size(105, 35);
            this.btnNewChildPackage.TabIndex = 4;
            this.btnNewChildPackage.Text = "创建新项目包";
            this.btnNewChildPackage.UseVisualStyleBackColor = true;
            // 
            // btnDeleteDetail
            // 
            this.btnDeleteDetail.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDeleteDetail.ForeColor = System.Drawing.Color.Red;
            this.btnDeleteDetail.Location = new System.Drawing.Point(127, 11);
            this.btnDeleteDetail.Name = "btnDeleteDetail";
            this.btnDeleteDetail.Size = new System.Drawing.Size(110, 35);
            this.btnDeleteDetail.TabIndex = 3;
            this.btnDeleteDetail.Text = "删除项目包明细";
            this.btnDeleteDetail.UseVisualStyleBackColor = true;
            // 
            // ucPackageInfoEdit1
            // 
            this.ucPackageInfoEdit1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.ucPackageInfoEdit1.CurrentChildPackage = null;
            this.ucPackageInfoEdit1.CurrentDetail = null;
            this.ucPackageInfoEdit1.CurrentPackage = null;
            this.ucPackageInfoEdit1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucPackageInfoEdit1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucPackageInfoEdit1.IsFullConvertToHalf = true;
            this.ucPackageInfoEdit1.IsPrint = false;
            this.ucPackageInfoEdit1.Location = new System.Drawing.Point(0, 0);
            this.ucPackageInfoEdit1.Name = "ucPackageInfoEdit1";
            this.ucPackageInfoEdit1.ParentFormToolBar = null;
            this.ucPackageInfoEdit1.Size = new System.Drawing.Size(981, 171);
            this.ucPackageInfoEdit1.TabIndex = 13;
            // 
            // ucPackageInfo1
            // 
            this.ucPackageInfo1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.ucPackageInfo1.CurrentPackage = null;
            this.ucPackageInfo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPackageInfo1.IsFullConvertToHalf = true;
            this.ucPackageInfo1.IsPrint = false;
            this.ucPackageInfo1.Location = new System.Drawing.Point(0, 0);
            this.ucPackageInfo1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ucPackageInfo1.Name = "ucPackageInfo1";
            this.ucPackageInfo1.Padding = new System.Windows.Forms.Padding(1);
            this.ucPackageInfo1.ParentFormToolBar = null;
            this.ucPackageInfo1.Size = new System.Drawing.Size(1285, 125);
            this.ucPackageInfo1.TabIndex = 2;
            // 
            // ucPackgeDetail
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.plBottomRight);
            this.Controls.Add(this.spBottom);
            this.Controls.Add(this.plBottomLeft);
            this.Controls.Add(this.plTop);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "ucPackgeDetail";
            this.Size = new System.Drawing.Size(1285, 458);
            this.pnlTop.ResumeLayout(false);
            this.plTop.ResumeLayout(false);
            this.plBottomLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgbChildPackage)).EndInit();
            this.plBottomRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgbPackageDetail)).EndInit();
            this.plBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private FS.FrameWork.WinForms.Controls.NeuPanel plTop;
        private ucPackageInfo ucPackageInfo1;
        private FS.FrameWork.WinForms.Controls.NeuPanel plBottomLeft;
        private System.Windows.Forms.DataGridView dgbChildPackage;
        private FS.FrameWork.WinForms.Controls.NeuSplitter spBottom;
        private FS.FrameWork.WinForms.Controls.NeuPanel plBottomRight;
        private System.Windows.Forms.DataGridView dgbPackageDetail;
        private FS.FrameWork.WinForms.Controls.NeuPanel plBottom;
        private System.Windows.Forms.Button btnDeleteDetail;
        private ucPackageInfoEdit ucPackageInfoEdit1;
        private System.Windows.Forms.Button btnNewChildPackage;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn DetailColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn DetailColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn DetailColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn DetailColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn DetailColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn DetailColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn DetailColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn DetailColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn DetailColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn DetailColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn DetailColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn DetailColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn DetailColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}
