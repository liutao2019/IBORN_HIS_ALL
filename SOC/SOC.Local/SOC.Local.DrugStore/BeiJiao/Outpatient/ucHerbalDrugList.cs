using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Pharmacy;

namespace FS.SOC.Local.DrugStore.BeiJiao.Outpatient
{
    public partial class ucHerbalDrugList : UserControl
    {
        public ucHerbalDrugList()
        {
            InitializeComponent();
        }

        #region 变量


        #endregion

        /// <summary>
        /// 生成条形码方法
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private Image CreateBarCode(string code)
        {
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            BarcodeLib.TYPE type = BarcodeLib.TYPE.CODE128;
            BarcodeLib.AlignmentPositions Align = BarcodeLib.AlignmentPositions.CENTER;
            b.IncludeLabel = true;
            b.Alignment = Align;
            return b.Encode(type, code, System.Drawing.Color.Black, System.Drawing.Color.White, 150, 50);
        }

        /// <summary>
        /// 打印
        /// </summary>
        private void PrintPage()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.SetPageSize(new FS.HISFC.Models.Base.PageSize("OutPatientDrugBill", 400, 400));
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(5, 5, this);
            }
            else
            {
                print.PrintPage(5, 5, this);
            }
        }

        /// <summary>
        /// 清除控件文字
        /// </summary>
        private void Clear()
        {
            lbName.Text = "";
            lbRecipe.Text = "";
            this.neuSpread1_Sheet1.RowCount = 0;
        }

        public void ShowData(ArrayList alData, string diagnose, DrugRecipe drugRecipe, DrugTerminal drugTerminal, string hospitalName, DateTime printTime, int pageNO, int maxPageNO)
        {

            #region 明细信息
            decimal totCost = 0;

            decimal days = 0;

            //总行数，不要增加，增加需要分页，减少时注意最后一行的汇总信息
            this.neuSpread1_Sheet1.RowCount = 13;
            int index = 0;
            for (int rowIndex = 0; rowIndex < alData.Count && index < alData.Count; rowIndex++)
            {
                //第一个药
                ApplyOut applyOut1 = alData[index] as ApplyOut;

                this.neuSpread1_Sheet1.Cells[rowIndex, 0].Text = applyOut1.Item.Name;
                this.neuSpread1_Sheet1.Cells[rowIndex, 1].Text = applyOut1.Operation.ApplyQty.ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut1.Item.MinUnit;
                this.neuSpread1_Sheet1.Cells[rowIndex, 2].Text = SOC.HISFC.BizProcess.Cache.Common.GetUsageName(applyOut1.Usage.ID);
                days = applyOut1.Days;
                if (applyOut1.Item.PackQty == 0)
                {
                    applyOut1.Item.PackQty = 1;
                }
                totCost += FS.FrameWork.Function.NConvert.ToDecimal((applyOut1.Item.PriceCollection.RetailPrice * (applyOut1.Operation.ApplyQty * applyOut1.Days / applyOut1.Item.PackQty)).ToString("F2"));
                index++;

                //第二个药
                if (index < alData.Count)
                {
                    ApplyOut applyOut2 = alData[index] as ApplyOut;

                    this.neuSpread1_Sheet1.Cells[rowIndex, 3].Text = applyOut2.Item.Name;
                    this.neuSpread1_Sheet1.Cells[rowIndex, 4].Text = applyOut2.Operation.ApplyQty.ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut2.Item.MinUnit;
                    this.neuSpread1_Sheet1.Cells[rowIndex, 5].Text = SOC.HISFC.BizProcess.Cache.Common.GetUsageName(applyOut2.Usage.ID);
                    if (applyOut2.Item.PackQty == 0)
                    {
                        applyOut2.Item.PackQty = 1;
                    }
                    totCost += FS.FrameWork.Function.NConvert.ToDecimal((applyOut2.Item.PriceCollection.RetailPrice * (applyOut2.Operation.ApplyQty * applyOut2.Days / applyOut2.Item.PackQty)).ToString("F2"));

                    index++;
                }
            }

            //最后一行
            if (maxPageNO > 1)
            {
                this.lbRecipe.Text = "处方号：" + drugRecipe.RecipeNO + "  " + pageNO.ToString() + "/" + maxPageNO.ToString();
                //分页的提示
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 3).RowSpan = 3;
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 3).Value = "注意：本方包含 " + maxPageNO.ToString() + " 页" + "  共 " + days.ToString("F4").TrimEnd('0').TrimEnd('.') + " 剂";
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 3).Font = new Font("宋体", 11F, FontStyle.Bold);
            }
            else
            {
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 4).Value = "共 " + days.ToString("F4").TrimEnd('0').TrimEnd('.') + " 剂";
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 4).ColumnSpan = 2;
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 4).Font = new Font("宋体", 11F, FontStyle.Bold);
            }
            this.nlbTotCost.Text = "总药价：" + totCost.ToString() + "元";
            this.nlbPrintTime.Text = "打印时间：" + printTime.ToString();
            #endregion
        }

        public int PrintDrugList(ArrayList alData, string diagnose, DrugRecipe drugRecipe, DrugTerminal drugTerminal, string hospitalName, DateTime printTime)
        {
            this.Clear();

            if (alData == null || drugRecipe == null)
            {
                return -1;
            }

            #region 标题信息
            //姓名
            lbName.Text = drugRecipe.PatientName + "  " + drugRecipe.Sex.Name + "  " + (new FS.FrameWork.Management.DataBaseManger()).GetAge(drugRecipe.Age);
            //处方号
            this.lbRecipe.Text = "处方号：" + drugRecipe.RecipeNO;

            #endregion 

            //分页
            decimal maxPageNO = Math.Ceiling((decimal)alData.Count / 25);
            for (int pageNO = 0; pageNO < maxPageNO; pageNO++)
            {
                int count = 25;
                if (count + pageNO * 25 > alData.Count)
                {
                    count = alData.Count - pageNO * 25;
                }
                ArrayList alOnePage = alData.GetRange(pageNO * 25, count);
                this.ShowData(alData, diagnose, drugRecipe, drugTerminal, hospitalName, printTime, pageNO + 1, (int)maxPageNO);               
              
                this.PrintPage();
            }
           
            return 0;
        }
    }
}
