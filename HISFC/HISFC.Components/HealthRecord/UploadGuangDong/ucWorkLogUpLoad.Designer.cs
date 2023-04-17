namespace FS.HISFC.Components.HealthRecord.UploadGuangDong
{
    partial class ucWorkLogUpLoad
    {
        /// <summary> 
        /// 必需的设计器变量。

        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。

        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose( bool disposing )
        {
            if (disposing && ( components != null ))
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。

        /// </summary>
        private void InitializeComponent()
        {
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.a = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanelTime = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuDateTimePickerFrom = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuDateTimePickerTo = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuPanel2 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.neuSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.label1 = new System.Windows.Forms.Label();
            this.neuPanel1.SuspendLayout();
            this.neuPanelTime.SuspendLayout();
            this.neuPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.BackColor = System.Drawing.SystemColors.Window;
            this.neuPanel1.Controls.Add(this.a);
            this.neuPanel1.Controls.Add(this.neuPanelTime);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(0, 0);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(1187, 81);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 2;
            // 
            // a
            // 
            this.a.AutoSize = true;
            this.a.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.a.Location = new System.Drawing.Point(370, 12);
            this.a.Name = "a";
            this.a.Size = new System.Drawing.Size(0, 19);
            this.a.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.a.TabIndex = 8;
            // 
            // neuPanelTime
            // 
            this.neuPanelTime.Controls.Add(this.label1);
            this.neuPanelTime.Controls.Add(this.neuLabel1);
            this.neuPanelTime.Controls.Add(this.neuDateTimePickerFrom);
            this.neuPanelTime.Controls.Add(this.neuDateTimePickerTo);
            this.neuPanelTime.Controls.Add(this.neuLabel2);
            this.neuPanelTime.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.neuPanelTime.Location = new System.Drawing.Point(0, 43);
            this.neuPanelTime.Name = "neuPanelTime";
            this.neuPanelTime.Size = new System.Drawing.Size(1187, 38);
            this.neuPanelTime.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanelTime.TabIndex = 6;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(15, 13);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(41, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 2;
            this.neuLabel1.Text = "时间：";
            // 
            // neuDateTimePickerFrom
            // 
            this.neuDateTimePickerFrom.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.neuDateTimePickerFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.neuDateTimePickerFrom.IsEnter2Tab = false;
            this.neuDateTimePickerFrom.Location = new System.Drawing.Point(62, 9);
            this.neuDateTimePickerFrom.Name = "neuDateTimePickerFrom";
            this.neuDateTimePickerFrom.Size = new System.Drawing.Size(153, 21);
            this.neuDateTimePickerFrom.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuDateTimePickerFrom.TabIndex = 0;
            // 
            // neuDateTimePickerTo
            // 
            this.neuDateTimePickerTo.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.neuDateTimePickerTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.neuDateTimePickerTo.IsEnter2Tab = false;
            this.neuDateTimePickerTo.Location = new System.Drawing.Point(244, 9);
            this.neuDateTimePickerTo.Name = "neuDateTimePickerTo";
            this.neuDateTimePickerTo.Size = new System.Drawing.Size(153, 21);
            this.neuDateTimePickerTo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuDateTimePickerTo.TabIndex = 1;
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(221, 13);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(17, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 3;
            this.neuLabel2.Text = "至";
            // 
            // neuPanel2
            // 
            this.neuPanel2.Controls.Add(this.neuSpread1);
            this.neuPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel2.Location = new System.Drawing.Point(0, 81);
            this.neuPanel2.Name = "neuPanel2";
            this.neuPanel2.Size = new System.Drawing.Size(1187, 475);
            this.neuPanel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel2.TabIndex = 3;
            // 
            // neuSpread1
            // 
            this.neuSpread1.About = "3.0.2004.2005";
            this.neuSpread1.AccessibleDescription = "neuSpread1, Sheet1, Row 0, Column 0, ";
            this.neuSpread1.AllowEditOverflow = true;
            this.neuSpread1.BackColor = System.Drawing.Color.White;
            this.neuSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuSpread1.EditModePermanent = true;
            this.neuSpread1.EditModeReplace = true;
            this.neuSpread1.FileName = "";
            this.neuSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.neuSpread1.IsAutoSaveGridStatus = false;
            this.neuSpread1.IsCanCustomConfigColumn = false;
            this.neuSpread1.Location = new System.Drawing.Point(0, 0);
            this.neuSpread1.Name = "neuSpread1";
            this.neuSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.neuSpread1_Sheet1});
            this.neuSpread1.Size = new System.Drawing.Size(1187, 475);
            this.neuSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuSpread1.TabIndex = 11;
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
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(413, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(635, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "温馨提示：若选择时间大于1天，直接查询并上传；若时间段是一天，则显示上传数据，并单击【上传】再把数据上传。";
            // 
            // ucWorkLogUpLoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.neuPanel2);
            this.Controls.Add(this.neuPanel1);
            this.Name = "ucWorkLogUpLoad";
            this.Size = new System.Drawing.Size(1187, 556);
            this.Load += new System.EventHandler(this.ucUpLoad_Load);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            this.neuPanelTime.ResumeLayout(false);
            this.neuPanelTime.PerformLayout();
            this.neuPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanelTime;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker neuDateTimePickerFrom;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker neuDateTimePickerTo;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel2;
        private FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1;
        private FarPoint.Win.Spread.SheetView neuSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuLabel a;
        private System.Windows.Forms.Label label1;

    }
}
