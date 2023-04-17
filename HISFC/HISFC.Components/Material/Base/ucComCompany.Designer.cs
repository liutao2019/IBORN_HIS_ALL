﻿namespace FS.HISFC.Components.Material.Base
{
    partial class ucComCompany
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
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.fpCompany = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpCompany_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.txtQueryCode = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.chbMisty = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.cmbLeach = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            ((System.ComponentModel.ISupportInitialize)(this.fpCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpCompany_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // fpCompany
            // 
            this.fpCompany.About = "2.5.2007.2005";
            this.fpCompany.AccessibleDescription = "fpCompany, Sheet1, Row 0, Column 0, ";
            this.fpCompany.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.fpCompany.BackColor = System.Drawing.Color.White;
            this.fpCompany.FileName = "";
            this.fpCompany.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpCompany.IsAutoSaveGridStatus = false;
            this.fpCompany.IsCanCustomConfigColumn = false;
            this.fpCompany.Location = new System.Drawing.Point(0, 32);
            this.fpCompany.Name = "fpCompany";
            this.fpCompany.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpCompany_Sheet1});
            this.fpCompany.Size = new System.Drawing.Size(718, 344);
            this.fpCompany.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpCompany.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpCompany.TextTipAppearance = tipAppearance1;
            this.fpCompany.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpCompany.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpMaterialQuery_CellDoubleClick);
            this.fpCompany.LeaveCell += new FarPoint.Win.Spread.LeaveCellEventHandler(this.fpCompany_LeaveCell);
            // 
            // fpCompany_Sheet1
            // 
            this.fpCompany_Sheet1.Reset();
            this.fpCompany_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpCompany_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpCompany_Sheet1.ColumnCount = 19;
            this.fpCompany_Sheet1.RowCount = 30;
            this.fpCompany_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpCompany_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "公司编码";
            this.fpCompany_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "公司名称";
            this.fpCompany_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "公司地址";
            this.fpCompany_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "公司电话";
            this.fpCompany_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "GMP信息";
            this.fpCompany_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "GSP信息";
            this.fpCompany_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "拼音码";
            this.fpCompany_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "五笔码";
            this.fpCompany_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "自定义码";
            this.fpCompany_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "类型";
            this.fpCompany_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "开户银行";
            this.fpCompany_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "开户帐号";
            this.fpCompany_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "加价率";
            this.fpCompany_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "备注";
            this.fpCompany_Sheet1.ColumnHeader.Cells.Get(0, 14).Value = "是否有效";
            this.fpCompany_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpCompany_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.fpCompany_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpCompany_Sheet1.Columns.Get(0).Label = "公司编码";
            this.fpCompany_Sheet1.Columns.Get(0).Visible = false;
            this.fpCompany_Sheet1.Columns.Get(0).Width = 79F;
            this.fpCompany_Sheet1.Columns.Get(1).Label = "公司名称";
            this.fpCompany_Sheet1.Columns.Get(1).Locked = true;
            this.fpCompany_Sheet1.Columns.Get(1).Width = 124F;
            this.fpCompany_Sheet1.Columns.Get(2).Label = "公司地址";
            this.fpCompany_Sheet1.Columns.Get(2).Locked = true;
            this.fpCompany_Sheet1.Columns.Get(2).Width = 133F;
            this.fpCompany_Sheet1.Columns.Get(3).Label = "公司电话";
            this.fpCompany_Sheet1.Columns.Get(3).Locked = true;
            this.fpCompany_Sheet1.Columns.Get(3).Width = 63F;
            this.fpCompany_Sheet1.Columns.Get(4).Label = "GMP信息";
            this.fpCompany_Sheet1.Columns.Get(4).Locked = true;
            this.fpCompany_Sheet1.Columns.Get(4).Width = 57F;
            this.fpCompany_Sheet1.Columns.Get(5).Label = "GSP信息";
            this.fpCompany_Sheet1.Columns.Get(5).Locked = true;
            this.fpCompany_Sheet1.Columns.Get(5).Width = 57F;
            this.fpCompany_Sheet1.Columns.Get(6).Label = "拼音码";
            this.fpCompany_Sheet1.Columns.Get(6).Locked = true;
            this.fpCompany_Sheet1.Columns.Get(6).Width = 50F;
            this.fpCompany_Sheet1.Columns.Get(7).Label = "五笔码";
            this.fpCompany_Sheet1.Columns.Get(7).Locked = true;
            this.fpCompany_Sheet1.Columns.Get(7).Width = 51F;
            this.fpCompany_Sheet1.Columns.Get(8).Label = "自定义码";
            this.fpCompany_Sheet1.Columns.Get(8).Locked = true;
            this.fpCompany_Sheet1.Columns.Get(8).Width = 63F;
            this.fpCompany_Sheet1.Columns.Get(9).Label = "类型";
            this.fpCompany_Sheet1.Columns.Get(9).Locked = true;
            this.fpCompany_Sheet1.Columns.Get(9).Visible = false;
            this.fpCompany_Sheet1.Columns.Get(9).Width = 39F;
            this.fpCompany_Sheet1.Columns.Get(10).Label = "开户银行";
            this.fpCompany_Sheet1.Columns.Get(10).Locked = true;
            this.fpCompany_Sheet1.Columns.Get(10).Width = 63F;
            this.fpCompany_Sheet1.Columns.Get(11).Label = "开户帐号";
            this.fpCompany_Sheet1.Columns.Get(11).Locked = true;
            this.fpCompany_Sheet1.Columns.Get(11).Width = 63F;
            this.fpCompany_Sheet1.Columns.Get(12).Label = "加价率";
            this.fpCompany_Sheet1.Columns.Get(12).Locked = true;
            this.fpCompany_Sheet1.Columns.Get(12).Width = 52F;
            this.fpCompany_Sheet1.Columns.Get(13).Label = "备注";
            this.fpCompany_Sheet1.Columns.Get(13).Locked = true;
            this.fpCompany_Sheet1.Columns.Get(13).Width = 39F;
            this.fpCompany_Sheet1.Columns.Get(14).CellType = checkBoxCellType1;
            this.fpCompany_Sheet1.Columns.Get(14).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpCompany_Sheet1.Columns.Get(14).Label = "是否有效";
            this.fpCompany_Sheet1.Columns.Get(14).Visible = false;
            this.fpCompany_Sheet1.Columns.Get(14).Width = 63F;
            this.fpCompany_Sheet1.Columns.Get(15).Visible = false;
            this.fpCompany_Sheet1.Columns.Get(16).Visible = false;
            this.fpCompany_Sheet1.Columns.Get(17).Visible = false;
            this.fpCompany_Sheet1.Columns.Get(18).Visible = false;
            this.fpCompany_Sheet1.DataAutoSizeColumns = false;
            this.fpCompany_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpCompany_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpCompany_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.fpCompany_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpCompany_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.fpCompany_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpCompany_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.fpCompany_Sheet1.SheetCornerStyle.Locked = false;
            this.fpCompany_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.fpCompany_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpCompany.SetViewportLeftColumn(0, 7);
            // 
            // txtQueryCode
            // 
            this.txtQueryCode.IsEnter2Tab = false;
            this.txtQueryCode.Location = new System.Drawing.Point(58, 8);
            this.txtQueryCode.Name = "txtQueryCode";
            this.txtQueryCode.Size = new System.Drawing.Size(130, 21);
            this.txtQueryCode.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtQueryCode.TabIndex = 1;
            this.txtQueryCode.TextChanged += new System.EventHandler(this.txtQueryCode_TextChanged);
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(10, 12);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(47, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 2;
            this.neuLabel1.Text = "查询码:";
            // 
            // chbMisty
            // 
            this.chbMisty.AutoSize = true;
            this.chbMisty.Location = new System.Drawing.Point(194, 10);
            this.chbMisty.Name = "chbMisty";
            this.chbMisty.Size = new System.Drawing.Size(72, 16);
            this.chbMisty.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chbMisty.TabIndex = 3;
            this.chbMisty.Text = "模糊查询";
            this.chbMisty.UseVisualStyleBackColor = true;
            // 
            // cmbLeach
            // 
            this.cmbLeach.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbLeach.FormattingEnabled = true;
            this.cmbLeach.IsEnter2Tab = false;
            this.cmbLeach.IsFlat = true;
            this.cmbLeach.IsLike = true;
            this.cmbLeach.Items.AddRange(new object[] {
            "营业执照",
            "经营许可证",
            "税务登记证",
            "组织机构代码证"});
            this.cmbLeach.Location = new System.Drawing.Point(455, 7);
            this.cmbLeach.Name = "cmbLeach";
            this.cmbLeach.PopForm = null;
            this.cmbLeach.ShowCustomerList = false;
            this.cmbLeach.ShowID = false;
            this.cmbLeach.Size = new System.Drawing.Size(137, 20);
            this.cmbLeach.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbLeach.TabIndex = 4;
            this.cmbLeach.Tag = "";
            this.cmbLeach.ToolBarUse = false;
            this.cmbLeach.SelectedIndexChanged += new System.EventHandler(this.cmbLeach_SelectedIndexChanged);
            this.cmbLeach.TextChanged += new System.EventHandler(this.cmbLeach_TextChanged);
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(372, 12);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(77, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 5;
            this.neuLabel2.Text = "过滤过期数据";
            // 
            // ucComCompany
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuLabel2);
            this.Controls.Add(this.cmbLeach);
            this.Controls.Add(this.chbMisty);
            this.Controls.Add(this.neuLabel1);
            this.Controls.Add(this.txtQueryCode);
            this.Controls.Add(this.fpCompany);
            this.Name = "ucComCompany";
            this.Size = new System.Drawing.Size(718, 376);
            ((System.ComponentModel.ISupportInitialize)(this.fpCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpCompany_Sheet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuSpread fpCompany;
        private FarPoint.Win.Spread.SheetView fpCompany_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtQueryCode;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox chbMisty;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbLeach;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
    }
}
