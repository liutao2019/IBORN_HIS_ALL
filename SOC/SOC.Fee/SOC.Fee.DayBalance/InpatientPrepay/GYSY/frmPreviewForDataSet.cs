using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SOC.Fee.DayBalance.InpatientPrepay.GYSY
	{
		/// <summary>
		/// 通用打印 用户farPoint 可传入 ArrayList 和 DataSource
		/// </summary>
    public class frmPreviewForDataSet : FS.FrameWork.WinForms.Controls.ucBaseControl
		{
			private System.ComponentModel.IContainer components;
			/// <summary>
			/// 构造函数
			/// </summary>
			public frmPreviewForDataSet()
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
				System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmPreviewForDataSet));
				this.toolBar1 = new System.Windows.Forms.ToolBar();
				this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
				this.tbExp = new System.Windows.Forms.ToolBarButton();
				this.toolBarButton3 = new System.Windows.Forms.ToolBarButton();
				this.tbPrintSetup = new System.Windows.Forms.ToolBarButton();
				this.tbPrint = new System.Windows.Forms.ToolBarButton();
				this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
				this.tbExit = new System.Windows.Forms.ToolBarButton();
				this.imageList1 = new System.Windows.Forms.ImageList(this.components);
				this.panel1 = new System.Windows.Forms.Panel();
				this.panel2 = new System.Windows.Forms.Panel();
				this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
				this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
				this.panel3 = new System.Windows.Forms.Panel();
				this.lblTitle = new System.Windows.Forms.Label();
				this.lblDateSpan = new System.Windows.Forms.Label();
				this.lblExecDept = new System.Windows.Forms.Label();
				this.label1 = new System.Windows.Forms.Label();
				this.lblPrintDate = new System.Windows.Forms.Label();
				this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
				this.label2 = new System.Windows.Forms.Label();
				this.panel4 = new System.Windows.Forms.Panel();
				this.panel1.SuspendLayout();
				this.panel2.SuspendLayout();
				((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
				((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
				this.panel3.SuspendLayout();
				this.panel4.SuspendLayout();
				this.SuspendLayout();
				// 
				// 
				// toolBar1
				// 
				this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
				this.toolBar1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
				this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																							this.toolBarButton2,
																							this.tbExp,
																							this.toolBarButton3,
																							this.tbPrintSetup,
																							this.tbPrint,
																							this.toolBarButton1,
																							this.tbExit});
				this.toolBar1.ButtonSize = new System.Drawing.Size(80, 60);
				this.toolBar1.DropDownArrows = true;
				this.toolBar1.ImageList = this.imageList1;
				this.toolBar1.Location = new System.Drawing.Point(0, 0);
				this.toolBar1.Name = "toolBar1";
				this.toolBar1.ShowToolTips = true;
				this.toolBar1.Size = new System.Drawing.Size(888, 58);
				this.toolBar1.TabIndex = 1;
				this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
				// 
				// toolBarButton2
				// 
				this.toolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
				// 
				// tbExp
				// 
				this.tbExp.ImageIndex = 3;
				this.tbExp.Text = "导出";
				this.tbExp.ToolTipText = "导出到本地的Excel文件";
				// 
				// toolBarButton3
				// 
				this.toolBarButton3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
				// 
				// tbPrintSetup
				// 
				this.tbPrintSetup.ImageIndex = 4;
				this.tbPrintSetup.Text = "打印设置";
				this.tbPrintSetup.ToolTipText = "打印设置";
				// 
				// tbPrint
				// 
				this.tbPrint.ImageIndex = 0;
				this.tbPrint.Text = "打印";
				// 
				// toolBarButton1
				// 
				this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
				// 
				// tbExit
				// 
				this.tbExit.ImageIndex = 1;
				this.tbExit.Text = "退   出";
				// 
				// imageList1
				// 
				this.imageList1.ImageSize = new System.Drawing.Size(32, 32);
				this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
				this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
				// 
				// panel1
				// 
				this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
					| System.Windows.Forms.AnchorStyles.Left) 
					| System.Windows.Forms.AnchorStyles.Right)));
				this.panel1.BackColor = System.Drawing.Color.White;
				this.panel1.Controls.Add(this.panel2);
				this.panel1.Controls.Add(this.panel3);
				this.panel1.Controls.Add(this.dateTimePicker1);
				this.panel1.Location = new System.Drawing.Point(66, 56);
				this.panel1.Name = "panel1";
				this.panel1.Size = new System.Drawing.Size(756, 444);
				this.panel1.TabIndex = 2;
				// 
				// panel2
				// 
				this.panel2.Controls.Add(this.fpSpread1);
				this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
				this.panel2.Location = new System.Drawing.Point(0, 64);
				this.panel2.Name = "panel2";
				this.panel2.Size = new System.Drawing.Size(756, 380);
				this.panel2.TabIndex = 9;
				// 
				// fpSpread1
				// 
				this.fpSpread1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
					| System.Windows.Forms.AnchorStyles.Left) 
					| System.Windows.Forms.AnchorStyles.Right)));
				this.fpSpread1.BackColor = System.Drawing.Color.White;
				this.fpSpread1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
				this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
				this.fpSpread1.Location = new System.Drawing.Point(48, 0);
				this.fpSpread1.Name = "fpSpread1";
				this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
																					   this.fpSpread1_Sheet1});
				this.fpSpread1.Size = new System.Drawing.Size(664, 368);
				this.fpSpread1.TabIndex = 4;
				// 
				// fpSpread1_Sheet1
				// 
				this.fpSpread1_Sheet1.Reset();
				this.fpSpread1_Sheet1.ColumnCount = 200;
				this.fpSpread1_Sheet1.RowCount = 0;
				this.fpSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Black, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, true, true, false, true, true);
				this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
				this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
				this.fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
				this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
				this.fpSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
				this.fpSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
				this.fpSpread1_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
				this.fpSpread1_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
				this.fpSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
				this.fpSpread1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
				this.fpSpread1_Sheet1.SheetName = "Sheet1";
				// 
				// panel3
				// 
				this.panel3.Controls.Add(this.lblTitle);
				this.panel3.Controls.Add(this.lblDateSpan);
				this.panel3.Controls.Add(this.lblExecDept);
				this.panel3.Controls.Add(this.label1);
				this.panel3.Controls.Add(this.lblPrintDate);
				this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
				this.panel3.Location = new System.Drawing.Point(0, 0);
				this.panel3.Name = "panel3";
				this.panel3.Size = new System.Drawing.Size(756, 64);
				this.panel3.TabIndex = 8;
				// 
				// lblTitle
				// 
				this.lblTitle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
				this.lblTitle.Location = new System.Drawing.Point(216, 8);
				this.lblTitle.Name = "lblTitle";
				this.lblTitle.Size = new System.Drawing.Size(336, 32);
				this.lblTitle.TabIndex = 0;
				this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
				// 
				// lblDateSpan
				// 
				this.lblDateSpan.Location = new System.Drawing.Point(56, 42);
				this.lblDateSpan.Name = "lblDateSpan";
				this.lblDateSpan.Size = new System.Drawing.Size(304, 16);
				this.lblDateSpan.TabIndex = 1;
				// 
				// lblExecDept
				// 
				this.lblExecDept.Location = new System.Drawing.Point(368, 42);
				this.lblExecDept.Name = "lblExecDept";
				this.lblExecDept.Size = new System.Drawing.Size(151, 16);
				this.lblExecDept.TabIndex = 2;
				// 
				// label1
				// 
				this.label1.Location = new System.Drawing.Point(520, 45);
				this.label1.Name = "label1";
				this.label1.Size = new System.Drawing.Size(61, 14);
				this.label1.TabIndex = 5;
				this.label1.Text = "打印日期:";
				// 
				// lblPrintDate
				// 
				this.lblPrintDate.Location = new System.Drawing.Point(585, 42);
				this.lblPrintDate.Name = "lblPrintDate";
				this.lblPrintDate.Size = new System.Drawing.Size(123, 17);
				this.lblPrintDate.TabIndex = 6;
				// 
				// dateTimePicker1
				// 
				this.dateTimePicker1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
				this.dateTimePicker1.Location = new System.Drawing.Point(232, 263);
				this.dateTimePicker1.Name = "dateTimePicker1";
				this.dateTimePicker1.Size = new System.Drawing.Size(133, 21);
				this.dateTimePicker1.TabIndex = 7;
				this.dateTimePicker1.Visible = false;
				// 
				// label2
				// 
				this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
				this.label2.Location = new System.Drawing.Point(24, 136);
				this.label2.Name = "label2";
				this.label2.Size = new System.Drawing.Size(24, 376);
				this.label2.TabIndex = 3;
				this.label2.Text = "提示：单击列标题进行排序！默认纸张A4纸。";
				// 
				// panel4
				// 
				this.panel4.BackColor = System.Drawing.Color.White;
				this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
				this.panel4.Controls.Add(this.label2);
				this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
				this.panel4.Location = new System.Drawing.Point(0, 0);
				this.panel4.Name = "panel4";
				this.panel4.Size = new System.Drawing.Size(888, 525);
				this.panel4.TabIndex = 4;
				// 
				// frmPreviewForDataSet
				// 
				this.BackColor = System.Drawing.SystemColors.Control;
				this.ClientSize = new System.Drawing.Size(888, 525);
				this.Controls.Add(this.toolBar1);
				this.Controls.Add(this.panel1);
				this.Controls.Add(this.panel4);
				this.Name = "frmPreviewForDataSet";
				this.Text = "报表预览界面";
			
				this.Load += new System.EventHandler(this.frmCrReportBase_Load);
				this.Controls.SetChildIndex(this.panel4, 0);
				this.Controls.SetChildIndex(this.panel1, 0);
				this.Controls.SetChildIndex(this.toolBar1, 0);

				this.panel1.ResumeLayout(false);
				this.panel2.ResumeLayout(false);
				((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
				((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
				this.panel3.ResumeLayout(false);
				this.panel4.ResumeLayout(false);
				this.ResumeLayout(false);

			}
			#endregion

			private System.Windows.Forms.ToolBar toolBar1;
			private System.Windows.Forms.ToolBarButton tbPrint;
			private System.Windows.Forms.ToolBarButton tbExit;
			private System.Windows.Forms.ToolBarButton toolBarButton1;
			private System.Windows.Forms.Panel panel1;
			public System.Windows.Forms.Label lblTitle;
			public System.Windows.Forms.Label lblDateSpan;
			public System.Windows.Forms.Label lblExecDept;
			public FarPoint.Win.Spread.FpSpread fpSpread1;
			private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
			private System.Windows.Forms.Label label1;
			private System.Windows.Forms.Label lblPrintDate;
			private System.Windows.Forms.DateTimePicker dateTimePicker1;
			private System.Windows.Forms.ToolBarButton toolBarButton2;
			private System.Windows.Forms.ToolBarButton toolBarButton3;
			private System.Windows.Forms.ToolBarButton tbExp;
			private System.Windows.Forms.ToolBarButton tbPrintSetup;
			private System.Windows.Forms.Panel panel3;
			private System.Windows.Forms.Panel panel2;
			private System.Windows.Forms.Panel panel4;
			//报表标题
//			public string ReportTitle = "";
			public bool isHaveColumn0 = true;
			private bool isPreview = true;
			private System.Windows.Forms.ImageList imageList1;
		    private string isHidecolumns = null ;
			#region 页面属性
			public bool IsPreview
			{
				set
				{
					this.isPreview = value;
					if(value)
					{
						this.tbPrint.Text = "打印预览";
					}
					else
					{
						this.tbPrint.Text = "打   印";
					}
				}
			}
			/// <summary>
			/// 要隐藏的列   "|" 是列间隔标志
			/// </summary>
			public string isHideColumns
			{
				set
				{
					this.isHidecolumns = value;
				}
				get
				{
					return this.isHideColumns ;
				}
			}
            private bool isallowSort = false;
			/// <summary>
			/// 允许排序
			/// </summary>
			public bool isAllowSort
			{
				set
				{
					this.isallowSort = value;
					if (value == true)
					{
						label2.Visible = true ;
						FarPoint.Win.Spread.Column col; 
						for (int i = 0; i< fpSpread1.Sheets[0].ColumnCount; i ++)
						{
							col = fpSpread1.Sheets[0].Columns[i]; 
							//				col.SortIndicator = FarPoint.Win.Spread.Model.SortIndicator.Descending; 
							col.ShowSortIndicator = false; 
							col.AllowAutoSort = true;
						}
					}
					else
					{
						label2.Visible = false;
					}
				}
				get
				{
					return this.isallowSort ;
				}
			}
            private string columnsWidth = null;
			private System.Windows.Forms.Label label2;
			/// <summary>
			/// 设置列宽度   格式  1,20|2,34|5,60|7,120
			/// ","前是列索引值，后是宽度  "|" 是列间隔标志
			/// </summary>
			public string ColumnsWidth
			{
				set
				{
					this.columnsWidth = value ;
				}
				get
				{
					return this.columnsWidth ;
				}
			}
            private bool isShowRowHeader = false;
		
			/// <summary>
			/// 是否显示行标题
			/// </summary>
			public bool IsShowRowHeader
			{
				set
				{
					this.isShowRowHeader = value ;
				}
				get
				{
					return this.isShowRowHeader ;
				}
			}
			public string PrintTitle
			{
				set
				{
					this.lblTitle.Text = value;
				}
			}
			public string PrintSearchDateSpan
			{
				set
				{
					this.lblDateSpan.Text = value;
				}
			}
			public string PrintDate
			{
				set
				{
					this.lblPrintDate.Text = value ;
				}
			}
			public string PrintSearchDept
			{
				set
				{
					this.lblExecDept.Text = value;
				}
			}
			
            
			#endregion
            FS.FrameWork.WinForms.Classes.Print p = new  FS.FrameWork.WinForms.Classes.Print();
			private void frmCrReportBase_Load(object sender, System.EventArgs e) 
			{
  
                //显示行标题
				fpSpread1.Sheets[0].RowHeaderVisible = isShowRowHeader;
					float pageWidth = 0;//计算宽度值
					if (isHidecolumns != null )
					{
						try
						{
							string[] hidecolumns= this.isHidecolumns.Split('|');
							foreach(string columnindex in hidecolumns)
							{
								fpSpread1_Sheet1.Columns[Convert.ToInt16(columnindex)].Visible = false ;
							}
						}
						catch
						{
							MessageBox.Show("传入参数 isHidecolumns  错误，请检查格式！\n 或列索引值是否超出范围！");
							return ;
						}
					}
					//计算列宽度
					if (this.columnsWidth != null)
					{
						try
						{
							string[] columnswidth = this.columnsWidth.Split('|');
							foreach(string columnwidth in  columnswidth)
							{
								string[] indexWidth = columnwidth.Split(',');
								fpSpread1_Sheet1.Columns[Convert.ToInt16(indexWidth[0])].Width = Convert.ToInt16(indexWidth[1]) ;
							}
						}
						catch
						{
							MessageBox.Show("传入参数 columnsWidth  错误，请检查格式！\n 或列索引值是否超出范围！");
							return ;
						}
					}
					else
					{
						//如果不设置列宽度,系统默认按宽度 720 处理
						for(int i = 0; i< this.fpSpread1_Sheet1.ColumnCount ; i ++)
							pageWidth = pageWidth + fpSpread1_Sheet1.Columns[i].Width;
						float tempWith = 0;
						if (pageWidth > 720)
						{
							for(int i = 0; i< fpSpread1_Sheet1.ColumnCount ; i ++)
							{
								tempWith = fpSpread1_Sheet1.Columns[i].Width;
								float dWidth = fpSpread1_Sheet1.Columns[i].Width - (pageWidth -720)/fpSpread1_Sheet1.ColumnCount;
								if (dWidth <= 0)
									fpSpread1_Sheet1.Columns[i].Width =  tempWith ;
							}
						}
					}

				}

			private void Print()
			{
				if(this.isPreview)
				{
					p.PrintPreview(15,5,this.panel1);
				}
				else
				{
					p.PrintPage(15,5,this.panel1);
				}
			}

			private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e) 
			{
				if(e.Button == this.tbPrint) 
				{
					this.Print();
				}
				if(e.Button == this.tbExp) 
				{
                    saveExcel();
				}
				if(e.Button == this.tbPrintSetup) 
				{
					p.ShowPageSetup();
					p.PrintPreview(15,5,this.panel1);
				}
			}
			/// <summary>
			/// 导出
			/// </summary>
			private void saveExcel()
			{
				SaveFileDialog saveFileDialog1 = new SaveFileDialog();
 
				saveFileDialog1.Filter = "Excel files (*.xls)|*.xls|Dbf files (*.dbf)|*.dbf|All files (*.*)|*.*"  ;
				saveFileDialog1.FilterIndex = 2 ;
				saveFileDialog1.RestoreDirectory = true ;
 
				if(saveFileDialog1.ShowDialog() == DialogResult.OK)
				{
					this.fpSpread1.SaveExcel(saveFileDialog1.FileName,FarPoint.Win.Spread.Model.IncludeHeaders.BothCustomOnly);
				}

			}

		}
	}
