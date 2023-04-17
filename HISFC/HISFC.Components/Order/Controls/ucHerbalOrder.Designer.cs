﻿namespace FS.HISFC.Components.Order.Controls
{
    partial class ucHerbalOrder
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
            this.neuGroupBox1 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.cmbFrequency = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.cmbMemo = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.dtEnd = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.dtBegin = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.txtNum = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cmbOrderType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuGroupBox2 = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.btnSaveGroup = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnNewRecipe = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnCancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnOK = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnDel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.fpEnter1 = new FS.FrameWork.WinForms.Controls.NeuFpEnter(this.components);
            this.fpEnter1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.pnInputItem = new System.Windows.Forms.Panel();
            this.neuGroupBox1.SuspendLayout();
            this.neuGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpEnter1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpEnter1_Sheet1)).BeginInit();
            this.pnInputItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuGroupBox1
            // 
            this.neuGroupBox1.Controls.Add(this.cmbFrequency);
            this.neuGroupBox1.Controls.Add(this.cmbMemo);
            this.neuGroupBox1.Controls.Add(this.dtEnd);
            this.neuGroupBox1.Controls.Add(this.neuLabel5);
            this.neuGroupBox1.Controls.Add(this.dtBegin);
            this.neuGroupBox1.Controls.Add(this.txtNum);
            this.neuGroupBox1.Controls.Add(this.neuLabel3);
            this.neuGroupBox1.Controls.Add(this.neuLabel4);
            this.neuGroupBox1.Controls.Add(this.neuLabel2);
            this.neuGroupBox1.Controls.Add(this.neuLabel1);
            this.neuGroupBox1.Controls.Add(this.cmbOrderType);
            this.neuGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.neuGroupBox1.Name = "neuGroupBox1";
            this.neuGroupBox1.Size = new System.Drawing.Size(843, 41);
            this.neuGroupBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox1.TabIndex = 0;
            this.neuGroupBox1.TabStop = false;
            // 
            // cmbFrequency
            // 
            this.cmbFrequency.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbFrequency.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbFrequency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFrequency.IsEnter2Tab = false;
            this.cmbFrequency.IsFlat = false;
            this.cmbFrequency.IsLike = true;
            this.cmbFrequency.IsListOnly = false;
            this.cmbFrequency.IsPopForm = true;
            this.cmbFrequency.IsShowCustomerList = false;
            this.cmbFrequency.IsShowID = false;
            this.cmbFrequency.Location = new System.Drawing.Point(132, 14);
            this.cmbFrequency.Name = "cmbFrequency";
            this.cmbFrequency.ShowCustomerList = false;
            this.cmbFrequency.ShowID = false;
            this.cmbFrequency.Size = new System.Drawing.Size(68, 20);
            this.cmbFrequency.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbFrequency.TabIndex = 1;
            this.cmbFrequency.Tag = "";
            this.cmbFrequency.ToolBarUse = false;
            this.cmbFrequency.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbFrequency_KeyDown);
            // 
            // cmbMemo
            // 
            this.cmbMemo.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbMemo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbMemo.IsEnter2Tab = false;
            this.cmbMemo.IsFlat = false;
            this.cmbMemo.IsLike = true;
            this.cmbMemo.IsListOnly = false;
            this.cmbMemo.IsPopForm = true;
            this.cmbMemo.IsShowCustomerList = true;
            this.cmbMemo.IsShowID = false;
            this.cmbMemo.Location = new System.Drawing.Point(347, 14);
            this.cmbMemo.Name = "cmbMemo";
            this.cmbMemo.ShowCustomerList = true;
            this.cmbMemo.ShowID = false;
            this.cmbMemo.Size = new System.Drawing.Size(128, 20);
            this.cmbMemo.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbMemo.TabIndex = 3;
            this.cmbMemo.Tag = "";
            this.cmbMemo.ToolBarUse = false;
            this.cmbMemo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbMemo_KeyDown);
            // 
            // dtEnd
            // 
            this.dtEnd.Checked = false;
            this.dtEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEnd.IsEnter2Tab = false;
            this.dtEnd.Location = new System.Drawing.Point(685, 12);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.ShowCheckBox = true;
            this.dtEnd.Size = new System.Drawing.Size(152, 21);
            this.dtEnd.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtEnd.TabIndex = 5;
            this.dtEnd.Visible = false;
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel5.Location = new System.Drawing.Point(652, 16);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(29, 12);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 4;
            this.neuLabel5.Text = "停止";
            this.neuLabel5.Visible = false;
            // 
            // dtBegin
            // 
            this.dtBegin.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtBegin.IsEnter2Tab = false;
            this.dtBegin.Location = new System.Drawing.Point(514, 12);
            this.dtBegin.Name = "dtBegin";
            this.dtBegin.Size = new System.Drawing.Size(137, 21);
            this.dtBegin.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtBegin.TabIndex = 4;
            this.dtBegin.Visible = false;
            // 
            // txtNum
            // 
            this.txtNum.IsEnter2Tab = false;
            this.txtNum.Location = new System.Drawing.Point(237, 13);
            this.txtNum.Name = "txtNum";
            this.txtNum.Size = new System.Drawing.Size(47, 21);
            this.txtNum.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtNum.TabIndex = 2;
            this.txtNum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNum_KeyDown);
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel3.Location = new System.Drawing.Point(302, 17);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(29, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 1;
            this.neuLabel3.Text = "用法";
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel4.Location = new System.Drawing.Point(481, 16);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(29, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 1;
            this.neuLabel4.Text = "开始";
            this.neuLabel4.Visible = false;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel2.Location = new System.Drawing.Point(203, 17);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(29, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 1;
            this.neuLabel2.Text = "剂数";
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.ForeColor = System.Drawing.Color.Blue;
            this.neuLabel1.Location = new System.Drawing.Point(97, 17);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(29, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 1;
            this.neuLabel1.Text = "频次";
            // 
            // cmbOrderType
            // 
            this.cmbOrderType.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbOrderType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbOrderType.FormattingEnabled = true;
            this.cmbOrderType.IsEnter2Tab = false;
            this.cmbOrderType.IsFlat = false;
            this.cmbOrderType.IsLike = true;
            this.cmbOrderType.IsListOnly = false;
            this.cmbOrderType.IsPopForm = true;
            this.cmbOrderType.IsShowCustomerList = false;
            this.cmbOrderType.IsShowID = false;
            this.cmbOrderType.Location = new System.Drawing.Point(3, 13);
            this.cmbOrderType.Name = "cmbOrderType";
            this.cmbOrderType.ShowCustomerList = false;
            this.cmbOrderType.ShowID = false;
            this.cmbOrderType.Size = new System.Drawing.Size(92, 20);
            this.cmbOrderType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbOrderType.TabIndex = 0;
            this.cmbOrderType.Tag = "";
            this.cmbOrderType.ToolBarUse = false;
            this.cmbOrderType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbOrderType_KeyDown);
            // 
            // neuGroupBox2
            // 
            this.neuGroupBox2.Controls.Add(this.btnSaveGroup);
            this.neuGroupBox2.Controls.Add(this.btnNewRecipe);
            this.neuGroupBox2.Controls.Add(this.btnCancel);
            this.neuGroupBox2.Controls.Add(this.btnOK);
            this.neuGroupBox2.Controls.Add(this.btnDel);
            this.neuGroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuGroupBox2.Location = new System.Drawing.Point(0, 463);
            this.neuGroupBox2.Name = "neuGroupBox2";
            this.neuGroupBox2.Size = new System.Drawing.Size(843, 37);
            this.neuGroupBox2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuGroupBox2.TabIndex = 1;
            this.neuGroupBox2.TabStop = false;
            // 
            // btnSaveGroup
            // 
            this.btnSaveGroup.Location = new System.Drawing.Point(509, 11);
            this.btnSaveGroup.Name = "btnSaveGroup";
            this.btnSaveGroup.Size = new System.Drawing.Size(75, 23);
            this.btnSaveGroup.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnSaveGroup.TabIndex = 6;
            this.btnSaveGroup.Text = "存组套";
            this.btnSaveGroup.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnSaveGroup.UseVisualStyleBackColor = true;
            this.btnSaveGroup.Click += new System.EventHandler(this.btnSaveGroup_Click);
            // 
            // btnNewRecipe
            // 
            this.btnNewRecipe.Location = new System.Drawing.Point(20, 11);
            this.btnNewRecipe.Name = "btnNewRecipe";
            this.btnNewRecipe.Size = new System.Drawing.Size(75, 23);
            this.btnNewRecipe.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnNewRecipe.TabIndex = 10;
            this.btnNewRecipe.Text = "新 开";
            this.btnNewRecipe.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnNewRecipe.UseVisualStyleBackColor = true;
            this.btnNewRecipe.Click += new System.EventHandler(this.btnNewRecipe_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(752, 11);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取 消";
            this.btnCancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(671, 11);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "确 定";
            this.btnOK.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(590, 11);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 23);
            this.btnDel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnDel.TabIndex = 7;
            this.btnDel.Text = "删 除";
            this.btnDel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnDel.UseVisualStyleBackColor = true;
            // 
            // fpEnter1
            // 
            this.fpEnter1.About = "3.0.2004.2005";
            this.fpEnter1.AccessibleDescription = "fpEnter1, Sheet1";
            this.fpEnter1.BackColor = System.Drawing.SystemColors.Control;
            this.fpEnter1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpEnter1.EditModePermanent = true;
            this.fpEnter1.EditModeReplace = true;
            this.fpEnter1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpEnter1.Location = new System.Drawing.Point(0, 41);
            this.fpEnter1.Name = "fpEnter1";
            this.fpEnter1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpEnter1.SelectNone = false;
            this.fpEnter1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpEnter1_Sheet1});
            this.fpEnter1.ShowListWhenOfFocus = false;
            this.fpEnter1.Size = new System.Drawing.Size(843, 422);
            this.fpEnter1.TabIndex = 4;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpEnter1.TextTipAppearance = tipAppearance1;
            this.fpEnter1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpEnter1_Sheet1
            // 
            this.fpEnter1_Sheet1.Reset();
            this.fpEnter1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpEnter1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpEnter1_Sheet1.ColumnCount = 6;
            this.fpEnter1_Sheet1.RowCount = 0;
            this.fpEnter1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "药品名称";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "规格";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "价格";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "数量";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "单位";
            this.fpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "备注";
            this.fpEnter1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpEnter1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpEnter1_Sheet1.Columns.Get(0).Label = "药品名称";
            this.fpEnter1_Sheet1.Columns.Get(0).Width = 136F;
            this.fpEnter1_Sheet1.Columns.Get(1).Label = "规格";
            this.fpEnter1_Sheet1.Columns.Get(1).Width = 97F;
            this.fpEnter1_Sheet1.Columns.Get(2).Label = "价格";
            this.fpEnter1_Sheet1.Columns.Get(2).Width = 75F;
            this.fpEnter1_Sheet1.Columns.Get(3).Label = "数量";
            this.fpEnter1_Sheet1.Columns.Get(3).Width = 73F;
            this.fpEnter1_Sheet1.Columns.Get(4).Label = "单位";
            this.fpEnter1_Sheet1.Columns.Get(4).Width = 73F;
            this.fpEnter1_Sheet1.Columns.Get(5).Label = "备注";
            this.fpEnter1_Sheet1.Columns.Get(5).Width = 120F;
            this.fpEnter1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpEnter1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpEnter1_Sheet1.RowHeader.Columns.Get(0).Width = 19F;
            this.fpEnter1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpEnter1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpEnter1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpEnter1_Sheet1.SheetCornerStyle.Locked = false;
            this.fpEnter1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.fpEnter1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpEnter1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpEnter1.SetActiveViewport(0, 1, 0);
            // 
            // pnInputItem
            // 
            this.pnInputItem.Controls.Add(this.fpEnter1);
            this.pnInputItem.Controls.Add(this.neuGroupBox1);
            this.pnInputItem.Controls.Add(this.neuGroupBox2);
            this.pnInputItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnInputItem.Location = new System.Drawing.Point(0, 0);
            this.pnInputItem.Name = "pnInputItem";
            this.pnInputItem.Size = new System.Drawing.Size(843, 500);
            this.pnInputItem.TabIndex = 5;
            // 
            // ucHerbalOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnInputItem);
            this.Name = "ucHerbalOrder";
            this.Size = new System.Drawing.Size(843, 500);
            this.neuGroupBox1.ResumeLayout(false);
            this.neuGroupBox1.PerformLayout();
            this.neuGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpEnter1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpEnter1_Sheet1)).EndInit();
            this.pnInputItem.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox1;
        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuGroupBox2;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbOrderType;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtEnd;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtBegin;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtNum;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        //{1B955310-2887-4994-94C7-80D990C80273}  修改Comobx类型
        private FS.FrameWork.WinForms.Controls.NeuFpEnter fpEnter1;
        private FarPoint.Win.Spread.SheetView fpEnter1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuButton btnCancel;
        private FS.FrameWork.WinForms.Controls.NeuButton btnOK;
        private FS.FrameWork.WinForms.Controls.NeuButton btnDel;
        private FS.FrameWork.WinForms.Controls.NeuButton btnNewRecipe;
        private FS.FrameWork.WinForms.Controls.NeuButton btnSaveGroup;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbMemo;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbFrequency;
        private System.Windows.Forms.Panel pnInputItem;
    }
}
