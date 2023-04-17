namespace FS.HISFC.Components.Order.OutPatient.Controls
{
    partial class ucOutPatientRecipe
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.pnDisplay = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lblFeeInfo = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblFeeDisplay = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblDisplay = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.ucOutPatientRecipeItemSelect1 = new FS.HISFC.Components.Order.OutPatient.Controls.ucOutPatientRecipeItemSelect();
            this.pnTop = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.pnOrderPactInfo = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.rdPact4 = new System.Windows.Forms.RadioButton();
            this.rdPact3 = new System.Windows.Forms.RadioButton();
            this.rdPact2 = new System.Windows.Forms.RadioButton();
            this.rdPact1 = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.pnDisplay.SuspendLayout();
            this.pnTop.SuspendLayout();
            this.pnOrderPactInfo.SuspendLayout();
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
            this.neuSpread1.Location = new System.Drawing.Point(0, 113);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(709, 341);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 1;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance2;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(this.neuSpread1_ColumnWidthChanged);
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.Rows.Default.Height = 25F;
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.neuSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // pnDisplay
            // 
            this.pnDisplay.Controls.Add(this.lblFeeInfo);
            this.pnDisplay.Controls.Add(this.lblFeeDisplay);
            this.pnDisplay.Controls.Add(this.lblDisplay);
            this.pnDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnDisplay.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pnDisplay.Location = new System.Drawing.Point(0, 0);
            this.pnDisplay.Name = "pnDisplay";
            this.pnDisplay.Size = new System.Drawing.Size(709, 28);
            this.pnDisplay.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnDisplay.TabIndex = 2;
            this.pnDisplay.Visible = false;
            // 
            // lblFeeInfo
            // 
            this.lblFeeInfo.AutoSize = true;
            this.lblFeeInfo.Font = new System.Drawing.Font("宋体", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblFeeInfo.ForeColor = System.Drawing.Color.Blue;
            this.lblFeeInfo.Location = new System.Drawing.Point(228, 8);
            this.lblFeeInfo.Name = "lblFeeInfo";
            this.lblFeeInfo.Size = new System.Drawing.Size(70, 12);
            this.lblFeeInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblFeeInfo.TabIndex = 5;
            this.lblFeeInfo.Text = "账户余额：";
            // 
            // lblFeeDisplay
            // 
            this.lblFeeDisplay.AutoSize = true;
            this.lblFeeDisplay.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblFeeDisplay.ForeColor = System.Drawing.Color.Red;
            this.lblFeeDisplay.Location = new System.Drawing.Point(6, 25);
            this.lblFeeDisplay.Name = "lblFeeDisplay";
            this.lblFeeDisplay.Size = new System.Drawing.Size(93, 16);
            this.lblFeeDisplay.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblFeeDisplay.TabIndex = 4;
            this.lblFeeDisplay.Text = "账户余额：";
            // 
            // lblDisplay
            // 
            this.lblDisplay.AutoSize = true;
            this.lblDisplay.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDisplay.ForeColor = System.Drawing.Color.Blue;
            this.lblDisplay.Location = new System.Drawing.Point(6, 4);
            this.lblDisplay.Name = "lblDisplay";
            this.lblDisplay.Size = new System.Drawing.Size(93, 16);
            this.lblDisplay.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblDisplay.TabIndex = 3;
            this.lblDisplay.Text = "账户余额：";
            // 
            // ucOutPatientRecipeItemSelect1
            // 
            this.ucOutPatientRecipeItemSelect1.BackColor = System.Drawing.Color.White;
            this.ucOutPatientRecipeItemSelect1.CurrOrder = null;
            this.ucOutPatientRecipeItemSelect1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucOutPatientRecipeItemSelect1.IsEditGroup = false;
            this.ucOutPatientRecipeItemSelect1.Location = new System.Drawing.Point(0, 28);
            this.ucOutPatientRecipeItemSelect1.Name = "ucOutPatientRecipeItemSelect1";
            this.ucOutPatientRecipeItemSelect1.Size = new System.Drawing.Size(709, 85);
            this.ucOutPatientRecipeItemSelect1.TabIndex = 0;
            // 
            // pnTop
            // 
            this.pnTop.Controls.Add(this.pnDisplay);
            this.pnTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTop.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pnTop.Location = new System.Drawing.Point(0, 0);
            this.pnTop.Name = "pnTop";
            this.pnTop.Size = new System.Drawing.Size(709, 28);
            this.pnTop.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnTop.TabIndex = 8;
            // 
            // pnOrderPactInfo
            // 
            this.pnOrderPactInfo.Controls.Add(this.rdPact4);
            this.pnOrderPactInfo.Controls.Add(this.rdPact3);
            this.pnOrderPactInfo.Controls.Add(this.rdPact2);
            this.pnOrderPactInfo.Controls.Add(this.rdPact1);
            this.pnOrderPactInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnOrderPactInfo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pnOrderPactInfo.Location = new System.Drawing.Point(0, 454);
            this.pnOrderPactInfo.Name = "pnOrderPactInfo";
            this.pnOrderPactInfo.Size = new System.Drawing.Size(709, 28);
            this.pnOrderPactInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnOrderPactInfo.TabIndex = 9;
            this.pnOrderPactInfo.Visible = false;
            // 
            // rdPact4
            // 
            this.rdPact4.AutoSize = true;
            this.rdPact4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdPact4.Location = new System.Drawing.Point(452, 6);
            this.rdPact4.Name = "rdPact4";
            this.rdPact4.Size = new System.Drawing.Size(55, 18);
            this.rdPact4.TabIndex = 3;
            this.rdPact4.TabStop = true;
            this.rdPact4.Text = "自费";
            this.rdPact4.UseVisualStyleBackColor = true;
            this.rdPact4.Visible = false;
            // 
            // rdPact3
            // 
            this.rdPact3.AutoSize = true;
            this.rdPact3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdPact3.Location = new System.Drawing.Point(310, 6);
            this.rdPact3.Name = "rdPact3";
            this.rdPact3.Size = new System.Drawing.Size(85, 18);
            this.rdPact3.TabIndex = 2;
            this.rdPact3.TabStop = true;
            this.rdPact3.Text = "顺德医保";
            this.rdPact3.UseVisualStyleBackColor = true;
            this.rdPact3.Visible = false;
            // 
            // rdPact2
            // 
            this.rdPact2.AutoSize = true;
            this.rdPact2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdPact2.Location = new System.Drawing.Point(192, 6);
            this.rdPact2.Name = "rdPact2";
            this.rdPact2.Size = new System.Drawing.Size(55, 18);
            this.rdPact2.TabIndex = 1;
            this.rdPact2.TabStop = true;
            this.rdPact2.Text = "自费";
            this.rdPact2.UseVisualStyleBackColor = true;
            this.rdPact2.Visible = false;
            // 
            // rdPact1
            // 
            this.rdPact1.AutoSize = true;
            this.rdPact1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdPact1.Location = new System.Drawing.Point(50, 6);
            this.rdPact1.Name = "rdPact1";
            this.rdPact1.Size = new System.Drawing.Size(85, 18);
            this.rdPact1.TabIndex = 0;
            this.rdPact1.TabStop = true;
            this.rdPact1.Text = "顺德医保";
            this.rdPact1.UseVisualStyleBackColor = true;
            this.rdPact1.Visible = false;
            // 
            // ucOutPatientRecipe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScroll = true;
            this.Controls.Add(this.neuSpread1);
            this.Controls.Add(this.ucOutPatientRecipeItemSelect1);
            this.Controls.Add(this.pnOrderPactInfo);
            this.Controls.Add(this.pnTop);
            this.Name = "ucOutPatientRecipe";
            this.Size = new System.Drawing.Size(709, 482);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.pnDisplay.ResumeLayout(false);
            this.pnDisplay.PerformLayout();
            this.pnTop.ResumeLayout(false);
            this.pnOrderPactInfo.ResumeLayout(false);
            this.pnOrderPactInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ucOutPatientRecipeItemSelect ucOutPatientRecipeItemSelect1;
        public FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnDisplay;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblDisplay;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblFeeDisplay;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnTop;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnOrderPactInfo;
        private System.Windows.Forms.RadioButton rdPact2;
        private System.Windows.Forms.RadioButton rdPact1;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblFeeInfo;
        private System.Windows.Forms.RadioButton rdPact4;
        private System.Windows.Forms.RadioButton rdPact3;


    }
}