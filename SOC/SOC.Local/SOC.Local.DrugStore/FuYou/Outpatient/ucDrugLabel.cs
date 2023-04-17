using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.DrugStore.FuYou.Outpatient
{
    public partial class ucDrugLabel : UserControl

    {
        public ucDrugLabel()
        {
            InitializeComponent();
        }

        System.Collections.ArrayList alApplyOut;

        FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
        FS.HISFC.Models.Base.PageSize pageSize;

        /// <summary>
        /// 记录组合情况
        /// </summary>
        System.Collections.Hashtable hsCombo = new System.Collections.Hashtable();        
       
        /// <summary>
        /// 清除显示信息
        /// </summary>
        private void Clear()
        {
           
            this.lbPatientName.Text = "";
            this.nlbPrintTime.Text = "";
            this.nlbPageNO.Text = "";
            this.lbDrugInfo.Text = "";
            this.lblSpec.Text = "";
            this.lbUsage.Text = "";
            this.nlbFrequence.Text = "";
            this.nlbOnceDose.Text = "";
            this.nlbMemo.Text = "";
            this.nlbRecipeNO.Text = "";

            this.nlbSendTerminal.Text = "";
            this.nlbComboNO.Text = "";
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
                    pageSize = new FS.HISFC.Models.Base.PageSize("OutPatientDrugLabel", 260, 200);
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

            return Common.Function.GetOnceDose(applyOut);
        }

        /// <summary>
        /// 打印标签
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="drugRecipe"></param>
        /// <param name="drugTerminal"></param>
        /// <returns></returns>
        public int PrintDrugLabel(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal, bool isPrintRegularName, string hospitalName, bool isPrintMemo, string qtyShowType, DateTime printTime)
        {
            if (alData == null || drugRecipe == null || drugTerminal == null)
            {
                return -1;
            }
            if (alApplyOut != null)
            {
                alApplyOut.Clear();
            }
            this.alApplyOut = alData;

            hsCombo.Clear();
            int comboNO = 1;
            int pageNO = 1;
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alData)
            {
                this.Clear();
                this.lbPatientName.Text = drugRecipe.PatientName;
                this.nlbPrintTime.Text = printTime.ToString();
                this.nlbPageNO.Text = pageNO.ToString() + "/" + alData.Count.ToString();
                string itemName = applyOut.Item.Name;
                if (isPrintRegularName)
                {
                    FS.HISFC.Models.Pharmacy.Item item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(applyOut.Item.ID);
                    if (item == null)
                    {
                        MessageBox.Show("打印标签时获取药品基本信息失败");
                        return -1;
                    }

                    itemName = item.NameCollection.RegularName;
                }
                this.lbDrugInfo.Text = itemName;
                this.lblSpec.Text = "规格：" + applyOut.Item.Specs;

                this.lbUsage.Text = SOC.HISFC.BizProcess.Cache.Common.GetUsageName(applyOut.Usage.ID);

                this.nlbFrequence.Text = this.GetFrequenceName(applyOut.Frequency);

                FS.HISFC.Models.Order.OutPatient.Order order = Common.Function.GetOrder(applyOut.OrderNO);

                if (order!=null)
                {
                    string onceDose = "";
                    if (!string.IsNullOrEmpty(order.DoseOnceDisplay))
                    {
                        onceDose = "每次" + order.DoseOnceDisplay + order.DoseUnit;
                    }
                    else
                    {
                        onceDose = "每次" + this.GetOnceDose(applyOut);
                    }
                    this.nlbOnceDose.Text = onceDose;

                    this.nlbMemo.Visible = isPrintMemo;
                    this.nlbMemo1.Visible = isPrintMemo;
                    try
                    {
                        this.nlbMemo.Text = order.Memo;
                    }
                    catch { }
                }
               
                this.nlbRePrint.Visible = !(drugRecipe.RecipeState == "0");
                try
                {
                    this.nlbSendTerminal.Text = FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugTerminal.Dept.ID) + drugTerminal.Name;
                }
                catch (Exception)
                {
                    this.nlbSendTerminal.Text =  drugTerminal.Name;
                }
                
                this.nlbHospitalInfo.Text = string.IsNullOrEmpty(hospitalName) ? "顺德妇幼保健院" : hospitalName;
                this.nlbRecipeNO.Text = drugRecipe.RecipeNO + "方";

                //int x = this.GetHospitalNameLocationX();
                //this.nlbHospitalInfo.Location = new Point(x, this.nlbHospitalInfo.Location.Y);
                if (!string.IsNullOrEmpty(applyOut.CombNO))
                {
                    if (!hsCombo.Contains(applyOut.CombNO))
                    {
                        hsCombo.Add(applyOut.CombNO, comboNO.ToString());
                        comboNO++;
                    }
                    this.nlbComboNO.Text = hsCombo[applyOut.CombNO].ToString() + "组";
                }
                else
                {
                    this.nlbComboNO.Text = "";
                }
                //数量显示处理
                string applyPackQty = "";
                //string price = applyOut.Item.PriceCollection.RetailPrice.ToString() + "元" + "/" + applyOut.Item.PackUnit;
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
                        applyPackQty += applyRe.ToString().TrimEnd('0').TrimEnd('.') + applyOut.Item.MinUnit;
                    }
                }
                else
                {
                    applyPackQty = (applyOut.Operation.ApplyQty * applyOut.Days).ToString().TrimEnd('0').TrimEnd('.') + applyOut.Item.MinUnit;
                    //price = (applyOut.Item.PriceCollection.RetailPrice / applyOut.Item.PackQty).ToString("F4") + "元" + "/" + applyOut.Item.MinUnit;
                }
                this.nlbDrugQty.Text = "共" + applyPackQty;
                this.BackColor = System.Drawing.Color.White;
                this.Print();
                pageNO++;
            }
            return 1;
        }
        private string fileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\OutpatientDrugStorePrintSetting.xml";

        private int GetHospitalNameLocationX()
        {
            return FS.FrameWork.Function.NConvert.ToInt32(SOC.Public.XML.SettingFile.ReadSetting(fileName, "Label", "HospitalNameLocationX", this.nlbHospitalInfo.Location.X.ToString()));
        }
    }
}
