using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Neusoft.HISFC.Models.Fee.Outpatient;
using Neusoft.HISFC.Models.Order.OutPatient;

namespace InterfaceInstanceDefault.IDealSubjob
{
    /// <summary>
    /// 医生站处理辅材
    /// </summary>
    [Obsolete("作废", true)]
    public class DealSubjobDefault : Neusoft.HISFC.BizProcess.Interface.Order.IDealSubjob
    {
        #region 变量

        Neusoft.HISFC.BizProcess.Integrate.Manager managerIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Manager();

        Neusoft.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Pharmacy();

        Neusoft.HISFC.BizProcess.Integrate.Fee feeManagement = new Neusoft.HISFC.BizProcess.Integrate.Fee();

        Neusoft.HISFC.BizProcess.Integrate.Order orderIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Order();

        Neusoft.HISFC.BizLogic.Fee.PactUnitItemRate pactItemRate = new Neusoft.HISFC.BizLogic.Fee.PactUnitItemRate();

        Neusoft.HISFC.BizLogic.Order.OutPatient.Order myOutOrder = new Neusoft.HISFC.BizLogic.Order.OutPatient.Order();

        static Hashtable hsUsageAndSub = new Hashtable();

        #endregion

        #region IDealSubjob 成员

        /// <summary>
        /// 添加辅材
        /// </summary>
        /// <param name="r"></param>
        /// <param name="alOrders"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        public int DealSubjob(Neusoft.HISFC.Models.Registration.Register r, System.Collections.ArrayList alOrders, ref string errText)
        {
            return 1;
        }

        #endregion

        #region IDealSubjob 成员

        /// <summary>
        /// 添加辅材
        /// </summary>
        /// <param name="r"></param>
        /// <param name="alOrders"></param>
        /// <param name="alSubOrders"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        public int DealSubjob(Neusoft.HISFC.Models.Registration.Register r, ArrayList alOrders, Neusoft.HISFC.Models.Order.OutPatient.Order outOrder, ref ArrayList alSubOrders, ref string errText)
        {
            alSubOrders = new ArrayList();
            //南庄特殊判断，此处不带辅材
            return 1;

            ArrayList alDealOrders = new ArrayList();

            foreach (Neusoft.HISFC.Models.Order.OutPatient.Order order in alOrders)
            {
                alDealOrders.Add(order.Clone());
            }

            if (r == null || r.ID.Length <= 0)
            {
                errText = "没有患者信息";
                return -1;
            }

            if (alDealOrders == null || alDealOrders.Count <= 0)
            {
                errText = "没有医嘱信息";
                return -1;
            }

            //这里重复查询数据了，外面调用的时候已经查询了，只是没有传进来...
            if (hsUsageAndSub.Count <= 0)
            {
                hsUsageAndSub = myOutOrder.GetUsageAndSub();
            }

            Hashtable hsDelCombo = new Hashtable();
            Hashtable hsAddCombo = new Hashtable();

            foreach (Neusoft.HISFC.Models.Order.OutPatient.Order order in alDealOrders)
            {
                #region 删除项目所带的附材

                if (!hsDelCombo.ContainsKey(order.Combo.ID))
                {
                    hsDelCombo.Add(order.Combo.ID, order);

                    ArrayList alSubAndOrder = feeManagement.QueryFeeDetailbyComoNOAndClinicCode(order.Combo.ID, r.ID);

                    for (int j = 0; j < alSubAndOrder.Count; j++)
                    {
                        Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList item = alSubAndOrder[j] as Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList;
                        if (item.Item.IsMaterial)
                        {
                            if (feeManagement.DeleteFeeItemListByRecipeNO(item.RecipeNO, item.SequenceNO.ToString()) < 0)
                            {
                                errText = feeManagement.Err;
                                return -1;
                            }
                        }
                    }
                }
                #endregion
            }

            #region 用法分组

            ArrayList sortList = new ArrayList();

            while (alDealOrders.Count > 0)
            {
                ArrayList sameNotes = new ArrayList();
                Order compareItem = alDealOrders[0] as Order;
                foreach (Order order in alDealOrders)
                {
                    if (order.Usage.ID == compareItem.Usage.ID)
                    {
                        sameNotes.Add(order);
                    }
                }
                sortList.Add(sameNotes);
                foreach (Order order in sameNotes)
                {
                    alDealOrders.Remove(order);
                }
            }
            #endregion

            foreach (ArrayList temp in sortList)
            {
                ArrayList counts = new ArrayList();
                ArrayList countUnits = new ArrayList();

                while (temp.Count > 0)
                {
                    countUnits = new ArrayList();
                    Order compareItem = temp[0] as Order;
                    foreach (Order order in temp)
                    {
                        if (order.Combo.ID == compareItem.Combo.ID)
                        {
                            countUnits.Add(order);
                        }
                    }

                    counts.Add(countUnits);

                    foreach (Order f in countUnits)
                    {
                        temp.Remove(f);
                    }
                }

                foreach (ArrayList tempCounts in counts)
                {
                    //第一组
                    if (tempCounts == counts[0])
                    {
                        Order order = tempCounts[0] as Order;
                        if (order.InjectCount == 0)
                        {
                            continue;
                        }

                        #region 添加皮试费用

                        if (order.HypoTest == Neusoft.HISFC.Models.Order.EnumHypoTest.NeedHypoTest) 
                        {
                            string hypoFeeCode = managerIntegrate.QueryControlerInfo("200025");

                            if (hypoFeeCode != null && hypoFeeCode != "" && hypoFeeCode != "-1" && hypoFeeCode.Length > 0)
                            {
                                //插入划价表时增加处方内流水号；
                                Neusoft.HISFC.Models.Fee.Item.Undrug item = null;
                                try
                                {
                                    item = feeManagement.GetItem(hypoFeeCode);//获得最新项目信息
                                    if (item == null)
                                    {
                                        errText = "查找项目失败:" + feeManagement.Err;
                                        return -1;
                                    }
                                    if (item.UnitFlag == "1")
                                    {
                                        item.Price = feeManagement.GetUndrugCombPrice(item.ID);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    errText = ex.Message;
                                    return -1;
                                }
                                if (item != null)
                                {
                                    item.Qty = 1;
                                }
                                Neusoft.HISFC.Models.Order.OutPatient.Order newOrder = order.Clone();
                                newOrder.ReciptNO = "";
                                newOrder.SequenceNO = -1;
                                if (item != null)
                                {
                                    newOrder.Item = item.Clone();
                                }

                                newOrder.Qty = 1;
                                if (item != null)
                                {
                                    newOrder.Unit = item.PriceUnit;
                                }
                                newOrder.Combo = order.Combo;//组合号
                                newOrder.ID = orderIntegrate.GetNewOrderID(); //医嘱流水号
                                if (newOrder.ID == "")
                                {
                                    errText = "获得医嘱流水号出错！";
                                    return -1;
                                }
                                //newOrder.Item.IsPharmacy = false;
                                newOrder.Item.ItemType = Neusoft.HISFC.Models.Base.EnumItemType.UnDrug;
                                newOrder.InjectCount = order.InjectCount;
                                newOrder.IsEmergency = order.IsEmergency;
                                newOrder.IsSubtbl = true;
                                newOrder.Usage = new Neusoft.FrameWork.Models.NeuObject();
                                newOrder.SequenceNO = -1;
                                if (newOrder.ExeDept.ID == "")//执行科室默认
                                    newOrder.ExeDept = order.ReciptDept;

                                alSubOrders.Add(newOrder);
                            }
                        }
                        #endregion

                        #region 添加附材

                        if (!hsUsageAndSub.Contains(order.Usage.ID))
                        {
                            continue;
                        }
                        ArrayList alSubtbls = (ArrayList)hsUsageAndSub[order.Usage.ID];
                        if (alSubtbls == null)
                        {
                            errText = "获得院注次数出错！\n" + feeManagement.Err;
                            return -1;
                        }

                        /*
                         * 数量收取规则
                         * 
                        "0"=="第一组*院注次数",
                        "1"=="第二组起*收取一次",
                        "2"=="每组*收取一次",
                        "3"=="每组*院注次数",
                        "4"=="每组*组内数量",
                        "5"=="每组*医嘱数量"
                         */
                        for (int m = 0; m < alSubtbls.Count; m++)
                        {
                            //第二组的过滤
                            if (((Neusoft.HISFC.Models.Order.OrderSubtbl)alSubtbls[m]).QtyRule.ToString() == "1")
                            {
                                continue;
                            }

                            //rep_no++;//插入划价表时增加处方内流水号；
                            Neusoft.HISFC.Models.Fee.Item.Undrug item = null;
                            try
                            {
                                if (((Neusoft.HISFC.Models.Order.OrderSubtbl)alSubtbls[m]).ID.Substring(0, 1) == "F")
                                {
                                    item = feeManagement.GetItem(((Neusoft.HISFC.Models.Order.OrderSubtbl)alSubtbls[m]).ID);//获得最新项目信息
                                    if (item.UnitFlag == "1")
                                    {
                                        item.Price = feeManagement.GetUndrugCombPrice(item.ID);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                errText = ex.Message;
                                return -1;
                            }
                            if (item != null)
                            {
                                if (((Neusoft.HISFC.Models.Order.OrderSubtbl)alSubtbls[m]).QtyRule.ToString() == "0")
                                {
                                    item.Qty = order.InjectCount;
                                }
                                else if (((Neusoft.HISFC.Models.Order.OrderSubtbl)alSubtbls[m]).QtyRule.ToString() == "2")
                                {
                                    item.Qty = 1;
                                }
                                else if (((Neusoft.HISFC.Models.Order.OrderSubtbl)alSubtbls[m]).QtyRule.ToString() == "3")
                                {
                                    item.Qty = order.InjectCount;
                                }
                                else if (((Neusoft.HISFC.Models.Order.OrderSubtbl)alSubtbls[m]).QtyRule.ToString() == "4")
                                {
                                    item.Qty = tempCounts.Count;
                                }
                                else if (((Neusoft.HISFC.Models.Order.OrderSubtbl)alSubtbls[m]).QtyRule.ToString() == "5")
                                {
                                    item.Qty = order.Qty;
                                }
                                else
                                {
                                    item.Qty = order.Qty;
                                }
                            }
                            Neusoft.HISFC.Models.Order.OutPatient.Order newOrder = order.Clone();
                            newOrder.ReciptNO = "";
                            newOrder.SequenceNO = -1;
                            if (item != null)
                            {
                                newOrder.Item = item.Clone();
                            }

                            if (((Neusoft.HISFC.Models.Order.OrderSubtbl)alSubtbls[m]).QtyRule.ToString() == "0")
                            {
                                newOrder.Qty = order.InjectCount;
                            }
                            else if (((Neusoft.HISFC.Models.Order.OrderSubtbl)alSubtbls[m]).QtyRule.ToString() == "2")
                            {
                                newOrder.Qty = 1;
                            }
                            else if (((Neusoft.HISFC.Models.Order.OrderSubtbl)alSubtbls[m]).QtyRule.ToString() == "3")
                            {
                                newOrder.Qty = order.InjectCount;
                            }
                            else if (((Neusoft.HISFC.Models.Order.OrderSubtbl)alSubtbls[m]).QtyRule.ToString() == "4")
                            {
                                newOrder.Qty = tempCounts.Count;
                            }
                            else if (((Neusoft.HISFC.Models.Order.OrderSubtbl)alSubtbls[m]).QtyRule.ToString() == "5")
                            {
                                newOrder.Qty = order.Qty;
                            }
                            else
                            {
                                newOrder.Qty = order.Qty;
                            }

                            if (item.Qty <= 0 || newOrder.Qty <= 0)
                            {
                                errText = "辅材带出项目" + item.Name + "数量为0！";
                                return -1;
                            }

                            if (item != null)
                            {
                                newOrder.Unit = item.PriceUnit;
                            }
                            newOrder.Combo = order.Combo;//组合号
                            newOrder.ID = this.orderIntegrate.GetNewOrderID();//医嘱流水号
                            if (newOrder.ID == "")
                            {
                                errText = "获得医嘱流水号出错！";
                                return -1;
                            }

                            newOrder.Item.ItemType = Neusoft.HISFC.Models.Base.EnumItemType.UnDrug;

                            newOrder.IsEmergency = order.IsEmergency;
                            newOrder.IsSubtbl = true;
                            newOrder.Usage = new Neusoft.FrameWork.Models.NeuObject();
                            newOrder.SequenceNO = -1;
                            if (newOrder.ExeDept.ID == "")//执行科室默认
                            {
                                newOrder.ExeDept = (this.myOutOrder.Operator as Neusoft.HISFC.Models.Base.Employee).Dept.Clone();
                            }

                            alSubOrders.Add(newOrder);
                        }
                        #endregion
                    }
                    else
                    {
                        Order order = tempCounts[0] as Order;

                        #region 添加皮试费用

                        if (order.HypoTest == Neusoft.HISFC.Models.Order.EnumHypoTest.NeedHypoTest) 
                        {
                            string hypoFeeCode = managerIntegrate.QueryControlerInfo("200025");

                            if (hypoFeeCode != null && hypoFeeCode != "" && hypoFeeCode != "-1" && hypoFeeCode.Length > 0)
                            {
                                //插入划价表时增加处方内流水号；
                                Neusoft.HISFC.Models.Fee.Item.Undrug item = null;
                                try
                                {
                                    item = feeManagement.GetItem(hypoFeeCode);//获得最新项目信息
                                    if (item == null)
                                    {
                                        errText = "查找项目失败:" + feeManagement.Err;
                                        return -1;
                                    }
                                    if (item.UnitFlag == "1")
                                    {
                                        item.Price = feeManagement.GetUndrugCombPrice(item.ID);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    errText = ex.Message;
                                    return -1;
                                }
                                if (item != null)
                                {
                                    item.Qty = 1;
                                }
                                Neusoft.HISFC.Models.Order.OutPatient.Order newOrder = order.Clone();
                                newOrder.ReciptNO = "";
                                newOrder.SequenceNO = -1;
                                if (item != null)
                                {
                                    newOrder.Item = item.Clone();
                                }

                                newOrder.Qty = 1;
                                if (item != null)
                                {
                                    newOrder.Unit = item.PriceUnit;
                                }
                                newOrder.Combo = order.Combo;//组合号
                                newOrder.ID = orderIntegrate.GetNewOrderID(); //医嘱流水号
                                if (newOrder.ID == "")
                                {
                                    errText = "获得医嘱流水号出错！";
                                    return -1;
                                }
                                //newOrder.Item.IsPharmacy = false;
                                newOrder.Item.ItemType = Neusoft.HISFC.Models.Base.EnumItemType.UnDrug;
                                newOrder.InjectCount = order.InjectCount;
                                newOrder.IsEmergency = order.IsEmergency;
                                newOrder.IsSubtbl = true;
                                newOrder.Usage = new Neusoft.FrameWork.Models.NeuObject();
                                newOrder.SequenceNO = -1;
                                if (newOrder.ExeDept.ID == "")//执行科室默认
                                    newOrder.ExeDept = order.ReciptDept;

                                alSubOrders.Add(newOrder);
                            }
                        }
                        #endregion

                        #region 添加附材

                        if (!hsUsageAndSub.Contains(order.Usage.ID))
                        {
                            continue;
                        }
                        ArrayList alSubtbls = (ArrayList)hsUsageAndSub[order.Usage.ID];
                        if (alSubtbls == null)
                        {
                            errText = "获得院注次数出错！\n" + feeManagement.Err;
                            return -1;
                        }

                        /*
                         * 数量收取规则
                         * 
                        "0"=="第一组*院注次数",
                        "1"=="第二组起*收取一次",
                        "2"=="每组*收取一次",
                        "3"=="每组*院注次数",
                        "4"=="每组*组内数量",
                        "5"=="每组*医嘱数量"
                         */
                        for (int m = 0; m < alSubtbls.Count; m++)
                        {
                            //第二组的过滤
                            if (((Neusoft.HISFC.Models.Order.OrderSubtbl)alSubtbls[m]).QtyRule.ToString() == "0")
                            {
                                continue;
                            }

                            //rep_no++;//插入划价表时增加处方内流水号；
                            Neusoft.HISFC.Models.Fee.Item.Undrug item = null;
                            try
                            {
                                if (((Neusoft.HISFC.Models.Order.OrderSubtbl)alSubtbls[m]).ID.Substring(0, 1) == "F")
                                {
                                    item = feeManagement.GetItem(((Neusoft.HISFC.Models.Order.OrderSubtbl)alSubtbls[m]).ID);//获得最新项目信息
                                    if (item.UnitFlag == "1")
                                    {
                                        item.Price = feeManagement.GetUndrugCombPrice(item.ID);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                errText = ex.Message;
                                return -1;
                            }
                            if (item != null)
                            {
                                if (((Neusoft.HISFC.Models.Order.OrderSubtbl)alSubtbls[m]).QtyRule.ToString() == "1")
                                {
                                    item.Qty = order.InjectCount;
                                }
                                else if (((Neusoft.HISFC.Models.Order.OrderSubtbl)alSubtbls[m]).QtyRule.ToString() == "2")
                                {
                                    item.Qty = 1;
                                }
                                else if (((Neusoft.HISFC.Models.Order.OrderSubtbl)alSubtbls[m]).QtyRule.ToString() == "3")
                                {
                                    item.Qty = order.InjectCount;
                                }
                                else if (((Neusoft.HISFC.Models.Order.OrderSubtbl)alSubtbls[m]).QtyRule.ToString() == "4")
                                {
                                    item.Qty = tempCounts.Count;
                                }
                                else if (((Neusoft.HISFC.Models.Order.OrderSubtbl)alSubtbls[m]).QtyRule.ToString() == "5")
                                {
                                    item.Qty = order.Qty;
                                }
                                else
                                {
                                    item.Qty = order.Qty;
                                }
                            }
                            Neusoft.HISFC.Models.Order.OutPatient.Order newOrder = order.Clone();
                            newOrder.ReciptNO = "";
                            newOrder.SequenceNO = -1;
                            if (item != null)
                            {
                                newOrder.Item = item.Clone();
                            }

                            if (((Neusoft.HISFC.Models.Order.OrderSubtbl)alSubtbls[m]).QtyRule.ToString() == "1")
                            {
                                newOrder.Qty = order.InjectCount;
                            }
                            else if (((Neusoft.HISFC.Models.Order.OrderSubtbl)alSubtbls[m]).QtyRule.ToString() == "2")
                            {
                                newOrder.Qty = 1;
                            }
                            else if (((Neusoft.HISFC.Models.Order.OrderSubtbl)alSubtbls[m]).QtyRule.ToString() == "3")
                            {
                                newOrder.Qty = order.InjectCount;
                            }
                            else if (((Neusoft.HISFC.Models.Order.OrderSubtbl)alSubtbls[m]).QtyRule.ToString() == "4")
                            {
                                newOrder.Qty = tempCounts.Count;
                            }
                            else if (((Neusoft.HISFC.Models.Order.OrderSubtbl)alSubtbls[m]).QtyRule.ToString() == "5")
                            {
                                newOrder.Qty = order.Qty;
                            }
                            else
                            {
                                newOrder.Qty = order.Qty;
                            }

                            if (item != null)
                            {
                                newOrder.Unit = item.PriceUnit;
                            }
                            newOrder.Combo = order.Combo;//组合号
                            newOrder.ID = this.orderIntegrate.GetNewOrderID();//医嘱流水号
                            if (newOrder.ID == "")
                            {
                                errText = "获得医嘱流水号出错！";
                                return -1;
                            }

                            newOrder.Item.ItemType = Neusoft.HISFC.Models.Base.EnumItemType.UnDrug;

                            newOrder.IsEmergency = order.IsEmergency;
                            newOrder.IsSubtbl = true;
                            newOrder.Usage = new Neusoft.FrameWork.Models.NeuObject();
                            newOrder.SequenceNO = -1;
                            if (newOrder.ExeDept.ID == "")//执行科室默认
                                newOrder.ExeDept = (this.myOutOrder.Operator as Neusoft.HISFC.Models.Base.Employee).Dept.Clone();

                            alSubOrders.Add(newOrder);
                        }
                        #endregion
                    }
                }
            }

            return 1;
        }

        #endregion

        #region IDealSubjob 成员

        public int DealSubjob(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo, bool isRealTime, Neusoft.HISFC.Models.Order.Inpatient.Order order, ArrayList alOrders, ref ArrayList alSubOrders, ref string errInfo)
        {
            return 1;
        }

        public DateTime FeeDate
        {
            get
            {
                return new DateTime();
            }
            set
            {
                FeeDate = value;
            }
        }

        #endregion

        #region IDealSubjob 成员

        bool isPopForChose = false;

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

        #endregion
    }
}