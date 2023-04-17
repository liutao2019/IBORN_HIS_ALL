using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.Cache
{
    /// <summary>
    /// [功能描述: 缓存医嘱相关信息]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-12]<br></br>
    /// </summary
    public class Order
    {
        /// <summary>
        /// 缓存字典信息
        /// </summary>
        private static Dictionary<string, FS.FrameWork.Public.ObjectHelper> dictionary = new Dictionary<string, FS.FrameWork.Public.ObjectHelper>();

        #region 医嘱类别

        /// <summary>
        /// 缓存医嘱类别
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper OrderTypeHelper
        {
            get
            {
                if (dictionary.ContainsKey("OrderType"))
                {
                    return dictionary["OrderType"];
                }
                else
                {
                    FS.FrameWork.Public.ObjectHelper orderTypeHelper = new FS.FrameWork.Public.ObjectHelper();
                    FS.HISFC.BizLogic.Manager.OrderType orderManager = new FS.HISFC.BizLogic.Manager.OrderType();
                    orderTypeHelper.ArrayObject = orderManager.GetList();

                    return orderTypeHelper;
                }
            }
        }

        /// <summary>
        /// 获取医嘱类别
        /// </summary>
        /// <returns></returns>
        public static ArrayList GetOrderType()
        {
            return OrderTypeHelper.ArrayObject;
        }

        /// <summary>
        /// 获取医嘱类别
        /// </summary>
        /// <returns></returns>
        public static FS.HISFC.Models.Order.OrderType GetOrderType(string typeID)
        {
            return (OrderTypeHelper.GetObjectFromID(typeID) as FS.HISFC.Models.Order.OrderType).Clone();
        }

        /// <summary>
        /// 获取医嘱类别
        /// </summary>
        /// <param name="isShort">是否临嘱</param>
        /// <returns></returns>
        public static ArrayList GetOrderType(bool isShort)
        {
            ArrayList alTemp = new ArrayList();
            foreach (FS.HISFC.Models.Order.OrderType obj in OrderTypeHelper.ArrayObject)
            {
                if (isShort)
                {
                    if (!obj.IsDecompose)
                    {
                        alTemp.Add(obj);
                    }
                }
                else
                {
                    if (obj.IsDecompose)
                    {
                        alTemp.Add(obj);
                    }
                }
            }

            return alTemp;
        }

        /// <summary>
        /// 根据医嘱类别编码获取医嘱类别名称
        /// </summary>
        /// <param name="orderTypeNO">医嘱类别编码</param>
        /// <returns>医嘱类别名称</returns>
        public static string GetOrderTypeName(string orderTypeNO)
        {
            if (string.IsNullOrEmpty(orderTypeNO))
            {
                return "";
            }

            if (OrderTypeHelper == null || OrderTypeHelper.ArrayObject == null)
            {
                return "获取医嘱类别信息发生错误";
            }
            return OrderTypeHelper.GetName(orderTypeNO);
        }
        #endregion

        #region 医嘱项目类别

        /// <summary>
        /// 缓存医嘱类别
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper OrderSysTypeHelper
        {
            get
            {
                if (dictionary.ContainsKey("OrderSysType"))
                {
                    return dictionary["OrderSysType"];
                }
                else
                {
                    ArrayList alAllType = FS.HISFC.Models.Base.SysClassEnumService.List();

                    FS.FrameWork.Models.NeuObject objAll = new FS.FrameWork.Models.NeuObject();
                    objAll.ID = "ALL";
                    objAll.Name = "全部";
                    alAllType.Add(objAll);

                    FS.FrameWork.Public.ObjectHelper orderTypeHelper = new FS.FrameWork.Public.ObjectHelper(alAllType);

                    dictionary.Add("OrderSysType", orderTypeHelper);

                    return orderTypeHelper;
                }
            }
        }

        /// <summary>
        /// 获取医嘱允许开立的系统类别
        /// </summary>
        /// <param name="isShort">是否临嘱</param>
        /// <returns></returns>
        public static ArrayList GetOrderSysType(bool isShort)
        {
            if (isShort)
            {
                return OrderSysTypeHelper.ArrayObject;//临时医嘱显示全部
            }

            //长期医嘱屏蔽些东西
            ArrayList alLongOrderSysType = new ArrayList();
            foreach (FS.FrameWork.Models.NeuObject obj in OrderSysTypeHelper.ArrayObject)
            {
                switch (obj.ID)
                {
                    case "UO": //手术
                    //case "UC": //检查
                    //case "UL":  //检验
                    case "PCC": //中草药
                    case "MC": //会诊
                    case "MRB": //转床
                    case "MRD": //转科
                    case "MRH": //预约出院
                        break;
                    default:
                        alLongOrderSysType.Add(obj);
                        break;
                }
            }
            return alLongOrderSysType;
        }

        #endregion

        #region 频次

        /// <summary>
        /// 频次帮助类
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper FrequencyHelper
        {
            get
            {
                if (dictionary.ContainsKey("Frequency"))
                {
                    return dictionary["Frequency"];
                }
                else
                {
                    FS.HISFC.BizLogic.Manager.Frequency managerIntegrate = new FS.HISFC.BizLogic.Manager.Frequency();
                    ArrayList al = managerIntegrate.GetAll("Root");
                    if (al != null)
                    {
                        FS.FrameWork.Public.ObjectHelper objectHepler = new FS.FrameWork.Public.ObjectHelper();
                        objectHepler.ArrayObject = al as ArrayList;
                        dictionary["Frequency"] = objectHepler;

                        return dictionary["Frequency"];
                    }
                    return new FS.FrameWork.Public.ObjectHelper();
                }
            }
        }

        public static ArrayList QueryFrequency()
        {
            return FrequencyHelper.ArrayObject;
        }

        public static FS.HISFC.Models.Order.Frequency GetFrequency(string frequencyCode)
        {
            FS.HISFC.Models.Order.Frequency frequency = null;

            frequency = FrequencyHelper.GetObjectFromID(frequencyCode) as FS.HISFC.Models.Order.Frequency;

            if (frequency == null)
            {
                return new FS.HISFC.Models.Order.Frequency();
            }

            return frequency.Clone();
        }

        public static string GetFrequencyName(string freqencyCode)
        {
            FS.HISFC.Models.Order.Frequency frequency = GetFrequency(freqencyCode);
            return frequency == null ? "" : frequency.Name;
        }



        /// <summary>
        /// 根据频次获得每天执行次数
        /// </summary>
        /// <param name="frequencyID"></param>
        /// <returns></returns>
        public static decimal GetFrequencyCount(string frequencyID)
        {
            FS.HISFC.Models.Order.Frequency frequencyObj = null;

            if (string.IsNullOrEmpty(frequencyID))
            {
                return -1;
            }
            string id = frequencyID.ToLower();
            if (id == "qd")//每天一次
            {
                return 1;
            }
            else if (id.Contains("qd"))
            {
                return 1;
            }
            else if (id == "bid")//每天两次
            {
                return 2;
            }
            else if (id == "tid")//每天三次
            {
                return 3;
            }
            else if (id == "hs")//睡前
            {
                return 1;
            }
            else if (id == "qn")//每晚一次
            {
                return 1;
            }
            else if (id == "qid")//每天四次
            {
                return 4;
            }
            else if (id == "pcd")//晚餐后
            {
                return 1;
            }
            else if (id == "pcl")//午餐后
            {
                return 1;
            }
            else if (id == "pcm")//早餐后
            {
                return 1;
            }
            else if (id == "prn")//必要时服用
            {
                return 1;
            }
            else if (id == "遵医嘱")
            {
                return 1;
            }
            else
            {
                frequencyObj = GetFrequency(frequencyID) as FS.HISFC.Models.Order.Frequency;

                if (frequencyObj != null)
                {
                    string[] str = frequencyObj.Time.Split('-');

                    if (!string.IsNullOrEmpty(frequencyObj.Days[0]))
                    {
                        return str.Length / FS.FrameWork.Function.NConvert.ToDecimal(frequencyObj.Days[0]);
                    }
                    else
                    {
                        return str.Length;
                    }
                }
                return -1;
            }
        }


        #endregion

        #region ICD10

        private static FS.FrameWork.Public.ObjectHelper ICD10Helper
        {
            get
            {
                if (dictionary.ContainsKey("ICD10"))
                {
                    return dictionary["ICD10"];
                }
                else
                {
                    FS.HISFC.BizLogic.HealthRecord.ICD icdManager = new FS.HISFC.BizLogic.HealthRecord.ICD();

                    FS.FrameWork.Public.ObjectHelper icd10Helper = new FS.FrameWork.Public.ObjectHelper(icdManager.Query(
                        FS.HISFC.Models.HealthRecord.EnumServer.ICDTypes.ICD10,
                        FS.HISFC.Models.HealthRecord.EnumServer.QueryTypes.Valid));

                    dictionary.Add("ICD10", icd10Helper);
                    return icd10Helper;
                }
            }
        }

        /// <summary>
        /// ICD10
        /// </summary>
        /// <returns></returns>
        public static ArrayList GetICD10()
        {
            if (ICD10Helper == null || ICD10Helper.ArrayObject == null)
            {
                return null;
            }
            return ICD10Helper.ArrayObject;
        }
        #endregion

        /// <summary>
        /// 转归信息
        /// </summary>
        private static FS.FrameWork.Public.ObjectHelper ZGHelper
        {
            get
            {
                if (dictionary.ContainsKey("ZG"))
                {
                    return dictionary["ZG"];
                }
                else
                {
                    FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();
                    ArrayList alZG = conMgr.GetAllList("ZG");

                    FS.FrameWork.Public.ObjectHelper zgHelper = new FS.FrameWork.Public.ObjectHelper(alZG);
                    dictionary.Add("ZG", zgHelper);
                    return zgHelper;
                }
            }
        }

        /// <summary>
        /// 获得转归列表
        /// </summary>
        /// <returns></returns>
        public static ArrayList GetZG()
        {
            if (ZGHelper == null || ZGHelper.ArrayObject == null)
            {
                return null;
            }
            return ZGHelper.ArrayObject;
        }

        /// <summary>
        /// 获得转归信息
        /// </summary>
        /// <param name="zgCode"></param>
        /// <returns></returns>
        public static FS.HISFC.Models.Base.Const GetZGInfo(string zgCode)
        {
            return ZGHelper.GetObjectFromID(zgCode) as FS.HISFC.Models.Base.Const;
        }

        #region 这种不适合放在缓存...待清理


        /// <summary>
        /// 是否新农合？？？
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper CooperationHelper
        {
            get
            {
                if (dictionary.ContainsKey("Cooperation"))
                {
                    return dictionary["Cooperation"];
                }
                else
                {
                    FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();
                    ArrayList al = constantMgr.GetAllList("ItemExtend#PACTUNIT");
                    FS.FrameWork.Public.ObjectHelper cooperationHelper = new FS.FrameWork.Public.ObjectHelper(al);
                    return cooperationHelper;
                }
            }
        }

        public static bool IsCooperation(string ComparID)
        {
            bool isCooperation = false;
            if (string.IsNullOrEmpty(ComparID))
            {
                return isCooperation;
            }

            if (CooperationHelper == null || CooperationHelper.ArrayObject == null)
            {
                return isCooperation;
            }
            FS.FrameWork.Models.NeuObject neuObject = CooperationHelper.GetObjectFromID(ComparID);
            if (neuObject == null)
            {
                return isCooperation;
            }
            else
            {
                isCooperation = true;
            }
            return isCooperation;
        }
        #endregion
    }
}
