using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Neusoft.SOC.Local.Order.OutPatientOrder.HeYuan.HPRM.Common
{
    /// <summary>
    /// ��ȡ��Ŀҽ��������Ϣ
    /// 02	ҽ�� 01	�Է�  03	����
    /// </summary>
    public class ItemExtendInfoAchieve //{014680EC-6381-408b-98FB-A549DAA49B82}: Neusoft.HISFC.BizProcess.Interface.Common.IItemExtendInfo
    {
        #region ����

        Neusoft.HISFC.BizLogic.Fee.Item itemMgr = new Neusoft.HISFC.BizLogic.Fee.Item();
      
        Neusoft.HISFC.BizLogic.Fee.UndrugPackAge undrugPkgMgr = new Neusoft.HISFC.BizLogic.Fee.UndrugPackAge();
        
        Neusoft.HISFC.BizLogic.Pharmacy.Item phaMgr = new Neusoft.HISFC.BizLogic.Pharmacy.Item();
        
        Neusoft.HISFC.BizLogic.Fee.Interface interfaceMgr = new Neusoft.HISFC.BizLogic.Fee.Interface();

        private Neusoft.HISFC.Models.Base.EnumItemType itemType = Neusoft.HISFC.Models.Base.EnumItemType.Drug;

        private Neusoft.HISFC.Models.Base.PactInfo pactInfo = new Neusoft.HISFC.Models.Base.PactInfo();

        /// <summary>
        /// ���ɹ�����
        /// </summary>
        private Neusoft.HISFC.BizProcess.Integrate.Manager interManager = new Neusoft.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// �Է���Ŀ��
        /// </summary>
        private Neusoft.SOC.HISFC.Fee.BizLogic.ComItemExtendInfo comItemExtendInfo = new Neusoft.SOC.HISFC.Fee.BizLogic.ComItemExtendInfo();


        /// <summary>
        /// ��ͬ��λ��Ϣ
        /// </summary>
        private Neusoft.HISFC.BizLogic.Fee.PactUnitInfo pactUnitInfoBizLogic = new Neusoft.HISFC.BizLogic.Fee.PactUnitInfo();


        /// <summary>
        /// ��ͬ��λ����ά����
        /// </summary>
        private Neusoft.SOC.HISFC.Fee.BizLogic.PactItemRate pactItemRate = new Neusoft.SOC.HISFC.Fee.BizLogic.PactItemRate();


        private static Hashtable hsCompareItems = new Hashtable();



        Neusoft.HISFC.Models.SIInterface.Compare compareItem = null;


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

        #endregion

        /// <summary>
        /// ��ȡҽ��������Ŀ��Ϣ
        /// </summary>
        /// <param name="itemCode">��Ŀ����</param>
        /// <param name="compareItem">������Ŀ��Ϣ</param>
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
                        errInfo = "��ȡҽ��������Ŀʧ�ܣ�" + interfaceMgr.Err;
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

                //0 ��С���� 1 ҩƷ 2 ��ҩƷ
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

        #region IItemExtendInfo ��Ա

        Neusoft.HISFC.Models.Pharmacy.Item drugItem = null;

        /// <summary>
        /// ��ȡҽ��������Ϣ
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="ExtendInfoTxt"></param>
        /// <param name="AlExtendInfo"></param>
        /// <returns>0 δ�ҵ�������Ϣ</returns>
        public int GetItemExtendInfo(string ItemID, ref string ExtendInfoTxt, ref System.Collections.ArrayList AlExtendInfo)
        {
            try
            {
                string txtReturn = string.Empty;
                ArrayList al = new ArrayList();

                compareItem = null;
                Neusoft.HISFC.Models.Base.PactItemRate pRate = null;

          
                //���ѵ�
                if (this.pactInfo.PayKind.ID == "03")
                {
                    ExtendInfoTxt = null;

                    List<Neusoft.SOC.HISFC.Fee.Models.ComItemExtend> itemList = comItemExtendInfo.QueryItemListByItemCode(ItemID);
                    if (itemList.Count > 0&&itemList[0].ZFFlag == "1")
                    {
                        ExtendInfoTxt = "[����] �Է�";
                    }
                    else
                    {
                        int rev = this.GetRateByPact(pactInfo.ID, ItemID, ref pRate);
                        if (rev == 0)
                        {
                            ExtendInfoTxt = "[����] �Ը����� " + this.pactInfo.Rate.PayRate;
                            //return 0;
                        }
                        else
                        {
                            if (pRate.Rate.PayRate == 1||pRate.Rate.OwnRate==1)
                            {
                                ExtendInfoTxt = "[����] ������";
                            }
                        }
                    }

                    if (this.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        drugItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(ItemID);
                        switch (drugItem.ExtendData4)
                        {
                            case "01":
                                ExtendInfoTxt += "\r\n\r\n����������ҩ";
                                break;
                            case "02":
                                ExtendInfoTxt += "\r\n\r\n��������������ҩ";
                                break;
                        }

                    }

                }
                else if (pactInfo.PayKind.ID == "02")
                {
                    #region ҽ����ʾ 

                    int rev = this.GetCompareItemInfo(ItemID, ref compareItem);
                    if (rev == -1)
                    {
                        return -1;
                    }

                    if (compareItem == null || compareItem.CenterItem.Rate == 1)
                    {
                        txtReturn = "�Է�";
                    }
                    else 
                    {
                        //ҽ������
                        string SIRate = string.Format("{0}", (1 - compareItem.CenterItem.Rate) * 100) + "%";
                        if (this.itemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            drugItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(ItemID);

                            txtReturn = "ҽ����������" + this.GetItemGrade(compareItem.CenterItem.ItemGrade) + "  ����������" + SIRate;

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
                                txtReturn = "ҽ����������" + this.GetItemGrade(compareItem.CenterItem.ItemGrade) + "\r\n" + "����������" + SIRate;
                            }
                        }
                    }


                    if (this.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        drugItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(ItemID);
                        switch (drugItem.ExtendData4)
                        {
                            case "01":
                                txtReturn += " ����������ҩ";
                                break;
                            case "02":
                                txtReturn += " ��������������ҩ";
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
        /// ��Ŀ���
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
