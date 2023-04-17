﻿namespace FS.HISFC.Components.Common.Controls
{
    partial class ucGroupDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucGroupDetail));
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.neuTreeView1 = new FS.FrameWork.WinForms.Controls.NeuTreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tbTotCost = new System.Windows.Forms.TextBox();
            this.txtMemo = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbDept = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtSpell = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtInput = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.chkValid = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.txtName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.neuPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.neuTreeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(823, 428);
            this.splitContainer1.SplitterDistance = 127;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.TabStop = false;
            // 
            // neuTreeView1
            // 
            this.neuTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuTreeView1.HideSelection = false;
            this.neuTreeView1.ImageIndex = 0;
            this.neuTreeView1.ImageList = this.imageList1;
            this.neuTreeView1.Location = new System.Drawing.Point(0, 0);
            this.neuTreeView1.Name = "neuTreeView1";
            this.neuTreeView1.SelectedImageIndex = 0;
            this.neuTreeView1.Size = new System.Drawing.Size(127, 428);
            this.neuTreeView1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuTreeView1.TabIndex = 12;
            this.neuTreeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.neuTreeView1_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Aqualix Icon 02.ico");
            this.imageList1.Images.SetKeyName(1, "002.GIF");
            this.imageList1.Images.SetKeyName(2, "006.GIF");
            this.imageList1.Images.SetKeyName(3, "11.gif");
            this.imageList1.Images.SetKeyName(4, "信息.GIF");
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.neuPanel1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.fpSpread1);
            this.splitContainer2.Panel2.Tag = "";
            this.splitContainer2.Size = new System.Drawing.Size(692, 428);
            this.splitContainer2.SplitterDistance = 105;
            this.splitContainer2.TabIndex = 2;
            this.splitContainer2.TabStop = false;
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.label1);
            this.neuPanel1.Controls.Add(this.tbTotCost);
            this.neuPanel1.Controls.Add(this.txtMemo);
            this.neuPanel1.Controls.Add(this.neuLabel5);
            this.neuPanel1.Controls.Add(this.cmbDept);
            this.neuPanel1.Controls.Add(this.neuLabel4);
            this.neuPanel1.Controls.Add(this.txtSpell);
            this.neuPanel1.Controls.Add(this.neuLabel3);
            this.neuPanel1.Controls.Add(this.txtInput);
            this.neuPanel1.Controls.Add(this.neuLabel2);
            this.neuPanel1.Controls.Add(this.chkValid);
            this.neuPanel1.Controls.Add(this.txtName);
            this.neuPanel1.Controls.Add(this.neuLabel1);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(692, 105);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(379, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "金额:";
            // 
            // tbTotCost
            // 
            this.tbTotCost.Enabled = false;
            this.tbTotCost.ForeColor = System.Drawing.Color.Red;
            this.tbTotCost.Location = new System.Drawing.Point(418, 12);
            this.tbTotCost.Name = "tbTotCost";
            this.tbTotCost.Size = new System.Drawing.Size(105, 21);
            this.tbTotCost.TabIndex = 11;
            // 
            // txtMemo
            // 
            this.txtMemo.IsEnter2Tab = false;
            this.txtMemo.Location = new System.Drawing.Point(70, 74);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(453, 21);
            this.txtMemo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtMemo.TabIndex = 10;
            this.txtMemo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMemo_KeyDown);
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Location = new System.Drawing.Point(13, 76);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(59, 12);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 9;
            this.neuLabel5.Text = "备    注:";
            // 
            // cmbDept
            // 
            this.cmbDept.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbDept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDept.FormattingEnabled = true;
            this.cmbDept.IsEnter2Tab = false;
            this.cmbDept.IsFlat = false;
            this.cmbDept.IsLike = true;
            this.cmbDept.IsListOnly = false;
            this.cmbDept.IsPopForm = true;
            this.cmbDept.IsShowCustomerList = false;
            this.cmbDept.IsShowID = false;
            this.cmbDept.Location = new System.Drawing.Point(418, 41);
            this.cmbDept.Name = "cmbDept";
            this.cmbDept.ShowCustomerList = false;
            this.cmbDept.ShowID = false;
            this.cmbDept.Size = new System.Drawing.Size(105, 20);
            this.cmbDept.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbDept.TabIndex = 8;
            this.cmbDept.Tag = "";
            this.cmbDept.ToolBarUse = false;
            this.cmbDept.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbDept_KeyDown);
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.ForeColor = System.Drawing.Color.Red;
            this.neuLabel4.Location = new System.Drawing.Point(356, 46);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(59, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 7;
            this.neuLabel4.Text = "所属科室:";
            // 
            // txtSpell
            // 
            this.txtSpell.IsEnter2Tab = false;
            this.txtSpell.Location = new System.Drawing.Point(250, 41);
            this.txtSpell.Name = "txtSpell";
            this.txtSpell.Size = new System.Drawing.Size(100, 21);
            this.txtSpell.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtSpell.TabIndex = 6;
            this.txtSpell.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSpell_KeyDown);
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(182, 45);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(59, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 5;
            this.neuLabel3.Text = "拼 音 码:";
            // 
            // txtInput
            // 
            this.txtInput.IsEnter2Tab = false;
            this.txtInput.Location = new System.Drawing.Point(71, 41);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(102, 21);
            this.txtInput.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtInput.TabIndex = 4;
            this.txtInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInput_KeyDown);
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(13, 45);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(59, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 3;
            this.neuLabel2.Text = "助 记 码:";
            // 
            // chkValid
            // 
            this.chkValid.AutoSize = true;
            this.chkValid.Location = new System.Drawing.Point(287, 14);
            this.chkValid.Name = "chkValid";
            this.chkValid.Size = new System.Drawing.Size(72, 16);
            this.chkValid.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chkValid.TabIndex = 2;
            this.chkValid.Text = "是否有效";
            this.chkValid.UseVisualStyleBackColor = true;
            this.chkValid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkValid_KeyDown);
            // 
            // txtName
            // 
            this.txtName.IsEnter2Tab = false;
            this.txtName.Location = new System.Drawing.Point(70, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(212, 21);
            this.txtName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtName.TabIndex = 1;
            this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.ForeColor = System.Drawing.Color.Red;
            this.neuLabel1.Location = new System.Drawing.Point(11, 16);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(59, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 0;
            this.neuLabel1.Text = "组套名称:";
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "3.0.2004.2005";
            this.fpSpread1.AccessibleDescription = "fpSpread1, Sheet1, Row 0, Column 0, ";
            this.fpSpread1.BackColor = System.Drawing.SystemColors.Control;
            this.fpSpread1.ButtonDrawMode = FarPoint.Win.Spread.ButtonDrawModes.CurrentCell;
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread1.EditModePermanent = true;
            this.fpSpread1.EditModeReplace = true;
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.Location = new System.Drawing.Point(0, 0);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1});
            this.fpSpread1.Size = new System.Drawing.Size(692, 319);
            this.fpSpread1.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread1.TextTipAppearance = tipAppearance1;
            this.fpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.EditModeOn += new System.EventHandler(this.fpSpread1_EditModeOn);
            this.fpSpread1.EditChange += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.fpSpread1_EditChange);
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.ColumnCount = 11;
            this.fpSpread1_Sheet1.RowCount = 0;
            this.fpSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "编码";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "项目名称";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "价格";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "数量";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "单位";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "执行科室";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "组合号";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "备注";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "操作员";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "操作时间";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "序号";
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.fpSpread1_Sheet1.Columns.Get(0).Label = "编码";
            this.fpSpread1_Sheet1.Columns.Get(0).Locked = true;
            this.fpSpread1_Sheet1.Columns.Get(0).Width = 52F;
            this.fpSpread1_Sheet1.Columns.Get(1).Label = "项目名称";
            this.fpSpread1_Sheet1.Columns.Get(1).Locked = false;
            this.fpSpread1_Sheet1.Columns.Get(1).Width = 201F;
            this.fpSpread1_Sheet1.Columns.Get(2).CellType = numberCellType1;
            this.fpSpread1_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.fpSpread1_Sheet1.Columns.Get(2).Label = "价格";
            this.fpSpread1_Sheet1.Columns.Get(2).Locked = true;
            this.fpSpread1_Sheet1.Columns.Get(2).Width = 76F;
            this.fpSpread1_Sheet1.Columns.Get(3).CellType = numberCellType2;
            this.fpSpread1_Sheet1.Columns.Get(3).Label = "数量";
            this.fpSpread1_Sheet1.Columns.Get(3).Locked = false;
            this.fpSpread1_Sheet1.Columns.Get(3).Width = 66F;
            this.fpSpread1_Sheet1.Columns.Get(4).Label = "单位";
            this.fpSpread1_Sheet1.Columns.Get(4).Locked = true;
            this.fpSpread1_Sheet1.Columns.Get(5).Label = "执行科室";
            this.fpSpread1_Sheet1.Columns.Get(5).Width = 90F;
            this.fpSpread1_Sheet1.Columns.Get(6).Label = "组合号";
            this.fpSpread1_Sheet1.Columns.Get(6).Width = 71F;
            this.fpSpread1_Sheet1.Columns.Get(7).Label = "备注";
            this.fpSpread1_Sheet1.Columns.Get(7).Locked = false;
            this.fpSpread1_Sheet1.Columns.Get(7).Width = 133F;
            this.fpSpread1_Sheet1.Columns.Get(8).Label = "操作员";
            this.fpSpread1_Sheet1.Columns.Get(8).Locked = true;
            this.fpSpread1_Sheet1.Columns.Get(8).Width = 0F;
            this.fpSpread1_Sheet1.Columns.Get(9).Label = "操作时间";
            this.fpSpread1_Sheet1.Columns.Get(9).Locked = true;
            this.fpSpread1_Sheet1.Columns.Get(9).Width = 150F;
            this.fpSpread1_Sheet1.Columns.Get(10).Label = "序号";
            this.fpSpread1_Sheet1.Columns.Get(10).Width = 0F;
            this.fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 19F;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.Rows.Default.Height = 22F;
            this.fpSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSpread1_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.fpSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpSpread1.SetActiveViewport(0, 1, 0);
            // 
            // ucGroupDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.Name = "ucGroupDetail";
            this.Size = new System.Drawing.Size(823, 428);
            this.Load += new System.EventHandler(this.ucGroupDetail_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private FS.FrameWork.WinForms.Controls.NeuTreeView neuTreeView1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox chkValid;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtName;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FarPoint.Win.Spread.FpSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDept;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtSpell;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtInput;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtMemo;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbTotCost;
    }
}
