namespace FS.WinForms.Report.Finance.FinOpb
{
    partial class ucFinOpbStatFeeDoct
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.button1 = new System.Windows.Forms.Button();
            this.btnFeeSelect = new System.Windows.Forms.Button();
            this.lblMemo = new System.Windows.Forms.Label();
            this.plLeft.SuspendLayout();
            this.plRight.SuspendLayout();
            this.plQueryCondition.SuspendLayout();
            this.plMain.SuspendLayout();
            this.plTop.SuspendLayout();
            this.plBottom.SuspendLayout();
            this.plRightTop.SuspendLayout();
            this.plRightBottom.SuspendLayout();
            this.gbMid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // plLeft
            // 
            this.plLeft.Visible = false;
            // 
            // plTop
            // 
            this.plTop.Controls.Add(this.btnFeeSelect);
            this.plTop.Controls.Add(this.lblMemo);
            this.plTop.Controls.SetChildIndex(this.lblMemo, 0);
            this.plTop.Controls.SetChildIndex(this.btnFeeSelect, 0);
            this.plTop.Controls.SetChildIndex(this.dtpBeginTime, 0);
            this.plTop.Controls.SetChildIndex(this.neuLabel1, 0);
            this.plTop.Controls.SetChildIndex(this.neuLabel2, 0);
            this.plTop.Controls.SetChildIndex(this.dtpEndTime, 0);
            // 
            // slLeft
            // 
            this.slLeft.Visible = false;
            // 
            // plLeftControl
            // 
            this.plLeftControl.Visible = false;
            // 
            // plRightTop
            // 
            this.plRightTop.Controls.Add(this.button1);
            this.plRightTop.Controls.SetChildIndex(this.button1, 0);
            this.plRightTop.Controls.SetChildIndex(this.neuSpread1, 0);
            // 
            // plRightBottom
            // 
            this.plRightBottom.Visible = false;
            // 
            // neuSpread1
            // 
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, ";
            this.neuSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread2.ActiveSheetIndex = -1;
            // 
            // neuSpread1_Sheet1
            // 
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ColumnHeader.RowCount = 0;
            this.neuSpread1_Sheet1.RowHeader.ColumnCount = 0;
            this.neuSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.SystemColors.Control, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.White, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.neuSpread1_Sheet1.Cells.Get(0, 0).ColumnSpan = 10;
            this.neuSpread1_Sheet1.Cells.Get(0, 0).Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Cells.Get(0, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(1, 0).ColumnSpan = 10;
            this.neuSpread1_Sheet1.Cells.Get(1, 0).Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Cells.Get(1, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(1, 0).Value = "门诊医生收入统计汇总表（按发票）";
            this.neuSpread1_Sheet1.Cells.Get(3, 0).ColumnSpan = 8;
            this.neuSpread1_Sheet1.Cells.Get(3, 0).Note = "统计日期：{0}-{1}";
            this.neuSpread1_Sheet1.Cells.Get(3, 0).NoteIndicatorColor = System.Drawing.Color.Transparent;
            this.neuSpread1_Sheet1.Cells.Get(3, 0).Tag = "{QueryParams}";
            this.neuSpread1_Sheet1.Cells.Get(3, 0).Value = "统计日期：";
            this.neuSpread1_Sheet1.Cells.Get(4, 0).ColumnSpan = 6;
            this.neuSpread1_Sheet1.Cells.Get(4, 0).Value = "制表人：";
            this.neuSpread1_Sheet1.Cells.Get(4, 6).ColumnSpan = 6;
            this.neuSpread1_Sheet1.Cells.Get(4, 6).Value = "打印日期：";
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(286, 116);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnFeeSelect
            // 
            this.btnFeeSelect.Location = new System.Drawing.Point(463, 12);
            this.btnFeeSelect.Name = "btnFeeSelect";
            this.btnFeeSelect.Size = new System.Drawing.Size(92, 23);
            this.btnFeeSelect.TabIndex = 4;
            this.btnFeeSelect.Text = "统计大类选择";
            this.btnFeeSelect.UseVisualStyleBackColor = true;
            this.btnFeeSelect.Click += new System.EventHandler(this.btnFeeSelect_Click);
            // 
            // lblMemo
            // 
            this.lblMemo.AutoSize = true;
            this.lblMemo.ForeColor = System.Drawing.Color.Blue;
            this.lblMemo.Location = new System.Drawing.Point(571, 19);
            this.lblMemo.Name = "lblMemo";
            this.lblMemo.Size = new System.Drawing.Size(233, 12);
            this.lblMemo.TabIndex = 5;
            this.lblMemo.Text = "您当前选择的统计类型是：[门诊自费发票]";
            // 
            // ucFinOpbStatFeeDoct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucFinOpbStatFeeDoct";
            this.SvMain = this.neuSpread1_Sheet1;
            this.UseParamCellsCount = 1;
            this.Load += new System.EventHandler(this.ucFinOpbStatFeeDoct_Load);
            this.plLeft.ResumeLayout(false);
            this.plRight.ResumeLayout(false);
            this.plQueryCondition.ResumeLayout(false);
            this.plMain.ResumeLayout(false);
            this.plTop.ResumeLayout(false);
            this.plTop.PerformLayout();
            this.plBottom.ResumeLayout(false);
            this.plRightTop.ResumeLayout(false);
            this.plRightBottom.ResumeLayout(false);
            this.gbMid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnFeeSelect;
        private System.Windows.Forms.Label lblMemo;
    }
}
