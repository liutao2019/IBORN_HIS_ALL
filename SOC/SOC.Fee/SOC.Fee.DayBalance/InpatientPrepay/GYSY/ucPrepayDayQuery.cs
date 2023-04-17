using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.CellType;
namespace SOC.Fee.DayBalance.InpatientPrepay.GYSY
{
	/// <summary>
	/// frmPrepayDayQuery 的摘要说明。
	/// </summary>
    public partial class ucPrepayDayQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Panel panel4;
		
		private System.Windows.Forms.Panel panel1;
        private SOC.Fee.DayBalance.InpatientPrepay.GYSY.ucPrepayDayByOP ucPrepayDayByOP1;
		//		private Fee.ucOldFeeReport ucOldFeeReport1;
		private System.Windows.Forms.DateTimePicker dateTimePicker1;
		private System.Windows.Forms.MonthCalendar monthCalendar1;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private FarPoint.Win.Spread.FpSpread fpSpread1;
		private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
		private System.Windows.Forms.CheckBox chkDept;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cboDept1;
        private Panel pnlYJMX;
        private Label label1;
        private Panel panel2;
        private Label lblBalanceDate;
        private Label lblOper;
		private System.ComponentModel.IContainer components;

		public ucPrepayDayQuery()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucPrepayDayQuery));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.ucPrepayDayByOP1 = new SOC.Fee.DayBalance.InpatientPrepay.GYSY.ucPrepayDayByOP();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.pnlYJMX = new System.Windows.Forms.Panel();
            this.lblBalanceDate = new System.Windows.Forms.Label();
            this.lblOper = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkDept = new System.Windows.Forms.CheckBox();
            this.cboDept1 = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            this.pnlYJMX.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.dateTimePicker2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btnOK);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(780, 48);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(88, 16);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(136, 21);
            this.dateTimePicker1.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(240, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 23);
            this.label6.TabIndex = 6;
            this.label6.Text = "至";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(264, 16);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(136, 21);
            this.dateTimePicker2.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(16, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 24);
            this.label5.TabIndex = 4;
            this.label5.Text = "时 间:";
            // 
            // btnOK
            // 
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOK.Location = new System.Drawing.Point(432, 16);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "历史日结";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(790, 651);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage1.Controls.Add(this.panel4);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(782, 626);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "按操作员";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.ucPrepayDayByOP1);
            this.panel4.Controls.Add(this.monthCalendar1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 48);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(780, 576);
            this.panel4.TabIndex = 1;
            // 
            // ucPrepayDayByOP1
            // 
            this.ucPrepayDayByOP1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ucPrepayDayByOP1.BeginDate = "";
            this.ucPrepayDayByOP1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPrepayDayByOP1.EndDate = "";
            this.ucPrepayDayByOP1.Location = new System.Drawing.Point(0, 0);
            this.ucPrepayDayByOP1.Name = "ucPrepayDayByOP1";
            this.ucPrepayDayByOP1.OperName = "";
            this.ucPrepayDayByOP1.Size = new System.Drawing.Size(780, 576);
            this.ucPrepayDayByOP1.TabIndex = 0;
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(88, 16);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 8;
            // 
            // tabPage3
            // 
            this.tabPage3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage3.Controls.Add(this.groupBox3);
            this.tabPage3.Controls.Add(this.groupBox2);
            this.tabPage3.Location = new System.Drawing.Point(4, 21);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(782, 626);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "操作员预交金明细";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.groupBox3.Controls.Add(this.panel2);
            this.groupBox3.Controls.Add(this.pnlYJMX);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 8);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(780, 616);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel2.Controls.Add(this.fpSpread1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 89);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(774, 524);
            this.panel2.TabIndex = 8;
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "3.0.2004.2005";
            this.fpSpread1.AccessibleDescription = "fpSpread1, Sheet1, Row 0, Column 0, ";
            this.fpSpread1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread1.Location = new System.Drawing.Point(0, 0);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1});
            this.fpSpread1.Size = new System.Drawing.Size(774, 524);
            this.fpSpread1.TabIndex = 6;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread1.TextTipAppearance = tipAppearance1;
            this.fpSpread1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(this.fpSpread1_ColumnWidthChanged);
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.ColumnCount = 11;
            this.fpSpread1_Sheet1.RowCount = 5;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "票据号";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "序号";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "住院号";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "姓名";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "预交金额";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "交付方式";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "科 室";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "是否结算";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "状态";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "收费员";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "操作时间";
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.Columns.Get(0).Label = "票据号";
            this.fpSpread1_Sheet1.Columns.Get(0).Width = 75F;
            this.fpSpread1_Sheet1.Columns.Get(1).Label = "序号";
            this.fpSpread1_Sheet1.Columns.Get(1).Width = 40F;
            this.fpSpread1_Sheet1.Columns.Get(2).Label = "住院号";
            this.fpSpread1_Sheet1.Columns.Get(2).Width = 81F;
            this.fpSpread1_Sheet1.Columns.Get(3).ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet1.Columns.Get(3).Label = "姓名";
            this.fpSpread1_Sheet1.Columns.Get(3).Width = 81F;
            this.fpSpread1_Sheet1.Columns.Get(4).AllowAutoSort = true;
            this.fpSpread1_Sheet1.Columns.Get(4).Label = "预交金额";
            this.fpSpread1_Sheet1.Columns.Get(4).Width = 75F;
            this.fpSpread1_Sheet1.Columns.Get(5).Label = "交付方式";
            this.fpSpread1_Sheet1.Columns.Get(5).Width = 71F;
            this.fpSpread1_Sheet1.Columns.Get(6).Label = "科 室";
            this.fpSpread1_Sheet1.Columns.Get(6).Width = 101F;
            this.fpSpread1_Sheet1.Columns.Get(8).Label = "状态";
            this.fpSpread1_Sheet1.Columns.Get(8).Width = 61F;
            this.fpSpread1_Sheet1.Columns.Get(10).Label = "操作时间";
            this.fpSpread1_Sheet1.Columns.Get(10).Width = 154F;
            this.fpSpread1_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.RowHeader.Visible = false;
            this.fpSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.SheetCornerStyle.ForeColor = System.Drawing.Color.Black;
            this.fpSpread1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // pnlYJMX
            // 
            this.pnlYJMX.BackColor = System.Drawing.Color.White;
            this.pnlYJMX.Controls.Add(this.lblBalanceDate);
            this.pnlYJMX.Controls.Add(this.lblOper);
            this.pnlYJMX.Controls.Add(this.label1);
            this.pnlYJMX.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlYJMX.Location = new System.Drawing.Point(3, 17);
            this.pnlYJMX.Name = "pnlYJMX";
            this.pnlYJMX.Size = new System.Drawing.Size(774, 72);
            this.pnlYJMX.TabIndex = 7;
            // 
            // lblBalanceDate
            // 
            this.lblBalanceDate.AutoSize = true;
            this.lblBalanceDate.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBalanceDate.Location = new System.Drawing.Point(264, 45);
            this.lblBalanceDate.Name = "lblBalanceDate";
            this.lblBalanceDate.Size = new System.Drawing.Size(88, 16);
            this.lblBalanceDate.TabIndex = 2;
            this.lblBalanceDate.Text = "统计时间：";
            // 
            // lblOper
            // 
            this.lblOper.AutoSize = true;
            this.lblOper.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOper.Location = new System.Drawing.Point(16, 45);
            this.lblOper.Name = "lblOper";
            this.lblOper.Size = new System.Drawing.Size(72, 16);
            this.lblOper.TabIndex = 1;
            this.lblOper.Text = "收费员：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(308, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "预交金明细表";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkDept);
            this.groupBox2.Controls.Add(this.cboDept1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(780, 8);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // chkDept
            // 
            this.chkDept.Location = new System.Drawing.Point(16, 16);
            this.chkDept.Name = "chkDept";
            this.chkDept.Size = new System.Drawing.Size(104, 24);
            this.chkDept.TabIndex = 9;
            this.chkDept.Text = "按科室汇总";
            this.chkDept.Visible = false;
            // 
            // cboDept1
            // 
            this.cboDept1.ArrowBackColor = System.Drawing.Color.Silver;
            this.cboDept1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cboDept1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboDept1.IsEnter2Tab = false;
            this.cboDept1.IsFlat = false;
            this.cboDept1.IsLike = true;
            this.cboDept1.IsListOnly = false;
            this.cboDept1.IsPopForm = true;
            this.cboDept1.IsShowCustomerList = true;
            this.cboDept1.IsShowID = false;
            this.cboDept1.Location = new System.Drawing.Point(128, 16);
            this.cboDept1.Name = "cboDept1";
            this.cboDept1.ShowCustomerList = true;
            this.cboDept1.ShowID = false;
            this.cboDept1.Size = new System.Drawing.Size(109, 22);
            this.cboDept1.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cboDept1.TabIndex = 8;
            this.cboDept1.Tag = "";
            this.cboDept1.ToolBarUse = false;
            this.cboDept1.Visible = false;
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "");
            this.imageList2.Images.SetKeyName(1, "");
            this.imageList2.Images.SetKeyName(2, "");
            this.imageList2.Images.SetKeyName(3, "");
            this.imageList2.Images.SetKeyName(4, "");
            this.imageList2.Images.SetKeyName(5, "");
            this.imageList2.Images.SetKeyName(6, "");
            this.imageList2.Images.SetKeyName(7, "");
            this.imageList2.Images.SetKeyName(8, "");
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "");
            this.imageList1.Images.SetKeyName(5, "");
            this.imageList1.Images.SetKeyName(6, "");
            this.imageList1.Images.SetKeyName(7, "");
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(792, 653);
            this.panel1.TabIndex = 4;
            // 
            // ucPrepayDayQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.panel1);
            this.Name = "ucPrepayDayQuery";
            this.Size = new System.Drawing.Size(792, 653);
            this.groupBox1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            this.pnlYJMX.ResumeLayout(false);
            this.pnlYJMX.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		#region 定义

        /// <summary>
        /// 控制参数
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        SOC.Fee.DayBalance.Manager.PrepayDayBalance oCReport = new  SOC.Fee.DayBalance.Manager.PrepayDayBalance();
		//FS.HISFC.Management.Fee.FeeReport oCReport = new FS.HISFC.Management.Fee.FeeReport();
		FS.HISFC.BizLogic.Manager.Department oCDept = new  FS.HISFC.BizLogic.Manager.Department();
        Object.PrepayDayBalance oEPrepayStat;


        Object.PrepayDayBalance oldPrepay = null;


        FS.HISFC.BizLogic.Manager.PageSize psManager = new FS.HISFC.BizLogic.Manager.PageSize();

        FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
 
		#endregion

		#region 事件
        private string settingXmlFile = FS.FrameWork.WinForms.Classes.Function.CurrentPath +
            FS.FrameWork.WinForms.Classes.Function.SettingPath + "DayBalancePrepayDayColumn.xml";
        private void fpSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1.Sheets[0], settingXmlFile);
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("日结查询", "历史日结查询", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B保存, true, false, null);
            toolBarService.AddToolButton("日结", "日结", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B保存, true, false, null);
            toolBarService.AddToolButton("日结取消", "日结取消", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);

            return this.toolBarService;
        }
		private void btnOK_Click(object sender, System.EventArgs e)
		{
			try
			{
				string strBegin = dateTimePicker1.Value.ToShortDateString()+"  00:00:00  ";
				string strEnd = dateTimePicker2.Value.ToShortDateString()+"  23:59:59  ";

                string strNo = Function.PopPrepay(strBegin, strEnd, 0);
                if (strNo != "")
                {
                    SOC.Fee.DayBalance.Object.PrepayDayBalance obj = this.ucPrepayDayByOP1.GetListByNo(strNo);
                    if (obj != null)
                    {
                        this.dateTimePicker1.Value = FS.FrameWork.Function.NConvert.ToDateTime(obj.BeginDate);
                        this.dateTimePicker1.Enabled = false;
                        this.dateTimePicker2.Value = FS.FrameWork.Function.NConvert.ToDateTime(obj.EndDate);
                        this.dateTimePicker2.Enabled = true;

                        obj.ID = strNo;
                        oldPrepay = obj;
                        this.GetObj();
                        this.BindFp3(strBegin, strEnd);
                    }
                    this.dateTimePicker2.Enabled = false;
                }
			}
			catch{}
		}
		private void BindFp3(string strBegin,string strEnd)
		{
            string strOperID = this.oCReport.Operator.ID; 

            DataSet dts = new DataSet();

            dts = oCReport.GetPrepayList(strBegin, strEnd, strOperID);


            if (dts.Tables[0].Rows.Count > 0)
            {
                DataRow AcountRow = dts.Tables[0].NewRow();

                //				AcountRow[0] = "合计";
                AcountRow[2] = "合计";//总费用
                AcountRow[3] = dts.Tables[0].Compute("sum(prepay_cost)", "");//预交款
                //AcountRow[3] = dts.Tables[0].Compute("sum(预交金额)","");

                if (AcountRow[4].ToString() != "" && AcountRow[2].ToString() != "")
                {
                    AcountRow[7] = Math.Abs(Convert.ToDecimal(AcountRow[4]) / Convert.ToDecimal(AcountRow[2]) * 100);//欠费率
                }

                dts.Tables[0].Rows.Add(AcountRow);
            }
            this.lblOper.Text = "操作员：" + this.oCReport.Operator.Name;
            this.lblBalanceDate.Text = "统计时间：" + strBegin + "至 " + strEnd;
            this.fpSpread1.DataSource = dts.Tables[0].DefaultView;
            this.fpSpread1_Sheet1.Rows.Get(this.fpSpread1_Sheet1.Rows.Count - 1).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
            this.InitFp();

		}

		private void InitFp()
		{


            this.fpSpread1_Sheet1.Columns.Get(0).Width = 81F;
            this.fpSpread1_Sheet1.Columns.Get(1).Width = 40F;
            this.fpSpread1_Sheet1.Columns.Get(2).Width = 81F;
            this.fpSpread1_Sheet1.Columns.Get(3).Width = 81F;
            this.fpSpread1_Sheet1.Columns.Get(4).Width = 75F;
            this.fpSpread1_Sheet1.Columns.Get(5).Width = 71F;
            this.fpSpread1_Sheet1.Columns.Get(6).Width = 101F;
            this.fpSpread1_Sheet1.Columns.Get(7).Width = 61F;
            this.fpSpread1_Sheet1.Columns.Get(8).Width = 61F;
            this.fpSpread1_Sheet1.Columns.Get(9).Width = 61F;
            this.fpSpread1_Sheet1.Columns.Get(10).Width = 154F;

            this.SetColumnProperty();
		}

        protected override int OnQuery(object sender, object neuObject)
        {
            if (this.btnOK.Visible==true)
            {
                this.InitControl();
            }
            oldPrepay = null;
            //显示日结
            this.GetObj();

            //查询预交金发票
            this.BindFp3(this.dateTimePicker1.Value.ToString(), this.dateTimePicker2.Value.ToString());
            return base.OnQuery(sender, neuObject);
        }




        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "日结查询":
                    {
                        if (this.tabControl1.SelectedIndex == 0)
                        {
                            this.dateTimePicker1.Enabled = true;
                            this.dateTimePicker2.Enabled = true;
                            this.btnOK.Visible = true;
                            this.btnOK.Enabled = true;
                           
                        }
                        break;
                    }
                case "日结":
                    {
                        if (this.oEPrepayStat==null)
                        {
                            MessageBox.Show("请查询要日结的数据！");
                            return;
                        }
                        DialogResult r = MessageBox.Show("是否确定日结", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (r == DialogResult.Cancel)
                            return;
                        try
                        {
                            if (this.ucPrepayDayByOP1.Add(this.GetObj()) != -1)
                            {
                                this.Print();

                            }
                        }
                        catch (Exception ee)
                        {
                            string Error = ee.Message;
                        }
				
                        break;
                    }
                case "日结取消":
                    {
                          DialogResult r = MessageBox.Show("是否确定日结", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (r == DialogResult.Cancel)
                            return;
                        try
                        {
                            if (this.DeletePrepayBalance() != -1)
                            {
                                OnQuery(null, null);

                            }
                        }
                        catch (Exception ee)
                        {
                            string Error = ee.Message;
                        }
				
                      
                        break;
                    }
              


            }
            base.ToolStrip_ItemClicked(sender, e);
        }


        protected override void OnLoad(EventArgs e)
        {
            this.InitControl();
            base.OnLoad(e);
        }

        public override int Export(object sender, object neuObject)
        {
            saveExcel();
            return base.Export(sender, neuObject);
        }
        public override int Print(object sender, object neuObject)
        {
            Print();
            return base.Print(sender, neuObject);
        }

		#endregion

		#region 方法

        /// <summary>
        /// 读取配置文件
        /// </summary>
        private void SetColumnProperty()
        {
            if (System.IO.File.Exists(this.settingXmlFile))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpSpread1.Sheets[0], this.settingXmlFile);
            }
        }

        private void Print()
        {

            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            if (this.tabControl1.SelectedIndex == 0)
            {
                p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                FS.HISFC.Models.Base.PageSize ps = psManager.GetPageSize("YJRJ");
                if (ps == null)
                {
                    ps = new FS.HISFC.Models.Base.PageSize("YJRJ", 1040, 850);
                    ps.Top = 0;
                    ps.Left = 0;
                }
                p.SetPageSize(ps);

                if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
                {
                    p.PrintPreview(ps.Top, ps.Left, this.ucPrepayDayByOP1);
                }
                else
                {
                    p.PrintPage(ps.Top, ps.Left, this.ucPrepayDayByOP1);
                }
            }
            else 
            {
                //p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                FS.HISFC.Models.Base.PageSize ps = psManager.GetPageSize("YJMX");
                if (ps == null)
                {
                    ps = new FS.HISFC.Models.Base.PageSize("YJMX", 1040, 850);
                    ps.Top = 0;
                    ps.Left = 0;
                }
                p.SetPageSize(ps);

                if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
                {
                    p.PrintPreview(ps.Top, ps.Left, this.groupBox3);
                }
                else
                {
                    p.PrintPage(ps.Top, ps.Left, this.groupBox3);
                }
            }
        }

		private void InitControl()
		{
			DateTime dt = new DateTime();
			dt = Convert.ToDateTime(this.oCReport.GetSysDate());
			this.cboDept1.AddItems(oCDept.GetDeptmentAll());
			this.SetVisible(true);
            //this.dateTimePicker2.Value = dt.AddDays(+1);//DateTime.Now.AddDays(+1);
            //this.dateTimePicker1.Value = dt.AddDays(-1);

            string strMaxTime = "";
            if (this.oCReport.GetMaxTime() != "")
            {
                strMaxTime = this.oCReport.GetMaxTime();
            }
            else
            {
                strMaxTime = DateTime.MinValue.ToString();
            }
            try
            {
                this.dateTimePicker1.Value = FS.FrameWork.Function.NConvert.ToDateTime(strMaxTime);
            }
            catch
            {
                this.dateTimePicker1.Value = dt;
            }
            this.dateTimePicker1.Enabled = false;
            //			
            string strEndDate = this.oCReport.GetSysDateTime();

            this.dateTimePicker2.Value = FS.FrameWork.Function.NConvert.ToDateTime(strEndDate);
            this.dateTimePicker2.Enabled = true;
            this.btnOK.Visible = false;

            this.SetColumnProperty();
		}

		private void SetVisible(bool bTag)
		{
			groupBox1.Visible = bTag;

		}

        private SOC.Fee.DayBalance.Object.PrepayDayBalance GetObj()
		{
            string strMaxTime = this.dateTimePicker1.Value.ToString();
            string strEndDate = this.dateTimePicker2.Value.ToString();

            string strOper = this.oCReport.Operator.ID;
            this.ucPrepayDayByOP1.GetCost(strMaxTime, strEndDate, oCReport.Operator.ID);
            this.oEPrepayStat = new  SOC.Fee.DayBalance.Object.PrepayDayBalance();
            string strTrans = "";
            if (this.oCReport.GetTransCost(strMaxTime, strEndDate, strOper) != "")
            {
                strTrans = this.oCReport.GetTransCost(strMaxTime, strEndDate, strOper);
            }
            else
            {
                strTrans = "0";
            }
            this.oEPrepayStat.FGCost = Convert.ToDecimal(strTrans);//转押金
            this.oEPrepayStat.User01 = this.oCReport.GetMinReceiptNo(strMaxTime, strEndDate, strOper) + "-" + this.oCReport.GetMaxReceiptNo(strMaxTime, strEndDate, strOper);//票据区间
            string strNum = "";
            if (this.oCReport.GetReceiptNum(strMaxTime, strEndDate, strOper) != "")
            {
                strNum = this.oCReport.GetReceiptNum(strMaxTime, strEndDate, strOper);
            }
            else
            {
                strNum = "0";
            }
            this.oEPrepayStat.PrepayNum = Convert.ToInt32(strNum);//预交张数
            this.oEPrepayStat.User02 = this.oCReport.GetOutReceipt(strMaxTime, strEndDate, strOper);//预交作废票子号


            this.oEPrepayStat.BeginDate = strMaxTime;
            this.ucPrepayDayByOP1.SetBillSpan(this.oEPrepayStat.User01);
            this.ucPrepayDayByOP1.lbOpsRoom.Text = "操作员:";
            this.ucPrepayDayByOP1.labOpsDate.Text = (oldPrepay == null ? this.oCReport.GetSysDateTime() : oldPrepay.BalancOper.OperTime.ToString());
            this.ucPrepayDayByOP1.labPrintDate.Text = this.oCReport.GetSysDateTime();
            this.ucPrepayDayByOP1.lbOpsRoom.Text += this.oCReport.Operator.ID;
            this.oEPrepayStat.EndDate = this.oCReport.GetSysDateTime(); ;
            this.ucPrepayDayByOP1.OperName = this.oCReport.Operator.Name;

            this.oEPrepayStat.BalancOper.ID = this.oCReport.Operator.ID;
            return this.oEPrepayStat;
		}


        private void saveExcel()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Excel files (*.xls)|*.xls|Dbf files (*.dbf)|*.dbf|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.fpSpread1.SaveExcel(saveFileDialog1.FileName, FarPoint.Win.Spread.Model.IncludeHeaders.BothCustomOnly);
            }

        }

        private int DeletePrepayBalance()
        {
            if (this.btnOK.Visible==true)
            {
               return  this.oCReport.DeletePrepayStat(this.oldPrepay.ID); 
            }
            return 1;
        }
 
		#endregion
	
	}
}
