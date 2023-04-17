using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.DrugStore.GuangZhou.GYSY.Inpatient
{
    /// <summary>
    /// [功能描述: 住院药房汇总单打印本地化实例]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-12]<br></br>
    /// 说明：
    /// 1、作为一个例子保留下来，不要更改
    /// 2、各项目如果修改不大的话，可以考虑继承方式
    /// </summary>
    public partial class ucTotalDrugBill : UserControl, FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientBill
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ucTotalDrugBill()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 每页的行数，这个是按照Letter纸张调整的，行高改变影响分页
        /// </summary>
        int pageRowNum = 200;

        /// <summary>
        /// 最大页码
        /// </summary>
        int totPageNO = 0;

        /// <summary>
        /// 打印的有效行数,当选择页码范围时有效
        /// </summary>
        int validRowNum = 0;

        FS.HISFC.Models.Base.PageSize pageSize = null;
        FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
        /// <summary>
        /// 常数管理类
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        #endregion

        #region 摆药单的通用方法

        /// <summary>
        /// 清屏
        /// </summary>
        private void Clear()
        {
            this.nlbTitle.Text = "住院药房汇总摆药单";
            this.nlbRowCount.Text = "记录数：";
            this.nlbBillNO.Text = "单据号：";
            this.nlbFirstPrintTime.Text = "首次打印：";
            this.nlbStockDept.Text = "发药科室：";
            this.nlbPrintTime.Text = "打印时间：";

            this.neuSpread1_Sheet1.Rows.Count = 0;
        }

        /// <summary>
        /// Fp设置
        /// </summary>
        private void SetFormat()
        {
            this.neuSpread1.ReadSchema(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InpatientDrugStoreTotDrugBill.xml");
        }

        /// <summary>
        /// 数据显示
        /// </summary>
        /// <param name="alData">出库申请applyout，而且是按照药品编码汇总了的</param>
        /// <param name="drugBillClass"></param>
        private void ShowBillTotData(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept)
        {
            FS.SOC.HISFC.BizLogic.Pharmacy.InOut itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.InOut(); 
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alData)
            {
                info.PlaceNO = itemMgr.GetPlaceNO(info.StockDept.ID, info.Item.ID);
            }
            alData.Sort(new CompareApplyOutByPlaceNO());

            this.SuspendLayout();
            string applyDeptName = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugBillClass.ApplyDept.ID);

            this.nlbTitle.Text = applyDeptName + drugBillClass.Name + "(汇总)";

            this.nlbRowCount.Text = "记录数：" + alData.Count.ToString();
            this.nlbBillNO.Text = "单据号：" + drugBillClass.DrugBillNO;
            this.nlbStockDept.Text = "发药科室：" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(stockDept.ID);

            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();

             

            int iRow = 0;            
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alData)
            {

                #region 数据赋值

                this.neuSpread1_Sheet1.Rows.Add(iRow, 1);
                this.neuSpread1.SetCellValue(0, iRow, "编码", SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemCustomNO(info.Item.ID));

                FS.HISFC.Models.Pharmacy.Item item = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(info.Item.ID);
                if (ctrlIntegrate.GetControlParam<bool>("HNPHA2", false, true))
                {

                    this.neuSpread1.SetCellValue(0, iRow, "名称", item.NameCollection.RegularName);
                }
                else
                {
                    this.neuSpread1.SetCellValue(0, iRow, "名称", item.Name);
                }
                //this.neuSpread1.SetCellValue(0, iRow, "名称", info.Item.Name);
                this.neuSpread1.SetCellValue(0, iRow, "规格", "  " + info.Item.Specs);
                this.neuSpread1.SetCellValue(0, iRow, "货位号", info.PlaceNO);
                

                this.neuSpread1.SetCellValue(0, iRow, "金额", (info.Item.PriceCollection.RetailPrice * (info.Operation.ApplyQty / info.Item.PackQty)).ToString("F2"));

                decimal applyQty = info.Operation.ApplyQty;
                string unit = info.Item.MinUnit;
                decimal price = info.Item.PriceCollection.RetailPrice;


                int outMinQty;
                int outPackQty = System.Math.DivRem((int)(info.Operation.ApplyQty ), (int)info.Item.PackQty, out outMinQty);
                if (string.IsNullOrEmpty(info.Item.PackUnit))
                {
                    if (info.Item.PackQty == 1)
                    {
                        info.Item.PackUnit = info.Item.MinUnit;
                    }
                    else
                    {
                        try 
                        {
                            info.Item.PackUnit = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(info.Item.ID).PackUnit;
                        }
                        catch { }
                    }
                }
                if (outPackQty == 0)
                {
                    applyQty = info.Operation.ApplyQty;
                    unit = info.Item.MinUnit;
                    price = info.Item.PriceCollection.RetailPrice / info.Item.PackQty;
                }
                else if (outMinQty == 0)
                {
                    applyQty = outPackQty;
                    unit = info.Item.PackUnit;
                }
                else
                {
                    applyQty = outPackQty;
                    unit = info.Item.PackUnit + outMinQty.ToString() + info.Item.MinUnit;
                }

                //打印时间，首次打印也是在保存后打印的，info.State不会等于0，而drugBillClass.ApplyState在调用该控件前处理过
                if (drugBillClass.ApplyState != "0")
                {
                    this.nlbFirstPrintTime.Text = "首次打印：" + info.Operation.ExamOper.OperTime.ToString();
                    this.nlbPrintTime.Text = "打印时间：" + DateTime.Now;
                }
                else
                {
                    this.nlbPrintTime.Text = "打印时间：" + info.Operation.ExamOper.OperTime.ToString();
                    this.nlbFirstPrintTime.Text = "";
                }


                this.neuSpread1.SetCellValue(0, iRow, "数量", applyQty.ToString("F4").TrimEnd('0').TrimEnd('.') + unit);
                //this.neuSpread1.SetCellValue(0, iRow, "单位", unit);
                this.neuSpread1.SetCellValue(0, iRow, "单价", price.ToString("F4").TrimEnd('0').TrimEnd('.'));

                #endregion

                iRow++;
            }

            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();

            #region 页码及汇总信息等处理

            try
            {
                if (this.neuSpread1_Sheet1.Rows.Count > 0)
                {
                    int index = this.neuSpread1_Sheet1.Rows.Count;
                    //页码
                    totPageNO = (int)Math.Ceiling((double)index / pageRowNum);
                    //从最后一页开始回溯添加页码
                    for (int pageNO = totPageNO; pageNO > 0; pageNO--)
                    {
                        //最后一页
                        if (pageNO == totPageNO)
                        {
                            this.neuSpread1_Sheet1.AddRows(index, 1);
                            //打印单底部文字
                            this.neuSpread1_Sheet1.Cells[index, 0].ColumnSpan = 8;
                            this.neuSpread1_Sheet1.Cells[index, 0].Text = "发药：                       核对：                      领药：                      ";
                            this.neuSpread1_Sheet1.Cells[index, 0].Font = new Font("宋体", 12f);
                            this.neuSpread1_Sheet1.Cells[index, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                            //标记页码，补打选择页码时用
                            this.neuSpread1_Sheet1.Rows[index].Tag = pageNO;
                            continue;
                        }
                        //到分页处添加一行
                        this.neuSpread1_Sheet1.AddRows(pageNO * pageRowNum, 1);
                        //页码位置在价格处
                        if (this.neuSpread1_Sheet1.Columns.Count - 1 > 6)
                        {
                            this.neuSpread1_Sheet1.Cells[index, 6].ColumnSpan = 2;
                        }
                        this.neuSpread1_Sheet1.Cells[pageNO * pageRowNum, 6].Text = "页：" + pageNO.ToString() + "/" + totPageNO.ToString();

                        //标记页码，补打选择页码时用
                        this.neuSpread1_Sheet1.Rows[pageNO * pageRowNum].Tag = pageNO;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            SetSheetStyle();

            this.ResumeLayout(true);

            #endregion
        }

        /// <summary>
        /// 摆药单格式打印
        /// 这个用于显示控件
        /// </summary>
        /// <param name="alData">applyout出库申请实体数组</param>
        /// <param name="drugBillClass">摆药单分类</param>
        /// <param name="stockDept">实发科室</param>
        private void ShowBillData(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept)
        {
            //清除数据
            this.Clear();

            //计算汇总量用，哈希表汇总
            Hashtable hsTotData = new Hashtable();
            System.Collections.ArrayList alTotData = new ArrayList();
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alData)
            {
                //汇总信息
                if (hsTotData.Contains(applyOut.Item.ID))
                {
                    FS.HISFC.Models.Pharmacy.ApplyOut applyOutTot = hsTotData[applyOut.Item.ID] as FS.HISFC.Models.Pharmacy.ApplyOut;
                    applyOutTot.Operation.ApplyQty += applyOut.Operation.ApplyQty;
                }
                else
                {
                    FS.HISFC.Models.Pharmacy.ApplyOut applyOutTot = applyOut.Clone();
                    hsTotData.Add(applyOutTot.Item.ID, applyOutTot);
                    alTotData.Add(applyOutTot);
                }
            }

            this.ShowBillTotData(alTotData, drugBillClass, stockDept);

            //重置标题位置
            this.ResetTitleLocation(false);
        }

        /// <summary>
        /// 获取纸张
        /// </summary>
        private FS.HISFC.Models.Base.PageSize GetPaperSize()
        {
            FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
            string dept = ((FS.HISFC.Models.Base.Employee)pageSizeMgr.Operator).Dept.ID;
            FS.HISFC.Models.Base.PageSize paperSize = pageSizeMgr.GetPageSize("InPatientDrugBillT", dept);

            //自适应纸张，注意打印机驱动不可以是正确的
            if (paperSize == null || paperSize.Height > 5000)
            {
                paperSize = new FS.HISFC.Models.Base.PageSize();
                paperSize.Name = DateTime.Now.ToString();
                try
                {
                    int width = 800;

                    int curHeight = 0;

                    int addHeight = this.validRowNum * (int)this.neuSpread1.ActiveSheet.Rows[0].Height;

                    int additionAddHeight = 120;

                    paperSize.Width = width;
                    paperSize.Height = (addHeight + curHeight + additionAddHeight);

                    this.Height = paperSize.Height;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("设置纸张出错>>" + ex.Message);
                }
            }
            if (!string.IsNullOrEmpty(paperSize.Printer) && paperSize.Printer.ToLower() == "default")
            {
                paperSize.Printer = "";
            }
            return paperSize;
        }

        /// <summary>
        /// 打印
        /// </summary>
        private void PrintPage()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

            this.Dock = DockStyle.None;

            FS.HISFC.Models.Base.PageSize paperSize = this.GetPaperSize();
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

            print.SetPageSize(paperSize);

            //管理员可以预览，方便查看问题
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(10, 10, this);
            }
            else
            {
                print.PrintPage(10, 10, this);
            }

            this.Dock = DockStyle.Fill;
        }

        #endregion

        #region 汇总单的特殊方法

        /// <summary>
        /// 设置FarPoint的样式
        /// </summary>
        private void SetSheetStyle()
        {
            FarPoint.Win.LineBorder lineBorder1 = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, false, false, true);
            FarPoint.Win.LineBorder lineBorder2 = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, false, false, false);

            for (int i = 0, j = this.neuSpread1_Sheet1.Rows.Count - 1; i < j; i++)
            {
                for (int k = 0; k < 7; k++)
                {
                    if (i < j - 1)
                    {
                        if (string.IsNullOrEmpty(this.neuSpread1_Sheet1.Cells[i, 2].Text))
                        {
                            this.neuSpread1_Sheet1.Cells.Get(i, k).Border = lineBorder2;
                        }
                        else
                        {
                            this.neuSpread1_Sheet1.Cells.Get(i, k).Border = lineBorder1;
                        }
                    }
                    else if (i == j - 1)
                    {
                        this.neuSpread1_Sheet1.Cells.Get(i, k).Border = lineBorder2;
                    }

                    this.neuSpread1_Sheet1.Cells.Get(i, k).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                }

                FarPoint.Win.LineBorder lineBorder3 = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 2, false, true, false, false);
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 0).Border = lineBorder3;
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            }
        }

        /// <summary>
        /// 重新设置标题位置
        /// </summary>
        private void ResetTitleLocation(bool isPrint)
        {
            this.nlbTitle.Dock = DockStyle.None;
            this.neuPanel1.Controls.Remove(this.nlbTitle);

            int with = 0;
            for (int colIndex = 0; colIndex < this.neuSpread1_Sheet1.ColumnCount; colIndex++)
            {
                if (this.neuSpread1_Sheet1.Columns[colIndex].Visible)
                {
                    with += (int)this.neuSpread1_Sheet1.Columns[colIndex].Width;
                }
            }
            if (!isPrint && with > this.neuPanel1.Width)
            {
                with = this.neuPanel1.Width;
            }
            this.nlbTitle.Location = new Point((with - this.nlbTitle.Size.Width) / 2, this.nlbTitle.Location.Y);

            this.neuPanel1.Controls.Add(this.nlbTitle);

        }

        #endregion

        #region 公用方法
        /// <summary>
        /// 初始化设置
        /// </summary>
        public void Init()
        {
            this.Clear();
            this.SetFormat();
            this.neuSpread1.ColumnWidthChanged -= new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread1_ColumnWidthChanged);
            this.neuSpread1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread1_ColumnWidthChanged);
        }

        void neuSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            this.neuSpread1.SaveSchema(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InpatientDrugStoreTotDrugBill.xml");
        }

        /// <summary>
        /// 提供没有范围选择的打印
        /// 一般在摆药保存时调用
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="drugBillClass"></param>
        /// <param name="stockDept"></param>
        public void PrintData(ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept)
        {
            if (alData == null || alData.Count == 0)
            {
                return;
            }

            this.ShowBillData(alData, drugBillClass, stockDept);
            this.validRowNum = this.neuSpread1_Sheet1.RowCount;
            this.PrintPage();
        }

        #endregion

        #region 排序类
        /// <summary>
        /// 排序类
        /// </summary>
        private class CompareApplyOutByPlaceNO : IComparer
        {
            public int Compare(object x, object y)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut o1 = x as FS.HISFC.Models.Pharmacy.ApplyOut;
                FS.HISFC.Models.Pharmacy.ApplyOut o2 = y as FS.HISFC.Models.Pharmacy.ApplyOut;

                string oX = o1.PlaceNO;          //货位号
                if (oX == null)
                {
                    oX = "";
                }
                string oY = o2.PlaceNO;          //货位号
                if (oY == null)
                {
                    oY = "";
                }
                //string oX = o1.Item.ID.ToString();
                //if (oX == null)
                //{
                //    oX = "";
                //}
                //string oY = o2.Item.ID.ToString();
                //if (oY == null)
                //{
                //    oY = "";
                //}

                return string.Compare(oX.ToString(), oY.ToString());
            }

        }

        #endregion

        #region IInpatientBill 成员，补打时用

        /// <summary>
        /// 提供摆药单数据显示的方法
        /// 一般在摆药单补打时调用
        /// </summary>
        /// <param name="alData">出库申请applyout</param>
        /// <param name="drugBillClass">摆药单分类</param>
        /// <param name="stockDept">库存科室</param>
        public void ShowData(ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept)
        {
            this.ShowBillData(alData, drugBillClass, stockDept);
        }

        /// <summary>
        /// 提供可以选择打印范围的打印方法
        /// </summary>
        public void OldPrint()
        {
            //if (this.totPageNO == 1)
            //{
            //    this.PrintPage();
            //}

            this.validRowNum = this.neuSpread1_Sheet1.RowCount;

            SOC.Windows.Forms.PrintPageSelectDialog printPageSelectDialog = new SOC.Windows.Forms.PrintPageSelectDialog();
            printPageSelectDialog.StartPosition = FormStartPosition.CenterScreen;
            printPageSelectDialog.MaxPageNO = this.totPageNO;
            printPageSelectDialog.ShowDialog();

            //开始页码为0，说明用户取消打印
            if (printPageSelectDialog.FromPageNO == 0)
            {
                return;
            }

            //打印全部
            if ((printPageSelectDialog.FromPageNO == 1 && printPageSelectDialog.ToPageNO == this.totPageNO))
            {
                this.PrintPage();
                return;
            }

            //选择了页
            int curPageNO = 1;
            for (int rowIndex = 0; rowIndex < this.neuSpread1_Sheet1.RowCount; rowIndex++)
            {
                if (this.neuSpread1_Sheet1.Rows[rowIndex].Tag != null)
                {
                    curPageNO = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Rows[rowIndex].Tag) + 1;
                }
                if (curPageNO >= printPageSelectDialog.FromPageNO && curPageNO <= printPageSelectDialog.ToPageNO)
                {
                    this.neuSpread1_Sheet1.Rows[rowIndex].Visible = true;
                }
                else
                {
                    this.neuSpread1_Sheet1.Rows[rowIndex].Visible = false;
                    this.validRowNum--;
                }
            }

            this.PrintPage();

            for (int rowIndex = 0; rowIndex < this.neuSpread1_Sheet1.RowCount; rowIndex++)
            {
                this.neuSpread1_Sheet1.Rows[rowIndex].Visible = true;
            }

            this.validRowNum = this.neuSpread1_Sheet1.RowCount;
            
        }

        public void Print()
        {
            //启用华南打印基类打印
            FS.SOC.Windows.Forms.PrintExtendPaper print = new FS.SOC.Windows.Forms.PrintExtendPaper();

            //获取维护的纸张
            if (pageSize == null)
            {
                pageSize = pageSizeMgr.GetPageSize("InPatientDrugBillD");
                //指定打印处理，default说明使用默认打印机的处理
                if (pageSize != null && pageSize.Printer.ToLower() == "default")
                {
                    pageSize.Printer = "";
                }
                //没有维护时默认一个纸张
                if (pageSize == null)
                {
                    pageSize = new FS.HISFC.Models.Base.PageSize("InPatientDrugBillD", 800, 1100);
                }
            }

            //打印边距处理
            print.DrawingMargins = new System.Drawing.Printing.Margins(pageSize.Left, 0, pageSize.Top, 0);

            //纸张处理
            print.PaperName = pageSize.Name;
            print.PaperHeight = pageSize.Height;
            print.PaperWidth = pageSize.Width;

            //打印机名称
            print.PrinterName = pageSize.Printer;

            //页码显示
            this.lbPageNO.Tag = "页码：{0}/{1}";
            print.PageNOControl = this.lbPageNO;

            //页眉控件，表示每页都打印
            print.HeaderControls.Add(this.neuPanel1);
            //页脚控件，表示每页都打印
            //print.FooterControls.Add(this.plBottom);

            //不显示页码选择
            print.IsShowPageNOChooseDialog = true;

            //管理员使用预览功能
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPageView(this);
            }
            else
            {
                print.PrintPage(this);
            }

        }

        /// <summary>
        /// 设置Dock属性，补打时用
        /// </summary>
        public DockStyle WinDockStyle
        {
            get
            {
                return this.Dock;
            }
            set
            {
                this.Dock = value;
            }
        }

        /// <summary>
        /// 单据类型，补打时用
        /// </summary>
        public SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType InpatientBillType
        {
            get
            {
                return FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.汇总;
            }
        }

        #endregion

    }
}
