using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.Order.OutPatientOrder.GYZL.Common
{
    /// <summary>
    /// 获取项目医保对照信息
    /// 02	医保 01	自费  03	公费
    /// </summary>
    public class ItemExtendInfoAchieve : FS.HISFC.BizProcess.Interface.Common.IItemCompareInfo
    {
        /// <summary>
        /// 集成管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager interManager = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 自费项目类
        /// </summary>
        private FS.SOC.HISFC.Fee.BizLogic.ComItemExtendInfo comItemExtendInfo = new FS.SOC.HISFC.Fee.BizLogic.ComItemExtendInfo();


        /// <summary>
        /// 合同单位比例维护类
        /// </summary>
        private FS.SOC.HISFC.Fee.BizLogic.PactItemRate pactItemRate = new FS.SOC.HISFC.Fee.BizLogic.PactItemRate();
        

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

        /// <summary>
        /// 存放对照项目
        /// </summary>
        Dictionary<string, Dictionary<string, FS.HISFC.Models.SIInterface.Compare>> dicCompare = null;

        FS.HISFC.BizLogic.Fee.Interface interfaceMgr = new FS.HISFC.BizLogic.Fee.Interface();

        /// <summary>
        /// 获取医保对照项目信息
        /// </summary>
        /// <param name="item.ID">项目编码</param>
        /// <param name="compareItem">对照项目信息</param>
        /// <returns></returns>
        public FS.HISFC.Models.SIInterface.Compare GetCompareItemInfo(string pactCode, string itemCode)
        {
            if (dicCompare == null)
            {
                dicCompare = new Dictionary<string, Dictionary<string, FS.HISFC.Models.SIInterface.Compare>>();
            }

            FS.HISFC.Models.SIInterface.Compare compareItem = null;

            if (dicCompare.ContainsKey(pactCode))
            {
                if (dicCompare[pactCode].ContainsKey(itemCode))
                {
                    return dicCompare[pactCode][itemCode];
                }
                else
                {
                    int rev = interfaceMgr.GetCompareSingleItem(pactCode, itemCode, ref compareItem);
                    if (rev == -1)
                    {
                        errInfo = "获取医保对照项目失败：" + interfaceMgr.Err;
                        compareItem = null;
                    }
                    else
                    {
                        dicCompare[pactCode].Add(itemCode, compareItem);
                    }
                    return compareItem;
                }
            }
            else
            {
                int rev = interfaceMgr.GetCompareSingleItem(pactCode, itemCode, ref compareItem);
                if (rev == -1)
                {
                    errInfo = "获取医保对照项目失败：" + interfaceMgr.Err;
                    compareItem = null;
                }
                else
                {
                    Dictionary<string, FS.HISFC.Models.SIInterface.Compare> dicPactCompare = new Dictionary<string, FS.HISFC.Models.SIInterface.Compare>();
                    dicPactCompare.Add(itemCode, compareItem);
                    dicCompare.Add(pactCode, dicPactCompare);
                }
                return compareItem;
            }
        }
        

        /// <summary>
        /// 对于公医患者，获得合同单位维护的报销信息
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="itemCode"></param>
        /// <param name="pRate"></param>
        /// <returns></returns>
        private int GetRateByPact(string pactCode, FS.HISFC.Models.Base.Item item, ref FS.HISFC.Models.Base.PactItemRate pRate)
        {
            try
            {
                pRate = null;

                //0 最小费用 1 药品 2 非药品
                List<FS.HISFC.Models.Base.PactItemRate> pRateList = this.pactItemRate.QueryByItem((item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug) ? "1" : "2", item.ID, "'" + pactCode + "'");
                if (pRateList.Count > 0)
                {
                    pRate = pRateList[0];
                    return 1;
                }
                else
                {
                    if (item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        FS.HISFC.Models.Pharmacy.Item drugItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(item.ID);
                        if (drugItem != null)
                        {
                            pRateList = this.pactItemRate.QueryByItem("0", drugItem.MinFee.ID, "'" + pactCode + "'");
                        }
                    }
                    else
                    {
                        FS.HISFC.Models.Fee.Item.Undrug undrug = undrug = SOC.HISFC.BizProcess.Cache.Fee.GetItem(item.ID);
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
            catch
            {
            }
            return 0;
        }

        private string GetItemGrade(string itemGrade)
        {
            return FS.HISFC.BizLogic.Fee.Interface.ShowItemGradeByCode(itemGrade);
        }

        #region IItemCompareInfo 成员


        public int GetCompareItemInfo(FS.HISFC.Models.Base.Item item, FS.HISFC.Models.Base.PactInfo pactInfo, ref FS.HISFC.Models.SIInterface.Compare compareItem, ref string strCompareInfo)
        {
            FS.HISFC.Models.Base.PactInfo pact = SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(pactInfo.ID);
            if (pact != null)
            {
                pactInfo = pact;
            }

            ArrayList al = new ArrayList();
            FS.HISFC.Models.Base.PactItemRate pRate = null;

            int warnFlag = 0;

            #region 公费的
            if (pactInfo.PayKind.ID == "03")
            {
                //查询公医项目对照
                List<FS.SOC.HISFC.Fee.Models.ComItemExtend> itemList = comItemExtendInfo.QueryItemListByItemCode(item.ID);
                if (itemList.Count > 0 && itemList[0].ZFFlag == "1")
                {
                    strCompareInfo = "[记账] 自费";
                    warnFlag = 0;
                }
                else
                {
                    int rev = this.GetRateByPact(pactInfo.ID, item, ref pRate);
                    if (rev == 0)
                    {
                        strCompareInfo = "[记账] 自付比例 " + pactInfo.Rate.PayRate;
                        warnFlag = 0;
                    }
                    else
                    {
                        if (pRate.Rate.PayRate == 1 || pRate.Rate.OwnRate == 1)
                        {
                            strCompareInfo = "[记账] 需审批";
                            warnFlag = 0;
                        }
                    }
                }

                if (item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    FS.HISFC.Models.Pharmacy.Item drugItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(item.ID);
                    switch (drugItem.ExtendData4)
                    {
                        case "01":
                            strCompareInfo += "\r\n\r\n属于肿瘤用药";
                            break;
                        case "02":
                            strCompareInfo += "\r\n\r\n属于肿瘤辅助用药";
                            break;
                    }

                }

            }
            #endregion

            #region 医保

            else if (pactInfo.PayKind.ID == "02")
            {
                #region 医保显示
                compareItem = this.GetCompareItemInfo(pactInfo.ID, item.ID);

                if (compareItem == null
                    || compareItem.CenterItem.Rate == 1
                    || string.IsNullOrEmpty(compareItem.CenterItem.ItemGrade)
                    || compareItem.CenterItem.ItemGrade == "3")
                {
                    strCompareInfo = "自费";
                    warnFlag = 0;
                }
                else
                {
                    //医保比例
                    string SIRate = string.Format("{0}", (1 - compareItem.CenterItem.Rate) * 100) + "%";
                    if (item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        FS.HISFC.Models.Pharmacy.Item drugItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(item.ID);

                        strCompareInfo = "医保报销级别：" + this.GetItemGrade(compareItem.CenterItem.ItemGrade) + "  报销比例：" + SIRate;
                        warnFlag = 1;

                    }
                    else
                    {
                        FS.HISFC.Models.Fee.Item.Undrug undrug = SOC.HISFC.BizProcess.Cache.Fee.GetItem(item.ID);
                        if (undrug == null)
                        {
                            errInfo = "查找项目失败！" + item.Name;
                            return -1;
                        }
                        else
                        {
                            strCompareInfo = "医保报销级别：" + this.GetItemGrade(compareItem.CenterItem.ItemGrade) + "\r\n" + "报销比例：" + SIRate;
                        }
                        warnFlag = 1;
                    }
                }


                if (item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    FS.HISFC.Models.Pharmacy.Item drugItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(item.ID);
                    switch (drugItem.ExtendData4)
                    {
                        case "01":
                            strCompareInfo += " 属于肿瘤用药";
                            break;
                        case "02":
                            strCompareInfo += " 属于肿瘤辅助用药";
                            break;
                    }

                }

                #endregion
            }

            #endregion

            return warnFlag;
        }

        #endregion
    }
}
