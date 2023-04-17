using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.WinForms.Controls;
using SOC.Local.Pharmacy.ZhuHai.Print.ZDWY;

namespace FS.SOC.Local.Pharmacy.ZhuHai.Print.ZDWY
{
    /// <summary>
    /// 东莞药品入库计划打印
    /// </summary>
    public partial class ucNewDrugNoticePrint : ucBaseControl, Base.IPharmacyBillPrint
    {
        /// <summary>
        /// 药品入库打印单
        /// </summary>
        public ucNewDrugNoticePrint()
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

        private FS.HISFC.BizLogic.Manager.Constant consMgr = new FS.HISFC.BizLogic.Manager.Constant();
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
            p.IsLandScape = true;
            #endregion
            if (this.printBill.IsNeedPreview)
            {
                p.PrintPreview(5, 0, this.neuPanelMain);
            }
            else
            {
                p.PrintPage(5, 0, this.neuPanelMain);
            }
            return 1;
        }

        /// <summary>
        /// 3赋值
        /// </summary>
        /// <param name="al">打印数组</param>
        /// <param name="i">第几页</param>
        /// <param name="count">总页数</param>
        /// <param name="title">标题</param>
        private void SetPrintData(ArrayList al, int inow, int icount, string title, FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill printBill)
        {
            if (al.Count <= 0)
            {
                MessageBox.Show("没有打印的数据!");
                return;
            }
            if (icount <= 0)
            {
                icount = 1;
            }
            
            DateTime printTime = this.consMgr.GetDateTimeFromSysDateTime();

            this.nlbTime.Text = "(" + printTime.Year + " - " + printTime.Month.ToString().PadLeft(2, '0') + ")";
            this.nlbTime1.Text = printTime.ToShortDateString();
            this.nlbOperName.Text = "经手人：" + this.consMgr.Operator.Name;

            #region farpoint赋值

            this.neuSpread1_Sheet1.RowCount = 0;
            for (int i = 0; i < al.Count; i++)
            {
                bool ismzNoSplit= false;
                bool iszyNoSplit = false;
                this.neuSpread1_Sheet1.Rows.Add(i, 1);
                this.neuSpread1_Sheet1.Rows.Default.Height = 50F;
                FS.HISFC.Models.Pharmacy.Item itemInfo = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem((al[i] as FS.HISFC.Models.Pharmacy.InPlan).Item.ID);
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count -1,0].Text = (inow * printBill.RowCount + i + 1).ToString();
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 1].Text = itemInfo.Name;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 2].Text = itemInfo.NameCollection.UserCode;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 3].Text = itemInfo.NameCollection.WBCode;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 4].Text = itemInfo.NameCollection.SpellCode;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 5].Text = itemInfo.Specs;
                if(("1|3|").Contains(itemInfo.SplitType.ToString() + "|"))
                {
                    ismzNoSplit = true;
                }
                if(("1|3|").Contains(itemInfo.LZSplitType.ToString() + "|"))
                {
                    iszyNoSplit = true;
                }
                if(ismzNoSplit)
                {
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 6].Text = itemInfo.PackUnit;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 7].Text = itemInfo.PriceCollection.RetailPrice.ToString("F4");
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 6].Text = itemInfo.MinUnit;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 7].Text = (itemInfo.PriceCollection.RetailPrice / itemInfo.PackQty).ToString("F4");
                }
                if(iszyNoSplit)
                {
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 8].Text = itemInfo.PackUnit;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 9].Text = itemInfo.PriceCollection.RetailPrice.ToString("F4");
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 8].Text = itemInfo.MinUnit;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 9].Text = (itemInfo.PriceCollection.RetailPrice / itemInfo.PackQty).ToString("F4");
                }
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 10].Text = " ";

                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 11].Text = itemInfo.SpecialFlag == "1"?"是":"否";
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 12].Text = string.IsNullOrEmpty(itemInfo.ProductID)?" ":itemInfo.ProductID;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 13].Text = " ";

            }

            #endregion

            this.resetTitleLocation();
        }

        /// <summary>
        /// 重新设置标题位置
        /// </summary>
        private void resetTitleLocation()
        {
            this.nlbTitle.Location = new Point((this.Width - this.nlbTitle.Width)/2,this.nlbTitle.Location.Y);
            this.nlbTime.Location = new Point((this.Width - this.nlbTime.Width)/2, this.nlbTime.Location.Y);
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

        public int SetPrintData(ArrayList alPrintData, FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill printBill)
        {
           Base.PrintBill.SortByCustomerCode(ref this.alPrintData);

           this.printBill = printBill;

           decimal totPage = Math.Ceiling(FS.FrameWork.Function.NConvert.ToDecimal(alPrintData.Count / printBill.RowCount));
           if (alPrintData.Count%printBill.RowCount != 0)
           {
               totPage++;
           }

           for (int i = 0; i < totPage; i++)
           {
               ArrayList allPrintData = new ArrayList();
               if (i == totPage - 1)
               {
                   allPrintData = alPrintData.GetRange(i * printBill.RowCount, alPrintData.Count - i * printBill.RowCount);
               }
               else
               {
                   allPrintData = alPrintData.GetRange(i * printBill.RowCount, printBill.RowCount);
               }
               this.SetPrintData(allPrintData, i, allPrintData.Count, string.Empty,printBill);
               this.Print();
           }

           return 1;
        }

        #endregion
    }
}
