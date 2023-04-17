namespace Neusoft.SOC.Local.PubReport.Components
{
    partial class ucPubReportPanel
    {
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ImageList imageList16;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Splitter splitter1;
		private Crownwood.Magic.Controls.TabControl tabControl1;
		private Crownwood.Magic.Controls.TabPage tbDetial;
		private System.Windows.Forms.Panel plShow1;
		private Crownwood.Magic.Controls.TabPage tbSum;
		private System.Windows.Forms.Panel plShow2;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.TreeView tvList;
        private System.Windows.Forms.Label label2;
		private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private CrystalDecisions.CrystalReports.Engine.ReportDocument printDoc = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
		
		private System.Windows.Forms.CheckBox chkIsRetire;
        private ucSIGYRep ucSIGYRep1;
		private Crownwood.Magic.Controls.TabPage tbExportPage;
		private FarPoint.Win.Spread.FpSpread fpSpread1;
		private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            this.imageList16 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1 = new Crownwood.Magic.Controls.TabControl();
            this.tbExportPage = new Crownwood.Magic.Controls.TabPage();
            this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.tbDetial = new Crownwood.Magic.Controls.TabPage();
            this.plShow1 = new System.Windows.Forms.Panel();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.tbSum = new Crownwood.Magic.Controls.TabPage();
            this.plShow2 = new System.Windows.Forms.Panel();
            this.ucSIGYRep1 = new SOC.Local.PubReport.Components.ucSIGYRep();
            this.chkIsRetire = new System.Windows.Forms.CheckBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tvList = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtStaticMonth = new SOC.Local.PubReport.Components.DateTimeTextBox(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tbExportPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            this.tbDetial.SuspendLayout();
            this.plShow1.SuspendLayout();
            this.tbSum.SuspendLayout();
            this.plShow2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList16
            // 
            this.imageList16.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList16.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList16.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(880, 526);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Controls.Add(this.splitter1);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(880, 524);
            this.panel2.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.IDEPixelArea = true;
            this.tabControl1.Location = new System.Drawing.Point(195, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.PositionTop = true;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.SelectedTab = this.tbDetial;
            this.tabControl1.Size = new System.Drawing.Size(685, 524);
            this.tabControl1.TabIndex = 3;
            this.tabControl1.TabPages.AddRange(new Crownwood.Magic.Controls.TabPage[] {
            this.tbDetial,
            this.tbSum,
            this.tbExportPage});
            this.tabControl1.SelectionChanged += new System.EventHandler(this.tabControl1_SelectionChanged);
            // 
            // tbExportPage
            // 
            this.tbExportPage.Controls.Add(this.fpSpread1);
            this.tbExportPage.Location = new System.Drawing.Point(0, 25);
            this.tbExportPage.Name = "tbExportPage";
            this.tbExportPage.Selected = false;
            this.tbExportPage.Size = new System.Drawing.Size(685, 499);
            this.tbExportPage.TabIndex = 5;
            this.tbExportPage.Title = "住院医药费报销明细";
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "2.5.2007.2005";
            this.fpSpread1.AccessibleDescription = "";
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread1.Location = new System.Drawing.Point(0, 0);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1});
            this.fpSpread1.Size = new System.Drawing.Size(685, 499);
            this.fpSpread1.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread1.TextTipAppearance = tipAppearance1;
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "单位";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "姓名";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "医疗证号";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "入院日";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "出院日";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "住院天数";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "西药";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "成药";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "诊金";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "检查";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "治疗";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "高检";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "特药";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "手术";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 14).Value = "输血";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 15).Value = "化验";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 16).Value = "床位";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 17).Value = "护理";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 18).Value = "总金额";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 19).Value = "自付比例";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 20).Value = "自付金额";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 21).Value = "实际记帐";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 22).Value = "审核支付";
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // tbDetial
            // 
            this.tbDetial.Controls.Add(this.plShow1);
            this.tbDetial.Location = new System.Drawing.Point(0, 25);
            this.tbDetial.Name = "tbDetial";
            this.tbDetial.Size = new System.Drawing.Size(685, 499);
            this.tbDetial.TabIndex = 3;
            this.tbDetial.Title = "住院医药费报销明细";
            // 
            // plShow1
            // 
            this.plShow1.BackColor = System.Drawing.Color.Azure;
            this.plShow1.Controls.Add(this.crystalReportViewer1);
            this.plShow1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plShow1.Location = new System.Drawing.Point(0, 0);
            this.plShow1.Name = "plShow1";
            this.plShow1.Size = new System.Drawing.Size(685, 499);
            this.plShow1.TabIndex = 0;
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.DisplayBackgroundEdge = false;
            this.crystalReportViewer1.DisplayGroupTree = false;
            this.crystalReportViewer1.DisplayStatusBar = false;
            this.crystalReportViewer1.DisplayToolbar = false;
            this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer1.Location = new System.Drawing.Point(0, 0);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.SelectionFormula = "";
            this.crystalReportViewer1.ShowGroupTreeButton = false;
            this.crystalReportViewer1.Size = new System.Drawing.Size(685, 499);
            this.crystalReportViewer1.TabIndex = 0;
            this.crystalReportViewer1.ViewTimeSelectionFormula = "";
            // 
            // tbSum
            // 
            this.tbSum.Controls.Add(this.plShow2);
            this.tbSum.Location = new System.Drawing.Point(0, 25);
            this.tbSum.Name = "tbSum";
            this.tbSum.Selected = false;
            this.tbSum.Size = new System.Drawing.Size(685, 499);
            this.tbSum.TabIndex = 4;
            this.tbSum.Title = "门诊住院月结算申报表";
            this.tbSum.Visible = false;
            // 
            // plShow2
            // 
            this.plShow2.AutoScroll = true;
            this.plShow2.BackColor = System.Drawing.Color.White;
            this.plShow2.Controls.Add(this.ucSIGYRep1);
            this.plShow2.Controls.Add(this.chkIsRetire);
            this.plShow2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plShow2.Location = new System.Drawing.Point(0, 0);
            this.plShow2.Name = "plShow2";
            this.plShow2.Size = new System.Drawing.Size(685, 499);
            this.plShow2.TabIndex = 0;
            // 
            // ucSIGYRep1
            // 
            this.ucSIGYRep1.IsPrint = false;
            this.ucSIGYRep1.IsRetire = false;
            this.ucSIGYRep1.Location = new System.Drawing.Point(0, 0);
            this.ucSIGYRep1.Name = "ucSIGYRep1";
            this.ucSIGYRep1.Size = new System.Drawing.Size(1400, 520);
            this.ucSIGYRep1.TabIndex = 2;
            // 
            // chkIsRetire
            // 
            this.chkIsRetire.BackColor = System.Drawing.SystemColors.Highlight;
            this.chkIsRetire.Location = new System.Drawing.Point(24, 8);
            this.chkIsRetire.Name = "chkIsRetire";
            this.chkIsRetire.Size = new System.Drawing.Size(104, 24);
            this.chkIsRetire.TabIndex = 1;
            this.chkIsRetire.Text = "省直离休";
            this.chkIsRetire.UseVisualStyleBackColor = false;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(192, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 524);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tvList);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(192, 524);
            this.panel3.TabIndex = 1;
            // 
            // tvList
            // 
            this.tvList.CheckBoxes = true;
            this.tvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvList.ImageIndex = 0;
            this.tvList.ImageList = this.imageList1;
            this.tvList.Location = new System.Drawing.Point(0, 50);
            this.tvList.Name = "tvList";
            this.tvList.SelectedImageIndex = 0;
            this.tvList.Size = new System.Drawing.Size(192, 474);
            this.tvList.TabIndex = 3;
            this.tvList.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvList_AfterCheck);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txtStaticMonth);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(192, 50);
            this.panel4.TabIndex = 0;
            // 
            // txtStaticMonth
            // 
            this.txtStaticMonth.Err = "";
            this.txtStaticMonth.Location = new System.Drawing.Point(55, 12);
            this.txtStaticMonth.Name = "txtStaticMonth";
            this.txtStaticMonth.Size = new System.Drawing.Size(131, 21);
            this.txtStaticMonth.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtStaticMonth.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "统计月份";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(192, 1);
            this.label2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(880, 2);
            this.label1.TabIndex = 0;
            // 
            // ucPubReportPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.panel1);
            this.Name = "ucPubReportPanel";
            this.Size = new System.Drawing.Size(880, 526);
            this.Load += new System.EventHandler(this.ucReportPanel_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tbExportPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            this.tbDetial.ResumeLayout(false);
            this.plShow1.ResumeLayout(false);
            this.tbSum.ResumeLayout(false);
            this.plShow2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

        private DateTimeTextBox txtStaticMonth;
        private System.Windows.Forms.Label label5;
    }
}
