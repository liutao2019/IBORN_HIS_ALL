using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.DrugStore.FOSI.Outpatient
{
    public partial class ucComboDrugLabel2 : UserControl
    {
        public ucComboDrugLabel2()
        {
            InitializeComponent();
        }

        FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
        FS.HISFC.Models.Base.PageSize pageSize;

        /// <summary>
        /// 清除显示信息
        /// </summary>
        private void Clear()
        {

            this.lbPatientName.Text = "";
            this.nlbPageNO.Text = "";
            this.lbUsage.Text = "";
            this.nlbFrequence.Text = "";
            this.nlbRecipeNO.Text = "";

            this.nlbDrugInfo1.Text = "";
            this.nlbDrugInfo2.Text = "";
            this.nlbDrugInfo3.Text = "";

            this.nlbSpecs1.Text = "";
            this.nlbSpecs2.Text = "";
            this.nlbSpecs3.Text = "";
            this.nlbSpecs4.Text = "";

            this.nlbOnceDose1.Text = "";
            this.nlbOnceDose2.Text = "";
            this.nlbOnceDose3.Text = "";
            this.nlbOnceDose4.Text = "";

            this.nlbDrugQty1.Text = "";
            this.nlbDrugQty2.Text = "";
            this.nlbDrugQty3.Text = "";
            this.nlbDrugQty4.Text = "";
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
                    pageSize = new FS.HISFC.Models.Base.PageSize("OutPatientDrugLabel", 400, 200);
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
            if(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(0, 0, this);
            }
            else
            {
                print.PrintPage(0, 0, this);
            }
        }

        /// <summary>
        /// 获取频次名称
        /// </summary>
        /// <param name="frequency"></param>
        /// <returns></returns>
        private string GetFrequenceName(FS.HISFC.Models.Order.Frequency frequency)
        {
            return Common.Function.GetFrequenceName(frequency);
        }

        /// <summary>
        /// 获取每次用量的最小单位表现形式
        /// </summary>
        /// <param name="applyOut"></param>
        /// <returns></returns>
        private string GetOnceDose(FS.HISFC.Models.Pharmacy.ApplyOut applyOut)
        {
            return Common.Function.GetOnceDoseFoSi(applyOut);
        }

        /// <summary>
        /// 打印标签
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="drugRecipe"></param>
        /// <param name="drugTerminal"></param>
        /// <returns></returns>
        public int PrintDrugLabel(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal, bool isPrintRegularName, string hospitalName, bool isPrintMemo, string qtyShowType, DateTime printTime, string pageNO)
        {
            if (alData == null || drugRecipe == null || drugTerminal == null)
            {
                return -1;
            }
            if (alData.Count != 4)
            {
                return -1;
            }

            this.Clear();
            this.lbPatientName.Text = drugRecipe.PatientName + "  " + drugRecipe.Sex.Name + "  " + (new FS.FrameWork.Management.DataBaseManger()).GetAge(drugRecipe.Age);
            this.nlbPageNO.Text = pageNO;

            #region 第一个药品
            FS.HISFC.Models.Pharmacy.ApplyOut applyOut = alData[0] as FS.HISFC.Models.Pharmacy.ApplyOut;
            string itemName = applyOut.Item.Name;
            FS.HISFC.Models.Pharmacy.Item item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(applyOut.Item.ID);
            if (item == null)
            {
                MessageBox.Show("打印标签时获取药品基本信息失败");
                return -1;
            }

            if (isPrintRegularName)
            {
               
                itemName = item.NameCollection.RegularName;
            }
            this.nlbDrugInfo1.Text = itemName;
            this.nlbSpecs1.Text = applyOut.Item.Specs;
            this.nlbOnceDose1.Text = "每次" + this.GetOnceDose(applyOut);
            //数量显示处理
            string applyPackQty = "";
            if (qtyShowType == "包装单位")
            {
                int applyQtyInt = 0;//这个取得商，是整包装单位的量，必须是整数
                decimal applyRe = 0;//这个取得余数，是最小单位的量，可能是小数
                applyQtyInt = (int)(applyOut.Operation.ApplyQty * applyOut.Days / applyOut.Item.PackQty);
                applyRe = applyOut.Operation.ApplyQty * applyOut.Days - applyQtyInt * applyOut.Item.PackQty;
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
                applyPackQty = (applyOut.Operation.ApplyQty * applyOut.Days).ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut.Item.MinUnit;
            }
            this.nlbDrugQty1.Text = "总量" + applyPackQty;
            FS.HISFC.Models.Order.OutPatient.Order order = SOC.Local.DrugStore.Common.Function.GetOrder(applyOut.OrderNO);
            if (order != null)
            {
                switch ((Int32)order.HypoTest)
                {
                    case 1:
                        if (item.IsAllergy)
                        {
                            this.nlbDrugInfo1.Text += "(免试)";
                        }
                        break;
                    case 2:
                        this.nlbDrugInfo1.Text += "(皮试)";
                        break;
                    case 3:
                        this.nlbDrugInfo1.Text += "(阳性)";
                        break;
                    case 4:
                        this.nlbDrugInfo1.Text += "(阴性)";
                        break;
                    default:
                        break;
                }
            }
            #endregion

            #region 第二个药品
            applyOut = alData[1] as FS.HISFC.Models.Pharmacy.ApplyOut;
            itemName = applyOut.Item.Name;

            item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(applyOut.Item.ID);
            if (item == null)
            {
                MessageBox.Show("打印标签时获取药品基本信息失败");
                return -1;
            }

            if (isPrintRegularName)
            {
                itemName = item.NameCollection.RegularName;
            }
            this.nlbDrugInfo2.Text = itemName;
            this.nlbSpecs2.Text = applyOut.Item.Specs;
            this.nlbOnceDose2.Text = "每次" + this.GetOnceDose(applyOut);
            //数量显示处理
            applyPackQty = "";
            if (qtyShowType == "包装单位")
            {
                int applyQtyInt = 0;//这个取得商，是整包装单位的量，必须是整数
                decimal applyRe = 0;//这个取得余数，是最小单位的量，可能是小数
                applyQtyInt = (int)(applyOut.Operation.ApplyQty * applyOut.Days / applyOut.Item.PackQty);
                applyRe = applyOut.Operation.ApplyQty * applyOut.Days - applyQtyInt * applyOut.Item.PackQty;
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
                applyPackQty = (applyOut.Operation.ApplyQty * applyOut.Days).ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut.Item.MinUnit;
            }
            this.nlbDrugQty2.Text = "总量" + applyPackQty;
            order = SOC.Local.DrugStore.Common.Function.GetOrder(applyOut.OrderNO);
            if (order != null)
            {
                switch ((Int32)order.HypoTest)
                {
                    case 1:
                        if (item.IsAllergy)
                        {
                            this.nlbDrugInfo2.Text += "(免试)";
                        }
                        break;
                    case 2:
                        this.nlbDrugInfo2.Text += "(皮试)";
                        break;
                    case 3:
                        this.nlbDrugInfo2.Text += "(阳性)";
                        break;
                    case 4:
                        this.nlbDrugInfo2.Text += "(阴性)";
                        break;
                    default:
                        break;
                }
            }
            #endregion

            #region 第三个药品
            applyOut = alData[2] as FS.HISFC.Models.Pharmacy.ApplyOut;
            itemName = applyOut.Item.Name;

            item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(applyOut.Item.ID);
            if (item == null)
            {
                MessageBox.Show("打印标签时获取药品基本信息失败");
                return -1;
            }

            if (isPrintRegularName)
            {
                itemName = item.NameCollection.RegularName;
            }
            this.nlbDrugInfo3.Text = itemName;
            this.nlbSpecs3.Text = applyOut.Item.Specs;
            this.nlbOnceDose3.Text = "每次" + this.GetOnceDose(applyOut);
            //数量显示处理
            applyPackQty = "";
            if (qtyShowType == "包装单位")
            {
                int applyQtyInt = 0;//这个取得商，是整包装单位的量，必须是整数
                decimal applyRe = 0;//这个取得余数，是最小单位的量，可能是小数
                applyQtyInt = (int)(applyOut.Operation.ApplyQty * applyOut.Days / applyOut.Item.PackQty);
                applyRe = applyOut.Operation.ApplyQty * applyOut.Days - applyQtyInt * applyOut.Item.PackQty;
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
                applyPackQty = (applyOut.Operation.ApplyQty * applyOut.Days).ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut.Item.MinUnit;
            }
            this.nlbDrugQty3.Text = "总量" + applyPackQty;
            order = SOC.Local.DrugStore.Common.Function.GetOrder(applyOut.OrderNO);
            if (order != null)
            {
                switch ((Int32)order.HypoTest)
                {
                    case 1:
                        if (item.IsAllergy)
                        {
                            this.nlbDrugInfo3.Text += "(免试)";
                        }
                        break;
                    case 2:
                        this.nlbDrugInfo3.Text += "(皮试)";
                        break;
                    case 3:
                        this.nlbDrugInfo3.Text += "(阳性)";
                        break;
                    case 4:
                        this.nlbDrugInfo3.Text += "(阴性)";
                        break;
                    default:
                        break;
                }
            }
            #endregion

            #region 第四个药品
            applyOut = alData[3] as FS.HISFC.Models.Pharmacy.ApplyOut;
            itemName = applyOut.Item.Name;

            item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(applyOut.Item.ID);
            if (item == null)
            {
                MessageBox.Show("打印标签时获取药品基本信息失败");
                return -1;
            }

            if (isPrintRegularName)
            {
                itemName = item.NameCollection.RegularName;
            }

            this.nlbDrugInfo4.Text = itemName;
            this.nlbSpecs4.Text = applyOut.Item.Specs;
            this.nlbOnceDose4.Text = "每次" + this.GetOnceDose(applyOut);
            //数量显示处理
            applyPackQty = "";
            if (qtyShowType == "包装单位")
            {
                int applyQtyInt = 0;//这个取得商，是整包装单位的量，必须是整数
                decimal applyRe = 0;//这个取得余数，是最小单位的量，可能是小数
                applyQtyInt = (int)(applyOut.Operation.ApplyQty * applyOut.Days / applyOut.Item.PackQty);
                applyRe = applyOut.Operation.ApplyQty * applyOut.Days - applyQtyInt * applyOut.Item.PackQty;
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
                applyPackQty = (applyOut.Operation.ApplyQty * applyOut.Days).ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut.Item.MinUnit;
            }
            this.nlbDrugQty4.Text = "总量" + applyPackQty;
            order = SOC.Local.DrugStore.Common.Function.GetOrder(applyOut.OrderNO);
            if (order != null)
            {
                switch ((Int32)order.HypoTest)
                {
                    case 1:
                        if (item.IsAllergy)
                        {
                            this.nlbDrugInfo4.Text += "(免试)";
                        }
                        break;
                    case 2:
                        this.nlbDrugInfo4.Text += "(皮试)";
                        break;
                    case 3:
                        this.nlbDrugInfo4.Text += "(阳性)";
                        break;
                    case 4:
                        this.nlbDrugInfo4.Text += "(阴性)";
                        break;
                    default:
                        break;
                }
            }
            #endregion


            //其它信息

            this.lbUsage.Text = SOC.HISFC.BizProcess.Cache.Common.GetUsageName(applyOut.Usage.ID);

            this.nlbFrequence.Text = this.GetFrequenceName(applyOut.Frequency);

            this.nlbRePrint.Visible = !(drugRecipe.RecipeState == "0");

            this.nlbRecipeNO.Text = drugRecipe.RecipeNO + "方";

            this.BackColor = System.Drawing.Color.White;

            //医生
            this.nlbDoctor.Text = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(drugRecipe.Doct.ID);

            this.Print();
            return 1;
        }

    }
    
}
