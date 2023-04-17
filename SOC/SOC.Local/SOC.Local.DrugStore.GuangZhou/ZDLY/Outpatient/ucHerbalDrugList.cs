using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Pharmacy;

namespace FS.SOC.Local.DrugStore.GuangZhou.ZDLY.Outpatient
{
    public partial class ucHerbalDrugList : UserControl
    {
        public ucHerbalDrugList()
        {
            InitializeComponent();
        }

        #region 变量
        private FS.HISFC.BizLogic.Registration.Register registerMgr = new FS.HISFC.BizLogic.Registration.Register();

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
            //print.SetPageSize(new FS.HISFC.Models.Base.PageSize("OutPatientDrugBill", 583, 800));
            print.SetPageSize(new FS.HISFC.Models.Base.PageSize("OutPatientDrugBill", 583, 850));
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(5, 0, this);
            }
            else
            {
                print.PrintPage(5, 0, this);
            }
        }

        /// <summary>
        /// 清除控件文字
        /// </summary>
        private void Clear()
        {
            lbName.Text = "";
            lbRecipe.Text = "";
            this.lblSeeNo.Text = "";
            this.lblAge.Text = "";
            this.lblSex.Text = "";
            this.lblCardNO.Text = "";
            this.lblSeeNo.Text = "";
            this.lblDeptName.Text = "";
            this.lblDiagNose.Text = "";
            this.lbRegisterDate.Text = "";
            this.nlbDoct.Text = "";
            this.lbRecipe.Text = "";
            this.lblPhone.Text = "";
            this.lbTotCost.Text = "";
            this.lblFeeOper.Text = "";
            this.lblPageNO.Text = "";
            this.lblPrintTime.Text = "";
            this.neuSpread1_Sheet1.RowCount = 0;
        }

        public void ShowData(ArrayList alData, string diagnose, DrugRecipe drugRecipe, DrugTerminal drugTerminal, string hospitalName, DateTime printTime, int pageNO, int maxPageNO)
        {

            #region 明细信息
            decimal totCost = 0;

            decimal days = 0;
            this.neuSpread1_Sheet1.RowCount = 0;

            //总行数，不要增加，增加需要分页，减少时注意最后一行的汇总信息
            this.neuSpread1_Sheet1.RowCount = (alData.Count + 1) / 2 + 1;
            int index = 0;
            //string memo = "";
            for (int rowIndex = 0; rowIndex < alData.Count && index < alData.Count; rowIndex++)
            {
                //第一个药
                ApplyOut applyOut1 = alData[index] as ApplyOut;

                this.neuSpread1_Sheet1.Cells[rowIndex, 0].Text = "  " + applyOut1.Item.Name;
                this.neuSpread1_Sheet1.Cells[rowIndex, 1].Text = (applyOut1.DoseOnce).ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut1.Item.DoseUnit;
                FS.HISFC.Models.Order.OutPatient.Order order = Common.Function.GetOrder(applyOut1.OrderNO);
                if (order != null)
                {
                    this.neuSpread1_Sheet1.Cells[rowIndex, 2].Text = order.Memo;
                }
                days = applyOut1.Days;
                if (applyOut1.Item.PackQty == 0)
                {
                    applyOut1.Item.PackQty = 1;
                }
                totCost += FS.FrameWork.Function.NConvert.ToDecimal((applyOut1.Item.PriceCollection.RetailPrice * (applyOut1.Operation.ApplyQty / applyOut1.Item.PackQty)).ToString("F2"));
                index++;

                //第二个药
                if (index < alData.Count)
                {
                    ApplyOut applyOut2 = alData[index] as ApplyOut;

                    this.neuSpread1_Sheet1.Cells[rowIndex, 3].Text = applyOut2.Item.Name;
                    this.neuSpread1_Sheet1.Cells[rowIndex, 4].Text = applyOut2.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut2.Item.DoseUnit;
                    FS.HISFC.Models.Order.OutPatient.Order order2 = Common.Function.GetOrder(applyOut2.OrderNO);
                    if (order != null)
                    {
                        this.neuSpread1_Sheet1.Cells[rowIndex, 5].Text = order2.Memo;
                    }
                    if (applyOut2.Item.PackQty == 0)
                    {
                        applyOut2.Item.PackQty = 1;
                    }
                    totCost += FS.FrameWork.Function.NConvert.ToDecimal((applyOut2.Item.PriceCollection.RetailPrice * (applyOut2.Operation.ApplyQty / applyOut2.Item.PackQty)).ToString("F2"));

                    index++;
                }
            }

            this.lbTotCost.Text =" 金额合计：" + totCost.ToString() + "元";

            //最后一行
            ApplyOut applyOut3 = alData[0] as ApplyOut;
            this.lblPageNO.Text = "第" + pageNO.ToString() + "页/共" + maxPageNO.ToString() + "页";
            string lastRowValue = "用法：共" + days.ToString("F4").TrimEnd('0').TrimEnd('.') + " 剂     共 " + alData.Count.ToString() + " 味       " + "" + SOC.HISFC.BizProcess.Cache.Common.GetUsageName(applyOut3.Usage.ID) + "                ";

            this.lblTotal.Text = lastRowValue;
            //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Border = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, false);

            #endregion
        }

        public int Print(ArrayList alData, string diagnose, DrugRecipe drugRecipe, DrugTerminal drugTerminal, string hospitalName, DateTime printTime)
        {
            this.Clear();

            if (alData == null || drugRecipe == null)
            {
                return -1;
            }
            FS.HISFC.Models.Registration.Register register = new FS.HISFC.Models.Registration.Register();
            register = registerMgr.GetByClinic(drugRecipe.ClinicNO);

            #region 标题信息
            //姓名
            lbName.Text = "姓名：" + drugRecipe.PatientName;
            //处方号
            this.lbRecipe.Text = "处方号：" + drugRecipe.RecipeNO;
            this.lblSex.Text = "性别：" + drugRecipe.Sex.Name;
            this.lblAge.Text = "年龄：" + (new FS.FrameWork.Management.DataBaseManger()).GetAge(drugRecipe.Age);
            this.lblCardNO.Text = "病历号：" + drugRecipe.CardNO;
            this.nlbDoct.Text = "医生：" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(drugRecipe.Doct.ID);
            this.lblDiagNose.Text = "诊断：" + diagnose;
            this.lblPrintTime.Text = "打印时间：" + printTime.ToString("yyyy-MM-dd hh:mm");
            this.lbRegisterDate.Text = "挂号日期：" + drugRecipe.RegTime.ToString("yyyy-MM-dd");
            if (register != null)
            {
                this.lbName.Text += "   " + register.Pact.Name.ToString();
                this.lblPhone.Text = "电话："+ register.PhoneHome.ToString();
                this.lblSeeNo.Text = "流水号：" + register.OrderNO.ToString();
            }
            this.lblFeeOper.Text = "收费：" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(drugRecipe.FeeOper.ID);

            #endregion

            //分页
            decimal maxPageNO = Math.Ceiling((decimal)alData.Count / 50);
            for (int pageNO = 0; pageNO < maxPageNO; pageNO++)
            {
                int count = 50;
                if (count + pageNO * 50 > alData.Count)
                {
                    count = alData.Count - pageNO * 50;
                }
                ArrayList alOnePage = new ArrayList();
                alOnePage = alData.GetRange(pageNO * 50, count);
                this.ShowData(alOnePage, diagnose, drugRecipe, drugTerminal, hospitalName, printTime, pageNO + 1, (int)maxPageNO);

                this.PrintPage();
            }


            return 0;
        }


    }
}
