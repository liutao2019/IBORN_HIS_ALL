﻿namespace FS.HISFC.Components.Pharmacy.Report
{
    partial class ucSystemListen
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
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.tbID = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuButton2 = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.tbIndex = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuButton1 = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuGroupBox2 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuButton3 = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbTime = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuButton4 = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuPanel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuRadioButton2 = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.neuButton5 = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuRadioButton1 = new FS.FrameWork.WinForms.Controls.NeuRadioButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.neuGroupBox1.SuspendLayout();
            this.neuGroupBox2.SuspendLayout();
            this.neuPanel1.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            this.neuPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, ";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.FileName = "";
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(3, 17);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(793, 341);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            this.neuSpread1_Sheet1.ColumnCount = 5;
            this.neuSpread1_Sheet1.RowCount = 1;
            this.neuSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(204))))), System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(204))))), FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.GhostWhite, System.Drawing.Color.Black, System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(153))))), System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, true, true, true);
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "监控序列";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "状态";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "监控进程";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "监控反馈";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "索引";
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.GhostWhite;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "监控序列";
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 67F;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "状态";
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 41F;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "监控进程";
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 260F;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "监控反馈";
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 300F;
            this.neuSpread1_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.DefaultStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.neuSpread1_Sheet1.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.neuSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.GhostWhite;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.neuSpread1_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.GhostWhite;
            this.neuSpread1_Sheet1.SheetCornerStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.SheetCornerStyle.Locked = false;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.tbID);
            this.neuGroupBox1.Controls.Add(this.neuLabel3);
            this.neuGroupBox1.Controls.Add(this.neuButton2);
            this.neuGroupBox1.Controls.Add(this.tbIndex);
            this.neuGroupBox1.Controls.Add(this.neuLabel2);
            this.neuGroupBox1.Controls.Add(this.tbName);
            this.neuGroupBox1.Controls.Add(this.neuLabel1);
            this.neuGroupBox1.Controls.Add(this.neuButton1);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(799, 0);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 2;
            this.neuGroupBox1.TabStop = false;
            this.neuGroupBox1.Text = "neuGroupBox1";
            this.neuGroupBox1.Visible = false;
            // 
            // tbID
            // 
            this.tbID.IsEnter2Tab = false;
            this.tbID.Location = new System.Drawing.Point(47, 17);
            this.tbID.Name = "tbID";
            this.tbID.Size = new System.Drawing.Size(28, 21);
            this.tbID.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbID.TabIndex = 7;
            this.tbID.Text = "1";
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(6, 22);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(35, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 6;
            this.neuLabel3.Text = "序列:";
            // 
            // neuButton2
            // 
            this.neuButton2.Location = new System.Drawing.Point(715, 17);
            this.neuButton2.Name = "neuButton2";
            this.neuButton2.Size = new System.Drawing.Size(75, 23);
            this.neuButton2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuButton2.TabIndex = 5;
            this.neuButton2.Text = "删除监控";
            this.neuButton2.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuButton2.UseVisualStyleBackColor = true;
            this.neuButton2.Click += new System.EventHandler(this.neuButton2_Click);
            // 
            // tbIndex
            // 
            this.tbIndex.IsEnter2Tab = false;
            this.tbIndex.Location = new System.Drawing.Point(370, 18);
            this.tbIndex.Name = "tbIndex";
            this.tbIndex.Size = new System.Drawing.Size(248, 21);
            this.tbIndex.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbIndex.TabIndex = 4;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(311, 22);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(53, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 3;
            this.neuLabel2.Text = "SQL索引:";
            // 
            // tbName
            // 
            this.tbName.IsEnter2Tab = false;
            this.tbName.Location = new System.Drawing.Point(151, 18);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(145, 21);
            this.tbName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbName.TabIndex = 2;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(86, 22);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(59, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 1;
            this.neuLabel1.Text = "监控描述:";
            // 
            // neuButton1
            // 
            this.neuButton1.Location = new System.Drawing.Point(634, 17);
            this.neuButton1.Name = "neuButton1";
            this.neuButton1.Size = new System.Drawing.Size(75, 23);
            this.neuButton1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuButton1.TabIndex = 0;
            this.neuButton1.Text = "添加监控";
            this.neuButton1.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuButton1.UseVisualStyleBackColor = true;
            this.neuButton1.Click += new System.EventHandler(this.neuButton1_Click);
            // 
            // neuGroupBox2
            // 
            this.neuGroupBox2.Controls.Add(this.neuSpread1);
            this.neuGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuGroupBox2.Location = new System.Drawing.Point(0, 29);
            this.neuGroupBox2.Name = "neuGroupBox2";
            this.neuGroupBox2.Size = new System.Drawing.Size(799, 361);
            this.neuGroupBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox2.TabIndex = 3;
            this.neuGroupBox2.TabStop = false;
            this.neuGroupBox2.Text = "neuGroupBox2";
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.neuPanel2);
            this.neuPanel1.Controls.Add(this.neuPanel3);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(799, 29);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 4;
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.neuButton3);
            this.neuPanel2.Controls.Add(this.neuLabel4);
            this.neuPanel2.Controls.Add(this.tbTime);
            this.neuPanel2.Controls.Add(this.neuLabel5);
            this.neuPanel2.Controls.Add(this.neuButton4);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel2.Location = new System.Drawing.Point(267, 0);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(532, 29);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 16;
            this.neuPanel2.Visible = false;
            // 
            // neuButton3
            // 
            this.neuButton3.Location = new System.Drawing.Point(165, 4);
            this.neuButton3.Name = "neuButton3";
            this.neuButton3.Size = new System.Drawing.Size(75, 23);
            this.neuButton3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuButton3.TabIndex = 11;
            this.neuButton3.Text = "启动";
            this.neuButton3.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuButton3.UseVisualStyleBackColor = true;
            this.neuButton3.Click += new System.EventHandler(this.neuButton3_Click);
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point(4, 11);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(83, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 8;
            this.neuLabel4.Text = "监控间隔时间:";
            // 
            // tbTime
            // 
            this.tbTime.IsEnter2Tab = false;
            this.tbTime.Location = new System.Drawing.Point(93, 6);
            this.tbTime.Name = "tbTime";
            this.tbTime.Size = new System.Drawing.Size(39, 21);
            this.tbTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbTime.TabIndex = 9;
            this.tbTime.Text = "300";
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Location = new System.Drawing.Point(142, 10);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(17, 12);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 10;
            this.neuLabel5.Text = "秒";
            // 
            // neuButton4
            // 
            this.neuButton4.Enabled = false;
            this.neuButton4.Location = new System.Drawing.Point(246, 4);
            this.neuButton4.Name = "neuButton4";
            this.neuButton4.Size = new System.Drawing.Size(75, 23);
            this.neuButton4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuButton4.TabIndex = 12;
            this.neuButton4.Text = "结束";
            this.neuButton4.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuButton4.UseVisualStyleBackColor = true;
            this.neuButton4.Click += new System.EventHandler(this.neuButton4_Click);
            // 
            // neuPanel3
            // 
            this.neuPanel3.Controls.Add(this.neuRadioButton2);
            this.neuPanel3.Controls.Add(this.neuButton5);
            this.neuPanel3.Controls.Add(this.neuRadioButton1);
            this.neuPanel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.neuPanel3.Location = new System.Drawing.Point(0, 0);
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size(267, 29);
            this.neuPanel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 17;
            // 
            // neuRadioButton2
            // 
            this.neuRadioButton2.AutoSize = true;
            this.neuRadioButton2.Location = new System.Drawing.Point(83, 8);
            this.neuRadioButton2.Name = "neuRadioButton2";
            this.neuRadioButton2.Size = new System.Drawing.Size(71, 16);
            this.neuRadioButton2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuRadioButton2.TabIndex = 15;
            this.neuRadioButton2.Text = "自动刷新";
            this.neuRadioButton2.UseVisualStyleBackColor = true;
            this.neuRadioButton2.CheckedChanged += new System.EventHandler(this.neuRadioButton2_CheckedChanged);
            // 
            // neuButton5
            // 
            this.neuButton5.Location = new System.Drawing.Point(175, 4);
            this.neuButton5.Name = "neuButton5";
            this.neuButton5.Size = new System.Drawing.Size(75, 23);
            this.neuButton5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuButton5.TabIndex = 13;
            this.neuButton5.Text = "立即刷新";
            this.neuButton5.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuButton5.UseVisualStyleBackColor = true;
            this.neuButton5.Click += new System.EventHandler(this.neuButton5_Click);
            // 
            // neuRadioButton1
            // 
            this.neuRadioButton1.AutoSize = true;
            this.neuRadioButton1.Checked = true;
            this.neuRadioButton1.Location = new System.Drawing.Point(7, 8);
            this.neuRadioButton1.Name = "neuRadioButton1";
            this.neuRadioButton1.Size = new System.Drawing.Size(71, 16);
            this.neuRadioButton1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuRadioButton1.TabIndex = 14;
            this.neuRadioButton1.TabStop = true;
            this.neuRadioButton1.Text = "手工刷新";
            this.neuRadioButton1.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            this.timer1.Interval = 100000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ucSystemListen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.neuGroupBox2);
            this.Controls.Add(this.neuPanel1);
            this.Controls.Add(this.neuGroupBox1);
            this.Name = "ucSystemListen";
            this.Size = new System.Drawing.Size(799, 390);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.neuGroupBox2.ResumeLayout(false);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel2.ResumeLayout(false);
            this.neuPanel2.PerformLayout();
            this.neuPanel3.ResumeLayout(false);
            this.neuPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox tbIndex;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuTextBox tbName;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuButton neuButton1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox2;
        private FS.FrameWork.WinForms.Controls.NeuButton neuButton2;
        private FS.FrameWork.WinForms.Controls.NeuTextBox tbID;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox tbTime;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuButton neuButton4;
        private FS.FrameWork.WinForms.Controls.NeuButton neuButton3;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private System.Windows.Forms.Timer timer1;
        private FS.FrameWork.WinForms.Controls.NeuButton neuButton5;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton neuRadioButton2;
        private FS.FrameWork.WinForms.Controls.NeuRadioButton neuRadioButton1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel3;
    }
}
