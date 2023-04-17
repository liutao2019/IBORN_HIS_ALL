namespace FS.HISFC.Components.Speciment.Report
{
    partial class ucQuerySpecInfo
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
            this.cmbDis = new System.Windows.Forms.ComboBox();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbtOrg = new System.Windows.Forms.RadioButton();
            this.rbtSubSpec = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbtSpec = new System.Windows.Forms.RadioButton();
            this.rbtOperRoom = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grpLabelRePrint = new System.Windows.Forms.GroupBox();
            this.tbSpecNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbSpecType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label22 = new System.Windows.Forms.Label();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtSpecNoExl = new System.Windows.Forms.TextBox();
            this.txtBarCode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.cbxNeedTime = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.grpLabelRePrint.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbDis
            // 
            this.cmbDis.FormattingEnabled = true;
            this.cmbDis.Location = new System.Drawing.Point(506, 23);
            this.cmbDis.Name = "cmbDis";
            this.cmbDis.Size = new System.Drawing.Size(72, 24);
            this.cmbDis.TabIndex = 19;
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
            this.neuSpread1.Size = new System.Drawing.Size(826, 399);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 1;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ColumnCount = 8;
            this.neuSpread1_Sheet1.RowCount = 1;
            this.neuSpread1_Sheet1.ActiveSkin = FarPoint.Win.Spread.DefaultSkins.Classic2;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "源条形码";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "分装后条码";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "位置信息";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "行";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "列";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "类型";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "病种";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "取材脏器";
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(105)))), ((int)(((byte)(107)))));
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.Columns.Get(0).AllowAutoFilter = true;
            this.neuSpread1_Sheet1.Columns.Get(0).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "源条形码";
            this.neuSpread1_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 112F;
            this.neuSpread1_Sheet1.Columns.Get(1).AllowAutoFilter = true;
            this.neuSpread1_Sheet1.Columns.Get(1).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "分装后条码";
            this.neuSpread1_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 186F;
            this.neuSpread1_Sheet1.Columns.Get(2).AllowAutoFilter = true;
            this.neuSpread1_Sheet1.Columns.Get(2).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "位置信息";
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 272F;
            this.neuSpread1_Sheet1.Columns.Get(3).AllowAutoFilter = true;
            this.neuSpread1_Sheet1.Columns.Get(3).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "行";
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 55F;
            this.neuSpread1_Sheet1.Columns.Get(4).AllowAutoFilter = true;
            this.neuSpread1_Sheet1.Columns.Get(4).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "列";
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 54F;
            this.neuSpread1_Sheet1.Columns.Get(5).AllowAutoFilter = true;
            this.neuSpread1_Sheet1.Columns.Get(5).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "类型";
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 107F;
            this.neuSpread1_Sheet1.Columns.Get(6).AllowAutoFilter = true;
            this.neuSpread1_Sheet1.Columns.Get(6).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get(6).Label = "病种";
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 98F;
            this.neuSpread1_Sheet1.Columns.Get(7).AllowAutoFilter = true;
            this.neuSpread1_Sheet1.Columns.Get(7).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get(7).Label = "取材脏器";
            this.neuSpread1_Sheet1.Columns.Get(7).Width = 121F;
            this.neuSpread1_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.Columns.Get(0).AllowAutoSort = true;
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(456, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 18;
            this.label1.Text = "病种:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbtOrg);
            this.groupBox3.Controls.Add(this.rbtSubSpec);
            this.groupBox3.Location = new System.Drawing.Point(7, 56);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(222, 54);
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
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbtSpec);
            this.groupBox2.Controls.Add(this.rbtOperRoom);
            this.groupBox2.Location = new System.Drawing.Point(260, 56);
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 178);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(832, 424);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询结果";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.neuSpread1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(826, 399);
            this.panel1.TabIndex = 12;
            // 
            // grpLabelRePrint
            // 
            this.grpLabelRePrint.Controls.Add(this.cbxNeedTime);
            this.grpLabelRePrint.Controls.Add(this.tbSpecNo);
            this.grpLabelRePrint.Controls.Add(this.label5);
            this.grpLabelRePrint.Controls.Add(this.cmbSpecType);
            this.grpLabelRePrint.Controls.Add(this.label12);
            this.grpLabelRePrint.Controls.Add(this.groupBox4);
            this.grpLabelRePrint.Controls.Add(this.cmbDis);
            this.grpLabelRePrint.Controls.Add(this.label1);
            this.grpLabelRePrint.Controls.Add(this.groupBox3);
            this.grpLabelRePrint.Controls.Add(this.groupBox2);
            this.grpLabelRePrint.Controls.Add(this.txtBarCode);
            this.grpLabelRePrint.Controls.Add(this.label4);
            this.grpLabelRePrint.Controls.Add(this.dtpEnd);
            this.grpLabelRePrint.Controls.Add(this.label3);
            this.grpLabelRePrint.Controls.Add(this.dtpStart);
            this.grpLabelRePrint.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpLabelRePrint.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpLabelRePrint.Location = new System.Drawing.Point(0, 0);
            this.grpLabelRePrint.Margin = new System.Windows.Forms.Padding(4);
            this.grpLabelRePrint.Name = "grpLabelRePrint";
            this.grpLabelRePrint.Padding = new System.Windows.Forms.Padding(4);
            this.grpLabelRePrint.Size = new System.Drawing.Size(832, 178);
            this.grpLabelRePrint.TabIndex = 13;
            this.grpLabelRePrint.TabStop = false;
            this.grpLabelRePrint.Text = "查询条件";
            // 
            // tbSpecNo
            // 
            this.tbSpecNo.Location = new System.Drawing.Point(598, 53);
            this.tbSpecNo.Name = "tbSpecNo";
            this.tbSpecNo.Size = new System.Drawing.Size(214, 26);
            this.tbSpecNo.TabIndex = 121;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(480, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 16);
            this.label5.TabIndex = 120;
            this.label5.Text = "标   本   号:";
            // 
            // cmbSpecType
            // 
            //this.cmbSpecType.A = false;
            this.cmbSpecType.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbSpecType.FormattingEnabled = true;
            this.cmbSpecType.IsFlat = true;
            this.cmbSpecType.IsLike = true;
            this.cmbSpecType.Location = new System.Drawing.Point(673, 23);
            this.cmbSpecType.Name = "cmbSpecType";
            this.cmbSpecType.PopForm = null;
            this.cmbSpecType.ShowCustomerList = false;
            this.cmbSpecType.ShowID = false;
            this.cmbSpecType.Size = new System.Drawing.Size(139, 24);
            this.cmbSpecType.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbSpecType.TabIndex = 119;
            this.cmbSpecType.Tag = "";
            this.cmbSpecType.ToolBarUse = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(587, 27);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 16);
            this.label12.TabIndex = 118;
            this.label12.Text = "标本类型:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label22);
            this.groupBox4.Controls.Add(this.btnQuery);
            this.groupBox4.Controls.Add(this.btnBrowse);
            this.groupBox4.Controls.Add(this.txtSpecNoExl);
            this.groupBox4.Location = new System.Drawing.Point(7, 116);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(809, 55);
            this.groupBox4.TabIndex = 117;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "导入查询";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(6, 27);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(272, 16);
            this.label22.TabIndex = 113;
            this.label22.Text = "标本号(文件导入方式，只支持Excel)";
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(666, 15);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(86, 34);
            this.btnQuery.TabIndex = 115;
            this.btnQuery.Text = "查询(&Q)";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(552, 15);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(108, 34);
            this.btnBrowse.TabIndex = 116;
            this.btnBrowse.Text = "...浏览(&B)";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtSpecNoExl
            // 
            this.txtSpecNoExl.Location = new System.Drawing.Point(284, 23);
            this.txtSpecNoExl.Name = "txtSpecNoExl";
            this.txtSpecNoExl.Size = new System.Drawing.Size(262, 26);
            this.txtSpecNoExl.TabIndex = 114;
            // 
            // txtBarCode
            // 
            this.txtBarCode.Location = new System.Drawing.Point(598, 86);
            this.txtBarCode.Name = "txtBarCode";
            this.txtBarCode.Size = new System.Drawing.Size(214, 26);
            this.txtBarCode.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(480, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "分装标本条码:";
            // 
            // dtpEnd
            // 
            this.dtpEnd.Location = new System.Drawing.Point(292, 21);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(137, 26);
            this.dtpEnd.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(270, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "-";
            // 
            // dtpStart
            // 
            this.dtpStart.Location = new System.Drawing.Point(127, 22);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(137, 26);
            this.dtpStart.TabIndex = 7;
            // 
            // fileDialog
            // 
            this.fileDialog.FileName = "openFileDialog1";
            // 
            // cbxNeedTime
            // 
            this.cbxNeedTime.AutoSize = true;
            this.cbxNeedTime.Checked = true;
            this.cbxNeedTime.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxNeedTime.Location = new System.Drawing.Point(22, 26);
            this.cbxNeedTime.Name = "cbxNeedTime";
            this.cbxNeedTime.Size = new System.Drawing.Size(99, 20);
            this.cbxNeedTime.TabIndex = 5;
            this.cbxNeedTime.Text = "操作时间:";
            this.cbxNeedTime.UseVisualStyleBackColor = true;
            // 
            // ucQuerySpecInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpLabelRePrint);
            this.Name = "ucQuerySpecInfo";
            this.Size = new System.Drawing.Size(832, 602);
            this.Load += new System.EventHandler(this.ucQuerySpecInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.grpLabelRePrint.ResumeLayout(false);
            this.grpLabelRePrint.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbDis;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbtOrg;
        private System.Windows.Forms.RadioButton rbtSubSpec;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbtSpec;
        private System.Windows.Forms.RadioButton rbtOperRoom;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox grpLabelRePrint;
        private System.Windows.Forms.TextBox txtBarCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtSpecNoExl;
        private System.Windows.Forms.OpenFileDialog fileDialog;
        private System.Windows.Forms.TextBox tbSpecNo;
        private System.Windows.Forms.Label label5;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbSpecType;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox cbxNeedTime;
    }
}
