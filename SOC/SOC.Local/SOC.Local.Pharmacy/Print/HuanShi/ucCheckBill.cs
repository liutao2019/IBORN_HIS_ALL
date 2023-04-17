using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.WinForms.Controls;

namespace FS.SOC.Local.Pharmacy.Print.HuanShi
{
    /// <summary>
    /// 东莞药品入库单据打印
    /// </summary>
    public partial class ucCheckBill : ucBaseControl, Base.IPharmacyBillPrint
    {
        /// <summary>
        /// 药品入库打印单
        /// </summary>
        public ucCheckBill()
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

        private decimal totPurchaseCost = 0;
        private decimal totRetailCost = 0;

        private string drugType = "";

        /// <summary>
        /// 打印相关属性值
        /// </summary>
        private Base.PrintBill printBill = new Base.PrintBill();

        private DateTime fOperDate = DateTime.Now;
        #endregion

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
                FS.HISFC.Models.Pharmacy.Check check = alPrintData[0] as FS.HISFC.Models.Pharmacy.Check;
                fOperDate = check.FOper.OperTime;

                FS.SOC.HISFC.BizLogic.Pharmacy.Check itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Check();
                ArrayList alCheckDetail = itemMgr.QueryCheckDetailByCheckCode(check.StockDept.ID, check.CheckNO);

                foreach (FS.HISFC.Models.Pharmacy.Check checkItem in alCheckDetail)
                {
                    if (checkItem.AdjustQty > 0 || checkItem.FStoreQty > 0)
                    {
                        this.alPrintData.Add(checkItem);
                        if (checkItem.PackQty==0)
                        {
                            checkItem.PackQty = 1;
                        }
                        totPurchaseCost += FS.FrameWork.Public.String.FormatNumber(checkItem.AdjustQty * checkItem.Item.PriceCollection.PurchasePrice / checkItem.Item.PackQty, 2);
                        totRetailCost += FS.FrameWork.Public.String.FormatNumber(checkItem.AdjustQty*checkItem.Item.PriceCollection.RetailPrice/checkItem.Item.PackQty,2);
                    }
                }

                Base.PrintBill.SortByCustomerCode(ref this.alPrintData);

                this.printBill = printBill;
                return this.Print();
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


            int pageTotNum = alPrintData.Count / this.printBill.RowCount;
            if (alPrintData.Count != this.printBill.RowCount * pageTotNum)
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

                for (int index = pageNow * this.printBill.RowCount; index < alPrintData.Count && index < (pageNow + 1) * this.printBill.RowCount; index++)
                {
                    al.Add(alPrintData[index]);
                }
                this.SetPrintData(al, pageNow + 1, pageTotNum, printBill.Title);

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
            // }
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
            this.lblTitle.Text = "{0}药品盘点单";
            if (al.Count <= 0)
            {
                MessageBox.Show("没有打印的数据!");
                return;
            }
            if (icount <= 0)
            {
                icount = 1;
            }
            FS.HISFC.Models.Pharmacy.Check info = (FS.HISFC.Models.Pharmacy.Check)al[0];

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
                    title = title.Replace("[药品类型]", tmpDrugType);
                }
                this.lblTitle.Text = title;
            }

            this.lblBillID.Text = "盘点单号:" + info.CheckNO;
            this.lblCheckDate.Text = "盘点日期:" + fOperDate.ToShortDateString();
            this.lblOper.Text = "制表人:" + FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(FS.FrameWork.Management.Connection.Operator.ID);
            this.lblPage.Text = "页:" + inow.ToString() + "/" + icount.ToString();


            #endregion

            #region farpoint赋值
            decimal sumRetail = 0;
            decimal sumPurchase = 0;
            decimal sumDif = 0;



            this.neuFpEnter1_Sheet1.RowCount = 0;
            for (int i = 0; i < al.Count; i++)
            {
                this.neuFpEnter1_Sheet1.AddRows(i, 1);
                FS.HISFC.Models.Pharmacy.Check check = al[i] as FS.HISFC.Models.Pharmacy.Check;
                this.neuFpEnter1_Sheet1.Cells[i, 0].Text = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(check.Item.ID).UserCode; //药品自定义码
                this.neuFpEnter1_Sheet1.Cells[i, 1].Text = check.Item.Name;//药品名称
                this.neuFpEnter1_Sheet1.Cells[i, 2].Text = check.Item.Specs;//规格		
                if (check.Item.PackQty == 0)
                    check.Item.PackQty = 1;
                decimal count = 0;
                count = check.Quantity;

                FarPoint.Win.Spread.CellType.NumberCellType n = new FarPoint.Win.Spread.CellType.NumberCellType();
                n.DecimalPlaces = 2;
                this.neuFpEnter1_Sheet1.Columns[6].CellType = n;
                this.neuFpEnter1_Sheet1.Columns[7].CellType = n;
                this.neuFpEnter1_Sheet1.Columns[8].CellType = n;
                this.neuFpEnter1_Sheet1.Columns[9].CellType = n;
                this.neuFpEnter1_Sheet1.Cells[i, 3].Text = check.Item.PackUnit;//单位

                this.neuFpEnter1_Sheet1.Cells[i, 4].Value = check.Item.PriceCollection.PurchasePrice;

                this.neuFpEnter1_Sheet1.Cells[i, 5].Value = check.Item.PriceCollection.RetailPrice;


                this.neuFpEnter1_Sheet1.Cells[i, 6].Value = check.FStoreQty;

                this.neuFpEnter1_Sheet1.Cells[i, 7].Value = check.AdjustQty;
                decimal purchaseCost = FrameWork.Public.String.FormatNumber(check.AdjustQty * check.Item.PriceCollection.PurchasePrice / check.Item.PackQty, 2);
                decimal retailCost = FrameWork.Public.String.FormatNumber(check.AdjustQty * check.Item.PriceCollection.RetailPrice / check.Item.PackQty, 2);
                this.neuFpEnter1_Sheet1.Cells[i, 8].Value = purchaseCost;
                this.neuFpEnter1_Sheet1.Cells[i, 9].Value = retailCost;
                this.neuFpEnter1_Sheet1.Cells[i, 10].Value = (retailCost - purchaseCost);


                sumPurchase += purchaseCost;
                sumRetail += retailCost;
               
                sumDif = sumRetail - sumPurchase;
            }
            //当前页数据
            this.lblCurRet.Text = "本页零售金额:" + sumRetail.ToString("F4");
            this.lblCurPur.Text = "本页购入金额:" + sumPurchase.ToString("F4");
            this.lblCurDif.Text = "本页购零差:" + sumDif.ToString("F4");

            //总数据
            this.lblTotPurCost.Text = totPurchaseCost.ToString();
            this.lblTotRetailCost.Text = totRetailCost.ToString();
            this.lblTotDif.Text = (totRetailCost - totPurchaseCost).ToString();

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

        /// <summary>
        /// 预览
        /// </summary>
        /// <returns></returns>
        public int Preview()
        {
            this.printBill.IsNeedPreview = true;
            return this.Print();
        }

    }
}
