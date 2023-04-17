using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.Order.OutPatientOrder.GYZL.Common
{
    /// <summary>
    /// ��ȡ��Ŀҽ��������Ϣ
    /// 02	ҽ�� 01	�Է�  03	����
    /// </summary>
    public class ItemExtendInfoAchieve : FS.HISFC.BizProcess.Interface.Common.IItemCompareInfo
    {
        /// <summary>
        /// ���ɹ�����
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager interManager = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// �Է���Ŀ��
        /// </summary>
        private FS.SOC.HISFC.Fee.BizLogic.ComItemExtendInfo comItemExtendInfo = new FS.SOC.HISFC.Fee.BizLogic.ComItemExtendInfo();


        /// <summary>
        /// ��ͬ��λ����ά����
        /// </summary>
        private FS.SOC.HISFC.Fee.BizLogic.PactItemRate pactItemRate = new FS.SOC.HISFC.Fee.BizLogic.PactItemRate();
        

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private string errInfo = "";

        /// <summary>
        /// ������Ϣ
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
        /// ��Ŷ�����Ŀ
        /// </summary>
        Dictionary<string, Dictionary<string, FS.HISFC.Models.SIInterface.Compare>> dicCompare = null;

        FS.HISFC.BizLogic.Fee.Interface interfaceMgr = new FS.HISFC.BizLogic.Fee.Interface();

        /// <summary>
        /// ��ȡҽ��������Ŀ��Ϣ
        /// </summary>
        /// <param name="item.ID">��Ŀ����</param>
        /// <param name="compareItem">������Ŀ��Ϣ</param>
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
                        errInfo = "��ȡҽ��������Ŀʧ�ܣ�" + interfaceMgr.Err;
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
                    errInfo = "��ȡҽ��������Ŀʧ�ܣ�" + interfaceMgr.Err;
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
        /// ���ڹ�ҽ���ߣ���ú�ͬ��λά���ı�����Ϣ
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

                //0 ��С���� 1 ҩƷ 2 ��ҩƷ
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

        #region IItemCompareInfo ��Ա


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

            #region ���ѵ�
            if (pactInfo.PayKind.ID == "03")
            {
                //��ѯ��ҽ��Ŀ����
                List<FS.SOC.HISFC.Fee.Models.ComItemExtend> itemList = comItemExtendInfo.QueryItemListByItemCode(item.ID);
                if (itemList.Count > 0 && itemList[0].ZFFlag == "1")
                {
                    strCompareInfo = "[����] �Է�";
                    warnFlag = 0;
                }
                else
                {
                    int rev = this.GetRateByPact(pactInfo.ID, item, ref pRate);
                    if (rev == 0)
                    {
                        strCompareInfo = "[����] �Ը����� " + pactInfo.Rate.PayRate;
                        warnFlag = 0;
                    }
                    else
                    {
                        if (pRate.Rate.PayRate == 1 || pRate.Rate.OwnRate == 1)
                        {
                            strCompareInfo = "[����] ������";
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
                            strCompareInfo += "\r\n\r\n����������ҩ";
                            break;
                        case "02":
                            strCompareInfo += "\r\n\r\n��������������ҩ";
                            break;
                    }

                }

            }
            #endregion

            #region ҽ��

            else if (pactInfo.PayKind.ID == "02")
            {
                #region ҽ����ʾ
                compareItem = this.GetCompareItemInfo(pactInfo.ID, item.ID);

                if (compareItem == null
                    || compareItem.CenterItem.Rate == 1
                    || string.IsNullOrEmpty(compareItem.CenterItem.ItemGrade)
                    || compareItem.CenterItem.ItemGrade == "3")
                {
                    strCompareInfo = "�Է�";
                    warnFlag = 0;
                }
                else
                {
                    //ҽ������
                    string SIRate = string.Format("{0}", (1 - compareItem.CenterItem.Rate) * 100) + "%";
                    if (item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        FS.HISFC.Models.Pharmacy.Item drugItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(item.ID);

                        strCompareInfo = "ҽ����������" + this.GetItemGrade(compareItem.CenterItem.ItemGrade) + "  ����������" + SIRate;
                        warnFlag = 1;

                    }
                    else
                    {
                        FS.HISFC.Models.Fee.Item.Undrug undrug = SOC.HISFC.BizProcess.Cache.Fee.GetItem(item.ID);
                        if (undrug == null)
                        {
                            errInfo = "������Ŀʧ�ܣ�" + item.Name;
                            return -1;
                        }
                        else
                        {
                            strCompareInfo = "ҽ����������" + this.GetItemGrade(compareItem.CenterItem.ItemGrade) + "\r\n" + "����������" + SIRate;
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
                            strCompareInfo += " ����������ҩ";
                            break;
                        case "02":
                            strCompareInfo += " ��������������ҩ";
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
