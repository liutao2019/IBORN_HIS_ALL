namespace FS.HISFC.Components.RADT.Forms
{
    partial class frmMessageShow
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
            this.neuPanelMain = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuPanelCenter = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.neuPanel3 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuLabelTip = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabelPatient = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuButtonNo = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuButtonYes = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.neuPanelMain.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            this.neuPanelCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.neuPanel3.SuspendLayout();
            this.neuPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanelMain
            // 
            this.neuPanelMain.BackColor = System.Drawing.Color.White;
            this.neuPanelMain.Controls.Add(this.neuPanel2);
            this.neuPanelMain.Controls.Add(this.neuPanel1);
            this.neuPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanelMain.Location = new System.Drawing.Point(0, 0);
            this.neuPanelMain.Name = "neuPanelMain";
            this.neuPanelMain.Size = new System.Drawing.Size(401, 413);
            this.neuPanelMain.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanelMain.TabIndex = 0;
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.neuPanelCenter);
            this.neuPanel2.Controls.Add(this.neuPanel3);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel2.Location = new System.Drawing.Point(0, 0);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(401, 363);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 2;
            // 
            // neuPanelCenter
            // 
            this.neuPanelCenter.Controls.Add(this.neuSpread1);
            this.neuPanelCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanelCenter.Location = new System.Drawing.Point(0, 60);
            this.neuPanelCenter.Name = "neuPanelCenter";
            this.neuPanelCenter.Size = new System.Drawing.Size(401, 303);
            this.neuPanelCenter.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanelCenter.TabIndex = 2;
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
            this.neuSpread1.Location = new System.Drawing.Point(0, 0);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(401, 303);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.neuSpread1.TextTipAppearance = tipAppearance1;
            this.neuSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ColumnCount = 1;
            this.neuSpread1_Sheet1.ColumnHeader.RowCount = 0;
            this.neuSpread1_Sheet1.RowCount = 1;
            this.neuSpread1_Sheet1.RowHeader.ColumnCount = 0;
            this.neuSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 450F;
            this.neuSpread1_Sheet1.PrintInfo.Footer = "";
            this.neuSpread1_Sheet1.PrintInfo.Header = "";
            this.neuSpread1_Sheet1.PrintInfo.JobName = "";
            this.neuSpread1_Sheet1.PrintInfo.Printer = "";
            this.neuSpread1_Sheet1.PrintInfo.ShowBorder = false;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.Rows.Default.Height = 30F;
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.neuSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // neuPanel3
            // 
            this.neuPanel3.Controls.Add(this.neuLabelTip);
            this.neuPanel3.Controls.Add(this.neuLabelPatient);
            this.neuPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel3.Location = new System.Drawing.Point(0, 0);
            this.neuPanel3.Name = "neuPanel3";
            this.neuPanel3.Size = new System.Drawing.Size(401, 60);
            this.neuPanel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel3.TabIndex = 1;
            // 
            // neuLabelTip
            // 
            this.neuLabelTip.AutoSize = true;
            this.neuLabelTip.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabelTip.Location = new System.Drawing.Point(12, 34);
            this.neuLabelTip.Name = "neuLabelTip";
            this.neuLabelTip.Size = new System.Drawing.Size(323, 16);
            this.neuLabelTip.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabelTip.TabIndex = 0;
            this.neuLabelTip.Text = "存在以下问题未处理,是否继续办理出院？";
            // 
            // neuLabelPatient
            // 
            this.neuLabelPatient.AutoSize = true;
            this.neuLabelPatient.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuLabelPatient.Location = new System.Drawing.Point(12, 8);
            this.neuLabelPatient.Name = "neuLabelPatient";
            this.neuLabelPatient.Size = new System.Drawing.Size(76, 16);
            this.neuLabelPatient.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabelPatient.TabIndex = 0;
            this.neuLabelPatient.Text = "患者信息";
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.neuButtonNo);
            this.neuPanel1.Controls.Add(this.neuButtonYes);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuPanel1.Location = new System.Drawing.Point(0, 363);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(401, 50);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 1;
            // 
            // neuButtonNo
            // 
            this.neuButtonNo.Location = new System.Drawing.Point(247, 9);
            this.neuButtonNo.Name = "neuButtonNo";
            this.neuButtonNo.Size = new System.Drawing.Size(75, 30);
            this.neuButtonNo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuButtonNo.TabIndex = 0;
            this.neuButtonNo.Text = "否(&N)";
            this.neuButtonNo.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuButtonNo.UseVisualStyleBackColor = true;
            this.neuButtonNo.Click += new System.EventHandler(this.neuButtonNo_Click);
            // 
            // neuButtonYes
            // 
            this.neuButtonYes.Location = new System.Drawing.Point(91, 9);
            this.neuButtonYes.Name = "neuButtonYes";
            this.neuButtonYes.Size = new System.Drawing.Size(75, 30);
            this.neuButtonYes.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuButtonYes.TabIndex = 0;
            this.neuButtonYes.Text = "是(&Y)";
            this.neuButtonYes.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.neuButtonYes.UseVisualStyleBackColor = true;
            this.neuButtonYes.Click += new System.EventHandler(this.neuButtonYes_Click);
            // 
            // frmMessageShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(401, 413);
            this.ControlBox = false;
            this.Controls.Add(this.neuPanelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "frmMessageShow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "提示";
            this.TopMost = true;
            this.neuPanelMain.ResumeLayout(false);
            this.neuPanel2.ResumeLayout(false);
            this.neuPanelCenter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.neuPanel3.ResumeLayout(false);
            this.neuPanel3.PerformLayout();
            this.neuPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanelMain;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuButton neuButtonNo;
        private FS.FrameWork.WinForms.Controls.NeuButton neuButtonYes;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel3;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabelTip;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabelPatient;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanelCenter;
    }
}
