using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.Cache
{
    /// <summary>
    /// [功能描述: 缓存收费相关等]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-12]<br></br>
    /// </summary
    public class Fee
    {
        #region 非药品项目信息

        /// <summary>
        /// 非药品项目帮助类
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper undrugHelper = null;

        /// <summary>
        /// 非药品项目缓存信息
        /// </summary>
        private static Dictionary<string, FS.HISFC.Models.Fee.Item.Undrug> dicUndrugItem = null;

        /// <summary>
        /// 有效的非药品项目
        /// </summary>
        //public static FS.FrameWork.Public.ObjectHelper validUndrugHelper = null;

        /// <summary>
        /// 获取非药品基本信息
        /// </summary>
        /// <param name="itemNO">非药品系统编码</param>
        /// <returns>非药品自定义码</returns>
        public static FS.HISFC.Models.Fee.Item.Undrug GetItem(string itemNO)
        {
            FS.HISFC.Models.Fee.Item.Undrug undrug = null;
            if (undrugHelper == null)
            {
                if (dicUndrugItem == null)
                {
                    dicUndrugItem = new Dictionary<string, FS.HISFC.Models.Fee.Item.Undrug>();
                }
                if (dicUndrugItem.ContainsKey(itemNO))
                {
                    return dicUndrugItem[itemNO];
                }
                else
                {
                    FS.HISFC.BizLogic.Fee.Item itemMgr = new FS.HISFC.BizLogic.Fee.Item();
                    undrug = itemMgr.GetUndrugByCode(itemNO);
                    dicUndrugItem.Add(itemNO, undrug);
                }
            }
            else
            {
                undrug = undrugHelper.GetObjectFromID(itemNO) as FS.HISFC.Models.Fee.Item.Undrug;
            }
            if (undrug != null)
            {
                return undrug.Clone();
            }
            //{BF3AB296-0D4A-4d39-9935-17A1260A721A}由于打印无法获取完整的可用医嘱列表，所以有必要时额外查询undrug
            if (undrug == null)
            {
                FS.HISFC.BizLogic.Fee.Item itemMgr = new FS.HISFC.BizLogic.Fee.Item();
                undrug = itemMgr.GetUndrugByCode(itemNO);
            }

            return undrug;
        }

        /// <summary>
        /// 获取所有在用的非药品
        /// </summary>
        /// <returns></returns>
        public static ArrayList GetValidItem()
        {
            ArrayList al = new ArrayList();
            if (undrugHelper == null)
            {
                FS.HISFC.BizLogic.Fee.Item itemMgr = new FS.HISFC.BizLogic.Fee.Item();
                undrugHelper = new FS.FrameWork.Public.ObjectHelper();
                undrugHelper.ArrayObject = new ArrayList(itemMgr.QueryItemsForOrder(((FS.HISFC.Models.Base.Employee)itemMgr.Operator).Dept.ID, true));

                foreach (FS.HISFC.Models.Fee.Item.Undrug undrug in undrugHelper.ArrayObject)
                {
                    if (undrug.IsValid)
                    {

                        if (undrug.FTRate.EMCRate > 0)
                        {
                            undrug.Price = undrug.Price * undrug.FTRate.EMCRate;

                            undrug.SpecialPrice = undrug.SpecialPrice * undrug.FTRate.EMCRate;
                        }
                        al.Add(undrug);
                    }
                }

            }
            else
            {
                al.AddRange(undrugHelper.ArrayObject);
            }
            return al;
        }

        /// <summary>
        /// 获取非药品的自定义码
        /// </summary>
        /// <param name="itemNO"></param>
        /// <returns></returns>
        public static string GetItemUserCode(string itemNO)
        {
            FS.HISFC.Models.Fee.Item.Undrug undrug = null;
            if (undrugHelper == null)
            {
                if (dicUndrugItem == null)
                {
                    dicUndrugItem = new Dictionary<string, FS.HISFC.Models.Fee.Item.Undrug>();
                }
                if (dicUndrugItem.ContainsKey(itemNO))
                {
                    return dicUndrugItem[itemNO].UserCode;
                }
                else
                {
                    FS.HISFC.BizLogic.Fee.Item itemMgr = new FS.HISFC.BizLogic.Fee.Item();
                    undrug = itemMgr.GetUndrugByCode(itemNO);
                    dicUndrugItem.Add(itemNO, undrug);
                }
            }
            else
            {
                undrug = undrugHelper.GetObjectFromID(itemNO) as FS.HISFC.Models.Fee.Item.Undrug;
            }
            if (undrug != null)
            {
                return undrug.UserCode;
            }
            return null;
        }

        #endregion

        #region 复合项目信息

        /// <summary>
        /// 复合项目明细
        /// </summary>
        public static Hashtable hsUndrugZTDetail = null;

        /// <summary>
        /// 根据复合项目编码获取复合项目明细
        /// </summary>
        /// <param name="ztItemCode"></param>
        /// <returns></returns>
        public static List<FS.HISFC.Models.Fee.Item.UndrugComb> GetUndrugZTDetail(string ztItemCode)
        {
            try
            {
                if (hsUndrugZTDetail == null)
                {
                    hsUndrugZTDetail = new Hashtable();

                }
                List<FS.HISFC.Models.Fee.Item.UndrugComb> ztList = new List<FS.HISFC.Models.Fee.Item.UndrugComb>();
                FS.HISFC.BizLogic.Manager.UndrugztManager undrugZTMgr = new FS.HISFC.BizLogic.Manager.UndrugztManager();

                if (!hsUndrugZTDetail.ContainsKey(ztItemCode))
                {
                    undrugZTMgr.QueryUnDrugztDetail(ztItemCode, ref ztList);
                    hsUndrugZTDetail.Add(ztItemCode, ztList);
                }
                /*
                foreach (FS.HISFC.Models.Fee.Item.UndrugComb undrug in ztList)
                {
                    if (!hsUndrugZTDetail.Contains(undrug.Package.ID))
                    {
                        List<FS.HISFC.Models.Fee.Item.UndrugComb> al = new List<FS.HISFC.Models.Fee.Item.UndrugComb>();
                        al.Add(undrug);
                        hsUndrugZTDetail.Add(undrug.Package.ID, al);
                    }
                    else
                    {
                        List<FS.HISFC.Models.Fee.Item.UndrugComb> al = hsUndrugZTDetail[undrug.Package.ID] as List<FS.HISFC.Models.Fee.Item.UndrugComb>;
                        al.Add(undrug);
                        hsUndrugZTDetail[undrug.Package.ID] = al;
                    }
                }
                */

            }
            catch
            {
                return null;
            }

            return hsUndrugZTDetail[ztItemCode] as List<FS.HISFC.Models.Fee.Item.UndrugComb>;
        }

        /// <summary>
        /// 获取符合项目中的一条明细项目
        /// </summary>
        /// <param name="ztItemCode"></param>
        /// <param name="detailCode"></param>
        /// <returns></returns>
        public static FS.HISFC.Models.Fee.Item.UndrugComb GetUndrugCombFromZT(string ztItemCode, string detailCode)
        {
            List<FS.HISFC.Models.Fee.Item.UndrugComb> listZT = GetUndrugZTDetail(ztItemCode);

            if (listZT == null
                || listZT.Count == 0)
            {
            }
            else
            {
                foreach (FS.HISFC.Models.Fee.Item.UndrugComb undrugComb in listZT)
                {
                    if (undrugComb.ID == detailCode)
                    {
                        return undrugComb;
                    }
                }
            }

            return null;
        }

        #endregion

        #region 挂号级别

        public static FS.FrameWork.Public.ObjectHelper regHelper = null;

        private static ArrayList alValidRegLevl = null;

        /// <summary>
        /// 获取所有挂号级别
        /// </summary>
        /// <returns></returns>
        public static ArrayList GetAllRegLevl()
        {
            if (regHelper == null)
            {
                regHelper = new FS.FrameWork.Public.ObjectHelper();
                FS.HISFC.BizLogic.Registration.RegLevel regLevlMgr = new FS.HISFC.BizLogic.Registration.RegLevel();
                regHelper.ArrayObject = regLevlMgr.Query();
            }
            return regHelper.ArrayObject;
        }

        /// <summary>
        /// 获取所有挂号级别
        /// </summary>
        /// <returns></returns>
        public static ArrayList GetValidRegLevl()
        {
            if (regHelper == null)
            {
                regHelper = new FS.FrameWork.Public.ObjectHelper();
                FS.HISFC.BizLogic.Registration.RegLevel regLevlMgr = new FS.HISFC.BizLogic.Registration.RegLevel();
                regHelper.ArrayObject = regLevlMgr.Query();
            }

            if (alValidRegLevl == null)
            {
                alValidRegLevl = new ArrayList();
                foreach (FS.HISFC.Models.Registration.RegLevel regLevl in regHelper.ArrayObject)
                {
                    if (regLevl.IsValid)
                    {
                        alValidRegLevl.Add(regLevl);
                    }
                }
            }

            return alValidRegLevl;
        }

        /// <summary>
        /// 根据挂号级别编码获取挂号级别
        /// </summary>
        /// <param name="levlCode"></param>
        /// <returns></returns>
        public static FS.HISFC.Models.Registration.RegLevel GetRegLevl(string levlCode)
        {
            if (regHelper == null)
            {
                regHelper = new FS.FrameWork.Public.ObjectHelper();
                FS.HISFC.BizLogic.Registration.RegLevel regLevlMgr = new FS.HISFC.BizLogic.Registration.RegLevel();
                regHelper.ArrayObject = regLevlMgr.Query();
            }


            return regHelper.GetObjectFromID(levlCode) as FS.HISFC.Models.Registration.RegLevel;
        }

        /// <summary>
        /// 获取急诊挂号级别
        /// </summary>
        /// <param name="levlCode"></param>
        /// <returns></returns>
        public static FS.HISFC.Models.Registration.RegLevel GetEmergRegLevl()
        {
            if (regHelper == null)
            {
                regHelper = new FS.FrameWork.Public.ObjectHelper();
                FS.HISFC.BizLogic.Registration.RegLevel regLevlMgr = new FS.HISFC.BizLogic.Registration.RegLevel();
                regHelper.ArrayObject = regLevlMgr.Query();
            }

            foreach (FS.HISFC.Models.Registration.RegLevel regLevl in regHelper.ArrayObject)
            {
                if (regLevl.IsValid && regLevl.IsEmergency)
                {
                    return regLevl;
                }
            }

            foreach (FS.HISFC.Models.Registration.RegLevel regLevl in regHelper.ArrayObject)
            {
                if (regLevl.IsEmergency)
                {
                    return regLevl;
                }
            }

            return null;
        }

        #endregion

        #region 挂号费

        /// <summary>
        /// 挂号费帮助类
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper regLevlFeeHelper = null;

        /// <summary>
        /// 获取所有维护的挂号费
        /// </summary>
        /// <returns></returns>
        public static ArrayList GetRegLevlFee()
        {
            if (regLevlFeeHelper == null)
            {
                regLevlFeeHelper = new FS.FrameWork.Public.ObjectHelper();
                FS.HISFC.BizLogic.Registration.RegLvlFee regLevlFeeMgr = new FS.HISFC.BizLogic.Registration.RegLvlFee();
                regLevlFeeHelper.ArrayObject = regLevlFeeMgr.Query();
            }

            return regLevlFeeHelper.ArrayObject;
        }

        private static Dictionary<string, Hashtable> dicRegLevlFee = null;

        /// <summary>
        /// 根据合同单位获取对应的的挂号费
        /// </summary>
        /// <param name="pactCode"></param>
        /// <returns></returns>
        public static ArrayList GetRegLevlFee(string pactCode)
        {
            if (regLevlFeeHelper == null)
            {
                regLevlFeeHelper = new FS.FrameWork.Public.ObjectHelper();
                FS.HISFC.BizLogic.Registration.RegLvlFee regLevlFeeMgr = new FS.HISFC.BizLogic.Registration.RegLvlFee();
                regLevlFeeHelper.ArrayObject = regLevlFeeMgr.Query();
            }
            if (dicRegLevlFee == null)
            {
                foreach (FS.HISFC.Models.Registration.RegLvlFee regFee in regLevlFeeHelper.ArrayObject)
                {
                    if (!dicRegLevlFee.ContainsKey(regFee.Pact.ID))
                    {
                        Hashtable hsFee = new Hashtable();
                        if (!hsFee.Contains(regFee.RegLevel.ID))
                        {
                            hsFee.Add(regFee.RegLevel.ID, regFee);
                        }
                        dicRegLevlFee.Add(regFee.Pact.ID, hsFee);
                    }
                    else
                    {
                        Hashtable hsFee = dicRegLevlFee[regFee.Pact.ID] as Hashtable;
                        if (!hsFee.Contains(regFee.RegLevel.ID))
                        {
                            hsFee.Add(regFee.RegLevel.ID, regFee);
                        }
                        dicRegLevlFee[regFee.Pact.ID] = hsFee;
                    }
                }
            }

            return new ArrayList(dicRegLevlFee[pactCode].Keys);
        }


        /// <summary>
        /// 根据合同单位、挂号级别获取对应的的挂号费
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="regLevlCode"></param>
        /// <returns></returns>
        public static FS.HISFC.Models.Registration.RegLvlFee GetRegLevlFee(string pactCode, string regLevlCode)
        {
            if (regLevlFeeHelper == null)
            {
                regLevlFeeHelper = new FS.FrameWork.Public.ObjectHelper();
                FS.HISFC.BizLogic.Registration.RegLvlFee regLevlFeeMgr = new FS.HISFC.BizLogic.Registration.RegLvlFee();
                regLevlFeeHelper.ArrayObject = regLevlFeeMgr.Query();
            }
            if (dicRegLevlFee == null)
            {
                foreach (FS.HISFC.Models.Registration.RegLvlFee regFee in regLevlFeeHelper.ArrayObject)
                {
                    if (!dicRegLevlFee.ContainsKey(regFee.Pact.ID))
                    {
                        Hashtable hsFee = new Hashtable();
                        if (!hsFee.Contains(regFee.RegLevel.ID))
                        {
                            hsFee.Add(regFee.RegLevel.ID, regFee);
                        }
                        dicRegLevlFee.Add(regFee.Pact.ID, hsFee);
                    }
                    else
                    {
                        Hashtable hsFee = dicRegLevlFee[regFee.Pact.ID] as Hashtable;
                        if (!hsFee.Contains(regFee.RegLevel.ID))
                        {
                            hsFee.Add(regFee.RegLevel.ID, regFee);
                        }
                        dicRegLevlFee[regFee.Pact.ID] = hsFee;
                    }
                }
            }

            if (dicRegLevlFee.ContainsKey(pactCode))
            {
                return dicRegLevlFee[pactCode][regLevlCode] as FS.HISFC.Models.Registration.RegLvlFee;
            }
            return null;
        }

        #endregion

        #region 合同单位

        /// <summary>
        /// 合同单位帮助类
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper pactHelper = null;

        /// <summary>
        /// 获取所有合同单位信息
        /// </summary>
        /// <returns></returns>
        public static ArrayList GetAllPactUnit()
        {
            if (pactHelper == null)
            {
                pactHelper = new FS.FrameWork.Public.ObjectHelper();
                FS.HISFC.BizLogic.Fee.PactUnitInfo pactMgr = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
                pactHelper.ArrayObject = pactMgr.QueryPactUnitAll();
            }
            return pactHelper.ArrayObject;
        }

        /// <summary>
        /// 获取住院合同单位信息
        /// </summary>
        /// <returns></returns>
        public static ArrayList GetPactUnitInPatient()
        {
            if (pactHelper == null)
            {
                pactHelper = new FS.FrameWork.Public.ObjectHelper();
                FS.HISFC.BizLogic.Fee.PactUnitInfo pactMgr = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
                pactHelper.ArrayObject = pactMgr.QueryPactUnitAll();
            }

            ArrayList alInpatientPact = new ArrayList();
            foreach (FS.HISFC.Models.Base.PactInfo pactObj in pactHelper.ArrayObject)
            {
                //PactSystemType 适用类别 0=全院、1=门诊、2=住院、3=系统后台使用
                if (pactObj.PactSystemType == "0" || pactObj.PactSystemType == "2")
                {
                    alInpatientPact.Add(pactObj);
                }
            }

            return alInpatientPact;
        }

        /// <summary>
        /// 获取门诊合同单位信息
        /// </summary>
        /// <returns></returns>
        public static ArrayList GetPactUnitOutPatient()
        {
            if (pactHelper == null)
            {
                pactHelper = new FS.FrameWork.Public.ObjectHelper();
                FS.HISFC.BizLogic.Fee.PactUnitInfo pactMgr = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
                pactHelper.ArrayObject = pactMgr.QueryPactUnitAll();
            }

            ArrayList alOutPatientPact = new ArrayList();
            foreach (FS.HISFC.Models.Base.PactInfo pactObj in pactHelper.ArrayObject)
            {
                //PactSystemType 适用类别 0=全院、1=门诊、2=住院、3=系统后台使用
                if (pactObj.PactSystemType == "0" || pactObj.PactSystemType == "1")
                {
                    alOutPatientPact.Add(pactObj);
                }
            }

            return alOutPatientPact;
        }

        /// <summary>
        /// 根据编码获取合同单位信息
        /// </summary>
        /// <param name="pactCode"></param>
        /// <returns></returns>
        public static FS.HISFC.Models.Base.PactInfo GetPactUnitInfo(string pactCode)
        {
            if (pactHelper == null)
            {
                pactHelper = new FS.FrameWork.Public.ObjectHelper();
                FS.HISFC.BizLogic.Fee.PactUnitInfo pactMgr = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
                pactHelper.ArrayObject = pactMgr.QueryPactUnitAll();
            }
            return pactHelper.GetObjectFromID(pactCode) as FS.HISFC.Models.Base.PactInfo;
        }

        #endregion
    }
}