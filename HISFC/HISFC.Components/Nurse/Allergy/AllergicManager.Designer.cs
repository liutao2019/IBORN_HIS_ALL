namespace FS.HISFC.Components.Nurse.Allergy
{
    partial class AllergicManager
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.DateTimeCellType dateTimeCellType1 = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AllergicManager));
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType2 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.fpAllergic = new FS.FrameWork.WinForms.Controls.NeuFpEnter(this.components);
            this.fpAllergic_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.btCancel = new System.Windows.Forms.Button();
            this.btSave = new System.Windows.Forms.Button();
            this.tbAdd = new System.Windows.Forms.Button();
            this.lblPatientInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            ((System.ComponentModel.ISupportInitialize)(this.fpAllergic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpAllergic_Sheet1)).BeginInit();
            this.neuPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // fpAllergic
            // 
            this.fpAllergic.About = "3.0.2004.2005";
            this.fpAllergic.AccessibleDescription = "fpEnter1, Sheet1, Row 0, Column 0, ";
            this.fpAllergic.AllowDragFill = true;
            this.fpAllergic.AllowDrop = true;
            this.fpAllergic.BackColor = System.Drawing.Color.Azure;
            this.fpAllergic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fpAllergic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpAllergic.EditModePermanent = true;
            this.fpAllergic.EditModeReplace = true;
            this.fpAllergic.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpAllergic.Location = new System.Drawing.Point(0, 71);
            this.fpAllergic.Name = "fpAllergic";
            this.fpAllergic.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpAllergic.SelectNone = false;
            this.fpAllergic.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpAllergic_Sheet1});
            this.fpAllergic.ShowListWhenOfFocus = false;
            this.fpAllergic.Size = new System.Drawing.Size(852, 453);
            this.fpAllergic.TabIndex = 2;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpAllergic.TextTipAppearance = tipAppearance1;
            this.fpAllergic.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpAllergic_Sheet1
            // 
            this.fpAllergic_Sheet1.Reset();
            this.fpAllergic_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpAllergic_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpAllergic_Sheet1.ColumnCount = 12;
            this.fpAllergic_Sheet1.RowCount = 0;
            this.fpAllergic_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.SystemColors.Control, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, true, true, true);
            this.fpAllergic_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "过敏类别";
            this.fpAllergic_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "过敏药品";
            this.fpAllergic_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "药理作用";
            this.fpAllergic_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "初诊";
            this.fpAllergic_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "皮试药品";
            this.fpAllergic_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "诊断医师代码";
            this.fpAllergic_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "备注";
            this.fpAllergic_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "有效性";
            this.fpAllergic_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "操作员";
            this.fpAllergic_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "操作时间";
            this.fpAllergic_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "作废人";
            this.fpAllergic_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "作废时间";
            this.fpAllergic_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpAllergic_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpAllergic_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.fpAllergic_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpAllergic_Sheet1.ColumnHeader.Rows.Get(0).Height = 26F;
            this.fpAllergic_Sheet1.Columns.Get(0).Label = "过敏类别";
            this.fpAllergic_Sheet1.Columns.Get(0).Width = 78F;
            this.fpAllergic_Sheet1.Columns.Get(1).CellType = textCellType1;
            this.fpAllergic_Sheet1.Columns.Get(1).Label = "过敏药品";
            this.fpAllergic_Sheet1.Columns.Get(1).Width = 120F;
            this.fpAllergic_Sheet1.Columns.Get(2).Label = "药理作用";
            this.fpAllergic_Sheet1.Columns.Get(2).Width = 120F;
            this.fpAllergic_Sheet1.Columns.Get(3).CellType = checkBoxCellType1;
            this.fpAllergic_Sheet1.Columns.Get(3).Label = "初诊";
            this.fpAllergic_Sheet1.Columns.Get(3).Visible = false;
            this.fpAllergic_Sheet1.Columns.Get(3).Width = 34F;
            dateTimeCellType1.Calendar = ((System.Globalization.Calendar)(resources.GetObject("dateTimeCellType1.Calendar")));
            dateTimeCellType1.CalendarDayFont = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dateTimeCellType1.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText;
            dateTimeCellType1.DateDefault = new System.DateTime(2008, 1, 3, 19, 50, 13, 0);
            dateTimeCellType1.TimeDefault = new System.DateTime(2008, 1, 3, 19, 50, 13, 0);
            this.fpAllergic_Sheet1.Columns.Get(4).CellType = dateTimeCellType1;
            this.fpAllergic_Sheet1.Columns.Get(4).Label = "皮试药品";
            this.fpAllergic_Sheet1.Columns.Get(4).Width = 120F;
            this.fpAllergic_Sheet1.Columns.Get(5).Label = "诊断医师代码";
            this.fpAllergic_Sheet1.Columns.Get(5).Visible = false;
            this.fpAllergic_Sheet1.Columns.Get(5).Width = 88F;
            this.fpAllergic_Sheet1.Columns.Get(6).CellType = textCellType2;
            this.fpAllergic_Sheet1.Columns.Get(6).Label = "备注";
            this.fpAllergic_Sheet1.Columns.Get(6).Width = 200F;
            this.fpAllergic_Sheet1.Columns.Get(7).CellType = checkBoxCellType2;
            this.fpAllergic_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpAllergic_Sheet1.Columns.Get(7).Label = "有效性";
            this.fpAllergic_Sheet1.Columns.Get(9).Label = "操作时间";
            this.fpAllergic_Sheet1.Columns.Get(9).Width = 100F;
            this.fpAllergic_Sheet1.Columns.Get(11).Label = "作废时间";
            this.fpAllergic_Sheet1.Columns.Get(11).Width = 100F;
            this.fpAllergic_Sheet1.GrayAreaBackColor = System.Drawing.Color.Transparent;
            this.fpAllergic_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpAllergic_Sheet1.RowHeader.Columns.Get(0).Width = 22F;
            this.fpAllergic_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpAllergic_Sheet1.RowHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpAllergic_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.fpAllergic_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpAllergic_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.fpAllergic_Sheet1.SheetCornerStyle.Locked = false;
            this.fpAllergic_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.fpAllergic_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpAllergic_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpAllergic.SetActiveViewport(0, 1, 0);
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.label1);
            this.neuPanel2.Controls.Add(this.btCancel);
            this.neuPanel2.Controls.Add(this.btSave);
            this.neuPanel2.Controls.Add(this.tbAdd);
            this.neuPanel2.Controls.Add(this.lblPatientInfo);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel2.Location = new System.Drawing.Point(0, 0);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(852, 71);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.OrangeRed;
            this.label1.Location = new System.Drawing.Point(3, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(241, 44);
            this.label1.TabIndex = 12;
            this.label1.Text = "红色为已作废的过敏记录,过敏记录保存后24小时内可以修改,超过此日期,只可以作废,不可以删除";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(447, 13);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 7;
            this.btCancel.Text = "作废";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(576, 13);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(75, 23);
            this.btSave.TabIndex = 6;
            this.btSave.Text = "保存";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // tbAdd
            // 
            this.tbAdd.Location = new System.Drawing.Point(304, 13);
            this.tbAdd.Name = "tbAdd";
            this.tbAdd.Size = new System.Drawing.Size(75, 23);
            this.tbAdd.TabIndex = 4;
            this.tbAdd.Text = "增加";
            this.tbAdd.UseVisualStyleBackColor = true;
            this.tbAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // lblPatientInfo
            // 
            this.lblPatientInfo.AutoSize = true;
            this.lblPatientInfo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPatientInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblPatientInfo.Location = new System.Drawing.Point(14, 48);
            this.lblPatientInfo.Name = "lblPatientInfo";
            this.lblPatientInfo.Size = new System.Drawing.Size(49, 14);
            this.lblPatientInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblPatientInfo.TabIndex = 3;
            this.lblPatientInfo.Text = "姓名：";
            // 
            // AllergicManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.fpAllergic);
            this.Controls.Add(this.neuPanel2);
            this.Name = "AllergicManager";
            this.Size = new System.Drawing.Size(852, 524);
            ((System.ComponentModel.ISupportInitialize)(this.fpAllergic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpAllergic_Sheet1)).EndInit();
            this.neuPanel2.ResumeLayout(false);
            this.neuPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuFpEnter fpEnter1;
        private FarPoint.Win.Spread.SheetView fpAllergic_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblPatientInfo;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button tbAdd;
        private System.Windows.Forms.Label label1;
        private FS.FrameWork.WinForms.Controls.NeuFpEnter fpAllergic;
    }
}
