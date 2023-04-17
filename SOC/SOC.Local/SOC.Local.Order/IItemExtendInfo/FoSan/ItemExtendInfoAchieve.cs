using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.Order.IItemExtendInfo.FoSan
{
    /// <summary>
    /// 获取项目医保对照信息
    /// </summary>
    public class ItemExtendInfoAchieve : FS.HISFC.BizProcess.Interface.Common.IItemCompareInfo
    {

        #region 变量

        FS.HISFC.BizLogic.Fee.Item itemMgr = new FS.HISFC.BizLogic.Fee.Item();
        FS.HISFC.BizLogic.Fee.UndrugPackAge undrugPkgMgr = new FS.HISFC.BizLogic.Fee.UndrugPackAge();
        FS.HISFC.BizLogic.Pharmacy.Item phaMgr = new FS.HISFC.BizLogic.Pharmacy.Item();
        FS.HISFC.BizLogic.Fee.Interface interfaceMgr = new FS.HISFC.BizLogic.Fee.Interface();

        private FS.HISFC.Models.Base.EnumItemType itemType = FS.HISFC.Models.Base.EnumItemType.Drug;

        private FS.HISFC.Models.Base.PactInfo pactInfo = new FS.HISFC.Models.Base.PactInfo();

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

        private static Hashtable hsCompareItems = new Hashtable();

        FS.HISFC.Models.SIInterface.Compare compareItem = null;

        /// <summary>
        /// 获取医保对照项目信息
        /// </summary>
        /// <param name="itemCode">项目编码</param>
        /// <param name="compareItem">对照项目信息</param>
        /// <returns></returns>
        public int GetCompareItemInfo(string itemCode, ref FS.HISFC.Models.SIInterface.Compare compareItem)
        {
            try
            {
                if (hsCompareItems.Contains(pactInfo.ID + itemCode))
                {
                    compareItem = hsCompareItems[pactInfo.ID + itemCode] as FS.HISFC.Models.SIInterface.Compare;
                }
                else
                {
                    int rev = interfaceMgr.GetCompareSingleItem("3", itemCode, ref compareItem);
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

        private FS.HISFC.BizLogic.Fee.PactUnitItemRate pactItemRate = new FS.HISFC.BizLogic.Fee.PactUnitItemRate();

        /// <summary>
        /// 获得合同单位维护的报销信息
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="itemCode"></param>
        /// <param name="pRate"></param>
        /// <returns></returns>
        private int GetRateByPact(string pactCode, string itemCode, ref FS.HISFC.Models.Base.PactItemRate pRate)
        {
            try
            {
                pRate = null;

                pRate = this.pactItemRate.GetOnepPactUnitItemRateByItem(pactCode, itemCode);

                if (pRate != null)
                {
                    return 1;
                }
            }
            catch { }
            return 0;
        }

        #region IItemExtendInfo 成员

        FS.HISFC.Models.Pharmacy.Item drugItem = null;

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
                FS.HISFC.Models.Base.PactItemRate pRate = null;

                int rev = this.GetCompareItemInfo(ItemID, ref compareItem);
                if (rev == -1)
                {
                    return -1;
                }
                else if (compareItem == null)
                {
                    ExtendInfoTxt = null;
                    //return 0;

                    rev = this.GetRateByPact(pactInfo.ID, ItemID, ref pRate);
                    if (rev == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        compareItem = new FS.HISFC.Models.SIInterface.Compare();
                        compareItem.CenterItem.Rate = pRate.Rate.PayRate;
                    }
                }

                //医保比例
                string SIRate = string.Format("{0}", compareItem.CenterItem.Rate * 100) + "%";
                //适应症信息
                string Practicablesymptomdepiction = compareItem.Practicablesymptomdepiction;

                if (this.itemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    drugItem = phaMgr.GetItem(ItemID);

                    txtReturn = "名称：【" + drugItem.Name + "】  编码：" + (string.IsNullOrEmpty(drugItem.UserCode) ? drugItem.ID : drugItem.UserCode)
                        + "\r\n\r\n医保标志：" + this.GetItemGrade(compareItem.CenterItem.ItemGrade) + "  报销比例：" + SIRate
                        + "\r\n\r\n" + "其他信息：" + Practicablesymptomdepiction;
                }
                else
                {
                    FS.HISFC.Models.Fee.Item.Undrug undrug = null;

                    undrug = itemMgr.GetValidItemByUndrugCode(ItemID);

                    //if (undrug != null && undrug.UnitFlag == "1")
                    //{
                    //    al = undrugPkgMgr.QueryUndrugPackagesBypackageCode(undrug.ID);
                    //    if (al == null)
                    //    {
                    //        errInfo = undrugPkgMgr.Err;
                    //        return -1;
                    //    }
                    //}
                    if (undrug == null)
                    {
                        errInfo = itemMgr.Err;
                        return -1;
                    }
                    else
                    {
                        txtReturn = "项目编码：" + undrug.UserCode + "\r\n" + "项目名称：" + undrug.Name + "\r\n医保报销级别：" + this.GetItemGrade(compareItem.CenterItem.ItemGrade) + "\r\n" + "医保比例：" + SIRate + "\r\n" + "适应症：" + Practicablesymptomdepiction;
                    }
                }

                ExtendInfoTxt = txtReturn;
                //AlExtendInfo = al;
            }
            catch { }
            return 1;
        }

        private string GetItemGrade(string itemGrade)
        {
            switch (itemGrade)
            {
                case "1":
                    return "甲类";
                    break;
                case "2":
                    return "乙类";
                default:
                    return "自费";
            }
        }


        /// <summary>
        /// 项目类别
        /// </summary>
        public FS.HISFC.Models.Base.EnumItemType ItemType
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

        public FS.HISFC.Models.Base.PactInfo PactInfo
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

        #region IItemCompareInfo 成员


        public int GetCompareItemInfo(FS.HISFC.Models.Base.Item item, FS.HISFC.Models.Base.PactInfo pactInfo, ref FS.HISFC.Models.SIInterface.Compare compare, ref string strCompareInfo)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
