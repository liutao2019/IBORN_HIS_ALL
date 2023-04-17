using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.DrugStore.GuangZhou.GYZL.Compound
{
    public class ICompoundJudgeImplment:FS.HISFC.BizProcess.Interface.Pharmacy.ICompoundJudge
    {

        public FS.FrameWork.Models.NeuObject CurCompoundDept
        {
            get
            {
                ArrayList alCompoundDept = this.consMgr.GetAllList("CompoundDept");

                if (alCompoundDept != null && alCompoundDept.Count > 0)
                {
                    FS.FrameWork.Models.NeuObject deptObj = alCompoundDept[0] as FS.FrameWork.Models.NeuObject;
                   return deptObj;
                }
                return null;

            }
        }

        /// <summary>
        /// 常数管理类
        /// </summary>
        public FS.HISFC.BizLogic.Manager.Constant consMgr = new FS.HISFC.BizLogic.Manager.Constant();

        public FS.SOC.HISFC.BizLogic.Pharmacy.Storage itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Storage();

        public FS.SOC.HISFC.BizLogic.Pharmacy.Compound compoundMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Compound();

        public FS.SOC.HISFC.BizLogic.Pharmacy.Constant consManager = new FS.SOC.HISFC.BizLogic.Pharmacy.Constant();

        #region ICompoundJudge 成员

        #endregion

        #region ICompoundJudge 成员
        /// <summary>
        /// 广医肿瘤用来发送到配置中心的项目组合
        /// </summary>
        /// <param name="execOrderList"></param>
        /// <param name="hsComb"></param>
        /// <returns></returns>
        public int GetComboItems(ArrayList execOrderList, DateTime dtNow,ref Hashtable hsComb)
        {
            if (execOrderList == null || execOrderList.Count == 0)
            {
                return -1;
            }

            Hashtable hsUsage = new Hashtable();

            string dateTime = string.Empty;

            ArrayList allLackCombo = new ArrayList();

            //配置中心发送时间判断,如果发送申请的时间大于设置的时间，则不发送到配置中心
            ArrayList alCompoundTime = this.consMgr.GetAllList("CompoundSendTime");

            if (alCompoundTime != null && alCompoundTime.Count > 0)
            {
                FS.FrameWork.Models.NeuObject deptObj = alCompoundTime[0] as FS.FrameWork.Models.NeuObject;

                dateTime = dtNow.ToString("yyyy - MM - dd")  + " " + deptObj.Name;

                //出异常的情况直接返回，避免因为维护错误影响业务正常运行
                try
                {
                    DateTime compoundEndTime = FS.FrameWork.Function.NConvert.ToDateTime(dateTime);
                    if (dtNow > compoundEndTime)
                    {
                        return 1;
                    }
                }
                catch (Exception ex)
                {
                    return 1;
                }
            }

            ArrayList alCompoundUsage = this.consMgr.GetAllList("CompoundUsage");

            if(alCompoundUsage != null && alCompoundUsage.Count != 0)
            {
                foreach(FS.FrameWork.Models.NeuObject objUsage in alCompoundUsage)
                {
                    hsUsage.Add(objUsage.ID,objUsage);
                }
            }

            ArrayList allCombo = new ArrayList();

            hsComb = new Hashtable();

            //默认放疗药品都发送至配置中心
            foreach (FS.HISFC.Models.Order.ExecOrder order in execOrderList)
            {
                 if (hsComb.Contains(order.Order.Combo.ID))
                    {
                        ArrayList execOrderListByComboNO = hsComb[order.Order.Combo.ID] as ArrayList;

                        ArrayList execOrderListByComboNOTmp = execOrderListByComboNO.Clone() as ArrayList;

                        foreach (FS.HISFC.Models.Order.ExecOrder orderTmp in execOrderListByComboNOTmp)
                        {
                            if (orderTmp.Order.ID == order.Order.ID)
                            {
                                continue;
                            }

                            execOrderListByComboNO.Add(orderTmp);

                            hsComb[order.Order.Combo.ID] = execOrderListByComboNO;
                        }
                     continue;
                    }

                FS.HISFC.Models.Pharmacy.Item itemTmp = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Order.Item.ID);

                if (FS.FrameWork.Function.NConvert.ToBoolean(itemTmp.SpecialFlag))
                {
                    ArrayList execOrderListByComboNO = new ArrayList();

                    allCombo.Add(order.Order.Combo.ID);

                    execOrderListByComboNO.Add(order);

                    hsComb.Add(order.Order.Combo.ID, execOrderListByComboNO);

                    continue;
                }

                if (hsUsage != null && hsUsage.Count > 0)
                {
                    if (hsUsage.Contains(order.Order.Usage.ID))
                    {
                        ArrayList execOrderListByComboNO = new ArrayList();

                        allCombo.Add(order.Order.Combo.ID);

                        execOrderListByComboNO.Add(order);

                        hsComb.Add(order.Order.Combo.ID, execOrderListByComboNO);

                        continue;
                        
                    }
                }
                int param = this.JudgeStockInfo(this.CurCompoundDept, order);

                if (param == -1)
                {
                    allLackCombo.Add(order.Order.Combo.ID);
                }
            }

    

            //排除所有组合内项目数为1的情况
            if (hsComb != null && hsComb.Count > 0)
            {
                foreach (Object combo in allCombo)
                {
                    ArrayList allData = hsComb[combo] as ArrayList;

                    if (allData.Count <= 1)
                    {
                        hsComb.Remove(combo);
                    }
                }
            }

            //排除所有库存不足的组合项目
            if (allLackCombo != null && allLackCombo.Count > 0)
            {
                foreach (string lackCombo in allLackCombo)
                {
                    if (hsComb.Contains(lackCombo))
                    {
                        hsComb.Remove(lackCombo);
                    }
                }
            }
            return 1;
        }

        /// <summary>
        /// 判断项目库存情况
        /// </summary>
        /// <param name="deptObj"></param>
        /// <param name="applyInfo"></param>
        /// <returns></returns>
        public int JudgeStockInfo(FS.FrameWork.Models.NeuObject neuDept, FS.HISFC.Models.Order.ExecOrder execOrder)
        {
            if (neuDept == null|| string.IsNullOrEmpty(neuDept.ID))
            {
                return -1;
            }
            if (execOrder == null || string.IsNullOrEmpty(execOrder.Order.Item.ID))
            {
                return -1;
            }
            FS.HISFC.Models.Pharmacy.Storage storage = this.itemMgr.GetStockInfoByDrugCode(neuDept.ID, execOrder.Order.Item.ID);

            if (storage == null || string.IsNullOrEmpty(storage.Item.ID))
            {
                return -1;
            }
            if (storage.ValidState != FS.HISFC.Models.Base.EnumValidState.Valid)
            {
                return -1;
            }
            if (storage.IsLackForInpatient)
            {
                return -1;
            }
            //此处应当考虑到预扣库存的情况
            decimal validStoreQty = storage.StoreQty;
           
            validStoreQty = storage.StoreQty - storage.PreOutQty;

            if (validStoreQty < execOrder.Order.Qty)
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 设置需要发药到配置中心的项目
        /// </summary>
        /// <param name="applyOut"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public int SetCompoundApply(FS.HISFC.Models.Pharmacy.ApplyOut applyOut, ref string errInfo)
        {
            if (applyOut == null)
            {
                errInfo = "出库申请实体为空或者处方号为空，请联系信息科";

                return -1;
            }

            applyOut.StockDept.ID = this.CurCompoundDept.ID;

            applyOut.StockDept.Name = this.CurCompoundDept.Name;

            string checkState = this.compoundMgr.GetCompoundState(applyOut);

            if (checkState == "0")
            {
                    applyOut.State = "C";
            }
            else
            {
                    applyOut.State = "0";
            }

                applyOut.CompoundGroup = consManager.GetOrderGroup(applyOut.UseTime);

                if (applyOut.CompoundGroup == null)
                {
                    applyOut.CompoundGroup = "4";
                }

                applyOut.CompoundGroup = applyOut.CompoundGroup + applyOut.UseTime.ToString("yyMMdd") + applyOut.CombNO + "C";
                return 1;
        }
        #endregion
    }
}
