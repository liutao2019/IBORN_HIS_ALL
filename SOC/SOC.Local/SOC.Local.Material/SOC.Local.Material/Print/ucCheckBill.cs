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
    public partial class ucCheckBill : ucBaseControl, FS.HISFC.Interface.Material.Print.IBillPrint
    {
        public ucCheckBill()
        {
            InitializeComponent();
        }

        int rowCount = 0;
        TotCost totCosetAll = new TotCost();
        ArrayList alPrintData = new ArrayList();
        FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
        FS.HISFC.BizLogic.Material.BizLogic.Base.BaseLogic baseLogic = new FS.HISFC.BizLogic.Material.BizLogic.Base.BaseLogic();
       
        #region IBillPrint 成员

        bool FS.HISFC.Interface.Material.Print.IBillPrint.IsRePrint
        {
            get
            {
                return false;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        int FS.HISFC.Interface.Material.Print.IBillPrint.Prieview()
        {
            return 1;
        }

        int FS.HISFC.Interface.Material.Print.IBillPrint.Print()
        {
            #region 打印信息设置
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.IsDataAutoExtend = false;
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;
            p.IsHaveGrid = true;
            //p.SetPageSize(this.printBill.PageSize);
            #endregion

            #region 打印
            int height = this.neuPanel5.Height;
            int ucHeight = this.Height;
            float rowHeight = this.neuFpEnter1_Sheet1.Rows[0].Height;
            this.neuFpEnter1_Sheet1.Columns[0].CellType = new FarPoint.Win.Spread.CellType.TextCellType();

            this.neuPanel5.Height += (int)rowHeight * rowCount;
            this.Height += (int)rowHeight * rowCount;
            p.PrintPage(5, 0, this.neuPanel1);
            #endregion

            return 1;
        }

        int FS.HISFC.Interface.Material.Print.IBillPrint.PrintRowCount
        {
            get { return 25; }
        }

        int FS.HISFC.Interface.Material.Print.IBillPrint.SetPrintData(List<FS.FrameWork.Models.NeuObject> DataList, int pageCount, int totPageCount)
        {
            if (DataList != null && DataList.Count > 0)
            {
                rowCount = DataList.Count;
                this.lblTitle.Text = "{0}物资盘点单";

                FarPoint.Win.Spread.CellType.NumberCellType n = new FarPoint.Win.Spread.CellType.NumberCellType();
                n.DecimalPlaces = 2;
                this.neuFpEnter1_Sheet1.Columns[6].CellType = n;
                this.neuFpEnter1_Sheet1.Columns[7].CellType = n;
                this.neuFpEnter1_Sheet1.Columns[8].CellType = n;
                this.neuFpEnter1_Sheet1.Columns[9].CellType = n;
                this.neuFpEnter1_Sheet1.Columns[10].CellType = n;

                //SOC.Local.Material.Base.Function.SortByCustomerCode(ref new ArrayList(DataList.ToArray()));

                FS.HISFC.BizLogic.Material.Object.CheckDetail check = DataList[0] as FS.HISFC.BizLogic.Material.Object.CheckDetail;
               
                #region label赋值
                this.lblTitle.Text = string.Format(this.lblTitle.Text, deptManager.GetDeptmentById(check.Storage.ID).Name);
                this.lblBillID.Text = "盘点单号:" + check.CheckCode;
                this.lblCheckDate.Text = "盘点日期:"+check.Oper.OperTime.ToShortDateString();
                this.lblOper.Text = "制表人:" + FS.FrameWork.Management.Connection.Operator.ID;
                this.lblPage.Text = "页:" + pageCount.ToString() + "/" + totPageCount.ToString();
                #endregion

                #region farpoint赋值
                decimal sumRetail = 0;
                decimal sumPurchase = 0;
                decimal sumDif = 0;

                this.neuFpEnter1_Sheet1.RowCount = 0;
                int i = 0;

                alPrintData = new ArrayList(DataList.ToArray());

                Base.Function.SortByCustomerCode(ref alPrintData);

                foreach (FS.HISFC.BizLogic.Material.Object.CheckDetail checkDetail in alPrintData)
                {
                    this.neuFpEnter1_Sheet1.AddRows(i, 1);
                    this.neuFpEnter1_Sheet1.Cells[i, 0].Text = baseLogic.GetBaseInfoByID(checkDetail.BaseInfo.ID).UserCode;
                    this.neuFpEnter1_Sheet1.Cells[i, 1].Text = checkDetail.BaseInfo.Name;
                    this.neuFpEnter1_Sheet1.Cells[i, 2].Text = checkDetail.Specs;	
                    if (checkDetail.PackQty == 0)
                        checkDetail.PackQty = 1;

                    this.neuFpEnter1_Sheet1.Cells[i, 4].Value = checkDetail.InPrice;

                    this.neuFpEnter1_Sheet1.Cells[i, 5].Value = checkDetail.SalePrice;

                    this.neuFpEnter1_Sheet1.Cells[i, 3].Text = checkDetail.MinUnit;
                    this.neuFpEnter1_Sheet1.Cells[i, 6].Value = FrameWork.Public.String.FormatNumber(checkDetail.FStoreNum, 2);
                    this.neuFpEnter1_Sheet1.Cells[i, 7].Value = FrameWork.Public.String.FormatNumber(checkDetail.AdjustNum, 2);
                    this.neuFpEnter1_Sheet1.Cells[i, 8].Value = FrameWork.Public.String.FormatNumber(checkDetail.AdjustNum - checkDetail.FStoreNum, 2);

                    decimal purchaseCost = FrameWork.Public.String.FormatNumber(checkDetail.AdjustNum * checkDetail.InPrice / checkDetail.PackQty, 2);
                    decimal retailCost = FrameWork.Public.String.FormatNumber(checkDetail.AdjustNum * checkDetail.SalePrice / checkDetail.PackQty, 2);
                    this.neuFpEnter1_Sheet1.Cells[i, 9].Value = purchaseCost;
                    this.neuFpEnter1_Sheet1.Cells[i, 10].Value = retailCost;
                    this.neuFpEnter1_Sheet1.Cells[i, 11].Value = (retailCost - purchaseCost);


                    sumPurchase += purchaseCost;
                    sumRetail += retailCost;

                    sumDif = sumRetail - sumPurchase;
                }
                //当前页数据
                this.lblCurRet.Text = "本页零售金额:" + sumRetail.ToString("F4");
                this.lblCurPur.Text = "本页购入金额:" + sumPurchase.ToString("F4");
                this.lblCurDif.Text = "本页购零差:" + sumDif.ToString("F4");

                //总数据
                this.lblTotPurCost.Text = totCosetAll.purchaseCost.ToString();
                this.lblTotRetailCost.Text = totCosetAll.retailCost.ToString();
                this.lblTotDif.Text = (totCosetAll.purchaseCost - totCosetAll.retailCost).ToString();

                #endregion

                return 1;
            }
            else
            {
                return 0;
            }
        }

        int FS.HISFC.Interface.Material.Print.IBillPrint.SetPrintDataTotal(List<FS.FrameWork.Models.NeuObject> DataList)
        {
            totCosetAll.purchaseCost = 0;
            totCosetAll.retailCost = 0;
            if (DataList != null && DataList.Count > 0)
            {
                foreach (FS.HISFC.BizLogic.Material.Object.CheckDetail checkDetail in DataList)
                {
                    totCosetAll.purchaseCost += checkDetail.AdjustNum * checkDetail.InPrice / checkDetail.PackQty;
                    totCosetAll.retailCost += checkDetail.AdjustNum * checkDetail.SalePrice / checkDetail.PackQty;
                }
            }
            return 1;
        }

        #endregion

        private class TotCost
        {
            public decimal purchaseCost;
            public decimal retailCost;
        }
    }
}
