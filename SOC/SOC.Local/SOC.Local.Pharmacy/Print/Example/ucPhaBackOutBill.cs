using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy;
using FS.FrameWork.WinForms.Controls;

namespace FS.SOC.Local.Pharmacy.Print.Example
{
    /// <summary>
    /// 东莞药库出库退库单据打印
    /// </summary>
    public partial class ucPhaBackOutBill : ucBaseControl, Base.IPharmacyBillPrint
    {
        /// <summary>
        /// 药库出库退库(内退)打印单
        /// </summary>
        public ucPhaBackOutBill()
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
            public decimal purchaseCost;
            public decimal retailCost;
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
        /// <param name="title">标题</param>
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
                this.lblTitle.Text = string.Format(this.lblTitle.Text, SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.StockDept.ID));
            }
            else
            {
                if (title.IndexOf("[库存科室]") != -1)
                {
                    title = title.Replace("[库存科室]", SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.StockDept.ID));
                }

                if (title.IndexOf("[退库]") != -1)
                {
                    title = title.Replace("[退库]", "内退");
                }

                this.lblTitle.Text = title;
            }

            this.lblCompany.Text = "退货单位: " + info.TargetDept.ID +" "+ SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.TargetDept.ID);
            this.lblBillID.Text = "出库单号: " + info.OutListNO;
            this.lblInputDate.Text = "退货日期: " + info.OutDate.ToShortDateString();
            this.lblOper.Text = "制单人:" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(info.Operation.ExamOper.ID); //BillPrintFun.GetEmplName(info.Operation.ExamOper.ID);
            this.lblPage.Text = "页：" + inow.ToString() + "/" + icount.ToString();
            this.neuLabel10.Text = "仓管员：";// +BillPrintFun.GetStockManagerName(info.StockDept.ID);
            this.neuLabel10.Visible = true;
            #endregion

            #region farpoint赋值
            decimal sumRetail = 0;
            decimal sumPurchase = 0;
            decimal sumDif = 0;

            this.neuFpEnter1_Sheet1.RowCount = 0;
            for (int i = 0; i < al.Count; i++)
            {
                this.neuFpEnter1_Sheet1.AddRows(i, 1);
                FS.HISFC.Models.Pharmacy.Output output = al[i] as FS.HISFC.Models.Pharmacy.Output;
                this.neuFpEnter1_Sheet1.Cells[i, 0].Text =   SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(output.Item.ID).UserCode; //药品自定义码
                this.neuFpEnter1_Sheet1.Cells[i, 1].Text = output.Item.Name;//药品名称
                this.neuFpEnter1_Sheet1.Cells[i, 2].Text = output.Item.Specs;//规格		
                if (output.Item.PackQty == 0)
                    output.Item.PackQty = 1;
                decimal count = 0;
                count = Math.Abs(output.Quantity);//出库退库数量是负的  但是需要打印出正数

                FarPoint.Win.Spread.CellType.NumberCellType n = new FarPoint.Win.Spread.CellType.NumberCellType();
                n.DecimalPlaces = 4;
                this.neuFpEnter1_Sheet1.Columns[7].CellType = n;
                this.neuFpEnter1_Sheet1.Columns[9].CellType = n;
                this.neuFpEnter1_Sheet1.Columns[8].CellType = n;
                this.neuFpEnter1_Sheet1.Columns[10].CellType = n;
                if (SOC.HISFC.BizProcess.Cache.Common.GetDept(output.StockDept.ID).DeptType.ID.ToString() == "P")
                {
                   
                    this.neuFpEnter1_Sheet1.Cells[i, 5].Text = output.Item.MinUnit;//单位
                    this.neuFpEnter1_Sheet1.Cells[i, 6].Text = (count).ToString();//数量				
                    this.neuFpEnter1_Sheet1.Cells[i, 7].Value = (output.Item.PriceCollection.PurchasePrice / output.Item.PackQty).ToString("F4");//买入价
                    this.neuFpEnter1_Sheet1.Cells[i, 9].Value = (output.Item.PriceCollection.WholeSalePrice / output.Item.PackQty).ToString("F4");//零售价
                }
                else
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 5].Text = output.Item.PackUnit;//单位
                    this.neuFpEnter1_Sheet1.Cells[i, 6].Text = (count / output.Item.PackQty).ToString("F2");//数量				
                    this.neuFpEnter1_Sheet1.Cells[i, 7].Value = output.Item.PriceCollection.PurchasePrice;//买入价
                    this.neuFpEnter1_Sheet1.Cells[i, 9].Value = output.Item.PriceCollection.WholeSalePrice;//零售价
               
                }
                this.neuFpEnter1_Sheet1.Cells[i, 3].Text = SOC.HISFC.BizProcess.Cache.Pharmacy.GetProducerName(output.Producer.ID);//生产厂家
                this.neuFpEnter1_Sheet1.Cells[i, 4].Text = output.BatchNO;
                this.neuFpEnter1_Sheet1.Cells[i, 8].Value = output.PurchaseCost;//买入金额
                this.neuFpEnter1_Sheet1.Cells[i, 10].Value = output.WholeSaleCost;//退库金额

                sumRetail = sumRetail + output.WholeSaleCost;
                sumPurchase = sumPurchase + output.PurchaseCost;
                sumDif = sumRetail - sumPurchase;
            }
            //当前页数据
            this.lblCurRet.Text = "本页零售金额：" + sumRetail.ToString("F4");
            this.lblCurPur.Text = "本页购入金额：" + sumPurchase.ToString("F4");
            this.lblCurDif.Text = "本页购零差：" + sumDif.ToString("F4");

            //总数据
            this.lblTotPurCost.Text = ((TotCost)hsTotCost[info.OutListNO]).purchaseCost.ToString("F4");
            this.lblTotRetailCost.Text = ((TotCost)hsTotCost[info.OutListNO]).retailCost.ToString("F4");

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
            for (int col = 0; col < this.neuFpEnter1_Sheet1.ColumnCount; col++)
            {
                if (this.neuFpEnter1_Sheet1.Columns[col].Visible)
                {
                    with += (int)this.neuFpEnter1_Sheet1.Columns[col].Width;
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
            float rowHeight = this.neuFpEnter1_Sheet1.Rows[0].Height;
            this.neuFpEnter1_Sheet1.Columns[0].CellType = new FarPoint.Win.Spread.CellType.TextCellType();

            //补打的时候可能有多张单据，分开
            System.Collections.Hashtable hs = new Hashtable();
            foreach (FS.HISFC.Models.Pharmacy.Output output in alPrintData)
            {
                FS.HISFC.Models.Pharmacy.Output o = output.Clone();
                if (hs.Contains(o.OutListNO))
                {

                    ArrayList al = (ArrayList)hs[o.OutListNO];
                    al.Add(o);

                    TotCost tc = (TotCost)hsTotCost[o.OutListNO];
                    tc.purchaseCost += o.PurchaseCost;
                    tc.retailCost += o.WholeSaleCost;
                }
                else
                {
                    ArrayList al = new ArrayList();
                    al.Add(o);
                    hs.Add(o.OutListNO, al);

                    TotCost tc = new TotCost();
                    tc.purchaseCost = o.PurchaseCost;
                    tc.retailCost = o.WholeSaleCost;
                    hsTotCost.Add(o.OutListNO, tc);
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

                    this.neuPanel4.Height += (int)rowHeight * al.Count;
                    this.Height += (int)rowHeight * al.Count;

                    if (this.printBill.IsNeedPreview)
                    {
                        p.PrintPreview(5, 0, this.neuPanel1);
                    }
                    else
                    {
                        p.PrintPage(5, 0, this.neuPanel1);
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
            ArrayList al = itemMgr.QueryOutputInfo(output.StockDept.ID, output.OutListNO, "2");
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
