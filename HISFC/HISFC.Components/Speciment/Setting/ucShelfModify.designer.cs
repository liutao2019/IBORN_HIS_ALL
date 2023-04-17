namespace FS.HISFC.Components.Speciment.Setting
{
    partial class ucShelfModify
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
            this.ucBaseControl1 = new FS.FrameWork.WinForms.Controls.ucBaseControl();
            this.neuLabel8 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtIceBoxType = new FS.FrameWork.WinForms.Controls.NeuListTextBox();
            this.neuLabel6 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbOrgOrBlood = new System.Windows.Forms.ComboBox();
            this.neuLabel7 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbSpecType = new System.Windows.Forms.ComboBox();
            this.gpBaseInfo = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.txtBarCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDiseaseType = new System.Windows.Forms.ComboBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.gpBaseInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // ucBaseControl1
            // 
            this.ucBaseControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucBaseControl1.IsPrint = false;
            this.ucBaseControl1.Location = new System.Drawing.Point(0, 0);
            this.ucBaseControl1.Margin = new System.Windows.Forms.Padding(4);
            this.ucBaseControl1.Name = "ucBaseControl1";
            this.ucBaseControl1.Size = new System.Drawing.Size(1419, 1067);
            this.ucBaseControl1.TabIndex = 0;
            // 
            // neuLabel8
            // 
            this.neuLabel8.AutoSize = true;
            this.neuLabel8.Location = new System.Drawing.Point(8, 26);
            this.neuLabel8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel8.Name = "neuLabel8";
            this.neuLabel8.Size = new System.Drawing.Size(88, 16);
            this.neuLabel8.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel8.TabIndex = 15;
            this.neuLabel8.Text = "冰箱类型：";
            // 
            // txtIceBoxType
            // 
            this.txtIceBoxType.EnterVisiable = false;
            this.txtIceBoxType.IsFind = false;
            //this.txtIceBoxType.IsSelctNone = true;
            //this.txtIceBoxType.IsSendToNext = false;
            //this.txtIceBoxType.IsShowID = false;
            //this.txtIceBoxType.ItemText = "";
            this.txtIceBoxType.ListBoxHeight = 100;
            //this.txtIceBoxType.ListBoxVisible = false;
            this.txtIceBoxType.ListBoxWidth = 150;
            this.txtIceBoxType.Location = new System.Drawing.Point(104, 21);
            this.txtIceBoxType.Margin = new System.Windows.Forms.Padding(4);
            this.txtIceBoxType.Name = "txtIceBoxType";
            this.txtIceBoxType.OmitFilter = true;
            this.txtIceBoxType.SelectedItem = null;
            this.txtIceBoxType.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtIceBoxType.ShowID = true;
            this.txtIceBoxType.Size = new System.Drawing.Size(100, 26);
            this.txtIceBoxType.TabIndex = 85;
            // 
            // neuLabel6
            // 
            this.neuLabel6.AutoSize = true;
            this.neuLabel6.Location = new System.Drawing.Point(497, 25);
            this.neuLabel6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel6.Name = "neuLabel6";
            this.neuLabel6.Size = new System.Drawing.Size(88, 16);
            this.neuLabel6.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel6.TabIndex = 88;
            this.neuLabel6.Text = "标本种类：";
            // 
            // cmbOrgOrBlood
            // 
            this.cmbOrgOrBlood.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrgOrBlood.FormattingEnabled = true;
            this.cmbOrgOrBlood.Location = new System.Drawing.Point(593, 21);
            this.cmbOrgOrBlood.Margin = new System.Windows.Forms.Padding(4);
            this.cmbOrgOrBlood.Name = "cmbOrgOrBlood";
            this.cmbOrgOrBlood.Size = new System.Drawing.Size(84, 24);
            this.cmbOrgOrBlood.TabIndex = 89;
            this.cmbOrgOrBlood.SelectedIndexChanged += new System.EventHandler(this.cmbOrgOrBlood_SelectedIndexChanged);
            // 
            // neuLabel7
            // 
            this.neuLabel7.AutoSize = true;
            this.neuLabel7.Location = new System.Drawing.Point(685, 25);
            this.neuLabel7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel7.Name = "neuLabel7";
            this.neuLabel7.Size = new System.Drawing.Size(88, 16);
            this.neuLabel7.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel7.TabIndex = 90;
            this.neuLabel7.Text = "标本类型：";
            // 
            // cmbSpecType
            // 
            this.cmbSpecType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpecType.FormattingEnabled = true;
            this.cmbSpecType.Location = new System.Drawing.Point(780, 21);
            this.cmbSpecType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSpecType.Name = "cmbSpecType";
            this.cmbSpecType.Size = new System.Drawing.Size(132, 24);
            this.cmbSpecType.TabIndex = 91;
            // 
            // gpBaseInfo
            // 
            this.gpBaseInfo.Controls.Add(this.txtBarCode);
            this.gpBaseInfo.Controls.Add(this.label1);
            this.gpBaseInfo.Controls.Add(this.cmbDiseaseType);
            this.gpBaseInfo.Controls.Add(this.neuLabel1);
            this.gpBaseInfo.Controls.Add(this.cmbSpecType);
            this.gpBaseInfo.Controls.Add(this.neuLabel7);
            this.gpBaseInfo.Controls.Add(this.cmbOrgOrBlood);
            this.gpBaseInfo.Controls.Add(this.neuLabel6);
            this.gpBaseInfo.Controls.Add(this.txtIceBoxType);
            this.gpBaseInfo.Controls.Add(this.neuLabel8);
            this.gpBaseInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpBaseInfo.Location = new System.Drawing.Point(0, 0);
            this.gpBaseInfo.Margin = new System.Windows.Forms.Padding(4);
            this.gpBaseInfo.Name = "gpBaseInfo";
            this.gpBaseInfo.Padding = new System.Windows.Forms.Padding(4);
            this.gpBaseInfo.Size = new System.Drawing.Size(1419, 57);
            this.gpBaseInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.gpBaseInfo.TabIndex = 6;
            this.gpBaseInfo.TabStop = false;
            this.gpBaseInfo.Text = "查询条件";
            // 
            // txtBarCode
            // 
            this.txtBarCode.Location = new System.Drawing.Point(991, 19);
            this.txtBarCode.Name = "txtBarCode";
            this.txtBarCode.Size = new System.Drawing.Size(166, 26);
            this.txtBarCode.TabIndex = 95;
            this.txtBarCode.TextChanged += new System.EventHandler(this.txtBarCode_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(922, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 94;
            this.label1.Text = "条码：";
            // 
            // cmbDiseaseType
            // 
            this.cmbDiseaseType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDiseaseType.FormattingEnabled = true;
            this.cmbDiseaseType.Location = new System.Drawing.Point(331, 21);
            this.cmbDiseaseType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDiseaseType.Name = "cmbDiseaseType";
            this.cmbDiseaseType.Size = new System.Drawing.Size(132, 24);
            this.cmbDiseaseType.TabIndex = 93;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(236, 25);
            this.neuLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(88, 16);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 92;
            this.neuLabel1.Text = "病种类型：";
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "2.5.2007.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, ";
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.FileName = "";
            this.neuSpread1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(4, 65);
            this.neuSpread1.Margin = new System.Windows.Forms.Padding(4);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(1409, 982);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.neuSpread1.TabIndex = 7;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(this.neuSpread1_SelectionChanged);
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ColumnCount = 12;
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.ActiveSkin = FarPoint.Win.Spread.DefaultSkins.Classic2;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(105)))), ((int)(((byte)(107)))));
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(11).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(11).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.DefaultStyle.Locked = false;
            this.neuSpread1_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 40F;
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
            this.neuSpread1.SetActiveViewport(1, 0);
            // 
            // ucShelfModify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuSpread1);
            this.Controls.Add(this.gpBaseInfo);
            this.Controls.Add(this.ucBaseControl1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ucShelfModify";
            this.Size = new System.Drawing.Size(1419, 1067);
            this.Load += new System.EventHandler(this.ucShelfModify_Load);
            this.gpBaseInfo.ResumeLayout(false);
            this.gpBaseInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.ucBaseControl ucBaseControl1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel8;
        private FS.FrameWork.WinForms.Controls.NeuListTextBox txtIceBoxType;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel6;
        private System.Windows.Forms.ComboBox cmbOrgOrBlood;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel7;
        private System.Windows.Forms.ComboBox cmbSpecType;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox gpBaseInfo;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private System.Windows.Forms.ComboBox cmbDiseaseType;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private System.Windows.Forms.TextBox txtBarCode;
        private System.Windows.Forms.Label label1;
    }
}
