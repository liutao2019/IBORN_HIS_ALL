using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.DrugStore.ShenZhen.BinHai.Inpatient
{
    public partial class ucDrugLabel : UserControl
    {
        public ucDrugLabel()
        {
            InitializeComponent();
        }

        FS.HISFC.Models.Base.PageSize pageSize = null;
        FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();

        /// <summary>
        /// 清空赋值的数据
        /// 对界面所有的label的Text赋值""
        /// </summary>
        /// <returns></returns>
        public int Clear()
        {
            foreach (Control c in this.Controls)
            {
                if (c is Label && (c.Name != "lblPhone" && c.Name != "lblPhoneNum" && c.Name != "lbLine" && c.Name != "label1"))
                {
                    c.Text = "";
                }
            }

            //二维码清空
            this.pictureBox1.Image = null;
            return 1;
        }

        /// <summary>
        /// 设置药品信息
        /// </summary>
        /// <param name="applyOut"></param>
        /// <param name="curPageNO"></param>
        /// <param name="maxPageNO"></param>
        /// <param name="isPrintRegularName"></param>
        /// <param name="isPrintMemo"></param>
        /// <param name="qtyShowType"></param>
        /// <returns></returns>
        private int SetDrugInfo
            (
            FS.HISFC.Models.Pharmacy.ApplyOut applyOut,
            int curPageNO,
            int maxPageNO,
            bool isPrintRegularName,
            bool isPrintMemo,
            string qtyShowType)
        {
            string printDrugName = applyOut.Item.Name;

            FS.HISFC.Models.Pharmacy.Item item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(applyOut.Item.ID);
            if (item == null)
            {
                MessageBox.Show("打印标签时获取药品基本信息失败");
                return -1;
            }

            if (isPrintRegularName)
            {
                printDrugName = item.NameCollection.RegularName + "(" + applyOut.Item.Name + ")";
            }

            this.lbDrugName.Text = printDrugName;
            if (item.NameCollection.EnglishName.Length >= 45)
            {
                this.lbDrugEnglistName.Text = item.NameCollection.EnglishName.Substring(0, 45);
            }
            else
            {
                this.lbDrugEnglistName.Text = item.NameCollection.EnglishName;
            }
            this.lbSpecs.Text = "(" + applyOut.Item.Specs + ")";

            //数量显示处理
            string applyPackQty = "";
            if (qtyShowType == "包装单位")
            {
                int applyQtyInt = 0;//这个取得商，是整包装单位的量，必须是整数
                decimal applyRe = 0;//这个取得余数，是最小单位的量，可能是小数
                applyQtyInt = (int)(applyOut.Operation.ApplyQty / applyOut.Item.PackQty);
                applyRe = applyOut.Operation.ApplyQty - applyQtyInt * applyOut.Item.PackQty;
                if (applyQtyInt > 0)
                {
                    applyPackQty += applyQtyInt.ToString() + applyOut.Item.PackUnit;
                }
                if (applyRe > 0)
                {
                    applyPackQty += applyRe.ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut.Item.MinUnit;
                }
            }
            else
            {
                applyPackQty = (applyOut.Operation.ApplyQty).ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut.Item.MinUnit;
            }

            this.lbQty.Text = applyPackQty;

            this.lbQty.Text = "";

            //this.lbUsage.Text = "用法："
            //    + FS.SOC.HISFC.BizProcess.Cache.Common.GetUsageName(applyOut.Usage.ID)
            //    + "  " + FS.SOC.HISFC.BizProcess.Cache.Order.GetFrequencyName(applyOut.Frequency.ID)
            //    + "  " + Common.Function.GetOnceDose(applyOut);
            this.lbUsage.Text = "用法："
                + FS.SOC.HISFC.BizProcess.Cache.Common.GetUsageName(applyOut.Usage.ID)
                + "  " + FS.SOC.Local.DrugStore.ShenZhen.Common.Function.GetFrequenceName(applyOut.Frequency)
                + "  " + SOC.Local.DrugStore.ShenZhen.Common.Function.GetOnceDose(applyOut) + "/次";

            if (isPrintMemo)
            {
                try
                {
                    //this.lbOrderMemo.Text = SOC.Local.DrugStore.ShenZhen.Common.Function.GetOrder(applyOut.OrderNO).Memo;
                }
                catch { }

                this.lbDrugCaution.Text = item.Product.Caution + "  " + SOC.Local.DrugStore.ShenZhen.Common.Function.GetOrder(applyOut.OrderNO).Memo;
                if (this.lbDrugCaution.Text != "  ")
                {
                    this.lbDrugCaution.BorderStyle = BorderStyle.FixedSingle;
                }
                else
                {
                    this.lbDrugCaution.BorderStyle = BorderStyle.None;
                }
            }
            string packageItemFlag = SOC.Local.DrugStore.ShenZhen.Common.Function.GetAccountFlagBySequenceNo(applyOut.RecipeNO, applyOut.SequenceNO.ToString());
            if (packageItemFlag != "3")
            {
                this.lbRetailPrice.Text = "单价:" + applyOut.Item.PriceCollection.RetailPrice.ToString("F2") + "￥";
                this.lbCost.Text = "金额:" + (applyOut.Item.PriceCollection.RetailPrice * applyOut.Operation.ApplyQty / applyOut.Item.PackQty).ToString("F2") + "￥";

            }
            this.lbPlace.Text = SOC.Local.DrugStore.ShenZhen.Common.Function.GetStockPlaceNO(applyOut);//货位号
            this.lbPageNO.Text = curPageNO.ToString() + "/" + maxPageNO.ToString();

            return 1;
        }

        /// <summary>
        /// 设置患者信息
        /// 同一张处方只设置一次
        /// </summary>
        /// <param name="drugRecipe"></param>
        /// <returns></returns>
        private int SetPatientInfo(string patientNO,string bedNO)
        {
            FS.HISFC.BizLogic.RADT.InPatient inpatient = new FS.HISFC.BizLogic.RADT.InPatient();
            FS.HISFC.Models.RADT.PatientInfo patientInfo = inpatient.GetPatientInfoByPatientNO(patientNO);
            this.lbPatientInfo.Text = FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.Instance.GetDepartmentName(patientInfo.PVisit.PatientLocation.NurseCell.ID) + " " + bedNO + "床 "
                + " " + patientInfo.Name
                + " " + FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.Instance.GetAge(patientInfo.Birthday)
                + " " + patientInfo.Sex.Name
               // + " " + DateTime.Now.ToString("MM-dd HH:mm")
                + "";
                this.lbPactName.Text = patientInfo.Pact.Name;
            return 1;
        }

        /// <summary>
        /// 设置其他信息：诊断等
        /// 同一张处方只设置一次
        /// </summary>
        /// <param name="hospitalName"></param>
        /// <param name="diagnose"></param>
        /// <param name="recipeNO"></param>
        /// <param name="windowName"></param>
        /// <returns></returns>
        private int SetOtherInfo(string hospitalName, string diagnose, string recipeNO, string windowName)
        {
            this.lbWindowName.Text = windowName;
            this.lblHospitalInfo.Text = hospitalName;
            this.pictureBox1.Image = SOC.Local.DrugStore.ShenZhen.Common.Function.Create2DBarcode(recipeNO);
            this.lblPhone.Text = "电话：";
            this.lblPhoneNum.Text = "86913333-3532";
            this.lblHospitalInfo.Text = "香港大学深圳医院";
            return 1;
        }

        /// <summary>
        /// 打印
        /// </summary>
        private void Print()
        {
            ////启用华南打印基类打印
            //FS.SOC.Windows.Forms.PrintExtendPaper print = new FS.SOC.Windows.Forms.PrintExtendPaper();

            ////获取维护的纸张
            //if (pageSize == null)
            //{
            //    pageSize = pageSizeMgr.GetPageSize("OutPatientDrugLabel");
            //    //指定打印处理，default说明使用默认打印机的处理
            //    if (pageSize != null && pageSize.Printer.ToLower() == "default")
            //    {
            //        pageSize.Printer = "";
            //    }
            //    //没有维护时默认一个纸张
            //    if (pageSize == null)
            //    {
            //        pageSize = new FS.HISFC.Models.Base.PageSize("OutPatientDrugLabel", 400, 200);
            //    }
            //}

            ////打印边距处理
            //print.DrawingMargins = new System.Drawing.Printing.Margins(pageSize.Left, 0, pageSize.Top, 0);
            //print.PaperName = pageSize.Name;
            //print.PaperHeight = pageSize.Height;
            //print.PaperWidth = pageSize.Width;
            //print.PrinterName = pageSize.Printer;

            ////不显示页码选择
            //print.IsShowPageNOChooseDialog = false;

            ////管理员使用预览功能
            //if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            //{
            //    print.PrintPageView(this);
            //}
            //else
            //{
            //    print.PrintPage(this);
            //}
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
                    pageSize = new FS.HISFC.Models.Base.PageSize("OutPatientDrugLabel", 400, 100);
                }
            }
            print.SetPageSize(pageSize);
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;

            //try
            //{
            //    //普济分院4号窗口自动打印总是暂停：打印机针头或纸张太薄或太厚都可能引起暂停
            //    FS.FrameWork.WinForms.Classes.Print.ResumePrintJob();
            //}
            //catch { }
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(0, 0, this);
            }
            else
            {
                print.PrintPage(0, 0, this);
            }
        }

        public int PrintDrugLabel
            (
            System.Collections.ArrayList alApplyOut,
            bool isPrintRegularName,
            bool isPrintMemo,
            string qtyShowType,
            string hospitalName,
            string diagnose
            )
        {
            int maxPageNO = alApplyOut.Count;
            int curPageNO = 1;

            this.Clear();
           
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alApplyOut)
            {
                this.SetDrugInfo(applyOut, curPageNO, maxPageNO, isPrintRegularName, isPrintMemo, qtyShowType);
                this.SetOtherInfo(hospitalName, "", applyOut.RecipeNO, "");
                string bedNO = applyOut.BedNO;
                if (bedNO.Length > 4)
                {
                    bedNO = bedNO.Substring(4);
                }
                this.SetPatientInfo(applyOut.PatientNO,bedNO);
                curPageNO++;

                this.Print();
            }

            return 1;
        }


    }
}
