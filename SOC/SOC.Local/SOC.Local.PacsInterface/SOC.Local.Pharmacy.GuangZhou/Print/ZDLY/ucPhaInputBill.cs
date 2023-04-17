using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Neusoft.FrameWork.WinForms.Controls;

namespace Neusoft.SOC.Local.Pharmacy.Print.ZDLY
{
    /// <summary>
    /// 东莞药品出库单据打印
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

        Neusoft.SOC.HISFC.BizLogic.Pharmacy.InOut item = new Neusoft.SOC.HISFC.BizLogic.Pharmacy.InOut();
        Neusoft.HISFC.BizLogic.Manager.PowerLevelManager powerLevelManager = new Neusoft.HISFC.BizLogic.Manager.PowerLevelManager();
        /// <summary>
        /// 常数管理类
        /// </summary>
        protected Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam ctrlIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// 单据的总金额
        /// </summary>
        private Hashtable hsTotCost = new Hashtable();

        public decimal storageSum;

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
        /// <param name="operCode">标题</param>
        private void SetPrintData(ArrayList al, int inow, int icount, string title)
        {
           
            if (al.Count <= 0)
            {
                MessageBox.Show("没有打印的数据!");
                return;
            }
            Neusoft.HISFC.Models.Pharmacy.Input info = (Neusoft.HISFC.Models.Pharmacy.Input)al[0];

            #region label赋值
            if (string.IsNullOrEmpty(title))
            {
                this.lblTitle.Text = string.Format(this.lblTitle.Text, Neusoft.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.StockDept.ID));
            }
            else
            {

                Neusoft.HISFC.Models.Admin.PowerLevelClass3 privClass3 = powerLevelManager.LoadLevel3ByPrimaryKey("0310", info.PrivType);

                title = title.Replace("[库存科室]", Neusoft.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.StockDept.ID));
                if (privClass3 != null)
                {
                    title = title.Replace("[操作类型]", privClass3.Class3Name);
                }

                this.lblTitle.Text = title;
            }

            this.lblCompany.Text = "供货公司: (" + info.Company.ID + ")" + Neusoft.SOC.HISFC.BizProcess.Cache.Pharmacy.GetCompanyName(info.Company.ID);
            this.lblBillID.Text = "单据号: " + info.InListNO;
            this.lblInputTime.Text = "入库日期: " + info.InDate.ToShortDateString();
            this.lblRecord.Text = "录单:" + Neusoft.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(info.Operation.ApplyOper.ID);
            this.lblOper.Text = "制表:" + Neusoft.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(info.Operation.ApplyOper.ID);

            lblStorageDept.Text = "入库科室: (" + info.StockDept.ID + ")" +Neusoft.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.StockDept.ID);
            
            lblPrintDate.Text = "打印日期： "+System.DateTime.Now.ToShortDateString();
            this.lblPage.Text = "页：" + inow.ToString() + "/" + icount.ToString();

            this.lblTotPurCost.Text = "购入金额：" + ((TotCost)hsTotCost[info.InListNO]).purchaseCost.ToString(Function.GetCostDecimalString());
            this.lblTotRetail.Text = "零售金额：" + ((TotCost)hsTotCost[info.InListNO]).retailCost.ToString(Function.GetCostDecimalString());

            this.lblTotDiff.Text = "进销差额：" + (((TotCost)hsTotCost[info.InListNO]).retailCost - 
                ((TotCost)hsTotCost[info.InListNO]).purchaseCost).ToString(Function.GetCostDecimalString());
          
            #endregion

            #region farpoint赋值
            decimal sumRetail = 0;
            decimal sumPurchase = 0;
            decimal sumDif = 0;


            this.neuFpEnter1_Sheet1.RowCount = 0;


            FarPoint.Win.Spread.CellType.NumberCellType nPrice = new FarPoint.Win.Spread.CellType.NumberCellType();
            nPrice.DecimalPlaces = Function.GetPriceDecimal();
            FarPoint.Win.Spread.CellType.NumberCellType nCost = new FarPoint.Win.Spread.CellType.NumberCellType();
            nCost.DecimalPlaces = Function.GetCostDecimal();
            FarPoint.Win.Spread.CellType.NumberCellType nQty = new FarPoint.Win.Spread.CellType.NumberCellType();
            nQty.DecimalPlaces = Function.GetQtyDecimal();

            this.neuFpEnter1_Sheet1.Columns[4].CellType = nQty;
            this.neuFpEnter1_Sheet1.Columns[6].CellType = nPrice;
            this.neuFpEnter1_Sheet1.Columns[7].CellType = nPrice;
            this.neuFpEnter1_Sheet1.Columns[8].CellType = nCost;
            this.neuFpEnter1_Sheet1.Columns[9].CellType = nCost;

            for (int i = 0; i < al.Count; i++)
            {
                this.neuFpEnter1_Sheet1.AddRows(i, 1);


                Neusoft.HISFC.Models.Pharmacy.Input input = al[i] as Neusoft.HISFC.Models.Pharmacy.Input;
                item.GetStorageNum(input.StockDept.ID, input.Item.ID, out  storageSum);

                Neusoft.HISFC.Models.Pharmacy.Item itemObj = Neusoft.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(input.Item.ID);

                this.neuFpEnter1_Sheet1.Cells[i, 0].Value = itemObj.UserCode;
                this.neuFpEnter1_Sheet1.Cells[i, 1].Value = itemObj.Name; //药品名称   
                this.neuFpEnter1_Sheet1.Cells[i, 2].Value = itemObj.Specs;//规格	
                this.neuFpEnter1_Sheet1.Cells[i, 3].Value = input.BatchNO;//批号

                if (input.Item.PackQty == 0)
                {
                    input.Item.PackQty = 1;
                }
                decimal count = input.Quantity;
                
                if (input.ShowState=="0")
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 4].Value = (count).ToString();//数量
                    this.neuFpEnter1_Sheet1.Cells[i, 5].Value = input.Item.MinUnit;//单位	 
                    this.neuFpEnter1_Sheet1.Cells[i, 6].Value = input.PriceCollection.PurchasePrice / input.Item.PackQty;//购入价
                    this.neuFpEnter1_Sheet1.Cells[i, 7].Value = input.PriceCollection.RetailPrice / input.Item.PackQty;//零售价
                }
                else
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 4].Value = (count / input.Item.PackQty).ToString("F2");//数量
                    this.neuFpEnter1_Sheet1.Cells[i, 5].Value = input.Item.PackUnit;//单位		
                    this.neuFpEnter1_Sheet1.Cells[i, 6].Value = input.PriceCollection.PurchasePrice.ToString();//购入价
                    this.neuFpEnter1_Sheet1.Cells[i, 7].Value = input.PriceCollection.RetailPrice.ToString();//零售价          
                }

                this.neuFpEnter1_Sheet1.Cells[i, 8].Value = input.PurchaseCost.ToString(Function.GetCostDecimalString());//购入金额
                this.neuFpEnter1_Sheet1.Cells[i, 9].Value = input.RetailCost.ToString(Function.GetCostDecimalString());//零售金额

                this.neuFpEnter1_Sheet1.Cells[i, 10].Value = input.ValidTime.ToShortDateString();//有效期

                sumRetail = sumRetail + input.RetailCost;
                sumPurchase += input.PurchaseCost;
                sumDif = sumRetail - sumPurchase;
            }


            #region 汇总页
            if (inow == icount)
            {
                int rowCount = this.neuFpEnter1_Sheet1.RowCount;
                this.neuFpEnter1_Sheet1.AddRows(rowCount, 1);
                this.neuFpEnter1_Sheet1.Cells[rowCount, 0].Value = "合计";
                this.neuFpEnter1_Sheet1.Cells[rowCount, 8].Value = ((TotCost)hsTotCost[info.InListNO]).purchaseCost.ToString(Function.GetCostDecimalString());
                this.neuFpEnter1_Sheet1.Cells[rowCount, 9].Value = ((TotCost)hsTotCost[info.InListNO]).retailCost.ToString(Function.GetCostDecimalString());
           
            }
            #endregion
           
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
            Neusoft.FrameWork.WinForms.Classes.Print p = new Neusoft.FrameWork.WinForms.Classes.Print();
            p.IsDataAutoExtend = false;
            p.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.Border;
            p.IsHaveGrid = true;
            p.SetPageSize(this.printBill.PageSize);
            #endregion

            #region 分页打印
            int height = this.neuPanel4.Height;
            int ucHeight = this.Height;
            float rowHeight = this.neuFpEnter1_Sheet1.Rows[0].Height;
            this.neuFpEnter1_Sheet1.Columns[0].CellType = new FarPoint.Win.Spread.CellType.TextCellType();

            if (this.printBill.Sort == Neusoft.SOC.Local.Pharmacy.Base.PrintBill.SortType.货位号)
            {
                Base.PrintBill.SortByPlaceNO(ref alPrintData);
            }
            else if (this.printBill.Sort == Neusoft.SOC.Local.Pharmacy.Base.PrintBill.SortType.物理顺序)
            {
                Base.PrintBill.SortByCustomerCode(ref alPrintData);
            }

            //补打的时候可能有多张单据，分开
            System.Collections.Hashtable hs = new Hashtable();
            foreach (Neusoft.HISFC.Models.Pharmacy.Input input in alPrintData)
            {
                Neusoft.HISFC.Models.Pharmacy.Input o = input.Clone();
                if (hs.Contains(o.InListNO))
                {

                    ArrayList al = (ArrayList)hs[o.InListNO];
                    al.Add(o);

                    TotCost tc = (TotCost)hsTotCost[o.InListNO];
                    tc.purchaseCost += o.PurchaseCost;
                    tc.retailCost += o.RetailCost;
                }
                else
                {
                    ArrayList al = new ArrayList();
                    al.Add(o);
                    hs.Add(o.InListNO, al);

                    TotCost tc = new TotCost();
                    tc.purchaseCost = o.PurchaseCost;
                    tc.retailCost = o.RetailCost;
                    hsTotCost.Add(o.InListNO, tc);
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

                
                int fromPage = 0;
                int toPage = 0;
                frmSelectPages frmSelect = new frmSelectPages();
                frmSelect.PageCount = pageTotNum;
                frmSelect.SetPages();
                DialogResult dRsult = frmSelect.ShowDialog();
                if (dRsult == DialogResult.OK)
                {
                    fromPage = frmSelect.FromPage - 1;
                    toPage = frmSelect.ToPage;
                }
                else
                {
                    return 0;
                }

                //分页打印
                for (int pageNow = fromPage; pageNow < toPage; pageNow++)
                {
                    ArrayList al = new ArrayList();

                    for (int index = pageNow * this.printBill.RowCount; index < alPrint.Count && index < (pageNow + 1) * this.printBill.RowCount; index++)
                    {
                        al.Add(alPrint[index]);
                    }
                    this.SetPrintData(al, pageNow + 1, pageTotNum, printBill.Title);

                    if (pageNow + 1 == pageTotNum)
                    {
                        this.neuPanel4.Height += (int)rowHeight * (al.Count+1);
                        this.Height += (int)rowHeight * (al.Count+1);
                    }
                    else
                    {
                        this.neuPanel4.Height += (int)rowHeight * al.Count;
                        this.Height += (int)rowHeight * al.Count;
                    }

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
            
            if (alPrintData != null && alPrintData.Count > 0)
            {
                string bill = (alPrintData[0] as Neusoft.HISFC.Models.Pharmacy.Input).InListNO;
                string dept = (alPrintData[0] as Neusoft.HISFC.Models.Pharmacy.Input).StockDept.ID;
                Neusoft.SOC.HISFC.BizLogic.Pharmacy.InOut itemMgr = new Neusoft.SOC.HISFC.BizLogic.Pharmacy.InOut();
                ArrayList al = itemMgr.QueryInputInfoByListID(dept, bill, "AAAA", "AAAA");
                this.alPrintData = al;
            }
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
