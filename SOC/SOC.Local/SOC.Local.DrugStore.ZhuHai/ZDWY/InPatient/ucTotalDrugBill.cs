using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.DrugStore.ZhuHai.ZDWY.InPatient
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
            this.PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDocumentPrintPage);
            PrintDocument.BeginPrint += new System.Drawing.Printing.PrintEventHandler(PrintDocument_BeginPrint);
        }

        #region 变量

        /// <summary>
        /// 每页的行数，这个是按照LetterpageRowNum，行高改变影响分页
        /// </summary>
        int pageRowNum = 14;

        /// <summary>
        /// 最大页码
        /// </summary>
        int totPageNO = 0;

        /// <summary>
        /// 打印的有效行数,当选择页码范围时有效
        /// </summary>
        int validRowNum = 0;

        /// <summary>
        /// 当前打印页的页码
        /// 程序自动计算的
        /// </summary>
        private int curPageNO = 1;

        /// <summary>
        /// 本次打印最大页码
        /// 程序自动计算的
        /// </summary>
        private int maxPageNO = 1;

        /// <summary>
        /// 打印用
        /// </summary>
        private System.Drawing.Printing.PrintDocument PrintDocument = new System.Drawing.Printing.PrintDocument();

        private System.Drawing.Printing.Margins DrawingMargins = new System.Drawing.Printing.Margins(20, 20, 10, 30);

        SOC.Windows.Forms.PrintPageSelectDialog socPrintPageSelectDialog = new FS.SOC.Windows.Forms.PrintPageSelectDialog();

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
            this.nlbStockDept.Text = "发药科室：";
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
        /// 获取发送类型
        /// </summary>
        /// <param name="applyOut"></param>
        /// <returns></returns>
        private string GetSendType(FS.HISFC.Models.Pharmacy.ApplyOut applyOut)
        {
            string sendType = string.Empty;
            switch (applyOut.SendType)
            {
                case 1:
                    sendType = "集中";
                    break;
                case 2:
                    sendType = "临时";
                    break;
                case 4:
                    sendType = "紧急";
                    break;
            }
            return sendType;
        }

        /// <summary>
        /// 数据显示
        /// </summary>
        /// <param name="alData">出库申请applyout，而且是按照药品编码汇总了的</param>
        /// <param name="drugBillClass"></param>
        private void ShowBillTotData(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept)
        {

            this.SuspendLayout();
            string applyDeptName = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugBillClass.ApplyDept.ID);

            string sendType = this.GetSendType(alData[0] as FS.HISFC.Models.Pharmacy.ApplyOut);

            this.nlbTitle.Text = applyDeptName + drugBillClass.Name + "(汇总)" + "(" + sendType + ")";

            this.nlbRowCount.Text = "记录数：" + alData.Count.ToString();
            this.nlbBillNO.Text = "单据号：" + drugBillClass.DrugBillNO;
            this.nlbStockDept.Text = "发药科室：" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(stockDept.ID);
            this.nlbNurseCell.Text = "病区：" + applyDeptName;
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            decimal totCost = 0m;
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alData)
            {
                if (FS.FrameWork.Function.NConvert.ToInt32(drugBillClass.ApplyState) != 0)
                {
                    this.nlbReprint.Visible = false;
                    if (!this.nlbTitle.Text.Contains("补打"))
                    {
                        this.nlbTitle.Text = this.nlbReprint.Text + this.nlbTitle.Text;
                    }
                }
                else
                {
                    this.nlbReprint.Visible = false;
                }
                info.PlaceNO = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemPlaceNo(info.StockDept.ID,info.Item.ID);
                info.User01 = FS.FrameWork.Function.NConvert.ToInt32(!SOC.HISFC.BizProcess.Cache.Pharmacy.isValueableItem(info.StockDept.ID, info.Item.ID)).ToString();
            }

            if (drugBillClass.ID == "T")
            {
                alData.Sort(new CompareApplyOutSpecs());
            }
            else
            { 
              alData.Sort(new CompareApplyOut());
            }
          
            int iRow = 0;
            DateTime dtFirstPrintTime = DateTime.MinValue;
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alData)
            {
                string valueable = string.Empty;
                if(!FS.FrameWork.Function.NConvert.ToBoolean(info.User01))
                {
                    valueable = "●";
                }
                dtFirstPrintTime = info.Operation.ExamOper.OperTime;
                #region 数据赋值
                this.neuSpread1_Sheet1.Rows.Add(iRow, 1);
                this.neuSpread1.SetCellValue(0,iRow,"序号",(iRow + 1).ToString());
                this.neuSpread1.SetCellValue(0, iRow, "自定义码", SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemCustomNO(info.Item.ID));
                this.neuSpread1.SetCellValue(0, iRow, "名称", valueable + info.Item.Name);
                this.neuSpread1.SetCellValue(0, iRow, "规格", "  " + info.Item.Specs);
                this.neuSpread1.SetCellValue(0, iRow, "货位号", info.PlaceNO);
              
                this.neuSpread1.SetCellValue(0, iRow, "金额", (info.Item.PriceCollection.RetailPrice * (info.Operation.ApplyQty / info.Item.PackQty)).ToString("F2"));

                this.neuSpread1_Sheet1.Cells[iRow, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.neuSpread1_Sheet1.Cells[iRow, 7].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

                totCost += (info.Item.PriceCollection.RetailPrice * (info.Operation.ApplyQty / info.Item.PackQty));

                decimal applyQty = info.Operation.ApplyQty;
                string unit = info.Item.MinUnit;
                decimal price = info.Item.PriceCollection.RetailPrice;


                int outMinQty;
                int outPackQty = System.Math.DivRem((int)(info.Operation.ApplyQty * info.Days), (int)info.Item.PackQty, out outMinQty);
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

                this.neuSpread1.SetCellValue(0, iRow, "数量", applyQty.ToString("F4").TrimEnd('0').TrimEnd('.') + unit);
                //this.neuSpread1.SetCellValue(0, iRow, "单位", unit);
                this.neuSpread1.SetCellValue(0, iRow, "单价", price.ToString("F4").TrimEnd('0').TrimEnd('.'));

                //打印时间，首次打印也是在保存后打印的，info.State不会等于0，而drugBillClass.ApplyState在调用该控件前处理过
                        

                //if (drugBillClass.ApplyState != "0")
                //{
                //    this.nlbFirstPrintTime.Text = "首次打印：" + info.Operation.ExamOper.OperTime.ToString();
                //    this.nlbPrintTime.Text = "打印时间：" + DateTime.Now;
                //}
                //else
                //{
                //    this.nlbPrintTime.Text = "打印时间：" + info.Operation.ExamOper.OperTime.ToString();
                //    this.nlbFirstPrintTime.Text = "";
                //}

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
                            this.neuSpread1_Sheet1.AddRows(index, 2);
                            //打印单底部文字
                            this.neuSpread1_Sheet1.Cells[index, 0].ColumnSpan = 9;
                            string sumTot = string.Empty;
                            sumTot = totCost.ToString("F2");
                            this.neuSpread1_Sheet1.Cells[index, 0].Text = "   执行配药师：                            核对护士：                              合计：  " + sumTot;
                            this.neuSpread1_Sheet1.Cells[index, 0].Font = new Font("宋体", 10f);
                            this.neuSpread1_Sheet1.Cells[index, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                           
                            this.neuSpread1_Sheet1.Cells[index + 1, 0].ColumnSpan = 9;
                            this.neuSpread1_Sheet1.Cells[index + 1, 0].Text = "配药时间：" + dtFirstPrintTime.ToString() + "                " + "打印时间：" + DateTime.Now;
                            this.neuSpread1_Sheet1.Cells[index + 1, 0].Font = new Font("宋体", 10f);
                            this.neuSpread1_Sheet1.Cells[index + 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                           
                            //标记页码，补打选择页码时用
                            this.neuSpread1_Sheet1.Rows[index].Tag = pageNO;
                            continue;
                        }
                        //到分页处添加一行
                        //this.neuSpread1_Sheet1.AddRows(pageNO * pageRowNum, 1);
                        //this.neuSpread1_Sheet1.Cells[pageNO * pageRowNum, 0].ColumnSpan = this.neuSpread1_Sheet1.Columns.Count;
                        //页码位置在价格处
                        //if (this.neuSpread1_Sheet1.Columns.Count - 1 > 6)
                        //{
                        //    this.neuSpread1_Sheet1.Cells[index, 6].ColumnSpan = 2;
                        //}
                        //this.neuSpread1_Sheet1.Cells[pageNO * pageRowNum, 6].Text = "页：" + pageNO.ToString() + "/" + totPageNO.ToString();

                        //标记页码，补打选择页码时用
                        //this.neuSpread1_Sheet1.Rows[pageNO * pageRowNum].Tag = pageNO;
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
            int height = 0;
            FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
            string dept = ((FS.HISFC.Models.Base.Employee)pageSizeMgr.Operator).Dept.ID;
            FS.HISFC.Models.Base.PageSize paperSize = null; //= pageSizeMgr.GetPageSize("InPatientDrugBillT", "ALL");

            //自适应纸张，注意打印机驱动不可以是正确的
            if (paperSize == null || paperSize.Height > 5000)
            {
                paperSize = new FS.HISFC.Models.Base.PageSize();
                paperSize.Name = DateTime.Now.ToString();
                try
                {
                    int width = 870;

                    int curHeight = 0;

                    int addHeight = this.validRowNum * (int)this.neuSpread1.ActiveSheet.Rows[0].Height;

                    int additionAddHeight = 120;

                    paperSize.Width = width;
                    paperSize.Height = (addHeight + curHeight + additionAddHeight);

                    //this.Height = paperSize.Height;
                    int ifMore = paperSize.Height % 550;
                    if (ifMore == 0)
                    {
                        height = paperSize.Height;
                    }
                    else
                    {
                        int pageNum = paperSize.Height / 550;
                        height = (pageNum + 1) * 550;
                    }
                    paperSize.Height = height;
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

        ///// <summary>
        ///// 打印
        ///// </summary>
        //private void PrintPage()
        //{
        //    FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

        //    this.Dock = DockStyle.None;

        //    FS.HISFC.Models.Base.PageSize paperSize = this.GetPaperSize();
        //    print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

        //    print.SetPageSize(paperSize);

        //    //管理员可以预览，方便查看问题
        //    if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
        //    {
        //        print.PrintPreview(10, 10, this);
        //    }
        //    else
        //    {
        //        print.PrintPage(10, 10, this);
        //    }

        //    this.Dock = DockStyle.Fill;
        //}

        #endregion

        #region 汇总单的特殊方法

        /// <summary>
        /// 设置FarPoint的样式
        /// </summary>
        private void SetSheetStyle()
        {

            FarPoint.Win.ComplexBorder bevelBorder1 = new FarPoint.Win.ComplexBorder(FarPoint.Win.ComplexBorderSide.Empty, FarPoint.Win.ComplexBorderSide.Empty, FarPoint.Win.ComplexBorderSide.Empty, new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.HairLine));
            FarPoint.Win.BevelBorder lineBorder1 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Lowered, System.Drawing.Color.Black, System.Drawing.Color.Black, 1, false, false, false, true);
            FarPoint.Win.BevelBorder lineBorder2 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Lowered, System.Drawing.Color.Black, System.Drawing.Color.Black, 1, false, false, false, true);

            for (int i = 0, j = this.neuSpread1_Sheet1.Rows.Count - 1; i < j; i++)
            {
                for (int k = 0; k < 8; k++)
                {

                    this.neuSpread1_Sheet1.Cells.Get(i, k).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                }

                FarPoint.Win.LineBorder lineBorder3 = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 2, false, true, false, false);
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 2, 0).Border = lineBorder3;
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            }
            for (int index = 0; index < this.neuSpread1_Sheet1.Rows.Count - 2; index++)
            {
                this.neuSpread1_Sheet1.Rows.Get(index).Border = bevelBorder1;
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
            this.ShowBillData(alData, drugBillClass, stockDept);
            this.validRowNum = this.neuSpread1_Sheet1.RowCount;
            this.PrintPage();
        }

        #endregion

        #region 排序类
        /// <summary>
        /// 排序类
        /// </summary>
        private class CompareApplyOut : IComparer
        {
            public int Compare(object x, object y)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut o1 = x as FS.HISFC.Models.Pharmacy.ApplyOut;
                FS.HISFC.Models.Pharmacy.ApplyOut o2 = y as FS.HISFC.Models.Pharmacy.ApplyOut;

                //string oX = o1.PlaceNO;          //货位号
                //if (oX == null)
                //{
                //    oX = "";
                //}
                //string oY = o2.PlaceNO;          //货位号
                //if (oY == null)
                //{
                //    oY = "";
                //}
                string oX = o1.User01 + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(o1.Item.ID).NameCollection.RegularSpell.UserCode.ToString();
                if (oX == null)
                {
                    oX = "";
                }
                string oY = o2.User01 + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(o2.Item.ID).NameCollection.RegularSpell.UserCode.ToString();
                if (oY == null)
                {
                    oY = "";
                }

                return string.Compare(oX.ToString(), oY.ToString());
            }

        }

        /// <summary>
        /// 按规格排序
        /// </summary>
        private class CompareApplyOutSpecs : IComparer
        {

            #region IComparer 成员

            public int Compare(object x, object y)
            {
                 FS.HISFC.Models.Pharmacy.ApplyOut o1 = x as FS.HISFC.Models.Pharmacy.ApplyOut;
                FS.HISFC.Models.Pharmacy.ApplyOut o2 = y as FS.HISFC.Models.Pharmacy.ApplyOut;

                //string oX = o1.PlaceNO;          //货位号
                //if (oX == null)
                //{
                //    oX = "";
                //}
                //string oY = o2.PlaceNO;          //货位号
                //if (oY == null)
                //{
                //    oY = "";
                //}
                decimal oX = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(o1.Item.ID).BaseDose;

                decimal oY = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(o2.Item.ID).BaseDose;

                return oX.CompareTo(oY);
            }

            #endregion
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
        public void Print()
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

        #region 打印函数
        void PrintDocument_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            curPageNO = 1;
        }
        private void PrintDocumentPrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //跳过选择打印范围外的数据
            while (this.curPageNO < this.socPrintPageSelectDialog.FromPageNO && this.curPageNO < this.maxPageNO)
            {
                curPageNO++;
            }

            if (this.curPageNO > this.maxPageNO || this.curPageNO > socPrintPageSelectDialog.ToPageNO)
            {
                this.curPageNO = 1;
                //this.maxPageNO = 1;
                e.HasMorePages = false;
                return;
            }

            Graphics graphics = e.Graphics;

            #region 标题绘制
            int mainTitleLocalX = this.DrawingMargins.Left + this.nlbTitle.Location.X;
            int mainTitleLoaclY = this.DrawingMargins.Top + this.nlbTitle.Location.Y;
            graphics.DrawString(this.nlbTitle.Text, this.nlbTitle.Font, new SolidBrush(this.nlbTitle.ForeColor), mainTitleLocalX, mainTitleLoaclY);

            int additionTitleLocalX = this.DrawingMargins.Left + this.nlbNurseCell.Location.X;
            int additionTitleLocalY = this.DrawingMargins.Top + this.nlbNurseCell.Location.Y;

            graphics.DrawString(this.nlbNurseCell.Text, this.nlbNurseCell.Font, new SolidBrush(this.nlbNurseCell.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.nlbBillNO.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.nlbBillNO.Location.Y;

            graphics.DrawString(this.nlbBillNO.Text, this.nlbBillNO.Font, new SolidBrush(this.nlbBillNO.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.nlbStockDept.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.nlbStockDept.Location.Y;

            graphics.DrawString(this.nlbStockDept.Text, this.nlbStockDept.Font, new SolidBrush(this.nlbStockDept.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.nlbRowCount.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.nlbRowCount.Location.Y;

            graphics.DrawString(this.nlbRowCount.Text, this.nlbRowCount.Font, new SolidBrush(this.nlbRowCount.ForeColor), additionTitleLocalX, additionTitleLocalY);

            #endregion

            #region Farpoint绘制
            int drawingWidth = 870 - this.DrawingMargins.Left - this.DrawingMargins.Right;
            int drawingHeight = 550 - this.DrawingMargins.Top - this.DrawingMargins.Bottom - this.neuPanel1.Height;
            this.neuSpread1.OwnerPrintDraw(graphics, new Rectangle(this.DrawingMargins.Left, this.DrawingMargins.Top + this.neuPanel1.Height, drawingWidth, drawingHeight), 0, this.curPageNO);

            #endregion

            #region 页码绘制

            additionTitleLocalX = this.DrawingMargins.Left + this.nlbPageNo.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.nlbPageNo.Location.Y;

            graphics.DrawString("页码：" + this.curPageNO.ToString() + "/" + this.maxPageNO.ToString(), this.nlbPageNo.Font, new SolidBrush(this.nlbPageNo.ForeColor), additionTitleLocalX, additionTitleLocalY);

            #endregion

            #region 分页
            if (this.curPageNO < this.socPrintPageSelectDialog.ToPageNO && this.curPageNO < maxPageNO)
            {
                e.HasMorePages = true;
                curPageNO++;
            }
            else
            {
                curPageNO = 1;
                //maxPageNO = 1;
                e.HasMorePages = false;
            }
            #endregion
        }

        private void SetPaperSize(System.Drawing.Printing.PaperSize paperSize)
        {
            if (paperSize == null)
            {
                paperSize = new System.Drawing.Printing.PaperSize("InPatientDrugBillD", 870, 550);
            }

            this.PrintDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize(paperSize.PaperName, paperSize.Width, paperSize.Height);
            this.PrintDocument.PrinterSettings.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize(paperSize.PaperName, paperSize.Width, paperSize.Height);
        }

        private void myPrintView()
        {
            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            printPreviewDialog.Document = this.PrintDocument;
            try
            {
                ((Form)printPreviewDialog).WindowState = FormWindowState.Maximized;
            }
            catch { }
            try
            {
                printPreviewDialog.ShowDialog();
                printPreviewDialog.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("打印机报错！" + ex.Message);
            }
        }

        /// <summary>
        /// 打印页码选择
        /// </summary>
        private bool ChoosePrintPageNO(Graphics graphics)
        {

            int drawingWidth = 870 - this.DrawingMargins.Left - this.DrawingMargins.Right;
            int drawingHeight = 550 - this.DrawingMargins.Top - this.DrawingMargins.Bottom - this.neuPanel1.Height;

            FarPoint.Win.Spread.PrintInfo printInfo = new FarPoint.Win.Spread.PrintInfo();
            printInfo.ShowBorder = false;
            printInfo.PrintType = FarPoint.Win.Spread.PrintType.All;
            printInfo.ShowRowHeaders = this.neuSpread1_Sheet1.RowHeader.Visible;
            this.neuSpread1_Sheet1.PrintInfo = printInfo;
            this.maxPageNO = neuSpread1.GetOwnerPrintPageCount(graphics, new Rectangle(this.DrawingMargins.Left, this.DrawingMargins.Top + this.neuPanel1.Height, drawingWidth, drawingHeight), 0);

            socPrintPageSelectDialog.MaxPageNO = this.maxPageNO;
            if (this.maxPageNO > 1)
            {
                socPrintPageSelectDialog.StartPosition = FormStartPosition.CenterScreen;
                socPrintPageSelectDialog.ShowDialog();
                if (socPrintPageSelectDialog.ToPageNO == 0)
                {
                    return false;
                }
            }

            return true;
        }

        protected void PrintView()
        {
            this.SetPaperSize(null);
            this.myPrintView();
        }

        /// <summary>
        /// 打印
        /// </summary>
        private void PrintPage()
        {
            System.Drawing.Printing.PaperSize paperSize = null;
            this.SetPaperSize(paperSize);
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                if (this.ChoosePrintPageNO(Graphics.FromHwnd(this.Handle)))
                {
                    this.PrintView();
                }
            }
            else
            {
                if (this.ChoosePrintPageNO(Graphics.FromHwnd(this.Handle)))
                {
                    this.SetPaperSize(paperSize);
                    this.PrintDocument.Print();
                }
            }

        }
        #endregion

    }

    public class SortByValueAble : IComparer
    {

        #region IComparer 成员

        public int Compare(object x, object y)
        {
            if ((x is FS.HISFC.Models.Pharmacy.ApplyOut) && (y is FS.HISFC.Models.Pharmacy.ApplyOut))
            {
                return (x as FS.HISFC.Models.Pharmacy.ApplyOut).User01.CompareTo((y as FS.HISFC.Models.Pharmacy.ApplyOut).User01);
            }
            return 1;
        }

        #endregion
    }
}
