using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.WinForms.Controls;

namespace FS.SOC.Local.Material.Print
{
    public partial class ucMatInputBill : UserControl, FS.HISFC.Interface.Material.Print.IBillPrint
    {
        FS.HISFC.InterfaceArchieve.Material.Manager.ManagerAchieve matManager = new FS.HISFC.InterfaceArchieve.Material.Manager.ManagerAchieve();
        FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
        FS.HISFC.BizLogic.Manager.Person personManager = new FS.HISFC.BizLogic.Manager.Person();
            
        public ucMatInputBill()
        {
            InitializeComponent();
        }

        private Hashtable hsTotCost = new Hashtable();

        private class TotCost
        {
            public decimal purchaseCost;
            public decimal retailCost;
        }

        TotCost totCosetAll = new TotCost();

        ArrayList alPrintData = new ArrayList();

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

        public int Prieview()
        {
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
            FarPoint.Win.Spread.CellType.NumberCellType nct = new FarPoint.Win.Spread.CellType.NumberCellType();
            nct.DecimalPlaces = 3;
            this.neuFpEnter1_Sheet1.Columns[7].CellType = nct;
            this.neuFpEnter1_Sheet1.Columns[8].CellType = nct;
            this.neuFpEnter1_Sheet1.Columns[9].CellType = nct;

            decimal sumRetail = 0;
            decimal sumPurchase = 0;
            decimal sumDif = 0;
            decimal sumDrugWholeCost = 0;
            decimal sumDrugPurCost = 0;

            if (DataList != null && DataList.Count > 0)
            {
                string titleIn = this.matManager.GetControlParam<string>("MATS31", true, "");
                FS.HISFC.BizLogic.Material.Object.Input info = DataList[0] as FS.HISFC.BizLogic.Material.Object.Input;
                this.lblTitle.Text = titleIn + deptManager.GetDeptmentById(info.StockInfo.Storage.ID).Name + "入库单"; //本地化

                string company = "";
                this.lblCompany.Text = "供货公司:" + info.StockInfo.Company.ID + " " + info.StockInfo.Company.Name;

                this.lblBillID.Text = "入库单号:" + info.InListCode;
                this.lblInputDate.Text = "入库日期:" + info.InDate.ToShortDateString();
                this.lblOper.Text = "制表人:" + personManager.GetPersonByID(FS.FrameWork.Management.Connection.Operator.ID).Name;
                this.neuLabel10.Text = "仓管员签名";
                this.lblPage.Text = "页:" + pageCount.ToString() + "/" + totPageCount.ToString();

                int i = 0;
                alPrintData = new ArrayList();
                this.neuFpEnter1_Sheet1.RowCount = 0;
                foreach (FS.HISFC.BizLogic.Material.Object.Input input in DataList)
                {
                    this.alPrintData.Add(input);
                    this.neuFpEnter1_Sheet1.AddRows(i, 1);
                    this.neuFpEnter1_Sheet1.Cells[i, 0].Text = input.StockInfo.BaseInfo.UserCode; //自定义码
                    this.neuFpEnter1_Sheet1.Cells[i, 1].Text = input.StockInfo.BaseInfo.Name;//名称
                    this.neuFpEnter1_Sheet1.Cells[i, 2].Text = input.StockInfo.BaseInfo.Specs;//规格		
                    if (input.StockInfo.BaseInfo.PackQty == 0)
                        input.StockInfo.BaseInfo.PackQty = 1;

                    this.neuFpEnter1_Sheet1.Cells[i, 3].Text = input.StockInfo.MinUnit;//单位
                    this.neuFpEnter1_Sheet1.Cells[i, 4].Text = input.InNum.ToString();//数量			

                    try
                    {
                        this.neuFpEnter1_Sheet1.Cells[i, 5].Text = input.StockInfo.BaseInfo.Factory.Name; //生产厂家
                    }
                    catch { }

                    if (input.StockInfo.ValidDate > DateTime.MinValue)
                    {
                        this.neuFpEnter1_Sheet1.Cells[i, 6].Text = input.StockInfo.ValidDate.ToString("yyyy-MM-dd");//有效期
                    }
                    this.neuFpEnter1_Sheet1.Cells[i, 7].Value = input.InPrice;  //购入单价
                    this.neuFpEnter1_Sheet1.Cells[i, 8].Value = input.InNum * input.InPrice / input.StockInfo.BaseInfo.PackQty;//购入金额
                    this.neuFpEnter1_Sheet1.Cells[i, 9].Value = input.InSaleCost; //零售金额

                    sumRetail = sumRetail + input.InSaleCost;
                    sumPurchase = sumPurchase + input.InNum * input.InPrice / input.StockInfo.BaseInfo.PackQty;
                    sumDif = sumRetail - sumPurchase;
                    i++;
                }

                this.lblCurRet.Text = "本页零售金额:" + sumRetail.ToString("F2");
                this.lblCurPur.Text = "本页购入金额:" + sumPurchase.ToString("F2");
                this.lblCurDif.Text = "本页购零差:" + sumDif.ToString("F2");

                this.lblTotPurCost.Text = totCosetAll.purchaseCost.ToString("F2"); //总金额
                this.lblTotRetailCost.Text = totCosetAll.retailCost.ToString("F2"); //总零价
                this.lblTotDif.Text = (totCosetAll.retailCost - totCosetAll.purchaseCost).ToString("F4"); //总差额

                resetTitleLocation();
            }
            return 1;
        }

        public int SetPrintDataTotal(List<FS.FrameWork.Models.NeuObject> DataList)
        {
            totCosetAll.purchaseCost =0 ;
            totCosetAll.retailCost = 0;
            if (DataList != null && DataList.Count > 0)
            {
                foreach (FS.HISFC.BizLogic.Material.Object.Input inPut in DataList)
                {
                    totCosetAll.purchaseCost += inPut.InNum * inPut.InPrice/inPut.StockInfo.PackQty;
                    totCosetAll.retailCost += inPut.InSaleCost ;
                }
            }
            return 1;
        }

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

        public int Print()
        {
            int pageHeight = this.matManager.GetControlParam<int>("MATS33", true, 100); //纸张高度

            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.IsDataAutoExtend = false;
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;
            p.IsHaveGrid = true;
            p.SetPageSize(new FS.HISFC.Models.Base.PageSize("1234",850,pageHeight));
            
            int height = this.neuPanel5.Height;
            int ucHeight = this.Height;
            float rowHeight = this.neuFpEnter1_Sheet1.Rows[0].Height;
            this.neuFpEnter1_Sheet1.Columns[0].CellType = new FarPoint.Win.Spread.CellType.TextCellType();

            System.Collections.Hashtable hs = new Hashtable();
            this.hsTotCost.Clear();
            foreach (FS.HISFC.BizLogic.Material.Object.Input input in alPrintData)
            {
                FS.HISFC.BizLogic.Material.Object.Input i = input.Clone();
                if (hs.Contains(i.InListCode))
                {
                    ArrayList al = (ArrayList)hs[i.InListCode];
                    al.Add(i);

                    TotCost tc = (TotCost)hsTotCost[i.InListCode];
                    tc.purchaseCost += i.InNum * i.InPrice/i.StockInfo.BaseInfo.PackQty;
                    tc.retailCost += i.InSaleCost;
                }
                else
                {
                    ArrayList al = new ArrayList();
                    al.Add(i);
                    hs.Add(i.InListCode, al);

                    TotCost tc = new TotCost();
                    tc.purchaseCost = i.InNum * i.InPrice/i.StockInfo.BaseInfo.PackQty;
                    tc.retailCost = i.InSaleCost;
                    hsTotCost.Add(input.InListCode, tc);
                }
            }

            foreach (ArrayList alPrint in hs.Values)
            {
                int pageTotNum = (int)Math.Ceiling((double)alPrint.Count / this.PrintRowCount);

                //分页打印
                for (int pageNow = 0; pageNow < pageTotNum; pageNow++)
                {
                    List<FS.FrameWork.Models.NeuObject> al = new List<FS.FrameWork.Models.NeuObject>();

                    for (int index = pageNow * this.PrintRowCount; index < alPrint.Count && index < (pageNow + 1) * this.PrintRowCount; index++)
                    {
                        al.Add(alPrint[index] as FS.FrameWork.Models.NeuObject);
                    }
                   
                    this.SetPrintData(al, pageNow+1, pageTotNum);

                    this.neuPanel5.Height += (int)rowHeight * al.Count;
                    this.Height += (int)rowHeight * al.Count;

                    p.PrintPage(5, 0, this.neuPanel1);

                    this.neuPanel5.Height = height;
                    this.Height = ucHeight;
                }
            }

            return 1;
        }
    }
}
