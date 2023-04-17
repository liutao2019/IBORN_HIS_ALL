using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.DrugStore.FuYou.Outpatient
{
    public partial class ucDrugBag : UserControl
    {
        public ucDrugBag()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 清屏操作
        /// </summary>
        public void Clear()
        {
            foreach (Control c in this.Controls)
            {
                if (c is Label)
                {
                    c.Text = "";
                }
            }
            this.nlbRePrint.Text = "补打";
            this.neuLabel1.Text = "温馨提示：为保证患者用药安全，药品一经发出，不";
            this.neuLabel2.Text = "得退换，咨询电话：22662106  22667821";
        }

        /// <summary>
        /// 频次时间间隔
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper freqIntervalHelper = null;

        public void PrintDrugBill(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal,string diagnose)
        {
            if (alData == null || alData.Count == 0)
            {
                return;
            }
            int curPageNO = 1;
            int maxPageNO = alData.Count;
            FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
            FS.HISFC.Models.Base.PageSize pageSize = pageSizeMgr.GetPageSize("DrugBag");
            if (pageSize == null)
            {
                pageSize = new FS.HISFC.Models.Base.PageSize();
                pageSize.Printer = "DrugBag";
                pageSize.Width = 472;
                pageSize.Height = 350;
                pageSize.Top = 102 - 38;

            }

            System.Drawing.Printing.PaperSize paperSize = new System.Drawing.Printing.PaperSize();
            paperSize.Width = pageSize.Width;
            paperSize.Height = pageSize.Height;

            SOC.Windows.Forms.PrintExtendPaper p = new FS.SOC.Windows.Forms.PrintExtendPaper();
            p.DrawingMargins.Top = pageSize.Top;
            p.IsLandscape = true;
            p.PrinterName = pageSize.Printer;

            p.SetPaperSize(paperSize);


            if (freqIntervalHelper == null)
            {
                freqIntervalHelper = new FS.FrameWork.Public.ObjectHelper();
                FS.HISFC.BizProcess.Integrate.Manager inteMgr=new FS.HISFC.BizProcess.Integrate.Manager();
                ArrayList al = inteMgr.GetConstantList("FreqInterval");

                freqIntervalHelper.ArrayObject = al;
            }

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alData)
            {
                this.Clear();

                this.lbTitle.Visible = false;
                this.nlbRecipeNO.Text = drugRecipe.RecipeNO + "方";
                this.nlbPageNO.Text = curPageNO.ToString() + "/" + maxPageNO.ToString();
                this.panelLine1.Visible = (curPageNO == maxPageNO);
                this.nlbCardNO.Text = "门诊号：" + drugRecipe.CardNO;
                this.nlbDeptName.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugRecipe.PatientDept.ID);

                FS.HISFC.Models.Order.OutPatient.Order order = SOC.Local.DrugStore.Common.Function.GetOrder(applyOut.OrderNO);
                if (order == null)
                {
                    return;
                }


                this.nlbSeeDate.Text = "就诊日期：" + order.MOTime.ToString("yyyy-MM-dd");
                this.nlbDiagnose.Text = "诊断：" + diagnose;

                this.lbPatient.Text = "姓名：" + drugRecipe.PatientName + "   性别：" + drugRecipe.Sex.Name +"   年龄："+Common.Function.GetAge(drugRecipe.Age);

                this.lbDrugInfo.Text = applyOut.Item.Name;
                this.lblSpec.Text = applyOut.Item.Specs;

                this.nlbDrugQty.Text = "共："+applyOut.Operation.ApplyQty.ToString() + applyOut.Item.MinUnit;

                if (freqIntervalHelper.GetObjectFromID(applyOut.Frequency.ID) != null)
                {
                    lblFreqInterval.Text = freqIntervalHelper.GetObjectFromID(applyOut.Frequency.ID).Memo;
                }

                //每次量
                //if (order != null)
                //{
                //    this.nlbOnceDose.Text = "每次" + order.ExtendFlag4.ToString() + order.DoseUnit;
                //}
                //else
                //{
                //    this.nlbOnceDose.Text = "每次" + applyOut.DoseOnce.ToString() + applyOut.Item.DoseUnit;
                //}
                this.nlbOnceDose.Text = "每次" + Common.Function.GetOnceDose(applyOut);
                if (applyOut.Item.MinUnit == applyOut.Item.DoseUnit)
                {
                    if (order != null&&!string.IsNullOrEmpty(order.DoseOnceDisplay))
                    {
                        this.nlbOnceDose.Text = "每次" + order.DoseOnceDisplay.ToString() + order.DoseUnit;
                    }
                }

                this.nlbRePrint.Visible = !(drugRecipe.RecipeState == "0");

                this.lbUsage.Text = SOC.HISFC.BizProcess.Cache.Common.GetUsageName(applyOut.Usage.ID);

                //频次
                this.nlbFrequence.Text = SOC.HISFC.BizProcess.Cache.Order.GetFrequencyName(applyOut.Frequency.ID);

                if (nlbFrequence.Text.Length > "每八小时一次".Length)
                {
                    nlbFrequence.Text = nlbFrequence.Text.Substring(0, "每八小时一次".Length);
                }

                this.nlbMemo.Text =     "注意事项：" + order.Memo;
                this.nlbDrugMemo.Text = "          " + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(applyOut.Item.ID).Product.Caution;

                this.nlbPrintTime.Text = pageSizeMgr.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd HH:mm:ss");
                this.nlbSendTerminal.Text = FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugTerminal.Dept.ID) + drugTerminal.Name;

                p.IsShowPageNOChooseDialog = false;
                if (((FS.HISFC.Models.Base.Employee)pageSizeMgr.Operator).IsManager)
                {
                    p.PrintPageView(this);
                }
                else
                {
                    p.PrintPage(this);
                }

                curPageNO++;
            }

        }
    }
}
