using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.SOC.Local.Pharmacy.Print.GYSY;
using FS.FrameWork.WinForms.Controls;

namespace FS.SOC.Local.Pharmacy.Print.GYSY
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

        FS.SOC.HISFC.BizLogic.Pharmacy.InOut item = new FS.SOC.HISFC.BizLogic.Pharmacy.InOut();
        FS.HISFC.BizLogic.Manager.PowerLevelManager powerLevelManager = new FS.HISFC.BizLogic.Manager.PowerLevelManager();
        /// <summary>
        /// 常数管理类
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

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
            FS.HISFC.Models.Pharmacy.Input info = (FS.HISFC.Models.Pharmacy.Input)al[0];

            #region label赋值
            if (string.IsNullOrEmpty(title))
            {
                this.lblTitle.Text = string.Format(this.lblTitle.Text, FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.StockDept.ID));
            }
            else
            {

                FS.HISFC.Models.Admin.PowerLevelClass3 privClass3 = powerLevelManager.LoadLevel3ByPrimaryKey("0310", info.PrivType);

                title = title.Replace("[库存科室]", FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.StockDept.ID));
                if (privClass3 != null)
                {
                    title = title.Replace("[操作类型]", privClass3.Class3Name);
                }

                this.lblTitle.Text = title;
            }

            this.lblCompany.Text = "供货公司: (" + info.Company.ID + ")" + FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetCompanyName(info.Company.ID);
            this.lblBillID.Text = "单据号: " + info.InListNO;
            this.lblInputDate.Text = "入库日期: " + info.InDate.ToShortDateString();
            this.lblOper.Text = "制单:" + FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(info.Operation.ExamOper.ID);

            lblStorageDept.Text = "入库科室: (" + info.StockDept.ID + ")" +FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.StockDept.ID);
            lblInvoiceNO.Text = "发票号码：" + info.InvoiceNO;
            
            lblPrintDate.Text = "打印日期： "+System.DateTime.Now.ToShortDateString();
            this.lblPage.Text = "页：" + inow.ToString() + "/" + icount.ToString();
          
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


            this.neuFpEnter1_Sheet1.Columns[2].CellType = nQty;
            this.neuFpEnter1_Sheet1.Columns[4].CellType = nPrice;
            this.neuFpEnter1_Sheet1.Columns[5].CellType = nPrice;
            this.neuFpEnter1_Sheet1.Columns[6].CellType = nCost;
            this.neuFpEnter1_Sheet1.Columns[8].CellType = nCost;
            this.neuFpEnter1_Sheet1.Columns[7].CellType = nCost;

            for (int i = 0; i < al.Count; i++)
            {
                this.neuFpEnter1_Sheet1.AddRows(i, 1);


                FS.HISFC.Models.Pharmacy.Input output = al[i] as FS.HISFC.Models.Pharmacy.Input;
                item.GetStorageNum(output.StockDept.ID, output.Item.ID, out  storageSum);

                FS.HISFC.Models.Pharmacy.Item itemObj = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(output.Item.ID);
                if (ctrlIntegrate.GetControlParam<bool>("HNPHA2", false, true))
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 0].Value = "(" + itemObj.UserCode + ")" + itemObj.NameCollection.RegularName; //药品自定义码                    
                }
                else
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 0].Value = "(" + itemObj.UserCode + ")" + itemObj.Name; //药品自定义码                    
                }


                //this.neuFpEnter1_Sheet1.Cells[i, 0].Value = "(" + FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(output.Item.ID).UserCode + ")" + output.Item.Name; //药品自定义码
                this.neuFpEnter1_Sheet1.Cells[i, 1].Value = output.Item.Specs;//规格	

                if (output.Item.PackQty == 0)
                {
                    output.Item.PackQty = 1;
                }
                decimal count = output.Quantity;
                
                if (output.ShowState=="0")
                {

                    this.neuFpEnter1_Sheet1.Cells[i, 2].Value = (count).ToString();//数量	
                    this.neuFpEnter1_Sheet1.Cells[i, 3].Value = output.Item.MinUnit;//单位
                    this.neuFpEnter1_Sheet1.Cells[i, 4].Value = output.PriceCollection.RetailPrice / output.Item.PackQty;
                    this.neuFpEnter1_Sheet1.Cells[i, 5].Value = output.PriceCollection.PurchasePrice / output.Item.PackQty;
                }
                else
                {

                    this.neuFpEnter1_Sheet1.Cells[i, 2].Value = (count / output.Item.PackQty).ToString("F2");//数量				
                    this.neuFpEnter1_Sheet1.Cells[i, 3].Value = output.Item.PackUnit;//单位
                    this.neuFpEnter1_Sheet1.Cells[i, 4].Value = output.PriceCollection.RetailPrice;
                    this.neuFpEnter1_Sheet1.Cells[i, 5].Value = output.PriceCollection.PurchasePrice;          
                }

                this.neuFpEnter1_Sheet1.Cells[i, 6].Value = output.RetailCost.ToString(Function.GetCostDecimalString());
                this.neuFpEnter1_Sheet1.Cells[i, 7].Value = output.PurchaseCost.ToString(Function.GetCostDecimalString());// output.ValidTime.Date.ToString();

                this.neuFpEnter1_Sheet1.Cells[i, 8].Value = FS.FrameWork.Public.String.FormatNumber(output.RetailCost, Function.GetCostDecimal()) - FS.FrameWork.Public.String.FormatNumber(output.PurchaseCost, Function.GetCostDecimal());

                sumRetail = sumRetail + output.RetailCost;
                sumPurchase += output.PurchaseCost;
                sumDif = sumRetail - sumPurchase;
            }


            #region 汇总页
            if (inow == icount)
            {
                int rowCount = this.neuFpEnter1_Sheet1.RowCount;
                this.neuFpEnter1_Sheet1.AddRows(rowCount, 1);
                this.neuFpEnter1_Sheet1.Cells[rowCount, 0].Value = "合计";
                this.neuFpEnter1_Sheet1.Cells[rowCount, 6].Value = ((TotCost)hsTotCost[info.InListNO]).retailCost.ToString(Function.GetCostDecimalString());
                this.neuFpEnter1_Sheet1.Cells[rowCount, 7].Value = ((TotCost)hsTotCost[info.InListNO]).purchaseCost.ToString(Function.GetCostDecimalString());
                this.neuFpEnter1_Sheet1.Cells[rowCount, 8].Value = (((TotCost)hsTotCost[info.InListNO]).retailCost - ((TotCost)hsTotCost[info.InListNO]).purchaseCost).ToString(Function.GetCostDecimalString());
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

            if (this.printBill.Sort == FS.SOC.Local.Pharmacy.Base.PrintBill.SortType.货位号)
            {
                Base.PrintBill.SortByPlaceNO(ref alPrintData);
            }
            else if (this.printBill.Sort == FS.SOC.Local.Pharmacy.Base.PrintBill.SortType.物理顺序)
            {
                Base.PrintBill.SortByCustomerCode(ref alPrintData);
            }

            //补打的时候可能有多张单据，分开
            System.Collections.Hashtable hs = new Hashtable();
            foreach (FS.HISFC.Models.Pharmacy.Input output in alPrintData)
            {
                FS.HISFC.Models.Pharmacy.Input o = output.Clone();
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
                string bill = (alPrintData[0] as FS.HISFC.Models.Pharmacy.Input).InListNO;
                string dept = (alPrintData[0] as FS.HISFC.Models.Pharmacy.Input).StockDept.ID;
                FS.SOC.HISFC.BizLogic.Pharmacy.InOut itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.InOut();
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

        private void neuPanel3_Paint(object sender, PaintEventArgs e)
        {

        }


        #endregion
    }
}
