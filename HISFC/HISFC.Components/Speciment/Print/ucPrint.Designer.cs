namespace FS.HISFC.Components.Speciment.Print
{
    partial class ucPrint
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.grpLabelRePrint = new System.Windows.Forms.GroupBox();
            this.cmbDis = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbtOrg = new System.Windows.Forms.RadioButton();
            this.rbtSubSpec = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbtSpec = new System.Windows.Forms.RadioButton();
            this.rbtOperRoom = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbPrinter = new System.Windows.Forms.ComboBox();
            this.txtBarCode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grpLabelRePrint.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpLabelRePrint
            // 
            this.grpLabelRePrint.Controls.Add(this.cmbDis);
            this.grpLabelRePrint.Controls.Add(this.label1);
            this.grpLabelRePrint.Controls.Add(this.groupBox3);
            this.grpLabelRePrint.Controls.Add(this.groupBox2);
            this.grpLabelRePrint.Controls.Add(this.label5);
            this.grpLabelRePrint.Controls.Add(this.cmbPrinter);
            this.grpLabelRePrint.Controls.Add(this.txtBarCode);
            this.grpLabelRePrint.Controls.Add(this.label4);
            this.grpLabelRePrint.Controls.Add(this.dtpEnd);
            this.grpLabelRePrint.Controls.Add(this.label3);
            this.grpLabelRePrint.Controls.Add(this.dtpStart);
            this.grpLabelRePrint.Controls.Add(this.label2);
            this.grpLabelRePrint.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpLabelRePrint.Location = new System.Drawing.Point(0, 0);
            this.grpLabelRePrint.Margin = new System.Windows.Forms.Padding(4);
            this.grpLabelRePrint.Name = "grpLabelRePrint";
            this.grpLabelRePrint.Padding = new System.Windows.Forms.Padding(4);
            this.grpLabelRePrint.Size = new System.Drawing.Size(933, 116);
            this.grpLabelRePrint.TabIndex = 0;
            this.grpLabelRePrint.TabStop = false;
            this.grpLabelRePrint.Text = "标签";
            // 
            // cmbDis
            // 
            this.cmbDis.FormattingEnabled = true;
            this.cmbDis.Items.AddRange(new object[] {
            "贝迪"});
            this.cmbDis.Location = new System.Drawing.Point(541, 46);
            this.cmbDis.Name = "cmbDis";
            this.cmbDis.Size = new System.Drawing.Size(72, 24);
            this.cmbDis.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(491, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 18;
            this.label1.Text = "病种:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbtOrg);
            this.groupBox3.Controls.Add(this.rbtSubSpec);
            this.groupBox3.Location = new System.Drawing.Point(50, 22);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 54);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "类型选择";
            // 
            // rbtOrg
            // 
            this.rbtOrg.AutoSize = true;
            this.rbtOrg.Location = new System.Drawing.Point(127, 25);
            this.rbtOrg.Name = "rbtOrg";
            this.rbtOrg.Size = new System.Drawing.Size(58, 20);
            this.rbtOrg.TabIndex = 4;
            this.rbtOrg.Text = "组织";
            this.rbtOrg.UseVisualStyleBackColor = true;
            this.rbtOrg.CheckedChanged += new System.EventHandler(this.rbtCheckedChanged);
            // 
            // rbtSubSpec
            // 
            this.rbtSubSpec.AutoSize = true;
            this.rbtSubSpec.Checked = true;
            this.rbtSubSpec.Location = new System.Drawing.Point(36, 25);
            this.rbtSubSpec.Name = "rbtSubSpec";
            this.rbtSubSpec.Size = new System.Drawing.Size(74, 20);
            this.rbtSubSpec.TabIndex = 1;
            this.rbtSubSpec.TabStop = true;
            this.rbtSubSpec.Text = "血标本";
            this.rbtSubSpec.UseVisualStyleBackColor = true;
            this.rbtSubSpec.CheckedChanged += new System.EventHandler(this.rbtCheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbtSpec);
            this.groupBox2.Controls.Add(this.rbtOperRoom);
            this.groupBox2.Location = new System.Drawing.Point(272, 22);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 54);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "操作地点";
            // 
            // rbtSpec
            // 
            this.rbtSpec.AutoSize = true;
            this.rbtSpec.Checked = true;
            this.rbtSpec.Location = new System.Drawing.Point(28, 25);
            this.rbtSpec.Name = "rbtSpec";
            this.rbtSpec.Size = new System.Drawing.Size(74, 20);
            this.rbtSpec.TabIndex = 15;
            this.rbtSpec.TabStop = true;
            this.rbtSpec.Text = "标本库";
            this.rbtSpec.UseVisualStyleBackColor = true;
            // 
            // rbtOperRoom
            // 
            this.rbtOperRoom.AutoSize = true;
            this.rbtOperRoom.Location = new System.Drawing.Point(120, 25);
            this.rbtOperRoom.Name = "rbtOperRoom";
            this.rbtOperRoom.Size = new System.Drawing.Size(74, 20);
            this.rbtOperRoom.TabIndex = 14;
            this.rbtOperRoom.Text = "手术室";
            this.rbtOperRoom.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(619, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 16);
            this.label5.TabIndex = 13;
            this.label5.Text = "打印机:";
            // 
            // cmbPrinter
            // 
            this.cmbPrinter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrinter.FormattingEnabled = true;
            this.cmbPrinter.Items.AddRange(new object[] {
            "贝迪"});
            this.cmbPrinter.Location = new System.Drawing.Point(689, 46);
            this.cmbPrinter.Name = "cmbPrinter";
            this.cmbPrinter.Size = new System.Drawing.Size(134, 24);
            this.cmbPrinter.TabIndex = 12;
            // 
            // txtBarCode
            // 
            this.txtBarCode.Location = new System.Drawing.Point(609, 86);
            this.txtBarCode.Name = "txtBarCode";
            this.txtBarCode.Size = new System.Drawing.Size(214, 26);
            this.txtBarCode.TabIndex = 11;
            this.txtBarCode.TextChanged += new System.EventHandler(this.txtBarCode_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(491, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "分装标本条码:";
            // 
            // dtpEnd
            // 
            this.dtpEnd.Location = new System.Drawing.Point(319, 84);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(137, 26);
            this.dtpEnd.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(297, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "-";
            // 
            // dtpStart
            // 
            this.dtpStart.Location = new System.Drawing.Point(154, 85);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(137, 26);
            this.dtpStart.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "操作时间:";
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "2.5.2007.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, ";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(0, 0);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(927, 428);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 1;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.AutoFilteredColumn += new FarPoint.Win.Spread.AutoFilteredColumnEventHandler(this.neuSpread1_AutoFilteredColumn);
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ColumnCount = 6;
            this.neuSpread1_Sheet1.RowCount = 1;
            this.neuSpread1_Sheet1.ActiveSkin = FarPoint.Win.Spread.DefaultSkins.Classic2;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "选择";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "源条形码";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "分装后条码";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "类型";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "病种";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "取材脏器";
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(105)))), ((int)(((byte)(107)))));
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.Columns.Get(0).CellType = checkBoxCellType1;
            this.neuSpread1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "选择";
            this.neuSpread1_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(1).AllowAutoFilter = true;
            this.neuSpread1_Sheet1.Columns.Get(1).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "源条形码";
            this.neuSpread1_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 112F;
            this.neuSpread1_Sheet1.Columns.Get(2).AllowAutoFilter = true;
            this.neuSpread1_Sheet1.Columns.Get(2).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "分装后条码";
            this.neuSpread1_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 186F;
            this.neuSpread1_Sheet1.Columns.Get(3).AllowAutoFilter = true;
            this.neuSpread1_Sheet1.Columns.Get(3).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "类型";
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 107F;
            this.neuSpread1_Sheet1.Columns.Get(4).AllowAutoFilter = true;
            this.neuSpread1_Sheet1.Columns.Get(4).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "病种";
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 98F;
            this.neuSpread1_Sheet1.Columns.Get(5).AllowAutoFilter = true;
            this.neuSpread1_Sheet1.Columns.Get(5).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "取材脏器";
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 121F;
            this.neuSpread1_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(105)))), ((int)(((byte)(107)))));
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(105)))), ((int)(((byte)(107)))));
            this.neuSpread1_Sheet1.SheetCornerStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.SheetCornerStyle.Locked = false;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkAll.Location = new System.Drawing.Point(3, 22);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(927, 20);
            this.chkAll.TabIndex = 11;
            this.chkAll.Text = "全选";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.chkAll);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 116);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(933, 473);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "请选择需要补打的数据";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.neuSpread1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 42);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(927, 428);
            this.panel1.TabIndex = 12;
            // 
            // ucPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpLabelRePrint);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ucPrint";
            this.Size = new System.Drawing.Size(933, 589);
            this.Load += new System.EventHandler(this.ucPrint_Load);
            this.grpLabelRePrint.ResumeLayout(false);
            this.grpLabelRePrint.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpLabelRePrint;
        private System.Windows.Forms.RadioButton rbtOrg;
        private System.Windows.Forms.RadioButton rbtSubSpec;
        private System.Windows.Forms.TextBox txtBarCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label label2;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbPrinter;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtSpec;
        private System.Windows.Forms.RadioButton rbtOperRoom;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmbDis;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
    }
}