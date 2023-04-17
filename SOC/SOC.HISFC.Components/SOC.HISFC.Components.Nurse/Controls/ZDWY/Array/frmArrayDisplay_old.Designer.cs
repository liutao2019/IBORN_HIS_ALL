namespace FS.SOC.HISFC.Components.Nurse.Controls.ZDWY.Array
{
    partial class frmArrayDisplay_old
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.LineBorder lineBorder1 = new FarPoint.Win.LineBorder(System.Drawing.Color.White);
            this.timerShow = new System.Windows.Forms.Timer(this.components);
            this.timerCall = new System.Windows.Forms.Timer(this.components);
            this.pnTop = new System.Windows.Forms.Panel();
            this.lblCallInfo = new System.Windows.Forms.Label();
            this.pnButton = new System.Windows.Forms.Panel();
            this.pnMain = new System.Windows.Forms.Panel();
            this.fpSpread1 = new FS.SOC.Windows.Forms.FpSpread(this.components);
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.timerChange = new System.Windows.Forms.Timer(this.components);
            this.pnTop.SuspendLayout();
            this.pnMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // timerShow
            // 
            this.timerShow.Interval = 10000;
            // 
            // pnTop
            // 
            this.pnTop.BackColor = System.Drawing.Color.DarkTurquoise;
            this.pnTop.Controls.Add(this.lblCallInfo);
            this.pnTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTop.ForeColor = System.Drawing.Color.White;
            this.pnTop.Location = new System.Drawing.Point(0, 0);
            this.pnTop.Name = "pnTop";
            this.pnTop.Size = new System.Drawing.Size(911, 76);
            this.pnTop.TabIndex = 0;
            // 
            // lblCallInfo
            // 
            this.lblCallInfo.BackColor = System.Drawing.Color.DarkTurquoise;
            this.lblCallInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCallInfo.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCallInfo.Location = new System.Drawing.Point(0, 0);
            this.lblCallInfo.Name = "lblCallInfo";
            this.lblCallInfo.Size = new System.Drawing.Size(911, 76);
            this.lblCallInfo.TabIndex = 0;
            this.lblCallInfo.Text = "挂号后请您在座椅上静候显示或声音叫号";
            this.lblCallInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCallInfo.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lblCallInfo_MouseDoubleClick);
            // 
            // pnButton
            // 
            this.pnButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnButton.Location = new System.Drawing.Point(0, 413);
            this.pnButton.Name = "pnButton";
            this.pnButton.Size = new System.Drawing.Size(911, 52);
            this.pnButton.TabIndex = 1;
            this.pnButton.Visible = false;
            // 
            // pnMain
            // 
            this.pnMain.Controls.Add(this.fpSpread1);
            this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnMain.Location = new System.Drawing.Point(0, 76);
            this.pnMain.Name = "pnMain";
            this.pnMain.Size = new System.Drawing.Size(911, 337);
            this.pnMain.TabIndex = 2;
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "3.0.2004.2005";
            this.fpSpread1.AccessibleDescription = "fpSpread1, Sheet1, Row 0, Column 0, ";
            this.fpSpread1.BackColor = System.Drawing.SystemColors.Control;
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            this.fpSpread1.Location = new System.Drawing.Point(0, 0);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1});
            this.fpSpread1.Size = new System.Drawing.Size(911, 337);
            this.fpSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpSpread1.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread1.TextTipAppearance = tipAppearance1;
            this.fpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.ColumnCount = 5;
            this.fpSpread1_Sheet1.RowCount = 1;
            this.fpSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Gainsboro, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))), System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpSpread1_Sheet1.Cells.Get(0, 1).Border = lineBorder1;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).BackColor = System.Drawing.Color.DarkTurquoise;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).BackColor = System.Drawing.Color.DarkTurquoise;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).BackColor = System.Drawing.Color.DarkTurquoise;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).BackColor = System.Drawing.Color.DarkTurquoise;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).BackColor = System.Drawing.Color.DarkTurquoise;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.ColumnHeader.Rows.Get(0).Height = 35F;
            this.fpSpread1_Sheet1.Columns.Get(0).Width = 122F;
            this.fpSpread1_Sheet1.Columns.Get(1).Width = 185F;
            this.fpSpread1_Sheet1.Columns.Get(2).Width = 185F;
            this.fpSpread1_Sheet1.Columns.Get(3).Width = 185F;
            this.fpSpread1_Sheet1.Columns.Get(4).Width = 185F;
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Gainsboro;
            this.fpSpread1_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.fpSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // timerChange
            // 
            this.timerChange.Interval = 10000;
            // 
            // frmArrayDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(911, 465);
            this.Controls.Add(this.pnMain);
            this.Controls.Add(this.pnButton);
            this.Controls.Add(this.pnTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmArrayDisplay";
            this.Text = "frmArrayDisplay";
            this.pnTop.ResumeLayout(false);
            this.pnMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerShow;
        private System.Windows.Forms.Timer timerCall;
        private System.Windows.Forms.Panel pnTop;
        private System.Windows.Forms.Panel pnButton;
        private System.Windows.Forms.Panel pnMain;
        private System.Windows.Forms.Label lblCallInfo;
        private FS.SOC.Windows.Forms.FpSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
        private System.Windows.Forms.Timer timerChange;
    }
}