using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace FS.SOC.Local.Pharmacy.Extend.BeiJiao
{
    public class BizExtendInterfaceImplement: FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IPharmacyBizExtend
    {
        ArrayList alPlan = new ArrayList();

        #region IPharmacyBizExtend 成员 其它

        public int AfterSave(string class2Code, string class3code, System.Collections.ArrayList alData, ref string errInfo)
        {
            return 1;
        }

        public FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting GetChooseDataSetting(string class2Code, string class3MeaningCode, string class3Code, string listType, ref string errInfo)
        {
            //使用核心默认的
            FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting chooseDataSetting = new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting();
            chooseDataSetting.IsDefault = true;
            return chooseDataSetting;
        }

        public uint GetCostDecimals(string class2Code, string class3MeaningCode, string type)
        {
            return 2;
        }

        public System.Collections.ArrayList SetCheckDetail(string stockDeptNO)
        {
            //返回null使用核心默认的
            return null;
        }
        #endregion

        #region IPharmacyBizExtend 成员 内部入库申请
        public System.Collections.ArrayList SetInnerInputApply(string applyDeptNO, string stockDeptNO, System.Collections.ArrayList alData)
        {
            using (frmSetPlan frmSetPlan = new frmSetPlan())
            {
                frmSetPlan.SetCompletedEven -= new frmSetPlan.SetCompletedHander(frmSetPlan_SetCompletedHander);
                frmSetPlan.SetCompletedEven += new frmSetPlan.SetCompletedHander(frmSetPlan_SetCompletedHander);
                frmSetPlan.Init(applyDeptNO, FS.HISFC.Models.Pharmacy.EnumDrugStencil.Apply);
                frmSetPlan.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                frmSetPlan.ShowDialog();


                if (alPlan == null)
                {
                    MessageBox.Show("生成内部入库申请发生错误", "错误>>");
                    return null;
                }
                if (alData != null && alData.Count > 0)
                {
                    //界面加载了项目，根据现有项目生成计划
                    for (int index1 = 0; index1 < alData.Count; index1++)
                    {
                        FS.HISFC.Models.Pharmacy.ApplyOut plan1 = alData[index1] as FS.HISFC.Models.Pharmacy.ApplyOut;
                        for (int index2 = 0; index2 < alPlan.Count; index2++)
                        {
                            FS.HISFC.Models.Pharmacy.InPlan plan2 = alPlan[index2] as FS.HISFC.Models.Pharmacy.InPlan;
                            if (plan1.Item.ID == plan2.Item.ID)
                            {
                                plan1.ExtFlag = plan2.OutputQty.ToString();//消耗量
                                plan1.ExtFlag1 = plan2.Extend;//参考量

                                alPlan.RemoveAt(index2);

                                break;
                            }
                        }
                    }
                    return alData;
                }
                else
                {
                    ArrayList alAppply = new ArrayList();
                    foreach (FS.HISFC.Models.Pharmacy.InPlan plan in alPlan)
                    {
                        FS.HISFC.Models.Pharmacy.ApplyOut applyOut = new FS.HISFC.Models.Pharmacy.ApplyOut();
                        applyOut.Item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(plan.Item.ID);

                        applyOut.StockDept.ID = stockDeptNO;
                        applyOut.ApplyDept.ID = applyDeptNO;
                        applyOut.ExtFlag = plan.OutputQty.ToString();//消耗量
                        applyOut.ExtFlag1 = plan.Extend;//参考量

                        applyOut.Class2Type = "0310";
                        applyOut.PrivType = "02";
                        applyOut.SystemType = "13";

                        applyOut.State = "0";                                   //状态 申请
                        applyOut.ShowState = "1";
                        applyOut.ShowUnit = applyOut.Item.PackUnit;

                        alAppply.Add(applyOut);
                    }

                    return alAppply;
                }
            }
        }
        #endregion

        #region IPharmacyBizExtend 成员 入库计划
        public System.Collections.ArrayList SetInputPlan(string stockDeptNO, System.Collections.ArrayList alData)
        {
            using(frmSetPlan frmSetPlan = new frmSetPlan())
            {
                frmSetPlan.SetCompletedEven -= new frmSetPlan.SetCompletedHander(frmSetPlan_SetCompletedHander);
                frmSetPlan.SetCompletedEven += new frmSetPlan.SetCompletedHander(frmSetPlan_SetCompletedHander);
                frmSetPlan.Init(stockDeptNO, FS.HISFC.Models.Pharmacy.EnumDrugStencil.Plan);
                frmSetPlan.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                frmSetPlan.ShowDialog();

                if (alPlan == null)
                {
                    MessageBox.Show("生成入库计划发生错误", "错误>>");
                    return null;
                }
                if (alData != null && alData.Count > 0)
                {
                    //界面加载了项目，根据现有项目生成计划
                    for (int index1 = 0; index1 < alData.Count; index1++)
                    {
                        FS.HISFC.Models.Pharmacy.InPlan plan1 = alData[index1] as FS.HISFC.Models.Pharmacy.InPlan;
                        for (int index2 = 0; index2 < alPlan.Count; index2++)
                        {
                            FS.HISFC.Models.Pharmacy.InPlan plan2 = alPlan[index2] as FS.HISFC.Models.Pharmacy.InPlan;
                            if (plan1.Item.ID == plan2.Item.ID)
                            {
                                plan1.PlanQty = plan2.PlanQty;
                                plan1.Formula = plan2.Formula;
                                plan1.Extend = plan2.Extend;//公式生成的参考计划量，将保存到数据库字段
                                alPlan.RemoveAt(index2);
                                break;
                            }
                        }
                    }
                    return alData;
                }
                else
                {
                    foreach (FS.HISFC.Models.Pharmacy.InPlan plan in alPlan)
                    {
                        plan.Item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(plan.Item.ID);
                        plan.Dept.ID = stockDeptNO;
                        //plan.Extend;//公式生成的参考计划量，将保存到数据库字段
                        if (string.IsNullOrEmpty(plan.Item.Product.Company.ID) && plan.Item.TenderOffer.IsTenderOffer)
                        {
                            plan.Company = plan.Item.TenderOffer.Company;
                        }
                        else
                        {
                            plan.Company = plan.Item.Product.Company;
                        }
                        if (plan.Company != null && !string.IsNullOrEmpty(plan.Company.ID))
                        {
                            plan.Company.Name = SOC.HISFC.BizProcess.Cache.Pharmacy.GetCompanyName(plan.Company.ID);
                        }
                    }
                }
            }
            return alPlan;
        }
        
        #endregion

        #region IPharmacyBizExtend 成员 入出库单号


        public string GetBillNO(string stockDeptNO, string class2Code, string class3code, ref string errInfo)
        {
            FS.FrameWork.Management.ExtendParam extentManager = new FS.FrameWork.Management.ExtendParam();

            string ListNO = "";
            decimal iSequence = 0;
            DateTime sysDate = extentManager.GetDateTimeFromSysDateTime();

            string inOutFlag = "";
            string inOutListCode = "";
            if (class2Code == "0310")
            {
                inOutFlag = "1";
                inOutListCode = "InListCode";
            }
            else
            {
                inOutFlag = "2";
                inOutListCode = "OutListCode";
            }

            //获取当前科室的单据最大流水号
            FS.HISFC.Models.Base.ExtendInfo deptExt = extentManager.GetComExtInfo(FS.HISFC.Models.Base.EnumExtendClass.DEPT, inOutListCode, stockDeptNO);
            if (deptExt == null)
            {
                return null;
            }
            else
            {
                if (deptExt.Item.ID == "")          //当前科室尚无记录 流水号置为1
                {
                    iSequence = 1;
                }
                else                                //当前科室存在记录 根据日期是否为当天 确定流水号是否归1
                {
                    if (deptExt.DateProperty.Month != sysDate.Month)
                    {
                        iSequence = 1;
                    }
                    else
                    {
                        iSequence = deptExt.NumberProperty + 1;
                    }
                }
                //生成单据号

                ListNO = stockDeptNO + sysDate.Year.ToString().Substring(2, 2) + sysDate.Month.ToString().PadLeft(2, '0') + inOutFlag + iSequence.ToString().PadLeft(4, '0');

                //保存当前最大流水号
                deptExt.Item.ID = stockDeptNO;
                deptExt.DateProperty = sysDate;
                deptExt.NumberProperty = iSequence;
                deptExt.PropertyCode = inOutListCode;
                deptExt.PropertyName = "科室单据号最大流水号";

                if (extentManager.SetComExtInfo(deptExt) == -1)
                {
                    return null;
                }
            }
            return ListNO;



            #region 默认的，注释掉
            //string billNO = "";

            //FS.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

            //billNO = phaIntegrate.GetInOutListNO(stockDeptNO, (class2Code == "0310"));
            //if (billNO == null)
            //{
            //    errInfo = "获取最新入库单号出错" + phaIntegrate.Err;
            //    return "-1";
            //}
            //return billNO;
            #endregion
        }

        #endregion

        #region IPharmacyBizExtend 成员 入库录入信息


        public FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IInputInfoControl GetInputInfoControl(string class3code, bool isSpecial, FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IInputInfoControl defaultInputInfoControl)
        {
            if (isSpecial)
            {
                return defaultInputInfoControl;
            }
            else
            {
                return new ucCommonInput();
            }
        }

        #endregion

        #region IPharmacyBizExtend 成员 药品基本信息扩展

        public FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IItemExtendControl GetItemExtendControl(FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IItemExtendControl defaultItemExtendControl)
        {
            return defaultItemExtendControl;
        }

        #endregion

        void frmSetPlan_SetCompletedHander(frmSetPlan.CreatePlanType type, string formula, params string[] param)
        {
            if (type == frmSetPlan.CreatePlanType.Consume)
            {
                SetPlan setPlanMgr = new SetPlan();
                //GetPlan(string deptNO, DateTime consumeBeginTime, DateTime consumeEndTime, int lowDays, int upDays, string drugType, string stencilNO)
                alPlan = setPlanMgr.GetPlan(param[0],
                    FS.FrameWork.Function.NConvert.ToDateTime(param[1]),
                    FS.FrameWork.Function.NConvert.ToDateTime(param[2]),
                    FS.FrameWork.Function.NConvert.ToInt32(param[3]),
                    FS.FrameWork.Function.NConvert.ToInt32(param[4]),
                    param[5],
                    param[6]
                    );
            }
            else if (type == frmSetPlan.CreatePlanType.Warning)
            {
                SetPlan setPlanMgr = new SetPlan();
                //GetPlan(string deptNO, string drugType, string stencilNO)
                alPlan = setPlanMgr.GetPlan(param[0], param[1], param[2]);
            }
        }
    }
}
