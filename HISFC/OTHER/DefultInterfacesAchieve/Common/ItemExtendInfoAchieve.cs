using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Threading;

namespace Neusoft.DefultInterfacesAchieve.Common
{
    /// <summary>
    /// 获取项目医保对照信息
    /// </summary>
    public class ItemExtendInfoAchieve : Neusoft.HISFC.BizProcess.Interface.Common.IItemExtendInfo
    {

        #region 变量

        Neusoft.HISFC.BizLogic.Fee.Item itemMgr = new Neusoft.HISFC.BizLogic.Fee.Item();
        Neusoft.HISFC.BizLogic.Fee.UndrugPackAge undrugPkgMgr = new Neusoft.HISFC.BizLogic.Fee.UndrugPackAge();
        Neusoft.HISFC.BizLogic.Pharmacy.Item phaMgr = new Neusoft.HISFC.BizLogic.Pharmacy.Item();
        Neusoft.HISFC.BizLogic.Fee.Interface interfaceMgr = new Neusoft.HISFC.BizLogic.Fee.Interface();

        private Neusoft.HISFC.Models.Base.EnumItemType itemType = Neusoft.HISFC.Models.Base.EnumItemType.Drug;

        private Neusoft.HISFC.Models.Base.PactInfo pactInfo = new Neusoft.HISFC.Models.Base.PactInfo();

        /// <summary>
        /// 控制参数信息
        /// </summary>
        private  Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam controlParamManager = new Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam();


        /// <summary>
        /// 合同单位信息
        /// </summary>
        private Neusoft.HISFC.BizLogic.Fee.PactUnitInfo pactUnitInfoBizLogic = new Neusoft.HISFC.BizLogic.Fee.PactUnitInfo();

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

        Neusoft.HISFC.Models.SIInterface.Compare compareItem = null;

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

        private Neusoft.HISFC.BizLogic.Fee.PactUnitItemRate pactItemRate = new Neusoft.HISFC.BizLogic.Fee.PactUnitItemRate();

        /// <summary>
        /// 获得合同单位维护的报销信息
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

        #region IItemExtendInfo 成员

        Neusoft.HISFC.Models.Pharmacy.Item drugItem = null;

        /// <summary>
        /// 医保对照项目列表
        /// </summary>
        Hashtable hsCompareItem = null;

        /// <summary>
        /// 合同单位维护优惠列表
        /// </summary>
        Hashtable hsPactItem = null;

        /// <summary>
        /// 是否已经使用多线程初始化 对照信息完毕？
        /// </summary>
        bool isInitCompareItem = false;

        /// <summary>
        /// 启用多线程
        /// </summary>
        private void ThreadGetCompareItem()
        {
            ThreadStart threadStart = new ThreadStart(GetCompareItem);
            Thread thread = new Thread(threadStart);
            thread.Start(); 
        }

        /// <summary>
        /// 获取所有医保对照信息
        /// </summary>
        private void GetCompareItem()
        {
            Compare comMgr = new Compare();
            hsCompareItem = comMgr.GetCompareItem();
            isInitCompareItem = true;
        }

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

                //医保比例
                string SIRate = string.Format("{0}", compareItem.CenterItem.Rate * 100) + "%";
                //适应症信息
                string Practicablesymptomdepiction = compareItem.Practicablesymptomdepiction;

                if (this.itemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                {
                    drugItem = Neusoft.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(ItemID);

                    txtReturn = "名称：【" + drugItem.Name + "】  编码：" + (string.IsNullOrEmpty(drugItem.UserCode) ? drugItem.ID : drugItem.UserCode)
                        + "\r\n\r\n级别：" + this.GetItemGrade(compareItem.CenterItem.ItemGrade) + "  比例：" + SIRate
                        + "\r\n\r\n" + "其他信息：" + Practicablesymptomdepiction;
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
                        txtReturn = "编码：" + undrug.UserCode + "\r\n" + "名称：" + undrug.Name + "\r\n级别：" + this.GetItemGrade(compareItem.CenterItem.ItemGrade) + "\r\n" + "比例：" + SIRate + "\r\n" + "适应症：" + Practicablesymptomdepiction;
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
            //        return "甲类";
            //        break;
            //    case "2":
            //        return "乙类";
            //    default:
            //        return "自费";
            //}
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
    /// 获取医保对照项目
    /// </summary>
    public class Compare : Neusoft.FrameWork.Management.Database
    {
        public Hashtable GetCompareItem()
        {
            string sql = @"select f.pact_code 合同单位编码,
                               f.his_code 项目编码,
                               f.center_item_grade 等级,
                               f.center_rate 报销比例,
                               f.center_memo 其它信息
                        from fin_com_compare f";

            Neusoft.HISFC.Models.SIInterface.Compare compareItem = null;
            Hashtable hsItem = new Hashtable();
            try
            {
                this.ExecQuery(sql);
                while (this.Reader.Read())
                {
                    compareItem = new Neusoft.HISFC.Models.SIInterface.Compare();

                    //0合同单位
                    compareItem.CenterItem.PactCode = Reader[0].ToString();

                    //1本地项目编码
                    compareItem.HisCode = Reader[1].ToString();

                    //11医保目录等级 1 甲类(统筹全部支付) 2 乙类(准予部分支付) 3 自费
                    compareItem.CenterItem.ItemGrade = Reader[2].ToString();

                    compareItem.CenterItem.Rate = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[3]);
                    //26特限描述（适应症描述）
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
            string sql = @"select f.pact_code 合同单位编码,
                                   f.fee_code 项目编码,
                                   ' ' 等级,--未知
                                   f.pub_ratio 报销比例
                            from fin_com_pactunitfeecoderate f";

            Neusoft.HISFC.Models.SIInterface.Compare compareItem = null;
            Hashtable hsItem = new Hashtable();
            try
            {
                this.ExecQuery(sql);
                while (this.Reader.Read())
                {
                    compareItem = new Neusoft.HISFC.Models.SIInterface.Compare();

                    //0合同单位
                    compareItem.CenterItem.PactCode = Reader[0].ToString();

                    //1本地项目编码
                    compareItem.HisCode = Reader[1].ToString();

                    //11医保目录等级 1 甲类(统筹全部支付) 2 乙类(准予部分支付) 3 自费
                    compareItem.CenterItem.ItemGrade = Reader[2].ToString();

                    compareItem.CenterItem.Rate = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[3]);

                    //26特限描述（适应症描述）
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
