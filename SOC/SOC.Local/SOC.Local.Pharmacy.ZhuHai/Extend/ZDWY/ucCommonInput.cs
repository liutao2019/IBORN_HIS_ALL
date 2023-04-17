using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Pharmacy.ZhuHai.Extend.ZDWY
{
    /// <summary>
    /// [功能描述: 入库明细录入]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-3]<br></br>
    /// 说明：没有业务流程，仅数据显示、获取用户修改结果
    /// </summary>
    public partial class ucCommonInput : UserControl, SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IInputInfoControl
    {
        public ucCommonInput()
        {
            InitializeComponent();
            allBaseDrugDic = consMgr.GetAllList("BASEDRUGCODE");
            foreach (FS.FrameWork.Models.NeuObject consInfo in allBaseDrugDic)
            {
                if (consInfo.Memo == "基药")
                {
                    alBaseDrugDic.Add(consInfo.ID);
                }
                if (consInfo.Memo == "疫苗")
                {
                    alYMDrugDic.Add(consInfo.ID);
                }
            }
        }

        /// <summary>
        /// 配置文件
        /// </summary>
        private string settingFile = "";

        /// <summary>
        /// 金额的小数位
        /// </summary>
        private uint costDecimals = 2;

        /// <summary>
        /// 当前药品基本信息，必须是和字典一致的
        /// 入库信息中的药品基本信息和此处记录的信息是不同的
        /// </summary>
        private FS.HISFC.Models.Pharmacy.Item curItem;

        /// <summary>
        /// 常数管理类
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant consMgr = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 价格调价公式
        /// </summary>
        private Hashtable hsFormula = new Hashtable();

        /// <summary>
        /// 调价管理类
        /// </summary>
        private FS.SOC.HISFC.BizLogic.Pharmacy.Adjust adjustMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Adjust();

        /// <summary>
        /// 基药字典
        /// </summary>
        ArrayList alBaseDrugDic = new ArrayList();

        /// <summary>
        /// 疫苗字典
        /// </summary>
        ArrayList alYMDrugDic = new ArrayList();

        /// <summary>
        /// 基药字典
        /// </summary>
        ArrayList allBaseDrugDic = new ArrayList();

        /// <summary>
        /// 设置控件的Enable属性，对不是必须录入的信息方便用户跳过
        /// </summary>
        /// <param name="c"></param>
        private void SetControlEnable(Control c)
        {
            string enable = "True";
            enable = SOC.Public.XML.SettingFile.ReadSetting(this.settingFile, "CommonInputControlEnable", c.Name, enable);
            c.Enabled = FS.FrameWork.Function.NConvert.ToBoolean(enable);
            if (!c.Enabled && c is TextBox)
            {
                ((TextBox)c).ReadOnly = true;
            }
        }

        /// <summary>
        /// 保存代表输入完成的控件
        /// </summary>
        private void SaveInputCompletedControl()
        {
            SOC.Public.XML.SettingFile.SaveSetting(this.settingFile, "InputCompletedControl", "Name", this.ntxtMemo.Name);
        }

        /// <summary>
        /// 获取代码录入完成的控件名称，用户在这个控件回车后就表示入库信息录入完成
        /// </summary>
        /// <returns></returns>
        private string GetInputCompletedControlName()
        {
            string controlName = "";
            controlName = SOC.Public.XML.SettingFile.ReadSetting(this.settingFile, "InputCompletedControl", "Name", controlName);
            return controlName;
        }

        private void SaveControlEnable(Control c)
        {
            string enable = "True";
            if (!c.Enabled)
            {
                enable = "False";
            }
            SOC.Public.XML.SettingFile.SaveSetting(this.settingFile, "CommonInputControlEnable", c.Name, enable);
        }

        /// <summary>
        /// 气泡提示信息
        /// </summary>
        protected void ShowBalloonTip(int timeOut, string title, string tipText, ToolTipIcon toolTipIcon)
        {
            if (this.DesignMode)
            {
                return;
            }
            if (this.ParentForm != null)
            {
                this.ParentForm.ShowInTaskbar = true;
                this.notifyIcon1.Visible = true;
                this.notifyIcon1.ShowBalloonTip(timeOut, title, tipText, toolTipIcon);
            }
        }

        /// <summary>
        /// 尝试向时间转换，如果不满足时间格式，则返回本身
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string TryConvertToDateTimeFormat(string text)
        {
            string value = text;
            if (!value.Contains('-'))
            {
                if (value.Length == 8)
                {
                    //20110301
                    value = value.Insert(4, "-");
                    //2011-0301
                    value = value.Insert(7, "-");
                }
                else if (value.Length == 6)
                {
                    if (value.StartsWith("20"))
                    {
                        //201131
                        value = value.Insert(4, "-");
                        value = value.Insert(6, "-");
                    }
                    else
                    {
                        //110301
                        value = value.Insert(0, "20");
                        value = value.Insert(4, "-");
                        value = value.Insert(7, "-");
                    }
                }

            }

            DateTime dt;
            if (DateTime.TryParse(value, out dt))
            {
                return value;
            }
            return text;
        }

        void ncmbItemUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.nlbNote.Text = string.Empty;
            nlbRate1.Text = "";
            if (this.curItem == null)
            {
                return;
            }
            if (this.curItem.PackQty == 0)
            {
                Function.ShowMessage("药品基本信息中包装数量有误，请通知相关人维护!\n有疑问请与系统管理员联系!", MessageBoxIcon.Information);
                return;
            }
            if (this.ncmbItemUnit.Tag != null && this.ncmbItemUnit.Tag.ToString() == "1")
            {
                this.ntxtRetailPrice.Text = (this.curItem.PriceCollection.RetailPrice / this.curItem.PackQty).ToString("F7");//{A035B825-194E-4227-8301-491670535647}
                if (FS.FrameWork.Function.NConvert.ToDecimal(this.ntxtPurchasePrice.Text) == this.curItem.PriceCollection.PurchasePrice)
                {
                    this.ntxtPurchasePrice.Text = (this.curItem.PriceCollection.PurchasePrice / this.curItem.PackQty).ToString("F7");
                }
                else
                {
                    this.ShowBalloonTip(4000, "提示：", "请注意【购入价】和【单位】是否匹配!", ToolTipIcon.Info);
                }
            }
            else if (this.ncmbItemUnit.Tag != null && this.ncmbItemUnit.Tag.ToString() == "0")
            {
                this.ntxtRetailPrice.Text = this.curItem.PriceCollection.RetailPrice.ToString("F7");
                if (FS.FrameWork.Function.NConvert.ToDecimal(this.ntxtPurchasePrice.Text) == this.curItem.PriceCollection.PurchasePrice / this.curItem.PackQty)
                {
                    this.ntxtPurchasePrice.Text = this.curItem.PriceCollection.PurchasePrice.ToString("F7");
                }
                else
                {
                    this.ShowBalloonTip(4000, "提示：", "请注意【购入价】和【单位】是否匹配!", ToolTipIcon.Info);
                }
            }
        }

        void c_OnKeyPressSendTab(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                System.Windows.Forms.SendKeys.Send("{Tab}");
            }
        }

        void c_KeyPressAsCompleted(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (this.curInputCompletedEven != null)
                {
                    this.curInputCompletedEven();
                }
            }
        }

        void nbtOK_Click(object sender, EventArgs e)
        {
            if (this.curInputCompletedEven != null)
            {
                this.curInputCompletedEven();
            }
        }

        void nbtOK_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.curInputCompletedEven != null)
            {
                this.curInputCompletedEven();
            }
        }

        void ntxtPurchasePrice_TextChanged(object sender, EventArgs e)
        {
            this.nlbNote.Text = string.Empty;
            nlbRate1.Text = "";
            decimal price = 0;
            if (!string.IsNullOrEmpty(ntxtPurchasePrice.Text) && !decimal.TryParse(ntxtPurchasePrice.Text, out price))
            {
                Function.ShowMessage("购入价不是数字，请修改！", MessageBoxIcon.Information);
                ntxtPurchasePrice.Select();
                ntxtPurchasePrice.Focus();
            }
            else
            {
                if (this.curItem == null)
                {
                    return;
                }
                decimal qty = FS.FrameWork.Function.NConvert.ToDecimal(this.ntxtInputQty.Text);

                price = FS.FrameWork.Function.NConvert.ToDecimal(this.ntxtPurchasePrice.Text);
                decimal purchaseCost = price * qty;
                this.ntxtPurchaseCost.Text = purchaseCost.ToString("F" + this.costDecimals.ToString());
                if (curItem.Type.ID == "C") //中药不调价
                {
                    ntxtWholeSalePrice.Text = ntxtRetailPrice.Text;

                }
                else
                {//西药降价不调价
                    if (this.ncmbItemUnit.Tag != null && this.ncmbItemUnit.Tag.ToString() == "1") //小单位
                    {
                        if (price * curItem.PackQty > curItem.PriceCollection.PurchasePrice && Convert.ToDouble(price * curItem.PackQty )* 1.15 > Convert.ToDouble (curItem.PriceCollection.RetailPrice)) //价格变动
                        {
                            this.GenerateWholeSalePrice(curItem, price);
                        }
                        else //价格不变动
                        {
                            ntxtWholeSalePrice.Text = ntxtRetailPrice.Text;
                        }
                    }
                    else //大单位
                    {
                        if (price > curItem.PriceCollection.PurchasePrice &&Convert.ToDouble (price) * 1.15>Convert.ToDouble( curItem.PriceCollection.RetailPrice)) //价格变动
                        {
                            this.GenerateWholeSalePrice(curItem, price);
                        }
                        else ////价格不变动
                        {
                            ntxtWholeSalePrice.Text = ntxtRetailPrice.Text;
                        }
                    }
                }
            }
        }

        void ntxtWholeSalePrice_TextChanged(object sender, EventArgs e)
        {
            decimal price = 0;
            if (!string.IsNullOrEmpty(ntxtWholeSalePrice.Text) && !decimal.TryParse(ntxtWholeSalePrice.Text, out price))
            {
                Function.ShowMessage("加成价不是数字，请修改！", MessageBoxIcon.Information);
                ntxtWholeSalePrice.Select();
                ntxtWholeSalePrice.Focus();
            }
            else
            {
                if (this.curItem == null)
                {
                    return;
                }
                decimal qty = FS.FrameWork.Function.NConvert.ToDecimal(this.ntxtInputQty.Text);
                price = FS.FrameWork.Function.NConvert.ToDecimal(this.ntxtWholeSalePrice.Text);
                decimal wholeSaleCost = price * qty;
                this.ntxtWholeSaleCost.Text = wholeSaleCost.ToString("F" + this.costDecimals.ToString());
            }
            if (price != 0 && FS.FrameWork.Function.NConvert.ToDecimal(this.ntxtPurchasePrice.Text) != 0)
            {
                this.nlbRate1.Text = (FS.FrameWork.Function.NConvert.ToDecimal(this.ntxtWholeSalePrice.Text) / FS.FrameWork.Function.NConvert.ToDecimal(this.ntxtPurchasePrice.Text)).ToString("F2");
            }

        }


        /// <summary>
        /// 获取调价的公式
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected string GetWholeSalePriceComputeFormula(FS.HISFC.Models.Pharmacy.Item item)
        {
            string  drugType = string.Empty;
            drugType = this.GetItemBaseDrugInfo(item);
            string formula = "";

            FS.HISFC.Models.Pharmacy.Item itemTmp = item.Clone() as FS.HISFC.Models.Pharmacy.Item;

            if (!string.IsNullOrEmpty(itemTmp.BigPackQty) && itemTmp.BigPackQty != "0")
            {
                itemTmp.PriceCollection.PurchasePrice = itemTmp.PriceCollection.PurchasePrice * FS.FrameWork.Function.NConvert.ToDecimal(itemTmp.BigPackQty);
            }

            FS.SOC.HISFC.Models.Pharmacy.Adjust.SpecialDrugFormula specialDrugFormula = this.adjustMgr.GetAdjustPriceSpeFormula(item.ID);

            if (specialDrugFormula == null)
            {
                Function.ShowMessage("获取调价公式发生错误，请与系统管理员联系并报告错误信息：" + this.adjustMgr.Err, MessageBoxIcon.Error);
                return "";
            }
            if (specialDrugFormula.ValidState == "1" && !string.IsNullOrEmpty(specialDrugFormula.Formula))
            {
                formula = specialDrugFormula.Formula;
            }
            else
            {
                ArrayList alFormula = this.adjustMgr.QueryAdjustPriceFormula();
                if (alFormula == null)
                {
                    Function.ShowMessage("获取调价公式发生错误，请与系统管理员联系并报告错误信息：" + this.adjustMgr.Err, MessageBoxIcon.Error);
                    return "";
                }

                foreach (FS.SOC.HISFC.Models.Pharmacy.Adjust.RetailPriceFormula retailPriceFormula in alFormula)
                {
                    if (retailPriceFormula.ValidState != "1")
                    {
                        continue;
                    }

                    if (drugType != retailPriceFormula.FomulaType)
                    {
                        continue;
                    }

                    if (retailPriceFormula.DrugType.ID == item.Type.ID)
                    {
                        if (retailPriceFormula.PriceType == "0" && itemTmp.PriceCollection.PurchasePrice >= retailPriceFormula.PriceLower && itemTmp.PriceCollection.PurchasePrice <= retailPriceFormula.PriceUpper)
                        {
                            formula = retailPriceFormula.Name;
                        }
                        else if (retailPriceFormula.PriceType == "1" && itemTmp.PriceCollection.WholeSalePrice >= retailPriceFormula.PriceLower && itemTmp.PriceCollection.WholeSalePrice <= retailPriceFormula.PriceUpper)
                        {
                            formula = retailPriceFormula.Name;
                        }
                        else if (retailPriceFormula.PriceType == "2" && itemTmp.PriceCollection.RetailPrice >= retailPriceFormula.PriceLower && itemTmp.PriceCollection.RetailPrice <= retailPriceFormula.PriceUpper)
                        {
                            formula = retailPriceFormula.Name;
                        }
                    }
                }
            }
            //hsFormula.Add(item.ID, formula);{A035B825-194E-4227-8301-491670535647}

            if (!string.IsNullOrEmpty(itemTmp.BigPackQty) && itemTmp.BigPackQty != "0")
            {
                formula = formula.Replace("购入价", (itemTmp.PriceCollection.PurchasePrice).ToString("F7").TrimEnd('0').TrimEnd('.'));
                formula = formula.Replace("批发价", (itemTmp.PriceCollection.WholeSalePrice).ToString("F7").TrimEnd('0').TrimEnd('.'));
                formula = formula.Replace("零售价", (itemTmp.PriceCollection.RetailPrice).ToString("F7").TrimEnd('0').TrimEnd('.'));
            }
            else
            {
                formula = formula.Replace("购入价", item.PriceCollection.PurchasePrice.ToString("F7").TrimEnd('0').TrimEnd('.'));
                formula = formula.Replace("批发价", item.PriceCollection.WholeSalePrice.ToString("F7").TrimEnd('0').TrimEnd('.'));
                formula = formula.Replace("零售价", item.PriceCollection.RetailPrice.ToString("F7").TrimEnd('0').TrimEnd('.'));
            }


            return formula;
        }

        private string GetItemBaseDrugInfo(FS.HISFC.Models.Pharmacy.Item item)
        {
            string drugType = string.Empty;
            string extend2 = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(item.ID).ExtendData2;
            if (alBaseDrugDic.Contains(extend2))
            {
                drugType = "1";
            }
            else if (alYMDrugDic.Contains(extend2))
            {
                drugType = "2";
            }
            else
            {
                drugType = "0";
            }

            return drugType;
        }

        /// <summary>
        /// 根据调价公式生成加成价格 {312EEE24-0D77-4390-9EA6-D9537A38E416}{A035B825-194E-4227-8301-491670535647}
        /// </summary>
        private void GenerateWholeSalePrice(FS.HISFC.Models.Pharmacy.Item curItem, decimal price)
        {
            FS.HISFC.Models.Pharmacy.Item item = curItem.Clone() as FS.HISFC.Models.Pharmacy.Item;
            if (curItem.Type.ID == "C") //中药不调价
            {
                // item.PriceCollection.PurchasePrice = price;
                if (this.ncmbItemUnit.Tag != null && this.ncmbItemUnit.Tag.ToString() == "1")
                {
                    item.PriceCollection.PurchasePrice = curItem.PriceCollection.PurchasePrice / curItem.PackQty;
                }
            }
            else
            {//西药降价不调价
                if (this.ncmbItemUnit.Tag != null && this.ncmbItemUnit.Tag.ToString() == "1") //小单位
                {
                    if (price * curItem.PackQty > curItem.PriceCollection.PurchasePrice) //价格变动
                    {
                        item.PriceCollection.PurchasePrice = price;
                    }
                    else //价格不变动
                    {
                        item.PriceCollection.PurchasePrice = item.PriceCollection.PurchasePrice / curItem.PackQty;
                    }
                }
                else //大单位
                {
                    if (price > curItem.PriceCollection.PurchasePrice) //价格变动
                    {
                        item.PriceCollection.PurchasePrice = price;
                    }
                    else ////价格不变动
                    {

                    }
                }
            }
            decimal bigPackQty = 0;
  
            string fomula = this.GetWholeSalePriceComputeFormula(item);
            decimal curWholeSalePrice = FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Public.String.ExpressionVal(fomula));//加成价格
            //{312EEE24-0D77-4390-9EA6-D9537A38E416}
            if (!string.IsNullOrEmpty(curItem.BigPackQty) && curItem.BigPackQty != "0")
            {
                this.ntxtWholeSalePrice.Text = (curWholeSalePrice/FS.FrameWork.Function.NConvert.ToDecimal(curItem.BigPackQty)).ToString("F7");
            }
            else
            {
                this.ntxtWholeSalePrice.Text = curWholeSalePrice.ToString("F7");
            }
            if (curItem.PriceCollection.TopRetailPrice > 0 && curWholeSalePrice > curItem.PriceCollection.TopRetailPrice)
            {
                this.nlbNote.Text = "请注意：" + curItem.Name + "当前零售价大于最高限价！";
            }
            else
            {
                this.nlbNote.Text = string.Empty;
            }
            this.nlbFumula1.Text = fomula;
        }

        void ntxtRetailPrice_TextChanged(object sender, EventArgs e)
        {

            decimal price = 0;
            if (!string.IsNullOrEmpty(this.ntxtRetailPrice.Text) && !decimal.TryParse(ntxtRetailPrice.Text, out price))
            {
                Function.ShowMessage("零售价不是数字，请修改！", MessageBoxIcon.Information);
                ntxtRetailPrice.Select();
                ntxtRetailPrice.Focus();
            }
            else
            {
                if (this.curItem == null)
                {
                    return;
                }
                decimal qty = FS.FrameWork.Function.NConvert.ToDecimal(this.ntxtInputQty.Text);

                price = FS.FrameWork.Function.NConvert.ToDecimal(ntxtRetailPrice.Text);
                decimal retailCost = price * qty;
                this.ntxtRetailCost.Text = retailCost.ToString("F" + this.costDecimals.ToString());

            }
        }

        void ntxtInputQty_TextChanged(object sender, EventArgs e)
        {
            decimal qty = 0;
            if (!string.IsNullOrEmpty(ntxtInputQty.Text) && !decimal.TryParse(ntxtInputQty.Text, out qty))
            {
                Function.ShowMessage("入库数量不是数字，请修改！", MessageBoxIcon.Information);
                ntxtInputQty.Select();
                ntxtInputQty.Focus();
            }
            else
            {
                if (this.curItem == null)
                {
                    return;
                }
                qty = FS.FrameWork.Function.NConvert.ToDecimal(this.ntxtInputQty.Text);

                decimal price = FS.FrameWork.Function.NConvert.ToDecimal(ntxtRetailPrice.Text);
                decimal retailCost = price * qty;
                this.ntxtRetailCost.Text = retailCost.ToString("F" + this.costDecimals.ToString());

                price = FS.FrameWork.Function.NConvert.ToDecimal(this.ntxtPurchasePrice.Text);
                decimal purchaseCost = price * qty;
                this.ntxtPurchaseCost.Text = purchaseCost.ToString("F" + this.costDecimals.ToString());
                price = FS.FrameWork.Function.NConvert.ToDecimal(this.ntxtWholeSalePrice.Text);
                decimal wholeSaleCost = price * qty;
                this.ntxtWholeSaleCost.Text = wholeSaleCost.ToString("F" + this.costDecimals.ToString());
            }
        }

        void ntxtValidDate_Leave(object sender, EventArgs e)
        {
            //DateTime time;
            //if (!string.IsNullOrEmpty(this.ntxtValidDate.Text) && !DateTime.TryParse(this.ntxtValidDate.Text, out time))
            //{
            //    Function.ShowMessage("有效期不是时间格式：yyyy-MM-dd，请修改！", MessageBoxIcon.Error);
            //    this.ntxtValidDate.Select();
            //    this.ntxtValidDate.Focus();
            //}

            this.ntxtValidDate.Text = this.TryConvertToDateTimeFormat(this.ntxtValidDate.Text);
        }

        void ntxtInvoiceDate_Leave(object sender, EventArgs e)
        {
            //DateTime time;
            //if (!string.IsNullOrEmpty(this.ntxtInvoiceDate.Text) && !DateTime.TryParse(this.ntxtInvoiceDate.Text, out time))
            //{
            //    Function.ShowMessage("发票日期不是时间格式：yyyy-MM-dd，请修改！", MessageBoxIcon.Error);
            //    this.ntxtInvoiceDate.Select();
            //    this.ntxtInvoiceDate.Focus();
            //}
            this.ntxtInvoiceDate.Text = this.TryConvertToDateTimeFormat(this.ntxtInvoiceDate.Text);
        }

        void c_Enter(object sender, EventArgs e)
        {
            try
            {
                ((TextBox)sender).SelectAll();
            }
            catch { }
        }

        void notifyIcon1_Click(object sender, EventArgs e)
        {
            this.notifyIcon1.Visible = false;
        }

        void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            this.notifyIcon1.Visible = false;
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.F5)
            {
                if (this.curInputCompletedEven != null)
                {
                    this.curInputCompletedEven();
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        #region IInputInfoControl 成员

        FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.InputCompletedHander curInputCompletedEven;

        /// <summary>
        /// 清空显示数据
        /// </summary>
        /// <returns></returns>
        public int Clear()
        {
            foreach (Control c in ngbInputInfo.Controls)
            {
                if (c is Label)
                {
                    if (c.Name == this.nlbTradeName1.Name||c.Name == this.nlbRate1.Name||c.Name == this.nlbFumula1.Name)
                    {
                        c.Text = "";
                    }
                    continue;
                }
                else if (c is Button)
                {
                    continue;
                }
                else if (c is CheckBox)
                {
                    (c as CheckBox).Checked = false;
                    continue;
                }
                else if (c.Name == "nTxtDeliverTime")
                {
                    continue;
                }
                else if (c.Name == "ncbManagerOper")
                {
                    continue;
                }
                else if (c.Name == "ncbBuyOper")
                {
                    continue;
                }
                else if (c.Name == this.ntxtInvoiceDate.Name)
                {
                    continue;
                }
                else if (c.Name == this.ntxtInvoiceNO.Name)
                {
                    continue;
                }
                c.Text = "";
            }

            this.nlbItemBaseinfo.Text = "请在左侧列表中选择药品";
            this.ncmbItemUnit.AddItems(new ArrayList());

            return 1;
        }

        /// <summary>
        /// 清空信息
        /// </summary>
        /// <param name="isClearInvoiceInfo">是否清空发票信息</param>
        /// <param name="isClearDeliveryInfo">是否清空送货单号信息</param>
        /// <returns></returns>
        public int Clear(bool isClearInvoiceInfo, bool isClearDeliveryInfo)
        {
            foreach (Control c in ngbInputInfo.Controls)
            {
                if (c is Label)
                {
                    continue;
                }
                else if (c is Button)
                {
                    continue;
                }
                else if (c is CheckBox)
                {
                    (c as CheckBox).Checked = false;
                    continue;
                }
                else if (c.Name == "ncbManagerOper")
                {
                    continue;
                }
                else if (c.Name == "ncbBuyOper")
                {
                    continue;
                }
                if (!isClearInvoiceInfo)
                {
                    if (c == this.ntxtInvoiceNO)
                    {
                        continue;
                    }
                    else if (c.Name == "nTxtDeliverTime")
                    {
                        continue;
                    }
                    else if (c == this.ntxtInvoiceDate)
                    {
                        continue;
                    }
                }
                if (!isClearDeliveryInfo && c == this.ntxtDeliveryNO)
                {
                    continue;
                }
                c.Text = "";
            }


            this.nlbItemBaseinfo.Text = "请在左侧列表中选择药品";
            this.ncmbItemUnit.AddItems(new ArrayList());

            return 1;
        }

        /// <summary>
        /// 初始化
        /// 控件Enabled属性
        /// </summary>
        /// <returns></returns>
        public int Init()
        {
            int param = 1;
            this.settingFile = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\PharmacyCommonInput.xml";
            BizExtendInterfaceImplement BizExtendInterfaceImplement = new BizExtendInterfaceImplement();
            this.costDecimals = BizExtendInterfaceImplement.GetCostDecimals("0310", "11", "0");

            #region Enable属性设置
            if (System.IO.File.Exists(this.settingFile))
            {
                foreach (Control c in this.ngbInputInfo.Controls)
                {
                    this.SetControlEnable(c);
                    if (c.Name == this.GetInputCompletedControlName())
                    {
                        if (!c.Enabled)
                        {
                            Function.ShowMessage("控件设置错误：代表入库信息录入完成的控件" + c.Name + "属性Enable为False\n程序将无法确认信息录入是否完成", MessageBoxIcon.Error);
                            //界面显示后将无法录入数据，暂时不返回
                            param = -1;
                        }
                        else
                        {
                            //用户在这个控件回车后就表示入库信息录入完成
                            c.KeyPress += new KeyPressEventHandler(c_KeyPressAsCompleted);
                            if (c is FS.FrameWork.WinForms.Controls.NeuTextBox)
                            {
                                ((FS.FrameWork.WinForms.Controls.NeuTextBox)c).IsEnter2Tab = false;
                            }
                        }
                    }
                    if (c is TextBox)
                    {
                        if (c.Name == this.ntxtInputQty.Name)
                        {
                            c.LostFocus += new EventHandler(c_LostFocus);
                        }
                        c.Enter += new EventHandler(c_Enter);
                    }
                }
            }
            else
            {
                foreach (Control c in this.ngbInputInfo.Controls)
                {
                    this.SaveControlEnable(c);
                }
            }
            #endregion

            #region 事件初始化
            this.ncmbItemUnit.SelectedIndexChanged -= new EventHandler(ncmbItemUnit_SelectedIndexChanged);
            this.ncmbItemUnit.SelectedIndexChanged += new EventHandler(ncmbItemUnit_SelectedIndexChanged);
            this.ncmbItemUnit.KeyPress -= new KeyPressEventHandler(c_OnKeyPressSendTab);
            this.ncmbItemUnit.KeyPress += new KeyPressEventHandler(c_OnKeyPressSendTab);

            //this.ncmbInvoiceType.KeyPress -= new KeyPressEventHandler(c_OnKeyPressSendTab);
            //this.ncmbInvoiceType.KeyPress += new KeyPressEventHandler(c_OnKeyPressSendTab);

            this.ncmbProducer.KeyPress -= new KeyPressEventHandler(c_OnKeyPressSendTab);
            this.ncmbProducer.KeyPress += new KeyPressEventHandler(c_OnKeyPressSendTab);

            this.nbtOK.Click -= new EventHandler(nbtOK_Click);
            this.nbtOK.Click += new EventHandler(nbtOK_Click);

            this.nbtOK.KeyPress -= new KeyPressEventHandler(nbtOK_KeyPress);
            this.nbtOK.KeyPress += new KeyPressEventHandler(nbtOK_KeyPress);

            this.ntxtInputQty.TextChanged -= new EventHandler(ntxtInputQty_TextChanged);
            this.ntxtInputQty.TextChanged += new EventHandler(ntxtInputQty_TextChanged);

            this.ntxtPurchasePrice.TextChanged -= new EventHandler(ntxtPurchasePrice_TextChanged);
            this.ntxtPurchasePrice.TextChanged += new EventHandler(ntxtPurchasePrice_TextChanged);

            this.ntxtRetailPrice.TextChanged -= new EventHandler(ntxtRetailPrice_TextChanged);
            this.ntxtRetailPrice.TextChanged += new EventHandler(ntxtRetailPrice_TextChanged);

            this.ntxtWholeSalePrice.TextChanged -= new EventHandler(ntxtWholeSalePrice_TextChanged);
            this.ntxtWholeSalePrice.TextChanged += new EventHandler(ntxtWholeSalePrice_TextChanged);

            this.ntxtValidDate.Leave -= new EventHandler(ntxtValidDate_Leave);
            this.ntxtValidDate.Leave += new EventHandler(ntxtValidDate_Leave);

            this.ntxtInvoiceDate.Leave -= new EventHandler(ntxtInvoiceDate_Leave);
            this.ntxtInvoiceDate.Leave += new EventHandler(ntxtInvoiceDate_Leave);

            this.notifyIcon1.Click -= new EventHandler(notifyIcon1_Click);
            this.notifyIcon1.Click += new EventHandler(notifyIcon1_Click);

            this.notifyIcon1.BalloonTipClicked -= new EventHandler(notifyIcon1_BalloonTipClicked);
            this.notifyIcon1.BalloonTipClicked += new EventHandler(notifyIcon1_BalloonTipClicked);

            #endregion

            #region 厂家

            SOC.HISFC.BizProcess.Cache.Pharmacy.InitProducer();
            this.ncmbProducer.alItems.Clear();
            this.ncmbProducer.AddItems(SOC.HISFC.BizProcess.Cache.Pharmacy.producerHelper.ArrayObject);

            #endregion

            #region 仓管员
            ArrayList allWareHouseWorkers = consMgr.GetAllList("WAREHOUSEWORKERS");
            this.ncbManagerOper.alItems.Clear();
            this.ncbManagerOper.AddItems(allWareHouseWorkers);
            this.ncbManagerOper.SelectedIndex = 0;
            #endregion

            #region 采购员
            ArrayList allBuyers = consMgr.GetAllList("BUYERS");
            this.ncbBuyOper.alItems.Clear();
            this.ncbBuyOper.AddItems(allBuyers);
            this.ncbBuyOper.SelectedIndex = 0;
            #endregion
            return param;
        }

        void c_LostFocus(object sender, EventArgs e)
        {
            if (((TextBox)sender).Name == this.ntxtInputQty.Name)
            {
                decimal qty = 0m;
                decimal.TryParse(this.ntxtInputQty.Text, out qty);
                if (qty == 0)
                {
                    this.ntxtInputQty.Focus();
                    this.ntxtInputQty.SelectAll();
                    return;
                }
            }
        }


        /// <summary>
        /// 设置焦点
        /// </summary>
        /// <returns></returns>
        public int SetFocusToInputQty()
        {
            this.ntxtInputQty.Select();
            this.ntxtInputQty.SelectAll();
            this.ntxtInputQty.Focus();
            //this.ntxtInvoiceNO.Select();
            //this.ntxtInvoiceNO.SelectAll();
            //this.ntxtInvoiceNO.Focus();

            return 1;
        }

        /// <summary>
        /// 将入库数据显示为药品基本信息实体
        /// 用于药品列表选中药品后
        /// </summary>
        /// <param name="item">药品</param>
        /// <param name="storage">最新库存信息</param>
        /// <param name="isUseBatchNO">是否使用库存信息的批号</param>
        /// <param name="defaultValidDate">默认有效期</param>
        /// <returns>-1失败</returns>
        public int SetInputObject(FS.HISFC.Models.Pharmacy.Item item, FS.HISFC.Models.Pharmacy.Storage storage, bool isUseBatchNO, DateTime defaultValidDate)
        {
            this.nlbNote.Text = string.Empty;
            nlbRate1.Text = "";
            if (item == null)
            {
                return -1;
            }
            if (item.PackQty == 0)
            {
                Function.ShowMessage("您要入库的药品基本信息中包装数量有误，请通知相关人维护!\n有疑问请与系统管理员联系!", MessageBoxIcon.Information);
                return -1;
            }
            this.curItem = item.Clone();

            #region 药品基本信息
            this.nlbItemBaseinfo.Text = curItem.Name
                + "   "
                + curItem.Specs
                + "   "
                + "购入价：" + curItem.PriceCollection.PurchasePrice.ToString("F4").TrimEnd('0').TrimEnd('.')
                + "  "
                + "加成价：" + curItem.PriceCollection.WholeSalePrice.ToString("F4").TrimEnd('0').TrimEnd('.')
                + "  "
                + "最高零售价" + curItem.PriceCollection.TopRetailPrice.ToString("F4").TrimEnd('0').TrimEnd('.')
                + "   "
                + "最小单位：" + curItem.MinUnit
                + "   "
                + "包装数量：" + curItem.PackQty.ToString("F4").TrimEnd('0').TrimEnd('.')
                + "   "
                + "包装单位：" + curItem.PriceUnit;
            #endregion

            #region 入库信息

            //药品名称
            this.nlbTradeName1.Text = curItem.Name + "   规格：" + curItem.Specs + "  单位：" + curItem.PackUnit; ;

            if (!string.IsNullOrEmpty(curItem.BigPackQty) && curItem.BigPackQty != "0")
            {
                this.nlbTradeName1.Text = this.nlbTradeName1.Text + " 大包装单位：" + curItem.BigPackUnit + " 大包装数量：" + curItem.BigPackQty;
            }

            //单位
            ArrayList alUnit = new ArrayList();
            FS.FrameWork.Models.NeuObject packUnit = new FS.FrameWork.Models.NeuObject();
            packUnit.ID = "0";
            packUnit.Name = curItem.PackUnit;
            alUnit.Add(packUnit);
            if (curItem.PackQty > 1)
            {
                FS.FrameWork.Models.NeuObject miniUnit = new FS.FrameWork.Models.NeuObject();
                miniUnit.ID = "1";
                miniUnit.Name = curItem.MinUnit;
                alUnit.Add(miniUnit);
            }
            this.ncmbItemUnit.AddItems(alUnit);
            this.ncmbItemUnit.Text = curItem.PackUnit;
            this.ncmbItemUnit.Tag = "0";

            //单价,使用单科调价的科室应该取库存价格，即使不用单科调价也可以取库存价格
            this.ntxtPurchasePrice.Text = curItem.PriceCollection.PurchasePrice.ToString();
            this.ntxtRetailPrice.Text = curItem.PriceCollection.RetailPrice.ToString();

            if (storage != null && storage.Item != null)
            {
                //this.ntxtPurchasePrice.Text = storage.Item.PriceCollection.PurchasePrice.ToString();
                this.ntxtRetailPrice.Text = storage.Item.PriceCollection.RetailPrice.ToString();
            }

            //批号
            if (isUseBatchNO && storage != null)
            {
                this.ntxtBatchNO.Text = storage.BatchNO;
            }

            //有效期
            if (defaultValidDate > DateTime.Now)
            {
                this.ntxtValidDate.Text = defaultValidDate.Date.ToString("yyyyMMdd");
            }
            if (string.IsNullOrEmpty(this.nTxtDeliverTime.Text))
            {
                this.nTxtDeliverTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            if (string.IsNullOrEmpty(this.ntxtInvoiceDate.Text))
            {
                this.ntxtInvoiceDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            //批准文号
            this.ntxtApproveNO.Text = curItem.Product.ApprovalInfo;

            //厂家
            this.ncmbProducer.Text = SOC.HISFC.BizProcess.Cache.Pharmacy.GetProducerName(curItem.Product.Producer.ID);
            this.ncmbProducer.Tag = curItem.Product.Producer.ID;

            //公式
            this.nlbFumula1.Text = this.GetWholeSalePriceComputeFormula(curItem);

            //是否招标药品
            this.ncbTendFlag.Checked = FS.FrameWork.Function.NConvert.ToBoolean(curItem.TenderOffer.IsTenderOffer);

            this.ntxtInputQty.Text = "0";

            #endregion

            return 1;

        }

        /// <summary>
        /// 将入库数据显示为药品基本信息实体
        /// 用于药品列表选中药品后
        /// </summary>
        /// <param name="item">药品</param>
        /// <returns>-1失败</returns>
        public int SetInputObject(FS.HISFC.Models.Pharmacy.Item item)
        {
            return this.SetInputObject(item, null, false, DateTime.Now.AddYears(-1));
        }

        /// <summary>
        /// 设置入库信息，双击明细数据FarPoint后修改入库信息调用
        /// </summary>
        /// <param name="input">入库实体</param>
        /// <param name="item">药品基本信息，重新查询数据后的基本信息</param>
        /// <returns>-1 失败</returns>
        public int SetInputObject(FS.HISFC.Models.Pharmacy.Input input, FS.HISFC.Models.Pharmacy.Item item)
        {
            if (input == null)
            {
                return -1;
            }
            if (item == null)
            {
                return -1;
            }
            if (item.PackQty == 0)
            {
                Function.ShowMessage("您要入库的药品基本信息中包装数量有误，请通知相关人维护!\n有疑问请与系统管理员联系!", MessageBoxIcon.Information);
                return -1;
            }

            this.Clear();
            this.curItem = item.Clone();

            #region 药品基本信息
            this.nlbItemBaseinfo.Text = curItem.Name
                + "   "
                + curItem.Specs
                + "   "
                + "购入价：" + curItem.PriceCollection.PurchasePrice.ToString("F4").TrimEnd('0').TrimEnd('.')
                + "  "
                + "加成价：" + curItem.PriceCollection.WholeSalePrice.ToString("F4").TrimEnd('0').TrimEnd('.')
                + "   "
                + "最高零售价" + curItem.PriceCollection.TopRetailPrice.ToString("F4").TrimEnd('0').TrimEnd('.')
                + "   "
                + "最小单位：" + curItem.MinUnit
                + "   "
                + "包装数量：" + curItem.PackQty.ToString("F4").TrimEnd('0').TrimEnd('.')
                + "   "
                + "包装单位：" + curItem.PriceUnit;
            #endregion

            #region 入库信息

            //单位
            ArrayList alUnit = new ArrayList();
            FS.FrameWork.Models.NeuObject packUnit = new FS.FrameWork.Models.NeuObject();
            packUnit.ID = "0";
            packUnit.Name = curItem.PackUnit;
            alUnit.Add(packUnit);
            if (curItem.PackQty > 1)
            {
                FS.FrameWork.Models.NeuObject miniUnit = new FS.FrameWork.Models.NeuObject();
                miniUnit.ID = "1";
                miniUnit.Name = curItem.MinUnit;
                alUnit.Add(miniUnit);
            }
            this.ncmbItemUnit.AddItems(alUnit);

            decimal purchasePrice = input.Item.PriceCollection.PurchasePrice;
            decimal retailPrice = input.Item.PriceCollection.RetailPrice;
            decimal wholeSalePrice = input.Item.PriceCollection.WholeSalePrice;

            if ((int)(input.Quantity / input.Item.PackQty) != input.Quantity / input.Item.PackQty)
            {
                purchasePrice = purchasePrice / input.Item.PackQty;
                retailPrice = retailPrice / input.Item.PackQty;
                wholeSalePrice = wholeSalePrice / input.Item.PackQty;

                this.ncmbItemUnit.Text = curItem.MinUnit;
                this.ncmbItemUnit.Tag = "1";

                this.ntxtInputQty.Text = input.Quantity.ToString("F4").TrimEnd('0').TrimEnd('.');
            }
            else
            {
                this.ncmbItemUnit.Text = curItem.PackUnit;
                this.ncmbItemUnit.Tag = "0";
                this.ntxtInputQty.Text = (input.Quantity / input.Item.PackQty).ToString("F4").TrimEnd('0').TrimEnd('.');
            }

            //单价 {312EEE24-0D77-4390-9EA6-D9537A38E416}
            this.ntxtPurchasePrice.Text = purchasePrice.ToString("F7").TrimEnd('0').TrimEnd('.');
            this.ntxtRetailPrice.Text = retailPrice.ToString("F7").TrimEnd('0').TrimEnd('.');
            this.ntxtWholeSalePrice.Text = wholeSalePrice.ToString("F7").TrimEnd('0').TrimEnd('.');

            //货位号
            //this.ntxtPlaceNO.Text = input.PlaceNO;

            //批号
            this.ntxtBatchNO.Text = input.BatchNO;

            //批准文号
            this.ntxtApproveNO.Text = input.Item.Product.ApprovalInfo;

            //有效期
            this.ntxtValidDate.Text = input.ValidTime.ToString("yyyy-MM-dd");

            //发票
            this.ntxtInvoiceNO.Text = input.InvoiceNO;
            if (input.InvoiceDate > DateTime.Now)
            {
                if (input.InvoiceDate.ToString("HH:mm:ss") == "00:00:00")
                {
                    this.ntxtInvoiceDate.Text = input.InvoiceDate.ToString("yyyy-MM-dd");
                }
                else
                {
                    this.ntxtInvoiceDate.Text = input.InvoiceDate.ToString("yyyy-MM-dd HH:mm:ss");
                }
            }

            this.nTxtDeliverTime.Text = input.User01;

            this.ncbManagerOper.Tag = input.User02;
            this.ncbManagerOper.Text = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(input.User02);

            this.ncbBuyOper.Tag = input.User03;
            this.ncbBuyOper.Text = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(input.User03);

            //this.ncmbInvoiceType.Tag = input.InvoiceType;
            //this.ncmbInvoiceType.Text = input.InvoiceType;

            //送货单号
            this.ntxtDeliveryNO.Text = input.DeliveryNO;

            //招标标记
            this.ncbTendFlag.Checked = input.Item.TenderOffer.IsTenderOffer;

            //厂家
            this.ncmbProducer.Tag = input.Producer.ID;
            if (string.IsNullOrEmpty(input.Producer.Name))
            {
                this.ncmbProducer.Text = SOC.HISFC.BizProcess.Cache.Pharmacy.GetProducerName(input.Producer.ID);
            }
            else
            {
                this.ncmbProducer.Text = input.Producer.Name;
            }

            //备注
            this.ntxtMemo.Text = input.Memo;

            #endregion

            return 1;
        }

        /// <summary>
        /// 获取入库信息（实体）
        /// </summary>
        /// <returns></returns>
        public FS.HISFC.Models.Pharmacy.Input GetInputObject()
        {
            FS.HISFC.Models.Pharmacy.Input input = new FS.HISFC.Models.Pharmacy.Input();
            if (this.curItem == null)
            {
                return null;
            }

            input.Item = this.curItem;

            //数量
            decimal qty = 0;
            if (decimal.TryParse(this.ntxtInputQty.Text, out qty))
            {
                //使用的是包装单位，数量乘以包装数量
                if (this.ncmbItemUnit.Tag.ToString() == "0")
                {
                    qty = qty * input.Item.PackQty;
                    input.ShowUnit = input.Item.PackUnit;
                    input.ShowState = "1";
                }
                else
                {
                    input.ShowUnit = input.Item.MinUnit;
                    input.ShowState = "0";
                }
            }
            else
            {
                Function.ShowMessage("入库数量不是数字，请修改！", MessageBoxIcon.Information);
                this.ntxtInputQty.Select();
                this.ntxtInputQty.Focus();
                return null;
            }
            if (qty <= 0)
            {
                Function.ShowMessage("入库数量必须大于零，请修改！", MessageBoxIcon.Information);
                this.ntxtInputQty.Select();
                this.ntxtInputQty.Focus();
                return null;
            }

            if (string.IsNullOrEmpty(this.ntxtValidDate.Text))// {47F1129F-CC17-41fa-9F1A-6D4E9085FD2E}
            {
                Function.ShowMessage("有效期不能为空！", MessageBoxIcon.Information);
                this.ntxtValidDate.Select();
                this.ntxtValidDate.Focus();
                return null;
            }
            input.Quantity = qty;

            //购入价
            decimal purchasePrice = 0;
            if (decimal.TryParse(this.ntxtPurchasePrice.Text, out purchasePrice))
            {
                //使用的是最小单位，价格乘以包装数量
                if (this.ncmbItemUnit.Tag.ToString() == "1")
                {
                    purchasePrice = purchasePrice * input.Item.PackQty;
                }
            }
            else
            {
                Function.ShowMessage("购入价不是数字，请修改！", MessageBoxIcon.Information);
                this.ntxtPurchasePrice.Select();
                this.ntxtPurchasePrice.Focus();
                return null;
            }
            if (purchasePrice < 0)
            {
                Function.ShowMessage("购入价必须大于等于零，请修改！", MessageBoxIcon.Information);
                this.ntxtPurchasePrice.Select();
                this.ntxtPurchasePrice.Focus();
                return null;
            }
            if (input.Item.PriceCollection.PurchasePrice != purchasePrice)
            {
                MessageBox.Show(input.Item.Name + "的购入价发生了变化，请通知调价", "提示!");
                this.ShowBalloonTip(5000, "温馨提示：", input.Item.Name + "的购入价发生了变化，请通知调价", ToolTipIcon.Info);
            }
            input.Item.PriceCollection.PurchasePrice = purchasePrice;

            //购入金额
            input.PurchaseCost = input.Item.PriceCollection.PurchasePrice * (input.Quantity / input.Item.PackQty);
            input.PurchaseCost = FS.FrameWork.Function.NConvert.ToDecimal(input.PurchaseCost.ToString("F" + this.costDecimals.ToString()));

            //加成价
            decimal wholeSalePrice = 0;
            if (decimal.TryParse(this.ntxtWholeSalePrice.Text, out wholeSalePrice))
            {
                //使用的是最小单位，价格乘以包装数量
                if (this.ncmbItemUnit.Tag.ToString() == "1")
                {
                    wholeSalePrice = wholeSalePrice * input.Item.PackQty;
                }
            }
            else
            {
                Function.ShowMessage("加成价不是数字，请修改！", MessageBoxIcon.Information);
                this.ntxtWholeSalePrice.Select();
                this.ntxtWholeSalePrice.Focus();
                return null;
            }
            if (wholeSalePrice < 0)
            {
                Function.ShowMessage("加成价必须大于等于零，请修改！", MessageBoxIcon.Information);
                this.ntxtPurchasePrice.Select();
                this.ntxtPurchasePrice.Focus();
                return null;
            }
            if (input.Item.PriceCollection.WholeSalePrice != wholeSalePrice)
            {
                this.ShowBalloonTip(5000, "温馨提示：", input.Item.Name + "的加成价发生了变化，请通知调价", ToolTipIcon.Info);
            }
            input.Item.PriceCollection.WholeSalePrice = wholeSalePrice;

            //加成金额
            input.WholeSaleCost = input.Item.PriceCollection.WholeSalePrice * (input.Quantity / input.Item.PackQty);
            input.WholeSaleCost = FS.FrameWork.Function.NConvert.ToDecimal(input.WholeSaleCost.ToString("F" + this.costDecimals.ToString()));

            if (input.Item.Type.ID != "C")
            {
                //批号
                if (string.IsNullOrEmpty(this.ntxtBatchNO.Text))
                {
                    Function.ShowMessage("生产批号不能为空，请修改！", MessageBoxIcon.Information);
                    this.ntxtBatchNO.Select();
                    this.ntxtBatchNO.Focus();
                    return null;
                }
                //生产厂家
                if (input.Item.Type.ID != "C" && string.IsNullOrEmpty(this.ncmbProducer.Text))
                {
                    Function.ShowMessage("生产厂家不能为空，请修改！", MessageBoxIcon.Information);
                    this.ncmbProducer.Select();
                    this.ncmbProducer.Focus();
                    return null;
                }
                if (SOC.Public.String.Length(this.ntxtBatchNO.Text) > 32)
                {
                    Function.ShowMessage("生产批号太长，允许范围32个字母（1个汉字按2个字母计算），请修改！", MessageBoxIcon.Information);
                    this.ntxtBatchNO.Select();
                    this.ntxtBatchNO.Focus();
                    return null;
                }
                input.BatchNO = this.ntxtBatchNO.Text;
            }
            else
            {
                if (string.IsNullOrEmpty(this.ntxtBatchNO.Text))
                {
                    input.BatchNo = "1";
                }
                else
                {
                    input.BatchNO = this.ntxtBatchNO.Text;
                }
            }

            //零售金额
            input.RetailCost = input.Item.PriceCollection.RetailPrice * (input.Quantity / input.Item.PackQty);
            input.RetailCost = FS.FrameWork.Function.NConvert.ToDecimal(input.RetailCost.ToString("F" + this.costDecimals.ToString()));

            //批发金额
            input.WholeSaleCost = input.Item.PriceCollection.WholeSalePrice * (input.Quantity / input.Item.PackQty);
            input.WholeSaleCost = FS.FrameWork.Function.NConvert.ToDecimal(input.WholeSaleCost.ToString("F" + this.costDecimals.ToString()));

            //批准文号，不判断长度，不够时考虑扩展数据库
            input.Item.Product.ApprovalInfo = this.ntxtApproveNO.Text.Trim();

            //调价公式
            input.Item.User03 = this.nlbFumula1.Text;

            //有效期
            if (!string.IsNullOrEmpty(this.ntxtValidDate.Text))
            {
                DateTime validDate;
                this.ntxtValidDate.Text = this.TryConvertToDateTimeFormat(this.ntxtValidDate.Text);

                if (DateTime.TryParse(this.ntxtValidDate.Text, out validDate))
                {
                    input.ValidTime = validDate.Date;
                }
                else
                {
                    Function.ShowMessage("有效期不是时间格式：yyyy-MM-dd，请修改！", MessageBoxIcon.Information);
                    this.ntxtValidDate.Select();
                    this.ntxtValidDate.Focus();
                    return null;
                }
            }
            else
            {
                input.ValidTime = DateTime.Now.AddYears(2).Date;
            }

            //{5D67E39A-C18B-4012-B4B0-3436BB914537}
            //#region 特殊处理中草药,饮片有效期为入库日期加六个月，颗粒剂为批号加3年
            //if (input.Item.Type.ID == "C")
            //{
            //    if (input.Item.Name.Contains("饮片"))
            //    {
            //        input.ValidTime = DateTime.Now.AddMonths(6).Date;
            //    }
            //    else
            //    {
            //        try
            //        {
            //            DateTime validDate;
            //            string validDateStr = input.BatchNO.Substring(0, 6);
            //            validDateStr = this.TryConvertToDateTimeFormat(validDateStr);
            //            if (DateTime.TryParse(validDateStr, out validDate))
            //            {
            //                input.ValidTime = validDate.AddMonths(36).Date;
            //            }
            //            else
            //            {
            //                input.ValidTime = DateTime.Now.AddMonths(36).Date;
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            input.ValidTime = DateTime.Now.AddMonths(36).Date;
            //        }
            //    }
            //}
            //#endregion

            //发票号，不判断长度，不够考虑扩展数据库
            input.InvoiceNO = this.ntxtInvoiceNO.Text.Trim();
            input.DeliveryNO = this.ntxtDeliveryNO.Text.Trim();

            if (!string.IsNullOrEmpty(this.ntxtInvoiceDate.Text))
            {
                DateTime invoiceDate;
                this.ntxtInvoiceDate.Text = this.TryConvertToDateTimeFormat(this.ntxtInvoiceDate.Text);
                if (DateTime.TryParse(this.ntxtInvoiceDate.Text, out invoiceDate))
                {
                    input.InvoiceDate = invoiceDate;
                }
                else
                {
                    Function.ShowMessage("发票日期不是时间格式：yyyy-MM-dd或者yyyy-MM-dd HH:mm:ss，请修改！", MessageBoxIcon.Information);
                    this.ntxtInvoiceDate.Select();
                    this.ntxtInvoiceDate.Focus();
                    return null;
                }
            }

            //发票类别
            //input.InvoiceType = this.ncmbInvoiceType.Text;
            //送单日期
            input.User01 = this.nTxtDeliverTime.Text;

            //生产厂家
            //if (!string.IsNullOrEmpty(this.ncmbProducer.Text) && this.ncmbProducer.Tag != null && this.ncmbProducer.Tag.ToString() != "")
            //{
            //    input.Producer.ID = this.ncmbProducer.Tag.ToString();
            //    input.Producer.Name = SOC.HISFC.BizProcess.Cache.Pharmacy.GetProducerName(input.Producer.ID);
            //}

            //生产厂家
            if (!string.IsNullOrEmpty(this.ncmbProducer.Text))
            {
                if (this.ncmbProducer.Tag == null || this.ncmbProducer.Tag.ToString() == "")
                {
                    input.Producer.ID = AutoSaveCompany();
                    input.Producer.Name = this.ncmbProducer.Text;
                }
                else
                {
                    if (SOC.HISFC.BizProcess.Cache.Pharmacy.GetProducerName(this.ncmbProducer.Tag.ToString()) != this.ncmbProducer.Text)
                    {
                        input.Producer.ID = AutoSaveCompany();
                        input.Producer.Name = this.ncmbProducer.Text;
                    }
                    else
                    {
                        input.Producer.ID = this.ncmbProducer.Tag.ToString();
                        input.Producer.Name = SOC.HISFC.BizProcess.Cache.Pharmacy.GetProducerName(input.Producer.ID);
                    }
                }
            }

            //招标标记
            input.Item.TenderOffer.IsTenderOffer = this.ncbTendFlag.Checked;

            //仓管员
            input.User02 = this.ncbManagerOper.Tag.ToString();

            //采购员
            input.User03 = this.ncbBuyOper.Tag.ToString();

            //备注
            input.Memo = this.ntxtMemo.Text.Trim();

            return input;
        }

        public FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.InputCompletedHander InputCompletedEven
        {
            get
            {
                return this.curInputCompletedEven;
            }
            set
            {
                this.curInputCompletedEven = value;
            }
        }

        public int Init(string unitShowState)
        {
            return this.Init();
        }

        #endregion

        private string AutoSaveCompany()
        {
            FS.HISFC.BizLogic.Manager.Spell spellManager = new FS.HISFC.BizLogic.Manager.Spell();

            FS.SOC.HISFC.BizLogic.Pharmacy.Constant phaConsManager = new FS.SOC.HISFC.BizLogic.Pharmacy.Constant();

            string companyID = "";
            FS.HISFC.Models.Pharmacy.Company com = new FS.HISFC.Models.Pharmacy.Company();
            com.Name = this.ncmbProducer.Text;
            FS.HISFC.Models.Base.Spell spCode = (FS.HISFC.Models.Base.Spell)spellManager.Get(com.Name);
            com.SpellCode = spCode.SpellCode;
            com.WBCode = spCode.WBCode;
            com.Type = "0";
            phaConsManager.InsertCompany(com, out companyID);

            SOC.HISFC.BizProcess.Cache.Pharmacy.ClearProducerCache();
            SOC.HISFC.BizProcess.Cache.Pharmacy.InitProducer();
            this.ncmbProducer.alItems.Clear();
            this.ncmbProducer.AddItems(SOC.HISFC.BizProcess.Cache.Pharmacy.producerHelper.ArrayObject);

            return companyID;
        }

    }
}
