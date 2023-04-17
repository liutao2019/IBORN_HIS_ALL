namespace FS.HISFC.Components.Order.Controls
{
    partial class ucPCCItemList
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
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            this.cbxIsReal = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.lbInput = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.fpSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.lbCount = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lklPageDown = new System.Windows.Forms.LinkLabel();
            this.btnPriorRow = new System.Windows.Forms.Button();
            this.lklPageUp = new System.Windows.Forms.LinkLabel();
            this.pnlBottom = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.btnNextRow = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbxIsReal
            // 
            this.cbxIsReal.AutoSize = true;
            this.cbxIsReal.ForeColor = System.Drawing.Color.Blue;
            this.cbxIsReal.Location = new System.Drawing.Point(293, 9);
            this.cbxIsReal.Name = "cbxIsReal";
            this.cbxIsReal.Size = new System.Drawing.Size(72, 16);
            this.cbxIsReal.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cbxIsReal.TabIndex = 2;
            this.cbxIsReal.Text = "模糊查找";
            this.cbxIsReal.UseVisualStyleBackColor = true;
            this.cbxIsReal.CheckedChanged += new System.EventHandler(this.cbxIsReal_CheckedChanged);
            // 
            // lbInput
            // 
            this.lbInput.AutoSize = true;
            this.lbInput.ForeColor = System.Drawing.Color.Blue;
            this.lbInput.Location = new System.Drawing.Point(26, 10);
            this.lbInput.Name = "lbInput";
            this.lbInput.Size = new System.Drawing.Size(41, 12);
            this.lbInput.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbInput.TabIndex = 1;
            this.lbInput.Text = "输入法";
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "3.0.2004.2005";
            this.fpSpread1.AccessibleDescription = "fpSpread1, Sheet1, Row 0, Column 0, ";
            this.fpSpread1.BackColor = System.Drawing.Color.White;
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread1.FileName = "";
            this.fpSpread1.HorizontalScrollBarHeight = 20;
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.IsAutoSaveGridStatus = false;
            this.fpSpread1.IsCanCustomConfigColumn = false;
            this.fpSpread1.Location = new System.Drawing.Point(0, 35);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1});
            this.fpSpread1.Size = new System.Drawing.Size(508, 225);
            this.fpSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpSpread1.TabIndex = 0;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread1.TextTipAppearance = tipAppearance2;
            this.fpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.VerticalScrollBarWidth = 20;
            this.fpSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpSpread1_CellDoubleClick);
            this.fpSpread1.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpSpread1_CellClick);
            this.fpSpread1.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(this.fpSpread1_SelectionChanged);
            this.fpSpread1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fpSpread1_KeyDown);
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.RowCount = 10;
            this.fpSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpSpread1_Sheet1.RowHeader.AutoText = FarPoint.Win.Spread.HeaderAutoText.Blank;
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 20F;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyleName = "F";
            this.fpSpread1_Sheet1.Rows.Get(1).Height = 22F;
            this.fpSpread1_Sheet1.Rows.Get(5).Height = 18F;
            this.fpSpread1_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.fpSpread1_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // lbCount
            // 
            this.lbCount.AutoSize = true;
            this.lbCount.Location = new System.Drawing.Point(427, 10);
            this.lbCount.Name = "lbCount";
            this.lbCount.Size = new System.Drawing.Size(0, 12);
            this.lbCount.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbCount.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnNextRow);
            this.panel1.Controls.Add(this.lklPageDown);
            this.panel1.Controls.Add(this.btnPriorRow);
            this.panel1.Controls.Add(this.lklPageUp);
            this.panel1.Controls.Add(this.cbxIsReal);
            this.panel1.Controls.Add(this.lbInput);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(508, 35);
            this.panel1.TabIndex = 4;
            // 
            // lklPageDown
            // 
            this.lklPageDown.AutoSize = true;
            this.lklPageDown.Location = new System.Drawing.Point(414, 16);
            this.lklPageDown.Name = "lklPageDown";
            this.lklPageDown.Size = new System.Drawing.Size(41, 12);
            this.lklPageDown.TabIndex = 9;
            this.lklPageDown.TabStop = true;
            this.lklPageDown.Text = "下一页";
            this.lklPageDown.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lklPageDown_LinkClicked);
            // 
            // btnPriorRow
            // 
            this.btnPriorRow.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPriorRow.Location = new System.Drawing.Point(483, 0);
            this.btnPriorRow.Name = "btnPriorRow";
            this.btnPriorRow.Size = new System.Drawing.Size(25, 35);
            this.btnPriorRow.TabIndex = 6;
            this.btnPriorRow.Text = "↑";
            this.btnPriorRow.UseVisualStyleBackColor = true;
            this.btnPriorRow.Visible = false;
            this.btnPriorRow.Click += new System.EventHandler(this.btnPriorRow_Click);
            // 
            // lklPageUp
            // 
            this.lklPageUp.AutoSize = true;
            this.lklPageUp.Location = new System.Drawing.Point(371, 16);
            this.lklPageUp.Name = "lklPageUp";
            this.lklPageUp.Size = new System.Drawing.Size(41, 12);
            this.lklPageUp.TabIndex = 8;
            this.lklPageUp.TabStop = true;
            this.lklPageUp.Text = "上一页";
            this.lklPageUp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lklPageUp_LinkClicked);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 260);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(508, 49);
            this.pnlBottom.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pnlBottom.TabIndex = 5;
            // 
            // btnNextRow
            // 
            this.btnNextRow.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNextRow.Location = new System.Drawing.Point(458, 0);
            this.btnNextRow.Name = "btnNextRow";
            this.btnNextRow.Size = new System.Drawing.Size(25, 35);
            this.btnNextRow.TabIndex = 7;
            this.btnNextRow.Text = "↓";
            this.btnNextRow.UseVisualStyleBackColor = true;
            this.btnNextRow.Visible = false;
            this.btnNextRow.Click += new System.EventHandler(this.btnNextRow_Click);
            // 
            // ucPCCItemList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fpSpread1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lbCount);
            this.Controls.Add(this.pnlBottom);
            this.Name = "ucPCCItemList";
            this.Size = new System.Drawing.Size(508, 309);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbInput;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox cbxIsReal;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbCount;
        private System.Windows.Forms.Panel panel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel pnlBottom;
        private System.Windows.Forms.Button btnPriorRow;
        private System.Windows.Forms.Button btnNextRow;
        private System.Windows.Forms.LinkLabel lklPageUp;
        private System.Windows.Forms.LinkLabel lklPageDown;
    }
}
