namespace FS.SOC.HISFC.Fee.Components.Maintenance.Pact
{
    partial class ucPactInfoManager
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
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.ncmbSystemType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ckValid = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ckUnValid = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.ckAllPact = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.txtCustomCode = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuSpread = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.fpItemGroup = new FarPoint.Win.Spread.SheetView();
            this.neuPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpItemGroup)).BeginInit();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.ncmbSystemType);
            this.neuPanel1.Controls.Add(this.neuLabel2);
            this.neuPanel1.Controls.Add(this.ckValid);
            this.neuPanel1.Controls.Add(this.ckUnValid);
            this.neuPanel1.Controls.Add(this.ckAllPact);
            this.neuPanel1.Controls.Add(this.txtCustomCode);
            this.neuPanel1.Controls.Add(this.neuLabel1);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(519, 32);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // ncmbSystemType
            // 
            this.ncmbSystemType.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.ncmbSystemType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ncmbSystemType.FormattingEnabled = true;
            this.ncmbSystemType.IsEnter2Tab = false;
            this.ncmbSystemType.IsFlat = false;
            this.ncmbSystemType.IsLike = true;
            this.ncmbSystemType.IsListOnly = false;
            this.ncmbSystemType.IsPopForm = true;
            this.ncmbSystemType.IsShowCustomerList = false;
            this.ncmbSystemType.IsShowID = false;
            this.ncmbSystemType.IsShowIDAndName = false;
            this.ncmbSystemType.Location = new System.Drawing.Point(461, 3);
            this.ncmbSystemType.Name = "ncmbSystemType";
            this.ncmbSystemType.ShowCustomerList = false;
            this.ncmbSystemType.ShowID = false;
            this.ncmbSystemType.Size = new System.Drawing.Size(72, 20);
            this.ncmbSystemType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.ncmbSystemType.TabIndex = 20;
            this.ncmbSystemType.Tag = "";
            this.ncmbSystemType.ToolBarUse = false;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(401, 8);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(65, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 19;
            this.neuLabel2.Text = "系统类别：";
            // 
            // ckValid
            // 
            this.ckValid.AutoSize = true;
            this.ckValid.Checked = true;
            this.ckValid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckValid.Location = new System.Drawing.Point(245, 7);
            this.ckValid.Name = "ckValid";
            this.ckValid.Size = new System.Drawing.Size(72, 16);
            this.ckValid.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ckValid.TabIndex = 18;
            this.ckValid.Text = "显示有效";
            this.ckValid.UseVisualStyleBackColor = true;
            // 
            // ckUnValid
            // 
            this.ckUnValid.AutoSize = true;
            this.ckUnValid.Location = new System.Drawing.Point(323, 7);
            this.ckUnValid.Name = "ckUnValid";
            this.ckUnValid.Size = new System.Drawing.Size(72, 16);
            this.ckUnValid.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ckUnValid.TabIndex = 17;
            this.ckUnValid.Text = "显示无效";
            this.ckUnValid.UseVisualStyleBackColor = true;
            // 
            // ckAllPact
            // 
            this.ckAllPact.AutoSize = true;
            this.ckAllPact.Location = new System.Drawing.Point(38, 7);
            this.ckAllPact.Name = "ckAllPact";
            this.ckAllPact.Size = new System.Drawing.Size(48, 16);
            this.ckAllPact.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.ckAllPact.TabIndex = 16;
            this.ckAllPact.Text = "全选";
            this.ckAllPact.UseVisualStyleBackColor = true;
            // 
            // txtCustomCode
            // 
            this.txtCustomCode.IsEnter2Tab = false;
            this.txtCustomCode.Location = new System.Drawing.Point(154, 4);
            this.txtCustomCode.Name = "txtCustomCode";
            this.txtCustomCode.Size = new System.Drawing.Size(85, 21);
            this.txtCustomCode.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtCustomCode.TabIndex = 3;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(92, 8);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(65, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 2;
            this.neuLabel1.Text = "自定义码：";
            // 
            // neuSpread
            // 
            this.neuSpread.About = "3.0.2004.2005";
            this.neuSpread.AccessibleDescription = "neuSpread, Sheet1, Row 0, Column 0, ";
            this.neuSpread.BackColor = System.Drawing.SystemColors.Control;
            this.neuSpread.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread.EditModeReplace = true;
            this.neuSpread.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread.Location = new System.Drawing.Point(0, 32);
            this.neuSpread.Name = "neuSpread";
            this.neuSpread.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpItemGroup});
            this.neuSpread.Size = new System.Drawing.Size(519, 344);
            this.neuSpread.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread.TabIndex = 8;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread.TextTipAppearance = tipAppearance2;
            this.neuSpread.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpItemGroup
            // 
            this.fpItemGroup.Reset();
            this.fpItemGroup.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpItemGroup.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpItemGroup.ColumnCount = 0;
            this.fpItemGroup.RowCount = 0;
            this.fpItemGroup.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpItemGroup.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpItemGroup.ColumnHeader.Rows.Get(0).Height = 25F;
            this.fpItemGroup.DefaultStyle.Locked = false;
            this.fpItemGroup.DefaultStyle.Parent = "DataAreaDefault";
            this.fpItemGroup.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpItemGroup.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpItemGroup.RowHeader.Columns.Default.Resizable = true;
            this.fpItemGroup.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpItemGroup.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpItemGroup.Rows.Default.Height = 22F;
            this.fpItemGroup.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpItemGroup.SheetCornerStyle.Locked = false;
            this.fpItemGroup.SheetCornerStyle.Parent = "HeaderDefault";
            this.fpItemGroup.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpItemGroup.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.neuSpread.SetActiveViewport(0, 1, 1);
            // 
            // ucPactInfoManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuSpread);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucPactInfoManager";
            this.Size = new System.Drawing.Size(519, 376);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpItemGroup)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        protected FS.SOC.Windows.Forms.FpSpread neuSpread;
        private FarPoint.Win.Spread.SheetView fpItemGroup;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtCustomCode;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ckAllPact;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ckUnValid;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox ckValid;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuComboBox ncmbSystemType;
    }
}
