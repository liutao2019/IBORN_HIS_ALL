using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Threading;

namespace Neusoft.DefultInterfacesAchieve.Common
{
    /// <summary>
    /// ��ȡ��Ŀҽ��������Ϣ
    /// </summary>
    public class ItemExtendInfoAchieve : Neusoft.HISFC.BizProcess.Interface.Common.IItemExtendInfo
    {

        #region ����

        Neusoft.HISFC.BizLogic.Fee.Item itemMgr = new Neusoft.HISFC.BizLogic.Fee.Item();
        Neusoft.HISFC.BizLogic.Fee.UndrugPackAge undrugPkgMgr = new Neusoft.HISFC.BizLogic.Fee.UndrugPackAge();
        Neusoft.HISFC.BizLogic.Pharmacy.Item phaMgr = new Neusoft.HISFC.BizLogic.Pharmacy.Item();
        Neusoft.HISFC.BizLogic.Fee.Interface interfaceMgr = new Neusoft.HISFC.BizLogic.Fee.Interface();

        private Neusoft.HISFC.Models.Base.EnumItemType itemType = Neusoft.HISFC.Models.Base.EnumItemType.Drug;

        private Neusoft.HISFC.Models.Base.PactInfo pactInfo = new Neusoft.HISFC.Models.Base.PactInfo();

        /// <summary>
        /// ���Ʋ�����Ϣ
        /// </summary>
        private  Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam controlParamManager = new Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam();


        /// <summary>
        /// ��ͬ��λ��Ϣ
        /// </summary>
        private Neusoft.HISFC.BizLogic.Fee.PactUnitInfo pactUnitInfoBizLogic = new Neusoft.HISFC.BizLogic.Fee.PactUnitInfo();

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

        private static Hashtable hsCompareItems = new Hashtable();

        Neusoft.HISFC.Models.SIInterface.Compare compareItem = null;

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

        private Neusoft.HISFC.BizLogic.Fee.PactUnitItemRate pactItemRate = new Neusoft.HISFC.BizLogic.Fee.PactUnitItemRate();

        /// <summary>
        /// ��ú�ͬ��λά���ı�����Ϣ
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="itemCode"></param>
        /// <param name="pRate"></param>
        /// <returns></returns>
        private int GetRateByPact(string pactCode,string itemCode,ref Neusoft.HISFC.Models.Base.PactItemRate pRate)
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

        #region IItemExtendInfo ��Ա

        Neusoft.HISFC.Models.Pharmacy.Item drugItem = null;

        /// <summary>
        /// ҽ��������Ŀ�б�
        /// </summary>
        Hashtable hsCompareItem = null;

        /// <summary>
        /// ��ͬ��λά���Ż��б�
        /// </summary>
        Hashtable hsPactItem = null;

        /// <summary>
        /// �Ƿ��Ѿ�ʹ�ö��̳߳�ʼ�� ������Ϣ��ϣ�
        /// </summary>
        bool isInitCompareItem = false;

        /// <summary>
        /// ���ö��߳�
        /// </summary>
        private void ThreadGetCompareItem()
        {
            ThreadStart threadStart = new ThreadStart(GetCompareItem);
            Thread thread = new Thread(threadStart);
            thread.Start(); 
        }

        /// <summary>
        /// ��ȡ����ҽ��������Ϣ
        /// </summary>
        private void GetCompareItem()
        {
            Compare comMgr = new Compare();
            hsCompareItem = comMgr.GetCompareItem();
            isInitCompareItem = true;
        }

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
                if (!this.isInitCompareItem)
                {
                    ThreadGetCompareItem();
                    ArrayList al = new ArrayList();

                    compareItem = null;
                    Neusoft.HISFC.Models.Base.PactItemRate pRate = null;

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
                            compareItem = new Neusoft.HISFC.Models.SIInterface.Compare();
                            compareItem.CenterItem.Rate = pRate.Rate.PayRate;
                        }
                    }
                }
                else
                {
                    compareItem = hsCompareItem[pactInfo.ID + "|" + ItemID] as Neusoft.HISFC.Models.SIInterface.Compare;
                    if (compareItem == null)
                    {
                        GetPact();
                        if (hsPactItem != null)
                        {
                            compareItem = hsPactItem[pactInfo.ID + "|" + ItemID] as Neusoft.HISFC.Models.SIInterface.Compare;
                        }
                    }

                    if (compareItem == null)
                    {
                        ExtendInfoTxt = "";
                        return 1;
                    }
                }

                //ҽ������
                string SIRate = string.Format("{0}", compareItem.CenterItem.Rate * 100) + "%";
                //��Ӧ֢��Ϣ
                string Practicablesymptomdepiction = compareItem.Practicablesymptomdepiction;

                if (this.itemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                {
                    drugItem = Neusoft.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(ItemID);

                    txtReturn = "���ƣ���" + drugItem.Name + "��  ���룺" + (string.IsNullOrEmpty(drugItem.UserCode) ? drugItem.ID : drugItem.UserCode)
                        + "\r\n\r\n����" + this.GetItemGrade(compareItem.CenterItem.ItemGrade) + "  ������" + SIRate
                        + "\r\n\r\n" + "������Ϣ��" + Practicablesymptomdepiction;
                }
                else
                {
                    Neusoft.HISFC.Models.Fee.Item.Undrug undrug = null;

                    undrug = Neusoft.SOC.HISFC.BizProcess.Cache.Fee.GetItem(ItemID);
                    if (undrug == null)
                    {
                        errInfo = itemMgr.Err;
                        return -1;
                    }
                    else
                    {
                        txtReturn = "���룺" + undrug.UserCode + "\r\n" + "���ƣ�" + undrug.Name + "\r\n����" + this.GetItemGrade(compareItem.CenterItem.ItemGrade) + "\r\n" + "������" + SIRate + "\r\n" + "��Ӧ֢��" + Practicablesymptomdepiction;
                    }
                }

                ExtendInfoTxt = txtReturn;
                //AlExtendInfo = al;
            }
            catch { }
            return 1;
        }

        private void GetPact()
        {
            if (hsPactItem == null)
            {
                Compare comMgr = new Compare();
                hsPactItem = comMgr.GetPactItem();
            }
        }

        private string GetItemGrade(string itemGrade)
        {
            return Neusoft.HISFC.BizLogic.Fee.Interface.ShowItemGradeByCode(itemGrade);
            //switch (itemGrade)
            //{
            //    case "1":
            //        return "����";
            //        break;
            //    case "2":
            //        return "����";
            //    default:
            //        return "�Է�";
            //}
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

        Hashtable hsPact = new Hashtable();
        public Neusoft.HISFC.Models.Base.PactInfo PactInfo
        {
            get
            {
                return this.pactInfo;
            }
            set
            {

                if (value.PayKind.ID == "02")
                {
                    this.pactInfo = value;
                }
                else
                {
                    string pactStr = controlParamManager.GetControlParam<string>("LHMZ06", false);
                    if (!string.IsNullOrEmpty(pactStr))
                    {
                        GetPact();
                        if (hsPactItem != null && hsPactItem.Contains(pactStr))
                        {
                            pactInfo = hsPactItem[pactStr] as Neusoft.HISFC.Models.Base.PactInfo;
                        }
                        else
                        {
                            this.pactInfo = pactUnitInfoBizLogic.GetPactUnitInfoByPactCode(pactStr);
                            hsPactItem.Add(pactInfo.ID, pactInfo.Clone());
                        }
                    }
                }

            }
        }

        #endregion
    }

    /// <summary>
    /// ��ȡҽ��������Ŀ
    /// </summary>
    public class Compare : Neusoft.FrameWork.Management.Database
    {
        public Hashtable GetCompareItem()
        {
            string sql = @"select f.pact_code ��ͬ��λ����,
                               f.his_code ��Ŀ����,
                               f.center_item_grade �ȼ�,
                               f.center_rate ��������,
                               f.center_memo ������Ϣ
                        from fin_com_compare f";

            Neusoft.HISFC.Models.SIInterface.Compare compareItem = null;
            Hashtable hsItem = new Hashtable();
            try
            {
                this.ExecQuery(sql);
                while (this.Reader.Read())
                {
                    compareItem = new Neusoft.HISFC.Models.SIInterface.Compare();

                    //0��ͬ��λ
                    compareItem.CenterItem.PactCode = Reader[0].ToString();

                    //1������Ŀ����
                    compareItem.HisCode = Reader[1].ToString();

                    //11ҽ��Ŀ¼�ȼ� 1 ����(ͳ��ȫ��֧��) 2 ����(׼�貿��֧��) 3 �Է�
                    compareItem.CenterItem.ItemGrade = Reader[2].ToString();

                    compareItem.CenterItem.Rate = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[3]);
                    //26������������Ӧ֢������
                    compareItem.Practicablesymptomdepiction = Reader[4].ToString();

                    if (!hsItem.Contains(compareItem.CenterItem.PactCode + "|" + compareItem.HisCode))
                    {
                        hsItem.Add(compareItem.CenterItem.PactCode + "|" + compareItem.HisCode, compareItem);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return hsItem;
            }
            return hsItem;
        }

        public Hashtable GetPactItem()
        {
            string sql = @"select f.pact_code ��ͬ��λ����,
                                   f.fee_code ��Ŀ����,
                                   ' ' �ȼ�,--δ֪
                                   f.pub_ratio ��������
                            from fin_com_pactunitfeecoderate f";

            Neusoft.HISFC.Models.SIInterface.Compare compareItem = null;
            Hashtable hsItem = new Hashtable();
            try
            {
                this.ExecQuery(sql);
                while (this.Reader.Read())
                {
                    compareItem = new Neusoft.HISFC.Models.SIInterface.Compare();

                    //0��ͬ��λ
                    compareItem.CenterItem.PactCode = Reader[0].ToString();

                    //1������Ŀ����
                    compareItem.HisCode = Reader[1].ToString();

                    //11ҽ��Ŀ¼�ȼ� 1 ����(ͳ��ȫ��֧��) 2 ����(׼�貿��֧��) 3 �Է�
                    compareItem.CenterItem.ItemGrade = Reader[2].ToString();

                    compareItem.CenterItem.Rate = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[3]);

                    //26������������Ӧ֢������
                    //compareItem.Practicablesymptomdepiction = Reader[4].ToString();

                    if (!hsItem.Contains(compareItem.CenterItem.PactCode + "|" + compareItem.HisCode))
                    {
                        hsItem.Add(compareItem.CenterItem.PactCode + "|" + compareItem.HisCode, compareItem);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return hsItem;
            }
            return hsItem;
        }
    }
}
