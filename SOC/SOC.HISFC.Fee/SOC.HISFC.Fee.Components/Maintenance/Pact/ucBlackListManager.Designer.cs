namespace FS.SOC.HISFC.Fee.Components.Maintenance.Pact
{
    partial class ucBlackListManager
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components;

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
            this.neuQueryGroupBox = new FS.FrameWork.WinForms.Controls.NeuGroupBox();
            this.neuPactCodeCbb = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuPactCodelbl = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuQueryTxtBox = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuQuerylbl = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuBlackListSpread = new FarPoint.Win.Spread.SheetView();
            this.neuSpread = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.neuQueryGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuBlackListSpread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread)).BeginInit();
            this.SuspendLayout();
            // 
            // neuQueryGroupBox
            // 
            this.neuQueryGroupBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.neuQueryGroupBox.Controls.Add(this.neuPactCodeCbb);
            this.neuQueryGroupBox.Controls.Add(this.neuPactCodelbl);
            this.neuQueryGroupBox.Controls.Add(this.neuQueryTxtBox);
            this.neuQueryGroupBox.Controls.Add(this.neuQuerylbl);
            this.neuQueryGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuQueryGroupBox.Location = new System.Drawing.Point(0, 0);
            this.neuQueryGroupBox.Name = "neuQueryGroupBox";
            this.neuQueryGroupBox.Size = new System.Drawing.Size(854, 49);
            this.neuQueryGroupBox.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuQueryGroupBox.TabIndex = 0;
            this.neuQueryGroupBox.TabStop = false;
            this.neuQueryGroupBox.Text = "查询设置";
            // 
            // neuPactCodeCbb
            // 
            this.neuPactCodeCbb.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.neuPactCodeCbb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.neuPactCodeCbb.FormattingEnabled = true;
            this.neuPactCodeCbb.IsEnter2Tab = false;
            this.neuPactCodeCbb.IsFlat = false;
            this.neuPactCodeCbb.IsLike = true;
            this.neuPactCodeCbb.IsListOnly = false;
            this.neuPactCodeCbb.IsPopForm = true;
            this.neuPactCodeCbb.IsShowCustomerList = false;
            this.neuPactCodeCbb.IsShowID = false;
            this.neuPactCodeCbb.IsShowIDAndName = false;
            this.neuPactCodeCbb.Location = new System.Drawing.Point(303, 24);
            this.neuPactCodeCbb.Name = "neuPactCodeCbb";
            this.neuPactCodeCbb.ShowCustomerList = false;
            this.neuPactCodeCbb.ShowID = false;
            this.neuPactCodeCbb.Size = new System.Drawing.Size(121, 20);
            this.neuPactCodeCbb.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.neuPactCodeCbb.TabIndex = 3;
            this.neuPactCodeCbb.Tag = "";
            this.neuPactCodeCbb.ToolBarUse = false;
            // 
            // neuPactCodelbl
            // 
            this.neuPactCodelbl.AutoSize = true;
            this.neuPactCodelbl.Location = new System.Drawing.Point(232, 27);
            this.neuPactCodelbl.Name = "neuPactCodelbl";
            this.neuPactCodelbl.Size = new System.Drawing.Size(65, 12);
            this.neuPactCodelbl.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPactCodelbl.TabIndex = 2;
            this.neuPactCodelbl.Text = "合同单位：";
            // 
            // neuQueryTxtBox
            // 
            this.neuQueryTxtBox.IsEnter2Tab = false;
            this.neuQueryTxtBox.Location = new System.Drawing.Point(106, 24);
            this.neuQueryTxtBox.Name = "neuQueryTxtBox";
            this.neuQueryTxtBox.Size = new System.Drawing.Size(100, 21);
            this.neuQueryTxtBox.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuQueryTxtBox.TabIndex = 1;
            // 
            // neuQuerylbl
            // 
            this.neuQuerylbl.AutoSize = true;
            this.neuQuerylbl.Location = new System.Drawing.Point(17, 27);
            this.neuQuerylbl.Name = "neuQuerylbl";
            this.neuQuerylbl.Size = new System.Drawing.Size(83, 12);
            this.neuQuerylbl.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuQuerylbl.TabIndex = 0;
            this.neuQuerylbl.Text = "姓名卡号过滤:";
            // 
            // neuBlackListSpread
            // 
            this.neuBlackListSpread.Reset();
            this.neuBlackListSpread.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuBlackListSpread.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuBlackListSpread.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.SystemColors.Control, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247))))), System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(217)))), ((int)(((byte)(217))))), System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.neuBlackListSpread.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.neuBlackListSpread.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuBlackListSpread.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.neuBlackListSpread.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.neuBlackListSpread.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuBlackListSpread.SheetCornerStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(247)))));
            this.neuBlackListSpread.SheetCornerStyle.Parent = "CornerDefault";
            this.neuBlackListSpread.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuBlackListSpread.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuSpread
            // 
            this.neuSpread.About = "3.0.2004.2005";
            this.neuSpread.AccessibleDescription = "neuSpread, Sheet1, Row 0, Column 0, ";
            this.neuSpread.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread.Location = new System.Drawing.Point(0, 49);
            this.neuSpread.Name = "neuSpread";
            this.neuSpread.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuBlackListSpread});
            this.neuSpread.Size = new System.Drawing.Size(854, 536);
            this.neuSpread.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread.TabIndex = 2;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread.TextTipAppearance = tipAppearance1;
            // 
            // ucBlackListManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuSpread);
            this.Controls.Add(this.neuQueryGroupBox);
            this.Name = "ucBlackListManager";
            this.Size = new System.Drawing.Size(854, 585);
            this.neuQueryGroupBox.ResumeLayout(false);
            this.neuQueryGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuBlackListSpread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuGroupBox neuQueryGroupBox;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuQuerylbl;
        private FS.FrameWork.WinForms.Controls.NeuTextBox neuQueryTxtBox;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuPactCodelbl;
        private FS.FrameWork.WinForms.Controls.NeuComboBox neuPactCodeCbb;
        private FarPoint.Win.Spread.SheetView neuBlackListSpread;
        private FS.SOC.Windows.Forms.FpSpread neuSpread;
    }
}
