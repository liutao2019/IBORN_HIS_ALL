namespace FS.HISFC.Components.Account.Controls.IBorn
{
    partial class ucAccountMedicalPackage
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
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.lblTitle = new System.Windows.Forms.Label();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.lblCreateTime = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblMemo = new System.Windows.Forms.Label();
            this.lblTotFee = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgbChildPackage = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fpChildPackage = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpChildPackage_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgbChildPackage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpChildPackage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpChildPackage_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(957, 26);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "套包详情";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.neuGroupBox1.Controls.Add(this.lblCreateTime);
            this.neuGroupBox1.Controls.Add(this.label14);
            this.neuGroupBox1.Controls.Add(this.lblStatus);
            this.neuGroupBox1.Controls.Add(this.label5);
            this.neuGroupBox1.Controls.Add(this.lblMemo);
            this.neuGroupBox1.Controls.Add(this.lblTotFee);
            this.neuGroupBox1.Controls.Add(this.lblName);
            this.neuGroupBox1.Controls.Add(this.label6);
            this.neuGroupBox1.Controls.Add(this.label2);
            this.neuGroupBox1.Controls.Add(this.label1);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 26);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(957, 93);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 101;
            this.neuGroupBox1.TabStop = false;
            // 
            // lblCreateTime
            // 
            this.lblCreateTime.AutoSize = true;
            this.lblCreateTime.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCreateTime.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblCreateTime.Location = new System.Drawing.Point(761, 41);
            this.lblCreateTime.Name = "lblCreateTime";
            this.lblCreateTime.Size = new System.Drawing.Size(143, 19);
            this.lblCreateTime.TabIndex = 23;
            this.lblCreateTime.Text = "2017-02-07 15:23:59";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label14.Location = new System.Drawing.Point(684, 41);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(74, 19);
            this.label14.TabIndex = 22;
            this.label14.Text = "创建日期：";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblStatus.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblStatus.Location = new System.Drawing.Point(761, 17);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(35, 19);
            this.lblStatus.TabIndex = 21;
            this.lblStatus.Text = "启用";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label5.Location = new System.Drawing.Point(684, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 19);
            this.label5.TabIndex = 20;
            this.label5.Text = "套包状态：";
            // 
            // lblMemo
            // 
            this.lblMemo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.lblMemo.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMemo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblMemo.Location = new System.Drawing.Point(86, 68);
            this.lblMemo.Name = "lblMemo";
            this.lblMemo.Size = new System.Drawing.Size(818, 25);
            this.lblMemo.TabIndex = 19;
            this.lblMemo.Text = "开院特惠酬宾套餐开院特惠酬宾套餐开院特惠酬宾套餐开院特惠酬宾套餐开院特惠酬宾套餐开院特惠酬宾套餐";
            // 
            // lblTotFee
            // 
            this.lblTotFee.AutoSize = true;
            this.lblTotFee.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTotFee.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblTotFee.Location = new System.Drawing.Point(86, 42);
            this.lblTotFee.Name = "lblTotFee";
            this.lblTotFee.Size = new System.Drawing.Size(89, 19);
            this.lblTotFee.TabIndex = 18;
            this.lblTotFee.Text = "￥689783.67";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("微软雅黑", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblName.Location = new System.Drawing.Point(86, 17);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(220, 19);
            this.lblName.TabIndex = 16;
            this.lblName.Text = "月子中心28天两室一厅豪华家庭套餐";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label6.Location = new System.Drawing.Point(6, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 19);
            this.label6.TabIndex = 15;
            this.label6.Text = "套包备注：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label2.Location = new System.Drawing.Point(6, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 19);
            this.label2.TabIndex = 13;
            this.label2.Text = "套包金额：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 19);
            this.label1.TabIndex = 12;
            this.label1.Text = "套包名称：";
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
            this.Column3});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgbChildPackage.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgbChildPackage.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgbChildPackage.EnableHeadersVisualStyles = false;
            this.dgbChildPackage.Location = new System.Drawing.Point(0, 119);
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
            this.dgbChildPackage.Size = new System.Drawing.Size(276, 466);
            this.dgbChildPackage.TabIndex = 102;
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
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.HeaderText = "名称";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // fpChildPackage
            // 
            this.fpChildPackage.About = "3.0.2004.2005";
            this.fpChildPackage.AccessibleDescription = "fpChildPackage, Sheet1, Row 0, Column 0, ";
            this.fpChildPackage.BackColor = System.Drawing.Color.White;
            this.fpChildPackage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpChildPackage.EditModePermanent = true;
            this.fpChildPackage.EditModeReplace = true;
            this.fpChildPackage.FileName = "";
            this.fpChildPackage.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpChildPackage.IsAutoSaveGridStatus = false;
            this.fpChildPackage.IsCanCustomConfigColumn = false;
            this.fpChildPackage.Location = new System.Drawing.Point(276, 119);
            this.fpChildPackage.Name = "fpChildPackage";
            this.fpChildPackage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpChildPackage.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpChildPackage_Sheet1});
            this.fpChildPackage.Size = new System.Drawing.Size(681, 466);
            this.fpChildPackage.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpChildPackage.TabIndex = 103;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpChildPackage.TextTipAppearance = tipAppearance1;
            this.fpChildPackage.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpChildPackage_Sheet1
            // 
            this.fpChildPackage_Sheet1.Reset();
            this.fpChildPackage_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpChildPackage_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpChildPackage_Sheet1.ColumnCount = 10;
            this.fpChildPackage_Sheet1.RowCount = 1;
            this.fpChildPackage_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "序号";
            this.fpChildPackage_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "编码";
            this.fpChildPackage_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "项目名称";
            this.fpChildPackage_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "次数";
            this.fpChildPackage_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "子项目编码";
            this.fpChildPackage_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "子项目名称";
            this.fpChildPackage_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "价格";
            this.fpChildPackage_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "备注";
            this.fpChildPackage_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "创建人";
            this.fpChildPackage_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "创建时间";
            this.fpChildPackage_Sheet1.ColumnHeader.Columns.Default.VisualStyles = FarPoint.Win.VisualStyles.Auto;
            this.fpChildPackage_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(217)))), ((int)(((byte)(234)))));
            this.fpChildPackage_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F);
            this.fpChildPackage_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpChildPackage_Sheet1.ColumnHeader.Rows.Default.VisualStyles = FarPoint.Win.VisualStyles.Auto;
            this.fpChildPackage_Sheet1.ColumnHeader.Rows.Get(0).Height = 22F;
            this.fpChildPackage_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.fpChildPackage_Sheet1.Columns.Get(0).Label = "序号";
            this.fpChildPackage_Sheet1.Columns.Get(0).Locked = true;
            this.fpChildPackage_Sheet1.Columns.Get(0).Width = 48F;
            this.fpChildPackage_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpChildPackage_Sheet1.Columns.Get(1).Label = "编码";
            this.fpChildPackage_Sheet1.Columns.Get(1).Locked = true;
            this.fpChildPackage_Sheet1.Columns.Get(1).Width = 122F;
            this.fpChildPackage_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpChildPackage_Sheet1.Columns.Get(2).Label = "项目名称";
            this.fpChildPackage_Sheet1.Columns.Get(2).Width = 180F;
            this.fpChildPackage_Sheet1.Columns.Get(3).Label = "次数";
            this.fpChildPackage_Sheet1.Columns.Get(3).Locked = true;
            this.fpChildPackage_Sheet1.Columns.Get(3).Width = 96F;
            this.fpChildPackage_Sheet1.Columns.Get(4).CellType = textCellType2;
            this.fpChildPackage_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.fpChildPackage_Sheet1.Columns.Get(4).Label = "子项目编码";
            this.fpChildPackage_Sheet1.Columns.Get(4).Locked = true;
            this.fpChildPackage_Sheet1.Columns.Get(4).Width = 132F;
            this.fpChildPackage_Sheet1.Columns.Get(5).CellType = textCellType3;
            this.fpChildPackage_Sheet1.Columns.Get(5).Label = "子项目名称";
            this.fpChildPackage_Sheet1.Columns.Get(5).Width = 122F;
            this.fpChildPackage_Sheet1.Columns.Get(6).CellType = textCellType4;
            this.fpChildPackage_Sheet1.Columns.Get(6).Label = "价格";
            this.fpChildPackage_Sheet1.Columns.Get(6).Width = 99F;
            this.fpChildPackage_Sheet1.Columns.Get(7).CellType = textCellType5;
            this.fpChildPackage_Sheet1.Columns.Get(7).Label = "备注";
            this.fpChildPackage_Sheet1.Columns.Get(7).Width = 108F;
            this.fpChildPackage_Sheet1.Columns.Get(8).Label = "创建人";
            this.fpChildPackage_Sheet1.Columns.Get(8).Width = 112F;
            this.fpChildPackage_Sheet1.Columns.Get(9).Label = "创建时间";
            this.fpChildPackage_Sheet1.Columns.Get(9).Width = 103F;
            this.fpChildPackage_Sheet1.DefaultStyle.Font = new System.Drawing.Font("宋体", 10.5F);
            this.fpChildPackage_Sheet1.DefaultStyle.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpChildPackage_Sheet1.DefaultStyle.Locked = true;
            this.fpChildPackage_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpChildPackage_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.fpChildPackage_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpChildPackage_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpChildPackage_Sheet1.RowHeader.Columns.Get(0).Width = 19F;
            this.fpChildPackage_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(217)))), ((int)(((byte)(234)))));
            this.fpChildPackage_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpChildPackage_Sheet1.Rows.Default.Height = 22F;
            this.fpChildPackage_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpChildPackage_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ucAccountMedicalPackage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fpChildPackage);
            this.Controls.Add(this.dgbChildPackage);
            this.Controls.Add(this.neuGroupBox1);
            this.Controls.Add(this.lblTitle);
            this.Name = "ucAccountMedicalPackage";
            this.Size = new System.Drawing.Size(957, 585);
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgbChildPackage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpChildPackage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpChildPackage_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private System.Windows.Forms.DataGridView dgbChildPackage;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpChildPackage;
        private FarPoint.Win.Spread.SheetView fpChildPackage_Sheet1;
        private System.Windows.Forms.Label lblMemo;
        private System.Windows.Forms.Label lblTotFee;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCreateTime;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;

    }
}
