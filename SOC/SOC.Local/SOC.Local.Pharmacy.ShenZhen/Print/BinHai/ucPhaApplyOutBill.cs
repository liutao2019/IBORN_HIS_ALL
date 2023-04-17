using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.WinForms.Controls;
using System.Collections;

namespace FS.SOC.Local.Pharmacy.ShenZhen.Print.BinHai
{
    public partial class ucPhaApplyOutBill : ucBaseControl, Base.IPharmacyBillPrint
    {
        public ucPhaApplyOutBill()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 所有待打印数据
        /// </summary>
        private ArrayList alPrintData = new ArrayList();

        /// <summary>
        /// 单据的总金额
        /// </summary>
        private Hashtable hsTotCost = new Hashtable();

        /// <summary>
        /// 
        /// </summary>
        private class TotCost
        {
            public decimal PurchaseCost;
            public decimal RetailCost;
            public decimal WholesaleCost;
        }

        #endregion


        #region 内部申请单打印
        /// <summary>
        /// 打印函数
        /// </summary>
        /// <param name="al">打印数组</param>
        /// <param name="i">第几页</param>
        /// <param name="count">总页数</param>
        /// <param name="title">标题</param>
        private void SetPrintData(ArrayList al, int inow, int icount, string title)
        {
            if (al.Count <= 0)
            {
                MessageBox.Show("没有打印的数据!");
                return;
            }
            if (icount <= 0)
            {
                icount = 1;
            }
            FS.HISFC.Models.Pharmacy.ApplyOut info = (FS.HISFC.Models.Pharmacy.ApplyOut)al[0];

            #region label赋值
            if (string.IsNullOrEmpty(title))
            {
                this.lbTitle.Text = string.Format(this.lbTitle.Text, FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.ApplyDept.ID));
            }
            else
            {
                if (title.IndexOf("[库存科室]") != -1)
                {
                    this.lbTitle.Text = title.Replace("[库存科室]", FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.ApplyDept.ID));
                }
                else
                {
                    this.lbTitle.Text = title;
                }
            }

            string company = "";
            //if (info.SourceCompanyType == "1")
            //{
            company = FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.StockDept.ID);
            //}
            //else
            //{
            //    company = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetCompanyName(info.Company.ID);
            //}
            this.lbCompany.Text = "送货部门:" + info.StockDept.ID + " " + company;

            this.lbBillNO.Text = "申请单号:" + info.BillNO;
            this.lblInputDate.Text = "申请日期:" + info.Operation.ApplyOper.OperTime.ToShortDateString();
            this.lbMadeBillOper.Text = "制表人:" + FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(info.Operation.ExamOper.ID);
            this.lblPage.Text = "页:" + inow.ToString() + "/" + icount.ToString();
            //this.lbPlanOper.Text = "采购员:";// +BillPrintFun.GetStockPlanPersonName(info.StockDept.ID);
            //this.lbStockOper.Text = "仓管员:";// +BillPrintFun.GetStockManagerName(info.StockDept.ID);//BillPrintFun.GetEmplName(info.Operation.ExamOper.ID);

            #endregion

            #region farpoint赋值
            //本页总计金额
            TotCost curPageCost = new TotCost();
            //本页药品金额
            TotCost curPagePCZCost = new TotCost();
            //本页卫生材料金额
            TotCost curPageBCost = new TotCost();
            //本页原材料金额
            TotCost curPageMCost = new TotCost();

            this.socFpSpread1_Sheet1.RowCount = 0;

            FarPoint.Win.Spread.CellType.NumberCellType n = new FarPoint.Win.Spread.CellType.NumberCellType();
            n.DecimalPlaces = 4;
            this.socFpSpread1.SetColumnCellType(0, "买入单价", n);
            this.socFpSpread1.SetColumnCellType(0, "买入金额", n);
            this.socFpSpread1.SetColumnCellType(0, "零售单价", n);

            for (int index = 0; index < al.Count; index++)
            {
                this.socFpSpread1_Sheet1.AddRows(index, 1);
                FS.HISFC.Models.Pharmacy.ApplyOut applyOut = al[index] as FS.HISFC.Models.Pharmacy.ApplyOut;
                this.socFpSpread1.SetCellValue(0, index, "编码", FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(applyOut.Item.ID).GBCode); //药品自定义码
                this.socFpSpread1.SetCellValue(0, index, "名称", applyOut.Item.Name);//药品名称
                this.socFpSpread1.SetCellValue(0, index, "规格", applyOut.Item.Specs);//规格	

                if (applyOut.Item.PackQty == 0)
                {
                    applyOut.Item.PackQty = 1;
                }
                decimal count = 0;
                count = applyOut.Operation.ApplyQty;

                if (applyOut.ShowState == "0")
                {
                    this.socFpSpread1.SetCellValue(0, index, "单位", applyOut.Item.MinUnit);//单位
                    this.socFpSpread1.SetCellValue(0, index, "数量", count);//数量				
                    this.socFpSpread1.SetCellValue(0, index, "买入单价", (applyOut.Item.PriceCollection.PurchasePrice / applyOut.Item.PackQty).ToString("F4"));
                    this.socFpSpread1.SetCellValue(0, index, "零售单价", (applyOut.Item.PriceCollection.RetailPrice / applyOut.Item.PackQty).ToString("F4"));
                }
                else
                {
                    this.socFpSpread1.SetCellValue(0, index, "单位", applyOut.Item.PackUnit);//单位
                    this.socFpSpread1.SetCellValue(0, index, "数量", (count / applyOut.Item.PackQty).ToString("F2").TrimEnd('0').TrimEnd('.'));//数量				
                    this.socFpSpread1.SetCellValue(0, index, "购入单价", applyOut.Item.PriceCollection.PurchasePrice);
                    this.socFpSpread1.SetCellValue(0, index, "零售单价", applyOut.Item.PriceCollection.RetailPrice);
                }
                FS.HISFC.Models.Pharmacy.Item item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(applyOut.Item.ID);
                this.socFpSpread1.SetCellValue(0, index, "厂家", FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetProducerName(item.Product.Producer.ID));
                //this.socFpSpread1.SetCellValue(0, index, "批号", applyOut.BatchNO);

                if (applyOut.ShowState == "0")
                {
                    this.socFpSpread1.SetCellValue(0, index, "购入金额", applyOut.Item.PriceCollection.PurchasePrice * count);

                    switch (applyOut.Item.Type.ID)
                    {
                        case "B":
                            curPageBCost.WholesaleCost += applyOut.Item.PriceCollection.WholeSalePrice * count;
                            curPageBCost.PurchaseCost += applyOut.Item.PriceCollection.PurchasePrice * count;
                            curPageBCost.RetailCost += applyOut.Item.PriceCollection.RetailPrice * count;
                            break;
                        case "M":
                            curPageMCost.WholesaleCost += applyOut.Item.PriceCollection.WholeSalePrice * count;
                            curPageMCost.PurchaseCost += applyOut.Item.PriceCollection.PurchasePrice * count;
                            curPageMCost.RetailCost += applyOut.Item.PriceCollection.RetailPrice * count;
                            break;
                        case "C":
                        case "P":
                        case "Z":
                        default:
                            curPagePCZCost.WholesaleCost += applyOut.Item.PriceCollection.WholeSalePrice * count;
                            curPagePCZCost.PurchaseCost += applyOut.Item.PriceCollection.PurchasePrice * count;
                            curPagePCZCost.RetailCost += applyOut.Item.PriceCollection.RetailPrice * count;
                            break;
                    }

                    curPageCost.WholesaleCost += applyOut.Item.PriceCollection.WholeSalePrice * count;
                    curPageCost.PurchaseCost += applyOut.Item.PriceCollection.PurchasePrice * count;
                    curPageCost.RetailCost += applyOut.Item.PriceCollection.RetailPrice * count;
                }
                else
                {
                    this.socFpSpread1.SetCellValue(0, index, "购入金额", applyOut.Item.PriceCollection.PurchasePrice * count / applyOut.Item.PackQty);

                    switch (applyOut.Item.Type.ID)
                    {
                        case "B":
                            curPageBCost.WholesaleCost += applyOut.Item.PriceCollection.WholeSalePrice * count / applyOut.Item.PackQty;
                            curPageBCost.PurchaseCost += applyOut.Item.PriceCollection.PurchasePrice * count / applyOut.Item.PackQty;
                            curPageBCost.RetailCost += applyOut.Item.PriceCollection.RetailPrice * count / applyOut.Item.PackQty;
                            break;
                        case "M":
                            curPageMCost.WholesaleCost += applyOut.Item.PriceCollection.WholeSalePrice * count / applyOut.Item.PackQty;
                            curPageMCost.PurchaseCost += applyOut.Item.PriceCollection.PurchasePrice * count / applyOut.Item.PackQty;
                            curPageMCost.RetailCost += applyOut.Item.PriceCollection.RetailPrice * count / applyOut.Item.PackQty;
                            break;
                        case "C":
                        case "P":
                        case "Z":
                        default:
                            curPagePCZCost.WholesaleCost += applyOut.Item.PriceCollection.WholeSalePrice * count / applyOut.Item.PackQty;
                            curPagePCZCost.PurchaseCost += applyOut.Item.PriceCollection.PurchasePrice * count / applyOut.Item.PackQty;
                            curPagePCZCost.RetailCost += applyOut.Item.PriceCollection.RetailPrice * count / applyOut.Item.PackQty;
                            break;
                    }

                    curPageCost.WholesaleCost += applyOut.Item.PriceCollection.WholeSalePrice * count / applyOut.Item.PackQty;
                    curPageCost.PurchaseCost += applyOut.Item.PriceCollection.PurchasePrice * count / applyOut.Item.PackQty;
                    curPageCost.RetailCost += applyOut.Item.PriceCollection.RetailPrice * count / applyOut.Item.PackQty;
                }

            }
            //当前页数据
            this.lbCurPageRetailCost.Text = "本页零售金额:" + curPageCost.RetailCost.ToString("F4");
            this.lbCurPagePurchaseCost.Text = "本页购入金额:" + curPageCost.PurchaseCost.ToString("F4");
            this.lbCurPageSubCost.Text = "本页零购差:" + (curPageCost.RetailCost - curPageCost.PurchaseCost).ToString("F4");

            //药品数据
            this.lbDrugRetailCost.Text = "药品零额:" + curPagePCZCost.RetailCost.ToString("F2");
            this.lbDrugPurchaseCost.Text = "药品购额:" + curPagePCZCost.PurchaseCost.ToString("F2");
            this.lbDrugSubCost.Text = "药品零购差:" + (curPagePCZCost.RetailCost - curPagePCZCost.PurchaseCost).ToString("F2");

            //原材料、卫生材料用购入价
            //this.lbBCost.Text = "卫生材料:" + curPageBCost.PurchaseCost.ToString("F4");
            //this.lbMCost.Text = "原材料:" + curPageBCost.PurchaseCost.ToString("F4");

            //总数据
            this.lbTotPurchaseCost.Text = "总购入金额：" + ((TotCost)hsTotCost[info.BillNO]).PurchaseCost.ToString("F2");
            this.lbTotRetailCost.Text = "总零售金额：" + ((TotCost)hsTotCost[info.BillNO]).RetailCost.ToString("F2");
            this.lbTotSubCost.Text = "总零购差额：" + (((TotCost)hsTotCost[info.BillNO]).RetailCost - ((TotCost)hsTotCost[info.BillNO]).PurchaseCost).ToString("F2");

            #endregion

            this.ResetTitleLocation();

        }

        /// <summary>
        /// 重新设置标题位置
        /// </summary>
        private void ResetTitleLocation()
        {
            this.neuPanel4.Controls.Remove(this.lbTitle);
            int with = 0;
            for (int col = 0; col < this.socFpSpread1_Sheet1.ColumnCount; col++)
            {
                if (this.socFpSpread1_Sheet1.Columns[col].Visible)
                {
                    with += (int)this.socFpSpread1_Sheet1.Columns[col].Width;
                }
            }
            if (with > this.neuPanel4.Width)
            {
                with = this.neuPanel4.Width;
            }
            this.lbTitle.Location = new Point((with - this.lbTitle.Size.Width) / 2, this.lbTitle.Location.Y);
            this.neuPanel4.Controls.Add(this.lbTitle);

        }
        #endregion

        #region IPharmacyBill 成员

        private Base.PrintBill printBill = new FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill();

        /// <summary>
        /// IBillPrint成员Print
        /// </summary>
        /// <returns></returns>
        public int Print()
        {
            #region 打印信息设置

            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.IsDataAutoExtend = false;
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;
            p.IsHaveGrid = true;
            p.SetPageSize(this.printBill.PageSize);
            #endregion

            #region 分页打印
            int height = this.neuPanel5.Height;
            int ucHeight = this.Height;
            float rowHeight = this.socFpSpread1_Sheet1.Rows[0].Height;

            //补打的时候可能有多张单据，分开
            System.Collections.Hashtable hs = new Hashtable();
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alPrintData)
            {
                if (hs.Contains(applyOut.BillNO))
                {

                    ArrayList al = (ArrayList)hs[applyOut.BillNO];
                    al.Add(applyOut);

                    TotCost tc = (TotCost)hsTotCost[applyOut.BillNO];
                    if (applyOut.ShowState == "0")
                    {
                        tc.PurchaseCost += applyOut.Item.PriceCollection.PurchasePrice * applyOut.Operation.ApplyQty;
                        tc.RetailCost += applyOut.Item.PriceCollection.RetailPrice * applyOut.Operation.ApplyQty;
                        tc.WholesaleCost += applyOut.Item.PriceCollection.WholeSalePrice * applyOut.Operation.ApplyQty;
                    }
                    else
                    {
                        tc.PurchaseCost += applyOut.Item.PriceCollection.PurchasePrice * applyOut.Operation.ApplyQty / applyOut.Item.PackQty;
                        tc.RetailCost += applyOut.Item.PriceCollection.RetailPrice * applyOut.Operation.ApplyQty / applyOut.Item.PackQty;
                        tc.WholesaleCost += applyOut.Item.PriceCollection.WholeSalePrice * applyOut.Operation.ApplyQty / applyOut.Item.PackQty;
                    }
                }
                else
                {
                    ArrayList al = new ArrayList();
                    al.Add(applyOut);
                    hs.Add(applyOut.BillNO, al);

                    TotCost tc = new TotCost();
                    if (applyOut.ShowState == "0")
                    {
                        tc.PurchaseCost += applyOut.Item.PriceCollection.PurchasePrice * applyOut.Operation.ApplyQty;
                        tc.RetailCost += applyOut.Item.PriceCollection.RetailPrice * applyOut.Operation.ApplyQty;
                        tc.WholesaleCost += applyOut.Item.PriceCollection.WholeSalePrice * applyOut.Operation.ApplyQty;
                    }
                    else
                    {
                        tc.PurchaseCost += applyOut.Item.PriceCollection.PurchasePrice * applyOut.Operation.ApplyQty / applyOut.Item.PackQty;
                        tc.RetailCost += applyOut.Item.PriceCollection.RetailPrice * applyOut.Operation.ApplyQty / applyOut.Item.PackQty;
                        tc.WholesaleCost += applyOut.Item.PriceCollection.WholeSalePrice * applyOut.Operation.ApplyQty / applyOut.Item.PackQty;
                    }
                    hsTotCost.Add(applyOut.BillNO, tc);
                }
            }

            //分单据打印
            foreach (ArrayList alPrint in hs.Values)
            {
                int pageTotNum = alPrint.Count / this.printBill.RowCount;
                if (alPrint.Count != this.printBill.RowCount * pageTotNum)
                {
                    pageTotNum++;
                }

                //分页打印
                for (int pageNow = 0; pageNow < pageTotNum; pageNow++)
                {
                    ArrayList al = new ArrayList();

                    for (int index = pageNow * this.printBill.RowCount; index < alPrint.Count && index < (pageNow + 1) * this.printBill.RowCount; index++)
                    {
                        al.Add(alPrint[index]);
                    }
                    this.SetPrintData(al, pageNow + 1, pageTotNum, printBill.Title);

                    this.neuPanel5.Height += (int)rowHeight * al.Count;
                    this.Height += (int)rowHeight * al.Count;

                    if (this.printBill.IsNeedPreview)
                    {
                        p.PrintPreview(0, 0, this.neuPanel1);
                    }
                    else
                    {
                        p.PrintPage(0, 0, this.neuPanel1);
                    }

                    this.neuPanel5.Height = height;
                    this.Height = ucHeight;
                }
            }
            #endregion

            return 1;
        }

        public int SetPrintData(ArrayList alPrintData, Base.PrintBill printBill)
        {
            if (alPrintData != null && alPrintData.Count > 0)
            {             
                string bill = (alPrintData[0] as FS.HISFC.Models.Pharmacy.ApplyOut).BillNO;
                string dept = (alPrintData[0] as FS.HISFC.Models.Pharmacy.ApplyOut).ApplyDept.ID;
                //FS.SOC.HISFC.BizLogic.Pharmacy.Item itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Item();
                FS.HISFC.BizLogic.Pharmacy.Item itemMgr = new FS.HISFC.BizLogic.Pharmacy.Item();
                ArrayList al = itemMgr.QueryApplyOutInfoByListCode(dept, bill, "1");
                this.alPrintData = al;
            }            
            //this.alPrintData = alPrintData;
            this.printBill = printBill;
            return this.Print();
        }

        public int Preview()
        {
            this.printBill.IsNeedPreview = true;
            return this.Print();
        }

        #endregion
    }
}
