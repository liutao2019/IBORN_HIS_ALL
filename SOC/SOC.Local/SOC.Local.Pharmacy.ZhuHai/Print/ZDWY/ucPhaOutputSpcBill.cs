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

namespace FS.SOC.Local.Pharmacy.ZhuHai.Print.ZDWY
{
    /// <summary>
    /// 东莞药库报废单据打印
    /// </summary>
    public partial class ucPhaOutputSpcBill : ucBaseControl, Base.IPharmacyBillPrint
    {
        /// <summary>
        /// 药库报废打印单
        /// </summary>
        public ucPhaOutputSpcBill()
        {
            InitializeComponent();
        }

        #region 变量
        private bool isReprint = false;
        /// <summary>
        /// 所有待打印数据
        /// </summary>
        private ArrayList alPrintData = new ArrayList();
        FS.HISFC.BizLogic.Manager.PowerLevelManager powerLevelManager = new FS.HISFC.BizLogic.Manager.PowerLevelManager();
        /// <summary>
        /// 常数管理类
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        #endregion

        #region 入库单打印
        /// <summary>
        /// 打印函数
        /// </summary>
        /// <param name="al">打印数组</param>
        /// <param name="i">第几页</param>
        /// <param name="count">总页数</param>
        /// <param name="title">制单人</param>
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

                FS.HISFC.Models.Admin.PowerLevelClass3 privClass3 = powerLevelManager.LoadLevel3ByPrimaryKey("0320", info.PrivType);

                title = title.Replace("[库存科室]", FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.StockDept.ID));
                if (privClass3 != null)
                {
                    title = title.Replace("[操作类型]", privClass3.Class3Name);
                }

                this.lblTitle.Text = title;
            }
            this.lblCompany.Text = "领用单位: " + info.TargetDept.ID + " " + FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.TargetDept.ID);
            this.lblBillID.Text = "单号: " + info.OutListNO;
            this.lblInputDate.Text = "日期: " + info.OutDate.ToShortDateString();
            this.lblOper.Text = "制单人:" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(info.Operation.ExamOper.ID);
            this.lblPage.Text = "页：" + inow.ToString() + "/" + icount.ToString();
            this.neuLabel10.Text = "仓管员：";// +BillPrintFun.GetStockManagerName(info.StockDept.ID); //BillPrintFun.GetEmplName(info.Operation.ExamOper.ID);
            this.neuLabel10.Visible = true;
            #endregion

            #region farpoint赋值
            decimal sumRetail = 0;
            decimal sumWholeSaleCost = 0;
            decimal sumPurchase = 0;
            decimal sumDif = 0;

            this.neuFpEnter1_Sheet1.RowCount = 0;
          
            for (int i = 0; i < al.Count; i++)
            {
                this.neuFpEnter1_Sheet1.AddRows(i, 1);
                FS.HISFC.Models.Pharmacy.Output output = al[i] as FS.HISFC.Models.Pharmacy.Output;
                this.neuFpEnter1_Sheet1.Cells[i, 0].Text = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(output.Item.ID).UserCode; //药品自定义码


                FS.HISFC.Models.Pharmacy.Item itemObj = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(output.Item.ID);
                if (ctrlIntegrate.GetControlParam<bool>("HNPHA2", false, true))
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 1].Text =  itemObj.NameCollection.RegularName; //药品自定义码                    
                }
                else
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 1].Text =  itemObj.Name; //药品自定义码                    
                }
                
                //this.neuFpEnter1_Sheet1.Cells[i, 1].Text = output.Item.Name;//药品名称
                this.neuFpEnter1_Sheet1.Cells[i, 2].Text = output.Item.Specs;//规格		
                this.neuFpEnter1_Sheet1.Cells[i, 3].Text = SOC.HISFC.BizProcess.Cache.Pharmacy.GetProducerName(output.Producer.ID);//生产厂家
                if (output.Item.PackQty == 0)
                    output.Item.PackQty = 1;
                decimal count = 0;
                count = output.Quantity;
                FarPoint.Win.Spread.CellType.NumberCellType n = new FarPoint.Win.Spread.CellType.NumberCellType();
                n.DecimalPlaces = 4;

                FarPoint.Win.Spread.CellType.NumberCellType nCost = new FarPoint.Win.Spread.CellType.NumberCellType();
                nCost.DecimalPlaces = Function.GetCostDecimal();

                this.neuFpEnter1_Sheet1.Columns[7].CellType = n;
                this.neuFpEnter1_Sheet1.Columns[8].CellType = nCost;
                this.neuFpEnter1_Sheet1.Columns[10].CellType = nCost;

                if (SOC.HISFC.BizProcess.Cache.Common.GetDept(output.StockDept.ID).DeptType.ID.ToString() == "P")
                {
                   
                    this.neuFpEnter1_Sheet1.Cells[i, 5].Text = output.Item.MinUnit;//单位
                    this.neuFpEnter1_Sheet1.Cells[i, 6].Text = (count).ToString();//数量				
                    this.neuFpEnter1_Sheet1.Cells[i, 7].Value = (output.PriceCollection.PurchasePrice / output.Item.PackQty).ToString("F4");
                }
                else
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 5].Text = output.Item.PackUnit;//单位
                    this.neuFpEnter1_Sheet1.Cells[i, 6].Text = (count / output.Item.PackQty).ToString("F2");//数量			
                    this.neuFpEnter1_Sheet1.Cells[i, 7].Text = output.PriceCollection.PurchasePrice.ToString("F4");


                }
                this.neuFpEnter1_Sheet1.Cells[i, 8].Value = output.PurchaseCost.ToString(Function.GetCostDecimalString());
           
                this.neuFpEnter1_Sheet1.Cells[i, 4].Text = output.BatchNO;//批号
                //this.neuFpEnter1_Sheet1.Cells[i, 9].Text = output.PriceCollection.RetailPrice.ToString("F2");
                this.neuFpEnter1_Sheet1.Cells[i, 9].Text = output.PriceCollection.WholeSalePrice.ToString("F2");
                //this.neuFpEnter1_Sheet1.Cells[i, 10].Value = output.RetailCost.ToString(Function.GetCostDecimalString());
                this.neuFpEnter1_Sheet1.Cells[i, 10].Value = output.WholeSaleCost.ToString(Function.GetCostDecimalString());
                this.neuFpEnter1_Sheet1.Cells[i, 11].Value = output.Memo;

                sumRetail += output.RetailCost;
                sumWholeSaleCost += output.WholeSaleCost;
                sumPurchase += output.PurchaseCost;
                sumDif = sumWholeSaleCost - sumPurchase;
            }
            //当前页数据
            //this.lblCurRet.Text = "买入金额合计：" + sumRetail.ToString("F4");
            //this.lblCurPur.Text = "零售金额合计：" + sumPurchase.ToString("F4");
            this.lblCurRet.Text = "零售金额合计：" + sumWholeSaleCost.ToString(Function.GetCostDecimalString());
            this.lblCurPur.Text = "买入金额合计：" + sumPurchase.ToString(Function.GetCostDecimalString());
            this.lblCurDif.Text = "购零差合计：" + sumDif.ToString(Function.GetCostDecimalString());

            if (SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(info.StockDept.ID).DeptType.ID.ToString() != "PI" && isReprint)
            {
                this.lblTitle.Text = "(补打)" + this.lblTitle.Text; ;
            }

            #endregion

            if (SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(info.StockDept.ID).DeptType.ID.ToString() == "PI")
            {
                this.neuLabel9.Visible = true;
                this.neuLabel10.Visible = true;
                this.neuLabel2.Visible = true;
                this.neuLabel3.Visible = true;
            }
            else
            {
                this.neuLabel9.Visible = false;
                this.neuLabel10.Visible = false;
                this.neuLabel2.Visible = false;
                this.neuLabel3.Visible = false;
            }
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
            int height = this.neuFpEnter1.Height;
            int ucHeight = this.Height;
            float rowHeight = this.neuFpEnter1_Sheet1.Rows[0].Height;
            this.neuFpEnter1_Sheet1.Columns[0].CellType = new FarPoint.Win.Spread.CellType.TextCellType();

            if (this.printBill.Sort == FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.SortType.货位号)
            {
                Base.PrintBill.SortByPlaceNO(ref alPrintData);
            }
            else if (this.printBill.Sort == FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.SortType.物理顺序)
            {
                //Base.PrintBill.SortByBillNO(ref alPrintData);
            }

            //补打的时候可能有多张单据，分开
            System.Collections.Hashtable hs = new Hashtable();
            foreach (FS.HISFC.Models.Pharmacy.Output output in alPrintData)
            {
                FS.HISFC.Models.Pharmacy.Output o = output.Clone();
                if (hs.Contains(o.OutListNO))
                {

                    ArrayList al = (ArrayList)hs[o.OutListNO];
                    al.Add(o);
                }
                else
                {
                    ArrayList al = new ArrayList();
                    al.Add(o);
                    hs.Add(o.OutListNO, al);
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
                int copyCount = 1;
                frmSelectPages frmSelect = new frmSelectPages();
                frmSelect.PageCount = pageTotNum;
                frmSelect.SetPages();
                DialogResult dRsult = frmSelect.ShowDialog();
                if (dRsult == DialogResult.OK)
                {
                    fromPage = frmSelect.FromPage - 1;
                    toPage = frmSelect.ToPage;
                    copyCount = frmSelect.CopyCount;
                }
                else
                {
                    return 0;
                }

                for (int i = 0; i < copyCount; i++)
                {

                    //分页打印
                    for (int pageNow = 0; pageNow < pageTotNum; pageNow++)
                    {
                        ArrayList al = new ArrayList();

                        for (int index = pageNow * this.printBill.RowCount; index < alPrint.Count && index < (pageNow + 1) * this.printBill.RowCount; index++)
                        {
                            al.Add(alPrint[index]);
                        }

                        this.SetPrintData(al, pageNow + 1, pageTotNum, printBill.Title);

                        this.neuFpEnter1.Height += (int)rowHeight * al.Count;
                        this.Height += (int)rowHeight * al.Count;

                        if (this.printBill.IsNeedPreview)
                        {
                            p.PrintPreview(5, 0, this.neuPanel1);
                        }
                        else
                        {
                            p.PrintPage(5, 0, this.neuPanel1);
                        }

                        this.neuFpEnter1.Height = height;
                        this.Height = ucHeight;
                    }
                }
            }
            #endregion

            return 1;
        }

        public int SetPrintData(ArrayList alPrintData, Base.PrintBill printBill)
        {
            isReprint = false;
            if (this.alPrintData == null || alPrintData.Count == 0)
            {
                return 1;
            }
            foreach (FS.HISFC.Models.Pharmacy.Output info in alPrintData)
            {
                if (!string.IsNullOrEmpty(info.SpecialFlag) && info.SpecialFlag.Contains("补打"))
                {
                    isReprint = true;

                }
            }
            FS.HISFC.Models.Pharmacy.Output output = alPrintData[0] as FS.HISFC.Models.Pharmacy.Output;
            FS.SOC.HISFC.BizLogic.Pharmacy.InOut itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.InOut();
            ArrayList al = itemMgr.QueryOutputInfo(output.StockDept.ID, output.OutListNO, output.State);
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

    }
}
