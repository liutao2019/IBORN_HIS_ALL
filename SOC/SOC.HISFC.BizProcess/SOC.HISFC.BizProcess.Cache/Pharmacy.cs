using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.Cache
{
    /// <summary>
    /// [功能描述: 缓存药品基本信息等]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-12]<br></br>
    /// </summary
    public class Pharmacy
    {
        #region 药品基本信息

        /// <summary>
        /// 药品基本信息
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper itemHelper;

        /// <summary>
        /// 缓存药品基本信息，提高命中率
        /// </summary>
        private static Hashtable hsItem;

        /// <summary>
        /// 获取药品基本信息
        /// </summary>
        /// <param name="itemNO">药品系统编码</param>
        /// <returns>药品自定义码</returns>
        public static string GetItemCustomNO(string itemNO)
        {
            FS.HISFC.Models.Pharmacy.Item item = GetItem(itemNO);
            if (item == null)
            {
                return "";
            }
            return item.UserCode;
        }

        /// <summary>
        /// 获取系统类别
        /// </summary>
        /// <param name="itemNO">药品编码</param>
        /// <returns>系统类别PCC草药 PCZ中成药 P西药</returns>
        public static string GetItemSysClass(string itemNO)
        {
            FS.HISFC.Models.Pharmacy.Item item = GetItem(itemNO);
            if (item == null)
            {
                return "";
            }
            return item.SysClass.ID.ToString();
        }

        /// <summary>
        /// 获取药品临购属性
        /// </summary>
        /// <param name="itemNO"></param>
        /// <returns></returns>
        public static string GetItemSpecial(string itemNO)
        {
            FS.HISFC.Models.Pharmacy.Item item = GetItem(itemNO);
            if(item == null)
            {
                return "";
            }
            return item.SpecialFlag3;
        }

        /// <summary>
        /// 获取药品基本信息
        /// </summary>
        /// <param name="itemNO">药品系统编码</param>
        /// <returns>药品自定义码</returns>
        public static FS.HISFC.Models.Pharmacy.Item GetItem(string itemNO)
        {
            if (string.IsNullOrEmpty(itemNO))
            {
                return null;
            }
            if (itemHelper == null)
            {
                itemHelper = new FS.FrameWork.Public.ObjectHelper();
                FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
                itemHelper.ArrayObject = new System.Collections.ArrayList(itemManager.QueryItemList());
            }
            if (itemHelper == null || itemHelper.ArrayObject == null)
            {
                return null;
            }

            FS.HISFC.Models.Pharmacy.Item item = null;
            if (hsItem == null)
            {
                hsItem = new Hashtable();
            }
            else
            {
                if (hsItem.Contains(itemNO))
                {
                    item = (FS.HISFC.Models.Pharmacy.Item)hsItem[itemNO];
                    if (item != null)
                    {
                        return item.Clone();
                    }
                    return item;
                }
            }
            item = (FS.HISFC.Models.Pharmacy.Item)itemHelper.GetObjectFromID(itemNO);
            if (!hsItem.Contains(itemNO))
            {
                hsItem.Add(itemNO, item);
            }
            if (item != null)
            {
                return item.Clone();
            }
            return item;
        }

        #endregion

        #region 供货公司、厂家

        /// <summary>
        /// 供货公司
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper companyHelper;

        /// <summary>
        /// 获取供货公司名称
        /// </summary>
        /// <param name="companyNO">供货公司编码</param>
        /// <returns>供货公司名称</returns>
        public static string GetCompanyName(string companyNO)
        {
            if (string.IsNullOrEmpty(companyNO))
            {
                return null;
            }
            if (companyHelper == null)
            {
                companyHelper = new FS.FrameWork.Public.ObjectHelper();
                FS.HISFC.BizLogic.Pharmacy.Constant constantMgr = new FS.HISFC.BizLogic.Pharmacy.Constant();
                companyHelper.ArrayObject = new System.Collections.ArrayList(constantMgr.QueryCompany("1"));
            }
            if (companyHelper == null || companyHelper.ArrayObject == null)
            {
                return null;
            }
            FS.HISFC.Models.Pharmacy.Company item = (FS.HISFC.Models.Pharmacy.Company)companyHelper.GetObjectFromID(companyNO);
            if (item == null)
            {
                return "";
            }
            return item.Name;
        }

        public static string GetCompanyNameByBatchNoAndItemNo(string deptCode,string itemCode,string batchNo)
        {
            if (string.IsNullOrEmpty(itemCode))
            {
                return null;
            }
            if (string.IsNullOrEmpty(batchNo))
            {
                return null;
            }
            if (string.IsNullOrEmpty(deptCode))
            {
                return null;
            }
            FS.HISFC.BizLogic.Pharmacy.Item itemMgr= new FS.HISFC.BizLogic.Pharmacy.Item();
            ArrayList alTemp = itemMgr.QueryStorageList(deptCode, itemCode, batchNo);
            string companyNO = "";
            DateTime maxDt = new DateTime();
            foreach (FS.HISFC.Models.Pharmacy.Storage storage in alTemp)
            {
                if (maxDt < storage.Operation.Oper.OperTime)
                {
                    maxDt = storage.Operation.Oper.OperTime;
                    companyNO = storage.Producer.ID;
                }
            }
            if (companyHelper == null)
            {
                companyHelper = new FS.FrameWork.Public.ObjectHelper();
                FS.HISFC.BizLogic.Pharmacy.Constant constantMgr = new FS.HISFC.BizLogic.Pharmacy.Constant();
                companyHelper.ArrayObject = new System.Collections.ArrayList(constantMgr.QueryCompany("1"));
            }
            if (companyHelper == null || companyHelper.ArrayObject == null)
            {
                return null;
            }
            FS.HISFC.Models.Pharmacy.Company item = (FS.HISFC.Models.Pharmacy.Company)companyHelper.GetObjectFromID(companyNO);
            if (item == null)
            {
                return "";
            }
            return item.Name;
        }

        public static string GetFarteryNameByBatchNoAndItemNo(string deptCode, string itemCode, string batchNo)
        {
            if (string.IsNullOrEmpty(itemCode))
            {
                return null;
            }
            if (string.IsNullOrEmpty(batchNo))
            {
                return null;
            }
            if (string.IsNullOrEmpty(deptCode))
            {
                return null;
            }
            FS.HISFC.BizLogic.Pharmacy.Item itemMgr = new FS.HISFC.BizLogic.Pharmacy.Item();
            ArrayList alTemp = itemMgr.QueryStorageList(deptCode, itemCode, batchNo);
            string companyNO = "";
            DateTime maxDt = new DateTime();
            foreach (FS.HISFC.Models.Pharmacy.Storage storage in alTemp)
            {
                if (maxDt < storage.Operation.Oper.OperTime)
                {
                    maxDt = storage.Operation.Oper.OperTime;
                    companyNO = storage.Producer.ID;
                }
            }
            if (companyHelper == null)
            {
                companyHelper = new FS.FrameWork.Public.ObjectHelper();
                FS.HISFC.BizLogic.Pharmacy.Constant constantMgr = new FS.HISFC.BizLogic.Pharmacy.Constant();
                companyHelper.ArrayObject = new System.Collections.ArrayList(constantMgr.QueryCompany("0"));
            }
            if (companyHelper == null || companyHelper.ArrayObject == null)
            {
                return null;
            }
            FS.HISFC.Models.Pharmacy.Company item = (FS.HISFC.Models.Pharmacy.Company)companyHelper.GetObjectFromID(companyNO);
            if (item == null)
            {
                return "";
            }
            return item.Name;
        }

        /// <summary>
        /// 初始化供货公司
        /// </summary>
        public static void InitCompany()
        {
            if (companyHelper == null)
            {
                companyHelper = new FS.FrameWork.Public.ObjectHelper();
                FS.HISFC.BizLogic.Pharmacy.Constant constantMgr = new FS.HISFC.BizLogic.Pharmacy.Constant();
                companyHelper.ArrayObject = new System.Collections.ArrayList(constantMgr.QueryCompany("1"));
            }
        }

        /// <summary>
        /// 生产厂家
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper producerHelper;

        /// <summary>
        /// 获取生产厂家名称
        /// </summary>
        /// <param name="producerNO">生产厂家编码</param>
        /// <returns>生产厂家名称</returns>
        public static string GetProducerName(string producerNO)
        {
            if (string.IsNullOrEmpty(producerNO))
            {
                return null;
            }
            if (producerHelper == null)
            {
                producerHelper = new FS.FrameWork.Public.ObjectHelper();
                FS.HISFC.BizLogic.Pharmacy.Constant constantMgr = new FS.HISFC.BizLogic.Pharmacy.Constant();
                producerHelper.ArrayObject = new System.Collections.ArrayList(constantMgr.QueryCompany("0"));
            }
            if (producerHelper == null || producerHelper.ArrayObject == null)
            {
                return null;
            }
            FS.HISFC.Models.Pharmacy.Company item = (FS.HISFC.Models.Pharmacy.Company)producerHelper.GetObjectFromID(producerNO);
            if (item == null)
            {
                return "";
            }
            return item.Name;
        }

        /// <summary>
        /// 初始化生产厂家
        /// </summary>
        public static void InitProducer()
        {
            if (producerHelper == null)
            {
                producerHelper = new FS.FrameWork.Public.ObjectHelper();
                FS.HISFC.BizLogic.Pharmacy.Constant constantMgr = new FS.HISFC.BizLogic.Pharmacy.Constant();
                producerHelper.ArrayObject = new System.Collections.ArrayList(constantMgr.QueryCompany("0"));
            }
        }

        /// <summary>
        /// {83FC79A3-09AD-40c3-A442-98D89A169669}
        /// 获取生产厂家
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="itemCode"></param>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public static string GetProducerNameByBatchNoAndItemNo(string deptCode, string itemCode, string batchNo)
        {
            if (string.IsNullOrEmpty(itemCode))
            {
                return null;
            }
            if (string.IsNullOrEmpty(batchNo))
            {
                return null;
            }
            if (string.IsNullOrEmpty(deptCode))
            {
                return null;
            }
            FS.HISFC.BizLogic.Pharmacy.Item itemMgr = new FS.HISFC.BizLogic.Pharmacy.Item();
            //ArrayList alTemp = itemMgr.QueryStorageList(deptCode, itemCode, batchNo);
            //string companyNO = "";
            //DateTime maxDt = new DateTime();
            //foreach (FS.HISFC.Models.Pharmacy.Storage storage in alTemp)
            //{
            //    if (maxDt < storage.Operation.Oper.OperTime)
            //    {
            //        maxDt = storage.Operation.Oper.OperTime;
            //        companyNO = storage.Producer.ID;
            //    }
            //}
            string sql = @"SELECT PRODUCER_CODE FROM (
                                  SELECT ROW_NUMBER()over(order by OPER_DATE desc) ROWNUMBER,
                                         OPER_DATE,
                                         PRODUCER_CODE
                                    FROM PHA_COM_STORAGE  t
                                   WHERE DRUG_DEPT_CODE = '{0}'  
                                     AND DRUG_CODE = '{1}'  
                                     AND Valid_Flag = FUN_GET_VALID
                                     AND (BATCH_NO = '{2}' OR '{2}'= 'ALL'))
                            WHERE ROWNUMBER = 1";
            string companyNO = string.Empty;
            try
            {
                sql = string.Format(sql, deptCode, itemCode, batchNo);
                companyNO = itemMgr.ExecSqlReturnOne(sql);
            }
            catch
            {
                companyNO = string.Empty;
            }

            if (producerHelper == null)
            {
                InitProducer();
            }
            if (producerHelper == null || producerHelper.ArrayObject == null)
            {
                return null;
            }
            FS.HISFC.Models.Pharmacy.Company item = (FS.HISFC.Models.Pharmacy.Company)producerHelper.GetObjectFromID(companyNO);
            if (item == null)
            {
                return "";
            }
            return item.Name;
        }


        /// <summary>
        /// 院外单位
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper unitHelper;

        /// <summary>
        /// 获取院外单位名称
        /// </summary>
        /// <param name="unitNO">院外单位编码</param>
        /// <returns>院外单位名称</returns>
        public static string GetUnitName(string unitNO)
        {
            if (string.IsNullOrEmpty(unitNO))
            {
                return null;
            }
            if (unitHelper == null)
            {
                unitHelper = new FS.FrameWork.Public.ObjectHelper();
                FS.HISFC.BizLogic.Pharmacy.Constant constantMgr = new FS.HISFC.BizLogic.Pharmacy.Constant();
                unitHelper.ArrayObject = new System.Collections.ArrayList(constantMgr.QueryCompany("2"));
            }
            if (unitHelper == null || unitHelper.ArrayObject == null)
            {
                return null;
            }
            FS.HISFC.Models.Pharmacy.Company item = (FS.HISFC.Models.Pharmacy.Company)unitHelper.GetObjectFromID(unitNO);
            if (item == null)
            {
                return "";
            }
            return item.Name;
        }

        /// <summary>
        /// 初始化生产厂家
        /// </summary>
        public static void InitUint()
        {
            if (unitHelper == null)
            {
                unitHelper = new FS.FrameWork.Public.ObjectHelper();
                FS.HISFC.BizLogic.Pharmacy.Constant constantMgr = new FS.HISFC.BizLogic.Pharmacy.Constant();
                unitHelper.ArrayObject = new System.Collections.ArrayList(constantMgr.QueryCompany("2"));
            }
        }
        #endregion

        #region 摆药单
        /// <summary>
        /// 摆药单分类哈希表
        /// </summary>
        public static System.Collections.Hashtable hsDrugBillClass = new System.Collections.Hashtable();

        /// <summary>
        /// 获取摆药单分类名称
        /// </summary>
        /// <param name="drugBillClassNO">摆药单分类编码</param>
        /// <returns>不处理错误</returns>
        public static string GetDrugBillClassName(string drugBillClassNO)
        {
            if (hsDrugBillClass.Contains(drugBillClassNO))
            {
                return (hsDrugBillClass[drugBillClassNO] as FS.HISFC.Models.Pharmacy.DrugBillClass).Name;
            }
            FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore drugStoreMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore();
            FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass = drugStoreMgr.GetDrugBillClass(drugBillClassNO);
            if (drugBillClass == null)
            {
                return "";
            }
            hsDrugBillClass.Add(drugBillClassNO, drugBillClass);
            if (drugBillClass == null)
            {
                return "";
            }
            return drugBillClass.Name;
        }

        /// <summary>
        /// 获取摆药单分类
        /// </summary>
        /// <param name="drugBillClassNO">摆药单分类编码</param>
        /// <returns>不处理错误</returns>
        public static FS.HISFC.Models.Pharmacy.DrugBillClass GetDrugBillClass(string drugBillClassNO)
        {
            if (hsDrugBillClass.Contains(drugBillClassNO))
            {
                return hsDrugBillClass[drugBillClassNO] as FS.HISFC.Models.Pharmacy.DrugBillClass;
            }
            FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore drugStoreMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore();
            FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass = drugStoreMgr.GetDrugBillClass(drugBillClassNO);
            if (drugBillClass == null)
            {
                return new FS.HISFC.Models.Pharmacy.DrugBillClass();
            }
            hsDrugBillClass.Add(drugBillClassNO, drugBillClass);

            return drugBillClass;
        }
        #endregion

        #region 药品发送类型
        /// <summary>
        /// 获取发送类型名称
        /// </summary>
        /// <param name="sendType"></param>
        /// <returns></returns>
        public static string GetDrugApplySendTypeName(int sendType)
        {
            if (sendType == 1)
            {
                return "集中";
            }
            else if (sendType == 2)
            {
                return "临时";
            }
            else if (sendType == 3)
            {
                //applyout中理论上不存在这个值，执行档标记为已经发药
                return "已经发药";
            }
            else if (sendType == 4)
            {
                return "紧急";
            }
            else if (sendType == 5)
            {
                return "隔日";
            }
            else if (sendType == 6)
            {
                return "隔日集中";
            }
            else if (sendType == 7)
            {
                return "隔日临时";
            }
            else if (sendType == 8)
            {
                return "按需发送";
            }
            return "";
        }
        #endregion

        #region 药品库存信息
        /// <summary>
        /// 药品库存信息
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper storeHelper;

        /// <summary>
        /// 获取货位号
        /// </summary>
        /// <param name="itemNO">药品编码</param>
        /// <returns>系统类别PCC草药 PCZ中成药 P西药</returns>
        public static string GetItemPlaceNo(string deptCode,string itemNO)
        {
            FS.HISFC.Models.Pharmacy.Storage item = GetStoreItem(deptCode, itemNO);
            if (item == null)
            {
                return "";
            }
            return item.PlaceNO;
        }

        /// <summary>
        /// 判断项目是否为贵重药品
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="itemNO"></param>
        /// <returns></returns>
        public static bool isValueableItem(string deptCode, string itemNO)
        {
            FS.HISFC.Models.Pharmacy.Storage item = GetStoreItem(deptCode, itemNO);
            if (item == null)
            {
                return false;
            }

            return (item.ManageQuality.ID == "V");
        }

        /// <summary>
        /// 获取药品基本信息
        /// </summary>
        /// <param name="itemNO">药品系统编码</param>
        /// <returns>药品自定义码</returns>
        public static FS.HISFC.Models.Pharmacy.Storage GetStoreItem(string deptCode, string itemNO)
        {
            if (string.IsNullOrEmpty(itemNO))
            {
                return null;
            }
            if (storeHelper == null)
            {
                storeHelper = new FS.FrameWork.Public.ObjectHelper();
                FS.HISFC.BizLogic.Pharmacy.Item itemMgr = new FS.HISFC.BizLogic.Pharmacy.Item();
                ArrayList alTmp = itemMgr.QueryStockinfoList(deptCode);
                ArrayList alNew =  new System.Collections.ArrayList();
                if (alTmp != null)
                {
                    foreach (FS.HISFC.Models.Pharmacy.Storage stmp in alTmp)
                    {
                        if (stmp != null)
                        {
                            stmp.ID = stmp.Item.ID;
                            alNew.Add(stmp);
                        }
                    }
                }
                storeHelper.ArrayObject = alNew;
            }
            if (storeHelper == null || storeHelper.ArrayObject == null)
            {
                return null;
            }
            FS.HISFC.Models.Pharmacy.Storage item = (FS.HISFC.Models.Pharmacy.Storage)storeHelper.GetObjectFromID(itemNO);
            if (item != null)
            {
                return item.Clone();
            }
            return item;
        }
        #endregion

        #region 清除缓存

        /// <summary>
        /// 清除药品基本信息缓存
        /// </summary>
        public static void ClearItemCache()
        {
            if (itemHelper!=null)
            {
                itemHelper = null;
            }
        }

        /// <summary>
        /// 清除院外单位缓存
        /// </summary>
        public static void ClearUnitCache()
        {
            if (unitHelper!=null)
            {
                unitHelper = null;
            }
        }

        /// <summary>
        /// 清除供货公司缓存
        /// </summary>
        public static void ClearCompanyCache()
        {
            if (companyHelper!=null)
            {
                companyHelper = null;
            }
        }

        /// <summary>
        /// 清除生产厂家缓存
        /// </summary>
        public static void ClearProducerCache()
        {
            if (producerHelper != null)
            {
                producerHelper = null;
            }
        }

        /// <summary>
        /// 清除药品基本信息缓存
        /// </summary>
        public static void ClearStoreCache()
        {
            if (storeHelper != null)
            {
                storeHelper = null;
            }
        }
        #endregion
    }
}
