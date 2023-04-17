namespace FS.HISFC.Components.Account.Forms
{
    partial class frmMedicalPackageDetail
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
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
            this.fpChildPackage = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpChildPackage_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.plBottom = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.btnDownTemplate = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnNewChildPackage = new System.Windows.Forms.Button();
            this.btnDeleteDetail = new System.Windows.Forms.Button();
            this.btnOut = new System.Windows.Forms.Button();
            this.neuGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpChildPackage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpChildPackage_Sheet1)).BeginInit();
            this.plBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
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
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(1161, 93);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 102;
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
            this.lblMemo.Size = new System.Drawing.Size(729, 25);
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
            this.fpChildPackage.Location = new System.Drawing.Point(0, 93);
            this.fpChildPackage.Name = "fpChildPackage";
            this.fpChildPackage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpChildPackage.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpChildPackage_Sheet1});
            this.fpChildPackage.Size = new System.Drawing.Size(1161, 485);
            this.fpChildPackage.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpChildPackage.TabIndex = 104;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpChildPackage.TextTipAppearance = tipAppearance2;
            this.fpChildPackage.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpChildPackage_Sheet1
            // 
            this.fpChildPackage_Sheet1.Reset();
            this.fpChildPackage_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpChildPackage_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpChildPackage_Sheet1.ColumnCount = 9;
            this.fpChildPackage_Sheet1.RowCount = 1;
            this.fpChildPackage_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "序号";
            this.fpChildPackage_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "编码";
            this.fpChildPackage_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "项目名称";
            this.fpChildPackage_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "次数";
            this.fpChildPackage_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "子项目编码";
            this.fpChildPackage_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "子项目名称";
            this.fpChildPackage_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "价格";
            this.fpChildPackage_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "备注";
            this.fpChildPackage_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "保存时验证";
            this.fpChildPackage_Sheet1.ColumnHeader.Columns.Default.VisualStyles = FarPoint.Win.VisualStyles.Auto;
            this.fpChildPackage_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(217)))), ((int)(((byte)(234)))));
            this.fpChildPackage_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F);
            this.fpChildPackage_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpChildPackage_Sheet1.ColumnHeader.Rows.Default.VisualStyles = FarPoint.Win.VisualStyles.Auto;
            this.fpChildPackage_Sheet1.ColumnHeader.Rows.Get(0).Height = 22F;
            this.fpChildPackage_Sheet1.Columns.Get(0).CellType = textCellType4;
            this.fpChildPackage_Sheet1.Columns.Get(0).Label = "序号";
            this.fpChildPackage_Sheet1.Columns.Get(0).Locked = true;
            this.fpChildPackage_Sheet1.Columns.Get(0).Width = 48F;
            this.fpChildPackage_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpChildPackage_Sheet1.Columns.Get(1).Label = "编码";
            this.fpChildPackage_Sheet1.Columns.Get(1).Locked = false;
            this.fpChildPackage_Sheet1.Columns.Get(1).Width = 122F;
            this.fpChildPackage_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpChildPackage_Sheet1.Columns.Get(2).Label = "项目名称";
            this.fpChildPackage_Sheet1.Columns.Get(2).Locked = false;
            this.fpChildPackage_Sheet1.Columns.Get(2).Width = 180F;
            this.fpChildPackage_Sheet1.Columns.Get(3).Label = "次数";
            this.fpChildPackage_Sheet1.Columns.Get(3).Locked = false;
            this.fpChildPackage_Sheet1.Columns.Get(3).Width = 96F;
            this.fpChildPackage_Sheet1.Columns.Get(4).CellType = textCellType5;
            this.fpChildPackage_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.fpChildPackage_Sheet1.Columns.Get(4).Label = "子项目编码";
            this.fpChildPackage_Sheet1.Columns.Get(4).Locked = false;
            this.fpChildPackage_Sheet1.Columns.Get(4).Width = 132F;
            this.fpChildPackage_Sheet1.Columns.Get(5).CellType = textCellType6;
            this.fpChildPackage_Sheet1.Columns.Get(5).Label = "子项目名称";
            this.fpChildPackage_Sheet1.Columns.Get(5).Locked = false;
            this.fpChildPackage_Sheet1.Columns.Get(5).Width = 122F;
            this.fpChildPackage_Sheet1.Columns.Get(6).Label = "价格";
            this.fpChildPackage_Sheet1.Columns.Get(6).Locked = false;
            this.fpChildPackage_Sheet1.Columns.Get(6).Width = 99F;
            this.fpChildPackage_Sheet1.Columns.Get(7).Label = "备注";
            this.fpChildPackage_Sheet1.Columns.Get(7).Locked = false;
            this.fpChildPackage_Sheet1.Columns.Get(7).Width = 144F;
            this.fpChildPackage_Sheet1.Columns.Get(8).Label = "保存时验证";
            this.fpChildPackage_Sheet1.Columns.Get(8).Locked = true;
            this.fpChildPackage_Sheet1.Columns.Get(8).Width = 180F;
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
            // plBottom
            // 
            this.plBottom.Controls.Add(this.btnOut);
            this.plBottom.Controls.Add(this.btnDownTemplate);
            this.plBottom.Controls.Add(this.btnImport);
            this.plBottom.Controls.Add(this.btnSave);
            this.plBottom.Controls.Add(this.btnNewChildPackage);
            this.plBottom.Controls.Add(this.btnDeleteDetail);
            this.plBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plBottom.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plBottom.Location = new System.Drawing.Point(0, 522);
            this.plBottom.Name = "plBottom";
            this.plBottom.Size = new System.Drawing.Size(1161, 56);
            this.plBottom.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plBottom.TabIndex = 105;
            // 
            // btnDownTemplate
            // 
            this.btnDownTemplate.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDownTemplate.ForeColor = System.Drawing.Color.Blue;
            this.btnDownTemplate.Location = new System.Drawing.Point(936, 11);
            this.btnDownTemplate.Name = "btnDownTemplate";
            this.btnDownTemplate.Size = new System.Drawing.Size(113, 35);
            this.btnDownTemplate.TabIndex = 7;
            this.btnDownTemplate.Text = "下载模板";
            this.btnDownTemplate.UseVisualStyleBackColor = true;
            this.btnDownTemplate.Click += new System.EventHandler(this.btnDownTemplate_Click);
            // 
            // btnImport
            // 
            this.btnImport.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnImport.ForeColor = System.Drawing.Color.Green;
            this.btnImport.Location = new System.Drawing.Point(633, 11);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(105, 35);
            this.btnImport.TabIndex = 6;
            this.btnImport.Text = "导入包明细";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.ForeColor = System.Drawing.Color.Blue;
            this.btnSave.Location = new System.Drawing.Point(243, 11);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(113, 35);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "保存明细包";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnNewChildPackage
            // 
            this.btnNewChildPackage.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnNewChildPackage.ForeColor = System.Drawing.Color.Green;
            this.btnNewChildPackage.Location = new System.Drawing.Point(16, 11);
            this.btnNewChildPackage.Name = "btnNewChildPackage";
            this.btnNewChildPackage.Size = new System.Drawing.Size(105, 35);
            this.btnNewChildPackage.TabIndex = 4;
            this.btnNewChildPackage.Text = "添加一行";
            this.btnNewChildPackage.UseVisualStyleBackColor = true;
            this.btnNewChildPackage.Click += new System.EventHandler(this.btnNewChildPackage_Click);
            // 
            // btnDeleteDetail
            // 
            this.btnDeleteDetail.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDeleteDetail.ForeColor = System.Drawing.Color.Red;
            this.btnDeleteDetail.Location = new System.Drawing.Point(127, 11);
            this.btnDeleteDetail.Name = "btnDeleteDetail";
            this.btnDeleteDetail.Size = new System.Drawing.Size(110, 35);
            this.btnDeleteDetail.TabIndex = 3;
            this.btnDeleteDetail.Text = "删除选中行";
            this.btnDeleteDetail.UseVisualStyleBackColor = true;
            this.btnDeleteDetail.Click += new System.EventHandler(this.btnDeleteDetail_Click);
            // 
            // btnOut
            // 
            this.btnOut.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOut.ForeColor = System.Drawing.Color.Green;
            this.btnOut.Location = new System.Drawing.Point(787, 11);
            this.btnOut.Name = "btnOut";
            this.btnOut.Size = new System.Drawing.Size(105, 35);
            this.btnOut.TabIndex = 8;
            this.btnOut.Text = "导出明细包";
            this.btnOut.UseVisualStyleBackColor = true;
            this.btnOut.Click += new System.EventHandler(this.btnOut_Click);
            // 
            // frmMedicalPackageDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1161, 578);
            this.Controls.Add(this.plBottom);
            this.Controls.Add(this.fpChildPackage);
            this.Controls.Add(this.neuGroupBox1);
            this.KeyPreview = true;
            this.Name = "frmMedicalPackageDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "明细包";
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpChildPackage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpChildPackage_Sheet1)).EndInit();
            this.plBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private System.Windows.Forms.Label lblCreateTime;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblMemo;
        private System.Windows.Forms.Label lblTotFee;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpChildPackage;
        private FarPoint.Win.Spread.SheetView fpChildPackage_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuPanel plBottom;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnNewChildPackage;
        private System.Windows.Forms.Button btnDeleteDetail;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnDownTemplate;
        private System.Windows.Forms.Button btnOut;
    }
}