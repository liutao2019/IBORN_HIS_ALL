using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.DrugStore.GuangZhou.GYZL.Compound
{
    public class CompoundFeeImplement : FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.ICompoundFee
    {
        #region ICompoundFee 成员

        public ArrayList injectRulesCons = new ArrayList();

        public string err = string.Empty;

        public FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();

        FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

        FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        public string Err
        {
            get
            {
                return this.err;
            }
            set
            {
                this.err = value;
            }
        }

        public ArrayList InjectRulesCons
        {
            get
            {
                injectRulesCons = consManager.GetAllList("InjectRuleCons");
                return injectRulesCons;
            }
            set
            {
                this.injectRulesCons = value;
            }
        }

        public int GetCompoundFeeList(System.Collections.ArrayList applyList, ref System.Collections.ArrayList feeList)
        {
            throw new NotImplementedException();
        }

        public int SaveFee(System.Collections.ArrayList alApplyOut, FS.FrameWork.Models.NeuObject execDept, System.Data.IDbTransaction trans)
        {

            #region 事务记录


            #endregion

            #region 形成待收费数据

            ArrayList alGroupApplyOut = new ArrayList();
            ArrayList alCombo = new ArrayList();
            //存储相同批次的出库申请项目
            Hashtable hsGroupApplyOut = new Hashtable();
            FS.SOC.Local.DrugStore.GuangZhou.GYZL.Compound.AdditionalItem AdditionalItemManagement = new FS.SOC.Local.DrugStore.GuangZhou.GYZL.Compound.AdditionalItem();

            #region 按批次形成数据

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alApplyOut)
            {
                if (hsGroupApplyOut.Contains(info.CompoundGroup))        //与上一条是同一批次流水
                {
                    ArrayList allData = (hsGroupApplyOut[info.CompoundGroup] as ArrayList).Clone() as ArrayList;
                    allData.Add(info);
                    hsGroupApplyOut[info.CompoundGroup] = allData;
                    continue;
                }
                else			//不同批次流水号
                {
                    ArrayList allData = new ArrayList();
                    allData.Add(info);
                    hsGroupApplyOut.Add(info.CompoundGroup, allData);

                    alGroupApplyOut.Add(info);
                }
            }

            #endregion

            #endregion

            System.Collections.Hashtable hsPatientInfo = new Hashtable();

            //alGroupApplyOut数组为每一批次+医嘱组合号保存一条数据
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alGroupApplyOut)
            {
                //查询获取患者基本信息
                FS.HISFC.Models.RADT.PatientInfo patient = radtIntegrate.QueryPatientInfoByInpatientNO(info.PatientNO);
                if (patient == null)
                {
                    this.Err = radtIntegrate.Err;
                    return -1;
                }
                patient.User01 = "1";

                //查询获取用法对应的附材
                ArrayList alList = AdditionalItemManagement.QueryAdditionalItem(false, info.Usage.ID, execDept.ID);
                if (alList == null)
                {
                    this.Err = consManager.Err;
                    return -1;
                }
                if (alList.Count > 0)
                {
                    //对于无效的常数维护项目 不进行收费
                    for (int i = 0; i < alList.Count; i++)
                    {
                        FS.HISFC.Models.Base.Item item = new FS.HISFC.Models.Base.Item();
                        FS.HISFC.Models.Base.Item tempTtem = new FS.HISFC.Models.Base.Item();
                        tempTtem = alList[i] as FS.HISFC.Models.Base.Item;

                        if (tempTtem == null)
                        {
                            this.Err = "未设置配置中心需收费的项目 无法完成费用自动收取";
                            return -1;
                        }

                        item = feeIntegrate.GetItem(tempTtem.ID);
                        if (item == null)
                        {
                            this.Err = "未设置配置中心需收费的项目 无法完成费用自动收取";
                            return -1;
                        }

                        //准备收费
                        item.Qty = FS.FrameWork.Function.NConvert.ToDecimal(tempTtem.Qty);

                        if (patient.PVisit.InState.ID.ToString() != FS.HISFC.Models.Base.EnumInState.I.ToString())
                        {
                            this.Err = info.Name + " 患者非在院状态，不能进行配置费收取操作";
                            return -1;
                        }

                        if (feeIntegrate.FeeAutoItem(patient, item, execDept.ID) == -1)
                        {
                            this.Err = feeIntegrate.Err;
                            return -1;
                        }

                        FS.HISFC.Models.Base.Item syringeItem = this.GetSyringe(hsGroupApplyOut[info.CompoundGroup] as ArrayList);
                        if (syringeItem == null)
                        {
                            this.Err = "没有获取到注射器带出项目，请联系信息科！";
                            return -1;
                        }
                        if (feeIntegrate.FeeAutoItem(patient, syringeItem, execDept.ID) == -1)
                        {
                            this.Err = feeIntegrate.Err;
                            return -1;
                        }
                    }
                }
                else
                {
                    this.Err = info.Usage.Name+"未设置配置中心需收费的项目 无法完成费用自动收取";
                    return -1;
                }
            }
            return 1;
        }

        public List<string> SetFeeState(System.Collections.ArrayList alApply)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 注射器收取规则
        /// </summary>
        /// <param name="applyData"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Base.Item GetSyringe(ArrayList applyData)
        {
            int basedose = 0;

            //如果都不满足，则带出默认的辅材
            string defaultItem = string.Empty;


            if (applyData == null || applyData.Count == 0)
            {
                return null;
            }

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut appInfo in applyData)
            {
                if (appInfo.Item.Quality.ID == "T")
                {
                    continue;
                }
                if (appInfo.Item.DoseUnit == "ml")
                {
                    basedose += FS.FrameWork.Function.NConvert.ToInt32(appInfo.DoseOnce);
                }
                else
                {
                    if (appInfo.Item.SecondDoseUnit == "ml")
                    {
                        basedose += FS.FrameWork.Function.NConvert.ToInt32((appInfo.Item.SecondBaseDose / appInfo.Item.BaseDose) * appInfo.DoseOnce);
                    }
                }
            }

            if (this.InjectRulesCons == null || this.InjectRulesCons.Count == 0)
            {
                this.Err = "没有维护注射器带出规则常数！";
                return null;
            }
            foreach (FS.FrameWork.Models.NeuObject injectRulesCon in this.InjectRulesCons)
            {
                defaultItem = injectRulesCon.Name;
                string[] strTmp = injectRulesCon.ID.Split('-');
                if (strTmp.Length != 2)
                {
                    this.Err = "注射器规则维护错误！";
                    return null;
                }
                int minValue = -1;
                int maxValue = -1;
                try
                {
                    minValue = FS.FrameWork.Function.NConvert.ToInt32(strTmp[0]);
                    maxValue = FS.FrameWork.Function.NConvert.ToInt32(strTmp[1]);
                }
                catch (Exception ex)
                {
                    this.Err = "格式化注射器规则对应的边界值错误，请核对！" + ex.Message;
                    return null;
                }
                if (minValue < basedose && maxValue >= basedose)
                {
                    string itemCode = injectRulesCon.Name;
                    if (!string.IsNullOrEmpty(itemCode))
                    {
                        FS.HISFC.Models.Base.Item item = this.feeIntegrate.GetItem(itemCode);
                        if (item == null)
                        {
                            return null;
                        }
                        return item;
                    }
                }
            }
            return this.feeIntegrate.GetItem(defaultItem);
        }

        #endregion
    }
}
