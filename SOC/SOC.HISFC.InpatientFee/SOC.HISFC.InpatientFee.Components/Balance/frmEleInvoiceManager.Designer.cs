namespace Neusoft.SOC.HISFC.InpatientFee.Components.Balance
{
    partial class frmEleInvoiceManager
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType7 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType8 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dtpEnd = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dtpBegin = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.jie = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.fpEleInvoice = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpEleInvoice_sheetView1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpEleInvoice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpEleInvoice_sheetView1)).BeginInit();
            this.SuspendLayout();
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.button1);
            this.neuPanel2.Controls.Add(this.textBox1);
            this.neuPanel2.Controls.Add(this.neuLabel2);
            this.neuPanel2.Controls.Add(this.dtpEnd);
            this.neuPanel2.Controls.Add(this.neuLabel1);
            this.neuPanel2.Controls.Add(this.dtpBegin);
            this.neuPanel2.Controls.Add(this.jie);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel2.Location = new System.Drawing.Point(0, 0);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(1148, 41);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 104;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(597, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(53, 9);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 5;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(12, 15);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(35, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 4;
            this.neuLabel2.Text = "姓名:";
            // 
            // dtpEnd
            // 
            this.dtpEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd.IsEnter2Tab = false;
            this.dtpEnd.Location = new System.Drawing.Point(417, 9);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(137, 21);
            this.dtpEnd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpEnd.TabIndex = 3;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(391, 13);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(23, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 2;
            this.neuLabel1.Text = "到:";
            // 
            // dtpBegin
            // 
            this.dtpBegin.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBegin.IsEnter2Tab = false;
            this.dtpBegin.Location = new System.Drawing.Point(246, 9);
            this.dtpBegin.Name = "dtpBegin";
            this.dtpBegin.Size = new System.Drawing.Size(139, 21);
            this.dtpBegin.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpBegin.TabIndex = 2;
            // 
            // jie
            // 
            this.jie.AutoSize = true;
            this.jie.Location = new System.Drawing.Point(189, 13);
            this.jie.Name = "jie";
            this.jie.Size = new System.Drawing.Size(59, 12);
            this.jie.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.jie.TabIndex = 0;
            this.jie.Text = "开票时间:";
            // 
            // fpEleInvoice
            // 
            this.fpEleInvoice.About = "3.0.2004.2005";
            this.fpEleInvoice.AccessibleDescription = "fpEleInvoice, Sheet1, Row 0, Column 0, ";
            this.fpEleInvoice.BackColor = System.Drawing.Color.White;
            this.fpEleInvoice.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.fpEleInvoice.EditModeReplace = true;
            this.fpEleInvoice.FileName = "";
            this.fpEleInvoice.Font = new System.Drawing.Font("宋体", 9F);
            this.fpEleInvoice.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpEleInvoice.IsAutoSaveGridStatus = false;
            this.fpEleInvoice.IsCanCustomConfigColumn = false;
            this.fpEleInvoice.Location = new System.Drawing.Point(0, 38);
            this.fpEleInvoice.Name = "fpEleInvoice";
            this.fpEleInvoice.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpEleInvoice.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpEleInvoice_sheetView1});
            this.fpEleInvoice.Size = new System.Drawing.Size(1148, 397);
            this.fpEleInvoice.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpEleInvoice.TabIndex = 103;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpEleInvoice.TextTipAppearance = tipAppearance2;
            this.fpEleInvoice.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpEleInvoice_sheetView1
            // 
            this.fpEleInvoice_sheetView1.Reset();
            this.fpEleInvoice_sheetView1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpEleInvoice_sheetView1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpEleInvoice_sheetView1.ColumnCount = 10;
            this.fpEleInvoice_sheetView1.RowCount = 1;
            this.fpEleInvoice_sheetView1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpEleInvoice_sheetView1.ColumnHeader.Cells.Get(0, 0).Value = "创建时间";
            this.fpEleInvoice_sheetView1.ColumnHeader.Cells.Get(0, 1).Value = "订单编号";
            this.fpEleInvoice_sheetView1.ColumnHeader.Cells.Get(0, 2).Value = "邮箱地址";
            this.fpEleInvoice_sheetView1.ColumnHeader.Cells.Get(0, 3).Value = "发票代码/发票号码";
            this.fpEleInvoice_sheetView1.ColumnHeader.Cells.Get(0, 4).Value = "发票抬头";
            this.fpEleInvoice_sheetView1.ColumnHeader.Cells.Get(0, 5).Value = "含税金额";
            this.fpEleInvoice_sheetView1.ColumnHeader.Cells.Get(0, 6).Value = "开票状态";
            this.fpEleInvoice_sheetView1.ColumnHeader.Cells.Get(0, 7).Value = "序列号";
            this.fpEleInvoice_sheetView1.ColumnHeader.Cells.Get(0, 8).Value = "冲红发票";
            this.fpEleInvoice_sheetView1.ColumnHeader.Cells.Get(0, 9).Value = "重新获取";
            this.fpEleInvoice_sheetView1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpEleInvoice_sheetView1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpEleInvoice_sheetView1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.fpEleInvoice_sheetView1.Columns.Default.NoteIndicatorColor = System.Drawing.Color.White;
            this.fpEleInvoice_sheetView1.Columns.Get(0).BackColor = System.Drawing.Color.White;
            this.fpEleInvoice_sheetView1.Columns.Get(0).CellType = textCellType5;
            this.fpEleInvoice_sheetView1.Columns.Get(0).Label = "创建时间";
            this.fpEleInvoice_sheetView1.Columns.Get(0).Width = 98F;
            this.fpEleInvoice_sheetView1.Columns.Get(1).BackColor = System.Drawing.Color.White;
            this.fpEleInvoice_sheetView1.Columns.Get(1).CellType = textCellType6;
            this.fpEleInvoice_sheetView1.Columns.Get(1).Label = "订单编号";
            this.fpEleInvoice_sheetView1.Columns.Get(1).Locked = false;
            this.fpEleInvoice_sheetView1.Columns.Get(1).Width = 96F;
            this.fpEleInvoice_sheetView1.Columns.Get(2).BackColor = System.Drawing.Color.White;
            this.fpEleInvoice_sheetView1.Columns.Get(2).CellType = textCellType7;
            this.fpEleInvoice_sheetView1.Columns.Get(2).Label = "邮箱地址";
            this.fpEleInvoice_sheetView1.Columns.Get(2).Locked = false;
            this.fpEleInvoice_sheetView1.Columns.Get(2).Width = 120F;
            this.fpEleInvoice_sheetView1.Columns.Get(3).BackColor = System.Drawing.Color.White;
            this.fpEleInvoice_sheetView1.Columns.Get(3).Label = "发票代码/发票号码";
            this.fpEleInvoice_sheetView1.Columns.Get(3).Locked = false;
            this.fpEleInvoice_sheetView1.Columns.Get(3).Width = 166F;
            this.fpEleInvoice_sheetView1.Columns.Get(4).BackColor = System.Drawing.Color.White;
            this.fpEleInvoice_sheetView1.Columns.Get(4).Label = "发票抬头";
            this.fpEleInvoice_sheetView1.Columns.Get(4).Locked = false;
            this.fpEleInvoice_sheetView1.Columns.Get(4).Width = 115F;
            this.fpEleInvoice_sheetView1.Columns.Get(5).BackColor = System.Drawing.Color.White;
            this.fpEleInvoice_sheetView1.Columns.Get(5).CellType = textCellType8;
            this.fpEleInvoice_sheetView1.Columns.Get(5).Label = "含税金额";
            this.fpEleInvoice_sheetView1.Columns.Get(5).Locked = false;
            this.fpEleInvoice_sheetView1.Columns.Get(5).Width = 134F;
            this.fpEleInvoice_sheetView1.Columns.Get(6).Label = "开票状态";
            this.fpEleInvoice_sheetView1.Columns.Get(6).Width = 116F;
            this.fpEleInvoice_sheetView1.Columns.Get(7).Label = "序列号";
            this.fpEleInvoice_sheetView1.Columns.Get(7).Width = 110F;
            this.fpEleInvoice_sheetView1.Columns.Get(8).Label = "冲红发票";
            this.fpEleInvoice_sheetView1.Columns.Get(8).Width = 81F;
            this.fpEleInvoice_sheetView1.Columns.Get(9).Label = "重新获取";
            this.fpEleInvoice_sheetView1.Columns.Get(9).Width = 86F;
            this.fpEleInvoice_sheetView1.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpEleInvoice_sheetView1.DefaultStyle.NoteIndicatorColor = System.Drawing.Color.White;
            this.fpEleInvoice_sheetView1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpEleInvoice_sheetView1.RowHeader.Columns.Default.Resizable = true;
            this.fpEleInvoice_sheetView1.RowHeader.Columns.Get(0).Width = 37F;
            this.fpEleInvoice_sheetView1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpEleInvoice_sheetView1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpEleInvoice_sheetView1.Rows.Default.Height = 25F;
            this.fpEleInvoice_sheetView1.Rows.Default.NoteIndicatorColor = System.Drawing.Color.White;
            this.fpEleInvoice_sheetView1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpEleInvoice_sheetView1.SheetCornerStyle.Parent = "CornerDefault";
            this.fpEleInvoice_sheetView1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpEleInvoice_sheetView1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // frmEleInvoiceManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1148, 435);
            this.Controls.Add(this.neuPanel2);
            this.Controls.Add(this.fpEleInvoice);
            this.KeyPreview = true;
            this.Name = "frmEleInvoiceManager";
            this.Text = "电子发票";
            this.neuPanel2.ResumeLayout(false);
            this.neuPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpEleInvoice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpEleInvoice_sheetView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected FS.FrameWork.WinForms.Controls.NeuSpread fpPayType;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpEnd;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpBegin;
        private FS.FrameWork.WinForms.Controls.NeuLabel jie;
        protected FS.FrameWork.WinForms.Controls.NeuSpread fpEleInvoice;
        protected FarPoint.Win.Spread.SheetView fpEleInvoice_sheetView1;

    }
}