namespace FS.HISFC.Components.HealthRecord.CaseLend
{
    partial class ucNeedBackReport
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
            this.fpSpread_Sheet = new FarPoint.Win.Spread.SheetView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtCaseNO = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbPerson = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbDept = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radBack = new System.Windows.Forms.RadioButton();
            this.radAll = new System.Windows.Forms.RadioButton();
            this.radUnBack = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread_Sheet)).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(821, 506);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.fpSpread1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 97);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(821, 409);
            this.panel3.TabIndex = 1;
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "2.5.2007.2005";
            this.fpSpread1.AccessibleDescription = "fpSpread1, Sheet1";
            this.fpSpread1.BackColor = System.Drawing.Color.White;
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread1.Location = new System.Drawing.Point(0, 0);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread_Sheet});
            this.fpSpread1.Size = new System.Drawing.Size(821, 409);
            this.fpSpread1.TabIndex = 3;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread1.TextTipAppearance = tipAppearance1;
            this.fpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpSpread_Sheet
            // 
            this.fpSpread_Sheet.Reset();
            this.fpSpread_Sheet.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread_Sheet.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread_Sheet.ColumnCount = 9;
            this.fpSpread_Sheet.RowCount = 0;
            this.fpSpread_Sheet.RowHeader.ColumnCount = 0;
            this.fpSpread_Sheet.ColumnHeader.Cells.Get(0, 0).Value = "病案号";
            this.fpSpread_Sheet.ColumnHeader.Cells.Get(0, 1).Value = "姓名";
            this.fpSpread_Sheet.ColumnHeader.Cells.Get(0, 2).Value = "出院日期";
            this.fpSpread_Sheet.ColumnHeader.Cells.Get(0, 3).Value = "借阅人";
            this.fpSpread_Sheet.ColumnHeader.Cells.Get(0, 4).Value = "借阅人科室";
            this.fpSpread_Sheet.ColumnHeader.Cells.Get(0, 5).Value = "借阅日期";
            this.fpSpread_Sheet.ColumnHeader.Cells.Get(0, 6).Value = "借阅类型";
            this.fpSpread_Sheet.ColumnHeader.Cells.Get(0, 7).Value = "借阅天数";
            this.fpSpread_Sheet.ColumnHeader.Cells.Get(0, 8).Value = "归还情况";
            this.fpSpread_Sheet.Columns.Get(0).AllowAutoSort = true;
            this.fpSpread_Sheet.Columns.Get(0).Label = "病案号";
            this.fpSpread_Sheet.Columns.Get(0).Width = 79F;
            this.fpSpread_Sheet.Columns.Get(1).AllowAutoSort = true;
            this.fpSpread_Sheet.Columns.Get(1).Label = "姓名";
            this.fpSpread_Sheet.Columns.Get(1).Width = 82F;
            this.fpSpread_Sheet.Columns.Get(2).AllowAutoSort = true;
            this.fpSpread_Sheet.Columns.Get(2).Label = "出院日期";
            this.fpSpread_Sheet.Columns.Get(2).Width = 80F;
            this.fpSpread_Sheet.Columns.Get(3).AllowAutoSort = true;
            this.fpSpread_Sheet.Columns.Get(3).Label = "借阅人";
            this.fpSpread_Sheet.Columns.Get(3).Width = 76F;
            this.fpSpread_Sheet.Columns.Get(4).AllowAutoSort = true;
            this.fpSpread_Sheet.Columns.Get(4).Label = "借阅人科室";
            this.fpSpread_Sheet.Columns.Get(4).Width = 97F;
            this.fpSpread_Sheet.Columns.Get(5).AllowAutoSort = true;
            this.fpSpread_Sheet.Columns.Get(5).Label = "借阅日期";
            this.fpSpread_Sheet.Columns.Get(5).Width = 98F;
            this.fpSpread_Sheet.Columns.Get(6).AllowAutoSort = true;
            this.fpSpread_Sheet.Columns.Get(6).Label = "借阅类型";
            this.fpSpread_Sheet.Columns.Get(6).Width = 91F;
            this.fpSpread_Sheet.Columns.Get(7).AllowAutoSort = true;
            this.fpSpread_Sheet.Columns.Get(7).Label = "借阅天数";
            this.fpSpread_Sheet.Columns.Get(7).Width = 72F;
            this.fpSpread_Sheet.GrayAreaBackColor = System.Drawing.Color.White;
            this.fpSpread_Sheet.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpSpread_Sheet.RowHeader.Columns.Default.Resizable = false;
            this.fpSpread_Sheet.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpSpread_Sheet.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpSpread_Sheet.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpSpread1.SetActiveViewport(1, 0);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtCaseNO);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.groupBox3);
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(821, 97);
            this.panel2.TabIndex = 0;
            // 
            // txtCaseNO
            // 
            this.txtCaseNO.BackColor = System.Drawing.Color.Azure;
            this.txtCaseNO.IsEnter2Tab = false;
            this.txtCaseNO.Location = new System.Drawing.Point(115, 19);
            this.txtCaseNO.Name = "txtCaseNO";
            this.txtCaseNO.Size = new System.Drawing.Size(70, 21);
            this.txtCaseNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCaseNO.TabIndex = 103;
            this.txtCaseNO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCaseNO_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(516, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 108;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 102;
            this.label5.Text = "病案号(回车)";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.cmbPerson);
            this.groupBox3.Controls.Add(this.cmbDept);
            this.groupBox3.Location = new System.Drawing.Point(200, -2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(379, 52);
            this.groupBox3.TabIndex = 107;
            this.groupBox3.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 97;
            this.label7.Text = "借阅人";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(228, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 98;
            this.label6.Text = "科室";
            // 
            // cmbPerson
            // 
            this.cmbPerson.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbPerson.IsEnter2Tab = false;
            this.cmbPerson.IsFlat = false;
            this.cmbPerson.IsLike = true;
            this.cmbPerson.Location = new System.Drawing.Point(72, 21);
            this.cmbPerson.Name = "cmbPerson";
            this.cmbPerson.PopForm = null;
            this.cmbPerson.ShowCustomerList = false;
            this.cmbPerson.ShowID = false;
            this.cmbPerson.Size = new System.Drawing.Size(89, 20);
            this.cmbPerson.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbPerson.TabIndex = 99;
            this.cmbPerson.Tag = "";
            this.cmbPerson.ToolBarUse = false;
            // 
            // cmbDept
            // 
            this.cmbDept.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbDept.FormattingEnabled = true;
            this.cmbDept.IsEnter2Tab = false;
            this.cmbDept.IsFlat = true;
            this.cmbDept.IsLike = true;
            this.cmbDept.Location = new System.Drawing.Point(276, 22);
            this.cmbDept.Name = "cmbDept";
            this.cmbDept.PopForm = null;
            this.cmbDept.ShowCustomerList = false;
            this.cmbDept.ShowID = false;
            this.cmbDept.Size = new System.Drawing.Size(98, 20);
            this.cmbDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbDept.TabIndex = 100;
            this.cmbDept.Tag = "";
            this.cmbDept.ToolBarUse = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.dateTimePicker1);
            this.groupBox2.Controls.Add(this.dateTimePicker2);
            this.groupBox2.Location = new System.Drawing.Point(35, 51);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(443, 41);
            this.groupBox2.TabIndex = 106;
            this.groupBox2.TabStop = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(26, 20);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 108;
            this.checkBox1.Text = "借阅时间";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(235, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 105;
            this.label2.Text = "至";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Enabled = false;
            this.dateTimePicker1.Location = new System.Drawing.Point(104, 17);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(112, 21);
            this.dateTimePicker1.TabIndex = 102;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Enabled = false;
            this.dateTimePicker2.Location = new System.Drawing.Point(275, 17);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(115, 21);
            this.dateTimePicker2.TabIndex = 103;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radBack);
            this.groupBox1.Controls.Add(this.radAll);
            this.groupBox1.Controls.Add(this.radUnBack);
            this.groupBox1.Location = new System.Drawing.Point(585, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(194, 49);
            this.groupBox1.TabIndex = 101;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "共用条件";
            // 
            // radBack
            // 
            this.radBack.AutoSize = true;
            this.radBack.Location = new System.Drawing.Point(141, 21);
            this.radBack.Name = "radBack";
            this.radBack.Size = new System.Drawing.Size(47, 16);
            this.radBack.TabIndex = 2;
            this.radBack.Text = "已还";
            this.radBack.UseVisualStyleBackColor = true;
            // 
            // radAll
            // 
            this.radAll.AutoSize = true;
            this.radAll.Location = new System.Drawing.Point(71, 21);
            this.radAll.Name = "radAll";
            this.radAll.Size = new System.Drawing.Size(47, 16);
            this.radAll.TabIndex = 1;
            this.radAll.Text = "全部";
            this.radAll.UseVisualStyleBackColor = true;
            // 
            // radUnBack
            // 
            this.radUnBack.AutoSize = true;
            this.radUnBack.Checked = true;
            this.radUnBack.Location = new System.Drawing.Point(6, 21);
            this.radUnBack.Name = "radUnBack";
            this.radUnBack.Size = new System.Drawing.Size(47, 16);
            this.radUnBack.TabIndex = 0;
            this.radUnBack.TabStop = true;
            this.radUnBack.Text = "未还";
            this.radUnBack.UseVisualStyleBackColor = true;
            // 
            // ucNeedBackReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "ucNeedBackReport";
            this.Size = new System.Drawing.Size(821, 506);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread_Sheet)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDept;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbPerson;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private FarPoint.Win.Spread.FpSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView fpSpread_Sheet;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radAll;
        private System.Windows.Forms.RadioButton radUnBack;
        private System.Windows.Forms.RadioButton radBack;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtCaseNO;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
    }
}
