namespace FoShanYDSI.Controls
{
    partial class ucUpFeeDetail
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
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.gbTop = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.lblItemInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtPatientNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel7 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.fpItemInfo = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpItemInfo_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.gbTop.SuspendLayout();
            this.neuGroupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpItemInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpItemInfo_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // gbTop
            // 
            this.gbTop.Controls.Add(this.lblItemInfo);
            this.gbTop.Controls.Add(this.txtPatientNO);
            this.gbTop.Controls.Add(this.neuLabel7);
            this.gbTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbTop.Location = new System.Drawing.Point(0, 0);
            this.gbTop.Name = "gbTop";
            this.gbTop.Size = new System.Drawing.Size(1152, 86);
            this.gbTop.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gbTop.TabIndex = 0;
            this.gbTop.TabStop = false;
            this.gbTop.Text = "操作信息";
            // 
            // lblItemInfo
            // 
            this.lblItemInfo.AutoSize = true;
            this.lblItemInfo.Location = new System.Drawing.Point(18, 62);
            this.lblItemInfo.Name = "lblItemInfo";
            this.lblItemInfo.Size = new System.Drawing.Size(317, 12);
            this.lblItemInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblItemInfo.TabIndex = 27;
            this.lblItemInfo.Text = "交易流水号{0}，错误代码 {1}，错误信息 {2}，总行数{3}";
            // 
            // txtPatientNO
            // 
            this.txtPatientNO.IsEnter2Tab = false;
            this.txtPatientNO.Location = new System.Drawing.Point(69, 26);
            this.txtPatientNO.Name = "txtPatientNO";
            this.txtPatientNO.Size = new System.Drawing.Size(160, 21);
            this.txtPatientNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtPatientNO.TabIndex = 25;
            this.txtPatientNO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatientNO_KeyDown);
            // 
            // neuLabel7
            // 
            this.neuLabel7.AutoSize = true;
            this.neuLabel7.Location = new System.Drawing.Point(18, 31);
            this.neuLabel7.Name = "neuLabel7";
            this.neuLabel7.Size = new System.Drawing.Size(53, 12);
            this.neuLabel7.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel7.TabIndex = 26;
            this.neuLabel7.Text = "住院号：";
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.panel1);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 86);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(1152, 521);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 1;
            this.neuGroupBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.AllowDrop = true;
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.fpItemInfo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1146, 501);
            this.panel1.TabIndex = 28;
            // 
            // fpItemInfo
            // 
            this.fpItemInfo.About = "3.0.2004.2005";
            this.fpItemInfo.AccessibleDescription = "fpItemInfo, Sheet1, Row 0, Column 0, ";
            this.fpItemInfo.BackColor = System.Drawing.Color.White;
            this.fpItemInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpItemInfo.FileName = "";
            this.fpItemInfo.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpItemInfo.IsAutoSaveGridStatus = false;
            this.fpItemInfo.IsCanCustomConfigColumn = false;
            this.fpItemInfo.Location = new System.Drawing.Point(0, 0);
            this.fpItemInfo.Name = "fpItemInfo";
            this.fpItemInfo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpItemInfo.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpItemInfo_Sheet1});
            this.fpItemInfo.Size = new System.Drawing.Size(1146, 501);
            this.fpItemInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpItemInfo.TabIndex = 1;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpItemInfo.TextTipAppearance = tipAppearance1;
            this.fpItemInfo.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpItemInfo_Sheet1
            // 
            this.fpItemInfo_Sheet1.Reset();
            this.fpItemInfo_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpItemInfo_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpItemInfo_Sheet1.ColumnCount = 23;
            this.fpItemInfo_Sheet1.RowCount = 100;
            this.fpItemInfo_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "顺序号";
            this.fpItemInfo_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "就诊登记号";
            this.fpItemInfo_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "结算业务号";
            this.fpItemInfo_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "大类代码";
            this.fpItemInfo_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "大类名称";
            this.fpItemInfo_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "社保三个目录统一编码";
            this.fpItemInfo_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "社保三个目录名称";
            this.fpItemInfo_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "国家药品编码本位码";
            this.fpItemInfo_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "监控使用标志";
            this.fpItemInfo_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "医用材料的注册证产品名称";
            this.fpItemInfo_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "医用材料的食药监注册号";
            this.fpItemInfo_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "医疗机构三个目录编码";
            this.fpItemInfo_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "医疗机构三个目录名称";
            this.fpItemInfo_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "医院费用序列号";
            this.fpItemInfo_Sheet1.ColumnHeader.Cells.Get(0, 14).Value = "医疗费总额";
            this.fpItemInfo_Sheet1.ColumnHeader.Cells.Get(0, 15).Value = "自付比例";
            this.fpItemInfo_Sheet1.ColumnHeader.Cells.Get(0, 16).Value = "自付金额";
            this.fpItemInfo_Sheet1.ColumnHeader.Cells.Get(0, 17).Value = "本次就诊政策范围外个人自费金额";
            this.fpItemInfo_Sheet1.ColumnHeader.Cells.Get(0, 18).Value = "本次就诊政策范围内个人自付金额";
            this.fpItemInfo_Sheet1.ColumnHeader.Cells.Get(0, 19).Value = "允许报销部分";
            this.fpItemInfo_Sheet1.ColumnHeader.Cells.Get(0, 20).Value = "收费项目等级";
            this.fpItemInfo_Sheet1.ColumnHeader.Cells.Get(0, 21).Value = "数量";
            this.fpItemInfo_Sheet1.ColumnHeader.Cells.Get(0, 22).Value = "单价";
            this.fpItemInfo_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.fpItemInfo_Sheet1.Columns.Get(0).Label = "顺序号";
            this.fpItemInfo_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General;
            this.fpItemInfo_Sheet1.Columns.Get(0).Width = 53F;
            this.fpItemInfo_Sheet1.Columns.Get(1).CellType = textCellType1;
            this.fpItemInfo_Sheet1.Columns.Get(1).Label = "就诊登记号";
            this.fpItemInfo_Sheet1.Columns.Get(1).Locked = true;
            this.fpItemInfo_Sheet1.Columns.Get(1).Width = 77F;
            this.fpItemInfo_Sheet1.Columns.Get(2).CellType = textCellType1;
            this.fpItemInfo_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpItemInfo_Sheet1.Columns.Get(2).Label = "结算业务号";
            this.fpItemInfo_Sheet1.Columns.Get(2).Locked = true;
            this.fpItemInfo_Sheet1.Columns.Get(2).Width = 77F;
            this.fpItemInfo_Sheet1.Columns.Get(3).CellType = textCellType1;
            this.fpItemInfo_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpItemInfo_Sheet1.Columns.Get(3).Label = "大类代码";
            this.fpItemInfo_Sheet1.Columns.Get(3).Locked = true;
            this.fpItemInfo_Sheet1.Columns.Get(3).Width = 65F;
            this.fpItemInfo_Sheet1.Columns.Get(4).CellType = textCellType1;
            this.fpItemInfo_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpItemInfo_Sheet1.Columns.Get(4).Label = "大类名称";
            this.fpItemInfo_Sheet1.Columns.Get(4).Locked = true;
            this.fpItemInfo_Sheet1.Columns.Get(4).Width = 65F;
            this.fpItemInfo_Sheet1.Columns.Get(5).CellType = textCellType1;
            this.fpItemInfo_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpItemInfo_Sheet1.Columns.Get(5).Label = "社保三个目录统一编码";
            this.fpItemInfo_Sheet1.Columns.Get(5).Locked = true;
            this.fpItemInfo_Sheet1.Columns.Get(5).Width = 137F;
            this.fpItemInfo_Sheet1.Columns.Get(6).CellType = textCellType1;
            this.fpItemInfo_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpItemInfo_Sheet1.Columns.Get(6).Label = "社保三个目录名称";
            this.fpItemInfo_Sheet1.Columns.Get(6).Locked = true;
            this.fpItemInfo_Sheet1.Columns.Get(6).Width = 113F;
            this.fpItemInfo_Sheet1.Columns.Get(7).CellType = textCellType1;
            this.fpItemInfo_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpItemInfo_Sheet1.Columns.Get(7).Label = "国家药品编码本位码";
            this.fpItemInfo_Sheet1.Columns.Get(7).Locked = true;
            this.fpItemInfo_Sheet1.Columns.Get(7).Width = 125F;
            this.fpItemInfo_Sheet1.Columns.Get(8).CellType = textCellType1;
            this.fpItemInfo_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpItemInfo_Sheet1.Columns.Get(8).Label = "监控使用标志";
            this.fpItemInfo_Sheet1.Columns.Get(8).Locked = true;
            this.fpItemInfo_Sheet1.Columns.Get(8).Width = 89F;
            this.fpItemInfo_Sheet1.Columns.Get(9).CellType = textCellType1;
            this.fpItemInfo_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpItemInfo_Sheet1.Columns.Get(9).Label = "医用材料的注册证产品名称";
            this.fpItemInfo_Sheet1.Columns.Get(9).Locked = true;
            this.fpItemInfo_Sheet1.Columns.Get(9).Width = 161F;
            this.fpItemInfo_Sheet1.Columns.Get(10).Label = "医用材料的食药监注册号";
            this.fpItemInfo_Sheet1.Columns.Get(10).Width = 149F;
            this.fpItemInfo_Sheet1.Columns.Get(11).Label = "医疗机构三个目录编码";
            this.fpItemInfo_Sheet1.Columns.Get(11).Width = 137F;
            this.fpItemInfo_Sheet1.Columns.Get(12).Label = "医疗机构三个目录名称";
            this.fpItemInfo_Sheet1.Columns.Get(12).Width = 137F;
            this.fpItemInfo_Sheet1.Columns.Get(13).Label = "医院费用序列号";
            this.fpItemInfo_Sheet1.Columns.Get(13).Width = 101F;
            this.fpItemInfo_Sheet1.Columns.Get(14).Label = "医疗费总额";
            this.fpItemInfo_Sheet1.Columns.Get(14).Width = 77F;
            this.fpItemInfo_Sheet1.Columns.Get(15).Label = "自付比例";
            this.fpItemInfo_Sheet1.Columns.Get(15).Width = 65F;
            this.fpItemInfo_Sheet1.Columns.Get(16).Label = "自付金额";
            this.fpItemInfo_Sheet1.Columns.Get(16).Width = 65F;
            this.fpItemInfo_Sheet1.Columns.Get(17).Label = "本次就诊政策范围外个人自费金额";
            this.fpItemInfo_Sheet1.Columns.Get(17).Width = 197F;
            this.fpItemInfo_Sheet1.Columns.Get(18).Label = "本次就诊政策范围内个人自付金额";
            this.fpItemInfo_Sheet1.Columns.Get(18).Width = 197F;
            this.fpItemInfo_Sheet1.Columns.Get(19).Label = "允许报销部分";
            this.fpItemInfo_Sheet1.Columns.Get(19).Width = 89F;
            this.fpItemInfo_Sheet1.Columns.Get(20).Label = "收费项目等级";
            this.fpItemInfo_Sheet1.Columns.Get(20).Width = 89F;
            this.fpItemInfo_Sheet1.Columns.Get(21).Label = "数量";
            this.fpItemInfo_Sheet1.Columns.Get(21).Width = 41F;
            this.fpItemInfo_Sheet1.Columns.Get(22).Label = "单价";
            this.fpItemInfo_Sheet1.Columns.Get(22).Width = 41F;
            this.fpItemInfo_Sheet1.DefaultStyle.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpItemInfo_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpItemInfo_Sheet1.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpItemInfo_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpItemInfo_Sheet1.RowHeader.Columns.Get(0).Width = 30F;
            this.fpItemInfo_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ucUpFeeDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuGroupBox1);
            this.Controls.Add(this.gbTop);
            this.Name = "ucUpFeeDetail";
            this.Size = new System.Drawing.Size(1152, 607);
            this.gbTop.ResumeLayout(false);
            this.gbTop.PerformLayout();
            this.neuGroupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpItemInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpItemInfo_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox gbTop;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtPatientNO;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel7;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblItemInfo;
        private System.Windows.Forms.Panel panel1;
        private FS.FrameWork.WinForms.Controls.NeuSpread fpItemInfo;
        private FarPoint.Win.Spread.SheetView fpItemInfo_Sheet1;
    }
}
