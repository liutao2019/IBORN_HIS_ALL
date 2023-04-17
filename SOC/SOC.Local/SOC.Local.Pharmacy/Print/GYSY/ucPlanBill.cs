using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.WinForms.Controls;

namespace FS.SOC.Local.Pharmacy.Print.GYSY
{
    /// <summary>
    /// 东莞药品入库计划打印
    /// </summary>
    public partial class ucPlanBill : ucBaseControl, Base.IPharmacyBillPrint
    {
        /// <summary>
        /// 药品入库打印单
        /// </summary>
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

        /// <summary>
        /// 常数管理类
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
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
            this.lblTitle.Text = "{0}药品入库计划单";
            if (al.Count <= 0)
            {
                MessageBox.Show("没有打印的数据!");
                return;
            }
            if (icount <= 0)
            {
                icount = 1;
            }
            FS.HISFC.Models.Pharmacy.InPlan plan = (FS.HISFC.Models.Pharmacy.InPlan)al[0];

            #region label赋值
            if (string.IsNullOrEmpty(title))
            {
                this.lblTitle.Text = string.Format(this.lblTitle.Text, FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(plan.Dept.ID), drugType);
            }
            else
            {
                if (title.IndexOf("[库存科室]") != -1)
                {
                    title = title.Replace("[库存科室]", FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(plan.Dept.ID));
                }
                if (title.IndexOf("[药品类型]") != -1)
                {
                    string tmpDrugType = "(" + this.drugType + ")";
                    title = title.Replace("[药品类型]", tmpDrugType);
                }
                this.lblTitle.Text = title;
            }

            this.lblCheckDate.Text = "计划日期:" + fOperDate.ToShortDateString();
            //this.lblOper.Text = "制表人:" + FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(FS.FrameWork.Management.Connection.Operator.ID);
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
                this.neuFpEnter1_Sheet1.Cells[i, 0].Text = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(info.Item.ID).UserCode; //药品自定义码

                FS.HISFC.Models.Pharmacy.Item itemObj = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(info.Item.ID);
                if (ctrlIntegrate.GetControlParam<bool>("HNPHA2", false, true))
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 1].Text =  itemObj.NameCollection.RegularName; //药品自定义码                    
                }
                else
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 1].Text =  itemObj.Name; //药品自定义码                    
                }
                
                //this.neuFpEnter1_Sheet1.Cells[i, 1].Text = info.Item.Name;//药品名称
                this.neuFpEnter1_Sheet1.Cells[i, 2].Text = info.Item.Specs;//规格		
                if (info.Item.PackQty == 0)
                    info.Item.PackQty = 1;
                decimal count = 0;
                count = info.InQty;

                FarPoint.Win.Spread.CellType.NumberCellType n = new FarPoint.Win.Spread.CellType.NumberCellType();
                n.DecimalPlaces = 2;
                this.neuFpEnter1_Sheet1.Columns[4].CellType = n;
                this.neuFpEnter1_Sheet1.Columns[5].CellType = n;
                this.neuFpEnter1_Sheet1.Columns[8].CellType = n;


                this.neuFpEnter1_Sheet1.Cells[i, 4].Value = info.Item.PriceCollection.PurchasePrice;

                this.neuFpEnter1_Sheet1.Cells[i, 5].Value = info.Item.PriceCollection.RetailPrice;
                if (departMent.DeptType.ID.ToString() == "PI")
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 3].Text = info.Item.PackUnit;//单位
                    this.neuFpEnter1_Sheet1.Cells[i, 6].Value = (info.PlanQty / info.Item.PackQty).ToString("F3").TrimEnd('0').TrimEnd('.');
                    this.neuFpEnter1_Sheet1.Cells[i, 7].Value = "";
                }
                else
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 3].Text = info.Item.MinUnit;
                    this.neuFpEnter1_Sheet1.Cells[i, 6].Value = info.PlanQty;
                    this.neuFpEnter1_Sheet1.Cells[i, 7].Value = "";
                }

                //decimal purchaseCost = FrameWork.Public.String.FormatNumber(check.AdjustQty * check.Item.PriceCollection.PurchasePrice / check.Item.PackQty, 2);
                //decimal retailCost = FrameWork.Public.String.FormatNumber(check.AdjustQty * check.Item.PriceCollection.RetailPrice / check.Item.PackQty, 2);
                this.neuFpEnter1_Sheet1.Cells[i, 8].Value = (info.PlanQty/info.Item.PackQty*info.Item.PriceCollection.PurchasePrice).ToString("F2");
                this.neuFpEnter1_Sheet1.Cells[i, 9].Value = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(plan.PlanOper.ID);
                this.neuFpEnter1_Sheet1.Cells[i, 10].Value = plan.PlanOper.OperTime.ToString("yyyy-MM-dd");


                //sumPurchase += purchaseCost;
                sumRetail += info.PlanQty / info.Item.PackQty * info.Item.PriceCollection.RetailPrice;
                sumPurchase += info.PlanQty / info.Item.PackQty * info.Item.PriceCollection.PurchasePrice;
                this.totRetailCost += info.PlanQty / info.Item.PackQty * info.Item.PriceCollection.RetailPrice;
                this.totPurchaseCost += info.PlanQty / info.Item.PackQty * info.Item.PriceCollection.PurchasePrice;
                //sumDif = sumRetail - sumPurchase;
            }

            //最后一页显示金额
            if (inow == icount)
            {   
                this.lblCurRet.Text = "零售金额:" + this.totRetailCost.ToString("F2");
                this.lblCurPur.Text = "购入金额:" + this.totPurchaseCost.ToString("F2");
                this.lblCurDif.Text = "购零差:" + (this.totRetailCost - this.totPurchaseCost).ToString("F2");
                this.lblOper.Text = "制表人:" + FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(FS.FrameWork.Management.Connection.Operator.ID);
            }
            else
            {
                this.lblOper.Text = "";//制表人
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


        #region IPharmacyBillPrint 成员

        public int SetPrintData(ArrayList alPrintData, FS.SOC.Local.Pharmacy.Base.PrintBill printBill)
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
           return this.Print();
        }

        #endregion
    }
}
