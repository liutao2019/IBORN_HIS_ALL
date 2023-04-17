using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FS.HISFC.Models.Order;

namespace FS.SOC.Local.Order.SubFeeSet
{
    /// <summary>
    /// 附材基础函数
    /// </summary>
    class Function
    {
        /// <summary>
        /// 获取用法的系统类别
        /// </summary>
        /// <param name="usageID"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public static string GetSysType(string usageID, ref string errInfo)
        {
            FS.HISFC.Models.Base.Const con = FS.SOC.HISFC.BizProcess.Cache.Common.GetUsage(usageID);
            if (con != null && !string.IsNullOrEmpty(con.UserCode))
            {
                return con.UserCode;
            }
            else
            {
                errInfo = "获取用法系统类别出错！";
                return usageID;
            }
        }

        /// <summary>
        /// 所有的门诊附材
        /// </summary>
        private static Hashtable hsOutSub = new Hashtable();

        /// <summary>
        /// 所有的住院附材
        /// </summary>
        private static Hashtable hsInSub = new Hashtable();

        /// <summary>
        /// 上次查询附材的时间
        /// 这里没有必要获取系统时间了
        /// </summary>
        private static DateTime getSubDate = DateTime.Now;

        /// <summary>
        /// 获取科室维护的所有附材
        /// </summary>
        /// <param name="type">类型：门诊、住院</param>
        /// <param name="deptCode"></param>
        /// <param name="errInfo"></param>
        /// <param name="hsSub"></param>
        /// <returns></returns>
        public static int GetSubListByDept(FS.HISFC.Models.Base.ServiceTypes type, string deptCode, ref string errInfo, ref Hashtable hsSub)
        {
            hsSub = new Hashtable();
            string area = type == FS.HISFC.Models.Base.ServiceTypes.C ? "0" : "1";

            try
            {
                ArrayList alSub = new ArrayList();
                if (area == "0")
                {
                    //每隔4小时，重新获取所有的附材，即当天维护的附材，如果医生不退出系统，4小时候才会生效
                    if (getSubDate.AddHours(4) < DateTime.Now)
                    {
                        hsOutSub = new Hashtable();
                    }

                    if (hsOutSub.Contains(deptCode))
                    {
                        alSub = hsOutSub[deptCode] as ArrayList;
                    }
                    else
                    {
                        FS.HISFC.BizLogic.Order.SubtblManager subMgr = new FS.HISFC.BizLogic.Order.SubtblManager();

                        alSub = subMgr.GetSubtblInfo(area, "ALL", deptCode);
                        if (alSub == null)
                        {
                            errInfo = subMgr.Err;
                            return -1;
                        }
                        hsOutSub.Add(deptCode, alSub);
                    }
                }
                else
                {
                    //每隔4小时，重新获取所有的附材，即当天维护的附材，如果医生不退出系统，4小时候才会生效
                    if (getSubDate.AddHours(4) < DateTime.Now)
                    {
                        hsOutSub = new Hashtable();
                    }

                    if (hsInSub.Contains(deptCode))
                    {
                        alSub = hsInSub[deptCode] as ArrayList;
                    }
                    else
                    {
                        FS.HISFC.BizLogic.Order.SubtblManager subMgr = new FS.HISFC.BizLogic.Order.SubtblManager();

                        alSub = subMgr.GetSubtblInfo(area, "ALL", deptCode);
                        if (alSub == null)
                        {
                            errInfo = subMgr.Err;
                            return -1;
                        }
                        hsInSub.Add(deptCode, alSub);
                    }
                }

                foreach (OrderSubtblNew subObj in alSub)
                {
                    if (!hsSub.Contains(subObj.TypeCode))
                    {
                        ArrayList alTemp = new ArrayList();
                        alTemp.Add(subObj);
                        hsSub.Add(subObj.TypeCode, alTemp);
                    }
                    else
                    {
                        ((ArrayList)hsSub[subObj.TypeCode]).Add(subObj);
                    }
                }
            }
            catch (Exception ex)
            {
                errInfo = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 根据执行档，获取实际执行的次数
        /// </summary>
        /// <param name="frequencyID"></param>
        /// <returns></returns>
        public static decimal GetRealExecCount(ArrayList alExecOrders, FS.HISFC.Models.Order.Inpatient.Order inOrder)
        {
            int execCount = 0;
            if (alExecOrders != null)
            {
                foreach (FS.HISFC.Models.Order.ExecOrder execOrder in alExecOrders)
                {
                    if (execOrder.Order.ID == inOrder.ID && execOrder.IsExec)
                    {
                        execCount += 1;
                    }
                }
            }
            return execCount;
        }

        /// <summary>
        /// 获取频次代表的每天次数
        /// </summary>
        /// <param name="frequencyID"></param>
        /// <returns></returns>
        public static decimal GetFrequencyCountByOneDay(string frequencyID)
        {
            if (string.IsNullOrEmpty(frequencyID))
            {
                return 1;
            }
            string id = frequencyID.ToLower().Replace(".", "");
            if (id == "qhh")//每0.5小时一次
            {
                return 48;
            }
            else if (id == "qh")//每1小时一次
            {
                return 24;
            }
            else if (id == "q4h")//每4小时一次
            {
                return 6;
            }
            else if (id == "q6h")//每6小时一次
            {
                return 4;
            }
            else if (id == "q8h")//每8小时一次
            {
                return 3;
            }
            else if (id == "q12h")//每12小时一次
            {
                return 2;
            }
            else if (id == "qd")//每天一次
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
            else if (id == "fid")//每天五次
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
                FS.HISFC.BizLogic.Manager.Frequency frequencyManagement = new FS.HISFC.BizLogic.Manager.Frequency();
                ArrayList alFrequency = frequencyManagement.GetBySysClassAndID("ROOT", "ALL", frequencyID);
                if (alFrequency != null && alFrequency.Count > 0)
                {
                    FS.HISFC.Models.Order.Frequency obj = alFrequency[0] as FS.HISFC.Models.Order.Frequency;
                    string[] str = obj.Time.Split('-');

                    //返回一天几次
                    //return str.Length;

                    //tid为3次 Q3D为1/3次
                    try
                    {
                        return str.Length / FS.FrameWork.Function.NConvert.ToDecimal(obj.Days[0]);
                    }
                    catch
                    {
                        return str.Length;
                    }
                }
                return 100;
            }
        }

        /// <summary>
        /// 附材删除
        /// </summary>
        /// <param name="alOrder"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public static int DeleteSubjobFeeForOutPatient(ArrayList alOrder, ref string errInfo)
        {
            //对保存过产生的附材删除,如果医生站保存函数删除附材的话，在此就没必要了
            //删除附材用，记录已删除的处方，避免重复删除
            Hashtable hsDelRecipe = new Hashtable();
            FS.HISFC.BizLogic.Fee.Outpatient outpatientFeeMgr = new FS.HISFC.BizLogic.Fee.Outpatient();
            FS.HISFC.BizLogic.Order.OutPatient.Order outOrderMgr = new FS.HISFC.BizLogic.Order.OutPatient.Order();

            foreach (FS.HISFC.Models.Order.OutPatient.Order order in alOrder)
            {
                if (order.IsSubtbl)
                {
                    if (outOrderMgr.DeleteOrder(order.SeeNO, FS.FrameWork.Function.NConvert.ToInt32(order.ID)) == -1)
                    {
                        errInfo = outOrderMgr.Err;
                        return -1;
                    }
                }

                //删除项目所带的附材
                //整方的辅材都删除
                //if (!string.IsNullOrEmpty(order.ReciptNO) && !hsDelRecipe.ContainsKey(order.ReciptNO))
                //{
                //    hsDelRecipe.Add(order.ReciptNO, null);
                //    if (outpatientFeeMgr.DeleteSubFeeItem(order.ReciptNO) < 1)
                //    {
                //        errInfo = outpatientFeeMgr.Err;
                //        return -1;
                //    }

                //}
            }

            //按照收费序列号重新删除一遍
            foreach (FS.HISFC.Models.Order.OutPatient.Order order in alOrder)
            {
                try
                {
                    ArrayList alSubAndOrder = outpatientFeeMgr.QueryValidFeeDetailbyClinicCodeAndRecipeSeq(order.Patient.ID, order.ReciptSequence);
                    if (alSubAndOrder == null)
                    {
                        errInfo = outpatientFeeMgr.Err;
                        return -1;
                    }

                    int rev = -1;
                    for (int j = 0; j < alSubAndOrder.Count; j++)
                    {
                        FS.HISFC.Models.Fee.Outpatient.FeeItemList item = alSubAndOrder[j] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                        if (item.Item.IsMaterial)
                        {
                            rev = outpatientFeeMgr.DeleteFeeItemListByRecipeNO(item.RecipeNO, item.SequenceNO.ToString());

                            if (rev == 0)
                            {
                                errInfo = "项目【" + item.Name + "】对应的附材已经收费，不允许删除！\r\n请退出界面重试！";
                                return -1;
                            }
                            else if (rev < 0)
                            {
                                errInfo = outpatientFeeMgr.Err;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    errInfo = ex.Message;
                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        /// 药品第二基本计量帮助类
        /// </summary>
        private static FS.FrameWork.Public.ObjectHelper phaDoseOnceHelper = null;

        /// <summary>
        /// 药品第二基本计量帮助类
        /// </summary>
        /// <returns></returns>
        public static FS.FrameWork.Public.ObjectHelper GetPhaDoseHelper()
        {
            if (phaDoseOnceHelper == null)
            {
                FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
                phaDoseOnceHelper = new FS.FrameWork.Public.ObjectHelper();
                phaDoseOnceHelper.ArrayObject = managerIntegrate.GetConstantList("PharmacyDoseOnce");
            }
            return phaDoseOnceHelper;
        }

        /// <summary>
        /// 药品性质帮助类
        /// </summary>
        private static FS.FrameWork.Public.ObjectHelper drugQuaulityHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 是否大输液
        /// </summary>
        /// <param name="qualityID"></param>
        /// <returns></returns>
        private static bool CheckIsSubPha(string qualityID)
        {
            if (drugQuaulityHelper == null || drugQuaulityHelper.ArrayObject.Count == 0)
            {
                FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
                //取药品剂型
                ArrayList alDrugQuaulity = managerIntegrate.GetConstantList("DRUGQUALITY");
                if (alDrugQuaulity == null)
                {
                    return true;
                }
                drugQuaulityHelper.ArrayObject = alDrugQuaulity;
            }

            if (((FS.HISFC.Models.Base.Const)drugQuaulityHelper.GetObjectFromID(qualityID)).UserCode.Trim() == "T"
                || (drugQuaulityHelper.GetObjectFromID(qualityID)).Memo.Contains("大输液"))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获得一组中用来计算附材的药品
        /// 是非大输液、最大每次量的药品
        /// </summary>
        /// <param name="alOrders"></param>
        /// <returns></returns>
        public static FS.HISFC.Models.Order.Order GetOrderForSub(ArrayList alCombOrders)
        {
            FS.HISFC.Models.Order.Order subOrder = null;
            decimal maxDoseOnce = 0;

            decimal tempDoseOnce = 0;

            foreach (FS.HISFC.Models.Order.Order obj in alCombOrders)
            {
                if (obj.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    continue;
                }


                if (obj.Item.ID == "999")
                {
                    //doseTemp = obj.DoseOnce;
                    if (obj.DoseUnit.Trim() == "ml")
                    {
                        if (obj.DoseOnce > maxDoseOnce)
                        {
                            maxDoseOnce = obj.DoseOnce;
                            subOrder = obj;
                        }
                    }

                }
                else
                {
                    FS.HISFC.Models.Pharmacy.Item phaItem = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(obj.Item.ID);//获得最新项目信

                    //decimal doseTemp = 0;
                    if (phaItem != null)
                    {
                        if (CheckIsSubPha(phaItem.Quality.ID))
                        {
                            continue;
                        }

                        if (obj.DoseUnit == phaItem.MinUnit)
                        {
                            obj.DoseUnit = phaItem.DoseUnit;
                            obj.DoseOnce = obj.DoseOnce * phaItem.BaseDose;
                        }


                        if (Function.GetPhaDoseHelper().GetObjectFromID(phaItem.ID) != null)
                        {
                            tempDoseOnce = FS.FrameWork.Function.NConvert.ToDecimal(((FS.HISFC.Models.Base.Const)Function.GetPhaDoseHelper().GetObjectFromID(phaItem.ID)).Name) * obj.DoseOnce / phaItem.BaseDose;

                        }
                        else
                        {
                            //只处理毫升的基本计量
                            if (obj.DoseUnit == null)
                            {
                                obj.DoseUnit = phaItem.DoseUnit;
                            }

                            if (obj.DoseUnit.Trim() == "ml")
                            {
                                tempDoseOnce = obj.DoseOnce;
                            }
                        }
                        if (tempDoseOnce > maxDoseOnce)
                        {
                            maxDoseOnce = tempDoseOnce;
                            subOrder = obj;
                        }

                    }
                }
            }


            if (subOrder != null)
            {
                return subOrder.Clone();
            }
            return null;
        }
    }

    /// <summary>
    /// 门诊组合列表根据排序号排序
    /// </summary>
    public class OutCombOrderCompare : IComparer
    {
        #region IComparer 成员

        public int Compare(object x, object y)
        {
            try
            {
                ArrayList al1 = x as ArrayList;
                ArrayList al2 = y as ArrayList;

                FS.HISFC.Models.Order.OutPatient.Order outOrder1 = al1[0] as FS.HISFC.Models.Order.OutPatient.Order;
                FS.HISFC.Models.Order.OutPatient.Order outOrder2 = al2[0] as FS.HISFC.Models.Order.OutPatient.Order;

                //decimal count1 = outOrder1.HerbalQty * Function.GetFrequencyCountByOneDay(outOrder1.Frequency.ID);
                //decimal count2 = outOrder2.HerbalQty * Function.GetFrequencyCountByOneDay(outOrder2.Frequency.ID);

                decimal count1 = outOrder1.SortID;
                decimal count2 = outOrder2.SortID;

                if (outOrder1.HerbalQty > outOrder2.HerbalQty)
                {
                    return -1;
                }
                else if (outOrder1.HerbalQty == outOrder2.HerbalQty)
                {
                    if (count1 > count2)
                    {
                        return 1;
                    }
                    else if (count2 == count1)
                    {
                        if (outOrder1.SubCombNO > outOrder2.SubCombNO)
                        {
                            return 1;
                        }
                        else if (outOrder2.SubCombNO == outOrder1.SubCombNO)
                        {
                            return 0;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    return 1;
                }
            }
            catch
            {
                return 0;
            }
        }

        #endregion
    }
}
