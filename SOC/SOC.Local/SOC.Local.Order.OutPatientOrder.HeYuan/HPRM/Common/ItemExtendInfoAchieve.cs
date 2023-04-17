using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Neusoft.SOC.Local.Order.OutPatientOrder.HeYuan.HPRM.Common
{
    /// <summary>
    /// 获取项目医保对照信息
    /// 02	医保 01	自费  03	公费
    /// </summary>
    public class ItemExtendInfoAchieve //{014680EC-6381-408b-98FB-A549DAA49B82}: Neusoft.HISFC.BizProcess.Interface.Common.IItemExtendInfo
    {
        #region 变量

        Neusoft.HISFC.BizLogic.Fee.Item itemMgr = new Neusoft.HISFC.BizLogic.Fee.Item();
      
        Neusoft.HISFC.BizLogic.Fee.UndrugPackAge undrugPkgMgr = new Neusoft.HISFC.BizLogic.Fee.UndrugPackAge();
        
        Neusoft.HISFC.BizLogic.Pharmacy.Item phaMgr = new Neusoft.HISFC.BizLogic.Pharmacy.Item();
        
        Neusoft.HISFC.BizLogic.Fee.Interface interfaceMgr = new Neusoft.HISFC.BizLogic.Fee.Interface();

        private Neusoft.HISFC.Models.Base.EnumItemType itemType = Neusoft.HISFC.Models.Base.EnumItemType.Drug;

        private Neusoft.HISFC.Models.Base.PactInfo pactInfo = new Neusoft.HISFC.Models.Base.PactInfo();

        /// <summary>
        /// 集成管理类
        /// </summary>
        private Neusoft.HISFC.BizProcess.Integrate.Manager interManager = new Neusoft.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 自费项目类
        /// </summary>
        private Neusoft.SOC.HISFC.Fee.BizLogic.ComItemExtendInfo comItemExtendInfo = new Neusoft.SOC.HISFC.Fee.BizLogic.ComItemExtendInfo();


        /// <summary>
        /// 合同单位信息
        /// </summary>
        private Neusoft.HISFC.BizLogic.Fee.PactUnitInfo pactUnitInfoBizLogic = new Neusoft.HISFC.BizLogic.Fee.PactUnitInfo();


        /// <summary>
        /// 合同单位比例维护类
        /// </summary>
        private Neusoft.SOC.HISFC.Fee.BizLogic.PactItemRate pactItemRate = new Neusoft.SOC.HISFC.Fee.BizLogic.PactItemRate();


        private static Hashtable hsCompareItems = new Hashtable();



        Neusoft.HISFC.Models.SIInterface.Compare compareItem = null;


        /// <summary>
        /// 错误信息
        /// </summary>
        private string errInfo = "";

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrInfo
        {
            get
            {
                return errInfo;
            }
            set
            {
                errInfo = value;
            }
        }

        #endregion

        /// <summary>
        /// 获取医保对照项目信息
        /// </summary>
        /// <param name="itemCode">项目编码</param>
        /// <param name="compareItem">对照项目信息</param>
        /// <returns></returns>
        public int GetCompareItemInfo(string itemCode, ref Neusoft.HISFC.Models.SIInterface.Compare compareItem)
        {
            try
            {
                if (hsCompareItems.Contains(pactInfo.ID + itemCode))
                {
                    compareItem = hsCompareItems[pactInfo.ID + itemCode] as Neusoft.HISFC.Models.SIInterface.Compare;
                }
                else
                {
                    int rev = interfaceMgr.GetCompareSingleItem(pactInfo.ID, itemCode, ref compareItem);
                    if (rev == -1)
                    {
                        errInfo = "获取医保对照项目失败：" + interfaceMgr.Err;
                        compareItem = null;
                        return -1;
                    }
                    else if (rev == -2)
                    {
                        compareItem = null;
                    }
                    if (compareItem != null)
                    {
                        if (!hsCompareItems.Contains(pactInfo.ID + itemCode))
                        {
                            hsCompareItems.Add(pactInfo.ID + itemCode, compareItem);
                        }
                    }
                }
                return 1;
            }
            catch
            { 
            }
            return 1;
        }




        /// <summary>
        /// 获得合同单位维护的报销信息
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="itemCode"></param>
        /// <param name="pRate"></param>
        /// <returns></returns>
        private int GetRateByPact(string pactCode, string itemCode, ref Neusoft.HISFC.Models.Base.PactItemRate pRate)
        {
            try
            {
                pRate = null;

                //0 最小费用 1 药品 2 非药品
                List<Neusoft.HISFC.Models.Base.PactItemRate> pRateList = this.pactItemRate.QueryByItem((this.itemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug) ? "1" : "2", itemCode, "'"+ pactCode+"'");
                if (pRateList.Count > 0)
                {
                    pRate = pRateList[0];
                    return 1;
                }
                else 
                {
                    if (this.itemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        drugItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(itemCode);
                        if (drugItem != null)
                        {
                            pRateList = this.pactItemRate.QueryByItem("0", drugItem.MinFee.ID, "'" + pactCode + "'");
                        }
                    }
                    else 
                    {
                        Neusoft.HISFC.Models.Fee.Item.Undrug undrug = undrug = itemMgr.GetValidItemByUndrugCode(itemCode);
                        if (undrug != null)
                        {
                            pRateList = this.pactItemRate.QueryByItem("0", undrug.MinFee.ID, "'" + pactCode + "'");

                        }
                    }
                    if (pRateList.Count > 0)
                    {
                        pRate = pRateList[0];
                        return 1;
                    }
                }
            }
            catch { }
            return 0;
        }

        #region IItemExtendInfo 成员

        Neusoft.HISFC.Models.Pharmacy.Item drugItem = null;

        /// <summary>
        /// 获取医保对照信息
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="ExtendInfoTxt"></param>
        /// <param name="AlExtendInfo"></param>
        /// <returns>0 未找到对照信息</returns>
        public int GetItemExtendInfo(string ItemID, ref string ExtendInfoTxt, ref System.Collections.ArrayList AlExtendInfo)
        {
            try
            {
                string txtReturn = string.Empty;
                ArrayList al = new ArrayList();

                compareItem = null;
                Neusoft.HISFC.Models.Base.PactItemRate pRate = null;

          
                //公费的
                if (this.pactInfo.PayKind.ID == "03")
                {
                    ExtendInfoTxt = null;

                    List<Neusoft.SOC.HISFC.Fee.Models.ComItemExtend> itemList = comItemExtendInfo.QueryItemListByItemCode(ItemID);
                    if (itemList.Count > 0&&itemList[0].ZFFlag == "1")
                    {
                        ExtendInfoTxt = "[记账] 自费";
                    }
                    else
                    {
                        int rev = this.GetRateByPact(pactInfo.ID, ItemID, ref pRate);
                        if (rev == 0)
                        {
                            ExtendInfoTxt = "[记账] 自付比例 " + this.pactInfo.Rate.PayRate;
                            //return 0;
                        }
                        else
                        {
                            if (pRate.Rate.PayRate == 1||pRate.Rate.OwnRate==1)
                            {
                                ExtendInfoTxt = "[记账] 需审批";
                            }
                        }
                    }

                    if (this.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        drugItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(ItemID);
                        switch (drugItem.ExtendData4)
                        {
                            case "01":
                                ExtendInfoTxt += "\r\n\r\n属于肿瘤用药";
                                break;
                            case "02":
                                ExtendInfoTxt += "\r\n\r\n属于肿瘤辅助用药";
                                break;
                        }

                    }

                }
                else if (pactInfo.PayKind.ID == "02")
                {
                    #region 医保显示 

                    int rev = this.GetCompareItemInfo(ItemID, ref compareItem);
                    if (rev == -1)
                    {
                        return -1;
                    }

                    if (compareItem == null || compareItem.CenterItem.Rate == 1)
                    {
                        txtReturn = "自费";
                    }
                    else 
                    {
                        //医保比例
                        string SIRate = string.Format("{0}", (1 - compareItem.CenterItem.Rate) * 100) + "%";
                        if (this.itemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            drugItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(ItemID);

                            txtReturn = "医保报销级别：" + this.GetItemGrade(compareItem.CenterItem.ItemGrade) + "  报销比例：" + SIRate;

                        }
                        else
                        {
                            Neusoft.HISFC.Models.Fee.Item.Undrug undrug = null;
                            undrug = SOC.HISFC.BizProcess.Cache.Fee.GetItem(ItemID);
                            if (undrug == null)
                            {
                                errInfo = itemMgr.Err;
                                return -1;
                            }
                            else
                            {
                                txtReturn = "医保报销级别：" + this.GetItemGrade(compareItem.CenterItem.ItemGrade) + "\r\n" + "报销比例：" + SIRate;
                            }
                        }
                    }


                    if (this.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        drugItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(ItemID);
                        switch (drugItem.ExtendData4)
                        {
                            case "01":
                                txtReturn += " 属于肿瘤用药";
                                break;
                            case "02":
                                txtReturn += " 属于肿瘤辅助用药";
                                break;
                        }

                    }

                    #endregion
                   
                    ExtendInfoTxt = txtReturn;

                }

            }
            catch { }
            return 1;
        }

        private string GetItemGrade(string itemGrade)
        {
            return Neusoft.HISFC.BizLogic.Fee.Interface.ShowItemGradeByCode(itemGrade);
        }


        /// <summary>
        /// 项目类别
        /// </summary>
        public Neusoft.HISFC.Models.Base.EnumItemType ItemType
        {
            get
            {
                return this.itemType;
            }
            set
            {
                this.itemType = value;
            }
        }

        public Neusoft.HISFC.Models.Base.PactInfo PactInfo
        {
            get
            {
                return this.pactInfo;
            }
            set
            {
                this.pactInfo = value;
            } 
        }

        #endregion
    }
}
