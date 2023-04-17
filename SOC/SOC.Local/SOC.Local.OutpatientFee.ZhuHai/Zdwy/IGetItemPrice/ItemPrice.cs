using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.OutpatientFee.ZhuHai.Zdwy.IGetItemPrice
{
    public class ItemPrice:FS.FrameWork.Management.Database, FS.HISFC.BizProcess.Interface.Fee.IGetItemPrice
    {
        FS.HISFC.BizLogic.Fee.Item itemManager = new FS.HISFC.BizLogic.Fee.Item();

        string sql = @"select t.item_code,t.unit_price,t.unit_price1,t.unit_price2 from fin_com_undruginfo t where t.item_code='{0}'";

        #region IGetItemPrice 成员

        public decimal GetPrice(string itemCode, FS.HISFC.Models.Registration.Register register, decimal UnitPrice, decimal ChildPrice, decimal SPPrice, decimal PurchasePrice, ref decimal orgPrice)
        {
            #region 中大五院模式-废弃

            //orgPrice = UnitPrice;
            //if (register!=null && register.Pact!=null&& register.Pact.PriceForm == "购入价")
            //{
            //    if (!string.IsNullOrEmpty(itemCode) && itemCode.Substring(0, 1) == "Y")
            //    {
            //        FS.HISFC.Models.Pharmacy.Item item = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(itemCode);
            //        if (item.Type.ID.ToString() == "PCC")
            //        {
            //            return UnitPrice;
            //        }
            //        else
            //        {
            //            if (PurchasePrice != 0)
            //            {
            //                return PurchasePrice;
            //            }
            //            else
            //            {
            //                decimal retailPrice2 = item.RetailPrice2;
            //                if (retailPrice2 == 0)
            //                {
            //                    return UnitPrice;
            //                }
            //                else
            //                {
            //                    return retailPrice2;
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        return UnitPrice;
            //    }
            //}
            //else
            //{
            //    return UnitPrice;
            //}

            #endregion

            /*
             * FIN_COM_PACTUNITINFO.PRICE_FORM IS '价格形式 0三甲价 1特诊价 2儿童价';
             * 
             * FIN_COM_UNDRUGINFO.UNIT_PRICE IS '三甲价';
             * FIN_COM_UNDRUGINFO.UNIT_PRICE2 IS '特诊价';
             * FIN_COM_UNDRUGINFO.UNIT_PRICE1 IS '儿童价';
             * 
             * PHA_COM_BASEINFO.RETAIL_PRICE IS '参考零售价';
             * PHA_COM_BASEINFO.RETAIL_PRICE2 IS '参考零售价2';
             * PHA_COM_BASEINFO.TOP_RETAILPRICE IS '最高零售价';
             * 
             * */

            orgPrice = UnitPrice;
            if (register != null && register.Pact != null)
            {
                if (!string.IsNullOrEmpty(itemCode) && itemCode.Substring(0, 1) == "Y")
                {
                    #region 药品

                    FS.HISFC.Models.Pharmacy.Item item = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(itemCode);
                    if (item == null || string.IsNullOrEmpty(item.ID))
                    {
                        return UnitPrice;
                    }

                    FS.HISFC.Models.Base.PactInfo pact = FS.SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(register.Pact.ID);
                    if (pact == null || string.IsNullOrEmpty(pact.ID))
                    {
                        return UnitPrice;
                    }

                    //PRICE_FORM IS '价格形式 0默认价 1特诊价 2儿童价';
                    if (pact.PriceForm == "默认价")
                    {
                        return UnitPrice;                   //0默认价 对应 参考零售价
                    }
                    else if (pact.PriceForm == "特诊价")
                    {
                        if (item.RetailPrice2 != 0)
                        {
                            UnitPrice = item.RetailPrice2;       //1特诊价  对应 参考零售价2
                        }
                        return UnitPrice;
                    }
                    else if (pact.PriceForm == "儿童价")
                    {
                        if (item.PriceCollection.TopRetailPrice != 0)
                        {
                            UnitPrice = item.PriceCollection.TopRetailPrice;     //2儿童价    对应  最高零售价
                        }
                        return UnitPrice;
                    }
                    else
                    {
                        return UnitPrice;
                    }

                    #endregion
                }
                else if (!string.IsNullOrEmpty(itemCode) && itemCode.Substring(0, 1) == "F")
                {
                    #region 非药品

                    FS.HISFC.Models.Fee.Item.Undrug undrug = FS.SOC.HISFC.BizProcess.Cache.Fee.GetItem(itemCode);
                    if (undrug == null || string.IsNullOrEmpty(undrug.ID))
                    {
                        return UnitPrice;
                    }

                    FS.HISFC.Models.Base.PactInfo pact = FS.SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(register.Pact.ID);
                    if (pact == null || string.IsNullOrEmpty(pact.ID))
                    {
                        return UnitPrice;
                    }

                    //PRICE_FORM IS '价格形式 0默认价 1特诊价 2儿童价';
                    if (pact.PriceForm == "默认价")
                    {
                        return UnitPrice;                   //0默认价 对应 三甲价
                    }
                    else if (pact.PriceForm == "特诊价")
                    {
                        if (undrug.SpecialPrice != 0)
                        {
                            UnitPrice = undrug.SpecialPrice;       //1特诊价  对应 特诊价
                        }
                        return UnitPrice; 
                    }
                    else if (pact.PriceForm == "儿童价")
                    {
                        if (undrug.ChildPrice != 0)
                        {
                            UnitPrice = undrug.ChildPrice;        //2儿童价    对应  儿童价
                        }
                        return UnitPrice; 
                    }
                    else
                    {
                        return UnitPrice;
                    }

                    #endregion
                }
                else
                {
                    return UnitPrice;
                }
            }
            else
            {
                return UnitPrice;
            }
        }

        public decimal GetPriceForInpatient(string itemCode, FS.HISFC.Models.RADT.PatientInfo patientInfo, decimal UnitPrice, decimal ChildPrice, decimal SPPrice, decimal PurchasePrice, ref decimal orgPrice)
        {
            if (patientInfo != null
                && patientInfo.Pact != null
                && !string.IsNullOrEmpty(patientInfo.Pact.ID)
                )
            {
                FS.HISFC.Models.Base.PactInfo pact = FS.SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(patientInfo.Pact.ID);
                if (pact == null || string.IsNullOrEmpty(pact.ID))
                {
                    return UnitPrice;
                }

                #region 药品

                if (!string.IsNullOrEmpty(itemCode) && itemCode.Substring(0, 1) == "Y")
                {
                    FS.HISFC.Models.Pharmacy.Item item = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(itemCode);
                    if (pact.PriceForm == "默认价")
                    {
                        orgPrice = UnitPrice;
                        return UnitPrice;
                    }
                    else if (pact.PriceForm == "购入价")
                    {
                        if (item.Type.ID.ToString() == "PCC")
                        {
                            orgPrice = UnitPrice;
                            return UnitPrice;
                        }
                        else
                        {
                            decimal defaultPrice = ((FS.HISFC.Models.Pharmacy.Item)item).PriceCollection.RetailPrice;
                            decimal purchasePrice = ((FS.HISFC.Models.Pharmacy.Item)item).RetailPrice2;
                            orgPrice = UnitPrice;
                            if (purchasePrice != 0)
                            {
                                return purchasePrice;
                            }
                            else
                            {
                                decimal retailPrice2 = item.RetailPrice2;
                                if (retailPrice2 == 0)
                                {
                                    return defaultPrice;
                                }
                                else
                                {
                                    return retailPrice2;
                                }
                            }
                        }
                    }
                    else if (pact.PriceForm == "特诊价")
                    {
                        if (item.RetailPrice2 != 0)
                        {
                            UnitPrice = item.RetailPrice2;       //1特诊价  对应 参考零售价2
                        }
                        orgPrice = UnitPrice;
                        return UnitPrice;
                    }
                    else if (pact.PriceForm == "儿童价")
                    {
                        if (item.PriceCollection.TopRetailPrice != 0)
                        {
                            UnitPrice = item.PriceCollection.TopRetailPrice;     //2儿童价    对应  最高零售价
                        }
                        orgPrice = UnitPrice;
                        return UnitPrice;
                    }
                    else
                    {
                        orgPrice = UnitPrice;
                        return UnitPrice;
                    }
                }
                #endregion
                #region 非药品
                else
                {
                    FS.HISFC.Models.Fee.Item.Undrug undrug = FS.SOC.HISFC.BizProcess.Cache.Fee.GetItem(itemCode);
                    if (undrug == null || string.IsNullOrEmpty(undrug.ID))
                    {
                        orgPrice = UnitPrice;
                        return UnitPrice;
                    }

                    if (pact.PriceForm == "默认价")
                    {
                        orgPrice = UnitPrice;
                        return UnitPrice;
                    }
                    else if (pact.PriceForm == "特诊价")
                    {
                        if (undrug.SpecialPrice != 0)
                        {
                            UnitPrice = undrug.SpecialPrice;       //1特诊价
                        }
                        orgPrice = UnitPrice;
                        return UnitPrice;
                    }
                    else if (pact.PriceForm == "儿童价")
                    {
                        if (undrug.ChildPrice != 0)
                        {
                            UnitPrice = undrug.ChildPrice;     //2儿童价
                        }
                        orgPrice = UnitPrice;
                        return UnitPrice;
                    }
                    else
                    {
                        orgPrice = UnitPrice;
                        return UnitPrice;
                    }
                    orgPrice = UnitPrice;
                    return UnitPrice;
                }
                #endregion
            }
            else
            {
                orgPrice = UnitPrice;
                return UnitPrice;
            }
        }

        private int getPrice(string itemCode, ref decimal UnitPrice,ref decimal ChildPrice,ref decimal SPPrice)
        {
            int i = this.ExecQuery(string.Format(sql, itemCode));
            if (i > 0)
            {
                if (this.Reader != null)
                {
                    try
                    {
                        if (this.Reader.Read())
                        {
                            UnitPrice = FS.FrameWork.Function.NConvert.ToDecimal(itemManager.Reader[1]);
                            SPPrice = FS.FrameWork.Function.NConvert.ToDecimal(itemManager.Reader[3]);
                        }
                    }
                    finally
                    {
                        if (this.Reader.IsClosed == false)
                        {
                            this.Reader.Close();
                        }
                    }
                }

                return 1;
            }
            else
            {
                return -1;
            }
        }


        #endregion
    }
}
