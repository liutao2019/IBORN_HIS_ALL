using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using FS.HISFC.BizLogic.Order;
using FS.HISFC.Models.Order;

namespace FS.SOC.Local.OutpatientFee.FuYou
{
    /// <summary>
    /// [功能描述: 妇幼门诊附材带出接口实现 ]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-03]<br></br>
    /// 说明：
    /// 1、这个接口是在医生站调用，目前是在保存函数的事务中调用的，启用全局事务，注意不可以在此回滚或提交
    /// 2、接口对传入参数不可以更改其值，仅对ref，out可以
    /// 3、医嘱删除问题：
    ///    医嘱的删除和附材的删除分开：附材的带出是接口处理的，那么附材的删除也应该由接口处理
    ///    目前医生站附材删除是根据组合号判断附材是否为医嘱的带出费用，如果带出的费用和医嘱组合不同，则删除不了附材
    /// </summary>>
    public class SubInterfaceImplement : FS.HISFC.BizProcess.Interface.Order.IDealSubjob
    {
        #region 变量

        static Hashtable hsUsageAndSub = new Hashtable();
        FS.HISFC.BizLogic.Order.OutPatient.Order myOutOrder = new FS.HISFC.BizLogic.Order.OutPatient.Order();
        FS.HISFC.BizProcess.Integrate.Fee feeManagement = new FS.HISFC.BizProcess.Integrate.Fee();
        FS.HISFC.BizProcess.Integrate.Order orderIntegrate = new FS.HISFC.BizProcess.Integrate.Order();

        //记录当前处理附材的主医嘱（仅一条），以便据此创建附材医嘱
        FS.HISFC.Models.Order.OutPatient.Order curOrder = null;

        #endregion

        #region IDealSubjob 成员

        public int DealSubjob(FS.HISFC.Models.Registration.Register r, System.Collections.ArrayList alOrders, ref System.Collections.ArrayList alSubOrders, ref string errText)
        {
            if (this.isPopForChose)
            {
                return 1;
            }

            this.curOrder = null;
            if (r == null || r.ID.Length <= 0)
            {
                errText = "计算附材时，没有患者信息";
                return -1;
            }

            if (alOrders == null || alOrders.Count <= 0)
            {
                errText = "计算附材时，没有医嘱信息";
                return -1;
            }

            int param = this.DeleteSubjobFee(alOrders, ref errText);
            if (param == -1)
            {
                return -1;
            }

            //这里重复查询数据了，外面调用的时候已经查询了，只是没有传进来...
            if (hsUsageAndSub.Count <= 0)
            {
                hsUsageAndSub = myOutOrder.GetUsageAndSub();

                if (hsUsageAndSub.Count <= 0)
                {
                    errText = "没有维护数据";
                    return 0;
                }
            }

            #region 按组合分组
            //理论上组合的排序在一起，但是以下分组对排序无要求
            //同组合的药品
            Hashtable hsSameCombo = new Hashtable();
            //静脉滴注组合，用药接瓶费收取
            Hashtable hsIVDSameCombo = new Hashtable();

            foreach (FS.HISFC.Models.Order.OutPatient.Order order in alOrders)
            {
                if (hsSameCombo.Contains(order.Combo.ID))
                {
                    ((ArrayList)(hsSameCombo[order.Combo.ID])).Add(order);
                }
                else
                {
                    ArrayList al = new ArrayList();
                    al.Add(order);
                    hsSameCombo.Add(order.Combo.ID, al);
                }

                if (hsIVDSameCombo.Contains(order.Combo.ID))
                {
                    ((ArrayList)(hsIVDSameCombo[order.Combo.ID])).Add(order);
                }
                else if (this.CheckIsIVD(order.Usage.ID))
                {
                    ArrayList al = new ArrayList();
                    al.Add(order);
                    hsIVDSameCombo.Add(order.Combo.ID, al);
                }
            }

            #endregion

            #region 妇幼需求关键，请认真阅读(请参考客户文档并与客户沟通)
            /*关键词：
             * 1、用法，仅包含注射用法的带附材。包括：
             *    肌注、
             *    静注、
             *    静滴、
             *    肌注(免皮试)、
             *    静注(免皮试)、
             *    静滴(免皮试)、
             *    肌注(皮试)、
             *    静注(皮试)、
             *    静滴(皮试)、
             *    皮下注射、
             *    皮试、
             *    穴位注射
             *    
             * 2、次数，是每天的次数，即频次，目前没有考虑周期小于1天的情况（如：每小时多少次）
             * 
             * 3、天数，一律视为全部院内注射，天数即为院内注射次数
             * 
             * 4、组数及每组药物数量，即组合数和每组药品品种数的乘积，种数不考虑同组合相同药品
             *    这样理解：所有溶液外的注射药品的品种数
             * 
             * 6、溶液，药品通用名称自定义码=3的表示溶液，为大输液，不计算品种数
             * 
             * 附材带出规则总结：
             * 1、费用归类，包括(请参考维护)：
             *    注射费： 最大院注
             *          肌注费：肌肉注射     + 一次性注射器
             *          静注费：静脉注射     + 一次性注射器 + 一次性头皮针
             *          静滴费：门诊静脉输液 + 一次性注射器 + 一次性头皮针 + 一次性输液器
             *          穴注费：穴位注射     + 一次性注射器
             *    皮试费：固定数量 每组收取
             *          皮试费：一次性注射器 + 皮内注射
             *          试针费：同上
             *          皮注费：同上
             *    接瓶费：每天组合数减1的累计和
             *          静脉输液\连续输液第二组起每组加改
             *    组合费： 静滴才有，每组*天数 合计
             *          一次性注射器(需要用它吸取药品混合到溶液中，避免污染各药品一个，当然要收钱了)
             * 2、用法对应的费用组合规律举例：
             *    肌注 =       肌注(免皮试)   = 肌注费*次数*天数
             *    肌注(皮试) = 肌注 + 皮试费
             *    静滴 =       静滴(免皮试)   = 静滴费*次数*天数 + 接瓶费*（组数-1）*天数 + 组合费*组数及每组药物数量*天数
             *    静滴(皮试) = 静滴 + 皮试费
             * */
            #endregion

            //以下附材费用计算由简入难
            #region 皮试费
            //皮试任何情况下都是每组只做一次，要不就不做皮试
            int astQty = 0;
            foreach (ArrayList alOneCombo in hsSameCombo.Values)
            {
                if (alOneCombo.Count == 0)
                {
                    continue;
                }
                foreach (FS.HISFC.Models.Order.OutPatient.Order order in alOneCombo)
                {
                    //每组仅收取一次，只要处理第一个皮试用法的药即可
                    if (this.CheckIsAST(order.Usage.ID) && order.Qty > 0)
                    {
                        astQty++;
                        if (this.curOrder == null)
                        {
                            this.curOrder = order;
                        }
                        break;
                    }
                }
            }
            //在此创建皮试附材费用实体，注意可能不止一种费用
            if (astQty > 0)
            {
                if (hsUsageAndSub.Contains("06"))
                {
                    ArrayList al = hsUsageAndSub["06"] as ArrayList;
                    foreach (FS.HISFC.Models.Order.OrderSubtbl o in al)
                    {
                        FS.HISFC.Models.Order.OutPatient.Order newOrder = this.CreateOrder(curOrder, o.ID, (decimal)astQty, ref errText);
                        if (newOrder == null)
                        {
                            return -1;
                        }
                        alSubOrders.Add(newOrder);
                    }
                }
            }
            #endregion

            #region 组合费
            //仅仅滴注有，每天每品种一次，则和频次没有关系
            //记录组合费数量
            int comboQty = 0;
            foreach (ArrayList alOneCombo in hsIVDSameCombo.Values)
            {
                if (alOneCombo.Count == 0)
                {
                    continue;
                }

                foreach (FS.HISFC.Models.Order.OutPatient.Order order in alOneCombo)
                {
                    if (!this.CheckIsMainDrug(order))
                    {
                        continue;
                    }
                    //考虑到每次用量*频次*天数!=总量的情况 
                    int days = this.GetRealDays(order);
                    if (curOrder == null)
                    {
                        curOrder = order;
                    }
                    comboQty = comboQty + days;
                }
            }
            //在此创建组合费费用实体，注意可能不止一种费用
            if (comboQty > 0)
            {
                ArrayList al = this.CreateComboSubOrders(curOrder, (decimal)comboQty, ref errText);
                if (al == null)
                {
                    return -1;
                }
                if (al.Count > 0)
                {
                    alSubOrders.AddRange(al);
                }
            }
            #endregion

            #region 注射费
            //数量为注射频次数*天数并考虑总量，和组合数、品种数没有关系
            //正解：最大注射次数

            Hashtable hsISubFeeSet = new Hashtable();
            string curUsageNO = "";

            foreach (ArrayList alOneCombo in hsSameCombo.Values)
            {
                if (alOneCombo.Count == 0)
                {
                    continue;
                }

                foreach (FS.HISFC.Models.Order.OutPatient.Order order in alOneCombo)
                {
                    if (!this.CheckIsMainDrug(order))
                    {
                        continue;
                    }
                    if (curOrder == null)
                    {
                        curOrder = order;
                    }
                    int thisQty = this.GetRealTimes(order);

                    curUsageNO = order.Usage.ID;

                    if (this.CheckIsIM(order.Usage.ID))
                    {
                        curUsageNO = "02";
                    }
                    else if (this.CheckIsIV(order.Usage.ID))
                    {
                        curUsageNO = "03";
                    }
                    else if (this.CheckIsIVD(order.Usage.ID))
                    {
                        curUsageNO = "04";
                    }
                    else if (order.Usage.ID == "21")
                    {
                        curUsageNO = "21";
                    }

                    if (hsISubFeeSet.Contains(curUsageNO))
                    {
                        if (curUsageNO == "02" || curUsageNO == "03")
                        {
                            SubFeeSet subFeeSet = hsISubFeeSet[curUsageNO] as SubFeeSet;
                            subFeeSet.Qty += thisQty;
                        }
                        else
                        {
                            SubFeeSet subFeeSet = hsISubFeeSet[curUsageNO] as SubFeeSet;
                            if (subFeeSet.Qty < thisQty)
                            {
                                subFeeSet.Qty = thisQty;
                            }
                        }
                    }
                    else
                    {
                        SubFeeSet subFeeSet = new SubFeeSet();
                        subFeeSet.UsageNO = curUsageNO;
                        subFeeSet.Qty = thisQty;
                        hsISubFeeSet.Add(subFeeSet.UsageNO, subFeeSet);
                    }
                }
            }
            //在此创建注射费
            foreach (SubFeeSet subFeeSet in hsISubFeeSet.Values)
            {
                string usageNO = subFeeSet.UsageNO;
                decimal qty = subFeeSet.Qty;
                if (qty > 0)
                {
                    if (hsUsageAndSub.Contains(usageNO))
                    {
                        ArrayList al = hsUsageAndSub[usageNO] as ArrayList;
                        foreach (FS.HISFC.Models.Order.OrderSubtbl o in al)
                        {
                            FS.HISFC.Models.Order.OutPatient.Order newOrder = this.CreateOrder(curOrder, o.ID, (int)qty, ref errText);
                            if (newOrder == null)
                            {
                                return -1;
                            }
                            alSubOrders.Add(newOrder);
                        }
                    }
                }
            }
            #endregion

            #region 接瓶费

            //仅和静脉滴注有关，接瓶费是每天组合数的累计和，不考虑频次
            int maxDays = 0;//所有静脉滴注的最大天数

            //记录每组最大注射天数
            Hashtable hsMaxDaysPerCombo = new Hashtable();

            foreach (ArrayList alOneCombo in hsIVDSameCombo.Values)
            {
                if (alOneCombo.Count == 0)
                {
                    continue;
                }

                int qty = 0;
                foreach (FS.HISFC.Models.Order.OutPatient.Order order in alOneCombo)
                {
                    if (order.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        continue;
                    }
                    int days = this.GetRealDays(order);
                    if (curOrder == null)
                    {
                        curOrder = order;
                    }
                    if (days > qty)
                    {
                        qty = days;
                    }
                    if (days > maxDays)
                    {
                        maxDays = days;
                    }
                    if (hsMaxDaysPerCombo.Contains(order.Combo.ID))
                    {
                        hsMaxDaysPerCombo[order.Combo.ID] = qty;
                    }
                    else
                    {
                        hsMaxDaysPerCombo.Add(order.Combo.ID, qty);
                    }
                }
            }

            //只有一个组合是不要考虑接瓶的
            if (hsMaxDaysPerCombo.Count > 1)
            {
                int totJPQty = 0;
                for (int day = 0; day < maxDays; day++)
                {
                    int perDayComboQty = -1;//接瓶费是每天组合数减1的累计和
                    foreach (int onceComboMaxDays in hsMaxDaysPerCombo.Values)
                    {
                        if (onceComboMaxDays > day)
                        {
                            perDayComboQty++;
                        }
                    }
                    totJPQty = totJPQty + perDayComboQty;
                }

                //在此创建接瓶费用
                if (totJPQty > 0)
                {
                    ArrayList al = this.CreateJPSubOrders(curOrder, (decimal)totJPQty, ref errText);
                    if (al == null)
                    {
                        return -1;
                    }
                    if (al.Count > 0)
                    {
                        alSubOrders.AddRange(al);
                    }
                }
            }
            #endregion

            ArrayList Temp = new ArrayList();
            Hashtable hsTemp = new Hashtable();
            //妇幼廖科要求 把相同的项目数量加在一起，所有辅材一个组合号、一个收费序列
            string combID = myOutOrder.GetNewOrderComboID();

            //如果医生没有删除、修改医嘱就点击保存，也要保证收费序列号和某一药品的一致，否则下次无法删除辅材 houwb
            string feeSeq = "";
            foreach (FS.HISFC.Models.Order.OutPatient.Order order in alOrders)
            {
                if (!string.IsNullOrEmpty(order.ReciptSequence))
                {
                    feeSeq = order.ReciptSequence;
                }
            }

            //不能重新生成序列号，否则无法删除辅材了...
            //string feeSeq = feeManagement.GetRecipeSequence();
            foreach (FS.HISFC.Models.Order.OutPatient.Order order in alSubOrders)
            {
                order.Combo.ID = myOutOrder.GetNewOrderComboID();
                if (!string.IsNullOrEmpty(feeSeq))
                {
                    order.ReciptSequence = feeSeq;
                }

                if (!hsTemp.Contains(order.Item.ID))
                {
                    hsTemp.Add(order.Item.ID, order);
                }
                else
                {
                    ((FS.HISFC.Models.Order.OutPatient.Order)hsTemp[order.Item.ID]).Qty += order.Qty;
                }
            }
            alSubOrders = new ArrayList();
            foreach (string itemID in new ArrayList(hsTemp.Keys))
            {
                alSubOrders.Add(hsTemp[itemID]);
            }

            return 1;
        }

        public int DealSubjob(FS.HISFC.Models.Registration.Register r, System.Collections.ArrayList alFee, ref string errText)
        {


            return 1;
        }

        #endregion

        #region 函数

        /// <summary>
        /// 检测用法是否皮试用法或带皮试的用法
        /// </summary>
        /// <param name="usageNO"></param>
        /// <returns></returns>
        private bool CheckIsAST(string usageNO)
        {
            string usageSysNO = usageNO;
            string usageName = "";
            FS.HISFC.Models.Base.Const con = SOC.HISFC.BizProcess.Cache.Common.GetUsage(usageNO);
            if (con != null)
            {
                usageSysNO = con.UserCode;
                usageName = con.Name;
            }
            if (usageSysNO == "IAST"
               || usageName.Replace(".", "").ToLower().Contains("ast")
                || usageName == "IH" || usageName.Replace(".", "").ToLower().Contains("ih"))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 检测用法是否为静脉注射
        /// </summary>
        /// <param name="usageNO"></param>
        /// <returns></returns>
        private bool CheckIsIV(string usageNO)
        {
            string usageSysNO = usageNO;
            string usageName = "";
            FS.HISFC.Models.Base.Const con = SOC.HISFC.BizProcess.Cache.Common.GetUsage(usageNO);
            if (con != null)
            {
                usageSysNO = con.UserCode;
                usageName = con.Name;
            }
            if (usageSysNO == "IV" || usageName.Replace(".", "").ToLower() == "iv")
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 检测用法是否为静脉滴注
        /// </summary>
        /// <param name="usageNO"></param>
        /// <returns></returns>
        private bool CheckIsIVD(string usageNO)
        {
            string usageSysNO = usageNO;
            string usageName = "";
            FS.HISFC.Models.Base.Const con = SOC.HISFC.BizProcess.Cache.Common.GetUsage(usageNO);
            if (con != null)
            {
                usageSysNO = con.UserCode;
                usageName = con.Name;
            }
            if (usageSysNO == "IVD" || usageName.Replace(".", "").ToLower() == "ivd")
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 检测用法是否为肌肉注射
        /// </summary>
        /// <param name="usageNO"></param>
        /// <returns></returns>
        private bool CheckIsIM(string usageNO)
        {
            string usageSysNO = usageNO;
            string usageName = "";
            FS.HISFC.Models.Base.Const con = SOC.HISFC.BizProcess.Cache.Common.GetUsage(usageNO);
            if (con != null)
            {
                usageSysNO = con.UserCode;
                usageName = con.Name;
            }
            if (usageSysNO == "IM" || usageName.Replace(".", "").ToLower() == "im")
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 检测药品是否为主药(溶液、非药品外的药品)
        /// </summary>
        /// <param name="drugNO"></param>
        /// <returns></returns>
        private bool CheckIsMainDrug(FS.HISFC.Models.Order.OutPatient.Order order)
        {
            if (order.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                return false; ;
            }
            FS.HISFC.Models.Pharmacy.Item item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID);
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
                FS.HISFC.BizLogic.Manager.Frequency frequencyManagement = new FS.HISFC.BizLogic.Manager.Frequency();
                ArrayList alFrequency = frequencyManagement.GetBySysClassAndID("ROOT", "ALL", frequencyID);
                if (alFrequency != null && alFrequency.Count > 0)
                {
                    FS.HISFC.Models.Order.Frequency obj = alFrequency[0] as FS.HISFC.Models.Order.Frequency;
                    string[] str = obj.Time.Split('-');
                    return str.Length;
                }
                return 100;
            }
        }

        /// <summary>
        /// 获取实际用药天数，总量并不一定和每次用量*频次*天数相等
        /// 实际天数=总量/(每次用量*频次)向上取整
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private int GetRealDays(FS.HISFC.Models.Order.OutPatient.Order order)
        {
            int days = 0;

            days = FS.FrameWork.Function.NConvert.ToInt32(order.HerbalQty);
            return days;

            decimal count = this.GetFrequencyCount(order.Frequency.ID);
            decimal doseOnce = order.DoseOnce;
            if (order.DoseUnit != order.Unit)
            {
                FS.HISFC.Models.Pharmacy.Item item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID);
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
        /// 获取实际用药次数，总量并不一定和每次用量*频次*天数相等
        /// 实际次数=总量/每次用量向上取整
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private int GetRealTimes(FS.HISFC.Models.Order.OutPatient.Order order)
        {
            int tiems = 0;
            tiems = FS.FrameWork.Function.NConvert.ToInt32(order.Qty);
            //静脉滴注一天只收一次
            if (CheckIsIVD(order.Usage.ID))
            {
                tiems = GetRealDays(order);
            }

            return tiems;

            decimal doseOnce = order.DoseOnce;
            if (order.DoseUnit != order.Unit)
            {
                FS.HISFC.Models.Pharmacy.Item item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID);
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
                            //每次量是最小单位
                        }
                    }
                    else if (order.Unit == item.PackUnit)//总量单位是包装单位
                    {
                        if (order.DoseUnit == item.DoseUnit)//每次量是剂量单位
                        {
                            doseOnce = doseOnce / item.BaseDose / item.PackQty;
                        }
                        else if (order.DoseUnit == item.MinUnit)//每次量是最小单位
                        {
                            doseOnce = doseOnce / item.PackQty;
                        }
                    }
                }
            }

            tiems = (int)(order.Qty / doseOnce);
            if (tiems < order.Qty / doseOnce)
            {
                tiems = tiems + 1;
            }

            //静脉滴注一天只收一次
            if (CheckIsIVD(order.Usage.ID))
            {
                tiems = GetRealDays(order);
            }

            return tiems;
        }

        /// <summary>
        /// 创建医嘱，目前仅限与非药品项目
        /// </summary>
        /// <param name="itemNO">项目编码</param>
        /// <param name="qty">总数量</param>
        /// <param name="errText">错误信息</param>
        /// <returns>null 发生错误</returns>
        private FS.HISFC.Models.Order.OutPatient.Order CreateOrder(FS.HISFC.Models.Order.OutPatient.Order order, string itemNO, decimal qty, ref string errText)
        {
            if (order == null)
            {
                errText = "附材计算时，用于clone的医嘱为null";
                return null;
            }
            if (string.IsNullOrEmpty(itemNO))
            {
                errText = "附材计算时，用于获取项目的编码为空";
                return null;
            }
            FS.HISFC.Models.Fee.Item.Undrug item = null;
            item = feeManagement.GetItem(itemNO);//获得最新项目信息
            if (item == null)
            {
                errText = "获取项目失败，尝试获取项目的项目编码为" + itemNO;
                return null;
            }
            if (item.UnitFlag == "1")
            {
                item.Price = feeManagement.GetUndrugCombPrice(itemNO);
            }

            item.Qty = qty;

            FS.HISFC.Models.Order.OutPatient.Order newOrder = order.Clone();

            try
            {
                newOrder.ReciptNO = "";
                newOrder.SequenceNO = -1;

                newOrder.Item = item.Clone();
                newOrder.Qty = qty;

                newOrder.Unit = item.PriceUnit;
                if (order.Combo == null || string.IsNullOrEmpty(order.Combo.ID))
                {
                    errText = "附材计算时，用于clone的医嘱组合号为null";
                    return null;
                }
                newOrder.Combo = order.Combo.Clone();//组合号

                newOrder.ID = this.orderIntegrate.GetNewOrderID();//医嘱流水号
                if (newOrder.ID == "")
                {
                    errText = "计算项目附材时，对新增加的附材获得医嘱流水号出错！";
                    return null;
                }

                newOrder.Item.ItemType = FS.HISFC.Models.Base.EnumItemType.UnDrug;

                newOrder.IsEmergency = order.IsEmergency;
                newOrder.IsSubtbl = true;
                newOrder.Usage = new FS.FrameWork.Models.NeuObject();
                newOrder.SequenceNO = -1;
                if (newOrder.ExeDept.ID == "")//执行科室默认
                {
                    newOrder.ExeDept = (this.myOutOrder.Operator as FS.HISFC.Models.Base.Employee).Dept.Clone();
                }
            }
            catch (Exception ex)
            {
                errText = "计算项目附材时，创建附材医嘱发生错误：" + ex.Message;
                return null;
            }
            return newOrder;
        }

        /// <summary>
        /// 接瓶费医嘱
        /// </summary>
        /// <param name="order">原始医嘱</param>
        /// <param name="qty">数量</param>
        /// <param name="errText">错误信息</param>
        /// <returns>null 发生错误</returns>
        private ArrayList CreateJPSubOrders(FS.HISFC.Models.Order.OutPatient.Order order, decimal qty, ref string errText)
        {
            ArrayList al = new ArrayList();
            FS.HISFC.Models.Order.OutPatient.Order newOrder = this.CreateOrder(order, "F00000040425", (decimal)qty, ref errText);
            if (newOrder == null)
            {
                return null;
            }
            al.Add(newOrder);

            return al;
        }

        /// <summary>
        /// 组合费医嘱
        /// </summary>
        /// <param name="order">原始医嘱</param>
        /// <param name="qty">数量</param>
        /// <param name="errText">错误信息</param>
        /// <returns>null 发生错误</returns>
        private ArrayList CreateComboSubOrders(FS.HISFC.Models.Order.OutPatient.Order order, decimal qty, ref string errText)
        {
            ArrayList al = new ArrayList();
            FS.HISFC.Models.Order.OutPatient.Order newOrder = this.CreateOrder(order, "F00000039488", (decimal)qty, ref errText);
            if (newOrder == null)
            {
                return null;
            }
            al.Add(newOrder);

            return al;
        }

        /// <summary>
        /// 附材删除
        /// </summary>
        /// <param name="register"></param>
        /// <param name="alOrder"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        private int DeleteSubjobFee(ArrayList alOrder, ref string errInfo)
        {
            //对保存过产生的附材删除,如果医生站保存函数删除附材的话，在此就没必要了
            //删除附材用，记录已删除的处方，避免重复删除
            Hashtable hsDelRecipe = new Hashtable();
            FS.HISFC.BizLogic.Fee.Outpatient outpatientFeeMgr = new FS.HISFC.BizLogic.Fee.Outpatient();

            foreach (FS.HISFC.Models.Order.OutPatient.Order order in alOrder)
            {
                //删除项目所带的附材
                //整方的辅材都删除
                if (!string.IsNullOrEmpty(order.ReciptNO) && !hsDelRecipe.ContainsKey(order.ReciptNO))
                {
                    hsDelRecipe.Add(order.ReciptNO, null);
                    if (outpatientFeeMgr.DeleteSubFeeItem(order.ReciptNO) < 0)
                    {
                        errInfo = outpatientFeeMgr.Err;
                        return -1;
                    }

                }
            }

            //按照收费序列号重新删除一遍
            foreach (FS.HISFC.Models.Order.OutPatient.Order order in alOrder)
            {
                try
                {
                    ArrayList alSubAndOrder = feeManagement.QueryValidFeeDetailbyClinicCodeAndRecipeSeq(order.Patient.ID, order.ReciptSequence);
                    if (alSubAndOrder == null)
                    {
                        errInfo = feeManagement.Err;
                        return -1;
                    }

                    for (int j = 0; j < alSubAndOrder.Count; j++)
                    {
                        FS.HISFC.Models.Fee.Outpatient.FeeItemList item = alSubAndOrder[j] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                        if (item.Item.IsMaterial)
                        {
                            if (feeManagement.DeleteFeeItemListByRecipeNO(item.RecipeNO, item.SequenceNO.ToString()) < 0)
                            {
                                errInfo = feeManagement.Err;
                                return -1;
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

        #endregion

        #region 住院附材处理

        /// <summary>
        /// 获取科室维护的所有附材
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="errInfo"></param>
        /// <param name="hsSub"></param>
        /// <returns></returns>
        private int GetSubListByDept(string deptCode, ref string errInfo, ref Hashtable hsSub)
        {
            hsSub = new Hashtable();

            try
            {
                ArrayList alSub = this.subMgr.GetSubtblInfo("1", "ALL", deptCode);
                if (alSub == null)
                {
                    errInfo = this.subMgr.Err;
                    return -1;
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
        /// 接瓶费数量
        /// </summary>
        int countMeetBottlesFee = -1;

        /// <summary>
        /// 已计算接瓶数量的医嘱
        /// </summary>
        Hashtable hsMeetBottleOrders = new Hashtable(); 

        private FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// 收取皮试费的医嘱
        /// </summary>
        Hashtable hsASTOrders = new Hashtable();

        /// <summary>
        /// 是否已收取静滴费
        /// </summary>
        bool isVDFee = false;

        /// <summary>
        /// 根据用法收取附材
        /// </summary>
        /// <param name="isRealTime">是否实时收取，否则为晚上后台收取</param>
        /// <param name="usage"></param>
        /// <param name="order"></param>
        /// <param name="alOrders"></param>
        /// <param name="patientInfo"></param>
        /// <param name="alSubOrders"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        private int DealSubjobByUsage(bool isRealTime, string usage, FS.HISFC.Models.Order.Inpatient.Order order, ArrayList alOrders, FS.HISFC.Models.RADT.PatientInfo patientInfo, ref ArrayList alSubOrders, ref string errInfo)
        {
            ArrayList alSubInfo = new ArrayList();

            //附材的带出根据患者所在科室来处理
            //如果到南庄这里要区分手术室开立 的情况，附材要根据开立医生带出附材
            //if (!string.IsNullOrEmpty(usage))
            //{
            //    alSubInfo = subMgr.GetSubtblInfo("1", usage, patientInfo.PVisit.PatientLocation.Dept.ID);
            //}
            //else
            //{
            //    alSubInfo = subMgr.GetSubtblInfo("1", order.Usage.ID, patientInfo.PVisit.PatientLocation.Dept.ID);
            //}

            //if (alSubInfo == null)
            //{
            //    errInfo = subMgr.Err;
            //    return -1;
            //}
            //else if (alSubInfo.Count == 0)
            //{
            //    return 1;
            //}
            ArrayList alASTSubinfo = new ArrayList();
            //ArrayList alASTSubinfo = subMgr.GetSubtblInfo("1", "06", order.ReciptDept.ID);
            //if (alASTSubinfo == null)
            //{
            //    errInfo = subMgr.Err;
            //    return -1;
            //}

            FS.HISFC.Models.Base.Item obj = null;
            alSubOrders = new ArrayList();

            #region 实时收取，附加到医嘱上

            if (isRealTime)
            {
                if (!hsSubList.Contains(order.Usage.ID))
                {
                    return 1;
                }

                alSubInfo = hsSubList[order.Usage.ID] as ArrayList;
                if (alSubInfo == null)
                {
                    errInfo = subMgr.Err;
                    return -1;
                }
                else if (alSubInfo.Count == 0)
                {
                    return 1;
                }

                foreach (OrderSubtblNew subOjb in alSubInfo)
                {
                    if (order.ReciptDept.ID == subOjb.Dept_code || subOjb.Dept_code == "ROOT")
                    {
                        obj = new FS.HISFC.Models.Base.Item();
                        obj.ID = subOjb.Item.ID;

                        //0 每组收取
                        if (subOjb.CombArea == "0")
                        {
                            switch (subOjb.FeeRule)
                            {
                                //0 固定数量
                                case "0":
                                    obj.Qty = subOjb.Qty;
                                    break;
                                //1 最大院注
                                case "1":
                                    obj.Qty = order.InjectCount;
                                    break;
                                //2 组内品种数
                                case "2":
                                    obj.Qty = GetFeeCountByCombOrders(alOrders, order.Combo.ID);
                                    break;
                                //3 最大医嘱数量
                                case "3":
                                    obj.Qty = order.Qty;
                                    break;
                                //4 频次数
                                case "4":
                                    //临嘱按照频次数量收取，长嘱按照固定数量收取
                                    if (order.OrderType.IsDecompose)
                                    {
                                        obj.Qty = subOjb.Qty;
                                    }
                                    else
                                    {
                                        obj.Qty = GetFrequencyCount(order.Frequency.ID);
                                    }
                                    break;
                            }

                            //限制类别：0 不限制 1 婴儿使用 2 非婴儿使用
                            if (subOjb.LimitType == "0")
                            {
                                alSubOrders.Add(obj);
                            }
                            else if (subOjb.LimitType == "1" && CheckIsChildren(patientInfo.Birthday))
                            {
                                alSubOrders.Add(obj);
                            }
                            else if (subOjb.LimitType == "2" && !CheckIsChildren(patientInfo.Birthday))
                            {
                                alSubOrders.Add(obj);
                            }
                        }
                    }
                }
            }
            #endregion

            #region 后台收取
            //静滴的首次收取和接瓶费
            else
            {
                hsMeetBottleOrders = new Hashtable();
                hsASTOrders = new Hashtable();
                countMeetBottlesFee = -1;
                isVDFee = false;

                //目前只判断妇幼的VD收取
                //静滴费=住院静脉输液*1+一次性注射器*2+一次性输液器*1+一次性头皮针*1
                //接瓶费=sum(组频次)-1
                foreach (FS.HISFC.Models.Order.Inpatient.Order orderObj in alOrders)
                {
                    #region  皮试费
                    if (this.CheckIsAST(orderObj.Usage.ID))
                    {
                        if (feeDate < new DateTime(2000, 1, 1))
                        {
                            feeDate = this.subMgr.GetDateTimeFromSysDateTime();
                        }

                        ////判断只有医嘱开始当天才收取皮试费用
                        //if (orderObj.BeginTime.Date == feeDate.Date//this.subMgr.GetDateTimeFromSysDateTime().Date
                        //    && !hsASTOrders.Contains(orderObj.Combo.ID))
                        //{
                            //--houwb 2012-3-26 修改规则频次费 根据各自维护的带出
                            if (!hsSubList.Contains(orderObj.Usage.ID))
                            {
                                continue;
                            }

                            alASTSubinfo = hsSubList[orderObj.Usage.ID] as ArrayList;
                            if (alASTSubinfo == null)
                            {
                                errInfo = subMgr.Err;
                                return -1;
                            }
                            else if (alASTSubinfo.Count == 0)
                            {
                                continue;
                            }
                            //--end

                            if (!hsASTOrders.Contains(orderObj.Combo.ID))
                            {
                                foreach (OrderSubtblNew subOjb in alASTSubinfo)
                                {
                                    obj = new FS.HISFC.Models.Base.Item();
                                    obj.ID = subOjb.Item.ID;

                                    //1 第一组收取  此处收取皮试费
                                    if (subOjb.CombArea == "1")
                                    {
                                        switch (subOjb.FeeRule)
                                        {
                                            //0 固定数量
                                            case "0":
                                                obj.Qty = subOjb.Qty;
                                                break;
                                            //1 最大院注
                                            case "1":
                                                obj.Qty = orderObj.InjectCount;
                                                break;
                                            //2 组内品种数
                                            case "2":
                                                obj.Qty = GetFeeCountByCombOrders(alOrders, orderObj.Combo.ID);
                                                break;
                                            //3 最大医嘱数量
                                            case "3":
                                                obj.Qty = orderObj.Qty;
                                                break;
                                            //4 频次数
                                            case "4":
                                                //临嘱按照频次数量收取，长嘱按照固定数量收取
                                                if (orderObj.OrderType.IsDecompose)
                                                {
                                                    obj.Qty = subOjb.Qty;
                                                }
                                                else
                                                {
                                                    obj.Qty = GetFrequencyCount(orderObj.Frequency.ID);
                                                }
                                                break;
                                        }

                                        //这里是收取皮试费的，所以只是在有首次收取标志的时候 才收取皮试费
                                        if (subOjb.FirstFeeFlag == "1")
                                        {
                                            //判断只有医嘱开始当天才收取皮试费用
                                            if (orderObj.BeginTime.Date == feeDate.Date)
                                            {

                                                //限制类别：0 不限制 1 婴儿使用 2 非婴儿使用
                                                if (subOjb.LimitType == "0")
                                                {
                                                    alSubOrders.Add(obj);
                                                }
                                                else if (subOjb.LimitType == "1" && CheckIsChildren(patientInfo.Birthday))
                                                {
                                                    alSubOrders.Add(obj);
                                                }
                                                else if (subOjb.LimitType == "2" && !CheckIsChildren(patientInfo.Birthday))
                                                {
                                                    alSubOrders.Add(obj);
                                                }
                                            }
                                        }
                                    }
                                }
                                hsASTOrders.Add(orderObj.Combo.ID, orderObj);
                            }
                        //}
                    }
                    #endregion

                    #region 静脉输液费

                    if (CheckIsIVD(orderObj.Usage.ID))
                    {
                        if (!hsSubList.Contains(orderObj.Usage.ID))
                        {
                            continue;
                        }

                        alSubInfo = hsSubList[orderObj.Usage.ID] as ArrayList;
                        if (alSubInfo == null)
                        {
                            errInfo = subMgr.Err;
                            return -1;
                        }
                        else if (alSubInfo.Count == 0)
                        {
                            continue;
                        }

                        foreach (OrderSubtblNew subOjb in alSubInfo)
                        {
                            //对于首次收取的皮试费，在上面已经处理
                            if (subOjb.FirstFeeFlag == "1")
                            {
                                continue;
                            }

                            if (orderObj.ReciptDept.ID == subOjb.Dept_code || subOjb.Dept_code == "ROOT")
                            {
                                obj = new FS.HISFC.Models.Base.Item();
                                obj.ID = subOjb.Item.ID;

                                //1 第一组收取  此处收取静滴费  判断包含此用法的一天只收取一次
                                //静滴费=住院静脉输液*1+一次性注射器*2+一次性输液器*1+一次性头皮针*1
                                if (subOjb.CombArea == "1")
                                {
                                    //属于静滴费，并且当前所有医嘱都没有收取静滴费
                                    if (!isVDFee)
                                    {
                                        switch (subOjb.FeeRule)
                                        {
                                            //0 固定数量
                                            case "0":
                                                obj.Qty = subOjb.Qty;
                                                break;
                                            //1 最大院注
                                            case "1":
                                                obj.Qty = orderObj.InjectCount;
                                                break;
                                            //2 组内品种数
                                            case "2":
                                                obj.Qty = GetFeeCountByCombOrders(alOrders, orderObj.Combo.ID);
                                                break;
                                            //3 最大医嘱数量
                                            case "3":
                                                obj.Qty = orderObj.Qty;
                                                break;
                                            //4 频次数
                                            case "4":
                                                //临嘱按照频次数量收取，长嘱按照固定数量收取
                                                if (orderObj.OrderType.IsDecompose)
                                                {
                                                    obj.Qty = subOjb.Qty;
                                                }
                                                else
                                                {
                                                    obj.Qty = GetFrequencyCount(orderObj.Frequency.ID);
                                                }
                                                break;
                                        }
                                        //限制类别：0 不限制 1 婴儿使用 2 非婴儿使用
                                        if (subOjb.LimitType == "0")
                                        {
                                            alSubOrders.Add(obj);
                                        }
                                        else if (subOjb.LimitType == "1" && CheckIsChildren(patientInfo.Birthday))
                                        {
                                            alSubOrders.Add(obj);
                                        }
                                        else if (subOjb.LimitType == "2" && !CheckIsChildren(patientInfo.Birthday))
                                        {
                                            alSubOrders.Add(obj);
                                        }
                                    }
                                }

                                //2 第二组起加收  此处收取接瓶费
                                //接瓶费=sum(组频次)-1
                                else if (subOjb.CombArea == "2")
                                {
                                    //属于静滴费，并且当前组 未计算过接瓶费
                                    if (this.CheckIsIVD(orderObj.Usage.ID) && !hsMeetBottleOrders.Contains(orderObj.Combo.ID))
                                    {
                                        //限制类别：0 不限制 1 婴儿使用 2 非婴儿使用
                                        if (subOjb.LimitType == "0" ||
                                            (subOjb.LimitType == "1" && CheckIsChildren(patientInfo.Birthday)) ||
                                            (subOjb.LimitType == "2" && !CheckIsChildren(patientInfo.Birthday)))
                                        {
                                            //住院第一天次数等于首日量  只有长期医嘱才按照首日量计算
                                            if (orderObj.MOTime.Date == this.feeDate.Date && orderObj.OrderType.IsDecompose)
                                            {
                                                countMeetBottlesFee += FS.FrameWork.Function.NConvert.ToInt32(orderObj.FirstUseNum);
                                            }
                                            else
                                            {
                                                countMeetBottlesFee += this.GetFrequencyCount(orderObj.Frequency.ID);
                                            }
                                            hsMeetBottleOrders.Add(orderObj.Combo.ID, orderObj);
                                        }
                                    }
                                }
                            }
                        }
                        isVDFee = true;
                    }

                    #endregion
                }

                //接瓶费收取
                if (countMeetBottlesFee > 0)
                {
                    obj = new FS.HISFC.Models.Base.Item();
                    //项目：静脉输液连续输液第二组起
                    obj.ID = "F00000040425";
                    obj.Qty = countMeetBottlesFee;
                    alSubOrders.Add(obj);
                }
            }

            #endregion

            return 1;
        }

        /// <summary>
        /// 是否是婴儿
        /// </summary>
        /// <param name="birthday"></param>
        /// <returns>false 不是婴儿</returns>
        private bool CheckIsChildren(DateTime birthday)
        {
            if (this.feeDate.Year <= childrenAge)
            {
                return false;
            }
            if (this.feeDate.AddYears(-childrenAge).Date < birthday.Date)
            {
                return true;
            }

            return false;
        }

        private bool isContionVD(ArrayList alOrders)
        {
            foreach (FS.HISFC.Models.Order.Inpatient.Order order in alOrders)
            {
                if (this.CheckIsIVD(order.Usage.ID))
                {
                    return true;
                }
            }
            return false;
        }

        private int GetFeeCountByCombOrders(ArrayList alOneCombo)
        {
            int comboQty = 0;
            foreach (FS.HISFC.Models.Order.OutPatient.Order order in alOneCombo)
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

        private int GetFeeCountByCombOrders(ArrayList alOneCombo, string combNo)
        {
            int comboQty = 0;
            foreach (FS.HISFC.Models.Order.OutPatient.Order order in alOneCombo)
            {
                if (order.Combo.ID == combNo)
                {
                    if (!this.CheckIsMainDrug(order))
                    {
                        continue;
                    }
                    //考虑到每次用量*频次*天数!=总量的情况
                    int days = this.GetRealDays(order);
                    comboQty += days;
                }
            }
            return comboQty;
        }

        SubtblManager subMgr = new SubtblManager();

        /// <summary>
        /// 所有附材信息
        /// </summary>
        Hashtable hsSubList = new Hashtable();

        /// <summary>
        /// 判断是否婴儿的年龄限制
        /// </summary>
        int childrenAge = 14;

        /// <summary>
        /// 医嘱按照开立顺序排序
        /// </summary>
        OrderCompareBySortID orderCompare = new OrderCompareBySortID();

        public int DealSubjob(FS.HISFC.Models.RADT.PatientInfo patientInfo, bool isRealTime, FS.HISFC.Models.Order.Inpatient.Order order, ArrayList alOrders, ref ArrayList alSubOrders, ref string errInfo)
        {
            if (this.isPopForChose)
            {
                return 1;
            }

            this.curOrder = null;
            if (patientInfo == null || patientInfo.ID.Length <= 0)
            {
                errInfo = "计算附材时，没有患者信息";
                return -1;
            }

            if (alOrders == null || alOrders.Count <= 0)
            {
                errInfo = "计算附材时，没有医嘱信息";
                return -1;
            }

            if (childrenAge == 14)
            {
                //获取设置的儿童年龄上限
                childrenAge = this.ctrlMgr.GetControlParam<int>("HN0001", true, 14);
            }

            //if (hsSubList.Count == 0 || ((ArrayList)hsSubList[0]).Count == 0 || ((OrderSubtblNew)((ArrayList)hsSubList[0])[0]).Dept_code != patientInfo.PVisit.PatientLocation.Dept.ID)
            //{
                if (this.GetSubListByDept(patientInfo.PVisit.PatientLocation.Dept.ID, ref errInfo, ref hsSubList) == -1)
                {
                    return -1;
                }
            //}

            alOrders.Sort(orderCompare);

            if (this.DealSubjobByUsage(isRealTime, "", order, alOrders, patientInfo, ref alSubOrders, ref errInfo) == -1)
            {
                return -1;
            }

            //皮试费
            //ArrayList alTemp = new ArrayList();
            //if (this.CheckIsAST(order.Usage.ID))
            //{
            //    //判断只有医嘱开始当天才收取皮试费用
            //    if (order.BeginTime.Date == this.subMgr.GetDateTimeFromSysDateTime().Date)
            //    {
            //        if (this.DealSubjobByUsage(isRealTime, "06", order, alOrders, ref alTemp, ref errInfo) == -1)
            //        {
            //            return -1;
            //        }
            //    }
            //}

            //alSubOrders.AddRange(alTemp);

            #region 组合费

            #endregion

            #region 皮试费

            #endregion

            #region 其他费

            #endregion

            return 1;
        }

        /// <summary>
        /// 要收取的附材的日期
        /// </summary>
        private DateTime feeDate;

        /// <summary>
        /// 要收取的附材的日期
        /// </summary>
        public DateTime FeeDate
        {
            get
            {
                return feeDate;
            }
            set
            {
                feeDate = value;
            }
        }

        #endregion

        #region IDealSubjob 成员

        bool isPopForChose = false;
        public bool IsPopForChose
        {
            get
            {
                return this.isPopForChose;
            }
            set
            {
                this.isPopForChose = value;
            }
        }

        #endregion

        #region IDealSubjob 成员


        public int DealSubjob(FS.HISFC.Models.Registration.Register r, ArrayList alOrders, FS.HISFC.Models.Order.OutPatient.Order outOrder, ref ArrayList alSubOrders, ref string errText)
        {
            throw new NotImplementedException();
        }

        public bool IsAllowUsageSubPopChoose
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

    /// <summary>
    /// 医嘱按照序号sortid排序
    /// </summary>
    public class OrderCompareBySortID : IComparer
    {
        #region IComparer 成员

        public int Compare(object x, object y)
        {
            try
            {
                FS.HISFC.Models.Order.Inpatient.Order order1 = x as FS.HISFC.Models.Order.Inpatient.Order;
                FS.HISFC.Models.Order.Inpatient.Order order2 = y as FS.HISFC.Models.Order.Inpatient.Order;
                if (order1.SortID > order2.SortID)
                {
                    return 1;
                }
                else if (order1.SortID == order2.SortID)
                {
                    return string.Compare(order1.ID, order2.ID);
                }
                else
                {
                    return -1;
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
