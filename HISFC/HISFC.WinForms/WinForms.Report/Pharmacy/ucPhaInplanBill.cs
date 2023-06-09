using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;


namespace FS.WinForms.Report.Pharmacy
{
    /// <summary>
    /// ucPhaInPlan 的摘要说明。
    /// </summary>
    public class ucPhaInPlanBill : System.Windows.Forms.UserControl, FS.HISFC.BizProcess.Interface.Pharmacy.IBillPrint
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.Container components = null;

        public ucPhaInPlanBill()
        {
            // 该调用是 Windows.Forms 窗体设计器所必需的。
            InitializeComponent();

            // TODO: 在 InitializeComponent 调用后添加任何初始化

            //this.sheetView1.Rows.Default.Height = 50F;

        }

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码
        /// <summary> 
        /// 设计器支持所需的方法 - 不要使用代码编辑器 
        /// 修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.lb2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl0 = new System.Windows.Forms.Label();
            this.lb11 = new System.Windows.Forms.Label();
            this.lb36 = new System.Windows.Forms.Label();
            this.lb12 = new System.Windows.Forms.Label();
            this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
            this.sheetView1 = new FarPoint.Win.Spread.SheetView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.lb2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lbl0);
            this.panel1.Controls.Add(this.lb11);
            this.panel1.Controls.Add(this.lb36);
            this.panel1.Controls.Add(this.lb12);
            this.panel1.Controls.Add(this.fpSpread1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(879, 416);
            this.panel1.TabIndex = 0;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(490, 72);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(169, 16);
            this.label13.TabIndex = 68;
            this.label13.Text = "负责人：";
            // 
            // lb2
            // 
            this.lb2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb2.Location = new System.Drawing.Point(3, 44);
            this.lb2.Name = "lb2";
            this.lb2.Size = new System.Drawing.Size(232, 14);
            this.lb2.TabIndex = 64;
            this.lb2.Text = "科室";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(248, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(211, 23);
            this.label1.TabIndex = 55;
            this.label1.Text = "单据号";
            // 
            // lbl0
            // 
            this.lbl0.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl0.Location = new System.Drawing.Point(280, 9);
            this.lbl0.Name = "lbl0";
            this.lbl0.Size = new System.Drawing.Size(240, 23);
            this.lbl0.TabIndex = 54;
            this.lbl0.Text = "药品入库计划单";
            // 
            // lb11
            // 
            this.lb11.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb11.Location = new System.Drawing.Point(248, 72);
            this.lb11.Name = "lb11";
            this.lb11.Size = new System.Drawing.Size(211, 16);
            this.lb11.TabIndex = 0;
            this.lb11.Text = "计划人";
            // 
            // lb36
            // 
            this.lb36.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb36.Location = new System.Drawing.Point(490, 44);
            this.lb36.Name = "lb36";
            this.lb36.Size = new System.Drawing.Size(112, 23);
            this.lb36.TabIndex = 53;
            this.lb36.Text = "页数";
            // 
            // lb12
            // 
            this.lb12.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb12.Location = new System.Drawing.Point(3, 72);
            this.lb12.Name = "lb12";
            this.lb12.Size = new System.Drawing.Size(232, 16);
            this.lb12.TabIndex = 1;
            this.lb12.Text = "计划日期";
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "3.0.2004.2005";
            this.fpSpread1.AccessibleDescription = "fpSpread1, Sheet1, Row 0, Column 0, ";
            this.fpSpread1.BackColor = System.Drawing.Color.Transparent;
            this.fpSpread1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fpSpread1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            this.fpSpread1.Location = new System.Drawing.Point(4, 91);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.sheetView1});
            this.fpSpread1.Size = new System.Drawing.Size(799, 191);
            this.fpSpread1.TabIndex = 49;
            this.fpSpread1.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread1.TextTipAppearance = tipAppearance1;
            this.fpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            // 
            // sheetView1
            // 
            this.sheetView1.Reset();
            this.sheetView1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.sheetView1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.sheetView1.ColumnCount = 11;
            this.sheetView1.RowCount = 0;
            this.sheetView1.RowHeader.ColumnCount = 0;
            this.sheetView1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.SystemColors.WindowText, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, true, false, true, true, true);
            this.sheetView1.ColumnHeader.Cells.Get(0, 0).Value = "药品码";
            this.sheetView1.ColumnHeader.Cells.Get(0, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.ColumnHeader.Cells.Get(0, 1).Value = "药品名称";
            this.sheetView1.ColumnHeader.Cells.Get(0, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.ColumnHeader.Cells.Get(0, 2).Value = "规格";
            this.sheetView1.ColumnHeader.Cells.Get(0, 3).Value = "单位";
            this.sheetView1.ColumnHeader.Cells.Get(0, 4).Value = "剂型";
            this.sheetView1.ColumnHeader.Cells.Get(0, 5).Value = "购进数量";
            this.sheetView1.ColumnHeader.Cells.Get(0, 6).Value = "购入价";
            this.sheetView1.ColumnHeader.Cells.Get(0, 7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.ColumnHeader.Cells.Get(0, 7).Value = "零售价";
            this.sheetView1.ColumnHeader.Cells.Get(0, 8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.ColumnHeader.Cells.Get(0, 8).Value = "购进金额";
            this.sheetView1.ColumnHeader.Cells.Get(0, 9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.ColumnHeader.Cells.Get(0, 9).Value = "零售金额";
            this.sheetView1.ColumnHeader.Cells.Get(0, 10).Value = "供货公司";
            this.sheetView1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.sheetView1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.sheetView1.ColumnHeader.DefaultStyle.Locked = false;
            this.sheetView1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.sheetView1.ColumnHeader.Rows.Get(0).Height = 26F;
            this.sheetView1.Columns.Get(0).CellType = textCellType1;
            this.sheetView1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.sheetView1.Columns.Get(0).Label = "药品码";
            this.sheetView1.Columns.Get(0).Width = 45F;
            this.sheetView1.Columns.Get(1).BackColor = System.Drawing.Color.White;
            this.sheetView1.Columns.Get(1).CellType = textCellType2;
            this.sheetView1.Columns.Get(1).ForeColor = System.Drawing.Color.Black;
            this.sheetView1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.sheetView1.Columns.Get(1).Label = "药品名称";
            this.sheetView1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetView1.Columns.Get(1).Width = 166F;
            this.sheetView1.Columns.Get(2).CellType = textCellType3;
            this.sheetView1.Columns.Get(2).ForeColor = System.Drawing.Color.Black;
            this.sheetView1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.sheetView1.Columns.Get(2).Label = "规格";
            this.sheetView1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetView1.Columns.Get(2).Width = 65F;
            this.sheetView1.Columns.Get(3).Label = "单位";
            this.sheetView1.Columns.Get(3).Width = 53F;
            this.sheetView1.Columns.Get(5).CellType = numberCellType1;
            this.sheetView1.Columns.Get(5).Label = "购进数量";
            this.sheetView1.Columns.Get(5).Width = 71F;
            this.sheetView1.Columns.Get(6).CellType = numberCellType2;
            this.sheetView1.Columns.Get(6).Label = "购入价";
            this.sheetView1.Columns.Get(6).Width = 53F;
            this.sheetView1.Columns.Get(7).CellType = textCellType4;
            this.sheetView1.Columns.Get(7).ForeColor = System.Drawing.Color.Black;
            this.sheetView1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.sheetView1.Columns.Get(7).Label = "零售价";
            this.sheetView1.Columns.Get(7).Width = 45F;
            this.sheetView1.Columns.Get(8).CellType = numberCellType3;
            this.sheetView1.Columns.Get(8).ForeColor = System.Drawing.Color.Black;
            this.sheetView1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.sheetView1.Columns.Get(8).Label = "购进金额";
            this.sheetView1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetView1.Columns.Get(9).ForeColor = System.Drawing.Color.Black;
            this.sheetView1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.sheetView1.Columns.Get(9).Label = "零售金额";
            this.sheetView1.Columns.Get(9).Width = 59F;
            this.sheetView1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.sheetView1.Columns.Get(10).Label = "供货公司";
            this.sheetView1.Columns.Get(10).Width = 125F;
            this.sheetView1.RowHeader.Columns.Default.Resizable = false;
            this.sheetView1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.sheetView1.RowHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.sheetView1.RowHeader.DefaultStyle.Locked = false;
            this.sheetView1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.sheetView1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.sheetView1.SheetCornerStyle.Locked = false;
            this.sheetView1.SheetCornerStyle.Parent = "HeaderDefault";
            this.sheetView1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.sheetView1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpSpread1.SetActiveViewport(0, 1, 0);
            // 
            // ucPhaInPlanBill
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Name = "ucPhaInPlanBill";
            this.Size = new System.Drawing.Size(879, 416);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetView1)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        #region 属性
        /// <summary>
        /// 是否补打
        /// </summary>
        public bool IsReprint = false;
        /// <summary>
        /// 
        /// </summary>
        private int decimals;
        /// <summary>
        /// 精度（注意farpoint小数位数）
        /// </summary>
        public int Decimals
        {
            get
            {
                return this.decimals;
            }
            set
            {
                this.decimals = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private int maxrowno;
        /// <summary>
        /// 最大行数（修改时请注意实际单据长度）
        /// </summary>
        public int MaxRowNo
        {
            get
            {
                return this.maxrowno;
            }
            set
            {
                this.maxrowno = value;
            }
        }
        
  
        /// <summary>
        /// 科室类
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();

        #region 设计器生成的
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lb11;
        private System.Windows.Forms.Label lb12;
        #endregion

        /// <summary>
        /// 药品函数
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Item itemMgr = new FS.HISFC.BizLogic.Pharmacy.Item();

        private FS.HISFC.BizLogic.Pharmacy.Constant conMgr = new FS.HISFC.BizLogic.Pharmacy.Constant();
        private FarPoint.Win.Spread.FpSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView sheetView1;
        private System.Windows.Forms.Label lb36;
        /// <summary>
        /// 人员类
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Person psMgr = new FS.HISFC.BizLogic.Manager.Person();
        private Label lbl0;
        private Label label1;
        private Label label13;
        private Label lb2;

        private FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();
        #endregion

        #region 方法

        #region 设置打印格式

        /// <summary>
        /// 设置打印数据
        /// </summary>
        /// <param name="al">数据</param>
        /// <param name="pageno">页码</param>
        /// <param name="operCode">操作员</param>
        /// <param name="Kind">打印类型 0.入库计划单  1.采购单   2.入库单   3.出库单    4.</param>
        public void SetDataForInput(ArrayList al, int pageno, string operCode, string kind)
        {
            this.panel1.Width = this.Width;
            try
            {
                ArrayList alPrint = new ArrayList();
                int icount = FS.FrameWork.Function.NConvert.ToInt32(Math.Ceiling(Convert.ToDouble(al.Count) / MaxRowNo));

                for (int i = 1; i <= icount; i++)
                {
                    if (i != icount)
                    {
                        alPrint = al.GetRange(MaxRowNo * (i - 1), MaxRowNo);
                        this.Print(alPrint, i, icount, operCode, kind);
                    }
                    else
                    {
                        int num = al.Count % MaxRowNo;
                        if (al.Count % MaxRowNo == 0)
                        {
                            num = MaxRowNo;
                        }
                        alPrint = al.GetRange(MaxRowNo * (i - 1), num);
                        this.Print(alPrint, i, icount, operCode, kind);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("打印出错!" + e.Message);
                return;
            }
        }

        #endregion

        #region 打印主函数

        /// <summary>
        /// 打印函数
        /// </summary>
        /// <param name="al">打印数组</param>
        /// <param name="i">第几页</param>
        /// <param name="count">总页数</param>
        /// <param name="operCode">制单人</param>
        /// <param name="kind">打印类型 0.入库计划单  1.采购单   2.入库单   3.出库单    4.</param>
        private void Print(ArrayList al, int inow, int icount, string operCode, string kind)
        {
            this.PrintInPlan(al, inow, icount, operCode);
        }
        #endregion

        #region 入库计划单打印

        /// <summary>
        /// 打印函数
        /// </summary>
        /// <param name="al">打印数组</param>
        /// <param name="i">第几页</param>
        /// <param name="count">总页数</param>
        /// <param name="operCode">制单人</param>
        private void PrintInPlan(ArrayList al, int inow, int icount, string operCode)
        {
            if (al.Count <= 0)
            {
                MessageBox.Show("没有打印的数据!");
                return;
            }
            FS.HISFC.BizLogic.Pharmacy.Constant constant = new FS.HISFC.BizLogic.Pharmacy.Constant();
            FS.HISFC.Models.Pharmacy.InPlan inPlan = (FS.HISFC.Models.Pharmacy.InPlan)al[0];
            FS.HISFC.BizProcess.Integrate.Pharmacy itemMgr = new FS.HISFC.BizProcess.Integrate.Pharmacy();
            FS.HISFC.BizLogic.Manager.Constant constantMgr= new FS.HISFC.BizLogic.Manager.Constant();

            string dosageID = string.Empty; //剂型

            #region label赋值
     
            if (this.IsReprint)
            {
                this.lbl0.Text = this.lbl0.Text + "(补打)";
            }

            //DateTime sysTime = this.itemMgr.GetDateTimeFromSysDateTime();
            //this.lb11.Text = "计划科室  " + inPlan.Dept+this.deptMgr.get + " 计划人  " + inPlan.PlanOper.ID;
            this.lb11.Text =  " 计划人：  " + inPlan.PlanOper.Name;
            this.lb12.Text = "计划日期：" + this.itemMgr.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd");
            this.lb36.Text = "第" + inow.ToString() + "页/共" + icount.ToString() + "页";
            this.label1.Text = "单据号：" + inPlan.BillNO;
            this.lb2.Text = "科室：" + inPlan.Dept.Name;
            
     
            #endregion

            #region farpoint赋值

            decimal sumNum5 = 0;
            decimal sumNum8 = 0;
            decimal sumNum9 = 0;
            this.sheetView1.RowCount = 0;
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            numberCellType1.DecimalPlaces = 4;
            this.sheetView1.Columns[6].CellType = numberCellType1;//{1EC17564-2FAD-4a77-97AC-4C57076888B2}
            this.sheetView1.Columns[9].CellType = numberCellType1;
            for (int i = 0; i < al.Count; i++)
            {
                this.sheetView1.AddRows(i, 1);
                FS.HISFC.Models.Pharmacy.InPlan info = al[i] as FS.HISFC.Models.Pharmacy.InPlan;
                //this.sheetView1.Cells[i, 0].Text = this.itemMgr.GetItem(output.Item.ID).NameCollection.UserCode;//药品自定义码
                //this.sheetView1.Cells[i, 0].Text = info.Item.NameCollection.UserCode;//药品自定义码
                this.sheetView1.Cells[i, 0].Text = info.Item.ID;//药品编码
                this.sheetView1.Cells[i, 1].Text = info.Item.Name;//药品名称
                this.sheetView1.Cells[i, 2].Text = info.Item.Specs;//规格
                this.sheetView1.Cells[i, 3].Text = info.Item.PackUnit;//单位

                dosageID = ((FS.HISFC.Models.Pharmacy.Item)this.itemMgr.GetItem(info.Item.ID)).DosageForm.ID;

                this.sheetView1.Cells[i, 4].Text = ((FS.FrameWork.Models.NeuObject)this.constantMgr.GetConstant("DOSAGEFORM", dosageID)).Name; //剂型

                if (info.Item.PackQty == 0) info.Item.PackQty = 1;
                

                this.sheetView1.Cells[i, 5].Text = (info.PlanQty / info.Item.PackQty).ToString();     //计划数量
                this.sheetView1.Cells[i, 6].Text = info.Item.PriceCollection.PurchasePrice.ToString();  //计划购入价  
                this.sheetView1.Cells[i, 7].Text = info.Item.PriceCollection.RetailPrice.ToString();//零售价
                this.sheetView1.Cells[i, 8].Text = ((info.PlanQty / info.Item.PackQty) * (info.Item.PriceCollection.PurchasePrice)).ToString();//计划金额   
                this.sheetView1.Cells[i, 9].Text = ((info.PlanQty / info.Item.PackQty) * (info.Item.PriceCollection.RetailPrice)).ToString();//零售金额   

             

                //if (info.Item.PackQty == 0) info.Item.PackQty = 1;
                //decimal count = 0, count2 = 0;
                //count = info.PlanQty / info.Item.PackQty;
                //count2 = (info.PlanQty / info.Item.PackQty) * (info.Item.PriceCollection.PurchasePrice);
   
                //this.sheetView1.Cells[i, 3].Text = info.Item.PriceCollection.PurchasePrice.ToString();  //计划购入价             
                //this.sheetView1.Cells[i, 4].Text = (info.PlanQty / info.Item.PackQty).ToString();     //计划数量
                //this.sheetView1.Cells[i, 5].Text = info.Item.PackUnit;//单位
                //this.sheetView1.Cells[i, 6].Text = ((info.PlanQty / info.Item.PackQty) * (info.Item.PriceCollection.PurchasePrice)).ToString();//计划金额             



                if (info.Item.Product.Company.Name != "")
                    this.sheetView1.Cells[i, 10].Text = info.Item.Product.Company.Name;         //供货公司;   
                else
                    this.sheetView1.Cells[i, 10].Text = "未选择";
                //if (info.Item.Product.Producer.Name != "")
                //    this.sheetView1.Cells[i, 8].Text = info.Item.Product.Producer.Name;        //生产厂家  
                //else
                //    this.sheetView1.Cells[i, 8].Text = "未选择";

                decimal count = 0, count2 = 0, count3 = 0;
                count = info.PlanQty / info.Item.PackQty;
                count2 = (info.PlanQty / info.Item.PackQty) * (info.Item.PriceCollection.PurchasePrice);
                count3 = (info.PlanQty / info.Item.PackQty) * (info.Item.PriceCollection.RetailPrice);
   
                sumNum5 = sumNum5 + count;
                sumNum8 = sumNum8 + count2;
                sumNum9 = sumNum9 + count3;

            }
            this.sheetView1.RowCount = al.Count + 1;
            this.sheetView1.Cells[al.Count, 0].Text = "合计";
            this.sheetView1.Cells[al.Count, 1].Text = "共" + al.Count + "行";//行数;
            this.sheetView1.Cells[al.Count, 5].Text = sumNum5.ToString();
            this.sheetView1.Cells[al.Count, 8].Text = sumNum8.ToString();
            this.sheetView1.Cells[al.Count, 9].Text = sumNum9.ToString();
            //宽度
            //this.panel4.Width = this.Width - 3;
            this.fpSpread1.Width = this.panel1.Width - 10;
            this.fpSpread1.Height = (int)this.sheetView1.RowHeader.Rows[0].Height +
                (int)(this.sheetView1.Rows[0].Height * (al.Count + 1)) + 10;

            #endregion

            #region 打印函数

            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();

            FS.HISFC.Components.Common.Classes.Function.GetPageSize("PhaInplan", ref p);

            p.PrintPage(5, 0, this.panel1);

            #endregion
        }

        #endregion

        #region IBillPrint 成员

        public int Prieview()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Print()
        {
            return 1;
        }

        public int SetData(ArrayList alPrintData, FS.HISFC.BizProcess.Interface.Pharmacy.BillType billType)
        {
            this.maxrowno = 12;
            this.panel1.Width = this.Width;

            ArrayList a0 = new ArrayList();
            ArrayList a1 = new ArrayList();
            ArrayList a2 = new ArrayList();
            ArrayList a3 = new ArrayList();
            ArrayList a4 = new ArrayList();
            ArrayList a5 = new ArrayList();

            #region 按照自定义编码进行分组

            foreach (FS.HISFC.Models.Pharmacy.InPlan inPlan in alPrintData)
            {
                try
                {
                    string code = inPlan.Item.NameCollection.UserCode;
                    string firstLetter = code.Substring(0, 1);
                    if (firstLetter == "1" || firstLetter == "2" || firstLetter == "3")
                    {
                        a0.Add(inPlan);
                    }
                    else if (firstLetter == "0")
                    {
                        a1.Add(inPlan);
                    }
                    else if (firstLetter == "4")
                    {
                        a2.Add(inPlan);
                    }
                    else if (firstLetter == "5")
                    {
                        a3.Add(inPlan);
                    }
                    else if (firstLetter == "8")
                    {
                        a4.Add(inPlan);
                    }
                    else if (firstLetter == "9")
                    {
                        a5.Add(inPlan);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("出错!" + e.Message);
                }
            }

            #endregion

            #region 调用打印函数 完成打印操作
            int inow = 0, icount = 0; string operCode = ",";
            try
            {
                //if (a0.Count != 0)
                //{
                //    this.label3.Text = "西药库";
                //    this.PrintGroupData(a0);

                //}
                //if (a1.Count != 0)
                //{
                //    this.label3.Text = "液体库";
                //    this.PrintGroupData(a1);
                //}
                //if (a2.Count != 0)
                //{
                //    this.label3.Text = "新特药库";
                //    this.PrintGroupData(a2);
                //}
                //if (a3.Count != 0)
                //{
                //    this.label3.Text = "自制药库";
                //    this.PrintGroupData(a3);
                //}
                //if (a4.Count != 0)
                //{
                //    this.label3.Text = "草药库";
                //    this.PrintGroupData(a4);
                //}
                //if (a5.Count != 0)
                //{
                //    this.label3.Text = "中成药药库";
                //    this.PrintGroupData(a5);
                //}
                this.PrintGroupData(alPrintData);
                return 1;
            }
            catch (Exception e)
            {
                MessageBox.Show("出错!" + e.Message);
                return -1;
            }

            #endregion
                      
        }

        public int SetData(ArrayList alPrintData, string privType)
        {

            return 1;
        }

        private int PrintGroupData(ArrayList alPrintData)
        {
            ArrayList alPrint = new ArrayList();
            int icount = FS.FrameWork.Function.NConvert.ToInt32(Math.Ceiling(Convert.ToDouble(alPrintData.Count) / MaxRowNo));

            for (int i = 1; i <= icount; i++)
            {

                if (i != icount)
                {
                    alPrint = alPrintData.GetRange(MaxRowNo * (i - 1), MaxRowNo);
                    this.Print(alPrint, i, icount, "", "");
                }
                else
                {
                    int num = alPrintData.Count % MaxRowNo;
                    if (alPrintData.Count % MaxRowNo == 0)
                    {
                        num = MaxRowNo;
                    }
                    alPrint = alPrintData.GetRange(MaxRowNo * (i - 1), num);
                    this.Print(alPrint, i, icount, "", "");
                }

            }
            return 1;
        }

        public int SetData(string billNO)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        private void label5_Click(object sender, EventArgs e)
        {

        }

    }
}
        #endregion 