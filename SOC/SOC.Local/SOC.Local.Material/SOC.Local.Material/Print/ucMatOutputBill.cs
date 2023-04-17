using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Material.Print
{
    public partial class ucMatOutputBill : UserControl, FS.HISFC.Interface.Material.Print.IBillPrint
    {
        public ucMatOutputBill()
        {
            InitializeComponent();
        }

        FS.HISFC.InterfaceArchieve.Material.Manager.ManagerAchieve matManager = new FS.HISFC.InterfaceArchieve.Material.Manager.ManagerAchieve();
        FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
        FS.HISFC.BizLogic.Manager.Person personManager = new FS.HISFC.BizLogic.Manager.Person();
        FS.HISFC.BizLogic.Material.BizLogic.Store.StockLogic stockManager = new FS.HISFC.BizLogic.Material.BizLogic.Store.StockLogic();

        #region 变量

        /// <summary>
        /// 所有待打印数据
        /// </summary>
        private ArrayList alPrintData = new ArrayList();

        /// <summary>
        /// 单据的总金额
        /// </summary>
        private Hashtable hsTotCost = new Hashtable();

        private decimal totCost = 0;

        private class TotCost
        {
            public decimal purchaseCost;
            public decimal retailCost;
        }

        TotCost totCosetAll = new TotCost(); //新加
        private int rowcount = 8;
        #endregion

        int totRows = 0;

        #region 入库单打印
        //private void SetPrintData(ArrayList al, int inow, int icount, string title)
        //{
        //    FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
        //    FS.HISFC.BizLogic.Manager.Person personManager = new FS.HISFC.BizLogic.Manager.Person();
        //    if (al.Count <= 0)
        //    {
        //        MessageBox.Show("没有打印的数据!");
        //        return;
        //    }
        //    FS.HISFC.BizLogic.Material.Object.Output info = (FS.HISFC.BizLogic.Material.Object.Output)al[0];

        //    string strDept = info.Storage.ID;
        //    if (strDept == "")
        //    {
        //        strDept = info.StockHeadInfo.Storage.ID;
        //    }
        //    this.totCost = 0;
        //    #region label赋值
        //    if (string.IsNullOrEmpty(title))
        //    {
               
        //    //    this.lblTitle.Text = string.Format(this.lblTitle.Tag.ToString(), deptManager.GetDeptmentById(strDept).Name);
        //        string titleIn = this.matManager.GetControlParam<string>("MATS31", true, "") + deptManager.GetDeptmentById(strDept).Name + "出库单";
        //        this.lblTitle.Text = titleIn; //本地化
        //    }
        //    else
        //    {
        //        if (title.IndexOf("[库存科室]") != -1)
        //        {
        //            this.lblTitle.Text = title.Replace("[库存科室]", deptManager.GetDeptmentById(strDept).Name);
        //        }
        //        else
        //        {
        //            this.lblTitle.Text = title;
        //        }
        //    }

        //    this.lblCompany.Text = "领料单位: " + info.TargetDept.ID + " " + deptManager.GetDeptmentById(info.TargetDept.ID).Name;
        //    this.lblBillID.Text = "领料号: " + info.OutListCode;
        //    this.lblInputDate.Text = "领料日期: " + info.OutDate.ToShortDateString();
        //    this.lblOper.Text = "制表人:" + personManager.GetPersonByID(info.Oper.ID).Name;
        //    this.neuLabel10.Text = "仓管员签名："    ;
        //    this.lblPage.Text = "页：" + inow.ToString() + "/" + this.totRows.ToString();
        //    this.neuLabel10.Visible = true;
        //    #endregion

        //    #region farpoint赋值
        //    decimal sumRetail = 0;
        //    decimal sumPurchase = 0;
        //    decimal sumDif = 0;
        //    decimal sumDrugWholeCost = 0;
        //    decimal sumDrugPurCost = 0;
            
        //    decimal sumBWholeCost = 0;
        //    decimal sumBPurCost = 0;


        //    this.neuFpEnter1_Sheet1.RowCount = 0;

        //    FS.HISFC.BizLogic.Material.BizLogic.Store.StockLogic stockManager = new FS.HISFC.BizLogic.Material.BizLogic.Store.StockLogic();

        //    for (int i = 0; i < al.Count; i++)
        //    {
        //        this.neuFpEnter1_Sheet1.AddRows(i, 1);
        //        this.neuFpEnter1_Sheet1.Columns[2].Width = 81F;

        //        FS.HISFC.BizLogic.Material.Object.Output output = al[i] as FS.HISFC.BizLogic.Material.Object.Output;
        //        //获取库存明细获取购入价
        //       FS.HISFC.BizLogic.Material.Object.StockDetail stockDetail=  stockManager.GetStockDetail(output.StockCode);
        //       if (stockDetail != null && string.IsNullOrEmpty(stockDetail.StockNo) == false)
        //       {
        //           output.OutPrice = stockDetail.SourceInPrice;
        //       }
        //       else
        //       {
        //           output.OutPrice = output.StockHeadInfo.StorePrice;
        //       }
        //        this.neuFpEnter1_Sheet1.Cells[i, 0].Text = output.BaseInfo.UserCode; //药品自定义码
        //        this.neuFpEnter1_Sheet1.Cells[i, 1].Text = output.BaseInfo.Name;//药品名称
        //        this.neuFpEnter1_Sheet1.Cells[i, 2].Text = output.BaseInfo.Specs;//规格		
        //        if (output.BaseInfo.PackQty == 0)
        //            output.BaseInfo.PackQty = 1;
        //        decimal count = 0;
        //        count = output.OutNum;
        //        FarPoint.Win.Spread.CellType.NumberCellType n = new FarPoint.Win.Spread.CellType.NumberCellType();
        //        n.DecimalPlaces = 4;
        //        this.neuFpEnter1_Sheet1.Columns[7].CellType = n;
        //        this.neuFpEnter1_Sheet1.Columns[8].CellType = n;
        //        this.neuFpEnter1_Sheet1.Columns[9].CellType = n;
        //        if (output.TargetDept.ID =="0")   //这个用于特殊科室来表示。
        //        {
                    
        //            this.neuFpEnter1_Sheet1.Cells[i, 3].Text = output.BaseInfo.MinUnit;//单位
        //            this.neuFpEnter1_Sheet1.Cells[i, 4].Text = (count).ToString();//数量				
        //            this.neuFpEnter1_Sheet1.Cells[i, 6].Value = output.OutPrice; //output.BaseInfo.InPrice;
        //            this.neuFpEnter1_Sheet1.Cells[i, 8].Value = output.OutSalePrice;
        //        }
        //        else
        //        {
        //            this.neuFpEnter1_Sheet1.Cells[i, 3].Text = output.BaseInfo.MinUnit;//单位
        //            this.neuFpEnter1_Sheet1.Cells[i, 4].Text = (count).ToString();//数量				
        //            this.neuFpEnter1_Sheet1.Cells[i, 6].Value = output.OutPrice;// output.BaseInfo.InPrice;
        //            this.neuFpEnter1_Sheet1.Cells[i, 8].Value = output.OutSalePrice;
        //        }
               
        //        switch(output.TargetDept.ID)
        //        {
        //            case "B":  //以后写特殊入库的
        //                sumBPurCost += output.OutPrice * output.OutNum;
        //                sumBWholeCost += output.OutSaleCost;
        //                break;

        //            default:

        //                sumDrugPurCost += output.OutPrice * output.OutNum;
        //                sumDrugWholeCost += output.OutSaleCost;
        //                break;

        //        }

        //        if (output.StockValidDate > DateTime.MinValue)
        //        {
        //            this.neuFpEnter1_Sheet1.Cells[i, 5].Text = output.StockValidDate.ToString("yyyy-MM-dd");
        //        }
        //        this.neuFpEnter1_Sheet1.Cells[i, 7].Value = (output.OutPrice * output.OutNum).ToString("F4");// 买入金额 (output.BaseInfo.InPrice * output.OutNum).ToString("F4");
        //        this.neuFpEnter1_Sheet1.Cells[i, 9].Value = output.OutSaleCost.ToString("F4"); //零售金额
        //        this.neuFpEnter1_Sheet1.Cells[i, 10].Value = (output.PrivStoreNum - count ).ToString("F2");

        //        sumRetail = sumRetail + output.OutSaleCost;
        //        sumPurchase += output.OutPrice/output.BaseInfo.PackQty * output.OutNum;
        //        sumDif = sumRetail - sumPurchase;
        //    }
        //    this.lblCurRet.Text = "本页零售金额：" + sumRetail.ToString("F2");
        //    this.lblCurPur.Text = "本页购入金额：" + sumPurchase.ToString("F2");
        //    this.lblCurDif.Text = "本页购零差：" + sumDif.ToString("F2");
        //    this.totCost += sumPurchase;

           
        //    //总数据
        //    //this.lblTotPurCost.Text = ((TotCost)hsTotCost[info.OutListCode]).purchaseCost.ToString("F4");
        //    //this.lblTotPurCost.Text = this.totCost.ToString();
        //    //this.lblTotRetailCost.Text = ((TotCost)hsTotCost[info.OutListCode]).retailCost.ToString("F2");
        //    //this.lblTotDif.Text = (((TotCost)hsTotCost[info.OutListCode]).retailCost - ((TotCost)hsTotCost[info.OutListCode]).purchaseCost).ToString("F4");
          
        //    this.lblTotPurCost.Text = this.totCosetAll.purchaseCost.ToString("F2"); //总购价
        //    this.lblTotRetailCost.Text = this.totCosetAll.retailCost.ToString("F2");//总差价
        //    this.lblTotDif.Text = (((TotCost)hsTotCost[info.OutListCode]).retailCost - ((TotCost)hsTotCost[info.OutListCode]).purchaseCost).ToString("F4");
          
        //    if (info.TargetDept.ID == "7014" || info.TargetDept.ID == "7015")
        //    {
        //        this.neuFpEnter1_Sheet1.Columns[6].Visible = false;
        //        this.neuFpEnter1_Sheet1.Columns[7].Visible = false;
        //        this.lblTotPurCost.Visible = false;
        //        this.lblCurPur.Text = "本页买入金额：";
        //    }
        //    else
        //    {
        //        this.neuFpEnter1_Sheet1.Columns[6].Visible = true;
        //        this.neuFpEnter1_Sheet1.Columns[7].Visible = true;
        //        this.lblTotPurCost.Visible = true;
        //    }

        //    #endregion

        //    this.resetTitleLocation();

        //}
        #endregion

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

        #region IBillPrint 成员

        public bool IsRePrint
        {
            get
            {
                return true;
            }
            set
            {
                
            }
        }

        public int  Prieview()
        {
            return this.Print();
        }

        public int Print()
        {
            #region 打印信息设置 

            int pageHeight = this.matManager.GetControlParam<int>("MATS33", true, 100); //纸张高度
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.IsDataAutoExtend = false;
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;
            p.IsHaveGrid = true;
            p.SetPageSize(new FS.HISFC.Models.Base.PageSize("1234", 850, pageHeight));
            
            //this.printBill.PageSize.HeightMM = pageHeight;
            //p.SetPageSize(this.printBill.PageSize);
            #endregion

            #region 分页打印
            int height = this.neuPanel4.Height;
            int ucHeight = this.Height;
            float rowHeight = this.neuFpEnter1_Sheet1.Rows[0].Height;
            this.neuFpEnter1_Sheet1.Columns[0].CellType = new FarPoint.Win.Spread.CellType.TextCellType();

            //补打的时候可能有多张单据，分开
            System.Collections.Hashtable hs = new Hashtable();
            this.hsTotCost.Clear();
            foreach (FS.HISFC.BizLogic.Material.Object.Output output in alPrintData)
            {
                FS.HISFC.BizLogic.Material.Object.Output o = output.Clone();

                if (hs.Contains(o.OutListCode))
                {

                    ArrayList al = (ArrayList)hs[o.OutListCode];
                    al.Add(o);

                    TotCost tc = (TotCost)hsTotCost[o.OutListCode];
                    tc.purchaseCost += o.OutPrice * o.OutNum / o.StorePackQty;
                    tc.retailCost += o.OutSaleCost;
                }
                else
                {
                    ArrayList al = new ArrayList();
                    al.Add(o);
                    hs.Add(o.OutListCode, al);

                    TotCost tc = new TotCost();
                    tc.purchaseCost = o.OutPrice * o.OutNum / o.StorePackQty;
                    tc.retailCost = o.OutSaleCost;
                    hsTotCost.Add(o.OutListCode, tc);
                }
            }

            foreach (ArrayList alPrint in hs.Values)
            {
                int pageTotNum = (int)Math.Ceiling((double)alPrint.Count / this.PrintRowCount);

                for (int pageNow = 0; pageNow < pageTotNum; pageNow++)
                {
                    //ArrayList al = new ArrayList();
                    List<FS.FrameWork.Models.NeuObject> al = new List<FS.FrameWork.Models.NeuObject>();
                    for (int index = pageNow * this.PrintRowCount; index < alPrint.Count && index < (pageNow + 1) * this.PrintRowCount; index++)
                    {
                        al.Add(alPrint[index] as FS.FrameWork.Models.NeuObject);
                    }

                    this.SetPrintData(al, pageNow + 1, pageTotNum);

                    this.neuPanel4.Height = this.neuPanel4.Height + (int)rowHeight * al.Count;
                    this.Height += (int)rowHeight * al.Count;

                    p.PrintPage(5, 0, this.neuPanel1);

                    this.neuPanel4.Height = height;
                    this.Height = ucHeight;
                }
            }
            #endregion

            return 1;
        }

        public int PrintRowCount
        {
            get
            {
                return this.matManager.GetControlParam<int>("MATS32", true, 8);
            }
        }

        public int SetPrintData(List<FS.FrameWork.Models.NeuObject> DataList, int pageCount, int totPageCount)
        {
            if (DataList == null || DataList.Count == 0)
            {
                return 1;
            }

            decimal sumRetail = 0;
            decimal sumPurchase = 0;
            decimal sumDif = 0;
            decimal sumDrugWholeCost = 0;
            decimal sumDrugPurCost = 0;

            decimal sumBWholeCost = 0;
            decimal sumBPurCost = 0;

            FarPoint.Win.Spread.CellType.NumberCellType nct = new FarPoint.Win.Spread.CellType.NumberCellType();
            nct.DecimalPlaces = 4;
            this.neuFpEnter1_Sheet1.Columns[7].CellType = nct;
            this.neuFpEnter1_Sheet1.Columns[8].CellType = nct;
            this.neuFpEnter1_Sheet1.Columns[9].CellType = nct;

            FS.HISFC.BizLogic.Material.Object.Output info = DataList[0] as FS.HISFC.BizLogic.Material.Object.Output;

            string strDept = info.Storage.ID;
            if (string.IsNullOrEmpty(strDept))
            {
                strDept = info.StockHeadInfo.Storage.ID;
            }

            string titleIn = this.matManager.GetControlParam<string>("MATS31", true, "");
            this.lblTitle.Text = titleIn + deptManager.GetDeptmentById(strDept).Name + "出库单"; //本地化
            
            this.lblCompany.Text = "领料单位: " + info.TargetDept.ID + " " + deptManager.GetDeptmentById(info.TargetDept.ID).Name;
            this.lblBillID.Text = "领料号: " + info.OutListCode;
            this.lblInputDate.Text = "领料日期: " + info.OutDate.ToShortDateString();
            this.lblOper.Text = "制表人:" + personManager.GetPersonByID(info.Oper.ID).Name;
            this.neuLabel10.Text = "仓管员签名：";
            this.lblPage.Text = "页：" + pageCount.ToString() + "/" + totPageCount.ToString();
            this.neuLabel10.Visible = true;

            int i = 0;
            alPrintData = new ArrayList();
            foreach(FS.HISFC.BizLogic.Material.Object.Output output in DataList)
            {
                alPrintData.Add(output);
                this.neuFpEnter1_Sheet1.AddRows(i, 1);
                this.neuFpEnter1_Sheet1.Columns[2].Width = 81F;

                FS.HISFC.BizLogic.Material.Object.StockDetail stockDetail = stockManager.GetStockDetail(output.StockCode);
                if (stockDetail != null && string.IsNullOrEmpty(stockDetail.StockNo) == false)
                {
                    output.OutPrice = stockDetail.SourceInPrice;
                }
                else
                {
                    output.OutPrice = output.StockHeadInfo.StorePrice;
                }
                this.neuFpEnter1_Sheet1.Cells[i, 0].Text = output.BaseInfo.UserCode; //药品自定义码
                this.neuFpEnter1_Sheet1.Cells[i, 1].Text = output.BaseInfo.Name;//药品名称
                this.neuFpEnter1_Sheet1.Cells[i, 2].Text = output.BaseInfo.Specs;//规格		
                if (output.BaseInfo.PackQty == 0)
                    output.BaseInfo.PackQty = 1;
                if (output.TargetDept.ID == "0")   //这个用于特殊科室来表示。
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 3].Text = output.BaseInfo.MinUnit;//单位
                    this.neuFpEnter1_Sheet1.Cells[i, 4].Text = output.OutNum.ToString();//数量				
                    this.neuFpEnter1_Sheet1.Cells[i, 6].Value = output.OutPrice; //output.BaseInfo.InPrice;
                    this.neuFpEnter1_Sheet1.Cells[i, 8].Value = output.OutSalePrice;
                }
                else
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 3].Text = output.BaseInfo.MinUnit;//单位
                    this.neuFpEnter1_Sheet1.Cells[i, 4].Text = output.OutNum.ToString();//数量				
                    this.neuFpEnter1_Sheet1.Cells[i, 6].Value = output.OutPrice;// output.BaseInfo.InPrice;
                    this.neuFpEnter1_Sheet1.Cells[i, 8].Value = output.OutSalePrice;
                }

                switch (output.TargetDept.ID)
                {
                    case "B":  //以后写特殊入库的
                        sumBPurCost += output.OutPrice * output.OutNum / output.StorePackQty;
                        sumBWholeCost += output.OutSaleCost;
                        break;

                    default:
                        sumDrugPurCost += output.OutPrice * output.OutNum / output.StorePackQty;
                        sumDrugWholeCost += output.OutSaleCost;
                        break;

                }

                if (output.StockValidDate > DateTime.MinValue)
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 5].Text = output.StockValidDate.ToString("yyyy-MM-dd");
                }
                this.neuFpEnter1_Sheet1.Cells[i, 7].Value = (output.OutPrice * output.OutNum / output.StorePackQty).ToString("F4");// 买入金额 (output.BaseInfo.InPrice * output.OutNum).ToString("F4");
                this.neuFpEnter1_Sheet1.Cells[i, 9].Value = output.OutSaleCost.ToString("F4"); //零售金额
                //this.neuFpEnter1_Sheet1.Cells[i, 10].Value = (output.PrivStoreNum - count).ToString("F2");

                sumRetail = sumRetail + output.OutSaleCost;
                sumPurchase += output.OutPrice * output.OutNum;//output.OutNum * output.BaseInfo.InPrice;
                sumDif = sumRetail - sumPurchase;
            }

            this.lblCurRet.Text = "本页零售金额：" + sumRetail.ToString("F2");
            this.lblCurPur.Text = "本页购入金额：" + sumPurchase.ToString("F2");
            this.lblCurDif.Text = "本页购零差：" + sumDif.ToString("F2");

            this.lblTotPurCost.Text = this.totCosetAll.purchaseCost.ToString("F2"); //总购价
            this.lblTotRetailCost.Text = this.totCosetAll.retailCost.ToString("F2");//总差价
            //this.lblTotDif.Text = (((TotCost)hsTotCost[info.OutListCode]).retailCost - ((TotCost)hsTotCost[info.OutListCode]).purchaseCost).ToString("F4");

            this.resetTitleLocation();

            return 1;
        }

        #endregion

        public int SetPrintDataTotal(List<FS.FrameWork.Models.NeuObject> DataList)
        {
            totCosetAll.purchaseCost = 0;
            totCosetAll.retailCost = 0;
            if (DataList != null && DataList.Count > 0)
            {
                foreach (FS.HISFC.BizLogic.Material.Object.Output outPut in DataList)
                {
                    totCosetAll.purchaseCost +=  outPut.OutCost ;
                    totCosetAll.retailCost +=  outPut.OutSaleCost;
                }
            }
            return 1;
        }
    }
}
