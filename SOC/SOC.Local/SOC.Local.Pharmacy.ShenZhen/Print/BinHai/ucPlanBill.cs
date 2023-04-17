using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.WinForms.Controls;
using System.Collections;

namespace FS.SOC.Local.Pharmacy.ShenZhen.Print.BinHai
{
    public partial class ucPlanBill : UserControl, Base.IPharmacyBillPrint
    {
        public ucPlanBill()
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
        FS.HISFC.BizLogic.Manager.Department dept = new FS.HISFC.BizLogic.Manager.Department();
        private decimal totPurchaseCost = 0;
        private decimal totRetailCost = 0;

        private string drugType = "";


        private Base.PrintBill printBill = new Base.PrintBill();

        private DateTime fOperDate = DateTime.Now;
        #endregion

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


            int pageTotNum = (alPrintData.Count+5) / this.printBill.RowCount;
            if (alPrintData.Count+5 != this.printBill.RowCount * pageTotNum)
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
                if (pageNow + 1 == pageTotNum)
                {
                    this.neuPanel5.Height += (int)rowHeight * (al.Count+5);
                    this.Height += (int)rowHeight * (al.Count+5);
                }
                else
                {
                    this.neuPanel5.Height += (int)rowHeight * al.Count;
                    this.Height += (int)rowHeight * al.Count;
                }
                
                if (this.printBill.IsNeedPreview)
                {
                    p.PrintPreview(0, 0, this.neuPanel1);
                }
                else
                {
                    p.PrintPage(0, 0, this.neuPanel1);
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
            //{FED2D2EE-4B9F-4eeb-A38D-26212DFD67CF}
            //this.lblTitle.Text = "{0}药品入库计划单";
            if (al.Count <= 0 && inow != icount)
            {
                MessageBox.Show("没有打印的数据!");
                return;
            }
            else if(al.Count<=0&&inow==icount)
            {
                this.lblTitle.Text = "香港大学深圳医院药品采购计划单";

                this.lblCheckDate.Text = "计划日期:" + fOperDate.ToShortDateString();
                this.lblPage.Text = "页:" + inow.ToString() + "/" + icount.ToString();

                this.neuFpEnter1_Sheet1.RowCount = 0;

                //最后一页显示金额
                if (inow == icount)
                {
                    this.lblCurRet.Text = "零售金额:" + this.totRetailCost.ToString("F2");
                    this.lblCurPur.Text = "购入金额:" + this.totPurchaseCost.ToString("F2");
                    this.lblCurDif.Text = "购零差:" + (this.totRetailCost - this.totPurchaseCost).ToString("F2");
                    this.lblOper.Text = "制表人:";
                    //{FED2D2EE-4B9F-4eeb-A38D-26212DFD67CF}
                    this.lblCurRet.Visible = true;
                    this.lblCurRet.Text = "总金额:" + this.totRetailCost.ToString("F2") + "元";
                    this.lblCurRet.Location = new Point(this.label4.Location.X, this.label4.Location.Y);
                    this.lblOper.Location = new Point(this.lblOper.Location.X, this.lblOper.Location.Y + 20);
                    this.label2.Location = new Point(this.label2.Location.X, this.label2.Location.Y + 20);
                    this.label3.Location = new Point(this.label3.Location.X, this.label3.Location.Y + 20);
                    this.label4.Location = new Point(this.label4.Location.X, this.label4.Location.Y + 20);
                    this.neuFpEnter1_Sheet1.AddRows(this.neuFpEnter1_Sheet1.RowCount, 5);
                }
                else
                {
                    this.lblCurRet.Visible = false;
                    this.lblOper.Text = "制表人:";//制表人
                    this.lblCurRet.Text = "";// "本页零售金额:" + sumRetail.ToString("F4");
                    this.lblCurPur.Text = "";// "本页购入金额:" + sumPurchase.ToString("F4");
                    this.lblCurDif.Text = "";// "本页购零差:" + sumDif.ToString("F4");
                }
                //总数据 
                this.resetTitleLocation();
                return;
            }
 
            if (icount <= 0)
            {
                icount = 1;
            }
            FS.HISFC.Models.Pharmacy.InPlan plan = (FS.HISFC.Models.Pharmacy.InPlan)al[0];

            #region label赋值
            //{FED2D2EE-4B9F-4eeb-A38D-26212DFD67CF}
            //if (string.IsNullOrEmpty(title))
            //{
            //    this.lblTitle.Text = string.Format(this.lblTitle.Text, FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(plan.Dept.ID), drugType);
            //}
            //else
            //{
            //    if (title.IndexOf("[库存科室]") != -1)
            //    {
            //        title = title.Replace("[库存科室]", FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(plan.Dept.ID));
            //    }
            //    if (title.IndexOf("[药品类型]") != -1)
            //    {
            //        string tmpDrugType = "(" + this.drugType + ")";
            //        title = title.Replace("[药品类型]", tmpDrugType);
            //    }
            //    this.lblTitle.Text = title;
            //}
            this.lblTitle.Text = "香港大学深圳医院药品采购计划单";

            this.lblCheckDate.Text = "计划日期:" + fOperDate.ToShortDateString();
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
                FS.HISFC.Models.Pharmacy.InPlan info = al[i] as FS.HISFC.Models.Pharmacy.InPlan;
                FS.HISFC.Models.Base.Department departMent = dept.GetDeptmentById(info.Dept.ID);
                this.neuFpEnter1_Sheet1.Cells[i, (int)ColIndex.ColGbCode].Text = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(info.Item.ID).GBCode; //药品自定义码
                this.neuFpEnter1_Sheet1.Cells[i, (int)ColIndex.ColDrugName].Text = info.Item.Name;//药品名称
                this.neuFpEnter1_Sheet1.Cells[i, (int)ColIndex.ColEnglishName].Text = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(info.Item.ID).NameCollection.EnglishName;//英文名称
                this.neuFpEnter1_Sheet1.Cells[i, (int)ColIndex.ColSpecs].Text = info.Item.Specs;//规格		
                if (info.Item.PackQty == 0)
                    info.Item.PackQty = 1;
                decimal count = 0;
                count = info.InQty;

                FarPoint.Win.Spread.CellType.NumberCellType n = new FarPoint.Win.Spread.CellType.NumberCellType();
                n.DecimalPlaces = 2;
                this.neuFpEnter1_Sheet1.Columns[(int)ColIndex.ColRetailPrice].CellType = n;
                this.neuFpEnter1_Sheet1.Columns[(int)ColIndex.ColPurchasePrice].CellType = n;

                this.neuFpEnter1_Sheet1.Cells[i, (int)ColIndex.ColRetailPrice].Value = info.Item.PriceCollection.RetailPrice;  //价格
                this.neuFpEnter1_Sheet1.Cells[i, (int)ColIndex.ColUnit].Text = info.Item.MinUnit;
                //    this.neuFpEnter1_Sheet1.Cells[i, 7].Value = info.PlanQty;
                //    this.neuFpEnter1_Sheet1.Cells[i, 8].Value = "";
                //if (departMent.DeptType.ID.ToString() == "PI")
                //{
                //    this.neuFpEnter1_Sheet1.Cells[i, 4].Text = info.Item.PackUnit;//单位
                //    this.neuFpEnter1_Sheet1.Cells[i, 7].Value = (info.PlanQty / info.Item.PackQty).ToString("F3").TrimEnd('0').TrimEnd('.');
                //    this.neuFpEnter1_Sheet1.Cells[i, 8].Value = "";
                //}
                //else
                //{
                this.neuFpEnter1_Sheet1.Cells[i, (int)ColIndex.ColUnit].Text = info.Item.PackUnit;   //单位
                this.neuFpEnter1_Sheet1.Cells[i, (int)ColIndex.ColPlanNum].Value = (info.PlanQty / info.Item.PackQty).ToString("F3").TrimEnd('0').TrimEnd('.');  //采购量
                this.neuFpEnter1_Sheet1.Cells[i, (int)ColIndex.ColPlanNum].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.neuFpEnter1_Sheet1.Cells[i, (int)ColIndex.ColConfirmNum].Value = "";    //修改量
                //}

                //decimal purchaseCost = FrameWork.Public.String.FormatNumber(check.AdjustQty * check.Item.PriceCollection.PurchasePrice / check.Item.PackQty, 2);
                //decimal retailCost = FrameWork.Public.String.FormatNumber(check.AdjustQty * check.Item.PriceCollection.RetailPrice / check.Item.PackQty, 2);
                this.neuFpEnter1_Sheet1.Cells[i, (int)ColIndex.ColProducer].Value = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetProducerName(FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(info.Item.ID).Product.Producer.ID);
                this.neuFpEnter1_Sheet1.Cells[i, (int)ColIndex.ColProducer].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                this.neuFpEnter1_Sheet1.Cells[i, (int)ColIndex.ColCompany].Value = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetCompanyName(FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(info.Item.ID).Product.Company.ID);

                sumRetail += info.PlanQty / info.Item.PackQty * info.Item.PriceCollection.RetailPrice;
                sumPurchase += info.PlanQty / info.Item.PackQty * info.Item.PriceCollection.PurchasePrice;
                this.totRetailCost += info.PlanQty / info.Item.PackQty * info.Item.PriceCollection.RetailPrice;
                this.totPurchaseCost += info.PlanQty / info.Item.PackQty * info.Item.PriceCollection.PurchasePrice;
            }

            //最后一页显示金额
            if (inow == icount)
            {
                this.lblCurRet.Text = "零售金额:" + this.totRetailCost.ToString("F2");
                this.lblCurPur.Text = "购入金额:" + this.totPurchaseCost.ToString("F2");
                this.lblCurDif.Text = "购零差:" + (this.totRetailCost - this.totPurchaseCost).ToString("F2");
                this.lblOper.Text = "制表人:";
                //{FED2D2EE-4B9F-4eeb-A38D-26212DFD67CF}
                this.lblCurRet.Visible = true;
                this.lblCurRet.Text = "总金额:" + this.totRetailCost.ToString("F2")+"元";
                this.lblCurRet.Location = new Point(this.label4.Location.X, this.label4.Location.Y);
                this.lblOper.Location = new Point(this.lblOper.Location.X, this.lblOper.Location.Y + 20);
                this.label2.Location = new Point(this.label2.Location.X, this.label2.Location.Y + 20);
                this.label3.Location = new Point(this.label3.Location.X, this.label3.Location.Y + 20);
                this.label4.Location = new Point(this.label4.Location.X, this.label4.Location.Y + 20);
                this.neuFpEnter1_Sheet1.AddRows(this.neuFpEnter1_Sheet1.RowCount, 5);
            }
            else
            {
                this.lblCurRet.Visible = false;
                this.lblOper.Text = "制表人:";//制表人
                this.lblCurRet.Text = "";// "本页零售金额:" + sumRetail.ToString("F4");
                this.lblCurPur.Text = "";// "本页购入金额:" + sumPurchase.ToString("F4");
                this.lblCurDif.Text = "";// "本页购零差:" + sumDif.ToString("F4");
            }
            //总数据
            

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

        /// <summary>
        /// 枚举
        /// </summary>
        private enum ColIndex
        {
            /// <summary>
            /// 产品ID
            /// </summary>
            ColGbCode,
            /// <summary>
            /// 中文药名
            /// </summary>
            ColDrugName,
            /// <summary>
            /// 英文药名
            /// </summary>
            ColEnglishName,
            /// <summary>
            /// 规格
            /// </summary>
            ColSpecs,
            /// <summary>
            /// 单位
            /// </summary>
            ColUnit,
            /// <summary>
            /// 生产企业
            /// </summary>
            ColProducer,
            /// <summary>
            /// 零售价
            /// </summary>
            ColPurchasePrice,
            /// <summary>
            /// 经销商
            /// </summary>
            ColCompany,
            /// <summary>
            /// 价格
            /// </summary>
            ColRetailPrice,
            /// <summary>
            /// 采购量
            /// </summary>
            ColPlanNum,
            /// <summary>
            /// 修改量
            /// </summary>
            ColConfirmNum
        }

        #region IPharmacyBillPrint 成员

        public int SetPrintData(ArrayList alPrintData, FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill printBill)
        {
            Base.PrintBill.SortByCustomerCode(ref this.alPrintData);
            string billNo = (alPrintData[0] as FS.HISFC.Models.Pharmacy.InPlan).BillNO;
            string deptNo = (alPrintData[0] as FS.HISFC.Models.Pharmacy.InPlan).Dept.ID;
            SOC.HISFC.BizLogic.Pharmacy.Plan plan = new FS.SOC.HISFC.BizLogic.Pharmacy.Plan();
            List<FS.HISFC.Models.Pharmacy.InPlan> al = plan.MergeInPlan(deptNo, billNo);
            for (int i = 0; i < al.Count; i++)
            {
                this.alPrintData.Add(alPrintData[i]);
            }
            this.printBill = printBill;
            //return this.Print();
            return this.Preview();
        }

        #endregion
    }
}
