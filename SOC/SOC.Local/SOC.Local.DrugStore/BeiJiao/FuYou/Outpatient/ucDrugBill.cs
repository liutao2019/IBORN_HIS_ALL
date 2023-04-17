using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Neusoft.FrameWork.Management;

namespace Neusoft.SOC.Local.DrugStore.FuYou.Outpatient
{
    public partial class ucDrugBill : UserControl
    {
        public ucDrugBill()
        {
            InitializeComponent();

        }

        /// <summary>
        /// 清屏操作
        /// </summary>
        public void Clear()
        {
            this.lbName.Text = "";
            this.lbSex.Text = "";
            this.lbAge.Text = "";
            this.lbDate.Text = "";
            this.lbDept.Text = "";
            this.lbDoct.Text = "";

            this.neuSpread1_Sheet1.Rows.Count = 0;
        }

        public void PrintPage()
        {
            Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();
            print.SetPageSize(new Neusoft.HISFC.Models.Base.PageSize("OutPatientDrugBill", 800, 1100/2));
            print.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;
            if (((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(5, 5, this);
            }
            else
            {
                print.PrintPage(5, 5, this);
            }
        }


        private void ShowBillData(ArrayList al, Neusoft.HISFC.Models.Pharmacy.DrugRecipe drugRecipe)
        {
            Neusoft.FrameWork.Management.DataBaseManger db = new DataBaseManger();

            this.lbName.Text = drugRecipe.PatientName;
            this.lbSex.Text = drugRecipe.Sex.Name;
            this.lbAge.Text = db.GetAge(drugRecipe.Age);
            this.lbDate.Text = drugRecipe.FeeOper.OperTime.ToString();
            this.lbDept.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugRecipe.DoctDept.ID);
            this.lbDoct.Text = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(drugRecipe.Doct.ID);
            this.nlbRePrint.Visible = !(drugRecipe.RecipeState == "0");


            this.neuSpread1_Sheet1.Rows.Count = 1;
            int iIndex = 1;
            this.neuSpread1_Sheet1.SetValue(0, 0, "");
            this.neuSpread1_Sheet1.SetValue(0, 1, "药品名称");
            this.neuSpread1_Sheet1.Cells[0, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.SetValue(0, 2, "总量");
            this.neuSpread1_Sheet1.Cells[0, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.SetValue(0, 3, "用量");
            this.neuSpread1_Sheet1.Cells[0, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.SetValue(0, 4, "次数");
            this.neuSpread1_Sheet1.Cells[0, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.SetValue(0, 5, "天数");
            this.neuSpread1_Sheet1.Cells[0, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.SetValue(0, 6, "用法");
            this.neuSpread1_Sheet1.Cells[0, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.SetValue(0, 7, "规格");
            this.neuSpread1_Sheet1.Cells[0, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            Neusoft.HISFC.BizLogic.Fee.Outpatient fee = new Neusoft.HISFC.BizLogic.Fee.Outpatient();
            Neusoft.HISFC.BizLogic.Pharmacy.Item item = new Neusoft.HISFC.BizLogic.Pharmacy.Item();
            Neusoft.HISFC.Models.Pharmacy.Item pha;
            ArrayList alFeeItemList = fee.QueryFeeItemListsByInvoiceNO(drugRecipe.InvoiceNO);
            decimal dTotFee = 0.0M;
            decimal dOwnFee = 0.0M;

            foreach (Neusoft.HISFC.Models.Pharmacy.ApplyOut info in al)
            {
                pha = new Neusoft.HISFC.Models.Pharmacy.Item();
                pha = item.GetItem(info.Item.ID);
                if (pha.Quality.ID == "S")
                {
                    this.lbDrugQuality.Text = "麻精一";
                }
                else if (pha.Quality.ID == "P")
                {
                    this.lbDrugQuality.Text = "精二";
                }
                else
                {
                    this.lbDrugQuality.Text = string.Empty;
                }
                this.neuSpread1_Sheet1.RowCount++;
                this.neuSpread1_Sheet1.SetValue(iIndex, 0, iIndex.ToString());
                this.neuSpread1_Sheet1.Cells[iIndex, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                this.neuSpread1_Sheet1.SetValue(iIndex, 1, info.Item.Name);
                this.neuSpread1_Sheet1.SetValue(iIndex, 2, info.Operation.ApplyQty.ToString() + info.Item.PackUnit);
                this.neuSpread1_Sheet1.Cells[iIndex, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.SetValue(iIndex, 3, info.DoseOnce.ToString() + info.Item.DoseUnit);
                this.neuSpread1_Sheet1.Cells[iIndex, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.SetValue(iIndex, 4, info.Frequency.ID);
                this.neuSpread1_Sheet1.Cells[iIndex, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.SetValue(iIndex, 5, info.Days);
                this.neuSpread1_Sheet1.Cells[iIndex, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.SetValue(iIndex, 6, info.Usage.Name);
                this.neuSpread1_Sheet1.Cells[iIndex, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.SetValue(iIndex, 7, info.Item.Specs);
                this.neuSpread1_Sheet1.Cells[iIndex, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.SetValue(iIndex, 8, info.CombNO);
                iIndex++;

                foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList feeItem in alFeeItemList)
                {
                    if (info.Item.ID == feeItem.Item.ID)
                    {
                        dTotFee += feeItem.FT.OwnCost + feeItem.FT.PayCost + feeItem.FT.PubCost;
                        dOwnFee += feeItem.FT.OwnCost;
                    }
                }
            }
            this.neuSpread1_Sheet1.Rows.Add(iIndex, 1);
            iIndex = this.neuSpread1_Sheet1.Rows.Count - 1;
            this.neuSpread1_Sheet1.Cells[iIndex, 0].ColumnSpan = 8;
            this.neuSpread1_Sheet1.Cells[iIndex, 0].Text = "总计：" + dTotFee.ToString() + "元   自费：" + dOwnFee.ToString() + "元                  配药员：      核对员：";
            this.neuSpread1_Sheet1.Rows[iIndex].Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));

            for (int i = 1; i < this.neuSpread1_Sheet1.RowCount - 1; i++)
            {
                if (this.neuSpread1_Sheet1.Cells[i, 8].Text == this.neuSpread1_Sheet1.Cells[i - 1, 8].Text)
                {
                    this.neuSpread1_Sheet1.Cells[i, 0].Text = this.neuSpread1_Sheet1.Cells[i - 1, 0].Text;
                }
            }
        }

        #region by cube
        public void PrintDrugBill(System.Collections.ArrayList alData, Neusoft.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, Neusoft.HISFC.Models.Pharmacy.DrugTerminal drugTerminal)
        {
            this.Clear();
            this.ShowBillData(alData, drugRecipe);
            this.PrintPage();
        }
        #endregion
    }
}
