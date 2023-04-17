using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.SOC.Local.Pharmacy.ShenZhen.Print.BinHai;
using FS.FrameWork.WinForms.Controls;

namespace FS.SOC.Local.Pharmacy.ShenZhen.Print.BinHai
{
    /// <summary>
    /// 东莞药品出库单据打印
    /// </summary>
    public partial class ucPhaOutputBill : ucBaseControl, Base.IPharmacyBillPrint
    {
        /// <summary>
        /// 药品入库打印单
        /// </summary>
        public ucPhaOutputBill()
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

        #region 打印主函数


        #region 入库单打印
        /// <summary>
        /// 打印函数
        /// </summary>
        /// <param name="al">打印数组</param>
        /// <param name="i">第几页</param>
        /// <param name="count">总页数</param>
        /// <param name="operCode">标题</param>
        private void SetPrintData(ArrayList al, int inow, int icount, string title)
        {
            if (al.Count <= 0)
            {
                MessageBox.Show("没有打印的数据!");
                return;
            }
            FS.HISFC.Models.Pharmacy.Output info = (FS.HISFC.Models.Pharmacy.Output)al[0];

            #region label赋值
            if (string.IsNullOrEmpty(title))
            {
                this.lblTitle.Text = string.Format(this.lblTitle.Text, FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.StockDept.ID));
            }
            else
            {
                if (title.IndexOf("[库存科室]") != -1)
                {
                    this.lblTitle.Text = title.Replace("[库存科室]", FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.StockDept.ID));
                }
                else
                {
                    this.lblTitle.Text = title;
                }
            }

            this.lblCompany.Text = "领药科室: " + info.TargetDept.ID + " " + FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.TargetDept.ID);
            this.lblBillID.Text = "出库号: " + info.OutListNO;
            this.lblInputDate.Text = "出库日期: " + info.OutDate.ToShortDateString();
            this.lblOper.Text = "制单人:" + FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(info.Operation.ExamOper.ID);
            this.lblPage.Text = "页：" + inow.ToString() + "/" + icount.ToString();
            //this.neuLabel10.Text = "仓管员：";// +BillPrintFun.GetStockManagerName(info.StockDept.ID);// BillPrintFun.GetEmplName(info.Operation.ExamOper.ID);
            //this.neuLabel10.Visible = true;
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
           // this.socFpSpread1.SetColumnCellType(0, "买入金额", n);
            this.socFpSpread1.SetColumnCellType(0, "零售单价", n);

            for (int index = 0; index < al.Count; index++)
            {
                this.socFpSpread1_Sheet1.AddRows(index, 1);
                FS.HISFC.Models.Pharmacy.Output output = al[index] as FS.HISFC.Models.Pharmacy.Output;
                this.socFpSpread1.SetCellValue(0, index, "产品ID", FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(output.Item.ID).GBCode); //药品自定义码
                this.socFpSpread1.SetCellValue(0, index, "名称", output.Item.Name);//药品名称
                this.socFpSpread1.SetCellValue(0, index, "规格", output.Item.Specs);//规格	

                if (output.Item.PackQty == 0)
                {
                    output.Item.PackQty = 1;
                }
                decimal count = 0;
                count = output.Quantity;

                if (output.ShowState == "0")
                {
                    this.socFpSpread1.SetCellValue(0, index, "单位", output.Item.MinUnit);//单位
                    this.socFpSpread1.SetCellValue(0, index, "数量", count);//数量				
                    this.socFpSpread1.SetCellValue(0, index, "买入单价", (output.Item.PriceCollection.PurchasePrice / output.Item.PackQty).ToString("F2"));
                    this.socFpSpread1.SetCellValue(0, index, "零售单价", (output.Item.PriceCollection.RetailPrice / output.Item.PackQty).ToString("F2"));
                }
                else
                {
                    this.socFpSpread1.SetCellValue(0, index, "单位", output.Item.PackUnit);//单位
                    this.socFpSpread1.SetCellValue(0, index, "数量", (count / output.Item.PackQty).ToString("F2").TrimEnd('0').TrimEnd('.'));//数量				
                    this.socFpSpread1.SetCellValue(0, index, "购入单价", output.Item.PriceCollection.PurchasePrice);
                    this.socFpSpread1.SetCellValue(0, index, "零售单价", output.Item.PriceCollection.RetailPrice);
                }
                this.socFpSpread1.SetCellValue(0, index, "有效期", output.ValidTime.ToShortDateString());//有效期
                this.socFpSpread1.SetCellValue(0, index, "厂家", FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetProducerName(output.Producer.ID));
                this.socFpSpread1.SetCellValue(0, index, "批号", output.BatchNO);
                this.socFpSpread1.SetCellValue(0, index, "购入金额", output.PurchaseCost);
                this.socFpSpread1.SetCellValue(0, index, "零售金额", output.RetailCost);


                switch (output.Item.Type.ID)
                {
                    case "B":
                        curPageBCost.WholesaleCost += output.WholeSaleCost;
                        curPageBCost.PurchaseCost += output.PurchaseCost;
                        curPageBCost.RetailCost += output.RetailCost;
                        break;
                    case "M":
                        curPageMCost.WholesaleCost += output.WholeSaleCost;
                        curPageMCost.PurchaseCost += output.PurchaseCost;
                        curPageMCost.RetailCost += output.RetailCost;
                        break;
                    case "C":
                    case "P":
                    case "Z":
                    default:
                        curPagePCZCost.WholesaleCost += output.WholeSaleCost;
                        curPagePCZCost.PurchaseCost += output.PurchaseCost;
                        curPagePCZCost.RetailCost += output.RetailCost;
                        break;
                }

                curPageCost.WholesaleCost += output.WholeSaleCost;
                curPageCost.PurchaseCost += output.PurchaseCost;
                curPageCost.RetailCost += output.RetailCost;

            }

            //当前页数据
            //this.lblCurRet.Text = "本页零售金额：" + sumRetail.ToString("F4");
            //this.lblCurPur.Text = "本页购入金额：" + sumPurchase.ToString("F4");
            //this.lblCurDif.Text = "本页购零差：" + sumDif.ToString("F4");

            //lblBDiff.Text = "卫生材料差：" + (sumBWholeCost - sumBPurCost).ToString("F4");
            //lblBPurCost.Text = "卫生材料购额：" + sumBPurCost.ToString("F4");
            //lblBWholeCost.Text = "卫生材料零额："+sumBWholeCost.ToString("F4");

            //lblDrugDiff.Text = "药品差额："+(sumDrugWholeCost-sumDrugPurCost).ToString("F4");
            //lblDrugPurCost.Text = "药品购额：" + sumDrugPurCost.ToString("F4");
            //lblDrugWholeCost.Text = "药品零额："+sumDrugWholeCost.ToString("F4");
           
            ////总数据
            //this.lblTotPurCost.Text = ((TotCost)hsTotCost[info.OutListNO]).purchaseCost.ToString("F4");
            //this.lblTotRetailCost.Text = ((TotCost)hsTotCost[info.OutListNO]).retailCost.ToString("F4");
            //this.lblTotDif.Text = (((TotCost)hsTotCost[info.OutListNO]).retailCost - ((TotCost)hsTotCost[info.OutListNO]).purchaseCost).ToString("F4");

            //当前页数据
            this.lblCurRet.Text = "零售金额:" + curPageCost.RetailCost.ToString("F2");
            this.lblCurPur.Text = "本页合计  购入金额:" + curPageCost.PurchaseCost.ToString("F2");
            this.lblCurDif.Text = "零购差:" + (curPageCost.RetailCost - curPageCost.PurchaseCost).ToString("F2");

            //药品数据
            //this.lblDrugWholeCost.Text = "药品零额:" + curPagePCZCost.RetailCost.ToString("F4");
            //this.lblDrugPurCost.Text = "药品购额:" + curPagePCZCost.PurchaseCost.ToString("F4");
            //this.lblDrugDiff.Text = "药品零购差:" + (curPagePCZCost.RetailCost - curPagePCZCost.PurchaseCost).ToString("F4");

            //原材料、卫生材料用购入价
            //this.lblBDiff.Text = "卫生材料差：" + (curPageBCost.RetailCost-curPageBCost.PurchaseCost).ToString("F4");
            //this.lblBPurCost.Text = "卫生材料购额：" + curPageBCost.PurchaseCost.ToString("F4");
            //this.lblBWholeCost.Text = "卫生材料零额：" + curPageBCost.RetailCost.ToString("F4");
            
            //总数据
            this.lblTotPurCost.Text = ((TotCost)hsTotCost[info.OutListNO]).PurchaseCost.ToString("F2");
            this.lblTotRetailCost.Text = ((TotCost)hsTotCost[info.OutListNO]).RetailCost.ToString("F2");
            this.lblTotDif.Text = (((TotCost)hsTotCost[info.OutListNO]).RetailCost - ((TotCost)hsTotCost[info.OutListNO]).PurchaseCost).ToString("F2");

            FS.FrameWork.Management.DataBaseManger obj = new FS.FrameWork.Management.DataBaseManger();
            DateTime date = obj.GetDateTimeFromSysDateTime();
            neuLabel5.Text = "制单日期：" + date.ToShortDateString();
            #endregion

            this.resetTitleLocation();

        }

        /// <summary>
        /// 重新设置标题位置
        /// </summary>
        private void resetTitleLocation()
        {
            this.neuPanel3.Controls.Remove(this.lblTitle);
            int with = 0;
            for (int col = 0; col < this.socFpSpread1_Sheet1.ColumnCount; col++)
            {
                if (this.socFpSpread1_Sheet1.Columns[col].Visible)
                {
                    with += (int)this.socFpSpread1_Sheet1.Columns[col].Width;
                }
            }
            if (with > this.neuPanel3.Width)
            {
                with = this.neuPanel3.Width;
            }
            this.lblTitle.Location = new Point((with - this.lblTitle.Size.Width) / 2, this.lblTitle.Location.Y);
            this.neuPanel3.Controls.Add(this.lblTitle);

        }

        #endregion

        #endregion

        #region IBillPrint 成员

        #region IPharmacyBill 成员

        private Base.PrintBill printBill = new Base.PrintBill();

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
            int height = this.neuPanel4.Height;
            int ucHeight = this.Height;
            float rowHeight = this.socFpSpread1_Sheet1.Rows[0].Height;
            this.socFpSpread1_Sheet1.Columns[0].CellType = new FarPoint.Win.Spread.CellType.TextCellType();

            //补打的时候可能有多张单据，分开
            System.Collections.Hashtable hs = new Hashtable();
            foreach (FS.HISFC.Models.Pharmacy.Output output in alPrintData)
            {
                if (hs.Contains(output.OutListNO))
                {

                    ArrayList al = (ArrayList)hs[output.OutListNO];
                    al.Add(output);

                    TotCost tc = (TotCost)hsTotCost[output.OutListNO];
                    tc.PurchaseCost += output.PurchaseCost;
                    tc.RetailCost += output.RetailCost;
                    tc.WholesaleCost += output.WholeSaleCost;
                }
                else
                {
                    ArrayList al = new ArrayList();
                    al.Add(output);
                    hs.Add(output.OutListNO, al);

                    TotCost tc = new TotCost();
                    tc.PurchaseCost += output.PurchaseCost;
                    tc.RetailCost += output.RetailCost;
                    tc.WholesaleCost += output.WholeSaleCost;
                    hsTotCost.Add(output.OutListNO, tc);
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
                    if(this.printBill.Sort == FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill.SortType.货位号)
                    {
                        Base.PrintBill.SortByPlaceNO(ref al);
                    }
                    else if (this.printBill.Sort == FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill.SortType.物理顺序)
                    {
                        Base.PrintBill.SortByBillNO(ref al);
                    }
                    this.SetPrintData(al, pageNow + 1, pageTotNum, printBill.Title);

                    this.neuPanel4.Height = this.neuPanel4.Height + (int)rowHeight * al.Count;
                    this.Height += (int)rowHeight * al.Count;

                    if (this.printBill.IsNeedPreview)
                    {
                        p.PrintPreview(0, 0, this.neuPanel1);
                    }
                    else
                    {
                        p.PrintPage(0, 0, this.neuPanel1);
                    }

                    this.neuPanel4.Height = height;
                    this.Height = ucHeight;
                }
            }
            #endregion

            return 1;
        }

        public int SetPrintData(ArrayList alPrintData, Base.PrintBill printBill)
        {
            if (this.alPrintData == null || alPrintData.Count == 0)
            {
                return 1;
            }
            FS.HISFC.Models.Pharmacy.Output output = alPrintData[0] as FS.HISFC.Models.Pharmacy.Output;
            FS.SOC.HISFC.BizLogic.Pharmacy.InOut itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.InOut();
            ArrayList al = itemMgr.QueryOutputInfo(output.StockDept.ID, output.OutListNO, output.State);
            if (al == null || al.Count == 0)
            {
                return 1;
            }
            this.alPrintData = al;
            this.printBill = printBill;
            return this.Print();
        }

        public int Preview()
        {
            this.printBill.IsNeedPreview = true;
            return this.Print();
        }

        #endregion

        #endregion
    }
}
