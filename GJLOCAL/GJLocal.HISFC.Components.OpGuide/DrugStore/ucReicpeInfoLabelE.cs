using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GJLocal.HISFC.Components.OpGuide.DrugStore
{
    public partial class ucReicpeInfoLabelE : UserControl
    {
        public ucReicpeInfoLabelE()
        {
            InitializeComponent();
        }

        FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
        FS.HISFC.Models.Base.PageSize pageSize;
        FS.HISFC.BizProcess.Integrate.Registration.Registration regMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        /// <summary>
        /// 清除显示信息
        /// </summary>
        private void Clear()
        {
            //foreach (Control c in this.Controls)
            //{
            //    if ((c is Label))
            //    {
            //        //if (c.Name == "nlbHospitalInfo" || c.Name == "nlbPhone" || c.Name == "nlbValueable")
            //        //{
            //        //    continue;
            //        //}
            //        //(c as Label).Text = string.Empty;
            //    }
            //    if(c is FS.FrameWork.WinForms.Controls.NeuPictureBox)
            //    { 
            //        (c as FS.FrameWork.WinForms.Controls.NeuPictureBox).Image = null;
            //    }
            //}
        }

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
        private void Print()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            if (pageSize == null)
            {
                pageSize = pageSizeMgr.GetPageSize("OutPatientDrugLabel");
                if (pageSize != null && pageSize.Printer.ToLower() == "default")
                {
                    pageSize.Printer = "";
                }
                if (pageSize == null)
                {
                    pageSize = new FS.HISFC.Models.Base.PageSize("OutPatientDrugLabel", 315, 138);
                }
            }
            print.SetPageSize(pageSize);
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;

            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(0, 0, this);
            }
            else
            {
                print.PrintPage(0, 0, this);
            }
        }

        /// <summary>
        /// 打印标签
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="drugRecipe"></param>
        /// <param name="drugTerminal"></param>
        /// <returns></returns>
        public int PrintDrugLabel(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal, bool isPrintRegularName, string hospitalName, bool isPrintMemo, string diagnose, DateTime printTime)
        {
            if (alData == null || drugRecipe == null || drugTerminal == null)
            {
                return -1;
            }
            int index = 1;
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyInfo in alData)
            {
                this.Clear();

                FS.HISFC.Models.Registration.Register regInfo = regMgr.GetByClinic(drugRecipe.ClinicNO);
                if (regInfo.PatientType == "2")
                {
                    this.lbPatientName.Text = "(V)";
                }
                else
                {
                    this.lbPatientName.Text = string.Empty;
                }
                this.lbPatientName.Text += drugRecipe.PatientName;
                //this.nlbPatientAge.Text = drugRecipe.Sex.Name + " " + (new FS.FrameWork.Management.DataBaseManger()).GetAge(drugRecipe.Age);
                if (drugRecipe.Sex.Name == "男")
                {
                    this.nlbPatientAge.Text = "Mr";
                }
                else
                {
                    this.nlbPatientAge.Text = "Ms";
                }
                //this.nlbDocDept.Text = FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugRecipe.DoctDept.ID);
                //this.nlbRePrint.Visible = !(drugRecipe.RecipeState == "0");
                this.nlbPrintTime.Text = printTime.Year.ToString() + "Y/年" + printTime.Month.ToString() + "M/月" + printTime.Day.ToString() + "D/日";
                //this.nlbInvoiceNO.Text = drugRecipe.InvoiceNO;
                //this.nlbSendTerminal.Text = drugTerminal.Name;
                this.nlbDrugEngName.Text = applyInfo.Item.NameCollection.EnglishName;// {6BF385F0-0366-4223-935B-9D1644280EBD} lfhm
                this.nlbDrugName.Text = applyInfo.Item.Name;
                //this.nlbSpecs.Text = applyInfo.Item.Specs;
                //bool isInjectUsage = FS.SOC.HISFC.BizProcess.Cache.Common.IsInjectUsage(applyInfo.Usage.ID);
                //if (!isInjectUsage)
                //{
                //    this.nlbUsageName.Text = applyInfo.Usage.Name;
                //    this.nlbFrequencyName.Text = Common.Function.GetFrequenceName(applyInfo.Frequency);
                //    this.nlbFrequencyTimeE.Text = Common.Function.GetOnceDose(applyInfo) + "/次";
                //}

                string strTemp = "({0})Times a day.Total for ({1}) days.一天({0})次，({1})天量.";
                string time = Common.Function.GetFrequenceTimesPerDay(applyInfo.Frequency);
                string days = applyInfo.Days.ToString();
                strTemp = string.Format(strTemp, time, days);
                this.lbTimeAndDay.Text = strTemp;
                //this.nlbFrequencyTimeC.Text = this.nlbFrequencyTimeE.Text;
                //this.lbDaysE.Text = applyInfo.Days.ToString();
                //this.lbDaysC.Text = applyInfo.Days.ToString();
                this.nlbFrequencyName.Text = applyInfo.Frequency.Name;
                this.nlbUsageName.Text = applyInfo.Usage.Name;
                this.lbJX.Text = Common.Function.GetDosageFormName(applyInfo.Item.ID);
                //this.nlbPageNO.Text = index + "-" + alData.Count;


                //this.nlbStockDept.Text = FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(applyInfo.StockDept.ID);
                this.nlbHospitalInfo.Text = (new FS.FrameWork.Management.DataBaseManger()).Hospital.Name;
                //数量显示处理
                string applyPackQty = "";
                string qtyShowType = "最小单位";//"包装单位";
                if (qtyShowType == "包装单位")
                {
                    int applyQtyInt = 0;//这个取得商，是整包装单位的量，必须是整数
                    decimal applyRe = 0;//这个取得余数，是最小单位的量，可能是小数
                    applyQtyInt = (int)(applyInfo.Operation.ApplyQty / applyInfo.Item.PackQty);
                    applyRe = applyInfo.Operation.ApplyQty - applyQtyInt * applyInfo.Item.PackQty;
                    if (applyQtyInt > 0)
                    {
                        applyPackQty += applyQtyInt.ToString() + applyInfo.Item.PackUnit;
                    }
                    if (applyRe > 0)
                    {
                        applyPackQty += applyRe.ToString("F4").TrimEnd('0').TrimEnd('.') + applyInfo.Item.MinUnit;
                    }
                }
                else
                {
                    applyPackQty = (applyInfo.Operation.ApplyQty).ToString("F4").TrimEnd('0').TrimEnd('.') + applyInfo.Item.MinUnit;
                }
                this.nlbTotQty.Text = applyPackQty;
                this.nlbOnceDose.Text = applyInfo.DoseOnce + applyInfo.Item.MinUnit;
                this.nlbMemo.Text = applyInfo.Memo;
                //this.npbRecipeNO.Image = this.CreateBarCode(drugRecipe.RecipeNO);
                //this.nlbDocName.Text = "医生:" + FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(drugRecipe.Doct.ID);

                //this.nlbDiagnose.Text = "诊断:" + diagnose;
                index++;
                this.Print();

            }
           
            return 1;
        }


    }
}
