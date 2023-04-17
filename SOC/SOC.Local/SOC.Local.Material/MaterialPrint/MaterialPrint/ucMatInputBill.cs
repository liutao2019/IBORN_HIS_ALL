using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.WinForms.Controls;

namespace SOC.Local.MaterialPrint
{
    public partial class ucMatInputBill : UserControl, FS.HISFC.Interface.Material.Print.IBillPrint
    {

        FS.HISFC.InterfaceArchieve.Material.Manager.ManagerAchieve matManager = new FS.HISFC.InterfaceArchieve.Material.Manager.ManagerAchieve();

        public ucMatInputBill()
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
        int totRows = 0;
        int pageNumber = 0;

        TotCost totCosetAll = new TotCost(); //新加
          
              

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
            FS.HISFC.BizLogic.Manager.Person personManager = new FS.HISFC.BizLogic.Manager.Person();
            FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
            //FS.HISFC.BizLogic.Material.BizLogic.Store.
            if (al.Count <= 0)
            {
                MessageBox.Show("没有打印的数据!");
                return;
            }
            if (icount <= 0)
            {
                icount = 1;
                inow = 1;
            }
            else
            {
                icount = this.totRows;
                inow = this.pageNumber;
            }
            FS.HISFC.BizLogic.Material.Object.Input info = (FS.HISFC.BizLogic.Material.Object.Input)al[0];

            #region label赋值
            if (string.IsNullOrEmpty(title))
            {
                this.lblTitle.Text = string.Format(this.lblTitle.Text, deptManager.GetDeptmentById(info.StockInfo.Storage.ID).Name);
            }
            else
            {
                if (title.IndexOf("[库存科室]") != -1)
                {


                    this.lblTitle.Text = title.Replace("[库存科室]", deptManager.GetDeptmentById(info.StockInfo.Storage.ID).Name);
                    
                }
                else
                {
                    this.lblTitle.Text = title; 
                }
            }
            string titleIn = this.matManager.GetControlParam<string>("MATS31", true, "");
            this.lblTitle.Text =  titleIn  + deptManager.GetDeptmentById(info.StockInfo.Storage.ID).Name + "入库单" ; //本地化

            string company = "";
            this.lblCompany.Text = "送货单位:" + info.StockInfo.Company.ID + " " + info.StockInfo.Company.Name;
            
            this.lblBillID.Text = "进仓号:" + info.InListCode;
            this.lblInputDate.Text = "进仓日期:" + info.InDate.ToShortDateString();
            this.lblOper.Text = "制表人:" + personManager.GetPersonByID(FS.FrameWork.Management.Connection.Operator.ID).Name;
            this.neuLabel10.Text = "仓管员签名"  ;
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

            this.neuFpEnter1_Sheet1.RowCount = 0;
            for (int i = 0; i < al.Count; i++)
            {
                this.neuFpEnter1_Sheet1.AddRows(i, 1);
                //FS.HISFC.Models.Pharmacy.Input input = al[i] as FS.HISFC.Models.Pharmacy.Input;
                FS.HISFC.BizLogic.Material.Object.Input input = al[i] as FS.HISFC.BizLogic.Material.Object.Input;
                this.neuFpEnter1_Sheet1.Cells[i, 0].Text = input.StockInfo.BaseInfo.UserCode;
                this.neuFpEnter1_Sheet1.Cells[i, 1].Text = input.StockInfo.BaseInfo.Name;//药品名称
                this.neuFpEnter1_Sheet1.Cells[i, 2].Text = input.StockInfo.BaseInfo.Specs;//规格		
                if (input.StockInfo.BaseInfo.PackQty == 0)
                    input.StockInfo.BaseInfo.PackQty = 1;
                decimal count = 0;
                count = input.InNum;

                FarPoint.Win.Spread.CellType.NumberCellType n = new FarPoint.Win.Spread.CellType.NumberCellType();
                n.DecimalPlaces = 3;
                this.neuFpEnter1_Sheet1.Columns[7].CellType = n;
                this.neuFpEnter1_Sheet1.Columns[8].CellType = n;
                this.neuFpEnter1_Sheet1.Columns[9].CellType = n;

                this.neuFpEnter1_Sheet1.Cells[i, 3].Text = input.StockInfo.MinUnit;//单位
                this.neuFpEnter1_Sheet1.Cells[i, 4].Text = input.InNum.ToString();//数量			


                this.neuFpEnter1_Sheet1.Cells[i, 7].Value = input.InPrice;
                this.neuFpEnter1_Sheet1.Cells[i, 9].Value = input.InSaleCost;

                try
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 5].Text = input.StockInfo.BaseInfo.Factory.Name;
                }
                catch { }

                if (input.StockInfo.ValidDate > DateTime.MinValue)
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 6].Text = input.StockInfo.ValidDate.ToString("yyyy-MM-dd");
                }
                this.neuFpEnter1_Sheet1.Cells[i, 8].Value = input.InNum * input.InPrice;
                sumPCZret += input.InSaleCost;
                sumPCZpur += input.InPrice * input.InNum;

                sumRetail = sumRetail + input.InSaleCost;
                sumPurchase = sumPurchase + input.InNum * input.InPrice;
                sumDif = sumRetail - sumPurchase;

          }
            //当前页数据
            this.lblCurRet.Text = "本页零售金额:"+sumRetail.ToString("F2");
            this.lblCurPur.Text = "本页购入金额:" + sumPurchase.ToString("F2");
            this.lblCurDif.Text = "本页购零差:" + sumDif.ToString("F2");
         
            //总数据
           //this.lblTotPurCost.Text = ((TotCost)hsTotCost[info.InListCode]).purchaseCost.ToString("F2"); //总金额
            //this.lblTotRetailCost.Text = ((TotCost)hsTotCost[info.InListCode]).retailCost.ToString("F2"); //总零价
            //this.lblTotDif.Text = (((TotCost)hsTotCost[info.InListCode]).retailCost - ((TotCost)hsTotCost[info.InListCode]).purchaseCost).ToString("F4"); //总差额
            this.lblTotPurCost.Text =  totCosetAll.purchaseCost.ToString("F2"); //总金额
            this.lblTotRetailCost.Text = totCosetAll.retailCost.ToString("F2"); //总零价
            this.lblTotDif.Text = (totCosetAll.retailCost -totCosetAll.purchaseCost).ToString("F4"); //总差额
    
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

        private PrintBill printBill = new PrintBill();

        /// <summary>
        /// IBillPrint成员Print
        /// </summary>
        /// <returns></returns>
        public int Print()
        {
            #region 打印信息设置
            int pageHeight = this.matManager.GetControlParam<int>("MATS33", true, 100); //纸张高度

            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.IsDataAutoExtend = false;
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;
            p.IsHaveGrid = true;
            this.printBill.PageSize.HeightMM = pageHeight;
            p.SetPageSize(this.printBill.PageSize);
            #endregion

            #region 分页打印
            int height = this.neuPanel5.Height;
            int ucHeight = this.Height;
            float rowHeight = this.neuFpEnter1_Sheet1.Rows[0].Height;
            this.neuFpEnter1_Sheet1.Columns[0].CellType = new FarPoint.Win.Spread.CellType.TextCellType();


            //补打的时候可能有多张单据，分开
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
                    tc.purchaseCost += i.InNum * i.InPrice;
                    tc.retailCost += i.InSaleCost;
                }
                else
                {
                    ArrayList al = new ArrayList();
                    al.Add(i);
                    hs.Add(i.InListCode, al);

                    TotCost tc = new TotCost();
                    tc.purchaseCost = i.InNum * i.InPrice;
                    tc.retailCost = i.InSaleCost;
                    hsTotCost.Add(input.InListCode, tc);
                }
            }

            //分单据打印
            foreach (ArrayList alPrint in hs.Values)
            {
                int pageTotNum = alPrint.Count / this.PrintRowCount;
                if (alPrint.Count != this.PrintRowCount * pageTotNum)
                {
                    pageTotNum++;
                }

                //分页打印
                for (int pageNow = 0; pageNow < pageTotNum; pageNow++)
                {
                    ArrayList al = new ArrayList();

                    for (int index = pageNow * this.PrintRowCount; index < alPrint.Count && index < (pageNow + 1) * this.PrintRowCount; index++)
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

        public int SetPrintData(ArrayList alPrintData, PrintBill printBill)
        {
            //if (alPrintData != null && alPrintData.Count > 0)
            //{
            //    string bill = (alPrintData[0] as FS.HISFC.Models.Pharmacy.Input).InListNO;
            //    string dept = (alPrintData[0] as FS.HISFC.Models.Pharmacy.Input).StockDept.ID;
            //    //FS.HISFC.BizLogic.Pharmacy.Item itemMgr = new FS.HISFC.BizLogic.Pharmacy.Item();
            //    //ArrayList al = itemMgr.QueryInputInfoByListID(dept, bill, "AAAA", "AAAA");
            //    this.alPrintData = al;
            //}
            ////this.alPrintData = alPrintData;
            //this.printBill = printBill;
            //return this.Print();
            return 1;
        }

        public int Preview()
        {
            this.printBill.IsNeedPreview = true;
            return this.Print();
        }

        #endregion



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

        public int Prieview()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 打印行数
        /// </summary>
        public int PrintRowCount
        {
            get 
            {
                return this.matManager.GetControlParam<int>("MATS32", true, 8); 
            }
        }

        public int SetPrintData(List<FS.FrameWork.Models.NeuObject> DataList, int pageCount, int totPageCount)
        {
            
            if (DataList != null && DataList.Count > 0)
            {
                ArrayList al = new ArrayList();
                //string bill = (alPrintData[0] as FS.HISFC.BizLogic.Material.Object.Input).InListCode;
                //string dept = (alPrintData[0] as FS.HISFC.BizLogic.Material.Object.Input).StockInfo.Storage.ID;
                foreach (FS.HISFC.BizLogic.Material.Object.Input input in DataList)
                {
                    al.Add(input);
                }
                //FS.HISFC.BizLogic.Pharmacy.Item itemMgr = new FS.HISFC.BizLogic.Pharmacy.Item();
                //ArrayList al = itemMgr.QueryInputInfoByListID(dept, bill, "AAAA", "AAAA");
                this.alPrintData = al;
                this.totRows = totPageCount;
                this.pageNumber = pageCount;
            }
            //this.alPrintData = alPrintData;
          //  this.printBill = printBill;
             return 1;
          //   return this.Print();
        }
        public int SetPrintDataTotal(List<FS.FrameWork.Models.NeuObject> DataList)
        {
            totCosetAll.purchaseCost =0 ;
            totCosetAll.retailCost = 0;
            if (DataList != null && DataList.Count > 0)
            {
                foreach (FS.HISFC.BizLogic.Material.Object.Input inPut in DataList)
                {
                    totCosetAll.purchaseCost += inPut.InNum * inPut.InPrice;
                    totCosetAll.retailCost += inPut.InSaleCost ;
                }
            }
            return 1;
        }

        #endregion

    }
}
