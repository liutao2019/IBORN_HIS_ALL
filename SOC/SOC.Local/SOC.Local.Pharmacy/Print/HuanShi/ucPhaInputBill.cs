using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.WinForms.Controls;
//using FS.SOC.HISFC.BizLogic.Pharmacy;

namespace FS.SOC.Local.Pharmacy.Print.HuanShi
{
    /// <summary>
    /// 东莞药品入库单据打印
    /// </summary>
    public partial class ucPhaInputBill : ucBaseControl, Base.IPharmacyBillPrint
    {
        /// <summary>
        /// 药品入库打印单
        /// </summary>
        public ucPhaInputBill()
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
        public FS.SOC.HISFC.BizLogic.Pharmacy.InOut pItem = new FS.SOC.HISFC.BizLogic.Pharmacy.InOut();




        /// <summary>
        /// 
        /// </summary>
        private class TotCost
        {
            public decimal purchaseCost;
            public decimal retailCost;
        }

        #endregion


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
            if (icount <= 0)
            {
                icount = 1;
            }
            FS.HISFC.Models.Pharmacy.Input info = (FS.HISFC.Models.Pharmacy.Input)al[0];

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

            string company = "";
            if (info.SourceCompanyType == "1")
            {
                company = FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.Company.ID);
            }
            else
            {
                company = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetCompanyName(info.Company.ID);
            }
            this.lblCompany.Text = "送货单位:" + info.Company.ID + " " + company;
            
            this.lblBillID.Text = "进仓号:" + info.InListNO;
            this.lblInputDate.Text = "进仓日期:" + info.InDate.ToShortDateString();
            this.lblOper.Text = "制表人:" + FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(info.Operation.ApplyOper.ID);
            this.neuLabel9.Text = "药品仓管员签名:";
            this.lblPage.Text = "页:"+inow.ToString() + "/" + icount.ToString();
          
            #endregion

            #region farpoint赋值
            decimal sumRetail = 0;
            decimal sumPurchase = 0;
            decimal sumDif = 0;
            //中西草
            decimal sumPCZret = 0;
            decimal sumPCZpur = 0;
            //卫生材料
            decimal sumBM = 0;
            //原材料
            decimal sumM = 0;

            decimal sumPPurCost = 0;
            decimal sumCPurCost = 0;
            decimal sumZPurCost = 0;
            decimal sumYPurCost = 0;
            decimal sumSPurCost = 0;
            decimal sumMPurCost = 0;

            FS.SOC.HISFC.BizLogic.Pharmacy.InOut inOutMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.InOut();
            ArrayList allData = inOutMgr.QueryInputInfoByListID(info.StockDept.ID, info.InListNO,info.Company.ID, "AAAA");
            if (allData.Count != 0 && allData != null)
            {
                for (int i = 0; i < allData.Count; i++)
                {
                    FS.HISFC.Models.Pharmacy.Input inPutModels = allData[i] as FS.HISFC.Models.Pharmacy.Input;

                    switch (inPutModels.Item.Type.ID)
                    {
                        case "C":
                            sumCPurCost += inPutModels.PurchaseCost;
                            break;
                        case "Z":
                            sumZPurCost += inPutModels.PurchaseCost;
                            break;
                        case "Y":
                            sumYPurCost += inPutModels.PurchaseCost;
                            break;
                        case "S":
                            sumSPurCost += inPutModels.PurchaseCost;
                            break;
                        case "P":
                            sumPPurCost += inPutModels.PurchaseCost;
                            break;
                        case "M":
                            sumMPurCost += inPutModels.PurchaseCost;
                            break;
                    }
                }
            }


            this.neuFpEnter1_Sheet1.RowCount = 0;
            for (int i = 0; i < al.Count; i++)
            {
                this.neuFpEnter1_Sheet1.AddRows(i, 1);
                FS.HISFC.Models.Pharmacy.Input input = al[i] as FS.HISFC.Models.Pharmacy.Input;
                this.neuFpEnter1_Sheet1.Cells[i, 0].Text = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(input.Item.ID).UserCode; //药品自定义码
                this.neuFpEnter1_Sheet1.Cells[i, 1].Text = input.Item.Name;//药品名称
                //this.neuFpEnter1_Sheet1.Cells[i, 10].Text = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(input.Item.ID).Product.ProducingArea;;//生产产地	
                this.neuFpEnter1_Sheet1.Cells[i, 2].Text = SOC.HISFC.BizProcess.Cache.Common.GetDrugTypeName(input.Item.Type.ID);
                this.neuFpEnter1_Sheet1.Cells[i, 11].Text = SOC.HISFC.BizProcess.Cache.Pharmacy.GetProducerName(input.Producer.ID);
                this.neuFpEnter1_Sheet1.Cells[i, 3].Value = input.Item.Specs;//规格
                if (input.Item.PackQty == 0)
                    input.Item.PackQty = 1;
                decimal count = 0;
                count = input.Quantity;

                FarPoint.Win.Spread.CellType.NumberCellType n = new FarPoint.Win.Spread.CellType.NumberCellType();
                n.DecimalPlaces = 4;

                FarPoint.Win.Spread.CellType.NumberCellType nCost = new FarPoint.Win.Spread.CellType.NumberCellType();
                nCost.DecimalPlaces = 2;
                nCost.DecimalPlaces = Function.GetCostDecimal();
                this.neuFpEnter1_Sheet1.Columns[7].CellType = nCost;
                this.neuFpEnter1_Sheet1.Columns[8].CellType = nCost;
                this.neuFpEnter1_Sheet1.Columns[9].CellType = nCost;
                this.neuFpEnter1_Sheet1.Columns[10].CellType = nCost;

                count = input.Quantity;
                if (input.ShowState == "0")
                {
                   
                    this.neuFpEnter1_Sheet1.Cells[i, 5].Value = input.Item.PackUnit;//单位
                    this.neuFpEnter1_Sheet1.Cells[i, 4].Value = (count / input.Item.PackQty).ToString();//
                   
                }
            
                
                else
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 5].Value = input.Item.PackUnit;//单位
                    this.neuFpEnter1_Sheet1.Cells[i, 4].Value = (count/input.Item.PackQty).ToString();//数量			
                }
                this.neuFpEnter1_Sheet1.Cells[i, 7].Value = input.Item.PriceCollection.PurchasePrice;
                this.neuFpEnter1_Sheet1.Cells[i, 8].Value = input.PurchaseCost;
                if (input.Item.Type.ID.ToString() == "P" || input.Item.Type.ID.ToString() == "Z")
                {
                    //this.neuFpEnter1_Sheet1.Cells[i, 8].Value = (FS.FrameWork.Function.NConvert.ToDecimal( input.Item.PriceCollection.PurchasePrice) * FS.FrameWork.Function.NConvert.ToDecimal(1.15)).ToString();
                    //this.neuFpEnter1_Sheet1.Cells[i, 9].Value = (FS.FrameWork.Function.NConvert.ToDecimal(input.PurchaseCost) * FS.FrameWork.Function.NConvert.ToDecimal(1.15)).ToString(); 
                    //this.neuFpEnter1_Sheet1.Cells[i, 10].Value = (FS.FrameWork.Function.NConvert.ToDecimal(input.PurchaseCost) * FS.FrameWork.Function.NConvert.ToDecimal(0.15)).ToString("F2"); 
                    this.neuFpEnter1_Sheet1.Cells[i, 9].Value = input.Item.PriceCollection.RetailPrice;
                    this.neuFpEnter1_Sheet1.Cells[i, 10].Value = input.RetailCost;
                    this.neuFpEnter1_Sheet1.Cells[i, 11].Value =  input.RetailCost - input.PurchaseCost;
                }
                else if (input.Item.Type.ID.ToString() == "C")
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 9].Value = input.Item.PriceCollection.RetailPrice;
                    this.neuFpEnter1_Sheet1.Cells[i, 10].Value = input.RetailCost;
                    this.neuFpEnter1_Sheet1.Cells[i, 11].Value = input.RetailCost - input.PurchaseCost;
                }
                else
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 9].Value = input.Item.PriceCollection.RetailPrice;
                    this.neuFpEnter1_Sheet1.Cells[i, 10].Value = input.RetailCost;
                    this.neuFpEnter1_Sheet1.Cells[i, 11].Value = input.RetailCost - input.PurchaseCost;
                }
 

                try
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 12].Text = SOC.HISFC.BizProcess.Cache.Pharmacy.GetProducerName(input.Producer.ID);
                }
                catch { }
                this.neuFpEnter1_Sheet1.Cells[i, 13].Text = (FS.FrameWork.Function.NConvert.ToDecimal(input.StoreQty) / FS.FrameWork.Function.NConvert.ToDecimal(input.Item.PackQty)).ToString("F0");
               
                if (input.ValidTime > DateTime.MinValue)
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 14].Text = input.ValidTime.Date.ToString("yyyy-MM-dd");
                }


                switch (input.Item.Type.ID)
                {
                    case "B":
                        sumBM += input.RetailCost;
                        break;
                    case "M":
                        sumM += input.PurchaseCost;
                        break;
                    case "C":
                        sumM += input.PurchaseCost;
                        sumBM += input.RetailCost;
                        break;
                    case "P":
                        sumM += input.PurchaseCost;
                        sumBM += input.RetailCost;
                        break;
                    case "Z":
                        sumM += input.PurchaseCost;
                        sumBM += input.RetailCost;
                        break;
                    default:
                        sumPCZret += input.RetailCost;
                        sumPCZpur += input.PurchaseCost;
                        break;
                }



                sumRetail = sumRetail + input.RetailCost;
                sumPurchase = sumPurchase + input.PurchaseCost;
                sumDif = sumRetail - sumPurchase;

          }
            //当前页数据
            this.lblCurRet.Text = "本页零售金额:" + sumRetail.ToString(Function.GetCostDecimalString());
            this.lblCurPur.Text = "本页购入金额:" + sumPurchase.ToString(Function.GetCostDecimalString());
            this.lblCurDif.Text = "本页购零差:" + sumDif.ToString(Function.GetCostDecimalString());
         
            //总数据
            this.lblTotPurCost.Text = ((TotCost)hsTotCost[info.InListNO]).purchaseCost.ToString(Function.GetCostDecimalString());
            this.lblTotRetailCost.Text = ((TotCost)hsTotCost[info.InListNO]).retailCost.ToString(Function.GetCostDecimalString());
            //this.lblTotDif.Text = (((TotCost)hsTotCost[info.InListNO]).retailCost - ((TotCost)hsTotCost[info.InListNO]).purchaseCost).ToString(Function.GetCostDecimalString());
            this.lblTotDiff.Text = "总毛利:" + (((TotCost)hsTotCost[info.InListNO]).retailCost - ((TotCost)hsTotCost[info.InListNO]).purchaseCost).ToString(Function.GetCostDecimalString());

            string sumByDrugType = "";
            if (sumPPurCost != 0)
            {
                sumByDrugType += "西药:" + sumPPurCost.ToString("F2") + "\n";
            }
            if (sumZPurCost != 0)
            {
                sumByDrugType += "成药:" + sumZPurCost.ToString("F2") + "\n";
            }
            if (sumCPurCost != 0)
            {
                sumByDrugType += "草药:" + sumCPurCost.ToString("F2") + "\n";
            }
            if (sumYPurCost != 0)
            {
                sumByDrugType += "疫苗:" + sumYPurCost.ToString("F2") + "\n";
            }
            if (sumSPurCost != 0)
            {
                sumByDrugType += "材料:" + sumSPurCost.ToString("F2") + "\n";
            }
            if (sumMPurCost != 0)
            {
                sumByDrugType += "耗材:" + sumMPurCost.ToString("F2") + "\n";
            }
            this.neulabel2.Text = sumByDrugType;
    
            #endregion

            this.resetTitleLocation();

        }
            
                

        /// <summary>
        /// 重新设置标题位置
        /// </summary>
        private void resetTitleLocation()
        {
            this.neuPanel4.Controls.Remove(this.lblTitle);
            int with = 0;
            for (int col = 0; col < this.neuFpEnter1_Sheet1.ColumnCount; col++)
            {
                if (this.neuFpEnter1_Sheet1.Columns[col].Visible)
                {
                    with += (int)this.neuFpEnter1_Sheet1.Columns[col].Width;
                }
            }
            if (with > this.neuPanel4.Width)
            {
                with = this.neuPanel4.Width;
            }
            this.lblTitle.Location = new Point((with - this.lblTitle.Size.Width) / 2, this.lblTitle.Location.Y);
            this.neuPanel4.Controls.Add(this.lblTitle);

        }
        #endregion

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
            int height = this.neuPanel5.Height;
            int ucHeight = this.Height;
            float rowHeight = this.neuFpEnter1_Sheet1.Rows[0].Height;
            this.neuFpEnter1_Sheet1.Columns[0].CellType = new FarPoint.Win.Spread.CellType.TextCellType();

            if (this.printBill.Sort == FS.SOC.Local.Pharmacy.Base.PrintBill.SortType.货位号)
            {
                Base.PrintBill.SortByPlaceNO(ref alPrintData);
            }
            else if (this.printBill.Sort == FS.SOC.Local.Pharmacy.Base.PrintBill.SortType.物理顺序)
            {
                Base.PrintBill.SortByBillNO(ref alPrintData);
            }

            //补打的时候可能有多张单据，分开
            System.Collections.Hashtable hs = new Hashtable();
            foreach (FS.HISFC.Models.Pharmacy.Input input in alPrintData)
            {
                FS.HISFC.Models.Pharmacy.Input i = input.Clone();
                if (hs.Contains(i.InListNO))
                {

                    ArrayList al = (ArrayList)hs[i.InListNO];
                    al.Add(i);

                    TotCost tc = (TotCost)hsTotCost[i.InListNO];
                    tc.purchaseCost += i.PurchaseCost;
                    tc.retailCost += i.RetailCost;
                    //if (i.Item.Type.ID.ToString() == "P" || i.Item.Type.ID.ToString() == "Z")
                    //{
                    //    tc.retailCost += i.RetailCost;
                    //}

                    //else if (i.Item.Type.ID.ToString() == "C")
                    //{
                    //    tc.retailCost+= FS.FrameWork.Function.NConvert.ToDecimal(i.PurchaseCost) * FS.FrameWork.Function.NConvert.ToDecimal(1.25);
                    //}
                    //else
                    //{
                    //    tc.retailCost += i.RetailCost;
                    //}
                    }
                else
                {
                    ArrayList al = new ArrayList();
                    al.Add(i);
                    hs.Add(i.InListNO, al);

                    TotCost tc = new TotCost();
                    tc.purchaseCost = i.PurchaseCost;
                    tc.retailCost = i.RetailCost;
                    //if (i.Item.Type.ID.ToString() == "P" || i.Item.Type.ID.ToString() == "Z")
                    //{
                    //    tc.retailCost += i.RetailCost;
                    //}

                    //else if (i.Item.Type.ID.ToString() == "C")
                    //{
                    //    tc.retailCost = +FS.FrameWork.Function.NConvert.ToDecimal(i.PurchaseCost) * FS.FrameWork.Function.NConvert.ToDecimal(1.25);
                    //}
                    //else
                    //{
                    //    tc.retailCost += i.RetailCost;
                    //}
                    hsTotCost.Add(input.InListNO, tc);
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
                   
                    this.SetPrintData(al, pageNow+1, pageTotNum, printBill.Title);

                    this.neuPanel5.Height += (int)rowHeight * al.Count;
                    this.Height += (int)rowHeight * al.Count;

                    if (this.printBill.IsNeedPreview)
                    {
                        p.PrintPreview(5, 0, this.neuPanel1);
                    }
                    else
                    {
                        p.PrintPage(5, 0, this.neuPanel1);
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
                string bill = (alPrintData[0] as FS.HISFC.Models.Pharmacy.Input).InListNO;
                string dept = (alPrintData[0] as FS.HISFC.Models.Pharmacy.Input).StockDept.ID;
                FS.SOC.HISFC.BizLogic.Pharmacy.InOut itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.InOut();
                ArrayList al = itemMgr.QueryInputInfoByListID(dept, bill, "AAAA", "AAAA");
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
