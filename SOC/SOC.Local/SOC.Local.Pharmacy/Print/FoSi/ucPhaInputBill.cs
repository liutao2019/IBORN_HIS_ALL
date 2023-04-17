using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.WinForms.Controls;
using FS.FrameWork.Function;

namespace FS.SOC.Local.Pharmacy.Print.FoSi
{
    /// <summary>
    /// 东莞药品入库单据打印---佛四一般入库
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

        /// <summary>
        /// 
        /// </summary>
        private class TotCost
        {
            public decimal purchaseCost;
            public decimal retailCost;
        }

        private string drugType="";

        /// <summary>
        /// 打印相关属性值
        /// </summary>
        private Base.PrintBill printBill = new Base.PrintBill();

        /// <summary>
        /// 总页数
        /// </summary>
        private int pageCount = 0;

        /// <summary>
        /// 当前打印页数
        /// </summary>
        private int curPage = 1;

        /// <summary>
        /// 常数管理业务层
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();
        #endregion

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
  
        /// <summary>
        /// 1外部调用       
        /// </summary>
        /// <param name="alPrintData"></param>
        /// <param name="printBill"></param>
        /// <returns></returns>
        public int SetPrintData(ArrayList alPrintData, Base.PrintBill printBill)
        {
            if (alPrintData != null && alPrintData.Count > 0)
            {
                string bill = (alPrintData[0] as FS.HISFC.Models.Pharmacy.Input).InListNO;
                string dept = (alPrintData[0] as FS.HISFC.Models.Pharmacy.Input).StockDept.ID;
                FS.SOC.HISFC.BizLogic.Pharmacy.InOut itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.InOut();
                ArrayList al = itemMgr.QueryInputInfoByListID(dept, bill, "AAAA", "AAAA");

                //Base.PrintBill.SortByCustomerCode(ref al);
                
                    ArrayList drugTypeList = constMgr.GetList(FS.HISFC.Models.Base.EnumConstant.ITEMTYPE);

                    #region 计算总页数
                    Hashtable hsDrugType = new Hashtable();
                    foreach (FS.HISFC.Models.Pharmacy.Input objInput in al)
                    {
                        if (!hsDrugType.Contains(objInput.Item.Type.ID))
                        {
                            hsDrugType.Add(objInput.Item.Type.ID, 1);
                        }
                        else
                        {
                            int countValue = NConvert.ToInt32(hsDrugType[objInput.Item.Type.ID]) + 1;
                            hsDrugType[objInput.Item.Type.ID] = countValue;
                        }
                    }

                    foreach (string keys in hsDrugType.Keys)
                    {
                        pageCount = pageCount + 1;
                        int drugCount = NConvert.ToInt32(hsDrugType[keys]);

                        int pageTotNum = drugCount / printBill.RowCount;
                        if (drugCount != printBill.RowCount * pageTotNum)
                        {
                            pageTotNum++;
                        }
                        pageCount = pageCount + pageTotNum - 1;
                    }
                    #endregion

                    foreach (FS.HISFC.Models.Base.Const con in drugTypeList)
                    {
                        ArrayList typeList = new ArrayList();

                        foreach (FS.HISFC.Models.Pharmacy.Input input in al)
                        {
                            if (input.Item.Type.ID == con.ID)
                            {
                                typeList.Add(input);
                            }
                        }

                        //this.alPrintData = typeList;

                        //this.printBill = printBill;

                        //this.drugType = con.Name;

                        //if (this.Print() == -1)
                        //{
                        //    return -1;
                        //}
                    }
                    int pageTotNumtemp = al.Count / printBill.RowCount;
                    if (al.Count != printBill.RowCount * pageTotNumtemp)
                    {
                        pageTotNumtemp++;
                    }
                    this.pageCount = pageTotNumtemp;
                    this.alPrintData = al;

                    //  this.alPrintData = typeList;

                    this.printBill = printBill;

                    //   this.drugType = con.Name;

                    if (this.Print() == -1)
                    {
                        return -1;
                    }
                    return 1;
                }
                else
                {
                    return 0;
                }
        }

        /// <summary>
        /// 2打印函数
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
            
            //补打的时候可能有多张单据，分开
            System.Collections.Hashtable hs = new Hashtable();
      
            foreach (FS.HISFC.Models.Pharmacy.Input input in alPrintData)
            {
                FS.HISFC.Models.Pharmacy.Input i = input.Clone();
                if (hs.Contains(i.InListNO))
                {

                    ArrayList al = (ArrayList)hs[i.InListNO];
                    al.Add(i);

                    if (hsTotCost.Contains(i.InListNO))
                    {
                        TotCost tc = (TotCost)hsTotCost[i.InListNO];
                        tc.purchaseCost += i.PurchaseCost;
                        tc.retailCost += i.RetailCost;
                    }
                    else
                    {
                        TotCost tc = new TotCost();
                        tc.purchaseCost = i.PurchaseCost;
                        tc.retailCost = i.RetailCost;
                        hsTotCost.Add(i.InListNO, tc);
                    }
                }
                else
                {
                    ArrayList al = new ArrayList();
                    al.Add(i);
                    hs.Add(i.InListNO, al);

                    if (hsTotCost.Contains(i.InListNO))
                    {
                        TotCost tc = (TotCost)hsTotCost[i.InListNO];
                        tc.purchaseCost += i.PurchaseCost;
                        tc.retailCost += i.RetailCost;
                    }
                    else
                    {
                        TotCost tc = new TotCost();
                        tc.purchaseCost = i.PurchaseCost;
                        tc.retailCost = i.RetailCost;
                        hsTotCost.Add(i.InListNO, tc);
                    }
                }
            }

            //分单据打印
            foreach (ArrayList alPrintList in hs.Values)
            {
                int pageTotNum = alPrintList.Count / this.printBill.RowCount;
                if (alPrintList.Count != this.printBill.RowCount * pageTotNum)
                {
                    pageTotNum++;
                }

                ArrayList alPrint = new ArrayList();
                foreach (FS.HISFC.Models.Pharmacy.Input item in alPrintList)
                {
                    alPrint.Add(item);
                }
                //if (this.printBill.Sort == FS.SOC.Local.Pharmacy.Base.PrintBill.SortType.货位号)
                //{
                //    Base.PrintBill.SortByPlaceNO(ref alPrint);
                //}
                //else if (this.printBill.Sort == FS.SOC.Local.Pharmacy.Base.PrintBill.SortType.物理顺序)
                //{
                //    Base.PrintBill.SortByBillNO(ref alPrint);
                //}
                //else
                //{
                //    Base.PrintBill.SortByCustomerCode(ref alPrint);
  
                //}
                

                //分页打印
                for (int pageNow = 0; pageNow < pageTotNum; pageNow++)
                {
                    if (pageNow == pageTotNum - 1)
                    {
                        this.lblSignOper1.Visible = true;
                        this.lblSignOper2.Visible = true;
                        this.lblSignOper3.Visible = true;
                    }
                    else
                    {
                        this.lblSignOper1.Visible = false;
                        this.lblSignOper2.Visible = false;
                        this.lblSignOper3.Visible = false;
                    }
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

        /// <summary>
        /// 3赋值
        /// </summary>
        /// <param name="al">打印数组</param>
        /// <param name="i">第几页</param>
        /// <param name="count">总页数</param>
        /// <param name="title">标题</param>
        private void SetPrintData(ArrayList al, int inow, int icount, string title)
        {
            this.lblTitle.Text = "{0}药品入库单({1})";
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
                this.lblTitle.Text = string.Format(this.lblTitle.Text, FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.StockDept.ID), drugType);
            }
            else
            {
                if (title.IndexOf("[库存科室]") != -1)
                {
                    title = title.Replace("[库存科室]", FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.StockDept.ID));
                }
                if (title.IndexOf("[药品类型]") != -1)
                {
                    string tmpDrugType = "(" + this.drugType + ")";
                    this.lblTitle.Text = title.Replace("[药品类型]", tmpDrugType);
                }
                this.lblTitle.Text = title;
            }

            if (this.lblTitle.Text.IndexOf("(") > 0)
            {
                this.lblTitle.Text = this.lblTitle.Text.Substring(0, this.lblTitle.Text.IndexOf("("));
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
            this.lblSignOper1.Text = "制表人:" + FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(info.Operation.ApplyOper.ID);
            this.lblPage.Text = "页：" + curPage.ToString() + "/" + pageCount.ToString();
            //只有最后一页才显示合计金额
            if (curPage != 1)
            {
                this.lblTotPurCost.Visible = false;
                this.lblTotRetailCost.Visible = false;
                this.lblTotDif.Visible = false;

                this.neuLabel99.Visible = false;
                this.neuLabel1.Visible = false;
                this.neuLabel2.Visible = false;
            }
            else
            {
                this.lblTotPurCost.Visible = true;
                this.lblTotRetailCost.Visible = true;
                //this.lblTotDif.Visible = true;

                this.neuLabel99.Visible = true;
                this.neuLabel1.Visible = true;
                //this.neuLabel2.Visible = true;
            }
            curPage = curPage + 1;

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

            this.neuFpEnter1_Sheet1.RowCount = 0;
            for (int i = 0; i < al.Count; i++)
            {
                this.neuFpEnter1_Sheet1.AddRows(i, 1);
                FS.HISFC.Models.Pharmacy.Input input = al[i] as FS.HISFC.Models.Pharmacy.Input;
                this.neuFpEnter1_Sheet1.Cells[i, 0].Text = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemCustomNO(input.Item.ID); //药品自定义码
                this.neuFpEnter1_Sheet1.Cells[i, 1].Text = input.Item.Name;//药品名称
                this.neuFpEnter1_Sheet1.Cells[i, 2].Text = input.Item.Specs;//规格		
                if (input.Item.PackQty == 0)
                    input.Item.PackQty = 1;
                decimal count = 0;
                count = input.Quantity;

                FarPoint.Win.Spread.CellType.NumberCellType n = new FarPoint.Win.Spread.CellType.NumberCellType();
                FarPoint.Win.Spread.CellType.NumberCellType m = new FarPoint.Win.Spread.CellType.NumberCellType();
                n.DecimalPlaces = 4;
                m.DecimalPlaces = 2;
                this.neuFpEnter1_Sheet1.Columns[6].CellType = n;
                this.neuFpEnter1_Sheet1.Columns[7].CellType = m;
                this.neuFpEnter1_Sheet1.Columns[8].CellType = n;
                this.neuFpEnter1_Sheet1.Columns[9].CellType = m;

                if (input.ShowState == "0")
                {

                    this.neuFpEnter1_Sheet1.Cells[i, 3].Text = input.Item.MinUnit;//单位
                    this.neuFpEnter1_Sheet1.Cells[i, 4].Text = (count).ToString();//数量				
                    this.neuFpEnter1_Sheet1.Cells[i, 6].Value = (input.Item.PriceCollection.PurchasePrice / input.Item.PackQty).ToString("F4");
                    this.neuFpEnter1_Sheet1.Cells[i, 8].Value = (input.Item.PriceCollection.RetailPrice / input.Item.PackQty).ToString("F4");

                }
                else
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 3].Text = input.Item.PackUnit;//单位
                    this.neuFpEnter1_Sheet1.Cells[i, 4].Text = (count / input.Item.PackQty).ToString("F2");//数量			


                    this.neuFpEnter1_Sheet1.Cells[i, 6].Value = input.Item.PriceCollection.PurchasePrice;
                    this.neuFpEnter1_Sheet1.Cells[i, 8].Value = input.Item.PriceCollection.RetailPrice;
                }

                //try
                //{
                //    this.neuFpEnter1_Sheet1.Cells[i, 5].Text = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(input.Item.ID).Product.ProducingArea;
                //}
                //catch { }

                //if (input.ValidTime > DateTime.MinValue)
                //{
                this.neuFpEnter1_Sheet1.Cells[i, 5].Text = input.BatchNO;//input.ValidTime.Date.ToString();
                //}
                this.neuFpEnter1_Sheet1.Cells[i, 7].Value = input.PurchaseCost.ToString("F2");
                this.neuFpEnter1_Sheet1.Cells[i, 9].Value = input.RetailCost.ToString("F2");

                //this.neuFpEnter1_Sheet1.Cells[i, 10].Value = input.ValidTime.ToShortDateString();
                //this.neuFpEnter1_Sheet1.Cells[i, 11].Value = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetProducerName(input.Producer.ID);
                this.neuFpEnter1_Sheet1.Cells[i, 10].Text = (FS.FrameWork.Function.NConvert.ToDecimal(input.StoreQty) / FS.FrameWork.Function.NConvert.ToDecimal(input.Item.PackQty)).ToString("F0");

                switch (input.Item.Type.ID)
                {
                    case "B":
                        sumBM += input.RetailCost;
                        break;
                    case "M":
                        sumM += input.PurchaseCost;
                        break;
                    case "C":
                    case "P":
                    case "Z":
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
            this.lblCurRet.Text = "本页零售金额:" + sumRetail.ToString("F2");
            this.lblCurPur.Text = "本页购入金额:" + sumPurchase.ToString("F2");
            this.lblCurDif.Text = "本页购零差:" + sumDif.ToString("F2");

            //总数据
            this.lblTotPurCost.Text = ((TotCost)hsTotCost[info.InListNO]).purchaseCost.ToString("F2");
            this.lblTotRetailCost.Text = ((TotCost)hsTotCost[info.InListNO]).retailCost.ToString("F2");
            this.lblTotDif.Text = (((TotCost)hsTotCost[info.InListNO]).retailCost - ((TotCost)hsTotCost[info.InListNO]).purchaseCost).ToString("F2");

            #endregion

            this.resetTitleLocation();

        }

        public int Preview()
        {
            this.printBill.IsNeedPreview = true;
            return this.Print();
        }

     
    }
}
