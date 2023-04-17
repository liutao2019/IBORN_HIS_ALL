using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Neusoft.SOC.Local.Order.ZhuHai.ZDLY.Common
{
    /// <summary>
    /// ��ȡ��Ŀҽ��������Ϣ
    /// 02	ҽ�� 01	�Է�  03	����
    /// </summary>
    public class ItemExtendInfoAchieve : Neusoft.HISFC.BizProcess.Interface.Common.IItemCompareInfo
    {

        /// <summary>
        /// ���ɹ�����
        /// </summary>
        private Neusoft.HISFC.BizProcess.Integrate.Manager interManager = new Neusoft.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// �Է���Ŀ��
        /// </summary>
        private Neusoft.SOC.HISFC.Fee.BizLogic.ComItemExtendInfo comItemExtendInfo = new Neusoft.SOC.HISFC.Fee.BizLogic.ComItemExtendInfo();


        /// <summary>
        /// ��ͬ��λ����ά����
        /// </summary>
        private Neusoft.SOC.HISFC.Fee.BizLogic.PactItemRate pactItemRate = new Neusoft.SOC.HISFC.Fee.BizLogic.PactItemRate();


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
        Dictionary<string, Dictionary<string, Neusoft.HISFC.Models.SIInterface.Compare>> dicCompare = null;

        Neusoft.HISFC.BizLogic.Fee.Interface interfaceMgr = new Neusoft.HISFC.BizLogic.Fee.Interface();

        /// <summary>
        /// ��ȡҽ��������Ŀ��Ϣ
        /// </summary>
        /// <param name="item.ID">��Ŀ����</param>
        /// <param name="compareItem">������Ŀ��Ϣ</param>
        /// <returns></returns>
        public Neusoft.HISFC.Models.SIInterface.Compare GetCompareItemInfo(string pactCode, string itemCode)
        {
            if (dicCompare == null)
            {
                dicCompare = new Dictionary<string, Dictionary<string, Neusoft.HISFC.Models.SIInterface.Compare>>();
            }

            Neusoft.HISFC.Models.SIInterface.Compare compareItem = null;

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
                    Dictionary<string, Neusoft.HISFC.Models.SIInterface.Compare> dicPactCompare = new Dictionary<string, Neusoft.HISFC.Models.SIInterface.Compare>();
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
        private int GetRateByPact(string pactCode, Neusoft.HISFC.Models.Base.Item item, ref Neusoft.HISFC.Models.Base.PactItemRate pRate)
        {
            try
            {
                pRate = null;

                //0 ��С���� 1 ҩƷ 2 ��ҩƷ
                List<Neusoft.HISFC.Models.Base.PactItemRate> pRateList = this.pactItemRate.QueryByItem((item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug) ? "1" : "2", item.ID, "'" + pactCode + "'");
                if (pRateList.Count > 0)
                {
                    pRate = pRateList[0];
                    return 1;
                }
                else
                {
                    if (item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        Neusoft.HISFC.Models.Pharmacy.Item drugItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(item.ID);
                        if (drugItem != null)
                        {
                            pRateList = this.pactItemRate.QueryByItem("0", drugItem.MinFee.ID, "'" + pactCode + "'");
                        }
                    }
                    else
                    {
                        Neusoft.HISFC.Models.Fee.Item.Undrug undrug = undrug = SOC.HISFC.BizProcess.Cache.Fee.GetItem(item.ID);
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
            return Neusoft.HISFC.BizLogic.Fee.Interface.ShowItemGradeByCode(itemGrade);
        }

        /// <summary>
        /// ��ú�ͬ��λά���ı�����Ϣ
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

                List<Neusoft.HISFC.Models.Base.PactItemRate> pRateList = new List<Neusoft.HISFC.Models.Base.PactItemRate>();

                Neusoft.HISFC.Models.Base.EnumItemType itemType = Neusoft.HISFC.Models.Base.EnumItemType.Drug;
                if (itemCode != "999")
                {
                    Neusoft.HISFC.Models.Fee.Item.Undrug undrug = SOC.HISFC.BizProcess.Cache.Fee.GetItem(itemCode);
                    if (undrug == null)
                    {
                        Neusoft.HISFC.Models.Pharmacy.Item item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(itemCode);
                        if (item != null)
                        {
                            itemType = item.ItemType;
                        }
                    }
                    else
                    {
                        itemType = undrug.ItemType;
                    }
                }

                //0 ��С���� 1 ҩƷ 2 ��ҩƷ
                pRateList = this.pactItemRate.QueryByItem((itemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug) ? "1" : "2", itemCode, "'" + pactCode + "'");

                if (pRateList.Count > 0)
                {
                    pRate = pRateList[0];
                    return 1;
                }
                else
                {
                    if (itemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        Neusoft.HISFC.Models.Pharmacy.Item drugItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(itemCode);
                        if (drugItem != null)
                        {
                            pRateList = this.pactItemRate.QueryByItem("0", drugItem.MinFee.ID, "'" + pactCode + "'");
                        }
                    }
                    else
                    {
                        Neusoft.HISFC.Models.Fee.Item.Undrug undrug = undrug = SOC.HISFC.BizProcess.Cache.Fee.GetItem(itemCode);
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

        #region IItemCompareInfo ��Ա

        public int GetCompareItemInfo(Neusoft.HISFC.Models.Base.Item item, Neusoft.HISFC.Models.Base.PactInfo pactInfo, ref Neusoft.HISFC.Models.SIInterface.Compare compareItem, ref string strCompareInfo)
        {
            if (pactInfo == null || item == null || item.ID == "999")
            {
                return 1;
            }

            Neusoft.HISFC.Models.Base.PactInfo pact = SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(pactInfo.ID);
            if (pact == null)
            {
                return -1;
            }
            pactInfo = pact;

            #region ��ԣ�����ҽ���ģ�������ԽдԽû�ṹ����
            Neusoft.FrameWork.Models.NeuObject civilworkerObject = this.interManager.GetConstansObj("civilworker", pactInfo.ID);
            bool isCivilworker = false;

            if ((!object.Equals(civilworkerObject, null) && !string.IsNullOrEmpty(civilworkerObject.ID)))
            {
                isCivilworker = true;
            }
            #endregion

            Neusoft.HISFC.Models.SIInterface.Compare compare = new Neusoft.HISFC.Models.SIInterface.Compare();

            int warnFlag = 0;

            //���ѵ�
            if (pactInfo.PayKind.ID == "03" || isCivilworker)
            {
                compare.HisCode = item.ID;
                compare.CenterItem = new Neusoft.HISFC.Models.SIInterface.Item();

                #region ��ȡ����ά����Ϣ

                Neusoft.HISFC.Models.Base.PactItemRate pRate = null;

                List<Neusoft.SOC.HISFC.Fee.Models.ComItemExtend> itemList = comItemExtendInfo.QueryItemListByItemCode(item.ID);
                if (itemList.Count > 0 && itemList[0].ZFFlag == "1")
                {
                    strCompareInfo = "�Է�";
                    compare.CenterItem.ItemGrade = "3";
                    warnFlag = 0;
                }
                else
                {
                    int rev = this.GetRateByPact(pactInfo.ID, item.ID, ref pRate);
                    if (rev == 0)
                    {
                        //strCompareInfo = "[����] �Ը����� " + this.pactInfo.Rate.PayRate;
                        //approvalFlag = true;
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

                #endregion

            }
            else if (pactInfo.PayKind.ID == "02" && !isCivilworker)
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
                    if (item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        Neusoft.HISFC.Models.Pharmacy.Item drugItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(item.ID);

                        strCompareInfo = "ҽ����������" + this.GetItemGrade(compareItem.CenterItem.ItemGrade) + "  ����������" + SIRate;
                        warnFlag = 1;

                    }
                    else
                    {
                        Neusoft.HISFC.Models.Fee.Item.Undrug undrug = SOC.HISFC.BizProcess.Cache.Fee.GetItem(item.ID);
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


                if (item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                {
                    Neusoft.HISFC.Models.Pharmacy.Item drugItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(item.ID);
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
            return warnFlag;
        }

        #endregion
    }
}
