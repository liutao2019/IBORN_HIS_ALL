using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Pharmacy;

namespace FS.SOC.Local.DrugStore.ZhuHai.ZDWY.Outpatient
{
    public partial class ucHerbalDrugList : UserControl
    {
        public ucHerbalDrugList()
        {
            InitializeComponent();
            this.SetFarpointHeader();
        }

        private void SetFarpointHeader()
        {
            for (int colunmnIndex = 0; colunmnIndex < this.neuSpread1_Sheet1.Columns.Count; colunmnIndex++)
            {
                this.neuSpread1_Sheet1.ColumnHeader.Columns[colunmnIndex].BackColor = System.Drawing.Color.Transparent;
            }
        }

        private int totRows = 16;

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
            print.SetPageSize(new FS.HISFC.Models.Base.PageSize("OutPatientDrugBill", 800, 550));
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
            foreach (Control c in this.nPanelTop.Controls)
            {
                if (c.Name == "nlbTitle")
                {
                    continue;
                }
                c.Text = string.Empty;
            }
            foreach (Control c in this.nPanelBottom.Controls)
            {
                if (c.Name == "nlbCheckOper" || c.Name == "nlbSendOper")
                { continue; }
                c.Text = string.Empty;
            }
            this.neuSpread1_Sheet1.Rows.Count = 0;

        }

        public void ShowData(ArrayList alData, string diagnose, DrugRecipe drugRecipe, DrugTerminal drugTerminal, string hospitalName, DateTime printTime, int pageNO, int maxPageNO,ref decimal totCost,bool isPCC)
        {

            #region 明细信息
            decimal days = 0;
            this.neuSpread1_Sheet1.RowCount = 0;

            //总行数，不要增加，增加需要分页，减少时注意最后一行的汇总信息
            this.neuSpread1_Sheet1.RowCount = alData.Count;
            for (int index = 0; index < alData.Count; index++)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut applyInfo = alData[index] as FS.HISFC.Models.Pharmacy.ApplyOut;
                this.neuSpread1_Sheet1.Cells[index, 0].Text = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(applyInfo.Item.ID).NameCollection.UserCode;
                this.neuSpread1_Sheet1.Cells[index, 1].Text = applyInfo.Item.Name;
                this.neuSpread1_Sheet1.Cells[index, 2].Text = applyInfo.Item.Specs;
                if (isPCC)
                {
                    this.neuSpread1_Sheet1.Cells[index, 3].Text = applyInfo.Operation.ApplyQty.ToString();
                    this.neuSpread1_Sheet1.Cells[index, 4].Text = applyInfo.Item.MinUnit;
                }
                else
                {
                    string applyPackQty = "";
                 
                    int applyQtyInt = 0;//这个取得商，是整包装单位的量，必须是整数
                    decimal applyRe = 0;//这个取得余数，是最小单位的量，可能是小数
                    applyQtyInt = (int)(applyInfo.Operation.ApplyQty / applyInfo.Item.PackQty);
                    applyRe = applyInfo.Operation.ApplyQty - applyQtyInt * applyInfo.Item.PackQty;
                    if (applyQtyInt > 0)
                    {
                        applyPackQty += applyQtyInt.ToString();
                        this.neuSpread1_Sheet1.Cells[index, 3].Text = applyPackQty.ToString();
                        this.neuSpread1_Sheet1.Cells[index, 4].Text = applyInfo.Item.PackUnit;

                    }
                    if (applyRe > 0)
                    {
                        applyPackQty += applyRe.ToString("F4").TrimEnd('0').TrimEnd('.');
                        this.neuSpread1_Sheet1.Cells[index, 3].Text = applyPackQty.ToString();
                        this.neuSpread1_Sheet1.Cells[index, 4].Text = applyInfo.Item.MinUnit;
                    }
                }
                this.neuSpread1_Sheet1.Cells[index, 5].Text = applyInfo.Usage.Name;
                this.neuSpread1_Sheet1.Cells[index, 6].Text = string.Empty;
                this.neuSpread1_Sheet1.Cells[index, 7].Text = Common.Function.GetOnceDose(applyInfo);
                this.neuSpread1_Sheet1.Cells[index, 8].Text = applyInfo.Item.PriceCollection.RetailPrice.ToString();
                this.neuSpread1_Sheet1.Cells[index, 9].Text = ((applyInfo.Item.PriceCollection.RetailPrice / applyInfo.Item.PackQty)* applyInfo.Operation.ApplyQty).ToString("F2");
                if (pageNO == maxPageNO && index == alData.Count - 1)
                {
                    this.neuSpread1_Sheet1.Rows[index].Border = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, Color.Black, Color.Black, 1, false, false, false, true);
                }
                totCost += (applyInfo.Item.PriceCollection.RetailPrice / applyInfo.Item.PackQty) * applyInfo.Operation.ApplyQty;
            }
            #endregion

        }

        public int Print(ArrayList alData, string diagnose, DrugRecipe drugRecipe, DrugTerminal drugTerminal, string hospitalName, DateTime printTime, int isAuto)
        {
            this.Clear();

            if (alData == null || drugRecipe == null)
            {
                return -1;
            }

            alData.Sort(new CompareByMoOrder());

            FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore drugStoreMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore();

            FS.HISFC.Models.Pharmacy.ApplyOut infoTmp = alData[0] as FS.HISFC.Models.Pharmacy.ApplyOut;

            #region 标题信息
            this.nlbTerminalName.Text = drugTerminal.Name;
            this.nlbDeptName.Text = "药房名称：" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(infoTmp.StockDept.ID);
            this.nlbInvoiceDate.Text = "发票日期：" + drugRecipe.FeeOper.OperTime.ToShortDateString();
            this.nlbInvoiceNO.Text = "发票号：" + drugRecipe.InvoiceNO;
            this.nlbPatientName.Text = "姓名：" + drugRecipe.PatientName;
            this.nlbFeeOper.Text = "收费员：" + drugRecipe.FeeOper.ID;
            this.nlbDocName.Text = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(drugRecipe.Doct.ID);
            this.nlbDoctDept.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugRecipe.DoctDept.ID);
            this.nlbDiagNose.Text = "诊断：" + diagnose;
            #endregion 

            #region 标题尾部信息
            this.nlbDrugOper.Text = "配药员：";
            this.nlbPrintTime.Text = "打印日期：" + drugStoreMgr.GetDateTimeFromSysDateTime();
            bool isPCC = (SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(infoTmp.Item.ID).Type.ID == "C") ? true : false;
            if (isPCC)
            {
                this.nlbTotQty.Text = "共" + alData.Count + "种药";
                this.nlbDays.Text = "剂数：" + infoTmp.Days;
                this.nlbSortNO.Text = this.GetSordId(infoTmp.RecipeNO,infoTmp.StockDept.ID);
            }
            #endregion

            //分页
            decimal totCost = 0m;
            decimal maxPageNO = Math.Ceiling((decimal)alData.Count / totRows);
            for (int pageNO = 0; pageNO < maxPageNO; pageNO++)
            {
                int count = totRows;
                this.SetPanelBottomVisible(false);
                if (count + pageNO * totRows > alData.Count)
                {
                    count = alData.Count - pageNO * totRows;
                }
                ArrayList alOnePage = new ArrayList(); 
                alOnePage=alData.GetRange(pageNO * totRows, count);
                this.ShowData(alOnePage, diagnose, drugRecipe, drugTerminal, hospitalName, printTime, pageNO + 1, (int)maxPageNO,ref  totCost,isPCC);
                if (pageNO == maxPageNO - 1)
                {
                    this.SetPanelBottomVisible(true);
                    this.nlbTotCost.Text = "金额/元：" + totCost.ToString("F2");
                    this.SuspendLayout();
                    this.nPanelMid.Dock = DockStyle.None;
                    this.nPanelBottom.Dock = DockStyle.None;
                    this.nPanelMid.Height = (int)(this.neuSpread1.Sheets[0].RowHeader.Rows[0].Height + count * this.neuSpread1_Sheet1.Rows.Default.Height + 10);
                    this.nPanelBottom.Location = new Point(0, this.nPanelTop.Height + this.nPanelMid.Height);
                    this.ResumeLayout();
                }
                this.PrintPage();
            }

            return 0;
        }

        /// <summary>
        /// 设置控件可见性
        /// </summary>
        /// <param name="isVisible"></param>
        private void SetPanelBottomVisible(bool isVisible)
        {
            foreach (Control c in this.nPanelBottom.Controls)
            {
                if (c is Label)
                {
                    c.Visible = isVisible;
                }
            }
        }

        private string GetSordId(string recipeNO,string deptCode)
        {
            FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore drugStoreMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore();
            string strSql = @"select sortid from
(
select e.recipe_no,rownum sortid from pha_sto_recipe e where e.fee_date > trunc(sysdate) and e.drug_dept_code = '{1}' order by e.fee_date
) where recipe_no = '{0}'";
            strSql = string.Format(strSql, recipeNO,deptCode);
            return drugStoreMgr.ExecSqlReturnOne(strSql, "0");
        }
          



    }

    public class CompareByMoOrder : IComparer
    {

        #region IComparer 成员

        public int Compare(object x, object y)
        {
            if ((x is FS.HISFC.Models.Pharmacy.ApplyOut) && (y is FS.HISFC.Models.Pharmacy.ApplyOut))
            {
                return (x as FS.HISFC.Models.Pharmacy.ApplyOut).OrderNO.CompareTo((y as FS.HISFC.Models.Pharmacy.ApplyOut).OrderNO);
            }
            return 1;
        }

        #endregion
    }
}
