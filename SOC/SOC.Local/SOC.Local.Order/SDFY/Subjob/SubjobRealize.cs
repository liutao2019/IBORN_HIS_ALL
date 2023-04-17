using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Neusoft.HISFC.BizLogic.Order;
using Neusoft.HISFC.Models.Order;

namespace SOC.Local.Order.SDFY.Subjob
{
    /// <summary>
    /// 顺德妇幼附材算法
    /// </summary>
    class SubjobRealize : Neusoft.HISFC.BizProcess.Interface.Order.IDealSubjob
    {
        /// <summary>
        /// 附材收取规则
        /// </summary>
        private Hashtable hsSubRules = null;

        private Hashtable hsOrder = null;

        /// <summary>
        /// 是否弹出选择
        /// </summary>
        private bool isPopForChose = false;

        /// <summary>
        /// 是否弹出选择
        /// </summary>
        public bool IsPopForChose
        {
            get
            {
                return isPopForChose;
            }
            set
            {
                isPopForChose = value;
            }
        }

        #region IDealSubjob 成员

        public int DealSubjob(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo, System.Collections.ArrayList alOrders, ref System.Collections.ArrayList alSubOrders, ref string errInfo)
        {
            hsOrder = new Hashtable();
            foreach (Neusoft.HISFC.Models.Order.Order orderObj in alOrders)
            {
                if (!hsOrder.Contains(orderObj.Combo.ID))
                {
                    ArrayList al = new ArrayList();
                    al.Add(orderObj);
                    hsOrder.Add(orderObj.Combo.ID, al);
                }
                else
                {
                    ((ArrayList)hsOrder[orderObj.Combo.ID]).Add(orderObj);
                }
            }

            return 1;
        }

        private int DealSubInfo(ArrayList alCombOrders, ref System.Collections.ArrayList alSubOrders, ref string errInfo)
        {
            Neusoft.HISFC.Models.Order.Order order=alCombOrders[0] as Neusoft.HISFC.Models.Order.Order;

            if (hsSubRules == null || hsSubRules.Count == 0)
            {
                hsSubRules = new Hashtable();
                SubtblManager subMgr = new SubtblManager();
                ArrayList alSub = subMgr.GetAllSubInfo();
                if (alSub == null)
                {
                    errInfo = subMgr.Err;
                    return -1;
                }

                try
                {
                    foreach (OrderSubtblNew subObj in alSub)
                    {
                        if (!hsSubRules.Contains(subObj.TypeCode))
                        {
                            ArrayList alTemp = new ArrayList();
                            alTemp.Add(subObj);
                            hsSubRules.Add(subObj.TypeCode, alTemp);
                        }
                        else
                        {
                            ((ArrayList)hsSubRules[subObj.TypeCode]).Add(subObj);
                        }
                    }
                }
                catch (Exception ex)
                {
                    errInfo = ex.Message;
                    return -1;
                }
            }

            /**
             * 适用范围：0 门诊；1 住院；2 全部
             * 区分科室：全院统一附材'ROOT'
             * 组范围：0 每组收取 
             *              1 第一组收取（晚上后台收取）
             *              2 第二组起加收（晚上后台收取）
             * 
             * 收取规则：
             *    0 固定数量
             *    1 最大院注
             *    2 组内品种数
             *    3 最大医嘱数量
             *    4 频次数
             * */

            ArrayList alSubInfo = hsSubRules[order.Usage.ID] as ArrayList;

            //带出项目
            Neusoft.HISFC.Models.Base.Item obj = null;
            if (alSubInfo != null)
            {
                alSubOrders = new ArrayList();
                foreach (OrderSubtblNew subObj in alSubInfo)
                {
                    if (order.ReciptDept.ID == subObj.Dept_code || subObj.Dept_code == "ROOT")
                    {
                        obj = new Neusoft.HISFC.Models.Base.Item();
                        obj.ID = subObj.Item.ID;

                        //0 每组收取
                        if (subObj.CombArea == "0")
                        {
                            switch (subObj.FeeRule)
                            {
                                //0 固定数量
                                case "0":
                                    obj.Qty = subObj.Qty;
                                    break;
                                //1 最大院注
                                case "1":
                                    obj.Qty = order.InjectCount;
                                    break;
                                //2 组内品种数
                                case "2":
                                    obj.Qty = GetFeeCountByCombOrders(alCombOrders);
                                    break;
                                //3 最大医嘱数量
                                case "3":
                                    obj.Qty=order.Qty;
                                    break;
                                //4 频次数
                                case "4":
                                    obj.Qty=GetFrequencyCount(order.Frequency.ID);
                                    break;
                            }
                        }
                        //1 第一组收取
                        else if (subObj.CombArea == "1")
                        {
                            switch (subObj.FeeRule)
                            {
                                //0 固定数量
                                case "0":
                                    obj.Qty = subObj.Qty;
                                    break;
                                //1 最大院注
                                case "1":
                                    obj.Qty = order.InjectCount;
                                    break;
                                //2 组内品种数
                                case "2":
                                    obj.Qty = GetFeeCountByCombOrders(alCombOrders);
                                    break;
                                //3 最大医嘱数量
                                case "3":
                                    obj.Qty = order.Qty;
                                    break;
                                //4 频次数
                                case "4":
                                    obj.Qty =SOC.Local.Order.SubFeeSet.Function.GetFrequencyCountByOneDay(order.Frequency.ID);
                                    break;
                            }
                        }
                        //第二组起加收
                        else if (subObj.CombArea == "2")
                        {
                            switch (subObj.FeeRule)
                            {
                                //0 固定数量
                                case "0":
                                    obj.Qty = subObj.Qty;
                                    break;
                                //1 最大院注
                                case "1":
                                    obj.Qty = order.InjectCount;
                                    break;
                                //2 组内品种数
                                case "2":
                                    obj.Qty = GetFeeCountByCombOrders(alCombOrders);
                                    break;
                                //3 最大医嘱数量
                                case "3":
                                    obj.Qty = order.Qty;
                                    break;
                                //4 频次数
                                case "4":
                                    obj.Qty =SOC.Local.Order.SubFeeSet.Function.GetFrequencyCountByOneDay(order.Frequency.ID);
                                    break;
                            }
                        }

                        alSubOrders.Add(subObj);
                    }
                }
            }

            return 1;
        }

        private int GetFeeCountByCombOrders(ArrayList alOneCombo)
        {
            int comboQty = 0;
            foreach (Neusoft.HISFC.Models.Order.OutPatient.Order order in alOneCombo)
            {
                if (!this.CheckIsMainDrug(order))
                {
                    continue;
                }
                //考虑到每次用量*频次*天数!=总量的情况
                int days = this.GetRealDays(order);
                comboQty += days;
            }
            return comboQty;
        }

        /// <summary>
        /// 获取实际用药天数，总量并不一定和每次用量*频次*天数相等
        /// 实际天数=总量/(每次用量*频次)向上取整
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private int GetRealDays(Neusoft.HISFC.Models.Order.Order order)
        {
            int days = 0;

            days = Neusoft.FrameWork.Function.NConvert.ToInt32(order.HerbalQty);
            return days;

            decimal count = this.GetFrequencyCount(order.Frequency.ID);
            decimal doseOnce = order.DoseOnce;
            if (order.DoseUnit != order.Unit)
            {
                Neusoft.HISFC.Models.Pharmacy.Item item = Neusoft.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID);                
                if (item != null)
                {
                    if (order.Unit == item.MinUnit)//总量单位是最小单位
                    {
                        if (order.DoseUnit == item.DoseUnit)//每次量是剂量单位
                        {
                            doseOnce = doseOnce / item.BaseDose;
                        }
                        else
                        {
                            //每次量是最小单位单位
                        }
                    }
                    else if (order.Unit == item.PackUnit)//总量单位是包装单位
                    {
                        if (order.DoseUnit == item.DoseUnit)//每次量是剂量单位
                        {
                            doseOnce = doseOnce / item.BaseDose / item.PackQty;
                        }
                        else if (order.DoseUnit == item.MinUnit)//每次量是最小单位单位
                        {
                            doseOnce = doseOnce / item.PackQty;
                        }
                    }
                }
            }

            days = (int)(order.Qty / (doseOnce * count));
            if (days < order.Qty / (doseOnce * count))
            {
                days = days + 1;
            }

            return days;
        }

        /// <summary>
        /// 检测药品是否为主药(溶液、非药品外的药品)
        /// </summary>
        /// <param name="drugNO"></param>
        /// <returns></returns>
        private bool CheckIsMainDrug(Neusoft.HISFC.Models.Order.OutPatient.Order order)
        {
            if (order.Item.ItemType != Neusoft.HISFC.Models.Base.EnumItemType.Drug)
            {
                return false;
            }
            Neusoft.HISFC.Models.Pharmacy.Item item = Neusoft.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID);
            //好像是社保方面的，妇幼商定通用名的自定义码为3 按照大输液处理，不再收取组合费和注射费
            if (item != null && item.NameCollection.RegularSpell.UserCode == "3")
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取频次代表的每天次数
        /// </summary>
        /// <param name="frequencyID"></param>
        /// <returns></returns>
        private int GetFrequencyCount(string frequencyID)
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
                Neusoft.HISFC.BizLogic.Manager.Frequency frequencyManagement = new Neusoft.HISFC.BizLogic.Manager.Frequency();
                ArrayList alFrequency = frequencyManagement.GetBySysClassAndID("ROOT", "ALL", frequencyID);
                if (alFrequency != null && alFrequency.Count > 0)
                {
                    Neusoft.HISFC.Models.Order.Frequency obj = alFrequency[0] as Neusoft.HISFC.Models.Order.Frequency;
                    string[] str = obj.Time.Split('-');
                    return str.Length;
                }
                return 100;
            }
        }

        public int DealSubjob(Neusoft.HISFC.Models.Registration.Register r, System.Collections.ArrayList alOrders, ref System.Collections.ArrayList alSubOrders, ref string errText)
        {
            return 1;
        }

        public int DealSubjob(Neusoft.HISFC.Models.Registration.Register r, System.Collections.ArrayList alFee, ref string errText)
        {
            return 1;
        }

        #endregion

        #region IDealSubjob 成员

        public int DealSubjob(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo, bool isRealTime, Neusoft.HISFC.Models.Order.Inpatient.Order order, ArrayList alOrders, ref ArrayList alSubOrders, ref string errInfo)
        {
            throw new NotImplementedException();
        }

        public DateTime FeeDate
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
