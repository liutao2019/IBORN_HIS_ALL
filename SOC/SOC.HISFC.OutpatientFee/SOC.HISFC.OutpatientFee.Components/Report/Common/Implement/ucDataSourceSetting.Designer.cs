namespace FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Implement
{
    partial class ucDataSourceSetting
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
            this.tbDataSourceSetting = new System.Windows.Forms.TabControl();
            this.tpBaseInfo = new System.Windows.Forms.TabPage();
            this.tpRowSum = new System.Windows.Forms.TabPage();
            this.tpRowGroup = new System.Windows.Forms.TabPage();
            this.tpColumnSum = new System.Windows.Forms.TabPage();
            this.tpColumnGroup = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbSql = new System.Windows.Forms.TabPage();
            this.ckAddMapData = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ckAddMapColumn = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ckAddMapRow = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.tpCrossInfo = new System.Windows.Forms.TabPage();
            this.neuCheckBox3 = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.neuCheckBox4 = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.neuCheckBox2 = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.neuCheckBox1 = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.neuCheckBox5 = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.lbColumns = new System.Windows.Forms.ListBox();
            this.gbColumns = new System.Windows.Forms.GroupBox();
            this.gbCrossRows = new System.Windows.Forms.GroupBox();
            this.gbCrossColumns = new System.Windows.Forms.GroupBox();
            this.gbCrossValue = new System.Windows.Forms.GroupBox();
            this.tbDataSourceSetting.SuspendLayout();
            this.tpBaseInfo.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpCrossInfo.SuspendLayout();
            this.gbColumns.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbDataSourceSetting
            // 
            this.tbDataSourceSetting.Controls.Add(this.tpBaseInfo);
            this.tbDataSourceSetting.Controls.Add(this.tpCrossInfo);
            this.tbDataSourceSetting.Controls.Add(this.tpRowSum);
            this.tbDataSourceSetting.Controls.Add(this.tpColumnSum);
            this.tbDataSourceSetting.Controls.Add(this.tpRowGroup);
            this.tbDataSourceSetting.Controls.Add(this.tpColumnGroup);
            this.tbDataSourceSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDataSourceSetting.Location = new System.Drawing.Point(0, 0);
            this.tbDataSourceSetting.Name = "tbDataSourceSetting";
            this.tbDataSourceSetting.SelectedIndex = 0;
            this.tbDataSourceSetting.Size = new System.Drawing.Size(818, 364);
            this.tbDataSourceSetting.TabIndex = 0;
            // 
            // tpBaseInfo
            // 
            this.tpBaseInfo.Controls.Add(this.groupBox2);
            this.tpBaseInfo.Controls.Add(this.groupBox1);
            this.tpBaseInfo.Location = new System.Drawing.Point(4, 22);
            this.tpBaseInfo.Name = "tpBaseInfo";
            this.tpBaseInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpBaseInfo.Size = new System.Drawing.Size(810, 338);
            this.tpBaseInfo.TabIndex = 0;
            this.tpBaseInfo.Text = "基本信息设置";
            this.tpBaseInfo.UseVisualStyleBackColor = true;
            // 
            // tpRowSum
            // 
            this.tpRowSum.Location = new System.Drawing.Point(4, 22);
            this.tpRowSum.Name = "tpRowSum";
            this.tpRowSum.Padding = new System.Windows.Forms.Padding(3);
            this.tpRowSum.Size = new System.Drawing.Size(810, 338);
            this.tpRowSum.TabIndex = 1;
            this.tpRowSum.Text = "行合计";
            this.tpRowSum.UseVisualStyleBackColor = true;
            // 
            // tpRowGroup
            // 
            this.tpRowGroup.Location = new System.Drawing.Point(4, 22);
            this.tpRowGroup.Name = "tpRowGroup";
            this.tpRowGroup.Size = new System.Drawing.Size(810, 338);
            this.tpRowGroup.TabIndex = 2;
            this.tpRowGroup.Text = "行分组";
            this.tpRowGroup.UseVisualStyleBackColor = true;
            // 
            // tpColumnSum
            // 
            this.tpColumnSum.Location = new System.Drawing.Point(4, 22);
            this.tpColumnSum.Name = "tpColumnSum";
            this.tpColumnSum.Size = new System.Drawing.Size(810, 338);
            this.tpColumnSum.TabIndex = 3;
            this.tpColumnSum.Text = "列合计";
            this.tpColumnSum.UseVisualStyleBackColor = true;
            // 
            // tpColumnGroup
            // 
            this.tpColumnGroup.Location = new System.Drawing.Point(4, 22);
            this.tpColumnGroup.Name = "tpColumnGroup";
            this.tpColumnGroup.Size = new System.Drawing.Size(810, 338);
            this.tpColumnGroup.TabIndex = 4;
            this.tpColumnGroup.Text = "列分组";
            this.tpColumnGroup.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.neuCheckBox5);
            this.groupBox1.Controls.Add(this.neuCheckBox1);
            this.groupBox1.Controls.Add(this.neuCheckBox2);
            this.groupBox1.Controls.Add(this.neuCheckBox4);
            this.groupBox1.Controls.Add(this.neuCheckBox3);
            this.groupBox1.Controls.Add(this.ckAddMapData);
            this.groupBox1.Controls.Add(this.ckAddMapColumn);
            this.groupBox1.Controls.Add(this.ckAddMapRow);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.neuLabel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 332);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据源定义";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tabControl1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(260, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(547, 332);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.White;
            this.txtName.IsEnter2Tab = false;
            this.txtName.Location = new System.Drawing.Point(71, 37);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(148, 21);
            this.txtName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtName.TabIndex = 18;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(24, 40);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(41, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 17;
            this.neuLabel1.Text = "名称：";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbSql);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 17);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(541, 312);
            this.tabControl1.TabIndex = 0;
            // 
            // tbSql
            // 
            this.tbSql.Location = new System.Drawing.Point(4, 22);
            this.tbSql.Name = "tbSql";
            this.tbSql.Padding = new System.Windows.Forms.Padding(3);
            this.tbSql.Size = new System.Drawing.Size(533, 286);
            this.tbSql.TabIndex = 0;
            this.tbSql.Text = "SQL";
            this.tbSql.UseVisualStyleBackColor = true;
            // 
            // ckAddMapData
            // 
            this.ckAddMapData.AutoSize = true;
            this.ckAddMapData.Location = new System.Drawing.Point(26, 107);
            this.ckAddMapData.Name = "ckAddMapData";
            this.ckAddMapData.Size = new System.Drawing.Size(96, 16);
            this.ckAddMapData.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ckAddMapData.TabIndex = 26;
            this.ckAddMapData.Text = "加入数据字典";
            this.ckAddMapData.UseVisualStyleBackColor = true;
            // 
            // ckAddMapColumn
            // 
            this.ckAddMapColumn.AutoSize = true;
            this.ckAddMapColumn.Location = new System.Drawing.Point(140, 76);
            this.ckAddMapColumn.Name = "ckAddMapColumn";
            this.ckAddMapColumn.Size = new System.Drawing.Size(108, 16);
            this.ckAddMapColumn.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ckAddMapColumn.TabIndex = 25;
            this.ckAddMapColumn.Text = "加入数据列字典";
            this.ckAddMapColumn.UseVisualStyleBackColor = true;
            // 
            // ckAddMapRow
            // 
            this.ckAddMapRow.AutoSize = true;
            this.ckAddMapRow.Location = new System.Drawing.Point(26, 76);
            this.ckAddMapRow.Name = "ckAddMapRow";
            this.ckAddMapRow.Size = new System.Drawing.Size(108, 16);
            this.ckAddMapRow.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ckAddMapRow.TabIndex = 24;
            this.ckAddMapRow.Text = "加入数据行字典";
            this.ckAddMapRow.UseVisualStyleBackColor = true;
            // 
            // tpCrossInfo
            // 
            this.tpCrossInfo.Controls.Add(this.gbCrossValue);
            this.tpCrossInfo.Controls.Add(this.gbCrossColumns);
            this.tpCrossInfo.Controls.Add(this.gbCrossRows);
            this.tpCrossInfo.Controls.Add(this.gbColumns);
            this.tpCrossInfo.Location = new System.Drawing.Point(4, 22);
            this.tpCrossInfo.Name = "tpCrossInfo";
            this.tpCrossInfo.Size = new System.Drawing.Size(810, 338);
            this.tpCrossInfo.TabIndex = 5;
            this.tpCrossInfo.Text = "交叉报表设置";
            this.tpCrossInfo.UseVisualStyleBackColor = true;
            // 
            // neuCheckBox3
            // 
            this.neuCheckBox3.AutoSize = true;
            this.neuCheckBox3.Location = new System.Drawing.Point(26, 184);
            this.neuCheckBox3.Name = "neuCheckBox3";
            this.neuCheckBox3.Size = new System.Drawing.Size(60, 16);
            this.neuCheckBox3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuCheckBox3.TabIndex = 27;
            this.neuCheckBox3.Text = "行合计";
            this.neuCheckBox3.UseVisualStyleBackColor = true;
            // 
            // neuCheckBox4
            // 
            this.neuCheckBox4.AutoSize = true;
            this.neuCheckBox4.Location = new System.Drawing.Point(26, 143);
            this.neuCheckBox4.Name = "neuCheckBox4";
            this.neuCheckBox4.Size = new System.Drawing.Size(72, 16);
            this.neuCheckBox4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuCheckBox4.TabIndex = 28;
            this.neuCheckBox4.Text = "交叉报表";
            this.neuCheckBox4.UseVisualStyleBackColor = true;
            // 
            // neuCheckBox2
            // 
            this.neuCheckBox2.AutoSize = true;
            this.neuCheckBox2.Location = new System.Drawing.Point(140, 184);
            this.neuCheckBox2.Name = "neuCheckBox2";
            this.neuCheckBox2.Size = new System.Drawing.Size(60, 16);
            this.neuCheckBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuCheckBox2.TabIndex = 30;
            this.neuCheckBox2.Text = "列合计";
            this.neuCheckBox2.UseVisualStyleBackColor = true;
            // 
            // neuCheckBox1
            // 
            this.neuCheckBox1.AutoSize = true;
            this.neuCheckBox1.Location = new System.Drawing.Point(26, 213);
            this.neuCheckBox1.Name = "neuCheckBox1";
            this.neuCheckBox1.Size = new System.Drawing.Size(60, 16);
            this.neuCheckBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuCheckBox1.TabIndex = 31;
            this.neuCheckBox1.Text = "行分组";
            this.neuCheckBox1.UseVisualStyleBackColor = true;
            // 
            // neuCheckBox5
            // 
            this.neuCheckBox5.AutoSize = true;
            this.neuCheckBox5.Location = new System.Drawing.Point(140, 213);
            this.neuCheckBox5.Name = "neuCheckBox5";
            this.neuCheckBox5.Size = new System.Drawing.Size(60, 16);
            this.neuCheckBox5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuCheckBox5.TabIndex = 32;
            this.neuCheckBox5.Text = "列分组";
            this.neuCheckBox5.UseVisualStyleBackColor = true;
            // 
            // lbColumns
            // 
            this.lbColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbColumns.FormattingEnabled = true;
            this.lbColumns.ItemHeight = 12;
            this.lbColumns.Location = new System.Drawing.Point(3, 17);
            this.lbColumns.Name = "lbColumns";
            this.lbColumns.Size = new System.Drawing.Size(202, 316);
            this.lbColumns.TabIndex = 0;
            // 
            // gbColumns
            // 
            this.gbColumns.Controls.Add(this.lbColumns);
            this.gbColumns.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbColumns.Location = new System.Drawing.Point(0, 0);
            this.gbColumns.Name = "gbColumns";
            this.gbColumns.Size = new System.Drawing.Size(208, 338);
            this.gbColumns.TabIndex = 1;
            this.gbColumns.TabStop = false;
            this.gbColumns.Text = "数据列";
            // 
            // gbCrossRows
            // 
            this.gbCrossRows.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbCrossRows.Location = new System.Drawing.Point(208, 0);
            this.gbCrossRows.Name = "gbCrossRows";
            this.gbCrossRows.Size = new System.Drawing.Size(200, 338);
            this.gbCrossRows.TabIndex = 2;
            this.gbCrossRows.TabStop = false;
            this.gbCrossRows.Text = "行";
            // 
            // gbCrossColumns
            // 
            this.gbCrossColumns.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbCrossColumns.Location = new System.Drawing.Point(408, 0);
            this.gbCrossColumns.Name = "gbCrossColumns";
            this.gbCrossColumns.Size = new System.Drawing.Size(200, 338);
            this.gbCrossColumns.TabIndex = 3;
            this.gbCrossColumns.TabStop = false;
            this.gbCrossColumns.Text = "列";
            // 
            // gbCrossValue
            // 
            this.gbCrossValue.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbCrossValue.Location = new System.Drawing.Point(608, 0);
            this.gbCrossValue.Name = "gbCrossValue";
            this.gbCrossValue.Size = new System.Drawing.Size(200, 338);
            this.gbCrossValue.TabIndex = 4;
            this.gbCrossValue.TabStop = false;
            this.gbCrossValue.Text = "值";
            // 
            // ucDataSourceSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbDataSourceSetting);
            this.Name = "ucDataSourceSetting";
            this.Size = new System.Drawing.Size(818, 364);
            this.tbDataSourceSetting.ResumeLayout(false);
            this.tpBaseInfo.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpCrossInfo.ResumeLayout(false);
            this.gbColumns.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbDataSourceSetting;
        private System.Windows.Forms.TabPage tpBaseInfo;
        private System.Windows.Forms.TabPage tpRowSum;
        private System.Windows.Forms.TabPage tpRowGroup;
        private System.Windows.Forms.TabPage tpColumnSum;
        private System.Windows.Forms.TabPage tpColumnGroup;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtName;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbSql;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ckAddMapData;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ckAddMapColumn;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ckAddMapRow;
        private System.Windows.Forms.TabPage tpCrossInfo;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox neuCheckBox1;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox neuCheckBox2;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox neuCheckBox4;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox neuCheckBox3;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox neuCheckBox5;
        private System.Windows.Forms.GroupBox gbColumns;
        private System.Windows.Forms.ListBox lbColumns;
        private System.Windows.Forms.GroupBox gbCrossValue;
        private System.Windows.Forms.GroupBox gbCrossColumns;
        private System.Windows.Forms.GroupBox gbCrossRows;
    }
}
